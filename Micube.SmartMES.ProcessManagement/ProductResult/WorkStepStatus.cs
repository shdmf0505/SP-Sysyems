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
using Micube.SmartMES.Commons;
using DevExpress.Utils.Menu;
using Micube.SmartMES.Commons.Controls;

#endregion

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 공정관리 > 생산실적 > 작업스텝현황
    /// 업  무  설  명  : 
    /// 생    성    자  : 조혜인
    /// 생    성    일  : 2019-10-02
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class WorkStepStatus : SmartConditionManualBaseForm
    {
        #region Local Variables

        

        #endregion

        #region 생성자

        public WorkStepStatus()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 화면의 컨텐츠 영역을 초기화한다.
        /// </summary>
        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeEvent();

            // TODO : 컨트롤 초기화 로직 구성
            InitializeGrid();
            Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").ReadOnly = true;
        }

        /// <summary>        
        /// 코드그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdList.GridButtonItem = GridButtonItem.Export;
           
            grdList.View.SetIsReadOnly();
            //거래처
            grdList.View.AddTextBoxColumn("VENDORNAME", 80).SetTextAlignment(TextAlignment.Left).SetLabel("VENDOR");
            //작업장
            grdList.View.AddTextBoxColumn("AREANAME", 80).SetTextAlignment(TextAlignment.Left).SetLabel("AREANAME");
            //작업일자
            grdList.View.AddTextBoxColumn("WORK_DATE", 130).SetTextAlignment(TextAlignment.Left).SetLabel("ACTUALDATE");
            //상태
            grdList.View.AddTextBoxColumn("STEP_NAME", 60).SetTextAlignment(TextAlignment.Left).SetLabel("STATE");
            //공정수순
            grdList.View.AddTextBoxColumn("USERSEQUENCE", 60).SetTextAlignment(TextAlignment.Center).SetLabel("USERSEQUENCE");
            //공정id
            grdList.View.AddTextBoxColumn("PROCESSSEGMENTID", 60).SetTextAlignment(TextAlignment.Left).SetLabel("PROCESSSEGMENTID");
            //공정명
            grdList.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 120).SetTextAlignment(TextAlignment.Left).SetLabel("PROCESSSEGMENTNAME");
            //품목코드   
			grdList.View.AddTextBoxColumn("PRODUCTDEFID", 100);
            //rev.
            grdList.View.AddTextBoxColumn("PRODUCTDEFVERSION", 50).SetLabel("PRODUCTREVISION");
            //품목명
            grdList.View.AddTextBoxColumn("PRODUCTDEFNAME", 150);
            //층수
            grdList.View.AddTextBoxColumn("LAYER", 50).SetTextAlignment(TextAlignment.Center).SetLabel("LAYER");
            //lot id
            grdList.View.AddTextBoxColumn("LOTID", 180).SetTextAlignment(TextAlignment.Center);
            //uom
			grdList.View.AddTextBoxColumn("UOM", 50).SetTextAlignment(TextAlignment.Center);
            //pcs
            grdList.View.AddSpinEditColumn("PCSQTY",80).SetDisplayFormat("#,##0", MaskTypes.Numeric, false);
            //pnl
			grdList.View.AddSpinEditColumn("PNLQTY",80).SetDisplayFormat("#,##0", MaskTypes.Numeric, false);
            //mm
			grdList.View.AddSpinEditColumn("MM", 80).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true);
            //불량PCS
			grdList.View.AddSpinEditColumn("DEFECTQTY", 80).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetLabel("DEFECTPCSQTY");
            //불량PCS
            grdList.View.AddSpinEditColumn("DEFECTPNLQTY", 80).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetLabel("DEFECTPNLQTY");
            //합수
            grdList.View.AddSpinEditColumn("PCSPNL", 80).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetLabel("ARRAY");
            //생성자
            grdList.View.AddTextBoxColumn("RECEIVEUSER", 70).SetTextAlignment(TextAlignment.Center).SetLabel("CREATOR");


			grdList.View.PopulateColumns();

        }

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        //protected override void OnToolbarSaveClick()
        //{
        //    base.OnToolbarSaveClick();

        //    // TODO : 저장 Rule 변경
        //    DataTable changed = grdList.GetChangedRows();

        //    ExecuteRule("SaveCodeClass", changed);
        //}

        #endregion

        #region 검색

        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            // TODO : 조회 SP 변경
            var values = Conditions.GetValues();


            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            
            DataTable dtLotHoldResult = await SqlExecuter.QueryAsync("SelectFourStepWorkStatus", "10001", values);
            
            if (dtLotHoldResult.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdList.DataSource = dtLotHoldResult;
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            // 품목
            InitializeCondition_ProductPopup();
            // 작업장
            CommonFunction.AddConditionAreaByAuthorityPopup("P_AREAID", 3.2, false, Conditions, false, false);
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").EditValueChanged += ProductdefIDChanged;

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용

        }
        private void ProductdefIDChanged(object sender, EventArgs e)
        {
            SmartSelectPopupEdit PopProdutid = sender as SmartSelectPopupEdit;

            if (Format.GetFullTrimString(PopProdutid.EditValue).Equals(string.Empty))
            {
                Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").Text = string.Empty;
            }
        }
        /// <summary>
        /// 팝업형 조회조건 생성 - 품목
        /// </summary>
        private void InitializeCondition_ProductPopup()
        {
            // SelectPopup 항목 추가
            var conditionProductId = Conditions.AddSelectPopup("P_PRODUCTDEFID", new SqlQuery("GetProductDefinitionList", "10002", $"PLANTID={UserInfo.Current.Plant}"), "PRODUCTDEF", "PRODUCTDEF")
                .SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupLayoutForm(800, 800)
                .SetPopupAutoFillColumns("PRODUCTDEFNAME")
                .SetLabel("PRODUCTDEFID")
                .SetPosition(2.1)
                .SetPopupResultCount(1)
                .SetPopupApplySelection((selectRow, gridRow) => {
                    string productDefName = "";

                    selectRow.AsEnumerable().ForEach(r => {
                        productDefName += Format.GetString(r["PRODUCTDEFNAME"]) + ",";
                    });

                    Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").EditValue = productDefName;
                });

            // 팝업에서 사용되는 검색조건 (품목코드/명)
            conditionProductId.Conditions.AddTextBox("TXTPRODUCTDEFNAME");
            //제품 구분 추가 2019.08.14 배선용, 노석안 대리 요청
            conditionProductId.Conditions.AddComboBox("PRODUCTDIVISION", new SqlQuery("GetTypeList", "10001", $"CODECLASSID=ProductDivision2", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                .SetDefault("Product");

            // 품목코드
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            // 품목명
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            // 품목버전
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 50);
            // 품목유형구분
            conditionProductId.GridColumns.AddComboBoxColumn("PRODUCTDEFTYPE", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductDefType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            // 생산구분
            conditionProductId.GridColumns.AddComboBoxColumn("PRODUCTIONTYPE", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            // 단위
            conditionProductId.GridColumns.AddComboBoxColumn("UNIT", 90, new SqlQuery("GetUomDefinitionList", "10001", "UOMCLASSID=Segment"), "UOMDEFNAME", "UOMDEFID");
        }




        #endregion

        #region 유효성 검사

        #endregion

        #region Private Function

        // TODO : 화면에서 사용할 내부 함수 추가
       
        #endregion
    }
}