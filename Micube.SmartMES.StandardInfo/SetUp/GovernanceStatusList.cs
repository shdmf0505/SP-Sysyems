#region using
using Micube.Framework.Net;
using Micube.Framework;
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

using DevExpress.XtraCharts;
using DevExpress.Utils;
using Micube.Framework.SmartControls.Validations;

using System.Globalization;

#endregion

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프로그램명 : 기준정보 > Setup > 품목규칙 등록
    /// 업 무 설명 : 품목 코드,명,계산식 등록
    /// 생  성  자 : 윤성원
    /// 생  성  일 : 2019-06-27
    /// 수정 이 력 : 
    /// </summary> 
	public partial class GovernanceStatusList : SmartConditionManualBaseForm
	{
        #region Local Variables
        #endregion

        #region 생성자
        public GovernanceStatusList()
		{
			InitializeComponent();
            InitializeEvent();
           
        }

        #endregion

        #region 컨텐츠 영역 초기화


        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGridIdDefinitionManagement()
        {

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // 승인 그리드 초기화
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            grdGovernanceStatu.GridButtonItem = GridButtonItem.None;   // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기
            grdGovernanceStatu.View.AddTextBoxColumn("GOVERNANCENO", 100).SetIsReadOnly();
            grdGovernanceStatu.View.AddTextBoxColumn("PRODUCTDEFID", 100).SetIsReadOnly();
            grdGovernanceStatu.View.AddTextBoxColumn("PRODUCTDEFVERSION", 100).SetIsReadOnly();
            grdGovernanceStatu.View.AddTextBoxColumn("STARTDATE", 100)
            .SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime)
            .SetIsReadOnly()
            .SetTextAlignment(TextAlignment.Center);

            grdGovernanceStatu.View.AddTextBoxColumn("SPECPERSON", 100).SetIsReadOnly();
            grdGovernanceStatu.View.AddTextBoxColumn("SPECPERSONNAME", 100).SetIsReadOnly();

            grdGovernanceStatu.View.AddComboBoxColumn("GOVERNANCETYPE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=GovernanceType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
            .SetIsReadOnly();

            //grdGovernanceStatu.View.AddTextBoxColumn("ENTERPRISEID", 100);
            //grdGovernanceStatu.View.AddTextBoxColumn("PLANTID", 100);
            grdGovernanceStatu.View.AddTextBoxColumn("DEPARTMENT", 100).SetIsReadOnly();
            grdGovernanceStatu.View.AddTextBoxColumn("REASON", 100).SetIsReadOnly();
            grdGovernanceStatu.View.AddTextBoxColumn("PRIORITY", 100).SetIsReadOnly();


            grdGovernanceStatu.View.AddComboBoxColumn("APPROVALTYPE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ApprovalType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
            .SetIsHidden();
            grdGovernanceStatu.View.AddTextBoxColumn("APPROVALID", 100).SetIsHidden();
            grdGovernanceStatu.View.AddTextBoxColumn("SALESPERSON", 100).SetIsReadOnly();
            grdGovernanceStatu.View.AddTextBoxColumn("SALESPERSONNAME", 100).SetIsReadOnly();


            //grdGovernanceStatu.View.AddTextBoxColumn("STATUS", 100);
            grdGovernanceStatu.View.AddTextBoxColumn("STARTDATE", 100)
            .SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime)
            .SetIsReadOnly()
            .SetTextAlignment(TextAlignment.Center);


            //grdGovernanceStatu.View.AddTextBoxColumn("REQUESTDATE", 100);

            grdGovernanceStatu.View.AddComboBoxColumn("IMPLEMENTATIONTYPE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ImplementationType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));



            grdGovernanceStatu.View.AddDateEditColumn("IMPLEMENTATIONDATE", 100)
            .SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime)
            .SetIsReadOnly()
            .SetTextAlignment(TextAlignment.Center);

            //grdGovernanceStatu.View.AddTextBoxColumn("ERPITEMDATE", 100);
            grdGovernanceStatu.View.AddTextBoxColumn("ISCNCATTACHED", 100).SetIsReadOnly();
            grdGovernanceStatu.View.AddTextBoxColumn("CAMREQUESTID", 100).SetIsReadOnly();
            grdGovernanceStatu.View.AddTextBoxColumn("CAMPERSON", 100).SetIsReadOnly();
            grdGovernanceStatu.View.AddTextBoxColumn("PCRNO", 100).SetIsReadOnly();
            grdGovernanceStatu.View.AddTextBoxColumn("PCRREQUESTER", 100).SetIsReadOnly();
            grdGovernanceStatu.View.AddTextBoxColumn("PCRDATE", 100).SetIsReadOnly();
            grdGovernanceStatu.View.AddDateEditColumn("MODELDELIVERYDATE", 100)
            .SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime)
            .SetIsReadOnly()
            .SetTextAlignment(TextAlignment.Center);

            grdGovernanceStatu.View.AddTextBoxColumn("MODELNO", 100).SetIsReadOnly();
            grdGovernanceStatu.View.AddTextBoxColumn("WORKTYPE", 100).SetIsReadOnly();
            grdGovernanceStatu.View.AddTextBoxColumn("WORKCLASS", 100).SetIsReadOnly();
            grdGovernanceStatu.View.AddTextBoxColumn("PRODUCTCLASS", 100).SetIsReadOnly();
            grdGovernanceStatu.View.AddTextBoxColumn("CUSTOMERID", 80).SetIsReadOnly();
            grdGovernanceStatu.View.AddTextBoxColumn("CUSTOMERNAME", 150).SetIsReadOnly();
            grdGovernanceStatu.View.AddTextBoxColumn("CUSTOMERREV", 100).SetIsReadOnly();
            grdGovernanceStatu.View.AddTextBoxColumn("PANELSIZE", 100).SetIsReadOnly();
            grdGovernanceStatu.View.AddTextBoxColumn("DESCRIPTION", 100).SetIsReadOnly();

          
            grdGovernanceStatu.View.PopulateColumns();

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // 승인 이력 그리드 초기화
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            grdApprovaltransaction.GridButtonItem = GridButtonItem.None;   // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기
            grdApprovaltransaction.View.AddTextBoxColumn("APPROVALTYPE", 80).SetIsHidden();
            grdApprovaltransaction.View.AddTextBoxColumn("APPROVALID", 80).SetIsHidden();
            grdApprovaltransaction.View.AddTextBoxColumn("ENTERPRISEID", 80).SetIsHidden();
            grdApprovaltransaction.View.AddTextBoxColumn("PLANTID", 80);
            grdApprovaltransaction.View.AddSpinEditColumn("SEQUENCE", 80).SetIsHidden();
            grdApprovaltransaction.View.AddSpinEditColumn("RESULTTYPE").SetIsHidden();
           
            //grdApprovaltransaction.View.AddTextBoxColumn("RESULTS", 80).SetIsHidden();
            grdApprovaltransaction.View.AddComboBoxColumn("RESULTS", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ApprovalStatus", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));


            //grdApprovaltransaction.View.AddTextBoxColumn("RESULTTYPE", 80).SetIsReadOnly();
            grdApprovaltransaction.View.AddTextBoxColumn("ACTOR", 80).SetIsReadOnly();
            grdApprovaltransaction.View.AddTextBoxColumn("ACTORNAME", 80).SetIsReadOnly();

            grdApprovaltransaction.View.AddDateEditColumn("STARTDATE", 100)
            .SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime)
            .SetIsReadOnly()
            .SetTextAlignment(TextAlignment.Center);
            grdApprovaltransaction.View.AddDateEditColumn("ENDDATE", 100)
            .SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime)
            .SetIsReadOnly()
            .SetTextAlignment(TextAlignment.Center);
            grdApprovaltransaction.View.AddTextBoxColumn("DESCRIPTION", 150);
            grdApprovaltransaction.View.PopulateColumns();


        }
       
        protected override void InitializeContent()
        {
            base.InitializeContent();
            InitializeGridIdDefinitionManagement();
        }

        #endregion

        #region 이벤트

        private void InitializeEvent()
        {
         
            grdGovernanceStatu.View.FocusedRowChanged += grdApproval_FocusedRowChanged;
            btnDetail.Click += BtnDetail_Click;
        }



        private void BtnDetail_Click(object sender, EventArgs e)
        {
            DataRow row = grdGovernanceStatu.View.GetFocusedDataRow();

            switch (row["GOVERNANCETYPE"].ToString())
            {
                case "RunningChange":
                    Dictionary<string, object> ParamRc = new Dictionary<string, object>();
                    ParamRc.Add("GOVERNANCENO", row["GOVERNANCENO"].ToString());
                    this.OpenMenu("PG-SD-0260", ParamRc);

                    break;
                case "NewRequest":
                    Dictionary<string, object> ParamNr = new Dictionary<string, object>();
                    ParamNr.Add("GOVERNANCENO", row["GOVERNANCENO"].ToString());
                    this.OpenMenu("PG-SD-0230", ParamNr);
                    break;
            }


        }
        private void grdApproval_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grdGovernanceStatu.View.FocusedRowHandle < 0)
                return;

            DataRow row = grdGovernanceStatu.View.GetFocusedDataRow();

            Dictionary<string, object> Param = new Dictionary<string, object>();
            Param.Add("ENTERPRISEID", row["ENTERPRISEID"].ToString());
            Param.Add("APPROVALID", row["GOVERNANCENO"].ToString());
            DataTable dt = SqlExecuter.Query("GetGovernanceStatusTransactionList", "10001", Param);
            grdApprovaltransaction.DataSource = dt;
        }

       

       

        #endregion

       


        #region 툴바
        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

         

        }
        #endregion

        #region 검색

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
		{
			await base.OnSearchAsync();

            // 조회조건에 입력된 값들을 Dictionary<string, object> Type으로 가져옴

            var values = this.Conditions.GetValues();
          
            

            DataTable dtApproval = await SqlExecuter.QueryAsync("GetGovernanceStatusList", "10001", values);

            if (dtApproval.Rows.Count < 1) // 
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }
            grdApprovaltransaction.DataSource = null;
            
            this.grdGovernanceStatu.DataSource = dtApproval;

        }
        #endregion
        /// <summary>
        /// 검색조건 초기화. 
        /// 조회조건 정보, 메뉴 - 조회조건 매핑 화면에 등록된 정보를 기준으로 구성됩니다.
        /// DB에 등록한 정보를 제외한 추가 조회조건 구성이 필요한 경우 사용합니다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();
            ////승인유형
            //Conditions.AddComboBox("APPROVALTYPE", new SqlQuery("GetCodeList", "00001", "CODECLASSID=ApprovalType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID").SetEmptyItem();
            ////처리상태
            //Conditions.AddComboBox("APPROVALSTATUS", new SqlQuery("GetCodeList", "00001", "CODECLASSID=ApprovalStatus", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID").SetEmptyItem();

            //InitializeCondition_Popup();
        }

        /// <summary>
        /// 검색조건 팝업 예제
        /// </summary>
        //private void InitializeCondition_Popup()
        //{
        //    //팝업 컬럼 설정
        //    var parentPopupColumn = Conditions.AddSelectPopup("USERID", new SqlQuery("GetUserAreaPerson", "10001"), "USERNAME", "USERID")
        //       .SetPopupLayout("USERID", PopupButtonStyles.Ok_Cancel, true, false)  //팝업창 UI 설정.
        //       .SetPopupResultCount(1)
        //       .SetIsReadOnly();  //팝업창 선택가능한 개수
               

        //    // 팝업에서 사용할 조회조건 항목 추가
        //    parentPopupColumn.Conditions.AddTextBox("USERNAME");

        //    // 팝업 그리드 설정
        //    parentPopupColumn.GridColumns.AddTextBoxColumn("USERID", 100);
        //    parentPopupColumn.GridColumns.AddTextBoxColumn("USERNAME", 150);
            
        //}
        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        //protected override void InitializeConditionControls()
        //{
        //    base.InitializeConditionControls();
        //    SmartSelectPopupEdit Popupedit = Conditions.GetControl<SmartSelectPopupEdit>("USERID");
        //    Popupedit.EditValue = UserInfo.Current.Id;
        //    Popupedit.Text = UserInfo.Current.Name;
        //    //Popupedit.Validated += Popupedit_Validated;

        //}

    }
}

