using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Micube.SmartMES.Commons;
using Micube.Framework.SmartControls.Grid;

namespace Micube.SmartMES.ProcessManagement
{ 
    public partial class TransitBatchRegist : SmartConditionBaseForm
    {
        string _Text;
        SmartBandedGrid grdList = new SmartBandedGrid();

        string nextprocess = "후공정 : ";

        public TransitBatchRegist()
        {
            InitializeComponent();
        }

        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeEvent();

            InitializeGrid_grdWIP();
            InitializeGrid_grdTransArea();

            this.ucDataUpDownBtn.SourceGrid = this.grdWIP;
            this.ucDataUpDownBtn.TargetGrid = this.grdTransArea;

            simpleLabelItem1.Text = nextprocess;
        }

        private void InitializeGrid_grdWIP()
        {
            grdWIP.GridButtonItem = GridButtonItem.None;
            grdWIP.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            
            grdWIP.Caption = "작업 완료 List";

            grdWIP.View.AddTextBoxColumn("PLANTID", 50).SetIsReadOnly();
            grdWIP.View.AddTextBoxColumn("LOTPRODUCTTYPE", 50).SetIsReadOnly();
            grdWIP.View.AddTextBoxColumn("THEPRODUCTTYPE", 50).SetIsReadOnly();
            grdWIP.View.AddTextBoxColumn("PRODUCTDEFID", 100).SetIsReadOnly();
            grdWIP.View.AddTextBoxColumn("PRODUCTDEFVERSION", 50).SetIsReadOnly();
            grdWIP.View.AddTextBoxColumn("PRODUCTDEFNAME", 100).SetIsReadOnly();
            grdWIP.View.AddTextBoxColumn("LOTID", 100).SetIsReadOnly();
            grdWIP.View.AddTextBoxColumn("USERSEQUENCE", 100).SetIsReadOnly();
            grdWIP.View.AddTextBoxColumn("PROCESSSEGMENTID", 100).SetIsReadOnly().SetIsHidden();
            grdWIP.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 100).SetIsReadOnly();
            grdWIP.View.AddTextBoxColumn("WORKAREA", 100).SetIsReadOnly();
            grdWIP.View.AddTextBoxColumn("PCSQTY", 100).SetIsReadOnly();
            grdWIP.View.AddTextBoxColumn("PNLQTY", 100).SetIsReadOnly();
            grdWIP.View.AddTextBoxColumn("TRACKOUTTIME", 100).SetIsReadOnly();
            grdWIP.View.AddTextBoxColumn("SUBPROCESSDEFID", 100).SetIsReadOnly().SetIsHidden(); 
            grdWIP.View.AddTextBoxColumn("PROCESSSEGMENTVERSION", 100).SetIsReadOnly().SetIsHidden();
            grdWIP.View.AddTextBoxColumn("RESOURCEID", 100).SetIsReadOnly().SetIsHidden();
            grdWIP.View.AddTextBoxColumn("SUBPROCESSDEFVERSION", 100).SetIsReadOnly().SetIsHidden();
            grdWIP.View.AddTextBoxColumn("LOTCREATEDTYPE", 100).SetIsReadOnly().SetIsHidden();
            grdWIP.View.AddTextBoxColumn("PROCESSDEFVERSION", 100).SetIsReadOnly().SetIsHidden();
            grdWIP.View.AddTextBoxColumn("PROCESSDEFID", 100).SetIsReadOnly().SetIsHidden();
            grdWIP.View.AddTextBoxColumn("PROCESSPATHSTACK", 100).SetIsReadOnly().SetIsHidden();
            grdWIP.View.AddTextBoxColumn("UNIT", 100).SetIsReadOnly().SetIsHidden();
            grdWIP.View.AddTextBoxColumn("DEFECTQTY", 100).SetIsReadOnly().SetIsHidden();
            grdWIP.View.AddTextBoxColumn("PATHSEQUENCE", 100).SetIsReadOnly();
            grdWIP.View.AddTextBoxColumn("PATHTYPE", 100).SetIsReadOnly();

            grdWIP.View.PopulateColumns();

            this.grdWIP.View.OptionsCustomization.AllowSort = false;
        }

        private void InitializeGrid_grdTransArea()
        {
            grdTransArea.GridButtonItem = GridButtonItem.None;
            grdTransArea.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            // 코드그룹ID
            grdTransArea.Caption = "대상 LOT";

            grdTransArea.View.AddTextBoxColumn("PLANTID", 100).SetIsReadOnly();
            grdTransArea.View.AddTextBoxColumn("LOTPRODUCTTYPE", 100).SetIsReadOnly().SetValidationIsRequired();
            grdTransArea.View.AddTextBoxColumn("THEPRODUCTTYPE", 100).SetIsReadOnly();
            grdTransArea.View.AddTextBoxColumn("PRODUCTDEFID", 100).SetIsReadOnly();
            grdTransArea.View.AddTextBoxColumn("PRODUCTDEFVERSION", 100).SetIsReadOnly();
            grdTransArea.View.AddTextBoxColumn("PRODUCTDEFNAME", 100).SetIsReadOnly();
            grdTransArea.View.AddTextBoxColumn("LOTID", 100).SetIsReadOnly();
            grdTransArea.View.AddTextBoxColumn("USERSEQUENCE", 100).SetIsReadOnly();
            grdTransArea.View.AddTextBoxColumn("PROCESSSEGMENTID", 100).SetIsReadOnly().SetIsHidden();
            grdTransArea.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 100).SetIsReadOnly();
            grdTransArea.View.AddTextBoxColumn("WORKAREA", 100).SetIsReadOnly();
            grdTransArea.View.AddTextBoxColumn("PCSQTY", 100).SetIsReadOnly();
            grdTransArea.View.AddTextBoxColumn("PNLQTY", 100).SetIsReadOnly();
            grdTransArea.View.AddTextBoxColumn("TRACKOUTTIME", 100).SetIsReadOnly();
            grdTransArea.View.AddTextBoxColumn("SUBPROCESSDEFID", 100).SetIsReadOnly().SetIsHidden();
            grdTransArea.View.AddTextBoxColumn("PROCESSSEGMENTVERSION", 100).SetIsReadOnly().SetIsHidden();
            grdTransArea.View.AddTextBoxColumn("RESOURCEID", 100).SetIsReadOnly().SetIsHidden();
            grdTransArea.View.AddTextBoxColumn("SUBPROCESSDEFVERSION", 100).SetIsReadOnly().SetIsHidden();
            grdTransArea.View.AddTextBoxColumn("LOTCREATEDTYPE", 100).SetIsReadOnly().SetIsHidden();
            grdTransArea.View.AddTextBoxColumn("PROCESSDEFVERSION", 100).SetIsReadOnly().SetIsHidden();
            grdTransArea.View.AddTextBoxColumn("PROCESSDEFID", 100).SetIsReadOnly().SetIsHidden();
            grdTransArea.View.AddTextBoxColumn("PROCESSPATHSTACK", 100).SetIsReadOnly().SetIsHidden();
            grdTransArea.View.AddTextBoxColumn("UNIT", 100).SetIsReadOnly().SetIsHidden();
            grdTransArea.View.AddTextBoxColumn("DEFECTQTY", 100).SetIsReadOnly().SetIsHidden();
            grdTransArea.View.AddTextBoxColumn("PATHSEQUENCE", 100).SetIsReadOnly();
            grdTransArea.View.AddTextBoxColumn("PATHTYPE", 100).SetIsReadOnly();

            grdTransArea.View.PopulateColumns();
        }

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            ucDataUpDownBtn.buttonClick += UcDataUpDownBtn_buttonClick;
            grdWIP.View.CheckStateChanged += View_CheckStateChanged;
        }

		private void UcDataUpDownBtn_buttonClick(object sender, EventArgs e)
        {
            DataTable dt = grdWIP.View.GetCheckedRows();
            DataTable tgdt = grdTransArea.DataSource as DataTable;

            Dictionary<string, object> nextProcparam = new Dictionary<string, object>();

            if (this.ucDataUpDownBtn.ButtonState.Equals("Down"))
            {
                CheckedValidation();

                if (tgdt == null || tgdt.Rows.Count <= 0)
                {
                    SetComboData();
                }
                else
                {
                    string productDefid = Format.GetString(dt.AsEnumerable().Select(r => Format.GetString("PRODUCTDEFID")).Distinct().FirstOrDefault());
                    string tgProductDefid = Format.GetString(tgdt.AsEnumerable().Select(r => Format.GetString("PRODUCTDEFID")).Distinct().FirstOrDefault());
                    string productDefVersion = Format.GetString(dt.AsEnumerable().Select(r => Format.GetString("PRODUCTDEFVERSION")).Distinct().FirstOrDefault());
                    string tgProductDefVersion = Format.GetString(tgdt.AsEnumerable().Select(r => Format.GetString("PRODUCTDEFVERSION")).Distinct().FirstOrDefault());
                    string processsegmentId = Format.GetString(dt.AsEnumerable().Select(r => Format.GetString("PROCESSSEGMENTID")).Distinct().FirstOrDefault());
                    string tgProcesssegmentId = Format.GetString(tgdt.AsEnumerable().Select(r => Format.GetString("PROCESSSEGMENTID")).Distinct().FirstOrDefault());
                    string processsegmentVersion = Format.GetString(dt.AsEnumerable().Select(r => Format.GetString("PROCESSSEGMENTVERSION")).Distinct().FirstOrDefault());
                    string tgProcesssegmentVersion = Format.GetString(tgdt.AsEnumerable().Select(r => Format.GetString("PROCESSSEGMENTVERSION")).Distinct().FirstOrDefault());
                    int userSequence = Format.GetInteger(dt.AsEnumerable().Select(r => Format.GetString(r["USERSEQUENCE"])).Distinct().FirstOrDefault());
                    int tgUserSequence = Format.GetInteger(tgdt.AsEnumerable().Select(r => Format.GetString(r["USERSEQUENCE"])).Distinct().FirstOrDefault());
                    string plantId = Format.GetString(dt.AsEnumerable().Select(r => Format.GetString("PLANTID")).Distinct().FirstOrDefault());
                    string tgPlantId = Format.GetString(tgdt.AsEnumerable().Select(r => Format.GetString("PLANTID")).Distinct().FirstOrDefault());
                    string lotType = Format.GetString(dt.AsEnumerable().Select(r => Format.GetString("LOTTYPEID")).Distinct().FirstOrDefault());
                    string tgLotType = Format.GetString(tgdt.AsEnumerable().Select(r => Format.GetString("LOTTYPEID")).Distinct().FirstOrDefault());
                    string processState = Format.GetString(dt.AsEnumerable().Select(r => Format.GetString("PROCESSSTATE")).Distinct().FirstOrDefault());
                    string tgProcessState = Format.GetString(tgdt.AsEnumerable().Select(r => Format.GetString("PROCESSSTATE")).Distinct().FirstOrDefault());


                    if (!productDefid.Equals(tgProductDefid) || !productDefVersion.Equals(tgProductDefVersion))
                    {
                        ShowMessage("NotAddTransAreaList", string.Format("{0}, {1}", Language.Get("PRODUCTDEF"), Language.Get("PRODUCTDEFVERSION")));
                        grdWIP.View.CheckedAll(false);
                        return;
                    }
                    else if (!processsegmentId.Equals(tgProcesssegmentId) || !processsegmentVersion.Equals(tgProcesssegmentVersion))
                    {
                        ShowMessage("NotAddTransAreaList", string.Format("{0}, {1}", Language.Get("PROCESSSEGMENT"), Language.Get("PROCESSSEGMENTVERSION")));
                        grdWIP.View.CheckedAll(false);
                        return;
                    }
                    else if (!userSequence.Equals(tgUserSequence))
                    {
                        ShowMessage("NotAddTransAreaList", Language.Get("USERSEQUENCE"));
                        grdWIP.View.CheckedAll(false);
                        return;
                    }
                    else if (!plantId.Equals(tgPlantId))
                    {
                        ShowMessage("NotAddTransAreaList", Language.Get("SITE"));
                        grdWIP.View.CheckedAll(false);
                        return;
                    }
                    else if (!lotType.Equals(tgLotType))
                    {
                        ShowMessage("NotAddTransAreaList", Language.Get("PRODUCTIONTYPE"));
                        grdWIP.View.CheckedAll(false);
                        return;
                    }

                    SetComboData();
                }
            }
            else
            {
                //2020-7-7 조일섭 수정 : 그리드에 레코드가 남아 있음에도 다음공정명 사라짐 현상 수정.
                simpleLabelItem1.Text = nextprocess;
                SetComboData();

                
            }
        }

        private void View_CheckStateChanged(object sender, EventArgs e)
        {
            GridCheckMarksSelection view = (GridCheckMarksSelection)sender;
            DataTable dt = grdWIP.View.GetCheckedRows();

            int productCount = dt.AsEnumerable().Select(r => r.Field<string>("PROCESSSEGMENTNAME")).Distinct().Count();
            if (productCount > 1)
            {
                grdWIP.View.CheckRow(grdWIP.View.GetFocusedDataSourceRowIndex(), false);

                throw MessageException.Create("MixSelectProcesssegmentname");
            }
        }

        private void CheckedValidation()
        {
            DataTable selected = grdWIP.View.GetCheckedRows();

            int productDefIdCount = selected.AsEnumerable().Select(r => Format.GetString(r["PRODUCTDEFID"])).Distinct().Count();
            if (productDefIdCount > 1)
            {
                grdWIP.View.CheckedAll(false);
                throw MessageException.Create("MixSelectProductDefID");
            }

            int productDefVerCount = selected.AsEnumerable().Select(r => Format.GetString(r["PRODUCTDEFVERSION"])).Distinct().Count();
            if (productDefVerCount > 1)
            {
                grdWIP.View.CheckedAll(false);
                throw MessageException.Create("MixSelectProductDefVersion");
            }

            int processsegmentIdCount = selected.AsEnumerable().Select(r => Format.GetString(r["PROCESSSEGMENTID"])).Distinct().Count();
            if (processsegmentIdCount > 1)
            {
                grdWIP.View.CheckedAll(false);
                throw MessageException.Create("MixSelectProcesssegmentname");
            }

            int processsegmentVerCount = selected.AsEnumerable().Select(r => Format.GetString(r["PROCESSSEGMENTVERSION"])).Distinct().Count();
            if (processsegmentVerCount > 1)
            {
                grdWIP.View.CheckedAll(false);
                throw MessageException.Create("MixSelectProcesssegmentname");
            }

            int usersequenceCount = selected.AsEnumerable().Select(r => Format.GetString(r["USERSEQUENCE"])).Distinct().Count();
            if (usersequenceCount > 1)
            {
                grdWIP.View.CheckedAll(false);
                throw MessageException.Create("MixProcessPath");
            }

            int plantIdCount = selected.AsEnumerable().Select(r => Format.GetString(r["PLANTID"])).Distinct().Count();
            if (plantIdCount > 1)
            {
                grdWIP.View.CheckedAll(false);
                throw MessageException.Create("MixSelectPlantID");
            }

            int lotTypeCount = selected.AsEnumerable().Select(r => Format.GetString(r["LOTTYPEID"])).Distinct().Count();
            if (lotTypeCount > 1)
            {
                grdWIP.View.CheckedAll(false);
                throw MessageException.Create("MixSelectLotType");
            }
        }

		private void SetComboData()
        {
            DataTable dt = grdWIP.View.GetCheckedRows();
            DataTable tgdt = grdTransArea.DataSource as DataTable;
            string buttonState = ucDataUpDownBtn.ButtonState;

            DataTable dataTable = new DataTable();
            string resourceId = string.Empty;
            Dictionary<string, object> param = new Dictionary<string, object>();

            if ((tgdt == null || tgdt.Rows.Count == 0) && buttonState.Equals("Down")) { dataTable = dt.Copy(); }
            else { dataTable = tgdt.Copy(); }

            switch (buttonState)
            {
                case "Up":
                    int checkedCount = grdTransArea.View.GetCheckedRows().Rows.Count;
                    if (grdTransArea.View.RowCount.Equals(checkedCount))
                    {
                        cboTargetArea.Editor.DataSource = null;
                        return;
                    }

                    for (int i = 0; i < grdTransArea.View.RowCount; i++)
                    {
                        if (!grdTransArea.View.IsRowChecked(i))
                            resourceId += Format.GetString(grdTransArea.View.GetRowCellValue(i, "RESOURCEID")) + ",";
                    }

                    break;
                case "Down":

                    if (tgdt != null && tgdt.Rows.Count > 1)
                    {
                        tgdt.AsEnumerable().ForEach(r => {
                            resourceId += Format.GetString(r["RESOURCEID"]) + ",";
                        });
                    }

                    dt.AsEnumerable().ForEach(r =>
                    {
                        resourceId += Format.GetString(r["RESOURCEID"]) + ",";
                    });

                    break;
            }

            string processDefid = string.Empty;
            string processDefversion = string.Empty;
            string Productdefid = string.Empty;
            string Productdefversion = string.Empty;
            string subprocessDefid = Format.GetFullTrimString(dataTable.Rows[0]["SUBPROCESSDEFID"]);
            string subprocessDefversion = Format.GetFullTrimString(dataTable.Rows[0]["SUBPROCESSDEFVERSION"]);
            string lotCreateType = Format.GetFullTrimString(dataTable.Rows[0]["LOTCREATEDTYPE"]);

            if (subprocessDefid.Equals(string.Empty) && !lotCreateType.Equals("Return"))
            {
                processDefid = Format.GetFullTrimString(dataTable.Rows[0]["PROCESSDEFID"]);
                processDefversion = Format.GetFullTrimString(dataTable.Rows[0]["PROCESSDEFVERSION"]);
                Productdefid = Format.GetFullTrimString(dataTable.Rows[0]["PRODUCTDEFID"]);
                Productdefversion = Format.GetFullTrimString(dataTable.Rows[0]["PRODUCTDEFVERSION"]);
            }
            else if (!string.IsNullOrWhiteSpace(subprocessDefid))
            {
                processDefid = subprocessDefid;
                processDefversion = subprocessDefversion;
            }
            else if (lotCreateType.Equals("Return"))
            {
                processDefid = Format.GetFullTrimString(dataTable.Rows[0]["PROCESSDEFID"]);
                processDefversion = Format.GetFullTrimString(dataTable.Rows[0]["PROCESSDEFVERSION"]);
            }
            param.Add("PLANTID", dataTable.Rows[0]["PLANTID"]);
            param.Add("RESOURCEID", resourceId);
            param.Add("LOTID", dataTable.Rows[0]["LOTID"]);
            if (!string.IsNullOrWhiteSpace(Productdefid))
            {
                param.Add("PRODUCTDEFID", Productdefid);
                param.Add("PRODUCTDEFVERSION", Productdefversion);
            }
            param.Add("PROCESSSEGMENTID", dataTable.Rows[0]["PROCESSSEGMENTID"]);
            param.Add("PROCESSSEGMENTVERSION", dataTable.Rows[0]["PROCESSSEGMENTVERSION"]);
            param.Add("PROCESSDEFID", processDefid);
            param.Add("PROCESSDEFVERSION", processDefversion);
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("THEPRODUCTTYPE", dataTable.Rows[0]["THEPRODUCTTYPE"]);
            param.Add("USERSEQUENCE", dataTable.Rows[0]["USERSEQUENCE"]);
            param.Add("PATHSEQUENCE", dataTable.Rows[0]["PATHSEQUENCE"]);
            param.Add("PATHTYPE", dataTable.Rows[0]["PATHTYPE"]);

            InitializeComboBox(param);
        }

		private void InitializeComboBox(Dictionary<string, object> param)
        {
            cboTargetArea.Editor.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboTargetArea.Editor.ValueMember = "RESOURCE";
            cboTargetArea.Editor.DisplayMember = "RESOURCENAME";
            cboTargetArea.Editor.ShowHeader = false;

            DataTable dataTable = new DataTable();

            Dictionary<string, object> tempparam = new Dictionary<string, object>();

            tempparam.Add("PROCESSDEFID", param["PRODUCTDEFID"]);
            tempparam.Add("PROCESSSEGMENTID", param["PROCESSSEGMENTID"]);
            tempparam.Add("PROCESSSEGMENTVERSION", param["PROCESSSEGMENTVERSION"]);
            tempparam.Add("USERSEQUENCE", param["USERSEQUENCE"]);

            string str = param["THEPRODUCTTYPE"].ToString();
            string pathType = param["PATHTYPE"].ToString();

            DataTable tempTable = new DataTable();

            if (str.Equals("반제품"))
            {
                if(pathType.Equals("Start") || pathType.Equals("Normal"))
                {

                }
                else
                {
                    tempTable = SqlExecuter.Query("TransitBatchRegist_Serach", "00002", tempparam);

                    if (tempTable.Rows.Count == 0)
                    {

                    }
                    else
                    {
                        param.Remove("PROCESSSEGMENTID");
                        param.Remove("PRODUCTDEFID");
                        param.Remove("PROCESSDEFID");
                        param.Remove("PROCESSSEGMENTVERSION");
                        param.Remove("USERSEQUENCE");

                        param.Add("PROCESSSEGMENTID", tempTable.Rows[0]["PROCESSSEGMENTID"]);
                        param.Add("PRODUCTDEFID", tempTable.Rows[0]["PRODUCTDEFID"]);
                        param.Add("PROCESSDEFID", tempTable.Rows[0]["PROCESSDEFID"]);
                        param.Add("PROCESSSEGMENTVERSION", tempTable.Rows[0]["PROCESSSEGMENTVERSION"]);
                        param.Add("USERSEQUENCE", tempTable.Rows[0]["USERSEQUENCE"]);
                    }
                }                
            }

            DataTable dtTargetAreaList = new DataTable();
            if (str.Equals("제품"))
            {
                dtTargetAreaList = SqlExecuter.Query("TransitBatchRegist_Area_Serach", "00001", param);
            }
            else if(str.Equals("반제품") && (pathType.Equals("Start") || pathType.Equals("Normal")))
            {
                dtTargetAreaList = SqlExecuter.Query("TransitBatchRegist_Area_Serach", "00002", param);
            }
            else if (str.Equals("반제품") && (pathType.Equals("End") || pathType.Equals("StartEnd"))) //2020-7-7 조일섭 수정 : 반제품의 최종공정 로트는 제품의 작업장을 가져오기 수정
            {
                dtTargetAreaList = SqlExecuter.Query("TransitBatchRegist_Area_Serach", "00004", param);
            }
            else
            {
                dtTargetAreaList = SqlExecuter.Query("TransitBatchRegist_Area_Serach", "00003", param);
            }

            // 2020-7-7 조일섭 수정 : 최종공정일 경우 다음공정 가져오기
            DataTable dtNextProc = new DataTable();

            if (pathType.Equals("Start") || pathType.Equals("Normal"))
            {
                 dtNextProc = SqlExecuter.Query("TransitBatchRegist_NextProcesssegment_Serach", "00001", param);  //다음공정명 가져오기
            }
            else if (pathType.Equals("End") || pathType.Equals("StartEnd"))
            {
                 dtNextProc = SqlExecuter.Query("TransitBatchRegist_NextProcesssegment_Serach", "00002", param);  //다음공정명 가져오기
            }

            if (dtNextProc.Rows.Count == 0)
            {
                simpleLabelItem1.Text = nextprocess;
            }
            else
            {
                string nextProc = Format.GetFullTrimString(dtNextProc.Rows[0]["NEXTPROCESSSEGMENTNAME"]);
                simpleLabelItem1.Text = nextprocess + nextProc;
            }

            cboTargetArea.Editor.DataSource = dtTargetAreaList;

            if (dtTargetAreaList.Rows.Count > 0)
            {
                cboTargetArea.Editor.ItemIndex = 0;
            }
        }

        private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            throw new NotImplementedException();
        }

        protected override void OnToolbarSaveClick()
        {
            if (ShowMessage(MessageBoxButtons.YesNo, DialogResult.No, "InfoSave") == DialogResult.No) return;

            base.OnToolbarSaveClick();

            if (!grdTransArea.Enabled)
                throw MessageException.Create("NoSaveData");

            Dictionary<string, object> workInfo = new Dictionary<string, object>();

            string worker = UserInfo.Current.Id;

            DataTable datatable = grdTransArea.DataSource as DataTable;

            int rowCount = datatable.Rows.Count;

            for (int i = 0; i < rowCount; i++)
            {
                string lotId = datatable.Rows[i]["LOTID"].ToString();
                string processPathId = datatable.Rows[i]["PROCESSPATHSTACK"].ToString();
                string processSegmentId = datatable.Rows[i]["PROCESSSEGMENTID"].ToString();
                string unit = datatable.Rows[i]["UNIT"].ToString();
                string goodQty = datatable.Rows[i]["PCSQTY"].ToString();
                string goodPnlQty = datatable.Rows[i]["PNLQTY"].ToString();
                double defectQty = 0.0;

                if (datatable.Rows[i]["DEFECTQTY"].ToString() != String.Empty)
                {
                    defectQty = Convert.ToDouble(datatable.Rows[i]["DEFECTQTY"].ToString());
                }

                string[] str = cboTargetArea.EditValue.ToString().Split('|');
                string resourceId = str[0];
                string transitArea = str[1];

                MessageWorker messageWorker = new MessageWorker("SaveSendLot");
                messageWorker.SetBody(new MessageBody()
                {
                    { "EnterpriseId", UserInfo.Current.Enterprise },
                    { "PlantId", UserInfo.Current.Plant },
                    { "Worker", worker },
                    { "LotId", lotId },
                    { "ProcessPathId", processPathId },
                    { "ProcessSegmentId", processSegmentId },
                    { "DefectUnit", unit },
                    { "GoodQty", goodQty },
                    { "GoodPnlQty", goodPnlQty },
                    { "DefectQty", defectQty },
                    { "ResourceId", resourceId },
                    { "TransitArea", transitArea },
                    { "Comment", "일괄인계등록 처리" }
                });
                messageWorker.Execute();
            }
            ShowMessage("SuccedSave");
        }

        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            this.grdWIP.View.ClearDatas(); ;
            this.grdTransArea.View.ClearDatas();
            this.cboTargetArea.Editor.DataSource = null;

            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable dtWipTransAreaList = await QueryAsync("TransitBatchRegist_Serach", "00001", values);
            if (dtWipTransAreaList.Rows.Count == 0)
            {
                ShowMessage("NoSelectData");
            }

            grdWIP.DataSource = dtWipTransAreaList;

            simpleLabelItem1.Text = nextprocess;
        }

        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            CommonFunction.AddConditionAreaByAuthorityPopup("P_AREAID", 3.1, false, Conditions, true, false);
            InitializeCondition_ProductPopup("P_PRODUCTDEFID", 3.2, true, Conditions);
        }

        public static ConditionCollection InitializeCondition_ProductPopup(string id, double position, bool isMultiSelect, ConditionCollection conditions, string displayFieldName = "PRODUCTDEFID", string valueFieldName = "PRODUCTDEFID"
            , bool hideVersion = false, int maxCheck = 0, bool isUseProductDivisionCondition = true)
        {
            var conditionProductId = conditions.AddSelectPopup(id, new SqlQuery("GetProductDefinitionList", "10001", $"PLANTID={UserInfo.Current.Plant}"), displayFieldName, valueFieldName)
                .SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupLayoutForm(800, 800)
                .SetPopupAutoFillColumns("UNIT")
                .SetLabel("PRODUCTDEFID")
                .SetPosition(position);

            if (isMultiSelect)
                conditionProductId.SetPopupResultCount(maxCheck);
            else
                conditionProductId.SetPopupResultCount(1);

            conditionProductId.Conditions.AddTextBox("TXTPRODUCTDEFNAME");
            if (isUseProductDivisionCondition)
                conditionProductId.Conditions.AddComboBox("PRODUCTDIVISION", new SqlQuery("GetTypeList", "10001", $"CODECLASSID=ProductDivision2", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID").SetDefault("Product");

            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            if (!hideVersion)
            {
                conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 100);
            }

            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFTYPE", 90);
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTIONTYPE", 90);
            conditionProductId.GridColumns.AddTextBoxColumn("UOMDEFNAME", 90);

            return conditions;
        }

        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();
        }

        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            grdTransArea.View.CheckValidation();

            DataTable changed = grdTransArea.GetChangedRows();

            if (changed.Rows.Count == 0)
            {
                throw MessageException.Create("NoSaveData");
            }
        }

        private void txtLotno_KeyDown(object sender, KeyEventArgs e)
        {
            string a = txtLotno.Text;

            if (e.KeyCode == Keys.Enter)
            {
                DataTable dt = grdWIP.DataSource as DataTable;
                DataRow[] row = dt.Select("LOTID='" + txtLotno.Text + "'");
                                
                DataRow newRow = dt.NewRow();
                newRow.ItemArray = row[0].ItemArray;
                dt.Rows.Remove(row[0]);
                dt.Rows.InsertAt(newRow, 0);

                grdWIP.View.CheckRow(0, true);

                dt.AcceptChanges();

                txtLotno.SelectAll();
                txtLotno.Focus();

                txtLotno.EditValue = string.Empty;

                CheckedValidation();

            }
            else
            {
                return;
            }
        }
    }
}