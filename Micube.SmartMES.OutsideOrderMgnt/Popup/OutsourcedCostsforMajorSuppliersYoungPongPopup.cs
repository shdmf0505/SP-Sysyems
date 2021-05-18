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
    /// 프 로 그 램 명   : 주요협력사정산내역 조회 
    /// 업  무  설  명  : 주요협력사정산내역 조회 
    /// 생    성    자  : 최별
    /// 생    성    일  : 2019-08-28
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class OutsourcedCostsforMajorSuppliersYoungPongPopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        public DataRow CurrentDataRow { get; set; }
        string _sPlantid = "";
        string _speriodid = "";
        #region Local Variables

        /// <summary>
        ///  Handler
        /// </summary>
        /// <param name="dt"></param>
        public delegate void chat_room_evecnt_handler(DataRow row);
        //public event chat_room_evecnt_handler write_handler;
        #endregion

        #region 생성자
        public OutsourcedCostsforMajorSuppliersYoungPongPopup()
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
        public OutsourcedCostsforMajorSuppliersYoungPongPopup(string strperiodid, string strplantid, string strvendorid, string strvendorname, string strperiodname)
        {
            InitializeComponent();
            _sPlantid = strplantid;
            _speriodid = strperiodid;
            InitializeComboBox();  // 콤보박스 셋팅 
            InitializeEvent();
            InitializeGrid();
            InitializeCondition();
            txtPeriodname.Text = strperiodname;
            txtOspVendorid.Text = strvendorid;
            txtOspVendorName.Text = strvendorname;



        }


        /// <summary>        
        /// 코드그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {

            // TODO : 그리드 초기화 로직 추가
            grdClaimConfirm.GridButtonItem = GridButtonItem.Export;
            grdClaimConfirm.View.SetIsReadOnly();
            grdClaimConfirm.View.AddTextBoxColumn("ENTERPRISEID", 120).SetIsHidden();            //  
            grdClaimConfirm.View.AddTextBoxColumn("PERIODID", 120).SetIsHidden();            //  
            grdClaimConfirm.View.AddTextBoxColumn("VENDORID", 120).SetIsHidden();            //  
            grdClaimConfirm.View.AddTextBoxColumn("PLANTID", 120).SetIsHidden();            // 
            grdClaimConfirm.View.AddTextBoxColumn("PERFORMANCEDATE", 120)
                .SetIsReadOnly();       // 

            grdClaimConfirm.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 120)
                .SetIsReadOnly();       // 
            grdClaimConfirm.View.AddTextBoxColumn("AREANAME", 120)
                .SetIsHidden();       // 
            grdClaimConfirm.View.AddTextBoxColumn("OSPVENDORNAME", 120)
                .SetIsReadOnly();        // 
            grdClaimConfirm.View.AddTextBoxColumn("PRODUCTDEFID", 100)
                .SetIsReadOnly();
            grdClaimConfirm.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80)
                .SetIsReadOnly();
            grdClaimConfirm.View.AddTextBoxColumn("PRODUCTDEFNAME", 200)
                .SetIsReadOnly();
            grdClaimConfirm.View.AddTextBoxColumn("LOTID", 200)
                .SetIsReadOnly();
            grdClaimConfirm.View.AddTextBoxColumn("PCSQTY", 120)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);                                //  pcsqty

            grdClaimConfirm.View.AddTextBoxColumn("PANELQTY", 120)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);                                //  panelqty

            grdClaimConfirm.View.AddTextBoxColumn("OSPPRICE", 100)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0.##", MaskTypes.Numeric);

            grdClaimConfirm.View.AddTextBoxColumn("OSPAMOUNT", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsReadOnly();
            grdClaimConfirm.View.AddTextBoxColumn("ISERROR", 120)
                .SetIsReadOnly();       // 
            grdClaimConfirm.View.AddTextBoxColumn("ISMAJORCLOSE", 120)
                .SetIsReadOnly();       // 
            grdClaimConfirm.View.PopulateColumns();



        }

        #region ComboBox  

        /// <summary>
        /// SmartComboBox 컨트롤 추가
        /// </summary>
        private void InitializeComboBox()
        {
            cboIssettle.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboIssettle.ValueMember = "CODEID";
            cboIssettle.DisplayMember = "CODENAME";
            cboIssettle.EditValue = "Y";

            cboIssettle.DataSource = SqlExecuter.Query("GetCodeAllListByOsp", "10001"
              , new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "CODECLASSID", "YesNo" } });

            cboIssettle.ShowHeader = false;

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
            grdClaimConfirm.View.AddingNewRow += View_AddingNewRow;
            grdClaimConfirm.View.CellValueChanged += View_CellValueChanged;
            // 검색
            btnSearch.Click += BtnSearch_Click;
            // 닫기
            btnClose.Click += BtnClose_Click;

        }


        /// <summary>
        /// 조회 클릭 - 메인 grid에 체크 데이터 전달
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            //필수 체크 년도 
            string strIsmajorclose = "";
            if (!(cboIssettle.Text.ToString().Equals("")))
            {
                strIsmajorclose = cboIssettle.EditValue.ToString();
            }
            Dictionary<string, object> Param = new Dictionary<string, object>();

            Param.Add("P_PERIODID", _speriodid);
            Param.Add("P_ENTERPRISEID", UserInfo.Current.Enterprise.ToString());
            Param.Add("P_PLANTID", _sPlantid);
            Param.Add("P_PERIODTYPE", "MajorSuppliers");
            Param.Add("P_VENDORID", txtOspVendorid.Text.ToString());
            Param.Add("P_ISMAJORCLOSE", strIsmajorclose);
            Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            Param = Commons.CommonFunction.ConvertParameter(Param);
            DataTable dtClaimClose = SqlExecuter.Query("GetOutsourcedCostsforMajorSuppliersPopup", "10001", Param);
            grdClaimConfirm.DataSource = dtClaimClose;
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
            grdClaimConfirm.View.SetFocusedRowCellValue("ENTERPRISEID", UserInfo.Current.Enterprise.ToString());// 회사코드
            grdClaimConfirm.View.SetFocusedRowCellValue("PLANTID", _sPlantid);// plantid
            grdClaimConfirm.View.SetFocusedRowCellValue("PERIODTYPE", "Claim");// PERIODTYPE Open

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
                grdClaimConfirm.View.CellValueChanged -= View_CellValueChanged;

                DataRow row = grdClaimConfirm.View.GetFocusedDataRow();

                if (row["PERIODNAME"].ToString().Equals(""))
                {
                    grdClaimConfirm.View.CellValueChanged += View_CellValueChanged;
                    return;
                }
                DateTime dateBudget = Convert.ToDateTime(row["PERIODNAME"].ToString());
                grdClaimConfirm.View.SetFocusedRowCellValue("PERIODNAME", dateBudget.ToString("yyyy-MM"));
                string strStartdate = dateBudget.ToString("yyyy-MM-01");
                grdClaimConfirm.View.SetFocusedRowCellValue("STARTDATE", strStartdate);
                DateTime dateEnd = Convert.ToDateTime(strStartdate);
                dateEnd = dateEnd.AddMonths(1);
                dateEnd = dateEnd.AddDays(-1);
                grdClaimConfirm.View.SetFocusedRowCellValue("ENDDATE", dateEnd.ToString("yyyy-MM-dd"));
                grdClaimConfirm.View.CellValueChanged += View_CellValueChanged;
            }
            if (e.Column.FieldName == "STARTDATE")
            {
                grdClaimConfirm.View.CellValueChanged -= View_CellValueChanged;

                DataRow row = grdClaimConfirm.View.GetFocusedDataRow();

                if (row["STARTDATE"].ToString().Equals(""))
                {
                    grdClaimConfirm.View.CellValueChanged += View_CellValueChanged;
                    return;
                }
                DateTime dateBudget = Convert.ToDateTime(row["STARTDATE"].ToString());
                grdClaimConfirm.View.SetFocusedRowCellValue("STARTDATE", dateBudget.ToString("yyyy-MM-dd"));
                grdClaimConfirm.View.CellValueChanged += View_CellValueChanged;
            }
            if (e.Column.FieldName == "ENDDATE")
            {
                grdClaimConfirm.View.CellValueChanged -= View_CellValueChanged;

                DataRow row = grdClaimConfirm.View.GetFocusedDataRow();

                if (row["ENDDATE"].ToString().Equals(""))
                {
                    grdClaimConfirm.View.CellValueChanged += View_CellValueChanged;
                    return;
                }
                DateTime dateBudget = Convert.ToDateTime(row["ENDDATE"].ToString());
                grdClaimConfirm.View.SetFocusedRowCellValue("ENDDATE", dateBudget.ToString("yyyy-MM-dd"));
                grdClaimConfirm.View.CellValueChanged += View_CellValueChanged;
            }
        }


        /// <summary>
        /// 단가 기준  key 중복 체크 
        /// </summary>
        /// <returns></returns>
        private bool CheckPriceDateKeyColumns()
        {
            bool blcheck = true;
            if (grdClaimConfirm.View.DataRowCount == 0)
            {
                return blcheck;
            }

            for (int irow = 0; irow < grdClaimConfirm.View.DataRowCount; irow++)
            {

                DataRow row = grdClaimConfirm.View.GetDataRow(irow);

                if (row.RowState == DataRowState.Added || row.RowState == DataRowState.Modified)
                {

                    DateTime dateStartdate = Convert.ToDateTime(row["STARTDATE"]);
                    DateTime dateEnddate = Convert.ToDateTime(row["ENDDATE"]);
                    string strperiodid = row["PERIODNAME"].ToString();

                    if (SearchPeriodidKey(strperiodid, irow) < 0)
                    {
                        blcheck = true;
                    }
                    else
                    {
                        string lblPeriodid = grdClaimConfirm.View.Columns["PERIODID"].Caption.ToString();
                        this.ShowMessage(MessageBoxButtons.OK, "InValidOspData007", lblPeriodid);
                        return false;
                    }
                    if (SearchPeriodDateKey(dateStartdate, dateEnddate, irow) < 0)
                    {
                        blcheck = true;

                    }
                    else
                    {
                        this.ShowMessage(MessageBoxButtons.OK, "OspCheckDuplStartEnd");
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

            for (int irow = 0; irow < grdClaimConfirm.View.DataRowCount; irow++)
            {
                if (icurRow != irow)
                {
                    DataRow row = grdClaimConfirm.View.GetDataRow(irow);

                    // 행 삭제만 제외 
                    if (grdClaimConfirm.View.IsDeletedRow(row) == false)
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
        /// 기간 중복 체크 함수
        /// </summary>
        /// <param name="dateStartdate"></param>
        /// <param name="dateEnddate"></param>
        /// <param name="icurRow"></param>
        /// <returns></returns>
        private int SearchPeriodDateKey(DateTime dateStartdate, DateTime dateEnddate, int icurRow)
        {
            int iresultRow = -1;

            for (int irow = 0; irow < grdClaimConfirm.View.DataRowCount; irow++)
            {
                if (icurRow != irow)
                {
                    DataRow row = grdClaimConfirm.View.GetDataRow(irow);

                    // 행 삭제만 제외 
                    if (grdClaimConfirm.View.IsDeletedRow(row) == false)
                    {
                        DateTime dateSearchStartdate = Convert.ToDateTime(row["STARTDATE"]);
                        DateTime dateSearchEnddate = Convert.ToDateTime(row["ENDDATE"]);
                        //시작일 비교
                        if (dateStartdate >= dateSearchStartdate && dateStartdate <= dateSearchEnddate)
                        {
                            return irow;
                        }
                        // 종료일 비교
                        if (dateEnddate >= dateSearchStartdate && dateEnddate <= dateSearchEnddate)
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
