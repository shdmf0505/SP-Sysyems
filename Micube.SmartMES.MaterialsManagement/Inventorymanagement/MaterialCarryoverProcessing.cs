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

namespace Micube.SmartMES.MaterialsManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 자재관리 > 자재재고관리 > 자재 이월 처리
    /// 업  무  설  명  : 자재 이월 처리를 관리한다.
    /// 생    성    자  : 최별
    /// 생    성    일  : 2019-07-30
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class MaterialCarryoverProcessing : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가

        string _sWorkTime = "";

        #endregion

        #region 생성자

        public MaterialCarryoverProcessing()
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
            DateTime dateNow = DateTime.Now;
            dtpStartMonth.EditValue = dateNow.ToString("yyyy-MM");
            dtpEndMonth.EditValue = dateNow.AddMonths(1).ToString("yyyy-MM");
            InitializeComboBox();
            //WORKTIME 정보 가져오기 
            OnPlantidInformationSearch(cboPlantid.EditValue.ToString());
            // TODO : 컨트롤 초기화 로직 구성
            InitializeGrid();
        }
        #region ComboBox  

        /// <summary>
        /// SmartComboBox 컨트롤 추가
        /// </summary>
        private void InitializeComboBox()
        {
            // plantid
            cboPlantid.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboPlantid.ValueMember = "PLANTID";
            cboPlantid.DisplayMember = "PLANTID";
            cboPlantid.EditValue = UserInfo.Current.Plant.ToString();
            cboPlantid.DataSource = SqlExecuter.Query("GetPlantList", "00001"
                                                     , new Dictionary<string, object>() { { "ENTERPRISEID", UserInfo.Current.Enterprise }, { "LANGUAGETYPE", UserInfo.Current.LanguageType } });

            cboPlantid.ShowHeader = false;

            selectOspWarehouseidPopup(UserInfo.Current.Plant.ToString());


        }
        #region 작업장 combox
        /// <summary>
        /// 작업장 combox 
        /// </summary>
        /// <param name="sPlantid"></param>
        private void selectOspWarehouseidPopup(string sPlantid)
        {

            ConditionItemSelectPopup popup = new ConditionItemSelectPopup();
            popup.SetPopupLayoutForm(520, 600, FormBorderStyle.FixedDialog);
            popup.SetPopupLayout("WAREHOUSENAME", PopupButtonStyles.Ok_Cancel, true, false);
            popup.Id = "WAREHOUSENAME";
            popup.LabelText = "WAREHOUSENAME";
            popup.SearchQuery = new SqlQuery("GetWarehouseidListByCsm", "10001", $"P_ENTERPRISEID={UserInfo.Current.Enterprise}"
                                                                           , $"LANGUAGETYPE={UserInfo.Current.LanguageType}"
                                                                           , $"P_PLANTID={sPlantid}");
            popup.IsMultiGrid = false;

            popup.DisplayFieldName = "WAREHOUSENAME";
            popup.ValueFieldName = "WAREHOUSEID";
            popup.LanguageKey = "WAREHOUSEID";

            popup.Conditions.AddTextBox("P_WAREHOUSENAME")
                .SetLabel("WAREHOUSENAME");
            popup.GridColumns.AddTextBoxColumn("WAREHOUSEID", 120)
                .SetLabel("WAREHOUSEID");
            popup.GridColumns.AddTextBoxColumn("WAREHOUSENAME", 200)
                .SetLabel("WAREHOUSENAME");

            popupOspWarehouseid.SelectPopupCondition = popup;
        }
        #endregion

        #endregion

        /// <summary>        
        /// 코드그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
           
        }

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            cboPlantid.EditValueChanged += CboPlantid_EditValueChanged;
            dtpStartMonth.EditValueChanged += dtpStartMonth_EditValueChanged;
        }
        /// <summary>
        /// Plantid 변경 따른 Areaid 변경처리 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboPlantid_EditValueChanged(object sender, EventArgs e)
        {

            selectOspWarehouseidPopup(cboPlantid.EditValue.ToString());

        }

        private void dtpStartMonth_EditValueChanged(object sender, EventArgs e)
        {
            if (dtpStartMonth.Text.ToString().Equals(""))
            {
                dtpEndMonth.Text = "";
            }
            else
            {
                DateTime dtStart = Convert.ToDateTime(dtpStartMonth.EditValue.ToString());
                dtpEndMonth.Text = dtStart.AddMonths(1).ToString("yyyy-MM");
            }
            // selectOspWarehouseidPopup(cboPlantid.EditValue.ToString());

        }
        /// <summary>
        /// 코드그룹 리스트 그리드에서 추가 버튼 클릭 시 호출
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
           // throw new NotImplementedException();
        }

        #endregion

        #region 툴바
        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            //base.OnToolbarClick(sender, e);
            SmartButton btn = sender as SmartButton;

            if (btn.Name.ToString().Equals("Carryprocess"))
            {

                ProcSave(btn.Text);
            }
        }
        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            //base.OnToolbarSaveClick();

            //DataTable dtSave = createSaveDatatable();
            //DateTime dtStart = Convert.ToDateTime(dtpStartMonth.EditValue.ToString());
            //DateTime dtEnd = Convert.ToDateTime(dtpEndMonth.EditValue.ToString());
            //string strDateMonthFormat = "";
            //strDateMonthFormat = "yyyy-MM-01 " + _sWorkTime;
            //int diffmonth = dtEnd.Month - dtStart.Month + 12 * (dtEnd.Year - dtStart.Year) +1 ;
            //for (int i = 0; i < diffmonth; i++)
            //{
            //    DataRow dr = dtSave.NewRow();
            //    DateTime dtworktime = dtStart.AddMonths(i);

            //    dr["ENTERPRISEID"] = UserInfo.Current.Enterprise.ToString();
            //    dr["PLANTID"] = cboPlantid.EditValue.ToString();
            //    dr["AREAID"] = popupOspAreaid.GetValue().ToString();
            //    dr["PREYEARMONTH"] = dtworktime.AddMonths(-1).ToString("yyyy-MM");
            //    dr["YEARMONTH"] = dtworktime.ToString("yyyy-MM");
            //    dr["TRANSACTIONSTARTDATE"] = dtworktime.ToString(strDateMonthFormat);
            //    dr["TRANSACTIONENDDATE"] = dtworktime.AddMonths(1).ToString(strDateMonthFormat);
            //    dr["USERID"] = UserInfo.Current.Id.ToString();

            //    dr["_STATE_"] = "added";


            //    dtSave.Rows.Add(dr);
            //}
           
            //// TODO : 저장 Rule 변경
           

            //ExecuteRule("MaterialCarryoverProcessing", dtSave);
        }

        #endregion

        #region 검색

        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

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
        }

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();
            if (dtpStartMonth.Text.ToString().Equals(""))
            {
                throw MessageException.Create("InValidOspRequiredField", lbMonth.Text); //메세지 
               
            }
            if (dtpEndMonth.Text.ToString().Equals(""))
            {
                throw MessageException.Create("InValidOspRequiredField", lbMonth.Text); //메세지 
              
            }
            DateTime dtStart = Convert.ToDateTime(dtpStartMonth.EditValue.ToString());
            DateTime dtEnd = Convert.ToDateTime(dtpEndMonth.EditValue.ToString());

            int diffmonth = dtEnd.Month - dtStart.Month + 12 * (dtEnd.Year - dtStart.Year) + 1;

            if (diffmonth <= 0)
            {
                // 다국어 처리  기간정보가 올바르지 않습니다. 
                throw MessageException.Create("InValidCsmData006");
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
        /// 저장시 기본테이블 생성
        /// </summary>
        /// <returns></returns>
        private DataTable createSaveDatatable()
        {
            DataTable dt = new DataTable();
            dt.TableName = "list";
            dt.Columns.Add("ENTERPRISEID");
            dt.Columns.Add("PLANTID");
            dt.Columns.Add("WAREHOUSEID");
            dt.Columns.Add("PREYEARMONTH");
            dt.Columns.Add("YEARMONTH");
            dt.Columns.Add("TRANSACTIONSTARTDATE");
            dt.Columns.Add("TRANSACTIONENDDATE");
            dt.Columns.Add("USERID");

            dt.Columns.Add("_STATE_");
            return dt;
        }

        /// <summary>
        /// plant정보에서 WORKTIME 
        /// </summary>
        private void OnPlantidInformationSearch(string sPlantid)
        {

            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add("P_PLANTID", sPlantid);
            DataTable dtPlantInfo = SqlExecuter.Query("GetPlantidInformatByCsm", "10001", dicParam);

            if (dtPlantInfo.Rows.Count == 1)
            {
                DataRow drPlant = dtPlantInfo.Rows[0];
                _sWorkTime = drPlant["WORKTIME"].ToString();
            }
            else
            {

                return;
            }

        }

        private void ProcSave(string strtitle)
        {
            if (dtpStartMonth.Text.ToString().Equals(""))
            {
                this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lbMonth.Text); //메세지 
                return;

            }
            //if (dtpEndMonth.Text.ToString().Equals(""))
            //{
            //    this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lbMonth.Text); //메세지 
            //    return;

            //}
            DateTime dtStart = Convert.ToDateTime(dtpStartMonth.EditValue.ToString());
            //DateTime dtEnd = Convert.ToDateTime(dtpEndMonth.EditValue.ToString());

            //int diffmonth = dtEnd.Month - dtStart.Month + 12 * (dtEnd.Year - dtStart.Year) + 1;

            //if (diffmonth <= 0)
            //{
            //    // 다국어 처리  기간정보가 올바르지 않습니다. 
            //    this.ShowMessage(MessageBoxButtons.OK, "InValidCsmData006");
            //    return;
            //}

            
            DialogResult result = System.Windows.Forms.DialogResult.No;

            result = this.ShowMessage(MessageBoxButtons.YesNo, "OspDoProcessWant", strtitle);//저장하시겠습니까? 

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    this.ShowWaitArea();
                    DataTable dtSave = createSaveDatatable();
                     dtStart = Convert.ToDateTime(dtpStartMonth.EditValue.ToString());
                   //  dtEnd = Convert.ToDateTime(dtpEndMonth.EditValue.ToString());
                    string strDateMonthFormat = "";
                    strDateMonthFormat = "yyyy-MM-01 " + _sWorkTime;
                   //  diffmonth = dtEnd.Month - dtStart.Month + 12 * (dtEnd.Year - dtStart.Year) + 1;
                   // for (int i = 0; i < diffmonth; i++)
                    {
                        DataRow dr = dtSave.NewRow();
                        DateTime dtworktime = dtStart.AddMonths(0);

                        dr["ENTERPRISEID"] = UserInfo.Current.Enterprise.ToString();
                        dr["PLANTID"] = cboPlantid.EditValue.ToString();
                        dr["WAREHOUSEID"] = popupOspWarehouseid.GetValue().ToString();
                        dr["PREYEARMONTH"] = dtworktime.ToString("yyyy-MM");
                        dr["YEARMONTH"] = dtworktime.AddMonths(1).ToString("yyyy-MM");
                        dr["TRANSACTIONSTARTDATE"] = dtworktime.ToString(strDateMonthFormat);
                        dr["TRANSACTIONENDDATE"] = dtworktime.AddMonths(1).ToString(strDateMonthFormat);
                        dr["USERID"] = UserInfo.Current.Id.ToString();

                        dr["_STATE_"] = "added";


                        dtSave.Rows.Add(dr);
                    }

                    // TODO : 저장 Rule 변경

                    MessageWorker worker = new MessageWorker("MaterialCarryoverProcessing");
                    worker.SetBody(new MessageBody()
                        {
                            { "list", dtSave }
                        });
                    worker.ExecuteWithTimeout(300);
                   // ExecuteRule("MaterialCarryoverProcessing", dtSave);

                    ShowMessage("SuccessOspProcess");
                    
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
        #endregion
    }
}
