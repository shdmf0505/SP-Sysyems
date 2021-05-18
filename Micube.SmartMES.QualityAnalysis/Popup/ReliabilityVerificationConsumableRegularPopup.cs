#region using

using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons;
using Micube.SmartMES.Commons.Controls;
using Micube.SmartMES.QualityAnalysis.KeyboardClipboard;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 신뢰성 검증 > 자재 검증 요청
    /// 업  무  설  명  : 자재 검증 요청
    /// 생    성    자  : 유호석
    /// 생    성    일  : 2019-08-13
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ReliabilityVerificationConsumableRegularPopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region Local Variables

        /// <summary>
        /// 팝업진입전 그리드의 Row
        /// </summary>
        public DataRow CurrentDataRow { get; set; }
        /// <summary>
        /// 그리드에 보여줄 데이터테이블
        /// </summary>
        string _sMode = string.Empty; // New/Modify
        string _sENTERPRISEID = string.Empty;
        string _sPLANTID = string.Empty;
        string _sREQUESTNO = string.Empty;
        string _sISSAMPLERECEIVE = string.Empty;
        StringBuilder _sbMailContents = new StringBuilder();
        private string[] formats = Enum.GetNames(typeof(ClipboardFormat));
        #endregion

        #region 생성자

        /// <summary>
        /// 생성자
        /// </summary>
        public ReliabilityVerificationConsumableRegularPopup()
        {
            InitializeComponent();

            InitializeEvent();
            InitializeEventResult();
        }

        public ReliabilityVerificationConsumableRegularPopup(string sMode, string sEnterprise, string sPLANTID, string sREQUESTNO, string sISSAMPLERECEIVE)
        {
            InitializeComponent();
            InitializeEvent();
            InitializeEventResult();
            _sMode = sMode;
            _sENTERPRISEID = sEnterprise;
            _sPLANTID = sPLANTID;
            _sREQUESTNO = sREQUESTNO;
            _sISSAMPLERECEIVE = sISSAMPLERECEIVE;
        }

        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            //grdQCReliabilityLot
            grdQCReliabilityLot.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Restore;
            grdQCReliabilityLot.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            //grdQCReliabilityLot.View.SetIsReadOnly();
            grdQCReliabilityLot.View.AddTextBoxColumn("REQUESTNO", 100).SetIsReadOnly().SetIsHidden();                //의뢰번호
            grdQCReliabilityLot.View.AddTextBoxColumn("ENTERPRISEID", 100).SetIsReadOnly().SetIsHidden();             //회사 ID
            grdQCReliabilityLot.View.AddTextBoxColumn("PLANTID", 100).SetIsReadOnly().SetIsHidden();                  //Site ID
            //grdQCReliabilityLot.View.AddTextBoxColumn("PRODUCTDEFID", 100).SetIsReadOnly();             //제품 정의 ID/품목
            //InitializeOSP_ProductDefIdPopup();                                               //제품 정의 ID/품목
            //자재코드
            grdQCReliabilityLot.View.AddTextBoxColumn("PRODUCTDEFID", 160).SetTextAlignment(TextAlignment.Center).SetLabel("COMPONENTITEMID").SetIsReadOnly();
            InitializeCondition_ConsumableDefPopup();
            
            //grdQCReliabilityLot.View.AddTextBoxColumn("PRODUCTDEFNAME", 180).SetIsReadOnly();
            grdQCReliabilityLot.View.AddTextBoxColumn("PRODUCTDEFVERSION", 100).SetIsHidden().SetTextAlignment(TextAlignment.Right);        //제품 정의 Version

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("P_ENTERPRISEID", _sENTERPRISEID);
            param.Add("P_PLANTID", _sPLANTID);
            param.Add("P_REQUESTNO", _sREQUESTNO);
            param.Add("P_APPROVALTYPE", "ReliabilityConsumeRegularInspection");
            DataTable dtApproval = SqlExecuter.Query("GetQCApproval", "10001", param);                     //결재 정보

            if (_sMode == "New")//
            {
                InitializeQCReliabilityLotPopup();
            }
            else
            {
                var user = dtApproval.AsEnumerable().Where(r => r["PROCESSTYPE"].ToString() == "Draft").FirstOrDefault();
                string sDraftUser = user == null ? string.Empty : user["CHARGERID"].ToString();
                if (sDraftUser != UserInfo.Current.Id   //기안자와 로그인 다를경우
                    || dtApproval.AsEnumerable().Where(r => r["PROCESSTYPE"].ToString() == "Draft" && r["APPROVALSTATE"].ToString() == "Approval").ToList().Count > 0)//기안자가 승인
                    grdQCReliabilityLot.View.AddTextBoxColumn("LOTID", 200).SetIsReadOnly();                    //LOT ID
                else
                    InitializeQCReliabilityLotPopup();
            }
            grdQCReliabilityLot.View.AddTextBoxColumn("WEEK", 100).SetIsReadOnly().SetIsHidden();
            grdQCReliabilityLot.View.AddTextBoxColumn("PROCESSSEGMENTID", 100).SetIsReadOnly().SetIsHidden();         //공정 ID
            grdQCReliabilityLot.View.AddTextBoxColumn("PROCESSSEGMENTVERSION", 100).SetIsReadOnly().SetIsHidden();    //공정 Version

            grdQCReliabilityLot.View.AddTextBoxColumn("PROCESSDEFID", 100).SetIsReadOnly().SetIsHidden();         //라우팅 ID
            grdQCReliabilityLot.View.AddTextBoxColumn("PROCESSDEFVERSION", 100).SetIsReadOnly().SetIsHidden();    //라우팅 Version
            grdQCReliabilityLot.View.AddTextBoxColumn("USERSEQUENCE", 100).SetIsReadOnly().SetIsHidden();    //공정수순

            grdQCReliabilityLot.View.AddTextBoxColumn("AREAID", 100).SetIsReadOnly().SetIsHidden();                   //작업장 ID
            grdQCReliabilityLot.View.AddTextBoxColumn("OUTPUTDATE", 100).SetIsReadOnly().SetIsHidden();               //출력일시
            grdQCReliabilityLot.View.AddSpinEditColumn("REQUESTQTY", 100).SetIsHidden();               //의뢰수량
            grdQCReliabilityLot.View.AddTextBoxColumn("INSPITEMID", 100).SetIsReadOnly().SetIsHidden();       //검사항목아이템
            Initialize_InspItemPopup();
            grdQCReliabilityLot.View.AddTextBoxColumn("INSPITEMVERSION", 100).SetIsReadOnly().SetIsHidden();       //검사항목아이템
            //grdQCReliabilityLot.View.AddSpinEditColumn("INSPITEMNAME", 100);       //검사항목아이템
            grdQCReliabilityLot.View.AddTextBoxColumn("PURPOSE", 100).SetIsReadOnly().SetIsHidden();                  //의뢰목적
            grdQCReliabilityLot.View.AddTextBoxColumn("DETAILS", 100).SetIsReadOnly().SetIsHidden();                  //의뢰상세내용
            grdQCReliabilityLot.View.AddTextBoxColumn("ISPOSTPROCESS", 100).SetIsReadOnly().SetIsHidden();            //검증후 처리여부
            grdQCReliabilityLot.View.AddTextBoxColumn("DESCRIPTION", 100).SetIsReadOnly().SetIsHidden();              //설명

            grdQCReliabilityLot.View.PopulateColumns();

            fpcReport.grdFileList.View.Columns["FILENAME"].Width = 200;
            fpcReport.grdFileList.View.Columns["FILEEXT"].Width = 100;
            fpcReport.grdFileList.View.Columns["FILESIZE"].Width = 100;
            fpcReport.grdFileList.View.Columns["COMMENTS"].Width = 400;

            ctrlApproval.grdApproval.View.Columns["PROCESSTYPE"].Width = 80;// 절차구분
            ctrlApproval.grdApproval.View.Columns["CHARGETYPE"].Width = 75;//역할구분
            ctrlApproval.grdApproval.View.Columns["USERNAME"].Width = 75;//담당자
            ctrlApproval.grdApproval.View.Columns["REJECTCOMMENTS"].Width = 400;//반려사유
        }
        /// <summary>
		/// 자재코드 - 팝업형 조회조건 생성
		/// </summary>
		private void InitializeCondition_ConsumableDefPopup()
        {
            var productDefId = grdQCReliabilityLot.View.AddSelectPopupColumn("PRODUCTDEFNAME", 220, new SqlQuery("GetReliabilityVerificationConsumableDefList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "PRODUCTDEFNAME")
                                              .SetPopupLayout("SELECTCONSUMABLEDEFID", PopupButtonStyles.Ok_Cancel, true, false)
                                              .SetPopupResultCount(1)
                                              .SetPopupLayoutForm(800, 800, FormBorderStyle.FixedToolWindow)
                                              .SetRelationIds("PLANTID")
                                              .SetLabel("CONSUMABLEDEFNAME")
                                              .SetValidationKeyColumn()
                                              .SetDefault("", "")
                                              .SetPopupQueryPopup((DataRow currentrow) =>
                                              {
                                                  if (string.IsNullOrWhiteSpace(_sPLANTID))
                                                  {
                                                      this.ShowMessage("NoSelectSite");
                                                      return false;
                                                  }

                                                  return true;
                                              })
                                               .SetPopupApplySelection((selectedRows, dataGridRow) =>
                                               {
                                                   // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
                                                   // dataGridRow : 현재 Focus가 있는 그리드의 DataRow

                                                   foreach (DataRow row in selectedRows)
                                                   {
                                                       dataGridRow["PRODUCTDEFVERSION"] = row["PRODUCTDEFVERSION"].ToString();
                                                       //dataGridRow["PRODUCTDEFNAME"] = row["PRODUCTDEFNAME"].ToString();
                                                       dataGridRow["PRODUCTDEFID"] = row["PRODUCTDEFID"].ToString();
                                                       
                                                   }

                                                   if (selectedRows.Count() == 0)
                                                   {
                                                       dataGridRow["PRODUCTDEFID"] = "";
                                                       dataGridRow["PRODUCTDEFVERSION"] = "";
                                                   }
                                               })
                                               ;


            //var consumableDefPopup = Conditions.AddSelectPopup("PRODUCTDEFID", new SqlQuery("GetConsumableDefList", "10002", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "PRODUCTDEFVERSION")
            //    .SetPopupLayout("SELECTCONSUMABLEDEFID", PopupButtonStyles.Ok_Cancel, true, true)
            //    .SetPopupResultCount(0)
            //    .SetPopupLayoutForm(800, 800, FormBorderStyle.FixedToolWindow)
            //    .SetPopupAutoFillColumns("CONSUMABLEDEFNAME")
            //    .SetPosition(1.3)
            //    .SetLabel("MATERIALDEF");

            productDefId.Conditions.AddComboBox("CONSUMABLECLASSID", new SqlQuery("GetConsumableclassListByCsm", "10001"), "CONSUMABLECLASSNAME", "CONSUMABLECLASSID").SetEmptyItem();
            productDefId.Conditions.AddTextBox("CONSUMABLEDEF");

            productDefId.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            productDefId.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 80);
            productDefId.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 360);
        }
        /// <summary>
        /// 검증항목을 선택하는 팝업
        /// </summary>
        private void Initialize_InspItemPopup()
        {
            //팝업 컬럼 설정
            //var inspItem = grdQCReliabilityLot.View.AddSelectPopupColumn("INSPITEMID", 200, new SqlQuery("GetReliabilityVerificationInspItemList", "10001", $"P_PLANTID={UserInfo.Current.Plant}", $"P_ENTERPRISEID={UserInfo.Current.Enterprise}"), "INSPITEMVERSION")
            var inspItem = grdQCReliabilityLot.View.AddSelectPopupColumn("INSPITEMNAME", 200, new SqlQuery("GetReliabilityVerificationInspItemList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "INSPITEMNAME")
                                              .SetPopupLayout("INSPITEMID", PopupButtonStyles.Ok_Cancel, true, false)
                                              .SetPopupResultCount(0)
                                              .SetPopupLayoutForm(600, 700, FormBorderStyle.FixedToolWindow)
                                              .SetLabel("INSPITEMNAME")
                                              .SetValidationKeyColumn()
                                              .SetDefault("", "")
                                              //.SetPopupQueryPopup((DataRow currentrow) =>
                                              //{
                                              //    if (string.IsNullOrWhiteSpace(_sPLANTID))
                                              //    {
                                              //        this.ShowMessage("NoSelectSite");
                                              //        return false;
                                              //    }

                                              //    return true;
                                              //})
                                               .SetPopupApplySelection((selectedRows, dataGridRow) =>
                                               {
                                                   // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
                                                   // dataGridRow : 현재 Focus가 있는 그리드의 DataRow
                                                   string sINSPITEMVERSION = "";
                                                   string sINSPITEMID = "";
                                                   foreach (DataRow row in selectedRows)
                                                   {
                                                       sINSPITEMVERSION = (sINSPITEMVERSION.Length == 0 ? sINSPITEMVERSION + "" : sINSPITEMVERSION + ",") + row["INSPITEMVERSION"].ToString();
                                                       sINSPITEMID = (sINSPITEMID.Length == 0 ? sINSPITEMID + "" : sINSPITEMID + ",") + row["INSPITEMID"].ToString();
                                                       //dataGridRow["INSPITEMVERSION"] = (dataGridRow["INSPITEMVERSION"].ToString().Length == 0 ? "" : "|") + row["INSPITEMVERSION"].ToString();
                                                       //dataGridRow["INSPITEMID"] = (dataGridRow["INSPITEMID"].ToString().Length == 0 ? "" : "|") + row["INSPITEMID"].ToString();
                                                   }
                                                   dataGridRow["INSPITEMVERSION"] = sINSPITEMVERSION;
                                                   dataGridRow["INSPITEMID"] = sINSPITEMID;
                                                   if (selectedRows.Count() == 0)
                                                   {
                                                       dataGridRow["INSPITEMID"] = "";
                                                   }
                                               })
                                               ;

            inspItem.Conditions.AddTextBox("TXTINSPITEMID").SetLabel("INSPITEMNAME");

            // 팝업 그리드
            // 품목코드
            inspItem.GridColumns.AddTextBoxColumn("inspitemclassNAME", 170);//검증항목
            inspItem.GridColumns.AddTextBoxColumn("INSPITEMNAME", 100);//검증종류
            inspItem.GridColumns.AddTextBoxColumn("INSPITEMID", 150);
        }
        /// <summary>
        ///  Lot No 선택하는 팝업
        /// </summary>
        private void InitializeQCReliabilityLotPopup()
        {
            //팝업 컬럼 설정
            //var conditionLotId = grdQCReliabilityLot.View.AddSelectPopupColumn("LOTID", new SqlQuery("GetLotIdList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
            var conditionLotId = grdQCReliabilityLot.View.AddSelectPopupColumn("LOTID", 200, new SqlQuery("GetLotIdListByReliabilityVerificationConsumableRequest", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                              .SetPopupLayout("SELECTLOTNO", PopupButtonStyles.Ok_Cancel, true, false)
                                              .SetPopupResultCount(1)
                                              .SetPopupLayoutForm(1200, 600, FormBorderStyle.FixedToolWindow)
                                              //.SetPopupAutoFillColumns("PRODUCTDEFNAME")
                                              .SetRelationIds("PLANTID")
                                              .SetLabel("LOTID")
                                              .SetPopupQueryPopup((DataRow currentrow) =>
                                              {
                                                  if (string.IsNullOrWhiteSpace(currentrow.ToStringNullToEmpty("PLANTID")))
                                                  {
                                                      this.ShowMessage("NoSelectSite");
                                                      return false;
                                                  }

                                                  if (string.IsNullOrWhiteSpace(currentrow.ToStringNullToEmpty("PRODUCTDEFID")))
                                                  {
                                                      this.ShowMessage(MessageBoxButtons.OK, "CheckRequireReliabilityConsum", Language.Get("ITEMCODE")); //메세지 
                                                      return false;
                                                  }

                                                  return true;
                                              })
                                               .SetPopupApplySelection((selectedRows, dataGridRow) =>
                                               {
                                                   foreach (DataRow row in selectedRows)
                                                   {
                                                       dataGridRow["WEEK"] = "*";
                                                       dataGridRow["PROCESSDEFID"] = "*";//라우팅 ID
                                                       dataGridRow["PROCESSDEFVERSION"] = "*";//라우팅 Version
                                                       dataGridRow["USERSEQUENCE"] = "*";//공정수순
                                                   }

                                                   if (selectedRows.Count() == 0)
                                                   {
                                                       dataGridRow["WEEK"] = "";
                                                       dataGridRow["PROCESSDEFID"] = "";
                                                       dataGridRow["PROCESSDEFVERSION"] = "";
                                                       dataGridRow["USERSEQUENCE"] = "";
                                                   }
                                               })
                                               ;

            conditionLotId.Conditions.AddTextBox("LOTID");

            conditionLotId.Conditions.AddTextBox("PLANTID")
                                   .SetPopupDefaultByGridColumnId("PLANTID")
                                   .SetIsHidden();
            conditionLotId.Conditions.AddTextBox("PRODUCTDEFID")
                       .SetPopupDefaultByGridColumnId("PRODUCTDEFID")
                       .SetIsHidden();

            conditionLotId.Conditions.AddTextBox("PRODUCTDEFVERSION")
           .SetPopupDefaultByGridColumnId("PRODUCTDEFVERSION")
           .SetIsHidden();

            // 팝업 그리드에서 보여줄 컬럼 정의
            // Lot No
            conditionLotId.GridColumns.AddTextBoxColumn("LOTID", 180);
            // 품목코드
            conditionLotId.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 130);
            // 품목버전
            conditionLotId.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 70);
            // 품목명
            conditionLotId.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 220);
            // Site
            conditionLotId.GridColumns.AddComboBoxColumn("PLANTID", 70, new SqlQuery("GetPlantList", "10002", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "PLANTNAME", "PLANTID");
            // 작업장
            conditionLotId.GridColumns.AddComboBoxColumn("AREAID", 90, new SqlQuery("GetAreaList", "10003", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "AREANAME", "AREAID");
            // 자재 LOT 상태
            //conditionLotId.GridColumns.AddTextBoxColumn("CONSUMABLESTATE", 90).SetIsHidden();
            // 최초 생성 수량
            conditionLotId.GridColumns.AddSpinEditColumn("CREATEDQTY", 70);
            //주차
            conditionLotId.GridColumns.AddSpinEditColumn("WEEK", 70);
            // 현재 수량
            conditionLotId.GridColumns.AddSpinEditColumn("CONSUMABLELOTQTY", 70);
            // 유효일
            //conditionLotId.GridColumns.AddDateEditColumn("EXPIREDDATE", 100).SetDisplayFormat("yyyy-MM-dd").SetIsHidden();
            // 보류상태
            conditionLotId.GridColumns.AddTextBoxColumn("ISHOLD", 70).SetTextAlignment(TextAlignment.Center);
            // 공급사 LOT ID
            //conditionLotId.GridColumns.AddTextBoxColumn("VENDORLOTID", 70).SetIsHidden();
            // 설명
            conditionLotId.GridColumns.AddTextBoxColumn("DESCRIPTION", 120);
        }
        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            this.Load += ReliabilityVerificationConsumableRegularPopup_Load;
            grdQCReliabilityLot.View.FocusedRowChanged += View_FocusedRowChanged;

            btnClose.Click += BtnClose_Click;
            //팝업저장버튼을 클릭시 이벤트
            btnSave.Click += BtnSave_Click;

            btnAddQCLot.Click += BtnAddQCLot_Click;
            btnDeleteQCLot.Click += BtnDeleteQCLot_Click;

            //btnADDAPPROVAL.Click += BtnADDAPPROVAL_Click;

            this.txtPURPOSE.EditValueChanged += new System.EventHandler(this.txtPURPOSE_EditValueChanged);
            this.txtDETAILS.EditValueChanged += new System.EventHandler(this.txtDETAILS_EditValueChanged);

            // 이미지 저장 이벤트
            btnImageSave.Click += (s, e) =>
            {
                var enumerator = flowMeasuredPicture.Controls.GetEnumerator();
                var i = 0;

                while (enumerator.MoveNext())
                {
                    VerificationResultControl vr = enumerator.Current as VerificationResultControl;

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
                            VerificationResultControl vr = enumerator.Current as VerificationResultControl;

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
        #region 그리드이벤트
        /// <summary>
        /// fpcReport
        /// 저장할 파일의 Key생성에 사용되는 컬럼목록 추가 해줌
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected void OnRowChanged(object sender, DataRowChangeEventArgs args)
        {
            if (args.Action == DataRowAction.Add)
            {
                if (!args.Row.Table.Columns.Contains("ENTERPRISEID")) args.Row.Table.Columns.Add("ENTERPRISEID");
                if (!args.Row.Table.Columns.Contains("PLANTID")) args.Row.Table.Columns.Add("PLANTID");

                DataRow dr = args.Row;
                dr["ENTERPRISEID"] = _sENTERPRISEID;
                dr["PLANTID"] = _sPLANTID;
            }
        }
        #endregion

        private void BtnAddQCLot_Click(object sender, EventArgs e)
        {
            grdQCReliabilityLot.View.CloseEditor();
            DataTable dtQCReliabilityLot = grdQCReliabilityLot.DataSource as DataTable;
            DataRow newRow = dtQCReliabilityLot.NewRow();
            newRow["PLANTID"] = _sPLANTID;
            newRow["ENTERPRISEID"] = _sENTERPRISEID;
            dtQCReliabilityLot.Rows.Add(newRow);
        }
        /// <summary>
        /// 제품정보 삭제
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDeleteQCLot_Click(object sender, EventArgs e)
        {
            ValidationCheckFile(grdQCReliabilityLot);

            grdQCReliabilityLot.View.DeleteCheckedRows();
        }
        
        private void txtPURPOSE_EditValueChanged(object sender, EventArgs e)
        {
            if (txtPURPOSE.EditValue.ToString().Length > 0)
            {
                if (grdQCReliabilityLot.View.FocusedRowHandle < 0)
                {
                    ShowMessage("NoSeletedLot"); // LOT을 선택하여 주십시오
                    txtPURPOSE.Text = string.Empty;
                    txtPURPOSE.Refresh();
                    return;
                }
                else
                {
                    DataRow row = this.grdQCReliabilityLot.View.GetFocusedDataRow();
                    if (row != null)
                    {
                        if(row["PURPOSE"].ToString() != txtPURPOSE.EditValue.ToString())
                            row["PURPOSE"] = txtPURPOSE.EditValue;
                    }
                    else
                    {
                        ShowMessage("NoSeletedLot"); // LOT을 선택하여 주십시오
                        txtPURPOSE.Text = string.Empty;
                        txtPURPOSE.Refresh();
                        return;
                    }
                }
            }

        }
        private void txtDETAILS_EditValueChanged(object sender, EventArgs e)
        {
            if (txtDETAILS.EditValue.ToString().Length > 0)
            {
                if (grdQCReliabilityLot.View.FocusedRowHandle < 0)
                {
                    ShowMessage("NoSeletedLot"); // LOT을 선택하여 주십시오
                    txtDETAILS.Text = string.Empty;
                    txtDETAILS.Refresh();
                    return;
                }
                else
                {
                    DataRow row = this.grdQCReliabilityLot.View.GetFocusedDataRow();
                    if (row != null)
                    {
                        if(row["DETAILS"].ToString() != txtDETAILS.EditValue.ToString())
                            row["DETAILS"] = txtDETAILS.EditValue;
                    }
                    else
                    {
                        ShowMessage("NoSeletedLot"); // LOT을 선택하여 주십시오
                        txtDETAILS.Text = string.Empty;
                        txtDETAILS.Refresh();
                        return;
                    }
                }
            }

        }
        private void cboISPOSTPROCESS_EditValueChanged(object sender, EventArgs e)
        {
            if (grdQCReliabilityLot.View.FocusedRowHandle < 0 && cboISPOSTPROCESS.EditValue.ToString() != string.Empty)
            {
                ShowMessage("NoSeletedLot"); // LOT을 선택하여 주십시오
                cboISPOSTPROCESS.EditValue = string.Empty;
                cboISPOSTPROCESS.Refresh();
                return;
            }
            else
            {
                DataRow row = this.grdQCReliabilityLot.View.GetFocusedDataRow();
                if (row != null)
                {
                    if(row["ISPOSTPROCESS"].ToString() != cboISPOSTPROCESS.EditValue.ToString())
                        row["ISPOSTPROCESS"] = cboISPOSTPROCESS.EditValue;
                }
                else if(cboISPOSTPROCESS.EditValue.ToString() != string.Empty)
                {
                    ShowMessage("NoSeletedLot"); // LOT을 선택하여 주십시오
                    cboISPOSTPROCESS.EditValue = string.Empty;
                    cboISPOSTPROCESS.Refresh();
                    return;
                }
            }
        }
        /// <summary>
        /// 저장버튼을 클릭했을때 검사 결과를 저장하는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            DialogResult result = System.Windows.Forms.DialogResult.No;
            ctrlApproval.grdApproval.View.CloseEditor();
            grdQCReliabilityLot.View.CloseEditor();
            fpcReport.grdFileList.View.CloseEditor();
            //Reportfile DataTable
            DataTable fileChanged = fpcReport.GetChangedRows();
            grdQCReliabilityLot.View.CheckValidation();

            DataTable dtReliabilityRequest = new DataTable();                                               //신뢰성의뢰
            DataTable dtApproval = (ctrlApproval.grdApproval.DataSource as DataTable).Copy();               //결재 정보
            DataTable dtReliabilityLot = grdQCReliabilityLot.GetChangedRows();                              //LOT정보
            DataTable dtFile = fpcReport.GetChangedRows();                                                  //파일

            //승인그룹에 한명이상 존재 해야 한다.
            if (dtApproval.Rows.Count > 0 && dtApproval.AsEnumerable().Where(r => r["PROCESSTYPE"].ToString() == "Approval").ToList().Count == 0)
            {
                ShowMessage("ApprovalApprobalCheck");//승인자가 등록되지 않았습니다.
                return;
            }

            //결재 유효성 검사
            ctrlApproval.ValidateApproval();

            if (!dtReliabilityRequest.Columns.Contains("REQUESTNO")) dtReliabilityRequest.Columns.Add("REQUESTNO");                             //의뢰번호
            if (!dtReliabilityRequest.Columns.Contains("ENTERPRISEID")) dtReliabilityRequest.Columns.Add("ENTERPRISEID");                       //회사 ID
            if (!dtReliabilityRequest.Columns.Contains("PLANTID")) dtReliabilityRequest.Columns.Add("PLANTID");                                 //Site ID
            if (!dtReliabilityRequest.Columns.Contains("RELIABILITYTYPE")) dtReliabilityRequest.Columns.Add("RELIABILITYTYPE");                 //신뢰성 구분
            if (!dtReliabilityRequest.Columns.Contains("REQUESTTYPE")) dtReliabilityRequest.Columns.Add("REQUESTTYPE");                         //의뢰구분
            if (!dtReliabilityRequest.Columns.Contains("REQUESTDATE")) dtReliabilityRequest.Columns.Add("REQUESTDATE");                         //의뢰일
            if (!dtReliabilityRequest.Columns.Contains("SAMPLERECEIVEDATE")) dtReliabilityRequest.Columns.Add("SAMPLERECEIVEDATE");             //시료접수일
            if (!dtReliabilityRequest.Columns.Contains("ISSAMPLERECEIVE")) dtReliabilityRequest.Columns.Add("ISSAMPLERECEIVE");                 //시료접수여부
            if (!dtReliabilityRequest.Columns.Contains("REQUESTOR")) dtReliabilityRequest.Columns.Add("REQUESTOR");                             //의뢰자
            if (!dtReliabilityRequest.Columns.Contains("REQUESTDEPT")) dtReliabilityRequest.Columns.Add("REQUESTDEPT");                         //의뢰부서
            if (!dtReliabilityRequest.Columns.Contains("REQUESTORJOBPOSITION")) dtReliabilityRequest.Columns.Add("REQUESTORJOBPOSITION");       //의뢰자 직책
            if (!dtReliabilityRequest.Columns.Contains("REQUESTEXTENSIONNO")) dtReliabilityRequest.Columns.Add("REQUESTEXTENSIONNO");           //의뢰자 내선번호
            if (!dtReliabilityRequest.Columns.Contains("REQUESTMOBILENO")) dtReliabilityRequest.Columns.Add("REQUESTMOBILENO");                 //의뢰자 휴대폰번호
            if (!dtReliabilityRequest.Columns.Contains("COMMENTS")) dtReliabilityRequest.Columns.Add("COMMENTS");                               //특이사항
            if (!dtReliabilityRequest.Columns.Contains("MEASURECOMPLETIONDATE")) dtReliabilityRequest.Columns.Add("MEASURECOMPLETIONDATE");     //계측완료일시
            if (!dtReliabilityRequest.Columns.Contains("PARENTREQUESTNO")) dtReliabilityRequest.Columns.Add("PARENTREQUESTNO");                 //원본 의뢰번호
            if (!dtReliabilityRequest.Columns.Contains("ISRECEIPT")) dtReliabilityRequest.Columns.Add("ISRECEIPT");                             //접수여부
            if (!dtReliabilityRequest.Columns.Contains("DESCRIPTION")) dtReliabilityRequest.Columns.Add("DESCRIPTION");                         //설명
            if (!dtReliabilityRequest.Columns.Contains("_STATE_")) dtReliabilityRequest.Columns.Add("_STATE_");

            DataRow drReliabilityRequest = dtReliabilityRequest.NewRow();

            //drReliabilityRequest["REQUESTNO"] =                                                   //의뢰번호
            drReliabilityRequest["ENTERPRISEID"] = _sENTERPRISEID;                                  //회사 ID
            drReliabilityRequest["PLANTID"] = _sPLANTID;                                            //Site ID
            //drReliabilityRequest["RELIABILITYTYPE"] = "NonRegular";                                 //신뢰성 구분
            drReliabilityRequest["RELIABILITYTYPE"] = "ReliabilityConsumeRegularInspection";          //신뢰성 구분
            drReliabilityRequest["REQUESTTYPE"] = cboRequestClass.EditValue.ToString();             //의뢰구분
                                                                                                    //drReliabilityRequest["REQUESTDATE"] = reqData.setRequestdate(Today());                //의뢰일
            drReliabilityRequest["SAMPLERECEIVEDATE"] = dtpSAMPLERECEIVEDATE.Text;                                          //시료접수일
            drReliabilityRequest["REQUESTOR"] = popupREQUESTOR.GetValue();                                //의뢰자
            drReliabilityRequest["REQUESTDEPT"] = txtREQUESTDEPT.Text;                              //의뢰부서
            drReliabilityRequest["REQUESTORJOBPOSITION"] = txtREQUESTORJOBPOSITION.Text;            //의뢰자 직책
            drReliabilityRequest["REQUESTEXTENSIONNO"] = txtREQUESTEXTENSIONNO.Text;                //의뢰자 내선번호
            drReliabilityRequest["REQUESTMOBILENO"] = txtREQUESTMOBILENO.Text;                      //의뢰자 휴대폰번호

            string sRequstDate = dtpREQUESTDATE.Text;
            string sSampleReceiveDate = dtpSAMPLERECEIVEDATE.Text;

            DateTime deRequstDate = new DateTime();
            DateTime deSampleReceiveDate = new DateTime();
            if (DateTime.TryParse(sSampleReceiveDate, out deSampleReceiveDate) && DateTime.TryParse(sRequstDate, out deRequstDate))
            {
                if (DateTime.Compare(deSampleReceiveDate, deRequstDate) < 0)//시료접수일 > 요청일
                {
                    ShowMessage("ReliabilityVeriSampleReceiveDate");//시료접수일은 요청일 이후 일자입니다
                    drReliabilityRequest["SAMPLERECEIVEDATE"] = DBNull.Value;
                    dtpSAMPLERECEIVEDATE.Text = string.Empty;
                    return;
                }

                //if (DateTime.Compare(deSampleReceiveDate, Convert.ToDateTime(DateTime.Now.ToShortDateString())) < 0)//시료접수일 > 오늘
                //{
                //    ShowMessage("ReliabilityVeriSampleReceiveDate2");//시료접수일은 오늘 이후 일자입니다
                //    drReliabilityRequest["SAMPLERECEIVEDATE"] = DBNull.Value;
                //    dtpSAMPLERECEIVEDATE.Text = string.Empty;
                //    return;
                //}
            }

            dtReliabilityRequest.Rows.Add(drReliabilityRequest);

            if (_sMode == "New")
            {
                drReliabilityRequest["_STATE_"] = "added";
            }
            else if (dtReliabilityRequest.Rows.Count > 0)
            {
                drReliabilityRequest["_STATE_"] = "modified";
                drReliabilityRequest["REQUESTNO"] = _sREQUESTNO;
            }

            //if (dtApproval.Rows.Count == 0
            //    && dtReliabilityLot.Rows.Count == 0
            //    && dtReliabilitySegmentRef.Rows.Count == 0
            //    && dtFile.Rows.Count == 0)
            //{
            //    // 저장할 데이터가 존재하지 않습니다.
            //    throw MessageException.Create("NoSaveData");
            //}

            foreach (DataRow dr in dtReliabilityLot.Rows)
            {
                //동일한 키가 존재 하면 서버단에서 기존 데이터를 삭제하기 떄문에 중복 체크를 해야 한다.
                var dList = from r in (grdQCReliabilityLot.DataSource as DataTable).AsEnumerable()
                            group r by new { PRODUCTDEFID = r.Field<string>("PRODUCTDEFID") == null ? "" : r.Field<string>("PRODUCTDEFID").ToString(), LOTID = r.Field<string>("LOTID") == null ? "" : r.Field<string>("LOTID") } into g
                            select g;
                dList = dList.Where(item => item.Count() > 1);

                if (dList.ToList().Count > 0)
                {
                    this.ShowMessage(MessageBoxButtons.OK, "CheckDupliProductDefId", Language.Get("ITEMCODE")); //동일 품목과 LOTID가 존재 합니다. 
                    return;
                }

                if (dr["_STATE_"].ToString() == "added" && dr["PRODUCTDEFID"].ToString() == string.Empty)//품목 없음
                {
                    //this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", Language.Get("ITEMCODE")); //메세지 
                    this.ShowMessage(MessageBoxButtons.OK, "CheckRequireReliabilityConsum"); //자재코드를 입력 하지 않았습니다.
                    return;
                }

                if (!dtReliabilityLot.Columns.Contains("OriginalLotID")) dtReliabilityLot.Columns.Add("OriginalLotID");
                DataColumn dc = dtReliabilityLot.Columns["LOTID"];

                if (dr["_STATE_"].ToString() == "modified")
                {
                    dr["OriginalLotID"] = dr[dc, DataRowVersion.Original];
                }
                else
                {
                    dr["OriginalLotID"] = dr["LOTID"];
                }

                if (dr["INSPITEMID"] == DBNull.Value)
                {
                    dr["INSPITEMID"] = "*";
                    dr["INSPITEMVERSION"] = "*";
                }

                if (dr["PURPOSE"].ToString().Length > 160)
                {
                    dr["PURPOSE"] = dr["PURPOSE"].ToString().Substring(0, 160);
                }

                if (dr["DETAILS"].ToString().Length > 2500)
                {
                    dr["DETAILS"] = dr["DETAILS"].ToString().Substring(0, 2500);
                }
            }

            result = this.ShowMessage(MessageBoxButtons.YesNo, "InfoPopupSave");//변경 내용을 저장하시겠습니까??

            if (result == System.Windows.Forms.DialogResult.No) return;

            try
            {
                this.ShowWaitArea();
                btnSave.Enabled = false;
                btnClose.Enabled = false;

                dtReliabilityRequest.TableName = "list1";           //신뢰성의뢰
                dtApproval.TableName = "list2";
                dtReliabilityLot.TableName = "list3";               //LOT정보
                dtFile.TableName = "list5";

                if (_sMode == "Modify")
                {
                    if (dtReliabilityLot.Rows.Count > 0 && !dtReliabilityLot.Columns.Contains("REQUESTNO")) dtReliabilityLot.Columns.Add("REQUESTNO");
                    if (dtFile.Rows.Count > 0 && !dtFile.Columns.Contains("REQUESTNO")) dtFile.Columns.Add("REQUESTNO");

                    foreach (DataRow dr in dtReliabilityLot.Rows)
                    {
                        dr["REQUESTNO"] = _sREQUESTNO;
                    }

                    foreach (DataRow dr in dtFile.Rows)
                    {
                        dr["REQUESTNO"] = _sREQUESTNO;
                    }
                }

                if (dtApproval.Rows.Count > 0 && !dtApproval.Columns.Contains("REQUESTNO")) dtApproval.Columns.Add("REQUESTNO");
                if (dtApproval.Rows.Count > 0 && !dtApproval.Columns.Contains("_STATE_")) dtApproval.Columns.Add("_STATE_");//자바 에서 모두 삭제후 INSERT 처리 하기때문에
                foreach (DataRow dr in dtApproval.Rows)
                {
                    dr["REQUESTNO"] = _sREQUESTNO;
                    dr["_STATE_"] = "added";//자바 에서 모두 삭제후 INSERT 처리 하기때문에
                    dr["APPROVALTYPE"] = "ReliabilityConsumeRegularInspection";
                }

                if (dtFile.Rows.Count > 0 && !dtFile.Columns.Contains("RESOURCETYPE")) dtFile.Columns.Add("RESOURCETYPE");
                foreach (DataRow dr in dtFile.Rows)
                {
                    dr["RESOURCETYPE"] = "ReliabilityConsumeRegularInspection";
                }

                DataSet rullSet = new DataSet();
                rullSet.Tables.Add(dtReliabilityRequest);
                rullSet.Tables.Add(dtApproval);
                rullSet.Tables.Add(dtReliabilityLot);
                rullSet.Tables.Add(dtFile);
                ExecuteRule("SaveReliabilityVerificationRequestNonRegularRecept", rullSet);

                //메일 보내기
                ctrlApproval.ApprovalMail("신뢰성 의뢰내용 확인 및 결재요청", _sbMailContents.ToString());
                // 반려할 경우 기안자에게 반려 메일 보냄-2020.01.16
                ctrlApproval.ApprovalMail("신뢰성 의뢰 반려", _sbMailContents.ToString());
                ShowMessage("SuccessSave");
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
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        /// <summary>
        /// 그리드의 Row Click시 조회
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            focusedRowChanged();
        }
        /// <summary>
        /// 닫기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// Form Road시 초기화
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReliabilityVerificationConsumableRegularPopup_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            dtpREQUESTDATE.Enabled = false;
            dtpSAMPLERECEIVEDATE.Enabled = false;
            InitializeGrid();
            InitializeGridResult();
            fpcReport.LanguageKey = "ATTACHEDFILE";
            fpcAttach.LanguageKey = "FILEATTACH";
            fpcReportResult.LanguageKey = "REPORT";
            fpcReport.ButtonVisible = true;
            fpcAttach.ButtonVisible = true;
            fpcReportResult.ButtonVisible = true;

            // 시료접수가 안됬으면 결과 등록 버튼 비활성화
            if (string.IsNullOrEmpty(_sISSAMPLERECEIVE))
            {
                btnSaveResult.Enabled = false;
            } else
            {
                btnSaveResult.Enabled = true;
            }

            //제조일력이 추가 될때 LotID입력
            popupREQUESTOR.SelectPopupCondition = UserPopup();

            Dictionary<string, object> param = new Dictionary<string, object>
            {
                { "ENTERPRISEID", UserInfo.Current.Enterprise },
                { "LANGUAGETYPE", UserInfo.Current.LanguageType },
                { "CODECLASSID", "RequestClass"}
            };

            cboRequestClass.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboRequestClass.ValueMember = "CODEID";
            cboRequestClass.DisplayMember = "CODENAME";
            //cboRequestClass.EditValue = "Y";
            cboRequestClass.DataSource = SqlExecuter.Query("GetCodeList", "00001", param); 
            cboRequestClass.ShowHeader = false;
            cboRequestClass.ItemIndex = 0;

            param = new Dictionary<string, object>
            {
                { "ENTERPRISEID", UserInfo.Current.Enterprise },
                { "LANGUAGETYPE", UserInfo.Current.LanguageType },
                { "CODECLASSID", "ReliabilityProcessingStatus"}
            };

            cboISPOSTPROCESS.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboISPOSTPROCESS.ValueMember = "CODEID";
            cboISPOSTPROCESS.DisplayMember = "CODENAME";
            //cboISPOSTPROCESS.EditValue = "Y";
            cboISPOSTPROCESS.DataSource = SqlExecuter.Query("GetCodeList", "00001", param);
            cboISPOSTPROCESS.ShowHeader = false;

            cboISPOSTPROCESS.EditValueChanged += new EventHandler(cboISPOSTPROCESS_EditValueChanged);

            Search();
            SearchResult();
            focusedRowChanged();

            selectUnitList(); // 단위

            //Control[] controls = GetAllControls(this);
            //foreach (Control ctr in controls)
            //{
            //    if (ctr.TabStop)
            //        ctr.TabStop = false;
            //}
        }
        private Control[] GetAllControls(Control containerControl)
        {
            List<Control> allControls = new List<Control>();
            Queue<Control.ControlCollection> queue = new Queue<Control.ControlCollection>();
            queue.Enqueue(containerControl.Controls);

            while (queue.Count > 0)
            {
                Control.ControlCollection controls = (Control.ControlCollection)queue.Dequeue();

                if (controls == null || controls.Count == 0) continue;

                foreach (Control control in controls)
                {
                    allControls.Add(control);
                    queue.Enqueue(control.Controls);
                }
            }

            return allControls.ToArray();
        }
        #endregion

        #region Private Function
        /// <summary>
        /// 체크한 항목이 있는지 확인
        /// </summary>
        private void ValidationCheckFile(SmartBandedGrid grd)
        {
            DataTable selectedFiles = grd.View.GetCheckedRows();

            if (selectedFiles.Rows.Count < 1)
            {
                throw MessageException.Create("GridNoChecked");
            }
        }
        private DialogResult checkSave()
        {
            if (grdQCReliabilityResultLot.View.FocusedRowHandle < 0) return System.Windows.Forms.DialogResult.No;

            DialogResult result = System.Windows.Forms.DialogResult.No;

            result = this.ShowMessage(MessageBoxButtons.YesNo, "InfoSaveAndSearch");//편집중 데이터를 저장 하시겠습니까?

            if (result == System.Windows.Forms.DialogResult.No)
                return result;
            else
                BtnSave_Click(null, null);

            return result;

        }
        /// <summary>
        /// 그리드의 포커스 행 변경시 행의 작업목록의 내용을 매핑처리 함.
        /// </summary>
        private void focusedRowChanged()
        {
            //포커스 행 체크 
            if (grdQCReliabilityLot.View.FocusedRowHandle < 0) return;

            var row = grdQCReliabilityLot.View.GetDataRow(grdQCReliabilityLot.View.FocusedRowHandle);

            txtPURPOSE.Text = string.Empty;
            txtDETAILS.Text = string.Empty;

            if (txtPURPOSE.Text != row["PURPOSE"].ToString())
                txtPURPOSE.Text = row["PURPOSE"].ToString();
            if (txtDETAILS.Text != row["DETAILS"].ToString())
                txtDETAILS.Text = row["DETAILS"].ToString();
            cboISPOSTPROCESS.EditValue = row["ISPOSTPROCESS"].ToString();
        }

        private void CheckAllCtrl(bool bEnable)
        {
            txtREQUESTDEPT.Enabled = bEnable;
            txtREQUESTEXTENSIONNO.Enabled = bEnable;
            txtREQUESTORJOBPOSITION.Enabled = bEnable;
            txtREQUESTMOBILENO.Enabled = bEnable;
            popupREQUESTOR.Enabled = bEnable;
            cboRequestClass.Enabled = bEnable;
            btnAddQCLot.Enabled = bEnable;
            btnDeleteQCLot.Enabled = bEnable;
            //txtPURPOSE.Enabled = bEnable;
            //txtDETAILS.Enabled = bEnable;
            cboISPOSTPROCESS.Enabled = bEnable;
            //btnSave.Enabled = bEnable;

            //ctrlApproval.btnADDAPPROVAL.Enabled = bEnable;

            fpcReport.btnFileAdd.Enabled = bEnable;
            fpcReport.btnFileDelete.Enabled = bEnable;
        }
        /// <summary>
        /// 검사자 팝업
        /// </summary>
        /// <returns></returns>
        private ConditionItemSelectPopup UserPopup()
        {
            ConditionItemSelectPopup popup = new ConditionItemSelectPopup();

            popup.SetPopupLayoutForm(565, 600, FormBorderStyle.FixedDialog);
            popup.SetPopupLayout("USERLIST", PopupButtonStyles.Ok_Cancel, true, true);
            popup.Id = "USERID";
            popup.SearchQuery = new SqlQuery("GetUserApproval", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}");
            popup.IsMultiGrid = false;
            popup.DisplayFieldName = "USERNAME";
            popup.ValueFieldName = "CHARGERID";
            popup.LanguageKey = "USER";
            popup.IsRequired = true;
            popup.SetPopupApplySelection((selectedRows, dataGridRow) =>
            {
                // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
                // dataGridRow : 현재 Focus가 있는 그리드의 DataRow
                if (selectedRows.Count() < 1)
                {
                    return;
                }

                foreach (DataRow row in selectedRows)
                {
                    //dataGridRow["DEFECTCODE"] = row["DEFECTCODE"].ToString();
                    //dataGridRow["DEFECTNAME"] = row["DEFECTNAME"].ToString();

                    txtREQUESTDEPT.Text = row["DEPARTMENT"].ToString();
                    txtREQUESTORJOBPOSITION.Text = row["POSITION"].ToString();
                    txtREQUESTMOBILENO.Text = row["CELLPHONENUMBER"].ToString();
                }
            });

            popup.Conditions.AddTextBox("P_USERNAME").SetLabel("NAME");

            popup.GridColumns.AddTextBoxColumn("CHARGERID", 100);
            popup.GridColumns.AddTextBoxColumn("USERNAME", 100);
            popup.GridColumns.AddTextBoxColumn("DEPARTMENT", 100);
            popup.GridColumns.AddTextBoxColumn("POSITION", 60);
            popup.GridColumns.AddTextBoxColumn("CELLPHONENUMBER", 100);

            return popup;
        }
        private DataTable GetApprovalStateAll()
        {
            Dictionary<string, object> values = new Dictionary<string, object>();
            return SqlExecuter.Query("GetApprovalAllStateByReliabilityVerificationRequest", "10001", values);
        }
        private DataTable GetApprovalState(string processType)
        {
            //-기안(Draft)
            //- 검토(Review)
            //- 승인(Approval)
            //- 수신(Receiving)
            string codeClassID = string.Empty;
            switch (processType)
            {
                case "Review":
                case "Approval":
                    codeClassID = "ApprovalSettleState";
                    break;
                case "Receiving":
                    codeClassID = "ReceivingSettleState";
                    break;
                default:
                    codeClassID = "DraftSettleState";
                    break;
            }
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("CODECLASSID", codeClassID);
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            return SqlExecuter.Query("GetCodeList", "00001", values);
        }

        
        //popup이 닫힐때 그리드를 재조회 하는 이벤트 (팝업결과 저장 후 재조회)
        private async void Popup_FormClosed()
        {
            //await OnSearchAsync();
        }
        /// <summary>
        /// 협력업체 정기등록 조회
        /// </summary>
        private void Search()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("P_ENTERPRISEID", _sENTERPRISEID);
            param.Add("P_PLANTID", _sPLANTID);
            param.Add("P_REQUESTNO", _sREQUESTNO);
            param.Add("P_RELIABILITYTYPE", "ReliabilityConsumeRegularInspection");
            param.Add("P_APPROVALTYPE", "ReliabilityConsumeRegularInspection");

            if (_sMode == "Modify")
            {
                DataTable dtReliabilityRequest = SqlExecuter.Query("GetReliabilityVerificationRequestNonRegularRgisterList", "10001", param);           //신뢰성의뢰
                DataTable dtApproval = SqlExecuter.Query("GetQCApproval", "10001", param);                     //결재 정보
                DataTable dtReliabilityLot = SqlExecuter.Query("GetQCReliabilityConsumableLot", "10001", param);               //LOT정보
                //DataTable dtReliabilitySegmentRef = SqlExecuter.Query("GetQCReliabilitySegmentRef", "10001", param);

                Dictionary<string, object> fileValues = new Dictionary<string, object>();
                //이미지 파일Search parameter
                fileValues.Add("P_FILERESOURCETYPE", "ReliabilityConsumeRegularInspection");
                fileValues.Add("P_FILERESOURCEID", _sREQUESTNO);
                fileValues.Add("P_FILERESOURCEVERSION", "0");

                DataTable dtFile = SqlExecuter.Query("SelectFileInspResult", "10001", fileValues);

                ctrlApproval.SetApproval = dtApproval;
                
                ctrlApproval.Approval_Enable();

                fpcReport.DataSource = dtFile;
                grdQCReliabilityLot.DataSource = dtReliabilityLot;

                if (dtReliabilityRequest != null && dtReliabilityRequest.Rows.Count > 0)
                {
                    DataRow drRequest = dtReliabilityRequest.Rows[0];
                    DateTime deREQUESTDATE = new DateTime();
                    if (DateTime.TryParse(drRequest["REQUESTDATE"].ToString(), out deREQUESTDATE))
                    {
                        dtpREQUESTDATE.Text = deREQUESTDATE.ToShortDateString();
                    }
                    DateTime deSAMPLERECEIVEDATE = new DateTime();
                    if (DateTime.TryParse(drRequest["SAMPLERECEIVEDATE"].ToString(), out deSAMPLERECEIVEDATE))
                        dtpSAMPLERECEIVEDATE.Text = deSAMPLERECEIVEDATE.ToShortDateString();

                    txtREQUESTDEPT.Text = drRequest["REQUESTDEPT"].ToString();
                    txtREQUESTEXTENSIONNO.Text = drRequest["REQUESTEXTENSIONNO"].ToString();
                    txtREQUESTORJOBPOSITION.Text = drRequest["REQUESTORJOBPOSITION"].ToString();
                    txtREQUESTMOBILENO.Text = drRequest["REQUESTMOBILENO"].ToString();
                    popupREQUESTOR.SetValue(drRequest["REQUESTOR"].ToString());
                    popupREQUESTOR.Text = drRequest["USERNAME"].ToString();
                    cboRequestClass.EditValue = drRequest["REQUESTTYPE"].ToString();

                    _sbMailContents = new StringBuilder();
                    _sbMailContents.Append("<p style='text-align:left;'>" + string.Format("○ {0}:", Language.Get("REQUESTDATETIME")));
                    _sbMailContents.AppendLine(drRequest["REQUESTDATE"].ToString() + "</p><br>");
                    _sbMailContents.Append("<p style='text-align:left;'>" + string.Format("○ {0}/{1}/{2}:", Language.Get("DEPARTMENT"), Language.Get("DUTY"), Language.Get("REQUESTTYPE")));
                    _sbMailContents.AppendLine(drRequest["REQUESTDEPT"].ToString() + "/" + drRequest["REQUESTDEPT"].ToString() + "/" + cboRequestClass.Text + "</p><br>");
                    _sbMailContents.Append("<p style='text-align:left;'>" + string.Format("○ {0}/{1}/{2}:", Language.Get("OSPREQUESTUSER"), Language.Get("EXTENSIONNUMBER"), Language.Get("PHONENUMBER")));
                    _sbMailContents.AppendLine(drRequest["REQUESTOR"].ToString() + "/" + drRequest["REQUESTEXTENSIONNO"].ToString() + "/" + drRequest["REQUESTMOBILENO"].ToString() + "</p><br>");

                    DataTable dtMailLot = SqlExecuter.Query("GetRequestNonRegularQCReliabilityLotMail", "10001", param);               //검증항목별 LOT
                    if (dtMailLot != null && dtMailLot.Rows.Count > 0)
                    {
                        foreach (DataRow drMailLot in dtMailLot.Rows)
                        {
                            _sbMailContents.Append("<p style='text-align:left;'>" + string.Format("○ {0}/{1}/{2}/{3}:", Language.Get("ITEMNAME"), Language.Get("PRODUCTREVISIONINPUT"), Language.Get("LOT"), Language.Get("INSPECTIONITEM")));
                            _sbMailContents.AppendLine(drMailLot["PRODUCTDEFNAME"].ToString() + "/" + drMailLot["PRODUCTDEFVERSION"].ToString() + "/" + drMailLot["LOTID"].ToString() + "/" + drMailLot["INSPITEMNAME"].ToString() + "</p><br>");
                        }
                    }

                    if (dtReliabilityLot != null && dtReliabilityLot.Rows.Count > 0)
                    {
                        DataRow drPURPOSE = dtReliabilityLot.Rows[0];
                        _sbMailContents.Append("<p style='text-align:left;'>" + string.Format("○ {0}", Language.Get("REQUESTPURPOSE")));
                        _sbMailContents.AppendLine(drPURPOSE["PURPOSE"].ToString() + "</p><br>");
                        _sbMailContents.Append("<p style='text-align:left;'>" + string.Format("○ {0}:", Language.Get("REQUESTDETAILS")));
                        _sbMailContents.AppendLine(drPURPOSE["DETAILS"].ToString() + "</p><br>");
                        _sbMailContents.Append("<p style='text-align:left;'>" + string.Format("○ {0}:", Language.Get("ISPOSTPROCESS")));
                        _sbMailContents.AppendLine(drPURPOSE["ISPOSTPROCESS_NAME"].ToString() + "</p><br>");
                    }


                    //APPROVALSTATE : 승인(Approval), 회수(Reclamation), 반려(Companion)
                    //PROCESSTYPE : 기안(Draft), 검토(Review), 승인(Approval), 수신(Receiving)
                    if (dtApproval.AsEnumerable().Where(r => r["PROCESSTYPE"].ToString() == "Draft" && r["APPROVALSTATE"].ToString() == "Approval").ToList().Count > 0)
                    {
                        CheckAllCtrl(false); //btnSave.Enabled = false;//기안자가 승인했으면
                    }
                    else
                    {
                        var user = dtApproval.AsEnumerable().Where(r => r["PROCESSTYPE"].ToString() == "Draft").FirstOrDefault();
                        string sDraftUser = user == null ? string.Empty : user["CHARGERID"].ToString();
                        string sAPPROVALSTATE = user == null ? string.Empty : user["APPROVALSTATE"].ToString();
                        if (sDraftUser != UserInfo.Current.Id)
                            CheckAllCtrl(false);// btnSave.Enabled = false;//로그인과 기안자가 다르면
                        else
                            CheckAllCtrl(true); //btnSave.Enabled = true;
                    }
                    //결재선에 로그인한 사람이 포함되어 있어야 결재자 버튼을 활성화 할 수 있다.
                    //if(dtApproval.AsEnumerable().Where(r => r["CHARGERID"].ToString() == UserInfo.Current.Id).ToList().Count > 0)
                    //    ctrlApproval.btnADDAPPROVAL.Enabled = true;
                    //else
                    //    ctrlApproval.btnADDAPPROVAL.Enabled = false;
                    //ctrlApproval.Approval_Enable();  

                    //APPROVALSTATE : 승인(Approval), 회수(Reclamation), 반려(Companion)
                    //PROCESSTYPE : 기안(Draft), 검토(Review), 승인(Approval), 수신(Receiving)
                    //모두 승인이 되어야  시료접수 일자를 등록 할 수있다.
                    if (dtApproval.Rows.Count > 0 && (dtApproval.AsEnumerable().Where(r => r["APPROVALSTATE"].ToString() == "Approval").ToList().Count == dtApproval.Rows.Count))
                    {
                        if (drRequest["SAMPLERECEIVEDATE"] == DBNull.Value)
                        {
                            dtpSAMPLERECEIVEDATE.Enabled = true;
                            if ((this.Owner as ReliabilityVerificationConsumableRegular).btnFlag.Enabled == true)
                                btnSave.Enabled = true;
                        }
                        else
                        {
                            //시료접수가 되면 수정불가능
                            dtpSAMPLERECEIVEDATE.Enabled = false;
                            if ((this.Owner as ReliabilityVerificationConsumableRegular).btnFlag.Enabled == true)
                                btnSave.Enabled = false;
                        }
                    }
                    else
                    {
                        //모든 결재가 승인않되면 시료접수일을 등록 할수 없다.
                        dtpSAMPLERECEIVEDATE.Enabled = false;
                        if ((this.Owner as ReliabilityVerificationConsumableRegular).btnFlag.Enabled == true)
                            btnSave.Enabled = true;
                    }
                }
            }
            else
            {
                DataTable dtApproval = SqlExecuter.Query("GetQCApproval", "10001", param);                     //결재 정보
                ctrlApproval.SetApproval = dtApproval;
            }
        }

        #endregion



        #region 유효성 검사
        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        //protected override void OnValidateContent()
        //{
        //    base.OnValidateContent();

        //    grdAuditManageregist.View.CheckValidation();

        //    DataTable changed = grdAuditManageregist.GetChangedRows();

        //    if (changed.Rows.Count == 0)
        //    {
        //        // 저장할 데이터가 존재하지 않습니다.
        //        throw MessageException.Create("NoSaveData");
        //    }
        //}
        #endregion


        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@결과

        #region Local Variables
        DataTable _dtInspectionFile;
        DataTable _dtReliabilityRequest;
        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGridResult()
        {
            //grdQCReliabilityLot
            grdQCReliabilityResultLot.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Restore;
            //grdQCReliabilityLot.View.SetIsReadOnly();
            grdQCReliabilityResultLot.View.AddTextBoxColumn("REQUESTNO", 100).SetIsReadOnly().SetIsHidden();                //의뢰번호
            grdQCReliabilityResultLot.View.AddTextBoxColumn("ENTERPRISEID", 100).SetIsReadOnly().SetIsHidden();             //회사 ID
            grdQCReliabilityResultLot.View.AddTextBoxColumn("PLANTID", 100).SetIsReadOnly().SetIsHidden();                  //Site ID
            grdQCReliabilityResultLot.View.AddTextBoxColumn("PRODUCTDEFID", 130).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();             //제품 정의 ID/품목
            grdQCReliabilityResultLot.View.AddTextBoxColumn("PRODUCTDEFNAME", 200).SetIsReadOnly();
            grdQCReliabilityResultLot.View.AddTextBoxColumn("PRODUCTDEFVERSION", 100).SetTextAlignment(TextAlignment.Right).SetIsReadOnly().SetIsHidden();        //제품 정의 Version
            grdQCReliabilityResultLot.View.AddTextBoxColumn("LOTID", 200).SetIsReadOnly();                    //LOT ID
            grdQCReliabilityResultLot.View.AddTextBoxColumn("WEEK", 50).SetTextAlignment(TextAlignment.Center).SetIsReadOnly().SetIsHidden();
            grdQCReliabilityResultLot.View.AddTextBoxColumn("PROCESSSEGMENTID", 100).SetIsReadOnly().SetIsHidden();         //공정 ID
            grdQCReliabilityResultLot.View.AddTextBoxColumn("PROCESSSEGMENTVERSION", 100).SetIsReadOnly().SetIsHidden();    //공정 Version
            grdQCReliabilityResultLot.View.AddTextBoxColumn("AREAID", 100).SetIsReadOnly().SetIsHidden();                   //작업장 ID
            grdQCReliabilityResultLot.View.AddTextBoxColumn("OUTPUTDATE", 100).SetIsReadOnly().SetIsHidden();               //출력일시
            grdQCReliabilityResultLot.View.AddSpinEditColumn("REQUESTQTY", 60).SetIsReadOnly().SetIsHidden();                           //의뢰수량
            grdQCReliabilityResultLot.View.AddTextBoxColumn("INSPITEMID", 100).SetIsHidden();                                              //검증항목
            grdQCReliabilityResultLot.View.AddTextBoxColumn("INSPITEMNAME", 100).SetIsReadOnly();                                              //검증항목
            grdQCReliabilityResultLot.View.AddComboBoxColumn("INSPECTIONRESULT", 60, new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "CODECLASSID=OKNG")).SetTextAlignment(TextAlignment.Center);// 판정결과 공통코드(OKNG)
            //grdQCReliabilityResultLot.View.AddComboBoxColumn("ISNCRPUBLISH", 100, new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "CODECLASSID=YesNo")).SetTextAlignment(TextAlignment.Center);// NCR 발행여부(Y/N) 공통코드(YesNo)
            //DEFECTCODE
            //DEFECTNAME
            //DefectCodePopup(); // 불량코드
            //grdQCReliabilityResultLot.View.AddTextBoxColumn("DEFECTNAME", 100).SetIsReadOnly();
            //grdQCReliabilityResultLot.View.AddTextBoxColumn("QCSEGMENTNAME", 100).SetIsReadOnly();
            //grdQCReliabilityResultLot.View.AddTextBoxColumn("QCSEGMENTID", 100).SetIsReadOnly();
            grdQCReliabilityResultLot.View.AddComboBoxColumn("ISCOMPLETION", 60, new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "CODECLASSID=YesNo")).SetTextAlignment(TextAlignment.Center);// 완료여부(Y/N) 공통코드(YesNo)
            grdQCReliabilityResultLot.View.PopulateColumns();

            grdMeasurValue.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Restore | GridButtonItem.Export;
            grdMeasurValue.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdMeasurValue.View.AddTextBoxColumn("REQUESTNO", 100).SetIsReadOnly().SetIsHidden();                //의뢰번호
            grdMeasurValue.View.AddTextBoxColumn("ENTERPRISEID", 100).SetIsReadOnly().SetIsHidden();             //회사 ID
            grdMeasurValue.View.AddTextBoxColumn("PLANTID", 100).SetIsReadOnly().SetIsHidden();                  //Site ID
            grdMeasurValue.View.AddTextBoxColumn("PRODUCTDEFID", 100).SetIsReadOnly().SetIsHidden();             //제품 정의 ID/품목
            grdMeasurValue.View.AddTextBoxColumn("PRODUCTDEFVERSION", 100).SetTextAlignment(TextAlignment.Right).SetIsReadOnly().SetIsHidden();        //제품 정의 Version
            grdMeasurValue.View.AddTextBoxColumn("LOTID", 200).SetIsReadOnly().SetIsHidden();                    //LOT ID
            grdMeasurValue.View.AddTextBoxColumn("INSPITEMID", 100).SetTextAlignment(TextAlignment.Center).SetIsReadOnly().SetIsHidden();//검사 항목 ID
            grdMeasurValue.View.AddTextBoxColumn("INSPITEMVERSION", 100).SetIsReadOnly().SetIsHidden();     //검사 항목 Version       
            grdMeasurValue.View.AddTextBoxColumn("MEASURETYPE", 100).SetIsReadOnly().SetIsHidden();    //측정구분
            grdMeasurValue.View.AddTextBoxColumn("SEQUENCE", 100).SetIsReadOnly().SetIsHidden();                   //순서
            grdMeasurValue.View.AddTextBoxColumn("TITLE", 190);                                         //제목
            grdMeasurValue.View.AddSpinEditColumn("MEASUREVALUE", 90).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{#,###,##0.####}", MaskTypes.Numeric); // 측정값
            grdMeasurValue.View.AddTextBoxColumn("COMMENTS", 380);                                      //내용
            grdMeasurValue.View.AddTextBoxColumn("UNIT", 100).SetIsReadOnly().SetIsHidden();            //단위

            grdMeasurValue.View.PopulateColumns();

            RepositoryItemTextEdit edit = new RepositoryItemTextEdit();
            edit.Mask.EditMask = "#,###,##0.####";
            edit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            edit.Mask.UseMaskAsDisplayFormat = true;

            grdMeasurValue.View.Columns["MEASUREVALUE"].ColumnEdit = edit;

            fpcAttach.grdFileList.View.Columns["FILENAME"].Width = 100;
            fpcAttach.grdFileList.View.Columns["FILEEXT"].Width = 50;
            fpcAttach.grdFileList.View.Columns["FILESIZE"].Width = 60;
            fpcAttach.grdFileList.View.Columns["COMMENTS"].Width = 200;

            fpcReportResult.grdFileList.View.Columns["FILENAME"].Width = 100;
            fpcReportResult.grdFileList.View.Columns["FILEEXT"].Width = 50;
            fpcReportResult.grdFileList.View.Columns["FILESIZE"].Width = 60;
            fpcReportResult.grdFileList.View.Columns["COMMENTS"].Width = 200;
        }
        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEventResult()
        {
            grdQCReliabilityResultLot.View.FocusedRowChanged += ResultView_FocusedRowChanged;
            grdQCReliabilityResultLot.View.ShowingEditor += View_CancelChange;
            btnCloseResult.Click += BtnCloseResult_Click;
            //팝업저장버튼을 클릭시 이벤트
            btnSaveResult.Click += BtnSaveResult_Click;
            this.KeyDown += ReliabilityVerificationConsumableRegular_KeyDown;
            //this.KeyUp += ReliabilityVerificationConsumableRegular_KeyUp;
            btnAddMeasurValue.Click += BtnAddMeasurValue_Click;
            btnDeleteMeasurValue.Click += BtnDeleteMeasurValue_Click;
        }
        //private void ReliabilityVerificationConsumableRegular_KeyUp(object sender, KeyEventArgs e)
        //{
        //    flowMeasuredPicture.
        //}
        /// <summary>        
        /// 붙여넣기 호출
        /// </summary>
        private void ReliabilityVerificationConsumableRegular_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.V && e.Modifiers == Keys.Control)
            {
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

                flowMeasuredPicture.Focus();
                if ((ClipboardFormat)format == ClipboardFormat.FileDrop)
                {
                    string[] files = (string[])data;
                    //foreach (string file_name in files)
                    for (int i = files.Length - 1; i >= 0; i--)
                    {
                        Bitmap image = new Bitmap(files[i]);
                        VerificationResultControl vr = new VerificationResultControl(image, files[i]);
                        flowMeasuredPicture.Controls.Add(vr);
                    }
                }
                else if ((ClipboardFormat)format == ClipboardFormat.Bitmap)
                {
                    Bitmap image = (Bitmap)data;
                    string sBitmapName = "ClipboardBitmap-" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".png";
                    VerificationResultControl vr = new VerificationResultControl(image, sBitmapName);
                    flowMeasuredPicture.Controls.Add(vr);
                    image.Save(sBitmapName, ImageFormat.Png);
                }

            }
        }
        #region 그리드이벤트
        
        /// <summary>
        /// fpcReport
        /// 저장할 파일의 Key생성에 사용되는 컬럼목록 추가 해줌
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected void OnRowChangedResult(object sender, DataRowChangeEventArgs args)
        {
            if (args.Action == DataRowAction.Add)
            {
                //if (!args.Row.Table.Columns.Contains("REQUESTNO")) args.Row.Table.Columns.Add("REQUESTNO");
                if (!args.Row.Table.Columns.Contains("RESOURCEID")) args.Row.Table.Columns.Add("RESOURCEID");

                DataRow dr = args.Row;
                //dr["REQUESTNO"] = _sREQUESTNO;
                DataRow rowFocused = grdQCReliabilityResultLot.View.GetFocusedDataRow();
                //_sREQUESTNO + row["PRODUCTDEFID"].ToString() + row["PRODUCTDEFVERSION"].ToString() + row["LOTID"].ToString() + row["INSPITEMID"].ToString() + row["INSPITEMVERSION"].ToString());//요청번호,품목,버전,lot,검증item,버전
                dr["RESOURCEID"] = _sREQUESTNO + rowFocused["PRODUCTDEFID"].ToString() + rowFocused["PRODUCTDEFVERSION"].ToString() + rowFocused["LOTID"].ToString() + rowFocused["INSPITEMID"].ToString() + rowFocused["INSPITEMVERSION"].ToString();
            }
        }
        #endregion

        private void BtnAddMeasurValue_Click(object sender, EventArgs e)
        {
            DataTable dtMeasurValue = grdMeasurValue.DataSource as DataTable;
            DataRow newRow = dtMeasurValue.NewRow();
            newRow["PLANTID"] = _sPLANTID;
            newRow["ENTERPRISEID"] = _sENTERPRISEID;
            newRow["REQUESTNO"] = _sREQUESTNO;
            newRow["INSPITEMID"] = "*";
            newRow["INSPITEMVERSION"] = "*";
            newRow["MEASURETYPE"] = "MeasuredValue";

            if (cboUnit.Text.Length == 0)
            {
                ShowMessage("CheckUnitSelection");//단위선택을 해주세요. 
                return;
            }
            newRow["UNIT"] = cboUnit.Text;
            DataRow row = this.grdQCReliabilityResultLot.View.GetFocusedDataRow();
            if (row != null)
            {
                newRow["PRODUCTDEFID"] = row["PRODUCTDEFID"];//제품 정의 ID
                newRow["PRODUCTDEFVERSION"] = row["PRODUCTDEFVERSION"];//제품 정의 Version
                newRow["LOTID"] = row["LOTID"];
                newRow["INSPITEMID"] = row["INSPITEMID"];
                newRow["INSPITEMVERSION"] = row["INSPITEMVERSION"];
                dtMeasurValue.Rows.Add(newRow);
            }
            else
            {
                ShowMessage("CheckLotFocusedDataRow");//제품정보를 선택해주세요.. 
                return;
            }
        }
        /// <summary>
        /// 제품정보 삭제
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDeleteMeasurValue_Click(object sender, EventArgs e)
        {
            ValidationCheckFile(grdMeasurValue);

            grdMeasurValue.View.DeleteCheckedRows();
        }
        ///////////////////////////
        private DataTable GetInspectionFile(int iRowHandle)
        {
            DataRow rFocusedDataRow = grdQCReliabilityResultLot.View.GetDataRow(iRowHandle);
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
                if (c.GetType() == typeof(VerificationResultControl))
                {
                    VerificationResultControl vr = c as VerificationResultControl;
                    string strFile = vr.strFile;
                    DataRow dr = dtInspectionFile.NewRow();
                    dr["INSPECTIONTYPE"] = "ReliaVerifiResultNonRegular";
                    dr["RESOURCEID"] = rFocusedDataRow["PRODUCTDEFID"].ToString();//품목
                    dr["PROCESSRELNO"] = rFocusedDataRow["REQUESTNO"].ToString();
                    dr["REQUESTNO"] = rFocusedDataRow["REQUESTNO"].ToString();
                    dr["PLANTID"] = _sPLANTID;
                    dr["ENTERPRISEID"] = _sENTERPRISEID;
                    dr["PRODUCTDEFID"] = rFocusedDataRow["PRODUCTDEFID"].ToString();
                    dr["PRODUCTDEFVERSION"] = rFocusedDataRow["PRODUCTDEFVERSION"].ToString();
                    dr["INSPITEMID"] = rFocusedDataRow["INSPITEMID"].ToString();
                    dr["INSPITEMVERSION"] = rFocusedDataRow["INSPITEMVERSION"].ToString();
                    dr["LOTID"] = rFocusedDataRow["LOTID"].ToString();
                    dr["FILERESOURCEID"] = rFocusedDataRow["PRODUCTDEFID"].ToString() + rFocusedDataRow["PRODUCTDEFVERSION"].ToString() + rFocusedDataRow["LOTID"].ToString() + rFocusedDataRow["INSPITEMID"].ToString() + rFocusedDataRow["INSPITEMVERSION"].ToString();
                    dr["TITLE"] = vr.selectTitle();//CT_QCRELIABILITYMEASURE.TITLE
                    if (vr.selectValue().Trim().Length == 0)
                        dr["MEASUREVALUE"] = DBNull.Value;//CT_QCRELIABILITYMEASURE.MEASUREVALUE 
                    else
                        dr["MEASUREVALUE"] = vr.selectValue();
                    dr["MEASURETYPE"] = "VerificationResult";//CT_QCRELIABILITYMEASURE.MEASURETYPE
                                                             //dr["FILEID"] =서버로직에서 SF_INSPECTIONFILE 신규입력[fileId]를 입력 CT_QCRELIABILITYMEASURE.FILEID
                    
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
                    dr["FILEPATH"] = "ReliaVerifiResultNonRegular";
                    dr["FILEFULLPATH"] = strFile;
                    dr["FILEINSPECTIONTYPE"] = "ReliaVerifiResultNonRegular";
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
        /// <summary>
        /// 이전 로우 저장
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SavePrevFocusedRow(DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            grdQCReliabilityResultLot.View.CloseEditor();
            grdMeasurValue.View.CloseEditor();
            fpcAttach.grdFileList.View.CloseEditor();
            fpcReportResult.grdFileList.View.CloseEditor();
            //DataRow rFocusedDataRow = this.grdQCReliabilityResultLot.View.GetFocusedDataRow();
            DataRow rFocusedDataRow = grdQCReliabilityResultLot.View.GetDataRow(e.PrevFocusedRowHandle);
            //Reportfile DataTable
            DataTable fileChanged = fpcAttach.GetChangedRows();
            grdQCReliabilityResultLot.View.CheckValidation();

            DataTable dtReliabilityLot = grdQCReliabilityResultLot.GetChangedRows();         //LOT정보 + Inspectionresult
            DataTable dtReliabilityMeasurValue = grdMeasurValue.GetChangedRows();       //계측값
            DataTable dtAttach = fpcAttach.GetChangedRows();                             //첨부파일
            DataTable dtReport = fpcReportResult.GetChangedRows();                            //보고서

            DataTable dtInspectionFile = null;
            dtInspectionFile = GetInspectionFile(e.PrevFocusedRowHandle);//@@네트워크 경로 변경

            #region dtInspectionFile
            ////_dtInspectionFile 조회할떄 세팅된다
            //dtInspectionFile = _dtInspectionFile.Clone();
            //if (!dtInspectionFile.Columns.Contains("LOTID")) dtInspectionFile.Columns.Add("LOTID");
            //if (!dtInspectionFile.Columns.Contains("MEASURETYPE")) dtInspectionFile.Columns.Add("MEASURETYPE");
            //if (!dtInspectionFile.Columns.Contains("MEASUREVALUE")) dtInspectionFile.Columns.Add("MEASUREVALUE");
            //if (!dtInspectionFile.Columns.Contains("TITLE")) dtInspectionFile.Columns.Add("TITLE");

            //Control.ControlCollection ctr = flowMeasuredPicture.Controls;
            //foreach (Control c in ctr)
            //{
            //    if (c.GetType() == typeof(VerificationResultControl))
            //    {
            //        VerificationResultControl vr = c as VerificationResultControl;
            //        string strFile = vr.strFile;
            //        DataRow dr = dtInspectionFile.NewRow();
            //        dr["INSPECTIONTYPE"] = "ReliaVerifiResultNonRegular";
            //        dr["RESOURCEID"] = rFocusedDataRow["PRODUCTDEFID"].ToString();//품목
            //        dr["PROCESSRELNO"] = rFocusedDataRow["REQUESTNO"].ToString();
            //        dr["REQUESTNO"] = rFocusedDataRow["REQUESTNO"].ToString();
            //        dr["PLANTID"] = _sPLANTID;
            //        dr["ENTERPRISEID"] = _sENTERPRISEID;
            //        dr["PRODUCTDEFID"] = rFocusedDataRow["PRODUCTDEFID"].ToString();
            //        dr["PRODUCTDEFVERSION"] = rFocusedDataRow["PRODUCTDEFVERSION"].ToString();
            //        dr["INSPITEMID"] = rFocusedDataRow["INSPITEMID"].ToString();
            //        dr["INSPITEMVERSION"] = rFocusedDataRow["INSPITEMVERSION"].ToString();
            //        dr["LOTID"] = rFocusedDataRow["LOTID"].ToString();
            //        dr["TITLE"] = vr.selectTitle();//CT_QCRELIABILITYMEASURE.TITLE
            //        if (vr.selectValue().Trim().Length == 0)
            //            dr["MEASUREVALUE"] = DBNull.Value;//CT_QCRELIABILITYMEASURE.MEASUREVALUE 
            //        else
            //            dr["MEASUREVALUE"] = vr.selectValue();
            //        dr["MEASURETYPE"] = "VerificationResult";//CT_QCRELIABILITYMEASURE.MEASURETYPE
            //                                                 //dr["FILEID"] =서버로직에서 SF_INSPECTIONFILE 신규입력[fileId]를 입력 CT_QCRELIABILITYMEASURE.FILEID
            //        Image image = vr.picMeasurePrinted.Image;

            //        byte[] memoryImage = (byte[])new ImageConverter().ConvertTo(image, typeof(byte[]));
            //        dr["FILESIZE"] = Math.Round(Convert.ToDouble(memoryImage.Length) / 1024);
            //        dr["FILEDATA"] = memoryImage;
            //        FileInfo fileInfo = new FileInfo(strFile);
            //        dr["FILENAME"] = fileInfo.Name;
            //        dr["FILEEXT"] = fileInfo.Extension;
            //        dtInspectionFile.Rows.Add(dr);

            //    }
            //} 
            #endregion


            DataTable dtLotInspectionResult = grdQCReliabilityResultLot.DataSource as DataTable;

            #region MyRegion
            //제품정보 그리드에 다수 Row가 존재 하더라도 현재 선택된 Row의 측정값/첨부파일/보고서만 저장 가능하다
            //측정값,첨부파일,보고서 저장시 매핑될 Inspectionresult가 없으면 에러.
            //if (dtReliabilityMeasurValue.Rows.Count > 0 ||
            //    dtAttach.Rows.Count > 0 ||
            //    dtReport.Rows.Count > 0 ||
            //    dtInspectionFile.Rows.Count > 0)

            //{
            //    var changedCols = new List<DataColumn>();

            //    if (rFocusedDataRow["TXNHISTKEY"].ToString().Length > 0)//SF_INSPECTIONRESULT 데이터가 존재 한다
            //    {
            //        changedCols.Add(rFocusedDataRow.Table.Columns["TXNHISTKEY"]);
            //    }
            //    else if (rFocusedDataRow.RowState == DataRowState.Modified)//SF_INSPECTIONRESULT 없는 데이터는  grdQCReliabilityLot.GetChangedRows()가 존재 해야 한다.
            //    {
            //        foreach (DataColumn dc in dtLotInspectionResult.Columns)
            //        {
            //            if (!rFocusedDataRow[dc, DataRowVersion.Original].Equals(
            //                 rFocusedDataRow[dc, DataRowVersion.Current]))
            //            {
            //                changedCols.Add(dc);
            //            }
            //        }
            //    }

            //    if (changedCols.Count == 0)
            //    {
            //        ShowMessage("CheckSaveReliaVerifiResultNonRegular");//검증결과등록전 제품의 판정결과를 등록해야 합니다. 
            //        return;
            //    }
            //}  
            #endregion

            //LOT정보 + Inspectionresult 조합이고, 저장시 Inspectionresult 테이블만 저장한다, RowState를 변경한다
            foreach (DataRow dr in dtReliabilityLot.Rows)
            {
                //if (dr["TXNHISTKEY"].ToString() == string.Empty)
                //    dr["_STATE_"] = "added";
                //else
                //    dr["_STATE_"] = "modified";
                if (!dtReliabilityLot.Columns.Contains("RESOURCEID")) dtReliabilityLot.Columns.Add("RESOURCEID");

                dr["RESOURCEID"] = dr["LOTID"];

                //if ((dr["INSPECTIONRESULT"] == DBNull.Value || dr["INSPECTIONRESULT"].ToString().Length <= 1) && dr["ISCOMPLETION"].ToString() == "Y")
                //{
                //    rFocusedDataRow["ISCOMPLETION"] = string.Empty;
                //    ShowMessage("CheckSaveReliaVerifiResultNonRegular");//검증결과등록전 제품의 판정결과를 등록해야 합니다.
                //    return;
                //}

                //if (dr["ISNCRPUBLISH"].ToString() == "Y" && dr["ISCOMPLETION"].ToString() != "Y")
                //{
                //    ShowMessage("CheckIsNcrPublishReliaVerifiResultNonRegular");//이상발생이면 검증결과가 완료되어야 합니다.
                //    grdQCReliabilityResultLot.View.FocusedRowChanged -= ResultView_FocusedRowChanged;
                //    grdQCReliabilityResultLot.View.FocusedRowHandle = e.PrevFocusedRowHandle;
                //    grdQCReliabilityResultLot.View.FocusedRowChanged += ResultView_FocusedRowChanged;
                //    return;
                //}

                //if (dr["INSPECTIONRESULT"].ToString() == "OK" && dr["DEFECTCODE"].ToString().Trim().Length == 0)
                //{
                //    ShowMessage("CheckInspectionResultReliaVerifiResultNonRegular");//판정결과 OK이면 불량코드가 선택되어야 합니다 . 
                //    grdQCReliabilityResultLot.View.FocusedRowChanged -= ResultView_FocusedRowChanged;
                //    grdQCReliabilityResultLot.View.FocusedRowHandle = e.PrevFocusedRowHandle;
                //    grdQCReliabilityResultLot.View.FocusedRowChanged += ResultView_FocusedRowChanged;
                //    return;
                //}

                //if (dr["ISNCRPUBLISH"].ToString() == "Y")
                //{
                //    //이상발생 사유코드 등록
                //    if (!dtReliabilityLot.Columns.Contains("REASONCODEID")) dtReliabilityLot.Columns.Add("REASONCODEID");

                //    dr["REASONCODEID"] = "LockReliablOutRegNonconform";
                //}
            }

            try
            {
                this.ShowWaitArea();
                btnSave.Enabled = false;
                btnClose.Enabled = false;

                dtReliabilityLot.TableName = "inspectionresultList";                //LOT정보 + Inspectionresult
                dtReliabilityMeasurValue.TableName = "measuredValueList";           //계측값 입력
                dtAttach.TableName = "attachFileList";                              //첨부파일
                dtReport.TableName = "reportFileList";                              //리포트
                if (dtInspectionFile != null) dtInspectionFile.TableName = "inspectionFile";                      //검증결과 이미지

                DataTable dtFocusedDataRow = dtLotInspectionResult.Clone();       //기본 정보용(그리드에 바인딩된 Data)
                DataRow dr = dtFocusedDataRow.NewRow();
                dr["REQUESTNO"] = rFocusedDataRow["REQUESTNO"].ToString();
                dr["ENTERPRISEID"] = rFocusedDataRow["ENTERPRISEID"].ToString();
                dr["PLANTID"] = rFocusedDataRow["PLANTID"].ToString();
                dr["PRODUCTDEFID"] = rFocusedDataRow["PRODUCTDEFID"].ToString();
                dr["PRODUCTDEFVERSION"] = rFocusedDataRow["PRODUCTDEFVERSION"].ToString();
                dr["LOTID"] = rFocusedDataRow["LOTID"].ToString();
                //dr["TXNHISTKEY"] = rFocusedDataRow["TXNHISTKEY"].ToString();//SF_INSPECTIONRESULT.txnhistkey
                dr["INSPITEMID"] = rFocusedDataRow["INSPITEMID"].ToString();
                dr["INSPITEMVERSION"] = rFocusedDataRow["INSPITEMVERSION"].ToString();

                dtFocusedDataRow.Rows.Add(dr);
                dtFocusedDataRow.TableName = "focusedDataRow";
                _dtReliabilityRequest.Rows[0]["COMMENTS"] = txtCOMMENTS.Text.ToString().Length > 2500 ? txtCOMMENTS.Text.Substring(0, 2500) : txtCOMMENTS.Text;
                DataTable dtReliabilityRequest = _dtReliabilityRequest.Copy();
                dtReliabilityRequest.TableName = "reliabilityRequest";

                DataSet rullSet = new DataSet();
                rullSet.Tables.Add(dtReliabilityLot);
                rullSet.Tables.Add(dtReliabilityMeasurValue);
                rullSet.Tables.Add(dtAttach);
                rullSet.Tables.Add(dtReport);
                if (dtInspectionFile != null) rullSet.Tables.Add(dtInspectionFile);
                rullSet.Tables.Add(dtFocusedDataRow);
                rullSet.Tables.Add(dtReliabilityRequest);
                ExecuteRule("SaveReliaVerifiResultNonRegular", rullSet);
                //@@네트워크 경로 변경
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
                ShowMessage("SuccessSave");
            }
            catch (Exception ex)
            {
                this.ShowError(ex);
            }
            finally
            {
                this.CloseWaitArea();
                btnSaveResult.Enabled = true;
                btnCloseResult.Enabled = true;

                //_dtInspectionFile = null;
                rFocusedDataRow["ISCOMPLETION_ORG"] = rFocusedDataRow["ISCOMPLETION"];
                DataTable dtQCReliabilityResultLot = grdQCReliabilityResultLot.DataSource as DataTable;
                dtQCReliabilityResultLot.AcceptChanges();
                if (dtQCReliabilityResultLot.Rows.Count > 0 && dtQCReliabilityResultLot.AsEnumerable().Where(r => r["ISCOMPLETION"].ToString() != "Y").ToList().Count == 0)
                {
                    //모두 완료 이므로 저장후 팝업을 닫는다
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    //저장후 팝업을 닫지 않는다
                    focusedRowChangedResult();
                }
            }
        }
        /// <summary>
        /// 저장버튼을 클릭했을때 검사 결과를 저장하는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSaveResult_Click(object sender, EventArgs e)
        {
            grdQCReliabilityResultLot.View.CloseEditor();
            grdMeasurValue.View.CloseEditor();
            fpcAttach.grdFileList.View.CloseEditor();
            fpcReportResult.grdFileList.View.CloseEditor();
            DataRow rFocusedDataRow = this.grdQCReliabilityResultLot.View.GetFocusedDataRow();
            //Reportfile DataTable
            DataTable fileChanged = fpcAttach.GetChangedRows();
            grdQCReliabilityResultLot.View.CheckValidation();

            DataTable dtReliabilityLot = grdQCReliabilityResultLot.GetChangedRows();         //LOT정보 + Inspectionresult
            DataTable dtReliabilityMeasurValue = grdMeasurValue.GetChangedRows();       //계측값
            DataTable dtAttach = fpcAttach.GetChangedRows();                             //첨부파일
            DataTable dtReport = fpcReportResult.GetChangedRows();                            //보고서

            DataTable dtInspectionFile = GetInspectionFile(this.grdQCReliabilityResultLot.View.FocusedRowHandle);
            DataTable dtLotInspectionResult = grdQCReliabilityResultLot.DataSource as DataTable;

            foreach (DataRow dr in dtReliabilityLot.Rows)
            {
                //if (!dtReliabilityLot.Columns.Contains("RESOURCEID")) dtReliabilityLot.Columns.Add("RESOURCEID");

                //dr["RESOURCEID"] = dr["LOTID"];

                //if ((dr["INSPECTIONRESULT"] == DBNull.Value ||  dr["INSPECTIONRESULT"].ToString().Length <= 1) && dr["ISCOMPLETION"].ToString() == "Y")
                //{
                //    rFocusedDataRow["ISCOMPLETION"] = string.Empty;
                //    ShowMessage("CheckSaveReliaVerifiResultNonRegular");//검증결과등록전 제품의 판정결과를 등록해야 합니다.
                //    return;
                //}

                //if (dr["INSPECTIONRESULT"].ToString() == "OK" && dr["DEFECTCODE"].ToString().Trim().Length == 0)
                //{
                //    ShowMessage("CheckInspectionResultReliaVerifiResultNonRegular");//판정결과 OK이면 불량코드가 선택되어야 합니다 . 
                //    return;
                //}
            }

            try
            {
                this.ShowWaitArea();
                btnSaveResult.Enabled = false;
                btnCloseResult.Enabled = false;

                dtReliabilityLot.TableName = "inspectionresultList";                //LOT정보 + Inspectionresult
                dtReliabilityMeasurValue.TableName = "measuredValueList";           //계측값 입력
                dtAttach.TableName = "attachFileList";                              //첨부파일
                dtReport.TableName = "reportFileList";                              //리포트
                if (dtInspectionFile != null) dtInspectionFile.TableName = "inspectionFile";                    //검증결과 이미지

                DataTable dtFocusedDataRow = dtLotInspectionResult.Clone();       //기본 정보용(그리드에 바인딩된 Data)
                DataRow dr = dtFocusedDataRow.NewRow();
                dr["REQUESTNO"] = rFocusedDataRow["REQUESTNO"].ToString();
                dr["ENTERPRISEID"] = rFocusedDataRow["ENTERPRISEID"].ToString();
                dr["PLANTID"] = rFocusedDataRow["PLANTID"].ToString();
                dr["PRODUCTDEFID"] = rFocusedDataRow["PRODUCTDEFID"].ToString();
                dr["PRODUCTDEFVERSION"] = rFocusedDataRow["PRODUCTDEFVERSION"].ToString();
                dr["LOTID"] = rFocusedDataRow["LOTID"].ToString();
                //dr["TXNHISTKEY"] = rFocusedDataRow["TXNHISTKEY"].ToString();//SF_INSPECTIONRESULT.txnhistkey
                dr["INSPITEMID"] = rFocusedDataRow["INSPITEMID"].ToString();
                dr["INSPITEMVERSION"] = rFocusedDataRow["INSPITEMVERSION"].ToString();

                dtFocusedDataRow.Rows.Add(dr);
                dtFocusedDataRow.TableName = "focusedDataRow";
                _dtReliabilityRequest.Rows[0]["COMMENTS"] = txtCOMMENTS.Text.ToString().Length > 2500 ? txtCOMMENTS.Text.Substring(0, 2500) : txtCOMMENTS.Text;
                DataTable dtReliabilityRequest = _dtReliabilityRequest.Copy();
                dtReliabilityRequest.TableName = "reliabilityRequest";

                DataSet rullSet = new DataSet();
                rullSet.Tables.Add(dtReliabilityLot);
                rullSet.Tables.Add(dtReliabilityMeasurValue);
                rullSet.Tables.Add(dtAttach);
                rullSet.Tables.Add(dtReport);
                if (dtInspectionFile != null) rullSet.Tables.Add(dtInspectionFile);
                rullSet.Tables.Add(dtFocusedDataRow);
                rullSet.Tables.Add(dtReliabilityRequest);
                ExecuteRule("SaveReliaVerifiResultNonRegular", rullSet);
                //@@네트워크 경로 변경
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

                ShowMessage("SuccessSave");
            }
            catch (Exception ex)
            {
                this.ShowError(ex);
            }
            finally
            {
                this.CloseWaitArea();
                btnSaveResult.Enabled = true;
                btnCloseResult.Enabled = true;
                //저장후 팝업을 닫지 않는다
                //_dtInspectionFile = null;

                if (dtReliabilityLot != null && dtReliabilityLot.Rows.Count > 0) (grdQCReliabilityResultLot.DataSource as DataTable).AcceptChanges();
                SearchResult();
                focusedRowChangedResult();
                //this.DialogResult = DialogResult.OK;
                //this.Close();
            }
        }
        /// <summary>
        /// 그리드의 Row Click시 조회
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResultView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.PrevFocusedRowHandle < 0)// 초기 폼로딩
            {
                focusedRowChangedResult();
            }
            else
            {
                DataTable dtQCReliabilityResultLot = grdQCReliabilityResultLot.DataSource as DataTable;
                if (dtQCReliabilityResultLot.Rows.Count > 0 && dtQCReliabilityResultLot.AsEnumerable().Where(r => r["ISCOMPLETION_ORG"].ToString() != "Y").ToList().Count == 0)
                {
                    focusedRowChangedResult();
                }
                else
                {
                    var row = grdQCReliabilityResultLot.View.GetDataRow(e.PrevFocusedRowHandle);
                    string sISCOMPLETION = row["ISCOMPLETION_ORG"].ToString();
                    if (sISCOMPLETION == "Y")
                    {
                        (grdQCReliabilityResultLot.DataSource as DataTable).RejectChanges();
                        focusedRowChangedResult();
                    }
                    else
                    {
                        DialogResult result = System.Windows.Forms.DialogResult.No;

                        DataTable dtReliabilityLot = grdQCReliabilityResultLot.GetChangedRows();         //LOT정보 + Inspectionresult
                        DataTable dtReliabilityMeasurValue = grdMeasurValue.GetChangedRows();       //계측값
                        DataTable dtAttach = fpcAttach.GetChangedRows();                             //첨부파일
                        DataTable dtReport = fpcReportResult.GetChangedRows();                            //보고서
                        DataTable dtInspectionFile = null;
                        dtInspectionFile = GetInspectionFile(e.PrevFocusedRowHandle);
                        #region 편집된 항목있는지 판별
                        bool bCheckIsSave = false;
                        if (_dtInspectionFile.Rows.Count != dtInspectionFile.Rows.Count)
                        {
                            bCheckIsSave = true;
                        }
                        else
                        {

                            foreach (DataRow dr in _dtInspectionFile.Rows)
                            {
                                IEnumerable<DataRow> dataRow = from im in dtInspectionFile.AsEnumerable()
                                                               where im.Field<string>("FILEID") == dr["FILEID"].ToString()
                                                               select im;
                                if (dataRow == null)
                                {
                                    bCheckIsSave = true;
                                    break;
                                }
                                else
                                {
                                    foreach (DataRow vr in dataRow)
                                    {
                                        foreach (DataColumn dc in _dtInspectionFile.Columns)
                                        {
                                            if (dtInspectionFile.Columns.Contains(dc.ColumnName))
                                            {
                                                if (dr[dc.ColumnName].ToString() != vr[dc.ColumnName].ToString()//@@네트워크 경로 변경
                                                    && (dc.ColumnName == "TITLE"
                                                    || dc.ColumnName == "MEASUREVALUE"
                                                    || dc.ColumnName == "FILEID")
                                                    )
                                                {
                                                    bCheckIsSave = true;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        #endregion

                        if (dtReliabilityLot.Rows.Count > 0
                            || dtReliabilityMeasurValue.Rows.Count > 0
                            || dtAttach.Rows.Count > 0
                            || dtReport.Rows.Count > 0
                            || bCheckIsSave)
                        {
                            result = this.ShowMessage(MessageBoxButtons.YesNo, "InfoSaveAndSearch");//편집중 데이터를 저장 하시겠습니까?
                            if (result == System.Windows.Forms.DialogResult.No)
                            {
                                //grdQCReliabilityResultLot.View.SelectRow(e.PrevFocusedRowHandle);
                                grdQCReliabilityResultLot.View.FocusedRowChanged -= ResultView_FocusedRowChanged;
                                grdQCReliabilityResultLot.View.FocusedRowHandle = e.PrevFocusedRowHandle;
                                grdQCReliabilityResultLot.View.FocusedRowChanged += ResultView_FocusedRowChanged;
                                return;
                            }
                            else
                                SavePrevFocusedRow(e);
                        }
                        else
                        {
                            focusedRowChangedResult();
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 닫기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCloseResult_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        #endregion

        #region Private Function
        //YJKIM : 이미지파일 서버 전송을 위한 DataTable을 반환
        //GetImageFileTable - 이미지파일을 ProgressFileDialog를 이용하여 전송하기 위한 DataTemplate을 생성하는 메소드
        private DataTable GetImageFileTable()//@@네트워크 경로 변경
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
        /// 불량코드 팝업 조회
        /// </summary>
        private void DefectCodePopup()
        {
            //var defectcodePopup = grdQCReliabilityLot.View.AddSelectPopupColumn("DEFECTCODE", new SqlQuery("GetDefectCodeList", "10002", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
            var defectcodePopup = grdQCReliabilityResultLot.View.AddSelectPopupColumn("DEFECTCODE", new SqlQuery("GetDefectCodeList", "10004", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetPopupLayout("DEFECTCODE", PopupButtonStyles.Ok_Cancel, true, true)
                .SetPopupResultCount(1)
                .SetPopupLayoutForm(400, 500, FormBorderStyle.FixedToolWindow)
                //.SetValidationIsRequired()
                .SetPopupAutoFillColumns("DEFECTCODE")
                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {
                    // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
                    // dataGridRow : 현재 Focus가 있는 그리드의 DataRow
                    if (selectedRows.Count() < 1)
                    {
                        return;
                    }

                    foreach (DataRow row in selectedRows)
                    {
                        dataGridRow["DEFECTCODE"] = row["DEFECTCODE"].ToString();
                        dataGridRow["DEFECTNAME"] = row["DEFECTCODENAME"].ToString();
                        dataGridRow["QCSEGMENTID"] = row["QCSEGMENTID"].ToString();
                        dataGridRow["QCSEGMENTNAME"] = row["QCSEGMENTNAME"].ToString();
                    }
                });

            defectcodePopup.Conditions.AddTextBox("DEFECTCODENAME");

            defectcodePopup.GridColumns.AddTextBoxColumn("DEFECTCODE", 150);
            defectcodePopup.GridColumns.AddTextBoxColumn("DEFECTCODENAME", 150);
            defectcodePopup.GridColumns.AddTextBoxColumn("QCSEGMENTID", 150);
            defectcodePopup.GridColumns.AddTextBoxColumn("QCSEGMENTNAME", 150);
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
        private void View_CancelChange(object sender, CancelEventArgs e)
        {
            DataRow row = grdQCReliabilityResultLot.View.GetFocusedDataRow();
            if (row["ISCOMPLETION_ORG"].ToString() == "Y")
            {
                e.Cancel = true;
            }
            //else if (grdQCReliabilityResultLot.View.GetFocusedDataRow().RowState == DataRowState.Modified 
            //    && grdQCReliabilityResultLot.View.GetFocusedRowCellValue("ISCOMPLETION").ToString().Length > 0 
            //    && row["INSPECTIONRESULT"].ToString().Length == 0)
            //{
            //    row["ISCOMPLETION"] = string.Empty;
            //    ShowMessage("CheckSaveReliaVerifiResultNonRegular");//검증결과등록전 제품의 판정결과를 등록해야 합니다.
            //    e.Cancel = true;
            //}
        }
        /// <summary>
        /// 그리드의 포커스 행 변경시 행의 작업목록의 내용을 매핑처리 함.
        /// </summary>
        private void focusedRowChangedResult()
        {
            //포커스 행 체크 
            if (grdQCReliabilityResultLot.View.FocusedRowHandle < 0) return;

            btnSaveResult.Enabled = true;
            var row = grdQCReliabilityResultLot.View.GetDataRow(grdQCReliabilityResultLot.View.FocusedRowHandle);
            //완료여부(Y)->수정 불가능
            string sISCOMPLETION = row["ISCOMPLETION"].ToString();
            if (sISCOMPLETION == "Y")
            {
                btnSaveResult.Enabled = false;
            }
            else
            {
                btnSaveResult.Enabled = (this.Owner as ReliabilityVerificationConsumableRegular).btnFlag.Enabled;
            }
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("P_ENTERPRISEID", _sENTERPRISEID);
            param.Add("P_PLANTID", _sPLANTID);
            param.Add("P_REQUESTNO", _sREQUESTNO);
            param.Add("P_PRODUCTDEFID", row["PRODUCTDEFID"].ToString());
            param.Add("P_LOTID", row["LOTID"].ToString());
            param.Add("P_MEASURETYPE", "MeasuredValue");
            param.Add("P_INSPITEMID", row["INSPITEMID"].ToString());
            param.Add("P_INSPITEMVERSION", row["INSPITEMVERSION"].ToString());
            DataTable dtReliabilityMeasurValue = SqlExecuter.Query("GetReliabilityNonRegularMeasureList", "10001", param);
            grdMeasurValue.DataSource = dtReliabilityMeasurValue;
            if (dtReliabilityMeasurValue.Rows.Count > 0)
            {
                cboUnit.EditValue = dtReliabilityMeasurValue.Rows[0]["UNIT"].ToString();
            }

            Dictionary<string, object> fileValues = new Dictionary<string, object>();
            //이미지 파일Search parameter
            fileValues.Add("P_FILERESOURCETYPE", "ReliaVerifiResultNonRegularFile");
            fileValues.Add("P_FILERESOURCEID", _sREQUESTNO + row["PRODUCTDEFID"].ToString() + row["PRODUCTDEFVERSION"].ToString() + row["LOTID"].ToString() + row["INSPITEMID"].ToString() + row["INSPITEMVERSION"].ToString());//요청번호,품목,버전,lot,검증item,버전
            fileValues.Add("P_FILERESOURCEVERSION", "0");

            DataTable fileTable = SqlExecuter.Query("SelectFileInspResult", "10001", fileValues);

            fpcAttach.DataSource = fileTable;

            (fpcAttach.DataSource as DataTable).RowChanged -= new DataRowChangeEventHandler(OnRowChangedResult);
            (fpcAttach.DataSource as DataTable).RowChanged += new DataRowChangeEventHandler(OnRowChangedResult);

            Dictionary<string, object> fileValues2 = new Dictionary<string, object>();
            //이미지 파일Search parameter
            fileValues2.Add("P_FILERESOURCETYPE", "ReliaVerifiResultNonRegularReport");
            fileValues2.Add("P_FILERESOURCEID", _sREQUESTNO + row["PRODUCTDEFID"].ToString() + row["PRODUCTDEFVERSION"].ToString() + row["LOTID"].ToString() + row["INSPITEMID"].ToString() + row["INSPITEMVERSION"].ToString());//요청번호,품목,버전,lot,검증item,버전
            fileValues2.Add("P_FILERESOURCEVERSION", "0");

            DataTable fileTable2 = SqlExecuter.Query("SelectFileInspResult", "10001", fileValues2);

            fpcReportResult.DataSource = fileTable2;

            (fpcReportResult.DataSource as DataTable).RowChanged -= new DataRowChangeEventHandler(OnRowChangedResult);
            (fpcReportResult.DataSource as DataTable).RowChanged += new DataRowChangeEventHandler(OnRowChangedResult);

            param.Add("P_INSPECTIONTYPE", "ReliaVerifiResultNonRegular");
            param.Add("P_RESOURCEID", row["PRODUCTDEFID"].ToString() + row["PRODUCTDEFVERSION"].ToString() + row["LOTID"].ToString() + row["INSPITEMID"].ToString() + row["INSPITEMVERSION"].ToString());//품목,버전,lot,검증item,버전
            param.Add("P_PROCESSRELNO", _sREQUESTNO);
            param["P_MEASURETYPE"] = "VerificationResult";
            _dtInspectionFile = SqlExecuter.Query("SelectInspectionFile", "10001", param);
            flowMeasuredPicture.Controls.Clear();
            if (_dtInspectionFile != null && _dtInspectionFile.Rows.Count != 0)
            {
                ImageConverter converter = new ImageConverter();
                foreach (DataRow dr in _dtInspectionFile.Rows)
                {
                    object file = dr["FILEDATA"];
                    string filePath = dr["FILEPATH"].ToString();
                    string fileName = dr["FILENAME"].ToString();
                    string fileText = dr["FILEEXT"].ToString();
                    string title = dr["TITLE"].ToString();
                    string measureValue = dr["MEASUREVALUE"].ToString();
                    if (file != null)
                    {
                        try
                        {
                            //Bitmap image = new Bitmap((Image)converter.ConvertFrom(file));
                            //Bitmap image = new Bitmap(GetImageFromWeb(AppConfiguration.GetString("Application.SmartDeploy.Url") + dr.GetString("WEBPATH")));

                            fileName = string.Join(".", fileName, fileText);
                            Bitmap image = CommonFunction.GetFtpImageFileToBitmap(filePath, fileName);

                            VerificationResultControl vr = new VerificationResultControl(image, fileName);
                            vr.setTitle(title);
                            vr.setValue(measureValue);
                            vr.Tag = dr["FILEID"].ToString();
                            flowMeasuredPicture.Controls.Add(vr);
                        } catch(Exception ex)
                        {
                        }
                    }
                }
            }

        }
        private Image GetImageFromWeb(string webPath)//@@네트워크 경로 변경
        {
            System.Net.WebClient Downloader = new System.Net.WebClient();

            Stream ImageStream = Downloader.OpenRead(webPath);

            Bitmap downloadImage = Bitmap.FromStream(ImageStream) as Bitmap;

            return downloadImage;
        }
        /// <summary>
        /// 
        /// </summary>
        private void SearchResult()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("P_ENTERPRISEID", _sENTERPRISEID);
            param.Add("P_PLANTID", _sPLANTID);
            param.Add("P_REQUESTNO", _sREQUESTNO);
            param.Add("P_RELIABILITYTYPE", "ReliabilityConsumeRegularInspection");
            DataTable dtReliabilityRequest = SqlExecuter.Query("GetReliabilityVerificationRequestNonRegularRgisterList", "10001", param);           //신뢰성의뢰
            DataTable dtReliabilityLot = SqlExecuter.Query("GetReliabilityVerificationConsumableRegularLot", "10001", param);               //LOT정보

            grdQCReliabilityResultLot.DataSource = dtReliabilityLot;

            if (dtReliabilityRequest.Rows.Count > 0)
            {
                _dtReliabilityRequest = dtReliabilityRequest;
                DataRow dr = dtReliabilityRequest.Rows[0];
                txtCOMMENTS.Text = dr["COMMENTS"].ToString();
            }

            //완료여부(Y)-> 수정 불가능
            //focusedRowChangedResult 이벤트에서 현재 선택된 로우의 완료여부에 따라 저장 버튼 활성화를 선택한다
            //if (dtReliabilityLot.Rows.Count > 0 && dtReliabilityLot.AsEnumerable().Where(r => r["ISCOMPLETION"].ToString() != "Y").ToList().Count == 0)
            //    btnSaveResult.Enabled = false;
            //else
            //    btnSaveResult.Enabled = true;
        }




        #endregion
    }
}
