#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;

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
    /// 프 로 그 램 명  : ex> 시스템관리 > 코드 관리 > 코드그룹 정보
    /// 업  무  설  명  : ex> 시스템에서 공통으로 사용되는 코드그룹 정보를 관리한다.
    /// 생    성    자  : 홍길동
    /// 생    성    일  : 2019-05-14
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class AttributeMapping : SmartConditionManualBaseForm
    {
        #region Local Variables

       

        #endregion

        #region 생성자

        public AttributeMapping()
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
        }

        /// <summary>        
        /// 코드그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            // 매핑
            grdAttributeTarget.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Copy | GridButtonItem.Import | GridButtonItem.Export;
            grdAttributeTarget.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            
            grdAttributeTarget.View.AddTextBoxColumn("MAPPING_SEQ", 100)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsReadOnly(); 
            grdAttributeTarget.View.AddTextBoxColumn("TABLENAME", 100).SetValidationIsRequired();
            grdAttributeTarget.View.AddTextBoxColumn("COLUMNID", 100).SetValidationIsRequired();
            grdAttributeTarget.View.AddTextBoxColumn("CODECLASS", 100).SetValidationIsRequired();
            grdAttributeTarget.View.AddTextBoxColumn("CODE", 100).SetValidationIsRequired();
            grdAttributeTarget.View.AddTextBoxColumn("CODETARGETTABLE", 100);
            grdAttributeTarget.View.AddTextBoxColumn("CODECLASSNAME", 100);
            grdAttributeTarget.View.AddTextBoxColumn("CODENAME", 100);
            grdAttributeTarget.View.AddTextBoxColumn("DESCRIPTION", 200);
            grdAttributeTarget.View.AddTextBoxColumn("YPECODECLASS", 100);
            grdAttributeTarget.View.AddTextBoxColumn("YPECODE", 100);
            grdAttributeTarget.View.AddTextBoxColumn("YPECODENAME", 100);
            grdAttributeTarget.View.AddTextBoxColumn("COMMENT", 100);
            ////grdAttributeTarget.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
            ////   .SetDefault("Valid")
            ////   .SetValidationIsRequired()
            ////   .SetTextAlignment(TextAlignment.Center);
            grdAttributeTarget.View.AddTextBoxColumn("CREATOR", 80)
              .SetIsReadOnly()
              .SetTextAlignment(TextAlignment.Center);

            grdAttributeTarget.View.AddTextBoxColumn("CREATEDTIME", 130)
               // Display Format 지정
               .SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime)
               .SetIsReadOnly()
               .SetTextAlignment(TextAlignment.Center);

            //grdAttributeTarget.View.AddTextBoxColumn("MODIFIER", 80)
            //   .SetIsReadOnly()
            //   .SetTextAlignment(TextAlignment.Center);

            //grdAttributeTarget.View.AddTextBoxColumn("MODIFIEDTIME", 130)
            //    .SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime)
            //    .SetIsReadOnly()
            //    .SetTextAlignment(TextAlignment.Center);

            grdAttributeTarget.View.SetKeyColumn("TABLENAME", "COLUMNID", "CODECLASS", "CODE");
            grdAttributeTarget.View.PopulateColumns();

        }

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            grdAttributeTarget.View.AddingNewRow += grdAttributeTarget_AddingNewRow;
        }

        /// <summary>
        /// 코드그룹 리스트 그리드에서 추가 버튼 클릭 시 호출
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void grdAttributeTarget_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            grdAttributeTarget.View.SetFocusedRowCellValue("VALIDSTATE", "Valid");
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
            DataTable dtSave = grdAttributeTarget.GetChangedRows();
            if (dtSave.Rows.Count > 0)
            {
                ExecuteRule("AttributeMapping", dtSave);
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

            var values = Conditions.GetValues();


            DataTable dtSegment = await SqlExecuter.QueryAsync("GetAttributeMapping", "10001", values);

            //grdCodeClass.DataSource = SqlExecuter.Procedure("usp_com_selectCodeClass_search", values);
            grdAttributeTarget.DataSource = dtSegment;

        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            var txttablename = Conditions.AddTextBox("P_TABLENAME")
              .SetLabel("TABLENAME")
              .SetPosition(1.0);
            var txtcolumnid = Conditions.AddTextBox("P_COLUMNID")
              .SetLabel("COLUMNID")
              .SetPosition(2.0);
            var txtMODIFIER = Conditions.AddTextBox("P_MODIFIER")
             .SetLabel("MODIFIER")
             .SetPosition(2.5);
            var Ospetcprogressstatuscbobox = Conditions.AddComboBox("P_VALIDSTATE", new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetEmptyItem("", "")
              .SetLabel("VALIDSTATE")
              .SetPosition(3.0)
              .SetEmptyItem("", "")
            ;
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

            DataTable changed = new DataTable();
            grdAttributeTarget.View.CheckValidation();

            changed = grdAttributeTarget.GetChangedRows();

            if (changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }

        #endregion

        #region Private Function

        // TODO : 화면에서 사용할 내부 함수 추가
        /// <summary>
        /// 마스터 정보를 조회한다.
        /// </summary>
        private void LoadData()
        {

        }

        #endregion
    }
}
