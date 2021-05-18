#region using

using DevExpress.XtraGrid.Views.Grid;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Grid.BandedGrid;
using Micube.SmartMES.Commons.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

#endregion

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 공정관리 > 생산실적 > 인시생산성
    /// 업  무  설  명  : 
    /// 생    성    자  : 황유성
    /// 생    성    일  : 2019-11-11
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class HumanTimeProductivity : SmartConditionManualBaseForm
	{
        #region ◆ Local Variables |
        private const int DAYS_IN_WEEK = 7;
        #endregion

        #region ◆ 생성자 |

        public HumanTimeProductivity()
		{
			InitializeComponent();
		}

        #endregion

        #region ◆ 컨텐츠 영역 초기화 |

        /// <summary>
        /// 화면의 컨텐츠 영역을 초기화한다.
        /// </summary>
        protected override void InitializeContent()
		{
			base.InitializeContent();

			InitializeEvent();

			// TODO : 컨트롤 초기화 로직 구성
			InitializeGrid();
		}

        #region ▶ Grid 설정 |
        /// <summary>        
        /// 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            #region - 인시당 상세 |
            grdHumanTimeDetailList.GridButtonItem = GridButtonItem.Export;
            grdHumanTimeDetailList.ShowStatusBar = false;
            grdHumanTimeDetailList.View.SetIsReadOnly();

            var grpBaseInfo = grdHumanTimeDetailList.View.AddGroupColumn("");
            // 협력사 / 작업장명
            grpBaseInfo.AddTextBoxColumn("VENDORNAME", 140);
            // 자사/협력사 구분
            grpBaseInfo.AddTextBoxColumn("PROCESSOWNTYPE", 60).SetLabel("OWNTYPE").SetTextAlignment(TextAlignment.Center);
            // 근무지
            grpBaseInfo.AddTextBoxColumn("WORKFACTORY", 60).SetLabel("FACTORY").SetTextAlignment(TextAlignment.Center);
            // 인시당 집계그룹
            grpBaseInfo.AddTextBoxColumn("PROCESSGROUPID", 60).SetTextAlignment(TextAlignment.Center);
            // 인시당 집계그룹명
            grpBaseInfo.AddTextBoxColumn("PROCESSGROUPNAME", 100);
            // 직/간접 구분
            grpBaseInfo.AddTextBoxColumn("DIRECTTYPE", 60).SetTextAlignment(TextAlignment.Center);
            // 직무코드
            grpBaseInfo.AddTextBoxColumn("DUTYID", 60).SetTextAlignment(TextAlignment.Center);
            // 직무명
            grpBaseInfo.AddTextBoxColumn("DUTYNAME", 100);
            // 작업일
            grpBaseInfo.AddTextBoxColumn("WORKDATE", 80).SetTextAlignment(TextAlignment.Center);
            //구분(주/야)
            grpBaseInfo.AddTextBoxColumn("DAYNIGHTTYPE", 60).SetTextAlignment(TextAlignment.Center);
            //재적
            grpBaseInfo.AddSpinEditColumn("TOTALWORKER", 110).SetDisplayFormat("#,##0.00", MaskTypes.Numeric, false);
            //출근
            grpBaseInfo.AddSpinEditColumn("NORMALWORK", 110).SetDisplayFormat("#,##0.00", MaskTypes.Numeric, false);
            //전일재공(PNL)
            grpBaseInfo.AddSpinEditColumn("LASTDAYWIPPNL", 110).SetDisplayFormat("#,##0.00", MaskTypes.Numeric, false);
            //전일재공(M2)
            grpBaseInfo.AddSpinEditColumn("LASTDAYWIPMM", 110).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true);
            //전일실적(PNL)
            grpBaseInfo.AddSpinEditColumn("LASTDAYRESULTPNL", 110).SetDisplayFormat("#,##0", MaskTypes.Numeric, false);
            //전일실적(M2)
            grpBaseInfo.AddSpinEditColumn("LASTDAYRESULTMM", 110).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true);
            //인시당(PNL)
            grpBaseInfo.AddSpinEditColumn("PNLPERHT", 110).SetDisplayFormat("#,##0", MaskTypes.Numeric, false);
            //인시당(M2)
            grpBaseInfo.AddSpinEditColumn("M2PERHT", 110).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true);
            //총 근무시간
            grpBaseInfo.AddSpinEditColumn("TOTALWORKTIME", 110).SetDisplayFormat("#,##0.00", MaskTypes.Numeric, false);
            //정상
            grpBaseInfo.AddSpinEditColumn("NORMALWORK", 110).SetDisplayFormat("#,##0.00", MaskTypes.Numeric, false);
            //잔업
            grpBaseInfo.AddSpinEditColumn("OVERTIMEWORK", 110).SetDisplayFormat("#,##0.00", MaskTypes.Numeric, false);
            //특근
            grpBaseInfo.AddSpinEditColumn("EXTRAWORK", 110).SetDisplayFormat("#,##0.00", MaskTypes.Numeric, false);
            //특근잔업
            grpBaseInfo.AddSpinEditColumn("EXTRAOVERWORK", 110).SetDisplayFormat("#,##0.00", MaskTypes.Numeric, false);

            //지원현황
            var grpSupportState = grdHumanTimeDetailList.View.AddGroupColumn("AID");
            //정상
            grpSupportState.AddSpinEditColumn("AIDNORMAL", 110).SetDisplayFormat("#,##0.00", MaskTypes.Numeric, false).SetLabel("NORMALWORK");
            //잔업
            grpSupportState.AddSpinEditColumn("AIDOVERTIMEWORK", 110).SetDisplayFormat("#,##0.00", MaskTypes.Numeric, false).SetLabel("OVERTIMEWORK");
            //특근
            grpSupportState.AddSpinEditColumn("AIDEXTRAWORK", 110).SetDisplayFormat("#,##0.00", MaskTypes.Numeric, false).SetLabel("EXTRAWORK");
            //특근잔업
            grpSupportState.AddSpinEditColumn("AIDEXTRAOVERWORK", 110).SetDisplayFormat("#,##0.00", MaskTypes.Numeric, false).SetLabel("EXTRAOVERWORK");

            var grpEtcInfo = grdHumanTimeDetailList.View.AddGroupColumn("");

            grpEtcInfo.AddTextBoxColumn("REGISTERTYPE", 80);

            grdHumanTimeDetailList.View.PopulateColumns();
            #endregion

            #region - 일별 공정현황 |
            InitializeGridDailySegment();
            #endregion

            #region - 일별 협력사 |
            InitializeGridDailyVendor();

            #endregion

            #region - 엑셀업로드 |
            grdExcelUpLoad.GridButtonItem = GridButtonItem.Import | GridButtonItem.Export;
            grdExcelUpLoad.View.SetIsReadOnly();

            grdExcelUpLoad.View.AddTextBoxColumn("PLANTID", 100);
            grdExcelUpLoad.View.AddTextBoxColumn("WORKDATE", 100).SetDisplayFormat("yyyy-MM-dd");
            grdExcelUpLoad.View.AddTextBoxColumn("WORKFACTORY", 100).SetLabel("FACTORY").SetTextAlignment(TextAlignment.Center);
            grdExcelUpLoad.View.AddTextBoxColumn("VENDORID", 100).SetDisplayFormat("{0}", MaskTypes.None);
            grdExcelUpLoad.View.AddTextBoxColumn("VENDORNAME", 100);
            grdExcelUpLoad.View.AddTextBoxColumn("PROCESSGROUPID", 100).SetDisplayFormat("{0}", MaskTypes.None);
            grdExcelUpLoad.View.AddTextBoxColumn("PROCESSGROUPNAME", 100);
            grdExcelUpLoad.View.AddTextBoxColumn("PROCESSOWNTYPE", 100).SetLabel("OWNTYPE").SetTextAlignment(TextAlignment.Center);
            grdExcelUpLoad.View.AddTextBoxColumn("DUTYID", 100).SetDisplayFormat("{0}", MaskTypes.None);
            grdExcelUpLoad.View.AddTextBoxColumn("DUTYNAME", 100);
            grdExcelUpLoad.View.AddTextBoxColumn("DIRECTTYPE", 100).SetDisplayFormat("{0}", MaskTypes.None);
            grdExcelUpLoad.View.AddTextBoxColumn("DAYNIGHTTYPE", 100);
            grdExcelUpLoad.View.AddTextBoxColumn("TOTALWORKER", 100);
            grdExcelUpLoad.View.AddTextBoxColumn("NORMALWORK", 100);
            grdExcelUpLoad.View.AddTextBoxColumn("OVERTIMEWORK", 100);
            grdExcelUpLoad.View.AddTextBoxColumn("EXTRAWORK", 100);
            grdExcelUpLoad.View.AddTextBoxColumn("EXTRAOVERWORK", 100);
            grdExcelUpLoad.View.AddTextBoxColumn("AIDNORMAL", 100);
            grdExcelUpLoad.View.AddTextBoxColumn("AIDOVERTIMEWORK", 100);
            grdExcelUpLoad.View.AddTextBoxColumn("AIDEXTRAWORK", 100);
            grdExcelUpLoad.View.AddTextBoxColumn("AIDEXTRAOVERWORK", 100);

            grdExcelUpLoad.View.PopulateColumns();
            #endregion
        } 

        private void InitializeGridDailySegment()
        {
            grdDailySegment.GridButtonItem = GridButtonItem.Export;
            grdDailySegment.View.ClearColumns();
            grdDailySegment.View.SetIsReadOnly();
            grdDailySegment.View.OptionsView.AllowCellMerge = true;
            grdDailySegment.View.AddTextBoxColumn("VENDORNAME", 125);
            grdDailySegment.View.AddTextBoxColumn("PROCESSGROUPNAME", 165);
            grdDailySegment.View.AddTextBoxColumn("CATEGORYNAME", 100);
            grdDailySegment.View.AddTextBoxColumn("ITEMNAME", 90).SetLabel("ATTRIBUTECODE");
            grdDailySegment.View.AddTextBoxColumn("UNITNAME", 60).SetLabel("UNIT");
            grdDailySegment.View.AddSpinEditColumn("ACCUMULATE", 100).SetDisplayFormat("#,##0.##");

            grdDailySegment.View.AddTextBoxColumn("AREAID").SetIsHidden();
        }

        private void InitializeGridDailyVendor()
        {
            grdDailyVendor.GridButtonItem = GridButtonItem.Export;
            grdDailyVendor.View.ClearColumns();
            grdDailyVendor.View.SetIsReadOnly();
            grdDailyVendor.View.OptionsView.AllowCellMerge = true;
            grdDailyVendor.View.AddTextBoxColumn("VENDORNAME", 125);
            grdDailyVendor.View.AddTextBoxColumn("CATEGORY", 100).SetLabel("CATEGORYNAME");
            grdDailyVendor.View.AddTextBoxColumn("ITEMNAME", 90).SetLabel("ATTRIBUTECODE");
            grdDailyVendor.View.AddTextBoxColumn("UNITNAME", 60).SetLabel("UNIT");

            grdDailyVendor.View.AddTextBoxColumn("AREAID").SetIsHidden();
        }
        #endregion

        #endregion

        #region ◆ Event |

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
		{
            grdDailySegment.View.CellMerge += grdDailySegmentView_CellMerge;
            grdDailyVendor.View.CellMerge += grdDailyVendorView_CellMerge;
            grdExcelUpLoad.HeaderButtonClickEvent += GrdExcelUpLoad_HeaderButtonClickEvent;
		}

        private void GrdExcelUpLoad_HeaderButtonClickEvent(object sender, HeaderButtonClickArgs args)
        {
            if (args.ClickItem == GridButtonItem.Import)
            {
                DataTable orgTable = grdExcelUpLoad.DataSource as DataTable;
                DataTable newTable = orgTable.Clone();
                newTable.Columns["VENDORID"].DataType = typeof(string);
                newTable.Columns["PROCESSGROUPID"].DataType = typeof(string);
                newTable.Columns["DUTYID"].DataType = typeof(string);
                newTable.Columns["DIRECTTYPE"].DataType = typeof(string);

                foreach(DataRow row in orgTable.Rows)
                {
                    newTable.ImportRow(row);
                }

                grdExcelUpLoad.DataSource = newTable;
            }
        }

        /// <summary>
        /// 사용자 지정 Cell Merge
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdDailySegmentView_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            GridView view = sender as GridView;

            if (view == null)
            {
                return;
            }

            if (e.Column.FieldName == "VENDORNAME" || e.Column.FieldName == "PROCESSSEGMENTNAME" || e.Column.FieldName == "CATEGORYNAME")
            {
                string str1 = view.GetRowCellDisplayText(e.RowHandle1, e.Column);
                string str2 = view.GetRowCellDisplayText(e.RowHandle2, e.Column);

                string area1 = view.GetRowCellValue(e.RowHandle1, "VENDORID").ToString();
                string area2 = view.GetRowCellValue(e.RowHandle2, "VENDORID").ToString();

                string segment1 = view.GetRowCellValue(e.RowHandle1, "PROCESSGROUPID").ToString();
                string segment2 = view.GetRowCellValue(e.RowHandle2, "PROCESSGROUPID").ToString();

                e.Merge = (str1 == str2 && area1 == area2 && segment1 == segment2);
            }
            else
            {
                e.Merge = false;
            }
            e.Handled = true;
        }

        private void grdDailyVendorView_CellMerge(object sender, CellMergeEventArgs e)
        {
            GridView view = sender as GridView;

            if (view == null)
            {
                return;
            }

            if (e.Column.FieldName == "VENDORNAME" || e.Column.FieldName == "CATEGORY")
            {
                string str1 = view.GetRowCellDisplayText(e.RowHandle1, e.Column);
                string str2 = view.GetRowCellDisplayText(e.RowHandle2, e.Column);

                string area1 = view.GetRowCellValue(e.RowHandle1, "VENDORID").ToString();
                string area2 = view.GetRowCellValue(e.RowHandle2, "VENDORID").ToString();

                e.Merge = (str1 == str2 && area1 == area2);
            }
            else
            {
                e.Merge = false;
            }
            e.Handled = true;
        }
        #endregion

        #region ◆ 툴바 |

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            DataTable dt = grdExcelUpLoad.DataSource as DataTable;

            MessageWorker worker = new MessageWorker("SaveLaborProductivity");
            worker.SetBody(new MessageBody()
            {
                { "enterpriseid", UserInfo.Current.Enterprise },
                { "datalist", dt }
            });

            worker.Execute();

            grdExcelUpLoad.View.ClearDatas();
        }
        #endregion

        #region ◆ 유효성 검사 |

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            DataTable dt = grdExcelUpLoad.DataSource as DataTable;

            if (dt == null || dt.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
            // TOTALWORKER
            // NORMALWORK
            /*
            if (dt.AsEnumerable().Count(r => r.IsNull("AREAID")) > 0 )
            {
                // 선택된 작업장이 없습니다.
                throw MessageException.Create("NoAreaSelected");
            }

            var rx = new Regex("([0-9]{4})-*([0-9]{2})-*([0-9]{2})", RegexOptions.IgnoreCase);

            if (dt.AsEnumerable().Count(r => !rx.IsMatch(r.Field<object>("WORKDATE").ToString())) > 0)
            {
                // 잘못된날짜데이터입니다.
                throw MessageException.Create("ValidateMWONONEDATE");
            }

            if (dt.AsEnumerable().Count(r => r.IsNull("DAYNIGHTTYPE")) > 0 ||
                dt.AsEnumerable().Count(r => !r.Field<string>("DAYNIGHTTYPE").Equals("DAY") && !r.Field<string>("DAYNIGHTTYPE").Equals("NIGHT")) > 0 )
            {
                // 잘못된 근무구분 데이터입니다.
                throw MessageException.Create("InValidShiftType");
            }

            if (dt.AsEnumerable().Count(r => r.IsNull("TOTALWORKER")) > 0)
            {
                // 재적인원을 입력하여 주십시오
                throw MessageException.Create("NoTOTALWORKER");
            }
            else if(dt.AsEnumerable().Count(r => r.IsNull("NORMALWORK")) > 0)
            {
                // 출근인원을 입력하여 주십시오.
                throw MessageException.Create("NoNORMALWORK");
            }
            */
        }

        #endregion

        #region ◆ 검색 |

        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
		{
			await base.OnSearchAsync();

            var values = Conditions.GetValues();
            // DateTime 파라미터 -> yyyy-MM-dd 로 변환
            values["P_PERIOD_PERIODFR"] = values["P_PERIOD_PERIODFR"].ToString().Substring(0, 10);
            values["P_PERIOD_PERIODTO"] = DateTime.Parse(values["P_PERIOD_PERIODTO"].ToString().Substring(0, 10)).AddDays(-1).ToString("yyyy-MM-dd");
            if (DateTime.Parse(values["P_PERIOD_PERIODFR"].ToString().Substring(0, 10)) >= DateTime.Parse(values["P_PERIOD_PERIODTO"].ToString().Substring(0, 10)))
            {
                values["P_PERIOD_PERIODTO"] = values["P_PERIOD_PERIODFR"];
            }

            /*
            // TODO : 조회 SP 변경
            var values = Conditions.GetValues();  
            values["P_STANDARDDATE"] = values["P_STANDARDDATE"].ToString().Substring(0, 10);
            */

            int index = tabHumanTimeProductivity.SelectedTabPageIndex;

			switch(index)
			{
				case 0://인시당 상세
                    values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                    DataTable dtHumanTimeDetail = await SqlExecuter.QueryAsync("SelectLaborProductivityDetail", "10001", values);

					if (dtHumanTimeDetail.Rows.Count < 1)
					{
						// 조회할 데이터가 없습니다.
						ShowMessage("NoSelectData");
					}

					grdHumanTimeDetailList.DataSource = dtHumanTimeDetail;
					break;
				case 1://일별현황(공정)
                    values.Add("P_LANGUAGETYPE", UserInfo.Current.LanguageType);
                    values.Add("P_USERID", UserInfo.Current.Id);
                    InitializeGridDailySegment();
                    AddDateColumnsToGrid(grdDailySegment.View, DateTime.Parse(values["P_PERIOD_PERIODFR"].ToString())
                        , DateTime.Parse(values["P_PERIOD_PERIODTO"].ToString()));
                    grdDailySegment.View.PopulateColumns();
                    grdDailySegment.View.FixColumn(new string[] { "VENDORNAME", "PROCESSGROUPNAME", "CATEGORYNAME", "ITEMNAME", "UNITNAME" });

                    DataTable dtHumanTimeSegment = await ProcedureAsync("usp_wip_selecthumantimeproductivitybyprocess", values);

					if (dtHumanTimeSegment.Rows.Count < 1)
					{
						// 조회할 데이터가 없습니다.
						ShowMessage("NoSelectData");
					}

					grdDailySegment.DataSource = dtHumanTimeSegment;
					break;
				case 2://일별현황(협력사)
                    values.Add("P_LANGUAGETYPE", UserInfo.Current.LanguageType);
                    values.Add("P_USERID", UserInfo.Current.Id);
                    InitializeGridDailyVendor();

                    // TODO : HERE

                    // 최근 3개월
                    AddMonthColumnsToGrid(grdDailyVendor.View, DateTime.Parse(values["P_PERIOD_PERIODFR"].ToString()), DateTime.Parse(values["P_PERIOD_PERIODTO"].ToString()));

                    // 최근 3주
                    AddWeekColumnsToGrid(grdDailyVendor.View, DateTime.Parse(values["P_PERIOD_PERIODFR"].ToString()), DateTime.Parse(values["P_PERIOD_PERIODTO"].ToString()));

                    // 최근 7일
                    AddDateColumnsToGrid(grdDailyVendor.View, DateTime.Parse(values["P_PERIOD_PERIODFR"].ToString()), DateTime.Parse(values["P_PERIOD_PERIODTO"].ToString()));

                    grdDailyVendor.View.PopulateColumns();
                    grdDailyVendor.View.FixColumn(new string[] { "VENDORNAME", "CATEGORY", "ITEMNAME", "UNITNAME" });

                    DataTable dtHumanTimeVendor = await ProcedureAsync("usp_wip_selecthumantimeproductivitybyvendor", values);

                    if (dtHumanTimeVendor.Rows.Count < 1)
                    {
                        // 조회할 데이터가 없습니다.
                        ShowMessage("NoSelectData");
                    }

                    grdDailyVendor.DataSource = dtHumanTimeVendor;
                    break;
			}
		}

		/// <summary>
		/// 조회조건 항목을 추가한다.
		/// </summary>
		protected override void InitializeCondition()
		{
			base.InitializeCondition();

			// TODO : 조회조건 추가 구성이 필요한 경우 사용

			//협력사
			InitializeCondition_VendorList();
			//공정
			Commons.CommonFunction.AddConditionProcessSegmentPopup("P_PROCESSSEGMENTID", 3.1, true, Conditions);
		}

		/// <summary>
		/// 조회조건의 컨트롤을 추가한다.
		/// </summary>
		protected override void InitializeConditionControls()
		{
			base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
        }

		/// <summary>
		/// 팝업형 조회조건 - 협력사
		/// </summary>
		private void InitializeCondition_VendorList()
		{
			var vendorCondition = Conditions.AddSelectPopup("P_VENDORID", new SqlQuery("GetVendorList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "VENDORNAME", "VENDORID")
				.SetPopupLayout("SELECTVENDORID", PopupButtonStyles.Ok_Cancel, true, true)
				.SetPopupLayoutForm(800, 800)
				.SetPopupAutoFillColumns("VENDORNAME")
				.SetLabel("PARTNERS")
				.SetPopupResultCount(0)
				.SetPosition(1.2);

			vendorCondition.Conditions.AddTextBox("VENDORID");

			vendorCondition.GridColumns.AddTextBoxColumn("VENDORID", 150);
			vendorCondition.GridColumns.AddTextBoxColumn("VENDORNAME", 250);
		}

        #endregion

        #region ◆ Private Function |

        /// <summary>
        /// 그리드에 월별 컬럼들을 추가한다.
        /// </summary>
        /// <param name="view"></param>
        /// <param name="standardDate">기준일자</param>
        /// <param name="months">기준일자 기준으로 몇월 전까지 추가할 것인지</param>
        private void AddMonthColumnsToGrid(SmartBandedGridView view, DateTime fromDate, DateTime toDate)
        {
            foreach (DateTime month in EachMonth(fromDate, toDate))
            {
                view.AddSpinEditColumn(month.ToString("yyyy-MM"), 100).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);
            }
        }

        /// <summary>
        /// fromDate 부터 months 만큼의 일자들을 반환한다(1개월 간격)
        /// </summary>
        /// <param name="fromDate">시작일자</param>
        /// <param name="months">몇개월을 생성할지</param>
        /// <returns></returns>
        private IEnumerable<DateTime> EachMonth(DateTime fromDate, DateTime toDate)
        {
            for(int i = 0; fromDate.AddMonths(i) <= toDate; i++)
            {
                yield return fromDate.AddMonths(i);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="view"></param>
        /// <param name="standardDate"></param>
        /// <param name="weeks"></param>
        private void AddWeekColumnsToGrid(SmartBandedGridView view, DateTime fromDate, DateTime toDate)
        {
            foreach (DateTime week in EachWeek(fromDate, toDate))
            {
                view.AddSpinEditColumn(ToWeekString(week), 100).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);
            }
        }

        /// <summary>
        /// fromDate 부터 weeks 만큼의 일자들을 반환한다(1주 간격)
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="weeks"></param>
        /// <returns></returns>
        private IEnumerable<DateTime> EachWeek(DateTime fromDate, DateTime toDate)
        {
            for (int i = 0; fromDate.AddDays(i * DAYS_IN_WEEK) <= toDate; i++)
            {
                yield return fromDate.AddDays(i * DAYS_IN_WEEK);
            }
        }

        private string ToWeekString(DateTime date)
        {
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            Calendar cal = dfi.Calendar;
            return "W" + cal.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday).ToString("00");
        }

        /// <summary>
        /// 그리드에 일자별 컬럼들을 추가
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        private void AddDateColumnsToGrid(SmartBandedGridView view, DateTime from, DateTime to)
        {
            foreach (DateTime day in EachDay(from, to))
            {
                view.AddSpinEditColumn(day.ToString("yyyy-MM-dd"), 100).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);
            }
        }

        /// <summary>
        /// from 부터 to까지의 일자들의 컬렉션을 반환한다.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        private IEnumerable<DateTime> EachDay(DateTime from, DateTime to)
        {
            for (var day = from.Date; day.Date <= to.Date; day = day.AddDays(1))
            {
                yield return day;
            }
        }

        #endregion
    }
}