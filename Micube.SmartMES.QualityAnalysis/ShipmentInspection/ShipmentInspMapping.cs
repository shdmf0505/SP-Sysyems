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

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 출하검사 > 출하검사 성적서 매핑
    /// 업  무  설  명  : 출하검사 성적서 출력에 앞서 출력 될 정보를 시스템과 출력양식 간 매핑 한다.
    /// 생    성    자  : JAR
    /// 생    성    일  : 2020-01-04
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ShipmentInspMapping : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가
        string _Text;                                       // 설명
        SmartBandedGrid grdList = new SmartBandedGrid();    // Main Grid

        #endregion

        #region 생성자

        public ShipmentInspMapping()
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
            //base.InitializeContent();
            //InitializeCondition();
            InitializeEvent();
            // TODO : 컨트롤 초기화 로직 구성
            InitializeGrid();
        }

        /// <summary>        
        /// 코드그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdMappingList.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdMappingList.View.SetIsReadOnly();
            // 코드그룹ID
            grdMappingList.View.AddTextBoxColumn("ProductClassID")
                .SetIsHidden();
            grdMappingList.View.AddTextBoxColumn("ProductDefID", 150);
            grdMappingList.View.AddTextBoxColumn("ProductDefName", 200);
            grdMappingList.View.AddTextBoxColumn("ProductDefVersion");
            grdMappingList.View.AddTextBoxColumn("ProductDefTypeID", 150)
                .SetIsHidden();
            grdMappingList.View.AddTextBoxColumn("ProductDefType", 150);
            grdMappingList.View.AddTextBoxColumn("CustomerID")
                .SetIsHidden();
            grdMappingList.View.AddTextBoxColumn("CustomerName", 150);
            grdMappingList.View.AddTextBoxColumn("RevisionNo", 100);
            grdMappingList.View.AddTextBoxColumn("StartSheetIndex")
                .SetIsHidden();
            grdMappingList.View.AddTextBoxColumn("FileName", 200);
            grdMappingList.View.AddTextBoxColumn("FileID")
                .SetIsHidden();
            grdMappingList.View.AddTextBoxColumn("FileSize")
                .SetIsHidden();
            grdMappingList.View.AddTextBoxColumn("EnterpriseID")
                .SetIsHidden();
            grdMappingList.View.AddTextBoxColumn("PlantID")
                .SetIsHidden();
            grdMappingList.View.PopulateColumns();
        }

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            grdMappingList.View.DoubleClick += View_DoubleClick;
        }
        /// <summary>
        /// 양식 리스트 더블클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                DataRow focusedRow = grdMappingList.View.GetFocusedDataRow();

                DialogManager.ShowWaitArea(pnlContent);
                ShipmentInspMappingPopup detailPopup = new ShipmentInspMappingPopup();
                detailPopup.CurrentDataRow = focusedRow;

                detailPopup.StartPosition = FormStartPosition.CenterParent;
                detailPopup.FormBorderStyle = FormBorderStyle.Sizable;
                detailPopup.Size = new Size(1920, 1024);

                DialogManager.CloseWaitArea(pnlContent);
                
                if(!detailPopup.ShowDialog().Equals(DialogResult.Cancel))
                {
                    grdMappingList.View.CloseEditor();
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                throw(ex);
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
            try
            {
                DialogManager.ShowWaitArea(this.pnlContent);
                await base.OnSearchAsync();

                var values = Conditions.GetValues();

                values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                values.Add("P_REVISIONNO", "");

                if (await SqlExecuter.QueryAsync("SelectShipmentInspMappingList", "10001", values) is DataTable dtMappingList)
                {
                    if (dtMappingList.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
                    {
                        ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                    }

                    grdMappingList.View.CloseEditor();
                    grdMappingList.DataSource = dtMappingList;
                }
            }
            catch (Exception ex)
            {
                throw Framework.MessageException.Create(ex.ToString());
            }
            finally
            {
                DialogManager.CloseWaitArea(this.pnlContent);
            }
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용

            this.Conditions.AddComboBox("P_EXISTDOCUMENT", new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={ UserInfo.Current.LanguageType }", $"CODECLASSID=ExistDocument"), "CODENAME", "CODEID")
                .SetDefault("*")
                .SetLabel("매핑여부");
            this.Conditions.AddComboBox("P_MAPPINGREVISION", new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={ UserInfo.Current.LanguageType }", $"CODECLASSID=MappingRevision"), "CODENAME", "CODEID")
            .SetDefault("*")
                .SetLabel("문서 Rev");
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

        // TODO : 화면에서 사용할 내부 함수 추가
        /// <summary>
        /// 마스터 정보를 조회한다.
        /// </summary>
        private async void LoadData()
        {
            await OnSearchAsync();
        }

        #endregion
    }
}
