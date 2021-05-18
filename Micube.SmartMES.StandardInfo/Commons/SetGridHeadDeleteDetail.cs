#region using
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프 로 그 램 명  : 그리드 툴 삭제 버튼 제어
    /// 업  무  설  명  : 그리드 추가 시에만 삭제 버튼 활성화 
    /// 생    성    자  : 윤성원
    /// 생    성    일  : 2019-07-05
    /// 수  정  이  력  : 
    /// </summary>
    public class SetGridHeadDeleteDetail
    {
        Micube.Framework.SmartControls.SmartBandedGrid _gridH;
        object[] _gridD;
        string[] _sKeyH;
        string[] _sKeyD;
        /// <summary>
        /// 그리드 마이너스버튼숨기기
        /// </summary>
        /// <param name="grid">그리드</param>

        /// <returns></returns>
        public SetGridHeadDeleteDetail(Micube.Framework.SmartControls.SmartBandedGrid gridH,object[] gridD, string[] sKeyH,string[] sKeyD)
        {
            //현재행
            _gridH = gridH;
            _gridD = gridD;
            _sKeyH = sKeyH;
            _sKeyD = sKeyD;
            _gridH.ToolbarDeletingRow += _gridH_ToolbarDeletingRow;
        }

        private void _gridH_ToolbarDeletingRow(object sender, CancelEventArgs e)
        {
            for (int grdi = 0; grdi < _gridD.Length; grdi++)
            {
                Micube.Framework.SmartControls.SmartBandedGrid _gridD1 = (Micube.Framework.SmartControls.SmartBandedGrid)_gridD[grdi];

                DataTable dt = (DataTable)_gridD1.DataSource;

                DataRow row = _gridH.View.GetFocusedDataRow();

                string sskey = "";
                for (int ikeyh = 0; ikeyh < _sKeyH.Length; ikeyh ++)
                {
                    sskey = _sKeyD[ikeyh] + "='" + row[_sKeyH[ikeyh]].ToString() + "'" + " AND";
                }

                if (dt.Select(sskey.Substring(0, sskey.Length - 3)).Length != 0)
                {
                    MSGBox.Show(MessageBoxType.Information, "GridDetailChk");
                    e.Cancel = true;
                    break;
                }

            }
        }

     


    }

   
}
