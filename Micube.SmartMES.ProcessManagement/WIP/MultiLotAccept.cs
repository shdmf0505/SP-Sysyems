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
    /// 프 로 그 램 명  : 공정 다중작업 > 인수등록
    /// 업  무  설  명  : 인수등록
    /// 생    성    자  : 박정훈
    /// 생    성    일  : 2019-11-01
    /// 수  정  이  력  : 2019-11-26, 박정훈, 화면구성을 최종검사와 같이 수정
    /// 
    /// 
    /// </summary>
    public partial class MultiLotAccept : SmartConditionManualBaseForm
    {
        #region ◆ Local Variables |

        #endregion

        #region ◆ 생성자 |
        /// <summary>
        /// 생성자
        /// </summary>
        public MultiLotAccept()
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

            InitializeControls();

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
            #region - 인수 Grid 설정 |
            grdWIP.GridButtonItem = GridButtonItem.Export;

            grdWIP.View.SetIsReadOnly();

            // CheckBox 설정
            this.grdWIP.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdWIP.View.AddTextBoxColumn("LOTID", 180).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdWIP.View.AddTextBoxColumn("PRODUCTDEFID", 150).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdWIP.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdWIP.View.AddTextBoxColumn("PRODUCTDEFNAME", 250).SetIsReadOnly();
            grdWIP.View.AddTextBoxColumn("PROCESSSEGMENTID", 150).SetIsHidden();
            grdWIP.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150).SetIsReadOnly();
            grdWIP.View.AddTextBoxColumn("USERSEQUENCE", 70).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdWIP.View.AddTextBoxColumn("UNIT", 60).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdWIP.View.AddTextBoxColumn("PANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}").SetIsReadOnly();
            grdWIP.View.AddTextBoxColumn("QTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}").SetIsReadOnly();
            grdWIP.View.AddTextBoxColumn("CUSTOMERNAME", 100).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdWIP.View.AddTextBoxColumn("INPUTDATE", 100).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdWIP.View.AddTextBoxColumn("PRODUCTIONORDERID", 120).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdWIP.View.AddTextBoxColumn("DUEDATE", 120).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdWIP.View.AddTextBoxColumn("PRODUCTIONTYPE", 80).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();

            grdWIP.View.PopulateColumns();

            grdWIP.Height = 350;
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
            this.Load += Form_Load;

            // TextBox Event
            txtLotId.Editor.KeyDown += TxtLotId_KeyDown;
            txtLotId.Editor.Click += Editor_Click;
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

            Search();
        }
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

            // TODO : 저장 Rule 변경
            DataTable lotList = grdWIP.View.GetCheckedRows();

            if (lotList == null || lotList.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }

            MessageWorker worker = new MessageWorker("SaveMultiLotAccept");
            worker.SetBody(new MessageBody()
            {
                { "EnterpriseID", UserInfo.Current.Enterprise },
                { "PlantID", UserInfo.Current.Plant },
                { "UserId", UserInfo.Current.Id },
                { "Lotlist", lotList }
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
        }

        #endregion

        #region ◆ 유효성 검사 |

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            grdWIP.View.CheckValidation();

            if(grdWIP.View.GetCheckedRows().Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
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
            grdWIP.View.ClearDatas();
            grdLotInfo.ClearData();
            grdComment.View.ClearDatas();
            grdProcessSpec.View.ClearDatas();

            this.ucMessage.ClearDatas();

            this.tabMain.Enabled = false;
        }
        #endregion

        #region ▶ 데이터 조회 |
        /// <summary>
        /// 데이터 조회
        /// </summary>
        private void Search()
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
            param2.Add("PROCESSSTATE", Constants.WaitForReceive);

            DataTable dt = SqlExecuter.Query("SelectWIPMultiStateList", "10001", param2);

            if (dt.Rows.Count < 1)
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                return;
            }

            grdWIP.DataSource = dt;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            param.Add("PLANTID", UserInfo.Current.Plant);
            param.Add("AREAID", txtArea.Editor.SelectedData.FirstOrDefault()["AREAID"].ToString());
            param.Add("LOTID", strLotId);
            param.Add("PROCESSSTATE", Constants.WaitForReceive);
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable lotInfo = SqlExecuter.Query("SelectLotInfoByProcess", "40001", param);

            if (lotInfo.Rows.Count < 1)
            {
                ClearDetailInfo();

                txtLotId.Text = "";
                txtLotId.Focus();

                // 조회할 데이터가 없습니다.
                DataTable AreaLot = SqlExecuter.Query("GetLotAreaAndState", "10001", param);
                ShowMessage("NotAreaOrState", Format.GetString(AreaLot.Rows[0]["AREANAME"]), Format.GetString(AreaLot.Rows[0]["PROCESSTATE"]));
            }

            grdLotInfo.DataSource = lotInfo;

            // Lot Message Setting
            this.ucMessage.SetDatasource(strLotId, grdLotInfo.GetFieldValue("PRODUCTDEFID").ToString()
                , grdLotInfo.GetFieldValue("PRODUCTDEFVERSION").ToString()
                , grdLotInfo.GetFieldValue("PROCESSSEGMENTID").ToString());

            // 특기사항
            Dictionary<string, object> commentParam = new Dictionary<string, object>();
            commentParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            commentParam.Add("PLANTID", UserInfo.Current.Plant);
            commentParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            commentParam.Add("LOTID", strLotId);
            commentParam.Add("PROCESSSEGMENTID", grdLotInfo.GetFieldValue("PROCESSSEGMENTID").ToString());

            this.grdComment.DataSource = SqlExecuter.Query("SelectCommentByProcess", "10001", commentParam);

            // 공정 SPEC
            Dictionary<string, object> processSpecParam = new Dictionary<string, object>();
            processSpecParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            processSpecParam.Add("PLANTID", UserInfo.Current.Plant);
            processSpecParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            processSpecParam.Add("LOTID", strLotId);
            processSpecParam.Add("PROCESSSEGMENTID", grdLotInfo.GetFieldValue("PROCESSSEGMENTID").ToString());
            processSpecParam.Add("CONTROLTYPE", "XBARR");
            processSpecParam.Add("SPECCLASSID", "OperationSpec");

            grdProcessSpec.DataSource = SqlExecuter.Query("SelectProcessSpecByProcess", "10001", processSpecParam);

            this.tabMain.Enabled = true;
            this.grdLotInfo.Enabled = true;
        } 
        #endregion

        #endregion
    }
}
