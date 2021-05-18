#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons;
using System;
using Micube.SmartMES.Commons.Controls;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Micube.SmartMES.Commons;

#endregion

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 수입 검사 관리 >공정 수입검사 의뢰
    /// 업  무  설  명  : 공정 수입검사를 의뢰한다. 
    /// 생    성    자  : 강유라
    /// 생    성    일  : 2019-09-01
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ProcessImportInspectionRequest : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가
        /// 부모 그리드로 데이터를 바인딩 시켜줄 델리게이트
        /// </summary>
        /// <param name="dt"></param>
        //public delegate void LotSelectionDataHandler(DataTable dt);
        //public event LotSelectionDataHandler LotSelectEvent;
        DataTable _requested;
        bool autoChange = false;

        #endregion

        #region 생성자

        public ProcessImportInspectionRequest()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 화면의 컨텐츠 영역을 초기화한다.
        /// </summary>
        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeEvent();

            // TODO : 컨트롤 초기화 로직 구성
            InitializeGrid();
        }

        /// <summary>        
        /// 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdInspectionRequest.GridButtonItem = GridButtonItem.Export;
            grdInspectionRequest.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdInspectionRequest.View.AddComboBoxColumn("REQUESTSTATUS", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=RequestStatus", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsReadOnly();

            grdInspectionRequest.View.AddTextBoxColumn("REQUESTDATE", 130)
                .SetLabel("REQUESTDATETIME")
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly();

            grdInspectionRequest.View.AddTextBoxColumn("AREANAME", 200)
                .SetIsReadOnly();

            grdInspectionRequest.View.AddTextBoxColumn("VENDORNAME", 100)
                .SetIsHidden()
                .SetIsReadOnly();

            grdInspectionRequest.View.AddTextBoxColumn("LOTID", 200)
                .SetIsReadOnly();

            grdInspectionRequest.View.AddTextBoxColumn("DEGREE", 80)
                .SetIsReadOnly();

            grdInspectionRequest.View.AddTextBoxColumn("PRODUCTDEFID", 200)
                .SetIsReadOnly();

            grdInspectionRequest.View.AddTextBoxColumn("PRODUCTDEFNAME", 200)
                .SetIsReadOnly();

            grdInspectionRequest.View.AddTextBoxColumn("PRODUCTDEFVERSION", 100)
                .SetIsReadOnly();

            grdInspectionRequest.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 200)
                .SetIsReadOnly();

            grdInspectionRequest.View.AddTextBoxColumn("PROCESSSEGMENTVERSION", 100)
                .SetIsReadOnly();

            grdInspectionRequest.View.AddTextBoxColumn("WORKENDPCSQTY", 100)
                .SetIsReadOnly();

            grdInspectionRequest.View.AddTextBoxColumn("WORKENDPANELQTY", 100)
                .SetIsReadOnly();

            grdInspectionRequest.View.AddTextBoxColumn("WORKENDMMQTY", 100)
                .SetIsReadOnly();

            grdInspectionRequest.View.AddTextBoxColumn("REQUESTOR", 100)
                .SetIsHidden();

            grdInspectionRequest.View.AddTextBoxColumn("REQUESTORNAME", 100)
                .SetIsReadOnly();

            grdInspectionRequest.View.AddTextBoxColumn("INSPECTIONDATE", 100)
                .SetIsReadOnly();

            grdInspectionRequest.View.AddTextBoxColumn("INSPECTIONRESULT", 100)
                .SetIsReadOnly();

            grdInspectionRequest.View.AddTextBoxColumn("ISSENDNAME", 100)
                .SetIsReadOnly()
                .SetLabel("ISTAKEOVER");

            grdInspectionRequest.View.AddTextBoxColumn("LOTHISTKEY", 200)
                .SetIsHidden();

            grdInspectionRequest.View.AddTextBoxColumn("REQUESTDATETIME", 200)
                .SetIsHidden();

            grdInspectionRequest.View.AddTextBoxColumn("ENTERPRISEID", 200)
                .SetIsHidden();

            grdInspectionRequest.View.AddTextBoxColumn("PLANTID", 200)
                .SetIsHidden();

            grdInspectionRequest.View.PopulateColumns();
        }
     
        #region 그리드 팝업 초기화
        /*
        private void InitializeGrdPopup_Vendor()
        {
            // 팝업 컬럼 설정
            var vendorPopup = grdInspectionRequest.View.AddSelectPopupColumn("VENDORNAME", 100, new SqlQuery("GetVendorList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"))
                                        .SetPopupLayout("VENDOR", PopupButtonStyles.Ok_Cancel, true, false)
                                        .SetPopupLayoutForm(400, 600)
                                        .SetPopupResultCount(1)
                                        .SetLabel("VENDOR")
                                        .SetPopupAutoFillColumns("VENDORNAME");

            // 팝업 조회조건
            vendorPopup.Conditions.AddTextBox("VENDORID")
                       .SetLabel("VENDOR");

            // 팝업 그리드
            vendorPopup.GridColumns.AddTextBoxColumn("VENDORID", 150);

            vendorPopup.GridColumns.AddTextBoxColumn("VENDORNAME", 200);
        }

        private void InitializeGrdPopup_lotId()
        {
            // 팝업 컬럼 설정
            var lotPopup = grdInspectionRequest.View.AddSelectPopupColumn("LOTID", 100, new SqlQuery("GetLotIdToInspRequest", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "VENDORID")
                                        .SetPopupLayout("LOTID", PopupButtonStyles.Ok_Cancel, true, false)
                                        .SetPopupLayoutForm(1200, 800)
                                        .SetPopupResultCount(1)
                                        .SetLabel("LOTID")
                                        .SetPopupApplySelection((selectedRows, dataGridRow) => 
                                        {
                                            if (selectedRows.Count() < 1)
                                            {
                                                return;
                                            }

                                            foreach (DataRow row in selectedRows)
                                            {
                                                dataGridRow["LOTHISTKEY"] = row["TXNHISTKEY"].ToString();
                                                dataGridRow["PRODUCTDEFID"] = row["PRODUCTDEFID"].ToString();
                                                dataGridRow["PRODUCTDEFVERSION"] = row["PRODUCTDEFVERSION"].ToString();
                                                dataGridRow["PRODUCTDEFNAME"] = row["PRODUCTDEFNAME"].ToString();
                                                dataGridRow["PROCESSSEGMENTID"] = row["PROCESSSEGMENTID"].ToString();
                                                dataGridRow["PROCESSSEGMENTVERSION"] = row["PROCESSSEGMENTVERSION"].ToString();
                                                dataGridRow["PROCESSSEGMENTNAME"] = row["PROCESSSEGMENTNAME"].ToString();
                                                dataGridRow["AREANAME"] = row["AREANAME"].ToString();
                                                dataGridRow["WORKENDPCSQTY"] = row["WORKENDPCSQTY"].ToString();
                                                dataGridRow["WORKENDPANELQTY"] = row["WORKENDPANELQTY"].ToString();
                                                dataGridRow["WORKENDMMQTY"] = row["WORKENDMMQTY"].ToString();
                                            }

                                        });


            // 팝업에서 사용되는 검색조건
            var conditionProductdef = lotPopup.Conditions.AddSelectPopup("PRODUCTDEFID", new SqlQuery("GetProductDefList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PRODUCTDEFNAME", "PRODUCTDEFID")
                .SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupLayoutForm(600, 800)
                .SetLabel("PRODUCTDEF")
                .SetPopupResultCount(1)
                .SetPopupAutoFillColumns("PRODUCTDEFNAME");

            conditionProductdef.Conditions.AddTextBox("PRODUCTDEF");

            conditionProductdef.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            conditionProductdef.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);

            // Lot No
            lotPopup.Conditions.AddTextBox("LOTID");

            // 공정
            var conditionProcessSegment = lotPopup.Conditions.AddSelectPopup("PROCESSSEGMENTID", new SqlQuery("GetProcessSegmentList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}",$"ENTERPRISEID={UserInfo.Current.Enterprise}",$"PLANTID={UserInfo.Current.Plant}"), "PROCESSSEGMENTNAME", "PROCESSSEGMENTID")
                .SetPopupLayout("SELECTPROCESSSEGMENT", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupLayoutForm(600, 800)
                .SetLabel("PROCESSSEGMENT")
                .SetPopupResultCount(1)
                .SetPopupAutoFillColumns("PROCESSSEGMENTNAME");

            conditionProcessSegment.Conditions.AddTextBox("PROCESSSEGMENT");

            conditionProcessSegment.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150);
            conditionProcessSegment.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);


            // 팝업 그리드
            lotPopup.GridColumns.AddTextBoxColumn("VENDORID", 150);

            lotPopup.GridColumns.AddTextBoxColumn("VENDORNAME", 200);
        }
        */
        #endregion
        

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            //btnSelectLot 버튼 이벤트
            Load += (s, e) => {

                SearchData();
            };

            txtLotId.Editor.KeyDown += TxtLotId_KeyDown;
            //btnSelectLot.Click += BtnSelectLot_Click;

            //delete 이벤트
            //grdInspectionRequest.ToolbarDeletingRow += GrdInspectionRequest_ToolbarDeletingRow;
            //new SetGridDeleteButonVisibleOnly(grdInspectionRequest);

            //체크박스 이벤트
            grdInspectionRequest.View.CheckStateChanged += View_CheckStateChanged;
            //의뢰취소 버튼 클릭 이벤트
            btnCancelRequest.Click += BtnCancelRequest_Click;
        
        }
        /// <summary>
        /// 의뢰 취소 버튼 클릭시 체크된 행은 의뢰 취소한다
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancelRequest_Click(object sender, EventArgs e)
        {
            Btn_CancelRequestClick();
        }

        /// <summary>
        /// 입고되지않은것 의뢰취소 이벤트 REALDELETE
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CheckStateChanged(object sender, EventArgs e)
        {
            //DataRow rowOld = grdInspectionRequest.View.GetDataRow(grdInspectionRequest.View.GetFocusedDataSourceRowIndex());
            
            DataRow row = grdInspectionRequest.View.GetFocusedDataRow();

            if (autoChange  == false &&(row["REQUESTSTATUS"].ToString().Equals("RequestComplete") || row["REQUESTSTATUS"].ToString().Equals("InspectionCompleted")))
            {
                autoChange = true;
                grdInspectionRequest.View.CheckRow(grdInspectionRequest.View.FocusedRowHandle, false);

                throw MessageException.Create("CantCancelInspRequest");//의뢰중인 건만 의뢰취소 가능합니다.
            }

            //2019-12-27 작업장 제한
            if (autoChange == false && !row["ISMODIFY"].ToString().Equals("Y"))
            {
                autoChange = true;
                grdInspectionRequest.View.CheckRow(grdInspectionRequest.View.FocusedRowHandle, false);

                string area = Format.GetString(row["AREANAME"]);
                throw MessageException.Create("NoMatchingAreaUser", area);
            }
            //2020-01-16 자주, 품질규격 검사 여부
            if (autoChange == false && row["SHIPREQUIRED"].ToString().Equals("Y"))
            {
                if (row["SELFSHIPINSPRESULT"].ToString().Equals("N"))
                {
                    autoChange = true;
                    grdInspectionRequest.View.CheckRow(grdInspectionRequest.View.FocusedRowHandle, false);
                    throw MessageException.Create("NotExistsInspectionShipResultOSP");
                }
            }

            if (autoChange == false && row["OPERATIONREQUIRED"].ToString().Equals("Y"))
            {
                if(row["MEASUREINSPRESULT"].ToString().Equals("N"))
                { 
                    autoChange = true;
                    grdInspectionRequest.View.CheckRow(grdInspectionRequest.View.FocusedRowHandle, false);
                    throw MessageException.Create("NotExistsInspectionQualityResultOSP");
                }
            }
                autoChange = false;           
        }

        /// <summary>
        /// 검색한 LotID중 검사 의뢰된지 않은 row에 체크하는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtLotId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtLotId.EditValue != null)
            {
                DataTable dt = grdInspectionRequest.DataSource as DataTable;

                var selectLotList = dt.AsEnumerable()
                    .Where(r => string.IsNullOrWhiteSpace(r["REQUESTDATE"].ToString())
                    && string.IsNullOrWhiteSpace(r["REQUESTOR"].ToString())
                    && string.IsNullOrWhiteSpace(r["DEGREE"].ToString())
                    && r["LOTID"].ToString().Equals(txtLotId.EditValue.ToString().Trim()))
                    .ToList();

                if (selectLotList.Count < 1)
                {
                    ShowMessage("NoDataToOSPRequest");//공정수입검사를 의뢰할 대상이 없습니다.
                    return;
                }

                string lotHistKey = selectLotList.CopyToDataTable().Rows[0]["LOTHISTKEY"].ToString();

                var handles = grdInspectionRequest.View.GetRowHandlesByValue("LOTHISTKEY", lotHistKey);
                int rowHandle = -1;

                foreach (var item in handles)
                {
                    DataRow row = grdInspectionRequest.View.GetDataRow(item);

                    if (string.IsNullOrWhiteSpace(row["DEGREE"].ToString()))
                    {
                        rowHandle = item;
                    }
                }

                if (rowHandle != -1)
                {
                    grdInspectionRequest.View.CheckRow(rowHandle, true);
                }
                else
                {
                    ShowMessage("NoDataToOSPRequest");//공정수입검사를 의뢰할 대상이 없습니다.      
                }
                
            }
        }

        /*
        /// <summary>
        /// 그리드 delete 버튼클릭 이벤트 
        /// 검사 결과가 등록된 검사의뢰는 삭제 할 수 없게 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdInspectionRequest_ToolbarDeletingRow(object sender, CancelEventArgs e)
        {
            DataTable dt = grdInspectionRequest.View.GetCheckedRows();

            foreach (DataRow row in dt.Rows)
            {
                if (!string.IsNullOrWhiteSpace(row["INSPECTIONRESULT"].ToString()))
                {
                    e.Cancel = true;
                    ShowMessage("CantDeleteInspected");
                    break;
                }
            }
        }
        */


        /*
        private void BtnSelectLot_Click(object sender, EventArgs e)
        {
            ProcessInspectionRequestLotPopup popup = new ProcessInspectionRequestLotPopup();
            popup.StartPosition = FormStartPosition.CenterParent;

            popup.LotSelectEvent += (dt) =>
            {
                BindingGrd(dt);
            };
            popup.ShowDialog(this);
        }*/
        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            //base.OnToolbarSaveClick();

            // TODO : 저장 Rule 변경
            //DataTable changed = grdInspectionRequest.View.GetCheckedRows();

            //MessageWorker messageWorker = new MessageWorker("SaveOSPInspectionRequest");

            //messageWorker.SetBody(new MessageBody()
            //{
            //    { "list", changed },
            //    { "fleg", "upsert" }
            //});

            //messageWorker.Execute();
        }

        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            //base.OnToolbarClick(sender, e);
            SmartButton btn = sender as SmartButton;

            if (btn.Name.ToString().Equals("Save"))
            {
                Btn_SaveClick(btn.Text);
            }
            else if (btn.Name.ToString().Equals("CancelRequest"))
            {
                Btn_CancelRequestClick();
            }
        }
        #endregion

        #region 검색

        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            // TODO : 조회 SP 변경
            var values = Conditions.GetValues();
            values.Add("RESOURCETYPE", "ProcessInspection");
            values.Add("ENTERPRISEID", Framework.UserInfo.Current.Enterprise);
            //values.Add("PLANTID", Framework.UserInfo.Current.Plant);
            values.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);

            DataTable dt = await SqlExecuter.QueryAsync("SelectOSPReqListUnionToReq", "10001", values);

            if (dt.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdInspectionRequest.DataSource = dt;

        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            InitializeConditionPopup_Vendor();
            InitializeConditionProcessSegmentId_Popup();
            // 작업장
            //CommonFunction.AddConditionAreaPopup("P_AREAID", 2.3, false, Conditions);
            InitializeConditionAreaId_Popup();
            // 품목
            CommonFunction.AddConditionProductPopup("P_PRODUCTDEFID", 2.4, true, Conditions);
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
            this.Conditions.GetControl<SmartComboBox>("P_PLANTID").EditValue = Framework.UserInfo.Current.Plant;
            this.Conditions.GetControl<SmartComboBox>("P_PLANTID").Enabled = false;
        }

        #endregion

        #region 조회조건 팝업

        /// <summary>
        /// 팝업형 조회조건 생성 - 작업장
        /// </summary>
        private void InitializeConditionAreaId_Popup()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("AreaType", "Area");
            //param.Add("PlantId", UserInfo.Current.Plant);
            param.Add("LanguageType", UserInfo.Current.LanguageType);
            param.Add("AREA", UserInfo.Current.Area);
            param.Add("P_USERID", UserInfo.Current.Id);

            var areaIdPopup = this.Conditions.AddSelectPopup("p_areaId", new SqlQuery("GetAuthorityUserUseArea", "10001", param), "AREANAME", "AREAID")
                .SetPopupLayout(Language.Get("SELECTAREAID"), PopupButtonStyles.Ok_Cancel, true, true)
                .SetPopupResultCount(1)
                .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                .SetPopupAutoFillColumns("AREANAME")
                .SetLabel("AREA")
                .SetRelationIds("P_PLANTID")
                .SetPosition(2.3);

            areaIdPopup.Conditions.AddTextBox("AREAIDNAME");

            areaIdPopup.GridColumns.AddTextBoxColumn("AREAID", 150);
            areaIdPopup.GridColumns.AddTextBoxColumn("AREANAME", 200);
        }

        private void InitializeConditionPopup_Vendor()
        {
            // 팝업 컬럼 설정
            var vendorPopup = Conditions.AddSelectPopup("P_VENDORID", new SqlQuery("GetVendorList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", "VENDORTYPE=Supplier"), "VENDORNAME", "VENDORID")
                                        .SetPopupLayout("VENDOR", PopupButtonStyles.Ok_Cancel, true, false)
                                        .SetPopupLayoutForm(400, 600)
                                        .SetPopupResultCount(1)
                                        .SetPosition(2.1)
                                        .SetRelationIds("P_PLANTID")
                                        .SetLabel("VENDOR")
                                        .SetPopupAutoFillColumns("VENDORNAME");

            // 팝업 조회조건
            vendorPopup.Conditions.AddTextBox("VENDORID")
                       .SetLabel("VENDOR");

            // 팝업 그리드
            vendorPopup.GridColumns.AddTextBoxColumn("VENDORID", 150);

            vendorPopup.GridColumns.AddTextBoxColumn("VENDORNAME", 200);
        }

        private void InitializeConditionProcessSegmentId_Popup()
        {
            var processSegmentIdPopup = this.Conditions.AddSelectPopup("P_PROCESSSEGMENTID", new SqlQuery("GetProcessSegmentList", "10002", $"PLANTID={UserInfo.Current.Plant}" ,$"LANGUAGETYPE={UserInfo.Current.LanguageType}", "PROCESSSEGMENTCLASSTYPE=MiddleProcessSegmentClass"), "PROCESSSEGMENTNAME", "PROCESSSEGMENTID")
                .SetPopupLayout(Language.Get("SELECTPROCESSSEGMENTID"), PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupResultCount(1)
                .SetPopupLayoutForm(800, 800, FormBorderStyle.FixedToolWindow)
                .SetPopupAutoFillColumns("PROCESSSEGMENTNAME")
                .SetLabel("PROCESSSEGMENT")
                .SetPosition(2.2);

            processSegmentIdPopup.Conditions.AddComboBox("PROCESSSEGMENTCLASSID_MIDDLE", new SqlQuery("GetProcessSegmentClassByType", "10001", $"PLANTID={UserInfo.Current.Plant}" ,$"LANGUAGETYPE={UserInfo.Current.LanguageType}", "PROCESSSEGMENTCLASSTYPE=MiddleProcessSegmentClass"), "PROCESSSEGMENTCLASSNAME", "PROCESSSEGMENTCLASSID")
                .SetLabel("MIDDLEPROCESSSEGMENT");
            processSegmentIdPopup.Conditions.AddTextBox("PROCESSSEGMENT");


            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME_TOP", 150)
                .SetLabel("LARGEPROCESSSEGMENT");
            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME_MIDDLE", 150)
                .SetLabel("MIDDLEPROCESSSEGMENT");
            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150);
            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
        }
        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            // TODO : 유효성 로직 변경
            grdInspectionRequest.View.CheckValidation();

            DataTable changed = grdInspectionRequest.View.GetCheckedRows();

            if (changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }

        #endregion

        #region Private Function
        private async void  SearchMainGrid()
        {
            await OnSearchAsync();
        }
        private  void SearchData()
        {
            var values = Conditions.GetValues();
            values.Add("RESOURCETYPE", "ProcessInspection");
            values.Add("ENTERPRISEID", Framework.UserInfo.Current.Enterprise);
            //values.Add("PLANTID", Framework.UserInfo.Current.Plant);
            values.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);

            DataTable dt =  SqlExecuter.Query("SelectOSPReqListUnionToReq", "10001", values);

            grdInspectionRequest.DataSource = dt;
        }

        /// <summary>
        /// 의뢰 취소 버튼 클릭시 체크된 행은 의뢰 취소하는 함수.
        /// </summary>
        private void Btn_CancelRequestClick()
        {
            DataTable toRequestCancel = grdInspectionRequest.View.GetCheckedRows();

            if (toRequestCancel.Rows.Count == 0)
            {
                ShowMessage("GridNoChecked");
                return;
            }

            MessageWorker messageWorker = new MessageWorker("SaveOSPInspectionRequest");

            messageWorker.SetBody(new MessageBody()
            {
                { "list", toRequestCancel },
                { "fleg", "delete" }
            });

            messageWorker.Execute();

            SearchMainGrid();
        }

        /// <summary>
        /// 저장 함수
        /// </summary>
        /// <param name="strtitle"></param>
        private void Btn_SaveClick(string strtitle)
        {
            grdInspectionRequest.View.CloseEditor();
            grdInspectionRequest.View.CheckValidation();

            DataTable changed = grdInspectionRequest.View.GetCheckedRows();

            if (changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                this.ShowMessage(MessageBoxButtons.OK, "NoSaveData");
                return;
            }

            DialogResult result = System.Windows.Forms.DialogResult.No;

            result = this.ShowMessage(MessageBoxButtons.YesNo, "InfoSave", strtitle);//저장하시겠습니까? 

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    this.ShowWaitArea();

                    MessageWorker messageWorker = new MessageWorker("SaveOSPInspectionRequest");

                    messageWorker.SetBody(new MessageBody()
                    {
                        { "list", changed },
                        { "fleg", "upsert" }
                    });

                    messageWorker.Execute();

                    ShowMessage("SuccessSave");
                    //재조회 
                    SearchData();
                }
                catch (Exception ex)
                {
                    this.ShowError(ex);
                }
                finally
                {
                    this.CloseWaitArea();


                }
            }
        }
        /*
        // TODO : 화면에서 사용할 내부 함수 추가
        /// <summary>
        /// lotid, LotHistKey 로 max차수의 인계처리여부를 확인하여 검사의뢰 유효성 체크하는 함수
        /// return 1 => 의뢰추가 불가
        ///       -1 => 의뢰추가 가능
        /// </summary>
        /// <param name="sLotId"></param>
        /// <returns></returns>
        private int checkGridLotId(string sLotId, string sLotHistKey)
        {
            DataTable grdTable = grdInspectionRequest.DataSource as DataTable;

            int flag = CheckDuplicate(SearchRequestedLotList(sLotId,sLotHistKey), sLotId, sLotHistKey);

            if (flag == -1)
            {
                if (grdTable.Rows.Count > 0)
                {
                    var grdAdded = grdTable.Copy().Rows.Cast<DataRow>()
                    .Where(r => string.IsNullOrWhiteSpace(r["DEGREE"].ToString()))
                    .ToList();

                    if(grdAdded.Count > 0)
                    return CheckDuplicate(grdAdded.CopyToDataTable(), sLotId, sLotHistKey);
                }

                return flag;
            }
            else
            {
                return flag;
            }


        }
        */
        /*
        /// <summary>
        /// LOTID를 조회하는 함수
        /// </summary>
        private void SearchLotId()
        {
            Dictionary<string, object> values = new Dictionary<string, object>()
            {
                {"ENTERPRISEID", Framework.UserInfo.Current.Enterprise},
                {"PLANTID",Framework.UserInfo.Current.Plant},
                {"LANGUAGETYPE",Framework.UserInfo.Current.LanguageType},
                {"LOTIDTXT", txtLotId.EditValue}
            };

            DataTable dt = SqlExecuter.Query("GetLotIdToInspRequest", "10001", values);

            if (dt.Rows.Count > 0)
            {
                BindingGrd(dt);
            }
            else
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }
        }
        */
        /*
        private void BindingGrd(DataTable dt)
        {
            foreach (DataRow row in dt.Rows)
            {
                string sLotid = row["LOTID"].ToString();
                string sLotHistKey = row["TXNHISTKEY"].ToString();
                int icheck = checkGridLotId(sLotid, sLotHistKey);
                if (icheck == -1)
                {
                    grdInspectionRequest.View.AddNewRow();
                    grdInspectionRequest.View.SetFocusedRowCellValue("ENTERPRISEID", UserInfo.Current.Enterprise);// 회사코드
                    grdInspectionRequest.View.SetFocusedRowCellValue("PLANTID", UserInfo.Current.Plant);// plantid
                    grdInspectionRequest.View.SetFocusedRowCellValue("LOTHISTKEY", row["TXNHISTKEY"]);// LOTHISTKEY
                    grdInspectionRequest.View.SetFocusedRowCellValue("LOTID", row["LOTID"]);// LOTID
                    grdInspectionRequest.View.SetFocusedRowCellValue("PRODUCTDEFID", row["PRODUCTDEFID"]);// 제품 ID
                    grdInspectionRequest.View.SetFocusedRowCellValue("PRODUCTDEFNAME", row["PRODUCTDEFNAME"]);// 제품명
                    grdInspectionRequest.View.SetFocusedRowCellValue("PRODUCTDEFVERSION", row["PRODUCTDEFVERSION"]);// 제품 정의 Version
                    grdInspectionRequest.View.SetFocusedRowCellValue("PROCESSSEGMENTNAME", row["PROCESSSEGMENTNAME"]);// 공정명
                    grdInspectionRequest.View.SetFocusedRowCellValue("PROCESSSEGMENTVERSION", row["PROCESSSEGMENTVERSION"]);// 공정 Version
                    grdInspectionRequest.View.SetFocusedRowCellValue("AREANAME", row["AREANAME"]);// 작업장 AREA 명
                    grdInspectionRequest.View.SetFocusedRowCellValue("VENDORNAME", row["VENDORNAME"]);// 협력사 명
                    grdInspectionRequest.View.SetFocusedRowCellValue("LOTHISTKEY", row["TXNHISTKEY"]);// LOTHISTKEY
                    grdInspectionRequest.View.SetFocusedRowCellValue("WORKENDPCSQTY", row["WORKENDPCSQTY"]);// WORKENDPCSQTY
                    grdInspectionRequest.View.SetFocusedRowCellValue("WORKENDPANELQTY", row["WORKENDPANELQTY"]);// WORKENDPANELQTY
                    grdInspectionRequest.View.SetFocusedRowCellValue("WORKENDMMQTY", row["WORKENDMMQTY"]);// WORKENDMMQTY
                    grdInspectionRequest.View.SetFocusedRowCellValue("REQUESTOR", UserInfo.Current.Id);// REQUESTOR
                    grdInspectionRequest.View.SetFocusedRowCellValue("REQUESTORNAME", UserInfo.Current.Name);// REQUESTORNAME
                    grdInspectionRequest.View.SetFocusedRowCellValue("REQUESTDATE", DateTime.Now.ToString("yyyy-MM-dd"));// REQUESTORNAME
                    grdInspectionRequest.View.SetFocusedRowCellValue("REQUESTDATETIME", DateTime.Now);// REQUESTORNAME

                }

            }


        }
        */
        /*
        /// <summary>
        /// 화면 로드시 저장된 의뢰목록중 차수가 가장큰 Row List를 조회해온다
        /// </summary>
        private DataTable SearchRequestedLotList(string lotId, string lotHistKey)
        {
            Dictionary<string, object> values = new Dictionary<string, object>()
            {
                {"LOTID", lotId},
                {"LOTHISTKEY",lotHistKey }
            };
            _requested = SqlExecuter.Query("GetOSPInspRequestedLot", "10001", values);

            return _requested;
        }

        private int CheckDuplicate(DataTable table, string sLotId, string sLotHistKey)
        {
            //lotId,lotHistKey의 인계처리여부
            var isHandOverProcess = table.AsEnumerable()
                .Where(r => r["LOTID"].ToString().Equals(sLotId) && r["LOTHISTKEY"].ToString().Equals(sLotHistKey))
                .Select(r => r["ISSEND"].ToString()).ToList();


            if (isHandOverProcess.Count == 0)
            {//lotId,lotHistKey 로 의뢰된 row없을때
                return -1;
            }
            else
            {//lotId,lotHistKey 로 의뢰된 row있을때
                if (isHandOverProcess[0].ToString().Equals("ReceivingCancel"))//의뢰된 row가 의뢰 취소 상태일때
                    return -1;
                else// 의뢰된 row가 검사하지않은상태 이거나 인계처리일 경우
                    return 1;
            }
        }
        */
        #endregion
    }

}
