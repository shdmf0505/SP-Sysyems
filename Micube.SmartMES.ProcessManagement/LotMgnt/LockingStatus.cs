#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons;
using Micube.SmartMES.Commons.Controls;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion using

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 공정관리 > 재공 관리 > Locking 현황
    /// 업  무  설  명  : Locking 현황을 조회한다
    /// 생    성    자  : 한주석
    /// 생    성    일  : 2019-10-02
    /// 수  정  이  력  : 2020-01-13, 박정훈, Locking 사유 그룹 / 사유코드별 조회가능하도록 수정
    ///
    ///
    /// </summary>
    public partial class LockingStatus : SmartConditionManualBaseForm
    {
        #region 생성자

        public LockingStatus()
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
        }

        /// <summary>
        /// Locking 현황 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            grdMain.GridButtonItem = GridButtonItem.Export;

            //LOCKING , RESERVE LOCKING
            grdMain.View.AddTextBoxColumn("LOCKTYPE", 80);
            //현 상태
            grdMain.View.AddTextBoxColumn("STATE", 60).SetTextAlignment(TextAlignment.Center).SetLabel("CURRENTSTATUS");
            //예약공정
            grdMain.View.AddTextBoxColumn("RESERVESEGMENTNAME", 130);
            // 중공정
            grdMain.View.AddTextBoxColumn("MIDDLEPROCESSSEGMENTNAME", 100).SetLabel("MIDDLEPROCESSSEGMENTCLASS");
            // 공정
            grdMain.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 130).SetLabel("OPERATION");
            // 양산구분
            grdMain.View.AddTextBoxColumn("LOTTYPE", 60).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            // 제품구분
            grdMain.View.AddTextBoxColumn("PRODUCTDEFTYPE", 60).SetLabel("THEPRODUCTTYPE").SetTextAlignment(TextAlignment.Center);
            // 작업장
            grdMain.View.AddTextBoxColumn("AREANAME", 120);
            //품목코드
            grdMain.View.AddTextBoxColumn("PRODUCTDEFID", 100).SetLabel("ITEMID");
            //품목명
            grdMain.View.AddTextBoxColumn("PRODUCTDEFNAME", 200).SetLabel("ITEMNAME");
            // Lot No.
            grdMain.View.AddTextBoxColumn("LOTID", 180).SetTextAlignment(TextAlignment.Center);
            // SITE.
            grdMain.View.AddTextBoxColumn("PLANTID", 50).SetTextAlignment(TextAlignment.Center);
            //자사구분
            grdMain.View.AddTextBoxColumn("OWNTYPE", 80);
            // 대분류
            grdMain.View.AddTextBoxColumn("LOCKINGGROUP", 100).SetLabel("LARGECLASS");
            // 중분류
            grdMain.View.AddTextBoxColumn("LOCKINGTYPE", 100).SetLabel("MIDDLECLASS");
            // 사유
            grdMain.View.AddTextBoxColumn("LOCKINGCODE", 100).SetLabel("PCRNO");
            // 지정일자
            grdMain.View.AddTextBoxColumn("CREATEDTIME", 130).SetLabel("DESIGNATEDDATE").SetTextAlignment(TextAlignment.Center);
            // Locking 지정자
            grdMain.View.AddTextBoxColumn("CREATOR", 80).SetLabel("OWNER").SetTextAlignment(TextAlignment.Center).SetLabel("LOCKINGASSIGNER");
            // 해제사유
            grdMain.View.AddTextBoxColumn("UNLOCKINGCODE", 100).SetLabel("RELEASEREASON");
            // 해제일
            grdMain.View.AddTextBoxColumn("RELEASETIME", 130).SetLabel("STOPRELEASEDATE").SetTextAlignment(TextAlignment.Center);
            // 해제자
            grdMain.View.AddTextBoxColumn("RELEASEUSER", 80).SetLabel("RELEASELOCKINGUSER").SetTextAlignment(TextAlignment.Center);
            // UOM
            grdMain.View.AddTextBoxColumn("UNIT", 60).SetLabel("UOM").SetTextAlignment(TextAlignment.Center).SetDisplayFormat("{0:#,##0}").SetIsHidden();
            // PNL
            grdMain.View.AddSpinEditColumn("PANELQTY", 80).SetLabel("PNL").SetDisplayFormat("{0:#,##0}");
            // PCS
            grdMain.View.AddSpinEditColumn("QTY", 80).SetDisplayFormat("{0:#,##0}");
            // 체공일자
            grdMain.View.AddSpinEditColumn("FORMALDATE", 80).SetDisplayFormat("{0:#,##0.00}", MaskTypes.Numeric, true);

            grdMain.View.PopulateColumns();

            grdMain.View.SetIsReadOnly();
        }

        #endregion 컨텐츠 영역 초기화

        #region Event

        public override void LoadForm(Dictionary<string, object> parameters)
        {
            base.LoadForm(parameters);
            if (parameters != null)
            {
                Conditions.GetControl<SmartSelectPopupEdit>("P_LOTID").SetValue(Format.GetString(parameters["LOTID"]));
                Conditions.GetControl<SmartSelectPopupEdit>("P_LOTID").Text = Format.GetString(parameters["LOTID"]);
                SendKeys.Send("{F5}");
            }
        }

        #endregion Event

        #region 검색

        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            grdMain.View.ClearDatas();

            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            if (await SqlExecuter.QueryAsync("SelectLockingStatus", "10001", values) is DataTable dtLokingClass)
            {
                if (dtLokingClass.Rows.Count.Equals(0))
                {
                    ShowMessage("NoSelectData");
                }

                grdMain.DataSource = dtLokingClass;
            }
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // 품목
            CommonFunction.AddConditionProductPopup("P_PRODUCTDEFID", 2.5, true, Conditions);

            // LOT ID
            CommonFunction.AddConditionLotPopup("P_LOTID", 3.5, true, Conditions);

            #region 공정 그룹

            var condition = Conditions.AddSelectPopup("P_PROCESSSEGMENTID", new SqlQuery("GetProcessSegmentList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PROCESSSEGMENTNAME", "PROCESSSEGMENTID")
                                      .SetPopupLayout("SELECTPROCESSSEGMENT", PopupButtonStyles.Ok_Cancel, true, false)
                                      .SetPopupLayoutForm(600, 800, FormBorderStyle.FixedToolWindow)
                                      .SetLabel("PROCESSSEGMENT")
                                      .SetPopupResultCount(1)
                                      .SetPopupAutoFillColumns("PROCESSSEGMENTNAME");

            condition.Conditions.AddTextBox("PROCESSSEGMENT");
            condition.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150);
            condition.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);

            #endregion 공정 그룹

            // 작업장
            CommonFunction.AddConditionAreaByAuthorityPopup("P_AREA", 6.5, true, Conditions, false, false);
        }

        #endregion 검색
    }
}