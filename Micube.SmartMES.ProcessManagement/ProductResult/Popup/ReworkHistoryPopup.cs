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

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 공정관리 > 생산실적 > 재작업이력조회 > 재작업 라우팅 팝업
    /// 업  무  설  명  : 재작업 라우팅 정보를 확인한다
    /// 생    성    자  : 황유성
    /// 생    성    일  : 2020-01-23
    /// 수  정  이  력  : 
    /// 
    /// </summary>
    public partial class ReworkHistoryPopup : SmartPopupBaseForm, ISmartCustomPopup
	{
        #region ◆ Public Variables |
        // 라우팅 ID
        public string ProcessDefId { get; set; }
        // 라우팅 버전
        public string ProcessDefVersion { get; set; }
        #endregion

        #region ◆ Local Variables
        public DataRow CurrentDataRow { get; set; }
        #endregion

        #region ◆ 생성자
        public ReworkHistoryPopup()
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
            grdProcessPath.View.AddTextBoxColumn("DESCRIPTION", 200)        // 특기사항
                .SetTextAlignment(TextAlignment.Left)
                .SetLabel("REMARKS")
                .SetIsReadOnly();
            grdProcessPath.View.PopulateColumns();
            grdProcessPath.View.CheckMarkSelection.ShowCheckBoxHeader = false;
        }
        #endregion

        #endregion

        #region ◆ Event
        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
		{
            this.Shown += ReworkHistoryPopup_Shown;
		}

        private void ReworkHistoryPopup_Shown(object sender, EventArgs e)
        {
            Search();
        }

        #region ▶ Button Event |

        #region - Search |
        private void Search()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("PROCESSDEFID", this.ProcessDefId);
            param.Add("PROCESSDEFVERSION", this.ProcessDefVersion);
            DataTable dt = SqlExecuter.Query("SelectCommentByProcess", "10021", param);
            grdProcessPath.DataSource = dt;
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
            this.Close();
        }
        #endregion
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
            grdProcessPath.DataSource = new SqlQuery("GetProcessPathList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PROCESSDEFID={processDefId}", $"PROCESSDEFVERSION={processDefVersion}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}").Execute();
        } 
        #endregion

        #endregion
    }
}
