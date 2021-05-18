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
    /// 프 로 그 램 명  : 품질 관리 > Defect Map >  작업조건 별 수율 관리
    /// 업  무  설  명  : 선택된 품목에 따른 작업조건 별 수율을 확인 한다.
    ///                  수동입력을 등록된 Data만 조회된다.
    /// 생    성    자  : 전우성
    /// 생    성    일  : 2019-10-28
    /// 수  정  이  력  : 2020-01-07 작업조건 조회 조건 변경
    /// 
    /// 
    /// </summary>
    public partial class DefectRateByRecipe : SmartConditionManualBaseForm
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

        public DefectRateByRecipe()
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

                if (await SqlExecuter.QueryAsync("GetDefectRateByRecipeList", "10001",
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

                    ((ucRateMain)pnlContent.Controls[0]).SetData(RateGroupType.RECIPEID, DefectMapHelper.GetRepairAnalysisByEnterpriseid(dt), _groupList, type);
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

            #region 검사 공정

            Conditions.AddComboBox("P_INSPECTIONPROCESS",
                                   new SqlQuery("GetProcessByProductdef", "10001",
                                                DefectMapHelper.AddLanguageTypeToConditions(new Dictionary<string, object>
                                                {
                                                    { "P_ENTERPRISEID", UserInfo.Current.Enterprise },
                                                    { "P_PLANTID", UserInfo.Current.Plant }
                                                })))
                      .SetLabel("INSPECTIONPROCESS")
                      .SetRelationIds("P_PRODUCTDEFID", "P_PRODUCTDEFVERSION", "P_INSPECTIONTYPE")
                      .SetEmptyItem()
                      .SetResultCount(0)
                      .SetPosition(3.1);

            #endregion

            #region 작업 공정

            Conditions.AddComboBox("P_WORKPROCESSSEGMENT",
                                   new SqlQuery("GetProcessSegmentByProcess", "10001",
                                                DefectMapHelper.AddLanguageTypeToConditions(new Dictionary<string, object>())))
                      .SetLabel("WORKPROCESSSEGMENT")
                      .SetRelationIds("P_PRODUCTDEFID", "P_PRODUCTDEFVERSION", "P_INSPECTIONPROCESS")
                      .SetPosition(3.2);

            #endregion

            #region 작업조건(Recipe) Group 

            Conditions.AddSelectPopup("P_RECIPEID", new popupRecipeGroupList(), "ITEM", "ITEM")
                      .SetLabel("WORKINGCONDITION")
                      .SetValidationIsRequired()
                      .SetPosition(4.1)
                      .SetPopupCustomParameter((popup, dataRow) =>
                      {
                          Dictionary<string, object> value = new Dictionary<string, object>()
                          {
                            {"P_PRODUCTDEFID", Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").Text },
                            {"P_PRODUCTDEFVERSION", String.Join(",", Conditions.GetControl<SmartCheckedComboBox>("P_PRODUCTDEFVERSION").GetValuesByList()) },
                            { "P_WORKPROCESSSEGMENT", Conditions.GetControl<SmartComboBox>("P_WORKPROCESSSEGMENT").GetDataValue() },
                            { "P_RECIPETYPE", Conditions.GetControl<SmartComboBox>("P_RECIPETYPE").GetDataValue() }
                          };

                          if (SqlExecuter.Query("GetRecipeList", "10001", DefectMapHelper.AddLanguageTypeToConditions(new Dictionary<string, object>
                                                                          {
                                                                                { "P_PRODUCTDEFID", Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").Text },
                                                                                { "P_PRODUCTDEFVERSION", String.Join(",", Conditions.GetControl<SmartCheckedComboBox>("P_PRODUCTDEFVERSION").GetValuesByList()) },
                                                                                { "P_WORKPROCESSSEGMENT", Conditions.GetControl<SmartComboBox>("P_WORKPROCESSSEGMENT").GetDataValue() },
                                                                                { "P_RECIPETYPE", Conditions.GetControl<SmartComboBox>("P_RECIPETYPE").GetDataValue() }
                                                                          })) is DataTable dt)
                          {
                              (popup as popupRecipeGroupList).SetData("WORKINGCONDITION", dt);
                              (popup as popupRecipeGroupList).ResultGroupDataEvent += (returnDt) => { _groupList = returnDt; };
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

            _selectedRow = dr;
        }

        #endregion
    }
}
