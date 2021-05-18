#region using


using DevExpress.XtraReports.UI;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons.Controls;
using System;
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
    public partial class ProductSpecification_YPE : SmartConditionManualBaseForm
    {

        #region Local Variables

        //제품정보
        private ProductSpecificationProductInfo_YPE productInfo { get; set; }
        //제품사양
        private ProductSpecificationProductSpec_YPE productSpec { get; set; }
        //회로사양
        private ProductSpecificationCircuitSpec_YPE circuitSpec { get; set; }
        //기타정보
        private ProductSpecificationEtcInfo_YPE etcInfo { get; set; }
        //도금사양
        private ProductSpecificationPlatingSpec_YPE platingSpec { get; set; }
        //도금사양
        private ProductSpecificationSurfaceSpec_YPE surfaceSpec { get; set; }

        #endregion

        #region 생성자

        public ProductSpecification_YPE()
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
                _parameters = parameters;
                string productDefId = parameters.ContainsKey("ITEMID") ? Format.GetString(parameters["ITEMID"]) : Format.GetString(parameters["PRODUCTDEFID"]);
                string productDefVersion = parameters.ContainsKey("ITEMVERSION") ? Format.GetString(parameters["ITEMVERSION"]) : Format.GetString(parameters["PRODUCTREVISION"]);
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

            productInfo = new ProductSpecificationProductInfo_YPE();
            productSpec = new ProductSpecificationProductSpec_YPE();
            circuitSpec = new ProductSpecificationCircuitSpec_YPE();
            etcInfo = new ProductSpecificationEtcInfo_YPE();
            platingSpec = new ProductSpecificationPlatingSpec_YPE();
            surfaceSpec = new ProductSpecificationSurfaceSpec_YPE();
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
            pnlsurfacespec.Controls.Add(surfaceSpec);
            surfaceSpec.Dock = DockStyle.Fill;
            surfaceSpec.Width = pnlsurfacespec.Width;
            surfaceSpec.Margin = new Padding(-1);

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

            //자재유형
            grdLayerComposition.View.AddComboBoxColumn("CONSUMABLETYPE", 75, new SqlQuery("GetTypeList", "10001", "CODECLASSID=MaterialDetailType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("MATERIALTYPE");
            // 자재코드
            //InitializeGrdLayerComposition_MaterialDefIdPopup();
            grdLayerComposition.View.AddTextBoxColumn("ASSEMBLYITEMID", 200)
             .SetLabel("MATERIALDEF")
             .SetValidationIsRequired();
            // 자재명
            grdLayerComposition.View.AddTextBoxColumn("BOTASSEMBLYITEMNAME", 200)
                .SetLabel("MATERIALNAME");
            // 규격
            grdLayerComposition.View.AddTextBoxColumn("SPEC", 100);

            // PNL SIZE X
            grdLayerComposition.View.AddTextBoxColumn("PNLSIZEXAXIS", 80)
                .SetTextAlignment(TextAlignment.Center);
            // PNL SIZE Y
            grdLayerComposition.View.AddTextBoxColumn("PNLSIZEYAXIS", 80)
                .SetTextAlignment(TextAlignment.Center);
            // PNL SIZE 
            grdLayerComposition.View.AddTextBoxColumn("PNLSIZE", 80)
                .SetIsHidden()
                .SetTextAlignment(TextAlignment.Center);

            // 배열수
            grdLayerComposition.View.AddTextBoxColumn("ARRAYPCS", 70)
                .SetTextAlignment(TextAlignment.Center);
            // 산출수
            grdLayerComposition.View.AddTextBoxColumn("CALCULATEPCS", 70)
                .SetTextAlignment(TextAlignment.Center);
            // 소요량
            grdLayerComposition.View.AddTextBoxColumn("REQUIREMENTQTY", 120)
                .SetTextAlignment(TextAlignment.Center);


            // 사용층
            grdLayerComposition.View.AddTextBoxColumn("USERLAYER", 80)
                .SetTextAlignment(TextAlignment.Center);
            // 제조업체
            grdLayerComposition.View.AddTextBoxColumn("MAKER", 120);

            // 잉크품목구분
            grdLayerComposition.View.AddTextBoxColumn("INKTYPE", 100);
            // 작업방법
            grdLayerComposition.View.AddTextBoxColumn("WORKMETHOD", 80)
                .SetTextAlignment(TextAlignment.Center);

            // 비고
            grdLayerComposition.View.AddTextBoxColumn("DESCRIPTION", 150)
                .SetLabel("REMARK");

            grdLayerComposition.View.PopulateColumns();

            #endregion


            #region 치공구

            //BBT JIG
            grdtoollist.GridButtonItem = GridButtonItem.Export | GridButtonItem.Add | GridButtonItem.Delete;
            grdtoollist.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;


            //품목ID
            grdtoollist.View.AddTextBoxColumn("PRODUCTDEFID", 110);
            //품목버전
            grdtoollist.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();
            //품목명
            grdtoollist.View.AddTextBoxColumn("PRODUCTDEFNAME", 300)
                .SetIsReadOnly();
            //구분
            grdtoollist.View.AddTextBoxColumn("TOOLCLASS", 100)
                .SetLabel("CLASS")
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();
            //Tool 유형1
            grdtoollist.View.AddTextBoxColumn("TOOLTYPE", 100)
                .SetLabel("TOOLCATEGORYDETAIL")
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();
            //Tool 유형2
            grdtoollist.View.AddTextBoxColumn("TOOLDETAILTYPE", 100)
                .SetLabel("TOOLDETAIL")
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();
            // Tool 형식
            grdtoollist.View.AddTextBoxColumn("TOOLFORM", 150)
                .SetLabel("TOOLFORMCODE")
                .SetIsReadOnly();
            //합수
            grdtoollist.View.AddTextBoxColumn("SUMMARY", 100)
                .SetIsReadOnly()
                .SetLabel("ARRAY");
            //사용층
            grdtoollist.View.AddTextBoxColumn("LAYER", 80)
                .SetLabel("USERLAYER")
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();
            //타수 PNL
            grdtoollist.View.AddTextBoxColumn("HITCOUNT", 100)
                .SetIsReadOnly();
            //치공구 ID
            grdtoollist.View.AddTextBoxColumn("DURABLEDEFID", 100)
                .SetIsReadOnly();
            //치공구명
            grdtoollist.View.AddTextBoxColumn("DURABLEDEFNAME", 300)
                .SetIsReadOnly();
            //치공구 VERSION
            grdtoollist.View.AddTextBoxColumn("DURABLEDEFVERSION", 100)
                .SetIsHidden()
                .SetIsReadOnly();
            //비고
            grdtoollist.View.AddTextBoxColumn("DESCRIPTION", 200)
                .SetIsReadOnly()
                .SetLabel("REMARK");


            grdtoollist.View.PopulateColumns();




            //기타
            grdetclist.GridButtonItem = GridButtonItem.Export | GridButtonItem.Add | GridButtonItem.Delete;
            grdetclist.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            //구분
            grdetclist.View.AddComboBoxColumn("DURABLECLASSID", 100, new SqlQuery("GetTypeList", "10001", "CODECLASSID=DurableClass", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("CLASS");
            //Tool 유형1  
            grdetclist.View.AddComboBoxColumn("TOOLTYPE", 100, new SqlQuery("GetToolTypeByClassId", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("TOOLCATEGORYDETAIL")
                .SetTextAlignment(TextAlignment.Center)
                .SetRelationIds("DURABLECLASSID");
            //Tool 유형2
            grdetclist.View.AddComboBoxColumn("TOOLDETAILTYPE", 100, new SqlQuery("GetDurableClassType2", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetRelationIds("DURABLECLASSID", "TOOLTYPE")
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("TOOLDETAIL");
            // Tool 형식
            grdetclist.View.AddComboBoxColumn("TOOLFORM", 150, new SqlQuery("GetTypeList", "10001", "CODECLASSID=ToolForm", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("TOOLFORMCODE");
            //합수
            grdetclist.View.AddTextBoxColumn("SUMMARY", 100)
                .SetLabel("ARRAY");
            //사용층
            grdetclist.View.AddComboBoxColumn("USERLAYER", 100, new SqlQuery("GetTypeList", "10001", "CODECLASSID=FilmUseLayer1", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetTextAlignment(TextAlignment.Center);
            //타수 PNL
            grdetclist.View.AddTextBoxColumn("HITCOUNT", 100);
            //비고
            grdetclist.View.AddTextBoxColumn("DESCRIPTION", 200)
                .SetLabel("REMARK");

            //치공구 유형
            grdetclist.View.AddComboBoxColumn("DURABLECLASS", 100, new SqlQuery("GetTypeList", "10001", "CODECLASSID=ItemToolType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsHidden()
                .SetDefault("MOLDETC")
                .SetLabel("DURABLETYPE");
            //SEQUENCE
            grdetclist.View.AddTextBoxColumn("SEQUENCE3", 80)
                .SetIsReadOnly()
                .SetIsHidden();

            grdetclist.View.PopulateColumns();


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
                                    if (row == null) return;
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


            this.grdetclist.View.AddingNewRow += (s, e) =>
            {
                e.NewRow["SEQUENCE3"] = s.RowCount;
            };

        }


        #endregion

        #region 툴바

        protected override void OnToolbarSaveClick()
        {



        }

        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            SmartButton btn = sender as SmartButton;

            if (btn.Name.ToString().Equals("Print"))
            {

                if (productInfo.Itemcodereturn().Equals("") || productInfo.Itemcodereturn() == null)
                    return;

                DataSet dsReport = new DataSet();
                DataTable itemdata = new DataTable();


                #region 라벨

                itemdata.Columns.Add(new DataColumn("LBLCUSTOMERREV", typeof(string))); // 고객 REV
                itemdata.Columns.Add(new DataColumn("LBLCUSTOMERID", typeof(string))); // 고객 ID
                itemdata.Columns.Add(new DataColumn("LBLITEMCODE", typeof(string))); // 품목코드
                itemdata.Columns.Add(new DataColumn("LBLITEMNAME", typeof(string))); // 품목코드
                itemdata.Columns.Add(new DataColumn("LBLCUSTOMER", typeof(string))); // 고객명
                itemdata.Columns.Add(new DataColumn("LBLLAYER", typeof(string))); // 층수
                itemdata.Columns.Add(new DataColumn("LBLPRODUCTDEFVERSION", typeof(string))); // 내부REV
                itemdata.Columns.Add(new DataColumn("LBLBASICCODE", typeof(string))); // 모델코드
                itemdata.Columns.Add(new DataColumn("LBLQRBUSINESSINFO", typeof(string))); // QR 사업부정보
                itemdata.Columns.Add(new DataColumn("LBLQRBUSINESSSUB", typeof(string))); // QR 사업부 SUB
                itemdata.Columns.Add(new DataColumn("LBLPRODUCTLEVEL", typeof(string))); // 제품등급
                itemdata.Columns.Add(new DataColumn("LBLPRODUCTIONTYPE", typeof(string))); // 생산구분
                itemdata.Columns.Add(new DataColumn("LBLQRPRODUCTIONTYPE", typeof(string))); // QR 생산구분
                itemdata.Columns.Add(new DataColumn("LBLQRMATERIALREV", typeof(string))); // QR 자재REV
                itemdata.Columns.Add(new DataColumn("LBLSALESMAN", typeof(string))); // 영업담당자
                itemdata.Columns.Add(new DataColumn("LBLMAINFACTORY", typeof(string))); // 주 제조공장
                itemdata.Columns.Add(new DataColumn("LBLRTRSHEET", typeof(string))); // RTR SHEET
                itemdata.Columns.Add(new DataColumn("LBLPRODUCTSHAPE", typeof(string))); // 제품타입
                itemdata.Columns.Add(new DataColumn("LBLSITESPECPERSON", typeof(string))); // 사양담당자
                itemdata.Columns.Add(new DataColumn("LBLMEASUREMENT", typeof(string))); // 치수측정
                itemdata.Columns.Add(new DataColumn("LBLRELIABILITY", typeof(string))); // 신뢰성
                itemdata.Columns.Add(new DataColumn("LBLHAZARDOUSSUBSTANCES", typeof(string))); // 유해물질
                itemdata.Columns.Add(new DataColumn("LBLPSRTOLERANCE", typeof(string))); // PSR공차
                itemdata.Columns.Add(new DataColumn("LBLPLATINGSPEC", typeof(string))); // 도금사양
                itemdata.Columns.Add(new DataColumn("LBLIMPEDANCECHECK", typeof(string))); // 임피던스 유무
                itemdata.Columns.Add(new DataColumn("LBLIMPEDANCESPEC2", typeof(string))); // 임피던스 SPEC
                itemdata.Columns.Add(new DataColumn("LBLIMPEDANCETYPE2", typeof(string))); // 임피던스 구분
                itemdata.Columns.Add(new DataColumn("LBLIMPEDANCELAYER", typeof(string))); // 임피던스 적용층
                itemdata.Columns.Add(new DataColumn("LBLPSRPOSITION", typeof(string))); // PSR 좌표
                itemdata.Columns.Add(new DataColumn("LBLELONGATIONCHECK", typeof(string))); // 연신율 유무
                itemdata.Columns.Add(new DataColumn("LBLELONGATION-1", typeof(string))); // 연신율 1
                itemdata.Columns.Add(new DataColumn("LBLELONGATION-2", typeof(string))); // 연신율 2
                itemdata.Columns.Add(new DataColumn("LBLELONGATION-3", typeof(string))); // 연신율 3
                itemdata.Columns.Add(new DataColumn("LBLPITCHBEFORE1", typeof(string))); // 적용전 PITCH1
                itemdata.Columns.Add(new DataColumn("LBLPITCHBEFORE2", typeof(string))); // 적용전 PITCH2
                itemdata.Columns.Add(new DataColumn("LBLPITCHBEFORE3", typeof(string))); // 적용전 PTICH3
                itemdata.Columns.Add(new DataColumn("LBLPITCHAFTER1", typeof(string))); // 적용후 PITCH1
                itemdata.Columns.Add(new DataColumn("LBLPITCHAFTER2", typeof(string))); // 적용후 PITCH2
                itemdata.Columns.Add(new DataColumn("LBLPITCHAFTER3", typeof(string))); // 적용후 PITCH3
                itemdata.Columns.Add(new DataColumn("LBLSPEC2", typeof(string))); // SPEC
                itemdata.Columns.Add(new DataColumn("LBLLSL", typeof(string))); // 하한값
                itemdata.Columns.Add(new DataColumn("LBLUSL", typeof(string))); // 상한값
                itemdata.Columns.Add(new DataColumn("LBLSTDVALUE", typeof(string))); // 기준값PLATINGSPEC
                itemdata.Columns.Add(new DataColumn("LBLPLATINGCONDITIONUM", typeof(string))); // 도금조건
                itemdata.Columns.Add(new DataColumn("LBLHOLEINSIDEWALLUM", typeof(string))); // 홀내벽
                itemdata.Columns.Add(new DataColumn("LBLSURFACETOTAL", typeof(string))); // 표면TOTAL
                itemdata.Columns.Add(new DataColumn("LBLDIMPLEUM", typeof(string))); // DIMPLE
                itemdata.Columns.Add(new DataColumn("LBLOVERFILLUM", typeof(string))); // OVERFILL
                itemdata.Columns.Add(new DataColumn("LBLNIOSP", typeof(string))); // NI/OSP
                itemdata.Columns.Add(new DataColumn("LBLAU", typeof(string))); // AU
                itemdata.Columns.Add(new DataColumn("LBLPDSN", typeof(string))); // PD/SN
                itemdata.Columns.Add(new DataColumn("LBLITEMSURFACE", typeof(string))); // 제품 면적
                itemdata.Columns.Add(new DataColumn("LBLSCRAPSURFACE", typeof(string))); // 스크랩 면적
                itemdata.Columns.Add(new DataColumn("LBLHOLEPLATINGAREA1", typeof(string))); // HOLE 도금면적1
                itemdata.Columns.Add(new DataColumn("LBLHOLEPLATINGAREA2", typeof(string))); // HOLE 도금면적2
                itemdata.Columns.Add(new DataColumn("LBLSPECCHANGE", typeof(string))); // 사양변경내용
                itemdata.Columns.Add(new DataColumn("LBLCOMMENT", typeof(string))); // 특이사항
                itemdata.Columns.Add(new DataColumn("LBLBOMINFO", typeof(string))); // BOM 정보
                itemdata.Columns.Add(new DataColumn("LBLCIRCUITSPEC", typeof(string))); // 회로사양
                itemdata.Columns.Add(new DataColumn("LBLMATERIALTYPE", typeof(string))); // 자재유형
                itemdata.Columns.Add(new DataColumn("LBLMATERIALDEF", typeof(string))); // 자재코드
                itemdata.Columns.Add(new DataColumn("LBLCONSUMABLEDEFNAME", typeof(string))); // 자재명
                itemdata.Columns.Add(new DataColumn("LBLSPEC", typeof(string))); // 규격
                itemdata.Columns.Add(new DataColumn("LBLPNLSIZE", typeof(string))); // PNLSIZE
                itemdata.Columns.Add(new DataColumn("LBLUSERLAYER", typeof(string))); // 사용층
                itemdata.Columns.Add(new DataColumn("LBLMAKER", typeof(string))); // MAKER
                itemdata.Columns.Add(new DataColumn("LBLINKTYPE", typeof(string))); // INK TYPE
                itemdata.Columns.Add(new DataColumn("LBLWORKMETHOD", typeof(string))); // 치공구 작업
                itemdata.Columns.Add(new DataColumn("LBLREMARK", typeof(string))); // 비고
                itemdata.Columns.Add(new DataColumn("LBLSURFACESPEC", typeof(string))); // 비고
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
                itemdata.Columns.Add(new DataColumn("LBLUOM", typeof(string))); // 특이사항
                itemdata.Columns.Add(new DataColumn("LBLPLANTID", typeof(string))); // 사이트명
                itemdata.Columns.Add(new DataColumn("LBLUSERSEQUENCE", typeof(string))); // 공정수순
                itemdata.Columns.Add(new DataColumn("LBLPROCESSSEGMENTID", typeof(string))); // 공정ID
                itemdata.Columns.Add(new DataColumn("LBLPROCESSSEGMENTNAME", typeof(string))); // 공정명
                itemdata.Columns.Add(new DataColumn("LBLPROCESSUOM", typeof(string))); // 공정UOM
                itemdata.Columns.Add(new DataColumn("LBLROUTING", typeof(string))); // Routing 정보
                itemdata.Columns.Add(new DataColumn("LBLSUBASSEMBLYCODE", typeof(string))); // 반제품코드
                itemdata.Columns.Add(new DataColumn("LBLSUBASSEMBLYREV", typeof(string))); // 반제품버전
                itemdata.Columns.Add(new DataColumn("LBLSUBASSEMBLYNAME", typeof(string))); // 반제품명
                itemdata.Columns.Add(new DataColumn("LBLVARIABLE1", typeof(string))); // pnlx
                itemdata.Columns.Add(new DataColumn("LBLVARIABLE2", typeof(string))); // pnly
                itemdata.Columns.Add(new DataColumn("LBLARRAY", typeof(string))); // 합수
                itemdata.Columns.Add(new DataColumn("LBLCALCULATION", typeof(string))); // 산출수
                itemdata.Columns.Add(new DataColumn("LBLSEMIPRODUCTINFO", typeof(string))); // 반제품정보
                itemdata.Columns.Add(new DataColumn("LBLTOOLFORM", typeof(string))); // TOOL 형식
                itemdata.Columns.Add(new DataColumn("LBLTOOLTYPE", typeof(string))); // TOOL 유형 1
                itemdata.Columns.Add(new DataColumn("LBLTOOLDETAILTYPE", typeof(string))); // TOOL 유형 2
                itemdata.Columns.Add(new DataColumn("LBLTOOLCLASS", typeof(string))); // 구분
                itemdata.Columns.Add(new DataColumn("LBLSUMMARY", typeof(string))); // 합수
                itemdata.Columns.Add(new DataColumn("LBLHITCOUNT", typeof(string))); // 타수
                itemdata.Columns.Add(new DataColumn("LBLMOLDJIG2", typeof(string))); // 금형 JIG
                itemdata.Columns.Add(new DataColumn("LBLMOLDETC2", typeof(string))); // 목형 기타
                #endregion

                #region row 생성 및 다국어 처리 

                DataRow row = itemdata.NewRow();

                row["LBLTOOLFORM"] = Language.Get("TOOLFORMCODE"); // TOOL 형식
                row["LBLTOOLTYPE"] = Language.Get("TOOLCATEGORYDETAIL"); // TOOL 유형 1
                row["LBLTOOLDETAILTYPE"] = Language.Get("TOOLDETAIL"); // TOOL 유형 2
                row["LBLTOOLCLASS"] = Language.Get("CLASS"); // 구분
                row["LBLSUMMARY"] = Language.Get("SUMMARY"); // 합수
                row["LBLHITCOUNT"] = Language.Get("HITCOUNT"); // 타수
                row["LBLMOLDJIG2"] = Language.Get("MOLDJIG2"); // 금형
                row["LBLMOLDETC2"] = Language.Get("MOLDETC2"); // 목형
                row["LBLSURFACESPEC"] = Language.Get("SURFACESPEC2"); // 표면처리사양
                row["LBLCUSTOMERREV"] = Language.Get("CUSTOMERREV"); // 고객 REV
                row["LBLCUSTOMERID"] = Language.Get("CUSTOMERID"); // 고객 ID
                row["LBLITEMCODE"] = Language.Get("ITEMCODE"); // 품목코드
                row["LBLITEMNAME"] = Language.Get("ITEMNAME"); // 품목명
                row["LBLCUSTOMER"] = Language.Get("TXTCUSTOMERID"); // 고객 ID명
                row["LBLLAYER"] = Language.Get("LAYER"); // 층수
                row["LBLUOM"] = Language.Get("UOM"); // UOM
                row["LBLPRODUCTDEFVERSION"] = Language.Get("PRODUCTDEFVERSION"); // 내부 REV
                row["LBLBASICCODE"] = Language.Get("BASICCODE"); // 승인여부
                row["LBLQRBUSINESSINFO"] = Language.Get("QRBUSINESSINFO"); //  QR 사업부정보
                row["LBLQRBUSINESSSUB"] = Language.Get("QRBUSINESSSUB"); // QR사업부 SUB
                row["LBLPRODUCTLEVEL"] = Language.Get("PRODUCTLEVEL"); // 제품등급
                row["LBLPRODUCTIONTYPE"] = Language.Get("PRODUCTIONTYPE"); // 생산구분
                row["LBLQRPRODUCTIONTYPE"] = Language.Get("QRPRODUCTIONTYPE"); // QR 생산구분
                row["LBLQRMATERIALREV"] = Language.Get("QRMATERIALREV"); // QR 자재REV
                row["LBLSALESMAN"] = Language.Get("SALESPERSONNAME"); // 영업담당자
                row["LBLMAINFACTORY"] = Language.Get("MAINFACTORY"); // 주 제조공장
                row["LBLRTRSHEET"] = Language.Get("RTRSHEET"); // ROLL SHEET
                row["LBLPRODUCTSHAPE"] = Language.Get("PRODUCTSHAPE"); // 제품타입
                row["LBLSITESPECPERSON"] = Language.Get("SITESPECPERSON"); // 사양담당자
                row["LBLMEASUREMENT"] = Language.Get("MEASUREMENT"); // 치수측정
                row["LBLRELIABILITY"] = Language.Get("RELIABILITY"); // 신뢰성
                row["LBLHAZARDOUSSUBSTANCES"] = Language.Get("HAZARDOUSSUBSTANCES"); // 유해물질
                row["LBLPSRTOLERANCE"] = Language.Get("PSRTOLERANCE"); // PSR 공차
                row["LBLPLATINGSPEC"] = Language.Get("PLATINGSPEC2"); // 도금사양
                row["LBLIMPEDANCECHECK"] = Language.Get("IMPEDANCECHECK"); // 임피던스유무
                row["LBLIMPEDANCESPEC2"] = Language.Get("IMPEDANCESPEC2"); // 임피던스 SPEC
                row["LBLIMPEDANCETYPE2"] = Language.Get("IMPEDANCETYPE2"); //  임피던스 구분
                row["LBLIMPEDANCELAYER"] = Language.Get("IMPEDANCELAYER"); //  임피던스 적용층
                row["LBLPSRPOSITION"] = Language.Get("PSRPOSITION"); // PSR 좌표
                row["LBLELONGATIONCHECK"] = Language.Get("ELONGATIONCHECK"); // 연신율 유무
                row["LBLELONGATION-1"] = Language.Get("ELONGATION-1"); // 연신율1
                row["LBLELONGATION-2"] = Language.Get("ELONGATION-2"); // 연신율2
                row["LBLELONGATION-3"] = Language.Get("ELONGATION-3"); // 연신율3
                row["LBLPITCHBEFORE1"] = Language.Get("PITCHBEFORE1"); // 적용전 PITCH1
                row["LBLPITCHBEFORE2"] = Language.Get("PITCHBEFORE2"); // 적용전 PITCH2
                row["LBLPITCHBEFORE3"] = Language.Get("PITCHBEFORE3"); // 적용전 PITCH3
                row["LBLPITCHAFTER1"] = Language.Get("PITCHAFTER1"); // 적용후 PITCH1
                row["LBLPITCHAFTER2"] = Language.Get("PITCHAFTER2"); // 적용후 PITCH2
                row["LBLPITCHAFTER3"] = Language.Get("PITCHAFTER3"); // 적용후 PITCH3
                row["LBLSPEC2"] = Language.Get("SPEC2"); // SPEC
                row["LBLLSL"] = Language.Get("LSL"); // 하한값
                row["LBLUSL"] = Language.Get("USL"); // 상한값
                row["LBLSTDVALUE"] = Language.Get("STDVALUE"); // 기준값
                row["LBLPLATINGCONDITIONUM"] = Language.Get("PLATINGTHICK"); // 도금두께
                row["LBLHOLEINSIDEWALLUM"] = Language.Get("HOLEINSIDEWALLUM"); // 홀내벽
                row["LBLSURFACETOTAL"] = Language.Get("SURFACETOTAL"); //표면TOTAL
                row["LBLDIMPLEUM"] = Language.Get("DIMPLEUM"); // DIMPLE
                row["LBLOVERFILLUM"] = Language.Get("OVERFILLUM"); // OVER FILL
                row["LBLNIOSP"] = Language.Get("NIOSP"); // NIOSP
                row["LBLAU"] = Language.Get("AU"); // AU
                row["LBLPDSN"] = Language.Get("PDSN"); // PDSN
                row["LBLITEMSURFACE"] = Language.Get("ITEMSURFACE"); // 제품면적
                row["LBLSCRAPSURFACE"] = Language.Get("SCRAPSURFACE"); // 스크랩면적
                row["LBLHOLEPLATINGAREA1"] = Language.Get("HOLEPLATINGAREA1"); // HOLE도금면적1
                row["LBLHOLEPLATINGAREA2"] = Language.Get("HOLEPLATINGAREA2"); // HOLE도금면적2
                row["LBLSPECCHANGE"] = Language.Get("SPECCHANGE"); // 사양변경내용
                row["LBLCOMMENT"] = Language.Get("COMMENT"); // 특이사항
                row["LBLBOMINFO"] = Language.Get("BOMINFO2"); // BOM정보
                row["LBLCIRCUITSPEC"] = Language.Get("CIRCUITSPEC2"); // 회로사양
                row["LBLMATERIALTYPE"] = Language.Get("MATERIALTYPE"); // 자재유형
                row["LBLMATERIALDEF"] = Language.Get("MATERIALDEF"); // 자재코드
                row["LBLCONSUMABLEDEFNAME"] = Language.Get("CONSUMABLEDEFNAME"); // 자재명
                row["LBLSPEC"] = Language.Get("SPEC"); // 규격
                row["LBLPNLSIZE"] = Language.Get("PNLSIZE"); // PNL SIZE
                row["LBLUSERLAYER"] = Language.Get("USERLAYER"); // 사용층
                row["LBLMAKER"] = Language.Get("MAKER"); // MAKER
                row["LBLINKTYPE"] = Language.Get("INKTYPE"); // INK TYPE
                row["LBLWORKMETHOD"] = Language.Get("WORKMETHOD"); // 치공구작업
                row["LBLREMARK"] = Language.Get("REMARK"); // 비고
                row["COMMENT1"] = etcInfo.comment1().ToString();
                row["CHANGECONTENT1"] = etcInfo.change1().ToString();
                row["LBLPLANTID"] = Language.Get("PLANTID"); // 사이트명
                row["LBLUSERSEQUENCE"] = Language.Get("USERSEQUENCE"); // 공정수순
                row["LBLPROCESSSEGMENTID"] = Language.Get("PROCESSSEGMENTID"); // 공정ID
                row["LBLPROCESSSEGMENTNAME"] = Language.Get("PROCESSSEGMENTNAME"); // 공정명
                row["LBLPROCESSUOM"] = Language.Get("PROCESSUOM"); // 공정UOM
                row["LBLROUTING"] = Language.Get("ROUTINGINFO"); // ROUTING 정보
                row["LBLSUBASSEMBLYCODE"] = Language.Get("SUBASSEMBLYCODE"); // 반제품코드
                row["LBLSUBASSEMBLYREV"] = Language.Get("SUBASSEMBLYREV");// 반제품버전
                row["LBLSUBASSEMBLYNAME"] = Language.Get("SUBASSEMBLYNAME");// 반제품명
                row["LBLVARIABLE1"] = Language.Get("VARIABLE1"); // pnlx
                row["LBLVARIABLE2"] = Language.Get("VARIABLE2"); // pnly
                row["LBLARRAY"] = Language.Get("ARRAY");  // 합수
                row["LBLCALCULATION"] = Language.Get("CALCULATION");  // 산출수
                row["LBLSEMIPRODUCTINFO"] = Language.Get("SEMIPRODUCTINFO");  // 반제품정보
                itemdata.Rows.Add(row);

                #endregion 라벨

                //출력을 위한 데이터 테이블 생성

                DataTable circuit = circuitSpec.PSRreturn();
                DataTable layer = grdLayerComposition.DataSource as DataTable;

                DataTable dtmold = grdtoollist.DataSource as DataTable;
                DataTable dtetc = grdetclist.DataSource as DataTable;

                DataTable moldtable = dtmold.Copy();
                DataTable etctalbe = dtetc.Copy();

                DataTable sumdt = new DataTable();
                var values = Conditions.GetValues();

                Dictionary<string, object> Param = new Dictionary<string, object>();
                Param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                Param.Add("P_PRODUCTDEFID", Format.GetString(values["P_ITEMID"]).Split('|')[0]);
                Param.Add("P_PRODUCTDEFVERSION", Format.GetString(values["P_ITEMID"]).Split('|')[1]);
                DataTable routing = SqlExecuter.Query("GetRoutingOperationList", "10001", Param);



                // 데이터셋에 각 화면에서 가져온 값들을 넣는다
                DataSet ds = new DataSet();
                ds.Tables.Add(platingSpec.Specreturn());
                ds.Tables.Add(circuitSpec.PSRreturn());
                ds.Tables.Add(itemdata);
                ds.Tables.Add(productInfo.Productinforeturn());
                ds.Tables.Add(productSpec.Productspecreturn());
                ds.Tables.Add(surfaceSpec.SurFacereturn());


                // merge를 쓰면 로우가 불필요하게 늘어나기 때문에 아래 함수를 이용하여 컬럼과 데이터를 합쳐서 sumdt 테이블에 넣는다
                sumdt = ColumnAdd(sumdt, ds);
                sumdt = DataAdd(sumdt, ds);

                //작성한 리포트
                Assembly assembly = Assembly.GetAssembly(this.GetType());
                Stream stream = assembly.GetManifestResourceStream("Micube.SmartMES.StandardInfo.Report.ItemSpec.repx");
                XtraReport report = XtraReport.FromStream(stream);

                //그리드의 데이터가 이미 다른 Dataset에 포함되어있기때문에 새로운 DataTable을 만들어 Copy 한다.
                DataTable layertable = layer.Copy();



                //릴레이션을 하기위한 컬럼추가
                moldtable.Columns.Add(new DataColumn("ITEMCODE", typeof(string)));
                etctalbe.Columns.Add(new DataColumn("ITEMCODE", typeof(string)));
                layertable.Columns.Add(new DataColumn("ITEMCODE", typeof(string)));
                for (int i = 0; i < layertable.Rows.Count; i++)
                {
                    layertable.Rows[i]["ITEMCODE"] = sumdt.Rows[0]["ITEMID"];
                }
                DataTable routingtable = routing.Copy();

                routingtable.Columns.Add(new DataColumn("ITEMCODE", typeof(string)));
                for (int i = 0; i < routingtable.Rows.Count; i++)
                {
                    routingtable.Rows[i]["ITEMCODE"] = sumdt.Rows[0]["ITEMID"];
                }
                for (int i = 0; i < moldtable.Rows.Count; i++)
                {
                    moldtable.Rows[i]["ITEMCODE"] = sumdt.Rows[0]["ITEMID"];
                }
                for (int i = 0; i < etctalbe.Rows.Count; i++)
                {
                    etctalbe.Rows[i]["ITEMCODE"] = sumdt.Rows[0]["ITEMID"];
                }

                moldtable.TableName = "mold";
                etctalbe.TableName = "etc";


                layertable.TableName = "layer";
                routingtable.TableName = "routing";


                sumdt.TableName = "label";
                dsReport.Tables.Add(sumdt);
                dsReport.Tables.Add(layertable);
                dsReport.Tables.Add(routingtable);
                dsReport.Tables.Add(moldtable);
                dsReport.Tables.Add(etctalbe);



                if (layertable.Rows.Count > 0)  // 자재 릴레이션
                {
                    DataRelation relation = new DataRelation("layer", sumdt.Columns["ITEMID"], layertable.Columns["ITEMCODE"]);
                    dsReport.Relations.Add(relation);
                }

                if (routing.Rows.Count > 0) // 자재 라우팅 
                {
                    DataRelation relation = new DataRelation("routing", sumdt.Columns["ITEMID"], routingtable.Columns["ITEMCODE"]);
                    dsReport.Relations.Add(relation);
                }
                if (etctalbe.Rows.Count > 0) // 기타 치공구
                {
                    DataRelation relation = new DataRelation("etc", sumdt.Columns["ITEMID"], etctalbe.Columns["ITEMCODE"]);
                    dsReport.Relations.Add(relation);
                }
                if (moldtable.Rows.Count > 0)  // 금형 치공구
                {
                    DataRelation relation = new DataRelation("mold", sumdt.Columns["ITEMID"], moldtable.Columns["ITEMCODE"]);
                    dsReport.Relations.Add(relation);
                }





                report.DataSource = dsReport;
                report.DataMember = "label";


                Band headerPage = report.Bands["Detail"];
                SetReportControlDataBinding(headerPage.Controls, sumdt);


                DetailReportBand detailReport4 = report.Bands["DetailReport3"] as DetailReportBand; // 상세정보 및 특이사항
                detailReport4.DataSource = dsReport;
                detailReport4.DataMember = "label";

                Band detailBand4 = detailReport4.Bands["Detail4"];
                SetReportControlDataBinding(detailBand4.Controls, sumdt);



                DetailReportBand detailReport1 = report.Bands["DetailReport"] as DetailReportBand;  //목형 컬럼
                detailReport1.DataSource = dsReport;
                detailReport1.DataMember = "rein";

                Band detailBand1 = detailReport1.Bands["GroupHeader3"];
                SetReportControlDataBinding(detailBand1.Controls, sumdt);


                DetailReportBand detailReport2 = report.Bands["DetailReport"] as DetailReportBand; // 목형 그리드
                detailReport2.DataSource = dsReport;
                detailReport2.DataMember = "etc";

                Band detailBand2 = detailReport2.Bands["Detail1"];
                SetReportControlDataBinding2(detailBand2.Controls, dsReport, "etc");

                detailReport1 = report.Bands["DetailReport1"] as DetailReportBand;  // 금형 컬럼
                detailReport1.DataSource = dsReport;
                detailReport1.DataMember = "rein";

                detailBand1 = detailReport1.Bands["GroupHeader4"];
                SetReportControlDataBinding(detailBand1.Controls, sumdt);


                detailReport2 = report.Bands["DetailReport1"] as DetailReportBand; // 금형 그리드
                detailReport2.DataSource = dsReport;
                detailReport2.DataMember = "mold";

                detailBand2 = detailReport2.Bands["Detail2"];
                SetReportControlDataBinding2(detailBand2.Controls, dsReport, "mold");



                DetailReportBand detailReport = report.Bands["DetailReport5"] as DetailReportBand;  //자재 컬럼
                detailReport.DataSource = dsReport;
                detailReport.DataMember = "rein";

                Band detailBand = detailReport.Bands["GroupHeader1"];
                SetReportControlDataBinding(detailBand.Controls, sumdt);


                DetailReportBand detailReport3 = report.Bands["DetailReport5"] as DetailReportBand; // 자재 그리드
                detailReport3.DataSource = dsReport;
                detailReport3.DataMember = "layer";

                Band detailBand3 = detailReport3.Bands["Detail6"];
                SetReportControlDataBinding2(detailBand3.Controls, dsReport, "layer");




                detailReport1 = report.Bands["DetailReport7"] as DetailReportBand;  //라우팅 컬럼
                detailReport1.DataSource = dsReport;
                detailReport1.DataMember = "rein";

                detailBand1 = detailReport1.Bands["GroupHeader2"];
                SetReportControlDataBinding(detailBand1.Controls, sumdt);


                detailReport2 = report.Bands["DetailReport7"] as DetailReportBand; // 라우팅 그리드
                detailReport2.DataSource = dsReport;
                detailReport2.DataMember = "routing";

                detailBand2 = detailReport2.Bands["Detail8"];
                SetReportControlDataBinding2(detailBand2.Controls, dsReport, "routing");




                Param = new Dictionary<string, object>();
                Param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                Param.Add("P_PRODUCTDEFID", Format.GetString(values["P_ITEMID"]).Split('|')[0]);
                Param.Add("P_PRODUCTDEFVERSION", Format.GetString(values["P_ITEMID"]).Split('|')[1]);

                DataTable subItem = SqlExecuter.Query("GetBOMTree", "10002", Param);


                foreach (DataRow r in subItem.Rows)
                {

                    DetailReportBand bomInfoMain = new DetailReportBand();
                    GroupHeaderBand bomInfoHeader = new GroupHeaderBand();
                    bomInfoHeader.Height = 0;
                    DetailBand bomInfoDetail = new DetailBand();
                    bomInfoDetail.Height = 0;


                    bomInfoMain.Bands.Add(bomInfoHeader);
                    bomInfoMain.Bands.Add(bomInfoDetail);


                    report.Bands.Add(bomInfoMain);


                    SetReportBOMInfo(r, bomInfoMain, bomInfoHeader, bomInfoDetail);

                    Param = new Dictionary<string, object>();
                    Param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                    Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                    Param.Add("P_PRODUCTDEFID", Format.GetString(r["ASSEMBLYITEMID"]));
                    Param.Add("P_PRODUCTDEFVERSION", Format.GetString(r["ASSEMBLYITEMVERSION"]));

                    DataTable routing2 = SqlExecuter.Query("GetRoutingOperationList", "10002", Param);

                    if (routing2 != null && routing2.Rows.Count > 0)
                    {
                        DetailReportBand bomRoutingMain = new DetailReportBand();
                        GroupHeaderBand bomRoutingHeader = new GroupHeaderBand();
                        bomRoutingHeader.Height = 0;
                        DetailBand bomRoutingDetail = new DetailBand();
                        bomRoutingDetail.Height = 0;
                        //        bomRoutingDetail.Height = routing2.Rows.Count * 11;

                        bomRoutingMain.Bands.Add(bomRoutingHeader);
                        bomRoutingMain.Bands.Add(bomRoutingDetail);

                        report.Bands.Add(bomRoutingMain);

                        SetReportBOMRouting(routing2, bomRoutingMain, bomRoutingHeader, bomRoutingDetail);

                    }

                }

                ReportPrintTool printTool = new ReportPrintTool(report);

                printTool.ShowRibbonPreview();

            }
            else if (btn.Name.ToString().Equals("Save"))
            {
                base.OnToolbarSaveClick();

                var values = Conditions.GetValues();
                if (Format.GetString(values["P_ITEMID"]).Equals(""))
                {
                    throw MessageException.Create("RequiredSearch2");
                }


                //제품정보


                Dictionary<string, object> productPairs = productInfo.Save();

                //제품사양
                Dictionary<string, object> productSpecPairs = productSpec.Save();

                //회로사양
                DataTable CircleSpec = circuitSpec.Save();

                //임피던스
                Dictionary<string, object> impedance = circuitSpec.Save2();



                //기타정보
                Dictionary<string, object> etcInfoPairs = etcInfo.Save();

                //도금 정보
                DataTable dtPlatingInfo = platingSpec.Save();

                DataTable dtsurface = surfaceSpec.Save();

                DataTable dtetc = this.grdetclist.GetChangedRows();

                surfaceSpec.ClearData();
                platingSpec.ClearData();

                MessageWorker worker = new MessageWorker("SaveProductSpec3");
                worker.SetBody(new MessageBody()
                {
                    { "enterpriseId", UserInfo.Current.Enterprise },
                    { "plantId", UserInfo.Current.Plant },
                    { "productInfo", productPairs },
                    { "productSpec", productSpecPairs },
                    { "etcInfo", etcInfoPairs },
                    { "circuitSpec", CircleSpec },
                    { "impedance", impedance },
                    { "toolinfo", dtetc },
                    { "surface", dtsurface },
                    { "platingInfo", dtPlatingInfo},

                });
                worker.Execute();
                ShowMessage("SuccedSave");
                OnSearchAsync();
            }

        }
        #endregion


        private void SetReportBOMInfo(DataRow row, DetailReportBand bomInfoMain, GroupHeaderBand bomInfoHeader, DetailBand bomInfoDetail)
        {
            Color borderColor = Color.Black;
            float borderWidth = 1;
            DevExpress.XtraPrinting.BorderDashStyle borderStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;

            // Create a table to represent headers
            XRTable tableHeader = CreateXRTable(785, bomInfoHeader.Height);

            tableHeader.SuspendLayout();

            tableHeader.BeginInit();

            XRTableRow emptyRow = CreateXRTableRow(785, 22);
            tableHeader.Rows.Add(emptyRow);

            XRTableRow headerRow = CreateXRTableRow(785, 22);

            tableHeader.Rows.Add(headerRow);

            headerRow.Cells.Add(CreateXRCell(785, 22, Language.Get("BOMINFO2"), DevExpress.XtraPrinting.TextAlignment.MiddleLeft, new Font("Times New Roman", (float)8.25, FontStyle.Bold), DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right, borderColor, borderWidth, borderStyle, ColorTranslator.FromHtml("#DCDCDC")));


            XRTableRow headerColumn = new XRTableRow();
            tableHeader.Rows.Add(headerColumn);

            headerColumn.Cells.Add(CreateXRCell(90, 22, Language.Get("SUBASSEMBLYCODE"), DevExpress.XtraPrinting.TextAlignment.MiddleCenter, new Font("Times New Roman", (float)7.75), DevExpress.XtraPrinting.BorderSide.All, borderColor, borderWidth, borderStyle, ColorTranslator.FromHtml("#DCDCDC")));
            headerColumn.Cells.Add(CreateXRCell(80, 22, Language.Get("SUBASSEMBLYREV"), DevExpress.XtraPrinting.TextAlignment.MiddleCenter, new Font("Times New Roman", (float)7.75), DevExpress.XtraPrinting.BorderSide.All, borderColor, borderWidth, borderStyle, ColorTranslator.FromHtml("#DCDCDC")));
            headerColumn.Cells.Add(CreateXRCell(180, 22, Language.Get("SUBASSEMBLYNAME"), DevExpress.XtraPrinting.TextAlignment.MiddleCenter, new Font("Times New Roman", (float)7.75), DevExpress.XtraPrinting.BorderSide.All, borderColor, borderWidth, borderStyle, ColorTranslator.FromHtml("#DCDCDC")));
            headerColumn.Cells.Add(CreateXRCell(60, 22, Language.Get("VARIABLE1"), DevExpress.XtraPrinting.TextAlignment.MiddleCenter, new Font("Times New Roman", (float)7.75), DevExpress.XtraPrinting.BorderSide.All, borderColor, borderWidth, borderStyle, ColorTranslator.FromHtml("#DCDCDC")));
            headerColumn.Cells.Add(CreateXRCell(60, 22, Language.Get("VARIABLE2"), DevExpress.XtraPrinting.TextAlignment.MiddleCenter, new Font("Times New Roman", (float)7.75), DevExpress.XtraPrinting.BorderSide.All, borderColor, borderWidth, borderStyle, ColorTranslator.FromHtml("#DCDCDC")));
            headerColumn.Cells.Add(CreateXRCell(60, 22, Language.Get("ARRAY"), DevExpress.XtraPrinting.TextAlignment.MiddleCenter, new Font("Times New Roman", (float)7.75), DevExpress.XtraPrinting.BorderSide.All, borderColor, borderWidth, borderStyle, ColorTranslator.FromHtml("#DCDCDC")));
            headerColumn.Cells.Add(CreateXRCell(60, 22, Language.Get("CALCULATION"), DevExpress.XtraPrinting.TextAlignment.MiddleCenter, new Font("Times New Roman", (float)7.75), DevExpress.XtraPrinting.BorderSide.All, borderColor, borderWidth, borderStyle, ColorTranslator.FromHtml("#DCDCDC")));
            headerColumn.Cells.Add(CreateXRCell(195, 22, Language.Get("REMARK"), DevExpress.XtraPrinting.TextAlignment.MiddleCenter, new Font("Times New Roman", (float)7.75), DevExpress.XtraPrinting.BorderSide.All, borderColor, borderWidth, borderStyle, ColorTranslator.FromHtml("#DCDCDC")));


            // Create a table to display data
            XRTable tableDetail = CreateXRTable(785, 22);


            tableDetail.SuspendLayout();
            tableDetail.BeginInit();

            XRTableRow headerDataRow = new XRTableRow();
            tableDetail.Rows.Add(headerDataRow);

            headerDataRow.Cells.Add(CreateXRCell(90, 22, row["ASSEMBLYITEMID"].ToString(), DevExpress.XtraPrinting.TextAlignment.MiddleLeft, new Font("Times New Roman", (float)7.75), DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right, borderColor, borderWidth, borderStyle, Color.White));
            headerDataRow.Cells.Add(CreateXRCell(80, 22, row["ASSEMBLYITEMVERSION"].ToString(), DevExpress.XtraPrinting.TextAlignment.MiddleLeft, new Font("Times New Roman", (float)7.75), DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right, borderColor, borderWidth, borderStyle, Color.White));
            headerDataRow.Cells.Add(CreateXRCell(180, 22, row["ASSEMBLYITEMNAME"].ToString(), DevExpress.XtraPrinting.TextAlignment.MiddleLeft, new Font("Times New Roman", (float)7.75), DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right, borderColor, borderWidth, borderStyle, Color.White));
            headerDataRow.Cells.Add(CreateXRCell(60, 22, row["PNLSIZEXAXIS2"].ToString(), DevExpress.XtraPrinting.TextAlignment.MiddleRight, new Font("Times New Roman", (float)7.75), DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right, borderColor, borderWidth, borderStyle, Color.White));
            headerDataRow.Cells.Add(CreateXRCell(60, 22, row["PNLSIZEYAXIS2"].ToString(), DevExpress.XtraPrinting.TextAlignment.MiddleRight, new Font("Times New Roman", (float)7.75), DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right, borderColor, borderWidth, borderStyle, Color.White));
            headerDataRow.Cells.Add(CreateXRCell(60, 22, row["PCSPNL2"].ToString(), DevExpress.XtraPrinting.TextAlignment.MiddleRight, new Font("Times New Roman", (float)7.75), DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right, borderColor, borderWidth, borderStyle, Color.White));
            headerDataRow.Cells.Add(CreateXRCell(60, 22, row["CALCULATEPCS"].ToString(), DevExpress.XtraPrinting.TextAlignment.MiddleRight, new Font("Times New Roman", (float)7.75), DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right, borderColor, borderWidth, borderStyle, Color.White));
            headerDataRow.Cells.Add(CreateXRCell(195, 22, row["DESCRIPTION2"].ToString(), DevExpress.XtraPrinting.TextAlignment.MiddleLeft, new Font("Times New Roman", (float)7.75), DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right, borderColor, borderWidth, borderStyle, Color.White));

            tableDetail.EvenStyleName = "EvenStyle";
            tableDetail.OddStyleName = "OddStyle";

            tableHeader.EndInit();
            tableDetail.EndInit();

            tableDetail.PerformLayout();
            bomInfoHeader.Controls.Add(tableHeader);
            bomInfoDetail.Controls.Add(tableDetail);


        }


        private void SetReportBOMRouting(DataTable dt, DetailReportBand bomInfoMain, GroupHeaderBand bomInfoHeader, DetailBand bomInfoDetail)
        {
            Color borderColor = Color.Black;
            float borderWidth = 1;
            DevExpress.XtraPrinting.BorderDashStyle borderStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            Font font = new Font("Times New Roman", (float)7.75);

            // Create a table to represent headers
            XRTable tableHeader = CreateXRTable(785, bomInfoHeader.Height);

            tableHeader.SuspendLayout();

            tableHeader.BeginInit();

            XRTableRow headerRow = CreateXRTableRow(785, 22);
            tableHeader.Rows.Add(headerRow);

            headerRow.Cells.Add(CreateXRCell(785, 22, Language.Get("ROUTINGINFO"), DevExpress.XtraPrinting.TextAlignment.MiddleLeft, font, DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right, borderColor, borderWidth, borderStyle, ColorTranslator.FromHtml("#DCDCDC")));



            XRTableRow headerColumn = CreateXRTableRow(785, 22);
            tableHeader.Rows.Add(headerColumn);

            headerColumn.Cells.Add(CreateXRCell(70, 22, Language.Get("PLANTID"), DevExpress.XtraPrinting.TextAlignment.MiddleCenter, new Font("Times New Roman", (float)7.75), DevExpress.XtraPrinting.BorderSide.All, borderColor, borderWidth, borderStyle, ColorTranslator.FromHtml("#DCDCDC")));
            headerColumn.Cells.Add(CreateXRCell(70, 22, Language.Get("USERSEQUENCE"), DevExpress.XtraPrinting.TextAlignment.MiddleCenter, new Font("Times New Roman", (float)7.75), DevExpress.XtraPrinting.BorderSide.All, borderColor, borderWidth, borderStyle, ColorTranslator.FromHtml("#DCDCDC")));
            headerColumn.Cells.Add(CreateXRCell(70, 22, Language.Get("PROCESSSEGMENTID"), DevExpress.XtraPrinting.TextAlignment.MiddleCenter, new Font("Times New Roman", (float)7.75), DevExpress.XtraPrinting.BorderSide.All, borderColor, borderWidth, borderStyle, ColorTranslator.FromHtml("#DCDCDC")));
            headerColumn.Cells.Add(CreateXRCell(240, 22, Language.Get("PROCESSSEGMENTNAME"), DevExpress.XtraPrinting.TextAlignment.MiddleCenter, new Font("Times New Roman", (float)7.75), DevExpress.XtraPrinting.BorderSide.All, borderColor, borderWidth, borderStyle, ColorTranslator.FromHtml("#DCDCDC")));
            headerColumn.Cells.Add(CreateXRCell(70, 22, Language.Get("PROCESSUOM"), DevExpress.XtraPrinting.TextAlignment.MiddleCenter, new Font("Times New Roman", (float)7.75), DevExpress.XtraPrinting.BorderSide.All, borderColor, borderWidth, borderStyle, ColorTranslator.FromHtml("#DCDCDC")));
            headerColumn.Cells.Add(CreateXRCell(265, 22, Language.Get("COMMENT"), DevExpress.XtraPrinting.TextAlignment.MiddleCenter, new Font("Times New Roman", (float)7.75), DevExpress.XtraPrinting.BorderSide.All, borderColor, borderWidth, borderStyle, ColorTranslator.FromHtml("#DCDCDC")));


            // Create a table to display data
            XRTable tableDetail = CreateXRTable(785, 22);

            tableDetail.SuspendLayout();
            tableDetail.BeginInit();

            foreach (DataRow row in dt.Rows)
            {
                XRTableRow headerDataRow = new XRTableRow();
                tableDetail.Rows.Add(headerDataRow);

                headerDataRow.Cells.Add(CreateXRCell(70, 22, row["PLANTID2"].ToString(), DevExpress.XtraPrinting.TextAlignment.MiddleCenter, font, DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right, borderColor, borderWidth, borderStyle, Color.White));
                headerDataRow.Cells.Add(CreateXRCell(70, 22, row["USERSEQUENCE"].ToString(), DevExpress.XtraPrinting.TextAlignment.MiddleCenter, font, DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right, borderColor, borderWidth, borderStyle, Color.White));
                headerDataRow.Cells.Add(CreateXRCell(70, 22, row["PROCESSSEGMENTID2"].ToString(), DevExpress.XtraPrinting.TextAlignment.MiddleCenter, font, DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right, borderColor, borderWidth, borderStyle, Color.White));
                headerDataRow.Cells.Add(CreateXRCell(240, 22, row["PROCESSSEGMENTNAME2"].ToString(), DevExpress.XtraPrinting.TextAlignment.MiddleLeft, font, DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right, borderColor, borderWidth, borderStyle, Color.White));
                headerDataRow.Cells.Add(CreateXRCell(70, 22, row["PROCESSUOM2"].ToString(), DevExpress.XtraPrinting.TextAlignment.MiddleCenter, font, DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right, borderColor, borderWidth, borderStyle, Color.White));
                headerDataRow.Cells.Add(CreateXRCell(265, 22, row["DESCRIPTION2"].ToString(), DevExpress.XtraPrinting.TextAlignment.MiddleLeft, font, DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right, borderColor, borderWidth, borderStyle, Color.White));

            }
            tableDetail.EvenStyleName = "EvenStyle";
            tableDetail.OddStyleName = "OddStyle";

            tableHeader.EndInit();
            tableDetail.EndInit();

            tableDetail.PerformLayout();
            bomInfoHeader.Controls.Add(tableHeader);
            bomInfoDetail.Controls.Add(tableDetail);


        }


        private XRTable CreateXRTable(int width, int height)
        {

            // Create a table to represent headers
            XRTable tableHeader = new XRTable();
            //        tableHeader.Padding = 0;
            //       tableHeader.SnapLineMargin = 0;

            tableHeader.Height = height;
            tableHeader.Width = width;

            return tableHeader;
        }
        private XRTableRow CreateXRTableRow(int width, int height)
        {
            XRTableRow headerRow = new XRTableRow();
            headerRow.Width = width;
            headerRow.Height = height;
            //          headerRow.Padding = 0;
            //          headerRow.SnapLineMargin = 0;
            return headerRow;
        }

        private XRTableCell CreateXRCell(int width, int height, string text, DevExpress.XtraPrinting.TextAlignment textAlignment, Font font, DevExpress.XtraPrinting.BorderSide border,
         Color borderColor, float borderWidth, DevExpress.XtraPrinting.BorderDashStyle borderStyle, Color backColor)
        {
            XRTableCell headerCell = new XRTableCell();
            headerCell.Width = width;
            headerCell.Height = height;
            headerCell.Text = text;
            //          headerCell.Padding = 0;
            //          headerCell.SnapLineMargin = 0;
            if (textAlignment == DevExpress.XtraPrinting.TextAlignment.MiddleLeft)
                headerCell.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 2, 0, 0);
            headerCell.TextAlignment = textAlignment;
            headerCell.Borders = border;
            headerCell.BorderColor = borderColor;
            headerCell.BorderWidth = borderWidth;
            headerCell.BorderDashStyle = borderStyle;
            headerCell.Font = font;
            headerCell.BackColor = backColor;


            return headerCell;
        }
        private void SetReportControlSetName(XRControlCollection controls, int index) // 노 릴레이션 데이터 바인딩
        {
            if (controls.Count > 0)
            {
                foreach (XRControl control in controls)
                {
                    //  if (!string.IsNullOrWhiteSpace(control.Tag.ToString()))
                    control.Name = control.Name + index;

                    SetReportControlSetName(control.Controls, index);
                }
            }
        }
        private void SetReportControlDataBinding_Test(XRControlCollection controls, DataTable dataSource) // 노 릴레이션 데이터 바인딩
        {
            if (controls.Count > 0)
            {
                foreach (XRControl control in controls)
                {
                    string columnName = control.Tag.ToString();

                    Console.WriteLine(string.Format("{0} t\t\t {1}", control.Name, columnName));
                    if (!string.IsNullOrWhiteSpace(columnName) && dataSource.Columns.Contains(columnName))
                    {
                        control.Text = dataSource.Rows[0][columnName].ToString();
                    }
                    //           control.DataBindings.Add("Text", dataSource, control.Tag.ToString());


                    //         SetReportControlDataBinding(control.Controls, dataSource);

                    SetReportControlDataBinding_Test(control.Controls, dataSource);
                }
            }
        }

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
                    if (table.Rows.Count < tablename.Rows.Count && j > table.Rows.Count - 1)
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
            platingSpec.ClearData();
            surfaceSpec.ClearData();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            values.Add("ITEMID", Format.GetString(values["P_ITEMID"]).Split('|')[0]);
            values.Add("ITEMVERSION", Format.GetString(values["P_ITEMID"]).Split('|')[1]);
            Dictionary<string, object> ParamISHF = new Dictionary<string, object>();
            ParamISHF.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            ParamISHF.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            ParamISHF.Add("ITEMID", Format.GetString(values["P_ITEMID"]).Split('|')[0]);
            ParamISHF.Add("ITEMVERSION", Format.GetString(values["P_ITEMID"]).Split('|')[1]);
            platingSpec.parameterreturn(ParamISHF);
            surfaceSpec.parameterreturn(ParamISHF);

            DataTable dtProductSpec = SqlExecuter.Query("SelectProductSpec", "10001", values);

            DataTable dtCircuit = SqlExecuter.Query("SelectProductSpecDetail", "10002", values);
            DataTable dtComment = SqlExecuter.Query("SelectProductSpecComment", "10001", values);
            DataTable dtMeterialInfo = SqlExecuter.Query("SelectProductSpecDetail_Meterial", "10005", values);
            DataTable dtInkInfo = SqlExecuter.Query("SelectProductSpecDetail_InkInfo", "10001", values);
            DataTable dtSubMeterial = SqlExecuter.Query("SelectProductSpecDetail_SubMeterial", "10001", values);
            DataTable dtPlating = SqlExecuter.Query("SelectProductSpecDetail_holeandplating", "10001", values);
            DataTable dtsurface = SqlExecuter.Query("SelectProductSpecDetail_Surface", "10001", values);
            grdtoollist.DataSource = SqlExecuter.Query("SelectToolByItem_YPE", "10002", values);
            grdetclist.DataSource = SqlExecuter.Query("SelectToolByItem_YPE", "10001", values);

            if (dtProductSpec.Rows.Count < 1)
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }
            else
            {
                productInfo.DataBind(dtProductSpec);
                productSpec.DataBind(dtProductSpec);
                circuitSpec.DataBind(dtProductSpec);
                circuitSpec.DataBind2(dtCircuit);
                etcInfo.DataBind(dtProductSpec);
                etcInfo.DataBind(dtComment);
                this.grdLayerComposition.DataSource = dtMeterialInfo;
                surfaceSpec.DataBind(dtsurface);
                platingSpec.DataBind(dtPlating);

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
                surfaceSpec.ClearData();
                this.grdLayerComposition.DataSource = null;

                this.grdetclist.DataSource = null;

                this.grdtoollist.DataSource = null;
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

            this.grdtoollist.View.CheckValidation();

            this.grdetclist.View.CheckValidation();

            this.grdLayerComposition.View.CheckValidation();


        }

        #endregion

        #region Private Function

        #endregion

        private void grdLayerComposition_Load(object sender, EventArgs e)
        {

        }
    }

}