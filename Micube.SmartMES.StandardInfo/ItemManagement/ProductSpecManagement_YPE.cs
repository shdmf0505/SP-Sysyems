#region using

using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;

using System;
using Micube.SmartMES.Commons.Controls;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프 로 그 램 명  : 기준정보 > 품목관리 > 품목사양정보
    /// 업  무  설  명  : 품목 사양 정보 등록
    /// 생    성    자  : 정승원
    /// 생    성    일  : 2019-12-16
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ProductSpecManagement_YPE : SmartConditionManualBaseForm
    {
		#region Local Variables

		//제품정보
		private ProductSpecManangementProductInfo_YPE productInfo { get; set; }
		//제품사양
		private ProductSpecManangementProductSpec_YPE productSpec { get; set; }
		//회로사양
		private ProductSpecManangementCircuitSpec_YPE circuitSpec { get; set; }
		//기타정보
		private ProductSpecManangementEtcInfo_YPE etcInfo { get; set; }
		//도금사양
		private ProductSpecManagementPlatingSpec_YPE platingSpec { get; set; }


        #endregion

        #region 생성자

        public ProductSpecManagement_YPE()
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

			productInfo = new ProductSpecManangementProductInfo_YPE();
			productSpec = new ProductSpecManangementProductSpec_YPE();
            circuitSpec = new ProductSpecManangementCircuitSpec_YPE();
			etcInfo = new ProductSpecManangementEtcInfo_YPE();
			platingSpec = new ProductSpecManagementPlatingSpec_YPE();

            InitializeEvent();
            InitializeTab();
            InitializeGrid();
        }

		/// <summary>
		/// Layout 초기화
		/// </summary>
        private void InitializeTab()
        {
            smartScrollableControlProductSpecInfo.Controls.Add(etcInfo);
            smartScrollableControlProductSpecInfo.Controls.Add(circuitSpec);
            smartScrollableControlProductSpecInfo.Controls.Add(productSpec);
            smartScrollableControlProductSpecInfo.Controls.Add(productInfo);

            productSpec.Dock = DockStyle.Top;
            productSpec.Height -= 15;
            productInfo.Dock = DockStyle.Top;
            productInfo.Height -= 15;
            circuitSpec.Dock = DockStyle.Top;
            //circuitSpec.Height -= 15;
            etcInfo.Dock = DockStyle.Top;
            etcInfo.Height -= 15;

            //productSpec.Width = smartScrollableControlProductSpecInfo.Width;
            //productInfo.Width = smartScrollableControlProductSpecInfo.Width;
            //circuitSpec.Width = smartScrollableControlProductSpecInfo.Width;
            //etcInfo.Width = smartScrollableControlProductSpecInfo.Width;


            pnlPlatingSpec.Controls.Add(platingSpec);
            platingSpec.Dock = DockStyle.Fill;
            platingSpec.Width = pnlPlatingSpec.Width;
            platingSpec.Margin = new Padding(-1);
        }

        /// <summary>        
        /// 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            #region 층간구성도

            grdLayerComposition.GridButtonItem = GridButtonItem.Export;
         //   grdLayerComposition.ButtonBarVisible(false);
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
            // 사용층
            grdLayerComposition.View.AddComboBoxColumn("USERLAYER", new SqlQuery("GetTypeList", "10001", "CODECLASSID=UserLayer", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            // 적용면
            grdLayerComposition.View.AddComboBoxColumn("WORKSURFACE", new SqlQuery("GetTypeList", "10001", "CODECLASSID=WorkSurface", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            // PNL SIZE
            grdLayerComposition.View.AddTextBoxColumn("PNLSIZE", 200);
            // 규격
            grdLayerComposition.View.AddTextBoxColumn("SPEC", 200);

            grdLayerComposition.View.PopulateColumns();

            #endregion

            #region Ink Spec

            grdInkSpec.GridButtonItem = GridButtonItem.Export | GridButtonItem.Add | GridButtonItem.Delete;
            grdInkSpec.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            ////순번
            grdInkSpec.View.AddTextBoxColumn("SEQUENCE1", 80)
                .SetIsHidden().SetLabel("MATERIALNAME");
            // 자재코드
            Initialize_MaterialDefIdPopup(grdInkSpec);
            // 자재명
            grdInkSpec.View.AddTextBoxColumn("DETAILNAME", 200)
                .SetIsReadOnly().SetLabel("MATERIALNAME");
			// 색상
			//grdInkSpec.View.AddColorEditColumn("COLOR", 100);
			grdInkSpec.View.AddComboBoxColumn("COLOR", 100, new SqlQuery("GetTypeList", "10001", "CODECLASSID=Color", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            
            // 적용면
            grdInkSpec.View.AddComboBoxColumn("FROMORIGINAL", new SqlQuery("GetTypeList", "10001", "CODECLASSID=WorkSurface", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("WORKSURFACE");
            // 규격
            grdInkSpec.View.AddTextBoxColumn("TOORIGINAL", 200)
                .SetLabel("SPEC");

            grdInkSpec.View.PopulateColumns();

            #endregion

            #region 보강판사양
            grdReinforcedPlate.GridButtonItem = GridButtonItem.Export | GridButtonItem.Add | GridButtonItem.Delete;
            grdReinforcedPlate.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            ////순번
            grdReinforcedPlate.View.AddTextBoxColumn("SEQUENCE2", 80)
                .SetIsHidden();
			// 자재품목구분
			grdReinforcedPlate.View.AddComboBoxColumn("CONSUMABLETYPE", 80, new SqlQuery("GetTypeList", "10001", "CODECLASSID=SubMaterialType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODEID")
             .SetValidationIsRequired();

            // 사용층1
            grdReinforcedPlate.View.AddComboBoxColumn("USERLAYER1", new SqlQuery("GetTypeList", "10001", "CODECLASSID=UserLayer", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            // 사용층2
            grdReinforcedPlate.View.AddComboBoxColumn("USERLAYER2", new SqlQuery("GetTypeList", "10001", "CODECLASSID=UserLayer", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            // 자재명
            grdReinforcedPlate.View.AddTextBoxColumn("MATERIALNAME", 150);
            // 부착방법
            grdReinforcedPlate.View.AddComboBoxColumn("SPECDETAILTO", new SqlQuery("GetTypeList", "10001", "CODECLASSID=AttachType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                .SetLabel("ATTACHTYPE");

            grdReinforcedPlate.View.PopulateColumns();


            #endregion

            #region 치공구

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

            #endregion

        }
   
        /// <summary>
        /// 자재코드 팝업 초기화
        /// </summary>
        private void Initialize_MaterialDefIdPopup(SmartBandedGrid grid)
		{
			var consumableDefId = grid.View.AddSelectPopupColumn("SPECDETAILFROM", new SqlQuery("GetConsumableDefList", "10003", "CONSUMABLECLASSID=RawMaterial", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
								.SetPopupLayout("CONSUMABLE", PopupButtonStyles.Ok_Cancel, true, false)
								.SetPopupResultCount(1)
								.SetLabel("MATERIALDEF")
								.SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
								.SetPopupAutoFillColumns("CONSUMABLEDEFNAME")
								.SetValidationKeyColumn()
								.SetPopupResultMapping("SPECDETAILFROM", "CONSUMABLEDEFID")
								.SetPopupApplySelection((selectedRows, dataGridRow) =>
								{
									DataRow row = selectedRows.FirstOrDefault();
									if(row == null) return;
									dataGridRow["SPECDETAILFROM"] = row["CONSUMABLEDEFID"].ToString();
                                    dataGridRow["DETAILNAME"] = row["CONSUMABLEDEFNAME"].ToString();
									//dataGridRow["MATERIALTYPE"] = row["CONSUMABLETYPE"].ToString();

								});

			consumableDefId.Conditions.AddTextBox("CONSUMABLEDEFID");

			consumableDefId.Conditions.AddTextBox("CONSUMABLEDEFNAME");

			// 팝업 그리드
			consumableDefId.GridColumns.AddTextBoxColumn("CONSUMABLEDEFID", 150);
			consumableDefId.GridColumns.AddTextBoxColumn("CONSUMABLEDEFNAME", 200);
			//consumableDefId.GridColumns.AddTextBoxColumn("CONSUMABLETYPE", 200);
		}

		#endregion

		#region Event

		/// <summary>
		/// 화면에서 사용되는 이벤트를 초기화한다.
		/// </summary>
		public void InitializeEvent()
        {
            this.grdInkSpec.View.AddingNewRow += (s, e) =>
            {
                e.NewRow["SEQUENCE1"] = s.RowCount;
            };
            this.grdReinforcedPlate.View.AddingNewRow += (s, e) =>
            {
                e.NewRow["SEQUENCE2"] = s.RowCount;
            };
            this.grdFixture.View.AddingNewRow += (s, e) =>
            {
                e.NewRow["SEQUENCE3"] = s.RowCount;
            };


        }


        #endregion

        #region 툴바

        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            SmartButton btn = sender as SmartButton;

            if (btn.Name.ToString().Equals("Print"))
            {
                if (productInfo.itemcodereturn().Equals("") || productInfo.itemcodereturn()==null)
                    return;

                DataSet dsReport = new DataSet();
                DataTable itemdata = new DataTable();



                #region 라벨

                itemdata.Columns.Add(new DataColumn("LBLINPUTDAY", typeof(string))); // 투입일
                itemdata.Columns.Add(new DataColumn("LBLOUTPUTDAY", typeof(string))); // 납품요청일
                itemdata.Columns.Add(new DataColumn("LBLITEMCODE", typeof(string))); // 품목코드
                itemdata.Columns.Add(new DataColumn("LBLITEMNAME", typeof(string))); // 품목코드
                itemdata.Columns.Add(new DataColumn("LBLFIRSTPRODUCTION", typeof(string))); // 초도양산
                itemdata.Columns.Add(new DataColumn("LBLLAYER", typeof(string))); // 층수
                itemdata.Columns.Add(new DataColumn("LBLPRODUCTDEFVERSION", typeof(string))); // 내부REV
                itemdata.Columns.Add(new DataColumn("LBLISACCEPT", typeof(string))); // 승인여부
                itemdata.Columns.Add(new DataColumn("LBLARRAY", typeof(string))); // 합수
                itemdata.Columns.Add(new DataColumn("LBLRCDIVISION", typeof(string))); // 변경구분
                itemdata.Columns.Add(new DataColumn("LBLPRODUCTLEVEL", typeof(string))); // 제품등급
                itemdata.Columns.Add(new DataColumn("LBLPRODUCTIONTYPE", typeof(string))); // 생산구분
                itemdata.Columns.Add(new DataColumn("LBLJOBTYPE", typeof(string))); // 작업구분
                itemdata.Columns.Add(new DataColumn("LBLARRAYPNL", typeof(string))); // ARRAY PNL
                itemdata.Columns.Add(new DataColumn("LBLCALCULATION", typeof(string))); // 산출수
                itemdata.Columns.Add(new DataColumn("LBLMAINFACTORY", typeof(string))); // 주 제조공장
                itemdata.Columns.Add(new DataColumn("LBLRTRSHEET", typeof(string))); // RTR SHEET
                itemdata.Columns.Add(new DataColumn("LBLPRODUCTSHAPE", typeof(string))); // 제품타입
                itemdata.Columns.Add(new DataColumn("LBLSITESPECPERSON", typeof(string))); // 사양담당자
                itemdata.Columns.Add(new DataColumn("LBLMEASUREMENT", typeof(string))); // 치수측정
                itemdata.Columns.Add(new DataColumn("LBLRELIABILITY", typeof(string))); // 신뢰성
                itemdata.Columns.Add(new DataColumn("LBLHAZARDOUSSUBSTANCES", typeof(string))); // 유해물질
                itemdata.Columns.Add(new DataColumn("LBLPSRTOLERANCE", typeof(string))); // PSR공차
                itemdata.Columns.Add(new DataColumn("LBLPLATINGSPEC", typeof(string))); // 도금사양
                itemdata.Columns.Add(new DataColumn("LBLOUTERCOPPERPLATING", typeof(string))); // 외층동도금
                itemdata.Columns.Add(new DataColumn("LBLINNERCOPPERPLATING1", typeof(string))); // 내층동도금1
                itemdata.Columns.Add(new DataColumn("LBLINNERCOPPERPLATING2", typeof(string))); // 내층동도금2
                itemdata.Columns.Add(new DataColumn("LBLINNERCOPPERPLATING3", typeof(string))); // 내층동도금3
                itemdata.Columns.Add(new DataColumn("LBLPLATINGCONDITION1", typeof(string))); // 도금조건1
                itemdata.Columns.Add(new DataColumn("LBLCUSTOMCONDITION1", typeof(string))); // 고객조건1
                itemdata.Columns.Add(new DataColumn("LBLPLATINGCONDITION2", typeof(string))); // 도금조건2
                itemdata.Columns.Add(new DataColumn("LBLCUSTOMCONDITION2", typeof(string))); // 고객조건2
                itemdata.Columns.Add(new DataColumn("LBLPLATINGCONDITION3", typeof(string))); // 도금조건3
                itemdata.Columns.Add(new DataColumn("LBLCUSTOMCONDITION3", typeof(string))); // 고객조건3
                itemdata.Columns.Add(new DataColumn("LBLPLATINGCONDITION4", typeof(string))); // 도금조건4
                itemdata.Columns.Add(new DataColumn("LBLCUSTOMCONDITION4", typeof(string))); // 고객조건4
                itemdata.Columns.Add(new DataColumn("LBLHOLEINSIDEWALL1", typeof(string))); // 홀내벽1
                itemdata.Columns.Add(new DataColumn("LBLPLANECUFOIL1", typeof(string))); // 면동박1
                itemdata.Columns.Add(new DataColumn("LBLDIMPLE1", typeof(string))); // DIMPLE1
                itemdata.Columns.Add(new DataColumn("LBLOVERFILL1", typeof(string))); // OVERFILL1
                itemdata.Columns.Add(new DataColumn("LBLHOLEINSIDEWALL2", typeof(string))); // 홀내벽2
                itemdata.Columns.Add(new DataColumn("LBLPLANECUFOIL2", typeof(string))); // 면동박2
                itemdata.Columns.Add(new DataColumn("LBLDIMPLE2", typeof(string))); // DIMPLE2
                itemdata.Columns.Add(new DataColumn("LBLOVERFILL2", typeof(string))); // OVERFILL2
                itemdata.Columns.Add(new DataColumn("LBLHOLEINSIDEWALL3", typeof(string))); // 홀내벽3
                itemdata.Columns.Add(new DataColumn("LBLPLANECUFOIL3", typeof(string))); // 면동박3
                itemdata.Columns.Add(new DataColumn("LBLDIMPLE3", typeof(string))); // DIMPLE3
                itemdata.Columns.Add(new DataColumn("LBLOVERFILL3", typeof(string))); // OVERFILL3
                itemdata.Columns.Add(new DataColumn("LBLHOLEINSIDEWALL4", typeof(string))); // 홀내벽4
                itemdata.Columns.Add(new DataColumn("LBLPLANECUFOIL4", typeof(string))); // 면동박4
                itemdata.Columns.Add(new DataColumn("LBLDIMPLE4", typeof(string))); // DIMPLE4
                itemdata.Columns.Add(new DataColumn("LBLOVERFILL4", typeof(string))); // OVERFILL4
                itemdata.Columns.Add(new DataColumn("LBLPSRPOSITION", typeof(string))); // PSR 좌표
                itemdata.Columns.Add(new DataColumn("LBLINNEROUTERLAYER", typeof(string))); // 내/외층 사용층
                itemdata.Columns.Add(new DataColumn("LBLBREADTHINTERVALORIGN", typeof(string))); // 폭/간격(원본)
                itemdata.Columns.Add(new DataColumn("LBLBREADTHINTERVALWORKING", typeof(string))); // 폭/간격(작업본)
                itemdata.Columns.Add(new DataColumn("LBLELONGATION1", typeof(string))); // 연신율1
                itemdata.Columns.Add(new DataColumn("LBLPITCHBEFORE1", typeof(string))); // 적용전 PITCH1
                itemdata.Columns.Add(new DataColumn("LBLPITCHAFTER1", typeof(string))); // 적용후 PITCH1
                itemdata.Columns.Add(new DataColumn("LBLELONGATION2", typeof(string))); // 연신율2
                itemdata.Columns.Add(new DataColumn("LBLPITCHBEFORE2", typeof(string))); // 적용전 PITCH2
                itemdata.Columns.Add(new DataColumn("LBLPITCHAFTER2", typeof(string))); // 적용후 PITCH2
                itemdata.Columns.Add(new DataColumn("LBLELONGATION3", typeof(string))); // 연신율3
                itemdata.Columns.Add(new DataColumn("LBLPITCHBEFORE3", typeof(string))); // 적용전 PITCH3
                itemdata.Columns.Add(new DataColumn("LBLPITCHAFTER3", typeof(string))); // 적용후 PITCH3
                itemdata.Columns.Add(new DataColumn("LBLHOLEPLATINGAREA1", typeof(string))); // HOLE 도금면적1
                itemdata.Columns.Add(new DataColumn("LBLHOLEPLATINGAREA2", typeof(string))); // HOLE 도금면적2
                itemdata.Columns.Add(new DataColumn("LBLSPECCHANGE", typeof(string))); // 사양변경내용
                itemdata.Columns.Add(new DataColumn("LBLCOMMENT", typeof(string))); // 특이사항
                itemdata.Columns.Add(new DataColumn("LBLINFORCEDPLATESPEC", typeof(string))); // 보강판사양
                itemdata.Columns.Add(new DataColumn("LBLCONSUMABLETYPE", typeof(string))); // 자재품목구분
                itemdata.Columns.Add(new DataColumn("LBLUSERLAYER1", typeof(string))); // 사용층1
                itemdata.Columns.Add(new DataColumn("LBLUSERLAYER2", typeof(string))); // 사용층2
                itemdata.Columns.Add(new DataColumn("LBLMATERIALNAME1", typeof(string))); // 자재명
                itemdata.Columns.Add(new DataColumn("LBLATTACHTYPE", typeof(string))); // 부착방법
                itemdata.Columns.Add(new DataColumn("LBLINKSPECIFICATION", typeof(string))); // 잉크사양
                itemdata.Columns.Add(new DataColumn("LBLMATERIALDEF1", typeof(string))); // 자재코드1
                itemdata.Columns.Add(new DataColumn("LBLLBLMATERIALNAME2", typeof(string))); // 자재명2
                itemdata.Columns.Add(new DataColumn("LBLCOLOR", typeof(string))); // 색상
                itemdata.Columns.Add(new DataColumn("LBLAPPLYSIDE", typeof(string))); // 적용면
                itemdata.Columns.Add(new DataColumn("LBLSPEC", typeof(string))); // 규격
                itemdata.Columns.Add(new DataColumn("LBLFLOORCOMPOSITION", typeof(string))); // 층 구성도
                itemdata.Columns.Add(new DataColumn("LBLMATERIALDEF2", typeof(string))); // 자재코드2
                itemdata.Columns.Add(new DataColumn("LBLMATERIALNAME3", typeof(string))); // 자재명3
                itemdata.Columns.Add(new DataColumn("LBLMATERIALTYPE", typeof(string))); // 자재유형
                itemdata.Columns.Add(new DataColumn("LBLUSERLAYER3", typeof(string))); // 사용층3
                itemdata.Columns.Add(new DataColumn("LBLWORKSURFACE", typeof(string))); // 제품작업면
                itemdata.Columns.Add(new DataColumn("LBLPNLSIZE", typeof(string))); // PNLSIZE
                itemdata.Columns.Add(new DataColumn("LBLSPEC2", typeof(string))); // 규격2
                itemdata.Columns.Add(new DataColumn("LBLCOMPONENTQTY", typeof(string))); // 소요량
                itemdata.Columns.Add(new DataColumn("CHANGECONTENT1", typeof(string))); // 사양변경내용1
                itemdata.Columns.Add(new DataColumn("CHANGECONTENT2", typeof(string))); // 사양변경내용2
                itemdata.Columns.Add(new DataColumn("CHANGECONTENT3", typeof(string))); // 사양변경내용3
                itemdata.Columns.Add(new DataColumn("CHANGECONTENT4", typeof(string))); // 사양변경내용4
                itemdata.Columns.Add(new DataColumn("CHANGECONTENT5", typeof(string))); // 사양변경내용5
                itemdata.Columns.Add(new DataColumn("COMMENT1", typeof(string))); // 특이사항
                itemdata.Columns.Add(new DataColumn("COMMENT2", typeof(string))); // 특이사항
                itemdata.Columns.Add(new DataColumn("COMMENT3", typeof(string))); // 특이사항
                itemdata.Columns.Add(new DataColumn("COMMENT4", typeof(string))); // 특이사항
                itemdata.Columns.Add(new DataColumn("COMMENT5", typeof(string))); // 특이사항
                #endregion

                #region row 생성 및 다국어 처리 

                DataRow row = itemdata.NewRow();

               
                row["LBLINPUTDAY"] = Language.Get("INPUTDAY"); // 투입일
                row["LBLOUTPUTDAY"] = Language.Get("DUEDATE"); // 납품요청일
                row["LBLITEMCODE"] = Language.Get("ITEMCODE"); // 품목코드
                row["LBLITEMNAME"] = Language.Get("ITEMNAME"); // 품목명
                row["LBLFIRSTPRODUCTION"] = Language.Get("FIRSTPRODUCTION"); // 초도양산
                row["LBLLAYER"] = Language.Get("LAYER"); // 층수
                row["LBLPRODUCTDEFVERSION"] = Language.Get("PRODUCTDEFVERSION"); // 내부 REV
                row["LBLISACCEPT"] = Language.Get("ISACCEPT"); // 승인여부
                row["LBLARRAY"] = Language.Get("ARRAY"); // 합수
                row["LBLRCDIVISION"] = Language.Get("RCDIVISION"); // 변경구분
                row["LBLPRODUCTLEVEL"] = Language.Get("PRODUCTLEVEL"); // 제품등급
                row["LBLPRODUCTIONTYPE"] = Language.Get("PRODUCTIONTYPE"); // 생산구분
                row["LBLJOBTYPE"] = Language.Get("JOBTYPE"); // 작업구분
                row["LBLARRAYPNL"] = Language.Get("ARRAYPNL"); // ARRAY/PNL
                row["LBLCALCULATION"] = Language.Get("CALCULATION"); // 산출수
                row["LBLMAINFACTORY"] = Language.Get("MAINFACTORY"); // 주 제조공장
                row["LBLRTRSHEET"] = Language.Get("RTRSHEET"); // ROLL SHEET
                row["LBLPRODUCTSHAPE"] = Language.Get("PRODUCTSHAPE"); // 제품타입
                row["LBLSITESPECPERSON"] = Language.Get("SITESPECPERSON"); // 사양담당자
                row["LBLMEASUREMENT"] = Language.Get("MEASUREMENT"); // 치수측정
                row["LBLRELIABILITY"] = Language.Get("RELIABILITY"); // 신뢰성
                row["LBLHAZARDOUSSUBSTANCES"] = Language.Get("HAZARDOUSSUBSTANCES"); // 유해물질
                row["LBLPSRTOLERANCE"] = Language.Get("PSRTOLERANCE"); // PSR 공차
                row["LBLPLATINGSPEC"] = Language.Get("PLATINGSPEC"); // 도금사양
                row["LBLOUTERCOPPERPLATING"] = Language.Get("OUTERCOPPERPLATING"); // 외층동도금
                row["LBLINNERCOPPERPLATING1"] = Language.Get("INNERCOPPERPLATING1"); // 내층동도금1
                row["LBLINNERCOPPERPLATING2"] = Language.Get("INNERCOPPERPLATING2"); //  내층동도금2
                row["LBLINNERCOPPERPLATING3"] = Language.Get("INNERCOPPERPLATING3"); //  내층동도금3
                row["LBLPLATINGCONDITION1"] = Language.Get("PLATINGCONDITION"); // 도금조건
                row["LBLCUSTOMCONDITION1"] = Language.Get("CUSTOMCONDITION"); // 고객조건
                row["LBLPLATINGCONDITION2"] = Language.Get("PLATINGCONDITION"); // 도금조건
                row["LBLCUSTOMCONDITION2"] = Language.Get("CUSTOMCONDITION"); // 고객조건
                row["LBLPLATINGCONDITION3"] = Language.Get("PLATINGCONDITION"); // 도금조건
                row["LBLCUSTOMCONDITION3"] = Language.Get("CUSTOMCONDITION"); // 고객조건
                row["LBLPLATINGCONDITION4"] = Language.Get("PLATINGCONDITION"); // 도금조건
                row["LBLCUSTOMCONDITION4"] = Language.Get("CUSTOMCONDITION"); // 고객조건
                row["LBLHOLEINSIDEWALL1"] = Language.Get("HOLEINSIDEWALL"); // 홀내벽
                row["LBLPLANECUFOIL1"] = Language.Get("PLANECUFOIL"); // 면동박
                row["LBLDIMPLE1"] = Language.Get("DIMPLE"); // DIMPLE
                row["LBLOVERFILL1"] = Language.Get("OVERFILL"); // OVER FILL
                row["LBLHOLEINSIDEWALL2"] = Language.Get("HOLEINSIDEWALL"); // 홀내벽
                row["LBLPLANECUFOIL2"] = Language.Get("PLANECUFOIL"); // 면동박
                row["LBLDIMPLE2"] = Language.Get("DIMPLE"); // DIMPLE
                row["LBLOVERFILL2"] = Language.Get("OVERFILL"); // OVER FILL
                row["LBLHOLEINSIDEWALL3"] = Language.Get("HOLEINSIDEWALL"); // 홀내벽
                row["LBLPLANECUFOIL3"] = Language.Get("PLANECUFOIL"); // 면동박
                row["LBLDIMPLE3"] = Language.Get("DIMPLE"); // DIMPLE
                row["LBLOVERFILL3"] = Language.Get("OVERFILL"); // OVER FILL
                row["LBLHOLEINSIDEWALL4"] = Language.Get("HOLEINSIDEWALL"); // 홀내벽
                row["LBLPLANECUFOIL4"] = Language.Get("PLANECUFOIL"); // 면동박
                row["LBLDIMPLE4"] = Language.Get("DIMPLE"); // DIMPLE
                row["LBLOVERFILL4"] = Language.Get("OVERFILL"); // OVER FILL
                row["LBLPSRPOSITION"] = Language.Get("PSRPOSITION"); // PSR좌표
                row["LBLINNEROUTERLAYER"] = Language.Get("INNEROUTERLAYER"); // 내/외층 사용층
                row["LBLBREADTHINTERVALORIGN"] = Language.Get("BREADTHINTERVALORIGN"); // 폭/간격(원본)
                row["LBLBREADTHINTERVALWORKING"] = Language.Get("BREADTHINTERVALWORKING"); // 폭/간격(작업본)
                row["LBLELONGATION1"] = Language.Get("ELONGATION"); // 연신율
                row["LBLPITCHBEFORE1"] = Language.Get("PITCHBEFORE"); // 적용전 PITCH
                row["LBLPITCHAFTER1"] = Language.Get("PITCHAFTER"); // 적용후 PITCH
                row["LBLELONGATION2"] = Language.Get("ELONGATION"); // 연신율
                row["LBLPITCHBEFORE2"] = Language.Get("PITCHBEFORE"); // 적용전 PITCH
                row["LBLPITCHAFTER2"] = Language.Get("PITCHAFTER"); // 적용후 PITCH
                row["LBLELONGATION3"] = Language.Get("ELONGATION"); // 연신율
                row["LBLPITCHBEFORE3"] = Language.Get("PITCHBEFORE"); // 적용전 PITCH
                row["LBLPITCHAFTER3"] = Language.Get("PITCHAFTER"); // 적용후 PITCH
                row["LBLHOLEPLATINGAREA1"] = Language.Get("HOLEPLATINGAREA1"); // HOLE도금면적1
                row["LBLHOLEPLATINGAREA2"] = Language.Get("HOLEPLATINGAREA2"); // HOLE도금면적2
                row["LBLSPECCHANGE"] = Language.Get("SPECCHANGE"); // 사양변경내용
                row["LBLCOMMENT"] = Language.Get("COMMENT"); // 특이사항
                row["LBLINFORCEDPLATESPEC"] = Language.Get("INFORCEDPLATESPEC"); // 보강판사양
                row["LBLCONSUMABLETYPE"] = Language.Get("CONSUMABLETYPE"); // 자재품목구분
                row["LBLUSERLAYER1"] = Language.Get("USERLAYER1"); // 사용층1
                row["LBLUSERLAYER2"] = Language.Get("USERLAYER2"); // 사용층2
                row["LBLMATERIALNAME1"] = Language.Get("MATERIALNAME"); // 자재명
                row["LBLATTACHTYPE"] = Language.Get("ATTACHTYPE"); // 부착방법
                row["LBLINKSPECIFICATION"] = Language.Get("INKSPECIFICATION"); // 잉크사양
                row["LBLMATERIALDEF1"] = Language.Get("MATERIALDEF"); // 자재코드
                row["LBLLBLMATERIALNAME2"] = Language.Get("MATERIALNAME"); // 자재명
                row["LBLCOLOR"] = Language.Get("COLOR"); // 색상
                row["LBLAPPLYSIDE"] = Language.Get("APPLYSIDE"); // 적용면
                row["LBLSPEC"] = Language.Get("SPEC"); // 규격
                row["LBLFLOORCOMPOSITION"] = Language.Get("FLOORCOMPOSITION"); // 층구성도
                row["LBLMATERIALDEF2"] = Language.Get("MATERIALDEF"); // 자재코드
                row["LBLMATERIALNAME3"] = Language.Get("MATERIALNAME"); // 자재명
                row["LBLMATERIALTYPE"] = Language.Get("MATERIALTYPE"); // 자재유형
                row["LBLUSERLAYER3"] = Language.Get("USERLAYER"); // 사용층
                row["LBLWORKSURFACE"] = Language.Get("WORKSURFACE"); // 제품작업면
                row["LBLPNLSIZE"] = Language.Get("PNLSIZE"); // PNLSIZE
                row["LBLSPEC"] = Language.Get("SPEC"); // 규격
                row["LBLCOMPONENTQTY"] = Language.Get("COMPONENTQTY"); // 소요량
                row["COMMENT1"] = etcInfo.comment1().ToString();
                row["COMMENT2"] = etcInfo.comment2().ToString();
                row["COMMENT3"] = etcInfo.comment3().ToString();
                row["COMMENT4"] = etcInfo.comment4().ToString();
                row["COMMENT5"] = etcInfo.comment5().ToString();
                row["CHANGECONTENT1"] = etcInfo.change1().ToString();
                row["CHANGECONTENT2"] = etcInfo.change2().ToString();
                row["CHANGECONTENT3"] = etcInfo.change3().ToString();
                row["CHANGECONTENT4"] = etcInfo.change4().ToString();
                row["CHANGECONTENT5"] = etcInfo.change5().ToString();
                itemdata.Rows.Add(row);

                #endregion 라벨

                //출력을 위한 데이터 테이블 생성
                DataTable circledt = circuitSpec.Gridcircuitreturn();
                DataTable circledttable = circledt.Copy();
                circledttable.TableName = "circledt";
                DataTable layer = grdLayerComposition.DataSource as DataTable; 
                DataTable ink = grdInkSpec.DataSource as DataTable;
                DataTable rein = grdReinforcedPlate.DataSource as DataTable;
                DataTable sumdt = new DataTable(); 

                // 데이터셋에 각 화면에서 가져온 값들을 넣는다
                DataSet ds = new DataSet();
                ds.Tables.Add(platingSpec.Specreturn());
                ds.Tables.Add(circuitSpec.PSRreturn());
                ds.Tables.Add(itemdata);
                ds.Tables.Add(productInfo.productinforeturn());
                ds.Tables.Add(productSpec.productspecreturn());
                ds.Tables.Add(circledttable);

                // merge를 쓰면 로우가 불필요하게 늘어나기 때문에 아래 함수를 이용하여 컬럼과 데이터를 합쳐서 sumdt 테이블에 넣는다
                sumdt = ColumnAdd(sumdt, ds);
                sumdt = DataAdd(sumdt, ds);

                //작성한 리포트
                Assembly assembly = Assembly.GetAssembly(this.GetType());
                Stream stream = assembly.GetManifestResourceStream("Micube.SmartMES.StandardInfo.Report.ItemSpecInfo.repx");
                XtraReport report = XtraReport.FromStream(stream);
                
 
                //그리드의 데이터가 이미 다른 Dataset에 포함되어있기때문에 새로운 DataTable을 만들어 Copy 한다.
                DataTable reintable = rein.Copy();
                DataTable layertable = layer.Copy();
                DataTable inktalbe = ink.Copy();


                //릴레이션을 하기위한 컬럼추가
                layertable.Columns.Add(new DataColumn("ITEMCODE", typeof(string))); 
                for(int i=0;i<layertable.Rows.Count;i++)
                {
                    layertable.Rows[i]["ITEMCODE"] = sumdt.Rows[0]["ITEMCODE"];
                }
                //프린터에서 두개의 그리드를 합쳐야만 하기 때문에 합치는 데이터 테이블 생성
                DataTable inkrein = new DataTable();
                ds = new DataSet();

                inkrein.TableName = "inkrein";
                reintable.TableName = "rein";
                layertable.TableName = "layer";
                inktalbe.TableName = "ink";

                //컬럼을 합칠때 두 테이블에 같은 컬럼을 제거하고 릴레이션은  로우가 많은 테이블을 기준으로 해야하기때문에 작은 테이블에 컬럼을 제거한다.
                if(inktalbe.Rows.Count> reintable.Rows.Count)
                {
                    reintable.Columns.RemoveAt(0);
                    reintable.Columns.RemoveAt(0);
                    reintable.Columns.RemoveAt(0);
                    reintable.Columns.Remove("CREATEDTIME");
                }
                else
                {
                    inktalbe.Columns.RemoveAt(0);
                    inktalbe.Columns.RemoveAt(0);
                    inktalbe.Columns.RemoveAt(0);
                    inktalbe.Columns.Remove("CREATEDTIME");
                }
   
                ds.Tables.Add(inktalbe);
                ds.Tables.Add(reintable);
                inkrein = ColumnAdd(inkrein, ds);
                inkrein = DataAdd(inkrein, ds);

                sumdt.TableName = "label";
                dsReport.Tables.Add(sumdt);
                dsReport.Tables.Add(inkrein);
                dsReport.Tables.Add(layertable);
                

                if (inkrein.Rows.Count > 0)
                {
                    DataRelation relation = new DataRelation("rein", sumdt.Columns["ITEMCODE"], inkrein.Columns["ITEMID"]);
                    dsReport.Relations.Add(relation);
                }

                if(layertable.Rows.Count>0)
                {
                    DataRelation relation2 = new DataRelation("layer", sumdt.Columns["ITEMCODE"], layertable.Columns["ITEMCODE"]);
                    dsReport.Relations.Add(relation2);
                }

                report.DataSource = dsReport;
                report.DataMember = "label";


                Band headerPage = report.Bands["Detail"];  // 제목 및 사진
                SetReportControlDataBinding(headerPage.Controls, sumdt);


                DetailReportBand detailReport4 = report.Bands["DetailReport3"] as DetailReportBand; // 상세정보 및 특이사항 및 자재 컬럼
                detailReport4.DataSource = dsReport;
                detailReport4.DataMember = "label";

                Band detailBand4 = detailReport4.Bands["Detail4"]; 
                SetReportControlDataBinding(detailBand4.Controls, sumdt);


                DetailReportBand detailReport5 = report.Bands["DetailReport"] as DetailReportBand; // 자재컬럼
                detailReport5.DataSource = dsReport;
                detailReport5.DataMember = "label";

                Band detailBand5 = detailReport5.Bands["GroupHeader1"];
                SetReportControlDataBinding(detailBand5.Controls, sumdt);


                DetailReportBand detailReport = report.Bands["DetailReport"] as DetailReportBand;  //자재 그리드
                detailReport.DataSource = dsReport;
                detailReport.DataMember = "rein";

                Band detailBand = detailReport.Bands["Detail1"];
                SetReportControlDataBinding2(detailBand.Controls, dsReport, "rein");


                DetailReportBand detailReport2 = report.Bands["DetailReport2"] as DetailReportBand; //층구성도 컬럼
                detailReport2.DataSource = dsReport;
                detailReport2.DataMember = "label";

                Band detailBand2 = detailReport2.Bands["GroupHeader2"];
                SetReportControlDataBinding(detailBand2.Controls, sumdt);


                DetailReportBand detailReport3 = report.Bands["DetailReport2"] as DetailReportBand; // 층구성도 그리드
                detailReport3.DataSource = dsReport;
                detailReport3.DataMember = "layer";

                Band detailBand3 = detailReport3.Bands["Detail3"];
                SetReportControlDataBinding2(detailBand3.Controls, dsReport, "layer");


                ReportPrintTool printTool = new ReportPrintTool(report);
                printTool.ShowRibbonPreview();

			}
            else if (btn.Name.ToString().Equals("Save"))
            {
                base.OnToolbarSaveClick();

                //제품정보
                Dictionary<string, object> productPairs = productInfo.Save();

                //제품사양
                Dictionary<string, object> productSpecPairs = productSpec.Save();

                //회로사양
                Dictionary<string, object> psrInfo;
                DataTable dtCircuit = circuitSpec.Save(out psrInfo);

                //기타정보
                Dictionary<string, object> etcInfoPairs = etcInfo.Save();

                //도금 정보
                DataTable dtPlatingInfo = platingSpec.Save();

                // 잉크정보
                DataTable dtInkInfo = this.grdInkSpec.GetChangedRows();

                // 부자재 정보
                DataTable dtSubMaterial = this.grdReinforcedPlate.GetChangedRows();

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
					{ "psrInfo", psrInfo },
					{ "etcInfo", etcInfoPairs },
					{ "inkInfo",  dtInkInfo},
					{ "subMaterial", dtSubMaterial},
					{ "platingInfo", dtPlatingInfo},
					{ "toolInfo", dtToolInfo },
					{ "toolComment", Format.GetString(memoToolComment.EditValue).TrimEnd() }

				});
                worker.Execute();
                ShowMessage("SuccessSave");
                OnSearchAsync();
            }


        }

         #endregion


        private void SetReportControlDataBinding(XRControlCollection controls, DataTable dataSource) // 노 릴레이션 데이터 바인딩
        {
            if (controls.Count > 0)
            {
                foreach (XRControl control in controls)
                {
                    if (!string.IsNullOrWhiteSpace(control.Tag.ToString()))
                        control.DataBindings.Add("Text", dataSource, control.Tag.ToString());

                    SetReportControlDataBinding(control.Controls, dataSource);
                }
            }
        }

        private static void SetReportControlDataBinding2(XRControlCollection controls, DataSet dataSource, string relationId) // 릴레이션 데이터 바인딩
        {
            if (controls.Count > 0)
            {
                foreach (XRControl control in controls)
                {
                    if (!string.IsNullOrWhiteSpace(control.Tag.ToString()))
                    {
                        string fieldName = "";

                        if (!string.IsNullOrWhiteSpace(relationId))
                            fieldName = string.Join(".", relationId, control.Tag.ToString());
                        else
                            fieldName = control.Tag.ToString();

                        control.DataBindings.Add("Text", dataSource, fieldName);
                    }

                    SetReportControlDataBinding2(control.Controls, dataSource, relationId);
                }
            }
        }




        private DataTable ColumnAdd(DataTable table, DataSet ds) // 컬럼 MERGE용 
        {
            for (int i = 0; i < ds.Tables.Count; i++)
            {
                for (int j = 0; j < ds.Tables[i].Columns.Count; j++)
                {

                    table.Columns.Add(new DataColumn(ds.Tables[i].Columns[j].ToString(), typeof(string)));
                }
            }
            return table;
        }

        private DataTable DataAdd(DataTable table, DataSet ds)  // DATA MERGE용 
        {
            for (int i = 0; i < ds.Tables.Count; i++)
            {
                DataTable tablename = ds.Tables[i];

                for (int j = 0; j < tablename.Rows.Count; j++)
                {
                    if (table.Rows.Count < tablename.Rows.Count && j>table.Rows.Count-1)
                    {
                        DataRow dr = table.NewRow();
                        for (int k = 0; k < tablename.Columns.Count; k++)
                        {
                            dr[tablename.Columns[k].ToString()] = tablename.Rows[j][tablename.Columns[k].ToString()];

                        }
                        table.Rows.Add(dr);

                    }
                    else
                    {
                        for (int k = 0; k < tablename.Columns.Count; k++)
                        {
                            table.Rows[j][tablename.Columns[k].ToString()] = tablename.Rows[j][tablename.Columns[k].ToString()];
                        }

                    }

                }
            }
            return table;
        }

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

			DataTable dtProductSpec = SqlExecuter.Query("SeletProductSpec", "10001", values);
			DataTable dtCircuit = SqlExecuter.Query("SeletProductSpecDetail", "10002", values);
			DataTable dtComment = SqlExecuter.Query("SeletProductSpecComment", "10001", values);

            DataTable dtMeterialInfo = SqlExecuter.Query("SeletProductSpecDetail_Meterial", "10001", values); //층 구성도
            DataTable dtInkInfo = SqlExecuter.Query("SeletProductSpecDetail_InkInfo", "10001", values); // 잉크사양
            DataTable dtSubMeterial = SqlExecuter.Query("SeletProductSpecDetail_SubMeterial", "10001", values); // 보강판사양
            DataTable dtPlating = SqlExecuter.Query("SeletProductSpecDetail_Plating", "10001", values); 

            DataTable dtTool = SqlExecuter.Query("SeletProductSpecDetail_ToolInfo", "10001", values);
            DataTable dtToolComment = SqlExecuter.Query("SeletProductSpecDetail_ToolComment", "10001", values);
            

            if (dtProductSpec.Rows.Count < 1)
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }
			else
			{
				productInfo.DataBind(dtProductSpec);
				productSpec.DataBind(dtProductSpec);
				circuitSpec.DataBind(dtCircuit, dtProductSpec);
				etcInfo.DataBind(dtProductSpec);
				etcInfo.DataBind(dtComment);

                this.grdLayerComposition.DataSource = dtMeterialInfo;
                this.grdInkSpec.DataSource = dtInkInfo;
                this.grdReinforcedPlate.DataSource = dtSubMeterial;

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
                this.grdInkSpec.DataSource = null;
                this.grdReinforcedPlate.DataSource = null;
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
            this.grdInkSpec.View.CheckValidation();
            this.grdLayerComposition.View.CheckValidation();
            this.grdReinforcedPlate.View.CheckValidation();


        }

        #endregion

        #region Private Function

        #endregion
    }

}
