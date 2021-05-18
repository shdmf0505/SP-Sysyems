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
using System.Globalization;
#endregion

namespace Micube.SmartMES.OutsideOrderMgnt
{
    /// <summary>
    /// 프 로 그 램 명  :  외주관리> 외주단가관리 > 가공단가등록
    /// 업  무  설  명  : 가공단가등록한다.
    /// 생    성    자  : CHOISSTAR
    /// 생    성    일  : 2019-07-05
    /// 수  정  이  력  : 
    /// 
    /// 
    /// 
    /// </summary>
    public partial class SubcontractPriceRegistration : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가

        private string _strPlantid = ""; // plant 변경시 작업 
        #endregion

        #region 생성자

        public SubcontractPriceRegistration()
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
            _strPlantid = UserInfo.Current.Plant.ToString();
            // TODO : 컨트롤 초기화 로직 구성
            InitializeGrid();
        }

        /// <summary>        
        /// 코드그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            //중공정 
            InitializeGrid_ProSClass();
            //단가코드 
            InitializeGrid_ClassPirceCode();
            //단가기준
            InitializeGrid_Combination();
            //단가
            InitializeGrid_OspPrice();
        }

        /// <summary>        
        /// 중공정 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid_ProSClass()
        {
            
            grdProSClass.GridButtonItem =  GridButtonItem.Export | GridButtonItem.Expand | GridButtonItem.Restore;
            grdProSClass.View.SetIsReadOnly();
            grdProSClass.View.AddTextBoxColumn("PLANTID", 200)
                .SetIsHidden();                                                          //  회사 ID
            grdProSClass.View.AddTextBoxColumn("ENTERPRISEID", 200)
                .SetIsHidden();                                                           //  공장 ID
            grdProSClass.View.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 100); 
            grdProSClass.View.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 200);
            grdProSClass.View.AddTextBoxColumn("PROCESSSEGMENTID", 100); 
            grdProSClass.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
            //grdProSClass.View.SetAutoFillColumn("PROCESSSEGMENTCLASSNAME");
            grdProSClass.View.PopulateColumns();

        }

        /// <summary>        
        /// 단가코드 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid_ClassPirceCode()
        {
           
            grdClassPirceCode.GridButtonItem = GridButtonItem.Export | GridButtonItem.Expand | GridButtonItem.Restore;
            grdClassPirceCode.View.SetIsReadOnly();
            grdClassPirceCode.View.AddTextBoxColumn("PLANTID", 200)
                .SetIsHidden();                                                          //  회사 ID
            grdClassPirceCode.View.AddTextBoxColumn("ENTERPRISEID", 200)
                .SetIsHidden();                                                           //  공장 ID
            grdClassPirceCode.View.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 150)
                .SetIsHidden();
            grdClassPirceCode.View.AddTextBoxColumn("OSPPRICECODE", 100);
            grdClassPirceCode.View.AddTextBoxColumn("OSPPRICENAME", 150);
            grdClassPirceCode.View.AddComboBoxColumn("SPECUNIT", 100, new SqlQuery("GetCodeList", "00001",
               "CODECLASSID=AttributeCode", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdClassPirceCode.View.AddComboBoxColumn("CALCULATEUNIT", 100, new SqlQuery("GetUomDefinitionMapListByOsp", "10001", "UOMCATEGORY=OSP", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "UOMDEFID", "UOMDEFID");
            grdClassPirceCode.View.AddTextBoxColumn("DESCRIPTION", 200);
            //grdClassPirceCode.View.SetAutoFillColumn("DESCRIPTION");
            grdClassPirceCode.View.PopulateColumns();
        }

        /// <summary>        
        /// 단가기준 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid_Combination()
        {
          
            grdCombination.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export | GridButtonItem.Expand | GridButtonItem.Restore;
            grdCombination.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdCombination.View.AddTextBoxColumn("ENTERPRISEID", 100)
                .SetIsHidden();
            grdCombination.View.AddTextBoxColumn("PLANTID", 100)
                .SetIsHidden();
            grdCombination.View.AddTextBoxColumn("PRICECOMBINATIONID", 100)
                .SetIsHidden();
            grdCombination.View.AddTextBoxColumn("OSPPRICECODE", 100)
                .SetIsHidden();
            grdCombination.View.AddComboBoxColumn("OWNTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OwnType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetEmptyItem("*","*");  // 
            grdCombination.View.AddComboBoxColumn("PROCESSPRICETYPE", 100 ,new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProcessPriceType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetEmptyItem("*", "*");  // 

            //this.InitializeGrid_VendorPopup();// 

            //grdCombination.View.AddTextBoxColumn("OSPVENDORNAME", 100)
            //     .SetIsReadOnly();
            InitializeGrid_AreaPopup();
            grdCombination.View.AddTextBoxColumn("AREANAME", 100)
                 .SetIsReadOnly();


            this.InitializeGrid_productdePopup();// 
            grdCombination.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80)
                .SetIsReadOnly();
            grdCombination.View.AddTextBoxColumn("PRODUCTDEFNAME", 200)
                .SetIsReadOnly(); 
            grdCombination.View.AddTextBoxColumn("DESCRIPTION", 200);
            //grdCombination.View.SetAutoFillColumn("DESCRIPTION");
            grdCombination.View.SetKeyColumn("PLANTID", "ENTERPRISEID", "PRICECOMBINATIONID", "OSPPRICECODE", "OWNTYPE", "PROCESSPRICETYPE", "AREAID", "PRODUCTDEFID", "PRODUCTDEFVERSION");
            grdCombination.View.PopulateColumns();
        }


        /// <summary>
        /// InitializeGrid_VendorPopup
        /// </summary>
        private void InitializeGrid_VendorPopup()
        {
            var popupGridProcessSegments = this.grdCombination.View.AddSelectPopupColumn("OSPVENDORID",
                new SqlQuery("GetVendorListByOsp", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}"))
                // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
                .SetPopupLayout("OSPVENDORID", PopupButtonStyles.Ok_Cancel, true, false)
                // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
                .SetPopupResultCount(1)
                .SetRelationIds("PLANTID")
                // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
                .SetPopupResultMapping("OSPVENDORID", "OSPVENDORID")
                // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
                .SetPopupLayoutForm(500, 600, FormBorderStyle.FixedToolWindow)
                // 그리드의 남은 영역을 채울 컬럼 설정

                .SetPopupAutoFillColumns("OSPVENDORNAME")
                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {
                    DataRow classRow = grdCombination.View.GetFocusedDataRow();

                    foreach (DataRow row in selectedRows)
                    {
                        classRow["OSPVENDORNAME"] = row["OSPVENDORNAME"];
                    }
                })
            ;
            // 팝업에서 사용할 조회조건 항목 추가
            popupGridProcessSegments.Conditions.AddTextBox("OSPVENDORNAME");
            popupGridProcessSegments.Conditions.AddTextBox("PLANTID")
               .SetPopupDefaultByGridColumnId("PLANTID")
                .SetLabel("")
               .SetIsHidden();
            // 팝업 그리드 설정
            popupGridProcessSegments.GridColumns.AddTextBoxColumn("OSPVENDORID", 150)
                .SetValidationKeyColumn();
            popupGridProcessSegments.GridColumns.AddTextBoxColumn("OSPVENDORNAME", 200);


        }
        /// <summary>
        /// InitializeGrid_AreaPopup
        /// </summary>
        private void InitializeGrid_AreaPopup()
        {
            var popupGridProcessSegments = this.grdCombination.View.AddSelectPopupColumn("AREAID",
                new SqlQuery("GetAreaidPopupListByOsp", "10001"
                , $"LANGUAGETYPE={UserInfo.Current.LanguageType}"
                , $"ENTERPRISEID={UserInfo.Current.Enterprise}"
                , $"P_PLANTID={_strPlantid}"))
                // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
                .SetPopupLayout("AREANAME", PopupButtonStyles.Ok_Cancel, true, false)
                // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
                .SetPopupResultCount(1)
               
                // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
                .SetPopupResultMapping("AREAID", "AREAID")
                // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
                .SetPopupLayoutForm(500, 600, FormBorderStyle.FixedToolWindow)
                // 그리드의 남은 영역을 채울 컬럼 설정

              
                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {
                    DataRow classRow = grdCombination.View.GetFocusedDataRow();

                    foreach (DataRow row in selectedRows)
                    {
                        classRow["AREANAME"] = row["AREANAME"];
                    }
                })
            ;
            // 팝업에서 사용할 조회조건 항목 추가
            popupGridProcessSegments.Conditions.AddTextBox("P_AREANAME")
                .SetLabel("AREANAME");
            
            // 팝업 그리드 설정
            popupGridProcessSegments.GridColumns.AddTextBoxColumn("AREAID", 150)
                .SetValidationKeyColumn();
            popupGridProcessSegments.GridColumns.AddTextBoxColumn("AREANAME", 200);


        }
        /// <summary>
        /// InitializeGrid_VendorPopup
        /// </summary>
        private void InitializeGrid_productdePopup()
        {
            var popupGridProcessSegments = this.grdCombination.View.AddSelectPopupColumn("PRODUCTDEFID",120,
                new SqlQuery("GetProductdefidPoplistByOsp", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}"))
                // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
                .SetPopupLayout("PRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)
                // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
                .SetPopupResultCount(1)
                .SetRelationIds("p_plantid")
                // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
                .SetPopupResultMapping("PRODUCTDEFID", "PRODUCTDEFID")
                // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
                .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                // 그리드의 남은 영역을 채울 컬럼 설정

                .SetPopupAutoFillColumns("PRODUCTDEFID")
                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {
                    DataRow classRow = grdCombination.View.GetFocusedDataRow();

                    foreach (DataRow row in selectedRows)
                    {
                        classRow["PRODUCTDEFVERSION"] = row["PRODUCTDEFVERSION"];
                        classRow["PRODUCTDEFNAME"] = row["PRODUCTDEFNAME"];
                    }
                })
            ;
            popupGridProcessSegments.Conditions.AddComboBox("PRODUCTDEFTYPE", new SqlQuery("GetCodeListByOspProductdeftype", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("PRODUCTDEFTYPE")
                .SetDefault("Product");
            popupGridProcessSegments.Conditions.AddTextBox("PRODUCTDEFNAME");
            // 팝업 그리드 설정
            popupGridProcessSegments.GridColumns.AddComboBoxColumn("PRODUCTDEFTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductDefType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsReadOnly();
            popupGridProcessSegments.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            popupGridProcessSegments.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 150);
            popupGridProcessSegments.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);


        }
        /// <summary>        
        /// 단가 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid_OspPrice()
        {
            
            grdOspPrice.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export | GridButtonItem.Expand | GridButtonItem.Restore;
            grdOspPrice.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdOspPrice.View.SetSortOrder("STARTDATE", DevExpress.Data.ColumnSortOrder.Descending);
            grdOspPrice.View.AddTextBoxColumn("ENTERPRISEID", 100)
                .SetIsHidden();
            grdOspPrice.View.AddTextBoxColumn("PRICECOMBINATIONID", 100)
                .SetIsHidden();
            grdOspPrice.View.AddTextBoxColumn("PLANTID", 100)
                .SetIsHidden();
            grdOspPrice.View.AddDateEditColumn("STARTDATE", 100)
                .SetValidationIsRequired()
                .SetDisplayFormat("yyyy-MM-dd", MaskTypes.DateTime);                              //  
            grdOspPrice.View.AddDateEditColumn("ENDDATE", 100)
                .SetValidationIsRequired()
                .SetDisplayFormat("yyyy-MM-dd", MaskTypes.DateTime);                              //  
            grdOspPrice.View.AddSpinEditColumn("OSPPRICE", 100)
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);                                     //
            grdOspPrice.View.AddTextBoxColumn("DESCRIPTION", 200);
            //grdOspPrice.View.SetAutoFillColumn("DESCRIPTION");
            grdOspPrice.View.SetKeyColumn("PLANTID", "ENTERPRISEID", "PRICECOMBINATIONID", "STARTDATE");
            grdOspPrice.View.PopulateColumns();
        }
        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {

            // TODO : 화면에서 사용할 이벤트 추가
            //버튼 이벤트 저장 
            btnSave.Click += BtnSave_Click;
            btnPriceSave.Click += BtnPriceSave_Click;

            grdCombination.View.AddingNewRow += GrdCombination_AddingNewRow;
            grdOspPrice.View.AddingNewRow += GrdOspPrice_AddingNewRow;
            //cellvalue change
            grdCombination.View.CellValueChanged += GrdCombination_CellValueChanged;
            grdOspPrice.View.CellValueChanged += GrdOspPrice_CellValueChanged;
            //FocusedRowChanged
            grdProSClass.View.FocusedRowChanged += GrdProSClass_FocusedRowChanged;
            grdClassPirceCode.View.FocusedRowChanged += GrdClassPirceCode_FocusedRowChanged;
            grdCombination.View.FocusedRowChanged += GrdCombination_FocusedRowChanged;


        }

        #region 버튼 Event(저장)

        /// <summary>
        /// 저장 -단가기준 등록
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            grdCombination.View.FocusedRowHandle = grdCombination.View.FocusedRowHandle;
            grdCombination.View.FocusedColumn = grdCombination.View.Columns["PRODUCTDEFNAME"];
            grdCombination.View.ShowEditor();
            DataTable changed = grdCombination.GetChangedRows();
            if (changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                this.ShowMessage(MessageBoxButtons.OK, "NoSaveData"); //메세지 
                return;
            }
            // 외주 단가 기준에 협력사 품목코드 null 값 대체(*) 처리
            SetNullReplaceCombination();
            for (int irow = 0; irow < grdCombination.View.DataRowCount; irow++)
            {
                DataRow row = grdCombination.View.GetDataRow(irow);
                string  strOwntype = row["OWNTYPE"].ToString();
                string strAreaid = row["AREAID"].ToString();
                if (!(strAreaid.Equals("*")) && strOwntype.Equals("*"))
                {
                    string lblOWNTYPE = grdCombination.View.Columns["OWNTYPE"].Caption.ToString();
                    string lblOSPVENDORID = lblOWNTYPE +" ,"+ grdCombination.View.Columns["AREANAME"].Caption.ToString();
                    this.ShowMessage(MessageBoxButtons.OK, "InValidOspData009" , lblOSPVENDORID); 
                    return;
                }
                
            }
            if (CheckCombinationKeyColumns()==false)
            {
                //다국어 처리  중복된 자료가 존재합니다.
                this.ShowMessage(MessageBoxButtons.OK, "OspDataOverlapCheck");
                return;
            }
            //유효성 체크(동일값 체크 할것)
            //특정 협력사를 지정하는 경우 자사구분을 공통으로 지정할수 없음.

            DialogResult result = System.Windows.Forms.DialogResult.No;

            result = this.ShowMessage(MessageBoxButtons.YesNo, "OspDoProcessWant", btnSave.Text);//저장하시겠습니까? 
           
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    this.ShowWaitArea();
                    btnSave.Enabled = false;

                    DataTable dtSave = grdCombination.GetChangedRows();
                   
                    ExecuteRule("OutsourcedBasedCombination", dtSave);

                    ShowMessage("SuccessOspProcess");

                }
                catch (Exception ex)
                {
                    this.ShowError(ex);
                }
                finally
                {
                    this.CloseWaitArea();
                    //재조회 행이동 처리 
                    focusedRowChangedClassPirceCode();
                    btnSave.Enabled = true;
                   
                }
            }

        }

        /// <summary>
        /// 단가 저장 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPriceSave_Click(object sender, EventArgs e)
        {
            grdOspPrice.View.FocusedRowHandle = grdOspPrice.View.FocusedRowHandle;
            grdOspPrice.View.FocusedColumn = grdOspPrice.View.Columns["STARTDATE"];
            grdOspPrice.View.ShowEditor();
            DataTable changed = grdOspPrice.GetChangedRows();
            if (changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                this.ShowMessage(MessageBoxButtons.OK, "NoSaveData"); //메세지 
                return;
            }
            grdOspPrice.View.FocusedRowHandle = grdOspPrice.View.FocusedRowHandle;
            grdOspPrice.View.FocusedColumn = grdOspPrice.View.Columns["DESCRIPTION"];
            grdOspPrice.View.ShowEditor();
            //단가 체크 
            //날짜 체크 하기 ....
            //수정된것만 날짜 기본 체크 (시작일 종료일 비교)
            DataTable dtbaseDate  = grdOspPrice.GetChangedRows();
            for (int irow = 0; irow < dtbaseDate.Rows.Count; irow++)
            {
                DataRow row = dtbaseDate.Rows[irow];
                DateTime StartDate = Convert.ToDateTime(row["STARTDATE"]);
                DateTime EndDate = Convert.ToDateTime(row["ENDDATE"]);
                if (StartDate > EndDate)
                {
                    // 다국어 메세지 처리 (종료일 보다 시작일이 자료가 존재합니다.)
                   
                    this.ShowMessage(MessageBoxButtons.OK, "OspCheckStartEnd");
                    return;
                }

            }
            if (CheckPriceDateKeyColumns() == false)
            {
                
                this.ShowMessage(MessageBoxButtons.OK, "OspCheckDuplStartEnd");
                return;
            } 
            
            //전체 체크 하기 ...

            //상태값 체크 여부 추가 (의뢰만)
            DialogResult result = System.Windows.Forms.DialogResult.No;

            result = this.ShowMessage(MessageBoxButtons.YesNo, "OspDoProcessWant", btnPriceSave.Text);//저장하시겠습니까? 
         
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    this.ShowWaitArea();
                    btnPriceSave.Enabled = false;
                    DataTable dtChang = grdOspPrice.GetChangedRows();
                    DataTable dtSave = grdOspPrice.GetChangedRows().Clone(); //테이블 레이아웃 복사
                    DataRow[] drSearch = dtChang.Select("_STATE_ ='deleted'");
                    for (int i = 0; i < drSearch.Length; i++)
                    {
                        dtSave.ImportRow(drSearch[i]);
                    }

                    drSearch = dtChang.Select("_STATE_ ='modified'");
                    for (int i = 0; i < drSearch.Length; i++)
                    {
                        dtSave.ImportRow(drSearch[i]);
                    }

                    drSearch = dtChang.Select("_STATE_ ='added'");
                    for (int i = 0; i < drSearch.Length; i++)
                    {
                        dtSave.ImportRow(drSearch[i]);
                    }

                   

                    ExecuteRule("OutsourcedBasedPriceRegistration", dtSave);

                    ShowMessage("SuccessOspProcess");

                }
                catch (Exception ex)
                {
                    this.ShowError(ex);
                }
                finally
                {
                    this.CloseWaitArea();
                    //재조회 처리 
                    focusedRowChangedCombination();
                    btnPriceSave.Enabled = true;
                   
                }
            }

        }
        #endregion
     
        #region 그리드 추가 (AddingNewRow)
        /// <summary>
        /// 단가기준 그리드에서 추가 버튼 클릭 시 호출
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void GrdCombination_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            //추가 할때 단가 그리드의 상태값을 체크 처리 
            //throw new NotImplementedException();
            // 단가코드 유무 체크 
            // 회사 ,plant...
            if (grdClassPirceCode.View.DataRowCount==0)
            {
                grdCombination.View.DeleteRow(grdCombination.View.FocusedRowHandle);
                //다국어  단가코드 정보가 없습니다. 추가를 할수 없습니다.
                return;
            }
            grdCombination.View.SetFocusedRowCellValue("ENTERPRISEID", UserInfo.Current.Enterprise.ToString());// 회사코드
            grdCombination.View.SetFocusedRowCellValue("PLANTID", grdClassPirceCode.View.GetFocusedRowCellValue("PLANTID"));// plantid
            grdCombination.View.SetFocusedRowCellValue("OSPPRICECODE", grdClassPirceCode.View.GetFocusedRowCellValue("OSPPRICECODE"));// plantid
            grdCombination.View.SetFocusedRowCellValue("OWNTYPE", "*");//
            grdCombination.View.SetFocusedRowCellValue("PROCESSPRICETYPE", "*");//
            grdCombination.View.SetFocusedRowCellValue("PRODUCTDEFID","*");//
            grdCombination.View.SetFocusedRowCellValue("PRODUCTDEFVERSION", "*");//
            grdCombination.View.SetFocusedRowCellValue("PRODUCTDEFNAME", "*");//
            grdCombination.View.SetFocusedRowCellValue("AREAID", "*");//
            grdCombination.View.SetFocusedRowCellValue("AREANAME", "*");//
                                                                             //단가 그리드 clear
            DataTable dtPrice = (grdOspPrice.DataSource as DataTable).Clone();
            grdOspPrice.DataSource = dtPrice;
        }

        /// <summary>
        /// 단가등록 그리드에서 추가 버튼 클릭 시 호출
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void GrdOspPrice_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            //추가 할때 단가기준 그리드의 상태값을 체크 처리 
          
            if (grdCombination.View.DataRowCount == 0)
            {
                grdOspPrice.View.DeleteRow(grdOspPrice.View.FocusedRowHandle);
               
                return;
            }
            DataTable changed = grdCombination.GetChangedRows();
            if (changed.Rows.Count > 0)
            {
                grdOspPrice.View.DeleteRow(grdOspPrice.View.FocusedRowHandle);
                
                return;
            }
            grdOspPrice.View.CellValueChanged -= GrdOspPrice_CellValueChanged;
            grdOspPrice.View.SetFocusedRowCellValue("ENTERPRISEID", UserInfo.Current.Enterprise.ToString());// 회사코드
            grdOspPrice.View.SetFocusedRowCellValue("PLANTID", grdCombination.View.GetFocusedRowCellValue("PLANTID"));// plantid
            grdOspPrice.View.SetFocusedRowCellValue("PRICECOMBINATIONID", grdCombination.View.GetFocusedRowCellValue("PRICECOMBINATIONID"));// plantid
            DateTime dateNow = DateTime.Now;
            grdOspPrice.View.SetFocusedRowCellValue("STARTDATE", dateNow.ToString("yyyy-MM-dd"));// 시작일
            grdOspPrice.View.SetFocusedRowCellValue("ENDDATE", "9999-12-31");//종료일
            grdOspPrice.View.CellValueChanged += GrdOspPrice_CellValueChanged;
            grdOspPrice.View.ClearSorting();
            grdOspPrice.View.Columns["STARTDATE"].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;


        }
        #endregion
        
        #region 그리드 값 변경 (CellValueChanged)
   
        /// <summary>
        /// 단가  그리드 포맷 변경 처리 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdOspPrice_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
           

            if (e.Column.FieldName == "STARTDATE")
            {
                grdOspPrice.View.CellValueChanged -= GrdOspPrice_CellValueChanged;

                DataRow row = grdOspPrice.View.GetFocusedDataRow();

                if (row["STARTDATE"].ToString().Equals(""))
                {
                    grdOspPrice.View.CellValueChanged += GrdOspPrice_CellValueChanged;
                    return;
                }

                DateTime dateBudget = Convert.ToDateTime(row["STARTDATE"].ToString());
                grdOspPrice.View.SetFocusedRowCellValue("STARTDATE", dateBudget.ToString("yyyy-MM-dd"));// 예산일자
                grdOspPrice.View.CellValueChanged += GrdOspPrice_CellValueChanged;
            }

            if (e.Column.FieldName == "ENDDATE")
            {
                grdOspPrice.View.CellValueChanged -= GrdOspPrice_CellValueChanged;

                DataRow row = grdOspPrice.View.GetFocusedDataRow();

                if (row["ENDDATE"].ToString().Equals(""))
                {
                    grdOspPrice.View.CellValueChanged += GrdOspPrice_CellValueChanged;
                    return;
                }
                DateTime dateBudget = Convert.ToDateTime(row["ENDDATE"].ToString());
                grdOspPrice.View.SetFocusedRowCellValue("ENDDATE", dateBudget.ToString("yyyy-MM-dd"));// 예산일자
                grdOspPrice.View.CellValueChanged += GrdOspPrice_CellValueChanged;
            }
           
        }

        /// <summary>
        /// 단가기준 그리드 변경 처리 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdCombination_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            
            if (e.Column.FieldName == "PRODUCTDEFID")
            {
                grdCombination.View.CellValueChanged -= GrdCombination_CellValueChanged;

                DataRow row = grdCombination.View.GetFocusedDataRow();

                if (row["PRODUCTDEFID"].ToString().Equals(""))
                {
                    grdCombination.View.SetFocusedRowCellValue("PRODUCTDEFID","*");// 
                    grdCombination.View.SetFocusedRowCellValue("PRODUCTDEFVERSION", "*");//
                    grdCombination.View.SetFocusedRowCellValue("PRODUCTDEFNAME", "*");//
                    grdCombination.View.CellValueChanged += GrdCombination_CellValueChanged;
                    return;
                }
             
                grdCombination.View.CellValueChanged += GrdCombination_CellValueChanged;
            }

            if (e.Column.FieldName == "OSPVENDORID")
            {
                grdCombination.View.CellValueChanged -= GrdCombination_CellValueChanged;

                DataRow row = grdCombination.View.GetFocusedDataRow();

                if (row["OSPVENDORID"].ToString().Equals(""))
                {
                    grdCombination.View.SetFocusedRowCellValue("OSPVENDORID", "*");// 
                    grdCombination.View.SetFocusedRowCellValue("OSPVENDORNAME", "*");//
                    grdCombination.View.CellValueChanged += GrdCombination_CellValueChanged;
                    return;
                }
             
                grdCombination.View.CellValueChanged += GrdCombination_CellValueChanged;
            }

        }
        #endregion
        #region 그리드 행 이동시(FocusedRowChange)

        /// <summary>
        ///  준공정 그리드의 포커스 행 변경 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdProSClass_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //단가기준 그리드 clear
            DataTable dtCombination = (grdCombination.DataSource as DataTable).Clone();
            grdCombination.DataSource = dtCombination;
            //단가 그리드 clear
            DataTable dtPrice = (grdOspPrice.DataSource as DataTable).Clone();
            grdOspPrice.DataSource = dtPrice;
            focusedRowChangedProSClass();
        }

        /// <summary>
        /// 단가코드 리스트 그리드의 포커스 행 변경 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdClassPirceCode_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            focusedRowChangedClassPirceCode();
        }

        /// <summary>
        /// 단가기준  리스트 그리드의 포커스 행 변경 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdCombination_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            focusedRowChangedCombination();
        }

        #endregion

        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

           
        }

        #endregion

        #region 검색

        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {

            await base.OnSearchAsync();
          
            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("P_ENTERPRISEID", UserInfo.Current.Enterprise);
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable dtBasedPrice = await SqlExecuter.QueryAsync("GetOutsourcedBasedProcesssegmentclassid", "10001", values);

            if (dtBasedPrice.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                                             //단가코드 그리드 clear
                DataTable dtPirceCode = (grdClassPirceCode.DataSource as DataTable).Clone();
                grdClassPirceCode.DataSource = dtPirceCode;
                //단가기준 그리드 clear
                DataTable dtCombination = (grdCombination.DataSource as DataTable).Clone();
                grdCombination.DataSource = dtCombination;
                //단가 그리드 clear
                DataTable dtPrice = (grdOspPrice.DataSource as DataTable).Clone();
                grdOspPrice.DataSource = dtPrice;
            }

            grdProSClass.DataSource = dtBasedPrice;
           
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            // site
            //InitializeConditionPopup_Plant();
            // 중공정 
            InitializeConditionPopup_Processsegmentclassid();
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
        /// 중공정 설정 
        /// </summary>
        private void InitializeConditionPopup_Processsegmentclassid()
        {

            // 팝업 컬럼설정
            var processsegmentclassidPopupColumn = Conditions.AddSelectPopup("p_processsegmentclassid", new SqlQuery("GetProcesssegmentclassidListByOsp", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "MIDDLEPROCESSSEGMENTCLASSNAME", "MIDDLEPROCESSSEGMENTCLASSID")
               .SetPopupLayout("MIDDLEPROCESSSEGMENTCLASS", PopupButtonStyles.Ok_Cancel, true, false)
               .SetPopupLayoutForm(400, 600)
               .SetLabel("MIDDLEPROCESSSEGMENTCLASS")
               .SetPopupResultCount(1)
               .SetPosition(0.2);

            // 팝업 조회조건
            processsegmentclassidPopupColumn.Conditions.AddTextBox("MIDDLEPROCESSSEGMENTCLASSNAME")
                .SetLabel("MIDDLEPROCESSSEGMENTCLASSNAME");

            // 팝업 그리드
            processsegmentclassidPopupColumn.GridColumns.AddTextBoxColumn("MIDDLEPROCESSSEGMENTCLASSID", 150)
                .SetValidationKeyColumn();
            processsegmentclassidPopupColumn.GridColumns.AddTextBoxColumn("MIDDLEPROCESSSEGMENTCLASSNAME", 200);

        }
        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
            SmartComboBox cboPlaint = Conditions.GetControl<SmartComboBox>("p_plantid");
            cboPlaint.EditValueChanged += cboPlaint_EditValueChanged;
        }
        private void cboPlaint_EditValueChanged(object sender, EventArgs e)
        {
            var values = Conditions.GetValues();

            _strPlantid = values["P_PLANTID"].ToString();
           
        }
        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

         
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
        /// 중공정 그리드의 포커스 행 변경시 행의 작업목록의 내용을 매핑처리 함.
        /// </summary>
        private void focusedRowChangedProSClass()
        {
            //포커스 행 체크 
            if (grdProSClass.View.FocusedRowHandle < 0) return;
           
            //단가코드 정보 가져오기 
            Dictionary<string, object> Param = new Dictionary<string, object>();
          
            Param.Add("P_PLANTID", grdProSClass.View.GetFocusedRowCellValue("PLANTID"));
            Param.Add("P_PROCESSSEGMENTCLASSID", grdProSClass.View.GetFocusedRowCellValue("PROCESSSEGMENTCLASSID"));
            Param.Add("P_PROCESSSEGMENTID", grdProSClass.View.GetFocusedRowCellValue("PROCESSSEGMENTID"));
            Param.Add("P_VALIDSTATE", "Valid");
            Param = Commons.CommonFunction.ConvertParameter(Param);
            DataTable dtPirceCode = SqlExecuter.Query("GetOutsourcedBasedOspPriceCodeList", "10001", Param);

            if (dtPirceCode.Rows.Count < 1)
            {
                //단가기준 그리드 clear
                DataTable dtCombination = (grdCombination.DataSource as DataTable).Clone();
                grdCombination.DataSource = dtCombination;
                //단가 그리드 clear
                DataTable dtPrice = (grdOspPrice.DataSource as DataTable).Clone();
                grdOspPrice.DataSource = dtPrice;
            }
            grdClassPirceCode.DataSource = dtPirceCode;
            if (dtPirceCode.Rows.Count >0 )
            {
                focusedRowChangedClassPirceCode();
            }
            
            
        }

        /// <summary>
        /// 그리드의 포커스 행 변경시 행의 작업목록의 내용을 매핑처리 함.
        /// </summary>
        private void focusedRowChangedClassPirceCode()
        {
            //포커스 행 체크 
            if (grdClassPirceCode.View.FocusedRowHandle < 0) return;

            //단가코드 정보 가져오기 
            Dictionary<string, object> Param = new Dictionary<string, object>();

            Param.Add("P_PLANTID", grdClassPirceCode.View.GetFocusedRowCellValue("PLANTID"));
            Param.Add("P_OSPPRICECODE", grdClassPirceCode.View.GetFocusedRowCellValue("OSPPRICECODE"));
            Param.Add("P_VALIDSTATE", "Valid");
            Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            Param = Commons.CommonFunction.ConvertParameter(Param);
            DataTable dtCombination = SqlExecuter.Query("GetOutsourcedBasedOspPriceCombinationList", "10001", Param);

            if (dtCombination.Rows.Count < 1)
            {
                //단가 그리드 clear
                DataTable dtPrice = (grdOspPrice.DataSource as DataTable).Clone();
                grdOspPrice.DataSource = dtPrice;
            }
            grdCombination.DataSource = dtCombination;

            if (dtCombination.Rows.Count > 0)
            {
                focusedRowChangedCombination();
            }
           
        }

        /// <summary>
        /// 그리드의 포커스 행 변경시 행의 작업목록의 내용을 매핑처리 함.
        /// </summary>
        private void focusedRowChangedCombination()
        {
            //포커스 행 체크 
             if (grdCombination.View.FocusedRowHandle < 0) return;

            Dictionary<string, object> Param = new Dictionary<string, object>();

            Param.Add("P_PLANTID", grdCombination.View.GetFocusedRowCellValue("PLANTID"));
            Param.Add("P_PRICECOMBINATIONID", grdCombination.View.GetFocusedRowCellValue("PRICECOMBINATIONID"));
            Param.Add("P_VALIDSTATE", "Valid");
            Param = Commons.CommonFunction.ConvertParameter(Param);
            DataTable dtPrice = SqlExecuter.Query("GetOutsourcedBasedOspPriceList", "10001", Param);
            grdOspPrice.DataSource = dtPrice;
          
        }

        /// <summary>
        /// 외주 단가 기준에 협력사 품목코드 null 값 대체(*) 처리 
        /// </summary>
        /// <param name="strRequestno"></param>
        private void SetNullReplaceCombination()
        {
           
            if (grdCombination.View.DataRowCount == 0)
            {
                return;
            }
            for (int irow = 0; irow < grdCombination.View.DataRowCount; irow++)
            {
                string sproductdefid = grdCombination.View.GetRowCellValue(irow, "PRODUCTDEFID").ToString();

                if (sproductdefid.Equals(""))
                {
                    grdCombination.View.SetRowCellValue(irow, "PRODUCTDEFID", "*");// 
                    grdCombination.View.SetRowCellValue(irow, "PRODUCTDEFVERSION", "*");//
                    grdCombination.View.SetRowCellValue(irow, "PRODUCTDEFNAME", "*");//
                  
                }
                string strareaid = grdCombination.View.GetRowCellValue(irow, "AREAID").ToString();

                if (strareaid.Equals(""))
                {
                    grdCombination.View.SetFocusedRowCellValue("AREAID", "*");// 
                    grdCombination.View.SetFocusedRowCellValue("AREANAME", "*");//
                }
             
            }
        }

        /// <summary>
        /// 단가 기준  key 중복 체크 
        /// </summary>
        /// <returns></returns>
        private bool CheckCombinationKeyColumns()
        {
            bool blcheck = true;
            if (grdCombination.View.DataRowCount == 0)
            {
                return blcheck;
            }

            string strOwntype = "";
            string strProcesspricetype = "";
            string strareaid = "";
            string strProductdefid = "";
            string strProductdefversion = "";
            for (int irow = 0; irow < grdCombination.View.DataRowCount; irow++)
            {
                DataRow row = grdCombination.View.GetDataRow(irow);
                strOwntype         = row["OWNTYPE"].ToString();
                strProcesspricetype = row["PROCESSPRICETYPE"].ToString();
                strareaid = row["AREAID"].ToString();
                strProductdefid = row["PRODUCTDEFID"].ToString();
                strProductdefversion = row["PRODUCTDEFVERSION"].ToString();
              
                if (row.RowState ==DataRowState.Added)
                {
                    if (SearchCombinationKey(strOwntype , strProcesspricetype, strareaid, strProductdefid, strProductdefversion, irow) < 0)
                    {
                        blcheck = true;
                       
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return blcheck;
        }

        /// <summary>
        ///  단가 기준 중복 행 찾기 
        /// </summary>
        /// <param name="strowntype"></param> 
        /// <param name="strprocesspricetype"></param>
        /// <param name="strospvendorid"></param>
        /// <param name="strproductdefid"></param>
        /// <param name="strproductdefversion"></param>
        /// <param name="icurRow"></param>
        /// <returns></returns>
        private int SearchCombinationKey (string strOwntype          , string strProcesspricetype,
                                          string strareaid      , string strProductdefid    , 
                                          string strProductdefversion, int icurRow)
        {
            int iresultRow =-1;
            string strbaseOwntype = "";
            string strbaseProcesspricetype = "";
            string strbaseareaid = "";
            string strbaseProductdefid = "";
            string strbaseProductdefversion = "";

            for (int irow = 0; irow < grdCombination.View.DataRowCount; irow++)
            {
                if (icurRow != irow)
                {
                    DataRow row = grdCombination.View.GetDataRow(irow);
                    // 행 삭제만 제외 
                    if (!(row.RowState == DataRowState.Deleted))
                    {
                        strbaseOwntype = row["OWNTYPE"].ToString();
                        strbaseProcesspricetype = row["PROCESSPRICETYPE"].ToString();
                        strbaseareaid = row["AREAID"].ToString();
                        strbaseProductdefid = row["PRODUCTDEFID"].ToString();
                        strbaseProductdefversion = row["PRODUCTDEFVERSION"].ToString();
                        if (strbaseOwntype.Equals(strOwntype) && strbaseProcesspricetype.Equals(strProcesspricetype) &&
                            strbaseareaid.Equals(strareaid) && strbaseProductdefid.Equals(strProductdefid) &&
                            strbaseProductdefversion.Equals(strProductdefversion))
                        {
                            return irow;
                        }
                    }
                }
            }
            return iresultRow;
        }

        /// <summary>
        /// 단가 기준  key 중복 체크 
        /// </summary>
        /// <returns></returns>
        private bool CheckPriceDateKeyColumns()
        {
            bool blcheck = true;
            if (grdOspPrice.View.DataRowCount == 0)
            {
                return blcheck;
            }

          
            for (int irow = 0; irow < grdOspPrice.View.DataRowCount; irow++)
            {

                DataRow row = grdOspPrice.View.GetDataRow(irow);
               
                if (row.RowState == DataRowState.Added || row.RowState == DataRowState.Modified)
                {
                    DateTime dateStartdate = Convert.ToDateTime(row["STARTDATE"]);
                    DateTime dateEnddate = Convert.ToDateTime(row["ENDDATE"]);
                    if (SearchPriceDateKey(dateStartdate, dateEnddate, irow) < 0)
                    {
                        blcheck = true;

                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return blcheck;
        }

        /// <summary>
        /// 기간 중복 체크 함수
        /// </summary>
        /// <param name="dateStartdate"></param>
        /// <param name="dateEnddate"></param>
        /// <param name="icurRow"></param>
        /// <returns></returns>
        private int SearchPriceDateKey(DateTime dateStartdate, DateTime dateEnddate, int icurRow)
        {
            int iresultRow = -1;

            for (int irow = 0; irow < grdOspPrice.View.DataRowCount; irow++)
            {
                if (icurRow != irow)
                {
                    DataRow row = grdOspPrice.View.GetDataRow(irow);
                   
                    // 행 삭제만 제외 
                    if (grdOspPrice.View.IsDeletedRow(row)== false)
                    {
                        DateTime dateSearchStartdate = Convert.ToDateTime(row["STARTDATE"]);
                        DateTime dateSearchEnddate = Convert.ToDateTime(row["ENDDATE"]);
                        //시작일 비교
                        if (dateStartdate >= dateSearchStartdate && dateStartdate <= dateSearchEnddate)
                        {
                            return irow;
                        }
                        // 종료일 비교
                        if (dateEnddate >= dateSearchStartdate && dateEnddate <= dateSearchEnddate)
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
