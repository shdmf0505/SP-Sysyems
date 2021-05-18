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
using DevExpress.XtraCharts;
using DevExpress.Utils;
using Micube.Framework.SmartControls.Validations;
#endregion

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프로그램명 : 기준정보 > Setup > 품목유형 등록 및 조회
    /// 업 무 설명 : 품목 유형등록 
    /// 생  성  자 : 윤성원
    /// 생  성  일 : 2019-06-27
    /// 수정 이 력 : 
    /// 
    /// 
    /// </summary> 
	public partial class CAMRequest : SmartConditionManualBaseForm
    {

        #region Local Variables
        #endregion

        #region 생성자
        public CAMRequest()
        {
            InitializeComponent();
            InitializeEvent();
        }
        #endregion


        #region 초기화
        /// <summary>
        /// 조회 조건 초기화
        /// </summary>
        protected override void InitializeCondition()
        {

            base.InitializeCondition();
            grdCamRequest.Hide();
            grdCamChang.Hide();
            //승인유형
            //Conditions.AddComboBox("APPROVALTYPE", new SqlQuery("GetCodeList", "00001", "CODECLASSID=ApprovalType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID");

            InitializeCondition_Popup();
         

        }


        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            if (this._parameters != null)
            {
                SmartSelectPopupEdit Popupedit = Conditions.GetControl<SmartSelectPopupEdit>("CAMREQUESTID");
                Popupedit.SetValue(_parameters["CAMREQUESTID"].ToString());
                Popupedit.Text = _parameters["CAMREQUESTID"].ToString();
                Popupedit.Refresh();
            }

        }

        #endregion

        #region 컨텐츠 영역 초기화

        private void InitializeControl()
        {
            // 모델 유형
            cboMODELTYPE.DisplayMember = "CODENAME";
            cboMODELTYPE.ValueMember = "CODEID";
            cboMODELTYPE.ShowHeader = false;
            Dictionary<string, object> ParamMODELTYPE = new Dictionary<string, object>();
            ParamMODELTYPE.Add("CODECLASSID", "ModelType");
            ParamMODELTYPE.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtValidState = SqlExecuter.Query("GetCodeList", "00001", ParamMODELTYPE);
            cboMODELTYPE.DataSource = dtValidState;

            // 작업구분
            cboWORKTYPE.DisplayMember = "CODENAME";
            cboWORKTYPE.ValueMember = "CODEID";
            cboWORKTYPE.ShowHeader = false;
            Dictionary<string, object> ParamWORKTYPE = new Dictionary<string, object>();
            ParamWORKTYPE.Add("CODECLASSID", "JobType");
            ParamWORKTYPE.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtParamWORKTYPE = SqlExecuter.Query("GetCodeList", "00001", ParamWORKTYPE);
            cboWORKTYPE.DataSource = dtParamWORKTYPE;

            // 진행구분
            cboPROCESSTYPE.DisplayMember = "CODENAME";
            cboPROCESSTYPE.ValueMember = "CODEID";
            cboPROCESSTYPE.ShowHeader = false;
            Dictionary<string, object> ParamPROCESSTYPE = new Dictionary<string, object>();
            ParamPROCESSTYPE.Add("CODECLASSID", "ProductionClass");
            ParamPROCESSTYPE.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtParamPROCESSTYPE = SqlExecuter.Query("GetCodeList", "00001", ParamPROCESSTYPE);
            cboPROCESSTYPE.DataSource = dtParamPROCESSTYPE;


            // 회로보정 내층
            cboINNERLAYERCORRECTION.DisplayMember = "CODENAME";
            cboINNERLAYERCORRECTION.ValueMember = "CODEID";
            cboINNERLAYERCORRECTION.ShowHeader = false;
            Dictionary<string, object> ParamINNERLAYERCORRECTION = new Dictionary<string, object>();
            ParamINNERLAYERCORRECTION.Add("CODECLASSID", "InnerLayerCorrection");
            ParamINNERLAYERCORRECTION.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtINNERLAYERCORRECTION = SqlExecuter.Query("GetCodeList", "00001", ParamINNERLAYERCORRECTION);
            cboINNERLAYERCORRECTION.DataSource = dtINNERLAYERCORRECTION;

            // 회로보정 외층
            cboOUTERLAYERCORRECTION.DisplayMember = "CODENAME";
            cboOUTERLAYERCORRECTION.ValueMember = "CODEID";
            cboOUTERLAYERCORRECTION.ShowHeader = false;
            Dictionary<string, object> ParamOUTERLAYERCORRECTION = new Dictionary<string, object>();
            ParamOUTERLAYERCORRECTION.Add("CODECLASSID", "OuterLayerCorrection");
            ParamOUTERLAYERCORRECTION.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtOUTERLAYERCORRECTION = SqlExecuter.Query("GetCodeList", "00001", ParamOUTERLAYERCORRECTION);
            cboINNERLAYERCORRECTION.DataSource = dtOUTERLAYERCORRECTION;

            // OBL 보정치
            cboOBLCORRECTION.DisplayMember = "CODENAME";
            cboOBLCORRECTION.ValueMember = "CODEID";
            cboOBLCORRECTION.ShowHeader = false;
            Dictionary<string, object> ParamOBLCORRECTION = new Dictionary<string, object>();
            ParamOBLCORRECTION.Add("CODECLASSID", "OBLCorrection");
            ParamOBLCORRECTION.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtOBLCORRECTION = SqlExecuter.Query("GetCodeList", "00001", ParamOBLCORRECTION);
            cboOBLCORRECTION.DataSource = dtOBLCORRECTION;

            // 쏠림동박적용
            cboAPPLYINGCOPPERFOIL.DisplayMember = "CODENAME";
            cboAPPLYINGCOPPERFOIL.ValueMember = "CODEID";
            cboAPPLYINGCOPPERFOIL.ShowHeader = false;
            Dictionary<string, object> ParamAPPLYINGCOPPERFOIL = new Dictionary<string, object>();
            ParamAPPLYINGCOPPERFOIL.Add("CODECLASSID", "ApplyingCopperFoil");
            ParamAPPLYINGCOPPERFOIL.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtAPPLYINGCOPPERFOIL = SqlExecuter.Query("GetCodeList", "00001", ParamAPPLYINGCOPPERFOIL);
            cboAPPLYINGCOPPERFOIL.DataSource = dtAPPLYINGCOPPERFOIL;

            // 적용층 단위
            cboAPPLIEDLAYERUNIT.DisplayMember = "CODENAME";
            cboAPPLIEDLAYERUNIT.ValueMember = "CODEID";
            cboAPPLIEDLAYERUNIT.ShowHeader = false;
            Dictionary<string, object> ParamAPPLIEDLAYERUNIT = new Dictionary<string, object>();
            ParamAPPLIEDLAYERUNIT.Add("CODECLASSID", "AppliedLayerUnit");
            ParamAPPLIEDLAYERUNIT.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtAPPLIEDLAYERUNIT = SqlExecuter.Query("GetCodeList", "00001", ParamAPPLIEDLAYERUNIT);
            cboAPPLIEDLAYERUNIT.DataSource = dtAPPLIEDLAYERUNIT;

            // ZIP 단자
            cboZIPTERMINAL.DisplayMember = "CODENAME";
            cboZIPTERMINAL.ValueMember = "CODEID";
            cboZIPTERMINAL.ShowHeader = false;
            Dictionary<string, object> ParamZIPTERMINAL = new Dictionary<string, object>();
            ParamZIPTERMINAL.Add("CODECLASSID", "ZIPTerminal");
            ParamZIPTERMINAL.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtZIPTERMINAL = SqlExecuter.Query("GetCodeList", "00001", ParamZIPTERMINAL);
            cboZIPTERMINAL.DataSource = dtZIPTERMINAL;

            // PAD 보정
            cboPADCORRECTION.DisplayMember = "CODENAME";
            cboPADCORRECTION.ValueMember = "CODEID";
            cboPADCORRECTION.ShowHeader = false;
            Dictionary<string, object> ParamPADCORRECTION = new Dictionary<string, object>();
            ParamPADCORRECTION.Add("CODECLASSID", "PADCorrection");
            ParamPADCORRECTION.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtPadcorrection = SqlExecuter.Query("GetCodeList", "00001", ParamPADCORRECTION);
            cboPADCORRECTION.DataSource = dtPadcorrection;

            // BBT 기준
            cboBBTSTANDARD.DisplayMember = "CODENAME";
            cboBBTSTANDARD.ValueMember = "CODEID";
            cboBBTSTANDARD.ShowHeader = false;
            Dictionary<string, object> ParamBBTSTANDARD = new Dictionary<string, object>();
            ParamBBTSTANDARD.Add("CODECLASSID", "BBTStandard");
            ParamBBTSTANDARD.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtBBTSTANDARD = SqlExecuter.Query("GetCodeList", "00001", ParamBBTSTANDARD);
            cboBBTSTANDARD.DataSource = dtBBTSTANDARD;

            // 증여여부
            cboINCREASEDPRODUCTION.DisplayMember = "CODENAME";
            cboINCREASEDPRODUCTION.ValueMember = "CODEID";
            cboINCREASEDPRODUCTION.ShowHeader = false;
            Dictionary<string, object> ParamINCREASEDPRODUCTION = new Dictionary<string, object>();
            ParamINCREASEDPRODUCTION.Add("CODECLASSID", "IncreasedProduction");
            ParamINCREASEDPRODUCTION.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtINCREASEDPRODUCTION = SqlExecuter.Query("GetCodeList", "00001", ParamINCREASEDPRODUCTION);
            cboINCREASEDPRODUCTION.DataSource = dtINCREASEDPRODUCTION;

            // Jig제작
            cboJIGPRODUCTION.DisplayMember = "CODENAME";
            cboJIGPRODUCTION.ValueMember = "CODEID";
            cboJIGPRODUCTION.ShowHeader = false;
            Dictionary<string, object> ParamJIGPRODUCTION = new Dictionary<string, object>();
            ParamJIGPRODUCTION.Add("CODECLASSID", "Jigproduction");
            ParamJIGPRODUCTION.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtJIGPRODUCTION = SqlExecuter.Query("GetCodeList", "00001", ParamJIGPRODUCTION);
            cboJIGPRODUCTION.DataSource = dtJIGPRODUCTION;

            // 단자구분
            cboTERMINALTYPE.DisplayMember = "CODENAME";
            cboTERMINALTYPE.ValueMember = "CODEID";
            cboTERMINALTYPE.ShowHeader = false;
            Dictionary<string, object> ParamTERMINALTYPE = new Dictionary<string, object>();
            ParamTERMINALTYPE.Add("CODECLASSID", "TerminalType");
            ParamTERMINALTYPE.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtTERMINALTYPE = SqlExecuter.Query("GetCodeList", "00001", ParamTERMINALTYPE);
            cboTERMINALTYPE.DataSource = dtTERMINALTYPE;

            // BBT방식
            cboBBTMETHOD.DisplayMember = "CODENAME";
            cboBBTMETHOD.ValueMember = "CODEID";
            cboBBTMETHOD.ShowHeader = false;
            Dictionary<string, object> ParamBBTMETHOD = new Dictionary<string, object>();
            ParamBBTMETHOD.Add("CODECLASSID", "BBTMethod");
            ParamBBTMETHOD.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtBBTMETHOD = SqlExecuter.Query("GetCodeList", "00001", ParamBBTMETHOD);
            cboBBTMETHOD.DataSource = dtBBTMETHOD;

            //BBT 제작처 기준
            cboBBTMANUFACTURINGSTANDARDS.DisplayMember = "CODENAME";
            cboBBTMANUFACTURINGSTANDARDS.ValueMember = "CODEID";
            cboBBTMANUFACTURINGSTANDARDS.ShowHeader = false;
            Dictionary<string, object> ParamBBTMANUFACTURINGSTANDARDS = new Dictionary<string, object>();
            ParamBBTMANUFACTURINGSTANDARDS.Add("CODECLASSID", "BBTManufacturingStandards");
            ParamBBTMANUFACTURINGSTANDARDS.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtBBTMANUFACTURINGSTANDARDS = SqlExecuter.Query("GetCodeList", "00001", ParamBBTMANUFACTURINGSTANDARDS);
            cboBBTMANUFACTURINGSTANDARDS.DataSource = dtBBTMANUFACTURINGSTANDARDS;

            //Micro Short 기준
            cboMICROSHORTSTANDARD.DisplayMember = "CODENAME";
            cboMICROSHORTSTANDARD.ValueMember = "CODEID";
            cboMICROSHORTSTANDARD.ShowHeader = false;
            Dictionary<string, object> ParamMICROSHORTSTANDARD = new Dictionary<string, object>();
            ParamMICROSHORTSTANDARD.Add("CODECLASSID", "MicroShortStandard");
            ParamMICROSHORTSTANDARD.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtMICROSHORTSTANDARD = SqlExecuter.Query("GetCodeList", "00001", ParamMICROSHORTSTANDARD);
            cboMICROSHORTSTANDARD.DataSource = dtMICROSHORTSTANDARD;

            //도금 유형
            cboPLATINGTYPE.DisplayMember = "CODENAME";
            cboPLATINGTYPE.ValueMember = "CODEID";
            cboPLATINGTYPE.ShowHeader = false;
            Dictionary<string, object> ParamPLATINGTYPE = new Dictionary<string, object>();
            ParamPLATINGTYPE.Add("CODECLASSID", "PlatingType");
            ParamPLATINGTYPE.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtPLATINGTYPE = SqlExecuter.Query("GetCodeList", "00001", ParamPLATINGTYPE);
            cboPLATINGTYPE.DataSource = dtPLATINGTYPE;


            //외곽 Guide
            cboOUTERGUIDE.DisplayMember = "CODENAME";
            cboOUTERGUIDE.ValueMember = "CODEID";
            cboOUTERGUIDE.ShowHeader = false;
            Dictionary<string, object> ParamOUTERGUIDE = new Dictionary<string, object>();
            ParamOUTERGUIDE.Add("CODECLASSID", "OuterGuide");
            ParamOUTERGUIDE.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtOUTERGUIDE = SqlExecuter.Query("GetCodeList", "00001", ParamOUTERGUIDE);
            cboOUTERGUIDE.DataSource = dtOUTERGUIDE;

            //Trimming인식마크
            cboTRIMMINGMARK.DisplayMember = "CODENAME";
            cboTRIMMINGMARK.ValueMember = "CODEID";
            cboTRIMMINGMARK.ShowHeader = false;
            Dictionary<string, object> ParamTRIMMINGMARK = new Dictionary<string, object>();
            ParamTRIMMINGMARK.Add("CODECLASSID", "TrimmingMark");
            ParamTRIMMINGMARK.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtTRIMMINGMARK = SqlExecuter.Query("GetCodeList", "00001", ParamTRIMMINGMARK);
            cboTRIMMINGMARK.DataSource = dtTRIMMINGMARK;

            //동박방향 C면
            cboCCOPPERFOIL.DisplayMember = "CODENAME";
            cboCCOPPERFOIL.ValueMember = "CODEID";
            cboCCOPPERFOIL.ShowHeader = false;
            Dictionary<string, object> ParamCCOPPERFOIL = new Dictionary<string, object>();
            ParamCCOPPERFOIL.Add("CODECLASSID", "TopAndBottom");
            ParamCCOPPERFOIL.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtCCOPPERFOIL = SqlExecuter.Query("GetCodeList", "00001", ParamCCOPPERFOIL);
            cboCCOPPERFOIL.DataSource = dtCCOPPERFOIL;

            //동박방향 S면
            cboSCOPPERFOIL.DisplayMember = "CODENAME";
            cboSCOPPERFOIL.ValueMember = "CODEID";
            cboSCOPPERFOIL.ShowHeader = false;
            Dictionary<string, object> ParamSCOPPERFOIL = new Dictionary<string, object>();
            ParamSCOPPERFOIL.Add("CODECLASSID", "TopAndBottom");
            ParamSCOPPERFOIL.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtSCOPPERFOIL = SqlExecuter.Query("GetCodeList", "00001", ParamSCOPPERFOIL);
            cboSCOPPERFOIL.DataSource = dtSCOPPERFOIL;

            //동박방향 2L
            cboCOPPERFOIL2L.DisplayMember = "CODENAME";
            cboCOPPERFOIL2L.ValueMember = "CODEID";
            cboCOPPERFOIL2L.ShowHeader = false;
            Dictionary<string, object> ParamCOPPERFOIL2L = new Dictionary<string, object>();
            ParamCOPPERFOIL2L.Add("CODECLASSID", "TopAndBottom");
            ParamCOPPERFOIL2L.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtCOPPERFOIL2L = SqlExecuter.Query("GetCodeList", "00001", ParamCOPPERFOIL2L);
            cboCOPPERFOIL2L.DataSource = dtCOPPERFOIL2L;

            //동박방향 3L
            cboCOPPERFOIL3L.DisplayMember = "CODENAME";
            cboCOPPERFOIL3L.ValueMember = "CODEID";
            cboCOPPERFOIL3L.ShowHeader = false;
            Dictionary<string, object> ParamCOPPERFOIL3L = new Dictionary<string, object>();
            ParamCOPPERFOIL3L.Add("CODECLASSID", "TopAndBottom");
            ParamCOPPERFOIL3L.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtCOPPERFOIL3L = SqlExecuter.Query("GetCodeList", "00001", ParamCOPPERFOIL3L);
            cboCOPPERFOIL3L.DataSource = dtCOPPERFOIL3L;

            //동박방향 4L
            cboCOPPERFOIL4L.DisplayMember = "CODENAME";
            cboCOPPERFOIL4L.ValueMember = "CODEID";
            cboCOPPERFOIL4L.ShowHeader = false;
            Dictionary<string, object> ParamCOPPERFOIL4L = new Dictionary<string, object>();
            ParamCOPPERFOIL4L.Add("CODECLASSID", "TopAndBottom");
            ParamCOPPERFOIL4L.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtCOPPERFOIL4L = SqlExecuter.Query("GetCodeList", "00001", ParamCOPPERFOIL4L);
            cboCOPPERFOIL4L.DataSource = dtCOPPERFOIL4L;

            //동박방향 5L
            cboCOPPERFOIL5L.DisplayMember = "CODENAME";
            cboCOPPERFOIL5L.ValueMember = "CODEID";
            cboCOPPERFOIL5L.ShowHeader = false;
            Dictionary<string, object> ParamCOPPERFOIL5L = new Dictionary<string, object>();
            ParamCOPPERFOIL5L.Add("CODECLASSID", "TopAndBottom");
            ParamCOPPERFOIL5L.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtCOPPERFOIL5L = SqlExecuter.Query("GetCodeList", "00001", ParamCOPPERFOIL5L);
            cboCOPPERFOIL5L.DataSource = dtCOPPERFOIL5L;

            //동박방향 6L
            cboCOPPERFOIL6L.DisplayMember = "CODENAME";
            cboCOPPERFOIL6L.ValueMember = "CODEID";
            cboCOPPERFOIL6L.ShowHeader = false;
            Dictionary<string, object> ParamCOPPERFOIL6L = new Dictionary<string, object>();
            ParamCOPPERFOIL6L.Add("CODECLASSID", "TopAndBottom");
            ParamCOPPERFOIL6L.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtCOPPERFOIL6L = SqlExecuter.Query("GetCodeList", "00001", ParamCOPPERFOIL6L);
            cboCOPPERFOIL6L.DataSource = dtCOPPERFOIL6L;

            //동박방향 7L
            cboCOPPERFOIL7L.DisplayMember = "CODENAME";
            cboCOPPERFOIL7L.ValueMember = "CODEID";
            cboCOPPERFOIL7L.ShowHeader = false;
            Dictionary<string, object> ParamCOPPERFOIL7L = new Dictionary<string, object>();
            ParamCOPPERFOIL7L.Add("CODECLASSID", "TopAndBottom");
            ParamCOPPERFOIL7L.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtCOPPERFOIL7L = SqlExecuter.Query("GetCodeList", "00001", ParamCOPPERFOIL7L);
            cboCOPPERFOIL7L.DataSource = dtCOPPERFOIL7L;

            //동박방향 8L
            cboCOPPERFOIL8L.DisplayMember = "CODENAME";
            cboCOPPERFOIL8L.ValueMember = "CODEID";
            cboCOPPERFOIL8L.ShowHeader = false;
            Dictionary<string, object> ParamCOPPERFOIL8L = new Dictionary<string, object>();
            ParamCOPPERFOIL8L.Add("CODECLASSID", "TopAndBottom");
            ParamCOPPERFOIL8L.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtCOPPERFOIL8L = SqlExecuter.Query("GetCodeList", "00001", ParamCOPPERFOIL8L);
            cboCOPPERFOIL8L.DataSource = dtCOPPERFOIL8L;

            //동박방향 9L
            cboCOPPERFOIL9L.DisplayMember = "CODENAME";
            cboCOPPERFOIL9L.ValueMember = "CODEID";
            cboCOPPERFOIL9L.ShowHeader = false;
            Dictionary<string, object> ParamCOPPERFOIL9L = new Dictionary<string, object>();
            ParamCOPPERFOIL9L.Add("CODECLASSID", "TopAndBottom");
            ParamCOPPERFOIL9L.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtCOPPERFOIL9L = SqlExecuter.Query("GetCodeList", "00001", ParamCOPPERFOIL9L);
            cboCOPPERFOIL9L.DataSource = dtCOPPERFOIL9L;

            //D/Etching 적용 내층단면
            cboDETCHINGINNERLAYER.DisplayMember = "CODENAME";
            cboDETCHINGINNERLAYER.ValueMember = "CODEID";
            cboDETCHINGINNERLAYER.ShowHeader = false;
            Dictionary<string, object> ParamDETCHINGINNERLAYER = new Dictionary<string, object>();
            ParamDETCHINGINNERLAYER.Add("CODECLASSID", "YesNo");
            ParamDETCHINGINNERLAYER.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtDETCHINGINNERLAYER = SqlExecuter.Query("GetCodeList", "00001", ParamDETCHINGINNERLAYER);
            cboDETCHINGINNERLAYER.DataSource = dtDETCHINGINNERLAYER;

            //D/Etching 적용 외층단면
            cboDETCHINGOUTERLAYER.DisplayMember = "CODENAME";
            cboDETCHINGOUTERLAYER.ValueMember = "CODEID";
            cboDETCHINGOUTERLAYER.ShowHeader = false;
            Dictionary<string, object> ParamDETCHINGOUTERLAYER = new Dictionary<string, object>();
            ParamDETCHINGOUTERLAYER.Add("CODECLASSID", "YesNo");
            ParamDETCHINGOUTERLAYER.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtDETCHINGOUTERLAYER = SqlExecuter.Query("GetCodeList", "00001", ParamDETCHINGOUTERLAYER);
            cboDETCHINGOUTERLAYER.DataSource = dtDETCHINGOUTERLAYER;

            //D/Etching적용내층양면동도금
            cboDETCHINGDOUBLESIDEDPLATING.DisplayMember = "CODENAME";
            cboDETCHINGDOUBLESIDEDPLATING.ValueMember = "CODEID";
            cboDETCHINGDOUBLESIDEDPLATING.ShowHeader = false;
            Dictionary<string, object> ParamDETCHINGDOUBLESIDEDPLATING = new Dictionary<string, object>();
            ParamDETCHINGDOUBLESIDEDPLATING.Add("CODECLASSID", "YesNo");
            ParamDETCHINGDOUBLESIDEDPLATING.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtDETCHINGDOUBLESIDEDPLATING = SqlExecuter.Query("GetCodeList", "00001", ParamDETCHINGDOUBLESIDEDPLATING);
            cboDETCHINGDOUBLESIDEDPLATING.DataSource = dtDETCHINGDOUBLESIDEDPLATING;

            //D/Etching적용내층양면동도금(분리형)
            cboDETCHINGDOUBLESIDEDPLATINGREMOVABLE.DisplayMember = "CODENAME";
            cboDETCHINGDOUBLESIDEDPLATINGREMOVABLE.ValueMember = "CODEID";
            cboDETCHINGDOUBLESIDEDPLATINGREMOVABLE.ShowHeader = false;
            Dictionary<string, object> ParamDETCHINGDOUBLESIDEDPLATINGREMOVABLE = new Dictionary<string, object>();
            ParamDETCHINGDOUBLESIDEDPLATINGREMOVABLE.Add("CODECLASSID", "YesNo");
            ParamDETCHINGDOUBLESIDEDPLATINGREMOVABLE.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtDETCHINGDOUBLESIDEDPLATINGREMOVABLE = SqlExecuter.Query("GetCodeList", "00001", ParamDETCHINGDOUBLESIDEDPLATINGREMOVABLE);
            cboDETCHINGDOUBLESIDEDPLATINGREMOVABLE.DataSource = dtDETCHINGDOUBLESIDEDPLATINGREMOVABLE;

            //D/Etching 적용 회로폭 60이하모델
            cboDETCHINGCIRCUITWIDTHLESS.DisplayMember = "CODENAME";
            cboDETCHINGCIRCUITWIDTHLESS.ValueMember = "CODEID";
            cboDETCHINGCIRCUITWIDTHLESS.ShowHeader = false;
            Dictionary<string, object> ParamDETCHINGCIRCUITWIDTHLESS = new Dictionary<string, object>();
            ParamDETCHINGCIRCUITWIDTHLESS.Add("CODECLASSID", "YesNo");
            ParamDETCHINGCIRCUITWIDTHLESS.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtDETCHINGCIRCUITWIDTHLESS = SqlExecuter.Query("GetCodeList", "00001", ParamDETCHINGCIRCUITWIDTHLESS);
            cboDETCHINGCIRCUITWIDTHLESS.DataSource = dtDETCHINGCIRCUITWIDTHLESS;

            //D/Etching적용내층양면PIC모델
            cboDETCHINGPIC.DisplayMember = "CODENAME";
            cboDETCHINGPIC.ValueMember = "CODEID";
            cboDETCHINGPIC.ShowHeader = false;
            Dictionary<string, object> ParamDETCHINGPIC = new Dictionary<string, object>();
            ParamDETCHINGPIC.Add("CODECLASSID", "YesNo");
            ParamDETCHINGPIC.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtDETCHINGPIC = SqlExecuter.Query("GetCodeList", "00001", ParamDETCHINGPIC);
            cboDETCHINGPIC.DataSource = dtDETCHINGPIC;

            //D/Etching 적용 미적용
            cboDETCHINGNOAPPLY.DisplayMember = "CODENAME";
            cboDETCHINGNOAPPLY.ValueMember = "CODEID";
            cboDETCHINGNOAPPLY.ShowHeader = false;
            Dictionary<string, object> ParamDETCHINGNOAPPLY = new Dictionary<string, object>();
            ParamDETCHINGNOAPPLY.Add("CODECLASSID", "YesNo");
            ParamDETCHINGNOAPPLY.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtDETCHINGNOAPPLY = SqlExecuter.Query("GetCodeList", "00001", ParamDETCHINGNOAPPLY);
            cboDETCHINGNOAPPLY.DataSource = dtDETCHINGNOAPPLY;

            //특이사항클레스
            cboCamSpecialnoteClass.DisplayMember = "PARENTNOTECLASSNAME";
            cboCamSpecialnoteClass.ValueMember = "PARENTNOTECLASSID";
            cboCamSpecialnoteClass.ShowHeader = false;
            cboCamSpecialnoteClass.UseEmptyItem = true;
            Dictionary<string, object> ParamCamSpecialnoteClass = new Dictionary<string, object>();
            ParamCamSpecialnoteClass.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            ParamCamSpecialnoteClass.Add("PLANTID", UserInfo.Current.Plant);
            ParamCamSpecialnoteClass.Add("NOTECLASSTYPE", "CAMRequest");
            DataTable dtCamSpecialnoteClass = SqlExecuter.Query("GetCamSpecialnoteclassCombo", "10001", ParamCamSpecialnoteClass);
            cboCamSpecialnoteClass.DataSource = dtCamSpecialnoteClass;

            //특이사항변경클레스
            cboCamSpecialnoteClassChg.DisplayMember = "PARENTNOTECLASSNAME";
            cboCamSpecialnoteClassChg.ValueMember = "PARENTNOTECLASSID";
            cboCamSpecialnoteClassChg.ShowHeader = false;
            cboCamSpecialnoteClassChg.UseEmptyItem = true;
            Dictionary<string, object> ParamCamSpecialnoteClassChg = new Dictionary<string, object>();
            ParamCamSpecialnoteClassChg.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            ParamCamSpecialnoteClassChg.Add("PLANTID", UserInfo.Current.Plant);
            ParamCamSpecialnoteClassChg.Add("NOTECLASSTYPE", "CAMChange");
            DataTable dtCamSpecialnoteClassChg = SqlExecuter.Query("GetCamSpecialnoteclassCombo", "10001", ParamCamSpecialnoteClassChg);
            cboCamSpecialnoteClassChg.DataSource = dtCamSpecialnoteClassChg;

            



            // 제작처
            ConditionItemSelectPopup cisidvendorCode = new ConditionItemSelectPopup();
            cisidvendorCode.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            cisidvendorCode.SetPopupLayout("VENDORID", PopupButtonStyles.Ok_Cancel);
            cisidvendorCode.Id = "VENDORID";
            cisidvendorCode.LabelText = "VENDORID";
            cisidvendorCode.SearchQuery = new SqlQuery("GetVendorList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}");
            cisidvendorCode.IsMultiGrid = false;
            cisidvendorCode.DisplayFieldName = "VENDORNAME";
            cisidvendorCode.ValueFieldName = "VENDORID";
            cisidvendorCode.LanguageKey = "VENDORID";
            cisidvendorCode.Conditions.AddTextBox("VENDORID");
            cisidvendorCode.GridColumns.AddTextBoxColumn("VENDORID", 150);
            cisidvendorCode.GridColumns.AddTextBoxColumn("VENDORNAME", 200);

            sspMANUFACTURER.SelectPopupCondition = cisidvendorCode;
            //sspMANUFACTURER.TextChanged += SspCustomerId_TextChanged;

            // 입고처
            ConditionItemSelectPopup cisiRECEIPTDEPT = new ConditionItemSelectPopup();
            cisiRECEIPTDEPT.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            cisiRECEIPTDEPT.SetPopupLayout("VENDORID", PopupButtonStyles.Ok_Cancel);
            cisiRECEIPTDEPT.Id = "VENDORID";
            cisiRECEIPTDEPT.LabelText = "VENDORID";
            cisiRECEIPTDEPT.SearchQuery = new SqlQuery("GetVendorList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}");
            cisiRECEIPTDEPT.IsMultiGrid = false;
            cisiRECEIPTDEPT.DisplayFieldName = "VENDORNAME";
            cisiRECEIPTDEPT.ValueFieldName = "VENDORID";
            cisiRECEIPTDEPT.LanguageKey = "VENDORID";
            cisiRECEIPTDEPT.Conditions.AddTextBox("VENDORID");
            cisiRECEIPTDEPT.GridColumns.AddTextBoxColumn("VENDORID", 150);
            cisiRECEIPTDEPT.GridColumns.AddTextBoxColumn("VENDORNAME", 200);
            sspRECEIPTDEPT.SelectPopupCondition = cisiRECEIPTDEPT;

            //// 승인자
            //ConditionItemSelectPopup cisiAPPROVER = new ConditionItemSelectPopup();
            //cisiAPPROVER.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            //cisiAPPROVER.SetPopupLayout("APPROVER", PopupButtonStyles.Ok_Cancel);
            //cisiAPPROVER.Id = "APPROVER";
            //cisiAPPROVER.LabelText = "APPROVER";
            //cisiAPPROVER.SearchQuery = new SqlQuery("GetUserAreaPerson", "10001");
            //cisiAPPROVER.IsMultiGrid = false;
            //cisiAPPROVER.DisplayFieldName = "USERNAME";
            //cisiAPPROVER.ValueFieldName = "USERID";
            //cisiAPPROVER.LanguageKey = "APPROVER";
            //cisiAPPROVER.Conditions.AddTextBox("USERNAME");
            //cisiAPPROVER.GridColumns.AddTextBoxColumn("USERID", 150);
            //cisiAPPROVER.GridColumns.AddTextBoxColumn("USERNAME", 200);
            //sspAPPROVER.SelectPopupCondition = cisiAPPROVER;


            //////////////////////////////////////////////////////////////////////////////
            //// 변경
            //////////////////////////////////////////////////////////////////////////////
            // 중요도
            cboChgIMPORTANCE.DisplayMember = "CODENAME";
            cboChgIMPORTANCE.ValueMember = "CODEID";
            cboChgIMPORTANCE.ShowHeader = false;
            Dictionary<string, object> ParamIMPORTANCE = new Dictionary<string, object>();
            ParamIMPORTANCE.Add("CODECLASSID", "Importance");
            ParamIMPORTANCE.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtIMPORTANCE = SqlExecuter.Query("GetCodeList", "00001", ParamIMPORTANCE);
            cboChgIMPORTANCE.DataSource = dtIMPORTANCE;


            // 우선순위
            cboChgPRIORITY.DisplayMember = "CODENAME";
            cboChgPRIORITY.ValueMember = "CODEID";
            cboChgPRIORITY.ShowHeader = false;
            Dictionary<string, object> ParamPRIORITY = new Dictionary<string, object>();
            ParamPRIORITY.Add("CODECLASSID", "Priority");
            ParamPRIORITY.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtPRIORITY = SqlExecuter.Query("GetCodeList", "00001", ParamPRIORITY);
            cboChgPRIORITY.DataSource = dtPRIORITY;

            // 고객승인
            cboChgCUSTOMERAPPRVAL.DisplayMember = "CODENAME";
            cboChgCUSTOMERAPPRVAL.ValueMember = "CODEID";
            cboChgCUSTOMERAPPRVAL.ShowHeader = false;
            Dictionary<string, object> ParamCUSTOMERAPPRVAL = new Dictionary<string, object>();
            ParamCUSTOMERAPPRVAL.Add("CODECLASSID", "CustomerApprval");
            ParamCUSTOMERAPPRVAL.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtCUSTOMERAPPRVAL = SqlExecuter.Query("GetCodeList", "00001", ParamCUSTOMERAPPRVAL);
            cboChgCUSTOMERAPPRVAL.DataSource = dtCUSTOMERAPPRVAL;

            // Test보고서
            cboChgTESTREPORT.DisplayMember = "CODENAME";
            cboChgTESTREPORT.ValueMember = "CODEID";
            cboChgTESTREPORT.ShowHeader = false;
            Dictionary<string, object> ParamTESTREPORT = new Dictionary<string, object>();
            ParamTESTREPORT.Add("CODECLASSID", "TestReport");
            ParamTESTREPORT.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtTESTREPORT = SqlExecuter.Query("GetCodeList", "00001", ParamTESTREPORT);
            cboChgTESTREPORT.DataSource = dtTESTREPORT;

            // 적용시점
            cboChgAPPLPOINT.DisplayMember = "CODENAME";
            cboChgAPPLPOINT.ValueMember = "CODEID";
            cboChgAPPLPOINT.ShowHeader = false;
            Dictionary<string, object> ParamAPPLPOINT = new Dictionary<string, object>();
            ParamAPPLPOINT.Add("CODECLASSID", "ApplPoint");
            ParamAPPLPOINT.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtAPPLPOINT = SqlExecuter.Query("GetCodeList", "00001", ParamAPPLPOINT);
            cboChgAPPLPOINT.DataSource = dtAPPLPOINT;

            // 예상문제
            cboChgRISKISSUE.DisplayMember = "CODENAME";
            cboChgRISKISSUE.ValueMember = "CODEID";
            cboChgRISKISSUE.ShowHeader = false;
            Dictionary<string, object> ParamRISKISSUE = new Dictionary<string, object>();
            ParamRISKISSUE.Add("CODECLASSID", "RiskIssue");
            ParamRISKISSUE.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtRISKISSUE = SqlExecuter.Query("GetCodeList", "00001", ParamRISKISSUE);
            cboChgRISKISSUE.DataSource = dtRISKISSUE;


        }

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGridIdDefinitionManagement()
        {
            grdCamRequest.GridButtonItem = GridButtonItem.All;   // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기

            grdCamRequest.View.AddTextBoxColumn("CAMREQUESTID");
            grdCamRequest.View.AddTextBoxColumn("ENTERPRISEID");
            grdCamRequest.View.AddTextBoxColumn("PLANTID");
            grdCamRequest.View.AddTextBoxColumn("GOVERNANCENO");
            grdCamRequest.View.AddTextBoxColumn("CAMTYPE");
            grdCamRequest.View.AddTextBoxColumn("DEPARTMENT");
            grdCamRequest.View.AddTextBoxColumn("PRODUCTDEFID");
            grdCamRequest.View.AddTextBoxColumn("PRODUCTDEFVERSION");
            grdCamRequest.View.AddTextBoxColumn("MODELNO");
            grdCamRequest.View.AddTextBoxColumn("SPECPERSON");
            grdCamRequest.View.AddTextBoxColumn("CAMPERSON");
            grdCamRequest.View.AddTextBoxColumn("PREVPRODUCTDEFID");
            grdCamRequest.View.AddTextBoxColumn("WORKDATE");
            grdCamRequest.View.AddTextBoxColumn("MODELDELIVERY");
            grdCamRequest.View.AddTextBoxColumn("WORKTYPE");
            grdCamRequest.View.AddTextBoxColumn("PRODUCTTYPE");
            grdCamRequest.View.AddTextBoxColumn("PANELSIZE");
            grdCamRequest.View.AddTextBoxColumn("MODELTYPE");
            grdCamRequest.View.AddTextBoxColumn("REASON");
            grdCamRequest.View.AddTextBoxColumn("IMPORTANCE");
            grdCamRequest.View.AddTextBoxColumn("PRIORITY");
            grdCamRequest.View.AddTextBoxColumn("APPROVALTYPE");
            grdCamRequest.View.AddTextBoxColumn("APPROVALID");
            grdCamRequest.View.AddTextBoxColumn("STATUS");
            grdCamRequest.View.AddTextBoxColumn("REQUESTER");
            grdCamRequest.View.AddTextBoxColumn("APPROVER");
            grdCamRequest.View.AddTextBoxColumn("REQUESTDATE");
            grdCamRequest.View.AddTextBoxColumn("APPROVEDATE");
            grdCamRequest.View.AddTextBoxColumn("PROCESSTYPE");
            grdCamRequest.View.AddTextBoxColumn("CUSTOMERNAME");
            grdCamRequest.View.AddTextBoxColumn("PREVCUSTOMERITEMID");
            grdCamRequest.View.AddTextBoxColumn("PREVCUSTOMERITEMVERSION");
            grdCamRequest.View.AddTextBoxColumn("CUSTOMERITEMNAME");
            grdCamRequest.View.AddTextBoxColumn("CUSTOMERSPEC");
            grdCamRequest.View.AddTextBoxColumn("PRUCHASINGDATE");
            grdCamRequest.View.AddTextBoxColumn("MANUFACTURER");
            grdCamRequest.View.AddTextBoxColumn("RECEIPTDEPT");
            grdCamRequest.View.AddTextBoxColumn("ELONGATION");
            grdCamRequest.View.AddTextBoxColumn("INCREASEDPRODUCTION");
            grdCamRequest.View.AddTextBoxColumn("GAPINNERLAYERCIRCUIT");
            grdCamRequest.View.AddTextBoxColumn("WIDEINNERLAYERCIRCUIT");
            grdCamRequest.View.AddTextBoxColumn("GAPOUTERLAYERCIRCUIT");
            grdCamRequest.View.AddTextBoxColumn("WIDEOUTERLAYERCIRCUIT");
            grdCamRequest.View.AddTextBoxColumn("INNERLAYERCORRECTION");
            grdCamRequest.View.AddTextBoxColumn("OUTERLAYERCORRECTION");
            grdCamRequest.View.AddTextBoxColumn("OBLCORRECTION");
            grdCamRequest.View.AddTextBoxColumn("BGACIRCUITCORRECTION");
            grdCamRequest.View.AddTextBoxColumn("BGAPSRCORRECTION");
            grdCamRequest.View.AddTextBoxColumn("APPLYINGCOPPERFOIL");
            grdCamRequest.View.AddTextBoxColumn("USERLAYER");
            grdCamRequest.View.AddTextBoxColumn("APPLIEDLAYERUNIT");
            grdCamRequest.View.AddTextBoxColumn("ZIPTERMINAL");
            grdCamRequest.View.AddTextBoxColumn("PADCORRECTION");
            grdCamRequest.View.AddTextBoxColumn("BBTSTANDARD");
            grdCamRequest.View.AddTextBoxColumn("JIGPRODUCTION");
            grdCamRequest.View.AddTextBoxColumn("TERMINALTYPE");
            grdCamRequest.View.AddTextBoxColumn("BBTMETHOD");
            grdCamRequest.View.AddTextBoxColumn("MICROSHORTREQUEST");
            grdCamRequest.View.AddTextBoxColumn("BBTMANUFACTURINGSTANDARDS");
            grdCamRequest.View.AddTextBoxColumn("MICROSHORTSTANDARD");
            grdCamRequest.View.AddTextBoxColumn("PLATINGTYPE");
            grdCamRequest.View.AddTextBoxColumn("PLATINGSEQUENCE");
            grdCamRequest.View.AddTextBoxColumn("MKNOTATION");
            grdCamRequest.View.AddTextBoxColumn("GUIDESTANDARD");
            grdCamRequest.View.AddTextBoxColumn("PLATINGCONNECTINGLAYER");
            grdCamRequest.View.AddTextBoxColumn("OUTERGUIDE");
            grdCamRequest.View.AddTextBoxColumn("VISIONPRESS");
            grdCamRequest.View.AddTextBoxColumn("TRIMMINGMARK");
            grdCamRequest.View.AddTextBoxColumn("CCOPPERFOIL");
            grdCamRequest.View.AddTextBoxColumn("SCOPPERFOIL");
            grdCamRequest.View.AddTextBoxColumn("COPPERFOIL2L");
            grdCamRequest.View.AddTextBoxColumn("COPPERFOIL3L");
            grdCamRequest.View.AddTextBoxColumn("COPPERFOIL4L");
            grdCamRequest.View.AddTextBoxColumn("COPPERFOIL5L");
            grdCamRequest.View.AddTextBoxColumn("COPPERFOIL6L");
            grdCamRequest.View.AddTextBoxColumn("COPPERFOIL7L");
            grdCamRequest.View.AddTextBoxColumn("COPPERFOIL8L");
            grdCamRequest.View.AddTextBoxColumn("COPPERFOIL9L");
            grdCamRequest.View.AddTextBoxColumn("DRYTYPE");
            grdCamRequest.View.AddTextBoxColumn("CONFORMAL");
            grdCamRequest.View.AddTextBoxColumn("BPSIZE");
            grdCamRequest.View.AddTextBoxColumn("PSR");
            grdCamRequest.View.AddTextBoxColumn("CL");
            grdCamRequest.View.AddTextBoxColumn("FLOORCOMPOSITION");
            grdCamRequest.View.AddTextBoxColumn("DETCHINGINNERLAYER");
            grdCamRequest.View.AddTextBoxColumn("DETCHINGOUTERLAYER");
            grdCamRequest.View.AddTextBoxColumn("DETCHINGDOUBLESIDEDPLATING");
            grdCamRequest.View.AddTextBoxColumn("DETCHINGDOUBLESIDEDPLATINGREMOVABLE");
            grdCamRequest.View.AddTextBoxColumn("DETCHINGCIRCUITWIDTHLESS");
            grdCamRequest.View.AddTextBoxColumn("DETCHINGPIC");
            grdCamRequest.View.AddTextBoxColumn("DETCHINGNOAPPLY");
            grdCamRequest.View.AddTextBoxColumn("SPECWEEK");
            grdCamRequest.View.AddTextBoxColumn("CAMWEEK");
            grdCamRequest.View.AddTextBoxColumn("PTHDRLSIZE");
            grdCamRequest.View.AddTextBoxColumn("NPTHDRLSIZE");
            grdCamRequest.View.AddTextBoxColumn("PTHSET");
            grdCamRequest.View.AddTextBoxColumn("NPTHSET");
            grdCamRequest.View.AddTextBoxColumn("REQUESTPROCESS");
            grdCamRequest.View.AddTextBoxColumn("DESCRIPTION");
            grdCamRequest.View.AddTextBoxColumn("VALIDSTATE");
            grdCamRequest.View.AddTextBoxColumn("DELIVERYDATE");
            grdCamRequest.View.PopulateColumns();

            grdCamChang.GridButtonItem = GridButtonItem.All;   // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기
            grdCamChang.View.AddTextBoxColumn("CAMREQUESTID");
            grdCamChang.View.AddTextBoxColumn("ENTERPRISEID");
            grdCamChang.View.AddTextBoxColumn("PLANTID");
            grdCamChang.View.AddTextBoxColumn("NOTECLASSTYPE");
            grdCamChang.View.AddTextBoxColumn("GOVERNANCENO");
            grdCamChang.View.AddTextBoxColumn("DEPARTMENT");
            grdCamChang.View.AddTextBoxColumn("WORKDATE");
            grdCamChang.View.AddTextBoxColumn("REASON");
            grdCamChang.View.AddTextBoxColumn("SPECPERSON");
            grdCamChang.View.AddTextBoxColumn("REQUESTDATE");
            grdCamChang.View.AddTextBoxColumn("REQUESTDEPT");
            grdCamChang.View.AddTextBoxColumn("REQUESTUSER");
            grdCamChang.View.AddTextBoxColumn("IMPLEMENTATIONDATE");
            grdCamChang.View.AddTextBoxColumn("APPOPERATION");
            grdCamChang.View.AddTextBoxColumn("APPLPOINT");
            grdCamChang.View.AddTextBoxColumn("TESTREPORT");
            grdCamChang.View.AddTextBoxColumn("RISKISSUE");
            grdCamChang.View.AddTextBoxColumn("CUSTOMERAPPRVAL");
            grdCamChang.View.AddTextBoxColumn("CONFERENCEORG");
            grdCamChang.View.AddTextBoxColumn("CONFERENCEPERSON");
            grdCamChang.View.AddTextBoxColumn("IMPORTANCE");
            grdCamChang.View.AddTextBoxColumn("PRIORITY");
            grdCamChang.View.AddTextBoxColumn("APPROVALTYPE");
            grdCamChang.View.AddTextBoxColumn("APPROVALID");
            grdCamChang.View.AddTextBoxColumn("CAMPERSON");
            grdCamChang.View.AddTextBoxColumn("STATUS");
            grdCamChang.View.AddTextBoxColumn("APPROVER");
            grdCamChang.View.AddTextBoxColumn("APPROVEDDATE");
            grdCamChang.View.AddTextBoxColumn("PREPRODUCTDEFID");
            grdCamChang.View.AddTextBoxColumn("MODELDELIVERY");
            grdCamChang.View.AddTextBoxColumn("PROGRESSCLASS");
            grdCamChang.View.AddTextBoxColumn("PRODUCTDEFID");
            grdCamChang.View.AddTextBoxColumn("PRODUCTDEFVERSION");
            grdCamChang.View.AddTextBoxColumn("MODELNO");
            grdCamChang.View.AddTextBoxColumn("DESCRIPTION");
            grdCamChang.View.AddTextBoxColumn("VALIDSTATE");
            grdCamChang.View.PopulateColumns();

            // 특기사항그룹
            grdCamSpecIalnoteClass.GridButtonItem = GridButtonItem.All;
            //grdCamSpecIalnoteClass.View.AddTextBoxColumn("NOTECLASSTYPE").SetIsHidden();
            grdCamSpecIalnoteClass.View.AddComboBoxColumn("NOTECLASSTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=NoteClassType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
            .SetIsHidden()
            ;
            grdCamSpecIalnoteClass.View.AddTextBoxColumn("ENTERPRISEID").SetIsHidden();
            grdCamSpecIalnoteClass.View.AddTextBoxColumn("PARENTNOTECLASSID", 100);
            grdCamSpecIalnoteClass.View.AddTextBoxColumn("NOTECLASSNAME", 100);
            grdCamSpecIalnoteClass.View.AddTextBoxColumn("NOTECLASSID", 100).SetIsHidden();
            grdCamSpecIalnoteClass.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdCamSpecIalnoteClass.View.AddTextBoxColumn("PLANTID").SetIsHidden();
            
            grdCamSpecIalnoteClass.View.AddTextBoxColumn("NOTETYPE").SetIsHidden();
            grdCamSpecIalnoteClass.View.AddTextBoxColumn("DESCRIPTION").SetIsHidden();
            grdCamSpecIalnoteClass.View.PopulateColumns();


            // 특기사항
            grdCamSpecialnote.GridButtonItem = GridButtonItem.All;
            grdCamSpecialnote.View.AddTextBoxColumn("CAMREQUESTID").SetIsHidden();
            grdCamSpecialnote.View.AddTextBoxColumn("ENTERPRISEID").SetIsHidden();
            grdCamSpecialnote.View.AddTextBoxColumn("PLANTID").SetIsHidden();
            grdCamSpecialnote.View.AddComboBoxColumn("PARENTNOTECLASSID", 100, new SqlQuery("GetCamSpecialnoteclassCombo", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"NOTECLASSTYPE={"CAMRequest"}"), "PARENTNOTECLASSNAME", "PARENTNOTECLASSID");
            grdCamSpecialnote.View.AddComboBoxColumn("NOTECLASSID", 100, new SqlQuery("GetCamSpecialnoteclassList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"NOTECLASSTYPE={"CAMRequest"}"), "NOTECLASSNAME", "NOTECLASSID")
            .SetRelationIds("PARENTNOTECLASSID");
            grdCamSpecialnote.View.AddTextBoxColumn("SEQUENCE").SetIsHidden();
            grdCamSpecialnote.View.AddTextBoxColumn("PROGRESSPERSON").SetIsHidden();
            grdCamSpecialnote.View.AddTextBoxColumn("CAMPERSON").SetIsHidden();
            grdCamSpecialnote.View.AddTextBoxColumn("DESCRIPTION", 250);
            grdCamSpecialnote.View.AddTextBoxColumn("STATUS", 100);
            grdCamSpecialnote.View.AddSpinEditColumn("XAXIS", 100);
            grdCamSpecialnote.View.AddSpinEditColumn("ST", 100);
            grdCamSpecialnote.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdCamSpecialnote.View.PopulateColumns();


            // 특기사항변경
            grdCamSpecialnoteChg.GridButtonItem = GridButtonItem.All;
            grdCamSpecialnoteChg.View.AddTextBoxColumn("CAMREQUESTID").SetIsHidden();
            grdCamSpecialnoteChg.View.AddTextBoxColumn("ENTERPRISEID").SetIsHidden();
            grdCamSpecialnoteChg.View.AddTextBoxColumn("PLANTID").SetIsHidden();
            grdCamSpecialnoteChg.View.AddComboBoxColumn("PARENTNOTECLASSID", 100, new SqlQuery("GetCamSpecialnoteclassCombo", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"NOTECLASSTYPE={"CAMChange"}"), "PARENTNOTECLASSNAME", "PARENTNOTECLASSID");

            grdCamSpecialnoteChg.View.AddComboBoxColumn("NOTECLASSID", 100, new SqlQuery("GetCamSpecialnoteclassList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"NOTECLASSTYPE={"CAMChange"}"), "NOTECLASSNAME", "NOTECLASSID")
            .SetRelationIds("PARENTNOTECLASSID");
            grdCamSpecialnoteChg.View.AddTextBoxColumn("SEQUENCE").SetIsHidden();
            grdCamSpecialnoteChg.View.AddTextBoxColumn("PROGRESSPERSON").SetIsHidden();
            grdCamSpecialnoteChg.View.AddTextBoxColumn("CAMPERSON").SetIsHidden();
            grdCamSpecialnoteChg.View.AddTextBoxColumn("DESCRIPTION", 250);
   
            grdCamSpecialnoteChg.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdCamSpecialnoteChg.View.PopulateColumns();

            


        }

        protected override void InitializeContent()
        {
            base.InitializeContent();
            InitializeControl();
            InitializeGridIdDefinitionManagement();

            if (this._parameters != null)
            {
                fnSearch();
            }

        }

        #endregion

        #region 툴바
        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

          


            switch (tabIdManagement.SelectedTabPageIndex)
            {
                case 0://의뢰

                    DataTable changed = new DataTable();
                    DataTable dtidclassserial = new DataTable();

                    GetControlsFrom confrom = new GetControlsFrom();
                    confrom.GetControlsFromGrid(smartSplitTableLayoutPanel4, grdCamRequest);
                    changed = grdCamRequest.GetChangedRows();

                    DataSet ds = new DataSet();
                    changed.TableName = "camRequest";
                    dtidclassserial.TableName = "idclassserial";

                    ds.Tables.Add(changed);
                    ds.Tables.Add(dtidclassserial);

                    ExecuteRule("CamRequest", ds);
                    

                    DataTable changedCamSpecialnote = grdCamSpecialnote.GetChangedRows();
                    ExecuteRule("CamSpecialnote", changedCamSpecialnote);

                    break;
                case 1://변경
                    //DataTable dtPackageProductHis = await SqlExecuter.QueryAsync("GetPackageProductHisList", "10001", values);

                    //if (dtPackageProductHis.Rows.Count < 1) // 
                    //{
                    //    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                    //}

                    //grdPackageProductHis.DataSource = dtPackageProductHis;

                    break;
                case 2://특이사항
                       // 특이사항등록
                    DataTable changedCamSpecIalnoteClass = grdCamSpecIalnoteClass.GetChangedRows();
                    ExecuteRule("CamSpecialnoteclass", changedCamSpecIalnoteClass);

                    break;


            }


            

        }
        #endregion

        #region 검색

        /// <summary>
        /// 검색조건 팝업 예제
        /// </summary>
        private void InitializeCondition_Popup()
        {
            //팝업 컬럼 설정
            var parentPopupColumn = Conditions.AddSelectPopup("CAMREQUESTID", new SqlQuery("GetCamrequestPupop", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"))
               .SetPopupLayout("CAMREQUESTID", PopupButtonStyles.Ok_Cancel, true, false)  //팝업창 UI 설정.
               .SetPopupResultCount(1)  //팝업창 선택가능한 개수
               //.SetRelationIds("APPROVALTYPE")
               ;

          


            parentPopupColumn.Conditions.AddDateEdit("CREATEDTIMEFR")
                .SetDisplayFormat("yyyy-MM-dd", MaskTypes.DateTime)
                .SetTextAlignment(TextAlignment.Center)
                .SetDefault(DateTime.Now.ToString("yyyy-MM-dd"))
                ;
            parentPopupColumn.Conditions.AddDateEdit("CREATEDTIMETO")
                .SetDisplayFormat("yyyy-MM-dd", MaskTypes.DateTime)
                .SetTextAlignment(TextAlignment.Center)
                .SetDefault(DateTime.Now.ToString("yyyy-MM-dd"))
                ;
            parentPopupColumn.Conditions.AddTextBox("TXTAPPROVER");

            parentPopupColumn.Conditions.AddComboBox("STATUS", new SqlQuery("GetCodeList", "00001", $"CODECLASSID=ApprovalStatus", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetEmptyItem();

            //팝업 그리드
            parentPopupColumn.GridColumns.AddTextBoxColumn("CAMREQUESTID", 150);
            parentPopupColumn.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            parentPopupColumn.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 100);
            parentPopupColumn.GridColumns.AddTextBoxColumn("CREATEDTIME", 100);

            parentPopupColumn.GridColumns.AddTextBoxColumn("SPECPERSON", 200);
            parentPopupColumn.GridColumns.AddTextBoxColumn("SPECPERSONNAME", 200);


        }


        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            fnSearch();

        }
        private void fnSearch()
        {
            // 그리드 초기화
            grdCamRequest.DataSource = null;
            // 조회조건에 입력된 값들을 Dictionary<string, object> Type으로 가져옴
            var values = this.Conditions.GetValues();
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            //TODO : Id를 수정하세요            

            SetControlsFrom scf = new SetControlsFrom();

            DataTable dtCamSpecialnote = new DataTable();
            switch (tabIdManagement.SelectedTabPageIndex)
            {
                case 0://의뢰

                    DataTable dtCamRequest =  SqlExecuter.Query("GetCamrequestList", "10001", values);
                    grdCamRequest.DataSource = dtCamRequest;
                    if (dtCamRequest.Rows.Count < 1) // 
                    {
                        ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                    }

                    dtCamSpecialnote =  SqlExecuter.Query("GetCamSpecialnoteList", "10001", values);
                    grdCamSpecialnote.DataSource = dtCamSpecialnote;
                    scf.SetControlsFromTable(smartSplitTableLayoutPanel4, dtCamRequest);

                    if(txtCAMREQUESTID.Text != "")
                    {
                        smartGroupBox1.Enabled = true;
                    }
                    else
                    {
                        smartGroupBox1.Enabled = false;
                    }
                    
                    break;
                case 1://변경

                    DataTable dtCamChange =  SqlExecuter.Query("GetCamChangeList", "10001", values);
                    grdCamChang.DataSource = dtCamChange;
                    if (dtCamChange.Rows.Count < 1) // 
                    {
                        ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                    }


                    scf.SetControlsFromTable(smartSplitTableLayoutPanel5, dtCamChange);


                    dtCamSpecialnote = SqlExecuter.Query("GetCamSpecialnoteList", "10001", values);
                    grdCamSpecialnoteChg.DataSource = dtCamSpecialnote;



                    break;
                case 2://특이사항
                    values.Add("PLANTID", UserInfo.Current.Plant);
                    values.Add("NOTECLASSTYPE", "CAMRequest");
                    DataTable dtCamSpecIalnoteClass =  SqlExecuter.Query("GetCamSpecialnoteclassList", "10001", values);

                    if (dtCamSpecIalnoteClass.Rows.Count < 1) // 
                    {
                        ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                    }

                    grdCamSpecIalnoteClass.DataSource = dtCamSpecIalnoteClass;

                    break;


            }
        }
        #endregion

            ////#region 유효성 검사
            /////// <summary>
            /////// 데이터 저장할때 컨텐츠 영역의 유효성 검사
            /////// </summary>
            ////protected override void OnValidateContent()
            ////{
            ////    base.OnValidateContent();

            ////    DataTable changed = new DataTable();
            ////    switch (tabIdManagement.SelectedTabPageIndex)
            ////    {
            ////        case 0://ID Class
            ////            GetControlsFrom gcf = new GetControlsFrom();
            ////            gcf.GetControlsFromGrid(smartSplitTableLayoutPanel4, grdPackageProduct);


            ////            //grdPackageProduct.View.CheckValidation();

            ////            changed = grdPackageProduct.GetChangedRows();

            ////            // 1CARD/1TARY
            ////            if (decimal.Parse(txtCardTary.Text) == 0 && decimal.Parse(txtCase.Text) == 0)
            ////            {
            ////                throw MessageException.Create("CardTaryCase");
            ////            }

            ////            // 포장분류
            ////            if (txtPackageclass.Text == "")
            ////            {
            ////                throw MessageException.Create("Packageclass");
            ////            }


            ////            //object obj = grdMDCList.DataSource;
            ////            //DataTable dt = (DataTable)obj;
            ////            //string sMessage = "";
            ////            //foreach (DataRow row in dt.DefaultView.ToTable(true, new string[] { "MASTERDATACLASSID" }).Rows)
            ////            //{
            ////            //    int count = dt.Select("MASTERDATACLASSID = '" + row["MASTERDATACLASSID"].ToString() + "'").Length;
            ////            //    //int i = int.Parse(dt.Compute("COUNT(MASTERDATACLASSID)", "MASTERDATACLASSID = '" + row["MASTERDATACLASSID"].ToString() + "'").ToString());
            ////            //    if (count > 1)
            ////            //    {
            ////            //        sMessage = sMessage + "품목유형 코드 " + row["MASTERDATACLASSID"].ToString() + "은" + count.ToString() + " 개가 중복입니다." + "\r\n";
            ////            //        //sfilter = "MASTERDATACLASSID = '" + row["MASTERDATACLASSID"].ToString() + "'";
            ////            //    }
            ////            //}
            ////            //if (sMessage != "")
            ////            //{
            ////            //    throw MessageException.Create(sMessage);
            ////            //}

            ////            break;
            ////        case 1://ID Definition


            ////            //object obj1 = grdAAGList.DataSource;
            ////            //DataTable dt1 = (DataTable)obj1;
            ////            //string sMessage1 = "";



            ////            //foreach (DataRow row in dt1.Rows)
            ////            //{
            ////            //    int count = dt1.Select("MASTERDATACLASSID = '" + row["MASTERDATACLASSID"].ToString() + "' AND ATTRIBUTEGROUPID = '" + row["ATTRIBUTEGROUPID"].ToString() + "'").Length;
            ////            //    //int i = int.Parse(dt.Compute("COUNT(MASTERDATACLASSID)", "MASTERDATACLASSID = '" + row["MASTERDATACLASSID"].ToString() + "'").ToString());
            ////            //    if (count > 1)
            ////            //    {
            ////            //        sMessage1 = row["MASTERDATACLASSID"].ToString() + "/" + row["ATTRIBUTEGROUPID"].ToString();
            ////            //        //sfilter = "MASTERDATACLASSID = '" + row["MASTERDATACLASSID"].ToString() + "'";
            ////            //    }
            ////            //}
            ////            //if (sMessage1 != "")
            ////            //{
            ////            //    throw MessageException.Create("InValidData007", sMessage1);
            ////            //}


            ////            break;
            ////        case 2://ID Definition
            ////            //grdIdDefinitionList.View.CheckValidation();
            ////            //changed = grdIdDefinitionList.GetChangedRows();
            ////            break;
            ////    }
            ////    if (changed.Rows.Count == 0)
            ////    {
            ////        // 저장할 데이터가 존재하지 않습니다.
            ////        throw MessageException.Create("NoSaveData");
            ////    }
            ////}
            ////#endregion

            #region 이벤트
            private void InitializeEvent()
        {

            tabIdManagement.Click += TabIdManagement_Click;
            //grdPackageProduct.View.AddingNewRow += grdPackageProduct_AddingNewRow;
            //txtPackQty.TextChanged += TxtCalculus_TextChanged;
            //txtCaseQty.TextChanged += TxtCalculus_TextChanged;
            //txtBoxQty.TextChanged += TxtCalculus_TextChanged;
            //btnCopy.Click += BtnCopy_Click;

            grdCamSpecIalnoteClass.View.AddingNewRow += grdCamSpecIalnoteClass_AddingNewRow;
            grdCamSpecialnote.View.AddingNewRow += grdCamSpecialnote_AddingNewRow;
            //grdCamRequest.View.FocusedRowChanged += grdCamRequest_FocusedRowChanged;

               

        }

        private void grdCamRequest_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grdCamRequest.View.FocusedRowHandle < 0)
                return;

            // 그리드 초기화
            DataTable dtGovernanceSiteClear = (DataTable)grdCamSpecialnote.DataSource;
            dtGovernanceSiteClear.Clear();
            //DataTable dtGovernanceProductClear = (DataTable)grdGovernanceProduct.DataSource;
            //dtGovernanceProductClear.Clear();


            DataRow dataRow = grdCamRequest.View.GetFocusedDataRow();
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("GOVERNANCENO", dataRow["GOVERNANCENO"].ToString());

            // 사이트 
            DataTable dtCamSpecialnote = SqlExecuter.Query("GetCamSpecialnoteList", "10001", param);
            grdCamSpecialnote.DataSource = dtCamSpecialnote;
        }

        private void grdCamSpecialnote_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            if (grdCamRequest.View.FocusedRowHandle < 0)
            {
                args.IsCancel = true;
            }

            if(cboCamSpecialnoteClass.GetDataValue().ToString() == "")
            {
                args.IsCancel = true;
                ShowMessage("CamSpecialnoteClass");
            }
            


            DataRow rowCamRequest = grdCamRequest.View.GetFocusedDataRow();

            args.NewRow["CAMREQUESTID"] = rowCamRequest["CAMREQUESTID"];
            args.NewRow["ENTERPRISEID"] = rowCamRequest["ENTERPRISEID"];
            args.NewRow["PLANTID"] = rowCamRequest["PLANTID"];
            args.NewRow["PARENTNOTECLASSID"] = cboCamSpecialnoteClass.GetDataValue();
            args.NewRow["VALIDSTATE"] = "Valid";

            DataTable dt = (DataTable)grdCamSpecialnote.DataSource;
            if (dt != null)
            {
                if (dt.Rows.Count != 0)
                {
                    if (dt.Select("1=1", "SEQUENCE DESC")[0]["SEQUENCE"].ToString() != "")
                    {
                        args.NewRow["SEQUENCE"] = decimal.Parse(dt.Select("1=1", "SEQUENCE DESC")[0]["SEQUENCE"].ToString()) + 1;
                    }
                    else
                    {
                        args.NewRow["SEQUENCE"] = 1;
                      
                    }
                }
                else
                {
                    args.NewRow["SEQUENCE"] = 1;
                
                }
            }
            else
            {
                args.NewRow["SEQUENCE"] = 1;
               
            }

        }

        private void grdCamSpecIalnoteClass_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            args.NewRow["NOTECLASSTYPE"] = "CAMRequest";
            args.NewRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
            args.NewRow["PLANTID"] = UserInfo.Current.Plant;
            args.NewRow["VALIDSTATE"] = "Valid";

            // 오늘날짜.
            Dictionary<string, object> paramdt = new Dictionary<string, object>();
            DataTable dtDate = SqlExecuter.Query("GetItemId", "10001", paramdt);
            string sdate = dtDate.Rows[0]["ITEMID"].ToString().Substring(0, 8);

            GetNumber number = new GetNumber();
            args.NewRow["NOTECLASSID"] = number.GetStdNumber("NoteClassId", "NS" + sdate);

         

        }

        private void BtnCopy_Click(object sender, EventArgs e)
        {
            Micube.SmartMES.StandardInfo.Popup.PackageProductCopyPopup ppcp = new Popup.PackageProductCopyPopup();
            ppcp.ShowDialog();

            if (ppcp.dtChk != null)
            {
                if (ppcp.dtChk.Rows.Count != 0)
                {
                    DataTable dtCopy = ppcp.dtChk;
                    dtCopy.Columns.Remove("CUSTOMERID");
                    dtCopy.Columns.Remove("CUSTOMERNAME");
                    dtCopy.Columns.Remove("PRODUCTDEFID");
                    dtCopy.Columns.Remove("PRODUCTDEFVERSION");
                    dtCopy.Columns.Remove("PRODUCTDEFNAME");
                    dtCopy.Columns.Remove("ITEMID");

                }
            }
            SetControlsFrom scf = new SetControlsFrom();
            scf.SetControlsFromTable(smartSplitTableLayoutPanel4, ppcp.dtChk);

        }

        private void TxtCalculus_TextChanged(object sender, EventArgs e)
        {
            Calculus();
        }

        void Calculus()
        {
            ////txtCardTary.Text = (decimal.Parse(txtPackQty.Text) * decimal.Parse(txtCaseQty.Text) * decimal.Parse(txtBoxQty.Text)).ToString();
            ////txtCase.Text = (decimal.Parse(txtPackQty.Text) * decimal.Parse(txtCaseQty.Text)).ToString();
        }




        #region 그리드이벤트

        // 품목유형별 정의 + 툴버튼  그리드 추가 이벤트
        private void grdPackageProduct_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            ////var values = this.Conditions.GetValues();
            ////args.NewRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
            ////args.NewRow["PLANTID"] = values["P_PLANTID"].ToString();

            ////args.NewRow["VALIDSTATE"] = "Valid";
            ////args.NewRow["ISCONFIRMATION"] = "N";

            ////cboValidState.EditValue = "Valid";
            ////cboIsConfirmation.EditValue = "N";

        }




        #endregion

        #region 기타이벤트

        private void TabIdManagement_Click(object sender, EventArgs e)
        {
            ////var values = this.Conditions.GetValues();
            ////values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);

            //SmartComboBox combobox = Conditions.GetControl<SmartComboBox>("APPROVALTYPE");

            switch (tabIdManagement.SelectedTabPageIndex)
            {
                case 0:
                    //combobox.EditValue = "CAMRequest";

                    //특이사항클레스
                    cboCamSpecialnoteClass.DisplayMember = "PARENTNOTECLASSNAME";
                    cboCamSpecialnoteClass.ValueMember = "PARENTNOTECLASSID";
                    cboCamSpecialnoteClass.ShowHeader = false;
                    cboCamSpecialnoteClass.UseEmptyItem = true;
                    Dictionary<string, object> ParamCamSpecialnoteClass = new Dictionary<string, object>();
                    ParamCamSpecialnoteClass.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                    ParamCamSpecialnoteClass.Add("PLANTID", UserInfo.Current.Plant);
                    ParamCamSpecialnoteClass.Add("NOTECLASSTYPE", "CAMRequest");
                    DataTable dtCamSpecialnoteClass = SqlExecuter.Query("GetCamSpecialnoteclassCombo", "10001", ParamCamSpecialnoteClass);
                    cboCamSpecialnoteClass.DataSource = dtCamSpecialnoteClass;


                    break;
                case 1:
                    // 등록된 마스트 클래스 데이터 조회
                    //combobox.EditValue = "CAMChange";
                    break;
                default:
                    break;

            }



        }

       



        #endregion

        #endregion



        ////#region Private Function

        ////// TODO : 화면에서 사용할 내부 함수 추가

        ////#endregion
    }
}
