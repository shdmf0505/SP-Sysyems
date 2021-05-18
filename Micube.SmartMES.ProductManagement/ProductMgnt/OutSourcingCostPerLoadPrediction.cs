#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons;

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

using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.BandedGrid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Menu;
using DevExpress.Utils.Menu;
using DevExpress.Spreadsheet;
#endregion

namespace Micube.SmartMES.ProductManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 생산관리 > 공정부하 > 공정부하예측
    /// 업  무  설  명  : 공정부하예측
    /// 생    성    자  : 배선용
    /// 생    성    일  : 2019-09-16
    /// 수  정  이  력  : 2019-10-14 황유성 스프레드시트를 그리드로 변경
    /// 
    /// 
    /// </summary>
    public partial class OutSourcingCostPerLoadPrediction : SmartConditionManualBaseForm
    {
        #region ◆ Local Variables |

        private string loadDate;
        private DateTime toDate;
        private DataTable oriTable;
        #endregion

        #region ◆ 생성자 |
        /// <summary>
        /// 생성자
        /// </summary>
        public OutSourcingCostPerLoadPrediction()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Form Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Form_Load(object sender, EventArgs e)
        {
            InitializeComboBox();
            InitializeGrid();
        }
        #endregion

        #region ◆ 컨텐츠 영역 초기화 |
        /// <summary>
        /// Control 설정
        /// </summary>
        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeEvent();
        }

        #region ▶ 조회조건 설정 |
        /// <summary>
        /// 검색조건 설정
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // 부하일
            Conditions.AddDateEdit("P_LOADDATE")
                .SetPosition(1.5)
                .SetValidationIsRequired()
                .SetDefault(DateTime.Today)
                .SetDisplayFormat("yyyy-MM-dd")
                .SetLabel("LOADDATE");

            // 품목
            CommonFunction.AddConditionProductPopup("P_PRODUCTDEFID", 2.5, true, Conditions);
            Conditions.GetCondition<ConditionItemSelectPopup>("P_PRODUCTDEFID").SetPopupApplySelection((selectedRows, dataGridRows) =>
            {
                List<DataRow> list = selectedRows.ToList<DataRow>();

                List<string> listRev = new List<string>();

                string productlist = string.Empty;
                selectedRows.ForEach(row =>
                {
                    string productid = Format.GetString(row["PRODUCTDEFID"]);
                    string revision = Format.GetString(row["PRODUCTDEFVERSION"]);
                    productlist = productlist + productid + ',';
                    listRev.Add(revision);
                }
                );

                productlist = productlist.TrimEnd(',');

                listRev = listRev.Distinct().ToList();
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                param.Add("P_PLANTID", UserInfo.Current.Plant);
                param.Add("P_PRODUCTDEFID", productlist);

                DataTable dt = SqlExecuter.Query("selectProductdefVesion", "10001", param);

                Conditions.GetControl<SmartComboBox>("P_PRODUCTDEFVERSION").DataSource = dt;
                if (listRev.Count > 0)
                {
                    if (listRev.Count == 1)
                        Conditions.GetControl<SmartComboBox>("P_PRODUCTDEFVERSION").EditValue = Format.GetFullTrimString(listRev[0]);
                    else
                        Conditions.GetControl<SmartComboBox>("P_PRODUCTDEFVERSION").EditValue = Format.GetFullTrimString('*');
                }
            });
            // 담당자
            InitializeConditionUser_Popup();
        }

        /// <summary>
        /// 조회조건 설정
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();
            Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").EditValueChanged += OutSourcingCostPerLoadPrediction_EditValueChanged;
        }

        private void OutSourcingCostPerLoadPrediction_EditValueChanged(object sender, EventArgs e)
        {
            SmartSelectPopupEdit PopProdutid = sender as SmartSelectPopupEdit;

            if (Format.GetFullTrimString(PopProdutid.EditValue).Equals(string.Empty))
            {
                Conditions.GetControl<SmartComboBox>("P_PRODUCTDEFVERSION").EditValue = string.Empty;
            }
        }
        #endregion

        #region ▶ ComboBox 초기화 |
        /// <summary>
        /// 콤보박스를 초기화 하는 함수
        /// </summary>
        private void InitializeComboBox()
        {
            
        }
        #endregion

        #region ▶ Grid 설정 |
        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            #region - 공정 부하 Grid 설정 
            grdLoad.View.ClearColumns();
            grdLoad.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            var groupProcess = grdLoad.View.AddGroupColumn("");

            groupProcess.AddTextBoxColumn("PRODUCTDEFID", 120).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            groupProcess.AddTextBoxColumn("PRODUCTDEFVERSION", 60).SetTextAlignment(TextAlignment.Center).SetIsReadOnly().SetLabel("PRODUCTREVISION");
            groupProcess.AddTextBoxColumn("PRODUCTDEFNAME", 200).SetIsReadOnly();
            groupProcess.AddTextBoxColumn("USERSEQUENCE", 60).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            groupProcess.AddTextBoxColumn("PROCESSSEGMENTID", 80).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            groupProcess.AddTextBoxColumn("PROCESSSEGMENTNAME", 200).SetIsReadOnly();
            groupProcess.AddTextBoxColumn("LOADTOPSEGMENTCLASSID", 130).SetIsReadOnly().SetIsHidden();
            groupProcess.AddTextBoxColumn("LOADMIDDLESEGMENTCLASSID", 130).SetIsReadOnly().SetIsHidden();
            groupProcess.AddTextBoxColumn("LOADSMALLSEGMENTCLASSID", 130).SetIsReadOnly().SetIsHidden();
            groupProcess.AddTextBoxColumn("TARGETPANELQTY", 130).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0").SetIsReadOnly();
            groupProcess.AddTextBoxColumn("TARGETSEGMENTQTY", 130).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0").SetIsReadOnly();
            groupProcess.AddTextBoxColumn("OUTSOUCINGRATIO", 130).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0").SetIsReadOnly();
            groupProcess.AddTextBoxColumn("WIP", 130).SetTextAlignment(TextAlignment.Right).SetIsReadOnly().SetLabel("WIPSTOCK");


            this.grdLoad.View.OptionsCustomization.AllowFilter = false;
            this.grdLoad.View.OptionsCustomization.AllowSort = false;
            this.grdLoad.View.OptionsCustomization.AllowGroup = false;
            this.grdLoad.View.OptionsCustomization.AllowQuickHideColumns = false;
            this.grdLoad.View.OptionsCustomization.AllowBandMoving = false;
            this.grdLoad.View.OptionsCustomization.AllowColumnMoving = false;
            this.grdLoad.View.OptionsCustomization.AllowChangeBandParent = false;
            #endregion
        }

        private void AddDateColumnsToGrid(DateTime from, DateTime to)
        {
            foreach (DateTime day in EachDay(from, to))
            {
                var groupEachDay = grdLoad.View.AddGroupColumn(day.ToString("yyyy-MM-dd"));


                // 공정부하
                ConditionItemTextBox load = groupEachDay.AddTextBoxColumn(day.ToString("yyyy-MM-dd") + "_loadqty", 80)
                    .SetDisplayFormat("#,##0")
                    .SetTextAlignment(TextAlignment.Right);
                load.LanguageKey = "LOADQTY";
                
                // 재공
                ConditionItemTextBox wipStock = groupEachDay.AddTextBoxColumn(day.ToString("yyyy-MM-dd") + "_planwipqty", 80)
                    .SetDisplayFormat("#,##0")
                    .SetIsReadOnly()
                    .SetTextAlignment(TextAlignment.Right);
                wipStock.LanguageKey = "EXPECTWIP";

                // 재공
                ConditionItemTextBox OsPrice = groupEachDay.AddTextBoxColumn(day.ToString("yyyy-MM-dd") + "_predicosprice", 80)
                    .SetDisplayFormat("#,##0")
                    .SetIsReadOnly()
                    .SetTextAlignment(TextAlignment.Right);
                OsPrice.LanguageKey = "OUTSOURCINGCOSTEXPENSE";
            }
        }

        #endregion

        #endregion

        #region ◆ Event |

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            this.Load += Form_Load;
            this.grdLoad.View.RowCellStyle += View_RowCellStyle;
            this.grdLoad.View.CellValueChanged += View_CellValueChanged;
        }

		/// <summary>
		/// 확정 버튼 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private async void BtnInit_ClickAsync(object sender, EventArgs e)
        {
            if (this.oriTable != null)
                this.grdLoad.DataSource = this.oriTable;


			MessageWorker worker = new MessageWorker("SaveLoadPredictionForSegment");
			worker.SetBody(new MessageBody()
			{
				{ "LOADDATE", loadDate }
				, { "LOADLIST", grdLoad.DataSource }
				, { "ISCONFIRM", "Y"}
				, { "USERID", UserInfo.Current.Id }
			});

			worker.Execute();
			ShowMessage("SuccedSave");

			await OnSearchAsync();
		}

		/// <summary>
		/// 셀 변경 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void View_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            this.grdLoad.View.CellValueChanged -= View_CellValueChanged;

            try
            {
                string productDefId = this.grdLoad.View.GetRowCellValue(e.RowHandle, "PRODUCTDEFID").ToString();
                string productDefVersion = this.grdLoad.View.GetRowCellValue(e.RowHandle, "PRODUCTDEFVERSION").ToString();
                string userSequence = this.grdLoad.View.GetRowCellValue(e.RowHandle, "USERSEQUENCE").ToString();
                string targetPanelQTY = this.grdLoad.View.GetRowCellValue(e.RowHandle, "TARGETPANELQTY").ToString();
                string targetSegmentQTY = this.grdLoad.View.GetRowCellValue(e.RowHandle, "TARGETSEGMENTQTY").ToString();
                string changeValueDate = e.Column.FieldName.Split('_')[0];

                DateTime changeDate = DateTime.ParseExact(changeValueDate, "yyyy-MM-dd", null);
                DateTime loadDate = DateTime.ParseExact(this.loadDate, "yyyy-MM-dd", null);

                DataTable loadDataTable = this.grdLoad.DataSource as DataTable;

                int firstIndex = loadDataTable.Rows.IndexOf(loadDataTable.Rows.OfType<DataRow>().Where(s => s["PRODUCTDEFID"].ToString().Equals(productDefId) &&
                s["PRODUCTDEFVERSION"].ToString().Equals(productDefVersion)).FirstOrDefault());

                int lastIndex = loadDataTable.Rows.IndexOf(loadDataTable.Rows.OfType<DataRow>().Where(s => s["PRODUCTDEFID"].ToString().Equals(productDefId) &&
                s["PRODUCTDEFVERSION"].ToString().Equals(productDefVersion)).LastOrDefault());

                for (int i = e.RowHandle; i < lastIndex; i++)
                {
                    int targetSegmentCount = int.Parse(string.IsNullOrWhiteSpace(loadDataTable.Rows[i]["TARGETSEGMENTQTY"].ToString()) == true ? "0" : loadDataTable.Rows[i]["TARGETSEGMENTQTY"].ToString());
                    int targetPanelCount = int.Parse(string.IsNullOrWhiteSpace(loadDataTable.Rows[i]["TARGETPANELQTY"].ToString()) == true ? "0" : loadDataTable.Rows[i]["TARGETPANELQTY"].ToString());
                    int wipCount = int.Parse(string.IsNullOrWhiteSpace(loadDataTable.Rows[i]["WIP"].ToString()) == true ? "0" : loadDataTable.Rows[i]["WIP"].ToString());

                    if (i == e.RowHandle)
                    {
                        //변경한 날짜의 Row부터 예상 재공/부하량 계산하도록 함수 호출
                        foreach (DateTime day in EachDay(changeDate, toDate))
                        {
                            LoadCalculator(i, firstIndex, targetSegmentCount, targetPanelCount, wipCount, day, changeDate, loadDataTable);
                        }
                    }
                    else
                    {
                        //변경한 Row의 다음 Row부터 전체 예상 재공/부하량 계산 하도록 함수 호출
                        foreach (DateTime day in EachDay(loadDate, toDate))
                        {
                            LoadCalculator(i, firstIndex, targetSegmentCount, targetPanelCount, wipCount, day, new DateTime(), loadDataTable);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                // 메세지박스 처리 필요 
            }
            finally
            {
                this.grdLoad.View.CellValueChanged += View_CellValueChanged;
            }
        }

		/// <summary>
		/// 부하 수량 계산
		/// </summary>
		/// <param name="index"></param>
		/// <param name="firstIndex"></param>
		/// <param name="targetSegmentCount"></param>
		/// <param name="targetPanelCount"></param>
		/// <param name="wipCount"></param>
		/// <param name="day"></param>
		/// <param name="changeDate"></param>
		/// <param name="loadDataTable"></param>
        private void LoadCalculator(int index, int firstIndex, int targetSegmentCount, int targetPanelCount, int wipCount, DateTime day, DateTime changeDate, DataTable loadDataTable)
        {
            int loadCount = 0;
            int beforeSegLoadCount = 0;
            int lastDayBeforeSegWipCount = 0;
            int expectWipCount = 0;
            DateTime lastDay = day.AddDays(-1);

            //현재 행이 해당 모델의 Revision의 첫번째 Index와 같으면 이전 공정 부하량 0으로 세팅
            if (index == firstIndex)
            {
                beforeSegLoadCount = 0;
            }
            else
            {
                //아닌경우 이전 공정의 부하량 가져옴
                beforeSegLoadCount = int.Parse(string.IsNullOrWhiteSpace(this.grdLoad.View.GetRowCellValue(index - 1, string.Format("{0}_LOADQTY", day.ToString("yyyy-MM-dd"))).ToString()) == true ? "0" : this.grdLoad.View.GetRowCellValue(index - 1, string.Format("{0}_LOADQTY", day.ToString("yyyy-MM-dd"))).ToString());
            }

            //현 공정의 부하량 가져옴
            loadCount = int.Parse(this.grdLoad.View.GetRowCellValue(index, string.Format("{0}_LOADQTY", day.ToString("yyyy-MM-dd"))).ToString());

            //해당 날짜의 컬럼이 있는지 확인
            if (loadDataTable.Columns.Contains(string.Format("{0}_PLANWIPQTY", lastDay.ToString("yyyy-MM-dd"))))
            {
                   
                lastDayBeforeSegWipCount = int.Parse(string.IsNullOrWhiteSpace(this.grdLoad.View.GetRowCellValue(index, string.Format("{0}_PLANWIPQTY", lastDay.ToString("yyyy-MM-dd"))).ToString()) == true ? "0" : this.grdLoad.View.GetRowCellValue(index, string.Format("{0}_PLANWIPQTY", lastDay.ToString("yyyy-MM-dd"))).ToString());

                if (day == changeDate)
                {
                    //변경한 행의 동일 날짜인 경우 예상 재공만 계산하여 Update
                    expectWipCount = (lastDayBeforeSegWipCount + beforeSegLoadCount) - loadCount;

                    this.grdLoad.View.SetRowCellValue(index, string.Format("{0}_PLANWIPQTY", day.ToString("yyyy-MM-dd")), expectWipCount);
                }
                else
                {
                    //일목표 공정 개수 만큼 전일 공정 재공의 합
                    //일목표 수량 보다 크면 일목표 수량이 부하량
                    int beforeSegWipCountSum = 0;

                    lastDayBeforeSegWipCount = int.Parse(this.grdLoad.View.GetRowCellValue(index, string.Format("{0}_PLANWIPQTY", lastDay.ToString("yyyy-MM-dd"))).ToString());

                    for (int j = index; j > index - targetSegmentCount; j--)
                    {
                        if (j >= 0)
                            beforeSegWipCountSum += int.Parse(this.grdLoad.View.GetRowCellValue(j, string.Format("{0}_PLANWIPQTY", lastDay.ToString("yyyy-MM-dd"))).ToString());
                        else
                            break;
                    }

                    if (beforeSegWipCountSum > targetPanelCount)
                        loadCount = targetPanelCount;
                    else
                        loadCount = beforeSegWipCountSum;

                    //전날 동일 공정 예상재공 + 현재 날짜의 이전 공정 부하량 - 현재 날짜의 진행 공정 부하량
                    expectWipCount = (lastDayBeforeSegWipCount + beforeSegLoadCount) - loadCount;

                    this.grdLoad.View.SetRowCellValue(index, string.Format("{0}_LOADQTY", day.ToString("yyyy-MM-dd")), loadCount);
                    this.grdLoad.View.SetRowCellValue(index, string.Format("{0}_PLANWIPQTY", day.ToString("yyyy-MM-dd")), expectWipCount);
                }

            }
            else
            {
                //첫날인경우 재공 현황 + 이전 공정 부하량(0) - 현재 공정 부하량 계산
                expectWipCount = (wipCount + beforeSegLoadCount) - loadCount;

                this.grdLoad.View.SetRowCellValue(index, string.Format("{0}_PLANWIPQTY", day.ToString("yyyy-MM-dd")), expectWipCount);
            }

        }

        private void View_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.RowHandle > -1)
            {

                if (e.Column.FieldName.ToUpper().Equals(loadDate + "_LOADQTY"))
                //    if (e.Column.FieldName.ToUpper().EndsWith("_LOADQTY"))
                {
                    e.Appearance.BackColor = Color.Yellow;
                }
            }
        }

        #endregion

        #region ◆ 툴바 |

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            // TODO : 저장 Rule 변경

            MessageWorker worker = new MessageWorker("SaveLoadPredictionForSegment");
            worker.SetBody(new MessageBody()
            {
                { "LOADDATE", loadDate }
                , { "LOADLIST", grdLoad.GetChangedRows() }
            });

            worker.Execute();
        }

        #endregion

        #region ◆ 검색 |

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            // 기존 Grid Data 초기화

            // p_productionDivision

            var values = Conditions.GetValues();
            values.Add("P_LOADTYPE", "SixDay");
            values["P_LOADDATE"] = values["P_LOADDATE"].ToString().Substring(0, 10);
            int gapDays = 0;
            if(values["P_LOADTYPE"].ToString() == "SixDay")
            {
                gapDays = 5;
            }
            else if (values["P_LOADTYPE"].ToString() == "ThirtyDay")
            {
                gapDays = 29;
            }
            else if (values["P_LOADTYPE"].ToString() == "NinetyDay")
            {
                gapDays = 89;
            }
            this.toDate = DateTime.Parse(values["P_LOADDATE"].ToString().Substring(0, 10)).AddDays(gapDays);
            values.Add("P_LANGUAGETYPE", UserInfo.Current.LanguageType);

            if(values["P_PRODUCTDEFID"] == null)
            {
                values["P_PRODUCTDEFID"] = string.Empty;
            }
            if(values["P_LOADOWNER"] == null)
            {
                values["P_LOADOWNER"] = string.Empty;
            }
            if(values["P_PRODUCTDEFVERSION"] == null)
            {
                values["P_PRODUCTDEFVERSION"] = string.Empty;
            }
            this.loadDate = values["P_LOADDATE"].ToString();

            InitializeGrid();
            AddDateColumnsToGrid(DateTime.Parse(values["P_LOADDATE"].ToString()), toDate);
            grdLoad.View.PopulateColumns();

            // grdLoad.View.FixColumn(new string[] { "PROCESSSEGMENTID", "PROCESSSEGMENTNAME", "AREANAME" });

            DataTable dtResult = this.Procedure("usp_wip_selectdaysegmentloadpredicosprice", values);
            if (dtResult.Rows.Count < 1)
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }
            else
            {
			}
            grdLoad.DataSource = dtResult;
          
            this.oriTable = dtResult.Copy();
        }

        /// <summary>
        /// 팝업형 조회조건 생성 - 사용자
        /// </summary>
        private void InitializeConditionUser_Popup()
        {
            // enterpriseid, plantid ,username
            var userPopup = this.Conditions.AddSelectPopup("p_loadowner", new SqlQuery("GetUserListByTool", "10001", $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "PROCESSSEGMENTCLASSTYPE=MiddleProcessSegmentClass"), "USERNAME", "USERID")
                .SetPopupLayout(Language.Get("SELECTUSER"), PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupResultCount(1)
                .SetPopupLayoutForm(800, 800, FormBorderStyle.FixedToolWindow)
                .SetPopupAutoFillColumns("USERNAME")
                .SetLabel("MANAGER")
                .SetPosition(4);

            userPopup.Conditions.AddTextBox("USERNAME");

            userPopup.GridColumns.AddTextBoxColumn("USERID", 150);
            userPopup.GridColumns.AddTextBoxColumn("USERNAME", 200);
        }
        #endregion

        #region ◆ 유효성 검사 |

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();
        }

        #endregion

        #region ◆ Private Function |
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
