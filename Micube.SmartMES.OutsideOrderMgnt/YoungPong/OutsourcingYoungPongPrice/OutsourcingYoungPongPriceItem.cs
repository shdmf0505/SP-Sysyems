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

namespace Micube.SmartMES.OutsideOrderMgnt
{
    /// <summary>
    /// 프 로 그 램 명  : 외주관리> 외주 단가 관리>외주단가항목관리
    /// 업  무  설  명  : 외주단가항목관리
    /// 생    성    자  : CHOISSTAR
    /// 생    성    일  : 2020-02-11
    /// 수  정  이  력  : 
    /// 
    /// 
    /// 
    /// </summary>
    public partial class OutsourcingYoungPongPriceItem : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가
        string _Text;                                       // 설명
        SmartBandedGrid grdList = new SmartBandedGrid();    // Main Grid

        #endregion

        #region 생성자

        public OutsourcingYoungPongPriceItem()
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
        /// 코드그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            
            grdMaster.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export | GridButtonItem.Expand | GridButtonItem.Restore;
            
            grdMaster.View.AddTextBoxColumn("PLANTID", 200)
                .SetIsHidden();                                                          //  회사 ID
            grdMaster.View.AddTextBoxColumn("ENTERPRISEID", 200)
                .SetIsHidden();                                                           //  공장 ID
            grdMaster.View.AddTextBoxColumn("PRICEITEMID", 100)
                .SetValidationIsRequired();
            grdMaster.View.AddLanguageColumn("PRICEITEMNAME", 150);
            
            grdMaster.View.AddComboBoxColumn("COMPONENTTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ComponentType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                //.SetEmptyItem("ComboBox")
                ;
            grdMaster.View.AddComboBoxColumn("DATASETTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OSPDataSetType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetEmptyItem("", "");
            
            grdMaster.View.AddTextBoxColumn("DATASET", 150);
            grdMaster.View.AddTextBoxColumn("FORMATMASK", 100);
            grdMaster.View.AddTextBoxColumn("DESCRIPTION", 200);
            grdMaster.View.AddTextBoxColumn("PRICEITEMIDUSECNT", 200)
                 .SetIsHidden();
            grdMaster.View.AddComboBoxColumn("VALIDSTATE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Valid");

            grdMaster.View.SetKeyColumn("PRICEITEMID");
            grdMaster.View.PopulateColumns();
        }

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            grdMaster.View.AddingNewRow += View_AddingNewRow;
            grdMaster.View.ShowingEditor += View_ShowingEditor;
            grdMaster.ToolbarDeleteRow += GrdMaster_ToolbarDeleteRow;
        }

        private void GrdMaster_ToolbarDeleteRow(object sender, EventArgs e)
        {

            DataRow row = grdMaster.View.GetFocusedDataRow();
            if (row == null)
            {
                return;
            }
            if (row.RowState != DataRowState.Added)
            {
                string strPriceitemidusecnt = "";
                if (grdMaster.View.GetFocusedRowCellValue("PRICEITEMIDUSECNT") == null)
                {
                    strPriceitemidusecnt = "";
                }
                else
                {
                    strPriceitemidusecnt = grdMaster.View.GetFocusedRowCellValue("PRICEITEMIDUSECNT").ToString();
                }
                //string strPriceitemidusecnt = grdMaster.View.GetFocusedRowCellValue("PRICEITEMIDUSECNT").ToString();
                decimal decPriceitemidusecnt = (strPriceitemidusecnt.ToString().Equals("") ? 0 : Convert.ToDecimal(strPriceitemidusecnt)); //

                if (decPriceitemidusecnt == 0)
                {
                    row["VALIDSTATE"]= "Invalid";
                    (grdMaster.View.DataSource as DataView).Table.AcceptChanges();
                }
                
            }

        }
        /// <summary>
        /// 코드그룹 리스트 그리드에서 추가 버튼 클릭 시 호출
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            grdMaster.View.SetFocusedRowCellValue("ENTERPRISEID", UserInfo.Current.Enterprise.ToString());// 회사코드
            grdMaster.View.SetFocusedRowCellValue("PLANTID", UserInfo.Current.Plant.ToString());//  plantid
            grdMaster.View.SetFocusedRowCellValue("COMPONENTTYPE", "ComboBox");
            grdMaster.View.SetFocusedRowCellValue("PRICEITEMIDUSECNT", "0");
            grdMaster.View.SetFocusedRowCellValue("VALIDSTATE", "Valid");
        }
        private void View_ShowingEditor(object sender, CancelEventArgs e)
        {
            string strPriceitemidusecnt = grdMaster.View.GetFocusedRowCellValue("PRICEITEMIDUSECNT").ToString();
            decimal decPriceitemidusecnt = (strPriceitemidusecnt.ToString().Equals("") ? 0 : Convert.ToDecimal(strPriceitemidusecnt)); //
          
            if (decPriceitemidusecnt >0)
            {
                if (grdMaster.View.FocusedColumn.FieldName.Equals("DATASETTYPE") || grdMaster.View.FocusedColumn.FieldName.Equals("DATASET")
                    || grdMaster.View.FocusedColumn.FieldName.Equals("FORMATMASK"))
                {
                    e.Cancel = true;
                }
            }
            
        }
        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
          
            //base.OnToolbarSaveClick();

            //// TODO : 저장 Rule 변경
            //DataTable changed = grdSpecial.GetChangedRows();

            //ExecuteRule("OutsourcedSpecialUnitPrice", changed);
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

            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable dtPriceItem = await SqlExecuter.QueryAsync("GetOutsourcingYoungPongPriceItem", "10001", values);
           

            if (dtPriceItem.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }
            grdMaster.DataSource = dtPriceItem;
          
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

            // TODO : 유효성 로직 변경
            grdList.View.CheckValidation();

            DataTable changed = grdList.GetChangedRows();

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
        private void ProcSave(string strtitle)
        {
            grdMaster.View.FocusedRowHandle = grdMaster.View.FocusedRowHandle;
            grdMaster.View.FocusedColumn = grdMaster.View.Columns["PRICEITEMID"];
            grdMaster.View.ShowEditor();

            grdMaster.View.CheckValidation();
           
            DataTable changed = grdMaster.GetChangedRows();


            if (changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                this.ShowMessage(MessageBoxButtons.OK, "NoSaveData");
                return;
            }
            //PRICEITEMID(중복값 체크)

            DialogResult result = System.Windows.Forms.DialogResult.No;

            result = this.ShowMessage(MessageBoxButtons.YesNo, "OspDoProcessWant", strtitle);//저장하시겠습니까? 

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    this.ShowWaitArea();
                    DataTable dtSave = grdMaster.GetChangedRows();

                    ExecuteRule("OutsourcingYoungPongPriceItem", dtSave);

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

          
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable dtPriceItem = SqlExecuter.Query("GetOutsourcingYoungPongPriceItem", "10001", values);

            if (dtPriceItem.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdMaster.DataSource = dtPriceItem;

        }
        #endregion
    }
}
