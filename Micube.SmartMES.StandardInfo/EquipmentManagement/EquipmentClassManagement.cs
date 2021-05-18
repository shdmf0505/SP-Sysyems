#region using

using DevExpress.XtraTreeList.Nodes;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Validations;
using Micube.SmartMES.Commons;
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
    /// 프 로 그 램 명  : 기준정보 > 설비 기준 정보 > 설비그룹
    /// 업  무  설  명  : 설비그룹 정보를 관리한다.
    /// 생    성    자  : 강유라
    /// 생    성    일  : 2019-05-16
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class EquipmentClassManagement : SmartConditionManualBaseForm
    {
        #region Local Variables

        string _focusedId="";//설비그룹 저장 후 설비트리의 포커스를 저장한 Row로 맟추기위한 변수 


        #endregion

        #region 생성자

        public EquipmentClassManagement()
        {
            InitializeComponent();
        }
        #endregion

        #region 컨텐츠 영역 초기화
        protected override void InitializeContent()
        {
            base.InitializeContent();
            grpBox.GridButtonItem = GridButtonItem.Refresh;
            InitializeEvent();
            InitializeGrid();
            InitializeTreeEquipmentClass();
        }
        /// <summary>
        /// 설비유형 트리 초기화
        /// </summary>
        private void InitializeTreeEquipmentClass()
        {
            treeEquipmentClass.SetResultCount(1);
            treeEquipmentClass.SetIsReadOnly();
            treeEquipmentClass.SetFocusedNode(treeEquipmentClass.FindNodeByFieldValue("NODETYPE", "ENTERPRISE"));

            Dictionary<string, object> param = new Dictionary<string, object>();
            //UserEnterprise 설정안되어있어서 임의로 지정 나중에 삭제 임시
            if (UserInfo.Current.Enterprise.Equals(""))
            {
                treeEquipmentClass.SetEmptyRoot("INTERFLEX", "INTERFLEX");
                param.Add("p_enterpriseid", "INTERFLEX");
            }
            else
            {
                treeEquipmentClass.SetEmptyRoot(UserInfo.Current.Enterprise, UserInfo.Current.Enterprise);
                param.Add("p_enterpriseid", UserInfo.Current.Enterprise);
            }
            treeEquipmentClass.SetMember("NAME", "ID", "PARENT");

            //param.Add("p_enterpriseid", UserInfo.Current.Enterprise);
            param.Add("p_languagetype", UserInfo.Current.LanguageType);
    

            treeEquipmentClass.DataSource = SqlExecuter.Query("SelectEquipmentClassManagement_Tree", "10002", param);
            treeEquipmentClass.PopulateColumns();
            treeEquipmentClass.ExpandAll();

            if (_focusedId.Equals(""))
            {
                treeEquipmentClass.SetFocusedNode(treeEquipmentClass.FindNodeByFieldValue("NODETYPE", "ENTERPRISE"));
            }
            else
            {
                treeEquipmentClass.SetFocusedNode(treeEquipmentClass.FindNodeByFieldValue("ID", _focusedId));
            }
        }

        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            this.grdEquipmentClass.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            //this.grdEquipmentClass.GridButtonItem -= GridButtonItem.Delete;

            this.grdEquipmentClass.View.AddTextBoxColumn("ENTERPRISEID", 200);                    
              //.SetIsHidden();

            this.grdEquipmentClass.View.AddTextBoxColumn("EQUIPMENTCLASSID", 150)
                .SetValidationKeyColumn();

            this.grdEquipmentClass.View.AddLanguageColumn("EQUIPMENTCLASSNAME", 200);

            this.grdEquipmentClass.View.AddTextBoxColumn("PARENTEQUIPMENTCLASSID", 150);
            // 붙여넣기를 위해서 막음    
            //.SetIsReadOnly();

            this.grdEquipmentClass.View.AddComboBoxColumn("EQUIPMENTCLASSTYPE", 100, new SqlQuery("GetTypeList", "10001", "CODECLASSID=EquipmentType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                //.SetValidationCustom(CustomValidation);
                .SetValidationIsRequired()
                .SetIsReadOnly();

            //code 등록후 codeclassid 수정  자동으로 들어가야함
            this.grdEquipmentClass.View.AddComboBoxColumn("HIERARCHY", 100, new SqlQuery("GetTypeList", "10001", "CODECLASSID=EquipmentClassHierarchy", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                .SetValidationIsRequired();

            this.grdEquipmentClass.View.AddTextBoxColumn("DESCRIPTION", 150);

            this.grdEquipmentClass.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Valid")
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Center);

            this.grdEquipmentClass.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            this.grdEquipmentClass.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            this.grdEquipmentClass.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            this.grdEquipmentClass.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            this.grdEquipmentClass.View.PopulateColumns();
        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            //설비트리를 새로고침하는 이벤트
            grpBox.CustomButtonClick += grpBox_CustomButtonClick;
            //설비트리의 포커스가 바뀔때 재조회하는 이벤트
            treeEquipmentClass.FocusedNodeChanged += TreeEquipmentClass_FocusedNodeChanged;
            //새로운 Row를 추가 할때 상위그룹과 enterprise를 자동입력해주는 이벤트
            grdEquipmentClass.View.AddingNewRow += View_AddingNewRow;
            //설비그룹이 대,중 그룹일 때 HIERARCHY를 입력하지 못하게하는 이벤트 
            grdEquipmentClass.View.ShowingEditor += GridView_ShowingEditor;
            //설비트리의 포커스 노드가 소그룹 일 때 하위 생성 불가 이벤트
            grdEquipmentClass.ToolbarAddingRow += GrdGrid_ToolbarAddingRow;
            //셀 값이 바뀔때 이벤트
            grdEquipmentClass.View.CellValueChanged += View_CellValueChanged;

            //new SetGridDeleteButonVisible(grdEquipmentClass);
        }

        /// <summary>
        /// 소그룹일때 (대, 중그룹 선택 못하게 하는이벤트)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName.Equals("HIERARCHY"))
            {
                DataRow focusRow = treeEquipmentClass.GetFocusedDataRow();
                string nodetype = focusRow["NODETYPE"].ToString();

                if (nodetype.Equals("MC") && e.Value != null &&  (e.Value.Equals("TopEquipment") || e.Value.Equals("MiddleEquipment")))
                {//상위노드가 중그룹이때 HIERARCHY를 대, 중으로 입력할 수 없음
                    this.ShowMessage("CantSelectHierarchy"); //상위 설비그룹이 중그룹일 경우 설비그룹과 설비단그룹 중에만 선택할 수 있습니다.
                    grdEquipmentClass.View.SetFocusedRowCellValue("HIERARCHY", null);
                }
            }
        }

        /// <summary>
        /// 설비그룹 트리를 새로고침하는 버튼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grpBox_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Caption == "GridRefresh")
            {
                InitializeTreeEquipmentClass();
                treeEquipmentClass.FocusedNode = treeEquipmentClass.GetNodeByVisibleIndex(0);
            }
        }

        /// <summary>
        /// 설비그룹의 포커스가 바뀔때 설비 그룹 재조회하는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeEquipmentClass_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            var param = Conditions.GetValues();
            DataRow focusRow = treeEquipmentClass.GetFocusedDataRow();

            //UserEnterprise 설정안되어있어서 임의로 지정 나중에 삭제 임시
            if (UserInfo.Current.Enterprise.Equals(""))
            {
                param.Add("p_enterpriseid", "INTERFLEX");//임시
            }
            else
            {
                param.Add("p_enterpriseid", UserInfo.Current.Enterprise);
            }

            param.Add("p_languagetype", UserInfo.Current.LanguageType);
      
            if (focusRow["NODETYPE"].ToString().Equals("GROUPTYPE"))
            {
                param.Add("p_parentequipmentclassid", "");
                param.Add("p_grouptype", focusRow["ID"]);

                this.grdEquipmentClass.DataSource = SqlExecuter.Query("SelectEquipmentClassManagement", "10001", param);
            }
            else if (focusRow["NODETYPE"].ToString().Equals("LC") || focusRow["NODETYPE"].ToString().Equals("MC"))
            {
                param.Add("p_parentequipmentclassid", focusRow["ID"]);
                param.Add("p_grouptype", "");
                this.grdEquipmentClass.DataSource = SqlExecuter.Query("SelectEquipmentClassManagement", "10001", param);
            }
            else if (focusRow["NODETYPE"].ToString().Equals("SC")|| focusRow["NODETYPE"].ToString().Equals("ENTERPRISE"))
            {
                this.grdEquipmentClass.DataSource = null;
            }
        }
        /// <summary>
        /// 새로운 Row를 추가 할 때 설비그룹 트리의 enterprise,parentequipmentclassid 입력하는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            DataRow focusRow = treeEquipmentClass.GetFocusedDataRow();

            string nodetype = focusRow["NODETYPE"].ToString();

            if (!nodetype.Equals("ENTERPRISE") && !nodetype.Equals("GROUPTYPE"))
            {
                string parentEquipmentId = focusRow["ID"].ToString();
                args.NewRow["PARENTEQUIPMENTCLASSID"] = parentEquipmentId;
            }


            //UserEnterprise 설정안되어있어서 임의로 지정 나중에 삭제 임시
            if (string.IsNullOrWhiteSpace(Framework.UserInfo.Current.Enterprise.ToString()))
            {
                args.NewRow["ENTERPRISEID"] = "INTERFLEX";//임시
            }
            else
            {
                args.NewRow["ENTERPRISEID"] = Framework.UserInfo.Current.Enterprise;
            }

            if (nodetype.Equals("GROUPTYPE"))
            {//설비 대그룹을 입력할 때
                args.NewRow["EQUIPMENTCLASSTYPE"] = focusRow["ID_COPY"];
                args.NewRow["HIERARCHY"] = "TopEquipment";
            }
            else if (nodetype.Equals("LC"))
            {//설비 중그룹을 입력할 때
                args.NewRow["EQUIPMENTCLASSTYPE"] = focusRow["EQUIPMENTCLASSTYPE"];
                args.NewRow["HIERARCHY"] = "MiddleEquipment";
            }
            else if (nodetype.Equals("MC"))
            {//설비 중그룹을 입력할 때
                args.NewRow["EQUIPMENTCLASSTYPE"] = focusRow["EQUIPMENTCLASSTYPE"];
            }

        }

        /// <summary>
        /// nodeType이 소그룹일 경우 새로운 Row를 추가하지 못하게 하는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdGrid_ToolbarAddingRow(object sender, CancelEventArgs e)
        {
            DataRow focusRow = treeEquipmentClass.GetFocusedDataRow();
            string nodetype = focusRow["NODETYPE"].ToString();

            if (nodetype.Equals("ENTERPRISE") || nodetype.Equals("SC"))
            {
                e.Cancel = true;
                this.ShowMessage("SelectRightNodeType");//알맞는 트리 노드를 선택해야 합니다. 
            }
        }

        /// <summary>
        /// 설비그룹이 소그룹일 때 만 설비그룹 HIERARCHY를 입력 할 수 있게 체크하는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridView_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (grdEquipmentClass.View.FocusedColumn.FieldName == "HIERARCHY")
            {
                DataRow focusRow = treeEquipmentClass.GetFocusedDataRow();
                if (!focusRow["NODETYPE"].ToString().Equals("MC"))
                {
                    this.ShowMessage("CantSelectEquipmentClassType");//상위 설비그룹이 중그룹 일 경우에만 선택입력 가능 합니다.
                    e.Cancel = true;
                }
            }
        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {

            DataRow focusRow = treeEquipmentClass.GetFocusedDataRow();

            _focusedId = focusRow["ID_COPY"].ToString();

            base.OnToolbarSaveClick();

            DataTable changed = grdEquipmentClass.GetChangedRows();
            ExecuteRule("SaveEquipmentClassManagement", changed);
            InitializeTreeEquipmentClass();
        }
        #endregion

        #region 검색

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            TreeListNode beforeTreeListNode = treeEquipmentClass.FocusedNode;  

            DataRow focusRow = treeEquipmentClass.GetFocusedDataRow();

            var values = Conditions.GetValues();

            //UserEnterprise 설정안되어있어서 임의로 지정 나중에 삭제 임시
            if (UserInfo.Current.Enterprise.Equals(""))
            {
                values.Add("p_enterpriseid", "INTERFLEX");//임시
            }
            else
            {
                values.Add("p_enterpriseid", UserInfo.Current.Enterprise);
            }

            values.Add("p_languagetype", UserInfo.Current.LanguageType);

            if (focusRow["NODETYPE"].ToString().Equals("GROUPTYPE"))
            {
                values.Add("p_parentequipmentclassid", "");
                values.Add("p_grouptype", focusRow["ID"]);
            }
            else
            {
                values.Add("p_parentequipmentclassid", focusRow["ID"]);
                values.Add("p_grouptype", "");
            }

            if (focusRow["NODETYPE"].ToString().Equals("SC") || focusRow["NODETYPE"].ToString().Equals("ENTERPRISE"))
            {
                grdEquipmentClass.DataSource = null;
            }
            else
            {
                DataTable dt = await SqlExecuter.QueryAsync("SelectEquipmentClassManagement", "10001", values);

                if (dt.Rows.Count < 1 && treeEquipmentClass.AllNodesCount == 0) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
                {
                    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                }

                grdEquipmentClass.DataSource = dt;             
            }

            treeEquipmentClass.SetFocusedNode(treeEquipmentClass.FindNodeByKeyID(beforeTreeListNode.GetValue("ID")));

            TreeListNode afterTreeListNode = treeEquipmentClass.FocusedNode;
            afterTreeListNode.Expanded = true;
            afterTreeListNode.Expand();
        }

        protected override void InitializeCondition()
        {
            base.InitializeCondition();
        }

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

            grdEquipmentClass.View.CheckValidation();

            DataTable changed = grdEquipmentClass.GetChangedRows();

            if (changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }


            // 삭제 확인
            DataTable deleted = grdEquipmentClass.GetChangesDeleted();
            foreach (DataRow deleteRow in deleted.Rows)
            {
                Dictionary<string, object> dicParam = new Dictionary<string, object>();
                dicParam.Add("EQUIPMENTCLASSID", deleteRow["EQUIPMENTCLASSID"].ToString());
                dicParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                DataTable dt = SqlExecuter.Query("GetUsingEquipmentClass", "00001", dicParam);
                if (dt != null && dt.Rows.Count > 0)
                {
                    throw MessageException.Create("UsedEquipmentClass", dt.Rows[0]["TYPE"].ToString(), dt.Rows[0]["ID"].ToString());
                }
            }
        }

        #endregion

        #region Private Function
        /// <summary>
        /// 설비그룹이 소그룹일 때 설비그룹 타입 필수입력 유효성검사 함수
        /// </summary>
        /// <param name="rowHandle"></param>
        /// <returns></returns>
        private IEnumerable<ValidationResult> CustomValidation(int rowHandle)
        {
            DataRow currentRow = grdEquipmentClass.View.GetDataRow(rowHandle);

            List<ValidationResult> result = new List<ValidationResult>();

            DataRow focused = treeEquipmentClass.GetFocusedDataRow(); 
            if (focused["NODETYPE"].ToString().Equals("MC")) 
            {
                ValidationResult resultLsl = new ValidationResult();

                resultLsl.Caption = Language.Get("EQUIPMENTCLASSTYPE"); //위반한 컬럼의 다국어
                resultLsl.FailMessage = Language.GetMessage("EquipmentClassTypeRequire").Message;
                resultLsl.Id = "EQUIPMENTCLASSTYPE";

                if (string.IsNullOrWhiteSpace(currentRow["EQUIPMENTCLASSTYPE"].ToString()))
                {
                    resultLsl.IsSucced = false;
                }

                result.Add(resultLsl);
            }

            return result;
        }
        #endregion
    }
}
