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
    public partial class OspConCurRequestPopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        public DataRow CurrentDataRow { get; set; }
        string _strRequestno = "";
        #region Local Variables

        /// <summary>
        ///  Handler
        /// </summary>
        /// <param name="dt"></param>
        public delegate void chat_room_evecnt_handler(DataRow row);
        //public event chat_room_evecnt_handler write_handler;
        #endregion

        #region 생성자
        public OspConCurRequestPopup()
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
        public OspConCurRequestPopup(string Requestno ,string Functionid)
        {
            InitializeComponent();
           
            InitializeComboBox();  // 콤보박스 셋팅 
            InitializeEvent();
            InitializeGrid();
            InitializeCondition();
            _strRequestno = Requestno;
             txtRequestno.Text = _strRequestno;
            cboFunctionid.EditValue = Functionid;


            BtnSearch_Click(null, null);
        }
        /// <summary>        
        /// 코드그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {

            grdSearch.GridButtonItem = GridButtonItem.Export;
            grdSearch.View.SetIsReadOnly();
            grdSearch.View.AddTextBoxColumn("REQUESTNO", 120)
                 .SetLabel("CSMREQUESTNO") ;
           
            grdSearch.View.AddComboBoxColumn("FUNCTIONID", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ConcurrentFunctionId", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
              .SetIsReadOnly();
            grdSearch.View.AddDateEditColumn("REQUESTTIME", 150)
                .SetLabel("CSMREQUESTDATE")
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss");
            grdSearch.View.AddTextBoxColumn("PARAMETERVALUE", 250);
           
            grdSearch.View.AddComboBoxColumn("STATUS", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ConcurrentStatus", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
               .SetIsReadOnly();
            grdSearch.View.AddComboBoxColumn("RESULTTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ConcurrentResultType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
               .SetIsReadOnly()
               .SetLabel("OSPRESULTTYPE");
            grdSearch.View.AddTextBoxColumn("RESULTDESCRIPTION", 200)
                .SetLabel("OSPRESULTDESCRIPTION");
            grdSearch.View.AddTextBoxColumn("REQUESTUSER", 120).SetIsHidden(); 
            grdSearch.View.AddTextBoxColumn("REQUESTUSERNAME", 120)
                 .SetLabel("CSMREQUESTUSERNAME"); 

            grdSearch.View.AddDateEditColumn("FINISHTIME", 150)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss");
            
            grdSearch.View.PopulateColumns();



        }

        #region ComboBox  

        /// <summary>
        /// SmartComboBox 컨트롤 추가
        /// </summary>
        private void InitializeComboBox()
        {

            cboFunctionid.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboFunctionid.ValueMember = "CODEID";
            cboFunctionid.DisplayMember = "CODENAME";
            //cboFunctionid.EditValue = "OSPCloseBatch";
            cboFunctionid.DataSource = SqlExecuter.Query("GetCodeAllListByOsp", "10001"
             , new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "CODECLASSID", "ConcurrentFunctionId" } });
            cboFunctionid.ShowHeader = false;
          
            cboStatus.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboStatus.ValueMember = "CODEID";
            cboStatus.DisplayMember = "CODENAME";
            cboStatus.EditValue = "Running";
            cboStatus.DataSource = SqlExecuter.Query("GetCodeAllListByOsp", "10001"
             , new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "CODECLASSID", "ConcurrentStatus" } });
            cboStatus.ShowHeader = false;
            
        }

        #endregion
        #endregion

        #region 초기화
        /// <summary>
        /// 조회 조건 초기화
        /// </summary>
        private void InitializeCondition()
        {

            DateTime dateNow = DateTime.Now;
            dtpStartDate.EditValue = dateNow.ToString("yyyy-MM-dd");
            dtpEndDate.EditValue = dateNow.AddDays(1).ToString("yyyy-MM-dd");

            selectRequestuserPopup(UserInfo.Current.Plant.ToString());
        }

        /// <summary>
        /// 작업장 
        /// </summary>
        /// <param name="sPlantid"></param>
        private void selectRequestuserPopup(string sPlantid)
        {
            // 팝업 컬럼설정
            ConditionItemSelectPopup popup = new ConditionItemSelectPopup();
            popup.SetPopupLayoutForm(400, 700, FormBorderStyle.FixedDialog);
            popup.SetPopupLayout("USERNAME", PopupButtonStyles.Ok_Cancel, true, false);
            popup.Id = "USERNAME";
            popup.LabelText = "USERNAME";
            popup.SearchQuery = new SqlQuery("GetUseridListByOsp", "10001",  $"LANGUAGETYPE={UserInfo.Current.LanguageType}"
                                                                           , $"P_PLANTID   ={UserInfo.Current.Plant}");
            popup.IsMultiGrid = false;

            popup.DisplayFieldName = "USERNAME";
            popup.ValueFieldName = "USERID";
            popup.LanguageKey = "USERID";
            popup.SetPopupApplySelection((selectedRows, dataGridRows) =>
            {
                //작업장 변경 시 작업자 조회
                selectedRows.Cast<DataRow>().ForEach(row =>
                {
                    popupRequestuser.SetValue(row["USERID"].ToString());
                    popupRequestuser.Text = row["USERNAME"].ToString();
                    popupRequestuser.EditValue = row["USERNAME"].ToString();
                });

            });

            popup.Conditions.AddTextBox("USERNAME")
                .SetLabel("REQUESTUSER");
            popup.GridColumns.AddTextBoxColumn("USERID", 120)
                .SetLabel("USERID");
            popup.GridColumns.AddTextBoxColumn("USERNAME", 200)
                .SetLabel("USERNAME");
          
            popupRequestuser.SelectPopupCondition = popup;
        }
        #endregion

        #region  이벤트
        private void InitializeEvent()
        {  // 행 추가시 
          
         
            // 검색
            btnSearch.Click += BtnSearch_Click;
           
        }

      
        /// <summary>
        /// 조회 클릭 - 메인 grid에 체크 데이터 전달
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearch_Click(object sender, EventArgs e)
        {
           
            string strStartdate = "";
            string strEnddate = "";
            string strDateFormat = "";
            strDateFormat = "yyyy-MM-dd";
            if (dtpStartDate.Text.ToString().Equals(""))
            {
                this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblRequesttime.Text); //메세지 
                return;

            }
            if (dtpEndDate.Text.ToString().Equals(""))
            {
                this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblRequesttime.Text); //메세지 
                return;

            }
            if (!dtpStartDate.Text.Equals(""))
            {
                DateTime dtStartdat = Convert.ToDateTime(dtpStartDate.Text.ToString());
                strStartdate = dtStartdat.ToString(strDateFormat);
            }
            if (!dtpEndDate.Text.Equals(""))
            {

                DateTime dtEnddat = Convert.ToDateTime(dtpEndDate.Text.ToString());
                //strEnddate = dtEnddat.AddDays(1).ToString(strDateFormat);
                strEnddate = dtEnddat.ToString(strDateFormat);
            }
            Dictionary<string, object> Param = new Dictionary<string, object>();
       
            Param.Add("P_PLANTID", UserInfo.Current.Plant.ToString());
            Param.Add("P_STATUS", cboStatus.EditValue);
            Param.Add("P_FUNCTIONID", cboFunctionid.EditValue);
            Param.Add("P_REQUESTUSER", popupRequestuser.GetValue());
            Param.Add("P_REQUESTNO", txtRequestno.Text.ToString().Trim());
            Param.Add("P_STARTDATE", strStartdate);
            Param.Add("P_ENDDATE", strEnddate);
            Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            Param = Commons.CommonFunction.ConvertParameter(Param);
            DataTable dtClaimClose = SqlExecuter.Query("GetOspConCurRequestPopup", "10001", Param);
            grdSearch.DataSource = dtClaimClose;
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
        ///  포맷 변경 처리 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {


        }




#endregion

    }
}
