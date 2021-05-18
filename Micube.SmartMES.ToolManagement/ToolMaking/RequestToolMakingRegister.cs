#region using
using DevExpress.XtraEditors.Repository;

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

namespace Micube.SmartMES.ToolManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 치공구관리 > 치공구제작수정 > 치공구내역등록
    /// 업  무  설  명  : 제작/수정 의뢰된 치공구의 내역을 관리한다.
    /// 생    성    자  : 김용조
    /// 생    성    일  : 2019-07-18
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class RequestToolMakingRegister : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가
        string _searchVendorID;                                       // 설명
        string _searchUserID;
        string _toolRegistType;
        DataTable _toolVersionTable;
        string[] _clipDatas;
        int _clipIndex = 1;
        IDataObject _clipBoardData;
        bool _isGoodCopy = true;
        ConditionItemSelectPopup popupGridToolArea;
        ConditionItemComboBox conditionFactoryBox;
        ConditionItemSelectPopup popupGridToolVendor;
        ConditionItemSelectPopup productPopup;
        #endregion

        #region 생성자

        public RequestToolMakingRegister()
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
            InitializeGridDetail();
            InitializeGridLot();
        }

        #region InitializeGrid - 제작수정의뢰현황목록을 초기화한다.
        /// <summary>        
        /// 제작수정의뢰현황목록을 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdToolRequest.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Export | GridButtonItem.Restore;  // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기            
            grdToolRequest.View.SetIsReadOnly();
            grdToolRequest.View.AddTextBoxColumn("TOOLPROGRESSSTATUSNAME", 80)          //진행상태
                ;
            grdToolRequest.View.AddTextBoxColumn("REQUESTDATE", 120)
                .SetDisplayFormat("yyyy-MM-dd");                                        //의뢰일자
            grdToolRequest.View.AddTextBoxColumn("REQUESTSEQUENCE", 100);               //의뢰순번
            grdToolRequest.View.AddTextBoxColumn("TOOLMAKETYPE")
                .SetIsHidden();                                                         //제작구분
            grdToolRequest.View.AddTextBoxColumn("TOOLMAKETYPENAME", 80)
                .SetTextAlignment(TextAlignment.Center)
                ;                                                                       //제작구분명칭
            grdToolRequest.View.AddTextBoxColumn("PRODUCTDEFID", 100);                  //품목코드
            grdToolRequest.View.AddTextBoxColumn("PRODUCTDEFVERSION", 100)
                ;                                                                       //품목버전
            grdToolRequest.View.AddTextBoxColumn("PRODUCTDEFNAME", 350);                //품목명
            grdToolRequest.View.AddTextBoxColumn("DELIVERYDATE", 120)
                .SetDisplayFormat("yyyy-MM-dd");                                        //납기일자
            grdToolRequest.View.AddTextBoxColumn("ISAPPROVALUSE", 80)                   //승인사용            
                .SetIsHidden()                                                          //2021-01-22 오근영 승인사용 숨김
                .SetTextAlignment(TextAlignment.Center);
            grdToolRequest.View.AddTextBoxColumn("REQUESTUSER", 80)
                .SetIsHidden();                                                         //의뢰자아이디
            grdToolRequest.View.AddTextBoxColumn("USERNAME", 80);                       //의뢰자명
            grdToolRequest.View.AddTextBoxColumn("REQUESTDEPARTMENT", 100);             //의뢰부서명

            //grdToolRequest.View.AddTextBoxColumn("REQUESTQTY", 50)
            //    .SetDisplayFormat("#,##0", MaskTypes.Numeric);                        //의뢰수량
            grdToolRequest.View.AddTextBoxColumn("REQUESTCOMMENT", 250);
            grdToolRequest.View.AddTextBoxColumn("DESCRIPTION", 250);                   //설명
            grdToolRequest.View.AddTextBoxColumn("TOOLPROGRESSSTATUS", 80)
                .SetIsHidden();             //진행상태코드            
            grdToolRequest.View.PopulateColumns();
        }
        #endregion

        #region InitializeGridDetail - 치공구내역목록을 초기화한다.
        /// <summary>        
        /// 치공구내역목록을 초기화한다.
        /// </summary>
        private void InitializeGridDetail()
        {
            _toolVersionTable = GetAllToolVersion();

            grdToolRequestDetail.View.CheckMarkSelection.ShowCheckBoxHeader = false;

            // TODO : 그리드 초기화 로직 추가
            grdToolRequestDetail.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Export | GridButtonItem.Restore;  // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기       
            grdToolRequestDetail.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;            
                        
            grdToolRequestDetail.View.AddTextBoxColumn("TOOLCODE", 120)                        //Tool코드
                .SetIsReadOnly(true);
            grdToolRequestDetail.View.AddTextBoxColumn("TOOLVERSION", 80)                
                .SetIsReadOnly(true);                                                          //Tool Rev.                                                                                  
            grdToolRequestDetail.View.AddTextBoxColumn("TOOLNAME", 350)
                .SetIsReadOnly(true);                                                          //Tool Rev.                                                                                  
            grdToolRequestDetail.View.AddSpinEditColumn("QTY", 80)
                .SetIsReadOnly(true);                                                          //의뢰수량
            //grdToolRequestDetail.View.AddComboBoxColumn("REVDURABLEDEFVERSION", 100, new SqlQuery("GetALlDurableDefVersionListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
            grdToolRequestDetail.View.AddTextBoxColumn("REVDURABLEDEFVERSION", 100)
                ;                                                                               //변경 Tool Rev.            
            grdToolRequestDetail.View.AddDateEditColumn("REQUESTDELIVERYDATE", 120)
                .SetValidationIsRequired()
                .SetDisplayFormat("yyyy-MM-dd", MaskTypes.DateTime)
                ;                                                                              //입고요청일            
            grdToolRequestDetail.View.AddDateEditColumn("PLANDELIVERYDATE", 120)
                .SetValidationIsRequired()
                .SetDisplayFormat("yyyy-MM-dd", MaskTypes.DateTime)                
                ;                                                                              //입고예정일
            grdToolRequestDetail.View.AddTextBoxColumn("VENDORID", 80)
                .SetIsHidden();
            InitializeVendorPopupColumnInDetailGrid();
            //grdToolRequestDetail.View.AddComboBoxColumn("PARTNERS", 150, new SqlQuery("GetVendorListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "VENDORNAME", "VENDORID" )
            //    .SetValidationIsRequired();                                                    //업체
            grdToolRequestDetail.View.AddTextBoxColumn("RECEIPTAREAID", 80)
                .SetIsHidden();
            InitializeAreaPopupColumnInDetailGrid();
            //grdToolRequestDetail.View.AddComboBoxColumn("AREA", 150, new SqlQuery("GetAreaListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "AREANAME", "AREAID")
            //    .SetValidationIsRequired();                                                    //입고작업장
            grdToolRequestDetail.View.AddComboBoxColumn("OWNSHIPTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OwnershipType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                .SetTextAlignment(TextAlignment.Center)
                ;
            grdToolRequestDetail.View.AddTextBoxColumn("USEDFACTOR", 100)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();                                                                //연배
            grdToolRequestDetail.View.AddTextBoxColumn("TOOLCATEGORYID")
                .SetIsHidden();                                                             //Tool구분
            grdToolRequestDetail.View.AddTextBoxColumn("TOOLCATEGORY", 250)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();                                                           //Tool구분명칭
            grdToolRequestDetail.View.AddTextBoxColumn("TOOLCATEGORYDETAILID")
                .SetIsHidden();                                                             //Tool유형
            grdToolRequestDetail.View.AddTextBoxColumn("TOOLCATEGORYDETAIL", 120)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();                                                           //Tool유형명칭
            grdToolRequestDetail.View.AddTextBoxColumn("TOOLDETAILID")
                .SetIsHidden();                                                             //상세유형
            grdToolRequestDetail.View.AddTextBoxColumn("TOOLDETAIL", 150)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();                                                           //상세유형명칭
            grdToolRequestDetail.View.AddSpinEditColumn("USEDLIMIT", 100)
                .SetIsReadOnly();                                                           //보증 타수
            grdToolRequestDetail.View.AddSpinEditColumn("CLEANLIMIT", 100)
                .SetIsReadOnly();                                                           //연마기준 타수
            grdToolRequestDetail.View.AddTextBoxColumn("ENTERPRISEID")
                .SetIsHidden();                                                             
            grdToolRequestDetail.View.AddTextBoxColumn("PLANTID")
                .SetIsHidden();
            grdToolRequestDetail.View.PopulateColumns();

            RepositoryItemLookUpEdit repositoryItems = new RepositoryItemLookUpEdit();
            repositoryItems.DisplayMember = "TOOLVERSION";
            repositoryItems.ValueMember = "TOOLVERSION";
            repositoryItems.DataSource = _toolVersionTable;
            repositoryItems.ShowHeader = false;
            repositoryItems.NullText = "";
            repositoryItems.NullValuePromptShowForEmptyValue = true;
            repositoryItems.PopulateColumns();


            grdToolRequestDetail.View.Columns["REVDURABLEDEFVERSION"].ColumnEdit = repositoryItems;
        }
        #endregion

        #region InitializeAreaPopupColumnInDetailGrid - 작업장을 팝업검색할 필드 설정

        /// <summary>
        /// grid ProcesssegmentidLis
        /// </summary>
        private void InitializeAreaPopupColumnInDetailGrid()
        {
            popupGridToolArea = this.grdToolRequestDetail.View.AddSelectPopupColumn("RECEIPTAREA", 150, new SqlQuery("GetAreaListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"))
                // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
                .SetPopupLayout("RECEIPTAREA", PopupButtonStyles.Ok_Cancel, true, false)
                // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
                .SetPopupResultCount(1)
                //필수값설정
                .SetValidationIsRequired()
                // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
                .SetPopupResultMapping("RECEIPTAREA", "AREANAME")
                // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
                .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                // 그리드의 남은 영역을 채울 컬럼 설정                
                .SetPopupAutoFillColumns("AREANAME")
                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {
                    int i = 0;
                    int currentIndex = grdToolRequestDetail.View.GetFocusedDataSourceRowIndex();

                    foreach (DataRow row in selectedRows)
                    {
                        if (i == 0)
                        {
                            grdToolRequestDetail.View.GetFocusedDataRow()["RECEIPTAREAID"] = row["AREAID"];
                            grdToolRequestDetail.View.GetFocusedDataRow()["RECEIPTAREA"] = row["AREANAME"];
                        }
                        i++;
                    }
                })
            //.SetPopupValidationCustom(ValidationParentAreaIdPopup);
            ;
            // 팝업에서 사용할 조회조건 항목 추가
            popupGridToolArea.Conditions.AddTextBox("AREANAME");
            conditionFactoryBox =  popupGridToolArea.Conditions.AddComboBox("FACTORYID", new SqlQuery("GetFactoryListByEqp", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "FACTORYNAME", "FACTORYID")
                ;

            // 팝업 그리드 설정
            popupGridToolArea.GridColumns.AddTextBoxColumn("AREAID", 100)
                //.SetIsHidden();
                .SetIsReadOnly();                                               //2021-01-22 오근영 작업장ID 추가
            popupGridToolArea.GridColumns.AddTextBoxColumn("AREANAME", 300)
                .SetIsReadOnly();
        }
        #endregion

        #region InitializeVendorPopupColumnInDetailGrid - 제작업체를 팝업검색할 필드 설정

        /// <summary>
        /// grid ProcesssegmentidLis
        /// </summary>
        private void InitializeVendorPopupColumnInDetailGrid()
        {
            popupGridToolVendor = this.grdToolRequestDetail.View.AddSelectPopupColumn("MAKEVENDOR", 150, new SqlQuery("GetVendorListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"))
                // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
                .SetPopupLayout("MAKEVENDOR", PopupButtonStyles.Ok_Cancel, true, false)
                // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
                .SetPopupResultCount(1)
                //필수값설정
                .SetValidationIsRequired()
                // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
                .SetPopupResultMapping("MAKEVENDOR", "VENDORNAME")
                // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
                .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                // 그리드의 남은 영역을 채울 컬럼 설정                
                .SetPopupAutoFillColumns("VENDORNAME")
                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {
                    int i = 0;
                    int currentIndex = grdToolRequestDetail.View.GetFocusedDataSourceRowIndex();

                    foreach (DataRow row in selectedRows)
                    {
                        if (i == 0)
                        {
                            grdToolRequestDetail.View.GetFocusedDataRow()["MAKEVENDOR"] = row["VENDORNAME"];
                            grdToolRequestDetail.View.GetFocusedDataRow()["VENDORID"] = row["VENDORID"];
                        }
                        i++;
                    }
                })
            //.SetPopupValidationCustom(ValidationParentAreaIdPopup);
            ;
            // 팝업에서 사용할 조회조건 항목 추가
            
            popupGridToolVendor.Conditions.AddTextBox("VENDORNAME");

            // 팝업 그리드 설정
            popupGridToolVendor.GridColumns.AddTextBoxColumn("VENDORID", 100)
                //.SetIsHidden();
                .SetIsReadOnly();                                                   //2021-01-22 오근영 거래처 코드 항목 추가
            popupGridToolVendor.GridColumns.AddTextBoxColumn("VENDORNAME", 300)
                .SetIsReadOnly();
        }
        #endregion

        #region InitializeGridLot - 치공구내역 LOT 리스트를 초기화한다.
        /// <summary>        
        /// 치공구내역 LOT 리스트를 초기화한다.
        /// </summary>
        private void InitializeGridLot()
        {
            // TODO : 그리드 초기화 로직 추가
            grdToolRequestLot.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Export | GridButtonItem.Restore;  // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기   
            grdToolRequestLot.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdToolRequestLot.View.SetIsReadOnly(true);

            grdToolRequestLot.View.AddTextBoxColumn("DURABLELOTID", 150)               //Tool코드
                ;
            grdToolRequestLot.View.AddTextBoxColumn("SENDORRECEIPTDATE", 150)               //Tool코드
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss");
            grdToolRequestLot.View.AddTextBoxColumn("AREAID")
                .SetIsHidden();                                                         //사업장아이디
            grdToolRequestLot.View.AddTextBoxColumn("AREANAME", 150)
                ;                                                                       //사업장
            grdToolRequestLot.View.AddTextBoxColumn("USEDCOUNT", 100)
                ;                                                                       //사용타수
            grdToolRequestLot.View.AddTextBoxColumn("TOTALUSEDCOUNT", 100)
                ;                                                                       //누적타수
            grdToolRequestLot.View.AddTextBoxColumn("TOTALCLEANCOUNT", 100)
                ;                                                                       //연마횟수
            grdToolRequestLot.View.AddTextBoxColumn("TOTALREPAIRCOUNT", 100)
                ;                                                                       //수리횟수
            grdToolRequestLot.View.AddTextBoxColumn("DURABLESTATE")
                .SetIsHidden();                                                         //상태아이디
            grdToolRequestLot.View.AddTextBoxColumn("DURABLESTATENAME", 120)
                .SetTextAlignment(TextAlignment.Center)
                ;                                                                       //상태
            grdToolRequestLot.View.PopulateColumns();
        }
        #endregion

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            grdToolRequest.View.FocusedRowChanged += grdToolRequest_FocusedRowChanged;
            grdToolRequestDetail.View.FocusedRowChanged += grdToolRequestDetail_FocusedRowChanged;
            grdToolRequestDetail.View.CellValueChanged += grdToolRequestDetail_CellValueChanged;
            grdToolRequestDetail.View.ShowingEditor += grdToolRequestDetail_ShowingEditor;
            grdToolRequestDetail.View.ShownEditor += grdToolRequestDetail_ShownEditor;

            btnFindToolNo.Click += BtnFindToolNo_Click;
            btnSave.Click += BtnSave_Click;
            btnDeleteLot.Click += BtnDeleteLot_Click;

            Shown += RequestToolMakingRegister_Shown;
        }

        #region RequestToolMakingRegister_Shown - Site관련정보를 화면로딩후 설정한다.
        private void RequestToolMakingRegister_Shown(object sender, EventArgs e)
        {
            ChangeSiteCondition();

            ((SmartComboBox)Conditions.GetControl("P_PLANTID")).EditValueChanged += ConditionPlant_EditValueChanged;
        }
        #endregion

        #region ConditionPlant_EditValueChanged - 검색조건의 Site정보 변경시 관련 쿼리들을 일괄 변경한다.
        private void ConditionPlant_EditValueChanged(object sender, EventArgs e)
        {
            ChangeSiteCondition();
        }
        #endregion

        #region BtnDeleteLot_Click - 치공구Lot을 제거
        private void BtnDeleteLot_Click(object sender, EventArgs e)
        {
            DeleteData();
        }
        #endregion

        #region grdToolRequestDetail_ShownEditor - 특정 컬럼의 동적바인딩
        private void grdToolRequestDetail_ShownEditor(object sender, EventArgs e)
        {
            if (grdToolRequestDetail.View.FocusedColumn.FieldName == "REVDURABLEDEFVERSION")
            {
                string toolCode = grdToolRequestDetail.View.GetFocusedDataRow().GetString("TOOLCODE");

                if (grdToolRequestDetail.View.ActiveEditor != null)
                {
                    ((DevExpress.XtraEditors.LookUpEdit)grdToolRequestDetail.View.ActiveEditor).Properties.DataSource = GetToolVersion(toolCode);
                }
            }
        }
        #endregion

        #region grdToolRequestDetail_ShowingEditor - 그리드의 입력제어
        private void grdToolRequestDetail_ShowingEditor(object sender, CancelEventArgs e)
        {
            //현재 등록상태가 아닌 상태는 수정이 불가능하게 변경 추후 결재 도입시 변경요망
            if(grdToolRequest.View.GetFocusedDataRow().GetString("TOOLPROGRESSSTATUS") == "Request" || grdToolRequest.View.GetFocusedDataRow().GetString("TOOLPROGRESSSTATUS") == "DetailRegist")
            {
                e.Cancel = true;
            }
            else if (grdToolRequestDetail.View.FocusedColumn.FieldName == "REVDURABLEDEFVERSION")
            {
                if (grdToolRequest.View.GetFocusedDataRow().GetString("TOOLMAKETYPE") == "Add" || grdToolRequest.View.GetFocusedDataRow().GetString("TOOLMAKETYPE") == "New" || grdToolRequest.View.GetFocusedDataRow().GetString("TOOLMAKETYPE") == "Repair")
                {
                    e.Cancel = true;
                }
            }
            else if (grdToolRequestDetail.View.FocusedColumn.FieldName == "OWNSHIPTYPE")
            {
                if (grdToolRequest.View.GetFocusedDataRow().GetString("TOOLMAKETYPE") == "Modify" || grdToolRequest.View.GetFocusedDataRow().GetString("TOOLMAKETYPE") == "Repair")
                {
                    e.Cancel = true;
                }
            }
        }
        #endregion

        #region grdToolRequestDetail_CellValueChanged - 그리드의 셀 변경 이벤트
        private void grdToolRequestDetail_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "REQUESTDELIVERYDATE")
            {
                grdToolRequestDetail.View.CellValueChanged -= grdToolRequestDetail_CellValueChanged;

                DataRow row = grdToolRequestDetail.View.GetFocusedDataRow();

                if (row["REQUESTDELIVERYDATE"].ToString().Equals(""))
                {
                    grdToolRequestDetail.View.CellValueChanged += grdToolRequestDetail_CellValueChanged;
                    return;
                }
                DateTime dateBudget = Convert.ToDateTime(row["REQUESTDELIVERYDATE"].ToString());
                grdToolRequestDetail.View.SetFocusedRowCellValue("REQUESTDELIVERYDATE", dateBudget.ToString("yyyy-MM-dd"));// 예산일자
                grdToolRequestDetail.View.CellValueChanged += grdToolRequestDetail_CellValueChanged;
            }
            else if (e.Column.FieldName == "PLANDELIVERYDATE")
            {
                grdToolRequestDetail.View.CellValueChanged -= grdToolRequestDetail_CellValueChanged;

                DataRow row = grdToolRequestDetail.View.GetFocusedDataRow();
                
                if (row["PLANDELIVERYDATE"].ToString().Equals(""))
                {
                    grdToolRequestDetail.View.CellValueChanged += grdToolRequestDetail_CellValueChanged;
                    return;
                }
                DateTime dateBudget = Convert.ToDateTime(row["PLANDELIVERYDATE"].ToString());
                grdToolRequestDetail.View.SetFocusedRowCellValue("PLANDELIVERYDATE", dateBudget.ToString("yyyy-MM-dd"));// 예산일자
                grdToolRequestDetail.View.CellValueChanged += grdToolRequestDetail_CellValueChanged;
            }
            else if (e.Column.FieldName == "MAKEVENDOR")
            {
                grdToolRequestDetail.View.CellValueChanged -= grdToolRequestDetail_CellValueChanged;

                DataRow row = grdToolRequestDetail.View.GetDataRow(e.RowHandle);

                if (row["MAKEVENDOR"].ToString().Equals(""))
                {
                    grdToolRequestDetail.View.CellValueChanged += grdToolRequestDetail_CellValueChanged;
                    return;
                }
                GetSingleMakeVendor(row.GetString("MAKEVENDOR"), e.RowHandle);
                grdToolRequestDetail.View.CellValueChanged += grdToolRequestDetail_CellValueChanged;
            }
            else if (e.Column.FieldName == "RECEIPTAREA")
            {
                grdToolRequestDetail.View.CellValueChanged -= grdToolRequestDetail_CellValueChanged;

                DataRow row = grdToolRequestDetail.View.GetDataRow(e.RowHandle);

                if (row["RECEIPTAREA"].ToString().Equals(""))
                {
                    grdToolRequestDetail.View.CellValueChanged += grdToolRequestDetail_CellValueChanged;
                    return;
                }
                GetSingleReceiptArea(row.GetString("RECEIPTAREA"), e.RowHandle);
                grdToolRequestDetail.View.CellValueChanged += grdToolRequestDetail_CellValueChanged;
            }
        }
        #endregion

        #region BtnSave_Click - 저장버튼이벤트
        private void BtnSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }
        #endregion

        #region BtnFindToolNo_Click - 내역등록을 입력할 치공구를 조회한다.
        private void BtnFindToolNo_Click(object sender, EventArgs e)
        {
            //SaveLotData();
        }
        #endregion

        #region grdToolRequest_FocusedRowChanged - 그리드의 행선택 이벤트
        private void grdToolRequest_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DisplayToolRequestInfo();
        }
        #endregion

        #region grdToolRequestDetail_FocusedRowChanged - 상세내역그리드의 행선택이벤트
        private void grdToolRequestDetail_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            SearchLotList();
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

            InitializeInsertForm();
            // TODO : 조회 SP 변경
            var values = Conditions.GetValues();

            #region 기간 검색형 전환 처리 - 만약 시간까지 포함하여 검색한다면 아래 문구는 필요없음            
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            #endregion
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable toolReqSearchResult = SqlExecuter.Query("GetToolRequestListForDetailByTool", "10001", values);

            grdToolRequest.DataSource = toolReqSearchResult;

            if (toolReqSearchResult.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                grdToolRequestDetail.DataSource = null;
                grdToolRequestLot.DataSource = null;
            }
            else
            {
                grdToolRequest.View.FocusedRowHandle = 0;
                DisplayToolRequestInfo();                
            }
        }

        #region Research - 재검색(데이터 입력후와 같은 경우에 사용)
        /// <summary>
        /// 저장 삭제후 재 검색시 사용
        /// 생성 및 수정시 발생한 아이디를 통해 자동 선택되도록 유도하며 삭제시에는 null로 매개변수를 받아서 첫번째 행을 보여준다. 행이 없다면 빈 값으로 설정
        /// </summary>
        /// <param name="inboudNo">입력 및 수정시 발생한 값의 아이디.</param>
        private void Research(string requestSequence, string requestDate)
        {
            // TODO : 조회 SP 변경
            InitializeInsertForm();

            var values = Conditions.GetValues();

            #region 기간 검색형 전환 처리 - 만약 시간까지 포함하여 검색한다면 아래 문구는 필요없음            
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);

            #endregion
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable sparePartSearchResult = SqlExecuter.Query("GetToolRequestListForDetailByTool", "10001", values);

            if (sparePartSearchResult.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                grdToolRequestDetail.DataSource = null;
                grdToolRequestLot.DataSource = null;
            }

            grdToolRequest.DataSource = sparePartSearchResult;

            //현재 작업한 결과를 선택할 수 있도록 한다.
            if (requestSequence == null && requestDate == null) //삭제 작업후
            {
                //값이 하나도 없다면 빈 화면
                if (grdToolRequest.View.RowCount == 0)
                {
                    //입력대기 화면으로 설정
                    InitializeInsertForm();
                }
                else
                {
                    //첫번재 값으로 설정
                    DisplayToolRequestInfoDetail(0);
                }
            }
            else //값이 있다면 해당 값에 맞는 행을 찾아서 선택
            {
                DateTime requestDateValue = Convert.ToDateTime(requestDate);
                int prevIndex = GetRowHandleInGrid(grdToolRequest, "REQUESTDATE", "REQUESTSEQUENCE", requestDateValue.ToString("yyyy-MM-dd"), requestSequence);

                if (prevIndex > -1)
                {
                    DisplayToolRequestInfoDetail(prevIndex);

                    SearchDetailList(null, null, null, null);
                    SearchLotList();
                }
                else
                {
                    //첫번재 값으로 설정
                    DisplayToolRequestInfoDetail(0);
                }
            }
        }
        #endregion

        #region SearchDetailList - 치공구제작의뢰중 제작목록을 조회
        /// <summary>
        /// 치공구제작의뢰중 제작목록을 조회
        /// PK : DurableDefID, DurableDefVersion, RequestDate, RequestSequence
        /// </summary>
        /// <param name="inboudNo">입력 및 수정시 발생한 값의 아이디.</param>
        private void SearchDetailList(string durableDefID, string durableDefVersion, string requestDate, string requestSequence)
        {
            if (grdToolRequest.View.FocusedRowHandle > -1)
            {
                Dictionary<string, object> Param = new Dictionary<string, object>();

                DataRow currentRow = grdToolRequest.View.GetFocusedDataRow();

                Param.Add("REQUESTSEQUENCE", currentRow["REQUESTSEQUENCE"]);
                Param.Add("REQUESTDATE", currentRow["REQUESTDATE"]);
                Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                Param = Commons.CommonFunction.ConvertParameter(Param);
                DataTable resultTable = SqlExecuter.Query("GetToolRequestDetailListForDetailByTool", "10001", Param);

                grdToolRequestDetail.DataSource = resultTable;

                if (durableDefID != null) //데이터 입력후 조회할 건이 있을 경우
                {
                    grdToolRequestDetail.View.FocusedRowHandle = GetRowHandleInGrid(grdToolRequestDetail, "TOOLCODE", "TOOLVERSION", "REQUESTDATE", "REQUESTSEQUENCE", durableDefID, durableDefVersion, requestDate, requestSequence);
                    SearchLotList();
                }
                else if (resultTable.Rows.Count > 0) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
                {
                    grdToolRequestDetail.View.FocusedRowHandle = 0;
                    SearchLotList();
                }
                else
                {
                    SearchLotList();
                }

                //Validation 설정
                if (currentRow.GetString("TOOLMAKETYPE") == "Modify")
                {
                    //btnFindToolNo.Visible = false;
                    _toolRegistType = "UPDATE";         //치공구 수정/수리
                    grdToolRequestDetail.View.Columns["REVDURABLEDEFVERSION"].AppearanceHeader.ForeColor = Color.Red;
                    grdToolRequestDetail.View.Columns["OWNSHIPTYPE"].AppearanceHeader.ForeColor = Color.Black;                    
                }
                else if (currentRow.GetString("TOOLMAKETYPE") == "Repair")
                {
                    _toolRegistType = "REPAIR";         //치공구 수정/수리
                    grdToolRequestDetail.View.Columns["REVDURABLEDEFVERSION"].AppearanceHeader.ForeColor = Color.Black;
                    grdToolRequestDetail.View.Columns["OWNSHIPTYPE"].AppearanceHeader.ForeColor = Color.Black;
                }
                else
                {
                    //btnFindToolNo.Visible = true;
                    _toolRegistType = "CREATE";         //치공구 신규/제작
                    grdToolRequestDetail.View.Columns["REVDURABLEDEFVERSION"].AppearanceHeader.ForeColor = Color.Black;
                    grdToolRequestDetail.View.Columns["OWNSHIPTYPE"].AppearanceHeader.ForeColor = Color.Black;
                }
            }
        }
        #endregion

        #region SearchLotList - 치공구제작의뢰중 수정목록을 조회
        /// <summary>
        /// 치공구제작의뢰중 수정목록을 조회
        /// </summary>
        /// <param name="inboudNo">입력 및 수정시 발생한 값의 아이디.</param>
        private void SearchLotList()
        {
            if (grdToolRequestDetail.View.FocusedRowHandle > -1)
            {
                Dictionary<string, object> Param = new Dictionary<string, object>();

                DataRow currentRow = grdToolRequest.View.GetFocusedDataRow();
                DataRow currentDetailRow = grdToolRequestDetail.View.GetFocusedDataRow();

                if (currentDetailRow != null)
                {                    
                    Param.Add("REQUESTSEQUENCE", currentRow["REQUESTSEQUENCE"]);
                    Param.Add("REQUESTDATE", currentRow["REQUESTDATE"]);
                    Param.Add("DURABLEDEFID", currentDetailRow["TOOLCODE"]);
                    Param.Add("DURABLEDEFVERSION", currentDetailRow["TOOLVERSION"]);
                    Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                    Param = Commons.CommonFunction.ConvertParameter(Param);
                    DataTable resultTable = SqlExecuter.Query("GetToolRequestDetailLotListForDetailByTool", "10001", Param);

                    grdToolRequestLot.DataSource = resultTable;
                }
                else
                {
                    grdToolRequestLot.DataSource = null;
                }
            }
        }
        #endregion
        
        #region 조회조건 제어
        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            //InitializeConditionPlant();
            InitializeConditionRegistType();
            InitializeConditionToolMakeType();
            InitializeConditionToolCodePopup();
            InitializeConditionToolNumberExists();            
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
            SmartPeriodEdit period = Conditions.GetControl<SmartPeriodEdit>("P_REQUESTDATE");
            //period.datePeriodFr.EditValue = DateTime.Now.ToString("yyyy-MM-01");
            //period.datePeriodTo.EditValue = DateTime.Now.AddMonths(1).ToString("yyyy-MM-01");
            period.comboPeriod.EditValue = "THISMONTH";
        }

        /// <summary>
        /// site 설정 
        /// </summary>
        private void InitializeConditionPlant()
        {
            var planttxtbox = Conditions.AddComboBox("PLANTID", new SqlQuery("GetPlantList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PLANTID", "PLANTID")
               .SetLabel("PLANT")
               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
               .SetPosition(0.1)
               .SetIsReadOnly(true)
               .SetDefault(UserInfo.Current.Plant, "PLANTID") //기본값 설정 UserInfo.Current.Plant
            ;
        }

        private void InitializeConditionRegistType()
        {
            var planttxtbox = Conditions.AddComboBox("TOOLREGISTTYPE", new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CODECLASSID=ToolRegistType"), "CODENAME", "CODEID")
               .SetLabel("TOOLREGISTTYPE")
               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
               .SetPosition(0.2)
               .SetValidationIsRequired()
               .SetDefault("NotRegist", "CODEID")
            ;
        }

        /// <summary>
        /// 제작구분 설정
        /// </summary>
        private void InitializeConditionToolMakeType()
        {
            var planttxtbox = Conditions.AddComboBox("TOOLMAKETYPE", new SqlQuery("GetToolMakeTypeCodeByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "TOOLMAKETYPENAME", "TOOLMAKETYPE")
               .SetLabel("TOOLMAKETYPE")
               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
               .SetPosition(0.3)
               .SetEmptyItem("","",true)
            ;
        }

        /// <summary>
        /// 품목코드 조회 설정
        /// </summary>
        private void InitializeConditionToolCodePopup()
        {
            productPopup = Conditions.AddSelectPopup("PRODUCTDEFID", new SqlQuery("GetProductdefidPoplistByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"PRODUCTDEFTYPE=Product"))
            .SetPopupLayout("PRODUCTDEFIDPOPUP", PopupButtonStyles.Ok_Cancel, true, false)
            .SetPopupLayoutForm(984, 465, FormBorderStyle.FixedToolWindow)
            .SetPopupResultMapping("PRODUCTDEFID", "PRODUCTDEFID")
            .SetLabel("PRODUCTDEF")
            .SetPopupResultCount(1)
            .SetPosition(2.5)
            ;

            productPopup.ValueFieldName = "PRODUCTDEFID";
            productPopup.DisplayFieldName = "PRODUCTDEFNAME";


            // 팝업 조회조건
            productPopup.Conditions.AddTextBox("PRODUCTDEF")
                .SetLabel("PRODUCTDEF");
            //productPopup.Conditions.AddComboBox("PRODUCTDEFTYPE", new SqlQuery("GetCodeListByOspProductdeftype", "10001", new Dictionary<string, object>() {{"LANGUAGETYPE", UserInfo.Current.LanguageType}}), "CODENAME", "CODEID")
            //    .SetEmptyItem("", "", true)
            //    .SetLabel("PRODUCTDEFTYPE")
            //    .ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly
            //    ;
            //productPopup.Conditions.AddTextBox("PRODUCTDEFID")
            //    .SetLabel("PRODUCTDEFID");


            // 팝업 그리드
            productPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 250)
                .SetIsReadOnly();
            productPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 80)
                .SetIsReadOnly();
            productPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 350)
                .SetIsReadOnly();
            productPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFCODE", 50)
                .SetIsHidden();
            productPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFTYPE", 200)
                .SetIsReadOnly();
        }

        /// <summary>
        /// 제작구분 설정
        /// </summary>
        private void InitializeConditionToolNumberExists()
        {
            var planttxtbox = Conditions.AddComboBox("TOOLNOEXISTS", new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CODECLASSID=YesNo"), "CODENAME", "CODEID")
               .SetLabel("TOOLNOEXISTS")
               .SetEmptyItem("", "", true)
               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
               .SetPosition(0.5)
               .SetEmptyItem("","",true)
            ;
        }
        #endregion

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();            
        }

        #region ValidateContent : 데이터검증
        /// <summary>
        /// 입력/수정 시의 Validation 체크
        /// 입력/수정 각각의 체크값이 서로 다르다면 상태에 따른 분기가 있어야 한다.
        /// </summary>
        /// <returns></returns>
        private bool ValidateContent(out string messageCode)
        {
            messageCode = "";
            //각각의 상태에 따라 보여줄 메세지가 상이하다면 각각의 분기에서 메세지를 표현해야 한다.            
            //회사아이디는 필수이다.
            if (UserInfo.Current.Enterprise == "")
            {
                messageCode = "ToolRegistValidation";
                return false;
            }

            //PlantID는 필수이다.
            if (!ValidateEditValue(Conditions.GetValue("P_PLANTID")))
            {
                messageCode = "ToolRegistValidation";
                return false;
            }

            DataTable dataTables = (DataTable)grdToolRequestDetail.DataSource;

            //신규/제작 과 수정/수리의 데이터 검증은 서로 다르다.
            if (_toolRegistType == "UPDATE")
            {
                //수리의 경우 rev durabledefVersion을 입력하지 않는다.
                foreach (DataRow currentRow in dataTables.Rows)
                {
                    if (!ValidateCellInGrid(currentRow, new string[] { "REQUESTDELIVERYDATE", "PLANDELIVERYDATE", "VENDORID", "RECEIPTAREAID", "REVDURABLEDEFVERSION" }))
                    {
                        messageCode = "ToolRegistValidation";
                        return false;
                    }
                    //수정출고의 경우 버전이 서로 달라야 한다.
                    if (currentRow.GetString("TOOLVERSION").Equals(currentRow.GetString("REVDURABLEDEFVERSION")))
                    {
                        messageCode = "ValidateUpdateToolRevisionCompare";
                        return false;
                    }
                }


            }
            else
            {
                foreach (DataRow currentRow in dataTables.Rows)
                {
                    if (!ValidateCellInGrid(currentRow, new string[] { "REQUESTDELIVERYDATE", "PLANDELIVERYDATE", "VENDORID", "RECEIPTAREAID" }))
                    {
                        messageCode = "ToolRegistValidation";
                        return false;
                    }
                }
            }

            return true;
        }
        #endregion

        #region ValidationSave : 저장 가능한지 검증
        /// <summary>
        /// 현재 상태값에 맞추어 저장을 할 수 있는지 판단한다.
        /// </summary>
        /// <returns></returns>
        private bool ValidationSave()
        {
            DataRow currentRow = grdToolRequest.View.GetFocusedDataRow();
            //Approval, Create, DetailRegist, Request           

            //승인상태가 아닐때는 Create, DetailRegist에서만 수정이 가능하다.
            if (currentRow["ISAPPROVALUSE"].ToString() == "N")
            {
                if (currentRow["TOOLPROGRESSSTATUS"].ToString() == "Create")
                    return true;

                //내역등록상태일때는 본인이 제작의뢰한 당사자일때만 수정이 가능하다.
                if (currentRow["TOOLPROGRESSSTATUS"].ToString() == "DetailRegist")
                {
                    if (currentRow["REQUESTUSER"].ToString() == UserInfo.Current.Id)
                        return true;
                    else
                        return false;
                }
            }
            else //승인상태일때에는 Approval일때만 수정이 가능하다.
            {
                if (currentRow["TOOLPROGRESSSTATUS"].ToString() == "Approval")
                    return true;
            }

            //DurableLot이 있으면 저장을 수행하지 않는다.
            if (grdToolRequestLot.View.RowCount > 0)
                return false;

            return false;
        }
        #endregion

        #region ValidationCellInGrid : 그리드의 특정셀에 대한 검증
        /// <summary>
        /// 그리드의 특정셀에 대한 검증
        /// </summary>
        /// <param name="currentRow"></param>
        /// <param name="columnNames"></param>
        /// <returns></returns>
        private bool ValidateCellInGrid(DataRow currentRow, string[] columnNames)
        {
            foreach (string columnName in columnNames)
            {
                if (currentRow[columnName] == null)
                    return false;
                if (currentRow[columnName].ToString() == "")
                    return false;
            }

            return true;
        }
        #endregion

        #region ValidateComboBoxEqualValue : 2개의 콤보박스의 값이 같은지 검증(같으면 false)
        /// <summary>
        /// 2개의 콤보박스에 데이터가 모두 있어야 하며
        /// 2개의 콤보박스의 데이터가 서로 동일하면 안된다.
        /// </summary>
        /// <param name="originBox"></param>
        /// <param name="targetBox"></param>
        /// <returns></returns>
        private bool ValidateComboBoxEqualValue(SmartComboBox originBox, SmartComboBox targetBox)
        {
            //두 콤보박스의 값이 같으면 안된다.
            if (originBox.EditValue.Equals(targetBox.EditValue))
                return false;
            return true;
        }
        #endregion

        #region ValidateNumericBox : 숫자를 입력하는 컨트롤의 검증
        /// <summary>
        /// 숫자를 입력하는 컨트롤을 대상으로 하여 입력받은 기준값보다 값이 같거나 크다면 true 작다면 false를 반환하는 메소드
        /// </summary>
        /// <param name="originBox"></param>
        /// <param name="ruleValue"></param>
        /// <returns></returns>
        private bool ValidateNumericBox(SmartTextBox originBox, int ruleValue)
        {
            //값이 없으면 안된다.
            if (originBox.EditValue == null)
                return false;

            int resultValue = 0;

            //입력받은 기준값(예를 들어 0)보다 작다면 false를 반환
            if (Int32.TryParse(originBox.EditValue.ToString(), out resultValue))
                if (resultValue <= ruleValue)
                    return false;

            //모두 통과했으므로 Validation Check완료
            return true;
        }
        #endregion

        #region ValidateEditValue : 컨트롤에 값이 입력되어 있지 않은지 검증
        /// <summary>
        /// 입력받은 editValue에 아무런 값이 입력되지 않았다면 false 입력받았다면 true
        /// </summary>
        /// <param name="editValue"></param>
        /// <returns></returns>
        private bool ValidateEditValue(object editValue)
        {
            if (editValue == null)
                return false;

            if (editValue.ToString() == "")
                return false;

            return true;
        }
        #endregion
        #endregion

        #region Private Function

        #region InitializeInsertForm : 입고등록하기 위한 화면 초기화
        /// <summary>
        /// 입고등록정보를 입력하기 위해 화면을 초기화 한다.
        /// </summary>
        private void InitializeInsertForm()
        {
            try
            {
                grdToolRequestDetail.DataSource = null;
                grdToolRequestLot.DataSource = null;
            }
            catch (Exception err)
            {
                ShowError(err);
            }
        }
        #endregion

        #region controlEnableProcess : 입력/수정/삭제를 위한 화면내 컨트롤들의 Enable 제어
        /// <summary>
        /// 진행상태값에 따른 입력 항목 lock 처리
        /// </summary>
        /// <param name="blProcess"></param>
        private void ControlEnableProcess(string currentStatus)
        {
            if (currentStatus == "added") //초기화 버튼을 클릭한 경우
            {
            }
            else if (currentStatus == "modified") //
            {
            }
            else
            {
            }
        }
        #endregion

        #region DisplaySparePartInboundInfo : 그리드에서 행 선택시 상세정보를 표시
        /// <summary>
        /// 그리드에서 행 선택시 상세정보를 표시하는 메소드
        /// </summary>
        private void DisplayToolRequestInfo()
        {
            //포커스 행 체크 
            if (grdToolRequest.View.FocusedRowHandle < 0) return;

            DisplayToolRequestInfoDetail(grdToolRequest.View.FocusedRowHandle);
        }
        #endregion

        #region DisplaySparePartInboundInfoDetail : 입력받은 인덱스의 그리드의 내용을 화면에 바인딩
        /// <summary>
        /// 입력받은 그리드내의 인덱스에 대한 내용을 화면에 표시한다.
        /// </summary>
        /// <param name="rowHandle"></param>
        private void DisplayToolRequestInfoDetail(int rowHandle)
        {
            //해당 업무에 맞는 Enable체크 수행
            DataRow toolReqInfo = grdToolRequest.View.GetDataRow(rowHandle);
            grdToolRequest.View.FocusedRowHandle = rowHandle;

            SearchDetailList(null, null, null, null);
        }
        #endregion

        #region CreateSaveToolRequestDatatable : ToolRequestDetail 입력/수정/삭제를 위한 DataTable의 Template 생성
        /// <summary>
        /// 데이터 입력/수정/삭제를 위해 서버로 전송하는 데이터테이블을 반환한다.
        /// </summary>
        /// <returns></returns>
        private DataTable CreateSaveToolRequestDatatable()
        {
            DataTable dt = new DataTable();
            dt.TableName = "toolRequestDetail";
            dt.Columns.Add("REQUESTDATE");
            dt.Columns.Add("REQUESTSEQUENCE");
            dt.Columns.Add("CREATOR");
            dt.Columns.Add("CREATEDTIME");
            dt.Columns.Add("MODIFIER");
            dt.Columns.Add("MODIFIEDTIME");
            dt.Columns.Add("LASTTXNHISTKEY");
            dt.Columns.Add("LASTTXNID");
            dt.Columns.Add("LASTTXNUSER");
            dt.Columns.Add("LASTTXNTIME");
            dt.Columns.Add("LASTTXNCOMMENT");
            dt.Columns.Add("VALIDSTATE");
            dt.Columns.Add("_STATE_");

            return dt;
        }
        #endregion

        #region CreateToolRequestDetailSaveDatatable : ToolRequestDetail 입력/수정/삭제를 위한 DataTable의 Template 생성
        /// <summary>
        /// 데이터 입력/수정/삭제를 위해 서버로 전송하는 데이터테이블을 반환한다.
        /// </summary>
        /// <returns></returns>
        private DataTable CreateSaveToolRequestDetailDatatable()
        {
            DataTable dt = new DataTable();
            dt.TableName = "toolRequestDetailList";
            dt.Columns.Add("REQUESTDATE");
            dt.Columns.Add("REQUESTSEQUENCE");
            dt.Columns.Add("DURABLEDEFID");
            dt.Columns.Add("DURABLEDEFVERSION");
            dt.Columns.Add("ENTERPRISEID");
            dt.Columns.Add("PLANTID");
            dt.Columns.Add("QTY");
            dt.Columns.Add("REQUESTDELIVERYDATE");
            dt.Columns.Add("PLANDELIVERYDATE");
            dt.Columns.Add("VENDORID");
            dt.Columns.Add("AREAID");
            dt.Columns.Add("OWNSHIPTYPE");
            dt.Columns.Add("REVDURABLEDEFVERSION");
            dt.Columns.Add("CREATOR");
            dt.Columns.Add("CREATEDTIME");
            dt.Columns.Add("MODIFIER");
            dt.Columns.Add("MODIFIEDTIME");
            dt.Columns.Add("LASTTXNHISTKEY");
            dt.Columns.Add("LASTTXNID");
            dt.Columns.Add("LASTTXNUSER");
            dt.Columns.Add("LASTTXNTIME");
            dt.Columns.Add("LASTTXNCOMMENT");
            dt.Columns.Add("VALIDSTATE");
            dt.Columns.Add("_STATE_");

            return dt;
        }
        #endregion

        #region CreateToolRequestDetailLotSaveDatatable : ToolRequestDetail 입력/수정/삭제를 위한 DataTable의 Template 생성
        /// <summary>
        /// 데이터 입력/수정/삭제를 위해 서버로 전송하는 데이터테이블을 반환한다.
        /// </summary>
        /// <returns></returns>
        private DataTable CreateSaveToolRequestDetailLotDatatable()
        {
            DataTable dt = new DataTable();
            dt.TableName = "toolRequestDetailLot";
            dt.Columns.Add("REQUESTDATE");
            dt.Columns.Add("REQUESTSEQUENCE");
            dt.Columns.Add("DURABLEDEFID");
            dt.Columns.Add("DURABLEDEFVERSION");
            dt.Columns.Add("DURABLELOTID");
            dt.Columns.Add("TOOLNUMBER");
            dt.Columns.Add("ENTERPRISEID");
            dt.Columns.Add("PLANTID");
            dt.Columns.Add("CREATOR");
            dt.Columns.Add("CREATEDTIME");
            dt.Columns.Add("MODIFIER");
            dt.Columns.Add("MODIFIEDTIME");
            dt.Columns.Add("LASTTXNHISTKEY");
            dt.Columns.Add("LASTTXNID");
            dt.Columns.Add("LASTTXNUSER");
            dt.Columns.Add("LASTTXNTIME");
            dt.Columns.Add("LASTTXNCOMMENT");
            dt.Columns.Add("VALIDSTATE");
            dt.Columns.Add("_STATE_");

            return dt;
        }
        #endregion

        #region SaveData : 입력/수정을 수행
        private void SaveData()
        {
            //Validation 체크 부분 작성 필요
            this.ShowWaitArea();

            //저장 로직
            try
            {
                btnFindToolNo.Enabled = false;
                btnSave.Enabled = false;

                if (grdToolRequest.View.GetFocusedDataRow() != null)
                {
                    string messageCode = "";
                    //Validation Check시 입력/수정시 검사할 값이 다르다면 수정필요
                    if (ValidateContent(out messageCode))
                    {
                        if (ValidationSave())
                        {
                            DataSet toolRequestSet = new DataSet();
                            DataTable toolReqData = CreateSaveToolRequestDatatable();
                            DataTable toolReqDetailData = CreateSaveToolRequestDetailDatatable();

                            DataTable focusTable = (DataTable)grdToolRequestDetail.DataSource;
                            DataRow parentRow = grdToolRequest.View.GetFocusedDataRow();

                            //ToolRequest
                            DataRow toolReqRow = toolReqData.NewRow();
                            toolReqRow["REQUESTDATE"] = parentRow["REQUESTDATE"];
                            toolReqRow["REQUESTSEQUENCE"] = parentRow["REQUESTSEQUENCE"];
                            toolReqRow["MODIFIER"] = UserInfo.Current.Id;
                            toolReqRow["MODIFIEDTIME"] = DateTime.Now.ToString();
                            toolReqRow["_STATE_"] = "modified";
                            toolReqRow["VALIDSTATE"] = "Valid";
                            toolReqData.Rows.Add(toolReqRow);

                            toolRequestSet.Tables.Add(toolReqData);

                            //ToolRequestDetail
                            foreach (DataRow focusRow in focusTable.Rows)
                            {
                                DataRow savedRow = toolReqDetailData.NewRow();

                                savedRow["REQUESTDATE"] = parentRow["REQUESTDATE"];
                                savedRow["REQUESTSEQUENCE"] = parentRow["REQUESTSEQUENCE"];
                                savedRow["DURABLEDEFID"] = focusRow["TOOLCODE"];
                                savedRow["DURABLEDEFVERSION"] = focusRow["TOOLVERSION"];
                                savedRow["ENTERPRISEID"] = focusRow["ENTERPRISEID"];
                                savedRow["PLANTID"] = focusRow["PLANTID"];
                                savedRow["AREAID"] = focusRow["RECEIPTAREAID"];
                                savedRow["VENDORID"] = focusRow["VENDORID"];
                                savedRow["OWNSHIPTYPE"] = focusRow["OWNSHIPTYPE"];
                                savedRow["QTY"] = focusRow["QTY"];
                                savedRow["REQUESTDELIVERYDATE"] = focusRow["REQUESTDELIVERYDATE"];
                                savedRow["PLANDELIVERYDATE"] = focusRow["PLANDELIVERYDATE"];
                                savedRow["REVDURABLEDEFVERSION"] = focusRow["REVDURABLEDEFVERSION"];
                                savedRow["MODIFIER"] = UserInfo.Current.Id;
                                savedRow["MODIFIEDTIME"] = DateTime.Now.ToString();

                                savedRow["_STATE_"] = "modified";
                                savedRow["VALIDSTATE"] = "Valid";

                                toolReqDetailData.Rows.Add(savedRow);
                            }

                            if (toolReqDetailData.Rows.Count > 0)
                            {
                                toolRequestSet.Tables.Add(toolReqDetailData);

                                //Lot Grid에 행이 존재한다면 입력할 필요가 없다.
                                if (grdToolRequestLot.View.RowCount == 0)
                                {
                                    //ToolRequestDetailLot
                                    //Lot생성을 동시진행
                                    DataTable lotData = GetSaveLotData();

                                    if (lotData != null)
                                        toolRequestSet.Tables.Add(lotData);
                                }
                                DataTable resultTable = this.ExecuteRule<DataTable>("ToolRequestDetail", toolRequestSet);

                                ////Server에서 결과값을 현재는 반환하지 않음 만약 반환해야 한다면 아래로직 구현
                                DataRow resultRow = resultTable.Rows[0];

                                ControlEnableProcess("modified");

                                ////가져온 결과값에 따라 재검색한 후 가져온 아이디에 맞는 행을 선택으로 변경한다.
                                //PK : DurableDefID, DurableDefVersion, RequestDate, RequestSequence
                                Research(resultRow.GetString("REQUESTSEQUENCE"), resultRow.GetString("REQUESTDATE"));
                                SearchDetailList(resultRow.GetString("TOOLCODE"), resultRow.GetString("TOOLVERSION"), resultRow.GetString("REQUESTDATE"), resultRow.GetString("REQUESTSEQUENCE"));
                            }
                            else
                            {
                                this.ShowMessage(MessageBoxButtons.OK, "NoDataChanged", "");
                            }
                        }
                        else
                        {
                            this.ShowMessage(MessageBoxButtons.OK, "ToolRegistStatusValidation", "");
                        }
                    }
                    else
                    {
                        this.ShowMessage(MessageBoxButtons.OK, messageCode, "");
                    }
                }
                else
                {
                    //ShowMessage(MessageBoxButtons.OK, String.Format("SelectItem", Language.GetDictionary("DURABLE")), "");
                    ShowMessage(MessageBoxButtons.OK, "SelectItem", Language.Get("TOOLREQUESTSTATUS"));
                }
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
            finally
            {
                this.CloseWaitArea();
                btnFindToolNo.Enabled = true;
                btnSave.Enabled = true;
            }
        }
        #endregion

        #region GetSaveLotData : Tool번호생성을 위한 ToolRequestDetailLOT 데이터를 생성
        private DataTable GetSaveLotData()
        {
            //Validation 체크 부분 작성 필요
            this.ShowWaitArea();

            //저장 로직
            try
            {

                btnFindToolNo.Enabled = false;
                btnSave.Enabled = false;
                string messageCode = "";
                //Validation Check시 입력/수정시 검사할 값이 다르다면 수정필요
                if (ValidateContent(out messageCode))
                {
                    //이미 Tool번호가 생성되었다면 수행하지 않는다.
                    if (GetLotItemCount())
                    {
                        //자격여부
                        if (ValidationSave())
                        {
                            DataSet toolRequestSet = new DataSet();

                            DataTable toolReqLotData = CreateSaveToolRequestDetailLotDatatable();

                            DataTable focusTable = (DataTable)grdToolRequestDetail.DataSource;

                            DataRow parentRow = grdToolRequest.View.GetFocusedDataRow();

                            foreach (DataRow focusRow in focusTable.Rows)
                            {
                                //입력된 수량만큼 반복
                                int qtyCount = focusRow.GetInteger("QTY");

                                for (int i = 0; i < qtyCount; i++)
                                {
                                    DataRow savedRow = toolReqLotData.NewRow();

                                    savedRow["REQUESTDATE"] = parentRow["REQUESTDATE"];
                                    savedRow["REQUESTSEQUENCE"] = parentRow["REQUESTSEQUENCE"];
                                    savedRow["DURABLEDEFID"] = focusRow["TOOLCODE"];
                                    savedRow["DURABLEDEFVERSION"] = focusRow["TOOLVERSION"];
                                    savedRow["TOOLNUMBER"] = ""; //Server에서 생성
                                    savedRow["DURABLELOTID"] = ""; //Server에서 생성
                                    savedRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                                    savedRow["PLANTID"] = Conditions.GetValue("P_PLANTID");

                                    savedRow["CREATOR"] = UserInfo.Current.Id;
                                    savedRow["CREATEDTIME"] = DateTime.Now.ToString();

                                    //무조건 입력이라고 상정한다.
                                    savedRow["_STATE_"] = "added";
                                    savedRow["VALIDSTATE"] = "Valid";

                                    toolReqLotData.Rows.Add(savedRow);
                                }
                            }

                            return toolReqLotData;
                            //toolRequestSet.Tables.Add(toolReqLotData);
                            ////데이터를 반환받을 필요가 없음
                            //this.ExecuteRule<DataTable>("ToolRequestDetailLot", toolRequestSet);

                            //ControlEnableProcess("modified");

                            ////부모가 되는 Detail에 관계된 Lot을 조회한다.
                            //SearchLotList();
                        }
                        else
                        {
                            this.ShowMessage(MessageBoxButtons.OK, "ToolRegistStatusValidation", "");
                            return null;
                        }
                    }
                    else
                    {
                        this.ShowMessage(MessageBoxButtons.OK, "ToolNumberCreated", "");
                        return null;
                    }
                }
                else
                {
                    this.ShowMessage(MessageBoxButtons.OK, messageCode, "");
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.CloseWaitArea();
                btnFindToolNo.Enabled = true;
                btnSave.Enabled = true;
            }
        }
        #endregion

        #region DeleteData : 삭제를 수행
        private void DeleteData()
        {
            //Validation 체크 부분 작성 필요

            this.ShowWaitArea();

            //저장 로직
            try
            {
                btnSave.Enabled = false;
                btnFindToolNo.Enabled = false;

                if (grdToolRequest.View.GetFocusedDataRow() != null)
                {
                    DialogResult result = this.ShowMessage(MessageBoxButtons.YesNo, "OspDoProcessWant", btnDeleteLot.Text);//삭제하시겠습니까? 

                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        DataSet toolRequestSet = new DataSet();

                        DataTable toolReqData = CreateSaveToolRequestDatatable();
                        DataTable toolReqDetailData = CreateSaveToolRequestDetailDatatable();
                        DataTable toolReqLotData = CreateSaveToolRequestDetailLotDatatable();

                        DataRow ruleReqRow = grdToolRequest.View.GetFocusedDataRow();
                        DataRow ruleDetailRow = grdToolRequestDetail.View.GetFocusedDataRow();

                        //ToolRequest
                        DataRow reqRow = toolReqData.NewRow();

                        DateTime requestDate = Convert.ToDateTime(ruleReqRow.GetString("REQUESTDATE"));

                        string requestDateStr = requestDate.ToString("yyyy-MM-dd");
                        string requestNoStr = ruleReqRow.GetString("REQUESTSEQUENCE");

                        reqRow["REQUESTDATE"] = requestDateStr;
                        reqRow["REQUESTSEQUENCE"] = requestNoStr;
                        reqRow["MODIFIER"] = UserInfo.Current.Id;
                        reqRow["VALIDSTATE"] = "Valid";
                        reqRow["_STATE_"] = "deleted";
                        toolReqData.Rows.Add(reqRow);

                        //ToolRequestDetail
                        DataRow detailRow = toolReqDetailData.NewRow();

                        detailRow["REQUESTDATE"] = requestDateStr;
                        detailRow["REQUESTSEQUENCE"] = requestNoStr;
                        detailRow["DURABLEDEFID"] = ruleDetailRow.GetString("TOOLCODE");
                        detailRow["DURABLEDEFVERSION"] = ruleDetailRow.GetString("TOOLVERSION");
                        detailRow["MODIFIER"] = UserInfo.Current.Id;
                        detailRow["VALIDSTATE"] = "Valid";
                        detailRow["_STATE_"] = "modified";

                        toolReqDetailData.Rows.Add(detailRow);

                        //ToolRequestDetailLot
                        DataTable ruleLotTable = grdToolRequestLot.View.GetCheckedRows();

                        foreach (DataRow ruleLotRow in ruleLotTable.Rows)
                        {
                            DataRow lotRow = toolReqLotData.NewRow();

                            lotRow["REQUESTDATE"] = requestDateStr;
                            lotRow["REQUESTSEQUENCE"] = requestNoStr;
                            lotRow["DURABLEDEFID"] = ruleDetailRow.GetString("TOOLCODE");
                            lotRow["DURABLEDEFVERSION"] = ruleDetailRow.GetString("TOOLVERSION");
                            lotRow["DURABLELOTID"] = ruleLotRow.GetString("DURABLELOTID");
                            lotRow["MODIFIER"] = UserInfo.Current.Id;
                            lotRow["VALIDSTATE"] = "Invalid";
                            lotRow["_STATE_"] = "deleted";

                            toolReqLotData.Rows.Add(lotRow);
                        }
                        toolRequestSet.Tables.Add(toolReqData);
                        toolRequestSet.Tables.Add(toolReqDetailData);
                        toolRequestSet.Tables.Add(toolReqLotData);

                        DataTable resultTable = this.ExecuteRule<DataTable>("ToolRequestDetailLot", toolRequestSet);

                        ControlEnableProcess("");

                        ////가져온 결과값에 따라 재검색한 후 가져온 아이디에 맞는 행을 선택으로 변경한다.
                        Research(requestNoStr, requestDateStr);
                    }
                }
                else
                {
                    //ShowMessage(MessageBoxButtons.OK, String.Format("SelectItem", Language.GetDictionary("DURABLE")), "");
                    ShowMessage(MessageBoxButtons.OK, "SelectItem", Language.Get("TOOLREQUESTSTATUS"));
                }
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
            finally
            {
                this.CloseWaitArea();
                btnFindToolNo.Enabled = true;
                btnSave.Enabled = true;
            }
        }
        #endregion

        #region GetRowHandleInGrid : 특정아이디의 값을 바탕으로 해당 내용을 그리드에서 찾은후 결과값의 그리드내의 인덱스를 반환한다.
        /// <summary>
        /// 특정아이디의 값을 바탕으로 해당 내용을 그리드에서 찾은후 결과값의 그리드내의 인덱스를 반환한다.
        /// 현재로선 String비교만을 한다. DateTime및 기타 다른 값들에 대해선 지원하지 않음
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="findValue"></param>
        /// <returns></returns>
        private int GetRowHandleInGrid(SmartBandedGrid targetGrid, string firstColumnName, string secondColumnName, string firstFindValue, string secondFindValue)
        {
            for (int i = 0; i < targetGrid.View.RowCount; i++)
            {
                if (firstFindValue.Equals(targetGrid.View.GetDataRow(i)[firstColumnName].ToString()) && secondFindValue.Equals(targetGrid.View.GetDataRow(i)[secondColumnName].ToString()))
                {
                    return i;
                }
            }
            return -1;
        }
        private int GetRowHandleInGrid(SmartBandedGrid targetGrid, string firstColumnName, string secondColumnName, string thirdColumnName, string forthColumnName, string firstFindValue, string secondFindValue, string thirdFindValue, string forthFindValue)
        {
            for (int i = 0; i < targetGrid.View.RowCount; i++)
            {
                if (firstFindValue.Equals(targetGrid.View.GetDataRow(i)[firstColumnName].ToString()) 
                    && secondFindValue.Equals(targetGrid.View.GetDataRow(i)[secondColumnName].ToString())
                    && thirdFindValue.Equals(targetGrid.View.GetDataRow(i)[thirdColumnName].ToString())
                    && forthFindValue.Equals(targetGrid.View.GetDataRow(i)[forthColumnName].ToString())
                    )
                {
                    return i;
                }
            }
            return -1;
        }
        #endregion

        #region GetLotItemCount - 치공구제작의뢰중 수정목록을 조회
        /// <summary>
        /// 치공구제작의뢰중 수정목록을 조회
        /// </summary>
        /// <param name="inboudNo">입력 및 수정시 발생한 값의 아이디.</param>
        private bool GetLotItemCount()
        {
            if (grdToolRequestDetail.View.FocusedRowHandle > -1)
            {
                Dictionary<string, object> Param = new Dictionary<string, object>();

                DataRow currentRow = grdToolRequest.View.GetFocusedDataRow();
                DataRow currentDetailRow = grdToolRequestDetail.View.GetFocusedDataRow();

                if (currentDetailRow != null)
                {
                    Param.Add("REQUESTSEQUENCE", currentRow["REQUESTSEQUENCE"]);
                    Param.Add("REQUESTDATE", currentRow["REQUESTDATE"]);
                    Param.Add("DURABLEDEFID", currentDetailRow["TOOLCODE"]);
                    Param.Add("DURABLEDEFVERSION", currentDetailRow["TOOLVERSION"]);
                    Param = Commons.CommonFunction.ConvertParameter(Param);
                    DataTable resultTable = SqlExecuter.Query("GetToolRequestDetailLotListForDetailByTool", "10001", Param);

                    if (resultTable.Rows.Count > 0)
                        return false;
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }
        #endregion

        #region GetAllToolVersion : 전체 툴의 버전을 반환
        private DataTable GetAllToolVersion()
        {
            Dictionary<string, object> values = new Dictionary<string, object>();
            return SqlExecuter.Query("GetALlDurableDefVersionListByTool", "10001", values);
        }
        #endregion

        #region GetToolVersion : 특정 툴의 버전을 반환
        private DataTable GetToolVersion(string toolCode)
        {
            DataRow currentRow = grdToolRequest.View.GetFocusedDataRow();
            DataRow currentRow1 = grdToolRequestDetail.View.GetFocusedDataRow();

            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("DURABLETYPE", "Tool");
            values.Add("PRODUCTDEFID", currentRow["PRODUCTDEFID"].ToString().Substring(0, 7));
            values.Add("TOOLVERSION", currentRow1["TOOLVERSION"].ToString());
            values = Commons.CommonFunction.ConvertParameter(values);
            return SqlExecuter.Query("GetDurableDefVersionListByTool", "10001", values);
        }
        #endregion

        #region GetSingleMakeVendor : 사용자가 입력한 제작업체명으로 검색 (클립보드의 데이터를 이용)
        /// <summary>
        /// 사용자가 입력한 제작업체명으로 검색
        /// </summary>
        private void GetSingleMakeVendor(string vendorName, int rowHandle)
        {
            if (_clipDatas == null)
            {
                _clipBoardData = Clipboard.GetDataObject();
                if (_clipBoardData != null)
                {
                    string[] clipFormats = _clipBoardData.GetFormats(true);


                    foreach (string format in clipFormats)
                    {
                        if (format.Equals("System.String"))
                        {
                            string tempStr = Convert.ToString(_clipBoardData.GetData(format));
                            tempStr = tempStr.Replace("\r\n", "\n");
                            if (tempStr.Substring(tempStr.Length - 1).Equals("\n"))
                                tempStr = tempStr.Substring(0, tempStr.Length - 1);
                            _clipDatas = tempStr.Split('\n');
                        }
                    }
                }
            }

            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            values.Add("VENDORNAME", vendorName);
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable savedResult = SqlExecuter.Query("GetVendorListByTool", "10001", values);

            if (savedResult.Rows.Count.Equals(1))
            {
                grdToolRequestDetail.View.SetRowCellValue(rowHandle, "VENDORID", savedResult.Rows[0].GetString("VENDORID"));
                grdToolRequestDetail.View.SetRowCellValue(rowHandle, "MAKEVENDOR", savedResult.Rows[0].GetString("VENDORNAME"));
            }
            else
            {
                grdToolRequestDetail.View.SetRowCellValue(rowHandle, "MAKEVENDOR", "");
                grdToolRequestDetail.View.SetRowCellValue(rowHandle, "VENDORID", "");

                _isGoodCopy = false; //무조건 메세지 출력
            }

            if (_clipDatas != null)
            {
                if (_clipIndex.Equals(_clipDatas.Length)) //마지막 행을 수행하고 난 이후 메세지 출력
                {
                    if (!_isGoodCopy)
                    {
                        ShowMessage(MessageBoxButtons.OK, "TOOLMAKEVENDORSELECT", "");
                    }

                    //초기화
                    _clipIndex = 1;
                    _clipDatas = null;
                    _isGoodCopy = true;
                }
                else
                {
                    _clipIndex++;
                }
            }
        }
        #endregion

        #region GetSingleReceiptArea : 사용자가 입력한 입고작업장으로 검색 (클립보드의 데이터를 이용)
        private void GetSingleReceiptArea(string areaName, int rowHandle)
        {
            if (_clipDatas == null)
            {
                _clipBoardData = Clipboard.GetDataObject();
                if (_clipBoardData != null)
                {
                    string[] clipFormats = _clipBoardData.GetFormats(true);


                    foreach (string format in clipFormats)
                    {
                        if (format.Equals("System.String"))
                        {
                            string tempStr = Convert.ToString(_clipBoardData.GetData(format));
                            tempStr = tempStr.Replace("\r\n", "\n");
                            if (tempStr.Substring(tempStr.Length - 1).Equals("\n"))
                                tempStr = tempStr.Substring(0, tempStr.Length - 1);
                            _clipDatas = tempStr.Split('\n');
                        }
                    }
                }
            }

            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            values.Add("AREANAME", areaName);
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable savedResult = SqlExecuter.Query("GetAreaListByTool", "10001", values);

            if (savedResult.Rows.Count.Equals(1))
            {
                grdToolRequestDetail.View.SetRowCellValue(rowHandle, "RECEIPTAREAID", savedResult.Rows[0].GetString("AREAID"));
                grdToolRequestDetail.View.SetRowCellValue(rowHandle, "RECEIPTAREA", savedResult.Rows[0].GetString("AREANAME"));
            }
            else
            {
                grdToolRequestDetail.View.SetRowCellValue(rowHandle, "RECEIPTAREAID", "");
                grdToolRequestDetail.View.SetRowCellValue(rowHandle, "RECEIPTAREA", "");

                _isGoodCopy = false; //무조건 메세지 출력
            }

            if (_clipDatas != null)
            {
                if (_clipIndex.Equals(_clipDatas.Length)) //마지막 행을 수행하고 난 이후 메세지 출력
                {
                    if (!_isGoodCopy)
                    {
                        ShowMessage(MessageBoxButtons.OK, "TOOLMAKEVENDORSELECT", "");
                    }

                    //초기화
                    _clipIndex = 1;
                    _clipDatas = null;
                    _isGoodCopy = true;
                }
                else
                {
                    _clipIndex++;
                }
            }
        }
        #endregion

        #region ChangeSiteCondition - 검색조건의 Site변경시 관련된 쿼리들을 변경
        private void ChangeSiteCondition()
        {
            if (popupGridToolArea != null)
                popupGridToolArea.SearchQuery = new SqlQuery("GetAreaListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}");


            if (conditionFactoryBox != null)
                conditionFactoryBox.Query = new SqlQuery("GetFactoryListByEqp", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}");


            if (popupGridToolVendor != null)
                popupGridToolVendor.SearchQuery = new SqlQuery("GetVendorListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}");

            if (productPopup != null)
                productPopup.SearchQuery = new SqlQuery("GetProductdefidPoplistByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}", $"PRODUCTDEFTYPE=Product");            
        }
        #endregion
        #endregion
    }
}
