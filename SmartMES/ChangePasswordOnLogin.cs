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
    public partial class ChangePasswordOnLogin : XtraForm
    {
        public string LanguageType;

        public DataTable dtDictionary;
        public DataTable dtMessage;

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

        private string _userId = "";

        public ChangePasswordOnLogin(string userId)
        {
            _userId = userId;

            InitializeComponent();

            InitializeEvent();
        }

        private void InitializeEvent()
        {
            Shown += ChangePassword_Shown;

            btnConfirm.Click += BtnConfirm_Click;
            //btnCancel.Click += BtnCancel_Click;

            FormClosing += ChangePassword_FormClosing;
        }

        private void InitializeLanguage()
        {
            Dictionary<string, object> dicLanguage = new Dictionary<string, object>();

            DataRow[] dataRows = dtDictionary.Select("LANGUAGETYPE = '" + LanguageType + "'");

            foreach (DataRow row in dataRows)
            {
                dicLanguage.Add(row["DICTIONARYID"].ToString(), row["DICTIONARYNAME"]);
            }

            lblCurrentPassword.Text = Format.GetString(dicLanguage["CURRENTPASSWORD"]);
            lblNewPassword.Text = Format.GetString(dicLanguage["NEWPASSWORD"]);
            lblNewPasswordConfirm.Text = Format.GetString(dicLanguage["NEWPASSWORDCONFIRM"]);

            btnConfirm.Text = Format.GetString(dicLanguage["CONFIRM"]);
            //btnCancel.Text = Language.Get("CANCEL");
        }

        private void InitializeMessage()
        {
            DataRow[] rowsEmptyCurrentPassword = dtMessage.Select("MESSAGEID = 'EMPTYCURRENTPASSWORD' AND LANGUAGETYPE = '" + LanguageType + "'");

            if (rowsEmptyCurrentPassword.Length > 0)
            {
                capEmptyCurrentPassword = rowsEmptyCurrentPassword[0]["CAPTION"].ToString();
                msgEmptyCurrentPassword = rowsEmptyCurrentPassword[0]["MESSAGE"].ToString();
            }


            DataRow[] rowsNewPasswordValidate = dtMessage.Select("MESSAGEID = 'PASSWORDVALIDATE' AND LANGUAGETYPE = '" + LanguageType + "'");

            if (rowsNewPasswordValidate.Length > 0)
            {
                capNewPasswordValidate = rowsNewPasswordValidate[0]["CAPTION"].ToString();
                msgNewPasswordValidate = rowsNewPasswordValidate[0]["MESSAGE"].ToString();
            }


            DataRow[] rowsPasswordNotMatching = dtMessage.Select("MESSAGEID = 'PASSWORDNOTMATCHING' AND LANGUAGETYPE = '" + LanguageType + "'");

            if (rowsPasswordNotMatching.Length > 0)
            {
                capPasswordNotMatching = rowsPasswordNotMatching[0]["CAPTION"].ToString();
                msgPasswordNotMatching = rowsPasswordNotMatching[0]["MESSAGE"].ToString();
            }


            DataRow[] rowsChangePasswordConfirm = dtMessage.Select("MESSAGEID = 'CHANGEPASSWORDCONFIRM' AND LANGUAGETYPE = '" + LanguageType + "'");

            if (rowsChangePasswordConfirm.Length > 0)
            {
                capChangePasswordConfirm = rowsChangePasswordConfirm[0]["CAPTION"].ToString();
                msgChangePasswordConfirm = rowsChangePasswordConfirm[0]["MESSAGE"].ToString();
            }


            DataRow[] rowsChangePasswordCancel = dtMessage.Select("MESSAGEID = 'CHANGEPASSWORDCANCEL' AND LANGUAGETYPE = '" + LanguageType + "'");

            if (rowsChangePasswordCancel.Length > 0)
            {
                capChangePasswordCancel = rowsChangePasswordCancel[0]["CAPTION"].ToString();
                msgChangePasswordCancel = rowsChangePasswordCancel[0]["MESSAGE"].ToString();
            }
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
                MSGBox.Show(MessageBoxType.Warning, capEmptyCurrentPassword, msgEmptyCurrentPassword);
                return;
            }

            if (!ValidateNewPassword(txtNewPassword.Text))
            {
                //MessageBox.Show(msgNewPasswordValidate, capNewPasswordValidate, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MSGBox.Show(MessageBoxType.Warning, capNewPasswordValidate, msgNewPasswordValidate);
                return;
            }

            if (txtNewPassword.Text != txtNewPasswordConfirm.Text)
            {
                //MessageBox.Show(msgPasswordNotMatching, capPasswordNotMatching, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MSGBox.Show(MessageBoxType.Warning, capPasswordNotMatching, msgPasswordNotMatching);
                return;
            }

            //DialogResult dialogResult = MessageBox.Show(msgChangePasswordConfirm, capChangePasswordConfirm, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            

            if (MSGBox.Show(MessageBoxType.Question, capChangePasswordConfirm, msgChangePasswordConfirm, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DialogResult = DialogResult.No;

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

                //if (MSGBox.Show(MessageBoxType.Question, capChangePasswordCancel, msgChangePasswordCancel, MessageBoxButtons.YesNo) == DialogResult.No)
                //{
                    e.Cancel = true;
                //}
            }
            if (DialogResult == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void SaveChangePassword()
        {
            DataTable dtResult = new DataTable();
            dtResult.Columns.Add("USERID", typeof(string));
            dtResult.Columns.Add("CURRENTPASSWORD", typeof(string));
            dtResult.Columns.Add("NEWPASSWORD", typeof(string));
            dtResult.Columns.Add("USERSTATE", typeof(string));

            DataRow row = dtResult.NewRow();
            row["USERID"] = _userId;
            row["CURRENTPASSWORD"] = Cryptography.SHA256Hash(txtCurrentPassword.Text);
            row["NEWPASSWORD"] = Cryptography.SHA256Hash(txtNewPassword.Text);
            row["USERSTATE"] = "Normal";

            dtResult.Rows.Add(row);

            dtResult.AcceptChanges();

            MessageWorker saveWorker = new MessageWorker("ChangePasswordOnLogin");
            saveWorker.SetBody(new MessageBody()
            {
                { "list", dtResult }
            });

            saveWorker.Execute();

            DialogResult = DialogResult.OK;
        }
    }
}
