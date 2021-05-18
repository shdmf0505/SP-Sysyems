#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons.Controls;
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

namespace Micube.SmartMES.SystemManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 시스템관리 > 요청 관리 > 요청 정보
    /// 업  무  설  명  : 요청 정보에 메뉴 정보 버튼 클릭시 메뉴 정보를 조회하는 Popup 
    /// 생    성    자  : 한주석
    /// 생    성    일  : 2019-10-22
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class RequestInfoPopup : SmartConditionManualBaseForm, ISmartCustomPopup
    {
        #region Local Variables

        /// <summary>
        /// 팝업진입전 그리드의 Row
        /// </summary>
        public DataRow CurrentDataRow { get; set; }

        /// <summary>
        /// 그리드에 보여줄 데이터테이블
        /// </summary>
        private DataTable _defectDt;

        public event SelectPopupApplyEventHandler Selected;


        #endregion

        #region 생성자

        /// <summary>
        /// 생성자
        /// </summary>
        public RequestInfoPopup()
        {
            InitializeComponent();
            InitializeEvent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeDefectCodeList()
        {
            grdmenuinfo.GridButtonItem -= GridButtonItem.CRUD;
            grdmenuinfo.View.SetIsReadOnly();
            grdmenuinfo.View.SetAutoFillColumn("DEFECTQTY");
            grdmenuinfo.View.AddTextBoxColumn("MENUID", 150); // 메뉴 ID
            grdmenuinfo.View.AddTextBoxColumn("MENUNAME", 200); // 메뉴 명
            grdmenuinfo.View.AddTextBoxColumn("PARENTMENUID", 150); // 상위 메뉴 ID

            grdmenuinfo.View.PopulateColumns();
            grdmenuinfo.DataSource = _defectDt;

        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            this.Load += ChemicalissuePopup_Load;
            this.btn_close.Click += BtnClose_Click;
        }

        /// <summary>
        /// 닫기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
           
        }

        /// <summary>
        /// Form Road시 초기화
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChemicalissuePopup_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            SearchDefectCode();

            InitializeDefectCodeList();
        }

        #endregion

        #region Private Function

        /// <summary>
        /// 메인 그리드에서 Lot ID를 가져와서 해당 Lot의 불량코드 정보조회
        /// </summary>
        private void SearchDefectCode()
        {


            Dictionary<string, object> param = new Dictionary<string, object>
            {
                { "MENUID",CurrentDataRow["MENUID"].ToString() },
                { "LANGUAGETYPE", UserInfo.Current.LanguageType },
            };

            _defectDt = SqlExecuter.Query("GetMenuInfo", "10001", param);


        }

        #endregion
    }
}
