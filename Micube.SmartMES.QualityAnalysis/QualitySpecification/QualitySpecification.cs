#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;

using Micube.SmartMES.Commons.Controls;

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질 > 품질규격 > 품질규격 측정값 등록
    /// 업  무  설  명  : 품질 규격 측정값을 등록하는 화면
    /// 생    성    자  : 전우성
    /// 생    성    일  : 2019-08-07
    /// 수  정  이  력  : 
    /// [2019-10-09] Grid에 PRODUCTDEFVERSION 컬럼 추가 : 유석진
    /// [2019-10-18] InitializeConditionAreaId_Popup() 추가 : 유석진
    /// [2019-10-18] InitializeConditionPopup_Customer() 추가 : 유석진
    /// [2020-03-16] 재측정 관련 로직 추가
    /// </summary>
    public partial class QualitySpecification : SmartConditionManualBaseForm
    {
        #region Local Variables

        /// <summary>
        /// 부모 form에서 보내주는 파라미터
        /// </summary>
        private Dictionary<string, object> _params;
        List<QcConditionDateDays> rtnlstHour;
        DataTable dtInputFirstData = new DataTable();
        DataTable dtInputHour = new DataTable();

        #endregion

        #region 생성자

        public QualitySpecification()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeLanguageKey();
            InitializeEvent();
            InitializeGrid();

            // 인터는 공장명을 안본다함
            grdMain.View.Columns["FACTORYID"].OwnerBand.Visible = UserInfo.Current.Enterprise.Equals("YOUNGPOONG");
        }

        /// <summary>
        /// Control Language Key 초기화
        /// </summary>
        private void InitializeLanguageKey()
        {
            grdMain.LanguageKey = "MEASUREDVALUE";
        }

        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            grdMain.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            grdMain.View.AddTextBoxColumn("PROCESSSEGMENTID").SetIsHidden();
            grdMain.View.AddTextBoxColumn("PLANTID").SetIsHidden();
            grdMain.View.AddTextBoxColumn("AREAID").SetIsHidden();
            grdMain.View.AddTextBoxColumn("EQUIPMENTID").SetIsHidden();
            grdMain.View.AddTextBoxColumn("CUSTOMERID").SetIsHidden();
            grdMain.View.AddTextBoxColumn("LOTTYPE").SetIsHidden();

            grdMain.View.AddTextBoxColumn("MEASUREDATETIME", 160).SetTextAlignment(TextAlignment.Left);
            grdMain.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 100).SetTextAlignment(TextAlignment.Left);
            grdMain.View.AddTextBoxColumn("AREANAME", 150).SetTextAlignment(TextAlignment.Left);
            grdMain.View.AddTextBoxColumn("FACTORYID", 150).SetLabel("FACTORYNAME").SetTextAlignment(TextAlignment.Left);
            grdMain.View.AddTextBoxColumn("EQUIPMENTUNIT", 100).SetTextAlignment(TextAlignment.Left);
            grdMain.View.AddTextBoxColumn("CUSTOMERNAME", 100).SetLabel("COMPANYCLIENT").SetTextAlignment(TextAlignment.Left);
            grdMain.View.AddTextBoxColumn("PRODUCTDEFID", 120).SetTextAlignment(TextAlignment.Left);
            grdMain.View.AddTextBoxColumn("PRODUCTDEFNAME", 180).SetTextAlignment(TextAlignment.Left);
            grdMain.View.AddTextBoxColumn("PRODUCTDEFVERSION", 100).SetTextAlignment(TextAlignment.Center);
            grdMain.View.AddTextBoxColumn("REWORKCOUNT", 60).SetTextAlignment(TextAlignment.Right);
            grdMain.View.AddTextBoxColumn("SUBNAME", 100).SetLabel("PROCESSPRICETYPE").SetTextAlignment(TextAlignment.Left);
            grdMain.View.AddTextBoxColumn("LOTID", 180).SetTextAlignment(TextAlignment.Left);
            grdMain.View.AddTextBoxColumn("DAITEMID", 130).SetLabel("MEASUREITEM").SetTextAlignment(TextAlignment.Left);
            grdMain.View.AddTextBoxColumn("DAITEMNAME", 100).SetLabel("MEASUREITEMNAME").SetTextAlignment(TextAlignment.Left);
            grdMain.View.AddTextBoxColumn("SPECRANGE", 130).SetTextAlignment(TextAlignment.Left);
            grdMain.View.AddTextBoxColumn("INSPECTIONRESULT", 60).SetTextAlignment(TextAlignment.Left);
            grdMain.View.AddTextBoxColumn("AVERAGEVALUE", 50).SetTextAlignment(TextAlignment.Right);
            grdMain.View.AddTextBoxColumn("MAXVALUE", 50).SetTextAlignment(TextAlignment.Right);
            grdMain.View.AddTextBoxColumn("MINVALUE", 50).SetTextAlignment(TextAlignment.Right);
            grdMain.View.AddTextBoxColumn("DEVIATION", 50).SetTextAlignment(TextAlignment.Right);

            // 2020-12-10 오근영 GRID 항목(헤더 및 데이타) 50개 추가(60 -> 110)
			// for (int i = 0; i < 60; i++)
			for (int i = 0; i < 110; i++)
            {
                grdMain.View.AddTextBoxColumn(string.Concat("MEASUREVALUE", "_", i + 1), 50)
                            .SetLabel(string.Concat(Language.Get("MEASURVALUE"), " ", i + 1))
                            .SetTextAlignment(TextAlignment.Right);
            }

            grdMain.View.PopulateColumns();
            grdMain.View.BestFitColumns();
            grdMain.View.SetIsReadOnly();

            grdMain.ShowStatusBar = true;
            grdMain.GridButtonItem = GridButtonItem.Export;
        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            //! Load event
            this.Load += (s, e) =>
            {
                if (DefectMapHelper.IsNull(_params))
                {
                    return;
                }

                popupMeasureValueRegistration popup = new popupMeasureValueRegistration()
                {
                    StartPosition = FormStartPosition.CenterParent,
                    LotId = Format.GetString(_params["LOTID"])
                };

                if (popup.ShowDialog().Equals(DialogResult.OK))
                {
                    AgainSearch();
                }
            };

            //! Grid 더블 클릭 이벤트
            grdMain.View.DoubleClick += (s, e) =>
            {
                if (grdMain.View.GetFocusedDataRow() is DataRow dr)
                {
                    popupMeasureValueRegistration popup = new popupMeasureValueRegistration()
                    {
                        StartPosition = FormStartPosition.CenterParent,
                        CurrentDataRow = dr
                    };

                    if (popup.ShowDialog().Equals(DialogResult.OK))
                    {
                        AgainSearch();
                    }
                }
            };

            //! 결과가 NG면 색상변경
            grdMain.View.RowStyle += (s, e) =>
            {
                if (Format.GetString(grdMain.View.GetRowCellValue(e.RowHandle, "INSPECTIONRESULT")) is string result)
                {
                    e.Appearance.ForeColor = result.Equals("OK") ? e.Appearance.ForeColor : Color.Red;
                }
            };
        }

        #endregion

        #region 툴바

        public override void LoadForm(Dictionary<string, object> parameters)
        {
            base.LoadForm(parameters);

            _params = parameters;
        }

        /// <summary>
        /// 등록 버튼 Toolbar 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            base.OnToolbarClick(sender, e);

            if (Format.GetString((sender as SmartButton).Name).Equals("Regist"))
            {
                popupMeasureValueRegistration popup = new popupMeasureValueRegistration()
                {
                    StartPosition = FormStartPosition.CenterParent
                };

                if (popup.ShowDialog().Equals(DialogResult.OK))
                {
                    AgainSearch();
                }
            }
        }

        #endregion

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
                grdMain.View.ClearDatas();

                if (await SqlExecuter.QueryAsync("GetQualitySpecification", "10001"
                                                , DefectMapHelper.AddLanguageTypeToConditions(Conditions.GetValues())) is DataTable dt)
                {
                    if (dt.Rows.Count.Equals(0))
                    {
                        ShowMessage("NoSelectData");
                        return;
                    }

                    grdMain.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                //throw MessageException.Create(ex.ToString());

                var values = DefectMapHelper.AddLanguageTypeToConditions(Conditions.GetValues());
                string dateStart = values["P_PERIOD_PERIODFR"].ToSafeString();
                string dateEnd = values["P_PERIOD_PERIODTO"].ToSafeString();

                List<QcConditionDateDays> rtnlstDay;
                rtnlstDay = SpcConditionList(dateStart, dateEnd);

                foreach (QcConditionDateDays days in rtnlstDay)
                {
                    rtnlstHour = SpcConditionListHour(days.DateStart, days.DateEnd);
                    foreach (QcConditionDateDays hours in rtnlstHour)
                    {
                        dtInputHour = null;
                        values["P_PERIOD_PERIODFR"] = hours.DateStart;
                        values["P_PERIOD_PERIODTO"] = hours.DateEnd;
                        try
                        {
                            dtInputHour = await SqlExecuter.QueryAsync("GetQualitySpecification", "10001", values);

                            //for (int i = 0; i < dtInputHour.Rows.Count; i++)
                            //{
                                //DataRow rowData = dtInputHour.Rows[i];
                                dtInputFirstData.Merge(dtInputHour);
                            //}
                        }
                        catch (Exception exh)
                        {
                            throw MessageException.Create(ex.ToString());
                        }

                    }
                }

                if (dtInputFirstData.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
                {
                    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                }

                grdMain.DataSource = dtInputFirstData;
            }
            finally
            {
                DialogManager.CloseWaitArea(this.pnlContent);
            }
        }

        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            Dictionary<string, object> param = new Dictionary<string, object>
            {
                { "LANGUAGETYPE", UserInfo.Current.LanguageType },
                { "P_USERID", UserInfo.Current.Id }
            };

            #region 작업장

            var areaIdPopup = Conditions.AddSelectPopup("p_areaId", new SqlQuery("GetAuthorityUserUseArea", "10001", param), "AREANAME", "AREAID")
                                        .SetPopupLayout(Language.Get("SELECTAREAID"), PopupButtonStyles.Ok_Cancel, true, true)
                                        .SetPopupResultCount(1)
                                        .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                                        .SetPopupAutoFillColumns("AREANAME")
                                        .SetLabel("AREA")
                                        .SetPosition(3);

            areaIdPopup.Conditions.AddTextBox("AREAIDNAME").SetLabel("AREAIDNAME");

            areaIdPopup.GridColumns.AddTextBoxColumn("AREAID", 150).SetValidationKeyColumn();
            areaIdPopup.GridColumns.AddTextBoxColumn("AREANAME", 200);

            #endregion

            #region 고객사

            var customerPopup = Conditions.AddSelectPopup("p_Customer", new SqlQuery("GetCustomerList", "10001", param), "CUSTOMERNAME", "CUSTOMERID")
                                          .SetPopupLayout("COMPANYCLIENT", PopupButtonStyles.Ok_Cancel, true, true)
                                          .SetPopupLayoutForm(400, 600)
                                          .SetPopupResultCount(1)
                                          .SetPosition(4)
                                          .SetLabel("COMPANYCLIENT")
                                          .SetPopupAutoFillColumns("TXTCUSTOMERID");

            customerPopup.Conditions.AddTextBox("TXTCUSTOMERID").SetLabel("COMPANYCLIENT");

            customerPopup.GridColumns.AddTextBoxColumn("CUSTOMERID", 150);
            customerPopup.GridColumns.AddTextBoxColumn("CUSTOMERNAME", 200);

            #endregion

            #region 공정

            var processSegmentIdPopup = this.Conditions.AddSelectPopup("P_PROCESSSEGMENTID", new SqlQuery("GetProcessSegmentList", "10002", $"PLANTID={UserInfo.Current.Plant}", "PROCESSSEGMENTCLASSTYPE=MiddleProcessSegmentClass", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PROCESSSEGMENTNAME", "PROCESSSEGMENTID")
                .SetPopupLayout(Language.Get("SELECTPROCESSSEGMENTID"), PopupButtonStyles.Ok_Cancel, true, true)
                .SetPopupResultCount(1)
                .SetPopupLayoutForm(800, 800, FormBorderStyle.FixedToolWindow)
                .SetPopupAutoFillColumns("PROCESSSEGMENTNAME")
                .SetLabel("PROCESSSEGMENT")
                .SetPosition(4.1);

            processSegmentIdPopup.Conditions.AddComboBox("PROCESSSEGMENTCLASSID_MIDDLE", new SqlQuery("GetProcessSegmentClassByType", "10001", $"PLANTID={UserInfo.Current.Plant}", "PROCESSSEGMENTCLASSTYPE=MiddleProcessSegmentClass", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PROCESSSEGMENTCLASSNAME", "PROCESSSEGMENTCLASSID")
                .SetLabel("MIDDLEPROCESSSEGMENT");
            processSegmentIdPopup.Conditions.AddTextBox("PROCESSSEGMENT");


            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME_TOP", 150)
                .SetLabel("LARGEPROCESSSEGMENT");
            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME_MIDDLE", 150)
                .SetLabel("MIDDLEPROCESSSEGMENT");
            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150);
            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);

            #endregion

            #region 검증항목

            // 팝업 컬럼설정
            var areaPopup = Conditions.AddSelectPopup("P_INSPITEMID", new SqlQuery("GetQCQualitySpecificationItem", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "INSPITEMNAME", "INSPITEMID")
               .SetPopupLayout("INSPITEM", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupLayoutForm(500, 600)
               .SetLabel("VERIFICATIONITEM")
               .SetPopupResultCount(1)
               .SetPosition(4.2);

            // 팝업 조회조건
            areaPopup.Conditions.AddTextBox("INSPITEM").SetLabel("VERIFICATIONITEM");

            // 팝업 그리드
            areaPopup.GridColumns.AddTextBoxColumn("INSPITEMID", 150)
                .SetLabel("VERIFICATIONITEMID")
                .SetValidationKeyColumn();
            areaPopup.GridColumns.AddTextBoxColumn("INSPITEMNAME", 200).SetLabel("VERIFICATIONITEM");

            #endregion

            #region 품목

            // 팝업 컬럼설정
            var productPopup = Conditions.AddSelectPopup("p_productdefId", new SqlQuery("GetProductListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PRODUCTDEFNAME", "PRODUCTDEFIDVERSION")
               .SetPopupLayout("PRODUCT", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupLayoutForm(600, 800)
               .SetLabel("PRODUCT")
               .SetPopupAutoFillColumns("PRODUCTDEFNAME")
               .SetPopupResultCount(1)
               .SetPosition(4.3);

            // 팝업 조회조건
            productPopup.Conditions.AddTextBox("PRODUCTDEFIDNAME");

            // 팝업 그리드
            productPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 120)
                .SetValidationKeyColumn();
            productPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 220);
            productPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 80);
            productPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFIDVERSION", 100)
                .SetIsHidden();

            #endregion

            #region User

            Conditions.AddTextBox("P_USER").SetIsHidden().SetDefault(UserInfo.Current.Id);

            #endregion
        }

        #endregion

        #region Private Function

        /// <summary>
        /// 재조회 
        /// </summary>
        private async void AgainSearch()
        {
            await OnSearchAsync();
        }

        /// <summary>
        /// 조회 조건중 일자 List 반환 함수.
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        private static List<QcConditionDateDays> SpcConditionList(string startDate, string endDate)
        {
            List<QcConditionDateDays> rtnlstDay = new List<QcConditionDateDays>();

            try
            {

                //DateTime dateStart = Convert.ToDateTime(param["P_PERIOD_PERIODFR"]); //2020-02-01 08:30:00
                //DateTime dateEnd = Convert.ToDateTime(param["P_PERIOD_PERIODTO"]);//2020-03-01 08:30:00

                DateTime dateStart;     //Convert.ToDateTime("2020-02-01 08:30:00");
                DateTime dateEnd;       //Convert.ToDateTime("2020-02-10 08:30:00");
                DateTime dateStartAdd;
                DateTime dateStartEnd;

                dateStart = Convert.ToDateTime(startDate);
                dateEnd = Convert.ToDateTime(endDate);


                TimeSpan ts = dateEnd - dateStart;

                int diffDay = ts.Days;  //날짜의 차이 구하기
                if (diffDay <= 0)
                {
                    diffDay = 1;
                }

                for (int i = 0; i < diffDay; i++)
                {
                    dateStartAdd = dateStart.AddDays(i);
                    dateStartEnd = dateStart.AddDays(i + 1);
                    //Console.WriteLine(dateStartAdd.ToString("yyyy-MM-dd HH:mm:ss"));
                    QcConditionDateDays tempDays = new QcConditionDateDays();
                    if (i != 0)
                    {
                        tempDays.DateStart = dateStartAdd.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    else
                    {
                        tempDays.DateStart = dateStartAdd.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    if (i < diffDay - 1)
                    {
                        tempDays.DateEnd = dateStartEnd.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    else
                    {
                        tempDays.DateEnd = dateEnd.ToString("yyyy-MM-dd HH:mm:ss");
                    }

                    rtnlstDay.Add(tempDays);

                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return rtnlstDay;
        }

        /// <summary>
        /// 조회 조건 시간별 List 함수.
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        private static List<QcConditionDateDays> SpcConditionListHour(string startDate, string endDate)
        {
            List<QcConditionDateDays> rtnlstHour = new List<QcConditionDateDays>();

            try
            {

                //DateTime dateStart = Convert.ToDateTime(param["P_PERIOD_PERIODFR"]); //2020-02-01 08:30:00
                //DateTime dateEnd = Convert.ToDateTime(param["P_PERIOD_PERIODTO"]);//2020-03-01 08:30:00

                DateTime HourStart;     //Convert.ToDateTime("2020-02-01 08:30:00");
                DateTime HourEnd;       //Convert.ToDateTime("2020-02-10 08:30:00");
                DateTime HourStartAdd;
                DateTime HourStartEnd;

                DateTime dateEndTemp;
                DateTime dateEndFirst;
                int addHours = 0;
                HourStart = Convert.ToDateTime(startDate);
                HourEnd = Convert.ToDateTime(endDate);
                string endFirst = HourEnd.ToString("yyyy-MM-dd HH:mm:ss");
                dateEndFirst = Convert.ToDateTime(endFirst);

                TimeSpan ts = HourEnd - HourStart;
                TimeSpan tsEnd = HourEnd - dateEndFirst;

                int diffHour = 0;
                if (tsEnd.Hours > 0)
                {
                    addHours = tsEnd.Hours;
                    ts = dateEndFirst - HourStart;
                    if (ts.Hours == 0)
                    {
                        diffHour = 24;
                    }
                    diffHour = diffHour + addHours + 1;  //시간의 차이 구하기
                }
                else
                {
                    diffHour = ts.Hours + 1;  //시간의 차이 구하기
                }


                if (diffHour <= 1)
                {
                    if (ts.Hours == 00 && ts.Days == 1)
                    {
                        diffHour = 24;
                    }
                    else
                    {
                        diffHour = 1;
                    }
                }

                for (int i = 0; i < diffHour; i++)
                {
                    HourStartAdd = HourStart.AddHours(i);
                    HourStartEnd = HourStart.AddHours(i + 1);
                    //Console.WriteLine(dateStartAdd.ToString("yyyy-MM-dd HH:mm:ss"));
                    QcConditionDateDays tempHours = new QcConditionDateDays();
                    if (i != 0)
                    {
                        tempHours.DateStart = HourStartAdd.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    else
                    {
                        tempHours.DateStart = HourStartAdd.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    if (i < diffHour - 1)
                    {
                        dateEndTemp = HourStart.AddHours(i + 1);
                        tempHours.DateEnd = dateEndTemp.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    else
                    {
                        tempHours.DateEnd = string.Format("{0}", HourEnd.ToString("yyyy-MM-dd HH:mm:ss"));
                    }

                    rtnlstHour.Add(tempHours);

                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return rtnlstHour;
        }

        #endregion
    }

    public class QcConditionDateDays
    {
        public string DateStart;
        public string DateEnd;
    }
}
