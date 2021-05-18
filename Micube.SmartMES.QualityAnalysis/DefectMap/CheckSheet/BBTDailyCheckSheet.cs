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
using System.Windows.Forms;
using Micube.SmartMES.Commons;

#endregion

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명 : Defect Map &gt; Check Sheet &gt; BBT 일일 Check Sheet 업 무 설 명 : Lot 단위 BBT 일일 체크시트
    /// 생 성 자 : 전우성 생 성 일 : 2020-02-03 수 정 이 력 :
    /// </summary>
    public partial class BBTDailyCheckSheet : SmartConditionManualBaseForm
    {
        #region Local Variables

        /// <summary>
        /// 불량코드 테이블
        /// </summary>
        private DataTable _defectCode;

        #endregion

        #region 생성자

        public BBTDailyCheckSheet()
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
            _defectCode = DefectMapHelper.GetBBTDefectCodeInfo;

            grdMain.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            var group = grdMain.View.AddGroupColumn("");
            group.AddTextBoxColumn("INSPECTDATE", 50);
            group.AddTextBoxColumn("PRODUCTREVISIONINPUT", 80);
            group.AddTextBoxColumn("PRODUCTDEFVERSION", 80); // 2021-01-08 오근영 (110) 내부Rev 추가
            group.AddTextBoxColumn("MODELPRODUCT", 200);
            group.AddTextBoxColumn("LOTID", 170);
            group.AddTextBoxColumn("INSPECTIONWORKAREA", 150);

            group = grdMain.View.AddGroupColumn("QCMFINALINSPECTQTY");
            group.AddTextBoxColumn("WORKSTARTPANELQTY", 50).SetLabel("PNL").SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("WORKSTARTPCSQTY", 50).SetLabel("PCS").SetTextAlignment(TextAlignment.Right);

            group = grdMain.View.AddGroupColumn("");
            group.AddTextBoxColumn("PNLARRAY", 50).SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("USEDFACTOR", 50).SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("GOODQTY", 50).SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("DEFECTRATE", 50).SetDisplayFormat("{0:f2}%", MaskTypes.Custom).SetTextAlignment(TextAlignment.Right);

            group = grdMain.View.AddGroupColumn("DEFECTCOUNT");
            group.AddTextBoxColumn("TOTALAMOUNT", 50).SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("OPENSUM", 50).SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("SHORTSUM", 50).SetTextAlignment(TextAlignment.Right);

            group = grdMain.View.AddGroupColumn("SHIPMENTINSPLRRDETAILLIST");
            foreach (DataRow dr in _defectCode.Rows)
            {
                switch (DefectMapHelper.StringByDataRowObejct(dr, "CODEID"))
                {
                    case "001":
                    case "011":
                    case "999":
                    case "045":
                    case "046":
                        break;
                    default:
                        group.AddTextBoxColumn(DefectMapHelper.StringByDataRowObejct(dr, "CODEID"), 80)
                             .SetLabel(DefectMapHelper.StringByDataRowObejct(dr, "CODENAME"))
                             .SetTextAlignment(TextAlignment.Right);
                        break;
                }
            }

            group = grdMain.View.AddGroupColumn("");
            group.AddTextBoxColumn("EQUIPMENTNAME", 200).SetLabel("EQUIPMENT");
            group.AddTextBoxColumn("WORKER", 50);
            group.AddTextBoxColumn("TOTALUSEDCOUNT", 50).SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("WORKSTARTTIME", 50);
            group.AddTextBoxColumn("WORKENDTIME", 50);
            group.AddTextBoxColumn("DURABLEDEFID", 50).SetIsHidden();
            group.AddTextBoxColumn("DURABLELOTID", 50); //2020-12-21 (109) 그리드 헤더명 변경(치공구 ID => Tool No.)(DURABLEDEFID => DURABLELOTID)
            group.AddTextBoxColumn("DURABLEDEFVERSION", 50);

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

                if (await SqlExecuter.QueryAsync("GetBbtDailyCheckSheetList", "10002",
                                                 DefectMapHelper.AddLanguageTypeToConditions(Conditions.GetValues())) is DataTable dt)
                {
                    if (dt.Rows.Count.Equals(0))
                    {
                        ShowMessage("NoSelectData");
                        return;
                    }

                    List<string> removeDefect = new List<string>()
                    {
                        "001","011","999","045","046"
                    };

                    var filterDT = dt.AsEnumerable().Where(x => !removeDefect.Contains(x.Field<string>("DEFECTCODE")));

                    if(filterDT.Count().Equals(0))
                    {
                        ShowMessage("NoSelectData");
                        return;
                    }

                    grdMain.DataSource = SetData(filterDT.CopyToDataTable());
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

            // 2021-01-08 오근영 (110) 조회조건 품목 추가
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

            #endregion
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
            if(dt.Rows.Count.Equals(0))
            {
                return dt;
            }

            DataTable table = (grdMain.DataSource as DataTable).Clone();
            DataRow newRow = table.NewRow();
            DataRow dr = dt.Rows[0];
            DefectManualTable orignal = new DefectManualTable();
            DefectManualTable comparison;            
            List<string> equipmentList = new List<string>();
            List<string> workerList = new List<string>();

            int openDefectCount = 0, shortDefectCount = 0, pnl, pcs;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i.Equals(0))
                {
                    comparison = new DefectManualTable()
                    {
                        INSPECTDATE             = DefectMapHelper.StringByDataRowObejct(dr, "INSPECTDATE"           ),
                        PRODUCTREVISIONINPUT    = DefectMapHelper.StringByDataRowObejct(dr, "PRODUCTREVISIONINPUT"  ),
                        MODELPRODUCT            = DefectMapHelper.StringByDataRowObejct(dr, "MODELPRODUCT"          ),
                        LOTID                   = DefectMapHelper.StringByDataRowObejct(dr, "LOTID"                 ),
                        INSPECTIONWORKAREA      = DefectMapHelper.StringByDataRowObejct(dr, "INSPECTIONWORKAREA"    ),
                        WORKSTARTPANELQTY       = DefectMapHelper.StringByDataRowObejct(dr, "WORKSTARTPANELQTY"     ),
                        WORKSTARTPCSQTY         = DefectMapHelper.StringByDataRowObejct(dr, "WORKSTARTPCSQTY"       ),
                        USEDFACTOR              = DefectMapHelper.StringByDataRowObejct(dr, "USEDFACTOR"            ),
                        EQUIPMENTNAME           = DefectMapHelper.StringByDataRowObejct(dr, "EQUIPMENTNAME"         ),
                        WORKER                  = DefectMapHelper.StringByDataRowObejct(dr, "WORKER"                ),
                        WORKSTARTTIME           = DefectMapHelper.StringByDataRowObejct(dr, "WORKSTARTTIME"         ),
                        WORKENDTIME             = DefectMapHelper.StringByDataRowObejct(dr, "WORKENDTIME"           ),
                        DURABLEDEFID            = DefectMapHelper.StringByDataRowObejct(dr, "DURABLEDEFID"          ),
                        DURABLEDEFVERSION       = DefectMapHelper.StringByDataRowObejct(dr, "DURABLEDEFVERSION"     ),
                        DURABLELOTID            = DefectMapHelper.StringByDataRowObejct(dr, "DURABLELOTID"          ),   //2020-12-21 (109) 그리드 헤더명 변경(치공구 ID => Tool No.)(DURABLEDEFID => DURABLELOTID)
                        PRODUCTDEFVERSION      = DefectMapHelper.StringByDataRowObejct(dr, "PRODUCTDEFVERSION"    )    //2021-01-07 (110) 내부 Rev 추가
                    };

                    DefectMapHelper.SetContainsKeyCheck(equipmentList, comparison.EQUIPMENTNAME);
                    DefectMapHelper.SetContainsKeyCheck(workerList, comparison.WORKER);
                    orignal = comparison;

                    newRow["INSPECTDATE"]           = orignal.INSPECTDATE;
                    newRow["PRODUCTREVISIONINPUT"]  = orignal.PRODUCTREVISIONINPUT;
                    newRow["PRODUCTDEFVERSION"]    = orignal.PRODUCTDEFVERSION;   //2021-01-08 오근영 (110) 내부 Rev 추가
                    newRow["MODELPRODUCT"]          = orignal.MODELPRODUCT;
                    newRow["LOTID"]                 = orignal.LOTID;
                    newRow["INSPECTIONWORKAREA"]    = orignal.INSPECTIONWORKAREA;
                    newRow["WORKSTARTPANELQTY"]     = orignal.WORKSTARTPANELQTY;
                    newRow["WORKSTARTPCSQTY"]       = orignal.WORKSTARTPCSQTY;
                    newRow["USEDFACTOR"]            = orignal.USEDFACTOR;
                    //newRow["EQUIPMENTNAME"]       = orignal.EQUIPMENTNAME;
                    //newRow["WORKER"]              = orignal.WORKER;
                    newRow["WORKSTARTTIME"]         = orignal.WORKSTARTTIME;
                    newRow["WORKENDTIME"]           = orignal.WORKENDTIME;
                    newRow["DURABLEDEFID"]          = orignal.DURABLEDEFID;
                    newRow["DURABLELOTID"]          = orignal.DURABLELOTID;         //2020-12-21 (109) 그리드 헤더명 변경(치공구 ID => Tool No.)(DURABLEDEFID => DURABLELOTID)
                    newRow["DURABLEDEFVERSION"]     = orignal.DURABLEDEFID;
                }
                else
                {
                    dr = dt.Rows[i];
                    comparison = new DefectManualTable()
                    {
                        INSPECTDATE             = DefectMapHelper.StringByDataRowObejct(dr, "INSPECTDATE"           ),
                        PRODUCTREVISIONINPUT    = DefectMapHelper.StringByDataRowObejct(dr, "PRODUCTREVISIONINPUT"  ),
                        MODELPRODUCT            = DefectMapHelper.StringByDataRowObejct(dr, "MODELPRODUCT"          ),
                        LOTID                   = DefectMapHelper.StringByDataRowObejct(dr, "LOTID"                 ),
                        INSPECTIONWORKAREA      = DefectMapHelper.StringByDataRowObejct(dr, "INSPECTIONWORKAREA"    ),
                        WORKSTARTPANELQTY       = DefectMapHelper.StringByDataRowObejct(dr, "WORKSTARTPANELQTY"     ),
                        WORKSTARTPCSQTY         = DefectMapHelper.StringByDataRowObejct(dr, "WORKSTARTPCSQTY"       ),
                        USEDFACTOR              = DefectMapHelper.StringByDataRowObejct(dr, "USEDFACTOR"            ),
                        EQUIPMENTNAME           = DefectMapHelper.StringByDataRowObejct(dr, "EQUIPMENTNAME"         ),
                        WORKER                  = DefectMapHelper.StringByDataRowObejct(dr, "WORKER"                ),
                        WORKSTARTTIME           = DefectMapHelper.StringByDataRowObejct(dr, "WORKSTARTTIME"         ),
                        WORKENDTIME             = DefectMapHelper.StringByDataRowObejct(dr, "WORKENDTIME"           ),
                        DURABLEDEFID            = DefectMapHelper.StringByDataRowObejct(dr, "DURABLEDEFID"          ),
                        DURABLEDEFVERSION       = DefectMapHelper.StringByDataRowObejct(dr, "DURABLEDEFVERSION"     ),
                        DURABLELOTID            = DefectMapHelper.StringByDataRowObejct(dr, "DURABLELOTID"          ),   //2020-12-21 (109) 그리드 헤더명 변경(치공구 ID => Tool No.)(DURABLEDEFID => DURABLELOTID)
                        PRODUCTDEFVERSION      = DefectMapHelper.StringByDataRowObejct(dr, "PRODUCTDEFVERSION"    )    //2021-01-08 (110) 내부 Rev 추가
                    };
                }

                if (DefectMapHelper.IsComparisonDefectManualTable(orignal, comparison))
                {
                    if (DefectMapHelper.StringByDataRowObejct(dr, "DEFECTCODE").ToString().Trim().Length > 0)
                    {
                        newRow[DefectMapHelper.StringByDataRowObejct(dr, "DEFECTCODE")] = DefectMapHelper.StringByDataRowObejct(dr, "DEFECTCOUNT");
                        DefectMapHelper.SetContainsKeyCheck(equipmentList, comparison.EQUIPMENTNAME);
                        DefectMapHelper.SetContainsKeyCheck(workerList, comparison.WORKER);
                    }
                }
                else
                {
                    pcs = Convert.ToInt32(orignal.WORKSTARTPCSQTY);
                    pnl = Convert.ToInt32(orignal.WORKSTARTPANELQTY);

                    newRow["PNLARRAY"] = (pcs.Equals(0) ? 1 : pcs) / (pnl.Equals(0) ? 1 : pnl);
                    newRow["TOTALAMOUNT"] = openDefectCount + shortDefectCount;
                    newRow["OPENSUM"] = openDefectCount;
                    newRow["SHORTSUM"] = shortDefectCount;

                    newRow["GOODQTY"] = (pcs - (openDefectCount + shortDefectCount));
                    newRow["DEFECTRATE"] = DefectMapHelper.SetDefectRate(DefectMapHelper.IntByDataRowObject(newRow, "TOTALAMOUNT"), pcs);
                    newRow["TOTALUSEDCOUNT"] = pcs / DefectMapHelper.IntByDataRowObject(newRow, "USEDFACTOR", 1);

                    newRow["EQUIPMENTNAME"] = string.Join(", ", equipmentList.ToArray());
                    newRow["WORKER"] = string.Join(", ", workerList.ToArray());

                    //newRow["EQUIPMENTNAME"] = orignal.EQUIPMENTNAME;
                    //newRow["WORKER"] = orignal.WORKER;

                    orignal = comparison;

                    equipmentList.Clear();
                    workerList.Clear();

                    openDefectCount = 0;
                    shortDefectCount = 0;

                    table.Rows.Add(newRow);
                    newRow = table.NewRow();
                    newRow["INSPECTDATE"] = orignal.INSPECTDATE;
                    newRow["PRODUCTREVISIONINPUT"] = orignal.PRODUCTREVISIONINPUT;
                    newRow["PRODUCTDEFVERSION"] = orignal.PRODUCTDEFVERSION;     //2021-01-08 (110) 내부 Rev 추가
                    newRow["MODELPRODUCT"] = orignal.MODELPRODUCT;
                    newRow["LOTID"] = orignal.LOTID;
                    newRow["INSPECTIONWORKAREA"] = orignal.INSPECTIONWORKAREA;
                    newRow["WORKSTARTPANELQTY"] = orignal.WORKSTARTPANELQTY;
                    newRow["WORKSTARTPCSQTY"] = orignal.WORKSTARTPCSQTY;
                    newRow["USEDFACTOR"] = orignal.USEDFACTOR;
                    newRow["WORKSTARTTIME"] = orignal.WORKSTARTTIME;
                    newRow["WORKENDTIME"] = orignal.WORKENDTIME;
                    newRow["DURABLEDEFID"] = orignal.DURABLEDEFID;
                    newRow["DURABLELOTID"] = orignal.DURABLELOTID;           //2020-12-21 (109) 그리드 헤더명 변경(치공구 ID => Tool No.)(DURABLEDEFID => DURABLELOTID)
                    newRow["DURABLEDEFVERSION"] = orignal.DURABLEDEFID;
                    if (DefectMapHelper.StringByDataRowObejct(dr, "DEFECTCODE").ToString().Trim().Length > 0)
                    {
                        newRow[DefectMapHelper.StringByDataRowObejct(dr, "DEFECTCODE")] = DefectMapHelper.StringByDataRowObejct(dr, "DEFECTCOUNT");

                        DefectMapHelper.SetContainsKeyCheck(equipmentList, comparison.EQUIPMENTNAME);
                        DefectMapHelper.SetContainsKeyCheck(workerList, comparison.WORKER);
                    }
                }

                switch (DefectMapHelper.StringByDataRowObejct(dr, "DEFECTCODE"))
                {
                    case "002":
                    case "004":
                    case "012":
                        openDefectCount += DefectMapHelper.IntByDataRowObject(dr, "DEFECTCOUNT");
                        break;
                    case "003":
                    case "005":
                    case "006":
                    case "007":
                    case "008":
                    case "009":
                    case "010":
                        shortDefectCount += DefectMapHelper.IntByDataRowObject(dr, "DEFECTCOUNT");
                        break;
                    default:
                        break;
                }
            }

            pcs = DefectMapHelper.IntByDataRowObject(newRow, "WORKSTARTPCSQTY");
            pnl = DefectMapHelper.IntByDataRowObject(newRow, "WORKSTARTPANELQTY");

            newRow["PNLARRAY"] = (pcs.Equals(0) ? 1 : pcs) / (pnl.Equals(0) ? 1 : pnl);
            newRow["TOTALAMOUNT"] = openDefectCount + shortDefectCount;
            newRow["OPENSUM"] = openDefectCount;
            newRow["SHORTSUM"] = shortDefectCount;

            newRow["GOODQTY"] = (pcs - (openDefectCount + shortDefectCount));
            newRow["DEFECTRATE"] = DefectMapHelper.SetDefectRate(DefectMapHelper.IntByDataRowObject(newRow, "TOTALAMOUNT"), pcs);
            newRow["TOTALUSEDCOUNT"] = pcs / DefectMapHelper.IntByDataRowObject(newRow, "USEDFACTOR", 1);

            //newRow["EQUIPMENTNAME"] = string.Join(", ", equipmentList.ToArray());
            //newRow["WORKER"] = string.Join(", ", workerList.ToArray());

            table.Rows.Add(newRow);
            //table.Rows.RemoveAt(0);

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
