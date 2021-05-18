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
#endregion

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프로그램명 : 기준정보 > Setup > 품목유형 등록 및 조회
    /// 업 무 설명 : 품목 유형등록 
    /// 생  성  자 : 윤성원
    /// 생  성  일 : 2019-06-27
    /// 수정 이 력 : 
    /// 
    /// 
    /// </summary> 
	public partial class MasterDataClass : SmartConditionManualBaseForm
	{
        #region Local Variables
        #endregion

        #region 생성자
        public MasterDataClass()
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

            // 품목유형
            //grdMDCList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdMDCList.GridButtonItem = GridButtonItem.All;   // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기
            grdMDCList.View.AddTextBoxColumn("ENTERPRISEID").SetIsHidden();
            grdMDCList.View.AddTextBoxColumn("PLANTID").SetIsHidden();
            grdMDCList.View.AddTextBoxColumn("VALIDSTATE").SetIsHidden();
            grdMDCList.View.AddTextBoxColumn("MASTERDATACLASSID", 150).SetValidationIsRequired().SetValidationKeyColumn();
            grdMDCList.View.AddTextBoxColumn("MASTERDATACLASSNAME", 200).SetValidationIsRequired();
            grdMDCList.View.AddTextBoxColumn("DESCRIPTION", 250);
            grdMDCList.View.AddComboBoxColumn("IDCLASSIDRULE", new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdMDCList.View.AddComboBoxColumn("DESCRIPTIONRULE", new SqlQuery("GetRuleDefinition", "10001", "RULETYPE=Description"), "RULENAME", "RULEID");
            grdMDCList.View.AddComboBoxColumn("DUPLICATERULE", new SqlQuery("GetRuleDefinition", "10001", "RULETYPE=Duplication"), "RULENAME", "RULEID");

            //grdMDCList.View.AddComboBoxColumn("ISAPPROVAL", new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
            //    .SetValidationIsRequired();

            grdMDCList.View.AddComboBoxColumn("ISAPPROVAL", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
            .SetDefault("N").SetTextAlignment(TextAlignment.Center)
            .SetValidationIsRequired();

            grdMDCList.View.AddComboBoxColumn("ITEMOWNER", new SqlQuery("GetCodeList", "00001", "CODECLASSID=ItemOwner", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdMDCList.View.AddComboBoxColumn("MESITEMTYPE", new SqlQuery("GetCodeList", "00001", "CODECLASSID=MESItemType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdMDCList.View.PopulateColumns();


            //품목유형
            grdMDCList1.GridButtonItem = GridButtonItem.None;   // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기
            grdMDCList1.View.AddTextBoxColumn("ENTERPRISEID");
            grdMDCList1.View.AddTextBoxColumn("MASTERDATACLASSID", 150).SetIsReadOnly();
            grdMDCList1.View.AddTextBoxColumn("MASTERDATACLASSNAME", 200).SetIsReadOnly();
            grdMDCList1.View.PopulateColumns();

            //속성
            grdAAGList.GridButtonItem = GridButtonItem.All;   // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기
            grdAAGList.View.AddTextBoxColumn("ENTERPRISEID").SetIsHidden(); 
            grdAAGList.View.AddTextBoxColumn("AABGSEQUENCE").SetIsHidden(); 
            grdAAGList.View.AddTextBoxColumn("PLANTID").SetIsHidden(); 
            grdAAGList.View.AddTextBoxColumn("VALIDSTATE").SetIsHidden();

            grdAAGList.View.AddTextBoxColumn("MASTERDATACLASSID", 150).SetValidationKeyColumn().SetValidationIsRequired();
            grdAAGList.View.AddComboBoxColumn("ATTRIBUTEGROUPID", new SqlQuery("GetAttributeGroupList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "DESCRIPTION", "ATTRIBUTEGROUPID").SetValidationIsRequired();
            //grdAAGList.View.AddTextBoxColumn("MASTERDATACLASSNAME", 200).SetValidationIsRequired();
            grdAAGList.View.PopulateColumns();

            //속성
            grdAttribGList.GridButtonItem = GridButtonItem.None;   // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기
            grdAttribGList.View.AddTextBoxColumn("ATTRIBUTEGROUPID").SetIsHidden();
            grdAttribGList.View.AddTextBoxColumn("ENTERPRISEID").SetIsHidden();
            grdAttribGList.View.AddTextBoxColumn("PLANTID").SetIsHidden();
            grdAttribGList.View.AddTextBoxColumn("VALIDSTATE").SetIsHidden();
            grdAttribGList.View.AddSpinEditColumn("ATTRIBUTESEQUENCE", 150).SetIsReadOnly();
            //grdAttribGList.View.AddTextBoxColumn("CODECLASSNAME", 250).SetIsReadOnly();
            grdAttribGList.View.AddTextBoxColumn("CODEID", 150).SetIsReadOnly();
            grdAttribGList.View.AddTextBoxColumn("CODENAME", 250).SetIsReadOnly();
            grdAttribGList.View.AddTextBoxColumn("DESCRIPTION", 250).SetIsReadOnly();
            grdAttribGList.View.PopulateColumns();

        }

        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeGridIdDefinitionManagement();
        }

        #endregion

        #region 툴바
        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            DataTable changed = new DataTable();
            switch (tabIdManagement.SelectedTabPageIndex)
            {
                case 0:// MDC
                    changed = grdMDCList.GetChangedRows();
                    ExecuteRule("MasterDataClass", changed);
                    break;
                case 1:// AAG
                    changed = grdAAGList.GetChangedRows();
                    ExecuteRule("AssignAttributeGroup", changed);
                    break;
                case 2:// MDCI
                    //changed = grdIdDefinitionList.GetChangedRows();
                    //ExecuteRule("IdDefinitionManagement", changed);
                    break;

            }
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
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            //TODO : Id를 수정하세요            
          
            switch (tabIdManagement.SelectedTabPageIndex)
            {
                case 0://ID Class
                    DataTable dtMDCList = await SqlExecuter.QueryAsync("GetmasterdataclassList", "10001", values);

                    if (dtMDCList.Rows.Count < 1) // 
                    {
                        ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                    }

                    this.grdMDCList.DataSource = dtMDCList;

                    break;
                case 1://ID Definition
                       //DataTable dtIdDefinitionList = await ProcedureAsync("usp_com_selectiddefinitionlist", values);

                    //if (dtIdDefinitionList.Rows.Count < 1) // 
                    //{
                    //    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                    //}

                    //grdIdDefinitionList.DataSource = dtIdDefinitionList;

                    break;
                case 2://ID Definition
                       //DataTable dtIdDefinitionList = await ProcedureAsync("usp_com_selectiddefinitionlist", values);

                    //if (dtIdDefinitionList.Rows.Count < 1) // 
                    //{
                    //    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                    //}

                    //grdIdDefinitionList.DataSource = dtIdDefinitionList;

                    break;
            }

        }
        #endregion

        #region 유효성 검사
        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            DataTable changed = new DataTable();
            switch (tabIdManagement.SelectedTabPageIndex)
            {
                case 0://ID Class
                    grdMDCList.View.CheckValidation();
                    DataTable dt = (DataTable)grdMDCList.DataSource;
                    changed = grdMDCList.GetChangedRows();

                    //object obj = grdMDCList.DataSource;
                    //DataTable dt = (DataTable)obj;
                    //string sMessage = "";
                    //foreach (DataRow row in dt.DefaultView.ToTable(true, new string[] { "MASTERDATACLASSID" }).Rows)
                    //{
                    //    int count = dt.Select("MASTERDATACLASSID = '" + row["MASTERDATACLASSID"].ToString() + "'").Length;
                    //    //int i = int.Parse(dt.Compute("COUNT(MASTERDATACLASSID)", "MASTERDATACLASSID = '" + row["MASTERDATACLASSID"].ToString() + "'").ToString());
                    //    if (count > 1)
                    //    {
                    //        sMessage = sMessage + "품목유형 코드 " + row["MASTERDATACLASSID"].ToString() + "은" + count.ToString() + " 개가 중복입니다." + "\r\n";
                    //        //sfilter = "MASTERDATACLASSID = '" + row["MASTERDATACLASSID"].ToString() + "'";
                    //    }
                    //}
                    //if (sMessage != "")
                    //{
                    //    throw MessageException.Create(sMessage);
                    //}

                    break;
                case 1://ID Definition
                    grdAAGList.View.CheckValidation();
                    changed = grdAAGList.GetChangedRows();

                    //object obj1 = grdAAGList.DataSource;
                    //DataTable dt1 = (DataTable)obj1;
                    //string sMessage1 = "";



                    //foreach (DataRow row in dt1.Rows)
                    //{
                    //    int count = dt1.Select("MASTERDATACLASSID = '" + row["MASTERDATACLASSID"].ToString() + "' AND ATTRIBUTEGROUPID = '" + row["ATTRIBUTEGROUPID"].ToString() + "'").Length;
                    //    //int i = int.Parse(dt.Compute("COUNT(MASTERDATACLASSID)", "MASTERDATACLASSID = '" + row["MASTERDATACLASSID"].ToString() + "'").ToString());
                    //    if (count > 1)
                    //    {
                    //        sMessage1 = row["MASTERDATACLASSID"].ToString() + "/" + row["ATTRIBUTEGROUPID"].ToString();
                    //        //sfilter = "MASTERDATACLASSID = '" + row["MASTERDATACLASSID"].ToString() + "'";
                    //    }
                    //}
                    //if (sMessage1 != "")
                    //{
                    //    throw MessageException.Create("InValidData007", sMessage1);
                    //}


                    break;
                case 2://ID Definition
                    //grdIdDefinitionList.View.CheckValidation();
                    //changed = grdIdDefinitionList.GetChangedRows();
                    break;
            }
            if (changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }
        #endregion

        #region 이벤트
        private void InitializeEvent()
        {
            
            tabIdManagement.Click += TabIdManagement_Click;
            grdMDCList1.View.FocusedRowChanged += grdMDCList1_FocusedRowChanged;
            //grdAAGList.View.FocusedRowChanged += grdAAGList_FocusedRowChanged;

            grdAAGList.View.Click += grdAAGList_Click;


            grdAAGList.View.AddingNewRow += grdAAGList_AddingNewRow;
            grdMDCList.View.AddingNewRow += grdMDCList_AddingNewRow;
            //grdAAGList.View.ValidateRow += grdAAGList_ValidateRow;

        }
        //속성그룹지정 그리드 클릭 이벤트
        private void grdAAGList_Click(object sender, EventArgs e)
        {
            grdAAGListFocusedRowChanged();
        }

        #region 그리드이벤트

       

        //속성그룹지정 그리드 포커스로우 체인지 함수
        private void grdAAGListFocusedRowChanged()
        {

            if (grdAAGList.View.FocusedRowHandle < 0)
                return;


            DataRow paramrow = this.grdAAGList.View.GetFocusedDataRow();

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("ATTRIBUTEGROUPID", paramrow["ATTRIBUTEGROUPID"].ToString());
            param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);

            DataTable dtAttrib = SqlExecuter.Query("GetAttributeList", "10001", param);
            this.grdAttribGList.DataSource = dtAttrib;

        }
       

        // 품목유형별 정의 + 툴버튼  그리드 추가 이벤트
        private void grdMDCList_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            DataRow row = this.grdMDCList.View.GetFocusedDataRow();
            args.NewRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
            args.NewRow["PLANTID"] = UserInfo.Current.Plant;
            args.NewRow["VALIDSTATE"] = "Valid";
            
        }

        // 품목 속성그룹지정 + 툴버튼 그리드 추가 이벤트
        private void grdAAGList_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            if (grdMDCList1.View.RowCount == 0)
            {
                sender.CancelAddNew();
                return;
            }

            DataRow row = this.grdMDCList1.View.GetFocusedDataRow();
            args.NewRow["ENTERPRISEID"] = row["ENTERPRISEID"];
            args.NewRow["MASTERDATACLASSID"] = row["MASTERDATACLASSID"];
            args.NewRow["PLANTID"] = row["PLANTID"];
            args.NewRow["VALIDSTATE"] = "Valid";
            

            object objMax = grdAAGList.DataSource;
            DataTable dtMax = (DataTable)objMax;

            // 순번증가
            if (grdAAGList.View.RowCount != 1)
            {
                args.NewRow["AABGSEQUENCE"] = int.Parse(dtMax.Compute("MAX(AABGSEQUENCE)", "").ToString()) + 1;
            }
            else
            {
                args.NewRow["AABGSEQUENCE"] = 1;
            }



        }

        //품목유형별 정의 그리드 포커스 로우 변경 이벤트
        private void grdMDCList1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            grdMDCList1FocusedRowChanged();
            grdAttribGList.DataSource = null;
        }

        //품목유형별 정의 그리드 포커스 로우변셩시 호출함수
        private void grdMDCList1FocusedRowChanged()
        {

            if (grdMDCList1.View.FocusedRowHandle < 0)
                return;

            
            DataRow paramrow = this.grdMDCList1.View.GetFocusedDataRow();
            

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("MASTERDATACLASSID", paramrow["MASTERDATACLASSID"].ToString());
            param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);

            DataTable dtAAGList1 = SqlExecuter.Query("GetAssignAttributeGroupList", "10001", param);
         
            this.grdAAGList.DataSource = dtAAGList1;

        }
        #endregion

        #region 기타이벤트

        private void TabIdManagement_Click(object sender, EventArgs e)
        {
            var values = this.Conditions.GetValues();
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);

            switch (tabIdManagement.SelectedTabPageIndex)
            {
                case 0:
                     // 마스터 클래스 조회
                    DataTable dtMDCList = SqlExecuter.Query("GetmasterdataclassList", "10001", values);
                    if (dtMDCList.Rows.Count < 1) // 
                    {
                        ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                    }

                    this.grdMDCList.DataSource = dtMDCList;
                    break;
                case 1:
                    // 등록된 마스트 클래스 데이터 조회
                    DataTable dtMDCList1 = SqlExecuter.Query("GetmasterdataclassList", "10001", values);
                    if (dtMDCList1.Rows.Count < 1) // 
                    {
                        ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                    }

                    this.grdMDCList1.DataSource = dtMDCList1;

                    break;
                default:
                    break;

            }


            //품목유형
            DataTable dtMDCL = grdMDCList.GetChangedRows();
            if (dtMDCL != null)
            {
                if (dtMDCL.Rows.Count != 0)
                {
                    //DialogResult result = ShowMessage(MessageBoxButtons.YesNo, "변경된 내역이 존재 합니다. 저장하시겠습니까 ? \r\n저장시 해당 탭으로 이동후 저장 합니다.");
                    //if (result == DialogResult.Yes)
                    //{
                    //    tabIdManagement.SelectedTabPageIndex = 0;
                    //    OnToolbarSaveClick();
                    //}
                    ShowMessage("ChangeMasterDataClassCheck");
                }
            }

            //속성그룹
            DataTable dtAAGL = grdAAGList.GetChangedRows();
            if (dtAAGL != null)
            {
                if (dtAAGL.Rows.Count != 0)
                {
                    //DialogResult result = ShowMessage(MessageBoxButtons.YesNo, "변경된 내역이 존재 합니다. 저장하시겠습니까 ? \r\n 저장시 해당 탭으로 이동후 저장 합니다.");
                    //if (result == DialogResult.Yes)
                    //{
                    //    tabIdManagement.SelectedTabPageIndex = 1;
                    //    OnToolbarSaveClick();
                    //}
                    ShowMessage("ChangeAttributeGroupCheck");
                }
            }
        }

        #endregion

        #endregion

       

        #region Private Function

        // TODO : 화면에서 사용할 내부 함수 추가

        #endregion
    }
}
