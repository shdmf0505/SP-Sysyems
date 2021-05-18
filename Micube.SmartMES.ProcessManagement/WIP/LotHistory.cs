#region using
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons;
using Micube.Framework.SmartControls.Grid.BandedGrid;
using Micube.Framework.SmartControls.Grid;

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
using System.Reflection;
using System.Threading;

using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraTreeList;
using DevExpress.Utils;
#endregion

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 공정관리 > 공정작업 > Lot History
    /// 업  무  설  명  : Lot History
    /// 생    성    자  : 박정훈
    /// 생    성    일  : 2019-09-08
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class LotHistory : SmartConditionManualBaseForm
    {
        #region ◆ Local Variables |
        private string[] UserAdminGroup = { "jhpark", "sybae", "hykang", "yshwang" };
        private Dictionary<string, object> _parameter;
        #endregion

            #region ◆ 생성자 |
            /// <summary>
            /// 생성자
            /// </summary>
        public LotHistory()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 2019.10.09 배선용
        /// 재공 조회에서 호출시 자동 조회
        /// </summary>
        /// <param name="parameters"></param>
        public override void LoadForm(Dictionary<string, object> parameters)
        {
            base.LoadForm(parameters);

            if (parameters != null)
            {
                //InitializeTreeList();
                _parameter = parameters;

                //Conditions.GetControl<SmartSelectPopupEdit>("LOTID").SetValue(Format.GetString(parameters["LOTID"]));
                //OnSearchAsync();
                //SearchData(Format.GetString(parameters["LOTID"]));
            }
        }
        #endregion

        #region ◆ 컨텐츠 영역 초기화 |
        /// <summary>
        /// Control 설정
        /// </summary>
        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeComboBox();
            InitializeGrid();
            InitializeTreeList();
            InitializeControls();

            InitializeEvent();
        }

        #region ▶ 조회조건 설정 |
        /// <summary>
        /// 검색조건 설정
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // Lot
            CommonFunction.AddConditionLotHistPopup("LOTID", 0.1, true, Conditions);

            Conditions.GetCondition<ConditionItemSelectPopup>("LOTID").SetValidationIsRequired();
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
            #region - LOT 정보 |
            grdLotInfo.ColumnCount = 4;
            grdLotInfo.SetInvisibleFields("PROCESSPATHID", "PROCESSSEGMENTNAME", "PREVPROCESSSEGMENTNAME", "PROCESSSEGMENTID", "PROCESSSEGMENTVERSION", "NEXTPROCESSSEGMENTNAME", "USERSEQUENCE"
                , "DEFECTUNIT", "STEPTYPE", "PROCESSSEGMENTTYPE", "STEPTYPE", "DURABLEDEFID", "PROCESSSTATE", "PANELPERQTY", "MM", "PCSPNL", "PRODUCTTYPE");
            #endregion

            #region - Lot Routing 이력 |
            grdLotRouting.GridButtonItem = GridButtonItem.Export;

            grdLotRouting.View.SetIsReadOnly();
            grdLotRouting.SetIsUseContextMenu(false);

            var ghistLotRoutingCol = grdLotRouting.View.AddGroupColumn("MANUFACTURINGHISTORY");
            ghistLotRoutingCol.AddTextBoxColumn("LOTID", 180).SetTextAlignment(TextAlignment.Center);
            //ghistLotRoutingCol.AddTextBoxColumn("PATHSEQUENCE", 70).SetIsHidden();
            ghistLotRoutingCol.AddTextBoxColumn("PROCESSSEQUENCE", 60).SetTextAlignment(TextAlignment.Center);
            ghistLotRoutingCol.AddTextBoxColumn("WORKTYPE", 70).SetTextAlignment(TextAlignment.Center);
            ghistLotRoutingCol.AddTextBoxColumn("PRODUCTDEFVERSION", 70).SetTextAlignment(TextAlignment.Center);

            ghistLotRoutingCol.AddTextBoxColumn("PROCESSSEGMENTID", 150).SetIsHidden();
            ghistLotRoutingCol.AddTextBoxColumn("PROCESSSEGMENTNAME", 150);
            ghistLotRoutingCol.AddTextBoxColumn("PLANTID", 60).SetTextAlignment(TextAlignment.Center);
            ghistLotRoutingCol.AddTextBoxColumn("AREANAME", 150);
            //ghistRoutingCol.AddTextBoxColumn("UOM", 60).SetTextAlignment(TextAlignment.Center);

            // 작업일자
            var ghistLotDateCol = grdLotRouting.View.AddGroupColumn("WORKDATE");
            ghistLotDateCol.AddTextBoxColumn("RECEIVEDATE", 140).SetTextAlignment(TextAlignment.Center);
            ghistLotDateCol.AddTextBoxColumn("STARTDATE", 140).SetTextAlignment(TextAlignment.Center);
            ghistLotDateCol.AddTextBoxColumn("WORKENDDATE", 140).SetTextAlignment(TextAlignment.Center);
            ghistLotDateCol.AddTextBoxColumn("LOTSENDDATE", 140).SetTextAlignment(TextAlignment.Center);

            // 인수수량정보
            var ghistLotINQTYCol = grdLotRouting.View.AddGroupColumn("INQTY");
            ghistLotINQTYCol.AddSpinEditColumn("RECEIVEPCSQTY", 80);
            ghistLotINQTYCol.AddSpinEditColumn("RECEIVEPANELQTY", 80);

            // 작업 시작수량정보
            var ghistLotSTARTQTYCol = grdLotRouting.View.AddGroupColumn("WIPSTARTQTY");
            ghistLotSTARTQTYCol.AddSpinEditColumn("WORKSTARTPCSQTY", 80);
            ghistLotSTARTQTYCol.AddSpinEditColumn("WORKSTARTPANELQTY", 80);

            // 작업 완료수량정보
            var ghistLotENDQTYCol = grdLotRouting.View.AddGroupColumn("WIPENDQTY");
            ghistLotENDQTYCol.AddSpinEditColumn("WORKENDPCSQTY", 80);
            ghistLotENDQTYCol.AddSpinEditColumn("WORKENDPANELQTY", 80);

            // 인계수량정보
            var ghistLotSENDQTYCol = grdLotRouting.View.AddGroupColumn("WIPSENDQTY");
            ghistLotSENDQTYCol.AddSpinEditColumn("SENDPCSQTY", 80);
            ghistLotSENDQTYCol.AddSpinEditColumn("SENDPANELQTY", 80);

            // 재공수량정보
            var ghistLotWIPCol = grdLotRouting.View.AddGroupColumn("WIPLIST");
            ghistLotWIPCol.AddSpinEditColumn("QTY", 80).SetLabel("PCS");
            ghistLotWIPCol.AddSpinEditColumn("PANELQTY", 80).SetLabel("PNL");

            // LEADTIME
            var ghistLotLTCol = grdLotRouting.View.AddGroupColumn("LEADTIME");
            ghistLotLTCol.AddSpinEditColumn("RECEIVELEADTIME", 80).SetLabel("LTHOUR_WAITFORRECEIVE")
                                .SetDisplayFormat("#,##0.00", MaskTypes.Numeric, true);
            ghistLotLTCol.AddSpinEditColumn("WORKSTARTLEADTIME", 80).SetLabel("LTHOUR_WORKSTART")
                                .SetDisplayFormat("#,##0.00", MaskTypes.Numeric, true);
            ghistLotLTCol.AddSpinEditColumn("WORKENDLEADTIME", 80).SetLabel("LTHOUR_WORKEND")
                                .SetDisplayFormat("#,##0.00", MaskTypes.Numeric, true);
            ghistLotLTCol.AddSpinEditColumn("SENDLEADTIME", 80).SetLabel("LTHOUR_WAITFORSEND")
                                .SetDisplayFormat("#,##0.00", MaskTypes.Numeric, true);
            ghistLotLTCol.AddSpinEditColumn("LEADTIME", 80).SetLabel("LTHOUR_SUM")
                                .SetDisplayFormat("#,##0.00", MaskTypes.Numeric, true);
            ghistLotLTCol.AddSpinEditColumn("LEADTIMESUM", 80).SetLabel("LTDAY_LEADTIME")
                                .SetDisplayFormat("#,##0.00", MaskTypes.Numeric, true);
            ghistLotLTCol.AddSpinEditColumn("CUM_LEADTIME", 80).SetLabel("CUMDAY_LEADTIME")
                        .SetDisplayFormat("#,##0.00", MaskTypes.Numeric, true);

            // DEFECT
            var ghistDefectCol = grdLotRouting.View.AddGroupColumn("DEFECT");
            ghistDefectCol.AddSpinEditColumn("DEFECTQTY", 80);
            ghistDefectCol.AddSpinEditColumn("CUM_DEFECTQTY", 80);

            grdLotRouting.View.PopulateColumns();
            #endregion

            #region - Lot 분할 이력 |
            grdLotSplitHistory.GridButtonItem = GridButtonItem.Export;

            grdLotSplitHistory.View.SetIsReadOnly();
            grdLotSplitHistory.SetIsUseContextMenu(false);

            grdLotSplitHistory.View.AddTextBoxColumn("SPLITTYPE", 80).SetTextAlignment(TextAlignment.Center);
            grdLotSplitHistory.View.AddTextBoxColumn("ROOTLOTID", 180).SetTextAlignment(TextAlignment.Center);
            grdLotSplitHistory.View.AddTextBoxColumn("SOURCELOTID", 180).SetLabel("ORIGINAL").SetTextAlignment(TextAlignment.Center);
            grdLotSplitHistory.View.AddTextBoxColumn("DESTINATIONLOTID", 180).SetLabel("TARGETWIP").SetTextAlignment(TextAlignment.Center);
            grdLotSplitHistory.View.AddTextBoxColumn("PLANTID", 80).SetTextAlignment(TextAlignment.Center);
            grdLotSplitHistory.View.AddTextBoxColumn("PROCESSDEFID", 120).SetTextAlignment(TextAlignment.Center);
            grdLotSplitHistory.View.AddTextBoxColumn("PROCESSDEFVERSION", 80).SetTextAlignment(TextAlignment.Center);
            grdLotSplitHistory.View.AddTextBoxColumn("PROCESSPATHID", 120).SetTextAlignment(TextAlignment.Center).SetLabel("PROCESSPATHID2");
            grdLotSplitHistory.View.AddTextBoxColumn("USERSEQUENCE", 80).SetTextAlignment(TextAlignment.Center);
            grdLotSplitHistory.View.AddTextBoxColumn("PROCESSSEGMENTID", 120).SetTextAlignment(TextAlignment.Center);
            grdLotSplitHistory.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150);
            grdLotSplitHistory.View.AddTextBoxColumn("AREAID", 100).SetTextAlignment(TextAlignment.Center);
            grdLotSplitHistory.View.AddTextBoxColumn("AREANAME", 120);
            grdLotSplitHistory.View.AddTextBoxColumn("QTY", 100).SetLabel("LEFTQTY").SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric);
            grdLotSplitHistory.View.AddTextBoxColumn("CREATEDQTY", 100).SetLabel("SPLITQTY").SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric);
            grdLotSplitHistory.View.AddTextBoxColumn("USERNAME", 180).SetTextAlignment(TextAlignment.Center);
            grdLotSplitHistory.View.AddTextBoxColumn("TXNTIME", 180).SetTextAlignment(TextAlignment.Center);

            grdLotSplitHistory.View.PopulateColumns();
            #endregion

            #region - Lot Raw History
            grdRawHistory.GridButtonItem = GridButtonItem.Export;

            grdRawHistory.View.SetIsReadOnly();
            grdRawHistory.SetIsUseContextMenu(false);

            grdRawHistory.View.AddTextBoxColumn("TXNID", 120);
            grdRawHistory.View.AddTextBoxColumn("TXNTIME", 170).SetTextAlignment(TextAlignment.Center);
            grdRawHistory.View.AddTextBoxColumn("LOTID", 180).SetTextAlignment(TextAlignment.Center);
            grdRawHistory.View.AddTextBoxColumn("ROOTLOTID", 180).SetTextAlignment(TextAlignment.Center);

            grdRawHistory.View.AddTextBoxColumn("PARENTLOTID", 180).SetTextAlignment(TextAlignment.Center);
            grdRawHistory.View.AddTextBoxColumn("LOTSTATE", 80).SetTextAlignment(TextAlignment.Center);
            grdRawHistory.View.AddTextBoxColumn("PROCESSSTATE", 100);
            grdRawHistory.View.AddTextBoxColumn("LOTCREATEDTYPE", 70).SetTextAlignment(TextAlignment.Center);
            grdRawHistory.View.AddTextBoxColumn("PLANTID", 70).SetTextAlignment(TextAlignment.Center);
            grdRawHistory.View.AddTextBoxColumn("PREVAREAID", 70).SetTextAlignment(TextAlignment.Center);
            grdRawHistory.View.AddTextBoxColumn("AREAID", 70).SetTextAlignment(TextAlignment.Center);
            grdRawHistory.View.AddTextBoxColumn("AREANAME", 150);
            grdRawHistory.View.AddTextBoxColumn("RESOURCEID", 80).SetTextAlignment(TextAlignment.Center);
            grdRawHistory.View.AddTextBoxColumn("RESOURCENAME", 150);
            grdRawHistory.View.AddTextBoxColumn("PRODUCTDEFID", 100).SetTextAlignment(TextAlignment.Center);
            grdRawHistory.View.AddTextBoxColumn("PRODUCTDEFNAME", 150);
            grdRawHistory.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetTextAlignment(TextAlignment.Center);
            grdRawHistory.View.AddTextBoxColumn("PROCESSDEFID", 100).SetTextAlignment(TextAlignment.Center);
            grdRawHistory.View.AddTextBoxColumn("PROCESSDEFVERSION", 80).SetTextAlignment(TextAlignment.Center);
            grdRawHistory.View.AddTextBoxColumn("PROCESSPATHSTACK", 160).SetTextAlignment(TextAlignment.Center);
            grdRawHistory.View.AddTextBoxColumn("PREVUSERSEQUENCE", 80).SetTextAlignment(TextAlignment.Center);
            grdRawHistory.View.AddTextBoxColumn("USERSEQUENCE", 80).SetTextAlignment(TextAlignment.Center);
            grdRawHistory.View.AddTextBoxColumn("SUBUSERSEQUENCE", 80).SetTextAlignment(TextAlignment.Center);
            grdRawHistory.View.AddTextBoxColumn("PREVPROCESSSEGMENTID", 100).SetTextAlignment(TextAlignment.Center);
            grdRawHistory.View.AddTextBoxColumn("PROCESSSEGMENTID", 100).SetTextAlignment(TextAlignment.Center);
            grdRawHistory.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150);
            grdRawHistory.View.AddTextBoxColumn("RECEIVEUSER", 100).SetTextAlignment(TextAlignment.Center);
            grdRawHistory.View.AddTextBoxColumn("RECEIVETIME", 170).SetTextAlignment(TextAlignment.Center);
            grdRawHistory.View.AddTextBoxColumn("TRACKINUSER", 100).SetTextAlignment(TextAlignment.Center);
            grdRawHistory.View.AddTextBoxColumn("TRACKINTIME", 150).SetTextAlignment(TextAlignment.Center);
            grdRawHistory.View.AddTextBoxColumn("TRACKOUTUSER", 100).SetTextAlignment(TextAlignment.Center);
            grdRawHistory.View.AddTextBoxColumn("TRACKOUTTIME", 170).SetTextAlignment(TextAlignment.Center);
            grdRawHistory.View.AddTextBoxColumn("SENDUSER", 100).SetTextAlignment(TextAlignment.Center);
            grdRawHistory.View.AddTextBoxColumn("SENDTIME", 170).SetTextAlignment(TextAlignment.Center);
            grdRawHistory.View.AddTextBoxColumn("ISHOLD", 80).SetTextAlignment(TextAlignment.Center);
            grdRawHistory.View.AddTextBoxColumn("ISLOCKING", 80).SetTextAlignment(TextAlignment.Center);
            grdRawHistory.View.AddTextBoxColumn("CREATEDQTY", 100).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric);
            grdRawHistory.View.AddTextBoxColumn("QTY", 100).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric);
            grdRawHistory.View.AddTextBoxColumn("PREVQTY", 100).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric);
            grdRawHistory.View.AddTextBoxColumn("DEFECTQTY", 100).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric);
            grdRawHistory.View.AddTextBoxColumn("WEEK", 80).SetLabel("PRINTWEEK").SetTextAlignment(TextAlignment.Center);
            grdRawHistory.View.AddTextBoxColumn("MKLOTID", 150).SetLabel("5010").SetTextAlignment(TextAlignment.Center);
            grdRawHistory.View.AddTextBoxColumn("PNLROOTLOTID", 180).SetTextAlignment(TextAlignment.Center);

            grdRawHistory.View.PopulateColumns();
            #endregion

            #region - 품목 Spec |
            //grdProductInfo.Height = 550;
            #endregion

            #region - 계측값 |
            grdInspectionMeasure.GridButtonItem = GridButtonItem.Export;

            grdInspectionMeasure.View.SetIsReadOnly();

            grdInspectionMeasure.View.AddTextBoxColumn("LOTID", 190).SetTextAlignment(TextAlignment.Center);
            grdInspectionMeasure.View.AddTextBoxColumn("PRODUCTDEFID", 70).SetIsHidden();
            grdInspectionMeasure.View.AddTextBoxColumn("PRODUCTDEFVERSION", 70).SetIsHidden();
            grdInspectionMeasure.View.AddTextBoxColumn("PLANTID", 70).SetIsHidden();
            grdInspectionMeasure.View.AddTextBoxColumn("FACTORYID", 70).SetIsHidden();
            grdInspectionMeasure.View.AddTextBoxColumn("AREAID", 70).SetIsHidden();
            grdInspectionMeasure.View.AddTextBoxColumn("EQUIPMENTID", 70).SetIsHidden();
            grdInspectionMeasure.View.AddTextBoxColumn("SUBNAME", 70).SetIsHidden();
            grdInspectionMeasure.View.AddTextBoxColumn("LOTTYPE", 70).SetIsHidden();
            grdInspectionMeasure.View.AddTextBoxColumn("PATHSEQUENCE", 70).SetIsHidden();
            grdInspectionMeasure.View.AddTextBoxColumn("USERSEQUENCE", 60).SetTextAlignment(TextAlignment.Center);
            grdInspectionMeasure.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150);
            grdInspectionMeasure.View.AddTextBoxColumn("MEASUREDATETIME", 140).SetTextAlignment(TextAlignment.Center);
            grdInspectionMeasure.View.AddTextBoxColumn("AREANAME", 150).SetTextAlignment(TextAlignment.Center);
            grdInspectionMeasure.View.AddTextBoxColumn("INSPITEMNAME", 250);
            grdInspectionMeasure.View.AddTextBoxColumn("SPEC", 250);
            grdInspectionMeasure.View.AddTextBoxColumn("INSPECTIONRESULT", 80).SetTextAlignment(TextAlignment.Center);
            grdInspectionMeasure.View.AddTextBoxColumn("ACTIONRESULT", 120).SetTextAlignment(TextAlignment.Center);

            grdInspectionMeasure.View.PopulateColumns();
            #endregion

            #region - 원부자재 |
            grdConsumable.GridButtonItem = GridButtonItem.Export;

            grdConsumable.View.SetIsReadOnly();

            grdConsumable.View.AddTextBoxColumn("LOTID", 190).SetTextAlignment(TextAlignment.Center);
            grdConsumable.View.AddTextBoxColumn("PATHSEQUENCE", 70).SetIsHidden();
            grdConsumable.View.AddTextBoxColumn("USERSEQUENCE", 60).SetTextAlignment(TextAlignment.Center);
            grdConsumable.View.AddTextBoxColumn("PROCESSSEGMENTID", 150).SetIsHidden();
            grdConsumable.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150);
            grdConsumable.View.AddTextBoxColumn("WORKENDDATE", 140).SetTextAlignment(TextAlignment.Center);
            grdConsumable.View.AddTextBoxColumn("AREANAME", 150);
            grdConsumable.View.AddTextBoxColumn("CONSUMABLEDEFID", 140).SetTextAlignment(TextAlignment.Center);
            grdConsumable.View.AddTextBoxColumn("CONSUMABLEDEFNAME", 280);
            grdConsumable.View.AddTextBoxColumn("UOM", 60).SetTextAlignment(TextAlignment.Center);
            grdConsumable.View.AddTextBoxColumn("REQUIREMENTQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###.#####}");
            grdConsumable.View.AddTextBoxColumn("CONSUMABLELOTID", 180).SetTextAlignment(TextAlignment.Center);
            grdConsumable.View.AddTextBoxColumn("CONSUMEDQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###.#####}");
            grdConsumable.View.AddTextBoxColumn("TRANSACTIONCODE", 80).SetTextAlignment(TextAlignment.Center);
            grdConsumable.View.AddTextBoxColumn("TXNUNIT", 60).SetLabel("UOM").SetTextAlignment(TextAlignment.Center);
            grdConsumable.View.AddTextBoxColumn("SENDQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###.#####}");

            grdConsumable.View.OptionsView.AllowCellMerge = true; // CellMerge
            grdConsumable.View.PopulateColumns();
            #endregion

            #region - 치공구 |
            grdDurable.GridButtonItem = GridButtonItem.Export;

            grdDurable.View.SetIsReadOnly();

            grdDurable.View.AddTextBoxColumn("LOTID", 190).SetTextAlignment(TextAlignment.Center);
            grdDurable.View.AddTextBoxColumn("PATHSEQUENCE", 70).SetIsHidden();
            grdDurable.View.AddTextBoxColumn("USERSEQUENCE", 60).SetTextAlignment(TextAlignment.Center);
            grdDurable.View.AddTextBoxColumn("PROCESSSEGMENTID", 150).SetIsHidden();
            grdDurable.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150);
            grdDurable.View.AddTextBoxColumn("WORKENDDATE", 140).SetTextAlignment(TextAlignment.Center);
            grdDurable.View.AddTextBoxColumn("AREANAME", 150);
            grdDurable.View.AddTextBoxColumn("DURABLECLASS", 60).SetTextAlignment(TextAlignment.Center);
            grdDurable.View.AddTextBoxColumn("DURABLEDEFVERSION", 100).SetTextAlignment(TextAlignment.Center);
            grdDurable.View.AddTextBoxColumn("DURABLELOTID", 150).SetTextAlignment(TextAlignment.Center);
            grdDurable.View.AddTextBoxColumn("TOTALUSEDCOUNT", 60).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("{0:#,###.##}");
            grdDurable.View.AddTextBoxColumn("USEDLIMIT", 100).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("{0:#,###.##}");

            grdDurable.View.PopulateColumns();
            #endregion

            #region - 설비 |
            grdEquipment.GridButtonItem = GridButtonItem.Export;

            grdEquipment.View.SetIsReadOnly();

            grdEquipment.View.AddTextBoxColumn("LOTID", 180).SetTextAlignment(TextAlignment.Center);
            grdEquipment.View.AddTextBoxColumn("PATHSEQUENCE", 70).SetIsHidden();
            grdEquipment.View.AddTextBoxColumn("USERSEQUENCE", 60).SetTextAlignment(TextAlignment.Center);
            grdEquipment.View.AddTextBoxColumn("STATE", 50).SetTextAlignment(TextAlignment.Center);
            grdEquipment.View.AddTextBoxColumn("PROCESSSEGMENTID", 150).SetIsHidden();
            grdEquipment.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 140);
            grdEquipment.View.AddTextBoxColumn("AREANAME", 150);
            grdEquipment.View.AddTextBoxColumn("EQUIPMENTID", 100).SetTextAlignment(TextAlignment.Center).SetIsHidden();
            grdEquipment.View.AddTextBoxColumn("EQUIPMENTNAME", 190);
            grdEquipment.View.AddTextBoxColumn("WORKER", 60).SetTextAlignment(TextAlignment.Center);
            grdEquipment.View.AddTextBoxColumn("WORKSTARTTIME", 120).SetTextAlignment(TextAlignment.Center);
            grdEquipment.View.AddTextBoxColumn("WORKENDDATE", 120).SetTextAlignment(TextAlignment.Center);

            grdEquipment.View.PopulateColumns();

            grdEquipment.View.OptionsView.AllowCellMerge = true; // CellMerge
            grdEquipment.View.Columns["STATE"].OptionsColumn.AllowMerge = DefaultBoolean.False;
            grdEquipment.View.Columns["PROCESSSEGMENTNAME"].OptionsColumn.AllowMerge = DefaultBoolean.False;
            grdEquipment.View.Columns["AREANAME"].OptionsColumn.AllowMerge = DefaultBoolean.False;

            #endregion

            #region - RECIPE |

            #region ＊ 설비 Recipe 정보 |
            grdRecipe.GridButtonItem = GridButtonItem.Export;

            grdRecipe.View.SetIsReadOnly();

            grdRecipe.View.AddTextBoxColumn("LOTID", 190).SetTextAlignment(TextAlignment.Center);
            grdRecipe.View.AddTextBoxColumn("PATHSEQUENCE", 70).SetIsHidden();
            grdRecipe.View.AddTextBoxColumn("USERSEQUENCE", 60).SetTextAlignment(TextAlignment.Center);
            grdRecipe.View.AddTextBoxColumn("PROCESSSEGMENTID", 150).SetIsHidden();
            grdRecipe.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150);
            grdRecipe.View.AddTextBoxColumn("AREANAME", 150);
            grdRecipe.View.AddTextBoxColumn("EQUIPMENTID", 100).SetTextAlignment(TextAlignment.Center).SetIsHidden();
            grdRecipe.View.AddTextBoxColumn("EQUIPMENTNAME", 190);
            grdRecipe.View.AddTextBoxColumn("RECIPEID", 100).SetTextAlignment(TextAlignment.Center);
            grdRecipe.View.AddTextBoxColumn("RECIPEVERSION", 100).SetIsHidden();
            grdRecipe.View.AddTextBoxColumn("WORKSTARTTIME", 140).SetTextAlignment(TextAlignment.Center);
            grdRecipe.View.AddTextBoxColumn("WORKENDDATE", 140).SetTextAlignment(TextAlignment.Center);

            grdRecipe.View.OptionsView.AllowCellMerge = true; // CellMerge
            grdRecipe.View.PopulateColumns(); 
            #endregion

            #region ＊ Recipe Parameter |
            grdRecipePara.GridButtonItem = GridButtonItem.Export;

            grdRecipePara.View.SetIsReadOnly();

            grdRecipePara.View.AddTextBoxColumn("EQUIPMENTID", 80).SetTextAlignment(TextAlignment.Center);
            grdRecipePara.View.AddTextBoxColumn("EQUIPMENTNAME", 150);
            grdRecipePara.View.AddTextBoxColumn("RECIPEID", 150).SetTextAlignment(TextAlignment.Center);
            grdRecipePara.View.AddTextBoxColumn("RECIPEVERSION", 60).SetLabel("ITEMVERSION").SetTextAlignment(TextAlignment.Center);
            grdRecipePara.View.AddTextBoxColumn("PARAMETERID", 100).SetTextAlignment(TextAlignment.Center);
            grdRecipePara.View.AddTextBoxColumn("TARGET", 80).SetTextAlignment(TextAlignment.Center);
            grdRecipePara.View.AddTextBoxColumn("LSL", 80).SetTextAlignment(TextAlignment.Center);
            grdRecipePara.View.AddTextBoxColumn("USL", 80).SetTextAlignment(TextAlignment.Center);

            grdRecipePara.View.OptionsView.AllowCellMerge = true; // CellMerge
            grdRecipePara.View.PopulateColumns();  
            #endregion

            #endregion

            #region - 이상발생 | 
            grdAbnormal.GridButtonItem = GridButtonItem.Export;

            grdAbnormal.View.SetIsReadOnly();

            grdAbnormal.View.AddTextBoxColumn("LOTID", 190).SetTextAlignment(TextAlignment.Center);
            grdAbnormal.View.AddTextBoxColumn("PATHSEQUENCE", 70).SetIsHidden();
            grdAbnormal.View.AddTextBoxColumn("USERSEQUENCE", 60).SetTextAlignment(TextAlignment.Center);
            grdAbnormal.View.AddTextBoxColumn("PROCESSSEGMENTID", 150).SetIsHidden();
            grdAbnormal.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150);
            grdAbnormal.View.AddTextBoxColumn("OCCURDATE", 140).SetTextAlignment(TextAlignment.Center);
            grdAbnormal.View.AddTextBoxColumn("AREANAME", 150);
            grdAbnormal.View.AddTextBoxColumn("ABNORMALTYPE", 100).SetTextAlignment(TextAlignment.Center);
            grdAbnormal.View.AddTextBoxColumn("REASONCODECLASSNAME", 150).SetTextAlignment(TextAlignment.Center);
            grdAbnormal.View.AddTextBoxColumn("REASONCODENAME", 150).SetTextAlignment(TextAlignment.Center);
            grdAbnormal.View.AddTextBoxColumn("ABNORMALSTATUS", 100).SetTextAlignment(TextAlignment.Center);
            grdAbnormal.View.AddTextBoxColumn("STOPRELEASEDATE", 140).SetTextAlignment(TextAlignment.Center);

            grdAbnormal.View.PopulateColumns();
            #endregion

            #region - 불량내역 |
            grdDefect.GridButtonItem = GridButtonItem.Export;

            grdDefect.View.SetIsReadOnly();

            var gDefectSegemntCol = grdDefect.View.AddGroupColumn("WORKPROCESSSEGMENT");
            gDefectSegemntCol.AddTextBoxColumn("LOTID", 190).SetTextAlignment(TextAlignment.Center);
            gDefectSegemntCol.AddTextBoxColumn("PATHSEQUENCE", 70).SetIsHidden();
            gDefectSegemntCol.AddTextBoxColumn("PROCESSSEQUENCE", 60).SetTextAlignment(TextAlignment.Center);
            gDefectSegemntCol.AddTextBoxColumn("PROCESSSEGMENTID", 150).SetIsHidden();
            gDefectSegemntCol.AddTextBoxColumn("PROCESSSEGMENTNAME", 150);
            gDefectSegemntCol.AddTextBoxColumn("AREANAME", 150);
            gDefectSegemntCol.AddTextBoxColumn("INPUTPCSQTY", 80).SetDisplayFormat("{0:#,###}");

            var gDefectCol = grdDefect.View.AddGroupColumn("DEFECT");
            gDefectCol.AddTextBoxColumn("DEFECTQTY", 80).SetDisplayFormat("{0:#,###}");
            //gDefectCol.AddTextBoxColumn("DEFECTETC", 80).SetDisplayFormat("{0:#,###}");

            var gDefectRateCol = grdDefect.View.AddGroupColumn("DEFECTIVERATE");
            gDefectRateCol.AddTextBoxColumn("DEFECTRATE", 80).SetTextAlignment(TextAlignment.Center);
            gDefectRateCol.AddTextBoxColumn("CUM_DEFECTRATE", 80).SetTextAlignment(TextAlignment.Center);

            var gDefectInspCol = grdDefect.View.AddGroupColumn("SELFINSPECTION");
            gDefectInspCol.AddTextBoxColumn("INBOUNDINSPECTION", 80).SetLabel("INBOUNDINSPECTION").SetTextAlignment(TextAlignment.Center);
            gDefectInspCol.AddTextBoxColumn("SELFINSPECTION", 80).SetLabel("SELFINSPECTIONSHIP").SetTextAlignment(TextAlignment.Center);
            gDefectInspCol.AddTextBoxColumn("AOI", 80).SetTextAlignment(TextAlignment.Center).SetLabel("AOIBBTDEFECT");

            var gDefectPreDictCol = grdDefect.View.AddGroupColumn("PREDICTDEFECTRATE");
            gDefectPreDictCol.AddTextBoxColumn("PROCESSSEGMENT", 80).SetTextAlignment(TextAlignment.Center);
            gDefectPreDictCol.AddTextBoxColumn("CUM_PREDICTDEFECTRATE", 80).SetTextAlignment(TextAlignment.Center);

            grdDefect.View.PopulateColumns();

            // 하단 Summary
            InitializeDefectSummary();
            #endregion

            #region - 주차정보 |

            grdInkjet.Width = 750;
            this.grdPacking.Width = 750;

            #region * Inkjet Grid |
            grdInkjet.GridButtonItem = GridButtonItem.Export;

            grdInkjet.View.SetIsReadOnly();

            grdInkjet.View.AddTextBoxColumn("LOTID", 190).SetTextAlignment(TextAlignment.Center);
            grdInkjet.View.AddTextBoxColumn("USERSEQUENCE", 60).SetTextAlignment(TextAlignment.Center);
            grdInkjet.View.AddTextBoxColumn("PROCESSSEGMENTID", 150).SetIsHidden();
            grdInkjet.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150);
            grdInkjet.View.AddTextBoxColumn("WORKENDDATE", 140).SetTextAlignment(TextAlignment.Center);
            grdInkjet.View.AddTextBoxColumn("AREANAME", 150);
            grdInkjet.View.AddTextBoxColumn("WEEK", 100).SetTextAlignment(TextAlignment.Center);

            grdInkjet.View.PopulateColumns();
            #endregion

            #region * QR Grid |
            grdQR.GridButtonItem = GridButtonItem.Export;

            grdQR.View.SetIsReadOnly();

            grdQR.View.AddTextBoxColumn("LOTID", 190).SetTextAlignment(TextAlignment.Center);
            grdQR.View.AddTextBoxColumn("USERSEQUENCE", 60).SetTextAlignment(TextAlignment.Center);
            grdQR.View.AddTextBoxColumn("PROCESSSEGMENTID", 150).SetIsHidden();
            grdQR.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150);
            grdQR.View.AddTextBoxColumn("WORKENDDATE", 140).SetTextAlignment(TextAlignment.Center);
            grdQR.View.AddTextBoxColumn("AREANAME", 150);
            grdQR.View.AddTextBoxColumn("QRNO", 100).SetTextAlignment(TextAlignment.Center);

            grdQR.View.PopulateColumns();
            #endregion

            #region * 포장 Grid |
            grdPacking.GridButtonItem = GridButtonItem.Export;

            grdPacking.View.SetIsReadOnly();

            grdPacking.View.AddTextBoxColumn("LOTID", 190).SetTextAlignment(TextAlignment.Center);
            grdPacking.View.AddTextBoxColumn("BOXNO", 180).SetTextAlignment(TextAlignment.Center);
            grdPacking.View.AddTextBoxColumn("WEEK", 60).SetTextAlignment(TextAlignment.Center);
            grdPacking.View.AddTextBoxColumn("QTY", 60).SetDisplayFormat("{0:#,###}");
            grdPacking.View.AddTextBoxColumn("PACKINGDATE", 140).SetTextAlignment(TextAlignment.Center);

            grdPacking.View.PopulateColumns();
            #endregion

            #endregion

            #region - FILM |

            grdFilm.GridButtonItem = GridButtonItem.Export;

            grdFilm.View.SetIsReadOnly();

            grdFilm.View.AddTextBoxColumn("LOTID", 190).SetTextAlignment(TextAlignment.Center);
            grdFilm.View.AddTextBoxColumn("PATHSEQUENCE", 70).SetIsHidden();
            grdFilm.View.AddTextBoxColumn("USERSEQUENCE", 60).SetTextAlignment(TextAlignment.Center);
            grdFilm.View.AddTextBoxColumn("PROCESSSEGMENTID", 150).SetIsHidden();
            grdFilm.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150);
            grdFilm.View.AddTextBoxColumn("WORKENDDATE", 140).SetTextAlignment(TextAlignment.Center);
            grdFilm.View.AddTextBoxColumn("AREANAME", 150);
            grdFilm.View.AddTextBoxColumn("CONTRACTION", 120).SetTextAlignment(TextAlignment.Center);
            grdFilm.View.AddTextBoxColumn("DURABLELOTID", 150).SetTextAlignment(TextAlignment.Center);

            grdFilm.View.PopulateColumns();
            #endregion

            #region - W-TIME |
            grdWTIME.GridButtonItem = GridButtonItem.Export;

            grdWTIME.View.SetIsReadOnly();

            grdWTIME.View.AddTextBoxColumn("LOTID", 190).SetTextAlignment(TextAlignment.Center);
            grdWTIME.View.AddTextBoxColumn("USERSEQUENCE", 80).SetTextAlignment(TextAlignment.Center);
            grdWTIME.View.AddTextBoxColumn("PROCESSSEGMENTID", 150).SetIsHidden();
            grdWTIME.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 120);
            grdWTIME.View.AddTextBoxColumn("TOUSERSEQUENCE", 80).SetTextAlignment(TextAlignment.Center);
            grdWTIME.View.AddTextBoxColumn("TOPROCESSSEGMENTNAME", 120);
            grdWTIME.View.AddTextBoxColumn("STARTTIME", 140).SetTextAlignment(TextAlignment.Center);
            grdWTIME.View.AddTextBoxColumn("STDTIMEPERMINUTE", 100).SetTextAlignment(TextAlignment.Center);
            grdWTIME.View.AddTextBoxColumn("LIMITTIME", 140).SetTextAlignment(TextAlignment.Center);
            grdWTIME.View.AddTextBoxColumn("EXECUTETIME", 140).SetTextAlignment(TextAlignment.Center);
            grdWTIME.View.AddTextBoxColumn("EXECUTETIMEPERMINUTE", 100).SetTextAlignment(TextAlignment.Center);

            grdWTIME.View.PopulateColumns();
            #endregion

            #region - 출하정보 |
            grdShipping.GridButtonItem = GridButtonItem.Export;

            grdShipping.View.SetIsReadOnly();

            grdShipping.View.AddTextBoxColumn("TXNHISTKEY", 180).SetIsHidden();
            grdShipping.View.AddTextBoxColumn("TXNGROUPHISTKEY", 180).SetIsHidden();
            grdShipping.View.AddTextBoxColumn("LOTID", 180).SetTextAlignment(TextAlignment.Center);
            grdShipping.View.AddTextBoxColumn("DEGREE", 60).SetTextAlignment(TextAlignment.Center);
            grdShipping.View.AddTextBoxColumn("INSPECTDATE", 150).SetTextAlignment(TextAlignment.Center);
            grdShipping.View.AddTextBoxColumn("AREANAME", 120);
            grdShipping.View.AddTextBoxColumn("INSPECTINRESULT", 80).SetTextAlignment(TextAlignment.Center);
            grdShipping.View.AddTextBoxColumn("INSPECTORNAME", 120).SetTextAlignment(TextAlignment.Center);
            grdShipping.View.AddTextBoxColumn("INSPECTIONDEFID", 100).SetIsHidden();

            grdShipping.View.PopulateColumns();
            #endregion

            #region - 출하정보 :: 불량정보 |
            grdShippingDefect.GridButtonItem = GridButtonItem.Export;

            grdShippingDefect.View.SetIsReadOnly();

            grdShippingDefect.View.AddTextBoxColumn("LOTID", 180).SetTextAlignment(TextAlignment.Center);
            grdShippingDefect.View.AddTextBoxColumn("DEFECTCODE", 80).SetTextAlignment(TextAlignment.Center);
            grdShippingDefect.View.AddTextBoxColumn("DEFECTCODENAME", 150);
            grdShippingDefect.View.AddTextBoxColumn("DEFECTQTY", 70).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###.##}");

            grdShippingDefect.View.PopulateColumns();

            grdShippingDefect.Width = 600;
            #endregion

            #region - 메시지 정보 | 
            grdMessage.GridButtonItem = GridButtonItem.Export;

            grdMessage.View.SetIsReadOnly();

            grdMessage.View.AddTextBoxColumn("LOTID", 190).SetTextAlignment(TextAlignment.Center);
            grdMessage.View.AddTextBoxColumn("TXNHISTKEY", 180).SetIsHidden();
            grdMessage.View.AddTextBoxColumn("PATHSEQUENCE", 70).SetIsHidden();
            grdMessage.View.AddTextBoxColumn("USERSEQUENCE", 60).SetTextAlignment(TextAlignment.Center);
            grdMessage.View.AddTextBoxColumn("PROCESSSEGMENTID", 150).SetIsHidden();
            grdMessage.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150);
            grdMessage.View.AddTextBoxColumn("AREANAME", 150);
            grdMessage.View.AddTextBoxColumn("MESSAGETYPE", 140).SetTextAlignment(TextAlignment.Center);
            grdMessage.View.AddTextBoxColumn("WRITER", 60).SetTextAlignment(TextAlignment.Center);
            grdMessage.View.AddTextBoxColumn("WRITEDATE", 140).SetTextAlignment(TextAlignment.Center);

            grdMessage.View.PopulateColumns();
            #endregion

            #region - 작업장변경 |
            grdChangeArea.GridButtonItem = GridButtonItem.Export;

            grdChangeArea.View.SetIsReadOnly();

            grdChangeArea.View.AddTextBoxColumn("LOTID", 190).SetTextAlignment(TextAlignment.Center);
            grdChangeArea.View.AddTextBoxColumn("USERSEQUENCE", 60).SetTextAlignment(TextAlignment.Center);
            grdChangeArea.View.AddTextBoxColumn("PROCESSSEGMENTID", 150).SetIsHidden();
            grdChangeArea.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150).SetTextAlignment(TextAlignment.Center);
            grdChangeArea.View.AddTextBoxColumn("PREVAREANAME", 140);
            grdChangeArea.View.AddTextBoxColumn("TOAREANAME", 140);
            grdChangeArea.View.AddTextBoxColumn("CHANGEDATE", 160).SetTextAlignment(TextAlignment.Center);
            grdChangeArea.View.AddTextBoxColumn("CHANGEUSER", 80).SetTextAlignment(TextAlignment.Center).SetLabel("PROCESSUSER"); 

            grdChangeArea.View.PopulateColumns();
            #endregion
        }

        #region ▶ Defect Grid Footer 추가 합계 표시 |
        /// <summary>
        /// Lot Grid Footer 추가 Panel, PCS 합계 표시
        /// </summary>
        private void InitializeDefectSummary()
        {
            grdDefect.View.Columns["LOTID"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdDefect.View.Columns["LOTID"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));
            grdDefect.View.Columns["DEFECTQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdDefect.View.Columns["DEFECTQTY"].SummaryItem.DisplayFormat = " ";
            //grdDefect.View.Columns["DEFECTETC"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //grdDefect.View.Columns["DEFECTETC"].SummaryItem.DisplayFormat = " ";
            grdDefect.View.Columns["INBOUNDINSPECTION"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdDefect.View.Columns["INBOUNDINSPECTION"].SummaryItem.DisplayFormat = " ";
            grdDefect.View.Columns["SELFINSPECTION"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdDefect.View.Columns["SELFINSPECTION"].SummaryItem.DisplayFormat = " ";
            grdDefect.View.Columns["AOI"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdDefect.View.Columns["AOI"].SummaryItem.DisplayFormat = " ";

            grdDefect.View.OptionsView.ShowFooter = true;
            grdDefect.ShowStatusBar = false;
        }
        #endregion
        #endregion

        #region ▶ 화면 Control 설정 |
        /// <summary>
        /// 화면 Control 설정
        /// </summary>
        private void InitializeControls()
        {
            if (UserInfo.Current.Enterprise.Equals("YOUNGPOONG"))
            {
                this.tabLotHist.TabPages[0].PageVisible = false;
            }

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("USERCLASSID", "Developer");
            param.Add("USERID", UserInfo.Current.Id);

            DataTable dt = SqlExecuter.Query("GetUserByUserClassId", "00001", param);

            if(UserAdminGroup.Contains(UserInfo.Current.Id))
            {
                this.tabMain.TabPages[2].PageVisible = true;
            }
            else
            {
                this.tabMain.TabPages[2].PageVisible = false;
            }
        }
        #endregion

        #region ▶ TreeList 설정
        /// <summary>
        /// LOT 가계도 TREELIST 
        /// </summary>
        private void InitializeTreeList()
        {
            treeLotGeneal.SetResultCount(1);
            treeLotGeneal.SetIsReadOnly();
            treeLotGeneal.SetMember("LOTNAME", "LOTID", "PARENTLOTID");
            treeLotGeneal.SetSortColumn("LEVEL", SortOrder.Ascending);
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

            Shown += LotHistory_Shown;

            // Tab Index Change
            this.tabLotHist.SelectedPageChanged += TabHistory_SelectedPageChanged;
            tabMain.SelectedPageChanged += TabMain_SelectedPageChanged;

            // TreeList Event
            treeLotGeneal.FocusedNodeChanged += treeGeneal_FocusedNodeChanged;
            treeLotGeneal.NodeCellStyle += TreeLotGeneal_NodeCellStyle;

            // Grid Event
            // 내부 Revision 변경 시 Cell에 색상 표시
            grdLotRouting.View.RowCellStyle += View_RowCellStyle;
            this.grdLotRouting.View.DoubleClick += LotRoutingView_DoubleClick;
            this.grdInspectionMeasure.View.DoubleClick += MeasureView_DoubleClick;
            this.grdShipping.View.RowClick += ShippingView_RowClick;
            this.grdMessage.View.RowClick += MessageView_RowClick;
            this.grdMessage.View.FocusedRowChanged += MessageView_FocusedRowChanged;

            this.grdRecipe.View.RowClick += RecipeView_RowClick;

            this.grdDefect.View.DoubleClick += DefectView_DoubleClick;
            grdDefect.View.CustomDrawFooterCell += GrdDefectView_CustomDrawFooterCell;
        }

        private void LotHistory_Shown(object sender, EventArgs e)
        {
            if (_parameter != null)
            {
                DialogManager.ShowWaitDialog();

                Conditions.GetControl<SmartSelectPopupEdit>("LOTID").SetValue(Format.GetString(_parameter["LOTID"]));

                SearchData(Format.GetString(_parameter["LOTID"]));

                DialogManager.Close();
            }
        }


        #region ▶ Tab Index Changed |
        /// <summary>
        /// Tab Index Changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabHistory_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            switch (this.tabLotHist.SelectedTabPageIndex)
            {
                case 0: // 사양정보
                    getProductSpec();
                    break;
                case 1: // 계측값
                    getMeasureValue();
                    break;
                case 2: // 원부자재
                    getConsumableMaterial();
                    break;
                case 3: // 치공구
                    getDurable();
                    break;
                case 4: // 설비
                    getEquipment();
                    break;
                case 5: // RECIPE
                    getRecipe();
                    break;
                case 6: // 이상발생
                    getAbnormal();
                    break;
                case 7: // 불량내역
                    getDefect();
                    break;
                case 8: // 주차정보
                    getWeek();
                    break;
                case 9: // FILM
                    getFilm();
                    break;
                case 10: // Q-Time
                    getWTIME();
                    break;
                case 11: // 출하정보
                    getShippingInfo();
                    break;
                case 12: // 메시지 정보
                    getMessage();
                    break;
                case 13: // 작업장 변경
                    getChangeArea();
                    break;
                default:
                    getProductSpec();
                    break;
            }
        }

        /// <summary>
        /// 상단 탭 선택 Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabMain_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (grdLotInfo.DataSource == null) return;

            string lotid = grdLotInfo.GetFieldValue("LOTID").ToString();

            switch (this.tabMain.SelectedTabPageIndex)
            {
                case 0: // Lot 공정이력
                    SearchData(lotid);
                    break;
                case 1: // Lot 분할 이력
                    getLotSplitHistory();
                    break;
                case 2: // Lot Raw Data
                    getLotRawHistory();
                    break;
                default:
                    SearchData(lotid);
                    break;
            }
        }
        #endregion

        #region ▶ Grid Event |

        #region - Lot Routing Row Cell Style
        /// <summary>
        /// Set Row Cell Color when Product Inner Revision Changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 1)
                return;

            if (e.Column.FieldName == "PRODUCTDEFVERSION")
            {
                string prevRevision = Format.GetString(grdLotRouting.View.GetRowCellValue(e.RowHandle - 1, e.Column));
                string curRevision = Format.GetString(grdLotRouting.View.GetRowCellValue(e.RowHandle, e.Column));

                if (prevRevision != curRevision)
                {
                    e.Appearance.BackColor = Color.Red;
                    e.Appearance.ForeColor = Color.White;
                }
            }
        }
        #endregion

        #region - Lot Routing Grid Double Click |
        /// <summary>
        /// Lot Routing Grid Double Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LotRoutingView_DoubleClick(object sender, EventArgs e)
        {
            pnlContent.ShowWaitArea();

            DataRow dr = grdLotRouting.View.GetFocusedDataRow();

            if (dr == null) return;

            string lotid = dr["LOTID"].ToString();

            SearchData(lotid);

            pnlContent.CloseWaitArea();
        }
        #endregion

        #region - 출하검사 Row Click |
        /// <summary>
        /// 출하검사 Row Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShippingView_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle < 0)
                return;

            DataRow dr = this.grdShipping.View.GetFocusedDataRow();

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("P_TXNGROUPHISTKEY", dr["TXNGROUPHISTKEY"].ToString());
            param.Add("P_LOTID", dr["LOTID"].ToString());
            param.Add("P_DEGREE", dr["DEGREE"].ToString());

            this.grdShippingDefect.DataSource = SqlExecuter.Query("SelectLotHistoryShipmentInspectionDefect", "10001", param);
        }
        #endregion

        #region - 메시지 Row Click |
        /// <summary>
        /// 메시지 Row Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MessageView_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle < 0)
                return;

            DataRow dr = this.grdMessage.View.GetFocusedDataRow();

            getLotMessage(dr);
        }
        #endregion

        #region - 메시지 Focuse Changed |
        /// <summary>
        /// 메시지 Focuse Changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MessageView_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle < 0)
                return;

            DataRow dr = this.grdMessage.View.GetFocusedDataRow();

            getLotMessage(dr);
        }
        #endregion

        #region - 계측검사 ROW DOUBLE CLICK |
        /// <summary>
        /// 계측검사 ROW DOUBLE CLICK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MeasureView_DoubleClick(object sender, EventArgs e)
        {
            Assembly asm = Assembly.LoadFrom("Micube.SmartMES.QualityAnalysis.dll");

            if (asm == null) return;

            Type type = asm.GetType("Micube.SmartMES.QualityAnalysis.popupMeasureValueRegistration");
            PropertyInfo rowProperty = type.GetProperty("CurrentDataRow");

            object obj = Activator.CreateInstance(type);
            rowProperty.SetValue(obj, grdInspectionMeasure.View.GetFocusedDataRow(), null);

            var frm = (Form)obj;

            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();

        }
        #endregion

        #region - Defect Grid Footer Sum Event |
        /// <summary>
        /// Grid Footer Sum Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdDefectView_CustomDrawFooterCell(object sender, DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventArgs e)
        {
            DataTable dt = grdDefect.DataSource as DataTable;

            if (dt == null) return;

            if (dt.Rows.Count > 0)
            {
                int defectQty = 0;
                //int defectETCQty = 0;
                int selfTakeInspQty = 0;
                int selfShippingInspQty = 0;
                int aoiInspQty = 0;

                dt.Rows.Cast<DataRow>().ForEach(row =>
                {
                    defectQty += Format.GetInteger(row["DEFECTQTY"]);
                    //defectETCQty += Format.GetInteger(row["DEFECTETC"]);
                    selfTakeInspQty += Format.GetInteger(row["INBOUNDINSPECTION"]);
                    selfShippingInspQty += Format.GetInteger(row["SELFINSPECTION"]);
                    aoiInspQty += Format.GetInteger(row["AOI"]);
                });

                if (e.Column.FieldName == "DEFECTQTY")
                {
                    e.Info.DisplayText = Format.GetString(defectQty);
                }
                /*
                if (e.Column.FieldName == "DEFECTETC")
                {
                    e.Info.DisplayText = Format.GetString(defectETCQty);
                }
                */
                if (e.Column.FieldName == "INBOUNDINSPECTION")
                {
                    e.Info.DisplayText = Format.GetString(selfTakeInspQty);
                }
                if (e.Column.FieldName == "SELFINSPECTION")
                {
                    e.Info.DisplayText = Format.GetString(selfShippingInspQty);
                }
                if (e.Column.FieldName == "AOI")
                {
                    e.Info.DisplayText = Format.GetString(aoiInspQty);
                }
            }
            else
            {
                grdDefect.View.Columns["DEFECTQTY"].SummaryItem.DisplayFormat = "  ";
                //grdDefect.View.Columns["DEFECTETC"].SummaryItem.DisplayFormat = "  ";
                grdDefect.View.Columns["INBOUNDINSPECTION"].SummaryItem.DisplayFormat = "  ";
                grdDefect.View.Columns["SELFINSPECTION"].SummaryItem.DisplayFormat = "  ";
                grdDefect.View.Columns["AOI"].SummaryItem.DisplayFormat = "  ";
            }
        }
        #endregion

        #region - Recipe Grid RowClick |
        /// <summary>
        /// Recipe Grid RowClick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RecipeView_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle < 0)
                return;

            DataRow dr = this.grdRecipe.View.GetFocusedDataRow();

            getRecipePara(dr);
        }
        #endregion

        #region - Lot Defect Double Click |
        /// <summary>
        /// Lot Defect Double Click 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DefectView_DoubleClick(object sender, EventArgs e)
        {
            DataRow dr = this.grdDefect.View.GetFocusedDataRow();

            LotHistoryDefectPopup pop = new LotHistoryDefectPopup(grdLotInfo.GetFieldValue("LOTID").ToString());
            pop.Size = new Size(1500, 900);
            pop.ShowDialog();
        } 
        #endregion
        #endregion

        #region ▶ TreeList Event |
        /// <summary>
        /// TreeList Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeGeneal_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            pnlContent.ShowWaitArea();

            DataRow focusRow = treeLotGeneal.GetFocusedDataRow();

            if (focusRow == null)
            {
                pnlContent.CloseWaitArea();
                return;
            }

            switch (this.tabMain.SelectedTabPageIndex)
            {
                case 0: // Lot 공정이력
                    getLotHistory(this.grdLotRouting, focusRow["LOTID"].ToString());
                    break;
                case 1: // Lot 분할 이력
                    getLotSplitHistory(focusRow["LOTID"].ToString());
                    break;
                case 2: // Lot Raw Data
                    getLotRawHistory(focusRow["LOTID"].ToString());
                    break;
                default:
                    break;
            }

            pnlContent.CloseWaitArea();
        }

        /// <summary>
        /// TreeList Node Cell Stype
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeLotGeneal_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                e.Appearance.ForeColor = Color.DodgerBlue;
                e.Appearance.FontStyleDelta = FontStyle.Bold;
            }
            else if (e.Node.Level == 1)
            {
                e.Appearance.ForeColor = Color.DeepPink;
                e.Appearance.FontStyleDelta = FontStyle.Bold;
            }
        }
        #endregion

        #endregion

        #region ◆ 툴바 |

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();
        }

        #region ▶ 데이터 저장 |
        /// <summary>
        /// 데이터 저장
        /// </summary>
        private void SaveRule()
        {
            // TODO : 저장 Rule 변경
        }
        #endregion

        #endregion

        #region ◆ 검색 |

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            // 기존 Grid Data 초기화
            SetInitControl();

            SearchData("");

            // LOT 가계도 Tree
            getLotGeneal(grdLotInfo.GetFieldValue("LOTID").ToString());
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
        private void SetInitControl()
        {
            // Data 초기화
            grdLotInfo.ClearData();
            grdProductInfo.ClearData();

            //grdRouting.View.ClearDatas();
            grdLotRouting.View.ClearDatas();
            grdInspectionMeasure.View.ClearDatas();
            grdConsumable.View.ClearDatas();
            grdDurable.View.ClearDatas();
            grdRecipe.View.ClearDatas();
            grdRecipePara.View.ClearDatas();
            grdAbnormal.View.ClearDatas();
            grdDefect.View.ClearDatas();
            grdInkjet.View.ClearDatas();
            grdFilm.View.ClearDatas();
            grdWTIME.View.ClearDatas();
            grdShipping.View.ClearDatas();
            grdMessage.View.ClearDatas();

            treeLotGeneal.DataSource = null;

            txtTitle.Text = string.Empty;
            txtComment.Rtf = string.Empty;
        }
        #endregion

        #region ▶ LOT 정보 및 LOT 가계도별 이력 조회 |
        /// <summary>
        /// LOT 정보 및 LOT 가계도별 이력 조회
        /// </summary>
        /// <param name="lotid">LotID</param>
        private void SearchData(string lotid)
        {
            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            if (!string.IsNullOrWhiteSpace(lotid))
            {
                values["LOTID"] = lotid;
            }
            else
            {
                lotid = values["LOTID"].ToString();
            }

            DataTable dt = SqlExecuter.Query("SelectLotInfoBylotID", "10003", values);

            if (dt.Rows.Count < 1)
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                return;
            }

            grdLotInfo.DataSource = dt;

            // LOT 공정이력
            getLotGeneal(lotid);

            if (treeLotGeneal.Nodes.Count == 1)
                treeGeneal_FocusedNodeChanged(treeLotGeneal, new FocusedNodeChangedEventArgs(null, treeLotGeneal.Nodes[0]));

            TabHistory_SelectedPageChanged(null, null);
            //getProductSpec();
        }
        #endregion

        #region ▶ LOT 생산이력 조회 |
        /// <summary>
        /// LOT 생산이력 조회
        /// </summary>
        /// <param name="grd"></param>
        /// <param name="lotID"></param>
        private void getLotHistory(SmartBandedGrid grd, string lotID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("LOTID", lotID);

            DataTable dt = SqlExecuter.Query("SelectLotWorkHistoryList", "10001", param);

            if (dt.Rows.Count < 1)
            {
                return;
            }

            grd.View.ClearDatas();

            grd.DataSource = dt;
        }
        #endregion

        #region ▶ LOT 가계도 조회 |
        /// <summary>
        /// LOT 이력 가계도 조회
        /// </summary>
        /// <param name="lotId"></param>
        private void getLotGeneal(string lotId)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("P_LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("LOTID", lotId);

            try
            {
                treeLotGeneal.DataSource = SqlExecuter.Query("SelectLotGenealTreeList", "10001", param);
                treeLotGeneal.PopulateColumns();
                treeLotGeneal.ExpandAll();

                treeLotGeneal.SetFocusedNode(treeLotGeneal.FindNodeByKeyID(lotId));
            }
            catch (Exception ex)
            {
            }
        }
        #endregion

        #region ▶ 품목 사양정보 |
        /// <summary>
        /// 품목 사양정보
        /// </summary>
        private void getProductSpec()
        {
            if (grdLotInfo.DataSource == null) return;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("P_PRODUCTDEFID", grdLotInfo.GetFieldValue("PRODUCTDEFID").ToString());
            param.Add("P_PRODUCTDEFVERSION", grdLotInfo.GetFieldValue("PRODUCTDEFVERSION").ToString());

            grdProductInfo.DataSource = SqlExecuter.Query("SelectProductSpecInfo", "10001", param);
        }
        #endregion

        #region ▶ 계측값 정보 |
        /// <summary>
        /// 계측값 정보
        /// </summary>
        private void getMeasureValue()
        {
            if (grdLotInfo.DataSource == null) return;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("LOTID", grdLotInfo.GetFieldValue("LOTID").ToString());

            grdInspectionMeasure.DataSource = SqlExecuter.Query("SelectLotHistoryMeasure", "10001", param);
        }
        #endregion

        #region ▶ 원부자재 정보 |
        /// <summary>
        /// 원부자재 정보
        /// </summary>
        private void getConsumableMaterial()
        {
            if (grdLotInfo.DataSource == null) return;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("LOTID", grdLotInfo.GetFieldValue("LOTID").ToString());

            grdConsumable.DataSource = SqlExecuter.Query("SelectLotHistoryConsumable", "10001", param);
        }
        #endregion

        #region ▶ 치공구 |
        /// <summary>
        /// 치공구 정보
        /// </summary>
        private void getDurable()
        {
            if (grdLotInfo.DataSource == null) return;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("LOTID", grdLotInfo.GetFieldValue("LOTID").ToString());

            grdDurable.DataSource = SqlExecuter.Query("SelectLotHistoryDurable", "10001", param);
        }
        #endregion

        #region ▶ 설비 |
        /// <summary>
        /// 설비
        /// </summary>
        private void getEquipment()
        {
            if (grdLotInfo.DataSource == null) return;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("LOTID", grdLotInfo.GetFieldValue("LOTID").ToString());

            grdEquipment.DataSource = SqlExecuter.Query("SelectLotHistoryEquipment", "10001", param);
        }
        #endregion

        #region ▶ Recipe |
        /// <summary>
        /// Recipe 정보
        /// </summary>
        private void getRecipe()
        {
            if (grdLotInfo.DataSource == null) return;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("LOTID", grdLotInfo.GetFieldValue("LOTID").ToString());

            grdRecipe.DataSource = SqlExecuter.Query("SelectLotHistoryEquipmentRecipe", "10001", param);
        }
        #endregion

        #region ▶ Recipe Parameter |
        /// <summary>
        /// Recipe 정보
        /// </summary>
        private void getRecipePara(DataRow dr)
        {
            if (grdLotInfo.DataSource == null) return;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("RECIPEID", dr["RECIPEID"].ToString());
            param.Add("EQUIPMENTID", dr["EQUIPMENTID"].ToString()); 

            grdRecipePara.DataSource = SqlExecuter.Query("SelectLotHistoryEquipmentRecipePara", "10001", param);
        }
        #endregion

        #region ▶ 이상발생 |
        /// <summary>
        /// 이상발생 정보
        /// </summary>
        private void getAbnormal()
        {
            if (grdLotInfo.DataSource == null) return;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("LOTID", grdLotInfo.GetFieldValue("LOTID").ToString());

            grdAbnormal.DataSource = SqlExecuter.Query("SelectLotHistoryAbnormal", "10001", param);
        }
        #endregion

        #region ▶ 불량내역 |
        /// <summary>
        /// 불량내역
        /// </summary>
        private void getDefect()
        {
            if (grdLotInfo.DataSource == null) return;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("LOTID", grdLotInfo.GetFieldValue("LOTID").ToString());

            grdDefect.DataSource = SqlExecuter.Query("SelectLotHistoryDefect", "10001", param);
        }
        #endregion

        #region ▶ 주차 정보 |
        /// <summary>
        /// 주차 정보
        /// </summary>
        private void getWeek()
        {
            if (grdLotInfo.DataSource == null) return;

            Dictionary<string, object> param1 = new Dictionary<string, object>();
            param1.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param1.Add("LOTID", grdLotInfo.GetFieldValue("LOTID").ToString());
            param1.Add("P_PROCESSSEGMENTTYPE", "MKPrint");

            grdInkjet.DataSource = SqlExecuter.Query("SelectLotHistoryInkjet", "10001", param1);

            Dictionary<string, object> param2 = new Dictionary<string, object>();
            param2.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param2.Add("LOTID", grdLotInfo.GetFieldValue("LOTID").ToString());
            param2.Add("P_PROCESSSEGMENTTYPE", "QRPrint");

            grdQR.DataSource = SqlExecuter.Query("SelectLotHistoryQR", "10001", param2);

            Dictionary<string, object> param3 = new Dictionary<string, object>();
            param3.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param3.Add("LOTID", grdLotInfo.GetFieldValue("LOTID").ToString());

            grdPacking.DataSource = SqlExecuter.Query("SelectLotHistoryPacking", "10001", param3);
        }
        #endregion

        #region ▶ FILM |
        /// <summary>
        /// Film 정보
        /// </summary>
        private void getFilm()
        {
            if (grdLotInfo.DataSource == null) return;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("LOTID", grdLotInfo.GetFieldValue("LOTID").ToString());

            grdFilm.DataSource = SqlExecuter.Query("SelectLotHistoryFilm", "10001", param);
        }
        #endregion

        #region ▶ W-TIME |
        /// <summary>
        /// Q-TIME 정보
        /// </summary>
        private void getWTIME()
        {
            if (grdLotInfo.DataSource == null) return;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("LOTID", grdLotInfo.GetFieldValue("LOTID").ToString());

            grdWTIME.DataSource = SqlExecuter.Query("SelectLotHistoryWtime", "10001", param);
        }
        #endregion

        #region ▶ 출하정보 |

        #region - 출하검사 정보 |
        /// <summary>
        /// 출하정보
        /// </summary>
        private void getShippingInfo()
        {
            if (grdLotInfo.DataSource == null) return;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("LOTID", grdLotInfo.GetFieldValue("LOTID").ToString());

            grdShipping.DataSource = SqlExecuter.Query("SelectLotHistoryShipmentInspection", "10001", param);
        }
        #endregion

        #endregion

        #region ▶ 메시지 정보 |
        /// <summary>
        /// 메시지 정보
        /// </summary>
        private void getMessage()
        {
            if (grdLotInfo.DataSource == null) return;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("LOTID", grdLotInfo.GetFieldValue("LOTID").ToString());

            grdMessage.DataSource = SqlExecuter.Query("SelectLotHistoryMessage", "10001", param);
        }
        #endregion

        #region ▶ Lot 메시지 내용 조회 |
        /// <summary>
        /// Lot 메시지 내용 조회
        /// </summary>
        /// <param name="dr"></param>
        private void getLotMessage(DataRow dr)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("P_TXNHISTKEY", dr["TXNHISTKEY"].ToString());
            param.Add("P_LOTID", dr["LOTID"].ToString());
            param.Add("P_PROCESSSEGMENTID", dr["PROCESSSEGMENTID"].ToString());
            param.Add("P_USERSEQUENCE", dr["PATHSEQUENCE"].ToString());

            DataTable dt = SqlExecuter.Query("SelectLotMessage", "10001", param);

            if (dt.Rows.Count < 1)
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            this.txtTitle.Text = dt.Rows[0]["TITLE"].ToString();
            this.txtComment.Rtf = dt.Rows[0]["MESSAGE"].ToString();
        }
        #endregion

        #region ▶ 작업장 변경 |
        /// <summary>
        /// 작업장 변경
        /// </summary>
        private void getChangeArea()
        {
            if (grdLotInfo.DataSource == null) return;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("LOTID", grdLotInfo.GetFieldValue("LOTID").ToString());

            grdChangeArea.DataSource = SqlExecuter.Query("SelectLotHistoryChangeArea", "10001", param);
        }
        #endregion

        #region ▶ Lot 분할 이력 |
        /// <summary>
        /// Lot 분할 이력
        /// </summary>
        private void getLotSplitHistory(string lotid = "")
        {
            if (grdLotInfo.DataSource == null) return;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("LOTID", string.IsNullOrWhiteSpace(lotid) ? grdLotInfo.GetFieldValue("LOTID").ToString() : lotid);

            grdLotSplitHistory.DataSource = SqlExecuter.Query("SelectLotSplitMergeHistory", "10001", param);
        }
        #endregion

        #region ▶ Lot Raw History Data |
        /// <summary>
        /// Lot Raw History Data
        /// </summary>
        private void getLotRawHistory(string lotid = "")
        {
            if (grdLotInfo.DataSource == null) return;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("LOTID", string.IsNullOrWhiteSpace(lotid) ? grdLotInfo.GetFieldValue("LOTID").ToString() : lotid);

            grdRawHistory.DataSource = SqlExecuter.Query("SelectLotRawHistory", "10001", param);
        } 
        #endregion

        #endregion
    }
}
