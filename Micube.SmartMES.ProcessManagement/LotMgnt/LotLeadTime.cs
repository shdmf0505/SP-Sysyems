#region using
using DevExpress.XtraGrid.Views.BandedGrid.ViewInfo;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons;
using Micube.Framework.SmartControls.Grid.Conditions;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Micube.SmartMES.Commons.Controls;
using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraPivotGrid;
using DevExpress.XtraGrid.Views.Grid;
#endregion

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 공정관리 > LOT관리 > 제조리드타임
    /// 업  무  설  명  : 제조리드타임 조회
    /// 생    성    자  : 박정훈
    /// 생    성    일  : 2019-10-28
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class LotLeadTime : SmartConditionManualBaseForm
    {
        #region ◆ Local Variables |

        string field = "";
        string layer = "";
        string processsegment = "";

        #endregion

        #region ◆ 생성자 |
        /// <summary>
        /// 생성자
        /// </summary>
        public LotLeadTime()
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

        //    xtraTabPage1.PageVisible = false;
        //   tpggraph.PageVisible = false;

            
        }

        #region ▶ 조회조건 설정 |
        /// <summary>
        /// 검색조건 설정
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // 품목
            CommonFunction.AddConditionProductPopup("P_PRODUCTDEFID", 2.1, true, Conditions);

            #region - 품목 VERSION 설정 |
            // 품목 VERSION 설정
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
            #endregion

            // Lot
            CommonFunction.AddConditionLotPopup("P_LOTID", 3.1, true, Conditions);

            // 작업장
            CommonFunction.AddConditionAreaPopup("P_AREAID", 3.2, true, Conditions);

            // 공정 선택
            CommonFunction.AddConditionProcessSegmentPopup("FROM_PROCESSSEGMENTID", 5.1, "FROMPROCESSSEGMENTNAME", false, Conditions);
            CommonFunction.AddConditionProcessSegmentPopup("TO_PROCESSSEGMENTID", 5.2, "TOPROCESSSEGMENTNAME", false, Conditions);
        }

        /// <summary>
        /// 조회조건 설정
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();
            Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").EditValueChanged += LotLeadTime_EditValueChanged;

        }

        private void LotLeadTime_EditValueChanged(object sender, EventArgs e)
        {

            int rowHandel = grdProduct.View.FocusedRowHandle;
            SmartBandedGrid grid = null;

            switch (tabMain.SelectedTabPage.Name)
            {
                case "tpgType":
                    grid = grdType;
                    break;
                case "tpgProduct":
                    grid = grdProduct;
                    break;
                case "tpgLot":
                    grid = grdLotList;
                    break;
                case "tpgProcess":
                    grid = grdProcess;
                    break;
                case "tpgArea":
                    grid = grdArea;
                    break;



            }
            String ProductdefId = Format.GetTrimString(grid.View.GetRowCellValue(rowHandel, "PRODUCTDEFID"));
            String Productdefversion = Format.GetTrimString(grid.View.GetRowCellValue(rowHandel, "PRODUCTDEFVERSION"));

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            param.Add("P_PLANTID", UserInfo.Current.Plant);
            param.Add("P_PRODUCTDEFID", ProductdefId);

            DataTable dt = SqlExecuter.Query("selectProductdefVesion", "10001", param);


            Conditions.GetControl<SmartComboBox>("P_PRODUCTDEFVERSION").DataSource = dt;
   
            Conditions.GetControl<SmartComboBox>("P_PRODUCTDEFVERSION").EditValue = Format.GetFullTrimString(Productdefversion);
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
            #region - Type |
            grdType.GridButtonItem = GridButtonItem.Export;

            grdType.View.SetIsReadOnly();

            var grpTypeDateCol = grdType.View.AddGroupColumn("");
            grpTypeDateCol.AddTextBoxColumn("DATE", 120).SetTextAlignment(TextAlignment.Center);

            var grpTypeTotalCol = grdType.View.AddGroupColumn("TOTALLEADTIME");
            grpTypeTotalCol.AddTextBoxColumn("SHAPE_SUMTOTAL", 80).SetTextAlignment(TextAlignment.Center).SetLabel("LTDAY_SUM");
            grpTypeTotalCol.AddTextBoxColumn("LOTCNT", 80).SetTextAlignment(TextAlignment.Center); 
            grpTypeTotalCol.AddTextBoxColumn("SHAPE_TOTAL", 80).SetTextAlignment(TextAlignment.Center);
            grpTypeTotalCol.AddTextBoxColumn("SHAPE_SS", 80).SetTextAlignment(TextAlignment.Center);
            grpTypeTotalCol.AddTextBoxColumn("SHAPE_DS", 80).SetTextAlignment(TextAlignment.Center);
            grpTypeTotalCol.AddTextBoxColumn("SHAPE_MT", 80).SetTextAlignment(TextAlignment.Center);
            grpTypeTotalCol.AddTextBoxColumn("SHAPE_RF", 80).SetLabel("YIELDRF").SetTextAlignment(TextAlignment.Center);

            var grpTypeDomesticCol = grdType.View.AddGroupColumn("DOMESTIC_LT");
            grpTypeDomesticCol.AddTextBoxColumn("DS_SUMTOTAL", 80).SetTextAlignment(TextAlignment.Center).SetLabel("LTDAY_SUM");
            grpTypeDomesticCol.AddTextBoxColumn("DS_LOTCNT", 80).SetTextAlignment(TextAlignment.Center).SetLabel("LOTCNT");
            grpTypeDomesticCol.AddTextBoxColumn("DS_TOTAL", 80).SetTextAlignment(TextAlignment.Center);
            grpTypeDomesticCol.AddTextBoxColumn("DS_SS", 80).SetTextAlignment(TextAlignment.Center);
            grpTypeDomesticCol.AddTextBoxColumn("DS_DS", 80).SetTextAlignment(TextAlignment.Center);
            grpTypeDomesticCol.AddTextBoxColumn("DS_MT", 80).SetTextAlignment(TextAlignment.Center);
            grpTypeDomesticCol.AddTextBoxColumn("DS_RF", 80).SetLabel("YIELDRF").SetTextAlignment(TextAlignment.Center);

            var grpTypeForeignCol = grdType.View.AddGroupColumn("FOREIGN_LT");
            grpTypeForeignCol.AddTextBoxColumn("FS_SUMTOTAL", 80).SetTextAlignment(TextAlignment.Center).SetLabel("LTDAY_SUM");
            grpTypeForeignCol.AddTextBoxColumn("FS_LOTCNT", 80).SetTextAlignment(TextAlignment.Center).SetLabel("LOTCNT");
            grpTypeForeignCol.AddTextBoxColumn("FS_TOTAL", 80).SetTextAlignment(TextAlignment.Center);
            grpTypeForeignCol.AddTextBoxColumn("FS_SS", 80).SetTextAlignment(TextAlignment.Center);
            grpTypeForeignCol.AddTextBoxColumn("FS_DS", 80).SetTextAlignment(TextAlignment.Center);
            grpTypeForeignCol.AddTextBoxColumn("FS_MT", 80).SetTextAlignment(TextAlignment.Center);
            grpTypeForeignCol.AddTextBoxColumn("FS_RF", 80).SetLabel("YIELDRF").SetTextAlignment(TextAlignment.Center);

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);

            DataTable dt = SqlExecuter.Query("GetPlantList", "00001", param);

            foreach (DataRow dr in dt.Rows)
            {
                string strPlant = dr["PLANTID"].ToString();

                ConditionItemGroup grpPlant = new ConditionItemGroup();

                grpPlant = grdType.View.AddGroupColumn(strPlant);
                grpPlant.AddTextBoxColumn(strPlant + "_TOTAL", 80).SetTextAlignment(TextAlignment.Center);
                grpPlant.AddTextBoxColumn(strPlant + "_SS", 80).SetTextAlignment(TextAlignment.Center);
                grpPlant.AddTextBoxColumn(strPlant + "_DS", 80).SetTextAlignment(TextAlignment.Center);
                grpPlant.AddTextBoxColumn(strPlant + "_MT", 80).SetTextAlignment(TextAlignment.Center);
                grpPlant.AddTextBoxColumn(strPlant + "_RF", 80).SetLabel("YIELDRF").SetTextAlignment(TextAlignment.Center);
            }

            grdType.View.PopulateColumns();

            #region 제품타입 코드 tag로 하드코딩
            for (int i=0; i<grdType.View.Columns.Count; i++)
            {
                string ColName = grdType.View.Columns[i].FieldName;
                if(ColName.Contains("_TOTAL"))
                {
                    grdType.View.Columns[i].Tag = "*";
                }
                else if (ColName.Contains("_SS"))
                {
                    grdType.View.Columns[i].Tag = "SS";
                }
                else if (ColName.Contains("_DS"))
                {
                    grdType.View.Columns[i].Tag = "DS";
                }
                else if (ColName.Contains("_MT"))
                {
                    grdType.View.Columns[i].Tag = "MT";
                }
                else if (ColName.Contains("_RF"))
                {
                    grdType.View.Columns[i].Tag = "RF";
                }

            }

            InitTypeSummaryRow();
            #endregion


            #endregion

            #region - 품목 |

            grdProduct.GridButtonItem = GridButtonItem.Export;

            grdProduct.View.SetIsReadOnly();

            var grpProductCol = grdProduct.View.AddGroupColumn("TABCURRENTBYITEM"); 
            grpProductCol.AddTextBoxColumn("LOTTYPE", 60).SetTextAlignment(TextAlignment.Center); // 타입(양면, 단면)
            grpProductCol.AddTextBoxColumn("PRODUCTSHAPE", 60).SetTextAlignment(TextAlignment.Center);
            grpProductCol.AddTextBoxColumn("CUSTOMERNAME", 100).SetTextAlignment(TextAlignment.Center);
            grpProductCol.AddTextBoxColumn("PRODUCTPLANTTYPE", 80).SetTextAlignment(TextAlignment.Center);  // 생산유형 (국내, 해외)
            grpProductCol.AddTextBoxColumn("PRODUCTDEFID", 150).SetTextAlignment(TextAlignment.Center);
            grpProductCol.AddTextBoxColumn("PRODUCTDEFVERSION", 60).SetTextAlignment(TextAlignment.Center);
            grpProductCol.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            grpProductCol.AddTextBoxColumn("LAYER", 60).SetTextAlignment(TextAlignment.Center);
            var grpProductLTCol = grdProduct.View.AddGroupColumn("LTDAY_LEADTIME");
            grpProductLTCol.AddTextBoxColumn("TOTALLEADTIME", 70).SetTextAlignment(TextAlignment.Right);
            grpProductLTCol.AddTextBoxColumn("DOMESTIC_LT_NORMAL", 70).SetTextAlignment(TextAlignment.Right); // 국내
            grpProductLTCol.AddTextBoxColumn("DOMESTIC_LT_NOT_NORMAL", 70).SetTextAlignment(TextAlignment.Right); // 비정상
            grpProductLTCol.AddTextBoxColumn("FOREIGN_LT_NORMAL", 70).SetTextAlignment(TextAlignment.Right); // 해외
            grpProductLTCol.AddTextBoxColumn("FOREIGN_LT_NOT_NORMAL", 70).SetTextAlignment(TextAlignment.Right); // 해외 비정상

            var grpProductETCCol = grdProduct.View.AddGroupColumn("ETC"); 
            grpProductETCCol.AddTextBoxColumn("LOTCNT", 80).SetTextAlignment(TextAlignment.Right); 

            grdProduct.View.PopulateColumns();
            InitProductSummaryRow();
            #endregion

            #region - LOT |
            grdLotList.GridButtonItem = GridButtonItem.Export;

            grdLotList.View.SetIsReadOnly();

            grdLotList.View.AddTextBoxColumn("LOTTYPE", 60).SetTextAlignment(TextAlignment.Center);
            // 타입(양면, 단면)
            grdLotList.View.AddTextBoxColumn("PRODUCTSHAPE", 60).SetTextAlignment(TextAlignment.Center);
            grdLotList.View.AddTextBoxColumn("CUSTOMERNAME", 100).SetTextAlignment(TextAlignment.Center);
            grdLotList.View.AddTextBoxColumn("PRODUCTPLANTTYPE", 80).SetTextAlignment(TextAlignment.Center);  // 생산유형 (국내, 해외)
            grdLotList.View.AddTextBoxColumn("PRODUCTDEFID", 150).SetTextAlignment(TextAlignment.Center);
            grdLotList.View.AddTextBoxColumn("PRODUCTDEFVERSION", 60).SetTextAlignment(TextAlignment.Center);
            grdLotList.View.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            grdLotList.View.AddTextBoxColumn("LAYER", 60).SetTextAlignment(TextAlignment.Center);
            grdLotList.View.AddTextBoxColumn("LOTID", 190).SetTextAlignment(TextAlignment.Center);
            grdLotList.View.AddTextBoxColumn("TOTALLEADTIME", 70).SetTextAlignment(TextAlignment.Right);
            grdLotList.View.AddTextBoxColumn("ROOTLOTID", 190).SetTextAlignment(TextAlignment.Center);
            grdLotList.View.AddTextBoxColumn("DOMESTIC_LT_NORMAL", 70).SetTextAlignment(TextAlignment.Right); // 국내
            grdLotList.View.AddTextBoxColumn("DOMESTIC_LT_NOT_NORMAL", 70).SetTextAlignment(TextAlignment.Right); // 비정상
            grdLotList.View.AddTextBoxColumn("FOREIGN_LT_NORMAL", 70).SetTextAlignment(TextAlignment.Right); // 해외
            grdLotList.View.AddTextBoxColumn("FOREIGN_LT_NOT_NORMAL", 70).SetTextAlignment(TextAlignment.Right); // 해외 비정상

            grdLotList.View.PopulateColumns();
            InitLotSummaryRow();
            #endregion

            #region - 공정 |
            grdProcess.GridButtonItem = GridButtonItem.Export;

            grdProcess.View.SetIsReadOnly();

            var grpProcessCol = grdProcess.View.AddGroupColumn("TABCURRENTBYITEM");
            grpProcessCol.AddTextBoxColumn("LOTTYPE", 60).SetTextAlignment(TextAlignment.Center); // 타입(양면, 단면)
            grpProcessCol.AddTextBoxColumn("PRODUCTSHAPE", 60).SetTextAlignment(TextAlignment.Center);
            grpProcessCol.AddTextBoxColumn("CUSTOMERNAME", 100).SetTextAlignment(TextAlignment.Center);
            grpProcessCol.AddTextBoxColumn("PRODUCTPLANTTYPE", 80).SetTextAlignment(TextAlignment.Center);  // 생산유형 (국내, 해외)
            grpProcessCol.AddTextBoxColumn("PRODUCTDEFID", 150).SetTextAlignment(TextAlignment.Center);
            grpProcessCol.AddTextBoxColumn("PRODUCTDEFVERSION", 60).SetTextAlignment(TextAlignment.Center);
            grpProcessCol.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            grpProcessCol.AddTextBoxColumn("PLANTID", 70).SetTextAlignment(TextAlignment.Center);
            grpProcessCol.AddTextBoxColumn("USERSEQUENCE", 70).SetTextAlignment(TextAlignment.Center);
            grpProcessCol.AddTextBoxColumn("PROCESSSEGMENTNAME", 120); 

            var grpProcessStepCol = grdProcess.View.AddGroupColumn("STEPPERLT");
            grpProcessStepCol.AddTextBoxColumn("RECEIVELEADTIME", 70).SetLabel("LTDAY_WAITFORRECEIVE").SetTextAlignment(TextAlignment.Right);
            grpProcessStepCol.AddTextBoxColumn("WORKSTARTLEADTIME", 70).SetLabel("LTDAY_WORKSTART").SetTextAlignment(TextAlignment.Right);
            grpProcessStepCol.AddTextBoxColumn("WORKENDLEADTIME", 70).SetLabel("LTDAY_WORKEND").SetTextAlignment(TextAlignment.Right);
            grpProcessStepCol.AddTextBoxColumn("SENDLEADTIME", 70).SetLabel("LTDAY_WAITFORSEND").SetTextAlignment(TextAlignment.Right);
            grpProcessStepCol.AddTextBoxColumn("LEADTIME", 70).SetLabel("LTDAY_SUM").SetTextAlignment(TextAlignment.Right);
            grpProcessStepCol.AddTextBoxColumn("LEADTIME_NORMAL", 70).SetLabel("NORMAL").SetTextAlignment(TextAlignment.Right);
            grpProcessStepCol.AddTextBoxColumn("LEADTIME_NOT_NORMAL", 70).SetLabel("CAPAIDLE").SetTextAlignment(TextAlignment.Right);
            grpProcessStepCol.AddTextBoxColumn("AVG_LEADTIME", 70).SetLabel("LTDAY_LEADTIME").SetTextAlignment(TextAlignment.Right);

            var grpProcessETCCol = grdProcess.View.AddGroupColumn("ETC");
            grpProcessETCCol.AddTextBoxColumn("LOTCNT", 80).SetTextAlignment(TextAlignment.Right);
            grpProcessETCCol.AddTextBoxColumn("INPUTQTY", 80).SetTextAlignment(TextAlignment.Right);

            grdProcess.View.PopulateColumns();
            InitSegmentSummaryRow();
            #endregion

            #region - 작업장 |
            grdArea.GridButtonItem = GridButtonItem.Export;

            grdArea.View.SetIsReadOnly();

            var grpAreaCol = grdArea.View.AddGroupColumn("TABCURRENTBYITEM");
            grpAreaCol.AddTextBoxColumn("AREANAME", 120).SetTextAlignment(TextAlignment.Center);
            grpAreaCol.AddTextBoxColumn("PLANTID", 70).SetTextAlignment(TextAlignment.Center);
            grpAreaCol.AddTextBoxColumn("PRODUCTDEFID", 150).SetTextAlignment(TextAlignment.Center);
            grpAreaCol.AddTextBoxColumn("PRODUCTDEFVERSION", 60).SetTextAlignment(TextAlignment.Center);
            grpAreaCol.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            grpAreaCol.AddTextBoxColumn("USERSEQUENCE", 70).SetTextAlignment(TextAlignment.Center);
            grpAreaCol.AddTextBoxColumn("PROCESSSEGMENTNAME", 120);

            var grpAreaStepCol = grdArea.View.AddGroupColumn("STEPPERLT");
            grpAreaStepCol.AddTextBoxColumn("RECEIVELEADTIME", 70).SetLabel("LTDAY_WAITFORRECEIVE").SetTextAlignment(TextAlignment.Right);
            grpAreaStepCol.AddTextBoxColumn("WORKSTARTLEADTIME", 70).SetLabel("LTDAY_WORKSTART").SetTextAlignment(TextAlignment.Right);
            grpAreaStepCol.AddTextBoxColumn("WORKENDLEADTIME", 70).SetLabel("LTDAY_WORKEND").SetTextAlignment(TextAlignment.Right);
            grpAreaStepCol.AddTextBoxColumn("SENDLEADTIME", 70).SetLabel("LTDAY_WAITFORSEND").SetTextAlignment(TextAlignment.Right);
            grpAreaStepCol.AddTextBoxColumn("LEADTIME", 70).SetLabel("LTDAY_SUM").SetTextAlignment(TextAlignment.Right);
            grpAreaStepCol.AddTextBoxColumn("LEADTIME_NORMAL", 70).SetLabel("NORMAL").SetTextAlignment(TextAlignment.Right);
            grpAreaStepCol.AddTextBoxColumn("LEADTIME_NOT_NORMAL", 70).SetLabel("CAPAIDLE").SetTextAlignment(TextAlignment.Right);
            grpAreaStepCol.AddTextBoxColumn("AVG_LEADTIME", 70).SetLabel("LTDAY_LEADTIME").SetTextAlignment(TextAlignment.Right);


            var grpAreaETCCol = grdArea.View.AddGroupColumn("ETC");
            grpAreaETCCol.AddTextBoxColumn("LOTCNT", 80).SetTextAlignment(TextAlignment.Right);
            grpAreaETCCol.AddTextBoxColumn("INPUTQTY", 80).SetTextAlignment(TextAlignment.Right);

            grdArea.View.PopulateColumns();

            InitAreaSummaryRow();
            #endregion
        }

        #region - Type 합계 표시 |
        /// <summary>
        /// Grid Footer 추가 Panel, PCS 합계 표시
        /// </summary>
        private void InitTypeSummaryRow()
        {
            grdType.View.Columns["SHAPE_SUMTOTAL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdType.View.Columns["SHAPE_SUMTOTAL"].SummaryItem.DisplayFormat = " ";
            grdType.View.Columns["LOTCNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdType.View.Columns["LOTCNT"].SummaryItem.DisplayFormat = " ";
            grdType.View.Columns["SHAPE_TOTAL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            grdType.View.Columns["SHAPE_TOTAL"].SummaryItem.DisplayFormat = " ";
            grdType.View.Columns["SHAPE_SS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            grdType.View.Columns["SHAPE_SS"].SummaryItem.DisplayFormat = " ";
            grdType.View.Columns["SHAPE_DS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            grdType.View.Columns["SHAPE_DS"].SummaryItem.DisplayFormat = " ";
            grdType.View.Columns["SHAPE_MT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            grdType.View.Columns["SHAPE_MT"].SummaryItem.DisplayFormat = " ";
            grdType.View.Columns["SHAPE_RF"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            grdType.View.Columns["SHAPE_RF"].SummaryItem.DisplayFormat = " ";

            grdType.View.Columns["DS_SUMTOTAL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdType.View.Columns["DS_SUMTOTAL"].SummaryItem.DisplayFormat = " ";
            grdType.View.Columns["DS_LOTCNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdType.View.Columns["DS_LOTCNT"].SummaryItem.DisplayFormat = " ";
            grdType.View.Columns["DS_TOTAL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            grdType.View.Columns["DS_TOTAL"].SummaryItem.DisplayFormat = " ";
            grdType.View.Columns["DS_SS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            grdType.View.Columns["DS_SS"].SummaryItem.DisplayFormat = " ";
            grdType.View.Columns["DS_DS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            grdType.View.Columns["DS_DS"].SummaryItem.DisplayFormat = " ";

            grdType.View.Columns["DS_MT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            grdType.View.Columns["DS_MT"].SummaryItem.DisplayFormat = " ";
            grdType.View.Columns["DS_RF"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            grdType.View.Columns["DS_RF"].SummaryItem.DisplayFormat = " ";

            grdType.View.Columns["FS_SUMTOTAL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdType.View.Columns["FS_LOTCNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdType.View.Columns["FS_TOTAL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            grdType.View.Columns["FS_TOTAL"].SummaryItem.DisplayFormat = " ";
            grdType.View.Columns["FS_SS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            grdType.View.Columns["FS_SS"].SummaryItem.DisplayFormat = " ";
            grdType.View.Columns["FS_DS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            grdType.View.Columns["FS_DS"].SummaryItem.DisplayFormat = " ";
            grdType.View.Columns["FS_MT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            grdType.View.Columns["FS_MT"].SummaryItem.DisplayFormat = " ";
            grdType.View.Columns["FS_RF"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            grdType.View.Columns["FS_RF"].SummaryItem.DisplayFormat = " ";

            grdType.View.OptionsView.ShowFooter = true;
            grdType.ShowStatusBar = false;
        }
        #endregion

        #region - 품목 합계 표시 |
        /// <summary>
        /// Grid Footer 추가 Panel, PCS 합계 표시
        /// </summary>
        private void InitProductSummaryRow()
        {


            grdProduct.View.Columns["TOTALLEADTIME"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            grdProduct.View.Columns["TOTALLEADTIME"].SummaryItem.DisplayFormat = " ";
            grdProduct.View.Columns["DOMESTIC_LT_NORMAL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            grdProduct.View.Columns["DOMESTIC_LT_NORMAL"].SummaryItem.DisplayFormat = " ";
            grdProduct.View.Columns["DOMESTIC_LT_NOT_NORMAL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            grdProduct.View.Columns["DOMESTIC_LT_NOT_NORMAL"].SummaryItem.DisplayFormat = " ";
            grdProduct.View.Columns["FOREIGN_LT_NOT_NORMAL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            grdProduct.View.Columns["FOREIGN_LT_NOT_NORMAL"].SummaryItem.DisplayFormat = " ";
            grdProduct.View.Columns["FOREIGN_LT_NORMAL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            grdProduct.View.Columns["FOREIGN_LT_NORMAL"].SummaryItem.DisplayFormat = " ";
            grdProduct.View.Columns["LOTCNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdProduct.View.Columns["LOTCNT"].SummaryItem.DisplayFormat = " ";


            grdProduct.View.OptionsView.ShowFooter = true;
            grdProduct.ShowStatusBar = true;
        }
        #endregion

        #region - LOT 합계 표시 |
        /// <summary>
        /// Grid Footer 추가 Panel, PCS 합계 표시
        /// </summary>
        private void InitLotSummaryRow()
        {
            grdLotList.View.Columns["TOTALLEADTIME"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            grdLotList.View.Columns["TOTALLEADTIME"].SummaryItem.DisplayFormat = " ";
            grdLotList.View.Columns["DOMESTIC_LT_NORMAL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            grdLotList.View.Columns["DOMESTIC_LT_NORMAL"].SummaryItem.DisplayFormat = " ";
            grdLotList.View.Columns["DOMESTIC_LT_NOT_NORMAL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            grdLotList.View.Columns["DOMESTIC_LT_NOT_NORMAL"].SummaryItem.DisplayFormat = " ";
            grdLotList.View.Columns["FOREIGN_LT_NORMAL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            grdLotList.View.Columns["FOREIGN_LT_NORMAL"].SummaryItem.DisplayFormat = " ";
            grdLotList.View.Columns["FOREIGN_LT_NOT_NORMAL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            grdLotList.View.Columns["FOREIGN_LT_NOT_NORMAL"].SummaryItem.DisplayFormat = " ";

            grdLotList.View.OptionsView.ShowFooter = true;
            grdLotList.ShowStatusBar = false;
        }
        #endregion

        #region - 공정 합계 표시 |
        /// <summary>
        /// Grid Footer 추가 Panel, PCS 합계 표시
        /// </summary>
        private void InitSegmentSummaryRow()
        {
            grdProcess.View.Columns["AVG_LEADTIME"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            grdProcess.View.Columns["AVG_LEADTIME"].SummaryItem.DisplayFormat = " ";
            grdProcess.View.Columns["RECEIVELEADTIME"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            grdProcess.View.Columns["RECEIVELEADTIME"].SummaryItem.DisplayFormat = " ";
            grdProcess.View.Columns["WORKSTARTLEADTIME"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            grdProcess.View.Columns["WORKSTARTLEADTIME"].SummaryItem.DisplayFormat = " ";
            grdProcess.View.Columns["WORKENDLEADTIME"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            grdProcess.View.Columns["WORKENDLEADTIME"].SummaryItem.DisplayFormat = " ";
            grdProcess.View.Columns["SENDLEADTIME"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            grdProcess.View.Columns["SENDLEADTIME"].SummaryItem.DisplayFormat = " ";
            grdProcess.View.Columns["LEADTIME"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdProcess.View.Columns["LEADTIME"].SummaryItem.DisplayFormat = " ";
            grdProcess.View.Columns["LOTCNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdProcess.View.Columns["LOTCNT"].SummaryItem.DisplayFormat = " ";
            grdProcess.View.Columns["INPUTQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdProcess.View.Columns["INPUTQTY"].SummaryItem.DisplayFormat = " ";
            grdProcess.View.Columns["LEADTIME_NORMAL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdProcess.View.Columns["LEADTIME_NORMAL"].SummaryItem.DisplayFormat = " ";
            grdProcess.View.Columns["LEADTIME_NOT_NORMAL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdProcess.View.Columns["LEADTIME_NOT_NORMAL"].SummaryItem.DisplayFormat = " ";

            grdProcess.View.OptionsView.ShowFooter = true;
            grdProcess.ShowStatusBar = false;
        }
        #endregion

        #region - 작업장 합계 표시 |
        /// <summary>
        /// Grid Footer 추가 Panel, PCS 합계 표시
        /// </summary>
        private void InitAreaSummaryRow()
        {
            grdArea.View.Columns["AVG_LEADTIME"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            grdArea.View.Columns["AVG_LEADTIME"].SummaryItem.DisplayFormat = " ";
            grdArea.View.Columns["RECEIVELEADTIME"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            grdArea.View.Columns["RECEIVELEADTIME"].SummaryItem.DisplayFormat = " ";
            grdArea.View.Columns["WORKSTARTLEADTIME"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            grdArea.View.Columns["WORKSTARTLEADTIME"].SummaryItem.DisplayFormat = " ";
            grdArea.View.Columns["WORKENDLEADTIME"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            grdArea.View.Columns["WORKENDLEADTIME"].SummaryItem.DisplayFormat = " ";
            grdArea.View.Columns["SENDLEADTIME"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            grdArea.View.Columns["SENDLEADTIME"].SummaryItem.DisplayFormat = " ";
            grdArea.View.Columns["LEADTIME"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdArea.View.Columns["LEADTIME"].SummaryItem.DisplayFormat = " ";
            grdArea.View.Columns["LOTCNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdArea.View.Columns["LOTCNT"].SummaryItem.DisplayFormat = " ";
            grdArea.View.Columns["INPUTQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdArea.View.Columns["INPUTQTY"].SummaryItem.DisplayFormat = " ";
            grdArea.View.Columns["LEADTIME_NORMAL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdArea.View.Columns["LEADTIME_NORMAL"].SummaryItem.DisplayFormat = " ";
            grdArea.View.Columns["LEADTIME_NOT_NORMAL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdArea.View.Columns["LEADTIME_NOT_NORMAL"].SummaryItem.DisplayFormat = " ";

            grdArea.View.OptionsView.ShowFooter = true;
            grdArea.ShowStatusBar = false;
        }
        #endregion

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


            grdProduct.View.CustomDrawFooterCell += GrdProductListView_CustomDrawFooterCell;
            grdProduct.View.RowCellStyle += View_RowCellStyle;
            grdProcess.View.RowCellStyle += View_RowCellStyle;
            grdArea.View.RowCellStyle += View_RowCellStyle;
            grdLotList.View.CustomDrawFooterCell += GrdLotListView_CustomDrawFooterCell;
            grdProcess.View.CustomDrawFooterCell += GrdProcessView_CustomDrawFooterCell;
            grdArea.View.CustomDrawFooterCell += GrdAreaView_CustomDrawFooterCell;
            grdType.View.CustomDrawFooterCell += GrdTypeView_CustomDrawFooterCell;

            grdType.View.DoubleClick += View_DoubleClick;
            grdProduct.View.DoubleClick += View_DoubleClickProduct;
            grdProcess.View.DoubleClick += View_DoubleClickProcess;
            grdArea.View.DoubleClick += View_DoubleClickArea;
            grdlayer.View.DoubleClick += View_DoubleClick1;
            grdlayer.View.RowCellClick += View_RowCellClick;
            chalayer.ObjectSelected += Chalayer_ObjectSelected;
        }

        private void Chalayer_ObjectSelected(object sender, HotTrackEventArgs e)
        {
            SeriesPoint sp = e.AdditionalObject as SeriesPoint;
            if (sp == null)
            {
                return;
            }
            Dictionary<string, object> param = Conditions.GetValues(); ;
            string processsegment = sp.Argument;
            LotLeadTimePopup pop = new LotLeadTimePopup(processsegment, field, param);
            pop.ShowDialog(this);



        }




            private void View_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {


             field = e.Column.FieldName;

        }

        
        private void View_DoubleClick1(object sender, EventArgs e)
        {
            DataRow dr = grdlayer.View.GetFocusedDataRow();
            
            var values = Conditions.GetValues();
            GridView view = sender as GridView;
            if (dr.Table.Columns.Contains(field) && !string.IsNullOrEmpty(Format.GetString(dr[field])))
            {
                values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                values.Add("LAYER", dr["구분"]);
                values.Add("MONTH", field);


                DataTable p1table = SqlExecuter.Query("GetLayerLeadTime", "10002", values);

                this.chalayer.ClearSeries();
                if (p1table.Rows.Count == 0)
                {
                    this.ShowMessage("NoSelectCondData");
                }
                else
                {
                    /*


    */
                    this.chalayer.AddBarSeries(Language.Get("PROCESSSEGMENT"), p1table)
                       .SetX_DataMember(DevExpress.XtraCharts.ScaleType.Auto, "PROCESSSEGMENTNAME")
                       .SetY_DataMember(DevExpress.XtraCharts.ScaleType.Numerical, "TIME")
                       .SetSeriesColor(true, "Blue")
                       .SetLegendTextPattern("{A}");


                    this.chalayer.PopulateSeries();

                    XYDiagram diagram = chalayer.Diagram as XYDiagram;
                    diagram.AxisY.Label.TextPattern = "{V:#,##0}";

                    this.chalayer.SetVisibleOptions(true, true, true).SetAxisInteger(true);
                    this.chalayer.CrosshairOptions.ShowCrosshairLabels = true;

                    DataTable dt = chalayer.DataSource as DataTable;


                }
            }
        }


            #region ▶ ComboBox Event |

            #endregion

            #region ▶ Grid Event |

            #region - Type Grid Footer Sum Event |
            /// <summary>
            /// Grid Footer Sum Event
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void GrdTypeView_CustomDrawFooterCell(object sender, DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventArgs e)
        {
            DataTable dt = grdType.DataSource as DataTable;

            if (dt == null) return;

            if (dt.Rows.Count > 0)
            {
                DevExpress.XtraGrid.GridSummaryItem avg = e.Info.SummaryItem;
                // Obtain the total summary's value. 
                double summaryValue = Convert.ToDouble(avg.SummaryValue);

                e.Info.DisplayText = string.Format("{0:#,0.00}", summaryValue);

                if (e.Info.ColumnInfo.Column.FieldName.Equals("SHAPE_TOTAL"))
                {
                    if (grdType.View.Columns["SHAPE_SUMTOTAL"].SummaryItem.SummaryValue == null) return;

                    double sumlt = Format.GetDouble(grdType.View.Columns["SHAPE_SUMTOTAL"].SummaryItem.SummaryValue.ToString(), 0);
                    double lotcnt = Format.GetDouble(grdType.View.Columns["LOTCNT"].SummaryItem.SummaryValue.ToString(), 0);

                    e.Info.DisplayText = string.Format("{0:#,0.00}", sumlt / lotcnt);
                }
                else if (e.Info.ColumnInfo.Column.FieldName.Equals("DS_TOTAL"))
                {
                    if (grdType.View.Columns["DS_SUMTOTAL"].SummaryItem.SummaryValue == null) return;

                    double sumlt = Format.GetDouble(grdType.View.Columns["DS_SUMTOTAL"].SummaryItem.SummaryValue.ToString(), 0);
                    double lotcnt = Format.GetDouble(grdType.View.Columns["DS_LOTCNT"].SummaryItem.SummaryValue.ToString(), 0);

                    e.Info.DisplayText = string.Format("{0:#,0.00}", sumlt / lotcnt);
                }
                else if (e.Info.ColumnInfo.Column.FieldName.Equals("FS_TOTAL"))
                {
                    if (grdType.View.Columns["FS_SUMTOTAL"].SummaryItem.SummaryValue == null) return;

                    double sumlt = Format.GetDouble(grdType.View.Columns["FS_SUMTOTAL"].SummaryItem.SummaryValue.ToString(), 0);
                    double lotcnt = Format.GetDouble(grdType.View.Columns["FS_LOTCNT"].SummaryItem.SummaryValue.ToString(), 0);

                    e.Info.DisplayText = string.Format("{0:#,0.00}", sumlt / lotcnt);
                }
            }
            else
            {
                grdType.View.Columns["SHAPE_SUMTOTAL"].SummaryItem.DisplayFormat = " ";
                grdType.View.Columns["LOTCNT"].SummaryItem.DisplayFormat = " ";
                grdType.View.Columns["SHAPE_TOTAL"].SummaryItem.DisplayFormat = " ";
                grdType.View.Columns["SHAPE_SS"].SummaryItem.DisplayFormat = " ";
                grdType.View.Columns["SHAPE_DS"].SummaryItem.DisplayFormat = " ";
                grdType.View.Columns["SHAPE_MT"].SummaryItem.DisplayFormat = " ";
                grdType.View.Columns["SHAPE_RF"].SummaryItem.DisplayFormat = " ";
                grdType.View.Columns["DS_SUMTOTAL"].SummaryItem.DisplayFormat = " ";
                grdType.View.Columns["DS_LOTCNT"].SummaryItem.DisplayFormat = " ";
                grdType.View.Columns["DS_TOTAL"].SummaryItem.DisplayFormat = " ";
                grdType.View.Columns["DS_SS"].SummaryItem.DisplayFormat = " ";
                grdType.View.Columns["DS_DS"].SummaryItem.DisplayFormat = " ";
                grdType.View.Columns["DS_MT"].SummaryItem.DisplayFormat = " ";
                grdType.View.Columns["DS_RF"].SummaryItem.DisplayFormat = " ";
                grdType.View.Columns["FS_SUMTOTAL"].SummaryItem.DisplayFormat = " ";
                grdType.View.Columns["FS_LOTCNT"].SummaryItem.DisplayFormat = " "; 
                grdType.View.Columns["FS_TOTAL"].SummaryItem.DisplayFormat = " ";
                grdType.View.Columns["FS_SS"].SummaryItem.DisplayFormat = " ";
                grdType.View.Columns["FS_DS"].SummaryItem.DisplayFormat = " ";
                grdType.View.Columns["FS_MT"].SummaryItem.DisplayFormat = " ";
                grdType.View.Columns["FS_RF"].SummaryItem.DisplayFormat = " ";
            }
        }
        #endregion

        #region - Product Grid Footer Sum Event |
        /// <summary>
        /// Grid Footer Sum Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdProductListView_CustomDrawFooterCell(object sender, DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventArgs e)
        {
            DataTable dt = grdProduct.DataSource as DataTable;

            if (dt == null) return;
     
            if(dt.Rows.Count>1)
            {
                DevExpress.XtraGrid.GridSummaryItem avg = e.Info.SummaryItem;
                // Obtain the total summary's value. 
                double summaryValue = Convert.ToDouble(avg.SummaryValue);

                e.Info.DisplayText = string.Format("{0:#,0.00}", summaryValue);
            }
            else
            {
                grdProduct.View.Columns["TOTALLEADTIME"].SummaryItem.DisplayFormat = " ";
                grdProduct.View.Columns["DOMESTIC_LT_NORMAL"].SummaryItem.DisplayFormat = " ";
                grdProduct.View.Columns["DOMESTIC_LT_NOT_NORMAL"].SummaryItem.DisplayFormat = " ";
                grdProduct.View.Columns["FOREIGN_LT_NOT_NORMAL"].SummaryItem.DisplayFormat = " ";
                grdProduct.View.Columns["FOREIGN_LT_NORMAL"].SummaryItem.DisplayFormat = " ";

                grdProduct.View.Columns["LOTCNT"].SummaryItem.DisplayFormat = " ";
            }
                
  

            
        }
        #endregion

        #region - LotList Grid Footer Sum Event |
        /// <summary>
        /// Grid Footer Sum Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdLotListView_CustomDrawFooterCell(object sender, DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventArgs e)
        {
            DataTable dt = grdLotList.DataSource as DataTable;

            if (dt == null) return;

            if (dt.Rows.Count > 0)
            {
                DevExpress.XtraGrid.GridSummaryItem avg = e.Info.SummaryItem;
                // Obtain the total summary's value. 
                double summaryValue = Convert.ToDouble(avg.SummaryValue);

                e.Info.DisplayText = string.Format("{0:#,0.00}", summaryValue);
            }
            else
            {
                grdLotList.View.Columns["TOTALLEADTIME"].SummaryItem.DisplayFormat = " ";
                grdLotList.View.Columns["DOMESTIC_LT_NORMAL"].SummaryItem.DisplayFormat = " ";
                grdLotList.View.Columns["DOMESTIC_LT_NOT_NORMAL"].SummaryItem.DisplayFormat = " ";
                grdLotList.View.Columns["FOREIGN_LT_NORMAL"].SummaryItem.DisplayFormat = " ";
                grdLotList.View.Columns["FOREIGN_LT_NOT_NORMAL"].SummaryItem.DisplayFormat = " ";
            }
        }
        #endregion

        #region - Process Grid Footer Sum Event |
        /// <summary>
        /// Grid Footer Sum Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdProcessView_CustomDrawFooterCell(object sender, DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventArgs e)
        {
            DataTable dt = grdProcess.DataSource as DataTable;

            if (dt == null) return;

            if (dt.Rows.Count > 0)
            {
                DevExpress.XtraGrid.GridSummaryItem avg = e.Info.SummaryItem;
                // Obtain the total summary's value. 
                double summaryValue = Convert.ToDouble(avg.SummaryValue);

                e.Info.DisplayText = string.Format("{0:#,0.00}", summaryValue);

                if(e.Info.ColumnInfo.Column.FieldName.Equals("AVG_LEADTIME"))
                {
                    if (grdProcess.View.Columns["LEADTIME"].SummaryItem.SummaryValue == null) return;

                    double sumlt = Format.GetDouble(grdProcess.View.Columns["LEADTIME"].SummaryItem.SummaryValue.ToString(), 0);
                    double lotcnt = Format.GetDouble(grdProcess.View.Columns["LOTCNT"].SummaryItem.SummaryValue.ToString(), 0);

                    e.Info.DisplayText = string.Format("{0:#,0.00}", sumlt / lotcnt);
                }
            }
            else
            {
                grdProcess.View.Columns["AVG_LEADTIME"].SummaryItem.DisplayFormat = " ";;
                grdProcess.View.Columns["RECEIVELEADTIME"].SummaryItem.DisplayFormat = " ";
                grdProcess.View.Columns["WORKSTARTLEADTIME"].SummaryItem.DisplayFormat = " ";
                grdProcess.View.Columns["WORKENDLEADTIME"].SummaryItem.DisplayFormat = " ";
                grdProcess.View.Columns["SENDLEADTIME"].SummaryItem.DisplayFormat = " ";
                grdProcess.View.Columns["LEADTIME"].SummaryItem.DisplayFormat = " ";
                grdProcess.View.Columns["LOTCNT"].SummaryItem.DisplayFormat = " ";
                grdProcess.View.Columns["INPUTQTY"].SummaryItem.DisplayFormat = " ";
            }
        }
        #endregion

        #region - Area Grid Footer Sum Event |
        /// <summary>
        /// Grid Footer Sum Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdAreaView_CustomDrawFooterCell(object sender, DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventArgs e)
        {
            DataTable dt = grdArea.DataSource as DataTable;

            if (dt == null) return;

            if (dt.Rows.Count > 0)
            {
                DevExpress.XtraGrid.GridSummaryItem avg = e.Info.SummaryItem;
                // Obtain the total summary's value. 
                double summaryValue = Convert.ToDouble(avg.SummaryValue);

                e.Info.DisplayText = string.Format("{0:#,0.00}", summaryValue);

                if (e.Info.ColumnInfo.Column.FieldName.Equals("AVG_LEADTIME"))
                {
                    if (grdArea.View.Columns["LEADTIME"].SummaryItem.SummaryValue == null) return;

                    double sumlt = Format.GetDouble(grdArea.View.Columns["LEADTIME"].SummaryItem.SummaryValue.ToString(), 0);
                    double lotcnt = Format.GetDouble(grdArea.View.Columns["LOTCNT"].SummaryItem.SummaryValue.ToString(), 0);

                    e.Info.DisplayText = string.Format("{0:#,0.00}", sumlt / lotcnt);
                }
            }
            else
            {
                grdArea.View.Columns["AVG_LEADTIME"].SummaryItem.DisplayFormat = " ";
                grdArea.View.Columns["RECEIVELEADTIME"].SummaryItem.DisplayFormat = " ";
                grdArea.View.Columns["WORKSTARTLEADTIME"].SummaryItem.DisplayFormat = " ";
                grdArea.View.Columns["WORKENDLEADTIME"].SummaryItem.DisplayFormat = " ";
                grdArea.View.Columns["SENDLEADTIME"].SummaryItem.DisplayFormat = " ";
                grdArea.View.Columns["LEADTIME"].SummaryItem.DisplayFormat = " ";
                grdArea.View.Columns["LOTCNT"].SummaryItem.DisplayFormat = " ";
                grdArea.View.Columns["INPUTQTY"].SummaryItem.DisplayFormat = " ";
            }
        }
        #endregion

        private void View_DoubleClickProduct(object sender, EventArgs e)
        {
            DXMouseEventArgs ea = e as DXMouseEventArgs;

            BandedGridHitInfo hitInfo = grdProduct.View.CalcHitInfo(ea.Location);

            string ProductShape = Format.GetFullTrimString(grdProduct.View.GetRowCellValue(hitInfo.RowHandle, "PRODUCTSHAPE"));
            string Productdefid = Format.GetFullTrimString(grdProduct.View.GetRowCellValue(hitInfo.RowHandle, "PRODUCTDEFID"));
            string productdefversion = Format.GetFullTrimString(grdProduct.View.GetRowCellValue(hitInfo.RowHandle, "PRODUCTDEFVERSION"));
            string productdefname = Format.GetFullTrimString(grdProduct.View.GetRowCellValue(hitInfo.RowHandle, "PRODUCTDEFNAME"));

            Conditions.GetControl<SmartComboBox>("P_PRODSHAPETYPE").EditValue = ProductShape;
            Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").SetValue( Productdefid);
            Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").Text = productdefname;
            Conditions.GetControl<SmartComboBox>("P_PRODUCTDEFVERSION").EditValue = productdefversion;


            //p_productdefversion

            if (hitInfo.Column.FieldName.Equals("LOTCNT"))
            {
                tabMain.SelectedTabPage = tpgLot;
            }
            else
            {
                tabMain.SelectedTabPage = tpgProcess;
            }

        }
        private void View_DoubleClickProcess(object sender, EventArgs e)
        {
            DXMouseEventArgs ea = e as DXMouseEventArgs;

            BandedGridHitInfo hitInfo = grdProcess.View.CalcHitInfo(ea.Location);

            string ProductShape = Format.GetFullTrimString(grdProcess.View.GetRowCellValue(hitInfo.RowHandle, "PRODUCTSHAPE"));
            string Productdefid = Format.GetFullTrimString(grdProcess.View.GetRowCellValue(hitInfo.RowHandle, "PRODUCTDEFID"));
            string productdefversion = Format.GetFullTrimString(grdProcess.View.GetRowCellValue(hitInfo.RowHandle, "PRODUCTDEFVERSION"));
            string productdefname = Format.GetFullTrimString(grdProcess.View.GetRowCellValue(hitInfo.RowHandle, "PRODUCTDEFNAME"));
            string processSegmentid = Format.GetFullTrimString(grdProcess.View.GetRowCellValue(hitInfo.RowHandle, "PROCESSSEGMENTID"));
            string processSegmentname = Format.GetFullTrimString(grdProcess.View.GetRowCellValue(hitInfo.RowHandle, "PROCESSSEGMENTNAME"));

            Conditions.GetControl<SmartComboBox>("P_PRODSHAPETYPE").EditValue = ProductShape;
            Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").SetValue(Productdefid);
            Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").Text = productdefname;
            Conditions.GetControl<SmartComboBox>("P_PRODUCTDEFVERSION").EditValue = productdefversion;

            Conditions.GetControl<SmartSelectPopupEdit>("FROM_PROCESSSEGMENTID").SetValue(processSegmentid);
            Conditions.GetControl<SmartSelectPopupEdit>("FROM_PROCESSSEGMENTID").Text = processSegmentname;

            Conditions.GetControl<SmartSelectPopupEdit>("TO_PROCESSSEGMENTID").SetValue(processSegmentid);
            Conditions.GetControl<SmartSelectPopupEdit>("TO_PROCESSSEGMENTID").Text = processSegmentname;

            if (hitInfo.Column.FieldName.Equals("LOTCNT"))
            {
                tabMain.SelectedTabPage = tpgLot;
            }
            else
            {
                tabMain.SelectedTabPage = tpgArea;
            }

        }
        private void View_DoubleClickArea(object sender, EventArgs e)
        {
            DXMouseEventArgs ea = e as DXMouseEventArgs;

            BandedGridHitInfo hitInfo = grdArea.View.CalcHitInfo(ea.Location);

            string ProductShape = Format.GetFullTrimString(grdArea.View.GetRowCellValue(hitInfo.RowHandle, "PRODUCTSHAPE"));
            string Productdefid = Format.GetFullTrimString(grdArea.View.GetRowCellValue(hitInfo.RowHandle, "PRODUCTDEFID"));
            string productdefversion = Format.GetFullTrimString(grdArea.View.GetRowCellValue(hitInfo.RowHandle, "PRODUCTDEFVERSION"));
            string productdefname = Format.GetFullTrimString(grdArea.View.GetRowCellValue(hitInfo.RowHandle, "PRODUCTDEFNAME"));
            string processSegmentid = Format.GetFullTrimString(grdArea.View.GetRowCellValue(hitInfo.RowHandle, "PROCESSSEGMENTID"));
            string processSegmentname = Format.GetFullTrimString(grdArea.View.GetRowCellValue(hitInfo.RowHandle, "PROCESSSEGMENTNAME"));

            Conditions.GetControl<SmartComboBox>("P_PRODSHAPETYPE").EditValue = ProductShape;
            Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").SetValue(Productdefid);
            Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").Text = productdefname;
            Conditions.GetControl<SmartComboBox>("P_PRODUCTDEFVERSION").EditValue = productdefversion;

            Conditions.GetControl<SmartSelectPopupEdit>("FROM_PROCESSSEGMENTID").SetValue(processSegmentid);
            Conditions.GetControl<SmartSelectPopupEdit>("FROM_PROCESSSEGMENTID").Text = processSegmentname;

            Conditions.GetControl<SmartSelectPopupEdit>("TO_PROCESSSEGMENTID").SetValue(processSegmentid);
            Conditions.GetControl<SmartSelectPopupEdit>("TO_PROCESSSEGMENTID").Text = processSegmentname;

            if (hitInfo.Column.FieldName.Equals("LOTCNT"))
            {
                tabMain.SelectedTabPage = tpgLot;
            }

        }
        //grdProcess.View.DoubleClick += View_DoubleClickArea;
        private void View_DoubleClick(object sender, EventArgs e)
        {
            DXMouseEventArgs ea = e as DXMouseEventArgs;

            BandedGridHitInfo hitInfo = grdType.View.CalcHitInfo(ea.Location);

            string Date = string.Empty;
            string ProductShape= "*";

            Date = Format.GetTrimString(grdType.View.GetRowCellValue(hitInfo.RowHandle, "DATE"));

            DateTime origineDate = DateTime.Parse(Conditions.GetControl<SmartPeriodEdit>("P_PERIOD").datePeriodFr.EditValue.ToString());
            string hourMin = origineDate.ToString("hh:ss");

            if (!hitInfo.Column.FieldName.Equals("DATE"))
            {
                ProductShape = hitInfo.Column.Tag.ToString();
            }

            DateTime frDate = DateTime.Parse(Date);

            Conditions.GetControl<SmartPeriodEdit>("P_PERIOD").datePeriodFr.EditValue = Date + " " + hourMin;
            Conditions.GetControl<SmartPeriodEdit>("P_PERIOD").datePeriodTo.EditValue = frDate.AddDays(1).ToString("yyyy-MM-dd") + " " + hourMin;

            //P_PRODSHAPETYPE
            Conditions.GetControl<SmartComboBox>("P_PRODSHAPETYPE").EditValue = ProductShape;
            //    hitInfo = grdType.View.CalcHitInfo(e.);

            tabMain.SelectedTabPage = tpgProduct;
        }

        #endregion
        private void View_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if(e.Column.FieldName.Equals("LOTCNT"))
            {
                e.Appearance.ForeColor = Color.Blue;
                Font ft = new  Font("Malgun Gothic", 9, FontStyle.Underline | FontStyle.Bold);
                e.Appearance.Font = ft;
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
        }

        #endregion

        #region ◆ 검색 |

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();


            switch (this.tabMain.SelectedTabPageIndex)
            {
                case 0: // Type
                    getLeadTimeType();
                    break;
                case 1: // 품목
                    getLeatTimeProduct();
                    break;
                case 2: // Lot
                    getLeatTimeLot();
                    break;
                case 3: // 공정
                    getLeatTimeSegment();
                    break;
                case 4: // 작업장
                    getLeatTimeArea();
                    break;
                case 5: // 월별 LT
                    SearchMonthChart();
                    break;
                case 6: // 월별 LT  사유별
                    SearchReasonChart();
                    break;
                case 7: // 층별 LT
                    SearchLayerChart();
                    break;
                default:
                    break;
            }
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

        // TODO : 화면에서 사용할 내부 함수 추가

        #region ▶ Control Data 초기화 |
        /// <summary>
        /// Control Data 초기화
        /// </summary>
        private void SetClearControl()
        {
            // Data 초기화
            
        }
        #endregion

        #region ▶ getLeatTimeType :: LeadTime - type별 |
        /// <summary>
        /// LeadTime :: type별
        /// </summary>
        private void getLeadTimeType()
        {
            var values = Conditions.GetValues();
            //values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("P_PROCESSTATE", values["P_2STEP"]);
            param.Add("P_PERIOD_PERIODFR", values["P_PERIOD_PERIODFR"]);
            param.Add("P_PERIOD_PERIODTO", values["P_PERIOD_PERIODTO"]);
            param.Add("P_PRODUCTDEFID", values["P_PRODUCTDEFID"] == null ? "" : values["P_PRODUCTDEFID"].ToString());
            param.Add("P_PRODUCTDEFVERSION", values["P_PRODUCTDEFVERSION"] == null ? "" : values["P_PRODUCTDEFVERSION"].ToString());
            param.Add("P_LOTID", values["P_LOTID"] == null ? "" : values["P_LOTID"].ToString());
            param.Add("P_CUSTOMER", values["P_CUSTOMER"] == null ? "" : values["P_CUSTOMER"].ToString());
            param.Add("P_AREAID", values["P_AREAID"] == null ? "" : values["P_AREAID"].ToString());
            param.Add("P_PRODSHAPETYPE", values["P_PRODSHAPETYPE"] == null ? "" : values["P_PRODSHAPETYPE"].ToString());
            param.Add("P_LAYER", values["P_LAYER"] == null ? "" : values["P_LAYER"].ToString());
            param.Add("P_WORKTYPE", values["P_WORKTYPE"] == null ? "" : values["P_WORKTYPE"].ToString());
    
            param.Add("P_PRODUCTIONTYPE", values["P_PRODUCTIONTYPE"] == null ? "" : values["P_PRODUCTIONTYPE"].ToString());

            grdType.DataSource = this.Procedure("usp_wip_leadtimebytype", param);
        }
        #endregion

        #region ▶ getLeatTimeProduct :: 품목별 |
        /// <summary>
        /// LeadTime :: 품목별
        /// </summary>
        private void getLeatTimeProduct()
        {
            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            grdProduct.DataSource = SqlExecuter.Query("SelectLeadTimeByProduct", "10001", values);
        }
        #endregion

        #region ▶ getLeatTimeLot :: Lot별 |
        /// <summary>
        /// LeadTime :: Lot별
        /// </summary>
        private void getLeatTimeLot()
        {
            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            grdLotList.DataSource = SqlExecuter.Query("SelectLeadTimeByLot", "10001", values);
        }
        #endregion

        #region ▶ getLeatTimeSegment :: 공정별 |
        // <summary>
        /// LeadTime :: 공정별
        /// </summary>
        private void getLeatTimeSegment()
        {
            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            if(values["FROM_PROCESSSEGMENTID"] != null && !string.IsNullOrWhiteSpace(values["FROM_PROCESSSEGMENTID"].ToString()))
            {
                if(values["P_PRODUCTDEFID"] == null || string.IsNullOrWhiteSpace(values["P_PRODUCTDEFID"].ToString()))
                {
                    // 품목 정보가 존재하지 않습니다. {0}
                    ShowMessage("NotExistProductDefinition");
                }

                if(values["TO_PROCESSSEGMENTID"] == null || string.IsNullOrWhiteSpace(values["TO_PROCESSSEGMENTID"].ToString()))
                {
                    // To 공정을 선택하여 주십시오.
                    ShowMessage("NoToProcessSegId");
                }
            }

            grdProcess.DataSource = SqlExecuter.Query("SelectLeadTimeBySegment", "10001", values);
        }
        #endregion

        #region ▶ getLeatTimeArea :: 작업장별
        /// <summary>
        /// LeadTime :: 작업장별
        /// </summary>
        private void getLeatTimeArea()
        {
            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            if (values["FROM_PROCESSSEGMENTID"] != null && !string.IsNullOrWhiteSpace(values["FROM_PROCESSSEGMENTID"].ToString()))
            {
                if (values["P_PRODUCTDEFID"] == null || string.IsNullOrWhiteSpace(values["P_PRODUCTDEFID"].ToString()))
                {
                    // 품목 정보가 존재하지 않습니다. {0}
                    ShowMessage("NotExistProductDefinition");
                }

                if (values["TO_PROCESSSEGMENTID"] == null || string.IsNullOrWhiteSpace(values["TO_PROCESSSEGMENTID"].ToString()))
                {
                    // To 공정을 선택하여 주십시오.
                    ShowMessage("NoToProcessSegId");
                }
            }

            grdArea.DataSource = SqlExecuter.Query("SelectLeadTimeByArea", "10001", values);
        }
        #endregion


        #region ▶ 월별 LT 현황

        private void SearchMonthChart()
        {
            this.chamonth.ClearSeries();
            grdmonth.GridButtonItem = GridButtonItem.Export;
            var values = Conditions.GetValues();

            // DateTime 파라미터 -> yyyy-MM-dd 로 변환
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            // 계획
            DataTable p1table =  SqlExecuter.Query("GetP1LeadTime", "10001", values);
            DataTable p2table = SqlExecuter.Query("GetP2LeadTime", "10001", values);
        
            DataTable entiretable = SqlExecuter.Query("GetEntireLeadTime", "10001", values);

            if (p1table.Rows.Count == 0 && p2table.Rows.Count == 0 && entiretable.Rows.Count == 0)
            {
                this.ShowMessage("NoSelectCondData");
            }
            else
            {

                this.chamonth.AddBarSeries(Language.Get("P1"), p1table)
                    .SetX_DataMember(DevExpress.XtraCharts.ScaleType.Auto, "DATE_TIME")
                    .SetY_DataMember(DevExpress.XtraCharts.ScaleType.Numerical, "TOTALLEADTIME")
                    .SetSeriesColor(true, "Blue")
                    .SetLegendTextPattern("{A}");
                this.chamonth.AddBarSeries(Language.Get("P2"), p2table)
                    .SetX_DataMember(DevExpress.XtraCharts.ScaleType.Auto, "DATE_TIME")
                    .SetY_DataMember(DevExpress.XtraCharts.ScaleType.Numerical, "TOTALLEADTIME")
                    .SetSeriesColor(true, "Red")
                    .SetLegendTextPattern("{A}");

                this.chamonth.AddBarSeries(Language.Get("ALL"), entiretable)
                   .SetX_DataMember(DevExpress.XtraCharts.ScaleType.Auto, "DATE_TIME")
                   .SetY_DataMember(DevExpress.XtraCharts.ScaleType.Numerical, "TOTALLEADTIME")
                   .SetSeriesColor(true, "Pupple")
                   .SetLegendTextPattern("{A}");



                this.chamonth.PopulateSeries();
                XYDiagram diagram = chamonth.Diagram as XYDiagram;
                diagram.AxisY.Label.TextPattern = "{V:#,##0}";



                this.chamonth.SetVisibleOptions(true, true, true).SetAxisInteger(true);
                this.chamonth.CrosshairOptions.ShowCrosshairLabels = true;

                

                DataTable dt = new DataTable();

                for (int i = 0; i < p1table.Rows.Count; i++)
                {
                    if (i == 0)
                        {
                        dt.Columns.Add(Language.Get("TYPE").ToString());
                        }
                    dt.Columns.Add(p1table.Rows[i]["DATE_TIME"].ToString());
                  }
                DataRow dr = dt.NewRow();
                for (int j = 0; j < entiretable.Rows.Count; j++)
                {
                  
                    if (j == 0)
                    {
                        dr[j] = Language.Get("ALL").ToString();
                        dr[j+1] = entiretable.Rows[j]["TOTALLEADTIME"].ToString();
                    }
                    else
                    { 
                    dr[j+1] = entiretable.Rows[j]["TOTALLEADTIME"].ToString();
                    }
                   
                }
                dt.Rows.Add(dr);
                dr = dt.NewRow();
                for (int j = 0; j < p1table.Rows.Count; j++)
                {
                    
                    if (j == 0)
                    {
                        dr[j] = "P1";
                        dr[j + 1] = p1table.Rows[j]["TOTALLEADTIME"].ToString();
                    }
                    else
                    {
                        dr[j + 1] = p1table.Rows[j]["TOTALLEADTIME"].ToString();
                    }
                    
                }
                dt.Rows.Add(dr);
                dr = dt.NewRow();
                for (int j = 0; j < p2table.Rows.Count; j++)
                {
                   
                        if (j == 0)
                        {
                            dr[j] = "P2";
                            dr[j + 1] = p2table.Rows[j]["TOTALLEADTIME"].ToString();
                    }
                        else
                        {
                            dr[j + 1] = p2table.Rows[j]["TOTALLEADTIME"].ToString();
                         }
                  
                }
                dt.Rows.Add(dr);
             grdmonth.View.ClearColumns();

                if (values["P_PRODUCTDEFID"] == null)
            {
                values["P_PRODUCTDEFID"] = string.Empty;
            }

                // DateTime 파라미터 -> yyyy-MM-dd 로 변환
                values["P_PERIOD_PERIODFR"] = values["P_PERIOD_PERIODFR"].ToString();

            values["P_PERIOD_PERIODTO"] = values["P_PERIOD_PERIODTO"].ToString();
            values.Add("P_LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Remove("P_PERIOD");
            if (!values.ContainsKey("P_LOTPRODUCTTYPESTATUS"))
            {
                values.Add("P_LOTPRODUCTTYPESTATUS", "");
            }

            
            AddDateColumnsToGrid(DateTime.Parse(values["P_PERIOD_PERIODFR"].ToString())
                , DateTime.Parse(values["P_PERIOD_PERIODTO"].ToString()));
                grdmonth.View.PopulateColumns();

             grdmonth.DataSource = dt;
                
            }
        }
        #endregion


        #region ▶ 월별 사유별 현황

        private void SearchReasonChart()
        {
            this.chareason.ClearSeries();

            grdresaon.GridButtonItem = GridButtonItem.Export;
            var values = Conditions.GetValues();

            // DateTime 파라미터 -> yyyy-MM-dd 로 변환
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            // 계획
            DataTable p1table = SqlExecuter.Query("GetReasonLeadTime", "10001", values);
        
            if (p1table.Rows.Count == 0)
            {
                this.ShowMessage("NoSelectCondData");
            }


                          if (p1table.Rows.Count == 0)
                          {
                              this.ShowMessage("NoSelectCondData");
                          }
                          else
                          {
                /*


*/
                DataTable distinctTable = p1table.DefaultView.ToTable(true, "DICTIONARYNAME");
                DataTable monthTable = p1table.DefaultView.ToTable(true, "DATE_TIME");
                DataView dv2 = new DataView(distinctTable);
                DataView dv = new DataView(p1table);
                DataView dv3 = new DataView(monthTable);
                dv.Sort = "DATE_TIME, DICTIONARYNAME";
                dv2.Sort = "DICTIONARYNAME";
                dv3.Sort = "DATE_TIME";
                p1table = dv.ToTable();
                distinctTable = dv2.ToTable();
                monthTable = dv3.ToTable();

                int count = 0;
                for (int i = 0; i < distinctTable.Rows.Count; i++)
                {
                    count = 0;
                    string[] month_array = new string[distinctTable.Rows.Count];

                    for (int j = 0; j < p1table.Rows.Count; j++)
                    {

                        if (p1table.Rows[j]["DICTIONARYNAME"].Equals(distinctTable.Rows[i]["DICTIONARYNAME"]))
                        {
                            month_array[count] = Format.GetString(p1table.Rows[j]["DATE_TIME"]);
                            count++;
                        }
                    }

                    if (count != monthTable.Rows.Count)
                    {
                        for (int k = 0; k < monthTable.Rows.Count; k++)
                        {

                            if (!month_array.Contains(Format.GetString(monthTable.Rows[k]["DATE_TIME"])))
                            {
                                DataRow dr4 = p1table.NewRow();
                                dr4["DICTIONARYNAME"] = distinctTable.Rows[i]["DICTIONARYNAME"];
                                dr4["DATE_TIME"] = monthTable.Rows[k]["DATE_TIME"];
                                dr4["TIME"] = 0;
                                p1table.Rows.Add(dr4);
                            }
                        }

                    }

                }
                dv = new DataView(p1table);
                dv.Sort = "DICTIONARYNAME, DATE_TIME";
                p1table = dv.ToTable();
                count = 0;
                for (int i = 0; i < distinctTable.Rows.Count; i++)
                {

                    DataTable dt3 = p1table.Clone();
                    DataRow dr2 = distinctTable.Rows[i];

                    for (int j = count; j < p1table.Rows.Count; j++)
                    {
                        if (Format.GetString(dr2["DICTIONARYNAME"]).Equals(Format.GetString(p1table.Rows[j]["DICTIONARYNAME"])))
                        {

                            DataRow dr4 = dt3.NewRow();
                            dr4.ItemArray = p1table.Rows[j].ItemArray;
                            dt3.Rows.Add(dr4);
                            count++;

                        }
                    }
                    this.chareason.AddStackedBarSeries(Format.GetString(dt3.Rows[0]["DICTIONARYNAME"]), dt3)
                        .SetX_DataMember(DevExpress.XtraCharts.ScaleType.Auto, "DATE_TIME")
                        .SetY_DataMember(DevExpress.XtraCharts.ScaleType.Numerical, "TIME")
                        .SetLegendTextPattern("{A}");

                }

                this.chareason.PopulateSeries();
                XYDiagram diagram = chareason.Diagram as XYDiagram;
                diagram.AxisY.Label.TextPattern = "{V:#,##0}";



                this.chareason.SetVisibleOptions(true, true, true).SetAxisInteger(true);
                this.chareason.CrosshairOptions.ShowCrosshairLabels = true;








            DataTable dt = new DataTable();

                              for (int i = 0; i < p1table.Rows.Count; i++)
                              {
                                  if (i == 0)
                                  {
                                      dt.Columns.Add(Language.Get("TYPE").ToString());
                                  }
                                    if (!dt.Columns.Contains(p1table.Rows[i]["DATE_TIME"].ToString()))
                                    {
                                        dt.Columns.Add(p1table.Rows[i]["DATE_TIME"].ToString());
                                    }
                              }

                count = 0;
                for (int i = 0; i < distinctTable.Rows.Count; i++)
                {

                    DataTable dt3 = p1table.Clone();
                    DataRow dr2 = distinctTable.Rows[i];

                    int count2 = 0;
                    DataRow dr = dt.NewRow();
                    for (int j = 0; j < p1table.Rows.Count; j++)
                    {
                   

                        if (Format.GetString(dr2["DICTIONARYNAME"]).Equals(Format.GetString(p1table.Rows[j]["DICTIONARYNAME"])))
                        {


                            if(count2 == 0)
                            {
                                dr["구분"] = p1table.Rows[j]["DICTIONARYNAME"].ToString();

                                string ar = Format.GetString(p1table.Rows[j]["DATE_TIME"]);
                                dr[ar] = p1table.Rows[j]["TIME"].ToString();
                                count2++;
                            }
                            else
                            {
                                string ar = Format.GetString(p1table.Rows[j]["DATE_TIME"]);
                                dr[ar] = p1table.Rows[j]["TIME"].ToString();
                       
                            }
                         


                        }
                

                    }
                    dt.Rows.Add(dr);

                }
        
                                grdresaon.View.ClearColumns();

                              if (values["P_PRODUCTDEFID"] == null)
                              {
                                  values["P_PRODUCTDEFID"] = string.Empty;
                              }

                              // DateTime 파라미터 -> yyyy-MM-dd 로 변환
                              values["P_PERIOD_PERIODFR"] = values["P_PERIOD_PERIODFR"].ToString();

                              values["P_PERIOD_PERIODTO"] = values["P_PERIOD_PERIODTO"].ToString();
                              values.Add("P_LANGUAGETYPE", UserInfo.Current.LanguageType);
                              values.Remove("P_PERIOD");
                              if (!values.ContainsKey("P_LOTPRODUCTTYPESTATUS"))
                              {
                                  values.Add("P_LOTPRODUCTTYPESTATUS", "");
                              }

                              
                              AddDateColumnsToGrid(DateTime.Parse(values["P_PERIOD_PERIODFR"].ToString())
                                  , DateTime.Parse(values["P_PERIOD_PERIODTO"].ToString()));
                              grdresaon.View.PopulateColumns();

                              grdresaon.DataSource = dt;



        
            }



        }




        private void SearchLayerChart()
        {
            this.chareason.ClearSeries();

            grdresaon.GridButtonItem = GridButtonItem.Export;
            var values = Conditions.GetValues();

            // DateTime 파라미터 -> yyyy-MM-dd 로 변환
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            // 계획
            DataTable p1table = SqlExecuter.Query("GetLayerLeadTime", "10001", values);

            if (p1table.Rows.Count == 0)
            {
                this.ShowMessage("NoSelectCondData");
            }
            else
            {
                /*


*/
                DataTable distinctTable = p1table.DefaultView.ToTable(true, "LAYER");
                DataTable monthTable = p1table.DefaultView.ToTable(true, "DATE_TIME");
                DataView dv2 = new DataView(distinctTable);
                DataView dv = new DataView(p1table);
                DataView dv3 = new DataView(monthTable);
                dv.Sort = "DATE_TIME, LAYER";
                dv2.Sort = "LAYER";
                dv3.Sort = "DATE_TIME";
                p1table = dv.ToTable();
                distinctTable = dv2.ToTable();
                monthTable = dv3.ToTable();

                int count = 0;
                for (int i = 0; i < distinctTable.Rows.Count; i++)
                {
                    count = 0;
                    string[] month_array = new string[distinctTable.Rows.Count];

                    for (int j = 0; j < p1table.Rows.Count; j++)
                    {

                        if (p1table.Rows[j]["LAYER"].Equals(distinctTable.Rows[i]["LAYER"]))
                        {
                            month_array[count] = Format.GetString(p1table.Rows[j]["DATE_TIME"]);
                            count++;
                        }
                    }

                    if (count != monthTable.Rows.Count)
                    {
                        for (int k = 0; k < monthTable.Rows.Count; k++)
                        {

                            if (!month_array.Contains(Format.GetString(monthTable.Rows[k]["DATE_TIME"])))
                            {
                                DataRow dr4 = p1table.NewRow();
                                dr4["LAYER"] = distinctTable.Rows[i]["LAYER"];
                                dr4["DATE_TIME"] = monthTable.Rows[k]["DATE_TIME"];
                                dr4["TIME"] = 0;
                                p1table.Rows.Add(dr4);
                            }
                        }

                    }

                }
                dv = new DataView(p1table);
                dv.Sort = "LAYER, DATE_TIME";
                p1table = dv.ToTable();
                count = 0;
                


              

                DataTable dt = new DataTable();

                for (int i = 0; i < p1table.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        dt.Columns.Add(Language.Get("TYPE").ToString());
                    }
                    if (!dt.Columns.Contains(p1table.Rows[i]["DATE_TIME"].ToString()))
                    {
                        dt.Columns.Add(p1table.Rows[i]["DATE_TIME"].ToString());
                    }
                }

                count = 0;
                for (int i = 0; i < distinctTable.Rows.Count; i++)
                {

                    DataTable dt3 = p1table.Clone();
                    DataRow dr2 = distinctTable.Rows[i];

                    int count2 = 0;
                    DataRow dr = dt.NewRow();
                    for (int j = 0; j < p1table.Rows.Count; j++)
                    {


                        if (Format.GetString(dr2["LAYER"]).Equals(Format.GetString(p1table.Rows[j]["LAYER"])))
                        {


                            if (count2 == 0)
                            {
                                dr["구분"] = p1table.Rows[j]["LAYER"].ToString();

                                string ar = Format.GetString(p1table.Rows[j]["DATE_TIME"]);
                                dr[ar] = p1table.Rows[j]["TIME"].ToString();
                                count2++;
                            }
                            else
                            {
                                string ar = Format.GetString(p1table.Rows[j]["DATE_TIME"]);
                                dr[ar] = p1table.Rows[j]["TIME"].ToString();

                            }



                        }


                    }
                    dt.Rows.Add(dr);

                }

                grdlayer.View.ClearColumns();

                if (values["P_PRODUCTDEFID"] == null)
                {
                    values["P_PRODUCTDEFID"] = string.Empty;
                }

                // DateTime 파라미터 -> yyyy-MM-dd 로 변환
                values["P_PERIOD_PERIODFR"] = values["P_PERIOD_PERIODFR"].ToString();

                values["P_PERIOD_PERIODTO"] = values["P_PERIOD_PERIODTO"].ToString();
                values.Add("P_LANGUAGETYPE", UserInfo.Current.LanguageType);
                values.Remove("P_PERIOD");
                if (!values.ContainsKey("P_LOTPRODUCTTYPESTATUS"))
                {
                    values.Add("P_LOTPRODUCTTYPESTATUS", "");
                }


                AddDateColumnsToGrid(DateTime.Parse(values["P_PERIOD_PERIODFR"].ToString())
                    , DateTime.Parse(values["P_PERIOD_PERIODTO"].ToString()));
                grdlayer.View.PopulateColumns();

                grdlayer.DataSource = dt;
                grdlayer.View.SetIsReadOnly();



            }



        }
        #endregion
        private void AddDateColumnsToGrid(DateTime from, DateTime to)
        {

            switch (this.tabMain.SelectedTabPageIndex)
            {
                case 5: // 월별 LT
                    grdmonth.View.AddTextBoxColumn(Language.Get("TYPE"), 80);
                    foreach (DateTime month in EachDay(from, to))
                    {
                        grdmonth.View.AddTextBoxColumn(month.ToString("yyyy-MM"), 80);

                    }
                    break;
                case 6: // 월별 LT  사유별
                    grdresaon.View.AddTextBoxColumn(Language.Get("TYPE"), 80);
                    foreach (DateTime month in EachDay(from, to))
                    {
                        grdresaon.View.AddTextBoxColumn(month.ToString("yyyy-MM"), 80);

                    }
                    break;
                case 7: // 층별 LT  사유별
                    grdlayer.View.AddTextBoxColumn(Language.Get("TYPE"), 80);
                    foreach (DateTime month in EachDay(from, to))
                    {
                        grdlayer.View.AddTextBoxColumn(month.ToString("yyyy-MM"), 80);

                    }
                    break;
                default:
                    break;
            }
       

        }

        // TODO : 화면에서 사용할 내부 함수 추가
        private IEnumerable<DateTime> EachDay(DateTime from, DateTime to)
        {
            for (var month = from.Date; month.Date <= to.Date; month = month.AddMonths(1))
            {
                yield return month;
            }
        }


        #endregion

        private void smartSpliterContainer3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ucChartDetail1_Load(object sender, EventArgs e)
        {

        }
    }
}
