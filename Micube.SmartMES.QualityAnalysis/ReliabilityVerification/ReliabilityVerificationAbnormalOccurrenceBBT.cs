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
using DevExpress.LookAndFeel;
using System.Reflection;
using DevExpress.Utils.Menu;
using System.Linq;
using DevExpress.XtraGrid.Views.Grid;
using System.Drawing;
#endregion

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 신뢰성검증 > 신뢰성 검증 이상 발생(정기)
    /// 업  무  설  명  : 신뢰성 정기 이상발생 조회 하는 화면이다.
    /// 생    성    자  : 유석진
    /// 생    성    일  : 2019-09-23
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ReliabilityVerificationAbnormalOccurrenceBBT : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가
        DXMenuItem _myContextMenu1;
        #endregion

        #region 생성자

        /// <summary>
        /// 생성자
        /// </summary>
        public ReliabilityVerificationAbnormalOccurrenceBBT()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeGrdReliabiVerifiAbnormalOccurenceRegular(); // 신뢰성 검증 이상발생(정기) 그리드 초기화
            InitializeEvent();
        }

        /// <summary>        
        /// 그리드 초기화(신뢰서 검증 이상발생(정기))
        /// </summary>
        private void InitializeGrdReliabiVerifiAbnormalOccurenceRegular()
        {
            grdReliabiVerifiAbnormalOccurenceRegular.View.SetIsReadOnly();
            grdReliabiVerifiAbnormalOccurenceRegular.GridButtonItem = GridButtonItem.Export | GridButtonItem.Expand | GridButtonItem.Restore;
            grdReliabiVerifiAbnormalOccurenceRegular.View.SetSortOrder("INSPECTIONDATE");
            grdReliabiVerifiAbnormalOccurenceRegular.View.SetSortOrder("LOTID");

            //진행현황
            grdReliabiVerifiAbnormalOccurenceRegular.View.AddTextBoxColumn("STATE", 120)
                .SetLabel("PROCESSSTATUS");

            grdReliabiVerifiAbnormalOccurenceRegular.View.AddTextBoxColumn("INSPECTIONDATE", 150);

            grdReliabiVerifiAbnormalOccurenceRegular.View.AddTextBoxColumn("PRODUCTDEFID", 150).SetTextAlignment(TextAlignment.Center);

            grdReliabiVerifiAbnormalOccurenceRegular.View.AddTextBoxColumn("PRODUCTDEFNAME", 160);

            grdReliabiVerifiAbnormalOccurenceRegular.View.AddTextBoxColumn("PRODUCTDEFVERSION", 70).SetTextAlignment(TextAlignment.Center); // Rev

            grdReliabiVerifiAbnormalOccurenceRegular.View.AddTextBoxColumn("LAYER", 70).SetTextAlignment(TextAlignment.Center); // 층수

            grdReliabiVerifiAbnormalOccurenceRegular.View.AddTextBoxColumn("LOTID", 180).SetTextAlignment(TextAlignment.Center);

            grdReliabiVerifiAbnormalOccurenceRegular.View.AddTextBoxColumn("DEFECTNAME", 120);

            grdReliabiVerifiAbnormalOccurenceRegular.View.AddTextBoxColumn("QCSEGMENTNAME", 150);

            //마감여부
            grdReliabiVerifiAbnormalOccurenceRegular.View.AddTextBoxColumn("ISCLOSE", 70).SetTextAlignment(TextAlignment.Center);

            //원인
            grdReliabiVerifiAbnormalOccurenceRegular.View.AddTextBoxColumn("REASONCONSUMABLEDEFNAME", 180)
                .SetLabel("REASONPRODUCTDEFNAME");

            grdReliabiVerifiAbnormalOccurenceRegular.View.AddTextBoxColumn("REASONCONSUMABLEDEFVERSION", 80);

            grdReliabiVerifiAbnormalOccurenceRegular.View.AddTextBoxColumn("REASONCONSUMABLELOTID", 180)
                .SetLabel("CAUSEMATERIALLOT");

            grdReliabiVerifiAbnormalOccurenceRegular.View.AddTextBoxColumn("REASONSEGMENTNAME", 100);

            grdReliabiVerifiAbnormalOccurenceRegular.View.AddTextBoxColumn("REASONAREANAME", 100);

            //2020-01-12 강유라 car관련 요청, 접수예정일, 접수, 승인,유효성평가 날짜 조회 추가
            //car 관련 날짜
            grdReliabiVerifiAbnormalOccurenceRegular.View.AddTextBoxColumn("CARREQUESTDATE", 150)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetLabel("CARREQUESTDATE")
                .SetTextAlignment(TextAlignment.Center);

            grdReliabiVerifiAbnormalOccurenceRegular.View.AddTextBoxColumn("CAREXPECTEDRECEIPTDATE", 150)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetLabel("CAREXPECTEDRECEIPTDATE")
                .SetTextAlignment(TextAlignment.Center);

            grdReliabiVerifiAbnormalOccurenceRegular.View.AddTextBoxColumn("RECEIPTDATE", 150)
                 .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                 .SetLabel("CARRECEIPTDATE")
                 .SetTextAlignment(TextAlignment.Center);

            grdReliabiVerifiAbnormalOccurenceRegular.View.AddTextBoxColumn("APPROVALDATE", 150)
                 .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                 .SetLabel("CARAPPROVALDATE")
                 .SetTextAlignment(TextAlignment.Center);

            grdReliabiVerifiAbnormalOccurenceRegular.View.AddTextBoxColumn("CLOSEDATE", 150)
                 .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                 .SetLabel("CARCLOSEDATE")
                 .SetTextAlignment(TextAlignment.Center);

            grdReliabiVerifiAbnormalOccurenceRegular.View.PopulateColumns();
        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            grdReliabiVerifiAbnormalOccurenceRegular.View.DoubleClick += GrdReliabiVerifiAbnormalOccurenceRegular_DoubleClick;
            grdReliabiVerifiAbnormalOccurenceRegular.InitContextMenuEvent += grdReliabiVerifiAbnormalOccurenceRegular_InitContextMenuEvent;

            //2020-01-14 강유라 affectLot isLocking 에 따른 추가 
            grdReliabiVerifiAbnormalOccurenceRegular.View.RowCellStyle += View_RowCellStyle;
        }

        /// <summary>
        /// affectLot isLocking Y 시 Row 빨간색
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView grid = sender as GridView;
            if (e.RowHandle < 0) return;

            DataRow row = (DataRow)grid.GetDataRow(e.RowHandle);

            if (Format.GetString(row["AFFECTISLOCKING"]).Equals("Y"))
            {
                if (!grid.IsCellSelected(e.RowHandle, e.Column))
                    e.Appearance.ForeColor = Color.Red;
            }

        }

        /// <summary>
        /// 이상발생 현황 목록 더블 클릭
        /// </summary>
        private void GrdReliabiVerifiAbnormalOccurenceRegular_DoubleClick(object sender, EventArgs e)
        {
            DialogManager.ShowWaitArea(pnlContent);

            ReliaVerifiResultBBTAbnormalOccurrencePopup popup = new ReliaVerifiResultBBTAbnormalOccurrencePopup(grdReliabiVerifiAbnormalOccurenceRegular.View.GetFocusedDataRow());
            popup.SearchAutoAffectLot();
            popup.ParentControl = this;
            popup.Owner = this;
            popup.btnSave.Enabled = btnFlag.Enabled;
            popup.ShowDialog();

            DialogManager.CloseWaitArea(pnlContent);
        }
        private void grdReliabiVerifiAbnormalOccurenceRegular_InitContextMenuEvent(SmartBandedGrid sender, Framework.SmartControls.Grid.InitContextMenuEventArgs args)
        {
            //Affect LOT 산정(출) 화면전환 메뉴ID : PG-QC-0556, 프로그램ID : Micube.SmartMES.QualityAnalysis.AffectLot
            args.AddMenus.Add(_myContextMenu1 = new DXMenuItem(Language.Get("MENU_PG-QC-0556"), OpenForm) { BeginGroup = true, Tag = "PG-QC-0556" });
        }
        private void OpenForm(object sender, EventArgs args)
        {
            try
            {
                DialogManager.ShowWaitDialog();

                DataRow currentRow = grdReliabiVerifiAbnormalOccurenceRegular.View.GetFocusedDataRow();
                if (currentRow == null) return;

                string menuId = (sender as DXMenuItem).Tag.ToString();

                var param = currentRow.Table.Columns
                    .Cast<DataColumn>()
                    .ToDictionary(col => col.ColumnName, col => currentRow[col.ColumnName]);

                OpenMenu(menuId, param); //다른창 호출..
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
        #endregion

        #region 툴바

        #endregion

        #region 검색

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            var values = Conditions.GetValues();
            values.Add("P_LANGUAGETYPE", Language.LanguageType);

            DataTable dt = null;

            dt = await SqlExecuter.QueryAsync("ReliabilityVerificationAbnormalOccurrenceBBTList", "10001", values);

            if (dt.Rows.Count < 1)
            {
                ShowMessage("NoSelectData");
            }

            grdReliabiVerifiAbnormalOccurenceRegular.DataSource = dt;
        }

        #endregion

        #region 조회조건 설정

        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // 품목
            CommonFunction.AddConditionProductPopup("P_PRODUCTDEFID", 2.2, true, Conditions);
            CommonFunction.AddConditionLotHistPopup("P_LOTID", 2.3, false, Conditions);
            // 불량 코드
            InitializeConditionPopup_DefectCode();
        }

        /// <summary>
        /// 불량코드 조회조건
        /// </summary>
        private void InitializeConditionPopup_DefectCode()
        {
            //// 팝업 컬럼설정
            //var defectCodePopup = Conditions.AddSelectPopup("p_defectCode", new SqlQuery("GetDefectCodeList", "10002", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "DEFECTNAME", "DEFECTCODE")
            //   .SetPopupLayout("DEFECTCODE", PopupButtonStyles.Ok_Cancel, true, true)
            //   .SetPopupLayoutForm(400, 600)
            //   .SetLabel("DEFECTCODE")
            //   .SetPopupResultCount(1)
            //   .SetPosition(3.2)
            //   .SetRelationIds("p_plantId");

            //// 팝업 조회조건
            //defectCodePopup.Conditions.AddTextBox("DEFECTCODENAME");

            //// 팝업 그리드
            //defectCodePopup.GridColumns.AddTextBoxColumn("DEFECTCODE", 150)
            //    .SetValidationKeyColumn();
            //defectCodePopup.GridColumns.AddTextBoxColumn("DEFECTNAME", 200);

            // 팝업 컬럼설정
            var defectCodePopup = Conditions.AddSelectPopup("p_defectCode", new SqlQuery("GetDefectCodeList", "10004", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "DEFECTCODENAME", "DEFECTCODE")
               .SetPopupLayout("DEFECTCODE", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupLayoutForm(700, 500)
               .SetLabel("DEFECTNAME")
               .SetPopupResultCount(1)
               .SetPosition(4.3)
               .SetRelationIds("p_plantId");

            // 팝업 조회조건
            defectCodePopup.Conditions.AddTextBox("DEFECTCODENAME");

            // 팝업 그리드

            defectCodePopup.GridColumns.AddTextBoxColumn("DEFECTCODE", 150).SetValidationKeyColumn(); ;
            defectCodePopup.GridColumns.AddTextBoxColumn("DEFECTCODENAME", 150);
            defectCodePopup.GridColumns.AddTextBoxColumn("QCSEGMENTID", 150);
            defectCodePopup.GridColumns.AddTextBoxColumn("QCSEGMENTNAME", 150);
        }

        #endregion

        #region 유효성 검사

        #endregion

        #region Private Function

        // TODO : 화면에서 사용할 내부 함수 추가

        /// <summary>
        /// 의뢰서 출력 조회
        /// </summary>
        private void SearchReliabiVerifiReqPrintRegular()
        {
            try
            {
                this.ShowWaitArea();

                var values = Conditions.GetValues();
                values.Add("P_LANGUAGETYPE", Language.LanguageType);

                DataTable dt = SqlExecuter.Query("GetReliabilityVerificationRequestRegularRgisterList", "10001", values);

                if (dt.Rows.Count < 1)
                {
                    ShowMessage("NoSelectData");
                }

                grdReliabiVerifiAbnormalOccurenceRegular.DataSource = dt;
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
