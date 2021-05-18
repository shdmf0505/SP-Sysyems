using DevExpress.Utils;
using DevExpress.Utils.Filtering.Internal;
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
    /// 업  무  설  명  : 신뢰성 의뢰(정기)를 등록하는 팝업
    /// 생    성    자  : 유석진
    /// 생    성    일  : 2019-07-12
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ReliabilityVerificationRequestRegularPopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region Local Variables

        /// <summary>
        /// 팝업진입전 그리드의 Row
        /// </summary>
        public DataRow CurrentDataRow { get; set; }
        public bool isEnable = true;

        #endregion

        #region 생성자

        public ReliabilityVerificationRequestRegularPopup(DataRow dr)
        {
            InitializeComponent();

            CurrentDataRow = dr;

            manufacturingHistoryControl1.CurrentDataRow = dr;
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
            grdInspection.LanguageKey = "MEASUREITEMLIST";
            grdMeasuredValue.LanguageKey = "MEASUREDVALUE";
        }

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            InitializeGrdProductInformation();
            InitializeGrdInspection();
            InitializeGrdMeasuredValue();
        }

        /// <summary>
        /// 측정 항목 그리드 초기화
        /// </summary>
        private void InitializeGrdInspection()
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
        private void InitializeGrdMeasuredValue()
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
        /// 제품 정보 그리드 초기화
        /// </summary>
        private void InitializeGrdProductInformation()
        {
            grdProductnformation.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Restore;

            grdProductnformation.View
                .AddTextBoxColumn("PRODUCTDEFID", 150)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 품목코드

            grdProductnformation.View
                .AddTextBoxColumn("PRODUCTDEFNAME", 210)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 품목명

            grdProductnformation.View
                .AddTextBoxColumn("PRODUCTDEFVERSION", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center); // Rev

            grdProductnformation.View
                .AddTextBoxColumn("LOTID", 150)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // LOT ID

            grdProductnformation.View
                .AddTextBoxColumn("PROCESSSEGMENTNAME", 210)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 표준공정

            grdProductnformation.View
                .AddTextBoxColumn("AREANAME", 210)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 작업장

            grdProductnformation.View
                .AddTextBoxColumn("EQUIPMENTNAME", 210)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 설비(호기)

            grdProductnformation.View
                .AddTextBoxColumn("WORKENDTIME", 180)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("TRACKOUTTIME"); // 작업종료시간

            grdProductnformation.View.PopulateColumns();
        }

        #endregion

        #region Event

        /// <summary>
        /// 이벤트 초기화
        /// </summary>
        private void InitializeEvent()
        {
            this.Load += ReliabilityVerificationRequestRegularPopup_Load;

            btnSave.Click += BtnSave_Click;
            btnClose.Click += BtnClose_Click;

            // 검사항목 리스트에서 항목 클릭 이벤트
            grdInspection.View.SelectionChanged += (s, e) =>
            {
                if (grdInspection.View.GetFocusedDataRow() is DataRow dr)
                {
                    grdMeasuredValue.View.ActiveFilterString = string.Concat("[INSPITEMID] = '", DefectMapHelper.StringByDataRowObejct(dr, "INSPITEMID"), "'");

                    /*
                    if (_isEdit)
                    {
                        grdMeasuredValue.GridButtonItem = GridButtonItem.None;
                        return;
                    }
                    */

                    if (grdMeasuredValue.View.DataRowCount.Equals(0))
                    {
                        grdMeasuredValue.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete;
                    }
                    else
                    {
                        if (grdMeasuredValue.View.GetDataRow(0).RowState.Equals(DataRowState.Added))
                        {
                            grdMeasuredValue.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete;
                        }
                        else
                        {
                            grdMeasuredValue.GridButtonItem = GridButtonItem.None;
                        }

                        //if (!Format.GetString(dr["SPECRANGE"]).Equals(Format.GetString(grdMeasuredValue.View.GetDataRow(0)["SPECRANGE"])))
                        //{
                        //    //(grdInspection.DataSource as DataTable).Rows[grdInspection.View.GetFocusedDataSourceRowIndex()];
                        //    dr["SPECRANGE"] = Format.GetString(grdMeasuredValue.View.GetDataRow(0)["SPECRANGE"]);
                        //}
                    }
                }
            };

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
        /// Form Road시 초기화
        /// </summary>
        private void ReliabilityVerificationRequestRegularPopup_Load(object sender, EventArgs e)
        {
            btnSave.Enabled = isEnable; // 부모창의 버튼 활성화 여부에 따라 저장 버튼 활성화

            SearchProductInformation(); // 제품 정보 조회
            SearchInspection(); // 측정 항목 조회
        }

        /// <summary>
        /// 저장버튼을 클릭했을때 검사 결과를 저장하는 이벤트
        /// </summary>
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
            this.Close();
        }

        #endregion

        #region 검색

        #endregion

        #region Private Function

        /// <summary>
        /// 제품 정보 조회
        /// </summary>
        private void SearchProductInformation()
        {
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("P_REQUESTNO", CurrentDataRow["REQUESTNO"]);
            values.Add("P_LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);
            values.Add("P_PLANTID", Framework.UserInfo.Current.Plant);

            DataTable dt = SqlExecuter.Query("GetReliabilityVerificationRequestRegularRgisterList", "10001", values);

            grdProductnformation.DataSource = dt;
        }

        /// <summary>
        /// 측정 항목 정보 조회
        /// </summary>
        private void SearchInspection()
        {
            Dictionary<string, object> value = new Dictionary<string, object>()
            {
                {"P_LOTID", CurrentDataRow["LOTID"] },
                {"P_AREAID", CurrentDataRow["AREAID"] },
                {"P_PLANTID", CurrentDataRow["PLANTID"] },
                {"P_LOTTYPE", CurrentDataRow["LOTTYPE"] },
                {"P_MEASURER", UserInfo.Current.Name },
                {"P_PROCESSSEGMENTID", CurrentDataRow["PROCESSSEGMENTID"] },
                {"P_PRODUCTDEFID", CurrentDataRow["PRODUCTDEFID"] }
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
        /// CT_QCRELIABILITYSEGMENTREF 테이블에 데이터 저장
        /// </summary>
        private void SaveData()
        {
            DataTable dt = manufacturingHistoryControl1.CurrentDataTable();

            if(dt.Rows.Count == 0)
            {
                throw MessageException.Create("NoSaveData");
            }

            DataTable productInfoTable = grdProductnformation.DataSource as DataTable;
            DataRow productInfoRow = productInfoTable.Rows[0];

            // CT_RELIABILITYREFMANUFACTURING Table에 저장될 Data
            DataTable menufacturingTable = new DataTable();
            menufacturingTable.TableName = "list";
            DataRow row = null;

            menufacturingTable.Columns.Add(new DataColumn("ENTERPRISEID", typeof(string)));
            menufacturingTable.Columns.Add(new DataColumn("FACTORYID", typeof(string)));
            menufacturingTable.Columns.Add(new DataColumn("LOTID", typeof(string)));
            menufacturingTable.Columns.Add(new DataColumn("REQUESTNO", typeof(string)));
            menufacturingTable.Columns.Add(new DataColumn("PRODUCTDEFID", typeof(string)));
            menufacturingTable.Columns.Add(new DataColumn("PRODUCTDEFVERSION", typeof(string)));
            menufacturingTable.Columns.Add(new DataColumn("REFERENCELOTID", typeof(string)));
            menufacturingTable.Columns.Add(new DataColumn("REFERENCETXNHISTKEY", typeof(string)));

            foreach (DataRow dr in dt.Rows)
            {
                row = menufacturingTable.NewRow();
                row["ENTERPRISEID"] = productInfoRow["ENTERPRISEID"];
                row["FACTORYID"] = productInfoRow["PLANTID"];
                row["LOTID"] = productInfoRow["LOTID"];
                row["REQUESTNO"] = productInfoRow["REQUESTNO"];
                row["PRODUCTDEFID"] = productInfoRow["PRODUCTDEFID"];
                row["PRODUCTDEFVERSION"] = productInfoRow["PRODUCTDEFVERSION"];
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