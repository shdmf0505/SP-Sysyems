#region using

using DevExpress.XtraGrid.Views.Grid;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
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

namespace Micube.SmartMES.OutsideOrderMgnt.Popup
{
    /// <summary>
    /// 프 로 그 램 명  : 외주처 변경 배분 
    /// 업  무  설  명  : 자원정보 가져오기 
    /// 생    성    자  : 최별
    /// 생    성    일  : 2019-08-08
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class OutsourcedResourceidpopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region Local Variables

        /// <summary>
        /// 선택한 Row를 보내기 위한 Handler
        /// </summary>
        /// <param name="row"></param>
        public delegate void ResultDataHandler(DataRow row);
        public event ResultDataHandler ResultDataEvent;

        /// <summary>
        /// 팝업진입전 그리드의 Row
        /// </summary>
        public DataRow CurrentDataRow { get; set; }
        private string _strProductdefid = "";
        private string _strProductdefversion = "";
        private string _strProcessdefid = "";
        private string _strProcessdefversion = "";
        private string _strProcesssegmentid = "";
        private string _strProcesssegmentversion = "";
        private string _strPlantid = "";
        #endregion

        #region 생성자

        /// <summary>
        /// 생성자
        /// </summary>
        public OutsourcedResourceidpopup()
        {
            InitializeComponent();

            InitializeEvent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// TextBox에 부모 그리드의 데이터 바인딩
        /// </summary>
        private void InitializeCurrentData()
        {
           
            _strProductdefid  = CurrentDataRow["PRODUCTDEFID"].ToString();
            _strProductdefversion = CurrentDataRow["PRODUCTDEFVERSION"].ToString();
            _strProcessdefid = CurrentDataRow["PROCESSDEFID"].ToString();
            _strProcessdefversion = CurrentDataRow["PROCESSDEFVERSION"].ToString();
            _strProcesssegmentid = CurrentDataRow["PROCESSSEGMENTID"].ToString();
            _strProcesssegmentversion = CurrentDataRow["PROCESSSEGMENTVERSION"].ToString();
            _strPlantid      = CurrentDataRow["PLANTID"].ToString();
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("P_PRODUCTDEFID", _strProductdefid);
            param.Add("P_PRODUCTDEFVERSION", _strProductdefversion);
            param.Add("P_PROCESSDEFID", _strProcessdefid);
            param.Add("P_PROCESSDEFVERSION", _strProcessdefversion);
            param.Add("P_PROCESSSEGMENTID", _strProcesssegmentid);
            param.Add("P_PROCESSSEGMENTVERSION", _strProcesssegmentversion);
            param.Add("P_PLANTID", _strPlantid);
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            cboAreaid.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboAreaid.ValueMember = "AREAID";
            cboAreaid.DisplayMember = "AREANAME";
            cboAreaid.EditValue = "";
            cboAreaid.ShowHeader = false;
            cboAreaid.DataSource = SqlExecuter.Query("GetAreaidByOspProcesssegmentid", "10001", param);
   

            
        }

        /// <summary>
        ///  그리드 리스트 초기화
        /// </summary>
        private void InitializegGrid()
        {
            grdSearch.GridButtonItem = GridButtonItem.Export;
            //grdSearch.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdSearch.View.SetIsReadOnly();
            grdSearch.View.AddTextBoxColumn("AREAID", 100)
                .SetIsHidden();
            grdSearch.View.AddTextBoxColumn("AREANAME", 100);
            grdSearch.View.AddTextBoxColumn("RESOURCEID", 100);
            grdSearch.View.AddTextBoxColumn("RESOURCENAME", 300);
           

            grdSearch.View.PopulateColumns();
        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            this.Load += OutsourcedResourceidpopup_Load;
            btnSearch.Click += BtnSearch_Click;
            btnSave.Click += BtnSave_Click;
            btnClose.Click += BtnCancel_Click;
            grdSearch.View.DoubleClick += grdSearch_DoubleClick;
        }
        /// <summary>
        /// 조회 클릭 - 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("P_PRODUCTDEFID", _strProductdefid);
            param.Add("P_PRODUCTDEFVERSION", _strProductdefversion);
            param.Add("P_PROCESSDEFID", _strProcessdefid);
            param.Add("P_PROCESSDEFVERSION", _strProcessdefversion);
            param.Add("P_PROCESSSEGMENTID", _strProcesssegmentid);
            param.Add("P_PROCESSSEGMENTVERSION", _strProcesssegmentversion);
            param.Add("P_PLANTID", _strPlantid);
            param.Add("P_AREAID", cboAreaid.EditValue.ToString()); //외주작업장
            param.Add("P_RESOURCENAME", txtResourcename.Text.Trim().ToString()); 
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param = Commons.CommonFunction.ConvertParameter(param);
            DataTable dtpop = SqlExecuter.Query("GetResourceidListByOsp", "10001", param);

            grdSearch.DataSource = dtpop;
        }
        /// <summary>
        /// 저장
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (grdSearch.View.DataRowCount==0)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            else
            {
                this.ResultDataEvent(grdSearch.View.GetFocusedDataRow());

                this.DialogResult = DialogResult.OK;
            }
           
        }
        private void grdSearch_DoubleClick(object sender, EventArgs e)
        {
            this.ResultDataEvent(grdSearch.View.GetFocusedDataRow());

            this.DialogResult = DialogResult.OK;
        }
        /// <summary>
        /// 취소
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// Form Road시 초기화
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OutsourcedResourceidpopup_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            InitializegGrid();
            InitializeCurrentData();

        }

        #endregion

        #region Private Function

        #endregion

        
    }
}
