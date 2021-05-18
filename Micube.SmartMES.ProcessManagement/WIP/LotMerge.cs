#region using
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons;
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

using DevExpress.XtraGrid.Views.Base;
#endregion

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 공정관리 > 공정작업 > Lot Merge
    /// 업  무  설  명  : Lot Merge
    /// 생    성    자  : 박정훈
    /// 생    성    일  : 2019-08-21
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class LotMerge : SmartConditionManualBaseForm
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
        public LotMerge()
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

            smartGroupBox1.Width = 500;

            smartGroupBox1.Height = 500;
            smartGroupBox2.Height = 500;

            this.ucDataUpDownBtnCtrl.SourceGrid = this.grdWIP;
            this.ucDataUpDownBtnCtrl.TargetGrid = this.grdTarget;

            this.ucDataUpDownBtnCtrl2.SourceGrid = this.grdWIP;
            this.ucDataUpDownBtnCtrl2.TargetGrid = this.grdSource;

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
            groupDefaultCol.AddTextBoxColumn("PROCESSDEFID", 80).SetIsHidden();
            groupDefaultCol.AddTextBoxColumn("PROCESSSEGMENTID", 80).SetIsHidden();
            groupDefaultCol.AddTextBoxColumn("USERSEQUENCE", 70).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
            groupDefaultCol.AddTextBoxColumn("PLANTID", 70).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("AREAID", 80).SetIsHidden();
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

            var groupSendCol = grdWIP.View.AddGroupColumn("WAITFORRECEIVE");
            groupSendCol.AddTextBoxColumn("SENDPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            groupSendCol.AddTextBoxColumn("SENDPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");

            var groupReceiveCol = grdWIP.View.AddGroupColumn("ACCEPT");
            groupReceiveCol.AddTextBoxColumn("RECEIVEPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            groupReceiveCol.AddTextBoxColumn("RECEIVEPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");

            var groupWorkStartCol = grdWIP.View.AddGroupColumn("WIPSTARTQTY");
            groupWorkStartCol.AddTextBoxColumn("WORKSTARTPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            groupWorkStartCol.AddTextBoxColumn("WORKSTARTPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");

            var groupWorkEndCol = grdWIP.View.AddGroupColumn("WIPENDQTY");
            groupWorkEndCol.AddTextBoxColumn("WORKENDPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            groupWorkEndCol.AddTextBoxColumn("WORKENDPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");

            var groupWIPCol = grdWIP.View.AddGroupColumn("WIPLIST");
            groupWIPCol.AddTextBoxColumn("LEADTIME", 80).SetTextAlignment(TextAlignment.Right);
            groupWIPCol.AddTextBoxColumn("LOTINPUTDATE", 150).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("{0:yyyy-MM-dd HH:mm:ss}");
            groupWIPCol.AddTextBoxColumn("DELIVERYDATE", 150).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("{0:yyyy-MM-dd HH:mm:ss}");
            groupWIPCol.AddTextBoxColumn("LEFTDATE", 80).SetTextAlignment(TextAlignment.Center);

            grdWIP.View.PopulateColumns();

            #endregion

            #region - Target Grid |
            grdTarget.GridButtonItem = GridButtonItem.Delete;
            grdTarget.View.SetIsReadOnly();

            // CheckBox 설정
            this.grdTarget.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdTarget.View.AddTextBoxColumn("LOTID", 200).SetTextAlignment(TextAlignment.Center);

            grdTarget.View.AddTextBoxColumn("LOTTYPE", 70).SetTextAlignment(TextAlignment.Center);
            grdTarget.View.AddTextBoxColumn("AREANAME", 150);
            grdTarget.View.AddTextBoxColumn("USERSEQUENCE", 70).SetTextAlignment(TextAlignment.Center);
            grdTarget.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
            grdTarget.View.AddTextBoxColumn("UNIT", 60).SetTextAlignment(TextAlignment.Center);
            grdTarget.View.AddTextBoxColumn("QTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            grdTarget.View.AddTextBoxColumn("PANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            grdTarget.View.AddTextBoxColumn("PRODUCTDEFID", 150).SetTextAlignment(TextAlignment.Center);
            grdTarget.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetTextAlignment(TextAlignment.Center);
            grdTarget.View.AddTextBoxColumn("PRODUCTDEFNAME", 300);
            grdTarget.View.AddTextBoxColumn("PLANTID", 70).SetTextAlignment(TextAlignment.Center);
            grdTarget.View.AddTextBoxColumn("RTRSHT", 70).SetTextAlignment(TextAlignment.Center);

            grdTarget.View.AddTextBoxColumn("PROCESSDEFID", 80).SetIsHidden();
            grdTarget.View.AddTextBoxColumn("AREAID", 80).SetIsHidden();
            grdTarget.View.AddTextBoxColumn("PROCESSSEGMENTID", 80).SetIsHidden();
            grdTarget.View.AddTextBoxColumn("ISLOCKING", 60).SetTextAlignment(TextAlignment.Center).SetIsHidden();
            grdTarget.View.AddTextBoxColumn("ISHOLD", 60).SetTextAlignment(TextAlignment.Center).SetIsHidden();
            grdTarget.View.AddTextBoxColumn("PROCESSSTATE", 60).SetTextAlignment(TextAlignment.Center).SetIsHidden();
            grdTarget.View.AddTextBoxColumn("RECEIVEPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetIsHidden();
            grdTarget.View.AddTextBoxColumn("RECEIVEPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetIsHidden();
            grdTarget.View.AddTextBoxColumn("WORKSTARTPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetIsHidden();
            grdTarget.View.AddTextBoxColumn("WORKSTARTPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetIsHidden();
            grdTarget.View.AddTextBoxColumn("WORKENDPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetIsHidden();
            grdTarget.View.AddTextBoxColumn("WORKENDPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetIsHidden();
            grdTarget.View.AddTextBoxColumn("SENDPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetIsHidden();
            grdTarget.View.AddTextBoxColumn("SENDPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetIsHidden();
            grdTarget.View.AddTextBoxColumn("LEADTIME", 80).SetTextAlignment(TextAlignment.Right).SetIsHidden();
            grdTarget.View.AddTextBoxColumn("LOTINPUTDATE", 150).SetTextAlignment(TextAlignment.Center).SetIsHidden();
            grdTarget.View.AddTextBoxColumn("DELIVERYDATE", 150).SetTextAlignment(TextAlignment.Center).SetIsHidden();
            grdTarget.View.AddTextBoxColumn("LEFTDATE", 80).SetTextAlignment(TextAlignment.Center).SetIsHidden();

            grdTarget.View.PopulateColumns();
            #endregion

            #region - Target Grid |
            grdSource.GridButtonItem = GridButtonItem.Delete;
            grdSource.View.SetIsReadOnly();

            // CheckBox 설정
            this.grdSource.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdSource.View.AddTextBoxColumn("LOTID", 200).SetTextAlignment(TextAlignment.Center);

            grdSource.View.AddTextBoxColumn("LOTTYPE", 70).SetTextAlignment(TextAlignment.Center);
            grdSource.View.AddTextBoxColumn("AREANAME", 150);
            grdSource.View.AddTextBoxColumn("USERSEQUENCE", 70).SetTextAlignment(TextAlignment.Center);
            grdSource.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
            grdSource.View.AddTextBoxColumn("UNIT", 60).SetTextAlignment(TextAlignment.Center);
            grdSource.View.AddTextBoxColumn("QTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            grdSource.View.AddTextBoxColumn("PANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            grdSource.View.AddTextBoxColumn("PRODUCTDEFID", 150).SetTextAlignment(TextAlignment.Center);
            grdSource.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetTextAlignment(TextAlignment.Center);
            grdSource.View.AddTextBoxColumn("PRODUCTDEFNAME", 300);
            grdSource.View.AddTextBoxColumn("PLANTID", 70).SetTextAlignment(TextAlignment.Center);
            grdSource.View.AddTextBoxColumn("RTRSHT", 70).SetTextAlignment(TextAlignment.Center);

            grdSource.View.AddTextBoxColumn("PROCESSDEFID", 80).SetIsHidden();
            grdSource.View.AddTextBoxColumn("AREAID", 80).SetIsHidden();
            grdSource.View.AddTextBoxColumn("PROCESSSEGMENTID", 80).SetIsHidden();
            grdSource.View.AddTextBoxColumn("ISLOCKING", 60).SetTextAlignment(TextAlignment.Center).SetIsHidden();
            grdSource.View.AddTextBoxColumn("ISHOLD", 60).SetTextAlignment(TextAlignment.Center).SetIsHidden();
            grdSource.View.AddTextBoxColumn("PROCESSSTATE", 60).SetTextAlignment(TextAlignment.Center).SetIsHidden();
            grdSource.View.AddTextBoxColumn("RECEIVEPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetIsHidden();
            grdSource.View.AddTextBoxColumn("RECEIVEPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetIsHidden();
            grdSource.View.AddTextBoxColumn("WORKSTARTPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetIsHidden();
            grdSource.View.AddTextBoxColumn("WORKSTARTPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetIsHidden();
            grdSource.View.AddTextBoxColumn("WORKENDPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetIsHidden();
            grdSource.View.AddTextBoxColumn("WORKENDPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetIsHidden();
            grdSource.View.AddTextBoxColumn("SENDPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetIsHidden();
            grdSource.View.AddTextBoxColumn("SENDPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetIsHidden();
            grdSource.View.AddTextBoxColumn("LEADTIME", 80).SetTextAlignment(TextAlignment.Right).SetIsHidden();
            grdSource.View.AddTextBoxColumn("LOTINPUTDATE", 150).SetTextAlignment(TextAlignment.Center).SetIsHidden();
            grdSource.View.AddTextBoxColumn("DELIVERYDATE", 150).SetTextAlignment(TextAlignment.Center).SetIsHidden();
            grdSource.View.AddTextBoxColumn("LEFTDATE", 80).SetTextAlignment(TextAlignment.Center).SetIsHidden();

            grdSource.View.PopulateColumns();
            #endregion

        }
        #endregion

        #region ▶ 화면 Control 설정 |
        /// <summary>
        /// 화면 Control 설정
        /// </summary>
        private void InitializeControls()
        {
            grdWIP.Height = 450;
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

            // Grid Event
            grdWIP.View.DoubleClick += View_DoubleClick;
            grdWIP.View.RowStyle += WIP_RowStyle;
            grdWIP.View.CheckStateChanged += WIP_CheckStateChanged;

            grdTarget.View.RowStyle += Target_RowStyle;

            // Button Event
            this.ucDataUpDownBtnCtrl.buttonClick += UcDataUpDownBtnCtrl_buttonClick;
            this.ucDataUpDownBtnCtrl2.buttonClick += UcDataUpDownBtnCtrl2_buttonClick;
        }

        #region ▶ Grid Event |

        #region - 재공 Grid 더블클릭 |
        /// <summary>
        /// 재공 Grid 더블클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_DoubleClick(object sender, EventArgs e)
        {
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
            CommonFunction.SetGridRowStyle(grdWIP, e);
        }
        #endregion

        #region - 재공 Grid Check Event |
        /// <summary>
        /// 재공 Grid Check Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WIP_CheckStateChanged(object sender, EventArgs e)
        {
            GridCheckMarksSelection view = (GridCheckMarksSelection)sender;

            DataTable dt = grdWIP.View.GetCheckedRows();

            // 제품 ID 체크
            int productCount = dt.AsEnumerable().Select(r => r.Field<string>("PRODUCTDEFID")).Distinct().Count();

            if (productCount > 1)
            {
                grdWIP.View.CheckRow(grdWIP.View.GetFocusedDataSourceRowIndex(), false);

                // 다른 품목 ID는 선택할 수 없습니다.
                throw MessageException.Create("MixSelectProductDefID");
            }

            // Version 체크
            int versionCount = dt.AsEnumerable().Select(r => r.Field<string>("PRODUCTDEFVERSION")).Distinct().Count();
            if (versionCount > 1)
            {
                grdWIP.View.CheckRow(grdWIP.View.GetFocusedDataSourceRowIndex(), false);

                // 다른 품목 ID는 선택할 수 없습니다.
                throw MessageException.Create("MixSelectProductDefVersion");
            }

            // Site 체크
            int plntCount = dt.AsEnumerable().Select(r => r.Field<string>("PLANTID")).Distinct().Count();
            if (plntCount > 1)
            {
                grdWIP.View.CheckRow(grdWIP.View.GetFocusedDataSourceRowIndex(), false);

                // 다른 Site는 선택할 수 없습니다.
                throw MessageException.Create("MixSelectPlantID");
            }

            // 양산 구분 체크
            int lottypeCount = dt.AsEnumerable().Select(r => r.Field<string>("LOTTYPE")).Distinct().Count();
            if (lottypeCount > 1)
            {
                grdWIP.View.CheckRow(grdWIP.View.GetFocusedDataSourceRowIndex(), false);

                // 다른 Site는 선택할 수 없습니다.
                throw MessageException.Create("MixSelectLotType");
            }

            // 공정수순 체크
            int usersequenceCount = dt.AsEnumerable().Select(r => r.Field<string>("USERSEQUENCE")).Distinct().Count();
            if (usersequenceCount > 1)
            {
                grdWIP.View.CheckRow(grdWIP.View.GetFocusedDataSourceRowIndex(), false);

                // 공정 수순은 같아야 합니다. 
                throw MessageException.Create("MixProcessPath");
            }
        }
        #endregion

        #region - Target Grid Row Stype Event |
        /// <summary>
        /// Target Grid Row Stype Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Target_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            CommonFunction.SetGridRowStyle(grdTarget, e);
        } 
        #endregion

        #endregion

        #region ▶ Up / Down User Control Button Click Event |
        /// <summary>
        /// Up / Down User Control Button Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UcDataUpDownBtnCtrl_buttonClick(object sender, EventArgs e)
        {
            if(this.ucDataUpDownBtnCtrl.ButtonState.Equals("Down"))
            {
                DataTable dt = grdWIP.View.GetCheckedRows();

                if(dt == null || dt.Rows.Count > 1)
                {
                    // 기준 LOT은 하나이상 선택하실 수 없습니다.
                    throw MessageException.Create("CannotSelect1OverTargetLot");
                }
            }
        }
        #endregion

        #region ▶ Up / Down User Control Button Click Event |
        /// <summary>
        /// Up / Down User Control Button Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UcDataUpDownBtnCtrl2_buttonClick(object sender, EventArgs e)
        {
            if (this.ucDataUpDownBtnCtrl2.ButtonState.Equals("Down"))
            {
                DataTable dt = grdTarget.DataSource as DataTable;

                if(dt == null || dt.Rows.Count == 0)
                {
                    // 기준 LOT을 먼저 선택하여 주십시오.
                    throw MessageException.Create("NeedTargetMergeLot"); 
                }

                DataTable sgdt = grdWIP.View.GetCheckedRows();

                if (sgdt == null || sgdt.Rows.Count <= 0) return;

                // 제품 ID 체크
                string productdefid = dt.AsEnumerable().Select(r => r.Field<string>("PRODUCTDEFID")).Distinct().FirstOrDefault().ToString();
                string tgproductdefid = sgdt.AsEnumerable().Select(r => r.Field<string>("PRODUCTDEFID")).Distinct().FirstOrDefault().ToString();

                if (!productdefid.Equals(tgproductdefid))
                {
                    grdWIP.View.CheckRow(grdWIP.View.GetFocusedDataSourceRowIndex(), false);

                    // 다른 품목 ID는 선택할 수 없습니다.
                    throw MessageException.Create("MixSelectProductDefID");
                }

                // Version 체크
                string defVersion = dt.AsEnumerable().Select(r => r.Field<string>("PRODUCTDEFVERSION")).Distinct().FirstOrDefault().ToString();
                string tgdefVersion = sgdt.AsEnumerable().Select(r => r.Field<string>("PRODUCTDEFVERSION")).Distinct().FirstOrDefault().ToString();

                if (!defVersion.Equals(tgdefVersion))
                {
                    grdWIP.View.CheckRow(grdWIP.View.GetFocusedDataSourceRowIndex(), false);

                    // 다른 품목 ID는 선택할 수 없습니다.
                    throw MessageException.Create("MixSelectProductDefVersion");
                }

                // Site 체크
                string plant = dt.AsEnumerable().Select(r => r.Field<string>("PLANTID")).Distinct().FirstOrDefault().ToString();
                string tgplant = sgdt.AsEnumerable().Select(r => r.Field<string>("PLANTID")).Distinct().FirstOrDefault().ToString();
                if (!plant.Equals(tgplant))
                {
                    grdWIP.View.CheckRow(grdWIP.View.GetFocusedDataSourceRowIndex(), false);

                    // 다른 Site는 선택할 수 없습니다.
                    throw MessageException.Create("MixSelectPlantID");
                }

                // 양산 구분 체크
                string lottype = dt.AsEnumerable().Select(r => r.Field<string>("LOTTYPE")).Distinct().FirstOrDefault().ToString();
                string tglottype = sgdt.AsEnumerable().Select(r => r.Field<string>("LOTTYPE")).Distinct().FirstOrDefault().ToString();
                if (!lottype.Equals(tglottype))
                {
                    grdWIP.View.CheckRow(grdWIP.View.GetFocusedDataSourceRowIndex(), false);

                    // 다른 Site는 선택할 수 없습니다.
                    throw MessageException.Create("MixSelectLotType");
                }

                // 공정수순 체크
                int usersequenceCount = dt.AsEnumerable().Select(r => r.Field<string>("USERSEQUENCE")).Distinct().Count();
                string usersequence = dt.AsEnumerable().Select(r => r.Field<string>("USERSEQUENCE")).Distinct().FirstOrDefault().ToString();
                string tgusersequence = sgdt.AsEnumerable().Select(r => r.Field<string>("USERSEQUENCE")).Distinct().FirstOrDefault().ToString();
                if (!usersequence.Equals(tgusersequence))
                {
                    grdWIP.View.CheckRow(grdWIP.View.GetFocusedDataSourceRowIndex(), false);

                    // 공정 수순은 같아야 합니다. 
                    throw MessageException.Create("MixProcessPath");
                }
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

            SaveRule();

            // 데이터 초기화
            SetInitControl();
        }

        #region ▶ 데이터 저장 |
        /// <summary>
        /// 데이터 저장
        /// </summary>
        private void SaveRule()
        {
            // TODO : 저장 Rule 변경

            DataTable targetList = grdSource.DataSource as DataTable;

            if (targetList == null || targetList.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }

            DataTable tRow = grdTarget.DataSource as DataTable;

            if(tRow == null)
            {
                // 기준 LOT을 먼저 선택하여 주십시오.
                throw MessageException.Create("NeedTargetMergeLot");
            }

            // Target Lot (병합이 되는 대표 LOT)
            string tgLot = tRow.Rows[0]["LOTID"].ToString();

            MessageWorker worker = new MessageWorker("SaveLotMerge");
            worker.SetBody(new MessageBody()
            {
                { "EnterpriseID", UserInfo.Current.Enterprise },
                { "PlantID", UserInfo.Current.Plant },
                { "UserId", UserInfo.Current.Id },
                { "LotId", tgLot },
                { "Lotlist", targetList }
            });

            worker.Execute();

            SetInitControl();
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

            DataTable dt = await SqlExecuter.QueryAsync("SelectWIPList", "10001", values);

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
            this.grdTarget.View.ClearDatas();
        }
        #endregion

        #endregion
    }
}
