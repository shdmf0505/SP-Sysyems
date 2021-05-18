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
using DevExpress.XtraEditors.Repository;

#endregion

namespace Micube.SmartMES.MaterialsManagement
{
    /// <summary>
    /// 프 로 그 램 명  :  자재관리 >자재 기준 정보  > 재고실사기준등록
    /// 업  무  설  명  :  재고 실사 기준정보를 관리한다.
    /// 생    성    자  : 최별
    /// 생    성    일  : 2019-07-11
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class MatInventoryPeriod : SmartConditionManualBaseForm
    {
        #region Local Variables
        private string _strPlantid = "";   // plant 변경시 작업 
        private string _strFactoryid = ""; //factoryid 변경시 작업 
       
        #endregion

        #region 생성자

        public MatInventoryPeriod()
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
            //이벤트 
            InitializeEvent();
            // TODO : 컨트롤 초기화 로직 구성
            InitializeGrid();
            // 작업용 plant 셋팅 (조회시 다시 셋팅)
            _strPlantid = UserInfo.Current.Plant.ToString();  
        }

        /// <summary>        
        /// 코드그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            grdInventoryPeriod.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export;
            grdInventoryPeriod.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdInventoryPeriod.View.AddTextBoxColumn("ENTERPRISEID", 100)
                .SetIsHidden();
            grdInventoryPeriod.View.AddTextBoxColumn("PLANTID", 100)
                .SetIsHidden();
            grdInventoryPeriod.View.AddTextBoxColumn("FACTORYID", 100)
                .SetIsHidden(); 
            grdInventoryPeriod.View.AddTextBoxColumn("PERIODID", 100)
                .SetIsHidden();
            grdInventoryPeriod.View.AddTextBoxColumn("PERIODTYPE", 100)
                .SetIsHidden();                                                  //InventoryCheck 셋팅 처리 
            grdInventoryPeriod.View.AddTextBoxColumn("PERIODNAME", 200);

            grdInventoryPeriod.View.AddTextBoxColumn("STARTTIME", 150)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime)
                .SetValidationIsRequired();

            grdInventoryPeriod.View.AddTextBoxColumn("ENDTIME", 150)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime)
                .SetValidationIsRequired();

            grdInventoryPeriod.View.AddComboBoxColumn("PERIODSTATE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=PeriodState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                 .SetDefault("Open")
                 .SetValidationIsRequired();

            grdInventoryPeriod.View.AddTextBoxColumn("MATREMARK",300);

            grdInventoryPeriod.View.AddTextBoxColumn("CREATORNAME", 100)
                .SetIsReadOnly(); 
            grdInventoryPeriod.View.AddTextBoxColumn("CREATEDTIME", 150)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetDefault("")
                .SetIsReadOnly();
            grdInventoryPeriod.View.AddTextBoxColumn("MODIFIERNAME", 100)
                .SetIsReadOnly(); 
            grdInventoryPeriod.View.AddTextBoxColumn("MODIFIEDTIME", 150)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetDefault("")
                .SetIsReadOnly();
            grdInventoryPeriod.View.SetKeyColumn("PLANTID", "STARTTIME", "ENDTIME");
            grdInventoryPeriod.View.PopulateColumns();

            //시작일, 종료일  셋팅 처리 
            RepositoryItemDateEdit rendt = new RepositoryItemDateEdit();
            rendt.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            rendt.Mask.EditMask = "yyyy-MM-dd HH:mm:ss";
            rendt.Mask.UseMaskAsDisplayFormat = true;
            grdInventoryPeriod.View.Columns["STARTTIME"].ColumnEdit = rendt;
            grdInventoryPeriod.View.Columns["ENDTIME"].ColumnEdit = rendt;

        }

    #endregion

        #region Event

    /// <summary>
    /// 화면에서 사용되는 이벤트를 초기화한다.
    /// </summary>
        public void InitializeEvent()
        {
            // 행 추가시 
            grdInventoryPeriod.View.AddingNewRow += View_AddingNewRow;
           

        }

        /// <summary>
        /// 그리드에서 추가 버튼 클릭 시 호출
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            //회사 ,site ,factory 
            //먼저조회 여부 확인 처리 
            var values = Conditions.GetValues();
            
            _strPlantid = values["P_PLANTID"].ToString();
            _strFactoryid = values["P_FACTORYID"].ToString();
            string strFactory = Conditions.GetControl<SmartComboBox>("P_FACTORYID").LabelText;
            int intfocusRow = grdInventoryPeriod.View.FocusedRowHandle;
            if (_strFactoryid.Equals(""))
            {
                this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", strFactory); //메세지 
                grdInventoryPeriod.View.DeleteRow(intfocusRow);
                return;
            }

            grdInventoryPeriod.View.SetFocusedRowCellValue("ENTERPRISEID", UserInfo.Current.Enterprise.ToString());// 회사코드
            grdInventoryPeriod.View.SetFocusedRowCellValue("PLANTID", _strPlantid);// plantid
            grdInventoryPeriod.View.SetFocusedRowCellValue("FACTORYID", _strFactoryid);// plantid
            grdInventoryPeriod.View.SetFocusedRowCellValue("PERIODTYPE", "InventoryCheck");// PERIODTYPE Open
           
        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
           // base.OnToolbarSaveClick();
           // //TODO: 저장 Rule 변경
           //DataTable changed = grdInventoryPeriod.GetChangedRows();

           // ExecuteRule("MatInventoryPeriod", changed);
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
            _strPlantid  = values["P_PLANTID"].ToString();
            _strFactoryid = values["P_FACTORYID"].ToString();
            #region 기간 검색형 전환 처리 
            if (!(values["P_SEARCHDATE_PERIODFR"].ToString().Equals("")))
            {
                DateTime requestDateFr = Convert.ToDateTime(values["P_SEARCHDATE_PERIODFR"]);
                values.Remove("P_SEARCHDATE_PERIODFR");
                values.Add("P_SEARCHDATE_PERIODFR", string.Format("{0:yyyy-MM-dd}", requestDateFr));
            }
            if (!(values["P_SEARCHDATE_PERIODTO"].ToString().Equals("")))
            {
                DateTime requestDateTo = Convert.ToDateTime(values["P_SEARCHDATE_PERIODTO"]);
                values.Remove("P_SEARCHDATE_PERIODTO");
                values.Add("P_SEARCHDATE_PERIODTO", string.Format("{0:yyyy-MM-dd}", requestDateTo));
            }

            #endregion

            values.Add("PERIODTYPE", "InventoryCheck");
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable dt = await SqlExecuter.QueryAsync("GetMatInventoryPeriod", "10001", values);

            if (dt.Rows.Count < 1) // 검색 조건에 해당하는 DATA가 없는 경우 
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdInventoryPeriod.DataSource = dt;
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();
            // site
            //InitializeConditionPopup_Plant();
            // factory
            InitializeConditionPopup_Factory();
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
        /// factory 설정 
        /// </summary>
        private void InitializeConditionPopup_Factory()
        {
            var plantCbobox = Conditions.AddComboBox("p_factoryid", new SqlQuery("GetFactoryListByCsm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "FACTORYNAME", "FACTORYID")
              .SetLabel("FACTORYID")
              .SetPosition(0.2)
              .SetValidationIsRequired()
              .SetRelationIds("p_plantid");
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
            grdInventoryPeriod.View.CheckValidation();

            DataTable changed = grdInventoryPeriod.GetChangedRows();

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
        private void ProcSave(string strtitle)
        {
            grdInventoryPeriod.View.FocusedRowHandle = grdInventoryPeriod.View.FocusedRowHandle;
            grdInventoryPeriod.View.FocusedColumn = grdInventoryPeriod.View.Columns["CREATORNAME"];
            grdInventoryPeriod.View.ShowEditor();
            // TODO : 유효성 로직 변경
            grdInventoryPeriod.View.CheckValidation();

            DataTable changed = grdInventoryPeriod.GetChangedRows();

            if (changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                this.ShowMessage(MessageBoxButtons.OK, "NoSaveData");
                return;
            }
            DialogResult result = System.Windows.Forms.DialogResult.No;

            result = this.ShowMessage(MessageBoxButtons.YesNo, "OspDoProcessWant", strtitle);//저장하시겠습니까? 

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    this.ShowWaitArea();
                    DataTable dtSave = grdInventoryPeriod.GetChangedRows();

                    ExecuteRule("MatInventoryPeriod", dtSave);
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
            _strPlantid = values["P_PLANTID"].ToString();
            _strFactoryid = values["P_FACTORYID"].ToString();
            #region 기간 검색형 전환 처리 
            if (!(values["P_SEARCHDATE_PERIODFR"].ToString().Equals("")))
            {
                DateTime requestDateFr = Convert.ToDateTime(values["P_SEARCHDATE_PERIODFR"]);
                values.Remove("P_SEARCHDATE_PERIODFR");
                values.Add("P_SEARCHDATE_PERIODFR", string.Format("{0:yyyy-MM-dd}", requestDateFr));
            }
            if (!(values["P_SEARCHDATE_PERIODTO"].ToString().Equals("")))
            {
                DateTime requestDateTo = Convert.ToDateTime(values["P_SEARCHDATE_PERIODTO"]);
                values.Remove("P_SEARCHDATE_PERIODTO");
                values.Add("P_SEARCHDATE_PERIODTO", string.Format("{0:yyyy-MM-dd}", requestDateTo));
            }

            #endregion

            values.Add("PERIODTYPE", "InventoryCheck");
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable dt = SqlExecuter.Query("GetMatInventoryPeriod", "10001", values);

            if (dt.Rows.Count < 1) // 검색 조건에 해당하는 DATA가 없는 경우 
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdInventoryPeriod.DataSource = dt;

        }
        #endregion
    }
}
