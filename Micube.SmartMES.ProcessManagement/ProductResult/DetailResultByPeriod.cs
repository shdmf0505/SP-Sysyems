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
using Micube.SmartMES.Commons;
using DevExpress.Utils.Menu;
using Micube.SmartMES.Commons.Controls;

#endregion

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 공정관리 > 생산실적 > 기간별 상세 실적
    /// 업  무  설  명  : 
    /// 생    성    자  : 황유성
    /// 생    성    일  : 2020-03-25
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class DetailResultByPeriod : SmartConditionManualBaseForm
    {
        #region Local Variables

        #endregion

        #region 생성자

        public DetailResultByPeriod()
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
            grdResult.GridButtonItem = GridButtonItem.Export;
            grdResult.View.SetIsReadOnly();

            grdResult.View.AddTextBoxColumn("PRODUCTIONDATE", 120).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetTextAlignment(TextAlignment.Center);
            grdResult.View.AddTextBoxColumn("AREAID", 90).SetTextAlignment(TextAlignment.Center);
            grdResult.View.AddTextBoxColumn("AREANAME", 170);
            grdResult.View.AddTextBoxColumn("USERSEQUENCE", 70).SetTextAlignment(TextAlignment.Center);
            grdResult.View.AddTextBoxColumn("PROCESSSEGMENTID", 70).SetTextAlignment(TextAlignment.Center);
            grdResult.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 120);
            grdResult.View.AddTextBoxColumn("OWNTYPE", 80).SetTextAlignment(TextAlignment.Center);
            grdResult.View.AddTextBoxColumn("PRODUCTDEFID", 120);
            grdResult.View.AddTextBoxColumn("PRODUCTDEFVERSION", 70);
            grdResult.View.AddTextBoxColumn("PRODUCTDEFNAME", 280);
            grdResult.View.AddTextBoxColumn("LAYER", 70);
            grdResult.View.AddTextBoxColumn("LOTINPUTTYPE", 80).SetTextAlignment(TextAlignment.Center);
            grdResult.View.AddTextBoxColumn("PRODUCTIONTYPE", 80).SetTextAlignment(TextAlignment.Center);
            grdResult.View.AddTextBoxColumn("RESULTTYPE", 80).SetTextAlignment(TextAlignment.Center).SetLabel("WOTYPE");
            grdResult.View.AddTextBoxColumn("LOTID", 200);
            grdResult.View.AddSpinEditColumn("PCSARY", 80).SetDisplayFormat("#,##0.####");
            grdResult.View.AddSpinEditColumn("ARYQTY", 80).SetLabel("ARRAY").SetDisplayFormat("#,##0.####");
            grdResult.View.AddSpinEditColumn("PCSMM", 80).SetDisplayFormat("#,##0.####");
            grdResult.View.AddTextBoxColumn("PRODUCTSIZE", 100).SetTextAlignment(TextAlignment.Center);
            grdResult.View.AddTextBoxColumn("WORKCOUNT", 50).SetTextAlignment(TextAlignment.Center);
            grdResult.View.AddSpinEditColumn("PANELQTY", 80).SetLabel("PNL").SetDisplayFormat("#,##0.####")
                .SetLabel("SENDPANELQTY");
            grdResult.View.AddSpinEditColumn("QTY", 80).SetLabel("GOODQTY").SetDisplayFormat("#,##0.####");
            grdResult.View.AddSpinEditColumn("M2QTY", 80).SetLabel("NORMALM2").SetDisplayFormat("#,##0.####");
            grdResult.View.AddSpinEditColumn("DEFECTQTY", 80).SetDisplayFormat("#,##0.####");
            grdResult.View.AddSpinEditColumn("M2DEFECTQTY", 80).SetLabel("DEFECTAREAM2").SetDisplayFormat("#,##0.####");
            grdResult.View.AddTextBoxColumn("PROCESSUOM", 80);
            grdResult.View.AddSpinEditColumn("UNITQTY", 80).SetLabel("UNITGOODQTY").SetDisplayFormat("#,##0.####");
            grdResult.View.AddSpinEditColumn("UNITDEFECTQTY", 80).SetDisplayFormat("#,##0.####");
            grdResult.View.AddTextBoxColumn("INPUTTIME", 120).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetTextAlignment(TextAlignment.Center);
            grdResult.View.AddSpinEditColumn("STAYINGHOUR", 80).SetLabel("DELAYTIME").SetDisplayFormat("#,##0.##");
            grdResult.View.AddTextBoxColumn("RECEIVETIME", 120).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetTextAlignment(TextAlignment.Center);
            grdResult.View.AddTextBoxColumn("WORKSTARTTIME", 120).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetTextAlignment(TextAlignment.Center);
            grdResult.View.AddTextBoxColumn("WORKENDTIME", 120).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetTextAlignment(TextAlignment.Center);
            grdResult.View.AddTextBoxColumn("SENDTIME", 120).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetTextAlignment(TextAlignment.Center).SetLabel("SENDLOTTIME");
            grdResult.View.AddSpinEditColumn("HOLEDIAMETER", 80).SetLabel("MAINPI").SetDisplayFormat("#,##0.####");
            grdResult.View.AddSpinEditColumn("HOLEQTY", 80).SetDisplayFormat("#,##0.####");
            grdResult.View.AddSpinEditColumn("STACKQTY", 80).SetDisplayFormat("#,##0.####");

            grdResult.View.PopulateColumns();

        }

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            grdResult.View.RowStyle += View_RowStyle;
        }

        private void View_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (grdResult.View.GetRowCellValue(e.RowHandle, "AREAID") == DBNull.Value)
            {
                e.Appearance.BackColor = Color.LightBlue;
                e.Appearance.FontStyleDelta = FontStyle.Bold;
                e.HighPriority = true;
            }
            else if (grdResult.View.GetRowCellValue(e.RowHandle, "PRODUCTIONDATE") == DBNull.Value)
            {
                e.Appearance.BackColor = Color.LightYellow;
                e.Appearance.FontStyleDelta = FontStyle.Bold;
                e.HighPriority = true;
            }
        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        //protected override void OnToolbarSaveClick()
        //{
        //    base.OnToolbarSaveClick();

        //    // TODO : 저장 Rule 변경
        //    DataTable changed = grdList.GetChangedRows();

        //    ExecuteRule("SaveCodeClass", changed);
        //}

        #endregion

        #region 검색

        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            var values = Conditions.GetValues();
            values.Add("P_LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable result = await SqlExecuter.QueryAsyncDirect("SelectDetailResultByPeriod", "10001", values);
            //DataTable result =  SqlExecuter.Query("SelectDetailResultByPeriod", "10001",  values);

            if (result.Rows.Count < 1) 
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdResult.DataSource = result;
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // 품목
            CommonFunction.AddConditionProductPopup("P_PRODUCTDEFID", 2.2, true, Conditions, "PRODUCTDEF", "PRODUCTDEF", false);
            // 공정 
            InitializeConditionProcessSegmentId_Popup();
            // 작업장
            CommonFunction.AddConditionAreaByAuthorityPopup("P_AREAID", 4.5, true, Conditions, false, false);
        }


        /// <summary>
        /// 팝업형 조회조건 생성 - 품목
        /// </summary>
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
                .SetPosition(2.2)
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
            /// 팝업형 조회조건 생성 - 공정
            /// </summary>
            private void InitializeConditionProcessSegmentId_Popup()
        {
            var processSegmentIdPopup = this.Conditions.AddSelectPopup("P_ProcessSegmentId", new SqlQuery("GetProcessSegmentList", "10002", $"PLANTID={UserInfo.Current.Plant}"
                , $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "PROCESSSEGMENTCLASSTYPE=MiddleProcessSegmentClass"), "PROCESSSEGMENTNAME", "PROCESSSEGMENTID")
                .SetPopupLayout(Language.Get("SELECTPROCESSSEGMENTID"), PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupResultCount(0)
                .SetPopupLayoutForm(800, 800, FormBorderStyle.FixedToolWindow)
                .SetPopupAutoFillColumns("PROCESSSEGMENTNAME")
                .SetLabel("PROCESSSEGMENT")
                .SetPosition(3.5);

            processSegmentIdPopup.Conditions.AddComboBox("PROCESSSEGMENTCLASSID_MIDDLE", new SqlQuery("GetProcessSegmentClassByType", "10001", $"PLANTID={UserInfo.Current.Plant}"
                , $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "PROCESSSEGMENTCLASSTYPE=MiddleProcessSegmentClass"), "PROCESSSEGMENTCLASSNAME", "PROCESSSEGMENTCLASSID")
                .SetLabel("MIDDLEPROCESSSEGMENT");
            processSegmentIdPopup.Conditions.AddTextBox("PROCESSSEGMENT");

            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME_TOP", 150)
                .SetLabel("LARGEPROCESSSEGMENT");
            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME_MIDDLE", 150)
                .SetLabel("MIDDLEPROCESSSEGMENT");
            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150);
            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
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
        //protected override void OnValidateContent()
        //{
        //    base.OnValidateContent();

        //    // TODO : 유효성 로직 변경
        //    grdList.View.CheckValidation();

        //    DataTable changed = grdList.GetChangedRows();

        //    if (changed.Rows.Count == 0)
        //    {
        //        // 저장할 데이터가 존재하지 않습니다.
        //        throw MessageException.Create("NoSaveData");
        //    }
        //}

        #endregion

        #region Private Function

        #endregion
    }
}
