#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion using

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프 로 그 램 명  : 기준정보 > 품목관리 > 부자재 routing BOM 약품 부자재
    /// 업  무  설  명  : 공정이 약품이면서 Button 동도금 인 경우에 발생되는 user control
    /// 생    성    자  : 전우성
    /// 생    성    일  : 2021-01-60
    /// 필  수  처  리  :
    ///
    /// </summary>
    public partial class ucSubMaterialByButtonCuPlating : ISubMaterialBomRouting
    {
        #region Global Variable

        /// <summary>
        /// 부모에게 받은 Dictionary정보
        /// </summary>
        private Dictionary<string, object> _dic;

        /// <summary>
        /// 부모에게 던져지는 Table
        /// 최초 조회된 Data가 없다면 Null 일 것
        /// </summary>
        private DataTable _resultTable;

        #endregion Global Variable

        #region 생성자

        public ucSubMaterialByButtonCuPlating(Dictionary<string, object> parentDic)
        {
            _dic = parentDic;

            InitializeComponent();
            InitializeLanguageKey();
            InitializeEvent();
            InitializeGrid();
        }

        #endregion 생성자

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// Language Key 초기화
        /// </summary>
        private void InitializeLanguageKey()
        {
            grdMain.Caption = Language.Get("CHEMICAL");
            layoutControlItem3.Text = Language.Get("AREACSMM2");
            layoutControlItem4.Text = Language.Get("AREASSMM2");
            btnAllApply.Text = Language.Get("ALLAPPLY");
        }

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            grdMain.GridButtonItem = GridButtonItem.Export;
            grdMain.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            grdMain.View.AddTextBoxColumn("EQUIPMENTCLASSID", 120).SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("EQUIPMENTCLASSNAME", 120).SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("EQUIPMENTID", 100).SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("EQUIPMENTNAME", 150).SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("MATERIALID", 100).SetLabel("CONSUMABLEDEFID").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("CONSUMABLEDEFNAME", 120).SetIsReadOnly();
            grdMain.View.AddComboBoxColumn("STOCKUNIT", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ReceivePayOutUnit", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsReadOnly();
            grdMain.View.AddSpinEditColumn("BOMQTYSTANDARD", 120).SetDisplayFormat("###,###,##0.#########").SetIsReadOnly().SetTextAlignment(TextAlignment.Right);
            grdMain.View.AddSpinEditColumn("PNLBYAMOUNTBOM", 120).SetDisplayFormat("###,###,##0.#########").SetIsReadOnly().SetTextAlignment(TextAlignment.Right);
            grdMain.View.AddSpinEditColumn("AREACS", 120).SetLabel("AREACSMM2").SetDisplayFormat("###,###,##0.#########").SetTextAlignment(TextAlignment.Right);
            grdMain.View.AddSpinEditColumn("AREASS", 120).SetLabel("AREASSMM2").SetDisplayFormat("###,###,##0.#########").SetTextAlignment(TextAlignment.Right);
            grdMain.View.AddSpinEditColumn("PNLAREA", 120).SetLabel("PNLAREAM2").SetDisplayFormat("###,###,##0.#########").SetIsReadOnly().SetTextAlignment(TextAlignment.Right);
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
            grdMain.View.AddTextBoxColumn("PROCESSSEGMENTID").SetIsHidden();
            grdMain.View.AddTextBoxColumn("PROCESSSEGMENTCLASSID").SetIsHidden();
            grdMain.View.AddTextBoxColumn("ENTERPRISEID").SetIsHidden();
            grdMain.View.AddTextBoxColumn("PLANTID").SetIsHidden();
            grdMain.View.AddTextBoxColumn("STATE").SetIsHidden();

            grdMain.View.PopulateColumns();

            grdMain.ShowStatusBar = true;

            grdMain.View.Columns["AREACS"].AppearanceCell.BackColor = Color.AntiqueWhite;
            grdMain.View.Columns["AREASS"].AppearanceCell.BackColor = Color.AntiqueWhite;
        }

        #endregion 컨텐츠 영역 초기화

        #region Event

        /// <summary>
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            // 양단면 값 변경시 이벤트
            grdMain.View.CellValueChanged += (s, e) =>
            {
                if (e.Column.FieldName.Equals("AREACS") || e.Column.FieldName.Equals("AREASS"))
                {
                    double cs = Format.GetDouble(grdMain.View.GetDataRow(e.RowHandle)["AREACS"], 0);
                    double ss = Format.GetDouble(grdMain.View.GetDataRow(e.RowHandle)["AREASS"], 0);

                    grdMain.View.GetDataRow(e.RowHandle)["PNLAREA"] = ((cs + ss) / 2) / 1000000;
                }
            };

            // 면적 계산 일괄 적용
            btnAllApply.Click += (s, e) =>
            {
                if (grdMain.View.DataRowCount.Equals(0))
                {
                    return;
                }

                double pnlArea = ((Format.GetDouble(spnCS.Text, 0) + Format.GetDouble(spnSS.Text, 0)) / 2) / 1000000;

                for (int i = 0; i < grdMain.View.DataRowCount; i++)
                {
                    grdMain.View.GetDataRow(i)["AREACS"] = Format.GetDouble(spnCS.Text, 0);
                    grdMain.View.GetDataRow(i)["AREASS"] = Format.GetDouble(spnSS.Text, 0);
                    grdMain.View.GetDataRow(i)["PNLAREA"] = pnlArea;
                }
            };
        }

        #endregion Event

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

                if (await SqlExecuter.QueryAsync("SelectSubMaterialDivisionByChemicalBOMRouting", "10002", _dic) is DataTable dt)
                {
                    if (dt.Rows.Count.Equals(0))
                    {
                        return;
                    }

                    grdMain.DataSource = dt;
                    _resultTable = SetDataTableChemical();
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
        /// 부모의 가져오기 버튼 클릭 이벤트 발생시 이벤트
        /// </summary>
        public override void SetDateAsync()
        {
            try
            {
                DialogManager.ShowWaitArea(this.Parent);

                if (DialogResult.No.Equals(MSGBox.Show(MessageBoxType.Warning, "ImportByInitData", MessageBoxButtons.YesNo)))
                {
                    return;
                }

                if (SqlExecuter.Query("SelectSubMaterialDivisionByChemicalBOM", "10002", _dic) is DataTable dt)
                {
                    if (_resultTable == null)
                    {
                        grdMain.DataSource = dt;
                    }
                    else
                    {
                        if (Format.GetString(grdMain.View.GetRowCellValue(0, "STATE"), string.Empty).Equals(string.Empty))
                        {
                            foreach (DataRow dr in (grdMain.DataSource as DataTable).Rows)
                            {
                                DataRow newRow = _resultTable.NewRow();
                                newRow["MATERIALTYPE"] = dr["MATERIALTYPE"];
                                newRow["PROCESSSEGMENTCLASSID"] = dr["PROCESSSEGMENTCLASSID"];
                                newRow["PROCESSSEGMENTID"] = dr["PROCESSSEGMENTID"];
                                newRow["PRODUCTDEFID"] = dr["PRODUCTDEFID"];
                                newRow["PRODUCTDEFVERSION"] = dr["PRODUCTDEFVERSION"];
                                newRow["EQUIPMENTID"] = dr["EQUIPMENTID"];
                                newRow["MATERIALID"] = dr["MATERIALID"];
                                newRow["ENTERPRISEID"] = dr["ENTERPRISEID"];
                                newRow["PLANTID"] = dr["PLANTID"];
                                newRow["ROOT_ASSEMBLYITEMID"] = dr["ROOT_ASSEMBLYITEMID"];
                                newRow["ROOT_ASSEMBLYITEMVERSION"] = dr["ROOT_ASSEMBLYITEMVERSION"];
                                newRow["WORKPLANE"] = "Double";
                                newRow["PNLAREA"] = dr["PNLAREA"];
                                newRow["AREACS"] = dr["AREACS"];
                                newRow["AREASS"] = dr["AREASS"];
                                newRow["_STATE_"] = "deleted";
                                _resultTable.Rows.Add(newRow.ItemArray);
                            }
                        }
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
                DialogManager.CloseWaitArea(this.Parent);
            }
        }

        /// <summary>
        /// 부모에게 Data 전달
        /// </summary>
        /// <returns></returns>
        public override DataSet GetData()
        {
            DataTable dt;

            // _resultTable이 null이면 기존에 등록된 Data가 없었음
            if (_resultTable == null)
            {
                dt = SetDataTableChemical();

                if (!grdMain.View.RowCount.Equals(0))
                {
                    DataRow newRow;

                    foreach (DataRow dr in (grdMain.DataSource as DataTable).Rows)
                    {
                        newRow = dt.NewRow();
                        newRow["MATERIALTYPE"] = dr["MATERIALTYPE"];
                        newRow["PROCESSSEGMENTCLASSID"] = dr["PROCESSSEGMENTCLASSID"];
                        newRow["PROCESSSEGMENTID"] = dr["PROCESSSEGMENTID"];
                        newRow["PRODUCTDEFID"] = dr["PRODUCTDEFID"];
                        newRow["PRODUCTDEFVERSION"] = dr["PRODUCTDEFVERSION"];
                        newRow["EQUIPMENTID"] = dr["EQUIPMENTID"];
                        newRow["MATERIALID"] = dr["MATERIALID"];
                        newRow["ENTERPRISEID"] = dr["ENTERPRISEID"];
                        newRow["PLANTID"] = dr["PLANTID"];
                        newRow["ROOT_ASSEMBLYITEMID"] = dr["ROOT_ASSEMBLYITEMID"];
                        newRow["ROOT_ASSEMBLYITEMVERSION"] = dr["ROOT_ASSEMBLYITEMVERSION"];
                        newRow["WORKPLANE"] = "Double";
                        newRow["PNLAREA"] = dr["PNLAREA"];
                        newRow["AREACS"] = dr["AREACS"];
                        newRow["AREASS"] = dr["AREASS"];
                        newRow["_STATE_"] = dr["STATE"];

                        dt.Rows.Add(newRow.ItemArray);
                    }
                }
            }
            else
            {
                // _resultTable이 Null이 아니지만 Cnt가 0이라면, 가져오기를 하지 않았음
                if (_resultTable.Rows.Count.Equals(0))
                {
                    dt = grdMain.GetChangedRows();
                    dt.TableName = "info";
                }
                else
                {
                    dt = SetDataTableChemical();
                    DataRow newRow;

                    _resultTable.PrimaryKey = new DataColumn[]
                    {
                        _resultTable.Columns["PROCESSSEGMENTID"],
                        _resultTable.Columns["PRODUCTDEFID"],
                        _resultTable.Columns["EQUIPMENTID"],
                        _resultTable.Columns["MATERIALID"]
                    };

                    foreach (DataRow dr in (grdMain.DataSource as DataTable).Rows) // 가져오기 data
                    {
                        // 기존 data
                        newRow = _resultTable.Rows.Find(new object[4] { dr["PROCESSSEGMENTID"], dr["PRODUCTDEFID"], dr["EQUIPMENTID"], dr["MATERIALID"] });

                        if (newRow == null)
                        {
                            newRow = dt.NewRow();
                            newRow["MATERIALTYPE"] = dr["MATERIALTYPE"];
                            newRow["PROCESSSEGMENTCLASSID"] = dr["PROCESSSEGMENTCLASSID"];
                            newRow["PROCESSSEGMENTID"] = dr["PROCESSSEGMENTID"];
                            newRow["PRODUCTDEFID"] = dr["PRODUCTDEFID"];
                            newRow["PRODUCTDEFVERSION"] = dr["PRODUCTDEFVERSION"];
                            newRow["EQUIPMENTID"] = dr["EQUIPMENTID"];
                            newRow["MATERIALID"] = dr["MATERIALID"];
                            newRow["ENTERPRISEID"] = dr["ENTERPRISEID"];
                            newRow["PLANTID"] = dr["PLANTID"];
                            newRow["ROOT_ASSEMBLYITEMID"] = dr["ROOT_ASSEMBLYITEMID"];
                            newRow["ROOT_ASSEMBLYITEMVERSION"] = dr["ROOT_ASSEMBLYITEMVERSION"];
                            newRow["WORKPLANE"] = "Double";
                            newRow["PNLAREA"] = dr["PNLAREA"];
                            newRow["AREACS"] = dr["AREACS"];
                            newRow["AREASS"] = dr["AREASS"];
                            newRow["_STATE_"] = dr["STATE"]; // added

                            dt.Rows.Add(newRow.ItemArray);
                        }
                        else
                        {
                            newRow["MATERIALTYPE"] = dr["MATERIALTYPE"];
                            newRow["PROCESSSEGMENTCLASSID"] = dr["PROCESSSEGMENTCLASSID"];
                            newRow["PROCESSSEGMENTID"] = dr["PROCESSSEGMENTID"];
                            newRow["PRODUCTDEFID"] = dr["PRODUCTDEFID"];
                            newRow["PRODUCTDEFVERSION"] = dr["PRODUCTDEFVERSION"];
                            newRow["EQUIPMENTID"] = dr["EQUIPMENTID"];
                            newRow["MATERIALID"] = dr["MATERIALID"];
                            newRow["ENTERPRISEID"] = dr["ENTERPRISEID"];
                            newRow["PLANTID"] = dr["PLANTID"];
                            newRow["ROOT_ASSEMBLYITEMID"] = dr["ROOT_ASSEMBLYITEMID"];
                            newRow["ROOT_ASSEMBLYITEMVERSION"] = dr["ROOT_ASSEMBLYITEMVERSION"];
                            newRow["WORKPLANE"] = "Double";
                            newRow["PNLAREA"] = dr["PNLAREA"];
                            newRow["AREACS"] = dr["AREACS"];
                            newRow["AREASS"] = dr["AREASS"];
                            newRow["_STATE_"] = "modified";

                            dt.Rows.Add(newRow.ItemArray);

                            _resultTable.Rows.Remove(newRow);
                        }
                    }

                    foreach (DataRow dr in _resultTable.Rows)
                    {
                        dt.Rows.Add(dr.ItemArray); // _STATE_ = "deleted";
                    }
                }
            }

            DataSet ds = new DataSet();
            ds.Tables.Add(dt);

            return ds;
        }

        /// <summary>
        /// 변경 내용 체크
        /// </summary>
        /// <returns></returns>
        public override void Validation() => grdMain.View.CheckValidation();

        #endregion Public Function
    }
}