#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons;
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
    /// 프 로 그 램 명  : 품질관리 > 불량관리 > Lot 병합승인
    /// 업  무  설  명  : 불량품 폐기취소 화면에서 병합요청을 한 불량들에 대해 승인이나 반려할 수 있는 화면이다.
    /// 생    성    자  : 유태근
    /// 생    성    일  : 2019-07-25
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class DefectMergeAccept : SmartConditionManualBaseForm
    {
        #region Local Variables

        #endregion

        #region 생성자

        public DefectMergeAccept()
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

            InitializeRequestMergeGrid();

            InitializeDefectCodeMergeHistoryLot();
            InitializeProductRouting();
            InitializeReworkRouting();
        }

        /// <summary>
        /// 병합요청정보 그리드
        /// </summary>
        private void InitializeRequestMergeGrid()
        {
            grdLotMergeAccept.View.SetIsReadOnly();
            grdLotMergeAccept.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdLotMergeAccept.GridButtonItem = GridButtonItem.Export;
            grdLotMergeAccept.View.CheckMarkSelection.MultiSelectCount = 1;

            var requestInfo = grdLotMergeAccept.View.AddGroupColumn("REQUESTINFO");

            requestInfo.AddTextBoxColumn("APPROVALSTATUS", 80)
                .SetTextAlignment(TextAlignment.Center); // 승인상태
            requestInfo.AddTextBoxColumn("REQUESTTIME", 200)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("REQUESTDATE"); // 요청일시
            requestInfo.AddTextBoxColumn("PRODUCTDEFID", 180); // 품목코드
            requestInfo.AddTextBoxColumn("PRODUCTDEFNAME", 250); // 품목명
            requestInfo.AddTextBoxColumn("PRODUCTDEFVERSION", 80)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("Rev"); // 품목 Version

            var requestSegment = grdLotMergeAccept.View.AddGroupColumn("REQUESTSEGMENT");

            requestSegment.AddTextBoxColumn("PLANTID", 100)
                .SetLabel("PLANT"); // Site
            requestSegment.AddSpinEditColumn("USERSEQUENCE", 80); // 순서
            requestSegment.AddTextBoxColumn("PROCESSSEGMENTNAME", 180); // 공정명
            requestSegment.AddTextBoxColumn("REQUESTUSER", 100)
                .SetTextAlignment(TextAlignment.Center); // 요청자

            var cancelMerge = grdLotMergeAccept.View.AddGroupColumn("MERGECANCELCOUNT");

            cancelMerge.AddTextBoxColumn("REPAIRLOTNO", 200)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("MERGELOTNO"); // 병합 Lot No
            cancelMerge.AddTextBoxColumn("PARENTLOTID", 200)
                .SetTextAlignment(TextAlignment.Center); // 병합 Parent Lot No
            cancelMerge.AddTextBoxColumn("REASONCANCEL", 80)
                .SetTextAlignment(TextAlignment.Center); // 취소사유
            cancelMerge.AddSpinEditColumn("MERGELOTCOUNT", 80)
                .SetTextAlignment(TextAlignment.Right)
                .SetLabel("LOT"); // 병합취소 LOT 수량
            cancelMerge.AddSpinEditColumn("MERGEPCSQTY", 80)
                .SetTextAlignment(TextAlignment.Right)
                .SetLabel("PCS"); // 병합취소 PCS 수량 
            cancelMerge.AddSpinEditColumn("MERGEPNLQTY", 80)
                .SetTextAlignment(TextAlignment.Right)
                .SetLabel("PNL"); // 병합취소 PNL 수량 
            cancelMerge.AddTextBoxColumn("UOM", 80)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(); // 병합취소 UOM 단위

            var etc = grdLotMergeAccept.View.AddGroupColumn("ETC");

            etc.AddTextBoxColumn("APPROVALUSER", 100)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(); // 승인자
            etc.AddTextBoxColumn("APPROVALDATE", 200)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(); // 승인일자

            var hidden = grdLotMergeAccept.View.AddGroupColumn("HIDDEN");

            hidden.AddTextBoxColumn("STATUS", 100)
                .SetIsHidden(); // 승인상태코드
            hidden.AddTextBoxColumn("REQUESTNO", 100)
                .SetIsHidden(); // 요청번호
            hidden.AddTextBoxColumn("MERGEROUTINGTYPE", 100)
                .SetIsHidden(); // 라우팅 타입
            hidden.AddTextBoxColumn("PROCESSDEFID", 100)
                .SetIsHidden(); // 라우팅 ID
            hidden.AddTextBoxColumn("PROCESSDEFVERSION", 100)
                .SetIsHidden(); // 라우팅 Version
            hidden.AddTextBoxColumn("PROCESSPATHID", 100)
                .SetIsHidden(); // 라우팅 상세 ID
            hidden.AddTextBoxColumn("REQUESTORCOMMENT", 100)
                .SetIsHidden(); // 요청 Comment
            hidden.AddTextBoxColumn("APPROVALCOMMENT", 100)
                .SetIsHidden(); // 승인/반려 Comment
            hidden.AddTextBoxColumn("RESOURCEID", 100)
                .SetIsHidden(); // 투입 자원 ID
            hidden.AddTextBoxColumn("RESOURCENAME", 100)
                .SetIsHidden(); // 투입 자원명
            hidden.AddTextBoxColumn("RETURNPROCESSPATHID", 100)
                .SetIsHidden(); // 재작업 후 라우팅 상세 ID
            hidden.AddTextBoxColumn("RETURNPROCESSSEGMENTID", 100)
                .SetIsHidden(); // 재작업 후 공정 ID
            hidden.AddTextBoxColumn("RETURNPROCESSSEGMENTVERSION", 100)
                .SetIsHidden(); // 재작업 후 공정 Version
            hidden.AddTextBoxColumn("RETURNUSERSEQUENCE", 100)
                .SetIsHidden(); // 재작업 후 공정수순
            hidden.AddTextBoxColumn("RETURNAREAID", 100)
                .SetIsHidden(); // 재작업 후 작업장 ID
            hidden.AddTextBoxColumn("RETURNRESOURCEID", 100)
                .SetIsHidden(); // 재작업 후 자원 ID
            hidden.AddTextBoxColumn("RETURNRESOURCENAME", 100)
                .SetIsHidden(); // 재작업 후 자원명

            grdLotMergeAccept.View.PopulateColumns();

            grdLotMergeAccept.View.OptionsNavigation.AutoMoveRowFocus = false;
        }

        /// <summary>        
        /// 병합한 불량코드 내역별로 Lot 정보를 조회한다.
        /// </summary>
        private void InitializeDefectCodeMergeHistoryLot()
        {
            grdMergeLotInfo.View.SetIsReadOnly();

            grdMergeLotInfo.View.AddTextBoxColumn("PARENTLOTID", 220)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("Lot No");
            grdMergeLotInfo.View.AddSpinEditColumn("PCS", 80)
                .SetTextAlignment(TextAlignment.Right);
            grdMergeLotInfo.View.AddSpinEditColumn("PNL", 80)
                .SetTextAlignment(TextAlignment.Right);
            grdMergeLotInfo.View.AddTextBoxColumn("LOTID", 200)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("DEFECTLOTID");
            grdMergeLotInfo.View.AddTextBoxColumn("DEFECTCODE", 100)
                .SetTextAlignment(TextAlignment.Center);
            grdMergeLotInfo.View.AddTextBoxColumn("DEFECTNAME", 200);
            grdMergeLotInfo.View.AddTextBoxColumn("QCSEGMENTNAME", 150);

            grdMergeLotInfo.View.PopulateColumns();

            grdMergeLotInfo.View.OptionsNavigation.AutoMoveRowFocus = false;
        }

        /// <summary>        
        /// 병합할 Lot의 지정한 품목라우팅 그리드
        /// </summary>
        private void InitializeProductRouting()
        {
            grdProductRouting.View.SetIsReadOnly();
            grdProductRouting.View.SetAutoFillColumn("PROCESSSEGMENTNAME");

            grdProductRouting.View.AddSpinEditColumn("USERSEQUENCE", 80)
                .SetTextAlignment(TextAlignment.Right); // 공정수순
            grdProductRouting.View.AddTextBoxColumn("PROCESSSEGMENTID", 150); // 공정 ID
            grdProductRouting.View.AddTextBoxColumn("PROCESSSEGMENTVERSION", 80)
                .SetTextAlignment(TextAlignment.Center); // 공정 Version
            grdProductRouting.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 220); // 공정명

            grdProductRouting.View.AddTextBoxColumn("PROCESSPATHID", 100)
                .SetIsHidden(); // 라우팅 상세 ID

            grdProductRouting.View.PopulateColumns();

            grdProductRouting.View.OptionsNavigation.AutoMoveRowFocus = false;
        }

        /// <summary>        
        /// 병합할 Lot의 지정한 재작업라우팅 그리드
        /// </summary>
        private void InitializeReworkRouting()
        {
            grdReworkRouting.View.SetIsReadOnly();
            grdReworkRouting.View.SetAutoFillColumn("PROCESSSEGMENTNAME");

            grdReworkRouting.View.AddSpinEditColumn("USERSEQUENCE", 80)
                .SetTextAlignment(TextAlignment.Right); // 공정수순
            grdReworkRouting.View.AddTextBoxColumn("PROCESSSEGMENTID", 150); // 공정 ID
            grdReworkRouting.View.AddTextBoxColumn("PROCESSSEGMENTVERSION", 80)
                .SetTextAlignment(TextAlignment.Center); // 공정 Version
            grdReworkRouting.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 220); // 공정명

            grdReworkRouting.View.AddTextBoxColumn("PROCESSPATHID", 100)
                .SetIsHidden(); // 라우팅 상세 ID

            grdReworkRouting.View.PopulateColumns();

            grdReworkRouting.View.OptionsNavigation.AutoMoveRowFocus = false;
        }

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            //btnAccept.Click += CommentPopup;
            //btnCancel.Click += CommentPopup;
            //btnReRequest.Click += BtnReRequest_Click;
            //btnLotCard.Click += BtnLotCard_Click;

            grdLotMergeAccept.View.RowClick += View_RowClick;
            grdLotMergeAccept.View.FocusedRowChanged += View_FocusedRowChanged;
            grdProductRouting.View.RowStyle += View_RowStyle;
        }

        /// <summary>
        /// 승인된 건에 대한 Lot Card 출력
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLotCard_Click(object sender, EventArgs e)
        {
            DataTable dt = grdLotMergeAccept.View.GetCheckedRows();

            // 체크된 행이 없으면 Exception
            if (dt.Rows.Count == 0)
            {
                throw MessageException.Create("GridNoChecked");
            }
            // 승인된 건이 아니라면 Exception
            else if (!dt.Rows[0]["STATUS"].Equals("APPROVAL"))
            {
                // 승인된 건에 대해서만 LotCard 출력이 가능합니다.
                throw MessageException.Create("LotCardIsApproval");
            }
            else if (this.ShowMessage(MessageBoxButtons.YesNo, "PrintMergeLotCard") == DialogResult.Yes)
            {
                CommonFunction.PrintLotCard(dt.Rows[0]["REPAIRLOTNO"].ToString(), LotCardType.Normal); // Merge LotCard출력
            }
        }

        /// <summary>
        /// 반려된 건에 대해서 재요청한다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReRequest_Click(object sender, EventArgs e)
        {
            DataTable dt = grdLotMergeAccept.View.GetCheckedRows();
            string approvalFlag = "R";

            // 체크된 행이 없다면 Exception
            if (dt.Rows.Count == 0)
            {
                throw MessageException.Create("GridNoChecked");
            }
            // 반려건이 아니라면 Exception
            else if (!dt.Rows[0]["STATUS"].Equals("REJECT"))
            {
                // 반려된 건에 대해서만 재요청할 수 있습니다.
                throw MessageException.Create("ReRequestIsReject");
            }

            if (this.ShowMessage(MessageBoxButtons.YesNo, "DoReRequest") == DialogResult.Yes)
            {
                MessageWorker worker = new MessageWorker("SaveLotDefectMergeApproval");
                worker.SetBody(new MessageBody()
                {
                    { "approvalUser", UserInfo.Current.Id }, // 요청자
                    { "approvalDate", DateTime.Now }, // 요청일시
                    { "requestNo", dt.Rows[0]["REQUESTNO"] }, // 요청번호
                    { "approvalFlag", approvalFlag} // 승인/반려/재요청 플래그
                });

                worker.Execute();
                this.OnSearchAsync();
            }
        }

        /// <summary>
        /// 품목라우팅일 경우 시작점을 표시해주는 Row 색깔
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (grdMergeLotInfo.View.RowCount == 0) return;

            // 품목라우팅일 경우
            if (grdLotMergeAccept.View.GetFocusedRowCellValue("MERGEROUTINGTYPE").Equals("Product"))
            {
                if (e.RowHandle != -1)
                {
                    if (grdProductRouting.View.GetRowCellValue(e.RowHandle, "PROCESSSEGMENTID").Equals(grdLotMergeAccept.View.GetFocusedRowCellValue("INPUTPROCESSSEGMENTID"))
                        && grdProductRouting.View.GetRowCellValue(e.RowHandle, "USERSEQUENCE").Equals(grdLotMergeAccept.View.GetFocusedRowCellValue("INPUTUSERSEQUENCE")))
                    {
                        e.HighPriority = true;
                        e.Appearance.BackColor = Color.Yellow;
                    }
                }
            }
            // 재작업라우팅일 경우(재작업 후 사용자가 돌아올 곳을 표시)
            else if (grdLotMergeAccept.View.GetFocusedRowCellValue("MERGEROUTINGTYPE").Equals("Rework"))
            {
                if (e.RowHandle != -1)
                {
                    if (grdProductRouting.View.GetRowCellValue(e.RowHandle, "PROCESSSEGMENTID").Equals(grdLotMergeAccept.View.GetFocusedRowCellValue("RETURNPROCESSSEGMENTID"))
                        && grdProductRouting.View.GetRowCellValue(e.RowHandle, "USERSEQUENCE").Equals(grdLotMergeAccept.View.GetFocusedRowCellValue("RETURNUSERSEQUENCE")))
                    {
                        e.HighPriority = true;
                        e.Appearance.BackColor = Color.Yellow;
                    }
                }
            }
        }

        /// <summary>
        /// 신청정보 그리드의 병합정보를 검색한다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (grdLotMergeAccept.View.RowCount != 0)
            {
                string lotId = string.IsNullOrWhiteSpace(Format.GetString(grdLotMergeAccept.View.GetRowCellValue(grdLotMergeAccept.View.FocusedRowHandle, "REPAIRLOTNO")))
                               ? Format.GetString(grdMergeLotInfo.View.GetRowCellValue(0, "LOTID"))
                               : Format.GetString(grdLotMergeAccept.View.GetRowCellValue(grdLotMergeAccept.View.FocusedRowHandle, "REPAIRLOTNO"));

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                param.Add("REQUESTNO", grdLotMergeAccept.View.GetRowCellValue(grdLotMergeAccept.View.FocusedRowHandle, "REQUESTNO"));
                param.Add("LOTID", lotId);
                param.Add("ROUTINGTYPE", grdLotMergeAccept.View.GetRowCellValue(grdLotMergeAccept.View.FocusedRowHandle, "MERGEROUTINGTYPE"));
                param.Add("PRODUCTDEFID", grdLotMergeAccept.View.GetRowCellValue(grdLotMergeAccept.View.FocusedRowHandle, "PRODUCTDEFID"));
                param.Add("PRODUCTDEFVERSION", grdLotMergeAccept.View.GetRowCellValue(grdLotMergeAccept.View.FocusedRowHandle, "PRODUCTDEFVERSION"));

                // 요청번호에 따른 병합Lot정보 조회
                DataTable dt1 = SqlExecuter.Query("GetDefectLotMergeLotInfo", "10001", param);

                txtMergeLotId1.EditValue = grdLotMergeAccept.View.GetRowCellValue(grdLotMergeAccept.View.FocusedRowHandle, "REPAIRLOTNO");
                txtPcsCnt.EditValue = dt1.AsEnumerable().Sum(r => Convert.ToInt32(r["PCS"]));
                txtPnlCnt.EditValue = dt1.AsEnumerable().Sum(r => Convert.ToInt32(r["PNL"]));
                grdMergeLotInfo.DataSource = dt1;

                // 요청번호에 따른 병합라우팅 조회
                DataTable productRouting = new DataTable();
                DataTable reworkRouting = new DataTable();

                // 품목 라우팅
                if (grdLotMergeAccept.View.GetRowCellValue(grdLotMergeAccept.View.FocusedRowHandle, "MERGEROUTINGTYPE").Equals("Product"))
                {
                    productRouting = SqlExecuter.Query("GetProductRoutingPreviousProcessPaths", "10003", param);

                    txtMergeLotId2.EditValue = grdLotMergeAccept.View.GetRowCellValue(grdLotMergeAccept.View.FocusedRowHandle, "REPAIRLOTNO");
                    txtReason1.EditValue = grdLotMergeAccept.View.GetRowCellValue(grdLotMergeAccept.View.FocusedRowHandle, "REASONCANCEL");
                    txtReworkRoutingId.EditValue = null;
                    txtReworkRoutingName.EditValue = null;
                    txtReworkResource.EditValue = null;
                    grdReworkRouting.DataSource = null;
                    gbxReworkRouting.Enabled = false;

                    grdProductRouting.DataSource = productRouting;
                }
                // 재작업 라우팅
                else
                {
                    productRouting = SqlExecuter.Query("GetProductRoutingPreviousProcessPaths", "10003", param);
                    reworkRouting = SqlExecuter.Query("GetDefectCancelReworkRouting", "10001", param);

                    txtMergeLotId2.EditValue = grdLotMergeAccept.View.GetRowCellValue(grdLotMergeAccept.View.FocusedRowHandle, "REPAIRLOTNO");
                    txtReason1.EditValue = grdLotMergeAccept.View.GetRowCellValue(grdLotMergeAccept.View.FocusedRowHandle, "REASONCANCEL");
                    txtProductResource.EditValue = grdLotMergeAccept.View.GetRowCellValue(grdLotMergeAccept.View.FocusedRowHandle, "RETURNRESOURCENAME");
                    txtReworkResource.EditValue = grdLotMergeAccept.View.GetRowCellValue(grdLotMergeAccept.View.FocusedRowHandle, "RESOURCENAME");

                    if (reworkRouting.Rows.Count != 0)
                    {
                        txtReworkRoutingId.EditValue = reworkRouting.Rows[0]["PROCESSDEFID"];
                        txtReworkRoutingName.EditValue = reworkRouting.Rows[0]["PROCESSDEFNAME"];
                    }

                    gbxReworkRouting.Enabled = true;

                    grdProductRouting.DataSource = productRouting;
                    grdReworkRouting.DataSource = reworkRouting;
                }

                // 요청번호에 따른 요청, 승인/반려 Comment 조회
                txtMergeLotId3.EditValue = grdLotMergeAccept.View.GetRowCellValue(grdLotMergeAccept.View.FocusedRowHandle, "REPAIRLOTNO");
                txtReason2.EditValue = grdLotMergeAccept.View.GetRowCellValue(grdLotMergeAccept.View.FocusedRowHandle, "REASONCANCEL");
                memoRequestUserComment.Text = grdLotMergeAccept.View.GetRowCellValue(grdLotMergeAccept.View.FocusedRowHandle, "REQUESTORCOMMENT").ToString();
                memoAcceptComment.Text = grdLotMergeAccept.View.GetRowCellValue(grdLotMergeAccept.View.FocusedRowHandle, "APPROVALCOMMENT").ToString();
            }
        }

        /// <summary>
        /// 신청정보 그리드의 병합정보를 검색한다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grdLotMergeAccept.View.RowCount != 0)
            {
                string lotId = string.IsNullOrWhiteSpace(Format.GetString(grdLotMergeAccept.View.GetRowCellValue(e.FocusedRowHandle, "REPAIRLOTNO")))
                               ? Format.GetString(grdMergeLotInfo.View.GetRowCellValue(0, "LOTID"))
                               : Format.GetString(grdLotMergeAccept.View.GetRowCellValue(e.FocusedRowHandle, "REPAIRLOTNO"));

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                param.Add("REQUESTNO", grdLotMergeAccept.View.GetRowCellValue(e.FocusedRowHandle, "REQUESTNO"));
                param.Add("LOTID", lotId);
                param.Add("ROUTINGTYPE", grdLotMergeAccept.View.GetRowCellValue(e.FocusedRowHandle, "MERGEROUTINGTYPE"));
                param.Add("PRODUCTDEFID", grdLotMergeAccept.View.GetRowCellValue(e.FocusedRowHandle, "PRODUCTDEFID"));
                param.Add("PRODUCTDEFVERSION", grdLotMergeAccept.View.GetRowCellValue(e.FocusedRowHandle, "PRODUCTDEFVERSION"));

                // 요청번호에 따른 병합Lot정보 조회
                DataTable dt1 = SqlExecuter.Query("GetDefectLotMergeLotInfo", "10001", param);

                txtMergeLotId1.EditValue = grdLotMergeAccept.View.GetRowCellValue(e.FocusedRowHandle, "REPAIRLOTNO");
                txtPcsCnt.EditValue = dt1.AsEnumerable().Sum(r => Convert.ToInt32(r["PCS"]));
                txtPnlCnt.EditValue = dt1.AsEnumerable().Sum(r => Convert.ToInt32(r["PNL"]));
                grdMergeLotInfo.DataSource = dt1;

                // 요청번호에 따른 병합라우팅 조회
                DataTable productRouting = new DataTable();
                DataTable reworkRouting = new DataTable();

                // 품목 라우팅
                if (grdLotMergeAccept.View.GetRowCellValue(e.FocusedRowHandle, "MERGEROUTINGTYPE").Equals("Product"))
                {
                    productRouting = SqlExecuter.Query("GetProductRoutingPreviousProcessPaths", "10003", param);

                    txtMergeLotId2.EditValue = grdLotMergeAccept.View.GetRowCellValue(e.FocusedRowHandle, "REPAIRLOTNO");
                    txtReason1.EditValue = grdLotMergeAccept.View.GetRowCellValue(e.FocusedRowHandle, "REASONCANCEL");
                    txtReworkRoutingId.EditValue = null;
                    txtReworkRoutingName.EditValue = null;
                    txtReworkResource.EditValue = null;
                    grdReworkRouting.DataSource = null;
                    gbxReworkRouting.Enabled = false;

                    grdProductRouting.DataSource = productRouting;
                }
                // 재작업 라우팅
                else
                {
                    productRouting = SqlExecuter.Query("GetProductRoutingPreviousProcessPaths", "10003", param);
                    reworkRouting = SqlExecuter.Query("GetDefectCancelReworkRouting", "10001", param);

                    txtMergeLotId2.EditValue = grdLotMergeAccept.View.GetRowCellValue(e.FocusedRowHandle, "REPAIRLOTNO");
                    txtReason1.EditValue = grdLotMergeAccept.View.GetRowCellValue(e.FocusedRowHandle, "REASONCANCEL");
                    txtProductResource.EditValue = grdLotMergeAccept.View.GetRowCellValue(e.FocusedRowHandle, "RETURNRESOURCENAME");
                    txtReworkResource.EditValue = grdLotMergeAccept.View.GetRowCellValue(e.FocusedRowHandle, "RESOURCENAME");

                    if (reworkRouting.Rows.Count != 0)
                    {
                        txtReworkRoutingId.EditValue = reworkRouting.Rows[0]["PROCESSDEFID"];
                        txtReworkRoutingName.EditValue = reworkRouting.Rows[0]["PROCESSDEFNAME"];
                    }

                    gbxReworkRouting.Enabled = true;

                    grdProductRouting.DataSource = productRouting;
                    grdReworkRouting.DataSource = reworkRouting;
                }

                // 요청번호에 따른 요청, 승인/반려 Comment 조회
                txtMergeLotId3.EditValue = grdLotMergeAccept.View.GetRowCellValue(e.FocusedRowHandle, "REPAIRLOTNO");
                txtReason2.EditValue = grdLotMergeAccept.View.GetRowCellValue(e.FocusedRowHandle, "REASONCANCEL");
                memoRequestUserComment.Text = grdLotMergeAccept.View.GetRowCellValue(e.FocusedRowHandle, "REQUESTORCOMMENT").ToString();
                memoAcceptComment.Text = grdLotMergeAccept.View.GetRowCellValue(e.FocusedRowHandle, "APPROVALCOMMENT").ToString();
            }
        }

        /// <summary>
        /// 승인/반려 Comment를 작성할 수 있는 Popup창을 호출한다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommentPopup(object sender, EventArgs e, string state)
        {
            SmartButton button = sender as SmartButton;

            // 체크된 행이 없으면 Exception
            if (grdLotMergeAccept.View.GetCheckedRows().Rows.Count == 0)
            {
                throw MessageException.Create("GridNoChecked");
            }
            // 신청상태가 아니라면 Exception
            else if (!grdLotMergeAccept.View.GetCheckedRows().Rows[0]["STATUS"].Equals("REQUEST"))
            {
                // 신청인 상태에 대해서만 승인이나 반려를 할 수 있습니다.
                throw MessageException.Create("RequestIsApprovalOrReject");
            }

            AcceptCancelPopup popup = new AcceptCancelPopup();
            popup.Owner = this;
            popup.CurrentDataRow = grdLotMergeAccept.View.GetCheckedRows().Rows[0];

            // 승인버튼을 눌렀다면 승인플래그를 넘겨주고 그게 아니라면 반려플래그를 넘겨준다.
            if (state == "Approval")
            {
                popup.approvalFlag = "Y";
            }
            else
            {
                popup.approvalFlag = "N";
            }
          
            if (popup.ShowDialog() == DialogResult.OK)
            {
                this.OnSearchAsync();
            }
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

        /// <summary>
        /// 툴바버튼 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            SmartButton btn = sender as SmartButton;

            if (btn.Name.ToString().Equals("Approval"))
            {
                CommentPopup(null, null, "Approval");
            }
            else if (btn.Name.ToString().Equals("Return"))
            {
                CommentPopup(null, null, "Return");
            }
            else if (btn.Name.ToString().Equals("RequestAgain"))
            {
                BtnReRequest_Click(null, null);
            }
            else if (btn.Name.ToString().Equals("LotCard"))
            {
                BtnLotCard_Click(null, null);
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

            DataTable dt = await SqlExecuter.QueryAsync("GetLotDefectMergeRequest", "10001", values);

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("NoSelectData");

                grdLotMergeAccept.DataSource = null;

                grdMergeLotInfo.DataSource = null;
                txtMergeLotId1.EditValue = null;
                txtPcsCnt.EditValue = null;
                txtPnlCnt.EditValue = null;

                grdProductRouting.DataSource = null;
                txtMergeLotId2.EditValue = null;
                txtReason1.EditValue = null;
                txtReworkRoutingId.EditValue = null;
                txtReworkRoutingName.EditValue = null;
                txtReworkResource.EditValue = null;

                txtMergeLotId3.EditValue = null;
                txtReason2.EditValue = null;
                memoRequestUserComment.EditValue = null;
                memoAcceptComment.EditValue = null;

                return;
            }

            grdLotMergeAccept.DataSource = dt;
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            //Conditions.AddComboBox("p_plantId", new SqlQuery("GetPlantListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "PLANTNAME", "PLANTID")
            //    .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
            //    .SetDefault(UserInfo.Current.Plant)
            //    .SetLabel("PLANT")
            //    .SetPosition(1.1);

            InitializeConditionPopup_Area();
            InitializeConditionPopup_Product();
        }

        /// <summary>
        /// 작업장 조회조건
        /// </summary>
        private void InitializeConditionPopup_Area()
        {
            // 팝업 컬럼설정
            var areaPopup = Conditions.AddSelectPopup("p_areaId", new SqlQuery("GetAreaListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "AREANAME", "AREAID")
               .SetPopupLayout("AREA", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupLayoutForm(500, 600)
               .SetLabel("AREA")
               .SetPopupResultCount(1)
               .SetPosition(1.2)
               .SetRelationIds("p_plantId");

            // 팝업 조회조건
            areaPopup.Conditions.AddTextBox("AREAIDNAME")
                .SetLabel("AREAIDNAME");

            // 팝업 그리드
            areaPopup.GridColumns.AddTextBoxColumn("AREAID", 150)
                .SetValidationKeyColumn();
            areaPopup.GridColumns.AddTextBoxColumn("AREANAME", 200);
        }

        /// <summary>
        /// 품목 조회조건
        /// </summary>
        private void InitializeConditionPopup_Product()
        {
            // 팝업 컬럼설정
            var productPopup = Conditions.AddSelectPopup("p_productdefId", new SqlQuery("GetProductListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PRODUCTDEFNAME", "PRODUCTDEFIDVERSION")
               .SetPopupLayout("PRODUCT", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupLayoutForm(600, 800)
               .SetLabel("PRODUCT")
               .SetPopupAutoFillColumns("PRODUCTDEFNAME")
               .SetPopupResultCount(1)
               .SetPosition(1.3);

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

        #endregion
    }
}
