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
    /// 프 로 그 램 명  : 공정관리 > LOT 관리 > LOT 인수작업장 변경
    /// 업  무  설  명  : 재공현황 중 4Step이 인수대기 및 인수상태인 LOT만 조회하여 인수작업장을 변경
    /// 생    성    자  : 정승원
    /// 생    성    일  : 2019-08-22
    /// 수  정  이  력  : 2019-10-26 정승원 작업장 -> 자원변경
    /// 
    /// 
    /// </summary>
    public partial class LotWorkAreaChange : SmartConditionManualBaseForm
    {
        #region Local Variables
        #endregion

        #region 생성자

        public LotWorkAreaChange()
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
            InitializeGridWipList();
            InitializeGridTargetWipList();

            this.ucDataUpDownBtn.SourceGrid = this.grdWIP;
            this.ucDataUpDownBtn.TargetGrid = this.grdTransArea;
        }

        /// <summary>        
        /// 재공현황 그리드를 초기화
        /// </summary>
        private void InitializeGridWipList()
        {
            grdWIP.GridButtonItem = GridButtonItem.Export;

            grdWIP.View.SetIsReadOnly();

            // CheckBox 설정
            this.grdWIP.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            var groupDefaultCol = grdWIP.View.AddGroupColumn("WIPLIST");
            groupDefaultCol.AddTextBoxColumn("LOTTYPE", 70).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("PRODUCTDEFID", 150).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            groupDefaultCol.AddTextBoxColumn("PROCESSDEFID", 150).SetIsHidden();
            groupDefaultCol.AddTextBoxColumn("PROCESSDEFVERSION", 50).SetIsHidden();
            groupDefaultCol.AddTextBoxColumn("PROCESSSTATE", 60).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("USERSEQUENCE", 70).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("PROCESSSEGMENTID", 150).SetIsHidden();
            groupDefaultCol.AddTextBoxColumn("PROCESSSEGMENTVERSION", 50).SetIsHidden();
            groupDefaultCol.AddTextBoxColumn("PROCESSSEGMENTNAME", 130);
            groupDefaultCol.AddTextBoxColumn("PLANTID", 70).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("AREAID", 150).SetIsHidden();
            groupDefaultCol.AddTextBoxColumn("AREANAME", 130);
            groupDefaultCol.AddTextBoxColumn("RTRSHT", 70).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("LOTID", 200).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("UNIT", 60).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("ISLOCKING", 60).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("ISHOLD", 60).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("RESOURCEID", 100).SetIsHidden();
            groupDefaultCol.AddTextBoxColumn("SUBPROCESSDEFID", 100).SetIsHidden();
            groupDefaultCol.AddTextBoxColumn("SUBPROCESSDEFVERSION", 100).SetIsHidden();
            groupDefaultCol.AddTextBoxColumn("LOTCREATEDTYPE", 100).SetIsHidden();

            var groupWipCol = grdWIP.View.AddGroupColumn("WIPQTY");
            groupWipCol.AddTextBoxColumn("QTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,##0}");
            groupWipCol.AddTextBoxColumn("PANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,##0}");


            var groupReceiveCol = grdWIP.View.AddGroupColumn("WAITFORRECEIVE");
            groupReceiveCol.AddTextBoxColumn("SENDPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,##0}");
            groupReceiveCol.AddTextBoxColumn("SENDPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,##0}");

            var groupWorkStartCol = grdWIP.View.AddGroupColumn("ACCEPT");
            groupWorkStartCol.AddTextBoxColumn("RECEIVEPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,##0}");
            groupWorkStartCol.AddTextBoxColumn("RECEIVEPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,##0}");

            var groupWorkEndCol = grdWIP.View.AddGroupColumn("WIPSTARTQTY");
            groupWorkEndCol.AddTextBoxColumn("WORKSTARTPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,##0}");
            groupWorkEndCol.AddTextBoxColumn("WORKSTARTPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,##0}");

            var groupSendCol = grdWIP.View.AddGroupColumn("WIPENDQTY");
            groupSendCol.AddTextBoxColumn("WORKENDPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,##0}");
            groupSendCol.AddTextBoxColumn("WORKENDPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,##0}");

            var groupWIPCol = grdWIP.View.AddGroupColumn("WIPLIST");
            groupWIPCol.AddTextBoxColumn("LEADTIME", 80).SetTextAlignment(TextAlignment.Right);
            groupWIPCol.AddTextBoxColumn("LOTINPUTDATE", 150).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("{0:yyyy-MM-dd HH:mm:ss}");
            groupWIPCol.AddTextBoxColumn("DELIVERYDATE", 150).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("{0:yyyy-MM-dd HH:mm:ss}");
            groupWIPCol.AddTextBoxColumn("LEFTDATE", 80).SetTextAlignment(TextAlignment.Center);
            groupWIPCol.AddTextBoxColumn("LOTTYPEID", 50).SetIsHidden();

            grdWIP.View.PopulateColumns();

        }

        /// <summary>
        /// 대상 LOT 그리드 초기화
        /// </summary>
        private void InitializeGridTargetWipList()
        {
            grdTransArea.GridButtonItem = GridButtonItem.None;

            grdTransArea.View.SetIsReadOnly();

            // CheckBox 설정
            this.grdTransArea.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdTransArea.View.AddTextBoxColumn("LOTTYPE", 70).SetTextAlignment(TextAlignment.Center);
            grdTransArea.View.AddTextBoxColumn("PRODUCTDEFID", 150).SetTextAlignment(TextAlignment.Center);
            grdTransArea.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetTextAlignment(TextAlignment.Center);
            grdTransArea.View.AddTextBoxColumn("PRODUCTDEFNAME", 300);
            grdTransArea.View.AddTextBoxColumn("PROCESSDEFID", 150).SetIsHidden();
            grdTransArea.View.AddTextBoxColumn("PROCESSDEFVERSION", 50).SetIsHidden();
            grdTransArea.View.AddTextBoxColumn("PROCESSSTATE", 60).SetTextAlignment(TextAlignment.Center);
            grdTransArea.View.AddTextBoxColumn("USERSEQUENCE", 70).SetTextAlignment(TextAlignment.Center);
            grdTransArea.View.AddTextBoxColumn("PROCESSSEGMENTID", 150);
            grdTransArea.View.AddTextBoxColumn("PROCESSSEGMENTVERSION", 50);
            grdTransArea.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
            grdTransArea.View.AddTextBoxColumn("PLANTID", 70).SetTextAlignment(TextAlignment.Center);
            grdTransArea.View.AddTextBoxColumn("AREAID", 150).SetIsHidden();
            grdTransArea.View.AddTextBoxColumn("AREANAME", 150);
            grdTransArea.View.AddTextBoxColumn("RTRSHT", 70).SetTextAlignment(TextAlignment.Center);
            grdTransArea.View.AddTextBoxColumn("LOTID", 200).SetTextAlignment(TextAlignment.Center);
            grdTransArea.View.AddTextBoxColumn("UNIT", 60).SetTextAlignment(TextAlignment.Center);
            grdTransArea.View.AddTextBoxColumn("ISLOCKING", 60).SetTextAlignment(TextAlignment.Center);
            grdTransArea.View.AddTextBoxColumn("ISHOLD", 60).SetTextAlignment(TextAlignment.Center);
            grdTransArea.View.AddTextBoxColumn("RESOURCEID", 100).SetIsHidden();
            grdTransArea.View.AddTextBoxColumn("SUBPROCESSDEFID", 100).SetIsHidden();
            grdTransArea.View.AddTextBoxColumn("SUBPROCESSDEFVERSION", 100).SetIsHidden();
            grdTransArea.View.AddTextBoxColumn("LOTCREATEDTYPE", 100).SetIsHidden();

            grdTransArea.View.AddTextBoxColumn("QTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            grdTransArea.View.AddTextBoxColumn("PANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");

            grdTransArea.View.AddTextBoxColumn("SENDPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            grdTransArea.View.AddTextBoxColumn("SENDPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");

            grdTransArea.View.AddTextBoxColumn("RECEIVEPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            grdTransArea.View.AddTextBoxColumn("RECEIVEPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");

            grdTransArea.View.AddTextBoxColumn("WORKSTARTPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            grdTransArea.View.AddTextBoxColumn("WORKSTARTPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");

            grdTransArea.View.AddTextBoxColumn("WORKENDPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            grdTransArea.View.AddTextBoxColumn("WORKENDPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");

            grdTransArea.View.AddTextBoxColumn("LEADTIME", 80).SetTextAlignment(TextAlignment.Right);
            grdTransArea.View.AddTextBoxColumn("LOTINPUTDATE", 150).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("{0:yyyy-MM-dd HH:mm:ss}");
            grdTransArea.View.AddTextBoxColumn("DELIVERYDATE", 150).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("{0:yyyy-MM-dd HH:mm:ss}");
            grdTransArea.View.AddTextBoxColumn("LEFTDATE", 80).SetTextAlignment(TextAlignment.Center);
            grdTransArea.View.AddTextBoxColumn("LOTTYPEID", 80).SetIsHidden();

            grdTransArea.View.PopulateColumns();
        }

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            ucDataUpDownBtn.buttonClick += UcDataUpDownBtn_buttonClick;
        }

        /// <summary>
        /// Up / Down 컨트롤 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UcDataUpDownBtn_buttonClick(object sender, EventArgs e)
        {
            DataTable dt = grdWIP.View.GetCheckedRows();
            DataTable tgdt = grdTransArea.DataSource as DataTable;

            if (this.ucDataUpDownBtn.ButtonState.Equals("Down"))
            {
                //같은 품목, 같은 버전 등 처리
                CheckedValidation();


                //작업장 조회
                if (tgdt == null || tgdt.Rows.Count <= 0)
                {
                    SetComboData();
                }
                else
                {
                    #region 동일 품목,공정, 수순, Site, LotType, ProcessState Validation
                    //품목
                    string productDefid = Format.GetString(dt.AsEnumerable().Select(r => Format.GetString("PRODUCTDEFID")).Distinct().FirstOrDefault());
                    string tgProductDefid = Format.GetString(tgdt.AsEnumerable().Select(r => Format.GetString("PRODUCTDEFID")).Distinct().FirstOrDefault());
                    string productDefVersion = Format.GetString(dt.AsEnumerable().Select(r => Format.GetString("PRODUCTDEFVERSION")).Distinct().FirstOrDefault());
                    string tgProductDefVersion = Format.GetString(tgdt.AsEnumerable().Select(r => Format.GetString("PRODUCTDEFVERSION")).Distinct().FirstOrDefault());

                    //공정
                    string processsegmentId = Format.GetString(dt.AsEnumerable().Select(r => Format.GetString("PROCESSSEGMENTID")).Distinct().FirstOrDefault());
                    string tgProcesssegmentId = Format.GetString(tgdt.AsEnumerable().Select(r => Format.GetString("PROCESSSEGMENTID")).Distinct().FirstOrDefault());
                    string processsegmentVersion = Format.GetString(dt.AsEnumerable().Select(r => Format.GetString("PROCESSSEGMENTVERSION")).Distinct().FirstOrDefault());
                    string tgProcesssegmentVersion = Format.GetString(tgdt.AsEnumerable().Select(r => Format.GetString("PROCESSSEGMENTVERSION")).Distinct().FirstOrDefault());

                    //공정 수순
                    int userSequence = Format.GetInteger(dt.AsEnumerable().Select(r => Format.GetString(r["USERSEQUENCE"])).Distinct().FirstOrDefault());
                    int tgUserSequence = Format.GetInteger(tgdt.AsEnumerable().Select(r => Format.GetString(r["USERSEQUENCE"])).Distinct().FirstOrDefault());

                    //Site
                    string plantId = Format.GetString(dt.AsEnumerable().Select(r => Format.GetString("PLANTID")).Distinct().FirstOrDefault());
                    string tgPlantId = Format.GetString(tgdt.AsEnumerable().Select(r => Format.GetString("PLANTID")).Distinct().FirstOrDefault());

                    //양산구분
                    string lotType = Format.GetString(dt.AsEnumerable().Select(r => Format.GetString("LOTTYPEID")).Distinct().FirstOrDefault());
                    string tgLotType = Format.GetString(tgdt.AsEnumerable().Select(r => Format.GetString("LOTTYPEID")).Distinct().FirstOrDefault());

                    //Process State
                    string processState = Format.GetString(dt.AsEnumerable().Select(r => Format.GetString("PROCESSSTATE")).Distinct().FirstOrDefault());
                    string tgProcessState = Format.GetString(tgdt.AsEnumerable().Select(r => Format.GetString("PROCESSSTATE")).Distinct().FirstOrDefault());


                    if (!productDefid.Equals(tgProductDefid) || !productDefVersion.Equals(tgProductDefVersion))
                    {
                        //같은 품목끼리만 작업장 변경이 가능합니다.
                        ShowMessage("NotAddTransAreaList", string.Format("{0}, {1}", Language.Get("PRODUCTDEF"), Language.Get("PRODUCTDEFVERSION")));
                        grdWIP.View.CheckedAll(false);
                        return;
                    }
                    else if (!processsegmentId.Equals(tgProcesssegmentId) || !processsegmentVersion.Equals(tgProcesssegmentVersion))
                    {
                        //같은 공정끼리만 작업장 변경이 가능합니다.
                        ShowMessage("NotAddTransAreaList", string.Format("{0}, {1}", Language.Get("PROCESSSEGMENT"), Language.Get("PROCESSSEGMENTVERSION")));
                        grdWIP.View.CheckedAll(false);
                        return;
                    }
                    else if (!userSequence.Equals(tgUserSequence))
                    {
                        //같은 수순끼리만 작업장 변경이 가능합니다.
                        ShowMessage("NotAddTransAreaList", Language.Get("USERSEQUENCE"));
                        grdWIP.View.CheckedAll(false);
                        return;
                    }
                    else if (!plantId.Equals(tgPlantId))
                    {
                        //같은 Site끼리만 작업장 변경이 가능합니다.
                        ShowMessage("NotAddTransAreaList", Language.Get("SITE"));
                        grdWIP.View.CheckedAll(false);
                        return;
                    }
                    else if (!lotType.Equals(tgLotType))
                    {
                        //같은 양산구분끼리만 작업장 변경이 가능합니다.
                        ShowMessage("NotAddTransAreaList", Language.Get("PRODUCTIONTYPE"));
                        grdWIP.View.CheckedAll(false);
                        return;
                    }
                    /*
					else if (!processState.Equals(tgProcessState))
					{
						//같은 Process State끼리만 작업장 변경이 가능합니다.
						ShowMessage("NotAddTransAreaList", Language.Get("PROCESSSTATE"));
						grdWIP.View.CheckedAll(false);
						return;
					}
					*/
                    #endregion

                    SetComboData();
                }
            }
            else
            {
                //재공 그리드에 있는 LOT의 작업장 + 대상 LOT 그리드에 있는 LOT의 작업장을 제외한 대상 작업장 조회
                SetComboData();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buttonState"></param>
        /// <param name="dt"></param>
        /// <param name="tgdt"></param>
        private void SetComboData()
        {
            /*
			DataTable dt = grdWIP.View.GetCheckedRows();
			DataTable tgdt = grdTransArea.DataSource as DataTable;
			string buttonState = ucDataUpDownBtn.ButtonState;

			//대상 작업장 변수
			DataTable dataTable = new DataTable();
			string areaId = string.Empty;
			Dictionary<string, object> param = new Dictionary<string, object>();


			if ((tgdt == null || tgdt.Rows.Count == 0) && buttonState.Equals("Down")){	dataTable = dt.Copy();	}
			else {	dataTable = tgdt.Copy();	}

			switch(buttonState)
			{
				case "Up":

					//Up 버튼 눌렀을 때 체크 안된(제외 할) LOT만 조회

					//대상 Lot에 데이터가 없으면 조회X
					int checkedCount = grdTransArea.View.GetCheckedRows().Rows.Count;
					if (grdTransArea.View.RowCount.Equals(checkedCount))
					{
						cboTargetArea.Editor.DataSource = null;
						return;
					}
						
					for (int i = 0; i < grdTransArea.View.RowCount; i++)
					{
						if (!grdTransArea.View.IsRowChecked(i))
							areaId += Format.GetString(grdTransArea.View.GetRowCellValue(i, "AREAID")) + ",";
					}

					break;
				case "Down":

					if (tgdt != null && tgdt.Rows.Count > 1)
					{
						tgdt.AsEnumerable().ForEach(r => {
							areaId += Format.GetString(r["AREAID"]) + ",";
						});
					}

					dt.AsEnumerable().ForEach(r =>
					{
						areaId += Format.GetString(r["AREAID"]) + ",";
					});

					break;
			}

			param.Add("PLANTID", dataTable.Rows[0]["PLANTID"]);
			param.Add("AREAID", areaId);
			param.Add("PRODUCTDEFID", dataTable.Rows[0]["PRODUCTDEFID"]);
			param.Add("PRODUCTDEFVERSION", dataTable.Rows[0]["PRODUCTDEFVERSION"]);
			param.Add("PROCESSSEGMENTID", dataTable.Rows[0]["PROCESSSEGMENTID"]);
			param.Add("PROCESSSEGMENTVERSION", dataTable.Rows[0]["PROCESSSEGMENTVERSION"]);
			param.Add("PROCESSDEFID", dataTable.Rows[0]["PROCESSDEFID"]);
			param.Add("PROCESSDEFVERSION", dataTable.Rows[0]["PROCESSDEFVERSION"]);
			param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
			*/



            DataTable dt = grdWIP.View.GetCheckedRows();
            DataTable tgdt = grdTransArea.DataSource as DataTable;
            string buttonState = ucDataUpDownBtn.ButtonState;

            //대상 작업장 변수
            DataTable dataTable = new DataTable();
            string resourceId = string.Empty;
            Dictionary<string, object> param = new Dictionary<string, object>();


            if ((tgdt == null || tgdt.Rows.Count == 0) && buttonState.Equals("Down")) { dataTable = dt.Copy(); }
            else { dataTable = tgdt.Copy(); }

            switch (buttonState)
            {
                case "Up":

                    //Up 버튼 눌렀을 때 체크 안된(제외 할) LOT만 조회

                    //대상 Lot에 데이터가 없으면 조회X
                    int checkedCount = grdTransArea.View.GetCheckedRows().Rows.Count;
                    if (grdTransArea.View.RowCount.Equals(checkedCount))
                    {
                        cboTargetArea.Editor.DataSource = null;
                        return;
                    }

                    for (int i = 0; i < grdTransArea.View.RowCount; i++)
                    {
                        if (!grdTransArea.View.IsRowChecked(i))
                            resourceId += Format.GetString(grdTransArea.View.GetRowCellValue(i, "RESOURCEID")) + ",";
                    }

                    break;
                case "Down":

                    if (tgdt != null && tgdt.Rows.Count > 1)
                    {
                        tgdt.AsEnumerable().ForEach(r => {
                            resourceId += Format.GetString(r["RESOURCEID"]) + ",";
                        });
                    }

                    dt.AsEnumerable().ForEach(r =>
                    {
                        resourceId += Format.GetString(r["RESOURCEID"]) + ",";
                    });

                    break;
            }

            string processDefid = string.Empty;
            string processDefversion = string.Empty;
            string Productdefid = string.Empty;
            string Productdefversion = string.Empty;
            string subprocessDefid = Format.GetFullTrimString(dataTable.Rows[0]["SUBPROCESSDEFID"]);
            string subprocessDefversion = Format.GetFullTrimString(dataTable.Rows[0]["SUBPROCESSDEFVERSION"]);
            string lotCreateType = Format.GetFullTrimString(dataTable.Rows[0]["LOTCREATEDTYPE"]);

            if (subprocessDefid.Equals(string.Empty) && !lotCreateType.Equals("Return"))
            {
                processDefid = Format.GetFullTrimString(dataTable.Rows[0]["PROCESSDEFID"]);
                processDefversion = Format.GetFullTrimString(dataTable.Rows[0]["PROCESSDEFVERSION"]);
                Productdefid = Format.GetFullTrimString(dataTable.Rows[0]["PRODUCTDEFID"]);
                Productdefversion = Format.GetFullTrimString(dataTable.Rows[0]["PRODUCTDEFVERSION"]);
            }
            else if (!string.IsNullOrWhiteSpace(subprocessDefid))
            {
                processDefid = subprocessDefid;
                processDefversion = subprocessDefversion;
            }
            else if (lotCreateType.Equals("Return"))
            {
                processDefid = Format.GetFullTrimString(dataTable.Rows[0]["PROCESSDEFID"]);
                processDefversion = Format.GetFullTrimString(dataTable.Rows[0]["PROCESSDEFVERSION"]);
                //Productdefid = "*";
                //Productdefversion = "*";
            }
            param.Add("PLANTID", dataTable.Rows[0]["PLANTID"]);
            param.Add("RESOURCEID", resourceId);
            param.Add("LOTID", dataTable.Rows[0]["LOTID"]);
            if (!string.IsNullOrWhiteSpace(Productdefid))
            {
                param.Add("PRODUCTDEFID", Productdefid);
                param.Add("PRODUCTDEFVERSION", Productdefversion);
            }
            param.Add("PROCESSSEGMENTID", dataTable.Rows[0]["PROCESSSEGMENTID"]);
            param.Add("PROCESSSEGMENTVERSION", dataTable.Rows[0]["PROCESSSEGMENTVERSION"]);
            param.Add("PROCESSDEFID", processDefid);
            param.Add("PROCESSDEFVERSION", processDefversion);
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);


            //대상 LOT 콤보 초기화(조회)
            InitializeComboBox(param);
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

            DataTable dt = grdTransArea.DataSource as DataTable;

            string transResourceId = Format.GetString(cboTargetArea.Editor.GetDataValue()).Split('|')[0];
            string transAreaId = Format.GetString(cboTargetArea.Editor.GetDataValue()).Split('|')[1];

            MessageWorker worker = new MessageWorker("SaveChangeArea");
            worker.SetBody(new MessageBody()
            {
                { "enterpriseId", UserInfo.Current.Enterprise },
                { "plantId", UserInfo.Current.Plant },
                { "transResourceId", transResourceId },
                { "transAreaId", transAreaId },
                { "transAreaList" , dt },
                { "uiSegment", dt.Rows[0]["PROCESSSEGMENTID"] }
            });

            worker.Execute();

            grdTransArea.View.ClearDatas();
        }

        #endregion

        #region 검색

        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            //초기화
            this.grdWIP.View.ClearDatas(); ;
            this.grdTransArea.View.ClearDatas();
            this.cboTargetArea.Editor.DataSource = null;

            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable dtWipTransAreaList = await QueryAsync("SelectWipTransAreaList", "10002", values);
            if (dtWipTransAreaList.Rows.Count == 0)
            {
                //조회할 데이터가 없습니다.
                ShowMessage("NoSelectData");
            }

            grdWIP.DataSource = dtWipTransAreaList;


        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용

            //Lot
            CommonFunction.AddConditionLotPopup("P_LOTID", 0.1, true, Conditions);
            //품목코드
            InitializeCondition_ProductPopup();
            //CommonFunction.AddConditionProductPopup("P_PRODUCTDEFID", 0.5, true, Conditions);
            //작업장
            InitializeConditionAreaId_Popup();
            //작업공정
            CommonFunction.AddConditionProcessSegmentPopup("P_PROCESSSEGMENT", 0.9, true, Conditions);
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            param.Add("PLANTID", UserInfo.Current.Plant);

            DataTable isWipSurveyResult = SqlExecuter.Query("GetUserArea", "10001", param);

            if (isWipSurveyResult.Rows[0]["AREARESPONSIBILITY"].Equals("RestrictUser"))
            {

                // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
                SmartSelectPopupEdit lot = Conditions.GetControl<SmartSelectPopupEdit>("P_LOTID");
                
                Conditions.GetCondition("P_LOTID").SetValidationIsRequired();
             




            }

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

            DataTable savedData = grdTransArea.DataSource as DataTable;

            if (savedData.Rows.Count < 1)
            {
                //저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }

            object targetArea = cboTargetArea.Editor.GetDataValue();
            if (targetArea == null)
            {
                //대상 작업장이 선택되지 않았습니다.
                throw MessageException.Create("NoSelectTargetArea");
            }
        }

        #endregion

        #region Private Function
        private void InitializeCondition_ProductPopup()
        {
            // SelectPopup 항목 추가
            var conditionProductId = Conditions.AddSelectPopup("P_PRODUCTDEFID", new SqlQuery("GetProductDefinitionList", "10002", $"PLANTID={UserInfo.Current.Plant}"), "PRODUCTDEF", "PRODUCTDEF")
                .SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, true)
                .SetPopupLayoutForm(800, 800)
                .SetPopupAutoFillColumns("PRODUCTDEFNAME")
                .SetLabel("PRODUCTDEFID")
                .SetPosition(1.2)
                .SetPopupResultCount(0)
                .SetPopupApplySelection((selectRow, gridRow) => {
                    string productDefName = "";

                    selectRow.AsEnumerable().ForEach(r => {
                        productDefName += Format.GetString(r["PRODUCTDEFNAME"]) + ",";
                    });

                    productDefName = productDefName.TrimEnd(',');

                    Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").EditValue = productDefName;
                });

            // 팝업에서 사용되는 검색조건 (품목코드/명)
            conditionProductId.Conditions.AddTextBox("TXTPRODUCTDEFNAME");
            //제품 구분 추가 2019.08.14 배선용, 노석안 대리 요청
            conditionProductId.Conditions.AddComboBox("PRODUCTDIVISION", new SqlQuery("GetTypeList", "10001", $"CODECLASSID=ProductDivision2", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                .SetDefault("Product");

            // 품목코드
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            // 품목명
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            // 품목버전
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 100);
            // 품목유형구분
            conditionProductId.GridColumns.AddComboBoxColumn("PRODUCTDEFTYPE", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductDefType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            // 생산구분
            conditionProductId.GridColumns.AddComboBoxColumn("PRODUCTIONTYPE", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            // 단위
            conditionProductId.GridColumns.AddComboBoxColumn("UNIT", 90, new SqlQuery("GetUomDefinitionList", "10001", "UOMCLASSID=Segment"), "UOMDEFNAME", "UOMDEFID");
        }

        /// <summary>
        /// 팝업형 조회조건 생성 - 작업장
        /// </summary>
        private void InitializeConditionAreaId_Popup()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("AreaType", "Area");
            param.Add("PlantId", UserInfo.Current.Plant);
            param.Add("LanguageType", UserInfo.Current.LanguageType);
            param.Add("AREA", UserInfo.Current.Area);

            var areaIdPopup = this.Conditions.AddSelectPopup("P_AREAID", new SqlQuery("GetAreaList", "10003", param), "AREANAME", "AREAID")
                .SetPopupLayout(Language.Get("SELECTAREAID"), PopupButtonStyles.Ok_Cancel, true, true)
                .SetPopupResultCount(1)
                .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                .SetPopupAutoFillColumns("AREANAME")
                .SetLabel("AREA")
                .SetPosition(0.7);

            areaIdPopup.Conditions.AddTextBox("AREA")
                .SetLabel("TXTAREA");

            areaIdPopup.GridColumns.AddTextBoxColumn("AREAID", 150);
            areaIdPopup.GridColumns.AddTextBoxColumn("AREANAME", 200);
        }
        /// <summary>
        /// 동일 품목, 공정, 수순 등 체크
        /// </summary>
        private void CheckedValidation()
        {
            DataTable selected = grdWIP.View.GetCheckedRows();

            //품목코드
            int productDefIdCount = selected.AsEnumerable().Select(r => Format.GetString(r["PRODUCTDEFID"])).Distinct().Count();
            if (productDefIdCount > 1)
            {
                //같은 품목끼리만 선택하여 주십시오.
                grdWIP.View.CheckedAll(false);
                throw MessageException.Create("MixSelectProductDefID");
            }

            //품목버전
            int productDefVerCount = selected.AsEnumerable().Select(r => Format.GetString(r["PRODUCTDEFVERSION"])).Distinct().Count();
            if (productDefVerCount > 1)
            {
                //같은 품목버전끼리만 선택하여 주십시오.
                grdWIP.View.CheckedAll(false);
                throw MessageException.Create("MixSelectProductDefVersion");
            }

            //공정ID
            int processsegmentIdCount = selected.AsEnumerable().Select(r => Format.GetString(r["PROCESSSEGMENTID"])).Distinct().Count();
            if (processsegmentIdCount > 1)
            {
                //같은 공정, 버전끼리만 선택하여 주십시오.
                grdWIP.View.CheckedAll(false);
                throw MessageException.Create("MixSelectProcesssegmentname");
            }

            //공정버전
            int processsegmentVerCount = selected.AsEnumerable().Select(r => Format.GetString(r["PROCESSSEGMENTVERSION"])).Distinct().Count();
            if (processsegmentVerCount > 1)
            {
                //같은 공정, 버전끼리만 선택하여 주십시오.
                grdWIP.View.CheckedAll(false);
                throw MessageException.Create("MixSelectProcesssegmentname");
            }

            //공정수순
            int usersequenceCount = selected.AsEnumerable().Select(r => Format.GetString(r["USERSEQUENCE"])).Distinct().Count();
            if (usersequenceCount > 1)
            {
                //같은 수순끼리만 선택하여 주십시오
                grdWIP.View.CheckedAll(false);
                throw MessageException.Create("MixProcessPath");
            }

            //Plant Id
            int plantIdCount = selected.AsEnumerable().Select(r => Format.GetString(r["PLANTID"])).Distinct().Count();
            if (plantIdCount > 1)
            {
                //같은 Site끼리만 선택하여 주십시오
                grdWIP.View.CheckedAll(false);
                throw MessageException.Create("MixSelectPlantID");
            }

            //LOT Type
            int lotTypeCount = selected.AsEnumerable().Select(r => Format.GetString(r["LOTTYPEID"])).Distinct().Count();
            if (lotTypeCount > 1)
            {
                //같은 양산구분끼리만 선택하여 주십시오
                grdWIP.View.CheckedAll(false);
                throw MessageException.Create("MixSelectLotType");
            }

            /*
			//Process State
			int processStateCount = selected.AsEnumerable().Select(r => Format.GetString(r["PROCESSSTATE"])).Distinct().Count();
			if (processStateCount > 1)
			{
				//같은 Process State끼리만 선택하여 주십시오
				grdWIP.View.CheckedAll(false);
				throw MessageException.Create("SelectSameItem", Language.Get("PROCESSSTATE"));
			}
			*/
        }

        /// <summary>
        /// 대상 작업장 ComboBox 초기화
        /// </summary>
        private void InitializeComboBox(Dictionary<string, object> param)
        {
            //cboTargetArea.Editor.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            //cboTargetArea.Editor.ValueMember = "AREAID";
            //cboTargetArea.Editor.DisplayMember = "AREANAME";
            //cboTargetArea.Editor.ShowHeader = false;

            //DataTable dtTargetAreaList = SqlExecuter.Query("GetTransAreaList", "10001", param);
            //cboTargetArea.Editor.DataSource = dtTargetAreaList;

            cboTargetArea.Editor.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboTargetArea.Editor.ValueMember = "RESOURCE";
            cboTargetArea.Editor.DisplayMember = "RESOURCENAME";
            cboTargetArea.Editor.ShowHeader = false;

            DataTable dtTargetAreaList = SqlExecuter.Query("GetTransAreaList", "10021", param);
            cboTargetArea.Editor.DataSource = dtTargetAreaList;

            if (dtTargetAreaList.Rows.Count > 0)
            {
                cboTargetArea.Editor.ItemIndex = 0;
            }
        }

        #endregion
    }
}
