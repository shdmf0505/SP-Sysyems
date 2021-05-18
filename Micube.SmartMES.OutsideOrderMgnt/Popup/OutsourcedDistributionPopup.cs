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
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Repository;
#endregion

namespace Micube.SmartMES.OutsideOrderMgnt.Popup
{
    /// <summary>
    /// 프 로 그 램 명  : 외주처 변경 배분 
    /// 업  무  설  명  : 외주처 변경처리함
    /// 생    성    자  : 최별
    /// 생    성    일  : 2019-08-08
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class OutsourcedDistributionPopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        public DataRow CurrentDataRow { get; set; }

        #region Local Variables
           
        private string _strTempPlantid = "";
        private string _strTempProcesssegmentid = "";
        private string _strLanguagetype = "";
        private string _sWorkTime = "";
       
        private Dictionary<string, object> _Param;// 배분적용하고 재조회시 사용 예정 임.
        /// <summary>
        ///  Handler
        /// </summary>
        /// <param name="dt"></param>
        public delegate void chat_room_evecnt_handler(DataRow row);
        //public event chat_room_evecnt_handler write_handler;
        #endregion

        #region 생성자
        public OutsourcedDistributionPopup()
        {
            InitializeComponent();
            // 콤보박스 셋팅 
            InitializeComboBox(); 

            InitializeEvent();

            InitializeCondition();
            InitializeGrid();


        }
        public OutsourcedDistributionPopup(Dictionary<string, object> DicParam)
        {
            InitializeComponent();
            // 콤보박스 셋팅 
            InitializeComboBox();

            InitializeEvent();

            InitializeCondition();
            //조회를 다시 해야함...

            _Param = DicParam;
            _strTempPlantid = _Param["P_PLANTID"].ToString();

            _strTempProcesssegmentid = _Param["P_PROCESSSEGMENTID"].ToString();
            _strLanguagetype = _Param["LANGUAGETYPE"].ToString();
            
            //plant worktime 가져오기 
            OnPlantidInformationSearch();
            //공정별 areaid 정보 가져오기 
            ///OnProcesssegmentidAreaInformationSearch();

            InitializeGrid();
            #region 품목코드 전환 처리 
            string sproductcode = "";
            if (!(_Param["P_PRODUCTCODE"] == null))
            {
                sproductcode = _Param["P_PRODUCTCODE"].ToString();
            }
          
            // 품목코드값이 있으면
            if (!(sproductcode.Equals("")))
            {
                string[] sproductd = sproductcode.Split('|');
                // plant 정보 다시 가져오기 
                _Param.Add("P_PRODUCTDEFID", sproductd[0].ToString());
                _Param.Add("P_PRODUCTDEFVERSION", sproductd[1].ToString());
            }
            #endregion

            #region 기간 검색형 전환 처리 

            if (!(_Param["P_RECEIPTDATE_PERIODFR"].ToString().Equals("")))
            {
                DateTime requestDateFr = Convert.ToDateTime(_Param["P_RECEIPTDATE_PERIODFR"]);
                _Param.Remove("P_RECEIPTDATE_PERIODFR");
                _Param.Add("P_RECEIPTDATE_PERIODFR", string.Format("{0:yyyy-MM-dd}", requestDateFr));
            }
            if (!(_Param["P_RECEIPTDATE_PERIODTO"].ToString().Equals("")))
            {
                DateTime requestDateTo = Convert.ToDateTime(_Param["P_RECEIPTDATE_PERIODTO"]);
                _Param.Remove("P_RECEIPTDATE_PERIODTO");
                _Param.Add("P_RECEIPTDATE_PERIODTO", string.Format("{0:yyyy-MM-dd}", requestDateTo));
            }


            #endregion
            BtnSearch_Click(null, null);
            //lot no 그리드 조회
            OnGridSearch();
          
        }
      
        /// <summary>
        /// 화면 그리드 셋팅 처리 
        /// </summary>
        private void InitializeGrid()
        {
            //공정현황
            InitializeGridProcessStatus();
          
            //LOT 목록 
            InitializeGrid_LotList();

            InitializeGrid_SegmentList();
        }
        /// <summary>        
        /// 이행율 초기화한다.
        /// </summary>
        private void InitializeGridProcessStatus()
        {
            grdProcessStatus.GridButtonItem = GridButtonItem.Export;

            grdProcessStatus.View.SetIsReadOnly();
            grdProcessStatus.View.AddComboBoxColumn("OWNTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OwnType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdProcessStatus.View.AddTextBoxColumn("VENDORNAME", 120); // 거래처명 2021-04-28 오근영 수정 areaname -> vendorname
            grdProcessStatus.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150);   //  공정명
            grdProcessStatus.View.AddTextBoxColumn("ALLOCATERATE", 120)
               .SetTextAlignment(TextAlignment.Right)
               .SetDisplayFormat("##0.##", MaskTypes.Numeric);                                // 배분율
            grdProcessStatus.View.AddTextBoxColumn("SENDPANELQTYMONRATE", 120)
               .SetTextAlignment(TextAlignment.Right)
               .SetDisplayFormat("##0.##", MaskTypes.Numeric);                                //
            grdProcessStatus.View.AddTextBoxColumn("SENDPANELQTYDAYRATE", 120)
               .SetTextAlignment(TextAlignment.Right)
               .SetDisplayFormat("##0.##", MaskTypes.Numeric);                                //
            grdProcessStatus.View.AddTextBoxColumn("PROPANELQTY", 120)
               .SetIsReadOnly()
               .SetTextAlignment(TextAlignment.Right)
               .SetDisplayFormat("#,##0", MaskTypes.Numeric);                                //  pcsqty
            grdProcessStatus.View.AddTextBoxColumn("SENDPANELQTYDAY", 120)
               .SetIsReadOnly()
               .SetTextAlignment(TextAlignment.Right)
               .SetDisplayFormat("#,##0", MaskTypes.Numeric);                                //  pcsqty
            grdProcessStatus.View.AddTextBoxColumn("WAITFORRECEIVEPNLQTY", 120)
               .SetIsReadOnly()
               .SetTextAlignment(TextAlignment.Right)
               .SetDisplayFormat("#,##0", MaskTypes.Numeric);                                //  pcsqty
            grdProcessStatus.View.AddTextBoxColumn("WAITPNLQTY", 120)
               .SetIsReadOnly()
               .SetTextAlignment(TextAlignment.Right)
               .SetDisplayFormat("#,##0", MaskTypes.Numeric);                                //  pcsqty
            grdProcessStatus.View.AddTextBoxColumn("RUNPNLQTY", 120)
               .SetIsReadOnly()
               .SetTextAlignment(TextAlignment.Right)
               .SetDisplayFormat("#,##0", MaskTypes.Numeric);                                //  pcsqty
            grdProcessStatus.View.AddTextBoxColumn("WAITFORSENDPNLQTY", 120)
               .SetIsReadOnly()
               .SetTextAlignment(TextAlignment.Right)
               .SetDisplayFormat("#,##0", MaskTypes.Numeric);                                //  pcsqty

            grdProcessStatus.View.PopulateColumns();
        }
        /// <summary>        
        ///  //LOT 목록  를 초기화한다.
        /// </summary>
        private void InitializeGrid_LotList()
        {
            // TODO : 그리드 초기화 로직 추가
            //ARAR
            grdLotList.GridButtonItem = GridButtonItem.Export;

           
            grdLotList.View.AddTextBoxColumn("ENTERPRISEID", 120)
               .SetIsHidden();                                                               //  회사 ID
            grdLotList.View.AddTextBoxColumn("PLANTID", 120)
               .SetIsHidden();                                                               //  공장 ID
            grdLotList.View.AddTextBoxColumn("OSPRECEIPTUSER", 120)
               .SetIsHidden();
            grdLotList.View.AddTextBoxColumn("OSPSENDER", 120)
               .SetIsHidden();
            grdLotList.View.AddTextBoxColumn("LOTHISTKEY", 120)
               .SetIsHidden();                                                               //  LOTHISTKEY
            grdLotList.View.AddTextBoxColumn("RECEIPTSEQUENCE", 120)
               .SetIsHidden();
            grdLotList.View.AddTextBoxColumn("LOTID", 200)
               .SetIsReadOnly();                                                             //  LOTID
            grdLotList.View.AddTextBoxColumn("RECEIPTDATE", 80)
               .SetDisplayFormat("yyyy-MM-dd")
               .SetIsReadOnly();                                                              // 입고일 
            grdLotList.View.AddTextBoxColumn("RECEIPTUSERNAME", 120)
               .SetIsReadOnly();                                                             // 입고자
            grdLotList.View.AddTextBoxColumn("PROCESSDEFID", 150)
               .SetIsHidden();                                                             //  제품 정의 ID
            grdLotList.View.AddTextBoxColumn("PROCESSDEFVERSION", 120)
               .SetIsHidden();                                                             //  제품 정의 Version
            grdLotList.View.AddComboBoxColumn("LOTTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=LotType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
               .SetIsReadOnly();                                                             //  양산구분               
            grdLotList.View.AddTextBoxColumn("PRODUCTDEFID", 150)
               .SetIsReadOnly();                                                             //  제품 정의 ID
            grdLotList.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80)
               .SetIsReadOnly();                                                             //  제품 정의 Version
            grdLotList.View.AddTextBoxColumn("PRODUCTDEFNAME", 200)
               .SetIsReadOnly();                                                             //  제품명
            grdLotList.View.AddTextBoxColumn("PROCESSSEGMENTID", 120)
               .SetIsHidden();                                                             //  공정 ID
            grdLotList.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150)
               .SetIsReadOnly();                                                             //  공정명
            //콤보박스 대체 처리 
            grdLotList.View.AddTextBoxColumn("AREAID", 120) 
               .SetIsHidden();

            grdLotList.View.AddTextBoxColumn("AREANAME", 120)
               .SetIsReadOnly();                                                               //  작업장 AREAID

            //콤보박스 대체 처리 
            grdLotList.View.AddTextBoxColumn("VENDORID", 120)                                  //  협력사ID VENDORID
               .SetIsHidden();

            grdLotList.View.AddTextBoxColumn("VENDORNAME", 120)                                //  협력사명 VENDORNAME
               .SetIsReadOnly();

            grdLotList.View.AddTextBoxColumn("USERSEQUENCENAME", 120)
               .SetIsHidden()
               .SetIsReadOnly();
            grdLotList.View.AddTextBoxColumn("ALTERNATESEQUENCENAME", 120)
              .SetIsHidden()
              .SetIsReadOnly(); // 공순
            grdLotList.View.AddTextBoxColumn("PREVPROCESSSEGMENTID", 120)
               .SetIsHidden();                                                              //  이전공정 ID
            grdLotList.View.AddTextBoxColumn("PREVPROCESSSEGMENTNAME", 150)
               .SetIsReadOnly();                                                             //  이전공정명
            grdLotList.View.AddTextBoxColumn("PREVAREAID", 120)
               .SetIsHidden();                                                             //  이전 작업장 PREVAREAID
            grdLotList.View.AddTextBoxColumn("PREVAREANAME", 120)
               .SetIsReadOnly();                                                               //  이전 작업장 PREVAREAID

            grdLotList.View.AddTextBoxColumn("PCSQTY", 120)
               .SetIsReadOnly()
               .SetTextAlignment(TextAlignment.Right)
               .SetDisplayFormat("#,##0", MaskTypes.Numeric);                                //  pcsqty
            
            grdLotList.View.AddTextBoxColumn("PANELQTY", 120)
               .SetIsReadOnly()
               .SetTextAlignment(TextAlignment.Right)
               .SetDisplayFormat("#,##0", MaskTypes.Numeric);                                //  panelqty
            grdLotList.View.AddTextBoxColumn("OSPMM", 120)
               .SetIsHidden()
               .SetTextAlignment(TextAlignment.Right)
               .SetDisplayFormat("#,##0.##", MaskTypes.Numeric);                                //  panelqty
            grdLotList.View.AddTextBoxColumn("CHECKDATE", 80)
               .SetDisplayFormat("yyyy-MM-dd")
               .SetIsReadOnly();                                                                  // 확인일
            grdLotList.View.AddTextBoxColumn("CHECKUSERNAME", 120)
               .SetIsReadOnly();                                                             // 확인자
            grdLotList.View.AddTextBoxColumn("CHECKUSER", 120)
               .SetIsHidden();                                                               //  LOTHISTKEY
            grdLotList.View.AddTextBoxColumn("PATHSEQUENCESTART", 120)
               .SetIsHidden();                                                              //  이전공정 ID
            grdLotList.View.AddTextBoxColumn("PATHSEQUENCEEND", 120)
               .SetIsHidden();                                                              //  이전공정 ID
            grdLotList.View.PopulateColumns();

        }
        /// <summary>        
        ///  //LOT 목록  를 초기화한다.
        /// </summary>
        private void InitializeGrid_SegmentList()
        {
            // TODO : 그리드 초기화 로직 추가
            //ARAR
            grdSegmentList.GridButtonItem = GridButtonItem.Export;


            grdSegmentList.View.AddTextBoxColumn("ENTERPRISEID", 120)
                .SetIsHidden();                                                               //  회사 ID
            grdSegmentList.View.AddTextBoxColumn("PLANTID", 120)
                .SetIsHidden();                                                               //  공장 ID
            grdSegmentList.View.AddTextBoxColumn("LOTHISTKEY", 120)
                .SetIsHidden();
            grdSegmentList.View.AddTextBoxColumn("PRODUCTDEFID", 120)
                .SetIsHidden();
            grdSegmentList.View.AddTextBoxColumn("PRODUCTDEFVERSION", 120)
                .SetIsHidden();
            grdSegmentList.View.AddTextBoxColumn("PROCESSDEFID", 120)
                .SetIsHidden();
            grdSegmentList.View.AddTextBoxColumn("PROCESSDEFVERSION", 120)
                .SetIsHidden();
            grdSegmentList.View.AddTextBoxColumn("PROCESSSEGMENTID", 120)
                .SetIsHidden();
            grdSegmentList.View.AddTextBoxColumn("PROCESSSEGMENTVERSION", 120)
                .SetIsHidden();
            grdSegmentList.View.AddTextBoxColumn("PATHSEQUENCE", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsHidden(); 
            grdSegmentList.View.AddTextBoxColumn("WORKCOUNT", 120)
                .SetIsHidden();
            grdSegmentList.View.AddTextBoxColumn("USERSEQUENCE", 80)
                .SetIsReadOnly();
            grdSegmentList.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 200)
                .SetIsReadOnly();
            grdSegmentList.View.AddTextBoxColumn("LOTID", 200)
                .SetIsReadOnly();                                                             //  LOTID
            //콤보박스 대체 처리 
            grdSegmentList.View.AddTextBoxColumn("AREAID", 120)
                .SetIsHidden();
           
            grdSegmentList.View.AddTextBoxColumn("PREVAREAID", 120)
                .SetIsHidden();
            grdSegmentList.View.AddTextBoxColumn("PREVAREANAME", 120)
                .SetIsHidden();
            grdSegmentList.View.AddTextBoxColumn("PREVRESOURCEID", 120)
                .SetIsHidden();
            grdSegmentList.View.AddTextBoxColumn("PREVRESOURCENAME", 120)
                .SetIsHidden();
            //팝업 추가 처리 예정...
            InitializeGrid_ResourceidPopup();

            grdSegmentList.View.AddTextBoxColumn("RESOURCENAME", 220)
                .SetIsReadOnly();
            grdSegmentList.View.AddTextBoxColumn("CHECKDATE", 80)
                .SetDisplayFormat("yyyy-MM-dd")
                .SetIsReadOnly();                                                                  // 확인일
            grdSegmentList.View.AddTextBoxColumn("CHECKUSERNAME", 120)
                .SetIsReadOnly();                                                             // 확인자
            grdSegmentList.View.AddTextBoxColumn("CHECKUSER", 120)
                .SetIsHidden();                                                               //  LOTHISTKEY
            grdSegmentList.View.PopulateColumns();

        }

        /// <summary>
        /// grid 자재 정보 가져오기 
        /// </summary>
        private void InitializeGrid_ResourceidPopup()
        {
             var popupGridResourceid = grdSegmentList.View.AddSelectPopupColumn("RESOURCEID", 120,new OutsourcedResourceidpopup())
                .SetLabel("RESOURCEID")
                .SetPopupResultCount(1)
                .SetPopupCustomParameter
                (
                    (sender, currentRow) =>
                    {
                        (sender as OutsourcedResourceidpopup).CurrentDataRow = grdSegmentList.View.GetFocusedDataRow();
                        
                        (sender as OutsourcedResourceidpopup).ResultDataEvent += (dr) =>
                        {
                            grdSegmentList.View.SetFocusedRowCellValue("RESOURCEID", dr["RESOURCEID"]);
                            grdSegmentList.View.SetFocusedRowCellValue("RESOURCENAME", dr["RESOURCENAME"]);
                            grdSegmentList.View.SetFocusedRowCellValue("AREAID", dr["AREAID"]);
                            
                        };
                        
                    }
                );
            


        }
        #region ComboBox  

        /// <summary>
        /// SmartComboBox 컨트롤 추가
        /// </summary>
        private void InitializeComboBox()
        {


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
        {
           
            btnSave.Click += BtnSave_Click;

            btnSaveOK.Click += BtnSaveOK_Click;

            btnSearch.Click += BtnSearch_Click;
            // 닫기
            btnClose.Click += BtnClose_Click;

            grdLotList.View.FocusedRowChanged += View_FocusedRowChanged;

        }

        /// <summary>
        /// 그리드의 포커스 행 변경 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            focusedRowChanged();
        }

        /// <summary>
        /// 그리드의 포커스 행 변경시 행의 작업목록의 내용을 매핑처리 함.
        /// </summary>
        private void focusedRowChanged()
        {
            //포커스 행 체크 
           
            //포커스 행 체크 
            if (grdLotList.View.FocusedRowHandle < 0)
            {
                grdSegmentList.View.ClearDatas();

                return;
            }
            Dictionary<string, object> Param = new Dictionary<string, object>();
            Param.Add("P_PLANTID", grdLotList.View.GetFocusedRowCellValue("PLANTID"));
            Param.Add("P_LOTHISTKEY", grdLotList.View.GetFocusedRowCellValue("LOTHISTKEY"));
            Param.Add("P_LOTID", grdLotList.View.GetFocusedRowCellValue("LOTID"));
            Param.Add("P_RECEIPTSEQUENCE", grdLotList.View.GetFocusedRowCellValue("RECEIPTSEQUENCE"));
            Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            Param = Commons.CommonFunction.ConvertParameter(Param);
            DataTable dtSegmentList = SqlExecuter.Query("GetOutsourcedDistributionLotSegmentid", "10001", Param);
            grdSegmentList.DataSource = dtSegmentList;
        }


        /// <summary>
        /// 확인 클릭시
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSaveOK_Click(object sender, EventArgs e)
        {
            //작업장 id null check 
            string strLotid = ""; // 추후 의뢰번호 Search용

            strLotid = grdLotList.View.GetFocusedRowCellValue("LOTID").ToString();
            DialogResult result = System.Windows.Forms.DialogResult.No;

            result = this.ShowMessage(MessageBoxButtons.YesNo, "OspDoProcessWant", btnSaveOK.Text);//저장하시겠습니까? 

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    this.ShowWaitArea();
                    btnSearch.Enabled = false;
                    btnSave.Enabled = false;
                    btnSaveOK.Enabled = false;
                    btnClose.Enabled = false;
                    // TODO : 저장 Rule 변경
                    DataTable dtsave = grdLotList.DataSource as DataTable;
                   
                    DataRow dr = null;
                   
                    for (int i = 0; i < dtsave.Rows.Count; i++)
                    {
                        dr = dtsave.Rows[i];
                        dr["CHECKUSER"] = UserInfo.Current.Id.ToString();

                    }
                    ExecuteRule("OutsourcedDistributionCheckSave", dtsave);

                    ShowMessage("SuccessOspProcess");

                    //재조회 처리 
                    OnGridSearch();
                    int irow = GetGridRowSearch(strLotid);
                    if (irow >= 0)
                    {
                        grdLotList.View.FocusedRowHandle = irow;
                        grdLotList.View.SelectRow(irow);
                        focusedRowChanged();
                    }
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
                    btnSaveOK.Enabled = true;

                }
            }

        }
        /// <summary>
        /// 저장 클릭시
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            //작업장 id null check 
            string strLotid = ""; // 추후 의뢰번호 Search용
            for (int i = 0; i < grdSegmentList.View.DataRowCount; i++)
            {
                
                string strConsumabledefid = grdSegmentList.View.GetRowCellValue(i, "RESOURCEID").ToString();
                if (strConsumabledefid.Equals(""))
                {
                    string lblAreaid = grdSegmentList.View.Columns["RESOURCEID"].Caption.ToString();
                    this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblAreaid); //메세지
                    return;
                }
                strLotid = grdSegmentList.View.GetRowCellValue(i, "LOTID").ToString();


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
                    btnSaveOK.Enabled = false;
                    // TODO : 저장 Rule 변경
                    DataTable changed = grdSegmentList.DataSource as DataTable;
                    DataView dvMapping = changed.DefaultView;
                    dvMapping.Sort = "PATHSEQUENCE ASC";
                    DataRow dr = null;
                    DataTable dtsave = dvMapping.ToTable();
                    for (int i = 0; i < dtsave.Rows.Count;i++)
                    {
                         dr = dtsave.Rows[i];
                         dr["CHECKUSER"] = UserInfo.Current.Id.ToString();
                       
                    }
                    ExecuteRule("OutsourcedDistribution", dtsave);

                    ShowMessage("SuccessOspProcess");
                   
                    //재조회 처리 
                    OnGridSearch();
                    int irow = GetGridRowSearch(strLotid);
                    if (irow >= 0)
                    {
                        grdLotList.View.FocusedRowHandle = irow;
                        grdLotList.View.SelectRow(irow);
                        focusedRowChanged();
                    }
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
                    btnSaveOK.Enabled = true;

                }
            }

        }

        /// <summary>
        /// 그리드 이동에 필요한 row 찾기
        /// </summary>
        /// <param name="strRequestno"></param>
        private int GetGridRowSearch(string strLotid)
        {
            int iRow = -1;
            if (grdLotList.View.DataRowCount == 0)
            {
                return iRow;
            }
            for (int i = 0; i < grdLotList.View.DataRowCount; i++)
            {
                if (grdLotList.View.GetRowCellValue(i, "LOTID").ToString().Equals(strLotid))
                {
                    return i;
                }
            }
            return iRow;
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
        /// 조회 클릭 - 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            
            DateTime datecurNow = DateTime.Now;

            string strtempdate  = datecurNow.ToString("yyyy-MM-dd ") + _sWorkTime;

            DateTime dttempdate = Convert.ToDateTime(strtempdate);
            if (datecurNow < dttempdate)
            {
                datecurNow = datecurNow.AddDays(-1);
                //기준일자가 현재일 -1
            }
            string strStartdate = datecurNow.ToString("yyyy-MM-dd ") + _sWorkTime;
            string strYearMonth = datecurNow.ToString("yyyy-MM");
            DateTime dtenddate = datecurNow;
            //DateTime dtenddate = datecurNow.AddDays(1);
            string strEnddate = dtenddate.ToString("yyyy-MM-dd ") + _sWorkTime;

            string strDateMonthFormat = "";
            strDateMonthFormat = "yyyy-MM-01 " + _sWorkTime;

            string strStartMonth = datecurNow.ToString("yyyy-MM-01 ") + _sWorkTime;
            string strEndMonth = datecurNow.AddMonths(1).ToString("yyyy-MM-01 ") + _sWorkTime;
         
            Dictionary<string, object> Param = new Dictionary<string, object>();
            Param.Add("P_PLANTID", _strTempPlantid);
            Param.Add("P_PROCESSSEGMENTID", _strTempProcesssegmentid);
            Param.Add("P_STARTDATE", strStartdate);
            Param.Add("P_ENDDATE", strEnddate);
            Param.Add("P_STARTMONTH", strStartMonth);
            Param.Add("P_ENDMONTH", strEndMonth);
            Param.Add("P_YEARMONTH", strYearMonth);
            Param.Add("LANGUAGETYPE", _strLanguagetype);
            Param = Commons.CommonFunction.ConvertParameter(Param);
            DataTable dtpop = SqlExecuter.Query("GetOutsourcedDistributionPopupByProcess", "10001", Param);
            //이행율 계산처리 
            if (dtpop.Rows.Count>0)
            {
                string strSendpanelqtymonsum = "";
                string strSendpanelqtydaysum = "";
                string strSendpanelqtymon = "";
                string strSendpanelqtyday = "";
                for (int i=0; i <dtpop.Rows.Count;i++)
                {
                    DataRow dr = dtpop.Rows[i];
                    strSendpanelqtymonsum = dr["SENDPANELQTYMONSUM"].ToString();
                    strSendpanelqtydaysum = dr["SENDPANELQTYDAYSUM"].ToString(); 
                    strSendpanelqtymon = dr["SENDPANELQTYMON"].ToString(); 
                    strSendpanelqtyday = dr["SENDPANELQTYDAY"].ToString();
                    double dblSendpanelqtymonsum = Convert.ToDouble(strSendpanelqtymonsum);
                    double dblSendpanelqtydaysum = Convert.ToDouble(strSendpanelqtydaysum);
                    double dblSendpanelqtymon = Convert.ToDouble(strSendpanelqtymon);
                    double dblSendpanelqtyday = Convert.ToDouble(strSendpanelqtyday);
                    if (dblSendpanelqtymonsum.Equals(0) || dblSendpanelqtymon.Equals(0))
                    {
                        dr["SENDPANELQTYMONRATE"] = 0;
                    }
                    else
                    {
                        double dblSendpanelqtymonRate = Math.Round(dblSendpanelqtymon / dblSendpanelqtymonsum * 100, 2);
                        dr["SENDPANELQTYMONRATE"] = dblSendpanelqtymonRate;
                    }
                    if (dblSendpanelqtydaysum.Equals(0) || dblSendpanelqtyday.Equals(0))
                    {
                        dr["SENDPANELQTYDAYRATE"] = 0;
                    }
                    else
                    {
                        double dblSendpanelqtydayRate = Math.Round(dblSendpanelqtyday / dblSendpanelqtydaysum * 100, 2);
                        dr["SENDPANELQTYDAYRATE"] = dblSendpanelqtydayRate;
                    }
                }
            }
            grdProcessStatus.DataSource = dtpop;
            
        }

        /// <summary>
        /// plant정보에서 WOKRTIME 
        /// </summary>
        private void OnPlantidInformationSearch()
        {

            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add("P_PLANTID", _strTempPlantid);
            dicParam = Commons.CommonFunction.ConvertParameter(dicParam);
            DataTable dtPlantInfo = SqlExecuter.Query("GetPlantidInformatByCsm", "10001", dicParam);

            if (dtPlantInfo.Rows.Count == 1)
            {
                DataRow drPlant = dtPlantInfo.Rows[0];
                _sWorkTime = drPlant["WORKTIME"].ToString();
            }
            else
            {
                return;
            }

        }
        
        /// <summary>
        /// //lot no 그리드 조회
        /// </summary>
        private void OnGridSearch()
        {
            _Param = Commons.CommonFunction.ConvertParameter(_Param); 
            DataTable dt = SqlExecuter.Query("GetOutsourcedDistribution", "10001", _Param);
            grdLotList.DataSource = dt;


        }
        #endregion


    }
}
