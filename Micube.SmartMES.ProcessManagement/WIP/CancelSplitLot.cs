#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Grid.BandedGrid;

using Micube.SmartMES.Commons;
using Micube.SmartMES.Commons.Controls;
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
using DevExpress.Utils;
#endregion

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 공정 관리 > 공정작업 > 인수 등록
    /// 업  무  설  명  : 입고 검사 완료된 인수 대기 중인 Lot을 인수 등록 한다.
    /// 생    성    자  : 강호윤
    /// 생    성    일  : 2019-06-27
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    /// 
    
    public partial class CancelSplitLot : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가
        private bool isMessageLoaded = false;
        private string lotId = null;
        #endregion

        #region 생성자

        public CancelSplitLot()
        {
            InitializeComponent();
            InitializeGrid();
            InitializationSummaryRow();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 화면의 컨텐츠 영역을 초기화한다.
        /// </summary>
        protected override void InitializeContent()
        {
            base.InitializeContent();

            UseAutoWaitArea = false;

            InitializeEvent();

            // TODO : 컨트롤 초기화 로직 구성
            InitializeControls();
        }

        private void InitializeControls()
        {
            txtLotId.ImeMode = ImeMode.Alpha;

            grdLotInfo.ColumnCount = 4;
            grdLotInfo.LabelWidthWeight = "40%";
            grdLotInfo.SetInvisibleFields("PROCESSSTATE", "PROCESSPATHID", "PROCESSSEGMENTID", "PROCESSSEGMENTVERSION",
                "NEXTPROCESSSEGMENTID", "NEXTPROCESSSEGMENTVERSION", "NEXTPROCESSSEGMENTTYPE", "PRODUCTDEFVERSION",
                "PRODUCTTYPE", "PRODUCTDEFTYPEID", "LOTTYPE", "ISHOLD",
                "AREANAME", "DEFECTUNIT", "PCSPNL", "PANELPERQTY",
                "PROCESSSEGMENTTYPE", "STEPTYPE", "ISPRINTLOTCARD", "ISPRINTRCLOTCARD",
                "TRACKINUSER", "TRACKINUSERNAME", "MATERIALCLASS", "AREAID",
                "ISRCLOT", "SELFSHIPINSPRESULT", "SELFTAKEINSPRESULT", "MEASUREINSPRESULT",
                "OSPINSPRESULT", "ISBEFOREROLLCUTTING", "PATHTYPE", "LOTSTATE",
                "WAREHOUSEID", "ISWEEKMNG", "DESCRIPTION", "PARENTPROCESSSEGMENTCLASSID",
                "RTRSHT", "PROCESSSEGMENTCLASSID", "RESOURCENAME", "ISCLAIMLOT", "OSMCHECK", "PROCESSUOM", "PCSARY", "BLKQTY", "RESOURCEID" ,
                "WIPPROCESSSTATE"
                );
            grdLotInfo.Enabled = false;
        }
        private void InitializeGrid()
        {
            grdSplitList.GridButtonItem = GridButtonItem.Export;

            grdSplitList.View.SetIsReadOnly();

            //LOT ID
            grdSplitList.View.AddTextBoxColumn("LOTID", 180).SetTextAlignment(TextAlignment.Left).SetLabel("LOTID");
            //공정id
            grdSplitList.View.AddTextBoxColumn("PROCESSSEGMENTID", 100).SetTextAlignment(TextAlignment.Left).SetIsHidden();
            //공정
            grdSplitList.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 100).SetTextAlignment(TextAlignment.Left);
            //공정순서
            grdSplitList.View.AddTextBoxColumn("USERSEQUENCE", 80).SetTextAlignment(TextAlignment.Center);
            //상태
            grdSplitList.View.AddTextBoxColumn("STATE", 80).SetTextAlignment(TextAlignment.Center);
            //작업장
            grdSplitList.View.AddTextBoxColumn("AREANAME", 120).SetTextAlignment(TextAlignment.Center);
            //분할 수량
            grdSplitList.View.AddSpinEditColumn("CHILDLOTQTY", 80).SetTextAlignment(TextAlignment.Right).SetLabel("SPLITQTY");
            //현재 수량
            grdSplitList.View.AddSpinEditColumn("QTY", 80).SetTextAlignment(TextAlignment.Right).SetLabel("CONSUMABLELOTQTY");

            grdSplitList.View.PopulateColumns();

            grdGroup.GridButtonItem = GridButtonItem.None;
            grdGroup.View.SetIsReadOnly();
            grdGroup.View.AddTextBoxColumn("GROUPNO", 180).SetTextAlignment(TextAlignment.Center);
            grdGroup.View.AddTextBoxColumn("GROUPKEY", 180).SetTextAlignment(TextAlignment.Left);
            grdGroup.View.PopulateColumns();

        }
        private void InitializationSummaryRow()
        {

            grdSplitList.View.Columns["LOTID"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            grdSplitList.View.Columns["LOTID"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdSplitList.View.Columns["CHILDLOTQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdSplitList.View.Columns["CHILDLOTQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdSplitList.View.Columns["QTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdSplitList.View.Columns["QTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdSplitList.View.OptionsView.ShowFooter = true;
            grdSplitList.ShowStatusBar = false;
        }
        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {

            txtLotId.Editor.KeyDown += TxtLotId_KeyDown;
            grdSplitList.View.CustomDrawFooterCell += View_CustomDrawFooterCell;
            grdSplitList.View.RowStyle += View_RowStyle;
            grdGroup.View.FocusedRowChanged += View_FocusedRowChanged;
            grdGroup.View.RowCellClick += View_RowCellClick;
        }

        private void View_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            
            if (e.RowHandle < 0) return;

            Dictionary<string, object> param = new Dictionary<string, object>();

            string lotid = Format.GetTrimString(grdLotInfo.GetFieldValue("LOTID"));
            string txnGroupKey = Format.GetTrimString(grdGroup.View.GetRowCellValue(e.RowHandle, "GROUPKEY"));

            param.Add("TXNGROUPKEY", txnGroupKey);
            param.Add("LOTID", lotid);
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable dtSplit = SqlExecuter.Query("SelectSplitChildLotList", "10001", param);
            grdSplitList.DataSource = dtSplit;
        }

        private void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            /*
            if (e.FocusedRowHandle < 0) return;

            Dictionary<string, object> param = new Dictionary<string, object>();

            string lotid = Format.GetTrimString(grdLotInfo.GetFieldValue("LOTID"));
            string txnGroupKey = Format.GetTrimString(grdGroup.View.GetRowCellValue(e.FocusedRowHandle, "GROUPKEY"));

            param.Add("TXNGROUPKEY", txnGroupKey);
            param.Add("LOTID", lotid);
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable dtSplit = SqlExecuter.Query("SelectSplitChildLotList", "10001", param);
            grdSplitList.DataSource = dtSplit;
            */
        }

        private void View_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;

            DataRow dr = grdSplitList.View.GetDataRow(e.RowHandle);

            DataRow drPLot = ((DataTable)grdLotInfo.DataSource).AsEnumerable().FirstOrDefault();

            string SplitProcessSegment = Format.GetTrimString(drPLot["PROCESSSEGMENTID"]);
            string ChildProcessSegment = Format.GetTrimString(dr["PROCESSSEGMENTID"]);

            string SplitState = Format.GetTrimString(drPLot["WIPPROCESSSTATE"]);
            string ChildState = Format.GetTrimString(dr["WIPPROCESSSTATE"]);

            int SpiltQty = Format.GetInteger(dr["CHILDLOTQTY"]);
            int currentQty = Format.GetInteger(dr["QTY"]);

            if(!SplitProcessSegment.Equals(ChildProcessSegment) || !SplitState.Equals(ChildState) || !SpiltQty.Equals(currentQty))
            {
                e.HighPriority = true;
                e.Appearance.BackColor = Color.Red;
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

        private void TxtLotId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var buttons = pnlToolbar.Controls.Find<SmartButton>(true);
                string lotid = txtLotId.Text.Trim();

                ClearInfo();
                if (lotid.Equals(string.Empty))
                {
                    ShowMessage("LotIdIsRequired");
                    txtLotId.Focus();
                    return;
                }
                try
                {
                    buttons.ForEach(button => button.IsBusy = true);
                    pnlContent.ShowWaitArea();

                    Dictionary<string, object> param = new Dictionary<string, object>();
                    param.Add("LOTID", lotid);
                    param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                    // TODO : Query Version 변경
                    DataTable lotInfo = SqlExecuter.Query("SelectLotInfoSplitParentLot", "10001", param);

                    if(lotInfo == null || lotInfo.Rows.Count == 0)
                    {
                        throw MessageException.Create("CheckSplitHistory");
                    }

                    if(Format.GetInteger(lotInfo.Rows[0]["SPLITPCSQTY"]).Equals(0))
                    {
                        throw MessageException.Create("CheckSplitHistory");
                    }

                    grdLotInfo.DataSource = lotInfo;

                    string isHold = Format.GetString(lotInfo.Rows[0]["ISHOLD"]);

                    if (isHold == "Y")
                    {
                        // 보류 상태의 Lot 입니다.
                        ShowMessage("LotIsHold", string.Format("LotId = {0}", txtLotId.Text));

                        txtLotId.Text = "";
                        txtLotId.Focus();

                        return;
                    }
                    grdLotInfo.Enabled = true;

                    DataTable dtSplit = SqlExecuter.Query("SelectSplitTxnGroupKey", "10001", param);
                    grdGroup.DataSource = dtSplit;

                    if (grdGroup.View.RowCount > 0)
                    {
                        DXMouseEventArgs dxm = new DXMouseEventArgs(MouseButtons.Left, 1, 1, 1, 1);
                        RowCellClickEventArgs arg = new RowCellClickEventArgs(dxm, 0, null);
                        View_RowCellClick(null, arg);
                    }
                }
                catch (Exception ex)
                {
                    throw MessageException.Create(ex.ToString());
                }
                finally
                {
                    pnlContent.CloseWaitArea();
                    buttons.ForEach(button => button.IsBusy = false);
                }
            }
        }

      
        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            if (!grdLotInfo.Enabled)
                throw MessageException.Create("NoSaveData");


            if (ShowMessage(MessageBoxButtons.YesNo, "InfoSave") == DialogResult.No) return;

            base.OnToolbarSaveClick();

          
            try
            {
                pnlContent.ShowWaitArea();

                int rowHandle = grdGroup.View.FocusedRowHandle;

                MessageWorker messageWorker = new MessageWorker("SaveCancelLotSplit");
                messageWorker.SetBody(new MessageBody()
                {
                    { "EnterpriseID", UserInfo.Current.Enterprise },
                    { "PlantID", UserInfo.Current.Plant },
                    { "UserId", UserInfo.Current.Id },
                    { "TxnGroupId", grdGroup.View.GetRowCellValue(rowHandle,"GROUPKEY") },
                    { "LotId", grdLotInfo.GetFieldValue("LOTID").ToString() }
                });

                messageWorker.Execute();
            }
            catch (Exception ex)
            {
                pnlContent.CloseWaitArea();
                throw MessageException.Create(ex.Message);
            }

            pnlContent.CloseWaitArea();

            ShowMessage("SuccedSave");

            txtLotId.Text = "";
            ClearInfo();
            txtLotId.Focus();
        }

        #endregion

        #region 검색

        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            // TODO : 조회 SP 변경
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
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

            // 3. 모랏 수량 분할수량 합이 같지 않을때 알림 메시지
            // 4. 모랏, 분할,공정 및 작업 상태가 같지 않을때 알림          
            // split list에 lot이 없을 경우
            DataTable dtLot = grdLotInfo.DataSource as DataTable;
            DataTable dtCLot = grdSplitList.DataSource as DataTable;


            if(dtLot == null || dtLot.Rows.Count ==0 )
            {
                throw MessageException.Create("NoSaveData");
            }

            if (dtCLot == null || dtCLot.Rows.Count == 0)
            {
                throw MessageException.Create("NoSaveData");
            }

            if(grdGroup.View.RowCount == 0)
            {
                throw MessageException.Create("NoSaveData");
            }

            decimal PLotPrevQty = Format.GetDecimal(dtLot.Rows[0]["PRVSPLITQTY"]);  //분할 이전 수량
            decimal PLotQty = Format.GetDecimal(dtLot.Rows[0]["PCSQTY"]); //모랏 수량
            decimal CLotSplitQty = dtCLot.AsEnumerable().Select(s => s.Field<decimal>("CHILDLOTQTY")).Sum(); //자식 분할 수량 합
            decimal CLotCurrentQty = dtCLot.AsEnumerable().Select(s => s.Field<decimal>("QTY")).Sum(); //자식 현재 수량 합

            string PProcess = Format.GetTrimString(dtLot.Rows[0]["PROCESSSEGMENTID"]);
            string PWipprocessstate = Format.GetTrimString(dtLot.Rows[0]["WIPPROCESSSTATE"]);

            /*
            if (!PLotPrevQty.Equals(PLotQty+ CLotCurrentQty))
            {
                //분할전 모Lot 수량과{0} 분할lot의 수량이{1} 맞지 않습니다. CheckSplitQty
                throw MessageException.Create("CheckSplitQty", PLotQty.ToString(), CLotCurrentQty.ToString());
            }
            */
            
            DataRow dr = dtCLot.AsEnumerable().Where(c => !c.Field<decimal>("CHILDLOTQTY").Equals(c.Field<decimal>("QTY"))).FirstOrDefault();
            if(dr != null)

            {
                //CheckChildSplitLotQty 분할 수량과 현수량이 맞지 않습니다.{0}
                throw MessageException.Create("CheckChildSplitLotQty", Format.GetTrimString(dr["LOTID"]));
            }

            dr = dtCLot.AsEnumerable().Where(c => !c.Field<string>("PROCESSSEGMENTID").Equals(PProcess) || !c.Field<string>("WIPPROCESSSTATE").Equals(PWipprocessstate)).FirstOrDefault();
            if (dr != null)

            {
                //모 Lot 의 공정 또는 작업 step이 맞지 않는 분할 Lot이 있습니다, {0}
                throw MessageException.Create("CheckSplitProcessState", Format.GetTrimString(dr["LOTID"]));
            }
        }

        #endregion

        #region Private Function

        private void ClearInfo()
        {
            grdLotInfo.ClearData();
            grdSplitList.DataSource = null;
            grdGroup.DataSource = null;
            //txtLotId.Text = string.Empty;
            txtLotId.Focus();
        }

        // TODO : 화면에서 사용할 내부 함수 추가

        #endregion
    }
}