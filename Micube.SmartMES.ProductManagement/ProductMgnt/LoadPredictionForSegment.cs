#region using

using DevExpress.XtraGrid.Views.Grid;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons;
using Micube.SmartMES.Commons.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
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
    public partial class LoadPredictionForSegment : SmartConditionManualBaseForm
    {
        #region ◆ Local Variables |

        private string loadDate;
        private DateTime toDate;
        #endregion

        #region ◆ 생성자 |
        /// <summary>
        /// 생성자
        /// </summary>
        public LoadPredictionForSegment()
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
            InitializeGrid();
            InitializeComboBox();
        }
        #endregion

        #region ◆ 컨텐츠 영역 초기화 |
        /// <summary>
        /// Control 설정
        /// </summary>
        protected override void InitializeContent()
        {
            base.InitializeContent();

            txtConfirmDate.Editor.ReadOnly = true;
            txtConfirmUser.Editor.ReadOnly = true;

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

            // 담당자
            InitializeConditionUser_Popup();

            Conditions.AddComboBox("P_LOADTYPE", new SqlQuery("GetCodeList", "00001", "CODECLASSID=LoadType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetPosition(5).SetLabel("LOADTYPE").SetValidationIsRequired();
        }

        /// <summary>
        /// 조회조건 설정
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();
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

            //2020-11-30 오근영 그리드 엑셀 다운로드 추가
            grdLoad.GridButtonItem = GridButtonItem.Export;

            if (UserInfo.Current.Enterprise == Constants.EnterPrise_InterFlex)
            {
                grdLoad.View.UseBandHeaderOnly = true;

                grdLoad.View.AddTextBoxColumn("PRODUCTDEFID", 150).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
                grdLoad.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
                grdLoad.View.AddTextBoxColumn("PRODUCTDEFNAME", 270).SetIsReadOnly();
                grdLoad.View.AddTextBoxColumn("USERSEQUENCE", 70).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
                grdLoad.View.AddTextBoxColumn("PROCESSSEGMENTID", 80).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
                grdLoad.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 200).SetIsReadOnly();
                grdLoad.View.AddTextBoxColumn("TARGETPANELQTY", 130).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0").SetIsReadOnly();
                grdLoad.View.AddTextBoxColumn("TARGETSEGMENTQTY", 130).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0").SetIsReadOnly();
                grdLoad.View.AddTextBoxColumn("WIP", 130).SetTextAlignment(TextAlignment.Right).SetIsReadOnly().SetLabel("BASEWIP");
                grdLoad.View.AddTextBoxColumn("PNLLOT", 130).SetTextAlignment(TextAlignment.Right).SetIsReadOnly();
            }
            else
            {
                var groupProcess = grdLoad.View.AddGroupColumn("");

                groupProcess.AddTextBoxColumn("PRODUCTDEFID", 150).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
                groupProcess.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
                groupProcess.AddTextBoxColumn("PRODUCTDEFNAME", 270).SetIsReadOnly();
                groupProcess.AddTextBoxColumn("USERSEQUENCE", 70).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
                groupProcess.AddTextBoxColumn("PROCESSSEGMENTID", 80).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
                groupProcess.AddTextBoxColumn("PROCESSSEGMENTNAME", 200).SetIsReadOnly();
                groupProcess.AddSpinEditColumn("TARGETPANELQTY", 130).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0");
                groupProcess.AddSpinEditColumn("TARGETSEGMENTQTY", 130).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0");
                groupProcess.AddTextBoxColumn("WIP", 130).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0").SetIsReadOnly().SetLabel("BASEWIP");
                groupProcess.AddTextBoxColumn("PNLLOT", 130).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0").SetIsReadOnly();
            }
            grdLoad.View.AddTextBoxColumn("CONFIRMDATE", 130).SetIsHidden();
            grdLoad.View.AddTextBoxColumn("CONFIRMUSERNAME", 80).SetIsHidden();
            grdLoad.View.AddTextBoxColumn("LOADTYPE", 80).SetIsHidden();

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
                    .SetIsReadOnly()
                    .SetTextAlignment(TextAlignment.Right);
                load.LanguageKey = "LOADQTY";

                // 재공
                ConditionItemTextBox wipStock = groupEachDay.AddTextBoxColumn(day.ToString("yyyy-MM-dd") + "_planwipqty", 80)
                    .SetDisplayFormat("#,##0")
                    .SetIsReadOnly()
                    .SetTextAlignment(TextAlignment.Right);
                wipStock.LanguageKey = "EXPECTWIP";
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
            this.btnInit.Click += BtnInit_ClickAsync;
            this.grdLoad.View.ShowingEditor += grdLoadView_ShowingEditor;
            this.grdLoad.View.RowCellStyle += View_RowCellStyle;
            this.grdLoad.View.CellValueChanged += View_CellValueChanged;
        }

        private void View_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            CalculateLoad(grdLoad.DataSource as DataTable);
        }

        private void View_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if ((e.Column.FieldName == "TARGETPANELQTY" || e.Column.FieldName == "TARGETSEGMENTQTY")
                && (string)grdLoad.View.GetRowCellValue(e.RowHandle, "ISCONFIRM") != "Y")
            {
                e.Appearance.ForeColor = Color.Black;
                e.Appearance.BackColor = Color.LightYellow;
            }
            if (e.Column.FieldName == "WIP")
            {
                e.Appearance.ForeColor = Color.Black;
                e.Appearance.BackColor = Color.PaleGreen;
            }
            if (e.Column.FieldName.ToLower().EndsWith("_planwipqty"))
            {
                e.Appearance.ForeColor = Color.Black;
                e.Appearance.BackColor = Color.FromArgb(0xcc, 0xcc, 0xff);
            }
        }

        private void grdLoadView_ShowingEditor(object sender, CancelEventArgs e)
        {
            DataRow row = grdLoad.View.GetFocusedDataRow();

            if (Format.GetString(row["ISCONFIRM"]) == "Y")
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 확정 버튼 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnInit_ClickAsync(object sender, EventArgs e)
        {
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
        #endregion

        #region ◆ 툴바 |

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            // TODO : 저장 Rule 변경

            // fix: 
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
            var values = Conditions.GetValues();
            int gapDays = GetGapDays();
            this.toDate = DateTime.Parse(values["P_LOADDATE"].ToString().Substring(0, 10)).AddDays(gapDays);
            values.Add("P_LANGUAGETYPE", UserInfo.Current.LanguageType);

            if (values["P_PRODUCTDEFID"] == null)
            {
                values["P_PRODUCTDEFID"] = string.Empty;
            }
            if (values["P_USERID"] == null)
            {
                values["P_USERID"] = string.Empty;
            }

            this.loadDate = values["P_LOADDATE"].ToString().Substring(0, 10);

            InitializeGrid();
            AddDateColumnsToGrid(DateTime.Parse(values["P_LOADDATE"].ToString()), toDate);
            grdLoad.View.PopulateColumns();

            if (UserInfo.Current.Enterprise == Constants.EnterPrise_InterFlex)
            {
                grdLoad.View.FixColumn(new string[] { "PRODUCTDEFID", "PRODUCTDEFVERSION", "PRODUCTDEFNAME", "USERSEQUENCE", "PROCESSSEGMENTID", "PROCESSSEGMENTNAME", "PLANTID" });

                ChangeLanguage();
            }

            DataTable dtResult = this.Procedure("usp_wip_selectdaysegmentload", values);
            if (dtResult.Rows.Count < 1)
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }
            else
            {
                txtConfirmDate.Text = dtResult.Rows[0]["CONFIRMDATE"].ToString();
                txtConfirmUser.Text = dtResult.Rows[0]["CONFIRMUSERNAME"].ToString();
            }
            grdLoad.DataSource = dtResult;

            CalculateLoad(dtResult); // NOTE : 부하
        }

        /// <summary>
        /// 팝업형 조회조건 생성 - 사용자
        /// </summary>
        private void InitializeConditionUser_Popup()
        {
            // enterpriseid, plantid ,username
            var userPopup = this.Conditions.AddSelectPopup("p_userid", new SqlQuery("GetUserListByTool", "10001",
                $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}",
                "PROCESSSEGMENTCLASSTYPE=MiddleProcessSegmentClass"), "USERNAME", "USERID")
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

        private int GetGapDays()
        {
            // 기존 Grid Data 초기화
            var values = Conditions.GetValues();
            values["P_LOADDATE"] = values["P_LOADDATE"].ToString().Substring(0, 10);
            if (values["P_LOADTYPE"].ToString() == "SixDay")
            {
                return 5;
            }
            else if (values["P_LOADTYPE"].ToString() == "ThirtyDay")
            {
                return 29;
            }
            else if (values["P_LOADTYPE"].ToString() == "NinetyDay")
            {
                return 89;
            }
            else
            {
                return 0;
            }
        }
        #endregion

        #region ◆ 부하량 계산
        // 부하량 계산
        private void CalculateLoad(DataTable dataSource)
        {
            int firstRow = 0;
            int lastRow = 0;

            string productDefId = null;
            string productDefVersion = null;
            bool confirmed = false;

            for (int i = 0; i < dataSource.Rows.Count; i++)
            {
                string eachId = dataSource.Rows[i]["PRODUCTDEFID"].ToString();
                string eachVersion = dataSource.Rows[i]["PRODUCTDEFVERSION"].ToString();

                if (eachId != productDefId || eachVersion != productDefVersion)
                {
                	// 부하량이 확정되지 않은 품목만 재계산한다
                    if (productDefId != null && !confirmed)
                    {
                        lastRow = i - 1;
                        CalculateProductLoad(dataSource, firstRow, lastRow);
                    }
                    productDefId = eachId;
                    productDefVersion = eachVersion;
                    firstRow = i;
                    confirmed = Format.GetString(dataSource.Rows[i]["ISCONFIRM"], "N") == "Y";
                }
            }
        	// 부하량이 확정되지 않은 품목만 재계산한다
            if(productDefId != null && !confirmed)
            {
                lastRow = dataSource.Rows.Count - 1;
                CalculateProductLoad(dataSource, firstRow, lastRow);
            }
        }

        // 품목별 부하량 계산
        private void CalculateProductLoad(DataTable dataSource, int firstRow, int lastRow)
        {
            LoadOfDay[] loadOfDay = CreateLoadOfDay(firstRow, lastRow);

            DateTime startDate = DateTime.Parse(this.loadDate);
            foreach (DateTime day in EachDay(startDate, this.toDate))
            {
            	// PNL/LOT의 수량을 가진 가상LOT을 생성
                var lots = CreateLots(dataSource, day, firstRow, lastRow);
                if (lots == null)
                {
                    return;
                }

                for (int i = firstRow; i <= lastRow; i++)
                {
                    int process = i - firstRow;
                    int panelPerDay = Format.GetInteger(dataSource.Rows[i]["TARGETPANELQTY"], 0);
                    int processPerDay = Format.GetInteger(dataSource.Rows[i]["TARGETSEGMENTQTY"], 0);
                    int panelPerLot = Format.GetInteger(dataSource.Rows[i]["PNLLOT"], 0);

                    int totalLoad = panelPerDay + loadOfDay[process].PartialUsedLoad;

                    // 이동 가능한 LOT들을 다음공정으로 보낸다
                    List<Lot> lotsToMove = PickLotsToMove(lots[process], totalLoad, panelPerLot);
                    MoveToNextProcess(lots, process, lotsToMove);

                    // LOT 수량을 채우지 못하고 남은 공정부하를 다음날 공정부하로 이전시킨다 
                    int movingPanels = lotsToMove.Sum(lot => lot.PanelQty);
                    if (lots[process].Count > 0)
                    {
                        loadOfDay[process].PartialUsedLoad = totalLoad - movingPanels;
                    }
                    else
                    {
                        loadOfDay[process].PartialUsedLoad = 0;
                    }

                    // 일별 공정부하 및 기말재공 기록
                    dataSource.Rows[i][day.ToString("yyyy-MM-dd") + "_loadqty"] = movingPanels;
                    dataSource.Rows[i][day.ToString("yyyy-MM-dd") + "_planwipqty"] = lots[process].Sum(lot => lot.PanelQty);
                }
            }
        }

        // 해당일의 기초재공으로 가상 LOT 생성
        private List<Lot>[] CreateLots(DataTable dataSource, DateTime day, int firstRow, int lastRow)
        {
            DateTime startDate = DateTime.Parse(this.loadDate);

            List<Lot>[] result = new List<Lot>[lastRow - firstRow + 1];
            for (int i = firstRow; i <= lastRow; i++)
            {
                int panelPerDay = Format.GetInteger(dataSource.Rows[i]["TARGETPANELQTY"], 0);		// 일별 목표 공정부하(Panel)
                int processPerDay = Format.GetInteger(dataSource.Rows[i]["TARGETSEGMENTQTY"], 0);	// 일별 목표 공정진행
                int panelPerLot = Format.GetInteger(dataSource.Rows[i]["PNLLOT"], 0);				// Panel / Lot
                int baseWip = 0;	// 기초재공
                if (panelPerLot == 0)
                {
                    return null;
                }
                if (day == startDate)
                {
                    baseWip = Format.GetInteger(dataSource.Rows[i]["WIP"], 0);	// 기초재공
                }
                else
                {
                    baseWip = Format.GetInteger(dataSource.Rows[i][day.AddDays(-1).ToString("yyyy-MM-dd") + "_planwipqty"], 0); // 전일기말
                }
                int process = i - firstRow;

                List<Lot> lots = new List<Lot>();
                for (int j = 0; j < baseWip / panelPerLot; j++)
                {
                    lots.Add(new Lot(panelPerLot, processPerDay));
                }
                int remainPanels = baseWip % panelPerLot;
                if (remainPanels > 0)
                {
                    lots.Add(new Lot(remainPanels, processPerDay));
                }
                result[process] = lots;
            }
            return result;
        }

        // 공정별 잔여 공정부하
        private LoadOfDay[] CreateLoadOfDay(int firstRow, int lastRow)
        {
            LoadOfDay[] result = new LoadOfDay[lastRow - firstRow + 1];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = new LoadOfDay();
            }
            return result;
        }

        // 공정진행 대상 Lot 선택
        // 일목표 공정부하 수량 내에서, 이동가능 공정수가 많은 Lot부터 이동
        private List<Lot> PickLotsToMove(List<Lot> lotsInProcess, int load, int panelPerLot)
        {
            var lotsCanMove = lotsInProcess.Where(lot => lot.RemainMove > 0);
            var result = lotsCanMove.ToList<Lot>();
            result.Sort(delegate (Lot l1, Lot l2) { return l2.RemainMove - l1.RemainMove; });
            return result.Take(load / panelPerLot).ToList<Lot>();
        }

        // 가상 Lot 다음공정으로 진행
        private void MoveToNextProcess(List<Lot>[] lots, int process, List<Lot> lotsToMove)
        {
            foreach (Lot lot in lotsToMove)
            {
                lot.RemainMove--;
            }
            lots[process] = lots[process].Except(lotsToMove).ToList();
            if (process < lots.Length - 1)
            {
                lots[process + 1].AddRange(lotsToMove);
            }
        }

        // 가상 Lot
        private class Lot
        {
            public int PanelQty { get; private set; } // 수량(PNL)
            public int RemainMove { get; set; } // 이동가능 공정 수

            public Lot(int panelQty, int remainMove)
            {
                this.PanelQty = panelQty;
                this.RemainMove = remainMove;
            }
        }

        // 일별 잔여 공정부하
        private class LoadOfDay
        {
            public int PartialUsedLoad { get; set; }	// 잔여 공정부하

            public LoadOfDay()
            {
                this.PartialUsedLoad = 0;
            }
        }
        #endregion
    }
}
