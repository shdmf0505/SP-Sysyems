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

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 공정 관리 > 공정작업 > 인계 취소
    /// 업  무  설  명  : 다중 Lot을 선택하여 일괄 인계 취소 처리 한다.
    /// 생    성    자  : 강호윤
    /// 생    성    일  : 2019-10-03
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class TransitCancel : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가

        #endregion

        #region 생성자

        public TransitCancel()
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
            InitializeInfo();
        }

        /// <summary>        
        /// 코드그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdLotList.GridButtonItem = GridButtonItem.None;
            grdLotList.ShowButtonBar = false;
            grdLotList.ShowStatusBar = false;

            grdLotList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdLotList.View.SetIsReadOnly();

            // Lot Id
            grdLotList.View.AddTextBoxColumn("LOTID", 200);
            // 공정ID
            grdLotList.View.AddTextBoxColumn("PROCESSSEGMENTID", 150)
                .SetIsHidden();
            // 공정버전
            grdLotList.View.AddTextBoxColumn("PROCESSSEGMENTVERSION", 80)
                .SetIsHidden();
            //공정순서
            grdLotList.View.AddTextBoxColumn("USERSEQUENCE", 70).SetTextAlignment(TextAlignment.Center);
            // 공정
            grdLotList.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150)
                .SetLabel("PROCESSSEGMENT");
            //작업장 ID
            grdLotList.View.AddTextBoxColumn("AREAID", 70)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsHidden();
            //작업장 명
            grdLotList.View.AddTextBoxColumn("AREANAME", 100);
            // 품목코드
            grdLotList.View.AddTextBoxColumn("PRODUCTDEFID", 150);
            // 품목버전
            grdLotList.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80)
                .SetIsHidden();
            // 품목명
            grdLotList.View.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            // UOM
            grdLotList.View.AddComboBoxColumn("UNIT", 90, new SqlQuery("GetUomDefinitionList", "10001", "UOMCLASSID=Segment"), "UOMDEFNAME", "UOMDEFID");
            // 수량
            grdLotList.View.AddSpinEditColumn("QTY", 90);
            //Pnl 수량
            grdLotList.View.AddTextBoxColumn("PANELQTY", 80)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            // 수주번호
            grdLotList.View.AddTextBoxColumn("PRODUCTIONORDERID", 150)
                           .SetIsHidden();
            // 라인
            grdLotList.View.AddTextBoxColumn("LINENO", 60)
                .SetIsHidden();
            grdLotList.View.AddTextBoxColumn("SEGMENTINCOMETIME", 130)
                .SetLabel("SENDTIME")
                .SetTextAlignment(TextAlignment.Center);

            grdLotList.View.AddTextBoxColumn("USERNAME", 100)
                .SetLabel("SENDUSER")
                .SetTextAlignment(TextAlignment.Center);

            grdLotList.View.PopulateColumns();
        }

        private void InitializeInfo()
        {
            ConditionItemSelectPopup workerCondition = new ConditionItemSelectPopup();
            workerCondition.SetPopupLayoutForm(450, 600, FormBorderStyle.SizableToolWindow);
            workerCondition.SetPopupLayout("USERLIST", PopupButtonStyles.Ok_Cancel, true, false);
            workerCondition.Id = "WORKER";
            workerCondition.SearchQuery = new SqlQuery("GetUserList", "10002", $"PLANTID={UserInfo.Current.Plant}");
            workerCondition.IsMultiGrid = false;
            workerCondition.DisplayFieldName = "WORKERNAME";
            workerCondition.ValueFieldName = "WORKERID";
            workerCondition.LanguageKey = "USER";
            workerCondition.SetPopupAutoFillColumns("WORKERNAME");

            workerCondition.Conditions.AddTextBox("USERIDNAME");

            workerCondition.GridColumns.AddTextBoxColumn("WORKERID", 150);
            workerCondition.GridColumns.AddTextBoxColumn("WORKERNAME", 200);

            txtWorker.Editor.SelectPopupCondition = workerCondition;

            txtWorker.Editor.SetValue(UserInfo.Current.Id);
            txtWorker.Editor.Text = UserInfo.Current.Name;
        }

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            pnlInfo.SizeChanged += PnlInfo_SizeChanged;
          
        }

        private void PnlInfo_SizeChanged(object sender, EventArgs e)
        {
            int labelWidth = txtWorker.Label.Width;

            tlpComment.ColumnStyles[0].Width = labelWidth;
           
        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
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

            // TODO : 저장 Rule 변경
            DataTable lotList = grdLotList.View.GetCheckedRows();

            string lotId = string.Join(",", lotList.Rows.Cast<DataRow>().Select(row => row["LOTID"].ToString()));

            var values = Conditions.GetValues();
            values.Add("LOTID", lotId);

            DataTable dt = SqlExecuter.Query("GetAcceptLotSplitCheck", "10001", values);

            if (dt.Rows.Count > 0)
            {
                throw MessageException.Create("LotSplitNoSend");

            }

            values.Add("AREAID", values["P_AREAID"]);
            values.Add("PROCESSSTATE", "WaitForSend");
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            values.Add("PLANTID", UserInfo.Current.Plant);
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            dt = SqlExecuter.Query("GetAcceptLotSplitChildCheck", "10001", values);

            if (dt.Rows.Count > 0)
            {
                 throw MessageException.Create("LotChildSplitNoSend");

            }


            dt = SqlExecuter.Query("GetLotProcessingNotSend", "10001", values);

            if (dt.Rows.Count > 0)
            {
                throw MessageException.Create("LotAreaNoSend");

            }

            DataTable processDefTypeInfo = SqlExecuter.Query("GetProcessDefTypeByProcess", "10001", values);

            string processDefType = "";
            string lastRework = "";

            if (processDefTypeInfo.Rows.Count > 0)
            {
                DataRow row = processDefTypeInfo.AsEnumerable().FirstOrDefault();

                processDefType = Format.GetString(row["PROCESSDEFTYPE"]);
                lastRework = Format.GetString(row["LASTREWORK"]);
            }

            string queryVersion = "10001";

            if (processDefType == "Rework")
                queryVersion = "10011";





            // TODO : Query Version 변경
            DataTable lotInfo = SqlExecuter.Query("SelectLotInfoByProcess", processDefType == "Rework" ? "10032" : "10031", values);

            if (lotInfo.Rows.Count < 1)
            {
                DataTable AreaLot = SqlExecuter.Query("GetLotAreaAndState", "10001", values);
                ShowMessage("NotAreaOrState", Format.GetString(AreaLot.Rows[0]["AREANAME"]), Format.GetString(AreaLot.Rows[0]["PROCESSTATE"]));

                return;
            }


            Dictionary<string, object> plantParam = new Dictionary<string, object>();
            plantParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            plantParam.Add("PLANTID", UserInfo.Current.Plant);




            string isHold = Format.GetString(lotInfo.Rows[0]["ISHOLD"]);
            string isLocking = Format.GetString(lotInfo.Rows[0]["ISLOCKING"]);

            if (isHold == "Y")
            {
                // 보류 상태의 Lot 입니다.
                throw MessageException.Create("LotIsHold", string.Format("LotId = {0}", Format.GetString(lotInfo.Rows[0]["LOTID"])));
            }

            if (isLocking == "Y")
            {
                // Locking 상태의 Lot 입니다.
                throw MessageException.Create("LotIsLocking", string.Format("LotId = {0}", Format.GetString(lotInfo.Rows[0]["LOTID"])));

           
            }



            //lotList.Rows.Cast<DataRow>().ForEach(row =>
            //{
            //    lotId += row["LOTID"].ToString() + ",";
            //});

            //lotId = lotId.Substring(0, lotId.Length - 1);

            string worker = txtWorker.Editor.SelectedData.FirstOrDefault()["WORKERID"].ToString();
            string comment = txtComment.Text;

            MessageWorker mw = new MessageWorker("SaveSendCancelLot");
            mw.SetBody(new MessageBody()
            {
                { "Worker", worker },
                { "Comment", comment },
                { "LotId", lotId }
            });

            mw.Execute();


            txtComment.Text = "";
        }

        #endregion

        #region 검색

        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            // TODO : 조회 SP 변경
            var values = Conditions.GetValues();
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            values.Add("PLANTID", UserInfo.Current.Plant);
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("PROCESSSTATE", "WaitForReceive");
            values.Add("PREVPROCESSSTATE", "WaitForSend");

            DataTable dtLotList = await QueryAsync("SelectLotListForSendCancel", "10001", values);

            if (dtLotList.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdLotList.DataSource = dtLotList;
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            CommonFunction.AddConditionProductPopup("P_PRODUCTDEFID", 0.3, false, Conditions);

            CommonFunction.AddConditionAreaByAuthorityPopup("P_AREAID", 0.1, false, Conditions,true,true);

            //Conditions.GetCondition("P_PRODUCTDEFID").SetValidationIsRequired();
            Conditions.GetCondition("P_AREAID").SetValidationIsRequired();
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

            // TODO : 유효성 로직 변경
            grdLotList.View.CheckValidation();

            DataTable changed = grdLotList.View.GetCheckedRows();

            if (changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }

            if (txtWorker.Editor.SelectedData.Count() < 1)
            {
                // 등록자는 필수 입력 항목입니다.
                throw MessageException.Create("WriterIsRequired");
            }
        }

        #endregion

        #region Private Function

        #endregion
    }
}
