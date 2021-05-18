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
#endregion

namespace Micube.SmartMES.ToolManagement.Popup
{
    /// <summary>
    /// 프 로 그 램 명  : 필름 제품코드 팝업
    /// 업  무  설  명  : ucFilmCodePopup사용자 컨트롤에서 호출하여 Film Code등의 정보를 조회할 때 사용
    /// 생    성    자  : 김용조
    /// 생    성    일  : 2019-08-19
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class FilmCodePopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region Local Variables
        public DataRow CurrentDataRow { get; set; }
        public delegate void LoadDataEventHandler(DataRow row);
        public event LoadDataEventHandler loadDataHandler;

        string _plantID;
        #endregion

        #region Properties
        /// <summary>
        /// 품목의 코드를 반환 및 설정한다.
        /// </summary>
        public string FilmCode
        {
            get { return txtFilmCode.Text; }
            set { txtFilmCode.Text = value; }
        }
        /// <summary>
        /// 외부로부터 받은 데이터테이블을 바인딩한다.
        /// </summary>
        public DataTable SearchResult
        {
            set
            {
                grdFilmCodeList.DataSource = value;
            }
        }
        #endregion

        public FilmCodePopup(string plantID)
        {
            InitializeComponent();

            InitializeComboBox();

            InitializeCondition();

            InitializeEvent();

            _plantID = plantID;
        }

        #region 초기화
        /// <summary>
        /// 조회 조건 초기화
        /// </summary>
        private void InitializeCondition()
        {
            //그리드 초기화
            InitializeGridIdDefinitionManagement();
        }
        #endregion

        #region ComboBox  
        /// <summary>
        /// SmartComboBox 컨트롤 추가
        /// </summary>
        private void InitializeComboBox()
        {
            // 작업구분값 정의 
            cboProcessSegment.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboProcessSegment.ValueMember = "PROCESSSEGEMENTID";
            cboProcessSegment.DisplayMember = "PROCESSSEGEMENTNAME";
            cboProcessSegment.UseEmptyItem = true;
            cboProcessSegment.EmptyItemCaption = "";
            cboProcessSegment.EmptyItemValue = "";
            DataTable dtProgcessSegment = SqlExecuter.Query("GetProcessSegmentByTool", "10001"
             , new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "ENTERPRISEID", UserInfo.Current.Enterprise }, { "PLANTID", _plantID } });

            cboProcessSegment.DataSource = dtProgcessSegment;

            cboProcessSegment.ShowHeader = false;
        }
        #endregion

        #region  이벤트
        private void InitializeEvent()
        {
            btnSearch.Click += BtnSearch_Click;
            btnConfirm.Click += BtnConfirm_Click;
            btnCancel.Click += BtnCancel_Click;
            grdFilmCodeList.View.DoubleClick += grdFilmCodeList_DoubleClick;

            txtProductCode.KeyPress += TxtProductCode_KeyPress;
            txtProductDefName.KeyPress += TxtProductDefName_KeyPress;
        }

        #region TxtProductDefName_KeyPress - 품목명 키입력 이벤트
        private void TxtProductDefName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                Search();
        }
        #endregion

        #region TxtProductCode_KeyPress - 품목코드 키입력 이벤트
        private void TxtProductCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            //엔터키 입력시 동작
            if (e.KeyChar == 13)
                Search();
        }
        #endregion

        #region BtnConfirm_Click - 확인버튼 이벤트
        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            DataRow row = grdFilmCodeList.View.GetFocusedDataRow();
            CurrentDataRow = row;
            loadDataHandler(row);
            this.Close();
        }
        #endregion

        #region grdFilmCodeList_DoubleClick - 그리드 더블클릭이벤트
        private void grdFilmCodeList_DoubleClick(object sender, EventArgs e)
        {
            DataRow row = grdFilmCodeList.View.GetFocusedDataRow();
            CurrentDataRow = row;
            loadDataHandler(row);
            this.Close();
        }
        #endregion

        #region BtnSearch_Click - 검색버튼 이벤트
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }
        #endregion

        #region BtnCancel_Click - 취소버튼이벤트
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
        #endregion

        #region 컨트롤 초기화
        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGridIdDefinitionManagement()
        {
            // GRID 초기화
            grdFilmCodeList.GridButtonItem = GridButtonItem.None;   // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기

            grdFilmCodeList.View.AddTextBoxColumn("FILMCODE", 150).SetIsReadOnly();
            grdFilmCodeList.View.AddTextBoxColumn("FILMVERSION", 80).SetIsReadOnly();
            grdFilmCodeList.View.AddTextBoxColumn("PRODUCTDEFID", 150).SetIsReadOnly();
            grdFilmCodeList.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetIsHidden();
            grdFilmCodeList.View.AddTextBoxColumn("PRODUCTDEFNAME", 280).SetIsReadOnly();
            grdFilmCodeList.View.AddTextBoxColumn("FILMCATEGORYID").SetIsHidden();
            grdFilmCodeList.View.AddTextBoxColumn("FILMCATEGORY", 100)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();
            grdFilmCodeList.View.AddTextBoxColumn("FILMDETAILCATEGORYID").SetIsHidden();
            grdFilmCodeList.View.AddTextBoxColumn("FILMDETAILCATEGORY", 100)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();
            grdFilmCodeList.View.AddTextBoxColumn("PROCESSSEGMENTVERSION", 100).SetIsReadOnly();
            grdFilmCodeList.View.AddTextBoxColumn("FILMUSELAYER", 100).SetIsHidden();
            grdFilmCodeList.View.AddTextBoxColumn("JOBTYPEID", 100).SetIsHidden();
            grdFilmCodeList.View.AddTextBoxColumn("JOBTYPE", 100)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsHidden();
            grdFilmCodeList.View.AddTextBoxColumn("CUSTOMERID", 100).SetIsHidden();
            grdFilmCodeList.View.AddTextBoxColumn("CUSTOMERNAME", 100).SetIsHidden();
            grdFilmCodeList.View.PopulateColumns();
        }
        #endregion

        #region 검색
        /// <summary>
        /// 검색을 수행한다. 각 컨트롤에 입력된 값을 파라미터로 받아들인다.
        /// </summary>
        /// <param name="durableDefName"></param>
        /// <param name="durableDefID"></param>
        /// <param name="durableDefVersion"></param>
        void Search()
        {
            Dictionary<string, object> Param = new Dictionary<string, object>();
            Param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            Param.Add("PLANTID", _plantID);
            Param.Add("PRODUCTDEFID", txtProductCode.EditValue);
            Param.Add("PRODUCTDEFNAME", txtProductDefName.EditValue);
            Param.Add("PROCESSSEGMENTID", cboProcessSegment.EditValue);
            Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            Param.Add("CURRENTLOGINID", UserInfo.Current.Id);

            Param = Commons.CommonFunction.ConvertParameter(Param);
            DataTable dt = SqlExecuter.Query("GetFilmCodePopupListByTool", "10001", Param);

            grdFilmCodeList.DataSource = dt;
        }
        #endregion
    }
}
