#region using

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

#endregion

namespace Micube.SmartMES.OutsideOrderMgnt
{
    /// <summary>
    /// 프 로 그 램 명  :  외주관리> 외주비현황 >일별 외주비 집계
    /// 업  무  설  명  :  일별 외주비 집계
    /// 생    성    자  : CHOISSTAR
    /// 생    성    일  : 2019-09-17
    /// 수  정  이  력  : 
    /// 
    /// 
    /// 
    /// </summary>
    public partial class OutsourcingYoungPongCostsByDate : SmartConditionManualBaseForm
    {
        #region Local Variables



        #endregion

        #region 생성자

        public OutsourcingYoungPongCostsByDate()
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

            InitializeGridQty();
            InitializeGridAmount();
        }

        private void InitializeGridQty()
        {
            //grdOspDateQty
            grdOspDateQty.GridButtonItem = GridButtonItem.Export | GridButtonItem.Expand | GridButtonItem.Restore;
            grdOspDateQty.View.SetIsReadOnly();
            //공정코드
            //공정명
            grdOspDateQty.View.AddTextBoxColumn("PROCESSSEGMENTID", 100);
            grdOspDateQty.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150);
            //협력사코드
            //협력사명
            grdOspDateQty.View.AddTextBoxColumn("OSPVENDORID", 80);
            grdOspDateQty.View.AddTextBoxColumn("OSPVENDORNAME", 100);
            //합계
            grdOspDateQty.View.AddTextBoxColumn("OSPTOTCOSTS", 120)
                 .SetTextAlignment(TextAlignment.Right)
                 .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            //1~31
            grdOspDateQty.View.AddTextBoxColumn("OSPDAY01", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            grdOspDateQty.View.AddTextBoxColumn("OSPDAY02", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            grdOspDateQty.View.AddTextBoxColumn("OSPDAY03", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            grdOspDateQty.View.AddTextBoxColumn("OSPDAY04", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            grdOspDateQty.View.AddTextBoxColumn("OSPDAY05", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            grdOspDateQty.View.AddTextBoxColumn("OSPDAY06", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            grdOspDateQty.View.AddTextBoxColumn("OSPDAY07", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            grdOspDateQty.View.AddTextBoxColumn("OSPDAY08", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            grdOspDateQty.View.AddTextBoxColumn("OSPDAY09", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            grdOspDateQty.View.AddTextBoxColumn("OSPDAY10", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            grdOspDateQty.View.AddTextBoxColumn("OSPDAY11", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            grdOspDateQty.View.AddTextBoxColumn("OSPDAY12", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            grdOspDateQty.View.AddTextBoxColumn("OSPDAY13", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            grdOspDateQty.View.AddTextBoxColumn("OSPDAY14", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            grdOspDateQty.View.AddTextBoxColumn("OSPDAY15", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            grdOspDateQty.View.AddTextBoxColumn("OSPDAY16", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            grdOspDateQty.View.AddTextBoxColumn("OSPDAY17", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            grdOspDateQty.View.AddTextBoxColumn("OSPDAY18", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            grdOspDateQty.View.AddTextBoxColumn("OSPDAY19", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            grdOspDateQty.View.AddTextBoxColumn("OSPDAY20", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            grdOspDateQty.View.AddTextBoxColumn("OSPDAY21", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            grdOspDateQty.View.AddTextBoxColumn("OSPDAY22", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            grdOspDateQty.View.AddTextBoxColumn("OSPDAY23", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            grdOspDateQty.View.AddTextBoxColumn("OSPDAY24", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            grdOspDateQty.View.AddTextBoxColumn("OSPDAY25", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            grdOspDateQty.View.AddTextBoxColumn("OSPDAY26", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            grdOspDateQty.View.AddTextBoxColumn("OSPDAY27", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            grdOspDateQty.View.AddTextBoxColumn("OSPDAY28", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            grdOspDateQty.View.AddTextBoxColumn("OSPDAY29", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            grdOspDateQty.View.AddTextBoxColumn("OSPDAY30", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            grdOspDateQty.View.AddTextBoxColumn("OSPDAY31", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);

            grdOspDateQty.View.PopulateColumns();


            grdOspDateQty.View.Columns["OSPVENDORNAME"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateQty.View.Columns["OSPVENDORNAME"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));
            grdOspDateQty.View.Columns["OSPTOTCOSTS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateQty.View.Columns["OSPTOTCOSTS"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateQty.View.Columns["OSPDAY01"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateQty.View.Columns["OSPDAY01"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateQty.View.Columns["OSPDAY02"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateQty.View.Columns["OSPDAY02"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateQty.View.Columns["OSPDAY03"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateQty.View.Columns["OSPDAY03"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateQty.View.Columns["OSPDAY04"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateQty.View.Columns["OSPDAY04"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateQty.View.Columns["OSPDAY05"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateQty.View.Columns["OSPDAY05"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateQty.View.Columns["OSPDAY06"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateQty.View.Columns["OSPDAY06"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateQty.View.Columns["OSPDAY07"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateQty.View.Columns["OSPDAY07"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateQty.View.Columns["OSPDAY08"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateQty.View.Columns["OSPDAY08"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateQty.View.Columns["OSPDAY09"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateQty.View.Columns["OSPDAY09"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateQty.View.Columns["OSPDAY10"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateQty.View.Columns["OSPDAY10"].SummaryItem.DisplayFormat = "{0:###,###.####}";

            grdOspDateQty.View.Columns["OSPDAY11"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateQty.View.Columns["OSPDAY11"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateQty.View.Columns["OSPDAY12"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateQty.View.Columns["OSPDAY12"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateQty.View.Columns["OSPDAY13"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateQty.View.Columns["OSPDAY13"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateQty.View.Columns["OSPDAY14"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateQty.View.Columns["OSPDAY14"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateQty.View.Columns["OSPDAY15"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateQty.View.Columns["OSPDAY15"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateQty.View.Columns["OSPDAY16"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateQty.View.Columns["OSPDAY16"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateQty.View.Columns["OSPDAY17"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateQty.View.Columns["OSPDAY17"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateQty.View.Columns["OSPDAY18"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateQty.View.Columns["OSPDAY18"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateQty.View.Columns["OSPDAY19"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateQty.View.Columns["OSPDAY19"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateQty.View.Columns["OSPDAY20"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateQty.View.Columns["OSPDAY20"].SummaryItem.DisplayFormat = "{0:###,###.####}";

            grdOspDateQty.View.Columns["OSPDAY21"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateQty.View.Columns["OSPDAY21"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateQty.View.Columns["OSPDAY22"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateQty.View.Columns["OSPDAY22"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateQty.View.Columns["OSPDAY23"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateQty.View.Columns["OSPDAY23"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateQty.View.Columns["OSPDAY24"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateQty.View.Columns["OSPDAY24"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateQty.View.Columns["OSPDAY25"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateQty.View.Columns["OSPDAY25"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateQty.View.Columns["OSPDAY26"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateQty.View.Columns["OSPDAY26"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateQty.View.Columns["OSPDAY27"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateQty.View.Columns["OSPDAY27"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateQty.View.Columns["OSPDAY28"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateQty.View.Columns["OSPDAY28"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateQty.View.Columns["OSPDAY29"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateQty.View.Columns["OSPDAY29"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateQty.View.Columns["OSPDAY30"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateQty.View.Columns["OSPDAY30"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateQty.View.Columns["OSPDAY31"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateQty.View.Columns["OSPDAY31"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateQty.View.OptionsView.ShowFooter = true;
            grdOspDateQty.ShowStatusBar = false;
        }
        private void InitializeGridAmount()
        {
            //grdOspDateAmount
            grdOspDateAmount.GridButtonItem = GridButtonItem.Export | GridButtonItem.Expand | GridButtonItem.Restore;
            grdOspDateAmount.View.SetIsReadOnly();

            var costbydate = grdOspDateAmount.View.AddGroupColumn("OSPCOSTSBYDATE");

            costbydate.AddTextBoxColumn("PROCESSSEGMENTID", 100);
            costbydate.AddTextBoxColumn("PROCESSSEGMENTNAME", 150);

            costbydate.AddTextBoxColumn("OSPVENDORID", 80);
            costbydate.AddTextBoxColumn("OSPVENDORNAME", 100);
            //합계(수량,금액) OSPTOTCOSTS
            var costtotcosts = grdOspDateAmount.View.AddGroupColumn("OSPTOTCOSTS");
            costtotcosts.AddTextBoxColumn("OSPTOTPCS", 120)
               .SetTextAlignment(TextAlignment.Right)
               .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costtotcosts.AddTextBoxColumn("OSPTOTPNL", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            costtotcosts.AddTextBoxColumn("OSPTOTMM", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);

            costtotcosts.AddTextBoxColumn("OSPTOTAMOUNT", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            //1~31
            var costospday01 = grdOspDateAmount.View.AddGroupColumn("OSPDAY01");
            costospday01.AddTextBoxColumn("OSPDAY01PCS", 120)
               .SetTextAlignment(TextAlignment.Right)
               .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday01.AddTextBoxColumn("OSPDAY01PNL", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            costospday01.AddTextBoxColumn("OSPDAY01MM", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            costospday01.AddTextBoxColumn("OSPDAY01AMOUNT", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            var costospday02 = grdOspDateAmount.View.AddGroupColumn("OSPDAY02");

            costospday02.AddTextBoxColumn("OSPDAY02PCS", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday02.AddTextBoxColumn("OSPDAY02PNL", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            costospday02.AddTextBoxColumn("OSPDAY02MM", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            costospday02.AddTextBoxColumn("OSPDAY02AMOUNT", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            var costospday03 = grdOspDateAmount.View.AddGroupColumn("OSPDAY03");
            costospday03.AddTextBoxColumn("OSPDAY03PCS", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday03.AddTextBoxColumn("OSPDAY03PNL", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            costospday03.AddTextBoxColumn("OSPDAY03MM", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            costospday03.AddTextBoxColumn("OSPDAY03AMOUNT", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            var costospday04 = grdOspDateAmount.View.AddGroupColumn("OSPDAY04");
            costospday04.AddTextBoxColumn("OSPDAY04PCS", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday04.AddTextBoxColumn("OSPDAY04PNL", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            costospday04.AddTextBoxColumn("OSPDAY04MM", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            costospday04.AddTextBoxColumn("OSPDAY04AMOUNT", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            var costospday05 = grdOspDateAmount.View.AddGroupColumn("OSPDAY05");
            costospday05.AddTextBoxColumn("OSPDAY05PCS", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday05.AddTextBoxColumn("OSPDAY05PNL", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            costospday05.AddTextBoxColumn("OSPDAY05MM", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            costospday05.AddTextBoxColumn("OSPDAY05AMOUNT", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            var costospday06 = grdOspDateAmount.View.AddGroupColumn("OSPDAY06");
            costospday06.AddTextBoxColumn("OSPDAY06PCS", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday06.AddTextBoxColumn("OSPDAY06PNL", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            costospday06.AddTextBoxColumn("OSPDAY06MM", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            costospday06.AddTextBoxColumn("OSPDAY06AMOUNT", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            var costospday07 = grdOspDateAmount.View.AddGroupColumn("OSPDAY07");
            costospday07.AddTextBoxColumn("OSPDAY07PCS", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday07.AddTextBoxColumn("OSPDAY07PNL", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            costospday07.AddTextBoxColumn("OSPDAY07MM", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            costospday07.AddTextBoxColumn("OSPDAY07AMOUNT", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            var costospday08 = grdOspDateAmount.View.AddGroupColumn("OSPDAY08");
            costospday08.AddTextBoxColumn("OSPDAY08PCS", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday08.AddTextBoxColumn("OSPDAY08PNL", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            costospday08.AddTextBoxColumn("OSPDAY08MM", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            costospday08.AddTextBoxColumn("OSPDAY08AMOUNT", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            var costospday09 = grdOspDateAmount.View.AddGroupColumn("OSPDAY09");
            costospday09.AddTextBoxColumn("OSPDAY09PCS", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday09.AddTextBoxColumn("OSPDAY09PNL", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            costospday09.AddTextBoxColumn("OSPDAY09MM", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            costospday09.AddTextBoxColumn("OSPDAY09AMOUNT", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            var costospday10 = grdOspDateAmount.View.AddGroupColumn("OSPDAY10");
            costospday10.AddTextBoxColumn("OSPDAY10PCS", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday10.AddTextBoxColumn("OSPDAY10PNL", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            costospday10.AddTextBoxColumn("OSPDAY10MM", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            costospday10.AddTextBoxColumn("OSPDAY10AMOUNT", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            var costospday11 = grdOspDateAmount.View.AddGroupColumn("OSPDAY11");
            costospday11.AddTextBoxColumn("OSPDAY11PCS", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday11.AddTextBoxColumn("OSPDAY11PNL", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            costospday11.AddTextBoxColumn("OSPDAY11MM", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            costospday11.AddTextBoxColumn("OSPDAY11AMOUNT", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            var costospday12 = grdOspDateAmount.View.AddGroupColumn("OSPDAY12");
            costospday12.AddTextBoxColumn("OSPDAY12PCS", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday12.AddTextBoxColumn("OSPDAY12PNL", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            costospday12.AddTextBoxColumn("OSPDAY12MM", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            costospday12.AddTextBoxColumn("OSPDAY12AMOUNT", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            var costospday13 = grdOspDateAmount.View.AddGroupColumn("OSPDAY13");
            costospday13.AddTextBoxColumn("OSPDAY13PCS", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday13.AddTextBoxColumn("OSPDAY13PNL", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            costospday13.AddTextBoxColumn("OSPDAY13MM", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            costospday13.AddTextBoxColumn("OSPDAY13AMOUNT", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            var costospday14 = grdOspDateAmount.View.AddGroupColumn("OSPDAY14");
            costospday14.AddTextBoxColumn("OSPDAY14PCS", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday14.AddTextBoxColumn("OSPDAY14PNL", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            costospday14.AddTextBoxColumn("OSPDAY14MM", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            costospday14.AddTextBoxColumn("OSPDAY14AMOUNT", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            var costospday15 = grdOspDateAmount.View.AddGroupColumn("OSPDAY15");
            costospday15.AddTextBoxColumn("OSPDAY15PCS", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday15.AddTextBoxColumn("OSPDAY15PNL", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            costospday15.AddTextBoxColumn("OSPDAY15MM", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            costospday15.AddTextBoxColumn("OSPDAY15AMOUNT", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            var costospday16 = grdOspDateAmount.View.AddGroupColumn("OSPDAY16");
            costospday16.AddTextBoxColumn("OSPDAY16PCS", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday16.AddTextBoxColumn("OSPDAY16PNL", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            costospday16.AddTextBoxColumn("OSPDAY16MM", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            costospday16.AddTextBoxColumn("OSPDAY16AMOUNT", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            var costospday17 = grdOspDateAmount.View.AddGroupColumn("OSPDAY17");
            costospday17.AddTextBoxColumn("OSPDAY17PCS", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday17.AddTextBoxColumn("OSPDAY17PNL", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            costospday17.AddTextBoxColumn("OSPDAY17MM", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            costospday17.AddTextBoxColumn("OSPDAY17AMOUNT", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            var costospday18 = grdOspDateAmount.View.AddGroupColumn("OSPDAY18");
            costospday18.AddTextBoxColumn("OSPDAY18PCS", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday18.AddTextBoxColumn("OSPDAY18PNL", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            costospday18.AddTextBoxColumn("OSPDAY18MM", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            costospday18.AddTextBoxColumn("OSPDAY18AMOUNT", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            var costospday19 = grdOspDateAmount.View.AddGroupColumn("OSPDAY19");
            costospday19.AddTextBoxColumn("OSPDAY19PCS", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday19.AddTextBoxColumn("OSPDAY19PNL", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            costospday19.AddTextBoxColumn("OSPDAY19MM", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            costospday19.AddTextBoxColumn("OSPDAY19AMOUNT", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            var costospday20 = grdOspDateAmount.View.AddGroupColumn("OSPDAY20");
            costospday20.AddTextBoxColumn("OSPDAY20PCS", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday20.AddTextBoxColumn("OSPDAY20PNL", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            costospday20.AddTextBoxColumn("OSPDAY20MM", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            costospday20.AddTextBoxColumn("OSPDAY20AMOUNT", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);

            var costospday21 = grdOspDateAmount.View.AddGroupColumn("OSPDAY21");
            costospday21.AddTextBoxColumn("OSPDAY21PCS", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday21.AddTextBoxColumn("OSPDAY21PNL", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            costospday21.AddTextBoxColumn("OSPDAY21MM", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            costospday21.AddTextBoxColumn("OSPDAY21AMOUNT", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            var costospday22 = grdOspDateAmount.View.AddGroupColumn("OSPDAY22");
            costospday22.AddTextBoxColumn("OSPDAY22PCS", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday22.AddTextBoxColumn("OSPDAY22PNL", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            costospday22.AddTextBoxColumn("OSPDAY22MM", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            costospday22.AddTextBoxColumn("OSPDAY22AMOUNT", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            var costospday23 = grdOspDateAmount.View.AddGroupColumn("OSPDAY23");
            costospday23.AddTextBoxColumn("OSPDAY23PCS", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday23.AddTextBoxColumn("OSPDAY23PNL", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            costospday23.AddTextBoxColumn("OSPDAY23MM", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            costospday23.AddTextBoxColumn("OSPDAY23AMOUNT", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            var costospday24 = grdOspDateAmount.View.AddGroupColumn("OSPDAY24");
            costospday24.AddTextBoxColumn("OSPDAY24PCS", 120)
             .SetTextAlignment(TextAlignment.Right)
             .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday24.AddTextBoxColumn("OSPDAY24PNL", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            costospday24.AddTextBoxColumn("OSPDAY24MM", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            costospday24.AddTextBoxColumn("OSPDAY24AMOUNT", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            var costospday25 = grdOspDateAmount.View.AddGroupColumn("OSPDAY25");
            costospday25.AddTextBoxColumn("OSPDAY25PCS", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday25.AddTextBoxColumn("OSPDAY25PNL", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            costospday25.AddTextBoxColumn("OSPDAY25MM", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            costospday25.AddTextBoxColumn("OSPDAY25AMOUNT", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            var costospday26 = grdOspDateAmount.View.AddGroupColumn("OSPDAY26");
            costospday26.AddTextBoxColumn("OSPDAY26PCS", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday26.AddTextBoxColumn("OSPDAY26PNL", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            costospday26.AddTextBoxColumn("OSPDAY26MM", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            costospday26.AddTextBoxColumn("OSPDAY26AMOUNT", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            var costospday27 = grdOspDateAmount.View.AddGroupColumn("OSPDAY27");
            costospday27.AddTextBoxColumn("OSPDAY27PCS", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday27.AddTextBoxColumn("OSPDAY27PNL", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            costospday27.AddTextBoxColumn("OSPDAY27MM", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            costospday27.AddTextBoxColumn("OSPDAY27AMOUNT", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            var costospday28 = grdOspDateAmount.View.AddGroupColumn("OSPDAY28");
            costospday28.AddTextBoxColumn("OSPDAY28PCS", 120)
               .SetTextAlignment(TextAlignment.Right)
               .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday28.AddTextBoxColumn("OSPDAY28PNL", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            costospday28.AddTextBoxColumn("OSPDAY28MM", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            costospday28.AddTextBoxColumn("OSPDAY28AMOUNT", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            var costospday29 = grdOspDateAmount.View.AddGroupColumn("OSPDAY29");
            costospday29.AddTextBoxColumn("OSPDAY29PCS", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday29.AddTextBoxColumn("OSPDAY29PNL", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            costospday29.AddTextBoxColumn("OSPDAY29MM", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            costospday29.AddTextBoxColumn("OSPDAY29AMOUNT", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            var costospday30 = grdOspDateAmount.View.AddGroupColumn("OSPDAY30");
            costospday30.AddTextBoxColumn("OSPDAY30PCS", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday30.AddTextBoxColumn("OSPDAY30PNL", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            costospday30.AddTextBoxColumn("OSPDAY30MM", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            costospday30.AddTextBoxColumn("OSPDAY30AMOUNT", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            var costospday31 = grdOspDateAmount.View.AddGroupColumn("OSPDAY31");
            costospday31.AddTextBoxColumn("OSPDAY31PCS", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday31.AddTextBoxColumn("OSPDAY31PNL", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            costospday31.AddTextBoxColumn("OSPDAY31MM", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            costospday31.AddTextBoxColumn("OSPDAY31AMOUNT", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);

            grdOspDateAmount.View.PopulateColumns();
            InitializationSummaryRow();
        }
        private void InitializationSummaryRow()
        {
            grdOspDateAmount.View.Columns["OSPVENDORNAME"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPVENDORNAME"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));
            grdOspDateAmount.View.Columns["OSPTOTPCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPTOTPCS"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdOspDateAmount.View.Columns["OSPTOTPNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPTOTPNL"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateAmount.View.Columns["OSPTOTMM"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPTOTMM"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateAmount.View.Columns["OSPTOTAMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPTOTAMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdOspDateAmount.View.Columns["OSPDAY01PCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY01PCS"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdOspDateAmount.View.Columns["OSPDAY01PNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY01PNL"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateAmount.View.Columns["OSPDAY01MM"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY01MM"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateAmount.View.Columns["OSPDAY01AMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY01AMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdOspDateAmount.View.Columns["OSPDAY02PCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY02PCS"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdOspDateAmount.View.Columns["OSPDAY02PNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY02PNL"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateAmount.View.Columns["OSPDAY02MM"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY02MM"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateAmount.View.Columns["OSPDAY02AMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY02AMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdOspDateAmount.View.Columns["OSPDAY03PCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY03PCS"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdOspDateAmount.View.Columns["OSPDAY03PNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY03PNL"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateAmount.View.Columns["OSPDAY03MM"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY03MM"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateAmount.View.Columns["OSPDAY03AMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY03AMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdOspDateAmount.View.Columns["OSPDAY04PCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY04PCS"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdOspDateAmount.View.Columns["OSPDAY04PNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY04PNL"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateAmount.View.Columns["OSPDAY04MM"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY04MM"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateAmount.View.Columns["OSPDAY04AMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY04AMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdOspDateAmount.View.Columns["OSPDAY05PCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY05PCS"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdOspDateAmount.View.Columns["OSPDAY05PNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY05PNL"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateAmount.View.Columns["OSPDAY05MM"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY05MM"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateAmount.View.Columns["OSPDAY05AMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY05AMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdOspDateAmount.View.Columns["OSPDAY06PCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY06PCS"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdOspDateAmount.View.Columns["OSPDAY06PNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY06PNL"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateAmount.View.Columns["OSPDAY06MM"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY06MM"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateAmount.View.Columns["OSPDAY06AMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY06AMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdOspDateAmount.View.Columns["OSPDAY07PCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY07PCS"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdOspDateAmount.View.Columns["OSPDAY07PNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY07PNL"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateAmount.View.Columns["OSPDAY07MM"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY07MM"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateAmount.View.Columns["OSPDAY07AMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY07AMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdOspDateAmount.View.Columns["OSPDAY08PCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY08PCS"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdOspDateAmount.View.Columns["OSPDAY08PNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY08PNL"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateAmount.View.Columns["OSPDAY08MM"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY08MM"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateAmount.View.Columns["OSPDAY08AMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY08AMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdOspDateAmount.View.Columns["OSPDAY09PCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY09PCS"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdOspDateAmount.View.Columns["OSPDAY09PNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY09PNL"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateAmount.View.Columns["OSPDAY09MM"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY09MM"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateAmount.View.Columns["OSPDAY09AMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY09AMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdOspDateAmount.View.Columns["OSPDAY10PCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY10PCS"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdOspDateAmount.View.Columns["OSPDAY10PNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY10PNL"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateAmount.View.Columns["OSPDAY10MM"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY10MM"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateAmount.View.Columns["OSPDAY10AMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY10AMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdOspDateAmount.View.Columns["OSPDAY11PCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY11PCS"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdOspDateAmount.View.Columns["OSPDAY11PNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY11PNL"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateAmount.View.Columns["OSPDAY11MM"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY11MM"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateAmount.View.Columns["OSPDAY11AMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY11AMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdOspDateAmount.View.Columns["OSPDAY12PCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY12PCS"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdOspDateAmount.View.Columns["OSPDAY12PNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY12PNL"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateAmount.View.Columns["OSPDAY12MM"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY12MM"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateAmount.View.Columns["OSPDAY12AMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY12AMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdOspDateAmount.View.Columns["OSPDAY13PCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY13PCS"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdOspDateAmount.View.Columns["OSPDAY13PNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY13PNL"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateAmount.View.Columns["OSPDAY13MM"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY13MM"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateAmount.View.Columns["OSPDAY13AMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY13AMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdOspDateAmount.View.Columns["OSPDAY14PCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY14PCS"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdOspDateAmount.View.Columns["OSPDAY14PNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY14PNL"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateAmount.View.Columns["OSPDAY14MM"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY14MM"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateAmount.View.Columns["OSPDAY14AMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY14AMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdOspDateAmount.View.Columns["OSPDAY15PCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY15PCS"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdOspDateAmount.View.Columns["OSPDAY15PNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY15PNL"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateAmount.View.Columns["OSPDAY15MM"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY15MM"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateAmount.View.Columns["OSPDAY15AMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY15AMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdOspDateAmount.View.Columns["OSPDAY16PCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY16PCS"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdOspDateAmount.View.Columns["OSPDAY16PNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY16PNL"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateAmount.View.Columns["OSPDAY16MM"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY16MM"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateAmount.View.Columns["OSPDAY16AMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY16AMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdOspDateAmount.View.Columns["OSPDAY17PCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY17PCS"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdOspDateAmount.View.Columns["OSPDAY17PNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY17PNL"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateAmount.View.Columns["OSPDAY17MM"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY17MM"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateAmount.View.Columns["OSPDAY17AMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY17AMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdOspDateAmount.View.Columns["OSPDAY18PCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY18PCS"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdOspDateAmount.View.Columns["OSPDAY18PNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY18PNL"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateAmount.View.Columns["OSPDAY18MM"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY18MM"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateAmount.View.Columns["OSPDAY18AMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY18AMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdOspDateAmount.View.Columns["OSPDAY19PCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY19PCS"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdOspDateAmount.View.Columns["OSPDAY19PNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY19PNL"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateAmount.View.Columns["OSPDAY19MM"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY19MM"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateAmount.View.Columns["OSPDAY19AMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY19AMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdOspDateAmount.View.Columns["OSPDAY20PCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY20PCS"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdOspDateAmount.View.Columns["OSPDAY20PNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY20PNL"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateAmount.View.Columns["OSPDAY20MM"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY20MM"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateAmount.View.Columns["OSPDAY20AMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY20AMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdOspDateAmount.View.Columns["OSPDAY21PCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY21PCS"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdOspDateAmount.View.Columns["OSPDAY21PNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY21PNL"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateAmount.View.Columns["OSPDAY21MM"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY21MM"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateAmount.View.Columns["OSPDAY21AMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY21AMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdOspDateAmount.View.Columns["OSPDAY22PCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY22PCS"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdOspDateAmount.View.Columns["OSPDAY22PNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY22PNL"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateAmount.View.Columns["OSPDAY22MM"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY22MM"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateAmount.View.Columns["OSPDAY22AMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY22AMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdOspDateAmount.View.Columns["OSPDAY23PCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY23PCS"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdOspDateAmount.View.Columns["OSPDAY23PNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY23PNL"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateAmount.View.Columns["OSPDAY23MM"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY23MM"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateAmount.View.Columns["OSPDAY23AMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY23AMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdOspDateAmount.View.Columns["OSPDAY24PCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY24PCS"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdOspDateAmount.View.Columns["OSPDAY24PNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY24PNL"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateAmount.View.Columns["OSPDAY24MM"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY24MM"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateAmount.View.Columns["OSPDAY24AMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY24AMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdOspDateAmount.View.Columns["OSPDAY25PCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY25PCS"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdOspDateAmount.View.Columns["OSPDAY25PNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY25PNL"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateAmount.View.Columns["OSPDAY25MM"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY25MM"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateAmount.View.Columns["OSPDAY25AMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY25AMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdOspDateAmount.View.Columns["OSPDAY26PCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY26PCS"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdOspDateAmount.View.Columns["OSPDAY26PNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY26PNL"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateAmount.View.Columns["OSPDAY26MM"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY26MM"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateAmount.View.Columns["OSPDAY26AMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY26AMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdOspDateAmount.View.Columns["OSPDAY27PCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY27PCS"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdOspDateAmount.View.Columns["OSPDAY27PNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY27PNL"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateAmount.View.Columns["OSPDAY27MM"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY27MM"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateAmount.View.Columns["OSPDAY27AMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY27AMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdOspDateAmount.View.Columns["OSPDAY28PCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY28PCS"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdOspDateAmount.View.Columns["OSPDAY28PNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY28PNL"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateAmount.View.Columns["OSPDAY28MM"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY28MM"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateAmount.View.Columns["OSPDAY28AMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY28AMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdOspDateAmount.View.Columns["OSPDAY29PCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY29PCS"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdOspDateAmount.View.Columns["OSPDAY29PNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY29PNL"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateAmount.View.Columns["OSPDAY29MM"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY29MM"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateAmount.View.Columns["OSPDAY29AMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY29AMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdOspDateAmount.View.Columns["OSPDAY30PCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY30PCS"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdOspDateAmount.View.Columns["OSPDAY30PNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY30PNL"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateAmount.View.Columns["OSPDAY30MM"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY30MM"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateAmount.View.Columns["OSPDAY30AMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY30AMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";


            grdOspDateAmount.View.Columns["OSPDAY31PCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY31PCS"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdOspDateAmount.View.Columns["OSPDAY31PNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY31PNL"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateAmount.View.Columns["OSPDAY31MM"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY31MM"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspDateAmount.View.Columns["OSPDAY31AMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspDateAmount.View.Columns["OSPDAY31AMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdOspDateAmount.View.OptionsView.ShowFooter = true;
            grdOspDateAmount.ShowStatusBar = false;
        }
        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {

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

        }

        #endregion

        #region 검색

        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();
            
            var values = Conditions.GetValues();
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
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            

            values = Commons.CommonFunction.ConvertParameter(values);
            if (tapCostDate.SelectedTabPage.Name.Equals("tapOspDayQty"))
            {
                DataTable dt = await SqlExecuter.QueryAsync("GetOutsourcingYoungPongCostsByDateQty", "10001", values);

                if (dt.Rows.Count < 1) // 검색 조건에 해당하는 DATA가 없는 경우 
                {
                    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                }

                grdOspDateQty.DataSource = dt;
            }
            else if (tapCostDate.SelectedTabPage.Name.Equals("tapOspDayAmount"))
            {
                DataTable dt = await SqlExecuter.QueryAsync("GetOutsourcingYoungPongCostsByDateAmount", "10001", values);

                if (dt.Rows.Count < 1) // 검색 조건에 해당하는 DATA가 없는 경우 
                {
                    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                }

                grdOspDateAmount.DataSource = dt;
            }


        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            //1.site
            //InitializeConditionPopup_Plant();

            InitializeConditionPopup_PeriodTypeOSP();
            //6.기준년월
            InitializeCondition_Yearmonth();
            //2.대공정
            InitializeConditionPopup_Parentprocesssegmentclassid();
            //3.중공정
            InitializeConditionPopup_Processsegmentclassid();

            //InitializeConditionPopup_OspAreaid();
            //4.협력사
            InitializeConditionPopup_OspVendorid();
            //5.양산구분
            InitializeConditionPopup_ProductionType();
           
            //7.품목코드
            InitializeConditionPopup_ProductDefId();
            //8.집계구분
            InitializeConditionPopup_SumCode();
        }
        /// <summary>
        /// site 설정 
        /// </summary>
        private void InitializeConditionPopup_Plant()
        {

            var planttxtbox = Conditions.AddComboBox("p_plantid", new SqlQuery("GetPlantList", "00001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PLANTID", "PLANTID")
               .SetLabel("PLANT")
               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false)
               .SetPosition(0.1)
               .SetDefault(UserInfo.Current.Plant, "p_plantId") //기본값 설정 UserInfo.Current.Plant
               .SetIsReadOnly(true)
               .SetValidationIsRequired()
            ;
            //   

        }
        /// <summary>
        ///외주실적
        /// </summary>
        private void InitializeConditionPopup_PeriodTypeOSP()
        {

            var owntypecbobox = Conditions.AddComboBox("p_PeriodType", new SqlQuery("GetCodeList", "00001", "CODECLASSID=PeriodTypeOSP", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("PERIODTYPEOSP")
                .SetPosition(0.2)
                .SetDefault("OutSourcing") //
                .SetValidationIsRequired()
             ;
        }
        /// <summary>
        /// 기준년월
        /// </summary>
        private void InitializeCondition_Yearmonth()
        {
            DateTime dateNow = DateTime.Now;
            string strym = dateNow.ToString("yyyy-MM");
            var YearmonthDT = Conditions.AddDateEdit("p_STANDARDYM")
               .SetLabel("SETTLEYM")
               .SetDisplayFormat("yyyy-MM")
               .SetPosition(0.4)
               .SetDefault(strym)
               .SetValidationIsRequired()
            ;

        }
        /// <summary>
        /// 대공정 설정 
        /// </summary>
        private void InitializeConditionPopup_Parentprocesssegmentclassid()
        {

            // 팝업 컬럼설정
            var processsegmentclassidPopupColumn = Conditions.AddSelectPopup("p_TOPPROCESSSEGMENTCLASSID", new SqlQuery("GetParentProcesssegmentclassidListByOsp", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "TOPPROCESSSEGMENTCLASSNAME", "TOPPROCESSSEGMENTCLASSID")
               .SetPopupLayout("TOPPROCESSSEGMENTCLASS", PopupButtonStyles.Ok_Cancel, true, false)
               .SetPopupLayoutForm(400, 600)
               .SetLabel("TOPPROCESSSEGMENTCLASS")
               .SetPopupResultCount(1)
               .SetPosition(0.6);

            // 팝업 조회조건
            processsegmentclassidPopupColumn.Conditions.AddTextBox("PROCESSSEGMENTCLASSNAME")
                .SetLabel("TOPPROCESSSEGMENTCLASSNAME");

            // 팝업 그리드
            processsegmentclassidPopupColumn.GridColumns.AddTextBoxColumn("TOPPROCESSSEGMENTCLASSID", 150);
            processsegmentclassidPopupColumn.GridColumns.AddTextBoxColumn("TOPPROCESSSEGMENTCLASSNAME", 200);

        }
        /// <summary>
        /// 중공정 설정 
        /// </summary>
        private void InitializeConditionPopup_Processsegmentclassid()
        {

            // 팝업 컬럼설정
            var processsegmentclassidPopupColumn = Conditions.AddSelectPopup("p_processsegmentclassid", new SqlQuery("GetProcesssegmentclassidListByOsp", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "MIDDLEPROCESSSEGMENTCLASSNAME", "MIDDLEPROCESSSEGMENTCLASSID")
               .SetPopupLayout("MIDDLEPROCESSSEGMENTCLASS", PopupButtonStyles.Ok_Cancel, true, false)
               .SetPopupLayoutForm(400, 600)
               .SetLabel("MIDDLEPROCESSSEGMENTCLASS")
               .SetRelationIds("P_TOPPROCESSSEGMENTCLASSID")
               .SetPopupResultCount(1)
               .SetPosition(0.8);

            // 팝업 조회조건
            processsegmentclassidPopupColumn.Conditions.AddTextBox("MIDDLEPROCESSSEGMENTCLASSNAME")
                .SetLabel("MIDDLEPROCESSSEGMENTCLASSNAME");

            // 팝업 그리드
            processsegmentclassidPopupColumn.GridColumns.AddTextBoxColumn("MIDDLEPROCESSSEGMENTCLASSID", 150);
            processsegmentclassidPopupColumn.GridColumns.AddTextBoxColumn("MIDDLEPROCESSSEGMENTCLASSNAME", 200);

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
               .SetRelationIds("p_plantid")
               .SetPosition(1.2);

            // 팝업 조회조건
            vendoridPopupColumn.Conditions.AddTextBox("OSPVENDORNAME")
                .SetLabel("OSPVENDORNAME");

            // 팝업 그리드
            vendoridPopupColumn.GridColumns.AddTextBoxColumn("OSPVENDORID", 150)
                .SetValidationKeyColumn();
            vendoridPopupColumn.GridColumns.AddTextBoxColumn("OSPVENDORNAME", 200);

            var txtOspVendor = Conditions.AddTextBox("P_OSPVENDORNAME")
               .SetLabel("OSPVENDORNAME")
               .SetPosition(1.4);

        }
        /// <summary>
        ///양산구분
        /// </summary>
        private void InitializeConditionPopup_ProductionType()
        {

            var owntypecbobox = Conditions.AddComboBox("p_ProductionType", new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
               .SetLabel("PRODUCTIONTYPE")
               //.SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false)
               .SetPosition(2.2)
               .SetEmptyItem("", "")
            ;
            //   .SetIsReadOnly(true);

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
                 .SetPosition(3.2);
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
                .SetPosition(3.4);
        }
        /// <summary>
        ///양산구분
        /// </summary>
        private void InitializeConditionPopup_SumCode()
        {

            var SumCodebox = Conditions.AddComboBox("p_ospsumcode", new SqlQuery("GetCodeList", "00001", "CODECLASSID=OSPSumCode", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
               .SetLabel("OSPSUMCODE")
               .SetPosition(3.6)
               .SetDefault("PNL", "PNL")
            ;


        }
        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
        }

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();


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

        #endregion
    }
}
