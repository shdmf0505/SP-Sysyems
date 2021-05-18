#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;

#endregion using

namespace Micube.SmartMES.EquipmentManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 설비관리 > Spare Part 관리 > S/P 재고조회
    /// 업  무  설  명  : Sparepart 부품의 재고를 조회한다.
    /// 생    성    자  : 김용조
    /// 생    성    일  : 2019-07-10
    /// 수  정  이  력  :
    /// 2021.02.18 전우성 화면 최적화 및 정리. 대체품 모델 추가
    ///
    /// </summary>
    public partial class SparePartStockBrowse : SmartConditionManualBaseForm
    {
        #region 생성자

        public SparePartStockBrowse()
        {
            InitializeComponent();
        }

        #endregion 생성자

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 화면의 컨텐츠 영역을 초기화한다.
        /// </summary>
        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeGrid();
            InitializeEvent();
        }

        /// <summary>
        /// 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            #region S/P 재고조회

            grdSPStock.GridButtonItem = GridButtonItem.Export;

            grdSPStock.View.AddTextBoxColumn("FACTORYID", 150).SetIsHidden();
            grdSPStock.View.AddTextBoxColumn("SPAREPARTVERSION", 150).SetIsHidden();

            grdSPStock.View.AddTextBoxColumn("FACTORYNAME", 60);
            grdSPStock.View.AddTextBoxColumn("SPAREPARTID", 100);
            grdSPStock.View.AddTextBoxColumn("SPAREPARTNAME", 150);
            grdSPStock.View.AddTextBoxColumn("MODELNAME", 150);
            grdSPStock.View.AddTextBoxColumn("ALTERNATEITEMID", 100);
            grdSPStock.View.AddTextBoxColumn("ALTERNATEITEMNAME", 150);
            grdSPStock.View.AddTextBoxColumn("ALTERNATEITEMMODEL", 200);

            grdSPStock.View.AddTextBoxColumn("MAKER", 100);
            grdSPStock.View.AddTextBoxColumn("SPEC", 150);
            grdSPStock.View.AddTextBoxColumn("SAFETYSTOCK", 80);
            grdSPStock.View.AddTextBoxColumn("QTY", 80);
            grdSPStock.View.AddSpinEditColumn("PRICE", 120);

            grdSPStock.View.PopulateColumns();

            grdSPStock.View.SetIsReadOnly();
            grdSPStock.ShowStatusBar = true;

            #endregion S/P 재고조회

            #region 사용내역

            grdInOutHistory.GridButtonItem = GridButtonItem.Export;

            grdInOutHistory.View.AddTextBoxColumn("INOUTTYPEID", 80).SetIsHidden();
            grdInOutHistory.View.AddTextBoxColumn("FACTORYID", 250).SetIsHidden();
            grdInOutHistory.View.AddTextBoxColumn("RELATIONFACTORYID", 80).SetIsHidden();
            grdInOutHistory.View.AddTextBoxColumn("EQUIPMENTID", 80).SetIsHidden();

            grdInOutHistory.View.AddTextBoxColumn("SEQUENCE", 80);
            grdInOutHistory.View.AddTextBoxColumn("TXNDATE", 160).SetDisplayFormat("yyyy-MM-dd HH:mm:ss");
            grdInOutHistory.View.AddTextBoxColumn("INOUTTYPE", 100).SetTextAlignment(TextAlignment.Center);
            grdInOutHistory.View.AddTextBoxColumn("FACTORYNAME", 180);
            grdInOutHistory.View.AddTextBoxColumn("INPUTQTY", 80);
            grdInOutHistory.View.AddTextBoxColumn("OUTPUTQTY", 80);
            grdInOutHistory.View.AddTextBoxColumn("RELATIONFACTORYNAME", 80);
            grdInOutHistory.View.AddTextBoxColumn("WORKORDERID", 150);
            grdInOutHistory.View.AddTextBoxColumn("EQUIPMENTNAME", 250);

            grdInOutHistory.View.PopulateColumns();

            grdInOutHistory.View.SetIsReadOnly();
            grdInOutHistory.ShowStatusBar = true;

            #endregion 사용내역
        }

        #endregion 컨텐츠 영역 초기화

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            grdSPStock.View.FocusedRowChanged += (s, e) =>
            {
                if (grdSPStock.View.FocusedRowHandle > -1)
                {
                    DataRow currentRow = grdSPStock.View.GetFocusedDataRow();

                    Dictionary<string, object> values = Conditions.GetValues();
                    values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                    values.Add("SPAREPARTID", currentRow.GetString("SPAREPARTID"));
                    values = Commons.CommonFunction.ConvertParameter(values);

                    grdInOutHistory.DataSource = SqlExecuter.Query("GetSparePartStockInAndOutReportByEqp", "10001", values);
                }

                if (e.FocusedRowHandle >= 0)
                {
                    DataRow currentRow = grdSPStock.View.GetDataRow(e.FocusedRowHandle);

                    Dictionary<string, object> values = new Dictionary<string, object>();
                    values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                    values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                    values.Add("ITEMID", currentRow.GetString("SPAREPARTID"));
                    values.Add("ITEMVERSION", currentRow.GetString("SPAREPARTVERSION"));
                    values = Commons.CommonFunction.ConvertParameter(values);

                    DataTable sparePartSearchResult = SqlExecuter.Query("GetSparePartImageByEqp", "10001", values);

                    if (sparePartSearchResult.Rows.Count > 0)
                    {
                        if (sparePartSearchResult.Rows[0]["IMAGE"] != null && !Format.GetString(sparePartSearchResult.Rows[0]["IMAGE"]).Equals(string.Empty))
                        {
                            speImage.Image = (Bitmap)new ImageConverter().ConvertFrom(sparePartSearchResult.Rows[0]["IMAGE"]);
                        }
                        else
                        {
                            speImage.Image = null;
                        }
                    }
                    else
                    {
                        speImage.Image = null;
                    }
                }
                else
                {
                    speImage.Image = null;
                }
            };

            grdSPStock.View.RowCellStyle += (s, e) =>
            {
                if (e.Column.FieldName.Equals("SAFETYSTOCK") || e.Column.FieldName.Equals("QTY"))
                {
                    if (grdSPStock.View.GetDataRow(e.RowHandle).GetInteger("SAFETYSTOCK") > grdSPStock.View.GetDataRow(e.RowHandle).GetInteger("QTY"))
                    {
                        e.Appearance.ForeColor = Color.Red;
                    }
                }
            };
        }

        #endregion Event

        #region 검색

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            try
            {
                DialogManager.ShowWaitArea(this.pnlContent);

                await base.OnSearchAsync();

                grdSPStock.View.ClearDatas();
                grdInOutHistory.View.ClearDatas();

                var values = Conditions.GetValues();
                values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                values = Commons.CommonFunction.ConvertParameter(values);

                if (SqlExecuter.Query("GetSparePartStockStatusReportByEqp", "10001", values) is DataTable dt)
                {
                    if (dt.Rows.Count.Equals(0))
                    {
                        ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                    }

                    grdSPStock.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                throw MessageException.Create(Format.GetString(ex));
            }
            finally
            {
                DialogManager.CloseWaitArea(this.pnlContent);
            }
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            #region 적정재고미만여부

            Conditions.AddComboBox("STOCKYN", new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CODECLASSID=StockCondition"), "CODENAME", "CODEID")
                      .SetLabel("STOCKYN")
                      .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                      .SetPosition(3)
                      .SetEmptyItem("", "", true);

            #endregion 적정재고미만여부

            #region 공장

            Conditions.AddComboBox("FACTORYID", new SqlQuery("GetFactoryListByEqp", "10001"), "FACTORYNAME", "FACTORYID")
                      .SetLabel("FACTORY")
                      .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                      .SetPosition(0.2)
                      .SetRelationIds("P_PLANTID") //사이트에 설정된 값에 따라 공장을 바인딩
                      .SetValidationIsRequired();

            #endregion 공장

            #region 중분류

            Conditions.AddComboBox("DURABLECLASSID", new SqlQuery("GetClassCodeListByEqp", "10001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "DURABLECLASSID", "9" } }), "DURABLECLASSNAME", "DURABLECLASSID")
                      .SetLabel("중분류")
                      .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                      .SetPosition(0.3)
                      .SetEmptyItem("전체조회", "", true);

            #endregion 중분류

            #region 소분류

            Conditions.AddComboBox("BOTTOMDURABLECLASSID", new SqlQuery("GetClassCodeListByEqp", "10001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType } }), "DURABLECLASSNAME", "DURABLECLASSID")
                      .SetLabel("소분류")
                      .SetEmptyItem("전체조회", "", true)
                      .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                      .SetPosition(0.4)
                      .SetRelationIds("DURABLECLASSID");

            #endregion 소분류

            #region 품목

            Conditions.AddTextBox("P_MODELNAME").SetLabel("MODELNAME").SetPosition(4.2);

            #endregion 품목
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            SmartComboBox cboFactoryid = Conditions.GetControl<SmartComboBox>("FACTORYID");
            DataTable dtfactory = cboFactoryid.DataSource as DataTable;
            if (dtfactory.Rows.Count > 0)
            {
                DataRow dr = dtfactory.Rows[0];
                cboFactoryid.EditValue = dr["FACTORYID"];
            }
        }

        #endregion 검색
    }
}