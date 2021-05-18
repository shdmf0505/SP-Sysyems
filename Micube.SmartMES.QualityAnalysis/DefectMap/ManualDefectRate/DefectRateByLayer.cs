#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;

using Micube.SmartMES.Commons.Controls;

using System;
using System.Data;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;

#endregion

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질 관리 > Defect Map >  Layer 별 수율 관리
    /// 업  무  설  명  : 선택된 품목에 따른 Lot/Layer 별 수율을 확인 한다.
    ///                  수동입력을 등록된 Data만 조회된다.
    /// 생    성    자  : 전우성
    /// 생    성    일  : 2019-05-27
    /// 수  정  이  력  : 
    /// 
    /// </summary>
    public partial class DefectRateByLayer : SmartConditionManualBaseForm
    {
        #region Local Variables

        /// <summary>
        /// Layer 조회조건 Group List (1, 2, 3) 정보 DataTable
        /// </summary>
        private DataTable _groupList;

        /// <summary>
        /// 조회 조건 품목에서 찾은 정보 Row
        /// </summary>
        private DataRow _selectedRow;

        #endregion

        #region 생성자

        public DefectRateByLayer()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        protected override void InitializeContent()
        {
            base.InitializeContent();

            pnlContent.Controls.Add(new ucRateMain() { Dock = DockStyle.Fill });
        }

        #endregion

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

                if (DefectMapHelper.IsNull(_selectedRow))
                {
                    ShowMessage("NoLotProductdefCondition");
                    return;
                }

                if (await SqlExecuter.QueryAsync("GetDefectRateByLayerList", "10001",
                                                 DefectMapHelper.AddLanguageTypeToConditions(Conditions.GetValues())) is DataTable dt)
                {
                    if (dt.Rows.Count.Equals(0))
                    {
                        ShowMessage("NoSelectData");
                        return;
                    }

                    EquipmentType type = Conditions.GetValues()["P_INSPECTIONTYPE"].Equals("AOIInspection") 
                                            ? EquipmentType.EQUIPMENTTYPE_AOI 
                                            : EquipmentType.EQUIPMENTTYPE_HOLE;

                    ((ucRateMain)pnlContent.Controls[0]).SetData(RateGroupType.LAYERID, DefectMapHelper.GetRepairAnalysisByEnterpriseid(dt), _groupList, type);
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

        protected override void InitializeCondition()
        {
            #region Lot

            Conditions.AddSelectPopup("P_LOTID", new popupLotListByPeriod(), "P_LOTID", "P_LOTID")
                      .SetLabel("LOTID")
                      .SetPosition(1.1)
                      .SetPopupCustomParameter((popup, dataRow) =>
                      {
                          (popup as popupLotListByPeriod).SetParams(
                              Conditions.GetControl<SmartPeriodEdit>("P_PERIOD").datePeriodFr.Text,
                              Conditions.GetControl<SmartPeriodEdit>("P_PERIOD").datePeriodTo.Text,
                              Conditions.GetControl<SmartSelectPopupEdit>("P_LOTID").Text
                          );

                          (popup as popupLotListByPeriod).SelectedRowEvent += (dr) => SetCondition(dr);
                      });

            #endregion

            #region 품목 

            Conditions.AddSelectPopup("P_PRODUCTDEFID", new popupProductListByPeriod(), "P_PRODUCTDEFID", "P_PRODUCTDEFID")
                      .SetLabel("PRODUCTDEFID")
                      .SetPosition(1.2)
                      .SetValidationKeyColumn()
                      .SetPopupCustomParameter((popup, dataRow) =>
                      {
                          (popup as popupProductListByPeriod).SetParams(
                              Conditions.GetControl<SmartPeriodEdit>("P_PERIOD").datePeriodFr.Text,
                              Conditions.GetControl<SmartPeriodEdit>("P_PERIOD").datePeriodTo.Text,
                              Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").Text
                          );

                          (popup as popupProductListByPeriod).SelectedRowEvent += (dr) => SetCondition(dr);
                      });

            #endregion

            #region 품목명

            Conditions.GetCondition<ConditionItemTextBox>("P_PRODUCTNAME").SetIsReadOnly();

            #endregion

            #region 품목 Version

            Conditions.AddComboBox("P_PRODUCTDEFVERSION",
                                    new SqlQuery("GetProductDefVersionByRate", "10001"))
                      .SetLabel("PRODUCTDEFVERSION")
                      .SetRelationIds("P_PRODUCTDEFID")
                      .SetEmptyItem()
                      .SetResultCount(0)
                      .SetPosition(2.1);

            #endregion

            #region Layer

            //팝업 컬럼 설정 : param, customControler, displayField, ValueFiled
            Conditions.AddSelectPopup("P_LAYERID", new popupGroupList(), "ITEM", "ITEM")
                      .SetLabel("LAYERNO")
                      .SetValidationIsRequired()
                      .SetPopupCustomParameter((popup, dataRow) =>
                      {
                          if (!Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").GetValue().Equals(""))
                          {
                              (popup as popupGroupList).SetData("LAYERLIST", SetGroupList(
                                                                SqlExecuter.Query("GetLayerByProductdef",
                                                                                  "10001",
                                                                                  new Dictionary<string, object> {
                                                                                { "P_PRODUCTDEFID", Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").GetValue() },
                                                                                { "P_PRODUCTDEFVERSION", string.Join(",", Conditions.GetControl<SmartCheckedComboBox>("P_PRODUCTDEFVERSION").GetValuesByList()) },
                                                                                { "P_ENTERPRISEID", UserInfo.Current.Enterprise }})));

                              (popup as popupGroupList).ResultGroupDataEvent += (dt) => { _groupList = dt; };
                          }
                      });

            #endregion
        }

        #endregion

        #region Private Function

        /// <summary>
        /// 조회 조건 설정
        /// </summary>
        /// <param name="dr"></param>
        private void SetCondition(DataRow dr)
        {
            Conditions.GetControl<SmartSelectPopupEdit>("P_LOTID").SetValue(DefectMapHelper.StringByDataRowObejct(dr, "P_LOTID"));
            Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").SetValue(DefectMapHelper.StringByDataRowObejct(dr, "P_PRODUCTDEFID"));
            Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").Text = DefectMapHelper.StringByDataRowObejct(dr, "P_PRODUCTDEFNAME");

            Conditions.GetControl<SmartPeriodEdit>("P_PERIOD").datePeriodFr.Text = DefectMapHelper.StringByDataRowObejct(dr, "P_PERIOD_PERIODFR");
            Conditions.GetControl<SmartPeriodEdit>("P_PERIOD").datePeriodTo.Text = DefectMapHelper.StringByDataRowObejct(dr, "P_PERIOD_PERIODTO");

            Conditions.GetControl<SmartSelectPopupEdit>("P_LAYERID").EditValue = string.Empty;

            _selectedRow = dr;
        }

        /// <summary>
        /// 품목 기준정보 받은 Layer(max)로 리스트 만들기
        /// 인터의 경우에는 CS와 SS를 사용
        /// </summary>
        /// <param name="layerDt"></param>
        /// <returns></returns>
        private DataTable SetGroupList(DataTable layerDt)
        {
            if (DefectMapHelper.IsNull(layerDt) || layerDt.Rows.Count.Equals(0))
            {
                return null;
            }

            DataTable dt = new DataTable();
            dt.Columns.Add("CODEID", typeof(string));
            dt.Columns.Add("CODENAME", typeof(string));

            string layer = DefectMapHelper.StringByDataRowObejct(layerDt.Rows[0], "LAYER");

            if (!int.TryParse(layer, out int rowNo))
            {
                if (layer == "DS")
                {
                    dt.Rows.Add("CS", "CS");
                    dt.Rows.Add("SS", "SS");
                }
                else
                {
                    return null;
                }
            }
            else
            {
                for (int i = 0; i < rowNo; i++)
                {
                    dt.Rows.Add(string.Concat(i + 1, "L"), string.Concat(i + 1, "L"));
                    //dt.Rows.Add(i > 8 ? string.Concat(i + 1) : string.Concat("0", i + 1), string.Concat(i + 1, "L"));
                }
            }

            return dt;
        }

        #endregion
    }
}
