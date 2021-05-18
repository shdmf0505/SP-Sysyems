#region using

using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
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

namespace Micube.SmartMES.SPC
{
    /// <summary>
    /// 프 로 그 램 명  : SPC> 공통 Popup> 
    /// 업  무  설  명  : 상세  분석 자료 표시
    /// 생    성    자  : 이승우
    /// 생    성    일  : 2019-07-10
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class SpcStatusDetailChartPopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region Local Variables

        /// <summary>
        /// 팝업진입전 그리드의 Row
        /// </summary>
        public DataRow CurrentDataRow { get; set; }


        #endregion

        #region 생성자

        /// <summary>
        /// 생성자
        /// </summary>
        public SpcStatusDetailChartPopup()
        {
            InitializeComponent();

            InitializeEvent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            this.Load += SpcStatusDetailChartPopup_Load;
            this.ucChartDetail1.SpcChartDetailbtnCloseClickEventHandler += UcChartDetail1_SpcChartDetailbtnCloseClickEventHandler;
            //this.Resize += SpcStatusDetailChartPopup_Resize;
            
        }

        /// <summary>
        /// btnClose 버튼 Click 이벤트 - 폼 종료.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UcChartDetail1_SpcChartDetailbtnCloseClickEventHandler(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// 닫기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// Form Road시 초기화
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpcStatusDetailChartPopup_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            string subgroupID = "";
            string subgroupName = "";
            try
            {

                subgroupID = ucChartDetail1.GetControlSpecSigmaResultSubGroup();
                subgroupName = ucChartDetail1.GetControlSpecSigmaResultsubGroupName();

                if (subgroupName != null && subgroupName != "")
                {
                    this.Text = string.Format("{0} - {1}", this.Text, subgroupName);
                    this.Tag = subgroupID;
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            CloseWait();
        }
        private void SpcStatusDetailChartPopup_Resize(object sender, EventArgs e)
        {
            this.FormResize();
        }
        #endregion

        #region Private Function

        /// <summary>
        /// Form Resize
        /// </summary>
        private void FormResize()
        {
            try
            {
                int mainPanel1position = 390;
                int rate = 2;
                //this.ucChartDetail1.splMain.SplitterPosition = position;
                this.ucChartDetail1.splMain.SplitterPosition = this.Height - (this.Height / rate);
                this.ucChartDetail1.splMainPanel1.SplitterPosition = mainPanel1position;
                //this.ucChartDetail1.splMainPanel1.SplitterPosition = this.Height - (this.Height / rate);
            }
            catch (Exception)
            {
                //throw;
            }
        }

        public void ShowWait()
        {
            DialogManager.ShowWaitArea(this.ucChartDetail1);
        }
        public void CloseWait()
        {
            DialogManager.CloseWaitArea(this.ucChartDetail1);
        }
        #endregion


    }
}
