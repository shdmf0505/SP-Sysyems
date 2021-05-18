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
using Micube.SmartMES.OutsideOrderMgnt.Popup;

#endregion

namespace Micube.SmartMES.OutsideOrderMgnt
{
    /// <summary>
    /// 프 로 그 램 명  :  외주관리> 외주 가공비마감 > 외주비마감등록
    /// 업  무  설  명  :  외주비마감등록
    /// 생    성    자  : CHOISSTAR
    /// 생    성    일  : 2019-08-28
    /// 수  정  이  력  : 
    /// 
    /// 
    /// 
    /// </summary>
    public partial class OutsourcingPeriodConfirmation : SmartConditionBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가
        string _Text;                                       // 설명
        SmartBandedGrid grdList = new SmartBandedGrid();    // Main Grid

        #endregion

        #region 생성자

        public OutsourcingPeriodConfirmation()
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

            selectOspPeriodidPopup(UserInfo.Current.Plant.ToString(), "OutSourcing");
            InitializeEvent();

            InitializeGrid();

        }

        /// <summary>        
        /// 코드그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            InitializeGrid_OspActual();
            InitializationSummaryRowOspActual();

            InitializeGrid_OspEtcWork();
            InitializationSummaryOspEtcWork();

            InitializeGrid_OspEtcAmount();
            InitializationSummaryOspEtcAmount();
        }
        private void InitializeGrid_OspActual()
        {
            grdOspActual.GridButtonItem = GridButtonItem.Export | GridButtonItem.Expand | GridButtonItem.Restore;
            grdOspActual.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdOspActual.View.AddTextBoxColumn("ENTERPRISEID", 80).SetIsHidden();
            grdOspActual.View.AddTextBoxColumn("WORKGUBUN", 120).SetIsHidden();
            grdOspActual.View.AddTextBoxColumn("PERIODID", 80) .SetIsHidden();
            grdOspActual.View.AddTextBoxColumn("OLDPERIODID", 120).SetIsHidden();            // 
            grdOspActual.View.AddTextBoxColumn("PLANTID", 200)
                .SetIsHidden();
            grdOspActual.View.AddTextBoxColumn("LOTHISTKEY", 200)
                .SetIsHidden();
            grdOspActual.View.AddTextBoxColumn("ISCLOSE", 80)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsHidden(); //마감여부
            
            grdOspActual.View.AddComboBoxColumn("PERIODSTATE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=PeriodState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Open")
                .SetIsHidden();
            grdOspActual.View.AddTextBoxColumn("CLOSEDATE", 120)
                .SetDisplayFormat("yyyy-MM-dd")
                .SetIsHidden();                                                      //  
            grdOspActual.View.AddTextBoxColumn("CLOSEEUSER", 80)
                .SetIsHidden();
            grdOspActual.View.AddTextBoxColumn("CLOSEUSERNAME", 80)
                .SetIsHidden();
            grdOspActual.View.AddTextBoxColumn("SETTLEDATE", 120)
                .SetLabel("EXPSETTLEDATE")
                .SetDisplayFormat("yyyy-MM-dd");                                                     //  
            grdOspActual.View.AddTextBoxColumn("PERIODNAME", 80)
                 .SetIsReadOnly();


            grdOspActual.View.AddTextBoxColumn("PROCESSSEGMENTID", 100)
                .SetIsReadOnly();
            grdOspActual.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150)
                .SetIsReadOnly();
        
            if (UserInfo.Current.Enterprise.Equals("YOUNGPOONG"))
            {
                grdOspActual.View.AddComboBoxColumn("OSPPRODUCTIONTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OSPProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("PRODUCTIONTYPE")
                .SetEmptyItem("*", "*");  // 
            }
            else
            {
                grdOspActual.View.AddComboBoxColumn("OWNTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OwnType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                    .SetEmptyItem("*", "*")
                    .SetIsReadOnly();  // 
                grdOspActual.View.AddComboBoxColumn("PROCESSPRICETYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProcessPriceType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                    .SetEmptyItem("*", "*")
                    .SetIsReadOnly();  // 
            }
            grdOspActual.View.AddTextBoxColumn("AREAID", 80)
                .SetIsHidden();
            grdOspActual.View.AddTextBoxColumn("AREANAME", 100)
                .SetIsReadOnly();
            grdOspActual.View.AddTextBoxColumn("OSPVENDORID", 80)
                .SetIsHidden();
            grdOspActual.View.AddTextBoxColumn("OSPVENDORNAME", 100)
                .SetIsReadOnly();
            grdOspActual.View.AddTextBoxColumn("PRODUCTDEFID", 150)
                .SetIsReadOnly();
            grdOspActual.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80)
                .SetIsReadOnly();
            grdOspActual.View.AddTextBoxColumn("PRODUCTDEFNAME", 200)
                    .SetIsReadOnly();

            grdOspActual.View.AddTextBoxColumn("PERFORMANCEDATE", 100)
                    .SetIsReadOnly();                              //  
            grdOspActual.View.AddTextBoxColumn("LOTID", 200)
                .SetIsReadOnly();
            grdOspActual.View.AddTextBoxColumn("PCSQTY", 120)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);                                //  pcsqty

            grdOspActual.View.AddTextBoxColumn("PANELQTY", 120)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);                                //  panelqty
            grdOspActual.View.AddTextBoxColumn("DEFECTPCSQTY", 120)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);                                //  pcsqty

            grdOspActual.View.AddSpinEditColumn("OSPPRICE", 100)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            grdOspActual.View.AddTextBoxColumn("ACTUALAMOUNT", 120)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);

            grdOspActual.View.AddTextBoxColumn("REDUCEAMOUNT", 120)
               .SetIsReadOnly()
               .SetTextAlignment(TextAlignment.Right)
               .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            grdOspActual.View.AddTextBoxColumn("SETTLEAMOUNT", 120)
               .SetIsReadOnly()
               .SetTextAlignment(TextAlignment.Right)
               .SetDisplayFormat("#,##0", MaskTypes.Numeric);

            grdOspActual.View.PopulateColumns();
        }

        private void InitializationSummaryRowOspActual()
        {
            grdOspActual.View.Columns["SETTLEDATE"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspActual.View.Columns["SETTLEDATE"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));

            grdOspActual.View.Columns["PCSQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspActual.View.Columns["PCSQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdOspActual.View.Columns["PANELQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspActual.View.Columns["PANELQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdOspActual.View.Columns["DEFECTPCSQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspActual.View.Columns["DEFECTPCSQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdOspActual.View.Columns["ACTUALAMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspActual.View.Columns["ACTUALAMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdOspActual.View.Columns["REDUCEAMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspActual.View.Columns["REDUCEAMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdOspActual.View.Columns["SETTLEAMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspActual.View.Columns["SETTLEAMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdOspActual.View.OptionsView.ShowFooter = true;
            grdOspActual.ShowStatusBar = false;
        }

        private void InitializeGrid_OspEtcWork()
        {
            grdOspEtcWork.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Export | GridButtonItem.Restore;
            grdOspEtcWork.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdOspEtcWork.View.AddTextBoxColumn("ENTERPRISEID", 80).SetIsHidden();
            grdOspEtcWork.View.AddTextBoxColumn("WORKGUBUN", 120).SetIsHidden();
            grdOspEtcWork.View.AddTextBoxColumn("PERIODID", 120).SetIsHidden();
            grdOspEtcWork.View.AddTextBoxColumn("OLDPERIODID", 120).SetIsHidden();
            grdOspEtcWork.View.AddTextBoxColumn("ISCLOSE", 80)
                 .SetTextAlignment(TextAlignment.Center)
                 .SetIsHidden(); //마감여부

            grdOspEtcWork.View.AddTextBoxColumn("SETTLEDATE", 120)
                .SetLabel("EXPSETTLEDATE")
                .SetDisplayFormat("yyyy-MM-dd");
            grdOspEtcWork.View.AddTextBoxColumn("PERIODNAME", 80)
                .SetIsReadOnly();
            grdOspEtcWork.View.AddComboBoxColumn("PERIODSTATE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=PeriodState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                  .SetDefault("Open")
                  .SetIsHidden();
            grdOspEtcWork.View.AddTextBoxColumn("CLOSEDATE", 120)
                 .SetDisplayFormat("yyyy-MM-dd")
                 .SetIsHidden();                                                     //  
            grdOspEtcWork.View.AddTextBoxColumn("CLOSEEUSER", 80)
                .SetIsHidden();
            grdOspEtcWork.View.AddTextBoxColumn("CLOSEUSERNAME", 80)
                .SetIsHidden();
            grdOspEtcWork.View.AddTextBoxColumn("PLANTID", 120)
                .SetIsHidden();                                                          //  공장 ID
            grdOspEtcWork.View.AddTextBoxColumn("REQUESTNO", 120)
                .SetIsReadOnly();               // 의뢰번호
            grdOspEtcWork.View.AddComboBoxColumn("OSPETCTYPE", 100
                , new SqlQuery("GetCodeList", "00001", "CODECLASSID=OSPEtcType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsReadOnly(); //작업구분        작업구분           

            grdOspEtcWork.View.AddTextBoxColumn("CUSTOMERID", 120)
                .SetIsHidden();              //  고객사 ID
            grdOspEtcWork.View.AddTextBoxColumn("CUSTOMERNAME", 120).SetIsReadOnly();            //  고객사 명
            grdOspEtcWork.View.AddTextBoxColumn("AREAID", 100)
                .SetIsHidden();
            grdOspEtcWork.View.AddTextBoxColumn("AREANAME", 100)
                .SetIsReadOnly();
            grdOspEtcWork.View.AddTextBoxColumn("OSPVENDORID", 120)
                .SetIsHidden();                //  협력사 ID
            grdOspEtcWork.View.AddTextBoxColumn("OSPVENDORNAME", 120).SetIsReadOnly();              //  협력사 명
            grdOspEtcWork.View.AddTextBoxColumn("REQUESTDEPARTMENT", 120)
                .SetIsHidden();   //    요청부서ID
            grdOspEtcWork.View.AddTextBoxColumn("OSPREQUESTUSER", 120)
                .SetIsHidden();                                                                      //    요청자ID
            grdOspEtcWork.View.AddComboBoxColumn("LOTPRODUCTTYPE", 100
                , new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsReadOnly();   //   양산구분
            grdOspEtcWork.View.AddTextBoxColumn("PRODUCTDEFID", 120)
                .SetIsReadOnly();            //  제품 정의 ID
            grdOspEtcWork.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80)
                .SetIsReadOnly();       //  제품 정의 Version
            grdOspEtcWork.View.AddTextBoxColumn("PRODUCTDEFNAME", 200)
                .SetIsReadOnly();          //  제품명
            grdOspEtcWork.View.AddTextBoxColumn("PROCESSSEGMENTID", 120)
                .SetIsHidden();        //  공정 ID
            grdOspEtcWork.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 120)
                .SetIsReadOnly();    //  공정명
            grdOspEtcWork.View.AddComboBoxColumn("UNIT", 60
                 , new SqlQuery("GetUomDefinitionMapListByOsp", "10001", "UOMCATEGORY=OSP"
                 , $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "UOMDEFID", "UOMDEFID")
                .SetIsReadOnly(); //  단위
            grdOspEtcWork.View.AddTextBoxColumn("SETTLEUSER", 120)
                .SetIsHidden();                                                                          //확정자ID
            grdOspEtcWork.View.AddTextBoxColumn("SETTLEUSERNAME", 120)
                .SetIsHidden();
                                          // 확정일자
            grdOspEtcWork.View.AddSpinEditColumn("SETTLEQTY", 120)
                .SetDisplayFormat("#,##0.####", MaskTypes.Numeric)
                .SetIsReadOnly();                                          //수량_확정
            grdOspEtcWork.View.AddSpinEditColumn("SETTLEDEFECTQTY", 120)
                .SetDisplayFormat("#,##0.####", MaskTypes.Numeric)
                .SetIsReadOnly();                                         //  불량수량_확정
            grdOspEtcWork.View.AddSpinEditColumn("SETTLEPRICE", 120)
                .SetDisplayFormat("#,##0.##", MaskTypes.Numeric)
                .SetIsReadOnly();                                         //  단가_확정
            grdOspEtcWork.View.AddTextBoxColumn("ISSETTLEDEFECTACCEPT", 80)
                .SetIsHidden();       //  불량수량적용여부_확정
            grdOspEtcWork.View.AddSpinEditColumn("SETTLEAMOUNT", 120)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsReadOnly();                                        //  금액_확정
            grdOspEtcWork.View.AddTextBoxColumn("SETTLEDESCRIPTION", 200)
                .SetIsReadOnly();
            grdOspEtcWork.View.AddSpinEditColumn("REQUESTQTY", 120)
                .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);                                       // 의뢰수량
            grdOspEtcWork.View.AddSpinEditColumn("REQUESTPRICE", 120)
                .SetDisplayFormat("#,##0.##", MaskTypes.Numeric)
                .SetIsHidden();                                        // 의뢰단가
            grdOspEtcWork.View.AddSpinEditColumn("REQUESTAMOUNT", 120)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsHidden();                                        // 의뢰금액

            grdOspEtcWork.View.AddTextBoxColumn("REQUESTDESCRIPTION", 200).SetIsHidden();       // 의뢰설명
            grdOspEtcWork.View.AddTextBoxColumn("OSPREQUESTUSERNAME", 120);           //    요청자명
            grdOspEtcWork.View.AddTextBoxColumn("REQUESTDATE", 120)
                .SetDisplayFormat("yyyy-MM-dd");                                                     //  의뢰일자
            grdOspEtcWork.View.AddTextBoxColumn("ISCHARGE", 80);                  // 유상여부 Check AddCheckBoxColumn
            grdOspEtcWork.View.AddTextBoxColumn("ISREQUESTDEFECTACCEPT", 80);    // 불량적용여부_의뢰
            grdOspEtcWork.View.AddTextBoxColumn("ETCLOTID", 150);                 // 기타작업_LOT_ID

            grdOspEtcWork.View.AddTextBoxColumn("ACTUALDATE", 120)
                .SetDisplayFormat("yyyy-MM-dd"); //   작업일자
            grdOspEtcWork.View.AddTextBoxColumn("ACTUALUSERNAME", 120); // 작업자
            grdOspEtcWork.View.AddSpinEditColumn("ACTUALQTY", 120)
                .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);                                        // 수량_실적
            grdOspEtcWork.View.AddSpinEditColumn("ACTUALDEFECTQTY", 120)
                .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);                                        //  불량수량_실적
            grdOspEtcWork.View.AddSpinEditColumn("ACTUALPRICE", 120)
                .SetDisplayFormat("#,##0.##", MaskTypes.Numeric);                                        //  단가_실적
            grdOspEtcWork.View.AddSpinEditColumn("ACTUALAMOUNT", 120)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);                                         // 금액_실적
            grdOspEtcWork.View.AddTextBoxColumn("ISACTUALDEFECTACCEPT", 80);       // 불량적용여부_실적
            grdOspEtcWork.View.AddTextBoxColumn("ACTUALUSER", 120)
                .SetIsHidden();                                                                        // 작업자

            grdOspEtcWork.View.AddTextBoxColumn("ACTUALDESCRIPTION", 200).SetIsHidden();             // 작업설명

            grdOspEtcWork.View.AddTextBoxColumn("APPROVALUSERNAME", 120)
                .SetIsHidden();                                                                        // 승인자
            grdOspEtcWork.View.AddTextBoxColumn("APPROVALDATE", 120)
                .SetDisplayFormat("yyyy-MM-dd")
                .SetIsHidden();                                                         // 승인일
            grdOspEtcWork.View.AddTextBoxColumn("ISAPPROVALDEFECTACCEP", 80)
                .SetIsHidden();       // 불량수량적용여부_승인

            grdOspEtcWork.View.PopulateColumns();
        }

        private void InitializationSummaryOspEtcWork()
        {
            grdOspEtcWork.View.Columns["SETTLEDATE"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspEtcWork.View.Columns["SETTLEDATE"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));

            grdOspEtcWork.View.Columns["SETTLEQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspEtcWork.View.Columns["SETTLEQTY"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspEtcWork.View.Columns["SETTLEDEFECTQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspEtcWork.View.Columns["SETTLEDEFECTQTY"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspEtcWork.View.Columns["SETTLEAMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspEtcWork.View.Columns["SETTLEAMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdOspEtcWork.View.Columns["REQUESTQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspEtcWork.View.Columns["REQUESTQTY"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspEtcWork.View.Columns["REQUESTAMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspEtcWork.View.Columns["REQUESTAMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdOspEtcWork.View.Columns["ACTUALQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspEtcWork.View.Columns["ACTUALQTY"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspEtcWork.View.Columns["ACTUALDEFECTQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspEtcWork.View.Columns["ACTUALDEFECTQTY"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspEtcWork.View.Columns["ACTUALAMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspEtcWork.View.Columns["ACTUALAMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdOspEtcWork.View.OptionsView.ShowFooter = true;
            grdOspEtcWork.ShowStatusBar = false;
        }


        private void InitializeGrid_OspEtcAmount()
        {
            // TODO : 그리드 초기화 로직 추가
            grdOspEtcAmount.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Export | GridButtonItem.Restore;
            grdOspEtcAmount.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdOspEtcAmount.View.SetIsReadOnly();
            grdOspEtcAmount.View.AddTextBoxColumn("ENTERPRISEID", 80).SetIsHidden();
            grdOspEtcAmount.View.AddTextBoxColumn("WORKGUBUN", 120).SetIsHidden();
            grdOspEtcAmount.View.AddTextBoxColumn("PERIODID", 120).SetIsHidden();
            grdOspEtcAmount.View.AddTextBoxColumn("OLDPERIODID", 120).SetIsHidden();
            grdOspEtcAmount.View.AddTextBoxColumn("ISCLOSE", 80)
                 .SetTextAlignment(TextAlignment.Center)
                 .SetIsHidden(); //마감여부
           
            grdOspEtcAmount.View.AddComboBoxColumn("PERIODSTATE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=PeriodState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
               .SetDefault("Open")
               .SetIsHidden();
            grdOspEtcAmount.View.AddTextBoxColumn("CLOSEDATE", 120)
               .SetDisplayFormat("yyyy-MM-dd")
               .SetIsHidden();                                                     //  
            grdOspEtcAmount.View.AddTextBoxColumn("CLOSEEUSER", 80)
                .SetIsHidden();
            grdOspEtcAmount.View.AddTextBoxColumn("CLOSEUSERNAME", 80)
                .SetIsHidden();
            grdOspEtcAmount.View.AddTextBoxColumn("PLANTID", 120)
                .SetIsHidden();
            grdOspEtcAmount.View.AddTextBoxColumn("SEQ", 120).SetIsHidden();               // 정산번호
            grdOspEtcAmount.View.AddTextBoxColumn("EXPSETTLEDATE", 120)
                .SetDisplayFormat("yyyy-MM-dd");                                                //  정산일자
            grdOspEtcAmount.View.AddTextBoxColumn("PERIODNAME", 80)
                .SetIsReadOnly();
            grdOspEtcAmount.View.AddComboBoxColumn("OSPETCTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OSPEtcType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));          //  작업구분           
            grdOspEtcAmount.View.AddTextBoxColumn("EXPSETTLEDEPARTMENT", 120);              // 정산부서
            grdOspEtcAmount.View.AddTextBoxColumn("EXPSETTLEUSER", 120);                 // 정산자ID
            grdOspEtcAmount.View.AddTextBoxColumn("EXPSETTLEUSERNAME", 120);               // 정산자명
            grdOspEtcAmount.View.AddTextBoxColumn("AREAID", 100)
                .SetIsHidden();
            grdOspEtcAmount.View.AddTextBoxColumn("AREANAME", 100)
                .SetIsReadOnly();
            grdOspEtcAmount.View.AddTextBoxColumn("OSPVENDORID", 80);                     // 협력사
            grdOspEtcAmount.View.AddTextBoxColumn("OSPVENDORNAME", 120);                   // 협력사
            grdOspEtcAmount.View.AddTextBoxColumn("ACTUALDATE", 120)
                .SetDisplayFormat("yyyy-MM-dd");                                                                  // 작업일자

            grdOspEtcAmount.View.AddTextBoxColumn("PROCESSSEGMENTID", 80);        //  공정 ID
            grdOspEtcAmount.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 120);    //  공정명
            grdOspEtcAmount.View.AddTextBoxColumn("PRODUCTDEFID", 120);                    // 제품 정의 ID
            grdOspEtcAmount.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80);               // 제품 정의 Version
            grdOspEtcAmount.View.AddTextBoxColumn("PRODUCTDEFNAME", 200);                  // 제품명
            grdOspEtcAmount.View.AddSpinEditColumn("EXPSETTLEAMOUNT", 120)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);                                                   // 정산금액
            grdOspEtcAmount.View.AddTextBoxColumn("ETCDESCRIPTION", 200);                  //  설명
            //grdOspEtcAmount.View.SetAutoFillColumn("ETCDESCRIPTION");
            grdOspEtcAmount.View.PopulateColumns();

        }

        private void InitializationSummaryOspEtcAmount()
        {
            grdOspEtcAmount.View.Columns["EXPSETTLEDATE"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspEtcAmount.View.Columns["EXPSETTLEDATE"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));

            grdOspEtcAmount.View.Columns["EXPSETTLEAMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspEtcAmount.View.Columns["EXPSETTLEAMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdOspEtcAmount.View.OptionsView.ShowFooter = true;
            grdOspEtcAmount.ShowStatusBar = false;
        }


        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            grdList.View.AddingNewRow += View_AddingNewRow;
            PopupPeriodid.EditValueChanged += PopupPeriodid_EditValueChanged;

        }

        private void PopupPeriodid_EditValueChanged(object sender, EventArgs e)
        {


            if (PopupPeriodid.EditValue.ToString().Equals(""))
            {
                txtPeriodstate.Text = "";
                return;
            }

        }
        /// <summary>
        /// 작업장 combox 
        /// </summary>
        /// <param name="sPlantid"></param>

        private void selectOspPeriodidPopup(string sPlantid,string strPeriodtype)
        {

            ConditionItemSelectPopup popup = new ConditionItemSelectPopup();
            popup.SetPopupLayoutForm(520, 600, FormBorderStyle.FixedDialog);
            popup.SetPopupLayout("PERIODNAME", PopupButtonStyles.Ok_Cancel, true, false);
            popup.Id = "PERIODNAME";
            popup.LabelText = "PERIODNAME";
            popup.SearchQuery = new SqlQuery("GetPeriodidListByOsp", "10001", $"P_ENTERPRISEID={UserInfo.Current.Enterprise}"
                                                                        , $"USERID={UserInfo.Current.Id}"
                                                                        , $"LANGUAGETYPE={UserInfo.Current.LanguageType}"
                                                                        , $"P_PLANTID={sPlantid}"
                                                                        , $"P_PERIODTYPE={strPeriodtype}");
            popup.IsMultiGrid = false;
            popup.SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
            {
                foreach (DataRow row in selectedRow)
                {
                    if (selectedRow.Count() > 0)
                    {
                        txtPeriodstate.Text = row["PERIODSTATE"].ToString();
      
                    }

                }
            });
            popup.DisplayFieldName = "PERIODNAME";
            popup.ValueFieldName = "PERIODID";
            popup.LanguageKey = "PERIODID";

            popup.Conditions.AddTextBox("P_PERIODNAME")
                .SetLabel("PERIODNAME");
            popup.GridColumns.AddTextBoxColumn("PERIODID", 120)
                .SetLabel("PERIODID")
                .SetIsHidden();
            popup.GridColumns.AddTextBoxColumn("PERIODNAME", 120)
               .SetLabel("PERIODNAME");
            popup.GridColumns.AddTextBoxColumn("PERIODSTATE", 200)
                .SetLabel("PERIODSTATE");

            PopupPeriodid.SelectPopupCondition = popup;
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
            //base.OnToolbarSaveClick();

            //// TODO : 저장 Rule 변경
            //DataTable changed = grdList.GetChangedRows();

            //ExecuteRule("SaveCodeClass", changed);
        }
        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            //base.OnToolbarClick(sender, e);
            SmartButton btn = sender as SmartButton;

            if (btn.Name.ToString().Equals("CloseProcess"))
            {

                ProcCloseprocess(btn.Text);
            }
            if (btn.Name.ToString().Equals("CloseCancel"))
            {

                ProcClosecancel(btn.Text);
            }
            if (btn.Name.ToString().Equals("Closeperiod"))
            {

                ProcCloseperiod(btn.Text); 
            }

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
            if (values["P_EXPSETTLEDATE_PERIODFR"].ToString().Equals("") && values["P_EXPSETTLEDATE_PERIODTO"].ToString().Equals("")
                && values["P_SENDDATE_PERIODFR"].ToString().Equals("") && values["P_SENDDATE_PERIODTO"].ToString().Equals(""))
            {
                ShowMessage("NoSelectData"); // 검색조건중 (정산기간은 입력해야합니다.다국어
                return;
            }
            if (values["P_PROCESSSEGMENTID"].ToString().Equals("") && values["P_PRODUCTCODE"].ToString().Equals("")
              && values["P_PRODUCTDEFNAME"].ToString().Equals("") && values["P_AREAID"].ToString().Equals("") && values["P_LOTID"].ToString().Equals("")
              && values["P_OSPVENDORID"].ToString().Equals(""))
            {
                ShowMessage("NoSelectData"); // 검색조건중 적어도(공정,품목,작업장,lot no) 기간입력해야합니다.다국어
                return;
            }
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            #region 품목코드 전환 처리 
            if (!(values["P_PRODUCTCODE"] == null))
            {
                string sproductcode = values["P_PRODUCTCODE"].ToString();
                // 품목코드값이 있으면
                if (!(sproductcode.Equals("")))
                {
                    string[] sproductd = sproductcode.Split('|');
                    // plant 정보 다시 가져오기 
                    values.Add("P_PRODUCTDEFID", sproductd[0].ToString());
                    values.Add("P_PRODUCTDEFVERSION", sproductd[1].ToString());
                }
            }
            #endregion
            if (values["P_YEARMONTH"] != null)
            {
                string stryearmonth = values["P_YEARMONTH"].ToString();
                if (!(stryearmonth.Equals("")))
                {
                    DateTime dtyearmonth = Convert.ToDateTime(stryearmonth);
                    values.Add("P_PERIODNAME", dtyearmonth.ToString("yyyy-MM"));
                }
            }
            //기간관리
            if (!(values["P_EXPSETTLEDATE_PERIODFR"].ToString().Equals("")))
            {
                DateTime requestDateFr = Convert.ToDateTime(values["P_EXPSETTLEDATE_PERIODFR"]);
                values.Remove("P_EXPSETTLEDATE_PERIODFR");
                values.Add("P_EXPSETTLEDATE_PERIODFR", string.Format("{0:yyyy-MM-dd}", requestDateFr));
            }
            if (!(values["P_EXPSETTLEDATE_PERIODTO"].ToString().Equals("")))
            {
                DateTime requestDateTo = Convert.ToDateTime(values["P_EXPSETTLEDATE_PERIODTO"]);
                values.Remove("P_EXPSETTLEDATE_PERIODTO");
                //requestDateTo = requestDateTo.AddDays(1);
                values.Add("P_EXPSETTLEDATE_PERIODTO", string.Format("{0:yyyy-MM-dd}", requestDateTo));
            }
            values = Commons.CommonFunction.ConvertParameter(values);
            if (tapIsExcept.SelectedTabPage.Name.Equals("tapOspActual"))
            {
                DataTable dt = await SqlExecuter.QueryAsync("GetOutsourcingPeriodConfirmationOspActual", "10001", values);

                if (dt.Rows.Count < 1) // 검색 조건에 해당하는 DATA가 없는 경우 
                {
                    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                }

                grdOspActual.DataSource = dt;
            }
            else if (tapIsExcept.SelectedTabPage.Name.Equals("tapOspEtcWork"))
            {
                DataTable dt = await SqlExecuter.QueryAsync("GetOutsourcingPeriodConfirmationOspEtcWork", "10001", values);

                if (dt.Rows.Count < 1) // 검색 조건에 해당하는 DATA가 없는 경우 
                {
                    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                }

                grdOspEtcWork.DataSource = dt;
            }
            else   //tapOspEtcAmount
            {
                DataTable dt = await SqlExecuter.QueryAsync("GetOutsourcingPeriodConfirmationOspEtcAmount", "10001", values);

                if (dt.Rows.Count < 1) // 검색 조건에 해당하는 DATA가 없는 경우 
                {
                    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                }

                grdOspEtcAmount.DataSource = dt;
            }
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            InitializeCondition_Yearmonth();

            InitializeConditionPopup_PeriodTypeOSP();
           
            //품목코드
            InitializeConditionPopup_ProductDefId();
            //품목명
            //공정id
            InitializeConditionPopup_ProcessSegment();
            //lotid
            InitializeConditionPopup_Lotno();
            //생산구분 

            //작업장.
            InitializeConditionPopup_OspAreaid();
            //협력사
            //협력사명 
            InitializeConditionPopup_OspVendorid();

            //정산여부 
            InitializeCondition_IsSettle();
        }

        /// <summary>
        ///외주실적
        /// </summary>
        private void InitializeConditionPopup_PeriodTypeOSP()
        {

            var owntypecbobox = Conditions.AddComboBox("p_PeriodType", new SqlQuery("GetCodeList", "00001", "CODECLASSID=PeriodTypeOSP", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
               .SetLabel("PERIODTYPEOSP")
               .SetPosition(0.2)
               .SetValidationIsRequired()
               .SetDefault("OutSourcing")
            ;
        }
        private void InitializeCondition_Yearmonth()
        {
            DateTime dateNow = DateTime.Now;
            string strym = dateNow.ToString("yyyy-MM");
            var YearmonthDT = Conditions.AddDateEdit("p_yearmonth")
               .SetLabel("CLOSEYM")
               .SetDisplayFormat("yyyy-MM")
               .SetPosition(3.2)
               .SetDefault("")
;
        }

        /// <summary>
        /// 품목코드 조회조건
        /// </summary>
        private void InitializeConditionPopup_ProductDefId()
        {
            var popupProduct = Conditions.AddSelectPopup("p_productcode",
                                                                new SqlQuery("GetProductdefidlistByOsp", "10001"
                                                                                , $"LANGUAGETYPE={ UserInfo.Current.LanguageType }"
                                                                                , $"ENTERPRISEID={UserInfo.Current.Enterprise}"
                                                                                ), "PRODUCTDEFNAME", "PRODUCTDEFCODE")
                 .SetPopupLayout("PRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)
                 .SetPopupLayoutForm(650, 600)
                 .SetLabel("PRODUCTDEFID")
                 .SetPopupResultCount(1)
                 .SetPosition(3.4);
            // 팝업 조회조건
            popupProduct.Conditions.AddComboBox("P_PRODUCTDEFTYPE", new SqlQuery("GetCodeListByOspProductdeftype", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("PRODUCTDEFTYPE")
                .SetDefault("Product");

            popupProduct.Conditions.AddTextBox("PRODUCTDEFNAME")
                .SetLabel("PRODUCTDEFID");


            popupProduct.GridColumns.AddTextBoxColumn("PRODUCTDEFCODE", 150)
                .SetIsHidden();
            popupProduct.GridColumns.AddComboBoxColumn("PRODUCTDEFTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductDefType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsReadOnly();
            popupProduct.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150).SetIsReadOnly();
            popupProduct.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetIsReadOnly();
            popupProduct.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200).SetIsReadOnly();

            var txtProductName = Conditions.AddTextBox("P_PRODUCTDEFNAME")
                .SetLabel("PRODUCTDEFNAME")
                .SetPosition(3.6);
        }
        /// <summary>
        /// ProcessSegment 설정 
        /// </summary>
        private void InitializeConditionPopup_ProcessSegment()
        {
            var ProcessSegmentPopupColumn = Conditions.AddSelectPopup("p_processsegmentid",
                                                               new SqlQuery("GetProcessSegmentListByOsp", "10001"
                                                                               , $"LANGUAGETYPE={ UserInfo.Current.LanguageType }"
                                                                               , $"ENTERPRISEID={UserInfo.Current.Enterprise}"
                                                                               ), "PROCESSSEGMENTNAME", "PROCESSSEGMENTID")
            .SetPopupLayout("PROCESSSEGMENTID", PopupButtonStyles.Ok_Cancel, true, false)
            .SetPopupLayoutForm(500, 600)
            .SetLabel("PROCESSSEGMENTID")
            .SetPopupResultCount(1)
            .SetPosition(3.8);

            // 팝업 조회조건
            ProcessSegmentPopupColumn.Conditions.AddTextBox("PROCESSSEGMENTNAME")
                 .SetLabel("PROCESSSEGMENT");

            // 팝업 그리드
            ProcessSegmentPopupColumn.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150)
                .SetValidationKeyColumn();
            ProcessSegmentPopupColumn.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);

        }

        private void InitializeConditionPopup_Lotno()
        {

            var txtOspVendor = Conditions.AddTextBox("P_LOTID")
               .SetLabel("LOTID")
               .SetPosition(4.0);

        }
        /// <summary>
        /// 작업장
        /// </summary>
        private void InitializeConditionPopup_OspAreaid()
        {
            // 팝업 컬럼설정
            var vendoridPopupColumn = Conditions.AddSelectPopup("p_areaid", new SqlQuery("GetAreaidPopupListByOsp", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "AREANAME", "AREAID")
               .SetPopupLayout("AREANAME", PopupButtonStyles.Ok_Cancel, true, false)
               .SetPopupLayoutForm(520, 600)
               .SetLabel("AREANAME")
               .SetRelationIds("p_plantId")
               .SetPopupResultCount(1)
               .SetPosition(4.4);
            // 팝업 조회조건
            vendoridPopupColumn.Conditions.AddTextBox("P_AREANAME")
                .SetLabel("AREANAME");

            // 팝업 그리드
            vendoridPopupColumn.GridColumns.AddTextBoxColumn("AREAID", 120);
            vendoridPopupColumn.GridColumns.AddTextBoxColumn("AREANAME", 200);


        }
        /// <summary>
        /// 작업업체 .고객 조회조건
        /// </summary>
        private void InitializeConditionPopup_OspVendorid()
        {
            // 팝업 컬럼설정
            var vendoridPopupColumn = Conditions.AddSelectPopup("p_ospvendorid", new SqlQuery("GetVendorListByOsp", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "OSPVENDORNAME", "OSPVENDORID")
               .SetPopupLayout("OSPVENDORID", PopupButtonStyles.Ok_Cancel, true, false)
               .SetPopupLayoutForm(500, 600)
               .SetLabel("OSPVENDORID")
               .SetPopupResultCount(1)
               .SetRelationIds("p_plantid", "p_areaid")
               .SetPosition(4.6);

            // 팝업 조회조건
            vendoridPopupColumn.Conditions.AddTextBox("OSPVENDORNAME")
                .SetLabel("OSPVENDORNAME");

            // 팝업 그리드
            vendoridPopupColumn.GridColumns.AddTextBoxColumn("OSPVENDORID", 150)
                .SetValidationKeyColumn();
            vendoridPopupColumn.GridColumns.AddTextBoxColumn("OSPVENDORNAME", 200);

            var txtOspVendor = Conditions.AddTextBox("P_OSPVENDORNAME")
              .SetLabel("OSPVENDORNAME")
              .SetPosition(4.8);


        }

        /// <summary>
        /// 정산여부
        /// </summary>
        private void InitializeCondition_IsSettle()
        {
            var YesNobox = Conditions.AddComboBox("p_isClose", new SqlQuery("GetCodeList", "00001", $"CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                 .SetLabel("ISCLOSE")
                 .SetPosition(5.4)
                 .SetEmptyItem("", "")
                 .SetDefault("*")
                 ;
        }
        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
            SmartComboBox cboPlaint = Conditions.GetControl<SmartComboBox>("p_plantid");
            
            cboPlaint.EditValueChanged += cboPlaint_EditValueChanged;
            SmartComboBox cboPeriodTypeOSP = Conditions.GetControl<SmartComboBox>("P_PERIODTYPE");
            cboPeriodTypeOSP.EditValueChanged += cboPeriodTypeOSP_EditValueChanged;
        }
        private void cboPlaint_EditValueChanged(object sender, EventArgs e)
        {
            var values = Conditions.GetValues();

            string strPlantid = values["P_PLANTID"].ToString();
            string strPeriodtype = values["P_PERIODTYPE"].ToString();
            selectOspPeriodidPopup(strPlantid, strPeriodtype);
        }
        private void cboPeriodTypeOSP_EditValueChanged(object sender, EventArgs e)
        {
            var values = Conditions.GetValues();

            string strPlantid = values["P_PLANTID"].ToString();
            string strPeriodtype = values["P_PERIODTYPE"].ToString();
            selectOspPeriodidPopup(strPlantid, strPeriodtype);
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
        private void ProcCloseperiod(string strtitle)
        {
            var values = Conditions.GetValues();
            string strPlantid = values["P_PLANTID"].ToString();
            OutsourcingPeriodConfirmationPopup itemPopup = new OutsourcingPeriodConfirmationPopup();
            itemPopup.ShowDialog(this);
        }
            // Settlecancel ProcSettleprocess
        private void ProcClosecancel(string strtitle)
        {
            if (blClosecancelCheck(strtitle) == false) return;
            DialogResult result = System.Windows.Forms.DialogResult.No;
            DataRow dr = null;
            DataTable dtSave = null;
            result = this.ShowMessage(MessageBoxButtons.YesNo, "OspDoProcessWant", strtitle);//저장하시겠습니까? 

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    this.ShowWaitArea();
                  
                    if (tapIsExcept.SelectedTabPage.Name.Equals("tapOspActual"))
                    {
                        dtSave = (grdOspActual.DataSource as DataTable).Clone();
                        DataTable dtcheck = grdOspActual.View.GetCheckedRows();

                        for (int irow = 0; irow < dtcheck.Rows.Count; irow++)
                        {
                            dr = dtcheck.Rows[irow];

                            if (dr["ISCLOSE"].ToString().Equals("Y"))
                            {
                                dr["CLOSEUSER"] = "";
                                dr["PERIODID"] = "";
                                dr["ISCLOSE"] = "N";
                                dtSave.ImportRow(dr);
                            }
                        }
                      
                        if (UserInfo.Current.Enterprise.Equals("YOUNGPOONG"))
                        {
                            dtSave.DefaultView.Sort = "OLDPERIODID desc,OSPVENDORID desc  ";
                        }
                        else
                        {
                            dtSave.DefaultView.Sort = "OLDPERIODID desc,AREAID desc ";
                        }
                        
                       // this.ExecuteRule("OutsourcingPeriodConfirmation", dtSave);
                    }
                    else if (tapIsExcept.SelectedTabPage.Name.Equals("tapOspEtcWork"))
                    {
                        DataTable dtcheck = grdOspEtcWork.View.GetCheckedRows();
                        dtSave = (grdOspEtcWork.DataSource as DataTable).Clone();
                        for (int irow = 0; irow < dtcheck.Rows.Count; irow++)
                        {
                            dr = dtcheck.Rows[irow];

                            if (dr["ISCLOSE"].ToString().Equals("Y"))
                            {
                                dr["CLOSEUSER"] = "";
                                dr["PERIODID"] = "";
                                dr["ISCLOSE"] = "N";
                                dtSave.ImportRow(dr);
                            }
                        }
                        if (UserInfo.Current.Enterprise.Equals("YOUNGPOONG"))
                        {
                            dtSave.DefaultView.Sort = "OLDPERIODID desc,OSPVENDORID desc  ";
                        }
                        else
                        {
                            dtSave.DefaultView.Sort = "OLDPERIODID desc,AREAID desc ";
                        }
                       // this.ExecuteRule("OutsourcingPeriodConfirmation", dtSave);
                    }
                    else if (tapIsExcept.SelectedTabPage.Name.Equals("tapOspEtcAmount"))
                    {
                        DataTable dtcheck = grdOspEtcAmount.View.GetCheckedRows();
                        dtSave = (grdOspEtcAmount.DataSource as DataTable).Clone();

                        for (int irow = 0; irow < dtcheck.Rows.Count; irow++)
                        {
                            dr = dtcheck.Rows[irow];

                            if (dr["ISCLOSE"].ToString().Equals("Y"))
                            {
                                dr["CLOSEUSER"] = "";
                                dr["PERIODID"] ="";
                                dr["ISCLOSE"] = "N";
                                dtSave.ImportRow(dr);
                            }
                        }
                        if (UserInfo.Current.Enterprise.Equals("YOUNGPOONG"))
                        {
                            dtSave.DefaultView.Sort = "OLDPERIODID desc,OSPVENDORID desc  ";
                        }
                        else
                        {
                            dtSave.DefaultView.Sort = "OLDPERIODID desc,AREAID desc ";
                        }
                       // this.ExecuteRule("OutsourcingPeriodConfirmation", dtSave);
                    }
                    MessageWorker worker = new MessageWorker("OutsourcingPeriodConfirmation");
                    worker.SetBody(new MessageBody()
                        {
                            { "list", dtSave }
                        });
                    worker.ExecuteWithTimeout(300);
                    ShowMessage("SuccessOspProcess");
                    OnSaveConfrimSearch();

                }
                catch (Exception ex)
                {
                    this.ShowError(ex);
                }
                finally
                {
                    this.CloseWaitArea();



                }
            }

        }

        private void ProcCloseprocess(string strtitle)
        {
            if (blCloseprocessCheck(strtitle) == false) return;
            DialogResult result = System.Windows.Forms.DialogResult.No;
            DataRow dr = null;
            DataTable dtSave = null;
            result = this.ShowMessage(MessageBoxButtons.YesNo, "OspDoProcessWant", strtitle);//저장하시겠습니까? 

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    this.ShowWaitArea();
                  
                    if (tapIsExcept.SelectedTabPage.Name.Equals("tapOspActual"))
                    {
                        dtSave = (grdOspActual.DataSource as DataTable).Clone();
                        DataTable dtcheck = grdOspActual.View.GetCheckedRows();

                        for (int irow = 0; irow < dtcheck.Rows.Count; irow++)
                        {
                            dr = dtcheck.Rows[irow];

                            if (dr["ISCLOSE"].ToString().Equals("N"))
                            {
                                dr["CLOSEUSER"] = UserInfo.Current.Id.ToString();
                                dr["PERIODID"] = PopupPeriodid.GetValue();
                                dr["ISCLOSE"] = "Y";
                                dtSave.ImportRow(dr);
                            }
                        }
                        if (UserInfo.Current.Enterprise.Equals("YOUNGPOONG"))
                        {
                            dtSave.DefaultView.Sort = "PERIODID desc,OSPVENDORID desc  ";
                        }
                        else
                        {
                            dtSave.DefaultView.Sort = "PERIODID desc,AREAID desc ";
                        }
                      //  this.ExecuteRule("OutsourcingPeriodConfirmation", dtSave);
                    }
                    else if (tapIsExcept.SelectedTabPage.Name.Equals("tapOspEtcWork"))
                    {
                        DataTable dtcheck = grdOspEtcWork.View.GetCheckedRows();
                        dtSave = (grdOspEtcWork.DataSource as DataTable).Clone();
                        for (int irow = 0; irow < dtcheck.Rows.Count; irow++)
                        {
                            dr = dtcheck.Rows[irow];

                            if (dr["ISCLOSE"].ToString().Equals("N"))
                            {
                                dr["CLOSEUSER"] = UserInfo.Current.Id.ToString();
                                dr["PERIODID"] = PopupPeriodid.GetValue();
                                dr["ISCLOSE"] = "Y";
                                dtSave.ImportRow(dr);
                            }
                        }
                        if (UserInfo.Current.Enterprise.Equals("YOUNGPOONG"))
                        {
                            dtSave.DefaultView.Sort = "PERIODID desc,OSPVENDORID desc  ";
                        }
                        else
                        {
                            dtSave.DefaultView.Sort = "PERIODID desc,AREAID desc ";
                        }
                       // this.ExecuteRule("OutsourcingPeriodConfirmation", dtSave);
                    }
                    else if (tapIsExcept.SelectedTabPage.Name.Equals("tapOspEtcAmount"))
                    {
                        DataTable dtcheck = grdOspEtcAmount.View.GetCheckedRows();
                        dtSave = (grdOspEtcAmount.DataSource as DataTable).Clone();

                        for (int irow = 0; irow < dtcheck.Rows.Count; irow++)
                        {
                            dr = dtcheck.Rows[irow];

                            if (dr["ISCLOSE"].ToString().Equals("N"))
                            {
                                dr["CLOSEUSER"] = UserInfo.Current.Id.ToString();
                                dr["PERIODID"] = PopupPeriodid.GetValue();
                                dr["ISCLOSE"] = "Y";
                                dtSave.ImportRow(dr);
                            }
                        }
                        if (UserInfo.Current.Enterprise.Equals("YOUNGPOONG"))
                        {
                            dtSave.DefaultView.Sort = "PERIODID desc,OSPVENDORID desc  ";
                        }
                        else
                        {
                            dtSave.DefaultView.Sort = "PERIODID desc,AREAID desc ";
                        }
                       // this.ExecuteRule("OutsourcingPeriodConfirmation", dtSave);
                    }
                    MessageWorker worker = new MessageWorker("OutsourcingPeriodConfirmation");
                    worker.SetBody(new MessageBody()
                        {
                            { "list", dtSave }
                        });
                    worker.ExecuteWithTimeout(300);
                    ShowMessage("SuccessOspProcess");
                    OnSaveConfrimSearch();

                }
                catch (Exception ex)
                {
                    this.ShowError(ex);
                }
                finally
                {
                    this.CloseWaitArea();



                }
            }
        }

        private bool blCloseprocessCheck(string strtitle)
        {
            int idatacount = 0;
            string strIsClose = "";
            string strPeriodstate = "";
            DataRow dr = null;
            //작업장 .
            if (PopupPeriodid.GetValue().ToString().Equals(""))
            {
                this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblAreaid.Text); //메세지 
                PopupPeriodid.Focus();
                return false;
            }
            if (!(txtPeriodstate.Text.Equals("Open")))
            {
                //다국어 처리 (마감되어 있는 월입니다. 마감 작업이 불가능합니다)
                this.ShowMessage("InValidOspData012");
                return false;
            }
            if (tapIsExcept.SelectedTabPage.Name.Equals("tapOspActual"))
            {
                DataTable dtcheck = grdOspActual.View.GetCheckedRows();

                if (dtcheck.Rows.Count == 0)
                {
                    ShowMessage("GridNoChecked"); //(선택된 내역이 없습니다)/
                    return false;
                }
                for (int irow = 0; irow < dtcheck.Rows.Count; irow++)
                {
                    dr = dtcheck.Rows[irow];
                    strIsClose = dr["ISCLOSE"].ToString();
                    strPeriodstate = dr["PERIODSTATE"].ToString();
                    if (strPeriodstate.Equals("Close"))
                    {
                        string lblConsumabledefid = grdOspActual.View.Columns["LOTID"].Caption.ToString();

                        this.ShowMessage(MessageBoxButtons.OK, "InValidOspData010", lblConsumabledefid + dr["LOTID"].ToString());
                        return false;

                    }
                    if (strIsClose.Equals("N"))
                    {
                        idatacount = idatacount + 1;
                    }

                }
            }
            else if (tapIsExcept.SelectedTabPage.Name.Equals("tapOspEtcWork"))
            {
                DataTable dtcheck = grdOspEtcWork.View.GetCheckedRows();

                if (dtcheck.Rows.Count == 0)
                {
                    ShowMessage("GridNoChecked"); //(선택된 내역이 없습니다)/
                    return false;
                }
                for (int irow = 0; irow < dtcheck.Rows.Count; irow++)
                {
                    dr = dtcheck.Rows[irow];
                    strPeriodstate = dr["PERIODSTATE"].ToString();
                    if (strPeriodstate.Equals("Close"))
                    {
                        string lblConsumabledefid = grdOspEtcWork.View.Columns["OSPVENDORNAME"].Caption.ToString();

                        this.ShowMessage(MessageBoxButtons.OK, "InValidOspData010", lblConsumabledefid + dr["OSPVENDORNAME"].ToString());
                        return false;

                    }
                    strIsClose = dr["ISCLOSE"].ToString();
                    if (strIsClose.Equals("N"))
                    {
                        idatacount = idatacount + 1;
                    }
                }
            }
            else if (tapIsExcept.SelectedTabPage.Name.Equals("tapOspEtcAmount"))
            {
                DataTable dtcheck = grdOspEtcAmount.View.GetCheckedRows();

                if (dtcheck.Rows.Count == 0)
                {
                    ShowMessage("GridNoChecked"); //(선택된 내역이 없습니다)/
                    return false;
                }
                for (int irow = 0; irow < dtcheck.Rows.Count; irow++)
                {
                    dr = dtcheck.Rows[irow];
                    strPeriodstate = dr["PERIODSTATE"].ToString();
                    if (strPeriodstate.Equals("Close"))
                    {
                        string lblConsumabledefid = grdOspEtcAmount.View.Columns["OSPVENDORNAME"].Caption.ToString();

                        this.ShowMessage(MessageBoxButtons.OK, "InValidOspData010", lblConsumabledefid + dr["OSPVENDORNAME"].ToString());
                        return false;

                    }
                    strIsClose = dr["ISCLOSE"].ToString();
                    if (strIsClose.Equals("N"))
                    {
                        idatacount = idatacount + 1;
                    }
                }
            }
            if (idatacount == 0)
            {
                this.ShowMessage(MessageBoxButtons.OK, "OspDataCountCheck",  strtitle);   //다국어 메세지 처리 
                return false;
            }
            return true;
        }
        private bool blClosecancelCheck(string strtitle)
        {
            int idatacount = 0;
            string strIsClose = "";
            string strPeriodstate = "";
            string strPeriodid = "";
            DataRow dr = null;
            if (tapIsExcept.SelectedTabPage.Name.Equals("tapOspActual"))
            {
                DataTable dtcheck = grdOspActual.View.GetCheckedRows();

                if (dtcheck.Rows.Count == 0)
                {
                    ShowMessage("GridNoChecked"); //(선택된 내역이 없습니다)/
                    return false;
                }
                for (int irow = 0; irow < dtcheck.Rows.Count; irow++)
                {
                    dr = dtcheck.Rows[irow];
                    strPeriodstate = dr["PERIODSTATE"].ToString();
                    if (strPeriodstate.Equals("Close"))
                    {
                        string lblConsumabledefid = grdOspActual.View.Columns["LOTID"].Caption.ToString();

                        this.ShowMessage(MessageBoxButtons.OK, "InValidOspData010", lblConsumabledefid + dr["LOTID"].ToString());
                        return false;

                    }
                    strPeriodid = dr["PERIODID"].ToString();
                    if (strPeriodid.Equals(""))
                    {
                        //마감월 확정되지 않은 자료가 존재합니다.(다국어)
                        this.ShowMessage(MessageBoxButtons.OK, "InValidOspData022");
                        return false;

                    }
                    strIsClose = dr["ISCLOSE"].ToString();
                    if (strIsClose.Equals("Y"))
                    {
                        idatacount = idatacount + 1;
                    }
                }
            }
            else if (tapIsExcept.SelectedTabPage.Name.Equals("tapOspEtcWork"))
            {
                DataTable dtcheck = grdOspEtcWork.View.GetCheckedRows();

                if (dtcheck.Rows.Count == 0)
                {
                    ShowMessage("GridNoChecked"); //(선택된 내역이 없습니다)/
                    return false;
                }
                for (int irow = 0; irow < dtcheck.Rows.Count; irow++)
                {
                    dr = dtcheck.Rows[irow];
                    strPeriodstate = dr["PERIODSTATE"].ToString();
                    if (strPeriodstate.Equals("Close"))
                    {
                        string lblConsumabledefid = grdOspEtcWork.View.Columns["OSPVENDORNAME"].Caption.ToString();

                        this.ShowMessage(MessageBoxButtons.OK, "InValidOspData010", lblConsumabledefid + dr["OSPVENDORNAME"].ToString());
                        return false;

                    }
                    strPeriodid = dr["PERIODID"].ToString();
                    if (strPeriodid.Equals(""))
                    {
                        string lblConsumabledefid = grdOspActual.View.Columns["PERIODNAME"].Caption.ToString();
                        //마감되어 있습니다. 
                        this.ShowMessage(MessageBoxButtons.OK, "InValidOspData010", lblConsumabledefid + dr["OSPVENDORNAME"].ToString());
                        return false;

                    }
                    strIsClose = dr["ISCLOSE"].ToString();
                    if (strIsClose.Equals("Y"))
                    {
                        idatacount = idatacount + 1;
                    }
                }
            }
            else if (tapIsExcept.SelectedTabPage.Name.Equals("tapOspEtcAmount"))
            {
                DataTable dtcheck = grdOspEtcAmount.View.GetCheckedRows();

                if (dtcheck.Rows.Count == 0)
                {
                    ShowMessage("GridNoChecked"); //(선택된 내역이 없습니다)/
                    return false;
                }
                for (int irow = 0; irow < dtcheck.Rows.Count; irow++)
                {
                    dr = dtcheck.Rows[irow];
                    strPeriodstate = dr["PERIODSTATE"].ToString();
                    if (strPeriodstate.Equals("Close"))
                    {
                        string lblConsumabledefid = grdOspEtcAmount.View.Columns["OSPVENDORNAME"].Caption.ToString();

                        this.ShowMessage(MessageBoxButtons.OK, "InValidOspData010", lblConsumabledefid + dr["OSPVENDORNAME"].ToString());
                        return false;

                    }
                    strPeriodid = dr["PERIODID"].ToString();
                    if (strPeriodid.Equals(""))
                    {
                        string lblConsumabledefid = grdOspActual.View.Columns["PERIODNAME"].Caption.ToString();
                        //마감되어 있습니다. 
                        this.ShowMessage(MessageBoxButtons.OK, "InValidOspData010", lblConsumabledefid + dr["OSPVENDORNAME"].ToString());
                        return false;

                    }
                    strIsClose = dr["ISCLOSE"].ToString();
                    if (strIsClose.Equals("Y"))
                    {
                        idatacount = idatacount + 1;
                    }
                }
            }
           
            if (idatacount == 0)
            {
                this.ShowMessage(MessageBoxButtons.OK, "OspDataCountCheck", strtitle);   //다국어 메세지 처리 
                return false;
            }
            return true;
        }

        private void OnSaveConfrimSearch()
        {

            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            string stryearmonth = values["P_YEARMONTH"].ToString();
            if (!(stryearmonth.Equals("")))
            {
                DateTime dtyearmonth = Convert.ToDateTime(stryearmonth);
                values.Add("P_PERIODNAME", dtyearmonth.ToString("yyyy-MM"));
            }
            #region 품목코드 전환 처리 
            if (!(values["P_PRODUCTCODE"] == null))
            {
                string sproductcode = values["P_PRODUCTCODE"].ToString();
                // 품목코드값이 있으면
                if (!(sproductcode.Equals("")))
                {
                    string[] sproductd = sproductcode.Split('|');
                    // plant 정보 다시 가져오기 
                    values.Add("P_PRODUCTDEFID", sproductd[0].ToString());
                    values.Add("P_PRODUCTDEFVERSION", sproductd[1].ToString());
                }
            }
            #endregion
            //기간관리
            if (!(values["P_EXPSETTLEDATE_PERIODFR"].ToString().Equals("")))
            {
                DateTime requestDateFr = Convert.ToDateTime(values["P_EXPSETTLEDATE_PERIODFR"]);
                values.Remove("P_EXPSETTLEDATE_PERIODFR");
                values.Add("P_EXPSETTLEDATE_PERIODFR", string.Format("{0:yyyy-MM-dd}", requestDateFr));
            }
            if (!(values["P_EXPSETTLEDATE_PERIODTO"].ToString().Equals("")))
            {
                DateTime requestDateTo = Convert.ToDateTime(values["P_EXPSETTLEDATE_PERIODTO"]);
                values.Remove("P_EXPSETTLEDATE_PERIODTO");
                //requestDateTo = requestDateTo.AddDays(1);
                values.Add("P_EXPSETTLEDATE_PERIODTO", string.Format("{0:yyyy-MM-dd}", requestDateTo));
            }
            values = Commons.CommonFunction.ConvertParameter(values);
            if (tapIsExcept.SelectedTabPage.Name.Equals("tapOspActual"))
            {
                DataTable dt =  SqlExecuter.Query("GetOutsourcingPeriodConfirmationOspActual", "10001", values);

                if (dt.Rows.Count < 1) // 검색 조건에 해당하는 DATA가 없는 경우 
                {
                    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                }

                grdOspActual.DataSource = dt;
            }
            else if (tapIsExcept.SelectedTabPage.Name.Equals("tapOspEtcWork"))
            {
                DataTable dt =  SqlExecuter.Query("GetOutsourcingPeriodConfirmationOspEtcWork", "10001", values);

                if (dt.Rows.Count < 1) // 검색 조건에 해당하는 DATA가 없는 경우 
                {
                    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                }

                grdOspEtcWork.DataSource = dt;
            }
            else   //tapOspEtcAmount
            {
                DataTable dt =  SqlExecuter.Query("GetOutsourcingPeriodConfirmationOspEtcAmount", "10001", values);

                if (dt.Rows.Count < 1) // 검색 조건에 해당하는 DATA가 없는 경우 
                {
                    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                }

                grdOspEtcAmount.DataSource = dt;
            }
           


        }
        #endregion
    }
}