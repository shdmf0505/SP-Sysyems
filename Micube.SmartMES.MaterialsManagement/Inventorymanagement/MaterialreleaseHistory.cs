#region using

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

namespace Micube.SmartMES.MaterialsManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 자재관리 > 자재 재고관리 > 자재 출고 이력
    /// 업  무  설  명  : 자재기타출고 / 자재 재고 조정 / 자재 이동 등록 History 조회 화면
    /// 생    성    자  : 전우성
    /// 생    성    일  : 2021-03-08
    /// 수  정  이  력  :
    ///
    ///
    /// </summary>
    public partial class MaterialreleaseHistory : SmartConditionManualBaseForm
    {
        #region Global Variable

        /// <summary>
        /// SelectPopup에 들어가는 parameter 전역 변수
        /// </summary>
        private readonly Dictionary<string, object> _param = new Dictionary<string, object>
        {
            { "LANGUAGETYPE", UserInfo.Current.LanguageType },
            { "ENTERPRISEID", UserInfo.Current.Enterprise },
            { "PLANTID", UserInfo.Current.Plant },
            { "USERID", UserInfo.Current.Id }
        };

        #endregion Global Variable

        #region 생성자

        public MaterialreleaseHistory()
        {
            InitializeComponent();
        }

        #endregion 생성자

        #region 컨텐츠 영역 초기화

        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeLanguageKey();
            InitializeGrid();
            InitializeEvent();
        }

        /// <summary>
        /// Language Key 초기화
        /// </summary>
        private void InitializeLanguageKey()
        {
            grdMain.LanguageKey = "LIST";
        }

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid(string type = "")
        {
            grdMain.GridButtonItem = GridButtonItem.Export;
            grdMain.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            grdMain.View.AddTextBoxColumn("CLASS", 100);
            grdMain.View.AddTextBoxColumn("WAREHOUSENAME", 120);
            grdMain.View.AddTextBoxColumn("AREANAME", 200);
            grdMain.View.AddTextBoxColumn("CONSUMABLEDEFID", 100);
            grdMain.View.AddTextBoxColumn("CONSUMABLEDEFNAME", 200);
            grdMain.View.AddTextBoxColumn("CONSUMABLEDEFVERSION", 80);
            grdMain.View.AddTextBoxColumn("CONSUMABLELOTID", 130);
            grdMain.View.AddTextBoxColumn("QTY", 60).SetTextAlignment(TextAlignment.Right);
            grdMain.View.AddTextBoxColumn("UNIT", 60).SetTextAlignment(TextAlignment.Right);

            switch (type)
            {
                case "EtcOutbound":
                    grdMain.View.AddTextBoxColumn("TRANSACTIONREASONCODE", 110);
                    grdMain.View.AddTextBoxColumn("TRANSACTIONREASONCODENAME", 140);
                    grdMain.View.AddTextBoxColumn("COSTCENTERCODE", 90);
                    grdMain.View.AddTextBoxColumn("COSTCENTERNAME", 140);
                    grdMain.View.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 140);
                    grdMain.View.AddTextBoxColumn("EQUIPMENTNAME", 140);

                    break;

                case "Adjust":
                    grdMain.View.AddTextBoxColumn("TRANSACTIONREASONCODE", 100);
                    grdMain.View.AddTextBoxColumn("TRANSACTIONREASONCODENAME", 140);
                    grdMain.View.AddTextBoxColumn("COSTCENTERCODE", 70);
                    grdMain.View.AddTextBoxColumn("COSTCENTERNAME", 100);
                    break;

                case "Move":
                    grdMain.View.AddTextBoxColumn("TOAREAID", 100);
                    grdMain.View.AddTextBoxColumn("TOAREANAME", 120);
                    grdMain.View.AddTextBoxColumn("TOWAREHOUSEID", 100);
                    grdMain.View.AddTextBoxColumn("TOWAREHOUSENAME", 120);
                    break;

                default:
                    grdMain.View.AddTextBoxColumn("TRANSACTIONREASONCODE", 100);
                    grdMain.View.AddTextBoxColumn("TRANSACTIONREASONCODENAME", 140);
                    grdMain.View.AddTextBoxColumn("COSTCENTERCODE", 70);
                    grdMain.View.AddTextBoxColumn("COSTCENTERNAME", 100);
                    grdMain.View.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 140);
                    grdMain.View.AddTextBoxColumn("EQUIPMENTNAME", 120);
                    grdMain.View.AddTextBoxColumn("TOAREAID", 100);
                    grdMain.View.AddTextBoxColumn("TOAREANAME", 120);
                    grdMain.View.AddTextBoxColumn("TOWAREHOUSEID", 100);
                    grdMain.View.AddTextBoxColumn("TOWAREHOUSENAME", 120);
                    break;
            }

            grdMain.View.AddTextBoxColumn("REMARK", 200);
            grdMain.View.AddTextBoxColumn("SENDOR", 80).SetTextAlignment(TextAlignment.Center);
            grdMain.View.AddTextBoxColumn("SENDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetTextAlignment(TextAlignment.Center);

            grdMain.View.PopulateColumns();
            grdMain.View.SetIsReadOnly();

            grdMain.ShowStatusBar = true;
        }

        #endregion 컨텐츠 영역 초기화

        #region Event

        /// <summary>
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            Conditions.GetControls<SmartSelectPopupEdit>().ForEach(control =>
            {
                // 조회조건의 ID 항목이 삭제시에 이름 삭제
                control.EditValueChanged += (s, e) =>
                {
                    if (Format.GetFullTrimString(control.EditValue).Equals(string.Empty))
                    {
                        switch (control.Name)
                        {
                            case "AREAID":
                                Conditions.GetControl<SmartTextBox>("AREANAME").Text = string.Empty;
                                break;

                            case "WAREHOUSEID":
                                Conditions.GetControl<SmartTextBox>("WAREHOUSENAME").Text = string.Empty;
                                break;

                            case "CONSUMABLEDEFID":
                                Conditions.GetControl<SmartTextBox>("CONSUMABLEDEFNAME").Text = string.Empty;
                                break;

                            case "PROCESSSEGMENTCLASSID":
                                Conditions.GetControl<SmartTextBox>("PROCESSSEGMENTCLASSNAME").Text = string.Empty;
                                break;

                            case "EQUIPMENTID":
                                Conditions.GetControl<SmartTextBox>("EQUIPMENTNAME").Text = string.Empty;
                                break;

                            default:
                                break;
                        }
                    }
                };
            });
        }

        #endregion Event

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

                grdMain.View.ClearColumns();
                grdMain.View.ClearDatas();

                Dictionary<string, object> param = Conditions.GetValues();
                param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                param.Add("USERID", UserInfo.Current.Id);
                param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                if (await SqlExecuter.QueryAsync("SelectMaterialreleaseHistory", "10001", param) is DataTable dt)
                {
                    if (dt.Rows.Count.Equals(0))
                    {
                        ShowMessage("NoSelectData");
                        return;
                    }

                    InitializeGrid(Format.GetString(param["P_STOCKTYPE"], string.Empty));

                    grdMain.DataSource = dt;
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
            #region 창고

            var condition = Conditions.AddSelectPopup("WAREHOUSEID", new SqlQuery("GetWarehouseidByCsm", "10001", _param))
                                      .SetPopupLayout("TXTWAREHOUSE", PopupButtonStyles.Ok_Cancel, true, true)
                                      .SetPopupLayoutForm(500, 600, FormBorderStyle.FixedToolWindow)
                                      .SetPopupResultCount(1)
                                      .SetPopupAutoFillColumns("WAREHOUSENAME")
                                      .SetPosition(1.11)
                                      .SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
                                      {
                                          selectedRow.ForEach(row =>
                                          {
                                              Conditions.GetControl<SmartTextBox>("WAREHOUSENAME").Text = Format.GetString(row.GetObject("WAREHOUSENAME"), string.Empty);
                                          });
                                      });

            condition.Conditions.AddTextBox("P_WAREHOUSENAME").SetLabel("TXTWAREHOUSE");

            condition.GridColumns.AddTextBoxColumn("WAREHOUSEID", 120);
            condition.GridColumns.AddTextBoxColumn("WAREHOUSENAME", 180);

            // 창고명
            Conditions.AddTextBox("WAREHOUSENAME").SetIsReadOnly().SetPosition(1.12);

            #endregion 창고

            #region 작업장

            condition = Conditions.AddSelectPopup("AREAID", new SqlQuery("GetAreaIDByCsm", "10001", _param))
                                  .SetPopupLayout("AREAIDNAME", PopupButtonStyles.Ok_Cancel, true, true)
                                  .SetPopupLayoutForm(500, 600, FormBorderStyle.FixedToolWindow)
                                  .SetPopupResultCount(1)
                                  .SetPopupAutoFillColumns("AREANAME")
                                  .SetPosition(1.21)
                                  .SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
                                  {
                                      selectedRow.ForEach(row =>
                                      {
                                          Conditions.GetControl<SmartTextBox>("AREANAME").Text = Format.GetString(row.GetObject("AREANAME"), string.Empty);
                                      });
                                  });

            condition.Conditions.AddTextBox("P_AREANAME").SetLabel("AREAIDNAME");

            condition.GridColumns.AddTextBoxColumn("AREAID", 120);
            condition.GridColumns.AddTextBoxColumn("AREANAME", 180);

            // 작업장명
            Conditions.AddTextBox("AREANAME").SetIsReadOnly().SetPosition(1.22);

            #endregion 작업장

            #region 자재

            condition = Conditions.AddSelectPopup("CONSUMABLEDEFID", new SqlQuery("GetConsumableDefinition", "10001", _param))
                                  .SetPopupLayout("CONSUMABLE", PopupButtonStyles.Ok_Cancel, true, true)
                                  .SetPopupLayoutForm(500, 600, FormBorderStyle.FixedToolWindow)
                                  .SetPopupResultCount(1)
                                  .SetPopupAutoFillColumns("CONSUMABLEDEFNAME")
                                  .SetLabel("CONSUMABLEDEFID")
                                  .SetPosition(1.31)
                                  .SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
                                  {
                                      selectedRow.ForEach(row =>
                                      {
                                          Conditions.GetControl<SmartTextBox>("CONSUMABLEDEFNAME").Text = Format.GetString(row.GetObject("CONSUMABLEDEFNAME"), "");
                                      });
                                  });

            condition.Conditions.AddTextBox("P_CONSUMABLEIDNAME").SetLabel("CONSUMABLEIDNAME");

            condition.GridColumns.AddTextBoxColumn("CONSUMABLEDEFID", 120);
            condition.GridColumns.AddTextBoxColumn("CONSUMABLEDEFVERSION", 80);
            condition.GridColumns.AddTextBoxColumn("CONSUMABLEDEFNAME", 180);

            // 자재명
            Conditions.AddTextBox("CONSUMABLEDEFNAME").SetIsReadOnly().SetPosition(1.32);

            #endregion 자재

            #region 공정그룹ID (중공정)

            condition = Conditions.AddSelectPopup("PROCESSSEGMENTCLASSID", new SqlQuery("GetProcessSegMentMiddle", "10003", _param))
                                  .SetPopupLayout("TXTLOADMIDDLESEGMENTCLASS", PopupButtonStyles.Ok_Cancel, true, true)
                                  .SetPopupLayoutForm(500, 600, FormBorderStyle.FixedToolWindow)
                                  .SetPopupResultCount(1)
                                  .SetPopupAutoFillColumns("PROCESSSEGMENTCLASSNAME")
                                  .SetPosition(1.41)
                                  .SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
                                  {
                                      selectedRow.ForEach(row =>
                                      {
                                          Conditions.GetControl<SmartTextBox>("PROCESSSEGMENTCLASSNAME").Text = Format.GetString(row.GetObject("PROCESSSEGMENTCLASSNAME"), "");
                                      });
                                  });

            condition.Conditions.AddTextBox("PROCESSSEGMENTCLASSID").SetLabel("TXTLOADMIDDLESEGMENTCLASS");

            condition.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 80);
            condition.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 120);

            // 공정그룹명
            Conditions.AddTextBox("PROCESSSEGMENTCLASSNAME").SetIsReadOnly().SetPosition(1.42);

            #endregion 공정그룹ID (중공정)

            #region 설비

            condition = Conditions.AddSelectPopup("EQUIPMENTID", new SqlQuery("GetEquipmentAndArea", "10001", _param))
                                  .SetPopupLayout("EQUIPMENTIDNAME", PopupButtonStyles.Ok_Cancel, true, true)
                                  .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                                  .SetPopupResultCount(1)
                                  .SetPosition(1.51)
                                  .SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
                                  {
                                      selectedRow.ForEach(row =>
                                      {
                                          Conditions.GetControl<SmartTextBox>("EQUIPMENTNAME").Text = Format.GetString(row.GetObject("EQUIPMENTNAME"), "");
                                      });
                                  });

            condition.Conditions.AddTextBox("P_EQUIPMENT").SetLabel("EQUIPMENTIDNAME");

            condition.GridColumns.AddTextBoxColumn("EQUIPMENTID", 80);
            condition.GridColumns.AddTextBoxColumn("EQUIPMENTNAME", 150);
            condition.GridColumns.AddTextBoxColumn("AREANAME", 150);

            // 설비명
            Conditions.AddTextBox("EQUIPMENTNAME").SetIsReadOnly().SetPosition(1.52);

            #endregion 설비
        }

        #endregion 검색
    }
}