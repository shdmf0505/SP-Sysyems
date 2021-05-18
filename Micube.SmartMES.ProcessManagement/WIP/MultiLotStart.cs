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
    /// 프 로 그 램 명  : 공정 다중작업 > 일괄작업시작
    /// 업  무  설  명  : 일괄작업시작
    /// 생    성    자  : 박정훈
    /// 생    성    일  : 2019-11-01
    /// 수  정  이  력  : 2019-11-26, 박정훈, 화면구성을 최종검사와 같이 수정
    /// 
    /// 
    /// </summary>
    public partial class MultiLotStart : SmartConditionManualBaseForm 
    {
        #region ◆ Local Variables |

        private decimal _panelPerQty = 0;
        private decimal _panelQty = 0;
        private decimal _qty = 0;

        #endregion

        #region ◆ 생성자 |
        /// <summary>
        /// 생성자
        /// </summary>
        public MultiLotStart()
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

            //if(UserInfo.Current.Enterprise.Equals("YOUNGPOONG"))
            //{
            //    tabMain.TabPages[4].PageVisible = false;
            //}
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
            #region - 작업시작 Grid 설정 |
            grdLotList.GridButtonItem = GridButtonItem.Export;

            grdLotList.View.SetIsReadOnly();

            // CheckBox 설정
            this.grdLotList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdLotList.View.AddTextBoxColumn("LOTID", 180).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdLotList.View.AddTextBoxColumn("PRODUCTDEFID", 150).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdLotList.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdLotList.View.AddTextBoxColumn("PRODUCTDEFNAME", 250).SetIsReadOnly();
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
            grdLotList.View.AddTextBoxColumn("PANELPERQTY", 80).SetIsHidden();

            grdLotList.View.PopulateColumns();

            grdLotList.Height = 350;
            #endregion

            #region - 작업시작 설비 |
            grdEquipment.GridButtonItem = GridButtonItem.None;
            grdEquipment.ShowButtonBar = false;
            grdEquipment.ShowStatusBar = false;

            grdEquipment.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdEquipment.View.SetIsReadOnly();

            // 설비코드
            grdEquipment.View.AddTextBoxColumn("EQUIPMENTID", 150);
            // 설비명
            grdEquipment.View.AddTextBoxColumn("EQUIPMENTNAME", 200);

            grdEquipment.View.PopulateColumns();
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

            #region - Defect Grid |
            grdDefect.VisibleLotId = true;
            grdDefect.InitializeControls();
            grdDefect.InitializeEvent();
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

            grdLotList.View.DoubleClick += LotList_DoubleClick;

            grdDefect.DefectQtyChanged += GrdDefect_DefectQtyChanged;
            grdDefect.View.AddingNewRow += Defect_AddingNewRow;
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

            ClearDetailInfo();

            if (txtArea.Editor.SelectedData.Count() < 1 || string.IsNullOrWhiteSpace(txtArea.EditValue.ToString()) || string.IsNullOrWhiteSpace(txtLotId.Text))
            {
                // 작업장, LOT No.는 필수 입력 항목입니다.
                ShowMessage("AreaLotIdIsRequired");
                ClearDetailInfo();
                return;
            }

            //2020-03-02 강유라 작업장이나 resourceId  없을 때 팝업
            string lotId = Format.GetString(txtLotId.Text).Trim();
            Dictionary<string, object> param = new Dictionary<string, object>()
            {
                {"LOTID",lotId}
            };

            DataTable dt = SqlExecuter.Query("selectAreaResourceByLot", "10001", param);

            if (dt.Rows.Count < 1)
            {
                DataTable AreaLot = SqlExecuter.Query("GetLotAreaAndState", "10001", param);
                ShowMessage("NotAreaOrState", Format.GetString(AreaLot.Rows[0]["AREANAME"]), Format.GetString(AreaLot.Rows[0]["PROCESSTATE"]));
                return;
            }
            //작업장이나, 자원이 없을 경우
            if (string.IsNullOrWhiteSpace(Format.GetFullTrimString(dt.Rows[0]["AREAID"])) || string.IsNullOrWhiteSpace(Format.GetFullTrimString(dt.Rows[0]["RESOURCEID"])))
            {
                SelectResourcePopup resourcePopup = new SelectResourcePopup(Format.GetString(dt.Rows[0]["LOTID"]), Format.GetString(dt.Rows[0]["PROCESSSEGMENTID"]), string.Empty);
                resourcePopup.ShowDialog();

                if (resourcePopup.DialogResult == DialogResult.OK)
                {
                    MessageWorker resourceWorker = new MessageWorker("SaveLotResourceId");
                    resourceWorker.SetBody(new MessageBody()
                            {
                                { "LotId", lotId },
                                { "ResourceId", resourcePopup.ResourceId }
                            });

                    resourceWorker.Execute();

                    TxtLotId_KeyDown(sender, e);
                }
                else
                {
                    // 현재 공정에서 사용할 자원을 선택하시기 바랍니다.
                    throw MessageException.Create("SelectResourceForCurrentProcess");
                }
            }

            // 대상 LOT 조회 :: RootLotID 기준
            SearchData();

            DialogResult result = WipFunction.ShowWindowTimeArrivedPopup(Format.GetFullTrimString(dt.Rows[0]["AREAID"]));    // Window Time 이 도래한 Lot 목록 표시
            if(result == DialogResult.OK)
            {
                OpenWindowTimeLot();
            }
        }
        #endregion

        #region ▶ Grid Event |

        #region - Lot 목록 더블 클릭 |
        /// <summary>
        /// Lot 목록 더블 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LotList_DoubleClick(object sender, EventArgs e)
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

        #region - Defect Grid Add New Row Event |
        /// <summary>
        /// Defect Grid Add New Row Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void Defect_AddingNewRow(SmartBandedGridView sender, AddNewRowArgs args)
        {
            DataRow dr = grdLotList.View.GetFocusedDataRow();

            if (dr == null) return;

            string lotid = dr["LOTID"].ToString();

            string ProcessUOM = Format.GetTrimString(dr["PROCESSUOM"]);
            decimal BlkQty = Format.GetDecimal(dr["PCSARY"]);
            decimal pcsPnl = Format.GetDecimal(dr["PCSPNL"].ToString());

            if (BlkQty.Equals(0))
            {
                MSGBox.Show(MessageBoxType.Information, Language.GetMessage("NotInputPNLBKL").Message);
                args.IsCancel = true;
                return;
            }

            if (pcsPnl.Equals(0))
            {
                MSGBox.Show(MessageBoxType.Information, Language.GetMessage("NotInputPCSPNL").Message);
                args.IsCancel = true;
                return;
            }

            decimal CalQty = ProcessUOM == "BLK" ? pcsPnl/BlkQty : _panelPerQty;

            grdDefect.SetInfo(CalQty, Format.GetInteger(dr["QTY"].ToString()));
            grdDefect.View.SetFocusedRowCellValue("LOTID", lotid);

            //grdDefect.SetConsumableDefComboBox();
        }
        #endregion

        #region - Defect 수량 Changed Event |
        /// <summary>
        /// Defect 수량 Changed Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdDefect_DefectQtyChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            CheckDefectQty(e.RowHandle);
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

            // Lot 정보
            DataTable lotList = grdLotList.View.GetCheckedRows();

            if (lotList == null || lotList.Rows.Count == 0)
            {
                throw MessageException.Create("NoSeletedLot");
            }

            // Defect 정보
            grdDefect.CheckDefect();

            DataTable defectList = grdDefect.DataSource as DataTable;

            DataTable dfdt = defectList.Clone();

            // 체크된 Lot 기준으로 Defect Code List를 Join하여 데이터를 한번 걸러냄
            var dfList = from lotRows in lotList.AsEnumerable()
                         join dfRows in defectList.AsEnumerable() on lotRows.Field<string>("LOTID") equals dfRows.Field<string>("LOTID")
                         select new
                         {
                             LOTID = lotRows.Field<string>("LOTID"),
                             DEFECTCODE = dfRows.Field<string>("DEFECTCODE"),
                             QCSEGMENTID = dfRows.Field<string>("QCSEGMENTID"),
                             PNLQTY = dfRows.Field<decimal>("PNLQTY"),
                             QTY = dfRows.Field<decimal>("QTY"),
                             REASONCONSUMABLEDEFID = dfRows.Field<string>("REASONCONSUMABLEDEFID"),
                             REASONCONSUMABLELOTID = dfRows.Field<string>("REASONCONSUMABLELOTID"),
                             REASONPROCESSSEGMENTID = dfRows.Field<string>("REASONPROCESSSEGMENTID"),
                             REASONAREAID = dfRows.Field<string>("REASONAREAID")
                         };

            foreach (var df in dfList)
            {
                DataRow dr = dfdt.NewRow();
                dr["LOTID"] = df.LOTID;
                dr["DEFECTCODE"] = df.DEFECTCODE;
                dr["QCSEGMENTID"] = df.QCSEGMENTID;
                dr["PNLQTY"] = df.PNLQTY;
                dr["QTY"] = df.QTY;
                dr["REASONCONSUMABLEDEFID"] = df.REASONCONSUMABLEDEFID;
                dr["REASONCONSUMABLELOTID"] = df.REASONCONSUMABLELOTID;
                dr["REASONPROCESSSEGMENTID"] = df.REASONPROCESSSEGMENTID;
                dr["REASONAREAID"] = df.REASONAREAID;
                dfdt.Rows.Add(dr);
            }

            // Lot별 Defect Sum 정보
            DataTable defectLot = new DataTable();
            defectLot.Columns.Add("LOTID", typeof(String));
            defectLot.Columns.Add("DEFECTQTY", typeof(Double));

            // 불량정보가 있을 경우 LOT별로 Group By하여 Defect수량 및 Panel수량 산출
            if (dfdt != null && dfdt.Rows.Count > 0)
            {
                var dqry = from dr in dfdt.AsEnumerable()
                           group dr by dr["LOTID"] into dg
                           select new
                           {
                               lotid = dg.Key,
                               defectqty = dg.Sum(r => r.Field<decimal>("QTY"))
                           };

                foreach (var x in dqry)
                {
                    DataRow dr = defectLot.NewRow();
                    dr["LOTID"] = x.lotid;
                    dr["DEFECTQTY"] = x.defectqty;

                    defectLot.Rows.Add(dr);
                }
            }

            // Lot 수량과 Defect SUM Qty 비교
            var cp = from lot in lotList.AsEnumerable()
                     join defect in defectLot.AsEnumerable() on lot.Field<string>("LOTID") equals defect.Field<string>("LOTID")
                     where lot.Field<decimal>("QTY") < Convert.ToDecimal(defect.Field<double>("DEFECTQTY"))
                     select defect
                     ;

            if (cp != null && cp.Count() > 0)
            {
                // 불량 수량은 Lot 수량보다 많을 수 없습니다. {0}
                throw MessageException.Create("LotQtyLargerThanDefectQty");
            }

            DataTable equipList = grdEquipment.View.GetCheckedRows();

            string equipmentId = string.Join(",", equipList.Rows.Cast<DataRow>().Select(row => Format.GetString(row["EQUIPMENTID"])));

            if (string.IsNullOrWhiteSpace(equipmentId))
            {
                // 설비는 필수로 선택해야 합니다. {0}
                throw MessageException.Create("EquipmentIsRequired");
            }

            MessageWorker worker = new MessageWorker("SaveMultiLotStart");
            worker.SetBody(new MessageBody()
            {
                { "EnterpriseID", UserInfo.Current.Enterprise },
                { "PlantID", UserInfo.Current.Plant },
                { "UserId", UserInfo.Current.Id },
                { "Lotlist", lotList },
                { "Equipmentlist", equipmentId },
                { "DefectLot", defectLot },
                { "DefectList", dfdt }
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
            grdEquipment.View.ClearDatas();
            grdComment.View.ClearDatas();
            grdProcessSpec.View.ClearDatas();
            grdDefect.View.ClearDatas();

            this.ucMessage.ClearDatas();

            this.tabMain.Enabled = false;
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
            param2.Add("PROCESSSTATE", Constants.Wait);

            DataTable dt = SqlExecuter.Query("SelectWIPMultiStateList", "10001", param2);

            if (dt.Rows.Count < 1)
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                return;
            }

            grdLotList.DataSource = dt;

            getLotInfo(strLotId);

            tabMain.Enabled = true;

            string lotId = string.Join(",", (grdLotList.DataSource as DataTable).AsEnumerable().Select(c => Format.GetString(c["LOTID"])).Distinct());

            grdDefect.SetConsumableDefComboBox(lotId);
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
            param.Add("PROCESSSTATE", Constants.Wait);
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable lotInfo = SqlExecuter.Query("SelectLotInfoByProcess", "40001", param);

            if (lotInfo.Rows.Count < 1)
            {
                ClearDetailInfo();

                txtLotId.Text = "";
                txtLotId.Focus();

                // 조회할 데이터가 없습니다.
                throw MessageException.Create("NoSelectData");
            }

            grdLotInfo.DataSource = lotInfo;

            grdLotInfo.Enabled = true;

            if (grdLotList.DataSource == null)
            {
                InitializeGrid();
            }

            _panelPerQty = decimal.Parse(lotInfo.Rows[0]["PANELPERQTY"].ToString());
            _panelQty = decimal.Parse(lotInfo.Rows[0]["PNLQTY"].ToString());
            _qty = decimal.Parse(lotInfo.Rows[0]["PCSQTY"].ToString());

            // 설비 데이터 조회
            Dictionary<string, object> equipmentParam = new Dictionary<string, object>();
            equipmentParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            equipmentParam.Add("PLANTID", UserInfo.Current.Plant);
            equipmentParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            equipmentParam.Add("LOTID", strLotId);
            equipmentParam.Add("EQUIPMENTTYPE", "Production");
            equipmentParam.Add("DETAILEQUIPMENTTYPE", "Main");

            DataTable equipmentList = SqlExecuter.Query("GetEquipmentByArea", "10031", equipmentParam);

            grdEquipment.DataSource = equipmentList;

            // 데이터가 하나면 자동 체크
            if(equipmentList != null && equipmentList.Rows.Count == 1)
            {
                grdEquipment.View.CheckedAll(true);
            }

            // Lot Message Setting
            this.ucMessage.SetDatasource(CommonFunction.changeArgString(strLotId), lotInfo.Rows[0]["PRODUCTDEFID"].ToString()
                , lotInfo.Rows[0]["PRODUCTDEFVERSION"].ToString()
                , lotInfo.Rows[0]["PROCESSSEGMENTID"].ToString());

            // 특기사항
            Dictionary<string, object> commentParam = new Dictionary<string, object>();
            commentParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            commentParam.Add("PLANTID", UserInfo.Current.Plant);
            commentParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            commentParam.Add("LOTID", CommonFunction.changeArgString(strLotId));
            commentParam.Add("PROCESSSEGMENTID", lotInfo.Rows[0]["PROCESSSEGMENTID"].ToString());

            this.grdComment.DataSource = SqlExecuter.Query("SelectCommentByProcess", "10001", commentParam);

            // 공정 SPEC
            Dictionary<string, object> processSpecParam = new Dictionary<string, object>();
            processSpecParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            processSpecParam.Add("PLANTID", UserInfo.Current.Plant);
            processSpecParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            processSpecParam.Add("LOTID", CommonFunction.changeArgString(strLotId));
            processSpecParam.Add("PROCESSSEGMENTID", lotInfo.Rows[0]["PROCESSSEGMENTID"].ToString());
            processSpecParam.Add("CONTROLTYPE", "XBARR");
            processSpecParam.Add("SPECCLASSID", "OperationSpec");

            grdProcessSpec.DataSource = SqlExecuter.Query("SelectProcessSpecByProcess", "10001", processSpecParam);

            //2021.03.05 uom에따른 불량 수량 변경
            string ProcessUOM = Format.GetTrimString(lotInfo.Rows[0]["PROCESSUOM"]);
            decimal BlkQty = Format.GetDecimal(lotInfo.Rows[0]["PCSARY"]);
            decimal pcsPnl = Format.GetDecimal(lotInfo.Rows[0]["PCSPNL"].ToString());

            if (BlkQty.Equals(0))
            {
                throw MessageException.Create("NotInputPNLBKL");
            }

            if (pcsPnl.Equals(0))
            {
                throw MessageException.Create("NotInputPCSPNL");
            }

            decimal CalQty = ProcessUOM == "BLK" ? pcsPnl/BlkQty : _panelPerQty;

            setDefectInputType(ProcessUOM);
            grdDefect.SetInfo(CalQty, _qty);
        }
        #endregion

        #region ▶ Lot 수량과 Defect 수량 비교 |
        /// <summary>
        /// Lot 수량과 Defect 수량 비교
        /// </summary>
        private void CheckDefectQty(int rowhandle)
        {
            DataTable lotList = grdLotList.DataSource as DataTable;
            DataTable defectList = grdDefect.DataSource as DataTable;

            var lt = lotList.AsEnumerable().ToList();

            var defect = from dr in defectList.AsEnumerable()
                         group dr by dr["LOTID"] into dg
                         select new
                         {
                             LOTID = dg.Key,
                             DEFECTQTY = dg.Sum(r => r.Field<decimal?>("QTY"))
                         };

            // Lot 수량과 Defect SUM Qty 비교
            var cp = from lot in lt.AsEnumerable()
                     join df in defect.AsEnumerable() on lot.Field<string>("LOTID") equals df.LOTID
                     where lot.Field<decimal>("QTY") < df.DEFECTQTY
                     select df
                     ;

            if (cp != null && cp.Count() > 0)
            {
                grdDefect.View.SetRowCellValue(rowhandle, "QTY", null);
                grdDefect.View.SetRowCellValue(rowhandle, "PNLQTY", null);
                grdDefect.View.SetRowCellValue(rowhandle, "DEFECTRATE", null);

                // 불량 수량은 Lot 수량보다 많을 수 없습니다. {0}
                ShowMessage("LotQtyLargerThanDefectQty");
            }

            var dl = defect.ToList();

            for (int i = 0; i < dl.Count(); i++)
            {
                grdLotList.View.SetRowCellValue(grdLotList.View.LocateByValue("LOTID", dl[i].LOTID), "DEFECTQTY", Format.GetInteger(dl[i].DEFECTQTY));
            }
        }
        #endregion

        private void setDefectInputType(string processUOM)
        {

            Color copyColor = grdDefect.View.Columns["DECISIONDEGREENAME"].AppearanceHeader.ForeColor;

            if (!processUOM.Equals("PCS"))
            {
                grdDefect.PcsSetting = true;
                grdDefect.View.GetConditions().GetCondition("PNLQTY").IsRequired = true;
                grdDefect.View.GetConditions().GetCondition("QTY").IsRequired = false;
                grdDefect.View.Columns["PNLQTY"].AppearanceHeader.ForeColor = Color.Red;
                grdDefect.View.Columns["QTY"].AppearanceHeader.ForeColor = copyColor;
            }
            else
            {
                grdDefect.PcsSetting = false;
                grdDefect.View.GetConditions().GetCondition("PNLQTY").IsRequired = false;
                grdDefect.View.GetConditions().GetCondition("QTY").IsRequired = true;
                grdDefect.View.Columns["QTY"].AppearanceHeader.ForeColor = Color.Red;
                grdDefect.View.Columns["PNLQTY"].AppearanceHeader.ForeColor = copyColor;
            }

            string ColCaption = processUOM == "BLK" ? Language.Get("QTY") + "(" + "BLK" + ")" : Language.Get("QTY").ToString() + "(" + "PNL" + ")";
            grdDefect.View.Columns["PNLQTY"].Caption = ColCaption;
            grdDefect.View.Columns["PNLQTY"].ToolTip = ColCaption;

            grdDefect.DefectUOM = processUOM;
            grdDefect.setDefectTextBox(processUOM);
        }
        private void OpenWindowTimeLot()
        {
            try
            {
                DialogManager.ShowWaitDialog();
                string menuId = "PG-SG-0480";   // Window Time Lot 조회
                var param = new Dictionary<string, object>();
                OpenMenu(menuId, param); //다른창 호출..
            }
            catch (Exception ex)
            {
                this.ShowError(ex);
            }
            finally
            {
                DialogManager.Close();
            }
        }
        #endregion
    }
}
