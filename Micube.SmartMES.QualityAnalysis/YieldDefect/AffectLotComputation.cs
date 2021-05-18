#region using

using DevExpress.Utils.Menu;
using DevExpress.XtraEditors.Controls;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons;
using Micube.SmartMES.Commons.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion using

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 수율현황 및 불량분석 > Affect Lot 산정(출)
    /// 업  무  설  명  : 기존의 AffectLot.cs 대신 새로개발된 화면
    /// 생    성    자  : 전우성
    /// 생    성    일  : 2021-04-14
    /// 수  정  이  력  :
    ///
    ///
    /// </summary>
    public partial class AffectLotComputation : SmartConditionManualBaseForm
    {
        #region 생성자

        public AffectLotComputation()
        {
            InitializeComponent();
        }

        #endregion 생성자

        #region 컨텐츠 영역 초기화

        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeLanguageKey();
            InitializeGrid();
            InitializeEvent();
        }

        /// <summary>
        /// Language Key 초기화
        /// </summary>
        private void InitializeLanguageKey()
        {
            grdMain.LanguageKey = "AFFECTLOT";
            smartLayoutControl1.SetLanguageKey(layoutControlItem2, "WORKER");
            smartLayoutControl1.SetLanguageKey(layoutControlItem3, "WEEK");
        }

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            grdMain.GridButtonItem = GridButtonItem.Export;
            grdMain.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            grdMain.View.AddTextBoxColumn("LOTID", 180);
            grdMain.View.AddTextBoxColumn("CUSTOMERNAME", 80);
            grdMain.View.AddTextBoxColumn("PRODUCTDEFNAME", 180);
            grdMain.View.AddTextBoxColumn("PRODUCTDEFID", 80);
            grdMain.View.AddTextBoxColumn("PRODUCTDEFVERSION", 70);
            grdMain.View.AddTextBoxColumn("PLANTID", 70);
            grdMain.View.AddTextBoxColumn("PROCESSDEFID", 80);
            grdMain.View.AddTextBoxColumn("PROCESSDEFVERSION", 70);
            grdMain.View.AddTextBoxColumn("USERSEQUENCE", 80);
            grdMain.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 120);
            grdMain.View.AddTextBoxColumn("AREANAME", 140);
            grdMain.View.AddComboBoxColumn("PRODUCTIONTYPE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdMain.View.AddComboBoxColumn("ISREWORK", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdMain.View.AddTextBoxColumn("SEGMENTINPUTDATE", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetTextAlignment(TextAlignment.Center);
            grdMain.View.AddTextBoxColumn("RECEIVEDATE", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetTextAlignment(TextAlignment.Center);
            grdMain.View.AddTextBoxColumn("STARTDATE", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetTextAlignment(TextAlignment.Center);
            grdMain.View.AddTextBoxColumn("WORKENDDATE", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetTextAlignment(TextAlignment.Center);
            grdMain.View.AddTextBoxColumn("LOTSENDDATE", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetTextAlignment(TextAlignment.Center);
            grdMain.View.AddTextBoxColumn("WEEK", 70);
            grdMain.View.AddTextBoxColumn("BOXNO", 70);
            grdMain.View.AddTextBoxColumn("EQUIPMENTCLASSID", 80);
            grdMain.View.AddTextBoxColumn("EQUIPMENTCLASSNAME", 100);
            grdMain.View.AddTextBoxColumn("EQUIPMENTID", 80);
            grdMain.View.AddTextBoxColumn("EQUIPMENTNAME", 100);
            grdMain.View.AddTextBoxColumn("WORKSTARTUSER", 80).SetLabel("WORKMAN");
            grdMain.View.AddTextBoxColumn("DURABLEDEFID", 130);
            grdMain.View.AddTextBoxColumn("DURABLEDEFNAME", 150);
            grdMain.View.AddTextBoxColumn("DURABLELOTID", 150);
            grdMain.View.AddComboBoxColumn("DURABLECLASSID", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=DurableClass", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetLabel("DURABLECLASS");
            grdMain.View.AddTextBoxColumn("MATERIALDEF", 150);
            grdMain.View.AddTextBoxColumn("CONSUMABLEDEFNAME", 150);
            grdMain.View.AddTextBoxColumn("MATERIALLOT", 130);
            grdMain.View.AddTextBoxColumn("SUBASSEMDEFID", 130).SetLabel("SEMIPRODUCTCODE");
            grdMain.View.AddTextBoxColumn("SUBASSEMDEFNAME", 150).SetLabel("SEMIPRODUCT");
            grdMain.View.AddTextBoxColumn("SUBASSEMLOTID", 150).SetLabel("SEMIPRODUCTLOT");
            grdMain.View.AddTextBoxColumn("PRESENTUSERSEQUENCE", 80);
            grdMain.View.AddTextBoxColumn("PRESENTPROCESSSEGMENT", 120);
            grdMain.View.AddTextBoxColumn("PRESENTAREA", 140);
            grdMain.View.AddTextBoxColumn("WINTIMEOVERPROCESSSEGMENT", 150);
            grdMain.View.AddSpinEditColumn("WINDOWTIMESTDTIME", 150).SetTextAlignment(TextAlignment.Right);
            grdMain.View.AddSpinEditColumn("WINTIMEPSGDELAYTIME", 150).SetTextAlignment(TextAlignment.Right);
            grdMain.View.AddSpinEditColumn("WINTIMEPSGOVERTIME", 150).SetTextAlignment(TextAlignment.Right);

            grdMain.View.PopulateColumns();

            grdMain.View.SetIsReadOnly();
            grdMain.View.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;

            grdMain.ShowStatusBar = true;
        }

        #endregion 컨텐츠 영역 초기화

        #region Event

        /// <summary>
        /// 이벤트 초기화
        /// </summary>
        private void InitializeEvent()
        {
            // 검색조건의 품목코드 x 버튼 클릭 이벤트
            if (Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID") is var popup)
            {
                popup.Properties.ButtonClick += (s, e) =>
                {
                    if (e.Button.Kind.Equals(ButtonPredefines.Clear))
                    {
                        Conditions.GetControl<SmartComboBox>("P_PRODUCTDEFVERSION").EditValue = string.Empty;
                        Conditions.GetControl<SmartTextBox>("PRODUCTDEFNAME").Text = string.Empty;
                    }
                };
            }

            txtWorker.TextChanged += (s, e) => TextFiltering();
            txtWeek.TextChanged += (s, e) => TextFiltering();

            // Context Menu 추가
            grdMain.InitContextMenuEvent += (s, e) =>
            {
                e.AddMenus.Add(new DXMenuItem(Language.Get("MENU_PG-SG-0340"), OpenForm) { BeginGroup = true, Tag = "PG-SG-0340" });
            };

            // 검색조건 기준방식에 따른 ReadOnly 처리 이벤트
            ((SmartComboBox)Conditions.GetControl("P_WINDOWSTIMEOVER")).EditValueChanged += (s, e) =>
            {
                SmartComboBox edit = s as SmartComboBox;

                ((SmartSelectPopupEdit)Conditions.GetControl("P_WTPROCESSSEGMENTID")).ReadOnly = !Format.GetFullTrimString(edit.EditValue).Equals("Y");
                Conditions.GetControl<SmartSelectPopupEdit>("P_WTPROCESSSEGMENTID").EditValue = string.Empty;
            };
        }

        /// <summary>
        /// Main Grid Context Menu Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OpenForm(object sender, EventArgs args)
        {
            if (grdMain.View.FocusedRowHandle < 0)
            {
                return;
            }

            try
            {
                DialogManager.ShowWaitDialog();

                DataRow currentRow = grdMain.View.GetFocusedDataRow();

                string menuId = (sender as DXMenuItem).Tag.ToString();

                var param = currentRow.Table.Columns
                                            .Cast<DataColumn>()
                                            .ToDictionary(col => col.ColumnName, col => currentRow[col.ColumnName]);

                OpenMenu(menuId, param);
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

        /// <summary>
        /// 필터링 이벤트
        /// </summary>
        private void TextFiltering()
        {
            string filter = string.Empty;

            if (!txtWorker.Text.Equals(string.Empty))
            {
                filter += string.Concat("Contains([WORKSTARTUSER], '", txtWorker.Text, "')");
            }

            if (!txtWeek.Text.Equals(string.Empty))
            {
                if (filter.Equals(string.Empty))
                {
                    filter += string.Concat("Contains([WEEK], '", txtWeek.Text, "')");
                }
                else
                {
                    filter += string.Concat(" And Contains([WEEK], '", txtWeek.Text, "')");
                }
            }

            grdMain.View.ActiveFilterString = filter;
        }

        #endregion Event

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

                Dictionary<string, object> param = Conditions.GetValues();
                param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                grdMain.View.ClearDatas();

                if (await SqlExecuter.QueryAsync("SelectAffectLotList", "10002", param) is DataTable dt)
                {
                    if (dt.Rows.Count.Equals(0))
                    {
                        ShowMessage("NoSelectData");
                        return;
                    }

                    grdMain.DataSource = dt;
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

        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            Dictionary<string, object> param = new Dictionary<string, object>
            {
                { "LANGUAGETYPE", UserInfo.Current.LanguageType },
                { "ENTERPRISEID", UserInfo.Current.Enterprise },
                { "PLANTID", UserInfo.Current.Plant }
            };

            // Lot No
            CommonFunction.AddConditionLotPopup("P_LOTID", 1, false, Conditions);

            #region 고객사

            var condition = Conditions.AddSelectPopup("P_CUSTOMER", new SqlQuery("GetCustomerList", "10001", param), "CUSTOMERNAME", "CUSTOMERID")
                .SetPopupLayout("SELECTCUSTOMERID", PopupButtonStyles.Ok_Cancel, true, true)
                .SetPopupLayoutForm(800, 800, FormBorderStyle.FixedToolWindow)
                .SetPopupAutoFillColumns("CUSTOMERNAME")
                .SetLabel("CUSTOMERID")
                .SetPosition(2)
                .SetPopupResultCount(1);

            condition.Conditions.AddTextBox("TXTCUSTOMERID");

            condition.GridColumns.AddTextBoxColumn("CUSTOMERID", 150);
            condition.GridColumns.AddTextBoxColumn("CUSTOMERNAME", 200);

            #endregion 고객사

            #region 품목코드

            CommonFunction.AddConditionProductPopup("P_PRODUCTDEFID", 3, false, Conditions, "PRODUCTDEFID");
            Conditions.GetCondition<ConditionItemSelectPopup>("P_PRODUCTDEFID").SetPopupApplySelection((selectedRows, dataGridRows) =>
            {
                string productDefName = Format.GetString(selectedRows.FirstOrDefault()["PRODUCTDEFNAME"]);

                List<DataRow> list = selectedRows.ToList();
                List<string> listRev = new List<string>();
                string productlist = string.Empty;

                selectedRows.ForEach(row =>
                {
                    string productid = Format.GetString(row["PRODUCTDEFID"]);
                    string revision = Format.GetString(row["PRODUCTDEFVERSION"]);
                    productlist = productlist + productid + ',';
                    listRev.Add(revision);
                });

                productlist = productlist.TrimEnd(',');

                listRev = listRev.Distinct().ToList();
                Dictionary<string, object> subParam = new Dictionary<string, object>
                {
                    { "ENTERPRISEID", UserInfo.Current.Enterprise },
                    { "P_PLANTID", UserInfo.Current.Plant },
                    { "P_PRODUCTDEFID", productlist }
                };

                Conditions.GetControl<SmartComboBox>("P_PRODUCTDEFVERSION").DataSource = SqlExecuter.Query("selectProductdefVesion", "10001", subParam);

                if (listRev.Count > 0)
                {
                    if (listRev.Count.Equals(1))
                    {
                        Conditions.GetControl<SmartComboBox>("P_PRODUCTDEFVERSION").EditValue = Format.GetFullTrimString(listRev[0]);
                    }
                    else
                    {
                        Conditions.GetControl<SmartComboBox>("P_PRODUCTDEFVERSION").EditValue = Format.GetFullTrimString('*');
                    }
                }

                Conditions.GetControl<SmartTextBox>("PRODUCTDEFNAME").Text = productDefName;
            });

            #endregion 품목코드

            // 품목명
            Conditions.AddTextBox("PRODUCTDEFNAME").SetIsReadOnly().SetPosition(4);

            // 품목 Rev
            Conditions.AddComboBox("P_PRODUCTDEFVERSION", new SqlQuery("selectProductdefVesion", "10001"), "PRODUCTDEFVERSIONNAME", "PRODUCTDEFVERSION")
                      .SetLabel("PRODUCTDEFVERSION")
                      .SetPosition(5)
                      .SetEmptyItem(Language.Get("ALLVIEWS"), "*")
                      .SetDefault("*");

            param.Add("CODECLASSID", "YieldProductionType");

            // 생산구분
            Conditions.AddComboBox("P_PRODUCTIONTYPE", new SqlQuery("GetCodeList", "00001", param))
                      .SetLabel("PRODUCTIONTYPE")
                      .SetPosition(6)
                      .SetEmptyItem(Language.Get("ALLVIEWS"), "*")
                      .SetDefault("*");

            // Box No
            Conditions.AddTextBox("P_BOXNO").SetLabel("BOXNO").SetPosition(7).SetIsReadOnly();

            //작업완료일
            Conditions.AddDateEdit("P_WORKRESULTDATEFR").SetLabel("WORKENDDATE").SetPosition(8).SetDefault(DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd HH:mm:ss"));
            Conditions.AddDateEdit("P_WORKRESULTDATETO").SetLabel("").SetPosition(9).SetDefault(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            //Site
            Conditions.AddComboBox("P_PLANTID", new SqlQuery("GetPlantList", "10019", param))
                      .SetLabel("PLANTID")
                      .SetPosition(10)
                      .SetValidationIsRequired()
                      .SetDefault("YPE")
                      .SetEmptyItem(Language.Get("ALLVIEWS"), "*");

            #region 공정

            param.Add("P_LOTWORKRESULTDATETYPE", "WORKENDTIME");

            condition = Conditions.AddSelectPopup("P_PROCESSSEGMENTID", new SqlQuery("GetProcessSegmentIdByProductdefid", "10001", param), "PROCESSSEGMENNAME", "PROCESSSEGMENTID")
                                  .SetPopupLayout("PROCESSSEGMENT", PopupButtonStyles.Ok_Cancel, true, true)
                                  .SetPopupLayoutForm(700, 800, FormBorderStyle.FixedToolWindow)
                                  .SetLabel("PROCESSSEGMENT")
                                  .SetPopupAutoFillColumns("PROCESSSEGMENNAME")
                                  .SetPopupResultCount(1)
                                  .SetPosition(11)
                                  .SetValidationIsRequired()
                                  .SetRelationIds("P_WORKRESULTDATEFR", "P_WORKRESULTDATETO", "P_PLANTID");

            condition.Conditions.AddTextBox("P_PROCESSSEGMENTID").SetLabel("PROCESSSEGMENT");

            condition.GridColumns.AddTextBoxColumn("TOPPROCESSSEGMENTCLASSID", 70);
            condition.GridColumns.AddTextBoxColumn("TOPPROCESSSEGMENTCLASSNAME", 70);
            condition.GridColumns.AddTextBoxColumn("MIDPROCESSSEGMENTCLASSID", 70).SetLabel("MIDDLEPROCESSSEGMENTCLASSID");
            condition.GridColumns.AddTextBoxColumn("MIDPROCESSSEGMENTCLASSNAME", 100).SetLabel("MIDDLEPROCESSSEGMENTCLASSNAME");
            condition.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 70).SetValidationKeyColumn();
            condition.GridColumns.AddTextBoxColumn("PROCESSSEGMENNAME", 150).SetLabel("PROCESSSEGMENTEXTLIST");

            #endregion 공정

            #region 작업장

            condition = Conditions.AddSelectPopup("P_AREAID", new SqlQuery("GetAreaIdByProcesssegmentId", "10001", param), "AREANAME", "AREAID")
                                  .SetPopupLayout("AREA", PopupButtonStyles.Ok_Cancel, true, true)
                                  .SetPopupLayoutForm(430, 800, FormBorderStyle.FixedToolWindow)
                                  .SetLabel("AREA")
                                  .SetPopupAutoFillColumns("AREANAME")
                                  .SetPopupResultCount(1)
                                  .SetValidationIsRequired()
                                  .SetPosition(12)
                                  .SetRelationIds("P_WORKRESULTDATEFR", "P_WORKRESULTDATETO", "P_PLANTID", "P_PROCESSSEGMENTID");

            condition.Conditions.AddTextBox("P_AREAID").SetLabel("AREA");

            condition.GridColumns.AddTextBoxColumn("AREAID", 100).SetValidationKeyColumn();
            condition.GridColumns.AddTextBoxColumn("AREANAME", 200);

            #endregion 작업장

            #region 설비그룹

            condition = Conditions.AddSelectPopup("P_EQUIPMENTCLASSID", new SqlQuery("GetEquipmentClassIdByAreaId", "10001", param), "EQUIPMENTCLASSNAME", "EQUIPMENTCLASSID")
                                  .SetPopupLayout("EQUIPMENTCLASSID", PopupButtonStyles.Ok_Cancel, true, true)
                                  .SetPopupLayoutForm(430, 800, FormBorderStyle.FixedToolWindow)
                                  .SetLabel("EQUIPMENTCLASSID")
                                  .SetPopupAutoFillColumns("EQUIPMENTCLASSNAME")
                                  .SetPopupResultCount(1)
                                  .SetPosition(13)
                                  .SetRelationIds("P_WORKRESULTDATETO", "P_PLANTID", "P_PROCESSSEGMENTID", "P_AREAID");

            condition.Conditions.AddTextBox("P_EQUIPMENTCLASSID").SetLabel("EQUIPMENTCLASSID");

            condition.GridColumns.AddTextBoxColumn("EQUIPMENTCLASSID", 100).SetValidationKeyColumn();
            condition.GridColumns.AddTextBoxColumn("EQUIPMENTCLASSNAME", 200);

            #endregion 설비그룹

            #region 설비

            condition = Conditions.AddSelectPopup("P_EQUIPMENTID", new SqlQuery("GetEquipmentidByResourceId", "10001", param), "EQUIPMENTIDNAME", "EQUIPMENTID")
                                  .SetPopupLayout("EQUIPMENTID", PopupButtonStyles.Ok_Cancel, true, true)
                                  .SetPopupLayoutForm(430, 800, FormBorderStyle.FixedToolWindow)
                                  .SetLabel("EQUIPMENTID")
                                  .SetPopupAutoFillColumns("EQUIPMENTIDNAME")
                                  .SetPopupResultCount(1)
                                  .SetPosition(14)
                                  .SetRelationIds("P_WORKRESULTDATEFR", "P_WORKRESULTDATETO", "P_PLANTID", "P_PROCESSSEGMENTID", "P_AREAID", "P_EQUIPMENTCLASSID");

            condition.Conditions.AddTextBox("P_EQUIPMENTID").SetLabel("EQUIPMENTID");

            condition.GridColumns.AddTextBoxColumn("EQUIPMENTID", 100).SetValidationKeyColumn();
            condition.GridColumns.AddTextBoxColumn("EQUIPMENTIDNAME", 200);

            #endregion 설비

            param["CODECLASSID"] = "DurableClass";

            // 치공구 구분
            Conditions.AddComboBox("P_DURABLECLASSID", new SqlQuery("GetCodeList", "00001", param))
                      .SetLabel("DURABLECLASS")
                      .SetPosition(15)
                      .SetDefault("*")
                      .SetEmptyItem(Language.Get("ALLVIEWS"), "*");

            #region 치공구

            condition = Conditions.AddSelectPopup("P_DURABLEDEFID", new SqlQuery("GetdDurableDefIdByDurableClassId", "10001", param), "DURABLEDEFNAME", "DURABLEDEFID")
                                  .SetPopupLayout("TOOLCHANGETYPE", PopupButtonStyles.Ok_Cancel, true, true)
                                  .SetPopupLayoutForm(430, 800, FormBorderStyle.FixedToolWindow)
                                  .SetLabel("TOOLCHANGETYPE")
                                  .SetPopupAutoFillColumns("DURABLEDEFNAME")
                                  .SetPopupResultCount(1)
                                  .SetPosition(16)
                                  .SetRelationIds("P_WORKRESULTDATEFR", "P_WORKRESULTDATETO", "P_DURABLECLASSID");

            condition.Conditions.AddTextBox("P_DURABLEDEFID").SetLabel("DURABLEDEFNAME");

            condition.GridColumns.AddTextBoxColumn("DURABLEDEFID", 100).SetValidationKeyColumn();
            condition.GridColumns.AddTextBoxColumn("DURABLEDEFNAME", 100).SetValidationKeyColumn();

            #endregion 치공구

            #region Tool No

            condition = Conditions.AddSelectPopup("P_DURABLELOTID", new SqlQuery("GetdurablelotidByDurableDefId", "10001", param), "DURABLELOTID", "DURABLELOTID")
                                  .SetPopupLayout("DURABLELOTID", PopupButtonStyles.Ok_Cancel, true, true)
                                  .SetPopupLayoutForm(430, 800, FormBorderStyle.FixedToolWindow)
                                  .SetLabel("DURABLELOTID")
                                  .SetPopupAutoFillColumns("DURABLELOTID")
                                  .SetPopupResultCount(1)
                                  .SetPosition(17)
                                  .SetRelationIds("P_WORKRESULTDATEFR", "P_WORKRESULTDATETO", "P_DURABLECLASSID", "P_DURABLEDEFID");

            condition.Conditions.AddTextBox("P_DURABLELOTID").SetLabel("DURABLELOTIDNO");

            condition.GridColumns.AddTextBoxColumn("DURABLELOTID", 100).SetValidationKeyColumn().SetLabel("DURABLELOTID");

            #endregion Tool No

            #region 자재

            condition = Conditions.AddSelectPopup("P_MATERIALDEFID", new SqlQuery("GetMaterial", "10001", param), "MATERIALDEFNAME", "MATERIALDEFID")
                                  .SetPopupLayout("MATERIALDEF", PopupButtonStyles.Ok_Cancel, true, true)
                                  .SetPopupLayoutForm(430, 800, FormBorderStyle.FixedToolWindow)
                                  .SetLabel("MATERIALDEF")
                                  .SetPopupAutoFillColumns("MATERIALDEFNAME")
                                  .SetPopupResultCount(1)
                                  .SetPosition(18)
                                  .SetRelationIds("P_WORKRESULTDATEFR", "P_WORKRESULTDATETO");

            condition.Conditions.AddTextBox("P_MATERIALDEFID").SetLabel("RAWMATERIAL");

            condition.GridColumns.AddTextBoxColumn("MATERIALDEFID", 100).SetLabel("MATERIALDEF");
            condition.GridColumns.AddTextBoxColumn("MATERIALDEFNAME", 100);

            #endregion 자재

            #region 자재 Lot

            condition = Conditions.AddSelectPopup("P_MATERIALLOTID", new SqlQuery("GetMaterialLot", "10001", param), "MATERIALLOTID", "MATERIALLOTID")
                                  .SetPopupLayout("MATERIALLOT", PopupButtonStyles.Ok_Cancel, true, true)
                                  .SetPopupLayoutForm(430, 800, FormBorderStyle.FixedToolWindow)
                                  .SetLabel("MATERIALLOT")
                                  .SetPopupAutoFillColumns("MATERIALLOTID")
                                  .SetPopupResultCount(1)
                                  .SetPosition(19)
                                  .SetRelationIds("P_WORKRESULTDATEFR", "P_WORKRESULTDATETO", "P_MATERIALDEFID");

            condition.Conditions.AddTextBox("P_MATERIALLOTID").SetLabel("MATERIALLOT");

            condition.GridColumns.AddTextBoxColumn("MATERIALLOTID", 100).SetLabel("MATERIALLOT");

            #endregion 자재 Lot

            #region 반제품

            condition = Conditions.AddSelectPopup("P_MATERIALDEFIDPRODUCT", new SqlQuery("GetProductMaterialLotId", "10001", param), "MATERIALDEFNAME", "MATERIALDEFID")
                                  .SetPopupLayout("SEMIPRODUCT", PopupButtonStyles.Ok_Cancel, true, true)
                                  .SetPopupLayoutForm(430, 800, FormBorderStyle.FixedToolWindow)
                                  .SetLabel("SEMIPRODUCT")
                                  .SetPopupAutoFillColumns("MATERIALDEFNAME")
                                  .SetPopupResultCount(1)
                                  .SetPosition(20)
                                  .SetRelationIds("P_WORKRESULTDATEFR", "P_WORKRESULTDATETO");

            condition.Conditions.AddTextBox("P_MATERIALDEFIDPRODUCT").SetLabel("SEMIPRODUCT");

            condition.GridColumns.AddTextBoxColumn("MATERIALDEFID", 100).SetValidationKeyColumn().SetLabel("SEMIPRODUCTCODE");
            condition.GridColumns.AddTextBoxColumn("MATERIALDEFNAME", 100).SetLabel("SEMIPRODUCT");

            #endregion 반제품

            #region 반제품 Lot

            condition = Conditions.AddSelectPopup("P_MATERIALLOTIDPRODUCT", new SqlQuery("GetMaterialLotIdByProductMaterialLotId", "10001", param), "MATERIALLOTID", "MATERIALLOTID")
                                  .SetPopupLayout("SEMIPRODUCTLOT", PopupButtonStyles.Ok_Cancel, true, true)
                                  .SetPopupLayoutForm(430, 800, FormBorderStyle.FixedToolWindow)
                                  .SetLabel("SEMIPRODUCTLOT")
                                  .SetPopupAutoFillColumns("MATERIALLOTID")
                                  .SetPopupResultCount(1)
                                  .SetPosition(21)
                                  .SetRelationIds("P_WORKRESULTDATEFR", "P_WORKRESULTDATETO", "P_MATERIALDEFIDPRODUCT");

            condition.Conditions.AddTextBox("P_MATERIALLOTIDPRODUCT").SetLabel("SEMIPRODUCTLOT");
            condition.GridColumns.AddTextBoxColumn("MATERIALLOTID", 100).SetValidationKeyColumn().SetLabel("SEMIPRODUCTLOT");

            #endregion 반제품 Lot

            param["CODECLASSID"] = "YesNo";

            // Window Time 초과
            Conditions.AddComboBox("P_WINDOWSTIMEOVER", new SqlQuery("GetCodeList", "00001", param))
                      .SetLabel("WINDOWTIMEOVER")
                      .SetPosition(22)
                      .SetDefault("N");

            #region Window Time 공정

            condition = Conditions.AddSelectPopup("P_WTPROCESSSEGMENTID", new SqlQuery("GetProcessSegmentList", "10002", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "PROCESSSEGMENTCLASSTYPE=MiddleProcessSegmentClass"), "PROCESSSEGMENTNAME", "PROCESSSEGMENTID")
                                  .SetPopupLayout(Language.Get("SELECTPROCESSSEGMENTID"), PopupButtonStyles.Ok_Cancel, true, true)
                                  .SetPopupLayoutForm(800, 800)
                                  .SetPopupResultCount(1)
                                  .SetPopupAutoFillColumns("PROCESSSEGMENTNAME")
                                  .SetLabel("WINTIMEPROCESSEGMENT")
                                  .SetIsReadOnly()
                                  .SetPosition(23);

            condition.Conditions.AddComboBox("PROCESSSEGMENTCLASSID_MIDDLE", new SqlQuery("GetProcessSegmentClassByType", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "PROCESSSEGMENTCLASSTYPE=MiddleProcessSegmentClass"), "PROCESSSEGMENTCLASSNAME", "PROCESSSEGMENTCLASSID")
                                .SetLabel("MIDDLEPROCESSSEGMENT")
                                .SetEmptyItem();
            condition.Conditions.AddTextBox("PROCESSSEGMENT");

            condition.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME_TOP", 150).SetLabel("LARGEPROCESSSEGMENT");
            condition.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME_MIDDLE", 150).SetLabel("MIDDLEPROCESSSEGMENT");
            condition.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150);
            condition.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);

            #endregion Window Time 공정
        }

        #endregion 검색
    }
}