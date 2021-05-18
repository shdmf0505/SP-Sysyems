#region using

using Micube.Framework.Net;
using Micube.Framework;
using Micube.Framework.SmartControls;
using Micube.SmartMES.StandardInfo.Popup;
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
    /// 프 로 그 램 명  : 기준 정보 > 품질기준정보 > 출하검사 
    /// 업  무  설  명  : 설비에서 넘어오는 Defect Code를 관리 한다.
    /// 생    성    자  : 전우성
    /// 생    성    일  : 2019-05-23
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class OutBaseInfo : SmartConditionManualBaseForm
    {
        #region Local Variables
        string sItemName = "";
        #endregion

        #region 생성자

        public OutBaseInfo()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        protected override void InitializeContent()
        {
            base.InitializeContent();
            


            InitializeLanguageKey();
            InitializeGrid();
            InitializeEvent();

           
            grdOutBaseInfo.LanguageKey = "SHIPINSPECTIONLIST";
           
        }

        /// <summary>
        /// Language Key 초기화
        /// </summary>
        private void InitializeLanguageKey()
        {
            grdOutBaseInfo.LanguageKey = "GRIDDEFECTCODELIST";
        }

        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            //grdOutBaseInfo.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export;
            // 2020.03.31-유석진-삭제 버튼 뺌, 기준정보는 등록하면 삭제 할 수 없음. 유효상태로 관리 가능
            grdOutBaseInfo.GridButtonItem = GridButtonItem.Add | GridButtonItem.Export;
            grdOutBaseInfo.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            
            //grdOutBaseInfo.View.AddTextBoxColumn("ITEMID", 100).SetIsReadOnly();
            grdOutBaseInfo.View.AddTextBoxColumn("ENTERPRISEID", 80).SetIsHidden();
            grdOutBaseInfo.View.AddTextBoxColumn("PLANTID", 80).SetIsHidden();
            grdOutBaseInfo.View.AddTextBoxColumn("PROCESSSEGID", 80).SetIsHidden();
            grdOutBaseInfo.View.AddTextBoxColumn("INSPITEMID", 80).SetIsHidden();
            grdOutBaseInfo.View.AddTextBoxColumn("INSPECTIONDEFID", 80).SetIsHidden();
            grdOutBaseInfo.View.AddTextBoxColumn("INSPECTIONDEFVERSION", 80).SetIsHidden();
            grdOutBaseInfo.View.AddTextBoxColumn("RESOURCETYPE", 80).SetIsHidden();
            InitializeGrid_ITEMPOPUP();
            grdOutBaseInfo.View.AddTextBoxColumn("ITEMVERSION", 80).SetIsReadOnly();
            grdOutBaseInfo.View.AddTextBoxColumn("ITEMNAME", 250).SetIsReadOnly();

            grdOutBaseInfo.View.AddComboBoxColumn("AQLINSPECTIONLEVEL", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=AQLSize", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetEmptyItem("", "");
            grdOutBaseInfo.View.AddComboBoxColumn("INSPECTORDEGREE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=InspectionGrade", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetEmptyItem("", "");
            grdOutBaseInfo.View.AddComboBoxColumn("ISINSPECTIONREQUIRED", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdOutBaseInfo.View.AddComboBoxColumn("ISAQL", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdOutBaseInfo.View.AddComboBoxColumn("AQLDEFECTLEVEL", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=AQLRate", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetEmptyItem("", "");
            //주기
            grdOutBaseInfo.View.AddComboBoxColumn("AQLCYCLE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=InspCycle", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
               .SetEmptyItem("","")
               .SetDefault("LOT");

            grdOutBaseInfo.View.AddComboBoxColumn("AQLDECISIONDEGREE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=DecisionDegree", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetEmptyItem("", "");

            grdOutBaseInfo.View.AddComboBoxColumn("ISNCR", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            grdOutBaseInfo.View.AddSpinEditColumn("NCRINSPECTIONQTY", 100);
            grdOutBaseInfo.View.AddComboBoxColumn("NCRCYCLE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=InspCycle", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetEmptyItem("", "")
                .SetDefault("LOT");
            grdOutBaseInfo.View.AddComboBoxColumn("NCRDECISIONDEGREE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=DecisionDegree", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetEmptyItem("", "");

            grdOutBaseInfo.View.AddSpinEditColumn("NCRLOTSIZE", 100);
       
            //grdOutBaseInfo.View.AddComboBoxColumn("INSPECTIONUNIT", 80, new SqlQuery("GetUomDefinitionList", "10001", $"UOMCLASSID=MMQC"), "UOMDEFNAME", "UOMDEFID")
            //    .SetDefault("PCS")
            //    .SetTextAlignment(TextAlignment.Center);


            grdOutBaseInfo.View.AddComboBoxColumn("VALIDSTATE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Valid")
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Center);

            grdOutBaseInfo.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdOutBaseInfo.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdOutBaseInfo.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdOutBaseInfo.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdOutBaseInfo.View.SetKeyColumn("ITEMID", "ITEMVERSION");
            grdOutBaseInfo.View.PopulateColumns();
            

            
        }

        private void InitializeGrid_ITEMPOPUP()
        {
            var parentPopupColumn = grdOutBaseInfo.View.AddSelectPopupColumn("ITEMID", new SqlQuery("GetProductdefidlistByStd", "10001"
                , $"ENTERPRISEID={UserInfo.Current.Enterprise}"
                , $"MASTERDATACLASSNAME={"Product"}"
                , $"PLANTID={UserInfo.Current.Plant}"))
                .SetPopupLayout("ITEMID", PopupButtonStyles.Ok_Cancel, true, false)  //팝업창 UI 설정.
                .SetPopupResultCount(1)  //팝업창 선택가능한 개수
                .SetPosition(0)
                .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {
                  
                    foreach (DataRow row in selectedRows)
                    {
                        dataGridRow["ITEMID"] = row["ITEMID"].ToString();
                        dataGridRow["ITEMNAME"] = row["ITEMNAME"].ToString();
                        dataGridRow["ITEMVERSION"] = "*";

                    }
                });

            parentPopupColumn.Conditions.AddTextBox("ITEMID");
            parentPopupColumn.Conditions.AddTextBox("ITEMNAME");
            parentPopupColumn.Conditions.AddTextBox("ITEMVERSION");
            parentPopupColumn.GridColumns.AddTextBoxColumn("ITEMID", 150);
            parentPopupColumn.GridColumns.AddTextBoxColumn("ITEMNAME", 200);
            parentPopupColumn.GridColumns.AddTextBoxColumn("ITEMVERSION", 100);
            ;
        }
        #endregion



        #region Event

        /// <summary>        
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {

            grdOutBaseInfo.View.CellValueChanged += View_CellValueChanged;
            grdOutBaseInfo.View.AddingNewRow += View_AddingNewRow;
            grdOutBaseInfo.ToolbarDeleteRow += GrdOutBaseInfo_ToolbarDeleteRow;
            grdOutBaseInfo.View.ShowingEditor += View_ShowingEditor;  
        }
        private void View_ShowingEditor(object sender, CancelEventArgs e)
        {
            
            if (grdOutBaseInfo.View.FocusedColumn.FieldName.Equals("AQLDECISIONDEGREE") && grdOutBaseInfo.View.GetFocusedRowCellValue("ISAQL").ToString().Equals("N"))
            {
                e.Cancel = true;
            }

        }
        /// <summary>
        /// 코드그룹 리스트 그리드에서 추가 버튼 클릭 시 호출
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {

          
            grdOutBaseInfo.View.SetFocusedRowCellValue("VALIDSTATE","Valid");
           
        }
        private void GrdOutBaseInfo_ToolbarDeleteRow(object sender, EventArgs e)
        {

            DataRow row = grdOutBaseInfo.View.GetFocusedDataRow();

            if (row.RowState != DataRowState.Added)
            {
                grdOutBaseInfo.View.SetFocusedRowCellValue("VALIDSTATE", "Invalid");
            }
           
        }

        private void View_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

            grdOutBaseInfo.View.CellValueChanged -= View_CellValueChanged;
            if (e.Column.FieldName == "ISAQL")
            {
           
                DataRow row = grdOutBaseInfo.View.GetFocusedDataRow();
             

                if (row["ISAQL"].ToString().Equals("N"))
                {
                    grdOutBaseInfo.View.SetFocusedRowCellValue("AQLCYCLE", "");
                    grdOutBaseInfo.View.SetFocusedRowCellValue("AQLDECISIONDEGREE", "");
                    grdOutBaseInfo.View.SetFocusedRowCellValue("AQLDEFECTLEVEL", "");
                  
                    grdOutBaseInfo.View.CellValueChanged += View_CellValueChanged;
                    return;
                }
                else
                {
                    grdOutBaseInfo.View.SetFocusedRowCellValue("AQLDECISIONDEGREE", "A");
                    grdOutBaseInfo.View.CellValueChanged += View_CellValueChanged;
                    return;
                  
                }

            }
            if (e.Column.FieldName == "ISNCR")
            {

                DataRow row = grdOutBaseInfo.View.GetFocusedDataRow();


                if (row["ISNCR"].ToString().Equals("N"))
                {
                    grdOutBaseInfo.View.SetFocusedRowCellValue("NCRCYCLE", "");
                    grdOutBaseInfo.View.SetFocusedRowCellValue("NCRDECISIONDEGREE", "");
                    grdOutBaseInfo.View.SetFocusedRowCellValue("NCRINSPECTIONQTY", "");
                    grdOutBaseInfo.View.SetFocusedRowCellValue("NCRLOTSIZE", "");
                    grdOutBaseInfo.View.CellValueChanged += View_CellValueChanged;
                    return;
                }
             

            }
            grdOutBaseInfo.View.CellValueChanged += View_CellValueChanged;


        }


        //private void grdOutBaseInfo_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        //{
        //    //DataRow rowOutBaseInfo = grdOutBaseInfo.View.GetFocusedDataRow();

        //    if (this.MenuId == "PG-SD-0467")
        //    {
        //        args.NewRow["INSPECTIONDEFID"] = "FinishInspection";
        //    }

        //    if (this.MenuId == "PG-SD-0468")
        //    {
        //        args.NewRow["INSPECTIONDEFID"] = "ShipmentInspection";
        //    }

        //    args.NewRow["RESOURCETYPE"] = "Product";
        //    args.NewRow["INSPECTIONDEFVERSION"] = "*";
        //    args.NewRow["INSPITEMID"] = "*";
        //    args.NewRow["INSPITEMVERSION"] = "*";

        //    args.NewRow["INSPECTIONUNIT"] = "PCS";

        //    //args.NewRow["PROCESSSEGMENTTYPE"] = "*";
        //    args.NewRow["PROCESSSEGID"] = "*";
        //    args.NewRow["PROCESSEGVERSION"] = "*";

        //    args.NewRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
        //    args.NewRow["PLANTID"] = UserInfo.Current.Plant;

        //    args.NewRow["VALIDSTATE"] = "Valid";


        //}

        #endregion

        #region 툴바

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            //base.OnToolbarSaveClick();

            //DataTable dtchange = grdOutBaseInfo.GetChangedRows();
           
            ////dtchange.Columns.Add("ENTERPRISEID");
            ////dtchange.Columns.Add("PLANTID");
            ////dtchange.Columns.Add("PROCESSSEGID");
            ////dtchange.Columns.Add("PLANTID");
            ////dtchange.Columns.Add("INSPITEMID");
            //dtchange.Columns.Add("RESOURCEID");
            //dtchange.Columns.Add("RESOURCEVERSION");
            ////dtchange.Columns.Add("RESOURCETYPE");
            ////dtchange.Columns.Add("INSPECTIONDEFID");
            ////dtchange.Columns.Add("INSPECTIONDEFVERSION");
          
            //foreach (DataRow row in dtchange.Rows)
            //{
            //    row["ENTERPRISEID"] = UserInfo.Current.Enterprise.ToString();
            //    row["PLANTID"] = "*";
            //    row["PROCESSSEGID"] = "*";
            //    row["INSPITEMID"] = "*";
            //    row["RESOURCEID"] = row["ITEMID"];
            //    row["RESOURCEVERSION"] = row["ITEMVERSION"];
            //    row["RESOURCETYPE"] = "Product";
            //    row["INSPECTIONDEFID"] = "ShipmentInspection";
            //    row["INSPECTIONDEFVERSION"] ="*";
              
            //    row["MODIFIER"] = UserInfo.Current.Id;
                
            //}
            //ExecuteRule("SaveInspectionitemrel", dtchange);
        }

        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            var txtItemid = Conditions.AddTextBox("ITEMID")
             .SetLabel("ITEMID")
             .SetPosition(1);
            var txtItemname = Conditions.AddTextBox("P_ITEMNAME")
             .SetLabel("ITEMNAME")
             .SetPosition(2);
           // InitializeConditionProductDefinitionId_Popup();


        }

        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            //Conditions.GetControl<SmartTextBox>("P_ITEMNAME").ReadOnly = true;
            //Conditions.GetControl<SmartTextBox>("P_ITEMVERSION").ReadOnly = true;
            //Conditions.GetControl<SmartSelectPopupEdit>("ITEMID").EditValueChanged += ProductDefIDChanged;

        }

        //private void ProductDefIDChanged(object sender, EventArgs e)
        //{
        //    SmartSelectPopupEdit PopProdutid = sender as SmartSelectPopupEdit;

        //    if (Format.GetFullTrimString(PopProdutid.EditValue).Equals(string.Empty))
        //    {
        //        Conditions.GetControl<SmartTextBox>("P_ITEMVERSION").EditValue = string.Empty;
        //        Conditions.GetControl<SmartTextBox>("P_ITEMNAME").EditValue = string.Empty;
        //    }
        //}


        #endregion

        #region 조회조건 영역 초기화
        /// <summary>
        /// 팝업형 조회조건 생성 - 품목 코드
        /// </summary>
        /// 

        //private void InitializeConditionProductDefinitionId_Popup()
        //{
        //    //팝업 컬럼 설정
        //    var parentPopupColumn = Conditions.AddSelectPopup("ITEMID", new SqlQuery("GetProductdefidlistByStd", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "ITEMID", "ITEMID")
        //       .SetPopupLayout("ITEMID", PopupButtonStyles.Ok_Cancel, true, false)  //팝업창 UI 설정.
        //       .SetPopupResultCount(1)  //팝업창 선택가능한 개수
        //       .SetPosition(0)
        //       .SetPopupApplySelection((selectRow, gridRow) => {


        //           List<string> productDefnameList = new List<string>();
        //           List<string> productRevisionList = new List<string>();

        //           selectRow.AsEnumerable().ForEach(r => {
        //               productDefnameList.Add(Format.GetString(r["ITEMNAME"]));
        //               productRevisionList.Add(Format.GetString(r["ITEMVERSION"]));
        //           });

        //           Conditions.GetControl<SmartTextBox>("P_ITEMNAME").EditValue = string.Join(",", productDefnameList);
        //           Conditions.GetControl<SmartTextBox>("P_ITEMVERSION").EditValue = string.Join(",", productRevisionList);
        //       });
        //    parentPopupColumn.Conditions.AddComboBox("MASTERDATACLASSNAME", new SqlQuery("GetCodeListByOspProductdeftype", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
        //       .SetLabel("MASTERDATACLASSNAME")
        //       .SetDefault("Product");
        //    parentPopupColumn.Conditions.AddTextBox("ITEMID");
        //    parentPopupColumn.Conditions.AddTextBox("ITEMNAME");
        //    parentPopupColumn.Conditions.AddTextBox("ITEMVERSION");
        //    //팝업 그리드
        //    parentPopupColumn.GridColumns.AddComboBoxColumn("MASTERDATACLASSNAME", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductDefType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsReadOnly();

        //    parentPopupColumn.GridColumns.AddTextBoxColumn("ITEMID", 150);
        //    parentPopupColumn.GridColumns.AddTextBoxColumn("ITEMNAME", 200);
        //    parentPopupColumn.GridColumns.AddTextBoxColumn("ITEMVERSION", 100);
           

        //}




        #endregion


        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            ///base.OnToolbarClick(sender, e);
            SmartButton btn = sender as SmartButton;

            if (btn.Name.ToString().Equals("Inspectionpoint"))
            {

                if (grdOutBaseInfo.View.RowCount > 0)
                {
                    DataRow row = grdOutBaseInfo.View.GetFocusedDataRow();

                    object[] obj = new object[11];
                    obj[0] = "";// row["INSPITEMCLASSID"];
                    obj[1] = row["INSPITEMID"];
                    obj[2] = row["INSPITEMVERSION"];
                    obj[3] = row["INSPECTIONDEFID"];
                    obj[4] = row["INSPECTIONDEFVERSION"];
                    obj[5] = row["ITEMID"];
                    obj[6] = row["RESOURCETYPE"];
                    obj[7] = row["ITEMVERSION"];
                    obj[8] = "Valid";
                    obj[9] = row["ENTERPRISEID"];
                    obj[10] = row["PLANTID"];

                    OutBaseInfoPopup outbase = new OutBaseInfoPopup(obj);
                    outbase.ShowDialog();

                }

            }
            else if (btn.Name.ToString().Equals("Save"))
            {

                ProcSave(btn.Text);
            }
        }

        #region 검색

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            try
            {
                await base.OnSearchAsync();
                var value = Conditions.GetValues();

                value.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                value.Add("INSPECTIONDEFID","ShipmentInspection");
               


                grdOutBaseInfo.DataSource = await SqlExecuter.QueryAsync("GetOutBaseInfo", "10001", value);
              
            }
            catch (Exception ex)
            {
                throw Framework.MessageException.Create(Format.GetString(ex));
            }
            finally
            {
                DialogManager.CloseWaitArea(this.pnlContent);
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

            grdOutBaseInfo.View.CheckValidation();

            if (grdOutBaseInfo.GetChangedRows().Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }

            for (int irow = 0; irow < grdOutBaseInfo.View.DataRowCount; irow++)
            {

                DataRow row = grdOutBaseInfo.View.GetDataRow(irow);

                if (row.RowState == DataRowState.Added)
                {
                    string strItemid = row["ITEMID"].ToString();
                    string strItemversion = row["ITEMVERSION"].ToString();
                  

                    if (SearchPriceDateKey(strItemid, strItemversion, irow) > 0)
                    {
                        throw MessageException.Create("InValidStdData004", strItemid +"-" + strItemversion); //메세지 );

                    }
                   
                }
            }
        }
        /// <summary>
        /// 자재 중복 체크 함수
        /// </summary>
        /// <param name="dateStartdate"></param>
        /// <param name="dateEnddate"></param>
        /// <param name="icurRow"></param>
        /// <returns></returns>
        private int SearchPriceDateKey(string strItemid, string strItemversion, int icurRow)
        {
            int iresultRow = -1;

            for (int irow = 0; irow < grdOutBaseInfo.View.DataRowCount; irow++)
            {
                if (icurRow != irow)
                {
                    DataRow row = grdOutBaseInfo.View.GetDataRow(irow);

                    // 행 삭제만 제외 
                    string strTempItemid = row["ITEMID"].ToString();
                    string strTempItemversion = row["ITEMVERSION"].ToString();
                   
                    if (strTempItemid.Equals(strItemid) && strTempItemversion.Equals(strItemversion))
                    {
                        return irow;
                    }

                   
                }
            }
            return iresultRow;
        }
        #endregion

        #region Private Function
        /// <summary>
        /// 팝업 컬럼에서 확인 클릭 시 다른 데이터 매핑 ( 품목)
        /// </summary>
        /// <param name="currentGridRow">화면 grid row</param>
        /// <param name="popupSelections">팝업에서 선택한 row</param>
        /// <returns></returns>
        private Micube.Framework.SmartControls.Validations.ValidationResultCommon ValidationProductPopup(DataRow currentGridRow, IEnumerable<DataRow> popupSelections)
        {
            Micube.Framework.SmartControls.Validations.ValidationResultCommon result = new Framework.SmartControls.Validations.ValidationResultCommon();

            foreach (DataRow row in popupSelections)
            {
                currentGridRow["ITEMVERSION"] = row["ITEMVERSION"];
                currentGridRow["ITEMNAME"] = row["ITEMNAME"];

            }
            return result;
        }


        private void ProcSave(string strtitle)
        {

            grdOutBaseInfo.View.FocusedRowHandle = grdOutBaseInfo.View.FocusedRowHandle;
            grdOutBaseInfo.View.FocusedColumn = grdOutBaseInfo.View.Columns["ITEMID"];
            grdOutBaseInfo.View.ShowEditor();
          
            grdOutBaseInfo.View.CheckValidation();

            if (grdOutBaseInfo.GetChangedRows().Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                this.ShowMessage(MessageBoxButtons.OK,"NoSaveData");
                return;
            }

            for (int irow = 0; irow < grdOutBaseInfo.View.DataRowCount; irow++)
            {

                DataRow row = grdOutBaseInfo.View.GetDataRow(irow);

                if (row.RowState == DataRowState.Added)
                {
                    string strItemid = row["ITEMID"].ToString();
                    string strItemversion = row["ITEMVERSION"].ToString();


                    if (SearchPriceDateKey(strItemid, strItemversion, irow) > 0)
                    {
                        this.ShowMessage(MessageBoxButtons.OK,"InValidStdData004", strItemid + "-" + strItemversion); //메세지 );
                        return;
                    }

                }
            }


            DialogResult result = System.Windows.Forms.DialogResult.No;

            result = this.ShowMessage(MessageBoxButtons.YesNo, "OspDoProcessWant", strtitle);//저장하시겠습니까? 

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    this.ShowWaitArea();
                    DataTable dtchange = grdOutBaseInfo.GetChangedRows();
                    dtchange.Columns.Add("RESOURCEID");
                    dtchange.Columns.Add("RESOURCEVERSION");
                    foreach (DataRow row in dtchange.Rows)
                    {
                        row["ENTERPRISEID"] = UserInfo.Current.Enterprise.ToString();
                        row["PLANTID"] = "*";
                        row["PROCESSSEGID"] = "*";
                        row["INSPITEMID"] = "*";
                        row["RESOURCEID"] = row["ITEMID"];
                        row["RESOURCEVERSION"] = row["ITEMVERSION"];
                        row["RESOURCETYPE"] = "Product";
                        row["INSPECTIONDEFID"] = "ShipmentInspection";
                        row["INSPECTIONDEFVERSION"] = "*";

                        row["MODIFIER"] = UserInfo.Current.Id;

                    }
                    ExecuteRule("SaveInspectionitemrel", dtchange);
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

        /// <summary>
        /// 
        /// </summary>
        private void OnSaveConfrimSearch()
        {
           
            var value = Conditions.GetValues();
           
            value.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            value.Add("INSPECTIONDEFID", "ShipmentInspection");

            grdOutBaseInfo.DataSource = SqlExecuter.Query("GetOutBaseInfo", "10001", value);
        

        }
        #endregion
    }
}
