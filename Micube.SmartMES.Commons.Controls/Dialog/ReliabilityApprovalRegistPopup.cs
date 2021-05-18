#region using

using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Grid.BandedGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion

namespace Micube.SmartMES.Commons.Controls
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > Audit 관리 > Audit 관리대장
    /// 업  무  설  명  : 협력업체 관리대장 현황 을 조회하여 점검결과를 등록한다.
    /// 생    성    자  : 유호석
    /// 생    성    일  : 2019-08-09
    /// 수  정  이  력  : 
    /// </summary>

    #region 참고정보
    /*
가)	"[요청부서],[승인부서],[수신부서]는 한명이상 존재 해야 한다"						
나)	각구역의 승인된 결재자는 구역에 수정이 불가능 하다						
다)	자기순번에 따라서 수정 권한이 주어진다						
라)	승인자 한명이라도 승인이 되면 검토자를 추가 할 수없다 						
마)	모든 결재 라인이 승인되면 [결재자추가] 버튼이 비활성화 되고 결재정보 그리드를 비활성화 시킨다						

순번	구분	결재상태				
1	검토자	NULL	>			검토자1은 다른 검토자를 추가 할 수있다
2	검토자	NULL				"          ,추가 되는 검토자는 순번이 3이 된다"
3	승인자	NULL				검토자1은 모든 사람을 삭제 할 수있다
4	승인자	NULL				"          ,가) 원칙에 충족하면 가능하다"
5	승인자	NULL				검토자1은 승인자5를 검토자로 변경 할 수있다
                        "         ,승인자5는 검토자3이 된다"
순번	구분	결재상태				
1	검토자	승인	>			검토자1은 수정 할 수없다
2	검토자	NULL				검토자2는 자기자신과 다음 순번들을 삭제 할 수있다
3	승인자	NULL				검토자2는 검토자1을 삭제 할 수  없다
4	승인자	NULL				검토자2는 또 다른 검토자를 추가 할 수있다
5	승인자	NULL				

순번	구분	결재상태				
1	검토자	승인	>			"검토자 1,2는 수정할 수없다"
2	검토자	승인				승인자3이 또 다른 사람을 검토자로 추가 할 수 있다
3	승인자	NULL				"           ,추가되는 검토자는 검토자3이 된다"
4	승인자	NULL				승인자3이 승인자4를 검토자로 변경 할 수 있다
5	승인자	NULL				          승인자4는 검토자3이 된다
                                "승인자3은 승인자 3,4,5를 삭제 할 수있다"

순번	구분	결재상태				
1	검토자	승인	>			승인자5는 자기 자신을 삭제 할 수있다
2	검토자	승인				"          ,이떄 구역이 모두 승인완료 이므로 메일을 보낸다"
3	승인자	승인				승인자5는 다른 사람을 승인자로 추가 할 수있다
4	승인자	승인				"         ,추가되는 승인자는 가장 느린 순번(6)을 갖는다"
5	승인자	NULL				승인자5는 자기자신을 검토자로 변경 할 수없다

순번	구분	결재상태				
1	승인자	NULL	>			승인자1은 검토자를 추가 할 수있다
2	승인자	NULL				"         이떄 추가 되는 검토자는 검토자1이 되고 승인자1,2,3은 승인자 2,3,4가 된다"
3	승인자	NULL				승인자1은 모든 사람을 삭제 할 수있다
                        "         ,가) 원칙에 충족하면 가능하다"

순번	구분	결재상태				
1	승인자	승인	>			승인자2는 검토자를 추가 할 수없다
2	승인자	NULL				승인자2는 승인자3을 검토자로 변경 할 수없다
3	승인자	NULL				
*/ 
    #endregion

    public partial class ReliabilityApprovalRegistPopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region Local Variables

        /// <summary>
        /// 팝업진입전 그리드의 Row
        /// </summary>
        public DataRow CurrentDataRow { get; set; }
        public DataTable QCApprovalDataTable { get; set; }

        /// <summary>
        /// 부모  Control 받기
        /// </summary>
        public SmartBandedGrid tParent { get; set; }
        DataTable _dtApprovalRole;
        string _sENTERPRISEID = string.Empty;
        string _sPLANTID = string.Empty;
        string _sVENDORID = string.Empty;
        string _sAREAID = string.Empty;
        string _sPROCESSSEGMENTID = string.Empty;
        string _sPROCESSSEGMENTVERSION = string.Empty;
        #endregion

        #region 생성자

        /// <summary>
        /// 생성자
        /// </summary>
        public ReliabilityApprovalRegistPopup(string sENTERPRISEID, string sPLANTID)
        {
            InitializeComponent();

            InitializeEvent();

            _sENTERPRISEID = sENTERPRISEID;
            _sPLANTID = sPLANTID;
        }

        /// <summary>
        /// 생성자
        /// </summary>
        public ReliabilityApprovalRegistPopup()
        {
            InitializeComponent();

            InitializeEvent();


        }

        public ReliabilityApprovalRegistPopup(string sENTERPRISEID, string sPLANTID, string sVENDORID, string sAREAID, string sPROCESSSEGMENTID,string sPROCESSSEGMENTVERSION)
        {
            InitializeComponent();
            InitializeEvent();

            _sENTERPRISEID = sENTERPRISEID;
            _sPLANTID = sPLANTID;
            _sVENDORID = sVENDORID;
            _sAREAID = sAREAID;
            _sPROCESSSEGMENTID = sPROCESSSEGMENTID;
            _sPROCESSSEGMENTVERSION = sPROCESSSEGMENTVERSION;
        }

        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            
            _dtApprovalRole = GetApprovalRoleAll();
            grdUser.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Restore;
            grdUser.View.SetIsReadOnly();
            grdUser.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdUser.View.AddTextBoxColumn("CHARGERID", 80).SetLabel("ID"); 
            grdUser.View.AddTextBoxColumn("USERNAME", 70).SetLabel("USERNAME"); 
            grdUser.View.AddTextBoxColumn("EMAILADDRESS", 180).SetLabel("EMAILADDRESS");
            grdUser.View.AddTextBoxColumn("DEPARTMENT", 100);
            grdUser.View.AddTextBoxColumn("POSITION", 200).SetIsHidden();
            grdUser.View.AddTextBoxColumn("CELLPHONENUMBER", 200).SetIsHidden();
            grdUser.View.AddTextBoxColumn("VALIDSTATE", 200).SetIsHidden();
            grdUser.View.PopulateColumns();

            //grdQCApproval1.View.SetIsReadOnly();
            grdQCApproval1.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdQCApproval1.View.AddTextBoxColumn("CHARGETYPE", 70).SetLabel("CLASS").SetTextAlignment(TextAlignment.Center);
            grdQCApproval1.View.AddTextBoxColumn("APPROVALSTATE", 80).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();//결제상태(승인여부)
            grdQCApproval1.View.AddTextBoxColumn("USERNAME", 80).SetLabel("USERNAME").SetIsReadOnly();
            grdQCApproval1.View.AddTextBoxColumn("DEPARTMENT", 130).SetLabel("DEPARTMENT").SetIsReadOnly();
            grdQCApproval1.View.AddTextBoxColumn("EMAILADDRESS", 150).SetLabel("EMAILADDRESS").SetIsReadOnly();
            grdQCApproval1.View.AddTextBoxColumn("CHARGERID", 130).SetIsHidden();
            grdQCApproval1.View.AddTextBoxColumn("PROCESSTYPE", 130).SetIsHidden();// 절차구분
            grdQCApproval1.View.AddTextBoxColumn("SEQUENCE", 130).SetIsHidden();
            grdQCApproval1.View.AddTextBoxColumn("REJECTCOMMENTS", 130).SetIsHidden();
            grdQCApproval1.View.AddTextBoxColumn("ENTERPRISEID", 130).SetIsHidden();
            grdQCApproval1.View.AddTextBoxColumn("PLANTID", 130).SetIsHidden();
            grdQCApproval1.View.AddTextBoxColumn("APPROVALTIME", 130).SetIsHidden();
            grdQCApproval1.View.AddTextBoxColumn("DESCRIPTION", 130).SetIsHidden();
            //grdQCApproval1.View.AddTextBoxColumn("GROUP_APPROVAL", 130).SetIsHidden();
            grdQCApproval1.View.PopulateColumns();


            //grdQCApproval2.View.SetIsReadOnly();
            grdQCApproval2.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdQCApproval2.View.AddTextBoxColumn("CHARGETYPE", 70).SetLabel("CLASS").SetTextAlignment(TextAlignment.Center);
            grdQCApproval2.View.AddTextBoxColumn("APPROVALSTATE", 80).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();//결제상태(승인여부)
            grdQCApproval2.View.AddTextBoxColumn("USERNAME", 80).SetLabel("USERNAME").SetIsReadOnly();
            grdQCApproval2.View.AddTextBoxColumn("DEPARTMENT", 130).SetLabel("DEPARTMENT").SetIsReadOnly();
            grdQCApproval2.View.AddTextBoxColumn("EMAILADDRESS", 150).SetLabel("EMAILADDRESS").SetIsReadOnly();
            grdQCApproval2.View.AddTextBoxColumn("CHARGERID", 130).SetIsHidden();
            grdQCApproval2.View.AddTextBoxColumn("PROCESSTYPE", 130).SetIsHidden();// 절차구분
            grdQCApproval2.View.AddTextBoxColumn("SEQUENCE", 130).SetIsHidden();
            grdQCApproval2.View.AddTextBoxColumn("REJECTCOMMENTS", 130).SetIsHidden();
            grdQCApproval2.View.AddTextBoxColumn("ENTERPRISEID", 130).SetIsHidden();
            grdQCApproval2.View.AddTextBoxColumn("PLANTID", 130).SetIsHidden();
            grdQCApproval2.View.AddTextBoxColumn("APPROVALTIME", 130).SetIsHidden();
            grdQCApproval2.View.AddTextBoxColumn("DESCRIPTION", 130).SetIsHidden();
            //grdQCApproval2.View.AddTextBoxColumn("GROUP_APPROVAL", 130).SetIsHidden();
            grdQCApproval2.View.PopulateColumns();

            //grdQCApproval3.View.SetIsReadOnly();
            grdQCApproval3.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdQCApproval3.View.AddTextBoxColumn("CHARGETYPE", 70).SetLabel("CLASS").SetTextAlignment(TextAlignment.Center);
            grdQCApproval3.View.AddTextBoxColumn("APPROVALSTATE", 80).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();//결제상태(승인여부)
            grdQCApproval3.View.AddTextBoxColumn("USERNAME", 80).SetLabel("USERNAME").SetIsReadOnly();
            grdQCApproval3.View.AddTextBoxColumn("DEPARTMENT", 130).SetLabel("DEPARTMENT").SetIsReadOnly();
            grdQCApproval3.View.AddTextBoxColumn("EMAILADDRESS", 150).SetLabel("EMAILADDRESS").SetIsReadOnly();
            grdQCApproval3.View.AddTextBoxColumn("CHARGERID", 130).SetIsHidden();
            grdQCApproval3.View.AddTextBoxColumn("PROCESSTYPE", 130).SetIsHidden();// 절차구분
            grdQCApproval3.View.AddTextBoxColumn("SEQUENCE", 130).SetIsHidden();
            grdQCApproval3.View.AddTextBoxColumn("REJECTCOMMENTS", 130).SetIsHidden();
            grdQCApproval3.View.AddTextBoxColumn("ENTERPRISEID", 130).SetIsHidden();
            grdQCApproval3.View.AddTextBoxColumn("PLANTID", 130).SetIsHidden();
            grdQCApproval3.View.AddTextBoxColumn("APPROVALTIME", 130).SetIsHidden();
            grdQCApproval3.View.AddTextBoxColumn("DESCRIPTION", 130).SetIsHidden();
            //grdQCApproval3.View.AddTextBoxColumn("GROUP_APPROVAL", 130).SetIsHidden();
            grdQCApproval3.View.PopulateColumns();

            RepositoryItemLookUpEdit repositoryItems = new RepositoryItemLookUpEdit();
            repositoryItems.DisplayMember = "CODENAME";
            repositoryItems.ValueMember = "CODEID";
            repositoryItems.DataSource = _dtApprovalRole;
            repositoryItems.ShowHeader = false;
            repositoryItems.NullText = "";
            repositoryItems.NullValuePromptShowForEmptyValue = true;
            repositoryItems.PopulateColumns();
            //repositoryItems.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            repositoryItems.Columns["CODEID"].Visible = false;

            grdQCApproval1.View.Columns["CHARGETYPE"].ColumnEdit = repositoryItems;
            grdQCApproval2.View.Columns["CHARGETYPE"].ColumnEdit = repositoryItems;
            grdQCApproval3.View.Columns["CHARGETYPE"].ColumnEdit = repositoryItems;

            Dictionary<string, object> values = new Dictionary<string, object>();
            DataTable dtApprovalState = SqlExecuter.Query("GetApprovalAllStateByReliabilityVerificationRequest", "10001", values);
            RepositoryItemLookUpEdit repositoryApprovalState = new RepositoryItemLookUpEdit();
            repositoryApprovalState.DisplayMember = "CODENAME";
            repositoryApprovalState.ValueMember = "CODEID";
            repositoryApprovalState.DataSource = dtApprovalState;
            repositoryApprovalState.ShowHeader = false;
            repositoryApprovalState.NullText = "";
            repositoryApprovalState.NullValuePromptShowForEmptyValue = true;
            repositoryApprovalState.PopulateColumns();
            //repositoryApprovalState.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            repositoryApprovalState.Columns["CODEID"].Visible = false;

            grdQCApproval1.View.Columns["APPROVALSTATE"].ColumnEdit = repositoryApprovalState;
            grdQCApproval2.View.Columns["APPROVALSTATE"].ColumnEdit = repositoryApprovalState;
            grdQCApproval3.View.Columns["APPROVALSTATE"].ColumnEdit = repositoryApprovalState;
        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            this.Load += ChemicalissuePopup_Load;
            grdUser.ToolbarAddingRow += View_ToolbarAddingRow;
            grdUser.HeaderButtonClickEvent += GrdManufacturingHistory_HeaderButtonClickEvent;

            grdQCApproval1.View.ShowingEditor += grdQCApproval_ShowingEditor;
            grdQCApproval2.View.ShowingEditor += grdQCApproval_ShowingEditor;
            grdQCApproval3.View.ShowingEditor += grdQCApproval_ShowingEditor;

            grdQCApproval1.View.CellValueChanged += grdQCApproval1_CellValueChanged;
            grdQCApproval2.View.CellValueChanged += grdQCApproval2_CellValueChanged;
            grdQCApproval3.View.CellValueChanged += grdQCApproval3_CellValueChanged;

            grdQCApproval1.View.ShownEditor += grdApproval_ShownEditor;
            grdQCApproval2.View.ShownEditor += grdApproval_ShownEditor;
            grdQCApproval3.View.ShownEditor += grdApproval_ShownEditor;

            //grdQCApproval1.View.MouseDown += grdApproval_MouseDown;
            //grdQCApproval2.View.MouseDown += grdApproval_MouseDown;
            //grdQCApproval3.View.MouseDown += grdApproval_MouseDown;
            //grdQCApproval4.View.MouseDown += grdApproval_MouseDown;

            btnClose.Click += BtnClose_Click;
            btnGetUserApproval.Click += BtnGetUserApproval_Click;
            txtID.KeyDown += TxtIDNAME_KeyDown;
            txtNAME.KeyDown += TxtIDNAME_KeyDown;
            //팝업저장버튼을 클릭시 이벤트
            btnSave.Click += BtnSave_Click;

            btnAdd1.Click += BtnApplovalAdd_Click;
            btnAdd2.Click += BtnApplovalAdd_Click;
            btnAdd3.Click += BtnApplovalAdd_Click;

            btnMinus1.Click += BtnApplovalMinus_Click;
            btnMinus2.Click += BtnApplovalMinus_Click;
            btnMinus3.Click += BtnApplovalMinus_Click;
        }

        #region 그리드이벤트
        private void grdApproval_ShownEditor(object sender, EventArgs e)
        {
            //APPROVALSTATE : 승인(Approval), 회수(Reclamation), 반려(Companion)
            //PROCESSTYPE : 기안(Draft), 검토(Review), 승인(Approval), 수신(Receiving)
            /*
             * CHARGETYPE : 기안(Draft) => RoleClassification1 Request 기안
             * SF_CODE : RoleClassification1 Request 기안
             * SF_CODE : RoleClassification2 Approval 승인
             * SF_CODE : RoleClassification2 Review  검토
            */
            DataRow row = (sender as SmartBandedGridView).GetFocusedDataRow();
            SmartBandedGridView viewApproval = sender as SmartBandedGridView;
            if (viewApproval.FocusedColumn.FieldName == "CHARGETYPE")
            {
                string processType = viewApproval.GetFocusedDataRow().GetString("PROCESSTYPE");
                string sCHARGERID = row["CHARGERID"].ToString();
                //기안(요청)그리드에 본인만 기안자로 인식하고 나머지는 승인,검토자로 인식한다.
                if (processType == "Draft")
                {
                    if(sCHARGERID == UserInfo.Current.Id)
                        processType = "Draft";
                    else
                        processType = "Review";
                }
                
                if (viewApproval.ActiveEditor != null)
                {
                    DataTable dtRoleClassification = GetApprovalState(processType);
                    DataTable dtSource = (sender as SmartBandedGridView).GridControl.DataSource as DataTable;

                    DevExpress.XtraEditors.LookUpEdit cboCHARGETYPE = ((DevExpress.XtraEditors.LookUpEdit)viewApproval.ActiveEditor);
                    cboCHARGETYPE.Properties.DataSource = dtRoleClassification;
                    //승인자가 존재하면 CHARGETYPE은 승인만 선택 가능 하다
                    if (dtSource.Rows.Cast<DataRow>().Where(r => r["CHARGETYPE"].ToString() == "Approval" && r["APPROVALSTATE"].ToString() == "Approval").ToList().Count > 0)
                    {
                        //RoleClassification2 => Approval 승인자,Review 검토자
                        //결재구분 (Review 검토자)는 삭제 한다
                        dtRoleClassification.AsEnumerable().Where(r => r.Field<string>("CODEID") == "Review").ToList().ForEach(s => s.Delete());
                    }

                    
                }
            }
        }
        //void grdApproval_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (e.Action == CollectionChangeAction.Refresh)
        //    {
        //        SmartBandedGridView gridView = sender as SmartBandedGridView;
        //        gridView.UnselectRow(gridView.FocusedRowHandle);
        //        DataRow row = (sender as SmartBandedGridView).GetFocusedDataRow();
        //        if (gridView.FocusedColumn.FieldName == "_INTERNAL_CHECKMARK_SELECTION_" && row["APPROVALSTATE"].ToString() == "Approval")
        //            //gridView.SetFocusedRowCellValue("_INTERNAL_CHECKMARK_SELECTION_", false);
        //            gridView.SetFocusedValue(false);
        //        //else
        //        //    gridView.SelectRow(gridView.FocusedRowHandle);
        //    }
        //}

        //private void grdApproval_MouseDown(object sender, MouseEventArgs e)
        //{
        //    GridView view = sender as GridView;
        //    GridHitInfo hitInfo = view.CalcHitInfo(e.Location);
        //    if (hitInfo.Column.FieldName == "_INTERNAL_CHECKMARK_SELECTION_")
        //    {
        //        DataRow row = (sender as SmartBandedGridView).GetFocusedDataRow();
        //        if(row["APPROVALSTATE"].ToString() == "Approval")
        //        {
        //            DXMouseEventArgs ea = DXMouseEventArgs.GetMouseArgs(e);
        //            ea.Handled = true;
        //        }
        //    }
        //}
        private void grdQCApproval1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName.Equals("CHARGETYPE"))
            {
                SmartBandedGridView gridView = sender as SmartBandedGridView;
                DataTable dt = gridView.GridDataSource as DataTable;
                dt.AcceptChanges();
                DataTable dt1 = dt.Select("chargetype = 'Request'", "").CopyToDataTable();
                DataTable dt2 = dt.Select("chargetype <> 'Request'", "chargetype desc,sequence").CopyToDataTable();
                dt1.Merge(dt2);
                grdQCApproval1.View.ClearDatas();
                grdQCApproval1.DataSource = dt1;
            }
        }
        private void grdQCApproval2_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName.Equals("CHARGETYPE"))
            {
                SmartBandedGridView gridView = sender as SmartBandedGridView;
                DataTable dt = gridView.GridDataSource as DataTable;
                dt.AcceptChanges();
                DataTable dt2 = dt.Copy();
                DataRow[] dra = dt2.Select("", "chargetype desc,sequence");
                if (dra.Length > 0)
                {

                    grdQCApproval2.View.ClearDatas();
                    grdQCApproval2.DataSource = dra.CopyToDataTable();
                }
            }
        }
        private void grdQCApproval3_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName.Equals("CHARGETYPE"))
            {
                SmartBandedGridView gridView = sender as SmartBandedGridView;
                DataTable dt = gridView.GridDataSource as DataTable;
                dt.AcceptChanges();
                DataTable dt2 = dt.Copy();
                DataRow[] dra = dt2.Select("", "chargetype desc,sequence");
                if (dra.Length > 0)
                {

                    grdQCApproval3.View.ClearDatas();
                    grdQCApproval3.DataSource = dra.CopyToDataTable();
                }
            }
        }
        private void grdQCApproval_ShowingEditor(object sender, CancelEventArgs e)
        {
            SmartBandedGridView gridView = sender as SmartBandedGridView;
            DataTable dtApproval = gridView.GridDataSource as DataTable;
            string sUser = UserInfo.Current.Id;
            //sUser = "20190079";
            int iThisGrdApprovalCnt = dtApproval.Rows.Cast<DataRow>().Where(r => r["APPROVALSTATE"].ToString() == "Approval").ToList().Count;
            if (dtApproval.Rows.Count > 0 && iThisGrdApprovalCnt == dtApproval.Rows.Count)
            {
                ShowMessage("ApprovalAddMinusCheck5");//승인 완료 그룹은 변경이 불가능 합니다
                e.Cancel = true;
                return;
            }

            DataRow row = (sender as SmartBandedGridView).GetFocusedDataRow();
            if (row["APPROVALSTATE"].ToString() == "Approval")
            {
                e.Cancel = true;
                return;
            }
            string sAPPROVALSTATE = row["APPROVALSTATE"].ToString();//승인(Approval), 회수(Reclamation), 반려(Companion)
            string sPROCESSTYPE = row["PROCESSTYPE"].ToString();//기안(Draft), 검토(Review), 승인(Approval), 수신(Receiving)


            ////////////////
            var IDraft = (grdQCApproval1.DataSource as DataTable).AsEnumerable().Where(r => r["CHARGETYPE"].ToString() == "Request" ).ToList();

            //요청자 ID
            var lRequesterID = IDraft.Where(r => r["CHARGETYPE"].ToString() == "Request").ToList().FirstOrDefault();
            if (lRequesterID["CHARGERID"].ToString() != sUser || lRequesterID["APPROVALSTATE"].ToString() == "Approval")//요청자가 로그인
            {
                //로그인 유저가 해당 구역에 존재 하는지 검사
                if (dtApproval.AsEnumerable().Where(r => r["CHARGERID"].ToString() == sUser).ToList().Count == 0)
                {
                    ShowMessage("ApprovalAddMinusCheck4");//자신이 속한 그룹에만 변경 권한이 있습니다
                    e.Cancel = true;
                    return;
                }
                else if (dtApproval.AsEnumerable().Where(r => r["CHARGERID"].ToString() == sUser).ToList().FirstOrDefault()["APPROVALSTATE"].ToString() == "Approval")
                {
                    ShowMessage("ApprovalAddMinusCheck2");//승인자는 변경 권한이 없습니다.
                    e.Cancel = true;
                    return;
                }

                //로그인한 유저의 순번
                string sSEQUENCE = dtApproval.AsEnumerable().Where(r => r["CHARGERID"].ToString() == sUser).FirstOrDefault()["SEQUENCE"].ToString();
                int iSEQUENCE = 0;
                Int32.TryParse(sSEQUENCE, out iSEQUENCE);
                //승인된 결재 순번
                int iMax = 0;
                var listAPPROVALSTATE = dtApproval.AsEnumerable().Where(r => r["APPROVALSTATE"].ToString() == "Approval").ToList();//승인(Approval)
                if (listAPPROVALSTATE.Count > 0)
                {
                    string maxValue = listAPPROVALSTATE.Max(r => r["SEQUENCE"]).ToString();
                    Int32.TryParse(maxValue, out iMax);

                    if ((iMax + 1) != iSEQUENCE)
                    {
                        ShowMessage("ApprovalAddMinusCheck3");//결재자 변경 권한이 아직 없습니다
                        e.Cancel = true;
                        return;
                    }
                }
                
            }
        }
        
        /// <summary>
        /// 그리드 해더 확장, 축소 버튼 에빈트
        /// </summary>
        private void GrdManufacturingHistory_HeaderButtonClickEvent(object sender, Framework.SmartControls.HeaderButtonClickArgs args)
        {
            if (args.ClickItem == GridButtonItem.Expand) // 확장
            {
                grdUser.Parent = this;
                grdUser.BringToFront();
            }
            else if (args.ClickItem == GridButtonItem.Restore) // 축소
            {
                grdUser.Parent = smartSplitTableLayoutPanel2;
                grdUser.BringToFront();
            }
        }
        private void View_ToolbarAddingRow(object sender, CancelEventArgs e)
        {
            SmartBandedGrid grid = sender as SmartBandedGrid;
            if (grid == null) return;

            DataTable dt = grid.DataSource as DataTable;
            if (dt.Rows.Cast<DataRow>().Where(r => r.RowState == DataRowState.Added).ToList().Count > 0)
            {
                e.Cancel = true;
            }
        }
        private void BtnGetUserApproval_Click(object sender, EventArgs e)
        {
            SearchUserApproval();
        }

        private void TxtIDNAME_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchUserApproval();
            }
        }
        #endregion


        private void BtnApplovalAdd_Click(object sender, EventArgs e)
        {
            DataTable checkedRows = grdUser.View.GetCheckedRows();
            if (checkedRows == null || checkedRows.Rows.Count == 0) return;

            SmartButton btn = sender as SmartButton;
            switch (btn.Name)
            {
                case "btnAdd1":
                    AddApprovalUser(checkedRows, grdQCApproval1, "Draft");
                    break;
                case "btnAdd2":
                    AddApprovalUser(checkedRows, grdQCApproval2, "Review");
                    break;
                case "btnAdd3":
                    AddApprovalUser(checkedRows, grdQCApproval3, "Approval");
                    break;
            }
            grdUser.View.CheckedAll(false);
            //for (int i = 0; i < grdUser.View.RowCount; i++)
            //{
            //    string sCHARGERID = grdUser.View.GetRowCellValue(i, "CHARGERID").ToString();
            //    if ((grdQCApproval1.DataSource as DataTable).AsEnumerable().Where(r => r["CHARGERID"].Equals(sCHARGERID)).ToList().Count() > 0)
            //    {
            //        var data = grdUser.View.row
            //        data
            //    }
            //}

        }
        private void BtnApplovalMinus_Click(object sender, EventArgs e)
        {
            string sUser = UserInfo.Current.Id;
            //sUser = "20190079";
            string sSEQUENCE = string.Empty;
            int iSEQUENCE = 0;
            int iMax = 0;
            
            SmartButton btn = sender as SmartButton;
            DataTable checkedRows = null;
            DataTable dtGrd = grdQCApproval1.DataSource as DataTable;//단순 초기화
            var listAPPROVALSTATE = dtGrd.AsEnumerable().Where(r => r["APPROVALSTATE"].ToString() == "Approval").ToList();//단순 초기화
            int iThisGrdApprovalCnt = 0;

            switch (btn.Name)
            {
                case "btnMinus1":
                    dtGrd = grdQCApproval1.DataSource as DataTable;
                    
                    //}
                    checkedRows = grdQCApproval1.View.GetCheckedRows();
                    if (checkedRows == null || checkedRows.Rows.Count == 0) return;
                    foreach (DataRow dr in checkedRows.Rows)
                    {
                        if(checkedRows.AsEnumerable().Where(r => r["PROCESSTYPE"].ToString() == "Draft" && r["CHARGETYPE"].ToString() == "Request").ToList().Count > 0)//기안자가 포함되었으면
                        {
                            ShowMessage("ApprovalDraftDelete");//요청자가 등록되지 않았습니다
                            return;
                        }

                        if (checkedRows.AsEnumerable().Where(r => r["APPROVALSTATE"].ToString() == "Approval" || r["APPROVALSTATE"].ToString() == "Reclamation" || r["APPROVALSTATE"].ToString() == "Companion").ToList().Count > 0)
                        {
                            ShowMessage("ApprovalDeleteCheckByState");//승인, 회수, 반려 인 결재상태는 삭제 될 수 없습니다.
                            return;
                        }
                        (grdQCApproval1.DataSource as DataTable).AsEnumerable().Where(r => r["CHARGERID"].Equals(dr["CHARGERID"].ToString())).ToList().ForEach(row => row.Delete());
                        (grdQCApproval1.DataSource as DataTable).AcceptChanges();
                    }
                    break;
                case "btnMinus2":
                    dtGrd = grdQCApproval2.DataSource as DataTable;
                    
                    checkedRows = grdQCApproval2.View.GetCheckedRows();
                    if (checkedRows == null || checkedRows.Rows.Count == 0) return;
                    foreach (DataRow dr in checkedRows.Rows)
                    {
                        if (checkedRows.AsEnumerable().Where(r => r["APPROVALSTATE"].ToString() == "Approval" || r["APPROVALSTATE"].ToString() == "Reclamation" || r["APPROVALSTATE"].ToString() == "Companion").ToList().Count > 0)
                        {
                            ShowMessage("ApprovalDeleteCheckByState");//승인, 회수, 반려 인 결재상태는 삭제 될 수 없습니다.
                            return;
                        }

                        (grdQCApproval2.DataSource as DataTable).AsEnumerable().Where(r => r["CHARGERID"].Equals(dr["CHARGERID"].ToString())).ToList().ForEach(row => row.Delete());
                        (grdQCApproval2.DataSource as DataTable).AcceptChanges();
                    }
                    break;
                case "btnMinus3":
                    dtGrd = grdQCApproval3.DataSource as DataTable;
                    
                    checkedRows = grdQCApproval3.View.GetCheckedRows();
                    if (checkedRows == null || checkedRows.Rows.Count == 0) return;
                    foreach (DataRow dr in checkedRows.Rows)
                    {
                        if (checkedRows.AsEnumerable().Where(r => r["APPROVALSTATE"].ToString() == "Approval" || r["APPROVALSTATE"].ToString() == "Reclamation" || r["APPROVALSTATE"].ToString() == "Companion").ToList().Count > 0)
                        {
                            ShowMessage("ApprovalDeleteCheckByState");//승인, 회수, 반려 인 결재상태는 삭제 될 수 없습니다.
                            return;
                        }

                        (grdQCApproval3.DataSource as DataTable).AsEnumerable().Where(r => r["CHARGERID"].Equals(dr["CHARGERID"].ToString())).ToList().ForEach(row => row.Delete());
                        (grdQCApproval3.DataSource as DataTable).AcceptChanges();
                    }
                    break;
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
            DataTable dt = grdQCApproval1.DataSource as DataTable;
            if (dt == null || dt.Rows.Count == 0)
            {
                ShowMessage("ApprovalApprobalCheck1");//요청부서가 등록되지 않았습니다.
                return;
            }
            dt = grdQCApproval3.DataSource as DataTable;
            if (dt == null || dt.Rows.Count == 0)
            {
                ShowMessage("ApprovalApprobalCheck2");//승인부서가 등록되지 않았습니다.
                return;
            }
            grdQCApproval1.View.CloseEditor();
            grdQCApproval2.View.CloseEditor();
            grdQCApproval3.View.CloseEditor();

            QCApprovalDataTable = (grdQCApproval1.DataSource as DataTable).Clone();
            QCApprovalDataTable.Merge(grdQCApproval1.DataSource as DataTable);
            QCApprovalDataTable.Merge(grdQCApproval2.DataSource as DataTable);
            QCApprovalDataTable.Merge(grdQCApproval3.DataSource as DataTable);

            if (!QCApprovalDataTable.Columns.Contains("APPROVALSTATE_ORG")) QCApprovalDataTable.Columns.Add("APPROVALSTATE_ORG");
            if (!QCApprovalDataTable.Columns.Contains("APPROVALTYPE")) QCApprovalDataTable.Columns.Add("APPROVALTYPE");

            for (int i = 0;i < QCApprovalDataTable.Rows.Count;i++)
            {
                DataRow dr = QCApprovalDataTable.Rows[i];
                dr["SEQUENCE"] = (i + 1);
                dr["APPROVALTYPE"] = "ReliabilityVerificationRequestNonRegular";
                dr["APPROVALSTATE_ORG"] = dr["APPROVALSTATE"];
            }
            //Request(기안), Approval(승인), Review(검토)
            int iCHARGETYPE = QCApprovalDataTable.Rows.Cast<DataRow>().Where(r => r["CHARGETYPE"].ToString() == "Request" || r["CHARGETYPE"].ToString() == "Approval" || r["CHARGETYPE"].ToString() == "Review").ToList().Count;
            if (QCApprovalDataTable.Rows.Count != iCHARGETYPE)
            {
                ShowMessage("RoleClassification");//역활구분을 선택해주세요.
                return;
            }
            try
            {
                this.ShowWaitArea();
                btnSave.Enabled = false;
                btnClose.Enabled = false;
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
        private void ChemicalissuePopup_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            InitializeGrid();
            //btnAdd1.Visible = false;
            //btnMinus1.Visible = false;
            if (QCApprovalDataTable != null && QCApprovalDataTable.Rows.Count > 0)
            {
                foreach (DataRow dr in QCApprovalDataTable.Select("PROCESSTYPE = 'Draft'", "SEQUENCE asc"))
                {
                    (grdQCApproval1.DataSource as DataTable).ImportRow(dr);
                }

                foreach (DataRow dr in QCApprovalDataTable.Select("PROCESSTYPE = 'Review'", "SEQUENCE asc"))
                {
                    (grdQCApproval2.DataSource as DataTable).ImportRow(dr);
                }

                foreach (DataRow dr in QCApprovalDataTable.Select("PROCESSTYPE = 'Approval'", "SEQUENCE asc"))
                {
                    (grdQCApproval3.DataSource as DataTable).ImportRow(dr);
                }
            }
            else
            {
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("P_USERIDNAME", UserInfo.Current.Id);
                //param.Add("AREAID", UserInfo.Current.LanguageType);

                DataTable userDt = SqlExecuter.Query("GetUserApproval", "10001", param);

                //if (userDt.Rows.Count > 0) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
                //{
                //    if(userDt.Columns.Contains("")
                //    //(grdQCApproval1.DataSource as DataTable).ImportRow(userDt.Rows[0]);
                //}

                AddApprovalUser(userDt, grdQCApproval1, "Draft");
            }

            SetAddMinusButtonEnable();
        }

        #endregion

        #region Private Function
        private void SetAddMinusButtonEnable()
        {
            string sUser = UserInfo.Current.Id;
            //sUser = "20190079";
            var IDraft = QCApprovalDataTable.AsEnumerable().Where(r => r["PROCESSTYPE"].ToString() == "Draft").OrderBy(r => r["SEQUENCE"]).ToList();
            var IReview = QCApprovalDataTable.AsEnumerable().Where(r => r["PROCESSTYPE"].ToString() == "Review").OrderBy(r => r["SEQUENCE"]).ToList();
            var IApproval = QCApprovalDataTable.AsEnumerable().Where(r => r["PROCESSTYPE"].ToString() == "Approval").OrderBy(r => r["SEQUENCE"]).ToList();
            var IReceiving = QCApprovalDataTable.AsEnumerable().Where(r => r["PROCESSTYPE"].ToString() == "Receiving").OrderBy(r => r["SEQUENCE"]).ToList();

            //요청자 ID
            var lRequesterID = IDraft.AsEnumerable().Where(r => r["CHARGETYPE"].ToString() == "Request").ToList().FirstOrDefault();
            if (lRequesterID["CHARGERID"].ToString() == sUser)//요청자가 로그인
            {
                if (lRequesterID["APPROVALSTATE"].ToString() == "Approval")//요청자가 승인
                {
                    //모든 버튼 비활성화
                    btnAdd1.Enabled = false;
                    btnMinus1.Enabled = false;
                    btnAdd2.Enabled = false;
                    btnMinus2.Enabled = false;
                    btnAdd3.Enabled = false;
                    btnMinus3.Enabled = false;

                    btnAdd1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
                    btnAdd2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
                    btnAdd3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;

                    btnMinus1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
                    btnMinus2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
                    btnMinus3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;

                    btnSave.Enabled = false;

                }
                else//요청자가 승인 안함
                {
                    //모든 버튼 활성화
                    btnAdd1.Enabled = true;
                    btnMinus1.Enabled = true;
                    btnAdd2.Enabled = true;
                    btnMinus2.Enabled = true;
                    btnAdd3.Enabled = true;
                    btnMinus3.Enabled = true;

                    btnAdd1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
                    btnAdd2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
                    btnAdd3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;

                    btnMinus1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
                    btnMinus2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
                    btnMinus3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
                }
            }
            else// 결재자 로그인
            {
                if ((IDraft.Where(r => r["APPROVALSTATE"].ToString() == "Approval").Count() == IDraft.Count() && IDraft.Count() > 0) //모두 승인됨
                    || IDraft.Where(r => r["CHARGERID"].ToString() == sUser).Count() == 0//결재부서에 없음
                    || IDraft.Where(r => r["CHARGERID"].ToString() == sUser && r["APPROVALSTATE"].ToString() == "Approval").Count() == 1//승인자
                    || !CheckSequence(IDraft))//결재순서
                {
                    //Draft 비활성화
                    btnAdd1.Enabled = false;
                    btnMinus1.Enabled = false;

                    btnAdd1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
                    btnMinus1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
                }
                if ((IReview.Where(r => r["APPROVALSTATE"].ToString() == "Approval").Count() == IReview.Count()  && IReview.Count() > 0)//모두 승인됨
                    || IReview.Where(r => r["CHARGERID"].ToString() == sUser).Count() == 0//결재부서에 없음
                    || IReview.Where(r => r["CHARGERID"].ToString() == sUser && r["APPROVALSTATE"].ToString() == "Approval").Count() == 1//승인자
                    || !CheckSequence(IReview))//결재순서
                {
                    //Review 비활성화
                    btnAdd2.Enabled = false;
                    btnMinus2.Enabled = false;

                    btnAdd2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
                    btnMinus2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
                }
                if ((IApproval.Where(r => r["APPROVALSTATE"].ToString() == "Approval").Count() == IApproval.Count() && IApproval.Count() > 0)//모두 승인됨
                    || IApproval.Where(r => r["CHARGERID"].ToString() == sUser).Count() == 0//결재부서에 없음
                    || IApproval.Where(r => r["CHARGERID"].ToString() == sUser && r["APPROVALSTATE"].ToString() == "Approval").Count() == 1//승인자
                    || !CheckSequence(IApproval))//결재순서
                {
                    //Approval 비활성화
                    btnAdd3.Enabled = false;
                    btnMinus3.Enabled = false;

                    btnAdd3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
                    btnMinus3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
                }
            }

            //if (!btnAdd1.Enabled
            //    && !btnAdd2.Enabled
            //    && !btnAdd3.Enabled
            //    && !btnAdd4.Enabled
            //    && !btnMinus1.Enabled
            //    && !btnMinus2.Enabled
            //    && !btnMinus3.Enabled
            //    && !btnMinus4.Enabled)
            //{
            //    btnSave.Enabled = false;
            //}
        }

        private bool CheckSequence(List<DataRow> list)
        {
            bool enable = true;
            string sUser = UserInfo.Current.Id;
            //sUser = "20190079";
            //로그인한 유저의 순번
            string sSEQUENCE = string.Empty;
            var ISEQUENCE = list.Where(r => r["CHARGERID"].ToString() == sUser);
            if (ISEQUENCE.Count() > 0)
                sSEQUENCE = ISEQUENCE.FirstOrDefault()["SEQUENCE"].ToString();
            int iSEQUENCE = 0;
            Int32.TryParse(sSEQUENCE, out iSEQUENCE);
            //승인된 결재 순번
            int iMax = 0;
            var lAPPROVALSTATE = list.Where(r => r["APPROVALSTATE"].ToString() == "Approval").ToList();//승인(Approval)
            if (lAPPROVALSTATE.Count > 0)
            {
                string maxValue = lAPPROVALSTATE.Max(r => r["SEQUENCE"]).ToString();
                Int32.TryParse(maxValue, out iMax);

                if ((iMax + 1) != iSEQUENCE)
                {
                    //ShowMessage("ApprovalAddMinusCheck3");//결재자 변경 권한이 아직 없습니다
                    //return;
                    enable = false;
                }
            }

            return enable;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt">체크된 유저</param>
        /// <param name="grd">복사될 그리드</param>
        /// <param name="sPROCESSTYPE">Draft, Review, Approval, Receiving</param>
        private void AddApprovalUser(DataTable dt, SmartBandedGrid grd, string sPROCESSTYPE)
        {
            string sUser = UserInfo.Current.Id;
            //sUser = "20190079";
            DataTable dtGrd = grd.DataSource as DataTable;
            int iThisGrdApprovalCnt = dtGrd.Rows.Cast<DataRow>().Where(r => r["APPROVALSTATE"].ToString() == "Approval").ToList().Count;
            //if (dtGrd.Rows.Count > 0 && iThisGrdApprovalCnt == dtGrd.Rows.Count)
            //{
            //    ShowMessage("ApprovalAddMinusCheck5");//승인 완료 그룹은 변경이 불가능 합니다
            //    return;
            //}
            //var lChargerID = dtGrd.AsEnumerable().Where(r => r["CHARGERID"].ToString() == sUser).ToList();
            //요청자 ID
            //var lRequesterID = (grdQCApproval1.DataSource as DataTable).AsEnumerable().Where(r => r["CHARGETYPE"].ToString() == "Request").ToList().FirstOrDefault();
            ////요청자가 로그인했고 승인 했다면
            //if (lRequesterID["CHARGERID"].ToString() != sUser || lRequesterID["APPROVALSTATE"].ToString() == "Approval")
            //{
            //    if (lChargerID.Count == 0)
            //    {
            //        ShowMessage("ApprovalAddMinusCheck4");//자신이 속한 그룹에만 변경 권한이 있습니다
            //        return;
            //    }
            //}
           
            //if (lChargerID.Count() > 0 && lChargerID.FirstOrDefault()["APPROVALSTATE"].ToString() == "Approval")
            //{
            //    ShowMessage("ApprovalAddMinusCheck2");//승인자는 변경 권한이 없습니다.
            //    return;
            //}
            //로그인한 유저의 순번
            string sSEQUENCE = string.Empty;
            var ISEQUENCE = dtGrd.AsEnumerable().Where(r => r["CHARGERID"].ToString() == sUser);
            if(ISEQUENCE.Count() > 0)
                sSEQUENCE = ISEQUENCE.FirstOrDefault()["SEQUENCE"].ToString();
            int iSEQUENCE = 0;
            Int32.TryParse(sSEQUENCE, out iSEQUENCE);
            //승인된 결재 순번
            int iMax = 0;
            var lAPPROVALSTATE = dtGrd.AsEnumerable().Where(r => r["APPROVALSTATE"].ToString() == "Approval").ToList();//승인(Approval)
            if (lAPPROVALSTATE.Count > 0)
            {
                string maxValue = lAPPROVALSTATE.Max(r => r["SEQUENCE"]).ToString();
                Int32.TryParse(maxValue, out iMax);

                if ((iMax + 1) != iSEQUENCE)
                {
                    ShowMessage("ApprovalAddMinusCheck3");//결재자 변경 권한이 아직 없습니다
                    return;
                }
            }

            if (!dt.Columns.Contains("ENTERPRISEID")) dt.Columns.Add("ENTERPRISEID");
            if (!dt.Columns.Contains("PLANTID")) dt.Columns.Add("PLANTID");
            if (!dt.Columns.Contains("SEQUENCE")) dt.Columns.Add("SEQUENCE", typeof(String));
            if (!dt.Columns.Contains("PROCESSTYPE")) dt.Columns.Add("PROCESSTYPE");//절차구분
            if (!dt.Columns.Contains("APPROVALSTATE")) dt.Columns.Add("APPROVALSTATE");//결제상태
            if (!dt.Columns.Contains("APPROVALSTATE_ORG")) dt.Columns.Add("APPROVALSTATE_ORG");//결제상태
            if (!dt.Columns.Contains("CHARGETYPE")) dt.Columns.Add("CHARGETYPE");//역할구분 Request(기안), Approval(승인), Review(검토)
            if (!dt.Columns.Contains("EMAILADDRESS")) dt.Columns.Add("EMAILADDRESS");
            //if (!dt.Columns.Contains("GROUP_APPROVAL")) dt.Columns.Add("GROUP_APPROVAL");//그룹결재완료flag

            DataTable dt1 = grdQCApproval1.DataSource as DataTable;
            DataTable dt2 = grdQCApproval2.DataSource as DataTable;
            DataTable dt3 = grdQCApproval3.DataSource as DataTable;

            DataTable dtAll = new DataTable();
            dtAll.Merge(dt1);
            dtAll.Merge(dt2);
            dtAll.Merge(dt3);
            foreach (DataRow dr in dt.Rows)
            {
                int iThisGrd = dtGrd.Rows.Cast<DataRow>().Where(r => r["CHARGERID"].ToString() == dr["CHARGERID"].ToString()).ToList().Count;
                int iAllGrd = dtAll.Rows.Cast<DataRow>().Where(r => r["CHARGERID"].ToString() == dr["CHARGERID"].ToString()).ToList().Count;
                if (iThisGrd == 0 && iAllGrd == 0)//동일인 체크
                {
                    dr["ENTERPRISEID"] = _sENTERPRISEID;
                    dr["PLANTID"] = _sPLANTID;
                    string max = string.Empty;
                    if (dtGrd.Rows.Count > 0)
                    {
                        object o = dtGrd.AsEnumerable().Max(r => r["SEQUENCE"].ToString());
                        max = o.ToString();
                    }
                    iMax = 0;
                    Int32.TryParse(max, out iMax);
                    dr["SEQUENCE"] = (iMax + 1).ToString();
                    dr["PROCESSTYPE"] = sPROCESSTYPE;
                    if (grd.Name == "grdQCApproval1")
                    {
                        if (dtGrd != null && dtGrd.Rows.Count > 0)
                            dr["CHARGETYPE"] = "";//Request(기안), Approval(승인), Review(검토)
                        else
                            dr["CHARGETYPE"] = "Request";//Request(기안), Approval(승인), Review(검토)
                    }

                    //if (grd.Name == "grdQCApproval1" && dtGrd.Rows.Count > 0)
                    //{
                    //    ShowMessage("ApprovalDraftInfo");//기안자는 1명만 허용됩니다
                    //    return;
                    //}
                    //else
                    //    dtGrd.ImportRow(dr);
                    dtGrd.ImportRow(dr);
                }
                else
                {
                    ShowMessage("ApprovalAllInfo");//동일한 사람이 다른 결제정보에 존재 합니다
                    return;
                }
            }
        }
        /// <summary>
        /// 사용자 검색
        /// </summary>
        private void SearchUserApproval()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("P_USERIDNAME",  txtID.Text);
            param.Add("P_USERNAME", txtNAME.Text);
            //param.Add("AREAID", UserInfo.Current.LanguageType);

            DataTable userDt = SqlExecuter.Query("GetUserApproval", "10001", param);

            if (userDt.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdUser.DataSource = userDt;
        }
        private DataTable GetApprovalRoleAll()
        {
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("CODECLASSID", "'RoleClassification1','RoleClassification2'");
            return SqlExecuter.Query("GetApprovalAllRoleByReliabilityVerificationRequest", "00001", values);
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
                case "Receiving":
                    codeClassID = "RoleClassification2";
                    break;
                default:
                    codeClassID = "RoleClassification1";
                    break;
            }
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("CODECLASSID", codeClassID);
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            return SqlExecuter.Query("GetCodeList", "00001", values);
        }
        #endregion

    }
}
