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
using DevExpress.XtraReports.UI;
using System.Reflection;
using System.IO;

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
    public partial class ProductSpecification2 : SmartConditionManualBaseForm
	{

        #region Local Variables

        //제품정보
        private ProductSpecificationProductInfo2 productInfo { get; set; }
        //제품사양
        private ProductSpecificationProductSpec2 productSpec { get; set; }
        //회로사양
        private ProductSpecificationCircuitSpec2 circuitSpec { get; set; }
        //기타정보
        private ProductSpecificationEtcInfo2 etcInfo { get; set; }

        //도금정보
        private ProductSpecificationPlatingSpec2 platingSpec { get; set; }



        #endregion

        #region 생성자

        public ProductSpecification2()
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
                string productDefId = Format.GetString(parameters["ITEMID"]);
                string productDefVersion = Format.GetString(parameters["ITEMVERSION"]);
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

            productInfo = new ProductSpecificationProductInfo2();
            productSpec = new ProductSpecificationProductSpec2();
            circuitSpec = new ProductSpecificationCircuitSpec2();
            etcInfo = new ProductSpecificationEtcInfo2();
            platingSpec = new ProductSpecificationPlatingSpec2();

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
            //색상
            grdInkInfo.View.AddComboBoxColumn("COLOR", 90, new SqlQuery("GetTypeList", "10001", "CODECLASSID=ColorType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            //사용층
            grdInkInfo.View.AddComboBoxColumn("USERLAYER1", 90, new SqlQuery("GetTypeList", "10001", "CODECLASSID=UserLayer", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            //사용층2
            grdInkInfo.View.AddComboBoxColumn("USERLAYER2", 90, new SqlQuery("GetTypeList", "10001", "CODECLASSID=UserLayer", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            //생성일자
            grdInkInfo.View.AddTextBoxColumn("CREATEDTIME", 100)
                .SetDisplayFormat("yyyy-MM-dd")
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("CREATEDATE");


            grdInkInfo.View.PopulateColumns();

            //부자재정보
            grdSubMaterial.GridButtonItem = GridButtonItem.None;

            //자재유형
            grdSubMaterial.View.AddTextBoxColumn("MATERIALDETAILTYPE", 120)
                .SetLabel("MATERIALTYPE")
                .SetValidationIsRequired();
            //부자재명
            grdSubMaterial.View.AddTextBoxColumn("COMPONENTITEMNAME", 100)
                .SetLabel("SUBSIDIARYNAME");
            //작업방법
            grdSubMaterial.View.AddTextBoxColumn("WORKMETHOD", 100)
                .SetLabel("ISMAINSEGMENT");
            //사용층
            grdSubMaterial.View.AddTextBoxColumn("USERLAYER", 80);

            grdSubMaterial.View.PopulateColumns();


            //금형/목형
            grdtoollist.GridButtonItem = GridButtonItem.Export | GridButtonItem.Add | GridButtonItem.Delete;
            grdtoollist.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            

            InitializeProductId_Popup();
            //품목버전
            grdtoollist.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();
            //치공구 유형
            grdtoollist.View.AddComboBoxColumn("DURABLECLASSID", 100, new SqlQuery("GetTypeList", "10001", "CODECLASSID=ItemToolType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsReadOnly()
                .SetDefault("Mold")
                .SetLabel("DURABLETYPE");
            //품목명
            grdtoollist.View.AddTextBoxColumn("PRODUCTDEFNAME", 300)
                .SetIsReadOnly();
            //SEQUENCE
            grdtoollist.View.AddTextBoxColumn("SEQUENCE3", 300)
                .SetIsReadOnly()
                .SetIsHidden();
            //치공구 ID
            grdtoollist.View.AddTextBoxColumn("DURABLEDEFID", 100)
                .SetIsHidden()
                .SetIsReadOnly();
            //치공구명
            grdtoollist.View.AddTextBoxColumn("DURABLEDEFNAME", 300)
                .SetIsReadOnly();
            //치공구 VERSION
            grdtoollist.View.AddTextBoxColumn("DURABLEDEFVERSION", 100)
                .SetIsHidden()
                .SetIsReadOnly();
            //치공구 유형
            grdtoollist.View.AddTextBoxColumn("TOOLFORM", 100)
                .SetIsReadOnly()
                .SetLabel("DURABLECATEGORYDETAILCODE");
            // 종류
            grdtoollist.View.AddTextBoxColumn("TOOLKIND", 100)
                .SetIsReadOnly()
                .SetLabel("CLASSIFY");
                
            //합수
            grdtoollist.View.AddTextBoxColumn("SUMMARY", 100)
                .SetIsReadOnly()
                .SetLabel("ARRAY");

            //구분
            grdtoollist.View.AddComboBoxColumn("TOOLCLASS", 100, new SqlQuery("GetTypeList", "10001", "CODECLASSID=ProductSpecToolClassify", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsReadOnly()
                .SetLabel("CLASS");

            //제작처6
            grdtoollist.View.AddTextBoxColumn("MANUFACTURER", 200)
                .SetIsReadOnly()
                .SetLabel("MANUFACTURER");
            //입고 작업장
            grdtoollist.View.AddTextBoxColumn("RECEIPTAREAID", 200)
                .SetIsReadOnly()
                .SetLabel("RECEIPTAREA");

            //SCALE
            grdtoollist.View.AddTextBoxColumn("SCALEX", 100)
                .SetIsReadOnly()
                .SetLabel("SCALE_X");
            grdtoollist.View.AddTextBoxColumn("SCALEY", 100)
                .SetIsReadOnly()
                .SetLabel("SCALE_Y");

            //비고
            grdtoollist.View.AddTextBoxColumn("DESCRIPTION", 200)
                .SetIsReadOnly()
                .SetLabel("REMARK");

            grdtoollist.View.PopulateColumns();


            //BBT JIG
            grdbbtjiglist.GridButtonItem = GridButtonItem.Export | GridButtonItem.Add | GridButtonItem.Delete;
            grdbbtjiglist.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            InitializeProductId_Popup2();

            //품목버전
            grdbbtjiglist.View.AddTextBoxColumn("PRODUCTDEFVERSION1", 80)
                .SetLabel("PRODUCTDEFVERSION")
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();
            //치공구 유형
            grdbbtjiglist.View.AddComboBoxColumn("DURABLECLASSID1", 100, new SqlQuery("GetTypeList", "10001", "CODECLASSID=ItemToolType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsReadOnly()
                .SetDefault("BBTJIG")
                .SetLabel("DURABLETYPE");
            //품목명
            grdbbtjiglist.View.AddTextBoxColumn("PRODUCTDEFNAME1", 300)
                .SetLabel("PRODUCTDEFNAME")
                .SetIsReadOnly();
            //SEQUENCE
            grdbbtjiglist.View.AddTextBoxColumn("SEQUENCE3", 300)
                .SetIsReadOnly()
                .SetIsHidden(); 
            //치공구 ID
            grdbbtjiglist.View.AddTextBoxColumn("DURABLEDEFID1", 100)
                .SetIsHidden()
                .SetIsReadOnly();
            //치공구명
            grdbbtjiglist.View.AddTextBoxColumn("DURABLEDEFNAME1", 300)
                .SetLabel("DURABLEDEFNAME")
                .SetIsReadOnly();
            //치공구 VERSION
            grdbbtjiglist.View.AddTextBoxColumn("DURABLEDEFVERSION1", 100)
                .SetIsHidden()
                .SetIsReadOnly();
            //치공구 유형
            grdbbtjiglist.View.AddTextBoxColumn("TOOLFORM1", 100)
                .SetIsReadOnly()
                .SetLabel("DURABLECATEGORYDETAILCODE");
            // 종류
            grdbbtjiglist.View.AddTextBoxColumn("TOOLKIND1", 150)
                .SetIsReadOnly()
                .SetLabel("CLASSIFY");


            //합수
            grdbbtjiglist.View.AddTextBoxColumn("SUMMARY1", 100)
                .SetIsReadOnly()
                .SetLabel("ARRAY");
            //구분
            grdbbtjiglist.View.AddComboBoxColumn("TOOLCLASS1", 100, new SqlQuery("GetTypeList", "10001", "CODECLASSID=ProductSpecToolClassify", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("CLASS")
                .SetIsReadOnly();
            //제작처6
            grdbbtjiglist.View.AddTextBoxColumn("MANUFACTURER1", 200)
                .SetIsReadOnly()
                .SetLabel("MANUFACTURER");
            //입고작업장
            grdbbtjiglist.View.AddTextBoxColumn("RECEIPTAREAID1", 200)
                .SetIsReadOnly()
                .SetLabel("RECEIPTAREA");

            //SCALE
            grdbbtjiglist.View.AddTextBoxColumn("SCALEX1", 100)
                .SetIsReadOnly()
                .SetLabel("SCALE_X");
            grdbbtjiglist.View.AddTextBoxColumn("SCALEY1", 100)
                .SetIsReadOnly()
                .SetLabel("SCALE_Y");

            //비고
            grdbbtjiglist.View.AddTextBoxColumn("DESCRIPTION1", 200)
                .SetIsReadOnly()
                .SetLabel("REMARK");

            grdbbtjiglist.View.PopulateColumns();




            //기타
            grdetclist.GridButtonItem = GridButtonItem.Export | GridButtonItem.Add | GridButtonItem.Delete;
            grdetclist.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;


            InitializeProductId_Popup3();

            //품목버전
            grdetclist.View.AddTextBoxColumn("PRODUCTDEFVERSION2", 80)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("PRODUCTDEFVERSION")
                .SetIsReadOnly();
            //치공구 유형
            grdetclist.View.AddComboBoxColumn("DURABLECLASSID2", 100, new SqlQuery("GetTypeList", "10001", "CODECLASSID=ItemToolType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsReadOnly()
                .SetLabel("DURABLETYPE")
                .SetDefault("ETC");
            //품목명
            grdetclist.View.AddTextBoxColumn("PRODUCTDEFNAME2", 300)
                .SetLabel("PRODUCTDEFNAME")
                .SetIsReadOnly();
            //SEQUENCE
            grdetclist.View.AddTextBoxColumn("SEQUENCE3", 300)
                .SetIsReadOnly()
                .SetIsHidden();
            //치공구 형식
            grdetclist.View.AddComboBoxColumn("TOOLFORM2", 100, new SqlQuery("GetTypeList", "10001", "CODECLASSID=ToolForm2", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("DURABLECATEGORYDETAILCODE")
                .SetTextAlignment(TextAlignment.Center);

      
            // 종류
            grdetclist.View.AddComboBoxColumn("TOOLKIND2", 150, new SqlQuery("GetToolTypeByClassId", "10002", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("CLASSIFY")
                .SetRelationIds("TOOLFORM2")
                .SetTextAlignment(TextAlignment.Center);


            //합수
            grdetclist.View.AddTextBoxColumn("SUMMARY2", 100)
                .SetLabel("ARRAY");
            //구분
            grdetclist.View.AddComboBoxColumn("TOOLCLASS2", 100, new SqlQuery("GetTypeList", "10001", "CODECLASSID=ProductSpecToolClassify", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("CLASS");

            //제작처6
            grdetclist.View.AddTextBoxColumn("MANUFACTURER2", 200)
                .SetLabel("MANUFACTURER");
            //입고작업장
            grdetclist.View.AddTextBoxColumn("RECEIPTAREAID2", 200)
                .SetLabel("RECEIPTAREA");

            //SCALE
            grdetclist.View.AddTextBoxColumn("SCALEX2", 100)
                .SetLabel("SCALE_X");
            grdetclist.View.AddTextBoxColumn("SCALEY2", 100)
                .SetLabel("SCALE_Y");

            //비고
            grdetclist.View.AddTextBoxColumn("DESCRIPTION2", 200)
                .SetLabel("REMARK");

            grdetclist.View.PopulateColumns();

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
                    if (row == null) return;
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
            this.grdtoollist.View.AddingNewRow += (s, e) =>
            {
                e.NewRow["SEQUENCE3"] = s.RowCount;
            };
            this.grdbbtjiglist.View.AddingNewRow += (s, e) =>
            {
                e.NewRow["SEQUENCE3"] = s.RowCount;
            };
            this.grdetclist.View.AddingNewRow += (s, e) =>
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

        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            SmartButton btn = sender as SmartButton;

            if (btn.Name.ToString().Equals("Print"))
            {

                if (productInfo.itemcodereturn().Equals("") || productInfo.itemcodereturn() == null)
                    return;

                DataSet dsReport = new DataSet();
                DataTable itemdata = new DataTable();


                #region 라벨

                itemdata.Columns.Add(new DataColumn("LBLCUSTOMERREV", typeof(string))); // 고객 REV
                itemdata.Columns.Add(new DataColumn("LBLENDUSER", typeof(string))); // ENDUSER
                itemdata.Columns.Add(new DataColumn("LBLITEMCODE", typeof(string))); // 품목코드
                itemdata.Columns.Add(new DataColumn("LBLITEMNAME", typeof(string))); // 품목명
                itemdata.Columns.Add(new DataColumn("LBLCUSTOMER", typeof(string))); // 고객명
                itemdata.Columns.Add(new DataColumn("LBLLAYER", typeof(string))); // 층수
                itemdata.Columns.Add(new DataColumn("LBLPRODUCTDEFVERSION", typeof(string))); // 내부REV
                itemdata.Columns.Add(new DataColumn("LBLPROJECTNAME", typeof(string))); //프로젝트명
                itemdata.Columns.Add(new DataColumn("LBLINPUTTYPE", typeof(string))); // 투입유형
                itemdata.Columns.Add(new DataColumn("LBLDELIVERYDATE", typeof(string))); // 납기일자
                itemdata.Columns.Add(new DataColumn("LBLOUTBOUNDFORMAT", typeof(string))); // 출고형태
                itemdata.Columns.Add(new DataColumn("LBLPRODUCTIONTYPE", typeof(string))); // 생산구분
                itemdata.Columns.Add(new DataColumn("LBLPCSSIZE", typeof(string))); // PCS SIZE
                itemdata.Columns.Add(new DataColumn("LBLPNLSIZE", typeof(string))); // PNL SIZE
                itemdata.Columns.Add(new DataColumn("LBLPCSPNL", typeof(string))); // PCS PNL
                itemdata.Columns.Add(new DataColumn("LBLPNLMM", typeof(string))); // PNL MM
                itemdata.Columns.Add(new DataColumn("LBLPCSMM", typeof(string))); // PCS MM
                itemdata.Columns.Add(new DataColumn("LBLPRODUCTSHAPE", typeof(string))); // 제품타입
                itemdata.Columns.Add(new DataColumn("LBLARRAYDIAGONALLENGTH", typeof(string))); // ARY 대각선길이
                itemdata.Columns.Add(new DataColumn("LBLMEASUREMENT", typeof(string))); // 치수측정
                itemdata.Columns.Add(new DataColumn("LBLRELIABILITY", typeof(string))); // 신뢰성
                itemdata.Columns.Add(new DataColumn("LBLHAZARDOUSSUBSTANCES", typeof(string))); // 유해물질
                itemdata.Columns.Add(new DataColumn("LBLARYSIZE", typeof(string))); // ARYSIZE
                itemdata.Columns.Add(new DataColumn("LBLPCSARY", typeof(string))); // PCS ARY
                itemdata.Columns.Add(new DataColumn("LBLX-OUTPCS", typeof(string))); // X-OUT PCS
                itemdata.Columns.Add(new DataColumn("LBLX-OUTPERCENT", typeof(string))); // X-OUT PRECENT
                itemdata.Columns.Add(new DataColumn("LBLISWEEKMNG", typeof(string))); // 주차관리
                itemdata.Columns.Add(new DataColumn("LBLOXIDE", typeof(string))); // OXIDE
                itemdata.Columns.Add(new DataColumn("LBLSEPARATINGPORTION", typeof(string))); // 분리부
                itemdata.Columns.Add(new DataColumn("LBLASSY", typeof(string))); // ASSY
                itemdata.Columns.Add(new DataColumn("LBLULMARK", typeof(string))); // ULMARK
                itemdata.Columns.Add(new DataColumn("LBLPLATINGSPEC", typeof(string))); // 도금사양
                itemdata.Columns.Add(new DataColumn("LBLIMPEDANCECHECK", typeof(string))); // 임피던스 유무
                itemdata.Columns.Add(new DataColumn("LBLIMPEDANCESPEC2", typeof(string))); // 임피던스 SPEC
                itemdata.Columns.Add(new DataColumn("LBLIMPEDANCETYPE2", typeof(string))); // 임피던스 구분
                itemdata.Columns.Add(new DataColumn("LBLIMPEDANCELAYER", typeof(string))); // 임피던스 적용층
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
                itemdata.Columns.Add(new DataColumn("LBLETCMATERIAL", typeof(string))); // 기타 자재정보
                itemdata.Columns.Add(new DataColumn("LBLINKINFO", typeof(string))); // 잉크정보
                itemdata.Columns.Add(new DataColumn("LBLRAWSUBMATERIAL", typeof(string))); // 원자재정보
                itemdata.Columns.Add(new DataColumn("LBLSTDVALUE", typeof(string))); // 기준값PLATINGSPEC
                itemdata.Columns.Add(new DataColumn("LBLPLANECUFOILUM", typeof(string))); // 면동박
                itemdata.Columns.Add(new DataColumn("LBLHOLEINSIDEWALLUM", typeof(string))); // 홀내벽
                itemdata.Columns.Add(new DataColumn("LBLSURFACETOTAL", typeof(string))); // 표면TOTAL
                itemdata.Columns.Add(new DataColumn("LBLDIMPLEUM", typeof(string))); // DIMPLE
                itemdata.Columns.Add(new DataColumn("LBLOVERFILLUM", typeof(string))); // OVERFILL
                itemdata.Columns.Add(new DataColumn("LBLNIOSP", typeof(string))); // NI/OSP
                itemdata.Columns.Add(new DataColumn("LBLAU", typeof(string))); // AU
                itemdata.Columns.Add(new DataColumn("LBLPDTIN", typeof(string))); // PD/SN
                itemdata.Columns.Add(new DataColumn("LBLPRODUCTLEVEL", typeof(string))); // 제품 면적
                itemdata.Columns.Add(new DataColumn("LBLSCRAPSURFACE", typeof(string))); // 스크랩 면적
                itemdata.Columns.Add(new DataColumn("LBLPLATINGAREASQMM", typeof(string))); // HOLE 도금면적1
                itemdata.Columns.Add(new DataColumn("LBLHOLEPLATINGAREA2", typeof(string))); // HOLE 도금면적2
                itemdata.Columns.Add(new DataColumn("LBLSPECCHANGE", typeof(string))); // 사양변경내용
                itemdata.Columns.Add(new DataColumn("LBLCOMMENT", typeof(string))); // 특이사항
                itemdata.Columns.Add(new DataColumn("LBLBOMINFO", typeof(string))); // BOM 정보
                itemdata.Columns.Add(new DataColumn("LBLCIRCUITSPEC", typeof(string))); // 회로사양
                itemdata.Columns.Add(new DataColumn("LBLMATERIALTYPE", typeof(string))); // 자재유형
                itemdata.Columns.Add(new DataColumn("LBLMATERIALDEF", typeof(string))); // 자재코드
                itemdata.Columns.Add(new DataColumn("LBLCONSUMABLEDEFNAME", typeof(string))); // 자재명
                itemdata.Columns.Add(new DataColumn("LBLSPEC", typeof(string))); // 규격

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
                itemdata.Columns.Add(new DataColumn("LBLMINSIZEINPUT", typeof(string))); // MINSIZE
                itemdata.Columns.Add(new DataColumn("LBLBREADTH", typeof(string))); // 폭
                itemdata.Columns.Add(new DataColumn("LBLINTERVAL", typeof(string))); // 간격
                itemdata.Columns.Add(new DataColumn("LBLLAND", typeof(string))); // LAND
                itemdata.Columns.Add(new DataColumn("LBLHOLE", typeof(string))); // HOLE
                itemdata.Columns.Add(new DataColumn("LBLOUTERLAYERCIRCUIT", typeof(string))); // 외층회로
                itemdata.Columns.Add(new DataColumn("LBLINNERLAYERCIRCUIT", typeof(string))); // 내층회로
                itemdata.Columns.Add(new DataColumn("LBLOLBCIRCUIT", typeof(string))); // OLB 회로
                itemdata.Columns.Add(new DataColumn("LBLMVH", typeof(string))); // MVH
                itemdata.Columns.Add(new DataColumn("LBLIVH", typeof(string))); // IVH
                itemdata.Columns.Add(new DataColumn("LBLPTH", typeof(string))); // PTH
                itemdata.Columns.Add(new DataColumn("LBLELONGATIONCHECK1", typeof(string))); // 연신율유뮤
                itemdata.Columns.Add(new DataColumn("LBLMVHKIND", typeof(string))); // MVH종류
                itemdata.Columns.Add(new DataColumn("LBLLASERSIZE", typeof(string))); // LASER SIZE
                itemdata.Columns.Add(new DataColumn("LBLMVHLAND", typeof(string))); // MVH LAND
                itemdata.Columns.Add(new DataColumn("LBLAPPLYMVH", typeof(string))); // MVH 가공
                itemdata.Columns.Add(new DataColumn("LBLANNULARING", typeof(string))); // ANNULARING
                itemdata.Columns.Add(new DataColumn("LBLCONFORMALSIZE", typeof(string))); // CONFORMAL
                itemdata.Columns.Add(new DataColumn("LBLMVHMACHINGDEPTH", typeof(string))); // MVH 깊이
                itemdata.Columns.Add(new DataColumn("LBLASPECTRATIO", typeof(string))); // ASPECT RATIO
                itemdata.Columns.Add(new DataColumn("LBLFLATNESSAREA", typeof(string))); // 평탄도 부위
                itemdata.Columns.Add(new DataColumn("LBLFLATNESSVALUEUM", typeof(string))); // 평탄도 값
                itemdata.Columns.Add(new DataColumn("LBLCOPPERFOILUPLAYER", typeof(string))); // 동박방향 위
                itemdata.Columns.Add(new DataColumn("LBLCOPPERFOILDOWNLAYER", typeof(string))); // 동박방향 아래
                itemdata.Columns.Add(new DataColumn("LBLCUSTOMERCRITERIAUM", typeof(string))); // 고객기준 UM
                itemdata.Columns.Add(new DataColumn("LBLPLATINGCONDITIONUM", typeof(string))); // 도금 조건 UM
                itemdata.Columns.Add(new DataColumn("LBLOUTERCOPPERPLATING", typeof(string))); // 외층동도금
                itemdata.Columns.Add(new DataColumn("LBLINNERCOPPERPLATING1", typeof(string))); // 내층동도금1
                itemdata.Columns.Add(new DataColumn("LBLINNERCOPPERPLATING2", typeof(string))); // 내층동도금 2
                itemdata.Columns.Add(new DataColumn("LBLINNERCOPPERPLATING3", typeof(string))); // 내층동도금 3
                itemdata.Columns.Add(new DataColumn("LBLSURFACEPLATING1", typeof(string))); // 표면도금1
                itemdata.Columns.Add(new DataColumn("LBLSURFACEPLATING2", typeof(string))); // 표면도금2
                itemdata.Columns.Add(new DataColumn("LBLSURFACEPLATING3", typeof(string))); // 표면도금3
                itemdata.Columns.Add(new DataColumn("LBLETC", typeof(string))); // 기타정보
                itemdata.Columns.Add(new DataColumn("LBLTHICKPOSITION", typeof(string))); // 부위
                itemdata.Columns.Add(new DataColumn("LBLTHICKNESSSPEC", typeof(string))); //  두께사양
                itemdata.Columns.Add(new DataColumn("LBLRTRSHEET", typeof(string))); //  두께사양
                itemdata.Columns.Add(new DataColumn("LBLSPECIALNOTE", typeof(string))); // 특이사항
                itemdata.Columns.Add(new DataColumn("LBLTHICKEXPERIMENTALVALUE", typeof(string))); // 실측치
                itemdata.Columns.Add(new DataColumn("LBLTHICKTHEORETICALVALUE", typeof(string))); // 이론치
                itemdata.Columns.Add(new DataColumn("LBLTHICKSPEC", typeof(string))); //  spec
                itemdata.Columns.Add(new DataColumn("LBLRECEIVENEWDATA", typeof(string))); // 신규데이터 접수
                itemdata.Columns.Add(new DataColumn("LBLINPUTNUMAXIS", typeof(string))); //  투입수축
                itemdata.Columns.Add(new DataColumn("LBLREGISTRATIONDATE", typeof(string))); // 등록일자
                itemdata.Columns.Add(new DataColumn("LBLMOLDANDWOODEN", typeof(string))); // 금형/목형
                itemdata.Columns.Add(new DataColumn("LBLBBTANDJIG", typeof(string))); //  BBT/JIG
                itemdata.Columns.Add(new DataColumn("LBLETCPRODUCT", typeof(string))); // 기타
                itemdata.Columns.Add(new DataColumn("LBLMATERIALDETAILTYPE", typeof(string))); // 자재유형
                itemdata.Columns.Add(new DataColumn("LBLCOMPONENTITEMNAME", typeof(string))); // 부자재명
                itemdata.Columns.Add(new DataColumn("LBLMATERIALNAME", typeof(string))); // 자재명
                itemdata.Columns.Add(new DataColumn("LBLSPECDETAILFROM", typeof(string))); // 잉크품목구분
                itemdata.Columns.Add(new DataColumn("LBLDETAILNAME", typeof(string))); // 부자재명
                itemdata.Columns.Add(new DataColumn("LBLCOLOR", typeof(string))); // 자재명
                itemdata.Columns.Add(new DataColumn("LBLSPECHISTORYCARD", typeof(string))); // 사양이력카드
                itemdata.Columns.Add(new DataColumn("LBLMANAGER", typeof(string))); // 담당자
                itemdata.Columns.Add(new DataColumn("LBLSTAFF", typeof(string))); // 검토자
                itemdata.Columns.Add(new DataColumn("LBLAPPROVER", typeof(string))); // 승인자


                itemdata.Columns.Add(new DataColumn("LBLTOOLKIND", typeof(string))); //  종류
                itemdata.Columns.Add(new DataColumn("LBLTOOLFORM", typeof(string))); // TOOL 형식
                itemdata.Columns.Add(new DataColumn("LBLTOOLCLASS", typeof(string))); // 구분
                itemdata.Columns.Add(new DataColumn("LBLSUMMARY", typeof(string))); // 합수
                itemdata.Columns.Add(new DataColumn("LBLMANUFACTURER", typeof(string))); // 제작처
                itemdata.Columns.Add(new DataColumn("LBLRECEIPTAREAID", typeof(string))); // 입고작업장
                itemdata.Columns.Add(new DataColumn("LBLSCALEX", typeof(string))); // SCALEX
                itemdata.Columns.Add(new DataColumn("LBLSCALEY", typeof(string))); // SCALEY
                itemdata.Columns.Add(new DataColumn("LBLDESCRIPTION", typeof(string))); // 비고
    


                #endregion

                #region row 생성 및 다국어 처리 

                DataRow row = itemdata.NewRow();



                row["LBLTOOLKIND"] = Language.Get("TOOLKIND"); // 종류
                row["LBLTOOLFORM"] = Language.Get("LBLPLATINGAREASQMM"); // TOOL 형식
                row["LBLTOOLCLASS"] = Language.Get("CLASS"); // 구분
                row["LBLSUMMARY"] = Language.Get("SUMMARY"); // 합수
                row["LBLMANUFACTURER"] = Language.Get("MANUFACTURER"); // 제작처
                row["LBLRECEIPTAREAID"] = Language.Get("RECEIPTAREA"); // 입고작업장
                row["LBLSCALEX"] = Language.Get("SCALEX"); // SCALE X
                row["LBLSCALEY"] = Language.Get("SCALEY"); // SCALE Y
                row["LBLDESCRIPTION"] = Language.Get("REMARK"); // 비고
                row["LBLMANAGER"] = Language.Get("MANAGER"); // 담당자
                row["LBLSTAFF"] = Language.Get("STAFF"); // 검토자
                row["LBLAPPROVER"] = Language.Get("APPROVER"); // 승인자
                row["LBLSPECHISTORYCARD"] = Language.Get("SPECHISTORYCARD"); // 사양이력카드
                row["LBLINPUTTYPE"] = Language.Get("INPUTTYPE"); // 납기일자
                row["LBLMATERIALDETAILTYPE"] = Language.Get("MATERIALTYPE"); // 자재유형
                row["LBLCOMPONENTITEMNAME"] = Language.Get("COMPONENTITEMNAME"); // 부자재명
                row["LBLMATERIALNAME"] = Language.Get("MATERIALNAME"); // 자재명
                row["LBLWORKMETHOD"] = Language.Get("ISMAINSEGMENT"); // 작업방식
                row["LBLSPECDETAILFROM"] = Language.Get("INKITEMCLASSIFY"); // 잉크품목구분
                row["LBLDETAILNAME"] = Language.Get("SUBSIDIARYNAME"); // 부자재명
                row["LBLCOLOR"] = Language.Get("COLOR"); // 색상
                row["LBLDELIVERYDATE"] = Language.Get("DELIVERYDATE"); // 납기일자
                row["LBLENDUSER"] = Language.Get("ENDUSER"); // enduser
                row["LBLPROJECTNAME"] = Language.Get("PROJECTNAME"); // 프로젝트명
                row["LBLOUTBOUNDFORMAT"] = Language.Get("OUTBOUNDFORMAT"); // 
                row["LBLPCSMM"] = Language.Get("PCSMM"); // PCSMM
                row["LBLPNLMM"] = Language.Get("PNLMM"); // PNLMM
                row["LBLPCSPNL"] = Language.Get("PCSPNL"); // PCSPNL
                row["LBLPRODUCTLEVEL"] = Language.Get("PRODUCTLEVEL"); //제품등급
                row["LBLPCSSIZE"] = Language.Get("PCSSIZE"); // PCSSIZE
                row["LBLARRAYDIAGONALLENGTH"] = Language.Get("ARRAYDIAGONALLENGTH"); // ARY 대각선길이
                row["LBLARYSIZE"] = Language.Get("ARYSIZE"); // ARYSIZE
                row["LBLPCSARY"] = Language.Get("PCSARY"); // PCS ARY
                row["LBLX-OUTPCS"] = Language.Get("X-OUTPCS"); //  X-OUT PCS
                row["LBLX-OUTPERCENT"] = Language.Get("X-OUTPERCENT"); // X-OUT PRECENT
                row["LBLISWEEKMNG"] = Language.Get("ISWEEKMNG"); // 주차관리
                row["LBLARRAYDIAGONALLENGTH"] = Language.Get("ARRAYDIAGONALLENGTH"); // ARY 대각선길이
                row["LBLARYSIZE"] = Language.Get("ARYSIZE"); // ARY SIZE
                row["LBLPCSARY"] = Language.Get("PCSARY"); // PCS ARY
                row["LBLX-OUTPCS"] = Language.Get("X-OUTPCS"); // X-OUT PCS
                row["LBLOXIDE"] = Language.Get("OXIDE"); //OXIDE
                row["LBLSEPARATINGPORTION"] = Language.Get("SEPARATINGPORTION"); // 분리부
                row["LBLASSY"] = Language.Get("ASSY"); // ASSY
                row["LBLULMARK"] = Language.Get("ULMARK"); // ULMARK

                row["LBLMOLDANDWOODEN"] = Language.Get("MOLDANDWOODEN2"); // 금형/목형
                row["LBLBBTANDJIG"] = Language.Get("BBTANDJIG2"); // BBT/JIG
                row["LBLETCPRODUCT"] = Language.Get("ETCSPEC2"); // 기타
                row["LBLTHICKPOSITION"] = Language.Get("THICKPOSITION"); // 부위
                row["LBLTHICKNESSSPEC"] = Language.Get("THICKNESSSPEC"); // 두께사양
                row["LBLSPECCHANGE"] = Language.Get("SPECCHANGE"); // 사양변경내용
                row["LBLSPECIALNOTE"] = Language.Get("SPECIALNOTE"); // 특이사항
                row["LBLTHICKEXPERIMENTALVALUE"] = Language.Get("THICKEXPERIMENTALVALUE"); // 실측치
                row["LBLTHICKTHEORETICALVALUE"] = Language.Get("THICKTHEORETICALVALUE"); // 이론치
                row["LBLTHICKSPEC"] = Language.Get("THICKSPEC"); // spec
                row["LBLRECEIVENEWDATA"] = Language.Get("RECEIVENEWDATA"); // 신규데이터 접수
                row["LBLINPUTNUMAXIS"] = Language.Get("INPUTNUMAXIS"); // 투입수축
                row["LBLREGISTRATIONDATE"] = Language.Get("REGISTRATIONDATE"); // 등록일자
                row["LBLETC"] = Language.Get("ETCINFO"); // 기타정보
                row["LBLSURFACEPLATING1"] = Language.Get("SURFACEPLATING1"); // 표면도금1
                row["LBLSURFACEPLATING2"] = Language.Get("SURFACEPLATING2"); // 표면도금2
                row["LBLSURFACEPLATING3"] = Language.Get("SURFACEPLATING3"); // 표면도금3
                row["LBLCUSTOMERCRITERIAUM"] = Language.Get("CUSTOMERCRITERIAUM"); // 고객기준 UM
                row["LBLPLATINGCONDITIONUM"] = Language.Get("PLATINGCONDITIONUM"); // 도금 조건 UM
                row["LBLOUTERCOPPERPLATING"] = Language.Get("OUTERCOPPERPLATING"); // 외층동도금
                row["LBLINNERCOPPERPLATING1"] = Language.Get("INNERCOPPERPLATING1"); // 내층동도금1
                row["LBLINNERCOPPERPLATING2"] = Language.Get("INNERCOPPERPLATING2"); // 내층동도금2
                row["LBLINNERCOPPERPLATING3"] = Language.Get("INNERCOPPERPLATING3"); // 내층동도금3
                row["LBLMINSIZEINPUT"] = Language.Get("MINSIZEINPUT"); // MINSIZE
                row["LBLBREADTH"] = Language.Get("BREADTH"); // 폭
                row["LBLINTERVAL"] = Language.Get("INTERVAL"); // 간격
                row["LBLLAND"] = Language.Get("LAND"); // LAND
                row["LBLITEMNAME"] = Language.Get("ITEMNAME"); // HOLE
                row["LBLOUTERLAYERCIRCUIT"] = Language.Get("OUTERLAYERCIRCUIT"); // 외층회로
                row["LBLINNERLAYERCIRCUIT"] = Language.Get("INNERLAYERCIRCUIT"); // 내층회로
                row["LBLOLBCIRCUIT"] = Language.Get("OLBCIRCUIT"); // OLB 회로
                row["LBLMVH"] = Language.Get("MVH"); // MVH
                row["LBLIVH"] = Language.Get("IVH"); // IVH
                row["LBLPTH"] = Language.Get("PTH"); // PTH
                row["LBLELONGATIONCHECK1"] = Language.Get("ELONGATIONCHECK1"); // 연신율 유무
                row["LBLMVHKIND"] = Language.Get("MVHKIND"); // MVH종류
                row["LBLLASERSIZE"] = Language.Get("LASERSIZE"); //  LASER SIZE
                row["LBLMVHLAND"] = Language.Get("MVHLAND"); // MVH LAND
                row["LBLAPPLYMVH"] = Language.Get("APPLYMVH"); // MVH 가공
                row["LBLANNULARING"] = Language.Get("ANNULARING"); // ANNULARING
                row["LBLCONFORMALSIZE"] = Language.Get("CONFORMALSIZE"); // CONFORMAL
                row["LBLMVHMACHINGDEPTH"] = Language.Get("MVHMACHINGDEPTH"); //MVH 깊이
                row["LBLASPECTRATIO"] = Language.Get("ASPECTRATIO"); // SPECT RATIO
                row["LBLFLATNESSAREA"] = Language.Get("FLATNESSAREA"); //  평탄도 부위
                row["LBLFLATNESSVALUEUM"] = Language.Get("FLATNESSVALUEUM"); // 평탄도 값
                row["LBLCOPPERFOILUPLAYER"] = Language.Get("COPPERFOILUPLAYER"); // 동박방향 위
                row["LBLCOPPERFOILDOWNLAYER"] = Language.Get("COPPERFOILDOWNLAYER"); // 동박방향 아래
                row["LBLSURFACESPEC"] = Language.Get("SURFACESPEC2"); // 표면처리사양
                row["LBLCUSTOMERREV"] = Language.Get("CUSTOMERREV"); // 고객 REV

                row["LBLITEMCODE"] = Language.Get("ITEMCODE"); // 품목코드
                row["LBLITEMNAME"] = Language.Get("ITEMNAME"); // 품목명
                row["LBLCUSTOMER"] = Language.Get("TXTCUSTOMERID"); // 고객 ID명
                row["LBLLAYER"] = Language.Get("LAYER"); // 층수
                row["LBLUOM"] = Language.Get("UOM"); // UOM
                row["LBLETCMATERIAL"] = Language.Get("ETCMATERIAL"); // 기타 부자재정보
                row["LBLRAWSUBMATERIAL"] = Language.Get("RAWSUBMATERIAL"); // 원 부자재 정보
                row["LBLINKINFO"] = Language.Get("INKINFO"); // 잉크 정보
                row["LBLPRODUCTDEFVERSION"] = Language.Get("PRODUCTDEFVERSION"); // 내부 REV

                row["LBLPRODUCTIONTYPE"] = Language.Get("PRODUCTIONTYPE"); // 생산구분V
                row["LBLRTRSHEET"] = Language.Get("RTRSHEET"); // ROLL SHEET
                row["LBLPRODUCTSHAPE"] = Language.Get("PRODUCTSHAPE"); // 제품타입
                row["LBLMEASUREMENT"] = Language.Get("MEASUREMENT"); // 치수측정
                row["LBLRELIABILITY"] = Language.Get("RELIABILITY"); // 신뢰성
                row["LBLHAZARDOUSSUBSTANCES"] = Language.Get("HAZARDOUSSUBSTANCES"); // 유해물질

                row["LBLPLATINGSPEC"] = Language.Get("PLATINGSPEC2"); // 도금사양
                row["LBLIMPEDANCECHECK"] = Language.Get("IMPEDANCECHECK"); // 임피던스유무
                row["LBLIMPEDANCESPEC2"] = Language.Get("IMPEDANCESPEC2"); // 임피던스 SPEC
                row["LBLIMPEDANCETYPE2"] = Language.Get("IMPEDANCETYPE2"); //  임피던스 구분
                row["LBLIMPEDANCELAYER"] = Language.Get("IMPEDANCELAYER"); //  임피던스 적용층
  
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
                row["LBLPLANECUFOILUM"] = Language.Get("PLANECUFOILUM"); // 도금두께
                row["LBLHOLEINSIDEWALLUM"] = Language.Get("HOLEINSIDEWALLUM"); // 홀내벽
                row["LBLSURFACETOTAL"] = Language.Get("SURFACETOTAL"); //표면TOTAL
                row["LBLDIMPLEUM"] = Language.Get("DIMPLEUM"); // DIMPLE
                row["LBLOVERFILLUM"] = Language.Get("OVERFILLUM"); // OVER FILL
                row["LBLNIOSP"] = Language.Get("NIOSP"); // NIOSP
                row["LBLAU"] = Language.Get("AU"); // AU
                row["LBLPDTIN"] = Language.Get("PDTIN"); // PDSN
                row["LBLHOLE"] = Language.Get("HOLE"); // PDSN
                row["LBLSCRAPSURFACE"] = Language.Get("SCRAPSURFACE"); // 스크랩면적
                row["LBLPLATINGAREASQMM"] = Language.Get("PLATINGAREASQMM"); // 도금면적 SQ MM
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
                row["LBLREMARK"] = Language.Get("REMARK"); // 비고
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
                
      
            

               DataTable sumdt = new DataTable();
               var values = Conditions.GetValues();

               Dictionary<string, object> Param = new Dictionary<string, object>();
               Param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
               Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
               Param.Add("P_PRODUCTDEFID", Format.GetString(values["P_ITEMID"]).Split('|')[0]);
               Param.Add("P_PRODUCTDEFVERSION", Format.GetString(values["P_ITEMID"]).Split('|')[1]);
               DataTable routing = SqlExecuter.Query("GetRoutingOperationList", "10001", Param);
                DataTable layer = grdLayerComposition.DataSource as DataTable;
                DataTable ink = grdInkInfo.DataSource as DataTable;
                DataTable submaterial = grdSubMaterial.DataSource as DataTable;
                DataTable dtmold = grdtoollist.DataSource as DataTable;
                DataTable dtjig = grdbbtjiglist.DataSource as DataTable;
                DataTable dtetc = grdetclist.DataSource as DataTable;

                productInfo.ComboChange(1);
                productSpec.ComboChange(1);

                // 데이터셋에 각 화면에서 가져온 값들을 넣는다
                DataSet ds = new DataSet();
                ds.Tables.Add(platingSpec.Platingreturn());
                ds.Tables.Add(circuitSpec.PSRreturn());
                ds.Tables.Add(itemdata);
                ds.Tables.Add(productInfo.productinforeturn());
                ds.Tables.Add(productSpec.productspecreturn());
                ds.Tables.Add(etcInfo.Etcreturn());

              // merge를 쓰면 로우가 불필요하게 늘어나기 때문에 아래 함수를 이용하여 컬럼과 데이터를 합쳐서 sumdt 테이블에 넣는다
                  sumdt = ColumnAdd(sumdt, ds);
                  sumdt = DataAdd(sumdt, ds);
               
                //작성한 리포트
                Assembly assembly = Assembly.GetAssembly(this.GetType());
                Stream stream = assembly.GetManifestResourceStream("Micube.SmartMES.StandardInfo.Report.ItemSpec_IFC.repx");
                XtraReport report = XtraReport.FromStream(stream);
                 
                //그리드의 데이터가 이미 다른 Dataset에 포함되어있기때문에 새로운 DataTable을 만들어 Copy 한다.
                DataTable layertable = layer.Copy();
                DataTable subtable = submaterial.Copy();
                DataTable inktalbe = ink.Copy();
                DataTable moldtable = dtmold.Copy();
                DataTable jigtable = dtjig.Copy();
                DataTable etctalbe = dtetc.Copy();
                DataTable inkmaterial = new DataTable();
                DataSet ds2 = new DataSet();

                //릴레이션을 하기위한 컬럼추가
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
                inktalbe.TableName = "ink";
                subtable.TableName = "submate";
                ds.Tables.Add(inktalbe);
                ds.Tables.Add(subtable);
                inktalbe.Columns.Remove("ITEMID");
                inktalbe.Columns.Remove("ITEMVERSION");
                inkmaterial = ColumnAdd(inkmaterial, ds);
                inkmaterial = DataAdd(inkmaterial, ds);


                layertable.TableName = "layer";
                routingtable.TableName = "routing";


                sumdt.TableName = "label";
                dsReport.Tables.Add(sumdt);
                dsReport.Tables.Add(layertable);
                dsReport.Tables.Add(routingtable);
                dsReport.Tables.Add(inkmaterial);

                moldtable.TableName = "mold";
                jigtable.TableName = "jig";
                etctalbe.TableName = "etc";
                dsReport.Tables.Add(moldtable);
                dsReport.Tables.Add(jigtable);
                dsReport.Tables.Add(etctalbe);
                moldtable.Columns.Add(new DataColumn("ITEMCODE", typeof(string)));
                jigtable.Columns.Add(new DataColumn("ITEMCODE", typeof(string)));
                etctalbe.Columns.Add(new DataColumn("ITEMCODE", typeof(string)));
                inkmaterial.Columns.Add(new DataColumn("ITEMCODE", typeof(string)));
                for (int i = 0; i < inkmaterial.Rows.Count; i++)
                {
                    inkmaterial.Rows[i]["ITEMCODE"] = sumdt.Rows[0]["ITEMID"];
                }
                for (int i = 0; i < moldtable.Rows.Count; i++)
                {
                    moldtable.Rows[i]["ITEMCODE"] = sumdt.Rows[0]["ITEMID"];
                }
                for (int i = 0; i < jigtable.Rows.Count; i++)
                {
                    jigtable.Rows[i]["ITEMCODE"] = sumdt.Rows[0]["ITEMID"];
                }
                for (int i = 0; i < etctalbe.Rows.Count; i++)
                {
                    etctalbe.Rows[i]["ITEMCODE"] = sumdt.Rows[0]["ITEMID"];
                }



                if (layertable.Rows.Count > 0)  // 원 자재 릴레이션
                {
                    DataRelation relation = new DataRelation("layer", sumdt.Columns["ITEMID"], layertable.Columns["ITEMCODE"]);
                    dsReport.Relations.Add(relation);
                }

                if (routing.Rows.Count > 0) // 품목 라우팅 
                {
                    DataRelation relation = new DataRelation("routing", sumdt.Columns["ITEMID"], routingtable.Columns["ITEMCODE"]);
                    dsReport.Relations.Add(relation);
                }
                if (inkmaterial.Rows.Count > 0) // sub자재 + ink 
                {
                    DataRelation relation = new DataRelation("inksub", sumdt.Columns["ITEMID"], inkmaterial.Columns["ITEMCODE"]);
                    dsReport.Relations.Add(relation);
                }


                if (moldtable.Rows.Count > 0)  // 원 자재 릴레이션
                {
                    DataRelation relation = new DataRelation("mold", sumdt.Columns["ITEMID"], moldtable.Columns["ITEMCODE"]);
                    dsReport.Relations.Add(relation);
                }

                if (jigtable.Rows.Count > 0) // 품목 라우팅 
                {
                    DataRelation relation = new DataRelation("bbt", sumdt.Columns["ITEMID"], jigtable.Columns["ITEMCODE"]);
                    dsReport.Relations.Add(relation);
                }
                if (etctalbe.Rows.Count > 0) // sub자재 + ink 
                {
                    DataRelation relation = new DataRelation("etc", sumdt.Columns["ITEMID"], etctalbe.Columns["ITEMCODE"]);
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


                DetailReportBand detailReport = report.Bands["DetailReport2"] as DetailReportBand;  // 원자재컬럼
                detailReport.DataSource = dsReport;
                detailReport.DataMember = "rein";

                Band detailBand = detailReport.Bands["GroupHeader4"];
                SetReportControlDataBinding(detailBand.Controls, sumdt);

                detailReport = report.Bands["DetailReport2"] as DetailReportBand; //  원 자재 그리드
                detailReport.DataSource = dsReport;
                detailReport.DataMember = "layer";

                detailBand = detailReport.Bands["Detail3"];
                SetReportControlDataBinding2(detailBand.Controls, dsReport, "layer");


                detailReport = report.Bands["DetailReport4"] as DetailReportBand;  //기타 잉크 컬럼
                detailReport.DataSource = dsReport;
                detailReport.DataMember = "rein";
            
                detailBand = detailReport.Bands["GroupHeader5"];
                SetReportControlDataBinding(detailBand.Controls, sumdt);


                detailReport = report.Bands["DetailReport4"] as DetailReportBand; // 자재 그리드
                detailReport.DataSource = dsReport;
                detailReport.DataMember = "inksub";

                detailBand = detailReport.Bands["Detail5"];
                SetReportControlDataBinding2(detailBand.Controls, dsReport, "inksub");


                detailReport = report.Bands["DetailReport8"] as DetailReportBand;  //회로 사양
                detailReport.DataSource = dsReport;
                detailReport.DataMember = "rein";

                detailBand = detailReport.Bands["Detail9"];
                SetReportControlDataBinding(detailBand.Controls, sumdt);

                detailReport = report.Bands["DetailReport1"] as DetailReportBand;  //금형/목형 컬럼
                detailReport.DataSource = dsReport;
                detailReport.DataMember = "rein";

                detailBand = detailReport.Bands["GroupHeader1"];
                SetReportControlDataBinding(detailBand.Controls, sumdt);


                detailReport = report.Bands["DetailReport1"] as DetailReportBand; // 금형/목형 그리드
                detailReport.DataSource = dsReport;
                detailReport.DataMember = "mold";

                detailBand = detailReport.Bands["Detail2"];
                SetReportControlDataBinding2(detailBand.Controls, dsReport, "mold");


                detailReport = report.Bands["DetailReport7"] as DetailReportBand;  //BBT JIG 컬럼
                detailReport.DataSource = dsReport;
                detailReport.DataMember = "rein";

                detailBand = detailReport.Bands["GroupHeader2"];
                SetReportControlDataBinding(detailBand.Controls, sumdt);


                detailReport = report.Bands["DetailReport7"] as DetailReportBand; // BBT JIG 그리드
                detailReport.DataSource = dsReport;
                detailReport.DataMember = "jig";

                detailBand = detailReport.Bands["Detail8"];
                SetReportControlDataBinding2(detailBand.Controls, dsReport, "jig");

                detailReport = report.Bands["DetailReport"] as DetailReportBand;  //기타 컬럼
                detailReport.DataSource = dsReport;
                detailReport.DataMember = "rein";

                detailBand = detailReport.Bands["GroupHeader3"];
                SetReportControlDataBinding(detailBand.Controls, sumdt);


                detailReport = report.Bands["DetailReport"] as DetailReportBand; // 기타 그리드
                detailReport.DataSource = dsReport;
                detailReport.DataMember = "etc";

                detailBand = detailReport.Bands["Detail1"];
                SetReportControlDataBinding2(detailBand.Controls, dsReport, "etc");

                detailReport = report.Bands["DetailReport9"] as DetailReportBand;  //라우팅 컬럼
                detailReport.DataSource = dsReport;
                detailReport.DataMember = "rein";

                detailBand = detailReport.Bands["GroupHeader6"];
                SetReportControlDataBinding(detailBand.Controls, sumdt);


                detailReport = report.Bands["DetailReport9"] as DetailReportBand; // 라우팅 그리드
                detailReport.DataSource = dsReport;
                detailReport.DataMember = "routing";

                detailBand = detailReport.Bands["Detail10"];
                SetReportControlDataBinding2(detailBand.Controls, dsReport, "routing");



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
                productInfo.ComboChange(2);
                productSpec.ComboChange(2);

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
                DataSet dsCircuit = circuitSpec.Save();

                DataTable dtCircuit = dsCircuit.Tables[0];

                DataTable dtmvh = dsCircuit.Tables[1];

                Dictionary<string, object> impedance = circuitSpec.Save2();

                //기타정보
                DataTable dtThick = new DataTable();
                Dictionary<string, object> etcInfoPairs = etcInfo.Save(out dtThick);

                // 잉크정보
                DataTable dtInkInfo = this.grdInkInfo.GetChangedRows();

                // 부자재 정보
                DataTable dtSubMaterial = this.grdSubMaterial.GetChangedRows();

                //도금 정보
                DataTable dtPlatingInfo = platingSpec.Save();

                DataTable dttoolinfo = grdtoollist.GetChangedRows();
                DataTable dtbbtinfo = grdbbtjiglist.GetChangedRows();
                DataTable dtetcinfo = grdetclist.GetChangedRows();

                //치공구 정보 


                MessageWorker worker = new MessageWorker("SaveProductSpec2");
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
					{ "impedance", impedance },
					{ "toolinfo", dttoolinfo },
					{ "etcinfo", dtetcinfo },
					{ "bbtinfo", dtbbtinfo }


				});
                worker.Execute();
                ShowMessage("SuccedSave");
                OnSearchAsync();
            }
        }





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
            headerColumn.Cells.Add(CreateXRCell(335, 22, Language.Get("COMMENT"), DevExpress.XtraPrinting.TextAlignment.MiddleCenter, new Font("Times New Roman", (float)7.75), DevExpress.XtraPrinting.BorderSide.All, borderColor, borderWidth, borderStyle, ColorTranslator.FromHtml("#DCDCDC")));


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
                headerDataRow.Cells.Add(CreateXRCell(335, 22, row["DESCRIPTION2"].ToString(), DevExpress.XtraPrinting.TextAlignment.MiddleLeft, font, DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right, borderColor, borderWidth, borderStyle, Color.White));

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

        protected override void OnToolbarSaveClick()
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


            DataTable dtProductSpec = await QueryAsync("SelectProductSpec", "10001", values);
            DataTable dtCircuit = await QueryAsync("SelectProductSpecDetail", "10001", values);
            DataTable dtThick = await QueryAsync("SelectProductThickSpec", "10001", values);
            DataTable dtComment = await QueryAsync("SelectProductSpecComment", "10001", values);

            DataTable dtimpedance = await QueryAsync("SelectProductSpec", "10001", values);

            DataTable dtMvh = await QueryAsync("SelectProductMVHSpec", "10001", values);



            DataTable dtMeterialInfo = await QueryAsync("SelectProductSpecDetail_Meterial", "10001", values);
            DataTable dtInkInfo = await QueryAsync("SelectProductSpecDetail_InkInfo", "10001", values);
            DataTable dtSubMeterial = await QueryAsync("SelectProductSpecDetail_Meterial", "10003", values);
            DataTable dtPlating = await QueryAsync("SelectProductSpecDetail_Plating", "10001", values);


            DataTable dtToolComment = await QueryAsync("SelectProductSpecDetail_ToolComment", "10001", values);
            
            grdtoollist.DataSource = await QueryAsync("GetToolByItem", "10001", values);

            grdbbtjiglist.DataSource = await QueryAsync("GetToolByItem", "10003", values);

            grdetclist.DataSource = await QueryAsync("GetToolByItem", "10004", values);

            if (dtProductSpec.Rows.Count < 1)
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }
            else
            {
                productInfo.DataBind(dtProductSpec);
                productSpec.DataBind(dtProductSpec);
                circuitSpec.DataBind(dtCircuit);
                circuitSpec.DataBind(dtMvh, true);
                circuitSpec.DataBind2(dtimpedance);
                etcInfo.DataBind(dtProductSpec);
                etcInfo.DataBind(dtThick, true);
                etcInfo.DataBind(dtComment);


                this.grdLayerComposition.DataSource = dtMeterialInfo;
                this.grdInkInfo.DataSource = dtInkInfo;
                this.grdSubMaterial.DataSource = dtSubMeterial;

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
        /// 팝업형 조회조건 생성 - 품목
        /// </summary>
        private void InitializeProductId_Popup()
        {
            // SelectPopup 항목 추가
            var conditionProductId = grdtoollist.View.AddSelectPopupColumn("PRODUCTDEFID", new SqlQuery("GetToolByItem", "10002", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupLayoutForm(800, 800)
                .SetPopupAutoFillColumns("ITEMNAME")
                .SetLabel("ITEMID")
                .SetPosition(4.2)
                .SetPopupResultCount(1)
                .SetPopupApplySelection((selectedRows, dataGridRows) =>
                {

                    var values = Conditions.GetValues();

                    string itemid = Format.GetString(values["P_ITEMID"]).Split('|')[0];
                    string itemversion =  Format.GetString(values["P_ITEMID"]).Split('|')[1];

                    foreach (DataRow row in selectedRows)
                    {


                        dataGridRows["PRODUCTDEFID"] = row["PRODUCTDEFID"];
                        dataGridRows["PRODUCTDEFVERSION"] = row["PRODUCTDEFVERSION"];
                        dataGridRows["PRODUCTDEFNAME"] = row["PRODUCTDEFNAME"];
                        dataGridRows["TOOLFORM"] = row["TOOLFORM"];
                        dataGridRows["TOOLKIND"] = row["TOOLKIND"];
                        if (Format.GetString(row["PRODUCTDEFID"]).Equals(itemid) && Format.GetString(row["PRODUCTDEFVERSION"]).Equals(itemversion))
                        {
                            dataGridRows["TOOLCLASS"] = "ProductSpecToolClassify_03";
                        }
                        else
                        {
                            dataGridRows["TOOLCLASS"] = "ProductSpecToolClassify_04";
                        }
                        dataGridRows["SUMMARY"] = row["SUMMARY"];
                        dataGridRows["MANUFACTURER"] = row["MANUFACTURER"];
                        dataGridRows["SCALEX"] = row["SCALEX"];
                        dataGridRows["SCALEY"] = row["SCALEY"];
                        dataGridRows["DURABLEDEFID"] = row["DURABLEDEFID"];
                        dataGridRows["DURABLEDEFVERSION"] = row["DURABLEDEFVERSION"];
                        dataGridRows["DESCRIPTION"] = row["DESCRIPTION"];
                        dataGridRows["DURABLEDEFNAME"] = row["DURABLEDEFNAME"];
                        dataGridRows["RECEIPTAREAID"] = row["RECEIPTAREAID"];
                        
                    }

                });

            // 팝업에서 사용되는 검색조건 (품목코드/명)
            conditionProductId.Conditions.AddTextBox("TXTITEM");

            // 품목코드
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            // 품목명
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            // 품목버전
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 100);
            //치공구ID
            conditionProductId.GridColumns.AddTextBoxColumn("DURABLEDEFID", 100);
            //치공구NAME
            conditionProductId.GridColumns.AddTextBoxColumn("DURABLEDEFNAME", 100);
        }


        /// <summary>
        /// 팝업형 조회조건 생성 - 품목
        /// </summary>
        private void InitializeProductId_Popup2()
        {
            // SelectPopup 항목 추가
            var conditionProductId = grdbbtjiglist.View.AddSelectPopupColumn("PRODUCTDEFID1", new SqlQuery("GetToolByItem", "10005", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupLayoutForm(800, 800)
                .SetPopupAutoFillColumns("ITEMNAME")
                .SetLabel("ITEMID")
                .SetPosition(4.2)
                .SetPopupResultCount(1)
                .SetPopupApplySelection((selectedRows, dataGridRows) =>
                {


                    var values = Conditions.GetValues();

                    string itemid = Format.GetString(values["P_ITEMID"]).Split('|')[0];
                    string itemversion = Format.GetString(values["P_ITEMID"]).Split('|')[1];
                    foreach (DataRow row in selectedRows)
                    {
                        dataGridRows["PRODUCTDEFID1"] = row["PRODUCTDEFID1"];
                        dataGridRows["PRODUCTDEFVERSION1"] = row["PRODUCTDEFVERSION"];
                        dataGridRows["PRODUCTDEFNAME1"] = row["PRODUCTDEFNAME"];
                        dataGridRows["TOOLFORM1"] = row["TOOLFORM"];
                        dataGridRows["TOOLKIND1"] = row["TOOLKIND"];
                        if(Format.GetString(row["PRODUCTDEFID1"]).Equals(itemid) && Format.GetString(row["PRODUCTDEFVERSION"]).Equals(itemversion))
                        {
                            dataGridRows["TOOLCLASS1"] = "ProductSpecToolClassify_03";
                        }
                        else
                        {
                            dataGridRows["TOOLCLASS1"] = "ProductSpecToolClassify_04";
                        }
                        dataGridRows["SUMMARY1"] = row["SUMMARY"];
                        dataGridRows["MANUFACTURER1"] = row["MANUFACTURER"];
                        dataGridRows["SCALEX1"] = row["SCALEX"];
                        dataGridRows["SCALEY1"] = row["SCALEY"];
                        dataGridRows["DURABLEDEFID1"] = row["DURABLEDEFID"];
                        dataGridRows["DESCRIPTION1"] = row["DESCRIPTION"];
                        dataGridRows["DURABLEDEFNAME1"] = row["DURABLEDEFNAME"];
                    }

                });

            // 팝업에서 사용되는 검색조건 (품목코드/명)
            conditionProductId.Conditions.AddTextBox("TXTITEM");

            // 품목코드
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFID1", 150)
                .SetLabel("PRODUCTDEFID");
            // 품목명
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            // 품목버전
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 100);
            //치공구ID
            conditionProductId.GridColumns.AddTextBoxColumn("DURABLEDEFID", 100);
            //치공구NAME
            conditionProductId.GridColumns.AddTextBoxColumn("DURABLEDEFNAME", 100);
        }




        /// <summary>
        /// 팝업형 조회조건 생성 - 품목
        /// </summary>
        private void InitializeProductId_Popup3()
        {
            // SelectPopup 항목 추가
            var conditionProductId = grdetclist.View.AddSelectPopupColumn("PRODUCTDEFID2", new SqlQuery("GetToolByItem", "10006", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupLayoutForm(800, 800)
                .SetPopupAutoFillColumns("ITEMNAME")
                .SetLabel("ITEMID")
                .SetPosition(4.2)
                .SetPopupResultCount(1)
                .SetPopupApplySelection((selectedRows, dataGridRows) =>
                {

                    foreach (DataRow row in selectedRows)
                    {
                        dataGridRows["PRODUCTDEFID2"] = row["PRODUCTDEFID2"];
                        dataGridRows["PRODUCTDEFVERSION2"] = row["PRODUCTDEFVERSION"];
                        dataGridRows["PRODUCTDEFNAME2"] = row["PRODUCTDEFNAME"];
   
      
                    }

                });

            // 팝업에서 사용되는 검색조건 (품목코드/명)
            conditionProductId.Conditions.AddTextBox("TXTITEM");

            // 품목코드
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFID2", 150)
                .SetLabel("PRODUCTDEFID");
            // 품목명
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            // 품목버전
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 100);

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
            this.grdInkInfo.View.CheckValidation();
            this.grdLayerComposition.View.CheckValidation();
            this.grdSubMaterial.View.CheckValidation();


        }


        #endregion

        #region Private Function

        #endregion

        private void smartSplitTableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void grdInkInfo_Load(object sender, EventArgs e)
        {

        }
    }

}
