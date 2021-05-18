#region using
using Micube.Framework.Net;
using Micube.Framework;
using Micube.Framework.SmartControls;
using System;
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
using System.IO;
using SmartDeploy.Common;
using Micube.SmartMES.Commons.Controls;
using Micube.SmartMES.StandardInfo.Popup;
using System.Net;
#endregion

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프로그램명 : 기준정보 > 사양진행관리 > CNC Data관리
    /// 업 무 설명 : CNC Data관리
    /// 생  성  자 : 윤성원
    /// 생  성  일 : 2019-12-06
    /// 수정 이 력 : 
    /// 
    /// 
    /// </summary> 
	public partial class CNCDataManagement : SmartConditionManualBaseForm
    {
        #region Local Variables
        private DataTable dtFile;

        Dictionary<SmartLabel, SmartLabel> dicButtonLabel;

        ProductionAuthority ProductUser;
        SampleAuthority SampleUser;
        ProcessType TypeUser;
        private enum FileType
        {
              CNC
            , FIRSTCNC
            , SECOND1CNC
            , SECOND2CNC
            , SECOND3CNC
            , LASERPROC 
            , LASERCNC  
            , ROUTER    
        }

        private enum ProductionAuthority
        {
              Manager
            , Processing
            , SpecManage
            , ProgManage
        }

        private enum SampleAuthority
        {
              Manager
            , SpecManage
            , ProcessManage
        }

        private enum ProcessType
        {
              All
            , First
            , Second
            , LaserCNC
            , PlCut
            , JigCNC
            , Router
            , CLLaser
        }
        #endregion

        #region 생성자
        public CNCDataManagement()
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

            InitializeCondition_Popup();

            dicButtonLabel = new Dictionary<SmartLabel, SmartLabel>();

            dicButtonLabel.Add(txtFirstCNCFileName, lblFirstCNCUser);
            dicButtonLabel.Add(txtSecond1CNCFileName, lblSecond1CNCUser);
            dicButtonLabel.Add(txtSecond2CNCFileName, lblSecond2CNCUser);
            dicButtonLabel.Add(txtSecond3CNCFileName, lblSecond3CNCUser);
            dicButtonLabel.Add(txtLaserProcFileName, lblLaserProcUser);
            dicButtonLabel.Add(txtLaserCNCFileName, lblLaserCNCUser);
            dicButtonLabel.Add(txtRouterFileName, lblRouterUser);
        }
        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeGrid();
            InitializeControl();
           

            EnabledControl(false);
            InitalizeButton();
        }
        /// <summary>        
        /// 코드그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            #region GRID PRODUCTION
            grdProduction.GridButtonItem = GridButtonItem.Import;
            grdProduction.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;

            grdProduction.View.AddTextBoxColumn("CNCNO", 50).SetIsReadOnly().IsHidden = true;
            grdProduction.View.AddTextBoxColumn("NO", 50).SetIsReadOnly().SetTextAlignment(TextAlignment.Right);
            grdProduction.View.AddTextBoxColumn("CUSTOMERNAME"  , 120).SetIsReadOnly().SetTextAlignment(TextAlignment.Left);
            grdProduction.View.AddTextBoxColumn("PRODUCTDEFID", 120).SetIsReadOnly().SetTextAlignment(TextAlignment.Left);
            grdProduction.View.AddTextBoxColumn("PRODUCTDEFNAME", 200).SetIsReadOnly().SetTextAlignment(TextAlignment.Left);
            grdProduction.View.AddTextBoxColumn("CNCTYPE", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdProduction.View.AddTextBoxColumn("PROCESSINGSTATUS", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdProduction.View.AddTextBoxColumn("LOCKSTATE", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);

            grdProduction.View.AddTextBoxColumn("XAXIS", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Right);
            grdProduction.View.AddTextBoxColumn("YAXIS", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Right);
            grdProduction.View.AddTextBoxColumn("ST"   , 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Right);

            grdProduction.View.AddTextBoxColumn("DOWNLOADCOUNT", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);

            grdProduction.View.AddTextBoxColumn("WRITER", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdProduction.View.AddTextBoxColumn("WRITEDATE", 130).SetIsReadOnly().SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime).SetTextAlignment(TextAlignment.Left);

            grdProduction.View.AddTextBoxColumn("MODIFIER", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdProduction.View.AddTextBoxColumn("MODIFIEDTIME", 130).SetIsReadOnly().SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime).SetTextAlignment(TextAlignment.Left);

            //Hidden Column
            //grdProduction.View.AddTextBoxColumn("PRODUCTDEFVERSION").SetIsReadOnly().IsHidden = true;       //품목Rev
            //grdProduction.View.AddTextBoxColumn("CNCWORKTYPE").SetIsReadOnly().IsHidden = true;             //CNC 작업구분
            //grdProduction.View.AddTextBoxColumn("PROGRESSOWNER").SetIsReadOnly().IsHidden = true;           //진도담당자
            //grdProduction.View.AddTextBoxColumn("OUTSOURCINGOWNER").SetIsReadOnly().IsHidden = true;        //외주담당자
            //grdProduction.View.AddTextBoxColumn("FILEID").SetIsReadOnly().IsHidden = true;                  //첨부 파일 ID
            //grdProduction.View.AddTextBoxColumn("FILEPATH").SetIsReadOnly().IsHidden = true;                //첨부 파일 경로
            //grdProduction.View.AddTextBoxColumn("VIEWFILENAMEASFILENAME").SetIsReadOnly().IsHidden = true;  //첨부 파일 명
            //grdProduction.View.AddTextBoxColumn("SPECIFICATIONCOMMENT").SetIsReadOnly().IsHidden = true;    //사양특이사항
            //grdProduction.View.AddTextBoxColumn("OUTSOURCINGCOMMENT").SetIsReadOnly().IsHidden = true;      //외주특이사항
            //grdProduction.View.AddTextBoxColumn("PROGRESSCOMMENT").SetIsReadOnly().IsHidden = true;         //진도특이사항

            grdProduction.View.PopulateColumns();
            #endregion

            #region GRID SAMPLE
            grdSample.GridButtonItem = GridButtonItem.Import;
            grdSample.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;

            var groupDefaultCol = grdSample.View.AddGroupColumn("");

            
            groupDefaultCol.AddTextBoxColumn("NO", 50).SetIsReadOnly().SetTextAlignment(TextAlignment.Right);
            groupDefaultCol.AddTextBoxColumn("CRCNO", 100).SetIsReadOnly().SetTextAlignment(TextAlignment.Right);
            groupDefaultCol.AddTextBoxColumn("PLANTID", 100).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("CUSTOMERID", 120).SetIsReadOnly().SetTextAlignment(TextAlignment.Left);
            groupDefaultCol.AddTextBoxColumn("CUSTOMERNAME", 120).SetIsReadOnly().SetTextAlignment(TextAlignment.Left);
            groupDefaultCol.AddTextBoxColumn("PRODUCTDEFNAME", 200).SetIsReadOnly().SetTextAlignment(TextAlignment.Left);
            groupDefaultCol.AddTextBoxColumn("PRODUCTDEFID", 120).SetIsReadOnly().SetTextAlignment(TextAlignment.Left);
            groupDefaultCol.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            var groupFileCol = grdSample.View.AddGroupColumn("ATTACHEDFILE");

            groupFileCol.AddTextBoxColumn("FIRSTCNCFILE", 80).SetIsReadOnly()  .SetTextAlignment(TextAlignment.Center);
            groupFileCol.AddTextBoxColumn("SECOND1CNCFILE", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            groupFileCol.AddTextBoxColumn("SECOND2CNCFILE", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            groupFileCol.AddTextBoxColumn("SECOND3CNCFILE", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            groupFileCol.AddTextBoxColumn("LASERPROCFILE", 80).SetIsReadOnly() .SetTextAlignment(TextAlignment.Center);
            groupFileCol.AddTextBoxColumn("LASERCNCFILE", 80).SetIsReadOnly()  .SetTextAlignment(TextAlignment.Center);
            groupFileCol.AddTextBoxColumn("ROUTERFILE", 80).SetIsReadOnly()    .SetTextAlignment(TextAlignment.Center);

            var groupDefaultCol2 = grdSample.View.AddGroupColumn("");
            groupDefaultCol2.AddTextBoxColumn("LOCKSTATE", 60).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);

            groupDefaultCol2.AddTextBoxColumn("WRITER", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            groupDefaultCol2.AddTextBoxColumn("WRITEDATE", 130).SetIsReadOnly().SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime).SetTextAlignment(TextAlignment.Left);
            groupDefaultCol2.AddTextBoxColumn("MODIFIER", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            groupDefaultCol2.AddTextBoxColumn("MODIFIEDTIME", 130).SetIsReadOnly().SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime).SetTextAlignment(TextAlignment.Left);
            
            //grdSample.View.AddTextBoxColumn("DOWNLOADCOUNT", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Left);

            //Hidden Column
            //grdSample.View.AddTextBoxColumn("PRODUCTDEFVERSION").SetIsReadOnly().IsHidden = true;   //품목Rev
            //grdSample.View.AddTextBoxColumn("SCALE_X").SetIsReadOnly().IsHidden = true;             //X축
            //grdSample.View.AddTextBoxColumn("SCALE_Y").SetIsReadOnly().IsHidden = true;             //Y축
            //grdSample.View.AddTextBoxColumn("SCALE_ST").SetIsReadOnly().IsHidden = true;            //S/T

            grdSample.View.PopulateColumns();
            #endregion

            #region GRID TRANSACTION
            grdTransaction.GridButtonItem = GridButtonItem.Import;
            grdTransaction.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;

            grdSample.View.AddTextBoxColumn("CNCNO", 50).SetIsReadOnly().IsHidden = true;
            grdTransaction.View.AddTextBoxColumn("NO", 50).SetIsReadOnly().SetTextAlignment(TextAlignment.Right);
            grdTransaction.View.AddTextBoxColumn("CUSTOMERNAME", 200).SetIsReadOnly().SetTextAlignment(TextAlignment.Left);
            grdTransaction.View.AddTextBoxColumn("PRODUCTDEFNAME", 200).SetIsReadOnly().SetTextAlignment(TextAlignment.Left);
            grdTransaction.View.AddTextBoxColumn("PRODUCTDEFID", 120).SetIsReadOnly().SetTextAlignment(TextAlignment.Left);
            grdTransaction.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Left);
            grdTransaction.View.AddTextBoxColumn("CNCTYPE", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdTransaction.View.AddTextBoxColumn("CNCWORKTYPE", 100).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            //grdTransaction.View.AddTextBoxColumn("CONNECTIONTYPE", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);

            grdTransaction.View.AddComboBoxColumn("CONNECTIONTYPE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=CNCConnectionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                               .SetIsReadOnly()
                               .SetTextAlignment(TextAlignment.Center);

            grdTransaction.View.AddTextBoxColumn("MODIFIER", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdTransaction.View.AddTextBoxColumn("MODIFIEDTIME", 130).SetIsReadOnly().SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime).SetTextAlignment(TextAlignment.Left);

            grdTransaction.View.PopulateColumns();
            #endregion

            #region GRID CAM PART
            grdCAMPartSendList.GridButtonItem = GridButtonItem.Import;
            grdCAMPartSendList.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;

            grdCAMPartSendList.View.AddTextBoxColumn("NO", 50).SetIsReadOnly().SetTextAlignment(TextAlignment.Right);
            grdCAMPartSendList.View.AddTextBoxColumn("WRITEDATE", 130).SetIsReadOnly().SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime).SetTextAlignment(TextAlignment.Center);
            grdCAMPartSendList.View.AddTextBoxColumn("COMMENTS", 200).SetIsReadOnly().SetTextAlignment(TextAlignment.Left);
            
            grdCAMPartSendList.View.AddTextBoxColumn("WRITER", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdCAMPartSendList.View.AddTextBoxColumn("ATTACHEDFILE", 120).SetIsReadOnly().SetTextAlignment(TextAlignment.Left);

            grdCAMPartSendList.View.PopulateColumns();
            #endregion
        }
        #endregion

        #region 컨텐츠 영역 초기화
        private void InitializeControl()
        {
            
            Control[] controls = pnlToolbar.Controls.Find("New", true);

            SmartButton btnNew = new SmartButton();

            if (controls.Count() > 0)
            {
                btnNew = (SmartButton)controls[0];
            }

            controls = pnlToolbar.Controls.Find("Input", true);

            SmartButton btnInput = new SmartButton();

            if (controls.Count() > 0)
            {
                btnInput = (SmartButton)controls[0];
            }

            controls = pnlToolbar.Controls.Find("Regist", true);

            SmartButton btnRegist = new SmartButton();

            if (controls.Count() > 0)
            {
                btnRegist = (SmartButton)controls[0];
            }

            btnRegist.Visible = false;

            btnNew.Visible = true;
            btnInput.Visible = true;

            if (btnNew.Enabled == false)
                btnNew.Enabled = true;

            if (btnInput.Enabled == false)
                btnInput.Enabled = true;

            tabIdManagement.TabPages[0].Text = Language.Get("CNCDATAPRODUCTION");
            tabIdManagement.TabPages[1].Text = Language.Get("CNCDATASAMPLE");
            tabIdManagement.TabPages[2].Text = Language.Get("CNCDATATRANSACTION");
            tabIdManagement.TabPages[3].Text = Language.Get("CAMPARTNOTICEMESSAGE");

            tabIdManagement.TabPages[0].Appearance.Header.BackColor = Color.Blue;
            tabIdManagement.TabPages[1].Appearance.Header.BackColor = Color.Yellow;

            // 고객명
            ConditionItemSelectPopup customerCode = new ConditionItemSelectPopup();
            customerCode.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            customerCode.SetPopupLayout("CUSTOMERID", PopupButtonStyles.Ok_Cancel);

            customerCode.Id = "CUSTOMERID";
            customerCode.LabelText = "CUSTOMERNAME";
            customerCode.SearchQuery = new SqlQuery("GetCustomerList", "10002", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}");
            customerCode.IsMultiGrid = false;
            customerCode.DisplayFieldName = "CUSTOMERNAME";
            customerCode.ValueFieldName = "CUSTOMERID";
            customerCode.LanguageKey = "CUSTOMERID";

            customerCode.Conditions.AddTextBox("CUSTOMERID");
            customerCode.Conditions.AddTextBox("CUSTOMERNAME");
            customerCode.GridColumns.AddTextBoxColumn("CUSTOMERID", 80);
            customerCode.GridColumns.AddTextBoxColumn("CUSTOMERNAME", 150);

            sspCustomerNamePro.SelectPopupCondition = customerCode;
            sspCustomerNameSam.SelectPopupCondition = customerCode;

            // 품목
            ConditionItemSelectPopup prodDefId = new ConditionItemSelectPopup();
            prodDefId.SetPopupLayoutForm(600, 600, FormBorderStyle.FixedDialog);
            prodDefId.SetPopupLayout("PRODUCTDEFID", PopupButtonStyles.Ok_Cancel);
            prodDefId.Id = "PRODUCTDEFID";
            prodDefId.LabelText = "PRODUCTDEF";
            prodDefId.SearchQuery = new SqlQuery("GetProductDefList", "10003", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}");
            prodDefId.IsMultiGrid = false;
            prodDefId.DisplayFieldName = "PRODUCTDEFID";
            prodDefId.ValueFieldName = "PRODUCTDEFID";
            prodDefId.LanguageKey = "PRODUCTDEF";
            prodDefId.SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
            {
                foreach (DataRow row in selectedRow)
                {
                    if (selectedRow.Count() > 0)
                    {
                        if (tabIdManagement.SelectedTabPageIndex == 0)
                        {
                            //txtProductDefVersionPro.EditValue = row["PRODUCTDEFID"].ToString();
                            txtProductDefNamePro.EditValue = row["PRODUCTDEFNAME"].ToString();
                            txtProductDefVersionPro.EditValue = row["PRODUCTDEFVERSION"].ToString();
                            sspCustomerNamePro.SetValue(row["CUSTOMERID"].ToString());
                            sspCustomerNamePro.Text = row["CUSTOMERNAME"].ToString();
                        }
                        else
                        {
                            txtProductDefNameSam.EditValue = row["PRODUCTDEFNAME"].ToString();
                            txtProductDefVersionSam.EditValue = row["PRODUCTDEFVERSION"].ToString();
                            sspCustomerNameSam.SetValue(row["CUSTOMERID"].ToString());
                            sspCustomerNameSam.Text = row["CUSTOMERNAME"].ToString();
                        }
                    }

                    //txtProductDefVersionPro.EditValue = prodDefVersion;
                    //txtProductDefNamePro.EditValue = prodDefName;
                }
            });
            prodDefId.Conditions.AddTextBox("PRODUCTDEF");
            //prodDefId.Conditions.AddTextBox("PRODUCTDEFNAME");
            prodDefId.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            prodDefId.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            prodDefId.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 60);
            prodDefId.GridColumns.AddTextBoxColumn("CUSTOMERID", 100);
            prodDefId.GridColumns.AddTextBoxColumn("CUSTOMERNAME", 150);

            sspProductDefIdPro.SelectPopupCondition = prodDefId;
            sspProductDefIdSam.SelectPopupCondition = prodDefId;

            // 진행정보
            InitializeComboBox(cboProcessType, "CNCProgressStatus");
            // CNC 작업구분
            InitializeComboBox(cboCNCWorkType, "CNCWorkType");
            //InitializeComboBox(cboCNCTypeSam, "CNCWorkType");
            
            // 잠금 상태
            InitializeComboBox(cboHoldStatePro, "LockState");
            InitializeComboBox(cboHoldStateSam, "LockState");
            
            // CNC 구분
            InitializeComboBox(cboCNCTypePro, "CNCType");
            //InitializeComboBox(cboCNCTypeSam, "CNCType");
        }
        
        //Sample Init
        private void InitializeSampleTab()
        {
            //CNC NO
            txtCNCNoSam.EditValue = string.Empty;
            //등록자
            txtWriterSam.EditValue = UserInfo.Current.Name;
            txtWriterSam.Tag = UserInfo.Current.Id;
            //고객명
            sspCustomerNameSam.SetValue(null);
            //CNC구분
            //cboCNCTypeSam.EditValue = string.Empty;
            //잠금상태
            cboHoldStateSam.EditValue = string.Empty;

            //등록일
            cboWriteDateSam.DateTime = DateTime.Now;
            //품목코드
            sspProductDefIdSam.SetValue(null);
            //품목버전
            txtProductDefVersionSam.EditValue = string.Empty;
            //품목명
            txtProductDefNameSam.EditValue = string.Empty;

            ////X
            //txtScaleXSam.EditValue = string.Empty;
            ////Y
            //txtScaleYSam.EditValue = string.Empty;
            ////S/T
            //txtScaleSTSam.EditValue = string.Empty;

            //1차 CNC 특이사항
            txtFirstCNCComment.EditValue = string.Empty;
            //2차 CNC 특이사항
            txtSecondCNCComment.EditValue = string.Empty;
            //LASER 가공 특이사항
            txtLastProcComment.EditValue = string.Empty;
            //LASER CNC 특이사항
            txtLaserCNCComment.EditValue = string.Empty;
            //ROUTER 특이사항
            txtRouterComment.EditValue = string.Empty;

            //1차 CNC
            txtFirstCNCScaleX.EditValue = string.Empty;
            txtFirstCNCScaleY.EditValue = string.Empty;
            txtFirstCNCFileName.Tag = string.Empty;
            txtFirstCNCFileName.Text = string.Empty;
            txtSelectFirstCNCFilePath.Text = string.Empty;
            lblFirstCNCUser.Text = string.Empty;
            lblFirstCNCUser.Tag = FileType.FIRSTCNC;
            //2차 CNC-1
            txtSecond1CNCScaleX.EditValue = string.Empty;
            txtSecond1CNCScaleY.EditValue = string.Empty;
            txtSecond1CNCScaleST.EditValue = string.Empty;

            txtSecond1CNCFileName.Text = string.Empty;
            txtSelectSecond1CNCFilePath.Text = string.Empty;
            txtSecond1CNCFileName.Tag = string.Empty;
            lblSecond1CNCUser.Text = string.Empty;
            lblSecond1CNCUser.Tag = FileType.SECOND1CNC;
            //2차 CNC-2
            txtSecond2CNCScaleX.EditValue = string.Empty;
            txtSecond2CNCScaleY.EditValue = string.Empty;
            txtSecond2CNCScaleST.EditValue = string.Empty;
            txtSecond2CNCFileName.Tag = string.Empty;
            txtSecond2CNCFileName.Text = string.Empty;
            txtSelectSecond2CNCFilePath.Text = string.Empty;
            lblSecond2CNCUser.Text = string.Empty;
            lblSecond2CNCUser.Tag = FileType.SECOND2CNC;
            //2차 CNC-3
            txtSecond3CNCScaleX.EditValue = string.Empty;
            txtSecond3CNCScaleY.EditValue = string.Empty;
            txtSecond3CNCScaleST.EditValue = string.Empty;
            txtSecond3CNCFileName.Tag = string.Empty;
            txtSecond3CNCFileName.Text = string.Empty;
            txtSelectSecond3CNCFilePath.Text = string.Empty;
            lblSecond3CNCUser.Text = string.Empty;
            lblSecond3CNCUser.Tag = FileType.SECOND3CNC;
            //LASER 가공
            txtLaserProcScaleX.EditValue = string.Empty;
            txtLaserProcScaleY.EditValue = string.Empty;
            txtLaserProcFileName.Tag = string.Empty;
            txtLaserProcFileName.Text = string.Empty;
            lblLaserProcUser.Text = string.Empty;
            lblLaserProcUser.Tag = FileType.LASERPROC;
            txtSelectLaserProcFilePath.Text = string.Empty;
            //LASER CNC
            txtLaserCNCFileName.Tag = string.Empty;
            txtLaserCNCFileName.Text = string.Empty;
            txtSelectLaserCNCFilePath.Text = string.Empty;
            lblLaserCNCUser.Text = string.Empty;
            lblLaserCNCUser.Tag = FileType.LASERCNC;
            //ROUTER
            txtRouterFileName.Tag = string.Empty;
            txtRouterFileName.Text = string.Empty;
            lblRouterUser.Text = string.Empty;
            lblRouterUser.Tag = FileType.ROUTER;
            txtSelectRouterFilePath.Text = string.Empty;
        }

        //Production Init
        private void InitializeProductionTab()
        {
            //CNC NO
            txtCNCNoPro.EditValue = string.Empty;
            //등록자
            txtWriterPro.EditValue = UserInfo.Current.Name;
            txtWriterPro.Tag = UserInfo.Current.Id;
            //고객명
            sspCustomerNamePro.SetValue(null);
            //품목코드
            sspProductDefIdPro.SetValue(null);
            //품목버전
            txtProductDefVersionPro.EditValue = string.Empty;
            //품목명
            txtProductDefNamePro.EditValue = string.Empty;
            //진행 정보
            cboProcessType.EditValue = string.Empty;
            //등록일
            cboWriteDatePro.DateTime = DateTime.Now;
            //CNC구분
            cboCNCTypePro.EditValue = string.Empty;
            //CNC 작업구분
            cboCNCWorkType.EditValue = string.Empty;
            //잠금상태
            cboHoldStatePro.EditValue = string.Empty;
            //진도담당자
            txtProgressOwner.EditValue = string.Empty;
            //외주담당자
            txtOutsourcingOwner.EditValue = string.Empty;
            //X
            txtScaleXPro.EditValue = string.Empty;
            //Y
            txtScaleYPro.EditValue = string.Empty;
            //S/T
            txtScaleSTPro.EditValue = string.Empty;
            //사양특이사항
            txtSpecComment.EditValue = string.Empty;
            //외주특이사항
            txtOutsComment.EditValue = string.Empty;
            //진도특이사항
            txtProcComment.EditValue = string.Empty;
            //첨부파일
            txtFileName.Text = string.Empty;
            //첨부파일선택
            txtSelectFilePath.EditValue = string.Empty;
        }
        
        //Tab Init
        private void InitializeTabControl()
        {
            InitializeProductionTab();

            InitializeSampleTab();

            EnabledControl(false);
        }

        private void InitalizeButton()
        {
            Control[] controls = pnlToolbar.Controls.Find("Save", true);

            SmartButton btnSave = new SmartButton();

            if (controls.Count() > 0)
            {
                btnSave = (SmartButton)controls[0];
            }

            controls = pnlToolbar.Controls.Find("New", true);

            SmartButton btnNew = new SmartButton();

            if (controls.Count() > 0)
            {
                btnNew = (SmartButton)controls[0];
            }

            controls = pnlToolbar.Controls.Find("Input", true);

            SmartButton btnInput = new SmartButton();

            if (controls.Count() > 0)
            {
                btnInput = (SmartButton)controls[0];
            }

            controls = pnlToolbar.Controls.Find("Regist", true);

            SmartButton btnRegist = new SmartButton();

            if (controls.Count() > 0)
            {
                btnRegist = (SmartButton)controls[0];
                btnRegist.Visible = false;
            }

            controls = pnlToolbar.Controls.Find("Authority", true);

            SmartButton btnAuthority = new SmartButton();

            if (controls.Count() > 0)
            {
                btnAuthority = (SmartButton)controls[0];
                btnAuthority.Visible = false;
            }

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("PLANTID", UserInfo.Current.Plant);
            param.Add("USERIDNAME", UserInfo.Current.Id);

            DataTable dtUserAuthority = SqlExecuter.Query("GetCNCUserAuthorityList", "10001", param);

            if (dtUserAuthority != null && dtUserAuthority.Rows.Count != 0)
            {
                if (!dtUserAuthority.Rows[0]["PRODUCTIONAUTHORITY"].ToString().Equals(""))
                    ProductUser = dtUserAuthority.Rows[0]["PRODUCTIONAUTHORITY"].ToString().ToEnum<ProductionAuthority>();
                if (!dtUserAuthority.Rows[0]["SAMPLEAUTHORITY"].ToString().Equals(""))
                    SampleUser = dtUserAuthority.Rows[0]["SAMPLEAUTHORITY"].ToString().ToEnum<SampleAuthority>();
                if (!dtUserAuthority.Rows[0]["PROCESSTYPE"].ToString().Equals(""))
                    TypeUser = dtUserAuthority.Rows[0]["PROCESSTYPE"].ToString().ToEnum<ProcessType>();
            }
            btnAuthority.Visible = false;
            btnNew.Visible = false;
            btnInput.Visible = false;
            btnRegist.Visible = false;
            btnSave.Visible = false;

            if (ProductUser == ProductionAuthority.Manager)
            {

                btnAuthority.Visible = true;
                btnNew.Visible = true;
                btnInput.Visible = true;
                btnSave.Visible = true;
            }
            else if (ProductUser == ProductionAuthority.ProgManage)
                btnInput.Visible = true;
            else if (ProductUser == ProductionAuthority.SpecManage)
            {
                btnNew.Visible = true;
                btnSave.Visible = true;
            }
            if (dtUserAuthority.Rows.Count > 0)
            {
                if (dtUserAuthority.Rows[0]["PRODUCTIONAUTHORITY"].ToString().Equals(""))
                {
                    btnAuthority.Visible = false;
                    btnNew.Visible = false;
                    btnInput.Visible = false;
                    btnRegist.Visible = false;
                    btnSave.Visible = false;
                }
            }
            if (dtUserAuthority.Rows.Count == 0)
            {
                btnAuthority.Visible = false;
                btnNew.Visible = false;
                btnInput.Visible = false;
                btnRegist.Visible = false;
                btnSave.Visible = false;
            }

        }

        //컨트롤 활성화
        private void EnabledControl(bool isEnabled)
        {
            #region 양산
            //첨부파일
            txtFileName.Enabled = false;
            //등록자
            txtWriterPro.Enabled = isEnabled;
            //고객명
            sspCustomerNamePro.Enabled = false;
            //품목코드
            sspProductDefIdPro.Enabled = isEnabled;
            //품목버전
            txtProductDefVersionPro.Enabled = isEnabled;
            //진행 정보
            //cboProcessType.Enabled = isEnabled;
            //등록일
            cboWriteDatePro.Enabled = isEnabled;
            //CNC구분
            cboCNCTypePro.Enabled = isEnabled;
            //CNC 작업구분
            cboCNCWorkType.Enabled = isEnabled;
            //잠금상태
            cboHoldStatePro.Enabled = isEnabled;
            //진도담당자
            txtProgressOwner.Enabled = isEnabled;
            //외주담당자
            txtOutsourcingOwner.Enabled = isEnabled;
            //X
            txtScaleXPro.Enabled = isEnabled;
            //Y
            txtScaleYPro.Enabled = isEnabled;
            //S/T
            txtScaleSTPro.Enabled = isEnabled;
            //사양특이사항
            txtSpecComment.Enabled = isEnabled;
            //외주특이사항
            txtOutsComment.Enabled = isEnabled;
            //진도특이사항
            txtProcComment.Enabled = isEnabled;
            //첨부파일선택
            txtSelectFilePath.Enabled = isEnabled;

            btnSelectFile.Enabled = isEnabled;
            #endregion

            if (ProductUser == ProductionAuthority.Manager || ProductUser == ProductionAuthority.Processing)
            {
                if (TypeUser == ProcessType.All || TypeUser.ToString().Contains(cboCNCTypePro.EditValue.ToString()))
                    txtFileName.Enabled = true;
            }

            #region 샘플
            //첨부파일
            txtFirstCNCFileName.Enabled = false;
            txtSecond1CNCFileName.Enabled = false;
            txtSecond2CNCFileName.Enabled = false;
            txtSecond3CNCFileName.Enabled = false;
            txtLaserProcFileName.Enabled = false;
            txtLaserCNCFileName.Enabled = false;
            txtRouterFileName.Enabled = false;
            //등록자
            txtWriterSam.Enabled = isEnabled;
            //고객명
            sspCustomerNameSam.Enabled = false;
            //CNC구분
            //cboCNCTypeSam.Enabled = isEnabled;
            //잠금상태
            cboHoldStateSam.Enabled = isEnabled;

            //등록일
            cboWriteDateSam.Enabled = isEnabled;
            //품목코드
            sspProductDefIdSam.Enabled = isEnabled;
            //품목버전
            txtProductDefVersionSam.Enabled = isEnabled;
            //품목명
            txtProductDefNameSam.Enabled = isEnabled;

            ////X
            //txtScaleXSam.Enabled = isEnabled;
            ////Y
            //txtScaleYSam.Enabled = isEnabled;
            ////S/T
            //txtScaleSTSam.Enabled = isEnabled;

            //1차 CNC 특이사항
            txtFirstCNCComment.Enabled = isEnabled;
            //2차 CNC 특이사항
            txtSecondCNCComment.Enabled = isEnabled;
            //LASER 가공 특이사항
            txtLastProcComment.Enabled = isEnabled;
            //LASER CNC 특이사항
            txtLaserCNCComment.Enabled = isEnabled;
            //ROUTER 특이사항
            txtRouterComment.Enabled = isEnabled;

            //1차 CNC
            txtFirstCNCScaleX.Enabled = isEnabled;
            txtFirstCNCScaleY.Enabled = isEnabled;
            //2차 CNC-1
            txtSecond1CNCScaleX.Enabled = isEnabled;
            txtSecond1CNCScaleY.Enabled = isEnabled;
            txtSecond1CNCScaleST.Enabled = isEnabled;
            //2차 CNC-2
            txtSecond2CNCScaleX.Enabled = isEnabled;
            txtSecond2CNCScaleY.Enabled = isEnabled;
            txtSecond2CNCScaleST.Enabled = isEnabled;
            //2차 CNC-3
            txtSecond3CNCScaleX.Enabled = isEnabled;
            txtSecond3CNCScaleY.Enabled = isEnabled;
            txtSecond3CNCScaleST.Enabled = isEnabled;
            //LASER 가공
            txtLaserProcScaleX.Enabled = isEnabled;
            txtLaserProcScaleY.Enabled = isEnabled;

            btnSelectFirstCNCFile.Enabled = isEnabled;
            btnSelectSecond1CNCFile.Enabled = isEnabled;
            btnSelectSecond2CNCFile.Enabled = isEnabled;
            btnSelectSecond3CNCFile.Enabled = isEnabled;
            btnSelectLaserProcFile.Enabled = isEnabled;
            btnSelectLaserCNCFile.Enabled = isEnabled;
            btnSelectRouterFile.Enabled = isEnabled;
            #endregion

            if (SampleUser == SampleAuthority.Manager || SampleUser == SampleAuthority.ProcessManage)
            {
                txtFirstCNCFileName.Enabled = true;
                txtSecond1CNCFileName.Enabled = true;
                txtSecond2CNCFileName.Enabled = true;
                txtSecond3CNCFileName.Enabled = true;
                txtLaserProcFileName.Enabled = true;
                txtLaserCNCFileName.Enabled = true;
                txtRouterFileName.Enabled = true;
            }
        }

        //Tab ComboBox
        private void InitializeComboBox(SmartComboBox smartComboBox, string codeClass )
        {
            // 잠금 상태
            smartComboBox.DisplayMember = "CODENAME";
            smartComboBox.ValueMember = "CODEID";

            Dictionary<string, object> paramLock = new Dictionary<string, object>();

            paramLock.Add("CODECLASSID", codeClass);
            paramLock.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable dt = SqlExecuter.Query("GetCodeList", "00001", paramLock);

            smartComboBox.DataSource = dt;

            if (dt.Rows.Count != 0)
            {
                smartComboBox.EditValue = dt.Rows[0]["CODENAME"];
            }

            smartComboBox.ShowHeader = false;
        }

        private void InitializeComboBox(SmartCheckedComboBox smartComboBox, string codeClass)
        {
            // 잠금 상태
            smartComboBox.DisplayMember = "CODENAME";
            smartComboBox.ValueMember = "CODEID";

            Dictionary<string, object> paramLock = new Dictionary<string, object>();

            paramLock.Add("CODECLASSID", codeClass);
            paramLock.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable dt = SqlExecuter.Query("GetCodeList", "00001", paramLock);

            smartComboBox.DataSource = dt;

            if (dt.Rows.Count != 0)
            {
                smartComboBox.EditValue = dt.Rows[0]["CODENAME"];
            }

            smartComboBox.ShowHeader = false;
            smartComboBox.SetVisibleColumns("CODENAME");
        }
        #endregion

        private void BindingSampleTab(DataRow row)
        {
            //CNC NO
            txtCNCNoSam.EditValue = row["CNCNO"].ToString();
            //등록자
            txtWriterSam.EditValue = row["WRITER"].ToString();
            //고객명
            sspCustomerNameSam.SetValue(row["CUSTOMERID"].ToString());
            //CNC구분
            //cboCNCTypeSam.EditValue = row["CNCTYPE"].ToString();
            //잠금상태
            cboHoldStateSam.EditValue = row["LOCKSTATE"].ToString();

            //등록일
            cboWriteDateSam.DateTime = Convert.ToDateTime(row["WRITEDATE"].ToString());
            //품목코드
            sspProductDefIdSam.SetValue(row["PRODUCTDEFID"].ToString());
            //품목버전
            txtProductDefVersionSam.EditValue = row["PRODUCTDEFVERSION"].ToString();
            //품목명
            txtProductDefNameSam.EditValue = row["PRODUCTDEFNAME"].ToString();

            ////X
            //txtScaleXSam.EditValue = row["XAXIS"].ToString();
            ////Y
            //txtScaleYSam.EditValue = row["YAXIS"].ToString();
            ////S/T
            //txtScaleSTSam.EditValue = row["ST"].ToString();

            //1차 CNC 특이사항
            txtFirstCNCComment.EditValue = row["FIRSTCNCCOMMENT"].ToString();
            //2차 CNC 특이사항
            txtSecondCNCComment.EditValue = row["SECONDCNCCOMMENT"].ToString();
            //LASER 가공 특이사항
            txtLastProcComment.EditValue = row["LASERPROCCOMMENT"].ToString();
            //LASER CNC 특이사항
            txtLaserCNCComment.EditValue = row["LASERCNCCOMMENT"].ToString();
            //ROUTER 특이사항
            txtRouterComment.EditValue = row["ROUTERCOMMENT"].ToString();

            //1차 CNC
            txtFirstCNCScaleX.EditValue = row["FIRSTCNCSCALE_X"].ToString();
            txtFirstCNCScaleY.EditValue = row["FIRSTCNCSCALE_Y"].ToString();
            
            txtFirstCNCFileName.Text = row["FIRSTCNCFILENAME"].ToString();
            txtFirstCNCFileName.Tag = row["FIRSTCNCFILEID"].ToString();

            lblFirstCNCUser.Text = row["FIRSTCNCUSER"].ToString();
            lblFirstCNCUser.Tag = row["FIRSTCNCUSERID"].ToString();

            lblFirstCNCUserPhone.Text = row["FIRSTCNCUSERPHONE"].ToString();
            //2차 CNC-1
            txtSecond1CNCScaleX.EditValue = row["SECOND1CNCSCALE_X"].ToString();
            txtSecond1CNCScaleY.EditValue = row["SECOND1CNCSCALE_Y"].ToString();
            txtSecond1CNCScaleST.EditValue = row["SECOND1CNCSCALE_ST"].ToString();
            
            txtSecond1CNCFileName.Text = row["SECOND1CNCFILENAME"].ToString();
            txtSecond1CNCFileName.Tag = row["SECOND1CNCFILEID"].ToString();

            lblSecond1CNCUser.Text = row["SECOND1CNCUSER"].ToString();
            lblSecond1CNCUser.Tag = row["SECOND1CNCUSERID"].ToString();

            lblFirstCNCUserPhone.Text = row["SECOND1CNCUSERPHONE"].ToString();
            //2차 CNC-2
            txtSecond2CNCScaleX.EditValue = row["SECOND2CNCSCALE_X"].ToString();
            txtSecond2CNCScaleY.EditValue = row["SECOND2CNCSCALE_Y"].ToString();
            txtSecond2CNCScaleST.EditValue = row["SECOND2CNCSCALE_ST"].ToString();

            txtSecond2CNCFileName.Text = row["SECOND2CNCFILENAME"].ToString();
            txtSecond2CNCFileName.Tag = row["SECOND2CNCFILEID"].ToString();

            lblSecond2CNCUser.Text = row["SECOND2CNCUSERID"].ToString();
            lblSecond2CNCUser.Tag = row["SECOND2CNCUSER"].ToString();

            lblFirstCNCUserPhone.Text = row["SECOND2CNCUSERPHONE"].ToString();
            //2차 CNC-3
            txtSecond3CNCScaleX.EditValue = row["SECOND3CNCSCALE_X"].ToString();
            txtSecond3CNCScaleY.EditValue = row["SECOND3CNCSCALE_Y"].ToString();
            txtSecond3CNCScaleST.EditValue = row["SECOND3CNCSCALE_ST"].ToString();

            txtSecond3CNCFileName.Text = row["SECOND3CNCFILENAME"].ToString();
            txtSecond3CNCFileName.Tag = row["SECOND3CNCFILEID"].ToString();

            lblSecond3CNCUser.Text = row["SECOND3CNCUSERID"].ToString();
            lblSecond3CNCUser.Tag = row["SECOND3CNCUSER"].ToString();

            lblFirstCNCUserPhone.Text = row["SECOND3CNCUSERPHONE"].ToString();
            //LASER 가공
            txtLaserProcScaleX.EditValue = row["LASERPROCSCALE_X"].ToString();
            txtLaserProcScaleY.EditValue = row["LASERPROCSCALE_Y"].ToString();

            txtLaserProcFileName.Text = row["LASERPROCFILENAME"].ToString();
            txtLaserProcFileName.Tag = row["LASERPROCFILEID"].ToString();

            lblLaserProcUser.Text = row["LASERPROCUSERID"].ToString();
            lblLaserProcUser.Tag = row["LASERPROCUSER"].ToString();

            lblFirstCNCUserPhone.Text = row["LASERPROCUSERPHONE"].ToString();
            //LASER CNC
            txtLaserCNCFileName.Text = row["LASERCNCFILENAME"].ToString();
            txtLaserCNCFileName.Tag = row["LASERCNCFILEID"].ToString();

            lblLaserCNCUser.Text = row["LASERCNCUSERID"].ToString();
            lblLaserCNCUser.Tag = row["LASERCNCUSER"].ToString();

            lblFirstCNCUserPhone.Text = row["LASERCNCUSERPHONE"].ToString();
            //ROUTER
            txtRouterFileName.Text = row["ROUTERFILENAME"].ToString();
            txtRouterFileName.Tag = row["ROUTERFILEID"].ToString();

            lblRouterUser.Text = row["ROUTERUSERID"].ToString();
            lblRouterUser.Tag = row["ROUTERUSER"].ToString();

            lblFirstCNCUserPhone.Text = row["ROUTERUSERPHONE"].ToString();
        }

        private void BindingProductionTab(DataRow row)
        {
            //CNC NO
            txtCNCNoPro.EditValue = row["CNCNO"].ToString();
            //등록자
            txtWriterPro.EditValue = row["WRITER"].ToString();
            //고객명
            sspCustomerNamePro.SetValue(row["CUSTOMERID"].ToString());
            //품목코드
            sspProductDefIdPro.SetValue(row["PRODUCTDEFID"].ToString());
            //품목명
            txtProductDefNamePro.EditValue = row["PRODUCTDEFNAME"].ToString();
            //품목버전
            txtProductDefVersionPro.EditValue = row["PRODUCTDEFVERSION"].ToString();
            //진행 정보
            cboProcessType.EditValue = row["PROCESSINGSTATUSCODE"].ToString();
            //등록일
            cboWriteDatePro.DateTime = Convert.ToDateTime(row["WRITEDATE"].ToString());
            //CNC구분
            cboCNCTypePro.EditValue = row["CNCTYPE"].ToString();
            //CNC 작업구분
            cboCNCWorkType.EditValue = row["CNCWORKTYPE"].ToString();
            //잠금상태
            cboHoldStatePro.EditValue = row["LOCKSTATE"].ToString();
            //진도담당자
            txtProgressOwner.EditValue = row["PROGRESSOWNER"].ToString();
            //외주담당자
            txtOutsourcingOwner.EditValue = row["OUTSOURCINGOWNER"].ToString();
            //X
            txtScaleXPro.EditValue = row["XAXIS"].ToString();
            //Y
            txtScaleYPro.EditValue = row["YAXIS"].ToString();
            //S/T
            txtScaleSTPro.EditValue = row["ST"].ToString();
            //사양특이사항
            txtSpecComment.EditValue = row["SPECIFICATIONCOMMENT"].ToString();
            //외주특이사항
            txtOutsComment.EditValue = row["OUTSOURCINGCOMMENT"].ToString();
            //진도특이사항
            txtProcComment.EditValue = row["PROGRESSCOMMENT"].ToString();
            //첨부파일
            txtFileName.Text = row["FILENAME"].ToString();
            txtFileName.Tag = row["FILEID"].ToString();
            //첨부파일선택
            //txtSelectFilePro.EditValue = row[""].ToString();
        }

        #region 툴바
        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarClick(ToolbarClickEventArgs e)
        {
            base.OnToolbarClick(e);

            Control[] controls = pnlToolbar.Controls.Find(e.Id, true);

            SmartButton btn = new SmartButton();

            if (controls.Count() > 0)// && controls[0] is SmartButton btnSave)
            {
                btn = (SmartButton)controls[0];
            }

            switch (e.Id)
            {
                case "New":
                    if (btn.Enabled == true)
                        btn.Enabled = false;

                    InitializeTabControl();

                    EnabledControl(true);

                    cboProcessType.ItemIndex = 0;
                    cboCNCTypePro.ItemIndex = 0;
                    cboHoldStatePro.ItemIndex = 0;
                    cboHoldStateSam.ItemIndex = 0;
                    break;
                case "Input":
                    if (cboProcessType.ItemIndex == 0)
                    {
                        txtProgressOwner.EditValue = UserInfo.Current.Name;
                        txtProgressOwner.Tag = UserInfo.Current.Id;

                        cboProcessType.ItemIndex = 1;

                        

                        DataSet ds = new DataSet();
                        ProductionSave(ds, 1);
                        ShowMessage("CompleteInput");
                        OnSearchAsync();


                 

                    }
                    break;
                case "Regist":
                    CNCDataPopup cncDataPopup = new CNCDataPopup();
                    cncDataPopup.StartPosition = FormStartPosition.CenterParent;

                    if (cncDataPopup.ShowDialog() == DialogResult.OK)
                    {
                        fnSearch("SelectCNCNoticeList", grdCAMPartSendList);
                    }

                    break;
                case "Authority":
                    CNCAuthority cncAuthority = new CNCAuthority();
                    cncAuthority.StartPosition = FormStartPosition.CenterParent;
                    cncAuthority.Show();
                    break;
            }
        }

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            Control[] controls = pnlToolbar.Controls.Find("New", true);

            SmartButton btn = new SmartButton();

            if (controls.Count() > 0)// && controls[0] is SmartButton btnSave)
            {
                btn = (SmartButton)controls[0];
            }

            DataSet ds = new DataSet();

            DataTable dtFiledata = new DataTable();

            if(dtFile != null)
                dtFiledata = dtFile.Copy();

            switch (tabIdManagement.SelectedTabPageIndex)
            {
                case 0://양산
                    ProductionSave(ds, 0);
                    
                    break;
               
                case 1://샘플
                    #region
                    DataTable dtSamp = (grdSample.DataSource as DataTable).Clone();

                    dtSamp.TableName = "CNCData";

                    dtSamp.Columns.Add("_STATE_");
                    dtSamp.Columns.Add("_PRODUCTTYPE_");

                    DataRow newSampRow = dtSamp.NewRow();

                    //newSampRow["_STATE_"] = !btn.Enabled ? "added" : "modified";
                    newSampRow["_STATE_"] = string.IsNullOrEmpty(txtCNCNoSam.EditValue.ToString()) ? "added" : "modified";
                    newSampRow["_PRODUCTTYPE_"] = "Sample";

                    //newSampRow["CNCNO"] = !btn.Enabled ? Guid.NewGuid() : txtCNCNoSam.EditValue;
                    newSampRow["CNCNO"] = string.IsNullOrEmpty(txtCNCNoSam.EditValue.ToString()) ? Guid.NewGuid() : txtCNCNoSam.EditValue;

                    newSampRow["PLANTID"] = Conditions.GetValues()["P_PLANTID"].ToString();
                    newSampRow["CUSTOMERID"] = sspCustomerNameSam.GetValue();
                    //newSampRow["CNCTYPE"] = cboCNCTypeSam.EditValue;
                    newSampRow["LOCKSTATE"] = cboHoldStateSam.EditValue;
                    newSampRow["PRODUCTDEFID"] = sspProductDefIdSam.GetValue();
                    newSampRow["PRODUCTDEFVERSION"] = txtProductDefVersionSam.EditValue;
                    //newSampRow["XAXIS"] = string.IsNullOrEmpty(txtScaleXSam.EditValue.ToString()) ? 0 : txtScaleXSam.EditValue;
                    //newSampRow["YAXIS"] = string.IsNullOrEmpty(txtScaleYSam.EditValue.ToString()) ? 0 : txtScaleYSam.EditValue;
                    //newSampRow["ST"] = string.IsNullOrEmpty(txtScaleSTSam.EditValue.ToString()) ? 0 : txtScaleSTSam.EditValue;
                    newSampRow["FIRSTCNCSCALE_X"] = string.IsNullOrEmpty(txtFirstCNCScaleX.EditValue.ToString()) ? 0 : txtFirstCNCScaleX.EditValue;
                    newSampRow["FIRSTCNCSCALE_Y"] = string.IsNullOrEmpty(txtFirstCNCScaleY.EditValue.ToString()) ? 0 : txtFirstCNCScaleY.EditValue;
                    newSampRow["FIRSTCNCCOMMENT"] = txtFirstCNCComment.EditValue;
                    newSampRow["SECOND1CNCSCALE_X"]  = string.IsNullOrEmpty(txtSecond1CNCScaleX.EditValue.ToString()) ?0: txtSecond1CNCScaleX.EditValue;
                    newSampRow["SECOND1CNCSCALE_Y"]  = string.IsNullOrEmpty(txtSecond1CNCScaleY.EditValue.ToString()) ?0: txtSecond1CNCScaleY.EditValue;
                    newSampRow["SECOND1CNCSCALE_ST"] = string.IsNullOrEmpty(txtSecond1CNCScaleST.EditValue.ToString()) ? 0 : txtSecond1CNCScaleST.EditValue;
                    newSampRow["SECOND2CNCSCALE_X"] = string.IsNullOrEmpty(txtSecond2CNCScaleX.EditValue.ToString()) ? 0 : txtSecond2CNCScaleX.EditValue;
                    newSampRow["SECOND2CNCSCALE_Y"] = string.IsNullOrEmpty(txtSecond2CNCScaleY.EditValue.ToString()) ? 0 : txtSecond2CNCScaleY.EditValue;
                    newSampRow["SECOND2CNCSCALE_ST"] = string.IsNullOrEmpty(txtSecond2CNCScaleST.EditValue.ToString()) ? 0 : txtSecond2CNCScaleST.EditValue;
                    newSampRow["SECOND3CNCSCALE_X"] = string.IsNullOrEmpty(txtSecond3CNCScaleX.EditValue.ToString())?0: txtSecond3CNCScaleX.EditValue;
                    newSampRow["SECOND3CNCSCALE_Y"] = string.IsNullOrEmpty(txtSecond3CNCScaleY.EditValue.ToString())?0: txtSecond3CNCScaleY.EditValue;
                    newSampRow["SECOND3CNCSCALE_ST"] = string.IsNullOrEmpty(txtSecond3CNCScaleST.EditValue.ToString())?0: txtSecond3CNCScaleST.EditValue;
                    newSampRow["SECONDCNCCOMMENT"] = txtSecondCNCComment.EditValue;
                    newSampRow["LASERPROCSCALE_X"] = string.IsNullOrEmpty(txtLaserProcScaleX.EditValue.ToString())?0: txtLaserProcScaleX.EditValue;
                    newSampRow["LASERPROCSCALE_Y"] = string.IsNullOrEmpty(txtLaserProcScaleY.EditValue.ToString())?0: txtLaserProcScaleY.EditValue;
                    //newSampRow["LASERPROCSCALE_ST"] = txtST
                    newSampRow["LASERPROCCOMMENT"] = txtLastProcComment.EditValue;
                    newSampRow["LASERCNCCOMMENT"] = txtLaserCNCComment.EditValue;
                    newSampRow["ROUTERCOMMENT"] = txtRouterComment.EditValue;

                    newSampRow["FIRSTCNCFILEID"] = txtSelectFirstCNCFilePath.Tag == null ? txtFirstCNCFileName.Tag : txtSelectFirstCNCFilePath.Tag;
                    newSampRow["SECOND1CNCFILEID"] = txtSelectSecond1CNCFilePath.Tag == null ? txtSecond1CNCFileName.Tag : txtSelectSecond1CNCFilePath.Tag;
                    newSampRow["SECOND2CNCFILEID"] = txtSelectSecond2CNCFilePath.Tag == null ? txtSecond2CNCFileName.Tag : txtSelectSecond2CNCFilePath.Tag;
                    newSampRow["SECOND3CNCFILEID"] = txtSelectSecond3CNCFilePath.Tag == null ? txtSecond3CNCFileName.Tag : txtSelectSecond3CNCFilePath.Tag;
                    newSampRow["LASERPROCFILEID"] = txtSelectLaserProcFilePath.Tag == null ? txtLaserProcFileName.Tag : txtSelectLaserProcFilePath.Tag;
                    newSampRow["LASERCNCFILEID"] = txtSelectLaserCNCFilePath.Tag == null ? txtLaserCNCFileName.Tag : txtSelectLaserCNCFilePath.Tag;
                    newSampRow["ROUTERFILEID"] = txtSelectRouterFilePath.Tag == null ? txtRouterFileName.Tag : txtSelectRouterFilePath.Tag;

                    newSampRow["FIRSTCNCUSERID"] = lblFirstCNCUser.Tag;
                    newSampRow["SECOND1CNCUSERID"] = lblSecond1CNCUser.Tag;
                    newSampRow["SECOND2CNCUSERID"] = lblSecond2CNCUser.Tag;
                    newSampRow["SECOND3CNCUSERID"] = lblSecond3CNCUser.Tag;
                    newSampRow["LASERPROCUSERID"] = lblLaserProcUser.Tag;
                    newSampRow["LASERCNCUSERID"] = lblLaserCNCUser.Tag;
                    newSampRow["ROUTERUSERID"] = lblRouterUser.Tag;

                    dtSamp.Rows.Add(newSampRow);

                    if (newSampRow != null)
                    {
                        if (dtSamp.Rows.Count != 0)
                        {
                            ds.Tables.Add(dtSamp);
                            ds.Tables.Add(dtFiledata);
                            ExecuteRule("SaveCNCDataManagement", ds);
                        }
                    }
                    break;
                    #endregion
                case 3://CAM 전달
                    break;
            }

            //File Upload
            if (dtFiledata.Rows.Count != 0)
                FileUploadToServer(dtFiledata);
            //object totalFileSize = dtFiledata.Compute("Sum(FILESIZE)", "FILESIZE > 0");

            //Commons.Controls.FileProgressDialog fileProgressDialog = new Commons.Controls.FileProgressDialog(dtFiledata, Commons.Controls.UpDownType.Upload, "", Convert.ToInt32(totalFileSize.ToString()));
            //fileProgressDialog.ShowDialog(this);

            dtFile.Clear();
        }
        #endregion

        /// <summary>
        /// 검색조건 팝업 예제
        /// </summary>
        private void InitializeCondition_Popup()
        {
            Conditions.AddTextBox("PRODUCTDEFVERSION").SetLabel("PRODUCTDEFVERSION").SetPosition(3.7);
            //팝업 컬럼 설정
            var productDefPopup = Conditions.AddSelectPopup("PRODUCTDEFID", new SqlQuery("GetProductDefList", "10003", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "PRODUCTDEFID", "PRODUCTDEFID")
               .SetPopupLayout("PRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)  //팝업창 UI 설정.
               .SetPopupResultCount(1)  //팝업창 선택가능한 개수
               .SetPosition(3.5)
               .SetDefault(null,null,null)
               .SetLabel("PRODUCTDEFID")
               .SetRelationIds("P_PLANTID")//, "P_CUSTOMER")
               .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
               .SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
               {
                   foreach (DataRow row in selectedRow)
                   {
                       if (selectedRow.Count() > 0)
                       {
                           if (tabIdManagement.SelectedTabPageIndex == 0)
                           {
                               this.Conditions.GetControl<SmartTextBox>("PRODUCTDEFVERSION").EditValue = row["PRODUCTDEFVERSION"].ToString();
                           }
                           else
                           {
                               this.Conditions.GetControl<SmartTextBox>("PRODUCTDEFVERSION").EditValue = row["PRODUCTDEFVERSION"].ToString();
                           }
                       }
                   }
               });
            productDefPopup.DisplayFieldName = "PRODUCTDEFID";
            productDefPopup.ValueFieldName = "PRODUCTDEFID";

            productDefPopup.Conditions.AddTextBox("PRODUCTDEF");
            //productDefPopup.Conditions.AddTextBox("PRODUCTDEFNAME");
            //productDefPopup.Conditions.AddTextBox("PRODUCTDEFVERSION");

            productDefPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150).SetLabel("PRODUCTDEFID");
            productDefPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 150).SetLabel("PRODUCTDEFNAME");
            productDefPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 100).SetLabel("PRODUCTDEFVERSION");

            
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            this.Conditions.GetControl<SmartComboBox>("P_PLANTID").EditValue = UserInfo.Current.Plant;
            this.Conditions.GetControl<SmartComboBox>("P_PLANTID").Enabled = false;

            //Conditions.AddTextBox("PRODUCTDEFVERSION").SetLabel("PRODUCTDEFVERSION").SetPosition(3.7);
        }

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            InitializeTabControl();

            await base.OnSearchAsync();

            // 조회 함수
            //fnSearch();
            
            string queryName = string.Empty;

            dtFile = new DataTable();
            dtFile.TableName = "FileTable";
            dtFile.Columns.Add("FILEID");
            dtFile.Columns.Add("FILENAME");
            dtFile.Columns.Add("FILEEXT");
            dtFile.Columns.Add("FILEPATH");
            dtFile.Columns.Add("SAFEFILENAME");
            dtFile.Columns.Add("FILESIZE", typeof(int));
            dtFile.Columns.Add("SEQUENCE");
            dtFile.Columns.Add("LOCALFILEPATH");
            dtFile.Columns.Add("RESOURCETYPE");
            dtFile.Columns.Add("RESOURCEID");
            dtFile.Columns.Add("RESOURCEVERSION");
            dtFile.Columns.Add("PROCESSINGSTATUS");

            switch (tabIdManagement.SelectedTabPageIndex)
            {
                case 0://CNC Data 양산
                    queryName = "SelectCNCDataProductionList";
                    fnSearch(queryName, grdProduction);
                    grdProduction.View.ClearSelection();

                    grdProduction.View.FocusedRowHandle = 0;
                    RowChange(grdProduction.View, 0);
                    break;
                case 1://CNC Data 샘플
                    queryName = "SelectCNCDataSampleList";
                    fnSearch(queryName, grdSample);
                    grdSample.View.ClearSelection();

                    grdSample.View.FocusedRowHandle = 0;
                    RowChange(grdSample.View, 0);
                    break;
                case 2://Transaction
                    queryName = "SelectCNCDataHistory";
                    fnSearch(queryName, grdTransaction);
                    grdTransaction.View.ClearSelection();

                    grdTransaction.View.FocusedRowHandle = 0;
                    break;
                case 3://CAM PART 전달 내역
                    queryName = "SelectCNCNoticeList";
                    fnSearch(queryName, grdCAMPartSendList);
                    grdCAMPartSendList.View.ClearSelection();

                    grdCAMPartSendList.View.FocusedRowHandle = 0;
                    break;
            }
        }

        private void fnSearch(string query, SmartBandedGrid smartBandedGrid)
        {
            // 조회조건에 입력된 값들을 Dictionary<string, object> Type으로 가져옴
            var values = Conditions.GetValues();
            values.Add("P_LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable dtCNCData = SqlExecuter.Query(query, "10001", values);

            if (dtCNCData.Rows.Count < 1)
            {
                // 조회할 데이터가 없습니다.
                ShowMessage("NoSelectData");
            }
            smartBandedGrid.DataSource = dtCNCData;
        }

        #region 이벤트
        private void InitializeEvent()
        {
            //탭 변경
            tabIdManagement.SelectedPageChanged += TabIdManagement_TabIndexChanged;
            //신규 생성
            //btnNew
            //투입
            //btnInput

            //양산
            //grdProduction.View.FocusedRowChanged += View_FocusedRowChanged;
            grdProduction.View.RowClick += View_RowClick;
            //양산 파일 선택
            btnSelectFile.Click += BtnFileSelect_Click;
            //양산 파일 다운
            txtFileName.Click += TxtFileName_Click;
            //txtFileName.HyperlinkClick += TxtFileName_HyperlinkClick;


            //샘플
            //grdSample.View.FocusedRowChanged += View_FocusedRowChanged;
            grdSample.View.RowClick += View_RowClick;
            //1차 CNC 파일 선택
            btnSelectFirstCNCFile.Click += BtnFileSelect_Click;
            //2차 CNC-1 파일 선택
            btnSelectSecond1CNCFile.Click += BtnFileSelect_Click;
            //2차 CNC-2 파일 선택
            btnSelectSecond2CNCFile.Click += BtnFileSelect_Click;
            //2차 CNC-3 파일 선택
            btnSelectSecond3CNCFile.Click += BtnFileSelect_Click;
            //LASER 가공 파일 선택
            btnSelectLaserProcFile.Click += BtnFileSelect_Click;
            //LASER-CNC 파일 선택
            btnSelectLaserCNCFile.Click += BtnFileSelect_Click;
            //ROUTER 파일 선택
            btnSelectRouterFile.Click += BtnFileSelect_Click;

            //1차 CNC 파일 다운
            txtFirstCNCFileName.Click += TxtFileName_Click;
            //2차 CNC-1 파일 다운
            txtSecond1CNCFileName.Click += TxtFileName_Click;
            //2차 CNC-2 파일 다운
            txtSecond2CNCFileName.Click += TxtFileName_Click;
            //2차 CNC-3 파일 다운
            txtSecond3CNCFileName.Click += TxtFileName_Click;
            //LASER 가공 파일 다운
            txtLaserProcFileName.Click += TxtFileName_Click;
            //LASER-CNC 파일 다운
            txtLaserCNCFileName.Click += TxtFileName_Click;
            //ROUTER 파일 다운
            txtRouterFileName.Click += TxtFileName_Click;

            //CAM PART 전달 내역
            grdCAMPartSendList.View.DoubleClick += View_DoubleClick;
            //CAM PART 전달 내역 등록
            //btnRegist
        }

        #region 그리드이벤트
        //Grid Row 선택 
        private void View_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            RowChange((sender as Micube.Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView), e.RowHandle);
        }

        private void RowChange(Micube.Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView grdCNCData, int RowHandle)
        {
            bool isEnabled = false;

            DataRowView rowView = (DataRowView)grdCNCData.GetRow(RowHandle);

            if (rowView != null)
            {
                DataRow row = rowView.Row;


                if (tabIdManagement.SelectedTabPageIndex == 0)
                {
                    BindingProductionTab(row);

                    //가공 업체가 글을 클릭
                    if (ProductUser == ProductionAuthority.Processing || ProductUser == ProductionAuthority.Manager)
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.Add("CNCNO");
                        dt.Columns.Add("CONNECTTYPE");

                        DataRow newRow = dt.NewRow();

                        newRow["CNCNO"] = row["CNCNO"];
                        newRow["CONNECTTYPE"] = "Input";

                        dt.Rows.Add(newRow);

                        ExecuteRule("SaveCNCDataHistory", dt);

                        isEnabled = true;
                    }

                    //Dictionary<string, object> param = new Dictionary<string, object>();
                    //param.Add("P_PLANTID", UserInfo.Current.Plant);
                    //param.Add("P_USERID", UserInfo.Current.Id);
                    //
                    //DataTable dtUserClass = SqlExecuter.Query("SelectPlantUserClass", "10001", param);
                    //
                    //BindingProductionTab(row);

                    //if (dtUserClass != null)
                    //{
                    //    foreach (DataRow userRow in dtUserClass.Rows)
                    //    {
                    //        if (userRow["USERCLASSID"].ToString().Equals("OutsourcingOwner"))
                    //        {
                    //            //if (btnInput.Enabled == false)
                    //            //{
                    //            //    btnInput.Enabled = true;
                    //            //    isEnabled = true;
                    //            //}
                    //        }
                    //
                    //        //가공 업체가 글을 클릭
                    //        if (userRow["USERCLASSID"].ToString().Equals("ProgressOwner"))
                    //        {
                    //            DataTable dt = new DataTable();
                    //            dt.Columns.Add("CNCNO");
                    //            dt.Columns.Add("CONNECTTYPE");
                    //
                    //            DataRow newRow = dt.NewRow();
                    //
                    //            newRow["CNCNO"] = row["CNCNO"];
                    //            newRow["CONNECTTYPE"] = "READ";
                    //
                    //            dt.Rows.Add(newRow);
                    //
                    //            ExecuteRule("SaveCNCDataHistory", dt);
                    //
                    //            isEnabled = true;
                    //        }
                    //    }
                    //}
                }
                else
                    BindingSampleTab(row);

                //사용자 그룹 지정 필요
                if (row["CREATORID"].Equals(UserInfo.Current.Id))
                    isEnabled = true;

                EnabledControl(isEnabled);
            }
        }

        //CAM PART 전달 내역 Click
        private void View_DoubleClick(object sender, EventArgs e)
        {
            DataRow row = (sender as Micube.Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView).GetFocusedDataRow();

            CNCDataPopup cncDataPopup = new CNCDataPopup(row);
            cncDataPopup.ShowDialog();

            base.OnSearchAsync();
        }
        #endregion

        #region 기타이벤트
        //File Download
        private void TxtFileName_Click(object sender, EventArgs e)
        {
            string filePath = (sender as SmartLabel).Text;
            string fileId = string.Empty;

            string cncNo = string.Empty;

            if (!string.IsNullOrEmpty(filePath))
            {
                fileId = (sender as SmartLabel).Tag.ToString();

                if (tabIdManagement.SelectedTabPageIndex == 0)
                {
                    cncNo = txtCNCNoPro.EditValue.ToString();

                    if (cboProcessType.ItemIndex == 1)
                    {
                        txtOutsourcingOwner.EditValue = UserInfo.Current.Name;
                        txtOutsourcingOwner.Tag = UserInfo.Current.Id;

                        cboProcessType.ItemIndex = 2;//가공
                        FileDownload(filePath, fileId, cncNo);
                        ShowMessage("CompleteProcess");
                        OnSearchAsync();
                    }
                }
                else if (tabIdManagement.SelectedTabPageIndex == 1)
                {
                    dicButtonLabel[(sender as SmartLabel)].Text = UserInfo.Current.Name;
                    dicButtonLabel[(sender as SmartLabel)].Tag = UserInfo.Current.Id;

                    cncNo = txtCNCNoSam.EditValue.ToString();
                    FileDownload(filePath, fileId, cncNo);
                }

            
            }


        }

        //탭 양산,샘플 변경
        private void TabIdManagement_TabIndexChanged(object sender, EventArgs e)
        {
            //사용자 권한 조회
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("PLANTID", UserInfo.Current.Plant);
            param.Add("USERIDNAME", UserInfo.Current.Id);

            DataTable dtUserAuthority = SqlExecuter.Query("GetCNCUserAuthorityList", "10001", param);

            if (dtUserAuthority != null && dtUserAuthority.Rows.Count != 0)
            {
                if(!dtUserAuthority.Rows[0]["PRODUCTIONAUTHORITY"].ToString().Equals(""))
                    ProductUser = dtUserAuthority.Rows[0]["PRODUCTIONAUTHORITY"].ToString().ToEnum<ProductionAuthority>();
                if(!dtUserAuthority.Rows[0]["SAMPLEAUTHORITY"].ToString().Equals(""))
                    SampleUser = dtUserAuthority.Rows[0]["SAMPLEAUTHORITY"].ToString().ToEnum<SampleAuthority>();
                if (!dtUserAuthority.Rows[0]["PROCESSTYPE"].ToString().Equals(""))
                    TypeUser = dtUserAuthority.Rows[0]["PROCESSTYPE"].ToString().ToEnum<ProcessType>();
                
            }

            #region Button
            Control[] controls = pnlToolbar.Controls.Find("Save", true);

            SmartButton btnSave = new SmartButton();

            if (controls.Count() > 0)
            {
                btnSave = (SmartButton)controls[0];
            }

            controls = pnlToolbar.Controls.Find("New", true);

            SmartButton btnNew = new SmartButton();

            if (controls.Count() > 0)
            {
                btnNew = (SmartButton)controls[0];
            }

            controls = pnlToolbar.Controls.Find("Input", true);

            SmartButton btnInput = new SmartButton();

            if (controls.Count() > 0)
            {
                btnInput = (SmartButton)controls[0];
            }

            controls = pnlToolbar.Controls.Find("Regist", true);

            SmartButton btnRegist = new SmartButton();

            if (controls.Count() > 0)
            {
                btnRegist = (SmartButton)controls[0];
                btnRegist.Visible = false;
            }

            controls = pnlToolbar.Controls.Find("Authority", true);

            SmartButton btnAuthority = new SmartButton();

            if (controls.Count() > 0)
            {
                btnAuthority = (SmartButton)controls[0];
                btnAuthority.Visible = false;
            }
            #endregion

            switch (tabIdManagement.SelectedTabPageIndex)
            {
                case 0:

                    btnAuthority.Visible = false;
                    btnNew.Visible = false;
                    btnInput.Visible = false;
                    btnRegist.Visible = false;
                    btnSave.Visible = false;
                    if (ProductUser == ProductionAuthority.Manager)
                    {

                        btnAuthority.Visible = true;
                        btnNew.Visible = true;
                        btnInput.Visible = true;
                        btnSave.Visible = true;
                    }
                    else if (ProductUser == ProductionAuthority.ProgManage)
                        btnInput.Visible = true;
                    else if (ProductUser == ProductionAuthority.SpecManage)
                    {
                        btnNew.Visible = true;
                        btnSave.Visible = true;
                    }

                    if (dtUserAuthority.Rows.Count > 0)
                    {
                        if (dtUserAuthority.Rows[0]["PRODUCTIONAUTHORITY"].ToString().Equals(""))
                        {
                            btnAuthority.Visible = false;
                            btnNew.Visible = false;
                            btnInput.Visible = false;
                            btnRegist.Visible = false;
                            btnSave.Visible = false;
                        }
                    }
                    break;
                case 1:
                    btnAuthority.Visible = false;
                    btnNew.Visible = false;
                    btnInput.Visible = false;
                    btnRegist.Visible = false;
                    btnSave.Visible = false;

                    if (SampleUser == SampleAuthority.Manager)
                    {
                        btnAuthority.Visible = true;
                        btnNew.Visible = true;
                        btnSave.Visible = true;
                    }
                    else if (SampleUser == SampleAuthority.SpecManage)
                    {
                        btnNew.Visible = true;
                        btnSave.Visible = true;
                    }
                    if (dtUserAuthority.Rows.Count > 0)
                    {
                        if (dtUserAuthority.Rows[0]["SAMPLEAUTHORITY"].ToString().Equals(""))
                        {
                            btnAuthority.Visible = false;
                            btnNew.Visible = false;
                            btnInput.Visible = false;
                            btnRegist.Visible = false;
                            btnSave.Visible = false;
                        }
                    }
                    break;
                case 3:
                    btnInput.Visible = false;
                    btnNew.Visible = false;
                    btnRegist.Visible = true;
                    btnAuthority.Visible = false;
                    btnSave.Visible = false;
                    break;
                default:
                    btnInput.Visible = false;
                    btnNew.Visible = false;
                    btnRegist.Visible = false;
                    btnAuthority.Visible = false;
                    btnSave.Visible = false;
                    break;
                   
            }
            if (dtUserAuthority.Rows.Count == 0)
            {
                btnAuthority.Visible = false;
                btnNew.Visible = false;
                btnInput.Visible = false;
                btnRegist.Visible = false;
                btnSave.Visible = false;
            }
            InitializeTabControl();
            OnSearchAsync();
        }

        //File 선택
        private void BtnFileSelect_Click(object sender, EventArgs e)
        {
            FileUpload((sender as SmartButton));
        }
        #endregion

        #region File Upload/Download
        private void FileDownload(string filePath, string FileId = null, string cncNo = null)
        {
            string ftpServerPath = Cryptography.AESDecrypt(AppConfiguration.GetString("Application.Ftp.Url"));
            string ftpServerUserId = Cryptography.AESDecrypt(AppConfiguration.GetString("Application.Ftp.Id"));
            string ftpServerPassword = Cryptography.AESDecrypt(AppConfiguration.GetString("Application.Ftp.Password"));

            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.SelectedPath = Application.StartupPath;

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    //string serverPath = AppConfiguration.GetString("Application.SmartDeploy.Url") + "CNCData";// filePath;//+ Format.GetString(files.Rows[0]["FILEPATH"]);
                    //serverPath = serverPath + ((serverPath.EndsWith("/")) ? "" : "/");
                    //DeployCommonFunction.DownLoadFile(serverPath, folderBrowserDialog.SelectedPath, filePath);// Format.GetString(files.Rows[0]["SAFEFILENAME"]));

                    string serverFilePath = "CNCData";

                    using (WebClient client = new WebClient())
                    {
                        string serverPath = ftpServerPath + serverFilePath;
                        serverPath = serverPath + ((serverPath.EndsWith("/")) ? "" : "/") + filePath;

                        client.Credentials = new NetworkCredential(ftpServerUserId, ftpServerPassword);

                        client.DownloadFile(serverPath, string.Join("\\", folderBrowserDialog.SelectedPath, filePath));
                    }

                    ShowMessage("파일 다운로드가 완료되었습니다.");

                    DataTable dt = new DataTable();
                    dt.Columns.Add("CNCNO");
                    dt.Columns.Add("_PRODUCTTYPE_");
                    dt.Columns.Add("FIRSTCNCUSERID");
                    dt.Columns.Add("SECOND1CNCUSERID");
                    dt.Columns.Add("SECOND2CNCUSERID");
                    dt.Columns.Add("SECOND3CNCUSERID");
                    dt.Columns.Add("LASERPROCUSERID");
                    dt.Columns.Add("LASERCNCUSERID");
                    dt.Columns.Add("ROUTERUSERID");

                    DataRow newRow = dt.NewRow();

                    newRow["CNCNO"] = cncNo;

                    if (tabIdManagement.SelectedTabPageIndex == 0)
                        newRow["_PRODUCTTYPE_"] = "Production";
                    if (tabIdManagement.SelectedTabPageIndex == 1)
                    {
                        newRow["_PRODUCTTYPE_"] = "Sample";

                        newRow["FIRSTCNCUSERID"] = lblFirstCNCUser.Tag;
                        newRow["SECOND1CNCUSERID"] = lblSecond1CNCUser.Tag;
                        newRow["SECOND2CNCUSERID"] = lblSecond2CNCUser.Tag;
                        newRow["SECOND3CNCUSERID"] = lblSecond3CNCUser.Tag;
                        newRow["LASERPROCUSERID"] = lblLaserProcUser.Tag;
                        newRow["LASERCNCUSERID"] = lblLaserCNCUser.Tag;
                        newRow["ROUTERUSERID"] = lblRouterUser.Tag;
                    }
                    dt.Rows.Add(newRow);

                    ExecuteRule("SaveCNCDataDownloadCount", dt);
                }
                catch (Exception ex)
                {
                    throw MessageException.Create(ex.Message);
                }
            }
        }

        private void FileUpload(SmartButton smartButton)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.Multiselect = true;
            openFileDialog.Filter = "All Files (*.*)|*.*";
            openFileDialog.FilterIndex = 0;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (dtFile == null)
                {
                    dtFile = new DataTable();
                    dtFile.TableName = "FileTable";
                    dtFile.Columns.Add("FILEID");
                    dtFile.Columns.Add("FILENAME");
                    dtFile.Columns.Add("FILEEXT");
                    dtFile.Columns.Add("FILEPATH");
                    dtFile.Columns.Add("SAFEFILENAME");
                    dtFile.Columns.Add("FILESIZE", typeof(int));
                    dtFile.Columns.Add("SEQUENCE");
                    dtFile.Columns.Add("LOCALFILEPATH");
                    dtFile.Columns.Add("RESOURCETYPE");
                    dtFile.Columns.Add("RESOURCEID");
                    dtFile.Columns.Add("RESOURCEVERSION");
                    dtFile.Columns.Add("PROCESSINGSTATUS");
                }

                string[] fullFileName = openFileDialog.FileNames;
                string[] safeFileName = openFileDialog.SafeFileNames;

                string uploadPath = "CNCData";

                FileType selectFileType = smartButton.Tag.ToString().ToUpper().ToEnum<FileType>();
                string resurceId = smartButton.Tag.ToString().ToUpper() + "-" + DateTime.Now.ToString("yyyyMMddHHmmssfff");

                foreach (string fileName in fullFileName)
                {
                    FileInfo fileInfo = new FileInfo(fileName);

                    DataRow[] rows = dtFile.Select("SAFEFILENAME = '" + fileInfo.Name + "'");
                    string addedFileName = "";

                    if (rows.Count() > 0)
                        addedFileName = "(1)";

                    DataRow newRow = dtFile.NewRow();

                    newRow["FILEID"] = "FILE-" + Guid.NewGuid().ToString("N");
                    newRow["FILENAME"] = fileInfo.Name.Substring(0, fileInfo.Name.Length - fileInfo.Extension.Length) + addedFileName;
                    newRow["FILEEXT"] = fileInfo.Extension.Replace(".", "");
                    newRow["FILEPATH"] = uploadPath;
                    newRow["SAFEFILENAME"] = fileInfo.Name.Substring(0, fileInfo.Name.Length - fileInfo.Extension.Length) + addedFileName + fileInfo.Extension;
                    newRow["FILESIZE"] = fileInfo.Length;
                    newRow["SEQUENCE"] = smartButton.TabIndex;
                    newRow["PROCESSINGSTATUS"] = "";
                    newRow["LOCALFILEPATH"] = fileInfo.FullName;
                    newRow["RESOURCETYPE"] = "CNCData";//Resource.Type;
                    newRow["RESOURCEID"] = resurceId;//Resource.Id;
                    newRow["RESOURCEVERSION"] = 1;//Resource.Version;

                    dtFile.Rows.Add(newRow);

                    //동일 파일 ROW 찾기
                    DataRow[] containRow = dtFile.Select("SEQUENCE = '" + smartButton.TabIndex + "'");

                    if (containRow.Count() > 1)
                    {
                        //이전 파일 삭제
                        dtFile.Rows.Remove(containRow[0]); 
                    }

                    switch (selectFileType)
                    {
                        case FileType.FIRSTCNC:
                            txtSelectFirstCNCFilePath.EditValue = fileInfo.Name;
                            txtSelectFirstCNCFilePath.Tag = newRow["FILEID"].ToString();//resurceId;
                            break;
                        case FileType.SECOND1CNC:
                            txtSelectSecond1CNCFilePath.EditValue = fileInfo.Name;
                            txtSelectSecond1CNCFilePath.Tag = newRow["FILEID"].ToString();//resurceId;
                            break;
                        case FileType.SECOND2CNC:
                            txtSelectSecond2CNCFilePath.EditValue = fileInfo.Name;
                            txtSelectSecond2CNCFilePath.Tag = newRow["FILEID"].ToString();//resurceId;
                            break;
                        case FileType.SECOND3CNC:
                            txtSelectSecond3CNCFilePath.EditValue = fileInfo.Name;
                            txtSelectSecond3CNCFilePath.Tag = newRow["FILEID"].ToString();//resurceId;
                            break;
                        case FileType.LASERPROC:
                            txtSelectLaserProcFilePath.EditValue = fileInfo.Name;
                            txtSelectLaserProcFilePath.Tag = newRow["FILEID"].ToString();//resurceId;
                            break;
                        case FileType.LASERCNC:
                            txtSelectLaserCNCFilePath.EditValue = fileInfo.Name;
                            txtSelectSecond1CNCFilePath.Tag = newRow["FILEID"].ToString();//resurceId;
                            break;
                        case FileType.ROUTER:
                            txtSelectRouterFilePath.EditValue = fileInfo.Name;
                            txtSelectRouterFilePath.Tag = newRow["FILEID"].ToString();//resurceId;
                            break;
                        default:
                            txtSelectFilePath.EditValue = fileInfo.Name;
                            txtSelectFilePath.Tag = newRow["FILEID"].ToString();//resurceId;
                            break;
                    }
                    //Commons.Controls.FileProgressDialog fileProgressDialog = new Commons.Controls.FileProgressDialog(dtFiledata, Commons.Controls.UpDownType.Upload, "", (int)totalFileSize);
                }
            }
        }

        //Server Upload
        private void FileUploadToServer(DataTable dtFile)
        {
            try
            {
                string ftpServerPath = Cryptography.AESDecrypt(AppConfiguration.GetString("Application.Ftp.Url"));
                string ftpServerUserId = Cryptography.AESDecrypt(AppConfiguration.GetString("Application.Ftp.Id"));
                string ftpServerPassword = Cryptography.AESDecrypt(AppConfiguration.GetString("Application.Ftp.Password"));

                int completeFileSize = 0;
                int completeFileCount = 0;

                DataTable dataSource = dtFile;

                foreach (DataRow row in dataSource.Rows)
                {
                    string fileName = Format.GetString(row["LOCALFILEPATH"]);
                    string safeFileName = Format.GetString(row["FILENAME"]) + "." + Format.GetString(row["FILEEXT"]);
                    string uploadPath = Format.GetString(row["FILEPATH"]);

                    if (!File.Exists(fileName))
                        throw new FileNotFoundException();

                    CreateFtpServerDirectory(ftpServerPath, uploadPath, ftpServerUserId, ftpServerPassword);

                    FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(ftpServerPath + string.Join("/", uploadPath, safeFileName));
                    ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;
                    ftpRequest.UseBinary = true;
                    ftpRequest.UsePassive = true;
                    ftpRequest.Credentials = new NetworkCredential(ftpServerUserId, ftpServerPassword);

                    byte[] byteFile;

                    //using (FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                    //{
                    //    byteFile = new byte[fileStream.Length];
                    //    fileStream.Read(byteFile, 0, byteFile.Length);
                    //}

                    using (FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                    {
                        byteFile = new byte[fileStream.Length];
                        fileStream.Read(byteFile, 0, byteFile.Length);
                    }

                    ftpRequest.ContentLength = byteFile.Length;
                    using (Stream stream = ftpRequest.GetRequestStream())
                    {
                        stream.Write(byteFile, 0, byteFile.Length);
                    }

                    //Commons.Controls.SmartDeployService.WebService webService = new Commons.Controls.SmartDeployService.WebService();
                    //webService.UploadFileAsync(byteFile, uploadPath, safeFileName);

                    //webService.UploadFileCompleted += (sender, args) =>
                    //{
                    //    if (args.Result == "SUCCESS")
                    //        row["PROCESSINGSTATUS"] = "Complete";
                    //    else
                    //        row["PROCESSINGSTATUS"] = "Error";

                    //    completeFileSize += Format.GetInteger(row["FILESIZE"]);
                    //    completeFileCount++;

                    //    if (completeFileCount == dataSource.Rows.Count)
                    //    {
                    //        //ShowMessage("파일 업로드가 완료되었습니다.");
                    //        //
                    //        //DialogResult = DialogResult.OK;
                    //    }
                    //};
                }
            }
            catch (Exception ex)
            {
                throw MessageException.Create(ex.Message);
            }
        }

        private void CreateFtpServerDirectory(string url, string path, string id, string pwd)
        {
            FtpWebRequest ftpRequest = null;
            Stream ftpStream = null;

            string[] subDirs = path.Split('/');

            string currentDir = url;

            foreach (string subDir in subDirs)
            {
                try
                {
                    currentDir = string.Join("/", currentDir.TrimEnd('/'), subDir);
                    ftpRequest = (FtpWebRequest)FtpWebRequest.Create(currentDir);
                    ftpRequest.Method = WebRequestMethods.Ftp.MakeDirectory;
                    ftpRequest.Credentials = new NetworkCredential(id, pwd);
                    ftpRequest.UseBinary = true;
                    ftpRequest.UsePassive = true;
                    ftpRequest.Proxy = null;

                    FtpWebResponse ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
                    ftpStream = ftpResponse.GetResponseStream();
                    ftpStream.Close();
                    ftpResponse.Close();
                }
                catch (WebException ex)
                {
                    FtpWebResponse ftpResponse = (FtpWebResponse)ex.Response;
                    ftpResponse.Close();
                }
            }
        }

        //Server Upload
        private void ProductionSave(DataSet ds, int check)
        {
            DataTable dtFiledata = new DataTable();

            if (dtFile != null)
                dtFiledata = dtFile.Copy();
            DataTable dtProd = (grdProduction.DataSource as DataTable).Clone();

            dtProd.TableName = "CNCData";

            dtProd.Columns.Add("_STATE_");
            dtProd.Columns.Add("_PRODUCTTYPE_");
            dtProd.Columns.Add("CHECK");
            DataRow newProdRow = dtProd.NewRow();

            //string state = cboProcessType.EditValue.ToString();

            //newProdRow["_STATE_"] = !btn.Enabled ? "added" : "modified";
            newProdRow["_STATE_"] = string.IsNullOrEmpty(txtCNCNoPro.EditValue.ToString()) ? "added" : "modified";
            newProdRow["_PRODUCTTYPE_"] = "Production";
            newProdRow["CHECK"] = check;
            //newProdRow["CNCNO"] = !btn.Enabled ? Guid.NewGuid() : txtCNCNoPro.EditValue;
            newProdRow["CNCNO"] = string.IsNullOrEmpty(txtCNCNoPro.EditValue.ToString()) ? Guid.NewGuid() : txtCNCNoPro.EditValue;

            newProdRow["PLANTID"] = Conditions.GetValues()["P_PLANTID"].ToString();
            newProdRow["CUSTOMERID"] = sspCustomerNamePro.GetValue();
            newProdRow["PRODUCTDEFID"] = sspProductDefIdPro.GetValue();
            newProdRow["PRODUCTDEFVERSION"] = txtProductDefVersionPro.EditValue;
            newProdRow["CNCTYPE"] = cboCNCTypePro.EditValue;
            newProdRow["CNCWORKTYPE"] = cboCNCWorkType.EditValue;
            newProdRow["LOCKSTATE"] = cboHoldStatePro.EditValue;
            newProdRow["XAXIS"] = string.IsNullOrEmpty(txtScaleXPro.EditValue.ToString()) ? 0 : txtScaleXPro.EditValue;
            newProdRow["YAXIS"] = string.IsNullOrEmpty(txtScaleYPro.EditValue.ToString()) ? 0 : txtScaleYPro.EditValue;
            newProdRow["ST"] = string.IsNullOrEmpty(txtScaleSTPro.EditValue.ToString()) ? 0 : txtScaleSTPro.EditValue;
            newProdRow["PROCESSINGSTATUS"] = cboProcessType.EditValue;
            newProdRow["PROGRESSOWNER"] = txtProgressOwner.Tag;
            newProdRow["OUTSOURCINGOWNER"] = txtOutsourcingOwner.Tag;
            newProdRow["SPECIFICATIONCOMMENT"] = txtSpecComment.EditValue;
            newProdRow["PROGRESSCOMMENT"] = txtProcComment.EditValue;
            newProdRow["OUTSOURCINGCOMMENT"] = txtOutsComment.EditValue;
            newProdRow["FILEID"] = txtSelectFilePath.Tag == null ? txtFileName.Tag : txtSelectFilePath.Tag;
            //newProdRow["DOWNLOADCOUNT"] = 0;

            dtProd.Rows.Add(newProdRow);

            if (newProdRow != null)
            {
                if (dtProd.Rows.Count != 0)
                {
                    dtProd.TableName = "CNCData";

                    ds.Tables.Add(dtProd);
                    ds.Tables.Add(dtFiledata);

                    ExecuteRule("SaveCNCDataManagement", ds);
                }
            }
        }
        #endregion

        #endregion
    }
}
