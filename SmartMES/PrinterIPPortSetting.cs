using DevExpress.XtraEditors;

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.Net.Data;
using Micube.Framework.SmartControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartMES
{
    public partial class PrinterIPPortSetting : XtraForm
    {

        public PrinterIPPortSetting()
        {
            InitializeComponent();

            InitializeEvent();
        }

        private void InitializeEvent()
        {
            Shown += PrinterIPPortSetting_Shown;

            btnConfirm.Click += BtnConfirm_Click;
            btnCancel.Click += BtnCancel_Click;

            txtIPAddress.KeyPress += TextBox_KeyPress;
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (ValidateIPAddress(this.txtIPAddress.Text))
                {
                    this.txtPort.SelectAll();
                    this.txtPort.Focus();
                }
                else
                {
                    MSGBox.Show(MessageBoxType.Warning, "IPADDRESSVALIDATE");
                    this.txtIPAddress.SelectAll();
                }
            }
        }

        private void InitializeLanguage()
        {
            lblIPAddress.Text = Language.Get("IPADDRESS");
            lblPort.Text = Language.Get("PORT");

            btnConfirm.Text = Language.Get("CONFIRM");
            btnCancel.Text = Language.Get("CANCEL");
        }

        private void InitializeMessage()
        {
            Language.LanguageMessageItem itemEmptyCurrentPassword = Language.GetMessage("EMPTYCURRENTPASSWORD");
            //capEmptyCurrentPassword = itemEmptyCurrentPassword.Title ?? itemEmptyCurrentPassword.Message;
            //msgEmptyCurrentPassword = itemEmptyCurrentPassword.Message;
        }

        private void PrinterIPPortSetting_Shown(object sender, EventArgs e)
        {
            InitializeLanguage();
            InitializeMessage();

             List<string> strIpPort = UserInfo.Current.Printer.Split(':').ToList();
            if (strIpPort != null && strIpPort.Count > 0)
            {
                if (ValidateIPAddress(strIpPort[0]))
                    this.txtIPAddress.Text = strIpPort[0];
                else
                    this.txtIPAddress.Text = string.Empty;
            }

            if (strIpPort != null && strIpPort.Count > 1)
                this.txtPort.Text = strIpPort[1];

            this.txtIPAddress.SelectAll();
            this.txtIPAddress.Focus();
        }

        private bool ValidateIPAddress(string strCheckIp)
        {
            bool isValidateOK = true;

            if (string.IsNullOrEmpty(strCheckIp))
                isValidateOK = false;

            string strPattern = @"^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$";

            isValidateOK = Regex.Match(strCheckIp, strPattern, RegexOptions.ECMAScript).Success;

            return isValidateOK;
        }

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtIPAddress.Text))
            {
                MSGBox.Show(MessageBoxType.Warning, "IPADDRESSVALIDATE");

                this.txtIPAddress.SelectAll();
                this.txtIPAddress.Focus();
                return;
            }

            //기본 프린터 설정을 변경하시겠습니까?
            if (MSGBox.Show(MessageBoxType.Question, "CHANGEPRINTERCONFIRM", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DialogResult = DialogResult.OK;

                SavePrinterIpPort();
                Close();
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void SavePrinterIpPort()
        {
            if (string.IsNullOrEmpty(this.txtPort.Text))
                UserInfo.Current.Printer = this.txtIPAddress.Text.Trim();
            else
                UserInfo.Current.Printer = this.txtIPAddress.Text.Trim() + ":" + this.txtPort.Text.Trim();

            UserInfo.Current.PrinterType = "IP";

            //config 파일 설정
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["DefaultPrinter"].Value = UserInfo.Current.Printer;
            config.AppSettings.Settings["PrinterType"].Value = UserInfo.Current.PrinterType;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
