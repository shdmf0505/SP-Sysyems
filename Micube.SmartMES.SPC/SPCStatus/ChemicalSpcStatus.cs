#region using

using Micube.Framework.Net;
using Micube.Framework;
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

using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Repository;

using Micube.SmartMES.Commons;
using Micube.SmartMES.Commons.SPCLibrary;
using Micube.Framework.Log;

#endregion

namespace Micube.SmartMES.SPC
{

    /// <summary>
    /// 프 로 그 램 명  : SPC > SPC 현황 > 약품분석 SPC 현황
    /// 업  무  설  명  : 약품 측정값을 분석하여 관리도 및 공정능력분석함.
    /// 생    성    자  : 이승우
    /// 생    성    일  : 2019-07-15
    /// 수  정  이  력  :
    /// 2020-02-06  Over Rules 수정.
    /// 2019-08-06  XBAR-R, XBAR-S, I-MR, P 추가 수정.
    ///             Chart Option 추가 수정.
    /// 2019-07-15  최초작성.
    /// 
    /// </summary>
    public partial class ChemicalSpcStatus : SmartConditionManualBaseForm
    {
        #region Local Variables

        /// <summary>
        /// 보충량저장, 확정의상태값
        /// </summary>
        private string _saveConfirmState;

        /// <summary>
        /// 약품별로 스펙을 담고있는 데이터테이블
        /// </summary>
        private DataTable _chemicalSpecDt = new DataTable();

        #region 1. 통계 Chart 지역변수        
        /// <summary>
        /// 통계분석 Parameter
        /// </summary>
        private AnalysisExecutionParameter _AnalysisParameter = AnalysisExecutionParameter.Create();
        /// <summary>
        /// DB 조회 구분 : true - 재분석 실행. false - 분석함.
        /// </summary>
        private bool _isAgainAnalysisExe = false;
        /// <summary>
        /// XBar 재분석 구분 : true - 재분석 실행. false - 분석함.
        /// </summary>
        private bool _isAgainAnalysisXBar = false;
        /// <summary>
        /// Cpk 재분석 구분 : true - 재분석 실행. false - 분석함.
        /// </summary>
        private bool _isAgainAnalysisCpk = false;
        /// <summary>
        /// Plot 재분석 구분 : true - 재분석 실행. false - 분석함.
        /// </summary>
        private bool _isAgainAnalysisPlot = false;
        /// <summary>
        /// XBarFrame 버튼 Check 유무
        /// </summary>
        private bool _isAgainAnalysisButtonXBar = false;
        /// <summary>
        /// XBarFrame 버튼 Check 유무 - Cpk
        /// </summary>
        private bool _isAgainAnalysisButtonCpk = false;
        /// <summary>
        /// XBarFrame 버튼 Check 유무 및 조회시.
        /// </summary>
        private bool _isAgainAnalysisRawData = false;
        /// <summary>
        /// XBarFrame 버튼 Check 유무 및 조회시.
        /// </summary>
        private bool _isAgainAnalysisOverRules = false;
        /// <summary>
        /// 조회 버튼 미실행 구분 : true-미실행, false-실행
        /// </summary>
        private bool _isCancelOnSearchAsync = false;
        #endregion

        #endregion

        #region 생성자

        /// <summary>
        /// 생성자
        /// </summary>
        public ChemicalSpcStatus()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeGrid();

            InitializeEvent();

        }

        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            grdRawData.GridButtonItem = GridButtonItem.Export;
            grdRawData.View.SetIsReadOnly();
            grdRawData.View.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 120)
                .SetLabel("LARGEPROCESSSEGMENTNAME")
                .SetIsReadOnly();  // 대공정명
            grdRawData.View.AddTextBoxColumn("EQUIPMENTNAME", 120)
                .SetIsReadOnly(); // 설비명
            grdRawData.View.AddTextBoxColumn("STATE", 100)
                .SetIsReadOnly(); // 상태
            grdRawData.View.AddTextBoxColumn("DEGREE", 80)
                .SetIsReadOnly(); // 차수
            grdRawData.View.AddTextBoxColumn("CHILDEQUIPMENTNAME", 120)
                .SetIsReadOnly(); // 설비단
            grdRawData.View.AddTextBoxColumn("CHEMICALNAME", 120)
                .SetIsReadOnly(); // 약품명
            //grdRawData.View.AddTextBoxColumn("CHEMICALLEVEL", 80)
            //    .SetIsReadOnly(); // 약품등급
            //grdRawData.View.AddTextBoxColumn("MANAGEMENTSCOPE", 180)
            //    .SetIsReadOnly(); // 관리범위

            grdRawData.View.AddSpinEditColumn("TITRATIONQTY", 100)
                .SetDisplayFormat("0.000", MaskTypes.Numeric); // 적정량 
            grdRawData.View.AddSpinEditColumn("NVALUE", 100).SetLabel("ANALYSISVALUE")
                .SetDisplayFormat("0.000", MaskTypes.Numeric)
                .SetIsReadOnly(); // 분석량
            grdRawData.View.AddSpinEditColumn("SUPPLEMENTQTY", 100)
                .SetDisplayFormat("0.000", MaskTypes.Numeric)
                .SetIsReadOnly(); // 보충량
            //grdRawData.View.AddTextBoxColumn("COLLECTIONTIME");
            //grdChemicalRegistration.View.AddComboBoxColumn("COLLECTIONTIME", new SqlQuery("GetCodeList", "00001", "CODECLASSID=TimeZone", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
            //    .SetDefault("01"); // 채취시간대
            //grdRawData.View.AddComboBoxColumn("ISSUPPLEMENT", new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
            //    .SetDefault("N"); // 액보충필요
            //grdRawData.View.AddComboBoxColumn("ISRESUPPLEMENT", new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
            //    .SetDefault("N"); // 재분석필요
            //grdRawData.View.AddTextBoxColumn("MESSAGE", 150); // 전달사항 

            //grdRawData.View.AddTextBoxColumn("CREATOR", 80)
            //    .SetIsReadOnly()
            //    .SetTextAlignment(TextAlignment.Center); // 생성자
            //grdRawData.View.AddTextBoxColumn("CREATEDTIME", 130)
            //    .SetDisplayFormat("yyyy-MM-dd HH;mm:ss")
            //    .SetIsReadOnly()
            //    .SetTextAlignment(TextAlignment.Center); // 생성시간
            //grdRawData.View.AddTextBoxColumn("MODIFIER", 80)
            //    .SetIsReadOnly()
            //    .SetTextAlignment(TextAlignment.Center); // 수정자
            //grdRawData.View.AddTextBoxColumn("MODIFIEDTIME", 130)
            //    .SetDisplayFormat("yyyy-MM-dd HH;mm:ss")
            //    .SetIsReadOnly()
            //    .SetTextAlignment(TextAlignment.Center); // 수정시간

            grdRawData.View.AddTextBoxColumn("SEQUENCE");
            //.SetIsHidden(); // 일련번호
            grdRawData.View.AddTextBoxColumn("ANALYSISDATE");
            //.SetIsHidden(); // 분석일자
            grdRawData.View.AddTextBoxColumn("PMTYPE");
            //.SetIsHidden(); // 구분
            grdRawData.View.AddTextBoxColumn("ANALYSISTYPE");
            //.SetIsHidden(); // 약품수질구분
            grdRawData.View.AddTextBoxColumn("PROCESSSEGMENTCLASSID");
            //.SetIsHidden(); // 대공정ID
            grdRawData.View.AddTextBoxColumn("EQUIPMENTID");
            //.SetIsHidden(); // 설비ID
            grdRawData.View.AddTextBoxColumn("CHILDEQUIPMENTID");
            //.SetIsHidden(); // 설비단ID
            grdRawData.View.AddTextBoxColumn("INSPITEMID");
            //.SetIsHidden(); // 약품ID
            grdRawData.View.AddTextBoxColumn("REANALYSISTYPE");
            //.SetIsHidden(); // 재분석, 분석 구분자
            //grdRawData.View.AddTextBoxColumn("PARENTSEQUENCE");
            ////.SetIsHidden(); // 상위일련번호
            //grdRawData.View.AddTextBoxColumn("STATECODEID");
            ////.SetIsHidden(); // 상태의 코드ID
            //grdRawData.View.AddTextBoxColumn("ISCLOSE");
            ////.SetIsHidden(); // 마감여부
            //grdRawData.View.AddTextBoxColumn("ISSPECOUT")
            //    .SetDefault("N");
            //    //.SetIsHidden(); // SpecOut 여부
            //grdRawData.View.AddTextBoxColumn("TANKSIZE");
            ////.SetIsHidden(); // 탱크사이즈
            //grdRawData.View.AddTextBoxColumn("ANALYSISCONST");
            ////.SetIsHidden(); // 분석상수
            //grdRawData.View.AddTextBoxColumn("QTYCONST");
            ////.SetIsHidden(); // 보충량상수
            //grdRawData.View.AddTextBoxColumn("SL");
                //.SetIsHidden(); // Default 차트타입 스펙값의 SL값

            grdRawData.View.PopulateColumns();

            //grdRawData.View.Columns[8].AppearanceCell.BackColor = Color.Moccasin; // 적정량 Column 색깔변경
            grdRawData.View.OptionsView.AllowCellMerge = false; // CellMerge
            //grdRawData.View.FixColumn(new string[] { "PROCESSSEGMENTCLASSNAME", "EQUIPMENTNAME", "STATE", "DEGREE", "CHILDEQUIPMENTNAME", "CHEMICALNAME", "CHEMICALLEVEL", "MANAGEMENTSCOPE" });

            RepositoryItemTimeEdit edit = new RepositoryItemTimeEdit();

            //edit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            //edit.Mask.EditMask = "([0-1]?[0-9]|2[0-3]):[0-5][0-9]";
            //edit.Mask.UseMaskAsDisplayFormat = true;

            //grdRawData.View.Columns["COLLECTIONTIME"].ColumnEdit = edit;

            #region Over Rule
            grdOverRules.GridButtonItem = GridButtonItem.Export;
            grdOverRules.View.SetIsReadOnly();
            //grdOverRules.View.AddTextBoxColumn("TEMPID", 120);
            //grdOverRules.View.AddTextBoxColumn("GROUPID", 120);
            //grdOverRules.View.AddTextBoxColumn("SUBGROUP", 120);
            grdOverRules.View.AddTextBoxColumn("SUBGROUPNAME", 120).SetLabel("SUBGROUPNAMEVIEW");
            grdOverRules.View.AddTextBoxColumn("SAMPLINGNAME", 120).SetLabel("SAMPLINGXAXIS");
            grdOverRules.View.AddTextBoxColumn("EQUIPMENTID", 120);
            grdOverRules.View.AddTextBoxColumn("EQUIPMENTNAME", 120);
            grdOverRules.View.AddTextBoxColumn("CHILDEQUIPMENTID", 120);
            grdOverRules.View.AddTextBoxColumn("CHILDEQUIPMENTNAME", 120);
            grdOverRules.View.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 120);
            grdOverRules.View.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 120);
            grdOverRules.View.AddTextBoxColumn("INSPITEMID", 120);
            grdOverRules.View.AddTextBoxColumn("CHEMICALNAME", 120);
            //grdOverRules.View.AddTextBoxColumn("MAX", 120);
            //grdOverRules.View.AddTextBoxColumn("MIN", 120);
            grdOverRules.View.AddTextBoxColumn("R", 120);
            grdOverRules.View.AddTextBoxColumn("RUCL", 120);
            grdOverRules.View.AddTextBoxColumn("RLCL", 120);
            grdOverRules.View.AddTextBoxColumn("RCL", 120);
            grdOverRules.View.AddTextBoxColumn("BAR", 120);

            grdOverRules.View.AddTextBoxColumn("USL", 120);
            grdOverRules.View.AddTextBoxColumn("LSL", 120);
            grdOverRules.View.AddTextBoxColumn("CSL", 120);

            grdOverRules.View.AddTextBoxColumn("UCL", 120);
            grdOverRules.View.AddTextBoxColumn("LCL", 120);
            grdOverRules.View.AddTextBoxColumn("CL", 120);
            //grdOverRules.View.AddTextBoxColumn("XBAR", 120);
            //grdOverRules.View.AddTextBoxColumn("RBAR", 120);
            //grdOverRules.View.AddTextBoxColumn("SUMVALUE", 120);
            //grdOverRules.View.AddTextBoxColumn("SUMSUBVALUE", 120);
            //grdOverRules.View.AddTextBoxColumn("TOTSUM", 120);
            //grdOverRules.View.AddTextBoxColumn("TOTAVGVALUE", 120);
            //grdOverRules.View.AddTextBoxColumn("SAMESIGMA", 120);
            //grdOverRules.View.AddTextBoxColumn("ISSAME", 120);
            //grdOverRules.View.AddTextBoxColumn("STDEVALUE", 120);
            //grdOverRules.View.AddTextBoxColumn("NN", 120);
            //grdOverRules.View.AddTextBoxColumn("SNN", 120);
            //grdOverRules.View.AddTextBoxColumn("TOTNN", 120);
            //grdOverRules.View.AddTextBoxColumn("GROUPNN", 120);

            grdOverRules.View.PopulateColumns();

            #endregion
        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            btnSupplementRegistartion.Enabled = false;
            this.tabMain.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.tabMain_SelectedPageChanged);
        }

        #region 2. 폼 이벤트
        /// <summary>
        /// 폼 Load 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChemicalSpcStatus_Load(object sender, EventArgs e)
        {
            this.tabAnalysis.PageVisible = false;
            this.ucXBarFrame1.SpcCpkChartEnterEventHandler += UcXBarFrame1_SpcCpkChartEnterEventHandler;
            this.ucXBarFrame1.ucXBarGrid01.ucXBar1.SpcChartDirectMessageUserEventHandler += UcXBar1_SpcChartDirectMessageUserEventHandler;
            this.ucXBarFrame1.ucXBarGrid01.ucXBar2.SpcChartDirectMessageUserEventHandler += UcXBar2_SpcChartDirectMessageUserEventHandler;
            this.ucXBarFrame1.ucXBarGrid01.ucXBar3.SpcChartDirectMessageUserEventHandler += UcXBar3_SpcChartDirectMessageUserEventHandler;
            this.ucXBarFrame1.ucXBarGrid01.ucXBar4.SpcChartDirectMessageUserEventHandler += UcXBar4_SpcChartDirectMessageUserEventHandler;
            this.ucXBarFrame1.SpcChartXBarFrameDirectMessageUserEventHandler += UcXBarFrame1_SpcChartXBarFrameDirectMessageUserEventHandler;

            this.ucXBarFrame1.SpcVtChartShowWaitAreaEventHandler += UcXBarFrame1_SpcVtChartShowWaitAreaEventHandler;
            this.ucXBarFrame1.ucXBarGrid01.SpcVtChartShowWaitAreaEventHandler += UcXBarGrid01_SpcVtChartShowWaitAreaEventHandler;
            this.ucCpkFrame1.ucCpkGrid01.SpcVtChartShowWaitAreaEventHandler += UcCpkGrid01_SpcVtChartShowWaitAreaEventHandler;
            //this.ucXBarFrame1.SetChartTypeCboSelectIndex(3);
            //this.ucXBarFrame1.cboLeftChartType.ItemIndex
            SpcClass.SpcDictionaryDataSetting();
            this.ucXBarFrame1.cboChartTypeSetting(3);

        }


        #region Chart Wait 이벤트
        /// <summary>
        /// XBarFrame Chart Change Wait
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="se"></param>
        private void UcXBarFrame1_SpcVtChartShowWaitAreaEventHandler(object sender, EventArgs e, SpcShowWaitAreaOption se)
        {
            se.CheckValue = se.ChartTypeChange;
            UcChartGridShowWaitArea(sender, e, se);
        }
        /// <summary>
        /// XBar Grid Wait 이벤트 실행.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="se"></param>
        private void UcXBarGrid01_SpcVtChartShowWaitAreaEventHandler(object sender, EventArgs e, SpcShowWaitAreaOption se)
        {
            if (_isAgainAnalysisButtonXBar != true)
            {
                UcChartGridShowWaitArea(sender, e, se);
            }
            _isAgainAnalysisButtonXBar = false;
        }
        /// <summary>
        /// Cpk Grid Wait 이벤트 실행.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="se"></param>
        private void UcCpkGrid01_SpcVtChartShowWaitAreaEventHandler(object sender, EventArgs e, SpcShowWaitAreaOption se)
        {
            if (_isAgainAnalysisButtonCpk != true)
            {
                UcChartGridShowWaitArea(sender, e, se);
            }
            _isAgainAnalysisButtonCpk = false;
        }
        /// <summary>
        /// 분석 Chart Waite 이벤트 실행.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="se"></param>
        private void UcAnalysisPlot1_SpcVtChartShowWaitAreaEventHandler(object sender, EventArgs e, SpcShowWaitAreaOption se)
        {
            UcChartGridShowWaitArea(sender, e, se);
        }
        /// <summary>
        /// Chart Grid Waite 실행.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="se"></param>
        private void UcChartGridShowWaitArea(object sender, EventArgs e, SpcShowWaitAreaOption se)
        {
            try
            {
                if (se.CheckValue)
                {
                    _isCancelOnSearchAsync = true;
                    DialogManager.ShowWaitArea(this.pnlContent);
                }
                else
                {
                    DialogManager.CloseWaitArea(this.pnlContent);
                    _isCancelOnSearchAsync = false;
                }
            }
            catch (Exception ex)
            {
                DialogManager.CloseWaitArea(this.pnlContent);
                _isCancelOnSearchAsync = false;
                Logger.Error(ex.Message);
            }
        }
        #endregion Chart Wait 이벤트


        /// <summary>
        /// XBarFrame Message 이벤트
        /// </summary>
        /// <param name="msg"></param>
        private void UcXBarFrame1_SpcChartXBarFrameDirectMessageUserEventHandler(SpcEventsChartMessage msg)
        {
            ShowMessage(msg.mainMessage);
        }

        /// <summary>
        /// Chart Option 변경.
        /// </summary>
        /// <param name="f"></param>
        private void UcXBarFrame1_SpcCpkChartEnterEventHandler(SpcFrameChangeData f)
        {
            _isAgainAnalysisXBar = f.isAgainAnalysisXBar;
            _isAgainAnalysisCpk = f.isAgainAnalysisCpk;
            _isAgainAnalysisPlot = f.isAgainAnalysis;
            _isAgainAnalysisButtonXBar = f.isAgainAnalysisButtonXBar;
            _isAgainAnalysisButtonCpk = f.isAgainAnalysisButtonCpk;
            _isAgainAnalysisOverRules = f.isAgainAnalysisOverRules;
            _AnalysisParameter.spcOption = f.SPCOption;
        }
        /// <summary>
        /// 폼 Resize
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChemicalSpcStatus_Resize(object sender, EventArgs e)
        {
            FormResize();
        }
        /// <summary>
        /// Form Resize
        /// </summary>
        private void FormResize()
        {
            try
            {
                int position = 190;
                int rate = 4;
                this.ucXBarFrame1.splMain.SplitterPosition = position;
                this.ucXBarFrame1.splMainSub.SplitterPosition = this.Height - (this.Height / rate);
                this.ucCpkFrame1.splMain.SplitterPosition = position;
                this.ucCpkFrame1.splMainSub.SplitterPosition = this.Height - (this.Height / rate);
            }
            catch (Exception)
            {
                //throw;
            }
        }
        #endregion

        #region Chart Message 이벤트
        /// <summary>
        /// Chart1 Message 이벤트
        /// </summary>
        /// <param name="msg"></param>
        private void UcXBar1_SpcChartDirectMessageUserEventHandler(SpcEventsChartMessage msg)
        {
            ShowMessage(msg.mainMessage);
        }
        /// <summary>
        /// Chart2 Message 이벤트
        /// </summary>
        /// <param name="msg"></param>
        private void UcXBar2_SpcChartDirectMessageUserEventHandler(SpcEventsChartMessage msg)
        {
            ShowMessage(msg.mainMessage);
        }
        /// <summary>
        /// Chart3 Message 이벤트
        /// </summary>
        /// <param name="msg"></param>
        private void UcXBar3_SpcChartDirectMessageUserEventHandler(SpcEventsChartMessage msg)
        {
            ShowMessage(msg.mainMessage);
        }
        /// <summary>
        /// Chart4 Message 이벤트
        /// </summary>
        /// <param name="msg"></param>
        private void UcXBar4_SpcChartDirectMessageUserEventHandler(SpcEventsChartMessage msg)
        {
            ShowMessage(msg.mainMessage);
        }
        #endregion Chart Message 이벤트

        #region 3. 통계 Chart 관련 이벤트
        //this.tabMain.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.tabMain_SelectedPageChanged);
        //this.tabMain.TabIndexChanged += new System.EventHandler(this.tabMain_TabIndexChanged);
        //private void tabMain_TabIndexChanged(object sender, EventArgs e)
        //{
        //    if (this.tabMain.SelectedTabPageIndex == 1)
        //    {
        //        this.ucCpkFrame1.AnalysisExecutionTest();
        //    }
        //}
        /// <summary>
        /// Tab Page 변경
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabMain_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            this.ChartAnalysisExecution();
        }
        #endregion

        /// <summary>
        /// 보충량 확정된 설비 ReadOnly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (grdRawData.View.GetRowCellValue(grdRawData.View.FocusedRowHandle, "STATECODEID").Equals("SupplementConfirmed"))
                e.Cancel = true;
        }

        /// <summary>
        /// 보충량 확정취소버튼 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnConfirmationCancle_Click(object sender, EventArgs e)
        {
            if (!grdRawData.View.GetFocusedRowCellValue("STATECODEID").Equals("SupplementConfirmed"))
            {
                // 보충량 확정상태가 아니므로 보충량확정취소를 할 수 없습니다.
                this.ShowMessage("보충량 확정상태가 아니므로 보충량확정취소를 할 수 없습니다.");
                return;
            }

            // 상태(분석대기)
            _saveConfirmState = "AnaysisStandby";

            DataTable changed = new DataTable();
            changed.TableName = "list";

            changed.Columns.Add("ENTERPRISEID");
            changed.Columns.Add("PLANTID");
            changed.Columns.Add("EQUIPMENTID");
            changed.Columns.Add("CHEMICALWATERTYPE");
            changed.Columns.Add("ANALYSISDATE");
            changed.Columns.Add("DEGREE");
            changed.Columns.Add("PROCESSSEGMENTCLASSID");
            changed.Columns.Add("SAVECONFIRM");
            changed.Columns.Add("PMTYPE");
            changed.Columns.Add("CANCLEFLAG");

            DataRow row = null;
            row = changed.NewRow();

            row["ENTERPRISEID"] = UserInfo.Current.Enterprise;
            row["PLANTID"] = UserInfo.Current.Plant;
            row["EQUIPMENTID"] = grdRawData.View.GetFocusedRowCellValue("EQUIPMENTID");
            row["CHEMICALWATERTYPE"] = grdRawData.View.GetFocusedRowCellValue("CHEMICALWATERTYPE");
            row["ANALYSISDATE"] = grdRawData.View.GetFocusedRowCellValue("ANALYSISDATE");
            row["DEGREE"] = grdRawData.View.GetFocusedRowCellValue("DEGREE");
            row["PROCESSSEGMENTCLASSID"] = grdRawData.View.GetFocusedRowCellValue("PROCESSSEGMENTCLASSID");
            row["SAVECONFIRM"] = _saveConfirmState;
            row["PMTYPE"] = grdRawData.View.GetFocusedRowCellValue("PMTYPE");
            row["CANCLEFLAG"] = "Cancle";
            changed.Rows.Add(row);

            if (this.ShowMessageBox("InfoPopupSave", "Information", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                this.ExecuteRule("SaveChemicalRegistration", changed);
                this.ShowMessage("SuccessSave");
                this.OnSearchAsync();
            }
            else
            {
                this.ShowMessage("CancelSave");
            }
        }

        /// <summary>
        /// 보충량 저장버튼 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSupplementRegistartion_Click(object sender, EventArgs e)
        {
            if (this.Conditions.GetValue("p_division").Equals("Period")
                && this.Conditions.GetValue("p_round").Equals("*"))
            {
                // 회차는 전체조회로 저장할 수 없습니다.
                this.ShowMessage("AllSearchNotSave");
                return;
            }

            // 상태(분석대기)
            _saveConfirmState = "AnaysisStandby";
            DataTable changed = grdRawData.GetChangedRows();

            if (changed.Rows.Count == 0)
            {
                this.ShowMessage("NoSaveData");
            }
            else
            {
                changed.Columns.Add("SAVECONFIRM");
                changed.Columns.Add("ENTERPRISEID");
                changed.Columns.Add("PLANTID");

                foreach (DataRow row in changed.Rows)
                {
                    row["SAVECONFIRM"] = _saveConfirmState;
                    row["ANALYSISDATE"] = string.Format("{0:yyyy-MM-dd}", this.Conditions.GetValue("p_analysisDate")); // 분석일자
                    row["PMTYPE"] = Convert.ToString(this.Conditions.GetValue("p_division")); // 구분
                    row["DEGREE"] = Convert.ToString(this.Conditions.GetValue("p_round")); // 차수
                    row["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                    row["PLANTID"] = UserInfo.Current.Plant;
                }
                if (this.ShowMessageBox("InfoPopupSave", "Information", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    DataSet ds = new DataSet();
                    ds.Tables.Add(changed.Copy());
                    _chemicalSpecDt.TableName = "specData";
                    ds.Tables.Add(_chemicalSpecDt.Copy());

                    this.ExecuteRule("SaveChemicalRegistration", ds);
                    this.ShowMessage("SuccessSave");
                    this.OnSearchAsync();
                }
                else
                {
                    this.ShowMessage("CancelSave");
                }
            }
        }

        /// <summary>
        /// 보충량 확정버튼 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSupplementConfirmation_Click(object sender, EventArgs e)
        {
            if (grdRawData.View.GetFocusedRowCellValue("STATECODEID").Equals("SupplementConfirmed"))
            {
                // 이미 보충량이 확정된 항목입니다.
                this.ShowMessage("AlreadySupplementConfirmed");
                return;
            }

            if ((grdRawData.View.GetFocusedDataRow() as DataRow).RowState == DataRowState.Modified)
            {
                // 변경된 사항을 저장후 확정해주세요.
                this.ShowMessage("AfterSaveChanges");
                return;
            }

            // 상태(보충량확정)
            _saveConfirmState = "SupplementConfirmed";

            DataTable changed = new DataTable();
            changed.TableName = "list";

            changed.Columns.Add("ENTERPRISEID");
            changed.Columns.Add("PLANTID");
            changed.Columns.Add("EQUIPMENTID");
            changed.Columns.Add("CHEMICALWATERTYPE");
            changed.Columns.Add("ANALYSISDATE");
            changed.Columns.Add("DEGREE");
            changed.Columns.Add("PROCESSSEGMENTCLASSID");
            changed.Columns.Add("SAVECONFIRM");
            changed.Columns.Add("PMTYPE");

            DataRow row = null;
            row = changed.NewRow();

            row["ENTERPRISEID"] = UserInfo.Current.Enterprise;
            row["PLANTID"] = UserInfo.Current.Plant;
            row["EQUIPMENTID"] = grdRawData.View.GetFocusedRowCellValue("EQUIPMENTID");
            row["CHEMICALWATERTYPE"] = grdRawData.View.GetFocusedRowCellValue("CHEMICALWATERTYPE");
            row["ANALYSISDATE"] = grdRawData.View.GetFocusedRowCellValue("ANALYSISDATE");
            row["DEGREE"] = grdRawData.View.GetFocusedRowCellValue("DEGREE");
            row["PROCESSSEGMENTCLASSID"] = grdRawData.View.GetFocusedRowCellValue("PROCESSSEGMENTCLASSID");
            row["SAVECONFIRM"] = _saveConfirmState;
            row["PMTYPE"] = grdRawData.View.GetFocusedRowCellValue("PMTYPE");
            changed.Rows.Add(row);

            if (this.ShowMessageBox("InfoPopupSave", "Information", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                this.ExecuteRule("SaveChemicalRegistration", changed);
                this.ShowMessage("SuccessSave");
                this.OnSearchAsync();
            }
            else
            {
                this.ShowMessage("CancelSave");
            }
        }

        /// <summary>
        /// Spec Out Data 색깔 변경
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.Column.FieldName == "PROCESSSEGMENTCLASSNAME" || e.Column.FieldName == "EQUIPMENTNAME"
                || e.Column.FieldName == "STATE" || e.Column.FieldName == "CHILDEQUIPMENTNAME"
                || e.Column.FieldName == "DEGREE")
            {
                e.Appearance.BackColor = Color.White;
            }

            if (grdRawData.View.GetRowCellValue(e.RowHandle, "ISSPECOUT").Equals("Y"))
            {
                if (e.Column.FieldName == "TITRATIONQTY" || e.Column.FieldName == "ANALYSISVALUE"
                    || e.Column.FieldName == "SUPPLEMENTQTY" || e.Column.FieldName == "COLLECTIONTIME"
                    || e.Column.FieldName == "CHEMICALNAME" || e.Column.FieldName == "CHEMICALLEVEL"
                    || e.Column.FieldName == "MANAGEMENTSCOPE")
                {
                    e.Appearance.BackColor = Color.PaleVioletRed;
                }
            }
        }

        /// <summary>
        /// 적정량 Spec Check Mailing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "TITRATIONQTY")
            {
                Dictionary<string, object> param = new Dictionary<string, object>();
                DataRow keyRow = grdRawData.View.GetFocusedDataRow();

                param.Add("PROCESSSEGMENTCLASSID", keyRow["PROCESSSEGMENTCLASSID"]);
                param.Add("EQUIPMENTID", keyRow["EQUIPMENTID"]);
                param.Add("CHILDEQUIPMENTID", keyRow["CHILDEQUIPMENTID"]);
                param.Add("INSPITEMID", keyRow["INSPITEMID"]);

                if (_chemicalSpecDt.Rows.Count == 0)
                {
                    _chemicalSpecDt = SqlExecuter.Query("GetInspitemSpecData", "10001", param);
                }
                else
                {
                    DataTable dt = new DataTable();
                    dt = SqlExecuter.Query("GetInspitemSpecData", "10001", param);

                    _chemicalSpecDt.Merge(dt, true, MissingSchemaAction.Ignore);
                    _chemicalSpecDt = _chemicalSpecDt.DefaultView.ToTable(true);
                }

                foreach (DataRow rw in _chemicalSpecDt.Rows)
                {
                    rw["REANALYSISTYPE"] = "Analysis";
                    rw["ANALYSISTYPE"] = Convert.ToString(this.Conditions.GetValue("p_chemicalWaterType"));
                    rw["ANALYSISDATE"] = string.Format("{0:yyyy-MM-dd}", this.Conditions.GetValue("p_analysisDate"));
                    rw["DEGREE"] = Convert.ToString(this.Conditions.GetValue("p_round"));
                    rw["PMTYPE"] = Convert.ToString(this.Conditions.GetValue("p_division"));
                }

                //// 자동계산(분석치, 보충량)
                //SetCalculationRule calc = new SetCalculationRule();
                //calc.SetCalculationRule_grid(new string[] { "ChemicalAnalysis", "ChemicalSupplement" }, grdChemicalRegistration);

                // Spec Check
                SpecCheck(_chemicalSpecDt);
            }
        }

        /// <summary>
        /// 사용자 지정 Cell Merge
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            GridView view = sender as GridView;

            if (view == null)
            {
                return;
            }

            if (e.Column.FieldName == "PROCESSSEGMENTCLASSNAME" || e.Column.FieldName == "EQUIPMENTNAME"
                || e.Column.FieldName == "STATE" || e.Column.FieldName == "CHILDEQUIPMENTNAME"
                || e.Column.FieldName == "DEGREE")
            {
                string str1 = view.GetRowCellDisplayText(e.RowHandle1, e.Column);
                string str2 = view.GetRowCellDisplayText(e.RowHandle2, e.Column);
                e.Merge = (str1 == str2);
            }
            else
            {
                e.Merge = false;
            }
            e.Handled = true;
        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            //base.OnToolbarSaveClick();

            //ExecuteRule("SaveChemicalRegistration", grdRawData.GetChangedRows());
        }

        #endregion

        #region 검색

        /// <summary>
        /// Main자료조회 - 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            //sia확인 : 약품검사 폼 조회.
            try
            {
                await base.OnSearchAsync();

                var values = Conditions.GetValues();

                //DateTime analysisDate = Convert.ToDateTime(values["P_ANALYSISDATE"]);
                //values["P_ANALYSISDATE"] = string.Format("{0:yyyy-MM-dd}", analysisDate);
                values.Add("P_LANGUAGETYPE", UserInfo.Current.LanguageType);

                //사용자 편집.
                Dictionary<string, object> param = values;
                string dateStart = param["P_PERIOD_PERIODFR"].ToSafeString();
                DateTime dtStart = Convert.ToDateTime(dateStart);
                dateStart = dtStart.ToString("yyyy-MM-dd HH:mm:ss");
                string dateEnd = param["P_PERIOD_PERIODTO"].ToSafeString();
                DateTime dtEnd = Convert.ToDateTime(dateEnd);
                dateEnd = dtEnd.ToString("yyyy-MM-dd HH:mm:ss");
                values["P_PERIOD_PERIODFR"] = dateStart;
                values["P_PERIOD_PERIODTO"] = dateEnd;

                #region 5.통계용 입력자료 DB 조회
                //초기화
                DataTable InputRawMaxCount;
                DataTable tempInputSpec;
                tempInputSpec = SpcClass.SpcCreateMainRawSpecTable();
                this.grdRawData.DataSource = null;
                this.grdRawData.View.ClearDatas();
                this.grdOverRules.DataSource = null;
                this.grdOverRules.View.ClearDatas();

                //전체 조회 Count 조회.
                InputRawMaxCount = await SqlExecuter.QueryAsync("GetSpcChemicalRawMaxCount", "10001", values);
                if (InputRawMaxCount != null && InputRawMaxCount.Rows.Count < 1)
                {
                    this.ucXBarFrame1.ClearChartControlsXBAR();
                    this.ucCpkFrame1.ClearChartControlsCpk();
                    // 조회할 데이터가 없습니다.
                    ShowMessage("NoSelectData");
                    return;
                }

                //조회 자료 전체 건수 확인.
                long? nQueryMaxCount = InputRawMaxCount.Rows[0]["RECORDMAXCOUNT"].ToNullOrInt64();
                Console.WriteLine(nQueryMaxCount);

                //DB Data 부분 수집 여부 Check
                _AnalysisParameter.dtInputRawData = null;
                _AnalysisParameter.dtInputSpecData = null;
                if (nQueryMaxCount < SpcFlag.nSpcQueryMaxCount)
                {
                    _AnalysisParameter.dtInputRawData = await SqlExecuter.QueryAsync("GetSpcChemicalRaw", "10001", values);
                }
                else
                {
                    await this.InputDataMerge(dateStart, dateEnd, values);
                }

                #region SPEC 조회 - Linq형
                //Spec 입력자료 구성.
                if (_AnalysisParameter.dtInputRawData != null && _AnalysisParameter.dtInputRawData.Rows.Count < 1)
                {
                    this.ucXBarFrame1.ClearChartControlsXBAR();
                    this.ucCpkFrame1.ClearChartControlsCpk();
                    // 조회할 데이터가 없습니다.
                    ShowMessage("NoSelectData");//sia확인 : 다국어 처리. - 
                    return;
                }


                values["P_PERIOD_PERIODFR"] = dateStart;
                values["P_PERIOD_PERIODTO"] = dateEnd;
                _AnalysisParameter.dtInputSpecData = await SqlExecuter.QueryAsync("GetSpcChemicalRawSpec", "10001", values);
                if (_AnalysisParameter.dtInputSpecData != null && _AnalysisParameter.dtInputSpecData.Rows.Count < 1)
                {
                    // 조회할 데이터가 없습니다.
                    ShowMessage("NoSelectData - Spec Data");
                    return;
                }

                #endregion SPEC 조회 - Linq형


                //Chart 구분
                string chartType = this.ucXBarFrame1.cboLeftChartType.GetDataValue().ToSafeString().ToUpper().Replace("_", "");
                _AnalysisParameter.spcOption.specDefaultChartType = "MR";
                _AnalysisParameter.spcOption.chartName.xBarChartType = chartType;
                _AnalysisParameter.spcOption.chartName.xCpkChartType = chartType;
                _AnalysisParameter.spcOption.ChartTypeSetting(chartType, ref _AnalysisParameter.spcOption);
                _AnalysisParameter.spcOption.sigmaType = this.ucXBarFrame1.chkLeftEstimate.Checked ? SigmaType.Yes : SigmaType.No;//추정치 사용 유무

                //*통계 분석 실행
                _isAgainAnalysisExe = true;
                _isAgainAnalysisXBar = true;
                _isAgainAnalysisCpk = true;
                _isAgainAnalysisPlot = true;
                _isAgainAnalysisButtonXBar = true;
                _isAgainAnalysisButtonCpk = true;
                _isAgainAnalysisRawData = true;
                _isAgainAnalysisOverRules = true;

                switch (chartType.ToUpper())
                {
                    case "I":
                    case "MR":
                    case "IMR":
                        _AnalysisParameter.dtInputRawMainFieldName.samplingNameFiled = "SAMPLINGIMR";
                        _AnalysisParameter.dtInputRawMainFieldName.samplingNameFiledName = "SAMPLINGIMRNAME";
                        _AnalysisParameter.dtInputRawMainFieldName.samplingNameFiled01 = "SAMPLINGIMR";
                        _AnalysisParameter.dtInputRawMainFieldName.samplingNameFiledName01 = "SAMPLINGIMRNAME";
                        break;
                    default:
                        _AnalysisParameter.dtInputRawMainFieldName.ClearDefault(ref _AnalysisParameter.dtInputRawMainFieldName);
                        break;
                }

                //통계 분석
                this.ChartAnalysisExecution();

                #endregion

                grdRawData.DataSource = _AnalysisParameter.dtInputRawData;
                grdRawData.ShowStatusBar = true;
                _chemicalSpecDt.Clear();
            }
            catch (Exception ex)
            {
                throw Framework.MessageException.Create(ex.ToString());
            }
            finally
            {
                DialogManager.CloseWaitArea(this.pnlContent);
                _isAgainAnalysisExe = false;
            }
        }
        
        /// <summary>
        /// 검색조건 초기화. 
        /// 조회조건 정보, 메뉴 - 조회조건 매핑 화면에 등록된 정보를 기준으로 구성됩니다.
        /// DB에 등록한 정보를 제외한 추가 조회조건 구성이 필요한 경우 사용합니다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            this.Conditions.AddComboBox("p_plantId", new SqlQuery("GetPlantListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "PLANTNAME", "PLANTID")
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                .SetDefault(UserInfo.Current.Plant)
                .SetValidationIsRequired()
                .SetLabel("PLANT")
                .SetPosition(1.1); // Site (기본값 Login 유저의 Site)

            this.Conditions.AddComboBox("p_chemicalWaterType", new SqlQuery("GetChemicalWaterType", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "INSPECTIONCLASSNAME", "INSPECTIONCLASSID")
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                .SetDefault("ChemicalInspection")
                .SetRelationIds("p_plantId")
                .SetValidationIsRequired()
                .SetLabel("CHEMICALWATERTYPE")
                .SetPosition(1.2); // 약품수질구분 (기본값 Login 유저의 Site에 해당하는 InspectionClassId)

            this.Conditions.AddComboBox("p_round", new SqlQuery("GetRoundListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CYCLESEQUENCETIME", "CYCLESEQUENCE")
                .SetRelationIds("p_plantId", "p_chemicalWaterType", "p_division")
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                .SetValidationIsRequired()
                .SetEmptyItem()
                .SetLabel("ROUND")
                .SetPosition(2.1); // 회차

            this.Conditions.AddComboBox("p_processsegmentclassId", new SqlQuery("GetLargeProcesssegmentListByQcm", "10001", "CODECLASSID=ChemicalAnalyRound", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PROCESSSEGMENTCLASSNAME", "PROCESSSEGMENTCLASSID")
                .SetRelationIds("p_plantId")
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                .SetEmptyItem()
                .SetLabel("LARGEPROCESSSEGMENT")
                .SetPosition(2.2); // 대공정

            InitializeConditionPopup_Equipment();
            InitializeConditionPopup_EquipmentStage();
            InitializeConditionPopup_Chemical();
        }


        /// <summary>
        /// Site 조회조건
        /// </summary>
        private void InitializeConditionPopup_Plant()
        {
            // 팝업 컬럼설정
            var plantPopupColumn = Conditions.AddSelectPopup("p_plantId", new SqlQuery("GetPlantListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PLANTNAME", "PLANTID")
               .SetPopupLayout("PLANT", PopupButtonStyles.Ok_Cancel, true, false)
               .SetPopupLayoutForm(400, 600)
               .SetLabel("PLANT")
               .SetPopupResultCount(1)
               .SetPosition(2.2)
               .SetDefault(UserInfo.Current.Plant, Framework.UserInfo.Current.Plant)
               .SetValidationIsRequired();

            // 팝업 조회조건
            plantPopupColumn.Conditions.AddTextBox("PLANTIDNAME")
                .SetLabel("PLANTIDNAME");

            // 팝업 그리드
            plantPopupColumn.GridColumns.AddTextBoxColumn("PLANTID", 150)
                .SetValidationKeyColumn();
            plantPopupColumn.GridColumns.AddTextBoxColumn("PLANTNAME", 200);
        }

        /// <summary>
        /// 대공정 조회조건
        /// </summary>
        private void InitializeConditionPopup_Process()
        {
            // 팝업 컬럼설정
            var processPopupColumn = Conditions.AddSelectPopup("p_processsegmentclassId", new SqlQuery("GetLargeProcesssegmentListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PROCESSSEGMENTCLASSNAME", "PROCESSSEGMENTCLASSID")
               .SetPopupLayout("LARGEPROCESSSEGMENT", PopupButtonStyles.Ok_Cancel, true, false)
               .SetPopupLayoutForm(400, 600)
               .SetLabel("LARGEPROCESSSEGMENT")
               .SetPopupResultCount(1)
               .SetPosition(2.3)
               .SetRelationIds("p_plantId");

            // 팝업 조회조건
            processPopupColumn.Conditions.AddTextBox("PROCESSSEGMENTCLASSIDNAME")
                .SetLabel("LARGEPROCESSSEGMENTIDNAME");

            // 팝업 그리드
            processPopupColumn.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 150)
                .SetLabel("LARGEPROCESSSEGMENTID")
                .SetValidationKeyColumn();
            processPopupColumn.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 200)
                .SetLabel("LARGEPROCESSSEGMENTNAME");
        }

        /// <summary>
        /// 설비 조회조건
        /// </summary>
        private void InitializeConditionPopup_Equipment()
        {
            // 팝업 컬럼설정
            var equipmentPopupColumn = Conditions.AddSelectPopup("p_equipmentId", new SqlQuery("GetEquipmentListByChemicalAnalysis", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "EQUIPMENTNAME", "EQUIPMENTID")
               .SetPopupLayout("EQUIPMENT", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupLayoutForm(400, 600)
               .SetLabel("EQUIPMENT")
               .SetPopupResultCount(1)
               .SetPosition(2.4)
               .SetRelationIds("p_plantId", "p_processsegmentclassId");

            // 팝업 조회조건
            equipmentPopupColumn.Conditions.AddTextBox("EQUIPMENTIDNAME")
                .SetLabel("EQUIPMENTIDNAME");

            // 팝업 그리드
            equipmentPopupColumn.GridColumns.AddTextBoxColumn("EQUIPMENTID", 150)
                .SetValidationKeyColumn();
            equipmentPopupColumn.GridColumns.AddTextBoxColumn("EQUIPMENTNAME", 200);
        }

        /// <summary>
        /// 설비단 조회조건
        /// </summary>
        private void InitializeConditionPopup_EquipmentStage()
        {
            // 팝업 컬럼설정
            var equipmentStagePopupColumn = Conditions.AddSelectPopup("p_childEquipmentId", new SqlQuery("GetChildEquipmentListByChemicalAnalysis", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CHILDEQUIPMENTNAME", "CHILDEQUIPMENTID")
               .SetPopupLayout("CHILDEQUIPMENT", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupLayoutForm(400, 600)
               .SetLabel("CHILDEQUIPMENT")
               .SetPopupResultCount(1)
               .SetPosition(2.5)
               .SetRelationIds("p_plantId", "p_equipmentId");

            // 팝업 조회조건
            equipmentStagePopupColumn.Conditions.AddTextBox("CHILDEQUIPMENTIDNAME")
                .SetLabel("CHILDEQUIPMENTIDNAME");

            // 팝업 그리드
            equipmentStagePopupColumn.GridColumns.AddTextBoxColumn("CHILDEQUIPMENTID", 150)
                .SetValidationKeyColumn();
            equipmentStagePopupColumn.GridColumns.AddTextBoxColumn("CHILDEQUIPMENTNAME", 200);
        }

        /// <summary>
        /// 약품 조회조건
        /// </summary>
        private void InitializeConditionPopup_Chemical()
        {
            // 팝업 컬럼설정
            var chemicalPopupColumn = Conditions.AddSelectPopup("p_chemicalId", new SqlQuery("GetChemicalListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "INSPITEMNAME", "INSPITEMID")
               .SetPopupLayout("CHEMICAL", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupLayoutForm(400, 600)
               .SetLabel("CHEMICAL")
               .SetPopupResultCount(1)
               .SetPosition(2.6)
               .SetRelationIds("p_processsegmentclassId", "p_equipmentId", "p_childEquipmentId");

            // 팝업 조회조건
            chemicalPopupColumn.Conditions.AddTextBox("INSPITEMIDNAME")
                .SetLabel("INSPITEMIDNAME");

            // 팝업 그리드
            chemicalPopupColumn.GridColumns.AddTextBoxColumn("INSPITEMID", 150)
                .SetValidationKeyColumn();
            chemicalPopupColumn.GridColumns.AddTextBoxColumn("INSPITEMNAME", 200);
        }

        #endregion

        #region 검색 Data 부분 조회후 Merge 처리 부분.
        /// <summary>
        /// 입력자료 Merge 함수
        /// </summary>
        /// <param name="startDate">시작일자</param>
        /// <param name="endDate">종료일자</param>
        /// <param name="param">검색조건</param>
        /// <returns></returns>
        private async Task InputDataMerge(string startDate, string endDate, Dictionary<string, object> param)
        {
            List<SpcConditionDateDays> rtnlstDay;
            DataTable dtInputFirstData = new DataTable();
            //var values = Conditions.GetValues();
            //string startDate = "2020-02-01 08:30:00";
            //string endDate = "2020-03-01 08:30:00";
            rtnlstDay = SpcClass.SpcConditionList(startDate, endDate);
            //await base.OnSearchAsync();
            int niCount = 0;
            foreach (SpcConditionDateDays days in rtnlstDay)
            {
                niCount++;
                param["P_PERIOD_PERIODFR"] = days.DateStart;
                param["P_PERIOD_PERIODTO"] = days.DateEnd;
                Console.WriteLine(string.Format("{0} : {1} ~ {2}", niCount, days.DateStart, days.DateEnd));
                if (niCount != 1)
                {
                    DataTable dtInputPartData;
                    dtInputPartData = await SqlExecuter.QueryAsync("GetSpcChemicalRaw", "10001", param);
                    if (dtInputPartData != null && dtInputPartData.Rows.Count < 1)
                    {
                        //Next Sentence...
                    }
                    else
                    {
                        for (int i = 0; i < dtInputPartData.Rows.Count; i++)
                        {
                            DataRow rowData = dtInputPartData.Rows[i];
                            dtInputFirstData.ImportRow(rowData);
                        }
                    }

                    Console.WriteLine(dtInputFirstData.Rows.Count);
                }
                else
                {

                    dtInputFirstData = await SqlExecuter.QueryAsync("GetSpcChemicalRaw", "10001", param);
                    if (dtInputFirstData != null && dtInputFirstData.Rows.Count < 1)
                    {
                        niCount = 0;
                    }
                }
            }
            //17010

            if (dtInputFirstData != null && dtInputFirstData.Rows.Count > 0)
            {
                _AnalysisParameter.dtInputRawData = dtInputFirstData;
            }

            Console.WriteLine("1");
        }

        ///// <summary>
        ///// Spec Data - DataTable
        ///// </summary>
        ///// <param name="tableName"></param>
        ///// <returns></returns>
        //public DataTable CreateSpecTable(string tableName = "dtTempSpecData")
        //{
        //    DataTable dt = new DataTable();
        //    dt.TableName = tableName;
        //    //Filed
        //    dt.Columns.Add("SUBGROUP", typeof(string));
        //    dt.Columns.Add("CTYPE", typeof(string));
        //    dt.Columns.Add("SPECTYPE", typeof(string));
        //    dt.Columns.Add("USL", typeof(double));
        //    dt.Columns.Add("SL", typeof(double));
        //    dt.Columns.Add("LSL", typeof(double));
        //    dt.Columns.Add("LCL", typeof(double));
        //    dt.Columns.Add("CL", typeof(double));
        //    dt.Columns.Add("UOL", typeof(double));
        //    dt.Columns.Add("LOL", typeof(double));
        //    return dt;
        //}
        /// <summary>
        /// 합계 Data - DataTable
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataTable CreateChartSumTable(string tableName = "dtChartSumTable")
        {
            DataTable dt = new DataTable();
            dt.TableName = tableName;
            //Filed
            dt.Columns.Add("TEMPID", typeof(long));
            dt.Columns.Add("GROUPID", typeof(long));
            dt.Columns.Add("SUBGROUP", typeof(string));
            dt.Columns.Add("XBAR", typeof(double));
            dt.Columns.Add("RBAR", typeof(double));
            dt.Columns.Add("SUMSTDEV", typeof(double));
            dt.Columns.Add("SUMSTDEVBAR", typeof(double));
            dt.Columns.Add("SUMSTDEVP", typeof(double));
            dt.Columns.Add("SUMSTDEVPBAR", typeof(double));
            dt.Columns.Add("SUMVA", typeof(double));
            dt.Columns.Add("TOTSUMVALUE", typeof(double));
            dt.Columns.Add("TOTSUMSUBVALUE", typeof(double));
            dt.Columns.Add("SUMTOTDEVSQU", typeof(double));
            dt.Columns.Add("AVGVALUE", typeof(double));
            dt.Columns.Add("TOTAVGVALUE", typeof(double));
            dt.Columns.Add("SAMESIGMA", typeof(double));
            dt.Columns.Add("ISSAME", typeof(bool));
            dt.Columns.Add("STDEVALUE", typeof(double));
            dt.Columns.Add("NN", typeof(long));
            dt.Columns.Add("SNN", typeof(ulong));
            dt.Columns.Add("TOTNN", typeof(ulong));
            dt.Columns.Add("GROUPNN", typeof(long));

            return dt;
        }

        /// <summary>
        /// 품질규격 Over Rules Grid DataTable
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataTable CreateOverRulesTable(string tableName = "dtOverRulesTable")
        {
            DataTable dt = new DataTable();
            dt.TableName = tableName;
            //Filed
            dt.Columns.Add("TEMPID", typeof(long));
            dt.Columns.Add("GROUPID", typeof(long));
            dt.Columns.Add("SUBGROUP", typeof(string));
            dt.Columns.Add("SUBGROUPNAME", typeof(string));
            dt.Columns.Add("SAMPLING", typeof(string));
            dt.Columns.Add("SAMPLINGNAME", typeof(string));

            //업무별 수정 Start ----------------------------------
            dt.Columns.Add("EQUIPMENTID", typeof(string));
            dt.Columns.Add("EQUIPMENTNAME", typeof(string));
            dt.Columns.Add("CHILDEQUIPMENTID", typeof(string));
            dt.Columns.Add("CHILDEQUIPMENTNAME", typeof(string));
            dt.Columns.Add("PROCESSSEGMENTCLASSID", typeof(string));
            dt.Columns.Add("PROCESSSEGMENTCLASSNAME", typeof(string));
            dt.Columns.Add("INSPITEMID", typeof(string));
            dt.Columns.Add("CHEMICALNAME", typeof(string));
            //업무별 수정 End ----------------------------------

            dt.Columns.Add("MAX", typeof(double));
            dt.Columns.Add("MIN", typeof(double));
            dt.Columns.Add("R", typeof(double));
            dt.Columns.Add("RUCL", typeof(double));
            dt.Columns.Add("RLCL", typeof(double));
            dt.Columns.Add("RCL", typeof(double));
            dt.Columns.Add("BAR", typeof(double));

            dt.Columns.Add("USL", typeof(double));//추가
            dt.Columns.Add("LSL", typeof(double));//추가
            dt.Columns.Add("CSL", typeof(double));//추가

            dt.Columns.Add("UCL", typeof(double));
            dt.Columns.Add("LCL", typeof(double));
            dt.Columns.Add("CL", typeof(double));
            dt.Columns.Add("XBAR", typeof(double));
            dt.Columns.Add("RBAR", typeof(double));
            dt.Columns.Add("SUMVALUE", typeof(double));
            dt.Columns.Add("SUMSUBVALUE", typeof(double));
            dt.Columns.Add("TOTSUM", typeof(double));
            dt.Columns.Add("TOTAVGVALUE", typeof(double));
            dt.Columns.Add("SAMESIGMA", typeof(double));
            dt.Columns.Add("ISSAME", typeof(bool));
            dt.Columns.Add("STDEVALUE", typeof(double));
            dt.Columns.Add("NN", typeof(long));
            dt.Columns.Add("SNN", typeof(ulong));
            dt.Columns.Add("TOTNN", typeof(ulong));
            dt.Columns.Add("GROUPNN", typeof(long));
            return dt;
        }


        public bool OverRulesDataCheck(DataRow dataRowOverRule)
        {
            bool returnValue = false;
            SpcSpec overRuleCheckData = SpcSpec.Create();

            try
            {
                overRuleCheckData.XBar = dataRowOverRule["BAR"].ToSafeDoubleStaMin();
                overRuleCheckData.XRar = dataRowOverRule["R"].ToSafeDoubleStaMin();

                overRuleCheckData.usl = dataRowOverRule["USL"].ToSafeDoubleStaMax();
                overRuleCheckData.lsl = dataRowOverRule["LSL"].ToSafeDoubleStaMin();

                overRuleCheckData.ucl = dataRowOverRule["UCL"].ToSafeDoubleStaMax();
                overRuleCheckData.lcl = dataRowOverRule["LCL"].ToSafeDoubleStaMin();
                overRuleCheckData.ccl = dataRowOverRule["CL"].ToSafeDoubleStaMax();

                overRuleCheckData.rUcl = dataRowOverRule["RUCL"].ToSafeDoubleStaMax();
                overRuleCheckData.rLcl = dataRowOverRule["RLCL"].ToSafeDoubleStaMin();
                //overRuleCheckData.r = dataRowOverRule["RCL"].ToSafeDoubleZero();

                //Bar
                if (overRuleCheckData.XBar != SpcLimit.MIN)
                {
                    //Control
                    if (overRuleCheckData.XBar > overRuleCheckData.ucl)
                    {
                        returnValue = true;
                    }

                    if (overRuleCheckData.XBar < overRuleCheckData.lcl)
                    {
                        returnValue = true;
                    }
                    //Spec
                    if (overRuleCheckData.XBar > overRuleCheckData.usl)
                    {
                        returnValue = true;
                    }

                    if (overRuleCheckData.XBar < overRuleCheckData.lsl)
                    {
                        returnValue = true;
                    }

                }

                //R
                if (overRuleCheckData.XRar != SpcLimit.MIN)
                {
                    //Control
                    if (overRuleCheckData.XRar > overRuleCheckData.rUcl)
                    {
                        returnValue = true;
                    }

                    if (overRuleCheckData.XRar < overRuleCheckData.rLcl)
                    {
                        returnValue = true;
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return returnValue;
        }


        #endregion

        #region 유효성 검사

        ///// <summary>
        ///// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        ///// </summary>
        //protected override void OnValidateContent()
        //{
        //    base.OnValidateContent();

        //    grdRawData.View.CheckValidation();

        //    if (grdRawData.GetChangedRows().Rows.Count == 0)
        //    {
        //        // 저장할 데이터가 존재하지 않습니다.
        //        throw MessageException.Create("NoSaveData");
        //    }
        //}

        #endregion

        #region Private Function

        #region 4. 통계용 함수
        /// <summary>
        /// 통계 분석 실행 - 공정능력 실행, 관리도 (Tab 이벤트에서 실행함)
        /// </summary>
        private void ChartAnalysisExecution()
        {
            string subGroupName = "";
            //if (!_isAgainAnalysisCpk)
            //{
            //    _isAgainAnalysisCpk = _AnalysisParameter.isAgainAnalysisCpk;
            //}

            try
            {
                switch (this.tabMain.SelectedTabPageIndex)
                {
                    case 0:
                    case 2:
                    case 3:
                    case 4:
                        if (_isAgainAnalysisExe != true && _isAgainAnalysisXBar == true)
                        {
                            //DialogManager.ShowWaitArea(this.ucXBarFrame1);
                            DialogManager.ShowWaitArea(this.pnlContent);
                        }
                        this.FormResize();
                        subGroupName = this.ucXBarFrame1.ucXBarGrid01.grp01.Text.ToSafeString();
                        if (_isAgainAnalysisXBar)
                        {
                            this.ucXBarFrame1.AnalysisExecution(_AnalysisParameter);
                            _isAgainAnalysisXBar = false;
                        }
                        break;
                    default:
                        break;
                }

                if (this.tabMain.SelectedTabPageIndex == 1)
                {
                    if (_isAgainAnalysisExe != true && _isAgainAnalysisCpk == true)
                    {
                        //DialogManager.ShowWaitArea(this.ucCpkFrame1);
                        DialogManager.ShowWaitArea(this.pnlContent);
                    }
                    this.FormResize();
                    subGroupName = this.ucCpkFrame1.ucCpkGrid01.grp01.Text.ToSafeString();
                    if (_isAgainAnalysisCpk)
                    {
                        this.ucCpkFrame1.AnalysisExecution(_AnalysisParameter);
                        _isAgainAnalysisCpk = false;
                    }
                }
                else if (this.tabMain.SelectedTabPageIndex == 2)
                {
                    //Plot Check
                    if (_isAgainAnalysisPlot)
                    {
                        this.ucAnalysisPlot1.SubGroupIdSetting(this.ucXBarFrame1._spcPara, ref _isAgainAnalysisPlot);
                        _isAgainAnalysisPlot = false;
                    }
                }
                else if (this.tabMain.SelectedTabPageIndex == 3)
                {
                    //Raw Data
                    if (_isAgainAnalysisRawData)
                    {
                        DialogManager.ShowWaitArea(this.pnlContent);
                        grdRawData.View.BestFitColumns();
                        _isAgainAnalysisRawData = false;
                    }
                }
                else if (this.tabMain.SelectedTabPageIndex == 4)
                {
                    //Over Rules
                    if (_isAgainAnalysisOverRules)
                    {
                        this.GridOverRules();
                        _isAgainAnalysisOverRules = false;
                    }
                }
            }
            catch (Exception ex)
            {
                //throw Framework.MessageException.Create(ex.ToString());
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (_isAgainAnalysisExe != true)
                {
                    DialogManager.CloseWaitArea(this.pnlContent);
                    //DialogManager.CloseWaitArea(this.ucXBarFrame1);
                    //DialogManager.CloseWaitArea(this.ucCpkFrame1);
                }
            }
        }

        #endregion


        #region 5. Over Rules Check 및 Grid 표시.

        /// <summary>
        /// Over Rules Check Data Grid에 표시.
        /// </summary>
        private void GridOverRules()
        {
            //OverRules Grid Data 편집.
            #region OverRules Grid Data 편집

            this.grdOverRules.DataSource = null;
            this.grdOverRules.View.ClearDatas();

            DataTable dtOverRulesTable = null;
            DataTable staChartData = null;
            DataTable staCpkData = null;
            dtOverRulesTable = this.CreateOverRulesTable();
            staChartData = this.ucXBarFrame1.ucXBarGrid01.rtnChartData;
            staCpkData = this.ucXBarFrame1.ucXBarGrid01.rtnCpkData;
            if (staChartData != null && staChartData.Rows.Count > 0)
            {
                try
                {
                    for (int i = 0; i < staChartData.Rows.Count; i++)
                    { 
                        DataRow srcData = staChartData.Rows[i];
                        DataRow dataRowOverRule = dtOverRulesTable.NewRow();
                        SpcFunction.IsDbNckInt64Write(dataRowOverRule, "TEMPID", srcData);
                        SpcFunction.IsDbNckStringWrite(dataRowOverRule, "SUBGROUP", srcData);
                        SpcFunction.IsDbNckStringWrite(dataRowOverRule, "SUBGROUPNAME", srcData, "", SpcClass.SubGroupSymbolDb, SpcClass.SubGroupSymbolView);
                        SpcFunction.IsDbNckStringWrite(dataRowOverRule, "SAMPLING", srcData);
                        SpcFunction.IsDbNckStringWrite(dataRowOverRule, "SAMPLINGNAME", srcData);

                        SpcFunction.IsDbNckDoubleWrite(dataRowOverRule, "BAR", srcData);
                        SpcFunction.IsDbNckDoubleWrite(dataRowOverRule, "R", srcData);

                        SpcFunction.IsDbNckDoubleWrite(dataRowOverRule, "UCL", srcData);
                        SpcFunction.IsDbNckDoubleWrite(dataRowOverRule, "LCL", srcData);
                        SpcFunction.IsDbNckDoubleWrite(dataRowOverRule, "CL", srcData);

                        SpcFunction.IsDbNckDoubleWrite(dataRowOverRule, "RUCL", srcData);
                        SpcFunction.IsDbNckDoubleWrite(dataRowOverRule, "RLCL", srcData);
                        SpcFunction.IsDbNckDoubleWrite(dataRowOverRule, "RCL", srcData);

                        string subGroupId = srcData["SUBGROUP"].ToSafeString();
                        var staCpkRaw = staCpkData.AsEnumerable().Where(x => x.Field<string>("SUBGROUP") == subGroupId);

                        foreach (var item in staCpkRaw)
                        {
                            SpcFunction.IsDbNckDoubleWrite(dataRowOverRule, "USL", item["USL"].ToSafeDoubleStaMin());
                            SpcFunction.IsDbNckDoubleWrite(dataRowOverRule, "CSL", item["CSL"].ToSafeDoubleStaMin());
                            SpcFunction.IsDbNckDoubleWrite(dataRowOverRule, "LSL", item["LSL"].ToSafeDoubleStaMin());
                        }

                        var dtGroupData = _AnalysisParameter.dtInputRawData.AsEnumerable()
                                                               .Where(x => x.Field<string>("SUBGROUP") == subGroupId)
                                                               .GroupBy(m => new { SUBGROUP = m.Field<string>("SUBGROUP") })
                                                               .Select(grp => new
                                                               {
                                                                       sSUBGROUP = grp.Key.SUBGROUP.ToSafeString(),
                                                                       //nUSL = grp.Where(x => x.Field<object>("USL") != null).Max(x => x.Field<object>("USL").ToSafeDoubleZero()),
                                                                       //nTSL = grp.Where(x => x.Field<object>("TARGET") != null).Max(x => x.Field<object>("TARGET").ToSafeDoubleZero()),
                                                                       //nLSL = grp.Where(x => x.Field<object>("LSL") != null).Min(x => x.Field<object>("LSL").ToSafeDoubleZero()),
                                                                       sEQUIPMENTID = grp.Where(x => x.Field<object>("EQUIPMENTID") != null).Max(x => x.Field<object>("EQUIPMENTID").ToSafeString()),//설비ID
                                                                       sEQUIPMENTNAME = grp.Where(x => x.Field<object>("EQUIPMENTNAME") != null).Max(x => x.Field<object>("EQUIPMENTNAME").ToSafeString()),//설비 명
                                                                       sCHILDEQUIPMENTID = grp.Where(x => x.Field<object>("CHILDEQUIPMENTID") != null).Max(x => x.Field<object>("CHILDEQUIPMENTID").ToSafeString()),//설비단 Id
                                                                       sCHILDEQUIPMENTNAME = grp.Where(x => x.Field<object>("CHILDEQUIPMENTNAME") != null).Max(x => x.Field<object>("CHILDEQUIPMENTNAME").ToSafeString()),//설비단 명
                                                                       sPROCESSSEGMENTCLASSID = grp.Where(x => x.Field<object>("PROCESSSEGMENTCLASSID") != null).Max(x => x.Field<object>("PROCESSSEGMENTCLASSID").ToSafeString()),//대공정 ID
                                                                       sPROCESSSEGMENTCLASSNAME = grp.Where(x => x.Field<object>("PROCESSSEGMENTCLASSNAME") != null).Max(x => x.Field<object>("PROCESSSEGMENTCLASSNAME").ToSafeString()),//대공정 명
                                                                       sINSPITEMID = grp.Where(x => x.Field<object>("INSPITEMID") != null).Max(x => x.Field<object>("INSPITEMID").ToSafeString()),//약품코드
                                                                       sCHEMICALNAME = grp.Where(x => x.Field<object>("CHEMICALNAME") != null).Max(x => x.Field<object>("CHEMICALNAME").ToSafeString())//약품명

                                                                   });
                        foreach (var s in dtGroupData)
                        {
                            SpcFunction.IsDbNckStringWrite(dataRowOverRule, "EQUIPMENTID", s.sEQUIPMENTID.ToSafeString());
                            SpcFunction.IsDbNckStringWrite(dataRowOverRule, "EQUIPMENTNAME", s.sEQUIPMENTNAME.ToSafeString());
                            SpcFunction.IsDbNckStringWrite(dataRowOverRule, "CHILDEQUIPMENTID", s.sCHILDEQUIPMENTID.ToSafeString());
                            SpcFunction.IsDbNckStringWrite(dataRowOverRule, "CHILDEQUIPMENTNAME", s.sCHILDEQUIPMENTNAME.ToSafeString());
                            SpcFunction.IsDbNckStringWrite(dataRowOverRule, "PROCESSSEGMENTCLASSID", s.sPROCESSSEGMENTCLASSID.ToSafeString());
                            SpcFunction.IsDbNckStringWrite(dataRowOverRule, "PROCESSSEGMENTCLASSNAME", s.sPROCESSSEGMENTCLASSNAME.ToSafeString());
                            SpcFunction.IsDbNckStringWrite(dataRowOverRule, "INSPITEMID", s.sINSPITEMID.ToSafeString());
                            SpcFunction.IsDbNckStringWrite(dataRowOverRule, "CHEMICALNAME", s.sCHEMICALNAME.ToSafeString());
                        }


                        bool overRuleCheck = OverRulesDataCheck(dataRowOverRule);

                        if (overRuleCheck)
                        {
                            dtOverRulesTable.Rows.Add(dataRowOverRule);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                //Console.WriteLine(staChartData.Rows.Count);
                //Subgroup 조회 or Join
                //Spec 조회 or Join
            }

            //Grid에 Raw Data 표시.

            grdOverRules.DataSource = dtOverRulesTable;
            grdOverRules.View.BestFitColumns();
            grdOverRules.ShowStatusBar = true;

            //Console.WriteLine("^^");
            #endregion OverRules Grid Data 편집
        }

        /// <summary>
        /// Spec Check 모듈
        /// </summary>
        private void SpecCheck(DataTable specDt)
        {
            DataRow focusRow = grdRawData.View.GetFocusedDataRow();
            DataRow row = specDt.Rows.Cast<DataRow>()
                                     .Where(r => r["INSPITEMID"].Equals(focusRow["INSPITEMID"].ToString()) && r["CONTROLTYPE"].Equals(r["DEFAULTCHARTTYPE"]))
                                     .First();

            //입력 Parameter 입력
            SpcLibraryHelper spcHelper = new SpcLibraryHelper();
            SpcRules spcRules = new SpcRules();
            spcRules.xbarr.uol = Convert.ToDouble(row["UOL"]);
            spcRules.xbarr.usl = Convert.ToDouble(row["USL"]);
            spcRules.xbarr.ucl = Convert.ToDouble(row["UCL"]);
            spcRules.xbarr.lcl = Convert.ToDouble(row["LCL"]);
            spcRules.xbarr.lsl = Convert.ToDouble(row["LSL"]);
            spcRules.xbarr.lol = Convert.ToDouble(row["LOL"]);
            spcRules.nValue = Convert.ToDouble(grdRawData.View.GetRowCellValue(grdRawData.View.FocusedRowHandle, "TITRATIONQTY"));

            spcRules.defaultChartType = SpcChartType.xbarr;

            //Spec Check
            SpcRulesOver returnRulesOver = new SpcRulesOver();
            returnRulesOver = spcHelper.SpcSpecRuleCheck(spcRules);

            //결과 표시
            if (returnRulesOver.isResult == true)
            {
                grdRawData.View.SetRowCellValue(grdRawData.View.FocusedRowHandle, "ISSPECOUT", "Y");

                // Spec Out입니다. 메일을 보내시겠습니까?
                if (this.ShowMessageBox(returnRulesOver.message.value, "Caption", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    //SendMailPopup popup = new SendMailPopup();
                    //popup.Owner = this;
                    //popup.CurrentDataRow = grdChemicalRegistration.View.GetFocusedDataRow();
                    //popup.ShowDialog();
                }
            }
            else
            {
                grdRawData.View.SetRowCellValue(grdRawData.View.FocusedRowHandle, "ISSPECOUT", "N");

                if (spcRules.nValue.IsDouble())
                {
                    this.ShowMessageBox("정상 - SPEC Check", "OO");
                }
                else
                {
                    this.ShowMessageBox("측정값이 없습니다.", "OO");
                }
            }
        }
        #endregion 5. Over Rules Check 및 Grid 표시.


        #endregion


    }
}
