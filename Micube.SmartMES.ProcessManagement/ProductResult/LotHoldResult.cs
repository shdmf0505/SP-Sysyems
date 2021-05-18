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
    /// 프 로 그 램 명  : 공정관리 > 셍산실적 > 보류현황 조회
    /// 업  무  설  명  : 
    /// 생    성    자  : 조혜인
    /// 생    성    일  : 2019-10-02
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class LotHoldResult : SmartConditionManualBaseForm
    {
        #region Local Variables

        

        #endregion

        #region 생성자

        public LotHoldResult()
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
        }

        /// <summary>        
        /// 코드그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdLotHoldResult.GridButtonItem = GridButtonItem.Export;
            grdLotHoldResult.SetIsUseContextMenu(true);
            grdLotHoldResult.View.SetIsReadOnly();

            grdLotHoldResult.View.AddComboBoxColumn("CLASS", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=HoldState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetTextAlignment(TextAlignment.Center);
			grdLotHoldResult.View.AddTextBoxColumn("CURRENTSTATUS", 60).SetTextAlignment(TextAlignment.Center);
			grdLotHoldResult.View.AddTextBoxColumn("HOLDDATE", 130).SetTextAlignment(TextAlignment.Center);
			grdLotHoldResult.View.AddTextBoxColumn("RELEASEHOLDDATE", 130).SetTextAlignment(TextAlignment.Center);
			grdLotHoldResult.View.AddTextBoxColumn("PRODUCTDEFID", 100);
            grdLotHoldResult.View.AddTextBoxColumn("PRODUCTDEFNAME", 180);
            grdLotHoldResult.View.AddTextBoxColumn("LOTID", 180).SetTextAlignment(TextAlignment.Center);
            grdLotHoldResult.View.AddTextBoxColumn("CUSTOMERNAME", 100);
            //grdLotHoldResult.View.AddTextBoxColumn("LAYER", 60).SetTextAlignment(TextAlignment.Center);
			grdLotHoldResult.View.AddTextBoxColumn("UOM", 60).SetTextAlignment(TextAlignment.Center);
			//grdLotHoldResult.View.AddSpinEditColumn("SUMMARY", 60).SetDisplayFormat("#,##0", MaskTypes.Numeric, false);
            //grdLotHoldResult.View.AddSpinEditColumn("QTY",80).SetDisplayFormat("#,##0", MaskTypes.Numeric, false); 
            grdLotHoldResult.View.AddSpinEditColumn("PCSQTY",80).SetDisplayFormat("#,##0", MaskTypes.Numeric, false);
			grdLotHoldResult.View.AddSpinEditColumn("PNLQTY",80).SetDisplayFormat("#,##0", MaskTypes.Numeric, false);
			grdLotHoldResult.View.AddSpinEditColumn("OSPMM",80).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true);
			grdLotHoldResult.View.AddSpinEditColumn("AMOUNTS", 80).SetDisplayFormat("#,##0", MaskTypes.Numeric, false);
			grdLotHoldResult.View.AddTextBoxColumn("HOLDUSER", 70).SetTextAlignment(TextAlignment.Center);
			grdLotHoldResult.View.AddTextBoxColumn("TOPPROCESSSEGMENTCLASS", 80);
            grdLotHoldResult.View.AddTextBoxColumn("MIDDLEPROCESSSEGMENTCLASS", 100);            
			grdLotHoldResult.View.AddTextBoxColumn("CLASSIFY", 100);
            grdLotHoldResult.View.AddTextBoxColumn("PCRNO",120);
			grdLotHoldResult.View.AddTextBoxColumn("HOLDCOMMENT", 150).SetLabel("DETAILNOTE");
			grdLotHoldResult.View.AddTextBoxColumn("RELEASECOMMENTS", 150).SetLabel("RELEASEREASON");
			

			grdLotHoldResult.View.PopulateColumns();

        }

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            grdLotHoldResult.InitContextMenuEvent += GrdLotHoldResultInitContextMenuEvent;
        }

        // Customizing Context Menu Item 초기화
        private void GrdLotHoldResultInitContextMenuEvent(SmartBandedGrid sender, Framework.SmartControls.Grid.InitContextMenuEventArgs args)
        {
            args.AddMenus.Add( new DXMenuItem(Language.Get("MENU_PG-SG-0450"), OpenForm) { BeginGroup = true, Tag = "PG-SG-0450" });
            args.AddMenus.Add( new DXMenuItem(Language.Get("MENU_PG-SG-0340"), OpenForm) { Tag = "PG-SG-0340" });
            args.AddMenus.Add(new DXMenuItem(Language.Get("MENU_PG-SG-0460"), OpenForm) { Tag = "PG-SG-0460" });
        }


        /// <summary>
        /// 코드그룹 리스트 그리드에서 추가 버튼 클릭 시 호출
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            throw new NotImplementedException();
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

            values["P_PRODUCTNAME"] = Format.GetString(values["P_PRODUCTNAME"]).TrimEnd(',');

            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            
            DataTable dtLotHoldResult = await SqlExecuter.QueryAsync("SelectLotHoldResult", "10001", values);
            
            if (dtLotHoldResult.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdLotHoldResult.DataSource = dtLotHoldResult;
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            // Lot
            CommonFunction.AddConditionLotPopup("P_LOTID", 2.1, true, Conditions);
            // 품목
            InitializeCondition_ProductPopup();
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
           
        }

        /// <summary>
        /// 팝업형 조회조건 생성 - 품목
        /// </summary>
        private void InitializeCondition_ProductPopup()
        {
            // SelectPopup 항목 추가
            var conditionProductId = Conditions.AddSelectPopup("P_PRODUCTDEFID", new SqlQuery("GetProductDefinitionList", "10002", $"PLANTID={UserInfo.Current.Plant}"), "PRODUCTDEF", "PRODUCTDEF")
                .SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, true)
                .SetPopupLayoutForm(800, 800)
                .SetPopupAutoFillColumns("PRODUCTDEFNAME")
                .SetLabel("PRODUCTDEFID")
                .SetPosition(1.2)
                .SetPopupResultCount(0)
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
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 100);
            // 품목유형구분
            conditionProductId.GridColumns.AddComboBoxColumn("PRODUCTDEFTYPE", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductDefType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            // 생산구분
            conditionProductId.GridColumns.AddComboBoxColumn("PRODUCTIONTYPE", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            // 단위
            conditionProductId.GridColumns.AddComboBoxColumn("UNIT", 90, new SqlQuery("GetUomDefinitionList", "10001", "UOMCLASSID=Segment"), "UOMDEFNAME", "UOMDEFID");
        }




        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
        /// </summary>
        //protected override void OnValidateContent()
        //{
        //    base.OnValidateContent();

        //    // TODO : 유효성 로직 변경
        //    grdList.View.CheckValidation();

        //    DataTable changed = grdList.GetChangedRows();

        //    if (changed.Rows.Count == 0)
        //    {
        //        // 저장할 데이터가 존재하지 않습니다.
        //        throw MessageException.Create("NoSaveData");
        //    }
        //}

        #endregion

        #region Private Function

        // TODO : 화면에서 사용할 내부 함수 추가
        /// <summary>
        /// 마스터 정보를 조회한다.
        /// </summary>
        private void LoadData()
        {

        }
        // Customizing Context Menu Item Click 이벤트
        private void OpenForm(object sender, EventArgs args)
        {
            try
            {
                DialogManager.ShowWaitDialog();

                DataRow currentRow = grdLotHoldResult.View.GetFocusedDataRow();

                string menuId = (sender as DXMenuItem).Tag.ToString();

                var param = currentRow.Table.Columns
                    .Cast<DataColumn>()
                    .ToDictionary(col => col.ColumnName, col => currentRow[col.ColumnName]);

                OpenMenu(menuId, param); //다른창 호출..
            }
            catch (Exception ex)
            {
                this.ShowError(ex);
            }
            finally
            {
                DialogManager.Close();
            }
        }


        #endregion
    }
}