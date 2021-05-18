#region using
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Grid.BandedGrid;
using Micube.Framework.SmartControls.Grid;

using Micube.SmartMES.Commons;

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

using DevExpress.Utils;
#endregion

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 공정 다중작업 > 일괄인계
    /// 업  무  설  명  : 일괄인계
    /// 생    성    자  : 박정훈
    /// 생    성    일  : 2019-11-26
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class MultiLotSend : SmartConditionManualBaseForm
    {
        #region ◆ Local Variables |

        #endregion

        #region ◆ 생성자 |
        /// <summary>
        /// 생성자
        /// </summary>
        public MultiLotSend()
        {
            InitializeComponent();
        }
        #endregion

        #region ◆ 컨텐츠 영역 초기화 |
        /// <summary>
        /// Control 설정
        /// </summary>
        protected override void InitializeContent()
        {
            base.InitializeContent();

            // TODO : 컨트롤 초기화 로직 구성
            InitializeControls();

            // Grid 초기화
            InitializeGrid();

            InitializeEvent();
        }

        #region ▶ Control 초기화 |
        /// <summary>
        /// Control 초기화
        /// </summary>
        private void InitializeControls()
        {
            ClearDetailInfo();

            // LOT 정보 GRID
            grdLotInfo.ColumnCount = 5;
            grdLotInfo.LabelWidthWeight = "40%";
            grdLotInfo.SetInvisibleFields("PROCESSPATHID", "PROCESSSEGMENTID", "PROCESSSEGMENTVERSION", "PRODUCTDEFVERSION",
                                        "PRODUCTTYPE", "DEFECTUNIT", "PCSPNL", "PANELPERQTY", "PROCESSSEGMENTTYPE", "STEPTYPE"
                                        , "ISRCLOT", "PROCESSSEGMENTCLASSID", "PROCESSUOM", "PCSARY", "SUBPROCESSDEFID", "LOTTYPE"
                                        , "STEPTYPE", "MATERIALCLASS", "TRACKINUSER", "ISPRINTRCLOTCARD", "ISPRINTLOTCARD", "PROCESSSEGMENTTYPE"
                                        , "PCSPNL", "PROCESSSTATE", "PROCESSPATHID", "NEXTPROCESSSEGMENTID", "NEXTPROCESSSEGMENTVERSION"
                                        , "PLANTID", "INPUTDATE", "PRODUCTDEFTYPE", "PRODUCTIONTYPE", "ISREWORK", "ISHOLD", "ISLOCKING", "RESOURCEID"
                                        , "TRACKINUSERNAME", "AREAID"
                                        );
            grdLotInfo.Enabled = false;

            grdLotInfo.Height = 150;

            #region - 작업장 |
            // 작업장
            ConditionItemSelectPopup areaCondition = new ConditionItemSelectPopup();
            areaCondition.Id = "AREA";
            areaCondition.SearchQuery = new SqlQuery("GetAreaListByAuthority", "10001", $"PLANTID={UserInfo.Current.Plant}", "ISMODIFY=Y", $"LANGUAGETYPE={UserInfo.Current.LanguageType}");
            areaCondition.ValueFieldName = "AREAID";
            areaCondition.DisplayFieldName = "AREANAME";
            areaCondition.SetPopupLayout("SELECTAREA", PopupButtonStyles.Ok_Cancel, true, false);
            areaCondition.SetPopupResultCount(1);
            areaCondition.SetPopupLayoutForm(600, 800, FormBorderStyle.FixedToolWindow);
            areaCondition.SetPopupAutoFillColumns("AREANAME");

            areaCondition.Conditions.AddTextBox("AREA");

            areaCondition.GridColumns.AddTextBoxColumn("AREAID", 150);
            areaCondition.GridColumns.AddTextBoxColumn("AREANAME", 200);

            txtArea.Editor.SelectPopupCondition = areaCondition;
            #endregion
        }
        #endregion

        #region ▶ 조회조건 설정 |
        /// <summary>
        /// 검색조건 설정
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();
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
            #region - 인계 Grid 설정 |
            grdLotList.GridButtonItem = GridButtonItem.Export;

            grdLotList.View.SetIsReadOnly();

            // CheckBox 설정
            this.grdLotList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdLotList.View.AddTextBoxColumn("LOTID", 180).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdLotList.View.AddTextBoxColumn("ENTERPRISEID", 80).SetIsHidden();
            grdLotList.View.AddTextBoxColumn("PLANTID", 80).SetIsHidden();
            grdLotList.View.AddTextBoxColumn("PRODUCTDEFID", 150).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdLotList.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdLotList.View.AddTextBoxColumn("PRODUCTDEFNAME", 250).SetIsReadOnly();
            grdLotList.View.AddTextBoxColumn("PROCESSDEFID", 80).SetIsHidden();
            grdLotList.View.AddTextBoxColumn("PROCESSDEFVERSION", 80).SetIsHidden();
            grdLotList.View.AddTextBoxColumn("PROCESSSEGMENTID", 150).SetIsHidden();
            grdLotList.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150).SetIsReadOnly();
            grdLotList.View.AddTextBoxColumn("USERSEQUENCE", 70).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdLotList.View.AddTextBoxColumn("UNIT", 60).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdLotList.View.AddTextBoxColumn("PANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}").SetIsReadOnly();
            grdLotList.View.AddTextBoxColumn("QTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}").SetIsReadOnly();
            grdLotList.View.AddTextBoxColumn("CUSTOMERNAME", 100).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdLotList.View.AddTextBoxColumn("INPUTDATE", 100).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdLotList.View.AddTextBoxColumn("PRODUCTIONORDERID", 120).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdLotList.View.AddTextBoxColumn("DUEDATE", 120).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdLotList.View.AddTextBoxColumn("PRODUCTIONTYPE", 80).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();

            grdLotList.View.PopulateColumns();

            #endregion

            #region - Lot Message |

            this.ucMessage.InitializeControls();

            #endregion

            #region - 특기사항 |
            grdComment.GridButtonItem = GridButtonItem.None;
            grdComment.ShowButtonBar = false;
            grdComment.ShowStatusBar = false;

            grdComment.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdComment.View.SetIsReadOnly();

            // 공정수순ID
            grdComment.View.AddTextBoxColumn("PROCESSPATHID", 150).SetValidationKeyColumn().SetIsHidden();
            // 공정수순
            grdComment.View.AddTextBoxColumn("USERSEQUENCE", 80);
            // 공정ID
            grdComment.View.AddTextBoxColumn("PROCESSSEGMENTID", 150).SetIsHidden();
            // 공정명
            grdComment.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
            // 특기사항
            grdComment.View.AddTextBoxColumn("DESCRIPTION", 500).SetLabel("REMARKS");
            // 현재공정여부
            grdComment.View.AddTextBoxColumn("ISCURRENTPROCESS", 70).SetIsHidden();

            grdComment.View.PopulateColumns();


            grdComment.View.OptionsView.ShowIndicator = false;
            grdComment.View.OptionsView.AllowCellMerge = true;

            grdComment.View.Columns["PROCESSPATHID"].OptionsColumn.AllowMerge = DefaultBoolean.False;
            grdComment.View.Columns["USERSEQUENCE"].OptionsColumn.AllowMerge = DefaultBoolean.False;
            grdComment.View.Columns["PROCESSSEGMENTID"].OptionsColumn.AllowMerge = DefaultBoolean.False;
            grdComment.View.Columns["PROCESSSEGMENTNAME"].OptionsColumn.AllowMerge = DefaultBoolean.False;
            grdComment.View.Columns["DESCRIPTION"].OptionsColumn.AllowMerge = DefaultBoolean.False;
            grdComment.View.Columns["ISCURRENTPROCESS"].OptionsColumn.AllowMerge = DefaultBoolean.False;
            #endregion

            #region - 공정 SPEC |
            grdProcessSpec.GridButtonItem = GridButtonItem.None;
            grdProcessSpec.ShowButtonBar = false;
            grdProcessSpec.ShowStatusBar = false;

            grdProcessSpec.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdProcessSpec.View.SetIsReadOnly();

            // 공정수순ID
            grdProcessSpec.View.AddTextBoxColumn("PROCESSPATHID", 150).SetValidationKeyColumn().SetIsHidden();
            // 공정수순
            grdProcessSpec.View.AddTextBoxColumn("USERSEQUENCE", 80);
            // 공정ID
            grdProcessSpec.View.AddTextBoxColumn("PROCESSSEGMENTID", 150).SetIsHidden();
            // 공정명
            grdProcessSpec.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
            // 항목
            grdProcessSpec.View.AddTextBoxColumn("SPECCLASSNAME", 120).SetLabel("SPECITEM");
            // 하한값
            grdProcessSpec.View.AddSpinEditColumn("LSL", 90).SetDisplayFormat("#,##0.00");
            // 중간값
            grdProcessSpec.View.AddSpinEditColumn("SL", 90).SetDisplayFormat("#,##0.00");
            // 상한값
            grdProcessSpec.View.AddSpinEditColumn("USL", 90).SetDisplayFormat("#,##0.00");
            // 현재공정여부
            grdProcessSpec.View.AddTextBoxColumn("ISCURRENTPROCESS", 70).SetIsHidden();

            grdProcessSpec.View.PopulateColumns();

            grdProcessSpec.View.OptionsView.ShowIndicator = false;
            grdProcessSpec.View.OptionsView.AllowCellMerge = true;

            grdProcessSpec.View.Columns["SPECCLASSNAME"].OptionsColumn.AllowMerge = DefaultBoolean.False;
            grdProcessSpec.View.Columns["LSL"].OptionsColumn.AllowMerge = DefaultBoolean.False;
            grdProcessSpec.View.Columns["SL"].OptionsColumn.AllowMerge = DefaultBoolean.False;
            grdProcessSpec.View.Columns["USL"].OptionsColumn.AllowMerge = DefaultBoolean.False;
            grdProcessSpec.View.Columns["ISCURRENTPROCESS"].OptionsColumn.AllowMerge = DefaultBoolean.False;
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
            // TextBox Event
            txtLotId.Editor.KeyDown += TxtLotId_KeyDown;
            txtLotId.Editor.Click += Editor_Click;

            // Grid Event
            grdLotList.View.DoubleClick += View_DoubleClick;
        }

        #region ▶ TEXBOX Event |
        /// <summary>
        /// TextBox Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Editor_Click(object sender, EventArgs e)
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
            if (e.KeyCode != Keys.Enter) return;

            SearchData();
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
            DataRow dr = grdLotList.View.GetFocusedDataRow();

            if (dr == null) return;

            // LOT 정보 조회
            string strLotid = dr["LOTID"].ToString();

            getLotInfo(strLotid);
            //this.cboInspector.EditValue = strInspector;

            grdLotList.View.CheckRow(((SmartBandedGridView)sender).FocusedRowHandle, true);
        }
        #endregion

        #endregion

        #endregion

        #region ◆ 툴바 |

        #region ▶ ToolBar 저장버튼 클릭 |
        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            if (ShowMessage(MessageBoxButtons.YesNo, DialogResult.No, "InfoSave") == DialogResult.No) return;

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
            DataTable data = grdLotList.View.GetCheckedRows();

            if (data == null || data.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }

            string strTransitArea = string.Empty;
            string strResourceid = string.Empty;
            if (cboTransitArea.GetValue() != null)
            {
                strTransitArea = Format.GetFullTrimString(cboTransitArea.Editor.GetColumnValue("AREAID"));
                strResourceid = Format.GetFullTrimString(cboTransitArea.Editor.GetColumnValue("RESOURCEID"));
            }

            if (string.IsNullOrWhiteSpace(strTransitArea))
            {
                // 인계처리시 인계작업장을 입력해야합니다.
                throw MessageException.Create("NeedToInputAreaWhenTakeOver");
            }

            MessageWorker worker = new MessageWorker("SaveMultiLotSend");
            worker.SetBody(new MessageBody()
            {
                { "EnterpriseID", UserInfo.Current.Enterprise },
                { "PlantID", UserInfo.Current.Plant },
                { "UserId", UserInfo.Current.Id },
                { "TransitArea", strTransitArea },
                { "Resourceid", strResourceid },
                { "Lotlist", data }
            });

            worker.Execute();

            ShowMessage("SuccedSave");

            ClearDetailInfo();

            this.txtLotId.Text = string.Empty;
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
            grdLotList.View.ClearDatas();

            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("P_PROCESSSTATE", "WaitForSend");

            DataTable dt = await SqlExecuter.QueryAsync("SelectWIPMultiStateList", "10001", values);

            if (dt.Rows.Count < 1)
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdLotList.DataSource = dt;
        }

        #endregion

        #region ◆ 유효성 검사 |

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            // TODO : 유효성 로직 변경
            var areaId = txtArea.Editor.SelectedData.FirstOrDefault()["AREAID"];

            if (areaId == null || string.IsNullOrWhiteSpace(txtArea.Editor.SelectedData.FirstOrDefault()["AREAID"].ToString()))
            {
                // 작업장선택은 필수 선택입니다.
                throw MessageException.Create("NoInputArea");
            }

            DataTable dt = grdLotList.View.GetCheckedRows();

            if (dt == null || dt.Rows.Count <= 0)
            {
                throw MessageException.Create("NoSaveData");
            }

            // 인계 작업장
            if (cboTransitArea.GetValue() == null)
            {
                throw MessageException.Create("SelectTransitArea");
            }
        }

        #endregion

        #region ◆ Private Function |

        // TODO : 화면에서 사용할 내부 함수 추가

        #region ▶ Control Data 초기화 |
        /// <summary>
        /// Control Data 초기화
        /// </summary>
        private void ClearDetailInfo()
        {
            // Data 초기화
            grdLotList.View.ClearDatas();
            grdLotInfo.ClearData();
            grdComment.View.ClearDatas();
            grdProcessSpec.View.ClearDatas();

            this.ucMessage.ClearDatas();

            this.tabMain.Enabled = false;
        }
        #endregion

        #region ▶ 인계 작업장 설정 |
        /// <summary>
        /// 인계 작업장 설정
        /// </summary>
        private void SetNextArea()
        {
            DataTable dt = grdLotList.View.GetCheckedRows();

            var nextSegmentId = dt.AsEnumerable().Select(r => r.Field<string>("PROCESSSEGMENTID")).Distinct().FirstOrDefault();
            var productDefId = dt.AsEnumerable().Select(r => r.Field<string>("PRODUCTDEFID")).Distinct().FirstOrDefault();
            var productDefVer = dt.AsEnumerable().Select(r => r.Field<string>("PRODUCTDEFVERSION")).Distinct().FirstOrDefault();
            var processDefId = dt.AsEnumerable().Select(r => r.Field<string>("PROCESSDEFID")).Distinct().FirstOrDefault();
            var processDefVer = dt.AsEnumerable().Select(r => r.Field<string>("PROCESSDEFVERSION")).Distinct().FirstOrDefault();

            // 인계작업장
            Dictionary<string, object> transitAreaParam = new Dictionary<string, object>();
            transitAreaParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            transitAreaParam.Add("PLANTID", UserInfo.Current.Plant);
            transitAreaParam.Add("PRODUCTDEFID", productDefId);
            transitAreaParam.Add("PRODUCTDEFVERSION", productDefVer);
            transitAreaParam.Add("PROCESSDEFID", processDefId);
            transitAreaParam.Add("PROCESSDEFVERSION", processDefVer);
            transitAreaParam.Add("PROCESSSEGMENTID", nextSegmentId);
            transitAreaParam.Add("RESOURCETYPE", "Resource");
            transitAreaParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable transitAreaList = new DataTable();

            transitAreaList = SqlExecuter.Query("GetTransitNextAreaList", "10001", transitAreaParam);

            string primaryAreaId = "";

            cboTransitArea.Editor.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboTransitArea.Editor.ShowHeader = false;
            cboTransitArea.Editor.ValueMember = "AREAID";
            cboTransitArea.Editor.DisplayMember = "AREANAME";
            cboTransitArea.Editor.UseEmptyItem = true;
            cboTransitArea.Editor.EmptyItemValue = "";
            cboTransitArea.Editor.EmptyItemCaption = "";
            cboTransitArea.Editor.DataSource = transitAreaList;
            cboTransitArea.EditValue = string.IsNullOrEmpty(primaryAreaId) ? cboTransitArea.Editor.EmptyItemValue : primaryAreaId;
        }
        #endregion

        #region ▶ 데이터 검색 |
        /// <summary>
        /// 데이터 검색
        /// </summary>
        private void SearchData()
        {
            ClearDetailInfo();

            if (txtArea.Editor.SelectedData.Count() < 1 || string.IsNullOrWhiteSpace(txtArea.EditValue.ToString()) || string.IsNullOrWhiteSpace(txtLotId.Text))
            {
                // 작업장, LOT No.는 필수 입력 항목입니다.
                ShowMessage("AreaLotIdIsRequired");
                ClearDetailInfo();
                return;
            }

            string strLotId = CommonFunction.changeArgString(this.txtLotId.Editor.Text);

            // 대상 LOT 조회 :: RootLotID 기준
            Dictionary<string, object> param2 = new Dictionary<string, object>();
            param2.Add("PLANTID", UserInfo.Current.Plant);
            param2.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param2.Add("AREAID", txtArea.Editor.SelectedData.FirstOrDefault()["AREAID"].ToString());
            param2.Add("LOTID", strLotId);
            param2.Add("PROCESSSTATE", Constants.WaitForSend);

            DataTable dt = SqlExecuter.Query("SelectWIPMultiStateList", "10001", param2);

            if (dt.Rows.Count < 1)
            {
                DataTable AreaLot = SqlExecuter.Query("GetLotAreaAndState", "10001", param2);
                ShowMessage("NotAreaOrState", Format.GetString(AreaLot.Rows[0]["AREANAME"]), Format.GetString(AreaLot.Rows[0]["PROCESSTATE"]));
                return;
            }

            grdLotList.DataSource = dt;

            getLotInfo(strLotId);

            grdLotInfo.Enabled = true;
            tabMain.Enabled = true;
        }
        #endregion

        #region ▶ Lot 정보 조회 |
        /// <summary>
        /// Lot 정보 조회
        /// </summary>
        /// <param name="strLotId"></param>
        private void getLotInfo(string strLotId)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            param.Add("PLANTID", UserInfo.Current.Plant);
            param.Add("AREAID", txtArea.Editor.SelectedData.FirstOrDefault()["AREAID"].ToString());
            param.Add("LOTID", strLotId);
            param.Add("PROCESSSTATE", Constants.WaitForSend);
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable processDefTypeInfo = SqlExecuter.Query("GetProcessDefTypeByProcess", "10001", param);

            string processDefType = "";
            string lastRework = "";

            if (processDefTypeInfo.Rows.Count > 0)
            {
                DataRow row = processDefTypeInfo.AsEnumerable().FirstOrDefault();

                processDefType = Format.GetString(row["PROCESSDEFTYPE"]);
                lastRework = Format.GetString(row["LASTREWORK"]);
            }

            string queryVersion = "10001";

            if (processDefType == "Rework")
                queryVersion = "10011";

            DataTable lotInfo = SqlExecuter.Query("SelectLotInfoByProcess", queryVersion, param);

            if (lotInfo.Rows.Count < 1)
            {
                ClearDetailInfo();

                txtLotId.Text = "";
                txtLotId.Focus();

                // 조회할 데이터가 없습니다.
                throw MessageException.Create("NoSelectData");
            }

            grdLotInfo.DataSource = lotInfo;

            // Lot Message Setting
            this.ucMessage.SetDatasource(CommonFunction.changeArgString(strLotId), grdLotInfo.GetFieldValue("PRODUCTDEFID").ToString()
                , grdLotInfo.GetFieldValue("PRODUCTDEFVERSION").ToString()
                , grdLotInfo.GetFieldValue("PROCESSSEGMENTID").ToString());

            #region - 특기사항 |
            // 특기사항
            Dictionary<string, object> commentParam = new Dictionary<string, object>();
            commentParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            commentParam.Add("PLANTID", UserInfo.Current.Plant);
            commentParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            commentParam.Add("LOTID", CommonFunction.changeArgString(txtLotId.Text));
            commentParam.Add("PROCESSSEGMENTID", grdLotInfo.GetFieldValue("PROCESSSEGMENTID").ToString());

            this.grdComment.DataSource = SqlExecuter.Query("SelectCommentByProcess", "10001", commentParam); 
            #endregion

            #region - 공정 SPEC|
            // 공정 SPEC
            Dictionary<string, object> processSpecParam = new Dictionary<string, object>();
            processSpecParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            processSpecParam.Add("PLANTID", UserInfo.Current.Plant);
            processSpecParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            processSpecParam.Add("LOTID", CommonFunction.changeArgString(txtLotId.Text));
            processSpecParam.Add("PROCESSSEGMENTID", grdLotInfo.GetFieldValue("PROCESSSEGMENTID").ToString());
            processSpecParam.Add("CONTROLTYPE", "XBARR");
            processSpecParam.Add("SPECCLASSID", "OperationSpec");

            grdProcessSpec.DataSource = SqlExecuter.Query("SelectProcessSpecByProcess", "10001", processSpecParam); 
            #endregion

            #region - 인계작업장 설정 |
            // 인계작업장
            Dictionary<string, object> transitAreaParam = new Dictionary<string, object>();
            transitAreaParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            transitAreaParam.Add("PLANTID", UserInfo.Current.Plant);
            transitAreaParam.Add("LOTID", strLotId);
            if (grdLotInfo.GetFieldValue("NEXTPROCESSSEGMENTID") != null && !string.IsNullOrWhiteSpace(grdLotInfo.GetFieldValue("NEXTPROCESSSEGMENTID").ToString()))
            {
                transitAreaParam.Add("PROCESSSEGMENTID", grdLotInfo.GetFieldValue("NEXTPROCESSSEGMENTID").ToString());
                transitAreaParam.Add("PROCESSSEGMENTVERSION", grdLotInfo.GetFieldValue("NEXTPROCESSSEGMENTVERSION").ToString());
            }
            transitAreaParam.Add("RESOURCETYPE", "Resource");
            transitAreaParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable transitAreaList = new DataTable();

            string stepType = grdLotInfo.GetFieldValue("STEPTYPE").ToString();
            // 인계 작업장
            if (stepType.Split(',').Contains("WaitForSend"))
            {
                cboTransitArea.Visible = true;

                transitAreaList = SqlExecuter.Query("GetTransitAreaList", "10032", transitAreaParam);

                string primaryAreaId = "";

                //후공정이 없으면 현재공정의 작업장을 보여준다.
                if (string.IsNullOrWhiteSpace(grdLotInfo.GetFieldValue("NEXTPROCESSSEGMENTID").ToString()))
                {
                    primaryAreaId = grdLotInfo.GetFieldValue("AREAID").ToString();
                }
                else
                {
                    for (int i = 0; i < transitAreaList.Rows.Count; i++)
                    {
                        DataRow areaRow = transitAreaList.Rows[i];

                        if (areaRow["ISPRIMARYRESOURCE"].ToString() == "Y")
                        {
                            primaryAreaId = areaRow["AREAID"].ToString();
                            break;
                        }
                    }
                }

                cboTransitArea.Editor.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
                cboTransitArea.Editor.ShowHeader = false;
                cboTransitArea.Editor.ValueMember = "RESOURCEID";
                cboTransitArea.Editor.DisplayMember = "RESOURCENAME";
                cboTransitArea.Editor.UseEmptyItem = true;
                cboTransitArea.Editor.EmptyItemValue = "";
                cboTransitArea.Editor.EmptyItemCaption = "";
                cboTransitArea.Editor.DataSource = transitAreaList;
                cboTransitArea.EditValue = string.IsNullOrEmpty(primaryAreaId) ? cboTransitArea.Editor.EmptyItemValue : primaryAreaId;
            }
            else
            {
                cboTransitArea.Visible = false;

                cboTransitArea.EditValue = null;
                cboTransitArea.Editor.DataSource = null;
            } 
            #endregion
        }
        #endregion

        #endregion
    }
}
