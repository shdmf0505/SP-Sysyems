#region using

using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting;

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using Micube.Framework.SmartControls.Validations;
using DevExpress.XtraPrinting.Drawing;
using System.Runtime.InteropServices;
using System.Net;
using Micube.SmartMES.Commons.Controls;
using System.Xml;

#endregion

namespace Micube.SmartMES.Commons
{
    /// <summary>
    /// MES 시스템에서 공통으로 사용하는 함수를 관리하는 클래스
    /// </summary>
    public static class CommonFunction
    {
      
        #region ◆ 공통 함수 ::: 

        #region - ValidationProduct :: 동일 품목코드, 품목버전 체크 |
        /// <summary>
        /// 동일 품목코드, 품목버전 체크
        /// </summary>
        /// <param name="currentGridRow"></param>
        /// <param name="popupSelections"></param>
        /// <returns></returns>
        private static ValidationResultCommon ValidationProduct(DataRow currentGridRow, IEnumerable<DataRow> popupSelections)
        {
            ValidationResultCommon result = new ValidationResultCommon();

            int productDefId = popupSelections.AsEnumerable().Select(r => Format.GetString(r["PRODUCTDEFID"])).Distinct().Count();
            if (productDefId > 1)
            {
                Language.LanguageMessageItem item = Language.GetMessage("MixSelectProductDefID");
                result.IsSucced = false;
                result.FailMessage = item.Message;
                result.Caption = item.Title;
            }

            int productDefVer = popupSelections.AsEnumerable().Select(r => Format.GetString(r["PRODUCTDEFVERSION"])).Distinct().Count();
            if (productDefId == 1 && productDefVer > 1)
            {
                Language.LanguageMessageItem item = Language.GetMessage("MixSelectProductDefVersion");
                result.IsSucced = false;
                result.FailMessage = item.Message;
                result.Caption = item.Title;
            }

            return result;
        }
        #endregion

        #region - setLabelLaungage :: 다국어 명 적용 |
        /// <summary>
        /// 다국어 명 적용
        /// </summary>
        /// <param name="band"></param>
        private static void setLabelLaungage(object band, string strGroupHeader = "GroupHeader1")
        {
            if (band is DetailReportBand)
            {
                DetailReportBand detailReport = band as DetailReportBand;
                Band groupHeader = detailReport.Bands[strGroupHeader];

                foreach (XRControl control in groupHeader.Controls)
                {
                    if (control is DevExpress.XtraReports.UI.XRLabel)
                    {
                        if (!string.IsNullOrEmpty(control.Tag.ToString()))
                        {
                            if (control.Name.Substring(0, 3).Equals("lbl"))
                            {
                                string bindText = Language.Get(control.Tag.ToString());

                                if (control is XRLabel label && label.Angle == 0)
                                {
                                    Font ft = BestSizeEstimator.GetFontToFitBounds(control as XRLabel, bindText);
                                    if (ft.Size < control.Font.Size)
                                    {
                                        control.Font = ft;
                                    }
                                }

                                control.Text = bindText;
                            }
                        }
                    }
                    else if (control is DevExpress.XtraReports.UI.XRTable)
                    {
                        XRTable xt = control as XRTable;

                        foreach (XRTableRow tr in xt.Rows)
                        {
                            for (int i = 0; i < tr.Cells.Count; i++)
                            {
                                if (!string.IsNullOrEmpty(tr.Cells[i].Tag.ToString()))
                                {
                                    tr.Cells[i].Text = Language.Get(tr.Cells[i].Tag.ToString());

                                }

                            }
                        }

                    }
                }

            }

        }
        #endregion

        #region - changeArgString :: SQL Injection 문자열 대체 |
        /// <summary>
        /// SQL Injection 문자열 대체
        /// </summary>
        /// <param name="sArgStr"></param>
        /// <returns></returns>
        public static string changeArgString(string sArgStr)
        {
            sArgStr = sArgStr.Replace("&quot;", "＂");
            sArgStr = sArgStr.Replace("&lt;", "＜");
            sArgStr = sArgStr.Replace("&gt;", "＞");
            sArgStr = sArgStr.Replace("'", "＇");
            sArgStr = sArgStr.Replace("<", "＜");
            sArgStr = sArgStr.Replace(">", "＞");
            //sArgStr = sArgStr.Replace("--", "――");
            sArgStr = sArgStr.Replace("&", "＆");
            sArgStr = sArgStr.Replace(";", "；");
            //sArgStr = sArgStr.Replace("*", "＊");
            //sArgStr = sArgStr.Replace("%", "％");
            sArgStr = sArgStr.Replace("+", "＋");
            sArgStr = sArgStr.Replace("=", "＝");
            sArgStr = sArgStr.Replace("\"", "＂");
            sArgStr = sArgStr.Replace("\\", "￦");
            sArgStr = sArgStr.Replace("^", "＾");
            sArgStr = sArgStr.Replace("?", "？");
            sArgStr = sArgStr.Replace("!", "！");
            sArgStr = sArgStr.Replace("@", "＠");
            sArgStr = sArgStr.Replace(",", "，");

            return sArgStr;
        }
        #endregion

        #region - TableDataDivide :: stdCnt부터 interval 간격으로 데이터 나눔 |
        /// <summary>
        /// stdCnt부터 interval 간격으로 데이터 나눔
        /// </summary>
        /// <param name="target"></param>
        /// <param name="standard"></param>
        /// <param name="stdCnt"></param>
        /// <returns></returns>
        private static DataTable TableDataDivide(DataTable target, DataTable input, int stdCnt, int interval)
        {
            for (int i = stdCnt; i < (stdCnt + interval); i++)
            {
                DataRow newRow = input.NewRow();
                newRow.ItemArray = target.Rows[i].ItemArray;
                input.Rows.Add(newRow);
            }

            return input;
        }
        #endregion

        #region - GetFtpImageFileToByte :: FTP에 업로드 된 이미지 파일을 Byte Array로 가져옴
        /// <summary>
        /// FTP에 업로드 된 이미지 파일을 Byte Array로 가져옴
        /// </summary>
        /// <param name="ftpFilePath">FTP 하위 파일 경로 (Sf_ObjectFile 테이블의 FilePath 필드 값)</param>
        /// <param name="fileName">확장자 포함 전체 파일명 (ex> ErrorImage.png)</param>
        /// <returns></returns>
        public static byte[] GetFtpImageFileToByte(string ftpFilePath, string fileName)
        {
            string ftpServerPath = Cryptography.AESDecrypt(AppConfiguration.GetString("Application.Ftp.Url"));
            string ftpServerUserId = Cryptography.AESDecrypt(AppConfiguration.GetString("Application.Ftp.Id"));
            string ftpServerPassword = Cryptography.AESDecrypt(AppConfiguration.GetString("Application.Ftp.Password"));

            string ftpFileFullPath = ftpServerPath + ftpFilePath + (ftpFilePath.EndsWith("/") ? "" : "/") + fileName;

            WebClient ftpClient = new WebClient();
            ftpClient.Credentials = new NetworkCredential(ftpServerUserId, ftpServerPassword);

            byte[] imageByte = ftpClient.DownloadData(ftpFileFullPath);

            ftpClient.Dispose();

            return imageByte;
        }

        /// <summary>
        /// FTP에 업로드 된 이미지 파일을 Byte Array로 가져옴
        /// </summary>
        /// <param name="ftpFilePath">FTP 하위 파일 경로 (Sf_ObjectFile 테이블의 FilePath 필드 값) + 확장자 포함 전체 파일명 (ex> SelfTakeInspection/ErrorImage.png)</param>
        /// <param name="isServerPath">true : yml ServerPath / false : ftpFilePath 포함</param>
        /// <returns></returns>
        /// [2020-03-16] 전우성 : 설비인터페이스의 경우 FilePath에 FullPath 표시됨
        public static byte[] GetFtpImageFileToByte(string ftpFilePath, bool isServerPath = true)
        {
            string ftpServerPath = Cryptography.AESDecrypt(AppConfiguration.GetString("Application.Ftp.Url"));
            string ftpServerUserId = Cryptography.AESDecrypt(AppConfiguration.GetString("Application.Ftp.Id"));
            string ftpServerPassword = Cryptography.AESDecrypt(AppConfiguration.GetString("Application.Ftp.Password"));

            string ftpFileFullPath = isServerPath ? ftpServerPath + ftpFilePath : ftpFilePath;

            byte[] imageByte = null;

            try
            {
                WebClient ftpClient = new WebClient();
                ftpClient.Credentials = new NetworkCredential(ftpServerUserId, ftpServerPassword);

                imageByte = ftpClient.DownloadData(ftpFileFullPath);

                ftpClient.Dispose();
            }
            catch
            {

            }

            return imageByte;
        }

        #endregion

        #region - GetFtpImageFileToBitmap :: FTP에 업로드 된 이미지 파일을 Bitmap으로 가져옴
        /// <summary>
        /// FTP에 업로드 된 이미지 파일을 Bitmap으로 가져옴
        /// </summary>
        /// <param name="ftpFilePath">FTP 하위 파일 경로 (Sf_ObjectFile 테이블의 FilePath 필드 값)</param>
        /// <param name="fileName">확장자 포함 전체 파일명 (ex> ErrorImage.png)</param>
        /// <returns></returns>
        public static Bitmap GetFtpImageFileToBitmap(string ftpFilePath, string fileName)
        {
            byte[] bytes = CommonFunction.GetFtpImageFileToByte(ftpFilePath, fileName);

            List<string> imgTypes = new List<string> { "jpg", "JPG", "jpeg", "JPEG", "bmp", "BMP", "gif", "GIF", "png", "PNG" };

            Bitmap image = null;
            // 2021.02.23 전우성 fileName.Substring 시 3자리로 자르는 경우 4자리 확장자는 이미지가 안나오는 오류 수정
            if (imgTypes.Contains(fileName.Substring(fileName.LastIndexOf('.') + 1)))
            {
                image = new Bitmap((Bitmap)new ImageConverter().ConvertFrom(bytes), new Size(300, 300));
            }

            return image;
        }

        /// <summary>
        /// FTP에 업로드 된 이미지 파일을 Bitmap으로 가져옴
        /// </summary>
        /// <param name="ftpFilePath">FTP 하위 파일 경로 (Sf_ObjectFile 테이블의 FilePath 필드 값)</param>
        /// <param name="fileName">확장자 포함 전체 파일명 (ex> ErrorImage.png)</param>
        /// <returns></returns>
        public static Bitmap GetFtpImageFileToBitmap(string ftpFilePath, string fileName, int width, int height)
        {
            byte[] bytes = CommonFunction.GetFtpImageFileToByte(ftpFilePath, fileName);

            List<string> imgTypes = new List<string> { "jpg", "JPG", "jpeg", "JPEG", "bmp", "BMP", "gif", "GIF", "png", "PNG" };

            Bitmap image = null;
            if (imgTypes.Contains(fileName.Substring(fileName.LastIndexOf('.') + 1, 3)))
            {
                image = new Bitmap((Bitmap)new ImageConverter().ConvertFrom(bytes), new Size(width, height));
            }

            return image;
        }

        /// <summary>
        /// FTP에 업로드 된 이미지 파일을 Bitmap으로 가져옴
        /// </summary>
        /// <param name="ftpFilePath">FTP 하위 파일 경로 (Sf_ObjectFile 테이블의 FilePath 필드 값) + 확장자 포함 전체 파일명 (ex> SelfTakeInspection/ErrorImage.png)</param>
        /// <param name="isServerPath">true : yml SerarPath / false : ftpFilePath 포함</param>
        /// <returns></returns>
        public static Bitmap GetFtpImageFileToBitmap(string ftpFilePath, bool isServerPath)
        {
            byte[] bytes = GetFtpImageFileToByte(ftpFilePath, isServerPath);

            List<string> imgTypes = new List<string> { "jpg", "JPG", "jpeg", "JPEG", "bmp", "BMP", "gif", "GIF", "png", "PNG" };

            Bitmap image = null;

            String fileName = ftpFilePath.Substring(ftpFilePath.LastIndexOf('/') + 1, ftpFilePath.Length);
            String fileExt = fileName.Substring(0, fileName.LastIndexOf('.'));

            if (imgTypes.Contains(fileExt))
            {
                image = new Bitmap((Bitmap)new ImageConverter().ConvertFrom(bytes), new Size(300, 300));
            }

            return image;
        }
        #endregion

        public static string generateLikeState(string param)
        {
            string[] paramter = param.Split(',');

            string returnParam = string.Empty;

            foreach (string temp in paramter)
            {
                returnParam = returnParam + string.Format("%{0}%,", temp);
            }

            return returnParam.TrimEnd(',')
                ;
        }

        #region -마이그랏 화면에서 분할 채번할 경우 사용
        public static string CreateNewLotid(string MigLot, string materialClass, string materialSequence)
        {
            //20190510 039 0 1A7 0 038 0
            string date = MigLot.Substring(2, 6);

            string inputseq = MigLot.Substring(9, 3);
            string reinputseq = MigLot.Substring(15, 1);
            string lotseq = MigLot.Substring(16, 3);
            string splitseq = MigLot.Substring(MigLot.Length - 1, 1);
            string plantCode = string.Empty;
            switch (UserInfo.Current.Plant)
            {
                case Constants.IFC:
                    plantCode = "F";
                    break;
                case Constants.IFV:
                    plantCode = "V";
                    break;
                case Constants.CCT:
                    plantCode = "C";
                    break;
                case Constants.YPE:
                    plantCode = "Y";
                    break;
                case Constants.YPEV:
                    plantCode = "P";
                    break;

            }
            reinputseq = (int.Parse(reinputseq) + 1).ToString();
            splitseq = (int.Parse(splitseq) + 1).ToString().PadLeft(3, '0');

            return string.Format("{0}{1}{2}-{3}-{4}-{5}-{6}", date, plantCode, inputseq, reinputseq, materialClass + materialSequence, lotseq, splitseq);
        }
        #endregion

        #region - GetBase36String :: 숫자를 36진수 문자열로 변환
        private const int Base = 36;
        private const string Chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public static string GetBase36String(int value)
        {
            string result;

            result = Chars[value % Base].ToString();


            return result;
        }
        #endregion
        #endregion

        #region ◆ 팝업창 및 CodeHelp :::

        #region - AddConditionProductPopup :: 검색조건에 품목 선택 팝업을 추가한다. |
        /// <summary>
        /// 검색조건에 품목 선택 팝업을 추가한다.
        /// </summary>
        /// <param name="id">검색조건 항목에 지정할 ID</param>
        /// <param name="position">검색조건을 추가할 순서</param>
        /// <param name="isMultiSelect">항목 복수 선택 여부</param>
        /// <param name="conditions">화면에서 사용되는 검색조건 컬렉션</param>
        /// <returns></returns>
        public static ConditionCollection AddConditionProductPopup(string id, double position, bool isMultiSelect, ConditionCollection conditions, string displayFieldName = "PRODUCTDEFNAME", string valueFieldName = "PRODUCTDEFID"
            , bool hideVersion = false, int maxCheck = 0, bool isUseProductDivisionCondition = true)
        {
            // SelectPopup 항목 추가
            var conditionProductId = conditions.AddSelectPopup(id, new SqlQuery("GetProductDefinitionList", "10001", $"PLANTID={UserInfo.Current.Plant}"), displayFieldName, valueFieldName)
                .SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupLayoutForm(800, 800)
                .SetPopupAutoFillColumns("UNIT")
                .SetLabel("PRODUCTDEFID")
                .SetPosition(position);

            // 복수 선택 여부에 따른 Result Count 지정
            if (isMultiSelect)
                conditionProductId.SetPopupResultCount(maxCheck);
            else
                conditionProductId.SetPopupResultCount(1);

            // 팝업에서 사용되는 검색조건 (품목코드/명)
            conditionProductId.Conditions.AddTextBox("TXTPRODUCTDEFNAME");
            //제품 구분 추가 2019.08.14 배선용, 노석안 대리 요청
            if (isUseProductDivisionCondition)
                conditionProductId.Conditions.AddComboBox("PRODUCTDIVISION", new SqlQuery("GetTypeList", "10001", $"CODECLASSID=ProductDivision2", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                                            .SetDefault("Product");

            // 팝업 그리드에서 보여줄 컬럼 정의
            // 품목코드
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            // 품목명
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            if (!hideVersion)
            {
                // 품목버전
                conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 100);
            }
            // 품목유형구분
            //conditionProductId.GridColumns.AddComboBoxColumn("PRODUCTDEFTYPE", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductDefType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFTYPE", 90);
            // 생산구분
            //conditionProductId.GridColumns.AddComboBoxColumn("PRODUCTIONTYPE", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTIONTYPE", 90);
            // 단위
            //conditionProductId.GridColumns.AddComboBoxColumn("UNIT", 90, new SqlQuery("GetUomDefinitionList", "10001", "UOMCLASSID=Segment"), "UOMDEFNAME", "UOMDEFID");
            conditionProductId.GridColumns.AddTextBoxColumn("UOMDEFNAME", 90);

            return conditions;
        }
        #endregion

        #region - AddConditionProductPopupPrefixSearch :: 검색조건에 품목 선택 팝업을 추가한다. |
        /// <summary>
        /// 검색조건에 품목 선택 팝업을 추가한다.
        /// </summary>
        /// <param name="id">검색조건 항목에 지정할 ID</param>
        /// <param name="position">검색조건을 추가할 순서</param>
        /// <param name="isMultiSelect">항목 복수 선택 여부</param>
        /// <param name="conditions">화면에서 사용되는 검색조건 컬렉션</param>
        /// <returns></returns>
        public static ConditionCollection AddConditionProductPopupPrefixSearch(string id, double position, bool isMultiSelect, ConditionCollection conditions, string displayFieldName = "PRODUCTDEFNAME", string valueFieldName = "PRODUCTDEFID"
            , bool hideVersion = false, int maxCheck = 0, bool isUseProductDivisionCondition = true)
        {
            // SelectPopup 항목 추가
            var conditionProductId = conditions.AddSelectPopup(id, new SqlQuery("GetProductDefinitionList", "10005", $"PLANTID={UserInfo.Current.Plant}"), displayFieldName, valueFieldName)
                .SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupLayoutForm(800, 800)
                .SetPopupAutoFillColumns("UNIT")
                .SetLabel("PRODUCTDEFID")
                .SetPosition(position);

            // 복수 선택 여부에 따른 Result Count 지정
            if (isMultiSelect)
                conditionProductId.SetPopupResultCount(maxCheck);
            else
                conditionProductId.SetPopupResultCount(1);

            // 팝업에서 사용되는 검색조건 (품목코드/명)
            conditionProductId.Conditions.AddTextBox("TXTPRODUCTDEFNAME");
            //제품 구분 추가 2019.08.14 배선용, 노석안 대리 요청
            if (isUseProductDivisionCondition)
                conditionProductId.Conditions.AddComboBox("PRODUCTDIVISION", new SqlQuery("GetTypeList", "10001", $"CODECLASSID=ProductDivision2", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                                            .SetDefault("Product");

            // 팝업 그리드에서 보여줄 컬럼 정의
            // 품목코드
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            // 품목명
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            if (!hideVersion)
            {
                // 품목버전
                conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 100);
            }
            // 품목유형구분
            //conditionProductId.GridColumns.AddComboBoxColumn("PRODUCTDEFTYPE", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductDefType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFTYPE", 90);
            // 생산구분
            //conditionProductId.GridColumns.AddComboBoxColumn("PRODUCTIONTYPE", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTIONTYPE", 90);
            // 단위
            //conditionProductId.GridColumns.AddComboBoxColumn("UNIT", 90, new SqlQuery("GetUomDefinitionList", "10001", "UOMCLASSID=Segment"), "UOMDEFNAME", "UOMDEFID");
            conditionProductId.GridColumns.AddTextBoxColumn("UOMDEFNAME", 90);

            return conditions;
        }
        #endregion

        #region - ConditionReworkRouting :: ReworkRouting 검색 팝업 |
        /// <summary>
        /// ReworkRouting 검색 팝업
        /// </summary>
        /// <param name="isMultiSelect"></param>
        /// <param name="lotid"></param>
        /// <returns></returns>
        public static ConditionItemSelectPopup ConditionReworkRouting(bool isMultiSelect, string lotid)
        {
            // SelectPopup 항목 추가
            ConditionItemSelectPopup options = new ConditionItemSelectPopup();

            options.SetPopupLayoutForm(800, 1000, FormBorderStyle.Sizable);
            options.SetPopupLayout("REWORKROUTING", PopupButtonStyles.Ok_Cancel, true, true);
            options.Id = "REWORK";
            options.SearchQuery = new SqlQuery("SelectReworkRouting", "10001", $"PLANTID={UserInfo.Current.Plant}", $"P_LOTID={lotid}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}");
            options.IsMultiGrid = isMultiSelect;
            options.DisplayFieldName = "REWORKNAME";
            options.ValueFieldName = "REWORKNUMBER";
            options.LanguageKey = "REWORKPROCESSID";

            options.Conditions.AddComboBox("PROCESSCLASS", new SqlQuery("GetTypeList", "10001", $"CODECLASSID=ProcessClassType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                                        .SetDefault("Process");

            options.GridColumns.AddTextBoxColumn("APPLICATIONTYPE", 70);
            options.GridColumns.AddTextBoxColumn("REWORKTYPE", 70);
            options.GridColumns.AddTextBoxColumn("TOPPROCESSSEGMENTID", 70);
            options.GridColumns.AddTextBoxColumn("REWORKNUMBER", 100);
            options.GridColumns.AddTextBoxColumn("REWORKVERSION", 50);
            options.GridColumns.AddTextBoxColumn("REWORKNAME", 100);

            return options;
        }
        #endregion

        #region - AddConditionProductDistinctPopup :: 검색조건에 품목 선택 팝업을 추가한다 |
        /// <summary>
        /// 검색조건에 품목 선택 팝업을 추가한다.
        /// </summary>
        /// <param name="id">검색조건 항목에 지정할 ID</param>
        /// <param name="position">검색조건을 추가할 순서</param>
        /// <param name="isMultiSelect">항목 복수 선택 여부</param>
        /// <param name="conditions">화면에서 사용되는 검색조건 컬렉션</param>
        /// <returns></returns>
        public static ConditionCollection AddConditionProductDistinctPopup(string id, double position, bool isMultiSelect, ConditionCollection conditions)
        {
            // SelectPopup 항목 추가
            var conditionProductId = conditions.AddSelectPopup(id, new SqlQuery("GetProductDefinitionList", "10003", $"PLANTID={UserInfo.Current.Plant}"), "PRODUCTDEFNAME", "PRODUCTDEFID")
                .SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupLayoutForm(800, 800)
                .SetPopupAutoFillColumns("UNIT")
                .SetLabel("PRODUCTDEFID")
                .SetPosition(position);

            // 복수 선택 여부에 따른 Result Count 지정
            if (isMultiSelect)
                conditionProductId.SetPopupResultCount(0);
            else
                conditionProductId.SetPopupResultCount(1);

            // 팝업에서 사용되는 검색조건 (품목코드/명)
            conditionProductId.Conditions.AddTextBox("TXTPRODUCTDEFNAME");
            //제품 구분 추가 2019.08.14 배선용, 노석안 대리 요청
            conditionProductId.Conditions.AddComboBox("PRODUCTDIVISION", new SqlQuery("GetTypeList", "10001", $"CODECLASSID=ProductDivision2", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                                        .SetDefault("Product");

            // 팝업 그리드에서 보여줄 컬럼 정의
            // 품목코드
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            // 품목명
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            // 품목유형구분
            conditionProductId.GridColumns.AddComboBoxColumn("PRODUCTDEFTYPE", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductDefType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            // 생산구분
            conditionProductId.GridColumns.AddComboBoxColumn("PRODUCTIONTYPE", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            // 단위
            conditionProductId.GridColumns.AddComboBoxColumn("UNIT", 90, new SqlQuery("GetUomDefinitionList", "10001", "UOMCLASSID=Segment"), "UOMDEFNAME", "UOMDEFID");



            return conditions;
        }
        #endregion

        #region - AddConditionAreaPopup :: 작업장 조회 팝업 |
        /// <summary>
        /// 작업장 조회 팝업
        /// </summary>
        /// <param name="id"></param>
        /// <param name="position"></param>
        /// <param name="isMultiSelect"></param>
        /// <param name="conditions"></param>
        /// <param name="IsRequired"></param>
        /// <returns></returns>
        public static ConditionCollection AddConditionAreaPopup(string id, double position, bool isMultiSelect, ConditionCollection conditions, bool IsRequired = false, bool checkAuthoriry = false)
        {
            // SelectPopup 항목 추가
            string AreaQuryID = string.Empty;
            string AreaQuryVersion = string.Empty;

            if (checkAuthoriry)
            {
                AreaQuryID = "GetAreaListByAuthority";
                AreaQuryVersion = "10001";
            }
            else
            {
                AreaQuryID = "GetAreaList";
                AreaQuryVersion = "10003";
            }

            var conditionAreaId = conditions.AddSelectPopup(id, new SqlQuery(AreaQuryID, AreaQuryVersion, $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", "AREATYPE=Area", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "AREANAME", "AREAID")
                .SetPopupLayout("SELECTAREAID", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupLayoutForm(800, 800)
                .SetLabel("AREAID")
                .SetPosition(position);

            conditionAreaId.IsRequired = IsRequired;
            // 복수 선택 여부에 따른 Result Count 지정
            if (isMultiSelect)
                conditionAreaId.SetPopupResultCount(0);
            else
                conditionAreaId.SetPopupResultCount(1);


            conditionAreaId.Conditions.AddTextBox("TXTAREA");

            conditionAreaId.GridColumns.AddTextBoxColumn("AREAID", 150);
            conditionAreaId.GridColumns.AddTextBoxColumn("AREANAME", 200);


            return conditions;
        }
        public static ConditionCollection AddConditionAreaPopup(string id, double position, bool isMultiSelect, ConditionCollection conditions, string LanguageKey, bool IsRequired = false)
        {
            // SelectPopup 항목 추가
            var conditionAreaId = conditions.AddSelectPopup(id, new SqlQuery("GetAreaList", "10003", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", "AREATYPE=Area", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "AREANAME", "AREAID")
                .SetPopupLayout("SELECTAREAID", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupLayoutForm(800, 800)
                .SetLabel(LanguageKey)
                .SetPosition(position);

            conditionAreaId.IsRequired = IsRequired;
            // 복수 선택 여부에 따른 Result Count 지정
            if (isMultiSelect)
                conditionAreaId.SetPopupResultCount(0);
            else
                conditionAreaId.SetPopupResultCount(1);


            conditionAreaId.Conditions.AddTextBox("TXTAREA");

            conditionAreaId.GridColumns.AddTextBoxColumn("AREAID", 150);
            conditionAreaId.GridColumns.AddTextBoxColumn("AREANAME", 200);


            return conditions;
        }
        /// <summary>
        /// 작업장 조회 팝업
        /// </summary>
        /// <param name="id"></param>
        /// <param name="position"></param>
        /// <param name="isMultiSelect"></param>
        /// <param name="conditions"></param>
        /// <param name="IsRequired"></param>
        /// <param name="IsModify">true : 트랜젝션/등록 화면, false : 재공/실적 화면</param>
        /// <returns></returns>
        public static ConditionCollection AddConditionAreaByAuthorityPopup(string id, double position, bool isMultiSelect, ConditionCollection conditions, bool IsRequired = false, bool isModify = true)
        {
            // SelectPopup 항목 추가
            var conditionAreaId = conditions.AddSelectPopup(id, new SqlQuery("GetAreaListByAuthority", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}"
                , $"PLANTID={UserInfo.Current.Plant}", "AREATYPE=Area", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ISMODIFY={(isModify ? "Y" : "N")}"), "AREANAME", "AREAID")
                .SetPopupLayout("SELECTAREAID", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupLayoutForm(800, 800)
                .SetLabel("AREAID")
                .SetPosition(position);

            conditionAreaId.IsRequired = IsRequired;
            // 복수 선택 여부에 따른 Result Count 지정
            if (isMultiSelect)
                conditionAreaId.SetPopupResultCount(0);
            else
                conditionAreaId.SetPopupResultCount(1);


            conditionAreaId.Conditions.AddTextBox("TXTAREA");

            conditionAreaId.GridColumns.AddTextBoxColumn("AREAID", 150);
            conditionAreaId.GridColumns.AddTextBoxColumn("AREANAME", 200);


            return conditions;
        }
        public static ConditionCollection AddConditionWareHouseID(string id, double position, bool isMultiSelect, ConditionCollection conditions, bool IsRequired = false)
        {
            // SelectPopup 항목 추가
            var conditionAreaId = conditions.AddSelectPopup(id, new SqlQuery("SelectWareHouseID", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}"
                , $"P_PLANTID={UserInfo.Current.Plant}", "AREATYPE=Area", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "WAREHOUSENAME", "WAREHOUSEID")
                .SetPopupLayout("WAREHOUSELIST", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupLayoutForm(800, 800)
                .SetLabel("COMPLETEWAREHOUSEID")
                .SetPosition(position);

            conditionAreaId.IsRequired = IsRequired;
            // 복수 선택 여부에 따른 Result Count 지정
            if (isMultiSelect)
                conditionAreaId.SetPopupResultCount(0);
            else
                conditionAreaId.SetPopupResultCount(1);


            conditionAreaId.Conditions.AddTextBox("TXTWAREHOUSE");

            conditionAreaId.GridColumns.AddTextBoxColumn("WAREHOUSEID", 150);
            conditionAreaId.GridColumns.AddTextBoxColumn("WAREHOUSENAME", 200);


            return conditions;
        }
        #endregion

        #region - AddConditionProcessSegmentPopup :: 팝업형 조회조건 - 공정 |
        /// <summary>
        /// 팝업형 조회조건 - 공정
        /// </summary>
        /// <param name="id"></param>
        /// <param name="position"></param>
        /// <param name="isMultiSelect"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public static ConditionCollection AddConditionProcessSegmentPopup(string id, double position, bool isMultiSelect, ConditionCollection conditions)
        {
            var processSegmentIdPopup = conditions.AddSelectPopup(id, new SqlQuery("GetProcessSegmentList", "10002", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "PROCESSSEGMENTCLASSTYPE=MiddleProcessSegmentClass"), "PROCESSSEGMENTNAME", "PROCESSSEGMENTID")            
                .SetPopupLayout(Language.Get("SELECTPROCESSSEGMENTID"), PopupButtonStyles.Ok_Cancel, true, true)
                .SetPopupLayoutForm(800, 800)
                .SetPopupAutoFillColumns("PROCESSSEGMENTNAME")
                .SetLabel("PROCESSSEGMENT")
                .SetPosition(position);

            // 복수 선택 여부에 따른 Result Count 지정
            if (isMultiSelect)
                processSegmentIdPopup.SetPopupResultCount(0);
            else
                processSegmentIdPopup.SetPopupResultCount(1);

            processSegmentIdPopup.Conditions.AddComboBox("PROCESSSEGMENTCLASSID_MIDDLE", new SqlQuery("GetProcessSegmentClassByType", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "PROCESSSEGMENTCLASSTYPE=MiddleProcessSegmentClass"), "PROCESSSEGMENTCLASSNAME", "PROCESSSEGMENTCLASSID")
                .SetLabel("MIDDLEPROCESSSEGMENT")
                .SetEmptyItem();
            processSegmentIdPopup.Conditions.AddTextBox("PROCESSSEGMENT");


            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME_TOP", 150)
                .SetLabel("LARGEPROCESSSEGMENT");
            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME_MIDDLE", 150)
                .SetLabel("MIDDLEPROCESSSEGMENT");
            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150);
            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);

            return conditions;
        }
        #endregion

        #region - AddConditionProcessSegmentPopup :: 팝업형 조회조건 - 공정 |
        /// <summary>
        /// 팝업형 조회조건 - 공정
        /// </summary>
        /// <param name="id"></param>
        /// <param name="position"></param>
        /// <param name="strLabelName"></param>
        /// <param name="isMultiSelect"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public static ConditionCollection AddConditionProcessSegmentPopup(string id, double position, string strLabelName, bool isMultiSelect, ConditionCollection conditions)
        {
            var processSegmentIdPopup = conditions.AddSelectPopup(id, new SqlQuery("GetProcessSegmentList", "10002", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "PROCESSSEGMENTCLASSTYPE=MiddleProcessSegmentClass"), "PROCESSSEGMENTNAME", "PROCESSSEGMENTID")
                .SetPopupLayout(Language.Get("SELECTPROCESSSEGMENTID"), PopupButtonStyles.Ok_Cancel, true, true)
                .SetPopupLayoutForm(800, 800)
                .SetPopupAutoFillColumns("PROCESSSEGMENTNAME")
                .SetLabel(strLabelName)
                .SetPosition(position);

            // 복수 선택 여부에 따른 Result Count 지정
            if (isMultiSelect)
                processSegmentIdPopup.SetPopupResultCount(0);
            else
                processSegmentIdPopup.SetPopupResultCount(1);

            processSegmentIdPopup.Conditions.AddComboBox("PROCESSSEGMENTCLASSID_MIDDLE", new SqlQuery("GetProcessSegmentClassByType", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "PROCESSSEGMENTCLASSTYPE=MiddleProcessSegmentClass"), "PROCESSSEGMENTCLASSNAME", "PROCESSSEGMENTCLASSID")
                .SetLabel("MIDDLEPROCESSSEGMENT")
                .SetEmptyItem();
            processSegmentIdPopup.Conditions.AddTextBox("PROCESSSEGMENT");


            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME_TOP", 150)
                .SetLabel("LARGEPROCESSSEGMENT");
            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME_MIDDLE", 150)
                .SetLabel("MIDDLEPROCESSSEGMENT");
            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150);
            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);

            return conditions;
        }
        #endregion

        #region - AddConditionBriefLotPopup :: 검색조건에 Lot No 선택 팝업을 추가 |
        /// <summary>
        /// 검색조건에 Lot No 선택 팝업을 추가한다.
        /// </summary>
        /// <param name="id">검색조건 항목에 지정할 ID</param>
        /// <param name="position">검색조건을 추가할 순서</param>
        /// <param name="isMultiSelect">항목 복수 선택 여부</param>
        /// <param name="conditions">화면에서 사용되는 검색조건 컬렉션</param>
        /// <param name="processSegmentType">공정 구분</param>
        /// <returns></returns>
        public static ConditionCollection AddConditionBriefLotPopup(string id, double position, bool isMultiSelect, ConditionCollection conditions, bool isValidation = false)
        {
            // SelectPopup 항목 추가
            var conditionLotId = conditions.AddSelectPopup(id, new SqlQuery("GetSelectBriefLotListPopup", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "LOTID", "LOTID")
                .SetPopupLayout("SELECTLOTNO", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupLayoutForm(1200, 800)
                .SetLabel("LOTID")
                .SetPosition(position)
                .SetSearchTextControlId("TXTLOTID");

            // 복수 선택 여부에 따른 Result Count 지정
            if (isMultiSelect)
                conditionLotId.SetPopupResultCount(0);
            else
                conditionLotId.SetPopupResultCount(1);

            //validation
            if (isValidation)
                conditionLotId.SetPopupValidationCustom(ValidationProduct);


            // 팝업에서 사용되는 검색조건
            var conditionProductdef = conditionLotId.Conditions.AddSelectPopup("TXTPRODUCTDEFNAME2", new SqlQuery("GetProductionOrderIdListOfLotInput", "10001", $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PRODUCTDEFNAME", "PRODUCTDEFID")
                .SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupLayoutForm(600, 800)
                .SetLabel("PRODUCTDEF")
                .SetPopupResultCount(1)
                .SetPopupAutoFillColumns("PRODUCTDEFNAME");

            conditionProductdef.Conditions.AddTextBox("TXTPRODUCTDEFNAME2");

            conditionProductdef.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            conditionProductdef.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);

            // 품목코드/명
            conditionLotId.Conditions.AddTextBox("TXTPRODUCTDEFIDNAME")
                .SetLabel("TXTPRODUCTDEFNAME");
            // Lot No
            conditionLotId.Conditions.AddTextBox("TXTLOTID")
                .SetLabel("LOTID");


            var conditionProcessSegment = conditionLotId.Conditions.AddSelectPopup("TXTPROCESSSEGMENT", new SqlQuery("GetProcessSegmentList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PROCESSSEGMENTNAME", "PROCESSSEGMENTID")
                .SetPopupLayout("SELECTPROCESSSEGMENT", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupLayoutForm(600, 800)
                .SetLabel("PROCESSSEGMENT")
                .SetPopupResultCount(1)
                .SetPopupAutoFillColumns("PROCESSSEGMENTNAME");

            conditionProcessSegment.Conditions.AddTextBox("PROCESSSEGMENT");

            conditionProcessSegment.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150);
            conditionProcessSegment.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);

            //사이트
            conditionLotId.GridColumns.AddTextBoxColumn("PLANTID", 60);
            // Lot No
            conditionLotId.GridColumns.AddTextBoxColumn("LOTID", 200);
            // 양산구분
            conditionLotId.GridColumns.AddTextBoxColumn("PRODUCTIONTYPE", 60);
            //품목코드
            conditionLotId.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 100);
            //품목리비전
            conditionLotId.GridColumns.AddTextBoxColumn("PRODUCTREVISION", 50);
            //품목명
            conditionLotId.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 150);
            //공정명
            conditionLotId.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 100);
            //공정수순
            conditionLotId.GridColumns.AddTextBoxColumn("USERSEQUENCE", 60);
            //roll/sheet
            conditionLotId.GridColumns.AddTextBoxColumn("RTRSHT", 60);
            //공정진행상태
            conditionLotId.GridColumns.AddTextBoxColumn("PROCESSSTATE", 70);
            //PCS
            conditionLotId.GridColumns.AddTextBoxColumn("PCS", 50);
            //PNL
            conditionLotId.GridColumns.AddTextBoxColumn("PNL", 50);
            return conditions;
        }
        #endregion

        #region - AddConditionLotPopup :: 검색조건에 Lot No 선택 팝업을 추가한다. |
        /// <summary>
        /// 검색조건에 Lot No 선택 팝업을 추가한다.
        /// </summary>
        /// <param name="id">검색조건 항목에 지정할 ID</param>
        /// <param name="position">검색조건을 추가할 순서</param>
        /// <param name="isMultiSelect">항목 복수 선택 여부</param>
        /// <param name="conditions">화면에서 사용되는 검색조건 컬렉션</param>
        /// <param name="processSegmentType">공정 구분</param>
        /// <returns></returns>
        public static ConditionCollection AddConditionLotPopup(string id, double position, bool isMultiSelect, ConditionCollection conditions, string processSegmentType = "", bool isValidation = false)
        {
            // SelectPopup 항목 추가
            var conditionLotId = conditions.AddSelectPopup(id, new SqlQuery("GetLotIdList", "10001", $"PROCESSSEGMENTTYPE={processSegmentType}"), "LOTID", "LOTID")
                .SetPopupLayout("SELECTLOTNO", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupLayoutForm(1200, 800)
                .SetLabel("LOTID")
                .SetPosition(position)
                .SetSearchTextControlId("TXTLOTID");

            // 복수 선택 여부에 따른 Result Count 지정
            if (isMultiSelect)
                conditionLotId.SetPopupResultCount(0);
            else
                conditionLotId.SetPopupResultCount(1);

            //validation
            if (isValidation)
                conditionLotId.SetPopupValidationCustom(ValidationProduct);


            // 팝업에서 사용되는 검색조건
            var conditionProductdef = conditionLotId.Conditions.AddSelectPopup("TXTPRODUCTDEFNAME2", new SqlQuery("GetProductionOrderIdListOfLotInput", "10001", $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PRODUCTDEFNAME", "PRODUCTDEFID")
                .SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupLayoutForm(600, 800)
                .SetLabel("PRODUCTDEF")
                .SetPopupResultCount(1)
                .SetPopupAutoFillColumns("PRODUCTDEFNAME");

            conditionProductdef.Conditions.AddTextBox("TXTPRODUCTDEFNAME2");

            conditionProductdef.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            conditionProductdef.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);

            // 품목코드/명
            conditionLotId.Conditions.AddTextBox("TXTPRODUCTDEFIDNAME")
                .SetLabel("TXTPRODUCTDEFNAME");
            // Lot No
            conditionLotId.Conditions.AddTextBox("TXTLOTID")
                .SetLabel("LOTID");

            // 고객사
            //conditionLotId.Conditions.AddComboBox("CBOCUSTOMER", new SqlQuery("GetCustomerList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "CUSTOMERNAME", "CUSTOMERID")
            //    .SetLabel("CUSTOMER")
            //    .SetEmptyItem()
            //    .SetResultCount(0);

            // 사업부
            //conditionLotId.Conditions.AddComboBox("CBOFACTORY", new SqlQuery("GetFactoryList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "FACTORYNAME", "FACTORYID")
            //    .SetLabel("BUSINESS")
            //    .SetResultCount(1);
            // 작업장
            //conditionLotId.Conditions.AddComboBox("CBOWORKPLACE", new SqlQuery("GetAreaList", "10003", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "AREANAME", "AREAID")
            //    .SetLabel("WORKPLACE")
            //    .SetResultCount(0)
            //    .SetRelationIds("CBOFACTORY");
            // 공정

            var conditionProcessSegment = conditionLotId.Conditions.AddSelectPopup("TXTPROCESSSEGMENT", new SqlQuery("GetProcessSegmentList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PROCESSSEGMENTNAME", "PROCESSSEGMENTID")
                .SetPopupLayout("SELECTPROCESSSEGMENT", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupLayoutForm(600, 800)
                .SetLabel("PROCESSSEGMENT")
                .SetPopupResultCount(1)
                .SetPopupAutoFillColumns("PROCESSSEGMENTNAME");

            conditionProcessSegment.Conditions.AddTextBox("PROCESSSEGMENT");

            conditionProcessSegment.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150);
            conditionProcessSegment.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
            // 상태구분
            //conditionLotId.Conditions.AddComboBox("CBOLOTSTATE", new SqlQuery("GetCodeList", "00001", "CODECLASSID=LotState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
            //    .SetLabel("LOTSTATE")
            //    .SetResultCount(1);
            /*
             * 
             * 
             * 
             * 
             * 검색조건 AddComboBox 시 에러발생 (개체참조 에러)
             * 
             * 
             * 
            // 양산구분
            conditionLotId.Conditions.AddComboBox("CBOPRODUCTIONTYPE", new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("PRODUCTIONTYPE")
                .SetEmptyItem()
                .SetResultCount(0);
            // RTR/SHT
            conditionLotId.Conditions.AddComboBox("CBORTRSHT", new SqlQuery("GetCodeList", "00001", "CODECLASSID=RTRSHT", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("RTRSHT")
                .SetEmptyItem()
                .SetResultCount(1);
            */
            // 작업구분
            //conditionLotId.Conditions.AddComboBox("CBOWORKTYPE", new SqlQuery("GetCodeList", "00001", "CODECLASSID=WorkType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
            //    .SetLabel("WORKTYPE")
            //    .SetResultCount(1);
            // 대공정
            //conditionLotId.Conditions.AddComboBox("CBOTOPPROCESS", new SqlQuery("GetProcessSegmentClassByType", "10001", "PROCESSSEGMENTCLASSTYPE=TopProcessSegmentClass"), "PROCESSSEGMENTCLASSNAME", "PROCESSSEGMENTCLASSID")
            //    .SetLabel("TOPPROCESSSEGMENTCLASS")
            //    .SetEmptyItem()
            //    .SetResultCount(1);
            //// 중공정
            //conditionLotId.Conditions.AddComboBox("CBOMIDDLEPROCESS", new SqlQuery("GetProcessSegmentClassByType", "10001", "PROCESSSEGMENTCLASSTYPE=MiddleProcessSegmentClass"), "PROCESSSEGMENTCLASSNAME", "PROCESSSEGMENTCLASSID")
            //    .SetLabel("MIDDLEPROCESSSEGMENTCLASS")
            //    .SetEmptyItem()
            //    .SetResultCount(1);

            // 팝업 그리드에서 보여줄 컬럼 정의
            // Lot No
            conditionLotId.GridColumns.AddTextBoxColumn("LOTID", 170);
            // 양산구분
            //conditionLotId.GridColumns.AddComboBoxColumn("LOTTYPE", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetLabel("PRODUCTIONTYPE");
            conditionLotId.GridColumns.AddTextBoxColumn("LOTTYPE", 90).SetLabel("PRODUCTIONTYPE");
            // 품목코드
            conditionLotId.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            // 품목버전
            conditionLotId.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 80);
            // 품목명
            conditionLotId.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            // 라우팅
            //conditionLotId.GridColumns.AddComboBoxColumn("PROCESSDEFID", 100, new SqlQuery("GetProcessDefinition", "1"), "PROCESSDEFNAME", "PROCESSDEFID");
            conditionLotId.GridColumns.AddTextBoxColumn("PROCESSDEFNAME", 100);
            // 공정
            //conditionLotId.GridColumns.AddComboBoxColumn("PROCESSSEGMENTID", 100, new SqlQuery("GetProcessSegmentList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PROCESSSEGMENTNAME", "PROCESSSEGMENTID");
            conditionLotId.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 100);
            // 순서
            conditionLotId.GridColumns.AddTextBoxColumn("USERSEQUENCE", 70);
            // Site
            //conditionLotId.GridColumns.AddComboBoxColumn("PLANTID", 90, new SqlQuery("GetPlantList", "10002", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "PLANTNAME", "PLANTID");
            conditionLotId.GridColumns.AddTextBoxColumn("PLANTNAME", 90);
            // 작업장
            //conditionLotId.GridColumns.AddComboBoxColumn("AREAID", 90, new SqlQuery("GetAreaList", "10003", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "AREANAME", "AREAID");
            conditionLotId.GridColumns.AddTextBoxColumn("AREANAME", 90);
            // Roll/Sheet
            //conditionLotId.GridColumns.AddComboBoxColumn("RTRSHT", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=RTRSHT", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            conditionLotId.GridColumns.AddTextBoxColumn("RTRSHT", 90);
            // 단위
            //conditionLotId.GridColumns.AddComboBoxColumn("UNIT", 90, new SqlQuery("GetUomDefinitionList", "10001", "UOMCLASSID=Segment"), "UOMDEFNAME", "UOMDEFID");
            conditionLotId.GridColumns.AddTextBoxColumn("UOMDEFNAME", 90);
            // 수량
            conditionLotId.GridColumns.AddSpinEditColumn("QTY", 90);
            // PCS 수량
            conditionLotId.GridColumns.AddSpinEditColumn("PCSQTY", 90);
            // PNL 수량
            conditionLotId.GridColumns.AddSpinEditColumn("PANELQTY", 90);
            // M2 수량
            conditionLotId.GridColumns.AddSpinEditColumn("M2QTY", 90);
            // 납기계획일
            conditionLotId.GridColumns.AddDateEditColumn("PLANENDTIME", 100).SetDisplayFormat("yyyy-MM-dd");
            // 기한
            conditionLotId.GridColumns.AddSpinEditColumn("LEFTDATE", 70);
            // 인수 Step PCS 수량
            conditionLotId.GridColumns.AddSpinEditColumn("RECEIVEPCSQTY", 90);
            // 인수 Step PNL 수량
            conditionLotId.GridColumns.AddSpinEditColumn("RECEIVEPANELQTY", 90);
            // 시작 Step PCS 수량
            conditionLotId.GridColumns.AddSpinEditColumn("WORKSTARTPCSQTY", 90);
            // 시작 Step PNL 수량
            conditionLotId.GridColumns.AddSpinEditColumn("WORKSTARTPANELQTY", 90);
            // 완료 Step PCS 수량
            conditionLotId.GridColumns.AddSpinEditColumn("WORKENDPCSQTY", 90);
            // 완료 Step PNL 수량
            conditionLotId.GridColumns.AddSpinEditColumn("WORKENDPANELQTY", 90);
            // 인계 Step PCS 수량
            conditionLotId.GridColumns.AddSpinEditColumn("SENDPCSQTY", 90);
            // 인계 Step PNL 수량
            conditionLotId.GridColumns.AddSpinEditColumn("SENDPANELQTY", 90);
            // 공정 Lead Time
            conditionLotId.GridColumns.AddTextBoxColumn("LEADTIME", 90);

            return conditions;
        }
        #endregion

        #region  - AddConditionLotHistPopup :: 검색조건에 Lot No 선택 팝업을 추가한다. |
        /// <summary>
        /// 검색조건에 Lot No 선택 팝업을 추가한다.
        /// </summary>
        /// <param name="id">검색조건 항목에 지정할 ID</param>
        /// <param name="position">검색조건을 추가할 순서</param>
        /// <param name="isMultiSelect">항목 복수 선택 여부</param>
        /// <param name="conditions">화면에서 사용되는 검색조건 컬렉션</param>
        /// <param name="processSegmentType">공정 구분</param>
        /// <returns></returns>
        public static ConditionCollection AddConditionLotHistPopup(string id, double position, bool isMultiSelect, ConditionCollection conditions, string processSegmentType = "", bool isValidation = false)
        {
            // SelectPopup 항목 추가
            var conditionLotId = conditions.AddSelectPopup(id, new SqlQuery("GetLotIdList", "10002", $"PROCESSSEGMENTTYPE={processSegmentType}"), "LOTID", "LOTID")
                .SetPopupLayout("SELECTLOTNO", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupLayoutForm(1200, 800)
                .SetLabel("LOTID")
                .SetPosition(position)
                .SetSearchTextControlId("TXTLOTID");

            // 복수 선택 여부에 따른 Result Count 지정
            if (isMultiSelect)
                conditionLotId.SetPopupResultCount(0);
            else
                conditionLotId.SetPopupResultCount(1);

            //validation
            if (isValidation)
                conditionLotId.SetPopupValidationCustom(ValidationProduct);

            // 팝업에서 사용되는 검색조건
            var conditionProductdef = conditionLotId.Conditions.AddSelectPopup("TXTPRODUCTDEFNAME2", new SqlQuery("GetProductDefinitionList", "10001", $"PLANTID={UserInfo.Current.Plant}"), "PRODUCTDEFNAME", "PRODUCTDEFID")
                .SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupLayoutForm(600, 800)
                .SetLabel("PRODUCTDEF")
                .SetPopupResultCount(1)
                .SetPopupAutoFillColumns("PRODUCTDEFNAME");

            conditionProductdef.Conditions.AddTextBox("TXTPRODUCTDEFNAME");

            conditionProductdef.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            conditionProductdef.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);

            // 품목코드/명
            conditionLotId.Conditions.AddTextBox("TXTPRODUCTDEFIDNAME")
                .SetLabel("TXTPRODUCTDEFNAME");
            // Lot No
            conditionLotId.Conditions.AddTextBox("TXTLOTID")
                .SetLabel("LOTID");

            // 고객사
            //conditionLotId.Conditions.AddComboBox("CBOCUSTOMER", new SqlQuery("GetCustomerList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "CUSTOMERNAME", "CUSTOMERID")
            //    .SetLabel("CUSTOMER")
            //    .SetEmptyItem()
            //    .SetResultCount(0);

            // 사업부
            //conditionLotId.Conditions.AddComboBox("CBOFACTORY", new SqlQuery("GetFactoryList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "FACTORYNAME", "FACTORYID")
            //    .SetLabel("BUSINESS")
            //    .SetResultCount(1);
            // 작업장
            //conditionLotId.Conditions.AddComboBox("CBOWORKPLACE", new SqlQuery("GetAreaList", "10003", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "AREANAME", "AREAID")
            //    .SetLabel("WORKPLACE")
            //    .SetResultCount(0)
            //    .SetRelationIds("CBOFACTORY");
            // 공정

            var conditionProcessSegment = conditionLotId.Conditions.AddSelectPopup("TXTPROCESSSEGMENT", new SqlQuery("GetProcessSegmentList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PROCESSSEGMENTNAME", "PROCESSSEGMENTID")
                .SetPopupLayout("SELECTPROCESSSEGMENT", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupLayoutForm(600, 800)
                .SetLabel("PROCESSSEGMENT")
                .SetPopupResultCount(1)
                .SetPopupAutoFillColumns("PROCESSSEGMENTNAME");

            conditionProcessSegment.Conditions.AddTextBox("PROCESSSEGMENT");

            conditionProcessSegment.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150);
            conditionProcessSegment.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
            // 상태구분
            //conditionLotId.Conditions.AddComboBox("CBOLOTSTATE", new SqlQuery("GetCodeList", "00001", "CODECLASSID=LotState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
            //    .SetLabel("LOTSTATE")
            //    .SetResultCount(1);
            /*
             * 
             * 
             * 
             * 
             * 검색조건 AddComboBox 시 에러발생 (개체참조 에러)
             * 
             * 
             * 
            // 양산구분
            conditionLotId.Conditions.AddComboBox("CBOPRODUCTIONTYPE", new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("PRODUCTIONTYPE")
                .SetEmptyItem()
                .SetResultCount(0);
            // RTR/SHT
            conditionLotId.Conditions.AddComboBox("CBORTRSHT", new SqlQuery("GetCodeList", "00001", "CODECLASSID=RTRSHT", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("RTRSHT")
                .SetEmptyItem()
                .SetResultCount(1);
            */
            // 작업구분
            //conditionLotId.Conditions.AddComboBox("CBOWORKTYPE", new SqlQuery("GetCodeList", "00001", "CODECLASSID=WorkType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
            //    .SetLabel("WORKTYPE")
            //    .SetResultCount(1);
            // 대공정
            //conditionLotId.Conditions.AddComboBox("CBOTOPPROCESS", new SqlQuery("GetProcessSegmentClassByType", "10001", "PROCESSSEGMENTCLASSTYPE=TopProcessSegmentClass"), "PROCESSSEGMENTCLASSNAME", "PROCESSSEGMENTCLASSID")
            //    .SetLabel("TOPPROCESSSEGMENTCLASS")
            //    .SetEmptyItem()
            //    .SetResultCount(1);
            //// 중공정
            //conditionLotId.Conditions.AddComboBox("CBOMIDDLEPROCESS", new SqlQuery("GetProcessSegmentClassByType", "10001", "PROCESSSEGMENTCLASSTYPE=MiddleProcessSegmentClass"), "PROCESSSEGMENTCLASSNAME", "PROCESSSEGMENTCLASSID")
            //    .SetLabel("MIDDLEPROCESSSEGMENTCLASS")
            //    .SetEmptyItem()
            //    .SetResultCount(1);

            // 팝업 그리드에서 보여줄 컬럼 정의
            var baseGroup = conditionLotId.GridColumns.AddGroupColumn("");
            // Lot No
            baseGroup.AddTextBoxColumn("LOTID", 170);
            // 양산구분
            //conditionLotId.GridColumns.AddComboBoxColumn("LOTTYPE", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetLabel("PRODUCTIONTYPE");
            baseGroup.AddTextBoxColumn("LOTTYPE", 90).SetLabel("PRODUCTIONTYPE");
            // 품목코드
            baseGroup.AddTextBoxColumn("PRODUCTDEFID", 150);
            // 품목버전
            baseGroup.AddTextBoxColumn("PRODUCTDEFVERSION", 80);
            // 품목명
            baseGroup.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            // 라우팅
            //conditionLotId.GridColumns.AddComboBoxColumn("PROCESSDEFID", 100, new SqlQuery("GetProcessDefinition", "1"), "PROCESSDEFNAME", "PROCESSDEFID");
            baseGroup.AddTextBoxColumn("PROCESSDEFNAME", 100);
            // 공정
            //conditionLotId.GridColumns.AddComboBoxColumn("PROCESSSEGMENTID", 100, new SqlQuery("GetProcessSegmentList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PROCESSSEGMENTNAME", "PROCESSSEGMENTID");
            baseGroup.AddTextBoxColumn("PROCESSSEGMENTNAME", 100);
            // 순서
            baseGroup.AddTextBoxColumn("USERSEQUENCE", 70);
            // Site
            //conditionLotId.GridColumns.AddComboBoxColumn("PLANTID", 90, new SqlQuery("GetPlantList", "10002", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "PLANTNAME", "PLANTID");
            baseGroup.AddTextBoxColumn("PLANTNAME", 90);
            // 작업장
            //conditionLotId.GridColumns.AddComboBoxColumn("AREAID", 90, new SqlQuery("GetAreaList", "10003", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "AREANAME", "AREAID");
            baseGroup.AddTextBoxColumn("AREANAME", 90);
            // Roll/Sheet
            //conditionLotId.GridColumns.AddComboBoxColumn("RTRSHT", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=RTRSHT", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            baseGroup.AddTextBoxColumn("RTRSHT", 90);
            // 단위
            //conditionLotId.GridColumns.AddComboBoxColumn("UNIT", 90, new SqlQuery("GetUomDefinitionList", "10001", "UOMCLASSID=Segment"), "UOMDEFNAME", "UOMDEFID");
            baseGroup.AddTextBoxColumn("UOMDEFNAME", 90);

            var qtyGroup = conditionLotId.GridColumns.AddGroupColumn("QTY");
            // 수량
            qtyGroup.AddSpinEditColumn("QTY", 90);
            // PCS 수량
            qtyGroup.AddSpinEditColumn("PCSQTY", 90);
            // PNL 수량
            qtyGroup.AddSpinEditColumn("PANELQTY", 90);
            // M2 수량
            qtyGroup.AddSpinEditColumn("M2QTY", 90);

            var dateGroup = conditionLotId.GridColumns.AddGroupColumn("");
            // 납기계획일
            dateGroup.AddDateEditColumn("PLANENDTIME", 100)
                .SetDisplayFormat("yyyy-MM-dd");
            // 기한
            dateGroup.AddSpinEditColumn("LEFTDATE", 70);

            var recvGroup = conditionLotId.GridColumns.AddGroupColumn("ACCEPT");
            // 인수 Step PCS 수량
            recvGroup.AddSpinEditColumn("RECEIVEPCSQTY", 90);
            // 인수 Step PNL 수량
            recvGroup.AddSpinEditColumn("RECEIVEPANELQTY", 90);

            var startGroup = conditionLotId.GridColumns.AddGroupColumn("START");
            // 시작 Step PCS 수량
            startGroup.AddSpinEditColumn("WORKSTARTPCSQTY", 90);
            // 시작 Step PNL 수량
            startGroup.AddSpinEditColumn("WORKSTARTPANELQTY", 90);

            var endGroup = conditionLotId.GridColumns.AddGroupColumn("COMPLETE");
            // 완료 Step PCS 수량
            endGroup.AddSpinEditColumn("WORKENDPCSQTY", 90);
            // 완료 Step PNL 수량
            endGroup.AddSpinEditColumn("WORKENDPANELQTY", 90);
            
            var sendGroup = conditionLotId.GridColumns.AddGroupColumn("WIPSENDQTY2");
            // 인계 Step PCS 수량
            sendGroup.AddSpinEditColumn("SENDPCSQTY", 90);
            // 인계 Step PNL 수량
            sendGroup.AddSpinEditColumn("SENDPANELQTY", 90);

            var leadTimeGroup = conditionLotId.GridColumns.AddGroupColumn("");
            // 공정 Lead Time
            leadTimeGroup.AddTextBoxColumn("LEADTIME", 90);

            return conditions;
        }
        #endregion

        #region - AddConditionSampleLotPopup :: 검색조건에 Sample / Test Lot No 선택 팝업을 추가 |
        /// <summary>
        /// 검색조건에 Lot No 선택 팝업을 추가한다.
        /// </summary>
        /// <param name="id">검색조건 항목에 지정할 ID</param>
        /// <param name="position">검색조건을 추가할 순서</param>
        /// <param name="isMultiSelect">항목 복수 선택 여부</param>
        /// <param name="conditions">화면에서 사용되는 검색조건 컬렉션</param>
        /// <param name="processSegmentType">공정 구분</param>
        /// <returns></returns>
        public static ConditionCollection AddConditionSampleLotPopup(string id, double position, bool isMultiSelect, ConditionCollection conditions, bool isValidation = false, bool isSampleRouting = false)
        {
            string isSampleRoutingStr = isSampleRouting ? "Y" : "N";

            // SelectPopup 항목 추가
            var conditionLotId = conditions.AddSelectPopup(id, new SqlQuery("GetSelectSampleLotListPopup", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "LOTID", "LOTID")
                .SetPopupLayout("SELECTLOTNO", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupLayoutForm(1200, 800)
                .SetLabel("LOTID")
                .SetPosition(position)
                .SetSearchTextControlId("TXTLOTID");

            // 복수 선택 여부에 따른 Result Count 지정
            if (isMultiSelect)
                conditionLotId.SetPopupResultCount(0);
            else
                conditionLotId.SetPopupResultCount(1);

            //validation
            if (isValidation)
                conditionLotId.SetPopupValidationCustom(ValidationProduct);

            // 팝업에서 사용되는 검색조건
            var conditionProductdef = conditionLotId.Conditions.AddSelectPopup("TXTPRODUCTDEFNAME2"
                , new SqlQuery("GetProductionOrderIdListOfLotInput", "10001", $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "PRODUCTIONTYPE=Sample"), "PRODUCTDEFNAME", "PRODUCTDEFID")
                .SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupLayoutForm(600, 800)
                .SetLabel("PRODUCTDEF")
                .SetPopupResultCount(1)
                .SetPopupAutoFillColumns("PRODUCTDEFNAME");

            conditionProductdef.Conditions.AddTextBox("TXTPRODUCTDEFNAME2");

            conditionProductdef.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            conditionProductdef.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);

            // 품목코드/명
            conditionLotId.Conditions.AddTextBox("TXTPRODUCTDEFIDNAME")
                .SetLabel("TXTPRODUCTDEFNAME");
            // Lot No
            conditionLotId.Conditions.AddTextBox("TXTLOTID")
                .SetLabel("LOTID");


            var conditionProcessSegment = conditionLotId.Conditions.AddSelectPopup("TXTPROCESSSEGMENT", new SqlQuery("GetProcessSegmentList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PROCESSSEGMENTNAME", "PROCESSSEGMENTID")
                .SetPopupLayout("SELECTPROCESSSEGMENT", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupLayoutForm(600, 800)
                .SetLabel("PROCESSSEGMENT")
                .SetPopupResultCount(1)
                .SetPopupAutoFillColumns("PROCESSSEGMENTNAME");

            conditionProcessSegment.Conditions.AddTextBox("PROCESSSEGMENT");

            conditionProcessSegment.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150);
            conditionProcessSegment.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);

            conditionLotId.Conditions.AddTextBox("ISSAMPLEROUTING")
                .SetDefault(isSampleRoutingStr)
                .SetIsHidden();

            //사이트
            conditionLotId.GridColumns.AddTextBoxColumn("PLANTID", 60);
            // Lot No
            conditionLotId.GridColumns.AddTextBoxColumn("LOTID", 200);
            // 양산구분
            conditionLotId.GridColumns.AddTextBoxColumn("PRODUCTIONTYPE", 60);
            //품목코드
            conditionLotId.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 100);
            //품목리비전
            conditionLotId.GridColumns.AddTextBoxColumn("PRODUCTREVISION", 50);
            //품목명
            conditionLotId.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 150);
            //공정명
            conditionLotId.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 100);
            //공정수순
            conditionLotId.GridColumns.AddTextBoxColumn("USERSEQUENCE", 60);
            //roll/sheet
            conditionLotId.GridColumns.AddTextBoxColumn("RTRSHT", 60);
            //공정진행상태
            conditionLotId.GridColumns.AddTextBoxColumn("PROCESSSTATE", 70);
            //PCS
            conditionLotId.GridColumns.AddTextBoxColumn("PCS", 50);
            //PNL
            conditionLotId.GridColumns.AddTextBoxColumn("PNL", 50);
            return conditions;
        }
        #endregion

        #region - AddConditionCustomerPopup :: 작업장 조회 팝업 |
        /// <summary>
        /// 작업장 조회 팝업
        /// </summary>
        /// <param name="id"></param>
        /// <param name="position"></param>
        /// <param name="isMultiSelect"></param>
        /// <param name="conditions"></param>
        /// <param name="IsRequired"></param>
        /// <returns></returns>
        public static ConditionCollection AddConditionCustomerPopup(string id, double position, bool isMultiSelect, ConditionCollection conditions, bool IsRequired = false)
        {
            // SelectPopup 항목 추가
            var conditionCustomerId = conditions.AddSelectPopup(id, new SqlQuery("GetCustomerList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CUSTOMERNAME", "CUSTOMERID")
                .SetPopupLayout("CUSTOMERID", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupLayoutForm(800, 800)
                .SetLabel("CUSTOMERID")
                .SetPosition(position);

            conditionCustomerId.IsRequired = IsRequired;
            // 복수 선택 여부에 따른 Result Count 지정
            if (isMultiSelect)
                conditionCustomerId.SetPopupResultCount(0);
            else
                conditionCustomerId.SetPopupResultCount(1);


            conditionCustomerId.Conditions.AddTextBox("TXTCUSTOMERID");

            conditionCustomerId.GridColumns.AddTextBoxColumn("CUSTOMERID", 150);
            conditionCustomerId.GridColumns.AddTextBoxColumn("CUSTOMERNAME", 200);


            return conditions;
        }
        #endregion
        #endregion

        #region - PrintMaterialStocktakingList :: 자재실사관리 - 자재실사표 출력 |
        /// <summary>
        /// 자재실사관리 - 자재실사표 출력
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="dt"></param>
        public static void PrintMaterialStocktakingList(Stream stream, DataTable dt)
        {
            //각 작업장 별 실사 LIST 출력
            var query = from dataRow in dt.AsEnumerable()
                        orderby dataRow["WAREHOUSEID"]
                        select new
                        {
                            WAREHOUSEID = dataRow["WAREHOUSEID"],
                            WAREHOUSENAME = dataRow["WAREHOUSENAME"],
                            CONSUMABLEDEFID = dataRow["CONSUMABLEDEFID"],
                            CONSUMABLEDEFNAME = dataRow["CONSUMABLEDEFNAME"],
                            UNIT = dataRow["UNIT"],
                            //CONSUMABLELOTID = dataRow["CONSUMABLELOTID"],
                            STOCKQTY = dataRow["STOCKQTY"],
                            BASEMONTH = dataRow["BASEMONTH"],
                            PRINTEDTIME = dataRow["PRINTEDTIME"]
                            //CYCLECOUNT = dataRow["CYCLECOUNT"]
                        };

            //디테일 TOTAL 정보
            DataTable detailInfo = new DataTable();
            detailInfo.Columns.Add("SEQ", typeof(int));
            detailInfo.Columns.Add("WAREHOUSEID", typeof(string));
            detailInfo.Columns.Add("WAREHOUSENAME", typeof(string));
            detailInfo.Columns.Add("CONSUMABLEDEFID", typeof(string));
            detailInfo.Columns.Add("CONSUMABLEDEFNAME", typeof(string));
            detailInfo.Columns.Add("UNIT", typeof(string));
            //detailInfo.Columns.Add("CONSUMABLELOTID", typeof(string));
            detailInfo.Columns.Add("STOCKQTY", typeof(decimal));
            //detailInfo.Columns.Add("CYCLECOUNT", typeof(decimal));
            detailInfo.Columns.Add("BASEMONTH", typeof(string));

            var dl = query.ToList();
            string prevArea = string.Empty;
            int cnt = 0;
            for (int i = 0; i < dl.Count; i++)
            {
                if (!prevArea.Equals(dl[i].WAREHOUSEID.ToString()))
                {
                    prevArea = dl[i].WAREHOUSEID.ToString();
                    cnt = 1;
                }

                detailInfo.Rows.Add(cnt, dl[i].WAREHOUSEID.ToString(), dl[i].WAREHOUSENAME.ToString(), dl[i].CONSUMABLEDEFID.ToString(), dl[i].CONSUMABLEDEFNAME.ToString(),
                    //dl[i].CONSUMABLELOTID.ToString(),
                    dl[i].UNIT.ToString(),
                    Format.GetDecimal(dl[i].STOCKQTY, 0),
                    //dl[i].STOCKQTY.ToString(),
                    dl[i].BASEMONTH.ToString());

                cnt++;
            }

            DataTable dtHeader = new DataTable();
            dtHeader.Columns.Add("WAREHOUSEID", typeof(string));
            dtHeader.Columns.Add("WAREHOUSENAME", typeof(string));
            dtHeader.Columns.Add("BASEMONTH", typeof(string));
            dtHeader.Columns.Add("PRINTEDTIME", typeof(String));

            var areaList = from areaRow in dt.AsEnumerable()
                           orderby areaRow["WAREHOUSEID"]
                           select new
                           {
                               AreaId = Format.GetString(areaRow["WAREHOUSEID"]),
                               AreaName = Format.GetString(areaRow["WAREHOUSENAME"]),
                               BaseMonth = Format.GetString(areaRow["BASEMONTH"]),
                               PrintedTime = Format.GetString(areaRow["PRINTEDTIME"])
                           };

            areaList.Distinct().ForEach(ar =>
            {
                dtHeader.Rows.Add(ar.AreaId, ar.AreaName , ar.BaseMonth , ar.PrintedTime);
            });

            dtHeader.AcceptChanges();

            DataTable dtDetail = detailInfo.AsEnumerable().CopyToDataTable();

            DataSet dsReport = new DataSet();
            dsReport.Tables.Add(dtHeader);
            dsReport.Tables.Add(dtDetail);

            DataRelation relation = new DataRelation("RelationAreaId", dtHeader.Columns["WAREHOUSEID"], dtDetail.Columns["WAREHOUSEID"]);
            dsReport.Relations.Add(relation);

            XtraReport report = XtraReport.FromStream(stream);
            report.DataSource = dsReport;
            report.DataMember = "Table1";

            Band band = report.Bands["Detail"];


            DetailReportBand detailReport = report.Bands["DetailReport"] as DetailReportBand;
            detailReport.DataSource = dsReport;
            detailReport.DataMember = "RelationAreaId";

            Band groupHeader = detailReport.Bands["GroupHeader1"];
            SetReportControlDataBinding(groupHeader.Controls, dsReport, "");

            Band detailBand = detailReport.Bands["Detail1"];
            setLabelLaungage(detailReport);
            SetReportControlDataBinding(detailBand.Controls, dsReport, "RelationAreaId");

            report.ShowPreviewDialog();

        }
        #endregion

        #region ◆ 포장 라벨 :::: 

        #region - printPackingLabel :: 포장 라벨 출력 |
        /// <summary>
        /// 포장 라벨 출력
        /// </summary>
        /// <param name="lotid"></param>
        public static void printPackingLabel(string lotid)
        {
            Dictionary<string, object> commentParam = new Dictionary<string, object>();
            commentParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            commentParam.Add("PLANTID", UserInfo.Current.Plant);
            commentParam.Add("LOTID", lotid);

            DataTable dt = SqlExecuter.Query("SelectLabelList", "10001", commentParam);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                byte[] labelData = dt.Rows[i]["LABELDATA"] as byte[];
                XtraReport report = new XtraReport();
                using (MemoryStream ms = new MemoryStream(labelData))
                {

                    ms.Write(labelData, 0, labelData.Length);

                    report = XtraReport.FromStream(ms);

                    DataRow dr = dt.Rows[i];

                    report = getLabelDataMappingReport(report, dr, lotid);

                    ImageExportOptions imageOptions = report.ExportOptions.Image;
                    imageOptions.Format = ImageFormat.Bmp;

                    report.ExportToImage(SystemCommonClass.ImageTempPath, imageOptions);

                    int w, h;
                    Bitmap b = new Bitmap(SystemCommonClass.ImageTempPath);
                    w = b.Width; h = b.Height;

                    string ZPLImageDataString = Micube.SmartMES.Commons.ZPLPrint.GetZPLIIImage(b, 20, 20); //ZPLUtil. SendImageToPrinter(1,1,b);
                    StringBuilder ZPLCommand = new StringBuilder();

                    ZPLCommand.AppendLine("^XA");
                    ZPLCommand.AppendLine(ZPLImageDataString);
                    ZPLCommand.AppendLine("^XZ");

                    Commons.ZPLPrint.SendStringToPrinter(ZPLCommand.ToString());
                    b.Dispose();

                    FileInfo fileDel = new FileInfo(SystemCommonClass.ImageTempPath);

                    if (fileDel.Exists) //삭제할 파일이 있는지
                    {
                        fileDel.Delete(); //없어도 에러안남
                    }

                }

            }
        }
        #endregion

        #region - PrintBoxLabel :: 포장 라벨 출력 (BoxNo) |
        /// <summary>
        /// 포장 라벨 출력 (BoxNo)
        /// </summary>
        /// <param name="boxno"></param>
        public static void PrintBoxLabel(string boxno)
        {
            Dictionary<string, object> commentParam = new Dictionary<string, object>();
            commentParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            commentParam.Add("PLANTID", UserInfo.Current.Plant);
            commentParam.Add("BOXNO", boxno);

            DataTable dt = SqlExecuter.Query("SelectLabelList", "10002", commentParam);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                byte[] labelData = dt.Rows[i]["LABELDATA"] as byte[];
                XtraReport report = new XtraReport();
                using (MemoryStream ms = new MemoryStream(labelData))
                {

                    ms.Write(labelData, 0, labelData.Length);

                    report = XtraReport.FromStream(ms);

                    DataRow dr = dt.Rows[i];

                    report = GetLabelDataMappingReport(report, dr, boxno);

                    ImageExportOptions imageOptions = report.ExportOptions.Image;
                    imageOptions.Format = ImageFormat.Bmp;

                    report.ExportToImage(SystemCommonClass.ImageTempPath, imageOptions);

                    int w, h;
                    Bitmap b = new Bitmap(SystemCommonClass.ImageTempPath);
                    w = b.Width; h = b.Height;

                    string ZPLImageDataString = Micube.SmartMES.Commons.ZPLPrint.GetZPLIIImage(b, 20, 20); //ZPLUtil. SendImageToPrinter(1,1,b);
                    StringBuilder ZPLCommand = new StringBuilder();

                    ZPLCommand.AppendLine("^XA");
                    ZPLCommand.AppendLine(ZPLImageDataString);
                    ZPLCommand.AppendLine("^XZ");

                    Commons.ZPLPrint.SendStringToPrinter(ZPLCommand.ToString());
                    b.Dispose();

                    FileInfo fileDel = new FileInfo(SystemCommonClass.ImageTempPath);

                    if (fileDel.Exists) //삭제할 파일이 있는지
                    {
                        fileDel.Delete(); //없어도 에러안남
                    }

                }

            }
        }
        #endregion

        #region - PrintLabel :: 라벨 Print (ZPL) |
        /// <summary>
        /// 라벨 Print (ZPL)
        /// </summary>
        /// <param name="report"></param>
        public static void PrintLabel(XtraReport report)
        {

            ImageExportOptions imageOptions = report.ExportOptions.Image;
            imageOptions.Format = ImageFormat.Bmp;

            report.ExportToImage(SystemCommonClass.ImageTempPath, imageOptions);

            int w, h;
            Bitmap b = new Bitmap(SystemCommonClass.ImageTempPath);
            w = b.Width; h = b.Height;

            string ZPLImageDataString = Micube.SmartMES.Commons.ZPLPrint.GetZPLIIImage(b, 20, 20); //ZPLUtil. SendImageToPrinter(1,1,b);
            StringBuilder ZPLCommand = new StringBuilder();

            ZPLCommand.AppendLine("^XA");
            ZPLCommand.AppendLine(ZPLImageDataString);
            ZPLCommand.AppendLine("^XZ");

            Commons.ZPLPrint.SendStringToPrinter(ZPLCommand.ToString());
            b.Dispose();

            FileInfo fileDel = new FileInfo(SystemCommonClass.ImageTempPath);

            if (fileDel.Exists) //삭제할 파일이 있는지
            {
                fileDel.Delete(); //없어도 에러안남
            }

        }

        /// <summary>
        /// 라벨 Print (ZPL)
        /// </summary>
        /// <param name="report"></param>
        public static void PrintLabel(string command)
        {
            Commons.ZPLPrint.SendStringToPrinter(command);
        }
        #endregion

        #region - GetPackingLabel :: 라벨 매핑 정보 조회하여 서식 Return (LotId) |
        /// <summary>
        /// 라벨 매핑 정보 조회하여 서식 Return (LotId)
        /// </summary>
        /// <param name="lotid"></param>
        /// <returns></returns>
        public static List<XtraReport> GetPackingLabel(string lotid)
        {
            Dictionary<string, object> commentParam = new Dictionary<string, object>();
            commentParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            commentParam.Add("PLANTID", UserInfo.Current.Plant);
            commentParam.Add("LOTID", lotid);

            DataTable dt = SqlExecuter.Query("SelectLabelList", "10001", commentParam);
            List<XtraReport> viewList = new List<XtraReport>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                byte[] labelData = dt.Rows[i]["LABELDATA"] as byte[];
                XtraReport report = new XtraReport();

                using (MemoryStream ms = new MemoryStream(labelData))
                {

                    ms.Write(labelData, 0, labelData.Length);

                    report = XtraReport.FromStream(ms);
                    report.Bookmark = dt.Rows[i]["LABELDEFID"].ToString();
                    report.DisplayName = dt.Rows[i]["LABELDEFNAME"].ToString();
                    DataRow dr = dt.Rows[i];

                    viewList.Add(getLabelDataMappingReport(report, dr, lotid));

                }

            }
            return viewList;
        }
        #endregion

        #region - GetBoxLabel :: 라벨 매핑정보 조회하여 서식 Return (BoxNo) |
        /// <summary>
        /// 라벨 매핑정보 조회하여 서식 Return (BoxNo)
        /// </summary>
        /// <param name="boxno"></param>
        /// <returns></returns>
        public static List<XtraReport> GetBoxLabel(string boxno)
        {
            Dictionary<string, object> commentParam = new Dictionary<string, object>();
            commentParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            commentParam.Add("PLANTID", UserInfo.Current.Plant);
            commentParam.Add("P_LABELTYPE", "Box");
            commentParam.Add("BOXNO", boxno);

            DataTable dt = SqlExecuter.Query("SelectLabelList", "10002", commentParam);
            List<XtraReport> viewList = new List<XtraReport>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                byte[] labelData = dt.Rows[i]["LABELDATA"] as byte[];
                XtraReport report = new XtraReport();

                using (MemoryStream ms = new MemoryStream(labelData))
                {

                    ms.Write(labelData, 0, labelData.Length);

                    report = XtraReport.FromStream(ms);
                    report.Bookmark = dt.Rows[i]["LABELDEFID"].ToString();
                    report.DisplayName = dt.Rows[i]["LABELDEFNAME"].ToString();

                    DataRow dr = dt.Rows[i];

                    viewList.Add(GetLabelDataMappingReport(report, dr, boxno));

                }

            }
            return viewList;
        }
        #endregion

        #region - GetLabelScriptByDefId :: 라벨ID로 라벨 스크립트 조회
        public static string GetLabelScriptByDefId(string labelDefId)
        {
            Dictionary<string, object> commentParam = new Dictionary<string, object>();
            commentParam.Add("P_LABELDEFID", labelDefId);
            DataTable dt = SqlExecuter.Query("SelectLabelList", "10004", commentParam);
            if(dt.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                return dt.Rows[0]["BARCODESCRIPT"].ToString();
            }
        }
        #endregion

        #region - GetBoxLabelType :: 라벨 유형별 매핑정보 조회하여 서식 Return (BoxNo) |
        /// <summary>
        /// 라벨 유형별 매핑정보 조회하여 서식 Return (BoxNo)
        /// </summary>
        /// <param name="boxno">BoxNo</param>
        /// <param name="labelType">라벨 유형</param>
        /// <returns></returns>
        /// 
        public static List<LabelInfo> GetLabelData(string boxno, string labelType, string isShipping, string customerId)
        {
            List<LabelInfo> listLabel = new List<LabelInfo>();

            Dictionary<string, object> commentParam = new Dictionary<string, object>();
            commentParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            commentParam.Add("PLANTID", UserInfo.Current.Plant);
            commentParam.Add("P_LABELTYPE", labelType);
            commentParam.Add("BOXNO", boxno);

            string QueryVersion = string.Empty;

            if (isShipping.Equals("N"))
            {
                commentParam.Add("P_LABELDEFID", "PackingExport");
                QueryVersion = "10003";
            }
            else
            {
                QueryVersion = "10002";
            }
            DataTable dt = SqlExecuter.Query("SelectLabelList", QueryVersion, commentParam);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                LabelInfo labelInfo = new LabelInfo();
                string queryID = Format.GetFullTrimString(dt.Rows[i]["QUERYID"]);
                string quertVersion = Format.GetFullTrimString(dt.Rows[i]["QUERYVERSION"]);
                string barcodeOption = Format.GetFullTrimString(dt.Rows[i]["BARCODEOPTION"]);

                string LabeDef = dt.Rows[i]["LABELDEFID"].ToString();
                string LabelName = dt.Rows[i]["LABELDEFNAME"].ToString();
                string LabelType = dt.Rows[i]["LABELTYPE"].ToString();
                string Parameters = dt.Rows[i]["PARAMETERS"].ToString();
                string FileName = dt.Rows[i]["FILENAME"].ToString();
                string BarcodeType = dt.Rows[i]["BARCODETYPE"].ToString();
                bool IsYApply = (dt.Rows[i]["ISYAPPLY"].ToString() != "N");

                Dictionary<string, object> LabelParam = new Dictionary<string, object>();
                LabelParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                LabelParam.Add("PLANTID", UserInfo.Current.Plant);
                LabelParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                LabelParam.Add("BOXNO", boxno);
                LabelParam.Add("CUSTOMERID", customerId);

                if (!string.IsNullOrEmpty(queryID))
                {
                    DataTable dtLabel = SqlExecuter.Query(queryID, quertVersion, LabelParam);

                    labelInfo.QueryId = queryID;
                    labelInfo.QueryVersion = quertVersion;
                    labelInfo.BoxNo = boxno;
                    labelInfo.LabelID = LabeDef;
                    labelInfo.LabelName = LabelName;
                    labelInfo.LabelDataTable = dtLabel;
                    labelInfo.LabelType = LabelType;
                    labelInfo.RdParameters = Parameters;
                    labelInfo.RdFileName = FileName;
                    labelInfo.BarcodeOption = barcodeOption;
                    labelInfo.BarcodeType = BarcodeType;
                    labelInfo.IsYApply = IsYApply;

                    XmlDocument document = new XmlDocument();
                    try
                    {
                        string script = dt.Rows[i]["BARCODESCRIPT"].ToString();

                        labelInfo.ZplBarcodeScript = script;

                        document.LoadXml(script);
                    }
                    catch
                    {
                        document = null;
                    }
                    labelInfo.XmlBarcodeScript = document;
                    listLabel.Add(labelInfo);
                }
            }
            return listLabel;
        }

        public static List<LabelInfo> GetLabelDataByProductDefId(string productDefId, string labelType, string isShipping)
        {
            List<LabelInfo> listLabel = new List<LabelInfo>();

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("P_PRODUCTDEFID", productDefId);
            param.Add("P_LABELTYPE", labelType);

            string QueryVersion = string.Empty;

            if (isShipping.Equals("N"))
            {
                param.Add("P_LABELDEFID", "PackingExport");
                QueryVersion = "10006";
            }
            else
            {
                QueryVersion = "10005";
            }
            DataTable dt = SqlExecuter.Query("SelectLabelList", QueryVersion, param);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                LabelInfo labelInfo = new LabelInfo();

                string queryID = Format.GetFullTrimString(dt.Rows[i]["QUERYID"]);
                string quertVersion = Format.GetFullTrimString(dt.Rows[i]["QUERYVERSION"]);

                string LabeDef = dt.Rows[i]["LABELDEFID"].ToString();
                string LabelName = dt.Rows[i]["LABELDEFNAME"].ToString();
                string LabelType = dt.Rows[i]["LABELTYPE"].ToString();

                Dictionary<string, object> LabelParam = new Dictionary<string, object>();
                LabelParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                LabelParam.Add("PLANTID", UserInfo.Current.Plant);
                LabelParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                LabelParam.Add("BOXNO", productDefId);

                if (!string.IsNullOrEmpty(queryID))
                {
                    DataTable dtLabel = SqlExecuter.Query(queryID, quertVersion, LabelParam);

                    labelInfo.LabelID = LabeDef;
                    labelInfo.LabelName = LabelName;
                    labelInfo.LabelDataTable = dtLabel;
                    labelInfo.LabelType = LabelType;

                    XmlDocument document = new XmlDocument();
                    try
                    {
                        string script = dt.Rows[i]["BARCODESCRIPT"].ToString();
                        labelInfo.ZplBarcodeScript = script;
                        document.LoadXml(script);
                    }
                    catch
                    {
                        document = null;
                    }
                    labelInfo.XmlBarcodeScript = document;
                    listLabel.Add(labelInfo);
                }
            }
            return listLabel;
        }

        public static List<XtraReport> GetBoxLabelType(string boxno, string labelType)
        {
            Dictionary<string, object> commentParam = new Dictionary<string, object>();
            commentParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            commentParam.Add("PLANTID", UserInfo.Current.Plant);
            commentParam.Add("P_LABELTYPE", labelType);
            commentParam.Add("BOXNO", boxno);

            DataTable dt = SqlExecuter.Query("SelectLabelList", "10002", commentParam);


            List<XtraReport> viewList = new List<XtraReport>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                byte[] labelData = dt.Rows[i]["LABELDATA"] as byte[];
                XtraReport report = new XtraReport();

                using (MemoryStream ms = new MemoryStream(labelData))
                {

                    ms.Write(labelData, 0, labelData.Length);

                    report = XtraReport.FromStream(ms);
                    report.Bookmark = dt.Rows[i]["LABELDEFID"].ToString();
                    report.DisplayName = dt.Rows[i]["LABELDEFNAME"].ToString();

                    DataRow dr = dt.Rows[i];

                    viewList.Add(GetLabelDataMappingReport(report, dr, boxno));

                }

            }
            return viewList;
        }
        #endregion

        #region - GetBoxLabelCase :: Case Label 매핑정보 조회하여 서식 Return (BoxNo) |
        /// <summary>
        /// 라벨 유형별 매핑정보 조회하여 서식 Return (BoxNo)
        /// </summary>
        /// <param name="boxno">BoxNo</param>
        /// <param name="labelType">라벨 유형</param>
        /// <returns></returns>
        public static List<XtraReport> GetBoxLabelCase(string boxno, string CaseNo)
        {
            Dictionary<string, object> commentParam = new Dictionary<string, object>();
            commentParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            commentParam.Add("PLANTID", UserInfo.Current.Plant);
            commentParam.Add("BOXNO", boxno);
            commentParam.Add("P_CASENO", CaseNo);

            DataTable dt = SqlExecuter.Query("SelectLabelCase", "10001", commentParam);
            List<XtraReport> viewList = new List<XtraReport>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                byte[] labelData = dt.Rows[i]["LABELDATA"] as byte[];
                XtraReport report = new XtraReport();

                using (MemoryStream ms = new MemoryStream(labelData))
                {

                    ms.Write(labelData, 0, labelData.Length);

                    report = XtraReport.FromStream(ms);
                    report.Bookmark = dt.Rows[i]["LABELDEFID"].ToString();
                    report.DisplayName = dt.Rows[i]["LABELDEFNAME"].ToString();

                    DataRow dr = dt.Rows[i];

                    viewList.Add(GetLabelDataMappingReport(report, dr, boxno));

                }

            }
            return viewList;
        }
        #endregion

        #region - GetBoxLabelXOUTOuter :: X-OUT 외부 Label 매핑정보 조회하여 서식 Return (BoxNo) |
        /// <summary>
        /// X-OUT 외부 Label 매핑정보 조회하여 서식 Return (BoxNo)
        /// </summary>
        /// <param name="boxno">BoxNo</param>
        /// <returns></returns>
        public static List<XtraReport> GetBoxLabelXOUTOuter(string boxno)
        {
            Dictionary<string, object> commentParam = new Dictionary<string, object>();
            commentParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            commentParam.Add("PLANTID", UserInfo.Current.Plant);
            commentParam.Add("BOXNO", boxno);

            DataTable dt = SqlExecuter.Query("SelectLabelXOutOuter", "10001", commentParam);
            List<XtraReport> viewList = new List<XtraReport>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                byte[] labelData = dt.Rows[i]["LABELDATA"] as byte[];
                XtraReport report = new XtraReport();

                using (MemoryStream ms = new MemoryStream(labelData))
                {

                    ms.Write(labelData, 0, labelData.Length);

                    report = XtraReport.FromStream(ms);
                    report.Bookmark = dt.Rows[i]["LABELDEFID"].ToString();
                    report.DisplayName = dt.Rows[i]["LABELDEFNAME"].ToString();

                    DataRow dr = dt.Rows[i];

                    viewList.Add(GetLabelDataMappingReport(report, dr, boxno));

                }

            }
            return viewList;
        }
        #endregion

        #region - GetBoxLabelXOUTInner :: X-OUT 내부 Label 매핑정보 조회하여 서식 Return (BoxNo) |
        /// <summary>
        /// X-OUT 내부 Label 매핑정보 조회하여 서식 Return (BoxNo)
        /// </summary>
        /// <param name="boxno">BoxNo</param>
        /// <param name="serialno">SerialNo</param>
        /// <returns></returns>
        public static List<XtraReport> GetBoxLabelXOUTInner(string boxno, string serialno)
        {
            Dictionary<string, object> commentParam = new Dictionary<string, object>();
            commentParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            commentParam.Add("PLANTID", UserInfo.Current.Plant);
            commentParam.Add("BOXNO", boxno);
            commentParam.Add("SERIALNO", serialno);

            DataTable dt = SqlExecuter.Query("SelectLabelXOutInner", "10001", commentParam);
            List<XtraReport> viewList = new List<XtraReport>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                byte[] labelData = dt.Rows[i]["LABELDATA"] as byte[];
                XtraReport report = new XtraReport();

                using (MemoryStream ms = new MemoryStream(labelData))
                {

                    ms.Write(labelData, 0, labelData.Length);

                    report = XtraReport.FromStream(ms);
                    report.Bookmark = dt.Rows[i]["LABELDEFID"].ToString();
                    report.DisplayName = dt.Rows[i]["LABELDEFNAME"].ToString();

                    DataRow dr = dt.Rows[i];

                    viewList.Add(GetLabelDataMappingReport(report, dr, boxno));

                }

            }
            return viewList;
        }
        #endregion

        #region - getLabelDataMappingReport :: 라벨 서식 적용 및 Tab별 출력 |
        /// <summary>
        /// 라벨 서식 적용 및 Tab별 출력
        /// </summary>
        /// <param name="report"></param>
        /// <param name="dr"></param>
        /// <param name="lotid"></param>
        /// <returns></returns>
        private static XtraReport getLabelDataMappingReport(XtraReport report, DataRow dr, string lotid)
        {

            string queryID = dr["QUERYID"].ToString();
            string quertVersion = dr["QUERYVERSION"].ToString();

            Dictionary<string, object> commentParam = new Dictionary<string, object>();
            commentParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            commentParam.Add("PLANTID", UserInfo.Current.Plant);
            commentParam.Add("LOTID", lotid);
            //         commentParam.Add("P_TYPE", "TEST");

            if (!string.IsNullOrEmpty(queryID))
            {
                DataTable dt = SqlExecuter.Query(queryID, quertVersion, commentParam);


                report.Tag = dt;

                Band detailBand = report.Bands.GetBandByType(typeof(DetailBand));

                //detailBand.Controls

                foreach (XRControl control in detailBand.Controls)
                {
                    if (control is DevExpress.XtraReports.UI.XRLabel)
                    {
                        if (!string.IsNullOrEmpty(control.Tag.ToString()))
                        {
                            if (dt.Columns.IndexOf(control.Tag.ToString()) > -1)
                            {
                                control.Text = dt.Rows[0][control.Tag.ToString()].ToString();
                            }
                            // control.Text = dr[""].ToString()
                        }


                    }
                    else if (control is DevExpress.XtraReports.UI.XRBarCode)
                    {
                        if (!string.IsNullOrEmpty(control.Tag.ToString()))
                        {
                            if (dt.Columns.IndexOf(control.Tag.ToString()) > -1)
                            {
                                control.Text = dt.Rows[0][control.Tag.ToString()].ToString();
                            }
                            // control.Text = dr[""].ToString()
                        }

                    }
                    else if (control is DevExpress.XtraReports.UI.XRTable)
                    {
                        XRTable xt = control as XRTable;

                        foreach (XRTableRow tr in xt.Rows)
                        {
                            for (int i = 0; i < tr.Cells.Count; i++)
                            {
                                if (!string.IsNullOrEmpty(tr.Cells[i].Tag.ToString()))
                                {
                                    if (dt.Columns.IndexOf(tr.Cells[i].Tag.ToString()) > -1)
                                    {
                                        tr.Cells[i].Text = dt.Rows[0][tr.Cells[i].Tag.ToString()].ToString();
                                    }


                                }

                            }
                        }

                    }

                }
            }

            return report;
        }
        #endregion

        #region - GetLabelDataMappingReport :: 라벨 서식 적용 및 Tab별 출력 (BoxNo) |
        /// <summary>
        /// BOXNO를 통해 QUERY ID 실행
        /// </summary>
        /// <param name="report"></param>
        /// <param name="dr"></param>
        /// <param name="boxno"></param>
        /// <returns></returns>
        private static XtraReport GetLabelDataMappingReport(XtraReport report, DataRow dr, string boxno)
        {

            string queryID = dr["QUERYID"].ToString();
            string quertVersion = dr["QUERYVERSION"].ToString();

            Dictionary<string, object> commentParam = new Dictionary<string, object>();
            commentParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            commentParam.Add("PLANTID", UserInfo.Current.Plant);
            commentParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            commentParam.Add("BOXNO", boxno);
            //         commentParam.Add("P_TYPE", "TEST");

            if (!string.IsNullOrEmpty(queryID))
            {
                DataTable dt = SqlExecuter.Query(queryID, quertVersion, commentParam);

                report.Tag = dt;

                Band detailBand = report.Bands.GetBandByType(typeof(DetailBand));

                //detailBand.Controls

                foreach (XRControl control in detailBand.Controls)
                {
                    if (control is DevExpress.XtraReports.UI.XRLabel)
                    {
                        if (!string.IsNullOrEmpty(control.Tag.ToString()))
                        {
                            if (dt.Columns.IndexOf(control.Tag.ToString()) > -1)
                            {
                                control.Text = dt.Rows[0][control.Tag.ToString()].ToString();
                            }
                            // control.Text = dr[""].ToString()
                        }


                    }
                    else if (control is DevExpress.XtraReports.UI.XRBarCode)
                    {
                        if (!string.IsNullOrEmpty(control.Tag.ToString()))
                        {
                            if (dt.Columns.IndexOf(control.Tag.ToString()) > -1)
                            {
                                control.Text = dt.Rows[0][control.Tag.ToString()].ToString();
                            }
                            // control.Text = dr[""].ToString()
                        }

                    }
                    else if (control is DevExpress.XtraReports.UI.XRTable)
                    {
                        XRTable xt = control as XRTable;

                        foreach (XRTableRow tr in xt.Rows)
                        {
                            for (int i = 0; i < tr.Cells.Count; i++)
                            {
                                if (!string.IsNullOrEmpty(tr.Cells[i].Tag.ToString()))
                                {
                                    if (dt.Columns.IndexOf(tr.Cells[i].Tag.ToString()) > -1)
                                    {
                                        tr.Cells[i].Text = dt.Rows[0][tr.Cells[i].Tag.ToString()].ToString();
                                    }


                                }

                            }
                        }

                    }

                }
            }

            return report;
        }
        #endregion

        #region - GetExportLabel :: 수출포장 라벨 매핑정보 조회하여 서식 Return (BoxNo) |
        /// <summary>
        /// 라벨 매핑정보 조회하여 서식 Return (BoxNo)
        /// </summary>
        /// <param name="boxno"></param>
        /// <returns></returns>
        public static List<XtraReport> GetExportLabel(string boxno)
        {
            Dictionary<string, object> commentParam = new Dictionary<string, object>();
            commentParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            commentParam.Add("PLANTID", UserInfo.Current.Plant);
            commentParam.Add("P_LABELDEFID", "PackingExport");
            commentParam.Add("BOXNO", boxno);

            DataTable dt = SqlExecuter.Query("SelectLabelList", "10003", commentParam);
            List<XtraReport> viewList = new List<XtraReport>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                byte[] labelData = dt.Rows[i]["LABELDATA"] as byte[];
                XtraReport report = new XtraReport();

                using (MemoryStream ms = new MemoryStream(labelData))
                {

                    ms.Write(labelData, 0, labelData.Length);

                    report = XtraReport.FromStream(ms);
                    report.Bookmark = dt.Rows[i]["LABELDEFID"].ToString();
                    report.DisplayName = dt.Rows[i]["LABELDEFNAME"].ToString();

                    DataRow dr = dt.Rows[i];

                    viewList.Add(GetLabelDataMappingReport(report, dr, boxno));

                }

            }
            return viewList;
        }
        #endregion


        #region :: LABEL별로 추가로 넣어야할 속성 추가
        public static Dictionary<string, object> AddLabePropertySDCUtypeCase(DataTable dt, Dictionary<string, object> dic,string Seq,string Qty)
        {

            DataTable dtValue = dt.Copy();

            string[] addList = Constants.SDCUtypeCaseAddList.Split(',');

            for (int i = 0; i < addList.Length; i++)
            {
                string addItem = addList[i];

                if (dic.ContainsKey(addItem))
                {
                    dic.Remove(addItem);
                }
                if (addItem.Equals("EXPIREDDATE"))
                {
                    if (!dtValue.Columns.Contains(addItem))
                    {
                        dtValue.Columns.Add("EXPIREDDATE");
                    }

                    string warranty = Format.GetString(dic["WARRANTY"]);
                    string boxdate = Format.GetTrimString(dic["BOXDATE"]);

                    string expired = string.Empty;

                    if (warranty.Equals("1") || warranty.Equals("6MONTH"))
                    {
                        expired = (DateTime.Parse(boxdate).AddMonths(6)).ToString("yyyy'/'MM'/'dd");
                    }
                    else if (warranty.Equals("2") || warranty.Equals("1YEAR"))
                    {
                        expired = (DateTime.Parse(boxdate).AddYears(1)).ToString("yyyy'/'MM'/'dd");
                    }
                    dtValue.Rows[0]["EXPIREDDATE"] = expired;

                }
                else if(addItem.Equals("ULOT"))
                {
                    string partNumber = Format.GetTrimString(dic["PARTNO"]);
                    partNumber = partNumber.Substring(partNumber.Length-6, 6);
                    string lotno = Format.GetTrimString(dic["LOTNOA"]).TrimStart('Y').TrimEnd('0');
                    string BoxWeek = Format.GetTrimString(dic["BOXWEEK"]);
                    string boxdate = Format.GetTrimString(dic["BOXDATE"]);
                    string date = DateTime.Parse(boxdate).ToString("MMdd");
                    string dateyear = DateTime.Parse(boxdate).ToString("yyyy");
                    dateyear = dateyear.Substring(partNumber.Length - 3, 1);
                    date = dateyear + date;
                    string Lotid = string.Format("{0}{1}{2}{3}{4}{5}", "Y", partNumber, lotno, BoxWeek, date, Seq);
                    if (!dtValue.Columns.Contains(addItem))
                    {
                        dtValue.Columns.Add("ULOT");
                    }
                    dtValue.Rows[0]["ULOT"] = Lotid;
         
                }
                else if (addItem.Equals("LOTNO"))
                {
                    string tmpLotid = "Y" + Format.GetTrimString(dtValue.Rows[0]["LOTNO"]);
                    dtValue.Rows[0]["LOTNO"] = tmpLotid;
                }
                if (dtValue.Columns.Contains(addItem))
                {
                    if (!string.IsNullOrEmpty(Format.GetTrimString(dtValue.Rows[0][addItem])))
                    {
                        dic.Add(addItem, (Format.GetTrimString(dtValue.Rows[0][addItem])));
                    }
                }
            }
            dic["QTY"] = Qty;

            return dic;
        }
        
        public static Dictionary<string,object> AddLabePropertySDCBOX(DataTable dt, Dictionary<string,object> dic )
        {

            string[] addList = Constants.SDCBoxAddList.Split(',');
            DataTable dtValue = dt.Copy();

            for (int i=0; i < addList.Length; i++)
            {
                string addItem = addList[i];

                if (!dic.ContainsKey(addItem))
                {
                    if (addItem.Equals("EXPIREDDATE"))
                    {
                        if (!dtValue.Columns.Contains(addItem))
                        {
                            dtValue.Columns.Add("EXPIREDDATE");
                        }

                        string warranty = Format.GetString(dic["WARRANTY"]);
                        string boxdate = Format.GetTrimString(dic["BOXDATE"]);

                        string expired = string.Empty;

                        if (warranty.Equals("1") || warranty.Equals("6MONTH"))
                        {
                            expired = (DateTime.Parse(boxdate).AddMonths(6)).ToString("yyyy'/'MM'/'dd");
                        }
                        else if (warranty.Equals("2") || warranty.Equals("1YEAR"))
                        {
                            expired = (DateTime.Parse(boxdate).AddYears(1)).ToString("yyyy'/'MM'/'dd");
                        }
                        dtValue.Rows[0]["EXPIREDDATE"] = expired;

                    }
                    else if (addItem.Equals("LOTNO"))
                    {
                        string tmpLotid = "Y" +  Format.GetTrimString(dtValue.Rows[0]["LOTNO"]);
                        dtValue.Rows[0]["LOTNO"] = tmpLotid;
                    }
                    if (dtValue.Columns.Contains(addItem))
                    {
                        if (!string.IsNullOrEmpty(Format.GetTrimString(dtValue.Rows[0][addItem])))
                        {
                            dic.Add(addItem, (Format.GetTrimString(dtValue.Rows[0][addItem])));
                        }
                    }
                }
            }

            return dic;
        }

        #endregion
        #endregion

        #region ◆ 공정 관련 공통 ::: 

        #region - GetLotRoutingList :: 
        /// <summary>
        /// GetLotRoutingList
        /// </summary>
        /// <param name="type"></param>
        /// <param name="lotid"></param>
        /// <returns></returns>
        private static DataTable GetLotRoutingList(LotCardType type, string lotid, string lotCardVer = "Ver1")
        {
            DataTable dt = new DataTable();

            if (type == LotCardType.Normal)
            {
                dt = SqlExecuter.Query("SelectProcessResultAndRoutingForLotCard_Normal", "10001", new Dictionary<string, object>() { { "LOTID", lotid }, { "LANGUAGETYPE", UserInfo.Current.LanguageType } });
            }
            else if (type == LotCardType.Return)
            {
                dt = SqlExecuter.Query("SelectProcessResultAndRoutingForLotCard_Return", "10001", new Dictionary<string, object>() { { "LOTID", lotid }, { "LANGUAGETYPE", UserInfo.Current.LanguageType } });
            }
            else if (type == LotCardType.Split)
            {
                string[] LotList = lotid.Split(',');

                for (int i = 0; i < LotList.Length; i++)
                {
                    string tmpLot = LotList[i];

                    DataTable lot = SqlExecuter.Query("SelectProcessResultAndRoutingForLotCard_Split", "10001", new Dictionary<string, object>() { { "LOTID", tmpLot }, { "LANGUAGETYPE", UserInfo.Current.LanguageType } });

                    dt.Merge(lot);
                }
            }
            else if (type == LotCardType.Rework)
            {
                if (lotCardVer == "Ver1")
                    dt = SqlExecuter.Query("SelectProcessResultAndRoutingForLotCard_Rework", "10001", new Dictionary<string, object>() { { "LOTID", lotid }, { "LANGUAGETYPE", UserInfo.Current.LanguageType } });               
                else            
                    dt = SqlExecuter.Query("SelectProcessResultAndRoutingForLotCard_Rework", "10002", new Dictionary<string, object>() { { "LOTID", lotid }, { "LANGUAGETYPE", UserInfo.Current.LanguageType } });
                
            }
            return dt;
        }
        #endregion

        #region - setReportRoutingListRework :: Rework Routing 조회 |
        /// <summary>
        /// Rework Routing 조회
        /// </summary>
        /// <param name="lotId"></param>
        /// <param name="printProcessRowPerPage"></param>
        /// <param name="dtProcess"></param>
        /// <returns></returns>
        private static DataTable setReportRoutingListRework(string lotId, int printProcessRowPerPage, DataTable dtProcess)
        {

            DataTable dtProcessList = new DataTable();
            dtProcessList.Columns.Add("LOTID", typeof(string));
            dtProcessList.Columns.Add("SEQUENCE", typeof(string));
            dtProcessList.Columns.Add("CHANGESTATE", typeof(string));
            dtProcessList.Columns.Add("PROCESSNAME", typeof(string));
            dtProcessList.Columns.Add("AREA", typeof(string));
            dtProcessList.Columns.Add("REMARK", typeof(string));
            dtProcessList.Columns.Add("DATE", typeof(string));
            dtProcessList.Columns.Add("PROCESSQTY", typeof(string));
            dtProcessList.Columns.Add("WORKPLACEWORKER", typeof(string));

            List<string> lotList = lotId.Split(',').ToList();
            lotList.ForEach(id =>
            {
                DataTable tempTable = dtProcess.Select("LOTID = '" + id + "'").CopyToDataTable().Rows.Cast<DataRow>().CopyToDataTable();

                int totalProcessCount = tempTable.Rows.Count;
                int pageCount = (totalProcessCount / (printProcessRowPerPage * 2)) + ((totalProcessCount % (printProcessRowPerPage * 2) == 0) ? 0 : 1);

                int rowNumber = 0;

                DataTable tempResult = dtProcessList.Clone();

                for (int i = 0; i < pageCount; i++)
                {
                    for (int j = 0; j < printProcessRowPerPage; j++)
                    {


                        if (rowNumber > totalProcessCount - 1) break;

                        DataRow processRow = tempTable.Rows[rowNumber];

                        DataRow newRow = tempResult.NewRow();
   




                        newRow["LOTID"] = processRow["LOTID"];
                        newRow["SEQUENCE"] = processRow["SEQUENCE"];
                        newRow["CHANGESTATE"] = processRow["CHANGESTATE"];
                        newRow["PROCESSNAME"] = processRow["PROCESSNAME"];
                        newRow["AREA"] = processRow["AREA"];
                        newRow["REMARK"] = processRow["REMARK"];
                        newRow["DATE"] = processRow["DATE"];
                        newRow["PROCESSQTY"] = processRow["PROCESSQTY"];
                        newRow["WORKPLACEWORKER"] = processRow["WORKPLACEWORKER"];

                        

                        tempResult.Rows.Add(newRow);

                        rowNumber++;
                    }
                }

                dtProcessList.Merge(tempResult);
            });

            return dtProcessList;
        }
        #endregion

        #region - setReportRoutingListNormal :: Rework Routing Lot Card 출력 데이터 조회 |
        /// <summary>
        /// Rework Routing Lot Card 출력 데이터 조회
        /// </summary>
        /// <param name="lotId"></param>
        /// <param name="printProcessRowPerPage"></param>
        /// <param name="dtProcess"></param>
        /// <returns></returns>
        private static DataTable setReportRoutingListNormal(string lotId, int printProcessRowPerPage, DataTable dtProcess)
        {

            DataTable dtProcessList = new DataTable();
            dtProcessList.Columns.Add("LOTID", typeof(string));
            dtProcessList.Columns.Add("SEQUENCE1", typeof(string));
            dtProcessList.Columns.Add("CHANGESTATE1", typeof(string));
            dtProcessList.Columns.Add("PROCESSNAME1", typeof(string));
            dtProcessList.Columns.Add("PROCESSINGDATE1", typeof(string));
            dtProcessList.Columns.Add("PROCESSQTY1", typeof(string));
            dtProcessList.Columns.Add("WORKPLACEWORKER1", typeof(string));
            dtProcessList.Columns.Add("SEQUENCE2", typeof(string));
            dtProcessList.Columns.Add("CHANGESTATE2", typeof(string));
            dtProcessList.Columns.Add("PROCESSNAME2", typeof(string));
            dtProcessList.Columns.Add("PROCESSINGDATE2", typeof(string));
            dtProcessList.Columns.Add("PROCESSQTY2", typeof(string));
            dtProcessList.Columns.Add("WORKPLACEWORKER2", typeof(string));

            List<string> lotList = lotId.Split(',').ToList();
            lotList.ForEach(id =>
            {
                DataTable tempTable = dtProcess.Select("LOTID = '" + id + "'").CopyToDataTable().Rows.Cast<DataRow>().OrderBy(row => row["SEQUENCE"]).CopyToDataTable();

                int totalProcessCount = tempTable.Rows.Count;
                int pageCount = (totalProcessCount / (printProcessRowPerPage * 2)) + ((totalProcessCount % (printProcessRowPerPage * 2) == 0) ? 0 : 1);

                int rowNumber = 0;

                DataTable tempResult = dtProcessList.Clone();

                for (int i = 0; i < pageCount; i++)
                {
                    for (int j = 0; j < printProcessRowPerPage; j++)
                    {
                        rowNumber = (i * printProcessRowPerPage * 2) + j;

                        if (rowNumber >= totalProcessCount)
                            break;

                        DataRow processRow = tempTable.Rows[rowNumber];

                        DataRow newRow = tempResult.NewRow();


                        newRow["LOTID"] = processRow["LOTID"];
                        newRow["SEQUENCE1"] = processRow["USERSEQUENCE"];
                        newRow["CHANGESTATE1"] = processRow["DIVISION"];
                        newRow["PROCESSNAME1"] = processRow["PROCESSSEGMENTNAME"];
                        newRow["PROCESSINGDATE1"] = processRow["SENDTIME"];
                        newRow["PROCESSQTY1"] = processRow["SENDQTY"];
                        newRow["WORKPLACEWORKER1"] = processRow["SENDUSER"];


                        tempResult.Rows.Add(newRow);
                    }

                    for (int j = 0; j < printProcessRowPerPage; j++)
                    {
                        rowNumber = (i * printProcessRowPerPage * 2) + (j + printProcessRowPerPage);

                        if (rowNumber >= totalProcessCount)
                            break;

                        DataRow processRow = tempTable.Rows[rowNumber];

                        DataRow newRow = tempResult.Rows[rowNumber - (printProcessRowPerPage * (i + 1))];

                        newRow["LOTID"] = processRow["LOTID"];
                        newRow["SEQUENCE2"] = processRow["USERSEQUENCE"];
                        newRow["CHANGESTATE2"] = processRow["DIVISION"];
                        newRow["PROCESSNAME2"] = processRow["PROCESSSEGMENTNAME"];
                        newRow["PROCESSINGDATE2"] = processRow["SENDTIME"];
                        newRow["PROCESSQTY2"] = processRow["SENDQTY"];
                        newRow["WORKPLACEWORKER2"] = processRow["SENDUSER"];
                    }
                }

                dtProcessList.Merge(tempResult);
            });

            return dtProcessList;
        }
        #endregion

        #region - PrintLotCard :: Lot Card를 출력 한다. (lotId, Printtype, printProcessRowPerPage)  |

        /// <summary>
        /// Lot Card를 출력 한다.(2021년 수정버전, 유태근)
        /// </summary>
        /// <param name="lotId">출력 할 Lot Id (복수 가능, ','(콤마)로 구분)</param>
        /// <param name="LotCardType">Report File Stream</param>
        /// <param name="printProcessRowPerPage">페이지 당 보여줄 공정 줄 수</param>
        public static void PrintLotCard_Ver2(string lotId, LotCardType Printtype)
        {
            #region 변수

            int lotCardCurrentIndex = 0; // 현재 LOT CARD 출력순서
            string lotCardCurrentLotId = ""; // 현재 LOT CARD 출력 ID

            #endregion

            #region 데이터 조회

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("LOTID", lotId);

            DataSet dsReport = new DataSet();

            DataTable dtQueryResult = SqlExecuter.Query("SelectLotInfoOnPrintLotCard", "10002", param);
            DataTable dtLotInfo = dtQueryResult.Clone();

            dtQueryResult.Rows.Cast<DataRow>().ForEach(lotRow =>
            {
                DataRow newRow = dtLotInfo.NewRow();
                newRow.ItemArray = lotRow.ItemArray.Clone() as object[];

                dtLotInfo.Rows.Add(newRow);
            });

            dtLotInfo.AcceptChanges();
            dtLotInfo.TableName = "LotInfoDt";
            
            dsReport.Tables.Add(dtLotInfo); // LOT_CARD 대상 LOT정보

            DataTable dtProcess = GetLotRoutingList(Printtype, lotId, "Ver2");
            DataTable copyDtProcess = dtProcess.Copy();
            copyDtProcess.TableName = "processDt";
            dsReport.Tables.Add(copyDtProcess);  // LOT_CARD 공정정보

            DataTable goldPlateDt = SqlExecuter.Query("GetLotCardGoldPlatingSegmentSpec", "10001", new Dictionary<string, object>() { { "LOTID", lotId }, { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "ENTERPRISEID", UserInfo.Current.Enterprise } });
            DataTable copyDtGoldPlate = goldPlateDt.Copy();
            copyDtGoldPlate.TableName = "goldPlatingDt";
            dsReport.Tables.Add(copyDtGoldPlate); // LOT_CARD 금도금 공정정보

            DataTable bomTreeDt = SqlExecuter.Query("GetLotCardBomTree", "10001", new Dictionary<string, object>() { { "LOTID", lotId }, { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "ENTERPRISEID", UserInfo.Current.Enterprise } });
            DataTable copySubAssemblyDt = new DataTable();
            if (bomTreeDt.Rows.Cast<DataRow>().Where(r => r["MASTERDATACLASSID"].Equals("SubAssembly")).Count() > 0)
                copySubAssemblyDt = bomTreeDt.Rows.Cast<DataRow>().Where(r => r["MASTERDATACLASSID"].Equals("SubAssembly")).CopyToDataTable();
            else
                copySubAssemblyDt = bomTreeDt.Clone(); 
            copySubAssemblyDt.TableName = "SubAssemblyDt";
            dsReport.Tables.Add(copySubAssemblyDt); // LOT_CARD 반제품 정보

            DataTable copyMaterialDt = new DataTable();
            if (bomTreeDt.Rows.Cast<DataRow>().Where(r => !r["MASTERDATACLASSID"].Equals("SubAssembly")).Count() > 0)
                copyMaterialDt = bomTreeDt.Rows.Cast<DataRow>().Where(r => !r["MASTERDATACLASSID"].Equals("SubAssembly")).CopyToDataTable();
            else
                copyMaterialDt = bomTreeDt.Clone();
            copyMaterialDt.TableName = "MaterialDt";
            dsReport.Tables.Add(copyMaterialDt); // LOT_CARD 자재 정보

            Assembly assembly = Assembly.GetAssembly(Type.GetType("Micube.SmartMES.Commons.CommonFunction"));

            string path = Constants.LotCardPath_YP; // LOT_CARD 레포트파일 경로

            Stream stream = assembly.GetManifestResourceStream(path);

            XtraReport report = XtraReport.FromStream(stream);
            report.DataSource = dsReport;
            report.DataMember = "LotInfoDt";

            #endregion

            #region 첫번째 페이지

            ///////////////////////////////////////////// 메인 LOT정보 영역 /////////////////////////////////////////////
            DataRelation mainReportRel = new DataRelation("MainReportRel", dtLotInfo.Columns["LOTID"], copyDtProcess.Columns["LOTID"]);
            dsReport.Relations.Add(mainReportRel);

            DetailReportBand mainReport = report.Bands["MainReport"] as DetailReportBand;
            mainReport.DataSource = dsReport;
            mainReport.DataMember = "MainReportRel";

            // 다국어 바인딩
            setLabelLaungage(mainReport, "MainReportHeader");

            // 헤더 바인딩
            Band mainReportHeader = mainReport.Bands["MainReportHeader"];
            SetReportControlDataBinding(mainReportHeader.Controls, dsReport, "", Printtype);

            SubBand comments = mainReportHeader.SubBands[0];        
            SetReportControlDataBinding(comments.Controls, dsReport, "", Printtype);

            // 반제품일땐 특이사항 항목을 보여주지 않음
            comments.BeforePrint += (s, e) =>
            {
                if (Format.GetString(dtLotInfo.Rows[lotCardCurrentIndex]["PRODUCTCLASSID"]) != "Product")
                {
                    e.Cancel = true;
                }

                lotCardCurrentLotId = Format.GetString(dtLotInfo.Rows[lotCardCurrentIndex]["LOTID"]);
            };

            // 로고 바인딩
            XRControl picLogo = mainReportHeader.FindControl("picLogo", true);

            if (picLogo != null && picLogo is XRPictureBox picBox)
            {
                if (UserInfo.Current.Enterprise == "INTERFLEX")
                {
                    picBox.Image = Properties.Resources.Logo_Interflex;
                    picBox.Sizing = ImageSizeMode.Squeeze;
                }
                else if (UserInfo.Current.Enterprise == "YOUNGPOONG")
                {
                    picBox.Image = Properties.Resources.Logo_Youngpoong;
                    picBox.Sizing = ImageSizeMode.Squeeze;
                }
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////

            ///////////////////////////////////////////// 금도금 공정정보 영역 ///////////////////////////////////////////////////
            DataRelation goldPlatingSegmentSpecRel = new DataRelation("goldPlatingSegmentSpecRel", dtLotInfo.Columns["LOTID"], copyDtGoldPlate.Columns["LOTID"]);
            dsReport.Relations.Add(goldPlatingSegmentSpecRel);

            DetailReportBand goldPlatingSegmentSpecReport = report.Bands["GoldPlatingSegmentSpecReport"] as DetailReportBand;
            goldPlatingSegmentSpecReport.DataSource = dsReport;
            goldPlatingSegmentSpecReport.DataMember = "goldPlatingSegmentSpecRel";

            // 다국어 바인딩
            setLabelLaungage(goldPlatingSegmentSpecReport, "GoldPlatingSegmentSpecReportHeader");

            // 헤더 바인딩
            Band goldPlatingSegmentSpecReportHeader = goldPlatingSegmentSpecReport.Bands["GoldPlatingSegmentSpecReportHeader"];
            SetReportControlDataBinding(goldPlatingSegmentSpecReportHeader.Controls, dsReport, "", Printtype);

            // 디테일 바인딩
            Band goldPlatingSegmentSpecReportDetail = goldPlatingSegmentSpecReport.Bands["GoldPlatingSegmentSpecReportDetail"];
            SetReportControlDataBinding(goldPlatingSegmentSpecReportDetail.Controls, dsReport, "goldPlatingSegmentSpecRel", Printtype);

            goldPlatingSegmentSpecReport.BeforePrint += (s, e) =>
            {
                if (copyDtGoldPlate.AsEnumerable().Where(r => r["LOTID"].Equals(lotCardCurrentLotId)).Count() < 1)
                {
                    e.Cancel = true;
                }
            };
            /////////////////////////////////////////////////////////////////////////////////////////////////////////

            ///////////////////////////////////////////// 반제품 정보영역 /////////////////////////////////////////////    
            DataRelation subAssemblyRel = new DataRelation("SubAssemblyRel", dtLotInfo.Columns["LOTID"], copySubAssemblyDt.Columns["LOTID"]);
            dsReport.Relations.Add(subAssemblyRel);

            DetailReportBand subAssemblyReport = report.Bands["SubAssemblyReport"] as DetailReportBand;
            subAssemblyReport.DataSource = dsReport;
            subAssemblyReport.DataMember = "SubAssemblyRel";

            // 다국어 바인딩
            setLabelLaungage(subAssemblyReport, "SubAssemblyReportHeader");

            // 헤더 바인딩
            Band subAssemblyReportHeader = subAssemblyReport.Bands["SubAssemblyReportHeader"];
            SetReportControlDataBinding(subAssemblyReportHeader.Controls, dsReport, "", Printtype);

            // 디테일 바인딩
            Band subAssemblyReportDetail = subAssemblyReport.Bands["SubAssemblyReportDetail"];
            SetReportControlDataBinding(subAssemblyReportDetail.Controls, dsReport, "SubAssemblyRel", Printtype);

            subAssemblyReport.BeforePrint += (s, e) =>
            {
                if (copySubAssemblyDt.AsEnumerable().Where(r => r["LOTID"].Equals(lotCardCurrentLotId)).Count() < 1)
                {
                    e.Cancel = true;
                }
            };
            //////////////////////////////////////////////////////////////////////////////////////////////////////

            ///////////////////////////////////////////// 자재 정보영역 /////////////////////////////////////////////
            DataRelation materialRel = new DataRelation("MaterialRel", dtLotInfo.Columns["LOTID"], copyMaterialDt.Columns["LOTID"]);
            dsReport.Relations.Add(materialRel);

            DetailReportBand materialReport = report.Bands["MaterialReport"] as DetailReportBand;
            materialReport.DataSource = dsReport;
            materialReport.DataMember = "MaterialRel";

            // 다국어 바인딩
            setLabelLaungage(materialReport, "MaterialReportHeader");

            // 헤더 바인딩
            Band materialReportHeader = materialReport.Bands["MaterialReportHeader"];
            SetReportControlDataBinding(materialReportHeader.Controls, dsReport, "", Printtype);

            // 디테일 바인딩
            Band materialReportDetail = materialReport.Bands["MaterialReportDetail"];
            SetReportControlDataBinding(materialReportDetail.Controls, dsReport, "MaterialRel", Printtype);

            materialReport.BeforePrint += (s, e) =>
            {
                if (copyMaterialDt.AsEnumerable().Where(r => r["LOTID"].Equals(lotCardCurrentLotId)).Count() < 1)
                {
                    e.Cancel = true;
                }

                if (dtLotInfo.Rows[lotCardCurrentIndex]["PRODUCTCLASSID"].Equals("Product"))
                {
                    report.Bands["MaterialReport"].PageBreak = PageBreak.AfterBand;                
                }
                else
                {
                    report.Bands["MaterialReport"].PageBreak = PageBreak.None;
                }
            };
            //////////////////////////////////////////////////////////////////////////////////////////////////////

            #endregion

            #region 두번째 페이지

            ///////////////////////////////////////////// 공정정보영역 /////////////////////////////////////////////
            //DataRelation processRel = new DataRelation("ProcessRel", dtLotInfo.Columns["LOTID"], copyDtProcess.Columns["LOTID"]);
            //dsReport.Relations.Add(processRel); 

            DetailReportBand processReport = report.Bands["ProcessReport"] as DetailReportBand;
            processReport.DataSource = dsReport;
            processReport.DataMember = "MainReportRel";

            // 다국어 바인딩
            setLabelLaungage(processReport, "ProcessReportHeader");

            // 헤더 바인딩
            Band processReportHeader = processReport.Bands["ProcessReportHeader"];
            SetReportControlDataBinding(processReportHeader.Controls, dsReport, "", Printtype);

            // 디테일 바인딩
            Band processReportDetail = processReport.Bands["ProcessReportDetail"];
            SetReportControlDataBinding(processReportDetail.Controls, dsReport, "MainReportRel", Printtype);
            //////////////////////////////////////////////////////////////////////////////////////////////////////

            #endregion

            #region 세번째 페이지

            ///////////////////////////////////////////// NCR 이력정보영역 ///////////////////////////////////////   
            DetailReportBand NCRReport = report.Bands["NCRReport"] as DetailReportBand;
            NCRReport.BeforePrint += (s, e) =>
            {
                // 제품이 아니라면 NCR 레포트 삭제
                if (Format.GetString(dtLotInfo.Rows[lotCardCurrentIndex]["PRODUCTCLASSID"]) != "Product")
                {
                    e.Cancel = true;
                }
                else
                {
                    // 제품중에서 양산이 아니라면 NCR 레포트 삭제
                    if (Format.GetString(dtLotInfo.Rows[lotCardCurrentIndex]["PRODUCTIONTYPEID"]) != "Production")
                    {
                        e.Cancel = true;
                    }
                }

                lotCardCurrentIndex++;
            };
            //////////////////////////////////////////////////////////////////////////////////////////////////////

            #endregion

            #region 워터마크 처리

            // Page 별 WaterMark 처리(RC Lot 인 경우)
            report.CreateDocument();

            List<int> rcWaterMarkIndex = new List<int>(); // 변경품
            List<int> reworkWaterMarkIndex = new List<int>(); // 재작업
            List<int> mrbWaterMarkIndex = new List<int>(); // MRB

            for (int i = 0; i < dtLotInfo.Rows.Count; i++)
            {
                DataRow lotRow = dtLotInfo.Rows[i];

                string curLotId = Format.GetString(lotRow["LOTID"]);
                string isRCLot = Format.GetString(lotRow["ISRCLOT"]);
                string isRework = Format.GetString(lotRow["ISREWORK"]);
                string isMRB = Format.GetString(lotRow["ISMRB"]);
      
                int curLotPageCnt = 10;

                if (isRCLot == "Y")
                {
                    for (int j = 0; j < curLotPageCnt; j++)
                    {
                        rcWaterMarkIndex.Add(j);
                    }
                }
                else
                {
                    if (isRework == "Y")
                    {
                        if (isMRB == "Y")
                        {
                            for (int j = 0; j < curLotPageCnt; j++)
                            {
                                mrbWaterMarkIndex.Add(j);
                            }
                        }
                        else
                        {
                            for (int j = 0; j < curLotPageCnt; j++)
                            {
                                reworkWaterMarkIndex.Add(j);
                            }
                        }
                    }
                }
            }

            foreach (Page page in report.PrintingSystem.Pages)
            {
                if (rcWaterMarkIndex.Contains(page.Index))
                    page.AssignWatermark(CreatePageWatermark("CHANGEREVISION"));
                else if (reworkWaterMarkIndex.Contains(page.Index))
                    page.AssignWatermark(CreatePageWatermark("REWORK"));
                else if (mrbWaterMarkIndex.Contains(page.Index))
                    page.AssignWatermark(CreatePageWatermark("MRB"));
            }

            #endregion
    
            report.ShowPreviewDialog();
        }

        /// <summary>
        /// Lot Card를 출력 한다.
        /// </summary>
        /// <param name="lotId">출력 할 Lot Id (복수 가능, ','(콤마)로 구분)</param>
        /// <param name="LotCardType">Report File Stream</param>
        /// <param name="printProcessRowPerPage">페이지 당 보여줄 공정 줄 수</param>
        public static void PrintLotCard(string lotId, LotCardType Printtype, int printProcessRowPerPage = 25)
        {
            if (UserInfo.Current.Enterprise == Constants.EnterPrise_YoungPoong)
                printProcessRowPerPage = 24;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("LOTID", lotId);

            DataSet dsReport = new DataSet();

            DataTable dtQueryResult = SqlExecuter.Query("SelectLotInfoOnPrintLotCard", "10001", param);
            DataTable dtLotInfo = dtQueryResult.Clone();

            dtQueryResult.Rows.Cast<DataRow>().ForEach(lotRow =>
            {
                DataRow newRow = dtLotInfo.NewRow();
                newRow.ItemArray = lotRow.ItemArray.Clone() as object[];

                dtLotInfo.Rows.Add(newRow);
            });


            dtLotInfo.AcceptChanges();

            dsReport.Tables.Add(dtLotInfo);

            //DataTable dtProcess = SqlExecuter.Query("SelectProcessResultAndRoutingForLotCard_Normal", "10001", new Dictionary<string, object>() { { "LOTID", lotId }, { "LANGUAGETYPE", UserInfo.Current.LanguageType } });
            DataTable dtProcess = GetLotRoutingList(Printtype, lotId);

            DataTable dtProcessList = new DataTable();
            if (Printtype == LotCardType.Rework)
                dtProcessList = setReportRoutingListRework(lotId, printProcessRowPerPage, dtProcess);
            else
                dtProcessList = setReportRoutingListNormal(lotId, printProcessRowPerPage, dtProcess);


            dtProcessList.AcceptChanges();





            dsReport.Tables.Add(dtProcessList);

            DataRelation relation = new DataRelation("RelationLotId", dtLotInfo.Columns["LOTID"], dtProcessList.Columns["LOTID"]);

            dsReport.Relations.Add(relation);

            Assembly assembly = Assembly.GetAssembly(Type.GetType("Micube.SmartMES.Commons.CommonFunction"));

            string path = string.Empty;
            switch (Printtype)
            {
                case LotCardType.Normal:
                case LotCardType.Merge:
                case LotCardType.Split:
                case LotCardType.RCChange:
                case LotCardType.Return:
                    if (UserInfo.Current.Enterprise.Equals(Constants.EnterPrise_InterFlex))
                        path = Constants.NormaLotCardPath;
                    else
                        path = Constants.NormaLotCardPath_YP;
                    break;
                case LotCardType.Rework:
                    path = Constants.ReworkLotCardPath;
                    break;

            }

            Stream stream = assembly.GetManifestResourceStream(path);

            XtraReport report = XtraReport.FromStream(stream);
            report.DataSource = dsReport;
            report.DataMember = "Table1";

            //if (Printtype.Equals(LotCardType.Rework))
            //{
            //    Watermark textWatermark = new Watermark();
            //    Font ft = new Font("HYGothic-Medium", 100, FontStyle.Bold);
            //    textWatermark.Text = Language.Get("REWORK");
            //    textWatermark.TextDirection = DirectionMode.ForwardDiagonal;
            //    textWatermark.Font = ft;
            //    textWatermark.ForeColor = Color.DodgerBlue;
            //    textWatermark.TextTransparency = 150;
            //    textWatermark.ShowBehind = false;
            //    report.Watermark.CopyFrom(textWatermark);
            //}
            Band band = report.Bands["Detail"];
            //SetReportControlDataBinding(band.Controls, dsReport.Tables[0]);


            DetailReportBand detailReport = report.Bands["DetailReport"] as DetailReportBand;
            detailReport.DataSource = dsReport;
            detailReport.DataMember = "RelationLotId";

            Band groupHeader = detailReport.Bands["GroupHeader1"];
            SetReportControlDataBinding(groupHeader.Controls, dsReport, "", Printtype);

            XRControl picLogo = groupHeader.FindControl("picLogo", true);

            if (picLogo != null && picLogo is XRPictureBox picBox)
            {
                if (UserInfo.Current.Enterprise == "INTERFLEX")
                    picBox.Image = Properties.Resources.Logo_Interflex;
                else if (UserInfo.Current.Enterprise == "YOUNGPOONG")
                    picBox.Image = Properties.Resources.Logo_Youngpoong;
            }

            Band detailBand = detailReport.Bands["Detail1"];
            setLabelLaungage(detailReport);
            SetReportControlDataBinding(detailBand.Controls, dsReport, "RelationLotId", Printtype);

            //report.AfterPrint += (sender, e) =>
            //{
            //    XtraReport rpt = sender as XtraReport;

            //    foreach (Page page in rpt.PrintingSystem.Pages)
            //    {
            //        PageWatermark textWatermark = new PageWatermark();
            //        Font ft = new Font("HYGothic-Medium", 100, FontStyle.Bold);
            //        textWatermark.Text = rpt.Pages.IndexOf(page).ToString();
            //        textWatermark.TextDirection = DirectionMode.ForwardDiagonal;
            //        textWatermark.Font = ft;
            //        textWatermark.ForeColor = Color.DodgerBlue;
            //        textWatermark.TextTransparency = 150;
            //        textWatermark.ShowBehind = false;
            //        page.AssignWatermark(textWatermark);
            //    }
            //};

            // Page 별 WaterMark 처리 (RC Lot 인 경우)
            report.CreateDocument();

            List<int> rcWaterMarkIndex = new List<int>();
            List<int> reworkWaterMarkIndex = new List<int>();
            List<int> mrbWaterMarkIndex = new List<int>();

            int plusIndex = 0;

            for (int i = 0; i < dtLotInfo.Rows.Count; i++)
            {
                DataRow lotRow = dtLotInfo.Rows[i];

                string curLotId = Format.GetString(lotRow["LOTID"]);
                string isRCLot = Format.GetString(lotRow["ISRCLOT"]);
                string isRework = Format.GetString(lotRow["ISREWORK"]);
                string isMRB = Format.GetString(lotRow["ISMRB"]);


                int curLotPageCnt = Format.GetInteger(Math.Ceiling(Format.GetDecimal(dtProcessList.AsEnumerable().Where(r => Format.GetString(r["LOTID"]) == curLotId).Count()) / Format.GetDecimal(printProcessRowPerPage)));

                if (isRCLot == "Y")
                {
                    for (int j = 0; j < curLotPageCnt; j++)
                    {
                        rcWaterMarkIndex.Add(i + plusIndex + j);
                    }
                }
                else
                {
                    if (isRework == "Y")
                    {
                        if (isMRB == "Y")
                        {
                            for (int j = 0; j < curLotPageCnt; j++)
                            {
                                mrbWaterMarkIndex.Add(i + plusIndex + j);
                            }
                        }
                        else
                        {
                            for (int j = 0; j < curLotPageCnt; j++)
                            {
                                reworkWaterMarkIndex.Add(i + plusIndex + j);
                            }
                        }
                    }
                }

                plusIndex += curLotPageCnt - 1;
            }

            foreach (Page page in report.PrintingSystem.Pages)
            {
                if (rcWaterMarkIndex.Contains(page.Index))
                    page.AssignWatermark(CreatePageWatermark("CHANGEREVISION"));
                else if (reworkWaterMarkIndex.Contains(page.Index))
                    page.AssignWatermark(CreatePageWatermark("REWORK"));
                else if (mrbWaterMarkIndex.Contains(page.Index))
                    page.AssignWatermark(CreatePageWatermark("MRB"));
            }

            //report.Print();
            report.ShowPreviewDialog();
        }

        /// <summary>
        /// RC Lot 인 경우 Page Watermark 생성
        /// </summary>
        /// <returns>생성한 Page Watermark 개체</returns>
        private static PageWatermark CreatePageRCChangeWatermark()
        {
            PageWatermark watermark = new PageWatermark();
            Font ft = new Font("HYGothic-Medium", 100, FontStyle.Bold);
            watermark.Text = Language.Get("CHANGEREVISION");
            watermark.TextDirection = DirectionMode.ForwardDiagonal;
            watermark.Font = ft;
            watermark.ForeColor = Color.DodgerBlue;
            watermark.TextTransparency = 150;
            watermark.ShowBehind = false;

            return watermark;
        }

        private static PageWatermark CreatePageWatermark(string languageKey)
        {
            PageWatermark watermark = new PageWatermark();
            Font ft = new Font("HYGothic-Medium", 100, FontStyle.Bold);
            watermark.Text = Language.Get(languageKey);
            watermark.TextDirection = DirectionMode.ForwardDiagonal;
            watermark.Font = ft;
            watermark.ForeColor = Color.DodgerBlue;
            watermark.TextTransparency = 150;
            watermark.ShowBehind = false;

            return watermark;
        }

        #endregion

        #region - PrintLotCard :: Lot Card를 출력 한다. (lotId, Stream, printProcessRowPerPage) |
        /// <summary>
        /// Lot Card를 출력 한다.
        /// </summary>
        /// <param name="lotId">출력 할 Lot Id (복수 가능, ','(콤마)로 구분)</param>
        /// <param name="stream">Report File Stream</param>
        /// <param name="printProcessRowPerPage">페이지 당 보여줄 공정 줄 수</param>
        public static void PrintLotCard(string lotId, Stream stream, int printProcessRowPerPage = 25)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("LOTID", lotId);

            DataSet dsReport = new DataSet();

            DataTable dtQueryResult = SqlExecuter.Query("SelectLotInfoOnPrintLotCard", "10001", param);
            DataTable dtLotInfo = dtQueryResult.Clone();

            dtQueryResult.Rows.Cast<DataRow>().ForEach(lotRow =>
            {
                DataRow newRow = dtLotInfo.NewRow();
                newRow.ItemArray = lotRow.ItemArray.Clone() as object[];

                dtLotInfo.Rows.Add(newRow);
            });


            dtLotInfo.AcceptChanges();

            dsReport.Tables.Add(dtLotInfo);

            DataTable dtProcess = SqlExecuter.Query("SelectProcessResultAndRoutingForLotCard_Normal", "10001", new Dictionary<string, object>() { { "LOTID", lotId }, { "LANGUAGETYPE", UserInfo.Current.LanguageType } });

            DataTable dtProcessList = new DataTable();
            dtProcessList.Columns.Add("LOTID", typeof(string));
            dtProcessList.Columns.Add("SEQUENCE1", typeof(string));
            dtProcessList.Columns.Add("CHANGESTATE1", typeof(string));
            dtProcessList.Columns.Add("PROCESSNAME1", typeof(string));
            dtProcessList.Columns.Add("PROCESSINGDATE1", typeof(string));
            dtProcessList.Columns.Add("PROCESSQTY1", typeof(string));
            dtProcessList.Columns.Add("WORKPLACEWORKER1", typeof(string));
            dtProcessList.Columns.Add("SEQUENCE2", typeof(string));
            dtProcessList.Columns.Add("CHANGESTATE2", typeof(string));
            dtProcessList.Columns.Add("PROCESSNAME2", typeof(string));
            dtProcessList.Columns.Add("PROCESSINGDATE2", typeof(string));
            dtProcessList.Columns.Add("PROCESSQTY2", typeof(string));
            dtProcessList.Columns.Add("WORKPLACEWORKER2", typeof(string));

            List<string> lotList = lotId.Split(',').ToList();
            lotList.ForEach(id =>
            {
                DataTable tempTable = dtProcess.Select("LOTID = '" + id + "'").CopyToDataTable().Rows.Cast<DataRow>().OrderBy(row => row["SEQUENCE"]).CopyToDataTable();

                int totalProcessCount = tempTable.Rows.Count;
                int pageCount = (totalProcessCount / (printProcessRowPerPage * 2)) + ((totalProcessCount % (printProcessRowPerPage * 2) == 0) ? 0 : 1);

                int rowNumber = 0;

                DataTable tempResult = dtProcessList.Clone();

                for (int i = 0; i < pageCount; i++)
                {
                    for (int j = 0; j < printProcessRowPerPage; j++)
                    {
                        rowNumber = (i * printProcessRowPerPage * 2) + j;

                        if (rowNumber >= totalProcessCount)
                            break;

                        DataRow processRow = tempTable.Rows[rowNumber];

                        DataRow newRow = tempResult.NewRow();

                        newRow["LOTID"] = processRow["LOTID"];
                        newRow["SEQUENCE1"] = processRow["USERSEQUENCE"];
                        newRow["CHANGESTATE1"] = processRow["DIVISION"];
                        newRow["PROCESSNAME1"] = processRow["PROCESSSEGMENTNAME"];
                        newRow["PROCESSINGDATE1"] = processRow["SENDTIME"];
                        newRow["PROCESSQTY1"] = processRow["SENDQTY"];
                        newRow["WORKPLACEWORKER1"] = processRow["SENDUSER"];


                        tempResult.Rows.Add(newRow);
                    }

                    for (int j = 0; j < printProcessRowPerPage; j++)
                    {
                        rowNumber = (i * printProcessRowPerPage * 2) + (j + printProcessRowPerPage);

                        if (rowNumber >= totalProcessCount)
                            break;

                        DataRow processRow = tempTable.Rows[rowNumber];

                        DataRow newRow = tempResult.Rows[rowNumber - (printProcessRowPerPage * (i + 1))];

                        newRow["LOTID"] = processRow["LOTID"];
                        newRow["SEQUENCE2"] = processRow["USERSEQUENCE"];
                        newRow["CHANGESTATE2"] = processRow["DIVISION"];
                        newRow["PROCESSNAME2"] = processRow["PROCESSSEGMENTNAME"];
                        newRow["PROCESSINGDATE2"] = processRow["SENDTIME"];
                        newRow["PROCESSQTY2"] = processRow["SENDQTY"];
                        newRow["WORKPLACEWORKER2"] = processRow["SENDUSER"];
                    }
                }

                dtProcessList.Merge(tempResult);
            });


            dtProcessList.AcceptChanges();

            dsReport.Tables.Add(dtProcessList);

            DataRelation relation = new DataRelation("RelationLotId", dtLotInfo.Columns["LOTID"], dtProcessList.Columns["LOTID"]);

            dsReport.Relations.Add(relation);

            //string fileName = "";

            //switch (type)
            //{
            //    case LotCardType.ProductionSample:
            //        fileName = "Lot Card_Production.repx";
            //        break;
            //    case LotCardType.Split:
            //        break;
            //    default:
            //        break;
            //}

            //XtraReport report = XtraReport.FromFile(Path.Combine(Application.StartupPath, "Reports", fileName));
            XtraReport report = XtraReport.FromStream(stream);
            report.DataSource = dsReport;
            report.DataMember = "Table1";

            Band band = report.Bands["Detail"];
            //SetReportControlDataBinding(band.Controls, dsReport.Tables[0]);


            DetailReportBand detailReport = report.Bands["DetailReport"] as DetailReportBand;
            detailReport.DataSource = dsReport;
            detailReport.DataMember = "RelationLotId";

            Band groupHeader = detailReport.Bands["GroupHeader1"];
            SetReportControlDataBinding(groupHeader.Controls, dsReport, "");

            XRControl picLogo = groupHeader.FindControl("picLogo", true);

            if (picLogo != null && picLogo is XRPictureBox picBox)
            {
                if (UserInfo.Current.Enterprise == "INTERFLEX")
                    picBox.Image = Properties.Resources.Logo_Interflex;
                else if (UserInfo.Current.Enterprise == "YOUNGPOONG")
                    picBox.Image = Properties.Resources.Logo_Youngpoong;
            }

            Band detailBand = detailReport.Bands["Detail1"];
            setLabelLaungage(detailReport);
            SetReportControlDataBinding(detailBand.Controls, dsReport, "RelationLotId");


            //report.Print();
            report.ShowPreviewDialog();
        }
        #endregion

        #region  - SetReportControlDataBinding :: Report 파일의 컨트롤 중 Tag(FieldName) 값이 있는 컨트롤에 DataBinding(Text)를 추가한다. |
        /// <summary>
        /// Report 파일의 컨트롤 중 Tag(FieldName) 값이 있는 컨트롤에 DataBinding(Text)를 추가한다.
        /// </summary>
        /// <param name="controls"></param>
        /// <param name="dataSource"></param>
        private static void SetReportControlDataBinding(XRControlCollection controls, DataSet dataSource, string relationId, LotCardType type = LotCardType.Normal)
        {
            if (controls.Count > 0)
            {
                foreach (XRControl control in controls)
                {
                    if (!string.IsNullOrWhiteSpace(control.Tag.ToString()) && !control.Name.Substring(0, 3).Equals("lbl"))
                    {
                        /*
                        if (control.Name == "barProductDefIdVersion" && type != LotCardType.RCChange)
                        {
                            control.Visible = false;
                            continue;
                        }
                        else
                        {
                            control.Visible = true;
                        }
                        */
                        string fieldName = "";

                        if (!string.IsNullOrWhiteSpace(relationId))
                            fieldName = string.Join(".", relationId, control.Tag.ToString());
                        else
                            fieldName = control.Tag.ToString();

                        control.DataBindings.Add("Text", dataSource, fieldName);
                    }

                    SetReportControlDataBinding(control.Controls, dataSource, relationId, type);
                }
            }
        }
        #endregion

        #region - CheckLotProcessStateByStepType :: Lot 정보의 StepType과 ProcessState에 따라 4-Step 화면에서 처리 가능 여부를 판단한다. |
        /// <summary>
        /// Lot 정보의 StepType과 ProcessState에 따라 4-Step 화면에서 처리 가능 여부를 판단한다.
        /// </summary>
        /// <param name="processType">현재 화면의 작업 구분</param>
        /// <param name="processState">해당 Lot의 Process State</param>
        /// <param name="stepType">Lot의 현재 공정의 Step Type</param>
        /// <returns>해당 Lot 정보가 현재 화면에서 작업 처리가 가능한지 여부</returns>
        public static bool CheckLotProcessStateByStepType(ProcessType processType, string processState, string stepType)
        {
            string processStateByCurrentStep = GetProcessStateByProcessType(processType);
            string[] stepList = stepType.Split(',');


            // Process State 다국어 정보 조회
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("CODECLASSID", "WipProcessState");
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable processStateDictionary = SqlExecuter.Query("GetCodeList", "00001", param);

            // Message 정보 조회
            Language.LanguageMessageItem notArrivalCurrentStateMsg = Language.GetMessage("NotArrivalCurrentState");
            Language.LanguageMessageItem alreadyPassCurrentStateMsg = Language.GetMessage("AlreadyPassCurrentState");
            Language.LanguageMessageItem invalidProcessStateMsg = Language.GetMessage("InvalidProcessState");
            Language.LanguageMessageItem invalidProcessStateInStepTypeMsg = Language.GetMessage("InvalidProcessStateInStepType");
            string state = "";


            // 유효하지 않은 Process Type 인지 체크
            if (string.IsNullOrWhiteSpace(processStateByCurrentStep))
                return false;

            // 현재 Process State가 Step Type에 있는지 체크
            if (stepList.Length < 1 || !stepList.Contains(processStateByCurrentStep))
            {
                // Lot 자원의 Step Type 에서 현재 진행상태가 유효하지 않습니다. Step Type : {0}, Process State : {1}
                MSGBox.Show(MessageBoxType.Information, invalidProcessStateInStepTypeMsg.Title, invalidProcessStateInStepTypeMsg.Message, stepType, processState);
                return false;
            }

            switch (processType)
            {
                case ProcessType.LotAccept:
                    if (processState.Equals(Constants.WaitForReceive))
                    {
                        return true;
                    }
                    else if (processState.Equals(Constants.Wait))
                    {
                        state = Format.GetString(processStateDictionary.Select("CODEID = 'Receive'").FirstOrDefault()["CODENAME"]);

                        // 인수 처리된 Lot 입니다. 인수 등록 처리를 진행할 수 없습니다.
                        MSGBox.Show(MessageBoxType.Information, alreadyPassCurrentStateMsg.Title, alreadyPassCurrentStateMsg.Message, state, Language.Get("MENU_PG-SG-0160"));
                        return false;
                    }
                    else if (processState.Equals(Constants.Run))
                    {
                        state = Format.GetString(processStateDictionary.Select("CODEID = 'WorkStart'").FirstOrDefault()["CODENAME"]);

                        // 작업시작 처리된 Lot 입니다. 인수 등록 처리를 진행할 수 없습니다.
                        MSGBox.Show(MessageBoxType.Information, alreadyPassCurrentStateMsg.Title, alreadyPassCurrentStateMsg.Message, state, Language.Get("MENU_PG-SG-0160"));
                        return false;
                    }
                    else if (processState.Equals(Constants.WaitForSend))
                    {
                        state = Format.GetString(processStateDictionary.Select("CODEID = 'WorkEnd'").FirstOrDefault()["CODENAME"]);

                        // 작업완료 처리된 Lot 입니다. 인수 등록 처리를 진행할 수 없습니다.
                        MSGBox.Show(MessageBoxType.Information, alreadyPassCurrentStateMsg.Title, alreadyPassCurrentStateMsg.Message, state, Language.Get("MENU_PG-SG-0160"));
                        return false;
                    }
                    else
                    {
                        // 유효하지 않은 진행상태 입니다. Process State : {0}
                        MSGBox.Show(MessageBoxType.Information, invalidProcessStateMsg.Title, invalidProcessStateMsg.Message, processState);
                        return false;
                    }
                case ProcessType.StartWork:
                    if (processState.Equals(Constants.WaitForReceive))
                    {
                        if (!stepList.Contains(Constants.WaitForReceive))
                        {
                            return true;
                        }
                        else
                        {
                            state = Format.GetString(processStateDictionary.Select("CODEID = 'Receive'").FirstOrDefault()["CODENAME"]);

                            // 인수 처리가 진행되지 않았습니다. 인수 등록 처리를 먼저 진행하시기 바랍니다.
                            MSGBox.Show(MessageBoxType.Information, notArrivalCurrentStateMsg.Title, notArrivalCurrentStateMsg.Message, state, Language.Get("MENU_PG-SG-0160"));
                            return false;
                        }
                    }
                    else if (processState.Equals(Constants.Wait))
                    {
                        if (stepList.Contains(Constants.WaitForReceive))
                        {
                            return true;
                        }
                        else
                        {
                            // Lot 자원의 Step Type 에서 현재 진행상태가 유효하지 않습니다. Step Type : {0}, Process State : {1}
                            MSGBox.Show(MessageBoxType.Information, invalidProcessStateInStepTypeMsg.Title, invalidProcessStateInStepTypeMsg.Message, stepType, processState);
                            return false;
                        }
                    }
                    else if (processState.Equals(Constants.Run))
                    {
                        state = Format.GetString(processStateDictionary.Select("CODEID = 'WorkStart'").FirstOrDefault()["CODENAME"]);

                        // 작업시작 처리된 Lot 입니다. 작업 시작 처리를 진행할 수 없습니다.
                        MSGBox.Show(MessageBoxType.Information, alreadyPassCurrentStateMsg.Title, alreadyPassCurrentStateMsg.Message, state, Language.Get("MENU_PG-SG-0190"));
                        return false;
                    }
                    else if (processState.Equals(Constants.WaitForSend))
                    {
                        state = Format.GetString(processStateDictionary.Select("CODEID = 'WorkEnd'").FirstOrDefault()["CODENAME"]);

                        // 작업완료 처리된 Lot 입니다. 작업 시작 처리를 진행할 수 없습니다.
                        MSGBox.Show(MessageBoxType.Information, alreadyPassCurrentStateMsg.Title, alreadyPassCurrentStateMsg.Message, state, Language.Get("MENU_PG-SG-0190"));
                        return false;
                    }
                    else
                    {
                        // 유효하지 않은 진행상태 입니다. Process State : {0}
                        MSGBox.Show(MessageBoxType.Information, invalidProcessStateMsg.Title, invalidProcessStateMsg.Message, processState);
                        return false;
                    }
                case ProcessType.WorkCompletion:
                    if (processState.Equals(Constants.WaitForReceive))
                    {
                        if (!stepList.Contains(Constants.WaitForReceive))
                        {
                            if (!stepList.Contains(Constants.Wait))
                            {
                                return true;
                            }
                            else
                            {
                                state = Format.GetString(processStateDictionary.Select("CODEID = 'WorkStart'").FirstOrDefault()["CODENAME"]);

                                // 작업시작 처리가 진행되지 않았습니다. 작업 시작 처리를 먼저 진행하시기 바랍니다.
                                MSGBox.Show(MessageBoxType.Information, notArrivalCurrentStateMsg.Title, notArrivalCurrentStateMsg.Message, state, Language.Get("MENU_PG-SG-0190"));
                                return false;
                            }
                        }
                        else
                        {
                            state = Format.GetString(processStateDictionary.Select("CODEID = 'Receive'").FirstOrDefault()["CODENAME"]);

                            // 인수 처리가 진행되지 않았습니다. 인수 등록 처리를 먼저 진행하시기 바랍니다.
                            MSGBox.Show(MessageBoxType.Information, notArrivalCurrentStateMsg.Title, notArrivalCurrentStateMsg.Message, state, Language.Get("MENU_PG-SG-0160"));
                            return false;
                        }
                    }
                    else if (processState.Equals(Constants.Wait))
                    {
                        if (stepList.Contains(Constants.WaitForReceive))
                        {
                            if (!stepList.Contains(Constants.Wait))
                            {
                                return true;
                            }
                            else
                            {
                                state = Format.GetString(processStateDictionary.Select("CODEID = 'WorkStart'").FirstOrDefault()["CODENAME"]);

                                // 작업시작 처리가 진행되지 않았습니다. 작업 시작 처리를 먼저 진행하시기 바랍니다.
                                MSGBox.Show(MessageBoxType.Information, notArrivalCurrentStateMsg.Title, notArrivalCurrentStateMsg.Message, state, Language.Get("MENU_PG-SG-0190"));
                                return false;
                            }
                        }
                        else
                        {
                            // Lot 자원의 Step Type 에서 현재 진행상태가 유효하지 않습니다. Step Type : {0}, Process State : {1}
                            MSGBox.Show(MessageBoxType.Information, invalidProcessStateInStepTypeMsg.Title, invalidProcessStateInStepTypeMsg.Message, stepType, processState);
                            return false;
                        }
                    }
                    else if (processState.Equals(Constants.Run))
                    {
                        if (stepList.Contains(Constants.Wait))
                        {
                            return true;
                        }
                        else
                        {
                            // Lot 자원의 Step Type 에서 현재 진행상태가 유효하지 않습니다. Step Type : {0}, Process State : {1}
                            MSGBox.Show(MessageBoxType.Information, invalidProcessStateInStepTypeMsg.Title, invalidProcessStateInStepTypeMsg.Message, stepType, processState);
                            return false;
                        }
                    }
                    else if (processState.Equals(Constants.WaitForSend))
                    {
                        state = Format.GetString(processStateDictionary.Select("CODEID = 'WorkEnd'").FirstOrDefault()["CODENAME"]);

                        // 작업완료 처리된 Lot 입니다. 작업 완료 처리를 진행할 수 없습니다.
                        MSGBox.Show(MessageBoxType.Information, alreadyPassCurrentStateMsg.Title, alreadyPassCurrentStateMsg.Message, state, Language.Get("MENU_PG-SG-0220"));
                        return false;
                    }
                    else
                    {
                        // 유효하지 않은 진행상태 입니다. Process State : {0}
                        MSGBox.Show(MessageBoxType.Information, invalidProcessStateMsg.Title, invalidProcessStateMsg.Message, processState);
                        return false;
                    }
                case ProcessType.TransitRegist:
                    if (processState.Equals(Constants.WaitForReceive))
                    {
                        if (!stepList.Contains(Constants.WaitForReceive))
                        {
                            if (!stepList.Contains(Constants.Wait))
                            {
                                if (!stepList.Contains(Constants.Run))
                                {
                                    return true;
                                }
                                else
                                {
                                    state = Format.GetString(processStateDictionary.Select("CODEID = 'WorkEnd'").FirstOrDefault()["CODENAME"]);

                                    // 작업완료 처리가 진행되지 않았습니다. 작업 완료 처리를 먼저 진행하시기 바랍니다.
                                    MSGBox.Show(MessageBoxType.Information, notArrivalCurrentStateMsg.Title, notArrivalCurrentStateMsg.Message, state, Language.Get("MENU_PG-SG-0220"));
                                    return false;
                                }
                            }
                            else
                            {
                                state = Format.GetString(processStateDictionary.Select("CODEID = 'WorkStart'").FirstOrDefault()["CODENAME"]);

                                // 작업시작 처리가 진행되지 않았습니다. 작업 시작 처리를 먼저 진행하시기 바랍니다.
                                MSGBox.Show(MessageBoxType.Information, notArrivalCurrentStateMsg.Title, notArrivalCurrentStateMsg.Message, state, Language.Get("MENU_PG-SG-0190"));
                                return false;
                            }
                        }
                        else
                        {
                            state = Format.GetString(processStateDictionary.Select("CODEID = 'Receive'").FirstOrDefault()["CODENAME"]);

                            // 인수 처리가 진행되지 않았습니다. 인수 등록 처리를 먼저 진행하시기 바랍니다.
                            MSGBox.Show(MessageBoxType.Information, notArrivalCurrentStateMsg.Title, notArrivalCurrentStateMsg.Message, state, Language.Get("MENU_PG-SG-0160"));
                            return false;
                        }
                    }
                    else if (processState.Equals(Constants.Wait))
                    {
                        if (stepList.Contains(Constants.WaitForReceive))
                        {
                            if (!stepList.Contains(Constants.Wait))
                            {
                                if (!stepList.Contains(Constants.Run))
                                {
                                    return true;
                                }
                                else
                                {
                                    state = Format.GetString(processStateDictionary.Select("CODEID = 'WorkEnd'").FirstOrDefault()["CODENAME"]);

                                    // 작업완료 처리가 진행되지 않았습니다. 작업 완료 처리를 먼저 진행하시기 바랍니다.
                                    MSGBox.Show(MessageBoxType.Information, notArrivalCurrentStateMsg.Title, notArrivalCurrentStateMsg.Message, state, Language.Get("MENU_PG-SG-0220"));
                                    return false;
                                }
                            }
                            else
                            {
                                state = Format.GetString(processStateDictionary.Select("CODEID = 'WorkStart'").FirstOrDefault()["CODENAME"]);

                                // 작업시작 처리가 진행되지 않았습니다. 작업 시작 처리를 먼저 진행하시기 바랍니다.
                                MSGBox.Show(MessageBoxType.Information, notArrivalCurrentStateMsg.Title, notArrivalCurrentStateMsg.Message, state, Language.Get("MENU_PG-SG-0190"));
                                return false;
                            }
                        }
                        else
                        {
                            // Lot 자원의 Step Type 에서 현재 진행상태가 유효하지 않습니다. Step Type : {0}, Process State : {1}
                            MSGBox.Show(MessageBoxType.Information, invalidProcessStateInStepTypeMsg.Title, invalidProcessStateInStepTypeMsg.Message, stepType, processState);
                            return false;
                        }
                    }
                    else if (processState.Equals(Constants.Run))
                    {
                        if (stepList.Contains(Constants.Wait))
                        {
                            if (!stepList.Contains(Constants.Run))
                            {
                                return true;
                            }
                            else
                            {
                                state = Format.GetString(processStateDictionary.Select("CODEID = 'WorkEnd'").FirstOrDefault()["CODENAME"]);

                                // 작업완료 처리가 진행되지 않았습니다. 작업 완료 처리를 먼저 진행하시기 바랍니다.
                                MSGBox.Show(MessageBoxType.Information, notArrivalCurrentStateMsg.Title, notArrivalCurrentStateMsg.Message, state, Language.Get("MENU_PG-SG-0220"));
                                return false;
                            }
                        }
                        else
                        {
                            // Lot 자원의 Step Type 에서 현재 진행상태가 유효하지 않습니다. Step Type : {0}, Process State : {1}
                            MSGBox.Show(MessageBoxType.Information, invalidProcessStateInStepTypeMsg.Title, invalidProcessStateInStepTypeMsg.Message, stepType, processState);
                            return false;
                        }
                    }
                    else if (processState.Equals(Constants.WaitForSend))
                    {
                        if (stepList.Contains(Constants.Run))
                        {
                            return true;
                        }
                        else
                        {
                            // Lot 자원의 Step Type 에서 현재 진행상태가 유효하지 않습니다. Step Type : {0}, Process State : {1}
                            MSGBox.Show(MessageBoxType.Information, invalidProcessStateInStepTypeMsg.Title, invalidProcessStateInStepTypeMsg.Message, stepType, processState);
                            return false;
                        }
                    }
                    else
                    {
                        // 유효하지 않은 진행상태 입니다. Process State : {0}
                        MSGBox.Show(MessageBoxType.Information, invalidProcessStateMsg.Title, invalidProcessStateMsg.Message, processState);
                        return false;
                    }
                default:
                    return false;
            }
        }
        #endregion

        #region  - GetProcessStateByProcessType :: 화면의 작업 구분에 따라 해당 화면에서 사용되는 Process State 정보를 반환한다. |
        /// <summary>
        /// 화면의 작업 구분에 따라 해당 화면에서 사용되는 Process State 정보를 반환한다.
        /// </summary>
        /// <param name="processType">현재 화면의 작업 구분</param>
        /// <returns>작업 구분에 따라 Lot이 가져야하는 Process State 문자열</returns>
        private static string GetProcessStateByProcessType(ProcessType processType)
        {
            switch (processType)
            {
                case ProcessType.LotAccept:
                    return Constants.WaitForReceive;
                case ProcessType.StartWork:
                    return Constants.Wait;
                case ProcessType.WorkCompletion:
                    return Constants.Run;
                case ProcessType.TransitRegist:
                    return Constants.WaitForSend;
                default:
                    return "";
            }
        }
        #endregion

        #region - CheckRCLot :: Lot의 RC 여부에 따라 Lot Card Revision을 비교하고 Revision이 다른 경우 Revision Lot Card를 출력하도록 메시지를 보여준다. |
        /// <summary>
        /// Lot의 RC 여부에 따라 Lot Card Revision을 비교하고 Revision이 다른 경우 Revision Lot Card를 출력하도록 메시지를 보여준다.
        /// </summary>
        /// <param name="lotInfo">공정 4-Step 화면에서 조회한 Lot 정보</param>
        /// <returns></returns>
        public static bool CheckRCLot(DataTable lotInfo)
        {
            DataRow row = lotInfo.Rows.Cast<DataRow>().FirstOrDefault();

            string isPrintLotCard = row["ISPRINTLOTCARD"].ToString();
            string isPrintRcLotCard = row["ISPRINTRCLOTCARD"].ToString();

            if (isPrintLotCard == "N" && isPrintRcLotCard == "Y")
            {
                string lotId = row["LOTID"].ToString();
                string productDefId = row["PRODUCTDEFID"].ToString();
                string productDefVersion = row["PRODUCTDEFVERSION"].ToString();

                string productRevision = productDefId + productDefVersion;

                ProductRevisionInputPopup popup = new ProductRevisionInputPopup();
                popup._LotId = lotId;
                popup._ProductRevision = productRevision;
                popup.ShowDialog();

                //if (productRevision != popup._ProductRevision)
                //{
                //    // Scan 한 Lot Card는 이전 Revision의 Lot Card 입니다. 신규 Revision Lot Card 출력 후 Scan 하시기 바랍니다.
                //    //MSGBox.Show(MessageBoxType.Warning, "NotEqualLotCardRevision");
                //    // Scan 한 Lot Card는 이전 Revision의 Lot Card 입니다. 신규 Revision Lot Card를 출력하시겠습니까?
                //    if (MSGBox.Show(MessageBoxType.Question, "NotEqualLotCardRevisionPrint", MessageBoxButtons.YesNo) == DialogResult.Yes)
                //    {
                //        PrintLotCard(row["LOTID"].ToString(), LotCardType.RCChange);
                //    }

                //    return false;
                //}


                //MessageWorker worker = new MessageWorker("SavePrintLotCard");
                //worker.SetBody(new MessageBody()
                //{
                //    { "LotId", row["LOTID"].ToString() }
                //});

                //worker.Execute();

                return true;
            }


            return true;
        }
        #endregion

        #region - PrintRequestConsumableRelease :: 자재불출요청 - 전표 출력 |
        /// <summary>
        /// 자재불출요청 - 전표 출력
        /// </summary>
        /// <param name="requestNo"></param>
        public static void PrintRequestConsumableRelease(string requestNo, Stream stream)
        {
            DataTable dtHeaderInfo2 = SqlExecuter.Query("SelectPrintChitHeaderInfo", "10001", new Dictionary<string, object>() { { "REQUESTNO", requestNo }, { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "PLANTID", UserInfo.Current.Plant } });
            DataTable dtTableInfo2 = SqlExecuter.Query("SelectPrintChitDetailTable", "10002", new Dictionary<string, object>() { { "REQUESTNO", requestNo }, { "PLANTID", UserInfo.Current.Plant } });


            XtraReport[] reports = null;
            XtraReport printReport = new XtraReport();
            DataTable[] tableInfoByPage = null;

            int cnt = dtTableInfo2.Rows.Count;
            int rest = cnt % 9;
            int val = cnt / 9;

            reports = val.Equals(0) ? new XtraReport[1] : (rest.Equals(0) ? new XtraReport[val] : new XtraReport[val + 1]);
            tableInfoByPage = val.Equals(0) ? new DataTable[1] : (rest.Equals(0) ? new DataTable[val] : new DataTable[val + 1]);
            int virtualVal = val.Equals(0) ? 1 : (rest.Equals(0) ? val : val + 1);

            for (int i = 0; i < virtualVal; i++)
            {
                reports[i] = XtraReport.FromStream(stream);
                tableInfoByPage[i] = dtTableInfo2.Clone();

                if ((i < val) || (virtualVal.Equals(1) && rest.Equals(0)))
                {
                    tableInfoByPage[i] = TableDataDivide(dtTableInfo2, tableInfoByPage[i], i * 9, 9);
                }
                else if (!val.Equals(virtualVal))
                {
                    tableInfoByPage[i] = TableDataDivide(dtTableInfo2, tableInfoByPage[i], i * 9, rest);
                }

                //빈 row 채우기
                if (rest > 0 && (i + 1) == virtualVal)
                {
                    for (int k = 0; k < (9 - rest); k++)
                    {
                        DataRow newRow = tableInfoByPage[i].NewRow();
                        newRow["REQUESTNO"] = requestNo;

                        tableInfoByPage[i].Rows.Add(newRow);
                    }
                }

                DataTable headerInfoByPage = dtHeaderInfo2.Copy();

                DataSet dsReport = new DataSet();
                headerInfoByPage.TableName = "HeaderInfo";
                tableInfoByPage[i].TableName = "TableInfo";
                dsReport.Tables.Add(headerInfoByPage);
                dsReport.Tables.Add(tableInfoByPage[i]);

                DataRelation relation = new DataRelation("RelationRequestNo", headerInfoByPage.Columns["REQUESTNO"], tableInfoByPage[i].Columns["REQUESTNO"]);
                dsReport.Relations.Add(relation);

                printReport.Pages.Add(SetReortPageData(reports[i], dsReport, "RelationRequestNo").FirstOrDefault());

            }


            printReport.ShowPreviewDialog();
        }
        #endregion

        #region - SetReortPageData :: 자재불출요청 page별 데이터 바인드 |
        /// <summary>
        /// 자재불출요청 page별 데이터 바인드
        /// </summary>
        /// <param name="report"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        private static PageList SetReortPageData(XtraReport report, DataSet ds, string relationNo, bool isMulti = true)
        {
            report.DataSource = ds;
            report.DataMember = "HeaderInfo";

            //회수용			
            DetailReportBand detailReport = report.Bands["DetailReport"] as DetailReportBand;
            detailReport.DataSource = ds;
            detailReport.DataMember = relationNo;

            Band groupHeader = detailReport.Bands["GroupHeader1"];
            SetReportControlDataBinding(groupHeader.Controls, ds, "");

            Band detailBand = detailReport.Bands["Detail1"];
            SetReportControlDataBinding(detailBand.Controls, ds, relationNo);

            setLabelLaungage(detailReport, "GroupHeader1");

			if(isMulti)
			{
				//불출용
				DetailReportBand detailReport1 = report.Bands["DetailReport1"] as DetailReportBand;
				detailReport1.DataSource = ds;
				detailReport1.DataMember = relationNo;

				Band groupHeader1 = detailReport1.Bands["GroupHeader2"];
				SetReportControlDataBinding(groupHeader1.Controls, ds, "");

				Band detailBand1 = detailReport1.Bands["Detail2"];
				SetReportControlDataBinding(detailBand1.Controls, ds, relationNo);

				setLabelLaungage(detailReport1, "GroupHeader2");
			}
           

            report.CreateDocument();

            return report.Pages;
        }
        #endregion

        #region - PrintBoxPacking :: 포장 인계 - 전표 출력 |
        /// <summary>
        /// 포장 인계 - 전표 출력
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="transitInfo"></param>
        public static void PrintBoxPacking(Stream stream, DataTable transitInfo)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("BOXNO", Format.GetString(transitInfo.Rows[0]["BOXNO"]));
            //param.Add("LOTID", Format.GetString(transitInfo.Rows[0]["LOTID"]));
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable dtDoc = SqlExecuter.Query("GetBoxPackingDocumentNo", "10001", param);
            DataTable dtDocumentInfo = dtDoc.Clone();

            DataRow row = dtDocumentInfo.NewRow();
            row["DOCUMENTNO"] = dtDoc.Rows[0]["DOCUMENTNO"];
            row["PRODUCTDEFID"] = dtDoc.Rows[0]["PRODUCTDEFID"];
            row["PRODUCTDEFNAME"] = dtDoc.Rows[0]["PRODUCTDEFNAME"];
            row["UNIT"] = dtDoc.Rows[0]["UNIT"];
            row["SENDTIME"] = dtDoc.Rows[0]["SENDTIME"];
            row["AREAID"] = dtDoc.Rows[0]["AREAID"];
            row["AREANAME"] = dtDoc.Rows[0]["AREANAME"];
            row["NOWDATE"] = dtDoc.Rows[0]["NOWDATE"];
            dtDocumentInfo.Rows.Add(row);


            dtDocumentInfo.Columns.Add("TOTALSENDQTY", typeof(decimal));//인계수량
            dtDocumentInfo.Columns.Add("TOTALPAGE", typeof(decimal));//총 페이지
            dtDocumentInfo.Columns.Add("CURRENTPAGE", typeof(decimal));//현재 페이지
            decimal qty = transitInfo.AsEnumerable().Sum(x => Format.GetDecimal(x["PCSQTY"]));
            dtDocumentInfo.Rows[0]["TOTALSENDQTY"] = qty;


            XtraReport[] reports = null;
            XtraReport printReport = new XtraReport();
            DataTable[] tableInfoByPage = null;

            int cnt = transitInfo.Rows.Count;
            int rest = cnt % 9;
            int val = cnt / 9;

            reports = val.Equals(0) ? new XtraReport[1] : (rest.Equals(0) ? new XtraReport[val] : new XtraReport[val + 1]);
            tableInfoByPage = val.Equals(0) ? new DataTable[1] : (rest.Equals(0) ? new DataTable[val] : new DataTable[val + 1]);
            int virtualVal = val.Equals(0) ? 1 : (rest.Equals(0) ? val : val + 1);

            for (int i = 0; i < virtualVal; i++)
            {
                //페이지 정보
                dtDocumentInfo.Rows[0]["TOTALPAGE"] = virtualVal;
                dtDocumentInfo.Rows[0]["CURRENTPAGE"] = i + 1;

                reports[i] = XtraReport.FromStream(stream);
                tableInfoByPage[i] = transitInfo.Clone();

                if ((i < val) || (virtualVal.Equals(1) && rest.Equals(0)))
                {
                    tableInfoByPage[i] = TableDataDivide(transitInfo, tableInfoByPage[i], i * 9, 9);
                }
                else if (!val.Equals(virtualVal))
                {
                    tableInfoByPage[i] = TableDataDivide(transitInfo, tableInfoByPage[i], i * 9, rest);
                }


                //빈 row 채우기
                if (rest > 0 && (i + 1) == virtualVal)
                {
                    for (int k = 0; k < (9 - rest); k++)
                    {
                        DataRow newRow = tableInfoByPage[i].NewRow();
                        newRow["DOCUMENTNO"] = dtDocumentInfo.Rows.Cast<DataRow>().FirstOrDefault()["DOCUMENTNO"];

                        tableInfoByPage[i].Rows.Add(newRow);
                    }
                }

                DataTable headerInfoByPage = dtDocumentInfo.Copy();

                DataSet dsReport = new DataSet();
                headerInfoByPage.TableName = "HeaderInfo";
                tableInfoByPage[i].TableName = "TableInfo";
                dsReport.Tables.Add(headerInfoByPage);
                dsReport.Tables.Add(tableInfoByPage[i]);

                DataRelation relation = new DataRelation("RelationDocumentNo", headerInfoByPage.Columns["DOCUMENTNO"], tableInfoByPage[i].Columns["DOCUMENTNO"]);
                dsReport.Relations.Add(relation);

                printReport.Pages.Add(SetReortPageData(reports[i], dsReport, "RelationDocumentNo").FirstOrDefault());
            }

            printReport.ShowPreviewDialog();
        }
        #endregion

        #region - PrintWipPhysicalCountList :: 재공실사관리 - 재공실사표 출력 |
        /// <summary>
        /// 재공실사관리 - 재공실사표 출력
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="dt"></param>
        public static void PrintWipPhysicalCountList(Stream stream, DataTable dt)
        {
            //각 작업장 별 실사 LIST 출력
            var query = from dataRow in dt.AsEnumerable()
                        orderby dataRow["AREAID"]
                        select new
                        {
                            AREAID = dataRow["AREAID"],
                            AREANAME = dataRow["AREANAME"],
                            PRODUCTDEFID = dataRow["PRODUCTDEFID"],
                            PRODUCTDEFNAME = dataRow["PRODUCTDEFNAME"],
                            LOTID = dataRow["LOTID"],
                            PROCESSSEGMENTNAME = dataRow["PROCESSSEGMENTNAME"],
                            PANELQTY = dataRow["PANELQTY"],
                            QTY = dataRow["QTY"],
                            EAQTY = dataRow["EAQTY"],
                            REALWIPPCSQTY = dataRow["REALWIPPCSQTY"],
                            REALWIPLOSSQTY = dataRow["REALWIPLOSSQTY"],
                            REALWIPETCQTY = dataRow["REALWIPETCQTY"],
                            REASONCOMMENT = dataRow["REASONCOMMENT"]
                        };

            //디테일 TOTAL 정보
            DataTable detailInfo = new DataTable();
            detailInfo.Columns.Add("SEQ", typeof(int));
            detailInfo.Columns.Add("AREAID", typeof(string));
            detailInfo.Columns.Add("AREANAME", typeof(string));
            detailInfo.Columns.Add("PRODUCTDEFID", typeof(string));
            detailInfo.Columns.Add("PRODUCTDEFNAME", typeof(string));
            detailInfo.Columns.Add("LOTID", typeof(string));
            detailInfo.Columns.Add("PROCESSSEGMENTNAME", typeof(string));
            detailInfo.Columns.Add("PANELQTY", typeof(decimal));
            detailInfo.Columns.Add("QTY", typeof(decimal));
            detailInfo.Columns.Add("EAQTY", typeof(decimal));
            detailInfo.Columns.Add("REALWIPPCSQTY", typeof(decimal));
            detailInfo.Columns.Add("REALWIPLOSSQTY", typeof(decimal));
            detailInfo.Columns.Add("REALWIPETCQTY", typeof(decimal));
            detailInfo.Columns.Add("REASONCOMMENT", typeof(string));

            var dl = query.ToList();
            string prevArea = string.Empty;
            int cnt = 0;
            for (int i = 0; i < dl.Count; i++)
            {
                if (!prevArea.Equals(dl[i].AREAID.ToString()))
                {
                    prevArea = dl[i].AREAID.ToString();
                    cnt = 1;
                }

                detailInfo.Rows.Add(cnt, dl[i].AREAID.ToString(), dl[i].AREANAME.ToString(), dl[i].PRODUCTDEFID.ToString(), dl[i].PRODUCTDEFNAME.ToString(), dl[i].LOTID.ToString(),
                    dl[i].PROCESSSEGMENTNAME.ToString(), Format.GetDecimal(dl[i].PANELQTY, 0), Format.GetDecimal(dl[i].QTY, 0), Format.GetDecimal(dl[i].EAQTY, 0),
                    dl[i].REALWIPPCSQTY, dl[i].REALWIPLOSSQTY, dl[i].REALWIPETCQTY, dl[i].REASONCOMMENT);

                cnt++;
            }
            
            DataTable dtHeader = new DataTable();
            dtHeader.Columns.Add("AREAID", typeof(string));
            dtHeader.Columns.Add("AREANAME", typeof(string));

            var areaList = from areaRow in dt.AsEnumerable()
                           orderby areaRow["AREAID"]
                           select new
                           {
                               AreaId = Format.GetString(areaRow["AREAID"]),
                               AreaName = Format.GetString(areaRow["AREANAME"])
                           };

            areaList.Distinct().ForEach(ar =>
            {
                dtHeader.Rows.Add(ar.AreaId, ar.AreaName);
            });

            dtHeader.AcceptChanges();

            DataTable dtDetail = detailInfo.AsEnumerable().CopyToDataTable();

            DataSet dsReport = new DataSet();
            dsReport.Tables.Add(dtHeader);
            dsReport.Tables.Add(dtDetail);

            DataRelation relation = new DataRelation("RelationAreaId", dtHeader.Columns["AREAID"], dtDetail.Columns["AREAID"]);
            dsReport.Relations.Add(relation);

            XtraReport report = XtraReport.FromStream(stream);
            report.DataSource = dsReport;
            report.DataMember = "Table1";

            Band band = report.Bands["Detail"];


            DetailReportBand detailReport = report.Bands["DetailReport"] as DetailReportBand;
            detailReport.DataSource = dsReport;
            detailReport.DataMember = "RelationAreaId";

            Band groupHeader = detailReport.Bands["GroupHeader1"];
            SetReportControlDataBinding(groupHeader.Controls, dsReport, "");

            Band detailBand = detailReport.Bands["Detail1"];
            setLabelLaungage(detailReport);
            SetReportControlDataBinding(detailBand.Controls, dsReport, "RelationAreaId");

            report.ShowPreviewDialog();

            /*
            XtraReport[] reports = null;
            XtraReport printReport = new XtraReport();

            List<string> areaList = detailInfo.AsEnumerable().Select(r => Format.GetString(r["AREAID"])).Distinct().ToList();
            for (int i = 0; i < areaList.Count; i++)
            {
                var wipListByArea = from dataRow in detailInfo.AsEnumerable()
                                    where dataRow["AREAID"].ToString() == areaList[i].ToString()
                                    select dataRow;

                //작업장별 디테일 정보
                DataTable dtDetail = wipListByArea.Cast<DataRow>().CopyToDataTable();

                //헤더 정보
                DataTable dtHeader = new DataTable();
                dtHeader.Columns.Add("AREAID", typeof(string));
                dtHeader.Columns.Add("AREANAME", typeof(string));

                DataRow row = dtHeader.NewRow();
                row["AREAID"] = dtDetail.Rows[0]["AREAID"].ToString();
                row["AREANAME"] = dtDetail.Rows[0]["AREANAME"].ToString();
                dtHeader.Rows.Add(row);

                DataSet dsReport = new DataSet();
                dtHeader.TableName = "HeaderInfo";
                dtDetail.TableName = "TableInfo";
                dsReport.Tables.Add(dtHeader);
                dsReport.Tables.Add(dtDetail);

                DataRelation relation = new DataRelation("RelationAreaId", dtHeader.Columns["AREAID"], dtDetail.Columns["AREAID"]);
                dsReport.Relations.Add(relation);

    //            reports = new XtraReport[areaList.Count];
    //            reports[i] = XtraReport.FromStream(stream);

    //            printReport.Pages.AddRange(SetReortPageData(reports[i], dsReport, "RelationAreaId", false));
                
				//report.DataSource = dsReport;
    //            report.DataMember = "HeaderInfo";

    //            DetailReportBand detailReport = report.Bands["DetailReport"] as DetailReportBand;
    //            detailReport.DataSource = dsReport;
    //            detailReport.DataMember = "RelationAreaId";

    //            Band groupHeader = detailReport.Bands["GroupHeader1"];
    //            SetReportControlDataBinding(groupHeader.Controls, dsReport, "");

    //            Band detailBand = detailReport.Bands["Detail1"];
    //            setLabelLaungage(detailReport);
    //            SetReportControlDataBinding(detailBand.Controls, dsReport, "RelationAreaId");

    //            setLabelLaungage(detailReport, "GroupHeader1");

    //            //report.Print();
    //            report.ShowPreviewDialog();
            }

            printReport.ShowPreviewDialog();
            */
        }
        #endregion

        #endregion

        #region ◆ 품질 관련 공통 :::

        #region - SendEmail :: 검사 결과 등록 후 SendEmail actionType 있을경우 이메일 전송 팝업  |

        /// <summary>
        /// 서버에서 반환받은 값으로 Email popup 보여주는 함수
        /// (isSendEmail == true 일때 호출)
        /// 강유라 2019-12-05 이메일 함수 추가 
        /// </summary>
        /// <param name="contents"></param>
         //*2019-12-26 xml로 수정중
        public static void ShowSendEmailPopupDataTable(DataTable dt)
        {
            SendInspectionMailPopup mailPopup = new SendInspectionMailPopup(dt);
            mailPopup.TopMost = true;
            mailPopup.StartPosition = FormStartPosition.CenterParent;
            mailPopup.setTitleAndContentsDataTable();

            mailPopup.ShowDialog();
        }

        /// <summary>
        /// 서버에서 반환받은 값으로 Email popup 보여주는 함수
        /// (isSendEmail == true 일때 호출)
        /// 유석진 2020-02-18 이메일 함수 추가, 첨부파일 파라미터 추가 
        /// </summary>
        /// <param name="contents"></param>
        /// <param name="file"></param>
        public static void ShowSendEmailPopupDataTable(DataTable dt, DataTable fileDt)
        {
            SendInspectionMailPopup mailPopup = new SendInspectionMailPopup(dt, fileDt);
            mailPopup.TopMost = true;
            mailPopup.StartPosition = FormStartPosition.CenterParent;
            mailPopup.setTitleAndContentsDataTable();

            mailPopup.ShowDialog();
        }

        public static void ShowSendEmailPopup(string title, string contents)
        {
            SendInspectionMailPopup mailPopup = new SendInspectionMailPopup();
            mailPopup.TopMost = true;
            mailPopup.StartPosition = FormStartPosition.CenterParent;
            mailPopup.setTitleAndContents(title, contents);

            mailPopup.ShowDialog();
        }

        private static DataTable CreateDataTableDefault()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("REMARK");
            dt.Columns.Add("USERID");
            dt.Columns.Add("TITLE");
            dt.Columns.Add("INSPECTION");
            dt.Columns.Add("LANGUAGETYPE");

            return dt;
        }
        #endregion

        #region - 약품분석 부적합 발행 이메일 :: 이메일 내용을 parameter로 받아 이메일 내용 구성  |

        /// <summary>
        /// Email 팝업에서 미리 보기 내용을 만드는 함수
        /// </summary>
        /// <returns></returns>
        public static string GetChemicalAbnormalEmailContents(DataRow row)
        {
            string content = "";

            content += "○ " + Language.Get("ANALYSISTYPE") + " : " + Format.GetString(row["ANALYSISTYPE"]) + "\r\n" // 분석종류
                     + "○ " + Language.Get("LARGEPROCESSSEGMENTNAME") + " : " + Format.GetString(row["LARGEPROCESSSEGMENTNAME"]) + "\r\n" // 대공정명
                     + "○ " + Language.Get("AREANAME") + " : " + Format.GetString(row["AREANAME"]) + "\r\n" // 작업장명
                     + "○ " + Language.Get("EQUIPMENTNAME") + " : " + Format.GetString(row["EQUIPMENTNAME"]) + "\r\n" // 설비명
                     + "○ " + Language.Get("CHILDEQUIPMENTNAME") + " : " + Format.GetString(row["CHILDEQUIPMENTNAME"]) + "\r\n" // 설비단명
                     + "○ " + Language.Get("CHEMICALNAME") + " : " + Format.GetString(row["CHEMICALNAME"]) + "\r\n" // 약품명
                     + "○ " + Language.Get("CHEMICALLEVEL") + " : " + Format.GetString(row["CHEMICALLEVEL"]) + "\r\n" // 약품등급
                     + "○ " + Language.Get("DEGREE") + " : " + Format.GetString(row["DEGREE"]) + "\r\n" // 차수
                     + "○ " + Language.Get("ANALYSISVALUE") + " : " + Format.GetString(row["ANALYSISVALUE"]) + "\r\n" // 분석량
                     + "○ " + Language.Get("MANAGEMENTSCOPE") + " : " + Format.GetString(row["MANAGEMENTSCOPE"]) + "\r\n" // 관리범위 
                     + "○ " + Language.Get("SPECSCOPE") + " : " + Format.GetString(row["SPECSCOPE"]) + "\r\n" // 규격범위
                     + "○ " + Language.Get("BREAKTYPE") + " : " + Format.GetString(row["BREAKTYPE"]) + "\r\n"; // 이탈유형

            return content;
        }

        /// <summary>
        /// Email에 전달 할 내용의 dataTable을 만드는 함수
        /// </summary>
        /// <returns></returns>
        public static DataTable CreateChemicalAbnormalEmailDt()
        {
            DataTable sendEmailDt = new DataTable();

            sendEmailDt.Columns.Add("ANALYSISTYPE"); // 분석종류
            sendEmailDt.Columns.Add("LARGEPROCESSSEGMENTNAME"); // 대공정명
            sendEmailDt.Columns.Add("AREAID"); // 작업장ID
            sendEmailDt.Columns.Add("AREANAME"); // 작업장명
            sendEmailDt.Columns.Add("EQUIPMENTNAME"); // 설비명
            sendEmailDt.Columns.Add("CHILDEQUIPMENTNAME"); // 설비단명
            sendEmailDt.Columns.Add("CHEMICALNAME"); // 약품명
            sendEmailDt.Columns.Add("CHEMICALLEVEL"); // 약품등급
            sendEmailDt.Columns.Add("DEGREE"); // 차수
            sendEmailDt.Columns.Add("ANALYSISVALUE"); // 분석량
            sendEmailDt.Columns.Add("MANAGEMENTSCOPE"); // 관리범위
            sendEmailDt.Columns.Add("SPECSCOPE"); // 규격범위
            sendEmailDt.Columns.Add("BREAKTYPE"); // 이탈유형
            sendEmailDt.Columns.Add("REMARK"); // 비고
            sendEmailDt.Columns.Add("USERID"); // 보내는사람
            sendEmailDt.Columns.Add("TITLE"); // 메일타이틀
            sendEmailDt.Columns.Add("INSPECTION"); // 검사종류
            sendEmailDt.Columns.Add("LANGUAGETYPE"); // 언어타입

            return sendEmailDt;
        }

        #endregion

        #region - 약품분석 보정(보충량) 부적합 발행 이메일 :: 이메일 내용을 parameter로 받아 이메일 내용 구성  |

        /// <summary>
        /// Email 팝업에서 미리 보기 내용을 만드는 함수
        /// </summary>
        /// <returns></returns>
        public static string GetChemicalReanalysisAbnormalEmailContents(DataRow row)
        {
            string content = "";

            content += "○ " + Language.Get("ANALYSISTYPE") + " : " + Format.GetString(row["ANALYSISTYPE"]) + "\r\n" // 분석종류
                     + "○ " + Language.Get("LARGEPROCESSSEGMENTNAME") + " : " + Format.GetString(row["LARGEPROCESSSEGMENTNAME"]) + "\r\n" // 대공정명
                     + "○ " + Language.Get("AREANAME") + " : " + Format.GetString(row["AREANAME"]) + "\r\n" // 작업장명
                     + "○ " + Language.Get("EQUIPMENTNAME") + " : " + Format.GetString(row["EQUIPMENTNAME"]) + "\r\n" // 설비명
                     + "○ " + Language.Get("CHILDEQUIPMENTNAME") + " : " + Format.GetString(row["CHILDEQUIPMENTNAME"]) + "\r\n" // 설비단명
                     + "○ " + Language.Get("CHEMICALNAME") + " : " + Format.GetString(row["CHEMICALNAME"]) + "\r\n" // 약품명
                     + "○ " + Language.Get("CHEMICALLEVEL") + " : " + Format.GetString(row["CHEMICALLEVEL"]) + "\r\n" // 약품등급
                     + "○ " + Language.Get("DEGREE") + " : " + Format.GetString(row["DEGREE"]) + "\r\n" // 차수
                     + "○ " + Language.Get("ANALYSISVALUE") + " : " + Format.GetString(row["ANALYSISVALUE"]) + "\r\n" // 분석량
                     + "○ " + Language.Get("MANAGEMENTSCOPE") + " : " + Format.GetString(row["MANAGEMENTSCOPE"]) + "\r\n" // 관리범위 
                     + "○ " + Language.Get("SPECSCOPE") + " : " + Format.GetString(row["SPECSCOPE"]) + "\r\n" // 규격범위
                     + "○ " + Language.Get("ISRESUPPLEMENT") + " : " + Format.GetString(row["ISRESUPPLEMENT"]) + "\r\n" // 재분석여부
                     + "○ " + Language.Get("MESSAGE") + " : " + Format.GetString(row["MESSAGE"]) + "\r\n"; // 전달사항

            return content;
        }

        /// <summary>
        /// Email에 전달 할 내용의 dataTable을 만드는 함수
        /// </summary>
        /// <returns></returns>
        public static DataTable CreateChemicalReanlaysisAbnormalEmailDt()
        {
            DataTable sendEmailDt = new DataTable();

            sendEmailDt.Columns.Add("ANALYSISTYPE"); // 분석종류
            sendEmailDt.Columns.Add("LARGEPROCESSSEGMENTNAME"); // 대공정명
            sendEmailDt.Columns.Add("AREAID"); // 작업장 ID
            sendEmailDt.Columns.Add("AREANAME"); // 작업장명
            sendEmailDt.Columns.Add("EQUIPMENTNAME"); // 설비명
            sendEmailDt.Columns.Add("CHILDEQUIPMENTNAME"); // 설비단명
            sendEmailDt.Columns.Add("CHEMICALNAME"); // 약품명
            sendEmailDt.Columns.Add("CHEMICALLEVEL"); // 약품등급
            sendEmailDt.Columns.Add("DEGREE"); // 차수
            sendEmailDt.Columns.Add("ANALYSISVALUE"); // 분석량
            sendEmailDt.Columns.Add("MANAGEMENTSCOPE"); // 관리범위
            sendEmailDt.Columns.Add("SPECSCOPE"); // 규격범위
            sendEmailDt.Columns.Add("ISRESUPPLEMENT"); // 재분석여부
            sendEmailDt.Columns.Add("MESSAGE"); // 전달사항
            sendEmailDt.Columns.Add("REMARK"); // 비고
            sendEmailDt.Columns.Add("USERID"); // 보내는사람
            sendEmailDt.Columns.Add("TITLE"); // 메일타이틀
            sendEmailDt.Columns.Add("INSPECTION"); // 검사종류
            sendEmailDt.Columns.Add("LANGUAGETYPE"); // 언어타입

            return sendEmailDt;
        }

        #endregion

        #region - 최종검사 부적합 발행 이메일 :: 이메일 내용을 parameter로 받아 이메일 내용 구성  |
        /// <summary>
        /// Email 팝업에서 미리 보기 내용을 만드는 함수
        /// </summary>
        /// <returns></returns>
        public static string GetFinishAbnormalEmailContents(DataRow row)
        {
            string content = "";

            content = string.Format(string.Concat(@"○{0}:{1}", "\r\n",
                                                                   "○{2}:{3}", "\r\n",
                                                                   "○{4}({5}/{6}):{7}({8}/{9})", "\r\n",
                                                                   "○{10}:{11}", "\r\n",
                                                                   "○{12}:{13}", "\r\n"),
                                                     Language.Get("STANDARDSEGMENT"),
                                                     Format.GetString(row["PROCESSSEGMENTNAME"]),
                                                     Language.Get("AREA"),
                                                     Format.GetString(row["AREANAME"]),
                                                     Language.Get("PRODUCTDEFNAME"),
                                                     Language.Get("PRODUCTDEFID"), Language.Get("PRODUCTDEFVERSION"),
                                                     Format.GetString(row["PRODUCTDEFNAME"]),
                                                     Format.GetString(row["PRODUCTDEFID"]), Format.GetString(row["PRODUCTDEFVERSION"]),
                                                     Language.Get("LOTID"), Format.GetString(row["LOTID"]),
                                                     Language.Get("DEFECTNAME"), Format.GetString(row["DEFECTNAME"]));

            return content;
        }

        /// <summary>
        /// Email에 전달 할 내용의 dataTable을 만드는 함수
        /// </summary>
        /// <returns></returns>
        public static DataTable CreateFinishAbnormalEmailDt()
        {
            DataTable sendEmailDt = new DataTable();

            sendEmailDt.Columns.Add("PROCESSSEGMENTNAME");
            sendEmailDt.Columns.Add("AREANAME");
            sendEmailDt.Columns.Add("PRODUCTDEFNAME");
            sendEmailDt.Columns.Add("PRODUCTDEFID");
            sendEmailDt.Columns.Add("PRODUCTDEFVERSION");
            sendEmailDt.Columns.Add("LOTID");
            sendEmailDt.Columns.Add("DEFECTNAME");
            sendEmailDt.Columns.Add("REMARK");
            sendEmailDt.Columns.Add("USERID");
            sendEmailDt.Columns.Add("TITLE");
            sendEmailDt.Columns.Add("INSPECTION");
            sendEmailDt.Columns.Add("LANGUAGETYPE");

            return sendEmailDt;
        }
        #endregion

        #region - 외주입고품(공정) 부적합 발행 이메일 :: 이메일 내용을 parameter로 받아 이메일 내용 구성  |
        /// <summary>
        /// Email 팝업에서 미리 보기 내용을 만드는 함수
        /// </summary>
        /// <returns></returns>
        public static string GetProcessAbnormalEmailContents(DataRow row)
        {
            string content = "";

            content = string.Format(string.Concat(@"○{0}({1}/{2}):{3}({4}/{5})", "\r\n",
                                                                   "○{6}:{7}", "\r\n",
                                                                   "○{8}:{9}({10})", "\r\n",
                                                                   "○{11}:{12}({13})", "\r\n",
                                                                   "○{14}:{15}({16})", "\r\n",
                                                                   "○{17}:{18}", "\r\n",
                                                                   "○{19}:{20}", "\r\n",
                                                                   "○{21}:{22}", "\r\n"),
                                                     Language.Get("PRODUCTDEFNAME"),
                                                     Language.Get("PRODUCTDEFID"), Language.Get("PRODUCTDEFVERSION"),
                                                     Format.GetString(row["PRODUCTDEFNAME"]),
                                                     Format.GetString(row["PRODUCTDEFID"]), Format.GetString(row["PRODUCTDEFVERSION"]),
                                                     Language.Get("LOTID"), Format.GetString(row["LOTID"]),
                                                     Language.Get("TOPPROCESSSEGMENTID"), Format.GetString(row["PROCESSSEGMENTCLASSNAME"]), Format.GetString(row["PROCESSSEGMENTCLASSID"]),
                                                     Language.Get("STANDARDSEGMENT"), Format.GetString(row["PROCESSSEGMENTNAME"]), Format.GetString(row["PROCESSSEGMENTID"]),
                                                     Language.Get("AREA"), Format.GetString(row["AREANAME"]), Format.GetString(row["AREAID"]),
                                                     Language.Get("INSPECTIONRESULT"), Format.GetString(row["INSPECTIONRESULT"]),
                                                     Language.Get("DEFECTNAME"), Format.GetString(row["DEFECTNAME"]),
                                                     Language.Get("MEASUREVALUE"), Format.GetString(row["MEASUREVALUE"]));

            return content;
        }

        /// <summary>
        /// Email에 전달 할 내용의 dataTable을 만드는 함수
        /// </summary>
        /// <returns></returns>
        public static DataTable CreateProcessAbnormalEmailDt()
        {
            DataTable sendEmailDt = new DataTable();

            sendEmailDt.Columns.Add("PRODUCTDEFNAME");
            sendEmailDt.Columns.Add("PRODUCTDEFID");
            sendEmailDt.Columns.Add("PRODUCTDEFVERSION");
            sendEmailDt.Columns.Add("LOTID");
            sendEmailDt.Columns.Add("PROCESSSEGMENTCLASSNAME");
            sendEmailDt.Columns.Add("PROCESSSEGMENTCLASSID");
            sendEmailDt.Columns.Add("PROCESSSEGMENTNAME");
            sendEmailDt.Columns.Add("PROCESSSEGMENTID");
            sendEmailDt.Columns.Add("AREANAME");
            sendEmailDt.Columns.Add("AREAID");
            sendEmailDt.Columns.Add("INSPECTIONRESULT");
            sendEmailDt.Columns.Add("DEFECTNAME");
            sendEmailDt.Columns.Add("MEASUREVALUE");
            sendEmailDt.Columns.Add("REMARK");
            sendEmailDt.Columns.Add("USERID");
            sendEmailDt.Columns.Add("TITLE");
            sendEmailDt.Columns.Add("INSPECTION");
            sendEmailDt.Columns.Add("LANGUAGETYPE");

            return sendEmailDt;
        }
        #endregion

        #region - 품질규격 부적합 발행 이메일 :: 이메일 내용을 parameter로 받아 이메일 내용 구성  |

        /// <summary>
        /// Email 팝업에서 미리 보기 내용을 만드는 함수
        /// </summary>
        /// <returns></returns>
        public static string GetMeasureValueRegistrationContents(DataRow dr)
        {
            StringBuilder contents = new StringBuilder();

            contents.Append(string.Format("○ {0}({1}/{2}):", Language.Get("PRODUCTDEFNAME"), Language.Get("PRODUCTDEFID"), Language.Get("PRODUCTDEFVERSION")));
            contents.Append(string.Concat(string.Format("{0}({1}/{2})", dr["PRODUCTDEFNAME"], dr["PRODUCTDEFID"], dr["PRODUCTDEFVERSION"]), "\r\n"));
            contents.Append(string.Concat(string.Format("○ {0}:{1}", Language.Get("LOT"), dr["RESOURCEID"]), "\r\n"));
            contents.Append(string.Concat(string.Format("○ {0}:{1}", Language.Get("MEASUREDATETIME"), dr["MEASUREDATETIME"]), "\r\n"));
            contents.Append(string.Concat(string.Format("○ {0}:{1}", Language.Get("MEASUREITEM"), dr["INSPITEMID"]), "\r\n"));
            contents.Append(string.Concat(string.Format("○ {0}:{1}", Language.Get("MEASURER"), dr["MEASURER"]), "\r\n"));
            contents.Append(string.Concat(string.Format("○ {0}:{1}", Language.Get("SITE"), dr["PLANTID"]), "\r\n"));
            contents.Append(string.Concat(string.Format("○ {0}:{1}", Language.Get("PROCESSSEGMENTEXTLIST"), dr["PROCESSSEGMENTID"]), "\r\n"));
            contents.Append(string.Concat(string.Format("○ {0}:{1}", Language.Get("AREA"), dr["AREAID"]), "\r\n"));
            contents.Append(string.Concat(string.Format("○ {0}:{1}", Language.Get("WORKSTARTEQUIPMENT"), dr["EQUIPMENTID"]), "\r\n"));
            contents.Append(string.Concat(string.Format("○ {0}:{1}", Language.Get("RANGE"), dr["SPECRANGE"]), "\r\n"));
            contents.Append(string.Concat(string.Format("○ {0}:{1}", Language.Get("MEASURVALUE"), dr["MEASUREVALUELIST"]), "\r\n"));
            contents.Append(string.Concat("\r\n", "\r\n"));

            return contents.ToString();
        }

        /// <summary>
        /// Email에 전달 할 내용의 dataTable을 만드는 함수
        /// </summary>
        /// <returns></returns>
        public static DataTable CreateMeasureValueRegistrationDt()
        {
            DataTable dt = CreateDataTableDefault();

            dt.Columns.Add("PRODUCTDEFNAME");
            dt.Columns.Add("PRODUCTDEFID");
            dt.Columns.Add("PRODUCTDEFVERSION");
            dt.Columns.Add("RESOURCEID");
            dt.Columns.Add("MEASUREDATETIME");
            dt.Columns.Add("INSPITEMID");
            dt.Columns.Add("MEASURER");
            dt.Columns.Add("PLANTID");
            dt.Columns.Add("PROCESSSEGMENTID");
            dt.Columns.Add("AREAID");
            dt.Columns.Add("EQUIPMENTID");
            dt.Columns.Add("SPECRANGE");
            dt.Columns.Add("MEASUREVALUELIST");

            return dt;
        }

        #endregion

        #region - 원자재/가공품 부적합 발행 이메일 :: 이메일 내용을 parameter로 받아 이메일 내용 구성  |
        /// <summary>
        /// Email 팝업에서 미리 보기 내용을 만드는 함수
        /// </summary>
        /// <returns></returns>
        public static string GetRawAbnormalEmailContents(DataRow row)
        {
            string content = "";

            content = string.Format(string.Concat(@"○{0}:{1}", "\r\n",
                                                                   "○{2}({3}/{4}):{5}({6}/{7})", "\r\n",
                                                                   "○{8}:{9}", "\r\n",
                                                                   "○{10}:{11}", "\r\n",
                                                                   "○{12}:{13}", "\r\n",
                                                                   "○{14}:{15}", "\r\n",
                                                                   "○{16}:{17}", "\r\n",
                                                                   "○{18}:{19}", "\r\n"),
                                                     Language.Get("CONVENDORNAME"),
                                                     Format.GetString(row["VENDORNAME"]),
                                                     Language.Get("MATERIALNAME"),
                                                     Language.Get("MATERIALDEF"), Language.Get("COMPONENTITEMVERSION"),
                                                     Format.GetString(row["CONSUMABLEDEFNAME"]),
                                                     Format.GetString(row["CONSUMABLEDEFID"]), Format.GetString(row["CONSUMABLEDEFVERSION"]),
                                                     Language.Get("MATERIALLOT"),
                                                     Format.GetString(row["MATERIALLOT"]),
                                                     Language.Get("ENTRYEXITDATE"), Format.GetString(row["ENTRYEXITDATE"]),
                                                     Language.Get("QTY"), Format.GetString(row["QTY"]),
                                                     Language.Get("INSPECTIONRESULT"), Format.GetString(row["INSPECTIONRESULT"]),
                                                     Language.Get("DEFECTNAME"), Format.GetString(row["DEFECTNAME"]),
                                                     Language.Get("MEASUREVALUE"), Format.GetString(row["MEASUREVALUE"]));

            return content;
        }

        /// <summary>
        /// Email에 전달 할 내용의 dataTable을 만드는 함수
        /// </summary>
        /// <returns></returns>
        public static DataTable CreateRawAbnormalEmailDt()
        {
            DataTable sendEmailDt = new DataTable();

            sendEmailDt.Columns.Add("VENDORNAME");
            sendEmailDt.Columns.Add("CONSUMABLEDEFNAME");
            sendEmailDt.Columns.Add("CONSUMABLEDEFID");
            sendEmailDt.Columns.Add("CONSUMABLEDEFVERSION");
            sendEmailDt.Columns.Add("MATERIALLOT");
            sendEmailDt.Columns.Add("QTY");
            sendEmailDt.Columns.Add("ENTRYEXITDATE");
            sendEmailDt.Columns.Add("DEFECTNAME");
            sendEmailDt.Columns.Add("MEASUREVALUE");
            sendEmailDt.Columns.Add("INSPECTIONRESULT");
            sendEmailDt.Columns.Add("REMARK");
            sendEmailDt.Columns.Add("USERID");
            sendEmailDt.Columns.Add("TITLE");
            sendEmailDt.Columns.Add("INSPECTION");
            sendEmailDt.Columns.Add("LANGUAGETYPE");

            return sendEmailDt;
        }
        #endregion

        #region - 신뢰성(정기)/(정기외) 부적합 발행 이메일 :: 이메일 내용을 parameter로 받아 이메일 내용 구성  |

        /// <summary>
        /// Email 팝업에서 미리 보기 내용을 만드는 함수-신뢰성(정기)
        /// </summary>
        /// <returns></returns>
        public static string GetReliabilityRegularEmailContents(DataRow row)
        {
            string content = "";

            content = string.Format(string.Concat(@"○ {0}({1}/{2}):{3}({4}/{5})", "\r\n",
                                                                   "○{6}:{7}", "\r\n",
                                                                   "○{8}:{9}({10})", "\r\n",
                                                                   "○{11}:{12}({13})", "\r\n",
                                                                   "○{14}:{15}", "\r\n",
                                                                   "○{16}:{17}", "\r\n",
                                                                   "○{18}:{19}", "\r\n"),
                                                     Language.Get("PRODUCTDEFNAME"),
                                                     Language.Get("PRODUCTDEFID"), Language.Get("PRODUCTDEFVERSION"),
                                                     Format.GetString(row["PRODUCTDEFNAME"]),
                                                     Format.GetString(row["PRODUCTDEFID"]), Format.GetString(row["PRODUCTDEFVERSION"]),
                                                     Language.Get("LOTID"), Format.GetString(row["LOTID"]),
                                                     Language.Get("STANDARDSEGMENT"), Format.GetString(row["PROCESSSEGMENTNAME"]), Format.GetString(row["PROCESSSEGMENTID"]),
                                                     Language.Get("AREA"), Format.GetString(row["AREANAME"]), Format.GetString(row["AREAID"]),
                                                     Language.Get("EQUIPMENTUNIT"), Format.GetString(row["EQUIPMENTLIST"]),
                                                     Language.Get("INSPECTIONRESULT"), Format.GetString(row["INSPECTIONRESULT"]),
                                                     Language.Get("DEFECTNAME"), Format.GetString(row["DEFECTNAME"]));

            return content;
        }

        /// <summary>
        /// Email 팝업에서 미리 보기 내용을 만드는 함수-신뢰성(정기)
        /// </summary>
        /// <returns></returns>
        public static DataTable CreateReliabilityRegularEmailDt()
        {
            DataTable sendEmailDt = new DataTable();

            sendEmailDt.Columns.Add("PRODUCTDEFNAME");
            sendEmailDt.Columns.Add("PRODUCTDEFID");
            sendEmailDt.Columns.Add("PRODUCTDEFVERSION");
            sendEmailDt.Columns.Add("LOTID");
            sendEmailDt.Columns.Add("PROCESSSEGMENTID");
            sendEmailDt.Columns.Add("PROCESSSEGMENTNAME");
            sendEmailDt.Columns.Add("AREAID");
            sendEmailDt.Columns.Add("AREANAME");
            sendEmailDt.Columns.Add("EQUIPMENTLIST");
            sendEmailDt.Columns.Add("INSPECTIONRESULT");
            sendEmailDt.Columns.Add("DEFECTNAME");
            sendEmailDt.Columns.Add("REMARK");
            sendEmailDt.Columns.Add("USERID");
            sendEmailDt.Columns.Add("TITLE");
            sendEmailDt.Columns.Add("INSPECTION");
            sendEmailDt.Columns.Add("LANGUAGETYPE");

            return sendEmailDt;
        }

        /// <summary>
        /// Email 팝업에서 미리 보기 내용을 만드는 함수-신뢰성(정기외)
        /// </summary>
        /// <returns></returns>
        public static string GetReliabilityNonRegularEmailContents(DataRow drLot)
        {
            string content = "";

            StringBuilder _sbMailContents = new StringBuilder();
            _sbMailContents.Append(string.Format("○ {0}({1}/{2}):", Language.Get("PRODUCTDEFNAME"), Language.Get("PRODUCTDEFID"), Language.Get("PRODUCTDEFVERSION")));
            _sbMailContents.AppendLine(string.Format("{0}({1}/{2})", drLot["PRODUCTDEFID"].ToString(), drLot["PRODUCTDEFNAME"].ToString(), drLot["PRODUCTDEFVERSION"].ToString()));
            _sbMailContents.Append(string.Format("○ {0}:", Language.Get("LOTID")));
            _sbMailContents.AppendLine(string.Format("{0}", drLot["LOTID"].ToString()));

            _sbMailContents.Append(string.Format("○ {0}:", Language.Get("STANDARDSEGMENT")));//표준공정
            _sbMailContents.AppendLine(string.Format("{0}({1})", drLot["PROCESSSEGMENTNAME"].ToString(),drLot["PROCESSSEGMENTID"].ToString()));
            _sbMailContents.Append(string.Format("○ {0}:", Language.Get("AREA")));//작업장
            _sbMailContents.AppendLine(string.Format("{0}({1})", drLot["AREANAME"].ToString(), drLot["AREAID"].ToString()));
            _sbMailContents.Append(string.Format("○ {0}:", Language.Get("EQUIPMENTUNIT")));//설비호기
            _sbMailContents.AppendLine(string.Format("{0}", drLot["EQUIPMENTLIST"].ToString()));
            _sbMailContents.Append(string.Format("○ {0}:", Language.Get("INSPECTIONRESULT")));//판정결과
            _sbMailContents.AppendLine(string.Format("{0}", drLot["INSPECTIONRESULT"].ToString()));
            _sbMailContents.Append(string.Format("○ {0}:", Language.Get("DEFECTNAME")));//불량명
            _sbMailContents.AppendLine(string.Format("{0}", drLot["DEFECTNAME"].ToString()));

            if(_sbMailContents.ToString().Length > 0)
                content = _sbMailContents.ToString();

            return content;
        }

        /// <summary>
        /// Email 팝업에서 미리 보기 내용을 만드는 함수-신뢰성(정기외)
        /// </summary>
        /// <returns></returns>
        public static DataTable CreateReliabilityNonRegularEmailDt()
        {
            DataTable sendEmailDt = new DataTable();

            sendEmailDt.Columns.Add("PRODUCTDEFNAME");
            sendEmailDt.Columns.Add("PRODUCTDEFID");
            sendEmailDt.Columns.Add("PRODUCTDEFVERSION");
            sendEmailDt.Columns.Add("LOTID");
            sendEmailDt.Columns.Add("PROCESSSEGMENTID");
            sendEmailDt.Columns.Add("PROCESSSEGMENTNAME");
            sendEmailDt.Columns.Add("AREANAME");
            sendEmailDt.Columns.Add("AREAID");
            sendEmailDt.Columns.Add("EQUIPMENTLIST");
            sendEmailDt.Columns.Add("INSPECTIONRESULT");
            sendEmailDt.Columns.Add("DEFECTNAME");
            sendEmailDt.Columns.Add("REMARK");
            sendEmailDt.Columns.Add("USERID");
            sendEmailDt.Columns.Add("TITLE");
            sendEmailDt.Columns.Add("INSPECTION");
            sendEmailDt.Columns.Add("LANGUAGETYPE");

            return sendEmailDt;
        }

        /// <summary>
        /// Email 팝업에서 미리 보기 내용을 만드는 함수-신뢰성(BBT)
        /// </summary>
        /// <returns></returns>
        public static string GetReliabilityBBTEmailContents(DataRow row)
        {
            string content = "";

            content = string.Format(string.Concat(@"○ {0}({1}/{2}):{3}({4}/{5})", "\r\n",
                                                                   "○{6}:{7}", "\r\n",
                                                                   "○{8}:{9}({10})", "\r\n",
                                                                   "○{11}:{12}({13})", "\r\n",
                                                                   "○{14}:{15}", "\r\n",
                                                                   "○{16}:{17}", "\r\n",
                                                                   "○{18}:{19}", "\r\n"),
                                                     Language.Get("PRODUCTDEFNAME"),
                                                     Language.Get("PRODUCTDEFID"), Language.Get("PRODUCTDEFVERSION"),
                                                     Format.GetString(row["PRODUCTDEFNAME"]),
                                                     Format.GetString(row["PRODUCTDEFID"]), Format.GetString(row["PRODUCTDEFVERSION"]),
                                                     Language.Get("LOTID"), Format.GetString(row["LOTID"]),
                                                     Language.Get("STANDARDSEGMENT"), Format.GetString(row["PROCESSSEGMENTNAME"]), Format.GetString(row["PROCESSSEGMENTID"]),
                                                     Language.Get("AREA"), Format.GetString(row["AREANAME"]), Format.GetString(row["AREAID"]),
                                                     Language.Get("EQUIPMENTUNIT"), Format.GetString(row["EQUIPMENTLIST"]),
                                                     Language.Get("INSPECTIONRESULT"), Format.GetString(row["INSPECTIONRESULT"]),
                                                     Language.Get("DEFECTNAME"), Format.GetString(row["DEFECTNAME"]));

            return content;
        }

        /// <summary>
        /// Email 팝업에서 미리 보기 내용을 만드는 함수-신뢰성(BBT)
        /// </summary>
        /// <returns></returns>
        public static DataTable CreateReliabilityBBTEmailDt()
        {
            DataTable sendEmailDt = new DataTable();

            sendEmailDt.Columns.Add("PRODUCTDEFNAME");
            sendEmailDt.Columns.Add("PRODUCTDEFID");
            sendEmailDt.Columns.Add("PRODUCTDEFVERSION");
            sendEmailDt.Columns.Add("LOTID");
            sendEmailDt.Columns.Add("PROCESSSEGMENTID");
            sendEmailDt.Columns.Add("PROCESSSEGMENTNAME");
            sendEmailDt.Columns.Add("AREAID");
            sendEmailDt.Columns.Add("AREANAME");
            sendEmailDt.Columns.Add("EQUIPMENTLIST");
            sendEmailDt.Columns.Add("INSPECTIONRESULT");
            sendEmailDt.Columns.Add("DEFECTNAME");
            sendEmailDt.Columns.Add("REMARK");
            sendEmailDt.Columns.Add("USERID");
            sendEmailDt.Columns.Add("TITLE");
            sendEmailDt.Columns.Add("INSPECTION");
            sendEmailDt.Columns.Add("LANGUAGETYPE");

            return sendEmailDt;
        }
        #endregion

        #region - 입고검사/자주검사 부적합 발행 이메일 :: 이메일 내용을 parameter로 받아 이메일 내용 구성  |
        /// <summary>
        /// Email 팝업에서 미리 보기 내용을 만드는 함수
        /// </summary>
        /// <returns></returns>
        public static string GetSelfTakeShipAbnormalEmailContents(DataRow row)
        {
            string content = "";

            content = string.Format(string.Concat(@"○{0}:{1}({2})", "\r\n",
                                                                   "○{3}:{4}({5})", "\r\n",
                                                                   "○{6}({7}/{8}):{9}({10}/{11})", "\r\n",
                                                                   "○{12}:{13}", "\r\n",
                                                                   "○{14}:{15}", "\r\n"),
                                                     Language.Get("PROCESSSEGMENT"), Format.GetString(row["PROCESSSEGMENTNAME"]), Format.GetString(row["PROCESSSEGMENTID"]),
                                                     Language.Get("AREA"), Format.GetString(row["AREANAME"]), Format.GetString(row["AREAID"]),
                                                     Language.Get("PRODUCTDEFNAME"),
                                                     Language.Get("PRODUCTDEFID"), Language.Get("PRODUCTDEFVERSION"),
                                                     Format.GetString(row["PRODUCTDEFNAME"]),
                                                     Format.GetString(row["PRODUCTDEFID"]), Format.GetString(row["PRODUCTDEFVERSION"]),
                                                     Language.Get("LOTID"), Format.GetString(row["LOTID"]),
                                                     Language.Get("DEFECTNAME"), Format.GetString(row["DEFECTCODENAME"]));
                                                     //,Language.Get("ISLOCKING"), Format.GetString(row["ISLOCKING"]));

            return content;
        }
        /// <summary>
        /// Email에 전달 할 내용의 dataTable을 만드는 함수
        /// </summary>
        /// <returns></returns>
        public static DataTable CreateSelfTakeShipAbnormalEmailDt()
        {
            DataTable sendEmailDt = new DataTable();

            sendEmailDt.Columns.Add("PROCESSSEGMENTNAME");
            sendEmailDt.Columns.Add("PROCESSSEGMENTID");
            sendEmailDt.Columns.Add("AREANAME");
            sendEmailDt.Columns.Add("AREAID");
            sendEmailDt.Columns.Add("PRODUCTDEFNAME");
            sendEmailDt.Columns.Add("PRODUCTDEFID");
            sendEmailDt.Columns.Add("PRODUCTDEFVERSION");
            sendEmailDt.Columns.Add("LOTID");
            sendEmailDt.Columns.Add("DEFECTCODENAME");
            sendEmailDt.Columns.Add("ISLOCKING");
            sendEmailDt.Columns.Add("REMARK");
            sendEmailDt.Columns.Add("USERID");
            sendEmailDt.Columns.Add("TITLE");
            sendEmailDt.Columns.Add("INSPECTION");
            sendEmailDt.Columns.Add("LANGUAGETYPE");

            return sendEmailDt;
        }
        #endregion

        #region - 출하검사 부적합 발행 이메일 :: 이메일 내용을 parameter로 받아 이메일 내용 구성  |
        /// <summary>
        /// Email 팝업에서 미리 보기 내용을 만드는 함수
        /// </summary>
        /// <returns></returns>
        public static string GetShipmentAbnormalEmailContents(DataRow row)
        {
            string content = "";

            content = string.Format(string.Concat(@"○{0}({1}/{2}):{3}({4}/{5})", "\r\n",
                                                                    "○{6}:{7}", "\r\n",
                                                                    "○{8}:{9}", "\r\n",
                                                                    "○{10}:{11}", "\r\n",
                                                                    "○{12}:{13}", "\r\n",
                                                                    "○{14}:{15}", "\r\n"),
                                                        Language.Get("PRODUCTDEFNAME"),
                                                        Language.Get("PRODUCTDEFID"), Language.Get("PRODUCTDEFVERSION"),
                                                        Format.GetString(row["PRODUCTDEFNAME"]),
                                                        Format.GetString(row["PRODUCTDEFID"]), Format.GetString(row["PRODUCTDEFVERSION"]),
                                                        Language.Get("LOTID"), Format.GetString(row["LOTID"]),
                                                        Language.Get("AREA"), Format.GetString(row["AREANAME"]),
                                                        Language.Get("COMPANYCLIENT"), Format.GetString(row["CUSTOMERNAME"]),
                                                        Language.Get("WEEKINFO"), Format.GetString(row["WEEK"]),
                                                        Language.Get("DEFECTNAME"), row["DEFECTNAME"]);

            return content;
        }
        /// <summary>
        /// Email에 전달 할 내용의 dataTable을 만드는 함수
        /// </summary>
        /// <returns></returns>
        public static DataTable CreateShipmentAbnormalEmailDt()
        {
            DataTable sendEmailDt = new DataTable();

            sendEmailDt.Columns.Add("PRODUCTDEFNAME");
            sendEmailDt.Columns.Add("PRODUCTDEFID");
            sendEmailDt.Columns.Add("PRODUCTDEFVERSION");
            sendEmailDt.Columns.Add("LOTID");
            sendEmailDt.Columns.Add("AREANAME");
            sendEmailDt.Columns.Add("CUSTOMERNAME");
            sendEmailDt.Columns.Add("WEEK");
            sendEmailDt.Columns.Add("DEFECTNAME");
            sendEmailDt.Columns.Add("REMARK");
            sendEmailDt.Columns.Add("USERID");
            sendEmailDt.Columns.Add("TITLE");
            sendEmailDt.Columns.Add("INSPECTION");
            sendEmailDt.Columns.Add("LANGUAGETYPE");

            return sendEmailDt;
        }
        #endregion

        #region - 가공품 부적합 발행 이메일 :: 이메일 내용을 parameter로 받아 이메일 내용 구성  |
        /// <summary>
        /// Email 팝업에서 미리 보기 내용을 만드는 함수
        /// </summary>
        /// <returns></returns>
        public static string GetSubassemblyAbnormalEmailContents(DataRow row)
        {
            string content = "";

            content = string.Format(string.Concat(@"○{0}:{1}", "\r\n",
                                                                   "○{2}({3}/{4}):{5}({6}/{7})", "\r\n",
                                                                   "○{8}:{9}", "\r\n",
                                                                   "○{10}({11}/{12}):{13}({14}/{15})", "\r\n",
                                                                   "○{16}:{17}", "\r\n",
                                                                   "○{18}:{19}({20})", "\r\n",
                                                                   "○{21}:{22}", "\r\n",
                                                                   "○{23}:{24}", "\r\n",
                                                                   "○{25}:{26}", "\r\n"),
                                                     Language.Get("CONVENDORNAME"),
                                                     Format.GetString(row["VENDORNAME"]),
                                                     Language.Get("MATERIALNAME"),
                                                     Language.Get("MATERIALDEF"), Language.Get("COMPONENTITEMVERSION"),
                                                     Format.GetString(row["CONSUMABLEDEFNAME"]),
                                                     Format.GetString(row["CONSUMABLEDEFID"]), Format.GetString(row["CONSUMABLEDEFVERSION"]),
                                                     Language.Get("MATERIALLOT"),
                                                     Format.GetString(row["MATERIALLOT"]),
                                                     Language.Get("PRODUCTDEFNAME"),
                                                     Language.Get("PRODUCTDEFID"), Language.Get("PRODUCTDEFVERSION"),
                                                     Format.GetString(row["PRODUCTDEFNAME"]),
                                                     Format.GetString(row["PRODUCTDEFID"]), Format.GetString(row["PRODUCTDEFVERSION"]),
                                                     Language.Get("LOTID"), Format.GetString(row["LOTID"]),
                                                     Language.Get("STANDARDSEGMENT"), Format.GetString(row["PROCESSSEGMENTNAME"]), Format.GetString(row["PROCESSSEGMENTID"]),
                                                     Language.Get("DEFECTNAME"), Format.GetString(row["DEFECTNAME"]),
                                                     Language.Get("MEASUREVALUE"), Format.GetString(row["MEASUREVALUE"]),
                                                     Language.Get("INSPECTIONRESULT"), Format.GetString(row["INSPECTIONRESULT"]));

            return content;
        }

        /// <summary>
        /// Email에 전달 할 내용의 dataTable을 만드는 함수
        /// </summary>
        /// <returns></returns>
        public static DataTable CreateSubassemblyAbnormalEmailDt()
        {
            DataTable sendEmailDt = new DataTable();

            sendEmailDt.Columns.Add("VENDORNAME");
            sendEmailDt.Columns.Add("CONSUMABLEDEFNAME");
            sendEmailDt.Columns.Add("CONSUMABLEDEFID");
            sendEmailDt.Columns.Add("CONSUMABLEDEFVERSION");
            sendEmailDt.Columns.Add("MATERIALLOT");
            sendEmailDt.Columns.Add("PRODUCTDEFNAME");
            sendEmailDt.Columns.Add("PRODUCTDEFID");
            sendEmailDt.Columns.Add("PRODUCTDEFVERSION");
            sendEmailDt.Columns.Add("LOTID");
            sendEmailDt.Columns.Add("PROCESSSEGMENTNAME");
            sendEmailDt.Columns.Add("PROCESSSEGMENTID");
            sendEmailDt.Columns.Add("INSPECTIONRESULT");
            sendEmailDt.Columns.Add("DEFECTNAME");
            sendEmailDt.Columns.Add("MEASUREVALUE");
            sendEmailDt.Columns.Add("REMARK");
            sendEmailDt.Columns.Add("USERID");
            sendEmailDt.Columns.Add("TITLE");
            sendEmailDt.Columns.Add("INSPECTION");
            sendEmailDt.Columns.Add("LANGUAGETYPE");

            return sendEmailDt;
        }
        #endregion

        #endregion

        #region ◆ Grid Event |

        #region - SetGridDoubleClickCheck :: Grid Row Double Click 시 Grid Check 설정 |
        /// <summary>
        /// Grid Row Double Click 시 Grid Check 설정
        /// </summary>
        /// <param name="grd"></param>
        /// <param name="sender"></param>
        public static void SetGridDoubleClickCheck(Micube.Framework.SmartControls.SmartBandedGrid grd, object sender)
        {
            // 더블클릭 시 체크박스 체크
            Micube.Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView view = (Micube.Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView)sender;

            if (grd.View.IsRowChecked(view.FocusedRowHandle))
                grd.View.CheckRow(view.FocusedRowHandle, false);
            else
                grd.View.CheckRow(view.FocusedRowHandle, true);
        }
        #endregion

        #region - SetGridRowStyle :: Grid Row Stype 설정 |
        /// <summary>
        /// Grid Row Stype 설정
        /// </summary>
        /// <param name="grd"></param>
        /// <param name="e"></param>
        public static void SetGridRowStyle(Micube.Framework.SmartControls.SmartBandedGrid grd, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0)
                return;

            int rowIndex = grd.View.FocusedRowHandle;

            if (rowIndex == e.RowHandle)
            {
                e.Appearance.BackColor = Color.FromArgb(254, 254, 16);
                e.HighPriority = true;
            }
        }
        #endregion

        #region  - SetGridColumnData :: Grid Column의 데이터 세팅 |
        /// <summary>
        /// Grid Column의 데이터 세팅
        /// </summary>
        /// <param name="grd"></param>
        /// <param name="dr"></param>
        public static void SetGridColumnData(SmartBandedGrid grd, DataRow dr)
        {
            foreach (DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn dc in grd.View.Columns)
            {
                if (dr.GetObject(dc.FieldName) == null) continue;

                string data = dr[dc.FieldName].ToString();

                if (!dc.FieldName.Equals("LOTID") && dr.Table.Columns[dc.FieldName].DataType.Name.Equals("Decimal"))
                    grd.View.SetFocusedRowCellValue(dc.FieldName, Format.GetDecimal(dr[dc.FieldName].ToString()));
                else
                    grd.View.SetFocusedRowCellValue(dc.FieldName, dr[dc.FieldName].ToString());
            }
        }
        #endregion

        #endregion

        #region YJKIM 
        public static Dictionary<string, object> ConvertParameter(Dictionary<string, object> target)
        {
            Dictionary<string, object> convertData = new Dictionary<string, object>();

            foreach (KeyValuePair<string, object> item in target)
            {
                if (item.Value != null)
                    convertData[item.Key] = changeArgStringSQL(item.Value.ToString());
                else
                    convertData[item.Key] = null;
            }

            return convertData;
        }
        #region - changeArgString :: SQL Injection 문자열 대체 |
        /// <summary>
        /// SQL Injection 문자열 대체
        /// </summary>
        /// <param name="sArgStr"></param>
        /// <returns></returns>
        public static string changeArgStringSQL(string sArgStr)
        {
            sArgStr = sArgStr.Replace("'", "''");

            return sArgStr;
        }
        #endregion
        #endregion

        #region ◆ Drawing Control |
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, Int32 wMsg, bool wParam, Int32 lParam);

        private const int WM_SETREDRAW = 11;

        public static void SuspendDrawing(Control parent)
        {
            SendMessage(parent.Handle, WM_SETREDRAW, false, 0);
        }

        public static void ResumeDrawing(Control parent)
        {
            SendMessage(parent.Handle, WM_SETREDRAW, true, 0);
            parent.Refresh();
        }
        #endregion


        #region - AddConditionProcessSegmentPopup :: 팝업형 조회조건 - 공정 (일괄작업완료 화면 전용)|
        /// <summary>
        /// 팝업형 조회조건 - 공정(일괄작업완료 화면 전용)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="position"></param>
        /// <param name="isMultiSelect"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public static ConditionCollection AddConditionProcessSegmentPopup_New(string id, double position, bool isMultiSelect, ConditionCollection conditions)
        {            
            //2020.09.09 문명진 수석 수정(작업장에 따른 공정 조회 = 대공정 기준)
            var processSegmentIdPopup = conditions.AddSelectPopup(id, new SqlQuery("GetProcessSegmentList", "10003", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "PROCESSSEGMENTCLASSTYPE=MiddleProcessSegmentClass"), "PROCESSSEGMENTNAME", "PROCESSSEGMENTID")
                .SetPopupLayout(Language.Get("SELECTPROCESSSEGMENTID"), PopupButtonStyles.Ok_Cancel, true, true)
                .SetPopupLayoutForm(800, 800)
                .SetPopupAutoFillColumns("PROCESSSEGMENTNAME")
                .SetLabel("PROCESSSEGMENT")
                .SetPosition(position);

            // 복수 선택 여부에 따른 Result Count 지정
            if (isMultiSelect)
                processSegmentIdPopup.SetPopupResultCount(0);
            else
                processSegmentIdPopup.SetPopupResultCount(1);

            processSegmentIdPopup.Conditions.AddComboBox("PROCESSSEGMENTCLASSID_MIDDLE", new SqlQuery("GetProcessSegmentClassByType", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "PROCESSSEGMENTCLASSTYPE=MiddleProcessSegmentClass"), "PROCESSSEGMENTCLASSNAME", "PROCESSSEGMENTCLASSID")
                .SetLabel("MIDDLEPROCESSSEGMENT")
                .SetEmptyItem();
            processSegmentIdPopup.Conditions.AddTextBox("PROCESSSEGMENT");


            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME_TOP", 150)
                .SetLabel("LARGEPROCESSSEGMENT");
            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME_MIDDLE", 150)
                .SetLabel("MIDDLEPROCESSSEGMENT");
            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150);
            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);

            return conditions;
        }
        #endregion
    }
}
