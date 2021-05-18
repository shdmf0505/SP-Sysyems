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
    /// 프 로 그 램 명  : 공정관리 > 생산실적 > 설비 별 실적 현황
    /// 업  무  설  명  : 
    /// 생    성    자  : 박윤신
    /// 생    성    일  : 2020-03-27
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class PerformancestatusbyEquipment : SmartConditionManualBaseForm
    {
        #region Local Variables

        #endregion

        #region 생성자

        public PerformancestatusbyEquipment()
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

            grdResult.View.AddTextBoxColumn("AREANAME", 160);
            grdResult.View.AddTextBoxColumn("EQUIPMENTID", 70);
            grdResult.View.AddTextBoxColumn("EQUIPMENTNAME", 100);
            grdResult.View.AddTextBoxColumn("TRACKINTIME", 120).SetLabel("WORKSTARTTIME").SetDisplayFormat("yyyy -MM-dd HH:mm:ss").SetTextAlignment(TextAlignment.Center);
            grdResult.View.AddTextBoxColumn("TRACKOUTTIME", 120).SetLabel("WORKENDTIME").SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetTextAlignment(TextAlignment.Center);
            grdResult.View.AddTextBoxColumn("LOTID", 160);
            grdResult.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 140);
            grdResult.View.AddTextBoxColumn("PRODUCTDEFID", 110);
            grdResult.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80);
            grdResult.View.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            grdResult.View.AddComboBoxColumn("PRODUCTIONTYPE", new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdResult.View.AddTextBoxColumn("PRODUCTSHAPE", 80).SetTextAlignment(TextAlignment.Center);
            grdResult.View.AddComboBoxColumn("LAYER", new SqlQuery("GetCodeList", "00001", "CODECLASSID=Layer", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdResult.View.AddTextBoxColumn("PCSQTY", 80);
            grdResult.View.AddTextBoxColumn("PNLQTY", 80);
            grdResult.View.AddTextBoxColumn("M2", 80);
            grdResult.View.AddTextBoxColumn("WORKMINUTE", 90).SetLabel("WORKTIME_MINUTE");
            
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
            
            if (grdResult.View.GetRowCellValue(e.RowHandle, "EQUIPMENTID") == DBNull.Value)
            {
                e.Appearance.BackColor = Color.LightBlue;
                e.Appearance.FontStyleDelta = FontStyle.Bold;
                e.HighPriority = true;
            }
            else if (grdResult.View.GetRowCellValue(e.RowHandle, "AREANAME") == DBNull.Value)
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
            
            // TODO : 조회 쿼리 변경
            DataTable result = await SqlExecuter.QueryAsync("SelectPerformancestatusbyEquipment", "10001", values);
            
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

            // TODO : 조회조건 추가 구성이 필요한 경우 사용

            // 공정 
            InitializeConditionProcessSegmentId_Popup();
            // 작업장
            CommonFunction.AddConditionAreaByAuthorityPopup("P_AREAID", 4.5, true, Conditions, false, false);
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
