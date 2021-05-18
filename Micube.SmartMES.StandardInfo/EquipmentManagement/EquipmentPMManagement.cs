#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons.Controls;
using System;
using System.Data;
using System.Threading.Tasks;

#endregion using

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프 로 그 램 명  : 기준정보 > 설비기준정보 > 설비 PM
    /// 업  무  설  명  : 설비 PM 정보를 관리한다.
    /// 생    성    자  : 강유라
    /// 생    성    일  : 2019-05-21
    /// 수  정  이  력  :
    ///     2021.02.18 전우성 코드 정규화 및 정리
    ///
    /// </summary>
    public partial class EquipmentPMManagement : SmartConditionManualBaseForm
    {
        #region 생성자

        public EquipmentPMManagement()
        {
            InitializeComponent();
        }

        #endregion 생성자

        #region 컨텐츠 영역 초기화

        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeGrid();
            InitializeEvent();
        }

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            grdPM.GridButtonItem = GridButtonItem.Add | GridButtonItem.Export;
            grdPM.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            grdPM.View.AddTextBoxColumn("ENTERPRISEID", 150).SetIsHidden();

            grdPM.View.AddTextBoxColumn("PLANTID", 150).SetIsReadOnly();
            grdPM.View.AddComboBoxColumn("MAINTITEMCLASSTYPE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=MaintType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdPM.View.AddTextBoxColumn("MAINTITEMCLASSID", 200).SetValidationKeyColumn();
            grdPM.View.AddLanguageColumn("MAINTITEMCLASSNAME", 150);
            grdPM.View.AddTextBoxColumn("DESCRIPTION", 150);
            grdPM.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetDefault("Valid").SetTextAlignment(TextAlignment.Center);

            grdPM.View.AddTextBoxColumn("CREATOR", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdPM.View.AddTextBoxColumn("CREATEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdPM.View.AddTextBoxColumn("MODIFIER", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdPM.View.AddTextBoxColumn("MODIFIEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly().SetTextAlignment(TextAlignment.Center);

            grdPM.View.PopulateColumns();

            grdPM.ShowStatusBar = true;
        }

        #endregion 컨텐츠 영역 초기화

        #region Event

        /// <summary>
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            // 새로운 ROW 추가시 PLANTID, ENTERPRISEID를 자동 등록해주는 이벤트
            grdPM.View.AddingNewRow += (s, e) =>
            {
                e.NewRow["PLANTID"] = UserInfo.Current.Plant;
                e.NewRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
            };

            //new SetGridDeleteButonVisible(grdPM);
        }

        #endregion Event

        #region 툴바

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            ExecuteRule("SavePMManagement", grdPM.GetChangedRows());
        }

        #endregion 툴바

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

                grdPM.View.ClearDatas();

                var values = Conditions.GetValues();
                values.Add("p_languagrtype", UserInfo.Current.LanguageType);

                if (await SqlExecuter.QueryAsync("SelectPMManagement", "10001", values) is DataTable dt)
                {
                    if (dt.Rows.Count.Equals(0))
                    {
                        ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                    }

                    grdPM.DataSource = dt;
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
        /// 공장 조회조건에 User Plant 정보 설정 및 수정 불가
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            this.Conditions.GetControl<SmartTextBox>("P_PLANTID").EditValue = UserInfo.Current.Plant;
            this.Conditions.GetControl<SmartTextBox>("P_PLANTID").Enabled = false;
        }

        #endregion 검색

        #region 유효성 검사

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            grdPM.View.CheckValidation();

            if (grdPM.GetChangedRows().Rows.Count.Equals(0))
            {
                throw MessageException.Create("NoSaveData"); // 저장할 데이터가 존재하지 않습니다.
            }
        }

        #endregion 유효성 검사
    }
}