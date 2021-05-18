#region using
using Micube.Framework.Net;
using Micube.Framework;
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
using DevExpress.XtraCharts;
using DevExpress.Utils;
using Micube.Framework.SmartControls.Validations;
using Micube.Framework.SmartControls.Grid.BandedGrid;
using System.IO;
using DevExpress.Spreadsheet;
using SmartDeploy.Common;
using Micube.SmartMES.Commons.Controls;
using System.Net;
using DevExpress.Utils.Menu;
#endregion

namespace Micube.SmartMES.StandardInfo
{
	/// <summary>
	/// 프로그램명 : 기준정보 > 사양진행관리 > RunningChange등록
	/// 업 무 설명 : RunningChange 등록 
	/// 생  성  자 : 윤성원
	/// 생  성  일 : 2019-06-27
	/// 수정 이 력 : 2019-11-25 정승원 설계변경
	/// 
	/// 
	/// </summary> 
	public partial class GovernanceChange2 : SmartConditionManualBaseForm
	{
		#region Local Variables
		private string _pcnNo = string.Empty;
		private string _productDefId = string.Empty;
		private string _productDefVer = string.Empty;
		private string _productName = string.Empty;
		private string _rcProductDefId = string.Empty;
		private string _rcProductDefVer = string.Empty;
		private string _rcProductName = string.Empty;
		private string _governanceNo = string.Empty;
		private string _governanceType = string.Empty;
		private string _state = string.Empty;
		private bool _isNotSelected = false;
		private bool _isLoadForm = false;
		private bool _isRemoved = false;
		private bool _isInit = true;

		public string _UploadPath { get; set; } = "GovernanceChange";
		private DataTable _inputDt = new DataTable();
		private DataTable dtFile;
		public ResourceInfo Resource { get; set; } = new ResourceInfo();
		private List<DXMenuItem> menuList = new List<DXMenuItem>();
		#endregion

		#region 생성자
		public GovernanceChange2()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 외부에서 호출시 자동 조회
		/// </summary>
		/// <param name="parameters"></param>
		public override void LoadForm(Dictionary<string, object> parameters)
		{
			base.LoadForm(parameters);

			if (parameters != null)
			{
				_parameters = parameters;


				if (_parameters != null && _parameters.ContainsKey("CALLMENU") && _parameters["CALLMENU"].Equals("ProductHistoryView"))
				{
					tabGovernanceChange.SelectedTabPageIndex = 1;

					if (_parameters.ContainsKey("P_GOVERNANCENO"))
					{
						Conditions.GetControl<SmartSelectPopupEdit>("P_GOVERNANCENO").SetValue(parameters["P_GOVERNANCENO"]);
						Conditions.SetValue("P_GOVERNANCENO", 0, parameters["P_GOVERNANCENO"]);
					}

					if (_parameters.ContainsKey("PRODUCTDEFID"))
						_productDefId = parameters["PRODUCTDEFID"].ToString();

					if (_parameters.ContainsKey("PRODUCTDEFVERSION"))
						_productDefVer = parameters["PRODUCTDEFVERSION"].ToString();

					if (_parameters.ContainsKey("RCPRODUCTDEFID"))
						_rcProductDefId = parameters["RCPRODUCTDEFID"].ToString();

					if (_parameters.ContainsKey("RCPRODUCTDEFVERSION"))
						_rcProductDefVer = parameters["RCPRODUCTDEFVERSION"].ToString();

					_isLoadForm = true;

					OnSearchAsync(); // 조회
				}
			}
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

			InitializeChangePointGrid();

            InitializeFileGrid();
            InitializeRoutingGrid();
			InitializeInputControls();

			InitializeQuickMenuList();

			/*
            //버튼 숨기기
            if (UserInfo.Current.Enterprise == "INTERFLEX")
            {
                smartLabel7.Visible = false;
                chkRC.Visible = false;
                chkNext.Visible = false;
            }
			*/

			btnEventToggle.Hide();
			btnEventToggle.Text = Language.Get("EACHTIME");
			var buttons = pnlToolbar.Controls.Find<SmartButton>(true);
			foreach (SmartButton button in buttons)
			{
				if (button.Name == "Confirmation" || button.Name == "Compare" || button.Name == "CancelConfirmation")
					button.Visible = false;
				else
					button.Visible = true;
			}

		}

		/// <summary>
		/// 퀵 메뉴 리스트 등록
		/// </summary>
		private void InitializeQuickMenuList()
		{
			menuList.Add(new DXMenuItem(Language.Get("MENU_PG-QC-0350"), OpenForm) { BeginGroup = true, Tag = "PG-QC-0350" });
		}

		/// <summary>
		/// 변경점 신청 LIST 조회
		/// </summary>
		private void InitializeChangePointGrid()
		{
			grdChangePointList.GridButtonItem = GridButtonItem.Export | GridButtonItem.Delete;
			grdChangePointList.View.SetIsReadOnly();
			grdChangePointList.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

			// 변경점 구분
			grdChangePointList.View.AddTextBoxColumn("RCSTATE", 60).SetTextAlignment(TextAlignment.Center);
			grdChangePointList.View.AddTextBoxColumn("RCSTATEID").SetIsHidden();
			// 변경점 신청일
			grdChangePointList.View.AddTextBoxColumn("REQUESTDATE", 80).SetTextAlignment(TextAlignment.Center).SetLabel("CHANGEPOINTDATE");
			//변경구분(사내/고객)
			grdChangePointList.View.AddComboBoxColumn("RCDIVISION", 60, new SqlQuery("GetTypeList", "10001", "CODECLASSID=GovernanceChangeType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetTextAlignment(TextAlignment.Center);
			//적용구분(RC/차기)
			grdChangePointList.View.AddComboBoxColumn("IMPLEMENTATIONTYPE", 60, new SqlQuery("GetTypeList", "10001", "CODECLASSID=GovernanceImplementationType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetTextAlignment(TextAlignment.Center);
			
			//고객사(기존)
			grdChangePointList.View.AddComboBoxColumn("EXISTCUSTOMER", 100, new SqlQuery("GetCustomerList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "CUSTOMERNAME", "CUSTOMERID");
			//품목 ID(기존)
			grdChangePointList.View.AddTextBoxColumn("PRODUCTDEFID", 100).SetLabel("EXISTINGPRODUCTDEFID");
			//품목 Version(기존)
			grdChangePointList.View.AddTextBoxColumn("PRODUCTDEFVERSION", 50).SetTextAlignment(TextAlignment.Center).SetLabel("EXISTINGPRODUCTDEFVERSION");
			//품목명(기존)
			grdChangePointList.View.AddTextBoxColumn("ITEMNAME", 200).SetLabel("EXISTINGPRODUCTDEFNAME");
			//사양담당자(기존)
			grdChangePointList.View.AddComboBoxColumn("EXISTSPECOWNER", 70, new SqlQuery("GetUserList", "10001", $"PLANTID={UserInfo.Current.Plant}"), "USERNAME", "USERID");

			//고객사(변경)
			grdChangePointList.View.AddComboBoxColumn("TRANSCUSTOMER", 100, new SqlQuery("GetCustomerList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "CUSTOMERNAME", "CUSTOMERID");
			//RC품목 ID(변경)
			grdChangePointList.View.AddTextBoxColumn("RCPRODUCTDEFID", 100).SetLabel("TRANSPRODUCTDEFID");
			//RC품목 Version(변경)
			grdChangePointList.View.AddTextBoxColumn("RCPRODUCTDEFVERSION", 50).SetTextAlignment(TextAlignment.Center).SetLabel("TRANSPRODUCTDEFVERSION");
			//RC품목명(변경)
			grdChangePointList.View.AddTextBoxColumn("RCITEMNAME", 200).SetLabel("TRANSPRODUCTDEFNAME");
			//사양담당자(변경)
			grdChangePointList.View.AddComboBoxColumn("TRANSSPECOWNER", 70, new SqlQuery("GetUserList", "10001", $"PLANTID={UserInfo.Current.Plant}"), "USERNAME", "USERID");

			//변경점 신청부서
			grdChangePointList.View.AddTextBoxColumn("REQUESTDEPARTMENT", 100).SetLabel("CHANGEPOINTDEPARTMENT");
			//요청자
			grdChangePointList.View.AddTextBoxColumn("REQUESTER", 70).SetIsHidden();
			grdChangePointList.View.AddTextBoxColumn("REQUESTORNAME", 70).SetTextAlignment(TextAlignment.Center);
			//변경사유
			grdChangePointList.View.AddTextBoxColumn("CHANGEREASON", 100);
			//변경점 관리번호
			grdChangePointList.View.AddTextBoxColumn("CHANGEPOINTNO", 120).SetTextAlignment(TextAlignment.Center);
			//거버넌스NO
			grdChangePointList.View.AddTextBoxColumn("GOVERNANCENO", 120).SetTextAlignment(TextAlignment.Center);
			//거버넌스TYPE
			grdChangePointList.View.AddTextBoxColumn("GOVERNANCETYPE", 100).SetIsHidden();
			// 변경제목
			grdChangePointList.View.AddTextBoxColumn("SUBJECT", 100).SetIsHidden();

			grdChangePointList.View.PopulateColumns();
		}

		/// <summary>
		/// 그리드 초기화
		/// </summary>
		private void InitializeRoutingGrid()
		{
			#region 라우팅 LIST
			grdFromRoutingList.GridButtonItem = GridButtonItem.None;
			grdFromRoutingList.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;
			grdFromRoutingList.View.SetIsReadOnly(true);
			grdFromRoutingList.View.AddTextBoxColumn("PLANTID", 60).SetTextAlignment(TextAlignment.Center);
			grdFromRoutingList.View.AddTextBoxColumn("USERSEQUENCE", 60).SetTextAlignment(TextAlignment.Center);
			grdFromRoutingList.View.AddTextBoxColumn("PROCESSSEGMENTID", 80).SetTextAlignment(TextAlignment.Center);
			grdFromRoutingList.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 160);
			grdFromRoutingList.View.AddComboBoxColumn("PROCESSCHANGETYPE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=CompareType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
			//grdFromRoutingList.View.AddComboBoxColumn("MATERIALCHANGETYPE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=CompareType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
			//grdFromRoutingList.View.AddComboBoxColumn("SPECCHANGETYPE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=CompareType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
			//grdFromRoutingList.View.AddComboBoxColumn("TOOLCHANGETYPE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=CompareType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
			//grdFromRouting.View.AddTextBoxColumn("DESCRIPTION", 250);
			grdFromRoutingList.View.PopulateColumns();

			grdToRoutingList.GridButtonItem = GridButtonItem.None;
			grdToRoutingList.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;
			grdToRoutingList.View.SetIsReadOnly(true);
			grdToRoutingList.View.AddTextBoxColumn("PLANTID", 60).SetTextAlignment(TextAlignment.Center);
			grdToRoutingList.View.AddTextBoxColumn("USERSEQUENCE", 60).SetTextAlignment(TextAlignment.Center);
			grdToRoutingList.View.AddTextBoxColumn("PROCESSSEGMENTID", 80).SetTextAlignment(TextAlignment.Center);
			grdToRoutingList.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 160);
			grdToRoutingList.View.AddComboBoxColumn("PROCESSCHANGETYPE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=CompareType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
			grdToRoutingList.View.AddComboBoxColumn("MATERIALCHANGETYPE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=CompareType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
			grdToRoutingList.View.AddComboBoxColumn("SPECCHANGETYPE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=CompareType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
			grdToRoutingList.View.AddComboBoxColumn("TOOLCHANGETYPE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=CompareType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
			//grdToRoutingList.View.AddTextBoxColumn("DESCRIPTION", 250);
			grdToRoutingList.View.PopulateColumns();

			#endregion

			#region 자재 LIST

			grdFromBomList.GridButtonItem = GridButtonItem.None;
			grdFromBomList.View.SetIsReadOnly(true);
			grdFromBomList.View.AddTextBoxColumn("MATERIALDEFID", 100);
			grdFromBomList.View.AddTextBoxColumn("MATERIALDEFNAME", 250);
			grdFromBomList.View.AddTextBoxColumn("MATERIALDEFVERSION", 100);
			grdFromBomList.View.AddSpinEditColumn("QTY", 100)
				.SetDisplayFormat("#,##0.000000000");
			grdFromBomList.View.PopulateColumns();

			grdToBomList.GridButtonItem = GridButtonItem.None;
			grdToBomList.View.SetIsReadOnly(true);
			grdToBomList.View.AddTextBoxColumn("MATERIALDEFID", 100);
			grdToBomList.View.AddTextBoxColumn("MATERIALDEFNAME", 250);
			grdToBomList.View.AddTextBoxColumn("MATERIALDEFVERSION", 100);
			grdToBomList.View.AddSpinEditColumn("QTY", 100)
				.SetDisplayFormat("#,##0.000000000");
			grdToBomList.View.PopulateColumns();

			#endregion

			#region 공정 SPEC LIST

			grdFromSpecList.GridButtonItem = GridButtonItem.None;
			grdFromSpecList.View.SetIsReadOnly();
			var localtionGroup = grdFromSpecList.View.AddGroupColumn("");
			var specGroup = grdFromSpecList.View.AddGroupColumn("GROUPSPEC");
			var controlLimitGroup = grdFromSpecList.View.AddGroupColumn("GROUPCONTROLLIMIT");
			var outlierGroup = grdFromSpecList.View.AddGroupColumn("GROUPOUTLIER");

			//검사항목
			localtionGroup.AddTextBoxColumn("INSPITEM", 100);
			//목표값
			//localtionGroup.AddSpinEditColumn("SL", 100).SetLabel("STDVALUE").SetDisplayFormat("#,##0.0000", MaskTypes.Numeric, true);

			//SPEC 상한
			specGroup.AddSpinEditColumn("USL", 100).SetLabel("USL").SetDisplayFormat("#,##0.0000", MaskTypes.Numeric, true);
            //기준값
            specGroup.AddSpinEditColumn("SL", 100).SetLabel("STDVALUE").SetDisplayFormat("#,##0.####", MaskTypes.Numeric, true);
            //SPEC 하한
            specGroup.AddSpinEditColumn("LSL", 100).SetLabel("LSL").SetDisplayFormat("#,##0.0000", MaskTypes.Numeric, true);

			//CONTROL LIMIT 상한값
			controlLimitGroup.AddSpinEditColumn("UCL", 100).SetLabel("USL").SetDisplayFormat("#,##0.0000", MaskTypes.Numeric, true);
			//CONTROL LIMIT 중앙값
			controlLimitGroup.AddSpinEditColumn("CL", 100).SetLabel("STDVALUE").SetDisplayFormat("#,##0.0000", MaskTypes.Numeric, true);
			//CONTROL LIMIT 하한값
			controlLimitGroup.AddSpinEditColumn("LCL", 100).SetLabel("LSL").SetDisplayFormat("#,##0.0000", MaskTypes.Numeric, true);

			//OUTLIER 상한값
			outlierGroup.AddSpinEditColumn("UOL", 100).SetLabel("USL").SetDisplayFormat("#,##0.0000", MaskTypes.Numeric, true);
			//OUTLIER 하한값
			outlierGroup.AddSpinEditColumn("LOL", 100).SetLabel("LSL").SetDisplayFormat("#,##0.0000", MaskTypes.Numeric, true);

			grdFromSpecList.View.PopulateColumns();


			grdToSpecList.GridButtonItem = GridButtonItem.None;
			grdToSpecList.View.SetIsReadOnly();
			var localtionGroup2 = grdToSpecList.View.AddGroupColumn("");
			var specGroup2 = grdToSpecList.View.AddGroupColumn("GROUPSPEC");
			var controlLimitGroup2 = grdToSpecList.View.AddGroupColumn("GROUPCONTROLLIMIT");
			var outlierGroup2 = grdToSpecList.View.AddGroupColumn("GROUPOUTLIER");

			localtionGroup2.AddTextBoxColumn("INSPITEM", 100);
			//목표값
			//localtionGroup2.AddSpinEditColumn("SL", 100).SetLabel("STDVALUE").SetDisplayFormat("#,##0.0000", MaskTypes.Numeric, true);

			//SPEC 상한
			specGroup2.AddSpinEditColumn("USL", 100).SetLabel("USL").SetDisplayFormat("#,##0.0000", MaskTypes.Numeric, true);
            //기준값
            specGroup2.AddSpinEditColumn("SL", 100).SetLabel("STDVALUE").SetDisplayFormat("#,##0.####", MaskTypes.Numeric, true);
            //SPEC 하한
            specGroup2.AddSpinEditColumn("LSL", 100).SetLabel("LSL").SetDisplayFormat("#,##0.0000", MaskTypes.Numeric, true);

			//CONTROL LIMIT 상한값
			controlLimitGroup2.AddSpinEditColumn("UCL", 100).SetLabel("USL").SetDisplayFormat("#,##0.0000", MaskTypes.Numeric, true);
			//CONTROL LIMIT 중앙값
			controlLimitGroup2.AddSpinEditColumn("CL", 100).SetLabel("STDVALUE").SetDisplayFormat("#,##0.0000", MaskTypes.Numeric, true);
			//CONTROL LIMIT 하한값
			controlLimitGroup2.AddSpinEditColumn("LCL", 100).SetLabel("LSL").SetDisplayFormat("#,##0.0000", MaskTypes.Numeric, true);

			//OUTLIER 상한값
			outlierGroup2.AddSpinEditColumn("UOL", 100).SetLabel("USL").SetDisplayFormat("#,##0.0000", MaskTypes.Numeric, true);
			//OUTLIER 하한값
			outlierGroup2.AddSpinEditColumn("LOL", 100).SetLabel("LSL").SetDisplayFormat("#,##0.0000", MaskTypes.Numeric, true);


			grdToSpecList.View.PopulateColumns();

			#endregion

			#region 치공구 LIST

			grdFromToolList.GridButtonItem = GridButtonItem.None;
			grdFromToolList.View.SetIsReadOnly(true);
			grdFromToolList.View.AddTextBoxColumn("DURABLEDEFID", 100);
			grdFromToolList.View.AddTextBoxColumn("DURABLEDEFVERSION", 60).SetTextAlignment(TextAlignment.Center);
			grdFromToolList.View.AddTextBoxColumn("DURABLEDEFNAME", 150);
			grdFromToolList.View.AddTextBoxColumn("FILMUSELAYER1", 80).SetTextAlignment(TextAlignment.Center);
			grdFromToolList.View.AddTextBoxColumn("FILMUSELAYER2", 80).SetTextAlignment(TextAlignment.Center);
			grdFromToolList.View.AddComboBoxColumn("DURABLETYPE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=DurableClassType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsReadOnly();
			grdFromToolList.View.PopulateColumns();

			grdToToolList.GridButtonItem = GridButtonItem.None;
			grdToToolList.View.SetIsReadOnly(true);
			grdToToolList.View.AddTextBoxColumn("DURABLEDEFID", 100);
			grdToToolList.View.AddTextBoxColumn("DURABLEDEFVERSION", 60).SetTextAlignment(TextAlignment.Center);
			grdToToolList.View.AddTextBoxColumn("DURABLEDEFNAME", 150);
			grdToToolList.View.AddTextBoxColumn("FILMUSELAYER1", 80).SetTextAlignment(TextAlignment.Center);
			grdToToolList.View.AddTextBoxColumn("FILMUSELAYER2", 80).SetTextAlignment(TextAlignment.Center);
			grdToToolList.View.AddComboBoxColumn("DURABLETYPE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=DurableClassType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
			grdToToolList.View.PopulateColumns();

			#endregion
		}


        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeFileGrid()
        {
            //grdFileList.GridButtonItem = GridButtonItem.None;
            grdFileList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdFileList.View.SetSortOrder("SEQUENCE");

            grdFileList.View.AddTextBoxColumn("FILEID", 100)
                .SetIsHidden()
                .SetIsReadOnly();
            grdFileList.View.AddTextBoxColumn("FILENAME", 300)
                .SetIsReadOnly();
            grdFileList.View.AddTextBoxColumn("FILEEXT", 100)
                .SetIsReadOnly();
            grdFileList.View.AddTextBoxColumn("FILEPATH", 200)
                .SetIsHidden()
                .SetIsReadOnly();
            grdFileList.View.AddTextBoxColumn("SAFEFILENAME", 200)
                .SetIsHidden()
                .SetIsReadOnly();
            grdFileList.View.AddSpinEditColumn("FILESIZE", 100)
                .SetDisplayFormat()
                .SetIsReadOnly();
            grdFileList.View.AddSpinEditColumn("SEQUENCE", 70)
                .SetIsHidden()
                .SetIsReadOnly();
            grdFileList.View.AddTextBoxColumn("LOCALFILEPATH", 200)
                .SetIsHidden()
                .SetIsReadOnly();
            grdFileList.View.AddTextBoxColumn("RESOURCETYPE", 100)
                .SetIsHidden()
                .SetIsReadOnly();
            grdFileList.View.AddTextBoxColumn("RESOURCEID", 100)
                .SetIsHidden()
                .SetIsReadOnly();
            grdFileList.View.AddTextBoxColumn("RESOURCEVERSION", 100)
                .SetIsHidden()
                .SetIsReadOnly();
            grdFileList.View.AddTextBoxColumn("COMMENTS", 300);
			grdFileList.View.AddTextBoxColumn("_STATE_").SetIsHidden();

            grdFileList.View.PopulateColumns();

        }


        /// <summary>
        /// 입력 컨트롤 초기화
        /// </summary>
        private void InitializeInputControls()
		{
			txtGovernanceNo.Visible = false;
			txtGovernanceNo.Tag = "GOVERNANCENO";
			txtGovernanceNo.Text = string.Empty;

			//변경점 관리번호
			txtChangePoint.Text = string.Empty;
			txtChangePoint.Tag = "CHANGEPOINTNO";
			txtChangePoint.Enabled = false;
			//InitializePopup_ChangePointNo();

			//품목(기존)
			InitializePopup_PrevProduct();

			//품목버전(기존)
			txtPrevProductRev.ReadOnly = true;
			txtPrevProductRev.Text = string.Empty;
			txtPrevProductRev.Tag = "PRODUCTDEFVERSION";

			//품목명(기존)
			txtPrevProductName.ReadOnly = true;
			txtPrevProductName.Text = string.Empty;
			txtPrevProductName.Tag = "ITEMNAME";

			//품목(변경)
			InitializePopup_Product();
			_isNotSelected = false;

			//품목버전(변경)
			//txtTransProductRev.ReadOnly = true;
			txtTransProductRev.Text = string.Empty;
			txtTransProductRev.Tag = "RCPRODUCTDEFVERSION";
			_isNotSelected = false;

			//품목명(변경)
			txtTransProductName.ReadOnly = true;
			txtTransProductName.Text = string.Empty;
			txtTransProductName.Tag = "RCITEMNAME";

			//요청부서
			txtRequestTeam.ReadOnly = true;
			txtRequestTeam.Text = string.Empty;
			txtRequestTeam.Tag = "REQUESTDEPARTMENT";

			//요청자
			cboRequestor.DisplayMember = "USERNAME";
			cboRequestor.ValueMember = "USERID";
			cboRequestor.UseEmptyItem = true;
			cboRequestor.DataSource = SqlExecuter.Query("GetUserList", "10001",new Dictionary<string, object>() { {"ENTERPRISEID", UserInfo.Current.Enterprise}, {"PLANTID", UserInfo.Current.Plant } });
			cboRequestor.EditValue = null;
			cboRequestor.Text = string.Empty;
			cboRequestor.Tag = "REQUESTORID";
			cboRequestor.Enabled = false;
			//InitializationPopup_Requestor();

			//고객사(기존)
			InitializePopup_Customer(cboPrevCustomer, false);

			//사양담당(기존)
			InitializationPopup_SpecOwner(cboPrevSpecOwner, false);

			//고객사(변경)
			InitializePopup_Customer(cboTransCustomer, true);

			//사양담당(변경)
			InitializationPopup_SpecOwner(cboTransSpecOwner, true);

			//등록일
			txtRegistorDate.ReadOnly = true;
			txtRegistorDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
			txtRegistorDate.EditValue = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
			txtRegistorDate.Tag = "REQUESTDATE";

			//변경사유
			txtTransReason.Text = string.Empty;
			txtTransReason.EditValue = null;
			txtTransReason.Tag = "CHANGEREASON";

			//Fileid
			//txtFileID.Text = string.Empty;
			//txtFileID.EditValue = null;
			//txtFileID.Tag = "FILENAME";

			//변경구분
			chkCustomerType.Checked = false;
			chkOurCompanyType.Checked = false;

			//적용구분
			chkRC.Checked = false;
			chkNext.Checked = false;
		}

		/// <summary>
		/// 변경점 관리번호 팝업 초기화
		/// </summary>
		private void InitializePopup_ChangePointNo()
		{
			/*
			if (popTransProduct.SelectPopupCondition != null) popTransProduct.SelectPopupCondition = null;

			ConditionItemSelectPopup options = new ConditionItemSelectPopup();
			options.SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow);
			options.SetPopupLayout("CHANGEPOINTLIST", PopupButtonStyles.Ok_Cancel, true, false);
			options.Id = "CHANGEPOINTNO";
			options.SearchQuery = new SqlQuery("GetChangePointNo", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}");
			options.DisplayFieldName = "CHANGEPOINTNO";
			options.ValueFieldName = "CHANGEPOINTNO";
			options.LanguageKey = "CHANGEPOINTNO";
			options.SetPopupResultCount(1);
			options.SetPopupApplySelection((selectRow, gridRow) =>
			{
				DataRow row = selectRow.FirstOrDefault();
				if(row == null)
				{
					cboPrevCustomer.EditValue = null;
					cboPrevCustomer.Text = string.Empty;
					//popPrevCustomer.SetValue(null);
					//popPrevCustomer.EditValue = null;
					//popPrevCustomer.Text = string.Empty;
					txtRequestTeam.EditValue = null;
					txtRequestTeam.Text = string.Empty;
					popRequestor.SetValue(null);
					popRequestor.EditValue = null;
					popRequestor.Text = string.Empty;
					return;
				}
				
				cboPrevCustomer.EditValue = Format.GetString(row["CUSTOMERID"]);
				cboPrevCustomer.Text = Format.GetString(row["CUSTOMERNAME"]);
				//popPrevCustomer.SetValue(Format.GetString(row["CUSTOMERID"]));
				//popPrevCustomer.EditValue = Format.GetString(row["CUSTOMERID"]);
				//popPrevCustomer.Text = Format.GetString(row["CUSTOMERNAME"]);
				txtRequestTeam.EditValue = Format.GetString(row["REQUESTDEPARTMENT"]);
				txtRequestTeam.Text = Format.GetString(row["REQUESTDEPARTMENT"]);
				popRequestor.SetValue(Format.GetString(row["REQUESTORID"]));
				popRequestor.EditValue = Format.GetString(row["REQUESTORID"]);
				popRequestor.Text = Format.GetString(row["REQUESTOR"]);

			});

			options.Conditions.AddDateEdit("FROMREQUESTDATE").SetValidationIsRequired().SetDefault(string.Format("{0:yyyy-MM-dd 00:00:00}", DateTime.Now.AddDays(-7)));
			options.Conditions.AddDateEdit("TOREQUESTDATE").SetValidationIsRequired().SetDefault(string.Format("{0:yyyy-MM-dd 23:59:59}", DateTime.Now));
			options.Conditions.AddTextBox("CHANGEPOINTNO");
			options.Conditions.AddTextBox("TXTPRODUCTDEFNAME");

			options.GridColumns.AddTextBoxColumn("CHANGEPOINTNO", 120).SetTextAlignment(TextAlignment.Center);
			options.GridColumns.AddTextBoxColumn("REQUESTDEPARTMENT", 80).SetLabel("CSMREQUESTDEPARTMENT");
			options.GridColumns.AddTextBoxColumn("SUBJECT", 100);
			options.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 100);
			options.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 60).SetTextAlignment(TextAlignment.Center);
			options.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 150);
			options.GridColumns.AddTextBoxColumn("CUTOMERNAME", 80);
			options.GridColumns.AddTextBoxColumn("CUSTOMERID", 80).SetIsHidden();
			options.GridColumns.AddTextBoxColumn("REQUESTOR", 60).SetTextAlignment(TextAlignment.Center);
			options.GridColumns.AddTextBoxColumn("REQUESTORID", 60).SetIsHidden();

			popChangePoint.SelectPopupCondition = options;
			popChangePoint.SetValue(null);
			popChangePoint.EditValue = null;
			popChangePoint.Text = string.Empty;
			popChangePoint.Tag = "CHANGEPOINTNO";
			popChangePoint.Enabled = false;
			*/
		}

		/// <summary>
		/// 품목(기존) 팝업 초기화
		/// </summary>
		private void InitializePopup_PrevProduct(bool isReadOnly = false)
		{
			if (popPrevProduct.SelectPopupCondition != null) popPrevProduct.SelectPopupCondition = null;

			ConditionItemSelectPopup options = new ConditionItemSelectPopup();
			options.SetPopupLayoutForm(800, 800, FormBorderStyle.FixedToolWindow);
			options.SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false);
			options.Id = "ITEMID";
			options.SearchQuery = new SqlQuery("GetItemMasterList", "10002", $"ENTERPRISEID={UserInfo.Current.Enterprise}", "PRODUCTDIVISION=Product");
			options.DisplayFieldName = "ITEMID";
			options.ValueFieldName = "ITEMID";
			options.LanguageKey = "ITEMID";
			options.SetPopupResultCount(1);
			options.SetPopupApplySelection((selectRow, gridRow) =>
			{
				DataRow row = selectRow.FirstOrDefault();
				if (row == null) return;

				popPrevProduct.SetValue(row["ITEMID"].ToString());
				popPrevProduct.Text = row["ITEMID"].ToString();
				txtPrevProductRev.Text = row["ITEMVERSION"].ToString();
				txtPrevProductName.Text = row["ITEMNAME"].ToString();
				cboPrevCustomer.EditValue = row["CUSTOMERID"];
				cboPrevSpecOwner.EditValue = row["SPECOWNERID"];

				if (chkOurCompanyType.Checked)
				{
					InitializePopup_Product("Y");
				}

			});
			options.SetPopupAutoFillColumns("ITEMNAME");


			options.Conditions.AddTextBox("ITEMID");
			options.Conditions.AddTextBox("ITEMVERSION");
			options.Conditions.AddTextBox("ITEMNAME");

			options.GridColumns.AddTextBoxColumn("ITEMID", 100);
			options.GridColumns.AddTextBoxColumn("ITEMVERSION", 60);
			options.GridColumns.AddTextBoxColumn("ITEMNAME", 200);
			options.GridColumns.AddTextBoxColumn("CUSTOMERNAME", 100).SetLabel("COMPANYCLIENT");
			options.GridColumns.AddTextBoxColumn("SPECOWNERNAME", 80).SetTextAlignment(TextAlignment.Center).SetLabel("SPECOWNER");
			options.GridColumns.AddTextBoxColumn("CUSTOMERID", 100).SetIsHidden();
			options.GridColumns.AddTextBoxColumn("SPECOWNERID", 100).SetIsHidden();

			popPrevProduct.SelectPopupCondition = options;
			popPrevProduct.SetValue(null);
			popPrevProduct.Text = string.Empty;
			popPrevProduct.Tag = "PRODUCTDEFID";
			popPrevProduct.ReadOnly = isReadOnly;
		}

		/// <summary>
		/// 품목(변경) 팝업 초기화
		/// </summary>
		private void InitializePopup_Product(string isOurCompany = "N")
		{
			if (popTransProduct.SelectPopupCondition != null) popTransProduct.SelectPopupCondition = null;

			ConditionItemSelectPopup options = new ConditionItemSelectPopup();
			options.SetPopupLayoutForm(800, 800, FormBorderStyle.FixedToolWindow);
			options.SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false);
			options.Id = "ITEMID";
            options.SearchQuery = new SqlQuery("GetItemMasterList", "10002", $"ENTERPRISEID={UserInfo.Current.Enterprise}", "PRODUCTDIVISION=Product");
			options.DisplayFieldName = "ITEMID";
			options.ValueFieldName = "ITEMID";
			options.LanguageKey = "ITEMID";
			options.SetPopupResultCount(1);
			options.SetPopupApplySelection((selectRow, gridRow) =>
			{
				DataRow row = selectRow.FirstOrDefault();
				if (row == null) return;

				popTransProduct.EditValue = row["ITEMID"].ToString();
				txtTransProductRev.Text = row["ITEMVERSION"].ToString();
				txtTransProductName.Text = row["ITEMNAME"].ToString();
				cboTransCustomer.EditValue = row["CUSTOMERID"];
				cboTransSpecOwner.EditValue = row["SPECOWNERID"];
			});
			options.SetPopupAutoFillColumns("ITEMNAME");

			if (isOurCompany.Equals("Y"))
			{
				options.Conditions.AddTextBox("ITEMID").SetDefault(popPrevProduct.EditValue);
			}
			else
			{
				options.Conditions.AddTextBox("ITEMID");
			}

			options.Conditions.AddTextBox("ITEMVERSION");
			options.Conditions.AddTextBox("ITEMNAME");

			options.GridColumns.AddTextBoxColumn("ITEMID", 100);
			options.GridColumns.AddTextBoxColumn("ITEMVERSION", 60);
			options.GridColumns.AddTextBoxColumn("ITEMNAME", 200);
			options.GridColumns.AddTextBoxColumn("CUSTOMERNAME", 100).SetLabel("COMPANYCLIENT");
			options.GridColumns.AddTextBoxColumn("SPECOWNERNAME", 80).SetTextAlignment(TextAlignment.Center).SetLabel("SPECOWNER");
			options.GridColumns.AddTextBoxColumn("CUSTOMERID", 100).SetIsHidden();
			options.GridColumns.AddTextBoxColumn("SPECOWNERID", 100).SetIsHidden();

			popTransProduct.SelectPopupCondition = options;
			popTransProduct.EditValue = null;
			popTransProduct.Text = string.Empty;
			txtTransProductRev.EditValue = null;
			txtTransProductRev.Text = string.Empty;
			txtTransProductName.EditValue = null;
			txtToProductName.Text = string.Empty;
			popTransProduct.Tag = "RCPRODUCTDEFID";
		}

		/// <summary>
		/// 요청자 팝업 초기화
		/// </summary>
		/// <param name="isReadOnly"></param>
		private void InitializationPopup_Requestor()
		{
			/*
			if (popRequestor.SelectPopupCondition != null) popRequestor.SelectPopupCondition = null;

			ConditionItemSelectPopup options2 = new ConditionItemSelectPopup();
			options2.SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow);
			options2.SetPopupLayout("USERLIST", PopupButtonStyles.Ok_Cancel, true, false);
			options2.Id = "USER";
			options2.SearchQuery = new SqlQuery("GetUserList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}");
			options2.DisplayFieldName = "USERNAME";
			options2.ValueFieldName = "USERID";
			options2.LanguageKey = "USER";
			options2.SetPopupResultCount(1);
			options2.SetPopupAutoFillColumns("USERNAME");

			options2.Conditions.AddTextBox("USERIDNAME");

			options2.GridColumns.AddTextBoxColumn("USERID", 150);
			options2.GridColumns.AddTextBoxColumn("USERNAME", 200);

			popRequestor.SelectPopupCondition = options2;
			popRequestor.EditValue = null;
			popRequestor.Text = string.Empty;
			popRequestor.Tag = "REQUESTORID";
			popRequestor.Enabled = false;
			*/
		}

		/// <summary>
		/// 고객사 팝업 초기화
		/// </summary>
		/// <param name="combo"></param>
		/// <param name="isChange"></param>
		private void InitializePopup_Customer(SmartComboBox combo, bool isChange)
		{
			combo.ValueMember = "CUSTOMERID";
			combo.DisplayMember = "CUSTOMERNAME";
			combo.Tag = (isChange == false) ? "EXISTCUSTOMER" : "TRANSCUSTOMER";
			combo.DataSource = SqlExecuter.Query("GetCustomerList", "10001", new Dictionary<string, object>(){ { "ENTERPRISEID", UserInfo.Current.Enterprise },
																											   { "PLANTID", UserInfo.Current.Plant } });
			combo.Enabled = false;
			combo.EditValue = null;
			combo.Text = string.Empty;

			/*
			if (popPrevCustomer.SelectPopupCondition != null) popPrevCustomer.SelectPopupCondition = null;

			ConditionItemSelectPopup options = new ConditionItemSelectPopup();
			options.SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow);
			options.SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false);
			options.Id = "CUSTOMERID";
			options.SearchQuery = new SqlQuery("GetCustomerList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}");
			options.DisplayFieldName = "CUSTOMERNAME";
			options.ValueFieldName = "CUSTOMERID";
			options.LanguageKey = "COMPANYCLIENT";
			options.SetPopupResultCount(1);
			options.SetPopupAutoFillColumns("CUSTOMERNAME");

			options.Conditions.AddTextBox("TXTCUSTOMERID");

			options.GridColumns.AddTextBoxColumn("CUSTOMERID", 100);
			options.GridColumns.AddTextBoxColumn("CUSTOMERNAME", 200);

			popPrevCustomer.SelectPopupCondition = options;
			popPrevCustomer.SetValue(null);
			popPrevCustomer.Text = string.Empty;
			popPrevCustomer.Tag = (isChange == false) ? "CUSTOMERID" : "CHANGECUSTOMERID";
			popPrevCustomer.ReadOnly = isReadOnly;
			*/
		}

		/// <summary>
		/// 사양담당 팝업 초기화
		/// </summary>
		/// <param name="combo"></param>
		/// <param name="isChange"></param>
		private void InitializationPopup_SpecOwner(SmartComboBox combo, bool isChange)
		{
			combo.ValueMember = "USERID";
			combo.DisplayMember = "USERNAME";
			combo.Tag = (isChange == false) ? "EXISTSPECOWNER" : "TRANSSPECOWNER";
			combo.DataSource = SqlExecuter.Query("SelectUserGroupUserSearch", "10001", new Dictionary<string, object>() { { "P_USERGROUPID", "SPECOWNER" } });
			combo.Enabled = false;

			combo.EditValue = null;
			combo.Text = string.Empty;

			/*
			if (popPrevSpecOwner.SelectPopupCondition != null) popPrevSpecOwner.SelectPopupCondition = null;

			ConditionItemSelectPopup options2 = new ConditionItemSelectPopup();
			options2.SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow);
			options2.SetPopupLayout("USERLIST", PopupButtonStyles.Ok_Cancel, true, false);
			options2.Id = "USER";
			options2.SearchQuery = new SqlQuery("SelectUserGroupUserSearch", "10001", $"P_USERGROUPID={"SPECOWNER"}");
			options2.DisplayFieldName = "USERNAME";
			options2.ValueFieldName = "USERID";
			options2.LanguageKey = "USER";
			options2.SetPopupResultCount(1);
			options2.SetPopupAutoFillColumns("USERNAME");

			options2.Conditions.AddTextBox("USERIDNAME");

			options2.GridColumns.AddTextBoxColumn("USERID", 150);
			options2.GridColumns.AddTextBoxColumn("USERNAME", 200);

			popPrevSpecOwner.SelectPopupCondition = options2;
			popPrevSpecOwner.EditValue = null;
			popPrevSpecOwner.Tag = (isChange == false) ? "SPECOWNERID" : "CHANGESPECOWNERID";
			popPrevSpecOwner.ReadOnly = isReadOnly;
			*/
		}

		#endregion

		#region 조회조건 초기화

		/// <summary>
		/// 조회 조건 초기화
		/// </summary>
		protected override void InitializeCondition()
		{
			base.InitializeCondition();

			//관리번호
			InitializeCondition_Popup();
			//고객사
			InitializeCondition_CustomerPopup();
			//제품범주
			//InitializeGrid_InventoryCgPopup();
			//품목코드
			InitializeCondition_ProductPopup();
			//사양담당
			InitializeCondition_Customer();
		}

		/// <summary>
		/// 조회조건의 컨트롤을 추가한다.
		/// </summary>
		protected override void InitializeConditionControls()
		{
			base.InitializeConditionControls();

			//다국어 변경
			SmartSelectPopupEdit specOwner = Conditions.GetControl<SmartSelectPopupEdit>("P_SPECOWNER");
			specOwner.LanguageKey = "SPECOWNER";
			specOwner.LabelText = Language.Get(specOwner.LanguageKey);

			SmartSelectPopupEdit customer = Conditions.GetControl<SmartSelectPopupEdit>("P_CUSTOMER");
			customer.LanguageKey = "COMPANYCLIENT";
			customer.LabelText = Language.Get(customer.LanguageKey);

			/*
			SmartSelectPopupEdit category = Conditions.GetControl<SmartSelectPopupEdit>("P_INVENTORYCATEGORY");
			category.LanguageKey = "PRODUCTTYPE";
			category.LabelText = Language.Get(category.LanguageKey);
			*/

			//3개월 설정
			SmartPeriodEdit period = Conditions.GetControl<SmartPeriodEdit>("P_PERIOD");
			DateTime toDate = Convert.ToDateTime(period.datePeriodTo.EditValue);
			period.datePeriodFr.EditValue = toDate.AddMonths(-3);


			//검색조건 숨기기
			SetConditionVisiblility("P_PLANTID", DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
			SetConditionVisiblility("P_PERIOD", DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
			SetConditionVisiblility("P_CUSTOMER", DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
			//SetConditionVisiblility("P_INVENTORYCATEGORY", DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
			SetConditionVisiblility("P_PRODUCTIONTYPE", DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
			SetConditionVisiblility("P_ITEMID", DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
			SetConditionVisiblility("P_PRODUCTNAME", DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
			SetConditionVisiblility("P_SPECOWNER", DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
			SetConditionVisiblility("P_GOVERNANCENO", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
			Conditions.GetCondition("P_GOVERNANCENO").IsRequired = false;
			SetConditionVisiblility("P_RCSTATE", DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
		}

		/// <summary>
		/// 관리번호 - 팝업형 조회조건 생성
		/// </summary>
		private void InitializeCondition_Popup()
		{
			var parentPopupColumn = Conditions.AddSelectPopup("P_GOVERNANCENO", new SqlQuery("GetRunningChangeList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "GOVERNANCENO", "GOVERNANCENO")
			   .SetPopupLayout("GOVERNANCENO", PopupButtonStyles.Ok_Cancel, true, false)
			   .SetPopupResultCount(1)
			   .SetPosition(0.1)
			   .SetLabel("MANAGENO")
			   .SetValidationIsRequired()
			   .SetPopupApplySelection((selectRow, gridRow) => {
				   DataRow row = selectRow.FirstOrDefault();
				   if (row == null)
				   {
					   InitPrivateValues();
					   return;
				   }

				   _productDefId = row["PRODUCTDEFID"].ToString();
				   _productDefVer = row["PRODUCTDEFVERSION"].ToString();
				   _productName = row["ITEMNAME"].ToString();
				   _rcProductDefId = row["RCPRODUCTDEFID"].ToString();
				   _rcProductDefVer = row["RCPRODUCTDEFVERSION"].ToString();
				   _rcProductName = row["RCPRODUCTDEFNAME"].ToString();

				   _governanceNo = row["GOVERNANCENO"].ToString();
				   _governanceType = row["GOVERNANCETYPE"].ToString();
				   _pcnNo = row["CHANGEPOINTNO"].ToString();
				   _state = Format.GetString(row["RCSTATEID"]);
			   });

			parentPopupColumn.Conditions.AddTextBox("GOVERNANCENO");
			parentPopupColumn.Conditions.AddTextBox("PRODUCTDEFID");
			parentPopupColumn.Conditions.AddTextBox("PRODUCTDEFVERSION");
			parentPopupColumn.Conditions.AddComboBox("STATUS", new SqlQuery("GetTypeList", "10001", "CODECLASSID=RCState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
				.SetEmptyItem();

			parentPopupColumn.GridColumns.AddTextBoxColumn("GOVERNANCENO", 130).SetTextAlignment(TextAlignment.Center);
			parentPopupColumn.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 100);
			parentPopupColumn.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 60);
			parentPopupColumn.GridColumns.AddTextBoxColumn("ITEMNAME", 200);
			parentPopupColumn.GridColumns.AddTextBoxColumn("RCPRODUCTDEFID", 100);
			parentPopupColumn.GridColumns.AddTextBoxColumn("RCPRODUCTDEFVERSION", 60);
			parentPopupColumn.GridColumns.AddTextBoxColumn("RCPRODUCTDEFNAME", 200);
			parentPopupColumn.GridColumns.AddComboBoxColumn("STATUS", 60, new SqlQuery("GetTypeList", "10001", "CODECLASSID=RCState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetTextAlignment(TextAlignment.Center);
			parentPopupColumn.GridColumns.AddTextBoxColumn("SPECOWNER", 80).SetTextAlignment(TextAlignment.Center);
			parentPopupColumn.GridColumns.AddTextBoxColumn("SPECOWNERNAME", 80).SetTextAlignment(TextAlignment.Center);
		}

		/// <summary>
		/// 팝업형 조회조건 생성 - 고객사
		/// </summary>
		private void InitializeCondition_CustomerPopup()
		{
			var conditionCustomer = Conditions.AddSelectPopup("P_CUSTOMER", new SqlQuery("GetCustomerList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "CUSTOMERNAME", "CUSTOMERID")
				.SetPopupLayout("SELECTCUSTOMERID", PopupButtonStyles.Ok_Cancel, true, false)
				.SetPopupLayoutForm(800, 800)
				.SetPopupAutoFillColumns("CUSTOMERNAME")
				.SetLabel("CUSTOMERID")
				.SetPosition(2.5)
				.SetPopupResultCount(0);

			// 팝업 조회조건
			conditionCustomer.Conditions.AddTextBox("TXTCUSTOMERID");

			// 팝업 그리드
			conditionCustomer.GridColumns.AddTextBoxColumn("CUSTOMERID", 80);
			conditionCustomer.GridColumns.AddTextBoxColumn("CUSTOMERNAME", 150);
		}

		/// <summary>
		/// 팝업형 조회조건 생성 - 제품범주
		/// </summary>
		private void InitializeGrid_InventoryCgPopup()
		{
			var parentPopupColumn = Conditions.AddSelectPopup("P_INVENTORYCATEGORY", new SqlQuery("GetCategoryPopup", "10021", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
				.SetPopupLayout("INVENTORYCATEGORY", PopupButtonStyles.Ok_Cancel, true, false)
				.SetPopupResultCount(1)
				.SetPopupResultMapping("INVENTORYCATEGORY", "CATEGORYID")
				.SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow)
				.SetPosition(2.6)
				.SetLabel("INVENTORYCATEGORY");

			parentPopupColumn.Conditions.AddComboBox("PARENTCATEGORYID", new SqlQuery("GetCategoryMidList", "10001", "TOPPARENTCATEGORYID=Inventory"), "CATEGORYNAME", "CATEGORYID").SetValidationIsRequired();
			parentPopupColumn.Conditions.AddTextBox("CATEGORYNAME");

			// 팝업 그리드 설정
			parentPopupColumn.GridColumns.AddTextBoxColumn("CATEGORYID", 150);
			parentPopupColumn.GridColumns.AddTextBoxColumn("CATEGORYNAME", 200);

		}

		/// <summary>
		/// 팝업형 조회조건 생성 - 품목
		/// </summary>
		private void InitializeCondition_ProductPopup()
		{
			// SelectPopup 항목 추가
			var conditionProductId = Conditions.AddSelectPopup("P_ITEMID", new SqlQuery("GetItemMasterList", "10002", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "ITEMVALUE", "ITEMVALUE")
				.SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)
				.SetPopupLayoutForm(800, 800)
				.SetPopupAutoFillColumns("ITEMNAME")
				.SetLabel("ITEMID")
				.SetPosition(3.2)
				.SetPopupResultCount(0)
				.SetPopupApplySelection((selectRow, gridRow) =>
				{
					string productDefName = "";

					selectRow.AsEnumerable().ForEach(r =>
					{
						productDefName += Format.GetString(r["ITEMNAME"]) + ",";
					});

					Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").EditValue = productDefName.TrimEnd(',');
				});

			// 팝업에서 사용되는 검색조건 (품목코드/명)
			conditionProductId.Conditions.AddTextBox("TXTITEM").SetLabel("PRODUCT");
			conditionProductId.Conditions.AddComboBox("PRODUCTDIVISION", new SqlQuery("GetTypeList", "10001", "CODECLASSID=ProductDivision2", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetDefault("Product");


			// 품목코드
			conditionProductId.GridColumns.AddTextBoxColumn("ITEMID", 150);
			// 품목명
			conditionProductId.GridColumns.AddTextBoxColumn("ITEMNAME", 200);
			// 품목버전
			conditionProductId.GridColumns.AddTextBoxColumn("ITEMVERSION", 100);
		}

		/// <summary>
		/// 팝업형 조회조건 생성 - 사양담당
		/// </summary>
		private void InitializeCondition_Customer()
		{
			var conditionCustomer = Conditions.AddSelectPopup("P_SPECOWNER", new SqlQuery("GetUserList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "USERNAME", "USERID")
				.SetPopupLayout("SELECTSPECOWNER", PopupButtonStyles.Ok_Cancel, true, false)
				.SetPopupLayoutForm(800, 800)
				.SetPopupAutoFillColumns("DEPARTMENT")
				.SetLabel("USERID")
				.SetPosition(4.5)
				.SetPopupResultCount(0);

			// 팝업 조회조건
			conditionCustomer.Conditions.AddTextBox("USERIDNAME");

			// 팝업 그리드
			conditionCustomer.GridColumns.AddTextBoxColumn("USERID", 150);
			conditionCustomer.GridColumns.AddTextBoxColumn("USERNAME", 200);
			conditionCustomer.GridColumns.AddTextBoxColumn("DEPARTMENT", 200);
		}
		#endregion

		#region 이벤트

		/// <summary>
		/// 이벤트
		/// </summary>
		private void InitializeEvent()
		{
			#region 첫번째탭 이벤트
			tabGovernanceChange.SelectedPageChanged += TabGovernanceChange_SelectedPageChanged;
			tabGovernanceChange.SelectedPageChanging += TabGovernanceChange_SelectedPageChanging;
			popTransProduct.EditValueChanged += PopTransProduct_EditValueChanged;
			grdChangePointList.View.FocusedRowChanged += View_FocusedRowChanged;
			grdChangePointList.View.DoubleClick += View_DoubleClick;
			chkCustomerType.CheckedChanged += ChkCustomerType_CheckedChanged;
			chkOurCompanyType.CheckedChanged += ChkOurCompanyType_CheckedChanged;
			btnAddFile.Click += BtnAddFile_Click;
			btnDeleteFile.Click += BtnDeleteFile_Click;
			btn_down.Click += Btn_down_Click;
			popPrevProduct.ButtonClick += PopPrevProduct_ButtonClick;
			popTransProduct.ButtonClick += PopTransProduct_ButtonClick;
			grdChangePointList.InitContextMenuEvent += GrdChangePointList_InitContextMenuEvent;
			grdFileList.View.RowStyle += View_RowStyle2;
			#endregion

			#region 두번째탭 이벤트
			btnEventToggle.CheckedChanged += BtnEventToggle_CheckedChanged;
			grdFromRoutingList.View.FocusedRowChanged += View_FocusedRowChanged1;
			grdToRoutingList.View.FocusedRowChanged += View_FocusedRowChanged2;
			grdFromRoutingList.View.RowStyle += View_RowStyle;
			grdToRoutingList.View.RowStyle += View_RowStyle1;
			tabFromInfo.SelectedPageChanged += TabFromInfo_SelectedPageChanged1;
			tabToInfo.SelectedPageChanged += TabToInfo_SelectedPageChanged1;

			//스크롤 이벤트
			grdFromRoutingList.View.TopRowChanged += View_TopRowChanged;
			grdToRoutingList.View.TopRowChanged += View_TopRowChanged1;
			grdFromRoutingList.View.LeftCoordChanged += View_LeftCoordChanged;
			grdToRoutingList.View.LeftCoordChanged += View_LeftCoordChanged1;
			grdFromSpecList.View.LeftCoordChanged += View_LeftCoordChanged2;
			grdToSpecList.View.LeftCoordChanged += View_LeftCoordChanged3;
			//탭 이벤트
			tabFromInfo.SelectedPageChanged += TabFromInfo_SelectedPageChanged;
			tabToInfo.SelectedPageChanged += TabToInfo_SelectedPageChanged;
			#endregion
		}

		/// <summary>
		/// Row Style 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void View_RowStyle2(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
		{
			if(Format.GetString(grdFileList.View.GetRowCellValue(e.RowHandle, "_STATE_")).Equals("deleted"))
			{
				e.Appearance.BackColor = Color.FromArgb(208, 252, 219);
				e.HighPriority = true;
			}

			if (Format.GetString(grdFileList.View.GetRowCellValue(e.RowHandle, "_STATE_")).Equals("added"))
			{
				e.Appearance.BackColor = Color.FromArgb(207, 218, 250);
				e.HighPriority = true;
			}

		}

		/// <summary>
		/// 파일 삭제
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnDeleteFile_Click(object sender, EventArgs e)
		{
			List<DataRowView> deleteRow = new List<DataRowView>();

			DataTable dt = grdFileList.DataSource as DataTable;
			DataTable chkDt = grdFileList.View.GetCheckedRows();
			for(int i = 0; i < dt.Rows.Count; i++)
			{
				for(int j = 0; j < chkDt.Rows.Count; j++)
				{
					if(chkDt.Rows[j]["SEQUENCE"] == dt.Rows[i]["SEQUENCE"])
					{
						if(string.IsNullOrWhiteSpace(Format.GetString(chkDt.Rows[j]["FILEID"])))
						{
							DataRowView row = grdFileList.View.GetRow(i) as DataRowView;
							deleteRow.Add(row);
						}
						else
						{
							dt.Rows[i]["_STATE_"] = "deleted";
						}						
					}
				}
			}

			foreach(DataRowView r in deleteRow)
			{
				r.Delete();
			}

			dt.AcceptChanges();
		}

		/// <summary>
		/// Customizing Context Menu Item 초기화
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		private void GrdChangePointList_InitContextMenuEvent(SmartBandedGrid sender, Framework.SmartControls.Grid.InitContextMenuEventArgs args)
		{
			for (int i = 0; i < menuList.Count; i++)
			{
				args.AddMenus.Add(menuList[i]);
			}
		}

		/// <summary>
		/// 품목(변경) x 버튼 눌렀을 때 초기화
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PopTransProduct_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			if (e.Button.Kind.Equals(DevExpress.XtraEditors.Controls.ButtonPredefines.Clear))
			{
				txtTransProductRev.EditValue = null;
				txtTransProductRev.Text = string.Empty;
				txtTransProductName.EditValue = null;
				txtTransProductName.Text = string.Empty;
			}
		}

		/// <summary>
		/// 품목(기존) x 버튼 눌렀을 때 초기화
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PopPrevProduct_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			if (e.Button.Kind.Equals(DevExpress.XtraEditors.Controls.ButtonPredefines.Clear))
			{
				txtPrevProductRev.EditValue = null;
				txtPrevProductRev.Text = string.Empty;
				txtPrevProductName.EditValue = null;
				txtPrevProductName.Text = string.Empty;
			}
		}


		/// <summary>
		/// 다운로드 버튼 클릭 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Btn_down_Click(object sender, EventArgs e)
		{
			FileDownload();
		}

		/// <summary>
		/// 파일 추가 버튼 클릭 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnAddFile_Click(object sender, EventArgs e)
		{
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "All Files (*.*)|*.*";
            openFileDialog.FilterIndex = 0;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string[] fullFileName = openFileDialog.FileNames;
                string[] safeFileName = openFileDialog.SafeFileNames;

                DataTable dataSource = grdFileList.DataSource as DataTable;

                int rowCount = grdFileList.View.RowCount + 1;

                foreach (string fileName in fullFileName)
                {
                    FileInfo fileInfo = new FileInfo(fileName);


                    DataRow[] rows = dataSource.Select("SAFEFILENAME = '" + fileInfo.Name + "'");
                    string addedFileName = "";

                    if (rows.Count() > 0)
                        addedFileName = "(" + rows.Count().ToString() + ")";

                    DataRow newRow = dataSource.NewRow();

                    newRow["FILEID"] = "";
                    newRow["FILENAME"] = fileInfo.Name.Substring(0, fileInfo.Name.Length - fileInfo.Extension.Length) + addedFileName;
                    newRow["FILEEXT"] = fileInfo.Extension.Replace(".", "");
                    newRow["FILEPATH"] = _UploadPath;
                    newRow["SAFEFILENAME"] = fileInfo.Name.Substring(0, fileInfo.Name.Length - fileInfo.Extension.Length) + fileInfo.Extension;//fileInfo.Name.Substring(0, fileInfo.Name.Length - fileInfo.Extension.Length) + addedFileName + fileInfo.Extension;
					newRow["FILESIZE"] = fileInfo.Length;
                    newRow["SEQUENCE"] = rowCount;
                    newRow["LOCALFILEPATH"] = fileInfo.FullName;
                    newRow["RESOURCETYPE"] = Resource.Type;
                    newRow["RESOURCEID"] = Resource.Id;
                    newRow["RESOURCEVERSION"] = Resource.Version;
					newRow["_STATE_"] = "added";

                    dataSource.Rows.Add(newRow);

                    rowCount++;
                }

				dataSource.AcceptChanges();

				//2020-02-18 강유라  파일 추가 또는 삭제 시 delete상태가 아닌 row 갯수를 할당 해준다
				//if (countRows)
				//{
				//    rowCount = (grdFileList.DataSource as DataTable).Rows.Count - grdFileList.View.GetDeletedRows().Count();
				//    FileRowCountChangedEvent(rowCount);
				//}
			}
        }

	

		/// <summary>
		/// 파일 업로드
		/// </summary>
		/// <param name="dtFile2"></param>
		private void FileUploadToServer(DataTable dtFile2)
		{
			string ftpServerPath = Cryptography.AESDecrypt(AppConfiguration.GetString("Application.Ftp.Url"));
			string ftpServerUserId = Cryptography.AESDecrypt(AppConfiguration.GetString("Application.Ftp.Id"));
			string ftpServerPassword = Cryptography.AESDecrypt(AppConfiguration.GetString("Application.Ftp.Password"));

			int totalFileSize = 0;

			foreach (DataRow row in dtFile2.Rows)
			{
				//row["PROCESSINGSTATUS"] = "Uploading";
				totalFileSize += Format.GetInteger(row["FILESIZE"]);

				if (row["_STATE_"].Equals("added"))
				{
					try
					{
						string fileName = Format.GetString(row["LOCALFILEPATH"]);
						string safeFileName = Format.GetString(row["FILENAME"]) + "." + Format.GetString(row["FILEEXT"]);
						string uploadPath = Format.GetString(row["FILEPATH"]);

						if (!File.Exists(fileName))
							throw new FileNotFoundException();

						CreateFtpServerDirectory(ftpServerPath, uploadPath, ftpServerUserId, ftpServerPassword);

						FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(ftpServerPath + string.Join("/", uploadPath, safeFileName));
						ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;
						ftpRequest.UseBinary = true;
						ftpRequest.UsePassive = true;
						ftpRequest.Credentials = new NetworkCredential(ftpServerUserId, ftpServerPassword);

						byte[] byteFile;

						//using (StreamReader reader = new StreamReader(fileName))
						//{
						//    byteFile = Encoding.UTF8.GetBytes(reader.ReadToEnd());
						//}

						using (FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
						{
							byteFile = new byte[fileStream.Length];
							fileStream.Read(byteFile, 0, byteFile.Length);
						}

						ftpRequest.ContentLength = byteFile.Length;
						using (Stream stream = ftpRequest.GetRequestStream())
						{
							stream.Write(byteFile, 0, byteFile.Length);
						}

					}
					catch(Exception ex)
					{
						//row["PROCESSINGSTATUS"] = "Error";
						throw MessageException.Create(ex.Message);
					}
				}//if
			}//foreach
		}

		/// <summary>
		/// FTP 서버에 디렉토리 생성
		/// </summary>
		/// <param name="url"></param>
		/// <param name="path"></param>
		/// <param name="id"></param>
		/// <param name="pwd"></param>
		private void CreateFtpServerDirectory(string url, string path, string id, string pwd)
		{
			FtpWebRequest ftpRequest = null;
			Stream ftpStream = null;

			string[] subDirs = path.Split('/');

			string currentDir = url;

			foreach (string subDir in subDirs)
			{
				try
				{
					currentDir = string.Join("/", currentDir.TrimEnd('/'), subDir);
					ftpRequest = (FtpWebRequest)FtpWebRequest.Create(currentDir);
					ftpRequest.Method = WebRequestMethods.Ftp.MakeDirectory;
					ftpRequest.Credentials = new NetworkCredential(id, pwd);
					ftpRequest.UseBinary = true;
					ftpRequest.UsePassive = true;
					ftpRequest.Proxy = null;

					FtpWebResponse ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
					ftpStream = ftpResponse.GetResponseStream();
					ftpStream.Close();
					ftpResponse.Close();
				}
				catch (WebException ex)
				{
					FtpWebResponse ftpResponse = (FtpWebResponse)ex.Response;
					ftpResponse.Close();
				}
			}
		}

		/// <summary>
		/// 파일 다운로드
		/// </summary>
		private void FileDownload()
        {
			DataTable chkDt = grdFileList.View.GetCheckedRows();
			if(chkDt.Rows.Count < 1) return;

			List<DataRow> aR = chkDt.AsEnumerable().Where(r => Format.GetString(r["_STATE_"]).Equals("added")).Distinct().ToList();
			if(aR.Count > 0)
			{
				//추가된 파일이 있습니다. 저장 후 진행해 주십시오.
				ShowMessage("AFTERSAVEANDDOWNLOAD");
				return;
			}

			FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
			folderBrowserDialog.SelectedPath = Application.StartupPath;
			
			if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				foreach (DataRow row in chkDt.Rows)
				{
					if(!Format.GetString(row["_STATE_"]).Equals("added"))
					{
						try
						{
							string ftpServerPath = Cryptography.AESDecrypt(AppConfiguration.GetString("Application.Ftp.Url"));
							string ftpServerUserId = Cryptography.AESDecrypt(AppConfiguration.GetString("Application.Ftp.Id"));
							string ftpServerPassword = Cryptography.AESDecrypt(AppConfiguration.GetString("Application.Ftp.Password"));

							using (WebClient client = new WebClient())
							{
								string serverPath = ftpServerPath + _UploadPath;
								serverPath = serverPath + ((serverPath.EndsWith("/")) ? "" : "/") + Format.GetString(row["FILENAME"]) + "." + Format.GetString(row["FILEEXT"]);

								client.Credentials = new NetworkCredential(ftpServerUserId, ftpServerPassword);

								client.DownloadFile(serverPath, string.Join("\\", folderBrowserDialog.SelectedPath, Format.GetString(row["FILENAME"]) + "." + Format.GetString(row["FILEEXT"])));

							}

						}
						catch (Exception ex)
						{
							throw MessageException.Create(ex.Message);
						}
					}
					
				}
				

				ShowMessage("SuccessDownload");
				OnSearchAsync();
			}

        }
        /// <summary>
        /// 이벤트 동시/개별 설정 및 해제
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEventToggle_CheckedChanged(object sender, EventArgs e)
		{
			if (btnEventToggle.Checked)
			{
				btnEventToggle.Text = Language.Get("SAMETIME");

				//스크롤 이벤트
				grdFromRoutingList.View.TopRowChanged -= View_TopRowChanged;
				grdToRoutingList.View.TopRowChanged -= View_TopRowChanged1;
				grdFromRoutingList.View.LeftCoordChanged -= View_LeftCoordChanged;
				grdToRoutingList.View.LeftCoordChanged -= View_LeftCoordChanged1;
				grdFromSpecList.View.LeftCoordChanged -= View_LeftCoordChanged2;
				grdToSpecList.View.LeftCoordChanged -= View_LeftCoordChanged3;
				//탭 이벤트
				tabFromInfo.SelectedPageChanged -= TabFromInfo_SelectedPageChanged;
				tabToInfo.SelectedPageChanged -= TabToInfo_SelectedPageChanged;
			}
			else
			{
				btnEventToggle.Text = Language.Get("EACHTIME");

				//스크롤 이벤트
				grdFromRoutingList.View.TopRowChanged += View_TopRowChanged;
				grdToRoutingList.View.TopRowChanged += View_TopRowChanged1;
				grdFromRoutingList.View.LeftCoordChanged += View_LeftCoordChanged;
				grdToRoutingList.View.LeftCoordChanged += View_LeftCoordChanged1;
				grdFromSpecList.View.LeftCoordChanged += View_LeftCoordChanged2;
				grdToSpecList.View.LeftCoordChanged += View_LeftCoordChanged3;
				//탭 이벤트
				tabFromInfo.SelectedPageChanged += TabFromInfo_SelectedPageChanged;
				tabToInfo.SelectedPageChanged += TabToInfo_SelectedPageChanged;
			}
		}

		private void TabToInfo_SelectedPageChanged1(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
		{
			DataRow toRow = grdToRoutingList.View.GetFocusedDataRow();
			if (toRow == null) return;
			TabToInfoDataBind(toRow);
		}

		/// <summary>
		/// FROM 탭 이동 시 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TabFromInfo_SelectedPageChanged1(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
		{
			DataRow frRow = grdFromRoutingList.View.GetFocusedDataRow();
			if (frRow == null) return;
			TabFromInfoDataBind(frRow);
		}

		/// <summary>
		/// 수정중 이동 시 체크
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TabGovernanceChange_SelectedPageChanging(object sender, DevExpress.XtraTab.TabPageChangingEventArgs e)
		{
			if (e.Page.Name.Equals("routingComparePage"))
			{
				DataRow selectRow = grdChangePointList.View.GetFocusedDataRow();
				if (selectRow == null) return;

				DataTable compareDt = new DataTable();
				compareDt = GetRowDataByDataTable(compareDt, selectRow);
				DialogResult result = DialogResult.None;
				bool isChanged = CheckDifferentInputData(_inputDt, compareDt, out result);

				if (!isChanged && result == DialogResult.Cancel)
				{
					e.Cancel = true;
				}
			}


		}


		/// <summary>
		/// 변경점 더블클릭 시 라우팅 비교
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void View_DoubleClick(object sender, EventArgs e)
		{
			if (grdChangePointList.View.FocusedRowHandle < 0) return;

			DataRow doubleClickRow = grdChangePointList.View.GetFocusedDataRow();
			if (doubleClickRow == null) return;

			if (string.IsNullOrEmpty(Format.GetString(doubleClickRow["RCSTATE"]))
			|| string.IsNullOrEmpty(Format.GetString(doubleClickRow["GOVERNANCENO"]))) return;

			//라우팅 비교 탭 이동
			tabGovernanceChange.SelectedTabPageIndex = 1;
			_governanceNo = Format.GetString(doubleClickRow["GOVERNANCENO"]);
			_governanceType = Format.GetString(doubleClickRow["GOVERNANCETYPE"]);
			_productDefId = Format.GetString(doubleClickRow["PRODUCTDEFID"]);
			_productName = Format.GetString(doubleClickRow["ITEMNAME"]);
			_productDefVer = Format.GetString(doubleClickRow["PRODUCTDEFVERSION"]);
			_rcProductDefId = Format.GetString(doubleClickRow["RCPRODUCTDEFID"]);
			_rcProductName = Format.GetString(doubleClickRow["RCITEMNAME"]);
			_rcProductDefVer = Format.GetString(doubleClickRow["RCPRODUCTDEFVERSION"]);
			_pcnNo = Format.GetString(doubleClickRow["CHANGEPOINTNO"]);
			_state = Format.GetString(doubleClickRow["RCSTATEID"]);

			//TabGovernanceChange_SelectedPageChanged(null, null);
			//SetConditionVisiblility("P_PLANTID", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
			//SetConditionVisiblility("P_PERIOD", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
			//SetConditionVisiblility("P_CUSTOMER", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
			//SetConditionVisiblility("P_INVENTORYCATEGORY", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
			//SetConditionVisiblility("P_PRODUCTIONTYPE", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
			//SetConditionVisiblility("P_ITEMID", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
			//SetConditionVisiblility("P_PRODUCTNAME", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
			//SetConditionVisiblility("P_SPECOWNER", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
			//SetConditionVisiblility("P_GOVERNANCENO", DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
			//Conditions.GetCondition("P_GOVERNANCENO").IsRequired = true;
			//SetConditionVisiblility("P_RCSTATE", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);

			//SmartSelectPopupEdit popGovernanceNo = Conditions.GetControl<SmartSelectPopupEdit>("P_GOVERNANCENO");
			//popGovernanceNo.SetValue(_governanceNo);

			//var buttons = pnlToolbar.Controls.Find<SmartButton>(true);
			//foreach (SmartButton button in buttons)
			//{
			//    if (button.Name == "Confirmation" || button.Name == "Compare")
			//        button.Visible = true;
			//    else
			//        button.Visible = false;
			//}

			OnSearchAsync();
		}

		/// <summary>
		/// 사내변경 Check State 변경 시 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ChkOurCompanyType_CheckedChanged(object sender, EventArgs e)
		{
			if (popPrevProduct.Enabled)
			{
				if (chkOurCompanyType.Checked)
				{
					InitializePopup_Product("Y");
					_isNotSelected = true;
				}
				else
				{
					InitializePopup_Product();
					_isNotSelected = false;
				}

				popTransProduct.SetValue(null);
				txtTransProductRev.EditValue = null;
				txtTransProductName.EditValue = null;
			}
		}

		/// <summary> 
		/// 고객변경 Check State 변경 시 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ChkCustomerType_CheckedChanged(object sender, EventArgs e)
		{
			if (chkCustomerType.Checked && popPrevProduct.Enabled)
			{
				popTransProduct.SetValue(null);
				txtTransProductRev.EditValue = null;
				txtTransProductName.EditValue = null;
			}
		}


		/// <summary>
		/// 라우팅 비교 시 색상 표기 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void View_RowStyle1(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
		{
			SetRowStyle(grdToRoutingList, e);
		}

		/// <summary>
		/// 라우팅 비교 시 색상 표기 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void View_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
		{
			SetRowStyle(grdFromRoutingList, e);
		}

		/// <summary>
		/// ROW 스타일 색상 표기
		/// </summary>
		/// <param name="grid"></param>
		/// <param name="e"></param>
		private void SetRowStyle(SmartBandedGrid grid, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
		{
			if (e.RowHandle < 0) return;

			if (grid != null)
			{
				if (Format.GetString(grid.View.GetRowCellValue(e.RowHandle, "PROCESSCHANGETYPE")).Equals("Move"))
				{
					e.Appearance.BackColor = Color.SkyBlue;
					e.HighPriority = true;
				}
				else if (Format.GetString(grid.View.GetRowCellValue(e.RowHandle, "PROCESSCHANGETYPE")).Equals("Add"))
				{
					e.Appearance.BackColor = Color.LightGreen;
					e.HighPriority = true;
				}
				else if (Format.GetString(grid.View.GetRowCellValue(e.RowHandle, "PROCESSCHANGETYPE")).Equals("Delete"))
				{
					e.Appearance.BackColor = Color.Orange;
					e.HighPriority = true;
				}

			}
		}


		/// <summary>
		/// To 라우팅 리스트 포커스 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void View_FocusedRowChanged2(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
		{
			if (grdToRoutingList.View.FocusedRowHandle < 0) return;

			CompareState(grdToRoutingList, grdFromRoutingList);
		}

		/// <summary>
		/// From 라우팅 리스트 포커스 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void View_FocusedRowChanged1(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
		{
			if (grdFromRoutingList.View.FocusedRowHandle < 0) return;

			CompareState(grdFromRoutingList, grdToRoutingList);
		}

		/// <summary>
		/// 양쪽 Grid 공정 상태 체크 
		/// </summary>
		/// <param name="sourceGrid"></param>
		/// <param name="targetGrid"></param>
		private void CompareState(SmartBandedGrid sourceGrid, SmartBandedGrid targetGrid)
		{
			DataRow selectRow = sourceGrid.View.GetFocusedDataRow();
			if (selectRow == null) return;

			string type = Format.GetString(selectRow["PROCESSCHANGETYPE"]);
			if (type.Equals("Move"))
			{
				string processSegmentId = selectRow["PROCESSSEGMENTID"].ToString();

				DataRow[] row = (targetGrid.DataSource as DataTable).Select("PROCESSSEGMENTID = '" + processSegmentId + "'");
				if (row.Count() <= 0) return;
				int toRowIndex = Format.GetInteger(row[0]["PATHSEQUENCE"]);


				targetGrid.View.UnselectRow(targetGrid.View.FocusedRowHandle);

				targetGrid.View.FocusedRowHandle = (toRowIndex - 1);
				targetGrid.View.SelectRow(toRowIndex - 1);
				RoutingListFocusedRowChange_BomList(targetGrid);
				RoutingListFocusedRowChange_SpecList(targetGrid);
				RoutingListFocusedRowChange_ToolList(targetGrid);
			}
			else
			{
				RoutingListFocusedRowChange_BomList(sourceGrid);
				RoutingListFocusedRowChange_SpecList(sourceGrid);
				RoutingListFocusedRowChange_ToolList(sourceGrid);
			}
		}

		/// <summary>
		/// ROUTING BOM 자재 리스트 조회
		/// </summary>
		private void RoutingListFocusedRowChange_BomList(SmartBandedGrid grid)
		{
			DataRow row = grid.View.GetFocusedDataRow();
			if (row == null) return;

			Dictionary<string, object> param = new Dictionary<string, object>();
			param.Add("PRODUCTDEFID", row["PRODUCTDEFID"]);
			param.Add("PRODUCTDEFVERSION", row["PRODUCTDEFVERSION"]);
			param.Add("PROCESSDEFID", row["PROCESSDEFID"]);
			param.Add("PROCESSDEFVERSION", row["PROCESSDEFVERSION"]);
			param.Add("PROCESSSEGMENTID", row["PROCESSSEGMENTID"]);
			param.Add("PROCESSSEGMENTVERSION", row["PROCESSSEGMENTVERSION"]);

			switch (grid.Name)
			{
				case "grdFromRoutingList":
					grdFromBomList.DataSource = SqlExecuter.Query("GetBomConsumableList", "10001", param);
					break;
				case "grdToRoutingList":
					grdToBomList.DataSource = SqlExecuter.Query("GetBomConsumableList", "10001", param);
					break;
			}
		}

		/// <summary>
		/// ROUTING SPEC 리스트 조회
		/// </summary>
		/// <param name="grid"></param>
		private void RoutingListFocusedRowChange_SpecList(SmartBandedGrid grid)
		{
			DataRow row = grid.View.GetFocusedDataRow();
			if (row == null) return;

			Dictionary<string, object> param = new Dictionary<string, object>();
			param.Add("PRODUCTDEFID", row["PRODUCTDEFID"]);
			param.Add("PRODUCTDEFVERSION", row["PRODUCTDEFVERSION"]);
			param.Add("PROCESSDEFID", row["PROCESSDEFID"]);
			param.Add("PROCESSDEFVERSION", row["PROCESSDEFVERSION"]);
			param.Add("PROCESSSEGMENTID", row["PROCESSSEGMENTID"]);
			param.Add("PROCESSSEGMENTVERSION", row["PROCESSSEGMENTVERSION"]);
			param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

			switch (grid.Name)
			{
				case "grdFromRoutingList":
					grdFromSpecList.DataSource = SqlExecuter.Query("GetProcessSpecList", "10001", param);
					break;
				case "grdToRoutingList":
					grdToSpecList.DataSource = SqlExecuter.Query("GetProcessSpecList", "10001", param);
					break;
			}
		}

		/// <summary>
		/// ROUTING 치공구 리스트 조회
		/// </summary>
		/// <param name="grid"></param>
		private void RoutingListFocusedRowChange_ToolList(SmartBandedGrid grid)
		{
			DataRow row = grid.View.GetFocusedDataRow();
			if (row == null) return;

			Dictionary<string, object> param = new Dictionary<string, object>();
			param.Add("PRODUCTDEFID", row["PRODUCTDEFID"]);
			param.Add("PRODUCTDEFVERSION", row["PRODUCTDEFVERSION"]);
			param.Add("PROCESSDEFID", row["PROCESSDEFID"]);
			param.Add("PROCESSDEFVERSION", row["PROCESSDEFVERSION"]);
			param.Add("PROCESSSEGMENTID", row["PROCESSSEGMENTID"]);
			param.Add("PROCESSSEGMENTVERSION", row["PROCESSSEGMENTVERSION"]);
			param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

			switch (grid.Name)
			{
				case "grdFromRoutingList":
					grdFromToolList.DataSource = SqlExecuter.Query("GetBorDurableList", "10001", param);
					break;
				case "grdToRoutingList":
					grdToToolList.DataSource = SqlExecuter.Query("GetBorDurableList", "10001", param);
					break;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="dt"></param>
		/// <param name="row"></param>
		/// <returns></returns>
		private DataTable GetRowDataByDataTable(DataTable dt, DataRow row)
		{
			if (row == null) return null;

			dt = row.Table.Clone();

			dt.ImportRow(row);
			dt.AcceptChanges();

			dt.Rows[0]["RCPRODUCTDEFID"] = popTransProduct.EditValue;
			dt.Rows[0]["CHANGEREASON"] = txtTransReason.EditValue;

			return dt;
		}

		/// <summary>
		/// 변경점 신청 리스트 선택 시 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
		{
			if (grdChangePointList.View.FocusedRowHandle < 0 || e.PrevFocusedRowHandle < 0) return;

			dtFile = null;

			DataRow selectRow = grdChangePointList.View.GetFocusedDataRow();
			if (selectRow == null) return;

			_isInit = false;

			DataTable compareDt = new DataTable();
			compareDt = GetRowDataByDataTable(compareDt, grdChangePointList.View.GetDataRow(e.PrevFocusedRowHandle));
			DialogResult result = DialogResult.None;
			bool isChanged = CheckDifferentInputData(_inputDt, compareDt, out result);

			if (isChanged || result == DialogResult.None)
			{
				RowDataBindInTableControls(selectRow);

				if (string.IsNullOrEmpty(Format.GetString(selectRow["RCSTATE"])))
				{
					EnableControls(true);
				}
				else
				{
					EnableControls(false);
				}

				chkCustomerType.Checked = false;
				chkOurCompanyType.Checked = false;
				if (Format.GetString(selectRow["PRODUCTDEFID"]) == Format.GetString(selectRow["RCPRODUCTDEFID"]))
				{
					chkOurCompanyType.Checked = true;
				}
				else if (!string.IsNullOrEmpty(Format.GetString(selectRow["RCPRODUCTDEFID"])))
				{
					chkCustomerType.Checked = true;
				}

				chkRC.Checked = false;
				chkNext.Checked = false;
				if (Format.GetString(selectRow["IMPLEMENTATIONTYPE"]).Equals("RC"))
				{
					chkRC.Checked = true;
				}
				else if (Format.GetString(selectRow["IMPLEMENTATIONTYPE"]).Equals("Next"))
				{
					chkNext.Checked = true;
				}
			}
			else
			{
				grdChangePointList.View.FocusedRowChanged -= View_FocusedRowChanged;
				grdChangePointList.View.SelectRow(e.PrevFocusedRowHandle);
				grdChangePointList.View.FocusedRowHandle = e.PrevFocusedRowHandle;
				grdChangePointList.View.FocusedRowChanged += View_FocusedRowChanged;
			}

			_inputDt = GetRowDataByDataTable(_inputDt, selectRow);

			FileBind(selectRow);
			//popTransProduct.SetValue(null);
			//txtTransProductRev.EditValue = null;
			//txtTransProductName.EditValue = null;
			//txtTransReason.EditValue = null;
		}

		/// <summary>
		/// 변경된 값 비교
		/// </summary>
		private bool CheckDifferentInputData(DataTable original, DataTable compare, out DialogResult result)
		{
			bool isDifference = false;
			result = DialogResult.None;

			if (original == null || compare == null) return isDifference;
			if (original.Rows.Count <= 0 || compare.Rows.Count <= 0) return isDifference;

			foreach (DataColumn oCol in original.Columns)
			{
				foreach (DataColumn cCol in compare.Columns)
				{
					if (oCol.ColumnName.Equals(cCol.ColumnName)
					&& Format.GetString(oCol.Table.Rows[0][oCol.ColumnName]) != Format.GetString(cCol.Table.Rows[0][cCol.ColumnName]))
					{
						//입력한 내용이 있으면 현재 작성중인 내용인 모두 삭제됩니다.
						result = ShowMessageBox("WRITTENBEDELETE", Language.Get("MESSAGEINFO"), MessageBoxButtons.OKCancel);
						if (result == DialogResult.OK)
						{
							isDifference = true;
						}
						break;
					}
				}//foreach

				if (isDifference)
				{
					break;
				}

			}//foreach

			return isDifference;
		}

		/// <summary>
		/// 탭 변경 시 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TabGovernanceChange_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
		{
			int index = tabGovernanceChange.SelectedTabPageIndex;
			var buttons = pnlToolbar.Controls.Find<SmartButton>(true);

			switch (index)
			{
				case 0:
					//버튼 show & hide
					btnEventToggle.Hide();

					SetConditionVisiblility("P_PLANTID", DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
					SetConditionVisiblility("P_PERIOD", DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
					SetConditionVisiblility("P_CUSTOMER", DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
					//SetConditionVisiblility("P_INVENTORYCATEGORY", DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
					SetConditionVisiblility("P_PRODUCTIONTYPE", DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
					SetConditionVisiblility("P_ITEMID", DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
					SetConditionVisiblility("P_PRODUCTNAME", DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
					SetConditionVisiblility("P_SPECOWNER", DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
					SetConditionVisiblility("P_GOVERNANCENO", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
					Conditions.GetCondition("P_GOVERNANCENO").IsRequired = false;
					SetConditionVisiblility("P_RCSTATE", DevExpress.XtraLayout.Utils.LayoutVisibility.Always);

					//Control[] controls = pnlToolbar.Controls.Find("Confirmation", true);
					//if (controls.Count() > 0 && controls[0] is SmartButton)
					//{
					//    SmartButton confirmButton = controls[0] as SmartButton;
					//    confirmButton.Visible = false;
					//}

					//controls = pnlToolbar.Controls.Find("Compare", true);
					//if (controls.Count() > 0 && controls[0] is SmartButton)
					//{
					//    SmartButton compareButton = controls[0] as SmartButton;
					//    compareButton.Visible = false;
					//}
					foreach (SmartButton button in buttons)
					{
						if (button.Name == "Confirmation" || button.Name == "Compare" || button.Name == "CancelConfirmation")
							button.Visible = false;
						else
							button.Visible = true;
					}
					//pnlToolbar.Controls.Find("Save", true).
					//SmartButton saveButton = pnlToolbar.Controls.Find<SmartButton>("Confirmation", true) as SmartButton;
					//saveButton.Visible = false;
					//Compare	비교	Checked

					break;
				case 1:

					InitPrivateValues();

					SetConditionVisiblility("P_PLANTID", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
					SetConditionVisiblility("P_PERIOD", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
					SetConditionVisiblility("P_CUSTOMER", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
					//SetConditionVisiblility("P_INVENTORYCATEGORY", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
					SetConditionVisiblility("P_PRODUCTIONTYPE", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
					SetConditionVisiblility("P_ITEMID", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
					SetConditionVisiblility("P_PRODUCTNAME", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
					SetConditionVisiblility("P_SPECOWNER", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
					SetConditionVisiblility("P_GOVERNANCENO", DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
					Conditions.GetCondition("P_GOVERNANCENO").IsRequired = true;
					SetConditionVisiblility("P_RCSTATE", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);


					//버튼 show & hide
					btnEventToggle.Show();
					foreach (SmartButton button in buttons)
					{
						if (button.Name == "Confirmation" || button.Name == "Compare" || button.Name == "CancelConfirmation")
							button.Visible = true;
						else
							button.Visible = false;
					}

					/*
					DataRow selectRow = grdChangePointList.View.GetFocusedDataRow();
					if(selectRow != null)
					{
						SmartSelectPopupEdit popGovernanceNo = Conditions.GetControl<SmartSelectPopupEdit>("P_GOVERNANCENO");
						popGovernanceNo.SetValue(selectRow["GOVERNANCENO"]);
						_productDefId = Format.GetString(selectRow["PRODUCTDEFID"]);
						_productDefVer = Format.GetString(selectRow["PRODUCTDEFVERSION"]);
						_rcProductDefId = Format.GetString(selectRow["RCPRODUCTDEFID"]);
						_rcProductDefVer = Format.GetString(selectRow["RCPRODUCTDEFVERSION"]);
						_pcnNo = Format.GetString(selectRow["CHANGEPOINTNO"]);
						_governanceNo = Format.GetString(selectRow["GOVERNANCENO"]);
						_governanceType = Format.GetString(selectRow["GOVERNANCETYPE"]);
					}
					OnSearchAsync();
					*/

					break;
			}
		}

		/// <summary>
		/// 품목(변경)팝업 값 변경 시 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PopTransProduct_EditValueChanged(object sender, EventArgs e)
		{
			//if (string.IsNullOrEmpty(popTransProductEditValue.ToString())) return;

			//if (chkOurCompanyType.Checked)
			//{
			//	if (txtPrevProduct.Text != popTransProductEditValue.ToString())
			//	{
			//		//같은 품목ID만 선택할 수 있습니다.
			//		ShowMessage("SELECTSAMEPRODUCEDEFID");
			//		popTransProduct.EditValue = string.Empty;
			//		return;
			//	}

			//	if ((txtPrevProduct.Text == popTransProductEditValue.ToString())
			//		&& (txtPrevProductRev.Text == txtProductRev.Text)
			//		&& !_isNotSelected)
			//	{
			//		//다른 품목 버전만 선택할 수 있습니다.
			//		ShowMessage("SELECTDIFFERNENTPRODUCEDEFVERSION");
			//		popTransProduct.EditValue = string.Empty;
			//		return;
			//	}
			//         }

			//if (chkCustomerType.Checked)
			//{
			//	if (txtPrevProduct.Text == popTransProduct.EditValue.ToString())
			//	{
			//		//다른 품목만 선택할 수 있습니다.
			//		ShowMessage("SELECTDIFFERNENTPRODUCEDEFID");
			//		popTransProduct.EditValue = string.Empty;
			//		return;
			//	}

			//         }

		}


		/// <summary>
		/// 스크롤 이동 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void View_LeftCoordChanged1(object sender, EventArgs e)
		{
			grdFromRoutingList.View.LeftCoord = grdToRoutingList.View.LeftCoord;
		}

		/// <summary>
		/// 스크롤 이동 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void View_LeftCoordChanged(object sender, EventArgs e)
		{
			grdToRoutingList.View.LeftCoord = grdFromRoutingList.View.LeftCoord;
		}

		/// <summary>
		/// 스크롤 이동 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void View_TopRowChanged1(object sender, EventArgs e)
		{
			grdFromRoutingList.View.TopRowIndex = grdToRoutingList.View.TopRowIndex;
		}

		/// <summary>
		/// 스크롤 이동 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void View_TopRowChanged(object sender, EventArgs e)
		{
			grdToRoutingList.View.TopRowIndex = grdFromRoutingList.View.TopRowIndex;
		}

		/// <summary>
		/// 스크롤 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void View_LeftCoordChanged3(object sender, EventArgs e)
		{
			grdFromSpecList.View.LeftCoord = grdToSpecList.View.LeftCoord;
		}

		/// <summary>
		/// 스크롤 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void View_LeftCoordChanged2(object sender, EventArgs e)
		{
			grdToSpecList.View.LeftCoord = grdFromSpecList.View.LeftCoord;
		}

		/// <summary>
		/// 탭 이동 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TabToInfo_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
		{
			tabFromInfo.SelectedTabPageIndex = tabToInfo.SelectedTabPageIndex;
		}

		/// <summary>
		/// 탭 이동 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TabFromInfo_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
		{
			tabToInfo.SelectedTabPageIndex = tabFromInfo.SelectedTabPageIndex;
		}

		#endregion

		/// <summary>
		/// ToolBar Action
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected override void OnToolbarClick(object sender, EventArgs e)
		{
			//base.OnToolbarClick(sender, e);
			SmartButton btn = sender as SmartButton;

			switch (btn.Name.ToString())
			{
				case "Initialization":
					int index = tabGovernanceChange.SelectedTabPageIndex;
					 
					switch (index)
					{
						case 0://변경등록

							InitializeInputControls();
							EnableControls(true);
							chkNext.Checked = true;
							_isInit = true;
							grdFileList.View.ClearDatas();
							break;
						case 1://Routing 비교

							ClearInitState();

							break;
					}
					break;
				case "Confirmation":
					if (string.IsNullOrWhiteSpace(_state))
					{
						return;
					}

					if (_state.Equals("Confirm"))
					{
						//이미 확정된 R/C 입니다.
						ShowMessage("ALREADYCONFIRMED");
						return;
					}

					if (tabGovernanceChange.SelectedTabPageIndex == 1)
					{
						MessageWorker worker = new MessageWorker("SaveRunningChange");
						worker.SetBody(new MessageBody()
						{
							{ "governanceNo", _governanceNo },
							{ "governanceType", _governanceType },
							{ "status", "Confirm" }
						});

						worker.Execute();

						ShowMessage("SuccessSave");

						_state = "Confirm";
						OnSearchAsync();
					}
					break;
				case "CancelConfirmation":
					if(string.IsNullOrWhiteSpace(_state))
					{
						return;
					}

					if(_state.Equals("Request"))
					{
						//이미 요청상태 입니다.
						ShowMessage("AlreadyRequest");
						return;
					}

					if (tabGovernanceChange.SelectedTabPageIndex == 1)
					{
						MessageWorker worker = new MessageWorker("SaveRunningChange");
						worker.SetBody(new MessageBody()
						{
							{ "governanceNo", _governanceNo },
							{ "governanceType", _governanceType },
							{ "status", "Request" }
						});

						worker.Execute();

						ShowMessage("SuccessSave");

						_state = "Request";
						OnSearchAsync();
					}
					break;
				case "Compare":
					if (tabGovernanceChange.SelectedTabPageIndex == 1)
					{
						Dictionary<string, object> param = new Dictionary<string, object>();
						param.Add("TARGET_PRODUCTDEFID", _productDefId);
						param.Add("TARGET_PRODUCTDEFVERSION", _productDefVer);
						param.Add("SOURCE_PRODUCTDEFID", _rcProductDefId);
						param.Add("SOURCE_PRODUCTDEFVERSION", _rcProductDefVer);
						param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
						param.Add("ISLEFT", "Y");
						//FROM 비교
						grdFromRoutingList.DataSource = SqlExecuter.Query("SelectCompareRoutingList", "10001", param);

						param.Clear();
						param.Add("TARGET_PRODUCTDEFID", _rcProductDefId);
						param.Add("TARGET_PRODUCTDEFVERSION", _rcProductDefVer);
						param.Add("SOURCE_PRODUCTDEFID", _productDefId);
						param.Add("SOURCE_PRODUCTDEFVERSION", _productDefVer);
						param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
						param.Add("ISLEFT", "N");
						//TO 비교
						grdToRoutingList.DataSource = SqlExecuter.Query("SelectCompareRoutingList", "10001", param);
					}
					break;
				case "Save":
  
                    DataTable dt2 = grdChangePointList.GetChangedRows();
                    if (dt2.Rows.Count > 0)
                    {

                        if (dt2.Rows[0]["_STATE_"].Equals("deleted"))
                        {
                            foreach (DataRow dr in dt2.Rows)
                            {
                                if (Format.GetString(dr["RCSTATEID"]).Equals("Confirm"))
                                {
                                    ShowMessage("확정된 사항은 삭제할 수 없습니다");
                                    return;
                                }


                            }
                            ExecuteRule("SaveRunningChange", dt2);
                            ShowMessage("SuccessSave");

                            //초기화
                            //InitializeInputControls();

                            OnSearchAsync();
                        }
                    }
                    else
                    {

                        if (tabGovernanceChange.SelectedTabPageIndex == 0)
                        {
                            if (!chkCustomerType.Checked & !chkOurCompanyType.Checked)
                            {
                                //변경 구분 선택은 필수입니다.
                                ShowMessage("ISREQUIREDSELECTCHANGETYPE");
                                return;
                            }

                            if (string.IsNullOrEmpty(Format.GetString(popTransProduct.EditValue))
                            || string.IsNullOrEmpty(Format.GetString(txtTransProductRev.EditValue)))
                            {
                                //변경 품목은 필수 입력입니다.
                                ShowMessage("ISREQUIREDRCPRODUCT");
                                return;
                            }

                            if (string.IsNullOrEmpty(Format.GetString(popPrevProduct.EditValue))
                            || string.IsNullOrEmpty(Format.GetString(txtPrevProductRev.Text)))
                            {
                                //기존 품목은 필수 입니다.
                                ShowMessage("ISREQUIREDPREVPRODUCT");
                                return;
                            }

                            DataRow selectRow = grdChangePointList.View.GetFocusedDataRow();
                            //if (!_isInit && selectRow != null && !string.IsNullOrWhiteSpace(Format.GetString(selectRow["RCSTATE"])))
                            //{
                            //	//이미 요청 또는 확정된 R/C 입니다.
                            //	ShowMessage("ALREADYREQUESTED");
                            //	return;
                            //}


                            DataTable dt = new DataTable();
                            if (string.IsNullOrWhiteSpace(txtGovernanceNo.Text))
                            {
                                dt.Columns.Add("DEPARTMENT", typeof(object));
                                dt.Columns.Add("REASON", typeof(object));
                                //dt.Columns.Add("SPECPERSON", typeof(object));
                                //dt.Columns.Add("CHANGESPECPERSON", typeof(object));
                                dt.Columns.Add("PCRNO", typeof(object));
                                dt.Columns.Add("PCRREQUESTER", typeof(object));
                                dt.Columns.Add("PCRDATE", typeof(object));
                                //dt.Columns.Add("CUSTOMERID", typeof(object));
                                //dt.Columns.Add("CHANGECUSTOMERID", typeof(object));
                                dt.Columns.Add("PRODUCTDEFID", typeof(object));
                                dt.Columns.Add("PRODUCTDEFVERSION", typeof(object));
                                dt.Columns.Add("RCPRODUCTDEFID", typeof(object));
                                dt.Columns.Add("RCPRODUCTDEFVERSION", typeof(object));
                                dt.Columns.Add("IMPLEMENTATIONTYPE", typeof(object));
                                dt.Columns.Add("FILEID", typeof(object));

                                DataRow newRow = dt.NewRow();
                                newRow["PRODUCTDEFID"] = popPrevProduct.EditValue;
                                newRow["PRODUCTDEFVERSION"] = txtPrevProductRev.Text;
                                newRow["RCPRODUCTDEFID"] = popTransProduct.EditValue;
                                newRow["RCPRODUCTDEFVERSION"] = txtTransProductRev.EditValue;
                                //newRow["CUSTOMERID"] = cboPrevCustomer.EditValue;//popPrevCustomer.GetValue();
                                //newRow["CHANGECUSTOMERID"] = cboTransCustomer.EditValue;//popTransCustomer.GetValue();
                                newRow["PCRDATE"] = (_isInit == true) ? null : ((selectRow != null) ? selectRow["REQUESTDATE_YYYYMMDDHHMISS"] : null);
                                newRow["PCRNO"] = txtChangePoint.Text;
                                newRow["PCRREQUESTER"] = cboRequestor.EditValue;
                                //newRow["SPECPERSON"] = cboPrevSpecOwner.EditValue;//popPrevSpecOwner.GetValue();
                                //newRow["CHANGESPECPERSON"] = cboTransSpecOwner.EditValue;//popTransSpecOwner.GetValue();
                                newRow["REASON"] = txtTransReason.Text;
                                newRow["DEPARTMENT"] = txtRequestTeam.Text;
                                //if (UserInfo.Current.Enterprise == "YOUNGPOONG")
                                newRow["IMPLEMENTATIONTYPE"] = chkRC.Checked ? "RC" : "Next";
                                //newRow["FILEID"] = (selectRow != null) ? selectRow["FILEID"] : _fileId;


                                dt.Rows.Add(newRow);

                            }

                            dtFile = grdFileList.DataSource as DataTable;

                            //Running Change 등록
                            MessageWorker worker = new MessageWorker("SaveRunningChange");
                            worker.SetBody(new MessageBody()
                        {
                            { "enterpriseId", UserInfo.Current.Enterprise },
                            { "plantId", UserInfo.Current.Plant },
                            { "runningChangeList", dt },
                            { "filelist", dtFile },
                            { "governanceNo", txtGovernanceNo.Text },
                            { "status", "Request" }
                        });

                            worker.Execute();

                        }





                        DataTable dtFiledata = new DataTable();

                        if (dtFile != null)
                        {
                            dtFiledata = dtFile.Copy();
                            dtFile.Clear();
                        }


                        //File Upload
                        List<DataRow> state = dtFiledata.AsEnumerable().Where(r => Format.GetString(r["_STATE_"]).Equals("added")).Distinct().ToList();
                        if (state.Count > 0 && dtFiledata.Rows.Count > 0)
                        {
                            FileUploadToServer(dtFiledata);
                        }

                        ShowMessage("SuccessSave");

                        //초기화
                        //InitializeInputControls();

                        OnSearchAsync();
                    }
					break;
			}
		}


		/// <summary>
		/// 상태 초기화
		/// </summary>
		private void ClearInitState()
		{
			DataTable frDt = grdFromRoutingList.DataSource as DataTable;
			foreach (DataRow row in frDt.Rows)
			{
				row["PROCESSCHANGETYPE"] = string.Empty;
				row["MATERIALCHANGETYPE"] = string.Empty;
				row["SPECCHANGETYPE"] = string.Empty;
				row["TOOLCHANGETYPE"] = string.Empty;
			}

			DataTable toDt = grdToRoutingList.DataSource as DataTable;
			foreach (DataRow row in toDt.Rows)
			{
				row["PROCESSCHANGETYPE"] = string.Empty;
				row["MATERIALCHANGETYPE"] = string.Empty;
				row["SPECCHANGETYPE"] = string.Empty;
				row["TOOLCHANGETYPE"] = string.Empty;
			}

			(grdFromRoutingList.DataSource as DataTable).AcceptChanges();
			(grdToRoutingList.DataSource as DataTable).AcceptChanges();
		}

		#region 검색

		/// <summary>
		/// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
		/// </summary>
		protected async override Task OnSearchAsync()
		{
			await base.OnSearchAsync();

			var values = Conditions.GetValues();
			values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
			values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

			int index = tabGovernanceChange.SelectedTabPageIndex;
			switch (index)
			{
				case 0:

					//초기화
					txtGovernanceNo.Text = string.Empty;
					//InitializeInputControls();
					if (dtFile != null)
					{
						dtFile.Clear();
					}
					values.Remove("P_GOVERNANCENO");

					DataTable dt = await QueryAsync("SelectChangePointList", "10002", values);

					if (dt.Rows.Count < 1)
					{
						// 조회할 데이터가 없습니다.
						ShowMessage("NoSelectData");
					}
					else
					{
						_isInit = false;
					}

					grdChangePointList.DataSource = dt;

					grdChangePointList.View.FocusedRowChanged -= View_FocusedRowChanged;
					grdChangePointList.View.FocusedRowHandle = 0;
					grdChangePointList.View.FocusedRowChanged += View_FocusedRowChanged;
					DataRow row = grdChangePointList.View.GetFocusedDataRow();

					if(row != null)
					{
						RowDataBindInTableControls(row);

						if (string.IsNullOrEmpty(Format.GetString(row["RCSTATE"])))
						{
							EnableControls(true);
						}
						else
						{
							EnableControls(false);
						}

						_inputDt = GetRowDataByDataTable(_inputDt, row);

						chkCustomerType.Checked = false;
						chkOurCompanyType.Checked = false;
						if (Format.GetString(row["RCDIVISION"]).Equals("OurCompany"))
						{
							chkOurCompanyType.Checked = true;
						}
						else if (Format.GetString(row["RCDIVISION"]).Equals("Customer"))
						{
							chkCustomerType.Checked = true;
						}

						chkRC.Checked = false;
						chkNext.Checked = false;
						if (Format.GetString(row["IMPLEMENTATIONTYPE"]).Equals("RC"))
						{
							chkRC.Checked = true;
						}
						else if (Format.GetString(row["IMPLEMENTATIONTYPE"]).Equals("Next"))
						{
							chkNext.Checked = true;
						}

						FileBind(row);
					}


					break;
				case 1:

					var buttons = pnlToolbar.Controls.Find<SmartButton>(true);
					foreach (SmartButton button in buttons)
					{
						if (button.Name == "Confirmation" || button.Name == "Compare" || button.Name == "CancelConfirmation")
						{
							button.Enabled = true;
						}
					}

					grdFromRoutingList.View.ClearDatas();
					grdFromBomList.View.ClearDatas();
					grdFromSpecList.View.ClearDatas();
					grdFromToolList.View.ClearDatas();

					grdToRoutingList.View.ClearDatas();
					grdToBomList.View.ClearDatas();
					grdToSpecList.View.ClearDatas();
					grdToToolList.View.ClearDatas();

					values.Remove("P_PRODUCTIONTYPE");
					values.Remove("P_PERIOD");
					values.Remove("P_PERIOD_PERIODFR");
					values.Remove("P_PERIOD_PERIODTO");
					values.Remove("P_PRODUCTNAME");
					values.Remove("P_CUSTOMER");
					//values.Remove("P_INVENTORYCATEGORY");
					values.Remove("P_ITEMID");
					values.Remove("P_SPECOWNER");

					//품목(기존) 조회
					values.Add("PRODUCTDEFID", _productDefId);
					values.Add("PRODUCTDEFVERSION", _productDefVer);

					DataTable dtFromRouting = await QueryAsync("SelectRoutingListByProduct", "10001", values);
					grdFromRoutingList.DataSource = dtFromRouting;

					txtFromProductId.Text = _productDefId;
					txtFromProductVer.Text = _productDefVer;
					txtFromProductName.Text = _productName;


					//품목(변경) 조회
					values["PRODUCTDEFID"] = _rcProductDefId;
					values["PRODUCTDEFVERSION"] = _rcProductDefVer;

					DataTable dtToRouting = await QueryAsync("SelectRoutingListByProduct", "10001", values);
					grdToRoutingList.DataSource = dtToRouting;

					txtToProductId.Text = _rcProductDefId;
					txtToProductVer.Text = _rcProductDefVer;
					txtToProductName.Text = _rcProductName;

					//하단 탭 조회
					if (dtFromRouting.Rows.Count > 0)
					{
						grdFromRoutingList.View.FocusedRowHandle = 0;
						DataRow frRow = grdFromRoutingList.View.GetFocusedDataRow();
						TabFromInfoDataBind(frRow);
					}

					if (dtToRouting.Rows.Count > 0)
					{
						grdToRoutingList.View.FocusedRowHandle = 0;
						DataRow toRow = grdToRoutingList.View.GetFocusedDataRow();
						TabToInfoDataBind(toRow);
					}

					//RC적용 여부 조회
					DataTable dtRc = SqlExecuter.Query("GetExistsPcrNo", "10001", new Dictionary<string, object>() { { "PCNNO", _pcnNo } });
					if (dtRc.Rows.Count > 0)
					{
						//비교, 확정 버튼 비활성화
						foreach (SmartButton button in buttons)
						{
							if (button.Name == "Confirmation" || button.Name == "Compare" || button.Name == "CancelConfirmation")
							{
								button.Enabled = false;
							}
						}
					}

					DataRow rcSelectRow = grdChangePointList.View.GetFocusedDataRow();
					if ((rcSelectRow != null && rcSelectRow["RCSTATEID"].Equals("Confirm")) || _isLoadForm)
					{
						_isLoadForm = false;

						Dictionary<string, object> param = new Dictionary<string, object>();
						param.Add("TARGET_PRODUCTDEFID", _productDefId);
						param.Add("TARGET_PRODUCTDEFVERSION", _productDefVer);
						param.Add("SOURCE_PRODUCTDEFID", _rcProductDefId);
						param.Add("SOURCE_PRODUCTDEFVERSION", _rcProductDefVer);
						param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
						param.Add("ISLEFT", "Y");
						//FROM 비교
						grdFromRoutingList.DataSource = SqlExecuter.Query("SelectCompareRoutingList", "10001", param);

						param.Clear();
						param.Add("TARGET_PRODUCTDEFID", _rcProductDefId);
						param.Add("TARGET_PRODUCTDEFVERSION", _rcProductDefVer);
						param.Add("SOURCE_PRODUCTDEFID", _productDefId);
						param.Add("SOURCE_PRODUCTDEFVERSION", _productDefVer);
						param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
						param.Add("ISLEFT", "N");
						//TO 비교
						grdToRoutingList.DataSource = SqlExecuter.Query("SelectCompareRoutingList", "10001", param);

					}
					break;
			}

		}

		#endregion

		/// <summary>
		/// 첨부된 파일 bind
		/// </summary>
		/// <param name="selectRow"></param>
		private void FileBind(DataRow selectRow)
		{
			//File Bind
			string[] fileIds = Format.GetString(selectRow["FILEID"]).Split(',');
			string[] fileNames = Format.GetString(selectRow["FILENAME"]).Split(',');
			string[] fileExts = Format.GetString(selectRow["FILEEXT"]).Split(',');
			string[] fileSizes = Format.GetString(selectRow["FILESIZE"]).Split(',');

			DataTable fileDt = (grdFileList.DataSource as DataTable).Clone();
			for (int i = 0; i < fileIds.Length; i++)
			{
				if (!string.IsNullOrWhiteSpace(fileIds[i]))
				{
					DataRow fr = fileDt.NewRow();
					fr["FILEID"] = fileIds[i];
					fr["FILENAME"] = fileNames[i];
					fr["FILEEXT"] = fileExts[i];
					fr["FILESIZE"] = fileSizes[i];

					fr["SEQUENCE"] = i;
					fr["RESOURCETYPE"] = "GovermanceChange";
					fr["RESOURCEID"] = selectRow["GOVERNANCENO"];
					fr["RESOURCEVERSION"] = "*";

					fileDt.Rows.Add(fr);
				}
			}

			grdFileList.DataSource = fileDt;

		}

		/// <summary>
		/// From 탭 데이터 조회
		/// </summary>
		private void TabFromInfoDataBind(DataRow r)
		{
			Dictionary<string, object> param = new Dictionary<string, object>();
			param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
			param.Add("PRODUCTDEFID", r["PRODUCTDEFID"]);
			param.Add("PRODUCTDEFVERSION", r["PRODUCTDEFVERSION"]);
			param.Add("PROCESSDEFID", r["PROCESSDEFID"]);
			param.Add("PROCESSDEFVERSION", r["PROCESSDEFVERSION"]);
			param.Add("PROCESSSEGMENTID", r["PROCESSSEGMENTID"]);
			param.Add("PROCESSSEGMENTVERSION", r["PROCESSSEGMENTVERSION"]);

			int selectIndex = tabFromInfo.SelectedTabPageIndex;
			switch (selectIndex)
			{
				case 0:
					//자재(기존)
					grdFromBomList.DataSource = SqlExecuter.Query("GetBomConsumableList", "10001", param);
					break;
				case 1:
					//공정스펙(기존)
					grdFromSpecList.DataSource = SqlExecuter.Query("GetProcessSpecList", "10001", param);
					break;
				case 2:
					//공정스펙(기존)
					grdFromToolList.DataSource = SqlExecuter.Query("GetBorDurableList", "10001", param);
					break;
			}
		}

		/// <summary>
		/// To 탭 데이터 조회
		/// </summary>
		private void TabToInfoDataBind(DataRow r)
		{
			Dictionary<string, object> param = new Dictionary<string, object>();
			param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
			param.Add("PRODUCTDEFID", r["PRODUCTDEFID"]);
			param.Add("PRODUCTDEFVERSION", r["PRODUCTDEFVERSION"]);
			param.Add("PROCESSDEFID", r["PROCESSDEFID"]);
			param.Add("PROCESSDEFVERSION", r["PROCESSDEFVERSION"]);
			param.Add("PROCESSSEGMENTID", r["PROCESSSEGMENTID"]);
			param.Add("PROCESSSEGMENTVERSION", r["PROCESSSEGMENTVERSION"]);

			int selectIndex = tabToInfo.SelectedTabPageIndex;
			switch (selectIndex)
			{
				case 0:
					//자재(변경)
					grdToBomList.DataSource = SqlExecuter.Query("GetBomConsumableList", "10001", param);
					break;
				case 1:
					//공정스펙(변경)
					grdToSpecList.DataSource = SqlExecuter.Query("GetProcessSpecList", "10001", param);
					break;
				case 2:
					//Tool(변경)
					grdToToolList.DataSource = SqlExecuter.Query("GetBorDurableList", "10001", param);
					break;
			}
		}


		#region Private Function

		/// <summary>
		/// Customizing Context Menu Item Click 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		private void OpenForm(object sender, EventArgs args)
		{
			try
			{
				DialogManager.ShowWaitDialog();

				DataRow currentRow = grdChangePointList.View.GetFocusedDataRow();
				if (currentRow == null) return;

				string menuId = (sender as DXMenuItem).Tag.ToString();

				var param = currentRow.Table.Columns
					.Cast<DataColumn>()
					.ToDictionary(col => col.ColumnName, col => currentRow[col.ColumnName]);

				param.Add("CALLMENU", "GovernanceChange2");

				OpenMenu(menuId, param); //다른창 호출..
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
			finally
			{
				DialogManager.Close();
			}
		}

		/// <summary>
		/// 내부 전역변수 클리어
		/// </summary>
		private void InitPrivateValues()
		{
			_productDefId = string.Empty;
			_productDefVer = string.Empty;
			_productName = string.Empty;
			_rcProductDefId = string.Empty;
			_rcProductDefVer = string.Empty;
			_rcProductName = string.Empty;
			_governanceNo = string.Empty;
			_governanceType = string.Empty;
			_pcnNo = string.Empty;
			_state = string.Empty;
		}

		/// <summary>
		/// 컨트롤 enable 처리
		/// </summary>
		/// <param name="isEnable"></param>
		private void EnableControls(bool isEnable)
		{
			chkCustomerType.Enabled = isEnable;
			chkOurCompanyType.Enabled = isEnable;
			chkRC.Enabled = isEnable;
			chkNext.Enabled = isEnable;
			txtRegistorDate.Enabled = isEnable;
			popPrevProduct.Enabled = isEnable;
			txtPrevProductRev.Enabled = isEnable;
			txtPrevProductName.Enabled = isEnable;
			popTransProduct.Enabled = isEnable;
			txtTransProductRev.Enabled = isEnable;
			txtTransProductName.Enabled = isEnable;
			txtRequestTeam.Enabled = isEnable;
			//txtFileID.Enabled = isEnable;
			txtTransReason.Enabled = isEnable;
		}

		/// <summary>
		/// SmartSplitTableLayoutPanel 안에 있는 컨트롤에 row데이터 바인드
		/// </summary>
		private void RowDataBindInTableControls(DataRow r)
		{
			if (r == null) return;

			foreach (Control ctl in tlpTransInfo.Controls)
			{
				string tag = Format.GetString(ctl.Tag);
				if (string.IsNullOrEmpty(Format.GetString(ctl.Tag))) continue;

				switch (ctl.GetType().Name)
				{
					case "SmartComboBox":
						SmartComboBox cbo = ctl as SmartComboBox;
						cbo.EditValue = r[tag];
						break;
					case "SmartMemoEdit":
					case "SmartTextBox":
						ctl.Text = r[tag].ToString();
						break;
					case "SmartSelectPopupEdit":
						SmartSelectPopupEdit pop = ctl as SmartSelectPopupEdit;
						if (!string.IsNullOrWhiteSpace(Format.GetString(r[tag])))
						{
							if (tag.EndsWith("ID"))
							{
								if (r.Table.Columns.Contains(tag.Remove(tag.Length - 2) + "NAME"))
								{
									pop.SetValue(r[tag]);
									pop.EditValue = r[tag];
									pop.Text = Format.GetString(r[tag.Remove(tag.Length - 2) + "NAME"]);
								}
								else
								{
									pop.SetValue(r[tag]);
									pop.EditValue = r[tag];
								}									
							}
							else
							{
								pop.SetValue(r[tag]);
								pop.EditValue = r[tag];
							}
						}
						else
						{
							pop.ClearValue();
						}
						break;
				}
			}
		}


        /// <summary>
        /// 체크한 항목이 있는지 확인
        /// </summary>
        private void ValidationCheckFile()
        {
            DataTable selectedFiles = grdFileList.View.GetCheckedRows();

            if (selectedFiles.Rows.Count < 1)
            {
                throw MessageException.Create("GridNoChecked");
            }
        }

        /// <summary>
        /// 체크한 파일들을 선택한 폴더에 다운로드
        /// </summary>
        /// <param name="files">체크한 파일 목록</param>
        /// <param name="type">다운로드 구분 (단일, 복수)</param>
        private void FileDownload(DataTable files, DownloadType type)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.SelectedPath = Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                switch (type)
                {
                    case DownloadType.Single:
                        try
                        {
                            string serverPath = AppConfiguration.GetString("Application.Ftp.Url") + Format.GetString(files.Rows[0]["FILEPATH"]);
                            serverPath = serverPath + ((serverPath.EndsWith("/")) ? "" : "/");
                            DeployCommonFunction.DownLoadFile(serverPath, folderBrowserDialog.SelectedPath, Format.GetString(files.Rows[0]["SAFEFILENAME"]));
                        }
                        catch (Exception ex)
                        {
                            throw MessageException.Create(ex.Message);
                        }

                        break;
                    case DownloadType.Multi:

                        //files.Columns.Add("PROCESSINGSTATUS", typeof(string));

                        int totalFileSize = 0;
                        foreach (DataRow row in files.Rows)
                        {
                            //row["PROCESSINGSTATUS"] = "Wait";

                            totalFileSize += Format.GetInteger(row["FILESIZE"]);
                        }

                        FileProgressDialog fileProgressDialog = new FileProgressDialog(files, UpDownType.Download, folderBrowserDialog.SelectedPath, totalFileSize);
                        fileProgressDialog.ShowDialog();

                        if (fileProgressDialog.DialogResult == DialogResult.Cancel)
                            throw MessageException.Create("파일 다운로드를 취소하였습니다.");


                        ProgressingResult result = fileProgressDialog.Result;

                        int resultCount = 0;

                        if (result.IsSuccess)
                            resultCount = result.SuccessFileCount;

                        grdFileList.View.CheckedAll(false);

                        //2020-02-26 강유라 다운로드 후 파일 실행 
                        //if (executeFileAfterDown)
                        //{
                        //    Process p = new Process();
                        //    ProcessStartInfo pi = new ProcessStartInfo();
                        //    pi.UseShellExecute = true;
                        //    pi.FileName = folderBrowserDialog.SelectedPath + "\\" + Format.GetString(files.Rows[0]["SAFEFILENAME"]);
                        //    p.StartInfo = pi;

                        //    try
                        //    {
                        //        p.Start();
                        //    }
                        //    catch (Exception Ex)
                        //    {
                        //        throw MessageException.Create("CanNotExecuteFile", Ex.Message);
                        //    }
                        //}

                        break;
                }

            }
        }


        /// <summary>
        /// 추가된 파일들을 서버에 업로드
        /// </summary>
        private int FileUpload()
        {
            DataTable addFiles = grdFileList.GetChangesAdded();
            //addFiles.Columns.Add("PROCESSINGSTATUS", typeof(string));

            int totalFileSize = 0;

            foreach (DataRow row in addFiles.Rows)
            {
                //row["PROCESSINGSTATUS"] = "Wait";

                totalFileSize += Format.GetInteger(row["FILESIZE"]);
            }

            addFiles.AcceptChanges();


            FileProgressDialog fileProgressDialog = new FileProgressDialog(addFiles, UpDownType.Upload, "", totalFileSize);
            fileProgressDialog.ShowDialog(this);

            if (fileProgressDialog.DialogResult == DialogResult.Cancel)
                throw MessageException.Create("파일 업로드를 취소하였습니다.");


            ProgressingResult result = fileProgressDialog.Result;

            if (result.IsSuccess)
                return result.SuccessFileCount;
            else
                return 0;           
        }


        /// <summary>
        /// 2019-11-16 강유라
        /// 품질 이상발생 저장시 임시저장한 파일 데이터테이블을 저장하는 함수
        /// 추가된 파일들을 서버에 업로드
        /// </summary>
        private int AddedFileUpload()
        {
            DataTable addFiles = grdFileList.DataSource as DataTable;
            //addFiles.Columns.Add("PROCESSINGSTATUS", typeof(string));

            int totalFileSize = 0;

            foreach (DataRow row in addFiles.Rows)
            {
                //row["PROCESSINGSTATUS"] = "Wait";

                totalFileSize += Format.GetInteger(row["FILESIZE"]);
            }

            addFiles.AcceptChanges();


            FileProgressDialog fileProgressDialog = new FileProgressDialog(addFiles, UpDownType.Upload, "", totalFileSize);
            fileProgressDialog.ShowDialog(this);

            if (fileProgressDialog.DialogResult == DialogResult.Cancel)
                throw MessageException.Create("파일 업로드를 취소하였습니다.");


            ProgressingResult result = fileProgressDialog.Result;

            if (result.IsSuccess)
                return result.SuccessFileCount;
            else
                return 0;
        }

        /// <summary>
        /// 체크한 파일들을 서버, DB 상에서 삭제
        /// </summary>
        private int FileDelete()
        {
            DataTable files = grdFileList.GetChangesDeleted();

            int deleteSuccessCount = 0;
            int deleteFailCount = 0;

            foreach (DataRow row in files.Rows)
            {
                if (DeployCommonFunction.RemoveFile(_UploadPath, Format.GetString(row["SAFEFILENAME"])))
                    deleteFailCount++;
                else
                    deleteSuccessCount++;
            }

            return deleteSuccessCount;

            //grdFileList.View.DeleteCheckedRows();

            //MSGBox.Show(MessageBoxType.Information, "선택한 파일이 삭제되었습니다." + Environment.NewLine + string.Format("성공 : {0}, 실패 : {1}", deleteSuccessCount, deleteFailCount));
        }

        public void SaveChangedFiles()
        {
            int uploadFileCount = FileUpload();
            int deleteFileCount = FileDelete();

            //MSGBox.Show(MessageBoxType.Information, string.Format("{0} 건의 파일을 업로드 하였습니다.", uploadFileCount) + Environment.NewLine + string.Format("{0} 건의 파일을 삭제 하였습니다.", deleteFileCount));
        }

        /// <summary>
        /// 2019-11-16 강유라
        /// 품질이상 발생 임시저장한 파일 데이터 테이블 저장
        /// 임시저장 되었던 테이블을 grdFileList에 바인딩 시켜 업로드하기위해 사용
        /// 업로드만 가능
        /// </summary>
        public void SaveAddedFiles()
        {
            int uploadFileCount = AddedFileUpload();
            //MSGBox.Show(MessageBoxType.Information, string.Format("{0} 건의 파일을 업로드 하였습니다.", uploadFileCount) + Environment.NewLine + string.Format("{0} 건의 파일을 삭제 하였습니다.", deleteFileCount));
        }

        public DataTable GetChangedRows()
        {
            return grdFileList.GetChangedRows();
        }

        public void ClearData()
        {
            if (grdFileList.DataSource == null) return;
            grdFileList.View.ClearDatas();
        }

        #endregion
    }
}

