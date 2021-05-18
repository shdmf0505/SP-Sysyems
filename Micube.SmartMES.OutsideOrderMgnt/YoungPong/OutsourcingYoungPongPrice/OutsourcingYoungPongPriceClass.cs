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
    /// 프 로 그 램 명  :  외주관리> 외주 단가 관리>외주단가분류관리
    /// 업  무  설  명  : 외주단가분류관리
    /// 생    성    자  : CHOISSTAR
    /// 생    성    일  : 2020-02-11
    /// 수  정  이  력  : 
    /// 
    /// 
    /// 
    /// </summary>
    public partial class OutsourcingYoungPongPriceClass : SmartConditionManualBaseForm
    {
        #region Local Variables

        #endregion

        #region 생성자

        public OutsourcingYoungPongPriceClass()
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
            InitializeGrid_Master();
            InitializeGrid_Detail();
            
        }
        /// <summary>        
        /// 코드그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid_Master()
        {
            grdMaster.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export | GridButtonItem.Expand | GridButtonItem.Restore;
           // grdMaster.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdMaster.View.AddTextBoxColumn("PLANTID", 200)
                .SetIsHidden();                                                          //  회사 ID
            grdMaster.View.AddTextBoxColumn("ENTERPRISEID", 200)
                .SetIsHidden();                                                           //  공장 ID
            grdMaster.View.AddTextBoxColumn("PRICECLASSID", 100)
                .SetValidationIsRequired();
            grdMaster.View.AddTextBoxColumn("PRICECLASSNAME", 150);

            grdMaster.View.AddTextBoxColumn("DESCRIPTION", 200);
            grdMaster.View.AddTextBoxColumn("PRICECLASSIDCNT", 200)
                 .SetIsHidden();
            grdMaster.View.AddComboBoxColumn("VALIDSTATE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Valid")
                .SetIsReadOnly();
            grdMaster.View.SetKeyColumn("PLANTID", "PRICECLASSID");
            grdMaster.View.PopulateColumns();
        }
        /// <summary>        
        /// 코드그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid_Detail()
        {

            grdDetail.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export | GridButtonItem.Expand | GridButtonItem.Restore;
            grdDetail.View.AddTextBoxColumn("PLANTID", 200)
                .SetIsHidden();                                                          //  회사 ID
            grdDetail.View.AddTextBoxColumn("ENTERPRISEID", 200)
                .SetIsHidden();                                                           //  공장 ID
            grdDetail.View.AddTextBoxColumn("PRICECLASSID", 100)
                .SetIsHidden();
            //팝업 추가 처리 
            InitializeGrid_Priceitemid();
            grdDetail.View.AddTextBoxColumn("PRICEITEMNAME", 150)
                .SetIsReadOnly();
            grdDetail.View.AddSpinEditColumn("PRIORITY", 100)
               .SetTextAlignment(TextAlignment.Right)
               .SetDisplayFormat("#,##0", MaskTypes.Numeric)
               .SetValidationIsRequired();
            grdDetail.View.AddComboBoxColumn("ISRANGE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
              .SetDefault("N");
            grdDetail.View.AddComboBoxColumn("RANGEUNIT",100, new SqlQuery("GetUomDefinitionMapListByOsp", "10001", "UOMCATEGORY=OSPRANGE", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "UOMDEFID", "UOMDEFID")
                .SetEmptyItem("","")
                .SetDefault("");
            grdDetail.View.AddComboBoxColumn("VALIDSTATE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
               .SetDefault("Valid")
               ;

            grdDetail.View.AddTextBoxColumn("DESCRIPTION", 200);
            grdDetail.View.SetKeyColumn("PLANTID", "PRICECLASSID", "PRICEITEMID");
            grdDetail.View.PopulateColumns();
        }


        /// <summary>
        /// grid Priceitemid 가져오기 
        /// </summary>
        private void InitializeGrid_Priceitemid()
        {
            var popupGridConsumabledefid = this.grdDetail.View.AddSelectPopupColumn("PRICEITEMID", 120, new SqlQuery("GetPriceitemidByOsp", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
                .SetPopupLayout("PRICEITEMPOPUP", PopupButtonStyles.Ok_Cancel, true, false)
                // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
                .SetPopupResultCount(1)
                .SetNotUseMultiColumnPaste()
                // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
               
                // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
                .SetPopupLayoutForm(700, 600)
                // 그리드의 남은 영역을 채울 컬럼 설정
                .SetValidationIsRequired()
                //.SetPopupAutoFillColumns("CONSUMABLEDEFNAME")

                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {

                    int irow = 0;
                    int crow = 0;
                    DataRow classRow = grdDetail.View.GetFocusedDataRow();
                    crow = grdDetail.View.FocusedRowHandle;

                    foreach (DataRow row in selectedRows)
                    {
                        if (irow == 0)
                        {
                            string sPriceitemid = row["PRICEITEMID"].ToString();
                         

                            int icheck = checkPriceitemid(sPriceitemid, crow);
                            if (icheck == -1)
                            {
                                classRow["PRICEITEMID"] = row["PRICEITEMID"];
                                classRow["PRICEITEMNAME"] = row["PRICEITEMNAME"];

                                grdDetail.View.RaiseValidateRow(crow);
                            }
                            else
                            {
                                classRow["PRICEITEMID"] = "";
                                classRow["PRICEITEMNAME"] = "";
                                irow = irow - 1;
                            }
                        }
                       
                        irow = irow + 1;
                    }
                })

            ;

            // 팝업 조회조건


            popupGridConsumabledefid.Conditions.AddTextBox("P_PRICEITEMNAME")
               .SetLabel("PRICEITEMNAME");
  
            popupGridConsumabledefid.GridColumns.AddTextBoxColumn("PRICEITEMID", 80).SetIsReadOnly();
            popupGridConsumabledefid.GridColumns.AddTextBoxColumn("PRICEITEMNAME",150).SetIsReadOnly();
            popupGridConsumabledefid.GridColumns.AddComboBoxColumn("DATASETTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OSPDataSetType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
               .SetIsReadOnly();
            popupGridConsumabledefid.GridColumns.AddTextBoxColumn("DATASET", 150).SetIsReadOnly();
        


        }
        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            grdMaster.View.AddingNewRow += GrdMaster_AddingNewRow;
            grdDetail.View.AddingNewRow += GrdDetail_AddingNewRow;
            //g
            grdMaster.View.FocusedRowChanged += GrdMaster_FocusedRowChanged;
            grdMaster.View.ShowingEditor += GrdMaster_ShowingEditor;
            grdDetail.View.ShowingEditor += GrdDetail_ShowingEditor;
            //
            grdMaster.ToolbarDeleteRow += GrdMaster_ToolbarDeleteRow;
            grdDetail.ToolbarDeleteRow += GrdDetail_ToolbarDeleteRow;
            

        }
        /// <summary>
        /// GrdMaster_ToolbarDeleteRow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdMaster_ToolbarDeleteRow(object sender, EventArgs e)
        {

            DataRow row = grdMaster.View.GetFocusedDataRow();
            if (row == null)
            {
                return;
            }
            if (row.RowState != DataRowState.Added)
            {
                string strPriceitemidusecnt ="";
                if (grdMaster.View.GetFocusedRowCellValue("PRICECLASSIDCNT")==null)
                {
                    strPriceitemidusecnt = "";
                }
                else
                {
                    strPriceitemidusecnt = grdMaster.View.GetFocusedRowCellValue("PRICECLASSIDCNT").ToString();
                }
                decimal decPriceitemidusecnt = (strPriceitemidusecnt.ToString().Equals("") ? 0 : Convert.ToDecimal(strPriceitemidusecnt)); //

                if (decPriceitemidusecnt == 0)
                {
                    row["VALIDSTATE"] = "Invalid";
                    (grdMaster.View.DataSource as DataView).Table.AcceptChanges();
                }

            }

        }
        /// <summary>
        /// GrdMaster_ToolbarDeleteRow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdDetail_ToolbarDeleteRow(object sender, EventArgs e)
        {

            DataRow row = grdDetail.View.GetFocusedDataRow();
            if (row ==null)
            {
                return;
            }
            if (row.RowState != DataRowState.Added)
            {
                
                string strPriceitemidusecnt = "";
                if (grdMaster.View.GetFocusedRowCellValue("PRICECLASSIDCNT") == null)
                {
                    strPriceitemidusecnt = "";
                }
                else
                {
                    strPriceitemidusecnt = grdMaster.View.GetFocusedRowCellValue("PRICECLASSIDCNT").ToString();
                }
                decimal decPriceitemidusecnt = (strPriceitemidusecnt.ToString().Equals("") ? 0 : Convert.ToDecimal(strPriceitemidusecnt)); //

                if (decPriceitemidusecnt == 0)
                {
                    row["VALIDSTATE"] = "Invalid";
                    (grdDetail.View.DataSource as DataView).Table.AcceptChanges();
                }

            }

        }
        /// <summary>
        /// 코드그룹 리스트 그리드에서 추가 버튼 클릭 시 호출
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void GrdMaster_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            //기
            // 
            var values = Conditions.GetValues();
            string strPlantid = values["P_PLANTID"].ToString();
            grdMaster.View.SetFocusedRowCellValue("ENTERPRISEID", UserInfo.Current.Enterprise.ToString());// 회사코드
            grdMaster.View.SetFocusedRowCellValue("PLANTID", strPlantid);// plantid
            grdMaster.View.SetFocusedRowCellValue("VALIDSTATE", "Valid");
        }
        /// <summary>
        /// 코드그룹 리스트 그리드에서 추가 버튼 클릭 시 호출
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void GrdDetail_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            if (grdMaster.View.DataRowCount==0)
            {
                int intfocusRow = grdDetail.View.FocusedRowHandle;
                grdDetail.View.DeleteRow(intfocusRow);
                return;
            }
            DataTable changed = grdMaster.GetChangedRows();
            if (changed.Rows.Count != 0)
            {
                int intfocusRow = grdDetail.View.FocusedRowHandle;
                grdDetail.View.DeleteRow(intfocusRow);
                return;
            }
            string strPriceclassidusecnt = grdMaster.View.GetFocusedRowCellValue("PRICECLASSIDUSECNT").ToString();
            decimal decPriceclassidusecnt = (strPriceclassidusecnt.ToString().Equals("") ? 0 : Convert.ToDecimal(strPriceclassidusecnt)); //

            if (decPriceclassidusecnt > 0)
            {
                int intfocusRow = grdDetail.View.FocusedRowHandle;
                grdDetail.View.DeleteRow(intfocusRow);
                return;
            }

            grdDetail.View.SetFocusedRowCellValue("ENTERPRISEID", UserInfo.Current.Enterprise.ToString());// 회사코드
            grdDetail.View.SetFocusedRowCellValue("PLANTID", grdMaster.View.GetFocusedRowCellValue("PLANTID"));// plantid
            grdDetail.View.SetFocusedRowCellValue("PRICECLASSID", grdMaster.View.GetFocusedRowCellValue("PRICECLASSID"));// 
            grdDetail.View.SetFocusedRowCellValue("ISRANGE", "N");// 
            grdDetail.View.SetFocusedRowCellValue("VALIDSTATE", "Valid");// 
        }

        private void GrdMaster_ShowingEditor(object sender, CancelEventArgs e)
        {
            //string strPriceclassidusecnt = grdMaster.View.GetFocusedRowCellValue("PRICECLASSIDUSECNT").ToString();
            //decimal decPriceclassidusecnt = (strPriceclassidusecnt.ToString().Equals("") ? 0 : Convert.ToDecimal(strPriceclassidusecnt)); //

            //if (decPriceclassidusecnt > 0)
            //{
                
            //        e.Cancel = true;
                
            //}

        }
        private void GrdDetail_ShowingEditor(object sender, CancelEventArgs e)
        {
            DataTable changed = grdMaster.GetChangedRows();

            if (changed.Rows.Count != 0)
            {
                e.Cancel = true;
            }

        }
        /// <summary>
        /// 단가코드 리스트 그리드의 포커스 행 변경 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdMaster_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            focusedRowChangedClass();
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
            //DataTable changed = grdList.GetChangedRows();

            //ExecuteRule("SaveCodeClass", changed);
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
            DataTable dtPriceItem = await SqlExecuter.QueryAsync("GetOutsourcingYoungPongPriceClass", "10001", values);


            if (dtPriceItem.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                grdDetail.View.ClearDatas();
            }
            grdMaster.DataSource = dtPriceItem;
            if (dtPriceItem.Rows.Count > 0)
            {
                grdMaster.View.FocusedRowHandle = 0;
                grdMaster.View.SelectRow(0);
                focusedRowChangedClass();
            }
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
        /// 그리드의 포커스 행 변경시 행의 작업목록의 내용을 매핑처리 함.
        /// </summary>
        private void focusedRowChangedClass()
        {
            //포커스 행 체크 
            if (grdMaster.View.FocusedRowHandle < 0) return;

            //단가코드 정보 가져오기 
            Dictionary<string, object> Param = new Dictionary<string, object>();

            Param.Add("P_PLANTID", grdMaster.View.GetFocusedRowCellValue("PLANTID"));
            Param.Add("P_PRICECLASSID", grdMaster.View.GetFocusedRowCellValue("PRICECLASSID"));
            Param.Add("P_VALIDSTATE", "Valid");
            Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            Param = Commons.CommonFunction.ConvertParameter(Param);
            DataTable dtDetail = SqlExecuter.Query("GetOutsourcingYoungPongPriceClassDetail", "10001", Param);

            if (dtDetail.Rows.Count < 1)
            {
                //단가 그리드 clear
                DataTable dtPrice = (grdDetail.DataSource as DataTable).Clone();
                grdDetail.DataSource = dtPrice;
            }
            else
            {
                grdDetail.DataSource = dtDetail;
            }
            
        }


        private void ProcSave(string strtitle)
        {
            grdMaster.View.FocusedRowHandle = grdMaster.View.FocusedRowHandle;
            grdMaster.View.FocusedColumn = grdMaster.View.Columns["PRICECLASSID"];
            grdMaster.View.ShowEditor();

            grdMaster.View.CheckValidation();

            DataTable changed = grdMaster.GetChangedRows();

            grdDetail.View.FocusedRowHandle = grdDetail.View.FocusedRowHandle;
            grdDetail.View.FocusedColumn = grdDetail.View.Columns["PRICEITEMNAME"];
            grdDetail.View.ShowEditor();

            grdDetail.View.CheckValidation();

            DataTable changedDetail = grdDetail.GetChangedRows();

            if (changed.Rows.Count == 0 && changedDetail.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                this.ShowMessage(MessageBoxButtons.OK, "NoSaveData");
                return;
            }
            //PRICEITEMID(중복값 체크)
            //우선 순위 중복값 체크 
            if (CheckPriceDateKeyColumns() == false)
            {

                return;
            }
            string strPriceclassid = grdMaster.View.GetFocusedRowCellValue("PRICECLASSID").ToString();
            DialogResult result = System.Windows.Forms.DialogResult.No;

            result = this.ShowMessage(MessageBoxButtons.YesNo, "OspDoProcessWant", strtitle);//저장하시겠습니까? 

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    this.ShowWaitArea();
                    DataTable dtSaveMaster = grdMaster.GetChangedRows();
                    dtSaveMaster.TableName = "listMain";
                    DataTable dtSaveDetail = grdDetail.GetChangedRows();
                    dtSaveDetail.TableName = "listDetail";
                    DataSet dsSave = new DataSet();
                    dsSave.Tables.Add(dtSaveMaster);
                    dsSave.Tables.Add(dtSaveDetail);
                    ExecuteRule("OutsourcingYoungPongPriceClass", dsSave);

                    ShowMessage("SuccessOspProcess");
                    //재조회 
                    OnSaveConfrimSearch();
                    if (!(strPriceclassid.Equals("")))
                    {
                        int irow = GetGridRowSearch(strPriceclassid);
                        if (irow >= 0)
                        {
                            grdMaster.View.FocusedRowHandle = irow;
                            grdMaster.View.SelectRow(irow);
                            focusedRowChangedClass();
                        }
                        else
                        {
                            grdMaster.View.FocusedRowHandle = 0;
                            grdMaster.View.SelectRow(0);
                            focusedRowChangedClass();
                        }
                        
                    }
                    //이동 처리 ..
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
            DataTable dtPriceItem = SqlExecuter.Query("GetOutsourcingYoungPongPriceClass", "10001", values);

            if (dtPriceItem.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdMaster.DataSource = dtPriceItem;

        }
        /// <summary>
        /// 그리드 이동에 필요한 row 찾기
        /// </summary>
        /// <param name="strRequestno"></param>
        private int GetGridRowSearch(string strPriceclassid)
        {
            int iRow = -1;
            if (grdMaster.View.DataRowCount == 0)
            {
                return iRow;
            }
            for (int i = 0; i < grdMaster.View.DataRowCount; i++)
            {
                if (grdMaster.View.GetRowCellValue(i, "PRICECLASSID").ToString().Equals(strPriceclassid))
                {
                    return i;
                }
            }
            return iRow;
        }
        /// <summary>
        /// sPriceitemid 체크 처리 
        /// </summary>
        /// <param name="sLotId"></param>
        /// <returns></returns>
        private int checkPriceitemid(string sPriceitemid,int crow)
        {

            for (int i = 0; i < grdDetail.View.DataRowCount; i++)
            {
                if (grdDetail.View.GetRowCellValue(i, "PRICEITEMID").ToString().Equals(sPriceitemid) && i != crow)
                {
                    return i;
                }
            }
            return -1;
        }
        /// <summary>
        /// 단가 기준  key 중복 체크 
        /// </summary>
        /// <returns></returns>
        private bool CheckPriceDateKeyColumns()
        {
            bool blcheck = true;
            if (grdDetail.View.DataRowCount == 0)
            {
                return blcheck;
            }

            for (int irow = 0; irow < grdDetail.View.DataRowCount; irow++)
            {

                DataRow row = grdDetail.View.GetDataRow(irow);

                if (row.RowState == DataRowState.Added || row.RowState == DataRowState.Modified)
                {
                    string strValue = row["PRICEITEMID"].ToString();

                    if (SearchPeriodidKey(strValue, "PRICEITEMID", irow) < 0)
                    {
                        blcheck = true;
                    }
                    else
                    {
                        // 에러 외주단가 항목
                        string lblPeriodid = grdDetail.View.Columns["PRICEITEMID"].Caption.ToString();
                        this.ShowMessage(MessageBoxButtons.OK, "InValidOspData007", lblPeriodid);
                        return false;
                    }
                    strValue = row["PRIORITY"].ToString();

                    if (SearchPeriodidKey(strValue, "PRIORITY", irow) < 0)
                    {
                        blcheck = true;
                    }
                    else
                    {
                        // 에러 우선순위 중복 
                        string lblPeriodid = grdDetail.View.Columns["PRIORITY"].Caption.ToString();
                        this.ShowMessage(MessageBoxButtons.OK, "InValidOspData007", lblPeriodid);
                        return false;
                    }
                    
                }
            }
            return blcheck;
        }
        /// <summary>
        ///  중복 체크 함수
        /// </summary>
        /// <param name="dateStartdate"></param>
        /// <param name="dateEnddate"></param>
        /// <param name="icurRow"></param>
        /// <returns></returns>
        private int SearchPeriodidKey(string strValue,string colstringName , int icurRow)
        {
            int iresultRow = -1;

            for (int irow = 0; irow < grdDetail.View.DataRowCount; irow++)
            {
                if (icurRow != irow)
                {
                    DataRow row = grdDetail.View.GetDataRow(irow);

                    // 행 삭제만 제외 
                    if (grdDetail.View.IsDeletedRow(row) == false)
                    {
                        string strTemnpValue = row[colstringName].ToString();

                        if (strValue.Equals(strTemnpValue))
                        {
                            return irow;
                        }

                    }
                }
            }
            return iresultRow;
        }
        #endregion
    }
}
