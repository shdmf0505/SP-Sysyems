using DevExpress.XtraEditors;

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.Net.Data;
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

namespace SmartMES
{
    public partial class ChangePassword : XtraForm
    {
        private string capEmptyCurrentPassword = "Enter Current Password";
        private string msgEmptyCurrentPassword = "Please enter current password.";

        private string capNewPasswordValidate = "Unacceptable Password";
        private string msgNewPasswordValidate = "Use at least 8 characters, upper or lower case letters, numbers, and special characters.";

        private string capPasswordNotMatching = "Password do not match";
        private string msgPasswordNotMatching = "Password do not match.";

        private string capChangePasswordConfirm = "Change Password Confirm";
        private string msgChangePasswordConfirm = "Do you want to change your password?";
        
        private string capChangePasswordCancel = "Change Password Cancel";
        private string msgChangePasswordCancel = "Do you want to cancel changing your password?";

        public ChangePassword()
        {
            InitializeComponent();

            InitializeEvent();
        }

        private void InitializeEvent()
        {
            Shown += ChangePassword_Shown;

            btnConfirm.Click += BtnConfirm_Click;
            btnCancel.Click += BtnCancel_Click;

            FormClosing += ChangePassword_FormClosing;
        }

        private void InitializeLanguage()
        {
            lblCurrentPassword.Text = Language.Get("CURRENTPASSWORD");
            lblNewPassword.Text = Language.Get("NEWPASSWORD");
            lblNewPasswordConfirm.Text = Language.Get("NEWPASSWORDCONFIRM");

            btnConfirm.Text = Language.Get("CONFIRM");
            btnCancel.Text = Language.Get("CANCEL");
        }

        private void InitializeMessage()
        {
            Language.LanguageMessageItem itemEmptyCurrentPassword = Language.GetMessage("EMPTYCURRENTPASSWORD");
            capEmptyCurrentPassword = itemEmptyCurrentPassword.Title ?? itemEmptyCurrentPassword.Message;
            msgEmptyCurrentPassword = itemEmptyCurrentPassword.Message;

            Language.LanguageMessageItem itemNewPasswordValidate = Language.GetMessage("PASSWORDVALIDATE");
            capNewPasswordValidate = itemNewPasswordValidate.Title ?? itemNewPasswordValidate.Message;
            msgNewPasswordValidate = itemNewPasswordValidate.Message;

            Language.LanguageMessageItem itemPasswordNotMatching = Language.GetMessage("PASSWORDNOTMATCHING");
            capPasswordNotMatching = itemPasswordNotMatching.Title ?? itemPasswordNotMatching.Message;
            msgPasswordNotMatching = itemPasswordNotMatching.Message;

            Language.LanguageMessageItem itemChangePasswordConfirm = Language.GetMessage("CHANGEPASSWORDCONFIRM");
            capChangePasswordConfirm = itemChangePasswordConfirm.Title ?? itemChangePasswordConfirm.Message;
            msgChangePasswordConfirm = itemChangePasswordConfirm.Message;

            Language.LanguageMessageItem itemChangePasswordCancel = Language.GetMessage("CHANGEPASSWORDCANCEL");
            capChangePasswordCancel = itemChangePasswordCancel.Title ?? itemChangePasswordCancel.Message;
            msgChangePasswordCancel = itemChangePasswordCancel.Message;
        }

        private void ChangePassword_Shown(object sender, EventArgs e)
        {
            InitializeLanguage();
            InitializeMessage();
        }

        private bool ValidateNewPassword(string newPassword)
        {
            bool isValidateOK = true;

            if (newPassword.Length < 8 ||
                !Regex.Match(newPassword, @"\d", RegexOptions.ECMAScript).Success ||
                (!Regex.Match(newPassword, @"[a-z]", RegexOptions.ECMAScript).Success &&
                 !Regex.Match(newPassword, @"[A-Z]", RegexOptions.ECMAScript).Success) ||
                !Regex.Match(newPassword, @"[!,@,#,$,%,^,&,*,?,_,~,-,\,(,)]", RegexOptions.ECMAScript).Success)
                isValidateOK = false;

            return isValidateOK;
        }

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCurrentPassword.Text))
            {
                //MessageBox.Show(msgEmptyCurrentPassword, capEmptyCurrentPassword, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MSGBox.Show(MessageBoxType.Warning, "EMPTYCURRENTPASSWORD");
                return;
            }

            if (!ValidateNewPassword(txtNewPassword.Text))
            {
                //MessageBox.Show(msgNewPasswordValidate, capNewPasswordValidate, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MSGBox.Show(MessageBoxType.Warning, "PASSWORDVALIDATE");
                return;
            }

            if (txtNewPassword.Text != txtNewPasswordConfirm.Text)
            {
                //MessageBox.Show(msgPasswordNotMatching, capPasswordNotMatching, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MSGBox.Show(MessageBoxType.Warning, "PASSWORDNOTMATCHING");
                return;
            }

            //DialogResult dialogResult = MessageBox.Show(msgChangePasswordConfirm, capChangePasswordConfirm, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            

            if (MSGBox.Show(MessageBoxType.Question, "CHANGEPASSWORDCONFIRM", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DialogResult = DialogResult.OK;

                SaveChangePassword();
                Close();
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void ChangePassword_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.Cancel)
            {
                //DialogResult dialogResult = MessageBox.Show(msgChangePasswordCancel, capChangePasswordCancel, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (MSGBox.Show(MessageBoxType.Question, "CHANGEPASSWORDCANCEL", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        private void SaveChangePassword()
        {
            DataTable dtResult = new DataTable();
            dtResult.Columns.Add("USERID", typeof(string));
            dtResult.Columns.Add("CURRENTPASSWORD", typeof(string));
            dtResult.Columns.Add("NEWPASSWORD", typeof(string));

            DataRow row = dtResult.NewRow();
            row["USERID"] = UserInfo.Current.Id;
            row["CURRENTPASSWORD"] = Cryptography.SHA256Hash(txtCurrentPassword.Text);
            row["NEWPASSWORD"] = txtNewPassword.Text;

            dtResult.Rows.Add(row);

            dtResult.AcceptChanges();

            MessageWorker saveWorker = new MessageWorker("ChangePassword");
            saveWorker.SetBody(new MessageBody()
            {
                { "list", dtResult }
            });

            saveWorker.Execute();
        }
    }
}
