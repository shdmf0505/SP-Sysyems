#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;

using Micube.SmartMES.Commons;
using Micube.SmartMES.Commons.Gerber;
using Micube.SmartMES.Commons.Controls;
using Micube.SmartMES.Commons.Gerber.Core;

using System;
using System.IO;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프 로 그 램 명  : 기준 정보 > 품질관리 > PCS 이미지 관리
    /// 업  무  설  명  : Defect Map PCS 도면 등록화면
    /// 생    성    자  : 전우성
    /// 생    성    일  : 2019-07-02
    /// 수  정  이  력  : 
    ///     2019-12-19  전우성 Table 형식 변경으로 전면 수정
    /// 
    /// 
    /// </summary>
    public partial class GerberDataConversionToPng : SmartConditionManualBaseForm
    {
        #region Local Variables

        private readonly string LOCALFILEPATH = Path.Combine(Application.StartupPath, "PCSIMAGE");

        #endregion

        #region 생성자

        public GerberDataConversionToPng()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeEvent();
            InitializeGrid();
            InitializeLanguageKey();

            txtWidth.Editor.ReadOnly = true;
            txtHeight.Editor.ReadOnly = true;
            txtPath.ReadOnly = true;

            btnConversion.Enabled = false;

            picMain.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
        }

        /// <summary>
        /// Language Key 초기화
        /// </summary>
        private void InitializeLanguageKey()
        {
            grdMain.LanguageKey = "IMAGELIST";
            grpMain.LanguageKey = "DETAIL";
            txtWidth.LanguageKey = "WIDTH";
            txtHeight.LanguageKey = "HEIGHT";
            btnConversion.LanguageKey = "IMAGEPROCESSING";
        }

        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            grdMain.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export;
            grdMain.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            //? 운영때 품목/코드 팝업으로 변경해야함
            #region 품목 코드/version 가져오기 - 운영에 오픈 / 영풍 GerberDataConversionToPng_YPE.cs 를 덮어야함
            /*
            var productDefPopup = grdMain.View.AddSelectPopupColumn("PRODUCTDEFID", new SqlQuery("GetProductDefList", "10003", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "PRODUCTDEFID")
                                         .SetValidationKeyColumn()
                                         .SetValidationIsRequired()
                                         .SetTextAlignment(TextAlignment.Left)
                                         .SetPopupAutoFillColumns("PRODUCTDEFID")
                                         .SetDefault(null, null, null)
                                         .SetPopupResultCount(1)
                                         .SetPopupLayout("PRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)  //팝업창 UI 설정.
                                         .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                                         .SetPopupApplySelection((selectedRow, dataGirdRow) =>
                                         {
                                             foreach (DataRow dr in selectedRow)
                                             {
                                                 grdMain.View.SetRowCellValue(grdMain.View.GetFocusedDataSourceRowIndex(), "PRODUCTDEFVERSION", Format.GetString(dr["PRODUCTDEFVERSION"]));
                                             }
                                         });

            productDefPopup.Conditions.AddTextBox("PRODUCTDEF");

            productDefPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150)
                                       .SetLabel("PRODUCTDEFID");

            productDefPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 150)
                                       .SetLabel("PRODUCTDEFNAME");

            productDefPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 100)
                                       .SetLabel("PRODUCTDEFVERSION");

            grdMain.View.AddTextBoxColumn("PRODUCTDEFVERSION", 60)
                   .SetValidationKeyColumn()
                   .SetValidationIsRequired()
                   .SetIsReadOnly()
                   .SetTextAlignment(TextAlignment.Left);
            */
            #endregion

            grdMain.View.AddTextBoxColumn("PRODUCTDEFID", 120)
                   .SetValidationKeyColumn()
                   .SetValidationIsRequired()
                   .SetTextAlignment(TextAlignment.Left);

            grdMain.View.AddTextBoxColumn("PRODUCTDEFVERSION", 60)
                   .SetValidationKeyColumn()
                   .SetValidationIsRequired()
                   .SetTextAlignment(TextAlignment.Left);

            grdMain.View.AddTextBoxColumn("WIDTH", 60)
                   .SetDisplayFormat("{0:f2}", MaskTypes.Custom)
                   .SetIsReadOnly()
                   .SetTextAlignment(TextAlignment.Right);

            grdMain.View.AddTextBoxColumn("HEIGHT", 60)
                   .SetDisplayFormat("{0:f2}", MaskTypes.Custom)
                   .SetIsReadOnly()
                   .SetTextAlignment(TextAlignment.Right);

            grdMain.View.AddTextBoxColumn("FILENAME", 110)
                   .SetIsReadOnly()
                   .SetIsHidden()
                   .SetTextAlignment(TextAlignment.Left);

            grdMain.View.AddTextBoxColumn("FILEDATA", 10)
                   .SetIsHidden();

            grdMain.View.AddTextBoxColumn("FILESIZE", 10)
                   .SetIsHidden();

            grdMain.View.AddTextBoxColumn("FILEEXT", 10)
                   .SetIsHidden();

            grdMain.View.AddTextBoxColumn("FILEPATH", 10)
                   .SetIsHidden();

            grdMain.View.AddTextBoxColumn("SAFEFILENAME", 10)
                   .SetIsHidden();

            grdMain.View.AddTextBoxColumn("LOCALFILEPATH", 10)
                   .SetIsHidden();

            grdMain.View.AddTextBoxColumn("PROCESSINGSTATUS", 10)
                   .SetIsHidden();

            grdMain.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                   .SetDefault("Valid")
                   .SetTextAlignment(TextAlignment.Center);

            grdMain.View.PopulateColumns();
            grdMain.View.BestFitColumns();
        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            #region 운영시 풀어야 함
            //? 기준정보 품목항목 삭제시 버젼도 삭제
            /*
            Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").EditValueChanged += (s, e) =>
            {
                SmartSelectPopupEdit poPopup = s as SmartSelectPopupEdit;

                if (Format.GetFullTrimString(poPopup.EditValue).Equals(string.Empty))
                {
                    Conditions.GetControl<SmartTextBox>("P_PRODUCTDEFVERSION").EditValue = string.Empty;
                }
            };
            */
            #endregion

            // 변환 버튼 이벤트
            btnConversion.Click += (s, e) =>
            {
                OpenFileDialog dialog = new OpenFileDialog()
                {
                    DefaultExt = "gbr",
                    Filter = "GBR File(*.gbr)|*.gbr"
                };

                if (dialog.ShowDialog().Equals(DialogResult.OK))
                {
                    try
                    {
                        DialogManager.ShowWaitArea(this.pnlContent);

                        ParsedGerber parsedGerber = Gerber.SaveGerberFileToImage(dialog.FileName, dialog.FileName, 200, Color.DarkGray, Color.White);

                        if (parsedGerber == null)
                        {
                            MSGBox.Show(MessageBoxType.Warning, "");
                            return;
                        }

                        DataRow dr = grdMain.View.GetDataRow(grdMain.View.GetFocusedDataSourceRowIndex());

                        string createFilePath = FileCreate(parsedGerber);

                        dr["FILENAME"] = Path.GetFileNameWithoutExtension(createFilePath);
                        dr["FILEDATA"] = Convert.ToBase64String((byte[])new ImageConverter().ConvertTo(parsedGerber.TopBitmap, typeof(byte[])));
                        dr["FILESIZE"] = new FileInfo(createFilePath).Length;
                        dr["FILEEXT"] = "jpg";
                        dr["FILEPATH"] = "PCSIMAGE";
                        dr["SAFEFILENAME"] = Path.GetFileName(createFilePath);
                        dr["PROCESSINGSTATUS"] = "Wait";
                        dr["LOCALFILEPATH"] = createFilePath;

                        dr["WIDTH"] = Format.GetString(parsedGerber.PanelWidth);
                        dr["HEIGHT"] = Format.GetString(parsedGerber.PanelHeight);

                        SetImageView(dr);

                        txtPath.Text = dialog.SafeFileName;
                    }
                    catch (Exception ex)
                    {
                        throw MessageException.Create(Format.GetString(ex));
                    }
                    finally
                    {
                        DialogManager.CloseWaitArea(this.pnlContent);
                    }
                }
            };

            // Grid 클릭 이벤트
            grdMain.View.FocusedRowChanged += (s, e) =>
            {
                try
                {
                    if (e.FocusedRowHandle < 0)
                    {
                        return;
                    }

                    DialogManager.ShowWaitArea(this.pnlContent);
                    SetImageView(grdMain.View.GetDataRow(grdMain.View.GetFocusedDataSourceRowIndex()));

                    btnConversion.Enabled = true;
                }
                catch (Exception ex)
                {
                    throw MessageException.Create(Format.GetString(ex));
                }
                finally
                {
                    DialogManager.CloseWaitArea(this.pnlContent);
                }
            };

            // Grid에 Data가 없으면 상세의 내용 삭제
            grdMain.ToolbarDeleteRow += (s, e) => SetClear();
        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            if (!FileUpload())
            {
                return;
            }

            ExecuteRule("QcpcsimageManagement", grdMain.GetChangedRows());

            DirectoryInfo dir = new DirectoryInfo(LOCALFILEPATH);
            if (dir.Exists)
            {
                Directory.Delete(LOCALFILEPATH, true);
            }
        }

        #endregion

        #region 검색

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            try
            {
                DialogManager.ShowWaitArea(this.pnlContent);

                await base.OnSearchAsync();

                if (await SqlExecuter.QueryAsync("GetPCSImage", "10001", Conditions.GetValues()) is DataTable dt)
                {
                    if (dt.Rows.Count.Equals(0))
                    {
                        grdMain.DataSource = null;
                        SetClear();

                        ShowMessage("NoSelectData");
                        return;
                    }

                    grdMain.DataSource = dt;
                    SetImageView(grdMain.View.GetDataRow(0));
                }
            }
            catch (Exception ex)
            {
                throw Framework.MessageException.Create(Format.GetString(ex));
            }
            finally
            {
                DialogManager.CloseWaitArea(this.pnlContent);
            }
        }

        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            //품목
            var productDefPopup = Conditions.AddSelectPopup("P_PRODUCTDEFID", new SqlQuery("GetProductDefList", "10003", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "PRODUCTDEFID", "PRODUCTDEFID")
                                            .SetLabel("PRODUCTDEFID")
                                            .SetPosition(0)
                                            .SetDefault(null, null, null)
                                            .SetPopupResultCount(1)
                                            .SetPopupLayout("PRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)
                                            .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                                            .SetPopupApplySelection((selectedRow, dataGirdRow) =>
                                            {
                                                foreach (DataRow row in selectedRow)
                                                {
                                                    if (selectedRow.Count() > 0)
                                                    {
                                                        this.Conditions.GetControl<SmartTextBox>("P_PRODUCTDEFVERSION").EditValue = row["PRODUCTDEFVERSION"].ToString();
                                                    }
                                                }
                                            });

            productDefPopup.Conditions.AddTextBox("PRODUCTDEF");

            productDefPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150).SetLabel("PRODUCTDEFID");
            productDefPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 150).SetLabel("PRODUCTDEFNAME");
            productDefPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 100).SetLabel("PRODUCTDEFVERSION");

            Conditions.AddTextBox("P_PRODUCTDEFVERSION")
                      .SetLabel("PRODUCTDEFVERSION")
                      .SetPosition(0.1)
                      .SetIsReadOnly();
        }

        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();
        }

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            grdMain.View.CheckValidation();

            if (grdMain.DataSource == null)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }

            if (grdMain.GetChangedRows().Rows.Count.Equals(0))
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }

        #endregion

        #region Private Function

        /// <summary>
        /// 상세(Detail) 화면의 내용을 초기화 한다.
        /// </summary>
        private void SetClear()
        {
            txtWidth.Text = "";
            txtHeight.Text = "";
            txtPath.Text = "";
            picMain.Image = null;
        }

        /// <summary>
        /// 거버 파일 로컬에 생성 후 경로 리턴
        /// </summary>
        /// <param name="parsedGerber"></param>
        /// <returns></returns>
        private string FileCreate(ParsedGerber parsedGerber)
        {
            DirectoryInfo dir = new DirectoryInfo(LOCALFILEPATH);
            if (!dir.Exists)
            {
                dir.Create();
            }

            Random rand = new Random();
            string cyInput = "abcdefghijklmnopqrstuvwxyz0123456789";
            string fileName = new string(Enumerable.Range(0, 30).Select(x => cyInput[rand.Next(0, cyInput.Length)]).ToArray());

            string createFilePath = Path.Combine(LOCALFILEPATH, string.Join(".", fileName, "jpg"));

            using (FileStream fs = File.Create(createFilePath))
            {
                byte[] bt = (byte[])new ImageConverter().ConvertTo(parsedGerber.TopBitmap, typeof(byte[]));
                fs.Write(bt, 0, bt.Length);
            }

            return createFilePath;
        }

        /// <summary>
        /// 저장 시 파일 업로드
        /// </summary>
        /// <returns></returns>
        private bool FileUpload()
        {
            int totalSize = 0;
            DataTable finalDt = (grdMain.DataSource as DataTable).Clone();

            foreach (DataRow dr in grdMain.GetChangedRows().Rows)
            {
                if (Format.GetString(dr["_STATE_"]).Equals("deleted"))
                {
                    continue;
                }

                if (Format.GetString(dr["FILENAME"]).Equals(""))
                {
                    throw MessageException.Create("ValidationImage");
                }

                finalDt.ImportRow(dr);
                totalSize = totalSize + Format.GetInteger(dr["FILESIZE"]);
            }

            if (totalSize.Equals(0))
            {
                return true;
            }

            FileProgressDialog dialog = new FileProgressDialog(finalDt, UpDownType.Upload, "", totalSize);

            if (!dialog.ShowDialog().Equals(DialogResult.OK))
            {
                return false;
            }

            return true;
        }


        /// <summary>
        /// Grid에 선택한 Row의 이미지를 보여준다.
        /// </summary>
        /// <param name="dr"></param>
        private void SetImageView(DataRow dr)
        {
            SetClear();

            txtWidth.Text = Format.GetString(dr["WIDTH"]) ?? "";
            txtHeight.Text = Format.GetString(dr["HEIGHT"]) ?? "";

            if (Format.GetString(dr["FILENAME"]).Equals(string.Empty))
            {
                return;
            }

            if (Format.GetString(dr["FILEDATA"]).Equals(string.Empty))
            {
                string serverPath = Format.GetString(dr.GetObject("FILEPATH"));

                string file = string.Join(".", Format.GetString(dr.GetObject("FILENAME")), Format.GetString(dr.GetObject("FILEEXT")));
                picMain.Image = CommonFunction.GetFtpImageFileToBitmap(serverPath, file);

                //string serverPath = AppConfiguration.GetString("Application.SmartDeploy.Url") + dr["FILEPATH"];
                //serverPath = serverPath + ((serverPath.EndsWith("/")) ? "" : "/");
                //serverPath = serverPath + string.Join(".", Format.GetString(dr["FILENAME"]), Format.GetString(dr["FILEEXT"]));
                //picMain.LoadAsync(serverPath);
            }
            else
            {
                picMain.Image = (Bitmap)new ImageConverter().ConvertFrom(Convert.FromBase64String(Format.GetString(dr["FILEDATA"])));
            }
        }

        #endregion
    }
}