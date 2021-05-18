#region using

using DevExpress.XtraLayout.Utils;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion using

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프 로 그 램 명  : 기준정보 > 품목정보 > 품목정보(제조기획)
    /// 업  무  설  명  : 제조기획 팀에서 사용하는 품목정보 리스트
    /// 생    성    자  : 전우성
    /// 생    성    일  : 2021-05-11
    /// 수  정  이  력  :
    ///
    ///
    /// </summary>
    public partial class ProductMasterMarketing : SmartConditionManualBaseForm
    {
        #region Global Variable

        /// <summary>
        /// 저장의 여부 확인
        /// </summary>
        private bool _isSave = false;

        #endregion Global Variable

        #region 생성자

        public ProductMasterMarketing()
        {
            InitializeComponent();
        }

        #endregion 생성자

        #region 컨텐츠 영역 초기화

        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeGrid();
            InitializeEvent();
            InitializeLanguageKey();
            InitializeControler();
        }

        /// <summary>
        /// Controler 초기화
        /// </summary>
        private void InitializeControler()
        {
            txtProductDefId.ReadOnly = true;
            txtProductdefVersion.ReadOnly = true;

            dateYear.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearsGroupView;
            dateYear.Properties.Mask.EditMask = "yyyy";
            dateYear.Properties.Mask.UseMaskAsDisplayFormat = true;
            dateYear.EditValue = DateTime.Now;

            SetConditionVisiblility("P_YEAR", LayoutVisibility.Never);
        }

        /// <summary>
        /// Language Key 초기화
        /// </summary>
        private void InitializeLanguageKey()
        {
            smartLayoutControl1.SetLanguageKey(layoutControlItem3, "YEAR");
            smartLayoutControl1.SetLanguageKey(layoutControlItem4, "PRODUCTDEFID");
            smartLayoutControl1.SetLanguageKey(layoutControlItem5, "PRODUCTDEFVERSION");
            tabMain.SetLanguageKey(xtraTabPage1, "INPUTINFO");
            tabMain.SetLanguageKey(xtraTabPage2, "ALLVIEWS");
            grdMain.LanguageKey = "PRODUCTINFOLIST";
            grdSub.LanguageKey = "MONTHLIST";
            grdHistory.LanguageKey = "LIST";
        }

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            #region 품목정보 List

            grdMain.GridButtonItem = GridButtonItem.Export;
            grdMain.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            grdMain.View.AddTextBoxColumn("MASTERDATACLASSID").SetIsHidden();
            grdMain.View.AddTextBoxColumn("PRODUCTIONTYPE").SetIsHidden();
            grdMain.View.AddTextBoxColumn("ENTERPRISEID").SetIsHidden();
            grdMain.View.AddTextBoxColumn("CUSTOMERID").SetIsHidden();
            grdMain.View.AddTextBoxColumn("PRODUCTTYPE").SetIsHidden();
            grdMain.View.AddTextBoxColumn("LAYER").SetIsHidden();

            grdMain.View.AddTextBoxColumn("PRODUCTDIVISION", 70);
            grdMain.View.AddTextBoxColumn("PRODUCTIONTYPENAME", 70);
            grdMain.View.AddTextBoxColumn("PRODUCTDEFID", 100);
            grdMain.View.AddTextBoxColumn("PRODUCTDEFVERSION", 70);
            grdMain.View.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            grdMain.View.AddTextBoxColumn("COMPANYCLIENT", 150);
            grdMain.View.AddTextBoxColumn("PRODUCTTYPENAME", 70).SetLabel("PRODUCTTYPE");
            grdMain.View.AddTextBoxColumn("LAYERNAME", 70);
            grdMain.View.AddTextBoxColumn("USERLAYER", 70);
            grdMain.View.AddTextBoxColumn("SMT", 70);
            grdMain.View.AddTextBoxColumn("APNNO", 80);
            grdMain.View.AddSpinEditColumn("BLKPNL", 90);
            grdMain.View.AddSpinEditColumn("ARRAYPCSPNL", 110);
            grdMain.View.AddSpinEditColumn("CALCULATION2", 110);
            grdMain.View.AddSpinEditColumn("PNLSIZEXAXIS", 110);
            grdMain.View.AddSpinEditColumn("PNLSIZEYAXIS", 110);
            grdMain.View.AddSpinEditColumn("PNLAREAM2", 100).SetDisplayFormat("{0:N6}");

            grdMain.View.PopulateColumns();
            grdMain.View.SetIsReadOnly();

            grdMain.ShowStatusBar = true;

            #endregion 품목정보 List

            #region 월별 구분 List

            grdSub.GridButtonItem = GridButtonItem.None;
            grdSub.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            grdSub.View.AddTextBoxColumn("ITEMID").SetIsHidden();
            grdSub.View.AddTextBoxColumn("ITEMVERSION").SetIsHidden();
            grdSub.View.AddTextBoxColumn("ENTERPRISEID").SetIsHidden();

            var group = grdSub.View.AddGroupColumn("");

            group.AddTextBoxColumn("PLANDATE", 70).SetLabel("YEARMONTH");

            group = grdSub.View.AddGroupColumn("SITEDIVISION");
            group.AddComboBoxColumn("INPUTFACTORY", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=AllFactory", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                 .SetLabel("INPUT")
                 .SetEmptyItem("", "");

            group.AddComboBoxColumn("FINALFACTORY", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=AllFactory", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                 .SetLabel("FINAL")
                 .SetEmptyItem("", "");

            group.AddComboBoxColumn("DELIVERYFACTORY", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=DeliveryFactory", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                 .SetLabel("DELIVERY")
                 .SetEmptyItem("", "");

            group = grdSub.View.AddGroupColumn("");
            group.AddComboBoxColumn("DIRECTDELIVERY", 200, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OverSeasTrans", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                 .SetEmptyItem("", "");

            group.AddComboBoxColumn("TRILATERALTRADE", 200, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OverSeasTrans", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                 .SetEmptyItem("", "");

            group = grdSub.View.AddGroupColumn("OSPPRICE");
            group.AddSpinEditColumn("FINISHEDPRICE", 120).SetDisplayFormat("{0:N5}").SetLabel("FINISHEDWON");
            group.AddSpinEditColumn("HALFPRODUCTPRICE", 120).SetDisplayFormat("{0:N5}").SetLabel("HALFPRODUCTWON");
            group.AddSpinEditColumn("SMTPRICE", 120).SetDisplayFormat("{0:N5}").SetLabel("SMTWON");

            group = grdSub.View.AddGroupColumn("");
            group.AddSpinEditColumn("YEILDPLAN", 120).SetDisplayFormat("{0:N2}").SetLabel("YEILDPLANPER");

            grdSub.View.PopulateColumns();

            grdSub.ShowStatusBar = false;

            #endregion 월별 구분 List

            #region 전체 조회

            grdHistory.GridButtonItem = GridButtonItem.Export;
            grdHistory.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            grdHistory.View.AddTextBoxColumn("MASTERDATACLASSID").SetIsHidden();
            grdHistory.View.AddTextBoxColumn("PRODUCTIONTYPE").SetIsHidden();
            grdHistory.View.AddTextBoxColumn("ENTERPRISEID").SetIsHidden();
            grdHistory.View.AddTextBoxColumn("CUSTOMERID").SetIsHidden();
            grdHistory.View.AddTextBoxColumn("PRODUCTTYPE").SetIsHidden();
            grdHistory.View.AddTextBoxColumn("LAYER").SetIsHidden();

            group = grdHistory.View.AddGroupColumn("");
            var subGroup = group.AddGroupColumn("");

            subGroup.AddTextBoxColumn("PRODUCTDIVISION", 70);
            subGroup.AddTextBoxColumn("PRODUCTIONTYPENAME", 70);
            subGroup.AddTextBoxColumn("PRODUCTDEFID", 100);
            subGroup.AddTextBoxColumn("PRODUCTDEFVERSION", 70);
            subGroup.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            subGroup.AddTextBoxColumn("COMPANYCLIENT", 150);
            subGroup.AddTextBoxColumn("PRODUCTTYPENAME", 70).SetLabel("PRODUCTTYPE");
            subGroup.AddTextBoxColumn("LAYERNAME", 70);
            subGroup.AddTextBoxColumn("USERLAYER", 70);
            subGroup.AddTextBoxColumn("SMT", 70);
            subGroup.AddTextBoxColumn("APNNO", 80);
            subGroup.AddSpinEditColumn("BLKPNL", 90);
            subGroup.AddSpinEditColumn("ARRAYPCSPNL", 110);
            subGroup.AddSpinEditColumn("CALCULATION2", 110);
            subGroup.AddSpinEditColumn("PNLSIZEXAXIS", 110);
            subGroup.AddSpinEditColumn("PNLSIZEYAXIS", 110);
            subGroup.AddSpinEditColumn("PNLAREAM2", 100).SetDisplayFormat("{0:N6}");

            for (int i = 1; i < 13; i++)
            {
                group = grdHistory.View.AddGroupColumn(string.Concat(i, Language.Get("NMONTH")));
                subGroup = group.AddGroupColumn("SITEDIVISION");
                subGroup.AddTextBoxColumn(string.Concat("INPUTFACTORY", i), 80).SetLabel("INPUT");
                subGroup.AddTextBoxColumn(string.Concat("FINALFACTORY", i), 80).SetLabel("FINAL");
                subGroup.AddTextBoxColumn(string.Concat("DELIVERYFACTORY", i), 80).SetLabel("DELIVERY");

                subGroup = group.AddGroupColumn("");
                subGroup.AddTextBoxColumn(string.Concat("DIRECTDELIVERY", i), 200).SetLabel("DIRECTDELIVERY");
                subGroup.AddTextBoxColumn(string.Concat("TRILATERALTRADE", i), 200).SetLabel("TRILATERALTRADE");

                subGroup = group.AddGroupColumn("OSPPRICE");
                subGroup.AddSpinEditColumn(string.Concat("FINISHEDPRICE", i), 120).SetDisplayFormat("{0:N5}").SetLabel("FINISHEDWON");
                subGroup.AddSpinEditColumn(string.Concat("HALFPRODUCTPRICE", i), 120).SetDisplayFormat("{0:N5}").SetLabel("HALFPRODUCTWON");
                subGroup.AddSpinEditColumn(string.Concat("SMTPRICE", i), 120).SetDisplayFormat("{0:N5}").SetLabel("SMTWON");
            }

            group = grdHistory.View.AddGroupColumn("");
            subGroup = group.AddGroupColumn("YEILDPLANPER");

            for (int i = 1; i < 13; i++)
            {
                subGroup.AddSpinEditColumn(string.Concat("YEILDPLAN", i), 80).SetDisplayFormat("{0:N2}").SetLabel(string.Concat(i, Language.Get("NMONTH")));
            }

            grdHistory.View.PopulateColumns();
            grdHistory.View.SetIsReadOnly();

            grdHistory.ShowStatusBar = true;

            #endregion 전체 조회
        }

        #endregion 컨텐츠 영역 초기화

        #region Event

        /// <summary>
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            // 연도 Date Controler 값 변경 이벤트
            dateYear.EditValueChanged += (s, e) => GetYearPlanData();

            // Main Grid Row 변경 이벤트
            grdMain.View.FocusedRowChanged += (s, e) => GetYearPlanData();

            // 연도 Date Controler 값 변경전 Grid에 변경된 내용이 있는지 체크 이벤트
            dateYear.EditValueChanging += (s, e) =>
            {
                if (_isSave)
                {
                    return;
                }

                if (grdSub.GetChangedRows() is DataTable dt)
                {
                    if (dt.Rows.Count.Equals(0))
                    {
                        return;
                    }

                    if (DialogResult.No.Equals(MSGBox.Show(MessageBoxType.Information, "ThereIsChangeMove", MessageBoxButtons.YesNo)))
                    {
                        e.Cancel = true;
                        return;
                    }
                    else
                    {
                        return;
                    }
                }
            };

            // Main Grid Row 변경 전 이벤트
            grdMain.View.BeforeLeaveRow += (s, e) =>
            {
                if (e.RowHandle < 0 || _isSave)
                {
                    return;
                }

                if (grdSub.GetChangedRows() is DataTable dt)
                {
                    if (dt.Rows.Count.Equals(0))
                    {
                        return;
                    }

                    if (DialogResult.No.Equals(MSGBox.Show(MessageBoxType.Information, "ThereIsChangeMove", MessageBoxButtons.YesNo)))
                    {
                        e.Allow = false;
                        return;
                    }
                    else
                    {
                        return;
                    }
                }
            };

            // Tab Page 변경시 이벤트
            tabMain.SelectedPageChanged += (s, e) =>
            {
                SetConditionVisiblility("P_YEAR", e.Page.Equals(xtraTabPage1) ? LayoutVisibility.Never : LayoutVisibility.Always);
            };
        }

        #endregion Event

        #region 툴바

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            ExecuteRule("ItemMasterMakingPlan", grdSub.GetChangedRows());

            _isSave = true;
        }

        #endregion 툴바

        #region 검색

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            try
            {
                DialogManager.ShowWaitArea(this.pnlContent);

                await base.OnSearchAsync();

                Dictionary<string, object> param = Conditions.GetValues();
                param["P_YEAR"] = ((DateTime)param["P_YEAR"]).ToString("yyyy");
                param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);

                string query = string.Empty;
                SmartBandedGrid grid;

                if (tabMain.SelectedTabPageIndex.Equals(0))
                {
                    grdMain.View.ClearDatas();
                    grdSub.View.ClearDatas();
                    query = "SelectProductPlan";
                    grid = grdMain;
                }
                else
                {
                    grdHistory.View.ClearDatas();
                    query = "SelectPlanMakingAll";
                    grid = grdHistory;
                }

                if (await SqlExecuter.QueryAsync(query, "10001", param) is DataTable dt)
                {
                    if (dt.Rows.Count.Equals(0))
                    {
                        ShowMessage("NoSelectData");
                        return;
                    }

                    grid.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                throw MessageException.Create(Format.GetString(ex));
            }
            finally
            {
                DialogManager.CloseWaitArea(this.pnlContent);

                _isSave = false;
            }
        }

        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            #region 고객사

            var condition = Conditions.AddSelectPopup("p_customerId", new SqlQuery("GetCustomerList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "CUSTOMERNAME", "CUSTOMERID")
                                      .SetPopupLayout("CUSTOMER", PopupButtonStyles.Ok_Cancel, true, true)
                                      .SetPopupLayoutForm(500, 600)
                                      .SetLabel("CUSTOMER")
                                      .SetPopupResultCount(1)
                                      .SetPosition(4.1);

            condition.Conditions.AddTextBox("TXTCUSTOMERID");

            condition.GridColumns.AddTextBoxColumn("CUSTOMERID", 150);
            condition.GridColumns.AddTextBoxColumn("CUSTOMERNAME", 200);

            #endregion 고객사

            // SMT
            Conditions.AddComboBox("p_smt", new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CODECLASSID=YesNo"))
                      .SetLabel("SMT")
                      .SetDefault("Y")
                      .SetPosition(9);
        }

        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            Conditions.GetControl<SmartDateEdit>("P_YEAR").EditValue = DateTime.Now;
        }

        #endregion 검색

        #region 유효성 검사

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            if (grdSub.GetChangedRows().Rows.Count.Equals(0))
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }

        #endregion 유효성 검사

        #region Private Function

        /// <summary>
        /// 하단 연간 Plan 리스트를 조회한다.
        /// </summary>
        private void GetYearPlanData()
        {
            if (grdMain.View.FocusedRowHandle < 0)
            {
                return;
            }

            try
            {
                DialogManager.ShowWaitArea(this.pnlContent);

                Dictionary<string, object> param = new Dictionary<string, object>
                {
                    { "P_YEAR", ((DateTime)dateYear.EditValue).ToString("yyyy") },
                    { "ITEMID", grdMain.View.GetFocusedRowCellValue("PRODUCTDEFID") },
                    { "ITEMVERSION", grdMain.View.GetFocusedRowCellValue("PRODUCTDEFVERSION") },
                    { "ENTERPRISEID", UserInfo.Current.Enterprise }
                };

                txtProductDefId.Text = Format.GetString(param["ITEMID"], string.Empty);
                txtProductdefVersion.Text = Format.GetString(param["ITEMVERSION"], string.Empty);

                grdSub.View.ClearDatas();

                if (SqlExecuter.Query("SelectPlanMakingByProduct", "10001", param) is DataTable dt)
                {
                    if (dt.Rows.Count.Equals(0))
                    {
                        ShowMessage("NoSelectData");
                        return;
                    }

                    grdSub.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                throw MessageException.Create(Format.GetString(ex));
            }
            finally
            {
                DialogManager.CloseWaitArea(this.pnlContent);
            }
        }

        #endregion Private Function
    }
}