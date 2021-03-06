#region using

using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraLayout.Utils;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion using

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프 로 그 램 명  : 기준정보 > 품목 관리 > 약품 BOM 소요량
    /// 업  무  설  명  : 약품 BOM 소요량 기준정보 등록 및 관리
    /// 생    성    자  : 전우성
    /// 생    성    일  : 2020-12-14
    /// 수  정  이  력  :
    ///
    ///
    /// </summary>
    public partial class ChemicalBomQty : SmartConditionManualBaseForm
    {
        #region Local Variables

        /// <summary>
        /// SelectPopup에 들어가는 parameter 전역 변수
        /// </summary>
        private readonly Dictionary<string, object> _param = new Dictionary<string, object>
        {
            { "LANGUAGETYPE", UserInfo.Current.LanguageType },
            { "ENTERPRISEID", UserInfo.Current.Enterprise },
            { "PLANTID", UserInfo.Current.Plant }
        };

        #endregion Local Variables

        #region 생성자

        public ChemicalBomQty()
        {
            InitializeComponent();
        }

        #endregion 생성자

        #region 컨텐츠 영역 초기화

        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeGrid();
            InitializeEvent();
            InitializeLanguageKey();
        }

        /// <summary>
        /// Language Key 초기화
        /// </summary>
        private void InitializeLanguageKey()
        {
            tabMain.SetLanguageKey(tabPageStandard, "STANDARD");
            tabMain.SetLanguageKey(tabPageHistory, "CHANGECONTENTS");
            grdMain.LanguageKey = "STANDARD";
            grdHistory.LanguageKey = "CHANGECONTENTS";
        }

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            #region 기준정보

            grdMain.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export;
            grdMain.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            grdMain.View.AddTextBoxColumn("MATERIALTYPE", 10).SetIsHidden();
            grdMain.View.AddTextBoxColumn("CHANGECOUNT", 10).SetIsHidden();
            grdMain.View.AddTextBoxColumn("ENTERPRISEID").SetIsHidden();
            grdMain.View.AddTextBoxColumn("PLANTID").SetIsHidden();

            #region Grid 설비ID 설정

            var condition = grdMain.View.AddSelectPopupColumn("EQUIPMENTID", 100, new SqlQuery("GetEquipmentAndArea", "10001", _param))
                                        .SetValidationKeyColumn()
                                        .SetValidationIsRequired()
                                        .SetPopupLayout("EQUIPMENTID", PopupButtonStyles.Ok_Cancel, true, true)
                                        .SetPopupLayoutForm(600, 400, FormBorderStyle.FixedToolWindow)
                                        .SetPopupResultCount(1)
                                        .SetPopupAutoFillColumns("EQUIPMENTNAME")
                                        .SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGridRow) =>
                                        {
                                            selectedRow.ForEach(row =>
                                            {
                                                dataGridRow["EQUIPMENTNAME"] = row["EQUIPMENTNAME"];
                                                dataGridRow["AREANAME"] = row["AREANAME"];
                                            });
                                        });

            condition.Conditions.AddTextBox("P_EQUIPMENT").SetLabel("EQUIPMENTIDNAME");

            condition.GridColumns.AddTextBoxColumn("EQUIPMENTID", 80);
            condition.GridColumns.AddTextBoxColumn("EQUIPMENTNAME", 120);
            condition.GridColumns.AddTextBoxColumn("AREANAME", 150);

            #endregion Grid 설비ID 설정

            grdMain.View.AddTextBoxColumn("EQUIPMENTNAME", 150).SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("AREANAME", 150).SetIsReadOnly();

            #region Grid 자재ID 설정

            condition = grdMain.View.AddSelectPopupColumn("MATERIALID", 140, new SqlQuery("GetMaterialID", "10001", _param))
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
                                            dataGridRow["STOCKUNIT"] = row["STOCKUNIT"];
                                        });
                                    });

            condition.Conditions.AddTextBox("P_MATERIALID").SetLabel("CONSUMABLEIDNAME");

            condition.GridColumns.AddTextBoxColumn("MATERIALID", 90).SetLabel("CONSUMABLEDEFID");
            condition.GridColumns.AddTextBoxColumn("CONSUMABLEDEFNAME", 170);
            condition.GridColumns.AddComboBoxColumn("STOCKUNIT", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ReceivePayOutUnit", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            #endregion Grid 자재ID 설정

            grdMain.View.AddTextBoxColumn("CONSUMABLEDEFNAME", 150).SetIsReadOnly();
            grdMain.View.AddComboBoxColumn("STOCKUNIT", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ReceivePayOutUnit", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsReadOnly();
            grdMain.View.AddSpinEditColumn("QUANTITY", 180).SetLabel("BOMQTY").SetDisplayFormat("###,###,##0.#########", MaskTypes.Numeric).SetValidationIsRequired().SetTextAlignment(TextAlignment.Right);
            grdMain.View.AddTextBoxColumn("MODIFIBASIS", 150);
            grdMain.View.AddTextBoxColumn("CREATOR", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdMain.View.AddTextBoxColumn("CREATEDTIME", 130).SetIsReadOnly().SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetTextAlignment(TextAlignment.Center);
            grdMain.View.AddTextBoxColumn("MODIFIER", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdMain.View.AddTextBoxColumn("MODIFIEDTIME", 130).SetIsReadOnly().SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetTextAlignment(TextAlignment.Center);

            grdMain.View.PopulateColumns();

            grdMain.ShowStatusBar = true;

            #endregion 기준정보

            #region 변경 내용

            grdHistory.GridButtonItem = GridButtonItem.Export;
            grdHistory.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            grdHistory.View.AddComboBoxColumn("TXNID", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=TxnIDState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetTextAlignment(TextAlignment.Left);
            grdHistory.View.AddTextBoxColumn("EQUIPMENTID", 100);
            grdHistory.View.AddTextBoxColumn("EQUIPMENTNAME", 150).SetIsReadOnly();
            grdHistory.View.AddTextBoxColumn("AREANAME", 150);
            grdHistory.View.AddTextBoxColumn("MATERIALID", 100).SetLabel("CONSUMABLEDEFID");
            grdHistory.View.AddTextBoxColumn("CONSUMABLEDEFNAME", 120);
            grdHistory.View.AddComboBoxColumn("STOCKUNIT", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ReceivePayOutUnit", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdHistory.View.AddSpinEditColumn("QUANTITY", 120).SetDisplayFormat("###,###,##0.#########", MaskTypes.Numeric).SetLabel("BOMQTY").SetTextAlignment(TextAlignment.Right);
            grdHistory.View.AddTextBoxColumn("MODIFIBASIS", 150);
            grdHistory.View.AddTextBoxColumn("CREATOR", 80).SetIsHidden().SetTextAlignment(TextAlignment.Center);
            grdHistory.View.AddTextBoxColumn("CREATEDTIME", 130).SetIsHidden().SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetTextAlignment(TextAlignment.Center);
            grdHistory.View.AddTextBoxColumn("MODIFIER", 80).SetTextAlignment(TextAlignment.Center);
            grdHistory.View.AddTextBoxColumn("MODIFIEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetTextAlignment(TextAlignment.Center);

            grdHistory.View.PopulateColumns();
            grdHistory.View.SetIsReadOnly();

            grdHistory.ShowStatusBar = true;

            #endregion 변경 내용
        }

        #endregion 컨텐츠 영역 초기화

        #region Event

        /// <summary>
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            // Tab Page 변경 이벤트
            tabMain.SelectedPageChanged += (s, e) =>
            {
                SetConditionVisiblility("P_PERIOD", e.Page.Equals(tabPageStandard) ? LayoutVisibility.Never : LayoutVisibility.Always);
                GetToolbarButtonById("Save").Visible = e.Page.Equals(tabPageStandard) ? true : false;
            };

            // Grid에 신규Row 발생시 이벤트
            grdMain.View.InitNewRow += (s, e) =>
            {
                grdMain.View.SetRowCellValue(e.RowHandle, "MATERIALTYPE", "Chemical");
                grdMain.View.SetRowCellValue(e.RowHandle, "CHANGECOUNT", 0);
                grdMain.View.SetRowCellValue(e.RowHandle, "ENTERPRISEID", UserInfo.Current.Enterprise);
                grdMain.View.SetRowCellValue(e.RowHandle, "PLANTID", UserInfo.Current.Plant);
            };

            // BOM소요량 입력시 표준소요량에 동일한 값을 입력하는 이벤트
            grdMain.View.CellValueChanged += (s, e) =>
            {
                if (!e.Column.FieldName.Equals("QUANTITY"))
                {
                    return;
                }

                grdMain.View.GetDataRow(e.RowHandle)["MODIFIBASIS"] = "";
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
                                grdMain.View.SetRowCellValue(grdMain.View.GetFocusedDataSourceRowIndex(), "STOCKUNIT", string.Empty);
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
                                grdMain.View.SetRowCellValue(grdMain.View.GetFocusedDataSourceRowIndex(), "STOCKUNIT", string.Empty);
                                break;
                        }
                    }
                };
            });

            Conditions.GetControls<SmartSelectPopupEdit>().ForEach(control =>
            {
                // 조회조건의 ID 항목을 backSpace로 모두 삭제시에 이름 삭제
                control.EditValueChanged += (s, e) =>
                {
                    if (Format.GetFullTrimString(control.EditValue).Equals(string.Empty))
                    {
                        switch (control.Name)
                        {
                            case "EQUIPMENTID":
                                Conditions.GetControl<SmartTextBox>("EQUIPMENTNAME").Text = string.Empty;
                                break;

                            case "AREAID":
                                Conditions.GetControl<SmartTextBox>("AREANAME").Text = string.Empty;
                                break;

                            case "MATERIALID":
                                Conditions.GetControl<SmartTextBox>("CONSUMABLEDEFNAME").Text = string.Empty;
                                break;

                            default:
                                break;
                        }
                    }
                };
            });
        }

        #endregion Event

        #region 툴바

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            ExecuteRule("SubMaterialBomInfo", grdMain.GetChangedRows());
        }

        #endregion 툴바

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

                Dictionary<string, object> searchParam = Conditions.GetValues();
                searchParam.Add("P_MATERIALTYPE", "Chemical");
                searchParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                SmartBandedGrid grid = tabMain.SelectedTabPageIndex.Equals(0) ? grdMain : grdHistory;

                grid.View.ClearDatas();

                if (await SqlExecuter.QueryAsync(tabMain.SelectedTabPageIndex.Equals(0) ? "SelectSubMaterialBomInfo" : "SelectSubMaterialBomInfoHistory", "10001", searchParam) is DataTable dt)
                {
                    if (dt.Rows.Count.Equals(0))
                    {
                        ShowMessage("NoSelectData");
                        return;
                    }

                    grid.DataSource = dt;
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

            #region 설비

            var condition = Conditions.AddSelectPopup("EQUIPMENTID", new SqlQuery("GetEquipmentAndArea", "10001", _param))
                                      .SetPopupLayout("EQUIPMENTIDNAME", PopupButtonStyles.Ok_Cancel, true, true)
                                      .SetPopupLayoutForm(400, 600, FormBorderStyle.FixedToolWindow)
                                      .SetPopupResultCount(1)
                                      .SetPopupAutoFillColumns("EQUIPMENTNAME")
                                      .SetPosition(2)
                                      .SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
                                      {
                                          selectedRow.ForEach(row =>
                                          {
                                              Conditions.GetControl<SmartTextBox>("EQUIPMENTNAME").Text = Format.GetString(row.GetObject("EQUIPMENTNAME"), "");
                                              Conditions.GetControl<SmartTextBox>("AREANAME").Text = string.Empty;
                                          });
                                      });

            condition.Conditions.AddTextBox("P_EQUIPMENT").SetLabel("EQUIPMENTIDNAME");

            condition.GridColumns.AddTextBoxColumn("EQUIPMENTID", 80);
            condition.GridColumns.AddTextBoxColumn("EQUIPMENTNAME", 120);

            // 설비명
            Conditions.AddTextBox("EQUIPMENTNAME").SetIsReadOnly().SetPosition(3);

            #endregion 설비

            #region 작업장

            condition = Conditions.AddSelectPopup("AREAID", new SqlQuery("GetAreaID", "10001", _param))
                                  .SetPopupLayout("AREAIDNAME", PopupButtonStyles.Ok_Cancel, true, true)
                                  .SetPopupLayoutForm(500, 600, FormBorderStyle.FixedToolWindow)
                                  .SetPopupResultCount(1)
                                  .SetPopupAutoFillColumns("AREANAME")
                                  .SetLabel("AREA")
                                  .SetPosition(4)
                                  .SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
                                  {
                                      selectedRow.ForEach(row =>
                                      {
                                          Conditions.GetControl<SmartTextBox>("AREANAME").Text = Format.GetString(row.GetObject("AREANAME"), "");
                                      });
                                  });

            condition.Conditions.AddTextBox("P_AREA").SetLabel("AREAIDNAME");

            condition.GridColumns.AddTextBoxColumn("AREAID", 120);
            condition.GridColumns.AddTextBoxColumn("AREANAME", 180);

            // 작업장명
            Conditions.AddTextBox("AREANAME").SetIsReadOnly().SetPosition(5);

            #endregion 작업장

            #region 자재

            condition = Conditions.AddSelectPopup("MATERIALID", new SqlQuery("GetMaterialID", "10001", _param))
                                  .SetPopupLayout("CONSUMABLE", PopupButtonStyles.Ok_Cancel, true, true)
                                  .SetPopupLayoutForm(700, 500, FormBorderStyle.FixedToolWindow)
                                  .SetPopupResultCount(1)
                                  .SetPopupAutoFillColumns("CONSUMABLEDEFNAME")
                                  .SetLabel("CONSUMABLEDEFID")
                                  .SetPosition(6)
                                  .SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
                                  {
                                      selectedRow.ForEach(row =>
                                      {
                                          Conditions.GetControl<SmartTextBox>("CONSUMABLEDEFNAME").Text = Format.GetString(row.GetObject("CONSUMABLEDEFNAME"), "");
                                      });
                                  });

            condition.Conditions.AddTextBox("P_MATERIALID").SetLabel("CONSUMABLEIDNAME");

            condition.GridColumns.AddTextBoxColumn("MATERIALID", 90).SetLabel("CONSUMABLEDEFID");
            condition.GridColumns.AddTextBoxColumn("CONSUMABLEDEFNAME", 170);
            condition.GridColumns.AddComboBoxColumn("STOCKUNIT", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ReceivePayOutUnit", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            // 자재명
            Conditions.AddTextBox("CONSUMABLEDEFNAME").SetIsReadOnly().SetPosition(7);

            #endregion 자재
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // 초기 기준정보 Tab일 경우 조회기간은 표시하지 않는다.
            SetConditionVisiblility("P_PERIOD", LayoutVisibility.Never);
        }

        #endregion 검색

        #region 유효성 검사

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            grdMain.View.CheckValidation();

            if (grdMain.GetChangedRows().Rows.Count.Equals(0))
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }

            foreach (DataRow dr in grdMain.GetChangedRows().Rows)
            {
                if (Format.GetString(dr.GetObject("MODIFIBASIS"), string.Empty).Equals(string.Empty))
                {
                    if (!Convert.ToInt32(Format.GetDouble(dr.GetObject("CHANGECOUNT"), 0)).Equals(0))
                    {
                        // 수정근거를 입력해야 합니다.
                        throw MessageException.Create("NoModifibasisMessage");
                    }
                }
            }
        }

        #endregion 유효성 검사
    }
}