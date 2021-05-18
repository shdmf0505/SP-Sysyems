#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;

using Micube.SmartMES.Commons.Controls;

using DevExpress.Utils.Menu;

using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

#endregion

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : Defect Map > Check Sheet > Hole AOI 일일 Check Sheet
    /// 업  무  설  명  : Lot 단위 Hole AOI 일일 체크시트
    /// 생    성    자  : 전우성
    /// 생    성    일  : 2020-02-11
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class HoleAOIDailyCheckSheet : SmartConditionManualBaseForm
    {
        #region Local Variables

        /// <summary>
        /// 불량코드 테이블
        /// </summary>
        private DataTable _defectCode;

        #endregion

        #region 생성자

        public HoleAOIDailyCheckSheet()
        {
            InitializeComponent();
        }

        #endregion

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
            _defectCode = DefectMapHelper.GetCodeListByClassID("DefectMapHoleDefectCode");

            grdMain.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            var group = grdMain.View.AddGroupColumn("");
            group.AddTextBoxColumn("INSPECTDATE", 50).SetTextAlignment(TextAlignment.Left);
            group.AddTextBoxColumn("PRODUCTREVISIONINPUT", 80).SetTextAlignment(TextAlignment.Left);
            group.AddTextBoxColumn("MODELPRODUCT", 200).SetTextAlignment(TextAlignment.Left);
            group.AddTextBoxColumn("INSPECTIONDEGREEPROCESS", 150).SetTextAlignment(TextAlignment.Left);
            group.AddTextBoxColumn("LOTID", 170).SetTextAlignment(TextAlignment.Left);
            group.AddTextBoxColumn("INSPECTIONWORKAREA", 150).SetTextAlignment(TextAlignment.Left);
            group.AddTextBoxColumn("SCANSIDE", 80).SetTextAlignment(TextAlignment.Left);

            group = grdMain.View.AddGroupColumn("QCMFINALINSPECTQTY");
            group.AddTextBoxColumn("WORKSTARTPANELQTY", 50).SetLabel("PNL").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("WORKSTARTPCSQTY", 50).SetLabel("PCS").SetTextAlignment(TextAlignment.Right);

            group = grdMain.View.AddGroupColumn("");
            group.AddTextBoxColumn("PCSDEFECTQTY", 60).SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("DEFECTRATE", 60).SetDisplayFormat("{0:f2}%", MaskTypes.Custom).SetTextAlignment(TextAlignment.Right);

            group = grdMain.View.AddGroupColumn("FRONTLAYER");
            group.AddTextBoxColumn("FRONTLAYER", 40).SetTextAlignment(TextAlignment.Left);
            foreach (DataRow dr in _defectCode.Rows)
            {
                group.AddTextBoxColumn(string.Concat("FRONT_", dr["CODEID"].ToString()), 80)
                     .SetLabel(dr["CODENAME"].ToString())
                     .SetTextAlignment(TextAlignment.Right);
            }

            group = grdMain.View.AddGroupColumn("BACKLAYER");
            group.AddTextBoxColumn("BACKLAYER", 40).SetTextAlignment(TextAlignment.Left);
            foreach (DataRow dr in _defectCode.Rows)
            {
                group.AddTextBoxColumn(string.Concat("BACK_", dr["CODEID"].ToString()), 80)
                     .SetLabel(dr["CODENAME"].ToString())
                     .SetTextAlignment(TextAlignment.Right);
            }

            group = grdMain.View.AddGroupColumn("QCMLRRWORKINFOR");
            group.AddTextBoxColumn("EQUIPMENTNAME", 40).SetLabel("EQUIPMENTUNIT").SetTextAlignment(TextAlignment.Left);
            group.AddTextBoxColumn("WORKER", 40).SetLabel("EQUIPMENTOP").SetTextAlignment(TextAlignment.Left);

            grdMain.View.PopulateColumns();
            grdMain.View.BestFitColumns();
            grdMain.View.SetIsReadOnly();

            grdMain.GridButtonItem = GridButtonItem.Export;
        }

        #endregion

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

        #endregion

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

                Dictionary<string, object> value = new Dictionary<string, object>
                {
                    { "P_SEARCHDATE", Conditions.GetControl<SmartDateEdit>("P_SEARCHDATE").Text }
                };

                if (await SqlExecuter.QueryAsync("GetHoleAoiDailyCheckSheetList", "10001",
                                                 DefectMapHelper.AddLanguageTypeToConditions(value)) is DataTable dt)
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

        #endregion

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

            foreach (DataRow dr in dt.Rows)
            {
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
                };

                if (DefectMapHelper.IsComparisonDefectManualTable(orignal, comparison))
                {
                    if (!string.IsNullOrEmpty(DefectMapHelper.StringByDataRowObejct(dr, "DEFECTCODE")))
                    {
                        if (DefectMapHelper.StringByDataRowObejct(dr, "FRONTLAYER").Equals(DefectMapHelper.StringByDataRowObejct(dr, "LAYERID")))
                        {
                            newRow[string.Concat("FRONT_", DefectMapHelper.StringByDataRowObejct(dr, "DEFECTCODE"))] = DefectMapHelper.StringByDataRowObejct(dr, "DEFECTCOUNT");
                            newRow["FRONTLAYERPCS"] = DefectMapHelper.IntByDataRowObject(newRow, "FRONTLAYERPCS") + DefectMapHelper.IntByDataRowObject(dr, "REPAIRRESULTQTY");
                            newRow["BACKLAYERPCS"] = DefectMapHelper.IntByDataRowObject(newRow, "BACKLAYERPCS");
                        }
                        else
                        {
                            newRow[string.Concat("BACK_", DefectMapHelper.StringByDataRowObejct(dr, "DEFECTCODE"))] = DefectMapHelper.StringByDataRowObejct(dr, "DEFECTCOUNT");
                            newRow["FRONTLAYERPCS"] = DefectMapHelper.IntByDataRowObject(newRow, "FRONTLAYERPCS");
                            newRow["BACKLAYERPCS"] = DefectMapHelper.IntByDataRowObject(newRow, "BACKLAYERPCS") + DefectMapHelper.IntByDataRowObject(dr, "REPAIRRESULTQTY");
                        }
                    }
                }
                else
                {
                    orignal = comparison;
                    newRow["PCSDEFECTQTY"] = defectCount;
                    newRow["DEFECTRATE"] = DefectMapHelper.SetDefectRate(defectCount, DefectMapHelper.IntByDataRowObject(dr, "WORKSTARTPCSQTY"));
                    defectCount = 0;

                    table.Rows.Add(newRow);
                    newRow = table.NewRow();
                    newRow["INSPECTDATE"] = orignal.INSPECTDATE;
                    newRow["PRODUCTREVISIONINPUT"] = orignal.PRODUCTREVISIONINPUT;
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

                    if (!string.IsNullOrEmpty(DefectMapHelper.StringByDataRowObejct(dr, "DEFECTCODE")))
                    {
                        if (orignal.FRONTLAYER.Equals(DefectMapHelper.StringByDataRowObejct(dr, "LAYERID")))
                        {
                            newRow[string.Concat("FRONT_", DefectMapHelper.StringByDataRowObejct(dr, "DEFECTCODE"))] = DefectMapHelper.StringByDataRowObejct(dr, "DEFECTCOUNT");
                            newRow["FRONTLAYERPCS"] = DefectMapHelper.IntByDataRowObject(newRow, "FRONTLAYERPCS") + DefectMapHelper.IntByDataRowObject(dr, "REPAIRRESULTQTY");
                            newRow["BACKLAYERPCS"] = DefectMapHelper.IntByDataRowObject(newRow, "BACKLAYERPCS");
                        }
                        else
                        {
                            newRow[string.Concat("BACK_", DefectMapHelper.StringByDataRowObejct(dr, "DEFECTCODE"))] = DefectMapHelper.StringByDataRowObejct(dr, "DEFECTCOUNT");
                            newRow["FRONTLAYERPCS"] = DefectMapHelper.IntByDataRowObject(newRow, "FRONTLAYERPCS");
                            newRow["BACKLAYERPCS"] = DefectMapHelper.IntByDataRowObject(newRow, "BACKLAYERPCS") + DefectMapHelper.IntByDataRowObject(dr, "REPAIRRESULTQTY");
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

        #endregion
    }
}
