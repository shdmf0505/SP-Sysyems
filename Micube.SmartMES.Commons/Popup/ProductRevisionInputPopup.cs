#region using

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

#endregion

namespace Micube.SmartMES.Commons
{
    public partial class ProductRevisionInputPopup : SmartPopupBaseForm
    {
        public ProductRevisionInputPopup()
        {
            InitializeComponent();

            Text = Language.Get("PRODUCTREVISIONINPUTPOPUP");
            lblProductRevisionInputContext.Text = Language.GetMessage("InputProductRevision").Message;
            lblProductRevisionInputTop.Text = Language.Get("PRODUCTREVISIONINPUT");
            lblProductRevisionInputBottom.Text = Language.Get("PRODUCTREVISIONBARCODE");
            btnPrintRCLotCard.Text = Language.Get("PRINTRCLOTCARD");

            InitializeEvent();
        }

        #region Event

        private void InitializeEvent()
        {
            Shown += ProductRevisionInputPopup_Shown;

            txtProductRevisionInput.KeyDown += TxtProductRevisionInput_KeyDown;
            btnPrintRCLotCard.Click += BtnPrintRCLotCard_Click;

            FormClosing += ProductRevisionInputPopup_FormClosing;
        }

        private void ProductRevisionInputPopup_Shown(object sender, EventArgs e)
        {
            txtProductRevisionInput.Focus();
        }

        private void TxtProductRevisionInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (_ProductRevision != txtProductRevisionInput.Text)
                {
                    // 스캔 한 품목 Revision과 현재 Lot의 품목 Revision이 일치하지 않습니다. 변경 Lot Card를 출력하시기 바랍니다.
                    MSGBox.Show(MessageBoxType.Warning, "PleasePrintRCLotCard");
                    txtProductRevisionInput.Text = "";
                    return;
                }
                else
                {
                    DialogResult = DialogResult.OK;

                    Close();
                }
            }
        }

        private void BtnPrintRCLotCard_Click(object sender, EventArgs e)
        {
            // LotCardType = RCChange 변경 필요
            CommonFunction.PrintLotCard(_LotId, LotCardType.Normal);


            MessageWorker worker = new MessageWorker("SavePrintLotCard");
            worker.SetBody(new MessageBody()
            {
                { "LotId", _LotId }
            });

            worker.Execute();
        }

        private void ProductRevisionInputPopup_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult != DialogResult.OK)
                e.Cancel = true;
        }

        #endregion

        #region Public Variables

        public string _LotId = "";
        public string _ProductRevision = "";

        #endregion
    }
}