using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 신뢰성검증 > 신뢰성 검증 의뢰(정기)
    /// 업  무  설  명  : 신뢰성 재의뢰(정기)를 등록하는 팝업
    /// 생    성    자  : 유석진
    /// 생    성    일  : 2019-07-12
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ReReliabilityVerificationRequestRegularPopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region Local Variables

        /// <summary>
        /// 팝업진입전 그리드의 Row
        /// </summary>
        public DataRow CurrentDataRow { get; set; }
        public bool isEnable = true;

        #endregion

        #region 생성자

        public ReReliabilityVerificationRequestRegularPopup(DataRow dr)
        {
            InitializeComponent();

            CurrentDataRow = dr;

            manufacturingHistoryControl1.CurrentDataRow = CurrentDataRow;
            manufacturingHistoryControl1.tPaent = this;

            InitializeGrid();
            InitializeEvent();
            InitializeLanguageKey();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// Language Key 초기화
        /// </summary>
        private void InitializeLanguageKey()
        {
            //grdInspection1.LanguageKey = "MEASUREITEMLIST";
            //grdMeasuredValue1.LanguageKey = "MEASUREDVALUE";
            grdInspection2.LanguageKey = "MEASUREITEMLIST";
            grdMeasuredValue2.LanguageKey = "MEASUREDVALUE";
        }

        private void InitializeGrid()
        {
            InitializeGrdProductInformation(grdProductInformation1);
            InitializeGrdProductInformation(grdProductInformation2);
            InitializeGrdMeasureHistory();
            //InitializeGrdInspection(grdInspection1); // 재의뢰 검사항목 초기화
            //InitializeGrdMeasuredValue(grdMeasuredValue1); // 재의뢰 측정값 초기화
            InitializeGrdInspection(grdInspection2); // 이전결과 검사항목 초기화
            InitializeGrdMeasuredValue(grdMeasuredValue2); // 이전결과 측정값 초기화
        }

        /// <summary>
        /// 측정 항목 그리드 초기화
        /// </summary>
        private void InitializeGrdInspection(SmartBandedGrid grdInspection)
        {
            grdInspection.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;

            grdInspection.View.AddTextBoxColumn("LOTID", 100)
                         .SetIsHidden();

            grdInspection.View.AddTextBoxColumn("PRODUCTDEFID", 100)
                         .SetIsHidden();

            grdInspection.View.AddTextBoxColumn("PROCESSSEGMENTID", 100)
                         .SetIsHidden();

            grdInspection.View.AddTextBoxColumn("ENTERPRISEID", 100)
                         .SetIsHidden();

            grdInspection.View.AddTextBoxColumn("PLANTID", 100)
                         .SetIsHidden();

            grdInspection.View.AddTextBoxColumn("AREAID", 100)
                         .SetIsHidden();

            grdInspection.View.AddTextBoxColumn("EQUIPMENTID", 100)
                         .SetIsHidden();

            grdInspection.View.AddTextBoxColumn("LOTTYPE", 100)
                         .SetIsHidden();

            grdInspection.View.AddTextBoxColumn("CUSTOMERID", 100)
                         .SetIsHidden();

            grdInspection.View.AddTextBoxColumn("MEASURER", 100)
                         .SetIsHidden();

            grdInspection.View.AddTextBoxColumn("INSPITEMID", 100)
                         .SetIsHidden();

            grdInspection.View.AddTextBoxColumn("CONTROLTYPE", 60)
                         .SetIsHidden();

            grdInspection.View.AddTextBoxColumn("USL", 30)
                         .SetIsHidden();

            grdInspection.View.AddTextBoxColumn("SL", 30)
                         .SetIsHidden();

            grdInspection.View.AddTextBoxColumn("LSL", 30)
                         .SetIsHidden();

            grdInspection.View.AddTextBoxColumn("INSPITEMNAME", 120)
                         .SetLabel("MEASUREITEM")
                         .SetTextAlignment(TextAlignment.Left);

            grdInspection.View.AddTextBoxColumn("SPECRANGE", 80)
                         .SetLabel("SPECRANGESPC")
                         .SetTextAlignment(TextAlignment.Left);

            grdInspection.View.AddTextBoxColumn("INSPECTIONDEFID", 80)
                         .SetIsHidden();

            grdInspection.View.AddTextBoxColumn("INSPECTIONDEFVERSION", 80)
                         .SetIsHidden();

            grdInspection.View.AddTextBoxColumn("INSPECTIONTYPE", 80)
                         .SetIsHidden();

            grdInspection.View.PopulateColumns();
            grdInspection.View.BestFitColumns();
            //grdInspection.View.OptionsView.ShowIndicator = false;

            grdInspection.View.SetIsReadOnly();
            grdInspection.ShowStatusBar = true;
            grdInspection.GridButtonItem = GridButtonItem.None;
        }

        /// <summary>
        /// 측정값 등록 그리드 초기화
        /// </summary>
        private void InitializeGrdMeasuredValue(SmartBandedGrid grdMeasuredValue)
        {
            grdMeasuredValue.View.AddTextBoxColumn("NO", 100)
                            .SetIsHidden();

            grdMeasuredValue.View.AddTextBoxColumn("INSPITEMID", 100)
                            .SetIsHidden();

            grdMeasuredValue.View.AddSpinEditColumn("MEASURVALUE", 100)
                            .SetDisplayFormat("{###,###.####}", MaskTypes.Numeric)
                            .SetIsReadOnly()
                            .SetTextAlignment(TextAlignment.Right);

            grdMeasuredValue.View.AddTextBoxColumn("FILENAME", 100)
                            .SetIsReadOnly()
                            .SetTextAlignment(TextAlignment.Left);

            grdMeasuredValue.View.AddTextBoxColumn("FILEDATA", 10)
                            .SetIsHidden();

            grdMeasuredValue.View.AddTextBoxColumn("FILESIZE", 10)
                            .SetIsHidden();

            grdMeasuredValue.View.AddTextBoxColumn("FILEEXT", 10)
                            .SetIsHidden();

            grdMeasuredValue.View.AddDateEditColumn("MEASUREDATETIME", 130)
                            .SetIsReadOnly()
                            .SetTextAlignment(TextAlignment.Left);

            grdMeasuredValue.View.AddTextBoxColumn("RESULT", 10)
                            .SetIsHidden();

            grdMeasuredValue.View.PopulateColumns();
            grdMeasuredValue.View.BestFitColumns();

            grdMeasuredValue.ShowStatusBar = false;
            grdMeasuredValue.GridButtonItem = GridButtonItem.None;
            grdMeasuredValue.View.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            grdMeasuredValue.View.GridControl.ToolTipController = new ToolTipController();
        }

        /// <summary>
        /// 제품 정보 그리드 초기화(재의뢰/이전결과)
        /// </summary>
        private void InitializeGrdProductInformation(SmartBandedGrid grid)
        {
            grid.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Restore;

            grid.View
                .AddTextBoxColumn("PRODUCTDEFID", 150)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 품목코드

            grid.View
                .AddTextBoxColumn("PRODUCTDEFNAME", 210)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 품목명

            grid.View
                .AddTextBoxColumn("PRODUCTDEFVERSION", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center); // Rev

            grid.View
                .AddTextBoxColumn("LOTID", 150)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // LOT ID

            grid.View
                .AddTextBoxColumn("PROCESSSEGMENTNAME", 210)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 표준공정

            grid.View
                .AddTextBoxColumn("AREANAME", 210)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 작업장

            grid.View
                .AddTextBoxColumn("EQUIPMENTNAME", 210)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 설비(호기)

            grid.View
                .AddTextBoxColumn("WORKENDTIME", 180)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("TRACKOUTTIME"); // 작업종료시간

            grid.View.PopulateColumns();
        }

        /// <summary>
        /// 제조 이력 그리드 초기화
        /// </summary>
        private void InitializeGrdMeasureHistory()
        {
            grdManufacturingHistory.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Restore;

            grdManufacturingHistory.View
                .AddTextBoxColumn("USERSEQUENCE", 100)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center); // 공정순서(번호)

            grdManufacturingHistory.View
                .AddTextBoxColumn("LOTID", 180)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // LOT ID

            grdManufacturingHistory.View
                .AddTextBoxColumn("PROCESSSEGMENTNAME", 210)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 공정명

            grdManufacturingHistory.View
                .AddTextBoxColumn("AREANAME", 210)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 작업장

            grdManufacturingHistory.View
                .AddTextBoxColumn("EQUIPMENTNAME", 210)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 설비(호기)

            grdManufacturingHistory.View
                .AddTextBoxColumn("TRACKOUTTIME", 180)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center); // 작업종료시간

            grdManufacturingHistory.View.PopulateColumns();
        }

        #endregion

        #region Event

        private void InitializeEvent()
        {
            this.Load += ReReliabilityVerificationRequestRegularPopup_Load;

            tabReReliabilityVerificationRequestRegular.SelectedPageChanged += TabReReliabilityVerificationRequestRegular_SelectedPageChanged;
            btnSave.Click += BtnSave_Click;
            btnClose.Click += BtnClose_Click;

            // 검사항목 리스트에서 항목 클릭 이벤트
            grdInspection2.View.SelectionChanged += (s, e) =>
            {
                if (grdInspection2.View.GetFocusedDataRow() is DataRow dr)
                {
                    grdMeasuredValue2.View.ActiveFilterString = string.Concat("[INSPITEMID] = '", DefectMapHelper.StringByDataRowObejct(dr, "INSPITEMID"), "'");

                    /*
                    if (_isEdit)
                    {
                        grdMeasuredValue.GridButtonItem = GridButtonItem.None;
                        return;
                    }
                    */

                    if (grdMeasuredValue2.View.DataRowCount.Equals(0))
                    {
                        grdMeasuredValue2.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete;
                    }
                    else
                    {
                        if (grdMeasuredValue2.View.GetDataRow(0).RowState.Equals(DataRowState.Added))
                        {
                            grdMeasuredValue2.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete;
                        }
                        else
                        {
                            grdMeasuredValue2.GridButtonItem = GridButtonItem.None;
                        }

                        //if (!Format.GetString(dr["SPECRANGE"]).Equals(Format.GetString(grdMeasuredValue.View.GetDataRow(0)["SPECRANGE"])))
                        //{
                        //    //(grdInspection.DataSource as DataTable).Rows[grdInspection.View.GetFocusedDataSourceRowIndex()];
                        //    dr["SPECRANGE"] = Format.GetString(grdMeasuredValue.View.GetDataRow(0)["SPECRANGE"]);
                        //}
                    }
                }
            };

            //InitializeGrdMeasureValue(grdMeasuredValue1); // 재의뢰 측정값 이벤트 초기화
            InitializeGrdMeasureValue(grdMeasuredValue2); // 이전결과 측정값 이벤트 조치과
        }

        /// <summary>        
        /// 측정값 이벤트 초기화
        /// </summary>
        private void InitializeGrdMeasureValue(SmartBandedGrid grdMeasuredValue)
        {
            // 측정 값 리스트에서 측정값이 규격 범위에 벗어났을 경우 처리 이벤트
            grdMeasuredValue.View.RowCellStyle += (s, e) =>
            {
                string result = grdMeasuredValue.View.GetRowCellValue(e.RowHandle, "RESULT").ToString();

                if (result == string.Empty)
                {
                    return;
                }

                if (e.Column.FieldName == "MEASURVALUE")
                {
                    e.Appearance.BackColor = result == "OK" ? e.Appearance.BackColor : Color.Red;
                    e.Appearance.ForeColor = result == "OK" ? e.Appearance.ForeColor : Color.White;
                }
            };

            // 측정값 첨부파일 툴팁 이벤트
            grdMeasuredValue.View.GridControl.ToolTipController.GetActiveObjectInfo += (s, e) =>
            {
                if (!e.SelectedControl.Equals(grdMeasuredValue))
                {
                    var hitInfo = grdMeasuredValue.View.CalcHitInfo(e.ControlMousePosition);
                    if (!hitInfo.InRowCell)
                    {
                        return;
                    }

                    if (hitInfo.Column.FieldName != "FILENAME")
                    {
                        return;
                    }

                    DataRow dr = (grdMeasuredValue.View.GetRow(hitInfo.RowHandle) as DataRowView).Row;

                    if (dr["FILEDATA"].ToString() == string.Empty && dr["FILENAME"].ToString() == string.Empty)
                    {
                        return;
                    }

                    ToolTipControlInfo toolTipControlInfo = new ToolTipControlInfo(hitInfo.RowHandle, "FILEDATA");
                    SuperToolTip superToolTip = new SuperToolTip();
                    superToolTip.Items.AddTitle(dr["FILENAME"].ToString());
                    superToolTip.Items.Add(new ToolTipItem()
                    {
                        Image = new Bitmap((Bitmap)new ImageConverter().ConvertFrom((byte[])dr["FILEDATA"]), new Size(300, 300))
                    });

                    toolTipControlInfo.SuperTip = superToolTip;
                    e.Info = toolTipControlInfo;
                }
            };
        }

        /// <summary>        
        /// 탭이 변경 되었을때 호출
        /// </summary>
        private void TabReReliabilityVerificationRequestRegular_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            SmartTabControl tab = sender as SmartTabControl;

            if (tab.SelectedTabPageIndex == 0) // 의뢰서 출력 조회
            {
                DataTable dt = SearchProductInformation(CurrentDataRow["REQUESTNO"].ToString(), grdProductInformation1); // 제품 정보 조회(재의뢰)
                DataRow dr = dt.Rows[0];
                //SearchInspection(dr, grdInspection1, grdMeasuredValue1); // 계측값 조회
                btnSave.Enabled = true;
            }
            else if (tab.SelectedTabPageIndex == 1) // 의로서 접수 조회
            {
                DataTable dt = SearchProductInformation(CurrentDataRow["PARENTREQUESTNO"].ToString(), grdProductInformation2); // 제품 정보 조회(이전결과)
                DataRow dr = dt.Rows[0];
                SearchMeasuringHistory();
                SearchInspection(dr, grdInspection2, grdMeasuredValue2); // 계측값 조회
                btnSave.Enabled = false;
            }

        }

        /// <summary>
        /// Form Road시 초기화
        /// </summary>
        private void ReReliabilityVerificationRequestRegularPopup_Load(object sender, EventArgs e)
        {
            btnSave.Enabled = isEnable; // 부모창의 버튼 활성화 여부에 따라 저장 버튼 활성화

            SearchProductInformation(CurrentDataRow["REQUESTNO"].ToString(), grdProductInformation1); // 제품 정보 조회
        }

        /// <summary>
        /// 저장버튼을 클릭했을때 검사 결과를 저장하는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            DialogResult result = System.Windows.Forms.DialogResult.No;

            result = this.ShowMessage(MessageBoxButtons.YesNo, "RequestNote");//의뢰를 저장하시겠습니까?

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    this.ShowWaitArea();
                    btnSave.Enabled = false;
                    btnClose.Enabled = false;

                    SaveData();

                    ShowMessage("SuccessSave");

                }
                catch (Exception ex)
                {
                    this.ShowError(ex);
                }
                finally
                {
                    this.CloseWaitArea();
                    btnSave.Enabled = true;
                    btnClose.Enabled = true;
                    this.Close();
                }
            }
        }

        /// <summary>
        /// 닫기버튼을 클릭했을때 팝업을 닫는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        #endregion

        #region Private Function

        /// <summary>
        /// 제품 정보 조회
        /// </summary>
        private DataTable SearchProductInformation(string requestno, SmartBandedGrid grid)
        {
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("P_REQUESTNO", requestno);
            values.Add("P_LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("P_PLANTID", UserInfo.Current.Plant);

            DataTable dt = SqlExecuter.Query("GetReliabilityVerificationRequestRegularRgisterList", "10001", values);

            grid.DataSource = dt;

            return dt;
        }

        /// <summary>
        /// 제조 이력 정보 조회
        /// </summary>
        private void SearchMeasuringHistory()
        {
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("P_REQUESTNO", CurrentDataRow["PARENTREQUESTNO"]);
            values.Add("P_LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable dt = SqlExecuter.Query("GetManufacturingHistoryList", "10001", values);

            grdManufacturingHistory.DataSource = dt;
        }

        /// <summary>
        /// 측정 항목 정보 조회
        /// </summary>
        private void SearchInspection(DataRow dr, SmartBandedGrid grdInspection, SmartBandedGrid grdMeasuredValue)
        {
            Dictionary<string, object> value = new Dictionary<string, object>()
            {
                {"P_LOTID", dr["LOTID"] },
                {"P_PLANTID", dr["PLANTID"] },
                {"P_AREAID", dr["AREAID"] },           
                {"P_LOTTYPE", dr["LOTTYPE"] },
                {"P_MEASURER", UserInfo.Current.Name },
                {"P_PROCESSSEGMENTID", dr["PROCESSSEGMENTID"] },
                {"P_PRODUCTDEFID", dr["PRODUCTDEFID"] }
            };

            if (SqlExecuter.Query("GetInsepctionSpecListByItem", "10002"
                                , DefectMapHelper.AddLanguageTypeToConditions(value)) is DataTable dt)
            {
                // Measured Value List 조회
                if (SqlExecuter.Query("GetQualitySpecificationValue", "10001", value) is DataTable subDt)
                {
                    grdMeasuredValue.DataSource = subDt;
                }

                grdInspection.DataSource = dt;
            }
        }

        /// <summary>
        /// CT_RELIABILITYREFMANUFACTURING 테이블에 데이터 저장
        /// </summary>
        private void SaveData()
        {
            DataTable dt = manufacturingHistoryControl1.CurrentDataTable();

            if (dt.Rows.Count == 0)
            {
                throw MessageException.Create("NoSaveData");
            }

            DataTable productInfoTable = grdProductInformation1.DataSource as DataTable;
            DataRow productInfoRow = productInfoTable.Rows[0];

            // CT_RELIABILITYREFMANUFACTURING Table에 저장될 Data
            DataTable menufacturingTable = new DataTable();
            menufacturingTable.TableName = "list";
            DataRow row = null;

            menufacturingTable.Columns.Add(new DataColumn("ENTERPRISEID", typeof(string)));
            menufacturingTable.Columns.Add(new DataColumn("FACTORYID", typeof(string)));
            menufacturingTable.Columns.Add(new DataColumn("PRODUCTDEFID", typeof(string)));
            menufacturingTable.Columns.Add(new DataColumn("PRODUCTDEFVERSION", typeof(string)));
            menufacturingTable.Columns.Add(new DataColumn("LOTID", typeof(string)));
            menufacturingTable.Columns.Add(new DataColumn("REQUESTNO", typeof(string)));
            menufacturingTable.Columns.Add(new DataColumn("INSPECTIONDEFID", typeof(string)));
            menufacturingTable.Columns.Add(new DataColumn("INSPECTIONDEFVERSION", typeof(string)));
            menufacturingTable.Columns.Add(new DataColumn("REFERENCELOTID", typeof(string)));
            menufacturingTable.Columns.Add(new DataColumn("REFERENCETXNHISTKEY", typeof(string)));

            foreach (DataRow dr in dt.Rows)
            {
                row = menufacturingTable.NewRow();
                row["ENTERPRISEID"] = productInfoRow["ENTERPRISEID"];
                row["FACTORYID"] = productInfoRow["PLANTID"];
                row["PRODUCTDEFID"] = productInfoRow["PRODUCTDEFID"];
                row["PRODUCTDEFVERSION"] = productInfoRow["PRODUCTDEFVERSION"];
                row["LOTID"] = productInfoRow["LOTID"];
                row["REQUESTNO"] = productInfoRow["REQUESTNO"];
                row["INSPECTIONDEFID"] = "*";
                row["INSPECTIONDEFVERSION"] = "*";
                row["REFERENCELOTID"] = dr["LOTID"];
                row["REFERENCETXNHISTKEY"] = dr["LOTWORKTXNHISTKEY"];

                menufacturingTable.Rows.Add(row);
            }

            DataSet rullSet = new DataSet();

            rullSet.Tables.Add(menufacturingTable);

            ExecuteRule("SaveReliabilityVerificationRequestRegular", rullSet);
        }

        #endregion
    }
}