using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Micube.Framework.Log;
using Ninject;

namespace SmartMES
{
    class Program : Micube.Framework.Modules.NinjectProgram
    {
        /// <summary>
        /// The main entry point for the application.
        /// MenuId=CodeClassManagement&ProgramId=Micube.SmartEES.SystemManagement.CodeClassManagement&Caption=코드그룹관리
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            bool createdNew = true;
            using (Mutex mutex = new Mutex(true, "SmartMES", out createdNew))
            {
                if(!createdNew)
                {
                    MessageBox.Show("Program is already running!");
                    return;
                }
                /*
                if (CheckProcess())
                {
                    MessageBox.Show("Program is already running!");
                    return;
                }
                */
                Kernel = new StandardKernel();
                Kernel.Load(new Modules.InjectModule());
                Kernel.Load(new Micube.Framework.SmartControls.Modules.InjectModule());
                //Kernel.Load(new Micube.SmartEES.Fdc.Modules.InjectModule());

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                //BonusSkins.Register();
                //SkinManager.EnableFormSkins();
                //SkinManager.EnableMdiFormSkins();
                //UserLookAndFeel.Default.SetSkinStyle(SkinSvgPalette.Bezier.Vacuum);
                WindowsFormsSettings.DefaultFont = new System.Drawing.Font("Tahoma", 10F);

                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
                Application.ThreadException += Application_ThreadException;
                //DialogManager.ShowStartSplashScreen();


                //MainForm mainForm = new MainForm();
                //Form mainForm = Kernel.Get<Micube.Framework.SmartControls.Interface.IOpenMenu>() as Form;
                LoginForm loginForm = new LoginForm();
    #if DEBUG
                loginForm.CallMenu = GetFirstFormName();
    #endif
                Application.Run(loginForm);
                //Application.Run(loginForm);
            }
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {

            Micube.Framework.SmartControls.Helpers.UIHelper.ShowError(e.ExceptionObject as Exception);
            Logger.Error((e.ExceptionObject as Exception).Message);
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            Micube.Framework.SmartControls.Helpers.UIHelper.ShowError(e.Exception);
            Logger.Error(e.Exception.Message);
        }

        private static bool CheckProcess()
        {
            string processName = Process.GetCurrentProcess().ProcessName;

            var process = from p in Process.GetProcesses()
                          where p.ProcessName.ToUpper() == processName.ToUpper()
                          select p;

            return process.Count() > 1;
        }

#if DEBUG

        static MenuInfo GetFirstFormName()
        {
            string startUp = ConfigurationManager.AppSettings["StartUp"];
            MenuInfo result = null;

            var commandLines = Helpers.DebugHelper.GetParameter(startUp);
            if (commandLines != null)
            {
                //MenuId=CodeClassManagement;ProgramId=Micube.SmartEES.SystemManagement.CodeClassManagement;Caption=코드그룹관리
                if (commandLines.ContainsKey("MenuId") && commandLines.ContainsKey("ProgramId"))
                {
                    result = new MenuInfo();
                    result.MenuId = commandLines["MenuId"];
                    result.ProgramId = commandLines["ProgramId"];
                    if (commandLines.ContainsKey("Caption")) result.Caption = commandLines["Caption"];
                }
            }

            return result;
        }

#endif
    }
}
