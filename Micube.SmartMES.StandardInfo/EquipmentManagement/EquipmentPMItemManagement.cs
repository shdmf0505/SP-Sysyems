#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons;
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
    /// 프 로 그 램 명  : 기준정보 > 설비기준정보 > 설비 점검 항목
    /// 업  무  설  명  : 설비 점검 항목 정보를 관리한다.
    /// 생    성    자  : 강유라
    /// 생    성    일  : 2019-05-22
    /// 수  정  이  력  : 2019-11-22 한주석 : 점검상세 탭 생성
    /// 
    /// 
    /// </summary>
    public partial class EquipmentPMItemManagement : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가

        #endregion

        #region 생성자

        public EquipmentPMItemManagement()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeEvent();
            
        }

        /// <summary>        
        /// PMList 그리드 초기화
        /// </summary>
        private void InitializeGridPM()
        {
            grdPM.View.SetIsReadOnly();

            grdPM.View.AddTextBoxColumn("MAINTITEMCLASSID", 200);

            grdPM.View.AddTextBoxColumn("MAINTITEMCLASSNAME", 150);

            grdPM.View.AddTextBoxColumn("PLANTID", 150);

            grdPM.View.AddTextBoxColumn("ENTERPRISEID", 150)
                .SetIsHidden();

            grdPM.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Valid")
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Center);

            grdPM.View.PopulateColumns();

        }

        /// <summary>
        /// 점검항목 그리드 초기화
        /// </summary>
        private void InitializeGridPMItem()
        {
            grdPMItem.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdPMItem.GridButtonItem -= GridButtonItem.Delete;

            grdPMItem.View.AddTextBoxColumn("MAINTITEMCLASSID", 200)
                .SetIsReadOnly();

            grdPMItem.View.AddTextBoxColumn("MAINTITEMID", 200)
                .SetValidationKeyColumn();

            grdPMItem.View.AddLanguageColumn("MAINTITEMNAME", 150);


            grdPMItem.View.AddTextBoxColumn("PLANTID", 150)
                .SetIsHidden();

            grdPMItem.View.AddTextBoxColumn("ENTERPRISEID", 150)
                .SetIsHidden();

            grdPMItem.View.AddTextBoxColumn("DESCRIPTION", 150);

            grdPMItem.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Valid")
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Center);

            grdPMItem.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdPMItem.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdPMItem.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdPMItem.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdPMItem.View.PopulateColumns();

        }

        /// <summary>        
        /// 점검상세 그리드 초기화
        /// </summary>
        private void InitializeGridDetail()
        {

            // 점검순서
            grdinspectionlist.View.AddTextBoxColumn("USERSEQUENCE", 80)
                .SetLabel("INSPECTIONSEQUENCE")
                .SetValidationKeyColumn()
                .SetIsReadOnly();
            grdinspectionlist.View.AddTextBoxColumn("MINTDETAILSEQUENCE", 50)
                .SetIsHidden();
            // 점검ID
            grdinspectionlist.View.AddTextBoxColumn("MAINTITEMID", 80)
                .SetIsHidden();
            // 설비ID
            grdinspectionlist.View.AddTextBoxColumn("EQUIPMENTCLASSID", 80)
                .SetIsHidden();
            grdinspectionlist.View.AddTextBoxColumn("MAINTDETAILITEM", 200);
            // 점검위치
            grdinspectionlist.View.AddTextBoxColumn("MAINTPOSITION", 200);
            // 점검방법
            grdinspectionlist.View.AddTextBoxColumn("MAINTMETHOD", 200);
            // 유효상태
            grdinspectionlist.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
               .SetDefault("Valid")
               .SetValidationIsRequired()
               .SetTextAlignment(TextAlignment.Center);

            grdinspectionlist.View.PopulateColumns();

        }


        /// <summary>        
        ///  설비그룹 그리드 초기화
        /// </summary>
        private void InitializeGridEC()
        {
            grdequipmentclass.GridButtonItem -= GridButtonItem.CRUD;
            // 설비그룹 ID
            grdequipmentclass.View.AddTextBoxColumn("EQUIPMENTCLASSID", 80)
                .SetIsReadOnly();
            // 설비그룹 명
            grdequipmentclass.View.AddTextBoxColumn("EQUIPMENTCLASSNAME", 200)
                .SetIsReadOnly();
  
            grdequipmentclass.View.PopulateColumns();

        }
        /// <summary> 
        /// PM명 , 점검명 콤보 박스 초기화
        /// </summary>
        private void InitializationCombo()
        {
            cbopm.DisplayMember = "MAINTITEMCLASSNAME";
            cbopm.ValueMember = "MAINTITEMCLASSID";
            cbopm.UseEmptyItem = true;
            cbopm.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cbopm.ShowHeader = false;
            DataTable pmlist = new SqlQuery("GetMainItemClass", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}").Execute();
            cbopm.DataSource = pmlist;
            cbopm.EditValue = pmlist.Rows[1];

            cboinspection.DisplayMember = "MAINTITEMNAME";
            cboinspection.ValueMember = "MAINTITEMID";
            cboinspection.UseEmptyItem = true;
            cboinspection.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboinspection.ShowHeader = false;
            
            DataTable inspectionlist = new SqlQuery("GetMainItem", "10001", $"MAINITEMCLASSID = {cbopm.EditValue}" , $"LANGUAGETYPE={UserInfo.Current.LanguageType}").Execute();
            cboinspection.DataSource = inspectionlist;
            
        }


        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            Load += EquipmentPMItemManagement_Load;
            grdPMItem.View.AddingNewRow += View_AddingNewRow;
            grdinspectionlist.View.AddingNewRow += View_AddingNewRow2;
            grdPM.ToolbarRefresh += GrdPM_ToolbarRefresh;
            grdPM.View.FocusedRowChanged += View_FocusedRowChanged;
            new SetGridDeleteButonVisible(grdPMItem);
            grdequipmentclass.View.FocusedRowChanged += View_FocusedEqRowChanged;
            cbopm.EditValueChanged += Cbopm_EditValueChanged;
            cboinspection.EditValueChanged += CboInspect_EditValueChanged;
            cbopm.EditValueChanged += CboInspect_EditValueChanged;
        }
  

        private void EquipmentPMItemManagement_Load(object sender, EventArgs e)
        {
            InitializeGridPM();
            InitializeGridPMItem();
            InitializeGridDetail();
            InitializeGridEC();
            InitializationCombo();
            LoadDataGridPM();
        }

        /// <summary>
        /// PM 콤보박스가 변화할때마다 생기는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void Cbopm_EditValueChanged(object sender, EventArgs e)
        {
           string pm = cbopm.EditValue.ToString();
            DataTable inspectionlist = new SqlQuery("GetMainItem", "10001", $"MAINTITEMCLASSID={pm}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}").Execute();
            cboinspection.EditValue = "";
            cboinspection.DataSource = inspectionlist;
            DataTable dt = cboinspection.DataSource as DataTable;
            focusedEqumentRowChanged();


        }

        /// <summary>
        /// 점검명 콤보박스가 변화할때마다 생기는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void CboInspect_EditValueChanged(object sender, EventArgs e)
        {
            focusedEqumentRowChanged();

        }
        /// <summary>
        /// PMList의 포커스된 Row가 변경될때 호출되는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            focusedRowChanged();
        }

        /// <summary>
        /// Equmentlist의 포커스된 Row가 변경될때 호출되는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_FocusedEqRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            grdinspectionlist.DataSource = null;
            focusedEqumentRowChanged();
        }


        /// <summary>
        /// PMList를 새로고침하는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdPM_ToolbarRefresh(object sender, EventArgs e)
        {
            try
            {
                int beforeFocusCodeClass = grdPM.View.FocusedRowHandle;  // 기존 포커스행을 가져오기 위한 handle.
                grdPM.ShowWaitArea(); // 진행중 박스
                LoadDataGridPM(); // 재로드
                grdPM.View.FocusedRowHandle = 0; // 첫 행 포커스
                grdPM.View.UnselectRow(beforeFocusCodeClass); // 기존 포커스행 선택 해제
                grdPM.View.SelectRow(0); //기존행 선택
                                         // focusedRowChanged(); //포커스된 class의 데이터 변경
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
            finally
            {
                grdPM.CloseWaitArea();
            }
        }

        /// <summary>
        /// 점검항목을 추가 할 때 PMList의 plant와 clasID를 입력해주는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            DataRow focusedRow = grdPM.View.GetFocusedDataRow();

            args.NewRow["PLANTID"] = focusedRow["PLANTID"].ToString();
            args.NewRow["ENTERPRISEID"] = focusedRow["ENTERPRISEID"].ToString();
            args.NewRow["MAINTITEMCLASSID"] = focusedRow["MAINTITEMCLASSID"].ToString();
        }

        /// <summary>
        /// 점검할목을 추가 할 때 PMList의 plant와 clasID를 입력해주는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRow2(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            DataTable dt = grdinspectionlist.DataSource as DataTable;
            int max = 0;
            
     
            


            if (dt == null)
            {
                args.NewRow["USERSEQUENCE"] = 1;
            }
            else
            {
                for(int i=0;i<dt.Rows.Count-1;i++)
     
            
                    if (max < Convert.ToInt32(dt.Rows[i]["USERSEQUENCE"].ToString()))
                    {
                        max = Convert.ToInt32(dt.Rows[i]["USERSEQUENCE"].ToString());
                    }

                
                max += 1;
                args.NewRow["USERSEQUENCE"] = max;
            }
        }



        #endregion

        #region 툴바

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();
            int index = smarttab.SelectedTabPageIndex;
            DataTable changed = null;
            switch (index)
            {
                case 0://입고 - 불출
                    changed = grdPMItem.GetChangedRows();
                    ExecuteRule("SavePMItemManagement", changed);
                    break;
                case 1:
                    changed = grdinspectionlist.GetChangedRows();
                    DataRow equipmentclass = grdequipmentclass.View.GetFocusedDataRow();
                   
                    foreach(DataRow dr in changed.Rows)
                    {
                        if (cboinspection.EditValue == null && dr["_STATE_"].ToString().Equals("added"))
                        {
                            throw MessageException.Create("CboClickPlease");
                        }
                            
                  
                    }

                    string ruleName = "SaveInspectionItemManagement";
                    string tableName = "list";
                    changed.TableName = tableName;

                    MessageWorker worker = new MessageWorker(ruleName);
                    worker.SetBody(new MessageBody()
                    {
                    { "maintitemid" , cboinspection.EditValue },
                    { "equipmentclass" ,equipmentclass["EQUIPMENTCLASSID"] },
                    { "enterpriseId", UserInfo.Current.Enterprise },
                    { "plantId", UserInfo.Current.Plant },
                    { tableName, changed }
                    });
                    worker.Execute();
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
            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);
            Dictionary<string, object> param = new Dictionary<string, object>();
            int index = smarttab.SelectedTabPageIndex;
            switch (index)
            {
                case 0://입고 - 불출
                    
                    values.Add("p_maintitemclassid", "");
                    DataTable classDt = await SqlExecuter.QueryAsync("SelectPMManagement", "10001", values);

                    if (classDt.Rows.Count < 1)
                    {
                        ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                        grdPMItem.View.ClearDatas();
                    }

                    grdPM.DataSource = classDt;

          
                    DataRow currentRow = grdPM.View.GetFocusedDataRow();
                    if (currentRow == null) return;
                    param.Add("p_maintitemclassid", currentRow["MAINTITEMCLASSID"].ToString());
                    param.Add("p_languagrtype", Framework.UserInfo.Current.LanguageType);

                    grdPMItem.DataSource = await SqlExecuter.QueryAsync("SelectPMItemManagement", "10001", values);
                    focusedRowChanged();
                    break;
                case 1:
					values.Add("HIERARCHY", "MainEquipment");
                    DataTable eqclass = await SqlExecuter.QueryAsync("GetEquipmentClassList", "10002", values);

                    if (eqclass.Rows.Count < 1)
                    {
                        ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                        grdPMItem.View.ClearDatas();
                    }
                    grdequipmentclass.DataSource = eqclass;

                    DataRow eqcurrentrow = grdequipmentclass.View.GetFocusedDataRow();
                    if (eqcurrentrow == null) return;
               
                    param.Add("EQUIPMENTCLASSID", eqcurrentrow["EQUIPMENTCLASSID"].ToString());
                    param.Add("MAINTITEMID", cboinspection.EditValue);

                    grdinspectionlist.DataSource = await SqlExecuter.QueryAsync("GetInspectionDetail", "10001", values);
                    focusedEqumentRowChanged();

                    break;
                    // TODO : 조회 SP 변경
            }
           
        }

        protected override void InitializeCondition()
        {
            base.InitializeCondition();
        }

        /// <summary>
        /// 공장 조회조건에 User Plant 정보 설정 및 수정불가
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            this.Conditions.GetControl<SmartTextBox>("P_PLANTID").EditValue = Framework.UserInfo.Current.Plant;
            this.Conditions.GetControl<SmartTextBox>("P_PLANTID").Enabled = false;
        }

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            // TODO : 유효성 로직 변경
            grdPMItem.View.CheckValidation();

            DataTable pmchanged = grdPMItem.GetChangedRows();
            DataTable inchanged = grdinspectionlist.GetChangedRows();
            if (pmchanged.Rows.Count == 0  && inchanged.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }

        #endregion

        #region Private Function

        /// <summary>
        /// PMList를 조회하는 함수
        /// </summary>
        private void LoadDataGridPM()
        {
            var values = Conditions.GetValues();
            values.Add("p_maintitemclassid", "");
            values.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);
            DataTable table = SqlExecuter.Query("SelectPMManagement", "10001", values);

            grdPM.DataSource = table;
        }

        /// <summary>
        /// PMList에 해당하는 점검 항목 조회하는 함수
        /// </summary>
        private void focusedRowChanged()
        {
            var row = grdPM.View.GetDataRow(grdPM.View.FocusedRowHandle);

            if (row==null) return;

            var param = Conditions.GetValues();
            
            param.Add("p_maintitemclassid", row["MAINTITEMCLASSID"].ToString());
            param.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);

            if (string.IsNullOrEmpty(row["MAINTITEMCLASSID"].ToString()))
            {
                ShowMessage("NoSelectData");
            }
            grdPMItem.DataSource = SqlExecuter.Query("SelectPMItemManagement", "10001", param);
        }

        /// <summary>
        /// Equmentclass에 해당하는 점검 항목 조회하는 함수
        /// </summary>
        private void focusedEqumentRowChanged()
        {
            var row = grdequipmentclass.View.GetDataRow(grdequipmentclass.View.FocusedRowHandle);

            if (row == null) return;

            var param = Conditions.GetValues();
            if (cboinspection.EditValue == null || cboinspection.EditValue.Equals(""))
            {
                grdinspectionlist.DataSource = null;
            }
            else
            {
                param.Add("EQUIPMENTCLASSID", row["EQUIPMENTCLASSID"].ToString());
                param.Add("MAINTITEMID", cboinspection.EditValue);
                param.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);


                if (string.IsNullOrEmpty(row["EQUIPMENTCLASSID"].ToString()))
                {
                    ShowMessage("NoSelectData");
                }
                DataTable dt = SqlExecuter.Query("GetInspectionDetail", "10001", param);
                grdinspectionlist.DataSource = dt;
            }
        }











        #endregion

        private void smartLabel2_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
