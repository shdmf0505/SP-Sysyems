#region using

using DevExpress.Utils;
using DevExpress.Utils.Menu;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Micube.Framework;
using Micube.Framework.Net;
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
using DevExpress.XtraLayout.Utils;

#endregion

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 수율현황 및 불량분석 > LOT별 수율 및 불량세부
    /// 업  무  설  명  : LOT별 수율현황/불량세부를 확인한다.
    /// 생    성    자  : 류시윤
    /// 생    성    일  : 2019-12-03
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class YieldDefectStatusByLOT : SmartConditionManualBaseForm
    {
        #region Local Variables

        DXMenuItem _myContextMenu1, _myContextMenu2, _myContextMenu3, _myContextMenu4, _myContextMenu5;

        // TODO : 화면에서 사용할 내부 변수 추가
        string _Text;                                       // 설명
        SmartBandedGrid grdList = new SmartBandedGrid();    // Main Grid
        
        /// <summary>
        /// 조회 조건 품목에서 찾은 정보 Row
        /// </summary>
        private DataRow _selectedRow;

        #endregion

        #region 생성자

        public YieldDefectStatusByLOT()
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
            //grdList.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Restore;
            //grdList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            

            initGridYieldRateByLOT();

            initGridDefectDetail();

            //InitializationSummaryRow();

        }

        private void InitializationSummaryRow()
        {
            #region 불량 그리드

            gridLOTYieldRate.View.Columns["LOTNO"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridLOTYieldRate.View.Columns["LOTNO"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));

            // PCS
            gridLOTYieldRate.View.Columns["PCSYIELDRATE"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            gridLOTYieldRate.View.Columns["PCSYIELDRATE"].SummaryItem.DisplayFormat = "{0:##0.#0 %}";

            gridLOTYieldRate.View.Columns["PCSDEFECTRATE"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            gridLOTYieldRate.View.Columns["PCSDEFECTRATE"].SummaryItem.DisplayFormat = "{0:##0.#0 %}";

            gridLOTYieldRate.View.Columns["PCSINPUTQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Max;
            gridLOTYieldRate.View.Columns["PCSINPUTQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            gridLOTYieldRate.View.Columns["PCSDEFECTQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridLOTYieldRate.View.Columns["PCSDEFECTQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            gridLOTYieldRate.View.Columns["PCSNORMALQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Min;
            gridLOTYieldRate.View.Columns["PCSNORMALQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            // 면적
            gridLOTYieldRate.View.Columns["AREAYIELDRATE"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            gridLOTYieldRate.View.Columns["AREAYIELDRATE"].SummaryItem.DisplayFormat = "{0:##0.#0 %}";

            gridLOTYieldRate.View.Columns["AREADEFECTRATE"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            gridLOTYieldRate.View.Columns["AREADEFECTRATE"].SummaryItem.DisplayFormat = "{0:##0.#0 %}";

            gridLOTYieldRate.View.Columns["AREAINPUTQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Max;
            gridLOTYieldRate.View.Columns["AREAINPUTQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            gridLOTYieldRate.View.Columns["AREADEFECTQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridLOTYieldRate.View.Columns["AREADEFECTQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            gridLOTYieldRate.View.Columns["AREANORMALQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Min;
            gridLOTYieldRate.View.Columns["AREANORMALQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            // 금액
            gridLOTYieldRate.View.Columns["PRICEYIELDRATE"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            gridLOTYieldRate.View.Columns["PRICEYIELDRATE"].SummaryItem.DisplayFormat = "{0:##0.#0 %}";

            gridLOTYieldRate.View.Columns["PRICEDEFECTRATE"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            gridLOTYieldRate.View.Columns["PRICEDEFECTRATE"].SummaryItem.DisplayFormat = "{0:##0.#0 %}";

            gridLOTYieldRate.View.Columns["PRICEINPUTQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Max;
            gridLOTYieldRate.View.Columns["PRICEINPUTQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            gridLOTYieldRate.View.Columns["PRICEDEFECTQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridLOTYieldRate.View.Columns["PRICEDEFECTQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            gridLOTYieldRate.View.Columns["PRICENORMALQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Min;
            gridLOTYieldRate.View.Columns["PRICENORMALQTY"].SummaryItem.DisplayFormat = "{0:###,###}";


            gridLOTYieldRate.View.OptionsView.ShowFooter = true;
            gridLOTYieldRate.ShowStatusBar = false;

            #endregion 불량 그리드

            #region 불량세부 그리드

            gridDefectDetail.View.Columns["LOTNO"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridDefectDetail.View.Columns["LOTNO"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));

            // 발견
            gridDefectDetail.View.Columns["PCSINPUTQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Max;
            gridDefectDetail.View.Columns["PCSINPUTQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            gridDefectDetail.View.Columns["PCSDEFECTQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridDefectDetail.View.Columns["PCSDEFECTQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            gridDefectDetail.View.Columns["PCSDEFECTRATE"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            gridDefectDetail.View.Columns["PCSDEFECTRATE"].SummaryItem.DisplayFormat = "{0:##0.#0 %}"; 

            gridDefectDetail.View.Columns["DEFECTPRICE"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridDefectDetail.View.Columns["DEFECTPRICE"].SummaryItem.DisplayFormat = "{0:###,###}";
            
            // 원인
            gridDefectDetail.View.Columns["INPUTQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Max;
            gridDefectDetail.View.Columns["INPUTQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            gridDefectDetail.View.Columns["DEFECTRATE"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            gridDefectDetail.View.Columns["DEFECTRATE"].SummaryItem.DisplayFormat = "{0:##0.#0 %}";

            gridDefectDetail.View.OptionsView.ShowFooter = true;
            gridDefectDetail.ShowStatusBar = false;

            #endregion 불량세부 그리드
        }

        private void initGridDefectDetail()
        {
            gridDefectDetail.View.OptionsBehavior.Editable = false;

            var defaultCol = gridDefectDetail.View.AddGroupColumn("");
            // TYPE : 단면, 양면, 멀티
            defaultCol.AddTextBoxColumn("PRODUCTSHAPE", 70).SetTextAlignment(TextAlignment.Center);
            // 층수
            if (UserInfo.Current.Enterprise.Equals("YOUNGPOONG"))
            {
                defaultCol.AddComboBoxColumn("LAYER", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=Layer", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))        // 층수'
                    .SetEmptyItem("", "")
                    .SetTextAlignment(TextAlignment.Center);
            }
            // 고객사
            defaultCol.AddTextBoxColumn("COMPANYCLIENT", 50).SetTextAlignment(TextAlignment.Left);
            // 품목명
            defaultCol.AddTextBoxColumn("PRODUCTDEFNAME", 100).SetTextAlignment(TextAlignment.Left);
            // 품목코드
            defaultCol.AddTextBoxColumn("PRODUCTDEFID", 100).SetTextAlignment(TextAlignment.Left);

            // 구분
            defaultCol.AddTextBoxColumn("LOCALE", 50).SetTextAlignment(TextAlignment.Center);
            // 출하SITE
            defaultCol.AddTextBoxColumn("SHIPMENTSITE", 65).SetTextAlignment(TextAlignment.Center);
            // 연계SITE
            defaultCol.AddTextBoxColumn("RELATEDSITE", 80).SetTextAlignment(TextAlignment.Center);
            // 구간지점
            defaultCol.AddTextBoxColumn("INTERSECTSITE", 65).SetTextAlignment(TextAlignment.Center);

            // 품목REV
            defaultCol.AddTextBoxColumn("PRODUCTDEFVERSION", 70).SetTextAlignment(TextAlignment.Center);
            // ROOT LOT
            defaultCol.AddTextBoxColumn("ROOTLOTID", 170).SetTextAlignment(TextAlignment.Left).SetLabel("ROOT LOT");
            // LOT NO
            defaultCol.AddTextBoxColumn("LOTID", 170).SetTextAlignment(TextAlignment.Left).SetLabel("LOT No.");


            var defectCol = gridDefectDetail.View.AddGroupColumn("DEFECTINFO");
            // 발견 Site
            defectCol.AddTextBoxColumn("DISCOVERYSITE", 80).SetTextAlignment(TextAlignment.Center).SetLabel("DEFECTFOUNDSITE");
            // 발견 공정
            defectCol.AddTextBoxColumn("PROCESSSEGMENTNAME", 100).SetTextAlignment(TextAlignment.Left).SetLabel("PROCESSSEGMENTNAME");
            // 투입수
            defectCol.AddTextBoxColumn("PCSINPUTQTY", 50).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric);
            // 불량코드
            if(UserInfo.Current.Enterprise.Equals("YOUNGPOONG"))
            {
                defectCol.AddTextBoxColumn("DEFECTCODE", 50)
                    .SetLabel("DEFECTCODEID")
                    .SetTextAlignment(TextAlignment.Left);
            }
            // 불량명
            defectCol.AddTextBoxColumn("DEFECTNAME", 50).SetTextAlignment(TextAlignment.Left);
            // 품질공정
            defectCol.AddTextBoxColumn("QCSEGMENTNAME", 50).SetTextAlignment(TextAlignment.Left).SetLabel("QCSEGMENT");
            // 불량수
            defectCol.AddTextBoxColumn("PCSDEFECTQTY", 50).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric);
            // 불량율 
            defectCol.AddTextBoxColumn("PCSDEFECTRATE", 50).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("##0.#0 %", MaskTypes.Numeric);
            // 불량금액 
            defectCol.AddTextBoxColumn("DEFECTPRICE", 50).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric);

            if (UserInfo.Current.Enterprise.Equals("YOUNGPOONG"))
            {
                // SMT불량금액
                defectCol.AddTextBoxColumn("SMTPRICEDEFECTQTY", 50)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetLabel("SMTDEFECTPRICE");
            }

            var defectCauseInfoCol = gridDefectDetail.View.AddGroupColumn("DEFECTCAUSEINFO");
            // Site
            defectCauseInfoCol.AddTextBoxColumn("REASONSITE", 50).SetTextAlignment(TextAlignment.Center).SetLabel("SITE");
            // 투입수
            defectCauseInfoCol.AddTextBoxColumn("INPUTQTY", 50).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric);
            // 불량률
            defectCauseInfoCol.AddTextBoxColumn("DEFECTRATE", 50).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("##0.#0 %", MaskTypes.Numeric);
            // 원인품목 
            defectCauseInfoCol.AddTextBoxColumn("REASONCONSUMABLEDEFNAME", 50).SetTextAlignment(TextAlignment.Left).SetLabel("REASONPRODUCT");
            // 구분 
            //defectCauseInfoCol.AddTextBoxColumn("CLASS", 50).SetTextAlignment(TextAlignment.Center);
            // 원인 공정 
            defectCauseInfoCol.AddTextBoxColumn("REASONSEGMENTNAME", 50).SetTextAlignment(TextAlignment.Left).SetLabel("CAUSEPROCESS");
            // 원인 작업장 
            defectCauseInfoCol.AddTextBoxColumn("REASONAREANAME", 50).SetTextAlignment(TextAlignment.Left).SetLabel("REASONAREA");

            var extCol = gridDefectDetail.View.AddGroupColumn("");

            // BASE/SMT 구분
            extCol.AddTextBoxColumn("BARESMTTYPE", 100).SetLabel("DIVBARESMT");
            
            // 검사공정
            extCol.AddTextBoxColumn("INSPPROCESSSEGMENT", 100)
                .SetTextAlignment(TextAlignment.Left);

            // 작업장
            extCol.AddTextBoxColumn("INSAREA", 100)
                .SetLabel("AREANAME")
                .SetTextAlignment(TextAlignment.Left);


            // 제품구분
            extCol.AddTextBoxColumn("PRODUCTDEFTYPE", 50).SetLabel("THEPRODUCTTYPE").SetTextAlignment(TextAlignment.Center);

            if (UserInfo.Current.Enterprise.Equals("YOUNGPOONG"))
            {
                // 작업완료일
                extCol.AddTextBoxColumn("WORKENDDATE", 100).SetLabel("WORKENDDATE").SetTextAlignment(TextAlignment.Left);

                // 집계일자
                extCol.AddTextBoxColumn("SENDTIME", 100).SetLabel("LOTSENDDATE").SetTextAlignment(TextAlignment.Left);

                // 집계일자
                extCol.AddTextBoxColumn("SUMMARYDATE", 100).SetLabel("SUMDAY").SetTextAlignment(TextAlignment.Left);

                //// CREATETIME
                //extCol.AddTextBoxColumn("CREATEDTIME", 100).SetLabel("CREATEDATE").SetTextAlignment(TextAlignment.Left);
            }
            else
            {
                // 집계일자
                extCol.AddTextBoxColumn("SUMMARYDATE", 100).SetLabel("SUMDAY").SetTextAlignment(TextAlignment.Left);

                // CREATETIME
                extCol.AddTextBoxColumn("CREATEDTIME", 100).SetLabel("CREATEDATE").SetTextAlignment(TextAlignment.Left);
            }

            gridDefectDetail.View.PopulateColumns();

            gridDefectDetail.View.BestFitColumns(true);
        }

        private void initGridYieldRateByLOT()
        {
            gridLOTYieldRate.View.OptionsBehavior.Editable = false;

            var defaultCol = gridLOTYieldRate.View.AddGroupColumn("");
            // TYPE : 단면, 양면, 멀티
            defaultCol.AddTextBoxColumn("PRODUCTSHAPE", 70).SetTextAlignment(TextAlignment.Center);
            // 층수
            if (UserInfo.Current.Enterprise.Equals("YOUNGPOONG"))
            {
                defaultCol.AddComboBoxColumn("LAYER", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=Layer", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))        // 층수'
                    .SetEmptyItem("", "")
                    .SetTextAlignment(TextAlignment.Center);
            }

            // 고객사
            defaultCol.AddTextBoxColumn("COMPANYCLIENT", 50).SetTextAlignment(TextAlignment.Left);
            // 품목명
            defaultCol.AddTextBoxColumn("PRODUCTDEFNAME", 100).SetTextAlignment(TextAlignment.Left);
            // 품목코드
            defaultCol.AddTextBoxColumn("PRODUCTDEFID", 100).SetTextAlignment(TextAlignment.Left);

            // 구분
            defaultCol.AddTextBoxColumn("LOCALE", 50).SetTextAlignment(TextAlignment.Center);
            // 출하SITE
            defaultCol.AddTextBoxColumn("SHIPMENTSITE", 65).SetTextAlignment(TextAlignment.Center);
            // 연계SITE
            defaultCol.AddTextBoxColumn("RELATEDSITE", 80).SetTextAlignment(TextAlignment.Center);
            // 구간지점
            defaultCol.AddTextBoxColumn("INTERSECTSITE", 65).SetTextAlignment(TextAlignment.Center);
            
            // 품목REV
            defaultCol.AddTextBoxColumn("PRODUCTDEFVERSION", 70).SetTextAlignment(TextAlignment.Center);
            // ROOT LOT
            defaultCol.AddTextBoxColumn("ROOTLOTID", 170).SetTextAlignment(TextAlignment.Left).SetLabel("ROOT LOT");
            // LOT NO
            defaultCol.AddTextBoxColumn("LOTID", 170).SetTextAlignment(TextAlignment.Left).SetLabel("LOT No.");

            #region PCS 수율 컬럼

            var pcsCol = gridLOTYieldRate.View.AddGroupColumn("GROUPPCSYIELDRATE");
            // 수율
            pcsCol.AddTextBoxColumn("PCSYIELDRATE", 50).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("##0.#0 %", MaskTypes.Custom);

            // 불량율
            pcsCol.AddTextBoxColumn("PCSDEFECTRATE", 50).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("##0.#0 %", MaskTypes.Numeric);

            // 투입수
            pcsCol.AddTextBoxColumn("PCSINPUTQTY", 50).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric);

            // 불량수
            pcsCol.AddTextBoxColumn("PCSDEFECTQTY", 50).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric);

            // 양품수
            pcsCol.AddTextBoxColumn("PCSNORMALQTY", 50).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric);

            #endregion PCS 수율 컬럼

            #region 면적당 수율 컬럼

            var areaCol = gridLOTYieldRate.View.AddGroupColumn("GROUPAREAYIELDRATE");
            // 수율
            areaCol.AddTextBoxColumn("AREAYIELDRATE", 50).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("##0.#0 %", MaskTypes.Numeric);

            // 불량율
            areaCol.AddTextBoxColumn("AREADEFECTRATE", 50).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("##0.#0 %", MaskTypes.Numeric);

            // 투입수
            areaCol.AddTextBoxColumn("AREAINPUTQTY", 50)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("##0.#0", MaskTypes.Numeric)
                .SetLabel("INPUTM2");

            // 불량수
            areaCol.AddTextBoxColumn("AREADEFECTQTY", 50)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("##0.#0", MaskTypes.Numeric)
                .SetLabel("DEFECTM2");

            // 양품수
            areaCol.AddTextBoxColumn("AREANORMALQTY", 50)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("##0.#0", MaskTypes.Numeric)
                .SetLabel("NORMALM2");

            #endregion 면적당 수율 컬럼

            #region 금액 수율 컬럼
            
            string strColCap = "GROUPPRICEYIELDRATE";

            if (UserInfo.Current.Plant == "IFC" || UserInfo.Current.Plant == "YPE")
                strColCap  += "KRW";
            else if (UserInfo.Current.Plant == "IFV" || UserInfo.Current.Plant == "YPEV")
                strColCap  += "VND";
            else if (UserInfo.Current.Plant == "CCT")
                strColCap  += "CNY";
            else
                strColCap  += "USD";

            var priceCol = gridLOTYieldRate.View.AddGroupColumn(strColCap);

            // 수율
            priceCol.AddTextBoxColumn("PRICEYIELDRATE", 50)
                .SetTextAlignment(TextAlignment.Center)
                .SetDisplayFormat("##0.#0 %", MaskTypes.Numeric);

            // 불량율
            priceCol.AddTextBoxColumn("PRICEDEFECTRATE", 50)
                .SetTextAlignment(TextAlignment.Center)
                .SetDisplayFormat("##0.#0 %", MaskTypes.Numeric);

            // 투입수
            priceCol.AddTextBoxColumn("PRICEINPUTQTY", 50)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetLabel("INPUTCOST");

            // 불량금액
            priceCol.AddTextBoxColumn("PRICEDEFECTQTY", 50)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetLabel("DEFECTCOST");

            if (UserInfo.Current.Enterprise.Equals("YOUNGPOONG"))
            {
                // SMT불량금액
                priceCol.AddTextBoxColumn("SMTPRICEDEFECTQTY", 50)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetLabel("SMTDEFECTPRICE");
            }

            // 양품수
            priceCol.AddTextBoxColumn("PRICENORMALQTY", 50)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetLabel("NORMALCOST");

            #endregion 금액 수율 컬럼

            var extCol = gridLOTYieldRate.View.AddGroupColumn("");

            // BASE/SMT 구분
            extCol.AddTextBoxColumn("BARESMTTYPE", 100).SetLabel("DIVBARESMT").SetTextAlignment(TextAlignment.Center);

            // 검사공정
            extCol.AddTextBoxColumn("INSPPROCESSSEGMENT", 100)
                .SetTextAlignment(TextAlignment.Left);

            // 작업장
            extCol.AddTextBoxColumn("INSAREA", 100)
                .SetLabel("AREANAME")
                .SetTextAlignment(TextAlignment.Left);

            // 제품구분
            extCol.AddTextBoxColumn("PRODUCTDEFTYPE", 50).SetLabel("THEPRODUCTTYPE").SetTextAlignment(TextAlignment.Center);

            if (UserInfo.Current.Enterprise.Equals("YOUNGPOONG"))
            {
                // 작업완료일
                extCol.AddTextBoxColumn("WORKENDDATE", 100).SetLabel("WORKENDDATE").SetTextAlignment(TextAlignment.Left);

                // 집계일자
                extCol.AddTextBoxColumn("SENDTIME", 100).SetLabel("LOTSENDDATE").SetTextAlignment(TextAlignment.Left);
              
                // 집계일자
                extCol.AddTextBoxColumn("SUMMARYDATE", 100).SetLabel("SUMDAY").SetTextAlignment(TextAlignment.Left);

                //// CREATETIME
                //extCol.AddTextBoxColumn("CREATEDTIME", 100).SetLabel("CREATEDATE").SetTextAlignment(TextAlignment.Left);
            }
            else
            {
                // 집계일자
                extCol.AddTextBoxColumn("SUMMARYDATE", 100).SetLabel("SUMDAY").SetTextAlignment(TextAlignment.Left);

                // CREATETIME
                extCol.AddTextBoxColumn("CREATEDTIME", 100).SetLabel("CREATEDATE").SetTextAlignment(TextAlignment.Left);
            }
            

            gridLOTYieldRate.View.PopulateColumns();

            gridLOTYieldRate.View.BestFitColumns(true);
        }

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            //grdList.View.AddingNewRow += View_AddingNewRow;
            Conditions.GetControl<SmartComboBox>("P_LOCALEDIV").EditValueChanged += (s, e) =>
            {
                SmartComboBox cbDiv = s as SmartComboBox;
                
                if(cbDiv.EditValue.Equals("LOCAL"))
                    Conditions.GetControl<SmartCheckedComboBox>("P_LINKPLANTID").Enabled = false;
                else
                    Conditions.GetControl<SmartCheckedComboBox>("P_LINKPLANTID").Enabled = true;
            };

            gridLOTYieldRate.View.DoubleClick += View_DoubleClick;

            gridLOTYieldRate.View.RowCellStyle += View_RowCellStyle;
            gridDefectDetail.View.RowCellStyle += View_RowCellStyle;
            gridLOTYieldRate.InitContextMenuEvent += Grid_InitContextMenuEvent;
            gridDefectDetail.InitContextMenuEvent += Grid_InitContextMenuEvent;

            smartTabControl1.SelectedPageChanged += (s, e) =>
            {
                SetConditionVisiblility("P_QCSEGMENTID", LayoutVisibility.Never);
                SetConditionVisiblility("P_DEFECTCODE", LayoutVisibility.Never);

                if (e.Page == tapDefectDetail)
                {
                    Conditions.GetControl<SmartComboBox>("P_PROCESSTYPE").Enabled = false;
                    SetConditionVisiblility("P_QCSEGMENTID", LayoutVisibility.Always);
                    SetConditionVisiblility("P_DEFECTCODE", LayoutVisibility.Always);
                }
                else
                {
                    Conditions.GetControl<SmartComboBox>("P_PROCESSTYPE").Enabled = true;
                    SetConditionVisiblility("P_QCSEGMENTID", LayoutVisibility.Never);
                    SetConditionVisiblility("P_DEFECTCODE", LayoutVisibility.Never);
                }
            };
        }

        private void View_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;

            DataRowView row = view.GetRow(e.RowHandle) as DataRowView;

            if(row["LV"].ToString().ToDouble() > 0)
            {
                e.Appearance.BackColor = Color.LightSkyBlue;
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            }
        }

        private void View_DoubleClick(object sender, EventArgs e)
        {
            DXMouseEventArgs ea = e as DXMouseEventArgs;
            GridView view = sender as GridView;
            GridHitInfo info = view.CalcHitInfo(ea.Location);
            var values = Conditions.GetValues();
            string condLotStandard = values["P_LOTSTANDARD"].ToString();
            if (info.InRow || info.InRowCell)
            {
                string strRootLot = view.GetRowCellValue(info.RowHandle, "ROOTLOTID").ToString();
                string strLot = view.GetRowCellValue(info.RowHandle, "LOTID").ToString();

                //TxtRootLot
                //TxtLotIdIsNotRequired
                Conditions.GetControl<SmartSelectPopupEdit>("P_ROOTLOT").EditValue = strRootLot;
                if(condLotStandard.Equals("SPLITLOT"))
                    Conditions.GetControl<SmartSelectPopupEdit>("P_LOTNO").EditValue = strLot;
                else
                    Conditions.GetControl<SmartSelectPopupEdit>("P_LOTNO").EditValue = "";

                smartTabControl1.SelectedTabPage = tapDefectDetail;
                this.OnSearchAsync();
            }
        }

        private void Grid_InitContextMenuEvent(SmartBandedGrid sender, Framework.SmartControls.Grid.InitContextMenuEventArgs args)
        {
            // LOT 이력조회
            args.AddMenus.Add(_myContextMenu1 = new DXMenuItem(Language.Get("MENU_PG-SG-0340"), OpenForm) { BeginGroup = true, Tag = "PG-SG-0340" });
            // 품목별 수율
            args.AddMenus.Add(_myContextMenu2 = new DXMenuItem(Language.Get("MENU_PG-QC-0460"), OpenForm) { Tag = "PG-QC-0460" });
            // Defect Map
            args.AddMenus.Add(_myContextMenu3 = new DXMenuItem(Language.Get("MENU_DefectMapByLot"), OpenForm) { Tag = "DefectMapByLot" });
            //args.AddMenus.Add(_myContextMenu4 = new DXMenuItem(Language.Get("MENU_DefectMapByItem"), OpenForm) { Tag = "DefectMapByItem" });

            args.AddMenus.Add(_myContextMenu5 = new DXMenuItem(Language.Get("MENU_PG-QC-0556"), OpenForm) { Tag = "PG-QC-0556" });
        }

        private void OpenForm(object sender, EventArgs args)
        {
            try
            {
                SmartBandedGrid grid;
                if (smartTabControl1.SelectedTabPage == tapLOTYieldRate)
                    grid = gridLOTYieldRate;
                else
                    grid = gridDefectDetail;

                DialogManager.ShowWaitDialog();

                DataRow currentRow = grid.View.GetFocusedDataRow();

                string menuId = (sender as DXMenuItem).Tag.ToString();

                var values = Conditions.GetValues();

                var param = currentRow.Table.Columns
                    .Cast<DataColumn>()
                    .ToDictionary(col => col.ColumnName, col => currentRow[col.ColumnName]);
                //if (param["LOTNO"] != null)
                //    param.Add("LOTID", param["LOTNO"].ToString());

                #region DEFECT MAP 화면 이동

                // DEFECT MAP 화면 이동시 필요
                if (values.ContainsKey("PRODUCTDEFVERSION"))
                {
                    if (param["PRODUCTDEFVERSION"] != null)
                        values["PRODUCTDEFVERSION"] = param["PRODUCTDEFVERSION"].ToString();
                    else
                        values["PRODUCTDEFVERSION"] = "";
                }
                else
                {
                    if (param["PRODUCTDEFVERSION"] != null)
                        values.Add("PRODUCTDEFVERSION", param["PRODUCTDEFVERSION"].ToString());
                    else
                        values.Add("PRODUCTDEFVERSION", "");
                }

                #endregion

                #region LOT 이력조회시

                if (values.ContainsKey("LOTID"))
                {
                    if (param["LOTID"] != null)
                        values["LOTID"] = param["LOTID"].ToString();
                    else
                        values["LOTID"] = "";
                }
                else
                {
                    if (param["LOTID"] != null)
                        values.Add("LOTID", param["LOTID"].ToString());
                    else
                        values.Add("LOTID", "");
                }

                #endregion

                #region 품목코드 처리

                if (values.ContainsKey("P_PRODUCTDEFID"))
                    values["P_PRODUCTDEFID"] = param["PRODUCTDEFID"].ToString() + param["PRODUCTDEFVERSION"].ToString();
                else
                    values.Add("P_PRODUCTDEFID", param["PRODUCTDEFID"].ToString() + param["PRODUCTDEFVERSION"].ToString());

                if(values.ContainsKey("P_PRODUCTNAME"))
                    values["P_PRODUCTNAME"] = param["PRODUCTDEFNAME"].ToString();
                else
                    values.Add("P_PRODUCTNAME", param["PRODUCTDEFNAME"].ToString());

                #endregion

                if (param["LV"].ToNullOrDouble() == null || param["LV"].ToNullOrDouble() != 0)
                    return;

                OpenMenu(menuId, values); //다른창 호출..
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
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            // TODO : 저장 Rule 변경
            DataTable changed = grdList.GetChangedRows();

            ExecuteRule("SaveCodeClass", changed);
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
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("PLANT", UserInfo.Current.Plant);
            if (values["P_LOCALEDIV"].ToString().Equals("LOCAL"))
                values["P_LINKPLANTID"] = "*";

            string qryYPE = string.Empty;
            if (UserInfo.Current.Enterprise.Equals("YOUNGPOONG"))
                qryYPE = "YPE";

            if (this.smartTabControl1.SelectedTabPage == tapLOTYieldRate)
            {
                DataTable dtResult = await SqlExecuter.QueryAsync("SelectYieldRateByLOT" + qryYPE, "10001", values);

                if (dtResult.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
                {
                    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                }

                gridLOTYieldRate.DataSource = dtResult;
                gridLOTYieldRate.View.BestFitColumns(true);
            }
            else
            {
                try
                {
                    //dfbase 입력
                    MessageWorker worker_dfbase = new MessageWorker("SaveYieldDefectStaus");
                    worker_dfbase.SetBody(new MessageBody()
                    {
                        { "lockingType", "LOTDFBASE" },
                        { "exceptdefectclass", values["P_EXCEPTDEFECTCLASS"] },
                        { "interplantid", values["P_INTERPLANTID"] },
                        { "linkplantid", values["P_LINKPLANTID"] },
                        { "localediv", values["P_LOCALEDIV"] },
                        { "plantid", values["P_PLANTID"] },
                        { "productdeftype", values["P_PRODUCTDEFTYPE"] },
                        { "prodshapetype", values["P_PRODSHAPETYPE"] },
                        { "baresmttype", values["P_BARESMTTYPE"] },
                        { "inspectionprocess", values["P_INSPECTIONPROCESS"] },
                        { "insptype", values["P_INSPTYPE"] },
                        { "lotstandard", values["P_LOTSTANDARD"] },
                        { "sumtype", values["P_SUMTYPE"] },
                        { "productiondivision", values["P_PRODUCTIONDIVISION"] },
                        { "processtype", values["P_PROCESSTYPE"] },
                        { "period", values["P_PERIOD"] },
                        { "period_periodfr", values["P_PERIOD_PERIODFR"] },
                        { "period_periodto", values["P_PERIOD_PERIODTO"] },
                        { "productname", values["P_PRODUCTNAME"] },
                        { "productdefid", values["P_PRODUCTDEFID"] },
                        { "rootlot", values["P_ROOTLOT"] },
                        { "lotno", values["P_LOTNO"] },
                        { "customer", values["P_CUSTOMER"] },
                        { "enterpriseid", values["ENTERPRISEID"] },
                        { "languagetype", values["LANGUAGETYPE"] },
                        { "plant", values["PLANT"] }
				        //{ "lotList", dt }
			        });

                    worker_dfbase.Execute();
                    

                    //lotdfdtl 부분 입력..                    
                    MessageWorker worker_ltdf = new MessageWorker("SaveYieldDefectStaus");
                    worker_ltdf.SetBody(new MessageBody()
                    {
                        { "lockingType", "LOTDFDTL" },
                        { "exceptdefectclass", values["P_EXCEPTDEFECTCLASS"] },
                        { "interplantid", values["P_INTERPLANTID"] },
                        { "linkplantid", values["P_LINKPLANTID"] },
                        { "localediv", values["P_LOCALEDIV"] },
                        { "plantid", values["P_PLANTID"] },
                        { "productdeftype", values["P_PRODUCTDEFTYPE"] },
                        { "prodshapetype", values["P_PRODSHAPETYPE"] },
                        { "baresmttype", values["P_BARESMTTYPE"] },
                        { "inspectionprocess", values["P_INSPECTIONPROCESS"] },
                        { "insptype", values["P_INSPTYPE"] },
                        { "lotstandard", values["P_LOTSTANDARD"] },
                        { "sumtype", values["P_SUMTYPE"] },
                        { "productiondivision", values["P_PRODUCTIONDIVISION"] },
                        { "processtype", values["P_PROCESSTYPE"] },
                        { "period", values["P_PERIOD"] },
                        { "period_periodfr", values["P_PERIOD_PERIODFR"] },
                        { "period_periodto", values["P_PERIOD_PERIODTO"] },
                        { "productname", values["P_PRODUCTNAME"] },
                        { "productdefid", values["P_PRODUCTDEFID"] },
                        { "rootlot", values["P_ROOTLOT"] },
                        { "lotno", values["P_LOTNO"] },
                        { "customer", values["P_CUSTOMER"] },
                        { "enterpriseid", values["ENTERPRISEID"] },
                        { "languagetype", values["LANGUAGETYPE"] },
                        { "plant", values["PLANT"] }
				        //{ "lotList", dt }
			        });

                    worker_ltdf.Execute();

                }
                catch (Exception ex)
                {
                    ShowMessage(ex.Message);
                }

                DataTable dtResult = new DataTable();

                dtResult = null;

                gridDefectDetail.DataSource = null;

                //DataTable dtResult = await SqlExecuter.QueryAsync("SelectDefectDetailByLOT" + qryYPE, "10001", values);


                dtResult = await SqlExecuter.QueryAsync("SelectDefectDetailByLOT" + qryYPE, "10001", values);

                if (dtResult.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
                {
                    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                }

                gridDefectDetail.DataSource = dtResult;
                //gridDefectDetail.View.BestFitColumns(true);
            } 
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            //base.InitializeCondition();
            string qryYPE = string.Empty;
            if (UserInfo.Current.Enterprise.Equals("YOUNGPOONG"))
                qryYPE = "YPE";

            // TODO : 조회조건 추가 구성이 필요한 경우 사용        
            #region 품목명

            Conditions.GetCondition<ConditionItemTextBox>("P_PRODUCTNAME").SetIsReadOnly();
            double posProdName = Conditions.GetCondition<ConditionItemTextBox>("P_PRODUCTNAME").Position;

            #endregion

            #region 품목 

            var productDefID = Conditions.AddSelectPopup("P_PRODUCTDEFID", new SqlQuery("GetProductListByYieldStatus", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PRODUCTDEFIDVERSION", "PRODUCTDEFIDVERSION")
                                         .SetLabel("PRODUCTDEFID")
                                         .SetPopupLayout("GRIDPRODUCTLIST", PopupButtonStyles.Ok_Cancel, true, false)
                                         .SetPopupLayoutForm(600, 400, FormBorderStyle.FixedToolWindow)
                                         .SetPosition(posProdName-0.1)
                                         .SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
                                         {
                                             string strProdCd = string.Empty;
                                             string strProdNm = string.Empty;
                                                
                                             foreach (DataRow row in selectedRow)
                                             {
                                                 strProdCd += row["PRODUCTDEFIDVERSION"] + ",";
                                                 strProdNm += row["PRODUCTDEFNAME"] + ",";
                                             }
                                             strProdCd = strProdCd.Substring(0, strProdCd.Length - 1);
                                             Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").SetValue(strProdCd);
                                             strProdNm = strProdNm.Substring(0, strProdNm.Length - 1);
                                             Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").Text = strProdNm;
                                         
                                         })
                                         .SetRelationIds("P_CUSTOMER")
                                         ;

            // 복수 선택 Result Count 지정
            productDefID.SetPopupResultCount(0);

            SmartTextBox tbProductID = Conditions.GetControl<SmartTextBox>("PRODUCTDEFID");

            productDefID.Conditions.AddTextBox("PRODUCTDEFID")
                                   .SetLabel("PRODUCTDEFID");

            //productDefID.Conditions.GetControl<SmartTextBox>("PRODUCTDEFID").EditValue = tbProductID.EditValue;

            productDefID.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 120)
                                    .SetLabel("PRODUCTDEFID");

            productDefID.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 250)
                                    .SetLabel("PRODUCTDEFNAME");

            productDefID.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 100)
                                    .SetLabel("PRODUCTDEFVERSION");

            productDefID.SetSearchTextControlId("PRODUCTDEFID");

            #endregion

            #region ROOT LOT

            var vRootLOT = Conditions.AddSelectPopup("P_ROOTLOT", new SqlQuery("GetYieldRootLotList" + qryYPE, "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "ROOTLOTID", "ROOTLOTID")
                                         .SetLabel("ROOT LOT")
                                         .SetPopupLayout("ROOT LOT", PopupButtonStyles.Ok_Cancel, true, false)
                                         .SetPopupLayoutForm(780, 400, FormBorderStyle.FixedToolWindow)
                                         .SetPosition(posProdName + 0.1)
                                         .SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
                                         {
                                             foreach (DataRow row in selectedRow)
                                             {
                                                 SetCondition(row);
                                             }
                                         })
                                         .SetRelationIds("P_PRODUCTDEFID");
            ;

            // 팝업에서 사용되는 검색조건
            var condRootLotProductdef = vRootLOT.Conditions.AddSelectPopup("TXTPRODUCTDEFNAME2", new SqlQuery("GetProductionOrderIdListOfLotInput", "10001", $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PRODUCTDEFNAME", "PRODUCTDEFID")
                .SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupLayoutForm(600, 800)
                .SetLabel("PRODUCTDEF")
                .SetPopupResultCount(1)
                .SetPopupAutoFillColumns("PRODUCTDEFNAME");

            condRootLotProductdef.Conditions.AddTextBox("TXTPRODUCTDEFNAME2");

            condRootLotProductdef.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            condRootLotProductdef.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            

            vRootLOT.Conditions.AddTextBox("ROOTLOTID")
                                   .SetLabel("ROOT LOT No.");

            vRootLOT.GridColumns.AddTextBoxColumn("ROOTLOTID", 170)
                                    .SetLabel("ROOT LOT");
            // 품목코드
            vRootLOT.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            // 품목버전
            vRootLOT.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 80);
            // 품목명
            vRootLOT.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);


            // 복수 선택 Result Count 지정
            vRootLOT.SetPopupResultCount(0);

            #endregion ROOT LOT

            #region LOT NO.
            
            var vLOTNO = Conditions.AddSelectPopup("P_LOTNO", new SqlQuery("GetYieldLotList" + qryYPE, "10002", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "LOTID", "LOTID")
                                         .SetLabel("LOT NO")
                                         .SetPopupLayout("LOT NO", PopupButtonStyles.Ok_Cancel, true, false)
                                         .SetPopupLayoutForm(900, 500, FormBorderStyle.FixedToolWindow)
                                         .SetPosition(posProdName + 0.1)
                                         .SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
                                         {
                                             foreach (DataRow row in selectedRow)
                                             {
                                                 SetCondition(row);
                                             }
                                         })
                                         .SetRelationIds("P_PRODUCTDEFID", "P_ROOTLOT");
            ;
            
            // 팝업에서 사용되는 검색조건
            var conditionProductdef = vLOTNO.Conditions.AddSelectPopup("TXTPRODUCTDEFNAME2", new SqlQuery("GetProductionOrderIdListOfLotInput", "10001", $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PRODUCTDEFNAME", "PRODUCTDEFID")
                .SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupLayoutForm(600, 800)
                .SetLabel("PRODUCTDEF")
                .SetPopupResultCount(1)
                .SetPopupAutoFillColumns("PRODUCTDEFNAME");

            conditionProductdef.Conditions.AddTextBox("TXTPRODUCTDEFNAME2");

            conditionProductdef.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            conditionProductdef.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);


            vLOTNO.Conditions.AddTextBox("LOTID")
                                   .SetLabel("LOT No.");

            // 팝업 그리드에서 보여줄 컬럼 정의
            // Lot No
            vLOTNO.GridColumns.AddTextBoxColumn("LOTID", 170)
                                     .SetLabel("LOT No.");

            vLOTNO.GridColumns.AddTextBoxColumn("ROOTLOTID", 170).SetLabel("ROOT LOT No.");

            //// 양산구분
            //vLOTNO.GridColumns.AddComboBoxColumn("LOTTYPE", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
            //    .SetLabel("PRODUCTIONTYPE");
            // 품목코드
            vLOTNO.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            // 품목버전
            vLOTNO.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 80);
            // 품목명
            vLOTNO.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);

            //vLOTNO.SetSearchTextControlId("LOTID");

            // 복수 선택 Result Count 지정
            vLOTNO.SetPopupResultCount(0);

            #endregion LOT NO.

            #region 고객사

            double posType = Conditions.GetCondition<ConditionItemComboBox>("P_PRODSHAPETYPE").Position;

            var vCustomer = Conditions.AddSelectPopup("P_CUSTOMER", new SqlQuery("GetCustomerListByYield", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "CUSTOMERNAME", "CUSTOMERID")
                                         .SetLabel("CUSTOMER")
                                         .SetPopupLayout("CUSTOMER", PopupButtonStyles.Ok_Cancel, true, false)
                                         .SetPopupLayoutForm(600, 400, FormBorderStyle.FixedToolWindow)
                                         .SetPosition(posType + 0.1)
                                         .SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
                                         {
                                             foreach (DataRow row in selectedRow)
                                             {
                                                 SetCondition(row);
                                             }
                                         });
            ;

            vCustomer.Conditions.AddTextBox("CUSTOMERNAME")
                                   .SetLabel("CUSTOMER");

            vCustomer.GridColumns.AddTextBoxColumn("CUSTOMERID", 120)
                                    .SetLabel("CUSTOMERID");

            vCustomer.GridColumns.AddTextBoxColumn("CUSTOMERNAME", 250)
                                    .SetLabel("CUSTOMERNAME");

            // 복수 선택 Result Count 지정
            vCustomer.SetPopupResultCount(0);

            #endregion 고객사

            #region 품질공정

            var condition = Conditions.AddSelectPopup("P_QCSEGMENTID", new SqlQuery("GetQcSegmentDefinitionByQcSegment", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "QCSEGMENTNAME", "QCSEGMENTID")
                                      .SetLabel("QCSEGMENT")
                                      .SetPopupLayout("QCSEGMENT", PopupButtonStyles.Ok_Cancel, true, false)
                                      .SetPopupLayoutForm(600, 400, FormBorderStyle.FixedToolWindow)
                                      .SetPosition(17);

            condition.Conditions.AddTextBox("P_QCSEGMENTID").SetLabel("QCSEGMENT");

            condition.GridColumns.AddTextBoxColumn("QCSEGMENTID", 120);
            condition.GridColumns.AddTextBoxColumn("QCSEGMENTNAME", 250);

            #endregion

            #region 불량코드

            condition = Conditions.AddSelectPopup("P_DEFECTCODE", new SqlQuery("GetDefectCodeByQcSegment", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "DEFECTNAME", "DEFECTCODE")
                                      .SetLabel("SPCUIDEFECTIVECODE")
                                      .SetPopupLayout("QCSEGMENT", PopupButtonStyles.Ok_Cancel, true, false)
                                      .SetPopupLayoutForm(600, 400, FormBorderStyle.FixedToolWindow)
                                      .SetPosition(18)
                                      .SetRelationIds("P_QCSEGMENTID");

            condition.Conditions.AddTextBox("P_DEFECTCODE").SetLabel("TXTDEFECTCODENAME");

            condition.GridColumns.AddTextBoxColumn("DEFECTCODE", 120);
            condition.GridColumns.AddTextBoxColumn("DEFECTNAME", 250);

            #endregion
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
            Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").EditValueChanged += YieldDefectStatusByLOT_EditValueChanged;

            //P_EXCEPTDEFECTCLASS
            SmartCheckedComboBox chkComboBox = Conditions.GetControl<SmartCheckedComboBox>("P_EXCEPTDEFECTCLASS");

            // LOT별 수율 Tab일 경우 아래 검색조건은 표시하지 않는다.
            SetConditionVisiblility("P_QCSEGMENTID", LayoutVisibility.Never);
            SetConditionVisiblility("P_DEFECTCODE", LayoutVisibility.Never);
        }

        private void YieldDefectStatusByLOT_EditValueChanged(object sender, EventArgs e)
        {
            var values = Conditions.GetValues();
            string strValue = Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").EditValue.ToString();   // values["P_PRODUCTDEFID"].ToString();

            if (string.IsNullOrEmpty(strValue))
            {
                Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").Text = "";
            }
        }

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            // TODO : 유효성 로직 변경
            grdList.View.CheckValidation();

            DataTable changed = grdList.GetChangedRows();

            if (changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }

        #endregion

        #region Private Function

        // TODO : 화면에서 사용할 내부 함수 추가
        /// <summary>
        /// 마스터 정보를 조회한다.
        /// </summary>
        private void LoadData()
        {

        }

        private void SetCondition(DataRow dr)
        {
            if (!DefectMapHelper.IsNull(dr.GetObject("P_PRODUCTDEFID")))
            {
                Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").SetValue(dr["PRODUCTDEFID"]);

                Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").Text = dr["PRODUCTDEFNAME"].ToString();
            }
            _selectedRow = dr;
        }

        #endregion

        private void LoadForm(object sender, EventArgs e)
        {
            string strLocal = Conditions.GetCondition<ConditionItemComboBox>("P_LOCALEDIV").DefaultValue.ToString();

            if (strLocal.Equals("LOCAL"))
            {
                Conditions.GetControl<SmartCheckedComboBox>("P_LINKPLANTID").Enabled = false;
            }
        }

        public override void LoadForm(Dictionary<string, object> parameters)
        {
            base.LoadForm(parameters);

            if (parameters != null)
            {
                string strLocal = Conditions.GetCondition<ConditionItemComboBox>("P_LOCALEDIV").DefaultValue.ToString();

                if (strLocal.Equals("LOCAL"))
                {
                    Conditions.GetControl<SmartCheckedComboBox>("P_LINKPLANTID").Enabled = false;
                }
                else
                    Conditions.GetControl<SmartCheckedComboBox>("P_LINKPLANTID").EditValue = parameters["P_LINKPLANTID"].ToString();


                Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").SetValue(parameters["P_PRODUCTDEFID"].ToString());
                Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").Text = Format.GetString(parameters["P_PRODUCTNAME"]);
                Conditions.GetControl<SmartComboBox>("P_PLANTID").EditValue = parameters["P_PLANTID"].ToString();
                SmartPeriodEdit period = Conditions.GetControl<SmartPeriodEdit>("P_PERIOD");
                period.datePeriodFr.EditValue = parameters["P_PERIOD_PERIODFR"];
                period.datePeriodTo.EditValue = parameters["P_PERIOD_PERIODTO"];

                if (parameters.ContainsKey("P_INSPTYPE"))
                    Conditions.GetControl<SmartComboBox>("P_INSPTYPE").EditValue = parameters["P_INSPTYPE"].ToString();

                if (parameters.ContainsKey("P_PLANTID"))
                    Conditions.GetControl<SmartComboBox>("P_PLANTID").EditValue = parameters["P_PLANTID"].ToString();

                if (parameters.ContainsKey("P_PRODUCTIONDIVISION"))
                    Conditions.GetControl<SmartComboBox>("P_PRODUCTIONDIVISION").EditValue = parameters["P_PRODUCTIONDIVISION"].ToString();

                if (parameters.ContainsKey("P_LOTSTANDARD"))
                    Conditions.GetControl<SmartComboBox>("P_LOTSTANDARD").EditValue = parameters["P_LOTSTANDARD"].ToString();

                if (parameters.ContainsKey("P_BARESMTTYPE"))
                    Conditions.GetControl<SmartComboBox>("P_BARESMTTYPE").EditValue = parameters["P_BARESMTTYPE"].ToString();

                if (parameters.ContainsKey("P_INSPECTIONPROCESS"))
                    Conditions.GetControl<SmartComboBox>("P_INSPECTIONPROCESS").EditValue = parameters["P_INSPECTIONPROCESS"].ToString();

                if (parameters.ContainsKey("P_PRODSHAPETYPE"))
                    Conditions.GetControl<SmartComboBox>("P_PRODSHAPETYPE").EditValue = parameters["P_PRODSHAPETYPE"].ToString();

                if (parameters.ContainsKey("P_QCSEGMENTID"))
                    Conditions.GetControl<SmartSelectPopupEdit>("P_QCSEGMENTID").SetValue(parameters["P_QCSEGMENTID"].ToString());

                if (parameters.ContainsKey("P_DEFECTCODE"))
                    Conditions.GetControl<SmartSelectPopupEdit>("P_DEFECTCODE").SetValue(parameters["P_DEFECTCODE"].ToString());

                if (parameters.ContainsKey("Tab"))
                {
                    if (parameters["Tab"].ToString().Equals("Detail"))
                        this.smartTabControl1.SelectedTabPage = tapDefectDetail;
                }

                this.OnSearchAsync();
            }
        }
    }
}
