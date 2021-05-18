#region using

using DevExpress.XtraGrid.Views.Grid;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;

using Micube.Framework.SmartControls.Grid.BandedGrid;
using Micube.Framework.SmartControls.Grid;

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

namespace Micube.SmartMES.Commons.Controls
{
    public partial class ReworkRoutingPop : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region ◆ Public Variables |
        /// <summary>
        /// 팝업진입전 그리드의 Row
        /// </summary>
        public DataRow CurrentDataRow { get; set; }

        // 품목 ID
        public string ProductDefID { get; set; }

        // 품목 Version
        public string ProductDefVersion { get; set; }

        // Lot ID
        public string LotId { get; set; }

        // Routing DefId
        public string ProcessDefId { get; set; }

        // Routing Version
        public string ProcessDefVersion { get; set; }

        // 공정 ID
        public string ProcessSegmentId { get; set; }
        
        // 공정명
        public string ProcessSegmentName { get; set; }

        // 공정수순
        public string UserSequence { get; set; }

        // 공정수순
        public string PathSequence { get; set; }

        // PathID
        public string ProcessPathId { get; set; }

        // Return 공정
        public string ReturnProcessSegmentId { get; set; }

        // Return 공정 수순
        public string ReturnPathSequence { get; set; }
        
        // Return PathID
        public string ReturnPathId { get; set; }

        // Rework 유형
        public string ReworkType { get; set; }

        // 품목명
        public string ProductDefName { get; set; }

        // 재작업 자원 ID
        public string ResourceId { get; set; }

        // 재작업 자원명
        public string ResourceName { get; set; }

        // 리턴공정 자원 ID
        public string ReturnResourceId { get; set; }

        // 리턴공정 자원명
        public string ReturnResourceName { get; set; }

        #endregion

        #region ◆ 생성자 |
        /// <summary>
        /// 생성자
        /// </summary>
        public ReworkRoutingPop()
        {
            InitializeComponent();
            this.Load += Form_Load;
        }

        /// <summary>
        /// Form Load Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form_Load(object sender, EventArgs e)
        {
            // Control Setting
            InitControls();

            // Event Set
            InitEvents();

            // Data 조회
            ucReworkRouting.Apply(this.LotId);

            lblProductDefID.EditValue = this.ProductDefID;
            lblProductDefName.EditValue = this.ProductDefName;
        }
        #endregion

        #region ◆ Control 초기화 |
        /// <summary>
        /// Control 초기화
        /// </summary>
        private void InitControls()
        {
        }

        #endregion

        #region ◆ Event 정의 |
        /// <summary>
        /// 이벤트 정의
        /// </summary>
        private void InitEvents()
        {
            // Button Event
            this.btnSave.Click += BtnSave_Click;
            this.btnCancel.Click += BtnCancel_Click;
        }

        #region ▶ Button Event |
        /// <summary>
        /// 취소 Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            ProcessPathId = string.Empty;
            UserSequence = string.Empty;
            PathSequence = string.Empty;

            ProcessDefId = string.Empty;
            ProcessDefVersion = string.Empty;
            ProcessSegmentId = string.Empty;
            ProcessSegmentName = string.Empty;

            ReturnProcessSegmentId = string.Empty;
            ReturnPathSequence = string.Empty;

            ReworkType = string.Empty;

            this.Close();
        }

        /// <summary>
        /// 선택 버튼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            ucReworkRouting.FillResultProperties();

            // TODO : 자원 저장 및 ReworkReturn 저장하도록 저장 룰 수정

            // 품목 Routing 인경우
            if(ucReworkRouting.IsProductRouting)
            {
                ProcessPathId = ucReworkRouting.ReworkProcessPathId;
                UserSequence = ucReworkRouting.ReworkUserSequence;
                PathSequence = ucReworkRouting.ReworkPathSequence.ToString();
                ProcessDefId = ucReworkRouting.LotProcessDefId;
                ProcessDefVersion = ucReworkRouting.LotProcessDefVersion;
                ProcessSegmentId = ucReworkRouting.ReworkProcessSegmentId;
                ProcessSegmentName = ucReworkRouting.ReworkProcessSegmentName;
                ResourceId = ucReworkRouting.ResourceId;
                ResourceName = ucReworkRouting.ResourceName;

                ReworkType = "PRODUCT";
            }
            else
            {
                ProcessDefId = ucReworkRouting.ReworkProcessDefId;
                ProcessDefVersion = ucReworkRouting.ReworkProcessDefVersion;
                ProcessSegmentId = ucReworkRouting.ReworkProcessSegmentId;
                ProcessSegmentName = ucReworkRouting.ReworkProcessSegmentName;
                ProcessPathId = ucReworkRouting.ReworkProcessPathId;
                UserSequence = ucReworkRouting.ReworkUserSequence;
                PathSequence = ucReworkRouting.ReworkPathSequence.ToString();
                ResourceId = ucReworkRouting.ToResourceId;
                ResourceName = ucReworkRouting.ToResourceName;

                // Return 공정
                ReturnProcessSegmentId = ucReworkRouting.ReturnProcessSegmentId;
                ReturnPathSequence = ucReworkRouting.ReturnPathSequence.ToString();
                ReturnPathId = ucReworkRouting.ReturnProcessPathId;
                ReturnResourceId = ucReworkRouting.ReturnResourceId;
                ReturnResourceName = ucReworkRouting.ReturnResourceName;

                ReworkType = "REWORK";
            }
            this.Close();
        }
        #endregion

        #endregion
    }
}
