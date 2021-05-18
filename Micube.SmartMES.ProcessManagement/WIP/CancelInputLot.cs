#region using
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons;
using Micube.SmartMES.Commons.Controls;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
#endregion

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 공정관리 > 투입관리 > LOT 투입 취소
    /// 업  무  설  명  : LOT 투입 취소
    /// 생    성    자  : 황유성
    /// 생    성    일  : 2019-09-18
    /// 수  정  이  력  : 
    /// 
    /// </summary>
    public partial class CancelInputLot : SmartConditionManualBaseForm
    {
        /// <summary>
        /// 생성자
        /// </summary>
        public CancelInputLot()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Control 설정
        /// </summary>
        protected override void InitializeContent()
        {
            base.InitializeContent();
            InitializeEvent();
        }

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            this.Load += Form_Load;
            Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").EditValueChanged += CancelInputLot_EditValueChanged;
        }

        /// <summary>
        /// Form Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Form_Load(object sender, EventArgs e)
        {
            InitializeGrid();
        }

        private void CancelInputLot_EditValueChanged(object sender, EventArgs e)
        {
            string productDef = Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").EditValue.ToString();
            Conditions.GetCondition<ConditionItemSelectPopup>("P_PRODUCTIONORDERID").SearchQuery = new SqlQuery("GetProductionOrderIdListOfLotInput", "10002"
                , $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"
                , $"PRODUCTDEF={productDef}");
        }

        /// <summary>
        /// 검색조건 설정
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            InitializeConditionProductOrderId_Popup();
            //InitializeConditionProductDefId_Popup();
            CommonFunction.AddConditionProductPopup("P_PRODUCTDEFID", 1.2, true, Conditions, "PRODUCTDEFNAME", "PRODUCTDEFID", false, 0, false);
        }

        /// <summary>
        /// 팝업형 조회조건 생성 - PO
        /// </summary>
        private void InitializeConditionProductOrderId_Popup()
        {
            var conditionProductionOrderId = Conditions.AddSelectPopup("P_PRODUCTIONORDERID"
                , new SqlQuery("GetProductionOrderIdListOfLotInput", "10002"
                , $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")
                , "PRODUCTIONORDER", "PRODUCTIONORDER")
               .SetPopupLayout("SELECTPRODUCTIONORDER", PopupButtonStyles.Ok_Cancel, true, false)
               .SetPopupResultCount(0)
               .SetPopupLayoutForm(1000, 800)
               .SetPopupAutoFillColumns("PRODUCTDEFNAME")
               .SetLabel("PRODUCTIONORDERID")
               .SetPosition(1.1)
               .SetPopupApplySelection((selectedRows, dataGridRows) =>
               {
                   // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
                   // dataGridRow : 현재 Focus가 있는 그리드의 DataRow
               });


            // 팝업에서 사용되는 검색조건 (P/O번호)
            conditionProductionOrderId.Conditions.AddTextBox("TXTPRODUCTIONORDERID");

            // 팝업 그리드에서 보여줄 컬럼 정의
            // P/O번호
            conditionProductionOrderId.GridColumns.AddTextBoxColumn("PRODUCTIONORDERID", 150)
                .SetValidationKeyColumn();
            // 수주라인
            conditionProductionOrderId.GridColumns.AddTextBoxColumn("LINENO", 50);
            // 수주량
            conditionProductionOrderId.GridColumns.AddSpinEditColumn("PLANQTY", 90);
            // 품목코드
            conditionProductionOrderId.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            // 품목명
            conditionProductionOrderId.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            // 품목버전
            conditionProductionOrderId.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 100);
            // 품목코드 | 품목버전
            conditionProductionOrderId.GridColumns.AddTextBoxColumn("PRODUCTDEF", 0).SetIsHidden();
        }

        /// <summary>
        /// 팝업형 조회조건 생성 - 품목
        /// </summary>
        private void InitializeConditionProductDefId_Popup()
        {
            // SelectPopup 항목 추가
            var conditionProductId = Conditions.AddSelectPopup("P_PRODUCTDEFID", new SqlQuery("GetProductDefinitionListForCancelInputLot", "10001", $"PLANTID={UserInfo.Current.Plant}"), "PRODUCTDEF", "PRODUCTDEF")
                .SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupLayoutForm(1000, 800)
                .SetPopupAutoFillColumns("PRODUCTDEFNAME")
                .SetLabel("PRODUCTDEF")
                .SetPosition(1.2)
                .SetValidationIsRequired()
                .SetPopupApplySelection((selectedRows, dataGridRows) =>
                {
                    //selectedRows : 팝업에서 선택한 DataRow(컬렉션)
                    //dataGridRow : 현재 Focus가 있는 그리드의 DataRow
                });

            conditionProductId.SetPopupResultCount(0);

            // 팝업에서 사용되는 검색조건 (품목코드/명)
            conditionProductId.Conditions.AddTextBox("TXTPRODUCTDEFNAME");
            //제품 구분 추가 2019.08.14 배선용, 노석안 대리 요청
            conditionProductId.Conditions.AddComboBox("PRODUCTDIVISION", new SqlQuery("GetTypeList", "10001", $"CODECLASSID=ProductDivision2", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                                        .SetDefault("Product");
            
            // 팝업 그리드에서 보여줄 컬럼 정의
            // 품목코드
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            // 품목명
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            // 품목버전
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 100);
            // 품목유형구분
            conditionProductId.GridColumns.AddComboBoxColumn("PRODUCTDEFTYPE", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductDefType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            // 생산구분
            conditionProductId.GridColumns.AddComboBoxColumn("PRODUCTIONTYPE", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            // 단위
            conditionProductId.GridColumns.AddComboBoxColumn("UNIT", 90, new SqlQuery("GetUomDefinitionList", "10001", "UOMCLASSID=Segment"), "UOMDEFNAME", "UOMDEFID");
            // 품목코드 | 품목버전
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEF", 0).SetIsHidden();
        }

        /// <summary>
        /// 조회조건 설정
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();
        } 

        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            grdWIP.GridButtonItem = GridButtonItem.Export;

            grdWIP.View.SetIsReadOnly();
            grdWIP.SetIsUseContextMenu(false);
            // CheckBox 설정
            grdWIP.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdWIP.View.AddTextBoxColumn("STARTEDDATE", 120).SetTextAlignment(TextAlignment.Center);
            grdWIP.View.AddTextBoxColumn("PLANTID", 50).SetTextAlignment(TextAlignment.Center);
            grdWIP.View.AddTextBoxColumn("LOTTYPE", 70).SetTextAlignment(TextAlignment.Center);

            grdWIP.View.AddTextBoxColumn("PRODUCTIONORDERID", 90).SetTextAlignment(TextAlignment.Center);
            grdWIP.View.AddTextBoxColumn("LINENO", 60).SetTextAlignment(TextAlignment.Center);
            grdWIP.View.AddTextBoxColumn("PRODUCTDEFID", 150).SetTextAlignment(TextAlignment.Center);
            grdWIP.View.AddTextBoxColumn("PRODUCTDEFNAME", 240);
            grdWIP.View.AddTextBoxColumn("PRODUCTREVISION", 50).SetTextAlignment(TextAlignment.Center);
            grdWIP.View.AddTextBoxColumn("LOTID", 190).SetTextAlignment(TextAlignment.Center);
            grdWIP.View.AddTextBoxColumn("USERSEQUENCE", 70).SetTextAlignment(TextAlignment.Center);
            grdWIP.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 160);
            grdWIP.View.AddTextBoxColumn("AREANAME", 140).SetTextAlignment(TextAlignment.Center);

            grdWIP.View.AddTextBoxColumn("RTRSHT", 70).SetTextAlignment(TextAlignment.Center);
            grdWIP.View.AddTextBoxColumn("QTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric);
            grdWIP.View.AddTextBoxColumn("PANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric);

            grdWIP.View.PopulateColumns();
        }

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            // 기존 Grid Data 초기화
            this.grdWIP.DataSource = null;
           
            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable dt = await SqlExecuter.QueryAsync("SelectWIPListToCancelInput", "10001", values);

            if (dt.Rows.Count < 1)
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdWIP.DataSource = dt;
        }

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            DataTable dtLotList = new DataTable();
            dtLotList.Columns.Add("LOTID", typeof(string));
            dtLotList.Columns.Add("RTRSHT", typeof(string));

            foreach(DataRow each in grdWIP.View.GetCheckedRows().Rows)
            {
                DataRow newRow = dtLotList.NewRow();
                newRow["LOTID"] = each["LOTID"];
                newRow["RTRSHT"] = each["RTRSHT"];
                dtLotList.Rows.Add(newRow);
            }

            MessageWorker worker = new MessageWorker("CancelInputLot");
            worker.SetBody(new MessageBody()
            {
                { "lotList", dtLotList }
            });
            worker.Execute();
        }

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();
        }
    }
}
