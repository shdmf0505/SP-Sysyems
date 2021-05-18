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
    /// 프 로 그 램 명  : 기준 정보 > 품질관리 > 평탄도 Spec 관리
    /// 업  무  설  명  : 평탄도 설비 인퍼페이스 Data의 기준이 될 Spec 관리화면
    /// 생    성    자  : 전우성
    /// 생    성    일  : 2020-02-26
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class SpecManagementByFlatness : SmartConditionManualBaseForm
    {
        #region Local Variables

        /// <summary>
        /// Filter에 사용된 품목코드
        /// </summary>
        private string _productDefID = string.Empty;

        #endregion

        #region 생성자

        public SpecManagementByFlatness()
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

            grdProduct.View.AddTextBoxColumn("PRODUCTDEFID", 110).SetTextAlignment(TextAlignment.Left);
            grdProduct.View.AddTextBoxColumn("PRODUCTDEFNAME", 150).SetTextAlignment(TextAlignment.Left);

            grdProduct.View.PopulateColumns();
            grdProduct.View.BestFitColumns();
            grdProduct.View.SetIsReadOnly();

            grdProduct.GridButtonItem = GridButtonItem.Export;

            #endregion

            #region 소분류

            grdSpec.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            grdSpec.View.AddTextBoxColumn("SPECCLASSID").SetIsHidden();
            grdSpec.View.AddTextBoxColumn("ENTERPRISEID").SetIsHidden();
            grdSpec.View.AddTextBoxColumn("PLANTID").SetIsHidden();
            grdSpec.View.AddTextBoxColumn("CONTROLTYPE").SetIsHidden();
            grdSpec.View.AddTextBoxColumn("PRODUCTDEFID").SetIsHidden();
            grdSpec.View.AddTextBoxColumn("INSPITEMID").SetIsHidden();

            grdSpec.View.AddSpinEditColumn("SPECVERSION", 60).SetLabel("VERSION").SetIsReadOnly().SetTextAlignment(TextAlignment.Left);
            grdSpec.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=UsageStatus", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                        .SetLabel("USAGESTATUS");

            grdSpec.View.AddTextBoxColumn("DESCRIPTION", 150).SetTextAlignment(TextAlignment.Left);
            grdSpec.View.AddSpinEditColumn("USL", 70).SetLabel("SPCUSL").SetDisplayFormat("{0:f4}", MaskTypes.Custom);
            grdSpec.View.AddSpinEditColumn("SL", 70).SetLabel("SPCSL").SetDisplayFormat("{0:f4}", MaskTypes.Custom);
            grdSpec.View.AddSpinEditColumn("LSL", 70).SetLabel("SPCLSL").SetDisplayFormat("{0:f4}", MaskTypes.Custom);
            grdSpec.View.AddSpinEditColumn("UCL", 70).SetDisplayFormat("{0:f4}", MaskTypes.Custom);
            grdSpec.View.AddSpinEditColumn("CL", 70).SetDisplayFormat("{0:f4}", MaskTypes.Custom);
            grdSpec.View.AddSpinEditColumn("LCL", 70).SetDisplayFormat("{0:f4}", MaskTypes.Custom);

            grdSpec.View.PopulateColumns();
            grdSpec.View.BestFitColumns();

            grdSpec.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete;
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
                _productDefID = Format.GetString(grdProduct.View.GetRowCellValue(e.FocusedRowHandle, "PRODUCTDEFID"), string.Empty);
                grdSpec.View.ActiveFilterString = string.Concat("[PRODUCTDEFID] = '", _productDefID, "'");
            };

            //! 소분류 Cell 값 변경 이벤트
            grdSpec.View.CellValueChanged += (s, e) =>
            {
                if (grdProduct.View.FocusedRowHandle < 0 || !e.Column.FieldName.Equals("VALIDSTATE") || e.Value.Equals("Invalid"))
                {
                    return;
                }

                for (int i = 0; i < grdSpec.View.RowCount; i++)
                {
                    if (i.Equals(e.RowHandle))
                    {
                        continue;
                    }

                    if (grdSpec.View.GetRowCellValue(i, "VALIDSTATE").Equals(e.Value))
                    {
                        grdSpec.View.SetRowCellValue(i, "VALIDSTATE", "Invalid");
                    }
                }
            };

            //! New Row 발생 전 이벤트
            grdSpec.ToolbarAddingRow += (s, e) => e.Cancel = grdProduct.View.FocusedRowHandle < 0 ? true : false;

            //! New Row 발생 이벤트
            grdSpec.View.InitNewRow += (s, e) =>
            {
                int version = grdSpec.View.DataRowCount.Equals(0) ? 1 :
                                (grdSpec.DataSource as DataTable).AsEnumerable()
                                                                 .Where(x => x.Field<string>("PRODUCTDEFID") == _productDefID)
                                                                 .Max(x => x.Field<int>("SPECVERSION")) + 1;

                grdSpec.View.SetRowCellValue(e.RowHandle, "SPECCLASSID", "SAVE_MEASURE_FLATNESS");
                grdSpec.View.SetRowCellValue(e.RowHandle, "ENTERPRISEID", UserInfo.Current.Enterprise);
                grdSpec.View.SetRowCellValue(e.RowHandle, "PLANTID", UserInfo.Current.Plant);
                grdSpec.View.SetRowCellValue(e.RowHandle, "PRODUCTDEFID", _productDefID);
                grdSpec.View.SetRowCellValue(e.RowHandle, "INSPITEMID", "Z");
                grdSpec.View.SetRowCellValue(e.RowHandle, "SPECVERSION", version);
                grdSpec.View.SetRowCellValue(e.RowHandle, "CONTROLTYPE", "XBARR");
                grdSpec.View.SetRowCellValue(e.RowHandle, "VALIDSTATE", "Invalid");
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

            #region 품목코드

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

            Conditions.AddTextBox("P_SPECCLASSID").SetIsHidden().SetDefault("SAVE_MEASURE_FLATNESS");

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