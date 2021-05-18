#region using

using DevExpress.Utils.Menu;
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
    /// 프 로 그 램 명  : Defect Map > Check Sheet > AOI 일일 Check Sheet
    /// 업  무  설  명  : Lot 단위 AOI 일일 체크시트
    /// 생    성    자  : 전우성
    /// 생    성    일  : 2020-01-22
    /// 수  정  이  력  :
    ///
    ///
    /// </summary>
    public partial class AOIDailyCheckSheet : SmartConditionManualBaseForm
    {
        #region Local Variables

        /// <summary>
        /// 불량코드 테이블
        /// </summary>
        private DataTable _defectCode;

        #endregion Local Variables

        #region 생성자

        public AOIDailyCheckSheet()
        {
            InitializeComponent();
        }

        #endregion 생성자

        #region 컨텐츠 영역 초기화

        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeEvent();
            InitializeGrid();
        }

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            _defectCode = DefectMapHelper.GetAOIDefectCodeInfo;

            grdMain.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            var group = grdMain.View.AddGroupColumn("");
            group.AddTextBoxColumn("INSPECTDATE", 50);
            group.AddTextBoxColumn("PRODUCTREVISIONINPUT", 80);
            group.AddTextBoxColumn("PRODUCTDEFVERSION", 80);
            group.AddTextBoxColumn("MODELPRODUCT", 200);
            group.AddTextBoxColumn("INSPECTIONDEGREEPROCESS", 150);
            group.AddTextBoxColumn("LOTID", 170);
            group.AddTextBoxColumn("INSPECTIONWORKAREA", 150);
            group.AddTextBoxColumn("SCANSIDE", 80);

            group = grdMain.View.AddGroupColumn("QCMFINALINSPECTQTY");
            group.AddTextBoxColumn("WORKSTARTPANELQTY", 050).SetLabel("PNL").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("WORKSTARTPCSQTY", 050).SetLabel("PCS").SetTextAlignment(TextAlignment.Right);

            group = grdMain.View.AddGroupColumn("");
            group.AddTextBoxColumn("PCSDEFECTQTY", 060).SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("DEFECTRATE", 060).SetDisplayFormat("{0:f2}%", MaskTypes.Custom).SetTextAlignment(TextAlignment.Right);

            group = grdMain.View.AddGroupColumn("FRONTLAYER");
            group.AddTextBoxColumn("FRONTLAYER", 040);

            foreach (DataRow dr in _defectCode.Rows)
            {
                if (!DefectMapHelper.StringByDataRowObejct(dr, "GROUPCODE").EndsWith("5"))
                {
                    group.AddTextBoxColumn(string.Concat("FRONT_", dr["SUBCODE"].ToString()), 80)
                     .SetLabel(dr["SUBNAME"].ToString())
                     .SetTextAlignment(TextAlignment.Right);
                }
            }

            group = grdMain.View.AddGroupColumn("BACKLAYER");
            group.AddTextBoxColumn("BACKLAYER", 40);
            foreach (DataRow dr in _defectCode.Rows)
            {
                if (!DefectMapHelper.StringByDataRowObejct(dr, "GROUPCODE").EndsWith("5"))
                {
                    group.AddTextBoxColumn(string.Concat("BACK_", dr["SUBCODE"].ToString()), 80)
                     .SetLabel(dr["SUBNAME"].ToString())
                     .SetTextAlignment(TextAlignment.Right);
                }
            }

            group = grdMain.View.AddGroupColumn("ANALYSISOPEN");
            group.AddTextBoxColumn("OPENTARGET", 90).SetLabel("ANALYSISTARGET");
            group.AddTextBoxColumn("OPENRESULT", 70).SetLabel("ANALYSISRESULT");
            group.AddTextBoxColumn("OPENPNL", 40).SetLabel("ANALYSISGOODPNLQTY");

            group = grdMain.View.AddGroupColumn("ANALYSISSHROT");
            group.AddTextBoxColumn("SHORTTARGET", 90).SetLabel("ANALYSISTARGET");
            group.AddTextBoxColumn("SHORTRESULT", 70).SetLabel("ANALYSISRESULT");
            group.AddTextBoxColumn("SHORTPNL", 40).SetLabel("ANALYSISGOODPNLQTY");

            group = grdMain.View.AddGroupColumn("");
            group.AddTextBoxColumn("ANALYSISVENDOR", 90);

            group = grdMain.View.AddGroupColumn("QCMLRRWORKINFOR");
            group.AddTextBoxColumn("EQUIPMENTNAME", 40).SetLabel("EQUIPMENTUNIT");
            group.AddTextBoxColumn("WORKER", 40).SetLabel("EQUIPMENTOP");

            grdMain.View.PopulateColumns();
            grdMain.View.BestFitColumns();
            grdMain.View.SetIsReadOnly();

            grdMain.GridButtonItem = GridButtonItem.Export;
        }

        #endregion 컨텐츠 영역 초기화

        #region Event

        /// <summary>
        /// 이벤트 초기화
        /// </summary>
        private void InitializeEvent()
        {
            // Grid Context Menu 등록 : Lot History (LOT 이력조회)
            grdMain.InitContextMenuEvent += (s, e) =>
                e.AddMenus.Add(new DXMenuItem(Language.Get("MENU_PG-SG-0340"), OpenForm) { BeginGroup = true, Tag = "PG-SG-0340" });
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
                grdMain.View.ClearDatas();

                await base.OnSearchAsync();

                if (await SqlExecuter.QueryAsync("GetAoiDailyCheckSheetList", "10002",
                                                 DefectMapHelper.AddLanguageTypeToConditions(Conditions.GetValues())) is DataTable dt)
                {
                    if (dt.Rows.Count.Equals(0))
                    {
                        ShowMessage("NoSelectData");
                        return;
                    }

                    grdMain.DataSource = SetData(dt);
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

            // 2021-01-08 오근영 (261) 조회조건 품목 추가
            CommonFunction.AddConditionProductPopup("P_PRODUCTDEFID", 2.5, true, Conditions, "PRODUCTDEF", "PRODUCTDEF", false);

            #region 작업장

            Dictionary<string, object> dicParam = new Dictionary<string, object>
            {
                { "ENTERPRISEID", UserInfo.Current.Enterprise },
                { "PLANTID", UserInfo.Current.Plant },
                { "LANGUAGETYPE", UserInfo.Current.LanguageType }
            };

            var condition = Conditions.AddSelectPopup("P_AREAID", new SqlQuery("GetAreaListAOI", "10007", dicParam), "AREANAME", "AREAID")
                                      .SetPopupLayout("SELECTWAREHOUSEID", PopupButtonStyles.Ok_Cancel, true, false)
                                      .SetPopupResultCount(0)
                                      .SetPopupLayoutForm(800, 800, FormBorderStyle.FixedToolWindow)
                                      .SetPosition(13)
                                      .SetLabel("AREANAME");

            condition.Conditions.AddTextBox("TXTAREA");

            condition.GridColumns.AddTextBoxColumn("AREAID", 120);
            condition.GridColumns.AddTextBoxColumn("AREANAME", 200);
            condition.GridColumns.AddTextBoxColumn("OWNTYPE", 080).SetTextAlignment(TextAlignment.Center);
            condition.GridColumns.AddTextBoxColumn("WAREHOUSEID", 090);
            condition.GridColumns.AddTextBoxColumn("WAREHOUSENAME", 110);
            condition.GridColumns.AddTextBoxColumn("VENDORID", 090);
            condition.GridColumns.AddTextBoxColumn("VENDORNAME", 110);

            #endregion 작업장
        }

        #endregion 검색

        #region Private Function

        /// <summary>
        /// 조회 Data Summary
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private DataTable SetData(DataTable dt)
        {
            DataTable table = (grdMain.DataSource as DataTable).Clone();
            DataRow newRow = table.NewRow();
            DefectManualTable orignal = new DefectManualTable();
            DefectManualTable comparison;
            int defectCount = 0;
            string layer = string.Empty;
            string defectCode = string.Empty;

            foreach (DataRow dr in dt.Rows)
            {
                defectCode = DefectMapHelper.StringByDataRowObejct(dr, "DEFECTCODE");

                comparison = new DefectManualTable()
                {
                    INSPECTDATE = DefectMapHelper.StringByDataRowObejct(dr, "INSPECTDATE"),
                    PRODUCTREVISIONINPUT = DefectMapHelper.StringByDataRowObejct(dr, "PRODUCTREVISIONINPUT"),
                    MODELPRODUCT = DefectMapHelper.StringByDataRowObejct(dr, "MODELPRODUCT"),
                    INSPECTIONDEGREEPROCESS = DefectMapHelper.StringByDataRowObejct(dr, "INSPECTIONDEGREEPROCESS"),
                    LOTID = DefectMapHelper.StringByDataRowObejct(dr, "LOTID"),
                    INSPECTIONWORKAREA = DefectMapHelper.StringByDataRowObejct(dr, "INSPECTIONWORKAREA"),
                    SCANSIDE = DefectMapHelper.StringByDataRowObejct(dr, "SCANSIDE"),
                    WORKSTARTPCSQTY = DefectMapHelper.StringByDataRowObejct(dr, "WORKSTARTPCSQTY"),
                    WORKSTARTPANELQTY = DefectMapHelper.StringByDataRowObejct(dr, "WORKSTARTPANELQTY"),
                    FRONTLAYER = DefectMapHelper.StringByDataRowObejct(dr, "FRONTLAYER"),
                    BACKLAYER = DefectMapHelper.StringByDataRowObejct(dr, "BACKLAYER"),
                    EQUIPMENTNAME = DefectMapHelper.StringByDataRowObejct(dr, "EQUIPMENTNAME"),
                    WORKER = DefectMapHelper.StringByDataRowObejct(dr, "WORKER"),
                    PRODUCTDEFVERSION = DefectMapHelper.StringByDataRowObejct(dr, "PRODUCTDEFVERSION"),
                    ANALYSISVENDOR = DefectMapHelper.StringByDataRowObejct(dr, "ANALYSISVENDOR")
                };

                if (defectCode.Equals("9999"))
                {
                    continue;
                }

                if (DefectMapHelper.IsComparisonDefectManualTable(orignal, comparison))
                {
                    if (!string.IsNullOrEmpty(defectCode) && !defectCode.StartsWith("5"))
                    {
                        layer = DefectMapHelper.StringByDataRowObejct(dr, "FRONTLAYER").Equals(DefectMapHelper.StringByDataRowObejct(dr, "LAYERID")) ? "FRONT_" : "BACK_";
                        newRow[string.Concat(layer, defectCode)] = DefectMapHelper.StringByDataRowObejct(dr, "DEFECTCOUNT");

                        if (defectCode.Equals("1001"))
                        {
                            newRow["OPENTARGET"] = DefectMapHelper.StringByDataRowObejct(dr, "ANALYSISTARGET");
                            newRow["OPENRESULT"] = DefectMapHelper.StringByDataRowObejct(dr, "ANALYSISRESULT");
                            newRow["OPENPNL"] = DefectMapHelper.StringByDataRowObejct(dr, "ANALYSISGOODPNLQTY");
                        }
                        else if (defectCode.Equals("2001"))
                        {
                            newRow["SHORTTARGET"] = DefectMapHelper.StringByDataRowObejct(dr, "ANALYSISTARGET");
                            newRow["SHORTRESULT"] = DefectMapHelper.StringByDataRowObejct(dr, "ANALYSISRESULT");
                            newRow["SHORTPNL"] = DefectMapHelper.StringByDataRowObejct(dr, "ANALYSISGOODPNLQTY");
                        }
                    }
                }
                else
                {
                    orignal = comparison;
                    newRow["PCSDEFECTQTY"] = defectCount;
                    newRow["DEFECTRATE"] = DefectMapHelper.SetDefectRate(defectCount, DefectMapHelper.IntByDataRowObject(newRow, "WORKSTARTPCSQTY"));

                    defectCount = 0;

                    table.Rows.Add(newRow);
                    newRow = table.NewRow();
                    newRow["INSPECTDATE"] = orignal.INSPECTDATE;
                    newRow["PRODUCTREVISIONINPUT"] = orignal.PRODUCTREVISIONINPUT;
                    newRow["PRODUCTDEFVERSION"] = orignal.PRODUCTDEFVERSION;
                    newRow["MODELPRODUCT"] = orignal.MODELPRODUCT;
                    newRow["INSPECTIONDEGREEPROCESS"] = orignal.INSPECTIONDEGREEPROCESS;
                    newRow["LOTID"] = orignal.LOTID;
                    newRow["INSPECTIONWORKAREA"] = orignal.INSPECTIONWORKAREA;
                    newRow["SCANSIDE"] = orignal.SCANSIDE;
                    newRow["WORKSTARTPCSQTY"] = orignal.WORKSTARTPCSQTY;
                    newRow["WORKSTARTPANELQTY"] = orignal.WORKSTARTPANELQTY;
                    newRow["FRONTLAYER"] = orignal.FRONTLAYER;
                    newRow["BACKLAYER"] = orignal.BACKLAYER;
                    newRow["EQUIPMENTNAME"] = orignal.EQUIPMENTNAME;
                    newRow["WORKER"] = orignal.WORKER;
                    newRow["ANALYSISVENDOR"] = orignal.ANALYSISVENDOR;

                    if (!string.IsNullOrEmpty(defectCode) && !defectCode.StartsWith("5"))
                    {
                        layer = orignal.FRONTLAYER.Equals(DefectMapHelper.StringByDataRowObejct(dr, "LAYERID")) ? "FRONT_" : "BACK_";
                        newRow[string.Concat(layer, defectCode)] = DefectMapHelper.StringByDataRowObejct(dr, "DEFECTCOUNT");

                        if (defectCode.Equals("1001"))
                        {
                            newRow["OPENTARGET"] = DefectMapHelper.StringByDataRowObejct(dr, "ANALYSISTARGET");
                            newRow["OPENRESULT"] = DefectMapHelper.StringByDataRowObejct(dr, "ANALYSISRESULT");
                            newRow["OPENPNL"] = DefectMapHelper.StringByDataRowObejct(dr, "ANALYSISGOODPNLQTY");
                        }
                        else if (defectCode.Equals("2001"))
                        {
                            newRow["SHORTTARGET"] = DefectMapHelper.StringByDataRowObejct(dr, "ANALYSISTARGET");
                            newRow["SHORTRESULT"] = DefectMapHelper.StringByDataRowObejct(dr, "ANALYSISRESULT");
                            newRow["SHORTPNL"] = DefectMapHelper.StringByDataRowObejct(dr, "ANALYSISGOODPNLQTY");
                        }
                    }
                }

                defectCount += DefectMapHelper.IntByDataRowObject(dr, "DEFECTCOUNT");
            }

            newRow["PCSDEFECTQTY"] = defectCount;
            newRow["DEFECTRATE"] = DefectMapHelper.SetDefectRate(defectCount, Convert.ToInt32(orignal.WORKSTARTPCSQTY));

            table.Rows.Add(newRow);
            table.Rows.RemoveAt(0);

            return table;
        }

        /// <summary>
        /// 팝업
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

                var param = currentRow.Table.Columns
                                      .Cast<DataColumn>()
                                      .ToDictionary(x => x.ColumnName,
                                                    x => currentRow[x.ColumnName]);

                OpenMenu(Format.GetString((sender as DXMenuItem).Tag), param); //다른창 호출..
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

        #endregion Private Function
    }
}