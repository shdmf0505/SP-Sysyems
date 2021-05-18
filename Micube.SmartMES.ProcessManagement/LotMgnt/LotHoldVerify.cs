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
using Micube.SmartMES.Commons.Controls;
#endregion

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 공정관리 > LOT관리 > Lot 보류
    /// 업  무  설  명  : Lot 보류 설정
    /// 생    성    자  : 박정훈
    /// 생    성    일  : 2019-07-25
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class LotHoldVerify : SmartConditionManualBaseForm
    {
        #region ◆ Local Variables |

        private DataTable _dtState = null;

        private Point pLable;
        private Point pText;


        #endregion

        #region ◆ 생성자 |
        /// <summary>
        /// 생성자
        /// </summary>
        public LotHoldVerify()
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

            // Up / Down Control 설정
            this.btnUpDown.SourceGrid = this.grdWIP;
            this.btnUpDown.TargetGrid = this.grdHold;

            InitializeEvent();

            pLable = new Point(lblComment.Location.X, lblComment.Location.Y);
            pText = new Point(txtComment.Location.X, txtComment.Location.Y);
    }

        #region ▶ 조회조건 설정 |
        /// <summary>
        /// 검색조건 설정
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // 품목
            CommonFunction.AddConditionProductPopup("P_PRODUCTDEFID", 1.5, false, Conditions);

            // 작업장
            CommonFunction.AddConditionAreaByAuthorityPopup("P_AREAID", 3.5, true, Conditions, false, true);

            Dictionary<string, object> param = new Dictionary<string, object>();

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
                param = new Dictionary<string, object>();
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

            #region - 불량코드 CodeHelp |
            // 불량코드
            ConditionItemSelectPopup workerCondition = new ConditionItemSelectPopup();
            workerCondition.Id = "DEFECTCODE";

            param = new Dictionary<string, object>();
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

            #endregion
        }

        /// <summary>
        /// 조회조건 설정
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            txtDefectCode.Editor.ReadOnly = true;
            txtDefectCode.Editor.SearchButtonReadOnly = true;
            txtDefectCode.Editor.ClearButtonVisible = false;

            SetConditionVisiblility("P_PERIOD", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
            SetConditionVisiblility("P_HOLDSTATE", DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
            Conditions.GetControl<SmartComboBox>("P_PRODUCTDEFTYPE").EditValue = "Product";
        } 
        #endregion

        #region ▶ ComboBox 초기화 |
        /// <summary>
        /// 콤보박스를 초기화 하는 함수
        /// </summary>
        private void InitializeComboBox()
        {
            #region - 대분류 ComboBox |
            // 분류 ComboBox 설정
            cboTopClass.Editor.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboTopClass.Editor.ValueMember = "REASONCODECLASSID";
            cboTopClass.Editor.DisplayMember = "REASONCODECLASSNAME";

            cboTopClass.Editor.DataSource = SqlExecuter.Query("GetReasonCodeClassList", "10002"
                    , new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "P_PARENTREASONCODECLASSID", "HoldCode" }, { "P_TYPE", "HoldVerify" } });

            cboTopClass.Editor.ShowHeader = false;
            #endregion

            #region - 대분류 ComboBox |
            // 분류 ComboBox 설정
            cboTopClass.Editor.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboTopClass.Editor.ValueMember = "REASONCODECLASSID";
            cboTopClass.Editor.DisplayMember = "REASONCODECLASSNAME";

            cboTopClass.Editor.DataSource = SqlExecuter.Query("GetReasonCodeClassList", "10002"
                    , new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "P_PARENTREASONCODECLASSID", "HoldCode" }, { "P_TYPE", "HoldVerify" } });

            cboTopClass.Editor.ShowHeader = false;
            #endregion

            #region 홀드상태 콤보
            cboState.Editor.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboState.Editor.ValueMember = "CODEID";
            cboState.Editor.DisplayMember = "CODENAME";

            cboState.Editor.DataSource = SqlExecuter.Query("GetCodeList", "00001"
                    , new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "CODECLASSID", "HoldVerifyStatus" } });

            cboState.Editor.ShowHeader = false;

            _dtState = cboState.Editor.DataSource as DataTable;
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

            // CheckBox 설정
            grdWIP.GridButtonItem = GridButtonItem.Export;
            this.grdWIP.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdWIP.View.SetIsReadOnly();
            //보류상태코드
            grdWIP.View.AddTextBoxColumn("HOLDSTATECODE", 80).SetTextAlignment(TextAlignment.Left).SetLabel("HOLDSTATECODE").SetIsHidden();
            //보류 상태
            grdWIP.View.AddTextBoxColumn("HOLDSTAE", 80).SetTextAlignment(TextAlignment.Left).SetLabel("HOLDSTAE");
            grdWIP.View.AddTextBoxColumn("PRODUCTDEFID", 100).SetLabel("PRODUCTDEFID");
            //rev.
            grdWIP.View.AddTextBoxColumn("PRODUCTDEFVERSION", 50).SetLabel("PRODUCTREVISION");
            //품목명
            grdWIP.View.AddTextBoxColumn("PRODUCTDEFNAME", 150);
            //lot id
            grdWIP.View.AddTextBoxColumn("LOTID", 180).SetTextAlignment(TextAlignment.Center);
            //공정수순
            grdWIP.View.AddTextBoxColumn("USERSEQUENCE", 60).SetTextAlignment(TextAlignment.Center).SetLabel("USERSEQUENCE");
            //공정명
            grdWIP.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 120).SetTextAlignment(TextAlignment.Left).SetLabel("PROCESSSEGMENTNAME");
            //작업장
            grdWIP.View.AddTextBoxColumn("AREANAME", 80).SetTextAlignment(TextAlignment.Left).SetLabel("AREANAME");
            //상태
            grdWIP.View.AddTextBoxColumn("WIPPROCESSSTATE", 60).SetTextAlignment(TextAlignment.Left).SetLabel("STATE");
            //uom
            grdWIP.View.AddTextBoxColumn("UOM", 50).SetTextAlignment(TextAlignment.Center);
            //pcs
            grdWIP.View.AddSpinEditColumn("PCSQTY", 80).SetDisplayFormat("#,##0", MaskTypes.Numeric, false);
            //pnl
            grdWIP.View.AddSpinEditColumn("PNLQTY", 80).SetDisplayFormat("#,##0", MaskTypes.Numeric, false);
            //mm
            grdWIP.View.AddSpinEditColumn("MM", 80).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true);
            //보류설정자   
            grdWIP.View.AddTextBoxColumn("HOLDUSER", 100);
            //보류설정일   
            grdWIP.View.AddTextBoxColumn("HOLDTIME", 100).SetLabel("HOLDDATE");
            //보류검증지정자   
            grdWIP.View.AddTextBoxColumn("HOLDVERIFYUSER", 100).SetLabel("HOLDVERIFYUSER");
            //보류검증지정일   
            grdWIP.View.AddTextBoxColumn("HOLDVERIFYTIME", 100).SetLabel("HOLDVERIFYTIME");
            grdWIP.View.AddTextBoxColumn("TXNKEY", 100).SetLabel("TXNKEY").SetIsHidden();
            grdWIP.View.PopulateColumns();

            #endregion

            #region - Locking Grid |
            grdHold.GridButtonItem = GridButtonItem.None;

            grdHold.View.SetIsReadOnly();

            // CheckBox 설정
            this.grdHold.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdHold.View.SetIsReadOnly();
            //보류상태코드
            grdHold.View.AddTextBoxColumn("HOLDSTATECODE", 80).SetTextAlignment(TextAlignment.Left).SetLabel("HOLDSTATECODE").SetIsHidden();
            //보류 상태
            grdHold.View.AddTextBoxColumn("HOLDSTAE", 80).SetTextAlignment(TextAlignment.Left).SetLabel("HOLDSTAE");
            grdHold.View.AddTextBoxColumn("PRODUCTDEFID", 100).SetLabel("PRODUCTDEFID");
            //rev.
            grdHold.View.AddTextBoxColumn("PRODUCTDEFVERSION", 50).SetLabel("PRODUCTREVISION");
            //품목명
            grdHold.View.AddTextBoxColumn("PRODUCTDEFNAME", 150);
            //lot id
            grdHold.View.AddTextBoxColumn("LOTID", 180).SetTextAlignment(TextAlignment.Center);
            //공정수순
            grdHold.View.AddTextBoxColumn("USERSEQUENCE", 60).SetTextAlignment(TextAlignment.Center).SetLabel("USERSEQUENCE");
            //공정명
            grdHold.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 120).SetTextAlignment(TextAlignment.Left).SetLabel("PROCESSSEGMENTNAME");
            //작업장
            grdHold.View.AddTextBoxColumn("AREANAME", 80).SetTextAlignment(TextAlignment.Left).SetLabel("AREANAME");
            //상태
            grdHold.View.AddTextBoxColumn("WIPPROCESSSTATE", 60).SetTextAlignment(TextAlignment.Left).SetLabel("STATE");
            //uom
            grdHold.View.AddTextBoxColumn("UOM", 50).SetTextAlignment(TextAlignment.Center);
            //pcs
            grdHold.View.AddSpinEditColumn("PCSQTY", 80).SetDisplayFormat("#,##0", MaskTypes.Numeric, false);
            //pnl
            grdHold.View.AddSpinEditColumn("PNLQTY", 80).SetDisplayFormat("#,##0", MaskTypes.Numeric, false);
            //mm
            grdHold.View.AddSpinEditColumn("MM", 80).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true);
            //보류설정자   
            grdHold.View.AddTextBoxColumn("HOLDUSER", 100);
            //보류설정일   
            grdHold.View.AddTextBoxColumn("HOLDTIME", 100).SetLabel("HOLDDATE");
            //보류검증지정자   
            grdHold.View.AddTextBoxColumn("HOLDVERIFYUSER", 100).SetLabel("HOLDVERIFYUSER");
            //보류검증지정일   
            grdHold.View.AddTextBoxColumn("HOLDVERIFYTIME", 100).SetLabel("HOLDVERIFYTIME");
            grdHold.View.AddTextBoxColumn("TXNKEY", 100).SetLabel("TXNKEY").SetIsHidden();
            grdHold.View.PopulateColumns();
            #endregion

            #region 폐기 그리드
            grdScrap.GridButtonItem = GridButtonItem.Export;

            grdScrap.View.SetIsReadOnly();

            // CheckBox 설정
            this.grdScrap.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            //보류상태코드
            grdScrap.View.AddTextBoxColumn("HOLDSTATECODE", 80).SetTextAlignment(TextAlignment.Left).SetLabel("HOLDSTATECODE").SetIsHidden();
            //보류 상태
            grdScrap.View.AddTextBoxColumn("HOLDSTAE", 80).SetTextAlignment(TextAlignment.Left).SetLabel("HOLDSTAE");
            grdScrap.View.AddTextBoxColumn("PRODUCTDEFID", 100).SetLabel("PRODUCTDEFID");
            //rev.
            grdScrap.View.AddTextBoxColumn("PRODUCTDEFVERSION", 50).SetLabel("PRODUCTREVISION");
            //품목명
            grdScrap.View.AddTextBoxColumn("PRODUCTDEFNAME", 150);
            //lot id
            grdScrap.View.AddTextBoxColumn("LOTID", 180).SetTextAlignment(TextAlignment.Center);
            //공정수순
            grdScrap.View.AddTextBoxColumn("USERSEQUENCE", 60).SetTextAlignment(TextAlignment.Center).SetLabel("USERSEQUENCE");
            //공정명
            grdScrap.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 120).SetTextAlignment(TextAlignment.Left).SetLabel("PROCESSSEGMENTNAME");
            //작업장
            grdScrap.View.AddTextBoxColumn("AREANAME", 80).SetTextAlignment(TextAlignment.Left).SetLabel("AREANAME");
            //상태
            grdScrap.View.AddTextBoxColumn("WIPPROCESSSTATE", 60).SetTextAlignment(TextAlignment.Left).SetLabel("STATE");
            //uom
            grdScrap.View.AddTextBoxColumn("UOM", 50).SetTextAlignment(TextAlignment.Center);
            //pcs
            grdScrap.View.AddSpinEditColumn("PCSQTY", 80).SetDisplayFormat("#,##0", MaskTypes.Numeric, false);
            //pnl
            grdScrap.View.AddSpinEditColumn("PNLQTY", 80).SetDisplayFormat("#,##0", MaskTypes.Numeric, false);
            //mm
            grdScrap.View.AddSpinEditColumn("MM", 80).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true);
            //보류설정자   
            grdScrap.View.AddTextBoxColumn("HOLDUSER", 100);
            //보류설정일   
            grdScrap.View.AddTextBoxColumn("HOLDTIME", 100).SetLabel("HOLDDATE");
            //보류검증지정자   
            grdScrap.View.AddTextBoxColumn("HOLDVERIFYUSER", 100).SetLabel("HOLDVERIFYUSER");
            //보류검증지정일   
            grdScrap.View.AddTextBoxColumn("HOLDVERIFYTIME", 100).SetLabel("HOLDVERIFYTIME");
            grdScrap.View.AddTextBoxColumn("TXNKEY", 100).SetLabel("TXNKEY").SetIsHidden();

            grdScrap.View.PopulateColumns();

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
            cboTopClass.Editor.EditValueChanged += cboTopClass_EditValueChanged;
            grdWIP.View.CheckStateChanged += View_CheckStateChanged;

            grdWIP.View.DoubleClick += View_DoubleClick;
            grdWIP.View.RowStyle += WIP_RowStyle;

            grdHold.View.RowStyle += Target_RowStyle;
            btnUpDown.buttonClick += BtnUpDown_buttonClick;

            cboState.Editor.EditValueChanged += Editor_EditValueChanged;

            tabHold.SelectedPageChanged += TabHold_SelectedPageChanged;

            txtDefectCode.Editor.ButtonClick += Editor_ButtonClick;
        }

        private void Editor_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            
        }

        private void TabHold_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if(e.Page == tpHoldVerify)
            {
                SetConditionVisiblility("P_PERIOD", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
                SetConditionVisiblility("P_HOLDSTATE", DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
                cboState.Editor.ReadOnly = false;
                cboTopClass.Editor.ReadOnly = false;
                cboReason.Editor.ReadOnly = false;

                this.btnUpDown.SourceGrid = this.grdWIP;

                grdHold.View.ClearDatas();

                txtDefectCode.Editor.ClearValue();
                txtDefectCode.Editor.SearchButtonReadOnly = true;
                txtDefectCode.Editor.ClearButtonVisible = false;

            }
            else
            {
                SetConditionVisiblility("P_PERIOD", DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
                SetConditionVisiblility("P_HOLDSTATE", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);

                cboState.EditValue = null;
                cboState.Editor.ReadOnly = true;
                cboTopClass.EditValue = null;
                cboReason.EditValue = null;
                cboTopClass.Editor.ReadOnly = true;
                cboReason.Editor.ReadOnly = true;

                this.btnUpDown.SourceGrid = this.grdScrap;
                grdWIP.View.ClearDatas();

                txtDefectCode.Editor.ClearValue();
                txtDefectCode.Editor.SearchButtonReadOnly = false;
                txtDefectCode.Editor.ClearButtonVisible = true;
            }
        }

        private void Editor_EditValueChanged(object sender, EventArgs e)
        {
            if (grdHold.DataSource == null) return;

            string CurrentState = (grdHold.DataSource as DataTable).AsEnumerable().Select(c => c.Field<string>("HOLDSTATECODE")).FirstOrDefault();
            string ChangeState = Format.GetTrimString(cboState.Editor.GetDataValue());

            cboTopClass.Editor.Text = string.Empty;
            cboTopClass.EditValue = null;

            cboReason.Editor.Text = string.Empty;
            cboReason.EditValue = null;

            txtComment.Text = string.Empty;

            if(CurrentState.Equals("Normal"))
            {
                cboTopClass.Editor.ReadOnly = false;
                cboReason.Editor.ReadOnly = false;
                txtComment.ReadOnly = false;
            }
            else
            {
                if(CurrentState.Equals("Verify") &&  ChangeState.Equals("Hold"))
                {
                    cboTopClass.Editor.ReadOnly = true;
                    cboReason.Editor.ReadOnly = true;
                    txtComment.ReadOnly = true;
                }
                else
                {
                    cboTopClass.Editor.ReadOnly = true;
                    cboReason.Editor.ReadOnly = true;
                    txtComment.ReadOnly = false;
                }
                /*
                cboTopClass.Visible = false;
                cboReason.Visible = false;

                Point pp = new Point(cboTopClass.Location.X, cboTopClass.Location.Y);
                lblComment.Location = pp;

                pp = new Point(cboReason.Location.X, cboReason.Location.Y);
                txtComment.Location = pp;
                */
            }

        }

        private void BtnUpDown_buttonClick(object sender, EventArgs e)
        {
            string btnState = btnUpDown.ButtonState;

            if (btnState.Equals("Down"))
            {
                DataTable dt = grdWIP.View.GetCheckedRows();

                if (dt == null || dt.Rows.Count == 0) return;

                string state = dt.AsEnumerable().Select(c => c.Field<string>("HOLDSTATECODE")).FirstOrDefault().ToString();

                DataTable dtList = _dtState.AsEnumerable().Where(c => !c.Field < string > ("CODEID").Equals(state)).CopyToDataTable();

                cboState.Editor.DataSource = dtList;

            }

        }
        private void View_CheckStateChanged(object sender, EventArgs e)
        {
            DataTable dt = grdWIP.View.GetCheckedRows() as DataTable;

            int rowhandle = grdWIP.View.FocusedRowHandle;

            if (!grdWIP.View.IsRowChecked(rowhandle)) return;

            string HoldStatus = Format.GetTrimString(grdWIP.View.GetRowCellValue(rowhandle, "HOLDSTATECODE"));

            int cnt = dt.AsEnumerable().Where(c => !c.Field<string>("HOLDSTATECODE").Equals(HoldStatus)).Count();

            if(cnt > 1)
            {
                grdWIP.View.CheckRow(rowhandle, false);
                //동일한 상태값만 선택 가능 합니다.
                throw MessageException.Create("CheckSameState");
            }

            dt = grdHold.DataSource as DataTable;

            if (dt == null || dt.Rows.Count == 0) return;

            cnt = dt.AsEnumerable().Where(c => !c.Field<string>("HOLDSTATECODE").Equals(HoldStatus)).Count();

            if (cnt > 1)
            {
                //동일한 상태값만 선택 가능 합니다.
                grdWIP.View.CheckRow(rowhandle, false);
                throw MessageException.Create("CheckSameState");
            }
        }

        #region ▶ ComboBox Event |
        /// <summary>
        /// 대분류 ComboBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboTopClass_EditValueChanged(object sender, EventArgs e)
        {
            if (cboTopClass.GetValue() == null || string.IsNullOrWhiteSpace(cboTopClass.GetValue().ToString()))
                return;

            #region - 사유코드 ComboBox |
            // 사유코드 ComboBox 설정 
            cboReason.Editor.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboReason.Editor.ValueMember = "REASONCODEID";
            cboReason.Editor.DisplayMember = "REASONCODENAME";

            cboReason.Editor.DataSource = SqlExecuter.Query("GetReasonCodeList", "10001"
                    , new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "P_REASONCODECLASSID", cboTopClass.GetValue().ToString() } });

            cboReason.Editor.ShowHeader = false;
            #endregion

        }

        /// <summary>
        /// 중분류 ComboBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboMiddleClass_EditValueChanged(object sender, EventArgs e)
        {
            /*
            //if (cboMiddleClass.GetValue() == null || string.IsNullOrWhiteSpace(cboMiddleClass.GetValue().ToString()))
                return;

            #region - 사유코드 ComboBox |
            // 사유코드 ComboBox 설정 
            cboReason.Editor.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboReason.Editor.ValueMember = "REASONCODEID";
            cboReason.Editor.DisplayMember = "REASONCODENAME";

            cboReason.Editor.DataSource = SqlExecuter.Query("GetReasonCodeList", "10001"
                    , new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "P_REASONCODECLASSID", cboMiddleClass.GetValue().ToString() } });

            cboReason.Editor.ShowHeader = false;
            #endregion
            */
        }
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
            // 더블클릭 시 체크박스 체크
            SmartBandedGridView view = (SmartBandedGridView)sender;

            if (grdWIP.View.IsRowChecked(view.FocusedRowHandle))
                grdWIP.View.CheckRow(view.FocusedRowHandle, false);
            else
                grdWIP.View.CheckRow(view.FocusedRowHandle, true);
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

            int rowIndex = grdWIP.View.FocusedRowHandle;

            if (rowIndex == e.RowHandle)
            {
                e.Appearance.BackColor = Color.FromArgb(254, 254, 16);
                e.HighPriority = true;
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
            if (e.RowHandle < 0)
                return;

            int rowIndex = grdHold.View.FocusedRowHandle;

            if (rowIndex == e.RowHandle)
            {
                e.Appearance.BackColor = Color.FromArgb(254, 254, 16);
                e.HighPriority = true;
            }
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
            DataTable lockList = grdHold.DataSource as DataTable;

            if (lockList == null || lockList.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }

            // Locking 분류
            string holdTopClass = (string)cboTopClass.GetValue();
            //string holdMiddleClass = (string)cboMiddleClass.GetValue();
            string holdkReason = (string)cboReason.GetValue();

            string HoldeState = Format.GetTrimString(cboState.EditValue);

            string transactionType = tabHold.SelectedTabPage == tpHoldVerify ? "HoldVerify" : "Scrap";

            string strDefectCode = string.Empty;
            string strQcSegmentId = string.Empty;

            if (tabHold.SelectedTabPage == tpScrap)
            {
                strDefectCode = txtDefectCode.Editor.SelectedData.FirstOrDefault()["DEFECTCODE"].ToString();
                strQcSegmentId = txtDefectCode.Editor.SelectedData.FirstOrDefault()["QCSEGMENTID"].ToString();
            }
            MessageWorker worker = new MessageWorker("SaveLotHoldVerify");
            worker.SetBody(new MessageBody()
            {
                { "TransactionType", transactionType },
                { "EnterpriseID", UserInfo.Current.Enterprise },
                { "PlantID", UserInfo.Current.Plant },
                { "HoldTopClass", holdTopClass },
                { "HoldState", HoldeState },
                //{ "HoldMiddleClass", holdMiddleClass },
                { "ReasonCode", holdkReason },
                { "Comments", txtComment.Text },
                { "UserId", UserInfo.Current.Id },
                { "DefectCode",  strDefectCode},
                { "QcSegmentId", strQcSegmentId },
                { "Lotlist", lockList }
            });

            worker.Execute();

            // Data 초기화
            this.cboTopClass.EditValue = null;
            //this.cboMiddleClass.EditValue = null;
            this.cboReason.EditValue = null;
            this.txtComment.Text = string.Empty;
            grdHold.DataSource = null;
            cboState.EditValue = null;
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
            this.grdHold.DataSource = null;

            //P_HOLDSTATE
            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            if(tabHold.SelectedTabPage == tpHoldVerify)
            {
                if (values.ContainsKey("P_PERIOD_PERIODFR")) values.Remove("P_PERIOD_PERIODFR");

                if (values.ContainsKey("P_PERIOD_PERIODTO")) values.Remove("P_PERIOD_PERIODTO");
            }
            else
            {
                if(values.ContainsKey("P_HOLDSTATE"))
                {
                    values.Remove("P_HOLDSTATE");
                    values.Add("P_HOLDSTATE", "Verify");
                }
            }
            DataTable dt = await SqlExecuter.QueryAsyncDirect("SelectHoldVerifyList", "10001", values);

            if (dt.Rows.Count < 1)
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            if (tabHold.SelectedTabPage == tpHoldVerify)
                grdWIP.DataSource = dt;
            else
                grdScrap.DataSource = dt;


        }

        #endregion

        #region ◆ 유효성 검사 |

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();


            DataTable dtTarget = grdHold.DataSource as DataTable;

            if (dtTarget == null || dtTarget.Rows.Count == 0) return;

            if (tabHold.SelectedTabPage == tpHoldVerify)
            {
                if (Format.GetTrimString(cboState.Editor.GetDataValue()).Equals(string.Empty))
                {
                    throw MessageException.Create("CheckSelectHoldState");
                }
            }
            else
            {
                if (Format.GetTrimString(txtDefectCode.Editor.GetValue()).Equals(string.Empty))
                {
                    throw MessageException.Create("NoDefectCode");
                }
            }

            string HoldState = Format.GetTrimString(cboState.Editor.GetDataValue());
            
            string CurState = dtTarget.AsEnumerable().Select(c => c.Field<string>("HOLDSTATECODE")).FirstOrDefault();

            if(CurState.Equals("Normal"))
            {
                if(Format.GetTrimString(cboTopClass.EditValue).Equals(string.Empty) || Format.GetTrimString(cboReason.EditValue).Equals(string.Empty) || Format.GetTrimString(txtComment.Text).Equals(string.Empty))
                {
                    throw MessageException.Create("CheckSelectHoldReason");
                }
            }
            else if((CurState.Equals("Hold") || CurState.Equals("Verify")) && HoldState.Equals("Normal"))
            {
                if (Format.GetTrimString(txtComment.Text).Equals(string.Empty))
                {
                    throw MessageException.Create("CheckSelectHoldReason");
                }
            }
            else
            {
                if (Format.GetTrimString(txtComment.Text).Equals(string.Empty))
                {
                    throw MessageException.Create("CheckSelectHoldReason");
                }
            }
        }

        #endregion

        #region ◆ Private Function |

        // TODO : 화면에서 사용할 내부 함수 추가

        #endregion
    }
}
