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
    /// 프 로 그 램 명  : 기준정보 > 설비 기준 정보 > SparePart 그룹
    /// 업  무  설  명  : Spare Part 그룹 정보를 관리한다.
    /// 생    성    자  : 장선미
    /// 생    성    일  : 2019-12-03
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class SparePartClassManagement : SmartConditionManualBaseForm
    {
        #region Local Variables

        string _focusedId = "";//설비그룹 저장 후 설비트리의 포커스를 저장한 Row로 맟추기위한 변수 

        #endregion

        #region 생성자

        public SparePartClassManagement()
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
            InitializeTreeSparePartClass();
        }
        /// <summary>
        /// 설비유형 트리 초기화
        /// </summary>
        private void InitializeTreeSparePartClass()
        {
            treeSparePartClass.SetResultCount(1);
            treeSparePartClass.SetIsReadOnly();
            treeSparePartClass.SetFocusedNode(treeSparePartClass.FindNodeByFieldValue("NODETYPE", "ENTERPRISE"));

            Dictionary<string, object> param = new Dictionary<string, object>();
            //UserEnterprise 설정안되어있어서 임의로 지정 나중에 삭제 임시
            if (UserInfo.Current.Enterprise.Equals(""))
            {
                treeSparePartClass.SetEmptyRoot("INTERFLEX", "INTERFLEX");
                param.Add("p_enterpriseid", "INTERFLEX");
            }
            else
            {
                treeSparePartClass.SetEmptyRoot(UserInfo.Current.Enterprise, UserInfo.Current.Enterprise);
                param.Add("p_enterpriseid", UserInfo.Current.Enterprise);
            }
            treeSparePartClass.SetMember("NAME", "ID", "PARENT");

            //param.Add("p_enterpriseid", UserInfo.Current.Enterprise);
            param.Add("p_languagetype", UserInfo.Current.LanguageType);
    

            treeSparePartClass.DataSource = SqlExecuter.Query("SelectSparePartClassManagement_Tree", "10001", param);
            treeSparePartClass.PopulateColumns();
            treeSparePartClass.ExpandAll();

            if (_focusedId.Equals(""))
            {
                treeSparePartClass.SetFocusedNode(treeSparePartClass.FindNodeByFieldValue("NODETYPE", "ENTERPRISE"));
            }
            else
            {
                treeSparePartClass.SetFocusedNode(treeSparePartClass.FindNodeByFieldValue("ID", _focusedId));
            }
        }

        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            this.grdSparePartClass.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            //this.grdSparePartClass.GridButtonItem -= GridButtonItem.Delete;

            this.grdSparePartClass.View.AddTextBoxColumn("ENTERPRISEID", 200)                
              .SetIsHidden();

            this.grdSparePartClass.View.AddTextBoxColumn("SPAREPARTCLASSID", 150)
                .SetValidationKeyColumn();

            this.grdSparePartClass.View.AddLanguageColumn("SPAREPARTCLASSNAME", 200);

            this.grdSparePartClass.View.AddTextBoxColumn("PARENTSPAREPARTCLASSID", 150)
                .SetIsReadOnly();

            this.grdSparePartClass.View.AddComboBoxColumn("SPAREPARTCLASSTYPE", 150, new SqlQuery("GetTypeList", "10001", "CODECLASSID=SparePartClass", $"LANGUAGETYPE ={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                //.SetValidationCustom(CustomValidation);
                .SetValidationIsRequired()
                .SetIsReadOnly();

            this.grdSparePartClass.View.AddTextBoxColumn("DESCRIPTION", 150);

            this.grdSparePartClass.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Valid")
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Center);

            this.grdSparePartClass.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            this.grdSparePartClass.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            this.grdSparePartClass.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            this.grdSparePartClass.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            this.grdSparePartClass.View.PopulateColumns();
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
            treeSparePartClass.FocusedNodeChanged += TreeSparePartClass_FocusedNodeChanged;
            //새로운 Row를 추가 할때 상위그룹과 enterprise를 자동입력해주는 이벤트
            grdSparePartClass.View.AddingNewRow += View_AddingNewRow;
            //설비트리의 포커스 노드가 소그룹 일 때 하위 생성 불가 이벤트
            grdSparePartClass.ToolbarAddingRow += GrdGrid_ToolbarAddingRow;

            //new SetGridDeleteButonVisible(grdSparePartClass);
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
                InitializeTreeSparePartClass();
                treeSparePartClass.FocusedNode = treeSparePartClass.GetNodeByVisibleIndex(0);
            }
        }

        /// <summary>
        /// Spare Part 그룹의 포커스가 바뀔때 Spare Part 그룹 재조회하는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeSparePartClass_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            var param = Conditions.GetValues();
            DataRow focusRow = treeSparePartClass.GetFocusedDataRow();

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

            if (focusRow["NODETYPE"].ToString().Equals("ENTERPRISE"))
            {
                // 대그룹 조회
                param.Add("p_parentSparePartclassid", "");
                param.Add("p_grouptype", "TopSparePart");

                this.grdSparePartClass.DataSource = SqlExecuter.Query("SelectSparePartClassManagement", "10001", param);
            }
            else if (focusRow["NODETYPE"].ToString().Equals("LC") || focusRow["NODETYPE"].ToString().Equals("MC"))
            {
                param.Add("p_parentSparePartclassid", focusRow["ID"]);
                param.Add("p_grouptype", "");
                this.grdSparePartClass.DataSource = SqlExecuter.Query("SelectSparePartClassManagement", "10001", param);
            }
            else if (focusRow["NODETYPE"].ToString().Equals("SC"))
            {
                this.grdSparePartClass.DataSource = null;
            }
        }
        /// <summary>
        /// 새로운 Row를 추가 할 때 설비그룹 트리의 enterprise,parentequipmentclassid 입력하는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            DataRow focusRow = treeSparePartClass.GetFocusedDataRow();

            string nodetype = focusRow["NODETYPE"].ToString();

            if (nodetype.Equals("ENTERPRISE"))
            {
                args.NewRow["SPAREPARTCLASSTYPE"] = "TopSparePart";
                args.NewRow["PARENTSPAREPARTCLASSID"] = "";

            }
            else
            {
                string parentSparePartId = focusRow["ID"].ToString();

                args.NewRow["PARENTSPAREPARTCLASSID"] = parentSparePartId;
            }

            if (nodetype.Equals("LC"))
            {//중그룹을 입력할 때
                args.NewRow["SPAREPARTCLASSTYPE"] = "MiddleSparePart";
            }
            else if (nodetype.Equals("MC"))
            {//중그룹을 입력할 때
                args.NewRow["SPAREPARTCLASSTYPE"] = "SmallSparePart";
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
        }

        /// <summary>
        /// nodeType이 소그룹일 경우 새로운 Row를 추가하지 못하게 하는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdGrid_ToolbarAddingRow(object sender, CancelEventArgs e)
        {
            DataRow focusRow = treeSparePartClass.GetFocusedDataRow();
            string nodetype = focusRow["NODETYPE"].ToString();

            if (nodetype.Equals("SC"))
            {
                e.Cancel = true;
                this.ShowMessage("SelectRightNodeType");//알맞는 트리 노드를 선택해야 합니다. 
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

            DataRow focusRow = treeSparePartClass.GetFocusedDataRow();

            _focusedId = focusRow["ID_COPY"].ToString();

            DataTable changed = grdSparePartClass.GetChangedRows();

            ExecuteRule("SaveSparePartClassManagement", changed);

            InitializeTreeSparePartClass();
        }
        #endregion

        #region 검색

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            TreeListNode beforeTreeListNode = treeSparePartClass.FocusedNode;  

            DataRow focusRow = treeSparePartClass.GetFocusedDataRow();

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

            if (focusRow["NODETYPE"].ToString().Equals("ENTERPRISE"))
            {
                values.Add("p_parentequipmentclassid", "");
                values.Add("p_grouptype", "TopSparePart");
            }
            else if (focusRow["NODETYPE"].ToString().Equals("LC") || focusRow["NODETYPE"].ToString().Equals("MC"))
            {
                values.Add("p_parentsparepartclassid", focusRow["ID"]);
                values.Add("p_grouptype", "");
            }

            if (focusRow["NODETYPE"].ToString().Equals("SC"))
            {
                grdSparePartClass.DataSource = null;
            }
            else
            {
                DataTable dt = await SqlExecuter.QueryAsync("SelectSparePartClassManagement", "10001", values);

                if (dt.Rows.Count < 1 && treeSparePartClass.AllNodesCount == 0) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
                {
                    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                }

                grdSparePartClass.DataSource = dt;             
            }

            treeSparePartClass.SetFocusedNode(treeSparePartClass.FindNodeByKeyID(beforeTreeListNode.GetValue("ID")));

            TreeListNode afterTreeListNode = treeSparePartClass.FocusedNode;
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

            grdSparePartClass.View.CheckValidation();

            DataTable changed = grdSparePartClass.GetChangedRows();

            if (changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }

            // 삭제 확인
            DataTable deleted = grdSparePartClass.GetChangesDeleted();
            foreach (DataRow deleteRow in deleted.Rows)
            {
                Dictionary<string, object> dicParam = new Dictionary<string, object>();
                dicParam.Add("SPAREPARTCLASSID", deleteRow["SPAREPARTCLASSID"].ToString());
                dicParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                DataTable dt = SqlExecuter.Query("GetUsingSparepartClass", "00001", dicParam);
                if (dt != null && dt.Rows.Count > 0)
                {
                    throw MessageException.Create("UsedSparePartClass", dt.Rows[0]["ID"].ToString());
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
            DataRow currentRow = grdSparePartClass.View.GetDataRow(rowHandle);

            List<ValidationResult> result = new List<ValidationResult>();

            DataRow focused = treeSparePartClass.GetFocusedDataRow(); 
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
