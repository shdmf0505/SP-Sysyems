using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.UserSkins;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using Micube.SmartMES.SPC;

namespace Micube.SmartMES.SPCStatus
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            BonusSkins.Register();

            try
            {

                SpcFlag.isTestMode = 1;

                //시계열
                //Application.Run(new TestSpcBaseChart01()); //시계열

                //R&R
                //Application.Run(new TestSpcBaseChart0302DB());

                //P 관리도
                //Application.Run(new TestSpcBaseChart03()); //UC Control - P Chart

                //* XBAR 관리도
                //TestSpcBaseChart03XBar01
                //Application.Run(new TestSpcBaseChart03XBar01()); //UC Control - XBar Chart
                //Application.Run(new TestSpcBaseChart05XBarFrame()); //UC Control - XBar Frame Chart

                //* 엑셀 Test
                //Application.Run(new TestSpcBaseChart0302DB()); 

                //* Function Test
                Application.Run(new TestSpcBaseChart03());

                //* Cpk
                //Application.Run(new TestSpcBaseChart04CpkFrame());
                //Application.Run(new TestSpcBaseChart04Cpk());


                //* Etc
                //Application.Run(new TestSpcBaseChart0301Raw());
                //Application.Run(new TestSpcBaseChart02()); Chart 직접 기능 Test
                //Application.Run(new TestSpcDataCreate01());//일반 기능 Test

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }
    }//end class
}//end namespace
