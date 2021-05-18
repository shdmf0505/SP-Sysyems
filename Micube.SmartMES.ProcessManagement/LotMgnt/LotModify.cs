#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Grid.BandedGrid;
using Micube.Framework.SmartControls.Grid;

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

using DevExpress.Utils;
using Micube.SmartMES.Commons.Controls;
#endregion

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 공정 관리 > Lot 관리 > LOT 정보 변경
    /// 업  무  설  명  : LOT정보를 수정 (주의 : 아무나 권한주면 안됨)
    /// 생    성    자  : 박정훈
    /// 생    성    일  : 2019-12-26
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class LotModify : SmartConditionManualBaseForm
    {
        #region ◆ Local Variables |

        // TODO : 화면에서 사용할 내부 변수 추가
        #endregion

        #region ◆ 생성자 |

        /// <summary>
        /// 생성자
        /// </summary>
        public LotModify()
        {
            InitializeComponent();

            string[] AuthorityUserId = { "jhpark", "sybae", "hykang", "yshwang", "swjeong", "sjlee", "jshan", "20170385" };

            if (!AuthorityUserId.Contains(UserInfo.Current.Id))
            {
                // 권한이없습니다.
                throw MessageException.Create("Powers");
            }
        }

        #endregion

        #region ◆ 컨텐츠 영역 초기화 |

        /// <summary>
        /// 화면의 컨텐츠 영역을 초기화한다.
        /// </summary>
        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeEvent();

            // TODO : 컨트롤 초기화 로직 구성
            InitializeControls();

            // Grid 초기화
            //InitializeGrid();
        }

        #region ▶ Control 초기화 |
        /// <summary>
        /// Control 초기화
        /// </summary>
        private void InitializeControls()
        {
            ClearControls();

            #region ▷ CodeHelp |

            #region - 자원 |
            ConditionItemSelectPopup resourceCondition = new ConditionItemSelectPopup();
            resourceCondition.Id = "RESOURCE";
            resourceCondition.SearchQuery = new SqlQuery("GetResourcePopup", "10002", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}");
            resourceCondition.ValueFieldName = "RESOURCEID";
            resourceCondition.DisplayFieldName = "DESCRIPTION";
            resourceCondition.SetPopupLayout("SELECTRESOURCE", PopupButtonStyles.Ok_Cancel, true, false);
            resourceCondition.SetPopupResultCount(1);
            resourceCondition.SetPopupLayoutForm(800, 800, FormBorderStyle.SizableToolWindow);
            resourceCondition.SetPopupAutoFillColumns("DESCRIPTION");

            resourceCondition.Conditions.AddTextBox("RESOURCEID");
            resourceCondition.Conditions.AddTextBox("DESCRIPTION");

            resourceCondition.GridColumns.AddTextBoxColumn("RESOURCEID", 100);
            resourceCondition.GridColumns.AddTextBoxColumn("DESCRIPTION", 220);
            resourceCondition.GridColumns.AddTextBoxColumn("AREAID", 80);
            resourceCondition.GridColumns.AddTextBoxColumn("AREANAME", 200);

            txtResourceID.Editor.SelectPopupCondition = resourceCondition;

            // 선택값중 작업장 ID 기본 세팅
            resourceCondition.SetPopupApplySelection((selectedRows, dataGridRows) =>
            {
                List<DataRow> list = selectedRows.ToList<DataRow>();

                if (list.Count == 0) return;

                this.txtAreaID.EditValue = list[0]["AREAID"];
            });
            #endregion

            #region - 공정 |
            ConditionItemSelectPopup segmentCondition = new ConditionItemSelectPopup();
            segmentCondition.Id = "PROCESSSEGMENTID";
            // segmentCondition.SearchQuery = new SqlQuery("GetProcessSegmentList", "10002", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "PROCESSSEGMENTCLASSTYPE=MiddleProcessSegmentClass");
            segmentCondition.SearchQuery = new SqlQuery("GetProcessPathByProcessDefId", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"P_PROCESSDEFID={this.txtProcessDefID.Text}", $"P_PROCESSDEFVERSION={this.txtProcessDefVersion.Text}");
            segmentCondition.ValueFieldName = "PROCESSSEGMENTID";
            segmentCondition.DisplayFieldName = "PROCESSSEGMENTNAME";
            segmentCondition.SetPopupLayout("SELECTPROCESSSEGMENTID", PopupButtonStyles.Ok_Cancel, true, false);
            segmentCondition.SetPopupResultCount(1);
            segmentCondition.SetPopupLayoutForm(800, 800, FormBorderStyle.SizableToolWindow);
            segmentCondition.SetPopupAutoFillColumns("PROCESSSEGMENTNAME");

            segmentCondition.GridColumns.AddTextBoxColumn("PROCESSPATHID", 150).SetTextAlignment(TextAlignment.Center);
            segmentCondition.GridColumns.AddTextBoxColumn("PROCESSDEFID", 100).SetTextAlignment(TextAlignment.Center);
            segmentCondition.GridColumns.AddTextBoxColumn("PROCESSDEFVERSION", 80).SetTextAlignment(TextAlignment.Center); 
            segmentCondition.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150).SetTextAlignment(TextAlignment.Center);
            segmentCondition.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 150);
            segmentCondition.GridColumns.AddTextBoxColumn("USERSEQUENCE", 80).SetTextAlignment(TextAlignment.Center);
            segmentCondition.GridColumns.AddTextBoxColumn("PATHSEQUENCE", 80).SetTextAlignment(TextAlignment.Center);

            txtProcessSegmentID.Editor.SelectPopupCondition = segmentCondition;

            // 선택값중 공정관련 정보 세팅
            segmentCondition.SetPopupApplySelection((selectedRows, dataGridRows) =>
            {
                List<DataRow> list = selectedRows.ToList<DataRow>();

                if (list.Count == 0) return;

                this.txtUserSequence.EditValue = list[0]["USERSEQUENCE"];
                if(txtSubProcessDefID.EditValue != null)
                    this.txtProcessStack.EditValue = string.Format("{0}.{1}", txtProcessStack.Text.Split('.')[0], list[0]["PROCESSPATHID"]);
                else
                    this.txtProcessStack.EditValue = list[0]["PROCESSPATHID"];
            });
            #endregion
            #endregion

            #region ▷ ComboBox |

            #region - Lot State |
            cboLotState.Editor.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboLotState.Editor.ValueMember = "STATEID";
            cboLotState.Editor.DisplayMember = "STATENAME";

            cboLotState.Editor.DataSource = SqlExecuter.Query("GetState", "10001"
                    , new Dictionary<string, object>() { { "STATEMODELID", "LotState" } });

            cboLotState.Editor.ShowHeader = false;
            #endregion

            #region - Process State |
            cboProcessState.Editor.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboProcessState.Editor.ValueMember = "STATEID";
            cboProcessState.Editor.DisplayMember = "STATENAME";

            cboProcessState.Editor.DataSource = SqlExecuter.Query("GetState", "10001"
                    , new Dictionary<string, object>() { { "STATEMODELID", "LotProcessState" } });

            cboProcessState.Editor.ShowHeader = false;
            #endregion

            #region - Lot Type |
            cboLotType.Editor.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboLotType.Editor.ValueMember = "CODEID";
            cboLotType.Editor.DisplayMember = "CODENAME";

            cboLotType.Editor.DataSource = SqlExecuter.Query("GetCodeList", "00001"
                    , new Dictionary<string, object>() { { "CODECLASSID", "LotType" }, { "LANGUAGETYPE", UserInfo.Current.LanguageType } });

            cboLotType.Editor.ShowHeader = false;
            #endregion

            #region - Lot Create Type |
            cboLotCreateType.Editor.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboLotCreateType.Editor.ValueMember = "CODEID";
            cboLotCreateType.Editor.DisplayMember = "CODENAME";

            cboLotCreateType.Editor.DataSource = SqlExecuter.Query("GetCodeList", "00001"
                    , new Dictionary<string, object>() { { "CODECLASSID", "LotCreateType" }, { "LANGUAGETYPE", UserInfo.Current.LanguageType } });

            cboLotCreateType.Editor.ShowHeader = false;
            #endregion

            #region - UOM | 
            cboUnit.Editor.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboUnit.Editor.ValueMember = "UOMDEFID";
            cboUnit.Editor.DisplayMember = "UOMDEFNAME";

            cboUnit.Editor.DataSource = SqlExecuter.Query("GetUomDefinitionList", "10001"
                    , new Dictionary<string, object>() { { "UOMCLASSID", "Segment" } });

            cboUnit.Editor.ShowHeader = false;
            #endregion

            #region - Hold | 
            cboIsHold.Editor.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboIsHold.Editor.ValueMember = "CODEID";
            cboIsHold.Editor.DisplayMember = "CODENAME";

            cboIsHold.Editor.DataSource = SqlExecuter.Query("GetCodeList", "00001"
                    , new Dictionary<string, object>() { { "CODECLASSID", "YesNo" }, { "LANGUAGETYPE", UserInfo.Current.LanguageType } });

            cboIsHold.Editor.ShowHeader = false;
            #endregion

            #region - Locking | 
            cboIsLocking.Editor.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboIsLocking.Editor.ValueMember = "CODEID";
            cboIsLocking.Editor.DisplayMember = "CODENAME";

            cboIsLocking.Editor.DataSource = SqlExecuter.Query("GetCodeList", "00001"
                    , new Dictionary<string, object>() { { "CODECLASSID", "YesNo" }, { "LANGUAGETYPE", UserInfo.Current.LanguageType } });

            cboIsLocking.Editor.ShowHeader = false;
            #endregion

            #region - Defected | 
            cboIsDefected.Editor.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboIsDefected.Editor.ValueMember = "CODEID";
            cboIsDefected.Editor.DisplayMember = "CODENAME";

            cboIsDefected.Editor.DataSource = SqlExecuter.Query("GetCodeList", "00001"
                    , new Dictionary<string, object>() { { "CODECLASSID", "YesNo" }, { "LANGUAGETYPE", UserInfo.Current.LanguageType } });

            cboIsDefected.Editor.ShowHeader = false;
            #endregion

            #region - Print LotCard | 
            cboIsPrintLotCard.Editor.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboIsPrintLotCard.Editor.ValueMember = "CODEID";
            cboIsPrintLotCard.Editor.DisplayMember = "CODENAME";

            cboIsPrintLotCard.Editor.DataSource = SqlExecuter.Query("GetCodeList", "00001"
                    , new Dictionary<string, object>() { { "CODECLASSID", "YesNo" }, { "LANGUAGETYPE", UserInfo.Current.LanguageType } });

            cboIsPrintLotCard.Editor.ShowHeader = false;
            #endregion

            #region - Print RC LotCard | 
            cboIsPrintRCLotCard.Editor.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboIsPrintRCLotCard.Editor.ValueMember = "CODEID";
            cboIsPrintRCLotCard.Editor.DisplayMember = "CODENAME";

            cboIsPrintRCLotCard.Editor.DataSource = SqlExecuter.Query("GetCodeList", "00001"
                    , new Dictionary<string, object>() { { "CODECLASSID", "YesNo" }, { "LANGUAGETYPE", UserInfo.Current.LanguageType } });

            cboIsPrintRCLotCard.Editor.ShowHeader = false;
            #endregion
            #endregion
        }
        #endregion

        #region ▶ Grid 초기화 |
        /// <summary>
        /// Grid 초기화
        /// </summary>
        private void InitializeGrid()
        {
        }

        #endregion

        #endregion

        #region ◆ Event |

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가

            // TextBox Event
            txtLotId.Editor.KeyDown += TxtLotId_KeyDown;
            txtLotId.Editor.Click += TxtLotIdEditor_Click;

            // CodeHelp
            txtProcessSegmentID.Editor.EditValueChanged += ProcessSegmentEditor_EditValueChanged;

            // Button Event
            btnSave.Click += BtnSave_Click;
        }

        #region ▶ TEXBOX Event |
        /// <summary>
        /// TextBox Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtLotIdEditor_Click(object sender, EventArgs e)
        {
            txtLotId.Editor.SelectAll();
        }

        /// <summary>
        /// LotID KeyDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtLotId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrWhiteSpace(txtLotId.Text))
                {
                    // 작업장, LOT No.는 필수 입력 항목입니다.
                    ShowMessage("AreaLotIdIsRequired");
                    return;
                }

                GetLotInfo(CommonFunction.changeArgString(txtLotId.Text));
            }
        }
        #endregion

        #region ▶ Button Event |
        /// <summary>
        /// 저장
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            OnToolbarSaveClick();
        }
        #endregion

        #region ▶ Grid Event |
        #endregion

        #region ▶ CodeHelp Event |
        /// <summary>
        /// 공정 CodeHelp EditValueChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProcessSegmentEditor_EditValueChanged(object sender, EventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(Format.GetString(txtSubProcessDefID.EditValue)))
                txtProcessSegmentID.Editor.SelectPopupCondition.SearchQuery = new SqlQuery("GetProcessPathByProcessDefId", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"P_PROCESSDEFID={this.txtSubProcessDefID.Text}", $"P_PROCESSDEFVERSION={this.txtSubProcessDefVersion.Text}");
            else
                txtProcessSegmentID.Editor.SelectPopupCondition.SearchQuery = new SqlQuery("GetProcessPathByProcessDefId", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"P_PROCESSDEFID={this.txtProcessDefID.Text}", $"P_PROCESSDEFVERSION={this.txtProcessDefVersion.Text}");
        }
        #endregion
        #endregion

        #region ◆ 툴바 |

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            // TODO : 저장 Rule 변경
            DataTable dt = new DataTable();
            dt.Columns.Add("LOTID");
            dt.Columns.Add("POID");
            dt.Columns.Add("LINENO");
            dt.Columns.Add("PLANTID");
            dt.Columns.Add("FACTORYID");
            dt.Columns.Add("RESOURCEID");
            dt.Columns.Add("AREAID");
            dt.Columns.Add("ROOTLOTID");
            dt.Columns.Add("PARENTLOTID");
            dt.Columns.Add("PNLROOTLOTID");
            dt.Columns.Add("MKLOTID");
            dt.Columns.Add("PRODUCTDEFID");
            dt.Columns.Add("PRODUCTDEFVERSION");
            dt.Columns.Add("PREVPRODUCTDEFID");
            dt.Columns.Add("PREVPRODUCTDEFVERSION");
            dt.Columns.Add("PROCESSDEFID");
            dt.Columns.Add("PROCESSDEFVERSION");
            dt.Columns.Add("SUBPROCESSDEFID");
            dt.Columns.Add("SUBPROCESSDEFVERSION");
            dt.Columns.Add("PROCESSPATHSTACK");
            dt.Columns.Add("USERSEQUENCE");
            dt.Columns.Add("PROCESSSEGMENTID");
            dt.Columns.Add("LOTSTARTDATE");
            dt.Columns.Add("LOTSTATE");
            dt.Columns.Add("PROCESSSTATE");
            dt.Columns.Add("LOTTYPE");
            dt.Columns.Add("LOTCREATEDTYPE");
            dt.Columns.Add("WEEK");
            dt.Columns.Add("UNIT");
            dt.Columns.Add("CREATEDQTY");
            dt.Columns.Add("PNLCREATEDQTY");
            dt.Columns.Add("PANELPERQTY");
            dt.Columns.Add("QTY");
            dt.Columns.Add("PCSQTY");
            dt.Columns.Add("PANELQTY");
            dt.Columns.Add("ISHOLD");
            dt.Columns.Add("ISLOCKING");
            dt.Columns.Add("WORKCOUNT");
            dt.Columns.Add("REWORKCOUNT");
            dt.Columns.Add("ISDEFECTED");
            dt.Columns.Add("ISPRINTLOTCARD");
            dt.Columns.Add("ISPRINTRCLOTCARD");

            DataRow dr = dt.NewRow();

            dr["LOTID"] = txtInputLotID.EditValue;
            dr["POID"] = txtPOID.EditValue;
            dr["LINENO"] = txtLineNo.EditValue;
            dr["PLANTID"] = txtPlantID.EditValue;
            dr["FACTORYID"] = txtFactoryID.EditValue;
            //dr["RESOURCEID"] = txtResourceID.EditValue;
            dr["RESOURCEID"] = txtResourceID.Editor.SelectedData.FirstOrDefault()["RESOURCEID"];
            dr["AREAID"] = txtAreaID.EditValue;
            dr["ROOTLOTID"] = txtRootLotID.EditValue;
            dr["PARENTLOTID"] = txtParentLotID.EditValue;
            dr["PNLROOTLOTID"] = txtPNLRootLotID.EditValue;
            dr["MKLOTID"] = txtMKLotID.EditValue;
            dr["PRODUCTDEFID"] = txtProductDefID.EditValue;
            dr["PRODUCTDEFVERSION"] = txtProductDefVersion.EditValue;
            dr["PREVPRODUCTDEFID"] = txtPrevProductDefID.EditValue;
            dr["PREVPRODUCTDEFVERSION"] = txtPrevProductDefVersion.EditValue;
            dr["PROCESSDEFID"] = txtProcessDefID.EditValue;
            dr["PROCESSDEFVERSION"] = txtProcessDefVersion.EditValue;
            dr["SUBPROCESSDEFID"] = txtSubProcessDefID.EditValue;
            dr["SUBPROCESSDEFVERSION"] = txtSubProcessDefVersion.EditValue;
            dr["PROCESSPATHSTACK"] = txtProcessStack.EditValue;
            dr["USERSEQUENCE"] = txtUserSequence.EditValue;
            //dr["PROCESSSEGMENTID"] = txtProcessSegmentID.EditValue;
            dr["PROCESSSEGMENTID"] = txtProcessSegmentID.Editor.SelectedData.FirstOrDefault()["PROCESSSEGMENTID"];
            dr["LOTSTATE"] = cboLotState.EditValue;
            dr["PROCESSSTATE"] = cboProcessState.EditValue;
            dr["LOTTYPE"] = cboLotType.EditValue;
            dr["LOTCREATEDTYPE"] = cboLotCreateType.EditValue;
            dr["WEEK"] = txtWeek.EditValue;
            dr["UNIT"] = cboUnit.EditValue;
            dr["CREATEDQTY"] = txtCreatedQTY.EditValue;
            dr["PNLCREATEDQTY"] = txtPNLRootLotQTY.EditValue;
            dr["PANELPERQTY"] = txtPanelPerQTY.EditValue;
            dr["QTY"] = txtQTY.EditValue;
            dr["PCSQTY"] = txtPCSQTY.EditValue;
            dr["PANELQTY"] = txtPNLQTY.EditValue;
            dr["ISHOLD"] = cboIsHold.EditValue;
            dr["ISLOCKING"] = cboIsLocking.EditValue;
            dr["WORKCOUNT"] = txtWorkCount.EditValue;
            dr["REWORKCOUNT"] = txtReworkCount.EditValue;
            dr["ISDEFECTED"] = cboIsDefected.EditValue;
            dr["ISPRINTLOTCARD"] = cboIsPrintLotCard.EditValue;
            dr["ISPRINTRCLOTCARD"] = cboIsPrintRCLotCard.EditValue;

            dt.Rows.Add(dr);

            MessageWorker messageWorker = new MessageWorker("SaveLot");
            messageWorker.SetBody(new MessageBody()
            {
                { "EnterpriseId", UserInfo.Current.Enterprise },
                { "PlantId", UserInfo.Current.Plant },
                { "UserId", UserInfo.Current.Id },
                { "LotId", this.txtInputLotID.EditValue },
                { "LotInfo", dt }
            });

            messageWorker.Execute();

            ClearLotDetailInfo();
        }

        #endregion

        #region ◆ 검색 |

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

        #region ◆ 유효성 검사 |

        /// <summary>
        /// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();
            
            if (txtResourceID.EditValue == null || string.IsNullOrWhiteSpace(txtResourceID.EditValue.ToString()))
            {
                // 자원 정보가 없습니다.
                throw MessageException.Create("NotExistResource");
            }
        }

        #endregion

        #region ◆ Private Function |

        // TODO : 화면에서 사용할 내부 함수 추가

        #region ▶ Control 초기화 |
        /// <summary>
        /// Control 초기화
        /// </summary>
        private void ClearControls()
        {
            ClearLotDetailInfo();
        }

        /// <summary>
        /// Lot 정보 Control 모두 초기화
        /// </summary>
        private void ClearLotDetailInfo()
        {
            foreach (Control ctrl in this.tableLayoutPanel1.Controls)
            {
                if (ctrl is SmartLabelTextBox)
                {
                    ((SmartLabelTextBox)ctrl).Editor.Text = string.Empty;
                    ((SmartLabelTextBox)ctrl).Editor.EditValue = string.Empty;
                }
                else if (ctrl is SmartLabelSelectPopupEdit)
                {
                    ((SmartLabelSelectPopupEdit)ctrl).Editor.Text = string.Empty;
                    ((SmartLabelSelectPopupEdit)ctrl).Editor.EditValue = string.Empty;
                }
                else if (ctrl is SmartLabelComboBox)
                {
                    ((SmartLabelComboBox)ctrl).Editor.Text = string.Empty;
                    ((SmartLabelComboBox)ctrl).Editor.EditValue = string.Empty;
                }
            }
        }
        #endregion

        #region ▶ GetLotInfo :: Lot 정보 조회 |
        /// <summary>
        /// Lot 정보 조회
        /// </summary>
        /// <param name="lotId"></param>
        private void GetLotInfo(string lotId)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LOTID", lotId);

            DataTable lotInfo = SqlExecuter.Query("SelectLotInfo", "10001", param);

            if (lotInfo == null || lotInfo.Rows.Count <= 0)
                return;

            DataRow row = lotInfo.Rows[0];

            ClearLotDetailInfo();

            this.txtInputLotID.Text = row["LOTID"].ToString();
            this.txtPOID.Text = row["PRODUCTIONORDERID"].ToString();
            this.txtLineNo.Text = row["LINENO"].ToString();
            this.txtPlantID.Text = row["PLANTID"].ToString();
            this.txtFactoryID.Text = row["FACTORYID"].ToString();
            this.txtResourceID.Editor.SetValue(row["RESOURCEID"]);
            this.txtAreaID.Text = row["AREAID"].ToString();
            this.txtRootLotID.Text = row["ROOTLOTID"].ToString();
            this.txtParentLotID.Text = row["PARENTLOTID"].ToString();
            this.txtPNLRootLotID.Text = row["PNLROOTLOTID"].ToString();
            this.txtMKLotID.Text = row["MKLOTID"].ToString();
            this.txtProductDefID.Text = row["PRODUCTDEFID"].ToString();
            this.txtProductDefVersion.Text = row["PRODUCTDEFVERSION"].ToString();
            this.txtPrevProductDefID.Text = row["PREVPRODUCTDEFID"].ToString();
            this.txtPrevProductDefVersion.Text = row["PREVPRODUCTDEFVERSION"].ToString();
            this.txtProcessDefID.Text = row["PROCESSDEFID"].ToString();
            this.txtProcessDefVersion.Text = row["PROCESSDEFVERSION"].ToString();
            this.txtSubProcessDefID.Text = row["SUBPROCESSDEFID"].ToString();
            this.txtSubProcessDefVersion.Text = row["SUBPROCESSDEFVERSION"].ToString();
            this.txtProcessStack.Text = row["PROCESSPATHSTACK"].ToString();
            this.txtUserSequence.Text = row["USERSEQUENCE"].ToString();
            this.txtProcessSegmentID.Editor.SetValue(row["PROCESSSEGMENTID"]);
            this.txtLotInputDate.Text = row["LOTSTARTDATE"].ToString();
            this.cboLotState.Editor.EditValue = row["LOTSTATE"].ToString();
            this.cboProcessState.Editor.EditValue = row["PROCESSSTATE"].ToString();
            this.cboLotType.Editor.EditValue = row["LOTTYPE"].ToString();
            this.cboLotCreateType.Editor.EditValue = row["LOTCREATEDTYPE"].ToString();
            this.txtWeek.Text = row["WEEK"].ToString();
            this.cboUnit.Editor.EditValue = row["UNIT"].ToString();
            this.txtCreatedQTY.Text = row["CREATEDQTY"].ToString();
            this.txtPNLRootLotQTY.Text = row["PNLCREATEDQTY"].ToString();
            this.txtPanelPerQTY.Text = row["PANELPERQTY"].ToString();
            this.txtQTY.Text = row["QTY"].ToString();
            this.txtPCSQTY.Text = row["PCSQTY"].ToString();
            this.txtPNLQTY.Text = row["PANELQTY"].ToString();
            this.cboIsHold.Editor.EditValue = row["ISHOLD"].ToString();
            this.cboIsLocking.Editor.EditValue = row["ISLOCKING"].ToString();
            this.txtWorkCount.Text = row["WORKCOUNT"].ToString();
            this.txtReworkCount.Text = row["REWORKCOUNT"].ToString();
            this.cboIsDefected.Editor.EditValue = row["ISDEFECTED"].ToString();
            this.cboIsPrintLotCard.Editor.EditValue = row["ISPRINTLOTCARD"].ToString();
            this.cboIsPrintRCLotCard.Editor.EditValue = row["ISPRINTRCLOTCARD"].ToString();
        } 
        #endregion

        #endregion
    }
}