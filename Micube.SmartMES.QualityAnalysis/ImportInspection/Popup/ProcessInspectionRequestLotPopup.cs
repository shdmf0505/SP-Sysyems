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
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 수입 검사 관리 > 공정 수입검사 의뢰 화면의 LotId 팝업
    /// 업  무  설  명  : 
    /// 생    성    자  : 강유라
    /// 생    성    일  : 2019-09-02
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    /// 
    public partial class ProcessInspectionRequestLotPopup : SmartPopupBaseForm, ISmartCustomPopup
    {

        #region Interface

        public DataRow CurrentDataRow { get; set; }

        #endregion

        #region Local Variables
        /// 부모 그리드로 데이터를 바인딩 시켜줄 델리게이트
        /// </summary>
        /// <param name="dt"></param>
        public delegate void LotSelectionDataHandler(DataTable dt);
        public event LotSelectionDataHandler LotSelectEvent;
        #endregion

        #region 생성자
        public ProcessInspectionRequestLotPopup()
        {
            InitializeComponent();
            InitializeEvent();
            InitializeGrid();
            CreateProductDefPopup();
            CreateProcessSegmentPopup();
        }
        #endregion

        #region 컨텐츠 영역 초기화
        private void InitializeGrid()
        {
            grdLot.GridButtonItem = GridButtonItem.None;
            grdLot.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdLot.View.SetIsReadOnly();

            grdLot.View.AddTextBoxColumn("LOTID", 150);

            grdLot.View.AddTextBoxColumn("VENDORID", 100);

            grdLot.View.AddTextBoxColumn("VENDORNAME", 100);

            grdLot.View.AddTextBoxColumn("PRODUCTDEFID", 100);

            grdLot.View.AddTextBoxColumn("PRODUCTDEFNAME", 200);

            grdLot.View.AddTextBoxColumn("PRODUCTDEFVERSION", 100);

            grdLot.View.AddTextBoxColumn("PROCESSSEGMENTID", 100);

            grdLot.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);

            grdLot.View.AddTextBoxColumn("PROCESSSEGMENTVERSION", 100);

            grdLot.View.AddTextBoxColumn("AREAID", 100);

            grdLot.View.AddTextBoxColumn("AREANAME", 100);

            grdLot.View.AddTextBoxColumn("WORKENDPCSQTY", 100);

            grdLot.View.AddTextBoxColumn("WORKENDPANELQTY", 100);

            grdLot.View.AddTextBoxColumn("WORKENDMMQTY", 100);

            grdLot.View.AddTextBoxColumn("LOTHISTKEY", 200)
                .SetIsHidden();

            grdLot.View.AddTextBoxColumn("ENTERPRISEID", 200)
                .SetIsHidden();

            grdLot.View.AddTextBoxColumn("PLANTID", 200)
                .SetIsHidden();

            grdLot.View.PopulateColumns();
        }

        #region 검색조건 팝업 초기화
        private void CreateProductDefPopup()
        {
            ConditionItemSelectPopup conditionItem = new ConditionItemSelectPopup();
            conditionItem.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            conditionItem.SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, true);
            conditionItem.Id = "PRODUCTDEFID";
            conditionItem.LabelText = "PRODUCTDEFID";
            //conditionItem.SearchQuery = new SqlQuery("GetProductDefList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}");
            conditionItem.SearchQuery = new SqlQuery("GetProductDefList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}");
            conditionItem.IsMultiGrid = false;
            conditionItem.DisplayFieldName = "PRODUCTDEFNAME";
            conditionItem.ValueFieldName = "PRODUCTDEFID";
            conditionItem.LanguageKey = "PRODUCTDEF";

            conditionItem.Conditions.AddTextBox("PRODUCTDEF");

            conditionItem.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            conditionItem.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            conditionItem.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 200);

            popProductDefId.SelectPopupCondition = conditionItem;
        }

        private void CreateProcessSegmentPopup()
        {
            ConditionItemSelectPopup conditionItem = new ConditionItemSelectPopup();
            conditionItem.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            conditionItem.SetPopupLayout("SELECTPROCESSSEGMENT", PopupButtonStyles.Ok_Cancel, true, true);
            conditionItem.Id = "PROCESSSEGMENTID";
            conditionItem.LabelText = "PROCESSSEGMENT";
            //conditionItem.SearchQuery = new SqlQuery("GetProcessSegmentList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}");
            conditionItem.SearchQuery = new SqlQuery("GetProcessSegmentList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}");
            conditionItem.IsMultiGrid = false;
            conditionItem.DisplayFieldName = "PROCESSSEGMENTNAME";
            conditionItem.ValueFieldName = "PROCESSSEGMENTID";
            conditionItem.LanguageKey = "PROCESSSEGMENT";

            conditionItem.Conditions.AddTextBox("PROCESSSEGMENT");

            conditionItem.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150);
            conditionItem.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
            conditionItem.GridColumns.AddTextBoxColumn("PROCESSSEGMENTVERSION", 200);

            popProcessSegmentId.SelectPopupCondition = conditionItem;
        }

        #endregion

        #endregion

        #region Event
        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            //닫기버튼 이벤트
            btnClose.Click += (s, e) => { this.Close(); };
            //검색버튼 이벤트
            btnSearch.Click += (s, e) => { SearchLotList(); };
            // 적용버튼 이벤트
            btnApply.Click += BtnApply_Click;
        }


        /// <summary>
        /// 적용 버튼 클릭시 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnApply_Click(object sender, EventArgs e)
        {
            DataTable dtcheck = grdLot.View.GetCheckedRows();

            if (dtcheck.Rows.Count == 0)
            {
                ShowMessage("GridNoChecked"); //(선택된 내역이 없습니다)/
                return;
            }

            LotSelectEvent(dtcheck);
            this.Close();
        }
        #endregion

        #region Private Function
        /// <summary>
        /// LotList를 조회하는 함수
        /// </summary>
        private void SearchLotList()
        {
            Dictionary<string, object> values = new Dictionary<string, object>()
            {
                {"LANGUAGETYPE", Framework.UserInfo.Current.LanguageType},
                {"ENTERPRISEID",Framework.UserInfo.Current.Enterprise},
                {"PLANTID",Framework.UserInfo.Current.Plant },
                {"PRODUCTDEFID",popProductDefId.GetValue() },
                {"PROCESSSEGMENTID",popProcessSegmentId.GetValue() },
                {"LOTID", txtLotId.EditValue}
            };

            DataTable dt = SqlExecuter.Query("GetLotIdToInspRequest", "10001", values);
            grdLot.DataSource = dt;
        }
        #endregion
    }
}
