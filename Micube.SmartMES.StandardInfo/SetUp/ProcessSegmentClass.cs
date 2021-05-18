#region using

using DevExpress.XtraTreeList.Nodes;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Validations;
using Micube.SmartMES.Commons.Controls;
using System.Collections.Generic;
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
    ///
    ///
    /// </summary>
    public partial class ProcessSegmentClass : SmartConditionManualBaseForm
    {
        #region Local Variables

        /// <summary>
        /// Tree에서 선택한 공정
        /// </summary>
        private string _focusProcessSegmentClass = "";

        /// <summary>
        /// 선택한 표준공정
        /// </summary>
        private string _focusProcessSegment = "";

        #endregion Local Variables

        #region 생성자

        public ProcessSegmentClass()
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
            base.InitializeContent();

            InitializeLanguageKey();
            InitializeControls();
            InitializeTreeArea();
            InitializeGrid();
            InitializeEvent();
        }

        /// <summary>
        /// 다국어 처리
        /// </summary>
        private void InitializeLanguageKey()
        {
            tabProcessSegment.SetLanguageKey(xtraTabPage1, "PROCESSSEGMENTEXTREGIST");
            tabProcessSegment.SetLanguageKey(xtraTabPage2, "PROCESSSEGMENTEXTSTATUS");
        }

        /// <summary>
        /// Control 초기화
        /// </summary>
        private void InitializeControls()
        {
            smartGroupBox1.GridButtonItem = GridButtonItem.Refresh;
        }

        /// <summary>
        /// Area Tree 초기화
        /// </summary>
        private void InitializeTreeArea()
        {
            // 대공정/중공정을 트리로 보여준다.
            treeProcesssegment.SetResultCount(1);
            treeProcesssegment.SetIsReadOnly();
            //루트에 회사코드 등록
            treeProcesssegment.SetEmptyRoot(UserInfo.Current.Enterprise, "");
            // 공정그룹명/공정그룹/모공정그룹
            treeProcesssegment.SetMember("PROCESSSEGMENTCLASSNAME", "PROCESSSEGMENTCLASSID", "PARENTPROCESSSEGMENTCLASSID");

            Dictionary<string, object> param = new Dictionary<string, object>
            {
                { "LANGUAGETYPE", UserInfo.Current.LanguageType },
                { "ENTERPRISEID", UserInfo.Current.Enterprise }
            };

            param.Add("VALIDSTATE", Conditions.GetValues()["P_VALIDSTATE"]);

            treeProcesssegment.DataSource = SqlExecuter.Query("GetProcessSegmentClass", "10003", param);
            treeProcesssegment.PopulateColumns();
            treeProcesssegment.ExpandAll();
        }

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            #region 공정그룹 Grid

            grdProcessSegmentClass.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export;
            grdProcessSegmentClass.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;

            grdProcessSegmentClass.View.AddTextBoxColumn("ENTERPRISEID").SetIsHidden();
            grdProcessSegmentClass.View.AddTextBoxColumn("PLANTID").SetIsHidden();

            grdProcessSegmentClass.View.AddComboBoxColumn("PROCESSSEGMENTCLASSTYPE", 110, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProcessSegmentClassType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                  .SetIsReadOnly()
                                  .SetTextAlignment(TextAlignment.Center);

            grdProcessSegmentClass.View.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 110).SetValidationIsRequired().SetTextAlignment(TextAlignment.Center);
            grdProcessSegmentClass.View.AddLanguageColumn("PROCESSSEGMENTCLASSNAME", 160);
            grdProcessSegmentClass.View.AddTextBoxColumn("PARENTPROCESSSEGMENTCLASSID", 90);
            grdProcessSegmentClass.View.AddComboBoxColumn("SUBMATERIALTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=SubMaterialDivision", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdProcessSegmentClass.View.AddComboBoxColumn("ISSUBMATERIALROUTING", 150, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdProcessSegmentClass.View.AddComboBoxColumn("ISSPECCHANGE", 150, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdProcessSegmentClass.View.AddTextBoxColumn("PARENTPROCESSSEGMENTCLASSNAME", 150).SetIsReadOnly().SetIsHidden();
            grdProcessSegmentClass.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsHidden();
            grdProcessSegmentClass.View.AddTextBoxColumn("CREATOR", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdProcessSegmentClass.View.AddTextBoxColumn("CREATEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdProcessSegmentClass.View.AddTextBoxColumn("MODIFIER", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdProcessSegmentClass.View.AddTextBoxColumn("MODIFIEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);

            grdProcessSegmentClass.View.PopulateColumns();

            #endregion 공정그룹 Grid

            #region 표준공정

            grdProcessSegment.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export;
            grdProcessSegment.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;

            grdProcessSegment.View.AddTextBoxColumn("ENTERPRISEID", 0).SetIsHidden().SetDefault(UserInfo.Current.Enterprise);
            grdProcessSegment.View.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 110).SetTextAlignment(TextAlignment.Center);
            grdProcessSegment.View.AddTextBoxColumn("STANDARDOPERATIONID", 100).SetTextAlignment(TextAlignment.Center);
            grdProcessSegment.View.AddLanguageColumn("PROCESSSEGMENTNAME", 160);                                                                                                                   // 공정명(다국어)

            #region 인시당 집계그룹 ID

            var control = grdProcessSegment.View.AddSelectPopupColumn("PROCESSGROUPID", 100, new SqlQuery("GetProcessGroupList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                           .SetPopupLayout("SELECTPROCESSGROUPID", PopupButtonStyles.Ok_Cancel, true, true)
                                           .SetPopupResultCount(1)
                                           .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow)
                                           .SetPopupAutoFillColumns("PROCESSGROUPNAME")
                                           .SetPopupApplySelection((selectedRows, dataGridRows) =>
                                           {
                                               DataRow selectedRow = selectedRows.AsEnumerable().FirstOrDefault();
                                               dataGridRows["PROCESSGROUPNAME"] =
                                                   selectedRow == null ? "" : Format.GetString(selectedRow["PROCESSGROUPNAME"]);
                                           });

            control.Conditions.AddTextBox("TXTPROCESSGROUP");

            control.GridColumns.AddTextBoxColumn("PROCESSGROUPID", 150);
            control.GridColumns.AddTextBoxColumn("PROCESSGROUPNAME", 200);

            #endregion 인시당 집계그룹 ID

            grdProcessSegment.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;

            grdProcessSegment.View.AddTextBoxColumn("PROCESSGROUPNAME", 90).SetIsReadOnly();
            grdProcessSegment.View.AddComboBoxColumn("SEGMENTTYPE", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProcessSegmentType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetTextAlignment(TextAlignment.Center);
            grdProcessSegment.View.AddComboBoxColumn("STEPCLASS", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=StepType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetTextAlignment(TextAlignment.Center);
            grdProcessSegment.View.AddTextBoxColumn("STEPTYPE", 250).SetIsHidden();
            grdProcessSegment.View.AddTextBoxColumn("STEPTYPENAME", 200).SetLabel("STEPTYPE").SetTextAlignment(TextAlignment.Center);
            grdProcessSegment.View.AddComboBoxColumn("SUBSEGMENTID", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=Subsegmentid1", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                             .SetValidationIsRequired()
                             .SetTextAlignment(TextAlignment.Center);

            grdProcessSegment.View.AddComboBoxColumn("ISTOOLWORK", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetTextAlignment(TextAlignment.Center);
            grdProcessSegment.View.AddComboBoxColumn("ISINCOMINGINSPECTION", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetTextAlignment(TextAlignment.Center);
            grdProcessSegment.View.AddComboBoxColumn("SUBSEGMENTID1", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OutsourcingSpecType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                             .SetEmptyItem("", "")
                             .SetLabel("OUTSOURCINGSPECTYPE")
                             .SetTextAlignment(TextAlignment.Center);

            grdProcessSegment.View.AddComboBoxColumn("PLATINGTYPE", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=PlatingType2", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                             .SetEmptyItem("", "")
                             .SetTextAlignment(TextAlignment.Center);

            grdProcessSegment.View.AddComboBoxColumn("ISREQUIREDMATERIAL", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetLabel("ISREQUIREDMATERIAL2")
                             .SetTextAlignment(TextAlignment.Center)
                             .SetEmptyItem("", "");

            grdProcessSegment.View.AddComboBoxColumn("ISREQUIREDOPERATIONSPEC", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                             .SetTextAlignment(TextAlignment.Center)
                             .SetEmptyItem("", "");

            grdProcessSegment.View.AddComboBoxColumn("ISREQUIREDTOOL", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                             .SetTextAlignment(TextAlignment.Center)
                             .SetEmptyItem("", "");

            grdProcessSegment.View.AddTextBoxColumn("COSTCODE", 90).SetTextAlignment(TextAlignment.Center);
            grdProcessSegment.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdProcessSegment.View.AddTextBoxColumn("CREATOR", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdProcessSegment.View.AddTextBoxColumn("CREATEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdProcessSegment.View.AddTextBoxColumn("MODIFIER", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdProcessSegment.View.AddTextBoxColumn("MODIFIEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdProcessSegment.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 0).SetIsHidden();

            grdProcessSegment.View.PopulateColumns();

            #endregion 표준공정

            #region 설비그룹

            grdProSegMEC.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export;
            grdProSegMEC.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;

            grdProSegMEC.View.AddTextBoxColumn("PROCESSSEGMENTID");
            grdProSegMEC.View.AddTextBoxColumn("ENTERPRISEID").SetIsHidden();
            grdProSegMEC.View.AddTextBoxColumn("PLANTID").SetIsHidden();
            grdProSegMEC.View.AddTextBoxColumn("EQPTSEQUENCE").SetIsHidden();

            #region 설비그룹ID

            control = grdProSegMEC.View.AddSelectPopupColumn("EQUIPMENTCLASSID", new SqlQuery("SelectEquipMentClass", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                  .SetPopupLayout("SELECTEQUIPMENTCLASS", PopupButtonStyles.Ok_Cancel, true, false)
                                  .SetPopupResultCount(0)
                                  .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow)
                                  .SetPopupValidationCustom(ValidationEquipmentClassIdPopup)
                                  .SetPopupApplySelection((selectedRows, dataGridRow) =>
                                  {
                                      DataTable dt2 = grdProSegMEC.DataSource as DataTable;
                                      int handle = grdProSegMEC.View.FocusedRowHandle;
                                      DataRow dr = grdProcessSegment.View.GetFocusedDataRow();

                                      dt2.Rows.RemoveAt(handle);
                                      foreach (DataRow row in selectedRows)
                                      {
                                          DataRow newrow = dt2.NewRow();

                                          object obj = grdProSegMEC.View.DataSource;
                                          DataTable dtPs = ((System.Data.DataView)obj).Table;

                                          DataRow[] row2 = dtPs.Select("1=1", "EQPTSEQUENCE DESC");

                                          newrow["EQUIPMENTCLASSID"] = row["EQUIPMENTCLASSID"];
                                          newrow["EQUIPMENTCLASSNAME"] = row["EQUIPMENTCLASSNAME"];
                                          newrow["PROCESSSEGMENTID"] = dr["STANDARDOPERATIONID"];
                                          newrow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                                          newrow["EQPTSEQUENCE"] = 0;
                                          if (row2.Length == 0)
                                          {
                                              newrow["EQPTSEQUENCE"] = 1;
                                          }
                                          else
                                          {
                                              newrow["EQPTSEQUENCE"] = int.Parse(row2[0]["EQPTSEQUENCE"].ToString()) + 1;
                                          }

                                          newrow["VALIDSTATE"] = "Valid";

                                          dt2.Rows.Add(newrow);
                                      }
                                  });

            control.Conditions.AddTextBox("EQUIPMENTCLASSID");
            control.Conditions.AddTextBox("EQUIPMENTCLASSNAME");

            control.GridColumns.AddTextBoxColumn("EQUIPMENTCLASSID", 100);
            control.GridColumns.AddTextBoxColumn("EQUIPMENTCLASSNAME", 150);
            control.GridColumns.AddTextBoxColumn("TOPEQUIPMENTCLASS", 150);
            control.GridColumns.AddTextBoxColumn("MIDDLEEQUIPMENTCLASS", 150);

            #endregion 설비그룹ID

            grdProSegMEC.View.AddTextBoxColumn("EQUIPMENTCLASSNAME", 250);
            grdProSegMEC.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdProSegMEC.View.AddTextBoxColumn("CREATOR", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdProSegMEC.View.AddTextBoxColumn("CREATEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdProSegMEC.View.AddTextBoxColumn("MODIFIER", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdProSegMEC.View.AddTextBoxColumn("MODIFIEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);

            grdProSegMEC.View.PopulateColumns();

            #endregion 설비그룹

            #region Spec 항목

            grdSpecattribute.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export;
            grdSpecattribute.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;

            grdSpecattribute.View.AddTextBoxColumn("PROCESSSEGMENTID").SetIsHidden();
            grdSpecattribute.View.AddTextBoxColumn("ENTERPRISEID").SetIsHidden();
            grdSpecattribute.View.AddTextBoxColumn("PLANTID").SetIsHidden();
            grdSpecattribute.View.AddTextBoxColumn("SPECSEQUENCE").SetIsHidden();
            grdSpecattribute.View.AddTextBoxColumn("INSPECTIONDEFID", 140).SetIsReadOnly();
            grdSpecattribute.View.AddComboBoxColumn("INSPITEMCLASSID", 140, new SqlQuery("GetInspitemClassCombo", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "INSPITEMCLASSNAME", "INSPITEMCLASSID");

            #region 검사항목

            control = grdSpecattribute.View.AddSelectPopupColumn("INSPITEMID", new SqlQuery("GetInspItemPupop", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}"
                                                                                                                       , $"LANGUAGETYPE={UserInfo.Current.LanguageType}"
                                                                                                                       , "INSPECTIONCLASSID=OperationInspection"))
                                      .SetPopupLayout("INSPITEMID", PopupButtonStyles.Ok_Cancel, true, false)
                                      .SetPopupResultCount(1)
                                      .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow)
                                      .SetValidationIsRequired()
                                      .SetPopupValidationCustom(ValidationSpitemPopup);

            control.Conditions.AddTextBox("INSPITEMID");
            control.Conditions.AddTextBox("INSPITEMNAME");
            control.Conditions.AddTextBox("INSPITEMCLASSID").SetPopupDefaultByGridColumnId("INSPITEMCLASSID").SetIsHidden();

            control.GridColumns.AddTextBoxColumn("INSPITEMID", 150);
            control.GridColumns.AddTextBoxColumn("INSPITEMNAME", 200);

            #endregion 검사항목

            grdSpecattribute.View.AddTextBoxColumn("INSPITEMNAME", 140).SetIsReadOnly();
            grdSpecattribute.View.AddComboBoxColumn("DEFAULTDISPLAY", 110, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                            .SetDefault("N")
                            .SetTextAlignment(TextAlignment.Center);

            grdSpecattribute.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                            .SetTextAlignment(TextAlignment.Center);

            grdSpecattribute.View.AddTextBoxColumn("CREATOR", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdSpecattribute.View.AddTextBoxColumn("CREATEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdSpecattribute.View.AddTextBoxColumn("MODIFIER", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdSpecattribute.View.AddTextBoxColumn("MODIFIEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);

            grdSpecattribute.View.PopulateColumns();

            #endregion Spec 항목

            #region 공정현황 - 표준공정 리스트

            grdprocesslist.GridButtonItem = GridButtonItem.Export;
            grdprocesslist.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;

            grdprocesslist.View.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 0).SetIsHidden();
            grdprocesslist.View.AddTextBoxColumn("STANDARDOPERATIONID", 100).SetTextAlignment(TextAlignment.Center);
            grdprocesslist.View.AddLanguageColumn("PROCESSSEGMENTNAME", 160);
            grdprocesslist.View.AddTextBoxColumn("TOPPROCESSSEGMENTCLASS", 80);
            grdprocesslist.View.AddTextBoxColumn("MIDDLEPROCESSSEGMENTCLASS", 80);
            grdprocesslist.View.AddComboBoxColumn("SEGMENTTYPE", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProcessSegmentType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                          .SetTextAlignment(TextAlignment.Center);

            grdprocesslist.View.AddComboBoxColumn("STEPCLASS", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=StepType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                          .SetTextAlignment(TextAlignment.Center);

            grdprocesslist.View.AddTextBoxColumn("STEPTYPE", 160);
            grdprocesslist.View.AddTextBoxColumn("PROCESSGROUPNAME", 160);
            grdprocesslist.View.AddComboBoxColumn("SUBSEGMENTID", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=Subsegmentid1", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                          .SetValidationIsRequired()
                          .SetTextAlignment(TextAlignment.Center);

            grdprocesslist.View.AddComboBoxColumn("ISTOOLWORK", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                          .SetTextAlignment(TextAlignment.Center);

            grdprocesslist.View.AddComboBoxColumn("ISINCOMINGINSPECTION", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                          .SetTextAlignment(TextAlignment.Center);

            grdprocesslist.View.AddComboBoxColumn("SUBSEGMENTID1", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OutsourcingSpecType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                          .SetEmptyItem("", "")
                          .SetLabel("OUTSOURCINGSPECTYPE")
                          .SetTextAlignment(TextAlignment.Center);

            grdprocesslist.View.AddComboBoxColumn("PLATINGTYPE", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=PlatingType2", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                          .SetEmptyItem("", "")
                          .SetTextAlignment(TextAlignment.Center);

            grdprocesslist.View.AddComboBoxColumn("ISREQUIREDMATERIAL", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                          .SetLabel("ISREQUIREDMATERIAL2")
                          .SetTextAlignment(TextAlignment.Center);

            grdprocesslist.View.AddComboBoxColumn("ISREQUIREDOPERATIONSPEC", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                          .SetTextAlignment(TextAlignment.Center);

            grdprocesslist.View.AddComboBoxColumn("ISREQUIREDTOOL", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                          .SetTextAlignment(TextAlignment.Center);

            grdprocesslist.View.AddComboBoxColumn("ISSPECCHANGE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                          .SetTextAlignment(TextAlignment.Center);

            grdprocesslist.View.AddTextBoxColumn("COSTCODE", 90)
                          .SetTextAlignment(TextAlignment.Center);

            grdprocesslist.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                          .SetTextAlignment(TextAlignment.Center);

            grdprocesslist.View.AddTextBoxColumn("CREATOR", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdprocesslist.View.AddTextBoxColumn("CREATEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdprocesslist.View.AddTextBoxColumn("MODIFIER", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdprocesslist.View.AddTextBoxColumn("MODIFIEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdprocesslist.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 0).SetIsHidden();

            grdprocesslist.View.PopulateColumns();

            grdprocesslist.View.SetIsReadOnly();

            #endregion 공정현황 - 표준공정 리스트
        }

        #endregion 컨텐츠 영역 초기화

        #region 이벤트

        /// <summary>
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            // 탭
            tabProcessSegment.SelectedPageChanged += (s, e) =>
            {
                var buttons = pnlToolbar.Controls.Find<SmartButton>(true);
                if (e.Page.Name.Equals("xtraTabPage1"))
                {
                    foreach (SmartButton button in buttons)
                    {
                        if (button.Name.Equals("Save"))
                        {
                            button.Visible = true;
                        }
                    }
                }
                else
                {
                    foreach (SmartButton button in buttons)
                    {
                        if (button.Name.Equals("Save"))
                            button.Visible = false;
                    }
                }
            };

            tabProcessSegment.SelectedPageChanging += (s, e) =>
            {
                DataTable dtProcessSegClassChanged = grdProcessSegmentClass.GetChangedRows();
                DataTable dtProcessSegChanged = grdProcessSegment.GetChangedRows();
                DataTable dtEquipmentClassChanged = grdProSegMEC.GetChangedRows();
                DataTable dtSpecAttributeChanged = grdSpecattribute.GetChangedRows();

                if (tabProcessSegment.SelectedTabPageIndex.Equals(0))
                {
                    if (dtProcessSegClassChanged.Rows.Count > 0 || dtProcessSegChanged.Rows.Count > 0 ||
                        dtEquipmentClassChanged.Rows.Count > 0 || dtSpecAttributeChanged.Rows.Count > 0)
                    {
                        if (ShowMessageBox("WRITTENBEDELETE", Language.Get("MESSAGEINFO"), MessageBoxButtons.OKCancel).Equals(DialogResult.Cancel))
                        {
                            e.Cancel = true;
                        }
                        else
                        {
                            DataRow focusRow = treeProcesssegment.GetFocusedDataRow();
                            if (focusRow == null)
                            {
                                return;
                            }

                            int prevHandle = grdProcessSegmentClass.View.FocusedRowHandle;

                            Dictionary<string, object> param = new Dictionary<string, object>
                            {
                                { "LANGUAGETYPE", UserInfo.Current.LanguageType },
                                { "ENTERPRISEID", UserInfo.Current.Enterprise },
                                { "PARENTPROCESSSEGMENTCLASSID", focusRow["PROCESSSEGMENTCLASSID"] },
                                { "VALIDSTATE", Conditions.GetValues()["P_VALIDSTATE"] }
                            };

                            if (SqlExecuter.Query("GetProcessSegmentClass", "10002", param) is DataTable dtProcessClass)
                            {
                                grdProcessSegmentClass.DataSource = dtProcessClass;

                                if (dtProcessClass.Rows.Count > 0)
                                {
                                    DataRow classRow = prevHandle.Equals(0) ? grdProcessSegmentClass.View.GetDataRow(0) : grdProcessSegmentClass.View.GetDataRow(prevHandle);
                                    int segmentPrevHandle = grdProcessSegment.View.FocusedRowHandle;

                                    param.Add("PROCESSSEGMENTCLASSID", classRow["PROCESSSEGMENTCLASSID"]);

                                    if (SqlExecuter.Query("GetProcessSegmentExtlist", "10001", param) is DataTable dtSegmentList)
                                    {
                                        grdProcessSegment.DataSource = dtSegmentList;

                                        if (dtSegmentList.Rows.Count > 0)
                                        {
                                            DataRow segmentRow = segmentPrevHandle.Equals(0) ? grdProcessSegment.View.GetDataRow(0) : grdProcessSegment.View.GetDataRow(segmentPrevHandle);

                                            param.Add("PLANTID", UserInfo.Current.Plant);
                                            param.Add("PROCESSSEGMENTID", classRow["STANDARDOPERATIONID"]);

                                            // 설비
                                            grdProSegMEC.DataSource = SqlExecuter.Query("GetProcessSegMentEqptClasslist", "10001", param);

                                            // 스펙
                                            grdSpecattribute.DataSource = SqlExecuter.Query("GetSpecAttributelist", "10001", param);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };

            grdProcessSegmentClass.View.AddingNewRow += (s, e) =>
            {
                DataRow focusRow = treeProcesssegment.GetFocusedDataRow();

                if (focusRow["PROCESSSEGMENTCLASSID"].ToString() == "")
                {
                    e.NewRow["PROCESSSEGMENTCLASSTYPE"] = "TopProcessSegmentClass";
                    e.NewRow["PARENTPROCESSSEGMENTCLASSID"] = "";
                }
                else
                {
                    e.NewRow["PROCESSSEGMENTCLASSTYPE"] = "MiddleProcessSegmentClass";
                    e.NewRow["PARENTPROCESSSEGMENTCLASSID"] = focusRow["PROCESSSEGMENTCLASSID"].ToString();
                }

                e.NewRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                e.NewRow["SUBMATERIALTYPE"] = "None";
                e.NewRow["ISSUBMATERIALROUTING"] = "N";
                e.NewRow["ISSPECCHANGE"] = "N";
                e.NewRow["VALIDSTATE"] = "Valid";
            };

            grdProcessSegmentClass.View.ColumnFilterChanged += (s, e) =>
            {
                if (!string.IsNullOrWhiteSpace(grdProcessSegmentClass.View.ActiveFilterString))
                {
                    int rowHandle = grdProcessSegmentClass.View.FocusedRowHandle;
                    grdProcessSegmentClass_FocusedRowChanged();
                }
            };

            //표준공정 그리드
            grdProcessSegment.View.AddingNewRow += (s, e) =>
            {
                DataTable dtChk = grdProSegMEC.GetChangedRows();
                if (dtChk.Rows.Count != 0)
                {
                    e.IsCancel = true;
                }

                DataRow row = grdProcessSegmentClass.View.GetFocusedDataRow();

                e.NewRow["PROCESSSEGMENTCLASSID"] = row["PROCESSSEGMENTCLASSID"];
                e.NewRow["VALIDSTATE"] = "Valid";
            };

            grdProcessSegment.View.ShownEditor += (s, e) =>
            {
                grdProcessSegment.View.SetIsReadOnly(!grdProSegMEC.GetChangedRows().Rows.Count.Equals(0));
            };

            grdProcessSegment.View.CellValueChanged += (s, e) =>
            {
                // 스텝유형이 변경되면 작업단위를 변경해준다.
                if (e.Column.FieldName.Equals("STEPCLASS"))
                {
                    Dictionary<string, object> Param = new Dictionary<string, object>()
                    {
                        {"CODECLASSID", e.Value },
                        {"LANGUAGETYPE", UserInfo.Current.LanguageType }
                    };

                    if (SqlExecuter.Query("GetCodeList", "00001", Param) is DataTable dtStep)
                    {
                        if (dtStep != null && dtStep.Rows.Count > 0)
                        {
                            List<string> lstCodeID = (from t in dtStep.AsEnumerable() select t.Field<string>("CODEID")).ToList();
                            List<string> lstCodeName = (from t in dtStep.AsEnumerable() select t.Field<string>("CODENAME")).ToList();

                            grdProcessSegment.View.SetRowCellValue(e.RowHandle, "STEPTYPE", string.Join(",", lstCodeID));
                            grdProcessSegment.View.SetRowCellValue(e.RowHandle, "STEPTYPENAME", string.Join(",", lstCodeName));
                        }
                    }
                }
            };

            //설비 그리드
            grdProSegMEC.View.AddingNewRow += (s, e) =>
            {
                DataTable dtChk = grdProcessSegment.GetChangedRows();
                if (dtChk.Rows.Count != 0)
                {
                    e.IsCancel = true;
                }

                DataRow drRow = grdProcessSegment.View.GetFocusedDataRow();

                object obj = grdProSegMEC.View.DataSource;
                DataTable dtPs = ((DataView)obj).Table;

                e.NewRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                e.NewRow["PLANTID"] = "";
                e.NewRow["PROCESSSEGMENTID"] = drRow["STANDARDOPERATIONID"];
                e.NewRow["EQPTSEQUENCE"] = 0;

                // 최종순번 값을 구해 + 1 을 해준다.
                DataRow[] row = dtPs.Select("1=1", "EQPTSEQUENCE DESC");
                e.NewRow["EQPTSEQUENCE"] = int.Parse(row[0]["EQPTSEQUENCE"].ToString()) + 1;
                e.NewRow["VALIDSTATE"] = "Valid";
            };

            grdProSegMEC.View.ShowingEditor += (s, e) =>
            {
                if (grdProSegMEC.View.GetFocusedDataRow().RowState != DataRowState.Added)
                {
                    e.Cancel = true;
                }
            };

            //스펙 그리드
            grdSpecattribute.View.AddingNewRow += (s, e) =>
            {
                DataTable dtChk = grdProcessSegment.GetChangedRows();

                // 공정그룹이 수정일경우 추가 불가능
                if (dtChk.Rows.Count != 0)
                {
                    e.IsCancel = true;
                }

                DataRow drRow = grdProcessSegment.View.GetFocusedDataRow();

                object obj = grdSpecattribute.View.DataSource;
                DataTable dtSb = ((DataView)obj).Table;

                e.NewRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                e.NewRow["PLANTID"] = UserInfo.Current.Plant;
                e.NewRow["PROCESSSEGMENTID"] = drRow["STANDARDOPERATIONID"];
                e.NewRow["SPECSEQUENCE"] = 0;
                e.NewRow["INSPECTIONDEFID"] = "OperationInspection";

                DataRow[] row = dtSb.Select("1=1", "SPECSEQUENCE DESC");

                e.NewRow["SPECSEQUENCE"] = int.Parse(row[0]["SPECSEQUENCE"].ToString()) + 1;
                e.NewRow["VALIDSTATE"] = "Valid";
            };

            treeProcesssegment.FocusedNodeChanged += TreeProcesssegment_FocusedNodeChanged;
            grdProcessSegmentClass.View.FocusedRowChanged += grdProcessSegmentClass_FocusedRowChanged;
            grdProcessSegment.View.FocusedRowChanged += grdProcessSegment_FocusedRowChanged1;

            new SetGridHeadDeleteDetail(grdProcessSegmentClass, new object[] { grdProcessSegment }, new string[] { "PROCESSSEGMENTCLASSID" }, new string[] { "PROCESSSEGMENTCLASSID" });
        }

        private void grdProcessSegmentClass_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grdProcessSegmentClass.View.FocusedRowHandle < 0)
            {
                return;
            }

            DataRow drFocusRow = grdProcessSegmentClass.View.GetFocusedDataRow();
            if (drFocusRow == null)
            {
                return;
            }

            DataTable dtProcessSeg = grdProcessSegment.GetChangedRows();
            DataTable dtEquipmentClass = grdProSegMEC.GetChangedRows();
            DataTable dtSpecAttribute = grdSpecattribute.GetChangedRows();

            if (dtProcessSeg.Rows.Count > 0 || dtEquipmentClass.Rows.Count > 0 || dtSpecAttribute.Rows.Count > 0)
            {
                if (ShowMessageBox("WRITTENBEDELETE", Language.Get("MESSAGEINFO"), MessageBoxButtons.OKCancel).Equals(DialogResult.Cancel))
                {
                    grdProcessSegmentClass.View.FocusedRowChanged -= grdProcessSegmentClass_FocusedRowChanged;
                    grdProcessSegmentClass.View.FocusedRowHandle = e.PrevFocusedRowHandle;
                    grdProcessSegmentClass.View.SelectRow(e.PrevFocusedRowHandle);
                    grdProcessSegmentClass.View.FocusedRowChanged += grdProcessSegmentClass_FocusedRowChanged;
                    return;
                }
            }

            _focusProcessSegmentClass = drFocusRow["PROCESSSEGMENTCLASSID"].ToString();
            _focusProcessSegment = string.Empty;

            if (grdProcessSegment.DataSource is DataTable dtProcessSegment)
            {
                dtProcessSegment.Clear();
            }

            var values = Conditions.GetValues();

            Dictionary<string, object> paramExt = new Dictionary<string, object>
            {
                { "ENTERPRISEID", UserInfo.Current.Enterprise },
                { "PROCESSSEGMENTCLASSID", drFocusRow["PROCESSSEGMENTCLASSID"] },
                { "VALIDSTATE", values["P_VALIDSTATE"] },
                { "LANGUAGETYPE", UserInfo.Current.LanguageType }
            };

            grdProcessSegment.DataSource = SqlExecuter.Query("GetProcessSegmentExtlist", "10001", paramExt);
        }

        private void grdProcessSegmentClass_FocusedRowChanged()
        {
            if (grdProcessSegmentClass.View.FocusedRowHandle < 0)
            {
                return;
            }

            DataRow drFocusRow = grdProcessSegmentClass.View.GetFocusedDataRow();
            if (drFocusRow == null)
            {
                return;
            }

            DataTable dtProcessSeg = grdProcessSegment.GetChangedRows();
            DataTable dtEquipmentClass = grdProSegMEC.GetChangedRows();
            DataTable dtSpecAttribute = grdSpecattribute.GetChangedRows();

            _focusProcessSegmentClass = drFocusRow["PROCESSSEGMENTCLASSID"].ToString();
            _focusProcessSegment = string.Empty;

            if (grdProcessSegment.DataSource is DataTable dtProcessSegment)
            {
                dtProcessSegment.Clear();
            }

            var values = Conditions.GetValues();

            Dictionary<string, object> paramExt = new Dictionary<string, object>
            {
                { "ENTERPRISEID", UserInfo.Current.Enterprise },
                { "PROCESSSEGMENTCLASSID", drFocusRow["PROCESSSEGMENTCLASSID"] },
                { "VALIDSTATE", values["P_VALIDSTATE"] },
                { "LANGUAGETYPE", UserInfo.Current.LanguageType }
            };

            grdProcessSegment.DataSource = SqlExecuter.Query("GetProcessSegmentExtlist", "10001", paramExt);
        }

        private void grdProcessSegment_FocusedRowChanged1(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grdProcessSegment.View.FocusedRowHandle < 0)
            {
                return;
            }

            DataTable dtEquipmentClass = grdProSegMEC.GetChangedRows();
            DataTable dtSpecAttribute = grdSpecattribute.GetChangedRows();

            if (dtEquipmentClass.Rows.Count > 0 || dtSpecAttribute.Rows.Count > 0)
            {
                if (ShowMessageBox("WRITTENBEDELETE", Language.Get("MESSAGEINFO"), MessageBoxButtons.OKCancel).Equals(DialogResult.Cancel))
                {
                    grdProcessSegment.View.FocusedRowChanged -= grdProcessSegment_FocusedRowChanged1;
                    grdProcessSegment.View.FocusedRowHandle = e.PrevFocusedRowHandle;
                    grdProcessSegment.View.SelectRow(e.PrevFocusedRowHandle);
                    grdProcessSegment.View.FocusedRowChanged += grdProcessSegment_FocusedRowChanged1;
                    return;
                }
            }

            if (grdProSegMEC.DataSource is DataTable dtProSegMEC)
            {
                dtProSegMEC.Clear();
            }

            if (grdSpecattribute.DataSource is DataTable dtSpecattribute)
            {
                dtSpecattribute.Clear();
            }

            DataRow GetGrdfocusRow = grdProcessSegment.View.GetFocusedDataRow();
            if (GetGrdfocusRow == null)
            {
                return;
            }

            _focusProcessSegment = GetGrdfocusRow["STANDARDOPERATIONID"].ToString();

            Dictionary<string, object> paramPS = new Dictionary<string, object>
            {
                { "PLANTID", UserInfo.Current.Plant },
                { "ENTERPRISEID", UserInfo.Current.Enterprise },
                { "PROCESSSEGMENTID", GetGrdfocusRow["STANDARDOPERATIONID"] },
                { "LANGUAGETYPE", UserInfo.Current.LanguageType },
                { "VALIDSTATE", Conditions.GetValues()["P_VALIDSTATE"] }
            };

            // 설비
            grdProSegMEC.DataSource = SqlExecuter.Query("GetProcessSegMentEqptClasslist", "10001", paramPS);

            // 스펙
            grdSpecattribute.DataSource = SqlExecuter.Query("GetSpecAttributelist", "10001", paramPS);
        }

        /// <summary>
        /// 트리 리스트에서 다른 노드 선택했을 때 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeProcesssegment_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (e.Node == null)
            {
                return;
            }

            pnlContent.ShowWaitArea();

            try
            {
                DataTable dtProcessSegClass = grdProcessSegmentClass.GetChangedRows();
                DataTable dtProcessSeg = grdProcessSegment.GetChangedRows();
                DataTable dtEquipmentClass = grdProSegMEC.GetChangedRows();
                DataTable dtSpecAttribut = grdSpecattribute.GetChangedRows();

                if (dtProcessSegClass.Rows.Count > 0 || dtProcessSeg.Rows.Count > 0 || dtEquipmentClass.Rows.Count > 0 || dtSpecAttribut.Rows.Count > 0)
                {
                    if (ShowMessageBox("WRITTENBEDELETE", Language.Get("MESSAGEINFO"), MessageBoxButtons.OKCancel).Equals(DialogResult.Cancel))
                    {
                        treeProcesssegment.FocusedNodeChanged -= TreeProcesssegment_FocusedNodeChanged;
                        treeProcesssegment.FocusedNode = e.OldNode;
                        treeProcesssegment.FocusedNodeChanged += TreeProcesssegment_FocusedNodeChanged;
                        return;
                    }
                }

                DataRow focusRow = treeProcesssegment.GetFocusedDataRow();
                if (focusRow == null)
                {
                    return;
                }

                if (grdProSegMEC.DataSource is DataTable dtProSegMEC)
                {
                    dtProSegMEC.Clear();
                }

                if (grdSpecattribute.DataSource is DataTable dtSpecattribute)
                {
                    dtSpecattribute.Clear();
                }

                if (grdProcessSegment.DataSource is DataTable dtProcessSegment)
                {
                    dtProcessSegment.Clear();
                }

                Dictionary<string, object> param = new Dictionary<string, object>
                {
                    { "LANGUAGETYPE", UserInfo.Current.LanguageType },
                    { "ENTERPRISEID", UserInfo.Current.Enterprise },
                    { "PARENTPROCESSSEGMENTCLASSID", focusRow["PROCESSSEGMENTCLASSID"].ToString() },
                    { "VALIDSTATE", Conditions.GetValues()["P_VALIDSTATE"].ToString() }
                };

                grdProcessSegmentClass.DataSource = SqlExecuter.Query("GetProcessSegmentClass", "10002", param);

                if (!string.IsNullOrEmpty(_focusProcessSegmentClass))
                {
                    grdProcessSegmentClass.View.FocusedRowHandle = grdProcessSegmentClass.View.GetRowHandleByValue("PROCESSSEGMENTCLASSID", _focusProcessSegmentClass);
                }

                DataRow GetGrdfocusRow = grdProcessSegmentClass.View.GetFocusedDataRow();

                if (GetGrdfocusRow != null)
                {
                    param.Add("PROCESSSEGMENTCLASSID", GetGrdfocusRow["PROCESSSEGMENTCLASSID"].ToString());
                    grdProcessSegment.DataSource = SqlExecuter.Query("GetProcessSegmentExtlist", "10001", param);

                    if (!string.IsNullOrEmpty(_focusProcessSegment))
                    {
                        grdProcessSegment.View.FocusedRowHandle = grdProcessSegment.View.GetRowHandleByValue("STANDARDOPERATIONID", _focusProcessSegment);
                    }
                }

                DataRow GetProcessSegment = grdProcessSegment.View.GetFocusedDataRow();

                if (GetProcessSegment != null)
                {
                    param.Add("PLANTID", UserInfo.Current.Plant);
                    param.Add("PROCESSSEGMENTID", GetProcessSegment["STANDARDOPERATIONID"].ToString());

                    // 설비
                    grdProSegMEC.DataSource = SqlExecuter.Query("GetProcessSegMentEqptClasslist", "10001", param);

                    // 스펙
                    grdSpecattribute.DataSource = SqlExecuter.Query("GetSpecAttributelist", "10001", param);
                }
            }
            finally
            {
                pnlContent.CloseWaitArea();
            }
        }

        #endregion 이벤트

        #region 툴바

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            DataTable dtProcessSegmentClass = new DataTable();
            DataTable dtgrdProcessSegment = new DataTable();
            DataTable dtgrdProSegMEC = new DataTable();
            DataTable dtgrdSpecattribute = new DataTable();

            dtProcessSegmentClass = grdProcessSegmentClass.GetChangedRows();
            dtProcessSegmentClass.TableName = "ProcessSegmentClass";

            dtgrdProcessSegment = grdProcessSegment.GetChangedRows();
            dtgrdProcessSegment.TableName = "ProcessSegment";

            dtgrdProSegMEC = grdProSegMEC.GetChangedRows();
            dtgrdProSegMEC.TableName = "ProcessEquipmentClass";

            dtgrdSpecattribute = grdSpecattribute.GetChangedRows();
            dtgrdSpecattribute.TableName = "ProcessSpec";

            DataSet dsChange = new DataSet();
            if (dtProcessSegmentClass != null && dtProcessSegmentClass.Rows.Count > 0)
            {
                dsChange.Tables.Add(dtProcessSegmentClass);
            }

            if (dtgrdProcessSegment != null && dtgrdProcessSegment.Rows.Count > 0)
            {
                dsChange.Tables.Add(dtgrdProcessSegment);
            }

            if (dtgrdProSegMEC != null && dtgrdProSegMEC.Rows.Count > 0)
            {
                dsChange.Tables.Add(dtgrdProSegMEC);
            }

            if (dtgrdSpecattribute != null && dtgrdSpecattribute.Rows.Count > 0)
            {
                dsChange.Tables.Add(dtgrdSpecattribute);
            }

            ExecuteRule("SaveProcessSegmentManagement", dsChange);
        }

        #endregion 툴바

        #region 검색

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            Dictionary<string, object> param = new Dictionary<string, object>
            {
                { "LANGUAGETYPE", UserInfo.Current.LanguageType },
                { "ENTERPRISEID", UserInfo.Current.Enterprise },
                { "VALIDSTATE", Conditions.GetValues()["P_VALIDSTATE"].ToString() }
            };

            if (tabProcessSegment.SelectedTabPageIndex.Equals(0))
            {
                TreeListNode prevTreeNode = treeProcesssegment.FocusedNode;
                int iPrevSegmentClassHandle = grdProcessSegmentClass.View.FocusedRowHandle;
                int iPrevSegmentHandle = grdProcessSegment.View.FocusedRowHandle;
                int iPrevAttributeHandle = grdSpecattribute.View.FocusedRowHandle;
                int iPrevEquipmentClassHandle = grdProSegMEC.View.FocusedRowHandle;

                grdProcessSegmentClass.View.ClearDatas();
                grdProSegMEC.View.ClearDatas();
                grdSpecattribute.View.ClearDatas();
                grdProcessSegment.View.ClearDatas();

                treeProcesssegment.DataSource = SqlExecuter.Query("GetProcessSegmentClass", "10003", param);
                treeProcesssegment.PopulateColumns();
                treeProcesssegment.ExpandAll();

                treeProcesssegment.FocusedNode = prevTreeNode;

                //SET 공정 그룹 focus
                grdProcessSegmentClass.View.FocusedRowHandle = (iPrevSegmentClassHandle <= 0) ? 0 : iPrevSegmentClassHandle;

                //SET 공정 focus
                grdProcessSegment.View.FocusedRowHandle = (iPrevSegmentHandle <= 0) ? 0 : iPrevSegmentHandle;

                //SET 설비그룹 focus
                grdProSegMEC.View.FocusedRowHandle = (iPrevEquipmentClassHandle <= 0) ? 0 : iPrevEquipmentClassHandle;

                //SET 스펙 focus
                grdSpecattribute.View.FocusedRowHandle = (iPrevAttributeHandle <= 0) ? 0 : iPrevAttributeHandle;
            }
            else
            {
                grdprocesslist.View.ClearDatas();

                grdprocesslist.DataSource = SqlExecuter.Query("SelectProcessSegmentExtlist", "10001", param);
            }
        }

        #endregion 검색

        #region 유효성 검사

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            DataTable dtProcessSegmentClass = new DataTable();
            DataTable dtgrdProcessSegment = new DataTable();
            DataTable dtgrdProSegMEC = new DataTable();
            DataTable dtgrdSpecattribute = new DataTable();

            dtProcessSegmentClass = grdProcessSegmentClass.GetChangedRows();
            dtgrdProcessSegment = grdProcessSegment.GetChangedRows();
            dtgrdProSegMEC = grdProSegMEC.GetChangedRows();
            dtgrdSpecattribute = grdSpecattribute.GetChangedRows();

            if (dtProcessSegmentClass.Rows.Count < 1 && dtgrdProcessSegment.Rows.Count < 1 && dtgrdProSegMEC.Rows.Count < 1 && dtgrdSpecattribute.Rows.Count < 1)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }

            if (dtProcessSegmentClass != null)
            {
                if (dtProcessSegmentClass.Rows.Count < 1)
                {
                    grdProcessSegmentClass.View.CheckValidation();
                }
            }

            if (dtgrdProcessSegment != null)
            {
                if (dtgrdProcessSegment.Rows.Count < 1)
                {
                    grdProcessSegment.View.CheckValidation();
                }
            }

            if (dtgrdProSegMEC != null)
            {
                if (dtgrdProSegMEC.Rows.Count < 1)
                {
                    grdProSegMEC.View.CheckValidation();
                }

                int iDistinct = dtgrdProSegMEC.AsEnumerable().Select(r => Format.GetString(r["EQUIPMENTCLASSID"]) + "|" + Format.GetString(r["PROCESSSEGMENTID"])).Distinct().Count();
                if (iDistinct < dtgrdProSegMEC.Rows.Count)
                {
                    //중복된 설비그룹 ID가 있습니다.
                    throw MessageException.Create("DUPLICATEEQUIPMENTCLASSID");
                }
            }

            if (dtgrdSpecattribute != null)
            {
                if (dtgrdSpecattribute.Rows.Count < 1)
                {
                    grdSpecattribute.View.CheckValidation();
                }
            }
        }

        #endregion 유효성 검사

        #region private Fuction

        /// <summary>
        /// 팝업 컬럼에서 확인 클릭 시 다른 데이터 매핑
        /// </summary>
        /// <param name="currentGridRow">화면 grid row</param>
        /// <param name="popupSelections">팝업에서 선택한 row</param>
        /// <returns></returns>
        private ValidationResultCommon ValidationEquipmentClassIdPopup(DataRow currentGridRow, IEnumerable<DataRow> popupSelections)
        {
            ValidationResultCommon result = new ValidationResultCommon();

            foreach (DataRow row in popupSelections)
            {
                currentGridRow["EQUIPMENTCLASSNAME"] = row["EQUIPMENTCLASSNAME"];
            }
            return result;
        }

        /// <summary>
        /// 팝업 컬럼에서 확인 클릭 시 다른 데이터 매핑
        /// </summary>
        /// <param name="currentGridRow">화면 grid row</param>
        /// <param name="popupSelections">팝업에서 선택한 row</param>
        /// <returns></returns>
        private ValidationResultCommon ValidationSpitemPopup(DataRow currentGridRow, IEnumerable<DataRow> popupSelections)
        {
            ValidationResultCommon result = new ValidationResultCommon();

            foreach (DataRow row in popupSelections)
            {
                currentGridRow["INSPITEMNAME"] = row["INSPITEMNAME"];
                currentGridRow["INSPECTIONDEFID"] = row["INSPECTIONDEFID"];
            }
            return result;
        }

        #endregion private Fuction
    }
}