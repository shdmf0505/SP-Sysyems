#region using
using DevExpress.XtraEditors;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Micube.SmartMES.StandardInfo.Popup;
#endregion

namespace Micube.SmartMES.StandardInfo
{

    /// <summary>
    /// 프 로 그 램 명  : 기준정보 > 품목관리 > 품목등록(사양)
    /// 업  무  설  명  : 품목스펙 등록
    /// 생    성    자  :  윤성원
    /// 생    성    일  : 2019-06-28
    /// 수  정  이  력  : 
    /// </summary>
    public partial class ProductItemSpecPopup : XtraForm 
    {
        #region Local Variables
        string _sITEMID = "";
        
        
        string _sITEMVERSION = "";
        string _sMASTERDATACLASSID = "";
        string _sENTERPRISEID = "";
        #endregion

        #region 생성자
        public ProductItemSpecPopup()
        {
            InitializeComponent();
            InitializeGridIdDefinitionManagement();
            InitializeEvent();
        }
        
        public ProductItemSpecPopup(string sITEMID , string sITEMVERSION, string sMASTERDATACLASSID, string sENTERPRISEID, string sIMPLEMENTATIONDATE)
        {
            InitializeComponent();

            _sITEMID            = sITEMID;
            
            
            _sITEMVERSION       = sITEMVERSION;
            _sMASTERDATACLASSID = sMASTERDATACLASSID;
            _sENTERPRISEID      = sENTERPRISEID;

            if(sIMPLEMENTATIONDATE != "")
            {
                tableLayoutPanel1.Enabled = false;
            }

            InitializeGridIdDefinitionManagement();
            InitializeEvent();
        }
        #endregion

        #region 초기화

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGridIdDefinitionManagement()
        {
            // GRID 초기화
            grdProductItemSpec.GridButtonItem = GridButtonItem.None;   // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기
            grdProductItemSpec.View.AddTextBoxColumn("ITEMID", 200);
            grdProductItemSpec.View.AddTextBoxColumn("ITEMVERSION", 200);
            grdProductItemSpec.View.AddTextBoxColumn("MASTERDATACLASSID", 200);
            grdProductItemSpec.View.AddTextBoxColumn("ENTERPRISEID", 200);
            grdProductItemSpec.View.AddTextBoxColumn("PLANTID", 200);
            grdProductItemSpec.View.AddTextBoxColumn("PRODUCTTYPE", 200);
            grdProductItemSpec.View.AddTextBoxColumn("CUSTOMERID", 80);
            grdProductItemSpec.View.AddTextBoxColumn("CUSTOMERNAME", 150);
            grdProductItemSpec.View.AddTextBoxColumn("CUSTOMERITEMID", 200);
            grdProductItemSpec.View.AddTextBoxColumn("CUSTOMERITEMVERSION", 200);
            grdProductItemSpec.View.AddTextBoxColumn("CUSTOMERITEMNAME", 200);
            grdProductItemSpec.View.AddTextBoxColumn("CUSTOMERSPEC", 200);
            grdProductItemSpec.View.AddTextBoxColumn("HSCODE", 200);
            grdProductItemSpec.View.AddTextBoxColumn("LAYER", 200);
            grdProductItemSpec.View.AddTextBoxColumn("USELAYER", 200);
            grdProductItemSpec.View.AddTextBoxColumn("COPPERTYPE", 200);
            grdProductItemSpec.View.AddTextBoxColumn("PACKINGQTY", 200);
            grdProductItemSpec.View.AddTextBoxColumn("PROJECTNAME", 200);
            grdProductItemSpec.View.AddTextBoxColumn("ENDUSER", 200);
            grdProductItemSpec.View.AddTextBoxColumn("COPPERPLATINGTYPE", 200);
            grdProductItemSpec.View.AddTextBoxColumn("PRODUCTTHICKNESS", 200);
            grdProductItemSpec.View.AddTextBoxColumn("UL_MARK", 200);
            grdProductItemSpec.View.AddTextBoxColumn("MANUFACTUREDDATE", 200);
            grdProductItemSpec.View.AddTextBoxColumn("HG_FR", 200);
            grdProductItemSpec.View.AddTextBoxColumn("ASSY", 200);
            grdProductItemSpec.View.AddTextBoxColumn("OXIDE", 200);
            grdProductItemSpec.View.AddTextBoxColumn("SEPARATINGPORTION", 200);
            grdProductItemSpec.View.AddTextBoxColumn("RTRSHEET", 200);
            grdProductItemSpec.View.AddTextBoxColumn("IMPEDANCE", 200);
            grdProductItemSpec.View.AddTextBoxColumn("INPUTTYPE", 200);
            grdProductItemSpec.View.AddTextBoxColumn("PCSSIZEXAXIS", 200);
            grdProductItemSpec.View.AddTextBoxColumn("PCSSIZEYAXIS", 200);
            grdProductItemSpec.View.AddTextBoxColumn("ARYSIZEXAXIS", 200);
            grdProductItemSpec.View.AddTextBoxColumn("ARYSIZEYAXIS", 200);
            grdProductItemSpec.View.AddTextBoxColumn("PNLSIZEXAXIS", 200);
            grdProductItemSpec.View.AddTextBoxColumn("PNLSIZEYAXIS", 200);
            grdProductItemSpec.View.AddTextBoxColumn("PCSPNL", 200);
            grdProductItemSpec.View.AddTextBoxColumn("PNLMM", 200);
            grdProductItemSpec.View.AddTextBoxColumn("PCSMM", 200);
            grdProductItemSpec.View.AddTextBoxColumn("INPUTSIZEXAXIS", 200);

            //grdProductItemSpec.View.AddTextBoxColumn("SURFACETREATMENT", 200);
            grdProductItemSpec.View.AddTextBoxColumn("HOLEPLATINGAREA", 200);

            grdProductItemSpec.View.AddTextBoxColumn("INNERLAYER", 200);
            grdProductItemSpec.View.AddTextBoxColumn("OUTERLAYER", 200);
            grdProductItemSpec.View.AddTextBoxColumn("INNERLAYERCIRCUIT", 200);
            grdProductItemSpec.View.AddTextBoxColumn("OUTERLAYERCIRCUIT", 200);
            grdProductItemSpec.View.AddTextBoxColumn("COPPERFOILUPLAYER", 200);
            grdProductItemSpec.View.AddTextBoxColumn("COPPERFOILDOWNLAYER", 200);
            grdProductItemSpec.View.AddTextBoxColumn("INNERLAYERTO", 200);
            grdProductItemSpec.View.AddTextBoxColumn("OUTERLAYERTO", 200);
            grdProductItemSpec.View.AddTextBoxColumn("JOBTYPE", 200);
            grdProductItemSpec.View.AddTextBoxColumn("PRODUCTIONTYPE", 200);

            grdProductItemSpec.View.AddTextBoxColumn("DUMMY", 200);
            //grdProductItemSpec.View.AddTextBoxColumn("BASEMATERIAL", 200);
            grdProductItemSpec.View.AddTextBoxColumn("INNERCIRCUITDISTANCE", 200);

            //grdProductItemSpec.View.AddTextBoxColumn("CLMATERIAL", 200);
            grdProductItemSpec.View.AddTextBoxColumn("OUTERCIRCUITDISTANCE", 200);

            //grdProductItemSpec.View.AddTextBoxColumn("SUBSIDIARY", 200);
            grdProductItemSpec.View.AddTextBoxColumn("INNERCIRCUITCOPPER", 200);

            //grdProductItemSpec.View.AddTextBoxColumn("TOLERANCE", 200);
            grdProductItemSpec.View.AddTextBoxColumn("OUTERCIRCUITCOPPER", 200);


            grdProductItemSpec.View.AddTextBoxColumn("INPUTSCALE", 200);
            grdProductItemSpec.View.AddTextBoxColumn("RELIABILITY", 200);
            grdProductItemSpec.View.AddTextBoxColumn("HAZARDOUSSUBSTANCES", 200);
            grdProductItemSpec.View.AddTextBoxColumn("MEASUREMENT", 200);
            grdProductItemSpec.View.AddTextBoxColumn("INKSPECIFICATION", 200);
            grdProductItemSpec.View.AddTextBoxColumn("OLBCIRCUIT", 200);
            grdProductItemSpec.View.AddTextBoxColumn("ELONGATION", 200);
            grdProductItemSpec.View.AddTextBoxColumn("PITCHBEFORE", 200);
            grdProductItemSpec.View.AddTextBoxColumn("PITCHAFTER", 200);
            //grdProductItemSpec.View.AddTextBoxColumn("CORRECTIONVALUES", 200);
            grdProductItemSpec.View.AddTextBoxColumn("PCSIMAGEID", 200);

            grdProductItemSpec.View.AddTextBoxColumn("SMD", 200);
            grdProductItemSpec.View.AddTextBoxColumn("SALESMAN", 200);
            grdProductItemSpec.View.AddTextBoxColumn("SPECIFICATIONMAN", 200);
            grdProductItemSpec.View.AddTextBoxColumn("CAMMAN", 200);
            grdProductItemSpec.View.AddTextBoxColumn("DESCRIPTION", 200);
            grdProductItemSpec.View.AddTextBoxColumn("AREASIZE", 200);
            

            grdProductItemSpec.View.AddTextBoxColumn("ISWEEKMNG", 200);
            grdProductItemSpec.View.AddTextBoxColumn("PCSARY", 200);
            grdProductItemSpec.View.AddTextBoxColumn("XOUT", 200);
            

            grdProductItemSpec.View.PopulateColumns();



            // 제품Type
            cboProductType.DisplayMember = "CODENAME";
            cboProductType.ValueMember = "CODEID";
            cboProductType.ShowHeader = false;
            Dictionary<string, object> ParamRt = new Dictionary<string, object>();
            ParamRt.Add("CODECLASSID", "ProductType");
            ParamRt.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtIdClasstype = SqlExecuter.Query("GetCodeList", "00001", ParamRt);
            cboProductType.DataSource = dtIdClasstype;

            // 동도금타입
            cboCopperPlatingType.DisplayMember = "CODENAME";
            cboCopperPlatingType.ValueMember = "CODEID";
            cboCopperPlatingType.ShowHeader = false;
            Dictionary<string, object> ParamCPT = new Dictionary<string, object>();
            ParamCPT.Add("CODECLASSID", "CopperPlatingType");
            ParamCPT.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtCTP = SqlExecuter.Query("GetCodeList", "00001", ParamCPT);
            cboCopperPlatingType.DataSource = dtCTP;

            //HS코드
            cboHSCode.DisplayMember = "CODENAME";
            cboHSCode.ValueMember = "CODEID";
            cboHSCode.ShowHeader = false;
            Dictionary<string, object> ParamHSC = new Dictionary<string, object>();
            ParamHSC.Add("CODECLASSID", "HSCode");
            ParamHSC.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtHSC = SqlExecuter.Query("GetCodeList", "00001", ParamHSC);
            cboHSCode.DataSource = dtHSC;

            //층수
            cboLayer.DisplayMember = "CODENAME";
            cboLayer.ValueMember = "CODEID";
            cboLayer.ShowHeader = false;
            Dictionary<string, object> ParamLy = new Dictionary<string, object>();
            ParamLy.Add("CODECLASSID", "HSCode");
            ParamLy.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtLy = SqlExecuter.Query("GetCodeList", "00001", ParamLy);
            cboLayer.DataSource = dtLy;

            //층수
            cboUserLayer.DisplayMember = "CODENAME";
            cboUserLayer.ValueMember = "CODEID";
            cboUserLayer.ShowHeader = false;
            Dictionary<string, object> ParamUy = new Dictionary<string, object>();
            ParamUy.Add("CODECLASSID", "UserLayer");
            ParamUy.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtUy = SqlExecuter.Query("GetCodeList", "00001", ParamUy);
            cboUserLayer.DataSource = dtUy;

            //UL-MARK
            cboulmark.DisplayMember = "CODENAME";
            cboulmark.ValueMember = "CODEID";
            cboulmark.ShowHeader = false;
            Dictionary<string, object> ParamUm = new Dictionary<string, object>();
            ParamUm.Add("CODECLASSID", "UL-MARK");
            ParamUm.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtUm = SqlExecuter.Query("GetCodeList", "00001", ParamUm);
            cboulmark.DataSource = dtUm;

            //Hg-Fr
            cboHgFr.DisplayMember = "CODENAME";
            cboHgFr.ValueMember = "CODEID";
            cboHgFr.ShowHeader = false;
            Dictionary<string, object> ParamHgFr = new Dictionary<string, object>();
            ParamHgFr.Add("CODECLASSID", "Hg-Fr");
            ParamHgFr.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtHgFr = SqlExecuter.Query("GetCodeList", "00001", ParamHgFr);
            cboHgFr.DataSource = dtHgFr;

            //ASSY
            cboASSY.DisplayMember = "CODENAME";
            cboASSY.ValueMember = "CODEID";
            cboASSY.ShowHeader = false;
            Dictionary<string, object> ParamASSY = new Dictionary<string, object>();
            ParamASSY.Add("CODECLASSID", "ASSY");
            ParamASSY.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtASSY = SqlExecuter.Query("GetCodeList", "00001", ParamASSY);
            cboASSY.DataSource = dtASSY;

            //OXIDE
            cboOXIDE.DisplayMember = "CODENAME";
            cboOXIDE.ValueMember = "CODEID";
            cboOXIDE.ShowHeader = false;
            Dictionary<string, object> ParamOXIDE = new Dictionary<string, object>();
            ParamOXIDE.Add("CODECLASSID", "OXIDE");
            ParamOXIDE.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtOXIDE = SqlExecuter.Query("GetCodeList", "00001", ParamOXIDE);
            cboOXIDE.DataSource = dtOXIDE;

            //분리부
            cboSeparatingPortion.DisplayMember = "CODENAME";
            cboSeparatingPortion.ValueMember = "CODEID";
            cboSeparatingPortion.ShowHeader = false;
            Dictionary<string, object> ParamSeparatingPortion = new Dictionary<string, object>();
            ParamSeparatingPortion.Add("CODECLASSID", "SeparatingPortion");
            ParamSeparatingPortion.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtSeparatingPortion = SqlExecuter.Query("GetCodeList", "00001", ParamSeparatingPortion);
            cboSeparatingPortion.DataSource = dtSeparatingPortion;

            //RTR/SHEET
            cboRTRSheet.DisplayMember = "CODENAME";
            cboRTRSheet.ValueMember = "CODEID";
            cboRTRSheet.ShowHeader = false;
            Dictionary<string, object> ParamRTRSheet = new Dictionary<string, object>();
            ParamRTRSheet.Add("CODECLASSID", "RTRSHT");
            ParamRTRSheet.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtRTRSheet = SqlExecuter.Query("GetCodeList", "00001", ParamRTRSheet);
            cboRTRSheet.DataSource = dtRTRSheet;

            //임피던스
            cboImpedance.DisplayMember = "CODENAME";
            cboImpedance.ValueMember = "CODEID";
            cboImpedance.ShowHeader = false;
            Dictionary<string, object> ParamImpedance = new Dictionary<string, object>();
            ParamImpedance.Add("CODECLASSID", "Impedance");
            ParamImpedance.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtImpedance = SqlExecuter.Query("GetCodeList", "00001", ParamImpedance);
            cboImpedance.DataSource = dtImpedance;

            //투입유형
            cboInputType.DisplayMember = "CODENAME";
            cboInputType.ValueMember = "CODEID";
            cboInputType.ShowHeader = false;
            Dictionary<string, object> ParamInputType = new Dictionary<string, object>();
            ParamInputType.Add("CODECLASSID", "InputType");
            ParamInputType.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtInputType = SqlExecuter.Query("GetCodeList", "00001", ParamInputType);
            cboInputType.DataSource = dtInputType;

            ////표면처리
            //cboSurfaceTreatment.DisplayMember = "CODENAME";
            //cboSurfaceTreatment.ValueMember = "CODEID";
            //cboSurfaceTreatment.ShowHeader = false;
            //Dictionary<string, object> ParamSurfaceTreatment = new Dictionary<string, object>();
            //ParamSurfaceTreatment.Add("CODECLASSID", "SurfaceTreatment");
            //ParamSurfaceTreatment.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            //DataTable dtSurfaceTreatment = SqlExecuter.Query("GetCodeList", "00001", ParamSurfaceTreatment);
            //cboSurfaceTreatment.DataSource = dtSurfaceTreatment;

            //내층동
            cboInnerLayer.DisplayMember = "CODENAME";
            cboInnerLayer.ValueMember = "CODEID";
            cboInnerLayer.ShowHeader = false;
            Dictionary<string, object> ParamInnerLayer = new Dictionary<string, object>();
            ParamInnerLayer.Add("CODECLASSID", "InnerLayer");
            ParamInnerLayer.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtInnerLayer = SqlExecuter.Query("GetCodeList", "00001", ParamInnerLayer);
            cboInnerLayer.DataSource = dtInnerLayer;

            //외층동
            cboOuterLayer.DisplayMember = "CODENAME";
            cboOuterLayer.ValueMember = "CODEID";
            cboOuterLayer.ShowHeader = false;
            Dictionary<string, object> ParamOuterLayer = new Dictionary<string, object>();
            ParamOuterLayer.Add("CODECLASSID", "OuterLayer");
            ParamOuterLayer.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtOuterLayer = SqlExecuter.Query("GetCodeList", "00001", ParamOuterLayer);
            cboOuterLayer.DataSource = dtOuterLayer;

            ////내층 WET 공정 투입방향
            //cboInnerLayerTo.DisplayMember = "CODENAME";
            //cboInnerLayerTo.ValueMember = "CODEID";
            //cboInnerLayerTo.ShowHeader = false;
            //Dictionary<string, object> ParamInnerLayerWET = new Dictionary<string, object>();
            //ParamInnerLayerWET.Add("CODECLASSID", "InnerLayerWET");
            //ParamInnerLayerWET.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            //DataTable dtInnerLayerWET = SqlExecuter.Query("GetCodeList", "00001", ParamInnerLayerWET);
            //cboInnerLayerTo.DataSource = dtInnerLayerWET;

            ////외층 WET 공정 투입방향
            //cboOuterLayerWET.DisplayMember = "CODENAME";
            //cboOuterLayerWET.ValueMember = "CODEID";
            //cboOuterLayerWET.ShowHeader = false;
            //Dictionary<string, object> ParamOuterLayerWET = new Dictionary<string, object>();
            //ParamOuterLayerWET.Add("CODECLASSID", "OuterLayerWET");
            //ParamOuterLayerWET.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            //DataTable dtOuterLayerWET = SqlExecuter.Query("GetCodeList", "00001", ParamOuterLayerWET);
            //cboOuterLayerWET.DataSource = dtOuterLayerWET;

            //작업구분
            cboJobType.DisplayMember = "CODENAME";
            cboJobType.ValueMember = "CODEID";
            cboJobType.ShowHeader = false;
            Dictionary<string, object> ParamJobType = new Dictionary<string, object>();
            ParamJobType.Add("CODECLASSID", "JobType");
            ParamJobType.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtJobType = SqlExecuter.Query("GetCodeList", "00001", ParamJobType);
            cboJobType.DataSource = dtJobType;

            //생산구분
            cboProductionType.DisplayMember = "CODENAME";
            cboProductionType.ValueMember = "CODEID";
            cboProductionType.ShowHeader = false;
            Dictionary<string, object> ParamProductionType = new Dictionary<string, object>();
            ParamProductionType.Add("CODECLASSID", "ProductionType");
            ParamProductionType.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtProductionType = SqlExecuter.Query("GetCodeList", "00001", ParamProductionType);
            cboProductionType.DataSource = dtProductionType;

            //투입SCALE
            cboInputSCALE.DisplayMember = "CODENAME";
            cboInputSCALE.ValueMember = "CODEID";
            cboInputSCALE.ShowHeader = false;
            Dictionary<string, object> ParamInputSCALE = new Dictionary<string, object>();
            ParamInputSCALE.Add("CODECLASSID", "InputSCALE");
            ParamInputSCALE.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtInputSCALE = SqlExecuter.Query("GetCodeList", "00001", ParamInputSCALE);
            cboInputSCALE.DataSource = dtInputSCALE;

            //신뢰성
            cboReliability.DisplayMember = "CODENAME";
            cboReliability.ValueMember = "CODEID";
            cboReliability.ShowHeader = false;
            Dictionary<string, object> ParamReliability = new Dictionary<string, object>();
            ParamReliability.Add("CODECLASSID", "Reliability");
            ParamReliability.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtReliability = SqlExecuter.Query("GetCodeList", "00001", ParamReliability);
            cboReliability.DataSource = dtReliability;

            //유해물질
            cboHazardousSubstances.DisplayMember = "CODENAME";
            cboHazardousSubstances.ValueMember = "CODEID";
            cboHazardousSubstances.ShowHeader = false;
            Dictionary<string, object> ParamHazardousSubstances = new Dictionary<string, object>();
            ParamHazardousSubstances.Add("CODECLASSID", "HazardousSubstances");
            ParamHazardousSubstances.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtHazardousSubstances = SqlExecuter.Query("GetCodeList", "00001", ParamHazardousSubstances);
            cboHazardousSubstances.DataSource = dtHazardousSubstances;

            //치수측정
            cboMeasurement.DisplayMember = "CODENAME";
            cboMeasurement.ValueMember = "CODEID";
            cboMeasurement.ShowHeader = false;
            Dictionary<string, object> ParamMeasurement = new Dictionary<string, object>();
            ParamMeasurement.Add("CODECLASSID", "Measurement");
            ParamMeasurement.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtMeasurement = SqlExecuter.Query("GetCodeList", "00001", ParamMeasurement);
            cboMeasurement.DataSource = dtMeasurement;

            //OLB회로
            cboOLBCircuit.DisplayMember = "CODENAME";
            cboOLBCircuit.ValueMember = "CODEID";
            cboOLBCircuit.ShowHeader = false;
            Dictionary<string, object> ParamOLBCircuit = new Dictionary<string, object>();
            ParamOLBCircuit.Add("CODECLASSID", "OLBCircuit");
            ParamOLBCircuit.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtOLBCircuit = SqlExecuter.Query("GetCodeList", "00001", ParamOLBCircuit);
            cboOLBCircuit.DataSource = dtOLBCircuit;

            // 거래처팝업
            ConditionItemSelectPopup cisidvendorCode = new ConditionItemSelectPopup();
            cisidvendorCode.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            cisidvendorCode.SetPopupLayout("CUSTOMERID", PopupButtonStyles.Ok_Cancel);
            
            cisidvendorCode.Id = "CUSTOMERID";
            cisidvendorCode.LabelText = "CUSTOMERID";
            cisidvendorCode.SearchQuery = new SqlQuery("GetCustomerList", "10002", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}");
            cisidvendorCode.IsMultiGrid = false;
            cisidvendorCode.DisplayFieldName = "CUSTOMERNAME";
            cisidvendorCode.ValueFieldName = "CUSTOMERID";
            cisidvendorCode.LanguageKey = "CUSTOMERID";
            
            cisidvendorCode.Conditions.AddTextBox("TXTCUSTOMERID");
            cisidvendorCode.GridColumns.AddTextBoxColumn("CUSTOMERID", 80);
            cisidvendorCode.GridColumns.AddTextBoxColumn("CUSTOMERNAME", 150);
            cisidvendorCode.GridColumns.AddTextBoxColumn("ADDRESS", 250);
            cisidvendorCode.GridColumns.AddTextBoxColumn("CEONAME", 100);
            cisidvendorCode.GridColumns.AddTextBoxColumn("TELNO", 100);
            cisidvendorCode.GridColumns.AddTextBoxColumn("FAXNO", 100);
            sspCustomerId.SelectPopupCondition = cisidvendorCode;
            sspCustomerId.TextChanged += SspCustomerId_TextChanged;

            //CAM담당
            ConditionItemSelectPopup cisiCAMMAN = new ConditionItemSelectPopup();
            cisiCAMMAN.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            cisiCAMMAN.SetPopupLayout("CAMMAN", PopupButtonStyles.Ok_Cancel);
            cisiCAMMAN.Id = "CAMMAN";
            cisiCAMMAN.LabelText = "CAMMAN";
            cisiCAMMAN.SearchQuery = new SqlQuery("GetUserAreaPerson", "10001");
            cisiCAMMAN.IsMultiGrid = false;
            cisiCAMMAN.DisplayFieldName = "USERNAME";
            cisiCAMMAN.ValueFieldName = "USERID";
            cisiCAMMAN.LanguageKey = "CAMMAN";
            cisiCAMMAN.Conditions.AddTextBox("USERNAME");
            cisiCAMMAN.GridColumns.AddTextBoxColumn("USERID", 150);
            cisiCAMMAN.GridColumns.AddTextBoxColumn("USERNAME", 200);
            sspCAMMAN.SelectPopupCondition = cisiCAMMAN;

            //영업담당
            ConditionItemSelectPopup cisiSALESMAN = new ConditionItemSelectPopup();
            cisiSALESMAN.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            cisiSALESMAN.SetPopupLayout("SALESMAN", PopupButtonStyles.Ok_Cancel);
            cisiSALESMAN.Id = "SALESMAN";
            cisiSALESMAN.LabelText = "SALESMAN";
            cisiSALESMAN.SearchQuery = new SqlQuery("GetUserAreaPerson", "10001");
            cisiSALESMAN.IsMultiGrid = false;
            cisiSALESMAN.DisplayFieldName = "USERNAME";
            cisiSALESMAN.ValueFieldName = "USERID";
            cisiSALESMAN.LanguageKey = "SALESMAN";
            cisiSALESMAN.Conditions.AddTextBox("USERNAME");
            cisiSALESMAN.GridColumns.AddTextBoxColumn("USERID", 150);
            cisiSALESMAN.GridColumns.AddTextBoxColumn("USERNAME", 200);
            sspSALESMAN.SelectPopupCondition = cisiSALESMAN;

            //사양담당
            ConditionItemSelectPopup cisiSPECIFICATIONMAN = new ConditionItemSelectPopup();
            cisiSPECIFICATIONMAN.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            cisiSPECIFICATIONMAN.SetPopupLayout("SPECIFICATIONMAN", PopupButtonStyles.Ok_Cancel);
            cisiSPECIFICATIONMAN.Id = "SPECIFICATIONMAN";
            cisiSPECIFICATIONMAN.LabelText = "SPECIFICATIONMAN";
            cisiSPECIFICATIONMAN.SearchQuery = new SqlQuery("GetUserAreaPerson", "10001");
            cisiSPECIFICATIONMAN.IsMultiGrid = false;
            cisiSPECIFICATIONMAN.DisplayFieldName = "USERNAME";
            cisiSPECIFICATIONMAN.ValueFieldName = "USERID";
            cisiSPECIFICATIONMAN.LanguageKey = "SPECIFICATIONMAN";
            cisiSPECIFICATIONMAN.Conditions.AddTextBox("USERNAME");
            cisiSPECIFICATIONMAN.GridColumns.AddTextBoxColumn("USERID", 150);
            cisiSPECIFICATIONMAN.GridColumns.AddTextBoxColumn("USERNAME", 200);
            sspSPECIFICATIONMAN.SelectPopupCondition = cisiSPECIFICATIONMAN;


            Dictionary<string, object> Param = new Dictionary<string, object>();
            Param.Add("ITEMID", _sITEMID);
            Param.Add("ITEMVERSION", _sITEMVERSION);
            //Param.Add("MASTERDATACLASSID", _sMASTERDATACLASSID);
            Param.Add("ENTERPRISEID", _sENTERPRISEID);

            DataTable dt = SqlExecuter.Query("GetProductitemspec", "10001", Param);
            if (dt.Rows.Count == 0)
            {
                grdProductItemSpec.View.AddNewRow();

                object objNew = this.grdProductItemSpec.DataSource;
                DataTable dtNew = (DataTable)objNew;

                dtNew.Rows[0]["ITEMID"] = _sITEMID;
                dtNew.Rows[0]["ITEMVERSION"] = _sITEMVERSION;
                //dtNew.Rows[0]["MASTERDATACLASSID"] = _sMASTERDATACLASSID;
                dtNew.Rows[0]["ENTERPRISEID"] = _sENTERPRISEID;
            }
            else
            {
                grdProductItemSpec.DataSource = dt;



                foreach (Control control in tableLayoutPanel1.Controls)
                {

                    if (control.ToString() == "DevExpress.XtraEditors.PanelControl")
                    {
                        foreach (Control controlc in control.Controls)
                        {
                            switch (controlc.ToString())
                            {

                                case "Micube.Framework.SmartControls.SmartSpinEdit":
                                case "Micube.Framework.SmartControls.SmartTextBox":
                                    if (dt.Columns.IndexOf(controlc.Tag.ToString()) != -1)
                                    {
                                        controlc.Text = dt.Rows[0][controlc.Tag.ToString()].ToString();
                                    }


                                    break;
                                case "Micube.Framework.SmartControls.SmartComboBox":

                                    Micube.Framework.SmartControls.SmartComboBox combox = new SmartComboBox();
                                    combox = (Micube.Framework.SmartControls.SmartComboBox)controlc;

                                    if (dt.Columns.IndexOf(controlc.Tag.ToString()) != -1)
                                    {
                                        combox.EditValue = dt.Rows[0][controlc.Tag.ToString()].ToString();
                                    }

                                    break;

                                case "Micube.Framework.SmartControls.SmartSelectPopupEdit":

                                    Micube.Framework.SmartControls.SmartSelectPopupEdit SelectPopup = new SmartSelectPopupEdit();

                                    SelectPopup = (Micube.Framework.SmartControls.SmartSelectPopupEdit)controlc;

                                    if (dt.Columns.IndexOf(controlc.Tag.ToString()) != -1)
                                    {

                                        SelectPopup.SetValue(dt.Rows[0][controlc.Tag.ToString()].ToString());
                                        if (controlc.Tag.ToString().IndexOf("ID") != -1)
                                        {
                                            SelectPopup.Text = dt.Rows[0][controlc.Tag.ToString().Replace("ID", "NAME")].ToString();
                                        }
                                        else
                                        {
                                            SelectPopup.Text = dt.Rows[0][controlc.Tag.ToString() + "NAME"].ToString();
                                        }
                                        //txtCustomerName.Text = dt.Rows[0]["CUSTOMERNAME"].ToString();
                                    }

                                    //Micube.SmartMES.StandardInfo.ucPublicPopup ucCp = new ucPublicPopup();
                                    //ucCp = (Micube.SmartMES.StandardInfo.ucPublicPopup)controlc;

                                    //if (dt.Columns.IndexOf(ucCp.CODE.Tag.ToString()) != -1)
                                    //{
                                    //    ucCp.CODE.Text = dt.Rows[0][ucCp.CODE.Tag.ToString()].ToString();
                                    //    ucCp.NAME.Text = dt.Rows[0][ucCp.NAME.Tag.ToString()].ToString();
                                    //}

                                    break;

                                case "Micube.Framework.SmartControls.SmartDateEdit":
                                    if (dt.Columns.IndexOf(controlc.Tag.ToString()) != -1)
                                    {
                                        controlc.Text = dt.Rows[0][controlc.Tag.ToString()].ToString();
                                    }
                                    break;
                            }
                        }
                    }
                }
                fnCaluation();
            }
        }

        private void SspCustomerId_TextChanged(object sender, EventArgs e)
        {
            txtCustomerName.Text = sspCustomerId.EditValue.ToString();
        }

      
        #endregion

        #region  이벤트
        private void InitializeEvent()
        {
          
            btnSave.Click += BtnSave_Click;
            btnYPE.Click += BtnYPE_Click;
            btnIFV.Click += BtnIFV_Click;
            btnIFC.Click += BtnIFC_Click;
           
            

            btnCancel.Click += BtnCancel_Click;

            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add("ENTERPRISEID", _sENTERPRISEID);
            DataTable dtResult = SqlExecuter.Query("GetCalculationRule", "10001", dicParam);

            // 품목규칙에 등록된 계산식 컬럼 TextChanged 이벤트 생성
            foreach (DataRow row in dtResult.Select("CODEID <> ''"))
            {
                foreach (Control control in tableLayoutPanel1.Controls)
                {
                    if (control.ToString() == "DevExpress.XtraEditors.PanelControl")
                    {
                        foreach (Control controlc in control.Controls)
                        {
                            switch (controlc.ToString())
                            {
                                case "Micube.Framework.SmartControls.SmartSpinEdit":
                                    
                                    if (row["CODEID"].ToString() == controlc.Tag.ToString())
                                    {
                                        controlc.TextChanged += TxtSizeAxis_TextChanged;
                                    
                                    }
                                    break;
                            }
                        }
                    }
                }
            }
        }

        

        private void TxtSizeAxis_TextChanged(object sender, EventArgs e)
        {
            fnCaluation();
        }

        public void fnCaluation()
        {
            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add("ENTERPRISEID", _sENTERPRISEID);
            DataTable dtResult = SqlExecuter.Query("GetCalculationRule", "10001", dicParam);

            foreach (DataRow row in dtResult.DefaultView.ToTable(true, new string[] { "TARGETATTRIBUTE" }).Rows)
            {
                String sAdd = "";

                foreach (DataRow rowAdd in dtResult.Select("TARGETATTRIBUTE = '" + row["TARGETATTRIBUTE"].ToString() + "'"))
                {
                    sAdd = sAdd + rowAdd["FRONTBRACKET"].ToString() + rowAdd["CODEID"].ToString() + rowAdd["OPERATOR"].ToString() + rowAdd["BACKBRACKET"].ToString();
                }

                foreach (Control control in tableLayoutPanel1.Controls)
                {

                    if (control.ToString() == "DevExpress.XtraEditors.PanelControl")
                    {
                        foreach (Control controlc in control.Controls)
                        {
                            switch (controlc.ToString())
                            {
                                case "Micube.Framework.SmartControls.SmartSpinEdit":
                                    sAdd = sAdd.Replace(controlc.Tag.ToString(), controlc.Text);
                                    break;

                            }
                        }
                    }
                }


                foreach (Control control in tableLayoutPanel1.Controls)
                {
                    if (control.ToString() == "DevExpress.XtraEditors.PanelControl")
                    {
                        foreach (Control controlc in control.Controls)
                        {
                            switch (controlc.ToString())
                            {
                                case "Micube.Framework.SmartControls.SmartSpinEdit":
                                    if (controlc.Tag.ToString() == row["TARGETATTRIBUTE"].ToString())
                                    {
                                        DataTable dtCalulation = new DataTable();
                                        controlc.Text = dtCalulation.Compute(sAdd, "").ToString();
                                    }

                                    break;
                            }
                        }
                    }
                }
            }
        }



        private void BtnIFC_Click(object sender, EventArgs e)
        {
            ProductItemIFCPopup ifcpopup = new ProductItemIFCPopup(_sMASTERDATACLASSID, _sITEMID, _sITEMVERSION, _sENTERPRISEID, "");
            ifcpopup.ShowDialog();

        }

        private void BtnIFV_Click(object sender, EventArgs e)
        {
            ProductItemIFVPopup ifvpopup = new ProductItemIFVPopup(_sMASTERDATACLASSID, _sITEMID, _sITEMVERSION, _sENTERPRISEID, "");
            ifvpopup.ShowDialog();

        }
        private void BtnYPE_Click(object sender, EventArgs e)
        {
            ProductItemYPEPopup ypepopup = new ProductItemYPEPopup(_sMASTERDATACLASSID, _sITEMID, _sITEMVERSION, _sENTERPRISEID, "");
            ypepopup.ShowDialog();
        }
        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {

                object obj = this.grdProductItemSpec.DataSource;
                DataTable dt = (DataTable)obj;

                //컨트롤 테그에 DB 컬럼을 등록 하고 컨트롤에 입력된 데이터를 테이블에 입력   
                foreach (Control control in tableLayoutPanel1.Controls)
                {

                    if (control.ToString() == "DevExpress.XtraEditors.PanelControl")
                    {
                        foreach (Control controlc in control.Controls)
                        {
                            switch (controlc.ToString())
                            {
                                case "Micube.Framework.SmartControls.SmartSpinEdit": // 숫자텍스트
                                case "Micube.Framework.SmartControls.SmartTextBox":  // 텍스트
                                    if (dt.Columns.IndexOf(controlc.Tag.ToString()) != -1)
                                    {
                                        dt.Rows[0][controlc.Tag.ToString()] = controlc.Text;
                                    }


                                    break;
                                case "Micube.Framework.SmartControls.SmartComboBox":  //콤보

                                    Micube.Framework.SmartControls.SmartComboBox combox = new SmartComboBox();
                                    combox = (Micube.Framework.SmartControls.SmartComboBox)controlc;

                                    if (dt.Columns.IndexOf(controlc.Tag.ToString()) != -1)
                                    {
                                        dt.Rows[0][controlc.Tag.ToString()] = combox.GetDataValue();
                                    }

                                    break;
                                    
                                case "Micube.Framework.SmartControls.SmartSelectPopupEdit": // 팝업
                                    Micube.Framework.SmartControls.SmartSelectPopupEdit SelectPopup = new SmartSelectPopupEdit();
                                    SelectPopup = (Micube.Framework.SmartControls.SmartSelectPopupEdit)controlc;

                                    if (dt.Columns.IndexOf(controlc.Tag.ToString()) != -1)
                                    {
                                        dt.Rows[0][controlc.Tag.ToString()] = SelectPopup.GetValue();
                                    }


                                    //Micube.SmartMES.StandardInfo.ucPublicPopup ucCp = new ucPublicPopup();
                                    //ucCp = (Micube.SmartMES.StandardInfo.ucPublicPopup)controlc;

                                    //if (dt.Columns.IndexOf(ucCp.CODE.Tag.ToString()) != -1)
                                    //{
                                    //    dt.Rows[0][ucCp.CODE.Tag.ToString()] = ucCp.CODE.Text;
                                    //}

                                    break;
                                case "Micube.SmartMES.StandardInfo.ucPcsImageid": // 사용자 정의 

                                    Micube.SmartMES.StandardInfo.ucPcsImageid ucPi = new ucPcsImageid();
                                    ucPi = (Micube.SmartMES.StandardInfo.ucPcsImageid)controlc;

                                    if (dt.Columns.IndexOf(ucPi.CODE.Tag.ToString()) != -1)
                                    {
                                        dt.Rows[0][ucPi.CODE.Tag.ToString()] = ucPi.CODE.Text;
                                    }

                                    break;

                                case "Micube.Framework.SmartControls.SmartDateEdit": // 날자
                                    if (dt.Columns.IndexOf(controlc.Tag.ToString()) != -1)
                                    {
                                        dt.Rows[0][controlc.Tag.ToString()] = controlc.Text;
                                    }
                                    break;
                            }
                        }
                    }
                }

                //<------------------------ 품목명 규칙 중복체크 시작 --------------------------->

                //<-- 시작 중복체크 타겟 품목명 DB 조회   
                Dictionary<string, object> dDrParam = new Dictionary<string, object>();
                dDrParam.Add("MASTERDATACLASSID", _sMASTERDATACLASSID);
                dDrParam.Add("ENTERPRISEID", _sENTERPRISEID);
                
                DataTable dtDr = SqlExecuter.Query("GetItemDuplicateRule", "10001", dDrParam);
                //<-- 종료 중복체크 타겟 품목명 DB 조회   

                String sAddChk = "";
                foreach (DataRow row in dtDr.DefaultView.ToTable(true, new string[] { "TARGETATTRIBUTE" }).Rows)
                {
                    
                    foreach (DataRow rowAdd in dtDr.Select("TARGETATTRIBUTE = '" + row["TARGETATTRIBUTE"].ToString() + "'"))
                    {
                        sAddChk = sAddChk + rowAdd["CODEID"].ToString() + rowAdd["SEPARATORCODE"].ToString();
                    }

                    foreach (Control control in tableLayoutPanel1.Controls)
                    {

                        if (control.ToString() == "DevExpress.XtraEditors.PanelControl")
                        {
                            foreach (Control controlc in control.Controls)
                            {
                                switch (controlc.ToString())
                                {
                                    case "Micube.Framework.SmartControls.SmartSpinEdit":
                                    case "Micube.Framework.SmartControls.SmartTextBox":
                                        if (dt.Columns.IndexOf(controlc.Tag.ToString()) != -1)
                                        {
                                                sAddChk = sAddChk.Replace(controlc.Tag.ToString(), controlc.Text);
                                        }


                                        break;
                                    case "Micube.Framework.SmartControls.SmartComboBox":

                                        Micube.Framework.SmartControls.SmartComboBox combox = new SmartComboBox();
                                        combox = (Micube.Framework.SmartControls.SmartComboBox)controlc;

                                        if (dt.Columns.IndexOf(controlc.Tag.ToString()) != -1)
                                        {
                                            if (combox.GetDataValue() != null)
                                            {
                                                sAddChk = sAddChk.Replace(controlc.Tag.ToString(), combox.GetDataValue().ToString());
                                            }
                                        }

                                        break;

                                    case "Micube.SmartMES.StandardInfo.ucCustomerPopup":

                                        //Micube.SmartMES.StandardInfo.ucPublicPopup ucCp = new ucPublicPopup();
                                        //ucCp = (Micube.SmartMES.StandardInfo.ucPublicPopup)controlc;

                                        //if (dt.Columns.IndexOf(ucCp.CODE.Tag.ToString()) != -1)
                                        //{
                                        //    sAddChk = sAddChk.Replace(ucCp.CODE.Tag.ToString(), controlc.Text);
                                        //}

                                        break;

                                    case "Micube.Framework.SmartControls.SmartDateEdit":
                                        if (dt.Columns.IndexOf(controlc.Tag.ToString()) != -1)
                                        {
                                            sAddChk = sAddChk.Replace(controlc.Tag.ToString(), controlc.Text);
                                        }
                                        break;
                                }
                            }
                        }
                    }
                }

                if(sAddChk != "")
                {
                    Dictionary<string, object> dDrChkParam = new Dictionary<string, object>();
                    dDrChkParam.Add("ENTERPRISEID", _sENTERPRISEID);
                    dDrChkParam.Add("ITEMNAME", sAddChk);
                    dDrChkParam.Add("ITEMID", _sITEMID);
                    dDrChkParam.Add("MASTERDATACLASSID", _sMASTERDATACLASSID);

                    //dicParam.Add("PLANTID", UserInfo.Current.Plant);
                    DataTable dtDrChk = SqlExecuter.Query("GetItemNameDuplicate", "10001", dDrChkParam);
                    if (dtDrChk.Rows.Count != 0)
                    {
                        MSGBox.Show(MessageBoxType.Information, "DuplicateItemName");
                    }
                }

                //<------------------------ 품목명 중복체크 끝 --------------------------->


                //<------------------------ 품목명 계산식 적용 시작 --------------------------->

                //<--  계산식 타겟 컬럼 및 계산 컬럼 DB 에서 가져오기 
                Dictionary<string, object> dicParam = new Dictionary<string, object>();
                dicParam.Add("MASTERDATACLASSID", _sMASTERDATACLASSID);
                dicParam.Add("ENTERPRISEID", _sENTERPRISEID);
                //dicParam.Add("PLANTID", UserInfo.Current.Plant);
                DataTable dtResult = SqlExecuter.Query("GetItemDescriptionRule", "10001", dicParam);

                DataTable dtItemMaster = new DataTable();
                dtItemMaster.Columns.Add("ITEMID");
                dtItemMaster.Columns.Add("ITEMVERSION");
                dtItemMaster.Columns.Add("MASTERDATACLASSID");
                dtItemMaster.Columns.Add("ENTERPRISEID");
                
                dtItemMaster.Columns.Add("_STATE_");

                foreach (DataRow row in dtResult.DefaultView.ToTable(true, new string[] { "TARGETATTRIBUTE" }).Rows)
                {
                    dtItemMaster.Columns.Add(row["TARGETATTRIBUTE"].ToString());

                    String sAdd = "";
                    DataRow rowItemMaster = dtItemMaster.NewRow();
                    rowItemMaster["ITEMID"] = _sITEMID;
                    rowItemMaster["ITEMVERSION"] = _sITEMVERSION;
                    rowItemMaster["MASTERDATACLASSID"] = _sMASTERDATACLASSID;
                    rowItemMaster["ENTERPRISEID"] = _sENTERPRISEID;
                    rowItemMaster["_STATE_"] = "modified";


                    foreach (DataRow rowAdd in dtResult.Select("TARGETATTRIBUTE = '" + row["TARGETATTRIBUTE"].ToString() + "'"))
                    {
                        sAdd = sAdd + rowAdd["CODEID"].ToString() + rowAdd["SEPARATORCODE"].ToString();
                    }
                    

                    foreach (Control control in tableLayoutPanel1.Controls)
                    {

                        if (control.ToString() == "DevExpress.XtraEditors.PanelControl")
                        {
                            foreach (Control controlc in control.Controls)
                            {
                                switch (controlc.ToString())
                                {
                                    case "Micube.Framework.SmartControls.SmartSpinEdit":
                                    case "Micube.Framework.SmartControls.SmartTextBox":
                                        if (dt.Columns.IndexOf(controlc.Tag.ToString()) != -1)
                                        {
                                                sAdd = sAdd.Replace(controlc.Tag.ToString(), controlc.Text);
                                        }


                                        break;
                                    case "Micube.Framework.SmartControls.SmartComboBox":

                                        Micube.Framework.SmartControls.SmartComboBox combox = new SmartComboBox();
                                        combox = (Micube.Framework.SmartControls.SmartComboBox)controlc;

                                        if (dt.Columns.IndexOf(controlc.Tag.ToString()) != -1)
                                        {
                                            if(combox.GetDataValue() != null)
                                            {
                                                sAdd = sAdd.Replace(controlc.Tag.ToString(), combox.GetDataValue().ToString());
                                            }
                                        }

                                        break;

                                    case "Micube.SmartMES.StandardInfo.ucPublicPopup":

                                        //Micube.SmartMES.StandardInfo.ucPublicPopup ucCp = new ucPublicPopup();
                                        //ucCp = (Micube.SmartMES.StandardInfo.ucPublicPopup)controlc;

                                        //if (dt.Columns.IndexOf(ucCp.CODE.Tag.ToString()) != -1)
                                        //{
                                        //    sAdd = sAdd.Replace(ucCp.CODE.Tag.ToString(), controlc.Text);
                                        //}

                                        break;

                                    case "Micube.Framework.SmartControls.SmartDateEdit":
                                        if (dt.Columns.IndexOf(controlc.Tag.ToString()) != -1)
                                        {
                                            sAdd = sAdd.Replace(controlc.Tag.ToString(), controlc.Text);
                                        }
                                        break;
                                }
                            }
                        }
                    }

                    rowItemMaster[row["TARGETATTRIBUTE"].ToString()] = sAdd;

                    dtItemMaster.Rows.Add(rowItemMaster);
                }

                //<------------------------ 품목명 계산식 적용 끝 --------------------------->



                // 제품 정보
                DataTable dtproductdefinition = new DataTable();
                dtproductdefinition.Columns.Add("PRODUCTDEFID");
                dtproductdefinition.Columns.Add("PRODUCTDEFVERSION");
                dtproductdefinition.Columns.Add("PRODUCTSHAPE");
                dtproductdefinition.Columns.Add("OWNER");
                dtproductdefinition.Columns.Add("CUSTOMERID");
                dtproductdefinition.Columns.Add("LAYER");
                dtproductdefinition.Columns.Add("ISWEEKMNG");
                dtproductdefinition.Columns.Add("RTRSHT");
                dtproductdefinition.Columns.Add("INPUTTYPE");
                dtproductdefinition.Columns.Add("PCSSIZEXAXIS");
                dtproductdefinition.Columns.Add("PCSSIZEYAXIS");
                dtproductdefinition.Columns.Add("ARYSIZEXAXIS");
                dtproductdefinition.Columns.Add("ARYSIZEYAXIS");
                dtproductdefinition.Columns.Add("PNLSIZEXAXIS");
                dtproductdefinition.Columns.Add("PNLSIZEYAXIS");
                dtproductdefinition.Columns.Add("PCSPNL");
                dtproductdefinition.Columns.Add("PNLMM");
                dtproductdefinition.Columns.Add("PCSMM");
                dtproductdefinition.Columns.Add("PCSARY");
                dtproductdefinition.Columns.Add("XOUT");
                dtproductdefinition.Columns.Add("_STATE_");


                DataTable changed = new DataTable();

                grdProductItemSpec.View.CheckValidation();
                changed = grdProductItemSpec.GetChangedRows();


                foreach (DataRow row in changed.Rows)
                {
                    if (row.RowState == DataRowState.Modified)
                    {
                        DataRow rowNew = dtproductdefinition.NewRow();
                        rowNew["PRODUCTDEFID"] = row["ITEMID"];
                        rowNew["PRODUCTDEFVERSION"] = row["ITEMVERSION"];
                        rowNew["PRODUCTSHAPE"] = row["PRODUCTTYPE"];
                        rowNew["OWNER"] = row["SPECIFICATIONMAN"];
                        rowNew["CUSTOMERID"] = row["CUSTOMERID"];
                        rowNew["LAYER"] = row["LAYER"];
                        rowNew["ISWEEKMNG"] = row["ISWEEKMNG"];
                        rowNew["RTRSHT"] = row["RTRSHT"];
                        rowNew["INPUTTYPE"] = row["INPUTTYPE"];
                        rowNew["PCSSIZEXAXIS"] = row["PCSSIZEXAXIS"];
                        rowNew["PCSSIZEYAXIS"] = row["PCSSIZEYAXIS"];
                        rowNew["ARYSIZEXAXIS"] = row["ARYSIZEXAXIS"];
                        rowNew["ARYSIZEYAXIS"] = row["ARYSIZEYAXIS"];
                        rowNew["PNLSIZEXAXIS"] = row["PNLSIZEXAXIS"];
                        rowNew["PNLSIZEYAXIS"] = row["PNLSIZEYAXIS"];
                        rowNew["PCSPNL"] = row["PCSPNL"];
                        rowNew["PNLMM"] = row["PNLMM"];
                        rowNew["PCSMM"] = row["PCSMM"];
                        rowNew["PCSARY"] = row["PCSARY"];
                        rowNew["XOUT"] = row["XOUT"];
                        rowNew["_STATE_"] = "modified";
                        dtproductdefinition.Rows.Add(rowNew);
                    }
                }


                dtproductdefinition.TableName = "productdefinition";
                


                if (changed.Rows.Count == 0)
                {
                    // 저장할 데이터가 존재하지 않습니다.
                    throw MessageException.Create("NoSaveData");
                }

                MessageWorker saveWorker = new MessageWorker("ProductItemSpec");
                saveWorker.SetBody(new MessageBody()
                {
                    { "productItemSpec", changed }
                    ,{ "itemMaster", dtItemMaster }
                    ,{ "productdefinition", dtproductdefinition }
                    
                });

                saveWorker.Execute();

                MSGBox.Show(MessageBoxType.Information, "SuccedSave");

                dt.AcceptChanges();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        #endregion
        
    }
}
