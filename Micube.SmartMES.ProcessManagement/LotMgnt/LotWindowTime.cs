#region using

using DevExpress.Utils.Menu;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons;
using Micube.SmartMES.Commons.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion using

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 공정관리 > 생산관리 > Window Time Lot 조회
    /// 업  무  설  명  : Window Time Lot 조회를 한다.
    /// 생    성    자  : 한주석
    /// 생    성    일  : 2019-10-10
    /// 수  정  이  력  :
    ///
    ///
    /// </summary>
    public partial class LotWindowTime : SmartConditionManualBaseForm
    {
        #region Local Variables

        private List<DXMenuItem> menuList = new List<DXMenuItem>();
        private Dictionary<string, object> _parameter;

        #endregion Local Variables

        #region 생성자

        public LotWindowTime()
        {
            InitializeComponent();
        }

        #endregion 생성자

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 화면의 컨텐츠 영역을 초기화한다.
        /// </summary>
        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeGrid();
            InitializeEvent();
            InitializeQuickMenuList();
        }

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            grdMain.GridButtonItem = GridButtonItem.Export;
            grdMain.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            var group = grdMain.View.AddGroupColumn("");
            //진행상태
            group.AddTextBoxColumn("PROCESSINGSTATUS", 45).SetTextAlignment(TextAlignment.Center);
            //양산구분
            group.AddTextBoxColumn("LOTTYPE", 45).SetTextAlignment(TextAlignment.Center);
            // Lot 상태
            group.AddComboBoxColumn("LOTSTATE", 45, new SqlQuery("GetCodeList", "00001", $"CODECLASSID=LotStatus", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                          .SetTextAlignment(TextAlignment.Center);
            //작업장
            group.AddTextBoxColumn("AREANAME", 150).SetLabel("PRESENTAREA");
            //공정명
            group.AddTextBoxColumn("PROCESSSEGMENTNAME", 150).SetLabel("PRESENTPROCESSSEGMENT");
            //품목코드
            group.AddTextBoxColumn("PRODUCTDEFID", 80).SetTextAlignment(TextAlignment.Center);
            //품목명
            group.AddTextBoxColumn("PRODUCTDEFNAME", 150);
            //Lot No
            group.AddTextBoxColumn("LOTID", 180).SetTextAlignment(TextAlignment.Center);
            //FROM 공정수순
            group.AddTextBoxColumn("FROMUSERSEQUENCE", 60).SetTextAlignment(TextAlignment.Center);
            //from 공정
            group.AddTextBoxColumn("FROMPROCESSSEGMENTNAME", 150);
            //from 상태
            group.AddTextBoxColumn("FROMPROCESSSTATE", 60).SetTextAlignment(TextAlignment.Center);
            //TO 공정수순
            group.AddTextBoxColumn("TOUSERSEQUENCE", 60).SetTextAlignment(TextAlignment.Center);
            //to 공정
            group.AddTextBoxColumn("TOPROCESSSEGMENTNAME", 150);
            //to 상태
            group.AddTextBoxColumn("TOPROCESSSTATE", 60).SetTextAlignment(TextAlignment.Center);
            //시작시간
            group.AddTextBoxColumn("SETTIME", 120).SetTextAlignment(TextAlignment.Center).SetLabel("STARTTIME").SetDisplayFormat("yyyy-MM-dd HH:mm:ss");
            //기준시간(분)
            group.AddSpinEditColumn("WTIMELIMIT", 70).SetTextAlignment(TextAlignment.Right).SetLabel("STDTIMEPERMINUTE");
            //제한시간
            group.AddTextBoxColumn("OCCURETIME", 120).SetTextAlignment(TextAlignment.Center).SetLabel("LIMITTIME").SetDisplayFormat("yyyy-MM-dd HH:mm:ss");
            //실행시간
            group.AddTextBoxColumn("WTIME", 120).SetTextAlignment(TextAlignment.Center).SetLabel("EXECUTETIME").SetDisplayFormat("yyyy-MM-dd HH:mm:ss");
            //실행시간(분)
            group.AddSpinEditColumn("ELAPSEDTIME", 70).SetTextAlignment(TextAlignment.Right).SetLabel("EXECUTETIMEPERMINUTE");
            //잔여시간
            group.AddSpinEditColumn("LEFTTIME", 70).SetTextAlignment(TextAlignment.Right).SetLabel("LEFTTIME_MINUTE");
            //공정순서
            //grdTimeLotInfo.AddTextBoxColumn("USERSEQUENCE", 50).SetTextAlignment(TextAlignment.Center);
            //Site
            group.AddTextBoxColumn("PLANTID", 50).SetTextAlignment(TextAlignment.Center);
            //RTR/SHT
            group.AddTextBoxColumn("RTRSHT", 50).SetTextAlignment(TextAlignment.Center);
            //UOM
            group.AddTextBoxColumn("UNIT", 50).SetTextAlignment(TextAlignment.Center).SetLabel("UOM");

            group = grdMain.View.AddGroupColumn("WIPQTY");
            group.AddSpinEditColumn("QTY", 70).SetDisplayFormat("#,##0", MaskTypes.Numeric, false);
            group.AddSpinEditColumn("PANELQTY", 70).SetDisplayFormat("#,##0", MaskTypes.Numeric, false);
            group.AddSpinEditColumn("MM", 70).SetDisplayFormat("#,##0.00", MaskTypes.Numeric, true);

            group = grdMain.View.AddGroupColumn("");
            //공정입고일
            group.AddTextBoxColumn("SEGMENTINBOUNDTIME", 120).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("yyyy-MM-dd HH:mm:ss");
            //공정 L/T(시간)
            group.AddSpinEditColumn("SEGMENT_LEADTIME", 70).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true);
            //투입일
            group.AddTextBoxColumn("LOTSTARTDATE", 120).SetTextAlignment(TextAlignment.Center).SetLabel("INPUTDATE").SetDisplayFormat("yyyy-MM-dd HH:mm:ss");
            //전체 L/T(일)
            group.AddSpinEditColumn("TOTALLEADTIME", 70).SetDisplayFormat("#,##0", MaskTypes.Numeric, false);
            //납기일
            group.AddTextBoxColumn("DUEDATE", 120).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("yyyy-MM-dd HH:mm:ss");
            //잔여일수
            group.AddSpinEditColumn("LEFTDATE", 70).SetDisplayFormat("#,##0", MaskTypes.Numeric, false);

            grdMain.View.PopulateColumns();

            grdMain.View.SetIsReadOnly();
        }

        /// <summary>
        /// 퀵 메뉴 리스트 등록
        /// </summary>
        private void InitializeQuickMenuList()
        {
            //LOT 이력조회
            menuList.Add(new DXMenuItem(Language.Get("MENU_PG-SG-0340"), OpenForm) { BeginGroup = true, Tag = "PG-SG-0340" });
            menuList.Add(new DXMenuItem(Language.Get("MENU_PG-SG-0625"), OpenForm) { Tag = "PG-SG-0625" });
            menuList.Add(new DXMenuItem(Language.Get("MENU_DEFECTMAPBYLOT"), OpenForm) { Tag = "DefectMapByLot" });
        }

        #endregion 컨텐츠 영역 초기화

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            grdMain.InitContextMenuEvent += (s, e) =>
            {
                for (int i = 0; i < menuList.Count; i++)
                {
                    e.AddMenus.Add(menuList[i]);
                }
            };

            grdMain.View.RowStyle += (s, e) =>
            {
                var row = grdMain.View.GetDataRow(e.RowHandle);
                if (row == null || row["wtime"] != DBNull.Value)
                {
                    return;
                }
                int leftTime = int.Parse(row["lefttime"].ToString());
                int timeLimit = int.Parse(row["wtimelimit"].ToString());
                if (leftTime <= timeLimit * 0.2)
                {
                    e.HighPriority = true;
                    e.Appearance.BackColor = Color.LightCoral;
                }
            };

            Shown += (s, e) =>
            {
                if (_parameter != null)
                {
                    DialogManager.ShowWaitDialog();
                    try
                    {
                        Conditions.GetControl<SmartComboBox>("P_PROGRESS").EditValue = "InProgress";
                        SendKeys.Send("{F5}");
                    }
                    finally
                    {
                        DialogManager.Close();
                    }
                }
            };
        }

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

                DataRow currentRow = grdMain.View.GetFocusedDataRow();
                if (currentRow == null)
                {
                    return;
                }

                string menuId = (sender as DXMenuItem).Tag.ToString();

                var param = currentRow.Table.Columns
                                            .Cast<DataColumn>()
                                            .ToDictionary(col => col.ColumnName, col => currentRow[col.ColumnName]);

                OpenMenu(menuId, param);
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
        /// 다른 메뉴에서 호출
        /// </summary>
        /// <param name="parameters"></param>
        public override void LoadForm(Dictionary<string, object> parameters)
        {
            base.LoadForm(parameters);

            if (parameters != null)
            {
                _parameter = parameters;
            }
        }

        #endregion Event

        #region 검색

        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            grdMain.View.ClearDatas();

            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            if (await QueryAsync("SelectWindowTimeLotList", "10001", values) is DataTable dt)
            {
                if (dt.Rows.Count.Equals(0))
                {
                    // 조회할 데이터가 없습니다.
                    ShowMessage("NoSelectData");
                }

                grdMain.DataSource = dt;
            }
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            #region 품목코드

            var condition = Conditions.AddSelectPopup("P_PRODUCTDEFID", new SqlQuery("GetProductDefinitionList", "10002", $"PLANTID={UserInfo.Current.Plant}"), "PRODUCTDEF", "PRODUCTDEF")
                                      .SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, true)
                                      .SetPopupLayoutForm(800, 800)
                                      .SetPopupAutoFillColumns("PRODUCTDEFNAME")
                                      .SetLabel("PRODUCTDEFID")
                                      .SetPosition(1.1)
                                      .SetPopupResultCount(0)
                                      .SetPopupApplySelection((selectRow, gridRow) =>
                                      {
                                          string productDefName = "";

                                          selectRow.AsEnumerable().ForEach(r =>
                                          {
                                              productDefName += Format.GetString(r["PRODUCTDEFNAME"]) + ",";
                                          });

                                          Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").EditValue = productDefName;
                                      });

            condition.Conditions.AddTextBox("TXTPRODUCTDEFNAME");
            condition.Conditions.AddComboBox("PRODUCTDIVISION", new SqlQuery("GetTypeList", "10001", $"CODECLASSID=ProductDivision2", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                .SetDefault("Product");

            condition.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            condition.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            condition.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 100);
            condition.GridColumns.AddComboBoxColumn("PRODUCTDEFTYPE", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductDefType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            condition.GridColumns.AddComboBoxColumn("PRODUCTIONTYPE", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            condition.GridColumns.AddComboBoxColumn("UNIT", 90, new SqlQuery("GetUomDefinitionList", "10001", "UOMCLASSID=Segment"), "UOMDEFNAME", "UOMDEFID");

            #endregion 품목코드

            //LOT
            CommonFunction.AddConditionLotPopup("P_LOTID", 1.2, true, Conditions);

            #region 고객사

            condition = Conditions.AddSelectPopup("P_CUSTOMER", new SqlQuery("GetCustomerList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "CUSTOMERNAME", "CUSTOMERID")
                                  .SetPopupLayout("SELECTCUSTOMERID", PopupButtonStyles.Ok_Cancel, true, true)
                                  .SetPopupLayoutForm(800, 800)
                                  .SetPopupAutoFillColumns("CUSTOMERNAME")
                                  .SetLabel("CUSTOMERID")
                                  .SetPosition(1.3)
                                  .SetPopupResultCount(0);

            condition.Conditions.AddTextBox("TXTCUSTOMERID");

            condition.GridColumns.AddTextBoxColumn("CUSTOMERID", 150);
            condition.GridColumns.AddTextBoxColumn("CUSTOMERNAME", 200);

            #endregion 고객사

            //작업장
            CommonFunction.AddConditionAreaByAuthorityPopup("P_AREAID", 1.4, true, Conditions, false, false);
            //공정
            CommonFunction.AddConditionProcessSegmentPopup("P_PROCESSSEGMENTID", 1.5, true, Conditions);
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
            SmartComboBox processState = Conditions.GetControl<SmartComboBox>("P_PROCESSTYPE");
            processState.LanguageKey = "STATETYPE";
            processState.LabelText = Language.Get(processState.LanguageKey);
        }

        #endregion 검색
    }
}