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

namespace Micube.SmartMES.OutsideOrderMgnt
{
    /// <summary>
    /// 프 로 그 램 명  :  외주관리> 외주 가공비마감 > 외주마감기간관리
    /// 업  무  설  명  :  외주마감기간관리 등록한다.
    /// 생    성    자  : CHOISSTAR
    /// 생    성    일  : 2019-08-13
    /// 수  정  이  력  : 
    /// 
    /// 
    /// 
    /// </summary>
    public partial class OutsourcingDeadline : SmartConditionManualBaseForm
    {
        #region Local Variables

        private string _strPlantid = "";   // plant 변경시 작업 
       
        #endregion

        #region 생성자

        public OutsourcingDeadline()
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
      
        private void InitializeGrid()
        {

            InitializeGrid_Period();
            InitializeGrid_PeriodMajor();
        }

       
        private void InitializeGrid_Period()
        {

            grdPeriod.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export;
           // grdPeriod.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdPeriod.View.AddTextBoxColumn("ENTERPRISEID", 100)
                .SetIsHidden();
            grdPeriod.View.AddTextBoxColumn("PLANTID", 100)
                .SetIsHidden();
            grdPeriod.View.AddTextBoxColumn("FACTORYID", 100)
                .SetIsHidden();
            grdPeriod.View.AddTextBoxColumn("PERIODID", 100)
                .SetIsHidden();
            grdPeriod.View.AddTextBoxColumn("PERIODTYPE", 100)
                .SetIsHidden();                                              
            grdPeriod.View.AddTextBoxColumn("PERIODNAME", 120)
                    .SetDisplayFormat("yyyy-MM", MaskTypes.DateTime)
                    .SetValidationIsRequired()
                    .SetLabel("CLOSEYM");
            grdPeriod.View.AddTextBoxColumn("DESCRIPTION", 250);

            grdPeriod.View.AddComboBoxColumn("PERIODSTATE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=PeriodState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                 .SetDefault("Open")
                 .SetValidationIsRequired();
            

            grdPeriod.View.AddTextBoxColumn("CREATORNAME", 100)
                .SetIsReadOnly();

            grdPeriod.View.AddTextBoxColumn("CREATEDTIME", 150)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetDefault("")
                .SetIsReadOnly();
            grdPeriod.View.AddTextBoxColumn("MODIFIERNAME", 100)
                .SetIsReadOnly();
            grdPeriod.View.AddTextBoxColumn("MODIFIEDTIME", 150)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetDefault("")
                .SetIsReadOnly();
           // grdPeriod.View.SetAutoFillColumn("DESCRIPTION");
            grdPeriod.View.SetKeyColumn("PLANTID", "ENTERPRISEID", "PERIODNAME");
            grdPeriod.View.PopulateColumns();

            //시작일, 종료일  셋팅 처리 
            RepositoryItemDateEdit rendt = new RepositoryItemDateEdit();
            rendt.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            rendt.Mask.EditMask = "yyyy-MM";
            rendt.Mask.UseMaskAsDisplayFormat = true;
            rendt.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearView;
            grdPeriod.View.Columns["PERIODNAME"].ColumnEdit = rendt;

        }

        private void InitializeGrid_PeriodMajor()
        {

            grdPeriodMajor.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export;
            // grdPeriod.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdPeriodMajor.View.AddTextBoxColumn("ENTERPRISEID", 100)
                .SetIsHidden();
            grdPeriodMajor.View.AddTextBoxColumn("PLANTID", 100)
                .SetIsHidden();
            grdPeriodMajor.View.AddTextBoxColumn("FACTORYID", 100)
                .SetIsHidden();
            grdPeriodMajor.View.AddTextBoxColumn("PERIODID", 100)
                .SetIsHidden();
            grdPeriodMajor.View.AddTextBoxColumn("PERIODTYPE", 100)
                .SetIsHidden();
            grdPeriodMajor.View.AddTextBoxColumn("PERIODNAME", 120)
                    .SetDisplayFormat("yyyy-MM", MaskTypes.DateTime)
                    .SetValidationIsRequired()
                    .SetLabel("CLOSEYM");
            grdPeriodMajor.View.AddTextBoxColumn("DESCRIPTION", 250);

            grdPeriodMajor.View.AddComboBoxColumn("PERIODSTATE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=PeriodState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                 .SetDefault("Open")
                 .SetValidationIsRequired();


            grdPeriodMajor.View.AddTextBoxColumn("CREATORNAME", 100)
                .SetIsReadOnly();

            grdPeriodMajor.View.AddTextBoxColumn("CREATEDTIME", 150)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetDefault("")
                .SetIsReadOnly();
            grdPeriodMajor.View.AddTextBoxColumn("MODIFIERNAME", 100)
                .SetIsReadOnly();
            grdPeriodMajor.View.AddTextBoxColumn("MODIFIEDTIME", 150)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetDefault("")
                .SetIsReadOnly();
            // grdPeriod.View.SetAutoFillColumn("DESCRIPTION");
            grdPeriodMajor.View.SetKeyColumn("PLANTID", "ENTERPRISEID", "PERIODNAME");
            grdPeriodMajor.View.PopulateColumns();

            //시작일, 종료일  셋팅 처리 
            RepositoryItemDateEdit rendt = new RepositoryItemDateEdit();
            rendt.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            rendt.Mask.EditMask = "yyyy-MM";
            rendt.Mask.UseMaskAsDisplayFormat = true;
            rendt.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearView;
            grdPeriodMajor.View.Columns["PERIODNAME"].ColumnEdit = rendt;

        }
        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // 행 추가시 
            grdPeriod.View.AddingNewRow += View_AddingNewRow;
            grdPeriod.View.CellValueChanged += View_CellValueChanged;
            grdPeriodMajor.View.AddingNewRow += GrdPeriodMajor_AddingNewRow;
            grdPeriodMajor.View.CellValueChanged += GrdPeriodMajor_CellValueChanged;

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
            string PeriodTypeOSP = "OutSourcing";
            grdPeriod.View.SetFocusedRowCellValue("ENTERPRISEID", UserInfo.Current.Enterprise.ToString());// 회사코드
            grdPeriod.View.SetFocusedRowCellValue("PLANTID", _strPlantid);// plantid
            grdPeriod.View.SetFocusedRowCellValue("PERIODTYPE", PeriodTypeOSP);

        }
        /// <summary>
        ///  포맷 변경 처리 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

            if (e.Column.FieldName == "PERIODNAME")
            {
                grdPeriod.View.CellValueChanged -= View_CellValueChanged;

                DataRow row = grdPeriod.View.GetFocusedDataRow();

                if (row["PERIODNAME"].ToString().Equals(""))
                {

                    grdPeriod.View.CellValueChanged += View_CellValueChanged;
                    return;
                }
                DateTime dateBudget = Convert.ToDateTime(row["PERIODNAME"].ToString());
                grdPeriod.View.SetFocusedRowCellValue("PERIODNAME", dateBudget.ToString("yyyy-MM"));

                grdPeriod.View.CellValueChanged += View_CellValueChanged;
            }

        }

        /// <summary>
        /// 그리드에서 추가 버튼 클릭 시 호출
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void GrdPeriodMajor_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            //회사 ,site ,factory 
            //먼저조회 여부 확인 처리 
            var values = Conditions.GetValues();
          
            _strPlantid = values["P_PLANTID"].ToString();
            string PeriodTypeOSP = "MajorSuppliers";
            grdPeriodMajor.View.SetFocusedRowCellValue("ENTERPRISEID", UserInfo.Current.Enterprise.ToString());// 회사코드
            grdPeriodMajor.View.SetFocusedRowCellValue("PLANTID", _strPlantid);// plantid
            grdPeriodMajor.View.SetFocusedRowCellValue("PERIODTYPE", PeriodTypeOSP);

        }
        /// <summary>
        ///  포맷 변경 처리 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdPeriodMajor_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

            if (e.Column.FieldName == "PERIODNAME")
            {
                grdPeriodMajor.View.CellValueChanged -= GrdPeriodMajor_CellValueChanged;

                DataRow row = grdPeriodMajor.View.GetFocusedDataRow();

                if (row["PERIODNAME"].ToString().Equals(""))
                {

                    grdPeriodMajor.View.CellValueChanged += GrdPeriodMajor_CellValueChanged;
                    return;
                }
                DateTime dateBudget = Convert.ToDateTime(row["PERIODNAME"].ToString());
                grdPeriodMajor.View.SetFocusedRowCellValue("PERIODNAME", dateBudget.ToString("yyyy-MM"));

                grdPeriodMajor.View.CellValueChanged += GrdPeriodMajor_CellValueChanged;
            }

        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            //base.OnToolbarSaveClick();

            //// TODO : 저장 Rule 변경
            //DataTable changed = grdPeriod.GetChangedRows();

            //ExecuteRule("OutsourcingDeadline", changed);
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
            
            if (tabClose.SelectedTabPage.Name.Equals("tapExpense"))
            {
                values.Add("P_PERIODTYPE", "OutSourcing");
                _strPlantid = values["P_PLANTID"].ToString();
                values = Commons.CommonFunction.ConvertParameter(values);
                DataTable dt = await SqlExecuter.QueryAsync("GetOutsourcingDeadline", "10001", values);
                if (dt.Rows.Count < 1) // 검색 조건에 해당하는 DATA가 없는 경우 
                {
                    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.'

                    DataTable dtPrice = (grdPeriod.DataSource as DataTable).Clone();
                    //grdDetail.View.ClearDatas();
                    grdPeriod.DataSource = dtPrice;
                }
                else
                {
                    grdPeriod.DataSource = dt;
                }
            }

            else if (tabClose.SelectedTabPage.Name.Equals("tapMajor"))
            {

                values.Add("P_PERIODTYPE", "MajorSuppliers");
                _strPlantid = values["P_PLANTID"].ToString();
                values = Commons.CommonFunction.ConvertParameter(values);
                DataTable dt = await SqlExecuter.QueryAsync("GetOutsourcingDeadline", "10001", values);
                if (dt.Rows.Count < 1) // 검색 조건에 해당하는 DATA가 없는 경우 
                {
                    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.'

                    DataTable dtPrice = (grdPeriodMajor.DataSource as DataTable).Clone();
                    //grdDetail.View.ClearDatas();
                    grdPeriodMajor.DataSource = dtPrice;
                }
                else
                {
                    grdPeriodMajor.DataSource = dt;
                }
            }
            
            
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            // site
          //  InitializeConditionPopup_Plant();
            
            // 기간 
            InitializeCondition_Closeyear();

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
              .SetValidationIsRequired()
              .SetIsReadOnly();

        }
       
        /// <summary>
        /// 마감년월
        /// </summary>
        private void InitializeCondition_Closeyear()
        {
            DateTime dateNow = DateTime.Now;
           string stryear = dateNow.ToString("yyyy");
            var txtLotid = Conditions.AddTextBox("p_closeym")
               .SetLabel("CLOSEYEAR")
               .SetDefault(stryear)
               .SetPosition(0.4);
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
            grdPeriod.View.FocusedRowHandle = grdPeriod.View.FocusedRowHandle;
            grdPeriod.View.FocusedColumn = grdPeriod.View.Columns["DESCRIPTION"];
            grdPeriod.View.ShowEditor();
            for (int i = 0; i < grdPeriod.View.DataRowCount; i++)
            {
                //PERIODID STARTDATE  ENDDATE Periodid Startdate  Enddate
                string strPeriodid = grdPeriod.View.GetRowCellValue(i, "PERIODNAME").ToString();
                if (strPeriodid.Equals(""))
                {
                    string lblPeriodid = grdPeriod.View.Columns["PERIODNAME"].Caption.ToString();
                    throw MessageException.Create( "InValidOspRequiredField", lblPeriodid); //메세지
                    
                }
                string strStartdate = grdPeriod.View.GetRowCellValue(i, "STARTDATE").ToString();
                if (strStartdate.Equals(""))
                {
                    string lblStartdate = grdPeriod.View.Columns["STARTDATE"].Caption.ToString();
                    throw MessageException.Create( "InValidOspRequiredField", lblStartdate); //메세지
                   
                }
                string strEnddate = grdPeriod.View.GetRowCellValue(i, "ENDDATE").ToString();
                if (strEnddate.Equals(""))
                {
                    string lblEnddate = grdPeriod.View.Columns["ENDDATE"].Caption.ToString();
                    throw MessageException.Create( "InValidOspRequiredField", lblEnddate); //메세지
                    
                }
                DateTime StartDate = Convert.ToDateTime(strStartdate);
                DateTime EndDate = Convert.ToDateTime(strEnddate);
                if (StartDate > EndDate)
                {
                    // 다국어 메세지 처리 (종료일 보다 시작일이 자료가 존재합니다.)
                    throw MessageException.Create("OspCheckStartEnd");
                   
                }
            }
            if (CheckPriceDateKeyColumns() == false)
            {

                return;
            }
            // TODO : 유효성 로직 변경
            grdPeriod.View.CheckValidation();

            DataTable changed = grdPeriod.GetChangedRows();

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
        /// <summary>
        /// 단가 기준  key 중복 체크 
        /// </summary>
        /// <returns></returns>
        private bool CheckPriceDateKeyColumns()
        {
            bool blcheck = true;
            if (tabClose.SelectedTabPage.Name.Equals("tapExpense"))
            {
                if (grdPeriod.View.DataRowCount == 0)
                {
                    return blcheck;
                }

                for (int irow = 0; irow < grdPeriod.View.DataRowCount; irow++)
                {

                    DataRow row = grdPeriod.View.GetDataRow(irow);

                    if (row.RowState == DataRowState.Added || row.RowState == DataRowState.Modified)
                    {

                        string strperiodid = row["PERIODNAME"].ToString();

                        if (SearchPeriodidKey(strperiodid, irow) < 0)
                        {
                            blcheck = true;
                        }
                        else
                        {
                            string lblPeriodid = grdPeriod.View.Columns["PERIODNAME"].Caption.ToString();
                            this.ShowMessage(MessageBoxButtons.OK, "InValidOspData007", lblPeriodid);
                            return false;
                        }

                    }
                }
            }

            else if (tabClose.SelectedTabPage.Name.Equals("tapMajor"))
            {
                if (grdPeriodMajor.View.DataRowCount == 0)
                {
                    return blcheck;
                }

                for (int irow = 0; irow < grdPeriodMajor.View.DataRowCount; irow++)
                {

                    DataRow row = grdPeriodMajor.View.GetDataRow(irow);

                    if (row.RowState == DataRowState.Added || row.RowState == DataRowState.Modified)
                    {

                        string strperiodid = row["PERIODNAME"].ToString();

                        if (SearchPeriodidKeyMajor(strperiodid, irow) < 0)
                        {
                            blcheck = true;
                        }
                        else
                        {
                            string lblPeriodid = grdPeriodMajor.View.Columns["PERIODNAME"].Caption.ToString();
                            this.ShowMessage(MessageBoxButtons.OK, "InValidOspData007", lblPeriodid);
                            return false;
                        }

                    }
                }

            }
            return blcheck;
        }
        /// <summary>
        /// Periodid 중복 체크 함수
        /// </summary>
        /// <param name="dateStartdate"></param>
        /// <param name="dateEnddate"></param>
        /// <param name="icurRow"></param>
        /// <returns></returns>
        private int SearchPeriodidKey(string strperiodid, int icurRow)
        {
            int iresultRow = -1;

            for (int irow = 0; irow < grdPeriod.View.DataRowCount; irow++)
            {
                if (icurRow != irow)
                {
                    DataRow row = grdPeriod.View.GetDataRow(irow);

                    // 행 삭제만 제외 
                    if (grdPeriod.View.IsDeletedRow(row) == false)
                    {
                        string strTempperiodid = row["PERIODNAME"].ToString();

                        if (strperiodid.Equals(strTempperiodid))
                        {
                            return irow;
                        }

                    }
                }
            }
            return iresultRow;
        }
        /// <summary>
        /// Periodid 중복 체크 함수
        /// </summary>
        /// <param name="dateStartdate"></param>
        /// <param name="dateEnddate"></param>
        /// <param name="icurRow"></param>
        /// <returns></returns>
        private int SearchPeriodidKeyMajor(string strperiodid, int icurRow)
        {
            int iresultRow = -1;

            for (int irow = 0; irow < grdPeriodMajor.View.DataRowCount; irow++)
            {
                if (icurRow != irow)
                {
                    DataRow row = grdPeriodMajor.View.GetDataRow(irow);

                    // 행 삭제만 제외 
                    if (grdPeriodMajor.View.IsDeletedRow(row) == false)
                    {
                        string strTempperiodid = row["PERIODNAME"].ToString();

                        if (strperiodid.Equals(strTempperiodid))
                        {
                            return irow;
                        }

                    }
                }
            }
            return iresultRow;
        }

        private void ProcSave(string strtitle)
        {
            if (tabClose.SelectedTabPage.Name.Equals("tapExpense"))
            {
                grdPeriod.View.FocusedRowHandle = grdPeriod.View.FocusedRowHandle;
                grdPeriod.View.FocusedColumn = grdPeriod.View.Columns["DESCRIPTION"];
                grdPeriod.View.ShowEditor();
                for (int i = 0; i < grdPeriod.View.DataRowCount; i++)
                {
                    //PERIODID STARTDATE  ENDDATE Periodid Startdate  Enddate
                    string strPeriodid = grdPeriod.View.GetRowCellValue(i, "PERIODNAME").ToString();
                    if (strPeriodid.Equals(""))
                    {
                        string lblPeriodid = grdPeriod.View.Columns["PERIODNAME"].Caption.ToString();
                        this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblPeriodid); //메세지
                        return;

                    }

                }
            }

            else if (tabClose.SelectedTabPage.Name.Equals("tapMajor"))
            {
                grdPeriodMajor.View.FocusedRowHandle = grdPeriodMajor.View.FocusedRowHandle;
                grdPeriodMajor.View.FocusedColumn = grdPeriodMajor.View.Columns["DESCRIPTION"];
                grdPeriodMajor.View.ShowEditor();
                for (int i = 0; i < grdPeriodMajor.View.DataRowCount; i++)
                {
                    //PERIODID STARTDATE  ENDDATE Periodid Startdate  Enddate
                    string strPeriodid = grdPeriodMajor.View.GetRowCellValue(i, "PERIODNAME").ToString();
                    if (strPeriodid.Equals(""))
                    {
                        string lblPeriodid = grdPeriodMajor.View.Columns["PERIODNAME"].Caption.ToString();
                        this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblPeriodid); //메세지
                        return;

                    }

                }

            }
           
            if (CheckPriceDateKeyColumns() == false)
            {

                return;
            }
            // TODO : 유효성 로직 변경
            if (tabClose.SelectedTabPage.Name.Equals("tapExpense"))
            {
                grdPeriod.View.CheckValidation();

                DataTable changed = grdPeriod.GetChangedRows();

                if (changed.Rows.Count == 0)
                {
                    // 저장할 데이터가 존재하지 않습니다.
                    this.ShowMessage(MessageBoxButtons.OK, "NoSaveData");
                    return;
                }
            }

            else if (tabClose.SelectedTabPage.Name.Equals("tapMajor"))
            {
                grdPeriodMajor.View.CheckValidation();

                DataTable changed = grdPeriodMajor.GetChangedRows();

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
                    if (tabClose.SelectedTabPage.Name.Equals("tapExpense"))
                    {
                        DataTable dtSave = grdPeriod.GetChangedRows();

                        ExecuteRule("OutsourcingDeadline", dtSave);

                    }
                    else if (tabClose.SelectedTabPage.Name.Equals("tapMajor"))
                    {
                        DataTable dtSave = grdPeriodMajor.GetChangedRows();
                        ExecuteRule("OutsourcingDeadline", dtSave);

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
           

            if (tabClose.SelectedTabPage.Name.Equals("tapExpense"))
            {
                values.Add("P_PERIODTYPE", "OutSourcing");
                _strPlantid = values["P_PLANTID"].ToString();
                values = Commons.CommonFunction.ConvertParameter(values);
                DataTable dt =  SqlExecuter.Query("GetOutsourcingDeadline", "10001", values);
                if (dt.Rows.Count < 1) // 검색 조건에 해당하는 DATA가 없는 경우 
                {
                    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.'

                    DataTable dtPrice = (grdPeriod.DataSource as DataTable).Clone();
                    //grdDetail.View.ClearDatas();
                    grdPeriod.DataSource = dtPrice;
                }
                else
                {
                    grdPeriod.DataSource = dt;
                }
            }

            else if (tabClose.SelectedTabPage.Name.Equals("tapMajor"))
            {

                values.Add("P_PERIODTYPE", "MajorSuppliers");
                _strPlantid = values["P_PLANTID"].ToString();
                values = Commons.CommonFunction.ConvertParameter(values);
                DataTable dt = SqlExecuter.Query("GetOutsourcingDeadline", "10001", values);
                if (dt.Rows.Count < 1) // 검색 조건에 해당하는 DATA가 없는 경우 
                {
                    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.'

                    DataTable dtPrice = (grdPeriodMajor.DataSource as DataTable).Clone();
                    //grdDetail.View.ClearDatas();
                    grdPeriodMajor.DataSource = dtPrice;
                }
                else
                {
                    grdPeriodMajor.DataSource = dt;
                }
            }

        }
        #endregion

        private void grdPeriod_Load(object sender, EventArgs e)
        {

        }
    }
}
