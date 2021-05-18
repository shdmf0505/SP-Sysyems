#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons;
//using Micube.SmartMES.ProcessManagement;
using System;
using Micube.SmartMES.Commons.Controls;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;
using System.IO;
using DevExpress.XtraGrid.Views.Grid;



#endregion

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 신뢰성검증 > 신뢰성 검증 결과등록(정기)
    /// 업  무  설  명  : 신뢰성 정기 결과 등록 하는 화면이다.
    /// 생    성    자  : 유석진
    /// 생    성    일  : 2019-07-10
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ReliaVerifiResultRegular : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가

        #endregion

        #region 생성자

        /// <summary>
        /// 생성자
        /// </summary>
        public ReliaVerifiResultRegular()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeGrdReliaVerifiResultRegular(); // 신뢰성 의뢰(정기) 그리드 초기화
            InitializeGrdReReliaVerifiResultRegular(); // 신뢰성 재의뢰(정기) 그리드 초기화
            InitializeEvent();            
        }

        /// <summary>        
        /// 그리드 초기화(신뢰서 의뢰 접수(정기))
        /// </summary>
        private void InitializeGrdReliaVerifiResultRegular()
        {
            grdReliaVerifiResultRegular.GridButtonItem -= GridButtonItem.Delete; // 삭제 버튼 비활성화
            grdReliaVerifiResultRegular.GridButtonItem -= GridButtonItem.Add; // 추가 버튼 비활성화
            grdReliaVerifiResultRegular.GridButtonItem -= GridButtonItem.Copy; // 복사 버튼 비활성화
            grdReliaVerifiResultRegular.GridButtonItem -= GridButtonItem.Import; // Import 버튼 비활성화

            grdReliaVerifiResultRegular.View.SetSortOrder("SAMPLERECEIVEDATE");
            grdReliaVerifiResultRegular.View.SetSortOrder("LOTID");

            grdReliaVerifiResultRegular.View
                .AddTextBoxColumn("SAMPLERECEIVEDATE", 180)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center); // 시료접수일시

            grdReliaVerifiResultRegular.View
                .AddTextBoxColumn("VERIFICOMPDATE", 180)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center); // 검증완료일시

            grdReliaVerifiResultRegular.View
                .AddTextBoxColumn("TRANSITIONDATE", 180)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right); // 경과일

            grdReliaVerifiResultRegular.View
                .AddTextBoxColumn("REQUESTDEPT", 210)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 의뢰부서(공정)

            grdReliaVerifiResultRegular.View
                .AddTextBoxColumn("PRODUCTDEFID", 150)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 품목코드

            grdReliaVerifiResultRegular.View
                .AddTextBoxColumn("PRODUCTDEFNAME", 210)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 품목명

            grdReliaVerifiResultRegular.View
                .AddTextBoxColumn("PRODUCTDEFVERSION", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center); // 품목Version

            grdReliaVerifiResultRegular.View
                .AddTextBoxColumn("LOTID", 170)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center); // LOT NO

            //var manufatureInfo = grdReliaVerifiResultRegular.View.AddGroupColumn("MANUFACTURINFO");

            grdReliaVerifiResultRegular.View
                .AddTextBoxColumn("PROCESSSEGMENTNAME", 210)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 표준공정

            grdReliaVerifiResultRegular.View
                .AddTextBoxColumn("AREANAME", 210)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 작업장

            grdReliaVerifiResultRegular.View
                .AddTextBoxColumn("EQUIPMENTNAME", 210)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 설비(호기)

            grdReliaVerifiResultRegular.View
                .AddTextBoxColumn("TRACKOUTTIME", 180)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center); // 작업종료시간

            grdReliaVerifiResultRegular.View
                .AddTextBoxColumn("INSPECTIONRESULT", 150)
                .SetDefault(false)
                .SetTextAlignment(TextAlignment.Center); // 판정결과

            grdReliaVerifiResultRegular.View
                .AddTextBoxColumn("ISNCRPUBLISH", 150)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center); // NCR발행여부

            grdReliaVerifiResultRegular.View
                .AddTextBoxColumn("DEFECTCODENAME", 150)
                .SetLabel("DEFECTNAME")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 불량명

            grdReliaVerifiResultRegular.View.AddTextBoxColumn("QCSEGMENTNAME", 150);

            grdReliaVerifiResultRegular.View
                .AddTextBoxColumn("ISCOMPLETION", 150)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center); // 완료여부

            grdReliaVerifiResultRegular.View
                .AddTextBoxColumn("ISREREQUEST", 150)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center); // 재의뢰여부

            grdReliaVerifiResultRegular.View
                .AddTextBoxColumn("MEASUREITEMNAME", 150)
                .SetLabel("VERIFICATIONITEM")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center); // 검증항목

            grdReliaVerifiResultRegular.View
                .AddTextBoxColumn("MEASUREVALUE1", 150)
                .SetLabel("SAMPLE1")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right); // 시료1

            grdReliaVerifiResultRegular.View
                .AddTextBoxColumn("MEASUREVALUE2", 150)
                .SetLabel("SAMPLE2")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right); // 시료2

            grdReliaVerifiResultRegular.View
                .AddTextBoxColumn("MEASUREVALUE3", 150)
                .SetLabel("SAMPLE3")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right); // 시료3

            grdReliaVerifiResultRegular.View
                .AddTextBoxColumn("MEASUREVALUE4", 150)
                .SetLabel("SAMPLE4")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right); // 시료4

            grdReliaVerifiResultRegular.View
                .AddTextBoxColumn("MEASUREVALUE5", 150)
                .SetLabel("SAMPLE5")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right); // 시료5

            grdReliaVerifiResultRegular.View
                .AddTextBoxColumn("MEASUREVALUE6", 150)
                .SetLabel("SAMPLE6")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right); // 시료6

            grdReliaVerifiResultRegular.View
                .AddTextBoxColumn("MEASUREVALUE7", 150)
                .SetLabel("SAMPLE7")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right); // 시료7

            grdReliaVerifiResultRegular.View
                .AddTextBoxColumn("MEASUREVALUE8", 150)
                .SetLabel("SAMPLE8")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right); // 시료8

            grdReliaVerifiResultRegular.View
                .AddTextBoxColumn("MEASUREVALUE9", 150)
                .SetLabel("SAMPLE9")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right); // 시료9

            grdReliaVerifiResultRegular.View
                .AddTextBoxColumn("MEASUREVALUE10", 150)
                .SetLabel("SAMPLE10")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right); // 시료10

            grdReliaVerifiResultRegular.View.PopulateColumns();

            grdReliaVerifiResultRegular.View.OptionsView.AllowCellMerge = true; // CellMerge
        }

        /// <summary>        
        /// 그리드 초기화(신뢰서 의뢰 재접수(정기))
        /// </summary>
        private void InitializeGrdReReliaVerifiResultRegular()
        {
            grdReReliaVerifiResultRegular.GridButtonItem -= GridButtonItem.Delete; // 삭제 버튼 비활성화
            grdReReliaVerifiResultRegular.GridButtonItem -= GridButtonItem.Add; // 추가 버튼 비활성화
            grdReReliaVerifiResultRegular.GridButtonItem -= GridButtonItem.Copy; // 복사 버튼 비활성화
            grdReReliaVerifiResultRegular.GridButtonItem -= GridButtonItem.Import; // Import 버튼 비활성화

            grdReReliaVerifiResultRegular.View.SetSortOrder("SAMPLERECEIVEDATE");
            grdReReliaVerifiResultRegular.View.SetSortOrder("LOTID");

            grdReReliaVerifiResultRegular.View
                .AddTextBoxColumn("SAMPLERECEIVEDATE", 180)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center); // 시료접수일시

            grdReReliaVerifiResultRegular.View
                .AddTextBoxColumn("VERIFICOMPDATE", 180)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center); // 검증완료일시

            grdReReliaVerifiResultRegular.View
                .AddTextBoxColumn("TRANSITIONDATE", 180)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right); // 경과일

            grdReReliaVerifiResultRegular.View
                .AddTextBoxColumn("REQUESTDEPT", 210)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 의뢰부서(공정)

            grdReReliaVerifiResultRegular.View
                .AddTextBoxColumn("PRODUCTDEFID", 150)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 품목코드

            grdReReliaVerifiResultRegular.View
                .AddTextBoxColumn("PRODUCTDEFNAME", 210)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 품목명

            grdReReliaVerifiResultRegular.View
                .AddTextBoxColumn("PRODUCTDEFVERSION", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center); // 품목Version

            grdReReliaVerifiResultRegular.View
                .AddTextBoxColumn("LOTID", 170)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center); // LOT NO

            //var manufatureInfo = grdReliaVerifiResultRegular.View.AddGroupColumn("MANUFACTURINFO");

            grdReReliaVerifiResultRegular.View
                .AddTextBoxColumn("PROCESSSEGMENTNAME", 210)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 표준공정

            grdReReliaVerifiResultRegular.View
                .AddTextBoxColumn("AREANAME", 210)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 작업장

            grdReReliaVerifiResultRegular.View
                .AddTextBoxColumn("EQUIPMENTNAME", 210)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 설비(호기)

            grdReReliaVerifiResultRegular.View
                .AddTextBoxColumn("TRACKOUTTIME", 180)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center); // 작업종료시간

            grdReReliaVerifiResultRegular.View
                .AddTextBoxColumn("INSPECTIONRESULT", 150)
                .SetDefault(false)
                .SetTextAlignment(TextAlignment.Center); // 판정결과

            grdReReliaVerifiResultRegular.View
                .AddTextBoxColumn("ISNCRPUBLISH", 150)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center); // NCR발행여부

            grdReReliaVerifiResultRegular.View
                .AddTextBoxColumn("DEFECTCODENAME", 150)
                .SetLabel("DEFECTNAME")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 불량명

            grdReReliaVerifiResultRegular.View.AddTextBoxColumn("QCSEGMENTNAME", 150);

            grdReReliaVerifiResultRegular.View
                .AddTextBoxColumn("ISCOMPLETION", 150)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center); // 완료여부

            grdReReliaVerifiResultRegular.View
                .AddTextBoxColumn("ISREREQUEST", 150)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center); // 재의뢰여부

            grdReReliaVerifiResultRegular.View
                .AddTextBoxColumn("MEASUREITEMNAME", 150)
                .SetLabel("VERIFICATIONITEM")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center); // 검증항목

            grdReReliaVerifiResultRegular.View
                .AddTextBoxColumn("MEASUREVALUE1", 150)
                .SetLabel("SAMPLE1")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right); // 시료1

            grdReReliaVerifiResultRegular.View
                .AddTextBoxColumn("MEASUREVALUE2", 150)
                .SetLabel("SAMPLE2")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right); // 시료2

            grdReReliaVerifiResultRegular.View
                .AddTextBoxColumn("MEASUREVALUE3", 150)
                .SetLabel("SAMPLE3")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right); // 시료3

            grdReReliaVerifiResultRegular.View
                .AddTextBoxColumn("MEASUREVALUE4", 150)
                .SetLabel("SAMPLE4")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right); // 시료4

            grdReReliaVerifiResultRegular.View
                .AddTextBoxColumn("MEASUREVALUE5", 150)
                .SetLabel("SAMPLE5")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right); // 시료5

            grdReReliaVerifiResultRegular.View
                .AddTextBoxColumn("MEASUREVALUE6", 150)
                .SetLabel("SAMPLE6")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right); // 시료6

            grdReReliaVerifiResultRegular.View
                .AddTextBoxColumn("MEASUREVALUE7", 150)
                .SetLabel("SAMPLE7")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right); // 시료7

            grdReReliaVerifiResultRegular.View
                .AddTextBoxColumn("MEASUREVALUE8", 150)
                .SetLabel("SAMPLE8")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right); // 시료8

            grdReReliaVerifiResultRegular.View
                .AddTextBoxColumn("MEASUREVALUE9", 150)
                .SetLabel("SAMPLE9")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right); // 시료9

            grdReReliaVerifiResultRegular.View
                .AddTextBoxColumn("MEASUREVALUE10", 150)
                .SetLabel("SAMPLE10")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right); // 시료10

            grdReReliaVerifiResultRegular.View.PopulateColumns();

            grdReReliaVerifiResultRegular.View.OptionsView.AllowCellMerge = true; // CellMerge
        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            tabReliaVerifiResultRegular.SelectedPageChanged += TabReliabilityVerificationRequestRegular_SelectedPageChanged;
            grdReliaVerifiResultRegular.View.DoubleClick += GrdReliaVerifiResultRegularView_DoubleClick;
            grdReReliaVerifiResultRegular.View.DoubleClick += grdReReliaVerifiResultRegularView_DoubleClick;

            this.Conditions.GetControl<SmartComboBox>("P_ISJUDGMENTRESULT").EditValueChanged += ReliaVerifiResultRegular_EditValueChanged;

            grdReliaVerifiResultRegular.View.CellMerge += View_CellMerge;
            grdReReliaVerifiResultRegular.View.CellMerge += View_CellMerge1;
        }

        // 재의뢰결과 Cell Merge
        private void View_CellMerge1(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            GridView view = sender as GridView;

            if (view == null) return;

            if (e.Column.FieldName == "SAMPLERECEIVEDATE" || e.Column.FieldName == "VERIFICOMPDATE"
                || e.Column.FieldName == "TRANSITIONDATE" || e.Column.FieldName == "REQUESTDEPT"
                || e.Column.FieldName == "PRODUCTDEFID" || e.Column.FieldName == "PRODUCTDEFNAME"
                || e.Column.FieldName == "PRODUCTDEFVERSION" || e.Column.FieldName == "LOTID"
                || e.Column.FieldName == "PROCESSSEGMENTNAME" || e.Column.FieldName == "AREANAME"
                || e.Column.FieldName == "EQUIPMENTNAME" || e.Column.FieldName == "TRACKOUTTIME"
                || e.Column.FieldName == "INSPECTIONRESULT" || e.Column.FieldName == "ISNCRPUBLISH"
                || e.Column.FieldName == "DEFECTCODENAME" || e.Column.FieldName == "ISCOMPLETION"
                || e.Column.FieldName == "ISREREQUEST" || e.Column.FieldName == "QCSEGMENTNAME")
            {
                var dr1 = grdReReliaVerifiResultRegular.View.GetDataRow(e.RowHandle1); //위에 행 정보
                var dr2 = grdReReliaVerifiResultRegular.View.GetDataRow(e.RowHandle2); //아래 행 정보

                //비교하는 이유 그래야 정상적으로 나옴.
                e.Merge = dr1["SAMPLERECEIVEDATE"].ToString().Equals(dr2["SAMPLERECEIVEDATE"].ToString()) && dr1["VERIFICOMPDATE"].ToString().Equals(dr2["VERIFICOMPDATE"].ToString())
                             && dr1["TRANSITIONDATE"].ToString().Equals(dr2["TRANSITIONDATE"].ToString()) && dr1["REQUESTDEPT"].ToString().Equals(dr2["REQUESTDEPT"].ToString())
                             && dr1["PRODUCTDEFID"].ToString().Equals(dr2["PRODUCTDEFID"].ToString()) && dr1["PRODUCTDEFNAME"].ToString().Equals(dr2["PRODUCTDEFNAME"].ToString())
                             && dr1["PRODUCTDEFVERSION"].ToString().Equals(dr2["PRODUCTDEFVERSION"].ToString()) && dr1["LOTID"].ToString().Equals(dr2["LOTID"].ToString())
                             && dr1["PROCESSSEGMENTNAME"].ToString().Equals(dr2["PROCESSSEGMENTNAME"].ToString()) && dr1["AREANAME"].ToString().Equals(dr2["AREANAME"].ToString())
                             && dr1["EQUIPMENTNAME"].ToString().Equals(dr2["EQUIPMENTNAME"].ToString()) && dr1["TRACKOUTTIME"].ToString().Equals(dr2["TRACKOUTTIME"].ToString())
                             && dr1["INSPECTIONRESULT"].ToString().Equals(dr2["INSPECTIONRESULT"].ToString()) && dr1["ISNCRPUBLISH"].ToString().Equals(dr2["ISNCRPUBLISH"].ToString())
                             && dr1["DEFECTCODENAME"].ToString().Equals(dr2["DEFECTCODENAME"].ToString()) && dr1["ISCOMPLETION"].ToString().Equals(dr2["ISCOMPLETION"].ToString())
                             && dr1["ISREREQUEST"].ToString().Equals(dr2["ISREREQUEST"].ToString()) && dr1["QCSEGMENTNAME"].ToString().Equals(dr2["QCSEGMENTNAME"].ToString());
            }
            else
            {
                e.Merge = false;
            }

            e.Handled = true;
        }

        // 의뢰결과 Cell Merge
        private void View_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            GridView view = sender as GridView;

            if (view == null) return;

            if (e.Column.FieldName == "SAMPLERECEIVEDATE" || e.Column.FieldName == "VERIFICOMPDATE"
                || e.Column.FieldName == "TRANSITIONDATE" || e.Column.FieldName == "REQUESTDEPT"
                || e.Column.FieldName == "PRODUCTDEFID" || e.Column.FieldName == "PRODUCTDEFNAME"
                || e.Column.FieldName == "PRODUCTDEFVERSION" || e.Column.FieldName == "LOTID"
                || e.Column.FieldName == "PROCESSSEGMENTNAME" || e.Column.FieldName == "AREANAME"
                || e.Column.FieldName == "EQUIPMENTNAME" || e.Column.FieldName == "TRACKOUTTIME"
                || e.Column.FieldName == "INSPECTIONRESULT" || e.Column.FieldName == "ISNCRPUBLISH"
                || e.Column.FieldName == "DEFECTCODENAME" || e.Column.FieldName == "ISCOMPLETION"
                || e.Column.FieldName == "ISREREQUEST" || e.Column.FieldName == "QCSEGMENTNAME")
            {
                var dr1 = grdReliaVerifiResultRegular.View.GetDataRow(e.RowHandle1); //위에 행 정보
                var dr2 = grdReliaVerifiResultRegular.View.GetDataRow(e.RowHandle2); //아래 행 정보

                //비교하는 이유 그래야 정상적으로 나옴.
                e.Merge = dr1["SAMPLERECEIVEDATE"].ToString().Equals(dr2["SAMPLERECEIVEDATE"].ToString()) && dr1["VERIFICOMPDATE"].ToString().Equals(dr2["VERIFICOMPDATE"].ToString())
                             && dr1["TRANSITIONDATE"].ToString().Equals(dr2["TRANSITIONDATE"].ToString()) && dr1["REQUESTDEPT"].ToString().Equals(dr2["REQUESTDEPT"].ToString())
                             && dr1["PRODUCTDEFID"].ToString().Equals(dr2["PRODUCTDEFID"].ToString()) && dr1["PRODUCTDEFNAME"].ToString().Equals(dr2["PRODUCTDEFNAME"].ToString())
                             && dr1["PRODUCTDEFVERSION"].ToString().Equals(dr2["PRODUCTDEFVERSION"].ToString()) && dr1["LOTID"].ToString().Equals(dr2["LOTID"].ToString())
                             && dr1["PROCESSSEGMENTNAME"].ToString().Equals(dr2["PROCESSSEGMENTNAME"].ToString()) && dr1["AREANAME"].ToString().Equals(dr2["AREANAME"].ToString())
                             && dr1["EQUIPMENTNAME"].ToString().Equals(dr2["EQUIPMENTNAME"].ToString()) && dr1["TRACKOUTTIME"].ToString().Equals(dr2["TRACKOUTTIME"].ToString())
                             && dr1["INSPECTIONRESULT"].ToString().Equals(dr2["INSPECTIONRESULT"].ToString()) && dr1["ISNCRPUBLISH"].ToString().Equals(dr2["ISNCRPUBLISH"].ToString())
                             && dr1["DEFECTCODENAME"].ToString().Equals(dr2["DEFECTCODENAME"].ToString()) && dr1["ISCOMPLETION"].ToString().Equals(dr2["ISCOMPLETION"].ToString())
                             && dr1["ISREREQUEST"].ToString().Equals(dr2["ISREREQUEST"].ToString()) && dr1["QCSEGMENTNAME"].ToString().Equals(dr2["QCSEGMENTNAME"].ToString());
            }
            else
            {
                e.Merge = false;
            }

            e.Handled = true;
        }

        /// <summary>        
        /// 판정결과 변경시
        /// </summary>
        private void ReliaVerifiResultRegular_EditValueChanged(object sender, EventArgs e)
        {
            if (this.Conditions.GetControl<SmartComboBox>("P_ISJUDGMENTRESULT").EditValue.ToString() == "OK")
            {
                this.Conditions.GetControl<SmartComboBox>("P_ISNCRISSUESTATUS").Enabled = false;

            } else
            {
                this.Conditions.GetControl<SmartComboBox>("P_ISNCRISSUESTATUS").Enabled = true;
            }
        }

        /// <summary>        
        /// 재의뢰 목록 더블클릭 시
        /// </summary>
        private void grdReReliaVerifiResultRegularView_DoubleClick(object sender, EventArgs e)
        {
            DialogManager.ShowWaitArea(pnlContent);
            DataRow row = grdReReliaVerifiResultRegular.View.GetFocusedDataRow();

            ReReliaVerifiResultRegularPopup popup = new ReReliaVerifiResultRegularPopup(row);
            popup.ParentForm = this;

            //버튼의 enable
            bool isEnable = btnPopupFlag.Enabled;
            popup.isEnable = isEnable;

            popup.ShowDialog();

            DialogManager.CloseWaitArea(pnlContent);
        }

        /// <summary>        
        /// 의뢰 목록 더블클릭 시
        /// </summary>
        private void GrdReliaVerifiResultRegularView_DoubleClick(object sender, EventArgs e)
        {
            DialogManager.ShowWaitArea(pnlContent);

            ReliaVerifiResultRegularPopup popup = new ReliaVerifiResultRegularPopup(grdReliaVerifiResultRegular.View.GetFocusedDataRow());
            popup.ParentForm = this;

            //버튼의 enable
            bool isEnable = btnPopupFlag.Enabled;
            popup.isEnable = isEnable;

            popup.ShowDialog();

            DialogManager.CloseWaitArea(pnlContent);
        }

        /// <summary>        
        /// 탭이 변경 되었을때 호출
        /// </summary>
        private void TabReliabilityVerificationRequestRegular_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            SmartTabControl tab = sender as SmartTabControl;

            if (tab.SelectedTabPageIndex == 0) // 의로서 접수 조회
            {
                SearchReliaVerifiResultRegular();
            }
            else if (tab.SelectedTabPageIndex == 1) // 의뢰서 재접수 조회
            {
                SearchReReliaVerifiResultRegular();
            }
        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            // TODO : 저장 Rule 변경
            //DataTable changed = grdCodeClass.GetChangedRows();

            //ExecuteRule("SaveCodeClass", changed);
        }

        #endregion

        #region 검색

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            var values = Conditions.GetValues();
            DataTable dt = null;

            if (tabReliaVerifiResultRegular.SelectedTabPageIndex == 0) // 신뢰성 의뢰
            {
                values.Add("P_ISSECOND", "R"); // 의뢰
                values.Add("P_LANGUAGETYPE", UserInfo.Current.LanguageType);
                dt = await SqlExecuter.QueryAsync("GetReliaVerifiResultRegularlist", "10002", values);

                if (dt.Rows.Count < 1)
                {
                    ShowMessage("NoSelectData");
                }

                grdReliaVerifiResultRegular.DataSource = dt;
            }
            else if (tabReliaVerifiResultRegular.SelectedTabPageIndex == 1) // 신뢰성 재의뢰
            {
                values.Add("P_ISSECOND", "S"); // 재의뢰
                values.Add("P_LANGUAGETYPE", UserInfo.Current.LanguageType);
                dt = await SqlExecuter.QueryAsync("GetReliaVerifiResultRegularlist", "10002", values);

                if (dt.Rows.Count < 1)
                {
                    ShowMessage("NoSelectData");
                }

                grdReReliaVerifiResultRegular.DataSource = dt;
            }
        }

        #endregion

        #region 조회조건 설정

        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            InitializeConditionAreaId_Popup();
            InitializeConditionProcessSegmentId_Popup();
            InitializeConditionProductdefId_Popup();

            this.Conditions.AddComboBox("p_processsegmentclassId", new SqlQuery("GetLargeProcesssegmentListByQcm", "10001", "CODECLASSID=ChemicalAnalyRound", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PROCESSSEGMENTCLASSNAME", "PROCESSSEGMENTCLASSID")
                //.SetRelationIds("p_plantId")
                //.SetRelationIds("p_chemicalWaterType")
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                .SetEmptyItem()
                .SetDefault("25")
                .SetLabel("LARGEPROCESSSEGMENT")
                .SetPosition(3.4); // 대공정]

            InitializeConditionEquipment_Popup();
            InitializeConditionPopup_InspItem();
        }

        /// <summary>
        /// 팝업형 조회조건 생성 - 작업장
        /// </summary>
        private void InitializeConditionAreaId_Popup()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            //param.Add("AreaType", "Area");
            //param.Add("PlantId", UserInfo.Current.Plant);
            param.Add("LanguageType", UserInfo.Current.LanguageType);
            param.Add("P_USERID", UserInfo.Current.Id);
            //param.Add("AREA", UserInfo.Current.Area);

            //SqlQuery areaList = new SqlQuery("GetAreaidListByCsm", "10001", $"P_PLANTID={UserInfo.Current.Plant}", $"P_AREANAME={UserInfo.Current.Area}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}");
            //DataTable areaTable = areaList.Execute();

            var areaIdPopup = this.Conditions.AddSelectPopup("p_areaId", new SqlQuery("GetAuthorityUserUseArea", "10001", param), "AREANAME", "AREAID")
                .SetPopupLayout(Language.Get("SELECTAREAID"), PopupButtonStyles.Ok_Cancel, true, true)
                .SetPopupResultCount(1)
                .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                .SetPopupAutoFillColumns("AREANAME")
                .SetLabel("AREA")
                //.SetDefault(areaTable.Rows[0]["AREANAME"], UserInfo.Current.Area)
                .SetPosition(3.1);

            areaIdPopup.Conditions.AddTextBox("AREAIDNAME")
                .SetLabel("AREAIDNAME");

            areaIdPopup.GridColumns.AddTextBoxColumn("AREAID", 150);
            areaIdPopup.GridColumns.AddTextBoxColumn("AREANAME", 200);
        }

        /// <summary>
        /// 팝업형 조회조건 생성 - 공정
        /// </summary>
        private void InitializeConditionProcessSegmentId_Popup()
        {
            var processSegmentIdPopup = this.Conditions.AddSelectPopup("P_ProcessSegmentId", new SqlQuery("GetProcessSegmentList", "10002", $"PLANTID={UserInfo.Current.Plant}", "PROCESSSEGMENTCLASSTYPE=MiddleProcessSegmentClass", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PROCESSSEGMENTNAME", "PROCESSSEGMENTID")
                .SetPopupLayout(Language.Get("SELECTPROCESSSEGMENTID"), PopupButtonStyles.Ok_Cancel, true, true)
                .SetPopupResultCount(1)
                .SetPopupLayoutForm(800, 800, FormBorderStyle.FixedToolWindow)
                .SetPopupAutoFillColumns("PROCESSSEGMENTNAME")
                .SetLabel("PROCESSSEGMENT")
                .SetPosition(3.2);

            processSegmentIdPopup.Conditions.AddComboBox("PROCESSSEGMENTCLASSID_MIDDLE", new SqlQuery("GetProcessSegmentClassByType", "10001", $"PLANTID={UserInfo.Current.Plant}", "PROCESSSEGMENTCLASSTYPE=MiddleProcessSegmentClass", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PROCESSSEGMENTCLASSNAME", "PROCESSSEGMENTCLASSID")
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
        /// 팝업형 조회조건 생성 - 품목
        /// </summary>
        private void InitializeConditionProductdefId_Popup()
        {
            // 팝업 컬럼설정
            var productPopup = Conditions.AddSelectPopup("p_productdefId", new SqlQuery("GetProductListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PRODUCTDEFNAME", "PRODUCTDEFIDVERSION")
               .SetPopupLayout("PRODUCT", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupLayoutForm(600, 800)
               .SetLabel("PRODUCT")
               .SetPopupAutoFillColumns("PRODUCTDEFNAME")
               .SetPopupResultCount(1)
               .SetPosition(3.3);

            // 팝업 조회조건
            productPopup.Conditions.AddTextBox("PRODUCTDEFIDNAME");

            // 팝업 그리드
            productPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 120)
                .SetValidationKeyColumn();
            productPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 220);
            productPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 80);
            productPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFIDVERSION", 100)
                .SetIsHidden();
        }

        /// <summary>
        /// 팝업형 조회조건 생성 - 설비
        /// </summary>
        private void InitializeConditionEquipment_Popup()
        {
            // 팝업 컬럼설정
            var equipmentPopupColumn = Conditions.AddSelectPopup("p_equipmentId", new SqlQuery("GetEquipmentListByChemicalAnalysis", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "EQUIPMENTNAME", "EQUIPMENTID")
               .SetPopupLayout("EQUIPMENT", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupLayoutForm(400, 600)
               .SetPopupAutoFillColumns("EQUIPMENTNAME")
               .SetLabel("EQUIPMENT")
               .SetPopupResultCount(1)
               .SetPosition(3.5)
               .SetRelationIds("p_plantId", "p_processsegmentclassId");

            // 팝업 조회조건
            equipmentPopupColumn.Conditions.AddTextBox("EQUIPMENTIDNAME")
                .SetLabel("EQUIPMENTIDNAME");

            // 팝업 그리드
            equipmentPopupColumn.GridColumns.AddTextBoxColumn("EQUIPMENTID", 150)
                .SetValidationKeyColumn();
            equipmentPopupColumn.GridColumns.AddTextBoxColumn("EQUIPMENTNAME", 200);
        }

        /// <summary>
        /// 검증항목 조회조건
        /// </summary>
        private void InitializeConditionPopup_InspItem()
        {
            // 팝업 컬럼설정
            var areaPopup = Conditions.AddSelectPopup("P_INSPITEMID", new SqlQuery("GetQCRealibilityInspitem", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "INSPITEMNAME", "INSPITEMID")
               .SetPopupLayout("INSPITEM", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupLayoutForm(500, 600)
               .SetLabel("VERIFICATIONITEM")
               .SetPopupResultCount(1)
               .SetPosition(4.1);

            // 팝업 조회조건
            areaPopup.Conditions.AddTextBox("INSPITEM").SetLabel("VERIFICATIONITEM");

            // 팝업 그리드
            areaPopup.GridColumns.AddTextBoxColumn("INSPITEMID", 150)
                .SetLabel("VERIFICATIONITEMID")
                .SetValidationKeyColumn();
            areaPopup.GridColumns.AddTextBoxColumn("INSPITEMNAME", 200).SetLabel("VERIFICATIONITEM");
        }

        #endregion

        #region Private Function

        /// <summary>
        /// 의뢰 조회
        /// </summary>
        private void SearchReliaVerifiResultRegular()
        {
            try
            {
                this.ShowWaitArea();

                var values = Conditions.GetValues();
                values.Add("P_ISSECOND", "R"); // 의뢰
                values.Add("P_LANGUAGETYPE", UserInfo.Current.LanguageType);
                DataTable dt = SqlExecuter.Query("GetReliaVerifiResultRegularlist", "10002", values);

                if (dt.Rows.Count < 1)
                {
                    ShowMessage("NoSelectData");
                }

                grdReliaVerifiResultRegular.DataSource = dt;
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

        /// <summary>
        /// 재의뢰 조회
        /// </summary>
        private void SearchReReliaVerifiResultRegular()
        {
            try
            {
                this.ShowWaitArea();
                var values = Conditions.GetValues();
                values.Add("P_ISSECOND", "S"); // 재의뢰
                values.Add("P_LANGUAGETYPE", UserInfo.Current.LanguageType);
                DataTable dt = SqlExecuter.Query("GetReliaVerifiResultRegularlist", "10002", values);

                if (dt.Rows.Count < 1)
                {
                    ShowMessage("NoSelectData");
                }

                grdReReliaVerifiResultRegular.DataSource = dt;
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

        #endregion

        #region Global Function

        /// <summary>
        /// Popup 닫혔을때 재검색하기 위한 함수
        /// </summary>
        public void Search()
        {
            OnSearchAsync();
        }

        #endregion
    }
}
