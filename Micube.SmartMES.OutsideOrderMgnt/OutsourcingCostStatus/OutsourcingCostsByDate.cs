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
    public partial class OutsourcingCostsByDate : SmartConditionManualBaseForm
    {
        #region Local Variables



        #endregion

        #region 생성자

        public OutsourcingCostsByDate()
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
            grdOspDateQty.View.AddTextBoxColumn("AREAID", 80);
            grdOspDateQty.View.AddTextBoxColumn("AREANAME", 100);
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
        }
        private void InitializeGridAmount()
        {
            //grdOspDateAmount
            grdOspDateAmount.GridButtonItem = GridButtonItem.Export | GridButtonItem.Expand | GridButtonItem.Restore;
            grdOspDateAmount.View.SetIsReadOnly();

            var costbydate = grdOspDateAmount.View.AddGroupColumn("OSPCOSTSBYDATE");

            costbydate.AddTextBoxColumn("PROCESSSEGMENTID", 100);
            costbydate.AddTextBoxColumn("PROCESSSEGMENTNAME", 150);
            costbydate.AddTextBoxColumn("AREAID", 80);
            costbydate.AddTextBoxColumn("AREANAME", 100);
            costbydate.AddTextBoxColumn("OSPVENDORID", 80);
            costbydate.AddTextBoxColumn("OSPVENDORNAME", 100);
            //합계(수량,금액) OSPTOTCOSTS
            var costtotcosts = grdOspDateAmount.View.AddGroupColumn("OSPTOTCOSTS");
            costtotcosts.AddTextBoxColumn("OSPTOTPCS", 120)
               .SetTextAlignment(TextAlignment.Right)
               .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costtotcosts.AddTextBoxColumn("OSPTOTPNL", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costtotcosts.AddTextBoxColumn("OSPTOTMM", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);

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
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday01.AddTextBoxColumn("OSPDAY01MM", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday01.AddTextBoxColumn("OSPDAY01AMOUNT", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            var costospday02 = grdOspDateAmount.View.AddGroupColumn("OSPDAY02");

            costospday02.AddTextBoxColumn("OSPDAY02PCS", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday02.AddTextBoxColumn("OSPDAY02PNL", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday02.AddTextBoxColumn("OSPDAY02MM", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday02.AddTextBoxColumn("OSPDAY02AMOUNT", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            var costospday03 = grdOspDateAmount.View.AddGroupColumn("OSPDAY03");
            costospday03.AddTextBoxColumn("OSPDAY03PCS", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday03.AddTextBoxColumn("OSPDAY03PNL", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday03.AddTextBoxColumn("OSPDAY03MM", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday03.AddTextBoxColumn("OSPDAY03AMOUNT", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            var costospday04 = grdOspDateAmount.View.AddGroupColumn("OSPDAY04");
            costospday04.AddTextBoxColumn("OSPDAY04PCS", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday04.AddTextBoxColumn("OSPDAY04PNL", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday04.AddTextBoxColumn("OSPDAY04MM", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday04.AddTextBoxColumn("OSPDAY04AMOUNT", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            var costospday05 = grdOspDateAmount.View.AddGroupColumn("OSPDAY05");
            costospday05.AddTextBoxColumn("OSPDAY05PCS", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday05.AddTextBoxColumn("OSPDAY05PNL", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday05.AddTextBoxColumn("OSPDAY05MM", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday05.AddTextBoxColumn("OSPDAY05AMOUNT", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            var costospday06 = grdOspDateAmount.View.AddGroupColumn("OSPDAY06");
            costospday06.AddTextBoxColumn("OSPDAY06PCS", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday06.AddTextBoxColumn("OSPDAY06PNL", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday06.AddTextBoxColumn("OSPDAY06MM", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday06.AddTextBoxColumn("OSPDAY06AMOUNT", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            var costospday07 = grdOspDateAmount.View.AddGroupColumn("OSPDAY07");
            costospday07.AddTextBoxColumn("OSPDAY07PCS", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday07.AddTextBoxColumn("OSPDAY07PNL", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday07.AddTextBoxColumn("OSPDAY07MM", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday07.AddTextBoxColumn("OSPDAY07AMOUNT", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            var costospday08 = grdOspDateAmount.View.AddGroupColumn("OSPDAY08");
            costospday08.AddTextBoxColumn("OSPDAY08PCS", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday08.AddTextBoxColumn("OSPDAY08PNL", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday08.AddTextBoxColumn("OSPDAY08MM", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday08.AddTextBoxColumn("OSPDAY08AMOUNT", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            var costospday09 = grdOspDateAmount.View.AddGroupColumn("OSPDAY09");
            costospday09.AddTextBoxColumn("OSPDAY09PCS", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday09.AddTextBoxColumn("OSPDAY09PNL", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday09.AddTextBoxColumn("OSPDAY09MM", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday09.AddTextBoxColumn("OSPDAY09AMOUNT", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            var costospday10 = grdOspDateAmount.View.AddGroupColumn("OSPDAY10");
            costospday10.AddTextBoxColumn("OSPDAY10PCS", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday10.AddTextBoxColumn("OSPDAY10PNL", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday10.AddTextBoxColumn("OSPDAY10MM", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday10.AddTextBoxColumn("OSPDAY10AMOUNT", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            var costospday11 = grdOspDateAmount.View.AddGroupColumn("OSPDAY11");
            costospday11.AddTextBoxColumn("OSPDAY11PCS", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday11.AddTextBoxColumn("OSPDAY11PNL", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday11.AddTextBoxColumn("OSPDAY11MM", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday11.AddTextBoxColumn("OSPDAY11AMOUNT", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            var costospday12 = grdOspDateAmount.View.AddGroupColumn("OSPDAY12");
            costospday12.AddTextBoxColumn("OSPDAY12PCS", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday12.AddTextBoxColumn("OSPDAY12PNL", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday12.AddTextBoxColumn("OSPDAY12MM", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday12.AddTextBoxColumn("OSPDAY12AMOUNT", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            var costospday13 = grdOspDateAmount.View.AddGroupColumn("OSPDAY13");
            costospday13.AddTextBoxColumn("OSPDAY13PCS", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday13.AddTextBoxColumn("OSPDAY13PNL", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday13.AddTextBoxColumn("OSPDAY13MM", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday13.AddTextBoxColumn("OSPDAY13AMOUNT", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            var costospday14 = grdOspDateAmount.View.AddGroupColumn("OSPDAY14");
            costospday14.AddTextBoxColumn("OSPDAY14PCS", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday14.AddTextBoxColumn("OSPDAY14PNL", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday14.AddTextBoxColumn("OSPDAY14MM", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday14.AddTextBoxColumn("OSPDAY14AMOUNT", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            var costospday15 = grdOspDateAmount.View.AddGroupColumn("OSPDAY15");
            costospday15.AddTextBoxColumn("OSPDAY15PCS", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday15.AddTextBoxColumn("OSPDAY15PNL", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday15.AddTextBoxColumn("OSPDAY15MM", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday15.AddTextBoxColumn("OSPDAY15AMOUNT", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            var costospday16 = grdOspDateAmount.View.AddGroupColumn("OSPDAY16");
            costospday16.AddTextBoxColumn("OSPDAY16PCS", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday16.AddTextBoxColumn("OSPDAY16PNL", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday16.AddTextBoxColumn("OSPDAY16MM", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday16.AddTextBoxColumn("OSPDAY16AMOUNT", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            var costospday17 = grdOspDateAmount.View.AddGroupColumn("OSPDAY17");
            costospday17.AddTextBoxColumn("OSPDAY17PCS", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday17.AddTextBoxColumn("OSPDAY17PNL", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday17.AddTextBoxColumn("OSPDAY17MM", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday17.AddTextBoxColumn("OSPDAY17AMOUNT", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            var costospday18 = grdOspDateAmount.View.AddGroupColumn("OSPDAY18");
            costospday18.AddTextBoxColumn("OSPDAY18PCS", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday18.AddTextBoxColumn("OSPDAY18PNL", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday18.AddTextBoxColumn("OSPDAY18MM", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday18.AddTextBoxColumn("OSPDAY18AMOUNT", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            var costospday19 = grdOspDateAmount.View.AddGroupColumn("OSPDAY19");
            costospday19.AddTextBoxColumn("OSPDAY19PCS", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday19.AddTextBoxColumn("OSPDAY19PNL", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday19.AddTextBoxColumn("OSPDAY19MM", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday19.AddTextBoxColumn("OSPDAY19AMOUNT", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            var costospday20 = grdOspDateAmount.View.AddGroupColumn("OSPDAY20");
            costospday20.AddTextBoxColumn("OSPDAY20PCS", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday20.AddTextBoxColumn("OSPDAY20PNL", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday20.AddTextBoxColumn("OSPDAY20MM", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday20.AddTextBoxColumn("OSPDAY20AMOUNT", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);

            var costospday21 = grdOspDateAmount.View.AddGroupColumn("OSPDAY21");
            costospday21.AddTextBoxColumn("OSPDAY21PCS", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday21.AddTextBoxColumn("OSPDAY21PNL", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday21.AddTextBoxColumn("OSPDAY21MM", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday21.AddTextBoxColumn("OSPDAY21AMOUNT", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            var costospday22 = grdOspDateAmount.View.AddGroupColumn("OSPDAY22");
            costospday22.AddTextBoxColumn("OSPDAY22PCS", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday22.AddTextBoxColumn("OSPDAY22PNL", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday22.AddTextBoxColumn("OSPDAY22MM", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday22.AddTextBoxColumn("OSPDAY22AMOUNT", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            var costospday23 = grdOspDateAmount.View.AddGroupColumn("OSPDAY23");
            costospday23.AddTextBoxColumn("OSPDAY23PCS", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday23.AddTextBoxColumn("OSPDAY23PNL", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday23.AddTextBoxColumn("OSPDAY23MM", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday23.AddTextBoxColumn("OSPDAY23AMOUNT", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            var costospday24 = grdOspDateAmount.View.AddGroupColumn("OSPDAY24");
            costospday24.AddTextBoxColumn("OSPDAY24PCS", 120)
             .SetTextAlignment(TextAlignment.Right)
             .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday24.AddTextBoxColumn("OSPDAY24PNL", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday24.AddTextBoxColumn("OSPDAY24MM", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday24.AddTextBoxColumn("OSPDAY24AMOUNT", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            var costospday25 = grdOspDateAmount.View.AddGroupColumn("OSPDAY25");
            costospday25.AddTextBoxColumn("OSPDAY25PCS", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday25.AddTextBoxColumn("OSPDAY25PNL", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday25.AddTextBoxColumn("OSPDAY25MM", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday25.AddTextBoxColumn("OSPDAY25AMOUNT", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            var costospday26 = grdOspDateAmount.View.AddGroupColumn("OSPDAY26");
            costospday26.AddTextBoxColumn("OSPDAY26PCS", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday26.AddTextBoxColumn("OSPDAY26PNL", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday26.AddTextBoxColumn("OSPDAY26MM", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday26.AddTextBoxColumn("OSPDAY26AMOUNT", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            var costospday27 = grdOspDateAmount.View.AddGroupColumn("OSPDAY27");
            costospday27.AddTextBoxColumn("OSPDAY27PCS", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday27.AddTextBoxColumn("OSPDAY27PNL", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday27.AddTextBoxColumn("OSPDAY27MM", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday27.AddTextBoxColumn("OSPDAY27AMOUNT", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            var costospday28 = grdOspDateAmount.View.AddGroupColumn("OSPDAY28");
            costospday28.AddTextBoxColumn("OSPDAY28PCS", 120)
               .SetTextAlignment(TextAlignment.Right)
               .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday28.AddTextBoxColumn("OSPDAY28PNL", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday28.AddTextBoxColumn("OSPDAY28MM", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday28.AddTextBoxColumn("OSPDAY28AMOUNT", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            var costospday29 = grdOspDateAmount.View.AddGroupColumn("OSPDAY29");
            costospday29.AddTextBoxColumn("OSPDAY29PCS", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday29.AddTextBoxColumn("OSPDAY29PNL", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday29.AddTextBoxColumn("OSPDAY29MM", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday29.AddTextBoxColumn("OSPDAY29AMOUNT", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            var costospday30 = grdOspDateAmount.View.AddGroupColumn("OSPDAY30");
            costospday30.AddTextBoxColumn("OSPDAY30PCS", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday30.AddTextBoxColumn("OSPDAY30PNL", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday30.AddTextBoxColumn("OSPDAY30MM", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday30.AddTextBoxColumn("OSPDAY30AMOUNT", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            var costospday31 = grdOspDateAmount.View.AddGroupColumn("OSPDAY31");
            costospday31.AddTextBoxColumn("OSPDAY31PCS", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday31.AddTextBoxColumn("OSPDAY31PNL", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday31.AddTextBoxColumn("OSPDAY31MM", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costospday31.AddTextBoxColumn("OSPDAY31AMOUNT", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);

            grdOspDateAmount.View.PopulateColumns();

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
                DataTable dt = await SqlExecuter.QueryAsync("GetOutsourcingCostsByDateQty", "10001", values);

                if (dt.Rows.Count < 1) // 검색 조건에 해당하는 DATA가 없는 경우 
                {
                    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                }

                grdOspDateQty.DataSource = dt;
            }
            else if (tapCostDate.SelectedTabPage.Name.Equals("tapOspDayAmount"))
            {
                DataTable dt = await SqlExecuter.QueryAsync("GetOutsourcingCostsByDateAmount", "10001", values);

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

            InitializeConditionPopup_OspAreaid();
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
               .SetPosition(1.1);
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
