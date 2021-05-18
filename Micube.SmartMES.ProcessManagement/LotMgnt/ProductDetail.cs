#region using
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons;
using Micube.Framework.SmartControls.Grid.BandedGrid;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DevExpress.XtraGrid.Views.Grid;
using Micube.SmartMES.Commons.Controls;
#endregion

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 재공관리 > 품목상세 (PG-SG-0625)
    /// 업  무  설  명  : 품목상세
    /// 생    성    자  : 박정훈
    /// 생    성    일  : 2019-09-10
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ProductDetail : SmartConditionManualBaseForm
    {
        #region ◆ Local Variables |

        #endregion

        #region ◆ 생성자 |
        /// <summary>
        /// 생성자
        /// </summary>
        public ProductDetail()
        {
            InitializeComponent();
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

            InitializeEvent();
        }
        /// <summary>
        /// 재공조회에서 링크로 화면 이동시 
        /// 2019.10.09 배선용
        /// </summary>
        /// <param name="parameters"></param>
        public override void LoadForm(Dictionary<string, object> parameters)
        {
            base.LoadForm(parameters);

            if (parameters != null)
            {
                Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").SetValue(Format.GetString(parameters["PRODUCTDEFID"]));
                Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").Text = Format.GetString(parameters["PRODUCTDEFNAME"]);

                DataTable dt = new DataTable();
                dt.Columns.Add("PRODUCTDEFID");
                dt.Columns.Add("PRODUCTDEFNAME");
                dt.Columns.Add("PRODUCTDEFTYPE");
                dt.Columns.Add("PRODUCTIONTYPE");
                dt.Columns.Add("UNIT");

                dt.Rows.Add(Format.GetString(parameters["PRODUCTDEFID"]), Format.GetString(parameters["PRODUCTDEFNAME"]), Format.GetString(parameters["PRODUCTDEFTYPE"]), Format.GetString(parameters["PRODUCTIONTYPE"]), Format.GetString(parameters["UNIT"]));
                cboProductDef.Editor.DataSource = dt;
                cboProductDef.Editor.ItemIndex = 0;
                OnSearchAsync();
            }
        }


        #region ▶ 조회조건 설정 |
        /// <summary>
        /// 검색조건 설정
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // 품목
            CommonFunction.AddConditionProductPopup("P_PRODUCTDEFID", 0.5, true, Conditions, "PRODUCTDEFID", "PRODUCTDEFID", true);

            Conditions.GetCondition<ConditionItemSelectPopup>("P_PRODUCTDEFID").SetPopupApplySelection((selectedRows, dataGridRows) =>
            {
                string productDefName = Format.GetString(selectedRows.FirstOrDefault()["PRODUCTDEFNAME"]);

                List<DataRow> list = selectedRows.ToList<DataRow>();

                if (list.Count == 0) return;

                DataTable dt = list.CopyToDataTable();

                // 품목코드 Binding
                cboProductDef.Editor.DataSource = dt;

                if (dt == null) return;

                cboProductDef.Editor.ItemIndex = 0;

                Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").Text = productDefName;
            });
        }

        /// <summary>
        /// 조회조건 설정
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").EditValueChanged += ProductdefIDChanged;
        }

        private void ProductdefIDChanged(object sender, EventArgs e)
        {
            SmartSelectPopupEdit PopProdutid = sender as SmartSelectPopupEdit;

            if (Format.GetFullTrimString(PopProdutid.EditValue).Equals(string.Empty))
            {
                Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").Text = string.Empty;
            }
        }
        #endregion

        #region ▶ ComboBox 초기화 |
        /// <summary>
        /// 콤보박스를 초기화 하는 함수
        /// </summary>
        private void InitializeComboBox()
        {
            #region - 제품 ComboBox |
            // 분류 ComboBox 설정
            cboProductDef.Editor.ValueMember = "PRODUCTDEFID";
            cboProductDef.Editor.DisplayMember = "PRODUCTDEFID";
            #endregion

            #region - Version ComboBox |
            // 분류 ComboBox 설정
            cboProductVersion.ValueMember = "PRODUCTDEFVERSION";
            cboProductVersion.DisplayMember = "PRODUCTDEFVERSION";
            # endregion
        }
        #endregion

        #region ▶ Grid 설정 |
        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            #region - 라우팅 Grid 설정 |
            grdRouting.GridButtonItem = GridButtonItem.Export;

            grdRouting.View.SetIsReadOnly();

            var grpRoutingCol = grdRouting.View.AddGroupColumn("PRODUCTROUTING");
            grpRoutingCol.AddTextBoxColumn("PROCESSDEFID", 70).SetIsHidden();
            grpRoutingCol.AddTextBoxColumn("PATHSEQUENCE", 70).SetIsHidden(); 
            grpRoutingCol.AddTextBoxColumn("USERSEQUENCE", 60).SetTextAlignment(TextAlignment.Center); 
            //grpRoutingCol.AddTextBoxColumn("WORKTYPE", 60).SetTextAlignment(TextAlignment.Center);
            grpRoutingCol.AddTextBoxColumn("PROCESSSEGMENTID", 150).SetIsHidden();
            grpRoutingCol.AddTextBoxColumn("PROCESSSEGMENTNAME", 150);
            grpRoutingCol.AddTextBoxColumn("PLANTID", 60).SetTextAlignment(TextAlignment.Center);
            //grpRoutingCol.AddTextBoxColumn("UOM", 60).SetTextAlignment(TextAlignment.Center);

            var grpWIPCol = grdRouting.View.AddGroupColumn("WIPQTY");
            grpWIPCol.AddTextBoxColumn("LOTCNT", 70).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            grpWIPCol.AddTextBoxColumn("WIPPCSQTY", 70).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            grpWIPCol.AddTextBoxColumn("WIPPNLQTY", 70).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");

            var grpWorkResultCol = grdRouting.View.AddGroupColumn("WIPRESULT");
            grpWorkResultCol.AddTextBoxColumn("RESULTLOTCNT", 70).SetLabel("LOTCNT").SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            // 실적
            grpWorkResultCol.AddTextBoxColumn("RESULTPCSQTY", 70).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            // 예정
            grpWorkResultCol.AddTextBoxColumn("RESULTPNLQTY", 70).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");

            grdRouting.View.PopulateColumns();

            grdRouting.Width = 670;

            grdRouting.View.OptionsView.ShowFooter = true;
            #endregion

            #region - 자재 Grid |
            grdConsume.GridButtonItem = GridButtonItem.Export;

            grdConsume.View.SetIsReadOnly();

            var grpConsumeRoutingCol = grdConsume.View.AddGroupColumn("ROUTINGLIST"); 
            grpConsumeRoutingCol.AddTextBoxColumn("USERSEQUENCE", 60).SetTextAlignment(TextAlignment.Center);
            grpConsumeRoutingCol.AddTextBoxColumn("PROCESSSEGMENTNAME", 150);
            grpConsumeRoutingCol.AddTextBoxColumn("PLANTID", 60).SetTextAlignment(TextAlignment.Center);
            grpConsumeRoutingCol.AddTextBoxColumn("CONSUMABLEDEFID", 150).SetIsHidden();
            grpConsumeRoutingCol.AddTextBoxColumn("CONSUMABLEDEFNAME", 200);

            var grpConsumeCol = grdConsume.View.AddGroupColumn("SUBSIDIARY");
            grpConsumeCol.AddTextBoxColumn("PDQTY", 70).SetLabel("PRODUCTWIP").SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            grpConsumeCol.AddTextBoxColumn("WIPQTY", 70).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            grpConsumeCol.AddTextBoxColumn("STOCKQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            grpConsumeCol.AddTextBoxColumn("LACKQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");

            grdConsume.View.PopulateColumns();
            #endregion

            #region - 치공구 |
            grdDurable.GridButtonItem = GridButtonItem.Export;

            grdDurable.View.SetIsReadOnly();

            var grpDurableRoutingCol = grdDurable.View.AddGroupColumn("ROUTINGLIST");
            grpDurableRoutingCol.AddTextBoxColumn("USERSEQUENCE", 60).SetTextAlignment(TextAlignment.Center);
            grpDurableRoutingCol.AddTextBoxColumn("PROCESSSEGMENTNAME", 150);
            grpDurableRoutingCol.AddTextBoxColumn("PLANTID", 60).SetTextAlignment(TextAlignment.Center);
            grpDurableRoutingCol.AddTextBoxColumn("DURABLEDEFNAME", 200);
            grpDurableRoutingCol.AddTextBoxColumn("AREANAME", 100).SetTextAlignment(TextAlignment.Center);

            var grpDurableCol = grdDurable.View.AddGroupColumn("TOOLINFORMATIONLIST");
            grpDurableCol.AddTextBoxColumn("DURABLEDEFVERSION", 80).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("{0:#,###}");
            grpDurableCol.AddTextBoxColumn("TOTALQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            grpDurableCol.AddTextBoxColumn("AVAILABLEQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            grpDurableCol.AddTextBoxColumn("REPAIRQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");

            grdDurable.View.PopulateColumns();
            #endregion

            #region - 설비 |
            grdEquipment.GridButtonItem = GridButtonItem.Export;

            grdEquipment.View.SetIsReadOnly();

            var grpEquipmentRoutingCol = grdEquipment.View.AddGroupColumn("ROUTINGLIST");
            grpEquipmentRoutingCol.AddTextBoxColumn("USERSEQUENCE", 70).SetTextAlignment(TextAlignment.Center);
            grpEquipmentRoutingCol.AddTextBoxColumn("PROCESSSEGMENTNAME", 150);
            grpEquipmentRoutingCol.AddTextBoxColumn("PLANTID", 70).SetTextAlignment(TextAlignment.Center);
            grpEquipmentRoutingCol.AddTextBoxColumn("AREANAME", 120).SetTextAlignment(TextAlignment.Center);
            grpEquipmentRoutingCol.AddTextBoxColumn("EQUIPMENTCLASS", 200);

            var grpEquipmentCol = grdEquipment.View.AddGroupColumn("EQUIPMENTSTATUS");
            grpEquipmentCol.AddTextBoxColumn("OWNTYPE", 80).SetTextAlignment(TextAlignment.Center); 
            grpEquipmentCol.AddTextBoxColumn("TOTALQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            grpEquipmentCol.AddTextBoxColumn("AVAILABLEQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            grpEquipmentCol.AddTextBoxColumn("REPAIRQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");

            grdEquipment.View.PopulateColumns();
            #endregion

            #region - 불량현황 |
            grdDefect.GridButtonItem = GridButtonItem.Export;

            grdDefect.View.SetIsReadOnly();

            grdDefect.View.AddTextBoxColumn("SALESORDERID", 70).SetTextAlignment(TextAlignment.Center);
            grdDefect.View.AddTextBoxColumn("LINENO", 70).SetTextAlignment(TextAlignment.Center);
            grdDefect.View.AddTextBoxColumn("PUREORDER", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            grdDefect.View.AddTextBoxColumn("INPUTDATE", 80).SetTextAlignment(TextAlignment.Center);
            grdDefect.View.AddTextBoxColumn("INPUTQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            grdDefect.View.AddTextBoxColumn("INPUTRATE", 80).SetTextAlignment(TextAlignment.Right);
            grdDefect.View.AddTextBoxColumn("WIPQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            grdDefect.View.AddTextBoxColumn("INCOMEQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}"); 
            grdDefect.View.AddTextBoxColumn("DEFECTQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            grdDefect.View.AddTextBoxColumn("INPUTDEFECTRATE", 80).SetTextAlignment(TextAlignment.Right);
            grdDefect.View.AddTextBoxColumn("SHIPDEFECTRATE", 80).SetTextAlignment(TextAlignment.Right);

            grdDefect.View.PopulateColumns();
            #endregion

            #region - 연계생산품 현황 |
            grdRelated.GridButtonItem = GridButtonItem.Export;

            grdRelated.View.SetIsReadOnly();

            var grpRelatedCol = grdRelated.View.AddGroupColumn("RELATEPRODUCTSTATUS");
            grpRelatedCol.AddTextBoxColumn("PLANTID", 70).SetTextAlignment(TextAlignment.Center);
            //grpRelatedCol.AddTextBoxColumn("USERSEQUENCE", 80).SetTextAlignment(TextAlignment.Center);
            //grpRelatedCol.AddTextBoxColumn("PROCESSSEGMENTNAME", 150);

            var grpRelatedWIPCol = grdRelated.View.AddGroupColumn("WIPLIST");
            grpRelatedWIPCol.AddTextBoxColumn("LOTCNT", 80).SetDisplayFormat("{0:#,###}");
            grpRelatedWIPCol.AddTextBoxColumn("WIPPCSQTY", 80).SetDisplayFormat("{0:#,###}");
            grpRelatedWIPCol.AddTextBoxColumn("WIPPNLQTY", 80).SetDisplayFormat("{0:#,###}");
            
            var grpRelatedPlanCol = grdRelated.View.AddGroupColumn("PLANED");
            grpRelatedPlanCol.AddTextBoxColumn("PLANPCSQTY", 80).SetDisplayFormat("{0:#,###}");
            grpRelatedPlanCol.AddTextBoxColumn("PLANPNLQTY", 80).SetDisplayFormat("{0:#,###}");

            var grpRelatedInputCol = grdRelated.View.AddGroupColumn("INPUTREADY"); 
            grpRelatedInputCol.AddTextBoxColumn("INPUTRDPCSQTY", 80).SetDisplayFormat("{0:#,###}");
            grpRelatedInputCol.AddTextBoxColumn("INPUTRDPNLQTY", 80).SetDisplayFormat("{0:#,###}");

            var grpRelatedShipCol = grdRelated.View.AddGroupColumn("SENDREADY");
            grpRelatedShipCol.AddTextBoxColumn("RDSHIPPCSQTY", 80).SetDisplayFormat("{0:#,###}");
            grpRelatedShipCol.AddTextBoxColumn("RDSHIPPNLQTY", 80).SetDisplayFormat("{0:#,###}");

            grdRelated.View.PopulateColumns();
            #endregion

            #region - 재공 Grid 설정 |
            grdWIP.GridButtonItem = GridButtonItem.Export;

            grdWIP.View.SetIsReadOnly();
            grdWIP.SetIsUseContextMenu(false);
            // CheckBox 설정

            var groupDefaultCol = grdWIP.View.AddGroupColumn("WIPLIST");
            groupDefaultCol.AddTextBoxColumn("PLANTID", 50).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("LOTTYPE", 80).SetTextAlignment(TextAlignment.Center);

            groupDefaultCol.AddTextBoxColumn("PRODUCTDEFID", 150).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("PRODUCTREVISION", 60).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            groupDefaultCol.AddTextBoxColumn("LOTID", 200).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("USERSEQUENCE", 70).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("PROCESSSEGMENTNAME", 150);
            groupDefaultCol.AddTextBoxColumn("AREANAME", 100).SetTextAlignment(TextAlignment.Center);

            var groupWipCol = grdWIP.View.AddGroupColumn("");
            groupWipCol.AddTextBoxColumn("RTRSHT", 70).SetTextAlignment(TextAlignment.Center);
            groupWipCol.AddTextBoxColumn("ISHOLD", 100).SetTextAlignment(TextAlignment.Center);
            groupWipCol.AddTextBoxColumn("ISLOCKING", 100).SetTextAlignment(TextAlignment.Center);
            groupWipCol.AddTextBoxColumn("QTY", 70).SetTextAlignment(TextAlignment.Right);
            groupWipCol.AddTextBoxColumn("PANELQTY", 70).SetTextAlignment(TextAlignment.Right);

            var groupSendCol = grdWIP.View.AddGroupColumn("WAITFORRECEIVE");
            groupSendCol.AddTextBoxColumn("SENDPCSQTY", 50).SetTextAlignment(TextAlignment.Right);
            groupSendCol.AddTextBoxColumn("SENDPANELQTY", 50).SetTextAlignment(TextAlignment.Right);

            var groupReceiveCol = grdWIP.View.AddGroupColumn("ACCEPT");
            groupReceiveCol.AddTextBoxColumn("RECEIVEPCSQTY", 50).SetTextAlignment(TextAlignment.Right);
            groupReceiveCol.AddTextBoxColumn("RECEIVEPANELQTY", 50).SetTextAlignment(TextAlignment.Right);

            var groupWorkStartCol = grdWIP.View.AddGroupColumn("WIPSTARTQTY");
            groupWorkStartCol.AddTextBoxColumn("WORKSTARTPCSQTY", 50).SetTextAlignment(TextAlignment.Right);
            groupWorkStartCol.AddTextBoxColumn("WORKSTARTPANELQTY", 50).SetTextAlignment(TextAlignment.Right);

            var groupWorkEndCol = grdWIP.View.AddGroupColumn("WIPENDQTY");
            groupWorkEndCol.AddTextBoxColumn("WORKENDPCSQTY", 50).SetTextAlignment(TextAlignment.Right);
            groupWorkEndCol.AddTextBoxColumn("WORKENDPANELQTY", 50).SetTextAlignment(TextAlignment.Right);

            var groupDefaultCol3 = grdWIP.View.AddGroupColumn("WIPLIST");
            groupDefaultCol3.AddTextBoxColumn("UNIT", 50).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol3.AddTextBoxColumn("SEGMENTINCOMETIME", 100).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol3.AddTextBoxColumn("PROCESSSEGMENTLEADTIME", 70).SetTextAlignment(TextAlignment.Right);
            groupDefaultCol3.AddTextBoxColumn("LOTINPUTDATE", 150).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol3.AddTextBoxColumn("TOTALLEADTIME", 50).SetTextAlignment(TextAlignment.Right);
            groupDefaultCol3.AddTextBoxColumn("DELIVERYDATE", 100).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol3.AddTextBoxColumn("LEFTDATE", 70).SetTextAlignment(TextAlignment.Center);
            groupWipCol.AddTextBoxColumn("RESPECTYIELD", 70).SetTextAlignment(TextAlignment.Right);

            grdWIP.View.PopulateColumns();
            #endregion

            #region - Lot 이력 |
            grdLotHist.GridButtonItem = GridButtonItem.Export;

            grdLotHist.View.SetIsReadOnly();
            grdLotHist.SetIsUseContextMenu(false);

            var ghistRoutingCol = grdLotHist.View.AddGroupColumn("WIPLIST");
            ghistRoutingCol.AddTextBoxColumn("LOTID", 180).SetTextAlignment(TextAlignment.Center);
            ghistRoutingCol.AddTextBoxColumn("PATHSEQUENCE", 70).SetIsHidden();
            ghistRoutingCol.AddTextBoxColumn("USERSEQUENCE", 60).SetTextAlignment(TextAlignment.Center);
            ghistRoutingCol.AddTextBoxColumn("WORKTYPE", 70).SetTextAlignment(TextAlignment.Center);

            ghistRoutingCol.AddTextBoxColumn("PROCESSSEGMENTID", 150).SetIsHidden();
            ghistRoutingCol.AddTextBoxColumn("PROCESSSEGMENTNAME", 150);
            ghistRoutingCol.AddTextBoxColumn("PLANTID", 60).SetTextAlignment(TextAlignment.Center);
            ghistRoutingCol.AddTextBoxColumn("AREANAME", 150);
            //ghistRoutingCol.AddTextBoxColumn("UOM", 60).SetTextAlignment(TextAlignment.Center);

            // 자원
            // 설비
            // 작업자

            // 작업일자
            var ghistDateCol = grdLotHist.View.AddGroupColumn("WORKDATE");
            ghistDateCol.AddTextBoxColumn("RECEIVEDATE", 140).SetTextAlignment(TextAlignment.Center);
            ghistDateCol.AddTextBoxColumn("STARTDATE", 140).SetTextAlignment(TextAlignment.Center);
            ghistDateCol.AddTextBoxColumn("WORKENDDATE", 140).SetTextAlignment(TextAlignment.Center);
            ghistDateCol.AddTextBoxColumn("LOTSENDDATE", 140).SetTextAlignment(TextAlignment.Center);

            // 인수수량정보
            var ghistINQTYCol = grdLotHist.View.AddGroupColumn("INQTY");
            ghistINQTYCol.AddTextBoxColumn("RECEIVEPCSQTY", 80).SetDisplayFormat("{0:#,###}");
            ghistINQTYCol.AddTextBoxColumn("RECEIVEPANELQTY", 80).SetDisplayFormat("{0:#,###}");

            // 작업 시작수량정보
            var ghistSTARTQTYCol = grdLotHist.View.AddGroupColumn("WIPSTARTQTY");
            ghistSTARTQTYCol.AddTextBoxColumn("WORKSTARTPCSQTY", 80).SetDisplayFormat("{0:#,###}");
            ghistSTARTQTYCol.AddTextBoxColumn("WORKSTARTPANELQTY", 80).SetDisplayFormat("{0:#,###}");

            // 작업 완료수량정보
            var ghistENDQTYCol = grdLotHist.View.AddGroupColumn("WIPENDQTY");
            ghistENDQTYCol.AddTextBoxColumn("WORKENDPCSQTY", 80).SetDisplayFormat("{0:#,###}");
            ghistENDQTYCol.AddTextBoxColumn("WORKENDPANELQTY", 80).SetDisplayFormat("{0:#,###}");

            // 인계수량정보
            var ghistSENDQTYCol = grdLotHist.View.AddGroupColumn("WIPSENDQTY");
            ghistSENDQTYCol.AddTextBoxColumn("SENDPCSQTY", 80).SetDisplayFormat("{0:#,###}");
            ghistSENDQTYCol.AddTextBoxColumn("SENDPANELQTY", 80).SetDisplayFormat("{0:#,###}");

            // LEADTIME
            var ghistLTCol = grdLotHist.View.AddGroupColumn("LEADTIME");
            ghistLTCol.AddTextBoxColumn("RECEIVELEADTIME", 80).SetDisplayFormat("{0:N2}");
            ghistLTCol.AddTextBoxColumn("WORKSTARTLEADTIME", 80).SetDisplayFormat("{0:N2}");
            ghistLTCol.AddTextBoxColumn("WORKENDLEADTIME", 80).SetDisplayFormat("{0:N2}");
            ghistLTCol.AddTextBoxColumn("SENDLEADTIME", 80).SetDisplayFormat("{0:N2}");
            ghistLTCol.AddTextBoxColumn("LEADTIME", 80).SetDisplayFormat("{0:N2}");
            ghistLTCol.AddTextBoxColumn("CUM_LEADTIME", 80).SetDisplayFormat("{0:N2}");

            // DEFECT
            var ghistDefectCol = grdLotHist.View.AddGroupColumn("DEFECT");
            ghistDefectCol.AddTextBoxColumn("DEFECTQTY", 80).SetDisplayFormat("{0:#,###}");
            ghistDefectCol.AddTextBoxColumn("CUM_DEFECTQTY", 80).SetDisplayFormat("{0:#,###.##}");

            grdLotHist.View.PopulateColumns();

            this.grdLotHist.Height = 350;
            #endregion

            #region - 생산실적 |
            grdResult.GridButtonItem = GridButtonItem.Export;
            grdResult.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdResult.View.SetIsReadOnly();

            var defaultCol = grdResult.View.AddGroupColumn("");
            //SITE
            defaultCol.AddTextBoxColumn("PLANTID", 60).SetTextAlignment(TextAlignment.Center);
            //작업장
            defaultCol.AddTextBoxColumn("AREANAME", 120);
            //RTR/SHT
            defaultCol.AddTextBoxColumn("RTRSHT", 80).SetTextAlignment(TextAlignment.Center);
            //자원
            defaultCol.AddTextBoxColumn("EQUIPMENT", 60).SetLabel("RESOURCE").SetTextAlignment(TextAlignment.Center);
            //양산구분
            defaultCol.AddTextBoxColumn("LOTTYPE", 60).SetTextAlignment(TextAlignment.Center);
            //품목코드
            defaultCol.AddTextBoxColumn("PRODUCTDEFID", 120).SetTextAlignment(TextAlignment.Center);
            defaultCol.AddTextBoxColumn("PRODUCTDEFVERSION", 60).SetLabel("PRODUCTREVISION").SetTextAlignment(TextAlignment.Center);
            //공정명
            defaultCol.AddTextBoxColumn("PROCESSSEGMENTNAME", 150);
            //LOTID
            defaultCol.AddTextBoxColumn("LOTID", 180).SetTextAlignment(TextAlignment.Center);

            //실적
            var sendCol = grdResult.View.AddGroupColumn("FIGURE");
            sendCol.AddTextBoxColumn("WORKENDPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric).SetLabel("PCS");
            sendCol.AddTextBoxColumn("WORKENDPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric).SetLabel("PNL");
            //현재공
            var wipCol = grdResult.View.AddGroupColumn("CURRENTWIP");
            wipCol.AddTextBoxColumn("WIPQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric).SetLabel("PCS");
            wipCol.AddTextBoxColumn("WIPPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric).SetLabel("PNL");
            //투입
            var inputCol = grdResult.View.AddGroupColumn("INPUT");
            inputCol.AddTextBoxColumn("INPUTPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric).SetLabel("PCS");
            inputCol.AddTextBoxColumn("INPUTPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric).SetLabel("PNL");
            //기초
            var baseCol = grdResult.View.AddGroupColumn("BASICS");
            baseCol.AddTextBoxColumn("BASEPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric).SetLabel("PCS");
            baseCol.AddTextBoxColumn("BASEPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric).SetLabel("PNL");

            var defaultCol2 = grdResult.View.AddGroupColumn("");
            //작업구분
            defaultCol2.AddComboBoxColumn("WORKTYPE", 80, new SqlQuery("GetTypeList", "10001", "CODECLASSID=LotWorkType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                .SetTextAlignment(TextAlignment.Center);

            grdResult.View.PopulateColumns();
            #endregion

            InitializationSummaryRow();
        }
        #endregion

        #region ▶ Footer 추가 Panel, PCS 합계 표시 |
        /// <summary>
        /// Footer 추가 Panel, PCS 합계 표시
        /// </summary>
        private void InitializationSummaryRow()
        {
            #region - 라우팅 |
            grdRouting.View.Columns["PROCESSDEFID"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdRouting.View.Columns["PROCESSDEFID"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));
            grdRouting.View.Columns["WIPPCSQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdRouting.View.Columns["WIPPCSQTY"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdRouting.View.Columns["WIPPNLQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdRouting.View.Columns["WIPPNLQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdRouting.View.OptionsView.ShowFooter = true;
            grdRouting.ShowStatusBar = false;
            #endregion
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
            this.tabHistory.SelectedPageChanged += TabHistory_SelectedPageChanged;

            this.grdRouting.View.DoubleClick += RoutingView_DoubleClick;
            this.grdRouting.View.CustomDrawFooterCell += RoutingView_CustomDrawFooterCell;

            this.grdWIP.View.DoubleClick += WIPView_DoubleClick;

            this.cboProductDef.Editor.EditValueChanged += cboProductDef_EditValueChanged;
        }

        #region ▶ Tab Index Changed |
        /// <summary>
        /// Tab Index Changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabHistory_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            // 자재사용이력
            if(this.tabHistory.SelectedTabPageIndex == 0)
            {
                getConsumableHistory();
            }
            else if(this.tabHistory.SelectedTabPageIndex == 1) // 치공구
            {
                getDurableHistory();
            }
            else if (this.tabHistory.SelectedTabPageIndex == 2) // 설비
            {
                getEquipmentHistory();
            }
            else if (this.tabHistory.SelectedTabPageIndex == 3) // 불량현황
            {
                getDefectHistory();
            }
        }
        #endregion

        #region ▶ Grid Event |

        #region - Routing 목록 Double Click |
        /// <summary>
        /// Routing 목록 Double Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RoutingView_DoubleClick(object sender, EventArgs e)
        {
            this.grdWIP.View.ClearDatas();
            this.grdLotHist.View.ClearDatas();
            this.grdResult.View.ClearDatas();

            DataRow dr = this.grdRouting.View.GetFocusedDataRow();

            if (dr == null) return;

            getWIPList(dr["PROCESSSEGMENTID"].ToString());
            getResultList(dr["PROCESSSEGMENTID"].ToString());

            SmartBandedGridView view = (SmartBandedGridView)sender;

            if (view.FocusedColumn.FieldName.Equals("WIPPCSQTY") || view.FocusedColumn.FieldName.Equals("WIPPNLQTY"))
                tabMain.SelectedTabPageIndex = 1;
            else if (view.FocusedColumn.FieldName.Equals("RESULTPCSQTY") || view.FocusedColumn.FieldName.Equals("RESULTPNLQTY"))
                tabMain.SelectedTabPageIndex = 2;
        }
        #endregion

        #region - 재공 목록 Double Click |
        /// <summary>
        /// 재공 목록 Double Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WIPView_DoubleClick(object sender, EventArgs e)
        {
            this.grdLotHist.View.ClearDatas();

            DataRow dr = this.grdWIP.View.GetFocusedDataRow();

            if (dr == null) return;

            getLotHistory(dr["LOTID"].ToString());
        }
        #endregion

        #region - Routing Grid Footer Sum Event |
        /// <summary>
        /// Grid Footer Sum Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RoutingView_CustomDrawFooterCell(object sender, DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventArgs e)
        {
            Font ft = new Font("Mangul Gothic", 10F, FontStyle.Bold);
            e.Appearance.BackColor = Color.LightBlue;
            e.Appearance.FillRectangle(e.Cache, e.Bounds);
            e.Info.AllowDrawBackground = false;
            e.Appearance.Font = ft;

            DataTable dt = grdRouting.DataSource as DataTable;

            if (dt == null) return;

            if (dt.Rows.Count > 0)
            {
                int PanelSum = 0;
                int qtySum = 0;
                dt.Rows.Cast<DataRow>().ForEach(row =>
                {
                    PanelSum += Format.GetInteger(row["WIPPNLQTY"]);
                    qtySum += Format.GetInteger(row["WIPPCSQTY"]);
                });

                if (e.Column.FieldName == "WIPPNLQTY")
                {
                    e.Info.DisplayText = string.Format("{0:###,###}", PanelSum);
                }
                if (e.Column.FieldName == "WIPPCSQTY")
                {
                    e.Info.DisplayText = string.Format("{0:###,###}", qtySum);
                }
            }
            else
            {
                grdRouting.View.Columns["WIPPNLQTY"].SummaryItem.DisplayFormat = "  ";
                grdRouting.View.Columns["WIPPCSQTY"].SummaryItem.DisplayFormat = "  ";
            }
        }
        #endregion

        #region - Related Grid Footer Sum Event |
        /// <summary>
        /// Grid Footer Sum Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RelatedView_CustomDrawFooterCell(object sender, DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventArgs e)
        {
            DataTable dt = grdRelated.DataSource as DataTable;

            if (dt == null) return;

            if (dt.Rows.Count > 0)
            {
                int PlanPcsSum = 0;
                int PlanPnlSum = 0;
                int InputPcsSum = 0;
                int InputPnlSum = 0;
                int WipPcsSum = 0;
                int WipPnlSum = 0;
                int ShipPcsSum = 0;
                int ShipPnlSum = 0;

                dt.Rows.Cast<DataRow>().ForEach(row =>
                {
                    WipPcsSum += Format.GetInteger(row["WIPPCSQTY"]);
                    WipPnlSum += Format.GetInteger(row["WIPPNLQTY"]);
                    //PlanPcsSum += Format.GetInteger(row["PLANPCSQTY"]);
                    //PlanPnlSum += Format.GetInteger(row["PLANPNLQTY"]);
                    //InputPcsSum += Format.GetInteger(row["INPUTPCSQTY"]);
                    //InputPnlSum += Format.GetInteger(row["INPUTPNLQTY"]);
                    ShipPcsSum += Format.GetInteger(row["RDSHIPPCSQTY"]);
                    ShipPnlSum += Format.GetInteger(row["RDSHIPPNLQTY"]);
                });

                if (e.Column.FieldName == "WIPPCSQTY")
                {
                    e.Info.DisplayText = string.Format(Format.GetString(WipPcsSum), "{0:#,###}");
                }
                if (e.Column.FieldName == "WIPPNLQTY")
                {
                    e.Info.DisplayText = string.Format(Format.GetString(WipPnlSum), "{0:#,###}");
                }
                if (e.Column.FieldName == "PLANPNLQTY")
                {
                    e.Info.DisplayText = string.Format(Format.GetString(PlanPnlSum), "{0:#,###}");
                }
                if (e.Column.FieldName == "PLANPCSQTY")
                {
                    e.Info.DisplayText = string.Format(Format.GetString(PlanPcsSum), "{0:#,###}");
                }
                if (e.Column.FieldName == "INPUTPCSQTY")
                {
                    e.Info.DisplayText = string.Format(Format.GetString(InputPcsSum), "{0:#,###}");
                }
                if (e.Column.FieldName == "INPUTPNLQTY")
                {
                    e.Info.DisplayText = string.Format(Format.GetString(InputPnlSum), "{0:#,###}");
                }
                if (e.Column.FieldName == "RDSHIPPCSQTY")
                {
                    e.Info.DisplayText = string.Format(Format.GetString(ShipPcsSum), "{0:#,###}");
                }
                if (e.Column.FieldName == "RDSHIPPNLQTY")
                {
                    e.Info.DisplayText = string.Format(Format.GetString(ShipPnlSum), "{0:#,###}");
                }
            }
            else
            {
                grdRelated.View.Columns["WIPPCSQTY"].SummaryItem.DisplayFormat = "  ";
                grdRelated.View.Columns["WIPPNLQTY"].SummaryItem.DisplayFormat = "  ";
                grdRelated.View.Columns["PLANPCSQTY"].SummaryItem.DisplayFormat = "  ";
                grdRelated.View.Columns["PLANPNLQTY"].SummaryItem.DisplayFormat = "  ";
                grdRelated.View.Columns["INPUTPCSQTY"].SummaryItem.DisplayFormat = "  ";
                grdRelated.View.Columns["INPUTPNLQTY"].SummaryItem.DisplayFormat = "  ";
                grdRelated.View.Columns["RDSHIPPCSQTY"].SummaryItem.DisplayFormat = "  ";
                grdRelated.View.Columns["RDSHIPPNLQTY"].SummaryItem.DisplayFormat = "  ";
            }
        }
        #endregion
        #endregion

        #region ▶ ComboBox Event |

        #region - 품목코드 |
        /// <summary>
        /// 품목코드 ComboBox Value Changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboProductDef_EditValueChanged(object sender, EventArgs e)
        {
            if (cboProductDef.Editor.EditValue == null) return;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("PRODUCTDEFID", cboProductDef.EditValue.ToString());

            DataTable dt = SqlExecuter.Query("GetProductDefVersion", "10003", param);

            this.cboProductVersion.DataSource = dt;

            if (dt == null) return;

            this.cboProductVersion.ItemIndex = 0;

            IEnumerable<DataRow> result = (from r in dt.AsEnumerable()
                                         where !string.IsNullOrWhiteSpace(r.Field<string>("PRODUCTDEFID"))
                                         select r
                                         ).ToList()
                                         ;

            DataTable ct = result.CopyToDataTable<DataRow>();

            if (ct == null) return;

            DataRow dr = ct.Rows[0];

            if (dr != null)
            {
                this.txtProductDefName.Editor.Text = dr["PRODUCTDEFNAME"].ToString();
                this.txtCustomer.Editor.Text = dr["CUSTOMERNAME"].ToString();
                this.txtProductType.Editor.Text = dr["PRODUCTIONTYPE"].ToString();
            }
        }
        #endregion

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

        #endregion

        #region ◆ 검색 |

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            Search();
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
            grdRouting.View.ClearDatas();
            grdConsume.View.ClearDatas();
            grdRelated.View.ClearDatas();
            grdDurable.View.ClearDatas();
            grdEquipment.View.ClearDatas();
            grdDefect.View.ClearDatas();
            grdWIP.View.ClearDatas();
            grdLotHist.View.ClearDatas();
        }
        #endregion

        #region ▶ 데이터 조회 |
        /// <summary>
        /// 데이터 조회
        /// </summary>
        private void Search()
        {
            // 기존 Grid Data 초기화
            SetInitControl();

            var values = Conditions.GetValues();

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("P_PRODUCTDEFID", cboProductDef.GetValue());
            param.Add("P_PRODUCTDEFVERSION", this.cboProductVersion.GetDataValue());
            param.Add("P_PRODUCTNAME", values["P_PRODUCTNAME"]);
            param.Add("P_PLANTID", values["P_PLANTID"]);
            param.Add("P_PERIOD_PERIODFR", values["P_PERIOD_PERIODFR"]);
            param.Add("P_PERIOD_PERIODTO", values["P_PERIOD_PERIODTO"]);

            // 데이터 조회
            DataTable dt = SqlExecuter.Query("SelectProductDetailRoutingList", "10001", param);

            if (dt.Rows.Count < 1)
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdRouting.DataSource = dt;

            // 자재 데이터 조회
            getConsumableHistory();

            // 연관 공정 데이터 조회
            getRelatedInfo();
        }
        #endregion

        #region ▶ 연관 공정 현황 |
        /// <summary>
        /// 공정별 자재 사용이력
        /// </summary>
        private void getRelatedInfo()
        {
            var values = Conditions.GetValues();

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("P_PRODUCTDEFID", cboProductDef.GetValue());
            param.Add("P_PRODUCTDEFVERSION", this.cboProductVersion.GetDataValue());
            param.Add("P_PRODUCTNAME", values["P_PRODUCTNAME"]);
            param.Add("P_PLANTID", values["P_PLANTID"]);

            DataTable dt = SqlExecuter.Query("SelectProductDetailRelatedSegment", "10001", param);

            if (dt.Rows.Count < 1)
            {
                return;
            }

            grdRelated.DataSource = dt;
        }

        #endregion

        #region ▶ 자재 사용이력 |
        /// <summary>
        /// 공정별 자재 사용이력
        /// </summary>
        private void getConsumableHistory()
        {
            var values = Conditions.GetValues();
            
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("P_PRODUCTDEFID", cboProductDef.GetValue());
            param.Add("P_PRODUCTDEFVERSION", this.cboProductVersion.GetDataValue());
            param.Add("P_PRODUCTNAME", values["P_PRODUCTNAME"]);
            param.Add("P_PLANTID", values["P_PLANTID"]);

            DataTable dt = SqlExecuter.Query("SelectProductDetailConsumableList", "10001", param);

            if (dt.Rows.Count < 1)
            {
                return;
            }

            grdConsume.DataSource = dt;
        }

        #endregion

        #region ▶ 치공구 사용이력 |
        /// <summary>
        /// 공정별 치공구 사용이력
        /// </summary>
        private void getDurableHistory()
        {
            var values = Conditions.GetValues();

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("P_PRODUCTDEFID", cboProductDef.GetValue());
            param.Add("P_PRODUCTDEFVERSION", this.cboProductVersion.GetDataValue());
            param.Add("P_PRODUCTNAME", values["P_PRODUCTNAME"]);
            param.Add("P_PLANTID", values["P_PLANTID"]);

            DataTable dt = SqlExecuter.Query("SelectProductDetailDurableList", "10001", param);

            if (dt.Rows.Count < 1)
            {
                return;
            }

            grdDurable.DataSource = dt;
        }

        #endregion

        #region ▶ 설비 사용이력 |
        /// <summary>
        /// 공정별 설비 사용이력
        /// </summary>
        private void getEquipmentHistory()
        {
            var values = Conditions.GetValues();

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("P_PRODUCTDEFID", cboProductDef.GetValue());
            param.Add("P_PRODUCTDEFVERSION", this.cboProductVersion.GetDataValue());
            param.Add("P_PRODUCTNAME", values["P_PRODUCTNAME"]);
            param.Add("P_PLANTID", values["P_PLANTID"]);

            DataTable dt = SqlExecuter.Query("SelectProductDetailEquipmentList", "10001", param);

            if (dt.Rows.Count < 1)
            {
                return;
            }

            grdEquipment.DataSource = dt;
        }

        #endregion

        #region ▶ 불량이력 |
        /// <summary>
        /// 공정별 불량이력
        /// </summary>
        private void getDefectHistory()
        {
            var values = Conditions.GetValues();

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("P_PRODUCTDEFID", cboProductDef.GetValue());
            param.Add("P_PRODUCTDEFVERSION", this.cboProductVersion.GetDataValue());
            param.Add("P_PRODUCTNAME", values["P_PRODUCTNAME"]);
            param.Add("P_PLANTID", values["P_PLANTID"]);

            DataTable dt = SqlExecuter.Query("SelectProductDetailDefectList", "10001", param);

            if (dt.Rows.Count < 1)
            {
                return;
            }

            grdDefect.DataSource = dt;
        }

        #endregion

        #region ▶ 재공현황 조회 |
        /// <summary>
        /// 재공현황 조회
        /// </summary>
        /// <param name="strSegmentID"></param>
        private void getWIPList(string strSegmentID)
        {
            var values = Conditions.GetValues();

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("P_PRODUCTDEFID", cboProductDef.GetValue());
            param.Add("P_PRODUCTDEFVERSION", this.cboProductVersion.GetDataValue());
            //param.Add("P_PRODUCTNAME", values["P_PRODUCTNAME"]);
            param.Add("P_PLANTID", values["P_PLANTID"]);
            param.Add("P_PROCESSSEGMENTID", strSegmentID);

            //DataTable dt = SqlExecuter.Query("SelectWIPList", "10002", param);
            DataTable dt = SqlExecuter.Query("SelectWIPListCommon", "10001", param);

            if (dt.Rows.Count < 1)
            {
                return;
            }

            grdWIP.DataSource = dt;
        }
        #endregion

        #region ▶ LOT 생산이력 조회 |
        /// <summary>
        /// LOT 생산이력 조회
        /// </summary>
        /// <param name="lotID"></param>
        private void getLotHistory(string lotID)
        {
            var values = Conditions.GetValues();

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("P_PRODUCTDEFID", cboProductDef.GetValue());
            param.Add("P_PRODUCTDEFVERSION", this.cboProductVersion.GetDataValue());
            param.Add("LOTID", lotID);

            DataTable dt = SqlExecuter.Query("SelectLotWorkHistoryList", "10001", param);

            if (dt.Rows.Count < 1)
            {
                return;
            }

            grdLotHist.DataSource = dt;
        }
        #endregion

        #region ▶ 공정실적 조회 |
        /// <summary>
        /// 재공현황 조회
        /// </summary>
        /// <param name="strSegmentID"></param>
        private void getResultList(string strSegmentID)
        {
            var values = Conditions.GetValues();

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("TYPE", "BYLOT"); 
            param.Add("P_PRODUCTDEFID", cboProductDef.GetValue());
            param.Add("P_PRODUCTDEFVERSION", this.cboProductVersion.GetDataValue());
            param.Add("P_PRODUCTNAME", values["P_PRODUCTNAME"]);
            param.Add("P_PLANTID", values["P_PLANTID"]);
            param.Add("P_PROCESSSEGMENTID", strSegmentID);
            param.Add("P_PERIOD_PERIODFR", values["P_PERIOD_PERIODFR"]);
            param.Add("P_PERIOD_PERIODTO", values["P_PERIOD_PERIODTO"]);
            //param.Add("P_SEARCHDATETO", values["P_SEARCHDATE"]);

            DataTable dt = SqlExecuter.Query("SelectProductDetailWorkResult", "10001", param);

            if (dt.Rows.Count < 1)
            {
                return;
            }

            grdResult.DataSource = dt;
        }
        #endregion

        #endregion
    }
}
