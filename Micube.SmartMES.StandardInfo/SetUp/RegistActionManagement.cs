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
using DevExpress.DataProcessing;
using DevExpress.Utils.MVVM;
using DevExpress.XtraGrid.Views.Base;

#endregion

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프 로 그 램 명  : 기준정보 > set up > Action 등록 및 조회
    /// 업  무  설  명  : 기준정보에서 Action 등록 및 조회
    /// 생    성    자  : 조혜인
    /// 생    성    일  : 2019-11-13
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class RegistActionManagement : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가

        #endregion

        #region 생성자

        public RegistActionManagement()
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
            InitializeComboBox();

            InitializeEvent();

            // TODO : 컨트롤 초기화 로직 구성
            InitializeGrid();
        }

        private void InitializeComboBox()
        {

            Dictionary<string, object> actionTypeParams = new Dictionary<string, object>();
            var validStatusParams = new Dictionary<string, object>();
            actionTypeParams.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            actionTypeParams.Add("CODECLASSID", "ActionType" );
            validStatusParams.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            validStatusParams.Add("CODECLASSID", "ValidState");
           
        }

        /// <summary>        
        /// 코드그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가

            #region Action조회

            grdAction.GridButtonItem = GridButtonItem.Add | GridButtonItem.Export ;

            grdAction.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;
            //Action ID
            grdAction.View.AddTextBoxColumn("ACTIONID", 120).SetTextAlignment(TextAlignment.Center).SetValidationIsRequired();
            //Action 먕
            grdAction.View.AddTextBoxColumn("ACTIONNAME", 150).SetTextAlignment(TextAlignment.Center);
            //Action 유형
            grdAction.View.AddComboBoxColumn("ACTIONTYPE", new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "CODECLASSID=ActionType"), "CODENAME").SetTextAlignment(TextAlignment.Center);
            //수신 사용자 그룹
            grdAction.View.AddComboBoxColumn("USERCLASSID", new SqlQuery("GetUserClassInfo", "10001"), "USERCLASSNAME", "USERCLASSID").SetTextAlignment(TextAlignment.Center).SetLabel("RCVUSERCLASS");
            //참조 사용자 그룹
            grdAction.View.AddComboBoxColumn("REFERENCEUSERCLASSID", new SqlQuery("GetUserClassInfo", "10001"), "USERCLASSNAME", "USERCLASSID").SetTextAlignment(TextAlignment.Center).SetLabel("REFUSERCLASS");
            //유효상태
            grdAction.View.AddComboBoxColumn("VALIDSTATE", 60,  new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "CODECLASSID=ValidState"), "CODENAME").SetTextAlignment(TextAlignment.Center).SetDefault("Valid");

            grdAction.View.AddTextBoxColumn("ENTERPRISEID", 150).SetTextAlignment(TextAlignment.Center).SetIsHidden();
            grdAction.View.AddTextBoxColumn("PLANTID", 150).SetTextAlignment(TextAlignment.Center).SetIsHidden();

            grdAction.View.AddTextBoxColumn("CREATOR", 80)
                              .SetIsReadOnly()
                              .SetTextAlignment(TextAlignment.Center);
            grdAction.View.AddTextBoxColumn("CREATEDTIME", 130)
                                // Display Format 지정
                                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime)
                                .SetIsReadOnly()
                                .SetTextAlignment(TextAlignment.Center);
            grdAction.View.AddTextBoxColumn("MODIFIER", 80)
                                .SetIsReadOnly()
                                .SetTextAlignment(TextAlignment.Center);
            grdAction.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime)
                                .SetIsReadOnly()
                                .SetTextAlignment(TextAlignment.Center);


            grdAction.View.PopulateColumns();

            
            #endregion
        }

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            grdAction.View.AddingNewRow += View_AddingNewRow;
            grdAction.View.ShowingEditor += View_ShowingEditor;
            btnSave.Click += btnSave_Click;
        }

        private void View_ShowingEditor(object sender, CancelEventArgs e)
        {
            Micube.Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView view = sender as Micube.Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView;

            if (view != null)
            {
                if (view.GetFocusedDataRow().RowState != DataRowState.Added)
                {
                    if (view.FocusedColumn.FieldName.Equals("ACTIONID"))
                    {
                        e.Cancel = true;
                    }
                }
                
            }
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.ShowMessageBox("InfoSave", "Caption", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                DataTable changed = grdAction.GetChangedRows();

                ExecuteRule("RegistActionManagement", changed);

                this.ShowMessage("SuccessSave");
            }
                

            // ExecuteRule("RegistActionManagement",);

            
        }
        

        
        /// <summary>
        /// 코드그룹 리스트 그리드에서 추가 버튼 클릭 시 호출
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
           // args.NewRow["VALIDSTATE"] = "Valid";
            args.NewRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
            args.NewRow["PLANTID"] = UserInfo.Current.Plant;
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
            DataTable changed = grdAction.GetChangedRows();

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
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            var dtActionList = await QueryAsync("SelectActionList", "10001", values);

            if (dtActionList.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdAction.DataSource = dtActionList;
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
            grdAction.View.CheckValidation();

            DataTable changed = grdAction.GetChangedRows();

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
