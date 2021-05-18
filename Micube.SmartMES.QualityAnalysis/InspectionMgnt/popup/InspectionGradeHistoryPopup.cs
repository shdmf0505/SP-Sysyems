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

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 검사원관리 > 검사원 등급관리 > 검사원 등급이력
    /// 업  무  설  명  : 검사원 등급에 대한 점수 이력을 보여준다.
    /// 생    성    자  : 유태근
    /// 생    성    일  : 2019-07-29
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class InspectionGradeHistoryPopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region Local Variables

        /// <summary>
        /// 팝업진입전 그리드의 Row
        /// </summary>
        public DataRow CurrentDataRow { get; set; }

        #endregion

        #region 생성자

        /// <summary>
        /// 생성자
        /// </summary>
        public InspectionGradeHistoryPopup()
        {
            InitializeComponent();

            InitializeEvent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 부모그리드에서 가져온 데이터 컨트롤에 바인딩
        /// </summary>
        private void InitializeCurrentData()
        {
            txtCapacityType.EditValue = CurrentDataRow["INSPECTIONCLASSNAME"];
            txtScore.EditValue = CurrentDataRow["LOWERSCORE"] + " ~ " + CurrentDataRow["UPPERSCORE"];
            txtGrade.EditValue = CurrentDataRow["GRADE"];
        }

        /// <summary>
        /// 검사원 등급이력 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            grdInspectionGrade.View.SetIsReadOnly();
            grdInspectionGrade.View.SetAutoFillColumn("CREATEDTIME");

            grdInspectionGrade.View.AddTextBoxColumn("CREATEDTIME", 250)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("REGISTRATIONDATE"); // 등록일자
            grdInspectionGrade.View.AddTextBoxColumn("SCORE", 200)
                .SetTextAlignment(TextAlignment.Center); // 점수
            grdInspectionGrade.View.AddTextBoxColumn("GRADE", 200)
                .SetTextAlignment(TextAlignment.Center); // 등급

            grdInspectionGrade.View.PopulateColumns();
        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            this.Load += ChemicalissuePopup_Load;

            btnClose.Click += BtnClose_Click;
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

            InitializeGrid();
            InitializeCurrentData();
            SearchHistory();
        }

        #endregion
 
        #region Private Function

        /// <summary>
        /// 이력 검색함수
        /// </summary>
        private void SearchHistory()
        {
            Dictionary<string, object> param = new Dictionary<string, object>()
            {
                { "P_INSPECTIONCLASSID", CurrentDataRow["INSPECTIONCLASSID"] },
                { "P_GRADE", CurrentDataRow["GRADE"] },
                { "P_LANGUAGETYPE", UserInfo.Current.LanguageType}
            };

            DataTable dt = SqlExecuter.Query("GetInspectionGradeHistory", "10001", param);
            grdInspectionGrade.DataSource = dt;
        }

        #endregion
    }
}
