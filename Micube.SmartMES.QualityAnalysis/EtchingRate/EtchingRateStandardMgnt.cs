#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Grid.BandedGrid;
using Micube.SmartMES.Commons.Controls;
using Micube.SmartMES.QualityAnalysis.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질 > 레칭레이트 > 에칭레이트 측정 값 등록
    /// 업  무  설  명  : 에칭레이트 측정 값 및 이상발생 관리한다.
    /// 생    성    자  : 전우성
    /// 생    성    일  : 2019-06-24
    /// 수  정  이  력  : 2019-07-17 강유라
    /// 
    /// 
    /// </summary>
    public partial class EtchingRateStandardMgnt : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가
        DataRow _addMeasure = null;
        DataRow _addReMeasure = null;
        private DataTable _grdMeasureTable = null;
        private List<object> _userAreaList = null;

        #endregion

        #region 생성자

        public EtchingRateStandardMgnt()
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
        }

        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            #region 첫번째 탭 초기화

            #region grdSpec 초기화

            grdSpec.GridButtonItem = GridButtonItem.Export | GridButtonItem.Expand | GridButtonItem.Restore;
            grdSpec.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;
            grdSpec.View.SetIsReadOnly();

            grdSpec.View.AddTextBoxColumn("SPECSEQUENCE", 150)
                .SetIsHidden();

            grdSpec.View.AddTextBoxColumn("AREAID", 150)
                .SetIsHidden();//***

            grdSpec.View.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME",150)
                .SetLabel("TOPPROCESSSEGMENTCLASS");

            grdSpec.View.AddTextBoxColumn("AREANAME", 150); //***

            grdSpec.View.AddTextBoxColumn("EQUIPMENTNAME", 150)
                .SetLabel("EQUIPMENT");

            grdSpec.View.AddTextBoxColumn("CHILDEQUIPMENTNAME", 150)
                .SetLabel("CHILDEQUIPMENT");

            grdSpec.View.AddTextBoxColumn("WORKTYPE", 150)
                .SetLabel("TYPECONDITION");

            grdSpec.View.AddTextBoxColumn("SPECRANGE", 150);

            grdSpec.View.AddTextBoxColumn("CONTROLRANGE", 150);

            grdSpec.View.PopulateColumns();

            #endregion

            #region grdMeasure 초기화

            grdMeasure.GridButtonItem = GridButtonItem.Export | GridButtonItem.Expand | GridButtonItem.Restore;
            grdMeasure.View.SetIsReadOnly();
            grdMeasure.View.SetSortOrder("MEASUREDATE", DevExpress.Data.ColumnSortOrder.Descending);
            //grdMeasure.View.SetSortOrder("MEASUREDEGREE", DevExpress.Data.ColumnSortOrder.Descending);

            grdMeasure.View.AddTextBoxColumn("MEASUREDATE", 150);

            grdMeasure.View.AddTextBoxColumn("MEASUREDEGREE", 150);

            grdMeasure.View.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 150)
                .SetLabel("TOPPROCESSSEGMENTCLASS");

            grdMeasure.View.AddTextBoxColumn("AREANAME", 150)
                .SetLabel("AREA");

            grdMeasure.View.AddTextBoxColumn("AREAID", 150)
                .SetIsHidden();
       
            grdMeasure.View.AddTextBoxColumn("EQUIPMENTNAME", 150)
                .SetLabel("EQUIPMENT");

            grdMeasure.View.AddTextBoxColumn("CHILDEQUIPMENTNAME", 150)
                .SetLabel("CHILDEQUIPMENT");

            grdMeasure.View.AddTextBoxColumn("WORKTYPE", 150)
                .SetLabel("TYPECONDITION");

            grdMeasure.View.AddTextBoxColumn("SPECRANGE", 150);

            grdMeasure.View.AddTextBoxColumn("CONTROLRANGE", 150);

            grdMeasure.View.AddTextBoxColumn("ETCHRATEVALUE", 150);

            grdMeasure.View.AddTextBoxColumn("ETCHRATEBEFOREVALUE", 150)
                .SetIsHidden();

            grdMeasure.View.AddTextBoxColumn("ETCHRATEAFTERVALUE", 150)
                .SetIsHidden();

            grdMeasure.View.AddTextBoxColumn("EQPTSPEED", 150);

            grdMeasure.View.AddTextBoxColumn("MEASURER", 150);

            grdMeasure.View.AddComboBoxColumn("RESULT", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OKNG", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            grdMeasure.View.AddTextBoxColumn("ENTERPRISEID", 150)
                .SetIsHidden();

            grdMeasure.View.AddTextBoxColumn("PLANTID", 150)
                .SetIsHidden();

            grdMeasure.View.AddTextBoxColumn("SPECSEQUENCE", 150)
                .SetIsHidden();

            grdMeasure.View.PopulateColumns();


            #endregion

            #endregion

            #region 두번째 탭 초기화

            #region grdAbnormal 초기화

            grdAbnormal.GridButtonItem = GridButtonItem.Export | GridButtonItem.Expand | GridButtonItem.Restore;
            grdAbnormal.View.SetIsReadOnly();

            grdAbnormal.View.AddTextBoxColumn("SPECSEQUENCE", 150)
                .SetIsHidden();

            grdAbnormal.View.AddTextBoxColumn("ENTERPRISEID", 150)
                .SetIsHidden();

            grdAbnormal.View.AddTextBoxColumn("PLANTID", 150)
                .SetIsHidden();

            grdAbnormal.View.AddTextBoxColumn("MEASUREDATE", 150);

            grdAbnormal.View.AddTextBoxColumn("MEASUREDEGREE", 150);

            grdAbnormal.View.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 150)
                .SetLabel("TOPPROCESSSEGMENTCLASS");

            grdAbnormal.View.AddTextBoxColumn("AREANAME", 150)
                .SetLabel("AREA") ;

            grdAbnormal.View.AddTextBoxColumn("EQUIPMENTNAME", 150)
                .SetLabel("EQUIPMENT");

            grdAbnormal.View.AddTextBoxColumn("CHILDEQUIPMENTNAME", 150)
                .SetLabel("CHILDEQUIPMENT");

            grdAbnormal.View.AddTextBoxColumn("WORKTYPE", 150)
                .SetLabel("TYPECONDITION");

            grdAbnormal.View.AddTextBoxColumn("SPECRANGE", 150);

            grdAbnormal.View.AddTextBoxColumn("CONTROLRANGE", 150);

            grdAbnormal.View.AddTextBoxColumn("ETCHRATEVALUE", 150);

            grdAbnormal.View.AddTextBoxColumn("ETCHRATEBEFOREVALUE", 150)
                .SetIsHidden();

            grdAbnormal.View.AddTextBoxColumn("ETCHRATEAFTERVALUE", 150)
                .SetIsHidden();

            grdAbnormal.View.AddTextBoxColumn("EQPTSPEED", 150)
                .SetIsHidden();

            grdAbnormal.View.AddTextBoxColumn("MEASURER", 150)
                .SetIsHidden();

            grdAbnormal.View.AddTextBoxColumn("REMEASUREDATE", 150)
                .SetIsHidden();

            grdAbnormal.View.AddTextBoxColumn("DEGREE", 150)
                .SetIsHidden();

            grdAbnormal.View.AddTextBoxColumn("REETCHRATEVALUE", 150)
                .SetIsHidden();

            grdAbnormal.View.AddTextBoxColumn("REETCHRATEBEFOREVALUE", 150)
                .SetIsHidden();

            grdAbnormal.View.AddTextBoxColumn("REETCHRATEAFTERVALUE", 150)
                .SetIsHidden();

            grdAbnormal.View.AddTextBoxColumn("REMEASURER", 150)
                .SetIsHidden();

            grdAbnormal.View.AddTextBoxColumn("ACTIONRESULT", 150)
                .SetIsHidden();

            grdAbnormal.View.AddTextBoxColumn("RERESULT", 150)
                .SetIsHidden();

            grdAbnormal.View.AddTextBoxColumn("FILEID", 150)
                .SetIsHidden();

            grdAbnormal.View.PopulateColumns();
            #endregion

            #region grdReMeasure 초기화

            grdReMeasure.GridButtonItem = GridButtonItem.Export | GridButtonItem.Expand | GridButtonItem.Restore;
            grdReMeasure.View.SetIsReadOnly();
            grdReMeasure.View.SetSortOrder("REMEASUREDATE",DevExpress.Data.ColumnSortOrder.Descending);
            grdReMeasure.View.SetSortOrder("DEGREE", DevExpress.Data.ColumnSortOrder.Descending);

            grdReMeasure.View.AddTextBoxColumn("REMEASUREDATE", 150);

            grdReMeasure.View.AddTextBoxColumn("DEGREE", 150)
                .SetLabel("REMEASUREDEGREE");

            grdReMeasure.View.AddTextBoxColumn("REETCHRATEVALUE", 150)
                .SetLabel("ETCHRATEVALUE");

            grdReMeasure.View.AddTextBoxColumn("REMEASURER", 150)
                .SetLabel("MEASURER"); 

            grdReMeasure.View.AddComboBoxColumn("RERESULT", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OKNG", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("RESULT");

            grdReMeasure.View.AddTextBoxColumn("ACTIONRESULT", 150);

            grdReMeasure.View.AddTextBoxColumn("SPECSEQUENCE", 150)
                .SetIsHidden();

            grdReMeasure.View.AddTextBoxColumn("ENTERPRISEID", 150)
                .SetIsHidden();

            grdReMeasure.View.AddTextBoxColumn("PLANTID", 150)
                .SetIsHidden();

            //팝업에 보여주기 위한 테이터
            grdReMeasure.View.AddTextBoxColumn("FILEID", 150)
                .SetIsHidden();

            grdReMeasure.View.AddTextBoxColumn("MEASUREDATE", 150)
                .SetIsHidden();

            grdReMeasure.View.AddTextBoxColumn("MEASUREDEGREE", 150)
                .SetIsHidden();

            grdReMeasure.View.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 150)
                .SetIsHidden();

            grdReMeasure.View.AddTextBoxColumn("EQUIPMENTNAME", 150)
                .SetIsHidden();

            grdReMeasure.View.AddTextBoxColumn("CHILDEQUIPMENTNAME", 150)
                .SetIsHidden();

            grdReMeasure.View.AddTextBoxColumn("WORKTYPE", 150)
                .SetIsHidden();

            grdReMeasure.View.AddTextBoxColumn("SPECRANGE", 150)
                .SetIsHidden();

            grdReMeasure.View.AddTextBoxColumn("CONTROLRANGE", 150)
                .SetIsHidden();

            grdReMeasure.View.AddTextBoxColumn("ETCHRATEVALUE", 150)
                .SetIsHidden();

            grdReMeasure.View.AddTextBoxColumn("ETCHRATEBEFOREVALUE", 150)
                .SetIsHidden();

            grdReMeasure.View.AddTextBoxColumn("ETCHRATEAFTERVALUE", 150)
                .SetIsHidden();

            grdReMeasure.View.AddTextBoxColumn("EQPTSPEED", 150)
                .SetIsHidden();

            grdReMeasure.View.AddTextBoxColumn("MEASURER", 150)
                .SetIsHidden();

            grdReMeasure.View.PopulateColumns();
            #endregion

            #endregion
        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            //화면 로드시 스펙def 조회 이벤트
            Load += (s, e) =>
            {
                SearchSpecDef();
                
                //SearchUserArea();
            };

            #region 첫번째 탭 이벤트
            //grdSpec 그리드 row를 더블클릭 했을 때 이벤트
            grdSpec.View.DoubleClick += View_GrdSpec_DoubleClick;
            //grdSpec 그리드의 포커스row가 바뀔때 이벤트
            grdSpec.View.FocusedRowChanged += View_GrdSpecFocusedRowChanged;
            //grdMeasure 그리드 row를 더블클릭 했을 때 이벤트
            grdMeasure.View.DoubleClick += View_GrdMeasure_DoubleClick;
            //grdMeasure 그리드에 새Row가 추가 될 때 이벤트
            grdMeasure.View.AddingNewRow += (s, e) =>
            {
                BindingMeasure(e.NewRow);
            };
            #endregion


            #region 두번째 탭 이벤트
            grdAbnormal.View.FocusedRowChanged += (s, e) => 
            {
                SeartchReMeasure();
            };


            //grdAbnormal 그리드 row를 더블클릭 했을 때 이벤트
            grdAbnormal.View.DoubleClick += View_GrdAbnormal_DoubleClick;
            //grdReMeasure 그리드 row를 더블클릭 했을 때 이벤트
            grdReMeasure.View.DoubleClick += View_GrdReMeasure_DoubleClick;
            //grdReMeasure 그리드에 새Row가 추가 될 때 이벤트
            grdReMeasure.View.AddingNewRow += (s, e) =>
            {
                BindingReMeasure(e.NewRow);
            };

            //판정 결과에 따라 컬럼의 색상을 바꿔주는 이벤트
            //grdReMeasure.View.ChagneCellColorEvent += (s, e) =>
            //{
            //    if (e.FieldName != "RERESULT") return;

            //    if (e.CurrentRow["RERESULT"].ToString().Equals("OK"))
            //    {
            //        e.BackColor = Color.Green;
            //    }
            //    else
            //    {
            //        e.BackColor = Color.Red;
            //    }
            //};

            //판정 결과에 따라 컬럼의 폰트색상을 바꿔주는 이벤트
            //grdReMeasure.View.RowCellStyle += (s, e) =>
            //{
            //    if (e.Column.FieldName != "RERESULT") return;

            //    if (e.CellValue.ToString().Equals("OK"))
            //    {
            //        e.Appearance.ForeColor = Color.Green;
            //    }
            //    else
            //    {
            //        e.Appearance.ForeColor = Color.Red;
            //    }
            //};
            #endregion
        }

        #region 첫번째 탭 이벤트
        /// <summary>
        /// grdMeasure 더블클릭 => 팝업 결과 grdMeasure에 수정
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_GrdMeasure_DoubleClick(object sender, EventArgs e)
        { 
            DataRow originRow = grdMeasure.View.GetFocusedDataRow();

            if (originRow == null) return;

            DataTable dt = (grdMeasure.DataSource as DataTable).Clone();
            DataRow row = dt.NewRow();
            row.ItemArray = originRow.ItemArray.Clone() as object[];

            if (row == null) return;

            EtchingRateMeasurePopup popup = new EtchingRateMeasurePopup()
            {
                StartPosition = FormStartPosition.CenterParent,
                Owner = this
            };

            popup.CurrentDataRow = row;

            if (string.IsNullOrWhiteSpace(row["MEASUREDEGREE"].ToString()))
            {//저장되기 전  row
                popup.UpdateMeasure(true);
                popup.ShowDialog();
                if (popup.DialogResult == DialogResult.OK)
                {
                    _addMeasure = popup.CurrentDataRow;
                    BindingMeasure(originRow);
                }
            }
            else
            {//저장된 후  row
                popup.UpdateMeasure(false);
                popup.ShowDialog();
            }
        }

        /// <summary>
        /// grdSpec의 선택된 row의 SpecSequence의 측정값을 필터링 하는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_GrdSpecFocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (_grdMeasureTable == null) return;
            FilterGrdMeasureData();
        }
        /// <summary>
        /// grdSpec 더블클릭 => 팝업 결과 grdMeasure에 추가
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_GrdSpec_DoubleClick(object sender, EventArgs e)
        {
            DataRow originRow = grdSpec.View.GetFocusedDataRow();

            if (originRow == null) return;


            if (Format.GetString(originRow["ISMODIFY"]).Equals("Y"))
            {
                DataTable dt = (grdSpec.DataSource as DataTable).Clone();
                DataRow row = dt.NewRow();
                row.ItemArray = originRow.ItemArray.Clone() as object[];

                if (row == null) return;

                EtchingRateMeasurePopup popup = new EtchingRateMeasurePopup()
                {
                    StartPosition = FormStartPosition.CenterParent,
                    Owner = this
                };

                popup.CurrentDataRow = row;
                popup.AddNewRow();

                if (popup.ShowDialog() == DialogResult.OK)
                {
                    _addMeasure = popup.CurrentDataRow;
                    grdMeasure.View.AddNewRow();
                }
            }
            else
            {
                ShowMessage("EtchingMeasureAreaValidation", $"AreaId={originRow["AREAID"].ToString()}", $"AreaName={originRow["AREANAME"].ToString()}");//사용자가 등록된 작업장의 측정결과만 입력가능합니다.
            }
            /*
            DataTable dt = (grdSpec.DataSource as DataTable).Clone();
            DataRow row = dt.NewRow();
            row.ItemArray = originRow.ItemArray.Clone() as object[];

            if (row == null) return;

            EtchingRateMeasurePopup popup = new EtchingRateMeasurePopup()
            {
                StartPosition = FormStartPosition.CenterParent,
                Owner = this
            };

            popup.CurrentDataRow = row;
            popup.AddNewRow();

            if (popup.ShowDialog() == DialogResult.OK)
            {
                _addMeasure = popup.CurrentDataRow;
                grdMeasure.View.AddNewRow();
            }*/
        }
        #endregion

        #region 두번째 탭 이벤트
        /// <summary>
        /// grdReMeasure 더블클릭 => 저장전 해당 row 수정 가능 / 저장 후 팝업 조회만 가능
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_GrdReMeasure_DoubleClick(object sender, EventArgs e)
        {
            DataRow originRow = grdReMeasure.View.GetFocusedDataRow();

            if (originRow == null) return;

            DataTable dt = (grdReMeasure.DataSource as DataTable).Clone();
            DataRow row = dt.NewRow();
            row.ItemArray = originRow.ItemArray.Clone() as object[];

            if (row == null) return;

            EtchingRateReMeasurePopup popup = new EtchingRateReMeasurePopup()
            {
                StartPosition = FormStartPosition.CenterParent,
                Owner = this
            };

            popup.CurrentDataRow = row;
            popup.AddNewRow();

            if (string.IsNullOrWhiteSpace(row["DEGREE"].ToString()))
            {//저장되기 전  row   
                popup.UpdateReMeasure(true);

                if (popup.ShowDialog() == DialogResult.OK)
                {
                    _addReMeasure = popup.CurrentDataRow;
                    BindingReMeasure(originRow);
                }
            }
            else
            {//저장된 후  row
                popup.UpdateReMeasure(false);
                popup.ShowDialog();
            }
        }

        /// <summary>
        /// grdAbnormal 더블클릭 => 팝업 결과 grdAbnormal에 컬럼 채워주기, 하단그리드에 ROW 추가
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_GrdAbnormal_DoubleClick(object sender, EventArgs e)
        {
            if (CheckLastResult() == false) return;
            
            DataRow originRow = grdAbnormal.View.GetFocusedDataRow();
            if (originRow == null) return;

            if (Format.GetString(originRow["ISMODIFY"]).Equals("Y"))
            {
                DataTable dt = (grdAbnormal.DataSource as DataTable).Clone();
                DataRow row = dt.NewRow();
                row.ItemArray = originRow.ItemArray.Clone() as object[];
                if (row == null) return;

                EtchingRateReMeasurePopup popup = new EtchingRateReMeasurePopup()
                {
                    StartPosition = FormStartPosition.CenterParent,
                    Owner = this
                };

                popup.CurrentDataRow = row;
                popup.AddNewRow();
                popup.SetReMeasureDate();

                if (popup.ShowDialog() == DialogResult.OK)
                {
                    _addReMeasure = popup.CurrentDataRow;
                    //BindingAbnormal();
                    grdReMeasure.View.AddNewRow();
                }
            }
            else
            {
                ShowMessage("EtchingMeasureAreaValidation", $"AreaId={originRow["AREAID"].ToString()}", $"AreaName={originRow["AREANAME"].ToString()}");//사용자가 등록된 작업장의 측정결과만 입력가능합니다.
            }

        }
        #endregion

        #endregion

        #region 툴바

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {/*
            base.OnToolbarSaveClick();

            DataTable changed = null;
            // TODO : 저장 Rule 변경
            if (tabEtchingRate.SelectedTabPageIndex == 0)
            {//첫번째 탭
                changed = grdMeasure.GetChangedRows();
                ExecuteRule("SaveEtchingRateMeasure", changed);
            }
            else
            {//두번째 탭
                changed = grdReMeasure.GetChangedRows();
                ExecuteRule("SaveEtchingRateReMeasure", changed);
            }*/

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

            // TODO : 조회 SP 변경
            SearchData();
           
        }

        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용

            InitializeConditionPopup_ProcessSegment();
            InitializeConditionPopup_Equipment();
            InitializeConditionPopup_ChildEquipment();
            InitializeConditionPopup_Area();
        }

        #region 조회조건 팝업 초기화

        /// <summary>
        /// 공정조회 조건 팝업
        /// </summary>
        private void InitializeConditionPopup_ProcessSegment()
        {
            var ProcessSegmentClassId = Conditions.AddSelectPopup("P_PROCESSSEGMENTCLASSID", new SqlQuery("GetProcessSegmentClass", "10001", "PROCESSSEGMENTCLASSTYPE=TopProcessSegmentClass", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PROCESSSEGMENTCLASSNAME", "PROCESSSEGMENTCLASSID")
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
        /// 작업장 조회조건
        /// </summary>
        private void InitializeConditionPopup_Area()
        {
            // 팝업 컬럼설정
            var areaPopup = Conditions.AddSelectPopup("P_AREAID", new SqlQuery("GetAuthorityUserUseArea", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"P_USERID={UserInfo.Current.Id}"), "AREANAME", "AREAID")
               .SetPopupLayout("AREA", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupLayoutForm(400, 600)
               .SetLabel("AREA")
               .SetPopupResultCount(1)
               .SetPosition(3.5)
               .SetRelationIds("P_PLANTID")
               .SetDefault(Framework.UserInfo.Current.Area, Framework.UserInfo.Current.Area);

            // 팝업 조회조건
            areaPopup.Conditions.AddTextBox("AREAIDNAME")
                .SetLabel("AREAIDNAME");

            // 팝업 그리드
            areaPopup.GridColumns.AddTextBoxColumn("AREAID", 150)
                .SetValidationKeyColumn();
            areaPopup.GridColumns.AddTextBoxColumn("AREANAME", 200);
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

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
        }

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            DataTable changed = null;
            // TODO : 유효성 로직 변경
            if (tabEtchingRate.SelectedTabPageIndex == 0)
            {//첫번째 탭
                grdMeasure.View.CheckValidation();
                changed = grdMeasure.GetChangedRows();
                CheckChange(changed);
            }
            else
            {//두번째 탭
                grdReMeasure.View.CheckValidation();
                changed = grdReMeasure.GetChangedRows();
                CheckChange(changed);
            }          
        }

        #endregion

        #region Private Function
        /// <summary>
        /// specDef를 조회하는 함수
        /// </summary>
        private void SearchSpecDef()
        {
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);

            grdSpec.DataSource = SqlExecuter.Query("SelectEtchingRateSpecEnable", "10001", values);
        }

        /// <summary>
        /// Load할 때 UserId가 속한 AreaIdList를 조회하는 함수
        /// </summary>
        private void SearchUserArea()
        {
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("P_USERID", Framework.UserInfo.Current.Id);
            values.Add("ENTERPRISEID", Framework.UserInfo.Current.Enterprise);
            values.Add("P_PLANTID", Framework.UserInfo.Current.Plant);
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable areaTable = SqlExecuter.Query("GetAuthorityUserUseArea", "10001", values);

            _userAreaList = areaTable.AsEnumerable().Select(r => r["AREAID"]).ToList();
        }

        /// <summary>
        /// 저장 할 데이터가 있는지 확인하는 함수
        /// </summary>
        /// <param name="table"></param>
        private void CheckChange(DataTable table)
        {
            if (table.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }

        /// <summary>
        /// 조회한 데이터가 있는지 확인하는 함수
        /// </summary>
        /// <param name="table"></param>
        private void CheckData(DataTable table)
        {
            if (table.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }
        }

        /// <summary>
        /// 첫번째 탭의 팝업결과를 바인딩 해주는 함수
        /// </summary>
        /// <param name="newRow"></param>
        private void BindingMeasure(DataRow newRow)
        {
            newRow["SPECSEQUENCE"] = _addMeasure["SPECSEQUENCE"];
            newRow["MEASUREDATE"] = _addMeasure["MEASUREDATE"];
            newRow["PROCESSSEGMENTCLASSNAME"] = _addMeasure["PROCESSSEGMENTCLASSNAME"];
            newRow["EQUIPMENTNAME"] = _addMeasure["EQUIPMENTNAME"];
            newRow["CHILDEQUIPMENTNAME"] = _addMeasure["CHILDEQUIPMENTNAME"];
            newRow["WORKTYPE"] = _addMeasure["WORKTYPE"];
            newRow["SPECRANGE"] = _addMeasure["SPECRANGE"];
            newRow["CONTROLRANGE"] = _addMeasure["CONTROLRANGE"];
            newRow["ETCHRATEVALUE"] = _addMeasure["ETCHRATEVALUE"];
            newRow["ETCHRATEBEFOREVALUE"] = _addMeasure["ETCHRATEBEFOREVALUE"];
            newRow["ETCHRATEAFTERVALUE"] = _addMeasure["ETCHRATEAFTERVALUE"];
            newRow["EQPTSPEED"] = _addMeasure["EQPTSPEED"];
            newRow["MEASURER"] = _addMeasure["MEASURER"];
            newRow["RESULT"] = _addMeasure["RESULT"];
            newRow["ENTERPRISEID"] = _addMeasure["ENTERPRISEID"];
            newRow["PLANTID"] = _addMeasure["PLANTID"];
            newRow["AREAID"] = _addMeasure["AREAID"];
            newRow["AREANAME"] = _addMeasure["AREANAME"];
        }

        /// <summary>
        /// grdAbnormal를 조회하는 함수
        /// </summary>
        /// <param name="flag"></param>
        private DataTable SearchAbnormal()
        {
            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);

            DataTable dt = SqlExecuter.Query("SelectEtchingRateAbnormal", "10001", values);

            return dt;
        }

        /// <summary>
        /// grdReMeasure 그리드를 조회하는 함수
        /// </summary>
        private void SeartchReMeasure()
        {
            DataRow row = grdAbnormal.View.GetFocusedDataRow();

            if (row == null) return;

            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("SPECSEQUENCE", row["SPECSEQUENCE"]);
            values.Add("MEASUREDATE", row["MEASUREDATE"]);
            values.Add("MEASUREDEGREE", row["MEASUREDEGREE"]);
            values.Add("ENTERPRISEID", row["ENTERPRISEID"]);
            values.Add("PLANTID", row["PLANTID"]);
            values.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);

            DataTable dt = SqlExecuter.Query("SelectEtchingRateReMeasure", "10001", values);
            grdReMeasure.DataSource = dt;
        }

        /// <summary>
        /// 두번째 탭의 팝업결과를 grdAbnormal 그리드에(상단) 컬럼에 바인딩 해주는 함수
        /// </summary>
        private void BindingAbnormal()
        {
            DataRow row = grdAbnormal.View.GetFocusedDataRow();

            row["REMEASUREDATE"] = _addReMeasure["REMEASUREDATE"];
            row["REETCHRATEVALUE"] = _addReMeasure["REETCHRATEVALUE"];
            row["REMEASURER"] = _addReMeasure["REMEASURER"];
            row["ACTIONRESULT"] = _addReMeasure["ACTIONRESULT"];
            row["RERESULT"] = _addReMeasure["RERESULT"];
        }

        /// <summary>
        /// 두번째 탭의 팝업결과를 grdReMeasure그리드에(하단) 바인딩 해주는 함수
        /// </summary>
        /// <param name="row"></param>
        private void BindingReMeasure(DataRow newRow)
        {
            newRow["SPECSEQUENCE"] = _addReMeasure["SPECSEQUENCE"];
            newRow["MEASUREDATE"] = _addReMeasure["MEASUREDATE"];
            newRow["MEASUREDEGREE"] = _addReMeasure["MEASUREDEGREE"];
            newRow["PROCESSSEGMENTCLASSNAME"] = _addReMeasure["PROCESSSEGMENTCLASSNAME"];
            newRow["EQUIPMENTNAME"] = _addReMeasure["EQUIPMENTNAME"];
            newRow["CHILDEQUIPMENTNAME"] = _addReMeasure["CHILDEQUIPMENTNAME"];
            newRow["WORKTYPE"] = _addReMeasure["WORKTYPE"];
            newRow["SPECRANGE"] = _addReMeasure["SPECRANGE"];
            newRow["CONTROLRANGE"] = _addReMeasure["CONTROLRANGE"];
            newRow["REMEASUREDATE"] = _addReMeasure["REMEASUREDATE"];
            newRow["REETCHRATEVALUE"] = _addReMeasure["REETCHRATEVALUE"];
            newRow["REETCHRATEBEFOREVALUE"] = _addReMeasure["REETCHRATEBEFOREVALUE"];
            newRow["REETCHRATEAFTERVALUE"] = _addReMeasure["REETCHRATEAFTERVALUE"];
            newRow["REMEASURER"] = _addReMeasure["REMEASURER"];
            newRow["RERESULT"] = _addReMeasure["RERESULT"];
            newRow["ACTIONRESULT"] = _addReMeasure["ACTIONRESULT"];
            newRow["ENTERPRISEID"] = _addReMeasure["ENTERPRISEID"];
            newRow["PLANTID"] = _addReMeasure["PLANTID"];
            //파일
            newRow["FILEID"] = _addReMeasure["FILEID"];
            newRow["FILENAME"] = _addReMeasure["FILENAME"];
            newRow["FILESIZE"] = _addReMeasure["FILESIZE"];
            newRow["FILEEXT"] = _addReMeasure["FILEEXT"];
            newRow["FILEPATH"] = _addReMeasure["FILEPATH"];
            newRow["SAFEFILENAME"] = _addReMeasure["SAFEFILENAME"];
            newRow["LOCALFILEPATH"] = _addReMeasure["LOCALFILEPATH"];
            newRow["FILEFULLPATH"] = _addReMeasure["FILEFULLPATH"];
            newRow["PROCESSINGSTATUS"] = _addReMeasure["PROCESSINGSTATUS"];
        }

        /// <summary>
        /// 마지막 재측정 값이 Y일 경우 재측정 결과를 입력할 수 없다
        /// </summary>
        /// <returns></returns>
        private bool CheckLastResult()
        {
            DataTable dt = grdReMeasure.DataSource as DataTable;

            if (dt.Rows.Count == 0)
            {
                return true;
            }
            else
            {
                var result = dt.AsEnumerable()
                    .Select(r => r["RERESULT"])
                    .ToList();

                if (result.Contains("OK"))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

        }

        /// <summary>
        /// grdSpec의 선택된 row로 grdMeasure 데이타를 필터링 하여 바인딩하는 함수
        /// </summary>
        private void FilterGrdMeasureData()
        {
            DataRow row = grdSpec.View.GetFocusedDataRow();
            DataTable bindingTable;
            if (row == null) return;
            string specSequence = row["SPECSEQUENCE"].ToString();

            var filteredData = _grdMeasureTable.AsEnumerable().Where(r => r["SPECSEQUENCE"].ToString().Equals(specSequence)).ToList();

            if (filteredData.Count > 0)
            {
                bindingTable = filteredData.CopyToDataTable();
                grdMeasure.DataSource = bindingTable;
            }
            else
            {
                grdMeasure.View.ClearDatas();
            }
        }


        /// <summary>
        /// 저장 함수
        /// </summary>
        /// <param name="strtitle"></param>
        private void Btn_SaveClick(string strtitle)
        {
            DataTable changed = null;
            string ruleName = "";
           
            if (tabEtchingRate.SelectedTabPageIndex == 0)
            {//첫번째 탭
                grdMeasure.View.CloseEditor();
                grdMeasure.View.CheckValidation();
                changed = grdMeasure.GetChangedRows();
                ruleName = "SaveEtchingRateMeasure";
               // ExecuteRule("SaveEtchingRateMeasure", changed);
            }
            else
            {//두번째 탭
                grdReMeasure.View.CloseEditor();
                grdReMeasure.View.CheckValidation();
                changed = grdReMeasure.GetChangedRows();
                ruleName = "SaveEtchingRateReMeasure";
                // ExecuteRule("SaveEtchingRateReMeasure", changed);
            }


            if (changed.Rows.Count == 0)
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

                    if (tabEtchingRate.SelectedTabPageIndex == 1)
                    {
                        DataTable fileUploadTable = QcmImageHelper.GetImageFileTable();
                        int totalFileSize = 0;
                        foreach (DataRow originRow in changed.Rows)
                        {
                            if (!originRow.IsNull("FILEID"))
                            {
                                DataRow newRow = fileUploadTable.NewRow();
                                newRow["FILEID"] = originRow["FILEID"];  
                                newRow["FILENAME"] = originRow["FILENAME"];
                                newRow["FILEEXT"] = originRow.GetString("FILEEXT").Replace(".", "");//파일업로드시에는 확장자에서 . 을 빼야 한다.
                                newRow["FILEPATH"] = originRow["FILEPATH"];// originRow["FILEFULLPATH1"];
                                newRow["SAFEFILENAME"] = originRow["FILENAME"];
                                newRow["FILESIZE"] = originRow["FILESIZE"];
                                newRow["LOCALFILEPATH"] = originRow["FILEFULLPATH"];
                                newRow["PROCESSINGSTATUS"] = "Wait";
                                newRow["SEQUENCE"] = 1;


                                //YJKIM : 서버에서 데이타베이스를 입력하기 위해 파일아이디를 전달해야 한다.
                                totalFileSize += originRow.GetInteger("FILESIZE");

                                fileUploadTable.Rows.Add(newRow);
                            }
                        }

                        if (fileUploadTable.Rows.Count > 0)
                        {
                            FileProgressDialog fileProgressDialog = new FileProgressDialog(fileUploadTable, UpDownType.Upload, "", totalFileSize);
                            fileProgressDialog.ShowDialog(this);

                            if (fileProgressDialog.DialogResult == DialogResult.Cancel)
                                throw MessageException.Create("CancelMessageToUploadFile");  //파일업로드를 취소하였습니다.

                            ProgressingResult fileResult = fileProgressDialog.Result;

                            if (!fileResult.IsSuccess)
                                throw MessageException.Create("FailMessageToUploadFile"); //파일업로드를 실패하였습니다.
                        }
                    }

                    ExecuteRule(ruleName, changed);
                    ShowMessage("SuccedSave");
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
        /// 데이터 검색 함수
        /// </summary>
        private void SearchData()
        {
            // TODO : 조회 SP 변경
            SearchSpecDef();
            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);

            _grdMeasureTable = SqlExecuter.Query("SelectEtchingRateMeasure", "10001", values);
            FilterGrdMeasureData();

            DataTable dt1 = SearchAbnormal();
            grdAbnormal.DataSource = dt1;
            SeartchReMeasure();

            if (tabEtchingRate.SelectedTabPageIndex == 0)
            {
                CheckData(_grdMeasureTable);
            }
            else
            {
                CheckData(dt1);
            }
        }
        #endregion
    }
}