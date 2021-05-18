#region using
using Micube.Framework.Net;
using Micube.Framework;
using Micube.Framework.SmartControls;
using System;
using Micube.SmartMES.Commons.Controls;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DevExpress.XtraCharts;
using DevExpress.Utils;
using Micube.Framework.SmartControls.Validations;
#endregion

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프로그램명 : 기준정보 > Setup > 품목규칙 등록
    /// 업 무 설명 : 품목 코드,명,계산식 등록
    /// 생  성  자 : 윤성원
    /// 생  성  일 : 2019-06-27
    /// 수정 이 력 : 
    /// </summary> 
	public partial class LeadTime : SmartConditionManualBaseForm
	{
        #region Local Variables
        #endregion

        #region 생성자
        public LeadTime()
		{
			InitializeComponent();
            InitializeEvent();
           
        }

        #endregion

        #region 컨텐츠 영역 초기화


        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGridIdDefinitionManagement()
        {

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Lead Time 관리 그리드 초기화
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            grdlayer.GridButtonItem = GridButtonItem.All;   // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기
            grdlayer.View.AddComboBoxColumn("LAYER", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=TypeLayer", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetTextAlignment(TextAlignment.Center);
            grdlayer.View.AddTextBoxColumn("LAYER_LT", 60).SetTextAlignment(TextAlignment.Center);
            grdlayer.View.AddTextBoxColumn("DESCRIPTION", 150);

            grdlayer.View.PopulateColumns();


            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("PLANTID", UserInfo.Current.Plant);


            Initialize_ProductPopup(grdproduct);
            grdproduct.View.AddTextBoxColumn("PRODUCTDEFVERSION",60).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdproduct.View.AddTextBoxColumn("PRODUCTDEFNAME", 200).SetIsReadOnly();
            grdproduct.View.AddComboBoxColumn("FACTORYID", 80, new SqlQuery("GetFactoryListByPlant", "10001", param), "FACTORYNAME", "FACTORYID") //공장구분
                .SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdproduct.View.AddComboBoxColumn("LAYER", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=TypeLayer", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdproduct.View.AddTextBoxColumn("LAYER_LT", 60).SetTextAlignment(TextAlignment.Center);
            grdproduct.View.AddTextBoxColumn("DESCRIPTION", 250);

            grdproduct.View.PopulateColumns();

            /*

            grdhistory.View.AddTextBoxColumn("PRODUCTDEFID", 80);
            grdhistory.View.AddTextBoxColumn("PROUDCTDEFVERSION", 250);
            grdhistory.View.AddTextBoxColumn("PRODUCTDEFNAME", 250);
            grdhistory.View.AddTextBoxColumn("SITE", 60).SetTextAlignment(TextAlignment.Center);
            grdhistory.View.AddTextBoxColumn("LAYER", 80).SetTextAlignment(TextAlignment.Center);
            grdhistory.View.AddTextBoxColumn("LAYER_LT", 80).SetTextAlignment(TextAlignment.Center);
            grdhistory.View.AddTextBoxColumn("DESCRIPTION", 250);
            grdhistory.View.AddTextBoxColumn("CREATOR", 80)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();
            grdhistory.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly()
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss");
            grdhistory.View.AddTextBoxColumn("MODIFIER", 80)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();
            grdhistory.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly()
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss");


            grdhistory.View.PopulateColumns();
        */

        }


        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeGridIdDefinitionManagement();
        }

     

        #endregion

        #region 이벤트

        private void InitializeEvent()
        {
            grdlayer.View.FocusedRowChanged += View_FocusedRowChanged1;

            grdproduct.View.AddingNewRow += View_AddingNewRow;

        }

        private void View_FocusedRowChanged1(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

            grdlayer_FocusedRowChanged();



        }

        private void grdlayer_FocusedRowChanged()
        {
            if (grdlayer.View.FocusedRowHandle < 0)
                return;

            DataRow row = grdlayer.View.GetFocusedDataRow();

            Dictionary<string, object> Param = new Dictionary<string, object>();
            Param.Add("LAYER", row["LAYER"].ToString());

            DataTable dtleadtime = SqlExecuter.Query("GetLeadTimeSub", "10001", Param);

            grdproduct.DataSource = dtleadtime;
        }

        private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            DataRow row = grdlayer.View.GetFocusedDataRow();

            args.NewRow["LAYER"] = row["LAYER"];

        }


       
      
        #endregion

        #region 툴바
        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            


            // 저장후 룰대상 그리드를 읽기 전용으로 변경 처리 한다.
            //grdRuleDefinitionList.View.SetIsReadOnly(false);
            //grdRuleDefinitionList.View.SetIsReadOnly(false);

        }
        #endregion

        #region 검색

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
		{
			await base.OnSearchAsync();

            // 조회조건에 입력된 값들을 Dictionary<string, object> Type으로 가져옴

            var values = this.Conditions.GetValues();
            //TODO : Id를 수정하세요            
            // Stored Procedure 호출
            //this.grdCodeClass.DataSource = await this.ProcedureAsync("usp_com_selectCodeClass", values);
            // Server Xml Query 호출



            DataTable dtleadtime = await SqlExecuter.QueryAsync("SelectLeadTimeMain", "10001", values);

            if (dtleadtime.Rows.Count < 1) // 
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            this.grdlayer.DataSource = dtleadtime;

            grdlayer_FocusedRowChanged();

        }
        #endregion

        /// <summary>
        /// 조회조건 영역 초기화 시작
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            



        }
        /// <summary>
        /// 검색조건 팝업 예제
        /// </summary>
        /// <summary>
        /// 품목코드 팝업 생성
        /// </summary>
        /// <param name="grid"></param>
        private void Initialize_ProductPopup(SmartBandedGrid grid)
        {
            //팝업 컬럼 설정
            var control = grid.View.AddSelectPopupColumn("PRODUCTDEFID", 90, new SqlQuery("GetProductDefList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                    .SetPopupLayout("PRODUCT", PopupButtonStyles.Ok_Cancel, true, false)
                                    .SetPopupResultCount()
                                    .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                                    .SetPopupResultMapping("DURABLEDEFNAME", "PRODUCTDEFID")
                                    //.SetRelationIds("PLANTID")
                                    .SetLabel("PRODUCTDEFID")
                                    .SetPopupApplySelection((selectedRows, gridRow) =>
                                    {
                                        foreach (DataRow row in selectedRows)
                                        {
                                            gridRow["PRODUCTDEFNAME"] = row["PRODUCTDEFNAME"].ToString();
                                            gridRow["PRODUCTDEFVERSION"] = row["PRODUCTDEFVERSION"].ToString();
                                      
                                            gridRow["FACTORYID"] = row["OWNERFACTORYID"].ToString();
                                        }
                                    });

            control.Conditions.AddTextBox("PRODUCTDEF");

            // 팝업 그리드
            control.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            control.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            control.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 200);
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();
            //SmartSelectPopupEdit Popupedit = Conditions.GetControl<SmartSelectPopupEdit>("ITEMCODE");
            //Popupedit.Validated += Popupedit_Validated;

        }


        #region 유효성 검사
        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            DataTable changed = new DataTable();
            grdlayer.View.CheckValidation();
            changed = grdlayer.GetChangedRows();

            DataTable changed1 = new DataTable();
            grdproduct.View.CheckValidation();
            changed1 = grdproduct.GetChangedRows();


            if (changed.Rows.Count == 0 && changed1.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }

            string layer = "layer";
            string product = "product";
            changed.TableName = layer;
            changed1.TableName = product;



            MessageWorker messageWorker = new MessageWorker("SaveLeadTime");
            messageWorker.SetBody(new MessageBody()
            {
                 { layer, changed }
                , { product, changed1}
            });
            messageWorker.Execute();


        }
        #endregion

        /// <summary>
        /// 팝업 컬럼에서 확인 클릭 시 다른 데이터 매핑 ( 작업장)
        /// </summary>
        /// <param name="currentGridRow">화면 grid row</param>
        /// <param name="popupSelections">팝업에서 선택한 row</param>
        /// <returns></returns>
        private Micube.Framework.SmartControls.Validations.ValidationResultCommon ValidationSpecificationsItemPopup(DataRow currentGridRow, IEnumerable<DataRow> popupSelections)
        {
            Micube.Framework.SmartControls.Validations.ValidationResultCommon result = new Framework.SmartControls.Validations.ValidationResultCommon();

            foreach (DataRow row in popupSelections)
            {
                Dictionary<string, object> Param = new Dictionary<string, object>();
                Param.Add("ENTERPRISEID", row["ENTERPRISEID"].ToString());
                Param.Add("ITEMID", row["ITEMID"].ToString());
                Param.Add("ITEMVERSION", row["ITEMVERSION"].ToString());
                DataTable dtRelationShipChk = SqlExecuter.Query("GetRelationShipChk", "10001", Param);

                if(dtRelationShipChk.Rows.Count > 1)
                {
                    Language.LanguageMessageItem item = Language.GetMessage("SelectOverlap", row["ITEMID"].ToString());
                    result.IsSucced = false;
                    result.FailMessage = item.Message;
                    result.Caption = item.Title;
                }
                else
                {
                    currentGridRow["LEADTIME"] = row["LEADTIME"];
                    currentGridRow["TACTTIME"] = row["TACTTIME"];
                    currentGridRow["TARGETKEY2"] = row["ITEMVERSION"];
                    currentGridRow["TARGETKEY3"] = row["ITEMNAME"];
                    currentGridRow["ITEMID"] = row["ITEMID"];
                    currentGridRow["MASTERDATACLASSID"] = row["MASTERDATACLASSID"];
                }
            }
            return result;
        }


    }
}

