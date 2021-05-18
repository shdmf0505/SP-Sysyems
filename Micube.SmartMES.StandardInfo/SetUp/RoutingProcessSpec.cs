#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion using

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프로그램명 : 기준정보 > Setup > 공정 측정 SPEC 등록
    /// 업 무 설명 : 품목별 측정 SPEC 등록
    /// 생  성  자 : 한주석
    /// 생  성  일 : 2019-06-27
    /// 수정 이 력 :
    /// </summary>
    public partial class RoutingProcessSpec : SmartConditionManualBaseForm
    {
        #region Local Variables

        private int lastFocusedOperationRowIndex = 0;

        #endregion Local Variables

        #region 생성자

        public RoutingProcessSpec()
        {
            InitializeComponent();
        }

        #endregion 생성자

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 설정 초기화
        /// </summary>
        protected override void InitializeContent()
        {
            InitializeGridList();
            InitializeControler();
            InitializeEvent();
        }

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGridList()
        {
            #region 공정

            grdOperation.GridButtonItem = GridButtonItem.Export | GridButtonItem.Import;

            grdOperation.View.AddTextBoxColumn("ITEMID", 120).SetIsReadOnly();
            grdOperation.View.AddTextBoxColumn("ITEMVERSION", 60).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdOperation.View.AddTextBoxColumn("PLANTID", 60).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdOperation.View.AddTextBoxColumn("USERSEQUENCE", 60).SetIsReadOnly().SetTextAlignment(TextAlignment.Right);
            grdOperation.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 200).SetIsReadOnly();
            grdOperation.View.AddTextBoxColumn("PROCESSUOM", 70).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdOperation.View.AddTextBoxColumn("DESCRIPTION", 200).SetIsReadOnly().SetLabel("COMMENT");
            grdOperation.View.AddTextBoxColumn("ISREQUIREDOPERATIONSPEC", 80).SetTextAlignment(TextAlignment.Center);
            grdOperation.View.AddTextBoxColumn("CREATOR", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdOperation.View.AddTextBoxColumn("CREATEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdOperation.View.AddTextBoxColumn("MODIFIER", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdOperation.View.AddTextBoxColumn("MODIFIEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly().SetTextAlignment(TextAlignment.Center);

            grdOperation.View.PopulateColumns();

            #endregion 공정

            #region 공정 Spec

            grdProcessSpec.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export | GridButtonItem.Import;

            var group = grdProcessSpec.View.AddGroupColumn("");

            #region 검사항목

            var popup = group.AddSelectPopupColumn("INSPITEMNAME", 200, new SqlQuery("GetSpecAttributelist", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}"
                                                                                                                    , $"LANGUAGETYPE={UserInfo.Current.LanguageType}"
                                                                                                                    , $"INSPECTIONCLASSID={"OperationInspection"}"), "INSPITEMNAME")
                             .SetLabel("INSPECTIONIDNAME")
                             .SetPopupLayout("INSPITEMID", PopupButtonStyles.Ok_Cancel, true, false)
                             .SetPopupResultCount(0)
                             .SetPopupResultMapping("INSPITEMNAME", "INSPITEMNAME")
                             .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow)
                             .SetPopupApplySelection((selectRows, gridRow) =>
                             {
                                 if (selectRows.Count() > 0)
                                 {
                                     DataTable dt2 = grdProcessSpec.DataSource as DataTable;
                                     int handle = grdProcessSpec.View.FocusedRowHandle;

                                     foreach (DataRow row in selectRows)
                                     {
                                         DataRow dr = null;
                                         if (dt2.Rows.Count < handle + 1)
                                         {
                                             dr = dt2.NewRow();
                                             dt2.Rows.Add(dr);
                                         }
                                         else
                                         {
                                             dr = grdProcessSpec.View.GetDataRow(handle);
                                         }

                                         dr["INSPITEMID"] = row["INSPITEMID"].ToString();
                                         dr["INSPITEMNAME"] = row["INSPITEMNAME"].ToString();
                                         dr["INSPITEMVERSION"] = row["INSPITEMVERSION"].ToString();
                                         dr["PROCESSSEGMENTID"] = grdOperation.View.GetFocusedDataRow()["PROCESSSEGMENTID"].ToString();
                                         dr["INSPECTIONDEFID"] = "OperationInspection-OperationInspection";
                                         dr["INSPECTIONDEFVERSION"] = "*";
                                         dr["SPECCLASSID"] = "OperationSpec";
                                         dr["DEFAULTCHARTTYPE"] = cboChartInfo.EditValue.ToString();
                                         dr["RESOURCETYPE"] = "ProcessSegmentID";

                                         grdProcessSpec.View.RaiseValidateRow(handle);

                                         handle++;
                                     }
                                 }
                             });

            popup.Conditions.AddTextBox("TXTINSPITEMNAME");
            popup.Conditions.AddTextBox("PROCESSSEGMENTID").SetIsHidden().SetPopupDefaultByGridColumnId("PROCESSSEGMENTID");

            popup.GridColumns.AddTextBoxColumn("INSPITEMID", 150);
            popup.GridColumns.AddTextBoxColumn("INSPITEMNAME", 200);

            #endregion 검사항목

            group.AddTextBoxColumn("LOCATION", 100);

            group = grdProcessSpec.View.AddGroupColumn("GROUPSPEC");
            group.AddSpinEditColumn("LSL", 100).SetLabel("LSL").SetDisplayFormat("#,##0.####");
            group.AddSpinEditColumn("SL", 100).SetLabel("STDVALUE").SetDisplayFormat("#,##0.####");
            group.AddSpinEditColumn("USL", 100).SetLabel("USL").SetDisplayFormat("#,##0.####");

            group = grdProcessSpec.View.AddGroupColumn("GROUPCONTROLLIMIT");
            group.AddSpinEditColumn("LCL", 100).SetLabel("LSL").SetDisplayFormat("#,##0.####");
            group.AddSpinEditColumn("CL", 100).SetLabel("STDVALUE").SetDisplayFormat("#,##0.####");
            group.AddSpinEditColumn("UCL", 100).SetLabel("USL").SetDisplayFormat("#,##0.####");

            group = grdProcessSpec.View.AddGroupColumn("GROUPOUTLIER");
            group.AddSpinEditColumn("LOL", 100).SetLabel("LSL").SetDisplayFormat("#,##0.####");
            group.AddSpinEditColumn("UOL", 100).SetLabel("USL").SetDisplayFormat("#,##0.####");

            group = grdProcessSpec.View.AddGroupColumn("");
            group.AddTextBoxColumn("CREATOR", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            group.AddTextBoxColumn("CREATEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            group.AddTextBoxColumn("MODIFIER", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            group.AddTextBoxColumn("MODIFIEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly().SetTextAlignment(TextAlignment.Center);

            grdProcessSpec.View.PopulateColumns();

            #endregion 공정 Spec
        }

        private void InitializeControler()
        {
            // UOM
            cboChartInfo.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboChartInfo.ShowHeader = false;
            cboChartInfo.ValueMember = "CODEID";
            cboChartInfo.DisplayMember = "CODENAME";
            cboChartInfo.UseEmptyItem = false;
            cboChartInfo.DataSource = SqlExecuter.Query("GetCodeList", "00001", new Dictionary<string, object>
                                                                                    {
                                                                                        { "CODECLASSID", "ControlType" },
                                                                                        { "LANGUAGETYPE", UserInfo.Current.LanguageType }
                                                                                    });
        }

        #endregion 컨텐츠 영역 초기화

        #region 이벤트

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
                Conditions.SetValue("P_PRODUCTDEFID", 0, parameters["ITEMID"]);
                Conditions.SetValue("P_PRODUCTDEFVERSION", 0, parameters["ITEMVERSION"]);
                Conditions.SetValue("P_PRODUCTNAME", 0, parameters["ITEMNAME"]);
                SendKeys.Send("{F5}");
            }
        }

        /// <summary>
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            grdOperation.View.RowCellStyle += (s, e) =>
            {
                if (e.RowHandle < 0)
                    return;

                if (!string.IsNullOrEmpty(grdOperation.View.GetRowCellValue(e.RowHandle, "ISREQUIREDOPERATIONSPEC").ToString()))
                {
                    e.Appearance.BackColor = Color.Yellow;
                    e.Appearance.ForeColor = Color.Blue;
                }
            };

            grdProcessSpec.View.AddingNewRow += (s, e) =>
            {
                if (grdOperation.View.FocusedRowHandle < 0)
                {
                    e.IsCancel = true;
                    return;
                }
                
                if (grdOperation.View.GetFocusedDataRow() is DataRow focusedRow)
                {
                    if (focusedRow.RowState.Equals(DataRowState.Added))
                    {
                        ShowMessage("NoSelectDataSaveAndProceed");
                        e.IsCancel = true;
                        return;
                    }
                }
                else
                {
                    e.IsCancel = true;
                    return;
                }

                e.NewRow["PROCESSSEGMENTID"] = grdOperation.View.GetFocusedDataRow()["PROCESSSEGMENTID"].ToString();
                e.NewRow["INSPECTIONDEFID"] = "OperationInspection-OperationInspection";
                e.NewRow["INSPECTIONDEFVERSION"] = "*";
                e.NewRow["SPECCLASSID"] = "OperationSpec";
                e.NewRow["DEFAULTCHARTTYPE"] = cboChartInfo.EditValue.ToString();
            };

            grdProcessSpec.View.ShowingEditor += (s, e) =>
            {
                string currentColumnName = grdProcessSpec.View.FocusedColumn.FieldName;

                if (currentColumnName.Equals("INSPITEMNAME"))
                {
                    e.Cancel = !grdProcessSpec.View.GetFocusedDataRow().RowState.Equals(DataRowState.Added);
                }
            };

            cboChartInfo.EditValueChanged += (s, e) =>
            {
                DataTable dt = grdProcessSpec.DataSource as DataTable;

                foreach (DataRow dr in dt.Rows)
                {
                    dr["DEFAULTCHARTTYPE"] = cboChartInfo.EditValue.ToString();
                }
            };

            grdOperation.View.FocusedRowChanged += grdOperationView_FocusedRowChanged;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdOperationView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            grdOperation.View.FocusedRowChanged -= grdOperationView_FocusedRowChanged;

            if(grdProcessSpec.DataSource == null)
            {
                grdOperation.View.FocusedRowChanged += grdOperationView_FocusedRowChanged;
                return;
            }

            if(grdProcessSpec.GetChangedRows() is DataTable dt)
            {
                if(dt.Rows.Count.Equals(0))
                {
                    SearchProcessSpec();
                }
                else
                {
                    if (ShowMessage(MessageBoxButtons.YesNo, "WRITTENBEDELETE") is DialogResult result)
                    {
                        switch (result)
                        {
                            case DialogResult.Yes:
                                SearchProcessSpec();
                                break;

                            case DialogResult.No:
                                grdOperation.View.FocusedRowHandle = e.PrevFocusedRowHandle;
                                break;
                        }
                    }
                }
            }

            grdOperation.View.FocusedRowChanged += grdOperationView_FocusedRowChanged;
        }

        #endregion 이벤트

        #region 툴바

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            DataTable dtSendData = new DataTable();

            string key = "processSpecList";
            dtSendData.Columns.Add("ENTERPRISEID");
            dtSendData.Columns.Add("PLANTID");
            dtSendData.Columns.Add("PRODUCTDEFID");
            dtSendData.Columns.Add("PRODUCTDEFVERSION");
            dtSendData.Columns.Add("PROCESSSEGMENTID");
            dtSendData.Columns.Add("PROCESSSEGMENTVERSION");
            dtSendData.Columns.Add("INSPITEMID");
            dtSendData.Columns.Add("INSPITEMNAME");
            dtSendData.Columns.Add("INSPITEMVERSION");
            dtSendData.Columns.Add("INSPECTIONDEFID");
            dtSendData.Columns.Add("INSPECTIONDEFVERSION");
            dtSendData.Columns.Add("RESOURCETYPE");

            dtSendData.Columns.Add("SPECCLASSID");
            dtSendData.Columns.Add("SPECSEQUENCE");
            dtSendData.Columns.Add("LOCATION");
            dtSendData.Columns.Add("DEFAULTCHARTTYPE");
            dtSendData.Columns.Add("LCL");
            dtSendData.Columns.Add("CL");
            dtSendData.Columns.Add("UCL");
            dtSendData.Columns.Add("LSL");
            dtSendData.Columns.Add("SL");
            dtSendData.Columns.Add("USL");
            dtSendData.Columns.Add("LOL");
            dtSendData.Columns.Add("UOL");
            dtSendData.Columns.Add("VALIDSTATE");
            dtSendData.Columns.Add("_STATE_");

            foreach (DataRow dr in grdProcessSpec.GetChangedRows().Rows)
            {
                dtSendData.Rows.Add(new object[]
                {
                    UserInfo.Current.Enterprise,
                    grdOperation.View.GetFocusedDataRow()["PLANTID"].ToString(),
                    grdOperation.View.GetFocusedDataRow()["ITEMID"].ToString(),
                    grdOperation.View.GetFocusedDataRow()["ITEMVERSION"].ToString(),
                    grdOperation.View.GetFocusedDataRow()["PROCESSSEGMENTID"].ToString(),
                    "*",
                    dr["INSPITEMID"].ToString(),
                    dr["INSPITEMNAME"].ToString(),
                    dr["INSPITEMVERSION"].ToString(),
                    dr["INSPECTIONDEFID"].ToString(),
                    dr["INSPECTIONDEFVERSION"].ToString(),
                    dr["RESOURCETYPE"].ToString(),
                    dr["SPECCLASSID"].ToString(),
                    dr["SPECSEQUENCE"].ToString(),
                    dr["LOCATION"].ToString(),
                    cboChartInfo.EditValue.ToString(),
                    dr["LCL"].ToString(),
                    dr["CL"].ToString(),
                    dr["UCL"].ToString(),
                    dr["LSL"].ToString(),
                    dr["SL"].ToString(),
                    dr["USL"].ToString(),
                    dr["LOL"].ToString(),
                    dr["UOL"].ToString(),
                    "Valid",
                    dr["_STATE_"].ToString()
                });
            }

            MessageWorker worker = new MessageWorker("RoutingProcessSpec");
            worker.SetBody(new MessageBody()
            {
                {  key, dtSendData}
            });

            worker.Execute();

            lastFocusedOperationRowIndex = grdOperation.View.FocusedRowHandle;
        }

        #endregion 툴바

        #region 검색

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            try
            {
                DialogManager.ShowWaitArea(this.pnlContent);

                await base.OnSearchAsync();

                var values = Conditions.GetValues();
                values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                values.Add("PLANTID", UserInfo.Current.Plant);
                values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                ClearData();

                if (await SqlExecuter.QueryAsync("GetProductDEFInfo", "10001", values) is DataTable dtProductDEF)
                {
                    if (dtProductDEF.Rows.Count.Equals(0))
                    {
                        // 조회할 데이터가 없습니다.
                        ShowMessage("NoSelectData");
                        return;
                    }

                    txtCutomerName.Text = dtProductDEF.Rows[0]["CUSTOMERNAME"].ToString();
                    txtProductDEFId.Text = dtProductDEF.Rows[0]["PRODUCTDEFID"].ToString();
                    txtProductDEFVersion.Text = dtProductDEF.Rows[0]["PRODUCTDEFVERSION"].ToString();
                    txtCompletionWareHouse.Text = dtProductDEF.Rows[0]["WAREHOUSENAME"].ToString();
                    txtProductDEFName.Text = dtProductDEF.Rows[0]["PRODUCTDEFNAME"].ToString();
                    txtWorkType.Text = dtProductDEF.Rows[0]["JOBTYPENAME"].ToString();
                    txtProductionType.Text = dtProductDEF.Rows[0]["PRODUCTIONTYPENAME"].ToString();

                    if (await SqlExecuter.QueryAsync("GetAllRoutingOperationList", "10001", values) is DataTable dtRouting)
                    {
                        grdOperation.DataSource = dtRouting;

                        if (lastFocusedOperationRowIndex > -1)
                        {
                            if (dtRouting.Rows.Count < lastFocusedOperationRowIndex)
                            {
                                lastFocusedOperationRowIndex = dtRouting.Rows.Count - 1;
                            }
                        }
                        else
                        {
                            lastFocusedOperationRowIndex = 0;
                        }

                        grdOperation.View.FocusedRowHandle = lastFocusedOperationRowIndex;
                    }

                    SearchProcessSpec();
                }
            }
            catch (Exception ex)
            {
                throw MessageException.Create(Format.GetString(ex));
            }
            finally
            {
                DialogManager.CloseWaitArea(this.pnlContent);
            }
        }

        /// <summary>
        /// 조회조건 영역 초기화 시작
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            #region 품목

            var conditionProductId = Conditions.AddSelectPopup("P_PRODUCTDEFID", new SqlQuery("GetProductDefinitionList", "10002", $"PLANTID={UserInfo.Current.Plant}"), "PRODUCTDEFID", "PRODUCTDEFID")
                                               .SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)
                                               .SetPopupLayoutForm(800, 800)
                                               .SetLabel("PRODUCTDEFID")
                                               .SetPosition(1.2)
                                               .SetValidationIsRequired()
                                               .SetPopupResultCount(1)
                                               .SetPopupApplySelection((selectRow, gridRow) =>
                                               {
                                                   List<string> productDefnameList = new List<string>();
                                                   List<string> productRevisionList = new List<string>();

                                                   selectRow.AsEnumerable().ForEach(r =>
                                                   {
                                                       productDefnameList.Add(Format.GetString(r["PRODUCTDEFNAME"]));
                                                       productRevisionList.Add(Format.GetString(r["PRODUCTDEFVERSION"]));
                                                   });

                                                   Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").EditValue = string.Join(",", productDefnameList);
                                                   Conditions.GetControl<SmartTextBox>("P_PRODUCTDEFVERSION").EditValue = string.Join(",", productRevisionList);
                                               });

            conditionProductId.Conditions.AddTextBox("TXTPRODUCTDEFNAME");
            conditionProductId.Conditions.AddComboBox("PRODUCTDIVISION", new SqlQuery("GetTypeList", "10001", $"CODECLASSID=ProductDivision2", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                                         .SetDefault("Product");

            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 100);
            conditionProductId.GridColumns.AddComboBoxColumn("PRODUCTDEFTYPE", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductDefType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            conditionProductId.GridColumns.AddComboBoxColumn("PRODUCTIONTYPE", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            conditionProductId.GridColumns.AddComboBoxColumn("UNIT", 90, new SqlQuery("GetUomDefinitionList", "10001", "UOMCLASSID=Segment"), "UOMDEFNAME", "UOMDEFID");

            #endregion 품목
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").ReadOnly = true;
            Conditions.GetControl<SmartTextBox>("P_PRODUCTDEFVERSION").ReadOnly = true;

            Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").EditValueChanged += (s, e) =>
            {
                SmartSelectPopupEdit PopProdutid = s as SmartSelectPopupEdit;

                if (Format.GetFullTrimString(PopProdutid.EditValue).Equals(string.Empty))
                {
                    Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").EditValue = string.Empty;
                    Conditions.GetControl<SmartTextBox>("P_PRODUCTDEFVERSION").EditValue = string.Empty;
                }
            };
        }

        #endregion 검색

        #region 유효성 검사

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            grdOperation.View.CheckValidation();
            grdProcessSpec.View.CheckValidation();

            if (grdProcessSpec.GetChangedRows().Rows.Count.Equals(0))
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }

            DataTable dt = grdProcessSpec.DataSource as DataTable;

            var value = from r in dt.AsEnumerable()
                        group r by new
                        {
                            INSPITEMID = r.Field<string>("INSPITEMID"),
                            INSPITEMNAME = r.Field<string>("INSPITEMNAME")
                        } into g
                        where g.Count() > 1
                        select g;

            if (value.Count() > 0)
            {
                throw MessageException.Create("DuplicationInspectionItemID", value.ElementAt(0).Key.INSPITEMNAME);
            }
        }

        #endregion 유효성 검사

        #region private Fuction

        /// <summary>
        ///
        /// </summary>
        private void SearchProcessSpec()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();

            if (grdOperation.View.FocusedRowHandle > -1)
            {
                //공정스펙
                param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                param.Add("RESOURCEID", grdOperation.View.GetFocusedDataRow()["ITEMID"].ToString());
                param.Add("RESOURCEVERSION", grdOperation.View.GetFocusedDataRow()["ITEMVERSION"].ToString());
                param.Add("PROCESSSEGMENTID", grdOperation.View.GetFocusedDataRow()["PROCESSSEGMENTID"].ToString());
                param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                param.Add("SPECCLASSID", "OperationSpec");

                if (SqlExecuter.Query("GetRoutingInspectionItemList", "10002", param) is DataTable dtProcessSpec)
                {
                    grdProcessSpec.DataSource = dtProcessSpec;

                    if (dtProcessSpec.Rows.Count.Equals(0))
                    {
                        cboChartInfo.EditValue = "XBARR";
                        cboChartInfo.ReadOnly = false;
                    }
                    else
                    {
                        if (dtProcessSpec.AsEnumerable().Where(s => !string.IsNullOrWhiteSpace(s["DEFAULTCHARTTYPE"].ToString())).FirstOrDefault() is DataRow chartRow)
                        {
                            cboChartInfo.EditValue = chartRow["DEFAULTCHARTTYPE"].ToString();
                            cboChartInfo.ReadOnly = true;
                        }
                        else
                        {
                            cboChartInfo.EditValue = "XBARR";
                            cboChartInfo.ReadOnly = false;
                        }
                    }
                }
                else
                {
                    cboChartInfo.EditValue = "XBARR";
                    cboChartInfo.ReadOnly = false;
                }
            }
        }

        private void ClearData()
        {
            txtCutomerName.Text = string.Empty;
            txtProductDEFId.Text = string.Empty;
            txtProductDEFVersion.Text = string.Empty;
            txtCompletionWareHouse.Text = string.Empty;
            txtProductDEFName.Text = string.Empty;
            txtWorkType.Text = string.Empty;
            txtProductionType.Text = string.Empty;

            grdOperation.DataSource = null;
            grdProcessSpec.DataSource = null;
        }

        #endregion private Fuction
    }
}