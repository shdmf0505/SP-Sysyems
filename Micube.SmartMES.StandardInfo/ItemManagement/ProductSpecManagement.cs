#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;

using System;
using Micube.SmartMES.Commons.Controls;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Base;
using SmartTextBox = Micube.Framework.SmartControls.SmartTextBox;
using System.Drawing;
using Micube.Framework.SmartControls.Grid.BandedGrid;

#endregion

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프 로 그 램 명  : 기준정보 > 품목 관리 > 품목 사양 정보
    /// 업  무  설  명  : 품목 스펙 저장
    /// 생    성    자  : 정승원
    /// 생    성    일  : 2019-12-12
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ProductSpecManagement : SmartConditionManualBaseForm
    {
		
		#region Local Variables

		//제품정보
		private ProductSpecManangementProductInfo productInfo { get; set; }
		//제품사양
        private ProductSpecManangementProductSpec productSpec { get; set; }
		//회로사양
        private ProductSpecManangementCircuitSpec circuitSpec { get; set; }
		//기타정보
        private ProductSpecManangementEtcInfo etcInfo { get; set; }

		//도금정보
        private ProductSpecManagementPlatingSpec platingSpec { get; set; }



		#endregion

		#region 생성자

		public ProductSpecManagement()
        {
            InitializeComponent();
        }

		/// <summary>
		/// 다른 화면에서 파라미터 넘길 때
		/// </summary>
		/// <param name="parameters"></param>
		public override void LoadForm(Dictionary<string, object> parameters)
		{
			base.LoadForm(parameters);

			if (parameters != null)
			{
				string productDefId = Format.GetString(parameters["PRODUCTDEFID"]);
				string productDefVersion = Format.GetString(parameters["PRODUCTREVISION"]);
				if (!string.IsNullOrWhiteSpace(productDefId) && !string.IsNullOrWhiteSpace(productDefVersion))
				{
					string product = productDefId + "|" + productDefVersion;
					Conditions.GetControl<SmartSelectPopupEdit>("P_ITEMID").SetValue(product);
					Conditions.GetControl<SmartSelectPopupEdit>("P_ITEMID").Text = product;

					OnSearchAsync();
				}
			}
		}

		#endregion



		#region 컨텐츠 영역 초기화

		/// <summary>
		/// 화면의 컨텐츠 영역을 초기화한다.
		/// </summary>
		protected override void InitializeContent()
        {
            base.InitializeContent();

            productInfo = new ProductSpecManangementProductInfo();
            productSpec = new ProductSpecManangementProductSpec();
            circuitSpec = new ProductSpecManangementCircuitSpec();
            etcInfo = new ProductSpecManangementEtcInfo();
            platingSpec = new ProductSpecManagementPlatingSpec();
            
            InitializeEvent();
            InitializeGrid();
            InitializeTableLayout();
        }

		/// <summary>
		/// TableLayout 초기화
		/// </summary>
        private void InitializeTableLayout()
        {
            xtraTabPage1.Controls.Add(etcInfo);
            etcInfo.Dock = DockStyle.Top;
            etcInfo.Height -= 15;

            xtraTabPage1.Controls.Add(circuitSpec);
            circuitSpec.Dock = DockStyle.Top;
            circuitSpec.Height -= 15;

            xtraTabPage1.Controls.Add(productSpec);
            productSpec.Dock = DockStyle.Top;
            productSpec.Height -= 15;

            xtraTabPage1.Controls.Add(productInfo);
            productInfo.Dock = DockStyle.Top;
            productInfo.Height -= 15;

            smartPanel1.Controls.Add(platingSpec);
            platingSpec.Dock = DockStyle.Fill;

            //etcInfo.Width = xtraTabPage1.Size.Width;
            //circuitSpec.Width = xtraTabPage1.Size.Width;
            //productSpec.Width = xtraTabPage1.Size.Width;
            //productInfo.Width = xtraTabPage1.Size.Width;

            //platingSpec.Width = smartPanel1.Size.Width;
            smartPanel1.Height = platingSpec.Height;

        }

        /// <summary>        
        /// 그리드를 초기화
        /// </summary>
        private void InitializeGrid()
        {
            //층간구성도
            grdLayerComposition.GridButtonItem = GridButtonItem.Export;
            grdLayerComposition.ButtonBarVisible(false);
            grdLayerComposition.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdLayerComposition.View.SetIsReadOnly();
            // 자재코드
            //InitializeGrdLayerComposition_MaterialDefIdPopup();
            grdLayerComposition.View.AddTextBoxColumn("ASSEMBLYITEMID", 200)
             .SetLabel("MATERIALDEF")
             .SetValidationIsRequired();
            // 자재명
            grdLayerComposition.View.AddTextBoxColumn("BOTASSEMBLYITEMNAME", 200)
                .SetLabel("MATERIALNAME");
            //자재유형
            grdLayerComposition.View.AddComboBoxColumn("CONSUMABLETYPE", 200, new SqlQuery("GetTypeList", "10001", "CODECLASSID=ConsumableType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("MATERIALTYPE");
            //사용층
            grdLayerComposition.View.AddComboBoxColumn("USERLAYER", 200, new SqlQuery("GetTypeList", "10001", "CODECLASSID=UserLayer", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("USERLAYER");

            grdLayerComposition.View.PopulateColumns();
            


            //잉크정보
            grdInkInfo.GridButtonItem = GridButtonItem.Export | GridButtonItem.Add | GridButtonItem.Delete;
            grdInkInfo.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            ////순번
            grdInkInfo.View.AddTextBoxColumn("SEQUENCE1", 80)
                .SetIsHidden();
            //잉크 품목 구분
            grdInkInfo.View.AddComboBoxColumn("SPECDETAILFROM", 80, new SqlQuery("GetTypeList", "10001", "CODECLASSID=InkSpec", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("INKITEMCLASSIFY")
                .SetValidationIsRequired();

            //잉크명
            grdInkInfo.View.AddTextBoxColumn("DETAILNAME", 180)
                .SetLabel("INKNAME");
            //사용층
            grdInkInfo.View.AddComboBoxColumn("COLOR", 180, new SqlQuery("GetTypeList", "10001", "CODECLASSID=UserLayer", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("USERLAYER");
            //생성일자
            grdInkInfo.View.AddTextBoxColumn("CREATEDTIME", 100)
                .SetDisplayFormat("yyyy-MM-dd")
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("CREATEDATE");
            

            grdInkInfo.View.PopulateColumns();

            //부자재정보
            grdSubMaterial.GridButtonItem = GridButtonItem.Export | GridButtonItem.Add | GridButtonItem.Delete;
            grdSubMaterial.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            ////순번
            grdSubMaterial.View.AddTextBoxColumn("SEQUENCE2", 80)
                .SetIsHidden();
            //부자재 품목 구분
            grdSubMaterial.View.AddComboBoxColumn("CONSUMABLETYPE", 80, new SqlQuery("GetTypeList", "10001", "CODECLASSID=SubMaterialType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODEID")
                .SetLabel("SUBSIDIARYITEMCLASSIFY")
                .SetValidationIsRequired();

            //부자재명
            grdSubMaterial.View.AddTextBoxColumn("DETAILNAME", 100)
                .SetLabel("SUBSIDIARYNAME");
            //사용층
            grdSubMaterial.View.AddComboBoxColumn("SPECDETAILTO", 80, new SqlQuery("GetTypeList", "10001", "CODECLASSID=UserLayer", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("USERLAYER");
            //작업방법
            grdSubMaterial.View.AddComboBoxColumn("USERLAYER1", 80, new SqlQuery("GetTypeList", "10001", "CODECLASSID=AttachType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("WORKMETHOD");
            //생성일자
            grdSubMaterial.View.AddTextBoxColumn("CREATEDTIME", 100)
                .SetDisplayFormat("yyyy-MM-dd")
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("CREATEDATE");

            grdSubMaterial.View.PopulateColumns();


            //치공구
            grdFixture.GridButtonItem = GridButtonItem.Export | GridButtonItem.Add | GridButtonItem.Delete;
            grdFixture.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            ////순번
            grdFixture.View.AddTextBoxColumn("SEQUENCE3", 80)
                .SetIsHidden();
            // 종류
            grdFixture.View.AddComboBoxColumn("TOOLTYPE", 150, new SqlQuery("GetTypeList", "10001", "CODECLASSID=ProductSpecToolType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("CLASSIFY")
                .SetValidationIsRequired();
            // 작업방식
            grdFixture.View.AddComboBoxColumn("WORKMETHOD", 150, new SqlQuery("GetTypeList", "10001", "CODECLASSID=ProductSpecToolWorkType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("ISMAINSEGMENT");
            //구분
            grdFixture.View.AddComboBoxColumn("TOOLCLASS", 100, new SqlQuery("GetTypeList", "10001", "CODECLASSID=ProductSpecToolClassify", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("CLASS");
            //합수
            grdFixture.View.AddTextBoxColumn("SUMMARY", 100)
                .SetLabel("ARRAY");
            //REV
            grdFixture.View.AddTextBoxColumn("REVISION", 100)
                .SetLabel("TARGETKEY2");
            //SIZE
            grdFixture.View.AddTextBoxColumn("SIZEX", 100)
                .SetLabel("SIZE_X");
            grdFixture.View.AddTextBoxColumn("SIZEY", 100)
                .SetLabel("SIZE_Y");
            //제작처6
            grdFixture.View.AddComboBoxColumn("MANUFACTURER", 200, new SqlQuery("GetTypeList", "10001", "CODECLASSID=ProductSpecToolMaker", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("MANUFACTURER");
            //작업업체
            grdFixture.View.AddComboBoxColumn("VENDOR", 200, new SqlQuery("GetTypeList", "10001", "CODECLASSID=ProductSpecToolWorker", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("WORKINGVENDOR");

            //SCALE
            grdFixture.View.AddTextBoxColumn("SCALEX", 100)
                .SetLabel("SCALE_X");
            grdFixture.View.AddTextBoxColumn("SCALEY", 100)
                .SetLabel("SCALE_Y");

            //비고
            grdFixture.View.AddTextBoxColumn("DESCRIPTION", 200)
                .SetLabel("REMARK");

            grdFixture.View.PopulateColumns();

        }

		private void InitializeGrdLayerComposition_MaterialDefIdPopup()
		{
			//팝업 컬럼 설정
			var consumableDefId = grdLayerComposition.View.AddSelectPopupColumn("MATERIALDEF", new SqlQuery("GetConsumableDefList", "10003", "CONSUMABLECLASSID=RawMaterial", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
				.SetPopupLayout("CONSUMABLE", PopupButtonStyles.Ok_Cancel, true, false)
				.SetPopupResultCount(1)
				.SetLabel("MATERIALDEF")
				.SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
				.SetPopupAutoFillColumns("CONSUMABLEDEFNAME")
				.SetValidationKeyColumn()
				.SetPopupResultMapping("MATERIALDEF", "CONSUMABLEDEFID")
				.SetPopupApplySelection((selectedRows, dataGridRow) =>
				{
					DataRow row = selectedRows.FirstOrDefault();
					if(row == null) return;
					dataGridRow["MATERIALNAME"] = row["CONSUMABLEDEFNAME"].ToString();
					dataGridRow["MATERIALTYPE"] = row["CONSUMABLETYPE"].ToString();
				});

			consumableDefId.Conditions.AddTextBox("CONSUMABLEDEFID");
			consumableDefId.Conditions.AddTextBox("CONSUMABLEDEFNAME");

			// 팝업 그리드
			consumableDefId.GridColumns.AddTextBoxColumn("CONSUMABLEDEFID", 150);
			consumableDefId.GridColumns.AddTextBoxColumn("CONSUMABLEDEFNAME", 200);
			consumableDefId.GridColumns.AddTextBoxColumn("CONSUMABLETYPE", 200);
		}

		#endregion

		#region Event

		/// <summary>
		/// 화면에서 사용되는 이벤트를 초기화한다.
		/// </summary>
		public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가

            xtraTabPage1.SizeChanged += xtraTabPage1_SizeChanged;

			this.grdInkInfo.View.AddingNewRow += (s, e) =>
			{
				e.NewRow["SEQUENCE1"] = s.RowCount;
			};
			this.grdSubMaterial.View.AddingNewRow += (s, e) =>
			{
				e.NewRow["SEQUENCE2"] = s.RowCount;
			};
			this.grdFixture.View.AddingNewRow += (s, e) =>
			{
				e.NewRow["SEQUENCE3"] = s.RowCount;
			};

            this.grdLayerComposition.View.RowCellStyle += View_RowCellStyle;
        }


		/// <summary>
		/// 층간구성도  자재유형에 따른 색상 변경
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void View_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.Column.FieldName.Equals("CONSUMABLETYPE"))
                if (grdLayerComposition.View.GetRowCellValue(e.RowHandle, "CONSUMABLETYPE").ToString().Equals("FG"))    //FG = MainBase 일때만 색상변경
                    e.Appearance.BackColor = Color.Yellow;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xtraTabPage1_SizeChanged(object sender, EventArgs e)
        {
            etcInfo.Width = xtraTabPage1.Size.Width;
            circuitSpec.Width = xtraTabPage1.Size.Width;
            productSpec.Width = xtraTabPage1.Size.Width;
            productInfo.Width = xtraTabPage1.Size.Width;
        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

			//제품정보
			Dictionary<string, object> productPairs = productInfo.Save();

			//제품사양
			Dictionary<string, object> productSpecPairs = productSpec.Save();

			//회로사양
			DataTable dtCircuit = circuitSpec.Save();

			//기타정보
			DataTable dtThick = new DataTable();
			Dictionary<string, object> etcInfoPairs = etcInfo.Save(out dtThick);
						
            // 잉크정보
            DataTable dtInkInfo = this.grdInkInfo.GetChangedRows();

            // 부자재 정보
            DataTable dtSubMaterial = this.grdSubMaterial.GetChangedRows();

            //도금 정보
            DataTable dtPlatingInfo = platingSpec.Save();

            //치공구 정보 
            DataTable dtToolInfo = this.grdFixture.GetChangedRows();
            
            MessageWorker worker = new MessageWorker("SaveProductSpec");
            worker.SetBody(new MessageBody()
            {
                { "enterpriseId", UserInfo.Current.Enterprise },
                { "plantId", UserInfo.Current.Plant },
                { "productInfo", productPairs },
                { "productSpec", productSpecPairs },
                { "circuitSpec", dtCircuit },
                { "etcInfo", etcInfoPairs },
                { "thickSpec", dtThick },
                { "inkInfo",  dtInkInfo},
                { "subMaterial", dtSubMaterial},
                { "platingInfo", dtPlatingInfo},
                { "toolInfo", dtToolInfo },
                { "toolComment", Format.GetString(memoToolComment.EditValue).TrimEnd() }

            });
			worker.Execute();
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
			values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
			values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
			values.Add("ITEMID", Format.GetString(values["P_ITEMID"]).Split('|')[0]);
			values.Add("ITEMVERSION", Format.GetString(values["P_ITEMID"]).Split('|')[1]);


			DataTable dtProductSpec = await QueryAsync("SeletProductSpec", "10001", values);
			DataTable dtCircuit = await QueryAsync("SeletProductSpecDetail", "10001", values);
			DataTable dtThick = await QueryAsync("SeletProductThickSpec", "10001", values);
			DataTable dtComment = await QueryAsync("SeletProductSpecComment", "10001", values);


            DataTable dtMeterialInfo = await QueryAsync("SeletProductSpecDetail_Meterial", "10001", values);
            DataTable dtInkInfo = await QueryAsync("SeletProductSpecDetail_InkInfo", "10001", values);
            DataTable dtSubMeterial = await QueryAsync("SeletProductSpecDetail_SubMeterial", "10001", values);
            DataTable dtPlating = await QueryAsync("SeletProductSpecDetail_Plating", "10001", values);

            DataTable dtTool = await QueryAsync("SeletProductSpecDetail_ToolInfo", "10001", values);
            DataTable dtToolComment = await QueryAsync("SeletProductSpecDetail_ToolComment", "10001", values);

            if (dtProductSpec.Rows.Count < 1)
			{
				ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
			}
			else
			{
                productInfo.DataBind(dtProductSpec);
                productSpec.DataBind(dtProductSpec);
                circuitSpec.DataBind(dtCircuit);
				etcInfo.DataBind(dtProductSpec);
				etcInfo.DataBind(dtThick, true);
				etcInfo.DataBind(dtComment);

                this.grdLayerComposition.DataSource = dtMeterialInfo;
                this.grdInkInfo.DataSource = dtInkInfo;
                this.grdSubMaterial.DataSource = dtSubMeterial;

                platingSpec.DataBind(dtPlating);

                this.grdFixture.DataSource = dtTool;
                if (dtToolComment != null && dtToolComment.Rows.Count > 0)
                    memoToolComment.EditValue = dtToolComment.Rows[0]["COMMENT"];
                else
                    memoToolComment.EditValue = null;
            }

		}

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            //품목코드
            InitializeCondition_ProductPopup();
        }


        /// <summary>
		/// 팝업형 조회조건 생성 - 품목
		/// </summary>
		private void InitializeCondition_ProductPopup()
        {
            // SelectPopup 항목 추가
            var conditionProductId = Conditions.AddSelectPopup("P_ITEMID", new SqlQuery("GetItemMasterList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "ITEM", "ITEM")
                .SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupLayoutForm(800, 800)
                .SetPopupAutoFillColumns("ITEMNAME")
                .SetLabel("ITEMID")
                .SetPosition(4.2)
                .SetPopupResultCount(1);

            // 팝업에서 사용되는 검색조건 (품목코드/명)
            conditionProductId.Conditions.AddTextBox("TXTITEM");

            // 품목코드
            conditionProductId.GridColumns.AddTextBoxColumn("ITEMID", 150);
            // 품목명
            conditionProductId.GridColumns.AddTextBoxColumn("ITEMNAME", 200);
            // 품목버전
            conditionProductId.GridColumns.AddTextBoxColumn("ITEMVERSION", 100);
        }


        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

			//품목 필수조건 설정
			Conditions.GetCondition("P_ITEMID").IsRequired = true;

            Conditions.GetControl<SmartSelectPopupEdit>("P_ITEMID").EditValueChanged += (s, e) =>
            {
                // 데이터 초기화
                productInfo.ClearData();
                productSpec.ClearData();
                circuitSpec.ClearData();
                etcInfo.ClearData();
                platingSpec.ClearData();

                this.grdLayerComposition.DataSource = null;
                this.grdInkInfo.DataSource = null;
                this.grdSubMaterial.DataSource = null;
                this.grdFixture.DataSource = null;

                memoToolComment.EditValue = null;
            };

        }

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            this.grdFixture.View.CheckValidation();
            this.grdInkInfo.View.CheckValidation();
            this.grdLayerComposition.View.CheckValidation();
            this.grdSubMaterial.View.CheckValidation();


        }

        #endregion

        #region Private Function

        #endregion


    }

}
