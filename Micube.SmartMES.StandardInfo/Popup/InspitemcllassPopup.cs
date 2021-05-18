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
using DevExpress.XtraGrid.Views.BandedGrid;

#endregion

namespace Micube.SmartMES.StandardInfo.Popup
{
    /// <summary>
    /// 프 로 그 램 명  : 기준정보 > Setup > 작업장 관리 > 설비 유형 팝업
    /// 업  무  설  명  : 설비 유형(EquipmentClass)을 선택
    /// 생    성    자  :  정승원
    /// 생    성    일  : 2019-05-22
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class InspitemcllassPopup : SmartPopupBaseForm, ISmartCustomPopup
	{
        public DataRow CurrentDataRow { get; set; }

        #region Local Variables
        string _CODECLASSID = "";
      

		//Resource Type = Equipment
		
        /// <summary>
        ///  선택한 list를 보내기 위한 Handler
        /// </summary>
        /// <param name="dt"></param>
        public delegate void chat_room_evecnt_handler(DataRow row);
        public event chat_room_evecnt_handler write_handler;
        #endregion

        #region 생성자
        public InspitemcllassPopup(string CODECLASSID)
        {
            _CODECLASSID = CODECLASSID;

            InitializeComponent();
            InitializeEvent();
            InitializeCondition();
            InitializeGridIdDefinitionManagement();
        }

        #endregion

        #region 초기화
        /// <summary>
        /// 조회 조건 초기화
        /// </summary>
        private void InitializeCondition()
        {

        }

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGridIdDefinitionManagement()
        {
            // GRID 초기화
            grdCode.GridButtonItem = GridButtonItem.None;   // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기
            grdCode.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;
          
            grdCode.View.AddTextBoxColumn("CODEID", 150).SetIsReadOnly();
            grdCode.View.AddTextBoxColumn("CODENAME", 100).SetIsReadOnly();

            grdCode.View.PopulateColumns();

            //RepositoryItemCheckEdit repositoryCheckEdit1 = grdProductItem.View.GridControl.RepositoryItems.Add("CheckEdit") as RepositoryItemCheckEdit;
            //repositoryCheckEdit1.ValueChecked = "True";
            //repositoryCheckEdit1.ValueUnchecked = "False";
            //repositoryCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            //grdProductItem.View.Columns["S"].ColumnEdit = repositoryCheckEdit1;

        }
        #endregion

        #region  이벤트
        private void InitializeEvent()
        {
            btnSearch.Click += BtnSearch_Click;
           
            btnCancel.Click += BtnCancel_Click;

            grdCode.View.DoubleClick += grdCode_DoubleClick;
        }

        private void grdCode_DoubleClick(object sender, EventArgs e)
        {
            CurrentDataRow = grdCode.View.GetFocusedDataRow();
            this.Close();
        }

       





        /// <summary>
        /// 조회 클릭 - 메인 grid에 체크 데이터 전달
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearch_Click(object sender, EventArgs e)
        {

            Dictionary<string, object> Param = new Dictionary<string, object>();
            Param.Add("CODECLASSID", _CODECLASSID);
            Param.Add("CODEID", txtCode.Text);
            Param.Add("CODENAME", txtName.Text);
            Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dt = SqlExecuter.Query("GetTypeList", "10001", Param);
            grdCode.DataSource = dt;
        }

       

        /// <summary>
        /// 취소 버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}
