#region using

using DevExpress.XtraReports.UI;

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
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion

namespace Micube.SmartMES.ProcessManagement
{
	/// <summary>
	/// 프 로 그 램 명  : 공정관리 > 투입관리 > LOT CARD 출력
	/// 업  무  설  명  : LOT CARD 출력하는 화면
	/// 생    성    자  : 정승원
	/// 생    성    일  : 2019-06-21
	/// 수  정  이  력  : 2019-06-27 Lot Card Print Logic 추가
	/// 
	/// 
	/// </summary>
	public partial class PrintLotCard : SmartConditionManualBaseForm
	{
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가
        int _printProcessRowPerPage = 25;

		#endregion

		#region 생성자

		public PrintLotCard()
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
			grdLotList.GridButtonItem = GridButtonItem.None;
            grdLotList.ShowButtonBar = false;
            grdLotList.ShowStatusBar = false;

			grdLotList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdLotList.View.SetIsReadOnly();

            //대공정
            /*
            2019.07.31 대공정,중공정 삭제 배선용 (최창선 과장 요청)
			grdLotList.View.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME_TOP", 200)
				.SetLabel("TOPPROCESSSEGMENTCLASS");
			//중공정
			grdLotList.View.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME_MIDDLE", 200)
				.SetLabel("MIDDLEPROCESSSEGMENTCLASS");
            */
            grdLotList.View.AddTextBoxColumn("PRODUCTIONORDERID", 120);
            grdLotList.View.AddTextBoxColumn("PLANTID", 60).SetTextAlignment(TextAlignment.Center);
            //LOT 생성 타입
            grdLotList.View.AddTextBoxColumn("WORKTYPE", 100).SetTextAlignment(TextAlignment.Center).SetIsHidden();
            grdLotList.View.AddTextBoxColumn("WORKTYPENAME", 60).SetTextAlignment(TextAlignment.Center).SetLabel("WORKTYPE");
            //작업장
            grdLotList.View.AddTextBoxColumn("AREANAME", 120);
			//품목코드
			grdLotList.View.AddTextBoxColumn("PRODUCTDEFID", 110);
            //버전
            grdLotList.View.AddTextBoxColumn("PRODUCTDEFVERSION", 60).SetLabel("ITEMVERSION");
            //품목명
            grdLotList.View.AddTextBoxColumn("PRODUCTDEFNAME", 150);
			//LOT#
			grdLotList.View.AddTextBoxColumn("LOTID", 150);
			//작업공정
			grdLotList.View.AddTextBoxColumn("USERSEQUENCE", 80).SetTextAlignment(TextAlignment.Center);
			//공정명
			grdLotList.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 100).SetTextAlignment(TextAlignment.Center);
			//UOM
			grdLotList.View.AddTextBoxColumn("UOM", 60).SetTextAlignment(TextAlignment.Center);
			//PNLs
			grdLotList.View.AddSpinEditColumn("PNL", 60).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetTextAlignment(TextAlignment.Right);
			//PCS
			grdLotList.View.AddSpinEditColumn("PCS", 60).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true).SetTextAlignment(TextAlignment.Right);
			//MM
			grdLotList.View.AddSpinEditColumn("MM", 60).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true).SetTextAlignment(TextAlignment.Right);
			//투입일자
			grdLotList.View.AddSpinEditColumn("INPUTDATE", 120);

            grdLotList.View.PopulateColumns();
		}

		#endregion

		#region Event

		/// <summary>
		/// 화면에서 사용되는 이벤트를 초기화한다.
		/// </summary>
		public void InitializeEvent()
		{
			// TODO : 화면에서 사용할 이벤트 추가
			btnPrint.Click += BtnPrint_Click;
		}

		/// <summary>
		/// LOT CARD 출력
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnPrint_Click(object sender, EventArgs e)
		{
            PrintReport();
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
			values.Add("PLANTID", UserInfo.Current.Plant);
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

			DataTable dtPlantLotCard = await QueryAsyncDirect("SelectPrintLotCardList", "10001", values);

			if (dtPlantLotCard.Rows.Count < 1) // 
			{
				ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
			}

			grdLotList.DataSource = dtPlantLotCard;
		}

		/// <summary>
		/// 조회조건 항목을 추가한다.
		/// </summary>
		protected override void InitializeCondition()
		{
			base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            //InitializeConditionLotId_Popup();
            //InitializeConditionProductDefinitionId_Popup();
            CommonFunction.AddConditionLotPopup("p_lotId", 1.1, false, Conditions);
            CommonFunction.AddConditionProductPopup("p_productDefId", 1.2, true, Conditions, "PRODUCTDEFID", "PRODUCTDEF", false, 0, false);

            CommonFunction.AddConditionAreaByAuthorityPopup("P_AREAID", 3.1, false, Conditions, false, false);                 // 작업장

            InitializeConditionProductOrderId_Popup();

            Conditions.GetCondition<ConditionItemComboBox>("P_PRODUCTDEFVERSION").SetIsHidden();

            Conditions.GetCondition<ConditionItemSelectPopup>("P_PRODUCTDEFID").SetPopupApplySelection((selectedRows, dataGridRows) =>
            {
                string productDefName = ""; //Format.GetString(selectedRows.FirstOrDefault()["PRODUCTDEFNAME"]);

                selectedRows.ForEach(r =>
                {
                    productDefName += Format.GetString(r["PRODUCTDEFNAME"]) + ",";
                });

                productDefName.TrimEnd(',');
                /*
                List<DataRow> list = selectedRows.ToList<DataRow>();

                List<string> listRev = new List<string>();

                string productlist = string.Empty;
                selectedRows.ForEach(row =>
                {
                    string productid = Format.GetString(row["PRODUCTDEFID"]);
                    string revision = Format.GetString(row["PRODUCTDEFVERSION"]);
                    productlist = productlist + productid + ',';
                    listRev.Add(revision);
                }
                );

                productlist = productlist.TrimEnd(',');

                listRev = listRev.Distinct().ToList();
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                param.Add("P_PLANTID", UserInfo.Current.Plant);
                param.Add("P_PRODUCTDEFID", productlist);

                DataTable dt = SqlExecuter.Query("selectProductdefVesion", "10001", param);


                Conditions.GetControl<SmartComboBox>("P_PRODUCTDEFVERSION").DataSource = dt;
                if (listRev.Count > 0)
                {
                    if (listRev.Count == 1)
                        Conditions.GetControl<SmartComboBox>("P_PRODUCTDEFVERSION").EditValue = Format.GetFullTrimString(listRev[0]);
                    else
                        Conditions.GetControl<SmartComboBox>("P_PRODUCTDEFVERSION").EditValue = Format.GetFullTrimString('*');
                }
                */
                Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").Text = productDefName;
            });
        }

		/// <summary>
		/// 조회조건의 컨트롤을 추가한다.
		/// </summary>
		protected override void InitializeConditionControls()
		{
			base.InitializeConditionControls();
            Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").EditValueChanged += ProductdefIDChanged;

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
            Conditions.GetControl<SmartComboBox>("p_productDefType").EditValue = "Product";
            Conditions.GetControl<SmartComboBox>("p_wipState").EditValue = "InProduction";
        }

        private void ProductdefIDChanged(object sender, EventArgs e)
        {
            SmartSelectPopupEdit PopProdutid = sender as SmartSelectPopupEdit;

            if (Format.GetFullTrimString(PopProdutid.EditValue).Equals(string.Empty))
            {
                Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").Text = string.Empty;
                //Conditions.GetControl<SmartComboBox>("P_PRODUCTDEFVERSION").EditValue = string.Empty;
            }
        }

        /// <summary>
        /// 팝업형 조회조건 생성 - LOTID
        /// </summary>
        private void InitializeConditionLotId_Popup()
		{
			LotConditionPopup popup = new LotConditionPopup();

			var lotIdPopup = this.Conditions.AddSelectPopup("p_lotId", popup, "LOTID", "LOTID")
				.SetPosition(0.1)
				.SetLabel("LOTID");

			//popup.SetValue("", "", "InProduction,InTransit");
		}

		/// <summary>
		/// 팝업형 조회조건 생성 - 품목 코드
		/// </summary>
		private void InitializeConditionProductDefinitionId_Popup()
		{
			var productDefIdPopup = this.Conditions.AddSelectPopup("p_productDefId", new SqlQuery("GetProductdefList", "10002", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "PRODUCTDEFNAME", "PRODUCTDEFID")
				.SetPopupLayout(Language.Get("SELECTPRODUCTDEFID"), PopupButtonStyles.Ok_Cancel, true, true)
				.SetPopupResultCount(1)
				.SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
				.SetPopupAutoFillColumns("PRODUCTDEFNAME")
				.SetLabel("PRODUCTIONDEFINITION")
				.SetPosition(0.2);

			productDefIdPopup.Conditions.AddTextBox("TXTPRODUCTDEFNAME");

			productDefIdPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 100);
			productDefIdPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 100);
			productDefIdPopup.GridColumns.AddTextBoxColumn("UOMDEFID", 100)
				.SetLabel("UOM");
			productDefIdPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 100);
			productDefIdPopup.GridColumns.AddTextBoxColumn("STATUS", 100)
				.SetLabel("PRODUCTDEFSTATUS");
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
		}

        #endregion

        #region Private Function

        // TODO : 화면에서 사용할 내부 함수 추가
        /// <summary>
        /// 선택한 Lot에 대한 LOT CARD 를 출력한다.
        /// </summary>
        private void PrintReport()
        {
            DataTable checkedRows = grdLotList.View.GetCheckedRows();

            if (checkedRows.Rows.Count < 1)
            {
                // LOT CARD를 출력할 Lot을 선택하시기 바랍니다.
                ShowMessage("SelectPrintLot");
                return;
            }

            btnPrint.IsBusy = true;
            pnlContent.ShowWaitArea();

            string lotId = string.Empty;
            string lotid_rework = string.Empty;
            string lotid_return = string.Empty;

            checkedRows.Rows.Cast<DataRow>().ForEach(row =>
            {
                if (Format.GetFullTrimString(row["LOTCARDTYPE"]).Equals("Normal"))
                    lotId += row["LOTID"].ToString() + ",";
                else if (Format.GetFullTrimString(row["LOTCARDTYPE"]).Equals("Return"))
                    lotid_return += row["LOTID"].ToString() + ",";
                else
                    lotid_rework += row["LOTID"].ToString() + ",";
            });

            if (!string.IsNullOrEmpty(lotId))
            {
                lotId = lotId.Substring(0, lotId.Length - 1);
                CommonFunction.PrintLotCard_Ver2(lotId, LotCardType.Normal);
            }
            if (!string.IsNullOrEmpty(lotid_rework))
            {
                lotid_rework = lotid_rework.Substring(0, lotid_rework.Length - 1);
                CommonFunction.PrintLotCard_Ver2(lotid_rework, LotCardType.Rework);
            }
            if (!string.IsNullOrEmpty(lotid_return))
            {
                lotid_return = lotid_return.Substring(0, lotid_return.Length - 1);
                CommonFunction.PrintLotCard_Ver2(lotid_return, LotCardType.Return);
            }

            #region  주석 처리 소스
            //Assembly assembly = Assembly.GetAssembly(this.GetType());
            //Stream stream = assembly.GetManifestResourceStream("Micube.SmartMES.ProcessManagement.Report.LotCardProduction.repx");



            //CommonFunction.PrintLotCard(lotId, stream);

            //Dictionary<string, object> param = new Dictionary<string, object>();
            //param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            //param.Add("LOTID", lotId);

            //DataSet dsReport = new DataSet();

            //DataTable dtQueryResult = SqlExecuter.Query("SelectLotInfoOnPrintLotCard", "10001", param);
            //DataTable dtLotInfo = dtQueryResult.Clone();

            //dtQueryResult.Rows.Cast<DataRow>().ForEach(lotRow =>
            //{
            //    DataRow newRow = dtLotInfo.NewRow();
            //    newRow.ItemArray = lotRow.ItemArray.Clone() as object[];

            //    dtLotInfo.Rows.Add(newRow);
            //});


            //dtLotInfo.AcceptChanges();

            //dsReport.Tables.Add(dtLotInfo);

            //DataTable dtProcess = SqlExecuter.Query("SelectProcessResultAndRoutingForLotCard_Normal", "10001", new Dictionary<string, object>() { { "LOTID", lotId } });

            //DataTable dtProcessList = new DataTable();
            //dtProcessList.Columns.Add("LOTID", typeof(string));
            //dtProcessList.Columns.Add("SEQUENCE1", typeof(string));
            //dtProcessList.Columns.Add("CHANGESTATE1", typeof(string));
            //dtProcessList.Columns.Add("PROCESSNAME1", typeof(string));
            //dtProcessList.Columns.Add("PROCESSINGDATE1", typeof(string));
            //dtProcessList.Columns.Add("PROCESSQTY1", typeof(string));
            //dtProcessList.Columns.Add("WORKPLACEWORKER1", typeof(string));
            //dtProcessList.Columns.Add("SEQUENCE2", typeof(string));
            //dtProcessList.Columns.Add("CHANGESTATE2", typeof(string));
            //dtProcessList.Columns.Add("PROCESSNAME2", typeof(string));
            //dtProcessList.Columns.Add("PROCESSINGDATE2", typeof(string));
            //dtProcessList.Columns.Add("PROCESSQTY2", typeof(string));
            //dtProcessList.Columns.Add("WORKPLACEWORKER2", typeof(string));

            //List<string> lotList = lotId.Split(',').ToList();
            //lotList.ForEach(id =>
            //{
            //    DataTable tempTable = dtProcess.Select("LOTID = '" + id + "'").CopyToDataTable().Rows.Cast<DataRow>().OrderBy(row => row["SEQUENCE"]).CopyToDataTable();

            //    int totalProcessCount = tempTable.Rows.Count;
            //    int pageCount = (totalProcessCount / (_printProcessRowPerPage * 2)) + ((totalProcessCount % (_printProcessRowPerPage * 2) == 0) ? 0 : 1);

            //    int rowNumber = 0;

            //    DataTable tempResult = dtProcessList.Clone();

            //    for (int i = 0; i < pageCount; i++)
            //    {
            //        for (int j = 0; j < _printProcessRowPerPage; j++)
            //        {
            //            rowNumber = (i * _printProcessRowPerPage * 2) + j;

            //            if (rowNumber >= totalProcessCount)
            //                break;

            //            DataRow processRow = tempTable.Rows[rowNumber];

            //            DataRow newRow = tempResult.NewRow();

            //            newRow["LOTID"] = processRow["LOTID"];
            //            newRow["SEQUENCE1"] = processRow["USERSEQUENCE"];
            //            newRow["CHANGESTATE1"] = processRow["DIVISION"];
            //            newRow["PROCESSNAME1"] = processRow["PROCESSSEGMENTNAME"];
            //            newRow["PROCESSINGDATE1"] = processRow["SENDTIME"];
            //            newRow["PROCESSQTY1"] = processRow["SENDQTY"];
            //            newRow["WORKPLACEWORKER1"] = processRow["SENDUSER"];


            //            tempResult.Rows.Add(newRow);
            //        }

            //        for (int j = 0; j < _printProcessRowPerPage; j++)
            //        {
            //            rowNumber = (i * _printProcessRowPerPage * 2) + (j + _printProcessRowPerPage);

            //            if (rowNumber >= totalProcessCount)
            //                break;

            //            DataRow processRow = tempTable.Rows[rowNumber];

            //            DataRow newRow = tempResult.Rows[rowNumber - _printProcessRowPerPage];

            //            newRow["LOTID"] = processRow["LOTID"];
            //            newRow["SEQUENCE2"] = processRow["USERSEQUENCE"];
            //            newRow["CHANGESTATE2"] = processRow["DIVISION"];
            //            newRow["PROCESSNAME2"] = processRow["PROCESSSEGMENTNAME"];
            //            newRow["PROCESSINGDATE2"] = processRow["SENDTIME"];
            //            newRow["PROCESSQTY2"] = processRow["SENDQTY"];
            //            newRow["WORKPLACEWORKER2"] = processRow["SENDUSER"];
            //        }
            //    }

            //    dtProcessList.Merge(tempResult);
            //});


            //dtProcessList.AcceptChanges();

            ////for (int i = 0; i < dtLotInfo.Rows.Count; i++)
            ////{
            ////    for (int j = 0; j < 30; j++)
            ////    {
            ////        DataRow newRow = dtProcessList.NewRow();
            ////        newRow["LOTID"] = dtLotInfo.Rows[i]["LOTID"];
            ////        newRow["SEQUENCE1"] = ((j + 1) * 10).ToString();
            ////        newRow["SEQUENCE2"] = ((j + 26) * 10).ToString();
            ////        newRow["PROCESSNAME1"] = dtLotInfo.Rows[i]["LOTID"];

            ////        dtProcessList.Rows.Add(newRow);
            ////    }
            ////}

            //dsReport.Tables.Add(dtProcessList);

            //DataRelation relation = new DataRelation("RelationLotId", dtLotInfo.Columns["LOTID"], dtProcessList.Columns["LOTID"]);

            //dsReport.Relations.Add(relation);

            //XtraReport report = XtraReport.FromFile(Path.Combine(Application.StartupPath, "Reports", "Lot Card_Production.repx"));
            //report.DataSource = dsReport;
            //report.DataMember = "Table1";

            //Band band = report.Bands["Detail"];
            ////SetReportControlDataBinding(band.Controls, dsReport.Tables[0]);


            //DetailReportBand detailReport = report.Bands["DetailReport"] as DetailReportBand;
            //detailReport.DataSource = dsReport;
            //detailReport.DataMember = "RelationLotId";

            //Band groupHeader = detailReport.Bands["GroupHeader1"];
            //SetReportControlDataBinding(groupHeader.Controls, dsReport, "");

            //XRControl picLogo = groupHeader.FindControl("picLogo", true);

            //if (picLogo != null && picLogo is XRPictureBox picBox)
            //{
            //    picBox.Image = Image.FromFile(Path.Combine(Application.StartupPath, "Reports", "Logo", "Logo.jpg"));
            //}

            //Band detailBand = detailReport.Bands["Detail1"];
            //SetReportControlDataBinding(detailBand.Controls, dsReport, "RelationLotId");


            //report.Print();
            #endregion

            pnlContent.CloseWaitArea();
            btnPrint.IsBusy = false;
        }
        private void InitializeConditionProductOrderId_Popup()
        {
            var conditionProductionOrderId = Conditions.AddSelectPopup("P_PRODUCTIONORDERID"
                    , new SqlQuery("GetProductionOrderIdListOfLotInput", "10001"
                        , $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")
                    , "PRODUCTIONORDERID", "PRODUCTIONORDERID")
                .SetPopupLayout("SELECTPRODUCTIONORDER", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupResultCount(0)
                .SetPopupLayoutForm(1000, 800)
                .SetPopupAutoFillColumns("PRODUCTDEFNAME")
                .SetLabel("PRODUCTIONORDERID")
                .SetPosition(10)
                .SetPopupApplySelection((selectedRows, dataGridRows) =>
                {
                    // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
                    // dataGridRow : 현재 Focus가 있는 그리드의 DataRow
                });


            // 팝업에서 사용되는 검색조건 (P/O번호)
            conditionProductionOrderId.Conditions.AddTextBox("TXTPRODUCTIONORDERID");

            // 팝업 그리드에서 보여줄 컬럼 정의
            // P/O번호
            conditionProductionOrderId.GridColumns.AddTextBoxColumn("PRODUCTIONORDERID", 150)
                .SetValidationKeyColumn();
            // 수주라인
            conditionProductionOrderId.GridColumns.AddTextBoxColumn("LINENO", 50);
            // 수주량
            conditionProductionOrderId.GridColumns.AddSpinEditColumn("PLANQTY", 90);
            // 품목코드
            conditionProductionOrderId.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            // 품목명
            conditionProductionOrderId.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            // 품목버전
            conditionProductionOrderId.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 100);
            // 품목코드 | 품목버전
            conditionProductionOrderId.GridColumns.AddTextBoxColumn("PRODUCTDEF", 0).SetIsHidden();
        }
        /// <summary>
        /// Report 파일의 컨트롤 중 Tag(FieldName) 값이 있는 컨트롤에 DataBinding(Text)를 추가한다.
        /// </summary>
        /// <param name="controls"></param>
        /// <param name="dataSource"></param>
        private void SetReportControlDataBinding(XRControlCollection controls, DataSet dataSource, string relationId)
        {
            if (controls.Count > 0)
            {
                foreach (XRControl control in controls)
                {
                    if (!string.IsNullOrWhiteSpace(control.Tag.ToString()))
                    {
                        string fieldName = "";

                        if (!string.IsNullOrWhiteSpace(relationId))
                            fieldName = string.Join(".", relationId, control.Tag.ToString());
                        else
                            fieldName = control.Tag.ToString();

                        control.DataBindings.Add("Text", dataSource, fieldName);
                    }

                    SetReportControlDataBinding(control.Controls, dataSource, relationId);
                }
            }
        }

		#endregion
	}
}
