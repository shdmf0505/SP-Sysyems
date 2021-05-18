#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons.Controls;

using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Collections.Generic;

#endregion

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 출하검사 > 3D 측정값 조회
    /// 업  무  설  명  : 3D 설비 인퍼페이스 Data의 조회화면
    /// 생    성    자  : 전우성
    /// 생    성    일  : 2020-03-03
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ThreeDimensionMeasureValueSearch : SmartConditionManualBaseForm
    {
        #region Local Variables

        #endregion

        #region 생성자

        public ThreeDimensionMeasureValueSearch()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeEvent();
            InitializeGrid();
        }

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            grdMain.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            grdMain.View.AddTextBoxColumn("PROCESSSEGMENTID", 10).SetIsHidden();
            grdMain.View.AddTextBoxColumn("PLANTID", 10).SetIsHidden();
            grdMain.View.AddTextBoxColumn("AREAID", 10).SetIsHidden();
            grdMain.View.AddTextBoxColumn("EQUIPMENTID", 10).SetIsHidden();
            grdMain.View.AddTextBoxColumn("CUSTOMERID", 10).SetIsHidden();
            grdMain.View.AddTextBoxColumn("SPECRANGE", 10).SetIsHidden();

            grdMain.View.AddTextBoxColumn("MEASUREDATETIME", 150).SetTextAlignment(TextAlignment.Left);
            grdMain.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 100).SetTextAlignment(TextAlignment.Left);
            grdMain.View.AddTextBoxColumn("AREANAME", 150).SetTextAlignment(TextAlignment.Left);
            grdMain.View.AddTextBoxColumn("FACTORYNAME", 150).SetTextAlignment(TextAlignment.Left);
            grdMain.View.AddTextBoxColumn("EQUIPMENTUNIT", 100).SetTextAlignment(TextAlignment.Left);
            grdMain.View.AddTextBoxColumn("CUSTOMERNAME", 100).SetLabel("COMPANYCLIENT").SetTextAlignment(TextAlignment.Left);
            grdMain.View.AddTextBoxColumn("PRODUCTDEFID", 90).SetTextAlignment(TextAlignment.Left);
            grdMain.View.AddTextBoxColumn("PRODUCTDEFNAME", 180).SetTextAlignment(TextAlignment.Left);
            grdMain.View.AddTextBoxColumn("PRODUCTDEFVERSION", 70).SetTextAlignment(TextAlignment.Center);
            grdMain.View.AddTextBoxColumn("LOTID", 150).SetTextAlignment(TextAlignment.Left);
            grdMain.View.AddTextBoxColumn("DAITEMID", 120).SetLabel("MEASUREITEM").SetTextAlignment(TextAlignment.Left);
            grdMain.View.AddTextBoxColumn("DEGREE", 60).SetTextAlignment(TextAlignment.Left);
            grdMain.View.AddTextBoxColumn("INSPECTIONRESULT", 60).SetTextAlignment(TextAlignment.Left);

            grdMain.View.PopulateColumns();
            grdMain.View.BestFitColumns();
            grdMain.View.SetIsReadOnly();

            grdMain.ShowStatusBar = true;
            grdMain.View.OptionsView.AllowCellMerge = true;
            grdMain.GridButtonItem = GridButtonItem.Export;
        }

        #endregion

        #region Event

        /// <summary>
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            //! Row 더블 클릭이벤트
            grdMain.View.DoubleClick += (s, e) =>
            {
                if (grdMain.View.GetFocusedDataRow() is DataRow dr)
                {
                    popupThreeDimensionMeasure popup = new popupThreeDimensionMeasure()
                    {
                        StartPosition = System.Windows.Forms.FormStartPosition.CenterParent,
                        CurrentDataRow = dr
                    };

                    popup.ShowDialog();
                }
            };

            //! 결과가 NG면 색상변경 이벤트
            grdMain.View.RowStyle += (s, e) => 
                e.Appearance.ForeColor = Format.GetString(grdMain.View.GetRowCellValue(e.RowHandle, "INSPECTIONRESULT")).Equals("OK") ? 
                                        e.Appearance.ForeColor : Color.Red;

            //! 조회조건에 P_PRODUCTDEFID가 존재시, SelectedPopup의 X버튼 클릭 이벤트
            if (Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID") is var control)
            {
                control.Properties.ButtonClick += (s, e) =>
                {
                    if ((s as DevExpress.XtraEditors.ButtonEdit).Properties.Buttons.IndexOf(e.Button).Equals(1))
                    {
                        Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").Text = string.Empty;
                    }
                };
            }
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
                grdMain.View.ClearDatas();

                Dictionary<string, object> value = Conditions.GetValues();
                value.Add("P_SPECCLASSID", "SAVE_MEASURE_3D");

                if (await SqlExecuter.QueryAsync("GetOutgoing3DMeasureList", "10001", DefectMapHelper.AddLanguageTypeToConditions(value)) is DataTable dt)
                {
                    if (dt.Rows.Count.Equals(0))
                    {
                        grdMain.View.ClearDatas();
                        ShowMessage("NoSelectData");
                        return;
                    }

                    grdMain.DataSource = dt;
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
            base.InitializeCondition();

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
        }

        #endregion
    }
}