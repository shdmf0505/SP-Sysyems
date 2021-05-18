#region using

using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
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
    /// 프 로 그 램 명  : 기준정보 > 품목관리 > 부자재 routing BOM D/F 부자재
    /// 업  무  설  명  : 공정이 D/F인 경우에 발생되는 user control
    /// 생    성    자  : 전우성
    /// 생    성    일  : 2021-01-12
    /// 필  수  처  리  :
    ///
    /// </summary>
    public partial class ucSubMaterialByDryFilim : ISubMaterialBomRouting
    {
        #region Global Variable

        /// <summary>
        /// SelectPopup에 들어가는 parameter 전역 변수
        /// </summary>
        private readonly Dictionary<string, object> _param = new Dictionary<string, object>
        {
            { "LANGUAGETYPE", UserInfo.Current.LanguageType },
            { "ENTERPRISEID", UserInfo.Current.Enterprise },
            { "PLANTID", UserInfo.Current.Plant }
        };

        /// <summary>
        /// 부모에게 받은 Dictionary정보
        /// </summary>
        private Dictionary<string, object> _dic;

        #endregion Global Variable

        #region 생성자

        /// <summary>
        /// 생성자
        /// </summary>
        public ucSubMaterialByDryFilim(Dictionary<string, object> parentDic)
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
            grdMain.Caption = Language.Get("DRYFILIM");
        }

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            grdMain.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export;
            grdMain.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            grdMain.View.AddComboBoxColumn("DRYFILIMNO", 50, new SqlQuery("GetCodeList", "00001", "CODECLASSID=DRYFILIMNO", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetLabel("NO");
            grdMain.View.AddComboBoxColumn("DRYFILIMTYPE", 50, new SqlQuery("GetCodeList", "00001", "CODECLASSID=STICKTYPE", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetLabel("TYPE").SetIsReadOnly();
            grdMain.View.AddComboBoxColumn("STICKMETHOD", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=STICKMETHOD", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsReadOnly();
            grdMain.View.AddComboBoxColumn("STICKDIRECTION", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=STICKDIRECTION", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("LOSSPERLOGIC", 100).SetIsReadOnly().SetTextAlignment(TextAlignment.Right);
            grdMain.View.AddTextBoxColumn("LOSSPER", 80).SetDisplayFormat("{0:n4}%").SetIsReadOnly().SetTextAlignment(TextAlignment.Right);

            #region Grid 자재ID 설정

            var condition = grdMain.View.AddSelectPopupColumn("MATERIALID", 140, new SqlQuery("GetMaterialID", "10002", _param))
                                        .SetValidationIsRequired()
                                        .SetPopupLayout("CONSUMABLE", PopupButtonStyles.Ok_Cancel, true, true)
                                        .SetPopupLayoutForm(700, 500, FormBorderStyle.FixedToolWindow)
                                        .SetPopupResultCount(1)
                                        .SetPopupAutoFillColumns("CONSUMABLEDEFNAME")
                                        .SetLabel("CONSUMABLEDEFID")
                                        .SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGridRow) =>
                                        {
                                            selectedRow.ForEach(row =>
                                            {
                                                dataGridRow["CONSUMABLEDEFNAME"] = row["CONSUMABLEDEFNAME"];
                                                dataGridRow["PNLAREA"] = row["MATERIALWIDTH"];
                                            });

                                            QtyAnalysis(dataGridRow);
                                        });

            condition.Conditions.AddTextBox("P_MATERIALID").SetLabel("CONSUMABLEIDNAME");

            condition.GridColumns.AddTextBoxColumn("MATERIALID", 90).SetLabel("CONSUMABLEDEFID");
            condition.GridColumns.AddTextBoxColumn("CONSUMABLEDEFNAME", 170);
            condition.GridColumns.AddComboBoxColumn("STOCKUNIT", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ReceivePayOutUnit", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            condition.GridColumns.AddTextBoxColumn("MATERIALWIDTH", 10).SetIsHidden();

            #endregion Grid 자재ID 설정

            grdMain.View.AddTextBoxColumn("CONSUMABLEDEFNAME", 120).SetIsReadOnly();
            grdMain.View.AddComboBoxColumn("WORKPLANE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=DoubleSingleSided", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            #region Grid 설비ID 설정

            condition = grdMain.View.AddSelectPopupColumn("EQUIPMENTID", 100, new SqlQuery("GetEquipmentAndArea", "10001", _param))
                                    .SetValidationKeyColumn()
                                    .SetValidationIsRequired()
                                    .SetPopupLayout("EQUIPMENTID", PopupButtonStyles.Ok_Cancel, true, true)
                                    .SetPopupLayoutForm(600, 400, FormBorderStyle.FixedToolWindow)
                                    .SetPopupResultCount(1)
                                    .SetPopupAutoFillColumns("EQUIPMENTNAME")
                                    .SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGridRow) =>
                                    {
                                        selectedRow.ForEach(row => { dataGridRow["EQUIPMENTNAME"] = row["EQUIPMENTNAME"]; });
                                    });

            condition.Conditions.AddTextBox("P_EQUIPMENT").SetLabel("EQUIPMENTIDNAME");

            condition.GridColumns.AddTextBoxColumn("EQUIPMENTID", 80);
            condition.GridColumns.AddTextBoxColumn("EQUIPMENTNAME", 120);
            condition.GridColumns.AddTextBoxColumn("AREANAME", 150);

            #endregion Grid 설비ID 설정

            grdMain.View.AddTextBoxColumn("EQUIPMENTNAME", 150).SetIsReadOnly();
            grdMain.View.AddSpinEditColumn("QTYM2PNL", 120).SetDisplayFormat("###,###,##0.#########", MaskTypes.Numeric).SetIsReadOnly().SetTextAlignment(TextAlignment.Right);
            grdMain.View.AddComboBoxColumn("CIRCUITDIVISION", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=CircuitDivision", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdMain.View.AddComboBoxColumn("STANDARDDIVISION", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=StandardDivision", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdMain.View.AddTextBoxColumn("CREATOR", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdMain.View.AddTextBoxColumn("CREATEDTIME", 130).SetIsReadOnly().SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetTextAlignment(TextAlignment.Center);
            grdMain.View.AddTextBoxColumn("MODIFIER", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdMain.View.AddTextBoxColumn("MODIFIEDTIME", 130).SetIsReadOnly().SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetTextAlignment(TextAlignment.Center);

            grdMain.View.AddTextBoxColumn("MATERIALTYPE").SetIsHidden();
            grdMain.View.AddTextBoxColumn("ROOT_ASSEMBLYITEMID").SetIsHidden();
            grdMain.View.AddTextBoxColumn("ROOT_ASSEMBLYITEMVERSION").SetIsHidden();
            grdMain.View.AddTextBoxColumn("PRODUCTDEFID").SetIsHidden();
            grdMain.View.AddTextBoxColumn("PRODUCTDEFVERSION").SetIsHidden();
            grdMain.View.AddTextBoxColumn("PNLX").SetIsHidden();
            grdMain.View.AddTextBoxColumn("PNLY").SetIsHidden();
            grdMain.View.AddTextBoxColumn("PNLAREA").SetIsHidden();
            grdMain.View.AddTextBoxColumn("PROCESSSEGMENTID").SetIsHidden();
            grdMain.View.AddTextBoxColumn("PROCESSSEGMENTCLASSID").SetIsHidden();
            grdMain.View.AddTextBoxColumn("ENTERPRISEID").SetIsHidden();
            grdMain.View.AddTextBoxColumn("PLANTID").SetIsHidden();

            grdMain.View.PopulateColumns();

            grdMain.ShowStatusBar = true;

            grdMain.View.Columns["DRYFILIMNO"].AppearanceCell.BackColor = Color.AntiqueWhite;
            grdMain.View.Columns["WORKPLANE"].AppearanceCell.BackColor = Color.AntiqueWhite;
            grdMain.View.Columns["CIRCUITDIVISION"].AppearanceCell.BackColor = Color.AntiqueWhite;
            grdMain.View.Columns["STANDARDDIVISION"].AppearanceCell.BackColor = Color.AntiqueWhite;
        }

        #endregion 컨텐츠 영역 초기화

        #region Event

        /// <summary>
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            // Grid에 신규Row 발생시 이벤트
            grdMain.View.AddingNewRow += (s, e) =>
            {
                e.NewRow["DRYFILIMNO"] = "NO1";
                e.NewRow["WORKPLANE"] = "Double";
                e.NewRow["CIRCUITDIVISION"] = "Circuit";
                e.NewRow["STANDARDDIVISION"] = "Standard";

                e.NewRow["MATERIALTYPE"] = "DF";
                e.NewRow["ROOT_ASSEMBLYITEMID"] = _dic["ROOT_ASSEMBLYITEMID"];
                e.NewRow["ROOT_ASSEMBLYITEMVERSION"] = _dic["ROOT_ASSEMBLYITEMVERSION"];
                e.NewRow["PRODUCTDEFID"] = _dic["ITEMID"];
                e.NewRow["PRODUCTDEFVERSION"] = _dic["ITEMVERSION"];
                e.NewRow["PNLX"] = _dic["PNLX"];
                e.NewRow["PNLY"] = _dic["PNLY"];
                e.NewRow["PROCESSSEGMENTID"] = _dic["PROCESSSEGMENTID"];
                e.NewRow["PROCESSSEGMENTCLASSID"] = _dic["PROCESSSEGMENTCLASSID"];
                e.NewRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                e.NewRow["PLANTID"] = UserInfo.Current.Plant;

                QtyAnalysis(e.NewRow);
            };

            // No, 작업면 변경시 이벤트
            grdMain.View.CellValueChanged += (s, e) =>
            {
                if (e.Column.FieldName.Equals("DRYFILIMNO") || e.Column.FieldName.Equals("WORKPLANE"))
                {
                    QtyAnalysis(grdMain.View.GetFocusedDataRow());
                }
            };

            grdMain.View.Columns.ForEach(control =>
            {
                if (!control.ColumnEdit.GetType().Name.Equals("RepositoryItemButtonEdit"))
                {
                    return;
                }

                // Grid에 설비ID, 자재ID 삭제버튼 클릭시 이벤트
                (control.ColumnEdit as RepositoryItemButtonEdit).ButtonClick += (s, e) =>
                {
                    if (e.Button.Kind.Equals(ButtonPredefines.Clear))
                    {
                        switch (control.FieldName)
                        {
                            case "EQUIPMENTID":
                                grdMain.View.SetRowCellValue(grdMain.View.GetFocusedDataSourceRowIndex(), "EQUIPMENTNAME", string.Empty);
                                grdMain.View.SetRowCellValue(grdMain.View.GetFocusedDataSourceRowIndex(), "AREANAME", string.Empty);
                                break;

                            case "MATERIALID":
                                grdMain.View.SetRowCellValue(grdMain.View.GetFocusedDataSourceRowIndex(), "CONSUMABLEDEFNAME", string.Empty);
                                grdMain.View.SetRowCellValue(grdMain.View.GetFocusedDataSourceRowIndex(), "PNLAREA", string.Empty);
                                QtyAnalysis(grdMain.View.GetFocusedDataRow());
                                break;
                        }
                    }
                };

                // Grid에 설비ID, 자재ID text 삭제시 이벤트
                control.ColumnEdit.EditValueChanged += (s, e) =>
                {
                    if (Format.GetFullTrimString((s as DevExpress.XtraEditors.ButtonEdit).EditValue).Equals(string.Empty))
                    {
                        switch (control.FieldName)
                        {
                            case "EQUIPMENTID":
                                grdMain.View.SetRowCellValue(grdMain.View.GetFocusedDataSourceRowIndex(), "EQUIPMENTNAME", string.Empty);
                                grdMain.View.SetRowCellValue(grdMain.View.GetFocusedDataSourceRowIndex(), "AREANAME", string.Empty);
                                break;

                            case "MATERIALID":
                                grdMain.View.SetRowCellValue(grdMain.View.GetFocusedDataSourceRowIndex(), "CONSUMABLEDEFNAME", string.Empty);
                                grdMain.View.SetRowCellValue(grdMain.View.GetFocusedDataSourceRowIndex(), "PNLAREA", string.Empty);
                                QtyAnalysis(grdMain.View.GetFocusedDataRow());
                                break;
                        }
                    }
                };
            });
        }

        #endregion Event

        #region Private Function

        /// 소요량 계산
        /// Dry Filim No변경시, 자재ID 변경시, 작업면 변경시 발생
        /// </summary>
        /// <param name="dr">DataRow</param>
        private void QtyAnalysis(DataRow dr)
        {
            double qty = 0;

            switch (Format.GetString(dr["DRYFILIMNO"], string.Empty))
            {
                // (Roll폭*MD*1.037*2)/1,000,000
                case "NO1":
                    dr["DRYFILIMTYPE"] = "RR";
                    dr["STICKMETHOD"] = "Roll";
                    dr["STICKDIRECTION"] = "General";
                    dr["LOSSPERLOGIC"] = "3.7";
                    dr["LOSSPER"] = "3.7";
                    qty = (Format.GetDouble(dr["PNLAREA"], 0) * Format.GetDouble(_dic["PNLY"], 0) * 1.037) / 1000000;
                    break;

                //(Roll폭 * (MD + 30) * 1.025 * 2) / 1,000,000
                case "NO2":
                    dr["DRYFILIMTYPE"] = "ST";
                    dr["STICKMETHOD"] = "Roll";
                    dr["STICKDIRECTION"] = "MD";
                    dr["LOSSPERLOGIC"] = "MD + 30mm + 2.5";
                    dr["LOSSPER"] = Format.GetString((Format.GetDouble(_dic["PNLY"], 0) + 30) * 0.025, string.Empty);
                    qty = (Format.GetDouble(dr["PNLAREA"], 0) * (Format.GetDouble(_dic["PNLY"], 0) + 30) * 1.025) / 1000000;
                    break;

                //(Roll폭*(TD+30)*1.025*2)/1,000,000
                case "NO3":
                    dr["DRYFILIMTYPE"] = "ST";
                    dr["STICKMETHOD"] = "Roll";
                    dr["STICKDIRECTION"] = "TD";
                    dr["LOSSPERLOGIC"] = "TD + 30mm + 2.5";
                    dr["LOSSPER"] = Format.GetString((Format.GetDouble(_dic["PNLX"], 0) + 30) * 0.025, string.Empty);
                    qty = (Format.GetDouble(dr["PNLAREA"], 0) * (Format.GetDouble(_dic["PNLX"], 0) + 30) * 1.025) / 1000000;
                    break;

                //(Roll폭*MD*1.025*2)/1,000,000
                case "NO4":
                    dr["DRYFILIMTYPE"] = "ST";
                    dr["STICKMETHOD"] = "Vacuum";
                    dr["STICKDIRECTION"] = "General";
                    dr["LOSSPERLOGIC"] = "2.5";
                    dr["LOSSPER"] = "2.5";
                    qty = (Format.GetDouble(dr["PNLAREA"], 0) * Format.GetDouble(_dic["PNLY"], 0) * 1.025) / 1000000;
                    break;

                default:
                    break;
            }

            qty = Format.GetString(dr["WORKPLANE"], string.Empty).Equals("Double") ? qty * 2 : qty;

            dr["QTYM2PNL"] = qty;
        }

        #endregion Private Function

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

                grdMain.View.ClearDatas();

                if (await SqlExecuter.QueryAsync("SelectSubMaterialDivisionByDryFilimBOMRouting", "10001", _dic) is DataTable dt)
                {
                    if (dt.Rows.Count.Equals(0))
                    {
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
            DataTable dt = grdMain.GetChangedRows();
            dt.TableName = "info";

            ds.Tables.Add(dt);

            return ds;
        }

        /// <summary>
        /// 복사 버튼 클릭이벤트
        /// </summary>
        /// <returns></returns>
        public override void SetDateAsync()
        {
            // 설비가 없는 항목이 있다면 모두 삭제

            popupSubmaterialRoutingCopy popup = new popupSubmaterialRoutingCopy("DF")
            {
                StartPosition = FormStartPosition.CenterParent
            };

            popup.SelectedRowTableEvent += (dt) =>
            {
                DataTable rootDt = grdMain.DataSource as DataTable;
                DataRow newRow = null;

                foreach (DataRow dr in dt.Rows)
                {
                    newRow = rootDt.Select(string.Concat("EQUIPMENTID='", Format.GetString(dr["EQUIPMENTID"], string.Empty), "'")).FirstOrDefault();

                    if (newRow == null)
                    {
                        newRow = rootDt.NewRow();
                        newRow["DRYFILIMNO"] = dr["DRYFILIMNO"];
                        newRow["MATERIALID"] = dr["MATERIALID"];
                        newRow["CONSUMABLEDEFNAME"] = dr["CONSUMABLEDEFNAME"];
                        newRow["WORKPLANE"] = dr["WORKPLANE"];
                        newRow["EQUIPMENTID"] = dr["EQUIPMENTID"];
                        newRow["EQUIPMENTNAME"] = dr["EQUIPMENTNAME"];
                        newRow["CIRCUITDIVISION"] = dr["CIRCUITDIVISION"];
                        newRow["STANDARDDIVISION"] = dr["STANDARDDIVISION"];
                        newRow["PNLAREA"] = dr["PNLAREA"];

                        newRow["MATERIALTYPE"] = _dic["SUBMATERIALTYPE"];
                        newRow["ROOT_ASSEMBLYITEMID"] = _dic["ROOT_ASSEMBLYITEMID"];
                        newRow["ROOT_ASSEMBLYITEMVERSION"] = _dic["ROOT_ASSEMBLYITEMVERSION"];
                        newRow["PRODUCTDEFID"] = _dic["ITEMID"];
                        newRow["PRODUCTDEFVERSION"] = _dic["ITEMVERSION"];
                        newRow["PNLX"] = _dic["PNLX"];
                        newRow["PNLY"] = _dic["PNLY"];
                        newRow["PROCESSSEGMENTCLASSID"] = _dic["PROCESSSEGMENTCLASSID"];
                        newRow["PROCESSSEGMENTID"] = _dic["PROCESSSEGMENTID"];
                        newRow["ENTERPRISEID"] = _dic["ENTERPRISEID"];
                        newRow["PLANTID"] = _dic["PLANTID"];

                        QtyAnalysis(newRow);

                        rootDt.Rows.Add(newRow);
                    }
                    else
                    {
                        newRow["MATERIALID"] = dr["MATERIALID"];
                        newRow["CONSUMABLEDEFNAME"] = dr["CONSUMABLEDEFNAME"];
                        newRow["DRYFILIMNO"] = dr["DRYFILIMNO"];
                        newRow["WORKPLANE"] = dr["WORKPLANE"];
                        newRow["CIRCUITDIVISION"] = dr["CIRCUITDIVISION"];
                        newRow["STANDARDDIVISION"] = dr["STANDARDDIVISION"];

                        QtyAnalysis(newRow);
                    }
                }
            };

            popup.ShowDialog();
        }

        /// <summary>
        /// 변경 내용 체크
        /// </summary>
        /// <returns></returns>
        public override void Validation() => grdMain.View.CheckValidation();

        #endregion Public Function
    }
}