#region using

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

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 불량품 폐기취소 > 재작업 라우팅 팝업
    /// 업  무  설  명  : 재작업 라우팅을 선택한다.
    /// 생    성    자  : 유태근
    /// 생    성    일  : 2019-10-28
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ReworkRoutingPopup : SmartPopupBaseForm, ISmartCustomPopup
	{
		#region Local Variables

        public string LotId { get; set; }

		public DataRow CurrentDataRow { get; set; }

        #endregion

        #region 생성자

        /// <summary>
        /// 생성자
        /// </summary>
        public ReworkRoutingPopup()
		{
			InitializeComponent();
            InitializeCombobox();

            InitializeEvent();
            InitializeGrid();
		}

        /// <summary>
        /// 콤보박스 초기화
        /// </summary>
        private void InitializeCombobox()
        {
            cboProcessClass.DisplayMember = "CODENAME";
            cboProcessClass.ValueMember = "CODEID";
            cboProcessClass.UseEmptyItem = true;
            cboProcessClass.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboProcessClass.ShowHeader = false;
            // 쿼리 틀린거 같음
            DataTable processClass = new SqlQuery("GetTypeList", "10001", $"CODECLASSID=ProcessClassType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}").Execute();
            cboProcessClass.DataSource = processClass;
        }

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            InitializeProcessDefGrid();
            InitializeProcessPathGrid();
        }

        /// <summary>
        /// 라우팅정의 그리드
        /// </summary>
        private void InitializeProcessDefGrid()
        {
            grdProcessDef.View.OptionsSelection.MultiSelect = false;
            grdProcessDef.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdProcessDef.View.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.False;
            grdProcessDef.View.CheckMarkSelection.ShowCheckBoxHeader = false;
            grdProcessDef.View.SetAutoFillColumn("DESCRIPTION");
            grdProcessDef.View.SetIsReadOnly();
            grdProcessDef.GridButtonItem = GridButtonItem.None;

            grdProcessDef.View.AddTextBoxColumn("APPLICATIONTYPE", 80)
                .SetTextAlignment(TextAlignment.Left); // 적용구분
            grdProcessDef.View.AddTextBoxColumn("REWORKTYPE", 100)              
                .SetTextAlignment(TextAlignment.Left); // 재작업구분
            grdProcessDef.View.AddTextBoxColumn("TOPPROCESSSEGMENTNAME", 100)      
                .SetTextAlignment(TextAlignment.Left)
                .SetLabel("LARGEPROCESSSEGMENT"); // 대공정
            grdProcessDef.View.AddTextBoxColumn("REWORKNUMBER", 100)           
                .SetTextAlignment(TextAlignment.Left); // 재작업번호
            grdProcessDef.View.AddTextBoxColumn("REWORKVERSION", 80)            
                .SetTextAlignment(TextAlignment.Center); // 재작업버전
            grdProcessDef.View.AddTextBoxColumn("REWORKNAME", 120)          
                .SetTextAlignment(TextAlignment.Left); // 재작업명
            grdProcessDef.View.AddTextBoxColumn("DESCRIPTION", 200)
                .SetTextAlignment(TextAlignment.Left); // 설명

            grdProcessDef.View.PopulateColumns();
        }

        /// <summary>
        /// 라우팅 상세정의 그리드
        /// </summary>
        private void InitializeProcessPathGrid()
        {
            grdProcessPath.View.OptionsSelection.MultiSelect = false;
            grdProcessPath.View.CheckMarkSelection.ShowCheckBoxHeader = false;
            grdProcessPath.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdProcessPath.View.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.False;
            grdProcessPath.View.SetIsReadOnly();
            grdProcessPath.GridButtonItem = GridButtonItem.None;

            grdProcessPath.View.AddTextBoxColumn("USERSEQUENCE", 80)       
                .SetTextAlignment(TextAlignment.Right); // 공정순서
            grdProcessPath.View.AddTextBoxColumn("PROCESSSEGMENTID", 100)   
                .SetTextAlignment(TextAlignment.Left); // 공정코드
            grdProcessPath.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 200) 
                .SetTextAlignment(TextAlignment.Left); // 공정명
            grdProcessPath.View.AddTextBoxColumn("PROCESSSEGMENTVERSION", 80)
                .SetTextAlignment(TextAlignment.Center); // 공정버전

            grdProcessPath.View.PopulateColumns();
        }

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
		{
            grdProcessDef.View.CheckStateChanged += View_CheckStateChanged;
            btnOk.Click += BtnOk_Click;
            btnCancel.Click += BtnCancel_Click;
            btnSearch.Click += BtnSearch_Click;
            txtReworkIdName.KeyDown += TxtReworkIdName_KeyDown;
		}

        /// <summary>
        /// 검색버튼엔터
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtReworkIdName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchProcessDef();
            }
        }

        /// <summary>
        /// 검색버튼클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            SearchProcessDef();
        }

        /// <summary>
        /// 적용버튼클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// 취소버튼클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.CurrentDataRow = null;
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// 라우팅 선택에 따라 해당하는 공정수순 보여주기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CheckStateChanged(object sender, EventArgs e)
        {
            // 한 행만 체크 가능
            grdProcessDef.View.CheckStateChanged -= View_CheckStateChanged;
            grdProcessDef.View.CheckedAll(false);

            grdProcessDef.View.CheckRow(grdProcessDef.View.GetFocusedDataSourceRowIndex(), true);
            grdProcessDef.View.CheckStateChanged += View_CheckStateChanged;

            DataTable checkedRows = grdProcessDef.View.GetCheckedRows();

            if (checkedRows.Rows.Count > 0)
            {
                this.CurrentDataRow = checkedRows.Rows[0];
                SearchProcessPath(checkedRows.Rows[0]["REWORKNUMBER"].ToString(), checkedRows.Rows[0]["REWORKVERSION"].ToString());
            }
        }

        /// <summary>
        /// 해당 라우팅의 라우팅 상세정의 검색
        /// </summary>
        /// <param name="processDefId"></param>
        /// <param name="processDefVersion"></param>
        private void SearchProcessPath(string processDefId, string processDefVersion)
        {
            grdProcessPath.DataSource = new SqlQuery("GetProcessPathList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PROCESSDEFID={processDefId}", $"PROCESSDEFVERSION={processDefVersion}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}").Execute();
        }

        #endregion

        #region Private Function

        /// <summary>
        /// 재작업라우팅 검색
        /// </summary>
        private void SearchProcessDef()
        {
            DataTable dt = new SqlQuery("SelectQCReworkRouting", "10001", $"PLANTID={UserInfo.Current.Plant}", $"P_LOTID={LotId}", $"P_PROCESSCLASSTYPE={cboProcessClass.EditValue}", $"P_REWORKIDNAME={txtReworkIdName.EditValue}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}").Execute();
            grdProcessDef.DataSource = dt;
        }

        #endregion
    }
}
