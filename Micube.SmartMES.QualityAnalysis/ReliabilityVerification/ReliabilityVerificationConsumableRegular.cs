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
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System.ComponentModel;
using Micube.Framework.SmartControls.Grid.BandedGrid;

#endregion

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 신뢰성검증 > 신뢰성검증(자재) 접수현황
    /// 업  무  설  명  : 
    /// 생    성    자  : 유호석
    /// 생    성    일  : 2019-07-10
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ReliabilityVerificationConsumableRegular : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가

        #endregion

        #region 생성자

        /// <summary>
        /// 생성자
        /// </summary>
        public ReliabilityVerificationConsumableRegular()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeGridReliabilityVerificationRequestRgisterRegular(); // 신뢰성 의뢰접수(정기) 그리드 초기화
            InitializeEvent();
        }

        /// <summary>        
        /// 그리드 초기화(신뢰서 의뢰 접수(정기))
        /// </summary>
        private void InitializeGridReliabilityVerificationRequestRgisterRegular()
        {
            grdReliabiVerifiReqRgistRegular.GridButtonItem -= GridButtonItem.Delete; // 삭제 버튼 비활성화
            grdReliabiVerifiReqRgistRegular.GridButtonItem -= GridButtonItem.Add; // 추가 버튼 비활성화
            grdReliabiVerifiReqRgistRegular.GridButtonItem -= GridButtonItem.Copy; // 복사 버튼 비활성화
            grdReliabiVerifiReqRgistRegular.GridButtonItem -= GridButtonItem.Import; // Import 버튼 비활성화

            grdReliabiVerifiReqRgistRegular.View.ShowingEditor += View_ShowingEditor;

            grdReliabiVerifiReqRgistRegular.View.SetSortOrder("REQUESTDATE");
            grdReliabiVerifiReqRgistRegular.View.SetSortOrder("REQUESTNO");

            /*
            grdReliabiVerifiReqRgistRegular.View
                .AddCheckBoxColumn("RECEIVE", 100)
                .SetDefault(false)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("ISSAMPLERECEIVE"); // 시료접수여부
            */

            grdReliabiVerifiReqRgistRegular.View
                .AddTextBoxColumn("REQUESTNO", 140)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center); // 의뢰번호

            grdReliabiVerifiReqRgistRegular.View
                .AddTextBoxColumn("REQUESTDATE", 180)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center); // 의뢰일시

            grdReliabiVerifiReqRgistRegular.View
                .AddTextBoxColumn("SAMPLERECEIVEDATE", 180)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center); // 시료접수일시

            grdReliabiVerifiReqRgistRegular.View
                .AddTextBoxColumn("TRANSITIONDATE", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right); // 경과일

            //의뢰구분
            grdReliabiVerifiReqRgistRegular.View.AddComboBoxColumn("REQUESTTYPE", 150, new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "CODECLASSID=RequestClass"))
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();// 공통코드(YesNo)

            grdReliabiVerifiReqRgistRegular.View
                .AddTextBoxColumn("REQUESTDEPT", 160)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 의뢰부서(공정)

            grdReliabiVerifiReqRgistRegular.View
                .AddTextBoxColumn("USERNAME", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center); // 의뢰자

            grdReliabiVerifiReqRgistRegular.View
                .AddTextBoxColumn("PROCESSTYPE", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center); // 절차구분

            grdReliabiVerifiReqRgistRegular.View
                .AddTextBoxColumn("CHARGERNAME", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("OWNERNAME"); // 담당자명

            grdReliabiVerifiReqRgistRegular.View
                .AddTextBoxColumn("APPROVALSTATE", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center); // 결제상태

            grdReliabiVerifiReqRgistRegular.View
                .AddTextBoxColumn("ALL_CNT", 80)
                .SetIsHidden()
                .SetTextAlignment(TextAlignment.Center); // 모든 결제자 수

            grdReliabiVerifiReqRgistRegular.View
                .AddTextBoxColumn("APPROVAL_CNT", 80)
                .SetIsHidden()
                .SetTextAlignment(TextAlignment.Center); // 승인 결제 수

            grdReliabiVerifiReqRgistRegular.View.PopulateColumns();
        }
        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {

            btnRecieve1.Click += BtnRecieve1_Click;
            btnNew.Click += btnNew_Click;
            grdReliabiVerifiReqRgistRegular.View.DoubleClick += GrdReliabiVerifiReqRgistRegularView_DoubleClick;
        }
        /// <summary>
        /// 모든 결제라인이 승인이 되어야 시료접수를 할 수 있다
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_ShowingEditor(object sender, CancelEventArgs e)
        {
            SmartBandedGridView gridView = sender as SmartBandedGridView;
            DataRow row = grdReliabiVerifiReqRgistRegular.View.GetFocusedDataRow();
            GridView view = sender as GridView;
            if (!view.FocusedColumn.FieldName.Equals("RECEIVE") || row["ISSAMPLERECEIVE"].ToString() == "1")
            {
                e.Cancel = true;
                return;
            }
            string sALL_CNT = row["ALL_CNT"].ToString();             // 모든 결제자 수
            string sAPPROVAL_CNT = row["APPROVAL_CNT"].ToString();        // 승인된 결제 수
            int iALL_CNT = 0;
            int iAPPROVAL_CNT = 0;
            Int32.TryParse(sALL_CNT, out iALL_CNT);
            Int32.TryParse(sAPPROVAL_CNT, out iAPPROVAL_CNT);
            if (iALL_CNT <= 0 || (iALL_CNT - iAPPROVAL_CNT) != 0)
            {
                this.ShowMessage("ApprovalCountInfo");//결제승인이 완료되지 않았습니다.
                e.Cancel = true;
            }
        }
         /// <summary>        
        /// 의리서 출력 완료후 출력일시 반영 및 조회
        /// </summary>
        private void PrintingSystem_EndPrint1(object sender, EventArgs e)
        {
            Search();
        }
        /// <summary>
        /// 신규 의뢰
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            //ReliabilityVerificationConsumableRegularPopup popup = new ReliabilityVerificationConsumableRegularPopup(grdReliabiVerifiReqRgistRegular.View.GetFocusedDataRow());
            //popup.ShowDialog();

            var values = Conditions.GetValues();
            string sPlant = values["P_PLANTID"].ToString();

            ReliabilityVerificationConsumableRegularPopup popup = new ReliabilityVerificationConsumableRegularPopup("New", UserInfo.Current.Enterprise, UserInfo.Current.Plant ,"" ,"");
            popup.StartPosition = FormStartPosition.CenterParent;
            popup.ShowDialog();
            if (popup.DialogResult == DialogResult.OK)
            {
                Popup_FormClosed();
            }
        }
        /// <summary>        
        /// 의뢰접수/재접수 접수 처리
        /// </summary>
        private void BtnRecieve1_Click(object sender, EventArgs e)
        {
            DialogResult result = System.Windows.Forms.DialogResult.No;

            result = this.ShowMessage(MessageBoxButtons.YesNo, "RegisterNote");//접수 하시겠습니까?

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    this.ShowWaitArea();
                    btnRecieve1.Enabled = false;

                    SaveRecept(grdReliabiVerifiReqRgistRegular);

                    ShowMessage("SuccessSave");

                    Search(); // 조회
                }
                catch (Exception ex)
                {
                    this.ShowError(ex);
                }
                finally
                {
                    this.CloseWaitArea();
                    btnRecieve1.Enabled = true;
                }
            }
        }
        /// <summary>        
        /// 의뢰접수 목록 더블클릭 시
        /// </summary>
        private void GrdReliabiVerifiReqRgistRegularView_DoubleClick(object sender, EventArgs e)
        {
            if (grdReliabiVerifiReqRgistRegular.View.FocusedRowHandle < 0) return;

            DXMouseEventArgs args = e as DXMouseEventArgs;
            GridView view = sender as GridView;
            GridHitInfo info = view.CalcHitInfo(args.Location);
            if (info.InRowCell && (info.Column.FieldName == "RECEIVE"))//시료접수여부
                return;

            DataRow row = this.grdReliabiVerifiReqRgistRegular.View.GetFocusedDataRow();
            ReliabilityVerificationConsumableRegularPopup popup = new ReliabilityVerificationConsumableRegularPopup("Modify", row["ENTERPRISEID"].ToString(), row["PLANTID"].ToString(), row["REQUESTNO"].ToString(), row["ISSAMPLERECEIVE"].ToString());
            popup.Owner = this;
            popup.btnSave.Enabled = btnFlag.Enabled;
            popup.btnSaveResult.Enabled = btnFlag.Enabled;
            popup.ShowDialog();

            if (popup.DialogResult == DialogResult.OK)
            {
                Popup_FormClosed();
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
        /// <summary>	
        /// 툴바버튼 클릭	
        /// </summary>	
        /// <param name="sender"></param>	
        /// <param name="e"></param>	
        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            SmartButton btn = sender as SmartButton;

            if (btn.Name.ToString().Equals("Regist"))//등록
            {
                btnNew_Click(null, null);
            }
            else if (btn.Name.ToString().Equals("Save"))//저장
            {
                BtnRecieve1_Click(null, null);
            }
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

            string sISCOMPLETION = values["P_ISCOMPLETION"].ToString();
            if (sISCOMPLETION.Length > 0 && sISCOMPLETION == "Y")
                values["P_ISCOMPLETION"] = "0";
            else if (sISCOMPLETION.Length > 0 && sISCOMPLETION == "N")
                values["P_ISCOMPLETION"] = "1";

            DataTable dt = null;

            dt = await SqlExecuter.QueryAsync("GetReliabilityVerificationConsumableRegularRgisterList", "10001", values);

            if (dt.Rows.Count < 1)
            {
                ShowMessage("NoSelectData");
            }

            grdReliabiVerifiReqRgistRegular.DataSource = dt;
        }

        #endregion

        #region 조회조건 설정

        protected override void InitializeCondition()
        {
            base.InitializeCondition();

        }
        #endregion


        #region 유효성 검사

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            // TODO : 유효성 로직 변경
            //grdCodeClass.View.CheckValidation();

            //DataTable changed = grdCodeClass.GetChangedRows();

            //if (changed.Rows.Count == 0)
            //{
            //    // 저장할 데이터가 존재하지 않습니다.
            //    throw MessageException.Create("NoSaveData");
            //}
        }

        #endregion

        #region Private Function

        /// <summary>
        /// 의뢰서 접수/재접수 접수 저장
        /// </summary>
        private void SaveRecept(SmartBandedGrid grid)
        {
            int i = 0;

            //DataTable dt = grid.DataSource as DataTable;

            DataTable dt = grid.GetChangedRows();

            foreach (DataRow dr in dt.Rows)
            {
                if (dr["RECEIVE"].ToString() == "True")
                {
                    i++;
                }
            }

            if (i == 0)
            {
                throw MessageException.Create("NoSaveData");
            }

            // CT_RELIABILITYREFMANUFACTURING Table에 저장될 Data
            DataTable reliabilityVerificationTable = new DataTable();
            reliabilityVerificationTable.TableName = "list";
            DataRow row = null;

            reliabilityVerificationTable.Columns.Add(new DataColumn("REQUESTNO", typeof(string)));
            reliabilityVerificationTable.Columns.Add(new DataColumn("ENTERPRISEID", typeof(string)));
            reliabilityVerificationTable.Columns.Add(new DataColumn("PLANTID", typeof(string)));
            reliabilityVerificationTable.Columns.Add(new DataColumn("RECEIVE", typeof(string))); ;

            foreach (DataRow dr in dt.Rows)
            {
                if (dr["RECEIVE"].ToString() == "True")
                {
                    row = reliabilityVerificationTable.NewRow();
                    row["REQUESTNO"] = dr["REQUESTNO"];
                    row["ENTERPRISEID"] = dr["ENTERPRISEID"];
                    row["PLANTID"] = dr["PLANTID"];
                    row["RECEIVE"] = "1";

                    reliabilityVerificationTable.Rows.Add(row);
                }
            }

            DataSet rullSet = new DataSet();
            rullSet.Tables.Add(reliabilityVerificationTable);

            ExecuteRule("SaveReliabilityVerificationRequestRegularRecept", rullSet);
        }

        /// <summary>
        /// Report 파일의 컨트롤 중 Tag(FieldName) 값이 있는 컨트롤에 DataBinding(Text)를 추가한다.
        /// </summary>
        private void SetReportControlDataBinding(XRControlCollection controls, DataTable dataSource)
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
        //popup이 닫힐때 그리드를 재조회 하는 이벤트 (팝업결과 저장 후 재조회)
        private async void Popup_FormClosed()
        {
            await OnSearchAsync();
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
