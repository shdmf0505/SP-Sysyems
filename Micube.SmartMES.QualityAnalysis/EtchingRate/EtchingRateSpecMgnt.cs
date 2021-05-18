#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;

using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

using System.Data;
using Micube.SmartMES.Commons.Controls;
using System.Threading.Tasks;
using System.Windows.Forms;
using Micube.SmartMES.Commons;
using DevExpress.XtraEditors.Repository;
using System;

#endregion

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질 > 레칭레이트 > 에칭레이트 규격 등록
    /// 업  무  설  명  : 에칭레이트 규격(SPEC) 관리한다.
    /// 생    성    자  : 전우성
    /// 생    성    일  : 2019-06-24
    /// 수  정  이  력  : 2019-07-15 강유라
    /// 
    /// 
    /// </summary>
    public partial class EtchingRateSpecMgnt : SmartConditionManualBaseForm
    {
        #region Local Variables
        
        #endregion

        #region 생성자

        public EtchingRateSpecMgnt()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeEvent();
            InitializeGrid();
            //InitializeLanguageKey();
        }

        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            grdMain.GridButtonItem = GridButtonItem.Add | GridButtonItem.Export;
            grdMain.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdMain.View.SetSortOrder("SPECSEQUENCE");
            grdMain.View.BestFitColumns();

            grdMain.View.AddTextBoxColumn("SPECSEQUENCE", 150)
                .SetIsHidden();


            grdMain.View.AddTextBoxColumn("SPECCLASSID", 150)
                 .SetIsHidden()
                 .SetDefault("EtchingRateSpec");


            grdMain.View.AddTextBoxColumn("PLANTID", 150)
                .SetIsReadOnly();

            grdMain.View.AddTextBoxColumn("ENTERPRISEID", 150)
                .SetIsHidden();

            #region 공정 팝업 PROCESSSEGMENTCLASSID
            var ProcessSegmentClassId = grdMain.View.AddSelectPopupColumn("PROCESSSEGMENTCLASSNAME", new SqlQuery("GetProcessSegmentClass", "10001", "PROCESSSEGMENTCLASSTYPE=TopProcessSegmentClass", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"),"PROCESSSEGMENTCLASSID")
                                               .SetPopupLayout("TOPPROCESSSEGMENTCLASS", PopupButtonStyles.Ok_Cancel, true, true)
                                               .SetLabel("TOPPROCESSSEGMENTCLASSNAME")
                                               .SetPopupAutoFillColumns("PROCESSSEGMENTCLASSNAME")
                                               .SetPopupResultCount(1)
                                               .SetPopupLayoutForm(500, 600, FormBorderStyle.FixedToolWindow)
                                               //.SetRelationIds("PLANTID")
                                               .SetValidationKeyColumn();
            //.SetPopupQueryPopup((DataRow currentrow) =>
            //{
            //    if (string.IsNullOrWhiteSpace(currentrow.ToStringNullToEmpty("PLANTID")))
            //    {
            //        this.ShowMessage("NoSelectSite");
            //        return false;
            //    }

            //    return true;
            //});

            ProcessSegmentClassId.Conditions.AddTextBox("PROCESSSEGMENTCLASS")
                .SetLabel("LARGEPROCESSSEGMENTIDNAME");

            ProcessSegmentClassId.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 150)
                .SetLabel("TOPPROCESSSEGMENTCLASSID");
            ProcessSegmentClassId.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 200)
                .SetLabel("TOPPROCESSSEGMENTCLASSNAME");

            grdMain.View.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 150)
                .SetLabel("TOPPROCESSSEGMENTCLASSID")
                .SetIsHidden();
            #endregion

            #region 설비(호기) 팝업 EQUIPMENTID
            var equipmentId = grdMain.View.AddSelectPopupColumn("EQUIPMENTNAME", new SqlQuery("GetEquipmentByTopProcesssegId", "10001", "DETAILEQUIPMENTTYPE=Main", $"LANGUAGETYPE={UserInfo.Current.LanguageType}",
                $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "EQUIPMENTID")
                                     .SetPopupLayout("EQUIPMENT", PopupButtonStyles.Ok_Cancel, true, true)
                                     .SetPopupResultCount(1)
                                     .SetValidationKeyColumn()
                                     .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                                     .SetPopupAutoFillColumns("EQUIPMENTNAME")
                                     .SetPopupQueryPopup((DataRow currentrow) =>
                                     {
                                         if (string.IsNullOrWhiteSpace(currentrow.ToStringNullToEmpty("PROCESSSEGMENTCLASSID")))
                                         {
                                             this.ShowMessage("NoSelectTopProcess");
                                             return false;
                                         }

                                         return true;
                                     });



            equipmentId.Conditions.AddTextBox("EQUIPMENT")
                .SetLabel("EQUIPMENTIDNAME");

            equipmentId.Conditions.AddTextBox("TOPPROCESSSEGMENTCLASSID")
                .SetPopupDefaultByGridColumnId("PROCESSSEGMENTCLASSID")
                .SetIsHidden();

            // 팝업 그리드
            equipmentId.GridColumns.AddTextBoxColumn("EQUIPMENTCLASSNAME", 150);
            equipmentId.GridColumns.AddTextBoxColumn("EQUIPMENTID", 150);
            equipmentId.GridColumns.AddTextBoxColumn("EQUIPMENTNAME", 200);

            grdMain.View.AddTextBoxColumn("EQUIPMENTID", 150)
                .SetIsHidden();

            #endregion

            #region 설비단 팝업 CHILDEQUIPMENTID
            var childEquipementId = grdMain.View.AddSelectPopupColumn("CHILDEQUIPMENTNAME", new SqlQuery("GetEquipmentListByDetailTypeUserArea", "10001", "EQUIPMENTCLASSTYPE=Production", "DETAILEQUIPMENTTYPE=Sub", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                                .SetPopupLayout("CHILDEQUIPMENT", PopupButtonStyles.Ok_Cancel, true, true)
                                                .SetPopupResultCount(1)
                                                .SetPopupResultMapping("CHILDEQUIPMENTNAME", "EQUIPMENTNAME")
                                                .SetPopupLayoutForm(500, 600, FormBorderStyle.FixedToolWindow)
                                                .SetPopupAutoFillColumns("EQUIPMENTNAME")
                                                .SetLabel("CHILDEQUIPMENTNAME")
                                                .SetRelationIds("PARENTEQUIPMENTID")
                                                .SetValidationKeyColumn()
                                                .SetPopupQueryPopup((DataRow currentrow) =>
                                                {
                                                    if (string.IsNullOrWhiteSpace(currentrow.ToStringNullToEmpty("EQUIPMENTID")))
                                                    {
                                                        this.ShowMessage("NoSelectParentEquipment");
                                                        return false;
                                                    }

                                                    return true;
                                                })
                                                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                                                {
                                                    // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
                                                    // dataGridRow : 현재 Focus가 있는 그리드의 DataRow

                                                    foreach (DataRow row in selectedRows)
                                                    {
                                                        dataGridRow["CHILDEQUIPMENTID"] = row["EQUIPMENTID"].ToString();
                                                    }

                                                });

            childEquipementId.Conditions.AddTextBox("EQUIPMENT")
                .SetLabel("CHILDEQUIPMENTIDNAME");

            childEquipementId.Conditions.AddTextBox("PARENTEQUIPMENTID")
                                        .SetPopupDefaultByGridColumnId("EQUIPMENTID")
                                        .SetIsHidden();
            // 팝업 그리드
            childEquipementId.GridColumns.AddTextBoxColumn("EQUIPMENTID", 150)
                .SetLabel("CHILDEQUIPMENTID");
            childEquipementId.GridColumns.AddTextBoxColumn("EQUIPMENTNAME", 200)
                .SetLabel("CHILDEQUIPMENTNAME");

            grdMain.View.AddTextBoxColumn("CHILDEQUIPMENTID", 150)
               .SetIsHidden();

            #endregion

            grdMain.View.AddTextBoxColumn("WORKTYPE", 100)
                .SetLabel("TYPECONDITION")
                .SetValidationKeyColumn()
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left);

            grdMain.View.AddTextBoxColumn("SPECRANGE", 140)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left);

            grdMain.View.AddTextBoxColumn("CONTROLRANGE", 140)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left);

            grdMain.View.AddTextBoxColumn("DESCRIPTION", 150)
                .SetIsReadOnly();

            grdMain.View.AddTextBoxColumn("WORKCONDITION", 100)
                .SetIsHidden();

            grdMain.View.AddTextBoxColumn("CHANGEREASON", 100)
               .SetIsHidden();

            grdMain.View.AddComboBoxColumn("DEFAULTCHARTTYPE", 100, new SqlQuery("GetTypeList", "10001", "CODECLASSID=ControlType", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}"))
               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false)
               .SetIsHidden();

            grdMain.View.PopulateColumns();

        }

        /// <summary>
        /// Language Key 초기화
        /// </summary>
        private void InitializeLanguageKey()
        {
            grdMain.LanguageKey = "MEASUREDVALUE";
        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            //Add Row 가 있을 때 Delete버튼 생성
            new SetGridDeleteButonVisibleSimple(grdMain);

            grdMain.View.AddingNewRow += (s, e) =>
              {
                  e.NewRow["ENTERPRISEID"] = Framework.UserInfo.Current.Enterprise;
                  e.NewRow["PLANTID"] = Framework.UserInfo.Current.Plant;
              };
      

            // 메인 그리드 더블클릭 이벤트
            grdMain.View.DoubleClick += (s, e) =>
            {
                DataRow dr = grdMain.View.GetFocusedDataRow();

                if (dr == null)
                {
                    return;
                }

                if (string.IsNullOrEmpty(dr["PROCESSSEGMENTCLASSID"].ToString()) ||
                    string.IsNullOrEmpty(dr["EQUIPMENTID"].ToString()) ||
                    string.IsNullOrEmpty(dr["CHILDEQUIPMENTID"].ToString()))
                {
                    return;
                }

                //버튼의 enable
                bool isEnable = btnPopupFlag.Enabled;
                /*
                if (!Format.GetString(dr["ISMODIFY"]).Equals("Y"))
                {
                    isEnable = false;
                }*/
                

                popupSpecValue popup = new popupSpecValue()
                {
                    StartPosition = FormStartPosition.CenterParent,
                    isButtonEnable = isEnable,
                    Owner = this
                };

                popup.CurrentDataRow = dr;
                popup.SetData();
                if (popup.ShowDialog() == DialogResult.OK)
                {
                    grdMain.View.SetFocusedRowCellValue("WORKTYPE", popup.CurrentDataRow["WORKTYPE"]);
                    grdMain.View.SetFocusedRowCellValue("WORKCONDITION", popup.CurrentDataRow["WORKCONDITION"]);
                    grdMain.View.SetFocusedRowCellValue("CHANGEREASON", popup.CurrentDataRow["CHANGEREASON"]);
                    grdMain.View.SetFocusedRowCellValue("CONTROLRANGE", popup.CurrentDataRow["CONTROLRANGE"]);
                    grdMain.View.SetFocusedRowCellValue("SPECRANGE", popup.CurrentDataRow["SPECRANGE"]);
                    grdMain.View.SetFocusedRowCellValue("DEFAULTCHARTTYPE", popup.CurrentDataRow["DEFAULTCHARTTYPE"]);                  
                }
            };
        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            //base.OnToolbarSaveClick();
            //DataTable dt = SetRowState(grdMain.GetChangedRows());
            //ExecuteRule("SaveEtchingRateSpecDef", dt);
        }

        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            SmartButton btn = sender as SmartButton;

            if (btn.Name.ToString().Equals("Save"))
            {
                Btn_SaveClick(btn.Text);
            }
        }
        #endregion

        #region 검색

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            var value = Conditions.GetValues();
            value.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);
            value.Add("WORKCONDITION", "Enable");

            if (await SqlExecuter.QueryAsync("SelectEtchingRateSpecDef", "10001", value) is DataTable dt)
            {
                if (dt.Rows.Count < 1)
                {
                    // 조회할 데이터가 없습니다.
                    ShowMessage("NoSelectData");
                }

                grdMain.DataSource = dt;
            }
        }

        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            InitializeConditionPopup_ProcessSegment();
            InitializeConditionPopup_Equipment();
            InitializeConditionPopup_ChildEquipment();
        }

        #region 조회조건 팝업 초기화

        /// <summary>
        /// 공정조회 조건 팝업
        /// </summary>
        private void InitializeConditionPopup_ProcessSegment()
        {
            var ProcessSegmentClassId = Conditions.AddSelectPopup("P_PROCESSSEGMENTCLASSID", new SqlQuery("GetProcessSegmentClass", "10001", "PROCESSSEGMENTCLASSTYPE=TopProcessSegmentClass", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"),"PROCESSSEGMENTCLASSNAME", "PROCESSSEGMENTCLASSID")
                                               .SetPopupLayout("TOPPROCESSSEGMENTCLASS", PopupButtonStyles.Ok_Cancel, true, true)
                                               .SetPopupAutoFillColumns("PROCESSSEGMENTCLASSNAME")
                                               .SetPopupResultCount(1)
                                               .SetPosition(3)
                                               .SetLabel("TOPPROCESSSEGMENTCLASS")
                                               .SetPopupLayoutForm(500, 600, FormBorderStyle.FixedToolWindow);


            ProcessSegmentClassId.Conditions.AddTextBox("PROCESSSEGMENTCLASS")
                .SetLabel("LARGEPROCESSSEGMENTIDNAME");


            ProcessSegmentClassId.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 150)
                .SetLabel("TOPPROCESSSEGMENTCLASSID");
            ProcessSegmentClassId.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 200)
                .SetLabel("TOPPROCESSSEGMENTCLASSNAME");
        }

        /// <summary>
        /// 설비 조회조건 팝업
        /// </summary>
        private void InitializeConditionPopup_Equipment()
        {
            var equipmentId = Conditions.AddSelectPopup("P_EQUIPMENTID", new SqlQuery("GetEquipmentByClassHierarchyUserArea", "10001", "DETAILEQUIPMENTTYPE=Main", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "EQUIPMENTNAME", "EQUIPMENTID")
                         .SetPopupLayout("EQUIPMENT", PopupButtonStyles.Ok_Cancel, true, false)
                         .SetPopupResultCount(1)
                         .SetPosition(4)
                         .SetLabel("EQUIPMENT")
                         .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                         .SetPopupAutoFillColumns("EQUIPMENTNAME");

            equipmentId.Conditions.AddComboBox("PARENTEQUIPMENTCLASSID", new SqlQuery("GetEquipmentClassHierarchyAndType", "10001", "HIERARCHY=TopEquipment", "EQUIPMENTCLASSTYPE=Production", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "EQUIPMENTCLASSNAME", "EQUIPMENTCLASSID")
                                  .SetValidationKeyColumn()
                                  .SetLabel("TOPEQUIPMENTCLASS");

            equipmentId.Conditions.AddComboBox("MIDDLEEQUIPMENTCLASSID", new SqlQuery("GetEquipmentClassHierarchyAndType", "10001", "HIERARCHY=MiddleEquipment", "EQUIPMENTCLASSTYPE=Production", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "EQUIPMENTCLASSNAME", "EQUIPMENTCLASSID")
                                  .SetValidationKeyColumn()
                                  .SetRelationIds("PARENTEQUIPMENTCLASSID")
                                  .SetLabel("MIDDLEEQUIPMENTCLASS");

            equipmentId.Conditions.AddTextBox("EQUIPMENT")
                .SetLabel("EQUIPMENTIDNAME");

            // 팝업 그리드
            equipmentId.GridColumns.AddTextBoxColumn("EQUIPMENTCLASSNAME", 150);
            equipmentId.GridColumns.AddTextBoxColumn("EQUIPMENTID", 150);
            equipmentId.GridColumns.AddTextBoxColumn("EQUIPMENTNAME", 200);

        }

        /// <summary>
        /// 설비단 조회조건 팝업
        /// </summary>
        private void InitializeConditionPopup_ChildEquipment()
        {
            var childEquipementId = Conditions.AddSelectPopup("P_CHILDEQUIPMENTID", new SqlQuery("GetEquipmentListByDetailTypeUserArea", "10001", "EQUIPMENTCLASSTYPE=Production", "DETAILEQUIPMENTTYPE=Sub", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "EQUIPMENTNAME", "EQUIPMENTID")
                                               .SetPopupLayout("CHILDEQUIPMENT", PopupButtonStyles.Ok_Cancel, true, false)
                                               .SetPopupResultCount(1)
                                               .SetPosition(5)
                                               .SetPopupLayoutForm(500, 600, FormBorderStyle.FixedToolWindow)
                                               .SetPopupAutoFillColumns("EQUIPMENTNAME")
                                               .SetLabel("CHILDEQUIPMENT")
                                               .SetRelationIds("P_EQUIPMENTID");

            childEquipementId.Conditions.AddTextBox("EQUIPMENT")
                .SetLabel("CHILDEQUIPMENTIDNAME");

            // 팝업 그리드
            childEquipementId.GridColumns.AddTextBoxColumn("EQUIPMENTID", 150)
                .SetLabel("CHILDEQUIPMENTID");
            childEquipementId.GridColumns.AddTextBoxColumn("EQUIPMENTNAME", 200)
                .SetLabel("CHILDEQUIPMENTNAME");
        }
        #endregion

        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();
        }

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            grdMain.View.CheckValidation();

            if (grdMain.GetChangedRows().Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }

        #endregion

        #region Private Function

        /// <summary>
        /// 
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        private DataTable SetRowState(DataTable table)
        {
            foreach (DataRow row in table.Rows)
            {
                row["_STATE_"] = "modified";
                row["FLAG"] = "TypeChange";

                if (string.IsNullOrWhiteSpace(row["PROCESSSEGMENTCLASSNAME"].ToString()))
                {
                    throw MessageException.Create("InValidRequiredField", Language.Get("TOPPROCESSSEGMENTCLASSNAME"));
                }
                else if (string.IsNullOrWhiteSpace(row["EQUIPMENTNAME"].ToString()))
                {
                    throw MessageException.Create("InValidRequiredField", Language.Get("EQUIPMENTNAME"));
                }
                else if (string.IsNullOrWhiteSpace(row["CHILDEQUIPMENTNAME"].ToString()))
                {
                    throw MessageException.Create("InValidRequiredField", Language.Get("CHILDEQUIPMENTNAME"));
                }
                else if (string.IsNullOrWhiteSpace(row["WORKTYPE"].ToString()))
                {
                    throw MessageException.Create("InValidRequiredField", Language.Get("TYPECONDITION"));
                }
            }

            return table;
        }

        /// <summary>
        /// 저장 함수
        /// </summary>
        /// <param name="strtitle"></param>
        private void Btn_SaveClick(string strtitle)
        {
            grdMain.View.CloseEditor();
            //grdMain.View.CheckValidation();
            DataTable dt = SetRowState(grdMain.GetChangedRows());

            if (dt.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                this.ShowMessage(MessageBoxButtons.OK, "NoSaveData");
                return;
            }

            DialogResult result = System.Windows.Forms.DialogResult.No;

            result = this.ShowMessage(MessageBoxButtons.YesNo, "InfoSave", strtitle);//저장하시겠습니까? 

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    this.ShowWaitArea();

                    ExecuteRule("SaveEtchingRateSpecDef", dt);
                    ShowMessage("SuccessSave");
                    //재조회 
                    SearchData();
                }
                catch (Exception ex)
                {
                    this.ShowError(ex);
                }
                finally
                {
                    this.CloseWaitArea();


                }
            }
        }

        /// <summary>
        /// 검색 함수
        /// </summary>
        private void SearchData()
        {

            var value = Conditions.GetValues();
            value.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);
            value.Add("WORKCONDITION", "Enable");

            if (SqlExecuter.Query("SelectEtchingRateSpecDef", "10001", value) is DataTable dt)
            {
                if (dt.Rows.Count < 1)
                {
                    // 조회할 데이터가 없습니다.
                    ShowMessage("NoSelectData");
                }

                grdMain.DataSource = dt;
            }
        }
        #endregion
    }
}
