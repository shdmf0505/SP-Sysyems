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
using DevExpress.XtraEditors.Repository;
#endregion

namespace Micube.SmartMES.OutsideOrderMgnt
{
    /// <summary>
    /// 프 로 그 램 명  :  외주관리 > 외주 예산 관리 >외주비 예산 등록
    /// 업  무  설  명  :  외주비 예산 등록한다..
    /// 생    성    자  : choisstar
    /// 생    성    일  : 2019-06-24
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class OutsourcingBudgetEexpenses : SmartConditionManualBaseForm
    {
        #region Local Variables

        private string _strPlantid = ""; // plant 변경시 작업 
        DataTable dtPlantInfo = null;
        #endregion

        #region 생성자

        public OutsourcingBudgetEexpenses()
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
            // 작업용 plant 셋팅 (조회시 다시 셋팅)
            _strPlantid = UserInfo.Current.Plant.ToString();
            // TODO : 컨트롤 초기화 로직 구성
            InitializeGrid();
            // plant 정보 (외주비 예산통제여부,외주비 기타예산 통제여부 )
            OnPlantidInformationSearch();
        }

        /// <summary>        
        /// 코드그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdOutsourcingBudgetEexpenses.GridButtonItem = GridButtonItem.Export;
           
            grdOutsourcingBudgetEexpenses.View.AddTextBoxColumn("PLANTID", 200)
                .SetIsHidden();                                                          //  회사 ID
            grdOutsourcingBudgetEexpenses.View.AddTextBoxColumn("ENTERPRISEID", 200)
                .SetIsHidden();                                                           //  공장 ID
            grdOutsourcingBudgetEexpenses.View.AddDateEditColumn("BUDGETDATE", 100)
                .SetValidationIsRequired()
                .SetDisplayFormat("yyyy-MM-dd",MaskTypes.DateTime);                              //  예산일자
            grdOutsourcingBudgetEexpenses.View.AddSpinEditColumn("BUDGETAMOUNT", 100)
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0.##", MaskTypes.Numeric);                                     //예산금액
            //grdOutsourcingBudgetEexpenses.View.AddSpinEditColumn("ETCBUDGETAMOUNT", 100)
            //    .SetValidationIsRequired()
            //    .SetTextAlignment(TextAlignment.Right)
            //    .SetDisplayFormat("#,##0.##", MaskTypes.Numeric,false);                                     //기타예산금액
            grdOutsourcingBudgetEexpenses.View.AddSpinEditColumn("BUDGETAMOUNTSUM", 100)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsReadOnly();                                     //외주실적
            //grdOutsourcingBudgetEexpenses.View.AddSpinEditColumn("ETCBUDGETAMOUNTSUM", 100)
            //    .SetTextAlignment(TextAlignment.Right)
            //    .SetDisplayFormat("#,##0", MaskTypes.Numeric)
            //    .SetIsReadOnly();                                      //기타실적 
            grdOutsourcingBudgetEexpenses.View.AddSpinEditColumn("BUDGETAMOUNTREMAIN", 100)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsReadOnly();                                     //외주예산잔여금액
            //grdOutsourcingBudgetEexpenses.View.AddSpinEditColumn("ETCBUDGETAMOUNTREMAIN", 100)
            //    .SetTextAlignment(TextAlignment.Right)
            //    .SetDisplayFormat("#,##0", MaskTypes.Numeric)
            //    .SetIsReadOnly();                                     //기타예산잔여금액
          
            grdOutsourcingBudgetEexpenses.View.AddTextBoxColumn("DESCRIPTION", 200);

            grdOutsourcingBudgetEexpenses.View.AddTextBoxColumn("CREATORNAME", 100)
                .SetIsReadOnly();

            grdOutsourcingBudgetEexpenses.View.AddTextBoxColumn("CREATEDTIME", 150)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetDefault("")
                .SetIsReadOnly();
            grdOutsourcingBudgetEexpenses.View.AddTextBoxColumn("MODIFIERNAME", 100)
                .SetIsReadOnly();
            grdOutsourcingBudgetEexpenses.View.AddTextBoxColumn("MODIFIEDTIME", 150)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetDefault("")
                .SetIsReadOnly();
            //grdOutsourcingBudgetEexpenses.View.SetAutoFillColumn("DESCRIPTION");
            grdOutsourcingBudgetEexpenses.View.SetKeyColumn("PLANTID", "ENTERPRISEID","BUDGETDATE");

            grdOutsourcingBudgetEexpenses.View.PopulateColumns();

        }

        /// <summary>
        /// site 설정 
        /// </summary>
        private void InitializeConditionPopup_Plant()
        {
            var plantCbobox = Conditions.AddComboBox("p_plantid", new SqlQuery("GetPlantList", "00001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PLANTID", "PLANTID")
              .SetLabel("PLANT")
              .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false)
              .SetPosition(0.1)
              .SetDefault(UserInfo.Current.Plant, "p_plantId") //기본값 설정 UserInfo.Current.Plant
              .SetIsReadOnly(true);
        }

        /// <summary>
        /// lotid 조회조건
        /// </summary>
        private void InitializeCondition_Lotid()
        {
            var txtLotid = Conditions.AddTextBox("PROCESSSEGMENTID")
               .SetLabel("LOTID")
               .SetIsHidden()
               .SetPosition(2.4);
        }
       
        /// <s
        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            grdOutsourcingBudgetEexpenses.View.AddingNewRow += View_AddingNewRow;
            grdOutsourcingBudgetEexpenses.View.CellValueChanged += View_CellValueChanged;

        }

        /// <summary>
        /// 예산일자 그리드 포맷 변경 처리 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            
            if (e.Column.FieldName == "BUDGETDATE")
            {
                grdOutsourcingBudgetEexpenses.View.CellValueChanged -= View_CellValueChanged;

                DataRow row = grdOutsourcingBudgetEexpenses.View.GetFocusedDataRow();

                if (row["BUDGETDATE"].ToString().Equals(""))
                {
                    grdOutsourcingBudgetEexpenses.View.CellValueChanged += View_CellValueChanged;
                    return;
                }
                DateTime dateBudget = Convert.ToDateTime(row["BUDGETDATE"].ToString());
                grdOutsourcingBudgetEexpenses.View.SetFocusedRowCellValue("BUDGETDATE", dateBudget.ToString("yyyy-MM-dd"));// 예산일자
                grdOutsourcingBudgetEexpenses.View.CellValueChanged += View_CellValueChanged;
            }
            if (e.Column.FieldName== "BUDGETAMOUNT")
            {
                grdOutsourcingBudgetEexpenses.View.CellValueChanged -= View_CellValueChanged;
                if (e.Value == null)
                {
                    grdOutsourcingBudgetEexpenses.View.CellValueChanged += View_CellValueChanged;
                    return;
                }
                if (!(e.Value.Equals("")))
                {
                    DataRow row = grdOutsourcingBudgetEexpenses.View.GetFocusedDataRow();
                    string strBudgetamount = row["BUDGETAMOUNT"].ToString();
                    decimal decBudgetamount = (strBudgetamount.ToString().Equals("") ? 0 : Convert.ToDecimal(strBudgetamount)); //
                    string strBudgetamountsum = row["BUDGETAMOUNTSUM"].ToString();
                    decimal decBudgetamountsum = (strBudgetamountsum.ToString().Equals("") ? 0 : Convert.ToDecimal(strBudgetamountsum)); //
                    row["BUDGETAMOUNTREMAIN"] = decBudgetamount - decBudgetamountsum;
                }
                
               
                grdOutsourcingBudgetEexpenses.View.CellValueChanged += View_CellValueChanged;
             }
           
        }

        /// <summary>
        /// 코드그룹 리스트 그리드에서 추가 버튼 클릭 시 호출
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            
            // 화사id  ,site id 추가  "PLANTID", "ENTERPRISEID",
            DateTime dateNow = DateTime.Now;
            var values = Conditions.GetValues();
            string sPlantid = values["P_PLANTID"].ToString();
            grdOutsourcingBudgetEexpenses.View.SetFocusedRowCellValue("ENTERPRISEID", UserInfo.Current.Enterprise.ToString());// 회사코드
            grdOutsourcingBudgetEexpenses.View.SetFocusedRowCellValue("PLANTID", sPlantid);// plantid
            grdOutsourcingBudgetEexpenses.View.SetFocusedRowCellValue("BUDGETDATE", dateNow.ToString("yyyy-MM-dd"));// 예산일자
        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            //base.OnToolbarSaveClick();

            //string strIsospbudgetcontrol = "N";
            //string strIsospetcbudgetcontrol = "N";
            //bool blPlantinfor = true;
            //if (dtPlantInfo != null && dtPlantInfo.Rows.Count == 1)
            //{
            //    DataRow drPlant = dtPlantInfo.Rows[0];

            //    if (chkIsospbudgetcontrol.Checked == true)
            //    {
            //        strIsospbudgetcontrol = "Y";
            //    }
            //    if (chkIsospetcbudgetcontrol.Checked == true)
            //    {
            //        strIsospetcbudgetcontrol = "Y";
            //    }
            //    if (drPlant["ISOSPBUDGETCONTROL"].ToString().Equals(strIsospbudgetcontrol) && drPlant["ISOSPETCBUDGETCONTROL"].ToString().Equals(strIsospetcbudgetcontrol))
            //    {
            //        blPlantinfor = false;
            //    }
            //    if (blPlantinfor == true)
            //    {
            //        drPlant["ISOSPBUDGETCONTROL"] = strIsospbudgetcontrol;
            //        drPlant["ISOSPETCBUDGETCONTROL"] = strIsospetcbudgetcontrol;
            //        ExecuteRule("OutsourcingBudgetEexpensesPlant", dtPlantInfo);
            //    }
            //}
            //// TODO : 저장 Rule 변경
            //DataTable changed = grdOutsourcingBudgetEexpenses.GetChangedRows();
            //if (changed.Rows.Count > 0)
            //{
            //    ExecuteRule("OutsourcingBudgetEexpenses", changed);
            //}
            
        }
        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            //base.OnToolbarClick(sender, e);
            SmartButton btn = sender as SmartButton;

            if (btn.Name.ToString().Equals("Save"))
            {

                ProcSave(btn.Text);
            }
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

            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            //#region 기간 검색형 전환 처리 
            //if (!(values["P_BUDGETDATE_PERIODFR"].ToString().Equals("")))
            //{
            //    DateTime requestDateFr = Convert.ToDateTime(values["P_BUDGETDATE_PERIODFR"]);
            //    values.Remove("P_BUDGETDATE_PERIODFR");
            //    values.Add("P_BUDGETDATE_PERIODFR", string.Format("{0:yyyy-MM-dd}", requestDateFr));
            //}

            //if (!(values["P_BUDGETDATE_PERIODTO"].ToString().Equals("")))
            //{
            //    DateTime requestDateTo = Convert.ToDateTime(values["P_BUDGETDATE_PERIODTO"]);
            //    values.Remove("P_BUDGETDATE_PERIODTO");
            //    values.Add("P_BUDGETDATE_PERIODTO", string.Format("{0:yyyy-MM-dd}", requestDateTo));
            //}
            //#endregion
            if (values["P_BUDGETMONTH"] != null)
            {
                string stryearmonth = values["P_BUDGETMONTH"].ToString();
                if (!(stryearmonth.Equals("")))
                {
                    DateTime dtyearmonth = Convert.ToDateTime(stryearmonth);
                    values.Remove("P_BUDGETMONTH");
                    values.Add("P_BUDGETMONTH", dtyearmonth.ToString("yyyy-MM"));
                }
            }
            string sPlantid  = values["P_PLANTID"].ToString();
            values.Add("P_ENTERPRISEID", UserInfo.Current.Enterprise);
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable dtBudgetEexpenses = await SqlExecuter.QueryAsync("GetOutsourcingBudgetExpenses", "10001", values);
            if (dtBudgetEexpenses.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }
            _strPlantid = sPlantid;
            grdOutsourcingBudgetEexpenses.DataSource = dtBudgetEexpenses;
            // plant 정보 다시 가져오기 
            OnPlantidInformationSearch();
            // site 비교처리 함.(Plant 정보및 기타 정보다시 셋팅 하기)
           
           
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();
            //plant
            //InitializeConditionPopup_Plant();
            InitializeCondition_Yearmonth();
            //공정명 
            InitializeCondition_Lotid();

        }

        private void InitializeCondition_Yearmonth()
        {
            DateTime dateNow = DateTime.Now;
            string strym = dateNow.ToString("yyyy-MM");
            var YearmonthDT = Conditions.AddDateEdit("P_BUDGETMONTH")
               .SetLabel("BUDGETYM")
               .SetDisplayFormat("yyyy-MM")
               .SetPosition(3.2)
               .SetDefault(strym)
               .SetValidationIsRequired()
;
        }
        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();
            
           
        }

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();
            string strIsospbudgetcontrol = "N";
            string strIsospetcbudgetcontrol = "N";
            bool blPlantinfor = true; // site 정보 변경여부 
            // TODO : 유효성 로직 변경
            if (dtPlantInfo != null && dtPlantInfo.Rows.Count == 1)
            {
                DataRow drPlant = dtPlantInfo.Rows[0];
               
                if (chkIsospbudgetcontrol.Checked == true)
                {
                    strIsospbudgetcontrol = "Y";
                }
                if (chkIsospetcbudgetcontrol.Checked == true)
                {
                    strIsospetcbudgetcontrol = "Y";
                }
                if (drPlant["ISOSPBUDGETCONTROL"].ToString().Equals(strIsospbudgetcontrol) && drPlant["ISOSPETCBUDGETCONTROL"].ToString().Equals(strIsospetcbudgetcontrol))
                {
                    blPlantinfor = false;
                }
            }

            grdOutsourcingBudgetEexpenses.View.CheckValidation();

            DataTable changed = grdOutsourcingBudgetEexpenses.GetChangedRows();

            if (changed.Rows.Count == 0 &&  blPlantinfor == false)
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
        /// <summary>
        /// plant정보에서 외주비 예산통제여부 ,외주비기타예산통제여부  
        /// </summary>
        private void OnPlantidInformationSearch()
        {

            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add("P_PLANTID", _strPlantid);
            dicParam = Commons.CommonFunction.ConvertParameter(dicParam);
            dtPlantInfo = SqlExecuter.Query("GetPlantidInformatByOsp", "10001", dicParam);
           
            if (dtPlantInfo.Rows.Count == 1)
            {
                DataRow drPlant = dtPlantInfo.Rows[0];
                if (drPlant["ISOSPBUDGETCONTROL"].ToString().Equals("Y"))
                {
                    chkIsospbudgetcontrol.Checked = true;
                }
                else
                {
                    chkIsospbudgetcontrol.Checked = false;
                }
                if (drPlant["ISOSPETCBUDGETCONTROL"].ToString().Equals("Y"))
                {
                    chkIsospetcbudgetcontrol.Checked = true;
                }
                else
                {
                    chkIsospetcbudgetcontrol.Checked = false;
                }
            }
            else
            { 
                chkIsospbudgetcontrol.Checked = false;
                chkIsospetcbudgetcontrol.Checked = false;
                return;
            }
             
        }
        private void ProcSave(string strtitle)
        {

            grdOutsourcingBudgetEexpenses.View.FocusedRowHandle = grdOutsourcingBudgetEexpenses.View.FocusedRowHandle;
            grdOutsourcingBudgetEexpenses.View.FocusedColumn = grdOutsourcingBudgetEexpenses.View.Columns["BUDGETDATE"];
            grdOutsourcingBudgetEexpenses.View.ShowEditor();
            string strIsospbudgetcontrol = "N";
            string strIsospetcbudgetcontrol = "N";
            bool blPlantinfor = true; // site 정보 변경여부 
            // TODO : 유효성 로직 변경
            if (dtPlantInfo != null && dtPlantInfo.Rows.Count == 1)
            {
                DataRow drPlant = dtPlantInfo.Rows[0];

                if (chkIsospbudgetcontrol.Checked == true)
                {
                    strIsospbudgetcontrol = "Y";
                }
                if (chkIsospetcbudgetcontrol.Checked == true)
                {
                    strIsospetcbudgetcontrol = "Y";
                }
                if (drPlant["ISOSPBUDGETCONTROL"].ToString().Equals(strIsospbudgetcontrol) && drPlant["ISOSPETCBUDGETCONTROL"].ToString().Equals(strIsospetcbudgetcontrol))
                {
                    blPlantinfor = false;
                }
            }

            grdOutsourcingBudgetEexpenses.View.CheckValidation();

            DataTable changed = grdOutsourcingBudgetEexpenses.GetChangedRows();

            if (changed.Rows.Count == 0 && blPlantinfor == false)
            {
                // 저장할 데이터가 존재하지 않습니다.
                this.ShowMessage(MessageBoxButtons.OK, "NoSaveData");
                return;
            }
            
            DialogResult result = System.Windows.Forms.DialogResult.No;

            result = this.ShowMessage(MessageBoxButtons.YesNo, "OspDoProcessWant", strtitle);//저장하시겠습니까? 

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    this.ShowWaitArea();

                   
                    if (dtPlantInfo != null && dtPlantInfo.Rows.Count == 1)
                    {
                        DataRow drPlant = dtPlantInfo.Rows[0];

                        if (chkIsospbudgetcontrol.Checked == true)
                        {
                            strIsospbudgetcontrol = "Y";
                        }
                        if (chkIsospetcbudgetcontrol.Checked == true)
                        {
                            strIsospetcbudgetcontrol = "Y";
                        }
                        if (drPlant["ISOSPBUDGETCONTROL"].ToString().Equals(strIsospbudgetcontrol) && drPlant["ISOSPETCBUDGETCONTROL"].ToString().Equals(strIsospetcbudgetcontrol))
                        {
                            blPlantinfor = false;
                        }
                        if (blPlantinfor == true)
                        {
                            drPlant["ISOSPBUDGETCONTROL"] = strIsospbudgetcontrol;
                            drPlant["ISOSPETCBUDGETCONTROL"] = strIsospetcbudgetcontrol;
                            ExecuteRule("OutsourcingBudgetEexpensesPlant", dtPlantInfo);
                        }
                    }
                    // TODO : 저장 Rule 변경
                    DataTable dtSave = grdOutsourcingBudgetEexpenses.GetChangedRows();
                    if (dtSave.Rows.Count > 0)
                    {
                        ExecuteRule("OutsourcingBudgetEexpenses", dtSave);
                    }
                    ShowMessage("SuccessOspProcess");
                    //재조회 
                    OnSaveConfrimSearch();
                }
                catch (Exception ex)
                {
                    this.ShowError(ex);
                }
                finally
                {
                    this.CloseWaitArea();


                }
            }
        }

        private void OnSaveConfrimSearch()
        {

            // TODO : 조회 SP 변경
            var values = Conditions.GetValues();

            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            if (values["P_BUDGETMONTH"] != null)
            {
                string stryearmonth = values["P_BUDGETMONTH"].ToString();
                if (!(stryearmonth.Equals("")))
                {
                    DateTime dtyearmonth = Convert.ToDateTime(stryearmonth);
                    values.Remove("P_BUDGETMONTH");
                    values.Add("P_BUDGETMONTH", dtyearmonth.ToString("yyyy-MM"));
                }
            }
            string sPlantid = values["P_PLANTID"].ToString();
            values.Add("P_ENTERPRISEID", UserInfo.Current.Enterprise);

            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable dtBudgetEexpenses =  SqlExecuter.Query("GetOutsourcingBudgetExpenses", "10001", values);
            if (dtBudgetEexpenses.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }
            _strPlantid = sPlantid;
            grdOutsourcingBudgetEexpenses.DataSource = dtBudgetEexpenses;
            // plant 정보 다시 가져오기 
            OnPlantidInformationSearch();

        }
        #endregion
    }
}
