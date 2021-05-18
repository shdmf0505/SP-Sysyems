#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Validations;
using static Micube.SmartMES.Commons.SPCLibrary.DataSets.SpcDataSet;

using DevExpress.XtraEditors.Repository;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.BandedGrid;
using Micube.SmartMES.Commons.SPCLibrary;

#endregion

namespace Micube.SmartMES.SPC
{
    /// <summary>
    /// 프 로 그 램 명  : SPC> SPC 현황 관리도 화면> Rule Check 버튼 Click
    /// 업  무  설  명  : SPC 관리도에서 Rule Check.
    /// 생    성    자  : 이승우
    /// 생    성    일  : 2019-12-01
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class SpcControlRulesSettingPopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region Local Variables
        /// <summary>
        /// 팝업진입전 그리드의 Row
        /// </summary>
        public DataRow CurrentDataRow { get; set; }
        /// <summary>
        /// Rules Check 자료 저장.
        /// </summary>
        public RulesCheck RulesCheckData;

        /// <summary>
        /// Spec Option.
        /// </summary>
        public SPCOption spcOption;
        #endregion

        #region 생성자

        /// <summary>
        /// 생성자
        /// </summary>
        public SpcControlRulesSettingPopup()
        {
            InitializeComponent();
            InitializeEvent();
            InitializeGrid();

            //this.CancelButton = btnClose;
        }

        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// Chart Raw Data 
        /// </summary>
        private void InitializeGrid()
        {
        }
        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {

        }

        /// <summary>
        /// 폼 Load 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpcControlRulesSettingPopup_Load(object sender, EventArgs e)
        {
            SpcClass.SpcDictionaryDataSetting();
            this.MessageSetting();
        }

        /// <summary>
        /// Rule 설명 및 해설 표시.
        /// </summary>
        private void MessageSetting()
        {
            //SpcClass.SpcDictionaryDataSetting();
            bool isCheck = SpcDictionary.CaptionDtCheck(SpcDicClassId.CONTROLLABEL);
            string rule01Description = "";
            string rule02Description = "";
            string rule03Description = "";
            string rule04Description = "";
            string rule05Description = "";
            string rule06Description = "";
            string rule07Description = "";
            string rule08Description = "";

            string rule01Comment = "";
            string rule02Comment = "";
            string rule03Comment = "";
            string rule04Comment = "";
            string rule05Comment = "";
            string rule06Comment = "";
            string rule07Comment = "";
            string rule08Comment = "";

            if (isCheck)
            {
                rule01Description = SpcDictionary.readControl(SpcDicClassId.CONTROLLABEL, "SPCRULESMSG01DISCRIPTION");
                rule02Description = SpcDictionary.readControl(SpcDicClassId.CONTROLLABEL, "SPCRULESMSG02DISCRIPTION");
                rule03Description = SpcDictionary.readControl(SpcDicClassId.CONTROLLABEL, "SPCRULESMSG03DISCRIPTION");
                rule04Description = SpcDictionary.readControl(SpcDicClassId.CONTROLLABEL, "SPCRULESMSG04DISCRIPTION");
                rule05Description = SpcDictionary.readControl(SpcDicClassId.CONTROLLABEL, "SPCRULESMSG05DISCRIPTION");
                rule06Description = SpcDictionary.readControl(SpcDicClassId.CONTROLLABEL, "SPCRULESMSG06DISCRIPTION");
                rule07Description = SpcDictionary.readControl(SpcDicClassId.CONTROLLABEL, "SPCRULESMSG07DISCRIPTION");
                rule08Description = SpcDictionary.readControl(SpcDicClassId.CONTROLLABEL, "SPCRULESMSG08DISCRIPTION");
                rule01Comment = SpcDictionary.readControl(SpcDicClassId.CONTROLLABEL, "SPCRULESMSG1COMMENT");
                rule02Comment = SpcDictionary.readControl(SpcDicClassId.CONTROLLABEL, "SPCRULESMSG2COMMENT");
                rule03Comment = SpcDictionary.readControl(SpcDicClassId.CONTROLLABEL, "SPCRULESMSG3COMMENT");
                rule04Comment = SpcDictionary.readControl(SpcDicClassId.CONTROLLABEL, "SPCRULESMSG4COMMENT");
                rule05Comment = SpcDictionary.readControl(SpcDicClassId.CONTROLLABEL, "SPCRULESMSG5COMMENT");
                rule06Comment = SpcDictionary.readControl(SpcDicClassId.CONTROLLABEL, "SPCRULESMSG6COMMENT");
                rule07Comment = SpcDictionary.readControl(SpcDicClassId.CONTROLLABEL, "SPCRULESMSG7COMMENT");
                rule08Comment = SpcDictionary.readControl(SpcDicClassId.CONTROLLABEL, "SPCRULESMSG8COMMENT");

                lblRule01.Text = rule01Description;
                lblRule02.Text = rule02Description;
                lblRule03.Text = rule03Description;
                lblRule04.Text = rule04Description;
                lblRule05.Text = rule05Description;
                lblRule06.Text = rule06Description;
                lblRule07.Text = rule07Description;
                lblRule08.Text = rule08Description;
                lblRule01.Tag = rule01Comment;
                lblRule02.Tag = rule02Comment;
                lblRule03.Tag = rule03Comment;
                lblRule04.Tag = rule04Comment;
                lblRule05.Tag = rule05Comment;
                lblRule06.Tag = rule06Comment;
                lblRule07.Tag = rule07Comment;
                lblRule08.Tag = rule08Comment;
                lblRule01.ToolTip = rule01Comment;
                lblRule02.ToolTip = rule02Comment;
                lblRule03.ToolTip = rule03Comment;
                lblRule04.ToolTip = rule04Comment;
                lblRule05.ToolTip = rule05Comment;
                lblRule06.ToolTip = rule06Comment;
                lblRule07.ToolTip = rule07Comment;
                lblRule08.ToolTip = rule08Comment;

            }
        }
        /// <summary>
        /// 실행 버튼 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAction_Click(object sender, EventArgs e)
        {
            bool result = false;
            result = ActionData();
            if (result)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        private bool ActionData()
        {
            bool result = false;

            this.txtTestView.Text = this.chkRule01.Checked.ToString();

            RulesCheckData = RulesCheck.Create();
            RulesCheckData.lstRulesPara.Clear();

            RulesPara ruleCheck01 = RulesPara.Create();
            RulesPara ruleCheck02 = RulesPara.Create();
            RulesPara ruleCheck03 = RulesPara.Create();
            RulesPara ruleCheck04 = RulesPara.Create();
            RulesPara ruleCheck05 = RulesPara.Create();
            RulesPara ruleCheck06 = RulesPara.Create();
            RulesPara ruleCheck07 = RulesPara.Create();
            RulesPara ruleCheck08 = RulesPara.Create();

            if (this.chkRule01.Checked)
            {
                ruleCheck01.ruleNo = 1;
                ruleCheck01.status = "";
                ruleCheck01.messageDiscription = this.lblRule01.Text;
                ruleCheck01.messageComment = this.lblRule01.Tag.ToSafeString();
                RulesCheckData.lstRulesPara.Add(ruleCheck01);
            }

            if (this.chkRule02.Checked)
            {
                ruleCheck02.ruleNo = 2;
                ruleCheck02.status = "";
                ruleCheck02.messageDiscription = this.lblRule02.Text;
                ruleCheck02.messageComment = this.lblRule02.Tag.ToSafeString();
                RulesCheckData.lstRulesPara.Add(ruleCheck02);
            }

            if (this.chkRule03.Checked)
            {
                ruleCheck03.ruleNo = 3;
                ruleCheck03.status = "";
                ruleCheck03.messageDiscription = this.lblRule03.Text;
                ruleCheck03.messageComment = this.lblRule03.Tag.ToSafeString();
                RulesCheckData.lstRulesPara.Add(ruleCheck03);
            }

            if (this.chkRule04.Checked)
            {
                ruleCheck04.ruleNo = 4;
                ruleCheck04.status = "";
                ruleCheck04.messageDiscription = this.lblRule04.Text;
                ruleCheck04.messageComment = this.lblRule04.Tag.ToSafeString();
                RulesCheckData.lstRulesPara.Add(ruleCheck04);
            }

            if (this.chkRule05.Checked)
            {
                ruleCheck05.ruleNo = 5;
                ruleCheck05.status = "";
                ruleCheck05.messageDiscription = this.lblRule05.Text;
                ruleCheck05.messageComment = this.lblRule05.Tag.ToSafeString();
                RulesCheckData.lstRulesPara.Add(ruleCheck05);
            }

            if (this.chkRule06.Checked)
            {
                ruleCheck06.ruleNo = 6;
                ruleCheck06.status = "";
                ruleCheck06.messageDiscription = this.lblRule06.Text;
                ruleCheck06.messageComment = this.lblRule06.Tag.ToSafeString();
                RulesCheckData.lstRulesPara.Add(ruleCheck06);
            }

            if (this.chkRule07.Checked)
            {
                ruleCheck07.ruleNo = 7;
                ruleCheck07.status = "";
                ruleCheck07.messageDiscription = this.lblRule07.Text;
                ruleCheck07.messageComment = this.lblRule07.Tag.ToSafeString();
                RulesCheckData.lstRulesPara.Add(ruleCheck07);
            }

            if (this.chkRule08.Checked)
            {
                ruleCheck08.ruleNo = 8;
                ruleCheck08.status = "";
                ruleCheck08.messageDiscription = this.lblRule08.Text;
                ruleCheck08.messageComment = this.lblRule08.Tag.ToSafeString();
                RulesCheckData.lstRulesPara.Add(ruleCheck08);
            }

            foreach (RulesPara item in RulesCheckData.lstRulesPara)
            {
                Console.WriteLine(item.ruleNo);
            }


            if (RulesCheckData.lstRulesPara.Count > 0)
            {
                result = true;
            }
            else
            {
                //선택된 Rule 번호가 없습니다.
                ShowMessage("선택된 Rule 번호가 없습니다.");
            }

            return result;

        }
        /// <summary>
        /// 저장
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 닫기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// CheckBox 전체 선택 및 해지.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            RulesCheckBoxChange(this.chkAll.Checked);
        }
        #endregion

        #region Private Function
        public void RulesModeCheck()
        {
            if (spcOption == null)
            {
                return;
            }

            switch (spcOption.chartType)
            {
                case ControlChartType.XBar_R:
                case ControlChartType.XBar_S:
                case ControlChartType.I_MR:
                case ControlChartType.Merger:
                    this.Text = SpcLibMessage.common.comCpk1047;//Rules Setting - Nelson rules
                    WesternRulesChnage(true);
                    break;
                case ControlChartType.np:
                case ControlChartType.p:
                case ControlChartType.c:
                case ControlChartType.u:
                default:
                    this.Text = SpcLibMessage.common.comCpk1048;//Rules Setting - Western Electric rules
                    WesternRulesChnage(false);
                    break;
            }
        }

        private void WesternRulesChnage(bool isValue)
        {
            this.lblNo05.Visible = isValue;
            this.txtRule05.Visible = isValue;
            this.chkRule05.Visible = isValue;
            this.lblRule05.Visible = isValue;
            this.lblNo06.Visible = isValue;
            this.txtRule06.Visible = isValue;
            this.chkRule06.Visible = isValue;
            this.lblRule06.Visible = isValue;
            this.lblNo07.Visible = isValue;
            this.txtRule07.Visible = isValue;
            this.chkRule07.Visible = isValue;
            this.lblRule07.Visible = isValue;
            this.lblNo08.Visible = isValue;
            this.txtRule08.Visible = isValue;
            this.chkRule08.Visible = isValue;
            this.lblRule08.Visible = isValue;



        }
        /// <summary>
        /// CheckBox 일괄 상태 변경.
        /// </summary>
        /// <param name="value"></param>
        private void RulesCheckBoxChange(bool value)
        {
            switch (spcOption.chartType)
            {
                case ControlChartType.XBar_R:
                case ControlChartType.XBar_S:
                case ControlChartType.I_MR:
                case ControlChartType.Merger:
                    this.chkRule01.Checked = value;
                    this.chkRule02.Checked = value;
                    this.chkRule03.Checked = value;
                    this.chkRule04.Checked = value;
                    this.chkRule05.Checked = value;
                    this.chkRule06.Checked = value;
                    this.chkRule07.Checked = value;
                    this.chkRule08.Checked = value;
                    break;
                case ControlChartType.np:
                case ControlChartType.p:
                case ControlChartType.c:
                case ControlChartType.u:
                default:
                    this.chkRule01.Checked = value;
                    this.chkRule02.Checked = value;
                    this.chkRule03.Checked = value;
                    this.chkRule04.Checked = value;
                    break;
            }

        }
        #endregion



    }
}
