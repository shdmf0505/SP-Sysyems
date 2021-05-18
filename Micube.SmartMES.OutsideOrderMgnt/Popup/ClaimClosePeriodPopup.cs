#region using
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Validations;
using System;
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

namespace Micube.SmartMES.OutsideOrderMgnt.Popup
{
    /// <summary>
    /// 프 로 그 램 명   : Claim 마감 기간등록
    /// 업  무  설  명  : Claim 마감 기간등록
    /// 생    성    자  : 최별
    /// 생    성    일  : 2019-08-28
    /// 수  정  이  력  :    
    /// 
    /// 
    /// </summary>
    public partial class ClaimClosePeriodPopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        public DataRow CurrentDataRow { get; set; }
        string _sPlantid = "";
        #region Local Variables

        /// <summary>
        ///  Handler
        /// </summary>
        /// <param name="dt"></param>
        public delegate void chat_room_evecnt_handler(DataRow row);
        //public event chat_room_evecnt_handler write_handler;
        #endregion

        #region 생성자
        public ClaimClosePeriodPopup()
        {
            InitializeComponent();
            InitializeComboBox();  // 콤보박스 셋팅 
            InitializeEvent();
            InitializeGrid();
            InitializeCondition();



        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SPlantid"></param>
        public ClaimClosePeriodPopup(string SPlantid)
        {
            InitializeComponent();
            _sPlantid = SPlantid;
            InitializeComboBox();  // 콤보박스 셋팅 
            InitializeEvent();
            InitializeGrid();
            InitializeCondition();
            
            DateTime dateNow = DateTime.Now;
            speCloseYm.EditValue = dateNow.ToString("yyyy");
            BtnSearch_Click(null, null);
        }
        /// <summary>        
        /// 코드그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {

            grdPeriod.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export;
            grdPeriod.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdPeriod.View.AddTextBoxColumn("ENTERPRISEID", 100)
                .SetIsHidden();
            grdPeriod.View.AddTextBoxColumn("PLANTID", 100)
                .SetIsHidden();
          
            grdPeriod.View.AddDateEditColumn("PERIODID", 100)
                .SetIsHidden();
            grdPeriod.View.AddTextBoxColumn("PERIODTYPE", 100)
                .SetIsHidden();                                                 
            grdPeriod.View.AddTextBoxColumn("PERIODNAME", 120)
                .SetDisplayFormat("yyyy-MM", MaskTypes.DateTime)
                .SetValidationIsRequired();
           
            //grdPeriod.View.AddDateEditColumn("STARTDATE", 150)
            //    .SetDisplayFormat("yyyy-MM-dd", MaskTypes.DateTime)
            //    .SetValidationIsRequired();

            //grdPeriod.View.AddDateEditColumn("ENDDATE", 150)
            //    .SetDisplayFormat("yyyy-MM-dd", MaskTypes.DateTime)
            //    .SetValidationIsRequired();

            grdPeriod.View.AddComboBoxColumn("PERIODSTATE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=PeriodState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                 .SetDefault("Open")
                 .SetValidationIsRequired();
            grdPeriod.View.AddTextBoxColumn("DESCRIPTION", 250);
            grdPeriod.View.AddTextBoxColumn("PERIODMONTH", 120)
                 .SetDisplayFormat("yyyy-MM", MaskTypes.DateTime)
                 .SetIsHidden();
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

        #region ComboBox  

        /// <summary>
        /// SmartComboBox 컨트롤 추가
        /// </summary>
        private void InitializeComboBox()
        {

            // SITE 
            cboPlantid.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboPlantid.ValueMember = "PLANTID";
            cboPlantid.DisplayMember = "PLANTID";
            cboPlantid.EditValue = _sPlantid;

            cboPlantid.DataSource = SqlExecuter.Query("GetPlantList", "00001"
             , new Dictionary<string, object>() { { "ENTERPRISEID", UserInfo.Current.Enterprise }, { "LANGUAGETYPE", UserInfo.Current.LanguageType } });

            cboPlantid.ShowHeader = false;
            cboPlantid.Enabled = false;
        }

        #endregion
        #endregion

        #region 초기화
        /// <summary>
        /// 조회 조건 초기화
        /// </summary>
        private void InitializeCondition()
        {


        }
        #endregion

        #region  이벤트
        private void InitializeEvent()
        {  // 행 추가시 
            grdPeriod.View.AddingNewRow += View_AddingNewRow;
            grdPeriod.View.CellValueChanged += View_CellValueChanged;
            // 검색
            btnSearch.Click += BtnSearch_Click;
            // 닫기
            btnClose.Click += BtnClose_Click;
            // 저장 
            btnSave.Click += BtnSave_Click;
        }

        /// <summary>
        /// 저장 클릭시
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            //작업장 id null check 
            //중복 체크 periodid  startdate      enddate
            grdPeriod.View.FocusedRowHandle = grdPeriod.View.FocusedRowHandle;
            grdPeriod.View.FocusedColumn = grdPeriod.View.Columns["PERIODSTATE"];
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
            if (CheckPriceDateKeyColumns() == false)
            {
              
                return;
            }
            DataTable changed = grdPeriod.GetChangedRows();

            if (changed.Rows.Count == 0 )
            {
                // 저장할 데이터가 존재하지 않습니다.
                this.ShowMessage(MessageBoxButtons.OK, "NoSaveData");
                return;
            }
            DialogResult result = System.Windows.Forms.DialogResult.No;

            result = this.ShowMessage(MessageBoxButtons.YesNo, "OspDoProcessWant", btnSave.Text);//저장하시겠습니까? 

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    this.ShowWaitArea();
                    btnSearch.Enabled = false;
                    btnSave.Enabled = false;
                    btnClose.Enabled = false;
                    // TODO : 저장 Rule 변경
                    DataTable dtSave = grdPeriod.GetChangedRows(); 

                    ExecuteRule("OutsourcedClaimClosePeriodPopup", dtSave);

                    ShowMessage("SuccessOspProcess");
                    //재조회 처리 
                    BtnSearch_Click(null, null);
                }
                catch (Exception ex)
                {
                    this.ShowError(ex);
                }
                finally
                {
                    this.CloseWaitArea();
                    btnSearch.Enabled = true;
                    btnSave.Enabled = true;
                    btnClose.Enabled = true;

                }
            }

        }
        /// <summary>
        /// 조회 클릭 - 메인 grid에 체크 데이터 전달
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            //필수 체크 년도 
            if (speCloseYm.Text.Trim().ToString().Equals(""))
            {
                this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblCloseYm.Text); //메세지 
                speCloseYm.Focus();
                return;
            }
            Dictionary<string, object> Param = new Dictionary<string, object>();
           
            Param.Add("P_CLOSEYM", speCloseYm.EditValue.ToString());
            Param.Add("P_ENTERPRISEID", UserInfo.Current.Enterprise.ToString());
            Param.Add("P_PERIODTYPE", "Claim");
            Param.Add("P_PLANTID", _sPlantid);
            Param = Commons.CommonFunction.ConvertParameter(Param);
            DataTable dtClaimClose = SqlExecuter.Query("GetClaimClosePeriodPopup", "10001", Param);
            grdPeriod.DataSource = dtClaimClose;
        }

        
        /// <summary>
        /// 닫기 클릭시
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClose_Click(object sender, EventArgs e)
        {

            this.Close();
        }
        /// <summary>
        /// 그리드에서 추가 버튼 클릭 시 호출
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            //회사 ,site ,factory 
            grdPeriod.View.SetFocusedRowCellValue("ENTERPRISEID", UserInfo.Current.Enterprise.ToString());// 회사코드
            grdPeriod.View.SetFocusedRowCellValue("PLANTID", _sPlantid);// plantid
            grdPeriod.View.SetFocusedRowCellValue("PERIODTYPE", "Claim");// PERIODTYPE Open

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
                grdPeriod.View.SetFocusedRowCellValue("PERIODMONTH", dateBudget.ToString("yyyy-MM"));
                grdPeriod.View.CellValueChanged += View_CellValueChanged;
            }
            
        }


        /// <summary>
        /// 단가 기준  key 중복 체크 
        /// </summary>
        /// <returns></returns>
        private bool CheckPriceDateKeyColumns()
        {
            bool blcheck = true;
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

                    if (SearchPeriodidKey(strperiodid ,irow) < 0)
                    {
                        blcheck = true;
                    }
                    else
                    {
                        string lblPeriodid = grdPeriod.View.Columns["PERIODID"].Caption.ToString();
                        this.ShowMessage(MessageBoxButtons.OK, "InValidOspData007", lblPeriodid);
                        return false;
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
        


        #endregion


    }
}
