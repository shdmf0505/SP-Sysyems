#region using

using DevExpress.XtraScheduler;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion using

namespace Micube.SmartMES.EquipmentManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 설비관리 > 보전관리 > 설비PM실적등록
    /// 업  무  설  명  : 설비PM의 실적을 등록한다.
    /// 생    성    자  : 김용조
    /// 생    성    일  : 2019-09-10
    /// 수  정  이  력  :
    ///         2021.03.12 전우성 요구사항 493번 백기영 과장 요청 실적등록 popup창(설비년간점검, 현장PM, 정기PM) 일원화(하나로 통일. 기준은 설비년간점검 POPUP창) 및 화면 최적화
    ///         2021.04.07 전우성 Appointment Start/End Time에 실제 등록한 시간을 보여줌
    ///
    /// </summary>
    public partial class RegistPMWorkOrder : SmartConditionManualBaseForm
    {
        #region Local Variables

        private DateTime _startDate = DateTime.Now;

        private string _displayViewInfoCaption;
        private string _displayDelayInfoCaption;
        private string _displayListCaption;

        #endregion Local Variables

        #region 생성자

        public RegistPMWorkOrder()
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

            SetSchedulerOptions();
            InitializeEvent();
        }

        /// <summary>
        /// 스케쥴러 컨트롤의 옵션을 설정한다.
        /// </summary>
        private void SetSchedulerOptions()
        {
            schPlan.Views.MonthView.AppointmentDisplayOptions.StartTimeVisibility = AppointmentTimeVisibility.Never;
            schPlan.Views.MonthView.AppointmentDisplayOptions.EndTimeVisibility = AppointmentTimeVisibility.Never;
            schPlan.Start = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-") + "01");
            schPlan.Views.MonthView.AppointmentDisplayOptions.ShowReminder = false;
            schPlan.OptionsBehavior.ShowRemindersForm = false;
            schPlan.ActiveViewType = SchedulerViewType.Month;
            schPlan.OptionsView.ToolTipVisibility = ToolTipVisibility.Never;
            schPlan.MonthView.ShowMoreButtons = false;
            schPlan.Views.MonthView.NavigationButtonVisibility = NavigationButtonVisibility.Never;
            schPlan.Views.MonthView.AllowScrollAnimation = false;
            schPlan.Views.MonthView.DateTimeScrollbarVisible = false;
            schPlan.Views.MonthView.UseOptimizedScrolling = false;

            _displayViewInfoCaption = Language.Get("REGISTWORKORDERRESULT");
            _displayDelayInfoCaption = Language.Get("DELAYMAINTREASON");
            _displayListCaption = Language.Get("WORKORDERRESULTLIST");
        }

        #endregion 컨텐츠 영역 초기화

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // 스케쥴러 바인딩시 각각의 점검의 상태에 따라 색을 변경
            schPlan.AppointmentViewInfoCustomizing += (s, e) =>
            {
                e.ViewInfo.ShouldShowToolTip = false;
                e.ViewInfo.ShowBell = false;
                e.ViewInfo.Appearance.Font = new Font(e.ViewInfo.Appearance.Font.Name, 9);

                if (e.ViewInfo.Appointment.Description.IndexOf("Create").Equals(0))
                {
                    e.ViewInfo.Appearance.BackColor = Color.Gold;
                    e.ViewInfo.Appearance.ForeColor = Color.Black;
                }
                else if (e.ViewInfo.Appointment.Description.IndexOf("Finish").Equals(0))
                {
                    e.ViewInfo.Appearance.BackColor = Color.LimeGreen;
                    e.ViewInfo.Appearance.ForeColor = Color.Black;
                }
                else if (e.ViewInfo.Appointment.Description.IndexOf("Skip").Equals(0))
                {
                    e.ViewInfo.Appearance.BackColor = Color.LightGray;
                    e.ViewInfo.Appearance.ForeColor = Color.Black;
                }
                else if (e.ViewInfo.Appointment.Description.IndexOf("Delay").Equals(0))
                {
                    e.ViewInfo.Appearance.BackColor = Color.Salmon;
                    e.ViewInfo.Appearance.ForeColor = Color.Black;
                }
                else if (e.ViewInfo.Appointment.Description.IndexOf("Start").Equals(0))
                {
                    e.ViewInfo.Appearance.BackColor = Color.PowderBlue;
                    e.ViewInfo.Appearance.ForeColor = Color.Black;
                }
            };

            // 스케쥴러 클릭이벤트 (각각의 점검및 특정일 모두 반응)
            schPlan.EditAppointmentFormShowing += (s, e) =>
            {
                try
                {
                    if (e.Appointment != null)
                    {
                        if (e.Appointment.Id != null)
                        {
                            Popup.RegistResultMaintWorkOrderAppPopup pmPopup = new Popup.RegistResultMaintWorkOrderAppPopup(e.Appointment.Id.ToString()
                                                                                                                            , Conditions.GetValue("P_PLANTID").ToString()
                                                                                                                            , e.Appointment.Description.Split('$')[2]
                                                                                                                            , e.Appointment.Description.Split('$')[3]);

                            pmPopup.SearchHandler += Research;
                            pmPopup.ShowDialog();

                            // 2021.03.12 전우성 요구사항 493번 백기영 과장 요청 실적등록 popup창(설비년간점검, 현장PM, 정기PM) 일원화(하나로 통일. 기준은 설비년간점검 POPUP창)
                            //string workOrderType = e.Appointment.Description.Split('$')[1];

                            //if (workOrderType.Equals("PM"))
                            //{
                            //    Popup.RegistResultMaintWorkOrderAppPopup pmPopup = new Popup.RegistResultMaintWorkOrderAppPopup(e.Appointment.Id.ToString()
                            //                                                                                                  , Conditions.GetValue("P_PLANTID").ToString()
                            //                                                                                                  , e.Appointment.Description.Split('$')[2]
                            //                                                                                                  , e.Appointment.Description.Split('$')[3]);

                            //    pmPopup.SearchHandler += Research;
                            //    pmPopup.ShowDialog();
                            //}
                            //else
                            //{
                            //    Popup.RegistResultMaintWorkOrderProductPopup productPopup = new Popup.RegistResultMaintWorkOrderProductPopup(e.Appointment.Id.ToString()
                            //                                                                                                               , Conditions.GetValue("P_PLANTID").ToString()
                            //                                                                                                               , e.Appointment.Description.Split('$')[2]);

                            //    productPopup.SearchHandler += Research;
                            //    productPopup.ShowDialog();
                            //}
                        }
                        else
                        {
                            //아이템이 아닌 날짜를 클릭하였으므로 전체 목록화면을 생성
                            Popup.MaintWorkOrderResultSelector selectorPopup = new Popup.MaintWorkOrderResultSelector(GetCurrentAppointments(e.Appointment.Start.ToString("yyyy-MM-dd"))
                                                                                                                    , e.Appointment.Start.ToString("yyyy-MM-dd")
                                                                                                                    , Conditions.GetValue("P_PLANTID").ToString(), "");
                            selectorPopup.SearchHandler += Research;
                            selectorPopup.ShowDialog();
                        }
                    }
                }
                catch (Exception err)
                {
                    throw err;
                }
                finally
                {
                    e.Handled = true;
                }
            };

            // 마우스휠 제어 이벤트 (선택된 월을 벗어나지 않도록 제어한다.)
            schPlan.MouseWheel += (s, e) => schPlan.Start = _startDate;

            // 스켈쥴러 컨트롤의 팝업창메뉴이벤트
            schPlan.PopupMenuShowing += (s, e) =>
            {
                //기본등록된 메뉴들을 제거하고 정의된 메뉴를 추가한다.
                if (e.Menu.Id.Equals(SchedulerMenuItemId.AppointmentMenu))
                {
                    e.Menu.RemoveMenuItem(SchedulerMenuItemId.OpenAppointment);
                    e.Menu.RemoveMenuItem(SchedulerMenuItemId.EditSeries);
                    e.Menu.RemoveMenuItem(SchedulerMenuItemId.RestoreOccurrence);
                    e.Menu.RemoveMenuItem(SchedulerMenuItemId.StatusSubMenu);
                    e.Menu.RemoveMenuItem(SchedulerMenuItemId.LabelSubMenu);
                    e.Menu.RemoveMenuItem(SchedulerMenuItemId.DeleteAppointment);

                    e.Menu.Items.Add(new SchedulerMenuItem(_displayViewInfoCaption, DisplayDetailInfo_Click));
                    e.Menu.Items.Add(new SchedulerMenuItem(_displayDelayInfoCaption, DisplayDelayInfo_Click));
                }
                else if (e.Menu.Id.Equals(SchedulerMenuItemId.DefaultMenu)) //점검을 선택하지 않은 경우에 팝업메뉴를 보여주지 않는다.
                {
                    e.Menu.RemoveMenuItem(SchedulerMenuItemId.NewAppointment);
                    e.Menu.RemoveMenuItem(SchedulerMenuItemId.NewAllDayEvent);
                    e.Menu.RemoveMenuItem(SchedulerMenuItemId.NewRecurringAppointment);
                    e.Menu.RemoveMenuItem(SchedulerMenuItemId.NewRecurringEvent);
                    e.Menu.RemoveMenuItem(SchedulerMenuItemId.GotoThisDay);
                    e.Menu.RemoveMenuItem(SchedulerMenuItemId.GotoToday);
                    e.Menu.RemoveMenuItem(SchedulerMenuItemId.GotoDate);
                    e.Menu.RemoveMenuItem(SchedulerMenuItemId.SwitchViewMenu);
                }
            };

            //  Site관련정보를 화면로딩후 설정한다.
            Shown += (sen, arge) =>
            {
                ChangeSiteCondition();

                // 검색조건의 Site정보 변경시 관련 쿼리들을 일괄 변경한다.
                ((SmartComboBox)Conditions.GetControl("P_PLANTID")).EditValueChanged += (sender, arg) => ChangeSiteCondition();

                Conditions.SetValue("p_planDate", 0, DateTime.Now.ToString("yyyy-MM"));

                //검색조건으로부터 포커스를 아웃하기 위해 Label에 포커스를 준다.
                lblCreate.Focus();
            };
        }

        /// <summary>
        /// 스케쥴러 컨트롤에 배치된 각각의 점검을 클릭했을때의 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DisplayDetailInfo_Click(object sender, EventArgs e)
        {
            this.ShowWaitArea();
            try
            {
                if (schPlan.SelectedAppointments.Count > 0)
                {
                    Appointment currentAppo = schPlan.SelectedAppointments[0];

                    // 2021.03.12 전우성 요구사항 493번 백기영 과장 요청 실적등록 popup창(설비년간점검, 현장PM, 정기PM) 일원화(하나로 통일. 기준은 설비년간점검 POPUP창)
                    Popup.RegistResultMaintWorkOrderAppPopup pmPopup = new Popup.RegistResultMaintWorkOrderAppPopup(currentAppo.Id.ToString()
                            , Conditions.GetValue("P_PLANTID").ToString()
                            , currentAppo.Description.Split('$')[2]
                            , currentAppo.Description.Split('$')[3]);

                    pmPopup.SearchHandler += Research;
                    pmPopup.ShowDialog();

                    //string workOrderType = currentAppo.Description.Split('$')[1];

                    //if (workOrderType.Equals("PM"))
                    //{
                    //    Popup.RegistResultMaintWorkOrderAppPopup pmPopup = new Popup.RegistResultMaintWorkOrderAppPopup(currentAppo.Id.ToString()
                    //        , Conditions.GetValue("P_PLANTID").ToString()
                    //        , currentAppo.Description.Split('$')[2]
                    //        , currentAppo.Description.Split('$')[3]);

                    //    pmPopup.SearchHandler += Research;
                    //    pmPopup.ShowDialog();
                    //}
                    //else
                    //{
                    //    Popup.RegistResultMaintWorkOrderProductPopup productPopup = new Popup.RegistResultMaintWorkOrderProductPopup(currentAppo.Id.ToString()
                    //        , Conditions.GetValue("P_PLANTID").ToString()
                    //        , currentAppo.Description.Split('$')[2]);

                    //    productPopup.SearchHandler += Research;
                    //    productPopup.ShowDialog();
                    //}
                }
            }
            catch (Exception err)
            {
                throw err;
            }
            finally
            {
                this.CloseWaitArea();
            }
        }

        /// <summary>
        /// 스케쥴러 컨트롤에서 각각의 점검의 팝업창에서 지연사유입력을 선택했을떄의 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DisplayDelayInfo_Click(object sender, EventArgs e)
        {
            this.ShowWaitArea();
            try
            {
                if (schPlan.SelectedAppointments.Count > 0)
                {
                    Appointment currentAppo = schPlan.SelectedAppointments[0];

                    Popup.RegistDelayReasonMaintWorkOrder delayPopup = new Popup.RegistDelayReasonMaintWorkOrder(currentAppo.Id.ToString()
                        , Conditions.GetValue("P_PLANTID").ToString()
                        , currentAppo.Description.Split('$')[2]);

                    delayPopup.SearchHandler += Research;
                    delayPopup.ShowDialog();
                }
            }
            catch (Exception err)
            {
                throw err;
            }
            finally
            {
                this.CloseWaitArea();
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

            var values = Conditions.GetValues();

            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            values.Add("CURRENTLOGINID", UserInfo.Current.Id);

            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable pmPlanTable = SqlExecuter.Query("GetMaintWorkOrderListForCalendarByEqp", "10001", values);

            if (pmPlanTable != null)
            {
                if (pmPlanTable.Rows.Count < 1)
                {
                    DisplayPMPlanList(pmPlanTable, Convert.ToDateTime(values["P_PLANDATE"]).ToString("yyyy-MM") + "-01");
                }
                else
                {
                    if (values["P_PLANDATE"] != null)
                    {
                        DisplayPMPlanList(pmPlanTable, Convert.ToDateTime(values["P_PLANDATE"]).ToString("yyyy-MM") + "-01");
                    }
                }
            }
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // PM, MM을 가져온다
            Conditions.AddComboBox("MAINTTYPE", new SqlQuery("GetCodeListByToolWithLanguage", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CODECLASSID=MaintType"), "CODENAME", "CODEID")
                      .SetLabel("MAINTTYPE")
                      .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                      .SetPosition(0.2)
                      .SetValidationIsRequired()
                      .SetDefault("PM");

            // 공장 조회
            Conditions.AddComboBox("FACTORYID", new SqlQuery("GetFactoryListByEqp", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "FACTORYNAME", "FACTORYID")
                      .SetLabel("FACTORY")
                      .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                      .SetPosition(1.1)
                      .SetEmptyItem("", "", true)
                      .SetRelationIds("P_PLANTID");

            #region 입고작업장

            var condition = Conditions.AddSelectPopup("AREAID", new SqlQuery("GetAreaListByTool", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                      .SetPopupLayout("AREA", PopupButtonStyles.Ok_Cancel, true, true)
                                      .SetPopupResultCount(1)
                                      .SetPosition(1.2)
                                      .SetPopupLayoutForm(600, 800, FormBorderStyle.FixedToolWindow)
                                      .SetPopupAutoFillColumns("AREAID")
                                      .SetPopupResultMapping("AREAID", "AREAID")
                                      .SetRelationIds("P_PLANTID");

            condition.ValueFieldName = "AREAID";
            condition.DisplayFieldName = "AREANAME";

            condition.Conditions.AddTextBox("AREANAME");

            condition.GridColumns.AddTextBoxColumn("AREAID", 150).SetIsHidden();
            condition.GridColumns.AddTextBoxColumn("AREANAME", 200);

            #endregion 입고작업장

            #region 설비

            condition = Conditions.AddSelectPopup("EQUIPMENTID", new SqlQuery("GetEquipmentListByEqp", "10001", $"LANGUAGETYPE={ UserInfo.Current.LanguageType }", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"CURRENTLOGINID={UserInfo.Current.Id}"))
                                  .SetPopupLayout("EQUIPMENTID", PopupButtonStyles.Ok_Cancel, true, false)
                                  .SetPopupLayoutForm(984, 465, FormBorderStyle.FixedToolWindow)
                                  .SetPopupResultMapping("EQUIPMENTID", "EQUIPMENTID")
                                  .SetLabel("EQUIPMENT")
                                  .SetPopupResultCount(1)
                                  .SetPosition(1.3);

            condition.ValueFieldName = "EQUIPMENTID";
            condition.DisplayFieldName = "EQUIPMENTNAME";

            condition.SetRelationIds("P_PLANTID");

            condition.Conditions.AddTextBox("EQUIPMENTID").SetLabel("EQUIPMENTID");
            condition.Conditions.AddComboBox("EQUIPMENTTYPE", new SqlQuery("GetCodeListByToolWithLanguage", "10001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "CODECLASSID", "EquipmentType" } }), "CODENAME", "CODEID")
                                    .SetEmptyItem("", "", true)
                                    .SetLabel("EQUIPMENTCLASSTYPE")
                                    .ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            condition.Conditions.AddComboBox("DETAILEQUIPMENTTYPE", new SqlQuery("GetCodeListByToolWithLanguage", "10001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "CODECLASSID", "DetailEquipmentType" } }), "CODENAME", "CODEID")
                                    .SetEmptyItem("", "", true)
                                    .SetLabel("DETAILEQUIPMENTTYPE")
                                    .ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;

            InitializeAreaPopupInSearchCondition(condition.Conditions);

            // 팝업 그리드
            condition.GridColumns.AddTextBoxColumn("EQUIPMENTID", 120).SetIsHidden();
            condition.GridColumns.AddTextBoxColumn("EQUIPMENTNAME", 200).SetIsReadOnly();
            condition.GridColumns.AddTextBoxColumn("EQUIPMENTCLASSID", 50).SetIsHidden();
            condition.GridColumns.AddTextBoxColumn("EQUIPMENTCLASS", 100).SetIsReadOnly();
            condition.GridColumns.AddTextBoxColumn("EQUIPMENTTYPEID", 50).SetIsHidden();
            condition.GridColumns.AddTextBoxColumn("EQUIPMENTTYPE", 100).SetIsReadOnly();
            condition.GridColumns.AddTextBoxColumn("DETAILEQUIPMENTTYPEID", 200).SetIsHidden();
            condition.GridColumns.AddTextBoxColumn("DETAILEQUIPMENTTYPE", 100).SetIsReadOnly();
            condition.GridColumns.AddTextBoxColumn("MODEL", 100).SetIsReadOnly();
            condition.GridColumns.AddTextBoxColumn("SERIAL", 150).SetIsReadOnly();
            condition.GridColumns.AddTextBoxColumn("DESCRIPTION", 200).SetIsReadOnly();

            #endregion 설비
        }

        /// <summary>
        /// 설비 SelectPopup 내부 작업장 SelectPoup
        /// </summary>
        private void InitializeAreaPopupInSearchCondition(ConditionCollection conditions)
        {
            var popupGridToolArea = conditions.AddSelectPopup("AREAID", new SqlQuery("GetAreaListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}"))
                                              .SetPopupLayout("AREA", PopupButtonStyles.Ok_Cancel, true, false)
                                              .SetPopupResultCount(1)
                                              .SetPopupResultMapping("AREAID", "AREAID")
                                              .SetPopupLayoutForm(500, 600, FormBorderStyle.FixedToolWindow)
                                              .SetPopupAutoFillColumns("AREAID");

            popupGridToolArea.ValueFieldName = "AREAID";
            popupGridToolArea.DisplayFieldName = "AREANAME";

            popupGridToolArea.Conditions.AddTextBox("AREANAME");

            popupGridToolArea.GridColumns.AddTextBoxColumn("AREAID", 300).SetIsHidden();
            popupGridToolArea.GridColumns.AddTextBoxColumn("AREANAME", 300).SetIsReadOnly();
        }

        #endregion 검색

        #region Private Function

        /// <summary>
        /// 재검색
        /// </summary>
        private void Research()
        {
            var values = Conditions.GetValues();

            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            values.Add("CURRENTLOGINID", UserInfo.Current.Id);

            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable pmPlanTable = SqlExecuter.Query("GetMaintWorkOrderListForCalendarByEqp", "10001", values);

            if (pmPlanTable.Rows.Count < 1)
            {
                DisplayPMPlanList(pmPlanTable, Convert.ToDateTime(values["P_PLANDATE"]).ToString("yyyy-MM") + "-01");
            }
            else
            {
                DisplayPMPlanList(pmPlanTable, Convert.ToDateTime(values["P_PLANDATE"]).ToString("yyyy-MM") + "-01");
            }
        }

        /// <summary>
        /// 달력에 점검 표시
        /// </summary>
        /// <param name="planTable"></param>
        /// <param name="startDate"></param>
        private void DisplayPMPlanList(DataTable planTable, string startDate)
        {
            schPlan.DataStorage.Appointments.Clear();

            schPlan.Start = Convert.ToDateTime(startDate + "-01");
            _startDate = schPlan.Start;

            if (planTable.Rows.Count > 0)
            {
                foreach (DataRow planRow in planTable.Rows)
                {
                    Appointment apt = schPlan.DataStorage.CreateAppointment(AppointmentType.Normal);
                    apt.Start = DBNull.Value.Equals(planRow.GetObject("STARTTIME")) ? Convert.ToDateTime(string.Concat(planRow.GetString("PLANDATE"), " 00:00:00")) : Convert.ToDateTime(planRow.GetObject("STARTTIME"));
                    apt.End = DBNull.Value.Equals(planRow.GetObject("FINISHTIME")) ? Convert.ToDateTime(string.Concat(apt.Start.ToString("yyyy-MM-dd"), " 23:59:59")) : Convert.ToDateTime(planRow.GetObject("FINISHTIME"));
                    apt.SetId(planRow.GetString("WORKORDERID"));
                    apt.Subject = planRow.GetString("WORKORDERNAME");
                    apt.Description = planRow.GetString("WORKORDERSTATUS") + "$" + planRow.GetString("WORKORDERTYPE") + "$" + planRow.GetString("ISMODIFY") + "$" + planRow.GetString("FACTORYID");
                    apt.HasReminder = true;

                    schPlan.DataStorage.Appointments.Add(apt);
                }
            }
            else
            {
                //빈 Appointment라도 만들어 주지 않으면 화면 reset이 되지 않음
                schPlan.DataStorage.Appointments.Add(schPlan.DataStorage.CreateAppointment(AppointmentType.Normal));
            }

            schPlan.DataStorage.Appointments.EndUpdate();
        }

        /// <summary>
        /// 현재 점검의 정보를 가져온다.
        /// </summary>
        /// <param name="currentDate"></param>
        /// <returns></returns>
        private DataTable GetCurrentAppointments(string currentDate)
        {
            DataTable appTable = new DataTable();
            appTable.Columns.Add("WORKORDERID");
            appTable.Columns.Add("WORKORDERNAME");
            appTable.Columns.Add("WORKORDERTYPE");
            appTable.Columns.Add("WORKORDERSTATUS");
            appTable.Columns.Add("ISMODIFY");
            appTable.Columns.Add("FACTORYID");
            for (int i = 0; i < schPlan.DataStorage.Appointments.Count; i++)
            {
                if (schPlan.DataStorage.Appointments[i].Id != null)
                {
                    if (schPlan.DataStorage.Appointments[i].Start.ToString("yyyy-MM-dd").Equals(currentDate))
                    {
                        DataRow appRow = appTable.NewRow();
                        appRow["WORKORDERID"] = schPlan.DataStorage.Appointments[i].Id.ToString();
                        appRow["WORKORDERNAME"] = schPlan.DataStorage.Appointments[i].Subject;
                        appRow["WORKORDERSTATUS"] = schPlan.DataStorage.Appointments[i].Description.Split('$')[0];
                        appRow["WORKORDERTYPE"] = schPlan.DataStorage.Appointments[i].Description.Split('$')[1];
                        appRow["ISMODIFY"] = schPlan.DataStorage.Appointments[i].Description.Split('$')[2];
                        appRow["FACTORYID"] = schPlan.DataStorage.Appointments[i].Description.Split('$')[3];

                        appTable.Rows.Add(appRow);
                    }
                }
            }
            return appTable;
        }

        /// <summary>
        /// 검색조건의 Site변경시 관련된 쿼리들을 변경
        /// </summary>
        private void ChangeSiteCondition()
        {
            //if (factoryBox != null)
            //    factoryBox.Query = new SqlQuery("GetFactoryListByEqp", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}");

            //if (popupGridToolArea != null)
            //    popupGridToolArea.SearchQuery = new SqlQuery("GetAreaListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}");

            if (Conditions.GetCondition("EQUIPMENTID") != null)
            {
                ((ConditionItemSelectPopup)((ConditionItemSelectPopup)Conditions.GetCondition("EQUIPMENTID")).Conditions.GetCondition("AREAID")).SearchQuery = new SqlQuery("GetAreaListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}", $"CURRENTLOGINID={UserInfo.Current.Id}");
            }
            //if (segmentCondition != null)
            //    segmentCondition.Query = new SqlQuery("GetProcessSegmentByTool", "10001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "ENTERPRISEID", UserInfo.Current.Enterprise }, { "PLANTID", Conditions.GetValue("P_PLANTID") } });

            //if (makeVendorPopup != null)
            //    makeVendorPopup.SearchQuery = new SqlQuery("GetVendorListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}");

            //if (filmCodeCondition != null)
            //    filmCodeCondition.SearchQuery = new SqlQuery("GetFilmCodePopupListByTool", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}");

            //ucFilmCode.PlantID = Conditions.GetValue("P_PLANTID").ToString();

            //작업장 검색조건 초기화
            //((SmartSelectPopupEdit)Conditions.GetControl("AREANAME")).ClearValue();
        }

        #endregion Private Function
    }
}