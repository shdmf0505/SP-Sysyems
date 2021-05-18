#region using
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
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
#endregion

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 공정관리 > 공정작업 > 인계등록(Roll Cutting)
    /// 업  무  설  명  : 
    /// 생    성    자  : 배선용
    /// 생    성    일  : 2019-08-23
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class SendLotRollCut : SmartConditionManualBaseForm
    {
        #region ◆ Local Variables |

        #endregion

        #region ◆ 생성자 |
        /// <summary>
        /// 생성자
        /// </summary>
        public SendLotRollCut()
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

            UseAutoWaitArea = false;

            this.ucDataUpDownBtnCtrl.SourceGrid = this.grdWIP;
            this.ucDataUpDownBtnCtrl.TargetGrid = this.grdLotList;

            InitializeEvent();
            InitializeInspectionUser();

            txtWorker.SetValue(UserInfo.Current.Id);
            txtWorker.Text = UserInfo.Current.Name;

            txtWorkerID.Text = UserInfo.Current.Id + " : " + UserInfo.Current.Name;

            cboArea.LanguageKey = "SENDRESOURCEID";
        }
        private void InitializeInspectionUser()
        {

            //GetUserList 10001
            ConditionItemSelectPopup options = new ConditionItemSelectPopup();
            options.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            options.SetPopupLayout("USERLIST", PopupButtonStyles.Ok_Cancel, true, false);
            options.Id = "USER";
            options.SearchQuery = new SqlQuery("GetUserList", "10001", $"PLANTID={UserInfo.Current.Plant}");
            options.IsMultiGrid = false;
            options.DisplayFieldName = "USERNAME";
            options.ValueFieldName = "USERID";
            options.LanguageKey = "USER";

            options.Conditions.AddTextBox("USERIDNAME");

            options.GridColumns.AddTextBoxColumn("USERID", 150);
            options.GridColumns.AddTextBoxColumn("USERNAME", 200);

            txtWorker.SelectPopupCondition = options;

        }
        #region ▶ 조회조건 설정 |
        /// <summary>
        /// 검색조건 설정
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // Lot
            CommonFunction.AddConditionAreaByAuthorityPopup("P_AREAID", 0.1, false, Conditions, true, true);
            // 품목
            CommonFunction.AddConditionProductPopup("P_PRODUCTDEFID", 0.5, true, Conditions);

            CommonFunction.AddConditionLotPopup("P_LOTID", 0.6, true, Conditions);

            if (UserInfo.Current.Enterprise.Equals(Constants.EnterPrise_InterFlex))
            {
                chkMerge.CheckState = CheckState.Unchecked;
                chkMerge.Visible = false;
            }
            else
            {
                chkMerge.CheckState = CheckState.Checked;
                chkMerge.Visible = true;
                chkMerge.ReadOnly = false;
            }

        }

        /// <summary>
        /// 조회조건 설정
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();
            chkPrintLotcard.Checked = true;
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
            grdWIP.GridButtonItem = GridButtonItem.Export;

            grdWIP.View.SetIsReadOnly();

            // CheckBox 설정
            this.grdWIP.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdWIP.View.AddTextBoxColumn("SALESORDERID", 100).SetTextAlignment(TextAlignment.Left);
            grdWIP.View.AddTextBoxColumn("LINENO", 70).SetTextAlignment(TextAlignment.Center);
            grdWIP.View.AddTextBoxColumn("PRODUCTDEFID", 100).SetTextAlignment(TextAlignment.Left);
            grdWIP.View.AddTextBoxColumn("PRODUCTDEFNAME", 150).SetTextAlignment(TextAlignment.Left);
            grdWIP.View.AddTextBoxColumn("ITEMVERSION", 70).SetTextAlignment(TextAlignment.Center);
            grdWIP.View.AddSpinEditColumn("PLANQTY", 70).SetTextAlignment(TextAlignment.Right);
            grdWIP.View.AddTextBoxColumn("PROCESSSEGMENTID", 150).SetTextAlignment(TextAlignment.Left).SetIsHidden();
            grdWIP.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150).SetTextAlignment(TextAlignment.Left);
            grdWIP.View.AddTextBoxColumn("USERSEQUENCE", 70).SetTextAlignment(TextAlignment.Center);
            grdWIP.View.AddTextBoxColumn("AREANAME", 120).SetTextAlignment(TextAlignment.Left);
            grdWIP.View.AddTextBoxColumn("AREAID", 120).SetTextAlignment(TextAlignment.Left).SetIsHidden();
            grdWIP.View.AddTextBoxColumn("LOTID", 120).SetTextAlignment(TextAlignment.Left);
            grdWIP.View.AddSpinEditColumn("LOTSIZE", 120).SetTextAlignment(TextAlignment.Right).SetIsHidden();
            grdWIP.View.AddTextBoxColumn("STARTEDDATE", 100).SetTextAlignment(TextAlignment.Center);
            grdWIP.View.AddSpinEditColumn("INPUTQTY", 70).SetTextAlignment(TextAlignment.Right);
            grdWIP.View.AddSpinEditColumn("PCS", 70).SetTextAlignment(TextAlignment.Right);
            grdWIP.View.AddSpinEditColumn("PNL", 70).SetTextAlignment(TextAlignment.Right);
            grdWIP.View.AddSpinEditColumn("MM", 70).SetTextAlignment(TextAlignment.Right);
            grdWIP.View.AddTextBoxColumn("INPUTSEQUENCE", 150).SetTextAlignment(TextAlignment.Center).SetIsHidden();
            grdWIP.View.AddTextBoxColumn("PANELPERQTY", 150).SetTextAlignment(TextAlignment.Center).SetIsHidden();
            grdWIP.View.AddTextBoxColumn("SEQ", 5).SetIsHidden();
            grdWIP.View.AddTextBoxColumn("AFTERPROCESSSEGMENTID", 5).SetIsHidden();
            grdWIP.View.AddTextBoxColumn("AFTERPROCESSSEGMENTVERSION", 5).SetIsHidden();
            grdWIP.View.AddTextBoxColumn("DESCRIPTION", 5).SetIsHidden();
            grdWIP.View.AddTextBoxColumn("MATERIALCLASS", 5).SetIsHidden();
            grdWIP.View.AddTextBoxColumn("MATERIALSEQUENCE", 5).SetIsHidden();

            grdWIP.View.PopulateColumns();

            #endregion

            #region - Locking Grid |
            grdLotList.GridButtonItem = GridButtonItem.None;

            this.grdLotList.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;
            grdLotList.View.SetIsReadOnly();
            grdLotList.View.EnableRowStateStyle = false;
            this.grdLotList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdLotList.View.AddTextBoxColumn("SALESORDERID", 100).SetTextAlignment(TextAlignment.Left).SetIsHidden();
            grdLotList.View.AddTextBoxColumn("LINENO", 70).SetTextAlignment(TextAlignment.Center).SetIsHidden();
            grdLotList.View.AddTextBoxColumn("PRODUCTDEFID", 100).SetTextAlignment(TextAlignment.Left);
            grdLotList.View.AddTextBoxColumn("PRODUCTDEFNAME", 150).SetTextAlignment(TextAlignment.Left);
            grdLotList.View.AddTextBoxColumn("ITEMVERSION", 70).SetTextAlignment(TextAlignment.Center);
            grdLotList.View.AddSpinEditColumn("PLANQTY", 70).SetTextAlignment(TextAlignment.Right).SetIsHidden(); ;
            grdLotList.View.AddTextBoxColumn("PROCESSSEGMENTID", 150).SetTextAlignment(TextAlignment.Left).SetIsHidden();
            grdLotList.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150).SetTextAlignment(TextAlignment.Left);
            grdLotList.View.AddTextBoxColumn("USERSEQUENCE", 70).SetTextAlignment(TextAlignment.Center).SetIsHidden();
            grdLotList.View.AddTextBoxColumn("AREANAME", 120).SetTextAlignment(TextAlignment.Left);
            grdLotList.View.AddTextBoxColumn("AREAID", 120).SetTextAlignment(TextAlignment.Left).SetIsHidden();
            grdLotList.View.AddTextBoxColumn("LOTID", 120).SetTextAlignment(TextAlignment.Left);
            grdLotList.View.AddSpinEditColumn("LOTSIZE", 120).SetTextAlignment(TextAlignment.Right).SetIsHidden();
            grdLotList.View.AddTextBoxColumn("STARTEDDATE", 100).SetTextAlignment(TextAlignment.Center);
            grdLotList.View.AddSpinEditColumn("INPUTQTY", 70).SetTextAlignment(TextAlignment.Right);
            grdLotList.View.AddSpinEditColumn("PCS", 70).SetTextAlignment(TextAlignment.Right);
            grdLotList.View.AddSpinEditColumn("PNL", 70).SetTextAlignment(TextAlignment.Right);
            grdLotList.View.AddSpinEditColumn("MM", 70).SetTextAlignment(TextAlignment.Right);
            grdLotList.View.AddTextBoxColumn("INPUTSEQUENCE", 150).SetTextAlignment(TextAlignment.Center).SetIsHidden();
            grdLotList.View.AddTextBoxColumn("PANELPERQTY", 150).SetTextAlignment(TextAlignment.Center).SetIsHidden();
            grdLotList.View.AddTextBoxColumn("SEQ", 5).SetIsHidden();
            grdLotList.View.AddTextBoxColumn("AFTERPROCESSSEGMENTID", 5).SetIsHidden();
            grdLotList.View.AddTextBoxColumn("AFTERPROCESSSEGMENTVERSION", 5).SetIsHidden();
            grdLotList.View.AddTextBoxColumn("DESCRIPTION", 5).SetIsHidden();
            grdLotList.View.AddTextBoxColumn("MATERIALCLASS", 5).SetIsHidden();
            grdLotList.View.AddTextBoxColumn("MATERIALSEQUENCE", 5).SetIsHidden();

            grdLotList.View.PopulateColumns();
            // CheckBox 설정
            //this.grdLotList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            // 모랏, lotid, 수량(pnl), 수량(pcs),ISMERGE(히든),MERGESUMQTY(히든)
            grdCreateLot.View.SetIsReadOnly();
            grdCreateLot.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdCreateLot.View.AddTextBoxColumn("PARENTLOT", 150).SetTextAlignment(TextAlignment.Center);
            grdCreateLot.View.AddTextBoxColumn("LOTID", 150).SetTextAlignment(TextAlignment.Center);
            grdCreateLot.View.AddTextBoxColumn("PANELQTY", 70).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            grdCreateLot.View.AddTextBoxColumn("QTY", 70).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            grdCreateLot.View.AddTextBoxColumn("ISMERGE", 50).SetTextAlignment(TextAlignment.Center).SetIsHidden();
            grdCreateLot.View.AddSpinEditColumn("MERGESUMQTY", 50).SetTextAlignment(TextAlignment.Right).SetIsHidden();
            grdCreateLot.View.AddSpinEditColumn("MERGELOT", 50).SetTextAlignment(TextAlignment.Right).SetIsHidden();

            grdCreateLot.View.PopulateColumns();
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
            grdWIP.View.RowStyle += View_RowStyle;
            grdWIP.View.DoubleClick += View_DoubleClick;
            ucDataUpDownBtnCtrl.buttonClick += UcDataUpDownBtnCtrl_buttonClick;
            btnUp.Click += BtnUp_Click;
            btnDown.Click += BtnDown_Click;
            btnCreate.Click += BtnCreate_Click;
            grdCreateLot.View.RowStyle += View_RowStyle1;
            grdWIP.View.CheckStateChanged += View_CheckStateChanged;
            btnPrint.Click += BtnPrint_Click;
            chkMerge.Click += ChkMerge_Click;
        }

        private void ChkMerge_Click(object sender, EventArgs e)
        {


            DataTable dt2 = grdLotList.DataSource as DataTable;

            if (dt2.Rows.Count > 0)
            {

                foreach (DataRow dr in dt2.Rows)
                {

                    Dictionary<string, object> param = new Dictionary<string, object>();
                    param.Add("LOTID", dr["LOTID"]);

                    DataTable isSplitLotCheck = SqlExecuter.Query("GetSplitCheck", "10001", param);

                    if (isSplitLotCheck.Rows.Count > 0 && !chkMerge.Checked)
                    {
                       
                        throw MessageException.Create("NotSplitLotMerge");
                    }

                }
            }


        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            PrintLotCard();
        }

        private void View_CheckStateChanged(object sender, EventArgs e)
        {

            //YOUNGPOONG
            //INTERFLEX
            int rowHandle = grdWIP.View.FocusedRowHandle;

            DataTable dt = grdWIP.View.GetCheckedRows();

            if (UserInfo.Current.Enterprise.Equals(Constants.EnterPrise_InterFlex))
            {
                if (dt.Rows.Count > 1)
                {
                    if (grdWIP.View.IsRowChecked(rowHandle))
                    {
                        ShowMessage("CheckRollCutSplit", "1");
                        grdWIP.View.CheckRow(rowHandle, false);
                    }
                }
            }
            /*
            else
            {
                if (dt.Rows.Count > 2)
                {
                    if (grdWIP.View.IsRowChecked(rowHandle))
                    {
                        ShowMessage("CheckRollCutSplit", "2");
                        grdWIP.View.CheckRow(rowHandle, false);
                    }
                }
            }
            */


        }

        private void View_RowStyle1(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle > -1)
            {
                DataRow dr = grdCreateLot.View.GetDataRow(e.RowHandle);
                if (Format.GetFullTrimString(dr["ISMERGE"]).Equals("Y"))
                {
                    e.Appearance.BackColor = Color.Yellow;
                    e.HighPriority = true;
                }
            }
        }

        private void BtnCreate_Click(object sender, EventArgs e)
        {

            if (Format.GetTrimString(txtLotSize.Text).Equals(string.Empty))
            {
                ShowMessage("Lot Size가 입력이 되어야 합니다.");
                return;
            }

            DataTable baseLots = grdLotList.DataSource as DataTable;

            if (chkMerge.Checked && baseLots.Rows.Count > 2)
            {
                // Lot 두개 이상을 병합 할 수 없습니다.
                throw MessageException.Create("MergeMoreThanTwoNotAllowed");
            }

            DataTable dt = grdLotList.DataSource as DataTable;

            GenerateLot(dt);
        }

        private void BtnDown_Click(object sender, EventArgs e)
        {
            int rowhandle = grdLotList.View.FocusedRowHandle;

            if (rowhandle < grdLotList.View.RowCount)
            {
                DataTable dt = grdLotList.DataSource as DataTable;

                // double seq = Format.GetDouble(dt.Rows[rowhandle + 1]["SEQ"], 0);

                DataRow dr = grdLotList.View.GetFocusedDataRow();

                DataRow newDr = dt.NewRow();
                newDr.ItemArray = dr.ItemArray;

                int newRowIdx = rowhandle + 1;

                if (newRowIdx < grdLotList.View.RowCount)
                {
                    dt.Rows.Remove(dr);
                    dt.Rows.InsertAt(newDr, newRowIdx);
                }

                grdLotList.View.RefreshData();

                //dt.Rows[rowhandle]["SEQ"] = seq - 0.001;
                //dt.AcceptChanges();
                //grdLotList.View.SetSortOrder("SEQ", DevExpress.Data.ColumnSortOrder.Ascending);
                //dt.DefaultView.Sort = "SEQ ASC";
                //dt.DefaultView.ToTable(); 
            }
        }

        private void BtnUp_Click(object sender, EventArgs e)
        {
            int rowhandle = grdLotList.View.FocusedRowHandle;

            if (rowhandle > 0)
            {
                DataTable dt = grdLotList.DataSource as DataTable;

                double seq = Format.GetDouble(dt.Rows[rowhandle - 1]["SEQ"], 0);

                DataRow dr = grdLotList.View.GetFocusedDataRow();

                DataRow newDr = dt.NewRow();
                newDr.ItemArray = dr.ItemArray;

                int newRowIdx = rowhandle - 1;

                if (newRowIdx > -1)
                {
                    dt.Rows.Remove(dr);
                    dt.Rows.InsertAt(newDr, newRowIdx);
                }
                grdLotList.View.RefreshData();

                //dt.Rows[rowhandle]["SEQ"] = seq - 0.001;
                //dt.AcceptChanges();
                //grdLotList.View.SetSortOrder("SEQ", DevExpress.Data.ColumnSortOrder.Ascending);
                //dt.DefaultView.Sort = "SEQ ASC";
                //dt.DefaultView.ToTable(); 
            }
        }

        private void UcDataUpDownBtnCtrl_buttonClick(object sender, EventArgs e)
        {
            string btnState = ucDataUpDownBtnCtrl.ButtonState;

            if (btnState.Equals("Down"))
            {
                DataTable dt2 = grdLotList.DataSource as DataTable;
                DataTable dt = grdWIP.View.GetCheckedRows();






                if (dt.Rows.Count > 0)
                {

                    foreach (DataRow dr in dt.Rows)
                    {

                        Dictionary<string, object> param = new Dictionary<string, object>();
                        param.Add("LOTID", dr["LOTID"]);

                        DataTable isSplitLotCheck = SqlExecuter.Query("GetSplitCheck", "10001", param);

                        if (isSplitLotCheck.Rows.Count > 0 && chkMerge.Checked)
                        {
                            throw MessageException.Create("NotSplitLotMerge");
                        }

                    }
                }


                if (dt2.Rows.Count > 0)
                {

                    foreach (DataRow dr in dt.Rows)
                    {
                        foreach (DataRow dr2 in dt2.Rows)
                        {
                            if (!dr["SALESORDERID"].Equals(dr2["SALESORDERID"]) || !dr["PRODUCTDEFID"].Equals(dr2["PRODUCTDEFID"]))
                            {
                                throw MessageException.Create("SameItemSalesOrder");
                            }
                            if (!Format.GetString(dr["LOTID"]).Substring(12, 1).Equals(Format.GetString(dr2["LOTID"]).Substring(12, 1)))
                            {
                                throw MessageException.Create("NotSameInputNumber");
                            }

                        }


                    }
                }
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count - 1; i++)
                    {
                        for (int j = i + 1; j < dt.Rows.Count; j++)
                        {
                            if (!dt.Rows[i]["SALESORDERID"].Equals(dt.Rows[j]["SALESORDERID"]) || !dt.Rows[i]["PRODUCTDEFID"].Equals(dt.Rows[j]["PRODUCTDEFID"]))
                            {
                                throw MessageException.Create("SameItemSalesOrder");
                            }
                            if (!Format.GetString(dt.Rows[i]["LOTID"]).Substring(12, 1).Equals(Format.GetString(dt.Rows[j]["LOTID"]).Substring(12, 1)))
                            {
                                throw MessageException.Create("NotSameInputNumber");
                            }
                        }

                    }


                }



                string SalseOrderID = Format.GetFullTrimString(dt.Rows[0]["SALESORDERID"]);
                string LineNo = Format.GetFullTrimString(dt.Rows[0]["LINENO"]);
                string SegmentID = Format.GetFullTrimString(dt.Rows[0]["PROCESSSEGMENTID"]);
                string productDefid = Format.GetFullTrimString(dt.Rows[0]["PRODUCTDEFID"]);

                int LotSize = Format.GetInteger(dt.Rows[0]["LOTSIZE"]);

                DataTable TargetDt = grdLotList.DataSource as DataTable;

                int icnt = TargetDt.AsEnumerable().Where(c => !c.Field<string>("SALESORDERID").Equals(SalseOrderID) && !c.Field<string>("LINENO").Equals("LINENO")).Count();
                int iSegcnt = dt.AsEnumerable().Where(c => !c.Field<string>("PROCESSSEGMENTID").Equals(SegmentID)).Count();
                int iProductcnt = dt.AsEnumerable().Where(c => !c.Field<string>("PRODUCTDEFID").Equals(productDefid)).Count();
                //SameProductDefinition
                /*
                if (icnt > 0)
                {
                    throw MessageException.Create("CheckDuplicationSalseOrder");
                }
                */
                if (iSegcnt > 0)
                {
                    throw MessageException.Create("CheckDupliSegment");
                }
                if (iProductcnt > 0)
                {
                    throw MessageException.Create("SameProductDefinition", productDefid);
                }

                int iTragetRow = TargetDt.Rows.Count + dt.Rows.Count;

                if (UserInfo.Current.Enterprise.Equals(Constants.EnterPrise_InterFlex))
                {
                    if (iTragetRow > 1)
                    {

                        throw MessageException.Create("CheckRollCutSplit", "1");
                    }
                }
                /*
                else
                {
                    if (iTragetRow > 2)
                    {

                        throw MessageException.Create("CheckRollCutSplit", "2");


                    }
                }
                */
                if (LotSize > 0)
                    txtLotSize.EditValue = LotSize;

                setAreaComboBox(dt);


                DataTable dtLot = grdLotList.DataSource as DataTable;

            }
        }

        private void View_DoubleClick(object sender, EventArgs e)
        {
            int rowhandle = grdWIP.View.FocusedRowHandle;

            bool isChecked = grdWIP.View.IsRowChecked(rowhandle);

            if (isChecked)
            {
                grdWIP.View.CheckRow(rowhandle, false);
            }
            else
            {
                grdWIP.View.CheckRow(rowhandle, true);
            }
        }

        private void View_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0)
                return;

            bool ischecked = grdWIP.View.IsRowChecked(e.RowHandle);

            int rowIndex = grdWIP.View.FocusedRowHandle;
            if (ischecked)
            {
                e.Appearance.BackColor = Color.FromArgb(254, 254, 16);
                e.HighPriority = true;
            }
        }

        #region ▶ ComboBox Event |

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
            DataTable dtLot = grdCreateLot.DataSource as DataTable;

            if (dtLot == null || dtLot.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }

            DataTable MergeLot = new DataTable();

            if (dtLot.AsEnumerable().Where(c => c.Field<string>("ISMERGE").Equals("Y")).Count() > 0)
            {
                MergeLot = dtLot.AsEnumerable().Where(c => c.Field<string>("ISMERGE").Equals("Y")).CopyToDataTable();
            }
            List<DataRow> listRow = MergeLot.AsEnumerable().Where(c => c.Field<string>("LOTID").Equals(c.Field<string>("MERGELOT"))).ToList<DataRow>();

            foreach (DataRow moveDr in listRow)
            {
                MergeLot.Rows.Remove(moveDr);
            }

            string strAreaid = Format.GetFullTrimString(cboArea.GetColumnValue("AREAID"));
            string strResourceid = Format.GetFullTrimString(cboArea.GetColumnValue("RESOURCEID"));

            try
            {
                pnlContent.ShowWaitArea();

                MessageWorker worker = new MessageWorker("SaveRollCutiing");
                worker.SetBody(new MessageBody()
                {
                    { "EnterpriseID", UserInfo.Current.Enterprise },
                    { "PlantID", UserInfo.Current.Plant },
                    //{ "ArearID",Format.GetString(cboArea.EditValue) },
                    { "ArearID",Format.GetString(strAreaid) },
                    { "ResourceID",Format.GetString(strResourceid) },
                    { "Comments", txtComment.Text },
                    { "UserId", UserInfo.Current.Id },
                    { "Lotlist", dtLot },
                    { "MergeLotlist", MergeLot }
                });

                worker.Execute();
            }
            catch (Exception ex)
            {
                throw MessageException.Create(ex.Message);
            }
            finally
            {
                pnlContent.CloseWaitArea();
            }

            // Data 초기화

            if (chkPrintLotcard.CheckState == CheckState.Checked)
            {
                PrintLotCard();
            }
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
            /*
            this.grdWIP.DataSource = null;
            this.grdLotList.DataSource = null;
            */
            SetInitControl();
            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable dt = await SqlExecuter.QueryAsync("SelectRollCuttingTarget", "10001", values);

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
            //NoSelectTargetArea
            if (string.IsNullOrEmpty(Format.GetFullTrimString(cboArea.EditValue)))
            {
                throw MessageException.Create("NoSelectTargetArea");
            }
        }

        #endregion

        #region ◆ Private Function |

        private void AllClear()
        {
            grdWIP.DataSource = null;
            grdLotList.DataSource = null;
            grdCreateLot.DataSource = null;

            cboArea.EditValue = string.Empty;
            txtWorker.EditValue = string.Empty;

        }
        private void PrintLotCard()
        {
            DataTable dt = grdCreateLot.DataSource as DataTable;
            string LotList = string.Empty;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string LotID = Format.GetString(dt.Rows[i]["LOTID"]);
                string ISMerge = Format.GetString(dt.Rows[i]["ISMERGE"]);
                string MergeLot = Format.GetString(dt.Rows[i]["MERGELOT"]);
                string PLotID = Format.GetString(dt.Rows[i]["PARENTLOT"]);

                // 2020-02-04, 박정훈, 영풍 요청으로 모 LOT도 LOTCARD 출력되도록 수정
                if (ISMerge.Equals("N") && string.IsNullOrEmpty(MergeLot)) // && !LotID.Equals(PLotID)
                {
                    LotList = LotList + LotID + ",";
                }
                else if (ISMerge.Equals("Y") && LotID.Equals(MergeLot))
                {
                    LotList = LotList + LotID + ",";
                }
            }

            if (!string.IsNullOrEmpty(LotList))
            {
                LotList = LotList.TrimEnd(',');
                CommonFunction.PrintLotCard_Ver2(LotList, LotCardType.Normal);
            }

        }
        // TODO : 화면에서 사용할 내부 함수 추가
        private void setAreaComboBox(DataTable dt)
        {

            DataRow dr = dt.Rows[0];

            Dictionary<string, object> transitAreaParam = new Dictionary<string, object>();
            transitAreaParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            transitAreaParam.Add("PLANTID", UserInfo.Current.Plant);
            transitAreaParam.Add("LOTID", Format.GetFullTrimString(dr["LOTID"]));
            transitAreaParam.Add("PROCESSSEGMENTID", dr["AFTERPROCESSSEGMENTID"].ToString());
            transitAreaParam.Add("PROCESSSEGMENTVERSION", dr["AFTERPROCESSSEGMENTVERSION"].ToString());
            transitAreaParam.Add("RESOURCETYPE", "Resource");
            //transitAreaParam.Add("RESOURCETYPE", "Area");
            transitAreaParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            cboArea.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboArea.ShowHeader = false;
            cboArea.ValueMember = "RESOURCEID";
            cboArea.DisplayMember = "RESOURCENAME";
            //cboArea.ValueMember = "AREAID";
            //cboArea.DisplayMember = "AREANAME";
            cboArea.UseEmptyItem = true;
            cboArea.EmptyItemValue = "";
            cboArea.EmptyItemCaption = "";
            cboArea.DataSource = SqlExecuter.Query("GetTransitAreaList", "10032", transitAreaParam);
            cboArea.EditValue = cboArea.EmptyItemValue;
        }


        private void GenerateLot(DataTable dtTargetLotlist)
        {
            string siteCode = "";

            switch (UserInfo.Current.Plant)
            {
                case "IFC":
                    siteCode = "F";
                    break;
                case "IFV":
                    siteCode = "V";
                    break;
                case "CCT":
                    siteCode = "C";
                    break;
                case "YPE":
                    siteCode = "Y";
                    break;
                case "YPEV":
                    siteCode = "P";
                    break;
            }

            DataTable dtLotList = new DataTable("LotList");

            dtLotList.Columns.Add("PARENTLOT");
            dtLotList.Columns.Add("LOTID");
            dtLotList.Columns.Add("PANELQTY");
            dtLotList.Columns.Add("QTY");
            dtLotList.Columns.Add("ISMERGE");
            dtLotList.Columns.Add("MERGESUMQTY");
            dtLotList.Columns.Add("MERGELOT");

            int lotCnt = 0;
            int lotSize = Format.GetInteger(txtLotSize.EditValue);

            string prevCheckLot = string.Empty;
            int sequece = 0;
            for (int i = 0; i < dtTargetLotlist.Rows.Count; i++)
            {

                DataRow dr = dtTargetLotlist.Rows[i];

                string InputSeq = Format.GetFullTrimString(dr["INPUTSEQUENCE"]);
                string parentLotid = Format.GetFullTrimString(dr["LOTID"]);
                // 마이그랏이면 MES 채번 규칙 적용하여 분할
                string MigFlag = Format.GetFullTrimString(dr["DESCRIPTION"]);
                string matClass = Format.GetFullTrimString(dr["MATERIALCLASS"]);
                string matSequence = Format.GetFullTrimString(dr["MATERIALSEQUENCE"]);

                string tempLot = string.Empty;
                if (MigFlag.Equals("MIG"))
                {
                    tempLot = CommonFunction.CreateNewLotid(parentLotid, matClass, matSequence);
                }
                else
                {
                    tempLot = parentLotid;
                }
                string checkLotid = tempLot.Substring(0, tempLot.Length - 4);
                Dictionary<string, object> values = new Dictionary<string, object>();
                values.Add("LOTID", checkLotid);

                if (MigFlag.Equals("MIG"))
                {
                    values.Add("MLOTID", parentLotid.Substring(0, parentLotid.Length - 1));
                }
                else
                {
                    values.Add("MLOTID", checkLotid);
                }

                DataTable dtSequence = SqlExecuter.Query("GetLotIdMaxSequence_Rollcut", "10001", values);
                if (!prevCheckLot.Equals(checkLotid))
                    sequece = Format.GetInteger(dtSequence.Rows.Cast<DataRow>().FirstOrDefault()["LASTSEQUENCE"]);

                int LotQty = Format.GetInteger(dr["PNL"]);
                int PanelperQty = Format.GetInteger(dr["PANELPERQTY"]);

                int checkLotQty = LotQty;

                bool lastLot = (i == (dtTargetLotlist.Rows.Count - 1)) ? true : false;

                int checkSplitCnt = 0;

                prevCheckLot = checkLotid;

                while (checkLotQty > 0)
                {
                    int prevQty = lotSize + 1;
                    string isPrevMerge = "N";

                    int splitQty = checkLotQty - lotSize;
                    string PrevLotid = string.Empty;
                    string creteLotNo = string.Empty;
                    string isMerge = "N";
                    string prevMergeLotid = string.Empty;
                    bool UsePrevLot = false;
                    int mergeSumQty = 0;
                    string MergeLotid = string.Empty;


                    if (dtLotList.Rows.Count > 0)
                    {
                        prevQty = Format.GetInteger(dtLotList.Rows[dtLotList.Rows.Count - 1]["QTY"]);
                        isPrevMerge = Format.GetString(dtLotList.Rows[dtLotList.Rows.Count - 1]["ISMERGE"]);
                        PrevLotid = Format.GetString(dtLotList.Rows[dtLotList.Rows.Count - 1]["LOTID"]);
                        mergeSumQty = Format.GetInteger(dtLotList.Rows[dtLotList.Rows.Count - 1]["MERGESUMQTY"]);
                        prevMergeLotid = Format.GetString(dtLotList.Rows[dtLotList.Rows.Count - 1]["MERGELOT"]);
                    }
                    else
                    {

                    }

                    //int creatQty = splitQty > 0 ? lotSize : checkLotQty;
                    int creatQty = 0;

                    if (isPrevMerge.Equals("N"))
                    {
                        isMerge = splitQty < 0 && chkMerge.Checked && !lastLot ? "Y" : "N";
                        creatQty = splitQty > 0 ? lotSize : checkLotQty;
                        mergeSumQty = splitQty > 0 ? lotSize : checkLotQty;
                        lotCnt++;
                    }
                    else
                    {
                        isMerge = mergeSumQty == lotSize ? "N" : "Y";
                        lotCnt++;
                        if (isMerge.Equals("Y"))
                        {
                            creatQty = lotSize - mergeSumQty;
                            creatQty = creatQty < checkLotQty ? creatQty : checkLotQty;
                            UsePrevLot = true;
                        }
                        else
                        {
                            creatQty = splitQty > 0 ? lotSize : checkLotQty; ;
                        }
                        if (mergeSumQty + checkLotQty > lotSize)
                        {
                            mergeSumQty = lotSize;
                        }
                        else
                        {
                            mergeSumQty = mergeSumQty + checkLotQty;
                        }
                        //mergeSumQty = lotSize;

                    }
                    checkLotQty = checkLotQty - creatQty;
                    /*
                    if (UsePrevLot)
                    {
                        creteLotNo = PrevLotid;
                    }
                    else
                    {
                        creteLotNo = lotNo + InputSeq + "-FG01-" + lotCnt.ToString("000") + "-000";
                    }
                    */
                    if (checkSplitCnt > 0)
                    {
                        sequece++;
                        creteLotNo = checkLotid + "-" + sequece.ToString("000");
                    }
                    else
                    {
                        //첫번째는 모랏 그대로?
                        creteLotNo = parentLotid;
                    }

                    if (isPrevMerge.Equals("N") && isMerge.Equals("Y"))
                    {
                        MergeLotid = creteLotNo;
                    }
                    else if (isPrevMerge.Equals("Y") && isMerge.Equals("Y"))
                    {
                        MergeLotid = prevMergeLotid;
                    }

                    dtLotList.Rows.Add(parentLotid, creteLotNo, creatQty, creatQty * PanelperQty, isMerge, mergeSumQty, MergeLotid);
                    checkSplitCnt++;
                }



            }
            grdCreateLot.DataSource = dtLotList;
        }


        #region ▶ Control Data 초기화 |
        /// <summary>
        /// Control Data 초기화
        /// </summary>
        private void SetInitControl()
        {
            grdWIP.View.ClearDatas();
            grdLotList.View.ClearDatas();
            grdCreateLot.View.ClearDatas();
            cboArea.EditValue = string.Empty;
            txtWorker.EditValue = string.Empty;
            txtComment.EditValue = string.Empty;
            txtLotSize.EditValue = string.Empty;
        }
        #endregion

        #endregion

        private void smartButton1_Click(object sender, EventArgs e)
        {
            string LotList = "191014F001-1-FG00-001-004,191014F001-1-FG00-001-005";
            CommonFunction.PrintLotCard_Ver2(LotList, LotCardType.Split);
        }
    }
}
