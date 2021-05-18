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
    /// 프 로 그 램 명  : 품질 관리 > Defect Map > Lot Defect Map 조회
    /// 업  무  설  명  : Lot 별로 Defect Map을 조회한다.
    ///                  설비에서 보낸 log Data의 내용만 조회 된다.
    /// 생    성    자  : 전우성
    /// 생    성    일  : 2019-05-01
    /// 수  정  이  력  : 
    /// 2019.10.09 배선용 using Micube.Framework 추가
    ///            배선용 재공조회에서 화면이동시 이벤트 처리 override LoadForm
    /// </summary>
    public partial class DefectMapByLot : SmartConditionManualBaseForm
    {
        #region Local Variables

        /// <summary>
        /// Override LoadForm로 처리시 조회조건
        /// </summary>
        private Dictionary<string, object> _param = null;

        #endregion

        #region 생성자

        /// <summary>
        /// 생성자
        /// </summary>
        public DefectMapByLot()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeEvent();

            pnlContent.Controls.Add(new ucLotDefetMap() { Dock = DockStyle.Fill });
        }

        #endregion

        #region Event

        /// <summary>        
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            Load += async (s, e) =>
            {
                if (!DefectMapHelper.IsNull(_param))
                {
                    Conditions.GetControl<SmartSelectPopupEdit>("P_LOTID").SetValue(Format.GetString(_param["LOTID"]));
                    Conditions.GetControl<SmartTextBox>("P_PRODUCTDEFID").Text = Format.GetString(_param["PRODUCTDEFID"]);
                    Conditions.GetControl<SmartTextBox>("P_PRODUCTDEFVERSION").Text = Format.GetString(_param["PRODUCTREVISION"]);

                    await OnSearchAsync();
                }
            };
        }

        /// <summary>
        /// 2019.10.09 배선용
        /// 재공조회에서 화면이동시 이벤트 처리
        /// </summary>
        /// <param name="parameters"></param>
        public override void LoadForm(Dictionary<string, object> parameters)
        {
            base.LoadForm(parameters);

            _param = parameters;
        }

        #endregion

        #region 검색

        protected async override Task OnSearchAsync()
        {
            try
            {
                DialogManager.ShowWaitArea(this.pnlContent);

                await base.OnSearchAsync();

                if (await SqlExecuter.QueryAsync("GetDefectMapList", "10001",
                                                 DefectMapHelper.AddLanguageTypeToConditions(Conditions.GetValues())) is DataTable dt)
                {
                    if (dt.Rows.Count.Equals(0))
                    {
                        ShowMessage("NoSelectData");
                        ((ucLotDefetMap)pnlContent.Controls[0]).Close();
                        return;
                    }

                    ((ucLotDefetMap)pnlContent.Controls[0]).Run(dt);
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

            // 최종 오픈 때 주석한 쿼리로 변경해야함
            var LotId = Conditions.AddSelectPopup("P_LOTID", new SqlQuery("GetDefectMapLotList", "10001"))
                                  .SetLabel("LOTID")
                                  .SetPopupLayout("GRIDLOTLIST", PopupButtonStyles.Ok_Cancel, true, false)
                                  .SetPopupLayoutForm(700, 400, FormBorderStyle.FixedToolWindow)
                                  .SetValidationIsRequired()
                                  .SetPosition(0)
                                  .SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
                                  {
                                      foreach (DataRow row in selectedRow)
                                      {
                                          Conditions.GetControl<SmartTextBox>("P_PRODUCTDEFID").Text = DefectMapHelper.StringByDataRowObejct(row, "P_PRODUCTDEFID");
                                          Conditions.GetControl<SmartTextBox>("P_PRODUCTDEFVERSION").Text = DefectMapHelper.StringByDataRowObejct(row, "P_PRODUCTDEFVERSION");
                                      }
                                  });

            LotId.Conditions.AddTextBox("P_LOTID").SetLabel("LOTID");
            LotId.Conditions.AddTextBox("P_PRODUCTDEFID").SetLabel("PRODUCTDEFID");

            LotId.GridColumns.AddTextBoxColumn("P_LOTID", 150).SetLabel("LOTID");
            LotId.GridColumns.AddTextBoxColumn("P_PRODUCTDEFID", 120).SetLabel("PRODUCTDEFID");
            LotId.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 150);
            LotId.GridColumns.AddTextBoxColumn("P_PRODUCTDEFVERSION", 80).SetLabel("PRODUCTDEFVERSION");

            Conditions.AddTextBox("P_PRODUCTDEFID").SetLabel("NONE").SetIsHidden();
            Conditions.AddTextBox("P_PRODUCTDEFVERSION").SetLabel("NONE").SetIsHidden();
        }

        #endregion
    }
}
