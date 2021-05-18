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

namespace Micube.SmartMES.QualityAnalysis
{
    public partial class MeasureFormManagementPopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        /// <summary>
        /// 팝업진입전 그리드의 Row
        /// </summary>
        public DataRow CurrentDataRow { get; set; }

        /// <summary>
        /// 부모  Control 받기
        /// </summary>
        public SmartBandedGrid tParent { get; set; }
        public DataTable       grdParent { get; set; }

        #region 생성자

        public MeasureFormManagementPopup()
        {
            InitializeComponent();

            InitializeGrid(); // 그리드 초기화
            InitializeEvent(); // 이벤트 초기화
        }

        public MeasureFormManagementPopup(string param)
        {
            InitializeComponent();

            InitializeGrid(); // 그리드 초기화
            InitializeEvent(); // 이벤트 초기화
            serach();
        }
        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            InitializeGrdMeasureHistory();
        }

        /// <summary>
        /// 제조 이력 그리드 초기화
        /// </summary>
        private void InitializeGrdMeasureHistory()
        {
            grdFileList.GridButtonItem = GridButtonItem.None;
            grdFileList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdFileList.View.SetSortOrder("SEQUENCE");

            grdFileList.View.AddTextBoxColumn("FILEID", 100)
                .SetIsHidden()
                .SetIsReadOnly();
            grdFileList.View.AddTextBoxColumn("FILENAME", 300)
                .SetIsReadOnly();
            grdFileList.View.AddTextBoxColumn("FILEEXT", 100)
                .SetIsReadOnly();
            grdFileList.View.AddTextBoxColumn("FILEPATH", 200)
                .SetIsHidden()
                .SetIsReadOnly();
            grdFileList.View.AddTextBoxColumn("SAFEFILENAME", 200)
                .SetIsHidden()
                .SetIsReadOnly();
            grdFileList.View.AddSpinEditColumn("FILESIZE", 100)
                .SetDisplayFormat()
                .SetIsReadOnly();
            grdFileList.View.AddSpinEditColumn("SEQUENCE", 70)
                .SetIsHidden()
                .SetIsReadOnly();
            grdFileList.View.AddTextBoxColumn("LOCALFILEPATH", 200)
                .SetIsHidden()
                .SetIsReadOnly();
            grdFileList.View.AddTextBoxColumn("RESOURCETYPE", 100)
                .SetIsHidden()
                .SetIsReadOnly();
            grdFileList.View.AddTextBoxColumn("RESOURCEID", 100)
                .SetIsHidden()
                .SetIsReadOnly();
            grdFileList.View.AddTextBoxColumn("RESOURCEVERSION", 100)
                .SetIsHidden()
                .SetIsReadOnly();
            grdFileList.View.AddTextBoxColumn("ENTERPRISEID", 100)
                .SetIsHidden()
                .SetIsReadOnly();
            grdFileList.View.AddTextBoxColumn("PLANTID", 100)
                .SetIsHidden()
                .SetIsReadOnly();
            grdFileList.View.AddTextBoxColumn("COMMENTS", 300);
            grdFileList.View.AddComboBoxColumn("VALIDSTATE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Invalid")
                .SetValidationIsRequired();

            grdFileList.View.PopulateColumns();

            grdFileList.View.OptionsNavigation.AutoMoveRowFocus = false;
        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            this.Load += ManufacturingHistoryPopup_Load; ;
            btnApply.Click += BtnApply_Click;
            btnClose.Click += BtnClose_Click;
        }

        /// <summary>        
        /// 적용 버튼 선택
        /// </summary>
        private void BtnApply_Click(object sender, EventArgs e)
        {
            DataTable dt = grdFileList.View.GetCheckedRows();

            grdParent = dt;

            this.Close();
        }

        /// <summary>        
        /// 닫기 버튼 선택
        /// </summary>
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        /// <summary>        
        /// 팝업화면 로드 후 이벤트 호출
        /// </summary>
        private void ManufacturingHistoryPopup_Load(object sender, EventArgs e)
        {

        }

        #endregion

        private void btnSearch_Click(object sender, EventArgs e)
        {
            serach();
        }
        private void serach()
        { 
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("P_PLANTID", "YPE");
            param.Add("P_VALIDSTATE", "Valid");
            param.Add("p_languageType", UserInfo.Current.LanguageType);

            DataTable dt = SqlExecuter.Query("GetMeasureFormFileList", "10001", param);

            if (dt.Rows.Count == 0)
            {
                // 조회할 데이터가 없습니다.
                this.ShowMessage("NoSelectData");
                ClearData();
                return;
            }

            grdFileList.DataSource = dt;

        }
        public void ClearData()
        {
            if (grdFileList.DataSource == null) return;
            grdFileList.View.ClearDatas();
        }
    }
}
