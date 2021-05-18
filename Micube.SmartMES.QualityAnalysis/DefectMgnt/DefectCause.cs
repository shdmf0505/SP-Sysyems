#region using

using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
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

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 불량관리 > 불량품 원인판정
    /// 업  무  설  명  : 불량코드들에 대해 반출하거나 내역을 조정하고 원인을 확정할 수 있다.
    /// 생    성    자  : 유태근
    /// 생    성    일  : 2019-07-03
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class DefectCause : SmartConditionManualBaseForm
    {
        #region Local Variables

        /// <summary>
        /// 원인불명 전체선택 Flag
        /// </summary>
        bool _allCheckFlag = true;

        #endregion

        #region 생성자

        public DefectCause()
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

            InitializeDefectCasue();
            InitializeDefectConfirmation();
        }

        /// <summary>        
        /// 불량 원인판정 그리드
        /// </summary>
        private void InitializeDefectCasue()
        {
            grdDefectCasue.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            //grdDefectCasue.View.CheckMarkSelection.MultiSelectCount = 1;
            grdDefectCasue.GridButtonItem = GridButtonItem.Export;
            grdDefectCasue.View.SetIsReadOnly();

            var defectInfo = grdDefectCasue.View.AddGroupColumn("DEFECTINFO");

            //defectInfo.AddTextBoxColumn("ISIFSUCCESS", 120)
            //    .SetTextAlignment(TextAlignment.Center); // IF 성공여부
            defectInfo.AddTextBoxColumn("STATUSNAME", 80)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("STATUS"); // 진행상태명
            defectInfo.AddTextBoxColumn("RECEIVETIME", 200)
                .SetTextAlignment(TextAlignment.Center); // 인수일시
            defectInfo.AddTextBoxColumn("PROCESSDATE", 200)
                .SetTextAlignment(TextAlignment.Center); // 처리시간
            defectInfo.AddTextBoxColumn("PRODUCTDEFID", 120); // 품목 ID
            defectInfo.AddTextBoxColumn("PRODUCTDEFNAME", 260)
                .SetTextAlignment(TextAlignment.Center); // 품목명
            defectInfo.AddTextBoxColumn("PRODUCTDEFVERSION", 80)
                .SetTextAlignment(TextAlignment.Center); // 품목버전
            defectInfo.AddTextBoxColumn("PARENTLOTID", 200)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("Lot No"); // Parent Lot No
            defectInfo.AddSpinEditColumn("DEFECTQTY", 80)
                .SetLabel("PCS"); // 불량 PCS 갯수
            defectInfo.AddSpinEditColumn("DEFECTPNLQTY", 80)
                .SetLabel("PNL"); // 불량 PNL 갯수
            defectInfo.AddTextBoxColumn("USERSEQUENCE", 80)
                .SetTextAlignment(TextAlignment.Right); // 공정순서
            defectInfo.AddTextBoxColumn("PROCESSSEGMENTNAME", 180); // 공정명
            defectInfo.AddTextBoxColumn("LOTID", 200)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("DEFECTLOTID"); // 불량 Lot No
            defectInfo.AddTextBoxColumn("AREANAME", 180); // 작업장명
            defectInfo.AddTextBoxColumn("PLANTID", 80); // Site
            defectInfo.AddTextBoxColumn("RECEIVEUSER", 100)
                .SetTextAlignment(TextAlignment.Center); // 인수자명
            defectInfo.AddTextBoxColumn("LOTTYPE", 100)
                .SetTextAlignment(TextAlignment.Center); // 양산구분
            defectInfo.AddTextBoxColumn("UNIT", 80)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("UOM"); // 불량단위

            var hidden = grdDefectCasue.View.AddGroupColumn("HIDDEN");

            hidden.AddTextBoxColumn("ENTERPRISEID", 100)
                .SetIsHidden();
            hidden.AddTextBoxColumn("PROCESSSEGMENTID", 100)
                .SetIsHidden();
            hidden.AddTextBoxColumn("PROCESSSEGMENTVERSION", 100)
                .SetIsHidden();
            hidden.AddTextBoxColumn("AREAID", 100)
                .SetIsHidden();
            hidden.AddTextBoxColumn("STATUS", 100)
                .SetIsHidden();

            grdDefectCasue.View.PopulateColumns();

            grdDefectCasue.View.OptionsNavigation.AutoMoveRowFocus = false;
        }

        /// <summary>        
        /// 불량 원인확정 그리드
        /// </summary>
        private void InitializeDefectConfirmation()
        {
            grdDefectConfirmation.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdDefectConfirmation.GridButtonItem = GridButtonItem.Export;

            var defect = grdDefectConfirmation.View.AddGroupColumn("DEFECTINFO");

            defect.AddTextBoxColumn("STATUS", 120)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(); // 상태
            defect.AddTextBoxColumn("DEFECTCODE", 100)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(); // 불량코드
            defect.AddTextBoxColumn("DEFECTCODENAME", 200)
                .SetIsReadOnly(); // 불량명
            defect.AddTextBoxColumn("QCSEGMENTNAME", 150)
                .SetIsReadOnly(); // 품질공정명

            var reasonSegment = grdDefectConfirmation.View.AddGroupColumn("CAUSEPROCESS");

            reasonSegment.AddTextBoxColumn("REASONCONSUMABLEDEFNAME", 220)
                .SetLabel("REASONPRODUCT")
                .SetIsReadOnly(); // 원인품목
            reasonSegment.AddTextBoxColumn("REASONCONSUMABLEDEFVERSION", 80)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("Rev")
                .SetIsReadOnly(); // 원인품목 Version
            reasonSegment.AddTextBoxColumn("REASONCONSUMABLELOTID", 200)
                .SetIsReadOnly(); // 원인자재 Lot
            reasonSegment.AddTextBoxColumn("REASONSEGMENTNAME", 180)
                .SetLabel("REASONSEGMENT")
                .SetIsReadOnly(); // 원인공정
            reasonSegment.AddTextBoxColumn("REASONAREANAME", 180)
                .SetLabel("REASONAREA")
                .SetIsReadOnly(); // 원인작업장
            reasonSegment.AddTextBoxColumn("REASONPLANTID", 100)
                .SetLabel("REASONPLANT")
                .SetIsReadOnly(); // 원인 Site

            var defectCount = grdDefectConfirmation.View.AddGroupColumn("DEFECTCOUNT");

            defectCount.AddSpinEditColumn("DEFECTQTY", 80)
                .SetLabel("PCS")
                .SetIsReadOnly();  // 불량 PCS 갯수
            defectCount.AddSpinEditColumn("DEFECTPNLQTY", 80)
                .SetLabel("PNL")
                .SetIsReadOnly(); // 불량 PNL 갯수
            defectCount.AddTextBoxColumn("UNIT", 80)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("UOM")
                .SetIsReadOnly(); // UOM
            defectCount.AddCheckEditColumn("ISUNKNOWN", 100)
                .SetDefault(false); // 원인불명
            defectCount.AddTextBoxColumn("DEFINETIME", 200)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(); // 확정일시
            defectCount.AddTextBoxColumn("DEFINEUSER", 100)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(); // 확정자
            defectCount.AddTextBoxColumn("OUTBOUNDQTY", 80)
                .SetIsReadOnly(); // 반출수량
            defectCount.AddTextBoxColumn("ISCLAIM", 120)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(); // Claim 포함여부

            var hidden = grdDefectConfirmation.View.AddGroupColumn("HIDDEN");

            hidden.AddTextBoxColumn("ENTERPRISEID", 100)
                .SetIsHidden(); // 회사 ID
            hidden.AddTextBoxColumn("PLANTID", 100)
                .SetIsHidden(); // Site ID
            hidden.AddTextBoxColumn("AREAID", 100)
                .SetIsHidden(); // 작업장 ID
            hidden.AddTextBoxColumn("PROCESSSEGMENTID", 100)
                .SetIsHidden(); // 공정 ID
            hidden.AddTextBoxColumn("PROCESSSEGMENTVERSION", 70)
                .SetIsHidden(); // 공정 Version
            hidden.AddTextBoxColumn("PRODUCTDEFID", 100)
                .SetIsHidden(); // 품목 ID
            hidden.AddTextBoxColumn("PRODUCTDEFVERSION", 100)
                .SetIsHidden(); // 품목 Version
            hidden.AddTextBoxColumn("REASONCONSUMABLEDEFID", 100)
                .SetIsHidden(); // 원인품목 ID
            hidden.AddTextBoxColumn("REASONSEGMENTID", 100)
                .SetIsHidden(); // 원인공정 ID
            hidden.AddTextBoxColumn("REASONAREAID", 100)
                .SetIsHidden(); // 원인작업장 ID
            hidden.AddTextBoxColumn("CONSUMABLEDEFID", 100)
                .SetIsHidden(); // 자재 ID
            hidden.AddTextBoxColumn("PARENTLOTID", 100)
                .SetIsHidden(); // Parent Lot No
            hidden.AddTextBoxColumn("LOTID", 100)
                .SetIsHidden(); // Lot No
            hidden.AddTextBoxColumn("SEQUENCE", 100)
                .SetIsHidden(); // 시퀀스
            hidden.AddTextBoxColumn("TXNHISTKEY", 100)
                .SetIsHidden(); // Txn Hist Key
            hidden.AddTextBoxColumn("LEFTQTY", 70)
                .SetIsHidden(); // 잔량
            hidden.AddTextBoxColumn("PROCESSDEFID", 100)
                .SetIsHidden(); // 라우팅 ID
            hidden.AddTextBoxColumn("PROCESSDEFVERSION", 100)
                .SetIsHidden(); // 라우팅 Version
            hidden.AddTextBoxColumn("PROCESSPATHID", 100)
                .SetIsHidden(); // 공정별 라우팅 ID 
            hidden.AddTextBoxColumn("USERSEQUENCE", 100)
                .SetIsHidden(); // 공정수순
            hidden.AddTextBoxColumn("STATUSCODE", 100)
                .SetIsHidden(); // 상태코드 ID
            hidden.AddTextBoxColumn("QCSEGMENTID", 100)
                .SetIsHidden(); // 품질공정 ID
            hidden.AddTextBoxColumn("CONFIRMSITE", 100)
                .SetIsHidden(); // Site 자동확정여부

            grdDefectConfirmation.View.PopulateColumns();

            grdDefectConfirmation.View.OptionsNavigation.AutoMoveRowFocus = false;
        }

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            grdDefectCasue.View.FocusedRowChanged += View_FocusedRowChanged;
            grdDefectCasue.View.RowClick += View_RowClick;
            grdDefectCasue.View.RowStyle += View_RowStyle;

            grdDefectConfirmation.View.RowCellClick += View_RowCellClick;
            grdDefectConfirmation.View.RowCellStyle += View_RowCellStyle;

            btnHold.Click += BtnHold_Click;
            btnCancelHold.Click += BtnCancelHold_Click;
            btnDefectConfirmation.Click += BtnDefectConfirmation_Click;
            btnConfirmationCancel.Click += BtnConfirmationCancle_Click;
            btnAllCause.Click += BtnAllCause_Click;
        }

        /// <summary>
        /// LOT별 불량코드 조회
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_RowClick(object sender, RowClickEventArgs e)
        {
            pnlContent.ShowWaitArea();
            SearchDefectLotDefectCode();
            pnlContent.CloseWaitArea();
        }

        /// <summary>
        /// Check Row 색깔변경
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_RowStyle(object sender, RowStyleEventArgs e)
        {
            //if (e.RowHandle < 0) return;
            //bool isChecked = grdDefectCasue.View.IsRowChecked(e.RowHandle);

            //if (isChecked)
            //{
            //    e.Appearance.BackColor = Color.FromArgb(254, 254, 16);
            //    e.HighPriority = true;
            //}

            if (e.RowHandle < 0) return;

            // 보류상태일때 빨간색으로 글자표시
            if (grdDefectCasue.View.GetDataRow(e.RowHandle)["STATUS"].Equals("HoldComplete"))
            {
                e.HighPriority = true;
                e.Appearance.ForeColor = Color.Red;
            }

            // 포커스 받은 Row 색깔표시
            GridView view = sender as GridView;
            if (e.RowHandle == view.FocusedRowHandle)
            {
                e.HighPriority = true;
                e.Appearance.BackColor = Color.Yellow;
            }
        }

        /// <summary>
        /// 보류 취소
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancelHold_Click(object sender, EventArgs e)
        {
            DataTable dt = grdDefectCasue.View.GetCheckedRows();

            // 체크된 행이 없을때
            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("GridNoChecked");
                return;
            }
            // 마감된 행이 포함되어 있다면 Exception
            else if (dt.AsEnumerable().Where(r => r["STATUS"].Equals("DeadlineComplete")).Count() > 0)
            {
                throw MessageException.Create("AlreadyDeadlineComplete"); // 마감된 행이 존재합니다.
            }
            // 보류처리상태가 아닌행이 포함되어있다면
            else if (dt.AsEnumerable().Where(r => !r["STATUS"].Equals("HoldComplete")).Count() > 0)
            {
                throw MessageException.Create("NotAlreadyHoldComplete"); // 보류처리상태가 아닌 행이 존재합니다.
            }
            // 보류취소 하시겠습니까?
            if (this.ShowMessageBox("IsCancelHold", "Information", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                dt.Columns.Add("HOLDFLAG");

                foreach (DataRow row in dt.Rows)
                {
                    row["HOLDFLAG"] = "CancelHold";
                }

                this.ExecuteRule("SaveLotDefectHold", dt);
                this.OnSearchAsync();
                this.SearchDefectLotDefectCode();
            }
        }
        
        /// <summary>
        /// 보류
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnHold_Click(object sender, EventArgs e)
        {
            DataTable dt = grdDefectCasue.View.GetCheckedRows();

            // 체크된 행이 없을때
            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("GridNoChecked");
                return;
            }
            // Interface 진행중인 행이 포함되어 있다면 Exception
            else if (dt.AsEnumerable().Where(r => r["ISIFSUCCESS"].Equals("Send")).Count() > 0)
            {
                throw MessageException.Create("인터페이스 진행중인 행이 존재합니다."); // 인터페이스 진행중인 행이 존재합니다.
            }
            // 마감된 행이 포함되어 있다면 Exception
            else if (dt.AsEnumerable().Where(r => r["STATUS"].Equals("DeadlineComplete")).Count() > 0)
            {
                throw MessageException.Create("AlreadyDeadlineComplete"); // 마감된 행이 존재합니다.
            }
            // 보류 하시겠습니까?
            if (this.ShowMessageBox("IsHold", "Information", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                dt.Columns.Add("HOLDFLAG");

                foreach (DataRow row in dt.Rows)
                {
                    row["HOLDFLAG"] = "Hold";
                }

                this.ExecuteRule("SaveLotDefectHold", dt);
                this.OnSearchAsync();
                this.SearchDefectLotDefectCode();
            }
        }

        /// <summary>
        /// 마감
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDeadline_Click(object sender, EventArgs e)
        {
            DataTable dt = grdDefectCasue.View.GetCheckedRows();

            // 체크된 행이 없을때
            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("GridNoChecked");
                return;
            }
            // I/F 성공여부가 성공이거나 진행중인 행이 포함되어 있다면 Exception
            else if (dt.AsEnumerable().Where(r => r["ISIFSUCCESS"].Equals("Success") || r["ISIFSUCCESS"].Equals("Send")).Count() > 0)
            {
                throw MessageException.Create("IFSuccessOrProgressRow"); // I/F가 성공했거나 진행중인 행이 존재합니다.
            }
            // 보류, 마감, 인수된 행이 포함되어 있다면 Exception
            else if (dt.AsEnumerable().Where(r => r["STATUS"].Equals("DeadlineComplete") || r["STATUS"].Equals("HoldComplete") || r["STATUS"].Equals("InboundComplete")).Count() > 0)
            {
                throw MessageException.Create("HoldOrDeadlineRow"); // 보류, 마감, 인수된 행이 존재합니다.
            }
            // 인터페이스 마감처리 하시겠습니까?
            if (this.ShowMessageBox("IsInterfaceDeadline", "Information", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                dt.Columns.Add("DEADLINEFLAG");

                foreach (DataRow row in dt.Rows)
                {
                    row["DEADLINEFLAG"] = "Deadline";
                }

                this.ExecuteRule("SaveDefectLotManualDeadline", dt);
                this.OnSearchAsync();
                this.SearchDefectLotDefectCode();
            }
        }

        /// <summary>
        /// 확정상태가 Y라면 붉은색으로 Row 표시
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (grdDefectConfirmation.View.GetRowCellValue(e.RowHandle, "STATUSCODE").Equals("Confirm"))
            {
                e.Appearance.BackColor = Color.PaleVioletRed;
            }
        }

        /// <summary>
        /// 원인불명 전체체크박스 선택
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAllCause_Click(object sender, EventArgs e)
        {
            if (grdDefectConfirmation.View.RowCount == 0) return;

            foreach (DataRow row in (grdDefectConfirmation.DataSource as DataTable).Rows)
            {
                // 한개라도 false면 전체를 true로 변경
                if (row["ISUNKNOWN"].ToString().Equals("False"))
                {
                    for (int i = 0; i < grdDefectConfirmation.View.RowCount; i++)
                    {
                        grdDefectConfirmation.View.SetRowCellValue(i, "ISUNKNOWN", true);
                    }
                    break;
                }
                // 전체가 true면 전체를 false로 변경
                else
                {
                    row["ISUNKNOWN"] = false;
                }
            }
            grdDefectConfirmation.View.RefreshData();
        }

        /// <summary>
        /// 확정취소
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnConfirmationCancle_Click(object sender, EventArgs e)
        {
            DataTable dt = grdDefectConfirmation.View.GetCheckedRows();
            DataRow parentRow = grdDefectCasue.View.GetFocusedDataRow();

            // 체크된 행이 없을때
            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("GridNoChecked");
                return;
            }
            // 마감된 건이라면 Exception
            else if (parentRow["STATUS"].Equals("DeadlineComplete"))
            {
                throw MessageException.Create("AlreadyDeadlineComplete");
            }
            // 확정정보가 없는 건이 존재한다면 Exception
            else if (dt.AsEnumerable().Where(r => string.IsNullOrWhiteSpace(Format.GetString(r["STATUS"]))).Count() > 0)
            {
                throw MessageException.Create("NotConfirmDataExist"); 
            }
            // Claim 마감된 건이 있다면 Exception
            else if (dt.AsEnumerable().Where(r => r["ISCLAIM"].Equals("Y")).Count() > 0)
            {
                throw MessageException.Create("ClaimCompleteExistenceRow");
            }
            // I/F 성공여부가 Send 또는 Success라면 Exception 
            else if (parentRow["ISIFSUCCESS"].Equals("Send") || parentRow["ISIFSUCCESS"].Equals("Success"))
            {
                throw MessageException.Create("IFSuccessOrProgressRow");
            }

            // 확정취소 하시겠습니까?
            if (this.ShowMessageBox("IsNoConfirmation", "Information", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                dt.Columns.Add("CONFIRMFLAG");

                foreach (DataRow row in dt.Rows)
                {
                    row["CONFIRMFLAG"] = "ConfirmCancel";
                    row["STATUS"] = "ConfirmCancel";
                    row["DEFINEUSER"] = UserInfo.Current.Id;
                    row["DEFINETIME"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }

                this.ExecuteRule("SaveLotDefectConfirmation", dt);
                this.ShowMessage("NoConfirmation");
                this.OnSearchAsync();
                this.SearchDefectLotDefectCode();
            }
        }

        /// <summary>
        /// 불량확정
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDefectConfirmation_Click(object sender, EventArgs e)
        {
            DataTable dt = grdDefectConfirmation.View.GetCheckedRows();

            // 체크된 행이 없을때
            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("GridNoChecked");
                return;
            }
            // 확정이나 마감된 행이 포함되어 있다면 Exception
            else if (dt.AsEnumerable().Where(r => r["STATUSCODE"].Equals("Confirm") || r["STATUSCODE"].Equals("DeadlineComplete")).Count() > 0)
            {
                throw MessageException.Create("AlreadyConfirmOrDeadlineComplete");
            }
            // 부모행이 보류처리상태라면 Exception
            else if (grdDefectCasue.View.GetFocusedDataRow()["STATUS"].Equals("HoldComplete"))
            {
                throw MessageException.Create("HoldStateIsNotDefectConfirm"); // 보류처리상태이므로 불량확정이 불가능합니다.
            }

            // 확정 하시겠습니까?
            if (this.ShowMessageBox("IsConfirmation", "Information", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                dt.Columns.Add("CONFIRMFLAG");

                foreach (DataRow row in dt.Rows)
                {
                    row["CONFIRMFLAG"] = "Confirm";
                    row["STATUS"] = "Confirm";
                    row["DEFINEUSER"] = UserInfo.Current.Id;
                    row["DEFINETIME"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }
                    
                this.ExecuteRule("SaveLotDefectConfirmation", dt);
                this.ShowMessage("Confirmation");
                this.OnSearchAsync();
                this.SearchDefectLotDefectCode();
            }
        }

        /// <summary>
        /// 팝업을 호출하기위한 그리드 Cell 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                // 상태가 확정이나 마감상태라면 불량조정 및 반출 할 수 없다.
                if (grdDefectConfirmation.View.GetRowCellValue(e.RowHandle, "STATUSCODE").Equals("Confirm")
                    || grdDefectConfirmation.View.GetRowCellValue(e.RowHandle, "STATUSCODE").Equals("DeadlineComplete"))
                {
                    // 확정이나 마감상태이므로 불량조정 및 반출할 수 없습니다.
                    throw MessageException.Create("NotDefectMakeupAndOutbound");
                }

                // 반출수량 Column을 더블클릭했다면 불량반출 팝업 호출
                if (e.Column.FieldName == "OUTBOUNDQTY")
                {
                    if (btnFlag.Enabled)
                    {
                        DefectCodeTakeout popup = new DefectCodeTakeout()
                        {
                            Owner = this,
                            CurrentDataRow = grdDefectConfirmation.View.GetFocusedDataRow()

                        };

                        if (popup.ShowDialog() == DialogResult.OK)
                        {
                            grdDefectConfirmation.BeginInvoke(new MethodInvoker(() =>
                            {
                                SearchDefectLotDefectCode();
                            }));
                        }
                    }
                }
                // 그 이외의 Column을 더블클릭했다면 불량내역조정 팝업 호출
                else
                {
                    if (e.Column.FieldName != "ISUNKNOWN")
                    {
                        if (btnFlag.Enabled)
                        {
                            DefectCodeMakeup popup = new DefectCodeMakeup()
                            {
                                Owner = this,
                                CurrentDataRow = grdDefectConfirmation.View.GetFocusedDataRow()
                            };

                            if (popup.ShowDialog() == DialogResult.OK)
                            {
                                grdDefectConfirmation.BeginInvoke(new MethodInvoker(() =>
                                {
                                    SearchDefectLotDefectCode();
                                }));
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// LOT별 불량코드 조회
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            pnlContent.ShowWaitArea();
            SearchDefectLotDefectCode();
            pnlContent.CloseWaitArea();      
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
            // DataTable changed = grdList.GetChangedRows();

            // ExecuteRule("SaveCodeClass", changed);
        }

        /// <summary>
        /// 툴바버튼 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            SmartButton btn = sender as SmartButton;

            if (btn.Name.ToString().Equals("Hold"))
            {
                BtnHold_Click(null, null);
            }
            else if (btn.Name.ToString().Equals("CancelHold"))
            {
                BtnCancelHold_Click(null, null);
            }
            else if (btn.Name.ToString().Equals("Deadline"))
            {
                BtnDeadline_Click(null, null);
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

            var values = Conditions.GetValues();
            values.Add("P_LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable dt = await SqlExecuter.QueryAsync("GetInboundDefectLot", "10002", values);

            if (dt.Rows.Count < 1)
            {
                this.ShowMessage("NoSelectData");
                grdDefectCasue.DataSource = null;
                grdDefectConfirmation.DataSource = null;
                return;
            }

            grdDefectCasue.DataSource = dt;
            SearchDefectLotDefectCode();
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            #region 작업장

            var condition = Conditions.AddSelectPopup("p_areaId", new SqlQuery("GetAreaListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "AREANAME", "AREAID")
                                      .SetPopupLayout("AREA", PopupButtonStyles.Ok_Cancel, true, true)
                                      .SetPopupLayoutForm(500, 600)
                                      .SetLabel("AREA")
                                      .SetPopupResultCount(1)
                                      .SetPosition(1.3)
                                      .SetRelationIds("p_plantId");

            // 팝업 조회조건
            condition.Conditions.AddTextBox("AREAIDNAME").SetLabel("AREAIDNAME");
            // 팝업 그리드
            condition.GridColumns.AddTextBoxColumn("AREAID", 150).SetValidationKeyColumn();
            condition.GridColumns.AddTextBoxColumn("AREANAME", 200);

            #endregion

            #region 품목

            condition = Conditions.AddSelectPopup("p_productdefId", new SqlQuery("GetProductListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PRODUCTDEFNAME", "PRODUCTDEFIDVERSION")
                                  .SetPopupLayout("PRODUCT", PopupButtonStyles.Ok_Cancel, true, true)
                                  .SetPopupLayoutForm(600, 800)
                                  .SetLabel("PRODUCT")
                                  .SetPopupAutoFillColumns("PRODUCTDEFNAME")
                                  .SetPopupResultCount(0) // 다중 선택
                                  .SetPosition(1.4);

            // 팝업 조회조건
            condition.Conditions.AddTextBox("PRODUCTDEFIDNAME");

            // 팝업 그리드
            condition.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 120).SetValidationKeyColumn();
            condition.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 220);
            condition.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 80);
            condition.GridColumns.AddTextBoxColumn("PRODUCTDEFIDVERSION", 100).SetIsHidden();

            #endregion

            // 폐기여부
            Conditions.AddComboBox("p_deadline", new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                      .SetLabel("SCRAPSTATUS")
                      .SetEmptyItem()
                      .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                      .SetPosition(6);
        }

        #endregion

        #region Private Function  

        /// <summary>
        /// LOT별 불량코드 조회 검색함수
        /// </summary>
        private void SearchDefectLotDefectCode()
        {
            if(!grdDefectCasue.View.IsFocusedView)
            {
                return;
            }

            DataRow dr = grdDefectCasue.View.GetFocusedDataRow();
            txtLot.EditValue = Format.GetString(dr["LOTID"], string.Empty);
            txtDefectCount.EditValue = Format.GetString(dr["DEFECTQTY"], string.Empty);
            txtUom.EditValue = Format.GetString(dr["UNIT"], string.Empty);

            Dictionary<string, object> param = new Dictionary<string, object>()
            {
                { "p_languageType", UserInfo.Current.LanguageType },
                { "p_lotId", Format.GetString(dr["LOTID"], string.Empty) },
                { "p_parentLotId", Format.GetString(dr["PARENTLOTID"], string.Empty) },
                { "p_userSequence", Format.GetString(dr["USERSEQUENCE"], string.Empty) }
            };

            grdDefectConfirmation.DataSource = SqlExecuter.Query("GetDefectLotDefectCode", "10002", param);

            //var row = grdDefectCasue.View.GetDataRow(grdDefectCasue.View.FocusedRowHandle);

            //if (row == null)
            //{
            //    return;
            //}

            //txtLot.EditValue = row["LOTID"];
            //txtDefectCount.EditValue = row["DEFECTQTY"];
            //txtUom.EditValue = row["UNIT"];

            //Dictionary<string, object> param = new Dictionary<string, object>()
            //{
            //    { "p_languageType", UserInfo.Current.LanguageType },
            //    { "p_lotId", row["LOTID"].ToString() },
            //    { "p_parentLotId", row["PARENTLOTID"] },
            //    { "p_userSequence", row["USERSEQUENCE"] }
            //};
            //grdDefectConfirmation.DataSource = SqlExecuter.Query("GetDefectLotDefectCode", "10002", param);
        }

        #endregion
    }
}
