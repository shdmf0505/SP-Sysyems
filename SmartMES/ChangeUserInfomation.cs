using DevExpress.XtraEditors;

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartMES
{
    public partial class ChangeUserInformation : XtraForm
    {
        private string _plantId = "";

        private string capChangeUserInfoCancel = "Change User Information Cancel";
        private string msgChangeUserInfoCancel = "Do you want to cancel changing user information?";

        private string capChangeUserInfoConfirm = "Change User Information Confrim";
        private string msgChangeUserInfoConfirm = "Do you want to confrim changing user information?";

        private string capEmptyEmailAddress = "Empty Email Address";
        private string msgEmptyEmailAddress = "Please enter your email address.";

        private string capEmailFormatInvalid = "Email Format Invalid";
        private string msgEmailFormatInvalid = "Email format is invalid.";

        public ChangeUserInformation()
        {
            InitializeComponent();

            InitializeEvent();

            GetUserInfo();
        }

        private void GetUserInfo()
        {
            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add("USERID", UserInfo.Current.Id);

            DataTable dtUserInfo = SqlExecuter.Query("GetLoginUserInfo", "00001", dicParam);

            if (dtUserInfo.Rows.Count > 0)
            {
                DataRow rowUserInfo = dtUserInfo.Rows.Cast<DataRow>().FirstOrDefault();

                txtUserId.Text = Format.GetString(rowUserInfo["USERID"]);
                txtUserName.Text = Format.GetString(rowUserInfo["USERNAME"]);
                txtNickName.Text = Format.GetString(rowUserInfo["NICKNAME"]);
                txtDescription.Text = Format.GetString(rowUserInfo["DESCRIPTION"]);
                txtDepartment.Text = Format.GetString(rowUserInfo["DEPARTMENT"]);
                txtPosition.Text = Format.GetString(rowUserInfo["POSITION"]);
                txtDuty.Text = Format.GetString(rowUserInfo["DUTY"]);
                txtEmailAddress.Text = Format.GetString(rowUserInfo["EMAILADDRESS"]);
                txtCellphoneNumber.Text = Format.GetString(rowUserInfo["CELLPHONENUMBER"]);
                cboDefaultLanguageType.EditValue = rowUserInfo["DEFAULTLANGUAGETYPE"];
                txtHomeAddress.Text = Format.GetString(rowUserInfo["HOMEADDRESS"]);

                _plantId = Format.GetString(rowUserInfo["PLANTID"]);
            }
        }

        private void InitializeEvent()
        {
            Shown += UserRequest_Shown;

            btnConfirm.Click += BtnConfirm_Click;
            btnCancel.Click += BtnCancel_Click;

            FormClosing += ChangeUserInformation_FormClosing;
        }

        private void InitializeLanguage()
        {
            Text = Language.Get("CHANGEUSERINFO");

            lblUserId.Text = Language.Get("USERID");
            lblUserName.Text = Language.Get("USERNAME");
            lblNickName.Text = Language.Get("NICKNAME");
            lblDescription.Text = Language.Get("DESCRIPTION");
            lblDepartment.Text = Language.Get("DEPARTMENT");
            lblPosition.Text = Language.Get("POSITION");
            lblDuty.Text = Language.Get("DUTY");
            lblEmailAddress.Text = Language.Get("EMAILADDRESS");
            lblCellphoneNumber.Text = Language.Get("CELLPHONENUMBER");
            lblDefaultLanguageType.Text = Language.Get("DEFAULTLANGUAGETYPE");
            lblHomeAddress.Text = Language.Get("HOMEADDRESS");

            btnConfirm.Text = Language.Get("CONFIRM");
            btnCancel.Text = Language.Get("CANCEL");
        }

        private void InitializeMessage()
        {
            Language.LanguageMessageItem itemChangeUserInfoCancel = Language.GetMessage("CHANGEUSERINFOCANCEL");
            capChangeUserInfoCancel = itemChangeUserInfoCancel.Title ?? itemChangeUserInfoCancel.Message;
            msgChangeUserInfoCancel = itemChangeUserInfoCancel.Message;

            Language.LanguageMessageItem itemChangeUserInfoConfirm = Language.GetMessage("CHANGEUSERINFOCONFIRM");
            capChangeUserInfoConfirm = itemChangeUserInfoConfirm.Title ?? itemChangeUserInfoConfirm.Message;
            msgChangeUserInfoConfirm = itemChangeUserInfoConfirm.Message;

            Language.LanguageMessageItem itemEmptyEmailAddress = Language.GetMessage("EMPTYEMAILADDRESS");
            capEmptyEmailAddress = itemEmptyEmailAddress.Title ?? itemEmptyEmailAddress.Message;
            msgEmptyEmailAddress = itemEmptyEmailAddress.Message;

            Language.LanguageMessageItem itemEmailFormatInvalid = Language.GetMessage("EMAILFORMATINVALID");
            capEmailFormatInvalid = itemEmailFormatInvalid.Title ?? itemEmailFormatInvalid.Message;
            msgEmailFormatInvalid = itemEmailFormatInvalid.Message;
        }

        private void InitializeLanguageType()
        {
            cboDefaultLanguageType.ValueMember = "CODEID";
            cboDefaultLanguageType.DisplayMember = "CODENAME";
            cboDefaultLanguageType.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboDefaultLanguageType.ShowHeader = false;

            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            dicParam.Add("CODECLASSID", "LanguageType");

            cboDefaultLanguageType.DataSource = SqlExecuter.Query("GetCodeList", "00001", dicParam);
        }

        private void UserRequest_Shown(object sender, EventArgs e)
        {
            InitializeLanguage();
            InitializeMessage();
            InitializeLanguageType();
        }

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmailAddress.Text))
            {
                //MessageBox.Show(msgEmptyEmailAddress, capEmptyEmailAddress, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MSGBox.Show(MessageBoxType.Warning, "EMPTYEMAILADDRESS");
                return;
            }

            //if (!new EmailAddressAttribute().IsValid(txtEmailAddress.Text))
            //{
            //    //MessageBox.Show(msgEmailFormatInvalid, capEmailFormatInvalid, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    MSGBox.Show(MessageBoxType.Warning, "EMAILFORMATINVALID");
            //    return;
            //}

            //DialogResult dialogResult = MessageBox.Show(msgChangeUserInfoConfirm, capChangeUserInfoConfirm, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (MSGBox.Show(MessageBoxType.Question, "CHANGEUSERINFOCONFIRM", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                SaveUserData();

                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void ChangeUserInformation_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.Cancel)
            {
                //DialogResult dialogResult = MessageBox.Show(msgChangeUserInfoCancel, capChangeUserInfoCancel, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (MSGBox.Show(MessageBoxType.Question, "CHANGEUSERINFOCANCEL", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        private void SaveUserData()
        {
            DataTable dtResult = new DataTable();
            dtResult.Columns.Add("USERID", typeof(string));
            dtResult.Columns.Add("USERNAME", typeof(string));
            dtResult.Columns.Add("PLANTID", typeof(string));
            dtResult.Columns.Add("DESCRIPTION", typeof(string));
            dtResult.Columns.Add("NICKNAME", typeof(string));
            dtResult.Columns.Add("DEPARTMENT", typeof(string));
            dtResult.Columns.Add("POSITION", typeof(string));
            dtResult.Columns.Add("DUTY", typeof(string));
            dtResult.Columns.Add("EMAILADDRESS", typeof(string));
            dtResult.Columns.Add("HOMEADDRESS", typeof(string));
            dtResult.Columns.Add("CELLPHONENUMBER", typeof(string));
            dtResult.Columns.Add("DEFAULTLANGUAGETYPE", typeof(string));
            dtResult.Columns.Add("VALIDSTATE", typeof(string));
            dtResult.Columns.Add("_STATE_", typeof(string));

            DataRow row = dtResult.NewRow();
            row["USERID"] = txtUserId.Text;
            row["USERNAME"] = txtUserName.Text;
            row["PLANTID"] = _plantId;
            row["DESCRIPTION"] = txtDescription.Text;
            row["NICKNAME"] = txtNickName.Text;
            row["DEPARTMENT"] = txtDepartment.Text;
            row["POSITION"] = txtPosition.Text;
            row["DUTY"] = txtDuty.Text;
            row["EMAILADDRESS"] = txtEmailAddress.Text;
            row["HOMEADDRESS"] = txtHomeAddress.Text;
            row["CELLPHONENUMBER"] = txtCellphoneNumber.Text;
            row["DEFAULTLANGUAGETYPE"] = cboDefaultLanguageType.EditValue;
            row["VALIDSTATE"] = "Valid";
            row["_STATE_"] = "modified";

            dtResult.Rows.Add(row);

            dtResult.AcceptChanges();

            MessageWorker saveWorker = new MessageWorker("SaveUser");
            saveWorker.SetBody(new MessageBody()
            {
                { "list", dtResult }
            });

            saveWorker.Execute();

            UserInfo.Current.Name = Format.GetString(row["USERNAME"]);
            UserInfo.Current.EmailAddress = Format.GetString(row["EMAILADDRESS"]);
            UserInfo.Current.CellPhoneNumber = Format.GetString(row["CELLPHONENUMBER"]);
        }
    }
}
