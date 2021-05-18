using Micube.Framework;
using Micube.Framework.Net;
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

namespace Micube.SmartMES.StandardInfo
{
    public partial class SetApproveListPopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region Interface
        public DataRow CurrentDataRow { get; set; }
        #endregion

        #region Local Variables
        private string _governanceNo = string.Empty;
		private string _governanceType = string.Empty;
        #endregion

        #region 생성자
        public SetApproveListPopup(string goverNo, string goverType, string specOwnerId, string specOwnerName)
        {
            InitializeComponent();
            InitializeGrid(goverNo, goverType, specOwnerId, specOwnerName);
            InitializeEvent();

			DataBindApprovalList(goverNo, goverType);
			_governanceNo = goverNo;
			_governanceType = goverType;

		}
        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// Grid 초기화
        /// </summary>
        private void InitializeGrid(string goverNo, string goverType, string specOwnerId, string specOwnerName)
        {
            grdApproveList.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
			grdApproveList.GridButtonItem = GridButtonItem.None;

			//거버넌스NO
			grdApproveList.View.AddTextBoxColumn("GOVERNANCENO", 100).SetDefault(goverNo).SetIsHidden();
			//거버넌스TYPE
			grdApproveList.View.AddTextBoxColumn("GOVERNANCETYPE", 100).SetDefault(goverType).SetIsHidden();
			//거버넌스 상태
			grdApproveList.View.AddComboBoxColumn("GOVERNANCESTATE", new SqlQuery("GetTypeList", "10001", "CODECLASSID=GovernanceState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
				.SetTextAlignment(TextAlignment.Center).SetLabel("APPROVALSTATE").SetIsReadOnly();

			//사양담당자ID
			grdApproveList.View.AddTextBoxColumn("SPECOWNER", 100).SetDefault(specOwnerId).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
			//사양담당자명
			grdApproveList.View.AddTextBoxColumn("SPECOWNERNAME", 100).SetDefault(specOwnerName).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
			//검토자ID
			Initialize_Reviewer();
			//검토자명
            grdApproveList.View.AddTextBoxColumn("REVIEWERNAME", 100).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
			//결재상태
			grdApproveList.View.AddComboBoxColumn("REVIEWERSTATE", new SqlQuery("GetTypeList", "10001", "CODECLASSID=ApprovalStatus", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
				.SetTextAlignment(TextAlignment.Center).SetLabel("STATE").SetIsReadOnly();
			//승인자ID
			Initialize_Approver();
			//승인자명
            grdApproveList.View.AddTextBoxColumn("APPROVERNAME", 100).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
			//결재상태
			grdApproveList.View.AddComboBoxColumn("APPROVERSTATE", new SqlQuery("GetTypeList", "10001", "CODECLASSID=ApprovalStatus", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
				.SetTextAlignment(TextAlignment.Center).SetLabel("STATE").SetIsReadOnly();

			grdApproveList.View.PopulateColumns();
        }

		/// <summary>
		/// 검토자 선택 팝업
		/// </summary>
		private void Initialize_Reviewer()
		{
			var reviewer = grdApproveList.View.AddSelectPopupColumn("REVIEWERID", 100, new SqlQuery("GetUserList", "10001", $"PLANTID={UserInfo.Current.Plant}"))
				.SetPopupLayout("SELECTUSER", PopupButtonStyles.Ok_Cancel, true, true)
				.SetPopupResultCount(1)
				.SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
				.SetPopupAutoFillColumns("DEPARTMENT")
				.SetPopupResultMapping("REVIEWERID", "USERID")
				.SetPopupApplySelection((selectedRows, dataGridRows) =>
				{
					DataRow row = selectedRows.FirstOrDefault();
					if (row == null)
					{
						dataGridRows["REVIEWERNAME"] = string.Empty;
						return;
					}
					dataGridRows["REVIEWERNAME"] = row["USERNAME"].ToString();
				})
				.SetTextAlignment(TextAlignment.Center);


			reviewer.Conditions.AddTextBox("USERIDNAME");

			reviewer.GridColumns.AddTextBoxColumn("USERID", 100);
			reviewer.GridColumns.AddTextBoxColumn("USERNAME", 100);
			reviewer.GridColumns.AddTextBoxColumn("DEPARTMENT", 100);
		}

		/// <summary>
		/// 승인자 선택 팝업
		/// </summary>
		private void Initialize_Approver()
		{
			var approver = grdApproveList.View.AddSelectPopupColumn("APPROVER", 100, new SqlQuery("GetUserList", "10001", $"PLANTID={UserInfo.Current.Plant}"))
				.SetPopupLayout("SELECTUSER", PopupButtonStyles.Ok_Cancel, true, true)
				.SetPopupResultCount(1)
				.SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
				.SetPopupAutoFillColumns("DEPARTMENT")
				.SetPopupResultMapping("APPROVER", "USERID")
				.SetPopupApplySelection((selectedRows, dataGridRows) =>
				{
					DataRow row = selectedRows.FirstOrDefault();
					if(row == null)
					{
						dataGridRows["APPROVERNAME"] = string.Empty;
						return;
					}
					dataGridRows["APPROVERNAME"] = row["USERNAME"].ToString();
				})
				.SetTextAlignment(TextAlignment.Center);

			approver.Conditions.AddTextBox("USERIDNAME");

			approver.GridColumns.AddTextBoxColumn("USERID", 100);
			approver.GridColumns.AddTextBoxColumn("USERNAME", 100);
			approver.GridColumns.AddTextBoxColumn("DEPARTMENT", 100);
		}
		#endregion

		#region Event

		/// <summary>
		/// Event 초기화 
		/// </summary>
		private void InitializeEvent()
        {
            //취소버튼 클릭 이벤트
            btnCancel.Click += (s, e) =>
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            };

            //확인버튼 클릭 이벤트
            btnOK.Click += BtnOK_Click;
			grdApproveList.View.AddingNewRow += View_AddingNewRow;
        }

		/// <summary>
		/// 그리드 row 추가 시 이벤트 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
		{
			DataTable dt = grdApproveList.DataSource as DataTable;

			if (dt.Rows.Count > 1)
			{
				//결재 라인 수를 초과하였습니다.
				ShowMessage("OVERAPPROVALLINE");
				args.IsCancel = true;
				return;
			}

			args.NewRow["APPROVALID"] = _governanceNo;
			args.NewRow["APPROVALTYPE"] = _governanceType;
		}

		/// <summary>
		/// 확인 버튼 클릭 시 결재라인 저장
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnOK_Click(object sender, EventArgs e)
        {			
			DataTable dt = grdApproveList.DataSource as DataTable;
			
			if(string.IsNullOrWhiteSpace(Format.GetString(dt.Rows[0]["APPROVER"])) 
			|| string.IsNullOrWhiteSpace(Format.GetString(dt.Rows[0]["APPROVERNAME"])))
			{
				//승인자를 지정하세요.
				ShowMessage("SETAPPROVER2");
				return;
			}

			//검토자 지정할 경우 검토자명 빈값 확인
			if(!string.IsNullOrWhiteSpace(Format.GetString(dt.Rows[0]["REVIEWERID"])))
			{
				if(string.IsNullOrWhiteSpace(Format.GetString(dt.Rows[0]["REVIEWERNAME"])))
				{
					//검토자 명이 비어있습니다. 검토자를 다시 선택해주세요.
					ShowMessage("ISEMPTYREVIEWERNAME");
					return;
				}
			}

			string governanceState = Format.GetString(dt.Rows[0]["GOVERNANCESTATE"]);
            bool retFlag = false;
			switch(governanceState)
			{
				case "Request":
					break;

				case "Working":
					//이미 결재 상신되어 진행중입니다.
					ShowMessage("ALREADYSETTEDAPPROVAL");
					break;

				case "Reject":
					//재기안 하시겠습니까?
					if(ShowMessageBox("ISREDRAFT", "Caption", MessageBoxButtons.OKCancel) == DialogResult.OK)
					{
                        if (string.Compare(dt.Rows[0]["REVIEWERID"].ToString(), dt.Rows[0]["APPROVER"].ToString()) == 0)
                        {
                            ShowMessage("SAMEREVIEWERANDAPPROVER");
                            return;
                        }
                        retFlag = SaveProcess(dt, true);
					}
					
					break;

				case "Approved":
				case "Input":
					//결재 완료된 건 입니다.
					ShowMessage("ISAPPROVED");
					break;

				default:
                    if (string.Compare(dt.Rows[0]["REVIEWERID"].ToString(), dt.Rows[0]["APPROVER"].ToString()) == 0)
                    {
						//검토자와 승인자는 동일인을 지정할 수 없습니다.
						ShowMessage("SAMEREVIEWERANDAPPROVER");
                        return;
                    }
                    retFlag = SaveProcess(dt);
					break;
			}
            if (retFlag)
			    this.Close();
		}

		#endregion

		/// <summary>
		/// 저장 프로세스
		/// </summary>
		/// <param name="dt"></param>
		private bool SaveProcess(DataTable dt, bool isReDraft = false)
		{
			string reviewer = Format.GetString(dt.Rows[0]["REVIEWERID"]);
			string approver = Format.GetString(dt.Rows[0]["APPROVER"]);

			if (string.IsNullOrEmpty(approver))
			{
				//승인자를 지정하세요.
				ShowMessage("SETAPPROVER2");
				return false;
			}

			MessageWorker worker = new MessageWorker("SaveRequestApproval");
			worker.SetBody(new MessageBody()
			{
				{ "requester", UserInfo.Current.Id },
				{ "enterpriseId", UserInfo.Current.Enterprise },
				{ "plantId", UserInfo.Current.Plant },
				{ "approveList", dt },
				{ "isReDraft", isReDraft }
			});

			worker.Execute();
			ShowMessage("SuccessSave");
            return true;
		}


		#region Private Function
		/// <summary>
		/// 셋팅된 결재라인 불러오기
		/// </summary>
		/// <param name="goverNo"></param>
		/// <param name="goverType"></param>
		private void DataBindApprovalList(string goverNo, string goverType)
		{
			DataTable dt  = SqlExecuter.Query("GetApprovalListSetted", "10002", new Dictionary<string, object>(){ { "GOVERNANCENO", goverNo },
																												  { "GOVERNANCETYPE", goverType },
																												  { "ENTERPRISEID", UserInfo.Current.Enterprise },
																												  { "PLANTID", UserInfo.Current.Plant } });
			if(dt.Rows.Count == 0)
			{
				grdApproveList.DataSource = SqlExecuter.Query("GetApprovalListSetted", "10001", new Dictionary<string, object>(){ { "GOVERNANCENO", goverNo },
																																  { "GOVERNANCETYPE", goverType },
																																  { "ENTERPRISEID", UserInfo.Current.Enterprise },
																																  { "PLANTID", UserInfo.Current.Plant } });
			}
			else
			{
				grdApproveList.DataSource = dt;
			}
		}
		#endregion

	}
}
