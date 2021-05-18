#region using
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Grid;
using Micube.Framework.SmartControls.Grid.BandedGrid;
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

using DevExpress.XtraGrid.Views.Base;
#endregion

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 공정관리 > 공정작업 > Lot 불량처리
    /// 업  무  설  명  : Lot 불량처리
    /// 생    성    자  : 박정훈
    /// 생    성    일  : 2019-08-08
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class LotDefect : SmartConditionManualBaseForm
    {
        #region ◆ Local Variables |
        private int panelPerQty = 0;
        private int panelQty = 0;
        private int qty = 0;
        #endregion

        #region ◆ 생성자 |
        /// <summary>
        /// 생성자
        /// </summary>
        public LotDefect()
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
            InitializeControls();
        }
        #endregion

        #region ◆ 컨텐츠 영역 초기화 |
        /// <summary>
        /// Control 설정
        /// </summary>
        protected override void InitializeContent()
        {
            base.InitializeContent();

            this.ucDataUpDownBtnCtrl.SourceGrid = this.grdWIP;
            this.ucDataUpDownBtnCtrl.TargetGrid = this.grdTargetAll;

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
            CommonFunction.AddConditionLotPopup("P_LOTID", 0.1, true, Conditions);
            // 품목
            CommonFunction.AddConditionProductPopup("P_PRODUCTDEFID", 0.5, true, Conditions);
            // 작업장
            CommonFunction.AddConditionAreaByAuthorityPopup("P_AREAID", 1.5, false, Conditions, false, true);
        }

        /// <summary>
        /// 조회조건 설정
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            Conditions.GetControl<SmartComboBox>("P_LOTPRODUCTTYPESTATUS").EditValue = "Production";
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
            #region - 재공 Grid 설정 |
            grdWIP.GridButtonItem = GridButtonItem.None;

            grdWIP.View.SetIsReadOnly();

            // CheckBox 설정
            this.grdWIP.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            var groupDefaultCol = grdWIP.View.AddGroupColumn("WIPLIST");
            groupDefaultCol.AddTextBoxColumn("LOTTYPE", 70).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("PRODUCTDEFID", 150).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("PRODUCTDEFNAME", 300);
            groupDefaultCol.AddTextBoxColumn("USERSEQUENCE", 70).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
            groupDefaultCol.AddTextBoxColumn("PLANTID", 70).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("AREANAME", 150);
            groupDefaultCol.AddTextBoxColumn("RTRSHT", 70).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("LOTID", 200).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("UNIT", 60).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("ISLOCKING", 60).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("ISHOLD", 60).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("PROCESSSTATE", 60).SetTextAlignment(TextAlignment.Center).SetIsHidden();

            var groupWipCol = grdWIP.View.AddGroupColumn("WIPQTY");
            groupWipCol.AddTextBoxColumn("QTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            groupWipCol.AddTextBoxColumn("PANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            groupWipCol.AddTextBoxColumn("PANELPERQTY", 80).SetIsHidden();

            var groupReceiveCol = grdWIP.View.AddGroupColumn("ACCEPT");
            groupReceiveCol.AddTextBoxColumn("RECEIVEPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            groupReceiveCol.AddTextBoxColumn("RECEIVEPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");

            var groupWorkStartCol = grdWIP.View.AddGroupColumn("WIPSTARTQTY");
            groupWorkStartCol.AddTextBoxColumn("WORKSTARTPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            groupWorkStartCol.AddTextBoxColumn("WORKSTARTPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");

            var groupWorkEndCol = grdWIP.View.AddGroupColumn("WIPENDQTY");
            groupWorkEndCol.AddTextBoxColumn("WORKENDPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            groupWorkEndCol.AddTextBoxColumn("WORKENDPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");

            var groupSendCol = grdWIP.View.AddGroupColumn("WIPSENDQTY");
            groupSendCol.AddTextBoxColumn("SENDPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            groupSendCol.AddTextBoxColumn("SENDPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");

            var groupWIPCol = grdWIP.View.AddGroupColumn("WIPLIST");
            groupWIPCol.AddTextBoxColumn("LEADTIME", 80).SetTextAlignment(TextAlignment.Right);
            groupWIPCol.AddTextBoxColumn("LOTINPUTDATE", 150).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("{0:yyyy-MM-dd HH:mm:ss}");
            groupWIPCol.AddTextBoxColumn("DELIVERYDATE", 150).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("{0:yyyy-MM-dd HH:mm:ss}");
            groupWIPCol.AddTextBoxColumn("LEFTDATE", 80).SetTextAlignment(TextAlignment.Center);

            groupWIPCol.AddTextBoxColumn("PCSARY", 80).SetTextAlignment(TextAlignment.Center).SetIsHidden();
            groupWIPCol.AddTextBoxColumn("PROCESSUOM", 80).SetTextAlignment(TextAlignment.Center).SetIsHidden();
            groupWIPCol.AddTextBoxColumn("PCSPNL", 80).SetTextAlignment(TextAlignment.Center).SetIsHidden();

            grdWIP.View.PopulateColumns();

            #endregion

            #region - Target All Grid |
            grdTargetAll.GridButtonItem = GridButtonItem.None;

            grdTargetAll.View.SetIsReadOnly();

            // CheckBox 설정
            this.grdTargetAll.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdTargetAll.View.AddTextBoxColumn("LOTID", 200).SetTextAlignment(TextAlignment.Center);
            grdTargetAll.View.AddTextBoxColumn("USERSEQUENCE", 70).SetTextAlignment(TextAlignment.Center);
            grdTargetAll.View.AddTextBoxColumn("AREANAME", 150);

            grdTargetAll.View.AddTextBoxColumn("QTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            grdTargetAll.View.AddTextBoxColumn("PANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");

            grdTargetAll.View.AddTextBoxColumn("LOTTYPE", 70).SetTextAlignment(TextAlignment.Center);
            grdTargetAll.View.AddTextBoxColumn("PRODUCTDEFID", 150).SetTextAlignment(TextAlignment.Center);
            grdTargetAll.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetTextAlignment(TextAlignment.Center);
            grdTargetAll.View.AddTextBoxColumn("PRODUCTDEFNAME", 300);
            grdTargetAll.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
            grdTargetAll.View.AddTextBoxColumn("PLANTID", 70).SetTextAlignment(TextAlignment.Center);
            grdTargetAll.View.AddTextBoxColumn("RTRSHT", 70).SetTextAlignment(TextAlignment.Center);

            grdTargetAll.View.AddTextBoxColumn("UNIT", 60).SetTextAlignment(TextAlignment.Center);
            grdTargetAll.View.AddTextBoxColumn("ISLOCKING", 60).SetTextAlignment(TextAlignment.Center);
            grdTargetAll.View.AddTextBoxColumn("ISHOLD", 60).SetTextAlignment(TextAlignment.Center);
            grdTargetAll.View.AddTextBoxColumn("PROCESSSTATE", 60).SetTextAlignment(TextAlignment.Center).SetIsHidden();

            grdTargetAll.View.AddTextBoxColumn("RECEIVEPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetIsHidden().SetDisplayFormat("{0:#,###}");
            grdTargetAll.View.AddTextBoxColumn("RECEIVEPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetIsHidden().SetDisplayFormat("{0:#,###}");

            grdTargetAll.View.AddTextBoxColumn("WORKSTARTPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetIsHidden().SetDisplayFormat("{0:#,###}");
            grdTargetAll.View.AddTextBoxColumn("WORKSTARTPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetIsHidden().SetDisplayFormat("{0:#,###}");

            grdTargetAll.View.AddTextBoxColumn("WORKENDPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetIsHidden().SetDisplayFormat("{0:#,###}");
            grdTargetAll.View.AddTextBoxColumn("WORKENDPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetIsHidden().SetDisplayFormat("{0:#,###}");

            grdTargetAll.View.AddTextBoxColumn("SENDPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetIsHidden().SetDisplayFormat("{0:#,###}");
            grdTargetAll.View.AddTextBoxColumn("SENDPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetIsHidden().SetDisplayFormat("{0:#,###}");

            grdTargetAll.View.AddTextBoxColumn("LEADTIME", 80).SetTextAlignment(TextAlignment.Right);
            grdTargetAll.View.AddTextBoxColumn("LOTINPUTDATE", 150).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("{0:yyyy-MM-dd HH:mm:ss}");
            grdTargetAll.View.AddTextBoxColumn("DELIVERYDATE", 150).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("{0:yyyy-MM-dd HH:mm:ss}");
            grdTargetAll.View.AddTextBoxColumn("LEFTDATE", 80).SetTextAlignment(TextAlignment.Center);

            grdTargetAll.View.PopulateColumns();
            #endregion

            #region - Target Lot Grid |
            grdTargetLot.GridButtonItem = GridButtonItem.None;

            grdTargetLot.View.SetIsReadOnly();

            // CheckBox 설정
            this.grdTargetLot.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdTargetLot.View.AddTextBoxColumn("LOTID", 200).SetTextAlignment(TextAlignment.Center);
            grdTargetLot.View.AddTextBoxColumn("USERSEQUENCE", 70).SetTextAlignment(TextAlignment.Center);
            grdTargetLot.View.AddTextBoxColumn("AREANAME", 150);

            grdTargetLot.View.AddTextBoxColumn("QTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            grdTargetLot.View.AddTextBoxColumn("PANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            grdTargetLot.View.AddTextBoxColumn("PANELPERQTY", 80).SetIsHidden();

            grdTargetLot.View.AddTextBoxColumn("LOTTYPE", 70).SetTextAlignment(TextAlignment.Center);
            grdTargetLot.View.AddTextBoxColumn("PRODUCTDEFID", 150).SetTextAlignment(TextAlignment.Center);
            grdTargetLot.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetTextAlignment(TextAlignment.Center);
            grdTargetLot.View.AddTextBoxColumn("PRODUCTDEFNAME", 300);
            grdTargetLot.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
            grdTargetLot.View.AddTextBoxColumn("PLANTID", 70).SetTextAlignment(TextAlignment.Center);
            grdTargetLot.View.AddTextBoxColumn("RTRSHT", 70).SetTextAlignment(TextAlignment.Center);

            grdTargetLot.View.AddTextBoxColumn("UNIT", 60).SetTextAlignment(TextAlignment.Center);
            grdTargetLot.View.AddTextBoxColumn("ISLOCKING", 60).SetTextAlignment(TextAlignment.Center);
            grdTargetLot.View.AddTextBoxColumn("ISHOLD", 60).SetTextAlignment(TextAlignment.Center);
            grdTargetLot.View.AddTextBoxColumn("PROCESSSTATE", 60).SetTextAlignment(TextAlignment.Center).SetIsHidden();

            grdTargetLot.View.AddTextBoxColumn("RECEIVEPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetIsHidden().SetDisplayFormat("{0:#,###}");
            grdTargetLot.View.AddTextBoxColumn("RECEIVEPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetIsHidden().SetDisplayFormat("{0:#,###}");

            grdTargetLot.View.AddTextBoxColumn("WORKSTARTPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetIsHidden().SetDisplayFormat("{0:#,###}");
            grdTargetLot.View.AddTextBoxColumn("WORKSTARTPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetIsHidden().SetDisplayFormat("{0:#,###}");

            grdTargetLot.View.AddTextBoxColumn("WORKENDPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetIsHidden().SetDisplayFormat("{0:#,###}");
            grdTargetLot.View.AddTextBoxColumn("WORKENDPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetIsHidden().SetDisplayFormat("{0:#,###}");

            grdTargetLot.View.AddTextBoxColumn("SENDPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetIsHidden().SetDisplayFormat("{0:#,###}");
            grdTargetLot.View.AddTextBoxColumn("SENDPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetIsHidden().SetDisplayFormat("{0:#,###}");

            grdTargetLot.View.AddTextBoxColumn("LEADTIME", 80).SetTextAlignment(TextAlignment.Right);
            grdTargetLot.View.AddTextBoxColumn("LOTINPUTDATE", 150).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("{0:yyyy-MM-dd HH:mm:ss}");
            grdTargetLot.View.AddTextBoxColumn("DELIVERYDATE", 150).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("{0:yyyy-MM-dd HH:mm:ss}");

            grdTargetLot.View.AddTextBoxColumn("PCSARY", 80).SetTextAlignment(TextAlignment.Center).SetIsHidden(); ;
            grdTargetLot.View.AddTextBoxColumn("PROCESSUOM", 80).SetTextAlignment(TextAlignment.Center).SetIsHidden(); ;
            grdTargetLot.View.AddTextBoxColumn("PCSPNL", 80).SetTextAlignment(TextAlignment.Center).SetIsHidden();

            grdTargetLot.View.PopulateColumns();
            #endregion

            #region - Defect List Gird |
            defectList.VisibleLotId = false;
            defectList.InitializeControls();
            defectList.InitializeEvent();
            #endregion
        }
        #endregion

        #region ▶ 화면 Control 설정 |
        /// <summary>
        /// 화면 Control 설정
        /// </summary>
        private void InitializeControls()
        {
            #region - 불량코드 CodeHelp |
            // 불량코드
            ConditionItemSelectPopup workerCondition = new ConditionItemSelectPopup();
            workerCondition.Id = "DEFECTCODE";

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            workerCondition.SearchQuery = new SqlQuery("GetDefectCodeList", "10001", param);
            workerCondition.ValueFieldName = "DEFECTCODE";
            workerCondition.DisplayFieldName = "DEFECTCODENAME";
            workerCondition.SetPopupLayout("DEFECTCODENAME", PopupButtonStyles.Ok_Cancel, true, false);
            workerCondition.SetPopupResultCount(1);
            workerCondition.SetPopupLayoutForm(700, 800, FormBorderStyle.FixedToolWindow);
            workerCondition.SetPopupAutoFillColumns("DEFECTCODENAME");

            // 팝업에서 사용되는 검색조건 (불량코드명)
            workerCondition.Conditions.AddTextBox("TXTDEFECTCODENAME");

            // 팝업 그리드에서 보여줄 컬럼 정의
            // 불량코드
            workerCondition.GridColumns.AddTextBoxColumn("DEFECTCODE", 150);
            // 불량코드명
            workerCondition.GridColumns.AddTextBoxColumn("DEFECTCODENAME", 200);
            // 품질공정 ID
            workerCondition.GridColumns.AddTextBoxColumn("QCSEGMENTID", 150);
            // 품질공정명
            workerCondition.GridColumns.AddTextBoxColumn("QCSEGMENTNAME", 200);

            txtDefectCode.Editor.SelectPopupCondition = workerCondition;

            Dictionary<string, object> uomParam = new Dictionary<string, object>();
            uomParam.Add("UOMCLASSID", "Process");


            // UOM
            cboUOM2.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboUOM2.ShowHeader = false;
            cboUOM2.ValueMember = "UOMDEFID";
            cboUOM2.DisplayMember = "UOMDEFNAME";
            cboUOM2.UseEmptyItem = true;
            cboUOM2.EmptyItemValue = "";
            cboUOM2.EmptyItemCaption = "";
            cboUOM2.DataSource = SqlExecuter.Query("GetUomDefinitionList", "10001", uomParam);

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
            this.Load += Form_Load;

            grdWIP.View.CheckStateChanged += View_CheckStateChanged;
            grdWIP.View.DoubleClick += WIP_DoubleClick;
            grdWIP.View.RowStyle += WIP_RowStyle;

            defectList.View.CustomDrawFooterCell += View_CustomDrawFooterCell;
            defectList.View.AddingNewRow += DefectView_AddingNewRow;

            this.grdTargetLot.View.DoubleClick += TargetLotView_DoubleClick;

            // Tab Event
            tabDefect.SelectedPageChanged += TabDefect_SelectedPageChanged;
            // Button Click Event
            this.ucDataUpDownBtnCtrl.buttonClick += UcDataUpDownBtnCtrl_buttonClick;

            cboUOM2.EditValueChanged += CboUOM2_EditValueChanged;


        }

        private void CboUOM2_EditValueChanged(object sender, EventArgs e)
        {

            string uom = Format.GetString(cboUOM2.EditValue);

            if (uom.Equals("PNL"))
            {
                DataTable dt = defectList.DataSource as DataTable;
                DataTable dt2 = grdTargetLot.DataSource as DataTable;
                defectList.View.Columns["QTY"].OptionsColumn.ReadOnly = true;

                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        decimal pnlper = Format.GetDecimal(Format.GetInteger(dt2.Rows[0]["PANELPERQTY"].ToString()));


                        if (string.IsNullOrWhiteSpace(dr["PNLQTY"].ToString()))
                            return;

                        if (pnlper == 0 || string.IsNullOrWhiteSpace(pnlper.ToString()))
                            return;

                        decimal pnlQty = Format.GetDecimal(dr["PNLQTY"].ToString());

                        decimal qty = pnlper * pnlQty;

                        dr["QTY"] = qty;

                    }
                }


            }
            else
            {
                defectList.View.Columns["QTY"].OptionsColumn.ReadOnly = false;
            }


        }



        #region ▶ Tab Index Changed Event |
        /// <summary>
        /// Tab Index Changed Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabDefect_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (this.ucDataUpDownBtnCtrl.TargetGrid != null) this.ucDataUpDownBtnCtrl.TargetGrid = null;

            if (tabDefect.SelectedTabPageIndex == 0)
                this.ucDataUpDownBtnCtrl.TargetGrid = this.grdTargetAll;
            else
                this.ucDataUpDownBtnCtrl.TargetGrid = this.grdTargetLot;

            //if (defectList.Width < 500)
            //    defectList.Width = tabDefect.Width / 2;
        }
        #endregion

        #region ▶ Grid Event |

        #region - 재공 Grid Check Event |
        /// <summary>
        /// 재공 Grid Check Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CheckStateChanged(object sender, EventArgs e)
        {
            DataTable dt = grdWIP.View.GetCheckedRows();

            if (this.tabDefect.SelectedTabPageIndex == 1 && dt.Rows.Count > 1)
            {
                grdWIP.View.CheckedAll(false);

                // {0}은(는) {1}보다 많을 수 없습니다.
                throw MessageException.Create("SelectOnlyOneLot");
            }
        }
        #endregion

        #region - 재공 Grid Double Click Event |
        /// <summary>
        /// 재공 Grid Double Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WIP_DoubleClick(object sender, EventArgs e)
        {
            // 더블클릭 시 체크박스 체크
            SmartBandedGridView view = (SmartBandedGridView)sender;

            CommonFunction.SetGridDoubleClickCheck(grdWIP, sender);
        }
        #endregion

        #region - 재공 Row Stype Event |
        /// <summary>
        /// 재공 Row Stype Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WIP_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0)
                return;

            bool isChecked = grdWIP.View.IsRowChecked(e.RowHandle);

            if (isChecked)
            {
                e.Appearance.BackColor = Color.FromArgb(254, 254, 16);
                e.HighPriority = true;
            }
        }
        #endregion

        #region - Lot 불량처리 대상 더블클릭시 |
        /// <summary>
        /// Lot 불량처리 대상 더블클릭시
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TargetLotView_DoubleClick(object sender, EventArgs e)
        {
            defectList.View.AddNewRow();
        }
        #endregion

        #region - Defect Footer Sum |
        /// <summary>
        /// Defect Footer Sum
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CustomDrawFooterCell(object sender, DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName == "QTY")
            {

                e.Info.DisplayText = Math.Ceiling(Format.GetDouble(e.Info.Value, 0.00)).ToString();
            }
        }
        #endregion

        #region - Defect Adding New Row |
        /// <summary>
        /// Defect Adding New Row
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void DefectView_AddingNewRow(SmartBandedGridView sender, AddNewRowArgs args)
        {
            DataTable dt = grdTargetLot.DataSource as DataTable;
            if (dt == null || dt.Rows.Count == 0)
            {
                args.IsCancel = true;
                return;
            }
            args.NewRow["LOTID"] = dt.Rows[0]["LOTID"];

            string ProcessUOM = Format.GetTrimString(dt.Rows[0]["PROCESSUOM"]);
            decimal BlkQty = Format.GetDecimal(dt.Rows[0]["PCSARY"]);
            decimal PCSPNL = Format.GetDecimal(dt.Rows[0]["PCSPNL"]);

            if (BlkQty.Equals(0))
            {
                MSGBox.Show(MessageBoxType.Information, Language.GetMessage("NotInputPNLBKL").Message);
                args.IsCancel = true;
                return;
            }

            if (PCSPNL.Equals(0))
            {
                MSGBox.Show(MessageBoxType.Information, Language.GetMessage("NotInputPCSPNL").Message);
                args.IsCancel = true;
                return;
            }

            decimal CalQty = ProcessUOM == "BLK" ? PCSPNL/BlkQty : Format.GetDecimal(dt.Rows[0]["PANELPERQTY"]);


            defectList.SetInfo(CalQty, Format.GetInteger(Format.GetInteger(dt.Rows[0]["QTY"].ToString())));

            //defectList.SetConsumableDefComboBox();
            //defectList.SetInfo(Format.GetInteger(dt.Rows[0]["PANELPERQTY"].ToString()), Format.GetInteger(dt.Rows[0]["QTY"].ToString()));
            defectList.View.SetFocusedRowCellValue("LOTID", dt.Rows[0]["LOTID"]);

            //defectList.SetConsumableDefComboBox();
        }
        #endregion

        #endregion

        #region ▶ ComboBox Event |

        #endregion

        #region ▶ Button Click Event |
        /// <summary>
        /// Up / Down Button Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UcDataUpDownBtnCtrl_buttonClick(object sender, EventArgs e)
        {
            if (this.ucDataUpDownBtnCtrl.ButtonState.Equals("Down"))
            {
                string lotId = Format.GetString(grdWIP.View.GetFocusedRowCellValue("LOTID"));


                var values = Conditions.GetValues();
                values.Add("LOTID", lotId); 

                 DataTable dt2 = SqlExecuter.Query("GetWaitReceiveCheck", "10001", values);


                if (dt2.Rows.Count > 0)
                {
                    throw MessageException.Create("NotDefectStartLot");
                }
                if (tabDefect.SelectedTabPageIndex == 1)
                {
                    defectList.SetConsumableDefComboBox(lotId);

                    setDefectUOMQty();

                    DataTable dt = grdTargetLot.DataSource as DataTable;

                    if (dt == null || dt.Rows.Count <= 0) return;

                    string strLotID = dt.AsEnumerable().Select(r => r.Field<string>("LOTID")).ToList()[0].ToString();

                    if (!string.IsNullOrWhiteSpace(strLotID))
                    {
                        // {0}은(는) {1}보다 많을 수 없습니다.
                        throw MessageException.Create("SelectOnlyOneLot");
                    }



                }
            }
            else if (this.ucDataUpDownBtnCtrl.ButtonState.Equals("Up") && tabDefect.SelectedTabPageIndex == 1)
            {
                if (grdTargetLot.View.GetCheckedRows().Rows.Count > 0)
                {
                    defectList.View.ClearDatas();
                }
            }
        }
        #endregion

        private void setDefectUOMQty()
        {
            //2021.03.05 uom에따른 불량 수량 변경

            DataRow dr = grdWIP.View.GetCheckedRows().AsEnumerable().FirstOrDefault();

            string ProcessUOM = Format.GetTrimString(dr["PROCESSUOM"]);

            decimal BlkQty = Format.GetDecimal(dr["PCSARY"]);
            decimal PCSPNL = Format.GetDecimal(dr["PCSPNL"]);

           if(BlkQty.Equals(0))
            {
                throw MessageException.Create("NotInputPNLBKL");
            }

            if (PCSPNL.Equals(0))
            {
                throw MessageException.Create("NotInputPCSPNL");
            }

            decimal CalQty = ProcessUOM == "BLK" ? PCSPNL/BlkQty : Format.GetDecimal(dr["PANELPERQTY"]);


            defectList.SetInfo(CalQty, Format.GetInteger(Format.GetInteger(dr["QTY"].ToString())));


            setDefectInputType(ProcessUOM);
            defectList.SetInfo(CalQty, Format.GetInteger(Format.GetInteger(dr["QTY"].ToString())));

        }
        private void setDefectInputType(string processUOM)
        {

            Color copyColor = defectList.View.Columns["DECISIONDEGREENAME"].AppearanceHeader.ForeColor;

            if (!processUOM.Equals("PCS"))
            {
                defectList.PcsSetting = true;
                defectList.View.GetConditions().GetCondition("PNLQTY").IsRequired = true;
                defectList.View.GetConditions().GetCondition("QTY").IsRequired = false;
                defectList.View.Columns["PNLQTY"].AppearanceHeader.ForeColor = Color.Red;
                defectList.View.Columns["QTY"].AppearanceHeader.ForeColor = copyColor;
            }
            else
            {
                defectList.PcsSetting = false;
                defectList.View.GetConditions().GetCondition("PNLQTY").IsRequired = false;
                defectList.View.GetConditions().GetCondition("QTY").IsRequired = true;
                defectList.View.Columns["QTY"].AppearanceHeader.ForeColor = Color.Red;
                defectList.View.Columns["PNLQTY"].AppearanceHeader.ForeColor = copyColor;
            }

            string ColCaption = processUOM == "BLK" ? Language.Get("QTY") + "(" + "BLK" + ")" : Language.Get("QTY").ToString() + "(" + "PNL" + ")";
            defectList.View.Columns["PNLQTY"].Caption = ColCaption;
            defectList.View.Columns["PNLQTY"].ToolTip = ColCaption;

            defectList.DefectUOM = processUOM;
            defectList.setDefectTextBox(processUOM);

            cboUOM2.EditValue = processUOM;
            cboUOM2.ReadOnly = true;
        }
        #endregion

        #region ◆ 툴바 |

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            // 재공실사 진행 여부 체크
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            param.Add("PLANTID", UserInfo.Current.Plant);

            DataTable isWipSurveyResult = SqlExecuter.Query("GetPlantIsWipSurvey", "10001", param);

            if (isWipSurveyResult.Rows.Count > 0)
            {
                DataRow row = isWipSurveyResult.AsEnumerable().FirstOrDefault();

                string isWipSurvey = Format.GetString(row["ISWIPSURVEY"]);

                if (isWipSurvey == "Y")
                {
                    // 재공실사가 진행 중 입니다. {0}을 진행할 수 없습니다.
                    ShowMessage("PLANTINWIPSURVEY", Language.Get(string.Join("_", "MENU", MenuId)));

                    return;
                }
            }

            if (tabDefect.SelectedTabPageIndex == 0)
                SaveDefectAll();
            else if (tabDefect.SelectedTabPageIndex == 1)
                SaveDefectLot();

            // 데이터 초기화
            SetInitControl();
        }

        #region ▶ 전체 불량 처리 |
        /// <summary>
        /// 전체 불량 처리
        /// </summary>
        private void SaveDefectAll()
        {
            // TODO : 저장 Rule 변경
            DataTable targetList = grdTargetAll.DataSource as DataTable;

            if (targetList == null || targetList.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }

            if (txtDefectCode.EditValue == null || string.IsNullOrWhiteSpace(txtDefectCode.EditValue.ToString()))
            {
                // 불량코드를 선택하여 주십시오.
                throw MessageException.Create("NoDefectCode");
            }

            string strDefectCode = txtDefectCode.Editor.SelectedData.FirstOrDefault()["DEFECTCODE"].ToString();
            string strQcSegmentId = txtDefectCode.Editor.SelectedData.FirstOrDefault()["QCSEGMENTID"].ToString();

            MessageWorker worker = new MessageWorker("SaveLotDefect");
            worker.SetBody(new MessageBody()
            {
                { "TransactionType", "DefectAll" },
                { "EnterpriseID", UserInfo.Current.Enterprise },
                { "PlantID", UserInfo.Current.Plant },
                { "UserId", UserInfo.Current.Id },
                { "DefectCode",  strDefectCode},
                { "QcSegmentId", strQcSegmentId },
                { "Lotlist", targetList }
            });

            worker.Execute();
        }
        #endregion

        #region ▶ Lot별 불량 처리 |
        /// <summary>
        /// Lot별 불량 처리
        /// </summary>
        private void SaveDefectLot()
        {
            // TODO : 저장 Rule 변경
            DataTable targetList = grdTargetLot.DataSource as DataTable;
            DataTable defectlist = this.defectList.DataSource as DataTable;

            if (targetList == null || targetList.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }

            if (defectlist == null || defectlist.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
            //     int defectQty = 0;
            int defectQty = defectlist.AsEnumerable().Sum(r => r.Field<Decimal>("QTY")).ToInt32();
            /*
            foreach (DataRow dr in defectlist.Rows)
            {
                defectQty += Convert.ToInt32(dr["QTY"].ToString());


            }*/


            if (defectQty <= 0)
            {
                // 불량수량은 0이상이어야 합니다.
                throw MessageException.Create("DefectQtyValidation");
            }

            if (string.IsNullOrEmpty((string)cboUOM2.EditValue))
            {
                // UOM을 선택하여 주십시오.
                throw MessageException.Create("SelectUOM");
            }

            MessageWorker worker = new MessageWorker("SaveLotDefect");
            worker.SetBody(new MessageBody()
            {
                { "TransactionType", "DefectLot" },
                { "EnterpriseID", UserInfo.Current.Enterprise },
                { "PlantID", UserInfo.Current.Plant },
                { "UserId", UserInfo.Current.Id },
                { "Lotlist", targetList },
                { "DefectQty", defectQty },
                { "DefectUom", cboUOM2.EditValue.ToString() },
                { "Defectlist", defectlist }
            });

            worker.Execute();
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

            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable dt = await SqlExecuter.QueryAsync("SelectWIPList", "10005", values);

            if (dt.Rows.Count < 1)
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdWIP.DataSource = dt;
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
            this.grdWIP.View.ClearDatas();
            this.grdTargetAll.View.ClearDatas();
            this.grdTargetLot.View.ClearDatas();
            this.defectList.View.ClearDatas();

            this.txtDefectCode.EditValue = string.Empty;
        }
        #endregion

        #endregion
    }
}
