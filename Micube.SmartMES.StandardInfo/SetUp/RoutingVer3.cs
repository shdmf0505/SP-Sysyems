#region using

using DevExpress.XtraTreeList.Nodes;
using Evaluator;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons.Controls;
using Micube.SmartMES.StandardInfo.Popup;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion using

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프로그램명 : 기준정보 > Setup > 표준공정등록
    /// 업 무 설명 : 표준공정 등록및 조회
    /// 생  성  자 : 윤성원
    /// 생  성  일 : 2019-06-27
    /// 수정 이 력 :
    /// </summary>
    public partial class RoutingVer3 : SmartConditionManualBaseForm
    {
        #region Local Variables

        private List<int> focusedNodeIndexList;
        private int lastFocusedOperationRowIndex = 0;
        private DataTable dtProductDEF;

        #endregion Local Variables

        #region 생성자

        public RoutingVer3()
        {
            InitializeComponent();
        }

        #endregion 생성자

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 설정 초기화
        /// </summary>
        protected override void InitializeContent()
        {
            BOMCalculatorList.CreateCalculators();

            InitializeGridList();
            InitializeEvent();
            InitializeControl();

            //         ConditionItemSelectPopup WarehouseCode = new ConditionItemSelectPopup();
            //         WarehouseCode.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            //         WarehouseCode.SetPopupLayout("WAREHOUSEID", PopupButtonStyles.Ok_Cancel);
            //         WarehouseCode.Id = "WAREHOUSEID";
            //         WarehouseCode.LabelText = "WAREHOUSEID";
            //         WarehouseCode.SearchQuery = new SqlQuery("GetWarehouseList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}");
            //         WarehouseCode.IsMultiGrid = false;
            //         WarehouseCode.DisplayFieldName = "COMPLETEWAREHOUSENAME";
            //         WarehouseCode.ValueFieldName = "COMPLETEWAREHOUSEID";
            //         WarehouseCode.LanguageKey = "WAREHOUSEID";
            //         WarehouseCode.Conditions.AddTextBox("TXTWAREHOUSE");
            //WarehouseCode.SetPopupAutoFillColumns("COMPLETEWAREHOUSENAME");
            //         WarehouseCode.GridColumns.AddTextBoxColumn("COMPLETEWAREHOUSEID", 100);
            //         WarehouseCode.GridColumns.AddTextBoxColumn("COMPLETEWAREHOUSENAME", 200);
            //         spCompletionWareHouse.SelectPopupCondition = WarehouseCode;

            scRTRSheet.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            scRTRSheet.ValueMember = "CODEID";
            scRTRSheet.DisplayMember = "CODENAME";

            scRTRSheet.DataSource = SqlExecuter.Query("GetCodeList", "00001"
                    , new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "CODECLASSID", "RTRSHT" } });
            scRTRSheet.UseEmptyItem = true;
            scRTRSheet.ShowHeader = false;

            scUseLayer.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            scUseLayer.ValueMember = "CODEID";
            scUseLayer.DisplayMember = "CODENAME";
            scUseLayer.UseEmptyItem = true;
            scUseLayer.ShowHeader = false;
            scUseLayer.DataSource = SqlExecuter.Query("GetCodeList", "00001"
                    , new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "CODECLASSID", "UserLayer" } });

            smartTabControl1.TabPages[3].PageVisible = false;
        }

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGridList()
        {
            InitializeGridOperation();
            InitializeGridConsumable();
            InitializeGridProcessSpec();
            InitializeGridDurable();
            InitializeGridProcessAttributeValue();
            InitializeGridAOIQCLayer();
            xtpAOILayer.PageVisible = false;
            smartTabControl1.SetLanguageKey(xtpAOILayer, "AOIVRSLAYER");
        }

        /// <summary>
        /// Area Tree 초기화
        /// </summary>
        private void InitializeTree()
        {
            treeRouting.DataSource = null;

            treeRouting.SetResultCount(1);
            treeRouting.SetIsReadOnly();
            //treeRouting.SetMember("DISPLAYNAME", "BOMID", "PARENTBOMID");
            treeRouting.SetMember("DISPLAYNAME", "BOMSEQUENCE", "PARENTBOMSEQUENCE");

            treeRouting.SetSortColumn("USERSEQUENCE");

            var values = Conditions.GetValues();
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);

            DataTable dt = SqlExecuter.Query("GetBOMTree", "10001", values);

            if (dt != null)
            {
                treeRouting.DataSource = dt;
                treeRouting.PopulateColumns();

                treeRouting.ExpandToLevel(0);

                if (focusedNodeIndexList != null && treeRouting.Nodes.Count > 0)
                {
                    TreeListNode focusedNode = treeRouting.Nodes[0];

                    for (int i = 1; i < focusedNodeIndexList.Count; i++)
                    {
                        if (focusedNode.Nodes.Count >= focusedNodeIndexList[i])
                        {
                            focusedNode = focusedNode.Nodes[focusedNodeIndexList[i]];
                        }
                    }

                    treeRouting.FocusedNode = focusedNode;
                }
            }
        }

        /// <summary>
        /// 공정 라우팅 그리드
        /// </summary>
        private void InitializeGridOperation()
        {
            // 공정
            grdOperation.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export | GridButtonItem.Import;
            grdOperation.View.AddComboBoxColumn("PLANTID", 60, new SqlQuery("GetPlantList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PLANTNAME", "PLANTID")
                .SetValidationIsRequired()
                .SetDefault(UserInfo.Current.Plant)
                .SetTextAlignment(TextAlignment.Center);

            grdOperation.View.AddTextBoxColumn("USERSEQUENCE", 60).SetValidationIsRequired().SetTextAlignment(TextAlignment.Center);

            #region 공정

            var condition = grdOperation.View.AddSelectPopupColumn("PROCESSSEGMENTID", 100, new SqlQuery("GetProcessSegmentExtPupop", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"VALIDSTATE={"Valid"}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                        .SetPopupLayout("SELECTPROCESSSEGMENT", PopupButtonStyles.Ok_Cancel, true, false)
                                        .SetValidationIsRequired()
                                        .SetPopupResultCount(0)
                                        .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow)
                                        .SetTextAlignment(TextAlignment.Center)
                                        .SetPopupApplySelection((selectRows, gridRow) =>
                                        {
                                            if (selectRows.Count() > 0)
                                            {
                                                DataTable dt2 = grdOperation.DataSource as DataTable;
                                                int handle = grdOperation.View.FocusedRowHandle;

                                                foreach (DataRow row in selectRows)
                                                {
                                                    DataRow dr = null;
                                                    if (dt2.Rows.Count < handle + 1)
                                                    {
                                                        dr = dt2.NewRow();
                                                        dt2.Rows.Add(dr);

                                                        string productDefId = dtProductDEF.Rows[0]["PRODUCTDEFID"].ToString();
                                                        string productDefVersion = dtProductDEF.Rows[0]["PRODUCTDEFVERSION"].ToString();

                                                        GetNumber number = new GetNumber();
                                                        string operationId = number.GetStdNumber("Operation", productDefId + productDefVersion);
                                                        dr["OPERATIONID"] = operationId;

                                                        DataTable dtProcessPath = grdOperation.DataSource as DataTable;

                                                        if (dtProcessPath.AsEnumerable().Where(s => !string.IsNullOrEmpty(s["USERSEQUENCE"].ToString())).Count() == 0)
                                                        {
                                                            dr["USERSEQUENCE"] = "10";
                                                        }
                                                        else
                                                        {
                                                            int userSequence = dtProcessPath.AsEnumerable().Where(s => !string.IsNullOrEmpty(s["USERSEQUENCE"].ToString())).Max(s => int.Parse(s["USERSEQUENCE"].ToString()));

                                                            dr["USERSEQUENCE"] = (((userSequence / 10) + 1) * 10).ToString();
                                                        }
                                                    }
                                                    else
                                                    {
                                                        dr = grdOperation.View.GetDataRow(handle);
                                                    }

                                                    dr["PROCESSSEGMENTID"] = row["PROCESSSEGMENTID"];
                                                    dr["PROCESSSEGMENTNAME"] = row["PROCESSSEGMENTNAME"];
                                                    dr["PROCESSSEGMENTCLASSID"] = row["PROCESSSEGMENTCLASSID"];

                                                    dr["PLANTID"] = UserInfo.Current.Plant;

                                                    if (treeRouting.FocusedNode == null)
                                                    {
                                                        dr["PROCESSUOM"] = "PNL";
                                                    }
                                                    else
                                                    {
                                                        if (UserInfo.Current.Enterprise == "INTERFLEX")
                                                        {
                                                            dr["PROCESSUOM"] = "PCS";
                                                        }
                                                        else if (UserInfo.Current.Enterprise == "YOUNGPOONG")
                                                        {
                                                            dr["PROCESSUOM"] = "PNL";
                                                        }
                                                    }

                                                    grdOperation.View.RaiseValidateRow(handle);
                                                    handle++;
                                                }
                                            }
                                        });

            // 팝업에서 사용할 조회조건 항목 추가
            condition.Conditions.AddTextBox("PROCESSSEGMENTID");
            condition.Conditions.AddTextBox("PROCESSSEGMENTNAME");

            // 팝업 그리드 설정
            condition.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150);
            condition.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
            condition.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 200);

            #endregion 공정

            grdOperation.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150).SetIsReadOnly();

            ConditionItemComboBox uomCombo = grdOperation.View.AddComboBoxColumn("PROCESSUOM", 80, new SqlQuery("GetUomDefinitionList", "10001", $"UOMCLASSID={"Process"}"), "UOMDEFID", "UOMDEFNAME")
                 .SetTextAlignment(TextAlignment.Center).SetDefault("PNL");

            grdOperation.View.AddTextBoxColumn("DESCRIPTION", 300).SetLabel("COMMENT");
            //공정변경여부
            grdOperation.View.AddComboBoxColumn("ISSEGMENTCHANGE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetTextAlignment(TextAlignment.Center);
            //자재필수여부
            grdOperation.View.AddTextBoxColumn("ISREQUIREDMATERIAL", 80).SetLabel("ISREQUIREDMATERIAL2").SetTextAlignment(TextAlignment.Center).SetIsHidden();
            //공정SPEC필수여부
            grdOperation.View.AddTextBoxColumn("ISREQUIREDOPERATIONSPEC", 80).SetTextAlignment(TextAlignment.Center).SetIsHidden();
            //TOOL필수여부
            grdOperation.View.AddTextBoxColumn("ISREQUIREDTOOL", 80).SetTextAlignment(TextAlignment.Center).SetIsHidden();
            //주차관리여부
            grdOperation.View.AddComboBoxColumn("ISWEEKMNG", 50, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetTextAlignment(TextAlignment.Center).SetDefault("");

            grdOperation.View.AddTextBoxColumn("CREATOR", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdOperation.View.AddTextBoxColumn("CREATEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdOperation.View.AddTextBoxColumn("MODIFIER", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdOperation.View.AddTextBoxColumn("MODIFIEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly().SetTextAlignment(TextAlignment.Center);

            grdOperation.View.AddTextBoxColumn("OPERATIONID", 100).SetIsHidden();
            grdOperation.View.AddTextBoxColumn("OPERATIONSEQUENCE", 100).SetIsHidden();
            grdOperation.View.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 100).SetIsHidden();
            grdOperation.View.AddTextBoxColumn("VALIDSTATE", 100).SetIsHidden();

            grdOperation.View.PopulateColumns();
        }

        /// <summary>
        /// 공정 스펙 그리드
        /// </summary>
        private void InitializeGridProcessSpec()
        {
            grdProcessSpec.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export | GridButtonItem.Import;

            #region 대분류

            var inpectionNameGroup = grdProcessSpec.View.AddGroupColumn("");

            var condition = inpectionNameGroup.AddSelectPopupColumn("INSPITEMNAME", 200, new SqlQuery("GetSpecAttributelist", "10001"
                                                                                                     , $"ENTERPRISEID={UserInfo.Current.Enterprise}"
                                                                                                     , $"LANGUAGETYPE={UserInfo.Current.LanguageType}"
                                                                                                     , $"INSPECTIONCLASSID={"OperationInspection"}"), "INSPITEMNAME")
                                              .SetLabel("INSPECTIONIDNAME")
                                              .SetPopupLayout("INSPITEMID", PopupButtonStyles.Ok_Cancel, true, false)
                                              .SetPopupResultCount(0)
                                              .SetPopupResultMapping("INSPITEMNAME", "INSPITEMNAME")
                                              .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow)
                                              .SetPopupApplySelection((selectRows, gridRow) =>
                                              {
                                                  if (selectRows.Count() > 0)
                                                  {
                                                      DataTable dt2 = grdProcessSpec.DataSource as DataTable;
                                                      int handle = grdProcessSpec.View.FocusedRowHandle;
                                                      foreach (DataRow row in selectRows)
                                                      {
                                                          DataRow dr = null;
                                                          if (dt2.Rows.Count < handle + 1)
                                                          {
                                                              dr = dt2.NewRow();
                                                              dt2.Rows.Add(dr);
                                                          }
                                                          else
                                                          {
                                                              dr = grdProcessSpec.View.GetDataRow(handle);
                                                          }

                                                          dr["INSPITEMID"] = row["INSPITEMID"].ToString();
                                                          dr["INSPITEMNAME"] = row["INSPITEMNAME"].ToString();
                                                          dr["INSPITEMVERSION"] = row["INSPITEMVERSION"].ToString();
                                                          dr["PROCESSSEGMENTID"] = grdOperation.View.GetFocusedDataRow()["PROCESSSEGMENTID"].ToString();
                                                          dr["INSPECTIONDEFID"] = "OperationInspection-OperationInspection";
                                                          dr["INSPECTIONDEFVERSION"] = "*";
                                                          dr["SPECCLASSID"] = "OperationSpec";
                                                          dr["DEFAULTCHARTTYPE"] = smcXBarChartInfo.Editor.EditValue.ToString();
                                                          dr["RESOURCETYPE"] = "ProcessSegmentID";

                                                          grdProcessSpec.View.RaiseValidateRow(handle);

                                                          handle++;
                                                      }
                                                  }
                                              });

            condition.Conditions.AddTextBox("TXTINSPITEMNAME");
            condition.Conditions.AddTextBox("PROCESSSEGMENTID").SetIsHidden().SetPopupDefaultByGridColumnId("PROCESSSEGMENTID");

            // 팝업 그리드 설정
            condition.GridColumns.AddTextBoxColumn("INSPITEMID", 150);
            condition.GridColumns.AddTextBoxColumn("INSPITEMNAME", 200);

            #endregion 대분류

            var localtionGroup = grdProcessSpec.View.AddGroupColumn("");
            var specGroup = grdProcessSpec.View.AddGroupColumn("GROUPSPEC");
            var controlLimitGroup = grdProcessSpec.View.AddGroupColumn("GROUPCONTROLLIMIT");
            var outlierGroup = grdProcessSpec.View.AddGroupColumn("GROUPOUTLIER");

            localtionGroup.AddTextBoxColumn("LOCATION", 100);

            specGroup.AddSpinEditColumn("LSL", 100).SetLabel("LSL").SetDisplayFormat("#,##0.####");
            specGroup.AddSpinEditColumn("SL", 100).SetLabel("STDVALUE").SetDisplayFormat("#,##0.####");
            specGroup.AddSpinEditColumn("USL", 100).SetLabel("USL").SetDisplayFormat("#,##0.####");

            controlLimitGroup.AddSpinEditColumn("LCL", 100).SetLabel("LSL").SetDisplayFormat("#,##0.####");
            controlLimitGroup.AddSpinEditColumn("CL", 100).SetLabel("STDVALUE").SetDisplayFormat("#,##0.####");
            controlLimitGroup.AddSpinEditColumn("UCL", 100).SetLabel("USL").SetDisplayFormat("#,##0.####");

            outlierGroup.AddSpinEditColumn("LOL", 100).SetLabel("LSL").SetDisplayFormat("#,##0.####");
            outlierGroup.AddSpinEditColumn("UOL", 100).SetLabel("USL").SetDisplayFormat("#,##0.####");

            grdProcessSpec.View.AddTextBoxColumn("CREATOR", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdProcessSpec.View.AddTextBoxColumn("CREATEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdProcessSpec.View.AddTextBoxColumn("MODIFIER", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdProcessSpec.View.AddTextBoxColumn("MODIFIEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly().SetTextAlignment(TextAlignment.Center);

            grdProcessSpec.View.PopulateColumns();

            Dictionary<string, object> param = new Dictionary<string, object>
            {
                { "CODECLASSID", "ControlType" },
                { "LANGUAGETYPE", UserInfo.Current.LanguageType }
            };

            // UOM
            smcXBarChartInfo.Editor.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            smcXBarChartInfo.Editor.ShowHeader = false;
            smcXBarChartInfo.Editor.ValueMember = "CODEID";
            smcXBarChartInfo.Editor.DisplayMember = "CODENAME";
            smcXBarChartInfo.Editor.UseEmptyItem = false;
            smcXBarChartInfo.Editor.DataSource = SqlExecuter.Query("GetCodeList", "00001", param);
        }

        /// <summary>
        /// 치공구 그리드
        /// </summary>
        private void InitializeGridDurable()
        {
            grdDurable.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export | GridButtonItem.Import;

            //OPERATION ID
            grdDurable.View.AddTextBoxColumn("OPERATIONID").SetIsHidden();
            grdDurable.View.AddTextBoxColumn("PROCESSSEGMENTCLASSID").SetIsHidden();
            grdDurable.View.AddTextBoxColumn("DURABLETYPE").SetIsHidden();
            //품목코드
            grdDurable.View.AddTextBoxColumn("PRODUCTDEFID", 100).SetIsReadOnly().SetIsHidden();
            //품목버전
            grdDurable.View.AddTextBoxColumn("PRODUCTDEFVERSION", 50).SetIsReadOnly().SetIsHidden();
            //품목명
            grdDurable.View.AddTextBoxColumn("PRODUCTDEFNAME", 150).SetIsReadOnly().SetIsHidden();
            //TOOL구분
            grdDurable.View.AddComboBoxColumn("DURABLECLASSID", 80, new SqlQuery("GetTypeList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "CODECLASSID=DurableClass"), "CODENAME", "CODEID")
                .SetTextAlignment(TextAlignment.Center).SetLabel("DURABLECATEGORYCODE").SetIsReadOnly();

            #region 치공구 코드

            var toolPopupColumn = grdDurable.View.AddSelectPopupColumn("TOOLCODE", 150, new SqlQuery("GetDurabledefiniTionPopup", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                            .SetPopupLayout("SELECTDURABLE", PopupButtonStyles.Ok_Cancel, true, false)
                                            .SetPopupResultCount(0)
                                            .SetPopupLayoutForm(1000, 800, FormBorderStyle.SizableToolWindow)
                                            .SetLabel("DURABLEDEFID")
                                            .SetPopupApplySelection((selectRows, dataGridRow) =>
                                            {
                                                if (selectRows.Count() > 0)
                                                {
                                                    DataTable dt2 = grdDurable.DataSource as DataTable;
                                                    int handle = grdDurable.View.FocusedRowHandle;
                                                    foreach (DataRow row in selectRows)
                                                    {
                                                        DataRow dr = null;
                                                        if (dt2.Rows.Count < handle + 1)
                                                        {
                                                            dr = dt2.NewRow();
                                                            dt2.Rows.Add(dr);
                                                        }
                                                        else
                                                        {
                                                            dr = grdDurable.View.GetDataRow(handle);
                                                        }

                                                        dr["PRODUCTDEFID"] = dtProductDEF.Rows[0]["PRODUCTDEFID"].ToString();
                                                        dr["PRODUCTDEFVERSION"] = dtProductDEF.Rows[0]["PRODUCTDEFVERSION"].ToString();
                                                        dr["PRODUCTDEFNAME"] = dtProductDEF.Rows[0]["PRODUCTDEFNAME"].ToString();
                                                        dr["DURABLETYPE"] = row["DURABLECLASSTYPE"];
                                                        dr["TOOLCODE"] = row["TOOLCODE"];
                                                        dr["TOOLNAME"] = row["DURABLEDEFNAME"];
                                                        dr["DURABLECLASSID"] = Format.GetString(row["DURABLECLASSID"]);//TOOL 구분 = DURABLECLASSID
                                                        dr["TOOLTYPE"] = Format.GetString(row["TOOLTYPE"]);//유형1 = TOOLTYPE
                                                        dr["TOOLDETAILTYPE"] = Format.GetString(row["TOOLDETAILTYPE"]);//유형2 = TOOLDETAILTYPE
                                                        dr["FORM"] = Format.GetString(row["FORM"]);//TOOL 형식 = FORM
                                                        dr["TOOLVERSION"] = Format.GetString(row["DURABLEDEFVERSION"]);//치공구 Rev
                                                        dr["SUMMARY"] = row["SUMMARY"];//합수
                                                        dr["FILMUSELAYER1"] = Format.GetString(row["FILMUSELAYER1"]);//사용층
                                                        dr["FILMUSELAYER1NAME"] = Format.GetString(row["FILMUSELAYER1NAME"]);//사용층
                                                        dr["HITCOUNT"] = row["HITCOUNT"];//타수

                                                        grdDurable.View.RaiseValidateRow(handle);
                                                        handle++;
                                                    }
                                                }
                                            });

            toolPopupColumn.Conditions.AddTextBox("ITEMID").SetLabel("TXTPRODUCTDEFNAME").SetPopupDefaultByGridColumnId("PRODUCTDEFID");

            toolPopupColumn.Conditions.AddTextBox("DURABLEDEFID");
            toolPopupColumn.Conditions.AddTextBox("DURABLEDEFNAME");
            toolPopupColumn.Conditions.AddTextBox("OPERATIONID").SetPopupDefaultByGridColumnId("OPERATIONID").SetIsHidden();

            toolPopupColumn.GridColumns.AddTextBoxColumn("DURABLECLASSTYPE", 60);
            toolPopupColumn.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 100);
            toolPopupColumn.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 50).SetTextAlignment(TextAlignment.Center);
            toolPopupColumn.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 150);
            toolPopupColumn.GridColumns.AddComboBoxColumn("DURABLECLASSID", 80, new SqlQuery("GetTypeList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "CODECLASSID=DurableClass"), "CODENAME", "CODEID")
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("DURABLECATEGORYCODE");

            toolPopupColumn.GridColumns.AddTextBoxColumn("TOOLCODE", 100).SetLabel("DURABLEDEFID");
            toolPopupColumn.GridColumns.AddTextBoxColumn("DURABLEDEFVERSION", 50).SetTextAlignment(TextAlignment.Center);
            toolPopupColumn.GridColumns.AddTextBoxColumn("DURABLEDEFNAME", 200);
            toolPopupColumn.GridColumns.AddComboBoxColumn("TOOLTYPE", 60, new SqlQuery("GetToolTypeByClassId", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                .SetTextAlignment(TextAlignment.Center).SetLabel("TOOLCATEGORYDETAIL");

            toolPopupColumn.GridColumns.AddComboBoxColumn("TOOLDETAILTYPE", 60, new SqlQuery("GetDurableClassType2", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                .SetTextAlignment(TextAlignment.Center).SetLabel("TOOLDETAIL");

            toolPopupColumn.GridColumns.AddTextBoxColumn("FORM", 80).SetLabel("TOOLFORMCODE").SetIsReadOnly();
            toolPopupColumn.GridColumns.AddSpinEditColumn("SUMMARY", 70).SetLabel("ARRAY");
            toolPopupColumn.GridColumns.AddTextBoxColumn("FILMUSELAYER1", 70).SetIsHidden();
            toolPopupColumn.GridColumns.AddTextBoxColumn("FILMUSELAYER1NAME", 70).SetLabel("USERLAYER");
            toolPopupColumn.GridColumns.AddSpinEditColumn("HITCOUNT", 70);

            #endregion 치공구 코드

            grdDurable.View.AddTextBoxColumn("TOOLNAME", 250);

            //치공구버전
            grdDurable.View.AddTextBoxColumn("TOOLVERSION", 50).SetTextAlignment(TextAlignment.Center).SetIsReadOnly().SetLabel("DURABLEDEFVERSION");
            //TOOL유형1
            grdDurable.View.AddComboBoxColumn("TOOLTYPE", 60, new SqlQuery("GetToolTypeByClassId", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                .SetTextAlignment(TextAlignment.Center).SetLabel("TOOLCATEGORYDETAIL").SetIsReadOnly();
            //TOOL유형2
            grdDurable.View.AddComboBoxColumn("TOOLDETAILTYPE", 60, new SqlQuery("GetDurableClassType2", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                .SetTextAlignment(TextAlignment.Center).SetLabel("TOOLDETAIL").SetIsReadOnly();
            //TOOL형식
            grdDurable.View.AddTextBoxColumn("FORM", 80)
                .SetLabel("TOOLFORMCODE").SetIsReadOnly();
            //합수
            grdDurable.View.AddSpinEditColumn("SUMMARY", 70).SetLabel("ARRAY").SetIsReadOnly();
            //사용층
            //grdDurable.View.AddComboBoxColumn("FILMUSELAYER1", 70, new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CODECLASSID=FilmUseLayer1"), "CODENAME", "CODEID")
            //	.SetLabel("USERLAYER").SetIsReadOnly();
            grdDurable.View.AddTextBoxColumn("FILMUSELAYER1", 70).SetIsHidden();
            grdDurable.View.AddTextBoxColumn("FILMUSELAYER1NAME", 70).SetLabel("USERLAYER").SetIsReadOnly();

            //타수
            grdDurable.View.AddTextBoxColumn("HITCOUNT", 70).SetIsReadOnly();
            //아대/일반
            grdDurable.View.AddComboBoxColumn("WRAPTYPE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OutsourcingSpecPanelGuide", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID").SetLabel("PANELGUIDENORMAL");

            grdDurable.View.AddTextBoxColumn("DESCRIPTION", 200);
            grdDurable.View.AddTextBoxColumn("CREATOR", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdDurable.View.AddTextBoxColumn("CREATEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdDurable.View.AddTextBoxColumn("MODIFIER", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdDurable.View.AddTextBoxColumn("MODIFIEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly().SetTextAlignment(TextAlignment.Center);

            grdDurable.View.PopulateColumns();

            (grdDurable.View.Columns["TOOLCODE"].ColumnEdit as DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit).ButtonClick += RoutingVer3_ButtonClick;
        }

        /// <summary>
        /// Attribute 그리드
        /// </summary>
        private void InitializeGridProcessAttributeValue()
        {
            grdProcessAttributeValue.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export | GridButtonItem.Import;

            #region 항목

            var control = grdProcessAttributeValue.View.AddSelectPopupColumn("ATTRIBUTECODE", 100, new SqlQuery("GetRoutingPSMAttributeValueCode", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"))
                                                       .SetPopupLayout("ATTRIBUTECODE", PopupButtonStyles.Ok_Cancel, true, false)
                                                       .SetPopupResultCount(0)
                                                       .SetValidationIsRequired()
                                                       .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow);

            control.Conditions.AddTextBox("ATTRIBUTECODE");
            control.Conditions.AddTextBox("PROCESSSEGMENTID").SetPopupDefaultByGridColumnId("PROCESSSEGMENTID").SetIsHidden();
            control.Conditions.AddTextBox("PROCESSSEGMENTCLASSID").SetPopupDefaultByGridColumnId("PROCESSSEGMENTCLASSID").SetIsHidden();

            control.GridColumns.AddTextBoxColumn("ATTRIBUTECODE", 150);

            #endregion 항목

            grdProcessAttributeValue.View.AddSpinEditColumn("ATTRIBUTEVALUE1", 200).SetDisplayFormat("#,##0.#########").SetValidationIsRequired();

            grdProcessAttributeValue.View.AddTextBoxColumn("CREATOR", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdProcessAttributeValue.View.AddTextBoxColumn("CREATEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdProcessAttributeValue.View.AddTextBoxColumn("MODIFIER", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdProcessAttributeValue.View.AddTextBoxColumn("MODIFIEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly().SetTextAlignment(TextAlignment.Center);

            grdProcessAttributeValue.View.PopulateColumns();
        }

        /// <summary>
        /// AOI 그리드
        /// </summary>
        private void InitializeGridAOIQCLayer()
        {
            grdAOIQCLayer.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export | GridButtonItem.Import;

            if (UserInfo.Current.Enterprise == "INTERFLEX")
            {
                grdAOIQCLayer.LanguageKey = "AOIQCLAYER";
                grdAOIQCLayer.View.AddComboBoxColumn("AOIQCLAYER", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=Layer", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
                grdAOIQCLayer.View.AddComboBoxColumn("AOIQCLAYER2", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=Layer", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            }
            else if (UserInfo.Current.Enterprise == "YOUNGPOONG")
            {
                grdAOIQCLayer.LanguageKey = "AOIVRSLAYER";
                grdAOIQCLayer.View.AddComboBoxColumn("AOIQCLAYER", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=Layer", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                    .SetLabel("AOIVRSLAYER");
                grdAOIQCLayer.View.AddComboBoxColumn("AOIQCLAYER2", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=Layer", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                    .SetLabel("AOIVRSLAYER2");
            }

            grdAOIQCLayer.View.AddTextBoxColumn("DESCRIPTION", 150);

            grdAOIQCLayer.View.AddTextBoxColumn("CREATOR", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdAOIQCLayer.View.AddTextBoxColumn("CREATEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdAOIQCLayer.View.AddTextBoxColumn("MODIFIER", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdAOIQCLayer.View.AddTextBoxColumn("MODIFIEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly().SetTextAlignment(TextAlignment.Center);

            grdAOIQCLayer.View.PopulateColumns();
        }

        /// <summary>
        /// 자재 그리드
        /// </summary>
        private void InitializeGridConsumable()
        {
            // 공정
            grdConsumable.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export | GridButtonItem.Import;

            //SITE
            grdConsumable.View.AddTextBoxColumn("PLANTID", 60).SetIsHidden();
            //자재유형
            grdConsumable.View.AddComboBoxColumn("MATERIALDETAILTYPE", 120, new SqlQuery("GetCodeList", "00001", "CODECLASSID=MaterialDetailType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetLabel("MATERIALTYPE");

            #region 자재코드

            var parentItem = grdConsumable.View.AddSelectPopupColumn("COMPONENTITEMID", 100, new SqlQuery("GetBomCompPopup", "10002", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                          .SetPopupLayout("SELECTCONSUMABLE", PopupButtonStyles.Ok_Cancel, true, false)
                                          .SetPopupResultCount(0)
                                          .SetPopupResultMapping("COMPONENTITEMID", "ITEMID")
                                          .SetPopupLayoutForm(1100, 600, FormBorderStyle.FixedToolWindow)
                                          .SetValidationIsRequired()
                                          //.SetPopupAutoFillColumns("DESCRIPTION")
                                          .SetPopupApplySelection((selectRows, gridRow) =>
                                          {
                                              if (selectRows.Count() > 0)
                                              {
                                                  DataTable dt2 = grdConsumable.DataSource as DataTable;
                                                  int handle = grdConsumable.View.FocusedRowHandle;

                                                  foreach (DataRow row in selectRows)
                                                  {
                                                      DataRow dr = null;
                                                      if (dt2.Rows.Count < handle + 1)
                                                      {
                                                          dr = dt2.NewRow();
                                                          dt2.Rows.Add(dr);
                                                      }
                                                      else
                                                      {
                                                          dr = grdConsumable.View.GetDataRow(handle);
                                                      }

                                                      dr["COMPONENTITEMID"] = row["ITEMID"].ToString();
                                                      dr["COMPONENTITEMVERSION"] = row["ITEMVERSION"].ToString();
                                                      dr["COMPONENTITEMNAME"] = row["ITEMNAME"].ToString();
                                                      dr["MATERIALTYPE"] = row["MASTERDATACLASSID"].ToString() == "SubAssembly" ? "Product" : "Consumable";
                                                      dr["COMPONENTUOM"] = row["UOMDEFID"].ToString();
                                                      dr["ISREQUIREDMATERIAL"] = "N";
                                                      dr["ISALTERABLE"] = "N";
                                                      dr["USERLAYER"] = "";
                                                      dr["WIPSUPPLYTYPE"] = row["LOTCONTROL"].ToString() == "Y" ? "Push" : "OperationPull";
                                                      dr["MULTIPLE"] = "1";

                                                      dr["SPEC"] = row["SPEC"].ToString();
                                                      grdConsumable.View.RaiseValidateRow(handle);
                                                      handle++;
                                                  }
                                              }
                                          });

            // 팝업에서 사용할 조회조건 항목 추가
            parentItem.Conditions.AddComboBox("MASTERDATACLASSID", new SqlQuery("GetmasterdataclassList", "10001", $"MESITEMTYPE={"Consumable"}", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "MASTERDATACLASSNAME", "MASTERDATACLASSID")
                .SetEmptyItem();
            parentItem.Conditions.AddTextBox("ITEMID").SetLabel("SEMIPRODUCTCONSUMABLE");
            parentItem.Conditions.AddTextBox("ITEMVERSION").SetLabel("SEMIPRODUCTCONSUMABLEREV");
            parentItem.Conditions.AddTextBox("ITEMNAME").SetLabel("SEMIPRODUCTCONSUMABLENAME");
            parentItem.Conditions.AddTextBox("PLANTID").SetPopupDefaultByGridColumnId("PLANTID").SetIsHidden();

            // 팝업 그리드 설정
            parentItem.GridColumns.AddTextBoxColumn("PLANTID", 60)
                .SetTextAlignment(TextAlignment.Center);
            parentItem.GridColumns.AddTextBoxColumn("MASTERDATACLASSNAME", 70).SetLabel("MATERIALTYPE")
                .SetTextAlignment(TextAlignment.Center);
            parentItem.GridColumns.AddTextBoxColumn("ITEMID", 120).SetLabel("COMPONENTITEMID");
            parentItem.GridColumns.AddTextBoxColumn("ITEMVERSION", 80).SetLabel("COMPONENTITEMVERSION");
            parentItem.GridColumns.AddTextBoxColumn("ITEMNAME", 250).SetLabel("COMPONENTITEMNAME");
            parentItem.GridColumns.AddTextBoxColumn("SPEC", 150).SetLabel("SPECDEFINITION");
            parentItem.GridColumns.AddComboBoxColumn("UOMDEFID", 70, new SqlQuery("GetUOMList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "UOMDEFNAME", "UOMDEFID").SetLabel("COMPONENTUOM");
            parentItem.GridColumns.AddTextBoxColumn("MAKER", 100).SetLabel("MEASURINGMANUFACTURER");

            #endregion 자재코드

            //자재명
            grdConsumable.View.AddTextBoxColumn("COMPONENTITEMNAME", 180).SetIsReadOnly();
            //규격
            grdConsumable.View.AddTextBoxColumn("SPEC", 100).SetLabel("SPECDEFINITION").SetIsReadOnly();
            //제조사
            grdConsumable.View.AddTextBoxColumn("MAKER", 100).SetLabel("MEASURINGMANUFACTURER").SetIsReadOnly();

            //PNLX
            grdConsumable.View.AddSpinEditColumn("VARIABLE1", 110).SetDisplayFormat("#,##0.#####");
            //PNLY
            grdConsumable.View.AddSpinEditColumn("VARIABLE2", 110).SetDisplayFormat("#,##0.#####");

            //합수
            grdConsumable.View.AddSpinEditColumn("SUMQTY", 100).SetLabel("ARRAY").SetDisplayFormat("#,##0.#####");
            //소요량
            grdConsumable.View.AddSpinEditColumn("COMPONENTQTY", 130).SetDisplayFormat("#,##0.#########").SetValidationIsRequired();
            //생지배수
            ConditionItemSpinEdit multiple = grdConsumable.View.AddSpinEditColumn("MULTIPLE", 80).SetDisplayFormat("#,##0").SetDefault(1);
            multiple.MinValue = 1;

            //사용층
            grdConsumable.View.AddComboBoxColumn("USERLAYER", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=UserLayer", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetDefault("");
            //UOM
            grdConsumable.View.AddComboBoxColumn("COMPONENTUOM", 80, new SqlQuery("GetUOMList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "UOMDEFID", "UOMDEFNAME")
                .SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            //색상
            grdConsumable.View.AddTextBoxColumn("COLOR", 70).SetIsReadOnly();

            //잉크품목구분
            grdConsumable.View.AddComboBoxColumn("INKTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OutsourcingSpecInkType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetLabel("INKITEMCLASSIFY");
            //작업방법
            grdConsumable.View.AddComboBoxColumn("WORKMETHOD", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OutsourcingSpecWorkType", $"LANGUAGETYPE={ UserInfo.Current.LanguageType }"));
            //공급유형
            grdConsumable.View.AddComboBoxColumn("WIPSUPPLYTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=WipSupplyType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetValidationIsRequired().SetDefault("Push");

            //RTR투입
            grdConsumable.View.AddComboBoxColumn("ISREQUIREDMATERIAL", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetTextAlignment(TextAlignment.Center).SetDefault("N");
            //대체자재사용
            grdConsumable.View.AddComboBoxColumn("ISALTERABLE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetTextAlignment(TextAlignment.Center).SetLabel("ISREPLACEMATERIAL").SetDefault("Y");
            //비고
            grdConsumable.View.AddTextBoxColumn("DESCRIPTION", 100);

            grdConsumable.View.AddTextBoxColumn("CREATOR", 80).SetIsReadOnly().SetTextAlignment(Framework.SmartControls.TextAlignment.Center);
            grdConsumable.View.AddTextBoxColumn("CREATEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly().SetTextAlignment(Framework.SmartControls.TextAlignment.Center);
            grdConsumable.View.AddTextBoxColumn("MODIFIER", 80).SetIsReadOnly().SetTextAlignment(Framework.SmartControls.TextAlignment.Center);
            grdConsumable.View.AddTextBoxColumn("MODIFIEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly().SetTextAlignment(Framework.SmartControls.TextAlignment.Center);

            grdConsumable.View.PopulateColumns();
        }

        public void InitializeControl()
        {
            grdOperation.View.OptionsCustomization.AllowSort = false;

            //var control = grdDurable.View.GetConditions().GetControl<SmartSelectPopupEdit>("TOOLCODE");
            //control.SelectPopupCondition.Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").EditValueChanged += (sender, e) =>
            //{
            //    SmartSelectPopupEdit PopProdutid = sender as SmartSelectPopupEdit;

            //    if (Format.GetFullTrimString(PopProdutid.EditValue).Equals(string.Empty))
            //    {
            //       control.SelectPopupCondition.Conditions.GetControl<SmartTextBox>("P_PRODUCTDEFID").EditValue = string.Empty;
            //       control.SelectPopupCondition.Conditions.GetControl<SmartTextBox>("PRODUCTDEFVERSION").EditValue = string.Empty;
            //       control.SelectPopupCondition.Conditions.GetControl<SmartTextBox>("ITEMID").EditValue = string.Empty;
            //       control.SelectPopupCondition.Conditions.GetControl<SmartTextBox>("ITEMVERSION").EditValue = string.Empty;

            //    }
            //};
        }

        #endregion 컨텐츠 영역 초기화

        #region 이벤트

        /// <summary>
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            grdOperation.View.FocusedRowChanged += grdOperationView_FocusedRowChanged;
            grdOperation.View.AddingNewRow += grdOperation_AddingNewRow;
            grdOperation.View.ShowingEditor += grdOperationView_ShowingEditor;
            grdOperation.ToolbarDeletingRow += grdOperation_ToolbarDeletingRow;
            grdOperation.View.KeyDown += grdOperationView_KeyDown;

            grdConsumable.View.AddingNewRow += grdConsumableView_AddingNewRow;
            grdConsumable.View.CellValueChanged += grdConsumableView_CellValueChanged;
            grdConsumable.View.ShowingEditor += grdConsumableView_ShowingEditor;
            grdConsumable.ToolbarDeletingRow += Grd_ToolbarDeletingRow;

            grdProcessSpec.View.AddingNewRow += grdProcessSpecView_AddingNewRow;
            grdProcessSpec.View.ShowingEditor += grdProcessSpecView_ShowingEditor;
            grdProcessSpec.ToolbarDeletingRow += Grd_ToolbarDeletingRow;

            grdDurable.View.AddingNewRow += grdDurableView_AddingNewRow;
            grdDurable.View.ShowingEditor += grdDurableView_ShowingEditor;
            grdDurable.ToolbarDeletingRow += Grd_ToolbarDeletingRow;

            grdProcessAttributeValue.View.AddingNewRow += grdProcessAttributeValueView_AddingNewRow;
            grdProcessAttributeValue.ToolbarDeletingRow += Grd_ToolbarDeletingRow;

            grdAOIQCLayer.View.AddingNewRow += grdAOIQCLayerView_AddingNewRow;
            grdAOIQCLayer.ToolbarDeletingRow += Grd_ToolbarDeletingRow;

            smartTabControl1.SelectedPageChanging += SmartTabControl1_SelectedPageChanging;
            smcXBarChartInfo.Editor.EditValueChanged += smcXBarChartInfoEditor_EditValueChanged;

            treeRouting.FocusedNodeChanged += TreeRouting_FocusedNodeChanged;
        }

        /// <summary>
        /// 외부에서 호출시 자동 조회
        /// </summary>
        /// <param name="parameters"></param>
        public override void LoadForm(Dictionary<string, object> parameters)
        {
            base.LoadForm(parameters);

            if (parameters != null)
            {
                _parameters = parameters;
                Conditions.SetValue("P_PRODUCTDEFID", 0, parameters["ITEMID"]);
                Conditions.SetValue("P_PRODUCTDEFVERSION", 0, parameters["ITEMVERSION"]);
                Conditions.SetValue("p_ProductName", 0, parameters["ITEMNAME"]);
                SendKeys.Send("{F5}");
            }
        }

        private void Grd_ToolbarDeletingRow(object sender, CancelEventArgs e)
        {
            DialogResult result = ShowMessage(MessageBoxButtons.YesNo, "IsDeleted"); //삭제하시겠습니까?

            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 팝업 컬럼 x버튼 누를 때 이벤트 - 치공구
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RoutingVer3_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind.Equals(DevExpress.XtraEditors.Controls.ButtonPredefines.Clear))
            {
                //grdDurable.View.SetFocusedRowCellValue("PRODUCTDEFID", string.Empty);
                //grdDurable.View.SetFocusedRowCellValue("PRODUCTDEFVERSION", string.Empty);
                //grdDurable.View.SetFocusedRowCellValue("PRODUCTDEFNAME", string.Empty);
                grdDurable.View.SetFocusedRowCellValue("DURABLETYPE", string.Empty);
                grdDurable.View.SetFocusedRowCellValue("DURABLEDEFVERSION", string.Empty);
                grdDurable.View.SetFocusedRowCellValue("DURABLECLASSID", string.Empty);
                grdDurable.View.SetFocusedRowCellValue("TOOLTYPE", string.Empty);
                grdDurable.View.SetFocusedRowCellValue("TOOLDETAILTYPE", string.Empty);
                grdDurable.View.SetFocusedRowCellValue("FORM", string.Empty);
                grdDurable.View.SetFocusedRowCellValue("TOOLVERSION", string.Empty);
                grdDurable.View.SetFocusedRowCellValue("SUMMARY", string.Empty);
                grdDurable.View.SetFocusedRowCellValue("FILMUSELAYER1", string.Empty);
                grdDurable.View.SetFocusedRowCellValue("HITCOUNT", string.Empty);
            }
        }

        private void grdOperationView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DataRow focusedRow = grdOperation.View.GetFocusedDataRow();
                DataTable dt = grdOperation.DataSource as DataTable;
                if (focusedRow.RowState == DataRowState.Added)
                {
                    dt.Rows.Remove(focusedRow);
                }
            }
        }

        private DataTable GetDataTableBySmartTabControl1(bool isValidation)
        {
            DataTable dtChangeData = new DataTable();
            switch (smartTabControl1.SelectedTabPageIndex)
            {
                case 0:
                    //자재
                    DataTable dtConsumable = grdConsumable.DataSource as DataTable;
                    if (dtConsumable != null)
                    {
                        if (isValidation)
                        {
                            var duplicateConsumable = from r in dtConsumable.AsEnumerable()
                                                      group r by new
                                                      {
                                                          COMPONENTITEMID = r.Field<string>("COMPONENTITEMID"),
                                                          COMPONENTITEMNAME = r.Field<string>("COMPONENTITEMNAME")
                                                      } into g
                                                      where g.Count() > 1
                                                      select g;

                            if (duplicateConsumable.Count() > 0)
                            {
                                throw MessageException.Create("DuplicationConsumableID", duplicateConsumable.ElementAt(0).Key.COMPONENTITEMNAME);
                            }
                        }
                        try
                        {
                            dtChangeData = grdConsumable.GetChangedRows();
                        }
                        catch { }
                    }
                    break;

                case 1:
                    //공정스펙
                    DataTable dtProcessSpec = grdProcessSpec.DataSource as DataTable;
                    if (dtProcessSpec != null)
                    {
                        if (isValidation)
                        {
                            var duplicateProcessSpec = from r in dtProcessSpec.AsEnumerable()
                                                       group r by new
                                                       {
                                                           INSPITEMID = r.Field<string>("INSPITEMID"),
                                                           INSPITEMNAME = r.Field<string>("INSPITEMNAME")
                                                       } into g
                                                       where g.Count() > 1
                                                       select g;

                            if (duplicateProcessSpec.Count() > 0)
                            {
                                throw MessageException.Create("DuplicationInspectionItemID", duplicateProcessSpec.ElementAt(0).Key.INSPITEMNAME);
                            }
                        }
                    }
                    try
                    {
                        dtChangeData = grdProcessSpec.GetChangedRows();
                    }
                    catch { }
                    break;

                case 2:
                    //치공구
                    DataTable dtDurable = grdDurable.DataSource as DataTable;
                    if (dtDurable != null)
                    {
                        if (isValidation)
                        {
                            var duplicateDurable = from r in dtDurable.AsEnumerable()
                                                   group r by new
                                                   {
                                                       TOOLCODE = r.Field<string>("TOOLCODE"),
                                                       TOOLNAME = r.Field<string>("TOOLNAME")
                                                   } into g
                                                   where g.Count() > 1
                                                   select g;

                            if (duplicateDurable.Count() > 0)
                            {
                                throw MessageException.Create("DuplicationDurableID", duplicateDurable.ElementAt(0).Key.TOOLNAME);
                            }
                        }

                        try
                        {
                            dtChangeData = grdDurable.GetChangedRows();
                        }
                        catch
                        { }
                    }
                    break;

                case 3:
                    //외주단가
                    try
                    {
                        dtChangeData = grdProcessAttributeValue.GetChangedRows();
                    }
                    catch { }
                    break;

                case 4:
                    //AOI 층
                    try
                    {
                        dtChangeData = grdAOIQCLayer.GetChangedRows();
                    }
                    catch { }
                    break;

                default:
                    break;
            }

            return dtChangeData;
        }

        private void SmartTabControl1_SelectedPageChanging(object sender, DevExpress.XtraTab.TabPageChangingEventArgs e)
        {
            if (GetDataTableBySmartTabControl1(false).Rows.Count > 0)
            {
                switch (ShowMessage(MessageBoxButtons.YesNo, "WRITTENBEDELETE"))
                {
                    case DialogResult.Yes:
                        SearchGrid(e.Page.Tag.ToString());
                        break;

                    case DialogResult.No:
                        e.Cancel = true;
                        break;
                }
            }
            else
            {
                SearchGrid(e.Page.Tag.ToString());
            }
        }

        private void grdOperation_ToolbarDeletingRow(object sender, CancelEventArgs e)
        {
            switch (ShowMessage(MessageBoxButtons.YesNo, "DeleteLinkedItemBySegment"))
            {
                case DialogResult.Yes:
                    break;

                case DialogResult.No:
                    e.Cancel = true;
                    break;
            }
        }

        private void smcXBarChartInfoEditor_EditValueChanged(object sender, EventArgs e)
        {
            if (grdProcessSpec.DataSource is DataTable dt)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    dr["DEFAULTCHARTTYPE"] = smcXBarChartInfo.Editor.EditValue.ToString();
                }
            }
        }

        private void grdOperationView_ShowingEditor(object sender, CancelEventArgs e)
        {
            string currentColumnName = grdOperation.View.FocusedColumn.FieldName;

            if (currentColumnName.Equals("PLANTID") || currentColumnName.Equals("PROCESSSEGMENTID"))
            {
                if (!grdOperation.View.GetFocusedDataRow().RowState.Equals(DataRowState.Added))
                {
                    e.Cancel = true;
                }
            }
        }

        private void grdConsumableView_ShowingEditor(object sender, CancelEventArgs e)
        {
            string currentColumnName = grdConsumable.View.FocusedColumn.FieldName;

            if (currentColumnName.Equals("COMPONENTITEMID"))
            {
                if (!grdConsumable.View.GetFocusedDataRow().RowState.Equals(DataRowState.Added))
                {
                    e.Cancel = true;
                }
            }

            if (currentColumnName.StartsWith("VARIABLE"))
            {
                MaterialType materialType = (MaterialType)Enum.Parse(typeof(MaterialType), dtProductDEF.Rows[0]["PRODUCTDEFCLASSID"].ToString(), true);

                BOMInfo bomInfo = BOMCalculatorList.BOMCalculators[materialType][0];

                if (bomInfo != null)
                {
                    if (currentColumnName.Equals("VARIABLE1"))
                    {
                        e.Cancel = !bomInfo.IsEditVariable1;
                    }

                    if (currentColumnName.Equals("VARIABLE2"))
                    {
                        e.Cancel = !bomInfo.IsEditVariable2;
                    }

                    if (currentColumnName.Equals("VARIABLE3"))
                    {
                        e.Cancel = !bomInfo.IsEditVariable3;
                    }

                    if (currentColumnName.Equals("VARIABLE4"))
                    {
                        e.Cancel = !bomInfo.IsEditVariable4;
                    }

                    if (currentColumnName.Equals("VARIABLE5"))
                    {
                        e.Cancel = !bomInfo.IsEditVariable5;
                    }
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void grdDurableView_ShowingEditor(object sender, CancelEventArgs e)
        {
            string currentColumnName = grdDurable.View.FocusedColumn.FieldName;

            if (currentColumnName.Equals("TOOLCODE"))
            {
                if (!grdDurable.View.GetFocusedDataRow().RowState.Equals(DataRowState.Added))
                {
                    e.Cancel = true;
                }
            }

            if (currentColumnName.Equals("WRAPTYPE") && !grdDurable.View.GetFocusedDataRow()["PROCESSSEGMENTCLASSID"].Equals("6052"))
            {
                e.Cancel = true;
            }
        }

        private void grdProcessSpecView_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (grdProcessSpec.View.FocusedColumn.FieldName.Equals("INSPITEMNAME"))
            {
                if (!grdProcessSpec.View.GetFocusedDataRow().RowState.Equals(DataRowState.Added))
                {
                    e.Cancel = true;
                }
            }
        }

        private void grdConsumableView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            grdConsumable.View.CellValueChanged -= grdConsumableView_CellValueChanged;

            MaterialType materialType = (MaterialType)Enum.Parse(typeof(MaterialType), dtProductDEF.Rows[0]["PRODUCTDEFCLASSID"].ToString(), true);

            if (e.Column.FieldName.Equals("SUMQTY"))
            {
                string sumQTY = e.Value.ToString();

                string pnlX = grdConsumable.View.GetFocusedRowCellValue("VARIABLE1").ToString().Trim();
                string pnlY = grdConsumable.View.GetFocusedRowCellValue("VARIABLE2").ToString().Trim();
                string multiple = grdConsumable.View.GetFocusedRowCellValue("MULTIPLE").ToString();

                CalculateYPECOMPONENTQTY(materialType, e.RowHandle, pnlX, pnlY, sumQTY, multiple);
            }

            if (e.Column.FieldName.StartsWith("VARIABLE"))
            {
                string pnlX = grdConsumable.View.GetFocusedRowCellValue("VARIABLE1").ToString().Trim();
                string pnlY = grdConsumable.View.GetFocusedRowCellValue("VARIABLE2").ToString().Trim();
                string sumQTY = grdConsumable.View.GetFocusedRowCellValue("SUMQTY").ToString();
                string multiple = grdConsumable.View.GetFocusedRowCellValue("MULTIPLE").ToString();

                CalculateYPECOMPONENTQTY(materialType, e.RowHandle, pnlX, pnlY, sumQTY, multiple);
            }

            if (e.Column.FieldName.Equals("MULTIPLE"))
            {
                string pnlX = grdConsumable.View.GetFocusedRowCellValue("VARIABLE1").ToString().Trim();
                string pnlY = grdConsumable.View.GetFocusedRowCellValue("VARIABLE2").ToString().Trim();
                string sumQTY = grdConsumable.View.GetFocusedRowCellValue("SUMQTY").ToString();
                string multiple = grdConsumable.View.GetFocusedRowCellValue("MULTIPLE").ToString();

                CalculateYPECOMPONENTQTY(materialType, e.RowHandle, pnlX, pnlY, sumQTY, multiple);
            }

            grdConsumable.View.CellValueChanged += grdConsumableView_CellValueChanged;
        }

        private void CalculateYPECOMPONENTQTY(MaterialType materialType, int rowHandle, string pnlX, string pnlY, string sumQTY, string multiple)
        {
            if (!string.IsNullOrWhiteSpace(pnlX) && pnlX != "0" && !string.IsNullOrWhiteSpace(pnlY) && pnlY != "0")
            {
                BOMInfo bomInfo = BOMCalculatorList.BOMCalculators[materialType][0];

                if (bomInfo != null)
                {
                    Eval x = new Eval();
                    x.SetSymbol("v1", Convert.ToDouble(string.IsNullOrWhiteSpace(grdConsumable.View.GetRowCellValue(rowHandle, "VARIABLE1").ToString()) ? "0" : grdConsumable.View.GetRowCellValue(rowHandle, "VARIABLE1").ToString()));
                    x.SetSymbol("v2", Convert.ToDouble(string.IsNullOrWhiteSpace(grdConsumable.View.GetRowCellValue(rowHandle, "VARIABLE2").ToString()) ? "0" : grdConsumable.View.GetRowCellValue(rowHandle, "VARIABLE2").ToString()));
                    x.SetSymbol("v3", Convert.ToDouble(string.IsNullOrWhiteSpace(multiple) ? "1" : multiple));

                    if (!string.IsNullOrWhiteSpace(sumQTY))
                    {
                        x.SetSymbol("v3", Convert.ToDouble(sumQTY));
                        x.SetSymbol("v4", Convert.ToDouble(multiple));

                        bomInfo.Formula = "(1 / v3) * (v1 / 1000) *  (v2 / 1000) * v4";
                    }
                    else
                    {
                        x.SetSymbol("v3", Convert.ToDouble(multiple));

                        bomInfo.Formula = "(v1 / 1000) *  (v2 / 1000) * v3";
                    }

                    grdConsumable.View.SetRowCellValue(rowHandle, "COMPONENTQTY", (Math.Truncate(x.Evaluate(bomInfo.Formula) * 1000000000) / 1000000000));
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(sumQTY))
                {
                    grdConsumable.View.SetFocusedRowCellValue("COMPONENTQTY", (Math.Truncate((1 / Convert.ToDouble(sumQTY)) * 1000000000) / 1000000000) * Convert.ToDouble(string.IsNullOrWhiteSpace(multiple) ? "1" : multiple));
                }
            }
        }

        private void TreeRouting_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (e.Node["MASTERDATACLASSID"].ToString() != "Product" && e.Node["MASTERDATACLASSID"].ToString() != "SubAssembly")
            {
                treeRouting.SetFocusedNode(e.OldNode);
                return;
            }

            treeRouting.FocusedNodeChanged -= TreeRouting_FocusedNodeChanged;

            string assemblyItemId = e.Node["ASSEMBLYITEMID"].ToString();
            string assemblyItemVersion = e.Node["ASSEMBLYITEMVERSION"].ToString();
            string plantId = e.Node["PLANTID"].ToString();

            Dictionary<string, object> values = new Dictionary<string, object>
            {
                { "ENTERPRISEID", UserInfo.Current.Enterprise },
                { "PLANTID", plantId },
                { "P_PRODUCTDEFID", assemblyItemId },
                { "P_PRODUCTDEFVERSION", assemblyItemVersion },
                { "LANGUAGETYPE", UserInfo.Current.LanguageType }
            };

            DataTable dtOperation = new DataTable();
            try
            {
                dtOperation = grdOperation.GetChangedRows();
            }
            catch { }

            DataTable dtChangeData = GetDataTableBySmartTabControl1(false);

            if (dtProductDEF != null)
            {
                //bool isChangeWareHouse = dtProductDEF.Rows[0]["COMPLETIONWAREHOUSEID"].ToString().Equals(spCompletionWareHouse.GetValue().ToString()) == true ? false : true;

                //if (isChangeWareHouse)
                //{
                //    dtProductDEF.Rows[0]["COMPLETIONWAREHOUSEID"] = spCompletionWareHouse.GetValue().ToString();
                //    dtProductDEF.Rows[0]["WAREHOUSENAME"] = spCompletionWareHouse.Text;
                //}
                bool isChangeRTRSheet = dtProductDEF.Rows[0]["RTRSHT"].ToString().Equals(scRTRSheet.EditValue.ToString()) == true ? false : true;

                if (isChangeRTRSheet)
                {
                    dtProductDEF.Rows[0]["RTRSHT"] = scRTRSheet.EditValue.ToString();
                }

                //if (isChangeWareHouse || isChangeRTRSheet || dtOperation.Rows.Count > 0 || dtChangeData.Rows.Count > 0)
                //{
                if (dtOperation.Rows.Count > 0 || dtChangeData.Rows.Count > 0)
                {
                    DialogResult result = ShowMessage(MessageBoxButtons.YesNo, "WRITTENBEDELETE");

                    switch (result)
                    {
                        case DialogResult.Yes:
                            SearchProductInfo(values);

                            break;

                        case DialogResult.No:
                            treeRouting.SetFocusedNode(e.OldNode);
                            break;
                    }
                }
                else
                {
                    SearchProductInfo(values);
                }
            }
            else
            {
                SearchProductInfo(values);
            }

            treeRouting.FocusedNodeChanged += TreeRouting_FocusedNodeChanged;
        }

        private void grdAOIQCLayerView_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            if (grdOperation.View.FocusedRowHandle < 0)
            {
                args.IsCancel = true;
                return;
            }

            DataRow focusedRow = grdOperation.View.GetFocusedDataRow();

            if (focusedRow != null)
            {
                if (focusedRow.RowState == DataRowState.Added) //|| focusedRow.RowState == DataRowState.Deleted)
                {
                    ShowMessage("NoSelectDataSaveAndProceed");
                    args.IsCancel = true;
                    return;
                }
            }
            else
            {
                args.IsCancel = true;
                return;
            }
        }

        private void grdProcessAttributeValueView_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            if (grdOperation.View.FocusedRowHandle < 0)
            {
                args.IsCancel = true;
                return;
            }

            DataRow focusedRow = grdOperation.View.GetFocusedDataRow();

            if (focusedRow != null)
            {
                if (focusedRow.RowState == DataRowState.Added) //|| focusedRow.RowState == DataRowState.Deleted)
                {
                    ShowMessage("NoSelectDataSaveAndProceed");
                    args.IsCancel = true;
                    return;
                }
            }
            else
            {
                args.IsCancel = true;
                return;
            }
        }

        private void grdDurableView_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            if (grdOperation.View.FocusedRowHandle < 0)
            {
                args.IsCancel = true;
                return;
            }

            DataRow focusedRow = grdOperation.View.GetFocusedDataRow();

            if (focusedRow != null)
            {
                if (focusedRow.RowState == DataRowState.Added) //|| focusedRow.RowState == DataRowState.Deleted)
                {
                    ShowMessage("NoSelectDataSaveAndProceed");
                    args.IsCancel = true;
                    return;
                }
            }
            else
            {
                args.IsCancel = true;
                return;
            }

            args.NewRow["OPERATIONID"] = focusedRow["OPERATIONID"];
            args.NewRow["EQUIPMENTID"] = "*";
            args.NewRow["RESOURCECLASSID"] = "*";
            args.NewRow["PROCESSSEGMENTCLASSID"] = focusedRow["PROCESSSEGMENTCLASSID"];
            args.NewRow["PRODUCTDEFID"] = txtProductDEFId.Text;
            args.NewRow["PRODUCTDEFVERSION"] = txtProductDEFVersion.Text;
            args.NewRow["PRODUCTDEFNAME"] = txtProductDEFName.Text;
        }

        private void grdProcessSpecView_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            if (grdOperation.View.FocusedRowHandle < 0)
            {
                args.IsCancel = true;
                return;
            }

            DataRow focusedRow = grdOperation.View.GetFocusedDataRow();

            if (focusedRow != null)
            {
                if (focusedRow.RowState == DataRowState.Added) //|| focusedRow.RowState == DataRowState.Deleted)
                {
                    ShowMessage("NoSelectDataSaveAndProceed");
                    args.IsCancel = true;
                    return;
                }
            }
            else
            {
                args.IsCancel = true;
                return;
            }

            args.NewRow["PROCESSSEGMENTID"] = grdOperation.View.GetFocusedDataRow()["PROCESSSEGMENTID"].ToString();
            args.NewRow["INSPECTIONDEFID"] = "OperationInspection-OperationInspection";
            args.NewRow["INSPECTIONDEFVERSION"] = "*";
            args.NewRow["SPECCLASSID"] = "OperationSpec";
            args.NewRow["DEFAULTCHARTTYPE"] = smcXBarChartInfo.Editor.EditValue.ToString();
        }

        private void grdConsumableView_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            if (grdOperation.View.FocusedRowHandle < 0)
            {
                args.IsCancel = true;
                return;
            }

            DataRow focusedRow = grdOperation.View.GetFocusedDataRow();

            if (focusedRow != null)
            {
                if (focusedRow.RowState == DataRowState.Added) //|| focusedRow.RowState == DataRowState.Deleted)
                {
                    ShowMessage("NoSelectDataSaveAndProceed");
                    args.IsCancel = true;
                    return;
                }
            }
            else
            {
                args.IsCancel = true;
                return;
            }

            args.NewRow["WIPSUPPLYTYPE"] = "Push";
            args.NewRow["PLANTID"] = focusedRow["PLANTID"];
            args.NewRow["ISREQUIREDMATERIAL"] = "N";
            args.NewRow["ISALTERABLE"] = "N";
        }

        private void grdOperationView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            grdOperation.View.FocusedRowChanged -= grdOperationView_FocusedRowChanged;

            if (dtProductDEF != null && e.FocusedRowHandle > -1 && e.PrevFocusedRowHandle > -1)
            {
                DataRow row = grdOperation.View.GetFocusedDataRow();
                if (row.RowState != DataRowState.Added)
                {
                    DataTable dtChangeData = GetDataTableBySmartTabControl1(false);

                    if (dtChangeData.Rows.Count > 0)
                    {
                        DialogResult result = ShowMessage(MessageBoxButtons.YesNo, "WRITTENBEDELETE");

                        switch (result)
                        {
                            case DialogResult.Yes:
                                SearchGrid(smartTabControl1.SelectedTabPage.Tag.ToString());
                                break;

                            case DialogResult.No:
                                grdOperation.View.FocusedRowHandle = e.PrevFocusedRowHandle;
                                break;
                        }
                    }
                    else
                    {
                        SearchGrid(smartTabControl1.SelectedTabPage.Tag.ToString());
                    }

                    if (grdOperation.View.GetFocusedRowCellValue("PROCESSSEGMENTNAME").ToString().ToUpper().Contains("AOI") || grdOperation.View.GetFocusedRowCellValue("PROCESSSEGMENTNAME").ToString().ToUpper().Contains("VRS"))
                    {
                        xtpAOILayer.PageVisible = true;
                    }
                    else
                    {
                        xtpAOILayer.PageVisible = false;
                        grdAOIQCLayer.DataSource = null;
                    }
                }
            }

            if (grdOperation.View.FocusedRowHandle > -1)
            {
                if (grdOperation.View.GetFocusedRowCellValue("ISREQUIREDMATERIAL").ToString().Equals("Y"))
                {
                    smartTabControl1.SelectedTabPageIndex = 0;
                }
                else if (grdOperation.View.GetFocusedRowCellValue("ISREQUIREDOPERATIONSPEC").ToString().Equals("Y"))
                {
                    smartTabControl1.SelectedTabPageIndex = 1;
                }
                else if (grdOperation.View.GetFocusedRowCellValue("ISREQUIREDTOOL").ToString().Equals("Y"))
                {
                    smartTabControl1.SelectedTabPageIndex = 2;
                }
            }

            grdOperation.View.FocusedRowChanged += grdOperationView_FocusedRowChanged;
        }

        private void grdOperation_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            if (dtProductDEF == null)
            {
                args.IsCancel = true;
                return;
            }

            string productDefId = dtProductDEF.Rows[0]["PRODUCTDEFID"].ToString();
            string productDefVersion = dtProductDEF.Rows[0]["PRODUCTDEFVERSION"].ToString();

            GetNumber number = new GetNumber();
            string operationId = number.GetStdNumber("Operation", productDefId + productDefVersion);

            if (treeRouting.FocusedNode == null)
            {
                args.NewRow["OPERATIONID"] = operationId;
                args.NewRow["PLANTID"] = UserInfo.Current.Plant;
                args.NewRow["PROCESSUOM"] = "PNL";
            }
            else
            {
                if (treeRouting.GetFocusedDataRow()["MASTERDATACLASSID"].ToString().Equals("SubAssembly") ||
                    treeRouting.GetFocusedDataRow()["MASTERDATACLASSID"].ToString().Equals("Product"))
                {
                    args.NewRow["OPERATIONID"] = operationId;
                    args.NewRow["PLANTID"] = UserInfo.Current.Plant;
                    args.NewRow["PROCESSUOM"] = "PNL";
                }
                else
                {
                    ShowMessage(MessageBoxButtons.OK, "WrongRegistBom");
                    args.IsCancel = true;
                    return;
                }
            }

            DataTable dtProcessPath = grdOperation.DataSource as DataTable;

            if (dtProcessPath.AsEnumerable().Where(s => !string.IsNullOrEmpty(s["USERSEQUENCE"].ToString())).Count() == 0)
            {
                args.NewRow["USERSEQUENCE"] = "10";
            }
            else
            {
                int userSequence = dtProcessPath.AsEnumerable().Where(s => !string.IsNullOrEmpty(s["USERSEQUENCE"].ToString())).Max(s => int.Parse(s["USERSEQUENCE"].ToString()));
                args.NewRow["USERSEQUENCE"] = (((userSequence / 10) + 1) * 10).ToString();
            }

            args.NewRow["ISSEGMENTCHANGE"] = "N";
        }

        #endregion 이벤트

        #region 툴바

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            if (dtProductDEF == null)
            {
                throw MessageException.Create("NoSaveData");
            }

            if (!(grdOperation.DataSource is DataTable dtValidationUserSequence))
            {
                throw MessageException.Create("NoSaveData");
            }

            string productDefClassId = dtProductDEF.Rows[0]["PRODUCTDEFCLASSID"].ToString();

            // 2021.04.19 품목코드가 제품인 경우에만 마지막이 pacing이 와야 한다
            if (productDefClassId.Equals("Product"))
            {
                int maxUserSequence = dtValidationUserSequence.AsEnumerable().Max(x => Convert.ToInt32(x.Field<string>("USERSEQUENCE")));

                string lastSeq = dtValidationUserSequence.AsEnumerable().Where(x => x.Field<string>("USERSEQUENCE").Equals(maxUserSequence.ToString()))
                                                                              .Select(x => x.Field<string>("PROCESSSEGMENTCLASSID")).ToList()[0];

                if (!lastSeq.Equals("8012"))
                {
                    throw MessageException.Create("LastProcesssegmentClassIDNotPaking");
                }
            }

            var userSequenceList = from r in dtValidationUserSequence.AsEnumerable()
                                   group r by new { USERSEQUENCE = r.Field<string>("USERSEQUENCE") } into g
                                   where g.Count() > 1
                                   select g;

            if (userSequenceList.Count() > 0)
            {
                throw MessageException.Create("DuplicationUserSequence", userSequenceList.ElementAt(0).Key.USERSEQUENCE);
            }

            DataTable dtValidationSegment = grdOperation.DataSource as DataTable;

            var segmentList = from r in dtValidationSegment.AsEnumerable()
                              group r by new { PROCESSSEGMENTID = r.Field<string>("PROCESSSEGMENTID") } into g
                              where g.Count() > 1
                              select g;

            if (segmentList.Count() > 0)
            {
                throw MessageException.Create("DuplicationSegmentID", segmentList.ElementAt(0).Key.PROCESSSEGMENTID);
            }

            DataTable dtOperation = grdOperation.GetChangedRows();

            DataTable dtChangeData = GetDataTableBySmartTabControl1(true);

            bool isChangeRTRSheet = dtProductDEF.Rows[0]["RTRSHT"].ToString().Equals(scRTRSheet.EditValue.ToString()) == true ? false : true;
            if (isChangeRTRSheet)
            {
                dtProductDEF.Rows[0]["RTRSHT"] = scRTRSheet.EditValue.ToString();
            }

            bool isChangePnlx = dtProductDEF.Rows[0]["PNLSIZEXAXIS"].ToString().Equals(spPnlX.EditValue.ToString()) == true ? false : true;

            if (isChangePnlx)
                dtProductDEF.Rows[0]["PNLSIZEXAXIS"] = spPnlX.EditValue.ToString();

            if (Format.GetDouble(dtProductDEF.Rows[0]["PNLSIZEXAXIS"], 0).Equals(0))
            {
                throw MessageException.Create("QtyInputZero", Language.Get("PANELX"));
            }

            bool isChangePnly = dtProductDEF.Rows[0]["PNLSIZEYAXIS"].ToString().Equals(spPnlY.EditValue.ToString()) == true ? false : true;

            if (isChangePnly)
                dtProductDEF.Rows[0]["PNLSIZEYAXIS"] = spPnlY.EditValue.ToString();

            if (Format.GetDouble(dtProductDEF.Rows[0]["PNLSIZEYAXIS"], 0).Equals(0))
            {
                throw MessageException.Create("QtyInputZero", Language.Get("PANELY"));
            }

            //2021.03.09 패널당 블럭수 추가
            bool isChangePNLBLK = dtProductDEF.Rows[0]["PCSARY"].ToString().Equals(spPCSBLK.EditValue.ToString()) == true ? false : true;

            if (isChangePNLBLK)
                dtProductDEF.Rows[0]["PCSARY"] = spPCSBLK.EditValue.ToString();

            bool isChangePCSPNL = dtProductDEF.Rows[0]["PCSPNL"].ToString().Equals(spPCSPNL.EditValue.ToString()) == true ? false : true;

            if (isChangePCSPNL)
                dtProductDEF.Rows[0]["PCSPNL"] = spPCSPNL.EditValue.ToString();

            if (Format.GetDouble(dtProductDEF.Rows[0]["PCSPNL"], 0).Equals(0))
            {
                throw MessageException.Create("QtyInputZero", Language.Get("ARRAYPCS"));
            }

            bool isChangePCSMM = dtProductDEF.Rows[0]["PCSMM"].ToString().Equals(spPCSMM.EditValue.ToString()) == true ? false : true;

            if (isChangePCSMM)
            {
                dtProductDEF.Rows[0]["PCSMM"] = spPCSMM.EditValue.ToString();
            }

            if (Format.GetDouble(dtProductDEF.Rows[0]["PCSMM"], 0).Equals(0))
            {
                throw MessageException.Create("QtyInputZero", Language.Get("CALCULATEPCS"));
            }

            bool isChangeRemark = dtProductDEF.Rows[0]["REMARK"].ToString().Equals(txtRemark.EditValue.ToString()) == true ? false : true;

            if (isChangeRemark)
            {
                dtProductDEF.Rows[0]["REMARK"] = txtRemark.EditValue.ToString();
            }

            bool isChangeUserLayer = dtProductDEF.Rows[0]["USELAYER"].ToString().Equals(scUseLayer.EditValue.ToString()) == true ? false : true;

            if (isChangeUserLayer)
            {
                dtProductDEF.Rows[0]["USELAYER"] = scUseLayer.EditValue.ToString();
            }

            //if (!isChangeWareHouse && !isChageRTRSheet && dtOperation.Rows.Count == 0 && dtChangeData.Rows.Count == 0 && !isChagePnlx && !isChagePnly)
            //    throw MessageException.Create("NoSaveData");

            if (!isChangeRTRSheet && dtOperation.Rows.Count == 0 && dtChangeData.Rows.Count == 0 && !isChangePnlx &&
                !isChangePnly && !isChangePCSPNL && !isChangePCSMM && !isChangeRemark && !isChangeUserLayer && !isChangePNLBLK)
            {
                throw MessageException.Create("NoSaveData");
            }

            dtProductDEF.Rows[0]["ENTERPRISEID"] = UserInfo.Current.Enterprise;
            dtProductDEF.Rows[0]["PLANTID"] = UserInfo.Current.Plant;
            dtProductDEF.Rows[0]["PROCESSDEFTYPE"] = "Main";
            dtProductDEF.Rows[0]["VERSIONSTATE"] = "Active";
            dtProductDEF.Rows[0]["VALIDSTATE"] = "Valid";
            dtProductDEF.Rows[0]["LANGUAGETYPE"] = UserInfo.Current.LanguageType;
            dtProductDEF.Rows[0]["_STATE_"] = isChangeRTRSheet || isChangePnlx || isChangePnly ? "modified" : "selected";

            DataTable dtSendOperation = new DataTable();
            dtSendOperation.Columns.Add("ENTERPRISEID");
            dtSendOperation.Columns.Add("PLANTID");
            //dtSendOperation.Columns.Add("COMPLETIONWAREHOUSEID");
            dtSendOperation.Columns.Add("VALIDSTATE");
            dtSendOperation.Columns.Add("OPERATIONID");
            dtSendOperation.Columns.Add("USERSEQUENCE");
            dtSendOperation.Columns.Add("DESCRIPTION");
            dtSendOperation.Columns.Add("PROCESSUOM");
            dtSendOperation.Columns.Add("PRODUCTDEFID");
            dtSendOperation.Columns.Add("PRODUCTDEFVERSION");
            dtSendOperation.Columns.Add("PRODUCTDEFNAME");
            dtSendOperation.Columns.Add("PRODUCTCLASSID");
            dtSendOperation.Columns.Add("PROCESSDEFTYPE");
            dtSendOperation.Columns.Add("VERSIONSTATE");
            dtSendOperation.Columns.Add("PROCESSSEGMENTID");
            dtSendOperation.Columns.Add("PROCESSSEGMENTVERSION");
            dtSendOperation.Columns.Add("OPERATIONSEQUENCE");
            dtSendOperation.Columns.Add("PATHTYPE");
            dtSendOperation.Columns.Add("_STATE_");
            dtSendOperation.Columns.Add("ISWEEKMNG");
            dtSendOperation.Columns.Add("ISSEGMENTCHANGE");

            string productDefId = dtProductDEF.Rows[0]["PRODUCTDEFID"].ToString();
            string productDefVersion = dtProductDEF.Rows[0]["PRODUCTDEFVERSION"].ToString();
            string productDefName = dtProductDEF.Rows[0]["PRODUCTDEFNAME"].ToString();
            //      dtOperation.DefaultView.Sort = "USERSEQUENCE asc";

            //    dtOperation = dtOperation.DefaultView.ToTable();

            foreach (DataRow dr in dtOperation.Rows)
            {
                dtSendOperation.Rows.Add(new object[] {
                      UserInfo.Current.Enterprise
                    , dr["PLANTID"].ToString()
                    //, spCompletionWareHouse.GetValue().ToString()
                    , "Valid"
                    , dr["OPERATIONID"].ToString()
                    , dr["USERSEQUENCE"].ToString()
                    , dr["DESCRIPTION"].ToString()
                    , dr["PROCESSUOM"].ToString()
                    , productDefId
                    , productDefVersion
                    , productDefName
                    , productDefClassId
                    , "Main"
                    , "Active"
                    , dr["PROCESSSEGMENTID"].ToString()
                    , "*"
                    , "1"
                    , "StartEnd"
                    , dr["_STATE_"].ToString()
                    , dr["ISWEEKMNG"]
                    , dr["ISSEGMENTCHANGE"]
                    });
            }

            Dictionary<string, DataTable> sendDic = new Dictionary<string, DataTable>();
            if (!grdOperation.View.IsDeletedRow(grdOperation.View.GetFocusedDataRow()))
            {
                sendDic = SaveSmartTabControl1(dtChangeData, grdOperation.View.GetFocusedDataRow());
            }

            if (sendDic.Count > 0)
            {
                MessageWorker worker = new MessageWorker("RoutingMgnt");
                worker.SetBody(new MessageBody()
                        {
                             { "routingList", dtSendOperation }
                           , { "productDefList", dtProductDEF }
                           , {  sendDic.ElementAt(0).Key,  sendDic.ElementAt(0).Value}
                        });

                worker.Execute();
            }
            else
            {
                MessageWorker worker = new MessageWorker("RoutingMgnt");
                worker.SetBody(new MessageBody()
                        {
                             { "routingList", dtSendOperation }
                           , { "productDefList", dtProductDEF }
                        });

                worker.Execute();
            }

            focusedNodeIndexList = new List<int>();

            if (treeRouting.FocusedNode != null)
            {
                RecursiveFindNodeIndex(treeRouting.FocusedNode);
            }
            else
            {
                focusedNodeIndexList = new List<int>();
            }

            lastFocusedOperationRowIndex = grdOperation.View.FocusedRowHandle;
        }

        /// <summary>
        /// 툴바 버튼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            base.OnToolbarClick(sender, e);

            if (!(sender is SmartButton btn))
            {
                return;
            }

            if (btn.Name.ToUpper().Equals("ROUTINGSEGMENTSEQUENCE"))
            {
                DataTable dtOperation = grdOperation.DataSource as DataTable;

                SetProcesssegmentSequence(dtOperation);
            }

            if (btn.Name.ToUpper().Equals("SETWAREHOUSE"))
            {
                DataTable routingDt = grdOperation.DataSource as DataTable;
                if (routingDt.Rows.Count < 1)
                {
                    return;
                }

                List<string> plantList = routingDt.AsEnumerable().Select(r => Format.GetString(r["PLANTID"])).Distinct().ToList();

                if (plantList.Count > 0)
                {
                    SetWarehousePopup pop = new SetWarehousePopup(plantList, txtProductDEFId.Text, txtProductDEFVersion.Text);
                    pop.ShowDialog();
                }
            }
        }

        #endregion 툴바

        #region 검색

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();
            treeRouting.DataSource = null;

            var values = Conditions.GetValues();
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            if (SearchProductInfo(values))
            {
                InitializeTree();
            }

            focusedNodeIndexList = new List<int>();
        }

        /// <summary>
        /// 조회조건 영역 초기화 시작
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            #region 품목

            // SelectPopup 항목 추가
            var conditionProductId = Conditions.AddSelectPopup("P_PRODUCTDEFID", new SqlQuery("GetProductDefinitionList", "10002", $"PLANTID={UserInfo.Current.Plant}"), "PRODUCTDEFID", "PRODUCTDEFID")
                                               .SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)
                                               .SetPopupLayoutForm(800, 800)
                                               .SetLabel("PRODUCTDEFID")
                                               .SetPosition(1.2)
                                               .SetValidationIsRequired()
                                               .SetPopupResultCount(1)
                                               .SetPopupApplySelection((selectRow, gridRow) =>
                                               {
                                                   List<string> productDefnameList = new List<string>();
                                                   List<string> productRevisionList = new List<string>();

                                                   selectRow.AsEnumerable().ForEach(r =>
                                                   {
                                                       productDefnameList.Add(Format.GetString(r["PRODUCTDEFNAME"]));
                                                       productRevisionList.Add(Format.GetString(r["PRODUCTDEFVERSION"]));
                                                   });

                                                   Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").EditValue = string.Join(",", productDefnameList);
                                                   Conditions.GetControl<SmartTextBox>("P_PRODUCTDEFVERSION").EditValue = string.Join(",", productRevisionList);
                                               });

            // 팝업에서 사용되는 검색조건 (품목코드/명)
            conditionProductId.Conditions.AddTextBox("TXTPRODUCTDEFNAME");
            //제품 구분 추가 2019.08.14 배선용, 노석안 대리 요청
            conditionProductId.Conditions.AddComboBox("PRODUCTDIVISION", new SqlQuery("GetTypeList", "10001", $"CODECLASSID=ProductDivision2", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                .SetEmptyItem(Language.Get("ALLVIEWS"), "", true);

            // 품목코드
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            // 품목명
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            // 품목버전
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 100);
            // 품목유형구분
            conditionProductId.GridColumns.AddComboBoxColumn("PRODUCTDEFTYPE", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductDefType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            // 생산구분
            conditionProductId.GridColumns.AddComboBoxColumn("PRODUCTIONTYPE", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            // 단위
            conditionProductId.GridColumns.AddComboBoxColumn("UNIT", 90, new SqlQuery("GetUomDefinitionList", "10001", "UOMCLASSID=Segment"), "UOMDEFNAME", "UOMDEFID");

            #endregion 품목
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").ReadOnly = true;
            Conditions.GetControl<SmartTextBox>("P_PRODUCTDEFVERSION").ReadOnly = true;

            Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").EditValueChanged += (s, e) =>
            {
                SmartSelectPopupEdit PopProdutid = s as SmartSelectPopupEdit;

                if (Format.GetFullTrimString(PopProdutid.EditValue).Equals(string.Empty))
                {
                    Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").EditValue = string.Empty;
                    Conditions.GetControl<SmartTextBox>("P_PRODUCTDEFVERSION").EditValue = string.Empty;
                }
            };
        }

        #endregion 검색

        #region 유효성 검사

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            grdOperation.View.CheckValidation();

            switch (smartTabControl1.SelectedTabPageIndex)
            {
                case 0:
                    //자재
                    grdConsumable.View.CheckValidation();
                    break;

                case 1:
                    //공정스펙
                    grdProcessSpec.View.CheckValidation();
                    break;

                case 2:
                    //치공구
                    grdDurable.View.CheckValidation();
                    break;

                case 3:
                    //외주단가000
                    grdProcessAttributeValue.View.CheckValidation();
                    break;

                case 4:
                    //AOI 층
                    grdAOIQCLayer.View.CheckValidation();
                    break;

                default:
                    break;
            }
        }

        #endregion 유효성 검사

        #region private Fuction

        /// <summary>
        /// 화면 Clear
        /// </summary>
        private void ClearData()
        {
            txtCutomerName.Text = string.Empty;
            txtProductDEFId.Text = string.Empty;
            txtProductDEFVersion.Text = string.Empty;
            //spCompletionWareHouse.EditValue = string.Empty;
            txtProductDEFName.Text = string.Empty;
            txtWorkType.Text = string.Empty;
            txtProductionType.Text = string.Empty;

            //       treeRouting.DataSource = null;
            grdOperation.DataSource = null;
            grdConsumable.DataSource = null;
            grdProcessSpec.DataSource = null;
            grdDurable.DataSource = null;
            grdProcessAttributeValue.DataSource = null;
            grdAOIQCLayer.DataSource = null;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        private bool SearchProductInfo(Dictionary<string, object> values)
        {
            ClearData();

            dtProductDEF = SqlExecuter.Query("GetProductDEFInfo", "10001", values);

            if (dtProductDEF.Rows.Count < 1)
            {
                dtProductDEF = null;
                // 조회할 데이터가 없습니다.
                ShowMessage("NoSelectData");

                return false;
            }

            dtProductDEF.Columns.Add("ENTERPRISEID");
            dtProductDEF.Columns.Add("PLANTID");
            dtProductDEF.Columns.Add("VALIDSTATE");
            dtProductDEF.Columns.Add("PROCESSDEFTYPE");
            dtProductDEF.Columns.Add("VERSIONSTATE");
            dtProductDEF.Columns.Add("_STATE_");
            dtProductDEF.Columns.Add("LANGUAGETYPE");

            spPnlX.Text = dtProductDEF.Rows[0]["PNLSIZEXAXIS"].ToString();
            spPnlY.Text = dtProductDEF.Rows[0]["PNLSIZEYAXIS"].ToString();
            spPCSPNL.Text = dtProductDEF.Rows[0]["PCSPNL"].ToString();
            spPCSMM.Text = dtProductDEF.Rows[0]["PCSMM"].ToString();
            spPCSBLK.Text = dtProductDEF.Rows[0]["PCSARY"].ToString();
            if (spPCSBLK.Text.Equals("0"))
            {
                spPCSBLK.Text = "1";
            }

            txtRemark.Text = dtProductDEF.Rows[0]["REMARK"].ToString();

            if (spPnlX.Text.Equals("") && spPnlY.Text.Equals(""))
            {
                string assemblyItemId = string.Empty;
                string assemblyItemVersion = string.Empty;
                string plantId = string.Empty;
                if (treeRouting.Nodes.Count == 0)
                {
                    assemblyItemId = values["P_PRODUCTDEFID"].ToString();
                    assemblyItemVersion = values["P_PRODUCTDEFVERSION"].ToString();
                    plantId = values["P_PLANTID"].ToString();
                }
                else
                {
                    TreeListNode node = treeRouting.Nodes[0];

                    assemblyItemId = node["ASSEMBLYITEMID"].ToString();
                    assemblyItemVersion = node["ASSEMBLYITEMVERSION"].ToString();
                    plantId = node["PLANTID"].ToString();
                }

                Dictionary<string, object> values2 = new Dictionary<string, object>
                {
                    { "ENTERPRISEID", UserInfo.Current.Enterprise },
                    { "PLANTID", plantId },
                    { "P_PRODUCTDEFID", assemblyItemId },
                    { "P_PRODUCTDEFVERSION", assemblyItemVersion },
                    { "LANGUAGETYPE", UserInfo.Current.LanguageType }
                };

                DataTable dtProductDEF2 = SqlExecuter.Query("GetProductDEFInfo", "10001", values2);

                spPnlX.Text = dtProductDEF2.Rows[0]["PNLSIZEXAXIS"].ToString();
                spPnlY.Text = dtProductDEF2.Rows[0]["PNLSIZEYAXIS"].ToString();
            }

            txtCutomerName.Text = dtProductDEF.Rows[0]["CUSTOMERNAME"].ToString();
            txtProductDEFId.Text = dtProductDEF.Rows[0]["PRODUCTDEFID"].ToString();
            txtProductDEFVersion.Text = dtProductDEF.Rows[0]["PRODUCTDEFVERSION"].ToString();
            //spCompletionWareHouse.SetValue(dtProductDEF.Rows[0]["COMPLETIONWAREHOUSEID"].ToString());
            //spCompletionWareHouse.Text = dtProductDEF.Rows[0]["WAREHOUSENAME"].ToString();
            txtProductDEFName.Text = dtProductDEF.Rows[0]["PRODUCTDEFNAME"].ToString();
            txtWorkType.Text = dtProductDEF.Rows[0]["JOBTYPENAME"].ToString();
            txtProductionType.Text = dtProductDEF.Rows[0]["PRODUCTIONTYPENAME"].ToString();
            scRTRSheet.EditValue = string.IsNullOrWhiteSpace(dtProductDEF.Rows[0]["RTRSHT"].ToString()) ? "SHT" : dtProductDEF.Rows[0]["RTRSHT"].ToString();
            scUseLayer.EditValue = string.IsNullOrWhiteSpace(dtProductDEF.Rows[0]["USELAYER"].ToString()) ? "" : dtProductDEF.Rows[0]["USELAYER"].ToString();

            DataTable dtRouting = SqlExecuter.Query("GetRoutingOperationList", "10001", values);

            grdOperation.DataSource = dtRouting;

            if (lastFocusedOperationRowIndex > -1)
            {
                if (dtRouting.Rows.Count < lastFocusedOperationRowIndex)
                {
                    lastFocusedOperationRowIndex = dtRouting.Rows.Count - 1;
                }
            }
            else
            {
                lastFocusedOperationRowIndex = 0;
            }

            grdOperation.View.FocusedRowHandle = lastFocusedOperationRowIndex;

            SearchGrid(smartTabControl1.SelectedTabPage.Tag.ToString());

            return true;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="dtChangeData"></param>
        /// <param name="operationRow"></param>
        /// <returns></returns>
        private Dictionary<string, DataTable> SaveSmartTabControl1(DataTable dtChangeData, DataRow operationRow)
        {
            Dictionary<string, DataTable> saveDic = new Dictionary<string, DataTable>();
            DataTable dtSendData = new DataTable();

            if (dtProductDEF != null)
            {
                string productDefId = dtProductDEF.Rows[0]["PRODUCTDEFID"].ToString();
                string productDefVersion = dtProductDEF.Rows[0]["PRODUCTDEFVERSION"].ToString();
                string productDefName = dtProductDEF.Rows[0]["PRODUCTDEFNAME"].ToString();
                string productDefClassId = dtProductDEF.Rows[0]["PRODUCTDEFCLASSID"].ToString();

                string key = string.Empty;
                switch (smartTabControl1.SelectedTabPageIndex)
                {
                    case 0:
                        key = "assemblyBillOfMaterialList";

                        dtSendData.Columns.Add("PRODUCTDEFID");
                        dtSendData.Columns.Add("PRODUCTDEFVERSION");
                        dtSendData.Columns.Add("PROCESSSEGMENTID");
                        dtSendData.Columns.Add("PROCESSSEGMENTVERSION");
                        dtSendData.Columns.Add("MATERIALTYPE");
                        dtSendData.Columns.Add("MATERIALDEFID");
                        dtSendData.Columns.Add("MATERIALDEFVERSION");
                        dtSendData.Columns.Add("SEQUENCE");
                        dtSendData.Columns.Add("ENTERPRISEID");
                        dtSendData.Columns.Add("PLANTID");
                        dtSendData.Columns.Add("UNIT");
                        dtSendData.Columns.Add("QTY");
                        dtSendData.Columns.Add("VALIDSTATE");
                        dtSendData.Columns.Add("WIPSUPPLYTYPE");
                        dtSendData.Columns.Add("COMPONENTBOMID");
                        dtSendData.Columns.Add("OPERATIONID");
                        dtSendData.Columns.Add("USERLAYER");
                        dtSendData.Columns.Add("ISREQUIREDMATERIAL");
                        dtSendData.Columns.Add("ISALTERABLE");
                        dtSendData.Columns.Add("PNLSIZEXAXIS");
                        dtSendData.Columns.Add("PNLSIZEYAXIS");
                        dtSendData.Columns.Add("MATERIALDETAILTYPE");
                        dtSendData.Columns.Add("INKTYPE");
                        dtSendData.Columns.Add("WORKMETHOD");
                        dtSendData.Columns.Add("DESCRIPTION");
                        dtSendData.Columns.Add("MULTIPLE");
                        dtSendData.Columns.Add("_STATE_");

                        if (dtChangeData.Rows.Count > 0)
                        {
                            var checkRows = dtChangeData.AsEnumerable().Where(s => Double.IsInfinity(Convert.ToDouble(s["COMPONENTQTY"].ToString()))).ToList();

                            if (checkRows.Count > 0)
                            {
                                throw MessageException.Create("ValidationComponentQTY", checkRows[0]["COMPONENTITEMID"].ToString());
                            }

                            foreach (DataRow dr in dtChangeData.Rows)
                            {
                                dtSendData.Rows.Add(new object[]
                                {
                                    productDefId
                                    , productDefVersion
                                    , operationRow["PROCESSSEGMENTID"].ToString()
                                    , "*"
                                    , dr["MATERIALTYPE"].ToString()
                                    , dr["COMPONENTITEMID"].ToString()
                                    , dr["COMPONENTITEMVERSION"].ToString()
                                    , string.IsNullOrWhiteSpace(dr["SEQUENCE"].ToString()) ? "0" : dr["SEQUENCE"].ToString()//SEQUENCE UI에서 가지고있는 인덱스를 찾아서 넣어야 하는지 확인 필요.
									, UserInfo.Current.Enterprise
                                    , operationRow["PLANTID"].ToString()
                                    , dr["COMPONENTUOM"].ToString()
                                    , dr["COMPONENTQTY"].ToString()
                                    , "Valid"
                                    , dr["WIPSUPPLYTYPE"].ToString()
                                    , ""
                                    , operationRow["OPERATIONID"].ToString()
                                    , dr["USERLAYER"].ToString()
                                    ,dr["ISREQUIREDMATERIAL"].ToString()
                                    ,dr["ISALTERABLE"].ToString()
                                    ,dr["VARIABLE1"].ToString()
                                    ,dr["VARIABLE2"].ToString()
                                    ,Format.GetString(dr["MATERIALDETAILTYPE"])
                                    ,Format.GetString(dr["INKTYPE"])
                                    ,Format.GetString(dr["WORKMETHOD"])
                                    ,Format.GetString(dr["DESCRIPTION"])
                                    ,Format.GetInteger(dr["MULTIPLE"])
                                    ,dr["_STATE_"].ToString()
                                 });
                            }
                        }
                        break;

                    case 1:
                        //공정스펙
                        key = "processSpecList";
                        dtSendData.Columns.Add("ENTERPRISEID");
                        dtSendData.Columns.Add("PLANTID");
                        dtSendData.Columns.Add("PRODUCTDEFID");
                        dtSendData.Columns.Add("PRODUCTDEFVERSION");
                        dtSendData.Columns.Add("PRODUCTDEFTYPE");
                        dtSendData.Columns.Add("PROCESSSEGMENTID");
                        dtSendData.Columns.Add("PROCESSSEGMENTVERSION");
                        dtSendData.Columns.Add("INSPITEMID");
                        dtSendData.Columns.Add("INSPITEMNAME");
                        dtSendData.Columns.Add("INSPITEMVERSION");
                        dtSendData.Columns.Add("INSPECTIONDEFID");
                        dtSendData.Columns.Add("INSPECTIONDEFVERSION");
                        dtSendData.Columns.Add("SPECCLASSID");
                        dtSendData.Columns.Add("SPECSEQUENCE");
                        dtSendData.Columns.Add("LOCATION");
                        dtSendData.Columns.Add("DEFAULTCHARTTYPE");
                        dtSendData.Columns.Add("LCL");
                        dtSendData.Columns.Add("CL");
                        dtSendData.Columns.Add("UCL");
                        dtSendData.Columns.Add("LSL");
                        dtSendData.Columns.Add("SL");
                        dtSendData.Columns.Add("USL");
                        dtSendData.Columns.Add("LOL");
                        dtSendData.Columns.Add("UOL");
                        dtSendData.Columns.Add("VALIDSTATE");
                        dtSendData.Columns.Add("RESOURCETYPE");
                        dtSendData.Columns.Add("_STATE_");

                        if (dtChangeData.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dtChangeData.Rows)
                            {
                                dtSendData.Rows.Add(new object[]
                                {
                                       UserInfo.Current.Enterprise
                                     , operationRow["PLANTID"].ToString()
                                     , productDefId
                                     , productDefVersion
                                     , productDefClassId
                                     , operationRow["PROCESSSEGMENTID"].ToString()
                                     , "*"
                                     , dr["INSPITEMID"].ToString()
                                     , dr["INSPITEMNAME"].ToString()
                                     , dr["INSPITEMVERSION"].ToString()
                                     , dr["INSPECTIONDEFID"].ToString()
                                     , dr["INSPECTIONDEFVERSION"].ToString()
                                     , dr["SPECCLASSID"].ToString()
                                     , dr["SPECSEQUENCE"].ToString()
                                     , dr["LOCATION"].ToString()
                                     , smcXBarChartInfo.Editor.EditValue.ToString()
                                     , dr["LCL"].ToString()
                                     , dr["CL"].ToString()
                                     , dr["UCL"].ToString()
                                     , dr["LSL"].ToString()
                                     , dr["SL"].ToString()
                                     , dr["USL"].ToString()
                                     , dr["LOL"].ToString()
                                     , dr["UOL"].ToString()
                                     , "Valid"
                                     , dr["RESOURCETYPE"].ToString()
                                     , dr["_STATE_"].ToString()
                                });
                            }
                        }

                        break;

                    case 2:
                        //치공구
                        key = "billOfDurableList";

                        dtSendData.Columns.Add("OPERATIONID");
                        dtSendData.Columns.Add("PRODUCTDEFID");
                        dtSendData.Columns.Add("PRODUCTDEFVERSION");
                        dtSendData.Columns.Add("PROCESSDEFID");
                        dtSendData.Columns.Add("PROCESSDEFVERSION");
                        dtSendData.Columns.Add("PROCESSSEGMENTID");
                        dtSendData.Columns.Add("PROCESSSEGMENTVERSION");
                        dtSendData.Columns.Add("RESOURCETYPE");
                        dtSendData.Columns.Add("DURABLETYPE");
                        dtSendData.Columns.Add("SEQUENCE");
                        dtSendData.Columns.Add("RESOURCEID");
                        dtSendData.Columns.Add("RESOURCEVERSION");
                        dtSendData.Columns.Add("EQUIPMENTID");
                        dtSendData.Columns.Add("RESOURCECLASSID");
                        dtSendData.Columns.Add("ISPRIMARYRESOURCE");
                        dtSendData.Columns.Add("ENTERPRISEID");
                        dtSendData.Columns.Add("PLANTID");
                        dtSendData.Columns.Add("DESCRIPTION");
                        dtSendData.Columns.Add("VALIDSTATE");
                        dtSendData.Columns.Add("FILMUSELAYER1");
                        dtSendData.Columns.Add("_STATE_");
                        dtSendData.Columns.Add("WRAPTYPE");

                        if (dtChangeData.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dtChangeData.Rows)
                            {
                                dtSendData.Rows.Add(new object[]
                                {
                                      operationRow["OPERATIONID"].ToString()
                                    , productDefId
                                    , productDefVersion
                                    , productDefId
                                    , productDefVersion
                                    , operationRow["PROCESSSEGMENTID"].ToString()
                                    , "*"
                                    , "Durable"
                                    , dr["DURABLETYPE"].ToString()
                                    ,  string.IsNullOrWhiteSpace(dr["SEQUENCE"].ToString()) == true ? "0" : dr["SEQUENCE"].ToString()//SEQUENCE UI에서 가지고있는 인덱스를 찾아서 넣어야 하는지 확인 필요.
                                    , dr["TOOLCODE"].ToString()
                                    , dr["TOOLVERSION"].ToString()
                                    , "*"
                                    , dr["RESOURCECLASSID"].ToString()
                                    , "Y"
                                    , UserInfo.Current.Enterprise
                                    , operationRow["PLANTID"].ToString()
                                    , dr["DESCRIPTION"].ToString()
                                    , "Valid"
                                    , dr["FILMUSELAYER1"].ToString()
                                    ,dr["_STATE_"].ToString()
                                    ,dr["WRAPTYPE"]
                                });
                            }
                        }

                        break;

                    case 3:
                        //외주단가
                        dtChangeData = grdProcessAttributeValue.GetChangedRows();
                        break;

                    case 4:
                        //AOI 층
                        key = "aoiList";

                        dtChangeData = grdAOIQCLayer.GetChangedRows();

                        dtSendData.Columns.Add("OPERATIONID");
                        dtSendData.Columns.Add("ENTERPRISEID");
                        dtSendData.Columns.Add("PLANTID");
                        dtSendData.Columns.Add("PROCESSSEGMENTID");
                        dtSendData.Columns.Add("INSPECTIONDEFID");
                        dtSendData.Columns.Add("AOIQCLAYER");
                        dtSendData.Columns.Add("INSPITEMID");
                        dtSendData.Columns.Add("DESCRIPTION");
                        dtSendData.Columns.Add("SEQUENCE");
                        dtSendData.Columns.Add("VALIDSTATE");
                        dtSendData.Columns.Add("AOIQCLAYER2");
                        dtSendData.Columns.Add("_STATE_");

                        if (dtChangeData.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dtChangeData.Rows)
                            {
                                dtSendData.Rows.Add(new object[]
                                {
                                      operationRow["OPERATIONID"].ToString()
                                    , UserInfo.Current.Enterprise
                                    , operationRow["PLANTID"].ToString()
                                    , operationRow["PROCESSSEGMENTID"].ToString()
                                    , ""
                                    , dr["AOIQCLAYER"].ToString()
                                    , ""
                                    , dr["DESCRIPTION"].ToString()
                                    , dr["SEQUENCE"].ToString()
                                    , "Valid"
                                    , dr["AOIQCLAYER2"].ToString()
                                    , dr["_STATE_"].ToString()
                                });
                            }
                        }

                        break;

                    default:
                        break;
                }
                saveDic.Add(key, dtSendData);
            }

            return saveDic;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="dtOperation"></param>
        private void SetProcesssegmentSequence(DataTable dtOperation)
        {
            int sequence = 0;

            grdOperation.View.PostEditor();

            DataTable changeTable = dtOperation.AsEnumerable().OrderBy(s => Convert.ToInt32(s["USERSEQUENCE"])).CopyToDataTable();

            for (int i = 0; i < changeTable.Rows.Count; i++)
            {
                changeTable.Rows[i]["USERSEQUENCE"] = sequence += 10;
                dtOperation.Rows[i].ItemArray = changeTable.Rows[i].ItemArray;
            }

            grdOperation.View.UpdateCurrentRow();
            grdOperation.View.RaiseValidateRow();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="query"></param>
        /// <param name="version"></param>
        /// <param name="param"></param>
        /// <param name="addColumns"></param>
        /// <returns></returns>
        private DataTable SearchGrid(SmartBandedGrid grid, string query, string version, Dictionary<string, object> param, params string[] addColumns)
        {
            grid.View.ClearDatas();

            DataTable dt = SqlExecuter.Query(query, version, param);

            grid.DataSource = dt;

            return null;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="pageName"></param>
        private void SearchGrid(string pageName)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();

            if (dtProductDEF != null && grdOperation.View.RowCount > 0)
            {
                string productDefId = dtProductDEF.Rows[0]["PRODUCTDEFID"].ToString();
                string productDefVersion = dtProductDEF.Rows[0]["PRODUCTDEFVERSION"].ToString();

                switch (pageName)
                {
                    case "Consumable":
                        //자재
                        param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                        param.Add("OPERATIONID", grdOperation.View.GetFocusedDataRow()["OPERATIONID"].ToString());
                        param.Add("PRODUCTDEFID", productDefId);
                        param.Add("PROCESSDEFVERSION", productDefVersion);

                        SearchGrid(grdConsumable, "GetRoutingBomList", "10001", param);

                        break;

                    case "OperationSpec":
                        //공정스펙
                        param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                        param.Add("RESOURCEID", productDefId);
                        param.Add("RESOURCEVERSION", productDefVersion);
                        param.Add("PROCESSSEGMENTID", grdOperation.View.GetFocusedDataRow()["PROCESSSEGMENTID"].ToString());
                        param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                        param.Add("SPECCLASSID", "OperationSpec");
                        SearchGrid(grdProcessSpec, "GetRoutingInspectionItemList", "10002", param);

                        DataTable dtProcessSpec = grdProcessSpec.DataSource as DataTable;

                        if (dtProcessSpec != null)
                        {
                            if (dtProcessSpec.Rows.Count == 0)
                            {
                                smcXBarChartInfo.Editor.EditValue = "XBARR";
                                smcXBarChartInfo.Editor.ReadOnly = false;
                            }
                            else
                            {
                                DataRow chartRow = dtProcessSpec.AsEnumerable().Where(s => !string.IsNullOrWhiteSpace(s["DEFAULTCHARTTYPE"].ToString())).FirstOrDefault();

                                if (chartRow == null)
                                {
                                    smcXBarChartInfo.Editor.EditValue = "XBARR";
                                    smcXBarChartInfo.Editor.ReadOnly = false;
                                }
                                else
                                {
                                    smcXBarChartInfo.Editor.EditValue = chartRow["DEFAULTCHARTTYPE"].ToString();
                                    smcXBarChartInfo.Editor.ReadOnly = true;
                                }
                            }
                        }
                        else
                        {
                            smcXBarChartInfo.Editor.EditValue = "XBARR";
                            smcXBarChartInfo.Editor.ReadOnly = false;
                        }

                        break;

                    case "Durable":
                        //치공구
                        param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                        param.Add("OPERATIONID", grdOperation.View.GetFocusedDataRow()["OPERATIONID"].ToString());
                        param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                        param.Add("PRODUCTDEFID", productDefId);
                        param.Add("PRODUCTDEFVERSION", productDefVersion);
                        SearchGrid(grdDurable, "GetRoutingDurableList", "10001", param);

                        break;

                    case "ProcessAttributeValue":
                        //외주단가
                        param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                        param.Add("PLANTID", grdOperation.View.GetFocusedDataRow()["PLANTID"].ToString());
                        param.Add("ITEMID", dtProductDEF.Rows[0]["PRODUCTDEFID"].ToString());
                        param.Add("ITEMVERSION", dtProductDEF.Rows[0]["PRODUCTDEFVERSION"].ToString());
                        param.Add("USERSEQUENCE", grdOperation.View.GetFocusedDataRow()["USERSEQUENCE"].ToString());
                        param.Add("PROCESSSEGMENTID", grdOperation.View.GetFocusedDataRow()["PROCESSSEGMENTID"].ToString());
                        param.Add("ATTRIBUTECLASS", "");

                        SearchGrid(grdProcessAttributeValue, "GetProcessAttributeValueList", "10001", param);

                        break;

                    case "AOILayer":
                        //AOI 층
                        param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                        param.Add("PLANTID", grdOperation.View.GetFocusedDataRow()["PLANTID"].ToString());
                        param.Add("OPERATIONID", grdOperation.View.GetFocusedDataRow()["OPERATIONID"].ToString());

                        SearchGrid(grdAOIQCLayer, "GetOperationspecvalue", "10001", param);

                        break;

                    default:
                        break;
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="tNode"></param>
        private void RecursiveFindNodeIndex(TreeListNode tNode)
        {
            if (tNode.ParentNode != null)
            {
                RecursiveFindNodeIndex(tNode.ParentNode);
            }

            focusedNodeIndexList.Add(treeRouting.GetNodeIndex(tNode));
        }

        #endregion private Fuction
    }
}