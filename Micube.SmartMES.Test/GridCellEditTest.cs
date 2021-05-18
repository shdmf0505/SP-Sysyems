#region using

using DevExpress.XtraEditors.Repository;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;

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

namespace Micube.SmartMES.Test
{
    /// <summary>
    /// 프 로 그 램 명  : ex> 시스템관리 > 코드 관리 > 코드그룹 정보
    /// 업  무  설  명  : ex> 시스템에서 공통으로 사용되는 코드그룹 정보를 관리한다.
    /// 생    성    자  : 홍길동
    /// 생    성    일  : 2019-05-14
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class GridCellEditTest : SmartConditionBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가
        RepositoryItemTextEdit textEdit;
        RepositoryItemSpinEdit spinEdit;
        RepositoryItemLookUpEdit lookupEdit;

        #endregion

        #region 생성자

        public GridCellEditTest()
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
            InitializeGrid();

            InitializeRepositoryItems();
        }

        /// <summary>        
        /// 코드그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdList.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Restore;
            grdList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            // 코드그룹ID
            grdList.View.AddTextBoxColumn("CODECLASSID", 150)
                .SetValidationIsRequired();
            // 코드그룹명
            grdList.View.AddTextBoxColumn("CODECLASSNAME", 200);
            // 코드그룹타입
            grdList.View.AddComboBoxColumn("CODECLASSTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=CodeClassType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetValidationIsRequired()
                .SetDefault("MES")
                .SetTextAlignment(TextAlignment.Center);
            // 설명
            grdList.View.AddTextBoxColumn("DESCRIPTION", 200);

            grdList.View.PopulateColumns();
        }

        private void InitializeRepositoryItems()
        {
            textEdit = new RepositoryItemTextEdit();
            spinEdit = new RepositoryItemSpinEdit();
            lookupEdit = new RepositoryItemLookUpEdit();

            spinEdit.DisplayFormat.FormatString = "#,##0.00";
            spinEdit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("CODECLASSID", "CodeClassType");
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            lookupEdit.ValueMember = "CODEID";
            lookupEdit.DisplayMember = "CODENAME";
            lookupEdit.ShowHeader = false;
            lookupEdit.DataSource = SqlExecuter.Query("GetCodeList", "00001", param);

            lookupEdit.PopulateColumns();

            lookupEdit.Columns["CODEID"].Visible = false;
        }

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            grdList.View.CustomRowCellEdit += GridView_CustomRowCellEdit;
        }

        private void GridView_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName == "DESCRIPTION")
            {
                string codeClassType = Format.GetString(grdList.View.GetFocusedRowCellValue("CODECLASSTYPE"));

                switch (codeClassType)
                {
                    case "System":
                        e.RepositoryItem = spinEdit;
                        break;
                    case "MES":
                        e.RepositoryItem = lookupEdit;
                        break;
                    default:
                        e.RepositoryItem = textEdit;
                        break;
                }
            }
        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            // TODO : 저장 Rule 변경
            DataTable changed = grdList.GetChangedRows();

            ExecuteRule("SaveCodeClass", changed);
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
            var values = Conditions.GetValues();
            //values.Add("P_VALIDSTATE", "Valid");

            DataTable dtCodeClass = await QueryAsync("SelectCodeClass", "00001", values);

            if (dtCodeClass.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdList.DataSource = dtCodeClass;
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

            // TODO : 유효성 로직 변경
            grdList.View.CheckValidation();

            DataTable changed = grdList.GetChangedRows();

            if (changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }

        #endregion

        #region Private Function

        #endregion
    }
}