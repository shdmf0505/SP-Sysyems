#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons.Controls;

using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;

#endregion

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프 로 그 램 명  : 기준 정보 > 품질관리 > 3D Spec 관리
    /// 업  무  설  명  : 3D 설비 인퍼페이스 Data의 기준이 될 Spec 관리화면
    /// 생    성    자  : 전우성
    /// 생    성    일  : 2020-02-24
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class SpecManagementBy3D : SmartConditionManualBaseForm
    {
        #region Local Variables

        /// <summary>
        /// 검사항목 List (Code List : Inspection3DItem)
        /// </summary>
        private DataTable _inspectionItemList = null;

        /// <summary>
        /// Filter에 사용된 품목코드
        /// </summary>
        private string _productDefID = string.Empty;

        /// <summary>
        /// Filter에 사용될 Version 
        /// </summary>
        private int _version = 0;

        #endregion

        #region 생성자

        public SpecManagementBy3D()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeEvent();
            InitializeLanguageKey();
            InitializeGrid();

            // 3차원 검사항목 List 가져오기
            _inspectionItemList = SqlExecuter.Query("GetCodeList", "00001",
                                                    new Dictionary<string, object>
                                                    {
                                                        { "CODECLASSID", "Inspection3DItem" },
                                                        { "LANGUAGETYPE", UserInfo.Current.LanguageType }
                                                    });
        }

        /// <summary>
        /// Language Key 초기화
        /// </summary>
        private void InitializeLanguageKey()
        {
            grdProduct.LanguageKey = "LARGECLASS";
            grdVersion.LanguageKey = "MIDDLECLASS";
            grdSpec.LanguageKey = "SMALLCLASS";
        }

        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            #region 대분류

            grdProduct.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            grdProduct.View.AddTextBoxColumn("PRODUCTDEFID", 110).SetTextAlignment(TextAlignment.Left);
            grdProduct.View.AddTextBoxColumn("PRODUCTDEFNAME", 150).SetTextAlignment(TextAlignment.Left);

            grdProduct.View.PopulateColumns();
            grdProduct.View.BestFitColumns();
            grdProduct.View.SetIsReadOnly();

            grdProduct.GridButtonItem = GridButtonItem.Export;

            #endregion

            #region 중분류

            grdVersion.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            grdVersion.View.AddTextBoxColumn("PRODUCTDEFID").SetIsHidden();
            grdVersion.View.AddSpinEditColumn("SPECVERSION", 60).SetLabel("VERSION").SetIsReadOnly().SetTextAlignment(TextAlignment.Left);
            grdVersion.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=UsageStatus", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                           .SetLabel("USAGESTATUS")
                           .SetTextAlignment(TextAlignment.Left);
            grdVersion.View.AddTextBoxColumn("DESCRIPTION", 150).SetTextAlignment(TextAlignment.Left);

            grdVersion.View.PopulateColumns();
            grdVersion.View.BestFitColumns();

            grdVersion.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete;
            grdVersion.View.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;

            #endregion

            #region 소분류

            grdSpec.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            grdSpec.View.AddTextBoxColumn("SPECCLASSID").SetIsHidden();
            grdSpec.View.AddTextBoxColumn("ENTERPRISEID").SetIsHidden();
            grdSpec.View.AddTextBoxColumn("PLANTID").SetIsHidden();
            grdSpec.View.AddTextBoxColumn("CONTROLTYPE").SetIsHidden();
            grdSpec.View.AddTextBoxColumn("PRODUCTDEFID").SetIsHidden();
            grdSpec.View.AddTextBoxColumn("SPECVERSION").SetIsHidden();
            grdSpec.View.AddTextBoxColumn("VALIDSTATE").SetIsHidden();
            grdSpec.View.AddTextBoxColumn("DESCRIPTION").SetIsHidden();

            grdSpec.View.AddComboBoxColumn("INSPITEMID", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=Inspection3DItem", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                   .SetLabel("CODETYPE")
                   .SetIsReadOnly()
                   .SetTextAlignment(TextAlignment.Left);

            grdSpec.View.AddSpinEditColumn("USL", 70).SetLabel("SPCUSL").SetDisplayFormat("{0:f4}", MaskTypes.Custom);
            grdSpec.View.AddSpinEditColumn("SL", 70).SetLabel("SPCSL").SetDisplayFormat("{0:f4}", MaskTypes.Custom);
            grdSpec.View.AddSpinEditColumn("LSL", 70).SetLabel("SPCLSL").SetDisplayFormat("{0:f4}", MaskTypes.Custom);
            grdSpec.View.AddSpinEditColumn("UCL", 70).SetDisplayFormat("{0:f4}", MaskTypes.Custom);
            grdSpec.View.AddSpinEditColumn("CL", 70).SetDisplayFormat("{0:f4}", MaskTypes.Custom);
            grdSpec.View.AddSpinEditColumn("LCL", 70).SetDisplayFormat("{0:f4}", MaskTypes.Custom);

            grdSpec.View.PopulateColumns();
            grdSpec.View.BestFitColumns();

            grdSpec.View.Columns["INSPITEMID"].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
            grdSpec.GridButtonItem = GridButtonItem.Export;
            grdSpec.View.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;

            #endregion
        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            //! 대분류 Row 변경 이벤트
            grdProduct.View.FocusedRowChanged += (s, e) => SetFocusedData();

            //! 중분류 Row 변경 이벤트
            grdVersion.View.FocusedRowChanged += (s, e) => SetFocusedData();

            //! 중분류 Cell 값 변경 이벤트
            grdVersion.View.CellValueChanged += (s, e) =>
            {
                if (grdProduct.View.FocusedRowHandle < 0 || !e.Column.FieldName.Equals("VALIDSTATE") || e.Value.Equals("Invalid"))
                {
                    return;
                }

                for (int i = 0; i < grdVersion.View.RowCount; i++)
                {
                    if (i.Equals(e.RowHandle))
                    {
                        continue;
                    }

                    if (grdVersion.View.GetRowCellValue(i, "VALIDSTATE").Equals(e.Value))
                    {
                        grdVersion.View.SetRowCellValue(i, "VALIDSTATE", "Invalid");
                    }
                }
            };

            //! New Row 발생 전 이벤트
            grdVersion.ToolbarAddingRow += (s, e) => e.Cancel = grdProduct.View.FocusedRowHandle < 0;

            //! New Row 발생 이벤트
            grdVersion.View.InitNewRow += (s, e) =>
            {
                _version = grdVersion.View.DataRowCount.Equals(0) ? 1 :
                            (grdVersion.DataSource as DataTable).AsEnumerable()
                                                                .Where(x => x.Field<string>("PRODUCTDEFID") == _productDefID)
                                                                .Max(x => x.Field<int>("SPECVERSION")) + 1;

                grdVersion.View.SetRowCellValue(e.RowHandle, "SPECVERSION", _version);
                grdVersion.View.SetRowCellValue(e.RowHandle, "PRODUCTDEFID", _productDefID);
                grdVersion.View.SetRowCellValue(e.RowHandle, "VALIDSTATE", "Invalid");

                DataTable dt = grdSpec.DataSource as DataTable;
                DataRow newRow = dt.NewRow();

                foreach (DataRow item in _inspectionItemList.Rows)
                {
                    newRow["SPECCLASSID"] = "SAVE_MEASURE_3D";
                    newRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                    newRow["PLANTID"] = UserInfo.Current.Plant;
                    newRow["PRODUCTDEFID"] = _productDefID;
                    newRow["INSPITEMID"] = item["CODEID"];
                    newRow["SPECVERSION"] = _version;
                    newRow["CONTROLTYPE"] = "XBARR";
                    newRow["VALIDSTATE"] = "Invalid";
                    dt.Rows.Add(newRow);
                    newRow = dt.NewRow();
                }
            };

            //! 중분류 삭제버튼 클릭 이벤트
            grdVersion.ToolbarDeletingRow += (s, e) =>
            {
                if ((s as SmartBandedGrid).View.GetFocusedDataRow() is DataRow focusedDr)
                {
                    // 연속으로 삭제시에 focused 없어지는 현상 처리
                    (s as SmartBandedGrid).View.SelectRow((s as SmartBandedGrid).View.FocusedRowHandle);

                    if (focusedDr.RowState.Equals(DataRowState.Added))
                    {
                        DataTable dt = (grdSpec.DataSource as DataTable);
                        foreach (DataRow dr in dt.Select())
                        {
                            if (Format.GetString(dr["PRODUCTDEFID"]).Equals(focusedDr["PRODUCTDEFID"])
                               && Format.GetInteger(dr["SPECVERSION"]).Equals(focusedDr["SPECVERSION"]))
                            {
                                dt.Rows.Remove(dr);
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < grdSpec.View.DataRowCount; i++)
                        {
                            grdSpec.View.DeleteRow(i);
                        }
                    }
                }
            };

            //! 조회조건 품목코드 x버튼 클릭시 이벤트
            if (Conditions.GetControl<SmartSelectPopupEdit>("PRODUCTDEFID") is var control)
            {
                control.Properties.ButtonClick += (s, e) =>
                {
                    if ((s as DevExpress.XtraEditors.ButtonEdit).Properties.Buttons.IndexOf(e.Button).Equals(1))
                    {
                        Conditions.GetControl<SmartTextBox>("P_PRODUCTDEFNAME").Text = string.Empty;
                    }
                };
            }
        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            // 저장시 소분류 항목을 Rule 처리한다. 중분류의 설명과 사용여부 처리
            foreach (DataRow dr in grdVersion.GetChangedRows().Rows)
            {
                if (Format.GetString(dr["_STATE_"]).Equals("deleted"))
                {
                    continue;
                }

                (grdSpec.DataSource as DataTable).AsEnumerable()
                                                 .Where(x => x.Field<string>("PRODUCTDEFID") == Format.GetString(dr["PRODUCTDEFID"])
                                                          && x.Field<int>("SPECVERSION") == Format.GetInteger(dr["SPECVERSION"]))
                                                 .ForEach(item =>
                                                 {
                                                     item["VALIDSTATE"] = dr["VALIDSTATE"];
                                                     item["DESCRIPTION"] = dr["DESCRIPTION"];
                                                 });
            }

            ExecuteRule("SpecManagementByInterface", grdSpec.GetChangedRows());
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

                await base.OnSearchAsync();
                grdProduct.View.ClearDatas();
                grdVersion.View.ClearDatas();
                grdSpec.View.ClearDatas();

                grdVersion.View.ActiveFilterString = string.Concat("[PRODUCTDEFID] = '", _productDefID, "'");
                grdSpec.View.ActiveFilterString = string.Concat("[PRODUCTDEFID] = '", _productDefID, "' AND [SPECVERSION] = '", _version, "'");
                
                // 대분류 조회
                if (await SqlExecuter.QueryAsync("GetProductDefList", "10007", Conditions.GetValues()) is DataTable dt)
                {
                    if (dt.Rows.Count.Equals(0))
                    {
                        grdProduct.View.ClearDatas();
                        ShowMessage("NoSelectData");
                        return;
                    }

                    grdProduct.DataSource = dt;
                }

                // 중분류, 소분류 조회 - 소분류에서 중분류 Data 수집
                if (await SqlExecuter.QueryAsync("GetInterfaceSpecList", "10001", Conditions.GetValues()) is DataTable specListDt)
                {
                    grdVersion.DataSource = SetVersionView(specListDt);
                    grdSpec.DataSource = specListDt;
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

            #region 품목코드 추가

            var productDefID = Conditions.AddSelectPopup("PRODUCTDEFID", new SqlQuery("GetProductDefList", "10007"))
                                         .SetPopupLayout("GRIDPRODUCTLIST", PopupButtonStyles.Ok_Cancel, true, false)
                                         .SetPopupLayoutForm(600, 400, FormBorderStyle.FixedToolWindow)
                                         .SetPosition(0)
                                         .SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
                                         {
                                             selectedRow.ForEach(row =>
                                             {
                                                 Conditions.GetControl<SmartTextBox>("P_PRODUCTDEFNAME").Text = row.GetString("PRODUCTDEFNAME");
                                             });
                                         });

            productDefID.Conditions.AddTextBox("PRODUCTDEFID");

            productDefID.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 120);
            productDefID.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 150);

            #endregion

            #region 품목명 - Display 전용으로 품목입력시 표시됨

            Conditions.GetCondition<ConditionItemTextBox>("P_PRODUCTDEFNAME").SetIsReadOnly();

            #endregion

            #region Spec Class

            Conditions.AddTextBox("P_SPECCLASSID").SetIsHidden().SetDefault("SAVE_MEASURE_3D");

            #endregion
        }

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            grdSpec.View.CheckValidation();
            grdVersion.View.CheckValidation();

            if (grdSpec.GetChangedRows().Rows.Count.Equals(0) && grdVersion.GetChangedRows().Rows.Count.Equals(0))
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }

        #endregion

        #region Private Function

        /// <summary>
        /// 조회시에 중분류에 입력될 항목 Groupping
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private DataTable SetVersionView(DataTable dt)
        {
            DataTable versionDt = new DataTable();
            versionDt.Columns.Add("PRODUCTDEFID", typeof(string));
            versionDt.Columns.Add("SPECVERSION", typeof(int));
            versionDt.Columns.Add("VALIDSTATE", typeof(string));
            versionDt.Columns.Add("DESCRIPTION", typeof(string));

            dt.AsEnumerable().GroupBy(x => new Tuple<string, int, string, string>(x.Field<string>("PRODUCTDEFID"), x.Field<int>("SPECVERSION"), x.Field<string>("VALIDSTATE"), x.Field<string>("DESCRIPTION")))
                             .OrderBy(x => x.Key.Item2)
                             .Select(x => new { PRODUCTDEFID = x.Key.Item1, SPECVERSION = x.Key.Item2, VALIDSTATE = x.Key.Item3, DESCRIPTION = x.Key.Item4 })
                             .ForEach(x =>
                             {
                                 versionDt.Rows.Add(new object[] { x.PRODUCTDEFID, x.SPECVERSION, x.VALIDSTATE, x.DESCRIPTION });
                             });

            return versionDt;
        }

        /// <summary>
        /// Focused Change시 Filtering
        /// </summary>
        private void SetFocusedData()
        {
            _productDefID = Format.GetString(grdProduct.View.GetRowCellValue(grdProduct.View.FocusedRowHandle, "PRODUCTDEFID"), string.Empty);
            grdVersion.View.ActiveFilterString = string.Concat("[PRODUCTDEFID] = '", _productDefID, "'");

            _version = Format.GetInteger(grdVersion.View.GetRowCellValue(grdVersion.View.FocusedRowHandle, "SPECVERSION"), 0);            
            grdSpec.View.ActiveFilterString = string.Concat("[PRODUCTDEFID] = '", _productDefID, "' AND [SPECVERSION] = '", _version, "'");
        }

        #endregion
    }
}