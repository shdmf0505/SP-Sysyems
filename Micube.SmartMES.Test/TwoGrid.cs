using Micube.Framework.Net;
using Micube.Framework;
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

using DevExpress.XtraCharts;
using DevExpress.Utils;
using Micube.Framework.SmartControls.Validations;
using Micube.SmartMES.Commons.Controls;

namespace Micube.SmartMES.Test
{
    public partial class TwoGrid : SmartConditionManualBaseForm
    {
        public TwoGrid()
        {
            InitializeComponent();
        }

        #region 툴바

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        /// <returns></returns>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            //TODO :  RuleName을 수정하세요 (저장기능이 없다면 현재 함수를 삭제하세요.)
            // 그리드에 수정된 행을 DataTable Type으로 가져옴
            DataTable changed = grdCode.GetChangedRows();

            // 서버 Rule 호출
            this.ExecuteRule("SaveCodeClass", changed);
        }

        #endregion

        #region 검색

        /// <summary>
        /// 비동기 override 모델, 비동기 모델은 검색에서만 제공합니다. ESC키로 취소 가능합니다.
        /// </summary>
        /// <returns></returns>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            // 조회조건에 입력된 값들을 Dictionary<string, object> Type으로 가져옴
            var values = this.Conditions.GetValues();

            //TODO : Id를 수정하세요            
            // Stored Procedure 호출
            this.grdCode.DataSource = await this.ProcedureAsync("usp_com_selectCode", values);
            // Server Xml Query 호출
            this.grdCode.DataSource = await SqlExecuter.QueryAsync("GetCodeList", "00001", values);
        }

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            DataTable changed = grdCode.GetChangedRows();

            // 수정된 내용이 없는 경우 메시지 처리
            if (changed.Rows.Count == 0)
                throw MessageException.Create("NoSaveData"); //저장할 데이터 없음

            //TODO : 그리드의 유효성 검사
            // 그리드 데이터 Validation 체크
            grdCode.View.CheckValidation();
        }

        #endregion

        #region 컨텐츠 영역 초기화
        /// <summary>
        /// 우측 컨텐츠 영역에 초기화할 코드를 넣으세요.
        /// </summary>
        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeCodeClassGrid();
            InitializeCodeGrid();

            InitializeEvent();
        }

        private void InitializeEvent()
        {
            Load += TwoGrid_Load;

            grdCodeClass.View.FocusedRowChanged += View_FocusedRowChanged;
            grdCodeClass.ToolbarRefresh += GrdCodeClass_ToolbarRefresh;

            grdCode.View.AddingNewRow += View_AddingNewRow;
        }

        private void TwoGrid_Load(object sender, EventArgs e)
        {
            RefreshCodeClassList();
        }

        private void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            FocusedRowChanged();
        }

        private void GrdCodeClass_ToolbarRefresh(object sender, EventArgs e)
        {
            RefreshCodeClassList();
        }

        private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            DataRow row = this.grdCodeClass.View.GetFocusedDataRow();

            args.NewRow["CODECLASSID"] = row["CODECLASSID"];
        }

        private void FocusedRowChanged()
        {
            if (grdCodeClass.View.FocusedRowHandle < 0)
                return;

            DataRow row = this.grdCodeClass.View.GetFocusedDataRow();

            Dictionary<string, object> param = new Dictionary<string, object>()
            {
                { "P_CODECLASSID", row["CODECLASSID"] }
            };

            this.grdCode.DataSource = this.Procedure("usp_com_selectCode", param);
        }

        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeCodeClassGrid()
        {
            this.grdCodeClass.GridButtonItem = GridButtonItem.Refresh;

            this.grdCodeClass.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;
            this.grdCodeClass.View.SetKeyColumn("CODECLASSID");

            // 그리드 전체 ReadOnly 설정
            this.grdCodeClass.View.SetIsReadOnly();

            this.grdCodeClass.View.AddTextBoxColumn("CODECLASSID", 150);
            this.grdCodeClass.View.AddTextBoxColumn("CODECLASSNAME", 200);

            this.grdCodeClass.View.OptionsSelection.MultiSelect = false;

            this.grdCodeClass.View.PopulateColumns();
        }

        private void InitializeCodeGrid()
        {
            this.grdCode.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            this.grdCode.View.SetKeyColumn("CODECLASSID", "CODEID");

            this.grdCode.View.AddTextBoxColumn("CODECLASSID", 150)
                .SetIsReadOnly();
            this.grdCode.View.AddTextBoxColumn("CODEID", 150);
            this.grdCode.View.AddTextBoxColumn("CODENAME", 200);
            this.grdCode.View.AddTextBoxColumn("DESCRIPTION", 200);
            this.grdCode.View.AddSpinEditColumn("DISPLAYSEQUENCE", 80);
            this.grdCode.View.AddComboBoxColumn("VALIDSTATE", new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "CODECLASSTYPE=ValidState"));

            this.grdCode.View.AddTextBoxColumn("CREATOR", 80)
                // ReadOnly 컬럼 지정
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            this.grdCode.View.AddTextBoxColumn("CREATEDTIME", 130)
                // Display Format 지정
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            this.grdCode.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            this.grdCode.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            this.grdCode.View.PopulateColumns();
        }

        private void RefreshCodeClassList()
        {
            var values = this.Conditions.GetValues();

            // Stored Procedure 호출
            this.grdCodeClass.DataSource = this.Procedure("usp_com_selectCodeClassList", values);
            // Server Xml Query 호출
            this.grdCodeClass.DataSource = SqlExecuter.Query("GetCodeClassList", "00001", values);
        }

        #endregion
    }
}
