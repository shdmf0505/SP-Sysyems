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
    /// 프 로 그 램 명  : 기준 정보 > 품질관리 > XRF Spec 관리
    /// 업  무  설  명  : XRF 설비 인퍼페이스 Data의 기준이 될 Spec 관리화면
    /// 생    성    자  : 전우성
    /// 생    성    일  : 2020-02-27
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class SpecManagementByXrf : SmartConditionManualBaseForm
    {
        #region Local Variables

        /// <summary>
        /// 검사항목 List (Code List : InspectionXrfItem)
        /// </summary>
        private DataTable _inspectionItemList = null;

        /// <summary>
        /// Filter에 사용된 품목코드
        /// </summary>
        private string _productDefID = string.Empty;

        #endregion

        #region 생성자

        public SpecManagementByXrf()
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

            // xrf 검사항목 List 가져오기
            _inspectionItemList = SqlExecuter.Query("GetCodeList", "00001",
                                                    new Dictionary<string, object>
                                                    {
                                                        { "CODECLASSID", "InspectionXrfItem" },
                                                        { "LANGUAGETYPE", UserInfo.Current.LanguageType }
                                                    });
        }

        /// <summary>
        /// Language Key 초기화
        /// </summary>
        private void InitializeLanguageKey()
        {
            grdProduct.LanguageKey = "LARGECLASS";
            grdSpec.LanguageKey = "SMALLCLASS";
        }

        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            #region 대분류

            grdProduct.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            grdProduct.View.AddTextBoxColumn("PRODUCTDEFID", 150).SetTextAlignment(TextAlignment.Left);
            grdProduct.View.AddTextBoxColumn("PRODUCTDEFNAME", 150).SetTextAlignment(TextAlignment.Left);

            grdProduct.View.PopulateColumns();
            grdProduct.View.BestFitColumns();
            grdProduct.View.SetIsReadOnly();

            grdProduct.GridButtonItem = GridButtonItem.None;

            #endregion

            #region 소분류

            grdSpec.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            grdSpec.View.AddTextBoxColumn("SPECCLASSID").SetIsHidden();
            grdSpec.View.AddTextBoxColumn("ENTERPRISEID").SetIsHidden();
            grdSpec.View.AddTextBoxColumn("PLANTID").SetIsHidden();
            grdSpec.View.AddTextBoxColumn("DEFAULTCHARTTYPE").SetIsHidden();
            grdSpec.View.AddTextBoxColumn("PRODUCTDEFID").SetIsHidden();
            grdSpec.View.AddTextBoxColumn("VALIDSTATE").SetIsHidden();
            grdSpec.View.AddTextBoxColumn("DESCRIPTION").SetIsHidden();
            grdSpec.View.AddSpinEditColumn("SPECVERSION").SetIsHidden();

            grdSpec.View.AddComboBoxColumn("INSPITEMID", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=InspectionXrfItem", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                   .SetLabel("INSPECTIONITEM")
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
            grdProduct.View.FocusedRowChanged += (s, e) =>
            {
                if (grdProduct.View.FocusedRowHandle < 0)
                {
                    return;
                }

                _productDefID = Format.GetString(grdProduct.View.GetRowCellValue(grdProduct.View.FocusedRowHandle, "PRODUCTDEFID"), string.Empty);
                grdSpec.View.ActiveFilterString = string.Concat("[PRODUCTDEFID] = '", _productDefID, "'");

                // 대분류 *은 삭제되면 안됨. 초기 설정으로 DB에 Default 값을 넣어줘야 한다.
                grdProduct.GridButtonItem = grdProduct.View.GetFocusedRowCellValue("PRODUCTDEFID").Equals("*")
                                                ? GridButtonItem.Add
                                                : GridButtonItem.Delete;
            };

            //! New Row 발생 전 이벤트
            grdProduct.ToolbarAddingRow += (s, e) =>
            {
                // popup을 통해서만 Newrow를 한다.
                e.Cancel = true;

                var popup = CreateSelectPopup("PRODUCTDEFID", new SqlQuery("GetProductDefList", "10008", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"))
                                    .SetPopupLayout("PRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)  //팝업창 UI 설정.
                                    .SetPopupResultCount(1)
                                    .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow);

                popup.Conditions.AddTextBox("PRODUCTDEFID");

                popup.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150).SetLabel("PRODUCTDEFID");
                popup.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 150).SetLabel("PRODUCTDEFNAME");

                ShowPopup(popup, (grdProduct.DataSource as DataTable).Rows.Cast<DataRow>()).ForEach(dr =>
                {
                    DataTable dt = grdProduct.DataSource as DataTable;
                    string productDefId = Format.GetString(dr["PRODUCTDEFID"]);

                    if (dt.Select(string.Concat("PRODUCTDEFID = '", productDefId, "'")).Count() > 0)
                    {
                        ShowMessage("InValidData002", new string[] { productDefId });
                        return;
                    }

                    DataRow newRow = dt.NewRow();

                    newRow["PRODUCTDEFID"] = productDefId;
                    newRow["PRODUCTDEFNAME"] = dr["PRODUCTDEFNAME"];
                    dt.Rows.Add(newRow);

                    DataTable specDt = grdSpec.DataSource as DataTable;
                    DataRow specNewRow = specDt.NewRow();

                    foreach (DataRow item in _inspectionItemList.Rows)
                    {
                        specNewRow["SPECCLASSID"] = "SAVE_MEASURE_XRF";
                        specNewRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                        specNewRow["PLANTID"] = UserInfo.Current.Plant;
                        specNewRow["PRODUCTDEFID"] = productDefId;
                        specNewRow["INSPITEMID"] = item["CODEID"];
                        specNewRow["SPECVERSION"] = 1;
                        specNewRow["CONTROLTYPE"] = "XBARR";
                        specNewRow["VALIDSTATE"] = "Valid";
                        specNewRow["DESCRIPTION"] = string.Empty;

                        specDt.Rows.Add(specNewRow);
                        specNewRow = specDt.NewRow();
                    }
                });
            };

            //! 중분류 삭제버튼 클릭 이벤트
            grdProduct.ToolbarDeletingRow += (s, e) =>
            {
                if ((s as SmartBandedGrid).View.GetFocusedDataRow() is DataRow item)
                {
                    // 연속으로 삭제시에 focused 없어지는 현상 처리
                    (s as SmartBandedGrid).View.SelectRow((s as SmartBandedGrid).View.FocusedRowHandle);

                    if (item.RowState.Equals(DataRowState.Added))
                    {
                        DataTable dt = (grdSpec.DataSource as DataTable);
                        foreach (DataRow dr in dt.Select())
                        {
                            if (Format.GetString(dr["PRODUCTDEFID"]).Equals(item["PRODUCTDEFID"]))
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

            // 조회조건 품목코드 x버튼 클릭시 이벤트
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
                grdSpec.View.ClearDatas();

                grdSpec.View.ActiveFilterString = string.Concat("[PRODUCTDEFID] = '", _productDefID, "'");

                // 대분류 조회
                if (await SqlExecuter.QueryAsync("GetInterfaceSpecProductList", "10001", Conditions.GetValues()) is DataTable dt)
                {
                    if (dt.Rows.Count.Equals(0))
                    {
                        grdProduct.View.ClearDatas();
                        ShowMessage("NoSelectData");
                        return;
                    }

                    grdProduct.DataSource = dt;
                }

                // 소분류 조회
                if (await SqlExecuter.QueryAsync("GetInterfaceSpecList", "10001", Conditions.GetValues()) is DataTable specListDt)
                {
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

            #region 품목명

            Conditions.GetCondition<ConditionItemTextBox>("P_PRODUCTDEFNAME").SetIsReadOnly();

            #endregion

            #region Spec Class

            Conditions.AddTextBox("P_SPECCLASSID").SetIsHidden().SetDefault("SAVE_MEASURE_XRF");

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

            if (grdSpec.GetChangedRows().Rows.Count.Equals(0))
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }

        #endregion
    }
}