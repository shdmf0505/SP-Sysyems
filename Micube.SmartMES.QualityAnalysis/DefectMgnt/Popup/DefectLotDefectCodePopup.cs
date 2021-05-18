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
    /// 프 로 그 램 명  : 품질관리 > 불량관리 > 불량품 인수등록
    /// 업  무  설  명  : 불량이 발생한 Lot에 대해 상세한 불량코드를 보여준다.
    /// 생    성    자  : 유태근
    /// 생    성    일  : 2019-07-04
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class DefectLotDefectCodePopup : SmartPopupBaseForm, ISmartCustomPopup
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

        #endregion

        #region 생성자

        /// <summary>
        /// 생성자
        /// </summary>
        public DefectLotDefectCodePopup()
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
            grdDefectCode.View.SetIsReadOnly();
            grdDefectCode.View.SetAutoFillColumn("DEFECTQTY");

            grdDefectCode.View.AddTextBoxColumn("PROCESSDATE", 200)
                .SetTextAlignment(TextAlignment.Center); // 처리일시
            grdDefectCode.View.AddTextBoxColumn("AREANAME", 120); // 작업장명
            grdDefectCode.View.AddTextBoxColumn("DEFECTCODE", 80)
                .SetTextAlignment(TextAlignment.Center); // 불량코드
            grdDefectCode.View.AddTextBoxColumn("DEFECTCODENAME", 150); // 불량명
            grdDefectCode.View.AddTextBoxColumn("QCSEGMENTNAME", 150); // 품질공정명
            grdDefectCode.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 180)
                .SetLabel("REASONSEGMENTNAME"); // 원인공정
            grdDefectCode.View.AddTextBoxColumn("DEFECTQTY", 80); // 불량수

            grdDefectCode.View.PopulateColumns();

            grdDefectCode.DataSource = _defectDt;
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
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LOTID", CurrentDataRow["LOTID"].ToString());
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            _defectDt = SqlExecuter.Query("GetDefectLotDefectCode", "10001", param);
        }

        #endregion
    }
}
