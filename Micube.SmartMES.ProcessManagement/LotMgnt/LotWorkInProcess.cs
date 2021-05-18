#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.BandedGrid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Menu;
using DevExpress.Utils.Menu;
using Micube.SmartMES.Commons.Controls;
#endregion

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 공정관리 > 재공관리 > 재공 조회
    /// 업  무  설  명  : 재공 조회
    /// 생    성    자  : 배선용
    /// 생    성    일  : 2019-07-11
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class LotWorkInProcess : SmartConditionManualBaseForm
    {
        #region ◆ Local Variables |
        DXMenuItem _myContextMenu1, _myContextMenu2, _myContextMenu3, _myContextMenu4, _myContextMenu5, _myContextMenu6, _myContextMenu7;
        #endregion

        #region ◆ 생성자 |
        /// <summary>
        /// 생성자
        /// </summary>
        public LotWorkInProcess()
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
            InitializationSummaryRow();
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

            // Lot
            CommonFunction.AddConditionLotPopup("P_LOTID", 2.5, true, Conditions);
            // 품목
            CommonFunction.AddConditionProductPopup("P_PRODUCTDEFID", 0.1, false, Conditions, "PRODUCTDEFID");
            //공정
            InitializeConditionProcessSegmentId_Popup();

            // 고객사
            CommonFunction.AddConditionCustomerPopup("P_CUSTOMER", 3.1, false, Conditions); 
            // 작업장
            CommonFunction.AddConditionAreaByAuthorityPopup("P_AREAID", 3.2, true, Conditions, false, false);

            Conditions.GetCondition<ConditionItemSelectPopup>("P_PRODUCTDEFID").SetPopupApplySelection((selectedRows, dataGridRows) =>
            {
                string productDefName = Format.GetString(selectedRows.FirstOrDefault()["PRODUCTDEFNAME"]);

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

                Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").Text = productDefName;
            });
        }

        /// <summary>
        /// 조회조건 설정
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            Conditions.GetControl<SmartComboBox>("P_PLANTID").EditValueChanged += LotWorkInProcess_EditValueChanged;
            Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").EditValueChanged += ProductdefIDChanged;

            Conditions.GetControl<SmartComboBox>("P_PRODUCTDEFTYPE").EditValue = "Product";
            Conditions.GetControl<SmartComboBox>("P_Hold").EditValue = "N";

            setInitAreaByAuthority();
            //p_productDefType
        }

        private void setInitAreaByAuthority()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            
            param.Add("PLANTID", UserInfo.Current.Plant);
            
            DataTable dt = SqlExecuter.Query("GetMyArea", "10001", param);

            if(dt != null && dt.Rows.Count.Equals(1))
            {
                Conditions.GetControl<SmartSelectPopupEdit>("P_AREAID").SetValue(Format.GetTrimString(dt.Rows[0]["AREAID"]));
                Conditions.GetControl<SmartSelectPopupEdit>("P_AREAID").Text = Format.GetTrimString(dt.Rows[0]["AREANAME"]);

            }
        }


        private void ProductdefIDChanged(object sender, EventArgs e)
        {
            SmartSelectPopupEdit PopProdutid = sender as SmartSelectPopupEdit;

            if(Format.GetFullTrimString(PopProdutid.EditValue).Equals(string.Empty))
            {
                Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").Text = string.Empty;
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

        private void InitializationSummaryRow()
        {
            grdWIP.View.Columns["PRODUCTDEFNAME"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdWIP.View.Columns["PRODUCTDEFNAME"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));

            grdWIP.View.Columns["LOTID"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            grdWIP.View.Columns["LOTID"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdWIP.View.Columns["WIPTOTALPCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdWIP.View.Columns["WIPTOTALPCS"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdWIP.View.Columns["WIPTOTALPNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdWIP.View.Columns["WIPTOTALPNL"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdWIP.View.Columns["SENDPCSQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdWIP.View.Columns["SENDPCSQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdWIP.View.Columns["SENDPANELQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdWIP.View.Columns["SENDPANELQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdWIP.View.Columns["RECEIVEPCSQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdWIP.View.Columns["RECEIVEPCSQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdWIP.View.Columns["RECEIVEPANELQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdWIP.View.Columns["RECEIVEPANELQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdWIP.View.Columns["WORKSTARTPCSQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdWIP.View.Columns["WORKSTARTPCSQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdWIP.View.Columns["WORKSTARTPANELQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdWIP.View.Columns["WORKSTARTPANELQTY"].SummaryItem.DisplayFormat = "{0:###,###}";


            grdWIP.View.Columns["WORKSTARTPCSQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdWIP.View.Columns["WORKSTARTPCSQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdWIP.View.Columns["WORKENDPCSQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdWIP.View.Columns["WORKENDPCSQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdWIP.View.Columns["WORKENDPANELQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdWIP.View.Columns["WORKENDPANELQTY"].SummaryItem.DisplayFormat = "{0:###,###}";


            grdWIP.View.OptionsView.ShowFooter = true;
            grdWIP.ShowStatusBar = false;
        }

        #region ▶ Grid 설정 |
        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            #region - 재공 Grid 설정 |
            grdWIP.GridButtonItem = GridButtonItem.Export;

            grdWIP.View.SetIsReadOnly();
            grdWIP.View.UseBandHeaderOnly = true;
            grdWIP.View.DateTimeColumnTextByTimeZone = true;
            // CheckBox 설정

            //siteid
            grdWIP.View.AddTextBoxColumn("PLANTID", 40)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("PLANT");
            //양산구분
            grdWIP.View.AddTextBoxColumn("LOTTYPE", 50)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("TYPE");
            //재작업 구분
            grdWIP.View.AddTextBoxColumn("PROCESSCLASSID_R", 50)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("TXNNAME");
            grdWIP.View.AddTextBoxColumn("REWORKDIVISION", 50)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsHidden();
            //품목코드
            grdWIP.View.AddTextBoxColumn("PRODUCTDEFID", 100)
                .SetTextAlignment(TextAlignment.Center);
            //품목 rev
            grdWIP.View.AddTextBoxColumn("PRODUCTREVISION", 40)
                .SetTextAlignment(TextAlignment.Center);
            //품목명
            grdWIP.View.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            grdWIP.View.AddTextBoxColumn("PRODUCTDEFTYPE", 200)
                .SetIsHidden();
            grdWIP.View.AddTextBoxColumn("PRODUCTIONTYPE", 200)
                .SetIsHidden();
            grdWIP.View.AddTextBoxColumn("UNIT", 200)
                .SetIsHidden();

            //Lot Id
            grdWIP.View.AddTextBoxColumn("LOTID", 160)
                .SetTextAlignment(TextAlignment.Center);
            //공정수순
            grdWIP.View.AddTextBoxColumn("USERSEQUENCE", 50)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("USERSEQUENCE_WORDWRAP")
                .SetIsCaptionWordWrap();
            //공정명
            grdWIP.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 120);
            //작업장명
            grdWIP.View.AddTextBoxColumn("AREANAME", 80)
                .SetTextAlignment(TextAlignment.Center);
            //공정 진행 상태
            grdWIP.View.AddTextBoxColumn("STATE", 70)
                .SetTextAlignment(TextAlignment.Center);


            if (UserInfo.Current.Enterprise.Equals(Constants.EnterPrise_InterFlex))
            {
                grdWIP.View.AddTextBoxColumn("UOM", 60)
                    .SetIsHidden();
            }
            else
            {
                grdWIP.View.AddTextBoxColumn("UOM", 60);
            }

            //SELFINSPECTIONSTAKE 자주검사(입고)
            //SELFINSPECTIONSHIP  자주검사(출하)
            //합계
            var groupWipSum = grdWIP.View.AddGroupColumn("SUM");
            //PCS
            groupWipSum.AddSpinEditColumn("WIPTOTALPCS", 80)
                .SetTextAlignment(TextAlignment.Right);
            //Panel 수량
            groupWipSum.AddSpinEditColumn("WIPTOTALPNL", 70)
                .SetTextAlignment(TextAlignment.Right);

            //인수대기
            var groupSendCol = grdWIP.View.AddGroupColumn("WAITFORRECEIVE");
            groupSendCol.AddSpinEditColumn("SENDPCSQTY", 50)
                .SetTextAlignment(TextAlignment.Right);
            groupSendCol.AddSpinEditColumn("SENDPANELQTY", 50)
                .SetTextAlignment(TextAlignment.Right);

            //입고검사
            grdWIP.View.AddTextBoxColumn("SELFTAKEINSPRESULT", 50)
                .SetTextAlignment(TextAlignment.Center).SetIsCaptionWordWrap();

            //인수
            var groupReceiveCol = grdWIP.View.AddGroupColumn("ACCEPT");
            groupReceiveCol.AddSpinEditColumn("RECEIVEPCSQTY", 50)
                .SetTextAlignment(TextAlignment.Right);
            groupReceiveCol.AddSpinEditColumn("RECEIVEPANELQTY", 50)
                .SetTextAlignment(TextAlignment.Right);

            //작업시작
            var groupWorkStartCol = grdWIP.View.AddGroupColumn("WIPSTARTQTY");
            groupWorkStartCol.AddSpinEditColumn("WORKSTARTPCSQTY", 50)
                .SetTextAlignment(TextAlignment.Right);
            groupWorkStartCol.AddSpinEditColumn("WORKSTARTPANELQTY", 50)
                .SetTextAlignment(TextAlignment.Right);

            //작업완료
            var groupWorkEndCol = grdWIP.View.AddGroupColumn("WIPENDQTY");
            groupWorkEndCol.AddSpinEditColumn("WORKENDPCSQTY", 50)
                .SetTextAlignment(TextAlignment.Right);
            groupWorkEndCol.AddSpinEditColumn("WORKENDPANELQTY", 50)
                .SetTextAlignment(TextAlignment.Right);

            //검사 (출고,측정)
            var groupSelfInspectionShip = grdWIP.View.AddGroupColumn("INSPECT");
            groupSelfInspectionShip.AddTextBoxColumn("SELFSHIPINSPRESULT", 50)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("OUTBOUND");
            groupSelfInspectionShip.AddTextBoxColumn("MEASUREINSPRESULT", 50)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("MEASURE");

            //단위
            grdWIP.View.AddTextBoxColumn("UNIT", 50)
                .SetTextAlignment(TextAlignment.Center);
            //공정입고일
            grdWIP.View.AddTextBoxColumn("SEGMENTINCOMETIME", 120)
                .SetTextAlignment(TextAlignment.Center);
            //공정 L/T
            // 2020-12-14 오근영 (43) 엑셀 다운로드 시 형식 변경(문자->숫자)
            grdWIP.View.AddSpinEditColumn("PROCESSSEGMENTLEADTIME", 60)
                .SetDisplayFormat("#,##0.0")
                .SetTextAlignment(TextAlignment.Right)
                .SetLabel("PROCESSSEGMENTLEADTIME_WORDWRAP")
                .SetIsCaptionWordWrap();
            //Lot 투입일
            grdWIP.View.AddTextBoxColumn("LOTINPUTDATE", 120)
                .SetTextAlignment(TextAlignment.Center);
            //전체 L/T
            // 2020-12-14 오근영 (43) 엑셀 다운로드 시 형식 변경(문자->숫자)
            grdWIP.View.AddSpinEditColumn("TOTALLEADTIME", 50)
                .SetDisplayFormat("#,##0.00")
                .SetTextAlignment(TextAlignment.Right)
                .SetLabel("LEADTIME_WORDWRAP")
                .SetIsCaptionWordWrap();
            //납기일
            grdWIP.View.AddTextBoxColumn("DELIVERYDATE", 70)
                .SetDisplayFormat("yyyy-MM-dd")
                .SetTextAlignment(TextAlignment.Center);
            //잔여 일수
            grdWIP.View.AddTextBoxColumn("LEFTDATE", 50)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("LEFTDATE_WORDWRAP")
                .SetIsCaptionWordWrap();
            //예상생산완료일
            grdWIP.View.AddTextBoxColumn("EXPECTPRODUCTDATE", 70)
                .SetDisplayFormat("yyyy-MM-dd")
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("EXPECTPRODUCTDATE_WORDWRAP")
                .SetIsCaptionWordWrap()
                .SetIsHidden();
            //Roll/Sheet
            grdWIP.View.AddTextBoxColumn("RTRSHT", 60)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("RTRSHT_WORDWRAP")
                .SetIsCaptionWordWrap();
            //보류 여부
            grdWIP.View.AddTextBoxColumn("ISHOLD", 50)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("HOLD");
            //Locking여부
            grdWIP.View.AddTextBoxColumn("ISLOCKING", 50)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("LOCKING");
            //예상 수율
            grdWIP.View.AddTextBoxColumn("RESPECTYIELD", 60)
                .SetTextAlignment(TextAlignment.Right);
            //수주
            var groupSalesOrderInfo = grdWIP.View.AddGroupColumn("SALESORDERINFO");
            groupSalesOrderInfo.AddTextBoxColumn("SALESORDERID", 70)
                .SetTextAlignment(TextAlignment.Right);
            groupSalesOrderInfo.AddTextBoxColumn("LINENO", 50)
                .SetTextAlignment(TextAlignment.Right);
            groupSalesOrderInfo.AddTextBoxColumn("PLANQTY", 50).SetLabel("ORDERQTY")
                                .SetTextAlignment(TextAlignment.Right);
            //물류상태
            grdWIP.View.AddTextBoxColumn("TRANSITSTATE", 90)
                .SetTextAlignment(TextAlignment.Center);


            grdWIP.View.PopulateColumns();

            if (UserInfo.Current.Enterprise == Constants.EnterPrise_InterFlex)
            {
                grdWIP.View.FixColumn(new string[] { "PLANTID", "LOTTYPE", "PROCESSCLASSID_R", "PRODUCTDEFID", "PRODUCTREVISION", "PRODUCTDEFNAME", "LOTID", "USERSEQUENCE", "PROCESSSEGMENTNAME" });
            }
            else if (UserInfo.Current.Enterprise == Constants.EnterPrise_YoungPoong)
            {
                grdWIP.View.FixColumn(new string[] { "PLANTID", "LOTTYPE", "PROCESSCLASSID_R", "PRODUCTDEFID", "PRODUCTREVISION", "PRODUCTDEFNAME" });
            }

            //grdWIP.View.FixColumn(new string[] { "PRODUCTDEFNAME" });//, "LOTTYPE", "PROCESSCLASSID", "PRODUCTDEFID", "PRODUCTREVISION", "PRODUCTDEFNAME", "LOTID", "USERSEQUENCE", "PROCESSSEGMENTNAME", "AREANAME", "STATE", "QTY", "PANELQTY" });
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
            //grdWIP.View.MouseMove += View_MouseMove;
            grdWIP.View.CustomDrawFooterCell += View_CustomDrawFooterCell;
            grdWIP.InitContextMenuEvent += GrdWIP_InitContextMenuEvent;
            //grdWIP.View.RowCellStyle += View_RowCellStyle;
            grdWIP.View.RowStyle += View_RowStyle;
            
            
        }

        private void View_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView gr = sender as GridView;
            if (e.RowHandle < 0)
                return;

            DataRow dr = gr.GetDataRow(e.RowHandle);

            if (Format.GetString(dr["ISHOLD"]).Equals("Y") || Format.GetString(dr["ISLOCKING"]).Equals("Y"))
            {
                //if (!gr.IsCellSelected(e.RowHandle, e.Column))
                    e.Appearance.ForeColor = Color.Red;
            }
            else if (Format.GetString(dr["REWORKDIVISION"]).Equals("Rework"))
            {
                //if (!gr.IsCellSelected(e.RowHandle, e.Column))
                    e.Appearance.ForeColor = Color.Blue;
            }
            else if (Format.GetString(dr["PROCESSCLASSID_R"]).Equals("Claim"))
            {
                //if (!gr.IsCellSelected(e.RowHandle, e.Column))
                    e.Appearance.ForeColor = Color.DarkOrange;
            }
        }

        private void View_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            //groupDefaultCol3.AddTextBoxColumn("ISHOLD", 100).SetTextAlignment(TextAlignment.Center);
            //Locking여부
            //groupDefaultCol3.AddTextBoxColumn("ISLOCKING", 100).SetTextAlignment(TextAlignment.Center);
            //REWORKDIVISION
            GridView gr = sender as GridView;
            if (e.RowHandle < 0) return;

            DataRow dr = (DataRow)gr.GetDataRow(e.RowHandle);

            if (Format.GetString(dr["ISHOLD"]).Equals("Y") || Format.GetString(dr["ISLOCKING"]).Equals("Y"))
            {
                if (!gr.IsCellSelected(e.RowHandle, e.Column))
                    e.Appearance.ForeColor = Color.Red;
            }
            else if (Format.GetString(dr["REWORKDIVISION"]).Equals("Rework"))
            {
                if (!gr.IsCellSelected(e.RowHandle, e.Column))
                    e.Appearance.ForeColor = Color.Blue;
            }
            else if (Format.GetString(dr["PROCESSCLASSID_R"]).Equals("Claim"))
            {
                if (!gr.IsCellSelected(e.RowHandle, e.Column))
                    e.Appearance.ForeColor = Color.DarkOrange;
            }
        }

        private void LotWorkInProcess_EditValueChanged(object sender, EventArgs e)
        {
            if(sender is SmartComboBox)
            {
                if (UserInfo.Current.Enterprise == Constants.EnterPrise_InterFlex) return;

                SmartComboBox plant = sender as SmartComboBox;

                string plantid = Format.GetFullTrimString(plant.EditValue);


                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                param.Add("P_PLANTID", plantid);

                DataTable dt = SqlExecuter.Query("SelectOwnerFactory", "10001", param);

                Conditions.GetControl<SmartComboBox>("P_OWNERFACTORY").DataSource = dt;


                if (plant.Name == "P_PLNATID")
                {


                }
            }
        }

        private void GrdWIP_InitContextMenuEvent(SmartBandedGrid sender, Framework.SmartControls.Grid.InitContextMenuEventArgs args)
        {
            args.AddMenus.Add(_myContextMenu1 = new DXMenuItem(Language.Get("MENU_PG-SG-0340"), OpenForm) { BeginGroup = true, Tag = "PG-SG-0340" });
            args.AddMenus.Add(_myContextMenu2 = new DXMenuItem(Language.Get("MENU_PG-SG-0625"), OpenForm) { Tag = "PG-SG-0625" });
            args.AddMenus.Add(_myContextMenu3 = new DXMenuItem(Language.Get("MENU_PG-SG-0450"), OpenForm) { Tag = "PG-SG-0450" });
            args.AddMenus.Add(_myContextMenu4 = new DXMenuItem(Language.Get("MENU_PG-SD-0292"), OpenForm) { Tag = "PG-SD-0292" });
            args.AddMenus.Add(_myContextMenu5 = new DXMenuItem(Language.Get("MENU_PG-SG-0540"), OpenForm) { Tag = "PG-SG-0540" });
            args.AddMenus.Add(_myContextMenu6 = new DXMenuItem(Language.Get("MENU_DefectMapByLot"), OpenForm) { Tag = "DefectMapByLot" });
            args.AddMenus.Add(_myContextMenu7 = new DXMenuItem(Language.Get("MENU_DefectMapByItem"), OpenForm) { Tag = "DefectMapByItem" });
        }
        private void OpenForm(object sender, EventArgs args)
        {
            if (grdWIP.View.FocusedRowHandle < 0)
                return;

            try
            {
                DialogManager.ShowWaitDialog();

                DataRow currentRow = grdWIP.View.GetFocusedDataRow();

                string menuId = (sender as DXMenuItem).Tag.ToString();

                var param = currentRow.Table.Columns
                    .Cast<DataColumn>()
                    .ToDictionary(col => col.ColumnName, col => currentRow[col.ColumnName]);

                OpenMenu(menuId, param); //다른창 호출..
            }
            catch (Exception ex)
            {
                this.ShowError(ex);
            }
            finally
            {
                DialogManager.Close();
            }
        }
        private void View_CustomDrawFooterCell(object sender, FooterCellCustomDrawEventArgs e)
        {
            Font ft = new Font("Mangul Gothic", 10F, FontStyle.Bold);
            e.Appearance.BackColor = Color.LightBlue;
            e.Appearance.FillRectangle(e.Cache, e.Bounds);
            e.Info.AllowDrawBackground = false;
            e.Appearance.Font = ft;
        }

        private void View_MouseMove(object sender, MouseEventArgs e)
        {
           
            int x = grdWIP.View.FocusedRowHandle;

            BandedGridHitInfo hitInfo = new BandedGridHitInfo();
            //GridHitInfo hitInfo = new GridHitInfo();
            hitInfo = grdWIP.View.CalcHitInfo(e.Location);
            
            if(hitInfo.Band == null)
            {
                toolTipWipInfo.HideHint();
                return;
            }
            if(hitInfo.Band.Name != "WAITFORRECEIVE" && hitInfo.Band.Name != "WAIT" && hitInfo.Band.Name != "RUN" && hitInfo.Band.Name != "WAITFORSEND")
            {
                toolTipWipInfo.HideHint();
                return;
            }
            if (hitInfo.InRowCell == true)
            {
                GridColumn column = hitInfo.Column;
                int RowHandle = hitInfo.RowHandle;

                DataTable dt = grdWIP.DataSource as DataTable;

                DataRow dr = dt.Rows[RowHandle];

                string strToolTipMessage = string.Empty;

                switch(hitInfo.Band.Name)
                {

                    case "WAITFORRECEIVE":
                        strToolTipMessage = string.Format(" 대기시간:{0} \r\n 전공정명:{1} \r\n 전작업장명: {2}", dr["WAITFORRECIVE"].ToString(), dr["PREVSEGMENTNAME"].ToString(), dr["PREVAREANAME"].ToString());
                        break;
                    case "WAIT":
                        strToolTipMessage = string.Format(" 경과시간:{0} \r\n 인수설비:{1}", dr["WAITFORRECIVE"].ToString(), dr["RECEIVEEQUIPMENT"].ToString());
                        break;
                    case "RUN":
                        strToolTipMessage = string.Format(" 경과시간:{0} \r\n 인수설비:{1}", dr["TRACKINLEADTIME"].ToString(), dr["STARTEQUIPMENT"].ToString());
                        break;
                    case "WAITFORSEND":
                        strToolTipMessage = string.Format(" 경과시간:{0} \r\n 자주검사:{1} \r\n 계측검사:{2}", dr["TRACKOUTLEADTIME"].ToString(), dr["ISPECTIONCHECK"].ToString(), "");
                        break;
                }

            
                toolTipWipInfo.ShowHint(strToolTipMessage, grdWIP.PointToScreen(new Point(e.X, e.Y)));

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

            // 기존 Grid Data 초기화
            grdWIP.View.ClearDatas();

            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            if(await QueryAsyncDirect("SelectWIPList", "30003", values) is DataTable dt)
            {
                if (dt.Rows.Count < 1)
                {
                    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                }

                grdWIP.DataSource = dt;
            }
        }
        /// <summary>
        /// 팝업형 조회조건 생성 - 공정
        /// </summary>
        private void InitializeConditionProcessSegmentId_Popup()
        {
            var processSegmentIdPopup = this.Conditions.AddSelectPopup("P_ProcessSegmentId", new SqlQuery("GetProcessSegmentList", "10002", $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "PROCESSSEGMENTCLASSTYPE=MiddleProcessSegmentClass"), "PROCESSSEGMENTNAME", "PROCESSSEGMENTID")
                .SetPopupLayout(Language.Get("SELECTPROCESSSEGMENTID"), PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupResultCount(1)
                .SetPopupLayoutForm(800, 800, FormBorderStyle.FixedToolWindow)
                .SetPopupAutoFillColumns("PROCESSSEGMENTNAME")
                .SetLabel("PROCESSSEGMENT")
                .SetPosition(6.1);

            processSegmentIdPopup.Conditions.AddComboBox("PROCESSSEGMENTCLASSID_MIDDLE", new SqlQuery("GetProcessSegmentClassByType", "10001", $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "PROCESSSEGMENTCLASSTYPE=MiddleProcessSegmentClass"), "PROCESSSEGMENTCLASSNAME", "PROCESSSEGMENTCLASSID")
                .SetLabel("MIDDLEPROCESSSEGMENT");
            processSegmentIdPopup.Conditions.AddTextBox("PROCESSSEGMENT");


            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME_TOP", 150)
                .SetLabel("LARGEPROCESSSEGMENT");
            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME_MIDDLE", 150)
                .SetLabel("MIDDLEPROCESSSEGMENT");
            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150);
            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
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
        private DXMenuCheckItem CreateCheckItem(string caption, string menuId)
        {
            DXMenuCheckItem item = new DXMenuCheckItem(caption, false, null, new EventHandler(OnQuickMenuItemClick));
            item.Tag = menuId;
            return item;
        }
        private void OnQuickMenuItemClick(object sender, EventArgs e)
        {
            DXMenuCheckItem item = sender as DXMenuCheckItem;
            string menuId = (string)item.Tag;

            DataRow row = grdWIP.View.GetFocusedDataRow();
            var param = row.Table.Columns
                .Cast<DataColumn>()
                .ToDictionary(col => col.ColumnName, col => row[col.ColumnName]);

            //메뉴 호출			
            OpenMenu(menuId, param);
        }
        #endregion
    }
}
