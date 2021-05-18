#region using

using Micube.Framework;
using Micube.Framework.Net;
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

#endregion

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프 로 그 램 명  : 기준정보 > 사양 실적 관리 > CAM 요청이력 조회
    /// 업  무  설  명  : CAM 요청이력을 조회한다
    /// 생    성    자  : 한주석
    /// 생    성    일  : 2019-12-17
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class CamRequestHistory : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가
        string _Text;                                       // 설명
        SmartBandedGrid grdList = new SmartBandedGrid();    // Main Grid

        #endregion

        #region 생성자

        public CamRequestHistory()
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
            grdcamlist.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdcamlist.GridButtonItem -= GridButtonItem.Delete;
            grdcamlist.GridButtonItem -= GridButtonItem.Add;

            grdcamlist.View.SetIsReadOnly();
            grdcamlist.View.AddTextBoxColumn("PLANTID", 80)
                .SetIsHidden();
            // 접수번호
            grdcamlist.View.AddTextBoxColumn("CAMREQUESTID", 105)
                .SetLabel("REGISTERNUMBER")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            // 고객사
            grdcamlist.View.AddTextBoxColumn("CUSTOMERID", 120)
                .SetLabel("COMPANYCLIENT");
            // 품목코드(변경전)
            grdcamlist.View.AddTextBoxColumn("ITEMID", 120)
                .SetLabel("PREVITEMID");
            // 품목REV.(변경전)
            grdcamlist.View.AddTextBoxColumn("ITEMVERSION", 130)
                .SetLabel("PREVITEMREV")
                .SetTextAlignment(TextAlignment.Center);
            // 품목명(변경전)
            grdcamlist.View.AddTextBoxColumn("ITEMNAME", 200)
                .SetLabel("PREVITEMNAME");
            // 품목코드(변경후)
            grdcamlist.View.AddTextBoxColumn("RCITEMID", 120)
                .SetLabel("AFTERITEMID");
            // 품목REV(변경후)
            grdcamlist.View.AddTextBoxColumn("RCITEMVERSION", 130)
                .SetLabel("AFTERITEMREV")
                .SetTextAlignment(TextAlignment.Center);
            // 품목명(변경후)
            grdcamlist.View.AddTextBoxColumn("RCITEMNAME", 200)
                .SetLabel("AFTERITEMNAME");
            // 작업구분
            grdcamlist.View.AddTextBoxColumn("JOBTYPE", 80)
                .SetTextAlignment(TextAlignment.Center);
            // 진행구분
            grdcamlist.View.AddTextBoxColumn("PROCESSTYPE", 80)
                .SetLabel("PROGRESSTYPE")
                .SetTextAlignment(TextAlignment.Center);
            // PNL SIZE Y
            grdcamlist.View.AddTextBoxColumn("PANELSIZE_Y", 100)
                .SetLabel("PNLSIZEY");
            // 모델납기일
            grdcamlist.View.AddTextBoxColumn("MODELDELIVERYDATE", 150)
                .SetLabel("MODELDELIVERYDAY");
            // 사양담당
            grdcamlist.View.AddTextBoxColumn("SPECOWNER", 80)
                .SetLabel("SPECPERSON")
                .SetTextAlignment(TextAlignment.Center);
            // CAM담당
            grdcamlist.View.AddTextBoxColumn("CAMOWNER", 80)
                .SetLabel("CAMMAN")
                .SetTextAlignment(TextAlignment.Center);
            // 제품 TYPE
            grdcamlist.View.AddTextBoxColumn("PRODUCTTYPE", 100)
                .SetLabel("PRODUCTSHAPE")
                .SetTextAlignment(TextAlignment.Center);
            // 기존 고객사 REV
            grdcamlist.View.AddTextBoxColumn("PREVCUSTOMERVERSION", 130)
                .SetLabel("EXISTCLIENTREV");
            // 적용 고객사 REV
            grdcamlist.View.AddTextBoxColumn("APPLYCUSTOMERVERSION", 130)
                .SetLabel("APPLYCLIENTREV");
            // 회로보정지 내층
            grdcamlist.View.AddTextBoxColumn("INNERLAYERCORRECTION", 120);
            // 회로보정지 외층
            grdcamlist.View.AddTextBoxColumn("OUTERLAYERCORRECTION", 120);
            // OLB 보정치
            grdcamlist.View.AddTextBoxColumn("OLBCORRECTION", 120);
            // 쏠림동박적용
            grdcamlist.View.AddTextBoxColumn("APPLYCOPPERFOIL", 120)
                .SetLabel("APPLYINGCOPPERFOIL");
            // 적용층
            grdcamlist.View.AddTextBoxColumn("APPLYLAYER", 80)
                .SetLabel("APPLIEDLAYER");

            grdcamlist.View.AddTextBoxColumn("APPLYLAYERCORRECTION", 100);


            // ZIP 단자
            grdcamlist.View.AddTextBoxColumn("ZIPTERMINAL", 120);
            // PAD보정
            grdcamlist.View.AddTextBoxColumn("PADCORRECTION", 120);
            // BBT 기준
            grdcamlist.View.AddTextBoxColumn("BBTSTANDARD", 120);
            // 제작처
            grdcamlist.View.AddTextBoxColumn("MANUFACTURER", 120);
            // 입고처
            grdcamlist.View.AddTextBoxColumn("RECEIVER", 120)
                .SetLabel("RECEIPTDEPT");
            // 연신율
            grdcamlist.View.AddTextBoxColumn("ELONGATION", 80);
            // 구분
            grdcamlist.View.AddTextBoxColumn("BBTTYPE", 80);
            // JIG 제작
            grdcamlist.View.AddTextBoxColumn("JIGPRODUCTION", 100);
            // 단자구분
            grdcamlist.View.AddTextBoxColumn("TERMINALTYPE", 100);
            // 방식
            grdcamlist.View.AddTextBoxColumn("BBTMETHOD", 100);
            // 납기일
            grdcamlist.View.AddTextBoxColumn("BBTDELIVERYDATE", 100);
            // MICROSHORT
            grdcamlist.View.AddTextBoxColumn("MICROSHORTREQUEST", 115)
                .SetLabel("MICROSHORT");
            // 표면도금1
            grdcamlist.View.AddTextBoxColumn("SURFACEPLATING1", 100);
            // 표면도금2
            grdcamlist.View.AddTextBoxColumn("SURFACEPLATING2", 100);
            // 표면도금3
            grdcamlist.View.AddTextBoxColumn("SURFACEPLATING3", 100);
            // M/K표기
            grdcamlist.View.AddTextBoxColumn("MKNOTATION", 100);
            // 사양 주차표기
            grdcamlist.View.AddTextBoxColumn("SPECWEEK", 100);
            // CAM 주차표기
            grdcamlist.View.AddTextBoxColumn("CAMWEEK", 100);
            // Guide 기준
            grdcamlist.View.AddTextBoxColumn("GUIDESTANDARD", 100);
            // 도금선 연결층
            grdcamlist.View.AddTextBoxColumn("PLATINGCONNECTINGLAYER", 100);
            // 외곽 Guide
            grdcamlist.View.AddTextBoxColumn("OUTERGUIDE", 100);
            // Vision Press 기준
            grdcamlist.View.AddTextBoxColumn("VISIONPRESS", 120);
            // Trimming 인식마크
            grdcamlist.View.AddTextBoxColumn("TRIMMINGMARK", 130);
            // 등록일
            grdcamlist.View.AddTextBoxColumn("CREATEDTIME", 200)
                .SetLabel("WRITEDATE")
                .SetTextAlignment(TextAlignment.Center);

            grdcamlist.View.AddTextBoxColumn("DRILLSPEC", 50)
                .SetIsHidden();
            grdcamlist.View.AddTextBoxColumn("OUTERLAYERSPEC", 50)
                .SetIsHidden();
            grdcamlist.View.AddTextBoxColumn("INNERLAYERSPEC", 50)
                .SetIsHidden();
            grdcamlist.View.AddTextBoxColumn("COVERLAYSPEC", 50)
                .SetIsHidden();
            grdcamlist.View.AddTextBoxColumn("PSRSPEC", 50)
                .SetIsHidden();
            grdcamlist.View.AddTextBoxColumn("LAYERADHESION", 50)
                .SetIsHidden();
            grdcamlist.View.AddTextBoxColumn("CUTLASERSPEC", 50)
                .SetIsHidden();
            grdcamlist.View.AddTextBoxColumn("PEELRSTSPEC", 50)
                .SetIsHidden();
            grdcamlist.View.AddTextBoxColumn("ROUTERSPEC", 50)
                .SetIsHidden();
            grdcamlist.View.AddTextBoxColumn("SILKSPEC", 50)
                .SetIsHidden();
            grdcamlist.View.AddTextBoxColumn("TOOLSPEC", 50)
                .SetIsHidden();
            grdcamlist.View.AddTextBoxColumn("PANELGUIDESPEC", 50)
                .SetIsHidden();
            grdcamlist.View.AddTextBoxColumn("ETCSPEC", 50)
                .SetIsHidden();
            grdcamlist.View.AddTextBoxColumn("COMMENT1", 50)
                .SetIsHidden();
            grdcamlist.View.AddTextBoxColumn("COMMENT2", 50)
                .SetIsHidden();
            grdcamlist.View.AddTextBoxColumn("COMMENT3", 50)
                .SetIsHidden();
            grdcamlist.View.AddTextBoxColumn("COMMENT4", 50)
                .SetIsHidden();
            grdcamlist.View.AddTextBoxColumn("COMMENT5", 50)
                .SetIsHidden();
            grdcamlist.View.PopulateColumns();


            ConditionItemSelectPopup camuser = new ConditionItemSelectPopup();
            camuser.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            camuser.SetPopupLayout("USERLIST", PopupButtonStyles.Ok_Cancel, true, false);
            camuser.Id = "USER";
            camuser.SearchQuery = new SqlQuery("GetUserList", "10001", $"PLANTID={UserInfo.Current.Plant}");
            camuser.IsMultiGrid = false;
            camuser.DisplayFieldName = "USERNAME";
            camuser.ValueFieldName = "USERID";
            camuser.LanguageKey = "USER";
            camuser.Conditions.AddTextBox("USERIDNAME");

            camuser.GridColumns.AddTextBoxColumn("USERID", 150);
            camuser.GridColumns.AddTextBoxColumn("USERNAME", 200);

            PopupUserCam.SelectPopupCondition = camuser;

            ConditionItemSelectPopup specowner = new ConditionItemSelectPopup();
            specowner.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            specowner.SetPopupLayout("USERLIST", PopupButtonStyles.Ok_Cancel, true, false);
            specowner.Id = "USER";
            specowner.SearchQuery = new SqlQuery("GetUserList", "10001", $"PLANTID={UserInfo.Current.Plant}");
            specowner.IsMultiGrid = false;
            specowner.DisplayFieldName = "USERNAME";
            specowner.ValueFieldName = "USERID";
            specowner.LanguageKey = "USER";

            specowner.Conditions.AddTextBox("USERIDNAME");

            specowner.GridColumns.AddTextBoxColumn("USERID", 150);
            specowner.GridColumns.AddTextBoxColumn("USERNAME", 200);

            PopupUserSpec.SelectPopupCondition = specowner;



            // 고객명
            ConditionItemSelectPopup customId = new ConditionItemSelectPopup();
            customId.SetPopupLayoutForm(600, 800, FormBorderStyle.FixedDialog);
            customId.SetPopupLayout("CUSTOMERID", PopupButtonStyles.Ok_Cancel);
            customId.Id = "CUSTOMERID";
            customId.LabelText = "CUSTOMERID";
            customId.SearchQuery = new SqlQuery("GetCustomerListBySaleOrder", "10002", $"PLANTID={UserInfo.Current.Plant}");
            customId.IsMultiGrid = false;
            customId.DisplayFieldName = "CUSTOMERNAME";
            customId.ValueFieldName = "CUSTOMERID";
            customId.LanguageKey = "CUSTOMERID";
            customId.Conditions.AddTextBox("CUSTOMERID");
            customId.Conditions.AddTextBox("CUSTOMERNAME");
            customId.GridColumns.AddTextBoxColumn("CUSTOMERID", 150);
            customId.GridColumns.AddTextBoxColumn("CUSTOMERNAME", 200);
            PopupSite.SelectPopupCondition = customId;



            // 변경 후 품목
            ConditionItemSelectPopup cisItemId = new ConditionItemSelectPopup();
            cisItemId.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            cisItemId.SetPopupLayout("ITEMID", PopupButtonStyles.Ok_Cancel);
            cisItemId.Id = "ITEMID";
            cisItemId.LabelText = "ITEMID";
            cisItemId.SearchQuery = new SqlQuery("GetProductItemGroup", "10002", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}");
            cisItemId.IsMultiGrid = false;
            cisItemId.DisplayFieldName = "ITEMID";
            cisItemId.ValueFieldName = "ITEMID";
            cisItemId.LanguageKey = "ITEMID";
            cisItemId.Conditions.AddTextBox("ITEMID");
            cisItemId.Conditions.AddTextBox("ITEMNAME");
            cisItemId.GridColumns.AddTextBoxColumn("ITEMID", 150);
            cisItemId.GridColumns.AddTextBoxColumn("ITEMNAME", 200);
            cisItemId.GridColumns.AddTextBoxColumn("ITEMVERSION", 80);
            PopupafterItem.SelectPopupCondition = cisItemId;
            PopupafterItem.ButtonClick += PopupafterItem_ButtonClick;
            cisItemId.SetPopupApplySelection((selectRow, gridRow) => {


                selectRow.AsEnumerable().ForEach(r => {

                    txtafterrev.EditValue = r["ITEMVERSION"].ToString();
                    txtaftername.EditValue = r["ITEMNAME"].ToString();
                });


            });




            // 변경 전 품목
            ConditionItemSelectPopup previtem = new ConditionItemSelectPopup();
            previtem.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            previtem.SetPopupLayout("ITEMID", PopupButtonStyles.Ok_Cancel);
            previtem.Id = "ITEMID";
            previtem.LabelText = "ITEMID";
            previtem.SearchQuery = new SqlQuery("GetProductItemGroup", "10002", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}");
            previtem.IsMultiGrid = false;
            previtem.DisplayFieldName = "ITEMID";
            previtem.ValueFieldName = "ITEMID";
            previtem.LanguageKey = "ITEMID";
            previtem.Conditions.AddTextBox("ITEMID");
            previtem.Conditions.AddTextBox("ITEMNAME");
            previtem.GridColumns.AddTextBoxColumn("ITEMID", 150);
            previtem.GridColumns.AddTextBoxColumn("ITEMNAME", 200);
            previtem.GridColumns.AddTextBoxColumn("ITEMVERSION", 80);
            PopupprevItem.SelectPopupCondition = previtem;
            PopupprevItem.ButtonClick += PopupprevItem_ButtonClick;
            previtem.SetPopupApplySelection((selectRow, gridRow) => {


                selectRow.AsEnumerable().ForEach(r => {

                    txtprevrev.EditValue = r["ITEMVERSION"].ToString();
                    txtprevitemname.EditValue = r["ITEMNAME"].ToString();
                });


            });





            // 진행구분
            comboprocesstype.DisplayMember = "CODENAME";
            comboprocesstype.ValueMember = "CODEID";
            comboprocesstype.ShowHeader = false;
            Dictionary<string, object> ParamISHF = new Dictionary<string, object>();
            ParamISHF.Add("CODECLASSID", "ProcessType");
            ParamISHF.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtISHF = SqlExecuter.Query("GetCodeList", "00001", ParamISHF);
            comboprocesstype.DataSource = dtISHF;


            // 작업구분
            combojobtype.DisplayMember = "CODENAME";
            combojobtype.ValueMember = "CODEID";
            combojobtype.ShowHeader = false;
            Dictionary<string, object> ParamISHF2 = new Dictionary<string, object>();
            ParamISHF2.Add("CODECLASSID", "JobType");
            ParamISHF2.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtISHF2 = SqlExecuter.Query("GetCodeList", "00001", ParamISHF2);
            combojobtype.DataSource = dtISHF2;


            // 제품타입
            combomodeltype.DisplayMember = "CODENAME";
            combomodeltype.ValueMember = "CODEID";
            combomodeltype.ShowHeader = false;
            Dictionary<string, object> ParamISHF3 = new Dictionary<string, object>();
            ParamISHF3.Add("CODECLASSID", "ProductType");
            ParamISHF3.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtISHF3 = SqlExecuter.Query("GetCodeList", "00001", ParamISHF3);
            combomodeltype.DataSource = dtISHF3;

            // 
            cboinnerlayer.DisplayMember = "CODENAME";
            cboinnerlayer.ValueMember = "CODEID";
            cboinnerlayer.ShowHeader = false;
            ParamISHF = new Dictionary<string, object>();
            ParamISHF.Add("CODECLASSID", "InnerLayerCorrection");
            ParamISHF.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            dtISHF = SqlExecuter.Query("GetCodeList", "00001", ParamISHF);
            cboinnerlayer.DataSource = dtISHF;

            cboouterlayer.DisplayMember = "CODENAME";
            cboouterlayer.ValueMember = "CODEID";
            cboouterlayer.ShowHeader = false;
            ParamISHF = new Dictionary<string, object>();
            ParamISHF.Add("CODECLASSID", "OuterLayerCorrection");
            ParamISHF.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            dtISHF = SqlExecuter.Query("GetCodeList", "00001", ParamISHF);
            cboouterlayer.DataSource = dtISHF;


            cboapply.DisplayMember = "CODENAME";
            cboapply.ValueMember = "CODEID";
            cboapply.ShowHeader = false;
            ParamISHF = new Dictionary<string, object>();
            ParamISHF.Add("CODECLASSID", "ApplyLayer");
            ParamISHF.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            dtISHF = SqlExecuter.Query("GetCodeList", "00001", ParamISHF);
            cboapply.DataSource = dtISHF;

            cboguide.DisplayMember = "CODENAME";
            cboguide.ValueMember = "CODEID";
            cboguide.ShowHeader = false;
            ParamISHF = new Dictionary<string, object>();
            ParamISHF.Add("CODECLASSID", "OuterGuide");
            ParamISHF.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            dtISHF = SqlExecuter.Query("GetCodeList", "00001", ParamISHF);
            cboguide.DataSource = dtISHF;

            cbotrimming.DisplayMember = "CODENAME";
            cbotrimming.ValueMember = "CODEID";
            cbotrimming.ShowHeader = false;
            ParamISHF = new Dictionary<string, object>();
            ParamISHF.Add("CODECLASSID", "TrimmingMark");
            ParamISHF.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            dtISHF = SqlExecuter.Query("GetCodeList", "00001", ParamISHF);
            cbotrimming.DataSource = dtISHF;

            cbofoil.DisplayMember = "CODENAME";
            cbofoil.ValueMember = "CODEID";
            cbofoil.ShowHeader = false;
            ParamISHF = new Dictionary<string, object>();
            ParamISHF.Add("CODECLASSID", "CopperFoil");
            ParamISHF.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            dtISHF = SqlExecuter.Query("GetCodeList", "00001", ParamISHF);
            cbofoil.DataSource = dtISHF;


            cbozip.DisplayMember = "CODENAME";
            cbozip.ValueMember = "CODEID";
            cbozip.ShowHeader = false;
            ParamISHF = new Dictionary<string, object>();
            ParamISHF.Add("CODECLASSID", "ZipTerminal");
            ParamISHF.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            dtISHF = SqlExecuter.Query("GetCodeList", "00001", ParamISHF);
            cbozip.DataSource = dtISHF;


            cbopad.DisplayMember = "CODENAME";
            cbopad.ValueMember = "CODEID";
            cbopad.ShowHeader = false;
            ParamISHF = new Dictionary<string, object>();
            ParamISHF.Add("CODECLASSID", "PadCorrection");
            ParamISHF.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            dtISHF = SqlExecuter.Query("GetCodeList", "00001", ParamISHF);
            cbopad.DataSource = dtISHF;


            cbobbtstand.DisplayMember = "CODENAME";
            cbobbtstand.ValueMember = "CODEID";
            cbobbtstand.ShowHeader = false;
            ParamISHF = new Dictionary<string, object>();
            ParamISHF.Add("CODECLASSID", "BBTStandard");
            ParamISHF.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            dtISHF = SqlExecuter.Query("GetCodeList", "00001", ParamISHF);
            cbobbtstand.DataSource = dtISHF;

            cbomanu.DisplayMember = "CODENAME";
            cbomanu.ValueMember = "CODEID";
            cbomanu.ShowHeader = false;
            ParamISHF = new Dictionary<string, object>();
            ParamISHF.Add("CODECLASSID", "ManuFacturer");
            ParamISHF.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            dtISHF = SqlExecuter.Query("GetCodeList", "00001", ParamISHF);
            cbomanu.DataSource = dtISHF;

            cboreceive.DisplayMember = "CODENAME";
            cboreceive.ValueMember = "CODEID";
            cboreceive.ShowHeader = false;
            ParamISHF = new Dictionary<string, object>();
            ParamISHF.Add("CODECLASSID", "Receiver");
            ParamISHF.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            dtISHF = SqlExecuter.Query("GetCodeList", "00001", ParamISHF);
            cboreceive.DataSource = dtISHF;

            cboelo.DisplayMember = "CODENAME";
            cboelo.ValueMember = "CODEID";
            cboelo.ShowHeader = false;
            ParamISHF = new Dictionary<string, object>();
            ParamISHF.Add("CODECLASSID", "Elongation");
            ParamISHF.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            dtISHF = SqlExecuter.Query("GetCodeList", "00001", ParamISHF);
            cboelo.DataSource = dtISHF;

            cbobbttype.DisplayMember = "CODENAME";
            cbobbttype.ValueMember = "CODEID";
            cbobbttype.ShowHeader = false;
            ParamISHF = new Dictionary<string, object>();
            ParamISHF.Add("CODECLASSID", "BBTType");
            ParamISHF.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            dtISHF = SqlExecuter.Query("GetCodeList", "00001", ParamISHF);
            cbobbttype.DataSource = dtISHF;

            cbojig.DisplayMember = "CODENAME";
            cbojig.ValueMember = "CODEID";
            cbojig.ShowHeader = false;
            ParamISHF = new Dictionary<string, object>();
            ParamISHF.Add("CODECLASSID", "JigProduction");
            ParamISHF.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            dtISHF = SqlExecuter.Query("GetCodeList", "00001", ParamISHF);
            cbojig.DataSource = dtISHF;

            cboterminal.DisplayMember = "CODENAME";
            cboterminal.ValueMember = "CODEID";
            cboterminal.ShowHeader = false;
            ParamISHF = new Dictionary<string, object>();
            ParamISHF.Add("CODECLASSID", "TerminalType");
            ParamISHF.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            dtISHF = SqlExecuter.Query("GetCodeList", "00001", ParamISHF);
            cboterminal.DataSource = dtISHF;

            cbobbtmethod.DisplayMember = "CODENAME";
            cbobbtmethod.ValueMember = "CODEID";
            cbobbtmethod.ShowHeader = false;
            ParamISHF = new Dictionary<string, object>();
            ParamISHF.Add("CODECLASSID", "BBTMethod");
            ParamISHF.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            dtISHF = SqlExecuter.Query("GetCodeList", "00001", ParamISHF);
            cbobbtmethod.DataSource = dtISHF;

            cbomicro.DisplayMember = "CODENAME";
            cbomicro.ValueMember = "CODEID";
            cbomicro.ShowHeader = false;
            ParamISHF = new Dictionary<string, object>();
            ParamISHF.Add("CODECLASSID", "Microshort");
            ParamISHF.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            dtISHF = SqlExecuter.Query("GetCodeList", "00001", ParamISHF);
            cbomicro.DataSource = dtISHF;

            cbosurface1.DisplayMember = "CODENAME";
            cbosurface1.ValueMember = "CODEID";
            cbosurface1.ShowHeader = false;
            ParamISHF = new Dictionary<string, object>();
            ParamISHF.Add("CODECLASSID", "Surfaceplating1");
            ParamISHF.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            dtISHF = SqlExecuter.Query("GetCodeList", "00001", ParamISHF);
            cbosurface1.DataSource = dtISHF;

            cbosurface2.DisplayMember = "CODENAME";
            cbosurface2.ValueMember = "CODEID";
            cbosurface2.ShowHeader = false;
            ParamISHF = new Dictionary<string, object>();
            ParamISHF.Add("CODECLASSID", "Surfaceplating2");
            ParamISHF.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            dtISHF = SqlExecuter.Query("GetCodeList", "00001", ParamISHF);
            cbosurface2.DataSource = dtISHF;

            cbosurface3.DisplayMember = "CODENAME";
            cbosurface3.ValueMember = "CODEID";
            cbosurface3.ShowHeader = false;
            ParamISHF = new Dictionary<string, object>();
            ParamISHF.Add("CODECLASSID", "Surfaceplating3");
            ParamISHF.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            dtISHF = SqlExecuter.Query("GetCodeList", "00001", ParamISHF);
            cbosurface3.DataSource = dtISHF;



            txtrequestnumber.ReadOnly = true;
            txtregistday.ReadOnly = true;
            txtaftername.ReadOnly = true;
            txtprevrev.ReadOnly = true;
            txtprevitemname.ReadOnly = true;
            txtafterrev.ReadOnly = true;
        }

        private void Comboprocesstype_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int a = 0;

            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)
            {
                comboprocesstype.EditValue = null;
            }

        }

        private void PopupprevItem_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
          
                if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Clear)
                {
                    txtprevitemname.EditValue = null;
                    txtprevrev.EditValue = null;
                }
            
        }

        private void PopupafterItem_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if(e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Clear)
            {
                txtaftername.EditValue = null;
                txtafterrev.EditValue = null;
            }
        }

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            grdcamlist.View.AddingNewRow += View_AddingNewRow;
            grdcamlist.View.FocusedRowChanged += View_FocusedRowChanged;
        
        }

        private void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
        
               DataRow dr = grdcamlist.View.GetFocusedDataRow();
            if (dr == null)
                return;

            PopupSite.EditValue = dr["CUSTOMERID"];
            txtregistday.EditValue = dr["CREATEDTIME"];
            PopupUserSpec.EditValue = dr["SPECOWNER"];
            PopupUserCam.EditValue = dr["CAMOWNER"];
            txtprevrev.EditValue = dr["ITEMVERSION"];
            PopupprevItem.EditValue = dr["ITEMID"];
            txtprevitemname.EditValue = dr["ITEMNAME"];
            PopupafterItem.EditValue = dr["RCITEMID"];
            txtafterrev.EditValue = dr["RCITEMVERSION"];
            txtaftername.EditValue = dr["RCITEMNAME"];
            DataTable jobdt = combojobtype.DataSource as DataTable;

            for (int i = 0; i < jobdt.Rows.Count; i++)
            {
                if (jobdt.Rows[i][1].ToString().Equals(dr["JOBTYPE"].ToString()))
                {
                    combojobtype.EditValue = jobdt.Rows[i]["CODEID"].ToString();
                    break;
                }
                else
                {
                    combojobtype.EditValue = null;
                }

            }

            DataTable productdt = combomodeltype.DataSource as DataTable;
            for (int i = 0; i < productdt.Rows.Count; i++)
            {
                if (productdt.Rows[i][1].ToString().Equals(dr["PRODUCTTYPE"].ToString()))
                {
                    combomodeltype.EditValue = productdt.Rows[i]["CODEID"].ToString();
                    break;
                }
                else
                {
                    combomodeltype.EditValue = null;
                }
            }
            DataTable processdt = comboprocesstype.DataSource as DataTable;

            for (int i = 0; i < processdt.Rows.Count; i++)
            {
                if (processdt.Rows[i][1].ToString().Equals(dr["PROCESSTYPE"].ToString()))
                {
                    comboprocesstype.EditValue = processdt.Rows[i]["CODEID"].ToString();
                    break;
                }
                else
                {
                    comboprocesstype.EditValue = null;
                }
            }
            DataTable innerdt = cboinnerlayer.DataSource as DataTable;
            for (int i = 0; i < innerdt.Rows.Count; i++)
            {
                if (innerdt.Rows[i][1].ToString().Equals(dr["INNERLAYERCORRECTION"].ToString()))
                {
                    cboinnerlayer.EditValue = innerdt.Rows[i]["CODEID"].ToString();
                    break;
                }
                else
                {
                    cboinnerlayer.EditValue = null;
                }
            }
            DataTable outerdt = cboouterlayer.DataSource as DataTable;
            for (int i = 0; i < outerdt.Rows.Count; i++)
            {
                if (outerdt.Rows[i][1].ToString().Equals(dr["OUTERLAYERCORRECTION"].ToString()))
                {
                    cboouterlayer.EditValue = outerdt.Rows[i]["CODEID"].ToString();
                    break;
                }
                else
                {
                    cboouterlayer.EditValue = null;
                }
            }
            DataTable apply = cboapply.DataSource as DataTable;
            for (int i = 0; i < apply.Rows.Count; i++)
            {
                if (apply.Rows[i][0].ToString().Equals(dr["APPLYLAYERCORRECTION"].ToString()))
                {
                    cboapply.EditValue = apply.Rows[i]["CODEID"].ToString();
                    break;
                }
                else
                {
                    cboapply.EditValue = null;
                }
            }
            DataTable elo = cboelo.DataSource as DataTable;
            for (int i = 0; i < elo.Rows.Count; i++)
            {
                if (elo.Rows[i][1].ToString().Equals(dr["ELONGATION"].ToString()))
                {
                    cboelo.EditValue = elo.Rows[i]["CODEID"].ToString();
                    break;
                }
                else
                {
                    cboelo.EditValue = null;
                }
            }

            DataTable bbtst = cbobbtstand.DataSource as DataTable;
            for (int i = 0; i < bbtst.Rows.Count; i++)
            {
                if (bbtst.Rows[i][1].ToString().Equals(dr["BBTSTANDARD"].ToString()))
                {
                    cbobbtstand.EditValue = bbtst.Rows[i]["CODEID"].ToString();
                    break;
                }
                else
                {
                    cbobbtstand.EditValue = null;
                }
            }
            DataTable manust = cbomanu.DataSource as DataTable;
            for (int i = 0; i < manust.Rows.Count; i++)
            {
                if (manust.Rows[i][1].ToString().Equals(dr["MANUFACTURER"].ToString()))
                {
                    cbomanu.EditValue = manust.Rows[i]["CODEID"].ToString();
                    break;
                }
                else
                {
                    cbomanu.EditValue = null;
                }
            }
            DataTable bbtme = cbobbtmethod.DataSource as DataTable;
            for (int i = 0; i < bbtme.Rows.Count; i++)
            {
                if (bbtme.Rows[i][1].ToString().Equals(dr["BBTMETHOD"].ToString()))
                {
                    cbobbtmethod.EditValue = bbtme.Rows[i]["CODEID"].ToString();
                    break;
                }
                else
                {
                    cbobbtmethod.EditValue = null;
                }
            }
            DataTable micro = cbomicro.DataSource as DataTable;
            for (int i = 0; i < micro.Rows.Count; i++)
            {
                if (micro.Rows[i][1].ToString().Equals(dr["MICROSHORTREQUEST"].ToString()))
                {
                    cbomicro.EditValue = micro.Rows[i]["CODEID"].ToString();
                    break;
                }
                else
                {
                    cbomicro.EditValue = null;
                }
            }
            DataTable sur1 = cbosurface1.DataSource as DataTable;
            for (int i = 0; i < sur1.Rows.Count; i++)
            {
                if (sur1.Rows[i][1].ToString().Equals(dr["SURFACEPLATING1"].ToString()))
                {
                    cbosurface1.EditValue = sur1.Rows[i]["CODEID"].ToString();
                    break;
                }
                else
                {
                    cbosurface1.EditValue = null;
                }
            }
            DataTable sur2 = cbosurface2.DataSource as DataTable;
            for (int i = 0; i < sur2.Rows.Count; i++)
            {
                if (sur2.Rows[i][1].ToString().Equals(dr["SURFACEPLATING2"].ToString()))
                {
                    cbosurface2.EditValue = sur2.Rows[i]["CODEID"].ToString();
                    break;
                }
                else
                {
                    cbosurface2.EditValue = null;
                }
            }
            DataTable sur3 = cbosurface3.DataSource as DataTable;
            for (int i = 0; i < sur3.Rows.Count; i++)
            {
                if (sur3.Rows[i][1].ToString().Equals(dr["SURFACEPLATING3"].ToString()))
                {
                    cbosurface3.EditValue = sur3.Rows[i]["CODEID"].ToString();
                    break;
                }
                else
                {
                    cbosurface3.EditValue = null;
                }
            }
            DataTable outguide = cboguide.DataSource as DataTable;
            for (int i = 0; i < outguide.Rows.Count; i++)
            {
                if (outguide.Rows[i][1].ToString().Equals(dr["OUTERGUIDE"].ToString()))
                {
                    cboguide.EditValue = outguide.Rows[i]["CODEID"].ToString();
                    break;
                }
                else
                {
                    cboguide.EditValue = null;
                }
            }
            DataTable trim = cbotrimming.DataSource as DataTable;
            for (int i = 0; i < trim.Rows.Count; i++)
            {
                if (trim.Rows[i][1].ToString().Equals(dr["TRIMMINGMARK"].ToString()))
                {
                    cbotrimming.EditValue = trim.Rows[i]["CODEID"].ToString();
                    break;
                }
                else
                {
                    cbotrimming.EditValue = null;
                }
            }
            DataTable receive = cboreceive.DataSource as DataTable;
            for (int i = 0; i < receive.Rows.Count; i++)
            {
                if (receive.Rows[i][1].ToString().Equals(dr["RECEIVER"].ToString()))
                {
                    cboreceive.EditValue = receive.Rows[i]["CODEID"].ToString();
                    break;
                }
                else
                {
                    cboreceive.EditValue = null;
                }
            }
            DataTable bbttype = cbobbttype.DataSource as DataTable;
            for (int i = 0; i < bbttype.Rows.Count; i++)
            {
                if (bbttype.Rows[i][1].ToString().Equals(dr["BBTTYPE"].ToString()))
                {
                    cbobbttype.EditValue = bbttype.Rows[i]["CODEID"].ToString();
                    break;
                }
                else
                {
                    cbobbttype.EditValue = null;
                }
            }
            DataTable zip = cbozip.DataSource as DataTable;
            for (int i = 0; i < zip.Rows.Count; i++)
            {
                if (zip.Rows[i][1].ToString().Equals(dr["ZIPTERMINAL"].ToString()))
                {
                    cbozip.EditValue = zip.Rows[i]["CODEID"].ToString();
                    break;
                }
                else
                {
                    cbozip.EditValue = null;
                }
            }
            DataTable foil = cbofoil.DataSource as DataTable;
            for (int i = 0; i < foil.Rows.Count; i++)
            {
                if (foil.Rows[i][1].ToString().Equals(dr["APPLYCOPPERFOIL"].ToString()))
                {
                    cbofoil.EditValue = foil.Rows[i]["CODEID"].ToString();
                    break;
                }
                else
                {
                    cbofoil.EditValue = null;
                }
            }
            DataTable pad = cbopad.DataSource as DataTable;
            for (int i = 0; i < pad.Rows.Count; i++)
            {
                if (pad.Rows[i][1].ToString().Equals(dr["PADCORRECTION"].ToString()))
                {
                    cbopad.EditValue = pad.Rows[i]["CODEID"].ToString();
                    break;
                }
                else
                {
                    cbopad.EditValue = null;
                }
            }
            DataTable terminal = cboterminal.DataSource as DataTable;
            for (int i = 0; i < terminal.Rows.Count; i++)
            {
                if (terminal.Rows[i][1].ToString().Equals(dr["TERMINALTYPE"].ToString()))
                {
                    cboterminal.EditValue = terminal.Rows[i]["CODEID"].ToString();
                    break;
                }
                else
                {
                    cboterminal.EditValue = null;
                }
            }
            DataTable jig = cbojig.DataSource as DataTable;
            for (int i = 0; i < jig.Rows.Count; i++)
            {
                if (jig.Rows[i][1].ToString().Equals(dr["JIGPRODUCTION"].ToString()))
                {
                    cbojig.EditValue = jig.Rows[i]["CODEID"].ToString();
                    break;
                }
                else
                {
                    cbojig.EditValue = null;
                }
            }
            txtapply.EditValue = dr["APPLYLAYER"];
            txtrequestnumber.EditValue = dr["CAMREQUESTID"];
            memoolb.EditValue = dr["OLBCORRECTION"];
            txtysize.EditValue = dr["PANELSIZE_Y"];
            txtmodeldate.EditValue = dr["MODELDELIVERYDATE"];
            txtprevversion.EditValue = dr["PREVCUSTOMERVERSION"];
            txtapplyversion.EditValue = dr["APPLYCUSTOMERVERSION"];
            txtbbtdate.EditValue = dr["BBTDELIVERYDATE"];
            txtmk.EditValue = dr["MKNOTATION"];
            txtspecweek.EditValue = dr["SPECWEEK"];
            txtcamweek.EditValue = dr["CAMWEEK"];
            txtguide.EditValue = dr["GUIDESTANDARD"];
            txtvisionpress.EditValue = dr["VISIONPRESS"];
            //txtDrill.EditValue = dr["DRILLSPEC"];
            //txtouter.EditValue = dr["OUTERLAYERSPEC"];
            //txtinner.EditValue = dr["INNERLAYERSPEC"];
            //txtcover.EditValue = dr["COVERLAYSPEC"];
            //txtpsr.EditValue = dr["PSRSPEC"];
            //txtlayer.EditValue = dr["LAYERADHESION"];
            //txtcut.EditValue = dr["CUTLASERSPEC"];
            //txtpeel.EditValue = dr["PEELRSTSPEC"];
            //txtsilk.EditValue = dr["SILKSPEC"];
            //txtrouter.EditValue = dr["ROUTERSPEC"];
            //txttool.EditValue = dr["TOOLSPEC"];
            //txtpanel.EditValue = dr["PANELGUIDESPEC"];
            //txtetc.EditValue = dr["ETCSPEC"];
            txtconnectlayer.EditValue = dr["PLATINGCONNECTINGLAYER"];
            txtcomment1.EditValue = dr["COMMENT1"];
            txtcomment2.EditValue = dr["COMMENT2"];
            txtcomment3.EditValue = dr["COMMENT3"];
            txtcomment4.EditValue = dr["COMMENT4"];
            txtcomment5.EditValue = dr["COMMENT5"];
        }

        /// <summary>
        /// 코드그룹 리스트 그리드에서 추가 버튼 클릭 시 호출
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {

            DataTable dt = grdcamlist.View.GetCheckedRows();
            DataRow dr = dt.Rows[0];
            if (dr == null)
                return;

            PopupSite.EditValue = dr["CUSTOMERID"];
            txtregistday.EditValue = dr["CREATEDTIME"];
            PopupUserSpec.EditValue = dr["SPECOWNER"];
            PopupUserCam.EditValue = dr["CAMOWNER"];
            txtprevrev.EditValue = dr["ITEMVERSION"];
            PopupprevItem.EditValue = dr["ITEMID"];
            txtprevitemname.EditValue = dr["ITEMNAME"];
            PopupafterItem.EditValue = dr["RCITEMID"];
            txtafterrev.EditValue = dr["RCITEMVERSION"];
            txtaftername.EditValue = dr["RCITEMNAME"];
            DataTable jobdt = combojobtype.DataSource as DataTable;

            for (int i = 0; i < jobdt.Rows.Count; i++)
            {
                if (jobdt.Rows[i][1].ToString().Equals(dr["JOBTYPE"].ToString()))
                {
                    combojobtype.EditValue = jobdt.Rows[i]["CODEID"].ToString();
                    break;
                }
                else
                {
                    combojobtype.EditValue = null;
                }

            }

            DataTable productdt = combomodeltype.DataSource as DataTable;

            for (int i = 0; i < productdt.Rows.Count; i++)
            {
                if (productdt.Rows[i][1].ToString().Equals(dr["PRODUCTTYPE"].ToString()))
                {
                    combomodeltype.EditValue = productdt.Rows[i]["CODEID"].ToString();
                    break;
                }
                else
                {
                    combomodeltype.EditValue = null;
                }

            }

            DataTable processdt = comboprocesstype.DataSource as DataTable;

            for (int i = 0; i < processdt.Rows.Count; i++)
            {
                if (processdt.Rows[i][1].ToString().Equals(dr["PROCESSTYPE"].ToString()))
                {
                    comboprocesstype.EditValue = processdt.Rows[i]["CODEID"].ToString();
                    break;
                }
                else
                {
                    comboprocesstype.EditValue = null;
                }

            }
            DataTable innerdt = cboinnerlayer.DataSource as DataTable;
            for (int i = 0; i < innerdt.Rows.Count; i++)
            {
                if (innerdt.Rows[i][1].ToString().Equals(dr["INNERLAYERCORRECTION"].ToString()))
                {
                    cboinnerlayer.EditValue = innerdt.Rows[i]["CODEID"].ToString();
                    break;
                }
                else
                {
                    cboinnerlayer.EditValue = null;
                }
            }
            DataTable outerdt = cboouterlayer.DataSource as DataTable;
            for (int i = 0; i < outerdt.Rows.Count; i++)
            {
                if (outerdt.Rows[i][1].ToString().Equals(dr["OUTERLAYERCORRECTION"].ToString()))
                {
                    cboouterlayer.EditValue = outerdt.Rows[i]["CODEID"].ToString();
                    break;
                }
                else
                {
                    cboouterlayer.EditValue = null;
                }
            }
            DataTable apply = cboapply.DataSource as DataTable;
            for (int i = 0; i < apply.Rows.Count; i++)
            {
                if (apply.Rows[i][1].ToString().Equals(dr["APPLYLAYERCORRECTION"].ToString()))
                {
                    cboapply.EditValue = apply.Rows[i]["CODEID"].ToString();
                    break;
                }
                else
                {
                    cboapply.EditValue = null;
                }
            }
            DataTable elo = cboelo.DataSource as DataTable;
            for (int i = 0; i < elo.Rows.Count; i++)
            {
                if (elo.Rows[i][1].ToString().Equals(dr["ELONGATION"].ToString()))
                {
                    cboelo.EditValue = elo.Rows[i]["CODEID"].ToString();
                    break;
                }
                else
                {
                    cboelo.EditValue = null;
                }
            }

            DataTable bbtst = cbobbtstand.DataSource as DataTable;
            for (int i = 0; i < bbtst.Rows.Count; i++)
            {
                if (bbtst.Rows[i][1].ToString().Equals(dr["BBTSTANDARD"].ToString()))
                {
                    cbobbtstand.EditValue = bbtst.Rows[i]["CODEID"].ToString();
                    break;
                }
                else
                {
                    cbobbtstand.EditValue = null;
                }
            }
            DataTable manust = cbomanu.DataSource as DataTable;
            for (int i = 0; i < manust.Rows.Count; i++)
            {
                if (manust.Rows[i][1].ToString().Equals(dr["MANUFACTURER"].ToString()))
                {
                    cbomanu.EditValue = manust.Rows[i]["CODEID"].ToString();
                    break;
                }
                else
                {
                    cbomanu.EditValue = null;
                }
            }
            DataTable bbtme = cbobbtmethod.DataSource as DataTable;
            for (int i = 0; i < bbtme.Rows.Count; i++)
            {
                if (bbtme.Rows[i][1].ToString().Equals(dr["BBTMETHOD"].ToString()))
                {
                    cbobbtmethod.EditValue = bbtme.Rows[i]["CODEID"].ToString();
                    break;
                }
                else
                {
                    cbobbtmethod.EditValue = null;
                }
            }
            DataTable micro = cbomicro.DataSource as DataTable;
            for (int i = 0; i < micro.Rows.Count; i++)
            {
                if (micro.Rows[i][1].ToString().Equals(dr["MICROSHORTREQUEST"].ToString()))
                {
                    cbomicro.EditValue = micro.Rows[i]["CODEID"].ToString();
                    break;
                }
                else
                {
                    cbomicro.EditValue = null;
                }
            }
            DataTable sur1 = cbosurface1.DataSource as DataTable;
            for (int i = 0; i < sur1.Rows.Count; i++)
            {
                if (sur1.Rows[i][1].ToString().Equals(dr["SURFACEPLATING1"].ToString()))
                {
                    cbosurface1.EditValue = sur1.Rows[i]["CODEID"].ToString();
                    break;
                }
                else
                {
                    cbosurface1.EditValue = null;
                }
            }
            DataTable sur2 = cbosurface2.DataSource as DataTable;
            for (int i = 0; i < sur2.Rows.Count; i++)
            {
                if (sur2.Rows[i][1].ToString().Equals(dr["SURFACEPLATING2"].ToString()))
                {
                    cbosurface2.EditValue = sur2.Rows[i]["CODEID"].ToString();
                    break;
                }
                else
                {
                    cbosurface2.EditValue = null;
                }
            }
            DataTable sur3 = cbosurface3.DataSource as DataTable;
            for (int i = 0; i < sur3.Rows.Count; i++)
            {
                if (sur3.Rows[i][1].ToString().Equals(dr["SURFACEPLATING3"].ToString()))
                {
                    cbosurface3.EditValue = sur3.Rows[i]["CODEID"].ToString();
                    break;
                }
                else
                {
                    cbosurface3.EditValue = null;
                }
            }
            DataTable outguide = cboguide.DataSource as DataTable;
            for (int i = 0; i < outguide.Rows.Count; i++)
            {
                if (outguide.Rows[i][1].ToString().Equals(dr["OUTERGUIDE"].ToString()))
                {
                    cboguide.EditValue = outguide.Rows[i]["CODEID"].ToString();
                    break;
                }
                else
                {
                    cboguide.EditValue = null;
                }
            }
            DataTable trim = cbotrimming.DataSource as DataTable;
            for (int i = 0; i < trim.Rows.Count; i++)
            {
                if (trim.Rows[i][1].ToString().Equals(dr["TRIMMINGMARK"].ToString()))
                {
                    cbotrimming.EditValue = trim.Rows[i]["CODEID"].ToString();
                    break;
                }
                else
                {
                    cbotrimming.EditValue = null;
                }
            }
            DataTable receive = cboreceive.DataSource as DataTable;
            for (int i = 0; i < receive.Rows.Count; i++)
            {
                if (receive.Rows[i][1].ToString().Equals(dr["RECEIVER"].ToString()))
                {
                    cboreceive.EditValue = receive.Rows[i]["CODEID"].ToString();
                    break;
                }
                else
                {
                    cboreceive.EditValue = null;
                }
            }
            DataTable bbttype = cbobbttype.DataSource as DataTable;
            for (int i = 0; i < bbttype.Rows.Count; i++)
            {
                if (bbttype.Rows[i][1].ToString().Equals(dr["BBTTYPE"].ToString()))
                {
                    cbobbttype.EditValue = bbttype.Rows[i]["CODEID"].ToString();
                    break;
                }
                else
                {
                    cbobbttype.EditValue = null;
                }
            }
            DataTable zip = cbozip.DataSource as DataTable;
            for (int i = 0; i < zip.Rows.Count; i++)
            {
                if (zip.Rows[i][1].ToString().Equals(dr["ZIPTERMINAL"].ToString()))
                {
                    cbozip.EditValue = zip.Rows[i]["CODEID"].ToString();
                    break;
                }
                else
                {
                    cbozip.EditValue = null;
                }
            }
            DataTable foil = cbofoil.DataSource as DataTable;
            for (int i = 0; i < foil.Rows.Count; i++)
            {
                if (foil.Rows[i][1].ToString().Equals(dr["APPLYCOPPERFOIL"].ToString()))
                {
                    cbofoil.EditValue = foil.Rows[i]["CODEID"].ToString();
                    break;
                }
                else
                {
                    cbofoil.EditValue = null;
                }
            }
            DataTable pad = cbopad.DataSource as DataTable;
            for (int i = 0; i < pad.Rows.Count; i++)
            {
                if (pad.Rows[i][1].ToString().Equals(dr["PADCORRECTION"].ToString()))
                {
                    cbopad.EditValue = pad.Rows[i]["CODEID"].ToString();
                    break;
                }
                else
                {
                    cbopad.EditValue = null;
                }
            }
            DataTable terminal = cboterminal.DataSource as DataTable;
            for (int i = 0; i < terminal.Rows.Count; i++)
            {
                if (terminal.Rows[i][1].ToString().Equals(dr["TERMINALTYPE"].ToString()))
                {
                    cboterminal.EditValue = terminal.Rows[i]["CODEID"].ToString();
                    break;
                }
                else
                {
                    cboterminal.EditValue = null;
                }
            }
            DataTable jig = cbojig.DataSource as DataTable;
            for (int i = 0; i < jig.Rows.Count; i++)
            {
                if (jig.Rows[i][1].ToString().Equals(dr["JIGPRODUCTION"].ToString()))
                {
                    cbojig.EditValue = jig.Rows[i]["CODEID"].ToString();
                    break;
                }
                else
                {
                    cbojig.EditValue = null;
                }
            }


            txtapply.EditValue = dr["APPLYLAYER"];
            memoolb.EditValue = dr["OLBCORRECTION"];
            txtysize.EditValue = dr["PANELSIZE_Y"];
            txtmodeldate.EditValue = dr["MODELDELIVERYDATE"];
            txtprevversion.EditValue = dr["PREVCUSTOMERVERSION"];
            txtapplyversion.EditValue = dr["APPLYCUSTOMERVERSION"];
            txtbbtdate.EditValue = dr["BBTDELIVERYDATE"];
            txtmk.EditValue = dr["MKNOTATION"];
            txtspecweek.EditValue = dr["SPECWEEK"];
            txtcamweek.EditValue = dr["CAMWEEK"];
            txtguide.EditValue = dr["GUIDESTANDARD"];
            txtvisionpress.EditValue = dr["VISIONPRESS"];
            //txtDrill.EditValue = dr["DRILLSPEC"];
            //txtouter.EditValue = dr["OUTERLAYERSPEC"];
            //txtinner.EditValue = dr["INNERLAYERSPEC"];
            //txtcover.EditValue = dr["COVERLAYSPEC"];
            //txtpsr.EditValue = dr["PSRSPEC"];
            //txtlayer.EditValue = dr["LAYERADHESION"];
            //txtcut.EditValue = dr["CUTLASERSPEC"];
            //txtpeel.EditValue = dr["PEELRSTSPEC"];
            //txtsilk.EditValue = dr["SILKSPEC"];
            //txtrouter.EditValue = dr["ROUTERSPEC"];
            //txttool.EditValue = dr["TOOLSPEC"];
            //txtpanel.EditValue = dr["PANELGUIDESPEC"];
            //txtetc.EditValue = dr["ETCSPEC"];
            txtconnectlayer.EditValue = dr["PLATINGCONNECTINGLAYER"];
            txtcomment1.EditValue = dr["COMMENT1"];
            txtcomment2.EditValue = dr["COMMENT2"];
            txtcomment3.EditValue = dr["COMMENT3"];
            txtcomment4.EditValue = dr["COMMENT4"];
            txtcomment5.EditValue = dr["COMMENT5"];

        }

        #endregion



        #region 툴바


        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            base.OnToolbarClick(sender, e);
            SmartButton btn = sender as SmartButton;

            if (btn.Name.ToString().Equals("Initialization"))
            {
                ProductDataReset();


            }

        }

        

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            // TODO : 저장 Rule 변경
            DataTable changed = grdcamlist.GetChangedRows();
            for(int i=0;i<changed.Rows.Count;i++)
            {
                changed.Rows.RemoveAt(0);
            }
            
            DataRow dr = changed.NewRow();
            dr["CUSTOMERID"] = PopupSite.EditValue;
           

            dr["SPECOWNER"] = PopupUserSpec.EditValue;
            dr["CAMOWNER"] = PopupUserCam.EditValue;
            dr["ITEMVERSION"] = txtprevrev.EditValue;
            dr["ITEMID"] = PopupprevItem.EditValue;
            dr["ITEMNAME"] = txtprevitemname.EditValue;
            dr["RCITEMID"] = PopupafterItem.EditValue;
            dr["RCITEMVERSION"]  = txtafterrev.EditValue;
            dr["RCITEMNAME"] = txtaftername.EditValue;
            dr["OLBCORRECTION"] = memoolb.EditValue;
          

        
                DataTable jobdt = combojobtype.DataSource as DataTable;
            if (combojobtype.EditValue!=null)
            {
                for (int i = 0; i < jobdt.Rows.Count; i++)
                {
                    if (jobdt.Rows[i][0].ToString().Equals(combojobtype.EditValue.ToString()))
                    {
                        dr["JOBTYPE"] = jobdt.Rows[i]["CODEID"].ToString();
                        break;
                    }

                }

            }
            if (combomodeltype.EditValue != null)
            {
                DataTable productdt = combomodeltype.DataSource as DataTable;

                for (int i = 0; i < productdt.Rows.Count; i++)
                {
                    if (productdt.Rows[i][0].ToString().Equals(combomodeltype.EditValue.ToString()))
                    {
                        dr["PRODUCTTYPE"] = productdt.Rows[i]["CODEID"].ToString();
                        break;
                    }

                }
            }

            if (comboprocesstype.EditValue != null)
            {
                DataTable processdt = comboprocesstype.DataSource as DataTable;


                for (int i = 0; i < processdt.Rows.Count; i++)
                {
                    if (processdt.Rows[i][0].ToString().Equals(comboprocesstype.EditValue.ToString()))
                    {
                        dr["PROCESSTYPE"] = processdt.Rows[i]["CODEID"].ToString();
                        break;
                    }

                }
            }
            if (cboinnerlayer.EditValue != null)
            {
                DataTable innerdt = cboinnerlayer.DataSource as DataTable;
                for (int i = 0; i < innerdt.Rows.Count; i++)
                {
                    if (innerdt.Rows[i][0].ToString().Equals(cboinnerlayer.EditValue.ToString()))
                    {
                        dr["INNERLAYERCORRECTION"] = innerdt.Rows[i]["CODEID"].ToString();
                        break;
                    }
                }
            }
            if (cboouterlayer.EditValue != null)
            {
                DataTable outerdt = cboouterlayer.DataSource as DataTable;
                for (int i = 0; i < outerdt.Rows.Count; i++)
                {
                    if (outerdt.Rows[i][0].ToString().Equals(cboouterlayer.EditValue.ToString()))
                    {
                        dr["OUTERLAYERCORRECTION"] = outerdt.Rows[i]["CODEID"].ToString();
                        break;
                    }
                }
            }
            if (cboapply.EditValue != null)
            {
                DataTable apply = cboapply.DataSource as DataTable;
                for (int i = 0; i < apply.Rows.Count; i++)
                {
                    if (apply.Rows[i][0].ToString().Equals(cboapply.EditValue.ToString()))
                    {
                        dr["APPLYLAYERCORRECTION"] = apply.Rows[i]["CODEID"].ToString();
                        break;
                    }
                }
            }
            if (cboelo.EditValue != null)
            {
                DataTable elo = cboelo.DataSource as DataTable;
                for (int i = 0; i < elo.Rows.Count; i++)
                {
                    if (elo.Rows[i][0].ToString().Equals(cboelo.EditValue.ToString()))
                    {
                        dr["ELONGATION"] = elo.Rows[i]["CODEID"].ToString();
                        break;
                    }
                }
            }
            if (cbobbtstand.EditValue != null)
            {
                DataTable bbtst = cbobbtstand.DataSource as DataTable;
                for (int i = 0; i < bbtst.Rows.Count; i++)
                {
                    if (bbtst.Rows[i][0].ToString().Equals(cbobbtstand.EditValue.ToString()))
                    {
                        dr["BBTSTANDARD"] = bbtst.Rows[i]["CODEID"].ToString();
                        break;
                    }
                }
            }
            if (cbomanu.EditValue != null)
            {
                DataTable manust = cbomanu.DataSource as DataTable;
                for (int i = 0; i < manust.Rows.Count; i++)
                {
                    if (manust.Rows[i][0].ToString().Equals(cbomanu.EditValue.ToString()))
                    {
                        dr["MANUFACTURER"] = manust.Rows[i]["CODEID"].ToString();
                        break;
                    }
                }
            }
            if (cbobbtmethod.EditValue != null)
            {
                DataTable bbtme = cbobbtmethod.DataSource as DataTable;
                for (int i = 0; i < bbtme.Rows.Count; i++)
                {
                    if (bbtme.Rows[i][0].ToString().Equals(cbobbtmethod.EditValue.ToString()))
                    {
                        dr["BBTMETHOD"] = bbtme.Rows[i]["CODEID"].ToString();
                        break;
                    }

                }
            }
            if (cbomicro.EditValue != null)
            {
                DataTable micro = cbomicro.DataSource as DataTable;
                for (int i = 0; i < micro.Rows.Count; i++)
                {
                    if (micro.Rows[i][0].ToString().Equals(cbomicro.EditValue.ToString()))
                    {
                        dr["MICROSHORTREQUEST"] = micro.Rows[i]["CODEID"].ToString();
                        break;
                    }
                }
            }
            if (cbosurface1.EditValue != null)
            {
                DataTable sur1 = cbosurface1.DataSource as DataTable;
                for (int i = 0; i < sur1.Rows.Count; i++)
                {
                    if (sur1.Rows[i][0].ToString().Equals(cbosurface1.EditValue.ToString()))
                    {
                        dr["SURFACEPLATING1"] = sur1.Rows[i]["CODEID"].ToString();
                        break;
                    }
                }
            }
            if (cbosurface2.EditValue != null)
            {
                DataTable sur2 = cbosurface2.DataSource as DataTable;
                for (int i = 0; i < sur2.Rows.Count; i++)
                {
                    if (sur2.Rows[i][0].ToString().Equals(cbosurface2.EditValue.ToString()))
                    {
                        dr["SURFACEPLATING2"] = sur2.Rows[i]["CODEID"].ToString();
                        break;
                    }
                }
            }
            if (cbosurface3.EditValue != null)
            {
                DataTable sur3 = cbosurface3.DataSource as DataTable;
                for (int i = 0; i < sur3.Rows.Count; i++)
                {
                    if (sur3.Rows[i][0].ToString().Equals(cbosurface3.EditValue.ToString()))
                    {
                        dr["SURFACEPLATING3"] = sur3.Rows[i]["CODEID"].ToString();
                        break;
                    }
                }
            }
            if (cboguide.EditValue != null)
            {
                DataTable outguide = cboguide.DataSource as DataTable;
                for (int i = 0; i < outguide.Rows.Count; i++)
                {
                    if (outguide.Rows[i][0].ToString().Equals(cboguide.EditValue.ToString()))
                    {
                        dr["OUTERGUIDE"] = outguide.Rows[i]["CODEID"].ToString();
                        break;
                    }
                }
            }
            if (cbotrimming.EditValue != null)
            {
                DataTable trim = cbotrimming.DataSource as DataTable;
                for (int i = 0; i < trim.Rows.Count; i++)
                {
                    if (trim.Rows[i][0].ToString().Equals(cbotrimming.EditValue.ToString()))
                    {
                        dr["TRIMMINGMARK"] = trim.Rows[i]["CODEID"].ToString();
                        break;
                    }

                }
            }
            if (cboreceive.EditValue != null)
            {
                DataTable receive = cboreceive.DataSource as DataTable;
                for (int i = 0; i < receive.Rows.Count; i++)
                {
                    if (receive.Rows[i][0].ToString().Equals(cboreceive.EditValue.ToString()))
                    {
                        dr["RECEIVER"] = receive.Rows[i]["CODEID"].ToString();
                        break;
                    }

                }
            }
            if (cbobbttype.EditValue != null)
            {
                DataTable bbttype = cbobbttype.DataSource as DataTable;
                for (int i = 0; i < bbttype.Rows.Count; i++)
                {
                    if (bbttype.Rows[i][0].ToString().Equals(cbobbttype.EditValue.ToString()))
                    {
                        dr["BBTTYPE"] = bbttype.Rows[i]["CODEID"].ToString();
                        break;
                    }
                }
            }
            if (cbozip.EditValue != null)
            {
                DataTable zip = cbozip.DataSource as DataTable;
                for (int i = 0; i < zip.Rows.Count; i++)
                {
                    if (zip.Rows[i][0].ToString().Equals(cbozip.EditValue.ToString()))
                    {
                        dr["ZIPTERMINAL"] = zip.Rows[i]["CODEID"].ToString();
                        break;
                    }

                }
            }
            if (cbofoil.EditValue != null)
            {
                DataTable foil = cbofoil.DataSource as DataTable;
                for (int i = 0; i < foil.Rows.Count; i++)
                {
                    if (foil.Rows[i][0].ToString().Equals(cbofoil.EditValue.ToString()))
                    {
                        dr["APPLYCOPPERFOIL"] = foil.Rows[i]["CODEID"].ToString();
                        break;
                    }

                }
            }
            if (cbopad.EditValue != null)
            {
                DataTable pad = cbopad.DataSource as DataTable;
                for (int i = 0; i < pad.Rows.Count; i++)
                {
                    if (pad.Rows[i][0].ToString().Equals(cbopad.EditValue.ToString()))
                    {
                        dr["PADCORRECTION"] = pad.Rows[i]["CODEID"].ToString();
                        break;
                    }

                }
            }
            if (cboterminal.EditValue != null)
            {
                DataTable terminal = cboterminal.DataSource as DataTable;
                for (int i = 0; i < terminal.Rows.Count; i++)
                {
                    if (terminal.Rows[i][0].ToString().Equals(cboterminal.EditValue.ToString()))
                    {
                        dr["TERMINALTYPE"] = terminal.Rows[i]["CODEID"].ToString();
                        break;
                    }

                }
            }
            if (cbojig.EditValue != null)
            {
                DataTable jig = cbojig.DataSource as DataTable;
                for (int i = 0; i < jig.Rows.Count; i++)
                {
                    if (jig.Rows[i][0].ToString().Equals(cbojig.EditValue.ToString()))
                    {
                        dr["JIGPRODUCTION"] = jig.Rows[i]["CODEID"].ToString();
                        break;
                    }

                }
            }







            dr["APPLYLAYER"] = txtapply.EditValue;
            dr["PANELSIZE_Y"] = txtysize.EditValue;
            dr["MODELDELIVERYDATE"] = Convert.ToDateTime(txtmodeldate.EditValue).ToString("yyyy-MM-dd");
            dr["PREVCUSTOMERVERSION"] = txtprevversion.EditValue;
            dr["APPLYCUSTOMERVERSION"] = txtapplyversion.EditValue;
            dr["BBTDELIVERYDATE"] =   txtbbtdate.EditValue;
            dr["MKNOTATION"]= txtmk.EditValue;
            dr["SPECWEEK"] = txtspecweek.EditValue;
            dr["CAMWEEK"] = txtcamweek.EditValue;
            dr["GUIDESTANDARD"] = txtguide.EditValue;
            dr["VISIONPRESS"]= txtvisionpress.EditValue;
            dr["PLATINGCONNECTINGLAYER"] = txtconnectlayer.EditValue;
            //dr["DRILLSPEC"] = txtDrill.EditValue;
            //dr["OUTERLAYERSPEC"] = txtouter.EditValue;
            //dr["INNERLAYERSPEC"] =txtinner.EditValue;
            //dr["COVERLAYSPEC"]= txtcover.EditValue;
            //dr["PSRSPEC"] = txtpsr.EditValue;
            //dr["LAYERADHESION"] = txtlayer.EditValue;
            //dr["CUTLASERSPEC"] = txtcut.EditValue;
            //dr["PEELRSTSPEC"] = txtpeel.EditValue;
            //dr["SILKSPEC"] = txtsilk.EditValue;
            //dr["ROUTERSPEC"] = txtrouter.EditValue;
            //dr["TOOLSPEC"] = txttool.EditValue;
            //dr["PANELGUIDESPEC"] = txtpanel.EditValue;
            //dr["ETCSPEC"] = txtetc.EditValue;
            dr["COMMENT1"] =txtcomment1.EditValue;
            dr["COMMENT2"] =txtcomment2.EditValue;
            dr["COMMENT3"] = txtcomment3.EditValue;
            dr["COMMENT4"]= txtcomment4.EditValue;
            dr["COMMENT5"] = txtcomment5.EditValue;
            dr["PLANTID"] = UserInfo.Current.Plant;
            dr["CAMREQUESTID"] = txtrequestnumber.EditValue;
            if(txtrequestnumber.EditValue==null || txtrequestnumber.EditValue.ToString().Equals(""))
            {
                dr["_STATE_"] = "added";
            }
            else
            {
                dr["_STATE_"] = "modified";

            }
            changed.Rows.Add(dr);
            ExecuteRule("SaveCamRequestHistory", changed);
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
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtCodeClass = SqlExecuter.Query("SelectCamHistory", "10001", values);

            if (dtCodeClass.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ProductDataReset();
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdcamlist.DataSource = dtCodeClass;
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();
            InitializeGrid_CustomerPopup();
            InitializeCondition_Popup();
            InitializeGrid_SpecUserPopup();
            InitializeGrid_CamUserPopup();
            
            // TODO : 조회조건 추가 구성이 필요한 경우 사용
        }


        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            Conditions.GetControl<SmartTextBox>("P_ITEMVERSION").ReadOnly = true;
            Conditions.GetControl<SmartTextBox>("P_ITEMNAME").ReadOnly = true;
            Conditions.GetControl<SmartSelectPopupEdit>("ITEMID").EditValueChanged += ProductDefIDChanged;


            //SmartSelectPopupEdit CustmerPopupedit = Conditions.GetControl<SmartSelectPopupEdit>("CUSTOMERID");
            //CustmerPopupedit.Validated += CustmerPopupedit_Validated;


        }

        private void ProductDefIDChanged(object sender, EventArgs e)
        {
            SmartSelectPopupEdit PopProdutid = sender as SmartSelectPopupEdit;

            if (Format.GetFullTrimString(PopProdutid.EditValue).Equals(string.Empty))
            {
                Conditions.GetControl<SmartTextBox>("P_ITEMVERSION").EditValue = string.Empty;
                Conditions.GetControl<SmartTextBox>("P_ITEMNAME").EditValue = string.Empty;
            }
        }

        private void InitializeGrid_CustomerPopup()
        {
            //추후 변경 예정

            var parentCodeClassPopupColumn = Conditions.AddSelectPopup("P_Customer", new SqlQuery("GetCustomerListBySaleOrder", "10001", $"PLANTID={UserInfo.Current.Plant}"), "CUSTOMERNAME", "CUSTOMERNAME")
               .SetPopupLayout("CUSTOMERNAME", PopupButtonStyles.Ok_Cancel, true, false)
               .SetPopupResultCount(0)
               .SetPosition(2)
               .SetPopupLayoutForm(800, 800, FormBorderStyle.FixedToolWindow)
               .SetLabel("CUSTOMERID")
               .SetPopupAutoFillColumns("CUSTOMERNAME");

            // 팝업에서 사용할 조회조건 항목 추가
            parentCodeClassPopupColumn.Conditions.AddTextBox("CUSTOMERID");
            parentCodeClassPopupColumn.Conditions.AddTextBox("CUSTOMERNAME");

            // 팝업 그리드 설정
            parentCodeClassPopupColumn.GridColumns.AddTextBoxColumn("CUSTOMERID", 150);
            parentCodeClassPopupColumn.GridColumns.AddTextBoxColumn("CUSTOMERNAME", 200);
        }


        private void InitializeGrid_CamUserPopup()
        {
            //추후 변경 예정
            //var parentCodeClassPopupColumn = this.grdAreaPerson.View.AddSelectPopupColumn("USERID", new SqlQuery("GetUserAreaPerson", "10001", $"PLANTID={UserInfo.Current.Plant}"))

            var parentCodeClassPopupColumn = Conditions.AddSelectPopup("P_SPECOWNER", new SqlQuery("GetUserAreaPerson", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "USERNAME", "USERNAME")
               .SetPopupLayout("USERNAME", PopupButtonStyles.Ok_Cancel, true, false)
               .SetPopupResultCount(0)
               .SetPosition(12)
               .SetPopupLayoutForm(800, 800, FormBorderStyle.FixedToolWindow)
               .SetLabel("SPECPERSON")
               .SetPopupAutoFillColumns("USERNAME");

            // 팝업에서 사용할 조회조건 항목 추가
            parentCodeClassPopupColumn.Conditions.AddTextBox("USERID");
            parentCodeClassPopupColumn.Conditions.AddTextBox("USERNAME");

            // 팝업 그리드 설정
            parentCodeClassPopupColumn.GridColumns.AddTextBoxColumn("USERID", 150);
            parentCodeClassPopupColumn.GridColumns.AddTextBoxColumn("USERNAME", 200);
        }


        private void InitializeGrid_SpecUserPopup()
        {
            //추후 변경 예정
            //var parentCodeClassPopupColumn = this.grdAreaPerson.View.AddSelectPopupColumn("USERID", new SqlQuery("GetUserAreaPerson", "10001", $"PLANTID={UserInfo.Current.Plant}"))

            var parentCodeClassPopupColumn = Conditions.AddSelectPopup("P_CAMCOWNER", new SqlQuery("GetUserAreaPerson", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "USERNAME", "USERNAME")
               .SetPopupLayout("USERNAME", PopupButtonStyles.Ok_Cancel, true, false)
               .SetPopupResultCount(0)
               .SetPosition(11)
               .SetPopupLayoutForm(800, 800, FormBorderStyle.FixedToolWindow)
               .SetLabel("CAMMAN")
               .SetPopupAutoFillColumns("USERNAME");

            // 팝업에서 사용할 조회조건 항목 추가
            parentCodeClassPopupColumn.Conditions.AddTextBox("USERID");
            parentCodeClassPopupColumn.Conditions.AddTextBox("USERNAME");

            // 팝업 그리드 설정
            parentCodeClassPopupColumn.GridColumns.AddTextBoxColumn("USERID", 150);
            parentCodeClassPopupColumn.GridColumns.AddTextBoxColumn("USERNAME", 200);
        }


        /// <summary>
        /// 검색조건 팝업 예제
        /// </summary>
        private void InitializeCondition_Popup()
        {
            //팝업 컬럼 설정
            var parentPopupColumn = Conditions.AddSelectPopup("ITEMID", new SqlQuery("GetProductItemGroup", "10002", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "ITEMID", "ITEMID")
               .SetPopupLayout("ITEMID", PopupButtonStyles.Ok_Cancel, true, false)  //팝업창 UI 설정.
               .SetPopupResultCount(1)  //팝업창 선택가능한 개수
               .SetPosition(5)
               .SetPopupApplySelection((selectRow, gridRow) => {

                   List<string> productRevisionList = new List<string>();
                   List<string> productNameList = new List<string>();
                   selectRow.AsEnumerable().ForEach(r => {
                       productRevisionList.Add(Format.GetString(r["ITEMVERSION"]));
                       productNameList.Add(Format.GetString(r["ITEMNAME"]));
                   });

                   Conditions.GetControl<SmartTextBox>("P_ITEMVERSION").EditValue = string.Join(",", productRevisionList);
                   Conditions.GetControl<SmartTextBox>("P_ITEMNAME").EditValue = string.Join(",", productNameList);
               });

            parentPopupColumn.Conditions.AddTextBox("ITEMID");
            parentPopupColumn.Conditions.AddTextBox("ITEMNAME");

            //팝업 그리드
            parentPopupColumn.GridColumns.AddTextBoxColumn("ITEMID", 150);
            parentPopupColumn.GridColumns.AddTextBoxColumn("ITEMNAME", 200);
            parentPopupColumn.GridColumns.AddTextBoxColumn("ITEMVERSION", 100);
            parentPopupColumn.GridColumns.AddTextBoxColumn("SPEC", 250);
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
        /// 입력되었던 데이터들을 초기화한다.
        /// </summary>
        private void ProductDataReset()
        {


            PopupSite.EditValue = null;
            txtregistday.EditValue = null;
            PopupUserSpec.EditValue = null;
            PopupUserCam.EditValue = null;
            txtprevrev.EditValue = null;
            PopupprevItem.EditValue = null;
            txtprevitemname.EditValue = null;
            PopupafterItem.EditValue = null;
            txtafterrev.EditValue = null;
            txtaftername.EditValue = null;
            combojobtype.EditValue = null;
            comboprocesstype.EditValue = null;
            memoolb.EditValue = null;
            combomodeltype.EditValue = null;
            txtysize.EditValue = null;
            txtmodeldate.EditValue = null;
            txtprevversion.EditValue = null;
            txtapplyversion.EditValue = null;
            txtbbtdate.EditValue = null;
            txtmk.EditValue = null;
            txtspecweek.EditValue = null;
            txtcamweek.EditValue = null;
            txtguide.EditValue = null;
            txtvisionpress.EditValue = null;
            //txtDrill.EditValue = null;
            //txtouter.EditValue = null;
            //txtinner.EditValue = null;
            //txtcover.EditValue = null;
            //txtpsr.EditValue = null;
            //txtlayer.EditValue = null;
            //txtcut.EditValue = null;
            //txtpeel.EditValue = null;
            //txtsilk.EditValue = null;
            //txtrouter.EditValue = null;
            //txttool.EditValue = null;
            //txtpanel.EditValue = null;
            //txtetc.EditValue = null;
            txtcomment5.EditValue = null;
            txtcomment1.EditValue = null;
            txtcomment2.EditValue = null;
            txtcomment4.EditValue = null;
            txtcomment3.EditValue = null;
            txtrequestnumber.EditValue = null;
            txtconnectlayer.EditValue = null;
            txtapply.EditValue = null;
            cboapply.EditValue = null;
            cbobbtmethod.EditValue = null;
            cbobbtstand.EditValue = null;
            cbobbttype.EditValue = null;
            cboelo.EditValue = null;
            cbofoil.EditValue = null;
            cboguide.EditValue = null;
            cboinnerlayer.EditValue = null;
            cbojig.EditValue = null;
            cbomanu.EditValue = null;
            cbomicro.EditValue = null;
            cboouterlayer.EditValue = null;
            cbopad.EditValue = null;
            cboreceive.EditValue = null;
            cbosurface1.EditValue = null;
            cbosurface2.EditValue = null;
            cbosurface3.EditValue = null;
            cboterminal.EditValue = null;
            cbotrimming.EditValue = null;
            cbozip.EditValue = null;


        }


        private void LoadData()
        {

        }

        #endregion

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void smartSplitTableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void smartTextBox28_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void smartLabel40_Click(object sender, EventArgs e)
        {

        }

        private void txtapplylayer_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void combojobtype_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void txtcomment2_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void smartLabel7_Click(object sender, EventArgs e)
        {

        }

        private void txtolb_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void memoEdit1_EditValueChanged(object sender, EventArgs e)
        {
                    }

        private void txtlayer_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void smartLabel12_Click(object sender, EventArgs e)
        {

        }

        private void smartLabel11_Click(object sender, EventArgs e)
        {

        }

        private void txtpeel_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void smartLabel64_Click(object sender, EventArgs e)
        {

        }

        private void smartLabel51_Click(object sender, EventArgs e)
        {

        }

        private void txtspecweek_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void smartLabel9_Click(object sender, EventArgs e)
        {

        }

        private void smartLabel64_Click_1(object sender, EventArgs e)
        {

        }
    }
}
