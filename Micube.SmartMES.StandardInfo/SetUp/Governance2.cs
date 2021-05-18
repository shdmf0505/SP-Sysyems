#region using

using DevExpress.XtraEditors.Controls;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion using

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프로그램명 : 기준정보 > 사양진행현황 > 모델등록 & 진행현황
    /// 업 무 설명 :
    /// 생  성  자 : 윤성원
    /// 생  성  일 : 2019-06-27
    /// 수정 이 력 : 2019-11-29 정승원 설계변경
    ///                  2020-01-02 장선미 삭제로직 추가
    ///              2021-04-07 전우성 코드 재정립, 작업구분, 생산구분 readonly, 생산구분 필수 제거
    ///
    /// </summary>
	public partial class Governance2 : SmartConditionManualBaseForm
    {
        #region Local Variables

        /// <summary>
        /// 거버넌스 NO
        /// </summary>
        private string _governanceNo = string.Empty;

        /// <summary>
        /// 거버넌스 Type
        /// </summary>
		private string _governanceType = string.Empty;

        /// <summary>
        ///
        /// </summary>
		private DataTable _inputDt = new DataTable();

        #endregion Local Variables

        #region 생성자

        public Governance2()
        {
            InitializeComponent();
        }

        #endregion 생성자

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 컨텐츠 영역 초기화
        /// </summary>
        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeGrid();
            InitializeEvent();
            InitializeControl();
        }

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            grdMain.GridButtonItem = GridButtonItem.Export | GridButtonItem.Delete;
            grdMain.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            //접수일
            grdMain.View.AddTextBoxColumn("RECEPTDATE", 100).SetTextAlignment(TextAlignment.Center);
            //고객사
            grdMain.View.AddTextBoxColumn("CUSTOMERID", 100).SetIsHidden();
            //고객명
            grdMain.View.AddTextBoxColumn("CUSTOMERNAME", 150);
            //고객사품목
            grdMain.View.AddTextBoxColumn("CUSTOMERITEMID", 110).SetTextAlignment(TextAlignment.Center);
            //고객사버전
            grdMain.View.AddTextBoxColumn("CUSTOMERITEMVERSION", 110).SetTextAlignment(TextAlignment.Center).SetLabel("CUSTOMERVERSION");
            //제품Type
            grdMain.View.AddTextBoxColumn("PRODUCTTYPE", 60).SetTextAlignment(TextAlignment.Center);
            // 층수
            grdMain.View.AddTextBoxColumn("LAYER", 60).SetTextAlignment(TextAlignment.Center);
            //작업구분
            grdMain.View.AddComboBoxColumn("JOBTYPE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=JobType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                        .SetTextAlignment(TextAlignment.Center)
                        .SetLabel("OSPETCTYPENAME");
            //생산구분
            grdMain.View.AddComboBoxColumn("PRODUCTIONTYPE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                        .SetTextAlignment(TextAlignment.Center);
            //모델ID
            grdMain.View.AddTextBoxColumn("MODELNO", 150);
            //품목코드
            grdMain.View.AddTextBoxColumn("PRODUCTDEFID", 80).SetTextAlignment(TextAlignment.Center);
            //품목버전
            grdMain.View.AddTextBoxColumn("PRODUCTDEFVERSION", 60).SetTextAlignment(TextAlignment.Center);
            //품목명
            grdMain.View.AddTextBoxColumn("ITEMNAME", 200);
            // 제품등급
            grdMain.View.AddTextBoxColumn("PRODUCTRATING", 60).SetTextAlignment(TextAlignment.Center);
            //사양작업
            grdMain.View.AddComboBoxColumn("SPECWORKTYPE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=GovernanceState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                        .SetTextAlignment(TextAlignment.Center)
                        .SetLabel("SPECWORKTYPENAME");
            //Holding
            grdMain.View.AddTextBoxColumn("ISHOLDING", 60).SetTextAlignment(TextAlignment.Center);
            //PNL SIZE
            grdMain.View.AddTextBoxColumn("PNLSIZE", 100);
            //합수
            grdMain.View.AddTextBoxColumn("ARRAY", 50);
            //산출수
            grdMain.View.AddTextBoxColumn("CALCULATION", 50);
            // 주제조공장
            grdMain.View.AddTextBoxColumn("MAINFACTORY", 80).SetTextAlignment(TextAlignment.Center);
            // 비고
            grdMain.View.AddTextBoxColumn("REMARK", 150);
            //
            grdMain.View.AddTextBoxColumn("RECEPTDATE_YYYYMMDD", 130).SetIsHidden();
            //승인일
            grdMain.View.AddTextBoxColumn("APPROVEDATE", 130).SetTextAlignment(TextAlignment.Center);
            //접수번호
            grdMain.View.AddTextBoxColumn("GOVERNANCENO", 130).SetTextAlignment(TextAlignment.Center);
            //거버넌스 타입
            grdMain.View.AddTextBoxColumn("GOVERNANCETYPE", 100).SetIsHidden();
            //거버넌스STATUS
            grdMain.View.AddTextBoxColumn("GOVERNANCESTATE").SetIsHidden();
            //사양담당
            grdMain.View.AddTextBoxColumn("SPECOWNERID", 80).SetTextAlignment(TextAlignment.Center).SetIsHidden();
            //사양 담당자
            grdMain.View.AddTextBoxColumn("SPECOWNERNAME", 80).SetTextAlignment(TextAlignment.Center);
            //영업담당
            grdMain.View.AddTextBoxColumn("SALESOWNERNAME", 80).SetTextAlignment(TextAlignment.Center);
            //비고
            grdMain.View.AddTextBoxColumn("COMMENT", 150);
            //수정자
            grdMain.View.AddTextBoxColumn("MODIFIER", 80).SetTextAlignment(TextAlignment.Center);
            //수정일
            grdMain.View.AddTextBoxColumn("MODIFIEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetTextAlignment(TextAlignment.Center);

            grdMain.View.PopulateColumns();
            grdMain.View.SetIsReadOnly();
        }

        /// <summary>
        /// 컨트롤 초기화
        /// </summary>
        private void InitializeControl()
        {
            //접수일
            txtReceptDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
            txtReceptDate.ReadOnly = true;
            txtReceptDate.Tag = "RECEPTDATE_YYYYMMDD";
            txtReceptDate.Properties.Mask.EditMask = "yyyy-MM-dd";

            // 작업구분
            ComboSetting(cboGovernanceType, "JOBTYPE", "JobType", true);

            // 생산구분
            ComboSetting(cboProductionType, "PRODUCTIONTYPE", "ProductionType", true);

            // 해외이관
            ComboSetting(cboOverSeas, "OVERSEASTRANS", "OverSeasTrans");

            //HOLDING
            ComboSetting(cboIsHolding, "ISHOLDING", "YesNo");

            //고객사버전
            lblCustomerVersion.Visible = false;
            txtCustomerVersion.Visible = false;

            //모델ID
            txtModelId.Text = string.Empty;
            txtModelId.Tag = "MODELNO";

            //CAM 담당
            lblCamOwner.Visible = false;
            popCamOwner.Visible = false;

            //CAM 작업
            lblCamState.Visible = false;
            cboCamState.Visible = false;

            //비고
            txtComment.Text = string.Empty;
            txtComment.Tag = "COMMENT";

            #region 고객사

            ConditionItemSelectPopup popup = new ConditionItemSelectPopup();
            popup.SetPopupLayoutForm(600, 600, FormBorderStyle.FixedDialog);
            popup.SetPopupLayout("SELECTCUSTOMERID", PopupButtonStyles.Ok_Cancel, true, false);
            popup.Id = "CUSTOMERID";
            popup.SearchQuery = new SqlQuery("GetCustomerList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}");
            popup.IsMultiGrid = false;
            popup.DisplayFieldName = "CUSTOMERNAME";
            popup.ValueFieldName = "CUSTOMERID";
            popup.LanguageKey = "CUSTOMER";
            popup.SetValidationIsRequired();

            popup.Conditions.AddTextBox("TXTCUSTOMERID");

            popup.GridColumns.AddTextBoxColumn("CUSTOMERID", 150);
            popup.GridColumns.AddTextBoxColumn("CUSTOMERNAME", 200);

            popCustomerId.SelectPopupCondition = popup;
            popCustomerId.Tag = "CUSTOMERID";

            #endregion 고객사

            #region 품목코드

            popup = new ConditionItemSelectPopup();
            popup.SetPopupLayoutForm(800, 800, FormBorderStyle.FixedDialog);
            popup.SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false);
            popup.Id = "ITEMID";
            popup.SearchQuery = new SqlQuery("GetProductDefIdJobType", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}");
            popup.IsMultiGrid = false;
            popup.DisplayFieldName = "ITEMID";
            popup.ValueFieldName = "ITEMID";
            popup.LanguageKey = "ITEMID";
            popup.SetPopupApplySelection((selectRow, gridRow) =>
            {
                DataRow row = selectRow.FirstOrDefault();
                txtItemVersion.Text = row["ITEMVERSION"].ToString();
                txtItemName.Text = row["ITEMNAME"].ToString();
                cboProductionType.EditValue = row["PRODUCTIONTYPE"];
                cboGovernanceType.EditValue = row["WORKCLASS"];
            });

            popup.Conditions.AddTextBox("TXTITEM").SetLabel("PRODUCT");

            popup.GridColumns.AddTextBoxColumn("ITEMID", 150);
            popup.GridColumns.AddTextBoxColumn("ITEMVERSION", 50);
            popup.GridColumns.AddTextBoxColumn("ITEMNAME", 200);

            popItemId.SelectPopupCondition = popup;
            popItemId.Tag = "PRODUCTDEFID";

            //품목버전
            txtItemVersion.ReadOnly = true;
            txtItemVersion.Tag = "PRODUCTDEFVERSION";

            //품목명
            txtItemName.ReadOnly = true;
            txtItemName.Tag = "ITEMNAME";

            #endregion 품목코드

            #region 사양담당

            popup = new ConditionItemSelectPopup();
            popup.SetPopupLayoutForm(600, 600, FormBorderStyle.FixedDialog);
            popup.SetPopupLayout("USERLIST", PopupButtonStyles.Ok_Cancel, true, false);
            popup.Id = "USER";
            popup.SearchQuery = new SqlQuery("SelectUserGroupUserSearch", "10001", $"P_USERGROUPID={"SPECOWNER"}");
            popup.IsMultiGrid = false;
            popup.DisplayFieldName = "USERNAME";
            popup.ValueFieldName = "USERID";
            popup.LanguageKey = "USER";

            popup.Conditions.AddTextBox("USERIDNAME");

            popup.GridColumns.AddTextBoxColumn("USERID", 150);
            popup.GridColumns.AddTextBoxColumn("USERNAME", 200);

            popSpecOwner.SelectPopupCondition = popup;
            popSpecOwner.Tag = "SPECOWNERID";

            #endregion 사양담당

            #region 영업담당

            popup = new ConditionItemSelectPopup();
            popup.SetPopupLayoutForm(600, 600, FormBorderStyle.FixedDialog);
            popup.SetPopupLayout("USERLIST", PopupButtonStyles.Ok_Cancel, true, false);
            popup.Id = "USER";
            popup.SearchQuery = new SqlQuery("SelectUserGroupUserSearch", "10001", $"P_USERGROUPID={"SALESOWNER"}");
            popup.IsMultiGrid = false;
            popup.DisplayFieldName = "USERNAME";
            popup.ValueFieldName = "USERID";
            popup.LanguageKey = "USER";

            popup.Conditions.AddTextBox("USERIDNAME");

            popup.GridColumns.AddTextBoxColumn("USERID", 150);
            popup.GridColumns.AddTextBoxColumn("USERNAME", 200);

            popSalesOwner.SelectPopupCondition = popup;
            popSalesOwner.Tag = "SALESOWNERID";

            #endregion 영업담당
        }

        #endregion 컨텐츠 영역 초기화

        #region 이벤트

        private void InitializeEvent()
        {
            grdMain.View.FocusedRowChanged += View_FocusedRowChanged;

            grdMain.View.RowStyle += (s, e) =>
            {
                if (grdMain.View.GetRowCellValue(e.RowHandle, "ISHOLDING") != null && grdMain.View.GetRowCellValue(e.RowHandle, "ISHOLDING").Equals("Y"))
                {
                    e.HighPriority = true;
                    e.Appearance.ForeColor = Color.White;
                    e.Appearance.BackColor = Color.Red;
                }
            };

            // 품목 팝업 VALUE 변경 시 이벤트
            popItemId.EditValueChanged += (s, e) =>
            {
                if (string.IsNullOrEmpty(Format.GetString(popItemId.EditValue)))
                {
                    popItemId.Tag = "PRODUCTDEFID";
                    txtItemVersion.Text = string.Empty;
                    txtItemName.Text = string.Empty;
                }
            };

            popItemId.EditValueChanged += (s, e) =>
            {
                if (Format.GetFullTrimString(popItemId.EditValue).Equals(string.Empty))
                {
                    txtItemVersion.Text = string.Empty;
                    txtItemName.Text = string.Empty;
                }
            };

            popItemId.ButtonClick += (s, e) =>
            {
                if (e.Button.Kind.Equals(ButtonPredefines.Clear))
                {
                    txtItemVersion.Text = string.Empty;
                    txtItemName.Text = string.Empty;
                }
            };
        }

        /// <summary>
        /// row 포커스 변경 시 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grdMain.View.FocusedRowHandle < 0 || e.PrevFocusedRowHandle < 0)
            {
                return;
            }

            DataRow selectRow = grdMain.View.GetFocusedDataRow();
            DataTable compareDt = new DataTable();
            compareDt = GetRowDataByDataTable(compareDt, grdMain.View.GetDataRow(e.PrevFocusedRowHandle));

            bool isChanged = CheckDifferentInputData(_inputDt, compareDt, out DialogResult result);

            if (isChanged || result.Equals(DialogResult.None))
            {
                FocusedRowDataBind(selectRow);
            }
            else
            {
                grdMain.View.FocusedRowChanged -= View_FocusedRowChanged;
                grdMain.View.SelectRow(e.PrevFocusedRowHandle);
                grdMain.View.FocusedRowHandle = e.PrevFocusedRowHandle;
                grdMain.View.FocusedRowChanged += View_FocusedRowChanged;
            }

            _inputDt = GetRowDataByDataTable(_inputDt, selectRow);
        }

        #endregion 이벤트

        #region 툴바

        /// <summary>
        /// 저장 버튼 클릭
        /// </summary>
        /// <returns></returns>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            DataTable dt = new DataTable();
            dt.Columns.Add("REQUESTDATE", typeof(object));
            dt.Columns.Add("INVENTORYCATEGORY", typeof(object));
            dt.Columns.Add("JOBTYPE", typeof(object));
            dt.Columns.Add("PRODUCTIONTYPE", typeof(object));
            dt.Columns.Add("CUSTOMERID", typeof(object));
            dt.Columns.Add("CUSTOMERVERSION", typeof(object));
            dt.Columns.Add("MODELID", typeof(object));
            dt.Columns.Add("ITEMID", typeof(object));
            dt.Columns.Add("ITEMVERSION", typeof(object));
            dt.Columns.Add("ITEMNAME", typeof(object));
            dt.Columns.Add("ISHOLDING", typeof(object));
            dt.Columns.Add("SPECOWNERID", typeof(object));
            dt.Columns.Add("CAMOWNERID", typeof(object));
            dt.Columns.Add("SALESOWNERID", typeof(object));
            dt.Columns.Add("COMMENT", typeof(object));
            dt.Columns.Add("CAMSTATE", typeof(object));
            dt.Columns.Add("GOVERNANCENO", typeof(object));
            dt.Columns.Add("GOVERNANCETYPE", typeof(object));
            dt.Columns.Add("OVERSEASTRANS", typeof(object));

            if (string.IsNullOrEmpty(txtModelId.Text))
            {
                ShowMessage("REQUIREDINPUTMODELNO"); //모델은 필수 입력입니다.
                return;
            }

            if (string.IsNullOrEmpty(Format.GetString(cboProductionType.EditValue)))
            {
                throw MessageException.Create("REQUIREDINPUTPRODUCTIONTYPE"); //생산구분은 필수 입력입니다.
            }

            if (popCustomerId.GetValue() == null || string.IsNullOrEmpty(popCustomerId.GetValue().ToString()))
            {
                ShowMessage("REQUIREDINPUTCUSTOMER"); //고객사는 필수 입력입니다.
                return;
            }

            if (Format.GetString(cboGovernanceType.GetDataValue()).Equals("RunningChange"))
            {
                ShowMessage("NOTINSERTRUNNINGCHANGE"); //R/C 등록은 'RunningChange 등록' 화면에서 가능합니다.
                return;
            }

            DataRow row = dt.NewRow();
            row["REQUESTDATE"] = txtReceptDate.Text;
            row["JOBTYPE"] = cboGovernanceType.GetDataValue();
            row["PRODUCTIONTYPE"] = cboProductionType.GetDataValue();
            row["CUSTOMERID"] = popCustomerId.GetValue().ToString().Contains("=") ? popCustomerId.GetValue().ToString().Split('=')[0] : popCustomerId.GetValue();
            row["CUSTOMERVERSION"] = txtCustomerVersion.Text;
            row["MODELID"] = txtModelId.Text;
            row["ITEMID"] = popItemId.GetValue().ToString().Contains("=") ? popItemId.GetValue().ToString().Split('=')[0] : popItemId.GetValue();
            row["ITEMVERSION"] = txtItemVersion.Text;
            row["ITEMNAME"] = txtItemName.Text;
            row["ISHOLDING"] = cboIsHolding.GetDataValue();
            row["SPECOWNERID"] = popSpecOwner.GetValue().ToString().Contains("=") ? popSpecOwner.GetValue().ToString().Split('=')[0] : popSpecOwner.GetValue();
            row["CAMOWNERID"] = popCamOwner.GetValue().ToString().Contains("=") ? popCamOwner.GetValue().ToString().Split('=')[0] : popCamOwner.GetValue();
            row["SALESOWNERID"] = popSalesOwner.GetValue().ToString().Contains("=") ? popSalesOwner.GetValue().ToString().Split('=')[0] : popSalesOwner.GetValue();
            row["COMMENT"] = txtComment.Text;
            row["CAMSTATE"] = cboCamState.GetDataValue();
            row["GOVERNANCENO"] = _governanceNo;
            row["GOVERNANCETYPE"] = "NewRequest";
            row["OVERSEASTRANS"] = cboOverSeas.GetDataValue();

            dt.Rows.Add(row);

            MessageWorker worker = new MessageWorker("SaveGovernance");
            worker.SetBody(new MessageBody()
            {
                { "saveState", string.IsNullOrEmpty(_governanceNo) && string.IsNullOrEmpty(_governanceType) ? "added" : "modified" },
                { "requester", UserInfo.Current.Id },
                { "enterpriseId", UserInfo.Current.Enterprise },
                { "plantId", UserInfo.Current.Plant },
                { "governanceList", dt },
                { "governanceDeleteList", grdMain.GetChangesDeleted() }
            });

            worker.Execute();
        }

        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            base.OnToolbarClick(sender, e);

            if ((sender as SmartButton).Name.Equals("Approval"))
            {
                DataRow selectRow = grdMain.View.GetFocusedDataRow();
                if (selectRow == null)
                {
                    return;
                }

                if (string.IsNullOrEmpty(Format.GetString(selectRow["GOVERNANCENO"])) || string.IsNullOrEmpty(Format.GetString(selectRow["GOVERNANCETYPE"])))
                {
                    ShowMessage("FIRSTSETMODELNO"); //모델 등록을 먼저 진행하세요.
                    return;
                }

                if (string.IsNullOrEmpty(Format.GetString(selectRow["PRODUCTDEFID"])) || string.IsNullOrEmpty(Format.GetString(selectRow["PRODUCTDEFVERSION"])))
                {
                    ShowMessage("FIRSTSETITEMMASTER"); //품목 매핑을 먼저 진행하세요.
                    return;
                }

                if (string.IsNullOrEmpty(Format.GetString(selectRow["SPECOWNERID"])) || string.IsNullOrEmpty(Format.GetString(selectRow["SPECOWNERNAME"])))
                {
                    ShowMessage("SETSPECOWNER"); //사양담당자를 지정하세요.
                    return;
                }

                if (Format.GetString(selectRow["SPECWORKTYPE"]).Equals("Working") || Format.GetString(selectRow["SPECWORKTYPE"]).Equals("Approved"))
                {
                    ShowMessage("AlreadyCARReceiptCompleted"); //이미 접수완료된 건입니다.
                    return;
                }

                if (Format.GetString(selectRow["SPECWORKTYPE"]).Equals("Input"))
                {
                    ShowMessage("ALREADYINPUT"); //이미 투입된 품목입니다.
                    return;
                }

                SetApproveListPopup popup = new SetApproveListPopup(Format.GetString(selectRow["GOVERNANCENO"]),
                                                                    Format.GetString(selectRow["GOVERNANCETYPE"]),
                                                                    Format.GetString(selectRow["SPECOWNERID"]),
                                                                    Format.GetString(selectRow["SPECOWNERNAME"]));
                popup.ShowDialog();

                SendKeys.Send("{F5}");
            }
            else if ((sender as SmartButton).Name.Equals("Initialization"))
            {
                _governanceNo = string.Empty;
                _governanceType = string.Empty;

                ClearControls();
            }
        }

        #endregion 툴바

        #region 검색

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();
            grdMain.View.ClearDatas();
            ClearControls();

            var values = Conditions.GetValues();
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            values.Add("PLANTID", UserInfo.Current.Plant);
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            if (await QueryAsync("SelectGovernanceList", "10002", values) is DataTable dt)
            {
                if (dt.Rows.Count.Equals(0))
                {
                    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                    return;
                }

                grdMain.DataSource = dt;

                int iHandle = grdMain.View.GetRowHandleByValue("GOVERNANCENO", _governanceNo);

                grdMain.View.FocusedRowChanged -= View_FocusedRowChanged;
                grdMain.View.FocusedRowHandle = iHandle <= 0 ? 0 : iHandle;
                grdMain.View.SelectRow(grdMain.View.FocusedRowHandle);
                grdMain.View.FocusedRowChanged += View_FocusedRowChanged;

                DataRow row = grdMain.View.GetFocusedDataRow();
                FocusedRowDataBind(row);
                _inputDt = GetRowDataByDataTable(_inputDt, row);
            }
        }

        /// <summary>
        /// 조회 조건 초기화
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            #region 고객사

            var condition = Conditions.AddSelectPopup("P_CUSTOMER", new SqlQuery("GetCustomerList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "CUSTOMERNAME", "CUSTOMERID")
                                      .SetPopupLayout("SELECTCUSTOMERID", PopupButtonStyles.Ok_Cancel, true, false)
                                      .SetPopupLayoutForm(800, 800)
                                      .SetPopupAutoFillColumns("CUSTOMERNAME")
                                      .SetLabel("CUSTOMERID")
                                      .SetPosition(2.5)
                                      .SetPopupResultCount(0);

            condition.Conditions.AddTextBox("TXTCUSTOMERID");

            condition.GridColumns.AddTextBoxColumn("CUSTOMERID", 150);
            condition.GridColumns.AddTextBoxColumn("CUSTOMERNAME", 200);

            #endregion 고객사

            #region 품목코드

            condition = Conditions.AddSelectPopup("P_ITEMID", new SqlQuery("GetItemMasterList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "ITEM", "ITEM")
                                  .SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)
                                  .SetPopupLayoutForm(800, 800)
                                  .SetPopupAutoFillColumns("ITEMNAME")
                                  .SetLabel("ITEMID")
                                  .SetPosition(4.2)
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

            condition.Conditions.AddTextBox("TXTITEM").SetLabel("PRODUCT");

            condition.GridColumns.AddTextBoxColumn("ITEMID", 150);
            condition.GridColumns.AddTextBoxColumn("ITEMNAME", 200);
            condition.GridColumns.AddTextBoxColumn("ITEMVERSION", 100);

            #endregion 품목코드

            #region 사양담당

            condition = Conditions.AddSelectPopup("P_SPECOWNER", new SqlQuery("SelectUserGroupUserSearch", "10001", $"P_USERGROUPID={"SPECOWNER"}"), "USERNAME", "USERID")
                                  .SetPopupLayout("SELECTSPECOWNER", PopupButtonStyles.Ok_Cancel, true, false)
                                  .SetPopupLayoutForm(800, 800)
                                  .SetPopupAutoFillColumns("DEPARTMENT")
                                  .SetLabel("USERID")
                                  .SetPosition(9.5)
                                  .SetPopupResultCount(0);

            condition.Conditions.AddTextBox("USERIDNAME");

            condition.GridColumns.AddTextBoxColumn("USERID", 150);
            condition.GridColumns.AddTextBoxColumn("USERNAME", 200);
            condition.GridColumns.AddTextBoxColumn("DEPARTMENT", 200);

            #endregion 사양담당
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

            Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").ReadOnly = true;

            Conditions.GetControl<SmartSelectPopupEdit>("P_ITEMID").EditValueChanged += (s, e) =>
            {
                SmartSelectPopupEdit PopProdutid = s as SmartSelectPopupEdit;

                if (Format.GetFullTrimString(PopProdutid.EditValue).Equals(string.Empty))
                {
                    Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").EditValue = string.Empty;
                }
            };
        }

        #endregion 검색

        #region Private Function

        /// <summary>
        /// 컨트롤 초기화
        /// </summary>
        private void ClearControls()
        {
            txtReceptDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
            cboGovernanceType.EditValue = string.Empty;
            cboProductionType.EditValue = string.Empty;
            popCustomerId.ClearValue();
            txtCustomerVersion.Text = string.Empty;
            txtModelId.Text = string.Empty;
            popItemId.ClearValue();
            txtItemVersion.Text = string.Empty;
            txtItemName.Text = string.Empty;
            popCamOwner.ClearValue();
            popSpecOwner.ClearValue();
            popSalesOwner.ClearValue();
            cboCamState.EditValue = string.Empty;
            cboIsHolding.EditValue = "N";
            txtComment.Text = string.Empty;
        }

        /// <summary>
        /// ROW 데이터 바인트
        /// </summary>
        private bool FocusedRowDataBind(DataRow row)
        {
            if (row == null)
            {
                _governanceNo = string.Empty;
                _governanceType = string.Empty;

                return false;
            }
            else
            {
                RowDataBindInTableControls(row);

                //포커된 ROW의 KEY 값을 넣어줌(저장 상태 체크)
                _governanceNo = Format.GetString(row["GOVERNANCENO"]);
                _governanceType = Format.GetString(row["GOVERNANCETYPE"]);
                return true;
            }
        }

        /// <summary>
        /// SmartSplitTableLayoutPanel 안에 있는 컨트롤에 row데이터 바인드
        /// </summary>
        private void RowDataBindInTableControls(DataRow r)
        {
            string tag = string.Empty;
            foreach (Control ctl in tlpInputInfo.Controls)
            {
                tag = Format.GetString(ctl.Tag);

                if (string.IsNullOrEmpty(Format.GetString(ctl.Tag)))
                {
                    continue;
                }

                switch (ctl.GetType().Name)
                {
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
                            }
                        }
                        else
                        {
                            pop.ClearValue();
                        }
                        break;

                    case "SmartComboBox":
                        (ctl as SmartComboBox).EditValue = r[tag];
                        break;

                    case "SmartTextBox":
                    case "SmartMemoEdit":
                        ctl.Text = r[tag].ToString();
                        break;
                }
            }
        }

        /// <summary>
        /// ComboBox Control 설정
        /// </summary>
        /// <param name="control"></param>
        /// <param name="tag"></param>
        /// <param name="codeClass"></param>
        private void ComboSetting(SmartComboBox control, string tag, string codeClass, bool isReadOnly = false)
        {
            control.DisplayMember = "CODENAME";
            control.ValueMember = "CODEID";
            control.Tag = tag;
            control.ShowHeader = false;
            control.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            control.Properties.ReadOnly = isReadOnly;
            control.DataSource = SqlExecuter.Query("GetCodeList", "00001", new Dictionary<string, object>(){ { "CODECLASSID", codeClass },
                                                                                                             { "LANGUAGETYPE", UserInfo.Current.LanguageType } });
        }

        /// <summary>
        /// 변경된 값 비교
        /// </summary>
        private bool CheckDifferentInputData(DataTable original, DataTable compare, out DialogResult result)
        {
            bool isDifference = false;
            result = DialogResult.None;

            if (original == null || compare == null)
            {
                return isDifference;
            }

            if (original.Rows.Count <= 0 || compare.Rows.Count <= 0)
            {
                return isDifference;
            }

            foreach (DataColumn oCol in original.Columns)
            {
                foreach (DataColumn cCol in compare.Columns)
                {
                    if (oCol.ColumnName.Equals(cCol.ColumnName) && Format.GetString(oCol.Table.Rows[0][oCol.ColumnName]) != Format.GetString(cCol.Table.Rows[0][cCol.ColumnName]))
                    {
                        //입력한 내용이 있으면 현재 작성중인 내용인 모두 삭제됩니다.
                        result = ShowMessageBox("WRITTENBEDELETE", Language.Get("MESSAGEINFO"), MessageBoxButtons.OKCancel);
                        if (result.Equals(DialogResult.OK))
                        {
                            isDifference = true;
                        }
                        break;
                    }
                }

                if (isDifference || result.Equals(DialogResult.Cancel))
                {
                    break;
                }
            }

            return isDifference;
        }

        /// <summary>
        /// 일부 ROW 데이터를 DataTable에 넣기
        /// </summary>
        /// <param name="row"></param>
        private DataTable GetRowDataByDataTable(DataTable dt, DataRow row)
        {
            if (row == null)
            {
                return null;
            }

            dt = row.Table.Clone();

            dt.ImportRow(row);
            dt.AcceptChanges();

            dt.Rows[0]["JOBTYPE"] = cboGovernanceType.GetDataValue();
            dt.Rows[0]["PRODUCTIONTYPE"] = cboProductionType.GetDataValue();
            dt.Rows[0]["CUSTOMERID"] = popCustomerId.GetValue();
            dt.Rows[0]["MODELNO"] = txtModelId.EditValue;
            dt.Rows[0]["CUSTOMERVERSION"] = txtCustomerVersion.EditValue;
            dt.Rows[0]["CAMOWNERID"] = popCamOwner.GetValue();
            dt.Rows[0]["CAMWORKSTATE"] = cboCamState.GetDataValue();
            dt.Rows[0]["SPECOWNERID"] = popSpecOwner.GetValue();
            dt.Rows[0]["SALESOWNERID"] = popSalesOwner.GetValue();
            dt.Rows[0]["COMMENT"] = txtComment.EditValue;
            dt.Rows[0]["ISHOLDING"] = cboIsHolding.EditValue;

            return dt;
        }

        #endregion Private Function
    }
}