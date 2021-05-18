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
    /// 프 로 그 램 명  : 기준정보 > 품목관리 > 부자재 routing BOM INK 부자재
    /// 업  무  설  명  : 공정이 INK인 경우에 발생되는 user control
    /// 생    성    자  : 전우성
    /// 생    성    일  : 2021-02-17
    /// 필  수  처  리  :
    ///
    /// </summary>
    public partial class ucSubMaterialByInk : ISubMaterialBomRouting
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
        public ucSubMaterialByInk(Dictionary<string, object> parentDic)
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
            grdMain.Caption = Language.Get("INK");
        }

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            grdMain.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export;
            grdMain.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            var group = grdMain.View.AddGroupColumn("");

            #region Grid 자재ID 설정

            var condition = group.AddSelectPopupColumn("MATERIALID", 140, new SqlQuery("GetMaterialID", "10002", new Dictionary<string, object>
                                                                                                {
                                                                                                    { "LANGUAGETYPE", UserInfo.Current.LanguageType },
                                                                                                    { "ENTERPRISEID", UserInfo.Current.Enterprise },
                                                                                                    { "PLANTID", UserInfo.Current.Plant }
                                                                                                }))
                                 .SetValidationKeyColumn()
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
                                     });
                                 });

            condition.Conditions.AddTextBox("P_MATERIALID").SetLabel("CONSUMABLEIDNAME");

            condition.GridColumns.AddTextBoxColumn("MATERIALID", 90).SetLabel("CONSUMABLEDEFID");
            condition.GridColumns.AddTextBoxColumn("CONSUMABLEDEFNAME", 170);
            condition.GridColumns.AddComboBoxColumn("STOCKUNIT", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ReceivePayOutUnit", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            condition.GridColumns.AddTextBoxColumn("MATERIALWIDTH", 10).SetIsHidden();

            #endregion Grid 자재ID 설정

            group.AddTextBoxColumn("CONSUMABLEDEFNAME", 120).SetIsReadOnly();

            group = grdMain.View.AddGroupColumn("UNITGCM");
            group.AddSpinEditColumn("INKIMPORTANCE", 80).SetLabel("INKSPECIFICGRAVITY").SetDisplayFormat("#0.####").SetTextAlignment(TextAlignment.Right).SetValueRange(0, 99).SetDefault(0);

            group = grdMain.View.AddGroupColumn("UNITPERCENTAGE");
            group.AddSpinEditColumn("NONVOLATILEMATTER", 120).SetDisplayFormat("#0.####").SetTextAlignment(TextAlignment.Right).SetValueRange(0, 99).SetDefault(0);

            group = grdMain.View.AddGroupColumn("UNITMM2");
            group.AddSpinEditColumn("COVERAGECS", 110).SetDisplayFormat("###,##0.#########").SetTextAlignment(TextAlignment.Right).SetValueRange(0, 999999).SetDefault(0);

            group = grdMain.View.AddGroupColumn("UNITMM2");
            group.AddSpinEditColumn("COVERAGESS", 110).SetDisplayFormat("###,##0.#########").SetTextAlignment(TextAlignment.Right).SetValueRange(0, 999999).SetDefault(0);

            group = grdMain.View.AddGroupColumn("UNITMM");
            group.AddSpinEditColumn("INKTHICKNESS", 100).SetDisplayFormat("#0.####").SetTextAlignment(TextAlignment.Right).SetDefault(0).SetValueRange(0, 999);

            group = grdMain.View.AddGroupColumn("UNITGPNL");
            group.AddTextBoxColumn("STANDARDQTY", 100).SetTextAlignment(TextAlignment.Right).SetDefault(0).SetIsReadOnly();

            group = grdMain.View.AddGroupColumn("UNITPERCENTAGE");
            group.AddSpinEditColumn("PRODUCTLOSS", 120).SetDisplayFormat("#0.####").SetTextAlignment(TextAlignment.Right).SetValueRange(-1, 1).SetDefault(0);

            group = grdMain.View.AddGroupColumn("UNITPERCENTAGE");
            group.AddSpinEditColumn("OUTPUTLOSS", 120).SetDisplayFormat("#0.####").SetTextAlignment(TextAlignment.Right).SetValueRange(-1, 1).SetDefault(0);

            group = grdMain.View.AddGroupColumn("UNITGPNL");
            group.AddTextBoxColumn("STANDARDQTYLOSS", 120).SetTextAlignment(TextAlignment.Right).SetDefault(0).SetIsReadOnly();

            group = grdMain.View.AddGroupColumn("");
            group.AddTextBoxColumn("PRODUCTLOSSREASON", 200);
            group.AddTextBoxColumn("OUTPUTLOSSREASON", 200);

            group.AddTextBoxColumn("CREATOR", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            group.AddTextBoxColumn("CREATEDTIME", 130).SetIsReadOnly().SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetTextAlignment(TextAlignment.Center);
            group.AddTextBoxColumn("MODIFIER", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            group.AddTextBoxColumn("MODIFIEDTIME", 130).SetIsReadOnly().SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetTextAlignment(TextAlignment.Center);

            grdMain.View.AddTextBoxColumn("MATERIALTYPE").SetIsHidden();
            grdMain.View.AddTextBoxColumn("ROOT_ASSEMBLYITEMID").SetIsHidden();
            grdMain.View.AddTextBoxColumn("ROOT_ASSEMBLYITEMVERSION").SetIsHidden();
            grdMain.View.AddTextBoxColumn("PRODUCTDEFID").SetIsHidden();
            grdMain.View.AddTextBoxColumn("PRODUCTDEFVERSION").SetIsHidden();
            grdMain.View.AddTextBoxColumn("PNLX").SetIsHidden();
            grdMain.View.AddTextBoxColumn("PNLY").SetIsHidden();
            grdMain.View.AddTextBoxColumn("PROCESSSEGMENTID").SetIsHidden();
            grdMain.View.AddTextBoxColumn("PROCESSSEGMENTCLASSID").SetIsHidden();
            grdMain.View.AddTextBoxColumn("ENTERPRISEID").SetIsHidden();
            grdMain.View.AddTextBoxColumn("PLANTID").SetIsHidden();

            grdMain.View.PopulateColumns();

            grdMain.ShowStatusBar = true;
        }

        #endregion 컨텐츠 영역 초기화

        #region Event

        /// <summary>
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            // H/P 내역은 1줄만 존재
            grdMain.ToolbarAddingRow += (s, e) => e.Cancel = grdMain.View.DataRowCount.Equals(1);

            // Grid에 신규Row 발생시 이벤트
            grdMain.View.InitNewRow += (s, e) =>
            {
                grdMain.View.SetRowCellValue(e.RowHandle, "MATERIALTYPE", "INK");
                grdMain.View.SetRowCellValue(e.RowHandle, "ROOT_ASSEMBLYITEMID", _dic["ROOT_ASSEMBLYITEMID"]);
                grdMain.View.SetRowCellValue(e.RowHandle, "ROOT_ASSEMBLYITEMVERSION", _dic["ROOT_ASSEMBLYITEMVERSION"]);
                grdMain.View.SetRowCellValue(e.RowHandle, "PRODUCTDEFID", _dic["ITEMID"]);
                grdMain.View.SetRowCellValue(e.RowHandle, "PRODUCTDEFVERSION", _dic["ITEMVERSION"]);
                grdMain.View.SetRowCellValue(e.RowHandle, "PNLX", _dic["PNLX"]);
                grdMain.View.SetRowCellValue(e.RowHandle, "PNLY", _dic["PNLY"]);
                grdMain.View.SetRowCellValue(e.RowHandle, "PROCESSSEGMENTID", _dic["PROCESSSEGMENTID"]);
                grdMain.View.SetRowCellValue(e.RowHandle, "PROCESSSEGMENTCLASSID", _dic["PROCESSSEGMENTCLASSID"]);
                grdMain.View.SetRowCellValue(e.RowHandle, "ENTERPRISEID", UserInfo.Current.Enterprise);
                grdMain.View.SetRowCellValue(e.RowHandle, "PLANTID", UserInfo.Current.Plant);
            };

            // Grid Value 변경시 이벤트
            grdMain.View.CellValueChanged += (s, e) =>
            {
                if (e.Column.FieldName.Equals("INKIMPORTANCE") || e.Column.FieldName.Equals("NONVOLATILEMATTER") || e.Column.FieldName.Equals("COVERAGECS") ||
                    e.Column.FieldName.Equals("COVERAGESS") || e.Column.FieldName.Equals("INKTHICKNESS") || e.Column.FieldName.Equals("PRODUCTLOSS") ||
                    e.Column.FieldName.Equals("OUTPUTLOSS"))
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

                // Grid에 자재ID 삭제버튼 클릭시 이벤트
                (control.ColumnEdit as RepositoryItemButtonEdit).ButtonClick += (s, e) =>
                {
                    if (e.Button.Kind.Equals(ButtonPredefines.Clear))
                    {
                        grdMain.View.SetRowCellValue(grdMain.View.GetFocusedDataSourceRowIndex(), "CONSUMABLEDEFNAME", string.Empty);
                    }
                };

                // Grid에 자재ID text 삭제시 이벤트
                control.ColumnEdit.EditValueChanged += (s, e) =>
                {
                    if (Format.GetFullTrimString((s as DevExpress.XtraEditors.ButtonEdit).EditValue).Equals(string.Empty))
                    {
                        grdMain.View.SetRowCellValue(grdMain.View.GetFocusedDataSourceRowIndex(), "CONSUMABLEDEFNAME", string.Empty);
                    }
                };
            });
        }

        #endregion Event

        #region Private Function

        /// 표준소요량 계산
        /// 표준 소요량 계산식 : INK 비중 / INK 불 휘발분 X INK 도포두께 Target 값 X (CS 도포면적 + SS 도포면적) / 1000
        /// 표준 소요량(LOSS포함) 계산식 : (INK 비중 / INK 불 휘발분 X INK 도포두께 Target 값 X(CS 도포면적 + SS 도포면적) / 1000) X(1+제품 LOSS + 생산량 LOSS)
        /// </summary>
        /// <param name="dr">DataRow</param>
        private void QtyAnalysis(DataRow dr)
        {
            double qty = Format.GetDouble(dr["INKIMPORTANCE"], 0) / Format.GetDouble(dr["NONVOLATILEMATTER"], 0) *
                         Format.GetDouble(dr["INKTHICKNESS"], 0) * (Format.GetDouble(dr["COVERAGECS"], 0) + Format.GetDouble(dr["COVERAGESS"], 0)) / 1000;

            qty = (qty.Equals(double.NaN) || double.IsInfinity(qty)) ? 0 : Math.Round(qty, 2);

            grdMain.View.SetFocusedRowCellValue("STANDARDQTY", qty);
            grdMain.View.SetFocusedRowCellValue("STANDARDQTYLOSS", Math.Round(qty * (1 + Format.GetDouble(dr["PRODUCTLOSS"], 0) + Format.GetDouble(dr["OUTPUTLOSS"], 0)), 2));
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

                if (await SqlExecuter.QueryAsync("SelectSubMaterialDivisionByInkBOMRouting", "10001", _dic) is DataTable dt)
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
            foreach (DataRow dr in grdMain.GetChangedRows().Rows)
            {
                if (Format.GetDecimal(dr["INKIMPORTANCE"], 0).Equals(0))
                {
                    throw MessageException.Create("InkValidationCheck", Format.GetString(dr["MATERIALID"], string.Empty), Language.Get("INKSPECIFICGRAVITY"));
                }
                else if (Format.GetDecimal(dr["NONVOLATILEMATTER"], 0).Equals(0))
                {
                    throw MessageException.Create("InkValidationCheck", Format.GetString(dr["MATERIALID"], string.Empty), Language.Get("NONVOLATILEMATTER"));
                }
                else if (Format.GetDecimal(dr["INKTHICKNESS"], 0).Equals(0))
                {
                    throw MessageException.Create("InkValidationCheck", Format.GetString(dr["MATERIALID"], string.Empty), Language.Get("INKTHICKNESS"));
                }
            }

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
            popupSubmaterialRoutingInkCopy popup = new popupSubmaterialRoutingInkCopy()
            {
                StartPosition = FormStartPosition.CenterParent
            };

            popup.SelectedRowTableEvent += (dr) =>
            {
                DataTable rootDt = grdMain.DataSource as DataTable;

                if (rootDt.Rows.Count.Equals(0))
                {
                    DataRow newRow = rootDt.NewRow();
                    newRow["MATERIALID"] = dr["MATERIALID"];
                    newRow["CONSUMABLEDEFNAME"] = dr["CONSUMABLEDEFNAME"];
                    newRow["INKIMPORTANCE"] = dr["INKIMPORTANCE"];
                    newRow["NONVOLATILEMATTER"] = dr["NONVOLATILEMATTER"];
                    newRow["COVERAGECS"] = dr["COVERAGECS"];
                    newRow["COVERAGESS"] = dr["COVERAGESS"];
                    newRow["INKTHICKNESS"] = dr["INKTHICKNESS"];
                    newRow["STANDARDQTY"] = dr["STANDARDQTY"];
                    newRow["PRODUCTLOSS"] = dr["PRODUCTLOSS"];
                    newRow["OUTPUTLOSS"] = dr["OUTPUTLOSS"];
                    newRow["STANDARDQTYLOSS"] = dr["STANDARDQTYLOSS"];

                    newRow["MATERIALTYPE"] = "INK";
                    newRow["ROOT_ASSEMBLYITEMID"] = _dic["ROOT_ASSEMBLYITEMID"];
                    newRow["ROOT_ASSEMBLYITEMVERSION"] = _dic["ROOT_ASSEMBLYITEMVERSION"];
                    newRow["PRODUCTDEFID"] = _dic["ITEMID"];
                    newRow["PRODUCTDEFVERSION"] = _dic["ITEMVERSION"];
                    newRow["PNLX"] = _dic["PNLX"];
                    newRow["PNLY"] = _dic["PNLY"];
                    newRow["PROCESSSEGMENTID"] = _dic["PROCESSSEGMENTID"];
                    newRow["PROCESSSEGMENTCLASSID"] = _dic["PROCESSSEGMENTCLASSID"];
                    newRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                    newRow["PLANTID"] = UserInfo.Current.Plant;

                    rootDt.Rows.Add(newRow.ItemArray);
                }
                else
                {
                    rootDt.Rows[0]["MATERIALID"] = dr["MATERIALID"];
                    rootDt.Rows[0]["CONSUMABLEDEFNAME"] = dr["CONSUMABLEDEFNAME"];
                    rootDt.Rows[0]["INKIMPORTANCE"] = dr["INKIMPORTANCE"];
                    rootDt.Rows[0]["NONVOLATILEMATTER"] = dr["NONVOLATILEMATTER"];
                    rootDt.Rows[0]["COVERAGECS"] = dr["COVERAGECS"];
                    rootDt.Rows[0]["COVERAGESS"] = dr["COVERAGESS"];
                    rootDt.Rows[0]["INKTHICKNESS"] = dr["INKTHICKNESS"];
                    rootDt.Rows[0]["STANDARDQTY"] = dr["STANDARDQTY"];
                    rootDt.Rows[0]["PRODUCTLOSS"] = dr["PRODUCTLOSS"];
                    rootDt.Rows[0]["OUTPUTLOSS"] = dr["OUTPUTLOSS"];
                    rootDt.Rows[0]["STANDARDQTYLOSS"] = dr["STANDARDQTYLOSS"];
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