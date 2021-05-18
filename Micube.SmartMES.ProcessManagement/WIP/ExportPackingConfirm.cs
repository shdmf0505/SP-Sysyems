#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons;
using Micube.SmartMES.Commons.Controls;
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
    /// 프 로 그 램 명  : 공정관리 > 재공 관리 > 체공 LOT조회
    /// 업  무  설  명  : 체공 LOT을 조회한다.
    /// 생    성    자  : 한주석
    /// 생    성    일  : 2019-10-08
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ExportPackingConfirm : SmartConditionManualBaseForm
    {
        #region Local Variables
        #endregion

        #region 생성자

        public ExportPackingConfirm()
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

            //Conditions.GetControl<SmartComboBox>("P_PRODUCTDEFTYPE").EditValue = "Product";
        }
        
        /// <summary>        
        /// 체공 LOT 조회 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {

            // TODO : 그리드 초기화 로직 추가
            grdPackingConfirm.GridButtonItem = GridButtonItem.Export;
            grdPackingConfirm.View.SetIsReadOnly();
            grdPackingConfirm.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            //SITE
            grdPackingConfirm.View.AddTextBoxColumn("PLANTID", 60).SetTextAlignment(TextAlignment.Center);
            //양산구분
            grdPackingConfirm.View.AddTextBoxColumn("PRODUCTIONTYPE", 60).SetTextAlignment(TextAlignment.Center).SetLabel("LOTTYPE");
            //품목코드
            grdPackingConfirm.View.AddTextBoxColumn("PRODUCTDEFID", 100).SetTextAlignment(TextAlignment.Left).SetLabel("ITEMID");
            //품목 리비전
            grdPackingConfirm.View.AddTextBoxColumn("PRODUCTDEFVERSION", 60).SetTextAlignment(TextAlignment.Center).SetLabel("ITEMVERSION");
            //품목명
            grdPackingConfirm.View.AddTextBoxColumn("PRODUCTDEFNAME", 150).SetTextAlignment(TextAlignment.Left);

            //공정수순
            grdPackingConfirm.View.AddTextBoxColumn("USERSEQUENCE", 70).SetTextAlignment(TextAlignment.Center);
            //공명명
            grdPackingConfirm.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 120).SetTextAlignment(TextAlignment.Left).SetLabel("PROCESSSEGMENT");
            //작업장
            grdPackingConfirm.View.AddTextBoxColumn("AREANAME", 100).SetTextAlignment(TextAlignment.Left);
            //LOT ID
            grdPackingConfirm.View.AddTextBoxColumn("LOTID", 120).SetTextAlignment(TextAlignment.Left);
            //수량
            grdPackingConfirm.View.AddSpinEditColumn("QTY", 60);
            //pnl 수량
            grdPackingConfirm.View.AddSpinEditColumn("PNLQTY", 60).SetLabel("PNL");

            grdPackingConfirm.View.PopulateColumns();
        }


        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
        }


       



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
			values.Add("P_LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable dtResult = await QueryAsync("SelectOutSidePackingWait", "10001", values);


            if (dtResult.Rows.Count < 1)
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }
            else
            {
                //txtConfirmDate.Text = dtResult.Rows[0]["CONFIRMDATE"].ToString();
                //txtConfirmUser.Text = dtResult.Rows[0]["CONFIRMUSERNAME"].ToString();
            }
            grdPackingConfirm.DataSource = dtResult;
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

			//품목코드
			InitializeCondition_ProductPopup();
			//LOTID
			CommonFunction.AddConditionLotPopup("P_LOTID", 1.1, true, Conditions);
			//공정
            CommonFunction.AddConditionProcessSegmentPopup("P_PROCESSSEGMENTID", 1.2, true, Conditions);
			//작업장
			CommonFunction.AddConditionAreaByAuthorityPopup("P_AREAID", 1.3, true, Conditions, false, false);
		}

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용

            /*
            Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").EditValueChanged += (sender, e) =>
            {
                SmartSelectPopupEdit edit = sender as SmartSelectPopupEdit;

                if (string.IsNullOrEmpty(Format.GetString(edit.EditValue)))
                    Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").Text = "";
            };
            */
        }

        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();
            DataTable dt = grdPackingConfirm.View.GetCheckedRows();

            if (dt == null || dt.Rows.Count == 0)
            {
                // LOT을 선택하여 주십시오.
                throw MessageException.Create("NoSeletedLot");
            }

            MessageWorker worker = new MessageWorker("SavePackingMoveConfirm");
            worker.SetBody(new MessageBody()
                    {
                        { "EnterpriseID", UserInfo.Current.Enterprise },
                        { "PlantID", UserInfo.Current.Plant },
                        { "UserId", UserInfo.Current.Id },
                        { "Lotlist", dt },
                    });

            worker.Execute();
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
				.SetPosition(0.1)
				.SetPopupResultCount(0)
				.SetPopupApplySelection((selectRow, gridRow) => {
					string productDefName = "";

                    if (selectRow.Count() > 0)
                    {
                        selectRow.AsEnumerable().ForEach(r =>
                        {
                            productDefName += Format.GetString(r["PRODUCTDEFNAME"]) + ",";
                        });
                    }

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

	}
}