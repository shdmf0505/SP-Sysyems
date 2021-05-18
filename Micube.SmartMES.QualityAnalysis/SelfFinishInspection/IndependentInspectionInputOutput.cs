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
    /// 프 로 그 램 명  : 품질관리 > 자주/최종검사 > 자주검사(입고/출하) 결과조회
    /// 업  무  설  명  : 자주검사(입고/출하)결과를 조회한다.
    /// 생    성    자  : 김용조
    /// 생    성    일  : 2019-11-01
    /// 수  정  이  력  : 2019-11-21 강유라 불량별 이미지 조회
    /// 
    /// 
    /// </summary>
    public partial class IndependentInspectionInputOutput : SmartConditionManualBaseForm
    {
        #region Local Variables
        #endregion

        #region 생성자

        public IndependentInspectionInputOutput()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 자주검사에서 결과 조회에서 팝업링크 구성
        /// 2020-01-18 배선용
        /// </summary>
        /// <param name="parameters"></param>
        public override void LoadForm(Dictionary<string, object> parameters)
        {
            base.LoadForm(parameters);
            if (parameters != null)
            {
                //InitializeTreeList();

                // 2020.02.24-유석진-자주검사(입고,출하)결과조회 화면에서 받은 조회기간 파리미터 설정 
                SmartPeriodEdit period = Conditions.GetControl<SmartPeriodEdit>("P_INSPECTIONDATE");
                if (parameters.ContainsKey("P_REQUESTDATE_PERIODFR"))
                    period.datePeriodFr.EditValue = parameters["P_REQUESTDATE_PERIODFR"];
                if (parameters.ContainsKey("P_REQUESTDATE_PERIODTO"))
                    period.datePeriodTo.EditValue = parameters["P_REQUESTDATE_PERIODTO"];
                Conditions.GetControl<SmartTextBox>("P_LOTID").Text = Format.GetString(parameters["LOTID"]);
                //OnSearchAsync();
                OnSearchAsync();
            }
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
        ///  그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            InitializeGrid_Master();
            InitializeGrid_Detail();
            InitializeGrid_File();
        }
        /// <summary>
        /// MASTER 그리드
        /// </summary>
        private void InitializeGrid_Master()
        {
            grdMaster.GridButtonItem = GridButtonItem.Export | GridButtonItem.Expand | GridButtonItem.Restore;
            grdMaster.View.SetIsReadOnly();
            grdMaster.View.AddTextBoxColumn("PLANTID", 120)
                .SetIsHidden();
            var costprocess = grdMaster.View.AddGroupColumn("QCMINSPECTPRCINFOR");
            costprocess.AddTextBoxColumn("INSPECTIONPROCESSDATE", 120)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss");
            costprocess.AddTextBoxColumn("PARENTPROCESSSEGMENTCLASSID", 100).SetIsHidden();
            costprocess.AddTextBoxColumn("TOPPROCESSSEGMENTCLASSNAME", 100).SetIsReadOnly(true);
            costprocess.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 100).SetIsHidden();
            costprocess.AddTextBoxColumn("MIDDLEPROCESSSEGMENTCLASSNAME", 100).SetIsReadOnly(true);
            costprocess.AddTextBoxColumn("PROCESSSEGMENTID", 100).SetIsHidden();
            costprocess.AddTextBoxColumn("USERSEQUENCE", 100).SetIsReadOnly(true);
            costprocess.AddTextBoxColumn("PROCESSSEGMENTNAME", 200).SetIsReadOnly(true);            
            costprocess.AddTextBoxColumn("AREAID", 100).SetIsHidden();
            costprocess.AddTextBoxColumn("DEGREE", 100);
            costprocess.AddTextBoxColumn("AREANAME", 150);
            costprocess.AddTextBoxColumn("INSPECTIONDEFID", 100).SetIsHidden();
            costprocess.AddTextBoxColumn("INSPECTIONDEFNAME", 100).SetLabel("INSEPCTIONDEFCATGNAME");
            costprocess.AddTextBoxColumn("INSPECTIONUSER", 100).SetIsHidden();
            //costprocess.AddTextBoxColumn("INSPECTIONUSERNAME", 100).SetIsHidden();//SF_USER 와 연결한 필드
            costprocess.AddTextBoxColumn("INSPECTIONUSERNAME", 100).SetLabel("INSPECTORNAME");

            //costprocess.AddComboBoxColumn("FINISHINSPECTIONTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=FinishInspectionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));  // 진행상태

            var costprd = grdMaster.View.AddGroupColumn("QCMLRRPRDINFOR");
            costprd.AddTextBoxColumn("PRODUCTDEFID", 100);
            costprd.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            costprd.AddTextBoxColumn("PRODUCTDEFVERSION", 80);
            costprd.AddTextBoxColumn("PARENTLOTID", 200);            
            costprd.AddTextBoxColumn("LOTID", 200);
            costprd.AddTextBoxColumn("WORKENDPCSQTY", 80)
               .SetTextAlignment(TextAlignment.Right)
               .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costprd.AddTextBoxColumn("WORKENDPANELQTY", 80)
               .SetTextAlignment(TextAlignment.Right)
               .SetDisplayFormat("#,##0", MaskTypes.Numeric);            

            var costInsp = grdMaster.View.AddGroupColumn("QCMFINALINSPECTQTY"); //검사수
            costInsp.AddTextBoxColumn("INSPECTIONPCSQTY", 80)
               .SetTextAlignment(TextAlignment.Right)
               .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costInsp.AddTextBoxColumn("INSPECTIONPNLQTY", 80)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);

            var costSpecout = grdMaster.View.AddGroupColumn("QCMFINALINSPECTSPECOUT"); // 불량수
            costSpecout.AddTextBoxColumn("SPECOUTPCSQTY", 80)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costSpecout.AddTextBoxColumn("SPECOUTPNLQTY", 80)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costSpecout.AddTextBoxColumn("SPECOUTPERCENTAGE", 100)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0.##", MaskTypes.Numeric);  //불량율
            costSpecout.AddTextBoxColumn("ISNCRPUBLISH", 100);

            //2020-02-07 강유라 재검사유 필드 추가 : 위치 추후  수정 할 수도있음
            costSpecout.AddTextBoxColumn("REWORKCOMMENT", 100).SetLabel("REINSPECTREASION") ;


            grdMaster.View.PopulateColumns();
            SetFooterSummary();

            grdMaster.View.OptionsCustomization.AllowColumnMoving = false;
            grdMaster.View.OptionsView.ShowFooter = true;
            grdMaster.View.FooterPanelHeight = 10;
            grdMaster.ShowStatusBar = false;
        }

        /// <summary>
        /// DETAIL 그리드
        /// </summary>
        private void InitializeGrid_Detail()
        {
            grdDetail.GridButtonItem = GridButtonItem.Export;
            grdDetail.View.SetIsReadOnly();
            grdDetail.View.AddTextBoxColumn("PLANTID", 120)
           .SetIsHidden();

            var costprocess = grdDetail.View.AddGroupColumn("");// 작업정보 
            //costprocess.AddTextBoxColumn("INSPECTIONPROCESSDATE", 120)
            //    .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
            //    .SetIsHidden();
            //costprocess.AddTextBoxColumn("DECISIONDEGREE", 100)
            //    .SetIsHidden();
            //costprocess.AddTextBoxColumn("DEGREE", 100).SetIsHidden();
            //costprocess.AddTextBoxColumn("DEFECTCODE", 100).SetIsHidden();
            costprocess.AddTextBoxColumn("DECISIONDEGREE", 100);
            costprocess.AddTextBoxColumn("DEFECTCODENAME", 100);
            costprocess.AddTextBoxColumn("QCSEGMENTNAME", 150);

            var costInsp = grdDetail.View.AddGroupColumn("QCMFINALINSPECTQTY");//검사수
            costInsp.AddTextBoxColumn("INSPECTIONPNLQTY", 80)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costInsp.AddTextBoxColumn("INSPECTIONPCSQTY", 100)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            var costSpec = grdDetail.View.AddGroupColumn("QCMFINALINSPECTSPECOUT");//불량수
            costSpec.AddTextBoxColumn("SPECOUTPNLQTY", 80)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costSpec.AddTextBoxColumn("SPECOUTPCSQTY", 100)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            var costRATE = grdDetail.View.AddGroupColumn("");
            costRATE.AddTextBoxColumn("SPECOUTPERCENTAGE", 80)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0.##", MaskTypes.Numeric);
            //원인공정 .
            var costRes = grdDetail.View.AddGroupColumn("QCMFINALINSPECTREASON"); //원인공정 .
            costRes.AddTextBoxColumn("REASONPRODUCTDEFNAME", 100);
            costRes.AddTextBoxColumn("REASONCONSUMABLELOTID", 100);
            costRes.AddTextBoxColumn("REASONSEGMENTNAME", 100);
            costRes.AddTextBoxColumn("REASONAREANAME", 100);
            //var costIsncr = grdDetail.View.AddGroupColumn("");
            //costIsncr.AddTextBoxColumn("ISNCRPUBLISH", 100);

            grdDetail.View.PopulateColumns();
        }

        /// <summary>
        /// FILE 그리드
        /// </summary>
        private void InitializeGrid_File()
        {
            grdFile.GridButtonItem = GridButtonItem.None;
            grdFile.View.SetIsReadOnly();
            grdFile.ShowStatusBar = false;
            grdFile.View.SetAutoFillColumn("FILENAME");

            grdFile.View.AddTextBoxColumn("FILENAME", 150);
            grdFile.View.AddTextBoxColumn("FILEEXT", 60).SetTextAlignment(TextAlignment.Center);
            grdFile.View.AddSpinEditColumn("FILESIZE", 60).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetTextAlignment(TextAlignment.Right);
            grdFile.View.AddTextBoxColumn("FILEPATH", 100).SetIsHidden();
            grdFile.View.AddTextBoxColumn("URL").SetIsHidden();

            grdFile.View.PopulateColumns();
        }
        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            grdMaster.View.FocusedRowChanged += View_FocusedRowChanged;

            grdDetail.View.FocusedRowChanged += View_FocusedRowChangedDetail;

            grdFile.View.FocusedRowChanged += View_FocusedRowChangedFile;
        }

        /// <summary>
        /// 코드그룹 리스트 그리드에서 추가 버튼 클릭 시 호출
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 그리드의 포커스 행 변경 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            focusedRowChanged();
            focusedRowChangedDetail();
            focusedRowChangedFile();
        }

        /// <summary>
        ///  Detail 그리드의 포커스 행 변경 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_FocusedRowChangedDetail(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            focusedRowChangedDetail();
            focusedRowChangedFile();
        }

        /// <summary>
        /// File 그리드의 포커스 행 변경 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_FocusedRowChangedFile(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            focusedRowChangedFile();
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

            values.Add("P_LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);
            #region 기간 검색형 전환 처리 
            if (!(values["P_INSPECTIONDATE_PERIODFR"].ToString().Equals("")))
            {
                DateTime requestDateFr = Convert.ToDateTime(values["P_INSPECTIONDATE_PERIODFR"]);
                values.Remove("P_INSPECTIONDATE_PERIODFR");
                values.Add("P_INSPECTIONDATE_PERIODFR", string.Format("{0:yyyy-MM-dd}", requestDateFr));
            }
            if (!(values["P_INSPECTIONDATE_PERIODTO"].ToString().Equals("")))
            {
                DateTime requestDateTo = Convert.ToDateTime(values["P_INSPECTIONDATE_PERIODTO"]);
                values.Remove("P_INSPECTIONDATE_PERIODTO");
                requestDateTo = requestDateTo.AddDays(1);
                values.Add("P_INSPECTIONDATE_PERIODTO", string.Format("{0:yyyy-MM-dd}", requestDateTo));
            }

            #endregion
            DataTable dt = await SqlExecuter.QueryAsync("GetIndependentInspectionResultInquiry", "10001", values);

            if (dt.Rows.Count < 1)
            {
                ShowMessage("NoSelectData");
                DataTable dtPirceCode = (grdDetail.DataSource as DataTable).Clone();
                grdDetail.DataSource = dtPirceCode;
            }

            grdMaster.DataSource = dt;
            if (dt.Rows.Count > 0)
            {
                grdMaster.View.FocusedRowHandle = 0;
                grdMaster.View.SelectRow(0);
                focusedRowChanged();
            }
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();
            //InitializeConditionPopup_Plant();
            //1처리기간 필수

            //2.품목코드 
            InitializeConditionPopup_Product();
            //3.모 LOT IT

            //4.LOT #
            InitializeCondition_Lotid();

            //6.공정 
            InitializeConditionPopup_Processsegment();
            //7.작업장.
            InitializeConditionAreaId_Popup();


        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();


        }
        /// <summary>
        /// 사이트 조회조건
        /// </summary>
        private void InitializeConditionPopup_Plant()
        {
            this.Conditions.AddComboBox("plantId", new SqlQuery("GetPlantListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PLANTNAME", "PLANTID")
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                .SetDefault(UserInfo.Current.Plant)
                .SetValidationIsRequired()
                .SetLabel("PLANT")
                .SetPosition(0.1);
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
               .SetPosition(2.2);

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
        ///LOT ,모LOT 
        /// </summary>
        private void InitializeCondition_Lotid()
        {


            var txtPARENTLOTID = Conditions.AddTextBox("P_PARENTLOTID")
               .SetLabel("PARENTLOTID")
               .SetPosition(2.5);
            var txtLOTID = Conditions.AddTextBox("P_LOTID")
              .SetLabel("LOTID")
              .SetPosition(2.7);

        }
        /// <summary>
        /// 표준공정 조회조건
        /// </summary>
        private void InitializeConditionPopup_Processsegment()
        {
            var processSegmentIdPopup = this.Conditions.AddSelectPopup("P_ProcessSegmentId", new SqlQuery("GetProcessSegmentList", "10002", $"PLANTID={UserInfo.Current.Plant}", "PROCESSSEGMENTCLASSTYPE=MiddleProcessSegmentClass", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PROCESSSEGMENTNAME", "PROCESSSEGMENTID")
                .SetPopupLayout(Language.Get("SELECTPROCESSSEGMENTID"), PopupButtonStyles.Ok_Cancel, true, true)
                .SetPopupResultCount(1)
                .SetPopupLayoutForm(800, 800, FormBorderStyle.FixedToolWindow)
                .SetPopupAutoFillColumns("PROCESSSEGMENTNAME")
                .SetLabel("PROCESSSEGMENT")
                .SetPosition(4.0);

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
        /// 팝업형 조회조건 생성 - 작업장        
        /// </summary>
        private void InitializeConditionAreaId_Popup()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            //param.Add("AreaType", "Area");
            param.Add("LanguageType", UserInfo.Current.LanguageType);
            param.Add("P_USERID", UserInfo.Current.Id);
            //param.Add("AREA", UserInfo.Current.Area);

            var areaIdPopup = this.Conditions.AddSelectPopup("p_areaId", new SqlQuery("GetAuthorityUserUseArea", "10001", param), "AREANAME", "AREAID")
                .SetPopupLayout(Language.Get("SELECTAREAID"), PopupButtonStyles.Ok_Cancel, true, true)
                .SetPopupResultCount(1)
                .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                .SetPopupAutoFillColumns("AREANAME")
                //.SetRelationIds("plantId")
                .SetLabel("AREA")
                .SetPosition(5.0);

            areaIdPopup.Conditions.AddTextBox("AREAIDNAME")
                .SetLabel("AREAIDNAME");

            areaIdPopup.GridColumns.AddTextBoxColumn("AREAID", 150);
            areaIdPopup.GridColumns.AddTextBoxColumn("AREANAME", 200);
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

        // TODO : 화면에서 사용할 내부 함수 추가
        /// <summary>
        /// 마스터 정보를 조회한다.
        /// </summary>
        private void LoadData()
        {

        }
        /// <summary>
        /// 그리드의 포커스 행 변경시 행의 작업목록의 내용을 매핑처리 함.
        /// </summary>
        private void focusedRowChanged()
        {
            //포커스 행 체크 
            if (grdMaster.View.FocusedRowHandle < 0) return;

            //단가코드 정보 가져오기 
            Dictionary<string, object> Param = new Dictionary<string, object>();

            Param.Add("P_RESOURCEID", grdMaster.View.GetFocusedRowCellValue("LOTID"));
            Param.Add("P_LANGUAGETYPE", UserInfo.Current.LanguageType);
            Param.Add("P_TXNGROUPHISTKEY", grdMaster.View.GetFocusedRowCellValue("TXNGROUPHISTKEY"));
            Param.Add("P_INSPECTIONDEFID", grdMaster.View.GetFocusedRowCellValue("INSPECTIONDEFID"));
            Param.Add("P_PLANTID", grdMaster.View.GetFocusedRowCellValue("PLANTID"));
            Param.Add("P_PROCESSRELNO", grdMaster.View.GetFocusedRowCellValue("PROCESSRELNO"));
            DataTable dtDetail = SqlExecuter.Query("GetIndependentInspectionResultDetailInquiry", "10001", Param);

            grdDetail.DataSource = dtDetail;

        }

        /// <summary>
        /// Detail 그리드의 포커스 행 변경시 행의 파일 내용을 매핑처리 함.
        /// </summary>
        private void focusedRowChangedDetail()
        {
            //포커스 행 체크 
            DataRow focusedRow = grdDetail.View.GetFocusedDataRow();

            if (focusedRow == null)
            {
                grdFile.View.ClearDatas();
                return;
            }

            //사진 정보 가져오기 
            Dictionary<string, object> Param = new Dictionary<string, object>();

            Param.Add("FILERESOURCETYPE", focusedRow["INSPECTIONDEFID"]);
            Param.Add("FILERESOURCEID", focusedRow["FILERESOURCEID"]);
            DataTable dtFile = SqlExecuter.Query("SelectSelfInspMeasureImage", "10001", Param);

            grdFile.DataSource = dtFile;
        }


        /// <summary>
        /// File 그리드의 포커스 행 변경시 이미지를 보여줌.
        /// </summary>
        private void focusedRowChangedFile()
        {
            //포커스 행 체크 
            DataRow focusedRow = grdFile.View.GetFocusedDataRow();

            if (focusedRow == null)
            {
                picBox.EditValue = null;
                return;
            }

            //이미지 바인딩
            //string fileURL = AppConfiguration.GetString("Application.SmartDeploy.Url") + focusedRow["URL"];
            string filePath = focusedRow["FILEPATH"].ToString();
            string fileName = string.Join(".", focusedRow["FILENAME"].ToString(), focusedRow["FILEEXT"].ToString());
            Bitmap image = CommonFunction.GetFtpImageFileToBitmap(filePath, fileName);
            picBox.Image = image;
        }


        /// <summary>
        /// Main Grid에 Layer에 Footer에 Summary 처리
        /// </summary>
        private void SetFooterSummary()
        { 
            grdMaster.View.Columns["WORKENDPCSQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdMaster.View.Columns["WORKENDPCSQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdMaster.View.Columns["WORKENDPANELQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdMaster.View.Columns["WORKENDPANELQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdMaster.View.Columns["INSPECTIONPCSQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdMaster.View.Columns["INSPECTIONPCSQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdMaster.View.Columns["INSPECTIONPNLQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdMaster.View.Columns["INSPECTIONPNLQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdMaster.View.Columns["SPECOUTPCSQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdMaster.View.Columns["SPECOUTPCSQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdMaster.View.Columns["SPECOUTPNLQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdMaster.View.Columns["SPECOUTPNLQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            //grdMaster.View.Columns["SPECOUTPERCENTAGE"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //grdMaster.View.Columns["SPECOUTPERCENTAGE"].SummaryItem.DisplayFormat = "{0:###,###}";
        }

        #endregion
    }
}
