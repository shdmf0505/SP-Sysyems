#region using

using DevExpress.XtraEditors.Repository;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Validations;
using System;
using Micube.SmartMES.Commons.Controls;
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
    /// 프 로 그 램 명  : 품질관리 > 품질 비용분석 > Q-Cost 현황
    /// 업  무  설  명  : 품질비용을 조회하고 생산/매출금액을 등록/수정한다.
    /// 생    성    자  : 유태근
    /// 생    성    일  : 2019-09-05
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class QCostStatus : SmartConditionManualBaseForm
    {
        #region Local Variables

        #endregion

        #region 생성자

        public QCostStatus()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 화면의 컨텐츠 영역을 초기화한다.
        /// </summary>
        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeEvent();
            InitializeGrid();
        }

        /// <summary>        
        /// 재공불량 금액현황 그리드
        /// </summary>
        private void InitializeGrid()
        {
            grdQCostStatus.GridButtonItem = GridButtonItem.Export;
            grdQCostStatus.View.SetIsReadOnly();

            var price = grdQCostStatus.View.AddGroupColumn("WIPPRICE"); // 금액

            price.AddTextBoxColumn("QCOSTTYPENAME", 220)
                .SetLabel("CLASS");
            price.AddTextBoxColumn("TOPCATEGORYNAME", 220)
                .SetLabel("LARGECLASS");
            price.AddTextBoxColumn("SMALLCATEGORYNAME", 220)
                .SetLabel("SMALLCLASS");
            price.AddSpinEditColumn("PRICEBEFOREMONTHAMOUNT", 120)
                .SetLabel("BEFOREMONTH");
            price.AddSpinEditColumn("PRICENOWMONTHAMOUNT", 120)
                .SetLabel("NOWMONTH");
            price.AddSpinEditColumn("PRICEPLUSMINUS", 120)
                .SetLabel("PLUSMINUS");

            var costShare = grdQCostStatus.View.AddGroupColumn("COSTSHARE"); // 비용대비 점유율

            costShare.AddTextBoxColumn("COSTBEFOREMONTHAMOUNT", 120)
                .SetLabel("BEFOREMONTH");
            costShare.AddTextBoxColumn("COSTNOWMONTHAMOUNT", 120)
                .SetLabel("NOWMONTH");
            costShare.AddTextBoxColumn("COSTPLUSMINUS", 120)
                .SetLabel("PLUSMINUS");

            var totalQualityCostShare = grdQCostStatus.View.AddGroupColumn("TOTALQUALITYCOSTSHARE"); // 총 품질비용 대비 점유율

            totalQualityCostShare.AddTextBoxColumn("QUALITYBEFOREMONTHAMOUNT", 120)
                .SetLabel("BEFOREMONTH");
            totalQualityCostShare.AddTextBoxColumn("QUALITYNOWMONTHAMOUNT", 120)
                .SetLabel("NOWMONTH");
            totalQualityCostShare.AddTextBoxColumn("QUALITYPLUSMINUS", 120)
                .SetLabel("PLUSMINUS");

            var productionCostShare = grdQCostStatus.View.AddGroupColumn("PRODUCTIONCOSTSHARE"); // 생산입고 금액대비 점유율

            productionCostShare.AddTextBoxColumn("PRODUCTBEFOREMONTHAMOUNT", 120)
                .SetLabel("BEFOREMONTH");
            productionCostShare.AddTextBoxColumn("PRODUCTNOWMONTHAMOUNT", 120)
                .SetLabel("NOWMONTH");
            productionCostShare.AddTextBoxColumn("PRODUCTPLUSMINUS", 120)
                .SetLabel("PLUSMINUS");

            var salesPriceShare = grdQCostStatus.View.AddGroupColumn("SALESPRICESHARE"); // 매출금액대비 점유율

            salesPriceShare.AddTextBoxColumn("SALESBEFOREMONTHAMOUNT", 120)
                .SetLabel("BEFOREMONTH");
            salesPriceShare.AddTextBoxColumn("SALESNOWMONTHAMOUNT", 120)
                .SetLabel("NOWMONTH");
            salesPriceShare.AddTextBoxColumn("SALESPLUSMINUS", 120)
                .SetLabel("PLUSMINUS");

            grdQCostStatus.View.PopulateColumns();

            RepositoryItemSpinEdit edit = new RepositoryItemSpinEdit();
            edit.Mask.EditMask = "^[+-]?[d,]*(.?d*)$";
            edit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;

            grdQCostStatus.View.OptionsNavigation.AutoMoveRowFocus = false;
        }

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            //btnPrice.Click += BtnPrice_Click;
        }

        /// <summary>
        /// 생산입고 매출금액 버튼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPrice_Click(object sender, EventArgs e)
        {
            ProductionPriceRegistrationPopup popup = new ProductionPriceRegistrationPopup();

            popup.Owner = this;
            popup._plantId = this.Conditions.GetValue("p_plantId").ToString(); // 조회조건 Site 세팅
            popup._standardMonth = this.Conditions.GetValue("p_standardMonth").ToString().Substring(0, 7); // 조회조건 기준년월 세팅
            popup._productionPrice = Convert.ToDouble(spinProductionPrice.EditValue);
            popup._salesPrice = Convert.ToDouble(spinSalesPrice.EditValue);

            if (popup.ShowDialog() == DialogResult.OK)
            {
                grdQCostStatus.BeginInvoke(new MethodInvoker(() =>
                {
                    OnSearchAsync();
                }));
            }
        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            //base.OnToolbarSaveClick();

            //DataTable changed = grdWorkDefectPriceStatus.GetChangedRows();

            //ExecuteRule("SaveInspectionGrade", changed);
        }

        /// <summary>
        /// 툴바버튼 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            SmartButton btn = sender as SmartButton;

            if (btn.Name.ToString().Equals("CostCalculation"))
            {
                BtnPrice_Click(null, null);
            }
        }

        #endregion

        #region 검색

        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            var values = Conditions.GetValues();
            values.Add("p_languageType", UserInfo.Current.LanguageType);

            DataTable dt = await SqlExecuter.QueryAsync("GetQCostStatus", "10001", values);

            if (dt.Rows.Count == 0)
            {
                // 조회할 데이터가 없습니다.
                this.ShowMessage("NoSelectData");

                spinProductionPrice.EditValue = null;
                spinSalesPrice.EditValue = null;
                grdQCostStatus.DataSource = null;
                return;
            }

            spinProductionPrice.EditValue = Convert.ToDouble(dt.Rows[0]["PRODUCTAMOUNT"]);
            spinSalesPrice.EditValue = Convert.ToDouble(dt.Rows[0]["SALESAMOUNT"]);
            grdQCostStatus.DataSource = dt;
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            Conditions.AddComboBox("p_qualityPriceType", new SqlQuery("GetQualityPriceType", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "QCOSTCODENAME", "QCOSTCODE")
                .SetLabel("QUALITYPRICETYPE")
                .SetRelationIds("p_plantId")
                .SetEmptyItem()
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                .SetPosition(1.1); // 품질비용구분
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            SmartDateEdit date = Conditions.GetControl<SmartDateEdit>("p_standardMonth");
            date.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearView;
            date.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            date.Properties.Mask.EditMask = "yyyy-MM";
            date.EditValue = DateTime.Now;
        }

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            //grdWorkDefectPriceStatus.View.CheckValidation();

            //DataTable changed = grdWorkDefectPriceStatus.GetChangedRows();

            //if (changed.Rows.Count == 0)
            //{
            //    // 저장할 데이터가 존재하지 않습니다.
            //    throw MessageException.Create("NoSaveData");
            //}
        }

        #endregion

        #region Private Function

        #endregion
    }
}
