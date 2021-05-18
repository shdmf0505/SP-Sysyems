#region using

using DevExpress.Utils;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;

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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

#endregion

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : ex> 시스템관리 > 코드 관리 > 코드그룹 정보
    /// 업  무  설  명  : ex> 시스템에서 공통으로 사용되는 코드그룹 정보를 관리한다.
    /// 생    성    자  : 홍길동
    /// 생    성    일  : 2019-05-14
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class LOTAnalysisGrid : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가
        string _Text;                                       // 설명
        SmartBandedGrid grdList = new SmartBandedGrid();    // Main Grid

        /// <summary>
        /// 조회 조건 품목에서 찾은 정보 Row
        /// </summary>
        private DataRow _selectedRow;

        /// <summary>
        /// 넘겨 받은 LOT 리스트 배열
        /// </summary>
        private string[] arrLot;

        /// <summary>
        /// 팝업에서 선택된 데이터
        /// </summary>
        DataTable _checked;


        #endregion

        #region 생성자

        public LOTAnalysisGrid()
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

            #region 랏 콤보박스 설정

            cbLOT.UseEmptyItem = true;
            cbLOT.EmptyItemCaption = "ALL";
            cbLOT.EmptyItemValue = "*";

            #endregion

            InitializeEvent();

            // TODO : 컨트롤 초기화 로직 구성
            InitializeGrid();

            tb100Match.BackColor = Color.Red;
            tb100Match.TextAlign = HorizontalAlignment.Center;
            tb100Match.ForeColor = Color.White;
            tb100Match.ReadOnly = true;

            tb70Match.BackColor = Color.OrangeRed;
            tb70Match.TextAlign = HorizontalAlignment.Center;
            tb70Match.ForeColor = Color.White;
            tb70Match.ReadOnly = true;

            tb50Match.BackColor = Color.Orange;
            tb50Match.TextAlign = HorizontalAlignment.Center;
            tb50Match.ForeColor = Color.White;
            tb50Match.ReadOnly = true;

            tb20Match.BackColor = Color.LightSalmon;
            tb20Match.TextAlign = HorizontalAlignment.Center;
            tb20Match.ForeColor = Color.White;
            tb20Match.ReadOnly = true;

            tb1Match.BackColor = Color.Gray;
            tb1Match.TextAlign = HorizontalAlignment.Center;
            tb1Match.ForeColor = Color.White;
            tb1Match.ReadOnly = true;
        }

        /// <summary>        
        /// 코드그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            #region 피벗 그리드 설정
            
            // 공정순서
            pvGridWorkTime.AddRowField("USERSEQUENCE", 120);
            // 작업구분
            pvGridWorkTime.AddRowField("WORKTYPE", "REWORKTYPE", 120);
            // 공정명
            pvGridWorkTime.AddRowField("PROCESSSEGMENTNAME", 120);
            // 작업장
            pvGridWorkTime.AddRowField("AREANAME", 120);
            // 작업설비
            pvGridWorkTime.AddRowField("EQUIPMENTNAME", 120);

            //pvGridWorkTime.AddDataField("LOTCOUNT", 120).SetCellFormat(FormatType.Numeric, "###,###.##");
            pvGridWorkTime.AddColumnField("DAY", "WORKDATE");
            pvGridWorkTime.AddColumnField("HOUR", "TXNTIME", 30);
            //.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Custom;

            pvGridWorkTime.AddDataField("LOTCOUNT", "PROCESSEDLOTCOUNT", 20);//.SetCellFormat(FormatType.Numeric, "###,##0");
            
            //pvGridWorkTime.AddDataField("INPUTPNLQTY", 200).SetCellFormat(FormatType.Numeric, "###,###.##");
            //pvGridWorkTime.AddDataField("INPUTMMQTY", 200).SetCellFormat(FormatType.Numeric, "###,###.##");
            //pvGridWorkTime.AddDataField("INPUTPRICE", 200).SetCellFormat(FormatType.Numeric, "###,###.##");
            //pvGridWorkTime.AddDataField("FBCBPRICE").SetCellFormat(FormatType.Numeric, "###,###.##");
            //pvGridWorkTime.AddDataField("ASSYPRICE").SetCellFormat(FormatType.Numeric, "###,###.##");
            //pvGridWorkTime.AddDataField("ETCPRICE").SetCellFormat(FormatType.Numeric, "###,###.##");

            pvGridWorkTime.AddFilterField("LOTID", 120);
            //pvGridWorkTime.AddFilterField("USERSEQUENCE", 100);
            //pvGridWorkTime.AddFilterField("WORKTYPE", "REWORKTYPE", 120);
            pvGridWorkTime.AddFilterField("PROCESSSEGMENTNAME", 120);
            //pvGridWorkTime.AddFilterField("AREANAME", 120);
            pvGridWorkTime.AddFilterField("EQUIPMENTID", 120);
            pvGridWorkTime.AddFilterField("WORKENDTIME", 100);//.SetCellFormat(FormatType.DateTime);


            //pvGridWorkTime.AddFilterField("RESULTTYPE").SetCellFormat(FormatType.DateTime);
            //pvGridWorkTime.AddFilterField("SPECAPPROVALDATE");
            //pvGridWorkTime.AddFilterField("LOTCREATEDTIME").SetCellFormat(FormatType.DateTime);
            //pvGridWorkTime.AddFilterField("INPUTLT").SetCellFormat(FormatType.Numeric, "###,###.##");
            //pvGridWorkTime.AddFilterField("LOTID", 120);
            //pvGridWorkTime.AddFilterField("REPLOTID", 120).SetCellFormat(FormatType.Numeric, "###,###.##");
            //pvGridWorkTime.AddFilterField("REPINPUTPCSQTY", 120).SetCellFormat(FormatType.Numeric, "###,###.##");
            //pvGridWorkTime.AddFilterField("REPINPUTPNLQTY", 120).SetCellFormat(FormatType.Numeric, "###,###.##");
            //pvGridWorkTime.AddFilterField("REPINPUTMMQTY", 120).SetCellFormat(FormatType.Numeric, "###,###.##");
            //pvGridWorkTime.AddFilterField("REPINPUTPRICE", 120).SetCellFormat(FormatType.Numeric, "###,###.##");

            //pvGridWorkTime.DefaultFilterEditorView = DevExpress.XtraEditors.FilterEditorViewMode.Text;
            

            pvGridWorkTime.PopulateFields();

            pvGridWorkTime.OptionsView.ShowColumnGrandTotals = false;
            pvGridWorkTime.OptionsView.ShowRowGrandTotals = false;
            //pvGridWorkTime.OptionsView.ShowFilterHeaders = false;
            //pvGridWorkTime.OptionsView.ShowDataHeaders = false;

            #endregion 피벗 그리드 설정

            #region 자재 그리드

            grdProcessConsumable.View.SetIsReadOnly();
            grdProcessConsumable.GridButtonItem = GridButtonItem.None;

            grdProcessConsumable.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150);
            grdProcessConsumable.View.AddTextBoxColumn("CONSUMABLELOTID", 150);
            grdProcessConsumable.View.AddTextBoxColumn("LOTID", 150);
            grdProcessConsumable.View.AddTextBoxColumn("CONSUMABLEDEFNAME", 150);

            //grdProcessConsumable.View.AddTextBoxColumn("SUBCONSUMABLELOTID", 150);
            grdProcessConsumable.View.AddTextBoxColumn("CONSUMABLE", 100);
            grdProcessConsumable.View.AddTextBoxColumn("WORKENDDATE", 150);

            grdProcessConsumable.View.PopulateColumns();

            grdProcessConsumable.View.OptionsView.AllowCellMerge = true;

            //grdProcessConsumable.View.AddTextBoxColumn("USERSEQUENCE", 100);
            //grdProcessConsumable.View.AddTextBoxColumn("WORKTYPE", 100);
            //grdProcessConsumable.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150);
            //grdProcessConsumable.View.AddTextBoxColumn("CONSUMABLEDEFID", 120);
            //grdProcessConsumable.View.AddTextBoxColumn("MATERIALLOTID", 200);
            //grdProcessConsumable.View.AddTextBoxColumn("LOTQTY", 100).SetLabel("PROCESSEDLOTCOUNT");

            //grdProcessConsumable.View.PopulateColumns();

            #endregion

            #region 치공구 그리드

            grdProcessDurable.View.SetIsReadOnly();
            grdProcessDurable.GridButtonItem = GridButtonItem.None;

            grdProcessDurable.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150);
            grdProcessDurable.View.AddTextBoxColumn("DURABLELOTID", 150);
            grdProcessDurable.View.AddTextBoxColumn("LOTID", 150);

            grdProcessDurable.View.AddTextBoxColumn("DURABLECLASS", 100).SetLabel("TOOLCHANGETYPE");
            //TOOLCODE
            grdProcessDurable.View.AddTextBoxColumn("TOOLCODE", 150);
            //TOOLNOSEQ
            grdProcessDurable.View.AddTextBoxColumn("TOOLNOSEQ", 150);
            //TOOLVERSION
            grdProcessDurable.View.AddTextBoxColumn("TOOLVERSION", 150);
            grdProcessDurable.View.AddTextBoxColumn("WORKENDDATE", 150);

            grdProcessDurable.View.PopulateColumns();

            grdProcessDurable.View.OptionsView.AllowCellMerge = true;

            //grdProcessDurable.View.AddTextBoxColumn("USERSEQUENCE", 100);
            //grdProcessDurable.View.AddTextBoxColumn("WORKTYPE", 100);
            //grdProcessDurable.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150);
            //grdProcessDurable.View.AddTextBoxColumn("DURABLECLASSNAME", 120);
            //grdProcessDurable.View.AddTextBoxColumn("DURABLEDEFNAME", 280);
            //grdProcessDurable.View.AddTextBoxColumn("LOTQTY", 100).SetLabel("PROCESSEDLOTCOUNT");

            //grdProcessDurable.View.PopulateColumns();

            #endregion
        }

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            //grdList.View.AddingNewRow += View_AddingNewRow;
            

            #region 피봇 그리드 확대, 축소

            gbPivotGrid.ExpandEvent += (s, e) =>
            {
                this.scMainGrid.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1;
            };

            gbPivotGrid.RestoreEvent += (s, e) =>
            {
                this.scMainGrid.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Both;
            };

            //pvGridWorkTime.CustomCellValue += PvGridWorkTime_CustomCellValue;
            pvGridWorkTime.CustomAppearance += PvGridWorkTime_CustomAppearance;
            #endregion

            #region 자재 그리드 확대, 축소

            gbConsumableGrid.ExpandEvent += (s, e) =>
            {
                this.scGridResConsumable.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1;
                this.scMainGrid.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2;

            };

            gbConsumableGrid.RestoreEvent += (s, e) =>
            {
                this.scGridResConsumable.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Both;
                this.scMainGrid.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Both;
            };

            #endregion

            #region 치공구 그리드 확대, 축소

            gbDurableGrid.ExpandEvent += (s, e) =>
            {
                this.scGridResConsumable.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2;
                this.scMainGrid.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2;
            };

            gbDurableGrid.RestoreEvent += (s, e) =>
            {
                this.scGridResConsumable.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Both;
                this.scMainGrid.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Both;
            };

            #endregion

            btnDefectAnalysis.Click += BtnDefectAnalysis_Click;

            #region 자재/치공구 정보 Cell Merge 이벤트

            grdProcessConsumable.View.CellMerge += grdProcessConsumable_CellMerge;
            //grdProcessConsumable.View.cha.RowCellStyle += grdProcessConsumableView_RowCellStyle;

            grdProcessDurable.View.CellMerge += grdProcessDurable_CellMerge;
            #endregion
        }

        private void grdProcessConsumableView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = (GridView)sender;
            GridViewInfo viewInfo = (GridViewInfo)view.GetViewInfo();
            GridCellInfo cell = viewInfo.GetGridCellInfo(e.RowHandle, e.Column);
            if (cell != null && cell.IsMerged)
            {
                foreach (GridCellInfo ci in cell.MergedCell.MergedCells)
                {
                    if (ci.RowHandle == view.FocusedRowHandle)
                    {
                        e.Appearance.Assign(viewInfo.PaintAppearance.FocusedRow);
                        break;
                    }
                }
            }
        }

        private void grdProcessConsumable_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            GridView view = (GridView)sender;
            GridViewInfo viewInfo = (GridViewInfo)view.GetViewInfo();
            GridCellInfo cell = viewInfo.GetGridCellInfo(e.RowHandle2, e.Column);

            if (e.Column.FieldName.Equals("PROCESSSEGMENTNAME"))
            {
                var dr1 = grdProcessConsumable.View.GetDataRow(e.RowHandle1); //위에 행 정보
                var dr2 = grdProcessConsumable.View.GetDataRow(e.RowHandle2); //아래 행 정보

                //비교하는 이유 그래야 정상적으로 나옴.
                e.Merge = dr1["PROCESSSEGMENTNAME"].ToString().Equals(dr2["PROCESSSEGMENTNAME"].ToString());
            }
            //DURABLELOTID
            else if (e.Column.FieldName.Equals("CONSUMABLELOTID"))
            {
                var dr1 = grdProcessConsumable.View.GetDataRow(e.RowHandle1); //위에 행 정보
                var dr2 = grdProcessConsumable.View.GetDataRow(e.RowHandle2); //아래 행 정보

                //비교하는 이유 그래야 정상적으로 나옴.
                e.Merge = dr1["CONSUMABLELOTID"].ToString().Equals(dr2["CONSUMABLELOTID"].ToString());
            }
            //DURABLECLASS
            else if (e.Column.FieldName.Equals("CONSUMABLEDEFNAME"))
            {
                var dr1 = grdProcessConsumable.View.GetDataRow(e.RowHandle1); //위에 행 정보
                var dr2 = grdProcessConsumable.View.GetDataRow(e.RowHandle2); //아래 행 정보

                //비교하는 이유 그래야 정상적으로 나옴.
                e.Merge = dr1["CONSUMABLEDEFNAME"].ToString().Equals(dr2["CONSUMABLEDEFNAME"].ToString());
            }
            else if (e.Column.FieldName.Equals("CONSUMABLE"))
            {
                var dr1 = grdProcessConsumable.View.GetDataRow(e.RowHandle1); //위에 행 정보
                var dr2 = grdProcessConsumable.View.GetDataRow(e.RowHandle2); //아래 행 정보

                //비교하는 이유 그래야 정상적으로 나옴.
                e.Merge = dr1["CONSUMABLE"].ToString().Equals(dr2["CONSUMABLE"].ToString());
            }
            else
                e.Merge = false;

            e.Handled = true;
        }

        private void grdProcessDurable_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            if (e.Column.FieldName.Equals("PROCESSSEGMENTNAME"))
            {
                var dr1 = grdProcessDurable.View.GetDataRow(e.RowHandle1); //위에 행 정보
                var dr2 = grdProcessDurable.View.GetDataRow(e.RowHandle2); //아래 행 정보

                //비교하는 이유 그래야 정상적으로 나옴.
                e.Merge = dr1["PROCESSSEGMENTNAME"].ToString().Equals(dr2["PROCESSSEGMENTNAME"].ToString());
            }
            //DURABLELOTID
            else if (e.Column.FieldName.Equals("DURABLELOTID"))
            {
                var dr1 = grdProcessDurable.View.GetDataRow(e.RowHandle1); //위에 행 정보
                var dr2 = grdProcessDurable.View.GetDataRow(e.RowHandle2); //아래 행 정보

                //비교하는 이유 그래야 정상적으로 나옴.
                e.Merge = dr1["DURABLELOTID"].ToString().Equals(dr2["DURABLELOTID"].ToString());
            }
            //DURABLECLASS
            else if (e.Column.FieldName.Equals("DURABLECLASS"))
            {
                var dr1 = grdProcessDurable.View.GetDataRow(e.RowHandle1); //위에 행 정보
                var dr2 = grdProcessDurable.View.GetDataRow(e.RowHandle2); //아래 행 정보

                //비교하는 이유 그래야 정상적으로 나옴.
                e.Merge = dr1["DURABLECLASS"].ToString().Equals(dr2["DURABLECLASS"].ToString());
            }
            else if (e.Column.FieldName.Equals("DURABLEDEFVERSION"))
            {
                var dr1 = grdProcessDurable.View.GetDataRow(e.RowHandle1); //위에 행 정보
                var dr2 = grdProcessDurable.View.GetDataRow(e.RowHandle2); //아래 행 정보

                //비교하는 이유 그래야 정상적으로 나옴.
                e.Merge = dr1["DURABLEDEFVERSION"].ToString().Equals(dr2["DURABLEDEFVERSION"].ToString());
            }
            else
                e.Merge = false;

            e.Handled = true;
        }

        /// <summary>
        /// 불량 분석 버튼 클릭 이벤트 - 불량분석(공정시계열) 화면 이동
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDefectAnalysis_Click(object sender, EventArgs e)
        {
            var parameters = Conditions.GetValues();

            OpenMenu("PG-QC-0555", parameters);
        }

        /// <summary>
        /// 피봇 그리드 Custom Appearance 이벤트 - Cell 백그라운드 색 설정
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PvGridWorkTime_CustomAppearance(object sender, DevExpress.XtraPivotGrid.PivotCustomAppearanceEventArgs e)
        {
            if (e.ColumnValueType == DevExpress.XtraPivotGrid.PivotGridValueType.Value)
            {
                int iCellVal = Convert.ToInt32(e.Value);
                int iLotCount = Convert.ToInt32(tbLotCount.EditValue);

                double dPcnt = ((double)iCellVal / (double)iLotCount) * 100.0;

                if (dPcnt == 100)
                {
                    e.Appearance.BackColor = Color.Red;
                    //e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                }
                else if(dPcnt < 100 && dPcnt >= 70)
                {
                    e.Appearance.BackColor = Color.OrangeRed;
                }
                else if(dPcnt < 70 && dPcnt >= 50)
                {
                    e.Appearance.BackColor = Color.Orange;
                }
                else if (dPcnt < 50 && dPcnt >= 20)
                {
                    e.Appearance.BackColor = Color.LightSalmon;
                }
                else if(dPcnt < 20 && dPcnt > 0)
                {
                    e.Appearance.BackColor = Color.LightGray;
                }

            }
        }

        /// <summary>
        /// 피봇 그리드 Custom Cell Value - 미사용
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PvGridWorkTime_CustomCellValue(object sender, DevExpress.XtraPivotGrid.PivotCellValueEventArgs e)
        {
            try
            {
                if (e.RowValueType == DevExpress.XtraPivotGrid.PivotGridValueType.Value)
                {
                    int iCellVal = Convert.ToInt32(e.Value);
                    int iLotCount = Convert.ToInt32(tbLotCount.EditValue);

                    //if (iCellVal == 0)
                    //    e.Value = "";
                }
            }
            catch(Exception ex)
            {
                ;
            }
        }

        /// <summary>
        /// 코드그룹 리스트 그리드에서 추가 버튼 클릭 시 호출
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            // TODO : 저장 Rule 변경
            DataTable changed = grdList.GetChangedRows();

            ExecuteRule("SaveCodeClass", changed);
        }

        #endregion

        #region 검색

        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            DialogManager.ShowWaitArea(this.pnlContent);

            await base.OnSearchAsync();

            // TODO : 조회 SP 변경
            var values = Conditions.GetValues();

            string txtPeriod = values["P_PERIOD_PERIODFR"].ToString().Substring(0, 10) + " ~ " + values["P_PERIOD_PERIODTO"].ToString().Substring(0, 10);
            string txtItemName = values["P_PRODUCTNAME"].ToString();

            itemSmartLabelTextBox.EditValue = txtItemName;
            periodSmartLabelTextBox.EditValue = txtPeriod;

            DataTable dt;
            if (arrLot == null || arrLot.Count() < 1) return;

            string strLot = string.Join(",", arrLot.ToList<string>());
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("P_LOTS", strLot);

            string qryYPE = string.Empty;
            if (UserInfo.Current.Enterprise.Equals("YOUNGPOONG"))
                qryYPE = "YPE";

            if (await SqlExecuter.QueryAsync("SelectYieldLOTAnalysisData" + qryYPE, "10001", values) is DataTable dt1)
            {
                //pvGridWorkTime.DataSource = dt1;

                DateTime minDate = Convert.ToDateTime(dt1.AsEnumerable().Min(r => r["STARTDATE"]));
                DateTime maxDate = Convert.ToDateTime(dt1.AsEnumerable().Max(r => r["STARTDATE"]));

                values.Add("P_STARTTIME", minDate);
                values.Add("P_ENDTIME", maxDate);

                if (await SqlExecuter.QueryAsync("SelectLotAnalysisPeriod" + qryYPE, "10001", values) is DataTable dtPeriod)
                {
                    var joinQry = from p in dtPeriod.AsEnumerable()
                                  join v in dt1.AsEnumerable()
                                  on new { DayGubun = p.Field<string>("DAY"), HourGubun = p.Field<string>("HOUR") } equals new { DayGubun = v.Field<string>("DAY"), HourGubun = v.Field<string>("HOUR") } into LEFTJOIN
                                  from result in LEFTJOIN.DefaultIfEmpty()
                                  select new
                                  {
                                      DAY = p.Field<string>("DAY"),
                                      HOUR = p.Field<string>("HOUR"),
                                      USERSEQUENCE = (result== null ? DBNull.Value : result["USERSEQUENCE"]),
                                      WORKCOUNT = (result == null ? DBNull.Value : result["WORKCOUNT"]),
                                      AREANAME = (result == null ? DBNull.Value : result["AREANAME"]),
                                      PROCESSSEGMENTNAME = (result == null ? DBNull.Value : result["PROCESSSEGMENTNAME"]),
                                      EQUIPMENTID = (result == null ? DBNull.Value : result["EQUIPMENTID"]),
                                      EQUIPMENTNAME = (result == null ? DBNull.Value : result["EQUIPMENTNAME"]),
                                      WORKTYPE = (result == null ? DBNull.Value : result["WORKTYPE"]),
                                      LOTID = (result == null ? DBNull.Value : result["LOTID"]),
                                      WORKENDTIME = (result == null ? DBNull.Value : result["WORKENDTIME"]),
                                      LOTCOUNT = (result == null ? DBNull.Value : result["LOTCOUNT"]),
                                  };

                    DataTable nTable = dt1.Clone();

                    foreach(var v in joinQry)
                    {
                        DataRow row = nTable.NewRow();

                        if (v.USERSEQUENCE == DBNull.Value) continue;

                        row["DAY"] = v.DAY;
                        row["HOUR"] = v.HOUR;
                        row["USERSEQUENCE"] = v.USERSEQUENCE;
                        row["WORKCOUNT"] = v.WORKCOUNT;
                        row["AREANAME"] = v.AREANAME;
                        row["PROCESSSEGMENTNAME"] = v.PROCESSSEGMENTNAME;
                        row["EQUIPMENTID"] = v.EQUIPMENTID;
                        row["EQUIPMENTNAME"] = v.EQUIPMENTNAME;
                        row["WORKTYPE"] = v.WORKTYPE;
                        row["LOTID"] = v.LOTID;
                        row["WORKENDTIME"] = v.WORKENDTIME;
                        row["LOTCOUNT"] = v.LOTCOUNT;

                        nTable.Rows.Add(row);
                    }

                    pvGridWorkTime.DataSource = nTable;
                }
            }

            if (await SqlExecuter.QueryAsync("SelectYieldLOTAnalysisDataByConsumable" + qryYPE, "10001", values) is DataTable dt2)
                grdProcessConsumable.DataSource = dt2;

            if (await SqlExecuter.QueryAsync("SelectYieldLOTAnalysisDataByDurable" + qryYPE, "10001", values) is DataTable dt3)
                grdProcessDurable.DataSource = dt3;

            //pvGridWorkTime.BestFit();
            grdProcessConsumable.View.BestFitColumns();
            grdProcessDurable.View.BestFitColumns();
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            #region 품목 

            var productDefID = Conditions.AddSelectPopup("P_PRODUCTDEFID", new SqlQuery("GetProductListByYieldStatus", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PRODUCTDEFIDVERSION", "PRODUCTDEFIDVERSION")
                                         .SetLabel("PRODUCTDEFID")
                                         .SetPopupLayout("GRIDPRODUCTLIST", PopupButtonStyles.Ok_Cancel, true, false)
                                         .SetPopupLayoutForm(650, 400, FormBorderStyle.FixedToolWindow)
                                         .SetPosition(1.1)                                        
                                         .SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
                                         {
                                             foreach (DataRow row in selectedRow)
                                             {
                                                 Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").SetValue(row["PRODUCTDEFID"]);

                                                 Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").Text = row["PRODUCTDEFNAME"].ToString();

                                                 _selectedRow = row;
                                             }
                                         });

            SmartTextBox tbProductID = Conditions.GetControl<SmartTextBox>("PRODUCTDEFID");

            // 필수 설정
            productDefID.IsRequired = true;

            productDefID.Conditions.AddTextBox("PRODUCTDEFID")
                                   .SetLabel("PRODUCTDEFID");

            //productDefID.Conditions.GetControl<SmartTextBox>("PRODUCTDEFID").EditValue = tbProductID.EditValue;

            productDefID.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 120)
                                    .SetLabel("PRODUCTDEFID");

            productDefID.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 250)
                                    .SetLabel("PRODUCTDEFNAME");

            productDefID.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 100)
                                    .SetLabel("PRODUCTDEFVERSION");


            #endregion

            #region 품목명

            Conditions.GetCondition<ConditionItemTextBox>("P_PRODUCTNAME").SetIsReadOnly();

            #endregion
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
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
            grdList.View.CheckValidation();

            DataTable changed = grdList.GetChangedRows();

            if (changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }

        #endregion

        #region Private Function

        // TODO : 화면에서 사용할 내부 함수 추가
        /// <summary>
        /// 마스터 정보를 조회한다.
        /// </summary>
        private void LoadData()
        {

        }

        #endregion

        public override void LoadForm(Dictionary<string, object> parameters)
        {
            base.LoadForm(parameters);

            if (parameters != null)
            {
                Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").SetValue(parameters["P_PRODUCTDEFID"].ToString());
                Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").EditValue = parameters["P_PRODUCTNAME"].ToString();
                Conditions.GetControl<SmartComboBox>("P_PLANTID").EditValue = parameters["P_PLANTID"].ToString();
                SmartPeriodEdit period = Conditions.GetControl<SmartPeriodEdit>("P_PERIOD");
                period.datePeriodFr.EditValue = parameters["P_PERIOD_PERIODFR"];
                period.datePeriodTo.EditValue = parameters["P_PERIOD_PERIODTO"];

                if (parameters.ContainsKey("P_LOTS"))
                {
                    arrLot = parameters["P_LOTS"].ToString().Split(',');
                    setLOTComboData(arrLot);
                }

                OnSearchAsync();
            }
        }

        private void btnLotSelect_Click(object sender, EventArgs e)
        {
            LotSelectPopup popup = new LotSelectPopup();
            popup.StartPosition = FormStartPosition.CenterParent;

            var values = Conditions.GetValues();

            popup.FromTime = Convert.ToDateTime(values["P_PERIOD_PERIODFR"]);
            popup.ToTime = Convert.ToDateTime(values["P_PERIOD_PERIODTO"]);

            popup.ProductDefID = values["P_PRODUCTDEFID"].ToString();

            string txtPeriod = values["P_PERIOD_PERIODFR"].ToString().Substring(0, 10) + " ~ " + values["P_PERIOD_PERIODTO"].ToString().Substring(0, 10);
            popup.StrPeriod = txtPeriod;

            popup.LotIDs = "";
            popup.SearchConditions = values;

            popup.ShowDialog();
            if (popup.DialogResult == DialogResult.OK)
            {
                _checked = popup._checkTable;

                if (_checked.Rows.Count > 0)
                {
                    string strLot = string.Join(",", _checked.Rows.OfType<DataRow>().Select(r => r["LOTID"].ToString()));
                    arrLot = strLot.Split(',');
                    setLOTComboData(arrLot);
                    //values.Add("P_LOTS", strLot);

                    //this.OpenMenu("PG-QC-0554", values);

                }
            }
        }

        private void setLOTComboData(string[] arrLOT)
        {
            if (cbLOT.DataSource != null)
                cbLOT.ClearContent();

            tbLotCount.EditValue = arrLOT.Count().ToString();

            DataTable dt = new DataTable();
            dt.Columns.Add("CODEID");
            dt.Columns.Add("CODENAME");

            foreach (string l in arrLot)
            {
                DataRow dr = dt.NewRow();
                dr["CODEID"] = l;
                dr["CODENAME"] = l;
                dt.Rows.Add(dr);
            }

            cbLOT.DataSource = dt;

            //cbLOT.ValueMember = "CODENAME";
            cbLOT.SetVisibleColumns("CODENAME");
            

            cbLOT.EditValue = "*";
        }
    }
}
