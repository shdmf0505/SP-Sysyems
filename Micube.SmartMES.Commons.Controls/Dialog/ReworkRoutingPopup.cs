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

namespace Micube.SmartMES.Commons.Controls
{
    /// <summary>
    /// 프 로 그 램 명  : 공정관리 > 공정작업 > 재작업 Lot 투입 > 재작업 라우팅 팝업
    /// 업  무  설  명  : 재작업 라우팅을 선택한다.
    /// 생    성    자  : 황유성
    /// 생    성    일  : 2019-10-17
    /// 수  정  이  력  : 2019-12-26, 박정훈, Region 및 주석정리
    ///                                      재작업 Routing의 대공정 조회조건 추가
    /// 
    /// 
    /// </summary>
    public partial class ReworkRoutingPopup : SmartPopupBaseForm, ISmartCustomPopup
	{
        #region ◆ Public Variables |
        // 공정 ID
        public string ProcessSegmentId { get; set; }
        #endregion

        #region ◆ Local Variables
        public string LotId { get; set; }
		public DataRow CurrentDataRow { get; set; }


        #endregion

        #region ◆ 생성자
        public ReworkRoutingPopup()
		{
			InitializeComponent();

            InitializeEvent();
            InitializeGrid();
		}

        #region ▶  Grid Setting |
        /// <summary>
        /// Grid Setting
        /// </summary>
        private void InitializeGrid()
        {
            InitializeProcessDefGrid();
            InitializeProcessPathGrid();
        }

        #region - Routing Grid |
        /// <summary>
        /// Routing Grid
        /// </summary>
        private void InitializeProcessDefGrid()
        {
            grdProcessDef.View.OptionsSelection.MultiSelect = false;
            grdProcessDef.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;
            grdProcessDef.View.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.False;
            grdProcessDef.View.CheckMarkSelection.ShowCheckBoxHeader = false;
            grdProcessDef.View.SetIsReadOnly();
            grdProcessDef.GridButtonItem = GridButtonItem.None;

            grdProcessDef.View.AddTextBoxColumn("REWORKNUMBER", 100)            // 재작업번호
                .SetTextAlignment(TextAlignment.Left);
            grdProcessDef.View.AddTextBoxColumn("REWORKVERSION", 80)            // 재작업버전
                .SetTextAlignment(TextAlignment.Left);
            grdProcessDef.View.AddTextBoxColumn("REWORKNAME", 120)              // 재작업명
                .SetTextAlignment(TextAlignment.Left);
            grdProcessDef.View.AddTextBoxColumn("DESCRIPTION", 200);            // 설명
            grdProcessDef.View.AddTextBoxColumn("APPLICATIONTYPE", 80);         // 적용구분
            grdProcessDef.View.AddTextBoxColumn("REWORKTYPE", 120)              // 재작업구분
                .SetTextAlignment(TextAlignment.Left);
            grdProcessDef.View.AddTextBoxColumn("TOPPROCESSSEGMENTNAME", 70)      // 대공정
                .SetTextAlignment(TextAlignment.Left)
                .SetLabel("TOPPROCESSSEGMENTID");
            grdProcessDef.View.AddTextBoxColumn("CREATOR", 90);                 // 생성자

            grdProcessDef.View.PopulateColumns();
        }
        #endregion

        #region - Routing Path Grid |
        /// <summary>
        /// Routing Path Grid
        /// </summary>
        private void InitializeProcessPathGrid()
        {
            grdProcessPath.View.OptionsSelection.MultiSelect = false;
            grdProcessPath.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdProcessPath.View.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.False;
            grdProcessPath.GridButtonItem = GridButtonItem.None;

            grdProcessPath.View.AddTextBoxColumn("USERSEQUENCE", 80)        // 공정순서
                .SetTextAlignment(TextAlignment.Left)
                .SetIsReadOnly();
            grdProcessPath.View.AddTextBoxColumn("PROCESSSEGMENTID", 100)   // 공정코드
                .SetTextAlignment(TextAlignment.Left)
                .SetIsReadOnly();
            grdProcessPath.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 200) // 공정명
                .SetTextAlignment(TextAlignment.Left)
                .SetIsReadOnly();
            grdProcessPath.View.AddTextBoxColumn("COMMENT", 300) // 특기사항
                .SetTextAlignment(TextAlignment.Left)
                .SetIsReadOnly()
                .SetLabel("OPERATIONDFF");
            grdProcessPath.View.PopulateColumns();
            grdProcessPath.View.CheckMarkSelection.ShowCheckBoxHeader = false;
        }  
        #endregion
        #endregion

        #endregion

        #region ◆ Event
        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
		{
            btnSearch.Click += BtnSearch_Click;
            btnOk.Click += BtnOk_Click;
            btnCancel.Click += BtnCancel_Click;

            grdProcessDef.View.FocusedRowChanged += View_FocusedRowChanged;

            txtReworkNumber.KeyDown += TxtReworkNumber_KeyDown;
            txtReworkName.KeyDown += TxtReworkName_KeyDown;

            this.Shown += ReworkRoutingPopup_Shown;
		}

        private void ReworkRoutingPopup_Shown(object sender, EventArgs e)
        {
            Search();
        }

        #region ▶ Button Event |

        #region - Search Button |
        /// <summary>
        /// Search Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void Search()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("PLANTID", UserInfo.Current.Plant);
            param.Add("LanguageType", UserInfo.Current.LanguageType);
            param.Add("P_LOTID", LotId);
            param.Add("REWORKNUMBER", txtReworkNumber.Text);
            param.Add("REWORKNAME", txtReworkName.Text);
            param.Add("P_FILTERPLANT", "Y");

            DataTable dt = SqlExecuter.Query("SelectReworkRouting", "10001", param);

            grdProcessDef.DataSource = dt;
        }
        #endregion

        #region - OK Button |
        /// <summary>
        /// OK Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        #endregion

        #region - Cancel Button |
        /// <summary>
        /// Cancel Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.CurrentDataRow = null;
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        #endregion
        #endregion

        #region ▶ Text Event |

        #region - 재작업 번호 조회 |
        /// <summary>
        /// 재작업 번호 조회
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtReworkNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnSearch_Click(this, null);
            }
        }
        #endregion

        #region - 재작업명 조회 |
        /// <summary>
        /// 재작업명 조회
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtReworkName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnSearch_Click(this, null);
            }
        }
        #endregion

        #endregion

        #region ▶ Grid Event |
        private void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow focusedRow = grdProcessDef.View.GetFocusedDataRow();
            if (focusedRow != null)
            {
                this.CurrentDataRow = focusedRow;
                SearchProcessPath(focusedRow["REWORKNUMBER"].ToString(), focusedRow["REWORKVERSION"].ToString());
            }
        }
        #endregion

        #endregion

        #region ◆ Private Function |

        #region ▶ Routing 조회 |
        /// <summary>
        /// Routing 조회
        /// </summary>
        /// <param name="processDefId"></param>
        /// <param name="processDefVersion"></param>
        private void SearchProcessPath(string processDefId, string processDefVersion)
        {
            grdProcessPath.DataSource = new SqlQuery("GetProcessPathList", "10005", $"ENTERPRISEID={UserInfo.Current.Enterprise}"
                , $"PROCESSDEFID={processDefId}", $"PROCESSDEFVERSION={processDefVersion}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"
                , $"LOTID={this.LotId}").Execute();
        } 
        #endregion

        #endregion
    }
}
