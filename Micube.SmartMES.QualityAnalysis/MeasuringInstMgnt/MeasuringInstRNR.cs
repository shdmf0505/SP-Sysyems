#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;

using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

using System.Data;
using Micube.SmartMES.Commons.Controls;
using System.Threading.Tasks;
using System.Windows.Forms;
using Micube.SmartMES.Commons;
using DevExpress.XtraEditors.Repository;
using DevExpress.Utils;
using System;
using System.Collections.Generic;
using Micube.Framework.Log;
using System.Linq;

#endregion

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 계측기 관리> R&R 평가 등록
    /// 업  무  설  명  : 계측기 관리사항을 조회함.
    /// 생    성    자  : 이승우
    /// 생    성    일  : 2019-10-01
    /// 수  정  이  력  : 2020-02-07 강유라 
    ///                  2019-10-29 Layout 변경
    ///                  2019-10-01 최초 생성일
    /// 
    /// 
    /// </summary>
    public partial class MeasuringInstRNR : SmartConditionManualBaseForm
    {
        #region Local Variables
        #endregion

        #region 생성자

        public MeasuringInstRNR()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeEvent();
            InitializeGridMain();
            InitializeGridSub();

        }

        /// <summary>        
        /// 그리드 초기화 - Main
        /// </summary>
        private void InitializeGridMain()
        {
            grdMain.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Restore | GridButtonItem.Export;
            grdMain.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdMain.View.BestFitColumns();

            grdMain.View.AddTextBoxColumn("SERIALNO", 150).SetLabel("MEASURINGSERIALNO").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("CONTROLNO", 150).SetLabel("MEASURINGCONTROLNO").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("CONTROLNAME", 150).SetLabel("MEASURINGCONTROLNAME").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("EQUIPMENTTYPENAME", 150).SetLabel("MEASURINGEQUIPMENTTYPENAME").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("MEASURINGID", 150).SetLabel("MEASURINGEQUIPMENTID").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("MEASURINGPRODUCTID", 150).SetLabel("MEASURINGPRODUCTDEFNAME").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("ENTERPRISEID", 150).SetLabel("MEASURINGENTERPRISEID").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("AREAID", 150).SetLabel("MEASURINGAREAID").SetIsReadOnly().SetIsHidden();
            grdMain.View.AddTextBoxColumn("AREANAME", 150).SetLabel("MEASURINGAREAID").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("PLANTID", 150).SetLabel("MEASURINGPLANTID").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("UNIT", 150).SetLabel("MEASURINGUNIT").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("MANUFACTURES", 150).SetLabel("MEASURINGMANUFACTURES").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("MANUFACTURECOUNTRY", 150).SetLabel("MEASURINGMANUFACTURECOUNTRY").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("MANUFACTURER", 150).SetLabel("MEASURINGMANUFACTURER").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("DEPARTMENT", 150).SetLabel("MEASURINGDEPARTMENT").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("PURCHASEDATE", 150).SetLabel("MEASURINGPURCHASEDATE").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("PURCHASEAMOUNT", 150).SetLabel("MEASURINGPURCHASEAMOUNT").SetIsReadOnly().SetDisplayFormat("{0:N2}", MaskTypes.Numeric);
            grdMain.View.AddTextBoxColumn("ISPC", 150).SetLabel("MEASURINGISPC").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("OSVERSION", 150).SetLabel("MEASURINGOSVERSION").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("SORTWAREVERSION", 150).SetLabel("MEASURINGSORTWAREVERSION").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("SCRAPDATE", 150).SetLabel("MEASURINGSCRAPDATE").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("ISSCRAP", 150).SetLabel("MEASURINGISSCRAP").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("REPAIRDISPOSALID", 150).SetLabel("MEASURINGREPAIRDISPOSALID").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("INSPECTIONCALIBRATIONCYCLE", 150).SetLabel("MEASURINGINSPECTIONCALIBRATIONCYCLE").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("ISINSPECTIONCALIBRATION", 150).SetLabel("MEASURINGISINSPECTIONCALIBRATION").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("ENDCALIBRATIONDATE", 150).SetLabel("MEASURINGENDCALIBRATIONDATE").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("NEXTCALIBRATIONDATE", 150).SetLabel("MEASURINGNEXTCALIBRATIONDATE").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("CALIBRATIONRESULTNAME", 150).SetLabel("MEASURINGCALIBRATIONRESULT").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("ISMIDDLE", 150).SetLabel("ISMIDDLE").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("MIDDLECHECKCYCLE", 150).SetLabel("MEASURINGMIDDLECHECKCYCLE").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("MIDDLECHECKDATE", 150).SetLabel("MEASURINGMIDDLECHECKDATE").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("NEXTMIDDLECHECKDATE", 150).SetLabel("MEASURINGNEXTMIDDLECHECKDATE").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("NEXTMIDDLECHECKRESULTNAME", 150).SetLabel("MEASURINGNEXTMIDDLECHECKRESULT").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("ISRNRTARGET", 150).SetLabel("MEASURINGISRNRTARGET").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("RNRCYCLE", 150).SetLabel("MEASURINGRNRCYCLE").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("ISRNR", 150).SetLabel("MEASURINGISRNR").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("RNRPROGRESSDATE", 150).SetLabel("MEASURINGRNRPROGRESSDATE").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("RNRNEXTPROGRESSDATE", 150).SetLabel("MEASURINGRNRNEXTPROGRESSDATE").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("RNRRESULTNAME", 150).SetLabel("MEASURINGRNRRESULT").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("RNRREPORTTYPE", 150).SetLabel("MEASURINGRNRREPORTTYPE").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("RNRREPORTFILENAME", 150).SetLabel("MEASURINGRNRREPORTFILENAME").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("CORRECTION", 150).SetLabel("MEASURINGCORRECTION").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("PERMITVALUE", 150).SetLabel("MEASURINGPERMITVALUE").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("ANALYTICALOCCASION", 150).SetLabel("MEASURINGANALYTICALOCCASION").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("INSPECTIONCALIBRATIONCOA", 150).SetLabel("MEASURINGINSPECTIONCALIBRATIONCOA").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("ISMEASURINGINSTRUMENT", 150).SetLabel("MEASURINGISMEASURINGINSTRUMENT").SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("COMMENTS", 150).SetLabel("MEASURINGCOMMENTS").SetIsReadOnly();

            grdMain.View.PopulateColumns();

        }

        /// <summary>        
        /// 그리드 초기화 - Main
        /// </summary>
        private void InitializeGridSub()
        {
            grdSub.GridButtonItem = GridButtonItem.Add | GridButtonItem.Export | GridButtonItem.Restore | GridButtonItem.Expand;
            grdSub.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;      
            grdSub.View.BestFitColumns();

            grdSub.View.AddDateEditColumn("MEASUREDATE", 100).SetLabel("MEASURINGMEASUREDATE").SetValidationKeyColumn()
                .SetTextAlignment(TextAlignment.Center).SetDisplayFormat("yyyy-MM-dd", MaskTypes.DateTime)
                .SetIsReadOnly(); // 수동입력/측정일자
            grdSub.View.AddTextBoxColumn("MEASUREUSERA", 150).SetLabel("MEASURINGMEASUREUSERA").SetValidationKeyColumn();
            grdSub.View.AddTextBoxColumn("MEASUREUSERB", 150).SetLabel("MEASURINGMEASUREUSERB").SetValidationKeyColumn();
            grdSub.View.AddTextBoxColumn("MEASUREUSERC", 150).SetLabel("MEASURINGMEASUREUSERC").SetValidationKeyColumn();
            
            grdSub.View.AddTextBoxColumn("REPORTNO", 150).SetLabel("MEASURINGREPORTNO");
            grdSub.View.AddTextBoxColumn("ANALYST", 150).SetLabel("MEASURINGANALYST");
            grdSub.View.AddSpinEditColumn("STUDYVARIATION", 150).SetLabel("MEASURINGSTUDYVARIATION");
            grdSub.View.AddSpinEditColumn("TESTTATE", 150).SetLabel("MEASURINGTESTTATE");
            grdSub.View.AddSpinEditColumn("TRIALS", 150).SetLabel("MEASURINGTRIALS");            
            grdSub.View.AddSpinEditColumn("INSPECTIONQTY", 150).SetLabel("MEASURINGINSPECTIONQTY");            
            grdSub.View.AddSpinEditColumn("CORRECTION", 150).SetLabel("MEASURINGCORRECTION");
            grdSub.View.AddSpinEditColumn("PERMITVALUE", 150).SetLabel("MEASURINGPERMITVALUE");
            grdSub.View.AddTextBoxColumn("ANALYTICALOCCASION", 150).SetLabel("MEASURINGANALYTICALOCCASION"); 

            grdSub.View.AddTextBoxColumn("SERIALNO", 150).SetLabel("MEASURINGSERIALNO").SetIsReadOnly().SetIsHidden();
            grdSub.View.AddTextBoxColumn("CONTROLNO", 150).SetLabel("MEASURINGCONTROLNO").SetIsReadOnly().SetIsHidden();

            grdSub.View.PopulateColumns();

        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            grdMain.View.DoubleClick += grdMain_DoubleClick;
            grdMain.View.FocusedRowChanged += View_FocusedRowChanged;

            //Add Row 가 있을 때 Delete버튼 생성
            new SetGridDeleteButonVisibleResizeExport(grdSub);
            grdSub.View.DoubleClick += grdSub_View_DoubleClick;
            //CoboBoxSettingEnterprise();//임시

            //grdSub Adding New Row 이벤트
            grdSub.ToolbarAddingRow += (s, e) => 
            {
                DataRow row = grdMain.View.GetFocusedDataRow();

                if (row == null) e.Cancel = true;
            };
            grdSub.View.AddingNewRow += View_AddingNewRow;

            btnSaveSubData.Click += btnSaveSubData_Click;

        }


        /// <summary>
        /// grdSub에 row를 추가 할 때 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            DataRow row = grdMain.View.GetFocusedDataRow();

            if (row == null) return;

            args.NewRow["MEASUREDATE"] = DateTime.Now.ToString("yyyy-MM-dd");
            args.NewRow["SERIALNO"] = row["SERIALNO"];
            args.NewRow["CONTROLNO"] = row["CONTROLNO"];

        }
        #region

        /// <summary>
        /// 2020-02-07 강유라
        /// grdMain 포커스 row changed이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow row = grdMain.View.GetDataRow(e.FocusedRowHandle);
            grdMainFocusedRowChanged(row);
        }

        /// <summary>
        /// 2020-02-07 강유라
        /// grdMain row 변경 함수
        /// </summary>
        /// <param name="row"></param>
        private void grdMainFocusedRowChanged(DataRow row)
        {
            try
            {
                OnSearchMeasuringRNRUserList(row);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }

        }

        /// <summary>
        /// 그리드 View DoubleClick 이벤트.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdMain_DoubleClick(object sender, System.EventArgs e)
        {

            // 등록 팝업
            DataRow row = grdMain.View.GetFocusedDataRow();
            if (row == null) return;

            DXMouseEventArgs args = e as DXMouseEventArgs;
            GridView view = sender as GridView;
            GridHitInfo info = view.CalcHitInfo(args.Location);

            DialogManager.ShowWaitArea(pnlContent);
            MeasuringCardInformationPopup frmPopup = new MeasuringCardInformationPopup(false);
            frmPopup.isReadOnlyControls = true;
            frmPopup.SetRowParent(row);
            frmPopup.StartPosition = FormStartPosition.CenterParent;//폼 중앙 배치.
            frmPopup.FormBorderStyle = FormBorderStyle.Sizable;
            frmPopup.Owner = this;
            //frmPopup.SetReadOnlyControls();2020-03-24 강유라 public -> private
            frmPopup.SaveButtonVisible(false);
            DialogManager.CloseWaitArea(pnlContent);

            frmPopup.ShowDialog();

        }
        /// <summary>
        /// 그리드 sub DoubleClick 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdSub_View_DoubleClick(object sender, EventArgs e)
        {
            DataRow row = grdSub.View.GetFocusedDataRow();
            // 등록 팝업
            if (row == null) return;

            DataTable dtGridSub = grdSub.GetChangedRows();
           
            //신규, 수정 데이터 있다면 팝업 열지 않음
            if (dtGridSub != null && dtGridSub.Rows.Count > 0)
            {
                return;
            }

            //등록된 파일 없을때 팝업 열지 않음
            if (string.IsNullOrWhiteSpace(Format.GetString(row["REPORTFILEPATH"])) ||
                string.IsNullOrWhiteSpace(Format.GetString(row["REPORTSAFEFILENAME"])))
            {
                throw MessageException.Create("NoFileMasuremnentReport");
            }

            DialogManager.ShowWaitArea(pnlContent);

            DXMouseEventArgs args = e as DXMouseEventArgs;
            GridView view = sender as GridView;
            GridHitInfo info = view.CalcHitInfo(args.Location);
         
            MeasuringInstRNRReportPopup frmPopup = new MeasuringInstRNRReportPopup();
            frmPopup.StartPosition = FormStartPosition.CenterParent;//폼 중앙 배치.
            frmPopup.Owner = this;
            frmPopup.CurrentDataRow = row;
            //frmPopup.EnterpriseValue = this.cboEnterprise.GetDataValue().ToSafeString();
            frmPopup.InitializeReport();
            DialogManager.CloseWaitArea(pnlContent);
            frmPopup.ShowDialog();
            
        }

 
        #endregion

        #endregion

        #region 툴바

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {

        }

        #endregion

        #region 검색

        /// <summary>
        /// 조회 - 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            var value = Conditions.GetValues();
            value.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);
            value.Add("ISDISPLAY", "Y");
            //value.Add("WORKCONDITION", "Enable");

            if (await SqlExecuter.QueryAsync("SelectRawMeasuringCardInformation", "10001", value) is DataTable dt)
            {
                if (dt.Rows.Count < 1)
                {
                    // 조회할 데이터가 없습니다.
                    ShowMessage("NoSelectData");
                }

                grdMain.DataSource = dt;

                DataRow row = grdMain.View.GetFocusedDataRow();
                grdMainFocusedRowChanged(row);
            }
        }
        /// <summary>
        /// 검색 조건 설정.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            //발생일자.
            DateTime dateNow = DateTime.Now;
            string strFirstDate = dateNow.ToString("yyyy-MM-01");
            string strEndDate = dateNow.ToString("yyyy-12-31");
            var dtStart = Conditions.AddDateEdit("P_PERIOD_PERIODFR")
               .SetLabel("MEASURINGDATEOFOCCURRENCE")
               .SetDisplayFormat("yyyy-MM-dd")
               .SetPosition(0.1)
               .SetDefault(strFirstDate)
               .SetValidationIsRequired();
            var dtEnd = Conditions.AddDateEdit("P_PERIOD_PERIODTO")
               .SetLabel("")
               .SetDisplayFormat("yyyy-MM-dd")
               .SetPosition(0.1)
               .SetDefault(strEndDate)
               .SetValidationIsRequired();

            //Site 공장
            /*
            this.Conditions.AddComboBox("p_plantId", new SqlQuery("GetPlantListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "PLANTNAME", "PLANTID")
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                .SetDefault(UserInfo.Current.Plant)
                .SetValidationIsRequired()
                .SetLabel("PLANT")
                .SetPosition(1.1); //표시순서 지정*/

            //Site 공장
            this.Conditions.AddTextBox("p_plantId")
                .SetLabel("PLANT")
                .SetPosition(1.1); //표시순서 지정

            //장비구분
            this.Conditions.AddComboBox("p_measurementtype", new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CODECLASSID={"MeasurementType"}"), "CODENAME", "CODEID")
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                //.SetEmptyItem()
                .SetValidationIsRequired()
                .SetLabel("MEASURINGEQUIPMENTTYPE")
                .SetDefault("Measurement")
                .SetPosition(2.1); // Subgroup 조회조건 
                
            //계측기 
            this.InitializeConditionMeasure_Popup();
         
        }

        #region 조회조건 팝업 초기화
        /// <summary>
        /// 팝업형 조회조건 생성 - 계측기 정보
        /// </summary>
        private void InitializeConditionMeasure_Popup()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            var popup = this.Conditions.AddSelectPopup("P_MEASURINGID", new SqlQuery("GetMeasuringIdList", "10001", param), "MEASURINGID", "MEASURINGID")
                .SetPopupLayout("MEASURINGEQUIPMENTID", PopupButtonStyles.Ok_Cancel, true, true)
                .SetPopupResultCount(1)
                .SetPopupLayoutForm(400, 600, FormBorderStyle.FixedToolWindow)
                //.SetPopupAutoFillColumns("AREANAME")
                .SetLabel("MEASURINGEQUIPMENTID")
                .SetRelationIds("P_PLANTID")
                .SetPopupAutoFillColumns("MEASURINGID")
                .SetPosition(2.2);

            popup.Conditions.AddTextBox("MEASURINGID").SetLabel("MEASURINGEQUIPMENTID");
            popup.GridColumns.AddTextBoxColumn("MEASURINGID", 150).SetLabel("MEASURINGEQUIPMENTID");
        }

        #endregion

        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();
        }
        /// <summary>
        /// 팝업형 조회조건 생성 - 계측기 정보
        /// </summary>
        private void OnSearchMeasuringRNRUserList(DataRow rowMain)
        {
            if (rowMain == null )
            {
                this.grdSub.DataSource = null;
                return;
            }

            string plantID = rowMain["PLANTID"].ToSafeString();
            string serialNo = rowMain["SERIALNO"].ToSafeString();
            string controlNo = rowMain["CONTROLNO"].ToSafeString();

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("P_PLANTID", plantID);
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("P_SERIALNO", serialNo);
            param.Add("P_CONTROLNO", controlNo);

            DataTable rnrUserTable = SqlExecuter.Query("SelectRawMeasuringInstRNR", "10001", param);
           
            this.grdSub.DataSource = rnrUserTable;
        
        }
        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            grdSub.View.CheckValidation();

            if (grdSub.GetChangedRows().Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }

        #endregion

        #region Private Function





        /// <summary>
        /// 저장 버튼 Click 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveSubData_Click(object sender, EventArgs e)
        {
            //// TODO : 저장 Rule 변경
           
                //base.OnToolbarSaveClick();
                // TODO : 저장 Rule 변경
                DataTable dt = grdSub.GetChangedRows();

                if (dt == null || dt.Rows.Count == 0)
                {
                    throw MessageException.Create("NoSaveData");
                }

 
                if (ErrorCount(dt) > 0)
                {
                    throw MessageException.Create("InValidRequiredField", Language.Get("MEASURER"));
                }

            try
            {
    
                if (this.ShowMessage(MessageBoxButtons.YesNo, "InfoSave") == DialogResult.Yes)
                {
                    this.ShowWaitArea();

                    ExecuteRule("SaveMeasuringInstRNR", dt);

                    //다시 조회.
                    DataRow row = grdMain.View.GetFocusedDataRow();
                    grdMainFocusedRowChanged(row);

                    ShowMessage("SuccedSave");
                }                   

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
        /// 필수값이 입력 되지않은 row수 
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private int ErrorCount(DataTable dt)
        {
            int errorCount = dt.AsEnumerable().Where(r =>
                string.IsNullOrWhiteSpace(Format.GetString(r["SERIALNO"])) ||
                string.IsNullOrWhiteSpace(Format.GetString(r["CONTROLNO"])) ||
                string.IsNullOrWhiteSpace(Format.GetString(r["MEASUREDATE"])) ||
                string.IsNullOrWhiteSpace(Format.GetString(r["MEASUREUSERA"])) ||
                string.IsNullOrWhiteSpace(Format.GetString(r["MEASUREUSERB"])) ||
                string.IsNullOrWhiteSpace(Format.GetString(r["MEASUREUSERC"])))
                .Count();

            return errorCount;
        }
        #endregion

        #region Enterprise 사업장 직접 Data입력형.
        /// <summary>
        /// 콤보박스 설정 - Enterprise
        /// </summary>
        public void CoboBoxSettingEnterprise()
        {/*
            DataTable cboEnterpriseDataTable = CreateDataTableAnalysisEnterprise();
            this.cboEnterprise.DataSource = cboEnterpriseDataTable;
            this.cboEnterprise.DisplayMember = "Label";
            this.cboEnterprise.ValueMember = "nValue";
            this.cboEnterprise.Columns[1].Visible = false;
            this.cboEnterprise.ShowHeader = false;
            this.cboEnterprise.ItemIndex = 0;*/
        }
        /// <summary>
        /// 콤보박스 입력 iTem 생성 함수. (사업장)
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataTable CreateDataTableAnalysisEnterprise(string tableName = "dtCboEnterprise")
        {
            DataTable dt = new DataTable();
            dt.TableName = tableName;
            //Filed
            dt.Columns.Add("Label", typeof(string));
            dt.Columns.Add("nValue", typeof(string));

            DataRow row;
            row = dt.NewRow(); row["Label"] = "인터"; row["nValue"] = "INTERFLEX"; dt.Rows.Add(row);
            row = dt.NewRow(); row["Label"] = "영풍"; row["nValue"] = "YOUNGPOONG"; dt.Rows.Add(row);

            return dt;
        }
        #endregion Enterprise 사업장 직접 Data입력형.


    }//end class
}
