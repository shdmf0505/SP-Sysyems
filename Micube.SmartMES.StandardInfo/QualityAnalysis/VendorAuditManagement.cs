#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;

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

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프 로 그 램 명  : 기준정보 > 품질 기준 정보 > 품질 업체별 Audit
    /// 업  무  설  명  : 품질 업체별 Audit을 관리한다.
    /// 생    성    자  : 강유라
    /// 생    성    일  : 2019-06-17
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class VendorAuditManagement : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가

        #endregion

        #region 생성자

        public VendorAuditManagement()
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

            // TODO : 컨트롤 초기화 로직 구성
            InitializeGrid();
        }

        /// <summary>        
        /// Vendor Audit 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdVendorAudit.GridButtonItem = GridButtonItem.None;
            grdVendorAudit.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdVendorAudit.View.AddTextBoxColumn("AREAID", 150)
                .SetIsReadOnly();
                //.SetIsHidden();

            grdVendorAudit.View.AddTextBoxColumn("AREANAME", 150)
                .SetIsReadOnly();

            grdVendorAudit.View.AddTextBoxColumn("VENDORNAME", 150)
                .SetIsReadOnly();

            grdVendorAudit.View.AddComboBoxColumn("OWNTYPE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OwnType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsReadOnly();

            grdVendorAudit.View.AddTextBoxColumn("AUDITID", 150)
                .SetIsReadOnly();

            grdVendorAudit.View.AddTextBoxColumn("STARTDATE", 150)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdVendorAudit.View.AddTextBoxColumn("UPDATEDATE", 150)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdVendorAudit.View.AddTextBoxColumn("LASTWORKDATE", 150)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdVendorAudit.View.AddSpinEditColumn("HOLDDAY", 150);

            //STATE CODECLASSID 수정 해야함 다국어 적용 위한 SELECT 
            grdVendorAudit.View.AddSpinEditColumn("AUDITSTATE", 150)
                .SetIsReadOnly();

            grdVendorAudit.View.AddTextBoxColumn("PLANTID", 150)
                .SetIsReadOnly();

            grdVendorAudit.View.AddTextBoxColumn("ENTERPRISEID", 150)
                .SetIsHidden();

            grdVendorAudit.View.AddTextBoxColumn("DESCRIPTION", 150);

            grdVendorAudit.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Valid")
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Center);

            grdVendorAudit.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdVendorAudit.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdVendorAudit.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdVendorAudit.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdVendorAudit.View.PopulateColumns();
        }

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            grdVendorAudit.View.AddingNewRow += View_AddingNewRow;
        }

        /// <summary>
        /// 새로운 ROW 추가시 PLANTID,ENTERPRISEID를 자동 등록해주는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            args.NewRow["PLANTID"] = Framework.UserInfo.Current.Plant;
            args.NewRow["ENTERPRISEID"] = Framework.UserInfo.Current.Enterprise;
        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            // TODO : 저장 Rule 변경
            DataTable changed = grdVendorAudit.GetChangedRows();

            ExecuteRule("SaveVendorAudit", changed);
        }

        #endregion

        #region 검색

        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            // TODO : 조회 SP 변경
            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);

            DataTable dt = await SqlExecuter.QueryAsync("SelectVendorAudit", "10001", values);

            if (dt.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdVendorAudit.DataSource = dt;
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
            this.Conditions.GetControl<SmartTextBox>("P_PLANTID").EditValue = Framework.UserInfo.Current.Plant;
            this.Conditions.GetControl<SmartTextBox>("P_PLANTID").Enabled = false;
        }

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            // TODO : 유효성 로직 변경
            grdVendorAudit.View.CheckValidation();

            DataTable changed = grdVendorAudit.GetChangedRows();

            if (changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }

        #endregion

        #region Private Function

        // TODO : 화면에서 사용할 내부 함수 추가
        /// <summary>
        /// 마스터 정보를 조회한다.
        /// </summary>
        private void LoadData()
        {

        }

        #endregion
    }
}
