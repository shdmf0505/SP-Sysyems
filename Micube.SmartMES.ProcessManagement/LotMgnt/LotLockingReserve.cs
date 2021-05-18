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
    /// 프 로 그 램 명  : 공정관리 > LOT관리 > Lot 예약 Locking
    /// 업  무  설  명  : Lot 예약 Locking 설정
    /// 생    성    자  : 박정훈
    /// 생    성    일  : 2019-07-29
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class LotLockingReserve : SmartConditionManualBaseForm
    {
        #region ◆ Local Variables |

        #endregion

        #region ◆ 생성자 |
        /// <summary>
        /// 생성자
        /// </summary>
        public LotLockingReserve()
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
            this.ucDataUpDownBtnCtrl.TargetGrid = this.grdLocking;

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
            #region - 분류 ComboBox |
            // 분류 ComboBox 설정
            cboClass.Editor.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboClass.Editor.ValueMember = "REASONCODECLASSID";
            cboClass.Editor.DisplayMember = "REASONCODECLASSNAME";

            cboClass.Editor.DataSource = SqlExecuter.Query("GetReasonCodeClassList", "10002"
                    , new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "P_PARENTREASONCODECLASSID", "LotLockingCode" } });

            cboClass.Editor.ShowHeader = false;
            #endregion
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
            groupDefaultCol.AddTextBoxColumn("PRODUCTDEFNAME", 250);
            groupDefaultCol.AddTextBoxColumn("USERSEQUENCE", 70).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
            groupDefaultCol.AddTextBoxColumn("PLANTID", 70).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("AREANAME", 100).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("RTRSHT", 70).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("LOTID", 200).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("UNIT", 60).SetTextAlignment(TextAlignment.Center);

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

            #region - Locking Grid |
            grdLocking.GridButtonItem = GridButtonItem.None;

            grdLocking.View.SetIsReadOnly();

            // CheckBox 설정
            this.grdLocking.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdLocking.View.AddTextBoxColumn("LOTTYPE", 100).SetTextAlignment(TextAlignment.Center);
            grdLocking.View.AddTextBoxColumn("PRODUCTDEFID", 150).SetTextAlignment(TextAlignment.Center);
            grdLocking.View.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            grdLocking.View.AddTextBoxColumn("USERSEQUENCE", 150).SetTextAlignment(TextAlignment.Center);
            grdLocking.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150);
            grdLocking.View.AddTextBoxColumn("PLANTID", 150).SetTextAlignment(TextAlignment.Center);
            grdLocking.View.AddTextBoxColumn("AREANAME", 150).SetTextAlignment(TextAlignment.Center);
            grdLocking.View.AddTextBoxColumn("RTRSHT", 150).SetTextAlignment(TextAlignment.Center);
            grdLocking.View.AddTextBoxColumn("LOTID", 150).SetTextAlignment(TextAlignment.Center);
            grdLocking.View.AddTextBoxColumn("UNIT", 150).SetTextAlignment(TextAlignment.Center);

            grdLocking.View.AddTextBoxColumn("QTY", 150).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}").SetIsHidden();
            grdLocking.View.AddTextBoxColumn("PANELQTY", 150).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}").SetIsHidden();

            grdLocking.View.AddTextBoxColumn("RECEIVEPCSQTY", 150).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}").SetIsHidden();
            grdLocking.View.AddTextBoxColumn("RECEIVEPANELQTY", 150).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}").SetIsHidden();

            grdLocking.View.AddTextBoxColumn("WORKSTARTPCSQTY", 150).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}").SetIsHidden();
            grdLocking.View.AddTextBoxColumn("WORKSTARTPANELQTY", 150).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}").SetIsHidden();

            grdLocking.View.AddTextBoxColumn("WORKENDPCSQTY", 150).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}").SetIsHidden();
            grdLocking.View.AddTextBoxColumn("WORKENDPANELQTY", 150).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}").SetIsHidden();

            grdLocking.View.AddTextBoxColumn("SENDPCSQTY", 150).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}").SetIsHidden();
            grdLocking.View.AddTextBoxColumn("SENDPANELQTY", 150).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}").SetIsHidden();

            grdLocking.View.AddTextBoxColumn("LEADTIME", 150).SetTextAlignment(TextAlignment.Right);
            grdLocking.View.AddTextBoxColumn("LOTINPUTDATE", 150).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("{0:yyyy-MM-dd HH:mm:ss}");
            grdLocking.View.AddTextBoxColumn("DELIVERYDATE", 150).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("{0:yyyy-MM-dd HH:mm:ss}");
            grdLocking.View.AddTextBoxColumn("LEFTDATE", 150).SetTextAlignment(TextAlignment.Center);

            grdLocking.View.PopulateColumns();
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

            // ComboBox Event
            cboClass.Editor.EditValueChanged += cboClass_EditValueChanged;

            grdWIP.View.CheckStateChanged += View_CheckStateChanged;
            grdWIP.View.DoubleClick += View_DoubleClick;
            grdWIP.View.RowStyle += WIP_RowStyle;

            grdLocking.View.RowStyle += Target_RowStyle;
            grdLocking.View.FocusedRowChanged += View_FocusedRowChanged;

            this.ucDataUpDownBtnCtrl.buttonClick += UcDataUpDownBtnCtrl_buttonClick;
        }

        #region ▶ Up / Down Button Event |
        /// <summary>
        /// 품목, 품목버전 체크
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UcDataUpDownBtnCtrl_buttonClick(object sender, EventArgs e)
        {
            if (!this.ucDataUpDownBtnCtrl.ButtonState.Equals("Down")) return;

            DataTable dt = grdWIP.View.GetCheckedRows();
            DataTable tgdt = grdLocking.DataSource as DataTable;

            if (tgdt == null || tgdt.Rows.Count <= 0) return;

            //품목
            string productDefId = dt.AsEnumerable().Select(r => Format.GetString(r["PRODUCTDEFID"])).Distinct().FirstOrDefault();
            string tgProductDefId = tgdt.AsEnumerable().Select(r => Format.GetString(r["PRODUCTDEFID"])).Distinct().FirstOrDefault();

            if (!productDefId.Equals(tgProductDefId))
            {
                grdWIP.View.CheckedAll(false);

                // 다른 품목 ID는 선택할 수 없습니다.
                throw MessageException.Create("MixSelectProductDefID");
            }

            //품목버전
            string productDefVer = dt.AsEnumerable().Select(r => Format.GetString(r["PRODUCTDEFVERSION"])).Distinct().FirstOrDefault();
            string tgProductDefVer = tgdt.AsEnumerable().Select(r => Format.GetString(r["PRODUCTDEFVERSION"])).Distinct().FirstOrDefault();

            if (!productDefVer.Equals(tgProductDefVer))
            {
                grdWIP.View.CheckedAll(false);

                //다른 품목 버전은 선택할 수 없습니다.
                throw MessageException.Create("MixSelectProductDefVersion");
            }
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

            int productCount = dt.AsEnumerable().Select(r => r.Field<string>("PRODUCTDEFID")).Distinct().Count();

            if(productCount > 1)
            {
				//grdWIP.View.CheckedAll(false);
				grdWIP.View.CheckMarkSelection.SelectRow(grdWIP.View.FocusedRowHandle, false);

				// 다른 품목 ID는 선택할 수 없습니다.
				throw MessageException.Create("MixSelectProductDefID");
            }

			int productVerCount = dt.AsEnumerable().Select(r => r.Field<string>("PRODUCTDEFVERSION")).Distinct().Count();

			if(productVerCount > 1)
			{
				grdWIP.View.CheckMarkSelection.SelectRow(grdWIP.View.FocusedRowHandle, false);

				//다른 품목 버전은 선택할 수 없습니다.
				throw MessageException.Create("MixSelectProductDefVersion");
			}
        }
        #endregion

        #region - FocusedRowChange |
        /// <summary>
        /// FocusedRowChange
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle < 0)
                return;

            if (e.FocusedRowHandle >= 0)
            {
                DataTable dt = grdLocking.DataSource as DataTable;

                if (dt == null || dt.Rows.Count <= 0) return;

                try
                {
                    string strSegmentSeq = dt.AsEnumerable().Max(r => r["USERSEQUENCE"]).ToString();
                    string strProductID = dt.AsEnumerable().Select(r => r.Field<string>("PRODUCTDEFID")).Distinct().ToList()[0].ToString();
                    string strProductVer = dt.AsEnumerable().Select(r => r.Field<string>("PRODUCTDEFVERSION")).Distinct().ToList()[0].ToString();

					// 분류 ComboBox 설정
					cboSegment.Editor.ComboBoxColumnShowType = ComboBoxColumnShowType.Custom;
					cboSegment.Editor.SetVisibleColumns("USERSEQUENCE", "PROCESSSEGMENTNAME", "PLANTID");
					cboSegment.Editor.ValueMember = "PROCESSSEGMENTID";
                    cboSegment.Editor.DisplayMember = "PROCESSSEGMENTNAME";

                    DataTable dtResult = SqlExecuter.Query("GetProcessPathByProductDefAndSequence", "10001"
                            , new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "P_PRODUCTDEFID", strProductID }, { "P_PRODUCTDEFVERSION", strProductVer }, { "P_USERSEQUENCE", strSegmentSeq } });
					
					dtResult.Columns["USERSEQUENCE"].SetOrdinal(0);
					dtResult.Columns["PROCESSSEGMENTNAME"].SetOrdinal(1);
					dtResult.Columns["PLANTID"].SetOrdinal(2);

					cboSegment.Editor.DataSource = dtResult;
					//cboSegment.Editor.ShowHeader = false;
				}
                catch
                {

                }
            }
        }
        #endregion

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

        #region - Target Grid Row Stype Event |
        /// <summary>
        /// Target Grid Row Stype Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Target_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            CommonFunction.SetGridRowStyle(grdLocking, e);
        }
        #endregion

        #endregion

        #region ▶ ComboBox Event |
        /// <summary>
        /// 분류 ComboBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboClass_EditValueChanged(object sender, EventArgs e)
        {
            if (cboClass.GetValue() == null || string.IsNullOrWhiteSpace(cboClass.GetValue().ToString()))
                return;

            #region - 사유코드 ComboBox |
            // 사유코드 ComboBox 설정 
            cboReason.Editor.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboReason.Editor.ValueMember = "REASONCODEID";
            cboReason.Editor.DisplayMember = "REASONCODENAME";

            cboReason.Editor.DataSource = SqlExecuter.Query("GetReasonCodeList", "10001"
                    , new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "P_REASONCODECLASSID", cboClass.GetValue().ToString() } });

            cboReason.Editor.ShowHeader = false;
            #endregion
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

            // TODO : 저장 Rule 변경
            DataTable lockList = grdLocking.DataSource as DataTable;

            if (lockList == null || lockList.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }

            // 공정선택 체크
            if (this.cboSegment.EditValue == null || string.IsNullOrWhiteSpace(this.cboSegment.EditValue.ToString()))
            {
                // 공정선택은 필수 선택입니다.
                throw MessageException.Create("RequiredProcessSegId");
            }

            // 분류선택 체크
            if (this.cboClass.EditValue == null || string.IsNullOrWhiteSpace(this.cboClass.EditValue.ToString()))
            {
                // 분류선택은 필수 선택입니다.
                throw MessageException.Create("NoExistLockingClass");
            }

            // 사유코드선택 체크
            if (this.cboReason.EditValue == null || string.IsNullOrWhiteSpace(this.cboReason.EditValue.ToString()))
            {
                // 사유코드 선택은 필수 선택입니다.
                throw MessageException.Create("NoExistReasonCode");
            }

            // Locking 분류
            string strSegmentID = (string)cboSegment.GetValue();
            string lockClass = (string)cboClass.GetValue();
            string lockReason = (string)cboReason.GetValue();

            MessageWorker worker = new MessageWorker("SaveLotLocking");
            worker.SetBody(new MessageBody()
            {
                { "TransactionType", "SetLotLockingReserve" },
                { "EnterpriseID", UserInfo.Current.Enterprise },
                { "PlantID", UserInfo.Current.Plant },
                { "SegmentID", strSegmentID },
                { "LockClass", lockClass },
                { "ReasonCode", lockReason },
                { "Comments", txtComment.Text },
                { "UserId", UserInfo.Current.Id },
                { "Lotlist", lockList }
            });

            worker.Execute();

            // Data 초기화
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
            this.grdLocking.DataSource = null;

            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            //DataTable dt = await SqlExecuter.QueryAsync("SelectWIPLockList", "10001", values);
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
            this.cboClass.EditValue = null;
            this.cboReason.EditValue = null;
            this.txtComment.Text = string.Empty;
        }
        #endregion
        #endregion
    }
}
