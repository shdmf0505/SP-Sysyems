#region using
using Micube.SmartMES.Commons.SPCLibrary;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
#endregion

/// <summary>
/// SPC Test Form 실행.
/// </summary>
namespace Micube.SmartMES.SPCStatus
{
    /// <summary>
    /// SPC Test Form 실행.
    /// </summary>
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

            Application.Run(new TestSPCStatisticsData());
            //Application.Run(new TestSPCLibrary01());
        }
    }
}
