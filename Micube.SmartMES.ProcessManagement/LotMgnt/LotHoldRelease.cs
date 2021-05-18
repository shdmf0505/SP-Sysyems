#region using
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
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
    /// 프 로 그 램 명  : 공정관리 > LOT관리 > Lot Release Hold
    /// 업  무  설  명  : Lot Hold 해제 설정
    /// 생    성    자  : 박정훈
    /// 생    성    일  : 2019-07-29
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class LotHoldRelease : SmartConditionManualBaseForm
    {
        #region ◆ Local Variables |

        #endregion

        #region ◆ 생성자 |
        /// <summary>
        /// 생성자
        /// </summary>
        public LotHoldRelease()
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

            this.ucDataUpDownBtnCtrl.SourceGrid = this.grdHold;
            this.ucDataUpDownBtnCtrl.TargetGrid = this.grdRelease;

            InitializeEvent();
        }

        #region ▶ 조회조건 설정 |
        /// <summary>
        /// 검색조건 설정sel
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // Lot
            CommonFunction.AddConditionLotPopup("P_LOTID", 0.1, true, Conditions);
            // 품목
            CommonFunction.AddConditionProductPopup("P_PRODUCTDEFID", 0.5, true, Conditions);

            // 작업장
            CommonFunction.AddConditionAreaByAuthorityPopup("P_AREAID", 2.5, true, Conditions, false, true);

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
        }

        /// <summary>
        /// 조회조건 설정
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            Conditions.GetControl<SmartComboBox>("P_PRODUCTDEFTYPE").EditValue = "Product";
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
            #region - Hold List Grid 설정 |
            grdHold.GridButtonItem = GridButtonItem.Export;

            grdHold.View.SetIsReadOnly();

            // CheckBox 설정
            this.grdHold.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            var groupDefaultCol = grdHold.View.AddGroupColumn("HOLDLIST");
            groupDefaultCol.AddTextBoxColumn("TXNHISTKEY", 100).SetTextAlignment(TextAlignment.Center).SetIsHidden(); 
            groupDefaultCol.AddTextBoxColumn("LOTTYPE", 70).SetTextAlignment(TextAlignment.Center); 
            groupDefaultCol.AddTextBoxColumn("PRODUCTDEFID", 150).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            groupDefaultCol.AddTextBoxColumn("USERSEQUENCE", 70).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
            groupDefaultCol.AddTextBoxColumn("PLANTID", 70).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("AREANAME", 70).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("RTRSHT", 70).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("PROCESSSTATE", 90).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("LOTID", 200).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("UNIT", 60).SetTextAlignment(TextAlignment.Center);

            var groupWipCol = grdHold.View.AddGroupColumn("WIPQTY");
            groupWipCol.AddTextBoxColumn("QTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            groupWipCol.AddTextBoxColumn("PANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");

            var groupLockCol = grdHold.View.AddGroupColumn("HOLDINFO");
            groupLockCol.AddTextBoxColumn("ISHOLD", 100).SetTextAlignment(TextAlignment.Center);
            groupLockCol.AddTextBoxColumn("HOLDTOPCLASS", 100).SetTextAlignment(TextAlignment.Center);
            //groupLockCol.AddTextBoxColumn("HOLDMIDDLECLASS", 100).SetTextAlignment(TextAlignment.Center);
            groupLockCol.AddTextBoxColumn("HOLDREASON", 150).SetTextAlignment(TextAlignment.Center);
            groupLockCol.AddTextBoxColumn("HOLDUSER", 100).SetTextAlignment(TextAlignment.Center);
            groupLockCol.AddTextBoxColumn("HOLDDATE", 150).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("{0:yyyy-MM-dd HH:mm:ss}");
            groupLockCol.AddTextBoxColumn("RELEASEHOLDUSER", 100).SetTextAlignment(TextAlignment.Center);
            groupLockCol.AddTextBoxColumn("RELEASEHOLDDATE", 150).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("{0:yyyy-MM-dd HH:mm:ss}");

            var groupWIPCol = grdHold.View.AddGroupColumn("WIPLIST");
            groupWIPCol.AddTextBoxColumn("LEADTIME", 80).SetTextAlignment(TextAlignment.Right);
            groupWIPCol.AddTextBoxColumn("LOTINPUTDATE", 150).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("{0:yyyy-MM-dd HH:mm:ss}");
            groupWIPCol.AddTextBoxColumn("DELIVERYDATE", 150).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("{0:yyyy-MM-dd HH:mm:ss}");
            groupWIPCol.AddTextBoxColumn("LEFTDATE", 80).SetTextAlignment(TextAlignment.Center);

            grdHold.View.PopulateColumns();
            #endregion

            #region - Release Hold Grid |
            grdRelease.GridButtonItem = GridButtonItem.None;

            grdRelease.View.SetIsReadOnly();

            // CheckBox 설정
            this.grdRelease.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdRelease.View.AddTextBoxColumn("TXNHISTKEY", 100).SetTextAlignment(TextAlignment.Center).SetIsHidden();
            grdRelease.View.AddTextBoxColumn("LOTTYPE", 70).SetTextAlignment(TextAlignment.Center);
            grdRelease.View.AddTextBoxColumn("PRODUCTDEFID", 150).SetTextAlignment(TextAlignment.Center);
            grdRelease.View.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            grdRelease.View.AddTextBoxColumn("USERSEQUENCE", 70).SetTextAlignment(TextAlignment.Center);
            grdRelease.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
            grdRelease.View.AddTextBoxColumn("PLANTID", 70).SetTextAlignment(TextAlignment.Center);
            grdRelease.View.AddTextBoxColumn("AREANAME", 70).SetTextAlignment(TextAlignment.Center);
            grdRelease.View.AddTextBoxColumn("RTRSHT", 50).SetTextAlignment(TextAlignment.Center);
            grdRelease.View.AddTextBoxColumn("LOTID", 200).SetTextAlignment(TextAlignment.Center);
            grdRelease.View.AddTextBoxColumn("UNIT", 60).SetTextAlignment(TextAlignment.Center);

            grdRelease.View.AddTextBoxColumn("QTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}").SetIsHidden();
            grdRelease.View.AddTextBoxColumn("PANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}").SetIsHidden();

            grdRelease.View.AddTextBoxColumn("ISHOLD", 100).SetTextAlignment(TextAlignment.Center).SetIsHidden();
            grdRelease.View.AddTextBoxColumn("HOLDTOPCLASS", 100).SetTextAlignment(TextAlignment.Center).SetIsHidden();
            //grdRelease.View.AddTextBoxColumn("HOLDMIDDLECLASS", 100).SetTextAlignment(TextAlignment.Center).SetIsHidden();
            grdRelease.View.AddTextBoxColumn("HOLDREASON", 150).SetTextAlignment(TextAlignment.Center).SetIsHidden();
            grdRelease.View.AddTextBoxColumn("HOLDUSER", 100).SetTextAlignment(TextAlignment.Center).SetIsHidden();
            grdRelease.View.AddTextBoxColumn("HOLDDATE", 150).SetTextAlignment(TextAlignment.Center).SetIsHidden();
            grdRelease.View.AddTextBoxColumn("RELEASEHOLDUSER", 100).SetTextAlignment(TextAlignment.Center).SetIsHidden();
            grdRelease.View.AddTextBoxColumn("RELEASEHOLDDATE", 150).SetTextAlignment(TextAlignment.Center).SetIsHidden();

            grdRelease.View.AddTextBoxColumn("LEADTIME", 80).SetTextAlignment(TextAlignment.Right);
            grdRelease.View.AddTextBoxColumn("LOTINPUTDATE", 150).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("{0:yyyy-MM-dd HH:mm:ss}");
            grdRelease.View.AddTextBoxColumn("DELIVERYDATE", 150).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("{0:yyyy-MM-dd HH:mm:ss}");
            grdRelease.View.AddTextBoxColumn("LEFTDATE", 80).SetTextAlignment(TextAlignment.Center);

            grdRelease.View.PopulateColumns();
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
            grdHold.View.DoubleClick += View_DoubleClick;
            grdHold.View.RowStyle += WIP_RowStyle;

            grdRelease.View.RowStyle += Target_RowStyle;
        }

        #region ▶ ComboBox Event |

        #endregion

        #region ▶ Grid Event |

        #region - 재공 Grid 더블클릭 |
        /// <summary>
        /// 재공 Grid 더블클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_DoubleClick(object sender, EventArgs e)
        {
            CommonFunction.SetGridDoubleClickCheck(grdHold, sender);
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
            CommonFunction.SetGridRowStyle(grdHold, e);
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
            CommonFunction.SetGridRowStyle(grdRelease, e);
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
            DataTable lockList = grdRelease.DataSource as DataTable;

            if (lockList == null || lockList.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }

            MessageWorker worker = new MessageWorker("SaveLotHold");
            worker.SetBody(new MessageBody()
            {
                { "TransactionType", "SetReleaseLotHold" },
                { "EnterpriseID", UserInfo.Current.Enterprise },
                { "PlantID", UserInfo.Current.Plant },
                { "Comments", txtComment.Text },
                { "UserId", UserInfo.Current.Id },
                { "Lotlist", lockList }
            });

            worker.Execute();

            // Data 초기화
            SetClearControl();
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
            this.grdHold.DataSource = null;
            this.grdRelease.DataSource = null;

            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("USERID", UserInfo.Current.Id);

            DataTable dt = await SqlExecuter.QueryAsync("SelectLotHoldList", "10001", values);

            if (dt.Rows.Count < 1)
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdHold.DataSource = dt;
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
            this.txtComment.Text = string.Empty;
        }
        #endregion

        #endregion
    }
}
