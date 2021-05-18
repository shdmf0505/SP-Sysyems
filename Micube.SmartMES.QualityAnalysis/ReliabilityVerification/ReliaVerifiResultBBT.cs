#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons;
//using Micube.SmartMES.ProcessManagement;
using System;
using Micube.SmartMES.Commons.Controls;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;
using System.IO;



#endregion

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 신뢰성검증 > 신뢰성 검증 의뢰(정기)
    /// 업  무  설  명  : 신뢰성 정기 의뢰를 하는 화면이다. 동도금 정기적으로 신뢰성 검증 하는 의뢰를 한다. 계측 값 등록 시 자동으로 신뢰성 의뢰가 등록 됨.  
    /// 생    성    자  : 유석진
    /// 생    성    일  : 2019-07-10
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ReliaVerifiResultBBT : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가
        DataTable _dtVerifiCount = new DataTable();
        #endregion

        #region 생성자

        /// <summary>
        /// 생성자
        /// </summary>
        public ReliaVerifiResultBBT()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeGrdReliaVerifiResultRegular(); // 신뢰성 의뢰(정기) 그리드 초기화
            InitializeEvent();

            Dictionary<string, object> radioParam = new Dictionary<string, object>();
            radioParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            radioParam.Add("CODECLASSID", "VerifiCount");

            _dtVerifiCount = SqlExecuter.Query("GetCodeList", "00001", radioParam);
        }

        /// <summary>        
        /// 그리드 초기화(신뢰서 의뢰 접수(정기))
        /// </summary>
        private void InitializeGrdReliaVerifiResultRegular()
        {
            grdReliaVerifiResultRegular.GridButtonItem -= GridButtonItem.Delete; // 삭제 버튼 비활성화
            grdReliaVerifiResultRegular.GridButtonItem -= GridButtonItem.Add; // 추가 버튼 비활성화
            grdReliaVerifiResultRegular.GridButtonItem -= GridButtonItem.Copy; // 복사 버튼 비활성화
            grdReliaVerifiResultRegular.GridButtonItem -= GridButtonItem.Import; // Import 버튼 비활성화

            grdReliaVerifiResultRegular.View.SetSortOrder("SAMPLERECEIVEDATE");
            grdReliaVerifiResultRegular.View.SetSortOrder("LOTID");

            grdReliaVerifiResultRegular.View.AddTextBoxColumn("SAMPLERECEIVEDATE", 180).SetIsReadOnly().SetTextAlignment(TextAlignment.Center); // 시료접수일시
            grdReliaVerifiResultRegular.View.AddTextBoxColumn("VERIFICOMPLETEDATE", 180).SetIsReadOnly().SetTextAlignment(TextAlignment.Center).SetLabel("VERIFICOMPDATE"); // 검증완료일시
            grdReliaVerifiResultRegular.View.AddTextBoxColumn("TRANSITIONDATE", 70).SetIsReadOnly().SetTextAlignment(TextAlignment.Right); // 경과일
            //grdReliaVerifiResultRegular.View.AddTextBoxColumn("REQUESTDEPT", 210).SetIsReadOnly().SetTextAlignment(TextAlignment.Left); // 의뢰부서(공정)
            grdReliaVerifiResultRegular.View.AddTextBoxColumn("PRODUCTDEFID", 120).SetIsReadOnly().SetTextAlignment(TextAlignment.Center); // 품목코드
            grdReliaVerifiResultRegular.View.AddTextBoxColumn("PRODUCTDEFNAME", 210).SetIsReadOnly().SetTextAlignment(TextAlignment.Left); // 품목명
            grdReliaVerifiResultRegular.View.AddTextBoxColumn("PRODUCTDEFVERSION", 70).SetIsReadOnly().SetTextAlignment(TextAlignment.Center); // Rev
            grdReliaVerifiResultRegular.View.AddTextBoxColumn("LOTID", 170).SetIsReadOnly().SetTextAlignment(TextAlignment.Center); // LOT NO
            grdReliaVerifiResultRegular.View.AddTextBoxColumn("AREANAME", 150).SetIsReadOnly().SetTextAlignment(TextAlignment.Left); // 작업장
            //var manufatureInfo = grdReliaVerifiResultRegular.View.AddGroupColumn("MANUFACTURINFO");
            grdReliaVerifiResultRegular.View.AddSpinEditColumn("WORKENDPCSQTY", 70)
                .SetIsReadOnly().SetTextAlignment(TextAlignment.Right)
                .SetLabel("TOTALQTY")
                .SetDisplayFormat("#,##0", MaskTypes.Numeric); // 총수량(작업완료 수량)
            grdReliaVerifiResultRegular.View.AddSpinEditColumn("VERIFICOUNT", 70)
                .SetIsReadOnly().SetTextAlignment(TextAlignment.Right)
                .SetLabel("NCRINSPECTIONQTY")
                .SetDisplayFormat("#,##0", MaskTypes.Numeric); // 검사수량
            //grdReliaVerifiResultRegular.View.AddTextBoxColumn("EQUIPMENTNAME", 210).SetIsReadOnly().SetTextAlignment(TextAlignment.Left); // 설비(호기)
            //grdReliaVerifiResultRegular.View.AddTextBoxColumn("TRACKOUTTIME", 180).SetIsReadOnly().SetTextAlignment(TextAlignment.Center); // 작업종료시간
            //grdReliaVerifiResultRegular.View.AddTextBoxColumn("INSPECTIONRESULT", 150).SetDefault(false).SetTextAlignment(TextAlignment.Center); // 판정결과
            //grdReliaVerifiResultRegular.View.AddTextBoxColumn("ISNCRPUBLISH", 150).SetIsReadOnly().SetTextAlignment(TextAlignment.Center); // NCR발행여부

            grdReliaVerifiResultRegular.View.AddSpinEditColumn("DEFECTQTY", 70)
                .SetLabel("DEFECTQTY").SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,###,##0.####", MaskTypes.Numeric); // 불량수량
            grdReliaVerifiResultRegular.View.AddSpinEditColumn("SPECOUTPERCENTAGE", 70)
                .SetLabel("PCSDEFECTRATE").SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,###,##0.####", MaskTypes.Numeric); // 불량율
            grdReliaVerifiResultRegular.View.AddTextBoxColumn("DEFECTCODENAME", 150).SetLabel("DEFECTNAME").SetIsReadOnly().SetTextAlignment(TextAlignment.Left); // 불량명
            //grdReliaVerifiResultRegular.View.AddTextBoxColumn("QCSEGMENTNAME", 150);
            //grdReliaVerifiResultRegular.View.AddTextBoxColumn("ISCOMPLETION", 150).SetIsReadOnly().SetTextAlignment(TextAlignment.Center); // 완료여부
            grdReliaVerifiResultRegular.View.PopulateColumns();
        }
        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            grdReliaVerifiResultRegular.View.DoubleClick += GrdReliaVerifiResultRegularView_DoubleClick;

            //this.Conditions.GetControl<SmartComboBox>("P_ISJUDGMENTRESULT").EditValueChanged += ReliaVerifiResultRegular_EditValueChanged;
        }

        /// <summary>        
        /// 판정결과 변경시
        /// </summary>
        private void ReliaVerifiResultRegular_EditValueChanged(object sender, EventArgs e)
        {
            if (this.Conditions.GetControl<SmartComboBox>("P_ISJUDGMENTRESULT").EditValue.ToString() == "OK")
            {
                this.Conditions.GetControl<SmartComboBox>("P_ISNCRISSUESTATUS").Enabled = false;

            } else
            {
                this.Conditions.GetControl<SmartComboBox>("P_ISNCRISSUESTATUS").Enabled = true;
            }
        }

        /// <summary>        
        /// 의뢰 목록 더블클릭 시
        /// </summary>
        private void GrdReliaVerifiResultRegularView_DoubleClick(object sender, EventArgs e)
        {
            DialogManager.ShowWaitArea(pnlContent);

            ReliaVerifiResultBBTPopup popup = new ReliaVerifiResultBBTPopup(grdReliaVerifiResultRegular.View.GetFocusedDataRow());
            popup.ParentForm = this;
            popup.Owner = this;
            popup.btnSave.Enabled = btnFlag.Enabled;
            popup.ShowDialog();

            DialogManager.CloseWaitArea(pnlContent);
        }
        #endregion

        #region 툴바

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            // TODO : 저장 Rule 변경
            //DataTable changed = grdCodeClass.GetChangedRows();

            //ExecuteRule("SaveCodeClass", changed);
        }

        #endregion

        #region 검색

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            var values = Conditions.GetValues();
            DataTable dt = null;


            //values.Add("P_VerifiCount", "R"); // 의뢰
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            dt = await SqlExecuter.QueryAsync("GetReliaVerifiResultBBTlist", "10001", values);

            if (dt.Rows.Count < 1)
            {
                ShowMessage("NoSelectData");
            }

            grdReliaVerifiResultRegular.DataSource = dt;
        }

        #endregion

        #region 조회조건 설정

        protected override void InitializeCondition()
        {
            base.InitializeCondition();
            //Site
            //var cboPlant = Conditions.AddComboBox("P_PLANTID", new SqlQuery("GetPlantList", "10019", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            //cboPlant.SetLabel("PLANTID");
            //cboPlant.SetPosition(1);
            //cboPlant.SetDefault("");
            //cboPlant.SetEmptyItem("", "");



            // 품목
            InitializeConditionPopup_Product();
            CommonFunction.AddConditionLotHistPopup("P_LOTID", 4.2, false, Conditions);
            //불량코드
            InitializeConditionPopup_DefectCode();
        }

        /// <summary>
        /// 품목 조회조건
        /// </summary>
        private void InitializeConditionPopup_Product()
        {
            // 팝업 컬럼설정
            var productPopup = Conditions.AddSelectPopup("p_productdefId", new SqlQuery("GetProductListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PRODUCTDEFNAME", "PRODUCTDEFIDVERSION")
               .SetPopupLayout("PRODUCT", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupLayoutForm(600, 800)
               .SetLabel("PRODUCT")
               .SetPopupAutoFillColumns("PRODUCTDEFNAME")
               .SetPopupResultCount(1)
               .SetPosition(4)
               .SetPopupResultCount(0);

            // 팝업 조회조건
            productPopup.Conditions.AddTextBox("PRODUCTDEFIDNAME");

            // 팝업 그리드
            productPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 120)
                .SetValidationKeyColumn();
            productPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 220);
            productPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 80);
            productPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFIDVERSION", 100)
                .SetIsHidden();
        }

        /// <summary>
        /// 불량코드 조회조건
        /// </summary>
        private void InitializeConditionPopup_DefectCode()
        {
            // 팝업 컬럼설정
            var defectCodePopup = Conditions.AddSelectPopup("p_defectCode", new SqlQuery("GetDefectCodeList", "10004", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "DEFECTCODENAME", "DEFECTCODE")
               .SetPopupLayout("DEFECTCODE", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupLayoutForm(700, 500)
               .SetLabel("DEFECTNAME")
               .SetPopupResultCount(1)
               .SetPosition(4.3)
               .SetRelationIds("p_plantId");

            // 팝업 조회조건
            defectCodePopup.Conditions.AddTextBox("DEFECTCODENAME");

            // 팝업 그리드

            defectCodePopup.GridColumns.AddTextBoxColumn("DEFECTCODE", 150).SetValidationKeyColumn(); ;
            defectCodePopup.GridColumns.AddTextBoxColumn("DEFECTCODENAME", 150);
            defectCodePopup.GridColumns.AddTextBoxColumn("QCSEGMENTID", 150);
            defectCodePopup.GridColumns.AddTextBoxColumn("QCSEGMENTNAME", 150);
        }

        #endregion

        #region Private Function


        #endregion

        #region Global Function

        /// <summary>
        /// Popup 닫혔을때 재검색하기 위한 함수
        /// </summary>
        public void Search()
        {
            OnSearchAsync();
        }

        #endregion
    }
}
