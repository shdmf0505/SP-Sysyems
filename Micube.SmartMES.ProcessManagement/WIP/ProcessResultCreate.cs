#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons;

using System;
using Micube.SmartMES.Commons.Controls;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Linq;

#endregion

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 공정관리 > 공정작업 > 공정실적 처리
    /// 업  무  설  명  : FROM 공정에서 TO 공정까지 가상의 실적을 생성
    /// 생    성    자  : 황유성
    /// 생    성    일  : 2019-12-02
    /// 수  정  이  력  :
    /// </summary>
    public partial class ProcessResultCreate : SmartConditionManualBaseForm
	{
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가

		#endregion

		#region 생성자

		public ProcessResultCreate()
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
            InitializeComboBox();
        }

        /// <summary>        
        /// 코드그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
		{
            grdLotList.GridButtonItem = GridButtonItem.None;

            grdLotList.View.SetIsReadOnly();

            // CheckBox 설정
            this.grdLotList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            var groupDefaultCol = grdLotList.View.AddGroupColumn("WIPLIST");
            groupDefaultCol.AddTextBoxColumn("PROCESSCLASSID_R", 70).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("REWORKDIVISION", 70).SetTextAlignment(TextAlignment.Center).SetIsHidden();
            groupDefaultCol.AddTextBoxColumn("LOTTYPE", 70).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("PRODUCTDEFID", 150).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("PRODUCTDEFNAME", 300);
            groupDefaultCol.AddTextBoxColumn("USERSEQUENCE", 70).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
            groupDefaultCol.AddTextBoxColumn("PLANTID", 70).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("AREANAME", 150);
            groupDefaultCol.AddTextBoxColumn("RTRSHT", 70).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("LOTID", 200).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("UNIT", 60).SetTextAlignment(TextAlignment.Center).SetIsHidden();
            groupDefaultCol.AddTextBoxColumn("ISLOCKING", 60).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("ISHOLD", 60).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("PROCESSSTATE", 100).SetTextAlignment(TextAlignment.Center);

            var groupWipCol = grdLotList.View.AddGroupColumn("WIPQTY");
            groupWipCol.AddTextBoxColumn("QTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            groupWipCol.AddTextBoxColumn("PANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");

            var groupReceiveCol = grdLotList.View.AddGroupColumn("WIPRECEIVEQTY");
            groupReceiveCol.AddTextBoxColumn("RECEIVEPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            groupReceiveCol.AddTextBoxColumn("RECEIVEPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");

            var groupWorkStartCol = grdLotList.View.AddGroupColumn("WIPSTARTQTY");
            groupWorkStartCol.AddTextBoxColumn("WORKSTARTPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            groupWorkStartCol.AddTextBoxColumn("WORKSTARTPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");

            var groupWorkEndCol = grdLotList.View.AddGroupColumn("WIPENDQTY");
            groupWorkEndCol.AddTextBoxColumn("WORKENDPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            groupWorkEndCol.AddTextBoxColumn("WORKENDPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");

            var groupSendCol = grdLotList.View.AddGroupColumn("WIPSENDQTY");
            groupSendCol.AddTextBoxColumn("SENDPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            groupSendCol.AddTextBoxColumn("SENDPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");

            var groupWIPCol = grdLotList.View.AddGroupColumn("WIPLIST");
            groupWIPCol.AddTextBoxColumn("LEADTIME", 80).SetTextAlignment(TextAlignment.Right);
            groupWIPCol.AddTextBoxColumn("LOTINPUTDATE", 150).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("{0:yyyy-MM-dd HH:mm:ss}");
            groupWIPCol.AddTextBoxColumn("DELIVERYDATE", 150).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("{0:yyyy-MM-dd HH:mm:ss}");
            groupWIPCol.AddTextBoxColumn("LEFTDATE", 80).SetTextAlignment(TextAlignment.Center);

            grdLotList.View.PopulateColumns();
        }

        private void InitializeComboBox()
        {
            // From 공정
            cboFromProcess.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboFromProcess.ShowHeader = false;
            cboFromProcess.ValueMember = "PROCESSSEGMENTID";
            cboFromProcess.DisplayMember = "PROCESSSEGMENTNAME";
            cboFromProcess.UseEmptyItem = false;

            // To 공정
            cboToProcess.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboToProcess.ShowHeader = false;
            cboToProcess.ValueMember = "PROCESSSEGMENTID";
            cboToProcess.DisplayMember = "PROCESSSEGMENTNAME";
            cboToProcess.UseEmptyItem = false;

            // To 공정 인계자원
            cboResource.DisplayMember = "RESOURCENAME";
            cboResource.ValueMember = "RESOURCEID";
            cboResource.ShowHeader = false;
            cboResource.UseEmptyItem = true;

            // To 공정 인계자원
            cboTargetCompany.DisplayMember = "CODENAME";
            cboTargetCompany.ValueMember = "CODEID";
            cboTargetCompany.ShowHeader = false;
            cboTargetCompany.UseEmptyItem = true;
            setComboBoxTagetCompany();
        }
        #endregion

        private void setComboBoxTagetCompany()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("CODECLASSID", "VirualResultTarget");
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            cboTargetCompany.DataSource = SqlExecuter.Query("GetCodeList", "00001", param);
        }


        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
		{
            // TODO : 화면에서 사용할 이벤트 추가
            cboFromProcess.EditValueChanged += CboFromProcess_EditValueChanged;
            cboToProcess.EditValueChanged += CboToProcess_EditValueChanged;
        }

        private void CboFromProcess_EditValueChanged(object sender, EventArgs e)
        {
            var values = Conditions.GetValues();

            // 샘플라우팅일때 To공정 콤보박스 바인딩 추가 -> 유태근 2020-03-06
            if (string.IsNullOrWhiteSpace(Format.GetString(values["P_PRODUCTDEFID"])))
            {
                string editValue = values["P_LOTID"].ToString();

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("LOTID", editValue);
                param.Add("PROCESSSEGMENTID", cboFromProcess.EditValue);
                param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                cboToProcess.DataSource = SqlExecuter.Query("GetProcessPathOfSample", "10001", param);
                cboToProcess.EditValue = null;
                cboResource.DataSource = null;
            }
            else if (string.IsNullOrWhiteSpace(Format.GetString(values["P_LOTID"])))
            {
                string editValue = values["P_PRODUCTDEFID"].ToString();
                if (!editValue.Contains("|"))
                {
                    return;
                }
                string productDefId = editValue.Split('|')[0];
                string productDefVersion = editValue.Split('|')[1];

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("PRODUCTDEFID", productDefId);
                param.Add("PRODUCTDEFVERSION", productDefVersion);
                param.Add("PROCESSSEGMENTID", cboFromProcess.EditValue);
                param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                cboToProcess.DataSource = SqlExecuter.Query("GetProcessPathOfProduct", "10001", param);
                cboToProcess.EditValue = null;
                cboResource.DataSource = null;
            }
        }

        private void CboToProcess_EditValueChanged(object sender, EventArgs e)
        {
            var values = Conditions.GetValues();

            // 샘플라우팅일때 인계자원 콤보박스 바인딩 추가 -> 유태근 2020-03-06
            if (string.IsNullOrWhiteSpace(Format.GetString(values["P_PRODUCTDEFID"])))
            {
                string editValue = values["P_LOTID"].ToString();

                if (cboToProcess.GetSelectedDataRow() == null) return;

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                param.Add("PROCESSDEFID", editValue);
                param.Add("PROCESSDEFVERSION", "*");
                param.Add("PLANTID", UserInfo.Current.Plant);
                param.Add("PROCESSSEGMENTID", (cboToProcess.GetSelectedDataRow() as DataRowView).Row["PROCESSSEGMENTID"]);
                param.Add("PROCESSSEGMENTVERSION", (cboToProcess.GetSelectedDataRow() as DataRowView).Row["PROCESSSEGMENTVERSION"]);
                param.Add("RESOURCETYPE", "Resource");
                DataTable dataTable = SqlExecuter.Query("GetTransitAreaList", "10051", param);
                cboResource.DataSource = dataTable;
                cboResource.EditValue = null;
                if (dataTable.Rows.Count == 2)
                {
                    cboResource.ItemIndex = 1;
                }
            }
            else if (string.IsNullOrWhiteSpace(Format.GetString(values["P_LOTID"])))
            {
                string editValue = values["P_PRODUCTDEFID"].ToString();
                if (!editValue.Contains("|"))
                {
                    return;
                }
                if (cboToProcess.GetSelectedDataRow() == null)
                    return;

                string productDefId = editValue.Split('|')[0];
                string productDefVersion = editValue.Split('|')[1];

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                param.Add("PLANTID", UserInfo.Current.Plant);
                param.Add("PRODUCTDEFID", productDefId);
                param.Add("PRODUCTDEFVERSION", productDefVersion);
                param.Add("PROCESSSEGMENTID", (cboToProcess.GetSelectedDataRow() as DataRowView).Row["PROCESSSEGMENTID"]);
                param.Add("PROCESSSEGMENTVERSION", (cboToProcess.GetSelectedDataRow() as DataRowView).Row["PROCESSSEGMENTVERSION"]);
                param.Add("RESOURCETYPE", "Resource");
                DataTable dataTable = SqlExecuter.Query("GetTransitAreaList", "10041", param);
                cboResource.DataSource = dataTable;
                cboResource.EditValue = null;
                if (dataTable.Rows.Count == 2)
                {
                    cboResource.ItemIndex = 1;
                }
            }
        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
		{
			base.OnToolbarSaveClick();

            // 재공실사 진행 여부 체크
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            param.Add("PLANTID", UserInfo.Current.Plant);

            DataTable isWipSurveyResult = SqlExecuter.Query("GetPlantIsWipSurvey", "10001", param);

            if (isWipSurveyResult.Rows.Count > 0)
            {
                DataRow row = isWipSurveyResult.AsEnumerable().FirstOrDefault();

                string isWipSurvey = Format.GetString(row["ISWIPSURVEY"]);

                if (isWipSurvey == "Y")
                {
                    // 재공실사가 진행 중 입니다. {0}을 진행할 수 없습니다.
                    ShowMessage("PLANTINWIPSURVEY", Language.Get(string.Join("_", "MENU", MenuId)));

                    return;
                }
            }

            // TODO : 저장 Rule 변경
            DataTable checkedLots = grdLotList.View.GetCheckedRows();
            if (checkedLots.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }

            if (string.IsNullOrEmpty((string)cboToProcess.EditValue))
            {
                // To 공정을 선택하여 주십시오.
                throw MessageException.Create("NoToProcessSegId");
            }

            if (string.IsNullOrEmpty((string)cboResource.EditValue))
            {
                // 인계 자원을 선택하시기 바랍니다.
                throw MessageException.Create("SelectTransitResource");
            }

            if (string.IsNullOrEmpty((string)cboTargetCompany.EditValue))
            {
                // 대상 법인을 선택하여 주십시오
                throw MessageException.Create("SelectTargetCompany");
            }

            // 유태근 2020-03-06 -> 행이 두개이상이라면 샘플라우팅인지 검사
            if (checkedLots.Rows.Count >= 2)
            {
                foreach (DataRow row in checkedLots.Rows)
                {
                    if (row["PROCESSDEFID"].Equals(row["LOTID"]))
                    {
                        throw MessageException.Create("샘플라우팅인 Lot은 공정실적처리를 한개의 Lot씩 진행할 수 있습니다.");
                        // SampleRoutingIsOnlyOneProcessResult
                    }
                }
            }

            //if (!productDefIdVersion.Contains("|"))
            //{
            //    // 품목 선택은 필수입니다.
            //    throw MessageException.Create("RequiredProductDefId");
            //}

            var values = Conditions.GetValues();
            string productDefIdVersion = values["P_PRODUCTDEFID"].ToString();

            string productDefId = "";
            string productDefVersion = "";

            if (productDefIdVersion.Contains("|"))
            {
                productDefId = productDefIdVersion.Split('|')[0];
                productDefVersion = productDefIdVersion.Split('|')[1];
            }     

            MessageWorker worker = new MessageWorker("SaveProcessResultCreate");
            worker.SetBody(new MessageBody()
            {
                { "PRODUCTDEFID", productDefId },
                { "PRODUCTDEFVERSION", productDefVersion },
                { "FROMPROCESSSEGMENTID", cboFromProcess.EditValue },
                { "TOPROCESSSEGMENTID", cboToProcess.EditValue },
                { "TRANSITRESOURCE", cboResource.EditValue },
                { "TARGETCOMPANY", cboTargetCompany.EditValue },
                { "LIST", checkedLots }
            });

            worker.Execute();
            ShowMessage("SuccedSave");
        }

        #endregion

        #region 검색

        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
		{
			await base.OnSearchAsync();

            if(string.IsNullOrEmpty((string)cboFromProcess.EditValue))
            {
                // From 공정을 선택하여 주십시오.
                throw MessageException.Create("NoFromProcessSegId");
            }

            var values = Conditions.GetValues();
            values.Add("P_PROCESSSEGMENTID", cboFromProcess.EditValue);
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            if (string.IsNullOrEmpty((string)values["P_LOTID"]) && string.IsNullOrEmpty((string)values["P_PRODUCTDEFID"]))
            {
                // Lot No와 품목 ID중 하나는 필수로 입력해야합니다.
                throw MessageException.Create("RequiredInputLotOrProduct");
            }

            DataTable dt = await SqlExecuter.QueryAsync("SelectWIPList", "10003", values);
            if (dt.Rows.Count < 1)
            {
                // 조회할 데이터가 없습니다.
                ShowMessage("NoSelectData");
            }
            grdLotList.DataSource = dt;
		}

		/// <summary>
		/// 조회조건 항목을 추가한다.
		/// </summary>
		protected override void InitializeCondition()
		{
			base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            // Lot
            CommonFunction.AddConditionSampleLotPopup("P_LOTID", 0.1, false, Conditions, false, false);
            // 품목
            AddConditionProductPopup("P_PRODUCTDEFID", 0.2, false, Conditions);
            // 작업장
            CommonFunction.AddConditionAreaPopup("P_AREAID", 1.5, false, Conditions);
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            SmartSelectPopupEdit lotPopup = Conditions.GetControl<SmartSelectPopupEdit>("P_LOTID");
            SmartSelectPopupEdit productPopup = Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID");

            lotPopup.EditValueChanged += (s, e) =>
            {
                if (!string.IsNullOrWhiteSpace(Format.GetString(lotPopup.EditValue)))
                {
                    productPopup.Text = null;
                    productPopup.SetValue(null);
                    cboToProcess.DataSource = null;
                    cboResource.DataSource = null;
                    RefreshCboFromProcess(Format.GetString(lotPopup.EditValue));
                }
            };

            productPopup.EditValueChanged += (s, e) =>
            {
                if (!string.IsNullOrWhiteSpace(Format.GetString(productPopup.EditValue)))
                {
                    lotPopup.Text = null;
                    lotPopup.SetValue(null);
                    cboToProcess.DataSource = null;
                    cboResource.DataSource = null;
                }
            };
        }

        /// <summary>
        /// 품목코드 조회 팝업
        /// </summary>
        /// <param name="id"></param>
        /// <param name="position"></param>
        /// <param name="isMultiSelect"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        private ConditionCollection AddConditionProductPopup(string id, double position, bool isMultiSelect, ConditionCollection conditions)
        {
            // SelectPopup 항목 추가
            var conditionProductId = conditions.AddSelectPopup(id, new SqlQuery("GetProductDefinitionList", "10004"), "PRODUCTDEFNAME", "PRODUCTDEF")
                .SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupLayoutForm(1000, 800)
                .SetLabel("PRODUCTDEFID")
                .SetPosition(position)
                //.SetValidationIsRequired()
                .SetPopupApplySelection((selectedRows, gridRow) => {
                    // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
                    // dataGridRow : 현재 Focus가 있는 그리드의 DataRow
                    string productDefId = null;
                    string productDefVersion = null;
                    foreach (DataRow each in selectedRows)
                    {
                        productDefId = each["PRODUCTDEFID"].ToString();
                        productDefVersion = each["PRODUCTDEFVERSION"].ToString();
                    }

                    RefreshCboFromProcess(productDefId, productDefVersion);
                });

            // 복수 선택 여부에 따른 Result Count 지정
            if (isMultiSelect)
            {
                conditionProductId.SetPopupResultCount(0);
            }
            else
            {
                conditionProductId.SetPopupResultCount(1);
            }

            // 팝업에서 사용되는 검색조건 (품목코드/명)
            conditionProductId.Conditions.AddTextBox("TXTPRODUCTDEFNAME");
            //제품 구분 추가 2019.08.14 배선용, 노석안 대리 요청
            conditionProductId.Conditions.AddComboBox("PRODUCTDIVISION", 
                    new SqlQuery("GetTypeList", "10001", $"CODECLASSID=ProductDivision2", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                .SetDefault("Product");

            // 팝업 그리드에서 보여줄 컬럼 정의
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 130)                // 품목 ID
                .SetTextAlignment(TextAlignment.Center);
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 90)            // 품목 Rev
                .SetTextAlignment(TextAlignment.Center);
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 250);             // 품목명
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEF", 0).SetIsHidden();     // 품목 ID + Ver
            conditionProductId.GridColumns.AddComboBoxColumn("PRODUCTDEFTYPE", 90               // 품목유형구분
                , new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductDefType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            conditionProductId.GridColumns.AddComboBoxColumn("PRODUCTIONTYPE", 90               // 생산구분
                , new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            return conditions;
        }

        private void RefreshCboFromProcess(string productDefId, string productDefVersion)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("PRODUCTDEFID", productDefId);
            param.Add("PRODUCTDEFVERSION", productDefVersion);
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            cboFromProcess.DataSource = SqlExecuter.Query("GetProcessPathOfProduct", "10001", param);
        }

        // 샘플라우팅일때 콤보박스 쿼리 추가 -> 유태근 2020-03-06
        private void RefreshCboFromProcess(string lotId)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LOTID", lotId);
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            cboFromProcess.DataSource = SqlExecuter.Query("GetProcessPathOfSample", "10001", param);
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
		}

        #endregion

        #region Private Function

        // TODO : 화면에서 사용할 내부 함수 추가

        #endregion
    }
}
