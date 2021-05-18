#region using

using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
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

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 품질비용분석 > Q-Cost 현황 > 생산입고 매출금액 등록 Popup
    /// 업  무  설  명  : Q-Cost 비용의 생산입고금액과 매출금액을 입력할 수 있는 팝업이다.
    /// 생    성    자  : 유태근
    /// 생    성    일  : 2019-09-06
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ProductionPriceRegistrationPopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region Local Variables

        /// <summary>
        /// 팝업진입전 그리드의 Row
        /// </summary>
        public DataRow CurrentDataRow { get; set; }

        /// <summary>
        /// 조회조건의 기준년월
        /// </summary>
        public string _standardMonth;

        /// <summary>
        /// 조회조건의 SiteID
        /// </summary>
        public string _plantId;

        /// <summary>
        /// 생산입고금액
        /// </summary>
        public double _productionPrice;

        /// <summary>
        /// 매출금액 
        /// </summary>
        public double _salesPrice;

        #endregion

        #region 생성자

        /// <summary>
        /// 생성자
        /// </summary>
        public ProductionPriceRegistrationPopup()
        {
            InitializeComponent();

            InitializeEvent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeDefectCodeList()
        {

        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            this.Load += ChemicalissuePopup_Load;

            btnClose.Click += BtnClose_Click;
            btnSave.Click += BtnSave_Click;
        }
        
        /// <summary>
        /// 저장
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (this.ShowMessage(MessageBoxButtons.YesNo, "InfoPopupSave") == DialogResult.Yes)
            {
                MessageWorker worker = new MessageWorker("SaveProductionSalesPriceRegistartion");
                worker.SetBody(new MessageBody()
                {
                    { "enterpriseId", UserInfo.Current.Enterprise }, // 유저의 회사ID
                    { "plantId", _plantId }, // 조회조건의 SiteID
                    { "standardYearMonth", _standardMonth }, // 조회조건의 기준년월
                    { "productionPrice", spinProductionPrice.EditValue }, // 생산입고금액
                    { "salesPrice", spinSalesPrice.EditValue }, // 매출금액
                });

                worker.Execute();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
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
        private void ChemicalissuePopup_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            SettingData();

            InitializeDefectCodeList();           
        }

        #endregion
 
        #region Private Function

        /// <summary>
        /// 조회조건의 기준년월, 생산입고금액, 매출금액을 세팅
        /// </summary>
        private void SettingData()
        {
            txtStandardMonth.EditValue = _standardMonth;

            spinProductionPrice.EditValue = _productionPrice;
            spinSalesPrice.EditValue = _salesPrice;
        }

        #endregion
    }
}
