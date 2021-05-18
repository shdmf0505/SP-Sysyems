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
    public partial class ProductItemSpecPopupVer2 : XtraForm 
    {
        #region Local Variables
        string _sITEMID = "";
        
        
        string _sITEMVERSION = "";
        string _sMASTERDATACLASSID = "";
        string _sENTERPRISEID = "";
        #endregion

        #region 생성자
        public ProductItemSpecPopupVer2()
        {
            InitializeComponent();
            InitializeGridIdDefinitionManagement();
            InitializeEvent();
        }
        
        public ProductItemSpecPopupVer2(string sITEMID , string sITEMVERSION, string sMASTERDATACLASSID, string sENTERPRISEID, string sIMPLEMENTATIONDATE)
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
            grdProductItemSpec.View.AddTextBoxColumn("LAYER", 200).SetValidationIsRequired();
            grdProductItemSpec.View.AddTextBoxColumn("USELAYER", 200);
            grdProductItemSpec.View.AddTextBoxColumn("COPPERTYPE", 200);
            grdProductItemSpec.View.AddTextBoxColumn("PACKINGQTY", 200);
            grdProductItemSpec.View.AddTextBoxColumn("PROJECTNAME", 200);
            grdProductItemSpec.View.AddTextBoxColumn("ENDUSER", 200);
            grdProductItemSpec.View.AddTextBoxColumn("COPPERPLATINGTYPE", 200);
            grdProductItemSpec.View.AddTextBoxColumn("PRODUCTTHICKNESSTYPE1", 200);
            grdProductItemSpec.View.AddTextBoxColumn("PRODUCTTHICKNESS1X", 200);
            grdProductItemSpec.View.AddTextBoxColumn("PRODUCTTHICKNESS1Y", 200);
            grdProductItemSpec.View.AddTextBoxColumn("PRODUCTTHICKNESSTYPE2", 200);
            grdProductItemSpec.View.AddTextBoxColumn("PRODUCTTHICKNESS2X", 200);
            grdProductItemSpec.View.AddTextBoxColumn("PRODUCTTHICKNESS2Y", 200);





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
            grdProductItemSpec.View.AddTextBoxColumn("PCSPNL", 200).SetValidationIsRequired(); 
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


            grdProductItemSpec.View.AddTextBoxColumn("INPUTSCALE1", 200);
            grdProductItemSpec.View.AddTextBoxColumn("INPUTSCALE2", 200);


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
            grdProductItemSpec.View.AddTextBoxColumn("LOTCONTROL", 200);
            grdProductItemSpec.View.AddTextBoxColumn("BLKSIZE", 200);
            

            grdProductItemSpec.View.PopulateColumns();

            // 자재
            grdBomComp.GridButtonItem = GridButtonItem.None;
            grdBomComp.View.AddTextBoxColumn("MASTERDATACLASSNAME", 80).SetIsReadOnly();
            grdBomComp.View.AddTextBoxColumn("ITEMID", 180).SetIsReadOnly();
            grdBomComp.View.AddTextBoxColumn("ITEMNAME", 200).SetIsReadOnly();
            if (UserInfo.Current.Enterprise == "YOUNGPOONG")
            {
                grdBomComp.View.AddTextBoxColumn("SPEC", 200).SetIsReadOnly();
            }
            grdBomComp.View.AddComboBoxColumn("MATERIALCLASS", 150, new SqlQuery("GetCodeList", "00001", "CODECLASSID=MaterialClass", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsReadOnly();

            grdBomComp.View.AddTextBoxColumn("ITEMVERSION", 100).SetIsHidden();
            grdBomComp.View.AddTextBoxColumn("USERLAYER", 80).SetIsReadOnly();
            grdBomComp.View.AddTextBoxColumn("WORKSURFACE", 100).SetIsReadOnly();

            grdBomComp.View.PopulateColumns();

            // INK
            grdInk.GridButtonItem = GridButtonItem.All;
            //grdInk.View.AddTextBoxColumn("MASTERDATACLASSNAME", 80);

            InitializeGrid_ItemMasterPopup();

            grdInk.View.AddTextBoxColumn("ITEMNAME1", 200);
            grdInk.View.AddTextBoxColumn("SPEC", 150).SetIsReadOnly();

            grdInk.View.AddComboBoxColumn("USERLAYER", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=UserLayer", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            //grdInk.View.AddTextBoxColumn("TOORIGINAL", 80);
            grdInk.View.PopulateColumns();

            // 회로사양
            grdCIRCUIT.GridButtonItem = GridButtonItem.All;
            grdCIRCUIT.View.AddTextBoxColumn("CIRCUITSPEC", 180);
            grdCIRCUIT.View.AddTextBoxColumn("CIRCUITSPECVALUE", 200);
            grdCIRCUIT.View.PopulateColumns();
            


            // 제품Type
            cboProductType.DisplayMember = "CODENAME";
            cboProductType.ValueMember = "CODEID";
            cboProductType.ShowHeader = false;
            Dictionary<string, object> ParamRt = new Dictionary<string, object>();
            ParamRt.Add("CODECLASSID", "ProductType");
            ParamRt.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtIdClasstype = SqlExecuter.Query("GetCodeList", "00001", ParamRt);
            cboProductType.DataSource = dtIdClasstype;

            ////// 동도금타입
            ////cboCopperPlatingType.DisplayMember = "CODENAME";
            ////cboCopperPlatingType.ValueMember = "CODEID";
            ////cboCopperPlatingType.ShowHeader = false;
            ////Dictionary<string, object> ParamCPT = new Dictionary<string, object>();
            ////ParamCPT.Add("CODECLASSID", "CopperPlatingType");
            ////ParamCPT.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            ////DataTable dtCTP = SqlExecuter.Query("GetCodeList", "00001", ParamCPT);
            ////cboCopperPlatingType.DataSource = dtCTP;

            //////HS코드
            ////cboHSCode.DisplayMember = "CODENAME";
            ////cboHSCode.ValueMember = "CODEID";
            ////cboHSCode.ShowHeader = false;
            ////Dictionary<string, object> ParamHSC = new Dictionary<string, object>();
            ////ParamHSC.Add("CODECLASSID", "HSCode");
            ////ParamHSC.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            ////DataTable dtHSC = SqlExecuter.Query("GetCodeList", "00001", ParamHSC);
            ////cboHSCode.DataSource = dtHSC;

            //층수
            cboLayer.DisplayMember = "CODENAME";
            cboLayer.ValueMember = "CODEID";
            cboLayer.ShowHeader = false;
            Dictionary<string, object> ParamLy = new Dictionary<string, object>();
            ParamLy.Add("CODECLASSID", "Layer");
            ParamLy.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtLy = SqlExecuter.Query("GetCodeList", "00001", ParamLy);
            cboLayer.DataSource = dtLy;

            if(UserInfo.Current.Enterprise == "YOUNGPOONG")
            {
                //층수
                cboUserLayer.DisplayMember = "CODENAME";
                cboUserLayer.ValueMember = "CODEID";
                cboUserLayer.ShowHeader = false;
                Dictionary<string, object> ParamUy = new Dictionary<string, object>();
                ParamUy.Add("CODECLASSID", "UserLayer");
                ParamUy.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                DataTable dtUy = SqlExecuter.Query("GetCodeList", "00001", ParamUy);
                cboUserLayer.DataSource = dtUy;

                cboUserLayer.Visible = true;
                lblUserLayer.Visible = true;
            }

            

            //UL-MARK
            cboulmark.DisplayMember = "CODENAME";
            cboulmark.ValueMember = "CODEID";
            cboulmark.ShowHeader = false;
            cboulmark.EmptyItemValue = 0;
            cboulmark.EmptyItemCaption = "";
            cboulmark.UseEmptyItem = true;

            Dictionary<string, object> ParamUm = new Dictionary<string, object>();
            ParamUm.Add("CODECLASSID", "YesNo");
            ParamUm.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtUm = SqlExecuter.Query("GetCodeList", "00001", ParamUm);
            cboulmark.DataSource = dtUm;

            ////////Hg-Fr
            //////cboHgFr.DisplayMember = "CODENAME";
            //////cboHgFr.ValueMember = "CODEID";
            //////cboHgFr.ShowHeader = false;
            //////Dictionary<string, object> ParamHgFr = new Dictionary<string, object>();
            //////ParamHgFr.Add("CODECLASSID", "Hg-Fr");
            //////ParamHgFr.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            //////DataTable dtHgFr = SqlExecuter.Query("GetCodeList", "00001", ParamHgFr);
            //////cboHgFr.DataSource = dtHgFr;

            //ASSY
            cboASSY.DisplayMember = "CODENAME";
            cboASSY.ValueMember = "CODEID";
            cboASSY.ShowHeader = false;
            cboASSY.EmptyItemValue = 0;
            cboASSY.EmptyItemCaption = "";
            cboASSY.UseEmptyItem = true;


            Dictionary<string, object> ParamASSY = new Dictionary<string, object>();
            ParamASSY.Add("CODECLASSID", "YesNo");
            ParamASSY.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtASSY = SqlExecuter.Query("GetCodeList", "00001", ParamASSY);
            cboASSY.DataSource = dtASSY;

            //OXIDE
            cboOXIDE.DisplayMember = "CODENAME";
            cboOXIDE.ValueMember = "CODEID";
            cboOXIDE.ShowHeader = false;
            cboOXIDE.EmptyItemValue = 0;
            cboOXIDE.EmptyItemCaption = "";
            cboOXIDE.UseEmptyItem = true;


            Dictionary<string, object> ParamOXIDE = new Dictionary<string, object>();
            ParamOXIDE.Add("CODECLASSID", "YesNo");
            ParamOXIDE.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtOXIDE = SqlExecuter.Query("GetCodeList", "00001", ParamOXIDE);
            cboOXIDE.DataSource = dtOXIDE;

            //분리부
            cboSeparatingPortion.DisplayMember = "CODENAME";
            cboSeparatingPortion.ValueMember = "CODEID";
            cboSeparatingPortion.ShowHeader = false;
            cboSeparatingPortion.EmptyItemValue = 0;
            cboSeparatingPortion.EmptyItemCaption = "";
            cboSeparatingPortion.UseEmptyItem = true;

            Dictionary<string, object> ParamSeparatingPortion = new Dictionary<string, object>();
            ParamSeparatingPortion.Add("CODECLASSID", "YesNo");
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
            //cboImpedance.DisplayMember = "CODENAME";
            //cboImpedance.ValueMember = "CODEID";
            //cboImpedance.ShowHeader = false;

            //cboImpedance.EmptyItemValue = 0;
            //cboImpedance.EmptyItemCaption = "";
            //cboImpedance.UseEmptyItem = true;

            //Dictionary<string, object> ParamImpedance = new Dictionary<string, object>();
            //ParamImpedance.Add("CODECLASSID", "YesNo");
            //ParamImpedance.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            //DataTable dtImpedance = SqlExecuter.Query("GetCodeList", "00001", ParamImpedance);
            //cboImpedance.DataSource = dtImpedance;

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

            ////////내층동
            //////cboInnerLayer.DisplayMember = "CODENAME";
            //////cboInnerLayer.ValueMember = "CODEID";
            //////cboInnerLayer.ShowHeader = false;
            //////Dictionary<string, object> ParamInnerLayer = new Dictionary<string, object>();
            //////ParamInnerLayer.Add("CODECLASSID", "InnerLayer");
            //////ParamInnerLayer.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            //////DataTable dtInnerLayer = SqlExecuter.Query("GetCodeList", "00001", ParamInnerLayer);
            //////cboInnerLayer.DataSource = dtInnerLayer;

            //////외층동
            ////cboOuterLayer.DisplayMember = "CODENAME";
            ////cboOuterLayer.ValueMember = "CODEID";
            ////cboOuterLayer.ShowHeader = false;
            ////Dictionary<string, object> ParamOuterLayer = new Dictionary<string, object>();
            ////ParamOuterLayer.Add("CODECLASSID", "OuterLayer");
            ////ParamOuterLayer.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            ////DataTable dtOuterLayer = SqlExecuter.Query("GetCodeList", "00001", ParamOuterLayer);
            ////cboOuterLayer.DataSource = dtOuterLayer;

            // 주차관리
            cboISWEEKMNG.DisplayMember = "CODENAME";
            cboISWEEKMNG.ValueMember = "CODEID";
            cboISWEEKMNG.ShowHeader = false;

            cboISWEEKMNG.EmptyItemValue = 0;
            cboISWEEKMNG.EmptyItemCaption = "";
            cboISWEEKMNG.UseEmptyItem = true;


            Dictionary<string, object> ParamISWEEKMNG = new Dictionary<string, object>();
            ParamISWEEKMNG.Add("CODECLASSID", "WeekCount");
            ParamISWEEKMNG.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtISWEEKMNG = SqlExecuter.Query("GetCodeList", "00001", ParamISWEEKMNG);
            cboISWEEKMNG.DataSource = dtISWEEKMNG;



            // Lot관리 여부
            cboLOTCONTROL.DisplayMember = "CODENAME";
            cboLOTCONTROL.ValueMember = "CODEID";
            cboLOTCONTROL.ShowHeader = false;
            Dictionary<string, object> ParamLOTCONTROL = new Dictionary<string, object>();
            ParamLOTCONTROL.Add("CODECLASSID", "YesNo");
            ParamLOTCONTROL.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtLOTCONTROL = SqlExecuter.Query("GetCodeList", "00001", ParamLOTCONTROL);
            cboLOTCONTROL.DataSource = dtLOTCONTROL;

            // Parameter 관리
            //cboISPARAMETER.DisplayMember = "CODENAME";
            //cboISPARAMETER.ValueMember = "CODEID";
            //cboISPARAMETER.ShowHeader = false;
            //Dictionary<string, object> ParamISPARAMETER = new Dictionary<string, object>();
            //ParamISPARAMETER.Add("CODECLASSID", "YesNo");
            //ParamISPARAMETER.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            //DataTable dtISPARAMETER = SqlExecuter.Query("GetCodeList", "00001", ParamISPARAMETER);
            //cboISPARAMETER.DataSource = dtISPARAMETER;

            

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
            //cboInputSCALE.DisplayMember = "CODENAME";
            //cboInputSCALE.ValueMember = "CODEID";
            //cboInputSCALE.ShowHeader = false;
            //Dictionary<string, object> ParamInputSCALE = new Dictionary<string, object>();
            //ParamInputSCALE.Add("CODECLASSID", "InputSCALE");
            //ParamInputSCALE.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            //DataTable dtInputSCALE = SqlExecuter.Query("GetCodeList", "00001", ParamInputSCALE);
            //cboInputSCALE.DataSource = dtInputSCALE;

            //신뢰성
            //cboReliability.DisplayMember = "CODENAME";
            //cboReliability.ValueMember = "CODEID";
            //cboReliability.ShowHeader = false;
            //Dictionary<string, object> ParamReliability = new Dictionary<string, object>();
            //ParamReliability.Add("CODECLASSID", "Reliability");
            //ParamReliability.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            //DataTable dtReliability = SqlExecuter.Query("GetCodeList", "00001", ParamReliability);
            //cboReliability.DataSource = dtReliability;

            //유해물질
            //cboHazardousSubstances.DisplayMember = "CODENAME";
            //cboHazardousSubstances.ValueMember = "CODEID";
            //cboHazardousSubstances.ShowHeader = false;
            //Dictionary<string, object> ParamHazardousSubstances = new Dictionary<string, object>();
            //ParamHazardousSubstances.Add("CODECLASSID", "HazardousSubstances");
            //ParamHazardousSubstances.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            //DataTable dtHazardousSubstances = SqlExecuter.Query("GetCodeList", "00001", ParamHazardousSubstances);
            //cboHazardousSubstances.DataSource = dtHazardousSubstances;

            //치수측정
            //cboMeasurement.DisplayMember = "CODENAME";
            //cboMeasurement.ValueMember = "CODEID";
            //cboMeasurement.ShowHeader = false;


            //Dictionary<string, object> ParamMeasurement = new Dictionary<string, object>();
            //ParamMeasurement.Add("CODECLASSID", "Measurement");
            //ParamMeasurement.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            //DataTable dtMeasurement = SqlExecuter.Query("GetCodeList", "00001", ParamMeasurement);
            //cboMeasurement.DataSource = dtMeasurement;


            
            if (UserInfo.Current.Enterprise == "YOUNGPOONG")
            {
                grdCIRCUIT.Visible = true;
                grdCIRCUIT.Dock = DockStyle.Fill;
            }


            if (UserInfo.Current.Enterprise == "INTERFLEX")
            {
                CIRCUIT_SPEC.Visible = true;
                //CIRCUIT_SPEC.Dock = DockStyle.Fill;

                lblCustomerItemVersion.Visible = true;
                txtCustomerItemVersion.Visible = true;

                lblCopperFoilUpLayer.Visible = true;
                cboCopperLayerUp.Visible = true;
                lblCopperFoilDownLayer.Visible = true;
                cboCopperLayerDown.Visible = true;

                pnlMINCL.Visible = true;
                pnlMINPSR.Visible = true;
                lblMINCL.Visible = true;
                lblMINPSR.Visible = true;
                //lblMINCL1.Visible = true;
                //txtMINCL.Visible = true;


                //lblMINPSR1.Visible = true;
                //txtMINPSR.Visible = true;

                lblLotMsgYn.Visible = true;
                cboLOTCONTROL.Visible = true;

                lblPNLmm.Visible = true;
                txtPNLmm.Visible = true;

                lblProductThickness1.Visible = true;
                lblProductThickness2.Visible = true;
                cboProductThickness1.Visible = true;
                cboProductThickness2.Visible = true;

                pnlProductThickness1.Visible = true;
                pnlProductThickness2.Visible = true;




                lblInputSizeXAxis.Visible = true;
                cboINPUTSIZEXAXIS.Visible = true;

                lblARYSizeXAxis.Visible = true;
                txtARYSizeXAxis.Visible = true;
                lblARYSizeXAxis1.Visible = true;
                txtARYSizeYAxis.Visible = true;

                //제품두께1
                cboProductThickness1.DisplayMember = "CODENAME";
                cboProductThickness1.ValueMember = "CODEID";
                cboProductThickness1.ShowHeader = false;
                cboProductThickness1.EmptyItemValue = 0;
                cboProductThickness1.EmptyItemCaption = "";
                cboProductThickness1.UseEmptyItem = true;
                Dictionary<string, object> ParamProductThickness1 = new Dictionary<string, object>();
                ParamProductThickness1.Add("CODECLASSID", "ProductThickness");
                ParamProductThickness1.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                DataTable dtProductThickness1 = SqlExecuter.Query("GetCodeList", "00001", ParamProductThickness1);
                cboProductThickness1.DataSource = dtProductThickness1;

                //제품두께2
                cboProductThickness2.DisplayMember = "CODENAME";
                cboProductThickness2.ValueMember = "CODEID";
                cboProductThickness2.ShowHeader = false;
                cboProductThickness2.EmptyItemValue = 0;
                cboProductThickness2.EmptyItemCaption = "";
                cboProductThickness2.UseEmptyItem = true;
                Dictionary<string, object> ParamProductThickness2 = new Dictionary<string, object>();
                ParamProductThickness2.Add("CODECLASSID", "ProductThickness");
                ParamProductThickness2.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                DataTable dtProductThickness2 = SqlExecuter.Query("GetCodeList", "00001", ParamProductThickness2);
                cboProductThickness2.DataSource = dtProductThickness2;
            }

            //InputSizeXAxis
            cboINPUTSIZEXAXIS.DisplayMember = "CODENAME";
            cboINPUTSIZEXAXIS.ValueMember = "CODEID";
            cboINPUTSIZEXAXIS.ShowHeader = false;
            cboINPUTSIZEXAXIS.EmptyItemValue = 0;
            cboINPUTSIZEXAXIS.EmptyItemCaption = "";
            cboINPUTSIZEXAXIS.UseEmptyItem = true;
            Dictionary<string, object> ParamINPUTSIZEXAXIS = new Dictionary<string, object>();
            ParamINPUTSIZEXAXIS.Add("CODECLASSID", "InputSize");
            ParamINPUTSIZEXAXIS.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtINPUTSIZEXAXIS = SqlExecuter.Query("GetCodeList", "00001", ParamINPUTSIZEXAXIS);
            cboINPUTSIZEXAXIS.DataSource = dtINPUTSIZEXAXIS;


            //CopperPlatingType1
            cboCopperPlatingType1.DisplayMember = "CODENAME";
            cboCopperPlatingType1.ValueMember = "CODEID";
            cboCopperPlatingType1.ShowHeader = false;
            cboCopperPlatingType1.EmptyItemValue = 0;
            cboCopperPlatingType1.EmptyItemCaption = "";
            cboCopperPlatingType1.UseEmptyItem = true;
            Dictionary<string, object> ParamCopperPlatingType1 = new Dictionary<string, object>();
            ParamCopperPlatingType1.Add("CODECLASSID", "CopperPlatingType");
            ParamCopperPlatingType1.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtCopperPlatingType1 = SqlExecuter.Query("GetCodeList", "00001", ParamCopperPlatingType1);
            cboCopperPlatingType1.DataSource = dtCopperPlatingType1;

            //CopperPlatingType2
            cboCopperPlatingType2.DisplayMember = "CODENAME";
            cboCopperPlatingType2.ValueMember = "CODEID";
            cboCopperPlatingType2.ShowHeader = false;
            cboCopperPlatingType2.EmptyItemValue = 0;
            cboCopperPlatingType2.EmptyItemCaption = "";
            cboCopperPlatingType2.UseEmptyItem = true;
            Dictionary<string, object> ParamCopperPlatingType2 = new Dictionary<string, object>();
            ParamCopperPlatingType2.Add("CODECLASSID", "CopperPlatingType");
            ParamCopperPlatingType2.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtCopperPlatingType2 = SqlExecuter.Query("GetCodeList", "00001", ParamCopperPlatingType2);
            cboCopperPlatingType2.DataSource = dtCopperPlatingType2;

            //CopperPlatingType3
            cboCopperPlatingType3.DisplayMember = "CODENAME";
            cboCopperPlatingType3.ValueMember = "CODEID";
            cboCopperPlatingType3.ShowHeader = false;
            cboCopperPlatingType3.EmptyItemValue = 0;
            cboCopperPlatingType3.EmptyItemCaption = "";
            cboCopperPlatingType3.UseEmptyItem = true;
            Dictionary<string, object> ParamCopperPlatingType3 = new Dictionary<string, object>();
            ParamCopperPlatingType3.Add("CODECLASSID", "CopperPlatingType");
            ParamCopperPlatingType3.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtCopperPlatingType3 = SqlExecuter.Query("GetCodeList", "00001", ParamCopperPlatingType3);
            cboCopperPlatingType3.DataSource = dtCopperPlatingType3;

            //CopperPlatingType4
            cboCopperPlatingType4.DisplayMember = "CODENAME";
            cboCopperPlatingType4.ValueMember = "CODEID";
            cboCopperPlatingType4.ShowHeader = false;
            cboCopperPlatingType4.EmptyItemValue = 0;
            cboCopperPlatingType4.EmptyItemCaption = "";
            cboCopperPlatingType4.UseEmptyItem = true;
            Dictionary<string, object> ParamCopperPlatingType4 = new Dictionary<string, object>();
            ParamCopperPlatingType4.Add("CODECLASSID", "CopperPlatingType");
            ParamCopperPlatingType4.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtCopperPlatingType4 = SqlExecuter.Query("GetCodeList", "00001", ParamCopperPlatingType4);
            cboCopperPlatingType4.DataSource = dtCopperPlatingType4;


            //PlatingType1
            cboPlatingType1.DisplayMember = "CODENAME";
            cboPlatingType1.ValueMember = "CODEID";
            cboPlatingType1.ShowHeader = false;
            cboPlatingType1.EmptyItemValue = 0;
            cboPlatingType1.EmptyItemCaption = "";
            cboPlatingType1.UseEmptyItem = true;
            Dictionary<string, object> ParamPlatingType1 = new Dictionary<string, object>();
            ParamPlatingType1.Add("CODECLASSID", "PlatingType");
            ParamPlatingType1.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtPlatingType1 = SqlExecuter.Query("GetCodeList", "00001", ParamPlatingType1);
            cboPlatingType1.DataSource = dtPlatingType1;

            //PlatingType2
            cboPlatingType2.DisplayMember = "CODENAME";
            cboPlatingType2.ValueMember = "CODEID";
            cboPlatingType2.ShowHeader = false;
            cboPlatingType2.EmptyItemValue = 0;
            cboPlatingType2.EmptyItemCaption = "";
            cboPlatingType2.UseEmptyItem = true;
            Dictionary<string, object> ParamPlatingType2 = new Dictionary<string, object>();
            ParamPlatingType2.Add("CODECLASSID", "PlatingType");
            ParamPlatingType2.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtPlatingType2 = SqlExecuter.Query("GetCodeList", "00001", ParamPlatingType2);
            cboPlatingType2.DataSource = dtPlatingType2;

            //PlatingType3
            cboPlatingType3.DisplayMember = "CODENAME";
            cboPlatingType3.ValueMember = "CODEID";
            cboPlatingType3.ShowHeader = false;
            cboPlatingType3.EmptyItemValue = 0;
            cboPlatingType3.EmptyItemCaption = "";
            cboPlatingType3.UseEmptyItem = true;
            Dictionary<string, object> ParamPlatingType3 = new Dictionary<string, object>();
            ParamPlatingType3.Add("CODECLASSID", "PlatingType");
            ParamPlatingType3.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtPlatingType3 = SqlExecuter.Query("GetCodeList", "00001", ParamPlatingType3);
            cboPlatingType3.DataSource = dtPlatingType3;


            //PlatingTypeMaterial1
            cboPlatingTypeMaterial1.DisplayMember = "CODENAME";
            cboPlatingTypeMaterial1.ValueMember = "CODEID";
            cboPlatingTypeMaterial1.ShowHeader = false;
            cboPlatingTypeMaterial1.EmptyItemValue = 0;
            cboPlatingTypeMaterial1.EmptyItemCaption = "";
            cboPlatingTypeMaterial1.UseEmptyItem = true;
            Dictionary<string, object> ParamPlatingTypeMaterial1 = new Dictionary<string, object>();
            ParamPlatingTypeMaterial1.Add("CODECLASSID", "PlatingTypeMaterial");
            ParamPlatingTypeMaterial1.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtPlatingTypeMaterial1 = SqlExecuter.Query("GetCodeList", "00001", ParamPlatingTypeMaterial1);
            cboPlatingTypeMaterial1.DataSource = dtPlatingTypeMaterial1;

            //PlatingTypeMaterial2
            cboPlatingTypeMaterial2.DisplayMember = "CODENAME";
            cboPlatingTypeMaterial2.ValueMember = "CODEID";
            cboPlatingTypeMaterial2.ShowHeader = false;
            cboPlatingTypeMaterial2.EmptyItemValue = 0;
            cboPlatingTypeMaterial2.EmptyItemCaption = "";
            cboPlatingTypeMaterial2.UseEmptyItem = true;
            Dictionary<string, object> ParamPlatingTypeMaterial2 = new Dictionary<string, object>();
            ParamPlatingTypeMaterial2.Add("CODECLASSID", "PlatingTypeMaterial");
            ParamPlatingTypeMaterial2.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtPlatingTypeMaterial2 = SqlExecuter.Query("GetCodeList", "00001", ParamPlatingTypeMaterial2);
            cboPlatingTypeMaterial2.DataSource = dtPlatingTypeMaterial2;

            //PlatingTypeMaterial3
            cboPlatingTypeMaterial3.DisplayMember = "CODENAME";
            cboPlatingTypeMaterial3.ValueMember = "CODEID";
            cboPlatingTypeMaterial3.ShowHeader = false;
            cboPlatingTypeMaterial3.EmptyItemValue = 0;
            cboPlatingTypeMaterial3.EmptyItemCaption = "";
            cboPlatingTypeMaterial3.UseEmptyItem = true;
            Dictionary<string, object> ParamPlatingTypeMaterial3 = new Dictionary<string, object>();
            ParamPlatingTypeMaterial3.Add("CODECLASSID", "PlatingTypeMaterial");
            ParamPlatingTypeMaterial3.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtPlatingTypeMaterial3 = SqlExecuter.Query("GetCodeList", "00001", ParamPlatingTypeMaterial3);
            cboPlatingTypeMaterial3.DataSource = dtPlatingTypeMaterial3;


            //PlatingTypeMaterial4
            cboPlatingTypeMaterial4.DisplayMember = "CODENAME";
            cboPlatingTypeMaterial4.ValueMember = "CODEID";
            cboPlatingTypeMaterial4.ShowHeader = false;
            cboPlatingTypeMaterial4.EmptyItemValue = 0;
            cboPlatingTypeMaterial4.EmptyItemCaption = "";
            cboPlatingTypeMaterial4.UseEmptyItem = true;
            Dictionary<string, object> ParamPlatingTypeMaterial4 = new Dictionary<string, object>();
            ParamPlatingTypeMaterial4.Add("CODECLASSID", "PlatingTypeMaterial");
            ParamPlatingTypeMaterial4.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtPlatingTypeMaterial4 = SqlExecuter.Query("GetCodeList", "00001", ParamPlatingTypeMaterial4);
            cboPlatingTypeMaterial4.DataSource = dtPlatingTypeMaterial4;

            //PlatingTypeMaterial5
            cboPlatingTypeMaterial5.DisplayMember = "CODENAME";
            cboPlatingTypeMaterial5.ValueMember = "CODEID";
            cboPlatingTypeMaterial5.ShowHeader = false;
            cboPlatingTypeMaterial5.EmptyItemValue = 0;
            cboPlatingTypeMaterial5.EmptyItemCaption = "";
            cboPlatingTypeMaterial5.UseEmptyItem = true;
            Dictionary<string, object> ParamPlatingTypeMaterial5 = new Dictionary<string, object>();
            ParamPlatingTypeMaterial5.Add("CODECLASSID", "PlatingTypeMaterial");
            ParamPlatingTypeMaterial5.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtPlatingTypeMaterial5 = SqlExecuter.Query("GetCodeList", "00001", ParamPlatingTypeMaterial5);
            cboPlatingTypeMaterial5.DataSource = dtPlatingTypeMaterial5;

            //PlatingTypeMaterial6
            cboPlatingTypeMaterial6.DisplayMember = "CODENAME";
            cboPlatingTypeMaterial6.ValueMember = "CODEID";
            cboPlatingTypeMaterial6.ShowHeader = false;
            cboPlatingTypeMaterial6.EmptyItemValue = 0;
            cboPlatingTypeMaterial6.EmptyItemCaption = "";
            cboPlatingTypeMaterial6.UseEmptyItem = true;
            Dictionary<string, object> ParamPlatingTypeMaterial6 = new Dictionary<string, object>();
            ParamPlatingTypeMaterial6.Add("CODECLASSID", "PlatingTypeMaterial");
            ParamPlatingTypeMaterial6.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtPlatingTypeMaterial6 = SqlExecuter.Query("GetCodeList", "00001", ParamPlatingTypeMaterial6);
            cboPlatingTypeMaterial6.DataSource = dtPlatingTypeMaterial6;

            //PlatingTypeMaterial7
            cboPlatingTypeMaterial7.DisplayMember = "CODENAME";
            cboPlatingTypeMaterial7.ValueMember = "CODEID";
            cboPlatingTypeMaterial7.ShowHeader = false;
            cboPlatingTypeMaterial7.EmptyItemValue = 0;
            cboPlatingTypeMaterial7.EmptyItemCaption = "";
            cboPlatingTypeMaterial7.UseEmptyItem = true;
            Dictionary<string, object> ParamPlatingTypeMaterial7 = new Dictionary<string, object>();
            ParamPlatingTypeMaterial7.Add("CODECLASSID", "PlatingTypeMaterial");
            ParamPlatingTypeMaterial7.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtPlatingTypeMaterial7 = SqlExecuter.Query("GetCodeList", "00001", ParamPlatingTypeMaterial7);
            cboPlatingTypeMaterial7.DataSource = dtPlatingTypeMaterial7;


            //PlatingTypeMaterial8
            cboPlatingTypeMaterial8.DisplayMember = "CODENAME";
            cboPlatingTypeMaterial8.ValueMember = "CODEID";
            cboPlatingTypeMaterial8.ShowHeader = false;
            cboPlatingTypeMaterial8.EmptyItemValue = 0;
            cboPlatingTypeMaterial8.EmptyItemCaption = "";
            cboPlatingTypeMaterial8.UseEmptyItem = true;
            Dictionary<string, object> ParamPlatingTypeMaterial8 = new Dictionary<string, object>();
            ParamPlatingTypeMaterial8.Add("CODECLASSID", "PlatingTypeMaterial");
            ParamPlatingTypeMaterial8.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtPlatingTypeMaterial8 = SqlExecuter.Query("GetCodeList", "00001", ParamPlatingTypeMaterial8);
            cboPlatingTypeMaterial8.DataSource = dtPlatingTypeMaterial8;


            //PlatingTypeMaterial9
            cboPlatingTypeMaterial9.DisplayMember = "CODENAME";
            cboPlatingTypeMaterial9.ValueMember = "CODEID";
            cboPlatingTypeMaterial9.ShowHeader = false;
            cboPlatingTypeMaterial9.EmptyItemValue = 0;
            cboPlatingTypeMaterial9.EmptyItemCaption = "";
            cboPlatingTypeMaterial9.UseEmptyItem = true;
            Dictionary<string, object> ParamPlatingTypeMaterial9 = new Dictionary<string, object>();
            ParamPlatingTypeMaterial9.Add("CODECLASSID", "PlatingType");
            ParamPlatingTypeMaterial9.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtPlatingTypeMaterial9 = SqlExecuter.Query("GetCodeList", "00001", ParamPlatingTypeMaterial9);
            cboPlatingTypeMaterial9.DataSource = dtPlatingTypeMaterial9;


            //PlatingTypeMaterial9
            cboEndUser.DisplayMember = "CODENAME";
            cboEndUser.ValueMember = "CODEID";
            cboEndUser.ShowHeader = false;
            cboEndUser.EmptyItemValue = 0;
            cboEndUser.EmptyItemCaption = "";
            cboEndUser.UseEmptyItem = true;
            Dictionary<string, object> ParamEndUser = new Dictionary<string, object>();
            ParamEndUser.Add("CODECLASSID", "EndUser");
            ParamEndUser.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtEndUser = SqlExecuter.Query("GetCodeList", "00001", ParamEndUser);
            cboEndUser.DataSource = dtEndUser;


            //cboCopperLayerUp
            cboCopperLayerUp.DisplayMember = "CODENAME";
            cboCopperLayerUp.ValueMember = "CODEID";
            cboCopperLayerUp.ShowHeader = false;
            cboCopperLayerUp.EmptyItemValue = 0;
            cboCopperLayerUp.EmptyItemCaption = "";
            cboCopperLayerUp.UseEmptyItem = true;
            Dictionary<string, object> ParamCopperLayerUp = new Dictionary<string, object>();
            ParamCopperLayerUp.Add("CODECLASSID", "CopperLayerUp");
            ParamCopperLayerUp.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtCopperLayerUp = SqlExecuter.Query("GetCodeList", "00001", ParamCopperLayerUp);
            cboCopperLayerUp.DataSource = dtCopperLayerUp;

            //cboCopperLayerDown
            cboCopperLayerDown.DisplayMember = "CODENAME";
            cboCopperLayerDown.ValueMember = "CODEID";
            cboCopperLayerDown.ShowHeader = false;
            cboCopperLayerDown.EmptyItemValue = 0;
            cboCopperLayerDown.EmptyItemCaption = "";
            cboCopperLayerDown.UseEmptyItem = true;
            Dictionary<string, object> ParamCopperLayerDown = new Dictionary<string, object>();
            ParamCopperLayerDown.Add("CODECLASSID", "CopperLayerDown");
            ParamCopperLayerDown.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtCopperLayerDown = SqlExecuter.Query("GetCodeList", "00001", ParamCopperLayerDown);
            cboCopperLayerDown.DataSource = dtCopperLayerDown;

            //////OLB회로
            ////cboOLBCircuit.DisplayMember = "CODENAME";
            ////cboOLBCircuit.ValueMember = "CODEID";
            ////cboOLBCircuit.ShowHeader = false;
            ////Dictionary<string, object> ParamOLBCircuit = new Dictionary<string, object>();
            ////ParamOLBCircuit.Add("CODECLASSID", "OLBCircuit");
            ////ParamOLBCircuit.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            ////DataTable dtOLBCircuit = SqlExecuter.Query("GetCodeList", "00001", ParamOLBCircuit);
            ////cboOLBCircuit.DataSource = dtOLBCircuit;

            // 거래처팝업
            ConditionItemSelectPopup cisidvendorCode = new ConditionItemSelectPopup();
            cisidvendorCode.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            cisidvendorCode.SetPopupLayout("CUSTOMERID", PopupButtonStyles.Ok_Cancel);
            
            cisidvendorCode.Id = "CUSTOMERID";
            cisidvendorCode.LabelText = "CUSTOMERID";
            cisidvendorCode.SearchQuery = new SqlQuery("GetCustomerList", "10002", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}");
            cisidvendorCode.IsMultiGrid = false;
            cisidvendorCode.DisplayFieldName = "CUSTOMERID";
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
            //sspCustomerId.TextChanged += SspCustomerId_TextChanged;

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

            DataTable dtBomComp = SqlExecuter.Query("GetItemMasterBomComp", "10001", Param);
            grdBomComp.DataSource = dtBomComp;



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
                    
                    if (control.ToString() == "Micube.Framework.SmartControls.SmartGroupBox")
                    {
                        // 제품사양 /기타
                        switch(control.Name)
                        {
                            case "groupboxspec":
                            case "groupItem":
                            case "groupdescriptionspec":
                                foreach (Control controlc in control.Controls)
                                {
                                    foreach (Control controlcc in controlc.Controls)
                                    {
                                        switch (controlcc.ToString())
                                        {

                                            case "Micube.Framework.SmartControls.SmartSpinEdit":
                                            case "Micube.Framework.SmartControls.SmartTextBox":
                                                if (dt.Columns.IndexOf(controlcc.Tag.ToString()) != -1)
                                                {
                                                    controlcc.Text = dt.Rows[0][controlcc.Tag.ToString()].ToString();
                                                }
                                                break;

                                            case "Micube.Framework.SmartControls.SmartPanel":
                                                foreach (Control controlccc in controlcc.Controls)
                                                {
                                                    switch (controlccc.ToString())
                                                    {
                                                        case "Micube.Framework.SmartControls.SmartSpinEdit": // 숫자텍스트
                                                        case "Micube.Framework.SmartControls.SmartTextBox":  // 텍스트
                                                                                                             //dt.Rows[0][controlccc.Tag.ToString()] = controlccc.Text;

                                                            controlccc.Text = dt.Rows[0][controlccc.Tag.ToString()].ToString();


                                                            break;
                                                    }

                                                }
                                                break;

                                            case "Micube.Framework.SmartControls.SmartComboBox":

                                                Micube.Framework.SmartControls.SmartComboBox combox = new SmartComboBox();
                                                combox = (Micube.Framework.SmartControls.SmartComboBox)controlcc;

                                                if (dt.Columns.IndexOf(controlcc.Tag.ToString()) != -1)
                                                {
                                                    combox.EditValue = dt.Rows[0][controlcc.Tag.ToString()].ToString();
                                                }

                                                break;

                                            case "Micube.Framework.SmartControls.SmartSelectPopupEdit":

                                                Micube.Framework.SmartControls.SmartSelectPopupEdit SelectPopup = new SmartSelectPopupEdit();

                                                SelectPopup = (Micube.Framework.SmartControls.SmartSelectPopupEdit)controlcc;

                                                if (dt.Columns.IndexOf(controlcc.Tag.ToString()) != -1)
                                                {

                                                    //SelectPopup.SetValue(dt.Rows[0][controlcc.Tag.ToString()].ToString());
                                                    //if (controlcc.Tag.ToString().IndexOf("ID") != -1)
                                                    //{
                                                    //    SelectPopup.Text = dt.Rows[0][controlcc.Tag.ToString().Replace("ID", "NAME")].ToString();
                                                    //}
                                                    //else
                                                    //{
                                                    //    SelectPopup.Text = dt.Rows[0][controlcc.Tag.ToString() + "NAME"].ToString();
                                                    //}

                                                    SelectPopup.SetValue(dt.Rows[0][controlcc.Tag.ToString()].ToString());

                                                    if (controlcc.Tag.ToString().IndexOf("ID") != -1)
                                                    {
                                                        if (SelectPopup.SelectPopupCondition.DisplayFieldName == controlcc.Tag.ToString())
                                                        {
                                                            SelectPopup.Text = dt.Rows[0][controlcc.Tag.ToString()].ToString();
                                                        }
                                                        else
                                                        {
                                                            SelectPopup.Text = dt.Rows[0][controlcc.Tag.ToString().Replace("ID", "NAME")].ToString();
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (SelectPopup.SelectPopupCondition.DisplayFieldName == controlcc.Tag.ToString())
                                                        {
                                                            SelectPopup.Text = dt.Rows[0][controlcc.Tag.ToString()].ToString();
                                                        }
                                                        else
                                                        {
                                                            SelectPopup.Text = dt.Rows[0][controlcc.Tag.ToString() + "NAME"].ToString();
                                                        }
                                                    }
                                                    SelectPopup.Refresh();

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
                                                if (dt.Columns.IndexOf(controlcc.Tag.ToString()) != -1)
                                                {
                                                    controlcc.Text = dt.Rows[0][controlcc.Tag.ToString()].ToString();
                                                }
                                                break;
                                        }
                                    }

                                }
                                break;

                        }

                    }
                }
                //fnCaluation();
 
                Dictionary<string, object> paramInkSpecification = new Dictionary<string, object>();
                paramInkSpecification.Add("ITEMID", txtItemCode.Text);
                paramInkSpecification.Add("ITEMVERSION", txtItemver.Text);
                paramInkSpecification.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                paramInkSpecification.Add("DETAILTYPE", "InkSpecification");
                DataTable dtInkSpecification = SqlExecuter.Query("GetProductItemSpecDetail", "10001", paramInkSpecification);
                grdInk.DataSource = dtInkSpecification;

                //////////
                // 도금사양
                Dictionary<string, object> paramSurfacePlating = new Dictionary<string, object>();
                paramSurfacePlating.Add("ITEMID", txtItemCode.Text);
                paramSurfacePlating.Add("ITEMVERSION", txtItemver.Text);
                paramSurfacePlating.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                paramSurfacePlating.Add("DETAILTYPE", "SurfacePlating");
                DataTable dtSurfacePlatingChk = SqlExecuter.Query("GetProductItemSpecDetail", "10001", paramSurfacePlating);
                int iSEQUENCE = 0;
                if (dtSurfacePlatingChk != null)
                {
                    if(dtSurfacePlatingChk.Rows.Count !=0)
                    {
                        
                        for (int irow = 0; irow <= 13; irow++)
                        {
                            if (irow <= 3)
                            {
                                for (int icol = 2; icol <= 3; icol++)
                                {

                                    Control ccontrol = this.PLATING_STPl.GetControlFromPosition(icol, irow);

                                    if (ccontrol != null)
                                    {
                                        switch (ccontrol.ToString())
                                        {
                                            case "Micube.Framework.SmartControls.SmartSpinEdit": // 숫자에디터
                                            case "Micube.Framework.SmartControls.SmartTextBox": // 에디터
                                                if (ccontrol.Tag.ToString() != "")
                                                {
                                                    DataRow[] rowi = dtSurfacePlatingChk.Select("SEQUENCE = " + iSEQUENCE + "");
                                                    ccontrol.Text = rowi[0][ccontrol.Tag.ToString()].ToString();

                                                }
                                                break;
                                            case "Micube.Framework.SmartControls.SmartComboBox": //콤보
                                                Micube.Framework.SmartControls.SmartComboBox combox = new SmartComboBox();
                                                combox = (Micube.Framework.SmartControls.SmartComboBox)ccontrol;
                                                if (ccontrol.Tag.ToString() != "")
                                                {
                                                    DataRow[] rowi = dtSurfacePlatingChk.Select("SEQUENCE = " + iSEQUENCE + "");
                                                    combox.EditValue = rowi[0][ccontrol.Tag.ToString()].ToString();
                                                }
                                                break;

                                                //case "Micube.Framework.SmartControls.SmartLabel":
                                                //    if (ccontrol.Tag.ToString() != "")
                                                //    {
                                                //        rowNew[ccontrol.Tag.ToString()] = ccontrol.Name;
                                                //    }
                                                //    break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                for (int icol = 2; icol <= 6; icol++)
                                {

                                    Control ccontrol = this.PLATING_STPl.GetControlFromPosition(icol, irow);

                                    if (ccontrol != null)
                                    {
                                        switch (ccontrol.ToString())
                                        {
                                            case "Micube.Framework.SmartControls.SmartSpinEdit": // 숫자에디터
                                            case "Micube.Framework.SmartControls.SmartTextBox": // 에디터
                                                if (ccontrol.Tag.ToString() != "")
                                                {
                                                    DataRow[] rowi = dtSurfacePlatingChk.Select("SEQUENCE = " + iSEQUENCE + "");
                                                    ccontrol.Text = rowi[0][ccontrol.Tag.ToString()].ToString();

                                                }
                                                break;
                                            case "Micube.Framework.SmartControls.SmartComboBox": //콤보
                                                Micube.Framework.SmartControls.SmartComboBox combox = new SmartComboBox();
                                                combox = (Micube.Framework.SmartControls.SmartComboBox)ccontrol;
                                                if (ccontrol.Tag.ToString() != "")
                                                {
                                                    DataRow[] rowi = dtSurfacePlatingChk.Select("SEQUENCE = " + iSEQUENCE + "");
                                                    combox.EditValue = rowi[0][ccontrol.Tag.ToString()].ToString();
                                                }
                                                break;
                                        }
                                    }
                                }
                            }

                            iSEQUENCE = iSEQUENCE + 1;
                        }

                        for (int irow = 0; irow <= 12; irow++)
                        {

                            if (irow <= 3)
                            {
                                for (int icol = 9; icol <= 10; icol++)
                                {
                                    Control ccontrol = this.PLATING_STPl.GetControlFromPosition(icol, irow);

                                    if (ccontrol != null)
                                    {
                                        switch (ccontrol.ToString())
                                        {
                                            case "Micube.Framework.SmartControls.SmartSpinEdit": // 숫자에디터
                                            case "Micube.Framework.SmartControls.SmartTextBox": // 에디터
                                                if (ccontrol.Tag.ToString() != "")
                                                {
                                                    DataRow[] rowi = dtSurfacePlatingChk.Select("SEQUENCE = " + iSEQUENCE + "");
                                                    ccontrol.Text = rowi[0][ccontrol.Tag.ToString()].ToString();
                                                }
                                                break;
                                            case "Micube.Framework.SmartControls.SmartComboBox": //콤보
                                                Micube.Framework.SmartControls.SmartComboBox combox = new SmartComboBox();
                                                combox = (Micube.Framework.SmartControls.SmartComboBox)ccontrol;
                                                if (ccontrol.Tag.ToString() != "")
                                                {
                                                    DataRow[] rowi = dtSurfacePlatingChk.Select("SEQUENCE = " + iSEQUENCE + "");
                                                    combox.EditValue = rowi[0][ccontrol.Tag.ToString()].ToString();
                                                }
                                                break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                for (int icol = 9; icol <= 14; icol++)
                                {
                                    Control ccontrol = this.PLATING_STPl.GetControlFromPosition(icol, irow);

                                    if (ccontrol != null)
                                    {
                                        switch (ccontrol.ToString())
                                        {
                                            case "Micube.Framework.SmartControls.SmartSpinEdit": // 숫자에디터
                                            case "Micube.Framework.SmartControls.SmartTextBox": // 에디터
                                                if (ccontrol.Tag.ToString() != "")
                                                {
                                                    DataRow[] rowi = dtSurfacePlatingChk.Select("SEQUENCE = " + iSEQUENCE + "");
                                                    ccontrol.Text = rowi[0][ccontrol.Tag.ToString()].ToString();
                                                }
                                                break;
                                            case "Micube.Framework.SmartControls.SmartComboBox": //콤보
                                                Micube.Framework.SmartControls.SmartComboBox combox = new SmartComboBox();
                                                combox = (Micube.Framework.SmartControls.SmartComboBox)ccontrol;
                                                if (ccontrol.Tag.ToString() != "")
                                                {
                                                    DataRow[] rowi = dtSurfacePlatingChk.Select("SEQUENCE = " + iSEQUENCE + "");
                                                    combox.EditValue = rowi[0][ccontrol.Tag.ToString()].ToString();
                                                }
                                                break;
                                        }
                                    }
                                }
                            }

                            iSEQUENCE = iSEQUENCE + 1;
                        }
                    }
                }


                ///////////
                // 회로사양 
                Dictionary<string, object> paramCircuit = new Dictionary<string, object>();
                paramCircuit.Add("ITEMID", txtItemCode.Text);
                paramCircuit.Add("ITEMVERSION", txtItemver.Text);
                paramCircuit.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                paramCircuit.Add("DETAILTYPE", "Circuit");
                DataTable dtCircuit = SqlExecuter.Query("GetProductItemSpecDetail", "10001", paramCircuit);

                if(dtCircuit != null)
                {
                    if(dtCircuit.Rows.Count !=0)
                    {
                        // 인터
                        if (UserInfo.Current.Enterprise == "INTERFLEX")
                        {

                            ///////////
                            // 회로사양 인터
                            iSEQUENCE = 1;
                            for (int irow = 1; irow <= 12; irow++)
                            {

                                for (int icol = 1; icol <= 4; icol++)
                                {

                                    Control ccontrol = this.CIRCUIT_SPEC.GetControlFromPosition(icol, irow);

                                    if (ccontrol != null)
                                    {
                                        switch (ccontrol.ToString())
                                        {
                                            case "Micube.Framework.SmartControls.SmartSpinEdit": // 숫자에디터
                                            case "Micube.Framework.SmartControls.SmartTextBox": // 에디터
                                                if (ccontrol.Tag.ToString() != "")
                                                {
                                                    DataRow[] rowi = dtCircuit.Select("SEQUENCE = " + iSEQUENCE + "");
                                                    ccontrol.Text = rowi[0][ccontrol.Tag.ToString()].ToString();

                                                }
                                                break;
                                            case "Micube.Framework.SmartControls.SmartComboBox": //콤보
                                                Micube.Framework.SmartControls.SmartComboBox combox = new SmartComboBox();
                                                combox = (Micube.Framework.SmartControls.SmartComboBox)ccontrol;
                                                if (ccontrol.Tag.ToString() != "")
                                                {
                                                    DataRow[] rowi = dtCircuit.Select("SEQUENCE = " + iSEQUENCE + "");
                                                    combox.EditValue = rowi[0][ccontrol.Tag.ToString()].ToString();
                                                }
                                                break;
                                        }
                                    }
                                }

                                iSEQUENCE = iSEQUENCE + 1;
                            }


                            for (int irow = 1; irow <= 12; irow++)
                            {

                                for (int icol = 5; icol <= 6; icol++)
                                {

                                    Control ccontrol = this.CIRCUIT_SPEC.GetControlFromPosition(icol, irow);

                                    if (ccontrol != null)
                                    {
                                        switch (ccontrol.ToString())
                                        {
                                            case "Micube.Framework.SmartControls.SmartSpinEdit": // 숫자에디터
                                            case "Micube.Framework.SmartControls.SmartTextBox": // 에디터
                                                if (ccontrol.Tag.ToString() != "")
                                                {
                                                    DataRow[] rowi = dtCircuit.Select("SEQUENCE = " + iSEQUENCE + "");
                                                    ccontrol.Text = rowi[0][ccontrol.Tag.ToString()].ToString();
                                                }
                                                break;
                                            case "Micube.Framework.SmartControls.SmartComboBox": //콤보
                                                Micube.Framework.SmartControls.SmartComboBox combox = new SmartComboBox();
                                                combox = (Micube.Framework.SmartControls.SmartComboBox)ccontrol;
                                                if (ccontrol.Tag.ToString() != "")
                                                {
                                                    DataRow[] rowi = dtCircuit.Select("SEQUENCE = " + iSEQUENCE + "");
                                                    combox.EditValue = rowi[0][ccontrol.Tag.ToString()].ToString();
                                                }
                                                break;
                                        }
                                    }
                                }

                                iSEQUENCE = iSEQUENCE + 1;

                            }
                        }
                       
                    }
                }

                // 영풍
                if (UserInfo.Current.Enterprise == "YOUNGPOONG")
                {
                    grdCIRCUIT.DataSource = dtCircuit;
                }


            }
        }

        //private void SspCustomerId_TextChanged(object sender, EventArgs e)
        //{

        //    Dictionary<string, object> ParamOLBCircuit = new Dictionary<string, object>();
        //    ParamOLBCircuit.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
        //    ParamOLBCircuit.Add("PLANTID", UserInfo.Current.Plant);
        //    ParamOLBCircuit.Add("CUSTOMERID", sspCustomerId.GetValue());

        //    DataTable dtCustomer = SqlExecuter.Query("GetCustomerList", "10002", ParamOLBCircuit);

        //    txtCustomerName.Text = dtCustomer.Rows[0]["CUSTOMERNAME"].ToString();
        //}

      
        #endregion

        #region  이벤트
        private void InitializeEvent()
        {
            sspCustomerId.Enabled = false;
            //sspCustomerId.Properties.ReadOnly = true;
            txtCustomerName.Properties.ReadOnly = true;
            cboProductType.Properties.ReadOnly = true;
            cboLayer.Properties.ReadOnly = true;

            btnSave.Click += BtnSave_Click;
            btnCancel.Click += BtnCancel_Click;

            btnSave1.Click += BtnSave_Click;
            btnCancel1.Click += BtnCancel_Click;

            grdInk.View.AddingNewRow += grdInk_AddingNewRow;
            grdCIRCUIT.View.AddingNewRow += grdCIRCUIT_AddingNewRow;
            ////btnYPE.Click += BtnYPE_Click;
            ////btnIFV.Click += BtnIFV_Click;
            ////btnIFC.Click += BtnIFC_Click;

            PLATING_STPl.CellPaint += PLATING_STPl_CellPaint;

            if (UserInfo.Current.Enterprise == "INTERFLEX")
            {
                //CIRCUIT_SPEC.CellPaint += CIRCUIT_SPEC_CellPaint;
            }

                Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add("ENTERPRISEID", _sENTERPRISEID);
            DataTable dtResult = SqlExecuter.Query("GetCalculationRule", "10001", dicParam);

            // 품목규칙에 등록된 계산식 컬럼 TextChanged 이벤트 생성
            foreach (DataRow row in dtResult.Select("CODEID <> ''"))
            {
                foreach (Control control in tableLayoutPanel1.Controls)
                {
                    if (control.ToString() == "Micube.Framework.SmartControls.SmartGroupBox")
                    {
                        foreach (Control controlc in control.Controls)
                        {
                            foreach (Control controlcc in controlc.Controls)
                            {
                                switch (controlcc.ToString())
                                {
                                    case "Micube.Framework.SmartControls.SmartSpinEdit":

                                        if (row["CODEID"].ToString() == controlcc.Tag.ToString())
                                        {
                                                    controlcc.TextChanged += TxtSizeAxis_TextChanged;

                                        }
                                        break;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void CIRCUIT_SPEC_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            switch (e.Row)
            {

                case 4:
                case 5:
                case 6:
                case 7:
                    e.Graphics.FillRectangle(Brushes.LightCyan, e.CellBounds);
                    break;
            }
        }

        private void PLATING_STPl_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            switch(e.Row)
            {
                case 0:
                case 1:
                    e.Graphics.FillRectangle(Brushes.LightCyan, e.CellBounds);
                    break;
                case 2:
                case 3:
                    e.Graphics.FillRectangle(Brushes.LightSkyBlue, e.CellBounds);
                    break;

                case 4:
                case 5:
                case 6:
                    e.Graphics.FillRectangle(Brushes.LightCyan, e.CellBounds);
                    break;
                case 7:
                case 8:
                case 9:
                    e.Graphics.FillRectangle(Brushes.LightSkyBlue, e.CellBounds);
                    break;

                case 10:
                case 11:
                case 12:
                    e.Graphics.FillRectangle(Brushes.LightCyan, e.CellBounds);
                    break;

            }
           
                
            
        }

        private void grdCIRCUIT_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {

            DataTable dt = (DataTable)grdCIRCUIT.DataSource;

            DataRow[] drmax = dt.Select("1=1", "SEQUENCE DESC");

            args.NewRow["SEQUENCE"] = 0;

            int iSEQUENCE = 0;
            if (drmax == null)
            {
                iSEQUENCE = 0;
            }
            else
            {
                iSEQUENCE = int.Parse(drmax[0]["SEQUENCE"].ToString()) + 1;
            }

            args.NewRow["ITEMID"] = txtItemCode.Text;
            args.NewRow["ITEMVERSION"] = txtItemver.Text;
            args.NewRow["SEQUENCE"] = iSEQUENCE;
            args.NewRow["DETAILTYPE"] = "Circuit";
            args.NewRow["VALIDSTATE"] = "Valid";
            args.NewRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
        }

        private void grdInk_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {

            DataTable dt = (DataTable)grdInk.DataSource;

            DataRow[] drmax = dt.Select("1=1", "SEQUENCE DESC");

            args.NewRow["SEQUENCE"] = 0;

            int iSEQUENCE = 0;
            if (drmax == null)
            {
                iSEQUENCE = 0;
            }
            else
            {
                iSEQUENCE = int.Parse(drmax[0]["SEQUENCE"].ToString()) + 1;
            }

            args.NewRow["ITEMID"] = txtItemCode.Text;
            args.NewRow["ITEMVERSION"] = txtItemver.Text;
            args.NewRow["SEQUENCE"] = iSEQUENCE;
            args.NewRow["DETAILTYPE"] = "InkSpecification";
            args.NewRow["VALIDSTATE"] = "Valid";
            args.NewRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
            
        }

        private void TxtSizeAxis_TextChanged(object sender, EventArgs e)
        {
            //fnCaluation();
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

                    if (control.ToString() == "Micube.Framework.SmartControls.SmartGroupBox")
                    {
                        foreach (Control controlc in control.Controls)
                        {
                            foreach (Control controlcc in controlc.Controls)
                            {
                                switch (controlcc.ToString())
                                {
                                    case "Micube.Framework.SmartControls.SmartSpinEdit":
                                        sAdd = sAdd.Replace(controlcc.Tag.ToString(), controlcc.Text);
                                        break;

                                }
                            }
                        }
                    }
                }


                foreach (Control control in tableLayoutPanel1.Controls)
                {
                    if (control.ToString() == "Micube.Framework.SmartControls.SmartGroupBox")
                    {
                        foreach (Control controlc in control.Controls)
                        {
                            foreach (Control controlcc in controlc.Controls)
                            {
                                switch (controlcc.ToString())
                                {
                                    case "Micube.Framework.SmartControls.SmartSpinEdit":
                                        if (controlcc.Tag.ToString() == row["TARGETATTRIBUTE"].ToString())
                                        {
                                            DataTable dtCalulation = new DataTable();
                                            controlcc.Text = dtCalulation.Compute(sAdd, "").ToString();
                                        }

                                        break;
                                }
                            }
                        }
                    }
                }
            }
        }


        private void InitializeGrid_ItemMasterPopup()
        {

            var parentItem = this.grdInk.View.AddSelectPopupColumn("ITEMID1", new SqlQuery("GetBomCompPopup", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}"))
            // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
            .SetPopupLayout("ITEMID1", PopupButtonStyles.Ok_Cancel, true, false)
            // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
            .SetPopupResultCount(1)
             // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)

             .SetPopupResultMapping("ITEMID1", "ITEMID")
            // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
            .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow)
            // 그리드의 남은 영역을 채울 컬럼 설정
            //.SetPopupAutoFillColumns("CODECLASSNAME")
            // Validation 이 필요한 경우 호출할 Method 지정
            .SetValidationIsRequired()
            .SetPopupApplySelection((selectedRows, dataGridRow) =>
            {
                // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
                // dataGridRow : 현재 Focus가 있는 그리드의 DataRow
                foreach (DataRow row in selectedRows)
                {
                    dataGridRow["ITEMNAME1"] = row["ITEMNAME"].ToString();
                    dataGridRow["SPEC"] = row["SPEC"].ToString();
                }
            });

            //.SetPopupValidationCustom(ValidationItemMasterPopup);

            // 팝업에서 사용할 조회조건 항목 추가
            parentItem.Conditions.AddTextBox("ITEMID");
            parentItem.Conditions.AddTextBox("ITEMVERSION");
            parentItem.Conditions.AddTextBox("ITEMNAME");
            
            // 팝업 그리드 설정
            parentItem.GridColumns.AddTextBoxColumn("ITEMID", 150);
            parentItem.GridColumns.AddTextBoxColumn("ITEMVERSION", 100);
            parentItem.GridColumns.AddTextBoxColumn("ITEMNAME", 250);
            parentItem.GridColumns.AddTextBoxColumn("SPEC", 150);
            
            parentItem.GridColumns.AddComboBoxColumn("UOMDEFID", 80, new SqlQuery("GetUOMList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "UOMDEFNAME", "UOMDEFID");

            //parentItem.GridColumns.AddTextBoxColumn("UOMDEFNAME", 250);

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
                    switch(control.Name)
                    {
                        case "groupboxspec":
                        case "groupItem":
                        case "groupdescriptionspec":
                            if (control.ToString() == "Micube.Framework.SmartControls.SmartGroupBox")
                            {
                                foreach (Control controlc in control.Controls)
                                {
                                    foreach (Control controlcc in controlc.Controls)
                                    {
                                        switch (controlcc.ToString())
                                        {
                                            case "Micube.Framework.SmartControls.SmartSpinEdit": // 숫자텍스트
                                            case "Micube.Framework.SmartControls.SmartTextBox":  // 텍스트
                                                if (dt.Columns.IndexOf(controlcc.Tag.ToString()) != -1)
                                                {
                                                    dt.Rows[0][controlcc.Tag.ToString()] = controlcc.Text;
                                                }


                                                break;

                                            case "Micube.Framework.SmartControls.SmartPanel":
                                                foreach (Control controlccc in controlcc.Controls)
                                                {
                                                    switch (controlccc.ToString())
                                                    {
                                                        case "Micube.Framework.SmartControls.SmartSpinEdit": // 숫자텍스트
                                                        case "Micube.Framework.SmartControls.SmartTextBox":  // 텍스트
                                                            dt.Rows[0][controlccc.Tag.ToString()] = controlccc.Text;
                                                            break;
                                                    }

                                                }
                                                break;


                                            case "Micube.Framework.SmartControls.SmartComboBox":  //콤보

                                                Micube.Framework.SmartControls.SmartComboBox combox = new SmartComboBox();
                                                combox = (Micube.Framework.SmartControls.SmartComboBox)controlcc;

                                                if (dt.Columns.IndexOf(controlcc.Tag.ToString()) != -1)
                                                {
                                                    if (combox.GetDataValue() != null)
                                                    {
                                                        dt.Rows[0][controlcc.Tag.ToString()] = combox.GetDataValue();
                                                    }
                                                    else
                                                    {

                                                        dt.Rows[0][controlcc.Tag.ToString()] = "";

                                                    }

                                                }

                                                break;

                                            case "Micube.Framework.SmartControls.SmartSelectPopupEdit": // 팝업
                                                Micube.Framework.SmartControls.SmartSelectPopupEdit SelectPopup = new SmartSelectPopupEdit();
                                                SelectPopup = (Micube.Framework.SmartControls.SmartSelectPopupEdit)controlcc;

                                                if (dt.Columns.IndexOf(controlcc.Tag.ToString()) != -1)
                                                {
                                                    dt.Rows[0][controlcc.Tag.ToString()] = SelectPopup.GetValue();
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
                                                ucPi = (Micube.SmartMES.StandardInfo.ucPcsImageid)controlcc;

                                                if (dt.Columns.IndexOf(ucPi.CODE.Tag.ToString()) != -1)
                                                {
                                                    dt.Rows[0][ucPi.CODE.Tag.ToString()] = ucPi.CODE.Text;
                                                }

                                                break;

                                            case "Micube.Framework.SmartControls.SmartDateEdit": // 날자
                                                if (dt.Columns.IndexOf(controlcc.Tag.ToString()) != -1)
                                                {
                                                    dt.Rows[0][controlcc.Tag.ToString()] = controlcc.Text;
                                                }
                                                break;
                                        }
                                    }
                                }
                            }
                            break;
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

                        if (control.ToString() == "Micube.Framework.SmartControls.SmartGroupBox")
                        {
                            foreach (Control controlc in control.Controls)
                            {
                                foreach (Control controlcc in controlc.Controls)
                                {
                                    switch (controlcc.ToString())
                                    {
                                        case "Micube.Framework.SmartControls.SmartSpinEdit":
                                        case "Micube.Framework.SmartControls.SmartTextBox":
                                            if (dt.Columns.IndexOf(controlcc.Tag.ToString()) != -1)
                                            {
                                                sAddChk = sAddChk.Replace(controlcc.Tag.ToString(), controlcc.Text);
                                            }


                                            break;
                                        case "Micube.Framework.SmartControls.SmartComboBox":

                                            Micube.Framework.SmartControls.SmartComboBox combox = new SmartComboBox();
                                            combox = (Micube.Framework.SmartControls.SmartComboBox)controlcc;

                                            if (dt.Columns.IndexOf(controlcc.Tag.ToString()) != -1)
                                            {
                                                if (combox.GetDataValue() != null)
                                                {
                                                    sAddChk = sAddChk.Replace(controlcc.Tag.ToString(), combox.GetDataValue().ToString());
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
                                            if (dt.Columns.IndexOf(controlcc.Tag.ToString()) != -1)
                                            {
                                                sAddChk = sAddChk.Replace(controlcc.Tag.ToString(), controlcc.Text);
                                            }
                                            break;
                                    }
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
                dtItemMaster.Columns.Add("LOTCONTROL");
                dtItemMaster.Columns.Add("_STATE_");


                



                  //foreach (DataRow row in dtResult.DefaultView.ToTable(true, new string[] { "TARGETATTRIBUTE" }).Rows)
                  //{
                  //    dtItemMaster.Columns.Add(row["TARGETATTRIBUTE"].ToString());

                  //    String sAdd = "";



                  //    foreach (DataRow rowAdd in dtResult.Select("TARGETATTRIBUTE = '" + row["TARGETATTRIBUTE"].ToString() + "'"))
                  //    {
                  //        sAdd = sAdd + rowAdd["CODEID"].ToString() + rowAdd["SEPARATORCODE"].ToString();
                  //    }


                  //    foreach (Control control in tableLayoutPanel1.Controls)
                  //    {

                  //        if (control.ToString() == "Micube.Framework.SmartControls.SmartGroupBox")
                  //        {
                  //            foreach (Control controlc in control.Controls)
                  //            {
                  //                foreach (Control controlcc in controlc.Controls)
                  //                {
                  //                    switch (controlcc.ToString())
                  //                    {
                  //                        case "Micube.Framework.SmartControls.SmartSpinEdit":
                  //                        case "Micube.Framework.SmartControls.SmartTextBox":
                  //                            if (dt.Columns.IndexOf(controlcc.Tag.ToString()) != -1)
                  //                            {
                  //                                sAdd = sAdd.Replace(controlcc.Tag.ToString(), controlcc.Text);
                  //                            }


                  //                            break;
                  //                        case "Micube.Framework.SmartControls.SmartComboBox":

                  //                            Micube.Framework.SmartControls.SmartComboBox combox = new SmartComboBox();
                  //                            combox = (Micube.Framework.SmartControls.SmartComboBox)controlcc;

                  //                            if (dt.Columns.IndexOf(controlcc.Tag.ToString()) != -1)
                  //                            {
                  //                                if (combox.GetDataValue() != null)
                  //                                {
                  //                                    sAdd = sAdd.Replace(controlcc.Tag.ToString(), combox.GetDataValue().ToString());
                  //                                }
                  //                            }

                  //                            break;

                  //                        case "Micube.SmartMES.StandardInfo.ucPublicPopup":

                  //                            //Micube.SmartMES.StandardInfo.ucPublicPopup ucCp = new ucPublicPopup();
                  //                            //ucCp = (Micube.SmartMES.StandardInfo.ucPublicPopup)controlc;

                  //                            //if (dt.Columns.IndexOf(ucCp.CODE.Tag.ToString()) != -1)
                  //                            //{
                  //                            //    sAdd = sAdd.Replace(ucCp.CODE.Tag.ToString(), controlc.Text);
                  //                            //}

                  //                            break;

                  //                        case "Micube.Framework.SmartControls.SmartDateEdit":
                  //                            if (dt.Columns.IndexOf(controlcc.Tag.ToString()) != -1)
                  //                            {
                  //                                sAdd = sAdd.Replace(controlcc.Tag.ToString(), controlcc.Text);
                  //                            }
                  //                            break;
                  //                    }
                  //                }
                  //            }
                  //        }
                  //    }

                  //    rowItemMaster[row["TARGETATTRIBUTE"].ToString()] = sAdd;

                  //    dtItemMaster.Rows.Add(rowItemMaster);
                  //}

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

                // 반제품 정보
                DataTable dtconsumabledefinition = new DataTable();
                dtconsumabledefinition.Columns.Add("CONSUMABLEDEFID");
                dtconsumabledefinition.Columns.Add("CONSUMABLEDEFVERSION");
                dtconsumabledefinition.Columns.Add("CONSUMABLECLASSID");
                dtconsumabledefinition.Columns.Add("CONSUMABLEDEFNAME");
                dtconsumabledefinition.Columns.Add("ENTERPRISEID");
                dtconsumabledefinition.Columns.Add("CONSUMABLETYPE");
                dtconsumabledefinition.Columns.Add("UNIT");
                dtconsumabledefinition.Columns.Add("DESCRIPTION");
                dtconsumabledefinition.Columns.Add("_STATE_");
                dtconsumabledefinition.Columns.Add("VALIDSTATE");
                dtconsumabledefinition.Columns.Add("ISLOTMNG");


                DataTable changed = new DataTable();

                grdProductItemSpec.View.CheckValidation();
                changed = grdProductItemSpec.GetChangedRows();


                foreach (DataRow row in changed.Rows)
                {

                    //if (row.RowState == DataRowState.Modified)
                    if (row["_STATE_"].ToString() == "modified")
                    {
                        DataRow rowItemMaster = dtItemMaster.NewRow();
                        rowItemMaster["ITEMID"] = _sITEMID;
                        rowItemMaster["ITEMVERSION"] = _sITEMVERSION;
                        rowItemMaster["MASTERDATACLASSID"] = _sMASTERDATACLASSID;
                        rowItemMaster["ENTERPRISEID"] = _sENTERPRISEID;
                        rowItemMaster["LOTCONTROL"] = row["LOTCONTROL"];
                        rowItemMaster["_STATE_"] = "modified";
                        dtItemMaster.Rows.Add(rowItemMaster);



                        if (_sMASTERDATACLASSID == "SubAssembly")
                        {
                            //반제품 정보 수정

                            DataRow rowconsumabledefinition = dtconsumabledefinition.NewRow();
                            rowconsumabledefinition["CONSUMABLEDEFID"] = row["ITEMID"];
                            rowconsumabledefinition["CONSUMABLEDEFVERSION"] = row["ITEMVERSION"];
                            //rowconsumabledefinition["CONSUMABLECLASSID"] = _sMASTERDATACLASSID;
                            //rowconsumabledefinition["CONSUMABLEDEFNAME"] = row["ITEMNAME"];
                            //rowconsumabledefinition["ENTERPRISEID"] = row["ENTERPRISEID"];
                            //rowconsumabledefinition["CONSUMABLETYPE"] = row["CONSUMABLETYPE"];
                            //rowconsumabledefinition["UNIT"] = row["ITEMUOM"];
                            //rowconsumabledefinition["DESCRIPTION"] = row["DESCRIPTION"];
                            //rowconsumabledefinition["_STATE_"] = "modified";
                            rowconsumabledefinition["ISLOTMNG"] = row["LOTCONTROL"];
                            rowconsumabledefinition["VALIDSTATE"] = "Valid";
                            dtconsumabledefinition.Rows.Add(rowconsumabledefinition);

                        }


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

                        ////// 도금사양
                        ////DataTable dtSurfacePlating = new DataTable();
                        ////dtSurfacePlating.Columns.Add("ITEMID");
                        ////dtSurfacePlating.Columns.Add("ITEMVERSION");
                        ////dtSurfacePlating.Columns.Add("SEQUENCE");
                        ////dtSurfacePlating.Columns.Add("DETAILTYPE");
                        ////dtSurfacePlating.Columns.Add("ENTERPRISEID");
                        ////dtSurfacePlating.Columns.Add("PLANTID");
                        ////dtSurfacePlating.Columns.Add("DETAILCLASS");
                        ////dtSurfacePlating.Columns.Add("DETAILNAME");
                        ////dtSurfacePlating.Columns.Add("SPECDETAILFROM");
                        ////dtSurfacePlating.Columns.Add("SPECDETAILTO");
                        ////dtSurfacePlating.Columns.Add("FROMORIGINAL");
                        ////dtSurfacePlating.Columns.Add("TOORIGINAL");
                        ////dtSurfacePlating.Columns.Add("DESCRIPTION");
                        ////dtSurfacePlating.Columns.Add("VALIDSTATE");
                        ////dtSurfacePlating.Columns.Add("_STATE_");


                        

                        ////Dictionary<string, object> paramSurfacePlating = new Dictionary<string, object>();
                        ////paramSurfacePlating.Add("ITEMID", row["ITEMID"].ToString());
                        ////paramSurfacePlating.Add("ITEMVERSION", row["ITEMVERSION"].ToString());
                        ////paramSurfacePlating.Add("ENTERPRISEID", row["ENTERPRISEID"].ToString());
                        ////paramSurfacePlating.Add("DETAILTYPE", "SurfacePlating");
                        ////DataTable dtSurfacePlatingChk = SqlExecuter.Query("GetProductItemSpecDetail", "10001", paramSurfacePlating);




                        ////DataRow rowSurfacePlating = dtSurfacePlating.NewRow();
                        ////rowSurfacePlating["ITEMID"] = row["ITEMID"];
                        ////rowSurfacePlating["ITEMVERSION"] = row["ITEMVERSION"];
                        ////rowSurfacePlating["SEQUENCE"] = 1;
                        ////rowSurfacePlating["DETAILTYPE"] = "SurfacePlating";
                        ////rowSurfacePlating["ENTERPRISEID"] = row["ENTERPRISEID"];
                        ////rowSurfacePlating["PLANTID"] = row["PLANTID"];
                        ////rowSurfacePlating["VALIDSTATE"] = "Valid";

                    
                        ////if (dtSurfacePlatingChk != null)
                        ////{
                        ////    if(dtSurfacePlatingChk.Rows.Count != 0)
                        ////    {
                        ////        rowSurfacePlating["_STATE_"] = "modified";
                        ////    }
                        ////    else
                        ////    {
                        ////        rowSurfacePlating["_STATE_"] = "added";
                        ////    }
                        ////}
                        ////else
                        ////{
                        ////    rowSurfacePlating["_STATE_"] = "added";
                        ////}

                        

                        ////foreach (Control controlc in GBPLATINGSPECINFO.Controls)
                        ////{

                              
                        ////    foreach (Control controlcc in controlc.Controls)
                        ////    {
                        ////        switch (controlcc.ToString())
                        ////        {
                        ////            case "Micube.Framework.SmartControls.SmartSpinEdit": // 숫자텍스트
                        ////            case "Micube.Framework.SmartControls.SmartTextBox":  // 텍스트
                        ////                if (dtSurfacePlating.Columns.IndexOf(controlcc.Tag.ToString()) != -1)
                        ////                {
                        ////                    int irow =  PLATING_STPl.GetRow(controlcc);
                        ////                    rowSurfacePlating[controlcc.Tag.ToString()] = controlcc.Text;
                        ////                }
                        ////               break;
                        ////            case "Micube.Framework.SmartControls.SmartLabel":  // 라벨
                        ////                if (dtSurfacePlating.Columns.IndexOf(controlcc.Tag.ToString()) != -1)
                        ////                {
                        ////                    rowSurfacePlating[controlcc.Tag.ToString()] = controlcc.Name;
                        ////                }
                        ////                break;

                                  
                        ////        }
                        ////    }
                        ////}

                        ////dtSurfacePlating.Rows.Add(rowSurfacePlating);


                        ////// 회로사양
                        ////DataTable dtCIRCUIT = new DataTable();
                        ////dtCIRCUIT.Columns.Add("ITEMID");
                        ////dtCIRCUIT.Columns.Add("ITEMVERSION");
                        ////dtCIRCUIT.Columns.Add("SEQUENCE");
                        ////dtCIRCUIT.Columns.Add("DETAILTYPE");
                        ////dtCIRCUIT.Columns.Add("ENTERPRISEID");
                        ////dtCIRCUIT.Columns.Add("PLANTID");
                        ////dtCIRCUIT.Columns.Add("DETAILCLASS");
                        ////dtCIRCUIT.Columns.Add("DETAILNAME");
                        ////dtCIRCUIT.Columns.Add("SPECDETAILFROM");
                        ////dtCIRCUIT.Columns.Add("SPECDETAILTO");
                        ////dtCIRCUIT.Columns.Add("FROMORIGINAL");
                        ////dtCIRCUIT.Columns.Add("TOORIGINAL");
                        ////dtCIRCUIT.Columns.Add("DESCRIPTION");
                        ////dtCIRCUIT.Columns.Add("VALIDSTATE");
                        ////dtCIRCUIT.Columns.Add("_STATE_");

                        ////DataRow rowCIRCUIT = dtCIRCUIT.NewRow();
                        ////rowCIRCUIT["ITEMID"] = row["ITEMID"];
                        ////rowCIRCUIT["ITEMVERSION"] = row["ITEMVERSION"];
                        ////rowCIRCUIT["SEQUENCE"] = 1;
                        ////rowCIRCUIT["DETAILTYPE"] = "CIRCUIT";
                        ////rowCIRCUIT["ENTERPRISEID"] = row["ENTERPRISEID"];
                        ////rowCIRCUIT["PLANTID"] = row["PLANTID"];
                        ////rowCIRCUIT["VALIDSTATE"] = "Valid";

                        ////foreach (Control controlc in GBPLATINGSPECINFO.Controls)
                        ////{
                        ////    foreach (Control controlcc in controlc.Controls)
                        ////    {
                        ////        switch (controlcc.ToString())
                        ////        {
                        ////            case "Micube.Framework.SmartControls.SmartSpinEdit": // 숫자텍스트
                        ////            case "Micube.Framework.SmartControls.SmartTextBox":  // 텍스트
                        ////                if (dtCIRCUIT.Columns.IndexOf(controlcc.Tag.ToString()) != -1)
                        ////                {
                        ////                    rowCIRCUIT[controlcc.Tag.ToString()] = controlcc.Text;
                        ////                }
                        ////                break;
                        ////            case "Micube.Framework.SmartControls.SmartLabel":  // 라벨
                        ////                if (dtCIRCUIT.Columns.IndexOf(controlcc.Tag.ToString()) != -1)
                        ////                {
                        ////                    rowCIRCUIT[controlcc.Tag.ToString()] = controlcc.Name;
                        ////                }
                        ////                break;


                        ////        }
                        ////    }
                        ////}

                        ////dtCIRCUIT.Rows.Add(rowCIRCUIT);



                    }
                }

                DataTable dtproductitemspecdetail = new DataTable();

               


                    dtproductitemspecdetail.Columns.Add("ITEMID");
                    dtproductitemspecdetail.Columns.Add("ITEMVERSION");
                    dtproductitemspecdetail.Columns.Add("SEQUENCE", typeof(decimal));
                    dtproductitemspecdetail.Columns.Add("DETAILTYPE");
                    dtproductitemspecdetail.Columns.Add("ENTERPRISEID");
                    //dtproductitemspecdetail.Columns.Add("PLANTID");
                    //dtproductitemspecdetail.Columns.Add("DETAILCLASS");
                    //dtproductitemspecdetail.Columns.Add("DETAILNAME");
                    dtproductitemspecdetail.Columns.Add("SPECDETAILFROM");
                    dtproductitemspecdetail.Columns.Add("SPECDETAILTO");
                    dtproductitemspecdetail.Columns.Add("FROMORIGINAL");
                    dtproductitemspecdetail.Columns.Add("TOORIGINAL");
                    // dtproductitemspecdetail.Columns.Add("DESCRIPTION");
                    dtproductitemspecdetail.Columns.Add("VALIDSTATE");
                    dtproductitemspecdetail.Columns.Add("_STATE_");


                    Dictionary<string, object> paramSurfacePlating = new Dictionary<string, object>();
                    paramSurfacePlating.Add("ITEMID", txtItemCode.Text);
                    paramSurfacePlating.Add("ITEMVERSION", txtItemver.Text);
                    paramSurfacePlating.Add("ENTERPRISEID", UserInfo.Current.Enterprise);

                    DataTable dtSurfacePlatingChk = SqlExecuter.Query("GetProductItemSpecDetail", "10001", paramSurfacePlating);

                    //////////
                    // 도금사양
                    int iSEQUENCE = 0;
                    for (int irow = 0; irow <= 13; irow++)
                    {

                        DataRow rowNew = dtproductitemspecdetail.NewRow();
                        rowNew["ITEMID"] = txtItemCode.Text;
                        rowNew["ITEMVERSION"] = txtItemver.Text;
                        rowNew["SEQUENCE"] = iSEQUENCE;
                        rowNew["DETAILTYPE"] = "SurfacePlating";
                        rowNew["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                        rowNew["VALIDSTATE"] = "Valid";

                        if (dtSurfacePlatingChk != null)
                        {
                            if (dtSurfacePlatingChk.Select("ITEMID = '"+ txtItemCode.Text + "' AND ITEMVERSION = '"+ txtItemver.Text + "' AND SEQUENCE = "+ iSEQUENCE + " AND DETAILTYPE = 'SurfacePlating' AND ENTERPRISEID = '"+ UserInfo.Current.Enterprise + "' ").Length != 0)
                            {
                                rowNew["_STATE_"] = "modified";
                            }
                            else
                            {
                                rowNew["_STATE_"] = "added";
                            }
                        }
                        else
                        {
                            rowNew["_STATE_"] = "added";
                        }

                        iSEQUENCE = iSEQUENCE + 1;


                        if (irow <= 3)
                        {
                            for (int icol = 2; icol <= 3; icol++)
                            {

                                Control ccontrol = this.PLATING_STPl.GetControlFromPosition(icol, irow);

                                if (ccontrol != null)
                                {
                                    switch (ccontrol.ToString())
                                    {
                                        case "Micube.Framework.SmartControls.SmartSpinEdit": // 숫자에디터
                                        case "Micube.Framework.SmartControls.SmartTextBox": // 에디터
                                            if (ccontrol.Tag.ToString() != "")
                                            {
                                                rowNew[ccontrol.Tag.ToString()] = ccontrol.Text;
                                            }
                                            break;
                                        case "Micube.Framework.SmartControls.SmartComboBox": //콤보
                                            Micube.Framework.SmartControls.SmartComboBox combox = new SmartComboBox();
                                            combox = (Micube.Framework.SmartControls.SmartComboBox)ccontrol;
                                            if (ccontrol.Tag.ToString() != "")
                                            {
                                                if (combox.GetDataValue() != null)
                                                {
                                                    if (rowNew[ccontrol.Tag.ToString()].ToString() != combox.GetDataValue().ToString())
                                                    {
                                                        rowNew[ccontrol.Tag.ToString()] = combox.GetDataValue();
                                                    }
                                                }
                                            }
                                            break;

                                            //case "Micube.Framework.SmartControls.SmartLabel":
                                            //    if (ccontrol.Tag.ToString() != "")
                                            //    {
                                            //        rowNew[ccontrol.Tag.ToString()] = ccontrol.Name;
                                            //    }
                                            //    break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            for (int icol = 2; icol <= 6; icol++)
                            {

                                Control ccontrol = this.PLATING_STPl.GetControlFromPosition(icol, irow);

                                if (ccontrol != null)
                                {
                                    switch (ccontrol.ToString())
                                    {
                                        case "Micube.Framework.SmartControls.SmartSpinEdit": // 숫자에디터
                                        case "Micube.Framework.SmartControls.SmartTextBox": // 에디터
                                            if (ccontrol.Tag.ToString() != "")
                                            {
                                                rowNew[ccontrol.Tag.ToString()] = ccontrol.Text;
                                            }
                                            break;
                                        case "Micube.Framework.SmartControls.SmartComboBox": //콤보
                                            Micube.Framework.SmartControls.SmartComboBox combox = new SmartComboBox();
                                            combox = (Micube.Framework.SmartControls.SmartComboBox)ccontrol;
                                            if (ccontrol.Tag.ToString() != "")
                                            {
                                                if (combox.GetDataValue() != null)
                                                {
                                                    //if (rowNew[ccontrol.Tag.ToString()].ToString() != combox.GetDataValue().ToString())
                                                    //{
                                                    rowNew[ccontrol.Tag.ToString()] = combox.GetDataValue();
                                                    //}
                                                }
                                            }
                                            break;
                                        case "Micube.Framework.SmartControls.SmartPanel":
                                            foreach (Control controlccc in ccontrol.Controls)
                                            {
                                                switch (controlccc.ToString())
                                                {
                                                    case "Micube.Framework.SmartControls.SmartSpinEdit": // 숫자텍스트
                                                    case "Micube.Framework.SmartControls.SmartTextBox":  // 텍스트
                                                        rowNew[controlccc.Tag.ToString()] = controlccc.Text;
                                                        break;
                                                }

                                            }
                                            break;


                                    }
                                }
                            }
                        }



                        dtproductitemspecdetail.Rows.Add(rowNew);
                    }



                    for (int irow = 0; irow <= 12; irow++)
                    {

                        DataRow rowNew = dtproductitemspecdetail.NewRow();
                        rowNew["ITEMID"] = txtItemCode.Text;
                        rowNew["ITEMVERSION"] = txtItemver.Text;
                        rowNew["SEQUENCE"] = iSEQUENCE;
                        rowNew["DETAILTYPE"] = "SurfacePlating";
                        rowNew["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                        rowNew["VALIDSTATE"] = "Valid";
                        if (dtSurfacePlatingChk != null)
                        {
                            if (dtSurfacePlatingChk.Select("ITEMID = '" + txtItemCode.Text + "' AND ITEMVERSION = '" + txtItemver.Text + "' AND SEQUENCE = " + iSEQUENCE + " AND DETAILTYPE = 'SurfacePlating' AND ENTERPRISEID = '" + UserInfo.Current.Enterprise + "' ").Length != 0)
                            {
                                rowNew["_STATE_"] = "modified";
                            }
                            else
                            {
                                rowNew["_STATE_"] = "added";
                            }
                        }
                        else
                        {
                            rowNew["_STATE_"] = "added";
                        }

                        iSEQUENCE = iSEQUENCE + 1;


                        if (irow <= 3)
                        {
                            for (int icol = 9; icol <= 10; icol++)
                            {
                                Control ccontrol = this.PLATING_STPl.GetControlFromPosition(icol, irow);

                                if (ccontrol != null)
                                {
                                    switch (ccontrol.ToString())
                                    {
                                        case "Micube.Framework.SmartControls.SmartSpinEdit": // 숫자에디터
                                        case "Micube.Framework.SmartControls.SmartTextBox": // 에디터
                                            if (ccontrol.Tag.ToString() != "")
                                            {
                                                rowNew[ccontrol.Tag.ToString()] = ccontrol.Text;
                                            }
                                            break;
                                        case "Micube.Framework.SmartControls.SmartComboBox": //콤보
                                            Micube.Framework.SmartControls.SmartComboBox combox = new SmartComboBox();
                                            combox = (Micube.Framework.SmartControls.SmartComboBox)ccontrol;
                                            if (ccontrol.Tag.ToString() != "")
                                            {
                                                if (combox.GetDataValue() != null)
                                                {
                                                    //if (rowNew[ccontrol.Tag.ToString()].ToString() != combox.GetDataValue().ToString())
                                                    //{
                                                    rowNew[ccontrol.Tag.ToString()] = combox.GetDataValue();
                                                    //}
                                                }
                                            }
                                            break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            for (int icol = 9; icol <= 14; icol++)
                            {
                                Control ccontrol = this.PLATING_STPl.GetControlFromPosition(icol, irow);

                                if (ccontrol != null)
                                {
                                    switch (ccontrol.ToString())
                                    {
                                        case "Micube.Framework.SmartControls.SmartSpinEdit": // 숫자에디터
                                        case "Micube.Framework.SmartControls.SmartTextBox": // 에디터
                                            if (ccontrol.Tag.ToString() != "")
                                            {
                                                rowNew[ccontrol.Tag.ToString()] = ccontrol.Text;
                                            }
                                            break;
                                        case "Micube.Framework.SmartControls.SmartComboBox": //콤보
                                            Micube.Framework.SmartControls.SmartComboBox combox = new SmartComboBox();
                                            combox = (Micube.Framework.SmartControls.SmartComboBox)ccontrol;
                                            if (ccontrol.Tag.ToString() != "")
                                            {
                                                if (combox.GetDataValue() != null)
                                                {
                                                    //if (rowNew[ccontrol.Tag.ToString()].ToString() != combox.GetDataValue().ToString())
                                                    //{
                                                    rowNew[ccontrol.Tag.ToString()] = combox.GetDataValue();
                                                    //}
                                                }
                                            }
                                            break;
                                    }
                                }
                            }
                        }

                        dtproductitemspecdetail.Rows.Add(rowNew);
                    }

                ///////////
                // 영풍 회로사양
                if (UserInfo.Current.Enterprise == "YOUNGPOONG")
                {

                    DataTable dtCIRCUIT = grdCIRCUIT.GetChangedRows();

                    foreach (DataRow rowCIRCUIT in dtCIRCUIT.Rows)
                    {
                        rowCIRCUIT["SPECDETAILFROM"] = rowCIRCUIT["CIRCUITSPEC"];
                        //rowink["SPECDETAILTO"] = rowink["ITEMNAME1"];
                        rowCIRCUIT["SPECDETAILTO"] = rowCIRCUIT["CIRCUITSPECVALUE"];
                    }

                    dtproductitemspecdetail.Merge(dtCIRCUIT);

                }

                ///////////
                // 인터플랙스 회로사양

                if (UserInfo.Current.Enterprise == "INTERFLEX")
                {

                    Dictionary<string, object> paramCircuit = new Dictionary<string, object>();
                    paramCircuit.Add("ITEMID", txtItemCode.Text);
                    paramCircuit.Add("ITEMVERSION", txtItemver.Text);
                    paramCircuit.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                    paramCircuit.Add("DETAILTYPE", "Circuit");
                    DataTable dtCircuit = SqlExecuter.Query("GetProductItemSpecDetail", "10001", paramCircuit);

                    iSEQUENCE = 1;
                    for (int irow = 1; irow <= 12; irow++)
                    {

                        DataRow rowNew = dtproductitemspecdetail.NewRow();
                        rowNew["ITEMID"] = txtItemCode.Text;
                        rowNew["ITEMVERSION"] = txtItemver.Text;
                        rowNew["SEQUENCE"] = iSEQUENCE;

                        rowNew["DETAILTYPE"] = "Circuit";
                        rowNew["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                        rowNew["VALIDSTATE"] = "Valid";
                        if (dtCircuit != null)
                        {
                            if (dtCircuit.Select("ITEMID = '" + txtItemCode.Text + "' AND ITEMVERSION = '" + txtItemver.Text + "' AND SEQUENCE = " + iSEQUENCE + "").Length != 0)
                            {
                                rowNew["_STATE_"] = "modified";
                            }
                            else
                            {
                                rowNew["_STATE_"] = "added";
                            }
                        }
                        else
                        {
                            rowNew["_STATE_"] = "added";
                        }

                        iSEQUENCE = iSEQUENCE + 1;


                        for (int icol = 1; icol <= 4; icol++)
                        {

                            Control ccontrol = this.CIRCUIT_SPEC.GetControlFromPosition(icol, irow);

                            if (ccontrol != null)
                            {
                                switch (ccontrol.ToString())
                                {
                                    case "Micube.Framework.SmartControls.SmartSpinEdit": // 숫자에디터
                                    case "Micube.Framework.SmartControls.SmartTextBox": // 에디터
                                        if (ccontrol.Tag.ToString() != "")
                                        {
                                            rowNew[ccontrol.Tag.ToString()] = ccontrol.Text;
                                        }
                                        break;
                                    case "Micube.Framework.SmartControls.SmartComboBox": //콤보
                                        Micube.Framework.SmartControls.SmartComboBox combox = new SmartComboBox();
                                        combox = (Micube.Framework.SmartControls.SmartComboBox)ccontrol;
                                        if (ccontrol.Tag.ToString() != "")
                                        {
                                            if (combox.GetDataValue() != null)
                                            {
                                                //if (rowNew[ccontrol.Tag.ToString()].ToString() != combox.GetDataValue().ToString())
                                                //{
                                                rowNew[ccontrol.Tag.ToString()] = combox.GetDataValue();
                                                //}
                                            }
                                        }
                                        break;
                                }
                            }
                        }

                        dtproductitemspecdetail.Rows.Add(rowNew);
                    }



                    for (int irow = 1; irow <= 12; irow++)
                    {

                        DataRow rowNew = dtproductitemspecdetail.NewRow();
                        rowNew["ITEMID"] = txtItemCode.Text;
                        rowNew["ITEMVERSION"] = txtItemver.Text;
                        rowNew["SEQUENCE"] = iSEQUENCE;
                        iSEQUENCE = iSEQUENCE + 1;
                        rowNew["DETAILTYPE"] = "Circuit";
                        rowNew["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                        rowNew["VALIDSTATE"] = "Valid";
                        if (dtSurfacePlatingChk != null)
                        {
                            if (dtSurfacePlatingChk.Rows.Count != 0)
                            {
                                rowNew["_STATE_"] = "modified";
                            }
                            else
                            {
                                rowNew["_STATE_"] = "added";
                            }
                        }
                        else
                        {
                            rowNew["_STATE_"] = "added";
                        }

                        for (int icol = 5; icol <= 6; icol++)
                        {

                            Control ccontrol = this.CIRCUIT_SPEC.GetControlFromPosition(icol, irow);

                            if (ccontrol != null)
                            {
                                switch (ccontrol.ToString())
                                {
                                    case "Micube.Framework.SmartControls.SmartSpinEdit": // 숫자에디터
                                    case "Micube.Framework.SmartControls.SmartTextBox": // 에디터
                                        if (ccontrol.Tag.ToString() != "")
                                        {
                                            rowNew[ccontrol.Tag.ToString()] = ccontrol.Text;
                                        }
                                        break;
                                    case "Micube.Framework.SmartControls.SmartComboBox": //콤보
                                        Micube.Framework.SmartControls.SmartComboBox combox = new SmartComboBox();
                                        combox = (Micube.Framework.SmartControls.SmartComboBox)ccontrol;
                                        if (ccontrol.Tag.ToString() != "")
                                        {
                                            if (combox.GetDataValue() != null)
                                            {
                                                //if (rowNew[ccontrol.Tag.ToString()].ToString() != combox.GetDataValue().ToString())
                                                //{
                                                rowNew[ccontrol.Tag.ToString()] = combox.GetDataValue();
                                                //}
                                            }
                                        }
                                        break;
                                }
                            }
                        }

                        dtproductitemspecdetail.Rows.Add(rowNew);
                    }

                }
                ///////// 회로사양 끝

                DataTable dtink =  grdInk.GetChangedRows();

                foreach(DataRow rowink in dtink.Rows)
                {
                    rowink["SPECDETAILFROM"] = rowink["ITEMID1"];
                    //rowink["SPECDETAILTO"] = rowink["ITEMNAME1"];
                    rowink["FROMORIGINAL"] = rowink["USERLAYER"];
                }


                dtproductitemspecdetail.TableName = "productitemspecdetail";
                dtproductitemspecdetail.Merge(dtink);

                // 자재 그룹 정보
                dtconsumabledefinition.TableName = "consumabledefinition";

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
                    ,{ "productitemspecdetail", dtproductitemspecdetail }
                    ,{ "consumabledefinition", dtconsumabledefinition }
                });

                saveWorker.Execute();

                MSGBox.Show(MessageBoxType.Information, "SuccedSave");

                dt.AcceptChanges();

                // 인크사양 삭제시 조회 
                if (dtproductitemspecdetail.Select("_STATE_ = 'deleted'").Length !=0)
                {
                    Dictionary<string, object> paramInkSpecification = new Dictionary<string, object>();
                    paramInkSpecification.Add("ITEMID", txtItemCode.Text);
                    paramInkSpecification.Add("ITEMVERSION", txtItemver.Text);
                    paramInkSpecification.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                    paramInkSpecification.Add("DETAILTYPE", "InkSpecification");
                    DataTable dtInkSpecification = SqlExecuter.Query("GetProductItemSpecDetail", "10001", paramInkSpecification);
                    grdInk.DataSource = dtInkSpecification;
                }
               

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
