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

namespace Micube.SmartMES.MaterialsManagement
{
    /// <summary>
    /// 프 로 그 램 명  :  자재관리 >자재 기준 정보  > 자재 거래 유형 및 사유 등록
    /// 업  무  설  명  :  자재 거래 유형 및 사유 등록를 관리한다.
    /// 생    성    자  : 최별
    /// 생    성    일  : 2019-07-11
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class MatTypeReasonRegistration : SmartConditionManualBaseForm
    {
        #region Local Variables

        #endregion

        #region 생성자

        public MatTypeReasonRegistration()
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
            if (UserInfo.Current.Enterprise.Equals("YOUNGPOONG"))
            {

                tapCostcentercode.PageVisible = true;
            }
            else
            {
                tapCostcentercode.PageVisible = false;
            }
        }

        /// <summary>        
        /// 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            //사유등록 그리드
            InitializeMatTypeGrid();
            //자재거래유형  그리드
            InitializeReasonGrid();
            if (UserInfo.Current.Enterprise.Equals("YOUNGPOONG"))
            {
               InitializeCostcentercode();
            }
           
        }
        /// <summary>        
        /// 자재거래유형  그리드를 초기화한다.
        /// </summary>
        private void InitializeCostcentercode()
        {
            grdCostcentercode.GridButtonItem = GridButtonItem.Export;
            grdCostcentercode.View.SetIsReadOnly();
            grdCostcentercode.View.AddTextBoxColumn("PLANTID", 100)
                .SetIsHidden();
            grdCostcentercode.View.AddTextBoxColumn("COSTCENTERCODE", 80);
            grdCostcentercode.View.AddTextBoxColumn("COSTCENTERNAME", 150);
            grdCostcentercode.View.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 100);
            grdCostcentercode.View.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 150);
            grdCostcentercode.View.AddTextBoxColumn("STARTDATE", 100)
                .SetDisplayFormat("yyyy-MM-dd");
            grdCostcentercode.View.AddTextBoxColumn("ENDDATE", 100)
                .SetDisplayFormat("yyyy-MM-dd");
            grdCostcentercode.View.AddComboBoxColumn("VALIDSTATE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Valid");
           
            grdCostcentercode.View.PopulateColumns();
        }
        /// <summary>        
        /// 자재거래유형  그리드를 초기화한다.
        /// </summary>
        private void InitializeMatTypeGrid()
        {
            grdMatType.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export;
            grdMatType.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdMatType.View.AddTextBoxColumn("PLANTID", 100)
                .SetIsHidden();
            grdMatType.View.AddTextBoxColumn("ENTERPRISEID", 100)
                .SetIsHidden();
            grdMatType.View.AddTextBoxColumn("TRANSACTIONCODE", 100)
                .SetValidationKeyColumn()
                .SetValidationIsRequired();
           
            grdMatType.View.AddTextBoxColumn("TRANSACTIONNAME", 300)
                .SetValidationIsRequired();
            grdMatType.View.AddComboBoxColumn("ISADDITIONALUSE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
               .SetDefault("N");
            grdMatType.View.AddComboBoxColumn("VALIDSTATE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Valid");
            grdMatType.View.AddTextBoxColumn("DESCRIPTION", 300);
           
            grdMatType.View.PopulateColumns();
        }
        /// <summary>        
        /// 사유등록 그리드를 초기화한다.
        /// </summary>
        private void InitializeReasonGrid()
        {
            grdReason.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export;
            grdReason.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdReason.View.AddTextBoxColumn("PLANTID", 100)
                .SetIsHidden();
            grdReason.View.AddTextBoxColumn("ENTERPRISEID", 100)
                .SetIsHidden();
           
            grdReason.View.AddTextBoxColumn("TRANSACTIONREASONCODE", 100)
                .SetValidationKeyColumn()
                .SetValidationIsRequired();
            grdReason.View.AddTextBoxColumn("TRANSACTIONREASONCODENAME", 300)
                .SetValidationIsRequired();
             grdReason.View.AddComboBoxColumn("TRANSACTIONCODE", 100, new SqlQuery("GetTransactioncodeByCsm", "10001",  $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "TRANSACTIONNAME", "TRANSACTIONCODE")
                .SetValidationKeyColumn()
                .SetValidationIsRequired();
           
            grdReason.View.AddComboBoxColumn("VALIDSTATE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Valid");
            grdReason.View.AddTextBoxColumn("DESCRIPTION", 300);
          //  grdReason.View.SetAutoFillColumn("DESCRIPTION");
            grdReason.View.PopulateColumns();
        }

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            //행추가 이벤트
            grdMatType.View.AddingNewRow += grdMatType_AddingNewRow;
            grdReason.View.AddingNewRow +=  grdReason_AddingNewRow;
            //코드 대문자로 치환 처리 이벤트
            grdMatType.View.CellValueChanged += grdMatType_CellValueChanged;
            grdReason.View.CellValueChanged += grdReason_CellValueChanged;
        }
        /// <summary>
        /// 거래유형 코드 그리드  유형코드 대문자로 처리 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdMatType_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "TRANSACTIONCODE")
            {
                grdMatType.View.CellValueChanged -= grdMatType_CellValueChanged;

                DataRow row = grdMatType.View.GetFocusedDataRow();

                string strTransactioncode = row["TRANSACTIONCODE"].ToString();

                grdMatType.View.SetFocusedRowCellValue("TRANSACTIONCODE", strTransactioncode.ToUpper());// 
                grdMatType.View.CellValueChanged += grdMatType_CellValueChanged;
                
            }
        }
        /// <summary>
        /// 거래사유코드 등록 그리드 거래사유코드 대문자로 처리 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdReason_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "TRANSACTIONREASONCODE")
            {
                grdReason.View.CellValueChanged -= grdReason_CellValueChanged;

                DataRow row = grdReason.View.GetFocusedDataRow();

                string strTransactioncode = row["TRANSACTIONREASONCODE"].ToString();

                grdReason.View.SetFocusedRowCellValue("TRANSACTIONREASONCODE", strTransactioncode.ToUpper());// 
                grdReason.View.CellValueChanged += grdReason_CellValueChanged;
            }
        }
        /// <summary>
        /// 코드그룹 리스트 그리드에서 추가 버튼 클릭 시 호출
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void grdMatType_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            var values = Conditions.GetValues();
           
            string _strPlantid = values["P_PLANTID"].ToString();
            grdMatType.View.SetFocusedRowCellValue("ENTERPRISEID", UserInfo.Current.Enterprise.ToString());// 회사코드
            grdMatType.View.SetFocusedRowCellValue("PLANTID", _strPlantid);// plantid

            
        }
        /// <summary>
        /// 코드그룹 리스트 그리드에서 추가 버튼 클릭 시 호출
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void grdReason_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            var values = Conditions.GetValues();
            string _strPlantid = values["P_PLANTID"].ToString();

            grdReason.View.SetFocusedRowCellValue("ENTERPRISEID", UserInfo.Current.Enterprise.ToString());// 회사코드
            grdReason.View.SetFocusedRowCellValue("PLANTID", _strPlantid);// plantid
           
           
        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            //base.OnToolbarSaveClick();

            //if (tabMain.SelectedTabPage.Name.Equals("tapTransactioncode"))
            //{
            //    DataTable changed = grdMatType.GetChangedRows();

            //    ExecuteRule("MatTypeRegistration", changed);
            //}
            //else
            //{
            //    DataTable changed = grdReason.GetChangedRows();

            //    ExecuteRule("MatReasonRegistration", changed);
            //}

            // TODO : 저장 Rule 변경
          
        }
        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            //base.OnToolbarClick(sender, e);
            SmartButton btn = sender as SmartButton;

            if (btn.Name.ToString().Equals("Save"))
            {

                ProcSave(btn.Text);
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
            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values = Commons.CommonFunction.ConvertParameter(values);
            if (tabMain.SelectedTabPage.Name.Equals("tapTransactioncode"))
            {
                DataTable dt = await SqlExecuter.QueryAsync("GetMatTypeRegistration", "10001", values);

                if (dt.Rows.Count < 1) // 검색 조건에 해당하는 DATA가 없는 경우 
                {
                    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                }

                grdMatType.DataSource = dt;
            }
            else if (tabMain.SelectedTabPage.Name.Equals("tapTransactionreasoncode"))
            {
                DataTable dt = await SqlExecuter.QueryAsync("GetMatReasonRegistration", "10001", values);

                if (dt.Rows.Count < 1) // 검색 조건에 해당하는 DATA가 없는 경우 
                {
                    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                }

                grdReason.DataSource = dt;
            }
            else
            {
                DataTable dt = await SqlExecuter.QueryAsync("GetCostcentercode", "10001", values);

                if (dt.Rows.Count < 1) // 검색 조건에 해당하는 DATA가 없는 경우 
                {
                    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                }

                grdCostcentercode.DataSource = dt;
            }

           
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            // site 설정 
            //InitializeConditionPopup_Plant();
            // 유효상태
            InitializeConditionPopup_ValidState();

        }
        /// <summary>
        /// site 설정 
        /// </summary>
        private void InitializeConditionPopup_Plant()
        {
            var plantCbobox = Conditions.AddComboBox("p_plantid", new SqlQuery("GetPlantList", "00001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PLANTID", "PLANTID")
              .SetLabel("PLANT")
              .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false)
              .SetPosition(0.1)
              .SetDefault(UserInfo.Current.Plant, "p_plantId") //기본값 설정 UserInfo.Current.Plant
              .SetValidationIsRequired();
            
        }
        /// <summary>
        ///  유효상태
        /// </summary>
        private void InitializeConditionPopup_ValidState()
        {
            var plantCbobox = Conditions.AddComboBox("p_validstate", new SqlQuery("GetCodeList", "00001", new Dictionary<string, object>() { { "CODECLASSID", "ValidState" }, { "LANGUAGETYPE", UserInfo.Current.LanguageType } }), "CODENAME", "CODEID")
              .SetLabel("VALIDSTATE")
              .SetPosition(0.2)
              .SetEmptyItem("","");

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


            if (tabMain.SelectedTabPage.Name.Equals("tapTransactioncode"))
            {
               
                grdMatType.View.CheckValidation();

                DataTable changed = grdMatType.GetChangedRows();

                if (changed.Rows.Count == 0)
                {
                    // 저장할 데이터가 존재하지 않습니다.
                    throw MessageException.Create("NoSaveData");
                }
            }
            else
            {
                

                grdReason.View.CheckValidation();

                DataTable changed = grdReason.GetChangedRows();

                if (changed.Rows.Count == 0)
                {
                    // 저장할 데이터가 존재하지 않습니다.
                    throw MessageException.Create("NoSaveData");
                }
            }
            // TODO : 유효성 로직 변경
          
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
        private void ProcSave(string strtitle)
        {
            if (tabMain.SelectedTabPage.Name.Equals("tapTransactioncode"))
            {
                grdMatType.View.FocusedRowHandle = grdMatType.View.FocusedRowHandle;
                grdMatType.View.FocusedColumn = grdMatType.View.Columns["TRANSACTIONCODE"];
                grdMatType.View.ShowEditor();
                grdMatType.View.CheckValidation();

                DataTable changedType = grdMatType.GetChangedRows();

                if (changedType.Rows.Count == 0)
                {
                    // 저장할 데이터가 존재하지 않습니다.
                    this.ShowMessage(MessageBoxButtons.OK, "NoSaveData");
                    return;
                }
            }
            else
            {
                grdReason.View.FocusedRowHandle = grdReason.View.FocusedRowHandle;
                grdReason.View.FocusedColumn = grdReason.View.Columns["TRANSACTIONREASONCODE"];
                grdReason.View.ShowEditor();
                grdReason.View.CheckValidation();

                DataTable changed = grdReason.GetChangedRows();

                if (changed.Rows.Count == 0)
                {
                    // 저장할 데이터가 존재하지 않습니다.
                    this.ShowMessage(MessageBoxButtons.OK, "NoSaveData");
                    return;
                }
            }

            DialogResult result = System.Windows.Forms.DialogResult.No;

            result = this.ShowMessage(MessageBoxButtons.YesNo, "OspDoProcessWant", strtitle);//저장하시겠습니까? 

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    this.ShowWaitArea();
                    if (tabMain.SelectedTabPage.Name.Equals("tapTransactioncode"))
                    {
                        DataTable changed = grdMatType.GetChangedRows();

                        ExecuteRule("MatTypeRegistration", changed);
                    }
                    else
                    {
                        DataTable changed = grdReason.GetChangedRows();

                        ExecuteRule("MatReasonRegistration", changed);
                    }
                    ShowMessage("SuccessOspProcess");
                    //재조회 
                    OnSaveConfrimSearch();
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

        private void OnSaveConfrimSearch()
        {

            // TODO : 조회 SP 변경
            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values = Commons.CommonFunction.ConvertParameter(values);
            if (tabMain.SelectedTabPage.Name.Equals("tapTransactioncode"))
            {
                DataTable dt =  SqlExecuter.Query("GetMatTypeRegistration", "10001", values);

                if (dt.Rows.Count < 1) // 검색 조건에 해당하는 DATA가 없는 경우 
                {
                    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                }

                grdMatType.DataSource = dt;
            }
            else
            {
                DataTable dt =  SqlExecuter.Query("GetMatReasonRegistration", "10001", values);

                if (dt.Rows.Count < 1) // 검색 조건에 해당하는 DATA가 없는 경우 
                {
                    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                }

                grdReason.DataSource = dt;
            }

        }
        #endregion
    }
}
