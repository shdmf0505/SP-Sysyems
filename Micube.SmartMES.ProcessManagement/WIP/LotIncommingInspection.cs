#region using

using DevExpress.Utils;
using DevExpress.Utils.Menu;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Menu;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons;
using System;
using Micube.SmartMES.Commons.Controls;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 공정관리 > 공정작업 > 입고검사등록
    /// 업  무  설  명  : 공정 인수 전 입고검사 결과를 등록하는 화면, 결과에 따라 NCR을 발행
    /// 생    성    자  : 정승원
    /// 생    성    일  : 2019-06-10
    /// 수  정  이  력  : 2019-06-26  수량 변경 화면 이벤트 처리 수정
    ///	               : 2019-12-13/hykang  인수 등록, 인계 등록 화면에서 오픈할 때 자동 조회 로직 추가
    /// 
    /// </summary>
    public partial class LotIncommingInspection : SmartConditionManualBaseForm
    {
        //검사수량 PNL, PCS 수량 변경 이벤트 핸들러
        private delegate double QtyHandler(double inputQty, double stdPcsPnl);

        /// <summary>
        /// 검사수량 PNL, PCS 수량 변경 이벤트
        /// </summary>
        class Quantity
        {
            public event QtyHandler ChangeQtyValue;
            public double Change(double inputQty, double stdPcsPnl)
            {
                return ChangeQtyValue(inputQty, stdPcsPnl);
            }
        }

        #region Local Variables
        //
        List<DXMenuItem> menuList = new List<DXMenuItem>();
        //rule로 보낼 검사 불량 리스트
        private DataTable _dtDefectData = null;
        //rule로 보낼 검사 결과 리스트
        private DataTable _dtResultData = null;

        //자주검사(입고) 메뉴ID
        private const string INSPECT_IN = "PG-SG-0150";
        //자주검사(출고) 메뉴ID
        private const string INSPECT_OUT = "PG-SG-0240";

        //메뉴에 따라 쓰일 변수들
        private string _menuId = "";
        private string _areaNameColumn = "";
        private string _InspectionDefId = "";
        private string _processState = "";

        //수량 변경 이벤트 객체
        private Quantity _quantity = null;

        //First Inspection User
        Dictionary<string, string> _inspectionUser = new Dictionary<string, string>();

        // 인수 등록, 인계 등록 화면에서 넘겨 받는 파라미터
        Dictionary<string, object> _trackingParam;

        #endregion

        #region 생성자

        public LotIncommingInspection()
        {
            InitializeComponent();
        }

        // Base Form 의 LoadForm 오버로드 (파라미터 받기)
        public override void LoadForm(Dictionary<string, object> parameters)
        {
            base.LoadForm(parameters);

            _trackingParam = parameters;
        }

        #endregion

        #region 컨텐츠 영역 초기화

        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeDefaultVariable();
            InitializeEvent();

            // TODO : 컨트롤 초기화 로직 구성
            InitializeLotGrid();
            InitializeNcrGrid();
            InitializeLotQtyGrid();
            InitializeInspectionUser();

            this.usInspectionResult1.InitializeControls();
            this.usInspectionResult1.DefectSplitContainer.Height = 300;

            this.usInspectionResult1.InitializeEvent();

            //로그인 유저로 작업자 지정
            popupInspectionUser.ClearButtonVisible = false;
            popupInspectionUser.SetValue(UserInfo.Current.Id);
            popupInspectionUser.Text = UserInfo.Current.Name;

            InitializeQuickMenuList();

            Font ft = new Font("Microsoft Sans Serif", 11);
            Font ft1 = new Font("Microsoft Sans Serif", 10);
            
            grdLotList.View.Appearance.BandPanel.Font = ft;
            grdLotList.View.Appearance.Row.Font = ft1;
            grdLotList.View.Appearance.HeaderPanel.Font = ft;
            
            txtLotId.Font = ft;
            lblLot.Font = ft1;
            lblArea.Font = ft1;
            lblInspector.Font = ft;

            

        }
        
        /// <summary>
        /// 퀵 메뉴 리스트 등록
        /// </summary>
        private void InitializeQuickMenuList()
        {
            menuList.Add(new DXMenuItem(Language.Get("MENU_PG-SG-0340"), OpenForm) { BeginGroup = true, Tag = "PG-SG-0340" });
            menuList.Add(new DXMenuItem(Language.Get("MENU_PG-SG-0460"), OpenForm) { Tag = "PG-SG-0460" });
            menuList.Add(new DXMenuItem(Language.Get("MENU_PG-SG-0490"), OpenForm) { Tag = "PG-SG-0490" });
            menuList.Add(new DXMenuItem(Language.Get("MENU_PG-QC-0560"), OpenForm) { Tag = "PG-QC-0560" });
            menuList.Add(new DXMenuItem(Language.Get("MENU_CHEMICALISSUE"), OpenForm) { Tag = "Chemicalissue" });
            menuList.Add(new DXMenuItem(Language.Get("MENU_RELIAVERIFIRESULTREGULAR"), OpenForm) { Tag = "ReliaVerifiResultRegular" });
        }
        /// <summary>
        /// 메뉴별 기본 변수 설정
        /// </summary>
        private void InitializeDefaultVariable()
        {
            //선택한 메뉴(자주검사입고 / 자주검사출하)
            _menuId = this.MenuId;

            //작업장 컬럼
            //자주검사(입고) = 이전작업장	|	자주검사(출고) = 작업장
            _areaNameColumn = _menuId == INSPECT_IN ? "PREVAREA" : "CURRENTAREA";

            //검사ID
            //자주검사(입고) = SelfInspectionTake	 | 	자주검사(출고) = SelfInspectionShip
            _InspectionDefId = _menuId == INSPECT_IN ? "SelfInspectionTake" : "SelfInspectionShip";

            _processState = _menuId == INSPECT_IN ? "WaitForReceive" : "WaitForSend";
        }

        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeLotGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdLotList.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdLotList.ShowButtonBar = false;
            //grdLotList.SetIsUseContextMenu(false);
            grdLotList.View.SetIsReadOnly();

            //검사차수
            grdLotList.View.AddTextBoxColumn("DEGREE", 60).SetTextAlignment(TextAlignment.Center);
            //품목코드
            grdLotList.View.AddTextBoxColumn("PRODUCTDEFID", 150);
            grdLotList.View.AddTextBoxColumn("PRODUCTDEFVERSION").SetIsHidden();
            grdLotList.View.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            //PANELQTY
            //grdLotList.View.AddTextBoxColumn("PANELQTY").SetIsHidden();
            //LOT ID
            grdLotList.View.AddTextBoxColumn("LOTID", 250);
            //공정순서
            grdLotList.View.AddTextBoxColumn("USERSEQUENCE", 80);

            //공정
            grdLotList.View.AddTextBoxColumn("PROCESSSEGMENTID").SetIsHidden();
            grdLotList.View.AddTextBoxColumn("PROCESSSEGMENTVERSION").SetIsHidden();
            grdLotList.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150).SetLabel("PROCESSSEGMENT");
            //PNL
            grdLotList.View.AddSpinEditColumn("PANELQTY", 100).SetLabel("PNL").SetDisplayFormat("#,##0");
            //PCS
            grdLotList.View.AddSpinEditColumn("QTY", 100).SetLabel("PCS").SetDisplayFormat("#,##0");
            //작업장
            grdLotList.View.AddTextBoxColumn("AREAID").SetIsHidden();
            grdLotList.View.AddTextBoxColumn("AREANAME", 100).SetLabel(_areaNameColumn);

            if(_menuId.Equals(INSPECT_OUT))
            {
                //작업설비
                grdLotList.View.AddTextBoxColumn("EQUIPMENTNAME", 150);
                //작업자
                grdLotList.View.AddTextBoxColumn("USERNAME", 80).SetLabel("ACTUALUSER");
                //작업완료시간
                grdLotList.View.AddTextBoxColumn("TRACKOUTTIME", 130);
            }

            //PCSPNL
            grdLotList.View.AddTextBoxColumn("PCSPNL").SetIsHidden();


            //Lot Type
            grdLotList.View.AddTextBoxColumn("LOTTYPE").SetIsHidden();
            //Lot 상태
            grdLotList.View.AddTextBoxColumn("LOTSTATE").SetIsHidden();
            //Process 상태
            grdLotList.View.AddTextBoxColumn("PROCESSSTATE").SetIsHidden();
            //검사기준서 File Name
            grdLotList.View.AddTextBoxColumn("FILENAME").SetIsHidden();
            //검사기준서 File Data
            grdLotList.View.AddTextBoxColumn("FILEDATA").SetIsHidden();

            grdLotList.View.AddTextBoxColumn("NCRINSPECTIONQTY").SetIsHidden();
            grdLotList.View.AddTextBoxColumn("INSPECTIONUNIT").SetIsHidden();
            grdLotList.View.AddTextBoxColumn("CURAREANAME").SetIsHidden();
            grdLotList.View.PopulateColumns();
            grdLotList.View.FixColumn(new string[] {"PRODUCTDEFID","PRODUCTDEFVERSION","PRODUCTDEFNAME","LOTID","USERSEQUENCE","PROCESSSEGMENTID","PROCESSSEGMENTVERSION","PROCESSSEGMENTNAME","PANELQTY", "QTY" });
        }

        /// <summary>
        /// NCR 발행 기준 그리드 초기화
        /// </summary>
        private void InitializeNcrGrid()
        {
            //grdNcrList.ShowButtonBar = false;
            grdNcrList.View.SetIsReadOnly();
            grdNcrList.GridButtonItem = GridButtonItem.None;
            /*
            grdNcrList.View.AddTextBoxColumn("QCGRADESEQUENCE", 50).SetTextAlignment(TextAlignment.Center).SetLabel("SEQ");
            grdNcrList.View.AddTextBoxColumn("QCGRADE", 100).SetTextAlignment(TextAlignment.Center).SetLabel("GRADE");
            grdNcrList.View.AddSpinEditColumn("NGRATE", 100).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetTextAlignment(TextAlignment.Right).SetLabel("RANGE");
            grdNcrList.View.AddTextBoxColumn("NGCONDITION").SetIsHidden();
            grdNcrList.View.AddTextBoxColumn("CONDITIONNAME", 100).SetTextAlignment(TextAlignment.Center).SetLabel("NGCONDITION");
            */
            grdNcrList.View.AddTextBoxColumn("DECISIONDEGREE").SetIsHidden();
            grdNcrList.View.AddTextBoxColumn("DECISIONDEGREENAME", 80).SetTextAlignment(TextAlignment.Center).SetLabel("DECISIONDEGREE");
            grdNcrList.View.AddTextBoxColumn("QCGRADE", 80).SetTextAlignment(TextAlignment.Center);
            grdNcrList.View.AddTextBoxColumn("NGCONDITION", 60).SetIsHidden();
            grdNcrList.View.AddTextBoxColumn("NGCONDITIONNAME", 80).SetTextAlignment(TextAlignment.Center).SetLabel("NGCONDITION");
            grdNcrList.View.AddTextBoxColumn("QTYORRATE", 60).SetIsHidden();
            grdNcrList.View.AddTextBoxColumn("QTYORRATENAME", 80).SetTextAlignment(TextAlignment.Center).SetLabel("QTYORRATE");
            grdNcrList.View.AddTextBoxColumn("NCRSPEC", 100).SetTextAlignment(TextAlignment.Center).SetLabel("RANGE");
            grdNcrList.View.AddTextBoxColumn("FROMRATE", 60).SetIsHidden();
            grdNcrList.View.AddTextBoxColumn("TORATE", 60).SetIsHidden();
            grdNcrList.View.AddTextBoxColumn("NGQUANTITY", 60).SetIsHidden();
            grdNcrList.View.PopulateColumns();
        }

        /// <summary>
        /// 검사수량 그리드 초기화
        /// </summary>
        private void InitializeLotQtyGrid()
        {
            grdLotQty.GridButtonItem = GridButtonItem.None;
            grdLotQty.ShowButtonBar = false;
            grdLotQty.ShowStatusBar = false;
            grdLotQty.View.EnableRowStateStyle = false;
            grdLotQty.View.OptionsView.ShowIndicator = false;
            grdLotQty.View.OptionsCustomization.AllowColumnResizing = false;

            //구분
            grdLotQty.View.AddTextBoxColumn("DIVISION", 99).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            //작업수량
            grdLotQty.View.AddSpinEditColumn("WORKQTY", 135).SetIsReadOnly().SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0");
            //검사수량
            grdLotQty.View.AddSpinEditColumn("INSPECTIONQTY", 135).SetDisplayFormat("#,##0", MaskTypes.Numeric, false); 
            //불량수량
            grdLotQty.View.AddSpinEditColumn("DEFECTQTY", 135).SetIsReadOnly().SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0");
            //기준 PNLPCS
            grdLotQty.View.AddTextBoxColumn("PCSPNL").SetIsHidden();

            grdLotQty.View.PopulateColumns();

            //데이터 초기화
            DataTable qtyTable = new DataTable();
            qtyTable.Columns.Add("DIVISION", typeof(string));
            qtyTable.Columns.Add("WORKQTY", typeof(string));
            qtyTable.Columns.Add("INSPECTIONQTY", typeof(string));
            qtyTable.Columns.Add("DEFECTQTY", typeof(string));
            qtyTable.Columns.Add("PCSPNL", typeof(string));

            qtyTable.Rows.Add("PNL", 0, 0, 0, 216);
            qtyTable.Rows.Add("PCS", 0, 0, 0, 216);
            
            grdLotQty.DataSource = qtyTable;


            //Grid Display Cell Value
            GridColumn colInspectionQty = grdLotQty.View.Columns["INSPECTIONQTY"];
            colInspectionQty.DisplayFormat.FormatString = "{0:f2}";
        }

        /// <summary>
        /// 검사자 팝업 컨트롤 초기화
        /// </summary>
        private void InitializeInspectionUser()
        {
            if (!smartPanel1.IsDesignMode())
            {
                //GetUserList 10001
                ConditionItemSelectPopup options = new ConditionItemSelectPopup();
                options.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
                options.SetPopupLayout("USERLIST", PopupButtonStyles.Ok_Cancel, true, false);
                options.Id = "USER";
                options.SearchQuery = new SqlQuery("GetUserList", "10001", $"PLANTID={UserInfo.Current.Plant}");
                options.IsMultiGrid = false;
                options.DisplayFieldName = "USERNAME";
                options.ValueFieldName = "USERID";
                options.LanguageKey = "USER";

                options.Conditions.AddTextBox("USERIDNAME");

                options.GridColumns.AddTextBoxColumn("USERID", 150);
                options.GridColumns.AddTextBoxColumn("USERNAME", 200);

                popupInspectionUser.SelectPopupCondition = options;
            }
        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            Load += LotIncommingInspection_Load;

            _quantity = new Quantity();
            _quantity.ChangeQtyValue += ChangePcsQty;
            _quantity.ChangeQtyValue += ChangePnlQty;

            grdLotList.View.RowStyle += View_RowStyle;

            grdLotList.InitContextMenuEvent += GrdLotList_InitContextMenuEvent;
            grdLotList.View.FocusedRowChanged += View_FocusedRowChanged;

            defectProcessListControl.EditValueChange += DefectProcessListControl1_EditValueChange;
            grdLotQty.View.CellValueChanged += View_CellValueChanged;
            grdLotQty.View.ShowingEditor += View_ShowingEditor;

            smartTabControl1.SelectedPageChanged += SmartTabControl1_SelectedPageChanged;


            //grdLotQty.View.MouseWheel += View_MouseWheel;
            //grdLotQty.View.MouseDown += View_MouseDown;

            btnSave.Click += BtnSave_ClickAsync;
        }

        private void SmartTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (e.Page == tpgInspectionHistory)
            {
                try
                {
                    smartTabControl1.ShowWaitArea();

                    string lotId = Format.GetString(grdLotList.View.GetFocusedRowCellValue("LOTID"));

                    usInspectionResult1.SearchInspectionData(lotId);
                }
                catch (Exception ex)
                {
                    throw MessageException.Create(ex.Message);
                }
                finally
                {
                    smartTabControl1.CloseWaitArea();
                }
            }
        }

        // 인수 등록, 인계 등록 화면에서 파라미터 전달 받은 경우 검색조건 세팅 및 조회
        private async void LotIncommingInspection_Load(object sender, EventArgs e)
        {
            if (_trackingParam != null)
            {
                Conditions.GetControl<SmartSelectPopupEdit>("P_AREAID").SetValue(Format.GetString(_trackingParam["AREAID"]));
				Conditions.GetControl<SmartSelectPopupEdit>("P_AREAID").Text = Format.GetString(_trackingParam["AREANAME"]);
				Conditions.GetControl<SmartSelectPopupEdit>("P_LOTID").SetValue(Format.GetString(_trackingParam["LOTID"]));

                await OnSearchAsync();
            }
        }

        /// <summary>
        /// ROW 스타일 - 차수 1 이상 회색
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_RowStyle(object sender, RowStyleEventArgs e)
        {
            if(e.RowHandle < 0) return;

            if(Format.GetInteger(grdLotList.View.GetRowCellValue(e.RowHandle, "DEGREE")) > 0)
            {
                e.Appearance.BackColor = Color.LightGray;
                e.HighPriority = true;
            }
        }

        private void View_MouseWheel(object sender, MouseEventArgs e)
        {
            (e as DXMouseEventArgs).Handled = true;
        }

        private void View_MouseDown(object sender, MouseEventArgs e)
        {
            GridView view = sender as GridView;
            GridHitInfo info = view.CalcHitInfo(e.Location);

            if (info.InRow && info.RowHandle == view.RowCount - 1)
            {
                view.BeginUpdate();
                view.FocusedRowHandle = info.RowHandle;
                view.FocusedColumn = info.Column;
                view.EndUpdate();

                ((DXMouseEventArgs)e).Handled = true;
            }
        }

        #region ▶ NCR 체크 |
        private bool checkNCR(string defectGrade, double defectRate,decimal defectQty,string strDefectCode, string imgResourceID)
        {
            DataTable dt = grdNcrList.DataSource as DataTable;

            List<DataRow> listDr = dt.AsEnumerable().Where(c => c.Field<string>("DECISIONDEGREE").Equals(defectGrade)).ToList<DataRow>();

            bool bcheck = true;

            string message = string.Empty;

            foreach (DataRow dr in listDr)
            {
                string judgeType = Format.GetTrimString(dr["NGCONDITION"]);
                double fromRate = Format.GetDouble(dr["FROMRATE"], 0);
                double toRate = Format.GetDouble(dr["TORATE"], 0);
                double ngQty = Format.GetDouble(dr["NGQUANTITY"], 0);
                double chkDefectQty = Format.GetDouble(defectQty,0);
                string checkType = Format.GetTrimString(dr["QTYORRATE"]);
                
                string spec = Format.GetTrimString(dr["NCRSPEC"]);
                string gcGrad = Format.GetTrimString(dr["QCGRADE"]);

                switch (judgeType)
                {
                    case "GE": //이상
                        if(checkType.Equals("RATE") && defectRate >= toRate)
                        {

                            message = SetNCRMessage(checkType, strDefectCode, gcGrad, defectQty.ToString(), spec, defectRate.ToString());
                            bcheck = false;
                        }
                        else if (checkType.Equals("QTY") && chkDefectQty >= ngQty)
                        {
                            message = SetNCRMessage(checkType, strDefectCode, gcGrad, defectQty.ToString(), spec, defectRate.ToString());

                            bcheck = false;
                        }
                            break;
                    case "BT": //사이
                        if (checkType.Equals("RATE") && defectRate >= fromRate && defectRate <= toRate)
                        {
                            message = SetNCRMessage(checkType, strDefectCode, gcGrad, defectQty.ToString(), spec, defectRate.ToString());
                            bcheck = false;
                        }
                        else if (checkType.Equals("QTY") && ngQty >= fromRate && ngQty <= toRate)
                        {
                            message = SetNCRMessage(checkType, strDefectCode, gcGrad, defectQty.ToString(), spec, defectRate.ToString());
                            bcheck = false;
                        }
                        break;
                    case "GT": //초과
                        if (checkType.Equals("RATE") && defectRate > toRate)
                        {
                            message = SetNCRMessage(checkType, strDefectCode, gcGrad, defectQty.ToString(), spec, defectRate.ToString());
                            bcheck = false;
                        }
                        else if (checkType.Equals("QTY") && chkDefectQty > ngQty)
                        {
                            message = SetNCRMessage(checkType, strDefectCode, gcGrad, defectQty.ToString(), spec, defectRate.ToString());
                            bcheck = false;
                        }
                        break;
                    case "LE": //이하
                        if (checkType.Equals("RATE") && defectRate <= toRate)
                        {
                            message = SetNCRMessage(checkType, strDefectCode, gcGrad, defectQty.ToString(), spec, defectRate.ToString());
                            bcheck = false;
                        }
                        else if (checkType.Equals("QTY") && chkDefectQty <= ngQty)
                        {

                        }
                            break;
                    case "LT": //미만
                        if (checkType.Equals("RATE") && defectRate < toRate)
                        {
                            message = SetNCRMessage(checkType, strDefectCode, gcGrad, defectQty.ToString(), spec, defectRate.ToString());
                            bcheck = false;
                        }
                        else if (checkType.Equals("QTY") && chkDefectQty < ngQty)
                        {
                            message = SetNCRMessage(checkType, strDefectCode, gcGrad, defectQty.ToString(), spec, defectRate.ToString());
                            bcheck = false;
                        }
                        break;
                    case "EQ": //이퀄
                        if (checkType.Equals("RATE") && defectRate == toRate)
                        {
                            message = SetNCRMessage(checkType, strDefectCode, gcGrad, defectQty.ToString(), spec, defectRate.ToString());
                            bcheck = false;
                        }
                        else if (checkType.Equals("RATE") && chkDefectQty == ngQty)
                        {
                            message = SetNCRMessage(checkType, strDefectCode, gcGrad, defectQty.ToString(), spec, defectRate.ToString());
                            bcheck = false;
                        }

                        break;
                }
            }

            //2020-03-06 강유라 영풍만 한시적으로 사진 등록 필수 해제   2021-02-09 오근영 INTERFLEX만 동작 조건 주석처리
            //if (UserInfo.Current.Enterprise.Equals("INTERFLEX"))
            //{
            //    if (!bcheck && string.IsNullOrWhiteSpace(imgResourceID))
            //    {
            //        //throw MessageException.Create("CHECKIMEAGEBYNCR");
            //        ShowMessage(MessageBoxButtons.OK, "CHECKIMEAGEBYNCR", message);
            //    }
            //    else
            //    {
            //        bcheck = true;
            //    }
            //}
            //else
            //{
            //    bcheck = true;
            //}

            return bcheck;
        }

        private string checkNCR2(string defectGrade, double defectRate, decimal defectQty, string strDefectCode, string strDefectCodeName, string strQCSegmentID, string strQCSegmentName, bool bChk2, string defectGradeName)
        {
            DataTable dt = grdNcrList.DataSource as DataTable;

            List<DataRow> listDr = dt.AsEnumerable().Where(c => c.Field<string>("DECISIONDEGREE").Equals(defectGrade)).ToList<DataRow>();

            bool bcheck = true;

            string message = string.Empty;

            foreach (DataRow dr in listDr)
            {
                string judgeType = Format.GetTrimString(dr["NGCONDITION"]);
                double fromRate = Format.GetDouble(dr["FROMRATE"], 0);
                double toRate = Format.GetDouble(dr["TORATE"], 0);
                double ngQty = Format.GetDouble(dr["NGQUANTITY"], 0);
                double chkDefectQty = Format.GetDouble(defectQty, 0);
                string checkType = Format.GetTrimString(dr["QTYORRATE"]);

                string spec = Format.GetTrimString(dr["NCRSPEC"]);
                string gcGrad = Format.GetTrimString(dr["QCGRADE"]);

                message =
                    ""
                    + "\r\n"
                    + "-. "
                    //+ Language.Get("DEFECTCODENAME")
                    //+ " : "
                    + strDefectCodeName
                    + ", "
                    //+ Language.Get("DECISIONDEGREE") 
                    //+ " : " 
                    + defectGradeName
                    + ", "
                    //+ Language.Get("QCSEGMENTNAME")
                    //+ " : "
                    + strQCSegmentName
                    + ", "
                    ;
                if (checkType.Equals("QTY"))
                {
                    message =
                        message
                        //+ Language.Get("PCSDEFECTQTY") 
                        //+ " : " 
                        + defectQty
                        + "PCS"
                        ;
                }
                else
                {
                    message =
                        message
                        //+ Language.Get("NCRDEFECTRATE") 
                        //+ " : " 
                        + defectRate.ToString("#,##0.00")
                        + "%"
                        ;
                }
                switch (judgeType)
                {
                    case "GE": //이상
                        if (checkType.Equals("RATE") && defectRate >= toRate)
                        {
                            bcheck = false;
                        }
                        else if (checkType.Equals("QTY") && chkDefectQty >= ngQty)
                        {
                            bcheck = false;
                        }
                        break;
                    case "BT": //사이
                        if (checkType.Equals("RATE") && defectRate >= fromRate && defectRate <= toRate)
                        {
                            bcheck = false;
                        }
                        else if (checkType.Equals("QTY") && ngQty >= fromRate && ngQty <= toRate)
                        {
                            bcheck = false;
                        }
                        break;
                    case "GT": //초과
                        if (checkType.Equals("RATE") && defectRate > toRate)
                        {
                            bcheck = false;
                        }
                        else if (checkType.Equals("QTY") && chkDefectQty > ngQty)
                        {
                            bcheck = false;
                        }
                        break;
                    case "LE": //이하
                        if (checkType.Equals("RATE") && defectRate <= toRate)
                        {
                            bcheck = false;
                        }
                        else if (checkType.Equals("QTY") && chkDefectQty <= ngQty)
                        {
                            bcheck = false; // 기존 로직에서는 빠져 있었음.
                        }
                        break;
                    case "LT": //미만
                        if (checkType.Equals("RATE") && defectRate < toRate)
                        {
                            bcheck = false;
                        }
                        else if (checkType.Equals("QTY") && chkDefectQty < ngQty)
                        {
                            bcheck = false;
                        }
                        break;
                    case "EQ": //이퀄
                        if (checkType.Equals("RATE") && defectRate == toRate)
                        {
                            bcheck = false;
                        }
                        else if (checkType.Equals("RATE") && chkDefectQty == ngQty)
                        {
                            bcheck = false;
                        }
                        break;
                }
            }

            if
                (
                !bcheck
                &&
                bChk2
                )
            {
                return message;
            }
            else
            {
                return "";
            }
        }
        #endregion ▶ NCR 체크 |
        /// <summary>
        /// 저장 버튼 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnSave_ClickAsync(object sender, EventArgs e)
        {
            bool isSave = DataValidation();
            if(!isSave) return;

            //********************************************************************************************
            // INSPECTION RESULT DATA
            //********************************************************************************************
            DataTable dtLotList = grdLotList.DataSource as DataTable;
            _dtResultData = dtLotList.Clone();

            foreach (DataRow row in dtLotList.Rows)
            {
                DataRow selected = grdLotList.View.GetFocusedDataRow();
                if (row.Equals(selected))
                {
                    _dtResultData.ImportRow(row);
                }
            }

            //********************************************************************************************
            // INSPECTION DEFECT DATA
            //********************************************************************************************
            DataTable defectList = defectProcessListControl.GetGridDataSource();
            if (defectList != null)
            {
                //불량 Datasource
                _dtDefectData = defectList.Clone();
                string message = string.Empty;

                foreach (DataRow row in defectList.Rows)
                {
                    decimal pcsQty = Format.GetDecimal(row["QTY"]);
                    decimal pnlQty = Format.GetDecimal(row["PNLQTY"]);
                    double defectRate = Format.GetDouble(row["DEFECTRATE"],0);
                    string defectGrade  = Format.GetTrimString(row["DECISIONDEGREE"]);
                    string defectGradeName = Format.GetTrimString(row["DECISIONDEGREENAME"]);
                    string defectCode = Format.GetTrimString(row["DEFECTCODE"]);
                    string defectCodeName = Format.GetTrimString(row["DEFECTCODENAME"]);
                    string qcsegmentid = Format.GetTrimString(row["QCSEGMENTID"]);
                    string qcsegmentname = Format.GetTrimString(row["QCSEGMENTNAME"]);

                    bool bChk2 =
                    (
                           string.IsNullOrEmpty(Format.GetString(row["FILEPATH"]))
                        || string.IsNullOrEmpty(Format.GetString(row["FILENAME"]))
                        || string.IsNullOrEmpty(Format.GetString(row["FILEEXT"]))
                        || string.IsNullOrEmpty(Format.GetString(row["FILESIZE"]))
                        || string.IsNullOrEmpty(Format.GetString(row["IMAGERESOURCEID"]))
                    );

                    message += checkNCR2(defectGrade, defectRate, pcsQty, defectCode, defectCodeName, qcsegmentid, qcsegmentname, bChk2, defectGradeName);

                    if (pcsQty > 0)
                    {
                        _dtDefectData.ImportRow(row);
                    }
                }//foreach				
                if (message.Length > 0)
                {
                    throw MessageException.Create("CHECKIMEAGEBYNCR2", "\r\n\r\n" + Language.Get("TARGETITEM") + message);
                }
            }

            //이상 발생 시 필요 데이터 :: 자주검사 차수, 부적합 사유 추가
            _dtDefectData.Columns.Add("DEGREE", typeof(string));
            _dtDefectData.Columns.Add("REASONCODEID", typeof(string));
            foreach (DataRow row in _dtDefectData.Rows)
            {
                row["DEGREE"] = (Format.GetDecimal(_dtResultData.Rows[0]["DEGREE"]) + 1).ToString();
                row["REASONCODEID"] = "LockSelfExamNonconfirm";
            }

            //********************************************************************************************
            // 검사수량(샘플수량), 불량수량
            //********************************************************************************************
            decimal inspectionQty = Format.GetDecimal(grdLotQty.View.GetRowCellValue(1, "INSPECTIONQTY"));
            decimal defectQty = Format.GetDecimal(grdLotQty.View.GetRowCellValue(1, "DEFECTQTY"));

            MessageWorker worker = new MessageWorker("LotIncommingInspection");
            worker.SetBody(new MessageBody()
            {
                { "enterpriseId", UserInfo.Current.Enterprise },
                { "plantId", UserInfo.Current.Plant },
                { "resultList", _dtResultData },
                { "defectList", _dtDefectData },
                { "inspectionQty", inspectionQty },
                { "specOutQty", defectQty },
                { "reinspectReason", txtReinspectReason.Text.Trim() },
                { "inspectionUser", popupInspectionUser.GetValue() },
                { "inspectionClassId", _InspectionDefId }
            });
            Framework.Net.Data.IResponse<DataTable> resultDt = worker.Execute<DataTable>();

            ShowMessage("SuccessSave");

            if (_menuId.Equals(INSPECT_OUT))
            {
                Dictionary<string, object> inspectionParam = new Dictionary<string, object>();
                inspectionParam.Add("LOTID", txtLotId.Text);
                inspectionParam.Add("INSPECTIONDEFID", "OperationInspection");

                DataTable inspectionResult = SqlExecuter.Query("GetCheckLotInspection", "10003", inspectionParam);
                if (inspectionResult.Rows[0]["CHECKINSPECTION"].Equals("N"))
                {
                    if (ShowMessage(MessageBoxButtons.YesNo, "MoveQualitySpecification") == DialogResult.Yes)
                    {
                        DataRow row = grdLotList.View.GetFocusedDataRow();
                        var param = row.Table.Columns
                            .Cast<DataColumn>()
                            .ToDictionary(col => col.ColumnName, col => row[col.ColumnName]);

                        //메뉴 호출			
                        OpenMenu("PG-QC-0120", param);
                    }
                }
            }

            //이메일 발송
            string title = _menuId == INSPECT_IN ? Language.Get("SELFTAKEABNORMALTITLE") : Language.Get("SELFABNORMALTITLE");			
            DataTable dt = resultDt.GetResultSet();
            if (dt.Rows.Count > 0)
            {
                if (Format.GetBoolean(dt.Rows[0]["ISSENDEMAIL"], false))
                {
                    DataTable dtEmail = CommonFunction.CreateSelfTakeShipAbnormalEmailDt();
                    DataRow newRow = dtEmail.NewRow();

                    newRow["PRODUCTDEFNAME"] = _dtResultData.Rows[0]["PRODUCTDEFNAME"];
                    newRow["PRODUCTDEFID"] = _dtResultData.Rows[0]["PRODUCTDEFID"];
                    newRow["PRODUCTDEFVERSION"] = _dtResultData.Rows[0]["PRODUCTDEFVERSION"];
                    newRow["LOTID"] = _dtResultData.Rows[0]["LOTID"];
                    newRow["PROCESSSEGMENTNAME"] = _dtResultData.Rows[0]["PROCESSSEGMENTNAME"];
                    newRow["PROCESSSEGMENTID"] = _dtResultData.Rows[0]["PROCESSSEGMENTID"];
                    newRow["AREANAME"] = _dtResultData.Rows[0]["CURAREANAME"];
                    newRow["AREAID"] = _dtResultData.Rows[0]["CURAREAID"];
                    newRow["USERID"] = UserInfo.Current.Id;
                    newRow["TITLE"] = title;
                    newRow["INSPECTION"] = _InspectionDefId;
                    newRow["LANGUAGETYPE"] = UserInfo.Current.LanguageType;
                    newRow["DEFECTCODENAME"] = string.Join("\r\n", dt.AsEnumerable().Select(r => Format.GetString(r["DEFECTCODENAME"])));
                    //newRow["ISLOCKING"] = string.Join(",", dt.AsEnumerable().Select(r => Format.GetString(r["ISLOCKING"])));

                    dtEmail.Rows.Add(newRow);

                    CommonFunction.ShowSendEmailPopupDataTable(dtEmail);
                }
            }

            /*
            string allContent = string.Empty;//이메일 내용 만들기

            DataTable dt = resultDt.GetResultSet();

            if(dt.Rows.Count > 0)
            {
                if (Format.GetBoolean(dt.Rows[0]["ISSENDEMAIL"], false))
                {
                    allContent += "○ " + Language.Get("PROCESSSEGMENT") + " : " 
                                        + Format.GetString(_dtResultData.Rows[0]["PROCESSSEGMENTNAME"]) 
                                        + "(" + Format.GetString(_dtResultData.Rows[0]["PROCESSSEGMENTID"]) + ")\r\n";

                    allContent += "○ " + Language.Get("AREA") + " : " 
                                        + Format.GetString(_dtResultData.Rows[0]["AREANAME"]) 
                                        + "(" + Format.GetString(_dtResultData.Rows[0]["AREAID"]) + ")\r\n";

                    allContent += "○ " + Language.Get("PRODUCTDEFNAME") + "(" 
                                        + Language.Get("PRODUCTDEFID") + "/" 
                                        + Language.Get("PRODUCTDEFVERSION") + ") : " 
                                        + Format.GetString(_dtResultData.Rows[0]["PRODUCTDEFNAME"]) 
                                        + "(" + Format.GetString(_dtResultData.Rows[0]["PRODUCTDEFID"]) 
                                        + "/" + Format.GetString(_dtResultData.Rows[0]["PRODUCTDEFVERSION"]) + ")\r\n";

                    allContent += "○ " + Language.Get("LOTID") + " : "
                                        + Format.GetString(_dtResultData.Rows[0]["LOTID"]) + ")\r\n";

                    allContent += "○ " + Language.Get("DEFECTCODENAME") + " : ";
                    List<string> defectNameList = new List<string>();
                    for (int i = 0; i < _dtDefectData.Rows.Count; i++)
                    {
                        defectNameList.Add(Format.GetString(_dtDefectData.Rows[i]["DEFECTCODENAME"]));
                    }
                    allContent += string.Join("/", defectNameList) + "\r\n";

                    allContent += "○ DATA(" + Language.Get("QCMFINALINSPECTQTY") + "/" 
                                + Language.Get("AREADEFECTQTY") + "/" 
                                + Language.Get("AREADEFECTRATE") + ") : \r\n";

                    for (int i = 0; i < _dtDefectData.Rows.Count; i++)
                    {
                        string temp = string.Empty;
                        temp += Format.GetDouble(inspectionQty, 0).ToString() 
                                + "/" + Format.GetDouble(_dtDefectData.Rows[i]["QTY"], 0).ToString() 
                                + "/" + Format.GetDouble(_dtDefectData.Rows[i]["DEFECTRATE"], 0).ToString() + "%\r\n";

                        allContent += temp;
                    }

                    string isLocking = Format.GetBoolean(dt.Rows[0]["ISSENDEMAIL"], false) == true ? "Y" : "N";

                    allContent += "○ " + Language.Get("ISLOCKING") + "(Y/N) : "
                                + isLocking + "\r\n";

                    CommonFunction.ShowSendEmailPopup(title, allContent);
                }
            }
            */

            await OnSearchAsync();

            //defectProcessListControl.SetGridClear();
            View_FocusedRowChanged(null, null);
        }

        // 2019-09-30 hykang 추가
        // Grid Context Menu Item 선언
        /// <summary>
        /// Customizing Context Menu Item 초기화
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void GrdLotList_InitContextMenuEvent(SmartBandedGrid sender, Framework.SmartControls.Grid.InitContextMenuEventArgs args)
        {
            for (int i = 0; i < menuList.Count; i++)
            {
                args.AddMenus.Add(menuList[i]);
            }
        }

        /// <summary>
        /// 셀 ReadOnly 처리(LOT의 Unit이 PCS, PNL이 아닌 Uint에 대하여 ReadOnly 처리)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_ShowingEditor(object sender, CancelEventArgs e)
        {
            DataRow selectedLot = grdLotList.View.GetFocusedDataRow();
			if(selectedLot == null) return;

            string InspUnit = Format.GetFullTrimString(selectedLot["INSPECTIONUNIT"]);
            string Inspqty = Format.GetFullTrimString(selectedLot["NCRINSPECTIONQTY"]);

            int rowhandle = grdLotQty.View.FocusedRowHandle;
            bool bcheck = false;

            if (!Format.GetString(selectedLot["UNIT"]).Equals("PNL") && !Format.GetString(selectedLot["UNIT"]).Equals("PCS") && !Format.GetString(selectedLot["UNIT"]).Equals("BLK"))
            {
                GridView view = sender as GridView;
                if(view.FocusedColumn.FieldName.Equals("INSPECTIONQTY"))
                {
                    e.Cancel = true;
                }
            }
            else if (!string.IsNullOrEmpty(InspUnit) && !string.IsNullOrEmpty(Inspqty) && !Inspqty.Equals("0"))
            {
                if (rowhandle == 0 && InspUnit.Equals("PNL")) bcheck = true;

                if (rowhandle == 1 && (InspUnit.Equals("PCS") || InspUnit.Equals("BLK"))) bcheck = true;

                if (!bcheck) e.Cancel = true;
            }

        }

        /// <summary>
        /// ROW 클릭 시 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grdLotList.View.FocusedRowHandle < 0)
            {
                DataTable dt = grdLotList.DataSource as DataTable;
                if (dt != null && dt.Rows.Count > 0)
                {
                    OnSearchAsync();
                }
                else
                {
                    return;
                }
            }

            try
            {
                pnlContent.ShowWaitArea();

                smartTabControl1.SelectedTabPage = tpgDefectResult;

                FocusedRowDataBind();
                DataBindNcrStandard();
                setInspectQty();
            }
            catch (Exception ex)
            {
                throw MessageException.Create(ex.Message);
            }
            finally
            {
                pnlContent.CloseWaitArea();
            }
        }

        /// <summary>
        /// 자주검사 기준정보에 의해 검사수량 셋팅 및 단위 셋팅
        /// </summary>
        private void setInspectQty()
        {
            //grdLotQty.View.AddTextBoxColumn("DIVISION", 99).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            //작업수량
            //grdLotQty.View.AddSpinEditColumn("WORKQTY", 135).SetIsReadOnly().SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0");

            //grdLotQty.View.AddSpinEditColumn("INSPECTIONQTY", 135).SetDisplayFormat("#,##0", MaskTypes.Numeric, false);

            int pnlRow = 0;
            int pcsRow = 0;
            for (int i = 0; i < grdLotQty.View.RowCount; i++)
            {
                string Unit = Format.GetFullTrimString(grdLotQty.View.GetRowCellValue(i, "DIVISION"));
                if(Unit.Equals("PCS"))
                {
                    pcsRow = i;
                }
                else if(Unit.Equals("PNL"))
                {
                    pnlRow = i;
                }
            }

            int RowHandle = grdLotList.View.FocusedRowHandle;
            

            int InspectQty =  int.Parse(Format.GetFullTrimString(grdLotList.View.GetRowCellValue(RowHandle, "NCRINSPECTIONQTY")));
            string unit = Format.GetFullTrimString(grdLotList.View.GetRowCellValue(RowHandle, "INSPECTIONUNIT"));
            int pcsPnl = Format.GetInteger(grdLotList.View.GetRowCellValue(RowHandle, "PCSPNL"));

            int pnlWorkQty = Format.GetInteger(grdLotQty.View.GetRowCellValue(pnlRow, "WORKQTY"));
            int pcsWorkQty = Format.GetInteger(grdLotQty.View.GetRowCellValue(pcsRow, "WORKQTY"));

            if (!InspectQty.Equals(0) && !string.IsNullOrEmpty(unit))
            {
                if(unit.Equals("PNL"))
                {
                    if (InspectQty > pnlWorkQty) InspectQty = pnlWorkQty;

                    //grdLotQty.View.SetRowCellValue(pnlRow, "INSPECTIONQTY", InspectQty);
                    (grdLotQty.DataSource as DataTable).Rows[pnlRow]["INSPECTIONQTY"] = InspectQty;
                    (grdLotQty.DataSource as DataTable).Rows[pcsRow]["INSPECTIONQTY"] = InspectQty * pcsPnl;
					defectProcessListControl.SetInspectQtyDataBind(InspectQty * pcsPnl);
				}
                else
                {
                    if (InspectQty > pcsWorkQty) InspectQty = pcsWorkQty;

                    //grdLotQty.View.SetRowCellValue(pcsRow, "INSPECTIONQTY", InspectQty);
                    (grdLotQty.DataSource as DataTable).Rows[pnlRow]["INSPECTIONQTY"] = Math.Ceiling(Format.GetDouble(InspectQty, 0) / Format.GetDouble(pcsPnl, 0));
                    (grdLotQty.DataSource as DataTable).Rows[pcsRow]["INSPECTIONQTY"] = InspectQty;
					defectProcessListControl.SetInspectQtyDataBind(InspectQty);
				}
            }

            (grdLotQty.DataSource as DataTable).AcceptChanges();
        }

        private void DataBindNcrStandard()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("INSPECTIONDEFID", _InspectionDefId);
            param.Add("PLANTID", UserInfo.Current.Plant);

            DataTable dtNcrList = SqlExecuter.Query("GetNcrStandardOfSelfInspection", "10002", param);
            grdNcrList.DataSource = dtNcrList;
        }

        /// <summary>
        /// row 클릭 시 데이터 바인드
        /// </summary>
        private void FocusedRowDataBind()
        {
            DataRow selectedRow = grdLotList.View.GetFocusedDataRow();

            if(selectedRow == null) return;

            string lotId = selectedRow["LOTID"].ToString();
            string areaName = selectedRow["CURAREANAME"].ToString();
            string processsegmentName = selectedRow["PROCESSSEGMENTNAME"].ToString();
            string InspectionCnt = Format.GetTrimString(selectedRow["DEGREE"]);

            decimal pnlQty = Format.GetDecimal(selectedRow["PANELQTY"], 0);
            decimal pcsQty = Format.GetDecimal(selectedRow["QTY"], 0);
            decimal pcsPnl = Format.GetDecimal(selectedRow["PCSPNL"], 0);

            txtLotId.Text = lotId;
            txtAreaName.Text = areaName;


            if (InspectionCnt.Equals("0"))
                txtReinspectReason.ReadOnly = true;
            else
                txtReinspectReason.ReadOnly = false;

            string InspUnit = Format.GetFullTrimString(selectedRow["INSPECTIONUNIT"]);
            decimal Inspqty = Format.GetDecimal(selectedRow["NCRINSPECTIONQTY"]);

            ClearLotQtyGrid(pnlQty, pcsQty, pcsPnl);

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LotId", lotId);
            param.Add("PlantId", UserInfo.Current.Plant);
            param.Add("LanguageType", UserInfo.Current.LanguageType);
            param.Add("INSPECTIONDEFID", _InspectionDefId);

            DataTable dtLotDefectList = SqlExecuter.Query("SelectLotDefectProcessList", "10002", param);
            defectProcessListControl.SetResultData(lotId, pcsQty, pcsPnl, dtLotDefectList, _InspectionDefId);

            // 검사이력 조회
            //this.usInspectionResult1.SearchInspectionData(lotId);
        }

        /// <summary>
        /// 수량 변경 시 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (grdLotQty.View.FocusedRowHandle < 0) return;

            //기존 불량 수량 Clear
            defectProcessListControl.SetGridQtyClear();

            if(e.Column.FieldName.Equals("INSPECTIONQTY"))
            {
                int inspectQty = 0;
                //int handleIndex = grdLotQty.View.FocusedRowHandle;
                if(e.RowHandle.Equals(0))
                {
                    inspectQty = Format.GetInteger(e.Value) * Format.GetInteger(grdLotList.View.GetFocusedDataRow()["PCSPNL"]);
                }
                else
                {
                    inspectQty = Format.GetInteger(e.Value);//Format.GetInteger(e.Value) / Format.GetInteger(grdLotList.View.GetFocusedDataRow()["PCSPNL"]);
                }
                
                defectProcessListControl.SetInspectQtyDataBind(inspectQty);
            }

            grdLotQty.View.CellValueChanged -= View_CellValueChanged;

            DataRow row = grdLotQty.View.GetFocusedDataRow();

            double pcsPnl = Format.GetDouble(row["PCSPNL"], 0); //Convert.ToDouble(row["PCSPNL"].ToString());
            double stdPNL = Format.GetDouble(grdLotQty.View.GetRowCellValue(0, "WORKQTY"), 0);
            double stdPCS = Format.GetDouble(grdLotQty.View.GetRowCellValue(1, "WORKQTY"), 0);

            double result = 0;
            int currentHandler = grdLotQty.View.FocusedRowHandle;
            switch (currentHandler)
            {
                case 0://PNL Row

                    double pnlQty = Format.GetDouble(row["INSPECTIONQTY"], 0); //Convert.ToDouble(row["INSPECTIONQTY"].ToString());
                    if (stdPNL < pnlQty)
                    {
                        //검사 수량은 작업 수량보다 많을 수 없습니다.
                        ShowMessage("CanNotToMuch", Language.Get("INSPECTIONQTY"), Language.Get("WORKQTY"));
                        grdLotQty.View.SetRowCellValue(0, "INSPECTIONQTY", 0);
                        grdLotQty.View.SetRowCellValue(1, "INSPECTIONQTY", 0);
                        break;
                    }
                    else
                    {
                        _quantity.ChangeQtyValue -= ChangePcsQty;

                        result = _quantity.Change(pnlQty, pcsPnl);
                        grdLotQty.View.SetRowCellValue(1, "INSPECTIONQTY", result);

                        _quantity.ChangeQtyValue += ChangePcsQty;
                    }

                    break;
                case 1://PCS Row

                    double pcsQty = Convert.ToDouble(row["INSPECTIONQTY"].ToString());
                    if (stdPCS < pcsQty)
                    {
                        //검사 수량은 작업 수량보다 많을 수 없습니다.
                        ShowMessage("CanNotToMuch", Language.Get("INSPECTIONQTY"), Language.Get("WORKQTY"));
                        grdLotQty.View.SetRowCellValue(0, "INSPECTIONQTY", 0);
                        grdLotQty.View.SetRowCellValue(1, "INSPECTIONQTY", 0);
                        break;
                    }
                    else
                    {
                        _quantity.ChangeQtyValue -= ChangePnlQty;

                        result = _quantity.Change(pcsQty, pcsPnl);

                        grdLotQty.View.SetRowCellValue(0, "INSPECTIONQTY", result);

                        _quantity.ChangeQtyValue += ChangePnlQty;
                    }
                    break;
            }//switch

            grdLotQty.View.CellValueChanged += View_CellValueChanged;
        }

        /// <summary>
        /// 불량 수량 입력 시 이벤트
        /// </summary>
        /// <param name="pnlQty"></param>
        /// <param name="pcsQty"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private void DefectProcessListControl1_EditValueChange(decimal pnlQty, int pcsQty, out CancelEventArgs e)
        {
            e = new CancelEventArgs();

            //수량 Validation
            decimal insPnlQty = Format.GetDecimal(grdLotQty.View.GetRowCellValue(0, "INSPECTIONQTY"), 0);
            int insPcsQty = Format.GetInteger(grdLotQty.View.GetRowCellValue(1, "INSPECTIONQTY"), 0);

            if(insPnlQty.Equals(0) || insPcsQty.Equals(0))
            {
                //검사 수량을(를) 먼저 입력하세요.
                ShowMessage("PriorityInputSomething", Language.Get("INSPECTIONQTY"));
                e.Cancel = true;
                return;
            }

            if (insPcsQty < pcsQty)
            {
                //불량 수량은(는) 검사 수량보다 많을 수 없습니다.
                ShowMessage("CanNotToMuch", Language.Get("DEFECTQTY"), Language.Get("INSPECTIONQTY"));
                e.Cancel = true;
                return;
            }

            grdLotQty.View.CellValueChanged -= View_CellValueChanged;

            //PNL 불량 수량
            grdLotQty.View.SetRowCellValue(0, "DEFECTQTY", pnlQty);
            //PCS 불량 수량
            grdLotQty.View.SetRowCellValue(1, "DEFECTQTY", pcsQty);

            grdLotQty.View.CellValueChanged += View_CellValueChanged;
        }
        
        #endregion

        #region 툴바

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();			
        }

        #endregion

        #region 검색

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            //초기화 작업
            InitControls();
            defectProcessListControl.IsSaved = true;

            // TODO : 조회 SP 변경
            var values = Conditions.GetValues();

            if (string.IsNullOrWhiteSpace(values["P_AREAID"].ToString()))
            {
                // 대상 작업장이 선택되지 않았습니다.
                ShowMessage("NoSelectTargetArea");
                return;
            }

            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("PLANTID", UserInfo.Current.Plant);
            values.Add("INSPECTIONCLASSID", _InspectionDefId);

            DataTable dtLotInspectionList = await QueryAsync("SelectLotInspectionList", "10001", values);

            if (dtLotInspectionList.Rows.Count == 0)
            {
                // 조회할 데이터가 없습니다.
                ShowMessage("NoSelectData"); 
            }
            else if(dtLotInspectionList.Rows.Count == 1)
            {
                grdLotList.View.FocusedRowHandle = 0;
                grdLotList.View.SelectRow(grdLotList.View.FocusedRowHandle);
            }

            grdLotList.DataSource = dtLotInspectionList;
            //View_FocusedRowChanged(null, null);
        }

        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용

            //작업장
            CommonFunction.AddConditionAreaByAuthorityPopup("P_AREAID", 0.1, false, Conditions, true,true);
            //공정
            CommonFunction.AddConditionProcessSegmentPopup("P_PROCESSSEGMENTID", 0.2, true, Conditions);
            //품목
            CommonFunction.AddConditionProductPopup("P_PRODUCTDEFID", 0.4, true, Conditions);
            //LOTID
            CommonFunction.AddConditionLotHistPopup("P_LOTID", 0.6, true, Conditions);
        }

        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();
            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용

            Conditions.GetControl<SmartSelectPopupEdit>("P_LOTID").ImeMode = ImeMode.Alpha;
        }

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();			
        }

        /// <summary>
        /// 데이터 Valid 체크
        /// </summary>
        private bool DataValidation()
        {
            bool isSave = defectProcessListControl.IsSavedCheck();

            //검사수량
            decimal insPnlQty = Format.GetDecimal(grdLotQty.View.GetRowCellValue(0, "INSPECTIONQTY"), 0);
            int insPcsQty = Format.GetInteger(grdLotQty.View.GetRowCellValue(1, "INSPECTIONQTY"), 0);
            //불량수량
            decimal defectPnlQty = Format.GetDecimal(grdLotQty.View.GetRowCellValue(0, "DEFECTQTY"), 0);
            int defectPcsQty = Format.GetInteger(grdLotQty.View.GetRowCellValue(1, "DEFECTQTY"), 0);

            //검사수량 Validation
            if (insPcsQty.Equals(0))
            {
                //검사수량이 0입니다. 검사 작업 후 진행하여 주십시오.
                throw MessageException.Create("MustDoInspect");
            }


            //수량 Validation
            if (insPnlQty.Equals(0) && insPcsQty.Equals(0))
            {
                grdLotQty.View.CellValueChanged -= View_CellValueChanged;
                grdLotQty.View.SetRowCellValue(0, "DEFECTQTY", 0);//PNL 불량 수량
                grdLotQty.View.SetRowCellValue(1, "DEFECTQTY", 0);//PCS 불량 수량
                grdLotQty.View.CellValueChanged += View_CellValueChanged;


                //검사 수량을(를) 먼저 입력하세요.
                throw MessageException.Create("PriorityInputSomething", Language.Get("INSPECTIONQTY"));
            }

            if (defectPcsQty > insPcsQty)
            {
                grdLotQty.View.CellValueChanged -= View_CellValueChanged;
                grdLotQty.View.SetRowCellValue(0, "DEFECTQTY", 0);//PNL 불량 수량
                grdLotQty.View.SetRowCellValue(1, "DEFECTQTY", 0);//PCS 불량 수량
                grdLotQty.View.CellValueChanged += View_CellValueChanged;

                //불량 수량은(는) 검사 수량보다 많을 수 없습니다.
                throw MessageException.Create("CanNotToMuch", Language.Get("DEFECTQTY"), Language.Get("INSPECTIONQTY"));
            }

            return isSave;
        }
        #endregion

        #region Private Function

        /// <summary>
        /// 수량 Grid 초기화
        /// </summary>
        private void ClearLotQtyGrid(decimal pnlQty = 0, decimal pcsQty = 0, decimal pcsPnl = 0)
        {
            DataTable dtLotQty = grdLotQty.DataSource as DataTable;

            dtLotQty.Rows[0]["WORKQTY"] = pnlQty;
            dtLotQty.Rows[1]["WORKQTY"] = pcsQty;

            dtLotQty.Rows[0]["INSPECTIONQTY"] = 0;
            dtLotQty.Rows[1]["INSPECTIONQTY"] = 0;

            dtLotQty.Rows[0]["DEFECTQTY"] = 0;
            dtLotQty.Rows[1]["DEFECTQTY"] = 0;

            dtLotQty.Rows[0]["PCSPNL"] = pcsPnl;
            dtLotQty.Rows[1]["PCSPNL"] = pcsPnl;

            dtLotQty.AcceptChanges();
        }


        /// <summary>
        /// 검사수량 PNL 수량 변경
        /// PCS 수량을 자동으로 계산해준다.
        /// </summary>
        /// <param name="pnlQty"></param>
        private double ChangePnlQty(double pnlQty, double pcsPnl)
        {
            defectProcessListControl.IsSaved = true;
            double pcsQty = pnlQty * pcsPnl;

            int realPcsQty = Format.GetInteger(grdLotQty.View.GetRowCellValue(1, "WORKQTY"));
            if(realPcsQty < pcsQty)
            {
                pcsQty = realPcsQty;
            }

            return pcsQty;
        }

        /// <summary>
        /// 검사수량 PCS 수량 변경
        /// PNL 수량을 자동으로 계산해준다.
        /// </summary>
        private double ChangePcsQty(double pcsQty, double pcsPnl)
        {
            defectProcessListControl.IsSaved = true;
            double pnlQty = Math.Ceiling(pcsQty / pcsPnl);
            return pnlQty;
        }

        /// <summary>
        /// Customizing Context Menu Item Click 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OpenForm(object sender, EventArgs args)
        {
            try
            {
                DialogManager.ShowWaitDialog();

                DataRow currentRow = grdLotList.View.GetFocusedDataRow();
                if (currentRow == null) return;

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

        private void InitControls()
        {
            grdLotList.View.ClearDatas();

            txtReinspectReason.EditValue = string.Empty;
            txtLotId.EditValue = string.Empty;
            txtAreaName.EditValue = string.Empty;
            //popupInspectionUser.EditValue = string.Empty;

            grdNcrList.View.ClearDatas();
            defectProcessListControl.SetGridClear();
        }

        /// <summary>
        /// NCR 메시지 
        /// </summary>
        /// <param name="checkType"></param>
        /// <param name="strDefectCode"></param>
        /// <param name="gcGrad"></param>
        /// <param name="defectQty"></param>
        /// <param name="spec"></param>
        /// <param name="defectRate"></param>
        /// <returns></returns>
        private string SetNCRMessage(string checkType, string strDefectCode, string gcGrad, string defectQty, string spec, string defectRate)
        {
            string message = string.Empty;

            message = "\r\n" + Language.Get("DEFECTCODE") + " : " + strDefectCode + "\r\n" + Language.Get("QCGRADE") + " : " + gcGrad + "\r\n";
            if (checkType.Equals("QTY"))
            {
                message = message + Language.Get("PCSDEFECTQTY") + " : " + defectQty + "\r\n" + Language.Get("RANGE") + " : " + spec;
            }
            else
            {
                message = message + Language.Get("NCRDEFECTRATE") + " : " + defectRate + "\r\n" + Language.Get("RANGE") + " : " + spec;
            }

            return message;
        }
        #endregion
    }
}
