using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;

using Micube.Framework;
using Micube.Framework.Log;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;

using Ninject;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using TimeZoneConverter;


namespace SmartMES
{
    public partial class LoginForm : XtraForm
    {
        private string _EnterpriseId = "";

        private DataTable _Dictionary = new DataTable();
        private DataTable _Message = new DataTable();

        private Settings.LoginSettingRepository _LoginSettingRepository = new Settings.LoginSettingRepository();

        private int _ServiceCount;
        private Dictionary<string, IConvertible> _ServiceList;


        #region 폼 배경 투명 처리

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect, // x-coordinate of upper-left corner
            int nTopRect, // y-coordinate of upper-left corner
            int nRightRect, // x-coordinate of lower-right corner
            int nBottomRect, // y-coordinate of lower-right corner
            int nWidthEllipse, // height of ellipse
            int nHeightEllipse // width of ellipse
        );

        [DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);

        [DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        [DllImport("dwmapi.dll")]
        public static extern int DwmIsCompositionEnabled(ref int pfEnabled);

        private bool m_aeroEnabled;                     // variables for box shadow
        private const int CS_DROPSHADOW = 0x00020000;
        private const int WM_NCPAINT = 0x0085;
        private const int WM_ACTIVATEAPP = 0x001C;

        public struct MARGINS                           // struct for box shadow
        {
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;
        }

        private const int WM_NCHITTEST = 0x84;          // variables for dragging the form
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;

        protected override CreateParams CreateParams
        {
            get
            {
                m_aeroEnabled = CheckAeroEnabled();

                CreateParams cp = base.CreateParams;
                if (!m_aeroEnabled)
                    cp.ClassStyle |= CS_DROPSHADOW;

                return cp;
            }
        }

        private bool CheckAeroEnabled()
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                int enabled = 0;
                DwmIsCompositionEnabled(ref enabled);
                return (enabled == 1) ? true : false;
            }
            return false;
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCPAINT:                        // box shadow
                    if (m_aeroEnabled)
                    {
                        var v = 2;
                        DwmSetWindowAttribute(this.Handle, 2, ref v, 4);
                        MARGINS margins = new MARGINS()
                        {
                            bottomHeight = 0,
                            leftWidth = 0,
                            rightWidth = 0,
                            topHeight = 0
                        };
                        DwmExtendFrameIntoClientArea(this.Handle, ref margins);

                    }
                    break;
                default:
                    break;
            }
            base.WndProc(ref m);

            if (m.Msg == WM_NCHITTEST && (int)m.Result == HTCLIENT)     // drag the form
                m.Result = (IntPtr)HTCAPTION;
        }

        #endregion

        public LoginForm()
        {
            InitializeComponent();

            SetEvent();

            GetLanguage();
            InitializeControls();
        }

        private void InitializeControls()
        {
            try
            {
                _LoginSettingRepository.Get("");

                // Service List
                DataTable dataSource = new DataTable();
                dataSource.Columns.Add("SERVICE");

                cboService.DisplayMember = "SERVICE";
                cboService.ValueMember = "SERVICE";
                cboService.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;

                Logger.Info("Get service list start.");
                string defaultService = AppConfiguration.GetString("Application.Service.Default");
                _ServiceList = AppConfiguration.GetDictionary("Application.Service.Url");
                Logger.Info("Get service list complete.");

                _ServiceCount = _ServiceList.Count;

                if (_ServiceCount > 1)
                    _ServiceList.ForEach(value => dataSource.Rows.Add(value.Key));
                else
                    dataSource.Rows.Add(defaultService);

                cboService.DataSource = dataSource;

                cboService.EditValue = defaultService;


                // LanguageType List
                //string languageType = ConfigurationManager.AppSettings["DefaultLanguageType"];
                string languageType = SettingConfig.Current.LanguageType;

                Dictionary<string, object> dicParam = new Dictionary<string, object>();
                dicParam.Add("LANGUAGETYPE", string.IsNullOrEmpty(languageType) ? "ko-KR" : languageType);

                cboLanguageType.Properties.DisplayMember = "CODENAME";
                cboLanguageType.Properties.ValueMember = "CODEID";
                cboLanguageType.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;

                Logger.Info("Get langauge type list start.");
                cboLanguageType.DataSource = SqlExecuter.Query("GetLanguageTypeListOnLogin", "00001", dicParam);
                Logger.Info("Get langauge type list complete.");


                cboLanguageType.EditValueChanged -= CboLanguageType_EditValueChanged;

                cboLanguageType.EditValue = dicParam["LANGUAGETYPE"];

                cboLanguageType.EditValueChanged += CboLanguageType_EditValueChanged;


                // Plant List
                //string plantId = ConfigurationManager.AppSettings["DefaultPlant"];
                string plantId = SettingConfig.Current.PlantId;

                cboPlantId.Properties.DisplayMember = "PLANTNAME";
                cboPlantId.Properties.ValueMember = "PLANTID";
                cboPlantId.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;

                Logger.Info("Get plant list start.");
                DataTable dtPlantList = SqlExecuter.Query("GetPlantListOnLogin", "00001", dicParam);
                Logger.Info("Get plant list complete.");

                cboPlantId.DataSource = dtPlantList;


                // Check Enterprise
                _EnterpriseId = Format.GetString(dtPlantList.AsEnumerable().FirstOrDefault()["ENTERPRISEID"]);

                if (_EnterpriseId == "INTERFLEX")
                {
                    picLogo.Image = new Bitmap(Properties.Resources.Logo_Interflex);
                }
                else if (_EnterpriseId == "YOUNGPOONG")
                {
                    picLogo.Image = new Bitmap(Properties.Resources.Logo_Youngpoong);
                }


                DataRow[] drPlants = dtPlantList.Select("PLANTID = '" + plantId + "'");

                if (string.IsNullOrEmpty(plantId) || drPlants.Count() < 1)
                    cboPlantId.ItemIndex = 0;
                else
                    cboPlantId.EditValue = plantId;


                // Login ID Save Y/N
                //string strSaveLoginIdYn = ConfigurationManager.AppSettings["SaveLoginIdYn"];

                Logger.Info("Get save id YN start.");
                bool saveLoginIdYn = SettingConfig.Current.IsSaveLoginId;
                Logger.Info("Get save id YN complete.");


                chkIDSave.EditValue = saveLoginIdYn;


                // If Save Login ID then Get Login ID
                if (saveLoginIdYn)
                {
                    //string saveLoginId = ConfigurationManager.AppSettings["SaveLoginId"];
                    Logger.Info("Get save id start.");
                    string saveLoginId = SettingConfig.Current.SaveLoginId;
                    Logger.Info("Get save id complete.");

                    txtID.EditValue = saveLoginId;
                }


                // Initializing Use Request and Password Forgot Control Text
                InitializeLinkControlText();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());

                _LoginSettingRepository.Delete();

                MSGBox.Show(MessageBoxType.Information, "G-MES", "프로그램 실행 중 오류가 발생하였습니다. 프로그램을 다시 실행 해주시기 바랍니다.");

                Process.GetCurrentProcess().Kill();
            }
        }

        /// <summary>
        /// 로그인 화면에서 사용되는 다국어 용어 및 메시지 조회
        /// </summary>
        private void GetLanguage()
        {
            Logger.Info("Get language list start.");
            _Dictionary = SqlExecuter.Query("GetLanguageListOnLogin", "00001");
            Logger.Info("Get language list complete.");

            Logger.Info("Get message list start.");
            _Message = SqlExecuter.Query("GetMessageListOnLogin", "00001");
            Logger.Info("Get message list complete.");
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        /// <summary>
        /// 사용 신청, 비밀번호 분실 컨트롤 Text 다국어 처리
        /// </summary>
        private void InitializeLinkControlText()
        {
            string strRequest = "Use Request";

            DataRow[] rowsRequest = _Message.Select("MESSAGEID = 'USEREQUEST' AND LANGUAGETYPE = '" + cboLanguageType.EditValue.ToString() + "'");

            if (rowsRequest.Length > 0)
                strRequest = rowsRequest[0]["MESSAGE"].ToString();

            linkRequest.Text = strRequest;


            string strPasswordForgot = "I forgot my password.";

            DataRow[] rowsPasswordForgot = _Message.Select("MESSAGEID = 'PASSWORDFORGOT' AND LANGUAGETYPE = '" + cboLanguageType.EditValue.ToString() + "'");

            if (rowsPasswordForgot.Length > 0)
                strPasswordForgot = rowsPasswordForgot[0]["MESSAGE"].ToString();

            linkForgot.Text = strPasswordForgot;
        }

        #region Event

        /// <summary>
        /// Event 초기화
        /// </summary>
        private void SetEvent()
        {
            txtPW.KeyDown += TxtPW_TextEnterKeyPress;
            txtID.KeyDown += TxtPW_TextEnterKeyPress;
            txtID.Enter += TxtID_Enter;
            txtPW.Enter += TxtPW_GotFocus;
            txtID.CustomDisplayText += TxtID_CustomDisplayText;
            txtPW.CustomDisplayText += TxtID_CustomDisplayText;
            picClose.Click += PicClose_Click;

            linkRequest.Click += LinkRequest_Click;
            linkForgot.Click += LinkForgot_Click;

            cboLanguageType.EditValueChanged += CboLanguageType_EditValueChanged;
        }

        /// <summary>
        /// 사용 신청 Click 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LinkRequest_Click(object sender, EventArgs e)
        {
            RegisterUser requestForm = new RegisterUser();
            requestForm.LanguageType = cboLanguageType.EditValue.ToString();
            requestForm.PlantId = cboPlantId.EditValue.ToString();
            requestForm.EnterpriseId = _EnterpriseId;
            requestForm.dtDictionary = _Dictionary;
            requestForm.dtMessage = _Message;

            requestForm.ShowDialog();
        }

        /// <summary>
        /// 비밀번호 분실 Click 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LinkForgot_Click(object sender, EventArgs e)
        {
            ForgotPassword forgotPasswordForm = new ForgotPassword();
            forgotPasswordForm.LanguageType = cboLanguageType.EditValue.ToString();
            forgotPasswordForm.dtDictionary = _Dictionary;
            forgotPasswordForm.dtMessage = _Message;

            forgotPasswordForm.ShowDialog();
        }

        /// <summary>
        /// LanguageType 변경 시 다국어 용어 및 메시지 언어권 변경
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboLanguageType_EditValueChanged(object sender, EventArgs e)
        {
            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add("LANGUAGETYPE", cboLanguageType.EditValue);

            cboPlantId.DataSource = SqlExecuter.Query("GetPlantListOnLogin", "00001", dicParam);

            cboLanguageType.DataSource = SqlExecuter.Query("GetLanguageTypeListOnLogin", "00001", dicParam);


            InitializeLinkControlText();
        }

        private void TxtID_CustomDisplayText(object sender, DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs e)
        {
            var textBox = sender as SmartTextBox;

            if (e.Value == null || string.IsNullOrEmpty(e.Value.ToString()))
            {
                if (textBox == txtID)
                {
                    e.DisplayText = "ID";

                    txtID.Properties.Appearance.ForeColor = Color.FromArgb(230, 230, 230);
                }
                else if (textBox == txtPW)
                {
                    e.DisplayText = "Password";

                    txtPW.Properties.Appearance.ForeColor = Color.FromArgb(230, 230, 230);
                }
            }
            else
            {
                if (textBox == txtID)
                    txtID.Properties.Appearance.ForeColor = Color.FromArgb(16, 56, 90);
                else if (textBox == txtPW)
                    txtPW.Properties.Appearance.ForeColor = Color.FromArgb(16, 56, 90);
            }
        }

        private void TxtID_Enter(object sender, EventArgs e)
        {
            txtID.SelectAll();
        }

        private void TxtPW_TextEnterKeyPress(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (sender == txtID) txtPW.Focus();
                else this.BtnMESLogin_Click(btnMESLogin, EventArgs.Empty);
            }

        }

        private void SmartLogin_Load(object sender, EventArgs e)
        {
            if (txtID.EditValue == null || string.IsNullOrWhiteSpace(txtID.EditValue.ToString()))
            {
                this.txtID.Focus();
                this.txtID.Select();
            }
            else if (txtPW.EditValue == null || string.IsNullOrWhiteSpace(txtPW.EditValue.ToString()))
            {
                this.txtPW.Focus();
                this.txtPW.Select();
            }

            Language.ChangeLanguage();

            LoadFirstForm();
        }

        private async void BtnMESLogin_Click(object sender, EventArgs e)
        {
            try
            {
                bool validation = LoginValidation();
                if (!validation)
                    return;

                await LoginCoreAsync(txtID.Text, txtPW.Text);

                DialogManager.ShowWaitDialog();

                MessageWorker worker = new MessageWorker("SaveConnectionHistory");

                worker.SetBody(new MessageBody()
                {
                    { "UserId", UserInfo.Current.Id },
                    { "ConnectionType", "Login" },
                    { "ConnectionTime", UserInfo.Current.LoginTime }
                });

                var saveResult = worker.Execute<DataTable>();
                DataTable resultData = saveResult.GetResultSet();

                UserInfo.Current.ConnectionKey = Format.GetString(resultData.Rows[0]["TXNHISTKEY"]);

                Hide();

                txtPW.Text = "";

                MainForm mainForm = Micube.Framework.Modules.NinjectProgram.Kernel.Get<MainForm>();
                mainForm.CallMenu = this.CallMenu;
                mainForm._ServiceCount = _ServiceCount;
                mainForm._ServiceList = _ServiceList;
                mainForm.Show(this);
                mainForm.Activate();
            }
            catch (Exception ex)
            {
                Micube.Framework.SmartControls.Helpers.UIHelper.ShowError(ex);
            }
            finally
            {
                DialogManager.Close();
            }
        }

        private void TxtPW_GotFocus(object sender, EventArgs e)
        {
            this.txtPW.SelectAll();
        }

        private void TxtID_GotFocus(object sender, EventArgs e)
        {
            this.txtID.SelectAll();
        }

        private void PicClose_Click(object sender, EventArgs e)
        {
            MouseEventArgs args = e as MouseEventArgs;

            if (args.Button == MouseButtons.Left)
                Close();
        }

        #endregion

        /// <summary>
        /// 로그인 시 UserInfo, FrameworkInfo 초기화
        /// </summary>
        /// <param name="loginData">로그인 사용자 정보</param>
        private void InitLogin(DataTable loginData)
        {
            var row = loginData.Rows[0];

            UserInfo.Current.Id = Format.GetString(row["USERID"]);
            UserInfo.Current.Name = Format.GetString(row["USERNAME"]);
            UserInfo.Current.Department = Format.GetString(row["DEPARTMENT"]);
            UserInfo.Current.Uiid = cboService.EditValue.ToString();
            UserInfo.Current.ServiceId = cboService.EditValue.ToString();
            UserInfo.Current.DefaultService = AppConfiguration.GetString("Application.Service.Default");
            UserInfo.Current.EmailAddress = Format.GetString(row["EMAILADDRESS"]);
            UserInfo.Current.CellPhoneNumber = Format.GetString(row["CELLPHONENUMBER"]);

            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            for (int i = 0; i < host.AddressList.Length; i++)
            {
                if (host.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                {
                    UserInfo.Current.UserIp = host.AddressList[i].ToString();
                }
            }

            UserInfo.Current.LanguageType = cboLanguageType.EditValue.ToString();
            UserInfo.Current.Enterprise = Format.GetString(row["ENTERPRISEID"]);
            //UserInfo.Current.Plant = cboPlantId.EditValue.ToString();
            UserInfo.Current.Plant = Format.GetString(row["PLANTID"]);
            UserInfo.Current.LoginPlant = Format.GetString(cboPlantId.EditValue);
            UserInfo.Current.Area = Format.GetString(row["AREAID"]);
            //UserInfo.Current.PlantStartBusinessHour = cboPlantId.GetColumnValue("STARTBUSINESSHOUR").ToString();
            UserInfo.Current.PlantStartBusinessHour = Format.GetString(row["STARTBUSINESSHOUR"]);
            UserInfo.Current.TimeZone = TZConvert.WindowsToIana(TimeZoneInfo.Local.Id);
            UserInfo.Current.LoginTime = DateTime.Now;

            //smjang - 로그인한 유저의 저장권한 사이트 쿼리로 조회
            Dictionary<string, object> userParam = new Dictionary<string, object>();
            userParam.Add("P_USERID", UserInfo.Current.Id);
            userParam.Add("P_ENTERPRISEID", UserInfo.Current.Enterprise);
            SqlQuery userQuery = new SqlQuery("GetUserAuthorityPlantList", "00001", userParam);
            DataTable dtUserPlant = userQuery.Execute();
            if (dtUserPlant != null && dtUserPlant.Rows.Count > 0)
            {
                UserInfo.Current.AuthorityPlant = dtUserPlant.AsEnumerable().Select(c => c["PLANTID"].ToString()).ToList();
            }

            //smjang - 설정된 프린터 셋팅
            UserInfo.Current.Printer = ConfigurationManager.AppSettings["DefaultPrinter"];
            UserInfo.Current.PrinterType = ConfigurationManager.AppSettings["PrinterType"];

            SetConfigInformation();

            Language.LanguageTypes.Clear();

            FrameworkSettings.Initialize(_ServiceList);

            NetworkSettings.SetServiceUrl(UserInfo.Current.Uiid);
        }

        /// <summary>
        /// LoginSetting.json 파일에 로그인 정보 저장
        /// </summary>
        private void SetConfigInformation()
        {
            SettingConfig.Current.LanguageType = cboLanguageType.EditValue.ToString();
            SettingConfig.Current.PlantId = cboPlantId.EditValue.ToString();
            SettingConfig.Current.IsSaveLoginId = (bool)chkIDSave.EditValue;
            SettingConfig.Current.SaveLoginId = (bool)chkIDSave.EditValue ? txtID.EditValue.ToString() : "";

            _LoginSettingRepository.Save("", SettingConfig.Current);
        }

#if DEBUG
        public void LoginCore(string userId, string password)
        {
            // application 종료될때 logout rule 호출 추가?                
            MessageWorker worker = new MessageWorker("LoginForDotnet");

            worker.SetBody(new MessageBody()
            {
                { "id", userId},
                { "password", Cryptography.SHA256Hash(password) }
            });



            var loginResult = worker.Execute<DataTable>();
            var resultData = loginResult.GetResultSet(); // .GetObject<Dictionary<string, object>>();

            InitLogin(resultData);
        }
#endif

        public async Task LoginCoreAsync(string userId, string password)
        {
            // application 종료될때 logout rule 호출 추가?
            NetworkSettings.SetServiceUrl(cboService.EditValue.ToString());

            MessageWorker worker = new MessageWorker("LoginForDotnet");

            worker.SetBody(new MessageBody()
            {
                { "id", userId },
                { "password", Cryptography.SHA256Hash(password) }
            });

            var loginResult = await worker.ExecuteAsync<DataTable>(null);
            var resultData = loginResult.GetResultSet();

            // 사용자 상태가 분실 또는 신규(사용자 정보 화면에서 등록) 인 경우(임시 비밀번호) 비밀번호 변경 처리
            if (resultData.Rows[0]["USERSTATE"].Equals("Forgot") || resultData.Rows[0]["USERSTATE"].Equals("Create"))
            {
                ChangePasswordOnLogin changePasswordForm = new ChangePasswordOnLogin(txtID.Text);
                changePasswordForm.LanguageType = cboLanguageType.EditValue.ToString();
                changePasswordForm.dtDictionary = _Dictionary;
                changePasswordForm.dtMessage = _Message;
                changePasswordForm.ShowDialog(this);
            }

            InitLogin(resultData);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            DataRow[] dataRows = _Message.Select("MESSAGEID = 'LOGINCANCEL' AND LANGUAGETYPE = '" + cboLanguageType.EditValue.ToString() + "'");

            string strMessage = "Do you want to cancel the login?";
            string strCaption = "Comfirm Login Cancel";

            if (dataRows.Length > 0)
            {
                strMessage = dataRows[0]["MESSAGE"].ToString();
                strCaption = dataRows[0]["CAPTION"].ToString();
            }

            DialogResult result = MSGBox.Show(MessageBoxType.Question, strCaption, strMessage, MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
                Application.Exit();
            else
                e.Cancel = true;
        }

        public void Cancel()
        {
            DataRow[] dataRows = _Message.Select("MESSAGEID = 'LOGINCANCEL' AND LANGUAGETYPE = '" + cboLanguageType.EditValue.ToString() + "'");

            string strMessage = "Do you want to cancel the login?";
            string strCaption = "Comfirm Login Cancel";

            if (dataRows.Length > 0)
            {
                strMessage = dataRows[0]["MESSAGE"].ToString();
                strCaption = dataRows[0]["CAPTION"].ToString();
            }

            DialogResult result = MSGBox.Show(MessageBoxType.Question, strCaption, strMessage, MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
                Application.Exit();
        }

        /// <summary>
        /// 아이디, 비밀번호 정상 입력 여부 확인
        /// </summary>
        /// <returns></returns>
        public bool LoginValidation()
        {
            string message = "";
            if (string.IsNullOrWhiteSpace(this.txtID.Text))
            {
                message += "아이디 ";
            }

            if (string.IsNullOrWhiteSpace(this.txtPW.Text))
            {
                message += "비밀번호 ";
            }

            if (!string.IsNullOrWhiteSpace(message))
            {
                message += "를 입력하세요";
                MSGBox.Show(MessageBoxType.Information, "Login", message);
                return false;
            }
            else
                return true;
        }

        /// <summary>
        /// 디버그용 띄울 창
        /// </summary>
        public MenuInfo CallMenu { get; set; }

        private void LoadFirstForm()
        {
#if DEBUG
            if (this.CallMenu != null)
            {
                string defaultLogin = ApplicationConfig.Current.GetDefaultLogin();
                string[] debugLogin = null;
                if (!string.IsNullOrWhiteSpace(defaultLogin))
                {
                    debugLogin = defaultLogin.Split(new char[] { '/' });
                }
                else
                {
                    debugLogin = new string[] { "admin", "admin" };
                }

                txtID.EditValue = debugLogin[0];
                txtPW.EditValue = debugLogin[1];

                BtnMESLogin_Click(btnMESLogin, EventArgs.Empty);
            }
#endif
        }
    }
}