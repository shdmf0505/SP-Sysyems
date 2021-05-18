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
using Micube.SmartMES.Commons.Controls;
#endregion

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 공정관리 > LOT관리 > Lot Release Locking
    /// 업  무  설  명  : Lot Locking 해제 설정
    /// 생    성    자  : 박정훈
    /// 생    성    일  : 2019-07-11
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class LotReleaseLocking : SmartConditionManualBaseForm
    {
        #region ◆ Local Variables |

        #endregion

        #region ◆ 생성자 |
        /// <summary>
        /// 생성자
        /// </summary>
        public LotReleaseLocking()
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

            this.ucDataUpDownBtnCtrl.SourceGrid = this.grdLock;
            this.ucDataUpDownBtnCtrl.TargetGrid = this.grdRelease;

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
        } 
        #endregion

        #region ▶ ComboBox 초기화 |
        /// <summary>
        /// 콤보박스를 초기화 하는 함수
        /// </summary>
        private void InitializeComboBox()
        {
            #region - 해제 사유코드 ComboBox |
            // 사유코드 ComboBox 설정 
            cboReason.Editor.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboReason.Editor.ValueMember = "REASONCODEID";
            cboReason.Editor.DisplayMember = "REASONCODENAME";

            cboReason.Editor.DataSource = SqlExecuter.Query("GetReasonCodeList", "10001"
                    , new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "P_REASONCODECLASSID", "LotLockingRelease" } });

            cboReason.Editor.ShowHeader = false;
            #endregion
        }
        #endregion

        #region ▶ Grid 설정 |
        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            #region - Locking List Grid 설정 |
            grdLock.GridButtonItem = GridButtonItem.Export;

            grdLock.View.SetIsReadOnly();

            // CheckBox 설정
            this.grdLock.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            var groupDefaultCol = grdLock.View.AddGroupColumn("LOCKINGLIST");
            groupDefaultCol.AddTextBoxColumn("TXNHISTKEY", 100).SetTextAlignment(TextAlignment.Center).SetIsHidden(); 
            groupDefaultCol.AddTextBoxColumn("LOTTYPE", 70).SetTextAlignment(TextAlignment.Center); 
            groupDefaultCol.AddTextBoxColumn("PRODUCTDEFID", 150).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            groupDefaultCol.AddTextBoxColumn("USERSEQUENCE", 70).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
            groupDefaultCol.AddTextBoxColumn("PLANTID", 70).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("AREANAME", 70).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("RTRSHT", 70).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("LOTID", 200).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("UNIT", 60).SetTextAlignment(TextAlignment.Center);

            var groupWipCol = grdLock.View.AddGroupColumn("WIPQTY");
            groupWipCol.AddTextBoxColumn("QTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            groupWipCol.AddTextBoxColumn("PANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");

            var groupLockCol = grdLock.View.AddGroupColumn("LOCKINFO");
            groupLockCol.AddTextBoxColumn("ISLOCKING", 100).SetTextAlignment(TextAlignment.Center);
            groupLockCol.AddTextBoxColumn("LOCKINGTYPE", 100).SetTextAlignment(TextAlignment.Center);
            groupLockCol.AddTextBoxColumn("LOCKINGREASON", 150).SetTextAlignment(TextAlignment.Center);
            groupLockCol.AddTextBoxColumn("LOCKINGUSER", 100).SetTextAlignment(TextAlignment.Center);
            groupLockCol.AddTextBoxColumn("LOCKINGDATE", 150).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("{0:yyyy-MM-dd HH:mm:ss}");

            var groupWIPCol = grdLock.View.AddGroupColumn("WIPINFO");
            groupWIPCol.AddTextBoxColumn("LEADTIME", 80).SetTextAlignment(TextAlignment.Right);
            groupWIPCol.AddTextBoxColumn("LOTINPUTDATE", 150).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("{0:yyyy-MM-dd HH:mm:ss}");
            groupWIPCol.AddTextBoxColumn("DELIVERYDATE", 150).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("{0:yyyy-MM-dd HH:mm:ss}");
            groupWIPCol.AddTextBoxColumn("LEFTDATE", 80).SetTextAlignment(TextAlignment.Center);

            grdLock.View.PopulateColumns();
            #endregion

            #region - Release Locking Grid |
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

            grdRelease.View.AddTextBoxColumn("ISLOCKING", 100).SetTextAlignment(TextAlignment.Center).SetIsHidden();
            grdRelease.View.AddTextBoxColumn("LOCKINGTYPE", 100).SetTextAlignment(TextAlignment.Center).SetIsHidden();
            grdRelease.View.AddTextBoxColumn("LOCKINGREASON", 150).SetTextAlignment(TextAlignment.Center).SetIsHidden();
            grdRelease.View.AddTextBoxColumn("LOCKINGUSER", 100).SetTextAlignment(TextAlignment.Center).SetIsHidden();
            grdRelease.View.AddTextBoxColumn("LOCKINGDATE", 150).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("{0:yyyy-MM-dd HH:mm:ss}").SetIsHidden();

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

            grdLock.View.DoubleClick += View_DoubleClick;
            grdLock.View.RowStyle += WIP_RowStyle;

            grdRelease.View.RowStyle += Target_RowStyle;
        }

        #region ▶ ComboBox Event |

        #endregion

        #region ▶ Grid Event |

        #region - Locking Grid 더블클릭 |
        /// <summary>
        /// 재공 Grid 더블클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_DoubleClick(object sender, EventArgs e)
        {
            CommonFunction.SetGridDoubleClickCheck(grdLock, sender);
        }
        #endregion

        #region - Locking Row Stype Event |
        /// <summary>
        /// 재공 Row Stype Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WIP_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            CommonFunction.SetGridRowStyle(grdLock, e);
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

            // TODO : 저장 Rule 변경
            DataTable lockList = grdRelease.DataSource as DataTable;

            if (lockList == null || lockList.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }

            /*
            // 사유코드선택 체크
            if (this.cboReason.EditValue == null || string.IsNullOrWhiteSpace(this.cboReason.EditValue.ToString()))
            {
                // 사유코드 선택은 필수 선택입니다.
                throw MessageException.Create("NoExistReasonCode");
            }
            */

            string lockReason = (string)cboReason.GetValue();

            MessageWorker worker = new MessageWorker("SaveLotLocking");
            worker.SetBody(new MessageBody()
            {
                { "TransactionType", "SetReleaseLocking" },
                { "EnterpriseID", UserInfo.Current.Enterprise },
                { "PlantID", UserInfo.Current.Plant },
                { "ReasonCode", lockReason },
                { "Comments", txtComment.Text },
                { "UserId", UserInfo.Current.Id },
                { "Lotlist", lockList }
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
            this.grdLock.DataSource = null;
            this.grdRelease.DataSource = null;

            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("USERID", UserInfo.Current.Id);

            DataTable dt = await SqlExecuter.QueryAsync("GetLotLockingList", "10001", values);

            if (dt.Rows.Count < 1)
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdLock.DataSource = dt;
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
            this.cboReason.EditValue = null;
            this.txtComment.Text = string.Empty;
        }
        #endregion

        #endregion
    }
}
