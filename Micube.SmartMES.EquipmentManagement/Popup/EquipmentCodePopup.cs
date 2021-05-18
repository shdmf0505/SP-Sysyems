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

namespace Micube.SmartMES.EquipmentManagement.Popup
{
    public partial class EquipmentCodePopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        public DataRow CurrentDataRow { get; set; }

        public EquipmentCodePopup()
        {
            InitializeComponent();
        }
    }
}
