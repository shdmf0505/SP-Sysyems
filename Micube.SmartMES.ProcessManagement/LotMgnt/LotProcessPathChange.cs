#region using
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Grid;
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
#endregion

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 공정관리 > LOT관리 > Lot 공정이동
    /// 업  무  설  명  : Lot 공정이동
    /// 생    성    자  : 박정훈
    /// 생    성    일  : 2019-07-29
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class LotProcessPathChange : SmartConditionManualBaseForm
    {
        #region ◆ Local Variables |

        #endregion

        #region ◆ 생성자 |
        /// <summary>
        /// 생성자
        /// </summary>
        public LotProcessPathChange()
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

            this.ucDataUpDownBtnCtrl.SourceGrid = this.grdWIP;
            this.ucDataUpDownBtnCtrl.TargetGrid = this.grdTarget;

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
            groupDefaultCol.AddTextBoxColumn("PROCESSCLASSID_R", 70).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("REWORKDIVISION", 70).SetTextAlignment(TextAlignment.Center).SetIsHidden();
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
            groupDefaultCol.AddTextBoxColumn("UNIT", 60).SetTextAlignment(TextAlignment.Center).SetIsHidden();
            groupDefaultCol.AddTextBoxColumn("ISLOCKING", 60).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("ISHOLD", 60).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("PROCESSSTATE", 100).SetTextAlignment(TextAlignment.Center);

            var groupWipCol = grdWIP.View.AddGroupColumn("WIPQTY");
            groupWipCol.AddTextBoxColumn("QTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            groupWipCol.AddTextBoxColumn("PANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");

            var groupReceiveCol = grdWIP.View.AddGroupColumn("WIPRECEIVEQTY");
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

            grdWIP.View.PopulateColumns();

            #endregion

            #region - Target Grid |
            grdTarget.GridButtonItem = GridButtonItem.None;

            grdTarget.View.SetIsReadOnly();

            // CheckBox 설정
            this.grdTarget.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdTarget.View.AddTextBoxColumn("LOTID", 200).SetTextAlignment(TextAlignment.Center);
            grdTarget.View.AddTextBoxColumn("USERSEQUENCE", 70).SetTextAlignment(TextAlignment.Center);
            grdTarget.View.AddTextBoxColumn("AREANAME", 150);

            grdTarget.View.AddTextBoxColumn("QTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            grdTarget.View.AddTextBoxColumn("PANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");

            grdTarget.View.AddTextBoxColumn("LOTTYPE", 70).SetTextAlignment(TextAlignment.Center);
            grdTarget.View.AddTextBoxColumn("PRODUCTDEFID", 150).SetTextAlignment(TextAlignment.Center);
            grdTarget.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetTextAlignment(TextAlignment.Center);
            grdTarget.View.AddTextBoxColumn("PRODUCTDEFNAME", 300);
            grdTarget.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
            grdTarget.View.AddTextBoxColumn("PLANTID", 70).SetTextAlignment(TextAlignment.Center);
            grdTarget.View.AddTextBoxColumn("RTRSHT", 70).SetTextAlignment(TextAlignment.Center);
            
            grdTarget.View.AddTextBoxColumn("UNIT", 60).SetTextAlignment(TextAlignment.Center).SetIsHidden();
            grdTarget.View.AddTextBoxColumn("ISLOCKING", 60).SetTextAlignment(TextAlignment.Center);
            grdTarget.View.AddTextBoxColumn("ISHOLD", 60).SetTextAlignment(TextAlignment.Center);
            grdTarget.View.AddTextBoxColumn("PROCESSSTATE", 60).SetTextAlignment(TextAlignment.Center).SetIsHidden();

            grdTarget.View.AddTextBoxColumn("RECEIVEPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetIsHidden().SetDisplayFormat("{0:#,###}");
            grdTarget.View.AddTextBoxColumn("RECEIVEPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetIsHidden().SetDisplayFormat("{0:#,###}");

            grdTarget.View.AddTextBoxColumn("WORKSTARTPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetIsHidden().SetDisplayFormat("{0:#,###}");
            grdTarget.View.AddTextBoxColumn("WORKSTARTPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetIsHidden().SetDisplayFormat("{0:#,###}");

            grdTarget.View.AddTextBoxColumn("WORKENDPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetIsHidden().SetDisplayFormat("{0:#,###}");
            grdTarget.View.AddTextBoxColumn("WORKENDPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetIsHidden().SetDisplayFormat("{0:#,###}");

            grdTarget.View.AddTextBoxColumn("SENDPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetIsHidden().SetDisplayFormat("{0:#,###}");
            grdTarget.View.AddTextBoxColumn("SENDPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetIsHidden().SetDisplayFormat("{0:#,###}");

            grdTarget.View.AddTextBoxColumn("LEADTIME", 80).SetTextAlignment(TextAlignment.Right);
            grdTarget.View.AddTextBoxColumn("LOTINPUTDATE", 150).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("{0:yyyy-MM-dd HH:mm:ss}");
            grdTarget.View.AddTextBoxColumn("DELIVERYDATE", 150).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("{0:yyyy-MM-dd HH:mm:ss}");
            grdTarget.View.AddTextBoxColumn("LEFTDATE", 80).SetTextAlignment(TextAlignment.Center);

            grdTarget.View.PopulateColumns();
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

            // Grid Event
            grdWIP.View.CheckStateChanged += View_CheckStateChanged;
            grdWIP.View.DoubleClick += WIP_DoubleClick;
            grdWIP.View.RowStyle += WIP_RowStyle;
            grdTarget.View.FocusedRowChanged += View_FocusedRowChanged;

            // ComboBox Event
            cboSegment.Editor.EditValueChanged += cboSegment_EditValueChanged;

            // Button Event
            this.ucDataUpDownBtnCtrl.buttonClick += UcDataUpDownBtnCtrl_buttonClick;
        }

        #region ▶ Grid Event |

        #region - 재공 Grid Check Event |
        /// <summary>
        /// 재공 Grid Check Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CheckStateChanged(object sender, EventArgs e)
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
            // 재작업 체크
            int checkRework = dt.AsEnumerable().Where(c => c.Field<string>("REWORKDIVISION").Equals("Rework")).Count();
            int checkSubProcess = dt.AsEnumerable().Where(c => c.Field<string>("PROCESSPATHSTACK").Split('.').Count() > 1).Count();
            if (checkRework > 0 && checkSubProcess > 0)
            {
                grdWIP.View.CheckRow(grdWIP.View.GetFocusedDataSourceRowIndex(), false);

                // 공정 수순은 같아야 합니다. 
                throw MessageException.Create("LotMoveCheckRework");
            }

            // 샘플 라우팅 체크
            ValidateSampleRouting(dt);
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
            CommonFunction.SetGridDoubleClickCheck(grdWIP, sender);
        }
        #endregion

        #region - Target Grid Row Changed Event |
        /// <summary>
        /// Grid Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle < 0)
                return;

            if (e.FocusedRowHandle >= 0)
            {
                DataTable dt = grdTarget.DataSource as DataTable;

                if (dt == null || dt.Rows.Count <= 0) return;

                try
                {
                    string strProductID = dt.AsEnumerable().Select(r => r.Field<string>("PRODUCTDEFID")).Distinct().ToList()[0].ToString();
                    string strProductRevNo = dt.AsEnumerable().Select(r => r.Field<string>("PRODUCTDEFVERSION")).Distinct().ToList()[0].ToString();

                    // 분류 ComboBox 설정
                    cboSegment.Editor.ComboBoxColumnShowType = ComboBoxColumnShowType.All;
                    cboSegment.Editor.ValueMember = "PROCESSSEGMENTID";
                    cboSegment.Editor.DisplayMember = "PROCESSSEGMENTNAME";
                    cboSegment.Editor.SetVisibleColumns("USERSEQUENCE", "PROCESSSEGMENTNAME");

                    cboSegment.Editor.DataSource = SqlExecuter.Query("GetProcessPathByProductDefAndSequence", "10001"
                            , new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "P_PRODUCTDEFID", strProductID }, { "P_PRODUCTDEFVERSION", strProductRevNo } });
                }
                catch
                {

                }
            }
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

        #endregion

        #region ▶ ComboBox Event |

        #region - 공정 ComboBox |
        /// <summary>
        /// 공정 ComboBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboSegment_EditValueChanged(object sender, EventArgs e)
        {
            if (cboSegment.GetValue() == null || string.IsNullOrWhiteSpace(cboSegment.GetValue().ToString()))
                return;

            DataTable dt = grdTarget.DataSource as DataTable;

            if (dt == null || dt.Rows.Count <= 0) return;

            // Site 체크
            string strPlant = dt.AsEnumerable().Select(r => r.Field<string>("PLANTID")).Distinct().ToList()[0].ToString();
            string strPathPlant = ((DataRowView)cboSegment.Editor.GetSelectedDataRow())[3].ToString();

            if(!strPlant.Equals(strPathPlant))
            {
                cboSegment.EditValue = null;

                // 다른 Site는 선택할 수 없습니다.
                throw MessageException.Create("MixSelectPlantID");
            }

            string strProductID = dt.AsEnumerable().Select(r => r.Field<string>("PRODUCTDEFID")).Distinct().ToList()[0].ToString();
            string strProductRevNo = dt.AsEnumerable().Select(r => r.Field<string>("PRODUCTDEFVERSION")).Distinct().ToList()[0].ToString();
            string strProcessDefId = dt.AsEnumerable().Select(r => r.Field<string>("PROCESSDEFID")).Distinct().ToList()[0].ToString();
            string strProcessDefVersion = dt.AsEnumerable().Select(r => r.Field<string>("PROCESSDEFVERSION")).Distinct().ToList()[0].ToString();

            #region - 작업장 ComboBox |
            // 작업장 ComboBox 설정 
            cboArea.Editor.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboArea.Editor.ValueMember = "RESOURCEID";
            cboArea.Editor.DisplayMember = "RESOURCENAME";

            cboArea.Editor.DataSource = SqlExecuter.Query("GetResourceInBORByProductSegment", "10001"
                    , new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "RESOURCETYPE", "Resource" }
                                                        , { "P_PRODUCTDEFID", strProductID }
                                                        , { "P_PRODUCTDEFVERSION", strProductRevNo }
                                                        , { "P_PROCESSDEFID", strProcessDefId }
                                                        , { "P_PROCESSDEFVERSION", strProcessDefVersion }
                                                        , { "P_PROCESSSEGMENTID", cboSegment.GetValue().ToString() }});

            cboArea.Editor.ShowHeader = false;
            #endregion
        }
        #endregion
        #endregion

        #region ▶ Button Event |
        /// <summary>
        /// Up / Down UserControl Button Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UcDataUpDownBtnCtrl_buttonClick(object sender, EventArgs e)
        {
            if (!this.ucDataUpDownBtnCtrl.ButtonState.Equals("Down")) return;

            DataTable dt = grdWIP.View.GetCheckedRows();
            DataTable tgdt = grdTarget.DataSource as DataTable;
            if(Format.GetString(dt.Rows[0]["PATHTYPE"]).Equals("Start"))
            {
                throw MessageException.Create("NotMoveStartLot");
            }


            if (tgdt == null|| tgdt.Rows.Count <= 0) return;

            // 제품 ID 체크
            string productdefid = dt.AsEnumerable().Select(r => r.Field<string>("PRODUCTDEFID")).Distinct().FirstOrDefault().ToString();
            string tgproductdefid = tgdt.AsEnumerable().Select(r => r.Field<string>("PRODUCTDEFID")).Distinct().FirstOrDefault().ToString();

            if (!productdefid.Equals(tgproductdefid))
            {
                grdWIP.View.CheckRow(grdWIP.View.GetFocusedDataSourceRowIndex(), false);

                // 다른 품목 ID는 선택할 수 없습니다.
                throw MessageException.Create("MixSelectProductDefID");
            }
            
            // Version 체크
            string defVersion = dt.AsEnumerable().Select(r => r.Field<string>("PRODUCTDEFVERSION")).Distinct().FirstOrDefault().ToString();
            string tgdefVersion = tgdt.AsEnumerable().Select(r => r.Field<string>("PRODUCTDEFVERSION")).Distinct().FirstOrDefault().ToString();

            if (!defVersion.Equals(tgdefVersion))
            {
                grdWIP.View.CheckRow(grdWIP.View.GetFocusedDataSourceRowIndex(), false);

                // 다른 품목 ID는 선택할 수 없습니다.
                throw MessageException.Create("MixSelectProductDefVersion");
            }

            // Site 체크
            string plant = dt.AsEnumerable().Select(r => r.Field<string>("PLANTID")).Distinct().FirstOrDefault().ToString();
            string tgplant = tgdt.AsEnumerable().Select(r => r.Field<string>("PLANTID")).Distinct().FirstOrDefault().ToString();
            if (!plant.Equals(tgplant))
            {
                grdWIP.View.CheckRow(grdWIP.View.GetFocusedDataSourceRowIndex(), false);

                // 다른 Site는 선택할 수 없습니다.
                throw MessageException.Create("MixSelectPlantID");
            }

            // 양산 구분 체크
            string lottype = dt.AsEnumerable().Select(r => r.Field<string>("LOTTYPE")).Distinct().FirstOrDefault().ToString();
            string tglottype = tgdt.AsEnumerable().Select(r => r.Field<string>("LOTTYPE")).Distinct().FirstOrDefault().ToString();
            if (!lottype.Equals(tglottype))
            {
                grdWIP.View.CheckRow(grdWIP.View.GetFocusedDataSourceRowIndex(), false);

                // 다른 Site는 선택할 수 없습니다.
                throw MessageException.Create("MixSelectLotType");
            }

            // 공정수순 체크
            int usersequenceCount = dt.AsEnumerable().Select(r => r.Field<string>("USERSEQUENCE")).Distinct().Count();
            string usersequence = dt.AsEnumerable().Select(r => r.Field<string>("USERSEQUENCE")).Distinct().FirstOrDefault().ToString();
            string tgusersequence = tgdt.AsEnumerable().Select(r => r.Field<string>("USERSEQUENCE")).Distinct().FirstOrDefault().ToString();
            if (!usersequence.Equals(tgusersequence))
            {
                grdWIP.View.CheckRow(grdWIP.View.GetFocusedDataSourceRowIndex(), false);

                // 공정 수순은 같아야 합니다. 
                throw MessageException.Create("MixProcessPath");
            }

            // 재작업 체크
            int reworkCheck = dt.AsEnumerable().Select(c => c.Field<string>("REWORKDIVISION")).Where<string>(s => s ==  "Rework").Count();
            int checkSubProcess = dt.AsEnumerable().Where(c => c.Field<string>("PROCESSPATHSTACK").Split('.').Count() > 1).Count();
            if (reworkCheck > 0 && checkSubProcess > 0)
            {
                grdWIP.View.CheckRow(grdWIP.View.GetFocusedDataSourceRowIndex(), false);

                throw MessageException.Create("LotMoveCheckRework");
            }

            ValidateSampleRouting(dt, tgdt);
        }

        private void ValidateSampleRouting(DataTable source)
        {
            bool isSampleRoutingLotExistsInSource = false;
            string sampleLotId = null;

            foreach (DataRow each in source.Rows)
            {
                if (each["ISLOTROUTING"].ToString() == "Y")
                {
                    isSampleRoutingLotExistsInSource = true;
                    sampleLotId = each["LOTID"].ToString();
                    break;
                }
            }

            if (isSampleRoutingLotExistsInSource && source.Rows.Count > 1)
            {
                grdWIP.View.CheckRow(grdWIP.View.GetFocusedDataSourceRowIndex(), false);

                // 샘플라우팅 LOT은 한번에 한개만 공정이동 시킬 수 있습니다. {0}
                throw MessageException.Create("OnlyOneSampleLotCanMove", string.Format("LotID={0}", sampleLotId));
            }
        }

        private void ValidateSampleRouting(DataTable source, DataTable target)
        {
            bool isSampleRoutingLotExistsInSource = false;
            bool isSampleRoutingLotExistsInTarget = false;
            string sampleLotId = null;

            foreach (DataRow each in source.Rows)
            {
                if(each["ISLOTROUTING"].ToString() == "Y")
                {
                    isSampleRoutingLotExistsInSource = true;
                    sampleLotId = each["LOTID"].ToString();
                    break;
                }
            }

            foreach(DataRow each in target.Rows)
            {
                if (each["ISLOTROUTING"].ToString() == "Y")
                {
                    isSampleRoutingLotExistsInTarget = true;
                    sampleLotId = each["LOTID"].ToString();
                    break;
                }
            }

            if ((isSampleRoutingLotExistsInSource && (source.Rows.Count > 1 || target.Rows.Count > 0)) || isSampleRoutingLotExistsInTarget)
            {
                grdWIP.View.CheckRow(grdWIP.View.GetFocusedDataSourceRowIndex(), false);

                // 샘플라우팅 LOT은 한번에 한개만 공정이동 시킬 수 있습니다. {0}
                throw MessageException.Create("OnlyOneSampleLotCanMove", string.Format("LotID={0}", sampleLotId));
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

            // TODO : 저장 Rule 변경
            DataTable targetList = grdTarget.DataSource as DataTable;

            if (targetList == null || targetList.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }

            // 대상공정선택 체크
            if (this.cboSegment.EditValue == null || string.IsNullOrWhiteSpace(this.cboSegment.EditValue.ToString()))
            {
                // 대상공정선택은 필수 선택입니다.
                throw MessageException.Create("RequiredProcessSegId");
            }

            // 공정순서 체크 
            string strSequence = targetList.AsEnumerable().Select(r => r.Field<string>("USERSEQUENCE")).ToList().Min().ToString();
            string strTargetSequence = ((DataRowView)cboSegment.Editor.GetSelectedDataRow())["USERSEQUENCE"].ToString();

            if (int.Parse(strSequence) > int.Parse(strTargetSequence))
            {
                DialogResult drslt = MessageBox.Show(Language.GetMessage("ISPREVSEGMENT").Message, "Confirm", MessageBoxButtons.YesNo);
                if(drslt == DialogResult.No)
                {
                    return;
                }
            }

            // 작업장선택 체크
            if (this.cboArea.EditValue == null || string.IsNullOrWhiteSpace(this.cboArea.EditValue.ToString()))
            {
                // 작업장선택은 필수 선택입니다.
                throw MessageException.Create("NoInputArea");
            }

            // ProcessPathID
            string strProcessPathID = ((DataRowView)cboSegment.Editor.GetSelectedDataRow())[3].ToString();

            string strTransitArea = string.Empty;
            string strResourceid = string.Empty;
            if (cboArea.GetValue() != null)
            {
                strTransitArea = Format.GetFullTrimString(cboArea.Editor.GetColumnValue("AREAID"));
                strResourceid = Format.GetFullTrimString(cboArea.Editor.GetColumnValue("RESOURCEID"));
            }

            MessageWorker worker = new MessageWorker("SaveMoveProcessPath");
            worker.SetBody(new MessageBody()
            {
                { "EnterpriseID", UserInfo.Current.Enterprise },
                { "PlantID", UserInfo.Current.Plant },
                { "ProcessSegmentID", this.cboSegment.EditValue.ToString() },
                { "ProcessPathID", strProcessPathID },
                { "FromSequence", strSequence },
                { "ToSequence", strTargetSequence },
                { "AreaID", strTransitArea },
                { "Resource", strResourceid },
                { "Comments", txtComment.Text },
                { "UserId", UserInfo.Current.Id },
                { "Lotlist", targetList }
            });

            worker.Execute();

            // 데이터 초기화
            SetInitControl();
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
            this.grdWIP.DataSource = null;
            this.grdTarget.DataSource = null;

            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("P_PROCESSSTATE", "WaitForReceive");
            values.Add("P_LOTSTATE", "InProduction"); 

            DataTable dt = await SqlExecuter.QueryAsync("SelectWIPList", "10006", values);

            if (dt.Rows.Count < 1)
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdWIP.DataSource = dt;

            SetInitControl();
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
            this.cboSegment.EditValue = null;
            this.cboArea.EditValue = null;
            this.txtComment.Text = string.Empty;
        } 
        #endregion
        #endregion
    }
}
