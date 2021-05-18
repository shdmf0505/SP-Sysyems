using Micube.Framework.SmartControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Micube.Framework.Net;
using Micube.Framework;
using System.IO;
using DevExpress.XtraEditors.Repository;
using Micube.SmartMES.Commons.Controls;
using Micube.SmartMES.QualityAnalysis.KeyboardClipboard;
using System.Drawing.Imaging;
using Micube.SmartMES.Commons;
using DevExpress.Utils;

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 신뢰성검증 > BBT 결과 등록
    /// 업  무  설  명  : BBT 결과 결과를 등록하는 팝업
    /// 생    성    자  : 유석진
    /// 생    성    일  : 2019-12-23
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ReliaVerifiResultBBTPopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region Local Variables

        /// <summary>
        /// 팝업진입전 그리드의 Row
        /// </summary>
        public DataRow CurrentDataRow { get; set; }

        /// <summary>
        /// 부모 화면 
        /// </summary>
        public SmartConditionBaseForm ParentForm { get; set; }

        DataTable _dtInspectionFile;
        private string[] formats = Enum.GetNames(typeof(ClipboardFormat));

        private string sINSPITEMID = string.Empty;
        private string sINSPITEMNAME = string.Empty;
        #endregion

        #region 생성자

        public ReliaVerifiResultBBTPopup(DataRow dr)
        {
            InitializeComponent();

            CurrentDataRow = dr;

            manufacturingHistoryControl1.CurrentDataRow = dr;
            manufacturingHistoryControl1.tPaent = this;

            InitializeControl();
            InitializeLanguageKey();
            InitializeGrid();
            InitializeEvent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 입력 컨트롤 초기화
        /// </summary>
        private void InitializeControl()
        {
            // 총수량
            txtTotalQty.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txtTotalQty.Properties.Mask.EditMask = "#,###,##0.####";
            txtTotalQty.Properties.Mask.UseMaskAsDisplayFormat = true;
            txtTotalQty.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
            // 검사수량
            txtVerifiCount.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txtVerifiCount.Properties.Mask.EditMask = "#,###,##0.####";
            txtVerifiCount.Properties.Mask.UseMaskAsDisplayFormat = true;
            txtVerifiCount.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
            // 불량수량
            txtDefectQty.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txtDefectQty.Properties.Mask.EditMask = "#,###,##0.####";
            txtDefectQty.Properties.Mask.UseMaskAsDisplayFormat = true;
            txtDefectQty.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
            // 불량율
            txtSpecoutPercentage.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txtSpecoutPercentage.Properties.Mask.EditMask = "#,###,##0.####";
            txtSpecoutPercentage.Properties.Mask.UseMaskAsDisplayFormat = true;
            txtSpecoutPercentage.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Far;

            selectUnitList(); // 단위

            selectCodeList("OKNG", cboJudgmentResult); // 판정결과
            selectCodeList("YesNo", cboIsNCRPublish); // NCR 발행여부
            selectCodeList("YesNo", cboIsCompletion); // 완료여부

            selectDefectCodePopup(); // 불량코 팝업
            selectInspitemPopup(); // 검증항목 팝업
        }

        /// <summary>
        /// Language Key 초기화
        /// </summary>
        private void InitializeLanguageKey()
        {
            //this.LanguageKey = "MEASURVALUEREGISTER";
            btnImageSave.LanguageKey = "EXPORT";
        }

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            InitializeGrdProductInformation(); // 제품 정보 그리드 초기화
            InitializeGridMeasureValue(); // 측정값 입력 그리드 초기화
        }
        /// <summary>
        /// 제품 정보 그리드 초기화
        /// </summary>
        private void InitializeGrdProductInformation()
        {
            grdProductnformation.View
                .AddTextBoxColumn("PRODUCTDEFID", 150)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 품목코드

            grdProductnformation.View
                .AddTextBoxColumn("PRODUCTDEFNAME", 210)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 품목명

            grdProductnformation.View
                .AddTextBoxColumn("LOTID", 150)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // LOT ID

            grdProductnformation.View
                .AddTextBoxColumn("PROCESSSEGMENTNAME", 210)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 표준공정

            grdProductnformation.View
                .AddTextBoxColumn("AREANAME", 210)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 작업장

            grdProductnformation.View
                .AddTextBoxColumn("EQUIPMENTNAME", 210)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 설비(호기)

            grdProductnformation.View
                .AddTextBoxColumn("TRACKOUTTIME", 180)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center); // 작업종료시간

            grdProductnformation.View.PopulateColumns();
        }

        private void InitializeGridMeasureValue()
        {
            grdMeasureValue.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export;
            grdMeasureValue.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdMeasureValue.View
                .AddTextBoxColumn("TITLE", 210)
                .SetTextAlignment(TextAlignment.Left); // 제목

            grdMeasureValue.View
                .AddSpinEditColumn("MEASUREVALUE", 210)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("{#,###,##0.####}", MaskTypes.Numeric); // 측정값

            grdMeasureValue.View
                .AddTextBoxColumn("COMMENTS", 500)
                .SetTextAlignment(TextAlignment.Left); // 내용

            grdMeasureValue.View.PopulateColumns();

            RepositoryItemTextEdit edit = new RepositoryItemTextEdit();
            edit.Mask.EditMask = "#,###,##0.####";
            edit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            edit.Mask.UseMaskAsDisplayFormat = true;

            grdMeasureValue.View.Columns["MEASUREVALUE"].ColumnEdit = edit;
        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        private void InitializeEvent()
        {
            this.Load += ReliaVerifiResultBBTPopup_Load;
            btnSave.Click += BtnSave_Click;
            btnClose.Click += BtnClose_Click;
            this.KeyDown += ReliaVerifiResultBBTPopup_KeyDown;
            cboJudgmentResult.Control.EditValueChanged += Control_EditValueChanged;
            // 2020.04.17-유석진-불량수량 입력 시 불량율 계산
            txtDefectQty.Properties.EditValueChanged += Properties_EditValueChanged;

            // 이미지 저장 이벤트
            btnImageSave.Click += (s, e) =>
            {
                var enumerator = flowMeasuredPicture.Controls.GetEnumerator();
                var i = 0;

                while (enumerator.MoveNext())
                {
                    BBTMultiVerificationResultControl vr = enumerator.Current as BBTMultiVerificationResultControl;

                    if (vr.selectImage().ToString() != string.Empty && vr.chkPicture.Checked == true)
                    {
                        i++;
                    }
                }

                if (i == 0) // 선택된 건이 없으면 이미지 저장 안됨
                {
                    return;
                }

                try
                {
                    DialogManager.ShowWaitArea(this);
                    FolderBrowserDialog dialog = new FolderBrowserDialog();
                    dialog.SelectedPath = Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);

                    if (dialog.ShowDialog().Equals(DialogResult.OK))
                    {
                        string folderPath = dialog.SelectedPath;

                        enumerator = flowMeasuredPicture.Controls.GetEnumerator();

                        while (enumerator.MoveNext())
                        {
                            BBTMultiVerificationResultControl vr = enumerator.Current as BBTMultiVerificationResultControl;

                            if (vr.selectImage().ToString() != string.Empty && vr.chkPicture.Checked == true)
                            {
                                Bitmap image = (Bitmap)vr.selectImage();
                                Bitmap newImage = new Bitmap(image);
                                image.Dispose();
                                image = null;
                                newImage.Save(string.Concat(folderPath, "\\", vr.strFileName()));

                                vr.picMeasurePrinted.Image = newImage;
                                vr.picMeasurePrinted.Tag = newImage;
                            }
                        }

                        ShowMessage("SuccedSave");
                    }
                }
                catch (Exception ex)
                {
                    throw Framework.MessageException.Create(ex.ToString());
                }
                finally
                {
                    DialogManager.CloseWaitArea(this);
                }
            };
        }

        // 2020.04.17-유석진-불량수량을 입력하면 불량율을 계산
        private void Properties_EditValueChanged(object sender, EventArgs e)
        {
            txtSpecoutPercentage.EditValue = (txtDefectQty.EditValue.ToSafeDecimal() / CurrentDataRow["VERIFICOUNT"].ToSafeDecimal())*100; 
        }

        /// <summary>        
        /// OK 일 경우 NCR 발행여부 N으로 고정
        /// </summary>
        private void Control_EditValueChanged(object sender, EventArgs e)
        {
            if(cboJudgmentResult.EditValue.Equals("OK"))
            {
                cboIsNCRPublish.EditValue = "N";
                cboIsNCRPublish.Enabled = false;
            } else
            {
                cboIsNCRPublish.EditValue = "Y";
                cboIsNCRPublish.Enabled = true;
            }
        }

        /// <summary>        
        /// 붙여넣기 호출
        /// </summary>
        private void ReliaVerifiResultBBTPopup_KeyDown(object sender, KeyEventArgs e)
        {
            if (tabMeasuredValue.SelectedTabPageIndex == 0)
            {
                if (e.KeyCode == Keys.V && e.Modifiers == Keys.Control)
                {
                    //try
                    //{
                    //    // Get the DataObject.
                    //    IDataObject data_object = Clipboard.GetDataObject();

                    //    // Look for a file drop.
                    //    if (data_object.GetDataPresent(DataFormats.FileDrop))
                    //    {
                    //        string[] files = (string[])
                    //            data_object.GetData(DataFormats.FileDrop);
                    //        //foreach (string file_name in files)
                    //        for(int i = files.Length - 1; i>=0; i--)
                    //        {
                    //            Bitmap image = new Bitmap(files[i]);
                    //            BBTMultiVerificationResultControl vr = new BBTMultiVerificationResultControl(image, files[i]);
                    //            flowMeasuredPicture.Controls.Add(vr);
                    //        }
                    //    }
                    //} catch(Exception ex)
                    //{
                    //    throw ex;
                    //} finally
                    //{                     
                    //}

                    //Control.ControlCollection ctr = flowMeasuredPicture.Controls;

                    //foreach (Control c in ctr)
                    //{
                    //    if (c.GetType() == typeof(BBTMultiVerificationResultControl))
                    //    {
                    //        BBTMultiVerificationResultControl vr = c as BBTMultiVerificationResultControl;
                    //        string strFile = vr.strFile;
                    //    }
                    //}

                    var iData = Clipboard.GetDataObject();

                    ClipboardFormat? format = null;

                    foreach (var f in formats)
                        if (iData.GetDataPresent(f))
                        {
                            format = (ClipboardFormat)Enum.Parse(typeof(ClipboardFormat), f);
                            break;
                        }

                    var data = iData.GetData(format.ToString());

                    if (data == null || format == null)
                        return;

                    //flowMeasuredPicture.Focus();
                    if ((ClipboardFormat)format == ClipboardFormat.FileDrop)
                    {
                        string[] files = (string[])data;
                        //foreach (string file_name in files)
                        for (int i = files.Length - 1; i >= 0; i--)
                        {
                            Bitmap image = new Bitmap(files[i]);
                            BBTMultiVerificationResultControl vr = new BBTMultiVerificationResultControl(image, files[i]);
                            flowMeasuredPicture.Controls.Add(vr);
                        }
                    }
                    else if ((ClipboardFormat)format == ClipboardFormat.Bitmap)
                    {
                        Bitmap image = (Bitmap)data;
                        string sBitmapName = "ClipboardBitmap-" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".png";
                        BBTMultiVerificationResultControl vr = new BBTMultiVerificationResultControl(image, sBitmapName);
                        flowMeasuredPicture.Controls.Add(vr);
                        image.Save(sBitmapName, ImageFormat.Png);
                    }
                }


            }
        }

        /// <summary>
        /// 닫기버튼 클릭시 화면 닫기
        /// </summary>
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 저장버튼 클릭시 신뢰성 검증 결과 등록
        /// </summary>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            // NCR 발행여부가 변경된 경우 Y인 경우 불량명 필수 입력
            if (cboIsNCRPublish.EditValue.Equals("Y"))
            {
                if (popDefectCodeName.EditValue.Equals(""))
                {
                    throw MessageException.Create("NoDefectCode");
                }
            }

            // 검사항목 필수 입력
            if (string.IsNullOrWhiteSpace(cboIsNCRPublish.EditValue.ToString()))
            {
                throw MessageException.Create("NoSelectRelDataRow");
            }

            DialogResult result = System.Windows.Forms.DialogResult.No;

            result = this.ShowMessage(MessageBoxButtons.YesNo, "InfoSave");// 저장 하시겠습니까?

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    this.ShowWaitArea();
                    btnSave.Enabled = false;
                    btnClose.Enabled = false;

                    SaveData();

                    ShowMessage("SuccessSave");

                    var _parent = ParentForm as ReliaVerifiResultBBT; // 저장결과 부모 화면에서 조회
                    _parent.Search();

                    this.Close();
                }
                catch (Exception ex)
                {
                    this.ShowError(ex);
                }
                finally
                {
                    this.CloseWaitArea();
                    btnSave.Enabled = true;
                    btnClose.Enabled = true;
                    //this.Close();
                }
            }
        }

        /// <summary>
        /// Form Road시 초기화
        /// </summary>
        private void ReliaVerifiResultBBTPopup_Load(object sender, EventArgs e)
        {
            grdAttachedFile.ButtonVisible = true;
            grdReport.ButtonVisible = true;
            grdAttachedFile.LanguageKey = "ATTACHEDFILE";
            grdReport.LanguageKey = "REPORT";

            SetMainInfo(); // 기본 입력 정보 조회
            SearchProductInformation(); // 제품 정보 조회

            SearchInspectionFile(); // 검증 결과 조회
            SearchMeasuredValue(); // 측정값 조회
            SearchAttachFile(); // 첨부파일 조회
            SearchReport(); // 보고서

            flowMeasuredPicture.AutoScroll = true; // 스크롤 생기게 함

            InitializePopup();
        }

        #endregion

        #region Private Function

        /// <summary>
        /// 검사자를 선택하는 팝업
        /// </summary>
        private void InitializePopup()
        {
            popInspector.Editor.SelectPopupCondition = CreateUserPopup();
        }

        /// <summary>
        /// 검사자 팝업
        /// </summary>
        /// <returns></returns>
        private ConditionItemSelectPopup CreateUserPopup()
        {
            ConditionItemSelectPopup conditionItem = new ConditionItemSelectPopup();
            conditionItem.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            conditionItem.SetPopupLayout("USERLIST", PopupButtonStyles.Ok_Cancel, true, true);
            conditionItem.Id = "INSPECTIONUSER";
            conditionItem.LabelText = "INSPECTIONUSER";
            conditionItem.SearchQuery = new SqlQuery("GetUserList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}");
            conditionItem.IsMultiGrid = false;
            conditionItem.DisplayFieldName = "USERNAME";
            conditionItem.ValueFieldName = "USERID";
            conditionItem.LanguageKey = "INSPECTIONUSER";

            conditionItem.Conditions.AddTextBox("USERIDNAME");

            conditionItem.GridColumns.AddTextBoxColumn("USERID", 150);
            conditionItem.GridColumns.AddTextBoxColumn("USERNAME", 200);

            return conditionItem;
        }

        /// <summary>
        /// @@네트워크 경로 변경
        /// GetImageFileTable - 이미지파일을 ProgressFileDialog를 이용하여 전송하기 위한 DataTemplate을 생성하는 메소드
        /// 이미지파일 서버 전송을 위한 DataTable을 반환
        /// </summary>
        /// <returns></returns>
        private DataTable GetImageFileTable()
        {
            DataTable fileTable = new DataTable("");

            fileTable.Columns.Add("FILEID");
            fileTable.Columns.Add("FILENAME");
            fileTable.Columns.Add("FILEEXT");
            fileTable.Columns.Add("FILEPATH");
            fileTable.Columns.Add("SAFEFILENAME");
            fileTable.Columns.Add("FILESIZE");
            fileTable.Columns.Add("SEQUENCE");
            fileTable.Columns.Add("LOCALFILEPATH");
            fileTable.Columns.Add("RESOURCETYPE");
            fileTable.Columns.Add("RESOURCEID");
            fileTable.Columns.Add("RESOURCEVERSION");
            fileTable.Columns.Add("PROCESSINGSTATUS");

            return fileTable;
        }
        /// <summary>
        /// 저장
        /// </summary>
        private void SaveData()
        {
            DataTable measureValueDt = grdMeasureValue.GetChangedRows();
            measureValueDt.TableName = "measuredValueList";

            DataSet rullSet = new DataSet();

            /*
            if (measureValueDt.Rows.Count == 0)
            {
                throw MessageException.Create("NoSaveData");
            }
            */

            DataTable pt = grdProductnformation.DataSource as DataTable;
            DataRow pr = pt.Rows[0];

            Control.ControlCollection ctr = flowMeasuredPicture.Controls;
            //SF_INSPECTIONFILE 생성
            //@@네트워크 경로 변경
            DataTable dtInspectionFile = GetInspectionFile(0);

            //rullSet.Tables.Add(dtInspectionFile);

            DataTable dt = manufacturingHistoryControl1.CurrentDataTable();
            DataTable menufacturingTable = new DataTable();
            menufacturingTable.TableName = "menufacturingList";
            DataRow manufacturingRow = null;

            menufacturingTable.Columns.Add(new DataColumn("ENTERPRISEID", typeof(string)));
            menufacturingTable.Columns.Add(new DataColumn("FACTORYID", typeof(string)));
            menufacturingTable.Columns.Add(new DataColumn("LOTID", typeof(string)));
            menufacturingTable.Columns.Add(new DataColumn("REQUESTNO", typeof(string)));
            menufacturingTable.Columns.Add(new DataColumn("PRODUCTDEFID", typeof(string)));
            menufacturingTable.Columns.Add(new DataColumn("PRODUCTDEFVERSION", typeof(string)));
            menufacturingTable.Columns.Add(new DataColumn("REFERENCELOTID", typeof(string)));
            menufacturingTable.Columns.Add(new DataColumn("REFERENCETXNHISTKEY", typeof(string)));

            foreach (DataRow dr in dt.Rows)
            {
                manufacturingRow = menufacturingTable.NewRow();
                manufacturingRow["ENTERPRISEID"] = pr["ENTERPRISEID"];
                manufacturingRow["FACTORYID"] = pr["PLANTID"];
                manufacturingRow["LOTID"] = pr["LOTID"];
                manufacturingRow["REQUESTNO"] = pr["REQUESTNO"];
                manufacturingRow["PRODUCTDEFID"] = pr["PRODUCTDEFID"];
                manufacturingRow["PRODUCTDEFVERSION"] = pr["PRODUCTDEFVERSION"];
                manufacturingRow["REFERENCELOTID"] = dr["LOTID"];
                manufacturingRow["REFERENCETXNHISTKEY"] = dr["LOTWORKTXNHISTKEY"];

                menufacturingTable.Rows.Add(manufacturingRow);
            }

            //rullSet.Tables.Add(menufacturingTable);

            DataTable mainTable = new DataTable();
            mainTable.TableName = "mainList";
            DataRow row = null;

            mainTable.Columns.Add(new DataColumn("REQUESTNO", typeof(string)));
            mainTable.Columns.Add(new DataColumn("ENTERPRISEID", typeof(string)));
            mainTable.Columns.Add(new DataColumn("PLANTID", typeof(string)));
            mainTable.Columns.Add(new DataColumn("USERSEQUENCE", typeof(string)));
            mainTable.Columns.Add(new DataColumn("RESOURCEID", typeof(string)));
            mainTable.Columns.Add(new DataColumn("LOTID", typeof(string)));
            mainTable.Columns.Add(new DataColumn("INSPECTIONRESULT", typeof(string)));
            mainTable.Columns.Add(new DataColumn("ISNCRPUBLISH", typeof(string)));
            mainTable.Columns.Add(new DataColumn("DEFECTCODE", typeof(string)));
            mainTable.Columns.Add(new DataColumn("ISCOMPLETION", typeof(string)));
            mainTable.Columns.Add(new DataColumn("COMMENTS", typeof(string)));
            mainTable.Columns.Add(new DataColumn("UNIT", typeof(string)));
            mainTable.Columns.Add(new DataColumn("PRODUCTDEFID", typeof(string)));
            mainTable.Columns.Add(new DataColumn("PRODUCTDEFVERSION", typeof(string)));
            mainTable.Columns.Add(new DataColumn("PROCESSSEGMENTID", typeof(string)));
            mainTable.Columns.Add(new DataColumn("PROCESSSEGMENTVERSION", typeof(string)));
            mainTable.Columns.Add(new DataColumn("AREAID", typeof(string)));
            mainTable.Columns.Add(new DataColumn("REASONCODEID", typeof(string)));
            mainTable.Columns.Add(new DataColumn("QCSEGMENTID", typeof(string)));
            mainTable.Columns.Add(new DataColumn("INSPITEMID", typeof(string)));
            mainTable.Columns.Add(new DataColumn("INSPITEMVERSION", typeof(string)));
            mainTable.Columns.Add(new DataColumn("OUTPUTDATE", typeof(string)));
            mainTable.Columns.Add(new DataColumn("PROCESSDEFID", typeof(string)));
            mainTable.Columns.Add(new DataColumn("PROCESSDEFVERSION", typeof(string)));
            mainTable.Columns.Add(new DataColumn("NCRDECISIONDEGREE", typeof(string)));
            mainTable.Columns.Add(new DataColumn("PRODUCTDEFNAME", typeof(string)));
            mainTable.Columns.Add(new DataColumn("AREANAME", typeof(string)));
            mainTable.Columns.Add(new DataColumn("DEFECTNAME", typeof(string)));
            mainTable.Columns.Add(new DataColumn("PROCESSSEGMENTNAME", typeof(string)));
            mainTable.Columns.Add(new DataColumn("INSPITEMNAME", typeof(string)));
            mainTable.Columns.Add(new DataColumn("REQUESTDATE", typeof(string)));
            mainTable.Columns.Add(new DataColumn("WORKCOUNT", typeof(string)));
            mainTable.Columns.Add(new DataColumn("VERIFICOUNT", typeof(string)));
            mainTable.Columns.Add(new DataColumn("INSPECTOR", typeof(string)));
            mainTable.Columns.Add(new DataColumn("DEFECTQTY", typeof(string)));
            mainTable.Columns.Add(new DataColumn("DEFECTRATE", typeof(string)));

            row = mainTable.NewRow();
            row["REQUESTNO"] = pr["REQUESTNO"]; // 의뢰번호
            row["ENTERPRISEID"] = pr["ENTERPRISEID"]; // 회사 ID
            row["PLANTID"] = pr["PLANTID"]; // Site ID
            row["USERSEQUENCE"] = pr["USERSEQUENCE"]; // USERSEQUENCE
            row["RESOURCEID"] = pr["LOTID"]; // RESOURCEID
            row["LOTID"] = pr["LOTID"]; // LOT ID
            row["INSPECTIONRESULT"] = cboJudgmentResult.EditValue; // 판정결과
            row["ISNCRPUBLISH"] = cboIsNCRPublish.EditValue; // NCR발행여부
            row["DEFECTCODE"] = popDefectCodeName.GetValue(); // 불량코드
            row["ISCOMPLETION"] = cboIsCompletion.EditValue; // 완료여부

            row["COMMENTS"] = memComments.EditValue; // 특이사항
            row["UNIT"] = cboUnit.EditValue; // 단위
            row["PRODUCTDEFID"] = pr["PRODUCTDEFID"]; // 제품정의 ID
            row["PRODUCTDEFVERSION"] = pr["PRODUCTDEFVERSION"]; // 제품정의 Version
            row["PROCESSSEGMENTID"] = pr["PROCESSSEGMENTID"]; // 공정 ID
            row["PROCESSSEGMENTVERSION"] = pr["PROCESSSEGMENTVERSION"]; // 공정 Version
            row["AREAID"] = pr["AREAID"]; // 작업장 ID
            row["REASONCODEID"] = "LockReliablRegNonconfirm"; // 사유코드(부적합 발생(신뢰성))
            row["QCSEGMENTID"] = txtQcSegmentId.EditValue; // 품질공정
            string[] sInspItemId = null;// 검증항목
            string sINSPITEMID = string.Empty;
            if (popInspItemId.GetValue() != null && popInspItemId.GetValue().ToString().Trim().Length > 0)
            {
                sInspItemId = popInspItemId.GetValue().ToString().Split(','); 
            }
            if (sInspItemId != null && sInspItemId.Length > 0)
            {
                sINSPITEMID = sInspItemId[0];
            }
            string[] sInspItemNm = null;// 검증항목명
            string sINSPITEMNM = string.Empty;
            if (popInspItemId.GetValue() != null && popInspItemId.GetValue().ToString().Trim().Length > 0)
            {
                sInspItemNm = popInspItemId.Text.Split(',');
            }
            if (sInspItemNm != null && sInspItemNm.Length > 0)
            {
                sINSPITEMNM = sInspItemNm[0];
            }
            //row["INSPITEMID"] = popInspItemId.GetValue(); // 검증항목
            row["INSPITEMID"] = sINSPITEMID; // 검증항목
            row["INSPITEMVERSION"] = "*"; // 검증항목
            row["OUTPUTDATE"] = pr["ISOUTPUTDATE"]; ;
            row["PROCESSDEFID"] = pr["PROCESSDEFID"];
            row["PROCESSDEFVERSION"] = pr["PROCESSDEFVERSION"];
            row["PRODUCTDEFNAME"] = pr["PRODUCTDEFNAME"];
            row["AREANAME"] = pr["AREANAME"];
            row["DEFECTNAME"] = popDefectCodeName.Text; ;
            row["PROCESSSEGMENTNAME"] = pr["PROCESSSEGMENTNAME"]; ;
            row["INSPITEMNAME"] = pr["INSPITEMNAME"]; ;
            row["REQUESTDATE"] = pr["REQUESTDATE"]; ; // 의뢰일시
            row["WORKCOUNT"] = pr["WORKCOUNT"];
            row["VERIFICOUNT"] = pr["VERIFICOUNT"];
            row["INSPECTOR"] = popInspector.Text;
            row["DEFECTQTY"] = txtDefectQty.Text; // 불량수량
            row["DEFECTRATE"] = txtSpecoutPercentage.Text; // 불량율

            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            //values.Add("INSPITEMID", popInspItemId.GetValue());
            values.Add("INSPITEMID", sINSPITEMID);

            DataTable itemDt = SqlExecuter.Query("GetReliabilityInspItemList", "10001", values);

            if (itemDt.Rows.Count == 0)
            {
                throw MessageException.Create("NoNCRStandardExceptionItem", sINSPITEMNM);
            }
            row["NCRDECISIONDEGREE"] = itemDt.Rows[0]["NCRDECISIONDEGREE"];

            mainTable.Rows.Add(row);

            //rullSet.Tables.Add(mainTable); // 기본 입력 정보
            //rullSet.Tables.Add(measureValueDt.Copy()); // 측정값 정보

            // 첨부파일 저장
            DataTable attachedFile = grdAttachedFile.GetChangedRows();
            attachedFile.TableName = "attachFileList";

            if (attachedFile.Rows.Count > 0)
            {
                int chkCount1 = 0;

                foreach (DataRow attachRow in attachedFile.Rows)
                {
                    if (attachRow["_STATE_"].ToString() == "added")
                    {
                        chkCount1++;
                    }

                    attachRow["RESOURCEID"] = pr["REQUESTNO"].ToString() + pr["LOTID"].ToString();
                    attachRow["RESOURCETYPE"] = "ReliaVerifiResultRegularAttach";
                    attachRow["RESOURCEVERSION"] = "1";
                    //row["FILEPATH"] = "InspectionMgnt/InspectionPaper";
                }

                if (chkCount1 > 0)
                {
                    grdAttachedFile.Resource.Id = pr["REQUESTNO"].ToString() + pr["LOTID"].ToString();
                    grdAttachedFile.Resource.Type = "ReliaVerifiResultRegularAttach";
                    grdAttachedFile.Resource.Version = "1";
                    //grdAttachedFile.UploadPath = "InspectionMgnt/InspectionPaper";

                    grdAttachedFile.SaveChangedFiles();
                }

                //rullSet.Tables.Add(attachedFile.Copy());
            }

            // 레포트파일 저장
            DataTable reportFile = grdReport.GetChangedRows();
            reportFile.TableName = "reportFileList";

            if (reportFile.Rows.Count > 0)
            {
                int chkCount2 = 0;

                foreach (DataRow reportRow in reportFile.Rows)
                {
                    if (reportRow["_STATE_"].ToString() == "added")
                    {
                        chkCount2++;
                    }

                    reportRow["RESOURCEID"] = pr["REQUESTNO"].ToString() + pr["LOTID"].ToString();
                    reportRow["RESOURCETYPE"] = "ReliaVerifiResultRegularReport";
                    reportRow["RESOURCEVERSION"] = "1";
                    //row["FILEPATH"] = "InspectionMgnt/InspectionPaper";
                }

                if (chkCount2 > 0)
                {
                    grdReport.Resource.Id = pr["REQUESTNO"].ToString() + pr["LOTID"].ToString(); // ResourceId
                    grdReport.Resource.Type = "ReliaVerifiResultRegularReport"; // ResourceType
                    grdReport.Resource.Version = "1"; // ResourceVersion
                    //grdReport.UploadPath = "InspectionMgnt/InspectionPaper";

                    grdReport.SaveChangedFiles();
                }

                //rullSet.Tables.Add(reportFile.Copy());
            }

            //ExecuteRule("SaveReliaVerifiResultRegular", rullSet);

            MessageWorker messageWorker = new MessageWorker("SaveReliaVerifiResultBBT");

            messageWorker.SetBody(new MessageBody()
            {
                { "inspectionFile", dtInspectionFile },
                { "menufacturingList", menufacturingTable },
                { "mainList", mainTable },
                { "measuredValueList", measureValueDt.Copy() },
                { "attachFileList", attachedFile.Copy() },
                { "reportFileList", reportFile.Copy()}
            });

            DataTable isSendEmailRS = messageWorker.Execute<DataTable>().GetResultSet();

            //@@네트워크 경로 변경----------------------------------------------------------------------
            //파일업로드 팝업 데이터 전달
            DataTable fileUploadTable = GetImageFileTable();
            int totalFileSize = 0;
            foreach (DataRow originRow in dtInspectionFile.Rows)
            {
                if (!originRow.IsNull("FILENAME") && originRow["DBVALUE_FILEID"].ToString() == "N")
                {
                    DataRow newRow = fileUploadTable.NewRow();
                    newRow["FILEID"] = originRow["FILEID"];         //Server에서 FileID를 생성하여서 가져와야 한다.
                    newRow["FILENAME"] = originRow["FILEID"];
                    newRow["FILEEXT"] = originRow.GetString("FILEEXT");      //파일업로드시에는 확장자에서 . 을 빼야 한다.
                    newRow["FILEPATH"] = originRow["FILEPATH"];// originRow["FILEFULLPATH1"];
                    newRow["SAFEFILENAME"] = originRow["FILENAME"];
                    newRow["FILESIZE"] = originRow["FILESIZE"];
                    newRow["SEQUENCE"] = originRow["SEQUENCE"];
                    newRow["LOCALFILEPATH"] = originRow["FILEFULLPATH"];
                    newRow["RESOURCETYPE"] = originRow["FILEINSPECTIONTYPE"];
                    newRow["RESOURCEID"] = originRow["FILERESOURCEID"]; //productDefID + productDefVersion + lotID + Inspitemid + Inspitemversion;
                    newRow["RESOURCEVERSION"] = "*";
                    newRow["PROCESSINGSTATUS"] = "Wait";


                    //YJKIM : 서버에서 데이타베이스를 입력하기 위해 파일아이디를 전달해야 한다.
                    totalFileSize += originRow.GetInteger("FILESIZE");

                    fileUploadTable.Rows.Add(newRow);
                }
            }

            if (fileUploadTable.Rows.Count > 0)
            {
                FileProgressDialog fileProgressDialog = new Micube.SmartMES.Commons.Controls.FileProgressDialog(fileUploadTable, UpDownType.Upload, "", totalFileSize);
                fileProgressDialog.ShowDialog(this);

                if (fileProgressDialog.DialogResult == DialogResult.Cancel)
                    throw MessageException.Create("CancelMessageToUploadFile");  //파일업로드를 취소하였습니다.

                ProgressingResult fileResult = fileProgressDialog.Result;

                if (!fileResult.IsSuccess)
                    throw MessageException.Create("FailMessageToUploadFile"); //파일업로드를 실패하였습니다.
            }
            //임시저장 삭제
            var lClipboardBitmap = dtInspectionFile.AsEnumerable().Where(r => r["FILEFULLPATH"].ToString().StartsWith("ClipboardBitmap-") == true).ToList();
            if (lClipboardBitmap != null && lClipboardBitmap.Count > 0)
            {
                foreach (DataRow drClipboardBitmap in lClipboardBitmap)
                {
                    File.Delete(drClipboardBitmap["FILEFULLPATH"].ToString());//@"C:\Temp\Data\Authors.txt"
                }

            }
            //-------------------------------------------------------------------------------------------------------

            if (isSendEmailRS != null && isSendEmailRS.Rows.Count > 0)
            {
                //DataRow drLot = mainTable.Rows[0];
                //StringBuilder _sbMailContents = new StringBuilder();
                //_sbMailContents.Append(string.Format("○ {0}({1}/{2}):", Language.Get("PRODUCTDEFNAME"), Language.Get("PRODUCTDEFID"), Language.Get("PRODUCTDEFVERSION")));
                //_sbMailContents.AppendLine(string.Format("{0}({1}/{2})", drLot["PRODUCTDEFNAME"].ToString(), drLot["PRODUCTDEFID"].ToString(), drLot["PRODUCTDEFVERSION"].ToString()));
                //_sbMailContents.Append(string.Format("○ {0}:", Language.Get("LOTID")));
                //_sbMailContents.AppendLine(string.Format("{0}", drLot["LOTID"].ToString()));

                //_sbMailContents.Append(string.Format("○ {0}:", Language.Get("STANDARDSEGMENT")));//표준공정
                //_sbMailContents.AppendLine(string.Format("{0}", drLot["PROCESSSEGMENTNAME"].ToString()));
                //_sbMailContents.Append(string.Format("○ {0}:", Language.Get("AREA")));//작업장
                //_sbMailContents.AppendLine(string.Format("{0}", drLot["AREANAME"].ToString()));
                //Dictionary<string, object> param = new Dictionary<string, object>();
                //param.Add("LOTID", drLot["LOTID"].ToString());
                //param.Add("PROCESSSEGMENTID", drLot["PROCESSSEGMENTID"].ToString());
                //param.Add("PROCESSSEGMENTVERSION", drLot["PROCESSSEGMENTVERSION"].ToString());
                //param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                //DataTable dtEquipmentList = SqlExecuter.Query("GetEquipmentListByLotAndProcessSegmentId", "10001", param);

                //if (dtEquipmentList != null && dtEquipmentList.Rows.Count > 0)
                //{
                //    _sbMailContents.Append(string.Format("○ {0}:", Language.Get("EQUIPMENTUNIT")));//설비호기
                //    _sbMailContents.AppendLine(string.Format("{0}", dtEquipmentList.Rows[0]["EQUIPMENTLIST"].ToString()));
                //}
                //_sbMailContents.Append(string.Format("○ {0}:", Language.Get("INSPECTIONRESULT")));//판정결과
                //_sbMailContents.AppendLine(string.Format("{0}", drLot["INSPECTIONRESULT"].ToString()));
                //_sbMailContents.Append(string.Format("○ {0}:", Language.Get("DEFECTNAME")));//불량명
                //_sbMailContents.AppendLine(string.Format("{0}", drLot["DEFECTNAME"].ToString()));

                //Micube.SmartMES.Commons.CommonFunction.ShowSendEmailPopup(Language.Get("NONCONFORMITYTITLE"), _sbMailContents.ToString());

                DataRow responseRow = isSendEmailRS.Rows[0];

                if (responseRow["ISSENDEMAIL"].ToString().Equals("True"))
                {
                    DataRow drLot = mainTable.Rows[0];
                    DataTable toSendDt = CommonFunction.CreateReliabilityBBTEmailDt();

                    DataRow newRow = toSendDt.NewRow();
                    newRow["PRODUCTDEFNAME"] = drLot["PRODUCTDEFNAME"];
                    newRow["PRODUCTDEFID"] = drLot["PRODUCTDEFID"];
                    newRow["PRODUCTDEFVERSION"] = drLot["PRODUCTDEFVERSION"];
                    newRow["LOTID"] = drLot["LOTID"];
                    newRow["PROCESSSEGMENTID"] = drLot["PROCESSSEGMENTID"];
                    newRow["PROCESSSEGMENTNAME"] = drLot["PROCESSSEGMENTNAME"];
                    newRow["AREAID"] = drLot["AREAID"];
                    newRow["AREANAME"] = drLot["AREANAME"];

                    Dictionary<string, object> param = new Dictionary<string, object>();
                    param.Add("LOTID", drLot["LOTID"].ToString());
                    param.Add("PROCESSSEGMENTID", drLot["PROCESSSEGMENTID"].ToString());
                    param.Add("PROCESSSEGMENTVERSION", drLot["PROCESSSEGMENTVERSION"].ToString());
                    param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                    DataTable dtEquipmentList = SqlExecuter.Query("GetEquipmentListByLotAndProcessSegmentId", "10001", param);

                    if (dtEquipmentList != null && dtEquipmentList.Rows.Count > 0)
                    {
                        newRow["EQUIPMENTLIST"] = dtEquipmentList.Rows[0]["EQUIPMENTLIST"];
                    }

                    newRow["INSPECTIONRESULT"] = drLot["INSPECTIONRESULT"];
                    newRow["DEFECTNAME"] = drLot["DEFECTNAME"];
                    newRow["REMARK"] = "";
                    newRow["USERID"] = UserInfo.Current.Id;
                    newRow["TITLE"] = Language.Get("RELIABILITYABBBTTITLE");//신뢰성(BBT) 부적합 발행
                    newRow["INSPECTION"] = "ReliabilityBBT";
                    newRow["LANGUAGETYPE"] = UserInfo.Current.LanguageType;

                    toSendDt.Rows.Add(newRow);

                    CommonFunction.ShowSendEmailPopupDataTable(toSendDt);
                }
            }
        }

        /// <summary>
        /// 검증 결과 조회
        /// </summary>
        private void SearchInspectionFile()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("P_INSPECTIONTYPE", "BBTReliaVerifiResultRegular");
            param.Add("P_RESOURCEID", CurrentDataRow["LOTID"].ToString());//품목 + LOT
            param.Add("P_PROCESSRELNO", CurrentDataRow["REQUESTNO"].ToString());
            param.Add("P_MEASURETYPE", "VerificationResult");
            _dtInspectionFile = SqlExecuter.Query("SelectInspectionFile", "10001", param);

            if (_dtInspectionFile != null && _dtInspectionFile.Rows.Count != 0)
            {
                ImageConverter converter = new ImageConverter();
                foreach (DataRow dr in _dtInspectionFile.Rows)
                {
                    // 2020.03.12-유석진-등록된 item, value 조회하는 로직 적용
                    DataTable measureValue = new DataTable();
                    measureValue.Columns.Add("MEASUREITEMID", typeof(string));
                    measureValue.Columns.Add("MEASUREVALUE", typeof(double));

                    object file = dr["FILEDATA"];
                    string filePath = dr["FILEPATH"].ToString();
                    string fileName = dr["FILENAME"].ToString();
                    string fileText = dr["FILEEXT"].ToString();
                    string title = dr["TITLE"].ToString();
                    if (file != null)
                    {
                        try
                        {
                            //Bitmap image = new Bitmap((Image)converter.ConvertFrom(file));
                            //Bitmap image = new Bitmap(GetImageFromWeb(AppConfiguration.GetString("Application.SmartDeploy.Url") + dr.GetString("WEBPATH")));

                            fileName = string.Join(".", fileName, fileText);
                            Bitmap image = CommonFunction.GetFtpImageFileToBitmap(filePath, fileName);

                            BBTMultiVerificationResultControl vr = new BBTMultiVerificationResultControl(image, fileName);
                            vr.setTitle(title);
                            vr.Tag = dr["FILEID"].ToString();
                            flowMeasuredPicture.Controls.Add(vr);

                            // 2020.03.12-유석진-등록된 item, value 조회하는 로직 적용
                            for (int i = 0; i < 10; i++)
                            {
                                if (!string.IsNullOrEmpty(dr["MEASUREITEMID" + (i + 1)].ToString()))
                                {
                                    DataRow measureDr = measureValue.NewRow();
                                    measureDr["MEASUREITEMID"] = dr["MEASUREITEMID" + (i + 1)];
                                    if (i > 0)
                                        measureDr["MEASUREVALUE"] = dr["MEASUREVALUE" + (i + 1)];
                                    else
                                        measureDr["MEASUREVALUE"] = dr["MEASUREVALUE"].ToString();

                                    measureValue.Rows.Add(measureDr);
                                }
                            }

                            vr.grdMeasureValue.DataSource = measureValue;
                        } catch(Exception ex)
                        {
                        }
                    }
                }
            }
        }
        /// <summary>
        /// //@@네트워크 경로 변경
        /// </summary>
        /// <param name="webPath"></param>
        /// <returns></returns>
        private Image GetImageFromWeb(string webPath)
        {
            System.Net.WebClient Downloader = new System.Net.WebClient();

            Stream ImageStream = Downloader.OpenRead(webPath);

            Bitmap downloadImage = Bitmap.FromStream(ImageStream) as Bitmap;

            return downloadImage;
        }
        /// <summary>
        /// 기본 입력 정보 설정
        /// </summary>
        private void SetMainInfo()
        {
            cboJudgmentResult.EditValue = CurrentDataRow["INSPECTIONRESULT"]; // 판정결과
            cboIsNCRPublish.EditValue = CurrentDataRow["ISNCRPUBLISH"]; // NCR발행여부
            popDefectCodeName.SetValue(CurrentDataRow["DEFECTCODE"]); // 불량코드
            popDefectCodeName.Text = CurrentDataRow["DEFECTCODENAME"].ToString(); // 불량명
            cboIsCompletion.EditValue = CurrentDataRow["ISCOMPLETION"]; // 완료여부
            
            txtQcSegmentId.EditValue = CurrentDataRow["QCSEGMENTID"]; // 품질공정 ID
            txtQcSegmentName.EditValue = CurrentDataRow["QCSEGMENTNAME"]; // 품질공정 명
            //popInspItemId.EditValueChanged -= PopInspItemId_EditValueChanged;
            popInspItemId.SetValue(CurrentDataRow["INSPITEMID"]); // 검증항목 ID
            popInspItemId.Text = CurrentDataRow["INSPITEMNAME"].ToString(); // 검증항목 명
            //popInspItemId.EditValueChanged += PopInspItemId_EditValueChanged;
            txtDefectQty.EditValue = CurrentDataRow["DEFECTQTY"]; // 불량수량
            //txtDefectQty.Enabled = false;
            txtSpecoutPercentage.EditValue = CurrentDataRow["SPECOUTPERCENTAGE"]; // 불량율
            txtSpecoutPercentage.Enabled = false;
            memComments.EditValue = CurrentDataRow["COMMENTS"]; // 특이사항
            txtTotalQty.EditValue = CurrentDataRow["WORKENDPCSQTY"]; // 총수량
            txtTotalQty.Enabled = false;
            txtVerifiCount.EditValue = CurrentDataRow["VERIFICOUNT"]; // 검사수량
            txtVerifiCount.Enabled = false;


            if (CurrentDataRow["DEFECTCODENAME"].ToString() != "")
            {
                popDefectCodeName.Enabled = false;            
            }

            if(CurrentDataRow["ISCOMPLETION"].ToString() == "Y")
            {
                btnSave.Visible = false;
            }

            popInspector.Text = CurrentDataRow["INSPECTOR"].ToString(); // 검사자
        }

        /// <summary>
        /// 제품 정보 조회
        /// </summary>
        private void SearchProductInformation()
        {
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("P_REQUESTNO", CurrentDataRow["REQUESTNO"]);
            values.Add("P_PLANTID", CurrentDataRow["PLANTID"]);
            values.Add("P_ISJUDGMENTRESULT", "*");
            values.Add("P_LANGUAGETYPE", Language.LanguageType);

            DataTable dt = SqlExecuter.Query("GetReliaVerifiResultLotBBTlist", "10001", values);
            dt.Columns.Add(new DataColumn("REQUESTQTY", typeof(string)));
            dt.Columns.Add(new DataColumn("REQUESTREASONS", typeof(string)));


            grdProductnformation.DataSource = dt;
        }

        /// <summary>
        /// 측정값 조회
        /// </summary>
        private void SearchMeasuredValue()
        {
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("P_REQUESTNO", CurrentDataRow["REQUESTNO"].ToString());
            values.Add("P_ENTERPRISEID", CurrentDataRow["ENTERPRISEID"].ToString());
            values.Add("P_PLANTID", CurrentDataRow["PLANTID"].ToString());
            values.Add("P_LOTID", CurrentDataRow["LOTID"].ToString());
            //values.Add("P_LOTWORKTXNHISTKEY", CurrentDataRow["TXNHISTKEY"].ToString());
            values.Add("P_MEASURETYPE", "MeasuredValue");

            DataTable dt = SqlExecuter.Query("GetQcreliabilityMeasureList", "10001", values);

            if (dt.Rows.Count > 0)
            {
                cboUnit.EditValue = dt.Rows[0]["UNIT"]; // 단위 설정
            }

            grdMeasureValue.DataSource = dt;
        }

        /// <summary>
        /// 첨부파일 조회
        /// </summary>
        private void SearchAttachFile()
        {
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("P_FILERESOURCETYPE", "ReliaVerifiResultRegularAttach");
            values.Add("P_FILERESOURCEID", CurrentDataRow["REQUESTNO"].ToString() + CurrentDataRow["LOTID"].ToString());
            values.Add("P_FILERESOURCEVERSION", "1");

            DataTable dt = SqlExecuter.Query("SelectFileInspResult", "10001", values);

            grdAttachedFile.DataSource = dt;
        }

        /// <summary>
        /// 보고서 조회
        /// </summary>
        private void SearchReport()
        {
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("P_FILERESOURCETYPE", "ReliaVerifiResultRegularReport");
            values.Add("P_FILERESOURCEID", CurrentDataRow["REQUESTNO"].ToString() + CurrentDataRow["LOTID"].ToString());
            values.Add("P_FILERESOURCEVERSION", "1");

            DataTable dt = SqlExecuter.Query("SelectFileInspResult", "10001", values);

            grdReport.DataSource = dt;
        }

        /// <summary>
        /// 단위 조회
        /// </summary>
        private void selectUnitList()
        {
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("ENTERPRISEID", "*");
            values.Add("PLANTID", "*");
            values.Add("UOMTYPE", "Unit");

            cboUnit.ValueMember = "UOMDEFID";
            cboUnit.DisplayMember = "UOMDEFNAME";
            cboUnit.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboUnit.ShowHeader = false;

            cboUnit.DataSource = SqlExecuter.Query("GetUOMList", "10001", values); ;
        }

        /// <summary>
        /// 공통코드 조회
        /// </summary>
        private void selectCodeList(string clodeCalss, object control)
        {
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("CODECLASSID", clodeCalss);

            SmartLabelComboBox lbCombo = control as SmartLabelComboBox;
            lbCombo.Control.ValueMember = "CODEID";
            lbCombo.Control.DisplayMember = "CODENAME";
            lbCombo.Control.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            lbCombo.Control.ShowHeader = false;

            lbCombo.Control.DataSource = SqlExecuter.Query("GetCodeList", "00001", values); ;
        }

        /// <summary>
        /// 불량코드 팝업 조회
        /// </summary>
        private void selectDefectCodePopup()
        {
            ConditionItemSelectPopup defectcodePopup = new ConditionItemSelectPopup();

            defectcodePopup.Id = "DEFECTCODE";
            defectcodePopup.SearchQuery = new SqlQuery("GetDefectCodeList", "10004", $"LANGUAGETYPE={UserInfo.Current.LanguageType}");
            defectcodePopup.ValueFieldName = "DEFECTCODE";
            defectcodePopup.DisplayFieldName = "DEFECTCODENAME";
            defectcodePopup.SetPopupLayout("SELECTDEFECTCODEID", PopupButtonStyles.Ok_Cancel, true, true);
            defectcodePopup.SetPopupResultCount(1);
            defectcodePopup.SetPopupLayoutForm(800, 500, FormBorderStyle.FixedToolWindow);
            defectcodePopup.SetValidationIsRequired();
            defectcodePopup.SetPopupAutoFillColumns("DEFECTCODENAME");
            defectcodePopup.SetLabel("DEFECTCODENAME");

            defectcodePopup.SetPopupApplySelection((selectedRows, dataGridRows) =>
                 {
                     // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
                     // dataGridRow : 현재 Focus가 있는 그리드의 DataRow

                     DataRow dr = selectedRows.FirstOrDefault();

                     txtQcSegmentName.EditValue = dr["QCSEGMENTNAME"].ToString();
                     txtQcSegmentId.EditValue = dr["QCSEGMENTID"].ToString();
                 });

            defectcodePopup.Conditions.AddComboBox("DEFECTCODECLASSID", new SqlQuery("GetDefectCodeClassList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "DEFECTCODECLASSNAME", "DEFECTCODECLASSID");
            defectcodePopup.Conditions.AddTextBox("TXTDEFECTCODENAME");

            defectcodePopup.GridColumns.AddTextBoxColumn("DEFECTCODE", 150)
                .SetIsReadOnly();
            defectcodePopup.GridColumns.AddTextBoxColumn("DEFECTCODENAME", 200)
                .SetIsReadOnly();
            defectcodePopup.GridColumns.AddTextBoxColumn("QCSEGMENTID", 150)
                .SetIsReadOnly();
            defectcodePopup.GridColumns.AddTextBoxColumn("QCSEGMENTNAME", 200)
                .SetIsReadOnly();

            popDefectCodeName.SelectPopupCondition = defectcodePopup;
        }

        /// <summary>
        /// 검증항목 팝업 조회
        /// </summary>
        private void selectInspitemPopup()
        {
            ConditionItemSelectPopup inspitemPopup = new ConditionItemSelectPopup();

            inspitemPopup.Id = "INSPITEMID";
            inspitemPopup.SearchQuery = new SqlQuery("GetReliabilityInspItemList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}");
            inspitemPopup.ValueFieldName = "INSPITEMID";
            inspitemPopup.DisplayFieldName = "INSPITEMNAME";
            inspitemPopup.SetPopupLayout("VERIFICATIONITEM", PopupButtonStyles.Ok_Cancel, true, true);
            inspitemPopup.SetPopupResultCount(0);
            inspitemPopup.SetPopupLayoutForm(400, 500, FormBorderStyle.FixedToolWindow);
            inspitemPopup.SetValidationIsRequired();
            inspitemPopup.SetPopupAutoFillColumns("INSPITEMNAME");
            inspitemPopup.SetLabel("VERIFICATIONITEM");
            inspitemPopup.Conditions.AddTextBox("VERIFICATIONITEM");
            //inspitemPopup.SetPopupApplySelection((selectedRows, dataGridRow) =>
            //{
            //    // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
            //    // dataGridRow : 현재 Focus가 있는 그리드의 DataRow
            //    if (selectedRows.Count() < 1)
            //    {
            //        return;
            //    }

            //    int i = 0;
            //    sINSPITEMID = string.Empty;
            //    sINSPITEMNAME = string.Empty;
            //    foreach (DataRow row in selectedRows)
            //    {
            //        if (i == 0)
            //        {
            //            sINSPITEMID = sINSPITEMID + row["INSPITEMID"].ToString();
            //            sINSPITEMNAME = sINSPITEMNAME + row["INSPITEMNAME"].ToString();
            //        }
            //        else
            //        {
            //            sINSPITEMID = sINSPITEMID + "," + row["INSPITEMID"].ToString();
            //            sINSPITEMNAME = sINSPITEMNAME + "," + row["INSPITEMNAME"].ToString();

            //        }
            //        i++;
            //    }

            //    //if (sINSPITEMID.Split(',').Length > 1)
            //    //{
            //    //    for(int j = 1; j< sINSPITEMID.Split(',').Length; j++)
            //    //    {
            //    //        string sitem = sINSPITEMID.Split(',')[j];
            //    //        if (sitem.Length > 0)
            //    //        {
            //    //            selectedRows.ToList()[0].Table.AsEnumerable().Where(r => r.Field<string>("INSPITEMID") == sitem).ToList().ForEach(s => s.Delete());
            //    //            selectedRows.ToList()[0].Table.AcceptChanges();
            //    //        }
            //    //    }
            //    //}


            //    //popInspItemId.SetValue(selectedRows.ToList()[0]["INSPITEMID"]);
            //    //popInspItemId.Text = selectedRows.ToList()[0]["INSPITEMNAME"].ToString();

            //    //Conditions.GetControl<SmartSelectPopupEdit>("popInspItemId").SetValue(selectedRows.ToList()[0]["INSPITEMID"]);
            //    //memComments.EditValue = sComments;

            //});

            inspitemPopup.GridColumns.AddTextBoxColumn("INSPITEMID", 150)
                .SetIsHidden();
            inspitemPopup.GridColumns.AddTextBoxColumn("INSPITEMNAME", 200)
                .SetIsReadOnly();

            popInspItemId.SelectPopupCondition = inspitemPopup;

            popInspItemId.EditValueChanged += PopInspItemId_EditValueChanged;
        }
        private string GetComments()
        {
            string vet = string.Empty;
            string[] sInspItemNm = sINSPITEMNAME.Split(','); // 검증항목 명
            if (sInspItemNm != null && sInspItemNm.Length > 0 && sInspItemNm[0].Trim().Length > 0)
            {
                vet = Language.Get("VERIFICATIONITEM") + " : " + string.Join(",", sInspItemNm);
            }
            return vet;
        }
        private void PopInspItemId_EditValueChanged(object sender, EventArgs e)
        {
            //string[] sInspItemId = sINSPITEMID.ToString().Split(','); // 검증항목
            //string[] sInspItemNm = sINSPITEMNAME.Split(','); // 검증항목 명

            string[] sInspItemId = popInspItemId.GetValue().ToString().Split(','); // 검증항목
            string[] sInspItemNm = popInspItemId.Text.Split(','); // 검증항목 명

            if (sInspItemId != null && sInspItemId.Length > 0 && sInspItemId[0].Trim().Length > 0)
            {
                //popInspItemId.EditValueChanged -= PopInspItemId_EditValueChanged;
                //popInspItemId.SetValue(sInspItemId[0]);
                //popInspItemId.Text = sInspItemNm[0];
                //popInspItemId.EditValueChanged += PopInspItemId_EditValueChanged;
                memComments.EditValue = Language.Get("VERIFICATIONITEM") + " : " + string.Join(",", sInspItemNm);
            }
            else
            {
                memComments.EditValue = Language.Get("VERIFICATIONITEM") + " : " + string.Join(",", sInspItemNm);
            }

            //sINSPITEMID = string.Empty;
            //sINSPITEMNAME = string.Empty;
        }

        ///////////////////////////
        /// <summary>
        /// <para>@@네트워크 경로 변경</para>
        /// <para>이미지 파일 to DataTable  변환</para>
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private DataTable GetInspectionFile(int iRowHandle)
        {
            DataRow rFocusedDataRow = null;
            rFocusedDataRow = this.grdProductnformation.View.GetDataRow(iRowHandle);
            //DataRow rFocusedDataRow = grdProductnformation.View.GetDataRow(e.PrevFocusedRowHandle);
            DataTable dtInspectionFile = null;
            //_dtInspectionFile 조회할떄 세팅된다
            dtInspectionFile = _dtInspectionFile.Clone();
            if (!dtInspectionFile.Columns.Contains("LOTID")) dtInspectionFile.Columns.Add("LOTID");
            if (!dtInspectionFile.Columns.Contains("MEASURETYPE")) dtInspectionFile.Columns.Add("MEASURETYPE");
            if (!dtInspectionFile.Columns.Contains("MEASUREVALUE")) dtInspectionFile.Columns.Add("MEASUREVALUE");
            if (!dtInspectionFile.Columns.Contains("TITLE")) dtInspectionFile.Columns.Add("TITLE");
            if (!dtInspectionFile.Columns.Contains("FILEPATH")) dtInspectionFile.Columns.Add("FILEPATH");
            if (!dtInspectionFile.Columns.Contains("LOCALFILEPATH")) dtInspectionFile.Columns.Add("LOCALFILEPATH");
            if (!dtInspectionFile.Columns.Contains("FILEINSPECTIONTYPE")) dtInspectionFile.Columns.Add("FILEINSPECTIONTYPE");
            if (!dtInspectionFile.Columns.Contains("FILERESOURCEID")) dtInspectionFile.Columns.Add("FILERESOURCEID");
            if (!dtInspectionFile.Columns.Contains("FILEFULLPATH")) dtInspectionFile.Columns.Add("FILEFULLPATH");
            if (!dtInspectionFile.Columns.Contains("DBVALUE_FILEID")) dtInspectionFile.Columns.Add("DBVALUE_FILEID");//
            if (!dtInspectionFile.Columns.Contains("SEQUENCE")) dtInspectionFile.Columns.Add("SEQUENCE");
            int iSequence = 1;
            Control.ControlCollection ctr = flowMeasuredPicture.Controls;
            foreach (Control c in ctr)
            {
                if (c.GetType() == typeof(BBTMultiVerificationResultControl))
                {
                    BBTMultiVerificationResultControl vr = c as BBTMultiVerificationResultControl;
                    string strFile = vr.strFile;
                    DataRow dr = dtInspectionFile.NewRow();
                    dr["INSPECTIONTYPE"] = "ReliaVerifiResultRegular";
                    dr["RESOURCEID"] = rFocusedDataRow["PRODUCTDEFID"].ToString();//품목
                    dr["PROCESSRELNO"] = rFocusedDataRow["REQUESTNO"].ToString();
                    dr["REQUESTNO"] = rFocusedDataRow["REQUESTNO"].ToString();
                    dr["PLANTID"] = rFocusedDataRow["PLANTID"].ToString();
                    dr["ENTERPRISEID"] = rFocusedDataRow["ENTERPRISEID"].ToString();
                    dr["PRODUCTDEFID"] = rFocusedDataRow["PRODUCTDEFID"].ToString();
                    dr["PRODUCTDEFVERSION"] = rFocusedDataRow["PRODUCTDEFVERSION"].ToString();
                    string[] sInspItemId = null;// 검증항목
                    string sINSPITEMID = string.Empty;
                    if (popInspItemId.GetValue() != null && popInspItemId.GetValue().ToString().Trim().Length > 0)
                    {
                        sInspItemId = popInspItemId.GetValue().ToString().Split(',');
                    }
                    if (sInspItemId != null && sInspItemId.Length > 0)
                    {
                        sINSPITEMID = sInspItemId[0];
                    }
                    dr["INSPITEMID"] = sINSPITEMID;
                    dr["INSPITEMVERSION"] = "*";
                    dr["LOTID"] = rFocusedDataRow["LOTID"].ToString();
                    dr["FILERESOURCEID"] = rFocusedDataRow["LOTID"].ToString();
                    dr["TITLE"] = vr.selectTitle();//CT_QCRELIABILITYMEASURE.TITLE
                    dr["MEASURETYPE"] = "VerificationResult";//CT_QCRELIABILITYMEASURE.MEASURETYPE
                                                             //dr["FILEID"] =서버로직에서 SF_INSPECTIONFILE 신규입력[fileId]를 입력 CT_QCRELIABILITYMEASURE.FILEID

                    // 2020.03.12-유석진-itemid, value 등록하는 로직 추가
                    vr.grdMeasureValue.View.CloseEditor();
                    DataTable vrDt = vr.grdMeasureValue.DataSource as DataTable;

                    for (int i = 0; i < vrDt.Rows.Count; i++)
                    {
                        DataRow vrDr = vrDt.Rows[i];

                        dr["MEASUREITEMID" + (i + 1)] = vrDr["MEASUREITEMID"];
                        if (i > 0)
                            dr["MEASUREVALUE" + (i + 1)] = vrDr["MEASUREVALUE"];
                        else
                            dr["MEASUREVALUE"] = vrDr["MEASUREVALUE"];
                    }

                    FileInfo fileInfo = new FileInfo(strFile);
                    Image image = vr.picMeasurePrinted.Image;

                    // byte 변환 변경-2020-01-29
                    Bitmap newImage = new Bitmap(image);
                    image.Dispose();
                    image = null;
                    byte[] memoryImage = (byte[])new ImageConverter().ConvertTo(newImage, typeof(byte[]));

                    vr.picMeasurePrinted.Image = newImage;
                    vr.picMeasurePrinted.Tag = newImage;

                    dr["FILESIZE"] = Math.Round(Convert.ToDouble(memoryImage.Length) / 1024);
                    dr["FILEDATA"] = memoryImage;
                    dr["FILENAME"] = fileInfo.Name;
                    dr["FILEEXT"] = fileInfo.Extension.Replace(".", "");      //파일업로드시에는 확장자에서 . 을 빼야 한다.
                    dr["FILEPATH"] = "BBTReliaVerifiResultRegular";
                    dr["FILEFULLPATH"] = strFile;
                    dr["FILEINSPECTIONTYPE"] = "BBTReliaVerifiResultRegular";
                    dr["SEQUENCE"] = (iSequence++).ToString();

                    //서버로직에서 SF_INSPECTIONFILE 신규입력[fileId]를 입력
                    //화면에서는 수정되었는지 비교를 위해 참조
                    if (fileInfo.Exists)
                    {
                        //dr["FILEID"] = "FILE-" + DateTime.Now.ToString("yyyyMMddHHmmssffff");
                        dr["FILEID"] = "FILE-" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + dr["SEQUENCE"].ToString();
                        dr["DBVALUE_FILEID"] = "N";//FILEID
                    }
                    else
                    {
                        dr["FILEID"] = vr.Tag;
                        dr["DBVALUE_FILEID"] = "Y";//FILEID
                    }

                    dtInspectionFile.Rows.Add(dr);

                }
            }
            return dtInspectionFile;
        }
        #endregion
    }
}
