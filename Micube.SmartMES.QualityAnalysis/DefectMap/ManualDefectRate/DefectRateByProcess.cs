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
    /// 프 로 그 램 명  : 품질 관리 > Defect Map >  공정/설비 별 수율 관리
    /// 업  무  설  명  : 선택된 품목에 따른 공정/설비 별 수율을 확인 한다.
    ///                  수동입력을 등록된 Data만 조회된다.
    /// 생    성    자  : 전우성
    /// 생    성    일  : 2019-10-24
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class DefectRateByProcess : SmartConditionManualBaseForm
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

        public DefectRateByProcess()
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

                if (await SqlExecuter.QueryAsync("GetDefectRateByPcoessList", "10001",
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

                    ((ucRateMain)pnlContent.Controls[0]).SetData(RateGroupType.EQUIPMENTID, DefectMapHelper.GetRepairAnalysisByEnterpriseid(dt), _groupList, type);
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
                      .SetResultCount(0);

            #endregion

            #region 작업 공정

            Conditions.AddComboBox("P_WORKPROCESSSEGMENT",
                                   new SqlQuery("GetProcessSegmentByProcess", "10001",
                                                DefectMapHelper.AddLanguageTypeToConditions(new Dictionary<string, object>())))
                      .SetLabel("WORKPROCESSSEGMENT")
                      .SetRelationIds("P_PRODUCTDEFID", "P_PRODUCTDEFVERSION", "P_INSPECTIONPROCESS");

            #endregion

            #region 자원

            Conditions.AddComboBox("P_RESOURCE",
                                   new SqlQuery("GetResourceIdByProcess", "10001",
                                                DefectMapHelper.AddLanguageTypeToConditions(new Dictionary<string, object>
                                                {
                                                    { "P_ENTERPRISEID", UserInfo.Current.Enterprise }
                                                })))
                      .SetLabel("RESOURCE")
                      .SetEmptyItem()
                      .SetRelationIds("P_PRODUCTDEFID", "P_PRODUCTDEFVERSION", "P_WORKPROCESSSEGMENT");

            #endregion

            #region 작업 설비 Group 

            //팝업 컬럼 설정 : param, customControler, displayField, ValueFiled
            Conditions.AddSelectPopup("P_WORKINGEQUIPMENT", new popupGroupList(), "ITEM", "ITEM")
                      .SetLabel("WORKINGEQUIPMENT")
                      .SetValidationIsRequired()
                      .SetPopupCustomParameter((popup, dataRow) =>
                      {
                            (popup as popupGroupList).SetData("WORKINGEQUIPLIST", 
                                                            SqlExecuter.Query("GetWorkingEquipmentByProcess","10001",
                                                                              DefectMapHelper.AddLanguageTypeToConditions(new Dictionary<string, object>
                                                                              {
                                                                                  { "P_ENTERPRISEID", UserInfo.Current.Enterprise },
                                                                                  { "P_RESOURCE", Conditions.GetControl<SmartComboBox>("P_RESOURCE").GetDataValue() }
                                                                              })));

                            (popup as popupGroupList).ResultGroupDataEvent += (dt) => { _groupList = dt; };
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

            Conditions.GetControl<SmartSelectPopupEdit>("P_WORKINGEQUIPMENT").SetValue("");

            _selectedRow = dr;
        }

        #endregion
    }
}
