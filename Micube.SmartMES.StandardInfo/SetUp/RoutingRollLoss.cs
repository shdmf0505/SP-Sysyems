#region using

using System;
using Micube.SmartMES.Commons.Controls;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Base;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Grid;
using Micube.Framework.SmartControls.Grid.BandedGrid;
using Micube.Framework.SmartControls.Validations;

#endregion

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    ///     프로그램명 : 기준정보 > Setup > 표준공정등록
    ///     업 무 설명 : 표준공정 등록및 조회
    ///     생  성  자 : 윤성원
    ///     생  성  일 : 2019-06-27
    ///     수정 이 력 :
    /// </summary>
    public partial class RoutingRollLoss : SmartConditionManualBaseForm
    {
        #region 생성자

        private int focusedRowHandle = 0;
        
        public RoutingRollLoss()
        {
            InitializeComponent();
        }




        /// <summary>
        /// 외부에서 호출시 자동 조회
        /// </summary>
        /// <param name="parameters"></param>
        public override void LoadForm(Dictionary<string, object> parameters)
        {
            base.LoadForm(parameters);

            if (parameters != null)
            {
                _parameters = parameters;
                Conditions.SetValue("ITEMID", 0, parameters["ITEMID"]);
                Conditions.SetValue("P_ITEMVERSION", 0, parameters["ITEMVERSION"]);
                OnSearchAsync();


            }
        }
        #endregion

        #region 툴바

        /// <summary>
        ///     저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            var dtSum = (DataTable)grdRollLossOperation.DataSource;
            var dtCopy = dtSum.Copy();
            //var dtSum = new DataTable();

            //dtSum.Columns.Add("PLANTID");
            //dtSum.Columns.Add("MATERIALID");
            //dtSum.Columns.Add("PRODUCTDEFID");
            //dtSum.Columns.Add("PRODUCTDEFVERSION");
            //dtSum.Columns.Add("OPERATIONID");
            //dtSum.Columns.Add("LOSSM");
            //dtSum.Columns.Add("LOSSPNL");
            //dtSum.Columns.Add("DESCRIPTION");
            //dtSum.Columns.Add("CREATOR");
            //dtSum.Columns.Add("CREATEDTIME");
            //dtSum.Columns.Add("MODIFIER");
            //dtSum.Columns.Add("MODIFIEDTIME");
            //dtSum.Columns.Add("_STATE_");

            //var routingRollRow = grdRoutingRollLoss.View.GetFocusedDataRow();

            //var newRow = dtSum.NewRow();
            //newRow["PLANTID"] = routingRollRow["PLANTID"];
            //newRow["MATERIALID"] = routingRollRow["MATERIALDEFNAME"];
            //newRow["PRODUCTDEFID"] = routingRollRow["PRODUCTDEFID"];
            //newRow["PRODUCTDEFVERSION"] = routingRollRow["PRODUCTDEFVERSION"];
            //newRow["OPERATIONID"] = routingRollRow["OPERATIONID"];
            //newRow["LOSSM"] = txtROLLLOSSM.Value;
            //newRow["LOSSPNL"] = txtRollSize.Value;

            //newRow["CREATOR"] = string.IsNullOrEmpty(routingRollRow["CREATOR"].ToString())
            //    ? string.Empty
            //    : routingRollRow["CREATOR"];
            //newRow["CREATEDTIME"] = string.IsNullOrEmpty(routingRollRow["CREATEDTIME"].ToString())
            //    ? string.Empty
            //    : routingRollRow["CREATEDTIME"];
            //newRow["MODIFIER"] = string.IsNullOrEmpty(routingRollRow["MODIFIER"].ToString())
            //    ? string.Empty
            //    : routingRollRow["MODIFIER"];
            //newRow["MODIFIEDTIME"] = string.IsNullOrEmpty(routingRollRow["MODIFIEDTIME"].ToString())
            //    ? string.Empty
            //    : routingRollRow["MODIFIEDTIME"];
            //newRow["_STATE_"] = "modified";

            //dtSum.Rows.Add(newRow);

            if (!dtCopy.Columns.Contains("MATERIALID"))
                dtCopy.Columns.Add("MATERIALID");
            if (!dtCopy.Columns.Contains("PRODUCTDEFID"))
                dtCopy.Columns.Add("PRODUCTDEFID");
            if (!dtCopy.Columns.Contains("_STATE_"))
                dtCopy.Columns.Add("_STATE_");

            //dtSum.AcceptChanges();

            foreach (DataRow dtSumRow in dtCopy.Rows)
            {
                dtSumRow["_STATE_"] = "modified";
                dtSumRow["MATERIALID"] = txtItemCodeRoll.Text;

            }

            focusedRowHandle = grdRoutingRollLoss.View.FocusedRowHandle;

            ExecuteRule("RollLossOperation", dtCopy);

            if (isNewData)
            {
                //생성
            }


            //if (dtSum.Compute("SUM(LOSSM)", "").ToString() != "")
            //{
            //    txtROLLLOSSM.Text = decimal.Parse(dtSum.Compute("SUM(LOSSM)", "").ToString()).ToString();
            //    txtLOSS.Value = (txtROLLLOSSM.Value / 100) * 100;
            //}
            //if (dtSum.Compute("SUM(LOSSPNL)", "").ToString() != "")
            //{
            //    txtRollSize.Text = decimal.Parse(dtSum.Compute("SUM(LOSSPNL)", "").ToString()).ToString();
            //}

            //if(txtAPPLYLOSSYSIZE.Value !=0)
            //{
            //    txtBASELOSS.Value = ((txtCOMPONENTQTY.Value * txtSUMMARY.Value) / txtAPPLYLOSSYSIZE.Value) * 100;
            //}
            //else
            //{
            //    txtBASELOSS.Value = 0;
            //}


            //GetControlsFrom cf = new GetControlsFrom();
            //cf.GetControlsFromGrid(smartSplitTableLayoutPanelRollRouting, grdRollLoss);

            //DataTable dtRollLoss = grdRollLoss.GetChangedRows();

            //if (dtRollLoss.Rows.Count != 0)
            //{
            //    dtRollLoss.TableName = "rollloss";
            //    DataTable dt = new DataTable();
            //    dt.TableName = "temp";
            //    DataSet ds = new DataSet();
            //    ds.Tables.Add(dtRollLoss);
            //    ds.Tables.Add(dt);
            //    ExecuteRule("Rollloss", ds);
            //}

            //DataTable dtRollLossOperation = grdRollLossOperation.GetChangedRows(); 

            //if (dtRollLossOperation.Rows.Count != 0)
            //{
            //    ExecuteRule("RollLossOperation", dtRollLossOperation);
            //}


            //DataTable Qtimedefinition = new DataTable();
            //Qtimedefinition = grdQtimedefinition.GetChangedRows();

            //if (Qtimedefinition.Rows.Count != 0)
            //{
            //    ExecuteRule("QtimeDefinition", Qtimedefinition);

            //}

            //DataTable QtimeAction = grdQtimeAction.GetChangedRows();

            //if (QtimeAction.Rows.Count != 0)
            //{
            //    ExecuteRule("QtimeAction", QtimeAction);
            //}
        }

        #endregion

        #region 검색

        /// <summary>
        ///     비동기 override 모델
        /// </summary>
        protected override async Task OnSearchAsync()
        {
            await base.OnSearchAsync();


            //그리드 초기화
            var dtRo = (DataTable) grdRollLossOperation.DataSource;
            if (dtRo != null) dtRo.Clear();

            var dtRr = (DataTable) grdRoutingRollLoss.DataSource;
            if (dtRr != null) dtRr.Clear();


            var values = Conditions.GetValues();
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);

            var dtS = await QueryAsync("GetRoutingRollLossList", "10002", values);

            grdRoutingRollLoss.DataSource = dtS;

            grdRoutingRollLoss.View.FocusedRowHandle = focusedRowHandle;

            var dataRow = grdRoutingRollLoss.View.GetFocusedDataRow();


            if (dtS.Rows.Count != 0)
            {
                var setitem = new SetControlsFrom();
                setitem.SetControlsFromRow(smartSplitTableLayoutPanelRollRouting, dataRow);
                //setitem.SetControlsFromRow(smartSplitTableLayoutPanel2, dataRow);

                //txtCOMPONENTQTY.Value = decimal.Parse(dataRow["COMPONENTQTY"].ToString());
                //txtAPPLYLOSSYSIZE.Value = txtCOMPONENTQTY.Value * txtSUMMARY.Value;

                //txtBASELOSS.Value = (txtAPPLYLOSSYSIZE.Value / txtPanelSizeY.Value) * 100;

                txtCOMPONENTQTY.Value = decimal.Parse(dataRow["COMPONENTQTY"].ToString());

                txtAPPLYLOSSYSIZE.Value = txtCOMPONENTQTY.Value * txtSUMMARY.Value;
                if (txtAPPLYLOSSYSIZE.Value != 0 && txtPanelSizeY.Value != 0)
                    txtBASELOSS.Value = txtAPPLYLOSSYSIZE.Value * 1000 / txtPanelSizeY.Value * 100;
                else
                    txtBASELOSS.Value = 0;

                //Dictionary<string, object> ParamRo = new Dictionary<string, object>();
                //ParamRo.Add("ENTERPRISEID", dataRow["ENTERPRISEID"].ToString());
                //ParamRo.Add("PARENTS_ROOL", dataRow["PARENTS_ROOL"].ToString());

                //ParamRo.Add("USERSEQUENCE_ROLL", dataRow["USERSEQUENCE_ROLL"].ToString());
                //ParamRo.Add("COMPONENTBOMID_ROLL", dataRow["COMPONENTBOMID_ROLL"].ToString());


                //dtRo = SqlExecuter.Query("GetRolllossOperationList", "10001", ParamRo);
                //grdRollLossOperation.DataSource = dtRo;
            }
        }

        #endregion


        #region 유효성 검사

        /// <summary>
        ///     데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            var dtProcessSegmentClass = new DataTable();
            var dtgrdProcessSegment = new DataTable();
            var dtgrdProSegMEC = new DataTable();
            var dtgrdSpecattribute = new DataTable();
        }

        #endregion

        #region Local Variables

        private readonly DataTable dttreeSet = new DataTable();
        private string sASSEMBLYROUTINGID = "";

        private readonly bool isNewData = false; //Roll Loss Operation  신규 데이터여부확인

        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        ///     그리드 초기화
        /// </summary>
        private void InitializeGridList()
        {
            // RollItem
            grdRoutingRollLoss.GridButtonItem = GridButtonItem.None;
            grdRoutingRollLoss.View.AddTextBoxColumn("PRODUCTDEFID", 100).SetIsReadOnly();
            grdRoutingRollLoss.View.AddTextBoxColumn("PRODUCTDEFVERSION", 100).SetIsReadOnly();
            grdRoutingRollLoss.View.AddTextBoxColumn("PRODUCTDEFNAME", 250).SetIsReadOnly();
            //grdRoutingRollLoss.View.AddTextBoxColumn("ASSEMBLYITEMUOMDEFID").SetIsHidden();
            grdRoutingRollLoss.View.AddTextBoxColumn("PLANTID").SetIsHidden();
            grdRoutingRollLoss.View.AddTextBoxColumn("MATERIALDEFID").SetIsHidden();
            grdRoutingRollLoss.View.AddTextBoxColumn("MATERIALDEFVERSION").SetIsHidden();
            grdRoutingRollLoss.View.AddTextBoxColumn("MATERIALDEFNAME").SetIsHidden();
            grdRoutingRollLoss.View.AddTextBoxColumn("PCSPNL").SetIsHidden();
            grdRoutingRollLoss.View.AddTextBoxColumn("PCSMM").SetIsHidden();
            grdRoutingRollLoss.View.AddTextBoxColumn("PNLSIZEYAXIS").SetIsHidden();
            grdRoutingRollLoss.View.AddTextBoxColumn("PNLSIZEXAXIS").SetIsHidden();
            grdRoutingRollLoss.View.AddTextBoxColumn("COMPONENTQTY").SetIsHidden();

            grdRoutingRollLoss.View.PopulateColumns();

            //// Roll
            //grdRollLoss.View.AddTextBoxColumn("OPERATIONID", 100);
            //grdRollLoss.View.AddTextBoxColumn("ROLLLOSSID", 100);
            //grdRollLoss.View.AddTextBoxColumn("ENTERPRISEID", 100);
            //grdRollLoss.View.AddTextBoxColumn("PLANTID", 100);
            //grdRollLoss.View.AddTextBoxColumn("PNL", 100);
            //grdRollLoss.View.AddTextBoxColumn("ROLLLOSSM", 100);
            //grdRollLoss.View.AddTextBoxColumn("LOSS", 100);
            //grdRollLoss.View.AddTextBoxColumn("BASELOSS", 100);
            //grdRollLoss.View.AddTextBoxColumn("APPLYLOSSYSIZE", 100);
            //grdRollLoss.View.AddTextBoxColumn("PNLSIZEX", 100);
            //grdRollLoss.View.AddTextBoxColumn("PNLSIZEY", 100);
            //grdRollLoss.View.AddTextBoxColumn("SUMMARY", 100);
            //grdRollLoss.View.AddTextBoxColumn("CALCULATION", 100);
            //grdRollLoss.View.AddTextBoxColumn("LASTSEQUENCE", 100);
            //grdRollLoss.View.AddTextBoxColumn("DESCRIPTION", 100);
            //grdRollLoss.View.AddTextBoxColumn("VALIDSTATE", 100);
            //grdRollLoss.View.AddTextBoxColumn("COMPONENTBOMID", 100);

            //grdRollLoss.View.PopulateColumns();

            // grdRollLossOperation
            grdRollLossOperation.GridButtonItem = GridButtonItem.None;

            grdRollLossOperation.View.AddTextBoxColumn("OPERATIONID", 100).SetIsHidden();
            grdRollLossOperation.View.AddTextBoxColumn("OPERATIONSEQUENCE", 100).SetIsHidden();
            grdRollLossOperation.View.AddTextBoxColumn("ROLLLOSSID", 100).SetIsHidden();
            grdRollLossOperation.View.AddTextBoxColumn("ENTERPRISEID", 100).SetIsHidden();
            grdRollLossOperation.View.AddTextBoxColumn("PLANTID", 100).SetIsHidden();
            grdRollLossOperation.View.AddTextBoxColumn("USERSEQUENCE", 100).SetIsReadOnly();
            grdRollLossOperation.View.AddTextBoxColumn("PROCESSSEGMENTID", 100).SetIsReadOnly();
            grdRollLossOperation.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 250).SetIsReadOnly();
            grdRollLossOperation.View.AddSpinEditColumn("LOSSM", 100)
                .SetDisplayFormat("##0.###").SetIsReadOnly();
            grdRollLossOperation.View.AddSpinEditColumn("LOSSPNL", 100)
                .SetDisplayFormat("##0.###");
            grdRollLossOperation.View.AddTextBoxColumn("CREATOR", 100).SetIsHidden();
            grdRollLossOperation.View.AddTextBoxColumn("CREATEDTIME", 100).SetIsHidden();
            grdRollLossOperation.View.AddTextBoxColumn("MODIFIER", 100).SetIsHidden();
            grdRollLossOperation.View.AddTextBoxColumn("MODIFIEDTIME", 100).SetIsHidden();
            grdRollLossOperation.View.AddTextBoxColumn("MATERIALID", 100).SetIsHidden();
            grdRollLossOperation.View.AddComboBoxColumn("VALIDSTATE", 60,
                new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState",
                    $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsHidden();
            grdRollLossOperation.View.PopulateColumns();
        }


        /// <summary>
        ///     Area Tree 초기화
        /// </summary>
        private void InitializeTreeArea()
        {
        }

        /// <summary>
        ///     설정 초기화
        /// </summary>
        protected override void InitializeContent()
        {
            InitializeGridList();
            InitializeEvent();


            //grdRollLoss.Hide();
            //Uom
            //cboUom.DisplayMember = "UOMDEFNAME";
            //cboUom.ValueMember = "UOMDEFID";
            //cboUom.ShowHeader = false;
            //Dictionary<string, object> ParamUom = new Dictionary<string, object>();
            //ParamUom.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            //ParamUom.Add("PLANTID", UserInfo.Current.Plant);
            //DataTable dtUom = SqlExecuter.Query("GetUOMList", "10001", ParamUom);
            //cboUom.DataSource = dtUom;

            //Site
            //cboSite.DisplayMember = "PLANTNAME";
            //cboSite.ValueMember = "PLANTID";
            //cboSite.ShowHeader = false;
            //var ParamSite = new Dictionary<string, object>();
            //ParamSite.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            //ParamSite.Add("PLANTID", UserInfo.Current.Plant);
            //ParamSite.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            //var dtSite = SqlExecuter.Query("GetPlantList", "10002", ParamSite);
            //cboSite.DataSource = dtSite;

            //// 유효상태
            //cboValidState.DisplayMember = "CODENAME";
            //cboValidState.ValueMember = "CODEID";
            //cboValidState.ShowHeader = false;
            //Dictionary<string, object> ParamValidState = new Dictionary<string, object>();
            //ParamValidState.Add("CODECLASSID", "ValidState");
            //ParamValidState.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            //DataTable dtValidState = SqlExecuter.Query("GetCodeList", "00001", ParamValidState);
            //cboValidState.DataSource = dtValidState;
        }

        #endregion

        #region 이벤트

        /// <summary>
        ///     이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            //grdRollLoss.View.AddingNewRow += grdRollLoss_AddingNewRow;
            //grdRollLossOperation.View.DataSourceChanged+= grdRollLossOperation_DataSourceChanged;
            grdRoutingRollLoss.View.FocusedRowChanged += grdRoutingRollLoss_FocusedRowChanged;


            //grdRollLossOperation.View.ValidateRow += grdRollLossOperation_ValidateRow;
            grdRollLossOperation.View.CellValueChanged += grdRollLossOperation_CellValueChanged;

            txtROLLLOSSM.ValueChanged += txtROLLLOSSM_ValueChanged;
            txtRollSize.ValueChanged += txtRollSize_ValueChanged;
        }

        private void grdRollLossOperation_DataSourceChanged(object sender, EventArgs e)
        {
            //DataRow dataRow = grdRoutingRollLoss.View.GetFocusedDataRow();
            //DataRow dataRowRolloss = grdRollLoss.View.GetFocusedDataRow();
        }

        private void txtRollSize_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtROLLLOSSM.Value == 0 && txtRollSize.Value == 0)
                    return;
                //txtLOSS.Value = (txtRollSize.Value / txtROLLLOSSM.Value) * 100;
                txtLOSS.Value = txtROLLLOSSM.Value / 100 * 100;
            }
            catch (Exception ex)
            {
                MessageException.Create(ex.ToString());
            }
        }

        private void grdRollLossOperation_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            try
            {
                if (!e.Column.FieldName.Equals("LOSSPNL"))
                {
                    double lossMSummary = 0;
                    double lossPnlSummary = 0;
                    foreach (DataRowView dataRow in (DataView) grdRollLossOperation.View.DataSource)
                    {
                        lossMSummary = lossMSummary + double.Parse(string.IsNullOrEmpty(dataRow["LOSSM"].ToString())
                                           ? "0"
                                           : dataRow["LOSSM"].ToString());
                        lossPnlSummary = lossPnlSummary +
                                         double.Parse(string.IsNullOrEmpty(dataRow["LOSSPNL"].ToString())
                                             ? "0"
                                             : dataRow["LOSSPNL"].ToString());
                    }

                    txtROLLLOSSM.Value = lossMSummary.ToDecimal();
                    txtRollSize.Value = lossPnlSummary.ToDecimal();
                }
                else
                {
                    var lossM = txtCOMPONENTQTY.Value * txtSUMMARY.Value * decimal.Parse(e.Value.ToString());

                    grdRollLossOperation.View.SetRowCellValue(e.RowHandle, "LOSSM", lossM);
                }
            }
            catch (Exception ex)
            {
                MessageException.Create(ex.Message);
            }
        }

        private void txtROLLLOSSM_ValueChanged(object sender, EventArgs e)
        {
            if (txtROLLLOSSM.Value != 0)
                txtLOSS.Value = txtROLLLOSSM.Value / 100 * 100;
            else
                txtLOSS.Value = 0;
        }


        private void grdRollLossOperation_ValidateRow(object sender, ValidateRowEventArgs e)
        {
            var dt = (DataTable) grdRollLossOperation.DataSource;

            if (dt.Compute("SUM(LOSSM)", "").ToString() != "")
            {
                txtROLLLOSSM.Text = decimal.Parse(dt.Compute("SUM(LOSSM)", "").ToString()).ToString();
                txtLOSS.Value = txtROLLLOSSM.Value / 100 * 100;
            }

            //if (dt.Compute("SUM(LOSSPNL)", "").ToString() != "")
            //{
            //    txtRollSize.Text = decimal.Parse(dt.Compute("SUM(LOSSPNL)", "").ToString()).ToString();
            //}
        }


        private void grdRollLossOperation_AddingNewRow(SmartBandedGridView sender, AddNewRowArgs args)
        {
            if (grdRoutingRollLoss.View.FocusedRowHandle < 0)
                return;

            var dataRow = grdRoutingRollLoss.View.GetFocusedDataRow();
            //DataRow dataRowRolloss = grdRollLoss.View.GetFocusedDataRow();

            args.NewRow["ENTERPRISEID"] = dataRow["ENTERPRISEID"];
            args.NewRow["OPERATIONID"] = dataRow["OPERATIONID"];
            args.NewRow["PLANTID"] = dataRow["PLANTID"];
            args.NewRow["COMPONENTBOMID"] = dataRow["COMPONENTBOMID_ROLL"];
            //args.NewRow["ROLLLOSSID"] = dataRowRolloss["ROLLLOSSID"];
            args.NewRow["LOSSM"] = 0;
            args.NewRow["LOSSPNL"] = 0;
            args.NewRow["VALIDSTATE"] = "Valid";


            tbCreator.Text = args.NewRow["CREATOR"].ToString();
            tbRegistDate.Text = args.NewRow["CREATEDTIME"].ToString();
            tbModifier.Text = string.IsNullOrEmpty(args.NewRow["MODIFIER"].ToString())
                ? string.Empty
                : args.NewRow["MODIFIER"].ToString();
            tbModifyDate.Text = string.IsNullOrEmpty(args.NewRow["MODIFIEDTIME"].ToString())
                ? string.Empty
                : args.NewRow["MODIFIEDTIME"].ToString();
        }

        private void grdRoutingRollLoss_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (grdRoutingRollLoss.View.FocusedRowHandle < 0)
                return;

            if (grdRollLossOperation.GetChangedRows().Rows.Count > 0)
            {
                DialogResult result = ShowMessage(MessageBoxButtons.YesNo, "WRITTENBEDELETE");

                switch (result)
                {
                    case DialogResult.Yes:
                        break;
                    case DialogResult.No:
                        grdRoutingRollLoss.View.FocusedRowHandle = e.PrevFocusedRowHandle;
                        return;
                }
            }

            var dataRow = grdRoutingRollLoss.View.GetFocusedDataRow();

            var setitem = new SetControlsFrom();
            setitem.InitializeControl(smartSplitTableLayoutPanelRollRouting);
            txtRollSize.Value = 0;
            txtROLLLOSSM.Value = 0;

            setitem.SetControl(smartSplitTableLayoutPanelRollRouting, dataRow);

            //setitem.SetControlsFromRow(smartSplitTableLayoutPanel2, dataRow);

            //Dictionary<string, object> ParamComp = new Dictionary<string, object>();
            //ParamComp.Add("ENTERPRISEID", dataRow["ENTERPRISEID"].ToString());
            //ParamComp.Add("OPERATIONID", dataRow["OPERATIONID"].ToString());
            //DataTable dtRollLoss = SqlExecuter.Query("GetRolllossList", "10001", ParamComp);
            //grdRollLoss.DataSource = dtRollLoss;

            //if (dtRollLoss.Rows.Count == 0)
            //{
            //    grdRollLoss.View.AddNewRow();
            //}

            //foreach (DataRow row in dtRollLoss.Rows)
            //{
            //    Dictionary<string, object> ParamLastSequence = new Dictionary<string, object>();
            //    ParamLastSequence.Add("ENTERPRISEID", dataRow["ENTERPRISEID"].ToString());
            //    ParamLastSequence.Add("ASSEMBLYITEMID", dataRow["PRODUCTDEFID"].ToString());
            //    ParamLastSequence.Add("ASSEMBLYITEMVERSION", dataRow["PRODUCTDEFVERSION"].ToString());
            //    DataTable dtLastSequence = SqlExecuter.Query("GetLastSequenceList", "10001", ParamLastSequence);


            //    if (dtLastSequence.Rows.Count != 0)
            //    {
            //        row["LASTSEQUENCE"] = dtLastSequence.Rows[0]["USERSEQUENCE_ROLL"].ToString();
            //    }

            //    row["PNLSIZEX"] = dataRow["PNLSIZEXAXIS"];
            //    row["PNLSIZEY"] = dataRow["PNLSIZEYAXIS"];
            //    row["SUMMARY"] = dataRow["PCSPNL"];
            //    row["CALCULATION"] = dataRow["PCSMM"];
            //    dtLastSequence.Columns.Add("COMPONENTQTY",typeof(decimal));

            //}

            //setitem.SetControl(smartSplitTableLayoutPanelRollRouting, dtRollLoss);

            var ParamRo = new Dictionary<string, object>();
            //ParamRo.Add("ENTERPRISEID", dataRow["ENTERPRISEID"].ToString());
            //ParamRo.Add("OPERATIONID", dataRow["OPERATIONID"].ToString());
            //ParamRo.Add("OPERATIONSEQUENCE", dataRow["OPERATIONSEQUENCE"].ToString());
            //DataTable dtRo = SqlExecuter.Query("GetRolllossOperationList", "10001", ParamRo);

            ParamRo.Add("ENTERPRISEID", dataRow["ENTERPRISEID"].ToString());
            ParamRo.Add("PRODUCTDEFID", dataRow["PRODUCTDEFID"].ToString());
            ParamRo.Add("PRODUCTDEFVERSION", dataRow["PRODUCTDEFVERSION"].ToString());
            ParamRo.Add("USERSEQUENCE", dataRow["USERSEQUENCE"].ToString());

            ParamRo.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            var dtRo = SqlExecuter.Query("GetRolllossOperationList", "10003", ParamRo);

            grdRollLossOperation.DataSource = dtRo;

            var focusedDataRow = grdRollLossOperation.View.GetFocusedDataRow();

            double lossMSummary = 0;
            double lossPnlSummary = 0;
            foreach (DataRowView ROLdataRow in (DataView)grdRollLossOperation.View.DataSource)
            {
                lossMSummary = lossMSummary + double.Parse(string.IsNullOrEmpty(ROLdataRow["LOSSM"].ToString())
                                   ? "0"
                                   : ROLdataRow["LOSSM"].ToString());
                lossPnlSummary = lossPnlSummary +
                                 double.Parse(string.IsNullOrEmpty(ROLdataRow["LOSSPNL"].ToString())
                                     ? "0"
                                     : ROLdataRow["LOSSPNL"].ToString());
            }

            txtROLLLOSSM.Value = lossMSummary.ToDecimal();
            txtRollSize.Value = lossPnlSummary.ToDecimal();

            txtAPPLYLOSSYSIZE.Value = txtCOMPONENTQTY.Value * txtSUMMARY.Value;
            if (txtAPPLYLOSSYSIZE.Value != 0 && txtPanelSizeY.Value != 0)
                txtBASELOSS.Value = txtAPPLYLOSSYSIZE.Value * 1000 / txtPanelSizeY.Value * 100;
            else
                txtBASELOSS.Value = 0;
            //args.NewRow["ENTERPRISEID"] = dataRow["ENTERPRISEID"];
            //args.NewRow["OPERATIONID"] = dataRow["OPERATIONID"];
            //args.NewRow["PLANTID"] = dataRow["PLANTID"];
            //args.NewRow["COMPONENTBOMID"] = dataRow["COMPONENTBOMID_ROLL"];
            ////args.NewRow["ROLLLOSSID"] = dataRowRolloss["ROLLLOSSID"];
            //args.NewRow["LOSSM"] = 0;
            //args.NewRow["LOSSPNL"] = 0;
            //args.NewRow["VALIDSTATE"] = "Valid";

            if (focusedDataRow == null)
                return;
            //tbCreator.Text = string.IsNullOrEmpty(focusedDataRow["CREATOR"].ToString())
            //    ? string.Empty
            //    : focusedDataRow["CREATOR"].ToString();
            //tbRegistDate.Text = string.IsNullOrEmpty(focusedDataRow["CREATEDTIME"].ToString())
            //    ? string.Empty
            //    : focusedDataRow["CREATEDTIME"].ToString();
            //tbModifier.Text = string.IsNullOrEmpty(focusedDataRow["MODIFIER"].ToString())
            //    ? string.Empty
            //    : focusedDataRow["MODIFIER"].ToString();
            //tbModifyDate.Text = string.IsNullOrEmpty(focusedDataRow["MODIFIEDTIME"].ToString())
            //    ? string.Empty
            //    : focusedDataRow["MODIFIEDTIME"].ToString();
        }


        private void grdRollLoss_AddingNewRow(SmartBandedGridView sender, AddNewRowArgs args)
        {
            var dataRow = grdRoutingRollLoss.View.GetFocusedDataRow();


            args.NewRow["ENTERPRISEID"] = dataRow["ENTERPRISEID"];
            args.NewRow["OPERATIONID"] = dataRow["OPERATIONID"];
            args.NewRow["PLANTID"] = dataRow["PLANTID"];

            // 채번 시리얼 존재 유무 체크
            // 오늘날짜.
            var paramdt = new Dictionary<string, object>();
            var dtDate = SqlExecuter.Query("GetItemId", "10001", paramdt);
            var sdate = dtDate.Rows[0]["ITEMID"].ToString().Substring(0, 8);

            var parampf = new Dictionary<string, object>();
            parampf.Add("IDCLASSID", "RollLoss");
            parampf.Add("PREFIX", sdate);

            var dtItemserialChk = SqlExecuter.Query("GetProductitemserial", "10001", parampf);

            var dtItemserialI = dtItemserialChk.Clone();
            dtItemserialI.Columns.Add("_STATE_");


            if (dtItemserialChk != null)
            {
                if (dtItemserialChk.Rows.Count == 0)
                {
                    var rowItemserialI = dtItemserialI.NewRow();
                    rowItemserialI["IDCLASSID"] = "RollLoss";
                    rowItemserialI["PREFIX"] = sdate;
                    rowItemserialI["LASTSERIALNO"] = "00001";
                    rowItemserialI["_STATE_"] = "added";
                    dtItemserialI.Rows.Add(rowItemserialI);
                }
                else
                {
                    var rowItemserialI = dtItemserialI.NewRow();
                    rowItemserialI["IDCLASSID"] = "RollLoss";
                    rowItemserialI["PREFIX"] = sdate;

                    var ilastserialno = 0;
                    ilastserialno = int.Parse(dtItemserialChk.Rows[0]["LASTSERIALNO"].ToString());
                    ilastserialno = ilastserialno + 1;


                    rowItemserialI["LASTSERIALNO"] =
                        ("0000" + ilastserialno).Substring(("0000" + ilastserialno).Length - 5, 5);
                    rowItemserialI["_STATE_"] = "modified";
                    dtItemserialI.Rows.Add(rowItemserialI);
                }
            }
            else
            {
                var rowItemserialI = dtItemserialI.NewRow();
                rowItemserialI["IDCLASSID"] = "RollLoss";
                rowItemserialI["PREFIX"] = sdate;
                rowItemserialI["LASTSERIALNO"] = "00001";
                rowItemserialI["_STATE_"] = "added";
                dtItemserialI.Rows.Add(rowItemserialI);
            }

            dtItemserialI.TableName = "idclassserial";
            var dtemp = new DataTable();
            dtemp.TableName = "temp";
            var dsChang = new DataSet();
            dsChang.Tables.Add(dtItemserialI);
            dsChang.Tables.Add(dtemp);

            ExecuteRule("Rollloss", dsChang);


            args.NewRow["ROLLLOSSID"] =
                dtItemserialI.Rows[0]["PREFIX"] + dtItemserialI.Rows[0]["LASTSERIALNO"].ToString();
            args.NewRow["VALIDSTATE"] = "Valid";
        }

        #endregion

        #region 조회조건 영역

        /// <summary>
        ///     조회조건 영역 초기화 시작
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            InitializeCondition_Popup();
            // 버전
        }


        /// <summary>
        ///     조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            Conditions.GetControl<SmartTextBox>("P_ITEMVERSION").ReadOnly = true;
            Conditions.GetControl<SmartSelectPopupEdit>("ITEMID").EditValueChanged += ProductDefIDChanged;
        }

        private void ProductDefIDChanged(object sender, EventArgs e)
        {
            var PopProdutid = sender as SmartSelectPopupEdit;

            if (Format.GetFullTrimString(PopProdutid.EditValue).Equals(string.Empty))
                Conditions.GetControl<SmartTextBox>("P_ITEMVERSION").EditValue = string.Empty;
        }

        /// <summary>
        ///     검색조건 팝업 예제
        /// </summary>
        private void InitializeCondition_Popup()
        {
            //팝업 컬럼 설정
            var parentPopupColumn = Conditions.AddSelectPopup("ITEMID",
                    new SqlQuery("GetProductItemGroup", "10003", $"ENTERPRISEID={UserInfo.Current.Enterprise}",
                        $"PLANTID={UserInfo.Current.Plant}"), "ITEMID", "ITEMID")
                .SetPopupLayout("ITEMID", PopupButtonStyles.Ok_Cancel, true, false) //팝업창 UI 설정.
                .SetPopupResultCount() //팝업창 선택가능한 개수
                .SetPosition(3.1)
                .SetPopupLayoutForm(Width=800, Height = 800)
                .SetPopupApplySelection((selectRow, gridRow) =>
                {
                    var productRevisionList = new List<string>();

                    selectRow.AsEnumerable().ForEach(r =>
                    {
                        productRevisionList.Add(Format.GetString(r["ITEMVERSION"]));
                    });

                    Conditions.GetControl<SmartTextBox>("P_ITEMVERSION").EditValue =
                        string.Join(",", productRevisionList);
                });
            parentPopupColumn.Conditions.AddComboBox("MASTERDATACLASSID",
                new SqlQuery("GetmasterdataclassList", "10001", $"ITEMOWNER={"Specifications"}",
                    $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "MASTERDATACLASSNAME", "MASTERDATACLASSID");
            parentPopupColumn.Conditions.AddTextBox("ITEMID");
            parentPopupColumn.Conditions.AddTextBox("ITEMNAME");

            //팝업 그리드
            parentPopupColumn.GridColumns.AddTextBoxColumn("MASTERDATACLASSNAME", 70)
                .SetTextAlignment(TextAlignment.Center);
            parentPopupColumn.GridColumns.AddTextBoxColumn("ITEMID", 110)
                .SetTextAlignment(TextAlignment.Center);
            parentPopupColumn.GridColumns.AddTextBoxColumn("ITEMVERSION", 70)
                .SetTextAlignment(TextAlignment.Center);
            parentPopupColumn.GridColumns.AddTextBoxColumn("ITEMNAME", 300);
            //parentPopupColumn.GridColumns.AddTextBoxColumn("SPEC", 250);
        }

        #endregion

        #region private Fuction

        /// <summary>
        ///     팝업 컬럼에서 확인 클릭 시 다른 데이터 매핑 (표준공정)
        /// </summary>
        /// <param name="currentGridRow">화면 grid row</param>
        /// <param name="popupSelections">팝업에서 선택한 row</param>
        /// <returns></returns>
        private ValidationResultCommon ValidationProcesssegMnetPopup(DataRow currentGridRow,
            IEnumerable<DataRow> popupSelections)
        {
            var result = new ValidationResultCommon();

            foreach (var row in popupSelections) currentGridRow["PROCESSSEGMENTNAME"] = row["PROCESSSEGMENTNAME"];
            return result;
        }

        /// <summary>
        ///     팝업 컬럼에서 확인 클릭 시 다른 데이터 매핑 ( 작업장)
        /// </summary>
        /// <param name="currentGridRow">화면 grid row</param>
        /// <param name="popupSelections">팝업에서 선택한 row</param>
        /// <returns></returns>
        private ValidationResultCommon ValidationAreaPopup(DataRow currentGridRow, IEnumerable<DataRow> popupSelections)
        {
            var result = new ValidationResultCommon();

            foreach (var row in popupSelections) currentGridRow["AREANAME"] = row["AREANAME"];
            return result;
        }


        /// <summary>
        ///     팝업 컬럼에서 확인 클릭 시 다른 데이터 매핑 ( 작업장)
        /// </summary>
        /// <param name="currentGridRow">화면 grid row</param>
        /// <param name="popupSelections">팝업에서 선택한 row</param>
        /// <returns></returns>
        private ValidationResultCommon ValidationProcesssegMnetToPopup(DataRow currentGridRow,
            IEnumerable<DataRow> popupSelections)
        {
            var result = new ValidationResultCommon();

            foreach (var row in popupSelections)
            {
                currentGridRow["TOPROCESSDEFID"] = row["ASSEMBLYITEMID"];
                currentGridRow["TOPROCESSDEFVERSION"] = row["ASSEMBLYITEMVERSION"];
                currentGridRow["TOPROCESSSEGMENTID"] = row["PROCESSSEGMENTID"];
                currentGridRow["TOPROCESSSEGMENTNAME"] = row["PROCESSSEGMENTNAME"];
            }

            return result;
        }

        /// <summary>
        ///     팝업 컬럼에서 확인 클릭 시 다른 데이터 매핑 ( 작업장)
        /// </summary>
        /// <param name="currentGridRow">화면 grid row</param>
        /// <param name="popupSelections">팝업에서 선택한 row</param>
        /// <returns></returns>
        private ValidationResultCommon ValidationProcesssegMnetFromPopup(DataRow currentGridRow,
            IEnumerable<DataRow> popupSelections)
        {
            var result = new ValidationResultCommon();

            foreach (var row in popupSelections)
            {
                var rowP = dttreeSet.Select("MASTERDATACLASSID = 'Product'");

                currentGridRow["PLANTID"] = row["PLANTID"];
                currentGridRow["AREAID"] = row["AREAID"];
                currentGridRow["PRODUCTDEFID"] = rowP[0]["ASSEMBLYITEMID"];
                currentGridRow["PRODUCTDEFVERSION"] = rowP[0]["ASSEMBLYITEMVERSION"];
                currentGridRow["USERSEQUENCE"] = row["USERSEQUENCE"];
                currentGridRow["PROCESSDEFID"] = row["ASSEMBLYITEMID"];
                currentGridRow["PROCESSDEFVERSION"] = row["ASSEMBLYITEMVERSION"];
                //currentGridRow["PROCESSPATHID"] = row["USERSEQUENCE"];
                currentGridRow["PROCESSSEGMENTID"] = row["PROCESSSEGMENTID"];
                currentGridRow["PROCESSSEGMENTNAME"] = row["PROCESSSEGMENTNAME"];
            }

            return result;
        }

        #endregion
    }
}
