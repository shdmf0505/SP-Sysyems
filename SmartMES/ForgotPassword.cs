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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartMES
{
    public partial class ForgotPassword : XtraForm
    {
        public string LanguageType;

        public DataTable dtDictionary;
        public DataTable dtMessage;

        private string capEmptyUserId = "Empty ID";
        private string msgEmptyUserId = "Please enter your ID.";

        private string capPasswordForgotConfirm = "Password Reset Confirm";
        private string msgPasswordForgotConfirm = "Do you want to request your password reset?";
        
        private string capPasswordForgotCancel = "Password Reset Cancel";
        private string msgPasswordForgotCancel = "Do you want to cancel request your password reset?";

        private string capUnregisteredUserId = "Unregistered ID";
        private string msgUnregisteredUserId = "Unregistered User ID.";

        public ForgotPassword()
        {
            InitializeComponent();

            InitializeEvent();
        }

        private void InitializeEvent()
        {
            Shown += ForgotPassword_Shown;

            btnConfirm.Click += BtnConfirm_Click;
            btnCancel.Click += BtnCancel_Click;

            FormClosing += ForgotPassword_FormClosing;
        }

        private void InitializeLanguage()
        {
            Dictionary<string, object> dicLanguage = new Dictionary<string, object>();

            DataRow[] dataRows = dtDictionary.Select("LANGUAGETYPE = '" + LanguageType + "'");

            foreach (DataRow row in dataRows)
            {
                dicLanguage.Add(row["DICTIONARYID"].ToString(), row["DICTIONARYNAME"]);
            }

            lblUserId.Text = Format.GetString(dicLanguage["USERID"]);

            btnConfirm.Text = Format.GetString(dicLanguage["CONFIRM"]);
            btnCancel.Text = Format.GetString(dicLanguage["CANCEL"]);
        }

        private void InitializeMessage()
        {
            DataRow[] rowsPasswordForgotConfirm = dtMessage.Select("MESSAGEID = 'PASSWORDFORGOTCONFIRM' AND LANGUAGETYPE = '" + LanguageType + "'");

            if (rowsPasswordForgotConfirm.Length > 0)
            {
                capPasswordForgotConfirm = rowsPasswordForgotConfirm[0]["CAPTION"].ToString();
                msgPasswordForgotConfirm = rowsPasswordForgotConfirm[0]["MESSAGE"].ToString();
            }

            Text = capPasswordForgotConfirm;


            DataRow[] rowsPasswordForgotCancel = dtMessage.Select("MESSAGEID = 'PASSWORDFORGOTCANCEL' AND LANGUAGETYPE = '" + LanguageType + "'");

            if (rowsPasswordForgotCancel.Length > 0)
            {
                capPasswordForgotCancel = rowsPasswordForgotCancel[0]["CAPTION"].ToString();
                msgPasswordForgotCancel = rowsPasswordForgotCancel[0]["MESSAGE"].ToString();
            }


            DataRow[] rowsEmptyUserId = dtMessage.Select("MESSAGEID = 'EMPTYUSERID' AND LANGUAGETYPE = '" + LanguageType + "'");

            if (rowsEmptyUserId.Length > 0)
            {
                capEmptyUserId = rowsEmptyUserId[0]["CAPTION"].ToString();
                msgEmptyUserId = rowsEmptyUserId[0]["MESSAGE"].ToString();
            }


            DataRow[] rowsUnregisteredUserId = dtMessage.Select("MESSAGEID = 'UNREGISTEREDUSERID' AND LANGUAGETYPE = '" + LanguageType + "'");

            if (rowsUnregisteredUserId.Length > 0)
            {
                capUnregisteredUserId = rowsUnregisteredUserId[0]["CAPTION"].ToString();
                msgUnregisteredUserId = rowsUnregisteredUserId[0]["MESSAGE"].ToString();
            }
        }

        private void ForgotPassword_Shown(object sender, EventArgs e)
        {
            InitializeLanguage();
            InitializeMessage();
        }

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserId.Text))
            {
                //MessageBox.Show(msgEmptyUserId, capEmptyUserId, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MSGBox.Show(MessageBoxType.Warning, capEmptyUserId, msgEmptyUserId);
                return;
            }

            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add("USERID", txtUserId.Text);

            DataTable dtResult = SqlExecuter.Query("CheckDuplicateIdOnLogin", "00001", dicParam);

            if (dtResult.Rows.Count < 1)
            {
                //MessageBox.Show(msgUnregisteredUserId, capUnregisteredUserId, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MSGBox.Show(MessageBoxType.Warning, capUnregisteredUserId, msgUnregisteredUserId);

                txtUserId.Text = "";
                return;
            }

            //DialogResult dialogResult = MessageBox.Show(msgPasswordForgotConfirm, capPasswordForgotConfirm, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (MSGBox.Show(MessageBoxType.Question, capPasswordForgotConfirm, msgPasswordForgotConfirm, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DialogResult = DialogResult.OK;

                SaveUserData(dtResult);

                Close();
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void ForgotPassword_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.Cancel)
            {
                //DialogResult dialogResult = MessageBox.Show(msgPasswordForgotCancel, capPasswordForgotCancel, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (MSGBox.Show(MessageBoxType.Question, capPasswordForgotCancel, msgPasswordForgotCancel, MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        private void SaveUserData(DataTable result)
        {
            // 2020-03-05 -- 유태근 비밀번호 초기화 신청시 해당 유저의 이메일이 등록되어 있다면 메일발송
            string emailAddress = Format.GetString(result.Rows[0]["EMAILADDRESS"]);

            DataTable dtResult = new DataTable();
            dtResult.Columns.Add("USERID", typeof(string));
            dtResult.Columns.Add("VALIDSTATE", typeof(string));
            dtResult.Columns.Add("USERSTATE", typeof(string));
            dtResult.Columns.Add("EMAILADDRESS", typeof(string));

            dtResult.Rows.Add(txtUserId.Text, "Invalid", "Forgot", emailAddress);

            dtResult.AcceptChanges();

            MessageWorker saveWorker = new MessageWorker("ForgotUser");
            saveWorker.SetBody(new MessageBody()
            {
                { "list", dtResult }
            });

            saveWorker.Execute();
        }

        /// <summary>
        /// 사용자에게 비밀번호 초기화 이메일을 보낸다.
        /// </summary>
        /// <param name="emailAdress"></param>
        private void SendResetPasswordMail(string emailAdress)
        {

        }
    }
}
