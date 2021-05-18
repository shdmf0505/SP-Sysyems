#region using

using Micube.SmartMES.Commons.SPCLibrary;
using Micube.Framework.SmartControls;
using static Micube.SmartMES.Commons.SPCLibrary.DataSets.SpcDataSet;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraLayout.Utils;

#endregion

namespace Micube.SmartMES.SPC.UserControl
{
    /// <summary>
    /// 프 로 그 램 명  : SPC > UserControl > ucXBarFrame
    /// 업  무  설  명  : SPC 통계 관리도 분석 Frame
    /// 생    성    자  : 이승우
    /// 생    성    일  : 2019-07-19
    /// 수  정  이  력  : 
    /// 
    /// 2019-07-19  XBar Chart 연결
    /// 2019-07-01  최초작성
    /// 
    /// </summary>
    public partial class ucXBarFrame : DevExpress.XtraEditors.XtraUserControl
    {
        #region Local Variables
        /// <summary>
        /// Chart Point Click 사용자 이벤트
        /// </summary>
        /// <param name="f"></param>
        public delegate void SpcXBarFrameChangeEventHandler(SpcFrameChangeData f);
        /// <summary>
        /// Chart Point Click 사용자 이벤트
        /// </summary>
        public virtual event SpcXBarFrameChangeEventHandler SpcCpkChartEnterEventHandler;
        /// <summary>
        /// Chart 내부 메세지 이벤트 - delegate
        /// </summary>
        /// <param name="msg"></param>
        public delegate void SpcChartXBarFrameDirectMessageEventHandler(SpcEventsChartMessage msg);
        /// <summary>
        /// Chart 내부 메세지 이벤트
        /// </summary>
        public virtual event SpcChartXBarFrameDirectMessageEventHandler SpcChartXBarFrameDirectMessageUserEventHandler;

        #region Spc Chart ShowWaitArea - Event
        public SpcShowWaitAreaOption spcWaitOption = new SpcShowWaitAreaOption();
        public delegate void SpcChartShowWaitAreaEventHandler(object sender, EventArgs e, SpcShowWaitAreaOption se);
        public virtual event SpcChartShowWaitAreaEventHandler SpcVtChartShowWaitAreaEventHandler;
        #endregion Spc Chart ShowWaitArea - Event

        /// <summary>
        /// 내부 Message 입력 Parameter
        /// </summary>
        public SpcEventsChartMessage spcMsg = new SpcEventsChartMessage();
        private string[] _gridCaption;
        /// <summary>
        /// 통계 분석용 DB Data 원본 자료
        /// </summary>
        private DataTable _dtInputRawData;
        /// <summary>
        /// 통계 분석용 DB Data 원본 자료의 기준정보성 SPEC
        /// </summary>
        private DataTable _dtInputSpecData;
        /// <summary>
        /// SPC 분석용 함수 모음.
        /// </summary>
        private SpcFunction _spcfnc = new SpcFunction();
        /// <summary>
        /// 재분석 구분 : true - 재분석 실행. false - 분석함.
        /// </summary>
        private bool _isAgainAnalysis = false;
        /// <summary>
        /// 통계분석 Parameter
        /// </summary>
        private AnalysisExecutionParameter _AnalysisParameter = new AnalysisExecutionParameter();

        public SPCPara _spcPara;
        #endregion

        #region 생성자

        public ucXBarFrame()
        {
            InitializeComponent();
            InitializeContent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        protected void InitializeContent()
        {
            InitializeControls();
            InitializeGrid(0, "", "");
            this.Load += new System.EventHandler(this.ucXBarFrame_Load);
        }

        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid(int rowMax, string subGroupTitle, string subGroupName = "")
        {
            grdSelectChartRawData.GridButtonItem = GridButtonItem.Export;
            grdSelectChartRawData.Caption = "";
            grdSelectChartRawData.DataSource = null;
            grdSelectChartRawData.View.ClearColumns();
            string fieldName = "";
            
            if (rowMax > 0)
            {
                grdSelectChartRawData.Caption = subGroupTitle;

                for (int i = 0; i < rowMax; i++)
                {
                    fieldName = string.Format("FIELD_{0:D3}", i + 1);
                    grdSelectChartRawData.View.AddSpinEditColumn(fieldName, 150)
                                           //.SetDisplayFormat("{0:f}", MaskTypes.Numeric) // 측정값
                                           .SetDisplayFormat("#,##0.##########", MaskTypes.Numeric)
                                           .SetLabel(_gridCaption[i])
                                           .SetIsReadOnly();
                }

                grdSelectChartRawData.View.PopulateColumns();
            }

            //grdChemicalRegistration.View.AddTextBoxColumn("field_001", 120)
            //    .SetLabel("field_001")
            //    .SetIsReadOnly();
            //grdChemicalRegistration.View.AddTextBoxColumn("EQUIPMENTNAME", 120)
            //    .SetIsReadOnly(); // 설비명
            //grdChemicalRegistration.View.AddTextBoxColumn("STATE", 100)
            //    .SetIsReadOnly(); // 상태
            //grdChemicalRegistration.View.AddTextBoxColumn("DEGREE", 80)
            //    .SetIsReadOnly(); // 차수
            //grdChemicalRegistration.View.AddTextBoxColumn("CHILDEQUIPMENTNAME", 120)
            //    .SetIsReadOnly(); // 설비단
            //grdChemicalRegistration.View.AddTextBoxColumn("CHEMICALNAME", 120)
            //    .SetIsReadOnly(); // 약품명
            //grdChemicalRegistration.View.AddTextBoxColumn("CHEMICALLEVEL", 80)
            //    .SetIsReadOnly(); // 약품등급
            //grdChemicalRegistration.View.AddTextBoxColumn("MANAGEMENTSCOPE", 180)
            //    .SetIsReadOnly(); // 관리범위

            //grdChemicalRegistration.View.AddSpinEditColumn("TITRATIONQTY", 100)
            //    .SetDisplayFormat("0.000", MaskTypes.Numeric); // 적정량 
            //grdChemicalRegistration.View.AddSpinEditColumn("ANALYSISVALUE", 100)
            //    .SetDisplayFormat("0.000", MaskTypes.Numeric)
            //    .SetIsReadOnly(); // 분석량
            //grdChemicalRegistration.View.AddSpinEditColumn("SUPPLEMENTQTY", 100)
            //    .SetDisplayFormat("0.000", MaskTypes.Numeric)
            //    .SetIsReadOnly(); // 보충량
            //grdChemicalRegistration.View.AddTextBoxColumn("COLLECTIONTIME");
            //grdChemicalRegistration.View.AddComboBoxColumn("COLLECTIONTIME", new SqlQuery("GetCodeList", "00001", "CODECLASSID=TimeZone", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
            //    .SetDefault("01"); // 채취시간대
            //grdChemicalRegistration.View.AddComboBoxColumn("ISSUPPLEMENT", new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
            //    .SetDefault("N"); // 액보충필요
            //grdChemicalRegistration.View.AddComboBoxColumn("ISRESUPPLEMENT", new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
            //    .SetDefault("N"); // 재분석필요
            //grdChemicalRegistration.View.AddTextBoxColumn("MESSAGE", 150); // 전달사항 

            //grdChemicalRegistration.View.AddTextBoxColumn("CREATOR", 80)
            //    .SetIsReadOnly()
            //    .SetTextAlignment(TextAlignment.Center); // 생성자
            //grdChemicalRegistration.View.AddTextBoxColumn("CREATEDTIME", 130)
            //    .SetDisplayFormat("yyyy-MM-dd HH;mm:ss")
            //    .SetIsReadOnly()
            //    .SetTextAlignment(TextAlignment.Center); // 생성시간
            //grdChemicalRegistration.View.AddTextBoxColumn("MODIFIER", 80)
            //    .SetIsReadOnly()
            //    .SetTextAlignment(TextAlignment.Center); // 수정자
            //grdChemicalRegistration.View.AddTextBoxColumn("MODIFIEDTIME", 130)
            //    .SetDisplayFormat("yyyy-MM-dd HH;mm:ss")
            //    .SetIsReadOnly()
            //    .SetTextAlignment(TextAlignment.Center); // 수정시간

            //grdChemicalRegistration.View.AddTextBoxColumn("SEQUENCE")
            //    .SetIsHidden(); // 일련번호
            //grdChemicalRegistration.View.AddTextBoxColumn("ANALYSISDATE")
            //    .SetIsHidden(); // 분석일자
            //grdChemicalRegistration.View.AddTextBoxColumn("PMTYPE")
            //    .SetIsHidden(); // 구분
            //grdChemicalRegistration.View.AddTextBoxColumn("ANALYSISTYPE")
            //    .SetIsHidden(); // 약품수질구분
            //grdChemicalRegistration.View.AddTextBoxColumn("PROCESSSEGMENTCLASSID")
            //    .SetIsHidden(); // 대공정ID
            //grdChemicalRegistration.View.AddTextBoxColumn("EQUIPMENTID")
            //    .SetIsHidden(); // 설비ID
            //grdChemicalRegistration.View.AddTextBoxColumn("CHILDEQUIPMENTID")
            //    .SetIsHidden(); // 설비단ID
            //grdChemicalRegistration.View.AddTextBoxColumn("INSPITEMID")
            //    .SetIsHidden(); // 약품ID
            //grdChemicalRegistration.View.AddTextBoxColumn("REANALYSISTYPE")
            //    .SetIsHidden(); // 재분석, 분석 구분자
            //grdChemicalRegistration.View.AddTextBoxColumn("PARENTSEQUENCE")
            //    .SetIsHidden(); // 상위일련번호
            //grdChemicalRegistration.View.AddTextBoxColumn("STATECODEID")
            //    .SetIsHidden(); // 상태의 코드ID
            //grdChemicalRegistration.View.AddTextBoxColumn("ISCLOSE")
            //    .SetIsHidden(); // 마감여부
            //grdChemicalRegistration.View.AddTextBoxColumn("ISSPECOUT")
            //    .SetDefault("N")
            //    .SetIsHidden(); // SpecOut 여부
            //grdChemicalRegistration.View.AddTextBoxColumn("TANKSIZE")
            //    .SetIsHidden(); // 탱크사이즈
            //grdChemicalRegistration.View.AddTextBoxColumn("ANALYSISCONST")
            //    .SetIsHidden(); // 분석상수
            //grdChemicalRegistration.View.AddTextBoxColumn("QTYCONST")
            //    .SetIsHidden(); // 보충량상수
            //grdChemicalRegistration.View.AddTextBoxColumn("SL")
            //    .SetIsHidden(); // Default 차트타입 스펙값의 SL값

            ////grdChemicalRegistration.View.PopulateColumns();
            ////grdChemicalRegistration.View.Columns[8].AppearanceCell.BackColor = Color.Moccasin; // 적정량 Column 색깔변경
            ////grdChemicalRegistration.View.OptionsView.AllowCellMerge = true; // CellMerge
            ////grdChemicalRegistration.View.FixColumn(new string[] { "PROCESSSEGMENTCLASSNAME", "EQUIPMENTNAME", "STATE", "DEGREE", "CHILDEQUIPMENTNAME", "CHEMICALNAME", "CHEMICALLEVEL", "MANAGEMENTSCOPE" });

            //RepositoryItemTimeEdit edit = new RepositoryItemTimeEdit();

            //edit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            //edit.Mask.EditMask = "([0-1]?[0-9]|2[0-3]):[0-5][0-9]";
            //edit.Mask.UseMaskAsDisplayFormat = true;

            //grdChemicalRegistration.View.Columns["COLLECTIONTIME"].ColumnEdit = edit;
        }

        /// <summary>
        /// 화면 Controls 초기화
        /// </summary>
        private void InitializeControls()
        {
            this.ControlLimitGroupCheck();

            this.rdoLeftGroup.Properties.Items[0].Description = SpcLibMessage.common.comCpk1031;//해석용
            this.rdoLeftGroup.Properties.Items[1].Description = SpcLibMessage.common.comCpk1040;//관리용
            this.rdoLeftGroup.Properties.Items[2].Description = SpcLibMessage.common.comCpk1041;//직접입력
            this.chkLeftEstimate.Text = SpcLibMessage.common.com1006;//추정치

            this.txtWholeMaxP1.Text = "";
            this.txtWholeMinP1.Text = "";
            this.txtWholeMaxP2.Text = "";
            this.txtWholeMinP2.Text = "";

            this.txtAllUslValue.Text = string.Empty;
            this.txtAllCslValue.Text = string.Empty;
            this.txtAllLslValue.Text = string.Empty;

            this.txtAllUclValue.Text = string.Empty;
            this.txtAllLclValue.Text = string.Empty;

            this.txtAllRUclValue.Text = string.Empty;
            this.txtAllRLclValue.Text = string.Empty;
        }

        /// <summary>
        /// 콤보박스 Chart Type 설정.
        /// </summary>
        /// <param name="option">
        /// 1: R, S, PL, MR
        /// 2: P, np, c, u
        /// 3: IMR
        /// 4: P, IMR
        /// </param>
        public void cboChartTypeSetting(int option = 0)
        {
            DataTable dtLeftChartType = SpcClass.CreateTableSpcComboBox(this.cboLeftChartType.Name.ToString());
            switch (option)
            {
                case 1:
                    SpcClass.SetcboLeftChartType(dtLeftChartType, ControlChartType.XBar_R);
                    SpcClass.SetcboLeftChartType(dtLeftChartType, ControlChartType.XBar_S);
                    SpcClass.SetcboLeftChartType(dtLeftChartType, ControlChartType.Merger);
                    SpcClass.SetcboLeftChartType(dtLeftChartType, ControlChartType.I_MR);
                    break;
                case 2:
                    SpcClass.SetcboLeftChartType(dtLeftChartType, ControlChartType.p);
                    SpcClass.SetcboLeftChartType(dtLeftChartType, ControlChartType.np);
                    SpcClass.SetcboLeftChartType(dtLeftChartType, ControlChartType.c);
                    SpcClass.SetcboLeftChartType(dtLeftChartType, ControlChartType.u);
                    break;
                case 3: //I-MR
                    SpcClass.SetcboLeftChartType(dtLeftChartType, ControlChartType.I_MR);
                    SpcClass.SetcboLeftChartType(dtLeftChartType, ControlChartType.XBar_R);
                    break;
                case 4: //P
                    SpcClass.SetcboLeftChartType(dtLeftChartType, ControlChartType.p);
                    SpcClass.SetcboLeftChartType(dtLeftChartType, ControlChartType.I_MR);
                    break;
                default:
                    SpcClass.SetcboLeftChartType(dtLeftChartType, ControlChartType.XBar_R);
                    SpcClass.SetcboLeftChartType(dtLeftChartType, ControlChartType.XBar_S);
                    SpcClass.SetcboLeftChartType(dtLeftChartType, ControlChartType.Merger);
                    SpcClass.SetcboLeftChartType(dtLeftChartType, ControlChartType.I_MR);
                    SpcClass.SetcboLeftChartType(dtLeftChartType, ControlChartType.p);
                    SpcClass.SetcboLeftChartType(dtLeftChartType, ControlChartType.np);
                    SpcClass.SetcboLeftChartType(dtLeftChartType, ControlChartType.c);
                    SpcClass.SetcboLeftChartType(dtLeftChartType, ControlChartType.u);
                    break;
            }


            this.cboLeftChartType.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            this.cboLeftChartType.ShowHeader = false;
            this.cboLeftChartType.DisplayMember = "Display";
            this.cboLeftChartType.ValueMember = "Value";
            this.cboLeftChartType.DataSource = dtLeftChartType;

            this.cboLeftChartType.ItemIndex = 0;

        }

        public void SetChartTypeCboSelectIndex(int index = 0)
        {
            this.cboLeftChartType.ItemIndex = index;
        }
        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            this.cboLeftChartType.TextChanged += new System.EventHandler(this.cboLeftChartType_TextChanged);
            this.rdoLeftGroup.SelectedIndexChanged += (s, e) => this.ControlLimitGroupCheck();
            this.ucXBarGrid01.ucXBar1.SpcCpkChartEnterEventHandler += (s, e, se) => ChartRawData(ucXBarGrid01.grp01.Tag.ToSafeString());
            this.ucXBarGrid01.ucXBar2.SpcCpkChartEnterEventHandler += (s, e, se) => ChartRawData(ucXBarGrid01.grp02.Tag.ToSafeString());
            this.ucXBarGrid01.ucXBar3.SpcCpkChartEnterEventHandler += (s, e, se) => ChartRawData(ucXBarGrid01.grp03.Tag.ToSafeString());
            this.ucXBarGrid01.ucXBar4.SpcCpkChartEnterEventHandler += (s, e, se) => ChartRawData(ucXBarGrid01.grp04.Tag.ToSafeString());
            //this.ucXBarGrid01.Resize += (s, e) =>
            //{
            //    //this.splMain.SplitterPosition = 170;
            //    //this.splMainSub.SplitterPosition = this.Height - (this.Height / 5);
            //};

            this.txtWholeMaxP1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtWholeMaxP1_KeyPress);
            this.txtWholeMinP1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtWholeMinP1_KeyPress);
            this.txtWholeMaxP2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtWholeMaxP2_KeyPress);
            this.txtWholeMinP2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtWholeMaxP2_KeyPress);

            this.txtAllUslValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAllUslValue_KeyPress);
            this.txtAllUslValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAllUslValue_KeyPress);
            this.txtAllCslValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAllCslValue_KeyPress);
            this.txtAllLslValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAllLslValue_KeyPress);
            this.txtAllUclValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAllUclValue_KeyPress);
            this.txtAllLclValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAllLclValue_KeyPress);
            this.txtAllRUclValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAllRUclValue_KeyPress);
            this.txtAllRLclValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAllRLclValue_KeyPress);

            this.ucXBarGrid01.spcNavigatorRowEnter += UcXBarGrid01_spcNavigatorRowEnter;
            //this.txtWholeMaxP1 += textBox1_KeyPress;
            ControlLimitGroupCheck();
        }

        /// <summary>
        /// Grid Navigator RowEnter 
        /// </summary>
        /// <param name="f"></param>
        private void UcXBarGrid01_spcNavigatorRowEnter(SpcEventNavigatorRowEnter f)
        {
            ChartRawData(ucXBarGrid01.grp01.Tag.ToSafeString());
        }

        private void ucXBarFrame_Load(object sender, EventArgs e)
        {
            this.InitializeEvent();
            this.btnReExecution.Text = SpcLibMessage.common.comCpk1043;//재분석
        }

        /// <summary>
        /// 통계 재분석 실행.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReExecution_Click(object sender, EventArgs e)
        {
            btnReExecution.Enabled = false;
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (_dtInputRawData != null && _dtInputRawData.Rows.Count > 0)
                {
                    //Chart 구분
                    string chartType = this.cboLeftChartType.GetDataValue().ToSafeString().ToUpper().Replace("_", "");
                    _AnalysisParameter.spcOption.chartName.xBarChartType = chartType;
                    _AnalysisParameter.spcOption.chartName.xCpkChartType = chartType;
                    _AnalysisParameter.spcOption.sigmaType = this.chkLeftEstimate.Checked ? SigmaType.Yes : SigmaType.No;//추정치 사용 유무
                    _AnalysisParameter.AgainAnalysisSigmaType = _AnalysisParameter.spcOption.sigmaType;
                    _isAgainAnalysis = true;
                    _AnalysisParameter.dtInputRawData = _dtInputRawData;
                    _AnalysisParameter.dtInputSpecData = _dtInputSpecData;

                    _AnalysisParameter.isAgainAnalysis = _isAgainAnalysis;
                    _AnalysisParameter.isAgainAnalysisXBar = false;
                    _AnalysisParameter.isAgainAnalysisCpk = true;
                    _AnalysisParameter.isAgainAnalysisButtonXBar = false;
                    _AnalysisParameter.isAgainAnalysisButtonCpk = true;
                    _AnalysisParameter.isAgainAnalysisOverRules = true;

                    //XBar Chart 분석 및 표시 실행.
                    this.AnalysisExecution(_AnalysisParameter);

                    SpcFrameChangeData f = new SpcFrameChangeData();
                    f.isAgainAnalysis = _AnalysisParameter.isAgainAnalysis;
                    f.isAgainAnalysisXBar = _AnalysisParameter.isAgainAnalysisXBar;
                    f.isAgainAnalysisCpk = _AnalysisParameter.isAgainAnalysisCpk;
                    f.isAgainAnalysisButtonXBar = _AnalysisParameter.isAgainAnalysisButtonXBar;
                    f.isAgainAnalysisButtonCpk = _AnalysisParameter.isAgainAnalysisButtonCpk;
                    f.isAgainAnalysisOverRules = _AnalysisParameter.isAgainAnalysisOverRules;
                    f.SPCOption = _AnalysisParameter.spcOption.CopyDeep();
                    switch (SpcFunction.isPublicTestMode)
                    {
                        case 0:
                            SpcCpkChartEnterEventHandler(f);
                            break;
                        case 1://Test Mode
                            SpcCpkChartEnterEventHandler(f);
                            break;
                        default:
                            SpcCpkChartEnterEventHandler(f);
                            break;
                    }

                    
                }
                else
                {
                    //조회한 자료가 없습니다. 우측의 조회버튼을 Click 하시길 바랍니다.
                    string messageData = SpcLibMessage.common.comCpk1027;
                    MessageChartXBarFrameInternal(messageData);
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Console.WriteLine(ex.Message);
                btnReExecution.Enabled = true;
            }
            btnReExecution.Enabled = true;
            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// cboLeftChartType_TabIndexChanged 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboLeftChartType_TabIndexChanged(object sender, EventArgs e)
        {
            string chartType = cboLeftChartType.GetDataValue().ToSafeString().ToUpper().Replace("_", "");
        }

        /// <summary>
        /// ChartType ComboBox TextChanged 이벤트
        /// Chart Control Option 초기화
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboLeftChartType_TextChanged(object sender, EventArgs e)
        {
            try
            {
                spcWaitOption.ChartTypeChange = true;
                SpcVtChartShowWaitAreaEventHandler?.Invoke(sender, e, spcWaitOption);
                ClearChartControlsXBAR();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                spcWaitOption.ChartTypeChange = false;
                SpcVtChartShowWaitAreaEventHandler?.Invoke(sender, e, spcWaitOption);
            }
        }

        /// <summary>
        /// Chart 관련 모든 Control Option 초기화
        /// </summary>
        public void ClearChartControlsXBAR()
        {
            //this.Cursor = Cursors.WaitCursor;
            cboLeftChartType.Enabled = false;
            btnReExecution.Enabled = false;
            Application.DoEvents();

            string chartType = cboLeftChartType.GetDataValue().ToSafeString().ToUpper().Replace("_", "");

            try
            {
                this.ucXBarGrid01.grp01.Text = "";
                this.ucXBarGrid01.grp02.Text = "";
                this.ucXBarGrid01.grp03.Text = "";
                this.ucXBarGrid01.grp04.Text = "";
                this.ucXBarGrid01.grp01.Tag = "";
                this.ucXBarGrid01.grp02.Tag = "";
                this.ucXBarGrid01.grp03.Tag = "";
                this.ucXBarGrid01.grp04.Tag = "";
                this.ucXBarGrid01.ucXBar1.ChartLegendsTitleChange(chartType);
                this.ucXBarGrid01.ucXBar2.ChartLegendsTitleChange(chartType);
                this.ucXBarGrid01.ucXBar3.ChartLegendsTitleChange(chartType);
                this.ucXBarGrid01.ucXBar4.ChartLegendsTitleChange(chartType);
                this.InitializeGrid(0, "", "");

                this.ucXBarGrid01.ClearNavigator();
                
                switch (chartType)
                {
                    case "XBARR":
                    case "R":
                    case "XBARS":
                    case "S":
                    case "XBARP":
                    case "PL":
                    case "IMR":
                    case "MR":
                    case "I":
                        ControlLimitOptionVisible(true);
                        if (this.ucXBarGrid01.ucXBar1.layoutItemRCCLValue.Visibility != LayoutVisibility.Always)
                        {
                            this.ucXBarGrid01.ucXBar1.DetailChartControlClearPCU(LayoutVisibility.Always);
                            this.ucXBarGrid01.ucXBar2.DetailChartControlClearPCU(LayoutVisibility.Always);
                            this.ucXBarGrid01.ucXBar3.DetailChartControlClearPCU(LayoutVisibility.Always);
                            this.ucXBarGrid01.ucXBar4.DetailChartControlClearPCU(LayoutVisibility.Always);
                        }
                    
                        break;
                    case "P":
                    case "NP":
                    case "C":
                    case "U":
                        ControlLimitOptionVisible(false);
                        if (this.ucXBarGrid01.ucXBar1.layoutItemRCCLValue.Visibility != LayoutVisibility.Never)
                        {
                            this.ucXBarGrid01.ucXBar1.DetailChartControlClearPCU(LayoutVisibility.Never);
                            this.ucXBarGrid01.ucXBar2.DetailChartControlClearPCU(LayoutVisibility.Never);
                            this.ucXBarGrid01.ucXBar3.DetailChartControlClearPCU(LayoutVisibility.Never);
                            this.ucXBarGrid01.ucXBar4.DetailChartControlClearPCU(LayoutVisibility.Never);
                        }
                    
                        break;
                    default:
                        break;
                }
                //Console.WriteLine(chartType);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                //this.Cursor = Cursors.Default;
            }
            finally
            {
                Application.DoEvents();
                cboLeftChartType.Enabled = true;
                btnReExecution.Enabled = true;
                //this.Cursor = Cursors.Default;
            }
        }
        /// <summary>
        /// Chart Type 변경시 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtWholeMaxP1_EditValueChanged(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Whole Check Leave 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtWholeMaxP1_Leave(object sender, EventArgs e)
        {
            DirectChartWholeRangeChange();
        }
        /// <summary>
        /// Whole Check Leave 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtWholeMinP1_Leave(object sender, EventArgs e)
        {
            DirectChartWholeRangeChange();
        }
        /// <summary>
        /// Whole Check Leave 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtWholeMaxP2_Leave(object sender, EventArgs e)
        {
            DirectChartWholeRangeChange();
        }
        /// <summary>
        /// Whole Check Leave 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtWholeMinP2_Leave(object sender, EventArgs e)
        {
            DirectChartWholeRangeChange();
        }

        /// <summary>
        /// KeyPress 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtWholeMaxP1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
                {
                    e.Handled = true;
                }

                // only allow one decimal point
                if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// KeyPress 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtWholeMinP1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
                {
                    e.Handled = true;
                }

                // only allow one decimal point
                if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// KeyPress 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtWholeMaxP2_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
                {
                    e.Handled = true;
                }

                // only allow one decimal point
                if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// KeyPress 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtWholeMinP2_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
                {
                    e.Handled = true;
                }

                // only allow one decimal point
                if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        //txtAllUslValue_KeyPress

        /// <summary>
        /// 직접입력 SPEE 값 입력 TextBox KeyPress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtAllUslValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
                {
                    e.Handled = true;
                }

                // only allow one decimal point
                if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// 직접입력 SPEE 값 입력 TextBox KeyPress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtAllCslValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
                {
                    e.Handled = true;
                }

                // only allow one decimal point
                if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// 직접입력 SPEE 값 입력 TextBox KeyPress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtAllLslValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
                {
                    e.Handled = true;
                }

                // only allow one decimal point
                if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// 직접입력 SPEE 값 입력 TextBox KeyPress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtAllUclValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
                {
                    e.Handled = true;
                }

                // only allow one decimal point
                if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// 직접입력 SPEE 값 입력 TextBox KeyPress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtAllLclValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
                {
                    e.Handled = true;
                }

                // only allow one decimal point
                if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// 직접입력 SPEE 값 입력 TextBox KeyPress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtAllRUclValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
                {
                    e.Handled = true;
                }

                // only allow one decimal point
                if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// 직접입력 SPEE 값 입력 TextBox KeyPress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtAllRLclValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
                {
                    e.Handled = true;
                }

                // only allow one decimal point
                if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void btnReExecutionTest_Click(object sender, EventArgs e)
        {
            btnReExecutionTestClick();
        }
        public AnalysisExecutionParameter btnReExecutionTestClick()
        {
            //단일 Test.
            //Chart 구분
            _AnalysisParameter.spcOption = SPCOption.Create();
            string chartType = this.cboLeftChartType.GetDataValue().ToSafeString().ToUpper().Replace("_", "");
            _AnalysisParameter.spcOption.chartName.xBarChartType = chartType;
            _AnalysisParameter.spcOption.chartName.xCpkChartType = chartType;
            _AnalysisParameter.spcOption.sigmaType = this.chkLeftEstimate.Checked ? SigmaType.Yes : SigmaType.No;//추정치 사용 유무
            _isAgainAnalysis = true;
            //_AnalysisParameter.dtInputRawData = _dtInputRawData;
            //_AnalysisParameter.dtInputSpecData = _dtInputSpecData;
            SpcFrameChangeData f = new SpcFrameChangeData();
            f.isAgainAnalysis = _isAgainAnalysis;
            f.SPCOption = _AnalysisParameter.spcOption;
            if (SpcFunction.isPublicTestMode != 3)
            {
                SpcCpkChartEnterEventHandler(f);
            }

            this.AnalysisExecutionTest(ref _AnalysisParameter);
            return _AnalysisParameter;
        }
        /// <summary>
        /// Test - 직접 Spec 입력
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTestDirectSpec_Click(object sender, EventArgs e)
        {
            this.txtAllUslValue.Text = "90";
            this.txtAllCslValue.Text = "70";
            this.txtAllLslValue.Text = "15";

            this.txtAllUclValue.Text = "60";
            this.txtAllLclValue.Text = "25";

            this.txtAllRUclValue.Text = "35";
            this.txtAllRLclValue.Text = "25";

        }
        #endregion

        #region Private Function
        /// <summary>
        /// 내부 Message 처리.
        /// </summary>
        /// <param name="message"></param>
        private void MessageChartXBarFrameInternal(string message)
        {
            try
            {
                spcMsg.mainMessage = message;
                SpcChartXBarFrameDirectMessageUserEventHandler?.Invoke(spcMsg);
            }
            catch (Exception ex)
            {
                message = string.Format("{0} - {1}", message, ex.Message);
                MessageBox.Show(message);
            }
        }
        /// <summary>
        /// Chart Type별 표시여부 결정.
        /// </summary>
        /// <param name="isValue"></param>
        private void ControlLimitOptionVisible(bool isValue)
        {
            if (this.lblAllRUcl.Visible != isValue)
            {
                this.lblAllRUcl.Visible = isValue;
                this.lblAllCsl.Visible = isValue;
                this.lblAllRLcl.Visible = isValue;
                this.txtAllRUclValue.Visible = isValue;
                this.txtAllCslValue.Visible = isValue;
                this.txtAllRLclValue.Visible = isValue;
            }
        }
        /// <summary>
        ///  Chart 하단 Grid Raw Data 표시.
        /// </summary>
        /// <param name="subGroupName"></param>
        /// <param name="samplingNameIndex"></param>
        private void ChartRawData(string subGroupName, int samplingNameIndex = 0)
        {
            if (_AnalysisParameter.spcOption == null)
            {
                return;
            }

            switch (_AnalysisParameter.spcOption.chartType)
            {
                case ControlChartType.XBar_R:
                case ControlChartType.XBar_S:
                case ControlChartType.Merger:
                    ChartRawDataDefault(subGroupName);
                    break;
                case ControlChartType.I_MR:
                    ChartRawDataSub01(subGroupName);
                    break;
                case ControlChartType.np:
                case ControlChartType.p:
                case ControlChartType.c:
                case ControlChartType.u:
                    ChartRawDataDefault(subGroupName);
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 하단 Grid Raw Data 표시 - 기본형.
        /// </summary>
        private void ChartRawDataDefault(string subGroupName)
        {
            //sia확인 : ucXBarFrame - 하단 Grid Raw Data 표시
            int i = 0;
            //grdSelectChartRawData.Caption = "";
            //grdSelectChartRawData.DataSource = null;
            //grdSelectChartRawData.View.ClearColumns();
            //grdSelectChartRawData.GridButtonItem = GridButtonItem.Export;

            if (this.ucXBarGrid01.spcPara.InputData == null)
            {
                return;
            }

            if (subGroupName == "")
            {
                return;
            }

            ////*Array 경계 기호 분할 작업
            //string[] delimiterChars_sym1 = { "@#$" };
            //string[] strDtcAry; //경계 기호 자료 저장 배열 변수.
            //strDtcAry = subGroupNames.Split(delimiterChars_sym1, StringSplitOptions.None);
            //if (strDtcAry != null && strDtcAry.Length > 0)
            //{
            //    searchFieldValue = strDtcAry[subGroupNameIndex];
            //}

            int subGroupMax = 0;
            int samplingMax = 0;
            string strSubgroupTitle = "";

            var sumSubGroup = from g in this.ucXBarGrid01.spcPara.InputData
                              where (g.SUBGROUP == subGroupName)
                              group g by new
                              {
                                  g.SUBGROUP,
                                  g.SAMPLING
                              }
                              into g
                              orderby g.Key.SAMPLING
                              select new
                              {
                                  vGROUPID = g.Max(s => s.GROUPID),
                                  vSAMPLEID = g.Max(s => s.SAMPLEID),
                                  vSUBGROUP = g.Key.SUBGROUP,
                                  vSAMPLING = g.Max(s => s.SAMPLING),
                                  vSUBGROUPNAME = g.Max(s => s.SUBGROUPNAME),
                                  vSAMPLINGNAME = g.Max(s => s.SAMPLINGNAME),
                                  vCOUNT = g.Count()
                              };



            foreach (var item in sumSubGroup)
            {
                subGroupMax++;
                if (item.vCOUNT > samplingMax)
                {
                    samplingMax = item.vCOUNT;
                }

                Console.WriteLine("{0} - {1}", item.vSAMPLING, item.vCOUNT);
            }
            
            Console.WriteLine("subGroupMax: {0},  samplingMax {1}", subGroupMax, samplingMax);
            Console.WriteLine("subGroupMax: {0},  samplingMax {1}", subGroupMax, samplingMax);

            int rowMax = 0;

            DataTable dataTable = new DataTable();
            string fieldName = string.Empty;

            dataTable.TableName = "ChartRaw";

            for (i = 0; i < subGroupMax; i++)
            {
                fieldName = string.Format("FIELD_{0:D3}", i + 1);
                dataTable.Columns.Add(fieldName, typeof(double));
            }

            for (i = 0; i < samplingMax; i++)
            {
                dataTable.Rows.Add(dataTable.NewRow());
            }

            //xbarData.SpcData.AsEnumerable().ToList<DataRow>().ForEach(r =>
            //{
            //    r["UOL"] = cntrolSpec.nXbar.uol; r["LOL"] = cntrolSpec.nXbar.lol;
            //    r["USL"] = cntrolSpec.nXbar.usl; r["CSL"] = cntrolSpec.nXbar.csl; r["LSL"] = cntrolSpec.nXbar.lsl;
            //    r["UCL"] = cntrolSpec.nXbar.ucl; r["CCL"] = cntrolSpec.nXbar.ccl; r["LCL"] = cntrolSpec.nXbar.lcl;
            //    //r["AUCL"] = nUCL + 15; r["ACCL"] = nCCL; r["ALCL"] = nLCL - 15;
            //});

            int fieldIndex = 0;
            int rowIndex = 0;
            _gridCaption = new string[subGroupMax];
            string samplingCaption = string.Empty;
            string samplingName = string.Empty;
            string samplingRow = string.Empty;

            var subGroupData = this.ucXBarGrid01.spcPara.InputData.Where(x => x.SUBGROUP == subGroupName)
                                                                    .OrderBy(r1 => r1.SAMPLING).ThenByDescending(r2 => r2.NVALUE);
            strSubgroupTitle = "";
            foreach (DataRow item in subGroupData)
            {
                samplingRow = SpcFunction.IsDbNck(item, "SAMPLING");

                if (samplingName != samplingRow)
                {
                    samplingCaption = samplingRow;
                    _gridCaption[fieldIndex] = samplingCaption;
                    fieldIndex++;
                    samplingName = samplingRow;
                    fieldName = string.Format("FIELD_{0:D3}", fieldIndex);
                    rowIndex = 0;
                }

                if (strSubgroupTitle == "")
                {
                    strSubgroupTitle = item["SUBGROUPNAME"].ToSafeString();
                    strSubgroupTitle = strSubgroupTitle.Replace(SpcSym.subgroupSep11, SpcSym.subgroupSep12);
                }

                DataRow dataRow = dataTable.Rows[rowIndex];
                dataRow[fieldName] = SpcFunction.IsDbNckDoubleMax(item, "NVALUE");

                rowIndex++;
                //dataTable.Rows.Add(dataRow);
            }

            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                InitializeGrid(subGroupMax, strSubgroupTitle, subGroupName);
                this.grdSelectChartRawData.DataSource = dataTable;
            }
            else
            {
                InitializeGrid(0, "");
            }

        }

        /// <summary>
        /// 하단 Grid Raw Data 표시 - 예외 01 - IMR형으로 사용.
        /// </summary>
        private void ChartRawDataSub01(string subGroupName)
        {
            int i = 0;
            //grdSelectChartRawData.Caption = "";
            //grdSelectChartRawData.View.ClearColumns();
            //grdSelectChartRawData = null;
            //grdSelectChartRawData.DataSource = null;
            
            //grdSelectChartRawData.GridButtonItem = GridButtonItem.Export;

            if (this.ucXBarGrid01.spcPara.InputData == null)
            {
                return;
            }

            if (subGroupName == "")
            {
                return;
            }

            ////*Array 경계 기호 분할 작업
            //string[] delimiterChars_sym1 = { "@#$" };
            //string[] strDtcAry; //경계 기호 자료 저장 배열 변수.
            //strDtcAry = subGroupNames.Split(delimiterChars_sym1, StringSplitOptions.None);
            //if (strDtcAry != null && strDtcAry.Length > 0)
            //{
            //    searchFieldValue = strDtcAry[subGroupNameIndex];
            //}

            int subGroupMax = 0;
            int samplingMax = 0;
            string strSubgroupTitle = "";
            try
            {

                var sumSubGroup = from g in this.ucXBarGrid01.spcPara.InputData
                                  where (g.SUBGROUP == subGroupName)
                                  group g by new
                                  {
                                      g.SUBGROUP,
                                      g.SAMPLING01
                                  }
                                  into g
                                  select new
                                  {
                                      vGROUPID = g.Max(s => s.GROUPID),
                                      vSAMPLEID = g.Max(s => s.SAMPLEID),
                                      vSUBGROUP = g.Key.SUBGROUP,
                                      vSAMPLING = g.Max(s => s.SAMPLING01),
                                      vSUBGROUPNAME = g.Max(s => s.SUBGROUPNAME),
                                      vSAMPLINGNAME = g.Max(s => s.SAMPLINGNAME01),
                                      vCOUNT = g.Count()
                                  };

                foreach (var item in sumSubGroup)
                {
                    subGroupMax++;
                    if (item.vCOUNT > samplingMax)
                    {
                        samplingMax = item.vCOUNT;
                    }

                    Console.WriteLine("{0} - {1}", item.vSAMPLING, item.vCOUNT);
                }

                Console.WriteLine("subGroupMax: {0},  samplingMax {1}", subGroupMax, samplingMax);
                Console.WriteLine("subGroupMax: {0},  samplingMax {1}", subGroupMax, samplingMax);

                int rowMax = 0;

                DataTable dataTable = new DataTable();
                string fieldName = string.Empty;

                dataTable.TableName = "ChartRaw";

                for (i = 0; i < subGroupMax; i++)
                {
                    fieldName = string.Format("FIELD_{0:D3}", i + 1);
                    dataTable.Columns.Add(fieldName, typeof(double));
                }

                for (i = 0; i < samplingMax; i++)
                {
                    dataTable.Rows.Add(dataTable.NewRow());
                }

                int fieldIndex = 0;
                int rowIndex = 0;
                _gridCaption = new string[subGroupMax];
                string samplingCaption = string.Empty;
                string samplingName = string.Empty;
                string samplingRow = string.Empty;

                var subGroupData = this.ucXBarGrid01.spcPara.InputData.Where(x => x.SUBGROUP == subGroupName);
                strSubgroupTitle = "";
                foreach (DataRow item in subGroupData)
                {
                    samplingRow = SpcFunction.IsDbNck(item, "SAMPLING01");

                    if (samplingName != samplingRow)
                    {
                        samplingCaption = samplingRow;
                        _gridCaption[fieldIndex] = samplingCaption;
                        fieldIndex++;
                        samplingName = samplingRow;
                        fieldName = string.Format("FIELD_{0:D3}", fieldIndex);
                        rowIndex = 0;
                    }

                    if (strSubgroupTitle == "")
                    {
                        strSubgroupTitle = item["SUBGROUPNAME"].ToSafeString();
                        strSubgroupTitle = strSubgroupTitle.Replace(SpcSym.subgroupSep11, SpcSym.subgroupSep12);
                    }

                    DataRow dataRow = dataTable.Rows[rowIndex];
                    dataRow[fieldName] = SpcFunction.IsDbNckDoubleMax(item, "NVALUE");
                    rowIndex++;
                }

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    InitializeGrid(subGroupMax, strSubgroupTitle, subGroupName);
                    this.grdSelectChartRawData.DataSource = dataTable;
                }
                else
                {
                    InitializeGrid(0, "");
                }
            }
            catch (Exception ex)
            {
                //sia필수확인 : 오류 확인 처리 할것 _gridCaption[fieldIndex] = samplingCaption; 1139 오류발생. IMR조회시 
                Console.WriteLine(ex.Message);
            }

        }

        /// <summary>
        /// 좌측 관리도 Limbit Option 처리
        /// </summary>
        private void ControlLimitGroupCheck()
        {
            switch (rdoLeftGroup.SelectedIndex)
            {
                case 0://해석용
                case 1://관리용
                    this.txtAllUslValue.ReadOnly = true;
                    this.txtAllCslValue.ReadOnly = true;
                    this.txtAllLslValue.ReadOnly = true;

                    this.txtAllUclValue.ReadOnly = true;
                    this.txtAllLclValue.ReadOnly = true;

                    this.txtAllRUclValue.ReadOnly = true;
                    this.txtAllRLclValue.ReadOnly = true;
                    break;
                case 2://직접입력
                    this.txtAllUslValue.Text = "";
                    this.txtAllCslValue.Text = "";
                    this.txtAllLslValue.Text = "";

                    this.txtAllUclValue.Text = "";
                    this.txtAllLclValue.Text = "";

                    this.txtAllRUclValue.Text = "";
                    this.txtAllRLclValue.Text = "";

                    this.txtAllUslValue.ReadOnly = false;
                    this.txtAllCslValue.ReadOnly = false;
                    this.txtAllLslValue.ReadOnly = false;

                    this.txtAllUclValue.ReadOnly = false;
                    this.txtAllLclValue.ReadOnly = false;

                    this.txtAllRUclValue.ReadOnly = false;
                    this.txtAllRLclValue.ReadOnly = false;
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// Whole Range 변경
        /// </summary>
        private void DirectChartWholeRangeChange()
        {
            //Whole Range
            ucXBarGrid01.chartGridWholeRange.WholeRangeP1Max = this.txtWholeMaxP1.Text.ToNullOrDouble();
            ucXBarGrid01.chartGridWholeRange.WholeRangeP1Min = this.txtWholeMinP1.Text.ToNullOrDouble();
            ucXBarGrid01.chartGridWholeRange.WholeRangeP2Max = this.txtWholeMaxP2.Text.ToNullOrDouble();
            ucXBarGrid01.chartGridWholeRange.WholeRangeP2Min = this.txtWholeMinP2.Text.ToNullOrDouble();
            if (this.ucXBarGrid01.chk01.Checked != false)
            {
                this.ucXBarGrid01.ucXBar1.ChartWholeRangeDirectChange(ucXBarGrid01.chartGridWholeRange);
            }
            if (this.ucXBarGrid01.chk02.Checked != false)
            {
                this.ucXBarGrid01.ucXBar2.ChartWholeRangeDirectChange(ucXBarGrid01.chartGridWholeRange);
            }
            if (this.ucXBarGrid01.chk03.Checked != false)
            {
                this.ucXBarGrid01.ucXBar3.ChartWholeRangeDirectChange(ucXBarGrid01.chartGridWholeRange);
            }
            if (this.ucXBarGrid01.chk04.Checked != false)
            {
                this.ucXBarGrid01.ucXBar4.ChartWholeRangeDirectChange(ucXBarGrid01.chartGridWholeRange);
            }
        }
        #endregion

        #region Public Function



        /// <summary>
        /// Xbar-R Chart 분석 및 표시 실행.
        /// </summary>
        public void AnalysisExecution(AnalysisExecutionParameter para)
        {
            _AnalysisParameter = para;
            _dtInputRawData = para.dtInputRawData;
            _dtInputSpecData = para.dtInputSpecData;
            AnalysisExecutionMain();
            ChartRawData(ucXBarGrid01.grp01.Tag.ToSafeString());
        }

        /// <summary>
        /// Xbar-R Chart 분석 및 표시 - Test
        /// </summary>
        public void AnalysisExecutionTest(ref AnalysisExecutionParameter para)
        {
            _AnalysisParameter = para;
            //_dtInputRawData = para.dtInputRawData;
            //_dtInputSpecData = para.dtInputSpecData;
            SPCPara specEdit = new SPCPara();
            
            //Chart 구분
            string chartType = cboLeftChartType.GetDataValue().ToSafeString().ToUpper().Replace("_", "");
            specEdit.ChartTypeMain(chartType);
            specEdit.spcOption = SPCOption.Create();

            //Sigma 구분
            bool isEstimate = this.chkLeftEstimate.Checked;
            specEdit.spcOption.sigmaType = isEstimate != true ? SigmaType.No : SigmaType.Yes;
            specEdit.spcOption.chartName.xBarChartType = chartType;
            specEdit.spcOption.chartName.xCpkChartType = chartType;

            //관리선 구분
            
            int rdoLeftGroupIndex = rdoLeftGroup.SelectedIndex;
            if (rdoLeftGroupIndex == 2)//직접입력
            {
                specEdit.spcOption.limitType = LimitType.Direct;
                specEdit.spcOption.LimitTypeUseIndex[1] = this.ucXBarGrid01.chk01.Checked;
                specEdit.spcOption.LimitTypeUseIndex[2] = this.ucXBarGrid01.chk02.Checked;
                specEdit.spcOption.LimitTypeUseIndex[3] = this.ucXBarGrid01.chk03.Checked;
                specEdit.spcOption.LimitTypeUseIndex[4] = this.ucXBarGrid01.chk04.Checked;

                specEdit.USL = this.txtAllUslValue.Text.ToNullOrDouble();
                specEdit.CSL = this.txtAllCslValue.Text.ToNullOrDouble();
                specEdit.LSL = this.txtAllLslValue.Text.ToNullOrDouble();

                specEdit.UCL = this.txtAllUclValue.Text.ToNullOrDouble();
                specEdit.LCL = this.txtAllLclValue.Text.ToNullOrDouble();

                specEdit.RUCL = this.txtAllRUclValue.Text.ToNullOrDouble();
                specEdit.RLCL = this.txtAllRLclValue.Text.ToNullOrDouble();
            }
            else if (rdoLeftGroupIndex == 1)//관리용
            {
                specEdit.spcOption.limitType = LimitType.Management;
                this.txtAllUclValue.Text = "";
                this.txtAllUslValue.Text = "";
                this.txtAllLclValue.Text = "";
                this.txtAllRUclValue.Text = "";
                this.txtAllCslValue.Text = "";
                this.txtAllRLclValue.Text = "";
            }
            else//해석용
            {
                specEdit.spcOption.limitType = LimitType.Interpretation;
                this.txtAllUclValue.Text = "";
                this.txtAllUslValue.Text = "";
                this.txtAllLclValue.Text = "";
                this.txtAllRUclValue.Text = "";
                this.txtAllCslValue.Text = "";
                this.txtAllRLclValue.Text = "";
            }

            //TEST 실행.
            this.ucXBarGrid01.XBarChartTestData(ref specEdit);//Test Data 입력
            this.ucXBarGrid01.XBarRExcuteTest(specEdit);//관리도 분석

            ChartRawData(ucXBarGrid01.grp01.Tag.ToSafeString());
        }

        /// <summary>
        /// 관리도 입력 자료 구성.
        /// Chart 분석 및 표시 실행.
        /// </summary>
        /// </summary>
        /// <param name="option">0-운영, 1-Test</param>
        private void AnalysisExecutionMain(int option = 0)
        {
            //_AnalysisParameter.dtInputRawMainFieldName.subgroupNameFiled 8/26 추가
            int i = 0;
            string firstSubgroup = "";
            SPCPara specEdit = new SPCPara();
            specEdit.spcOption = _AnalysisParameter.spcOption;
            specEdit.spcOption.sigmaType = _AnalysisParameter.spcOption.sigmaType;
            specEdit.isUnbiasing = _AnalysisParameter.spcOption.sigmaType;

            //int rowMaxInputData = 0;
            int rdoLeftGroupIndex = rdoLeftGroup.SelectedIndex;
            this.ucXBarGrid01.Dock = DockStyle.None;
            this.ucXBarGrid01.Dock = DockStyle.Fill;

            //Chart 구분
            string chartType = cboLeftChartType.GetDataValue().ToSafeString().ToUpper().Replace("_","");
            _AnalysisParameter.spcOption.ChartTypeSetting(chartType, ref _AnalysisParameter.spcOption);
            specEdit.ChartTypeMain(chartType);

            #region 관리도 ucXBarFrame DB 입력 Raw Data 구성 부분.
            //sia중요 : 관리도 ucXBarFrame DB 입력 Raw Data 구성 부분.

            //Raw Data 전이
            specEdit.tableRawData = _dtInputRawData;
            specEdit.tableSubgroupSpec = _dtInputSpecData;
            string chartMainType = specEdit.ChartTypeMain();
            //var rowData = _dtInputData.AsEnumerable().Where(w => w["ctype"].ToSafeString() = "").list();
            if (option == 0)
            {
                //Start - Raw Data 입력 Part.......................................................
                var rowDatas = _dtInputRawData.AsEnumerable();
                specEdit.InputData = new ParPIDataTable();
                var lstRow = rowDatas.AsParallel()
                        .OrderBy(or => or.Field<string>(_AnalysisParameter.dtInputRawMainFieldName.subgroupNameFiled))
                        .ThenBy(or => or.Field<string>(_AnalysisParameter.dtInputRawMainFieldName.samplingNameFiled));
                foreach (DataRow row in lstRow)
                {
                    DataRow dr = specEdit.InputData.NewRow();
                    if (firstSubgroup == "")
                    {
                        firstSubgroup = SpcFunction.IsDbNck(row, _AnalysisParameter.dtInputRawMainFieldName.subgroupNameFiled);
                    }
                    dr["GROUPID"] = 1;
                    dr["SAMPLEID"] = 1;//자동증가
                    dr["SUBGROUP"] = SpcFunction.IsDbNck(row, _AnalysisParameter.dtInputRawMainFieldName.subgroupNameFiled);
                    dr["SUBGROUPNAME"] = SpcFunction.IsDbNck(row, _AnalysisParameter.dtInputRawMainFieldName.subgroupNameFiledName);
                    dr["SAMPLING"] = SpcFunction.IsDbNck(row, _AnalysisParameter.dtInputRawMainFieldName.samplingNameFiled);
                    dr["SAMPLINGNAME"] = SpcFunction.IsDbNck(row, _AnalysisParameter.dtInputRawMainFieldName.samplingNameFiledName);
                    if (_AnalysisParameter.dtInputRawMainFieldName.samplingNameFiled01 != null
                            && _AnalysisParameter.dtInputRawMainFieldName.samplingNameFiled01 != "")
                    {
                        dr["SAMPLING01"] = SpcFunction.IsDbNck(row, _AnalysisParameter.dtInputRawMainFieldName.samplingNameFiled01);
                    }
                    else
                    {
                        dr["SAMPLING01"] = dr["SAMPLING"];
                    }
                    if (_AnalysisParameter.dtInputRawMainFieldName.samplingNameFiledName01 != null
                            && _AnalysisParameter.dtInputRawMainFieldName.samplingNameFiledName01 != "")
                    {
                        dr["SAMPLINGNAME01"] = SpcFunction.IsDbNck(row, _AnalysisParameter.dtInputRawMainFieldName.samplingNameFiledName01);
                    }
                    else
                    {
                        dr["SAMPLINGNAME01"] = dr["SAMPLINGNAME"];
                    }

                    if (_AnalysisParameter.dtInputRawMainFieldName.samplingNameFiled02 != null
                            && _AnalysisParameter.dtInputRawMainFieldName.samplingNameFiled02 != "")
                    {
                        dr["SAMPLING02"] = SpcFunction.IsDbNck(row, _AnalysisParameter.dtInputRawMainFieldName.samplingNameFiled02);
                    }
                    else
                    {
                        dr["SAMPLING02"] = dr["SAMPLING"];
                    }
                    if (_AnalysisParameter.dtInputRawMainFieldName.samplingNameFiledName02 != null
                            && _AnalysisParameter.dtInputRawMainFieldName.samplingNameFiledName02 != "")
                    {
                        dr["SAMPLINGNAME02"] = SpcFunction.IsDbNck(row, _AnalysisParameter.dtInputRawMainFieldName.samplingNameFiledName02);
                    }
                    else
                    {
                        dr["SAMPLINGNAME02"] = dr["SAMPLINGNAME"];
                    }
                    dr["NVALUE"] = SpcFunction.IsDbNckDoubleMin(row, "NVALUE");
                    dr["NSUBVALUE"] = SpcFunction.IsDbNckDoubleMin(row, "NSUBVALUE");
                    //if (chartType == "P")
                    //{
                    //    dr["NSUBVALUE"] = SpcFunction.IsDbNckDoubleMin(row, "NSUBVALUE");
                    //}
                    //else
                    //{
                    //    dr["NSUBVALUE"] = 0f;
                    //}

                    specEdit.InputData.Rows.Add(dr);
                }
                

                Console.WriteLine(specEdit.InputData.Rows.Count);
                //end - Raw Data 입력 Part .......................................................

                //Start - Spec Data 입력 Part.......................................................
                i = 0;
                int nLength = 0;
                specEdit.InputSpec = new ParPISpecDataTable();
                
                if (_dtInputSpecData != null && _dtInputSpecData.Rows.Count > 0)
                {
                    nLength = _dtInputSpecData.Rows.Count;
                    for (i = 0; i < nLength; i++)
                    {
                        DataRow rowSpec = _dtInputSpecData.Rows[i];
                        DataRow drs = specEdit.InputSpec.NewRow();
                        drs["TEMP"] = i;
                        drs["SUBGROUP"] = SpcFunction.IsDbNck(rowSpec, _AnalysisParameter.dtInputRawMainFieldName.subgroupNameFiled);
                        drs["CHARTTYPE"] = SpcFunction.IsDbNck(rowSpec, "CTYPE");
                        drs["USL"] = SpcFunction.IsDbNckDoubleMin(rowSpec, "USL");
                        drs["CSL"] = SpcFunction.IsDbNckDoubleMin(rowSpec, "SL");
                        drs["LSL"] = SpcFunction.IsDbNckDoubleMin(rowSpec, "LSL");
                        drs["UCL"] = SpcFunction.IsDbNckDoubleMin(rowSpec, "UCL");
                        drs["CCL"] = SpcFunction.IsDbNckDoubleMin(rowSpec, "SL");
                        drs["LCL"] = SpcFunction.IsDbNckDoubleMin(rowSpec, "LCL");
                        drs["UOL"] = SpcFunction.IsDbNckDoubleMin(rowSpec, "UOL");
                        drs["LOL"] = SpcFunction.IsDbNckDoubleMin(rowSpec, "LOL");
                        specEdit.InputSpec.Rows.Add(drs);
                    }
                }
                Console.WriteLine(specEdit.InputSpec.Rows.Count);
                //End - Spec Data 입력 Part.......................................................

            }
            #endregion

            if (specEdit.spcOption == null)
            {
                specEdit.spcOption = SPCOption.Create();
            }

            if (rdoLeftGroupIndex == 2)//직접입력
            {
                specEdit.spcOption.limitType = LimitType.Direct;
                specEdit.spcOption.LimitTypeUseIndex[1] = this.ucXBarGrid01.chk01.Checked;
                specEdit.spcOption.LimitTypeUseIndex[2] = this.ucXBarGrid01.chk02.Checked;
                specEdit.spcOption.LimitTypeUseIndex[3] = this.ucXBarGrid01.chk03.Checked;
                specEdit.spcOption.LimitTypeUseIndex[4] = this.ucXBarGrid01.chk04.Checked;

                specEdit.USL = this.txtAllUslValue.Text.ToNullOrDouble();
                specEdit.CSL = this.txtAllCslValue.Text.ToNullOrDouble();
                specEdit.LSL = this.txtAllLslValue.Text.ToNullOrDouble();

                specEdit.UCL = this.txtAllUclValue.Text.ToNullOrDouble();
                specEdit.LCL = this.txtAllLclValue.Text.ToNullOrDouble();

                specEdit.RUCL = this.txtAllRUclValue.Text.ToNullOrDouble();
                specEdit.RLCL = this.txtAllRLclValue.Text.ToNullOrDouble();

            }
            else if (rdoLeftGroupIndex == 1)//관리용
            {
                specEdit.spcOption.limitType = LimitType.Management;
                this.txtAllUslValue.Text = "";
                this.txtAllCslValue.Text = "";
                this.txtAllLslValue.Text = "";

                this.txtAllUclValue.Text  = "";
                this.txtAllLclValue.Text  = "";

                this.txtAllRUclValue.Text = "";
                this.txtAllRLclValue.Text = "";
                if (specEdit.InputSpec != null && specEdit.InputSpec.Rows.Count > 0 && firstSubgroup != "")
                {
                    var lstSpecMain = specEdit.InputSpec
                                        .Where(w => w.SUBGROUP == firstSubgroup && w.CHARTTYPE == specEdit.ChartTypeMain())
                                        .OrderBy(or => or.SUBGROUP);
                    foreach (var item in lstSpecMain)
                    {
                        Console.WriteLine(item.SUBGROUP);
                        specEdit.USL = item.USL;
                        specEdit.CSL = item.CSL;
                        specEdit.LSL = item.LSL;

                        specEdit.UCL = item.UCL;
                        specEdit.LCL = item.LCL;

                        specEdit.UOL = item.UOL;
                        specEdit.LOL = item.LOL;
                    }

                    string chartSubType = specEdit.ChartTypeSub();
                    if (chartSubType != "")
                    {
                        var lstSpecSub = specEdit.InputSpec
                        .Where(w => w.SUBGROUP == firstSubgroup && w.CHARTTYPE == chartSubType)
                        .OrderBy(or => or.SUBGROUP);
                        foreach (var item in lstSpecSub)
                        {
                            Console.WriteLine(item.SUBGROUP);
                            if(specEdit.USL == null) specEdit.USL = item.USL;
                            if (specEdit.CSL == null) specEdit.CSL = item.CSL;
                            if (specEdit.LSL == null) specEdit.LSL = item.LSL;

                            specEdit.RUCL = item.UCL;
                            //specEdit.RCCL = item.CCL;
                            specEdit.RLCL = item.LCL;
                        }
                        //this.txtAllRUclValue.Text = specEdit.RUCL.ToSafeString();
                        //this.txtAllCslValue.Text = specEdit.RCCL.ToSafeString();
                        //this.txtAllRLclValue.Text = specEdit.RLCL.ToSafeString();
                    }
                }
            }
            else//해석용
            {
                specEdit.spcOption.limitType = LimitType.Interpretation;
                this.txtAllUslValue.Text = "";
                this.txtAllCslValue.Text = "";
                this.txtAllLslValue.Text = "";

                this.txtAllUclValue.Text = "";
                this.txtAllLclValue.Text = "";

                this.txtAllRUclValue.Text = "";
                this.txtAllRLclValue.Text = "";
            }

            //this.ucXBarGrid01.spcPara.LOGMODE = "N";
            this.ucXBarGrid01.spcPara.SpecMode = 0;  
            this.ucXBarGrid01.spcPara.isUnbiasing = chkLeftEstimate.Checked ? SigmaType.Yes : SigmaType.No;

            this.ucXBarGrid01.spcPara.USL = specEdit.USL;
            this.ucXBarGrid01.spcPara.CSL = specEdit.CSL;
            this.ucXBarGrid01.spcPara.LSL = specEdit.LSL;

            this.ucXBarGrid01.spcPara.UCL = specEdit.UCL;
            this.ucXBarGrid01.spcPara.CCL = specEdit.CCL;
            this.ucXBarGrid01.spcPara.LCL = specEdit.LCL;

            this.ucXBarGrid01.spcPara.UOL = specEdit.UOL;
            this.ucXBarGrid01.spcPara.LOL = specEdit.LOL;

            this.ucXBarGrid01.spcPara.RUCL = specEdit.RUCL;
            this.ucXBarGrid01.spcPara.RCCL = specEdit.RCCL;
            this.ucXBarGrid01.spcPara.RLCL = specEdit.RLCL;
            //this.ucXBarGrid01.spcPara.UCL = this.txtAllUclValue.

            //*관리도 실행.
            this.ucXBarGrid01.XBarRExcute(specEdit);
            //this.ucXBarGrid01.XBarRExcuteTest();

            RtnControlDataTable staRowData = this.ucXBarGrid01.staRowData;

            string subGroupName = this.ucXBarGrid01.grp01.Tag.ToSafeString();
            if (subGroupName != "")
            {
                this.ChartRawData(subGroupName);
            }

            _spcPara = specEdit;
            //RtnControlDataTable staRowData = this.ucXBarGrid01.chartData;
            //if (staRowData != null && staRowData.Rows.Count > 0)
            //{
            //    this.grdChemicalRegistration.DataSource = staRowData;
            //    //this.grdChemicalRegistration.Caption
            //}


        }




        #endregion


    }
}
