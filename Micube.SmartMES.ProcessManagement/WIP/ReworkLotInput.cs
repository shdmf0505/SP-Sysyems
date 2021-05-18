#region using
using DevExpress.XtraGrid.Views.Grid;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons;
using Micube.SmartMES.Commons.Controls;

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms; 
#endregion


namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 공정관리 > 공정작업 > 재작업 Lot 투입
    /// 업  무  설  명  : 재작업 Lot 투입
    /// 생    성    자  : 황유성
    /// 생    성    일  : 2019-10-14
    /// 수  정  이  력  : 2019-10-23, 재작업 라우팅 선택 팝업에서 라우팅 패스 표시
    ///                  2019-12-26, 박정훈, Source Region 및 주석처리(위치 재배치)
    /// 
    /// 
    /// </summary>
    public partial class ReworkLotInput : SmartConditionManualBaseForm
    {
        #region ◆ Variables |
        private string lotId;
        private ReworkRoutingPopup reworkPopup = new ReworkRoutingPopup();
        #endregion

        #region ◆ 생성자 |
        /// <summary>
        /// 생성자
        /// </summary>
        public ReworkLotInput()
        {
            InitializeComponent();
        }
        #endregion

        #region ◆ Control 초기화 |
        /// <summary>
        /// 초기화
        /// </summary>
        protected override void InitializeContent()
        {
            base.InitializeContent();

            UseAutoWaitArea = false;

            InitializeControls();
            InitializeEvent();
        }

        #region ▶ 컨트롤 초기화 |
        /// <summary>
        /// 컨트롤 초기화
        /// </summary>
        private void InitializeControls()
        {
            grdLotInfo.ColumnCount = 5;
            grdLotInfo.LabelWidthWeight = "40%";
            grdLotInfo.SetInvisibleFields("PROCESSPATHID", "PROCESSSEGMENTID", "PROCESSSEGMENTVERSION", "PRODUCTDEFVERSION", "PRODUCTTYPE", "DEFECTUNIT"
                , "PANELPERQTY", "PCSPNL", "PROCESSSEGMENTTYPE", "STEPTYPE", "DURABLEDEFID", "PROCESSSTATE", "ISREWORK", "ISLOCKING", "ISHOLD", "RESOURCEID", "PRODUCTIONTYPE");
            Clear();
        }
        #endregion

        #endregion

        #region ◆ 이벤트 초기화 |
        /// <summary>
        /// 이벤트 초기화
        /// </summary>
        private void InitializeEvent()
        {
            this.Shown += ReworkLotInput_Shown;

            // Button
            btnInit.Click += BtnInit_Click;

            // TextBox
            txtLotId.Editor.KeyDown += TxtLotId_KeyDown;
        }

        private void ReworkLotInput_Shown(object sender, EventArgs e)
        {
            ucReworkRouting.SplitterPosition = this.Width / 2;
            ucReworkRouting.Clear();
        }

        #region ▶ Button Event |
        /// <summary>
        /// 초기화 버튼 클릭 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnInit_Click(object sender, EventArgs e)
        {
            Clear();
            txtLotId.Text = string.Empty;
        }
        #endregion

        #region ▶ TextBox Event |
        /// <summary>
        /// LOT ID 텍스트 박스에서 엔터키 입력시 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtLotId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string lotId = txtLotId.Text;

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("LOT_ID", lotId);

                DataTable lotState = SqlExecuter.Query("GetLotstateCheck", "00001", param);

                if (lotState.Rows.Count > 0)
                {
                    throw MessageException.Create("LotStateWaitForReceive");
                }

                Clear();
                this.lotId = lotId;
                SearchLotInfo(lotId);
                ucReworkRouting.Apply(this.lotId);
            }
        } 
        #endregion
        
        #endregion

        #region ◆ 저장 버튼 Click |
        /// <summary>
        /// 저장버튼 클릭 시 이벤트 처리
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            // 재공실사 진행 여부 체크
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            param.Add("PLANTID", UserInfo.Current.Plant);

            DataTable isWipSurveyResult = SqlExecuter.Query("GetPlantIsWipSurvey", "10001", param);

            if (isWipSurveyResult.Rows.Count > 0)
            {
                DataRow row = isWipSurveyResult.AsEnumerable().FirstOrDefault();

                string isWipSurvey = Format.GetString(row["ISWIPSURVEY"]);

                if (isWipSurvey == "Y")
                {
                    // 재공실사가 진행 중 입니다. {0}을 진행할 수 없습니다.
                    ShowMessage("PLANTINWIPSURVEY", Language.Get(string.Join("_", "MENU", MenuId)));

                    return;
                }
            }

            if (lotId == null)
            {
                // 해당 Lot이 존재하지 않습니다. {0}
                throw MessageException.Create("NotExistLot");
            }

            ucReworkRouting.FillResultProperties();

            try
            {
                pnlContent.ShowWaitArea();
                MessageWorker worker = new MessageWorker("SaveLotRework");
                worker.SetBody(new MessageBody()
                {
                    { "lotid", lotId }
                    , { "reworkprocessdefid", ucReworkRouting.ReworkProcessDefId }
                    , { "reworkprocessdefversion", ucReworkRouting.ReworkProcessDefVersion }
                    , { "returnprocesspathid", ucReworkRouting.ReturnProcessPathId }
                    , { "returnresourceid", ucReworkRouting.ReturnResourceId }
                    , { "returnareaid", ucReworkRouting.ReturnAreaId }
                    , { "resourceid", ucReworkRouting.ResourceId }
                    , { "areaid", ucReworkRouting.AreaId }
                    , { "toresourceid" ,ucReworkRouting.ToResourceId }
                    , { "toprocesspathid", ucReworkRouting.ToProcessPathId }
                    , { "toareaid", ucReworkRouting.ToAreaId }
                    , { "enterpriseid", UserInfo.Current.Enterprise }
                    , { "plantid", UserInfo.Current.Plant }
                    , { "isproductrouting", ucReworkRouting.IsProductRouting ? "Y" : "N" }
                });
                worker.Execute();
            }
            catch (Exception ex)
            {
                throw MessageException.Create(ex.Message);
            }
            finally
            {
                pnlContent.CloseWaitArea();
            }

            if (!ucReworkRouting.IsProductRouting)
            {
                Commons.CommonFunction.PrintLotCard_Ver2(lotId, LotCardType.Rework);
            }

            Clear();
        }
        #endregion

        #region ◆ Private Function |

        #region ▶ Clear :: 화면 클리어 |
        /// <summary>
        /// 화면 클리어
        /// </summary>
        private void Clear()
        {
            lotId = null;
            txtLotId.Text = string.Empty;
            grdLotInfo.ClearData();
            ucReworkRouting.Clear();
        }
        #endregion

        #region ▶ SearchLotInfo :: LOT 정보 조회 |
        /// <summary>
        /// LOT 정보 조회
        /// </summary>
        private void SearchLotInfo(string inputLotId)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            param.Add("PLANTID", UserInfo.Current.Plant);
            param.Add("LOTID", inputLotId);
            param.Add("ISREWORK", "N");
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable lotInfo = SqlExecuter.Query("SelectLotInfoBylotIDbyAreaAuthority", "10001", param);

            if (lotInfo.Rows.Count > 0)
            {
                if (lotInfo.Rows[0]["ISREWORK"].ToString() == "Y")
                {
                    lotId = null;
                    grdLotInfo.DataSource = lotInfo.Clone();
                    ucReworkRouting.Clear();
                    // 재작업중인 LOT을 다시 재작업 할 수 없습니다.
                    this.ShowMessage("LotAlreadyIsRework");
                }
                else if (lotInfo.Rows[0]["PROCESSSTATE"].ToString() == "Run")
                {
                    lotId = null;
                    grdLotInfo.DataSource = lotInfo.Clone();
                    ucReworkRouting.Clear();
                    // 공정진행 상태가 Run인 LOT은 재작업 할 수 없습니다. {0}
                    this.ShowMessage("LotProcessStateIsRun", inputLotId);
                }
                else if (lotInfo.Rows[0]["ISHOLD"].ToString() == "Y")
                {
                    lotId = null;
                    grdLotInfo.DataSource = lotInfo.Clone();
                    ucReworkRouting.Clear();
                    // 보류 상태의 Lot 입니다. {0}
                    this.ShowMessage("LotIsHold", inputLotId);
                }
                else
                {
                    lotId = lotInfo.Rows[0]["LOTID"].ToString();
                    grdLotInfo.DataSource = lotInfo;
                }
            }
            else
            {
                // TODO : Inner Join 조건 확인(존재하지만 공정이 없는 LOT도 조회 안됨)
                lotId = null;
                grdLotInfo.DataSource = lotInfo;
                ucReworkRouting.Clear();
                // 해당 Lot이 존재하지 않습니다. {0}
                this.ShowMessage("NotExistLot", string.Format("LotId = {0}", inputLotId));
            }
        }
        #endregion

        #endregion
    }
}
