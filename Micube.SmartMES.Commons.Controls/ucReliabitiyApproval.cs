#region using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Micube.Framework.SmartControls;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.SmartMES.Commons.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using System.Text.RegularExpressions;
#endregion

namespace Micube.SmartMES.Commons.Controls
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 결제정보저장 User Control
    /// 업  무  설  명  : 품질관리에서 사용되는 결제정보저장 User Control이다.
    /// 생    성    자  : 유호석
    /// 생    성    일  : 2019-09-10
    /// 
    /// 
    /// </summary>
    public partial class ucReliabitiyApproval : UserControl, IDisposable, IEventAggregatorSubscriber
    {
        #region Local Variables

        SmartConditionBaseForm _form = new SmartConditionBaseForm();

        /// <summary>
        /// 임시저장값을 담을 변수
        /// </summary>
        DataTable _dtApprovalState;
        //저장시 결재부서별 승인완료가 변경됬는지 판단하기 위해
        public DataTable _dtApproval = null;
        public DataRow ParentDataRow { get; set; }

        /// <summary>
        /// 1. 데이터그리드에 바인딩 한다.
        /// 2. 결재선에 로그인한 사람이 포함되어 있어야 결재자 버튼을 활성화 할 수 있다.
        /// </summary>
        public DataTable SetApproval
        {
            set {
                if (value.Rows.Count == 0)
                {
                    Dictionary<string, object> param = new Dictionary<string, object>();
                    param.Add("P_REQUESTNO", string.Empty);
                    param.Add("P_USERIDNAME", UserInfo.Current.Id);
                    DataTable dtApproval = SqlExecuter.Query("GetQCApproval", "10001", param);                     //결재 정보

                    DataTable userDt = SqlExecuter.Query("GetUserApproval", "10001", param);

                    DataRow newRow = dtApproval.NewRow();
                    newRow["CHARGERID"] = UserInfo.Current.Id;
                    newRow["CHARGETYPE"] = "Request";
                    newRow["PROCESSTYPE"] = "Draft";
                    newRow["SEQUENCE"] = "1";
                    newRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                    newRow["PLANTID"] = UserInfo.Current.Plant;
                    newRow["USERNAME"] = userDt.Rows.Count > 0 ? userDt.Rows[0]["USERNAME"].ToString() : string.Empty;
                    newRow["DEPARTMENT"] = userDt.Rows.Count > 0 ? userDt.Rows[0]["DEPARTMENT"].ToString() : string.Empty;
                    newRow["EMAILADDRESS"] = userDt.Rows.Count > 0 ? userDt.Rows[0]["EMAILADDRESS"].ToString() : string.Empty;
                    dtApproval.Rows.Add(newRow);
                    dtApproval.AcceptChanges();

                    _dtApproval = dtApproval;
                    grdApproval.DataSource = dtApproval;
                    (grdApproval.DataSource as DataTable).Rows[0].SetAdded();
                }
                else
                {
                    _dtApproval = value;
                    grdApproval.DataSource = value;
                }
                Approval_Enable();
            }
        }
        #endregion

        #region 생성자

        /// <summary>
        /// 생성자
        /// </summary>
        public ucReliabitiyApproval()
        {
            InitializeComponent();

            if (!smartSplitTableLayoutPanel1.IsDesignMode())
            {
                InitializePopup();

                InitializeGrid();

                InitializeDefaultValue();

                InitializeEvent();
            }
        }

        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            _dtApprovalState = GetApprovalStateAll();
            grdApproval.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Restore;
            //grdApproval.View.SetIsReadOnly();
            grdApproval.View.AddComboBoxColumn("PROCESSTYPE", 100, new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "CODECLASSID=ProcedureType"))
                .SetTextAlignment(TextAlignment.Center).SetIsReadOnly();// 절차구분
            //grdApproval.View.AddTextBoxColumn("CHARGETYPE", 200).SetTextAlignment(TextAlignment.Center);//역할구분
            grdApproval.View.AddComboBoxColumn("CHARGETYPE", 100, new SqlQuery("GetApprovalAllRoleByReliabilityVerificationRequest", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "CODECLASSID='RoleClassification1','RoleClassification2'"))
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("CLASS")
                .SetIsReadOnly();
            //grdApproval.View.AddTextBoxColumn("CHARGETYPENAME", 100).SetTextAlignment(TextAlignment.Center).SetLabel("CLASS");//역할구분
            grdApproval.View.AddSpinEditColumn("USERNAME", 100).SetLabel("OWNERNAME").SetIsReadOnly().SetIsReadOnly().SetTextAlignment(TextAlignment.Center); //담당자
            grdApproval.View.AddTextBoxColumn("DEPARTMENT", 100).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);//부서
            grdApproval.View.AddTextBoxColumn("APPROVALSTATE", 100).SetTextAlignment(TextAlignment.Center);//결제상태(승인여부)
            grdApproval.View.AddTextBoxColumn("REJECTCOMMENTS", 400);//반려사유


            grdApproval.View.AddTextBoxColumn("SEQUENCE", 200).SetIsHidden();//순서

            grdApproval.View.AddSpinEditColumn("ENTERPRISEID", 100).SetIsHidden();
            grdApproval.View.AddSpinEditColumn("PLANTID", 100).SetIsHidden();

            grdApproval.View.AddTextBoxColumn("CHARGERID", 200).SetIsHidden();//담당자 ID
            grdApproval.View.AddTextBoxColumn("APPROVALTIME", 200).SetIsHidden();//결제시간
            grdApproval.View.AddTextBoxColumn("DESCRIPTION", 200).SetIsHidden();//
            grdApproval.View.AddTextBoxColumn("APPROVALNO", 200).SetIsHidden();//결제번호
            grdApproval.View.AddTextBoxColumn("APPROVALTYPE", 200).SetIsHidden();//결제구분
            //grdApproval.View.AddTextBoxColumn("GROUP_APPROVAL", 130);

            grdApproval.View.PopulateColumns();

            //RepositoryItemDateEdit date = new RepositoryItemDateEdit();
            //date.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            //date.Mask.EditMask = "yyyy-MM-dd";
            //date.Mask.UseMaskAsDisplayFormat = true;

            //grdApproval.View.Columns["APPROVALTIME"].ColumnEdit = date;

            RepositoryItemLookUpEdit repositoryItems = new RepositoryItemLookUpEdit();
            repositoryItems.DisplayMember = "CODENAME";
            repositoryItems.ValueMember = "CODEID";
            repositoryItems.DataSource = _dtApprovalState;
            repositoryItems.ShowHeader = false;
            repositoryItems.NullText = "";
            repositoryItems.NullValuePromptShowForEmptyValue = true;
            repositoryItems.PopulateColumns();
            //repositoryItems.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            repositoryItems.Columns["CODEID"].Visible = false;

            grdApproval.View.Columns["APPROVALSTATE"].ColumnEdit = repositoryItems;
        }

        /// <summary>
        /// 로드시 컨트롤에 기본값
        /// </summary>
        private void InitializeDefaultValue()
        {
        }

        #endregion

        #region Popup

        /// <summary>
        /// Popup 초기화
        /// </summary>
        private void InitializePopup()
        {
            
        }
        #endregion

        #region Event

        /// <summary>
        /// 이벤트 초기화
        /// </summary>
        private void InitializeEvent()
        {
            this.Load += Approval_Load;
            grdApproval.View.ShowingEditor += View_ShowingEditor;
            grdApproval.View.ShowingEditor += grdApproval_ShowingEditor;
            grdApproval.View.ShownEditor += grdApproval_ShownEditor;
            grdApproval.View.CellValueChanged += View_CellValueChanged;
            grdApproval.HeaderButtonClickEvent += GrdAuditManageregist_HeaderButtonClickEvent;

            btnADDAPPROVAL.Click += BtnADDAPPROVAL_Click;
        }

        
        /// <summary>
		/// 결제담당자 검사
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void View_ShowingEditor(object sender, CancelEventArgs e)
        {
            //APPROVALSTATE : 승인(Approval), 회수(Reclamation), 반려(Companion)
            //PROCESSTYPE : 기안(Draft), 검토(Review), 승인(Approval), 수신(Receiving)
            DataRow row = grdApproval.View.GetFocusedDataRow();
            GridView view = sender as GridView;
            string sUser = UserInfo.Current.Id;
            //sUser = "20190079";
            if (!view.FocusedColumn.FieldName.Equals("APPROVALSTATE"))
            {
                return;
            }
            if (row["APPROVALSTATE"].ToString().ToUpper() == "APPROVAL")
            {
                //e.Cancel = true;
                //return;
            }
            DataTable dtApproval = grdApproval.DataSource as DataTable;

            string sAPPROVALSTATE = row["APPROVALSTATE"].ToString();//승인(Approval), 회수(Reclamation), 반려(Companion)
            string sPROCESSTYPE = row["PROCESSTYPE"].ToString();//기안(Draft), 검토(Review), 승인(Approval), 수신(Receiving)

            //모두 반려또는 회수상태일떔만 기안(Draft)에서 승인(Approval)으로 변경할수 있다
            //if (dtApproval.AsEnumerable().Where(r => r["APPROVALSTATE_ORG"].ToString() == "Reclamation" || r["APPROVALSTATE_ORG"].ToString() == "Companion").ToList().Count > 0)
            //{
            //    if (sPROCESSTYPE == "Draft" && sAPPROVALSTATE == "Reclamation")
            //    {
            //        //회수이외 모든 차수에 반려가 됬을떄 요청자가  결제상태를 변경시 
            //    }
            //    else
            //    {
            //        e.Cancel = true;
            //        throw MessageException.Create("ApprovalInValid");
            //        //ShowMessage("ApprovalInValid");//회수 또는 반려된 결제 입니다
            //    }
            //}

            //if (dtApproval.AsEnumerable().Where(r => r["APPROVALSTATE_ORG"].ToString() == "Reclamation" || r["APPROVALSTATE_ORG"].ToString() == "Companion").ToList().Count > 0)
            //{
            //    ShowMessage("ApprovalInValid");//회수 또는 반려된 결제 입니다
            //    e.Cancel = true;
            //    return;
            //}
            //Reclamation
            if (row["CHARGERID"].ToString() != sUser)
            {
                //e.Cancel = true;
                //MessageBox.Show("결제담당자가 아님니다.");
                throw MessageException.Create("ApprovalChargeInfo");//결제담당자가 아님니다.
            }
            string sSEQUENCE = row["SEQUENCE"].ToString();
            int iSEQUENCE = 0;
            Int32.TryParse(sSEQUENCE, out iSEQUENCE);

            int iMax = 0;            
            var listAPPROVALSTATE = dtApproval.AsEnumerable().Where(r => r["APPROVALSTATE_ORG"].ToString() == "Approval").ToList();//승인(Approval)
            if (listAPPROVALSTATE.Count > 0)
            {
                string maxValue = listAPPROVALSTATE.Max(r => r["SEQUENCE"]).ToString();
                Int32.TryParse(maxValue, out iMax);
            }

            // 현재 선택된 결제 상태가 회수가 아니면 결재 순서여부 체크-회수(Reclamation)
            if (row["APPROVALSTATE_ORG"].ToString() != "Reclamation")
            {
                if ((iMax + 1) != iSEQUENCE)
                {
                    //e.Cancel = true;
                    //MessageBox.Show("결제 순서가 올바르지 않습니다.");
                    throw MessageException.Create("ApprovalSequenceInfo");//결제 순서가 올바르지 않습니다.
                }
            }
            //var s = dtApproval.Rows.Cast<DataRow>().Where(r => r["APPROVALSTATE"].ToString() == "Approval").ToList();
        }
        private void grdApproval_ShowingEditor(object sender, CancelEventArgs e)
        {
            ////현재 등록상태가 아닌 상태는 수정이 불가능하게 변경 추후 결재 도입시 변경요망
            //if (grdApproval.View.GetFocusedDataRow().GetString("TOOLPROGRESSSTATUS") != "Create")
            //{
            //    e.Cancel = true;
            //}
            //else if (grdApproval.View.FocusedColumn.FieldName == "APPROVALSTATE")
            //{
            //    if (grdApproval.View.GetFocusedDataRow().GetString("TOOLMAKETYPE") == "Add" || grdApproval.View.GetFocusedDataRow().GetString("TOOLMAKETYPE") == "New")
            //    {
            //        e.Cancel = true;
            //    }
            //}
        }
        private void grdApproval_ShownEditor(object sender, EventArgs e)
        {
            string processType = grdApproval.View.GetFocusedDataRow().GetString("PROCESSTYPE");
            string chargeType = grdApproval.View.GetFocusedDataRow().GetString("CHARGETYPE");//구분

            if (grdApproval.View.FocusedColumn.FieldName == "APPROVALSTATE")
            {
                if (grdApproval.View.ActiveEditor != null)
                {
                    ((DevExpress.XtraEditors.LookUpEdit)grdApproval.View.ActiveEditor).Properties.DataSource = GetApprovalState(processType, chargeType);
                }
            }
            //else if (grdApproval.View.FocusedColumn.FieldName == "CHARGETYPE")
            //{
                
            //    //기안(요청)그리드에 본인만 기안자로 인식하고 나머지는 승인,검토자로 인식한다.
            //    //processType == "Draft", chargeType == "Request"   : 결제 요청자
            //    //processType == "Draft", chargeType == "Approval"  : 결제 승인/검토자

            //    if (processType == "Draft" && chargeType == "Request")
            //    {
            //        processType = "Review";
            //    }
            //    else
            //        processType = "Review";

            //    if (grdApproval.View.ActiveEditor != null)
            //    {
            //        ((DevExpress.XtraEditors.LookUpEdit)grdApproval.View.ActiveEditor).Properties.DataSource = GetChargeType(processType);
            //    }
            //}
        }
        private void View_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (grdApproval.View.FocusedRowHandle < 0) return;

            DataRow row = grdApproval.View.GetFocusedDataRow();
            if (e.Column.FieldName.Equals("APPROVALSTATE"))
            {
                DataTable dtApproval = grdApproval.DataSource as DataTable;
                string sAPPROVALSTATE = row["APPROVALSTATE"].ToString();//승인(Approval), 회수(Reclamation), 반려(Companion)
                string sPROCESSTYPE = row["PROCESSTYPE"].ToString();//기안(Draft), 검토(Review), 승인(Approval), 수신(Receiving)
                string sSEQUENCE = row["SEQUENCE"].ToString();
                //절차구분(ProcedureType)
                //- 기안(Draft)
                //- 검토(Review)
                //- 승인(Approval)
                //- 수신(Receiving)

                //processType == "Draft", chargeType == "Request"   : 결제 요청자
                //processType == "Draft", chargeType == "Approval"  : 결제 승인/검토자
                if (sAPPROVALSTATE == "Companion" && sPROCESSTYPE != "Draft")
                {
                    //검토자, 승인자가 반려하면 
                    //1)절차구분(검토, 승인) 반려상태로
                    //2)절차구분(요청) and 역활구분(기안) 회수상태로 변경한다
                    //3)절차구분(요청) and 역활구분(검토,승인) 반려상태로 변경한다
                    DataRow[] drs = dtApproval.Select("SEQUENCE < " + sSEQUENCE);//수신(Receiving)은 반려가 없다
                    if (drs != null && drs.Length > 0)
                    {
                        foreach (DataRow dr in drs)
                        {
                            if(dr["PROCESSTYPE"].ToString()== "Draft")
                            {
                                if(dr["CHARGETYPE"].ToString() == "Request")//역활구분 :기안(Request), 검토(Review), 승인(Approval)
                                    dr["APPROVALSTATE"] = "Reclamation";//회수
                                //else
                                    //dr["APPROVALSTATE"] = "Companion";//반려
                            }
                            //else
                                //dr["APPROVALSTATE"] = "Companion";//반려
                        }
                    }

                    dtApproval.AcceptChanges();
                }
                else
                {
                    // 현재 결재중인 행의 결재 Sequence
                    int nowSeq = Convert.ToInt32(row["SEQUENCE"]);
                    
                    foreach (DataRow checkRow in dtApproval.Rows)
                    {
                        // 현재 결재중인 행의 결재보다 앞순번은 모두 승인처리
                        if (nowSeq > Convert.ToInt32(checkRow["SEQUENCE"]))
                        {
                            checkRow["APPROVALSTATE"] = "Approval";
                        } else if (nowSeq != Convert.ToInt32(checkRow["SEQUENCE"]))
                        {
                            checkRow["APPROVALSTATE"] = "";
                            checkRow["REJECTCOMMENTS"] = "";
                        }
                    }

                    dtApproval.AcceptChanges();
                }
                //else if (sAPPROVALSTATE == "Approval" && sPROCESSTYPE == "Draft")
                //{
                //    //기안자가 승인하면 
                //    //1)절차구분(검토, 승인, 수신) null 상태로 초기화 한다
                //    DataRow[] drs = dtApproval.Select("SEQUENCE > " + sSEQUENCE);
                //    if (drs != null && drs.Length > 0)
                //    {
                //        foreach (DataRow dr in drs)
                //        {
                //            if (dr["PROCESSTYPE"].ToString() != "Draft")
                //                dr["APPROVALSTATE"] = "";
                //        }
                //    }
                //    dtApproval.AcceptChanges();
                //}

            }
        }
        /// <summary>
        /// 그리드 해더 확장, 축소 버튼 에빈트
        /// </summary>
        private void GrdAuditManageregist_HeaderButtonClickEvent(object sender, Framework.SmartControls.HeaderButtonClickArgs args)
        {
            args.Button.Visible = true;
            if (args.ClickItem == GridButtonItem.Add)
            {
                //args.Button.Enabled = false;
                //GridButtonItem buttonItem = grdAuditManageregist.GridButtonItem;

                //DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton groupBoxButton = args.Button;

            }
            if (args.ClickItem == GridButtonItem.Expand) // 확장
            {
                grdApproval.Parent = this;
                grdApproval.BringToFront();
            }
            else if (args.ClickItem == GridButtonItem.Restore) // 축소
            {
                grdApproval.Parent = smartSplitTableLayoutPanel1;
                grdApproval.BringToFront();
            }
        }
        private void BtnADDAPPROVAL_Click(object sender, EventArgs e)
        {
            ReliabilityApprovalRegistPopup approvalRegistPopup = new ReliabilityApprovalRegistPopup(UserInfo.Current.Enterprise, UserInfo.Current.Plant);
            approvalRegistPopup.StartPosition = FormStartPosition.CenterParent;
            approvalRegistPopup.Owner = this.Parent as Form;
            approvalRegistPopup.QCApprovalDataTable = grdApproval.DataSource as DataTable;
            DialogManager.CloseWaitArea(flowLayoutPanel3);
            approvalRegistPopup.ShowDialog();
            if (approvalRegistPopup.DialogResult == DialogResult.OK)
            {
                grdApproval.DataSource = approvalRegistPopup.QCApprovalDataTable;

                DataTable dt = grdApproval.DataSource as DataTable;
                foreach (DataRow dr in dt.Rows)
                {
                    dr.SetAdded();
                }
            }


        }
        /// <summary>
        /// 컨트롤 로드시 작업
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Approval_Load(object sender, EventArgs e)
        {
        }
        #endregion

        #region Private Function
        public void Approval_Enable()
        {
            DataTable dtApproval = grdApproval.DataSource as DataTable;
            if (dtApproval != null && dtApproval.Rows.Count > 0)
            {
                //결재선에 로그인한 사람이 포함되어 있어야 결재자 버튼을 활성화 할 수 있다.
                if (dtApproval.AsEnumerable().Where(r => r["CHARGERID"].ToString() == UserInfo.Current.Id).ToList().Count > 0)
                    btnADDAPPROVAL.Enabled = true;
                else
                    btnADDAPPROVAL.Enabled = false;
            }
        }
        private DataTable GetApprovalStateAll()
        {
            Dictionary<string, object> values = new Dictionary<string, object>();
            return SqlExecuter.Query("GetApprovalAllStateByReliabilityVerificationRequest", "10001", values);
        }
        private DataTable GetApprovalState(string processType, string chargeType)
        {
            //-기안(Draft)
            //- 검토(Review)
            //- 승인(Approval)
            //- 수신(Receiving)

            //processType == "Draft", chargeType == "Request"   : 결제 요청자
            //processType == "Draft", chargeType == "Approval"  : 결제 승인/검토자
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
                case "Draft":
                    if(chargeType == "Request")
                        codeClassID = "DraftSettleState";
                    else
                        codeClassID = "ApprovalSettleState";
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

        private DataTable GetChargeType(string chargeType)
        {
            //-기안(Draft)
            //- 검토(Review)
            //- 승인(Approval)
            //- 수신(Receiving)

            //processType == "Draft", chargeType == "Request"   : 결제 요청자
            //processType == "Draft", chargeType == "Approval"  : 결제 승인/검토자
            string codeClassID = string.Empty;
            switch (chargeType)
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

        /// <summary>
        /// 결재부서 필수 체크 : 요청부서,승인부서,수신부서 등록되지 않았습니다.
        /// </summary>
        public void ValidateApproval()
        {
            DataTable dtApproval = grdApproval.DataSource as DataTable;
            var listDraft = dtApproval.AsEnumerable().Where(r => GetIsNull(r["PROCESSTYPE"]) == "Draft").ToList();
            if (dtApproval.Rows.Count > 0 && listDraft.Count == 0)//PROCESSTYPE(절차구분) = 기안(Draft), 검토(Review), 승인(Approval), 수신(Receiving)
            {
                throw MessageException.Create("ApprovalApprobalCheck1");//요청부서가 등록되지 않았습니다.
            }

            var listApproval = dtApproval.AsEnumerable().Where(r => GetIsNull(r["PROCESSTYPE"]) == "Approval").ToList();
            if (dtApproval.Rows.Count > 0 && listApproval.Count == 0)//PROCESSTYPE(절차구분) = 기안(Draft), 검토(Review), 승인(Approval), 수신(Receiving)
            {
                throw MessageException.Create("ApprovalApprobalCheck2");//승인부서가 등록되지 않았습니다.
            }

            /*
            var listReceiving = dtApproval.AsEnumerable().Where(r => GetIsNull(r["PROCESSTYPE"]) == "Receiving").ToList();
            if (dtApproval.Rows.Count > 0 && listReceiving.Count == 0)//PROCESSTYPE(절차구분)초기화 = 기안(Draft), 검토(Review), 승인(Approval), 수신(Receiving)
            {
                throw MessageException.Create("ApprovalApprobalCheck3");//수신부서가 등록되지 않았습니다.
            }
            */
        }
        /// <summary>
        /// <para>메일 제목.</para> 
        /// <para>메일 내용.</para> 
        /// <para>결재추가버튼 팝업에서 결재라인변경했을떄 결재완성부서가 생성되면 메일 보냄.</para> 
        /// <para>현재 팝업에서 결재라인별 승인상태 변경했을떄 결재완성부서가 생성되면 메일 보냄. </para> 
        /// </summary>
        /// <param name="sTitle"></param>
        /// <param name="sContents"></param>
        public void ApprovalMail(string sTitle, string sContents)
        {
            DataSet rullSet = new DataSet();
            DataTable dtApproval = grdApproval.DataSource as DataTable;

            //APPROVALSTATE : 승인(Approval), 회수(Reclamation), 반려(Companion)
            //PROCESSTYPE : 기안(Draft), 검토(Review), 승인(Approval), 수신(Receiving)
            var listDraft = dtApproval.AsEnumerable().Where(r => r["PROCESSTYPE"].ToString() == "Draft").ToList();
            //초기DB 조회시 결재완성 부서
            string sGroupProcessTypeDB = string.Empty;

            var lGroupApprovalDB = from r in _dtApproval.AsEnumerable()
                                   group r by new { PROCESSTYPE = r.Field<string>("PROCESSTYPE").ToString() } into g
                                   //where g.Count() > 1
                                   let r = new
                                   {
                                       PROCESSTYPE = g.Key.PROCESSTYPE,
                                       APPROVALSTATE = g.Min(r => r.Field<String>("APPROVALSTATE_ORG")),
                                       SEQUENCE = g.Max(r => r.Field<Int32>("SEQUENCE")),
                                   }
                                   orderby r.SEQUENCE ascending
                                   select r;
            var lGroupApprovalDBApproval = lGroupApprovalDB.Where(r => GetIsNull(r.APPROVALSTATE) == "Approval").OrderByDescending(o => o.SEQUENCE).ToList();
            if (lGroupApprovalDBApproval != null && lGroupApprovalDBApproval.Count() > 0)
                sGroupProcessTypeDB = lGroupApprovalDBApproval.FirstOrDefault().PROCESSTYPE;
            //결재추가버튼으로 정보 변경후 결재완성된 부서
            string sGroupProcessTypeData = string.Empty;
            /* dtApproval
            SEQUENCE PROCESSTYPE     APPROVALSTATE
            ----------------------------------------------
            1           Draft           Approval
            2           Draft           Approval
            3           Review          Approval
            4           Review          Approval
            5           Review          Approval
            6           Review          Approval
            7           Review          Approval
            8           Review          Approval
            9           Review          Approval
            10          Approval    
            11          Approval    
            12          Approval    
            13          Approval    
            */
            var lGroupApproval = from r in dtApproval.AsEnumerable()
                                     //group r by new { PROCESSTYPE = r.Field<string>("PROCESSTYPE").ToString(), APPROVALSTATE = r.Field<string>("APPROVALSTATE") } into g
                                 group r by new { PROCESSTYPE = r.Field<string>("PROCESSTYPE").ToString() } into g
                                 let r = new
                                 {
                                     PROCESSTYPE = g.Key.PROCESSTYPE,
                                     APPROVALSTATE = g.Min(r => r.Field<String>("APPROVALSTATE")),
                                     SEQUENCE = g.Max(r => r.Field<Int32>("SEQUENCE")),
                                 }
                                 orderby r.SEQUENCE ascending
                                 select r;
            /* lGroupApproval
            SEQUENCE PROCESSTYPE  APPROVALSTATE
            ----------------------------------------------
            2           Draft         Approval
            9           Review        Approval
            13          Approval    
            */

            var lGroupApprovalApproval = lGroupApproval.Where(r => r.APPROVALSTATE == "Approval").OrderByDescending(o => o.SEQUENCE).ToList();
            /* lGroupApprovalApproval
            SEQUENCE PROCESSTYPE PROCESSTYPE APPROVALSTATE
            ----------------------------------------------
            9           Review      Review      Approval
            2           Draft       Draft       Approval
            */
            if (lGroupApprovalApproval != null && lGroupApprovalApproval.Count() > 0)
                sGroupProcessTypeData = lGroupApprovalApproval.FirstOrDefault().PROCESSTYPE;
            // sGroupProcessTypeData = > Review
            if (sGroupProcessTypeDB != sGroupProcessTypeData)
            {
                //메일 보내기
                if (lGroupApprovalApproval.Count() > 0)
                {
                    var lSEQUENCE = lGroupApproval.Where(r => GetIsNull(r.APPROVALSTATE) == string.Empty 
                    || GetIsNull(r.APPROVALSTATE) == "Reclamation"
                    || GetIsNull(r.APPROVALSTATE) == "Companion").ToList();
                    if (lSEQUENCE.Count > 0)
                    {
                        DataRow[] dra = dtApproval.Select(string.Format("PROCESSTYPE = '{0}'", lSEQUENCE[0].PROCESSTYPE));
                        if (dra != null && dra.Length > 0)
                        {
                            string sNextGroup = dra[0]["PROCESSTYPE"].ToString();
                            var lMail = dtApproval.AsEnumerable().Where(r => GetIsNull(r["PROCESSTYPE"]) == sNextGroup).ToList();
                            DataTable sendTable = new DataTable();
                            sendTable.TableName = "list";
                            DataRow row = null;

                            sendTable.Columns.Add(new DataColumn("USERID", typeof(string)));
                            sendTable.Columns.Add(new DataColumn("EMAILADDRESS", typeof(string)));
                            sendTable.Columns.Add(new DataColumn("TITLE", typeof(string)));
                            sendTable.Columns.Add(new DataColumn("CONTENT", typeof(string)));

                            foreach (DataRow r in lMail)
                            {
                                if (IsValidEmail(r["EMAILADDRESS"].ToString()))
                                {
                                    
                                    row = sendTable.NewRow();
                                    row["USERID"] = listDraft.FirstOrDefault()["CHARGERID"].ToString();
                                    string sEMAILADDRESS = r["EMAILADDRESS"].ToString();
                                    //sEMAILADDRESS = "hs.yoo@interflex.co.kr";
                                    row["EMAILADDRESS"] = sEMAILADDRESS;
                                    row["TITLE"] = sTitle;//"신뢰성(정기외) 결재";
                                    row["CONTENT"] = sContents;

                                    sendTable.Rows.Add(row);
                                }
                            }
                            
                            //rullSet.Tables.Add(sendTable);
                            //ExecuteRule("SendMailChemicalAnalysis", rullSet);

                            MessageWorker worker = new MessageWorker("SendMailChemicalAnalysis");
                            worker.SetBody(new MessageBody()
                            {
                                { "list", sendTable }, 
                            });

                            worker.Execute();
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 반려할 경우 기안자에게 반려 메일 보냄-2020.01.16
        /// <para>메일 제목.</para> 
        /// <para>메일 내용.</para> 
        /// <para>결재추가버튼 팝업에서 결재라인변경했을떄 결재완성부서가 생성되면 메일 보냄.</para> 
        /// <para>현재 팝업에서 결재라인별 승인상태 변경했을떄 결재완성부서가 생성되면 메일 보냄. </para> 
        /// </summary>
        /// <param name="sTitle"></param>
        /// <param name="sContents"></param>
        public void ApprovalCompanionMail(string sTitle, string sContents)
        {
            DataTable dtApproval = grdApproval.DataSource as DataTable;
            var listDraft = dtApproval.AsEnumerable().Where(r => r["PROCESSTYPE"].ToString() == "Draft").ToList();

            DataRow approvalRow = grdApproval.View.GetFocusedDataRow();
            string sAPPROVALSTATE = approvalRow["APPROVALSTATE"].ToString();//승인(Approval), 회수(Reclamation), 반려(Companion)
            string sPROCESSTYPE = approvalRow["PROCESSTYPE"].ToString();//기안(Draft), 검토(Review), 승인(Approval), 수신(Receiving)

            //검토자, 승인자가 반려하면 
            if (sAPPROVALSTATE == "Companion" && sPROCESSTYPE != "Draft")
            {
                //메일 보내기
                DataTable sendTable = new DataTable();
                sendTable.TableName = "list";
                DataRow row = null;

                sendTable.Columns.Add(new DataColumn("USERID", typeof(string)));
                sendTable.Columns.Add(new DataColumn("EMAILADDRESS", typeof(string)));
                sendTable.Columns.Add(new DataColumn("TITLE", typeof(string)));
                sendTable.Columns.Add(new DataColumn("CONTENT", typeof(string)));

                if (IsValidEmail(listDraft.FirstOrDefault()["EMAILADDRESS"].ToString()))
                {
                    row = sendTable.NewRow();
                    row["USERID"] = approvalRow["CHARGERID"].ToString();
                    string sEMAILADDRESS = listDraft.FirstOrDefault()["EMAILADDRESS"].ToString();
                    row["EMAILADDRESS"] = sEMAILADDRESS;
                    row["TITLE"] = sTitle;//"신뢰성(정기외) 결재";
                    row["CONTENT"] = sContents;

                    sendTable.Rows.Add(row);
                }

                MessageWorker worker = new MessageWorker("SendMailChemicalAnalysis");
                worker.SetBody(new MessageBody()
                {
                    { "list", sendTable },
                });

                worker.Execute();
            }
        }
        private string GetIsNull(object o)
        {
            string ret = string.Empty;
            if (o != null)
                ret = o.ToString();
            return ret;
        }
        /// <summary>
        /// <para>결재추가버튼 팝업에서 결재라인변경했을떄 결재완성부서가 생성되면 메일 보냄.</para> 
        /// <para>현재 팝업에서 결재라인별 승인상태 변경했을떄 결재완성부서가 생성되면 메일 보냄. </para> 
        /// </summary>
        /// <param name="sContents"></param>
        public void ApprovalMail(string sContents)
        {
            DataTable dtApproval = grdApproval.DataSource as DataTable;
            
            //APPROVALSTATE : 승인(Approval), 회수(Reclamation), 반려(Companion)
            //PROCESSTYPE : 기안(Draft), 검토(Review), 승인(Approval), 수신(Receiving)

            //초기DB 조회시 결재완성 부서
            string sGroupProcessTypeDB = string.Empty;

            var lGroupApprovalDB = from r in _dtApproval.AsEnumerable()
                                   group r by new { PROCESSTYPE = r.Field<string>("PROCESSTYPE").ToString() } into g
                                   //where g.Count() > 1
                                   let r = new
                                   {
                                       PROCESSTYPE = g.Key.PROCESSTYPE,
                                       APPROVALSTATE = g.Min(r => r.Field<String>("APPROVALSTATE")),
                                       SEQUENCE = g.Max(r => r.Field<Int32>("SEQUENCE")),
                                   }
                                   orderby r.SEQUENCE ascending
                                   select r;
            var lGroupApprovalDBApproval = lGroupApprovalDB.Where(r => r.APPROVALSTATE == "Approval").OrderByDescending(o => o.SEQUENCE).ToList();
            if (lGroupApprovalDBApproval != null && lGroupApprovalDBApproval.Count() > 0)
                sGroupProcessTypeDB = lGroupApprovalDBApproval.FirstOrDefault().PROCESSTYPE;
            //결재추가버튼으로 정보 변경후 결재완성된 부서
            string sGroupProcessTypeData = string.Empty;
            /* dtApproval
            SEQUENCE PROCESSTYPE     APPROVALSTATE
            ----------------------------------------------
            1           Draft           Approval
            2           Draft           Approval
            3           Review          Approval
            4           Review          Approval
            5           Review          Approval
            6           Review          Approval
            7           Review          Approval
            8           Review          Approval
            9           Review          Approval
            10          Approval    
            11          Approval    
            12          Approval    
            13          Approval    
            */
            var lGroupApproval = from r in dtApproval.AsEnumerable()
                                     //group r by new { PROCESSTYPE = r.Field<string>("PROCESSTYPE").ToString(), APPROVALSTATE = r.Field<string>("APPROVALSTATE") } into g
                                 group r by new { PROCESSTYPE = r.Field<string>("PROCESSTYPE").ToString() } into g
                                 let r = new
                                 {
                                     PROCESSTYPE = g.Key.PROCESSTYPE,
                                     APPROVALSTATE = g.Min(r => r.Field<String>("APPROVALSTATE")),
                                     SEQUENCE = g.Max(r => r.Field<Int32>("SEQUENCE")),
                                 }
                                 orderby r.SEQUENCE ascending
                                 select r;
            /* lGroupApproval
            SEQUENCE PROCESSTYPE  APPROVALSTATE
            ----------------------------------------------
            2           Draft         Approval
            9           Review        Approval
            13          Approval    
            */

            var lGroupApprovalApproval = lGroupApproval.Where(r => r.APPROVALSTATE == "Approval").OrderByDescending(o => o.SEQUENCE).ToList();
            /* lGroupApprovalApproval
            SEQUENCE PROCESSTYPE PROCESSTYPE APPROVALSTATE
            ----------------------------------------------
            9           Review      Review      Approval
            2           Draft       Draft       Approval
            */
            if (lGroupApprovalApproval != null && lGroupApprovalApproval.Count() > 0)
                sGroupProcessTypeData = lGroupApprovalApproval.FirstOrDefault().PROCESSTYPE;
            // sGroupProcessTypeData = > Review
            if (sGroupProcessTypeDB != sGroupProcessTypeData)
            {
                //메일 보내기
                if (lGroupApprovalApproval.Count() > 0)
                {
                    var lSEQUENCE = lGroupApproval.Where(r => r.APPROVALSTATE.ToString() == string.Empty).ToList();
                    if (lSEQUENCE.Count > 0)
                    {
                        DataRow[] dra = dtApproval.Select(string.Format("PROCESSTYPE = '{0}'", lSEQUENCE[0].PROCESSTYPE));
                        if (dra != null && dra.Length > 0)
                        {
                            string sNextGroup = dra[0]["PROCESSTYPE"].ToString();
                            var lMail = dtApproval.AsEnumerable().Where(r => r["PROCESSTYPE"].ToString() == sNextGroup).ToList();
                            DataTable sendTable = new DataTable();
                            sendTable.TableName = "list";
                            DataRow row = null;

                            sendTable.Columns.Add(new DataColumn("USERID", typeof(string)));
                            sendTable.Columns.Add(new DataColumn("EMAILADDRESS", typeof(string)));
                            sendTable.Columns.Add(new DataColumn("TITLE", typeof(string)));
                            sendTable.Columns.Add(new DataColumn("CONTENT", typeof(string)));

                            foreach (DataRow r in lMail)
                            {
                                if (IsValidEmail(r["EMAILADDRESS"].ToString()))
                                {
                                    row = sendTable.NewRow();
                                    row["USERID"] = r["CHARGERID"].ToString();
                                    row["EMAILADDRESS"] = r["EMAILADDRESS"].ToString();
                                    row["TITLE"] = "신뢰성(정기외) 결재";
                                    row["CONTENT"] = sContents;

                                    sendTable.Rows.Add(row);
                                }
                            }
                            DataSet rullSet = new DataSet();
                            rullSet.Tables.Add(sendTable);
                            //ExecuteRule("SendMailChemicalAnalysis", rullSet);
                        }
                    }
                }
            }
        }

        private bool IsValidEmail(string email)
        {
            bool valid = false;
            valid = Regex.IsMatch(email, @"[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?");
            return valid;
        }

        #endregion
    }

}
