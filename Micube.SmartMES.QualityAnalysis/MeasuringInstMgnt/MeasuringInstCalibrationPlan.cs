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
using System;
using System.Collections.Generic;
using Micube.Framework.Log;
using System.Linq;

#endregion

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 계측기 관리> 계측기 관리대장
    /// 업  무  설  명  : 계측기 관리사항을 조회함.
    /// 생    성    자  : 이승우
    /// 생    성    일  : 2019-10-01
    /// 수  정  이  력  : 2019-10-29 Layout 변경
    ///                  2019-10-01 최초 생성일
    /// 
    /// 
    /// </summary>
    public partial class MeasuringInstCalibrationPlan : SmartConditionManualBaseForm
    {
        #region Local Variables
        
        #endregion

        #region 생성자

        public MeasuringInstCalibrationPlan()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// Form Controls 초기화 
        /// </summary>
        protected override void InitializeContent()
        {
            base.InitializeContent();

            //SetComboBoxCreateValueQuarterType();

            InitializeEvent();
            //InitializeGrid();
            InitializeCalibration();
            InitializeMiddle();
            InitializeRNR();
            //InitializeLanguageKey();
        }

        /// <summary>
        /// Grid 초기화 교정
        /// </summary>
        private void InitializeCalibration()
        {
            int columnWith = 90;
            grdCalibration.GridButtonItem = GridButtonItem.Export | GridButtonItem.Expand | GridButtonItem.Restore;
            grdCalibration.View.SetIsReadOnly();
            //grdCalibration.View.AddTextBoxColumn("SERIALNO", 150).SetLabel("MEASURINGSERIALNO").SetIsReadOnly();

            var groupFirst = this.grdCalibration.View.AddGroupColumn("MODE");//구분
            groupFirst.AddTextBoxColumn("MODENAME", 150).SetLabel(" ").SetTextAlignment(TextAlignment.Center);

            var groupMain = this.grdCalibration.View.AddGroupColumn("MEASURINGMONTHLYREMEDIATIONTARGETQUANTITY");//월별 교정 대상 수량
            //specInfoGroup.AddTextBoxColumn("", 150);
            var groupSub01 = groupMain.AddGroupColumn("1x").SetLabel("1");
            groupSub01.AddTextBoxColumn("C01", columnWith).SetLabel("1").SetTextAlignment(TextAlignment.Right);
            groupSub01.AddTextBoxColumn("C02", columnWith).SetLabel("2").SetTextAlignment(TextAlignment.Right);
            groupSub01.AddTextBoxColumn("C03", columnWith).SetLabel("3").SetTextAlignment(TextAlignment.Right);

            var groupSub02 = groupMain.AddGroupColumn("2x").SetLabel("2");
            groupSub02.AddTextBoxColumn("C04", columnWith).SetLabel("4").SetTextAlignment(TextAlignment.Right);
            groupSub02.AddTextBoxColumn("C05", columnWith).SetLabel("5").SetTextAlignment(TextAlignment.Right);
            groupSub02.AddTextBoxColumn("C06", columnWith).SetLabel("6").SetTextAlignment(TextAlignment.Right);

            var groupSub03 = groupMain.AddGroupColumn("3x").SetLabel("3");
            groupSub03.AddTextBoxColumn("C07", columnWith).SetLabel("7").SetTextAlignment(TextAlignment.Right);
            groupSub03.AddTextBoxColumn("C08", columnWith).SetLabel("8").SetTextAlignment(TextAlignment.Right);
            groupSub03.AddTextBoxColumn("C09", columnWith).SetLabel("9").SetTextAlignment(TextAlignment.Right);

            var groupSub04 = groupMain.AddGroupColumn("4x").SetLabel("4");
            groupSub04.AddTextBoxColumn("C10", columnWith).SetLabel("10").SetTextAlignment(TextAlignment.Right);
            groupSub04.AddTextBoxColumn("C11", columnWith).SetLabel("11").SetTextAlignment(TextAlignment.Right);
            groupSub04.AddTextBoxColumn("C12", columnWith).SetLabel("12").SetTextAlignment(TextAlignment.Right);

            var groupSubTotal = this.grdCalibration.View.AddGroupColumn("MEASURINGOVERALLPROGRESSRATE");//전체진행율
            groupSubTotal.AddTextBoxColumn("TOTAL", columnWith).SetLabel(" ").SetTextAlignment(TextAlignment.Right);

            grdCalibration.View.PopulateColumns();
        }
        /// <summary>
        /// Grid 초기화 중간
        /// </summary>
        private void InitializeMiddle()
        {
            int columnWith = 90;
            grdMiddle.GridButtonItem = GridButtonItem.Export | GridButtonItem.Expand | GridButtonItem.Restore;
            grdMiddle.View.SetIsReadOnly();
            //grdMiddle.View.AddTextBoxColumn("SERIALNO", 150).SetLabel("MEASURINGSERIALNO").SetIsReadOnly();

            var groupFirst = this.grdMiddle.View.AddGroupColumn("MODE");//구분
            groupFirst.AddTextBoxColumn("MODENAME", 150).SetLabel(" ").SetTextAlignment(TextAlignment.Center);

            var groupMain = this.grdMiddle.View.AddGroupColumn("MEASURINGMONTHLYINTERIMCHECKTARGET");//"월별 중간 점검 대상 수량"
            //specInfoGroup.AddTextBoxColumn("", 150);
            var groupSub01 = groupMain.AddGroupColumn("1x").SetLabel("1");
            groupSub01.AddTextBoxColumn("C01", columnWith).SetLabel("1").SetTextAlignment(TextAlignment.Right);
            groupSub01.AddTextBoxColumn("C02", columnWith).SetLabel("2").SetTextAlignment(TextAlignment.Right);
            groupSub01.AddTextBoxColumn("C03", columnWith).SetLabel("3").SetTextAlignment(TextAlignment.Right);

            var groupSub02 = groupMain.AddGroupColumn("2x").SetLabel("2");
            groupSub02.AddTextBoxColumn("C04", columnWith).SetLabel("4").SetTextAlignment(TextAlignment.Right);
            groupSub02.AddTextBoxColumn("C05", columnWith).SetLabel("5").SetTextAlignment(TextAlignment.Right);
            groupSub02.AddTextBoxColumn("C06", columnWith).SetLabel("6").SetTextAlignment(TextAlignment.Right);

            var groupSub03 = groupMain.AddGroupColumn("3x").SetLabel("3");
            groupSub03.AddTextBoxColumn("C07", columnWith).SetLabel("7").SetTextAlignment(TextAlignment.Right);
            groupSub03.AddTextBoxColumn("C08", columnWith).SetLabel("8").SetTextAlignment(TextAlignment.Right);
            groupSub03.AddTextBoxColumn("C09", columnWith).SetLabel("9").SetTextAlignment(TextAlignment.Right);

            var groupSub04 = groupMain.AddGroupColumn("4x").SetLabel("4");
            groupSub04.AddTextBoxColumn("C10", columnWith).SetLabel("10").SetTextAlignment(TextAlignment.Right);
            groupSub04.AddTextBoxColumn("C11", columnWith).SetLabel("11").SetTextAlignment(TextAlignment.Right);
            groupSub04.AddTextBoxColumn("C12", columnWith).SetLabel("12").SetTextAlignment(TextAlignment.Right);

            var groupSubTotal = this.grdMiddle.View.AddGroupColumn("MEASURINGOVERALLPROGRESSRATE");//전체진행율
            groupSubTotal.AddTextBoxColumn("TOTAL", columnWith).SetLabel(" ").SetTextAlignment(TextAlignment.Right);

            grdMiddle.View.PopulateColumns();
        }
        /// <summary>
        /// Grid 초기화 R&R
        /// </summary>
        private void InitializeRNR()
        {
            int columnWith = 90;
            grdRNR.GridButtonItem = GridButtonItem.Export | GridButtonItem.Expand | GridButtonItem.Restore;
            grdRNR.View.SetIsReadOnly();
            //grdRNR.View.AddTextBoxColumn("SERIALNO", 150).SetLabel("MEASURINGSERIALNO").SetIsReadOnly();

            var groupFirst = this.grdRNR.View.AddGroupColumn("MODE");//구분
            groupFirst.AddTextBoxColumn("MODENAME", 150).SetLabel(" ").SetTextAlignment(TextAlignment.Center);

            var groupMain = this.grdRNR.View.AddGroupColumn("MEASURINGMONTHLYRNRTARGETQUANTITY");//월별 R&R 대상 수량
            //specInfoGroup.AddTextBoxColumn("", 150);
            var groupSub01 = groupMain.AddGroupColumn("1x").SetLabel("1");
            groupSub01.AddTextBoxColumn("C01", columnWith).SetLabel("1").SetTextAlignment(TextAlignment.Right);
            groupSub01.AddTextBoxColumn("C02", columnWith).SetLabel("2").SetTextAlignment(TextAlignment.Right);
            groupSub01.AddTextBoxColumn("C03", columnWith).SetLabel("3").SetTextAlignment(TextAlignment.Right);

            var groupSub02 = groupMain.AddGroupColumn("2x").SetLabel("2");
            groupSub02.AddTextBoxColumn("C04", columnWith).SetLabel("4").SetTextAlignment(TextAlignment.Right);
            groupSub02.AddTextBoxColumn("C05", columnWith).SetLabel("5").SetTextAlignment(TextAlignment.Right);
            groupSub02.AddTextBoxColumn("C06", columnWith).SetLabel("6").SetTextAlignment(TextAlignment.Right);

            var groupSub03 = groupMain.AddGroupColumn("3x").SetLabel("3");
            groupSub03.AddTextBoxColumn("C07", columnWith).SetLabel("7").SetTextAlignment(TextAlignment.Right);
            groupSub03.AddTextBoxColumn("C08", columnWith).SetLabel("8").SetTextAlignment(TextAlignment.Right);
            groupSub03.AddTextBoxColumn("C09", columnWith).SetLabel("9").SetTextAlignment(TextAlignment.Right);

            var groupSub04 = groupMain.AddGroupColumn("4x").SetLabel("4");
            groupSub04.AddTextBoxColumn("C10", columnWith).SetLabel("10").SetTextAlignment(TextAlignment.Right);
            groupSub04.AddTextBoxColumn("C11", columnWith).SetLabel("11").SetTextAlignment(TextAlignment.Right);
            groupSub04.AddTextBoxColumn("C12", columnWith).SetLabel("12").SetTextAlignment(TextAlignment.Right);

            var groupSubTotal = this.grdRNR.View.AddGroupColumn("MEASURINGOVERALLPROGRESSRATE");//전체진행율
            groupSubTotal.AddTextBoxColumn("TOTAL", columnWith).SetLabel(" ").SetTextAlignment(TextAlignment.Right);

            grdRNR.View.PopulateColumns();
        }

        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            grdCalibration.GridButtonItem = GridButtonItem.Add | GridButtonItem.Copy;
            grdCalibration.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdCalibration.View.SetSortOrder("SPECSEQUENCE");
            grdCalibration.View.BestFitColumns();

            grdCalibration.View.AddTextBoxColumn("SPECSEQUENCE", 150)
                .SetIsHidden();


            grdCalibration.View.AddTextBoxColumn("SPECCLASSID", 150)
                 .SetIsHidden()
                 .SetDefault("EtchingRateSpec");


            grdCalibration.View.AddTextBoxColumn("PLANTID", 150);

            grdCalibration.View.AddTextBoxColumn("ENTERPRISEID", 150)
                .SetIsHidden();

            #region 공정 팝업 PROCESSSEGMENTCLASSID
            var ProcessSegmentClassId = grdCalibration.View.AddSelectPopupColumn("PROCESSSEGMENTCLASSNAME", new SqlQuery("GetProcessSegmentClass", "10001", "PROCESSSEGMENTCLASSTYPE=TopProcessSegmentClass", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"),"PROCESSSEGMENTCLASSID")
                                               .SetPopupLayout("TOPPROCESSSEGMENTCLASS", PopupButtonStyles.Ok_Cancel, true, true)
                                               .SetLabel("TOPPROCESSSEGMENTCLASSNAME")
                                               .SetPopupAutoFillColumns("PROCESSSEGMENTCLASSNAME")
                                               .SetPopupResultCount(1)
                                               .SetPopupLayoutForm(500, 600, FormBorderStyle.FixedToolWindow)
                                               //.SetRelationIds("PLANTID")
                                               .SetValidationKeyColumn();
            //.SetPopupQueryPopup((DataRow currentrow) =>
            //{
            //    if (string.IsNullOrWhiteSpace(currentrow.ToStringNullToEmpty("PLANTID")))
            //    {
            //        this.ShowMessage("NoSelectSite");
            //        return false;
            //    }

            //    return true;
            //});

            ProcessSegmentClassId.Conditions.AddTextBox("PROCESSSEGMENTCLASS")
                .SetLabel("LARGEPROCESSSEGMENTIDNAME");

            ProcessSegmentClassId.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 150)
                .SetLabel("TOPPROCESSSEGMENTCLASSID");
            ProcessSegmentClassId.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 200)
                .SetLabel("TOPPROCESSSEGMENTCLASSNAME");

            grdCalibration.View.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 150)
                .SetLabel("TOPPROCESSSEGMENTCLASSID")
                .SetIsHidden();
            #endregion

            #region 설비(호기) 팝업 EQUIPMENTID
            var equipmentId = grdCalibration.View.AddSelectPopupColumn("EQUIPMENTNAME", new SqlQuery("GetEquipmentByClassHierarchy", "10001", "DETAILEQUIPMENTTYPE=Main", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "EQUIPMENTID")
                                     .SetPopupLayout("EQUIPMENT", PopupButtonStyles.Ok_Cancel, true, false)
                                     .SetPopupResultCount(1)
                                     .SetValidationKeyColumn()
                                     .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                                     .SetPopupAutoFillColumns("EQUIPMENTNAME");

            equipmentId.Conditions.AddComboBox("PARENTEQUIPMENTCLASSID", new SqlQuery("GetEquipmentClassHierarchyAndType", "10001", "HIERARCHY=TopEquipment", "EQUIPMENTCLASSTYPE=Production", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "EQUIPMENTCLASSNAME", "EQUIPMENTCLASSID")
                                  .SetValidationKeyColumn()
                                  .SetLabel("TOPEQUIPMENTCLASS");

            equipmentId.Conditions.AddComboBox("MIDDLEEQUIPMENTCLASSID", new SqlQuery("GetEquipmentClassHierarchyAndType", "10001", "HIERARCHY=MiddleEquipment", "EQUIPMENTCLASSTYPE=Production", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "EQUIPMENTCLASSNAME", "EQUIPMENTCLASSID")
                                  .SetValidationKeyColumn()
                                  .SetRelationIds("PARENTEQUIPMENTCLASSID")
                                  .SetLabel("MIDDLEEQUIPMENTCLASS");

            equipmentId.Conditions.AddTextBox("EQUIPMENT")
                .SetLabel("EQUIPMENTIDNAME");

            equipmentId.Conditions.AddTextBox("PROCESSSEGMENTCLASSID")
                .SetPopupDefaultByGridColumnId("PROCESSSEGMENTCLASSID")
                .SetIsHidden();

            // 팝업 그리드
            equipmentId.GridColumns.AddTextBoxColumn("EQUIPMENTCLASSNAME", 150);
            equipmentId.GridColumns.AddTextBoxColumn("EQUIPMENTID", 150);
            equipmentId.GridColumns.AddTextBoxColumn("EQUIPMENTNAME", 200);

            grdCalibration.View.AddTextBoxColumn("EQUIPMENTID", 150)
                .SetIsHidden();

            #endregion

            #region 설비단 팝업 CHILDEQUIPMENTID
            var childEquipementId = grdCalibration.View.AddSelectPopupColumn("CHILDEQUIPMENTNAME", new SqlQuery("GetEquipmentListByDetailType", "10001", "EQUIPMENTCLASSTYPE=Production", "DETAILEQUIPMENTTYPE=Sub", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                                .SetPopupLayout("CHILDEQUIPMENT", PopupButtonStyles.Ok_Cancel, true, false)
                                                .SetPopupResultCount(1)
                                                .SetPopupResultMapping("CHILDEQUIPMENTNAME", "EQUIPMENTNAME")
                                                .SetPopupLayoutForm(500, 600, FormBorderStyle.FixedToolWindow)
                                                .SetPopupAutoFillColumns("EQUIPMENTNAME")
                                                .SetLabel("CHILDEQUIPMENTNAME")
                                                .SetRelationIds("PARENTEQUIPMENTID")
                                                .SetValidationKeyColumn()
                                                .SetPopupQueryPopup((DataRow currentrow) =>
                                                {
                                                    if (string.IsNullOrWhiteSpace(currentrow.ToStringNullToEmpty("EQUIPMENTID")))
                                                    {
                                                        this.ShowMessage("NoSelectParentEquipment");
                                                        return false;
                                                    }

                                                    return true;
                                                })
                                                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                                                {
                                                    // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
                                                    // dataGridRow : 현재 Focus가 있는 그리드의 DataRow

                                                    foreach (DataRow row in selectedRows)
                                                    {
                                                        dataGridRow["CHILDEQUIPMENTID"] = row["EQUIPMENTID"].ToString();
                                                    }

                                                });

            childEquipementId.Conditions.AddTextBox("EQUIPMENT")
                .SetLabel("CHILDEQUIPMENTIDNAME");

            childEquipementId.Conditions.AddTextBox("PARENTEQUIPMENTID")
                                        .SetPopupDefaultByGridColumnId("EQUIPMENTID")
                                        .SetIsHidden();
            // 팝업 그리드
            childEquipementId.GridColumns.AddTextBoxColumn("EQUIPMENTID", 150)
                .SetLabel("CHILDEQUIPMENTID");
            childEquipementId.GridColumns.AddTextBoxColumn("EQUIPMENTNAME", 200)
                .SetLabel("CHILDEQUIPMENTNAME");

            grdCalibration.View.AddTextBoxColumn("CHILDEQUIPMENTID", 150)
               .SetIsHidden();

            #endregion

            grdCalibration.View.AddTextBoxColumn("WORKTYPE", 100)
                .SetLabel("TYPECONDITION")
                .SetValidationKeyColumn()
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left);

            grdCalibration.View.AddTextBoxColumn("SPECRANGE", 140)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left);

            grdCalibration.View.AddTextBoxColumn("CONTROLRANGE", 140)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left);

            grdCalibration.View.AddTextBoxColumn("DESCRIPTION", 150)
                .SetIsReadOnly();

            grdCalibration.View.AddTextBoxColumn("WORKCONDITION", 100)
                .SetIsHidden();

            grdCalibration.View.AddTextBoxColumn("CHANGEREASON", 100)
               .SetIsHidden();

            grdCalibration.View.AddComboBoxColumn("DEFAULTCHARTTYPE", 100, new SqlQuery("GetTypeList", "10001", "CODECLASSID=ControlType", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}"))
               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false)
               .SetIsHidden();

            grdCalibration.View.PopulateColumns();

        }

        /// <summary>
        /// Language Key 초기화
        /// </summary>
        private void InitializeLanguageKey()
        {
            grdCalibration.LanguageKey = "MEASUREDVALUE";
        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            //Add Row 가 있을 때 Delete버튼 생성
            new SetGridDeleteButonVisibleSimple(grdCalibration);

            grdCalibration.View.AddingNewRow += (s, e) =>
              {
                  //e.NewRow["ENTERPRISEID"] = Framework.UserInfo.Current.Enterprise;
                  //e.NewRow["PLANTID"] = Framework.UserInfo.Current.Plant;
              };
      

            // 메인 그리드 더블클릭 이벤트
            grdCalibration.View.DoubleClick += (s, e) =>
            {
                
            };
        }

        #region Form 이벤트
        /// <summary>
        /// Form Load 이벤트.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MeasuringInstCalibrationPlan_Load(object sender, System.EventArgs e)
        {
            DictionaryDataSetting();
        }

        #endregion

        #endregion

        #region 툴바

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();
            DataTable dt = SetRowState(grdCalibration.GetChangedRows());
            ExecuteRule("SaveEtchingRateSpecDef", dt);
        }

        #endregion

        #region 검색

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();
            int i;
            var value = Conditions.GetValues();
            value.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);
            value.Add("WORKCONDITION", "Enable");

            bool isDataCheck = false;
            this.grdCalibration.DataSource = null;
            this.grdMiddle.DataSource = null;
            this.grdRNR.DataSource = null;

            if (await SqlExecuter.QueryAsync("SelectRawMeasuringInstCalibrationPlanCalibration", "10001", value) is DataTable dt1)
            {
                if (dt1.Rows.Count > 0)
                {
                    isDataCheck = true;
                    this.grdCalibration.DataSource = EidtDbData(dt1, "Calibration", 1);
                }
            }

            if (await SqlExecuter.QueryAsync("SelectRawMeasuringInstCalibrationPlanMiddle", "10001", value) is DataTable dt2)
            {
                if (dt2.Rows.Count > 0)
                {
                    isDataCheck = true;
                    this.grdMiddle.DataSource = EidtDbData(dt2, "Middle", 2);
                }
            }

            if (await SqlExecuter.QueryAsync("SelectRawMeasuringInstCalibrationPlanRNR", "10001", value) is DataTable dt3)
            {
                if (dt3.Rows.Count > 0)
                {
                    isDataCheck = true;
                    this.grdRNR.DataSource = EidtDbData(dt3, "RNR", 3);
                }
            }

            if (!isDataCheck)
            {
                // 조회할 데이터가 없습니다.
                ShowMessage("NoSelectData");
            }

        }


        /// <summary>
        /// DB 조회 자료 업무별 편집 후 반환.
        /// </summary>
        /// <param name="dtReadData"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        private DataTable EidtDbData(DataTable dtReadData, string tableName, int mode)
        {
            int i = 0;
            string fieldName = "";
            string sumCnt = "";
            string sumCp = "";
            double nSumCnt = 0;
            double nSumCp = 0;
            double ntotRate = 0;
            double ntotRatePercent = 0;

            DataTable dtResult = this.CreateTableGrid("tableName");

            try
            {
                DataRow drRowCal1 = dtResult.NewRow();
                DataRow drRowCal2 = dtResult.NewRow();
                DataRow drRowCal3 = dtResult.NewRow();
                DataRow drRowCal4 = dtResult.NewRow();

                switch (mode)
                {
                    case 1:
                        drRowCal1["MODENAME"] = "월별대상수량";
                        drRowCal2["MODENAME"] = "교정완료";
                        drRowCal3["MODENAME"] = "검교정진행율(%)";
                        drRowCal4["MODENAME"] = "검교정진행율";
                        break;
                    case 2:
                        drRowCal1["MODENAME"] = "월별대상수량";
                        drRowCal2["MODENAME"] = "교정완료";
                        drRowCal3["MODENAME"] = "중간점검진행율(%)";
                        drRowCal4["MODENAME"] = "중간점검진행율";
                        break;
                    case 3:
                        drRowCal1["MODENAME"] = "월별대상수량";
                        drRowCal2["MODENAME"] = "교정완료";
                        drRowCal3["MODENAME"] = "R&R진행율(%)";
                        drRowCal4["MODENAME"] = "R&R진행율";
                        break;
                    default:
                        break;
                }

                for (i = 0; i < dtReadData.Rows.Count; i++)
                {
                    DataRow drSrc = dtReadData.Rows[i];
                    fieldName = drSrc["FILEDNAME"].ToString();
                    sumCnt = drSrc["CNT"].ToString();//Count.
                    sumCp = drSrc["CP"].ToString();//Completion.
                    nSumCnt += sumCnt.ToSafeDoubleZero();
                    nSumCp += sumCp.ToSafeDoubleZero();

                    drRowCal1[fieldName] = sumCnt;
                    drRowCal2[fieldName] = sumCp;
                    drRowCal3[fieldName] = drSrc["RATEPERCENT"].ToString();
                    drRowCal4[fieldName] = drSrc["RATE"].ToString();
                }

                if (nSumCp != 0 && nSumCnt != 0)
                {
                    ntotRate = nSumCp / nSumCnt;
                    ntotRatePercent = Math.Round(ntotRate, 4) * 100;
                }

                drRowCal1["TOTAL"] = nSumCnt.ToString();
                drRowCal2["TOTAL"] = nSumCp.ToString();
                drRowCal3["TOTAL"] = ntotRatePercent.ToString();
                drRowCal4["TOTAL"] = ntotRate.ToString();

                dtResult.Rows.Add(drRowCal1);
                dtResult.Rows.Add(drRowCal2);
                dtResult.Rows.Add(drRowCal3);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }

            return dtResult;

        }

        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            //발생일자.
            DateTime dateNow = DateTime.Now;
            string strFirstDate = dateNow.ToString("yyyy-01-01");
            string strEndDate = dateNow.ToString("yyyy-12-31");
            var dtStart = Conditions.AddDateEdit("P_PERIOD_PERIODFR")
               .SetLabel("MEASURINGDATEOFOCCURRENCE") 
               .SetDisplayFormat("yyyy-MM-dd")
               .SetPosition(0.1)
               .SetDefault(strFirstDate)
               .SetValidationIsRequired();
            var dtEnd= Conditions.AddDateEdit("P_PERIOD_PERIODTO")
               .SetLabel("")
               .SetDisplayFormat("yyyy-MM-dd")
               .SetPosition(0.1)
               .SetDefault(strEndDate)
               .SetValidationIsRequired();

            /*
            //Site 공장
            this.Conditions.AddComboBox("p_plantId", new SqlQuery("GetPlantListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "PLANTNAME", "PLANTID")
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                .SetDefault(UserInfo.Current.Plant)
                .SetValidationIsRequired()
                .SetLabel("PLANT")
                .SetPosition(1.1); //표시순서 지정
            */

            //Site 공장
            this.Conditions.AddTextBox("p_plantId")
                .SetLabel("PLANT")
                .SetPosition(1.1); //표시순서 지정


            //장비구분
            this.Conditions.AddComboBox("p_inspectionResultResouceType", new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CODECLASSID={"MeasurementType"}"), "CODENAME", "CODEID")
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                .SetDefault("Measurement")
                //.SetValidationIsRequired()
                .SetLabel("MEASURINGEQUIPMENTTYPE")
                .SetPosition(2.1); // Subgroup 조회조건 

            //계측기 
            this.InitializeConditionEquipmentInfo_Popup();

            //부서
            this.InitializeConditionDepartmentInfo_Popup();

            //InitializeConditionPopup_ProcessSegment();
            //InitializeConditionPopup_Equipment();
            //InitializeConditionPopup_ChildEquipment();
        }

        #region 조회조건 팝업 초기화

        /// <summary>
        /// 팝업형 조회조건 생성 - 계측기 정보
        /// </summary>
        private void InitializeConditionEquipmentInfo_Popup()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            var popup = this.Conditions.AddSelectPopup("p_equipmentId", new SqlQuery("GetMeasuringSearchEquipment", "10001", param), "EQUIPMENTNAME", "EQUIPMENTID")
                .SetPopupLayout("EQUIPMENTID", PopupButtonStyles.Ok_Cancel, true, true)
                .SetPopupResultCount(1)
                .SetPopupLayoutForm(1200, 600, FormBorderStyle.FixedToolWindow)
                //.SetPopupAutoFillColumns("AREANAME")
                .SetRelationIds("p_plantId")
                .SetLabel("MEASURINGEQUIPMENTID")
                .SetPosition(2.1);

            popup.GridColumns.AddTextBoxColumn("EQUIPMENTID", 150).SetLabel("EQUIPMENTID");
            popup.GridColumns.AddTextBoxColumn("RECIPECLASSID", 150).SetLabel("RECIPECLASSID");
            popup.GridColumns.AddTextBoxColumn("EQUIPMENTCLASSID", 150).SetLabel("EQUIPMENTCLASSID");
            popup.GridColumns.AddTextBoxColumn("EQUIPMENTNAME", 150).SetLabel("EQUIPMENTNAME");
            popup.GridColumns.AddTextBoxColumn("ENTERPRISEID", 150).SetLabel("ENTERPRISEID");
            popup.GridColumns.AddTextBoxColumn("PLANTID", 150).SetLabel("PLANTID");
            popup.GridColumns.AddTextBoxColumn("AREAID", 150).SetLabel("AREAID");
            popup.GridColumns.AddTextBoxColumn("LOCATIONID", 150).SetLabel("LOCATIONID");
            popup.GridColumns.AddTextBoxColumn("EQUIPMENTTYPE", 150).SetLabel("EQUIPMENTTYPE");
            popup.GridColumns.AddTextBoxColumn("DETAILEQUIPMENTTYPE", 150).SetLabel("DETAILEQUIPMENTTYPE");
            popup.GridColumns.AddTextBoxColumn("PARENTEQUIPMENTID", 150).SetLabel("PARENTEQUIPMENTID");
            popup.GridColumns.AddTextBoxColumn("STATEMODELID", 150).SetLabel("STATEMODELID");
            popup.GridColumns.AddTextBoxColumn("VENDORID", 150).SetLabel("VENDORID");
            popup.GridColumns.AddTextBoxColumn("MODEL", 150).SetLabel("MODEL");
            popup.GridColumns.AddTextBoxColumn("SERIALNO", 150).SetLabel("SERIALNO");
            popup.GridColumns.AddTextBoxColumn("PROCESSUNIT", 150).SetLabel("PROCESSUNIT");
            popup.GridColumns.AddTextBoxColumn("MINCAPACITY", 150).SetLabel("MINCAPACITY");
            popup.GridColumns.AddTextBoxColumn("MAXCAPACITY", 150).SetLabel("MAXCAPACITY");
            popup.GridColumns.AddTextBoxColumn("TACTTIME", 150).SetLabel("TACTTIME");
            popup.GridColumns.AddTextBoxColumn("LEADTIME", 150).SetLabel("LEADTIME");
            popup.GridColumns.AddTextBoxColumn("STATE", 150).SetLabel("STATE");
            popup.GridColumns.AddTextBoxColumn("E10STATE", 150).SetLabel("E10STATE");
            popup.GridColumns.AddTextBoxColumn("CONTROLMODE", 150).SetLabel("CONTROLMODE");
            popup.GridColumns.AddTextBoxColumn("OPERATIONMODE", 150).SetLabel("OPERATIONMODE");
            popup.GridColumns.AddTextBoxColumn("CURRENTRECIPEDEFID", 150).SetLabel("CURRENTRECIPEDEFID");
            popup.GridColumns.AddTextBoxColumn("CURRENTRECIPEDEFVERSION", 150).SetLabel("CURRENTRECIPEDEFVERSION");
            popup.GridColumns.AddTextBoxColumn("PROCESSEDLOTCOUNT", 150).SetLabel("PROCESSEDLOTCOUNT");
            popup.GridColumns.AddTextBoxColumn("EQUIPMENTCHARACTERISTICS", 150).SetLabel("EQUIPMENTCHARACTERISTICS");
            popup.GridColumns.AddTextBoxColumn("SEPARATOR", 150).SetLabel("SEPARATOR");
            popup.GridColumns.AddTextBoxColumn("RESOURCEID", 150).SetLabel("RESOURCEID");
            popup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 150).SetLabel("PROCESSSEGMENTCLASSID");
            popup.GridColumns.AddTextBoxColumn("WORKPLACE", 150).SetLabel("WORKPLACE");
            popup.GridColumns.AddTextBoxColumn("PURCHASETYPE", 150).SetLabel("PURCHASETYPE");
            popup.GridColumns.AddTextBoxColumn("PRODUCTIONTYPE", 150).SetLabel("PRODUCTIONTYPE");
            popup.GridColumns.AddTextBoxColumn("EQUIPMENTLEVEL", 150).SetLabel("EQUIPMENTLEVEL");
            popup.GridColumns.AddTextBoxColumn("PURCHASECOST", 150).SetLabel("PURCHASECOST");
            popup.GridColumns.AddTextBoxColumn("MANUFACTUREDDATE", 150).SetLabel("MANUFACTUREDDATE");
            popup.GridColumns.AddTextBoxColumn("INSTALLATIONDATE", 150).SetLabel("INSTALLATIONDATE");
            popup.GridColumns.AddTextBoxColumn("MANUFACTURECOUNTRY", 150).SetLabel("MANUFACTURECOUNTRY");
            popup.GridColumns.AddTextBoxColumn("MANUFACTURER", 150).SetLabel("MANUFACTURER");
            popup.GridColumns.AddTextBoxColumn("TELNO", 150).SetLabel("TELNO");
            popup.GridColumns.AddTextBoxColumn("ISKPI", 150).SetLabel("ISKPI");
            popup.GridColumns.AddTextBoxColumn("ISCAPA", 150).SetLabel("ISCAPA");
            popup.GridColumns.AddTextBoxColumn("LINKTYPE", 150).SetLabel("LINKTYPE");
            popup.GridColumns.AddTextBoxColumn("PREVLINK", 150).SetLabel("PREVLINK");
            popup.GridColumns.AddTextBoxColumn("AFTERLINK", 150).SetLabel("AFTERLINK");
        }

        /// <summary>
        /// 부석
        /// </summary>
        private void InitializeConditionDepartmentInfo_Popup()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            var popup = this.Conditions.AddSelectPopup("p_departmentInfo", new SqlQuery("GetMeasuringSearchDepartment", "10001", param), "DEPARTMENTNAME", "DEPARTMENTNAME")
                .SetPopupLayout("DEPARTMENTNAME", PopupButtonStyles.Ok_Cancel, true, true)
                //.SetPopupResultCount(1)
                .SetPopupLayoutForm(300, 600, FormBorderStyle.FixedToolWindow)
                //.SetPopupAutoFillColumns("AREANAME")
                .SetRelationIds("p_plantId")
                .SetLabel("MEASURINGDEPARTMENT")
                .SetPosition(3.4);

            popup.GridColumns.AddTextBoxColumn("DEPARTMENTNAME", 150)
                .SetLabel("DEPARTMENTNAME");
        }

     
        #endregion

        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();
        }

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            grdCalibration.View.CheckValidation();

            if (grdCalibration.GetChangedRows().Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }

        #endregion

        #region Private Function
        /// <summary>
        /// 사용 사전 정보 조회
        /// 다국어직접조회
        /// </summary>
        public static void DictionaryDataSetting()
        {
            ConditionCollection Conditions = new ConditionCollection();
            //var row = grdDictionaryClass.View.GetDataRow(grdDictionaryClass.View.FocusedRowHandle);
            //grdDictionary.DataSource = SqlExecuter.Procedure("usp_com_selectDictionary", cond);
            try
            {
                if (Dictionary.dtGridCaption == null || Dictionary.dtGridCaption.Rows.Count <= 0)
                {
                    Dictionary.Language = UserInfo.Current.LanguageType;
                    var cond = Conditions.GetValues();
                    cond.Add("P_VALIDSTATE", "Valid");
                    cond.Add("P_DICTIONARYCLASSID", "CONDITIONLABEL");//GRID
                    cond.Add("P_CONDITIONITEM", "*");
                    cond.Add("P_CONDITIONVALUE", "MEASURING");
                    Dictionary.dtGridCaption = SqlExecuter.Query("SelectDictionary", "10001", cond);
                    //Console.Write(SpcDictionary.dtGridCaption.Rows.Count);

                }
            }
            catch (Exception ex)
            {
         
            }

            //string testData = SpcDictionary.read(SpcDicClassId.GRID, "SPCPCOUNT");

        }

        public static class Dictionary
        {
            public static string Language;
            public static DataTable dtGridCaption;
            public static string read(DicClassId grp, string id)
            {
                string data = "";
                string classID = grp.ToString();

                try
                {
                    var dtList = dtGridCaption.AsEnumerable().AsParallel();
                    var query = from r in dtList
                                where r.Field<string>("DICTIONARYCLASSID") == classID
                                where r.Field<string>("DICTIONARYID") == id
                                select new
                                {
                                    classid = r.Field<string>("DICTIONARYID")
                                        ,
                                    id = r.Field<string>("DICTIONARYID")
                                        ,
                                    kor = r.Field<string>("DICTIONARYNAME$$KO-KR")
                                        ,
                                    usa = r.Field<string>("DICTIONARYNAME$$EN-US")
                                        ,
                                    chi = r.Field<string>("DICTIONARYNAME$$ZH-CN")
                                        ,
                                    vin = r.Field<string>("DICTIONARYNAME$$VI-VN")
                                };
                    foreach (var rw in query)
                    {
                        switch (Language)
                        {
                            case "en-US":
                                data = rw.usa.ToString();
                                break;
                            case "zh-CN":
                                data = rw.kor.ToString();
                                break;
                            case "vi-VN":
                                data = rw.chi.ToString();
                                break;
                            case "ko-KR":
                            default:
                                data = rw.vin.ToString();
                                break;
                        }

           
                    }

                    if (data != null && data != "")
                    {
                    }
                    else
                    {
                        data = id;
                    }

                }
                catch (Exception ex)
                {
                    data = id;        
                }

                return data;
            }
        }

        /// <summary>
        /// 사전 정보 테이블 dictionaryclassid (SF_DICTIONARY)
        /// </summary>
        public enum DicClassId : int
        {
            COMMON = 1
            , GRID = 2
            , BUTTON = 3
            , GROUPCAPTION = 4
            , CONTROLLABEL = 5
            , CONDITION = 6
            , CONDITIONLABEL = 7
            , Framework = 8
            , MAINFORM = 9
            , MASTERDICTIONARYCLASS = 10
            , MENU = 11
            , POPUP = 12
            , TOOLBAR = 13
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        private DataTable SetRowState(DataTable table)
        {
            foreach (DataRow row in table.Rows)
            {
                row["_STATE_"] = "modified";
                row["FLAG"] = "TypeChange";
            }

            return table;
        }


        /// <summary>
        /// Display Table 생성.
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        private DataTable CreateTableGrid(string tableName)
        {
            DataTable dataTable = new DataTable();
            dataTable.TableName = tableName;

            dataTable.Columns.Add("MODENAME", typeof(string));
            dataTable.Columns.Add("C01", typeof(string));
            dataTable.Columns.Add("C02", typeof(string));
            dataTable.Columns.Add("C03", typeof(string));
            dataTable.Columns.Add("C04", typeof(string));
            dataTable.Columns.Add("C05", typeof(string));
            dataTable.Columns.Add("C06", typeof(string));
            dataTable.Columns.Add("C07", typeof(string));
            dataTable.Columns.Add("C08", typeof(string));
            dataTable.Columns.Add("C09", typeof(string));
            dataTable.Columns.Add("C10", typeof(string));
            dataTable.Columns.Add("C11", typeof(string));
            dataTable.Columns.Add("C12", typeof(string));
            dataTable.Columns.Add("TOTAL", typeof(string));

            return dataTable;
        }

        /// <summary>
        /// 콤보박스에서 구분(분기자료) 설정
        /// </summary>
        /*private void SetComboBoxCreateValueQuarterType()
        {
            DataTable dataTableQuarterType;

            SetComboBoxInitial(cboQuarterType);

            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            dicParam.Add("CODECLASSID", "QuarterType");

            dataTableQuarterType = SqlExecuter.Query("GetCodeList", "00001", dicParam);
            if (dataTableQuarterType != null)
            {
                this.cboQuarterType.DataSource = dataTableQuarterType;
                this.cboQuarterType.EditValue = "Quarter1";
            }
        }*/

        /// <summary>
        /// 콤보박스 초기 공통 속성값 설정.
        /// </summary>
        /// <param name="cboBox"></param>
        private void SetComboBoxInitial(SmartComboBox cboBox)
        {
            cboBox.ValueMember = "CODEID";
            cboBox.DisplayMember = "CODENAME";
            cboBox.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboBox.ShowHeader = false;
        }
        #endregion


    }
}

