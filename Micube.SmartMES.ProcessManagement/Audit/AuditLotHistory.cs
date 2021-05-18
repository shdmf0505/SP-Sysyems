#region using
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons;
using Micube.Framework.SmartControls.Grid.BandedGrid;
using Micube.Framework.SmartControls.Grid;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraTreeList;
using Micube.SmartMES.Commons.Controls;
#endregion

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 공정관리 > 공정작업 > Audit Lot History
    /// 업  무  설  명  : Lot History
    /// 생    성    자  : 박정훈
    /// 생    성    일  : 2019-11-20
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class AuditLotHistory : SmartConditionManualBaseForm
    {
        #region ◆ Local Variables |

        #endregion

        #region ◆ 생성자 |
        /// <summary>
        /// 생성자
        /// </summary>
        public AuditLotHistory()
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
                //Conditions.GetCondition<ConditionItemSelectPopup>("P_PRODUCTDEFID")
                Conditions.GetControl<SmartSelectPopupEdit>("LOTID").SetValue(Format.GetString(parameters["LOTID"]));
                OnSearchAsync();
            }
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
            InitializeTreeList();
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
                , "DEFECTUNIT", "STEPTYPE", "PROCESSSEGMENTTYPE", "STEPTYPE", "DURABLEDEFID", "PROCESSSTATE", "PANELPERQTY", "MM", "PCSPNL");
            #endregion

            #region - Lot Routing 이력 |
            grdLotRouting.GridButtonItem = GridButtonItem.Export;

            grdLotRouting.View.SetIsReadOnly();
            grdLotRouting.SetIsUseContextMenu(false);

            var ghistLotRoutingCol = grdLotRouting.View.AddGroupColumn("MANUFACTURINGHISTORY");
            ghistLotRoutingCol.AddTextBoxColumn("LOTID", 180).SetTextAlignment(TextAlignment.Center);
            //ghistLotRoutingCol.AddTextBoxColumn("PATHSEQUENCE", 70).SetIsHidden();
            ghistLotRoutingCol.AddTextBoxColumn("PROCESSSEQUENCE", 60).SetTextAlignment(TextAlignment.Center);
            //ghistLotRoutingCol.AddTextBoxColumn("WORKTYPE", 70).SetTextAlignment(TextAlignment.Center);

            ghistLotRoutingCol.AddTextBoxColumn("PROCESSSEGMENTID", 150).SetIsHidden();
            ghistLotRoutingCol.AddTextBoxColumn("PROCESSSEGMENTNAME", 150);
            //ghistLotRoutingCol.AddTextBoxColumn("PLANTID", 60).SetTextAlignment(TextAlignment.Center);
            //ghistLotRoutingCol.AddTextBoxColumn("AREANAME", 150);

            // 작업일자
            var ghistLotDateCol = grdLotRouting.View.AddGroupColumn("WORKDATE");
            ghistLotDateCol.AddTextBoxColumn("RECEIVEDATE", 140).SetTextAlignment(TextAlignment.Center);
            ghistLotDateCol.AddTextBoxColumn("STARTDATE", 140).SetTextAlignment(TextAlignment.Center);
            ghistLotDateCol.AddTextBoxColumn("WORKENDDATE", 140).SetTextAlignment(TextAlignment.Center);
            ghistLotDateCol.AddTextBoxColumn("LOTSENDDATE", 140).SetTextAlignment(TextAlignment.Center);

            // 인수수량정보
            var ghistLotINQTYCol = grdLotRouting.View.AddGroupColumn("INQTY");
            ghistLotINQTYCol.AddTextBoxColumn("RECEIVEPCSQTY", 80).SetDisplayFormat("{0:#,###}");
            ghistLotINQTYCol.AddTextBoxColumn("RECEIVEPANELQTY", 80).SetDisplayFormat("{0:#,###}");

            // 작업 시작수량정보
            var ghistLotSTARTQTYCol = grdLotRouting.View.AddGroupColumn("WIPSTARTQTY");
            ghistLotSTARTQTYCol.AddTextBoxColumn("WORKSTARTPCSQTY", 80).SetDisplayFormat("{0:#,###}");
            ghistLotSTARTQTYCol.AddTextBoxColumn("WORKSTARTPANELQTY", 80).SetDisplayFormat("{0:#,###}");

            // 작업 완료수량정보
            var ghistLotENDQTYCol = grdLotRouting.View.AddGroupColumn("WIPENDQTY");
            ghistLotENDQTYCol.AddTextBoxColumn("WORKENDPCSQTY", 80).SetDisplayFormat("{0:#,###}");
            ghistLotENDQTYCol.AddTextBoxColumn("WORKENDPANELQTY", 80).SetDisplayFormat("{0:#,###}");

            // 인계수량정보
            var ghistLotSENDQTYCol = grdLotRouting.View.AddGroupColumn("WIPSENDQTY");
            ghistLotSENDQTYCol.AddTextBoxColumn("SENDPCSQTY", 80).SetDisplayFormat("{0:#,###}");
            ghistLotSENDQTYCol.AddTextBoxColumn("SENDPANELQTY", 80).SetDisplayFormat("{0:#,###}");

            // LEADTIME
            var ghistLotLTCol = grdLotRouting.View.AddGroupColumn("LEADTIME");
            ghistLotLTCol.AddTextBoxColumn("RECEIVELEADTIME", 80).SetDisplayFormat("{0:N2}");
            ghistLotLTCol.AddTextBoxColumn("WORKSTARTLEADTIME", 80).SetDisplayFormat("{0:N2}");
            ghistLotLTCol.AddTextBoxColumn("WORKENDLEADTIME", 80).SetDisplayFormat("{0:N2}");
            ghistLotLTCol.AddTextBoxColumn("SENDLEADTIME", 80).SetDisplayFormat("{0:N2}");
            ghistLotLTCol.AddTextBoxColumn("LEADTIME", 80).SetDisplayFormat("{0:N2}");
            ghistLotLTCol.AddTextBoxColumn("CUM_LEADTIME", 80).SetDisplayFormat("{0:N2}");

            // DEFECT
            //var ghistDefectCol = grdLotRouting.View.AddGroupColumn("DEFECT");
            //ghistDefectCol.AddTextBoxColumn("DEFECTQTY", 80).SetDisplayFormat("{0:#,###}");
            //ghistDefectCol.AddTextBoxColumn("CUM_DEFECTQTY", 80).SetDisplayFormat("{0:#,###.##}");

            grdLotRouting.View.PopulateColumns();
            #endregion

            #region - 계측값 |
            grdInspectionMeasure.GridButtonItem = GridButtonItem.Export;

            grdInspectionMeasure.View.SetIsReadOnly();

            grdInspectionMeasure.View.AddTextBoxColumn("LOTID", 190).SetTextAlignment(TextAlignment.Center);
            grdInspectionMeasure.View.AddTextBoxColumn("PRODUCTDEFID", 70).SetIsHidden();
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
            //grdConsumable.View.AddTextBoxColumn("AREANAME", 150);
            grdConsumable.View.AddTextBoxColumn("CONSUMABLEDEFID", 140).SetTextAlignment(TextAlignment.Center);
            grdConsumable.View.AddTextBoxColumn("CONSUMABLEDEFNAME", 280);
            grdConsumable.View.AddTextBoxColumn("CONSUMABLELOTID", 170).SetTextAlignment(TextAlignment.Center);
            grdConsumable.View.AddTextBoxColumn("UOM", 60).SetTextAlignment(TextAlignment.Center);
            grdConsumable.View.AddTextBoxColumn("CONSUMEDQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###.##}");

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
            //grdDurable.View.AddTextBoxColumn("AREANAME", 150);
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

            grdEquipment.View.AddTextBoxColumn("LOTID", 190).SetTextAlignment(TextAlignment.Center);
            grdEquipment.View.AddTextBoxColumn("PATHSEQUENCE", 70).SetIsHidden();
            grdEquipment.View.AddTextBoxColumn("USERSEQUENCE", 60).SetTextAlignment(TextAlignment.Center);
            grdEquipment.View.AddTextBoxColumn("PROCESSSEGMENTID", 150).SetIsHidden();
            grdEquipment.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150);
            //grdEquipment.View.AddTextBoxColumn("AREANAME", 150);
            grdEquipment.View.AddTextBoxColumn("EQUIPMENTID", 100).SetTextAlignment(TextAlignment.Center).SetIsHidden();
            grdEquipment.View.AddTextBoxColumn("EQUIPMENTNAME", 220);
            grdEquipment.View.AddTextBoxColumn("WORKSTARTTIME", 140).SetTextAlignment(TextAlignment.Center);
            grdEquipment.View.AddTextBoxColumn("WORKENDDATE", 140).SetTextAlignment(TextAlignment.Center);

            grdEquipment.View.OptionsView.AllowCellMerge = true; // CellMerge
            grdEquipment.View.PopulateColumns();
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
            //grdInkjet.View.AddTextBoxColumn("AREANAME", 150);
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
            //grdQR.View.AddTextBoxColumn("AREANAME", 150);
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
            //grdFilm.View.AddTextBoxColumn("AREANAME", 150);
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
            grdWTIME.View.AddTextBoxColumn("SETTIME", 120).SetTextAlignment(TextAlignment.Center);
            grdWTIME.View.AddTextBoxColumn("OCCURETIME", 120).SetTextAlignment(TextAlignment.Center);
            grdWTIME.View.AddTextBoxColumn("EXECUTETIME", 70).SetTextAlignment(TextAlignment.Center);
            grdWTIME.View.AddTextBoxColumn("WTIMELIMIT", 120).SetTextAlignment(TextAlignment.Center);

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
            //grdShipping.View.AddTextBoxColumn("AREANAME", 120);
            grdShipping.View.AddTextBoxColumn("INSPECTINRESULT", 80).SetTextAlignment(TextAlignment.Center);
            grdShipping.View.AddTextBoxColumn("INSPECTORNAME", 120).SetTextAlignment(TextAlignment.Center);
            grdShipping.View.AddTextBoxColumn("INSPECTIONDEFID", 100).SetIsHidden();

            grdShipping.View.PopulateColumns();
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
            //grdMessage.View.AddTextBoxColumn("AREANAME", 150);
            grdMessage.View.AddTextBoxColumn("MESSAGETYPE", 140).SetTextAlignment(TextAlignment.Center);
            grdMessage.View.AddTextBoxColumn("WRITER", 60).SetTextAlignment(TextAlignment.Center);
            grdMessage.View.AddTextBoxColumn("WRITEDATE", 140).SetTextAlignment(TextAlignment.Center);

            grdMessage.View.PopulateColumns();
            #endregion
        }
        #endregion

        #region ▶ 화면 Control 설정 |
        /// <summary>
        /// 화면 Control 설정
        /// </summary>
        private void InitializeControls()
        {
        }
        #endregion

        #region ▶ TreeList 설정
        /// <summary>
        /// LOT 가계도 TREELIST 
        /// </summary>
        private void InitializeTreeList()
        {
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

            // Tab Index Change
            this.tabLotHist.SelectedPageChanged += TabHistory_SelectedPageChanged;

            // Grid Event
            grdLotRouting.View.DoubleClick += LotRoutingView_DoubleClick;
            this.grdMessage.View.RowClick += MessageView_RowClick;
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
                case 0: // 계측값
                    getMeasureValue();
                    break;
                case 1: // 원부자재
                    getConsumableMaterial();
                    break;
                case 2: // 치공구
                    getDurable();
                    break;
                case 3: // 설비
                    getEquipment();
                    break;
                case 4: // 주차정보
                    getWeek();
                    break;
                case 5: // FILM
                    getFilm();
                    break;
                case 6: // Q-Time
                    getWTIME();
                    break;
                case 7: // 출하정보
                    getShippingInfo();
                    break;
                case 8: // 메시지 정보
                    getMessage();
                    break;
                default:
                    getMeasureValue();
                    break;
            }
        }
        #endregion

        #region ▶ Grid Event |

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

            Search(lotid);

            pnlContent.CloseWaitArea();
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

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("P_TXNHISTKEY", dr["TXNHISTKEY"].ToString()); 
            param.Add("P_LOTID", dr["LOTID"].ToString());
            param.Add("P_PROCESSSEGMENTID", dr["PROCESSSEGMENTID"].ToString()); 
            param.Add("P_USERSEQUENCE", dr["USERSEQUENCE"].ToString());

            DataTable dt = SqlExecuter.Query("SelectLotMessage", "10001", param);

            if (dt.Rows.Count < 1)
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            this.txtTitle.Text = dt.Rows[0]["TITLE"].ToString();
            this.txtComment.Rtf = dt.Rows[0]["MESSAGE"].ToString();
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

            Search("");
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

            grdLotRouting.View.ClearDatas();
            grdInspectionMeasure.View.ClearDatas();
            grdConsumable.View.ClearDatas();
            grdDurable.View.ClearDatas();
            grdInkjet.View.ClearDatas();
            grdFilm.View.ClearDatas();
            grdWTIME.View.ClearDatas();
            grdShipping.View.ClearDatas();
            grdMessage.View.ClearDatas();

            txtTitle.Text = string.Empty;
            txtComment.Rtf = string.Empty;
        }
        #endregion

        #region ▶ LOT 정보 및 LOT 가계도별 이력 조회 |
        /// <summary>
        /// LOT 정보 및 LOT 가계도별 이력 조회
        /// </summary>
        /// <param name="lotid">LotID</param>
        private void Search(string lotid)
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
            getLotHistory(grdLotRouting, lotid);

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

        #endregion
    }
}
