#region using
using Micube.Framework.Net;
using Micube.Framework;
using Micube.Framework.SmartControls;
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

using DevExpress.XtraCharts;
using DevExpress.Utils;
using Micube.Framework.SmartControls.Validations;

using System.Globalization;

#endregion

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프로그램명 : 기준정보 > Setup > 품목규칙 등록
    /// 업 무 설명 : 품목 코드,명,계산식 등록
    /// 생  성  자 : 윤성원
    /// 생  성  일 : 2019-06-27
    /// 수정 이 력 : 
    /// </summary> 
	public partial class Approval : SmartConditionManualBaseForm
	{
        #region Local Variables
        #endregion

        #region 생성자
        public Approval()
		{
			InitializeComponent();
            InitializeEvent();
           
        }

        #endregion

        #region 컨텐츠 영역 초기화


        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGridIdDefinitionManagement()
        {

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // 승인 그리드 초기화
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            grdApproval.GridButtonItem = GridButtonItem.None;   // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기
            grdApproval.View.SetIsReadOnly();
            grdApproval.View.AddComboBoxColumn("APPROVALTYPE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ApprovalType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdApproval.View.AddTextBoxColumn("APPROVALID",80);
            grdApproval.View.AddTextBoxColumn("ENTERPRISEID").SetIsHidden();
            grdApproval.View.AddTextBoxColumn("PLANTID").SetIsHidden();
            grdApproval.View.AddComboBoxColumn("APPROVALSTATUS", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ApprovalStatus", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            grdApproval.View.AddTextBoxColumn("REQUESTOR").SetIsReadOnly();
            grdApproval.View.AddTextBoxColumn("REQUESTORNAME").SetIsReadOnly();


            grdApproval.View.AddTextBoxColumn("REQUESTDATE", 80)
            .SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime)
            .SetIsReadOnly()
            .SetTextAlignment(TextAlignment.Center);
            grdApproval.View.AddTextBoxColumn("REVIEWER", 80).SetIsReadOnly();
            grdApproval.View.AddTextBoxColumn("REVIEWDATE", 80)
            .SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime)
            .SetIsReadOnly()
            .SetTextAlignment(TextAlignment.Center);

            grdApproval.View.AddTextBoxColumn("APPROVER").SetIsReadOnly();
            grdApproval.View.AddTextBoxColumn("APPROVERNAME").SetIsReadOnly();

            grdApproval.View.AddTextBoxColumn("APPROVEDATE",80)
            .SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime)
            .SetIsReadOnly()
            .SetTextAlignment(TextAlignment.Center);
            grdApproval.View.AddTextBoxColumn("DESCRIPTION",150);
            grdApproval.View.PopulateColumns();

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // 승인 이력 그리드 초기화
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            grdApprovaltransaction.GridButtonItem = GridButtonItem.None;   // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기
            grdApprovaltransaction.View.SetIsReadOnly();
            grdApprovaltransaction.View.AddTextBoxColumn("APPROVALTYPE", 80).SetIsHidden();
            grdApprovaltransaction.View.AddTextBoxColumn("APPROVALID", 100).SetIsHidden();
            grdApprovaltransaction.View.AddTextBoxColumn("ENTERPRISEID", 80).SetIsHidden();
            grdApprovaltransaction.View.AddSpinEditColumn("SEQUENCE", 80).SetIsHidden();
            grdApprovaltransaction.View.AddTextBoxColumn("PLANTID", 80);
            grdApprovaltransaction.View.AddComboBoxColumn("RESULTS", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ApprovalStatus", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdApprovaltransaction.View.AddTextBoxColumn("RESULTTYPE", 100).SetIsHidden();
            grdApprovaltransaction.View.AddTextBoxColumn("ACTOR", 80).SetIsReadOnly();
            grdApprovaltransaction.View.AddTextBoxColumn("ACTORNAME", 80).SetIsReadOnly();

            grdApprovaltransaction.View.AddTextBoxColumn("STARTDATE", 100)
            .SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime)
            .SetIsReadOnly()
            .SetTextAlignment(TextAlignment.Center);
            grdApprovaltransaction.View.AddTextBoxColumn("ENDDATE", 100)
            .SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime)
            .SetIsReadOnly()
            .SetTextAlignment(TextAlignment.Center);
            grdApprovaltransaction.View.AddTextBoxColumn("DESCRIPTION", 150);
            grdApprovaltransaction.View.PopulateColumns();


        }
       
        protected override void InitializeContent()
        {
            base.InitializeContent();
            InitializeGridIdDefinitionManagement();
        }

        #endregion

        #region 이벤트

        private void InitializeEvent()
        {
            btnCancelRequest.Click += BtnCancelRequest_Click;
            btnReject.Click += BtnReject_Click;
            btnApproved.Click += BtnApproved_Click;
            btnconfirm.Click += Btnconfirm_Click;
            grdApproval.View.FocusedRowChanged += grdApproval_FocusedRowChanged;
            btnDetail.Click += BtnDetail_Click;

            // 파일업로드
            btnFileUpload.Click += BtnFileUpload_Click;
        }

        private void BtnFileUpload_Click(object sender, EventArgs e)
        {
            DataRow row = this.grdApproval.View.GetFocusedDataRow();
            ItemMasterfilePopup pis = new ItemMasterfilePopup(row["APPROVALID"].ToString(), "", "Approval", "ApprovalMgnt / Approval");
            pis.ShowDialog();
           
        }


        private void BtnDetail_Click(object sender, EventArgs e)
        {
            DataRow row = grdApproval.View.GetFocusedDataRow();

            switch (row["APPROVALTYPE"].ToString())
            {
                case "RunningChange":
                    Dictionary<string, object> ParamRc = new Dictionary<string, object>();
                    ParamRc.Add("GOVERNANCENO", row["APPROVALID"].ToString());
                    this.OpenMenu("PG-SD-0260", ParamRc);

                    break;
                case "NewRequest":
                    Dictionary<string, object> ParamNr = new Dictionary<string, object>();
                    ParamNr.Add("GOVERNANCENO", row["APPROVALID"].ToString());
                    this.OpenMenu("PG-SD-0230", ParamNr);
                    break;
            }

            
        }

        private void Btnconfirm_Click(object sender, EventArgs e)
        {
            DataRow row = grdApproval.View.GetFocusedDataRow();

            if (row["APPROVALSTATUS"].ToString() != "Approved")
            {
                ShowMessage("Approved");
                return;
            }


            DataTable Approval = new DataTable();
            Approval.Columns.Add("APPROVALTYPE");
            Approval.Columns.Add("APPROVALID");
            Approval.Columns.Add("ENTERPRISEID");
            Approval.Columns.Add("PLANTID");
            Approval.Columns.Add("APPROVALSTATUS");
            Approval.Columns.Add("APPROVEDATE");
            Approval.Columns.Add("_STATE_");


            DataTable Approvaltransaction = new DataTable();
            Approvaltransaction.Columns.Add("APPROVALTYPE");
            Approvaltransaction.Columns.Add("APPROVALID");
            Approvaltransaction.Columns.Add("ENTERPRISEID");
            Approvaltransaction.Columns.Add("PLANTID");
            Approvaltransaction.Columns.Add("SEQUENCE");
            Approvaltransaction.Columns.Add("RESULTS");
            Approvaltransaction.Columns.Add("RESULTTYPE");
            Approvaltransaction.Columns.Add("ACTOR");
            Approvaltransaction.Columns.Add("STARTDATE");
            Approvaltransaction.Columns.Add("ENDDATE");
            Approvaltransaction.Columns.Add("DESCRIPTION");
            Approvaltransaction.Columns.Add("VALIDSTATE");
            Approvaltransaction.Columns.Add("_STATE_");



            Dictionary<string, object> Param = new Dictionary<string, object>();
            Param.Add("ENTERPRISEID", row["ENTERPRISEID"].ToString());
            Param.Add("PLANTID", row["PLANTID"].ToString());
            Param.Add("APPROVALTYPE", row["APPROVALTYPE"].ToString());
            Param.Add("APPROVALID", row["APPROVALID"].ToString());

            DataTable dt = SqlExecuter.Query("GetApprovalTransactionList", "10001", Param);


            DataRow rowAt = Approvaltransaction.NewRow();
            rowAt["APPROVALTYPE"] = row["APPROVALTYPE"];
            rowAt["APPROVALID"] = row["APPROVALID"];
            rowAt["ENTERPRISEID"] = row["ENTERPRISEID"];
            rowAt["PLANTID"] = row["PLANTID"];

            if (dt != null)
            {
                if (dt.Rows.Count != 0)
                {
                    rowAt["SEQUENCE"] = decimal.Parse(dt.Compute("MAX(SEQUENCE)", "").ToString()) + 1;
                }
                else
                {
                    rowAt["SEQUENCE"] = 1;
                }
            }
            else
            {
                rowAt["SEQUENCE"] = 1;
            }


            rowAt["RESULTS"] = "Reviewer";
            rowAt["RESULTTYPE"] = "Approved"; 
             rowAt["ACTOR"] = UserInfo.Current.Id;

            if (dt != null)
            {
                if (dt.Rows.Count != 0)
                {
                    rowAt["STARTDATE"] = DateTime.Parse(dt.Compute("MAX(ENDDATE)", "").ToString()).ToString("yyyy-MM-dd hh:mm:ss");
                }
            }

            rowAt["ENDDATE"] = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            rowAt["_STATE_"] = "added";
            rowAt["DESCRIPTION"] = row["DESCRIPTION"];
            Approvaltransaction.Rows.Add(rowAt);


            Dictionary<string, object> ParamReviewer = new Dictionary<string, object>();
            ParamReviewer.Add("ENTERPRISEID", row["ENTERPRISEID"].ToString());
            ParamReviewer.Add("PLANTID", row["PLANTID"].ToString());
            ParamReviewer.Add("APPROVALTYPE", row["APPROVALTYPE"].ToString());
            ParamReviewer.Add("APPROVALID", row["APPROVALID"].ToString());
            ParamReviewer.Add("REVIEWER_GR", row["REVIEWER_GR"].ToString());
            DataTable dtReviewerChk = SqlExecuter.Query("GetReviewerChk", "10001", ParamReviewer);

            if(dtReviewerChk.Select("APPROVALYN = 'N' and UC.USERID <> '"+ UserInfo.Current.Id + "' ").Length == 0)
            {
                DataRow rowApproval = Approval.NewRow();

                rowApproval["APPROVALTYPE"] = row["APPROVALTYPE"];
                rowApproval["APPROVALID"] = row["APPROVALID"];
                rowApproval["ENTERPRISEID"] = row["ENTERPRISEID"];
                rowApproval["PLANTID"] = row["PLANTID"];

                if (row["APPROVER"].ToString() == UserInfo.Current.Id)
                {
                    rowApproval["APPROVALSTATUS"] = "Complete";
                    rowApproval["REVIEWDATE"] = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                    rowApproval["_STATE_"] = "modified";
                }
                else
                {
                    ShowMessage("Powers"); // 조회할 데이터가 없습니다.
                    return;
                }

                Approval.Rows.Add(rowApproval);
            }

            Approvaltransaction.TableName = "approvaltransaction";
            Approval.TableName = "approva";

            DataSet ds = new DataSet();
            ds.Tables.Add(Approval);
            ds.Tables.Add(Approvaltransaction);
            ExecuteRule("Approvaltransaction", ds);
            ShowMessage("SuccedSave");
        }

        private void grdApproval_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grdApproval.View.FocusedRowHandle < 0)
                return;

            DataRow row = grdApproval.View.GetFocusedDataRow();

            Dictionary<string, object> Param = new Dictionary<string, object>();
            Param.Add("ENTERPRISEID", row["ENTERPRISEID"].ToString());
            //Param.Add("PLANTID", row["PLANTID"].ToString());
            //Param.Add("APPROVALTYPE", row["APPROVALTYPE"].ToString());
            Param.Add("APPROVALID", row["APPROVALID"].ToString());

            DataTable dt = SqlExecuter.Query("GetGovernanceStatusTransactionList", "10001", Param);
            grdApprovaltransaction.DataSource = dt;
        }

        private void BtnApproved_Click(object sender, EventArgs e)
        {
            DataRow row = grdApproval.View.GetFocusedDataRow();

            if(row["APPROVALSTATUS"].ToString() !=  "RequestApproval")
            {
                ShowMessage("RequestApproval");
                return;
            }


           DataTable Approval = new DataTable();
           Approval.Columns.Add("APPROVALTYPE");
           Approval.Columns.Add("APPROVALID");
           Approval.Columns.Add("ENTERPRISEID");
           Approval.Columns.Add("PLANTID");
           Approval.Columns.Add("APPROVALSTATUS");
           Approval.Columns.Add("REQUESTOR");
           Approval.Columns.Add("REQUESTDATE");
           Approval.Columns.Add("APPROVEDATE");
           Approval.Columns.Add("APPROVER");
           Approval.Columns.Add("_STATE_");

            DataTable Approvaltransaction = new DataTable();
            Approvaltransaction.Columns.Add("APPROVALTYPE");
            Approvaltransaction.Columns.Add("APPROVALID");
            Approvaltransaction.Columns.Add("ENTERPRISEID");
            Approvaltransaction.Columns.Add("PLANTID");
            Approvaltransaction.Columns.Add("SEQUENCE");
            Approvaltransaction.Columns.Add("RESULTS");
            Approvaltransaction.Columns.Add("RESULTTYPE");
            Approvaltransaction.Columns.Add("ACTOR");
            Approvaltransaction.Columns.Add("STARTDATE");
            Approvaltransaction.Columns.Add("ENDDATE");
            Approvaltransaction.Columns.Add("DESCRIPTION");
            Approvaltransaction.Columns.Add("VALIDSTATE");
            Approvaltransaction.Columns.Add("_STATE_");


            DataTable Governancesite = new DataTable();
            Governancesite.Columns.Add("GOVERNANCENO");
            Governancesite.Columns.Add("GOVERNANCETYPE");
            Governancesite.Columns.Add("ENTERPRISEID");
            Governancesite.Columns.Add("PLANTID");
            Governancesite.Columns.Add("STATE");
            Governancesite.Columns.Add("APPROVEDDATE");
            Governancesite.Columns.Add("SEQUENCE");
            Governancesite.Columns.Add("_STATE_");



            Dictionary<string, object> Param = new Dictionary<string, object>();
            Param.Add("ENTERPRISEID", row["ENTERPRISEID"].ToString());
            Param.Add("PLANTID", row["PLANTID"].ToString());
            Param.Add("APPROVALTYPE", row["APPROVALTYPE"].ToString());
            Param.Add("APPROVALID", row["APPROVALID"].ToString());

            DataTable dt = SqlExecuter.Query("GetApprovalTransactionList", "10001", Param);


            DataRow rowAt = Approvaltransaction.NewRow();
            rowAt["APPROVALTYPE"] = row["APPROVALTYPE"];
            rowAt["APPROVALID"] = row["APPROVALID"];
            rowAt["ENTERPRISEID"] = row["ENTERPRISEID"];
            rowAt["PLANTID"] = row["PLANTID"];

            if (dt != null)
            {
                if (dt.Rows.Count != 0)
                {
                    rowAt["SEQUENCE"] = decimal.Parse(dt.Compute("MAX(SEQUENCE)","").ToString()) + 1;
                }
                else
                {
                    rowAt["SEQUENCE"] = 1;
                }
            }
            else
            {
                rowAt["SEQUENCE"] = 1;
            }

            
            rowAt["RESULTS"] = "Approved"; 
            rowAt["RESULTTYPE"] = "Approver"; 
            rowAt["ACTOR"] = UserInfo.Current.Id;

            if (dt != null)
            {
                if (dt.Rows.Count != 0)
                {
                    rowAt["STARTDATE"] = DateTime.Parse(dt.Compute("MAX(ENDDATE)", "").ToString()).ToString("yyyy-MM-dd hh:mm:ss");
                }
            }
         
            rowAt["ENDDATE"] = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            rowAt["_STATE_"] = "added";
            rowAt["DESCRIPTION"] = row["DESCRIPTION"];

            Approvaltransaction.Rows.Add(rowAt);


            string sAPPROVEDATE = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");

            DataRow rowApproval = Approval.NewRow();
            rowApproval["APPROVALTYPE"] = row["APPROVALTYPE"];
            rowApproval["APPROVALID"] = row["APPROVALID"];
            rowApproval["ENTERPRISEID"] = row["ENTERPRISEID"];
            rowApproval["PLANTID"] = row["PLANTID"];


            if (row["APPROVER"].ToString() == UserInfo.Current.Id)
            {
                rowApproval["APPROVALSTATUS"] = "Approved";
                rowApproval["APPROVEDATE"] = sAPPROVEDATE;
                rowApproval["_STATE_"] = "modified";
            }
            else
            {
                ShowMessage("Powers"); // 조회할 데이터가 없습니다.
                return;
            }

            Approval.Rows.Add(rowApproval);

            switch (row["APPROVALTYPE"].ToString())
            {
                case "RunningChange":
                    Dictionary<string, object> ParamGv = new Dictionary<string, object>();
                    ParamGv.Add("ENTERPRISEID", row["ENTERPRISEID"].ToString());
                    ParamGv.Add("GOVERNANCENO", row["APPROVALID"].ToString());
                    DataTable dtGv = SqlExecuter.Query("GetGovernacesitePlant", "10001", ParamGv);


                    string sSEQUENCE = dtGv.Select("GOVERNANCENO = '" + row["APPROVALID"].ToString() + "' AND PLANTID = '" + row["PLANTID"].ToString() + "'")[0]["SEQUENCE"].ToString();

                    DataRow[] rowInsert = dtGv.Select("SEQUENCE > " + sSEQUENCE + "", "SEQUENCE DESC");

                    if (rowInsert.Length != 0)
                    {
                        DataRow rowApprovalInsert = Approval.NewRow();
                        rowApprovalInsert["APPROVALTYPE"] = row["APPROVALTYPE"];
                        rowApprovalInsert["APPROVALID"] = row["APPROVALID"];
                        rowApprovalInsert["ENTERPRISEID"] = row["ENTERPRISEID"];
                        rowApprovalInsert["PLANTID"] = rowInsert[0]["PLANTID"];
                        rowApprovalInsert["APPROVALSTATUS"] = "Working";
                        rowApprovalInsert["_STATE_"] = "added";
                        Approval.Rows.Add(rowApprovalInsert);

                        DataRow rowGInsert = Governancesite.NewRow();
                        rowGInsert["GOVERNANCETYPE"] = row["APPROVALTYPE"];
                        rowGInsert["GOVERNANCENO"] = row["APPROVALID"];
                        rowGInsert["ENTERPRISEID"] = row["ENTERPRISEID"];
                        rowGInsert["PLANTID"] = rowInsert[0]["PLANTID"];
                        rowGInsert["STATE"] = "Working";
                        rowGInsert["SEQUENCE"] = rowInsert[0]["SEQUENCE"];
                        rowGInsert["_STATE_"] = "modified";
                        Governancesite.Rows.Add(rowGInsert);
                    }




                    DataRow rowGr = Governancesite.NewRow();
                    rowGr["GOVERNANCETYPE"] = row["APPROVALTYPE"];
                    rowGr["GOVERNANCENO"] = row["APPROVALID"];
                    rowGr["ENTERPRISEID"] = row["ENTERPRISEID"];
                    rowGr["PLANTID"] = row["PLANTID"];
                    rowGr["STATE"] = "Approved";
                    rowGr["SEQUENCE"] = dtGv.Select("GOVERNANCENO = '" + row["APPROVALID"].ToString() + "' AND PLANTID = '" + row["PLANTID"].ToString() + "'")[0]["SEQUENCE"].ToString();
                    rowGr["_STATE_"] = "modified";
                    Governancesite.Rows.Add(rowGr);
                    break;
                case "NewRequest":
                    Dictionary<string, object> ParamGn = new Dictionary<string, object>();
                    ParamGn.Add("ENTERPRISEID", row["ENTERPRISEID"].ToString());
                    ParamGn.Add("GOVERNANCENO", row["APPROVALID"].ToString());
                    DataTable dtGn = SqlExecuter.Query("GetGovernacesitePlant", "10001", ParamGn);

                    string sSEQUENCEn = dtGn.Select("GOVERNANCENO = '" + row["APPROVALID"].ToString() + "' AND PLANTID = '" + row["PLANTID"].ToString() + "'")[0]["SEQUENCE"].ToString();

                    DataRow[] rowInsertN = dtGn.Select("SEQUENCE > " + sSEQUENCEn + "", "SEQUENCE DESC");

                    if (rowInsertN.Length != 0)
                    {
                        DataRow rowApprovalInsert = Approval.NewRow();
                        rowApprovalInsert["APPROVALTYPE"] = row["APPROVALTYPE"];
                        rowApprovalInsert["APPROVALID"] = row["APPROVALID"];
                        rowApprovalInsert["ENTERPRISEID"] = row["ENTERPRISEID"];
                        rowApprovalInsert["PLANTID"] = rowInsertN[0]["PLANTID"];
                        rowApprovalInsert["APPROVALSTATUS"] = "Working";
                        rowApprovalInsert["_STATE_"] = "added";
                        Approval.Rows.Add(rowApprovalInsert);

                        DataRow rowGInsert = Governancesite.NewRow();
                        rowGInsert["GOVERNANCETYPE"] = row["APPROVALTYPE"];
                        rowGInsert["GOVERNANCENO"] = row["APPROVALID"];
                        rowGInsert["ENTERPRISEID"] = row["ENTERPRISEID"];
                        rowGInsert["PLANTID"] = rowInsertN[0]["PLANTID"];
                        rowGInsert["STATE"] = "Working";
                        rowGInsert["SEQUENCE"] = rowInsertN[0]["SEQUENCE"];
                        rowGInsert["_STATE_"] = "modified";
                        Governancesite.Rows.Add(rowGInsert);
                    }



                    DataRow rowGn = Governancesite.NewRow();
                    rowGn["GOVERNANCETYPE"] = row["APPROVALTYPE"];
                    rowGn["GOVERNANCENO"] = row["APPROVALID"];
                    rowGn["ENTERPRISEID"] = row["ENTERPRISEID"];
                    rowGn["PLANTID"] = row["PLANTID"];
                    rowGn["STATE"] = "Approved";
                    rowGn["SEQUENCE"] = dtGn.Select("GOVERNANCENO = '" + row["APPROVALID"].ToString() + "' AND PLANTID = '" + row["PLANTID"].ToString() + "'")[0]["SEQUENCE"].ToString();
                    rowGn["_STATE_"] = "modified";
                    Governancesite.Rows.Add(rowGn);
                    break;
            }

            Governancesite.TableName = "governanceSite";
            Approvaltransaction.TableName = "approvaltransaction";
            Approval.TableName = "approva";
          
            DataSet ds = new DataSet();

            ds.Tables.Add(Governancesite);
            ds.Tables.Add(Approval);
            ds.Tables.Add(Approvaltransaction);

            ExecuteRule("Approvaltransaction", ds);
            ShowMessage("SuccedSave");
            SaveSearch();
            //changed.Rows.Add(rowchanged);

        }

        private void BtnReject_Click(object sender, EventArgs e)
        {

            DataRow row = grdApproval.View.GetFocusedDataRow();

            if (row["APPROVALSTATUS"].ToString() != "RequestApproval")
            {
                ShowMessage("RequestApproval");
                return;
                
            }

            DataTable Approval = new DataTable();
            Approval.Columns.Add("APPROVALTYPE");
            Approval.Columns.Add("APPROVALID");
            Approval.Columns.Add("ENTERPRISEID");
            Approval.Columns.Add("PLANTID");
            Approval.Columns.Add("APPROVALSTATUS");
            Approval.Columns.Add("APPROVEDATE", typeof(DateTime));
            Approval.Columns.Add("_STATE_");


            DataTable Approvaltransaction = new DataTable();
            Approvaltransaction.Columns.Add("APPROVALTYPE");
            Approvaltransaction.Columns.Add("APPROVALID");
            Approvaltransaction.Columns.Add("ENTERPRISEID");
            Approvaltransaction.Columns.Add("PLANTID");
            Approvaltransaction.Columns.Add("SEQUENCE");
            Approvaltransaction.Columns.Add("RESULTS");
            Approvaltransaction.Columns.Add("RESULTTYPE");
            Approvaltransaction.Columns.Add("ACTOR");
            Approvaltransaction.Columns.Add("STARTDATE");
            Approvaltransaction.Columns.Add("ENDDATE");
            Approvaltransaction.Columns.Add("DESCRIPTION");
            Approvaltransaction.Columns.Add("VALIDSTATE");
            Approvaltransaction.Columns.Add("_STATE_");

            DataTable Governancesite = new DataTable();
            Governancesite.Columns.Add("GOVERNANCENO");
            Governancesite.Columns.Add("GOVERNANCETYPE");
            Governancesite.Columns.Add("ENTERPRISEID");
            Governancesite.Columns.Add("PLANTID");
            Governancesite.Columns.Add("SEQUENCE");
            Governancesite.Columns.Add("STATE");
            Governancesite.Columns.Add("APPROVEDDATE");
            Governancesite.Columns.Add("_STATE_");


            Dictionary<string, object> Param = new Dictionary<string, object>();
            Param.Add("ENTERPRISEID", row["ENTERPRISEID"].ToString());
            Param.Add("PLANTID", row["PLANTID"].ToString());
            Param.Add("APPROVALTYPE", row["APPROVALTYPE"].ToString());
            Param.Add("APPROVALID", row["APPROVALID"].ToString());

            DataTable dt = SqlExecuter.Query("GetApprovalTransactionList", "10001", Param);


            DataRow rowAt = Approvaltransaction.NewRow();
            rowAt["APPROVALTYPE"] = row["APPROVALTYPE"];
            rowAt["APPROVALID"] = row["APPROVALID"];
            rowAt["ENTERPRISEID"] = row["ENTERPRISEID"];
            rowAt["PLANTID"] = row["PLANTID"];

            if (dt != null)
            {
                if (dt.Rows.Count != 0)
                {
                    rowAt["SEQUENCE"] = decimal.Parse(dt.Compute("MAX(SEQUENCE)", "").ToString()) + 1;
                }
                else
                {
                    rowAt["SEQUENCE"] = 1;
                }
            }
            else
            {
                rowAt["SEQUENCE"] = 1;
            }


            rowAt["RESULTS"] = "Reject"; 
             rowAt["RESULTTYPE"] = "Approved";
            rowAt["ACTOR"] = UserInfo.Current.Id;

            if (dt != null)
            {
                if (dt.Rows.Count != 0)
                {
                    rowAt["STARTDATE"] = DateTime.Parse(dt.Compute("MAX(ENDDATE)", "").ToString()).ToString("yyyy-MM-dd hh:mm:ss");
                }
            }

            rowAt["ENDDATE"] = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            rowAt["_STATE_"] = "added";
            rowAt["DESCRIPTION"] = row["DESCRIPTION"];
            Approvaltransaction.Rows.Add(rowAt);


            DataRow rowApproval = Approval.NewRow();

            rowApproval["APPROVALTYPE"] = row["APPROVALTYPE"];
            rowApproval["APPROVALID"] = row["APPROVALID"];
            rowApproval["ENTERPRISEID"] = row["ENTERPRISEID"];
            rowApproval["PLANTID"] = row["PLANTID"];


            if (row["APPROVER"].ToString() == UserInfo.Current.Id)
            {
                rowApproval["APPROVALSTATUS"] = "Reject";
                //rowApproval["APPROVEDATE"] = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                rowApproval["_STATE_"] = "modified";
            }
            else
            {
                ShowMessage("Powers"); // 조회할 데이터가 없습니다.
                return;
            }

            Approval.Rows.Add(rowApproval);


            switch (row["APPROVALTYPE"].ToString())
            {
                case "RunningChange":

                    Dictionary<string, object> ParamGv = new Dictionary<string, object>();
                    ParamGv.Add("ENTERPRISEID", row["ENTERPRISEID"].ToString());
                    ParamGv.Add("GOVERNANCENO", row["APPROVALID"].ToString());
                    DataTable dtGv = SqlExecuter.Query("GetGovernacesitePlant", "10001", ParamGv);

                    DataRow rowGr = Governancesite.NewRow();
                    rowGr["GOVERNANCETYPE"] = row["APPROVALTYPE"];
                    rowGr["GOVERNANCENO"] = row["APPROVALID"];
                    rowGr["ENTERPRISEID"] = row["ENTERPRISEID"];
                    rowGr["PLANTID"] = row["PLANTID"];
                    rowGr["STATE"] = "Reject";
                    rowGr["SEQUENCE"] = dtGv.Select("GOVERNANCENO = '" + row["APPROVALID"].ToString() + "' AND PLANTID = '" + row["PLANTID"].ToString() + "'")[0]["SEQUENCE"].ToString();
                    rowGr["_STATE_"] = "modified";
                    Governancesite.Rows.Add(rowGr);

                    break;
                case "NewRequest":
                    Dictionary<string, object> ParamGn = new Dictionary<string, object>();
                    ParamGn.Add("ENTERPRISEID", row["ENTERPRISEID"].ToString());
                    ParamGn.Add("GOVERNANCENO", row["APPROVALID"].ToString());
                    DataTable dtGn = SqlExecuter.Query("GetGovernacesitePlant", "10001", ParamGn);

                    DataRow rowGn = Governancesite.NewRow();
                    rowGn["GOVERNANCETYPE"] = row["APPROVALTYPE"];
                    rowGn["GOVERNANCENO"] = row["APPROVALID"];
                    rowGn["ENTERPRISEID"] = row["ENTERPRISEID"];
                    rowGn["PLANTID"] = row["PLANTID"];
                    rowGn["STATE"] = "Reject";
                    rowGn["SEQUENCE"] = dtGn.Select("GOVERNANCENO = '" + row["APPROVALID"].ToString() + "' AND PLANTID = '" + row["PLANTID"].ToString() + "'")[0]["SEQUENCE"].ToString();
                    rowGn["_STATE_"] = "modified";
                    Governancesite.Rows.Add(rowGn);
                    break;
            }


            Governancesite.TableName = "governanceSite";
            Approvaltransaction.TableName = "approvaltransaction";
            Approval.TableName = "approva";

            DataSet ds = new DataSet();
            ds.Tables.Add(Governancesite);
            ds.Tables.Add(Approval);
            ds.Tables.Add(Approvaltransaction);
            ExecuteRule("Approvaltransaction", ds);
            ShowMessage("SuccedSave");
            SaveSearch();

        }

        private void BtnCancelRequest_Click(object sender, EventArgs e)
        {

            DataRow row = grdApproval.View.GetFocusedDataRow();

            if (row["APPROVALSTATUS"].ToString() != "RequestApproval")
            {
                ShowMessage("RequestApproval");
                return;

            }

            DataTable Approval = new DataTable();
            Approval.Columns.Add("APPROVALTYPE");
            Approval.Columns.Add("APPROVALID");
            Approval.Columns.Add("ENTERPRISEID");
            Approval.Columns.Add("PLANTID");
            Approval.Columns.Add("APPROVALSTATUS");
            Approval.Columns.Add("APPROVEDATE");
            Approval.Columns.Add("_STATE_");


            DataTable Approvaltransaction = new DataTable();
            Approvaltransaction.Columns.Add("APPROVALTYPE");
            Approvaltransaction.Columns.Add("APPROVALID");
            Approvaltransaction.Columns.Add("ENTERPRISEID");
            Approvaltransaction.Columns.Add("PLANTID");
            Approvaltransaction.Columns.Add("SEQUENCE");
            Approvaltransaction.Columns.Add("RESULTS");
            Approvaltransaction.Columns.Add("RESULTTYPE");
            Approvaltransaction.Columns.Add("ACTOR");
            Approvaltransaction.Columns.Add("STARTDATE");
            Approvaltransaction.Columns.Add("ENDDATE");
            Approvaltransaction.Columns.Add("DESCRIPTION");
            Approvaltransaction.Columns.Add("VALIDSTATE");
            Approvaltransaction.Columns.Add("_STATE_");


            DataTable Governancesite = new DataTable();
            Governancesite.Columns.Add("GOVERNANCENO");
            Governancesite.Columns.Add("GOVERNANCETYPE");
            Governancesite.Columns.Add("ENTERPRISEID");
            Governancesite.Columns.Add("PLANTID");
            Governancesite.Columns.Add("STATE");
            Governancesite.Columns.Add("APPROVEDDATE");
            Governancesite.Columns.Add("SEQUENCE");
            Governancesite.Columns.Add("_STATE_");



            Dictionary<string, object> Param = new Dictionary<string, object>();
            Param.Add("ENTERPRISEID", row["ENTERPRISEID"].ToString());
            Param.Add("PLANTID", row["PLANTID"].ToString());
            Param.Add("APPROVALTYPE", row["APPROVALTYPE"].ToString());
            Param.Add("APPROVALID", row["APPROVALID"].ToString());

            DataTable dt = SqlExecuter.Query("GetApprovalTransactionList", "10001", Param);


            DataRow rowAt = Approvaltransaction.NewRow();
            rowAt["APPROVALTYPE"] = row["APPROVALTYPE"];
            rowAt["APPROVALID"] = row["APPROVALID"];
            rowAt["ENTERPRISEID"] = row["ENTERPRISEID"];
            rowAt["PLANTID"] = row["PLANTID"];

            if (dt != null)
            {
                if (dt.Rows.Count != 0)
                {
                    rowAt["SEQUENCE"] = decimal.Parse(dt.Compute("MAX(SEQUENCE)", "").ToString()) + 1;
                }
                else
                {
                    rowAt["SEQUENCE"] = 1;
                }
            }
            else
            {
                rowAt["SEQUENCE"] = 1;
            }


            rowAt["RESULTS"] = "CancelRequest"; 
             rowAt["RESULTTYPE"] = "Requester";
            rowAt["ACTOR"] = UserInfo.Current.Id;

            if (dt != null)
            {
                if (dt.Rows.Count != 0)
                {
                    rowAt["STARTDATE"] =DateTime.Parse( dt.Compute("MAX(ENDDATE)", "").ToString()).ToString("yyyy-MM-dd hh:mm:ss");
                }
            }

            rowAt["ENDDATE"] = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            rowAt["_STATE_"] = "added";
            rowAt["DESCRIPTION"] = row["DESCRIPTION"];
            Approvaltransaction.Rows.Add(rowAt);


            DataRow rowApproval = Approval.NewRow();

            rowApproval["APPROVALTYPE"] = row["APPROVALTYPE"];
            rowApproval["APPROVALID"] = row["APPROVALID"];
            rowApproval["ENTERPRISEID"] = row["ENTERPRISEID"];
            rowApproval["PLANTID"] = row["PLANTID"];

            // 요청자
            if (row["REQUESTOR"].ToString() == UserInfo.Current.Id)
            {
                rowApproval["APPROVALSTATUS"] = "CancelRequest";
                //rowApproval["APPROVEDATE"] = "" DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                rowApproval["_STATE_"] = "modified";
            }
            else
            {
                ShowMessage("Powers"); // 조회할 데이터가 없습니다.
                return;
            }

            Approval.Rows.Add(rowApproval);


            switch (row["APPROVALTYPE"].ToString())
            {
                case "RunningChange":
                case "NewRequest":

                    Dictionary<string, object> ParamGv = new Dictionary<string, object>();
                    ParamGv.Add("ENTERPRISEID", row["ENTERPRISEID"].ToString());
                    ParamGv.Add("GOVERNANCENO", row["APPROVALID"].ToString());
                    DataTable dtGv = SqlExecuter.Query("GetGovernacesitePlant", "10001", ParamGv);


                    string sSEQUENCE = dtGv.Select("GOVERNANCENO = '" + row["APPROVALID"].ToString() + "' AND PLANTID = '" + row["PLANTID"].ToString() + "'")[0]["SEQUENCE"].ToString();

                 


                    DataRow rowGs = Governancesite.NewRow();
                    rowGs["GOVERNANCETYPE"] = row["APPROVALTYPE"];
                    rowGs["GOVERNANCENO"] = row["APPROVALID"];
                    rowGs["ENTERPRISEID"] = row["ENTERPRISEID"];
                    rowGs["PLANTID"] = row["PLANTID"];
                    rowGs["STATE"] = "CancelRequest";
                    // rowGs["APPROVEDDATE"] = sAPPROVEDATE;

                    rowGs["SEQUENCE"] = sSEQUENCE;


                    rowGs["_STATE_"] = "modified";
                    Governancesite.Rows.Add(rowGs);
                    break;
            }

          

            Governancesite.TableName = "governanceSite";
            Approvaltransaction.TableName = "approvaltransaction";
            Approval.TableName = "approva";

            DataSet ds = new DataSet();
            ds.Tables.Add(Governancesite);
            ds.Tables.Add(Approval);
            ds.Tables.Add(Approvaltransaction);
            ExecuteRule("Approvaltransaction", ds);
            ShowMessage("SuccedSave");
            SaveSearch();

        }

        #endregion

        void SaveSearch()
        {
            var values = this.Conditions.GetValues();
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            values["USERID"] = UserInfo.Current.Id;
          
            DataTable dtApproval = SqlExecuter.Query("GetApprovalList", "10001", values);
           
            grdApprovaltransaction.DataSource = null;
            DataTable dt = (DataTable)grdApproval.DataSource;
            dt.Clear();
            this.grdApproval.DataSource = dtApproval;
        }


        #region 툴바
        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

         

        }
        #endregion

        #region 검색

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
		{
			await base.OnSearchAsync();

            // 조회조건에 입력된 값들을 Dictionary<string, object> Type으로 가져옴

            var values = this.Conditions.GetValues();
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            values["USERID"] = UserInfo.Current.Id;
            

            DataTable dtApproval = await SqlExecuter.QueryAsync("GetApprovalList", "10001", values);

            if (dtApproval.Rows.Count < 1) // 
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }
            grdApprovaltransaction.DataSource = null;
            
            this.grdApproval.DataSource = dtApproval;

        }
        #endregion
        /// <summary>
        /// 검색조건 초기화. 
        /// 조회조건 정보, 메뉴 - 조회조건 매핑 화면에 등록된 정보를 기준으로 구성됩니다.
        /// DB에 등록한 정보를 제외한 추가 조회조건 구성이 필요한 경우 사용합니다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();
            //승인유형
            Conditions.AddComboBox("APPROVALTYPE", new SqlQuery("GetCodeList", "00001", "CODECLASSID=ApprovalType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID").SetEmptyItem();
            //처리상태
            Conditions.AddComboBox("APPROVALSTATUS", new SqlQuery("GetCodeList", "00001", "CODECLASSID=ApprovalStatus", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID").SetEmptyItem();

            InitializeCondition_Popup();
        

        }

        /// <summary>
        /// 검색조건 팝업 예제
        /// </summary>
        private void InitializeCondition_Popup()
        {
            //팝업 컬럼 설정
            var parentPopupColumn = Conditions.AddSelectPopup("USERID", new SqlQuery("GetUserAreaPerson", "10001"), "USERNAME", "USERID")
               .SetPopupLayout("USERID", PopupButtonStyles.Ok_Cancel, true, false)  //팝업창 UI 설정.
               .SetPopupResultCount(1)
               .SetIsReadOnly();  //팝업창 선택가능한 개수
               

            // 팝업에서 사용할 조회조건 항목 추가
            parentPopupColumn.Conditions.AddTextBox("USERNAME");

            // 팝업 그리드 설정
            parentPopupColumn.GridColumns.AddTextBoxColumn("USERID", 100);
            parentPopupColumn.GridColumns.AddTextBoxColumn("USERNAME", 150);
            
        }
        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();
            SmartSelectPopupEdit Popupedit = Conditions.GetControl<SmartSelectPopupEdit>("USERID");
            Popupedit.EditValue = UserInfo.Current.Id;
            Popupedit.Text = UserInfo.Current.Name;
            //Popupedit.Validated += Popupedit_Validated;

        }


        #region 유효성 검사
        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            DataTable changed = new DataTable();
            grdProcessdefinition.View.CheckValidation();
            changed = grdProcessdefinition.GetChangedRows();

            DataTable changed1 = new DataTable();
            grdProcessPath.View.CheckValidation();
            changed1 = grdProcessPath.GetChangedRows();

            //DataTable changed2 = new DataTable();
            //grdOperation.View.CheckValidation();
            //changed2 = grdOperation.GetChangedRows();

            //if (changed.Rows.Count == 0 && changed1.Rows.Count == 0 && changed2.Rows.Count == 0)
            //{
            //    // 저장할 데이터가 존재하지 않습니다.
            //    throw MessageException.Create("NoSaveData");
            //}
        }
        #endregion


        /// <summary>
        /// 팝업 컬럼에서 확인 클릭 시 다른 데이터 매핑 ( 작업장)
        /// </summary>
        /// <param name="currentGridRow">화면 grid row</param>
        /// <param name="popupSelections">팝업에서 선택한 row</param>
        /// <returns></returns>
        private Micube.Framework.SmartControls.Validations.ValidationResultCommon ValidationProcessSegMentPopup(DataRow currentGridRow, IEnumerable<DataRow> popupSelections)
        {
            Micube.Framework.SmartControls.Validations.ValidationResultCommon result = new Framework.SmartControls.Validations.ValidationResultCommon();

            foreach (DataRow row in popupSelections)
            {
                currentGridRow["PROCESSSEGMENTNAME"] = row["PROCESSSEGMENTNAME"];
                currentGridRow["PROCESSSEGMENTVERSION"] = row["PROCESSSEGMENTVERSION"];
                
                currentGridRow["AREAID"] = row["AREAID"];
                currentGridRow["AREANAME"] = row["AREANAME"];
                currentGridRow["EQUIPMENTCLASSID"] = row["EQUIPMENTCLASSID"];
                currentGridRow["EQUIPMENTCLASSNAME"] = row["EQUIPMENTCLASSNAME"];

                currentGridRow["WAREHOUSEID"] = row["WAREHOUSEID"];
                currentGridRow["WAREHOUSENAME"] = row["WAREHOUSENAME"];
            }
            return result;
        }

        /// <summary>
        /// 팝업 컬럼에서 확인 클릭 시 다른 데이터 매핑 ( 작업장)
        /// </summary>
        /// <param name="currentGridRow">화면 grid row</param>
        /// <param name="popupSelections">팝업에서 선택한 row</param>
        /// <returns></returns>
        private Micube.Framework.SmartControls.Validations.ValidationResultCommon ValidationSpecificationsItemPopup(DataRow currentGridRow, IEnumerable<DataRow> popupSelections)
        {
            Micube.Framework.SmartControls.Validations.ValidationResultCommon result = new Framework.SmartControls.Validations.ValidationResultCommon();

            foreach (DataRow row in popupSelections)
            {
                currentGridRow["PROCESSDEFVERSION"] = row["ITEMVERSION"];
                currentGridRow["PROCESSDEFNAME"] = row["ITEMNAME"];
                
            }
            return result;
        }
        /// <summary>
        /// 팝업 컬럼에서 확인 클릭 시 다른 데이터 매핑 ( 작업장)
        /// </summary>
        /// <param name="currentGridRow">화면 grid row</param>
        /// <param name="popupSelections">팝업에서 선택한 row</param>
        /// <returns></returns>
        private Micube.Framework.SmartControls.Validations.ValidationResultCommon ValidationBillofMaterialPopup(DataRow currentGridRow, IEnumerable<DataRow> popupSelections)
        {
            Micube.Framework.SmartControls.Validations.ValidationResultCommon result = new Framework.SmartControls.Validations.ValidationResultCommon();

            foreach (DataRow row in popupSelections)
            {
                currentGridRow["MATERIALDEFNAME"] = row["ITEMNAME"];
                currentGridRow["MATERIALDEFVERSION"] = row["ITEMVERSION"];
                currentGridRow["UNIT"] = row["UOMDEFID"];
               

            }
            return result;
        }

    }
}

