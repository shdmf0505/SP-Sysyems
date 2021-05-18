#region using

using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons.Controls;
using Micube.SmartMES.MaterialsManagement.Popup;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

#endregion using

namespace Micube.SmartMES.MaterialsManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 자재관리 > 자재재고관리 > 자재 기타 출고
    /// 업  무  설  명  : 자재 기타 출고를 관리한다.
    /// 생    성    자  : 최별
    /// 생    성    일  : 2019-07-30
    /// 수  정  이  력  :
    ///     2021.02.22 전우성 : 자재ID 입력 후 재조회 안되는 오류 수정
    ///                         자재ID 변경 시 본 Row가 삭제 후 맨하단에 생기는 오류 수정
    ///
    /// </summary>
    public partial class MaterialOtherGoodsIssue : SmartConditionManualBaseForm
    {
        #region Local Variables

        private string _sWorkTime = "";
        private bool blSave = true;  //저장 여부 확인 처리
        private bool blSavePeriodid = true;  //재고마감여부
        private bool blIsuseplantauthority = true;
        private DataTable _dtSite = null;
        private string _strDateChangeAuth = "";

        #endregion Local Variables

        #region 생성자

        public MaterialOtherGoodsIssue()
        {
            InitializeComponent();
        }

        #endregion 생성자

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 화면의 컨텐츠 영역을 초기화한다.
        /// </summary>
        protected override void InitializeContent()
        {
            base.InitializeContent();
            _strDateChangeAuth = "false";
            if (pnlToolbar.Controls["layoutToolbar"] != null)
            {
                if (pnlToolbar.Controls["layoutToolbar"].Controls["DateChangeAuth"] != null)
                {
                    //if (pnlToolbar.Controls["layoutToolbar"].Controls["DateChangeAuth"].Visible == true)
                    {
                        pnlToolbar.Controls["layoutToolbar"].Controls["DateChangeAuth"].Visible = false;
                        _strDateChangeAuth = "true";
                    }
                }
            }

            InitializeEvent();
            InitializeGrid();
            InitializeComboBox();
            OnPlantidInformationSearch(cboPlantid.EditValue.ToString());
            BtnClear_Click(null, null);
        }

        /// <summary>
        /// SmartComboBox 컨트롤 추가
        /// </summary>
        private void InitializeComboBox()
        {
            cboPlantid.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboPlantid.ValueMember = "PLANTID";
            cboPlantid.DisplayMember = "PLANTID";
            cboPlantid.EditValue = UserInfo.Current.Plant.ToString();
            _dtSite = SqlExecuter.Query("GetPlantListAuthorityBycms", "10001", new Dictionary<string, object>() { { "USERID", UserInfo.Current.Id }, { "LANGUAGETYPE", UserInfo.Current.LanguageType } });
            cboPlantid.DataSource = _dtSite;
            cboPlantid.ShowHeader = false;
            DataRow[] rowsPlant = _dtSite.Select("PLANTID = '" + UserInfo.Current.Plant.ToString() + "'");

            if (rowsPlant.Length > 0)
            {
                if (rowsPlant[0]["ISUSEPLANTAUTHORITY"].ToString().Equals("Y"))
                {
                    blIsuseplantauthority = true;

                    if (pnlToolbar.Controls["layoutToolbar"] != null)
                    {
                        if (pnlToolbar.Controls["layoutToolbar"].Controls["Save"] != null)
                        {
                            pnlToolbar.Controls["layoutToolbar"].Controls["Save"].Enabled = true;
                        }

                        if (pnlToolbar.Controls["layoutToolbar"].Controls["Initialization"] != null)
                        {
                            pnlToolbar.Controls["layoutToolbar"].Controls["Initialization"].Enabled = true;
                        }
                    }
                }
                else
                {
                    blIsuseplantauthority = false;

                    if (pnlToolbar.Controls["layoutToolbar"] != null)
                    {
                        if (pnlToolbar.Controls["layoutToolbar"].Controls["Save"] != null)
                        {
                            pnlToolbar.Controls["layoutToolbar"].Controls["Save"].Enabled = false;
                        }
                        if (pnlToolbar.Controls["layoutToolbar"].Controls["Initialization"] != null)
                        {
                            pnlToolbar.Controls["layoutToolbar"].Controls["Initialization"].Enabled = false;
                        }
                    }
                }
            }

            selectOspAreaidPopup(UserInfo.Current.Plant.ToString());
        }

        /// <summary>
        /// 작업장 Select Popup
        /// </summary>
        /// <param name="sPlantid"></param>

        private void selectOspAreaidPopup(string sPlantid)
        {
            ConditionItemSelectPopup popup = new ConditionItemSelectPopup();
            popup.SetPopupLayoutForm(520, 600, FormBorderStyle.FixedDialog);
            popup.SetPopupLayout("AREANAME", PopupButtonStyles.Ok_Cancel, true, false);
            popup.Id = "AREANAME";
            popup.LabelText = "AREANAME";
            popup.SearchQuery = new SqlQuery("GetAreaidListAuthorityByCsm", "10001", $"P_ENTERPRISEID={UserInfo.Current.Enterprise}"
                                                                          , $"USERID={UserInfo.Current.Id}"
                                                                          , $"LANGUAGETYPE={UserInfo.Current.LanguageType}"
                                                                          , $"P_PLANTID={sPlantid}");
            popup.IsMultiGrid = false;
            popup.SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
            {
                foreach (DataRow row in selectedRow)
                {
                    if (selectedRow.Count() > 0)
                    {
                        txtwarehouseid.Text = row["WAREHOUSEID"].ToString();
                        txtwarehousename.Text = row["WAREHOUSENAME"].ToString();

                        if (row["ISMODIFY"].ToString().Equals("Y"))
                        {
                            if (blIsuseplantauthority)
                            {
                                if (pnlToolbar.Controls["layoutToolbar"] != null)
                                {
                                    if (pnlToolbar.Controls["layoutToolbar"].Controls["Save"] != null)
                                    {
                                        pnlToolbar.Controls["layoutToolbar"].Controls["Save"].Enabled = true;
                                    }

                                    if (pnlToolbar.Controls["layoutToolbar"].Controls["Initialization"] != null)
                                    {
                                        pnlToolbar.Controls["layoutToolbar"].Controls["Initialization"].Enabled = true;
                                    }
                                }
                            }
                            else
                            {
                                if (pnlToolbar.Controls["layoutToolbar"] != null)
                                {
                                    if (pnlToolbar.Controls["layoutToolbar"].Controls["Save"] != null)
                                    {
                                        pnlToolbar.Controls["layoutToolbar"].Controls["Save"].Enabled = false;
                                    }

                                    if (pnlToolbar.Controls["layoutToolbar"].Controls["Initialization"] != null)
                                    {
                                        pnlToolbar.Controls["layoutToolbar"].Controls["Initialization"].Enabled = true;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (pnlToolbar.Controls["layoutToolbar"] != null)
                            {
                                if (pnlToolbar.Controls["layoutToolbar"].Controls["Save"] != null)
                                {
                                    pnlToolbar.Controls["layoutToolbar"].Controls["Save"].Enabled = false;
                                }

                                if (pnlToolbar.Controls["layoutToolbar"].Controls["Initialization"] != null)
                                {
                                    pnlToolbar.Controls["layoutToolbar"].Controls["Initialization"].Enabled = true;
                                }
                            }
                        }
                    }
                }
            });

            popup.DisplayFieldName = "AREANAME";
            popup.ValueFieldName = "AREAID";
            popup.LanguageKey = "AREAID";

            popup.Conditions.AddTextBox("P_AREANAME").SetLabel("AREANAME");
            popup.GridColumns.AddTextBoxColumn("AREAID", 120).SetLabel("AREAID");
            popup.GridColumns.AddTextBoxColumn("AREANAME", 200).SetLabel("AREANAME");

            popupOspAreaid.SelectPopupCondition = popup;
        }

        /// <summary>
        /// 코드그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            grdMain.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export;
            grdMain.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdMain.View.AddTextBoxColumn("ENTERPRISEID", 150).SetIsHidden();
            grdMain.View.AddTextBoxColumn("PLANTID", 150).SetIsHidden();
            grdMain.View.AddTextBoxColumn("TRANSACTIONTYPE", 150).SetIsHidden();
            grdMain.View.AddTextBoxColumn("TRANSACTIONNO", 150).SetIsHidden();
            grdMain.View.AddTextBoxColumn("TRANSACTIONDATE", 150).SetIsHidden();
            grdMain.View.AddTextBoxColumn("TRANSACTIONSEQUENCE", 80).SetIsHidden();

            //grdMaterial.View.AddSelectPopupColumn("CONSUMABLEDEFID", 120, new popupMaterial())
            //                .SetValidationIsRequired()
            //                .SetPopupCustomParameter((ISmartCustomPopup popup, DataRow dataRow) =>
            //                {
            //                    (popup as popupMaterial).SetMaterial(Format.GetString(dataRow["CONSUMABLEDEFID"], string.Empty));

            //                    (popup as popupMaterial).SelectedRowTableEvent += (dt) =>
            //                    {
            //                        (grdMaterial.DataSource as DataTable).Rows.RemoveAt(grdMaterial.View.FocusedRowHandle);

            //                        DataRow newRow;
            //                        DateTime dttransdate = Convert.ToDateTime(dptTransactiondate.EditValue.ToString());

            //                        foreach (DataRow dr in dt.Rows)
            //                        {
            //                            newRow = (grdMaterial.DataSource as DataTable).NewRow();

            //                            newRow["CONSUMABLEDEFID"] = dr["CONSUMABLEDEFID"];
            //                            newRow["CONSUMABLEDEFNAME"] = dr["CONSUMABLEDEFNAME"];
            //                            newRow["CONSUMABLEDEFVERSION"] = dr["CONSUMABLEDEFVERSION"];
            //                            newRow["CONSUMABLELOTID"] = dr["CONSUMABLELOTID"];
            //                            newRow["ISLOTMNG"] = dr["ISLOTMNG"];
            //                            newRow["TRANSACTIONTYPE"] = "EtcOutbound";
            //                            newRow["UNIT"] = dr["UNIT"];
            //                            newRow["PLANTID"] = cboPlantid.EditValue;
            //                            newRow["STOCKQTY"] = dr["STOCKQTY"];
            //                            newRow["TRANSACTIONCODE"] = "MISC_ISSUE";
            //                            newRow["FROMAREAID"] = popupOspAreaid.GetValue();
            //                            newRow["AVAILABLEQTY"] = dr["AVAILABLEQTY"];
            //                            newRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;

            //                            newRow["TRANSACTIONNO"] = btnTransactionno.EditValue.ToString();
            //                            newRow["FROMWAREHOUSEID"] = txtwarehouseid.EditValue.ToString();
            //                            newRow["FROMWAREHOUSENAME"] = txtwarehousename.Text;
            //                            newRow["FROMAREANAME"] = popupOspAreaid.Text;
            //                            newRow["TRANSACTIONDATE"] = dptTransactiondate.EditValue.ToString();
            //                            newRow["TRANSACTIONDATE"] = dptTransactiondate.EditValue.ToString();
            //                            newRow["WORKTIME"] = _sWorkTime;
            //                            newRow["YEARMONTH"] = dttransdate.ToString("yyyy-MM");
            //                            newRow["STARTDATE"] = dttransdate.ToString("yyyy-MM-01 " + "00:00:00");
            //                            newRow["ENDDATE"] = dttransdate.ToString("yyyy-MM-dd HH:mm:ss");

            //                            (grdMaterial.DataSource as DataTable).Rows.Add(newRow.ItemArray);
            //                        }
            //                    };
            //                });

            #region 자재

            var control = grdMain.View.AddSelectPopupColumn("CONSUMABLEDEFID", 120, new SqlQuery("GetConsumabledefinitionStockQtyOnlyListByCsm", "10001"))
                                          .SetPopupLayout("CONSUMABLEIDPOPUP", PopupButtonStyles.Ok_Cancel, true, false)
                                          .SetPopupResultCount(0)
                                          .SetPopupResultMapping("CONSUMABLEDEFID", "CONSUMABLEDEFID")
                                          .SetPopupLayoutForm(1200, 600)
                                          .SetValidationIsRequired()
                                          .SetRelationIds("PLANTID", "ENTERPRISEID", "FROMWAREHOUSEID", "YEARMONTH", "STARTDATE", "ENDDATE")
                                          .SetPopupApplySelection((selectedRows, dataGridRow) =>
                                          {
                                              int i = 0;
                                              DataRow row;
                                              DateTime dttransdate = Convert.ToDateTime(dptTransactiondate.EditValue.ToString());

                                              foreach (DataRow classRow in selectedRows)
                                              {
                                                  if (i.Equals(0))
                                                  {
                                                      dataGridRow["CONSUMABLEDEFID"] = classRow["CONSUMABLEDEFID"];
                                                      dataGridRow["CONSUMABLEDEFNAME"] = classRow["CONSUMABLEDEFNAME"];
                                                      dataGridRow["CONSUMABLEDEFVERSION"] = classRow["CONSUMABLEDEFVERSION"];
                                                      dataGridRow["CONSUMABLELOTID"] = classRow["CONSUMABLELOTID"];
                                                      dataGridRow["ISLOTMNG"] = classRow["ISLOTMNG"];
                                                      dataGridRow["UNIT"] = classRow["UNIT"];
                                                      dataGridRow["STOCKQTY"] = classRow["STOCKQTY"];
                                                      dataGridRow["AVAILABLEQTY"] = classRow["AVAILABLEQTY"];
                                                  }
                                                  else
                                                  {
                                                      row = (grdMain.DataSource as DataTable).NewRow();
                                                      row["CONSUMABLEDEFID"] = classRow["CONSUMABLEDEFID"];
                                                      row["CONSUMABLEDEFNAME"] = classRow["CONSUMABLEDEFNAME"];
                                                      row["CONSUMABLEDEFVERSION"] = classRow["CONSUMABLEDEFVERSION"];
                                                      row["CONSUMABLELOTID"] = classRow["CONSUMABLELOTID"];
                                                      row["ISLOTMNG"] = classRow["ISLOTMNG"];
                                                      row["TRANSACTIONTYPE"] = "EtcOutbound";
                                                      row["UNIT"] = classRow["UNIT"];
                                                      row["PLANTID"] = cboPlantid.EditValue;
                                                      row["STOCKQTY"] = classRow["STOCKQTY"];
                                                      row["TRANSACTIONCODE"] = "MISC_ISSUE";
                                                      row["FROMAREAID"] = popupOspAreaid.GetValue();
                                                      row["AVAILABLEQTY"] = classRow["AVAILABLEQTY"];
                                                      row["ENTERPRISEID"] = UserInfo.Current.Enterprise;

                                                      row["TRANSACTIONNO"] = btnTransactionno.EditValue.ToString();
                                                      row["FROMWAREHOUSEID"] = txtwarehouseid.EditValue.ToString();
                                                      row["FROMWAREHOUSENAME"] = txtwarehousename.Text;
                                                      row["FROMAREANAME"] = popupOspAreaid.Text;
                                                      row["TRANSACTIONDATE"] = dptTransactiondate.EditValue.ToString();
                                                      row["TRANSACTIONDATE"] = dptTransactiondate.EditValue.ToString();
                                                      row["WORKTIME"] = _sWorkTime;
                                                      row["YEARMONTH"] = dttransdate.ToString("yyyy-MM");
                                                      row["STARTDATE"] = dttransdate.ToString("yyyy-MM-01 " + "00:00:00");
                                                      row["ENDDATE"] = dttransdate.ToString("yyyy-MM-dd HH:mm:ss");

                                                      (grdMain.DataSource as DataTable).Rows.Add(row);
                                                  }

                                                  i++;
                                              }
                                          });

            control.Conditions.AddComboBox("P_CONSUMABLECLASSID", new SqlQuery("GetConsumableclassListByCsm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CONSUMABLECLASSNAME", "CONSUMABLECLASSID")
                              .SetLabel("CONSUMABLECLASSID")
                              .SetEmptyItem("", "");

            control.Conditions.AddTextBox("P_CONSUMABLEDEFID").SetLabel("CONSUMABLEDEFID");
            control.Conditions.AddTextBox("P_CONSUMABLEDEFNAME").SetLabel("CONSUMABLEDEFNAME");
            control.Conditions.AddTextBox("P_CONSUMABLELOTID").SetLabel("CONSUMABLELOTID");
            control.Conditions.AddTextBox("P_ENTERPRISEID").SetPopupDefaultByGridColumnId("ENTERPRISEID").SetIsHidden();
            control.Conditions.AddTextBox("P_PLANTID").SetPopupDefaultByGridColumnId("PLANTID").SetIsHidden();
            control.Conditions.AddTextBox("P_FROMWAREHOUSEID").SetPopupDefaultByGridColumnId("FROMWAREHOUSEID").SetIsHidden();
            control.Conditions.AddTextBox("P_YEARMONTH").SetPopupDefaultByGridColumnId("YEARMONTH").SetIsHidden();
            control.Conditions.AddTextBox("P_STARTDATE").SetPopupDefaultByGridColumnId("STARTDATE").SetIsHidden();
            control.Conditions.AddTextBox("P_ENDDATE").SetPopupDefaultByGridColumnId("ENDDATE").SetIsHidden();
            control.Conditions.AddTextBox("LANGUAGETYPE").SetDefault(UserInfo.Current.LanguageType).SetIsHidden();

            control.GridColumns.AddComboBoxColumn("CONSUMABLECLASSID", 100, new SqlQuery("GetConsumableclassListByCsm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CONSUMABLECLASSNAME", "CONSUMABLECLASSID").SetIsReadOnly();
            control.GridColumns.AddTextBoxColumn("CONSUMABLEDEFID", 100).SetIsReadOnly();
            control.GridColumns.AddTextBoxColumn("CONSUMABLEDEFVERSION", 80).SetIsReadOnly();
            control.GridColumns.AddTextBoxColumn("CONSUMABLEDEFNAME", 270).SetIsReadOnly();
            control.GridColumns.AddTextBoxColumn("CONSUMABLELOTID", 200).SetIsReadOnly();
            control.GridColumns.AddSpinEditColumn("STOCKQTY", 100).SetDisplayFormat("#,###,##0.#####", MaskTypes.Numeric).SetIsReadOnly();
            control.GridColumns.AddSpinEditColumn("AVAILABLEQTY", 100).SetDisplayFormat("#,###,##0.#####", MaskTypes.Numeric).SetIsReadOnly();
            control.GridColumns.AddTextBoxColumn("UNIT", 80).SetIsHidden();
            control.GridColumns.AddTextBoxColumn("ISLOTMNG", 80).SetIsHidden();

            #endregion 자재

            grdMain.View.AddTextBoxColumn("CONSUMABLEDEFVERSION", 80).SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("CONSUMABLEDEFNAME", 200).SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("CONSUMABLELOTID", 150).SetValidationIsRequired().SetIsReadOnly();
            grdMain.View.AddSpinEditColumn("STOCKQTY", 100).SetDisplayFormat("#,###,##0.#####", MaskTypes.Numeric).SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("AVAILABLEQTY", 100).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,###,##0.#####", MaskTypes.Numeric).SetIsReadOnly();
            grdMain.View.AddSpinEditColumn("QTY", 100).SetValidationIsRequired().SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,###,##0.#####", MaskTypes.Numeric).IsFloatValue = true;
            grdMain.View.AddTextBoxColumn("UNIT", 80).SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("FROMWAREHOUSEID", 150).SetIsHidden();
            grdMain.View.AddTextBoxColumn("FROMWAREHOUSENAME", 150).SetIsHidden();
            grdMain.View.AddTextBoxColumn("FROMAREAID", 150).SetIsHidden();
            grdMain.View.AddTextBoxColumn("FROMAREANAME", 150).SetIsHidden();
            grdMain.View.AddTextBoxColumn("TRANSACTIONCODE", 150).SetIsHidden();

            #region Reason Code 정보

            control = grdMain.View.AddSelectPopupColumn("TRANSACTIONREASONCODE", new SqlQuery("GetTransactionreasoncodeByCsm", "10001"))
                                      .SetPopupLayout("TRANSACTIONREASONCODE", PopupButtonStyles.Ok_Cancel, true, false)
                                      .SetPopupResultCount(1)
                                      .SetPopupResultMapping("TRANSACTIONREASONCODE", "TRANSACTIONREASONCODE")
                                      .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                                      .SetRelationIds("PLANTID", "ENTERPRISEID", "TRANSACTIONCODE")
                                      .SetPopupAutoFillColumns("TRANSACTIONREASONCODENAME")
                                      .SetValidationIsRequired()
                                      .SetPopupApplySelection((selectedRows, dataGridRow) =>
                                      {
                                          DataRow classRow = grdMain.View.GetFocusedDataRow();

                                          foreach (DataRow row in selectedRows)
                                          {
                                              classRow["TRANSACTIONREASONCODENAME"] = row["TRANSACTIONREASONCODENAME"];
                                          }
                                      });

            control.Conditions.AddTextBox("TRANSACTIONREASONCODENAME");
            control.Conditions.AddTextBox("P_ENTERPRISEID").SetPopupDefaultByGridColumnId("ENTERPRISEID").SetIsHidden();
            control.Conditions.AddTextBox("P_PLANTID").SetPopupDefaultByGridColumnId("PLANTID").SetIsHidden();
            control.Conditions.AddTextBox("P_TRANSACTIONCODE").SetPopupDefaultByGridColumnId("TRANSACTIONCODE").SetIsHidden();

            control.GridColumns.AddTextBoxColumn("TRANSACTIONREASONCODE", 150);
            control.GridColumns.AddTextBoxColumn("TRANSACTIONREASONCODENAME", 200);

            #endregion Reason Code 정보

            grdMain.View.AddTextBoxColumn("TRANSACTIONREASONCODENAME", 150);

            #region Costcentercod 정보

            control = grdMain.View.AddSelectPopupColumn("COSTCENTERCODE", new SqlQuery("GetCostcentercodeByCsm", "10001"))
                                      .SetPopupLayout("COSTCENTERNAME", PopupButtonStyles.Ok_Cancel, true, false)
                                      .SetPopupResultCount(1)
                                      .SetPopupResultMapping("COSTCENTERCODE", "COSTCENTERCODE")
                                      .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                                      .SetRelationIds("PLANTID", "FROMAREAID")
                                      .SetValidationIsRequired()
                                      .SetPopupApplySelection((selectedRows, dataGridRow) =>
                                      {
                                          DataRow classRow = grdMain.View.GetFocusedDataRow();

                                          foreach (DataRow row in selectedRows)
                                          {
                                              classRow["COSTCENTERNAME"] = row["COSTCENTERNAME"];
                                          }
                                      });

            control.Conditions.AddTextBox("COSTCENTERNAME");
            control.Conditions.AddTextBox("P_AREAID").SetPopupDefaultByGridColumnId("FROMAREAID").SetIsHidden();

            control.GridColumns.AddTextBoxColumn("COSTCENTERCODE", 100);
            control.GridColumns.AddTextBoxColumn("COSTCENTERNAME", 250);

            #endregion Costcentercod 정보

            grdMain.View.AddTextBoxColumn("COSTCENTERNAME", 100).SetIsReadOnly();

            #region Grid공정 그룹명

            control = grdMain.View.AddSelectPopupColumn("PROCESSSEGMENTCLASSNAME", 100, new SqlQuery("GetProcessSegMentMiddle", "10003"), "PROCESSSEGMENTCLASSNAME")
                                        .SetValidationKeyColumn()
                                        .SetValidationIsRequired()
                                        .SetPopupLayout("TXTLOADMIDDLESEGMENTCLASS", PopupButtonStyles.Ok_Cancel, true, true)
                                        .SetPopupLayoutForm(400, 600, FormBorderStyle.FixedToolWindow)
                                        .SetPopupResultCount(1)
                                        .SetPopupAutoFillColumns("PROCESSSEGMENTCLASSNAME")
                                        .SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
                                        {
                                            selectedRow.ForEach(row =>
                                            {
                                                grdMain.View.SetFocusedRowCellValue("PROCESSSEGMENTCLASSID", selectedRow.FirstOrDefault()["PROCESSSEGMENTCLASSID"]);
                                            });
                                        });

            control.Conditions.AddTextBox("PROCESSSEGMENTCLASSID").SetLabel("TXTLOADMIDDLESEGMENTCLASS");
            control.Conditions.AddTextBox("P_PRODUCTDEFTYPE").SetDefault("Product").SetIsHidden();
            control.Conditions.AddTextBox("PLANTID").SetDefault(UserInfo.Current.Plant).SetIsHidden();
            control.Conditions.AddTextBox("ENTERPRISEID").SetDefault(UserInfo.Current.Enterprise).SetIsHidden();
            control.Conditions.AddTextBox("LANGUAGETYPE").SetDefault(UserInfo.Current.LanguageType).SetIsHidden();

            control.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 80);
            control.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 120);

            #endregion Grid공정 그룹명

            grdMain.View.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 150).SetIsHidden();

            #region 설비

            control = grdMain.View.AddSelectPopupColumn("EQUIPMENTNAME", 100, new SqlQuery("GetEquipmentAndArea", "10001"), "EQUIPMENTNAME")
                                        .SetValidationKeyColumn()
                                        .SetValidationIsRequired()
                                        .SetTextAlignment(TextAlignment.Left)
                                        .SetPopupLayout("EQUIPMENTID", PopupButtonStyles.Ok_Cancel, true, true)
                                        .SetPopupLayoutForm(600, 400, FormBorderStyle.FixedToolWindow)
                                        .SetPopupResultCount(1)
                                        .SetPopupAutoFillColumns("EQUIPMENTNAME")
                                        .SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
                                        {
                                            selectedRow.ForEach(row =>
                                            {
                                                grdMain.View.SetFocusedRowCellValue("EQUIPMENTID", selectedRow.FirstOrDefault()["EQUIPMENTID"]);
                                            });
                                        });

            control.Conditions.AddTextBox("P_EQUIPMENT").SetLabel("EQUIPMENTIDNAME");
            control.Conditions.AddTextBox("PLANTID").SetDefault(UserInfo.Current.Plant).SetIsHidden();
            control.Conditions.AddTextBox("ENTERPRISEID").SetDefault(UserInfo.Current.Enterprise).SetIsHidden();
            control.Conditions.AddTextBox("LANGUAGETYPE").SetDefault(UserInfo.Current.LanguageType).SetIsHidden();

            control.GridColumns.AddTextBoxColumn("EQUIPMENTID", 80);
            control.GridColumns.AddTextBoxColumn("EQUIPMENTNAME", 120);
            control.GridColumns.AddTextBoxColumn("AREANAME", 150);

            #endregion 설비

            grdMain.View.AddTextBoxColumn("EQUIPMENTID", 150).SetIsHidden();

            grdMain.View.AddTextBoxColumn("MATREMARK", 300);
            grdMain.View.AddTextBoxColumn("ISLOTMNG", 150).SetIsHidden();
            grdMain.View.AddTextBoxColumn("YEARMONTH", 150).SetIsHidden();
            grdMain.View.AddTextBoxColumn("STARTDATE", 150).SetIsHidden();
            grdMain.View.AddTextBoxColumn("ENDDATE", 150).SetIsHidden();
            grdMain.View.AddTextBoxColumn("WORKTIME", 150).SetIsHidden();

            grdMain.View.SetKeyColumn("PLANTID", "ENTERPRISEID", "CONSUMABLEDEFID", "CONSUMABLELOTID");
            grdMain.View.PopulateColumns();

            RepositoryItemTextEdit edit = new RepositoryItemTextEdit();
            edit.Mask.EditMask = "#,###,##0.#####";
            edit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;

            grdMain.View.Columns["QTY"].ColumnEdit = edit;
        }

        #endregion 컨텐츠 영역 초기화

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            grdMain.View.AddingNewRow += (s, e) =>
            {
                if (!blSave)
                {
                    int intfocusRow = grdMain.View.FocusedRowHandle;
                    grdMain.View.DeleteRow(intfocusRow);
                    return;
                }

                if (popupOspAreaid.GetValue().ToString().Equals(""))
                {
                    int intfocusRow = grdMain.View.FocusedRowHandle;
                    grdMain.View.DeleteRow(intfocusRow);
                    this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblAreaid.Text); //메세지
                    popupOspAreaid.Focus();
                    return;
                }

                DateTime dttransdate = Convert.ToDateTime(dptTransactiondate.EditValue.ToString());

                grdMain.View.SetFocusedRowCellValue("ENTERPRISEID", UserInfo.Current.Enterprise.ToString());// 회사코드
                grdMain.View.SetFocusedRowCellValue("PLANTID", cboPlantid.EditValue.ToString());// plantid
                grdMain.View.SetFocusedRowCellValue("TRANSACTIONNO", btnTransactionno.EditValue.ToString());
                grdMain.View.SetFocusedRowCellValue("FROMWAREHOUSEID", txtwarehouseid.EditValue.ToString());
                grdMain.View.SetFocusedRowCellValue("FROMWAREHOUSENAME", txtwarehousename.Text.ToString());
                grdMain.View.SetFocusedRowCellValue("FROMAREAID", popupOspAreaid.GetValue().ToString());
                grdMain.View.SetFocusedRowCellValue("FROMAREANAME", popupOspAreaid.Text.ToString());
                grdMain.View.SetFocusedRowCellValue("TRANSACTIONDATE", dptTransactiondate.EditValue.ToString());
                grdMain.View.SetFocusedRowCellValue("TRANSACTIONTYPE", "EtcOutbound");
                grdMain.View.SetFocusedRowCellValue("TRANSACTIONCODE", "MISC_ISSUE");
                grdMain.View.SetFocusedRowCellValue("CONSUMABLELOTID", "*");
                grdMain.View.SetFocusedRowCellValue("WORKTIME", _sWorkTime);
                grdMain.View.SetFocusedRowCellValue("YEARMONTH", dttransdate.ToString("yyyy-MM"));
                grdMain.View.SetFocusedRowCellValue("STARTDATE", dttransdate.ToString("yyyy-MM-01 " + "00:00:00"));
                grdMain.View.SetFocusedRowCellValue("ENDDATE", dttransdate.ToString("yyyy-MM-dd HH:mm:ss"));
            };

            // 입고번호 클릭 시 호출
            btnTransactionno.ButtonClick += (s, e) =>
            {
                if (e.Button.Kind.ToString().Equals("Delete"))
                {
                    BtnClear_Click(null, null);  //화면 clear
                }
                else
                {
                    PopupShow();
                }
            };

            // Plantid 변경 따른 Areaid 변경처리
            cboPlantid.EditValueChanged += (s, e) =>
            {
                if (_dtSite == null)
                {
                    return;
                }

                popupOspAreaid.SetValue("");
                popupOspAreaid.Text = "";
                popupOspAreaid.EditValue = "";
                txtwarehouseid.Text = "";
                txtwarehousename.Text = "";
                DataRow[] rowsPlant = _dtSite.Select("PLANTID = '" + cboPlantid.EditValue.ToString() + "'");

                if (rowsPlant.Length > 0)
                {
                    if (rowsPlant[0]["ISUSEPLANTAUTHORITY"].ToString().Equals("Y"))
                    {
                        blIsuseplantauthority = true;

                        if (pnlToolbar.Controls["layoutToolbar"] != null)
                        {
                            if (pnlToolbar.Controls["layoutToolbar"].Controls["Save"] != null)
                            {
                                pnlToolbar.Controls["layoutToolbar"].Controls["Save"].Enabled = true;
                            }

                            if (pnlToolbar.Controls["layoutToolbar"].Controls["Initialization"] != null)
                            {
                                pnlToolbar.Controls["layoutToolbar"].Controls["Initialization"].Enabled = true;
                            }
                        }
                    }
                    else
                    {
                        blIsuseplantauthority = false;

                        if (pnlToolbar.Controls["layoutToolbar"] != null)
                        {
                            if (pnlToolbar.Controls["layoutToolbar"].Controls["Save"] != null)
                            {
                                pnlToolbar.Controls["layoutToolbar"].Controls["Save"].Enabled = false;
                            }

                            if (pnlToolbar.Controls["layoutToolbar"].Controls["Initialization"] != null)
                            {
                                pnlToolbar.Controls["layoutToolbar"].Controls["Initialization"].Enabled = true;
                            }
                        }
                    }
                }

                selectOspAreaidPopup(cboPlantid.EditValue.ToString());
            };

            // Cell ReadOnly 처리
            grdMain.View.ShowingEditor += (s, e) =>
            {
                if (!(grdMain.View.GetFocusedRowCellValue("TRANSACTIONSEQUENCE").ToString().Equals("")))
                {
                    e.Cancel = true;
                }

                if (grdMain.View.FocusedColumn.FieldName.Equals("QTY") && grdMain.View.GetFocusedRowCellValue("CONSUMABLEDEFID").ToString().Equals(""))
                {
                    e.Cancel = true;
                }
            };

            grdMain.View.Columns.ForEach(control =>
            {
                if (!control.ColumnEdit.GetType().Name.Equals("RepositoryItemButtonEdit"))
                {
                    return;
                }

                // Grid에 SelectPopup 삭제버튼 클릭시 이벤트
                (control.ColumnEdit as RepositoryItemButtonEdit).ButtonClick += (s, e) =>
                {
                    if (e.Button.Kind.Equals(ButtonPredefines.Clear))
                    {
                        switch (control.FieldName)
                        {
                            case "EQUIPMENTNAME":
                                grdMain.View.SetRowCellValue(grdMain.View.GetFocusedDataSourceRowIndex(), "EQUIPMENTID", string.Empty);
                                break;

                            case "PROCESSSEGMENTCLASSNAME":
                                grdMain.View.SetRowCellValue(grdMain.View.GetFocusedDataSourceRowIndex(), "PROCESSSEGMENTCLASSID", string.Empty);
                                break;

                            case "COSTCENTERCODE":
                                grdMain.View.SetRowCellValue(grdMain.View.GetFocusedDataSourceRowIndex(), "COSTCENTERNAME", string.Empty);
                                break;

                            case "TRANSACTIONREASONCODE":
                                grdMain.View.SetRowCellValue(grdMain.View.GetFocusedDataSourceRowIndex(), "TRANSACTIONREASONCODENAME", string.Empty);
                                break;
                        }
                    }
                };

                // Grid에 SelectPopup text 삭제시 이벤트
                control.ColumnEdit.EditValueChanged += (s, e) =>
                {
                    if (Format.GetFullTrimString((s as DevExpress.XtraEditors.ButtonEdit).EditValue).Equals(string.Empty))
                    {
                        switch (control.FieldName)
                        {
                            case "EQUIPMENTNAME":
                                grdMain.View.SetRowCellValue(grdMain.View.GetFocusedDataSourceRowIndex(), "EQUIPMENTID", string.Empty);
                                break;

                            case "PROCESSSEGMENTCLASSNAME":
                                grdMain.View.SetRowCellValue(grdMain.View.GetFocusedDataSourceRowIndex(), "PROCESSSEGMENTCLASSID", string.Empty);
                                break;

                            case "COSTCENTERCODE":
                                grdMain.View.SetRowCellValue(grdMain.View.GetFocusedDataSourceRowIndex(), "COSTCENTERNAME", string.Empty);
                                break;

                            case "TRANSACTIONREASONCODE":
                                grdMain.View.SetRowCellValue(grdMain.View.GetFocusedDataSourceRowIndex(), "TRANSACTIONREASONCODENAME", string.Empty);
                                break;
                        }
                    }
                };
            });

            popupOspAreaid.EditValueChanged += popupOspAreaid_EditValueChanged;
            btnClear.Click += BtnClear_Click;
            btnSave.Click += BtnSave_Click;
        }

        /// <summary>
        /// Areaid 제한 로직 추가
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void popupOspAreaid_EditValueChanged(object sender, EventArgs e)
        {
            if (popupOspAreaid.EditValue.ToString().Equals(""))
            {
                txtwarehouseid.Text = "";
                txtwarehousename.Text = "";
                return;
            }

            if (grdMain.View.RowCount == 0)
            {
                return;
            }

            DataTable dtview = grdMain.DataSource as DataTable;
            DataRow[] drviw = dtview.Select("FROMAREAID <> '" + popupOspAreaid.GetValue().ToString() + "' AND CONSUMABLEDEFID <> '' ");

            if (drviw.Length > 0)
            {
                popupOspAreaid.EditValueChanged -= popupOspAreaid_EditValueChanged;
                popupOspAreaid.SetValue(drviw[0]["FROMAREAID"].ToString());
                popupOspAreaid.Text = drviw[0]["FROMAREANAME"].ToString();
                popupOspAreaid.EditValue = drviw[0]["FROMAREANAME"].ToString();
                popupOspAreaid.SelectedText = drviw[0]["FROMAREANAME"].ToString();
                this.ShowMessage(MessageBoxButtons.OK, "InValidCsmData010"); //메세지

                popupOspAreaid.EditValueChanged += popupOspAreaid_EditValueChanged;

                return;
            }

            for (int i = 0; i < grdMain.View.DataRowCount; i++)
            {
                grdMain.View.SetRowCellValue(i, "FROMWAREHOUSEID", txtwarehouseid.Text.ToString());
                grdMain.View.SetRowCellValue(i, "FROMWAREHOUSENAME", txtwarehousename.Text.ToString());
                grdMain.View.SetRowCellValue(i, "FROMAREAID", popupOspAreaid.GetValue().ToString());
                grdMain.View.SetRowCellValue(i, "FROMAREANAME", popupOspAreaid.Text.ToString());
            }
        }

        /// <summary>
        /// 저장 -자재기타입고(header)내역을 저장
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!blSave)
            {
                this.ShowMessage(MessageBoxButtons.OK, "InValidCsmData011"); //메세지
                return;
            }

            if (!blSavePeriodid)
            {
                this.ShowMessage(MessageBoxButtons.OK, "InValidCsmData009"); //메세지
                return;
            }

            if (popupOspAreaid.GetValue().ToString().Equals(""))
            {
                this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblAreaid.Text); //메세지
                popupOspAreaid.Focus();
                return;
            }

            //입고일시
            if (dptTransactiondate.Text.ToString().Equals(""))
            {
                this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblTransactiondate.Text); //메세지
                dptTransactiondate.Focus();
                return;
            }

            //lot no (*) 체크 처리 예정.
            SetNullReplaceCombination();

            //자재불출 목록 리스트 체크
            grdMain.View.FocusedRowHandle = grdMain.View.FocusedRowHandle;
            grdMain.View.FocusedColumn = grdMain.View.Columns["CONSUMABLEDEFNAME"];
            grdMain.View.ShowEditor();
            DataTable changed = grdMain.GetChangedRows();

            if (changed.Rows.Count.Equals(0))
            {
                // 저장할 데이터가 존재하지 않습니다.
                this.ShowMessage(MessageBoxButtons.OK, "NoSaveData"); //메세지
                return;
            }

            for (int i = 0; i < grdMain.View.DataRowCount; i++)
            {
                DataRow row = grdMain.View.GetDataRow(i);

                string strAreaid = grdMain.View.GetRowCellValue(i, "FROMAREAID").ToString();
                if (!(popupOspAreaid.GetValue().ToString().Equals(strAreaid)))
                {
                    row["FROMAREAID"] = popupOspAreaid.GetValue().ToString();
                }

                string strConsumabledefid = grdMain.View.GetRowCellValue(i, "CONSUMABLEDEFID").ToString();
                if (strConsumabledefid.Equals(""))
                {
                    string lblConsumabledefid = grdMain.View.Columns["CONSUMABLEDEFID"].Caption.ToString();
                    this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblConsumabledefid); //메세지
                    return;
                }

                string strConsumabledefversion = grdMain.View.GetRowCellValue(i, "CONSUMABLEDEFVERSION").ToString();
                if (strConsumabledefversion.Equals(""))
                {
                    string lblConsumabledefid = grdMain.View.Columns["CONSUMABLEDEFVERSION"].Caption.ToString();
                    this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblConsumabledefid); //메세지
                    return;
                }

                string strQty = grdMain.View.GetRowCellValue(i, "QTY").ToString();
                double dblQty = (strQty.ToString().Equals("") ? 0 : Convert.ToDouble(strQty));
                string strStockQty = grdMain.View.GetRowCellValue(i, "AVAILABLEQTY").ToString();
                double dblStockQty = (strStockQty.ToString().Equals("") ? 0 : Convert.ToDouble(strStockQty));

                if (dblQty.Equals(0))
                {
                    string lblQty = grdMain.View.Columns["QTY"].Caption.ToString();
                    this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblQty); //메세지
                    return;
                }

                if (dblQty < 0)
                {
                    string lblQty = grdMain.View.Columns["QTY"].Caption.ToString();
                    this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblQty); //메세지
                    return;
                }

                if (dblQty > dblStockQty)
                {
                    string lblQty = grdMain.View.Columns["QTY"].Caption.ToString();
                    this.ShowMessage(MessageBoxButtons.OK, "InValidCsmData004", strConsumabledefid); //다국어 메세지 추가
                    return;
                }

                string strTransactionreasoncode = grdMain.View.GetRowCellValue(i, "TRANSACTIONREASONCODE").ToString();
                if (strTransactionreasoncode.Equals(""))
                {
                    string lblTransactionreasoncode = grdMain.View.Columns["TRANSACTIONREASONCODE"].Caption.ToString();
                    this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblTransactionreasoncode); //메세지
                    return;
                }

                string strCostcentercode = grdMain.View.GetRowCellValue(i, "COSTCENTERCODE").ToString();
                if (strCostcentercode.Equals(""))
                {
                    string lblTransactionreasoncode = grdMain.View.Columns["COSTCENTERCODE"].Caption.ToString();
                    this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblTransactionreasoncode); //메세지
                    return;
                }

                if (grdMain.View.GetRowCellValue(i, "EQUIPMENTID").ToString().Equals(""))
                {
                    ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", grdMain.View.Columns["EQUIPMENTNAME"].Caption); //메세지
                    return;
                }

                if (grdMain.View.GetRowCellValue(i, "PROCESSSEGMENTCLASSID").ToString().Equals(""))
                {
                    ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", grdMain.View.Columns["PROCESSSEGMENTCLASSID"].Caption); //메세지
                    return;
                }
            }

            if (!CheckPriceDateKeyColumns())
            {
                //다국어 교체 처리
                string lblConsumabledefid = grdMain.View.Columns["CONSUMABLEDEFID"].Caption.ToString();
                this.ShowMessage(MessageBoxButtons.OK, "InValidOspData007", lblConsumabledefid);
                return;
            }

            if (ShowMessage(MessageBoxButtons.YesNo, "OspDoProcessWant", btnSave.Text).Equals(DialogResult.Yes)) // 저장하시겠습니까?
            {
                try
                {
                    this.ShowWaitArea();

                    btnSave.Enabled = false;
                    btnClear.Enabled = false;

                    DataTable dt = createSaveHeaderDatatable();

                    if (_strDateChangeAuth == "false")
                    {
                        DateTime dateNow = DateTime.Now;
                        dptTransactiondate.EditValue = dateNow.ToString("yyyy-MM-dd HH:mm:ss");//입고시간
                    }

                    DataRow dr = dt.NewRow();
                    dr["ENTERPRISEID"] = UserInfo.Current.Enterprise.ToString();
                    dr["PLANTID"] = cboPlantid.EditValue.ToString();
                    dr["AREAID"] = popupOspAreaid.GetValue().ToString();
                    dr["DESCRIPTION"] = txtDescription.Text.ToString();
                    dr["TRANSACTIONNO"] = btnTransactionno.Text.ToString();
                    DateTime dtTrans = Convert.ToDateTime(dptTransactiondate.EditValue.ToString());

                    if (btnTransactionno.Text.ToString().Equals(""))
                    {
                        dr["TRANSACTIONDATE"] = dtTrans.ToString("yyyy-MM-dd HH:mm:ss");
                        dr["TRANSACTIONTYPE"] = "EtcOutbound";
                        dr["USERID"] = UserInfo.Current.Id.ToString();
                        dr["DEPARTMENT"] = UserInfo.Current.Department.ToString();
                        dr["_STATE_"] = "added";
                    }
                    else
                    {
                        dr["_STATE_"] = "modified";
                    }

                    dt.Rows.Add(dr);

                    DataSet ds = new DataSet();
                    ds.Tables.Add(dt);

                    DataTable dtSave = (grdMain.GetChangedRows() as DataTable).Clone();
                    dtSave.TableName = "listDetail";
                    dtSave.Columns["CONSUMABLEDEFID"].DataType = typeof(string);
                    dtSave.Columns["CONSUMABLELOTID"].DataType = typeof(string);
                    dtSave.Columns["QTY"].DataType = typeof(double);
                    DataTable dtSavechang = grdMain.GetChangedRows();

                    for (int irow = 0; irow < dtSavechang.Rows.Count; irow++)
                    {
                        dr = dtSavechang.Rows[irow];
                        dr["TRANSACTIONDATE"] = dtTrans.ToString("yyyy-MM-dd HH:mm:ss");
                        dr["FROMWAREHOUSEID"] = txtwarehouseid.EditValue.ToString();
                        dtSave.ImportRow(dr);
                    }

                    dtSave.DefaultView.Sort = "_STATE_ desc ";

                    ds.Tables.Add(dtSave);

                    DataTable saveResult = ExecuteRule<DataTable>("MaterialOtherGoodsIssue", ds);
                    DataRow resultData = saveResult.Rows[0];
                    string strtempRequestno = resultData.ItemArray[0].ToString();

                    btnTransactionno.EditValue = strtempRequestno;
                    controlReadOnlyProcess(true);
                    dptTransactiondate.Enabled = false;

                    string stransdate = dtTrans.ToString("yyyy-MM-dd HH:mm:ss");
                    OnSaveConfrimSearch("Save", cboPlantid.EditValue.ToString(), btnTransactionno.EditValue.ToString(), stransdate);
                    ShowMessage("SuccessOspProcess");
                    blSave = false;
                }
                catch (Exception ex)
                {
                    this.ShowError(ex);
                }
                finally
                {
                    this.CloseWaitArea();

                    btnSave.Enabled = true;
                    btnClear.Enabled = true;
                }
            }
        }

        /// <summary>
        /// 초기화 - 초기화 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClear_Click(object sender, EventArgs e)
        {
            btnTransactionno.Text = "";//입고번호

            if (_strDateChangeAuth == "true")
            {
                DateTime dateNow = DateTime.Now;
                dptTransactiondate.EditValue = dateNow.ToString("yyyy-MM-dd HH:mm:ss");//입고시간
                dptTransactiondate.Enabled = true;
            }
            else
            {
                DateTime dateNow = DateTime.Now;
                dptTransactiondate.EditValue = dateNow.ToString("yyyy-MM-dd HH:mm:ss");//입고시간
                dptTransactiondate.Enabled = false;
            }

            txtUserName.Text = UserInfo.Current.Name.ToString();//입고자
            txtDescription.Text = "";//비고
            DataTable dtMat = (grdMain.DataSource as DataTable).Clone();//그리드 초기화
            grdMain.DataSource = dtMat;
            popupOspAreaid.SetValue("");
            popupOspAreaid.Text = "";
            popupOspAreaid.EditValue = "";
            txtwarehouseid.Text = "";
            txtwarehousename.Text = "";
            controlReadOnlyProcess(false);
            blSave = true;
            blSavePeriodid = true;
        }

        #endregion Event

        #region 툴바

        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            SmartButton btn = sender as SmartButton;

            if (btn.Name.ToString().Equals("Save"))
            {
                BtnSave_Click(null, null);
            }

            if (btn.Name.ToString().Equals("Initialization"))
            {
                BtnClear_Click(null, null);
            }
        }

        #endregion 툴바

        #region Private Function

        /// <summary>
        /// 복사시 기본테이블 생성
        /// </summary>
        /// <returns></returns>
        private DataTable createSaveHeaderDatatable()
        {
            DataTable dt = new DataTable()
            {
                TableName = "listMain"
            };

            dt.Columns.Add("ENTERPRISEID");
            dt.Columns.Add("PLANTID");
            dt.Columns.Add("AREAID");
            dt.Columns.Add("TRANSACTIONNO");
            dt.Columns.Add("TRANSACTIONDATE");
            dt.Columns.Add("TRANSACTIONTYPE");
            dt.Columns.Add("USERID");
            dt.Columns.Add("DEPARTMENT");
            dt.Columns.Add("DESCRIPTION");
            dt.Columns.Add("_STATE_");

            return dt;
        }

        /// <summary>
        /// 자재기타 입고 (header) 입력 항목 lock 처리
        /// </summary>
        /// <param name="blProcess"></param>
        private void controlReadOnlyProcess(bool blProcess)
        {
            popupOspAreaid.Enabled = blProcess ? false : true;
            cboPlantid.ReadOnly = blProcess;
            popupOspAreaid.ReadOnly = blProcess;
        }

        /// <summary>
        /// 자재기타입고내역 (LINE )LOTNO  값 대체(*) 처리
        /// </summary>
        /// <param name="strRequestno"></param>
        private void SetNullReplaceCombination()
        {
            if (grdMain.View.DataRowCount.Equals(0))
            {
                return;
            }

            for (int irow = 0; irow < grdMain.View.DataRowCount; irow++)
            {
                DataRow dr = grdMain.View.GetDataRow(irow);

                string sproductdefid = dr["CONSUMABLELOTID"].ToString();
                string strAreaid = dr["FROMAREAID"].ToString();
                string strPlantid = dr["PLANTID"].ToString();

                if (sproductdefid.Equals(""))
                {
                    dr["CONSUMABLELOTID"] = "*";
                }

                if (!(strAreaid.Equals(popupOspAreaid.GetValue().ToString())))
                {
                    dr["FROMAREAID"] = popupOspAreaid.GetValue().ToString();
                }

                if (!(strPlantid.Equals(cboPlantid.EditValue.ToString())))
                {
                    dr["PLANTID"] = cboPlantid.EditValue.ToString();
                }
            }
        }

        /// <summary>
        /// 재 기타 출고 – Popup
        /// </summary>
        private void PopupShow()
        {
            string sAreaid = "";
            string sAreaName = "";
            string sPlantid = "";

            if (!(popupOspAreaid.GetValue().ToString().Equals("")))
            {
                sAreaid = popupOspAreaid.GetValue().ToString();
                sAreaName = popupOspAreaid.Text.ToString();
            }

            if (cboPlantid.EditValue != null)
            {
                sPlantid = cboPlantid.EditValue.ToString();
            }

            MaterialOtherGoodsIssuePopup ReceiptPopup = new MaterialOtherGoodsIssuePopup(sPlantid, sAreaid, sAreaName);
            ReceiptPopup.WriteHandler += ReceiptPopup_write_handler;

            if (ReceiptPopup.CurrentDataRow != null)
            {
                if (ReceiptPopup.CurrentDataRow.Table.Rows.Count == 1)
                {
                    ReceiptPopup.Close();
                }
                else
                {
                    ReceiptPopup.ShowDialog(this);
                }
            }
            else
            {
                ReceiptPopup.ShowDialog(this);
            }
        }

        /// <summary>
        /// 자재 기타 입고 내역 – 결과값 가져오기
        /// </summary>
        /// <param name="row"></param>
        private void ReceiptPopup_write_handler(DataRow row)
        {
            if (row != null)
            {
                popupOspAreaid.EditValueChanged -= popupOspAreaid_EditValueChanged;
                OnSaveConfrimSearch("Search", row["PLANTID"].ToString(), row["TRANSACTIONNOISSUE"].ToString(), row["TRANSACTIONDATEISSUE"].ToString());
                OnPlantidInformationSearch(row["PLANTID"].ToString());
                blSave = false;
                popupOspAreaid.EditValueChanged += popupOspAreaid_EditValueChanged;
            }
        }

        /// <summary>
        /// 입고번호 재조회시 사용
        /// </summary>
        /// <param name="workgubun"></param>
        /// <param name="sPlantid"></param>
        /// <param name="sTransactionDate"></param>
        private void OnSaveConfrimSearch(string workgubun, string sPlantid, string sTransActionNo, string sTransactionDate)
        {
            DateTime dttransdate = Convert.ToDateTime(sTransactionDate);
            string strStartdate = dttransdate.ToString("yyyy-MM-01 " + "00:00:00");
            string strmonth = dttransdate.ToString("yyyy-MM");
            string strEnddate = dttransdate.ToString("yyyy-MM-dd HH:mm:ss");

            grdMain.View.SetFocusedRowCellValue("YEARMONTH", strmonth);
            grdMain.View.SetFocusedRowCellValue("STARTDATE", strStartdate);
            grdMain.View.SetFocusedRowCellValue("ENDDATE", strEnddate);

            Dictionary<string, object> Param = new Dictionary<string, object>
            {
                { "P_STARTDATE", strStartdate },
                { "P_YEARMONTH", strmonth },
                { "P_ENTERPRISEID", UserInfo.Current.Enterprise.ToString() },
                { "P_PLANTID", sPlantid },
                { "P_TRANSACTIONNO", sTransActionNo },
                { "P_TRANSACTIONTYPE", "EtcOutbound" },
                { "P_TRANSACTIONCODE", "MISC_ISSUE" },
                { "USERID", UserInfo.Current.Id.ToString() },
                { "LANGUAGETYPE", UserInfo.Current.LanguageType }
            };

            Param = Commons.CommonFunction.ConvertParameter(Param);
            DataTable dtReceipt = SqlExecuter.Query("GetMaterialOtherGoodsIssueSearch", "10001", Param);
            grdMain.DataSource = dtReceipt;

            #region 재고실사및 재고마감여부 확인

            string strAreaid = dtReceipt.Rows[0]["FROMAREAID"].ToString();
            string strcheckdate = dtReceipt.Rows[0]["TRANSACTIONDATE"].ToString();
            DateTime dtcheckdate = Convert.ToDateTime(strcheckdate);
            strcheckdate = strEnddate = dtcheckdate.ToString("yyyy-MM-dd HH:mm:ss");

            Dictionary<string, object> Paramcheck = new Dictionary<string, object>
            {
                { "P_ENTERPRISEID", UserInfo.Current.Enterprise.ToString() },
                { "P_PLANTID", sPlantid },
                { "P_AREAID", strAreaid },
                { "P_CHECKDATE", strcheckdate }
            };
            Paramcheck = Commons.CommonFunction.ConvertParameter(Paramcheck);
            DataTable dtcheck = SqlExecuter.Query("GetCheckPeriodCloseCsm", "10001", Paramcheck);

            int intcheck = int.Parse(dtcheck.Rows[0]["CNTPERIODID"].ToString());

            if (intcheck > 0)
            {
                blSavePeriodid = false;
            }

            #endregion 재고실사및 재고마감여부 확인

            //입고내역 header 정보
            if (workgubun.Equals("Search"))
            {
                //화면 lock 처리
                controlReadOnlyProcess(true);
                dptTransactiondate.Enabled = false;

                if (dtReceipt.Rows.Count > 0)
                {
                    DataRow drhead = dtReceipt.Rows[0];
                    cboPlantid.EditValue = drhead["PLANTID"].ToString();
                    btnTransactionno.EditValue = drhead["TRANSACTIONNO"].ToString();
                    popupOspAreaid.SetValue(drhead["FROMAREAID"].ToString());
                    popupOspAreaid.Text = drhead["FROMAREANAME"].ToString();
                    popupOspAreaid.EditValue = drhead["FROMAREANAME"].ToString();
                    txtwarehouseid.Text = drhead["FROMWAREHOUSEID"].ToString();
                    txtwarehousename.Text = drhead["FROMWAREHOUSENAME"].ToString();
                    DateTime dtdate = Convert.ToDateTime(drhead["TRANSACTIONDATE"].ToString());
                    dptTransactiondate.EditValue = dtdate;
                    txtUserName.EditValue = drhead["USERNAME"].ToString();
                }
            }
        }

        /// <summary>
        /// plant정보에서 WORKTIME
        /// </summary>
        private void OnPlantidInformationSearch(string sPlantid)
        {
            DataTable dtPlantInfo = SqlExecuter.Query("GetPlantidInformatByCsm", "10001", new Dictionary<string, object> { { "P_PLANTID", sPlantid } });

            _sWorkTime = dtPlantInfo.Rows.Count.Equals(1) ? dtPlantInfo.Rows[0].ToString() : _sWorkTime;
        }

        /// <summary>
        /// 자재기준  key 중복 체크
        /// </summary>
        /// <returns></returns>
        private bool CheckPriceDateKeyColumns()
        {
            bool blcheck = true;

            if (grdMain.View.DataRowCount.Equals(0))
            {
                return blcheck;
            }

            for (int irow = 0; irow < grdMain.View.DataRowCount; irow++)
            {
                DataRow row = grdMain.View.GetDataRow(irow);

                if (row.RowState.Equals(DataRowState.Added) || row.RowState.Equals(DataRowState.Modified))
                {
                    string strConsumabledefid = row["CONSUMABLEDEFID"].ToString();
                    string strConsumabledefversion = row["CONSUMABLEDEFVERSION"].ToString();
                    string strConsumablelotid = row["CONSUMABLELOTID"].ToString();

                    if (SearchPriceDateKey(strConsumabledefid, strConsumabledefversion, strConsumablelotid, irow) < 0)
                    {
                        blcheck = true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return blcheck;
        }

        /// <summary>
        /// 자재 중복 체크 함수
        /// </summary>
        /// <param name="dateStartdate"></param>
        /// <param name="dateEnddate"></param>
        /// <param name="icurRow"></param>
        /// <returns></returns>
        private int SearchPriceDateKey(string strConsumabledefid, string strConsumabledefversion, string strConsumablelotid, int icurRow)
        {
            int iresultRow = -1;

            for (int irow = 0; irow < grdMain.View.DataRowCount; irow++)
            {
                if (icurRow != irow)
                {
                    DataRow row = grdMain.View.GetDataRow(irow);

                    // 행 삭제만 제외
                    if (grdMain.View.IsDeletedRow(row) == false)
                    {
                        string strTempConsumabledefid = row["CONSUMABLEDEFID"].ToString();
                        string strTempConsumabledefversion = row["CONSUMABLEDEFVERSION"].ToString();
                        string strTempConsumablelotid = row["CONSUMABLELOTID"].ToString();
                        if (strTempConsumabledefid.Equals(strConsumabledefid) && strTempConsumabledefversion.Equals(strConsumabledefversion) && strTempConsumablelotid.Equals(strConsumablelotid))
                        {
                            return irow;
                        }
                    }
                }
            }

            return iresultRow;
        }

        #endregion Private Function
    }
}