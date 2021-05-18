#region using

using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion using

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프 로 그 램 명  : 기준정보 > 품목관리 > 부자재 routing BOM H/P 부자재
    /// 업  무  설  명  : 공정이 H/P인 경우에 발생되는 user control
    /// 생    성    자  : 전우성
    /// 생    성    일  : 2021-01-27
    /// 필  수  처  리  :
    ///
    /// </summary>
    public partial class ucSubMaterialByHotPress : ISubMaterialBomRouting
    {
        #region Global Variable

        /// <summary>
        /// 부모에게 받은 Dictionary정보
        /// </summary>
        private Dictionary<string, object> _dic;

        #endregion Global Variable

        #region 생성자

        /// <summary>
        /// 생성자
        /// </summary>
        public ucSubMaterialByHotPress(Dictionary<string, object> parentDic)
        {
            _dic = parentDic;

            InitializeComponent();
            InitializeGrid();
            InitializeEvent();
            InitializeLanguageKey();
        }

        #endregion 생성자

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// Language Key 초기화
        /// </summary>
        private void InitializeLanguageKey()
        {
            grdInfo.Caption = Language.Get("HPINFO");
            grdChange.Caption = Language.Get("CHANGEHISTORY");
            grdNote.Caption = Language.Get("COMMENT");
            grdLayup.Caption = Language.Get("LAYUPLIST");
        }

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            #region Grid H/P 내역

            grdInfo.GridButtonItem = GridButtonItem.None;
            grdInfo.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            grdInfo.View.AddComboBoxColumn("RACKSIZE", 110, new SqlQuery("GetCodeList", "00001", "CODECLASSID=RackSize", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetDefault("370");
            grdInfo.View.AddSpinEditColumn("TEMPC", 100).SetTextAlignment(TextAlignment.Right).SetDefault(0).SetValueRange(0, 999);
            grdInfo.View.AddSpinEditColumn("PRESS", 90).SetLabel("PRESSUREBAR").SetTextAlignment(TextAlignment.Right).SetDefault(0).SetValueRange(0, 999);
            grdInfo.View.AddSpinEditColumn("TIMEMINUTE", 80).SetTextAlignment(TextAlignment.Right).SetDefault(0).SetValueRange(0, 999);
            grdInfo.View.AddSpinEditColumn("COOLINGTEMPC", 110).SetTextAlignment(TextAlignment.Right).SetDefault(0).SetValueRange(0, 999);
            grdInfo.View.AddSpinEditColumn("GAONTIMEMINUTE", 110).SetLabel("GAONTIME").SetTextAlignment(TextAlignment.Right).SetDefault(0).SetValueRange(0, 999);
            grdInfo.View.AddSpinEditColumn("GAMONTIMEMINUTE", 110).SetLabel("GAMONTIME").SetTextAlignment(TextAlignment.Right).SetDefault(0).SetValueRange(0, 999);
            grdInfo.View.AddSpinEditColumn("STACK", 70).SetTextAlignment(TextAlignment.Right).SetDefault(0).SetValueRange(0, 999);
            grdInfo.View.AddComboBoxColumn("SINGLE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=SingleColumn", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetDefault("1");
            grdInfo.View.AddComboBoxColumn("COL", 70, new SqlQuery("GetCodeList", "00001", "CODECLASSID=SingleColumn", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetLabel("COLUMN").SetDefault("1");
            grdInfo.View.AddComboBoxColumn("BOX", 70, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetDefault("N");
            grdInfo.View.AddComboBoxColumn("BAKING", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetDefault("N");

            grdInfo.View.PopulateColumns();

            grdInfo.ShowStatusBar = false;

            #endregion Grid H/P 내역

            #region Grid 변경이력

            grdChange.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete;
            grdChange.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            grdChange.View.AddTextBoxColumn("SEQ").SetIsHidden();
            grdChange.View.AddTextBoxColumn("NOTETYPE").SetDefault("Change").SetIsHidden();
            grdChange.View.AddTextBoxColumn("NO", 40).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdChange.View.AddTextBoxColumn("CHANGEDATE", 130).SetIsReadOnly().SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetTextAlignment(TextAlignment.Center);
            grdChange.View.AddTextBoxColumn("NOTE").SetValidationIsRequired().SetLabel("CHANGEHISTORY");

            grdChange.View.PopulateColumns();

            grdChange.View.SetAutoFillColumn("NOTE");
            grdChange.ShowStatusBar = false;

            #endregion Grid 변경이력

            #region Grid 특이사항

            grdNote.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete;
            grdNote.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            grdNote.View.AddTextBoxColumn("SEQ").SetIsHidden();
            grdNote.View.AddTextBoxColumn("NOTETYPE").SetDefault("Note").SetIsHidden();
            grdNote.View.AddTextBoxColumn("NO", 40).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdNote.View.AddTextBoxColumn("CHANGEDATE", 130).SetIsReadOnly().SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetTextAlignment(TextAlignment.Center);
            grdNote.View.AddTextBoxColumn("NOTE").SetValidationIsRequired().SetLabel("COMMENT");

            grdNote.View.PopulateColumns();

            grdNote.View.SetAutoFillColumn("NOTE");
            grdNote.ShowStatusBar = false;

            #endregion Grid 특이사항

            #region Grid Lay-Up 목록

            grdLayup.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export | GridButtonItem.Expand | GridButtonItem.Restore;
            grdLayup.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            grdLayup.View.AddComboBoxColumn("UOMDEFID", 50, new SqlQuery("GetUomDefinitionList", "10001", "UOMCLASSID=OutsourcingSpec"), "UOMDEFNAME", "UOMDEFID")
                         .SetLabel("UOM")
                         .SetTextAlignment(TextAlignment.Center)
                         .SetDefault("PNL");
            grdLayup.View.AddComboBoxColumn("DETAILTYPE", 70, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OutsourcingSpecVacuumNormal", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                         .SetLabel("VACUUMNORMAL").SetDefault("Normal");
            grdLayup.View.AddComboBoxColumn("SPECSUBTYPE", 110, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OutsourcingSpecStackMaterialType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                         .SetLabel("LAYUPSTRUCT").SetValidationIsRequired();

            #region 자재ID

            var controls = grdLayup.View.AddSelectPopupColumn("MATERIALDEFID", 120, new SqlQuery("GetRoutingBomListPopup", "10002", $"ENTERPRISEID={UserInfo.Current.Enterprise}"))
                                        .SetLabel("MATERIALDEF")
                                        .SetValidationIsRequired()
                                        .SetPopupLayout("SELECTCONSUMABLE", PopupButtonStyles.Ok_Cancel, true, false)
                                        .SetRelationIds("ITEMTYPE")
                                        .SetPopupResultCount(1)
                                        .SetPopupResultMapping("MATERIALDEFID", "COMPONENTITEMID")
                                        .SetPopupLayoutForm(800, 800, FormBorderStyle.FixedToolWindow)
                                        .SetPopupAutoFillColumns("COMPONENTITEMNAME")
                                        .SetPopupApplySelection((selectedRows, dataGridRow) =>
                                        {
                                            selectedRows.ForEach(row =>
                                            {
                                                dataGridRow["MATERIALDEFVERSION"] = row["COMPONENTITEMVERSION"];
                                                dataGridRow["MATERIALNAME"] = row["COMPONENTITEMNAME"];
                                                dataGridRow["COMPONENTUOM"] = row["COMPONENTUOM"];
                                                dataGridRow["SPEC"] = row["SPEC"];
                                            });
                                        });

            // 팝업에서 사용할 조회조건 항목 추가
            controls.Conditions.AddTextBox("P_ITEMID").SetLabel("CONSUMABLEIDNAME");
            controls.Conditions.AddTextBox("P_ITEMTYPE").SetPopupDefaultByGridColumnId("ITEMTYPE").SetIsHidden();

            // 팝업 그리드 설정
            controls.GridColumns.AddTextBoxColumn("COMPONENTITEMID", 100).SetLabel("ITEMID1");
            controls.GridColumns.AddTextBoxColumn("COMPONENTITEMVERSION", 80).SetLabel("ITEMVERSION");
            controls.GridColumns.AddTextBoxColumn("COMPONENTITEMNAME", 250).SetLabel("ITEMNAME1");
            controls.GridColumns.AddComboBoxColumn("COMPONENTUOM", 80, new SqlQuery("GetUOMList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "UOMDEFID", "UOMDEFNAME").SetIsReadOnly();
            controls.GridColumns.AddTextBoxColumn("SPEC", 100);

            #endregion 자재ID

            grdLayup.View.AddTextBoxColumn("MATERIALNAME", 150).SetIsReadOnly();
            grdLayup.View.AddTextBoxColumn("SPEC", 70).SetIsReadOnly();
            grdLayup.View.AddComboBoxColumn("COMPONENTUOM", 70, new SqlQuery("GetUOMList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "UOMDEFID", "UOMDEFNAME").SetIsReadOnly();
            grdLayup.View.AddSpinEditColumn("MINVALUE", 90).SetDisplayFormat("###,###,##0.#########", MaskTypes.Numeric).SetTextAlignment(TextAlignment.Right)
                         .SetLabel("COMPONENTQTY").SetDefault(0);
            grdLayup.View.AddTextBoxColumn("REALQTY", 60).SetDefault(0).SetTextAlignment(TextAlignment.Right).SetIsReadOnly();

            grdLayup.View.AddTextBoxColumn("OUTSOURCINGSPECNO").SetIsHidden();
            grdLayup.View.AddTextBoxColumn("MATERIALDEFVERSION").SetIsHidden();
            grdLayup.View.AddTextBoxColumn("ITEMTYPE").SetDefault("0").SetIsHidden();
            grdLayup.View.AddTextBoxColumn("P_SPECTYPE").SetIsHidden();

            grdLayup.View.PopulateColumns();

            grdLayup.ShowStatusBar = true;

            #endregion Grid Lay-Up 목록
        }

        #endregion 컨텐츠 영역 초기화

        #region Event

        /// <summary>
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            // H/P 내역이 존재하지 않으면 나머지 Grid에 Row추가 불가
            grdChange.ToolbarAddingRow += (s, e) => e.Cancel = grdInfo.View.DataRowCount.Equals(0);
            grdNote.ToolbarAddingRow += (s, e) => e.Cancel = grdInfo.View.DataRowCount.Equals(0);
            grdLayup.ToolbarAddingRow += (s, e) => e.Cancel = grdInfo.View.DataRowCount.Equals(0);

            // H/P 내역 값 변경 이벤트
            grdInfo.View.CellValueChanged += (s, e) =>
            {
                if (e.RowHandle < 0 || !(e.Column.FieldName.Equals("SINGLE") || e.Column.FieldName.Equals("COL")) || grdLayup.View.DataRowCount.Equals(0))
                {
                    return;
                }

                foreach (DataRow dr in (grdLayup.DataSource as DataTable).Rows)
                {
                    dr["REALQTY"] = Format.GetDouble(dr["MINVALUE"], 0) /
                                        (Format.GetInteger(grdInfo.View.GetRowCellValue(0, "SINGLE"), 0) *
                                            Format.GetInteger(grdInfo.View.GetRowCellValue(0, "COL"), 0));
                }
            };

            // LayUp 내역 Row 추가 이벤트
            grdLayup.View.InitNewRow += (s, e) =>
                grdLayup.View.SetRowCellValue(e.RowHandle, "OUTSOURCINGSPECNO", grdLayup.View.DataRowCount.Equals(0) ? 1 : GetLayupMaxOperNo());

            // LayUp 내역 값 변경 이벤트
            grdLayup.View.CellValueChanged += (s, e) =>
            {
                if (e.RowHandle < 0 || !(e.Column.FieldName.Equals("MINVALUE") || e.Column.FieldName.Equals("SPECSUBTYPE")) || grdInfo.View.DataRowCount.Equals(0))
                {
                    return;
                }

                switch (e.Column.FieldName)
                {
                    case "SPECSUBTYPE":
                        string str = Format.GetString(e.Value, string.Empty);
                        grdLayup.View.SetRowCellValue(e.RowHandle, "ITEMTYPE", Format.GetInteger(str.Substring(str.Length - 3), 5) < 5 ? 1 : 0);
                        grdLayup.View.SetRowCellValue(e.RowHandle, "MATERIALDEFVERSION", string.Empty);
                        grdLayup.View.SetRowCellValue(e.RowHandle, "MATERIALNAME", string.Empty);
                        grdLayup.View.SetRowCellValue(e.RowHandle, "COMPONENTUOM", string.Empty);
                        grdLayup.View.SetRowCellValue(e.RowHandle, "SPEC", string.Empty);
                        break;

                    case "MINVALUE":
                        grdLayup.View.SetRowCellValue(e.RowHandle, "REALQTY", Format.GetDouble(e.Value, 0) / (Format.GetInteger(grdInfo.View.GetRowCellValue(0, "SINGLE"), 0) * Format.GetInteger(grdInfo.View.GetRowCellValue(0, "COL"), 0)));
                        break;
                }
            };

            // LayUp 내역 자재ID 삭제 이벤트
            grdLayup.View.Columns.ForEach(control =>
            {
                if (!control.ColumnEdit.GetType().Name.Equals("RepositoryItemButtonEdit"))
                {
                    return;
                }

                // Grid에 자재ID 삭제버튼 클릭시 이벤트
                (control.ColumnEdit as RepositoryItemButtonEdit).ButtonClick += (s, e) =>
                {
                    if (e.Button.Kind.Equals(ButtonPredefines.Clear))
                    {
                        grdLayup.View.SetRowCellValue(grdLayup.View.GetFocusedDataSourceRowIndex(), "MATERIALDEFVERSION", string.Empty);
                        grdLayup.View.SetRowCellValue(grdLayup.View.GetFocusedDataSourceRowIndex(), "MATERIALNAME", string.Empty);
                        grdLayup.View.SetRowCellValue(grdLayup.View.GetFocusedDataSourceRowIndex(), "COMPONENTUOM", string.Empty);
                        grdLayup.View.SetRowCellValue(grdLayup.View.GetFocusedDataSourceRowIndex(), "SPEC", string.Empty);
                    }
                };

                // Grid에 자재ID text 삭제시 이벤트
                control.ColumnEdit.EditValueChanged += (s, e) =>
                {
                    if (Format.GetFullTrimString((s as DevExpress.XtraEditors.ButtonEdit).EditValue).Equals(string.Empty))
                    {
                        grdLayup.View.SetRowCellValue(grdLayup.View.GetFocusedDataSourceRowIndex(), "MATERIALDEFVERSION", string.Empty);
                        grdLayup.View.SetRowCellValue(grdLayup.View.GetFocusedDataSourceRowIndex(), "MATERIALNAME", string.Empty);
                        grdLayup.View.SetRowCellValue(grdLayup.View.GetFocusedDataSourceRowIndex(), "COMPONENTUOM", string.Empty);
                        grdLayup.View.SetRowCellValue(grdLayup.View.GetFocusedDataSourceRowIndex(), "SPEC", string.Empty);
                    }
                };
            });
        }

        #endregion Event

        #region private Fuction

        /// <summary>
        /// Layup OutsourcingSpecNo 값 생성
        /// </summary>
        private int GetLayupMaxOperNo()
            => grdLayup.View.DataRowCount.Equals(0) ? 1 :(grdLayup.DataSource as DataTable).AsEnumerable().Max(x => Format.GetInteger(x["OUTSOURCINGSPECNO"])) + 1;

        /// <summary>
        /// DataRow에 값을 입력한다.
        /// </summary>
        /// <param name="dr"></param>
        private void SetRowData(ref DataRow dr)
        {
            dr["MATERIALTYPE"] = _dic["SUBMATERIALTYPE"];
            dr["PROCESSSEGMENTCLASSID"] = _dic["PROCESSSEGMENTCLASSID"];
            dr["PROCESSSEGMENTID"] = _dic["PROCESSSEGMENTID"];
            dr["PRODUCTDEFID"] = _dic["ITEMID"];
            dr["PRODUCTDEFVERSION"] = _dic["ITEMVERSION"];
            dr["ENTERPRISEID"] = _dic["ENTERPRISEID"];
            dr["PLANTID"] = _dic["PLANTID"];
        }

        #endregion private Fuction

        #region Public Function

        /// <summary>
        /// 부모화면에서 선택한 공정에 따른 내용을 조회한다.
        /// </summary>
        /// <returns></returns>
        public override async Task Search()
        {
            try
            {
                DialogManager.ShowWaitArea(this);

                grdInfo.View.ClearDatas();
                grdChange.View.ClearDatas();
                grdNote.View.ClearDatas();
                grdLayup.View.ClearDatas();

                if (await SqlExecuter.QueryAsync("SelectSubMaterialDivisionByHPBOMRouting", "10001", _dic) is DataTable dt)
                {
                    grdInfo.DataSource = dt;
                    
                    // 등록된 HP가 없다면 무조건 1Row 추가
                    if (dt.Rows.Count.Equals(0))
                    {
                        DataRow dr = (grdInfo.DataSource as DataTable).NewRow();
                        dr["RACKSIZE"] = "370";
                        dr["TIMEMINUTE"] = 0;
                        dr["PRESS"] = 0;
                        dr["TEMPC"] = 0;
                        dr["COOLINGTEMPC"] = 0;
                        dr["GAONTIMEMINUTE"] = 0;
                        dr["GAMONTIMEMINUTE"] = 0;
                        dr["STACK"] = 0;
                        dr["SINGLE"] = "1";
                        dr["COL"] = "1";
                        dr["BOX"] = "N";
                        dr["BAKING"] = "N";
                        (grdInfo.DataSource as DataTable).Rows.Add(dr.ItemArray);
                    }

                    _dic.Add("NOTETYPE", "Change");
                    grdChange.DataSource = await SqlExecuter.QueryAsync("SelectSubMaterialDivisionByHPBOMRoutingNote", "10001", _dic);

                    _dic["NOTETYPE"] = "Note";
                    grdNote.DataSource = await SqlExecuter.QueryAsync("SelectSubMaterialDivisionByHPBOMRoutingNote", "10001", _dic);

                    _dic.Add("SINGLE", dt.Rows[0]["SINGLE"]);
                    _dic.Add("COL", dt.Rows[0]["COL"]);
                    grdLayup.DataSource = await SqlExecuter.QueryAsync("SelectSubMaterialDivisionByHPBOMRoutingLayup", "10001", _dic);
                }
            }
            catch (Exception ex)
            {
                throw MessageException.Create(Format.GetString(ex));
            }
            finally
            {
                DialogManager.CloseWaitArea(this);
            }
        }

        /// <summary>
        /// 부모에게 Data 전달
        /// </summary>
        /// <returns></returns>
        public override DataSet GetData()
        {
            DataSet ds = new DataSet();

            #region H/P 내역 부분

            // Row Count가 0이거나 1이라 IF처리 안했음
            foreach (DataRow dr in grdInfo.GetChangedRows().Rows)
            {
                DataTable dt = SetDataTableHP();

                DataRow newRow = dt.NewRow();

                SetRowData(ref newRow);
                newRow["ROOT_ASSEMBLYITEMID"] = _dic["ROOT_ASSEMBLYITEMID"];
                newRow["ROOT_ASSEMBLYITEMVERSION"] = _dic["ROOT_ASSEMBLYITEMVERSION"];
                newRow["RACKSIZE"] = Format.GetString(dr["RACKSIZE"], string.Empty);
                newRow["TIMEMINUTE"] = Format.GetDouble(dr["TIMEMINUTE"], 0);
                newRow["PRESS"] = Format.GetDouble(dr["PRESS"], 0);
                newRow["TEMPC"] = Format.GetDouble(dr["TEMPC"], 0);
                newRow["COOLINGTEMPC"] = Format.GetDouble(dr["COOLINGTEMPC"], 0);
                newRow["GAONTIMEMINUTE"] = Format.GetDouble(dr["GAONTIMEMINUTE"], 0);
                newRow["GAMONTIMEMINUTE"] = Format.GetDouble(dr["GAMONTIMEMINUTE"], 0);
                newRow["STACK"] = Format.GetDouble(dr["STACK"], 0);
                newRow["SINGLE"] = Format.GetDouble(dr["SINGLE"], 0);
                newRow["COL"] = Format.GetDouble(dr["COL"], 0);
                newRow["BOX"] = Format.GetString(dr["BOX"], string.Empty);
                newRow["BAKING"] = Format.GetString(dr["BAKING"], string.Empty);
                newRow["_STATE_"] = Format.GetString(dr["_STATE_"]);

                dt.Rows.Add(newRow.ItemArray);
                ds.Tables.Add(dt);
            }

            #endregion H/P 내역 부분

            #region 변역이력 / 특이사항 부분

            if (!grdChange.GetChangedRows().Rows.Count.Equals(0) || !grdNote.GetChangedRows().Rows.Count.Equals(0))
            {
                DataTable dt = SetDataTablePrimaryKey();
                dt.Columns.Add("SEQ", typeof(int));
                dt.Columns.Add("NOTETYPE", typeof(string));
                dt.Columns.Add("NOTE", typeof(string));

                dt.TableName = "note";

                DataRow newRow;

                foreach (DataRow dr in grdChange.GetChangedRows().Rows)
                {
                    newRow = dt.NewRow();
                    SetRowData(ref newRow);
                    newRow["SEQ"] = dr["SEQ"];
                    newRow["NOTETYPE"] = dr["NOTETYPE"];
                    newRow["NOTE"] = dr["NOTE"];
                    newRow["_STATE_"] = dr["_STATE_"];
                    dt.Rows.Add(newRow.ItemArray);
                }

                foreach (DataRow dr in grdNote.GetChangedRows().Rows)
                {
                    newRow = dt.NewRow();
                    SetRowData(ref newRow);
                    newRow["SEQ"] = dr["SEQ"];
                    newRow["NOTETYPE"] = dr["NOTETYPE"];
                    newRow["NOTE"] = dr["NOTE"];
                    newRow["_STATE_"] = dr["_STATE_"];
                    dt.Rows.Add(newRow.ItemArray);
                }

                ds.Tables.Add(dt);
            }

            #endregion 변역이력 / 특이사항 부분

            #region LayUp 목록 부분

            if (!grdLayup.GetChangedRows().Rows.Count.Equals(0))
            {

                DataTable dt = SetDataTablePrimaryKey();
                dt.Columns.Add("UOMDEFID", typeof(string));
                dt.Columns.Add("DETAILTYPE", typeof(string));
                dt.Columns.Add("SPECSUBTYPE", typeof(string));
                dt.Columns.Add("MATERIALDEFID", typeof(string));
                dt.Columns.Add("MATERIALDEFVERSION", typeof(string));
                dt.Columns.Add("MINVALUE", typeof(double));
                dt.Columns.Add("OUTSOURCINGSPECNO", typeof(string));
                dt.Columns.Add("P_SPECTYPE", typeof(string));

                dt.TableName = "list";

                DataRow newRow;

                foreach (DataRow dr in grdLayup.GetChangedRows().Rows)
                {
                    newRow = dt.NewRow();
                    SetRowData(ref newRow);
                    newRow["UOMDEFID"] = dr["UOMDEFID"];
                    newRow["DETAILTYPE"] = dr["DETAILTYPE"];
                    newRow["SPECSUBTYPE"] = dr["SPECSUBTYPE"];
                    newRow["MATERIALDEFID"] = dr["MATERIALDEFID"];
                    newRow["MATERIALDEFVERSION"] = dr["MATERIALDEFVERSION"];
                    newRow["MINVALUE"] = dr["MINVALUE"];
                    newRow["OUTSOURCINGSPECNO"] = dr["OUTSOURCINGSPECNO"];
                    newRow["_STATE_"] = dr["_STATE_"];
                    newRow["P_SPECTYPE"] = string.Empty;
                    dt.Rows.Add(newRow.ItemArray);
                }

                ds.Tables.Add(dt);
            }

            #endregion LayUp 목록 부분

            return ds;
        }

        /// <summary>
        /// 복사 버튼 클릭이벤트
        /// </summary>
        /// <returns></returns>
        public override void SetDateAsync()
        {
            popupSubmaterialRoutingHpCopy popup = new popupSubmaterialRoutingHpCopy()
            {
                StartPosition = FormStartPosition.CenterParent
            };

            // TableSet에는 2개의 Table이 존재하며, 첫번째가 Info, 두번째가 Layup
            // popupSubmaterialRoutingHpCopy > btnOK.Click Event;
            popup.SelectedRowTableSetEvent += (ds) =>
            {
                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    if (i.Equals(0))
                    {
                        // 무조건 Row가 존재함
                        DataRow dr = ds.Tables[i].Rows[0];
                        grdInfo.View.SetRowCellValue(0, "RACKSIZE", dr["RACKSIZE"]);
                        grdInfo.View.SetRowCellValue(0, "TIMEMINUTE", dr["TIMEMINUTE"]);
                        grdInfo.View.SetRowCellValue(0, "PRESS", dr["PRESS"]);
                        grdInfo.View.SetRowCellValue(0, "TEMPC", dr["TEMPC"]);
                        grdInfo.View.SetRowCellValue(0, "COOLINGTEMPC", dr["COOLINGTEMPC"]);
                        grdInfo.View.SetRowCellValue(0, "GAONTIMEMINUTE", dr["GAONTIMEMINUTE"]);
                        grdInfo.View.SetRowCellValue(0, "GAMONTIMEMINUTE", dr["GAMONTIMEMINUTE"]);
                        grdInfo.View.SetRowCellValue(0, "STACK", dr["STACK"]);
                        grdInfo.View.SetRowCellValue(0, "SINGLE", dr["SINGLE"]);
                        grdInfo.View.SetRowCellValue(0, "COL", dr["COL"]);
                        grdInfo.View.SetRowCellValue(0, "BOX", dr["BOX"]);
                        grdInfo.View.SetRowCellValue(0, "BAKING", dr["BAKING"]);
                    }
                    else
                    {
                        DataRow newRow;

                        foreach (DataRow dr in ds.Tables[i].Rows)
                        {
                            newRow = (grdLayup.DataSource as DataTable).NewRow();
                            newRow["UOMDEFID"] = dr["UOMDEFID"];
                            newRow["DETAILTYPE"] = dr["DETAILTYPE"];
                            newRow["SPECSUBTYPE"] = dr["SPECSUBTYPE"];
                            newRow["MATERIALDEFID"] = dr["MATERIALDEFID"];
                            newRow["MATERIALNAME"] = dr["MATERIALNAME"];
                            newRow["SPEC"] = dr["SPEC"];
                            newRow["COMPONENTUOM"] = dr["COMPONENTUOM"];
                            newRow["MINVALUE"] = dr["MINVALUE"];
                            newRow["REALQTY"] = dr["REALQTY"];
                            newRow["OUTSOURCINGSPECNO"] = GetLayupMaxOperNo();
                            newRow["MATERIALDEFVERSION"] = dr["MATERIALDEFVERSION"];
                            newRow["ITEMTYPE"] = dr["ITEMTYPE"];
                            (grdLayup.DataSource as DataTable).Rows.Add(newRow.ItemArray);
                        }
                    }
                }
            };

            popup.ShowDialog();
        }

        /// <summary>
        /// 변경 내용 체크
        /// </summary>
        /// <returns></returns>
        public override void Validation()
        {
            grdChange.View.CheckValidation();
            grdNote.View.CheckValidation();
            grdLayup.View.CheckValidation();
        }

        #endregion Public Function
    }
}