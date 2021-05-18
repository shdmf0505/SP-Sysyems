#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion

/// <summary>
/// QualityAnalysis EtchingRate 에서 사용되는 공통 함수
/// </summary>

namespace Micube.SmartMES.QualityAnalysis
{
    class EtchingRateHelper
    {
        public static byte[] SetConvertFile(string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                byte[] b = File.ReadAllBytes(fileName);
                fs.Read(b, 0, Convert.ToInt32(fs.Length));

                fs.Close();
                return b;
            }
        }
    }
}
