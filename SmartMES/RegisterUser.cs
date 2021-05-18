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
    public partial class RegisterUser : XtraForm
    {
        public string LanguageType;
        public string PlantId;
        public string EnterpriseId;

        public DataTable dtDictionary;
        public DataTable dtMessage;

        private bool? DuplicateCheckResult;

        private string msgErrorText = "Can not be used";

        private string capCannotUseId = "Cannot use id";
        private string msgCannotUseId = "This user id cannot use. User id should be combination numeric or alphabet.";

        private string capPasswordNotMatching = "Password do not match";
        private string msgPasswordNotMatching = "Password do not match.";

        private string msgPasswordValidate = "Use at least 8 characters, upper or lower case letters, numbers, and special characters.";

        private string capUseRequestCancel = "Use Request Cancel";
        private string msgUseRequestCancel = "Do you want to cancel the use request?";

        private string capUseRequestConfirm = "Use Request Confrim";
        private string msgUseRequestConfirm = "Do you want to confrim the use request?";

        private string capEmptyUserId = "Empty ID";
        private string msgEmptyUserId = "Please enter your ID.";

        private string capEmptyPassword = "Empty Password";
        private string msgEmptyPassword = "Please enter your password.";

        private string capEmptyEmailAddress = "Empty Email Address";
        private string msgEmptyEmailAddress = "Please enter your email address.";

        private string capEmailFormatInvalid = "Email Format Invalid";
        private string msgEmailFormatInvalid = "Email format is invalid.";

        private string capDoNotDuplicateCheck = "Duplicate Check";
        private string msgDoNotDuplicateCheck = "Please check ID duplication.";

        private string capHaveDuplicateId = "Duplicate Check";
        private string msgHaveDuplicateId = "Your ID is duplicate.";

        private string capNotHaveDuplicateId = "Duplicate Check";
        private string msgNotHaveDuplicateId = "Your ID is available.";

        private string capRequiredItem = "Required Item";
        private string msgRequiredItem = "{0} item is required.";


        public RegisterUser()
        {
            InitializeComponent();

            InitializeEvent();
        }

        private void InitializeEvent()
        {
            Shown += UserRequest_Shown;

            btnDuplicateCheck.Click += BtnDuplicateCheck_Click;
            btnConfirm.Click += BtnConfirm_Click;
            btnCancel.Click += BtnCancel_Click;

            txtPassword.Validating += TxtPassword_Validating;
            txtPassword.InvalidValue += TxtPassword_InvalidValue;

            txtPassword.EditValueChanging += TxtPasswordConfirm_EditValueChanging;
            txtPasswordConfirm.EditValueChanging += TxtPasswordConfirm_EditValueChanging;

            cboEmailDomain.EditValueChanged += CboEmailDomain_EditValueChanged;

            FormClosing += UserRequest_FormClosing;
        }

        private void InitializeLanguage()
        {
            Dictionary<string, object> dicLanguage = new Dictionary<string, object>();

            DataRow[] dataRows = dtDictionary.Select("LANGUAGETYPE = '" + LanguageType + "'");

            foreach (DataRow row in dataRows)
            {
                dicLanguage.Add(row["DICTIONARYID"].ToString(), row["DICTIONARYNAME"]);
            }

            Text = Format.GetString(dicLanguage["USEREQUEST"]);

            lblUserId.Text = Format.GetString(dicLanguage["USERID"]);
            lblPassword.Text = Format.GetString(dicLanguage["PASSWORD"]);
            lblPasswordConfirm.Text = Format.GetString(dicLanguage["PASSWORDCONFIRM"]);
            lblUserName.Text = Format.GetString(dicLanguage["USERNAME"]);
            lblNickName.Text = Format.GetString(dicLanguage["NICKNAME"]);
            lblDescription.Text = Format.GetString(dicLanguage["DESCRIPTION"]);
            lblDepartment.Text = Format.GetString(dicLanguage["DEPARTMENT"]);
            lblPosition.Text = Format.GetString(dicLanguage["POSITION"]);
            lblDuty.Text = Format.GetString(dicLanguage["DUTY"]);
            lblEmailAddress.Text = Format.GetString(dicLanguage["EMAILADDRESS"]);
            lblCellphoneNumber.Text = Format.GetString(dicLanguage["CELLPHONENUMBER"]);
            lblDefaultLanguageType.Text = Format.GetString(dicLanguage["DEFAULTLANGUAGETYPE"]);
            lblPlantId.Text = Format.GetString(dicLanguage["PLANT"]);
            lblHomeAddress.Text = Format.GetString(dicLanguage["HOMEADDRESS"]);

            btnDuplicateCheck.Text = Format.GetString(dicLanguage["REDUPLICATIONCHECK"]);
            btnConfirm.Text = Format.GetString(dicLanguage["CONFIRM"]);
            btnCancel.Text = Format.GetString(dicLanguage["CANCEL"]);


            cboEmailDomain.ValueMember = "CODEID";
            cboEmailDomain.DisplayMember = "CODENAME";
            cboEmailDomain.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboEmailDomain.ShowHeader = false;

            DataTable domain = new DataTable();
            domain.Columns.Add("CODEID", typeof(string));
            domain.Columns.Add("CODENAME", typeof(string));

            domain.Rows.Add("GROUPWARE", Format.GetString(dicLanguage["GROUPWARE"]));
            domain.Rows.Add("DAUM", "Daum");
            domain.Rows.Add("NAVER", "Naver");
            domain.Rows.Add("GMAIL", "Gmail");
            domain.Rows.Add("INPUT", Format.GetString(dicLanguage["DIRECTINPUT"]));

            domain.AcceptChanges();

            cboEmailDomain.DataSource = domain;
            cboEmailDomain.EditValue = "GROUPWARE";
        }

        private void InitializeMessage()
        {
            DataRow[] rowsErrorText = dtDictionary.Select("DICTIONARYID = 'INVALIDVALUE' AND LANGUAGETYPE = '" + LanguageType + "'");

            if (rowsErrorText.Length > 0)
                msgErrorText = rowsErrorText[0]["DICTIONARYNAME"].ToString();


            DataRow[] rowCannotUseId = dtMessage.Select("MESSAGEID = 'CANNOTUSEID' AND LANGUAGETYPE = '" + LanguageType + "'");

            if (rowCannotUseId.Length > 0)
            {
                capCannotUseId = rowCannotUseId[0]["CAPTION"].ToString();
                msgCannotUseId = rowCannotUseId[0]["MESSAGE"].ToString();
            }


            DataRow[] rowsPasswordNotMatching = dtMessage.Select("MESSAGEID = 'PASSWORDNOTMATCHING' AND LANGUAGETYPE = '" + LanguageType + "'");

            if (rowsPasswordNotMatching.Length > 0)
            {
                capPasswordNotMatching = rowsPasswordNotMatching[0]["CAPTION"].ToString();
                msgPasswordNotMatching = rowsPasswordNotMatching[0]["MESSAGE"].ToString();
            }


            DataRow[] rowsPasswordValidate = dtMessage.Select("MESSAGEID = 'PASSWORDVALIDATE' AND LANGUAGETYPE = '" + LanguageType + "'");

            if (rowsPasswordValidate.Length > 0)
                msgPasswordValidate = rowsPasswordValidate[0]["MESSAGE"].ToString();


            DataRow[] rowsUseRequestCancel = dtMessage.Select("MESSAGEID = 'USEREQUESTCANCEL' AND LANGUAGETYPE = '" + LanguageType + "'");

            if (rowsUseRequestCancel.Length > 0)
            {
                capUseRequestCancel = rowsUseRequestCancel[0]["CAPTION"].ToString();
                msgUseRequestCancel = rowsUseRequestCancel[0]["MESSAGE"].ToString();
            }


            DataRow[] rowsUseRequestConfirm = dtMessage.Select("MESSAGEID = 'USEREQUESTCONFRIM' AND LANGUAGETYPE = '" + LanguageType + "'");

            if (rowsUseRequestConfirm.Length > 0)
            {
                capUseRequestConfirm = rowsUseRequestConfirm[0]["CAPTION"].ToString();
                msgUseRequestConfirm = rowsUseRequestConfirm[0]["MESSAGE"].ToString();
            }


            DataRow[] rowsEmptyUserid = dtMessage.Select("MESSAGEID = 'EMPTYUSERID' AND LANGUAGETYPE = '" + LanguageType + "'");

            if (rowsEmptyUserid.Length > 0)
            {
                capEmptyUserId = rowsEmptyUserid[0]["CAPTION"].ToString();
                msgEmptyUserId = rowsEmptyUserid[0]["MESSAGE"].ToString();
            }


            DataRow[] rowsEmptyPassword = dtMessage.Select("MESSAGEID = 'EMPTYPASSWORD' AND LANGUAGETYPE = '" + LanguageType + "'");

            if (rowsEmptyPassword.Length > 0)
            {
                capEmptyPassword = rowsEmptyPassword[0]["CAPTION"].ToString();
                msgEmptyPassword = rowsEmptyPassword[0]["MESSAGE"].ToString();
            }


            DataRow[] rowsEmptyEmailAddress = dtMessage.Select("MESSAGEID = 'EMPTYEMAILADDRESS' AND LANGUAGETYPE = '" + LanguageType + "'");

            if (rowsEmptyEmailAddress.Length > 0)
            {
                capEmptyEmailAddress = rowsEmptyEmailAddress[0]["CAPTION"].ToString();
                msgEmptyEmailAddress = rowsEmptyEmailAddress[0]["MESSAGE"].ToString();
            }


            DataRow[] rowsEmailFormatInvalid = dtMessage.Select("MESSAGEID = 'EMAILFORMATINVALID' AND LANGUAGETYPE = '" + LanguageType + "'");

            if (rowsEmailFormatInvalid.Length > 0)
            {
                capEmailFormatInvalid = rowsEmailFormatInvalid[0]["CAPTION"].ToString();
                msgEmailFormatInvalid = rowsEmailFormatInvalid[0]["MESSAGE"].ToString();
            }


            DataRow[] rowsDoNotDuplicateCheck = dtMessage.Select("MESSAGEID = 'DONOTDUPLICATECHECK' AND LANGUAGETYPE = '" + LanguageType + "'");

            if (rowsDoNotDuplicateCheck.Length > 0)
            {
                capDoNotDuplicateCheck = rowsDoNotDuplicateCheck[0]["CAPTION"].ToString();
                msgDoNotDuplicateCheck = rowsDoNotDuplicateCheck[0]["MESSAGE"].ToString();
            }


            DataRow[] rowsHaveDuplicateId = dtMessage.Select("MESSAGEID = 'HAVEDUPLICATEID' AND LANGUAGETYPE = '" + LanguageType + "'");

            if (rowsHaveDuplicateId.Length > 0)
            {
                capHaveDuplicateId = rowsHaveDuplicateId[0]["CAPTION"].ToString();
                msgHaveDuplicateId = rowsHaveDuplicateId[0]["MESSAGE"].ToString();
            }


            DataRow[] rowsNotHaveDuplicateId = dtMessage.Select("MESSAGEID = 'NOTHAVEDUPLICATEID' AND LANGUAGETYPE = '" + LanguageType + "'");

            if (rowsNotHaveDuplicateId.Length > 0)
            {
                capNotHaveDuplicateId = rowsNotHaveDuplicateId[0]["CAPTION"].ToString();
                msgNotHaveDuplicateId = rowsNotHaveDuplicateId[0]["MESSAGE"].ToString();
            }


            DataRow[] rowsRequiredItem = dtMessage.Select("MESSAGEID = 'REQUIREDITEM' AND LANGUAGETYPE = '" + LanguageType + "'");

            if (rowsRequiredItem.Length > 0)
            {
                capRequiredItem = rowsRequiredItem[0]["CAPTION"].ToString();
                msgRequiredItem = rowsRequiredItem[0]["MESSAGE"].ToString();
            }
        }

        private void InitializeLanguageType()
        {
            cboDefaultLanguageType.ValueMember = "CODEID";
            cboDefaultLanguageType.DisplayMember = "CODENAME";
            cboDefaultLanguageType.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboDefaultLanguageType.ShowHeader = false;

            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add("LANGUAGETYPE", LanguageType);

            cboDefaultLanguageType.DataSource = SqlExecuter.Query("GetLanguageTypeListOnLogin", "00001", dicParam);

            cboDefaultLanguageType.EditValue = LanguageType;


            cboPlantId.ValueMember = "PLANTID";
            cboPlantId.DisplayMember = "PLANTNAME";
            cboPlantId.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboPlantId.ShowHeader = false;

            cboPlantId.DataSource = SqlExecuter.Query("GetPlantListOnLogin", "00001", dicParam);

            cboPlantId.EditValue = PlantId;
        }

        private void UserRequest_Shown(object sender, EventArgs e)
        {
            InitializeLanguage();
            InitializeMessage();
            InitializeLanguageType();
        }

        private void BtnDuplicateCheck_Click(object sender, EventArgs e)
        {
            string strUserId = txtUserId.Text;

            if (Regex.Match(txtUserId.Text, @"[ㄱ-ㅎ가힣]", RegexOptions.ECMAScript).Success ||
                Regex.Match(txtUserId.Text, @"[!,@,#,$,%,^,&,*,?,_,~,-,\,(,)]", RegexOptions.ECMAScript).Success)
            {
                MSGBox.Show(MessageBoxType.Error, capCannotUseId, msgCannotUseId);
                return;
            }

            if (string.IsNullOrEmpty(strUserId))
            {
                //MessageBox.Show(msgEmptyUserId, capEmptyUserId, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MSGBox.Show(MessageBoxType.Warning, capEmptyUserId, msgEmptyUserId);
                return;
            }

            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add("USERID", strUserId);

            DataTable dtResult = SqlExecuter.Query("CheckDuplicateIdOnLogin", "00001", dicParam);

            if (dtResult.Rows.Count > 0)
            {
                //MessageBox.Show(msgHaveDuplicateId, capHaveDuplicateId, MessageBoxButtons.OK, MessageBoxIcon.Information);
                MSGBox.Show(MessageBoxType.Information, capHaveDuplicateId, msgHaveDuplicateId);
                DuplicateCheckResult = false;
            }
            else
            {
                //MessageBox.Show(msgNotHaveDuplicateId, capNotHaveDuplicateId, MessageBoxButtons.OK, MessageBoxIcon.Information);
                MSGBox.Show(MessageBoxType.Information, capNotHaveDuplicateId, msgNotHaveDuplicateId);
                DuplicateCheckResult = true;
            }
        }

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            if (DuplicateCheckResult == null)
            {
                //MessageBox.Show(msgDoNotDuplicateCheck, capDoNotDuplicateCheck, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MSGBox.Show(MessageBoxType.Warning, capDoNotDuplicateCheck, msgDoNotDuplicateCheck);
                return;
            }

            if (DuplicateCheckResult == false)
            {
                //MessageBox.Show(msgHaveDuplicateId, capHaveDuplicateId, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MSGBox.Show(MessageBoxType.Warning, capHaveDuplicateId, msgHaveDuplicateId);
                return;
            }

            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                //MessageBox.Show(msgEmptyPassword, capEmptyPassword, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MSGBox.Show(MessageBoxType.Warning, capEmptyPassword, msgEmptyPassword);
                return;
            }

            if (!string.IsNullOrEmpty(lblPasswordMessage.Text))
            {
                MSGBox.Show(MessageBoxType.Warning, "", msgPasswordValidate);
                return;
            }

            if (txtPassword.Text != txtPasswordConfirm.Text)
            {
                //MessageBox.Show(msgPasswordNotMatching, capPasswordNotMatching, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MSGBox.Show(MessageBoxType.Warning, capPasswordNotMatching, msgPasswordNotMatching);
                return;
            }

            if (string.IsNullOrEmpty(txtUserName.Text.Trim()))
            {
                MSGBox.Show(MessageBoxType.Warning, capRequiredItem, string.Format(msgRequiredItem, lblUserName.Text));
                return;
            }

            if (string.IsNullOrEmpty(txtDepartment.Text.Trim()))
            {
                MSGBox.Show(MessageBoxType.Warning, capRequiredItem, string.Format(msgRequiredItem, lblDepartment.Text));
                return;
            }

            if (string.IsNullOrEmpty(txtPosition.Text.Trim()))
            {
                MSGBox.Show(MessageBoxType.Warning, capRequiredItem, string.Format(msgRequiredItem, lblPosition.Text));
                return;
            }

            if (string.IsNullOrEmpty(txtDuty.Text.Trim()))
            {
                MSGBox.Show(MessageBoxType.Warning, capRequiredItem, string.Format(msgRequiredItem, lblDuty.Text));
                return;
            }

            if (string.IsNullOrEmpty(txtEmailAddress.Text.Trim()) || string.IsNullOrEmpty(txtEmailDomain.Text.Trim()))
            {
                //MessageBox.Show(msgEmptyEmailAddress, capEmptyEmailAddress, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MSGBox.Show(MessageBoxType.Warning, capEmptyEmailAddress, msgEmptyEmailAddress);
                return;
            }

            //if (!new EmailAddressAttribute().IsValid(txtEmailAddress.Text))
            //{
            //    //MessageBox.Show(msgEmailFormatInvalid, capEmailFormatInvalid, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    MSGBox.Show(MessageBoxType.Warning, capEmailFormatInvalid, msgEmailFormatInvalid);
            //    return;
            //}

            if (string.IsNullOrEmpty(txtCellphoneNumber.Text.Trim()))
            {
                MSGBox.Show(MessageBoxType.Warning, capRequiredItem, string.Format(msgRequiredItem, lblCellphoneNumber.Text));
                return;
            }

            //DialogResult dialogResult = MessageBox.Show(msgUseRequestConfirm, capUseRequestConfirm, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (MSGBox.Show(MessageBoxType.Question, capUseRequestConfirm, msgUseRequestConfirm, MessageBoxButtons.YesNo) == DialogResult.Yes)
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

        private void TxtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtPassword.Text.Length < 8 ||
                !Regex.Match(txtPassword.Text, @"\d", RegexOptions.ECMAScript).Success ||
                (!Regex.Match(txtPassword.Text, @"[a-z]", RegexOptions.ECMAScript).Success &&
                 !Regex.Match(txtPassword.Text, @"[A-Z]", RegexOptions.ECMAScript).Success) ||
                !Regex.Match(txtPassword.Text, @"[!,@,#,$,%,^,&,*,?,_,~,-,\,(,)]", RegexOptions.ECMAScript).Success)
            {
                e.Cancel = true;

                lblPasswordMessage.Text = msgPasswordValidate;
            }

            if (!e.Cancel)
                lblPasswordMessage.Text = "";
        }

        private void TxtPassword_InvalidValue(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.DisplayError;
            e.ErrorText = msgErrorText;
        }

        private void TxtPasswordConfirm_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (txtPassword.Text != txtPasswordConfirm.Text)
            {
                lblPasswordConfirmMessage.Text = msgPasswordNotMatching;
            }
            else
            {
                lblPasswordConfirmMessage.Text = "";
            }
        }

        private void CboEmailDomain_EditValueChanged(object sender, EventArgs e)
        {
            string domain = Format.GetString(cboEmailDomain.EditValue);

            if (domain == "INPUT")
                txtEmailDomain.ReadOnly = false;
            else
                txtEmailDomain.ReadOnly = true;

            string enterpriseDomain = "";

            if (EnterpriseId == "INTERFLEX")
                enterpriseDomain = "interflex.co.kr";
            else if (EnterpriseId == "YOUNGPOONG")
                enterpriseDomain = "ypfpc.co.kr";

            switch (domain)
            {
                case "GROUPWARE":
                    txtEmailDomain.Text = enterpriseDomain;
                    break;
                case "DAUM":
                    txtEmailDomain.Text = "daum.net";
                    break;
                case "NAVER":
                    txtEmailDomain.Text = "naver.com";
                    break;
                case "GMAIL":
                    txtEmailDomain.Text = "gmail.com";
                    break;
                case "INPUT":
                    txtEmailDomain.Text = "";
                    break;
            }
        }

        private void UserRequest_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.Cancel)
            {
                //DialogResult dialogResult = MessageBox.Show(msgUseRequestCancel, capUseRequestCancel, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (MSGBox.Show(MessageBoxType.Question, capUseRequestCancel, msgUseRequestCancel, MessageBoxButtons.YesNo) == DialogResult.No)
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
            dtResult.Columns.Add("DESCRIPTION", typeof(string));
            dtResult.Columns.Add("PASSWORD", typeof(string));
            dtResult.Columns.Add("NICKNAME", typeof(string));
            dtResult.Columns.Add("DEPARTMENT", typeof(string));
            dtResult.Columns.Add("POSITION", typeof(string));
            dtResult.Columns.Add("DUTY", typeof(string));
            dtResult.Columns.Add("EMAILADDRESS", typeof(string));
            dtResult.Columns.Add("HOMEADDRESS", typeof(string));
            dtResult.Columns.Add("CELLPHONENUMBER", typeof(string));
            dtResult.Columns.Add("LANGUAGETYPE", typeof(string));
            dtResult.Columns.Add("PLANTID", typeof(string));
            dtResult.Columns.Add("VALIDSTATE", typeof(string));
            dtResult.Columns.Add("USERSTATE", typeof(string));
            dtResult.Columns.Add("_STATE_", typeof(string));

            DataRow row = dtResult.NewRow();
            row["USERID"] = txtUserId.Text;
            row["USERNAME"] = txtUserName.Text;
            row["DESCRIPTION"] = txtDescription.Text;
            row["PASSWORD"] = Cryptography.SHA256Hash(txtPassword.Text);
            row["NICKNAME"] = txtNickName.Text;
            row["DEPARTMENT"] = txtDepartment.Text;
            row["POSITION"] = txtPosition.Text;
            row["DUTY"] = txtDuty.Text;
            row["EMAILADDRESS"] = string.Join("@", txtEmailAddress.Text, txtEmailDomain.Text);
            row["HOMEADDRESS"] = txtHomeAddress.Text;
            row["CELLPHONENUMBER"] = txtCellphoneNumber.Text;
            row["LANGUAGETYPE"] = cboDefaultLanguageType.EditValue.ToString();
            row["PLANTID"] = cboPlantId.EditValue.ToString();
            row["VALIDSTATE"] = "Invalid";
            row["USERSTATE"] = "Request";
            row["_STATE_"] = "added";

            dtResult.Rows.Add(row);

            dtResult.AcceptChanges();

            MessageWorker saveWorker = new MessageWorker("RegisterUser");
            saveWorker.SetBody(new MessageBody()
            {
                { "list", dtResult }
            });

            saveWorker.Execute();
        }
    }
}
