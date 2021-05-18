#region using

using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Grid.BandedGrid;
using Micube.SmartMES.StandardInfo.Popup;
using System;
using Micube.SmartMES.Commons.Controls;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프 로 그 램 명  : 기준정보 > 품질 기준 정보 > 검사기준정보
    /// 업  무  설  명  : 검사 기준 정보 연계정보를 관리 한다
    /// 생    성    자  : 강유라
    /// 생    성    일  : 2019-06-18
    /// 수  정  이  력  : 2019-07-12 , 강유라, 화면설계 수정후 재작업
    /// 
    /// 
    /// </summary>
    public partial class InspectionMasterInfoManagement : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가
        private DataRow _rowInspection = null; //inpection그리드의 Focused Row가 변경될 때 선택된 Row를 담을 변수 , Rel그리드 조회시 파라미터로 필요
        private DataRow _rowResource = null; //resource그리드의 Focused Row가 변경될 때 선택된 Row를 담을 변수 , Rel그리드 조회시 파라미터로 필요
        private DataRow _rowInspItemclass = null; //inspItemClass그리드의 Focused Row가 변경될 때 선택된 Row를 담을 변수 , Rel그리드 조회시 파라미터로 필요
        private DataRow _rowRel = null; //Rel그리드의 Focused Row가 변경될 때 선택된 Row를 담을 변수 , point 그리드 조회시 파라미터로 필요
        private string _inspectionClassType = "RawInspection"; //조회조건에 따라 다른 팝업쿼리를 실행하기 위한 조건

        #endregion

        #region
        /// <summary>
        /// inspectionClassType 코드로 관리되기때문에 코드로 등록 된 대로 대소문자 구분하여 등록
        /// </summary>
        private enum InspectionClassType { RawInspection, SubassemblyInspection, SubsidiaryInspection, OSPInspection,
            SelfInspectionTake, SelfInspectionShip, ReliabilityInspection, FinishInspection,
            ShipmentInspection, OperationInspection, MeasurementInspection }
        #endregion

        #region 생성자

        public InspectionMasterInfoManagement()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 화면의 컨텐츠 영역을 초기화한다.
        /// </summary>
        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeEvent();

            // TODO : 컨트롤 초기화 로직 구성
            InitializeGrdInspection();
            InitializeGrdRel();
            InitializeGrdResource();
            InitializeGrdInspItemClass(_inspectionClassType);
            
        }

        #region 검사 종류 리스트 그리드를 초기화한다.
        /// <summary>        
        /// 검사 종류 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrdInspection()
        {
            if (grdInspection.View.IsInitializeColumns)
            {
                grdInspection.View.ClearColumns();
            }

            // TODO : 그리드 초기화 로직 추가
            grdInspection.GridButtonItem = GridButtonItem.None;
            grdInspection.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;
            grdInspection.View.SetIsReadOnly();

            grdInspection.View.AddTextBoxColumn("RESOURCETYPE", 100);
            grdInspection.View.AddTextBoxColumn("RESOURCEID", 100);
            grdInspection.View.AddTextBoxColumn("RESOURCEVERSION", 100);
            grdInspection.View.AddTextBoxColumn("RESOURCENAME", 250);
            grdInspection.View.AddTextBoxColumn("SEGMENTTYPE", 100);
            grdInspection.View.AddTextBoxColumn("PROCESSSEGMENTID", 100);
            grdInspection.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 250);



            grdInspection.View.PopulateColumns();
        }
        #endregion

        #region 검사 품목 리스트 그리드를 초기화한다.
        /// <summary>        
        /// 검사품목 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrdResource()
        {
            // TODO : 그리드 초기화 로직 추가
            grdResource.GridButtonItem = GridButtonItem.None;
            grdResource.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;
            
            grdResource.View.AddTextBoxColumn("PARENTINSPITEMCLASSID", 100).SetIsReadOnly();
            grdResource.View.AddTextBoxColumn("PARENTINSPITEMCLASSNAME", 200).SetIsReadOnly();
            grdResource.View.AddTextBoxColumn("INSPITEMCLASSID", 100).SetIsReadOnly();
            grdResource.View.AddTextBoxColumn("INSPITEMCLASSNAME", 200).SetIsReadOnly();

            grdResource.View.AddTextBoxColumn("INSPITEMID", 100).SetIsReadOnly();
            grdResource.View.AddTextBoxColumn("INSPITEMNAME", 200).SetIsReadOnly();
            grdResource.View.AddComboBoxColumn("INSPITEMTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=InspItemType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
            .SetIsReadOnly();
            grdResource.View.PopulateColumns();
        }
        /// <summary>
        /// 검사 품목을 선택하는 팝업 / inspectionClassType 별로 자재/ 품목 조회 
        /// </summary>
        private void InitializeGrdRaw_ResourcePopup()
        {
            //팝업 컬럼 설정
            var consumableDefId = grdResource.View.AddSelectPopupColumn("RESOURCEID", new SqlQuery("GetResourceList", "10001", $"INSPECTIONCLASSTYPE={_inspectionClassType}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                            .SetPopupLayout("RESOURCE", PopupButtonStyles.Ok_Cancel, true, false)
                                            .SetPopupResultCount(1)
                                            .SetLabel("RESOURCE")
                                            .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                                            .SetValidationKeyColumn()
                                            .SetPopupResultMapping("RESOURCEID", "ID")
                                            .SetPopupApplySelection((selectedRows, dataGridRow) =>
                                            {
                                                // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
                                                // dataGridRow : 현재 Focus가 있는 그리드의 DataRow

                                                foreach (DataRow row in selectedRows)
                                                {
                                                    dataGridRow["RESOURCENAME"] = row["NAME"].ToString();
                                                    dataGridRow["RESOURCEVERSION"] = row["VERSION"].ToString();
                                                }

                                                _rowResource = dataGridRow;

                                            });

            consumableDefId.Conditions.AddTextBox("ID");

            consumableDefId.Conditions.AddTextBox("NAME");

            // 팝업 그리드
            consumableDefId.GridColumns.AddTextBoxColumn("ID", 150);
            consumableDefId.GridColumns.AddTextBoxColumn("NAME", 200);
            consumableDefId.GridColumns.AddTextBoxColumn("VERSION", 200);
        }
        #endregion

        #region 검사 방법 리스트 그리드를 초기화한다.

        /// <summary>        
        /// 검사 방법 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrdInspItemClass(string inspectionClassType)
        {
            if (grdInspItemClass.View.IsInitializeColumns)
            {
                grdInspItemClass.View.ClearColumns();
            }

            // TODO : 그리드 초기화 로직 추가
            grdInspItemClass.GridButtonItem = GridButtonItem.Add;
            grdInspItemClass.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;

            //Relation을 위해 파라미터로 넘겨줄 컬럼
            grdInspItemClass.View.AddTextBoxColumn("INSPECTIONCLASSID", 150)
                .SetIsHidden();

            if (inspectionClassType.Equals(InspectionClassType.RawInspection.ToString()))
            {//검사 방법을 팝업으로 선택하는 경우 : 원자재
                grdInspItemClass.View.AddTextBoxColumn("PARENTINSPITEMCLASSID", 150)
                    .SetIsReadOnly();

                grdInspItemClass.View.AddTextBoxColumn("PARENTINSPITEMCLASSNAME", 200)
                    .SetIsReadOnly();

                InitializeGrdRawInspItem_Popup();

            }
            else
            {//검사 방법을 콤보박스로 선택하는 경우
                grdInspItemClass.View.AddComboBoxColumn("INSPITEMCLASSID", 150, new SqlQuery("GetInspItemByInspectionClassId", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "INSPITEMCLASSID", "INSPITEMCLASSID")
                    .SetRelationIds("INSPECTIONCLASSID")
                    .SetValidationKeyColumn();
            }

            grdInspItemClass.View.AddTextBoxColumn("INSPITEMCLASSNAME", 200)
                .SetIsReadOnly();

            grdInspItemClass.View.PopulateColumns();
        }
        /// <summary>
        /// INSPITEMCLASSID를 검색하는 팝업
        /// </summary>
        private void InitializeGrdRawInspItem_Popup()
        {
            var inspItemclassId = grdInspItemClass.View.AddSelectPopupColumn("INSPITEMCLASSID", 150, new SqlQuery("GetInspItemByInspectionClassId", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetPopupLayout("INSPITEMCLASS", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupResultCount(1)
                .SetRelationIds("INSPECTIONCLASSID")
                .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                .SetValidationKeyColumn()
                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {
                    // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
                    // dataGridRow : 현재 Focus가 있는 그리드의 DataRow
                    foreach (DataRow row in selectedRows)
                    {
                        dataGridRow["PARENTINSPITEMCLASSID"] = row["PARENTINSPITEMCLASSID"].ToString();
                        dataGridRow["PARENTINSPITEMCLASSNAME"] = row["PARENTINSPITEMCLASSNAME"].ToString();
                        dataGridRow["INSPITEMCLASSNAME"] = row["INSPITEMCLASSNAME"].ToString();
                    }

                    FocusedRowChange_grdInspItem(dataGridRow);
                });

            inspItemclassId.Conditions.AddTextBox("INSPITEMCLASSID");

            inspItemclassId.Conditions.AddTextBox("INSPITEMCLASSNAME");

            inspItemclassId.Conditions.AddTextBox("INSPECTIONCLASSID")
                .SetPopupDefaultByGridColumnId("INSPECTIONCLASSID")
                .SetIsHidden();

            inspItemclassId.GridColumns.AddTextBoxColumn("PARENTINSPITEMCLASSID", 150)
                .SetIsReadOnly();
            inspItemclassId.GridColumns.AddTextBoxColumn("PARENTINSPITEMCLASSNAME", 150)
                .SetIsReadOnly();
            inspItemclassId.GridColumns.AddTextBoxColumn("INSPITEMCLASSID", 150)
                .SetIsReadOnly();
            inspItemclassId.GridColumns.AddTextBoxColumn("INSPITEMCLASSNAME", 150)
                .SetIsReadOnly();
        }

        #endregion

        #region 검사 항목 연계 리스트 그리드를 초기화한다.

        /// <summary>        
        /// 검사항목 연계 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrdRel()
        {
            //if (grdInspItem.View.IsInitializeColumns)
            //{
            //    grdInspItem.View.ClearColumns();
            //}

            // TODO : 그리드 초기화 로직 추가
            grdInspItemRel.GridButtonItem = GridButtonItem.None;
            //grdInspItem.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            //grdInspItem.View.CheckMarkSelection.MultiSelectCount = 1;

            //inspectionItemRel, InspectionAttribute 테이블의 PK
            grdInspItemRel.View.AddTextBoxColumn("INSPITEMCLASSID", 150)
            .SetIsReadOnly();

            grdInspItemRel.View.AddTextBoxColumn("INSPITEMID", 150)
            .SetIsReadOnly();

            grdInspItemRel.View.AddTextBoxColumn("INSPITEMNAME", 150)
            .SetIsReadOnly();

            grdInspItemRel.View.AddTextBoxColumn("INSPITEMVERSION", 150)
            .SetIsReadOnly();

            grdInspItemRel.View.AddTextBoxColumn("INSPECTIONDEFID", 150)
           .SetIsReadOnly();

            grdInspItemRel.View.AddTextBoxColumn("INSPECTIONDEFVERSION", 150)
           .SetIsReadOnly();

            grdInspItemRel.View.AddTextBoxColumn("RESOURCETYPE", 150)
            .SetIsReadOnly();

            grdInspItemRel.View.AddTextBoxColumn("RESOURCEID", 150)
            .SetIsReadOnly();

            grdInspItemRel.View.AddTextBoxColumn("RESOURCEVERSION", 150)
            .SetIsReadOnly();

            grdInspItemRel.View.AddTextBoxColumn("SPECCLASSID", 150).SetIsReadOnly();
            grdInspItemRel.View.AddTextBoxColumn("SPECSEQUENCE", 150).SetIsReadOnly();
            grdInspItemRel.View.AddTextBoxColumn("ISINSPECTIONREQUIRED", 150).SetIsReadOnly();
            grdInspItemRel.View.AddTextBoxColumn("INSPECTORDEGREE", 150).SetIsReadOnly();
            grdInspItemRel.View.AddTextBoxColumn("ISAQL", 150).SetIsReadOnly();
            grdInspItemRel.View.AddTextBoxColumn("AQLINSPECTIONLEVEL", 150).SetIsReadOnly();
            grdInspItemRel.View.AddSpinEditColumn("AQLDEFECTLEVEL", 150).SetIsReadOnly();

            grdInspItemRel.View.AddTextBoxColumn("AQLDECISIONDEGREE", 150).SetIsReadOnly();
            grdInspItemRel.View.AddTextBoxColumn("AQLCYCLE", 150).SetIsReadOnly();
            grdInspItemRel.View.AddTextBoxColumn("ISNCR", 150).SetIsReadOnly();
            grdInspItemRel.View.AddTextBoxColumn("NCRINSPECTIONQTY", 150).SetIsReadOnly();
            grdInspItemRel.View.AddTextBoxColumn("NCRCYCLE", 150).SetIsReadOnly();
            grdInspItemRel.View.AddTextBoxColumn("NCRDECISIONDEGREE", 150).SetIsReadOnly();
            grdInspItemRel.View.AddTextBoxColumn("NCRDEFECTRATE", 150).SetIsReadOnly();
            grdInspItemRel.View.AddTextBoxColumn("NCRLOTSIZE", 150).SetIsReadOnly();
            grdInspItemRel.View.AddTextBoxColumn("QTYUNIT", 150).SetIsReadOnly();
            grdInspItemRel.View.AddTextBoxColumn("INSPECTIONUNIT", 150).SetIsReadOnly();

            grdInspItemRel.View.AddTextBoxColumn("PLNATID", 100)
            .SetIsReadOnly(); 

            grdInspItemRel.View.AddTextBoxColumn("ENTERPRISEID", 100)
            .SetIsReadOnly();

            //inspectionItemRel 테이블
            grdInspItemRel.View.AddTextBoxColumn("VALIDTYPE", 100);

            //inspectionItemRel 단위 CODECLASSID 수정
            grdInspItemRel.View.AddComboBoxColumn("UNIT", 80, new SqlQuery("GetUOMList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "UOMDEFNAME", "UOMDEFID");

            grdInspItemRel.View.PopulateColumns();
        }
        #endregion

        #region 측정 시기 리스트 그리드를 초기화한다.
        /// <summary>
        /// 측정시기 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrdPoint()
        {
            if (grdInspectionPoint.View.IsInitializeColumns)
            {
                grdInspectionPoint.View.ClearColumns();
            }

            grdInspectionPoint.GridButtonItem = GridButtonItem.Add;
            grdInspectionPoint.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;

            //PK
            grdInspectionPoint.View.AddTextBoxColumn("INSPECTIONPOINTID", 100)
                .SetIsHidden();

            grdInspectionPoint.View.AddTextBoxColumn("INDPITEMID", 100)
                .SetIsHidden();

            grdInspectionPoint.View.AddTextBoxColumn("INSPITEMVERSION", 100)
                .SetIsHidden();

            grdInspectionPoint.View.AddTextBoxColumn("INSPITEMCLASSID", 100)
                .SetIsHidden();

            grdInspectionPoint.View.AddTextBoxColumn("INSPECTIONDEFID", 100)
                .SetIsHidden();

            grdInspectionPoint.View.AddTextBoxColumn("INSPECTIONDEFVERSION", 100)
                .SetIsHidden();

            grdInspectionPoint.View.AddTextBoxColumn("RESOURCEID", 100)
                .SetIsHidden();

            grdInspectionPoint.View.AddTextBoxColumn("RESOURCEVERSION", 100)
                .SetIsHidden();

            grdInspectionPoint.View.AddTextBoxColumn("RESOURCETYPE", 100)
                .SetIsHidden();

            grdInspectionPoint.View.AddTextBoxColumn("ENTERPRISEID", 100)
                .SetIsHidden();

            grdInspectionPoint.View.AddTextBoxColumn("PLANTID", 100)
                .SetIsHidden();

            grdInspectionPoint.View.AddComboBoxColumn("INSPECTIONPOINTNAME", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=InspectionPointName", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            grdInspectionPoint.View.AddSpinEditColumn("INSPECTIONQTY", 100);

            grdInspectionPoint.View.AddSpinEditColumn("POINTQTY", 100);

            grdInspectionPoint.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
               .SetValidationIsRequired()
               .SetDefault("Valid")
               .SetTextAlignment(TextAlignment.Center);

            grdInspectionPoint.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdInspectionPoint.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdInspectionPoint.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdInspectionPoint.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);


            grdInspectionPoint.View.PopulateColumns();
        }
        #endregion

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            //Spec 버튼 클릭시 이벤트
            btnToSpec.Click += BtnToSpec_Click;

            Load += InspectionMasterInfoManagement_Load;

            //grdInspection 그리드의 Focused Row가 바뀔 때 이벤트
            grdInspection.View.FocusedRowChanged += GrdInspection_FocusedRowChanged;
            //grdResource 그리드의 Row추가시 확인 이벤트
            grdResource.ToolbarAddingRow += View_ToolbarAddingRow;
            //grdResource 그리드의 Focused Row가 바뀔 때 이벤트
            //grdResource.View.FocusedRowChanged += GrdResource_FocusedRowChanged;
            //grdInspItemClass 그리드의 Row추가시 확인 이벤트
            grdInspItemClass.ToolbarAddingRow += View_ToolbarAddingRow;
            //grdInspItemClass 그리드의 새 row 추가 이벤트
            grdInspItemClass.View.AddingNewRow += View_GrdRawInspItemClass_AddingNewRow;
            //grdInspItemClass 그리드의 Focused Row가 바뀔 때 이벤트
            grdInspItemClass.View.FocusedRowChanged += View_GrdInspItemClass_FocusedRowChanged;
            //grdInspItemClass 그리드의 CellValue가 바뀔 때 이벤트 
            grdInspItemClass.View.CellValueChanged += View_GrdInspItemClass_CellValueChanged;
            //grdInspItem 그리드의 Focused Row가 바뀔 때 이벤트
            grdInspItemRel.View.FocusedRowChanged += View_GrdRel_FocusedRowChanged;
            //grdInspectionPoint 그리드의 새 row 추가 이벤트
            grdInspectionPoint.View.AddingNewRow += View_GrdPoint_AddingNewRow;
            //grdInspectionPoint 그리드의 Row추가시 확인 이벤트
            grdInspectionPoint.ToolbarAddingRow += GrdPoint_ToolbarAddingRow;
            //grdInspItem 더블 클릭이벤트
            grdInspItemRel.View.DoubleClick += View_DoubleClick;
        
        }


        /// <summary>
        /// 그리드를 더블 클릭했을 때 specDetail을 조회화는 팝업
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_DoubleClick(object sender, EventArgs e)
        {
            DXMouseEventArgs ea = e as DXMouseEventArgs;
            SmartBandedGridView view = sender as SmartBandedGridView;
            GridHitInfo info = view.CalcHitInfo(ea.Location);

            DataRow chkData = grdInspItemRel.View.GetDataRow(info.RowHandle);
      
            if (chkData == null) return;

            DialogManager.ShowWaitArea(pnlContent);

            SpecRegisterDetailPopUp popup = new SpecRegisterDetailPopUp();
            popup.SetControlHidden();
            popup._specSequence = chkData["SPECSEQUENCE"].ToString();
            popup._specClassId = chkData["SPECCLASSID"].ToString();
            popup._cboCheck = chkData["DEFAULTCHARTTYPE"].ToString();//hc.kim  데이터 넘김 
            popup.Owner = this;
            popup.ShowDialog();

            DialogManager.CloseWaitArea(pnlContent);
        }


        private void InspectionMasterInfoManagement_Load(object sender, EventArgs e)
        {
            tpgInspectionPoint.PageVisible = false;
        }

        /// <summary>
        /// Inspection 그리드의 Inspection정보 를 파라미터로 하여 등록된 Resource 정보를 조회하는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdInspection_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //SmartBandedGridView grid = sender as SmartBandedGridView;
            //if (grid == null) return;

            //_rowInspection = null;
            //_rowInspection = grid.GetFocusedDataRow();
            //if (_rowInspection == null) return;

            //SearchResource();

            DataRow rowInspection = grdInspection.View.GetFocusedDataRow();
            if(rowInspection != null)
            {
                var values = Conditions.GetValues();
                values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                values.Add("RESOURCEID", rowInspection["RESOURCEID"]);
                values.Add("RESOURCEVERSION", rowInspection["RESOURCEVERSION"]);

                values.Add("SEGMENTTYPE", rowInspection["SEGMENTTYPE"]);
                values.Add("PROCESSSEGID", rowInspection["PROCESSSEGMENTID"]);

                grdInspItemRel.View.ClearDatas();
                grdInspItemRel.DataSource = SqlExecuter.Query("SelectInspectionItemRelByInspItemClass", "10002", values);

                
                Dictionary<string, object> Param = new Dictionary<string, object>();
                Param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                Param.Add("INSPITEMCLASSTYPE", values["P_INSPECTIONCLASSTYPE"]);
                Param.Add("PARENTINSPITEMCLASSID", rowInspection["RESOURCEVERSION"].ToString());

                grdResource.DataSource = SqlExecuter.Query("GetInspectionImportRawmaterialResource", "10001", Param);

            }

        }

        /// <summary>
        /// Resource 그리드의 Resource정보를 파라미터로 하여 등록된 InspItemClass 정보를 조회하는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdResource_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            SmartBandedGridView grid = sender as SmartBandedGridView;
            if (grid == null) return;
            _rowResource = grid.GetFocusedDataRow();
            if (_rowResource == null) return;

            //상위 조건이 바뀌면 하위조건 없애주기 위함
            _rowInspItemclass = null;
            grdInspItemClass.View.ClearDatas();

            grdInspItemClass.DataSource = SqlExecuter.Query("SelectInspItemClassByResourceInfo", "10001", SetSearchParameter());
        }
        /// <summary>
        /// 새로 추가 한 Row가 이미 있는 경우 메세지 띄운후 Row추가 여부를 정하는 이벤트 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_ToolbarAddingRow(object sender, CancelEventArgs e)
        {
            SmartBandedGrid grid = sender as SmartBandedGrid;
            if (grid == null) return;

            if (grid.Name.Equals(grdInspItemClass.Name) && (_inspectionClassType.Equals(InspectionClassType.RawInspection.ToString()) || _inspectionClassType.Equals(InspectionClassType.SubassemblyInspection.ToString())
                || _inspectionClassType.Equals(InspectionClassType.SubsidiaryInspection.ToString())
                || _inspectionClassType.Equals(InspectionClassType.OSPInspection.ToString()) || _inspectionClassType.Equals(InspectionClassType.FinishInspection.ToString())
                || _inspectionClassType.Equals(InspectionClassType.ShipmentInspection.ToString()) || _inspectionClassType.Equals(InspectionClassType.OperationInspection.ToString())))
            {//검사 품목이 있을 때
                DataRow row = grdResource.View.GetFocusedDataRow();
                if (row== null || string.IsNullOrWhiteSpace(row["RESOURCEID"].ToString()))
                {
                    ShowMessage("자원 정보를 우선 입력하세요.");
                    e.Cancel = true;
                    return;
                }
            }

            var rowState = (grid.DataSource as DataTable).Rows.Cast<DataRow>()
                .Select(r => r.RowState.ToString()).ToList();

            //추가 한 후 저장하지 않은 데이터가 있을 때
            if (rowState.Contains("Added"))
            {
                DialogResult result = System.Windows.Forms.DialogResult.No;

                result = this.ShowMessage(MessageBoxButtons.YesNo, "DataIsNotSaved");//새로운 항목을 추가 할 경우 입력한 정보는 저장 되지 않습니다. 계속 하시겠습니까?

                if (result == System.Windows.Forms.DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }
        /// <summary>
        /// INSPECTIONCLASSID(파라메터로 필요) 컬럼 에 값을 자동으로 넣어주는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_GrdRawInspItemClass_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            args.NewRow["INSPECTIONCLASSID"] = _rowInspection["INSPECTIONCLASSID"];
        }

        /// <summary>
        /// InspItemClass 그리드의 InspItemClass정보를 파라미터로 하여 등록된 InspectionItemRel, InspectionAttribute 정보를 조회하는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_GrdInspItemClass_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            SmartBandedGridView grid = sender as SmartBandedGridView;
            if (grid == null) return;
            _rowInspItemclass = grid.GetFocusedDataRow();
            if (_rowInspItemclass == null) return;

            //상위 조건이 바뀌면 하위조건 없애주기 위함
            _rowRel = null;
            grdInspItemRel.View.ClearDatas();

            FocusedRowChange_grdInspItem(_rowInspItemclass);
        }


        /// <summary>
        /// InspItemClassId 값이 바뀔 때 InspItemName에 값 입력,
        /// AddRow를 _InspItemRow에 할당
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_GrdInspItemClass_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName.Equals("INSPITEMCLASSID"))
            {
                SmartBandedGridView grid = sender as SmartBandedGridView;
                if (e.Column.ColumnEdit is RepositoryItemLookUpEdit lookup)
                {
                    object inspItemClassName = lookup.GetDataSourceValue("INSPITEMCLASSNAME", lookup.GetDataSourceRowIndex("INSPITEMCLASSID", e.Value));
                    grid.SetFocusedRowCellValue("INSPITEMCLASSNAME", inspItemClassName);
                }
                _rowInspItemclass = grid.GetDataRow(e.RowHandle);

                FocusedRowChange_grdInspItem(_rowInspItemclass);

            }
        }

        /// <summary>
        /// Rel 그리드의 정보를 파라미터로 하여 InspectionPoint 테이블을 조회하는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_GrdRel_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            SmartBandedGridView grid = sender as SmartBandedGridView;
            if (grid == null) return;
            _rowRel = grid.GetFocusedDataRow();
            if (_rowRel == null) return;

            if (!_inspectionClassType.Equals(InspectionClassType.RawInspection.ToString()))
            {
                grdInspectionPoint.View.ClearDatas();
            }

            grdInspectionPoint.DataSource = SqlExecuter.Query("SelectInspectionPointByRelInfo", "10001", SetSearchPointParameter());
        }


        /// <summary>
        /// Point 그리드에 새로운 Row를 추가 할때 Rel그리드의 값 넘겨주기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_GrdPoint_AddingNewRow(SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            args.NewRow["INSPITEMID"] = _rowRel["INSPITEMID"];
            args.NewRow["INSPITEMVERSION"] = _rowRel["INSPITEMVERSION"];
            args.NewRow["INSPITEMCLASSID"] = _rowRel["INSPITEMCLASSID"];
            args.NewRow["INSPECTIONDEFID"] = _rowRel["INSPECTIONDEFID"];
            args.NewRow["INSPECTIONDEFVERSION"] = _rowRel["INSPECTIONDEFVERSION"];
            args.NewRow["RESOURCEID"] = _rowRel["RESOURCEID"];
            args.NewRow["RESOURCEVERSION"] = _rowRel["RESOURCEVERSION"];
            args.NewRow["RESOURCETYPE"] = _rowRel["RESOURCETYPE"];
            args.NewRow["PLANTID"] = _rowRel["PLANTID"];
            args.NewRow["ENTERPRISEID"] = _rowRel["ENTERPRISEID"];
        }

        /// <summary>
        /// 측정시기를 입력할때 검사 연계 데이터가 저장되있지않으면(new Row) 일때 측정시기 등록을 막는 이벤트 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdPoint_ToolbarAddingRow(object sender, CancelEventArgs e)
        {
            if (_rowRel != null && string.IsNullOrWhiteSpace(_rowRel["CREATEDTIME"].ToString()))
            {
                e.Cancel = true;
                throw MessageException.Create("MustSaveRelData");
            }
        }


        /// <summary>
        /// Rel 그리드의 row하나를 선택하고 Spec버튼을 클릭했을 때 이벤트
        /// 1)Rel 의 Key 값이 없는 경우 specDefinition 테이블을 조회하여 
        /// 2)_1 등록되어있다면 SpecSequence를 inspectionitemrel 테이블에 update
        /// 2)_2 등록되어있지 않다면 체크된 Rel row를 Spec 등록하는 화면으로 이동시켜준다
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnToSpec_Click(object sender, EventArgs e)
        {
            if (grdInspItemRel.View.GetCheckedRows().Rows.Count != 1)
            {//Rel그리드의 선택된 항목이 없을 때
                this.ShowMessage("NoSelectRelDataRow");// 검사 항목 연계를 선택 해 주세요.
                return;
            }

            DataRow checkedRow = grdInspItemRel.View.GetCheckedRows().Rows[0];

            if (checkedRow.RowState.ToString().Equals("Modified"))
            {//Rel그리드의 선택된 항목이 저장되지 않았을 때
                this.ShowMessage("NeedToSaveRelDataRow");// 검사 항목 연계를 우선 저장 해 주세요.
                return;
            }


            if (string.IsNullOrWhiteSpace(checkedRow["SPECSEQUENCE"].ToString()))
            {//선택 된 Rel 그리드의 등록된 SPECSEQUENCE가 없을 때

                DataTable specTable = null;
                Dictionary<string, object> values = new Dictionary<string, object>();
                //values.Add("ENTERPRISEID", Framework.UserInfo.Current.Enterprise);

                if (_inspectionClassType.Equals(InspectionClassType.RawInspection.ToString()) || _inspectionClassType.Equals(InspectionClassType.SubassemblyInspection.ToString())
                    || _inspectionClassType.Equals(InspectionClassType.SubsidiaryInspection.ToString()))
                {//원자재 스펙 : Key : ConsumableDefId, ParentInspItemclassId, inspItemClassId, InspItemId 
                    values.Add("CONSUMABLEDEFID", checkedRow["RESOURCEID"]);
                    values.Add("INSPITEMID", checkedRow["INSPITEMID"]);
                    values.Add("INSPITEMCLASSID", checkedRow["INSPITEMCLASSID"]);
                    values.Add("PARENTINSPITEMCLASSID", checkedRow["PARENTINSPITMCLASSID"]);
                    if (_inspectionClassType.Equals(InspectionClassType.RawInspection.ToString()))
                    {//원자재
                        values.Add("SPECCLASSID", "RawSpec");
                    }
                    else if (_inspectionClassType.Equals(InspectionClassType.SubassemblyInspection.ToString()))
                    {//가공품
                        values.Add("SPECCLASSID", "SubassemblyInspection");
                    }
                    else if (_inspectionClassType.Equals(InspectionClassType.SubsidiaryInspection.ToString()))
                    {//부자재
                        values.Add("SPECCLASSID", "SubsidiaryInspection");
                    }
                }
                else if (_inspectionClassType.Equals(InspectionClassType.OSPInspection.ToString()) || _inspectionClassType.Equals(InspectionClassType.ShipmentInspection.ToString()))
                {//OSP, Shipment 스펙 : Key : ProcessSegmentClassId(중공정), ProductDefId, inspItemClassId, InspItemId 
                    values.Add("PRODUCTDEFID", checkedRow["RESOURCEID"]);
                    values.Add("INSPITEMID", checkedRow["INSPITEMID"]);
                    values.Add("INSPITEMCLASSID", checkedRow["INSPITEMCLASSID"]);
                    values.Add("PROCESSSEGMENTCLASSID", _rowInspection["INSPECTIONDEFSEGMENT"]);

                    if (_inspectionClassType.Equals(InspectionClassType.OSPInspection.ToString()))
                    {//OSP
                        values.Add("SPECCLASSID", "OSPSpec");
                    }
                    else if (_inspectionClassType.Equals(InspectionClassType.ShipmentInspection.ToString()))
                    {//Shipment
                        values.Add("SPECCLASSID", "ShipmentSpec");
                    }
                }
                else if (_inspectionClassType.Equals(InspectionClassType.OperationInspection.ToString()))
                {//Operation 스펙 : Key : ProcessSegmentId(표준공정), ProductDefId, InspItemId
                    values.Add("PRODUCTDEFID", checkedRow["RESOURCEID"]);
                    values.Add("INSPITEMID", checkedRow["INSPITEMID"]);
                    values.Add("PROCESSSEGMENTID", _rowInspection["INSPECTIONDEFSEGMENT"]);
                    values.Add("SPECCLASSID", "OperationSpec");
                }
                else if (_inspectionClassType.Equals(InspectionClassType.MeasurementInspection.ToString()))
                {//Measurement 스펙 : Key : ProcessSegmentId(표준공정), EquipmentId, inspItemClassId, InspItemId Rel에 설비정보 없는데..?
                 //HOLDING
                }

                specTable = SqlExecuter.Query("SelectSpecByRelInfo", "10001", values);

                if (specTable.Rows.Count == 0)
                {//등록 된 spec정보가 없을때
                    DialogResult result = System.Windows.Forms.DialogResult.No;

                    result = this.ShowMessage(MessageBoxButtons.YesNo, "SpecNotRegistered");//해당 정보의 Spec이 등록 되어있지 않습니다. Spec등록 화면으로 이동 하시겠습니까?

                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        try
                        {
                            DialogManager.ShowWaitDialog();

                            Dictionary<string, object> param = new Dictionary<string, object>();
                            param.Add("row", grdInspItemRel.View.GetCheckedRows().Rows[0]);
                            param.Add("inspectionClassType", _inspectionClassType);
                            param.Add("fromMenu", "Inspections");



                            this.OpenMenu("PG-SD-0440", param);
                        }
                        catch (Exception ex)
                        {
                            this.ShowError(ex);
                        }
                        finally
                        {
                            DialogManager.Close();
                        }
                    }

                }
                else
                {//등록된 spec 정보가 있을 때
                    DialogResult result = System.Windows.Forms.DialogResult.No;

                    result = this.ShowMessage(MessageBoxButtons.YesNo, "SpecSequenceConfirmNote");//등록된 Spec 정보가 있습니다. 검사 항목 연계와 연동 하시겠습니까?

                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        try
                        {
                            this.ShowWaitArea();
                            checkedRow["SPECSEQUENCE"] = specTable.Rows[0]["SPECSEQUENCE"];
                            checkedRow["SPECCLASSID"] = specTable.Rows[0]["SPECCLASSID"];
                            DataTable changed = grdInspItemRel.View.GetCheckedRows().Clone();
                            changed.ImportRow(checkedRow);
                            ExecuteRule("SaveInspectionItemAttributeSpecSequence", changed);

                            ShowMessage("SuccessSave");

                            grdInspection.View.ClearDatas();
                            SearchInspection();
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

            }
            else
            {//선택 된 Rel 그리드의 등록된 SPECSEQUENCE가 있을 때
                this.ShowMessage("HasSpecSequence");// 스펙정보가 이미 연동되었습니다.
                return;
            }
        }
        /// <summary>
        /// 코드그룹 리스트 그리드에서 추가 버튼 클릭 시 호출
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>


        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            // TODO : 저장 Rule 변경
            DataTable relChanged = null;
            DataTable pointChanged = null;

            if (_inspectionClassType.Equals(InspectionClassType.RawInspection.ToString()))
            {//품목만 있는 탭
                relChanged = SaveGrdRelWithResoueceItem(grdInspItemRel.GetChangedRows());
                relChanged.TableName = "list";
                ExecuteRule("SaveInspectionItemRelAttributePoint", relChanged);
            }
            else if (_inspectionClassType.Equals(InspectionClassType.SubassemblyInspection.ToString()) || _inspectionClassType.Equals(InspectionClassType.SubsidiaryInspection.ToString())
                || _inspectionClassType.Equals(InspectionClassType.OSPInspection.ToString()) || _inspectionClassType.Equals(InspectionClassType.FinishInspection.ToString()) 
                || _inspectionClassType.Equals(InspectionClassType.ShipmentInspection.ToString()) || _inspectionClassType.Equals(InspectionClassType.OperationInspection.ToString()))
            {//품목, 검사시기가있고 ,품목그리드의 검사 추가 정보 업데이트 해야하는 경우

                relChanged = SaveGrdRelWithResoueceItemAndUpdateInspectionInfo(grdInspItemRel.GetChangedRows());
                pointChanged = grdInspectionPoint.GetChangedRows();

                DataSet rullSet = new DataSet();

                relChanged.TableName = "list";
                pointChanged.TableName = "pointList";


                if (relChanged.Rows.Count > 0)
                {
                    rullSet.Tables.Add(relChanged);
                }

                if (pointChanged.Rows.Count > 0)
                {
                    rullSet.Tables.Add(pointChanged);
                }

                ExecuteRule("SaveInspectionItemRelAttributePointWithInspectionInfo", rullSet);

            }
            else if (_inspectionClassType.Equals(InspectionClassType.SelfInspectionTake.ToString()) || _inspectionClassType.Equals(InspectionClassType.SelfInspectionShip.ToString()) 
                || _inspectionClassType.Equals(InspectionClassType.ReliabilityInspection.ToString()))
            {//품목 없고 검사시기 만 있는 경우
                relChanged = SaveGrdRel(grdInspItemRel.GetChangedRows());
                pointChanged = grdInspectionPoint.GetChangedRows();

                DataSet rullSet = new DataSet();

                relChanged.TableName = "list";
                pointChanged.TableName = "pointList";

                if (relChanged.Rows.Count > 0)
                {
                    rullSet.Tables.Add(relChanged);
                }

                if (pointChanged.Rows.Count > 0)
                {
                    rullSet.Tables.Add(pointChanged);
                }

                ExecuteRule("SaveInspectionItemRelAttributePoint", rullSet);
            }
        }

        #endregion

        #region 검색

        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            // TODO : 조회 SP 변경
            //_rowInspection = null;
            //_rowResource = null;
            //_rowInspItemclass = null;
            //_rowRel = null;

            //_inspectionClassType = Conditions.GetValue("p_inspectionclasstype").ToString();

            SearchInspection();

            //if (_inspectionClassType.Equals(InspectionClassType.SelfInspectionTake.ToString()) || _inspectionClassType.Equals(InspectionClassType.SelfInspectionShip.ToString())
            //    || _inspectionClassType.Equals(InspectionClassType.ReliabilityInspection.ToString()) || _inspectionClassType.Equals(InspectionClassType.FinishInspection.ToString())
            //    || _inspectionClassType.Equals(InspectionClassType.MeasurementInspection.ToString()))
            //{//계측기 R&R _tabIndex ==10 빠질수도 있어서 버튼 비활성화
            //    btnToSpec.Enabled = false;
            //}
            //else
            //{
            //    btnToSpec.Enabled = true;
            //}

            //if (!_inspectionClassType.Equals(InspectionClassType.RawInspection.ToString()))
            //{
            //    tpgInspectionPoint.PageVisible = true;
            //}
            //else
            //{
            //    tpgInspectionPoint.PageVisible = false;
            //}

        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
        }

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            DataTable changed = null;
            DataTable changed2 = null;

            if (_inspectionClassType.Equals(InspectionClassType.RawInspection.ToString()))
            {//검사시기가 없는 탭 
                grdInspItemRel.View.CheckValidation();
                changed = grdInspItemRel.GetChangedRows();
                CheckChangedDataOneGrd(changed);
            }
            else
            {//검사시기가 있는 탭
                //rel 그리드
                grdInspItemRel.View.CheckValidation();
                changed = grdInspItemRel.GetChangedRows();

                //point 그리드
                grdInspectionPoint.View.CheckValidation();
                changed2 = grdInspectionPoint.GetChangedRows();
                CheckChangedDataTwoGrd(changed, changed2);

            }
        }

        #endregion

        #region Private Function

        // TODO : 화면에서 사용할 내부 함수 추가
        /// <summary>
        /// grdInspItemClass 그리드의 포커스된 Row가 바뀔 때 Rel 그리드를 조회 하는 함수
        /// </summary>
        /// <param name="row"></param>
        private void FocusedRowChange_grdInspItem(DataRow row)
        {

            
            
        }

        /// <summary>
        /// Point그리드를 입력하지 않는 경우 유효성 검사 함수
        /// </summary>
        /// <param name="table"></param>
        private void CheckChangedDataOneGrd(DataTable table)
        {
            if (table.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }


        /// <summary>
        /// Point 그리드가 있는 경우 유효성 검사 함수
        /// 검사 항목이 우선 저장된 후에 측정 포인트 저장 해야함
        /// </summary>
        /// <param name="table"></param>
        /// <param name="table2"></param>
        private void CheckChangedDataTwoGrd(DataTable table, DataTable table2)
        {
            if (table.Rows.Count == 0 && table2.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }
      


        /// <summary>
        /// inspItemClass 그리드를 조회 하기 위한 파라미터 세팅 함수
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, object> SetSearchParameter()
        {
            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);
            values.Add("P_INSPECTIONDEFID", _rowInspection["INSPECTIONDEFID"]);
            values.Add("P_INSPECTIONDEFVERSION", _rowInspection["INSPECTIONDEFVERSION"]);

            if (_inspectionClassType.Equals(InspectionClassType.RawInspection.ToString()) || _inspectionClassType.Equals(InspectionClassType.SubassemblyInspection.ToString())
                || _inspectionClassType.Equals(InspectionClassType.SubsidiaryInspection.ToString())
                || _inspectionClassType.Equals(InspectionClassType.OSPInspection.ToString()) || _inspectionClassType.Equals(InspectionClassType.FinishInspection.ToString())
                || _inspectionClassType.Equals(InspectionClassType.ShipmentInspection.ToString()) || _inspectionClassType.Equals(InspectionClassType.OperationInspection.ToString()))
            {//검사 품목이 있을 때
                values.Add("P_RESOURCEID", _rowResource["RESOURCEID"]);
                values.Add("P_RESOURCEVERSION", _rowResource["RESOURCEVERSION"]);
            }
            else
            {//검사 품목이 없을 때
                //변경**
                //values.Add("P_RESOURCEID", _rowInspection["INSPECTIONDEFID"]);
                //values.Add("P_RESOURCEVERSION", _rowInspection["INSPECTIONDEFVERSION"]);
                values.Add("P_RESOURCEID", _rowInspection["INSPECTIONDEFSEGMENT"]);
                values.Add("P_RESOURCEVERSION", _rowInspection["INSPECTIONDEFVERSION"]);
            }
            values.Add("P_RESOURCETYPE", _rowInspection["RESOURCETYPE"]);

            return values;
        }

        private void SearchInspection()
        {
            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);
            //_inspectionClassType = values["P_INSPECTIONCLASSTYPE"].ToString();

            //if (_inspectionClassType.Equals(InspectionClassType.RawInspection.ToString()) || _inspectionClassType.Equals(InspectionClassType.SubassemblyInspection.ToString())
            //    || _inspectionClassType.Equals(InspectionClassType.SubsidiaryInspection.ToString()) 
            //    || _inspectionClassType.Equals(InspectionClassType.OSPInspection.ToString()) || _inspectionClassType.Equals(InspectionClassType.FinishInspection.ToString())
            //    || _inspectionClassType.Equals(InspectionClassType.ShipmentInspection.ToString()) || _inspectionClassType.Equals(InspectionClassType.OperationInspection.ToString()))
            //{//품목이 필요한 검사의 경우 품목 탭 visible true 처리
            //    tpgResource.PageVisible = true;
            //}
            //else
            //{//품목이 필요 없는 검사의 경우 품목 탭 visible false 처리
            //    tpgResource.PageVisible = false;
            //}

            ////조회시 마다 그리드 컨트롤 재 초기화 (검사 종류마다 그리드 컬럼 조금씩 다름)
            //InitializeGrdInspection();
            //InitializeGrdResource(_inspectionClassType);
            //InitializeGrdInspItemClass(_inspectionClassType);
            //InitializeGrdRel(_inspectionClassType);

            //if (!_inspectionClassType.Equals(InspectionClassType.RawInspection.ToString()))
            //{
            //    InitializeGrdPoint();
            //}
            
            DataTable dt = SqlExecuter.Query("SelectInspectionByInspectionType", "10002", values);

            if (dt.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdInspection.View.ClearDatas();
            grdInspection.DataSource = dt;
            SearchResource();

        }

        private void SearchResource()
        {
            //_rowInspection = grdInspection.View.GetFocusedDataRow();

            //if (_inspectionClassType.Equals(InspectionClassType.RawInspection.ToString()) || _inspectionClassType.Equals(InspectionClassType.SubassemblyInspection.ToString())
            //|| _inspectionClassType.Equals(InspectionClassType.SubsidiaryInspection.ToString())
            //|| _inspectionClassType.Equals(InspectionClassType.OSPInspection.ToString()) || _inspectionClassType.Equals(InspectionClassType.FinishInspection.ToString())
            //|| _inspectionClassType.Equals(InspectionClassType.ShipmentInspection.ToString()) || _inspectionClassType.Equals(InspectionClassType.OperationInspection.ToString()))
            //{//검사 품목이 있을 때
            //    //상위 조건이 바뀌면 하위조건 없애주기 위함
            //    _rowResource = null;
            //    grdResource.View.ClearDatas();
            //    grdInspItemClass.View.ClearDatas();
            //    grdInspItem.View.ClearDatas();

            //    if (!_inspectionClassType.Equals(InspectionClassType.RawInspection.ToString()))
            //    {
            //        grdInspectionPoint.View.ClearDatas();
            //    }
            //    grdResource.DataSource = SqlExecuter.Query("SelectResourceItemByInspectionDefInfo", "10001", SetSearchResourceParameter());
            //}
            //else
            //{//검사 품목이 없을 때
            //    //상위 조건이 바뀌면 하위조건 없애주기 위함
            //    _rowInspItemclass = null;
            //    grdInspItemClass.View.ClearDatas();
            //    grdInspItem.View.ClearDatas();
            //    grdInspectionPoint.View.ClearDatas();
            //    grdInspItemClass.DataSource = SqlExecuter.Query("SelectInspItemClassByResourceInfo", "10001", SetSearchParameter());
            //}
        }

        /// <summary>
        /// 검사 품목이 있는 경우 InspectionItemRel, InspectionAttribute 테이블에
        /// 저장 하기전, 각각 그리드의 선택된 항목을 추가 해 주는 함수(품목그리드의 검사정보를 같이 저장하는 경우)
        /// </summary>
        /// <param name="changed"></param>
        /// <returns></returns>
        private DataTable SaveGrdRelWithResoueceItemAndUpdateInspectionInfo(DataTable changed)
        {
            foreach (DataRow row in changed.Rows)
            {
                row["INSPECTIONDEFID"] = _rowInspection["INSPECTIONDEFID"];
                row["INSPECTIONDEFVERSION"] = _rowInspection["INSPECTIONDEFVERSION"];
                row["RESOURCEID"] = _rowResource["RESOURCEID"];
                row["RESOURCEVERSION"] = _rowResource["RESOURCEVERSION"];

                if (_inspectionClassType.Equals(InspectionClassType.OSPInspection.ToString()) || _inspectionClassType.Equals(InspectionClassType.FinishInspection.ToString())
                || _inspectionClassType.Equals(InspectionClassType.ShipmentInspection.ToString()) || _inspectionClassType.Equals(InspectionClassType.OperationInspection.ToString()))
                {//일괄
                    row["QTYTYPE"] = _rowResource["QTYTYPE"];
                }
   
                row["INSPECTORDEGREE"] = _rowResource["INSPECTORDEGREE"];
                row["INSPECTIONLEVEL"] = _rowResource["INSPECTIONLEVEL"];
                row["DEFECTLEVEL"] = _rowResource["DEFECTLEVEL"];
                row["RESOURCETYPE"] = _rowInspection["RESOURCETYPE"];
                row["INSPITEMCLASSID"] = _rowInspItemclass["INSPITEMCLASSID"];
                row["PLANTID"] = Framework.UserInfo.Current.Plant;
                row["ENTERPRISEID"] = Framework.UserInfo.Current.Enterprise;
            }

            return changed;
        }

        /// <summary>
        /// 검사 품목이 있는 경우 InspectionItemRel, InspectionAttribute 테이블에
        /// 저장 하기전, 각각 그리드의 선택된 항목을 추가 해 주는 함수(품목그리드의 검사정보를 같이 저장하지 않을때)
        /// </summary>
        /// <param name="changed"></param>
        /// <returns></returns>
        private DataTable SaveGrdRelWithResoueceItem(DataTable changed)
        {
            foreach (DataRow row in changed.Rows)
            {
                row["INSPECTIONDEFID"] = _rowInspection["INSPECTIONDEFID"];
                row["INSPECTIONDEFVERSION"] = _rowInspection["INSPECTIONDEFVERSION"];
                row["RESOURCEID"] = _rowResource["RESOURCEID"];
                row["RESOURCEVERSION"] = _rowResource["RESOURCEVERSION"];
                row["RESOURCETYPE"] = _rowInspection["RESOURCETYPE"];
                row["INSPITEMCLASSID"] = _rowInspItemclass["INSPITEMCLASSID"];
                row["PLANTID"] = Framework.UserInfo.Current.Plant;
                row["ENTERPRISEID"] = Framework.UserInfo.Current.Enterprise;
            }

            return changed;
        }

        /// <summary>
        /// 검사 품목이 없는 경우 InspectionItemRel, InspectionAttribute 테이블에
        /// 저장 하기전, 각각 그리드의 선택된 항목을 추가 해 주는 함수
        /// </summary>
        /// <param name="changed"></param>
        /// <returns></returns>
        private DataTable SaveGrdRel(DataTable changed)
        {
            foreach (DataRow row in changed.Rows)
            {
                row["INSPECTIONDEFID"] = _rowInspection["INSPECTIONDEFID"];
                row["INSPECTIONDEFVERSION"] = _rowInspection["INSPECTIONDEFVERSION"];
                //변경**
                //row["RESOURCEID"] = _rowInspection["INSPECTIONDEFID"];
                //row["RESOURCEVERSION"] = _rowInspection["INSPECTIONDEFVERSION"];
                row["RESOURCEID"] = _rowInspection["INSPECTIONDEFSEGMENT"];
                row["RESOURCEVERSION"] = _rowInspection["INSPECTIONDEFVERSION"];
                row["RESOURCETYPE"] = _rowInspection["RESOURCETYPE"];
                row["INSPITEMCLASSID"] = _rowInspItemclass["INSPITEMCLASSID"];
                row["PLANTID"] = Framework.UserInfo.Current.Plant;
                row["ENTERPRISEID"] = Framework.UserInfo.Current.Enterprise;
            }

            return changed;
        }


        /// <summary>
        /// point 그리드를 조회 하기 위한 파라미터 세팅 함수
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, object> SetSearchPointParameter()
        {
            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);
            values.Add("P_INSPECTIONDEFID", _rowRel["INSPECTIONDEFID"]);
            values.Add("P_INSPECTIONDEFVERSION", _rowRel["INSPECTIONDEFVERSION"]);
            values.Add("P_RESOURCEID", _rowRel["RESOURCEID"]);
            values.Add("P_RESOURCEVERSION", _rowRel["RESOURCEVERSION"]);
            values.Add("P_RESOURCETYPE", _rowRel["RESOURCETYPE"]);
            values.Add("P_INSPITEMCLASSID", _rowRel["INSPITEMCLASSID"]);
            values.Add("P_INSPITEMID", _rowRel["INSPITEMID"]);
            values.Add("P_INSPITEMVERSION", _rowRel["INSPITEMVERSION"]);

            return values;
        }

        /// <summary>
        /// Resource 그리드를 조회 하기 위한 파라미터 세팅 함수
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, object> SetSearchResourceParameter()
        {
            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);
            values.Add("P_INSPECTIONDEFID", _rowInspection["INSPECTIONDEFID"]);
            values.Add("P_INSPECTIONDEFVERSION", _rowInspection["INSPECTIONDEFVERSION"]);
            values.Add("P_RESOURCETYPE", _rowInspection["RESOURCETYPE"]);

            return values;
        }
        #endregion
    }
}
