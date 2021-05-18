#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Grid.BandedGrid;

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
    /// 프 로 그 램 명  : 치공구관리 > 치공구 제작수정 > 치공구 제작의뢰
    /// 업  무  설  명  : 치공구에 대한 제작/수정의 의뢰를 조회 및 관리한다.
    /// 생    성    자  : 김용조
    /// 생    성    일  : 2019-07-11
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class RequestToolMaking : SmartConditionManualBaseForm
    {
        #region Local Variables

        //화면의 현재 상태  added, modified, browse;
        private string _currentStatus = "browse";
        private string _requestSequence = "";
        private string _requestDate = "";
        private string _currentApprovalStatus = "";
        ConditionItemSelectPopup productPopup;
        ConditionItemSelectPopup popupGridToolDurableLot;
        ConditionItemSelectPopup popupGridApprovalUser;
        ConditionItemComboBox popupAreaControl;
        #endregion

        #region 생성자

        public RequestToolMaking()
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
            InitRequiredControl();
            InitializeGrid();
            InitializeGridDetail();
            InitializeGridLot();
            InitializeToolMakeTypeComboBox();

            InitializeInsertForm();
            InitializeApprovalUser();

            //팝업창 오픈후 입력받은 컨트롤들을 설정
            ucProductCode.msgHandler += ShowMessageInfo;
            ucProductCode.SetSmartTextBoxForSearchData(txtProductDeptName, grdToolRequestDetail, "PRODUCTDEFID");
        }

        private void InitialInputControls()
        {
            //필수항목 등록
        }

        #region InitRequiredControl - 필수입력항목들을 체크한다.
        private void InitRequiredControl()
        {
            SetRequiredValidationControl(lblRequestDate);
            SetRequiredValidationControl(lblTollMakeType);
            SetRequiredValidationControl(lblProductDefID);
            SetRequiredValidationControl(lblRequestQty);
        }
        #endregion

        #region InitializeGrid - 치공구내역목록을 초기화한다.
        /// <summary>        
        /// 치공구내역목록을 초기화한다.
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
            grdToolRequest.View.AddTextBoxColumn("REQUESTSEQUENCE", 80);               //의뢰순번
            grdToolRequest.View.AddTextBoxColumn("TOOLMAKETYPE")
                .SetIsHidden();                                                         //제작구분
            grdToolRequest.View.AddTextBoxColumn("TOOLMAKETYPENAME", 80)
                .SetTextAlignment(TextAlignment.Center)
                ;                                                                       //제작구분명칭
            grdToolRequest.View.AddTextBoxColumn("PRODUCTDEFID", 100);                  //품목코드
            grdToolRequest.View.AddTextBoxColumn("PRODUCTDEFVERSION", 100)
                ;                                                                       //품목버전
            grdToolRequest.View.AddTextBoxColumn("PRODUCTDEFNAME", 300);                //품목명
            grdToolRequest.View.AddTextBoxColumn("REQUESTQTY", 80)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);                          //의뢰수량
            grdToolRequest.View.AddTextBoxColumn("DELIVERYDATE", 120)
                .SetDisplayFormat("yyyy-MM-dd");                                        //납기일자
            grdToolRequest.View.AddTextBoxColumn("ISAPPROVALUSE", 80)
                .SetTextAlignment(TextAlignment.Center);                                //승인사용            
            grdToolRequest.View.AddTextBoxColumn("REQUESTUSER", 80)
                .SetIsHidden();                                                         //의뢰자아이디
            grdToolRequest.View.AddTextBoxColumn("REQUESTUSERNAME", 80);                //의뢰자명
            grdToolRequest.View.AddTextBoxColumn("REQUESTDEPARTMENT", 100);             //의뢰부서명
            grdToolRequest.View.AddTextBoxColumn("REQUESTCOMMENT", 250);                //제작사유
            grdToolRequest.View.AddTextBoxColumn("DESCRIPTION", 250);                    //설명
            grdToolRequest.View.AddTextBoxColumn("TOOLPROGRESSSTATUS", 80)
                .SetIsHidden();                                                          //진행상태코드            
            grdToolRequest.View.AddTextBoxColumn("APPROVALNO", 80)
                .SetIsHidden();                                                          //진행상태코드            
            grdToolRequest.View.PopulateColumns();
        }
        #endregion

        #region InitializeGridDetail - 치공구 상세내역목록을 초기화한다.
        /// <summary>        
        /// 치공구 상세내역목록을 초기화한다.
        /// </summary>
        private void InitializeGridDetail()
        {
            // TODO : 그리드 초기화 로직 추가
            grdToolRequestDetail.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Export | GridButtonItem.Restore;  // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기       
            grdToolRequestDetail.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            InitializePopupColumnInDetailGrid();
            //grdToolRequestDetail.View.AddTextBoxColumn("TOOLCODE", 80)                //Tool코드
            //    ;
            grdToolRequestDetail.View.AddTextBoxColumn("TOOLVERSION", 80)
                .SetValidationIsRequired()
                .SetIsReadOnly(true);                                                   //Tool Rev.            
                                                                                        //grdToolRequestDetail.View.AddTextBoxColumn("QTY", 100)
                                                                                        //.SetDisplayFormat("#,##0", MaskTypes.Numeric)
                                                                                        //.SetValidationIsRequired();                                           //의뢰수량
            grdToolRequestDetail.View.AddSpinEditColumn("QTY", 100)
                .SetValidationIsRequired();                                             //의뢰수량
            grdToolRequestDetail.View.AddTextBoxColumn("TOOLNAME", 350)
                .SetIsReadOnly();                                                   //Tool Rev.
            grdToolRequestDetail.View.AddTextBoxColumn("TOOLCATEGORYID")
                .SetIsHidden();                                                         //Tool구분
            grdToolRequestDetail.View.AddTextBoxColumn("TOOLCATEGORY", 250)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);                                //Tool구분명칭
            grdToolRequestDetail.View.AddTextBoxColumn("TOOLCATEGORYDETAILID")
                .SetIsHidden();                                                         //Tool유형
            grdToolRequestDetail.View.AddTextBoxColumn("TOOLCATEGORYDETAIL", 120)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();                                                       //Tool유형명칭
            grdToolRequestDetail.View.AddTextBoxColumn("TOOLDETAILID")
                .SetIsHidden();                                                         //상세유형
            grdToolRequestDetail.View.AddTextBoxColumn("TOOLDETAIL", 150)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();                                                       //상세유형명칭
            //grdToolRequestDetail.View.AddTextBoxColumn("USELAYER", 80)
            //    .SetIsReadOnly();                                                       //상세유형명칭

            grdToolRequestDetail.View.AddComboBoxColumn("USELAYER", 65, new SqlQuery("GetCodeList", "00001", "CODECLASSID=UserLayer", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("USERLAYER")
                .SetIsReadOnly(); 

            grdToolRequestDetail.View.AddTextBoxColumn("ROWSTATUS")
                .SetIsHidden();                                                         //현재 행의 상태 '' = upddate, 'added' = insert, 'delete' = delete
            grdToolRequestDetail.View.AddTextBoxColumn("PRODUCTDEFID")
                .SetIsHidden();                                                         //팝업창 조회를 위한 품목코드
            grdToolRequestDetail.View.AddTextBoxColumn("REMARK", 200)
                ;                                                                       //상세유형명칭
            grdToolRequestDetail.View.PopulateColumns();
        }
        #endregion

        #region InitializePopupColumnInDetailGrid - 치공구내역 Detail리스트에서 TOOCODE를 검색할 필드를 설정

        /// <summary>
        /// grid ProcesssegmentidLis
        /// </summary>
        private void InitializePopupColumnInDetailGrid()
        {
            ConditionItemSelectPopup popupGridToolDurableDetail = this.grdToolRequestDetail.View.AddSelectPopupColumn("TOOLCODE", 200, new SqlQuery("GetToolDetailCodeListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}"))
                // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
                .SetPopupLayout("TOOLCODEBROWSE", PopupButtonStyles.Ok_Cancel, true, false)
                // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
                .SetPopupResultCount(1)
                //필수값설정
                .SetValidationIsRequired()
                // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
                .SetPopupResultMapping("TOOLCODE", "TOOLCODE")
                // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
                .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                // 그리드의 남은 영역을 채울 컬럼 설정
                .SetRelationIds("PRODUCTDEFID")
                .SetPopupAutoFillColumns("TOOLVERSION")
                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {
                    foreach (DataRow row in selectedRows)
                    {
                        grdToolRequestDetail.View.SetFocusedRowCellValue("TOOLVERSION", row["TOOLVERSION"]);
                        grdToolRequestDetail.View.SetFocusedRowCellValue("TOOLCATEGORYID", row["TOOLCATEGORYID"]);
                        grdToolRequestDetail.View.SetFocusedRowCellValue("TOOLCATEGORY", row["TOOLCATEGORY"]);
                        grdToolRequestDetail.View.SetFocusedRowCellValue("TOOLCATEGORYDETAILID", row["TOOLCATEGORYDETAILID"]);
                        grdToolRequestDetail.View.SetFocusedRowCellValue("TOOLCATEGORYDETAIL", row["TOOLCATEGORYDETAIL"]);
                        grdToolRequestDetail.View.SetFocusedRowCellValue("TOOLDETAILID", row["TOOLDETAILID"]);
                        grdToolRequestDetail.View.SetFocusedRowCellValue("TOOLDETAIL", row["TOOLDETAIL"]);
                        grdToolRequestDetail.View.SetFocusedRowCellValue("TOOLCODE", row["TOOLCODE"].ToString());
                        grdToolRequestDetail.View.SetFocusedRowCellValue("TOOLNAME", row["TOOLNAME"]);
                    }
                })
            //.SetPopupValidationCustom(ValidationParentAreaIdPopup);
            ;
            // 팝업에서 사용할 조회조건 항목 추가
            popupGridToolDurableDetail.Conditions.AddTextBox("TOOLCODE");
            popupGridToolDurableDetail.Conditions.AddComboBox("TOOLCATEGORY", new SqlQuery("GetDurableClassIDByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"DURABLECLASSTYPE=Tool", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "DURABLECLASSNAME", "DURABLECLASSID");
            popupGridToolDurableDetail.Conditions.AddComboBox("TOOLCATEGORYDETAIL", new SqlQuery("GetCodeClassIDListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "CODENAME", "CODEID")
                .SetRelationIds("TOOLCATEGORY")
                ;

            popupGridToolDurableDetail.Conditions.AddTextBox("PRODUCTDEFID")
                .SetPopupDefaultByGridColumnId("PRODUCTDEFID")
                .SetIsHidden();

            // 팝업 그리드 설정
            popupGridToolDurableDetail.IsMultiGrid = false;
            popupGridToolDurableDetail.GridColumns.AddTextBoxColumn("TOOLCODE", 150)
                .SetValidationKeyColumn();
            popupGridToolDurableDetail.GridColumns.AddTextBoxColumn("TOOLNAME", 350)
                ;
            popupGridToolDurableDetail.GridColumns.AddTextBoxColumn("TOOLVERSION", 80);
            popupGridToolDurableDetail.GridColumns.AddTextBoxColumn("LOTID", 150);
            popupGridToolDurableDetail.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            popupGridToolDurableDetail.GridColumns.AddTextBoxColumn("USECOUNT", 80)
                .SetIsHidden();
            popupGridToolDurableDetail.GridColumns.AddTextBoxColumn("TOTALUSEDCOUNT", 80)
                .SetIsHidden();
            popupGridToolDurableDetail.GridColumns.AddTextBoxColumn("TOTALCLEANCOUNT", 80)
                .SetIsHidden();
            popupGridToolDurableDetail.GridColumns.AddTextBoxColumn("TOTALREPAIRCOUNT", 80)
                .SetIsHidden();
            popupGridToolDurableDetail.GridColumns.AddTextBoxColumn("AREAID", 0)
                .SetIsHidden();
            popupGridToolDurableDetail.GridColumns.AddTextBoxColumn("AREANAME", 150)
                .SetIsHidden();
            popupGridToolDurableDetail.GridColumns.AddTextBoxColumn("DURABLESTATE", 100)
                .SetIsHidden();
            popupGridToolDurableDetail.GridColumns.AddTextBoxColumn("TOOLCATEGORYID", 100)
                .SetIsHidden();
            popupGridToolDurableDetail.GridColumns.AddTextBoxColumn("TOOLCATEGORY", 100)
                ;
            popupGridToolDurableDetail.GridColumns.AddTextBoxColumn("TOOLCATEGORYDETAILID", 100)
                .SetIsHidden();
            popupGridToolDurableDetail.GridColumns.AddTextBoxColumn("TOOLCATEGORYDETAIL", 100)
                ;
            popupGridToolDurableDetail.GridColumns.AddTextBoxColumn("TOOLDETAILID", 100)
                .SetIsHidden();
            popupGridToolDurableDetail.GridColumns.AddTextBoxColumn("TOOLDETAIL", 100)
                .SetIsHidden();
        }
        #endregion

        #region InitializeGridLot - 치공구내역 LOT 리스트를 초기화한다.
        /// <summary>        
        /// 치공구내역 LOT 리스트를 초기화한다.
        /// </summary>
        private void InitializeGridLot()
        {
            // TODO : 그리드 초기화 로직 추가
            grdToolRequestDetailLOT.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Export | GridButtonItem.Restore;  // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기    
            grdToolRequestDetailLOT.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            InitializePopupColumnInLotGrid(); //Tool Code
            //grdToolRequestDetailLOT.View.AddTextBoxColumn("TOOLNUMBER", 150)            //Tool코드
            //    ;
            grdToolRequestDetailLOT.View.AddTextBoxColumn("TOOLCODE", 80)               //Tool코드
                .SetIsHidden();

            grdToolRequestDetailLOT.View.AddTextBoxColumn("TOOLVERSION")
                .SetIsHidden();                                                         //TOOL Version
            grdToolRequestDetailLOT.View.AddTextBoxColumn("TOOLNAME", 400)
                .SetIsReadOnly(true)
                ;                                                                       //치공구명
            //grdToolRequestDetailLOT.View.AddTextBoxColumn("DURABLELOTID")
            //    .SetIsHidden();                                                       //Durable LOT ID
            grdToolRequestDetailLOT.View.AddTextBoxColumn("AREAID")
                .SetIsHidden();                                                         //작업장
            grdToolRequestDetailLOT.View.AddTextBoxColumn("AREANAME", 200)
                ;                                                                       //작업장명            
            grdToolRequestDetailLOT.View.AddTextBoxColumn("TOOLCATEGORYID")
                .SetIsHidden();                                                         //Tool구분
            grdToolRequestDetailLOT.View.AddTextBoxColumn("TOOLCATEGORY", 250)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(true);                                                   //Tool구분명칭
            grdToolRequestDetailLOT.View.AddTextBoxColumn("TOOLCATEGORYDETAILID", 120)
                .SetIsHidden();                                                         //Tool유형
            grdToolRequestDetailLOT.View.AddTextBoxColumn("TOOLCATEGORYDETAIL", 120)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(true);                                                   //Tool유형명칭
            grdToolRequestDetailLOT.View.AddTextBoxColumn("TOOLDETAILID", 120)
                .SetIsHidden();                                                         //상세유형
            grdToolRequestDetailLOT.View.AddTextBoxColumn("TOOLDETAIL", 120)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();                                                       //상세유형명칭
            grdToolRequestDetailLOT.View.AddTextBoxColumn("USELAYER", 80)
                .SetIsReadOnly();                                                       //상세유형명칭
            grdToolRequestDetailLOT.View.AddTextBoxColumn("ROWSTATUS")
                .SetIsHidden();                                                         //현재 행의 상태 '' = upddate, 'added' = insert, 'delete' = delete
            grdToolRequestDetailLOT.View.AddTextBoxColumn("REMARK", 200)
                ;                                                                      //상세유형명칭
            grdToolRequestDetailLOT.View.PopulateColumns();
        }
        #endregion

        #region InitializePopupColumnInLotGrid - 치공구내역 LOT리스트에서 TOOCODE를 검색할 필드를 설정
        /// <summary>
        /// grid ProcesssegmentidLis
        /// </summary>
        private void InitializePopupColumnInLotGrid()
        {
            popupGridToolDurableLot = this.grdToolRequestDetailLOT.View.AddSelectPopupColumn("TOOLNUMBER", 200, new SqlQuery("GetDurableLotToolListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}"))
                // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
                .SetPopupLayout("TOOLNOBROWSE", PopupButtonStyles.Ok_Cancel, true, false)
                // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
                .SetPopupResultCount(1)
                //필수값설정
                .SetValidationIsRequired()
                // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
                .SetPopupResultMapping("TOOLNUMBER", "TOOLNUMBER")
                // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
                .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                // 그리드의 남은 영역을 채울 컬럼 설정

                .SetPopupAutoFillColumns("TOOLVERSION")
                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {
                    foreach (DataRow row in selectedRows)
                    {
                        grdToolRequestDetailLOT.View.GetFocusedDataRow()["TOOLCODE"] = row["TOOLCODE"];
                        grdToolRequestDetailLOT.View.GetFocusedDataRow()["TOOLVERSION"] = row["TOOLVERSION"];
                        grdToolRequestDetailLOT.View.GetFocusedDataRow()["TOOLNUMBER"] = row["TOOLNUMBER"];
                        grdToolRequestDetailLOT.View.GetFocusedDataRow()["TOOLNAME"] = row["TOOLNAME"];
                        grdToolRequestDetailLOT.View.GetFocusedDataRow()["AREAID"] = row["AREAID"];
                        grdToolRequestDetailLOT.View.GetFocusedDataRow()["AREANAME"] = row["AREANAME"];
                        grdToolRequestDetailLOT.View.GetFocusedDataRow()["TOOLCATEGORYID"] = row["TOOLCATEGORYID"];
                        grdToolRequestDetailLOT.View.GetFocusedDataRow()["TOOLCATEGORY"] = row["TOOLCATEGORY"];
                        grdToolRequestDetailLOT.View.GetFocusedDataRow()["TOOLCATEGORYDETAILID"] = row["TOOLCATEGORYDETAILID"];
                        grdToolRequestDetailLOT.View.GetFocusedDataRow()["TOOLCATEGORYDETAIL"] = row["TOOLCATEGORYDETAIL"];
                        grdToolRequestDetailLOT.View.GetFocusedDataRow()["TOOLDETAILID"] = row["TOOLDETAILID"];
                        grdToolRequestDetailLOT.View.GetFocusedDataRow()["TOOLDETAIL"] = row["TOOLDETAIL"];
                    }
                })
            //.SetPopupValidationCustom(ValidationParentAreaIdPopup);
            ;
            // 팝업에서 사용할 조회조건 항목 추가
            popupGridToolDurableLot.Conditions.AddTextBox("TOOLNUMBER");
            popupGridToolDurableLot.Conditions.AddComboBox("TOOLCATEGORY", new SqlQuery("GetDurableClassIDByTool", "10001", $"DURABLECLASSTYPE=Tool", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "DURABLECLASSNAME", "DURABLECLASSID")
                .SetEmptyItem("", "", true)
                ;
            popupGridToolDurableLot.Conditions.AddComboBox("TOOLCATEGORYDETAIL", new SqlQuery("GetCodeClassIDListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "CODENAME", "CODEID")
                .SetEmptyItem("", "", true)
                .SetRelationIds("TOOLCATEGORY")
                ;
            //Form_Shown 이벤트에서 아래구문을 추가한다.
            popupAreaControl = popupGridToolDurableLot.Conditions.AddComboBox("AREAID", new SqlQuery("GetAreaIDListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "AREANAME", "AREAID")
                .SetEmptyItem("", "", true)
                ;

            // 팝업 그리드 설정
            popupGridToolDurableLot.IsMultiGrid = false;
            popupGridToolDurableLot.GridColumns.AddTextBoxColumn("TOOLCODE", 150)
                .SetValidationKeyColumn();
            popupGridToolDurableLot.GridColumns.AddTextBoxColumn("TOOLVERSION", 100);
            popupGridToolDurableLot.GridColumns.AddTextBoxColumn("TOOLNUMBER", 150);
            popupGridToolDurableLot.GridColumns.AddTextBoxColumn("TOOLNAME", 350);
            popupGridToolDurableLot.GridColumns.AddTextBoxColumn("USECOUNT", 80);
            popupGridToolDurableLot.GridColumns.AddTextBoxColumn("TOTALUSEDCOUNT", 80);
            popupGridToolDurableLot.GridColumns.AddTextBoxColumn("TOTALCLEANCOUNT", 80);
            popupGridToolDurableLot.GridColumns.AddTextBoxColumn("TOTALREPAIRCOUNT", 80);
            popupGridToolDurableLot.GridColumns.AddTextBoxColumn("AREAID", 0)
                .SetIsHidden();
            popupGridToolDurableLot.GridColumns.AddTextBoxColumn("AREANAME", 150)
                .SetIsHidden();
            popupGridToolDurableLot.GridColumns.AddTextBoxColumn("DURABLESTATE", 100)
                .SetIsHidden();
            popupGridToolDurableLot.GridColumns.AddTextBoxColumn("TOOLCATEGORYID", 100)
                .SetIsHidden();
            popupGridToolDurableLot.GridColumns.AddTextBoxColumn("TOOLCATEGORY", 100)
                .SetIsHidden();
            popupGridToolDurableLot.GridColumns.AddTextBoxColumn("TOOLCATEGORYDETAILID", 100)
                .SetIsHidden();
            popupGridToolDurableLot.GridColumns.AddTextBoxColumn("TOOLCATEGORYDETAIL", 100)
                .SetIsHidden();
            popupGridToolDurableLot.GridColumns.AddTextBoxColumn("TOOLDETAILID", 100)
                .SetIsHidden();
            popupGridToolDurableLot.GridColumns.AddTextBoxColumn("TOOLDETAIL", 100)
                .SetIsHidden();
        }
        #endregion

        #region InitializeApprovalUser - 결재예정자 목록을 조회한다.
        /// <summary>        
        /// 결재예정자 목록을 조회한다.
        /// </summary>
        private void InitializeApprovalUser()
        {
            // TODO : 그리드 초기화 로직 추가
            grdApprovalUsers.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete;  // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기    
            grdApprovalUsers.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;

            InitializePopupColumnInApprovalUser();                                      //User Name
            grdApprovalUsers.View.AddTextBoxColumn("APPROVALUSERID")
                .SetIsHidden();                                                         //사용자아이디            
            grdApprovalUsers.View.AddTextBoxColumn("DEPARTMENT", 110)
                .SetIsReadOnly();                                                       //부서
            grdApprovalUsers.View.PopulateColumns();
        }
        #endregion

        #region InitializePopupColumnInApprovalUser - 결재예정자목록에서 사용자를 조회할 때 사용
        /// <summary>
        /// grid ProcesssegmentidLis
        /// </summary>
        private void InitializePopupColumnInApprovalUser()
        {
            popupGridApprovalUser = grdApprovalUsers.View.AddSelectPopupColumn("APPROVALUSER", 132, new SqlQuery("GetUserListForPopupByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"))
                // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
                .SetPopupLayout("USERLIST", PopupButtonStyles.Ok_Cancel, true, false)
                // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
                .SetPopupResultCount(1)
                //필수값설정
                .SetValidationIsRequired()
                // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
                .SetPopupResultMapping("APPROVALUSER", "USERNAME")
                // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
                .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow)
                .SetPopupAutoFillColumns("APPROVALUSER")
                // 그리드의 남은 영역을 채울 컬럼 설정
                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {
                    foreach (DataRow row in selectedRows)
                    {
                        grdApprovalUsers.View.GetFocusedDataRow()["APPROVALUSERID"] = row["USERID"];
                        grdApprovalUsers.View.GetFocusedDataRow()["APPROVALUSER"] = row["USERNAME"];
                        grdApprovalUsers.View.GetFocusedDataRow()["DEPARTMENT"] = row["DEPARTMENT"];
                    }
                })
            //.SetPopupValidationCustom(ValidationParentAreaIdPopup);
            ;
            // 팝업 조회조건            
            popupGridApprovalUser.Conditions.AddTextBox("USERNAME")
                .SetLabel("USERNAME");

            // 팝업 그리드
            popupGridApprovalUser.IsMultiGrid = false;
            popupGridApprovalUser.GridColumns.AddTextBoxColumn("USERID", 10)
                .SetIsHidden();
            popupGridApprovalUser.GridColumns.AddTextBoxColumn("USERNAME", 150)
                .SetIsReadOnly();
            popupGridApprovalUser.GridColumns.AddTextBoxColumn("DEPARTMENT", 150)
                .SetIsReadOnly();
            popupGridApprovalUser.GridColumns.AddTextBoxColumn("DUTY", 100)
                .SetIsReadOnly();
            popupGridApprovalUser.GridColumns.AddTextBoxColumn("PLANT", 150)
                .SetIsReadOnly();
            popupGridApprovalUser.GridColumns.AddTextBoxColumn("ENTERPRISE", 200)
                .SetIsReadOnly();
        }
        #endregion
        #endregion

        #region ComboBox  초기화
        /// <summary>
        /// Factory ComboBox를 초기화한다. 검색조건의 Site에 따라 값이 변경되어야 하므로
        /// 초기화 버튼 클릭시 재로딩하는 것으로 한다.
        /// </summary>
        /// <param name="plantID"></param>
        private void InitializeToolMakeTypeComboBox()
        {
            // 검색조건에 정의된 공장을 정리
            cboToolMakeType.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboToolMakeType.ValueMember = "TOOLMAKETYPE";
            cboToolMakeType.DisplayMember = "TOOLMAKETYPENAME";
            cboToolMakeType.EditValue = "1";

            cboToolMakeType.DataSource = SqlExecuter.Query("GetToolMakeTypeCodeByTool", "10001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType } });

            cboToolMakeType.ShowHeader = false;
        }
        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가       
            cboToolMakeType.EditValueChanged += CboToolMakeType_EditValueChanged;
            //btnInitialize.Click += BtnInitialize_Click;
            //btnModify.Click += BtnModify_Click;
            //btnErase.Click += BtnErase_Click;
            btnAddToolToDetail.Click += BtnAddToolToDetail_Click;
            btnAddToolToLot.Click += BtnAddToolToLot_Click;
            btnRemoveDetail.Click += BtnRemoveDetail_Click;
            btnRemoveLot.Click += BtnRemoveLot_Click;
            //btnApproval.Click += BtnApproval_Click;
            //btnReport.Click += BtnReport_Click;
            Shown += RequestToolMaking_Shown;

            chkIsApprovalUse.CheckedChanged += ChkIsApprovalUse_CheckedChanged;

            grdToolRequestDetailLOT.View.AddingNewRow += grdToolRequestDetailLOT_AddingNewRow;
            grdToolRequestDetail.View.AddingNewRow += grdToolRequestDetail_AddingNewRow;
            grdToolRequestDetail.View.CellValueChanged += grdToolRequestDetail_CellValueChanged;
            grdToolRequest.View.FocusedRowChanged += View_FocusedRowChanged;
            grdApprovalUsers.View.ShowingEditor += grdApprovalUsers_ShowingEditor;
            grdToolRequest.View.RowCellClick += grdToolRequest_RowCellClick;
        }

        #region RequestToolMaking_Shown - ToolBar 및 Site관련 설정을 화면 로딩후에 일괄 적용
        private void RequestToolMaking_Shown(object sender, EventArgs e)
        {
            ChangeSiteCondition();

            //다중 Site 권한을 가진 사용자가 Site를 변경시 환경을 변경해줘야한다.
            ((SmartComboBox)Conditions.GetControl("P_PLANTID")).EditValueChanged += ConditionPlant_EditValueChanged;
        }
        #endregion

        #region ConditionPlant_EditValueChanged - 조회조건의 Site선택변경 이벤트
        private void ConditionPlant_EditValueChanged(object sender, EventArgs e)
        {
            ChangeSiteCondition();
        }
        #endregion

        #region grdToolRequest_RowCellClick - 그리드의 로우셀 클릭 이벤트
        private void grdToolRequest_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            DisplayToolRequestInfo();
        }
        #endregion

        #region BtnReport_Click - 리포트버튼이벤트
        private void BtnReport_Click(object sender, EventArgs e)
        {
            ShowPrintData();
        }
        #endregion

        #region grdApprovalUsers_ShowingEditor - 그리드의 입력제어이벤트
        private void grdApprovalUsers_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (grdApprovalUsers.View.FocusedColumn.FieldName.Equals("APPROVALUSER"))
                if (grdApprovalUsers.View.GetFocusedRowCellValue("APPROVALUSER") != null)
                    if (grdApprovalUsers.View.GetFocusedRowCellValue("APPROVALUSER").ToString() != "")
                        e.Cancel = true;
        }
        #endregion

        #region BtnApproval_Click - 결재버튼이벤트
        private void BtnApproval_Click(object sender, EventArgs e)
        {
            //결재지정자에 해당 로그인한 사용자가 있어야 하며 결재요청상태이어야 결재가 가능하다.
            //if (GetCurrentApprovalState().Equals("Request"))
            //{
            //    if(IsApprovalUser(UserInfo.Current.Id))
            //    {
            //        ApprovalData();
            //    }
            //    else
            //    {
            //        this.ShowMessage(MessageBoxButtons.OK, "ApprovalAuthorization", "");
            //    }
            //}
            //else
            //{
            //    this.ShowMessage(MessageBoxButtons.OK, "ApprovalRequestStatus", "");
            //}
            DisplayApprovalPopup();
        }
        #endregion

        #region ChkIsApprovalUse_CheckedChanged - 체크컨트롤 체크변경이벤트
        private void ChkIsApprovalUse_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIsApprovalUse.Checked)
            {
                grdApprovalUsers.View.SetIsReadOnly(false);
            }
            else
            {
                grdApprovalUsers.View.SetIsReadOnly(true);
            }
        }
        #endregion

        #region grdToolRequestDetail_CellValueChanged - 제작그리드 셀 변경이벤트
        private void grdToolRequestDetail_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName.Equals("QTY"))
            {
                CalculateQtyInGridCell(grdToolRequestDetail, txtRequestQty, "QTY");
            }
        }
        #endregion

        #region BtnRemoveLot_Click - 치공구수정 제거버튼 이벤트
        private void BtnRemoveLot_Click(object sender, EventArgs e)
        {
            DeleteLotGridRow();
        }
        #endregion

        #region BtnRemoveDetail_Click - 치공구제작 제거버튼 이벤트
        private void BtnRemoveDetail_Click(object sender, EventArgs e)
        {
            DelteDetailGridRow();
            CalculateQtyInGridCell(grdToolRequestDetail, txtRequestQty, "QTY");
        }
        #endregion

        #region BtnAddToolToLot_Click - 치공구수정 추가버튼 이벤트
        private void BtnAddToolToLot_Click(object sender, EventArgs e)
        {
            DisplayUpdateToolPopup();
        }
        #endregion

        #region BtnAddToolToDetail_Click - 치공구제작 추가버튼 이벤트
        private void BtnAddToolToDetail_Click(object sender, EventArgs e)
        {
            DisplayMakeToolPopup();
        }
        #endregion

        #region View_FocusedRowChanged - 의뢰그리드선택이벤트 - 치공구제작탭 혹은 수정탭을 조회한다.
        private void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DisplayToolRequestInfo();

            //어떤것을 조회하게 될지 모르므로 모든 탭을 보여줌
            //tabPageToolRequestDetail.PageVisible = true;
            //tabPageToolRequestLot.PageVisible = true;            
        }
        #endregion

        #region BtnErase_Click - 의뢰 제거 이벤트
        private void BtnErase_Click(object sender, EventArgs e)
        {
            DeleteData();
        }
        #endregion

        #region BtnModify_Click - 의뢰 수정이벤트
        private void BtnModify_Click(object sender, EventArgs e)
        {
            SaveData();
        }
        #endregion

        #region grdToolRequestDetail_AddingNewRow - 치공구제작 행추가이벤트
        private void grdToolRequestDetail_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            //Row추가시 자동으로 추가해야할 데이터가 있다면 설정한다.
            CalculateQtyInGridCell(grdToolRequestDetail, txtRequestQty, "QTY");
        }
        #endregion

        #region grdToolRequestDetailLOT_AddingNewRow - 치공구수정 행추가이벤트
        private void grdToolRequestDetailLOT_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            //Row추가시 자동으로 추가해야할 데이터가 있다면 설정한다.
            if (cboToolMakeType.EditValue.ToString().Equals("1"))
            {
                args.IsCancel = true;
                ShowMessage(MessageBoxButtons.OK, "ToolDetailCodeValidation");
            }
            else if (cboToolMakeType.EditValue.ToString().Equals("Add") || cboToolMakeType.EditValue.ToString().Equals("New"))
            {
                args.IsCancel = true;
                ShowMessage(MessageBoxButtons.OK, "ToolRequestMakeTypeRepair");
            }
            else
            {
                if (ValidateProductCode())
                {
                    grdToolRequestDetailLOT.View.SetFocusedRowCellValue("ROWSTATUS", "added");// 회사코드
                    txtRequestQty.EditValue = grdToolRequestDetailLOT.View.RowCount;
                }
                else
                {
                    args.IsCancel = true;
                    ShowMessage(MessageBoxButtons.OK, "ToolRequestProductCodeValidation");
                }
            }
        }
        #endregion

        #region BtnInitialize_Click - 초기화버튼 이벤트
        private void BtnInitialize_Click(object sender, EventArgs e)
        {
            InitializeInsertForm();
        }
        #endregion

        #region CboToolMakeType_EditValueChanged - 치공구제작/수정 결정 이벤트
        private void CboToolMakeType_EditValueChanged(object sender, EventArgs e)
        {
            //제작구분에 따라 화면 제어
            if (!cboToolMakeType.ReadOnly)
                ProcessToolMake(cboToolMakeType.EditValue.ToString());
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

            // TODO : 저장 Rule 변경            
        }

        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            //base.OnToolbarClick(sender, e);
            SmartButton btn = sender as SmartButton;

            if (btn.Name.ToString().Equals("Save"))
                BtnModify_Click(sender, e);
            else if (btn.Name.ToString().Equals("Delete"))
                BtnErase_Click(sender, e);
            else if (btn.Name.ToString().Equals("RequestReport"))
                BtnReport_Click(sender, e);
            else if (btn.Name.ToString().Equals("Payment"))
                BtnApproval_Click(sender, e);
            else if (btn.Name.ToString().Equals("Initialization"))
                BtnInitialize_Click(sender, e);
        }

        #endregion

        #region 검색

        #region OnSearchAsync - 검색
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
            values.Add("ENTEPRISEID", UserInfo.Current.Enterprise);
            #endregion
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable toolReqSearchResult = SqlExecuter.Query("GetToolRequestListByTool", "10001", values);



            grdToolRequest.DataSource = toolReqSearchResult;

            if (toolReqSearchResult.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }
            else
            {
                grdToolRequest.View.FocusedRowHandle = 0;
                DisplayToolRequestInfo();
            }
        }
        #endregion

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
            values.Add("ENTEPRISEID", UserInfo.Current.Enterprise);
            #endregion
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable sparePartSearchResult = SqlExecuter.Query("GetToolRequestListByTool", "10001", values);

            if (sparePartSearchResult.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                //InitializeInsertForm();
                //return;
                grdToolRequest.View.ClearDatas();
            }
            else
            {
                grdToolRequest.DataSource = sparePartSearchResult;
            }
            //grdToolRequest.DataSource = sparePartSearchResult;

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
                    grdToolRequest.View.FocusedRowHandle = 0;
                    DisplayToolRequestInfo();
                }
            }
            else //값이 있다면 해당 값에 맞는 행을 찾아서 선택
            {
                ViewSavedData(requestDate, requestSequence);
            }
        }
        #endregion

        #region SearchDetailList - 치공구제작의뢰중 제작목록을 조회
        /// <summary>
        /// 치공구제작의뢰중 제작목록을 조회
        /// </summary>
        /// <param name="inboudNo">입력 및 수정시 발생한 값의 아이디.</param>
        private void SearchDetailList()
        {
            Dictionary<string, object> Param = new Dictionary<string, object>();

            DataRow currentRow = grdToolRequest.View.GetFocusedDataRow();

            Param.Add("REQUESTSEQUENCE", currentRow["REQUESTSEQUENCE"]);
            Param.Add("REQUESTDATE", currentRow["REQUESTDATE"]);
            Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            Param = Commons.CommonFunction.ConvertParameter(Param);
            DataTable resultTable = SqlExecuter.Query("GetToolRequestDetailListByTool", "10001", Param);

            grdToolRequestDetail.DataSource = resultTable;
        }
        #endregion

        #region SearchLotList - 치공구제작의뢰중 수정목록을 조회
        /// <summary>
        /// 치공구제작의뢰중 수정목록을 조회
        /// </summary>
        /// <param name="inboudNo">입력 및 수정시 발생한 값의 아이디.</param>
        private void SearchLotList()
        {
            Dictionary<string, object> Param = new Dictionary<string, object>();

            DataRow currentRow = grdToolRequest.View.GetFocusedDataRow();

            Param.Add("REQUESTSEQUENCE", currentRow["REQUESTSEQUENCE"]);
            Param.Add("REQUESTDATE", currentRow["REQUESTDATE"]);
            Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            Param = Commons.CommonFunction.ConvertParameter(Param);
            DataTable resultTable = SqlExecuter.Query("GetToolRequestDetailLotListByTool", "10001", Param);

            grdToolRequestDetailLOT.DataSource = resultTable;

            //값이 있다면 탭을 보여준다.
            //if (resultTable.Rows.Count > 0)
            //    tabRequestToolInfo.SelectedTabPage = tabPageToolRequestLot;
        }
        #endregion

        #region SearchRequestApprovalUser - 결재지정자를 조회
        /// <summary>
        /// 결재지정자를 조회
        /// </summary>
        private void SearchRequestApprovalUser()
        {
            Dictionary<string, object> Param = new Dictionary<string, object>();

            DataRow currentRow = grdToolRequest.View.GetFocusedDataRow();

            Param.Add("REQUESTSEQUENCE", currentRow["REQUESTSEQUENCE"]);
            Param.Add("REQUESTDATE", currentRow["REQUESTDATE"]);
            Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            Param = Commons.CommonFunction.ConvertParameter(Param);
            DataTable resultTable = SqlExecuter.Query("GetRequestApprovalUserListByTool", "10001", Param);

            grdApprovalUsers.DataSource = resultTable;
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
            //InitializeCondition_Plant();
            InitializeCondition_ToolMakeType();
            InitializeCondition_ToolCodePopup();
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
        }

        /// <summary>
        /// site 설정 
        /// </summary>
        private void InitializeCondition_Plant()
        {
            var planttxtbox = Conditions.AddComboBox("PLANTID", new SqlQuery("GetPlantList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PLANTID", "PLANTID")
               .SetLabel("PLANT")
               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
               .SetPosition(0.1)
               .SetIsReadOnly(true)
               .SetDefault(UserInfo.Current.Plant, "PLANTID") //기본값 설정 UserInfo.Current.Plant
            ;
        }
        /// <summary>
        /// 제작구분 설정
        /// </summary>
        private void InitializeCondition_ToolMakeType()
        {
            var planttxtbox = Conditions.AddComboBox("TOOLMAKETYPE", new SqlQuery("GetToolMakeTypeCodeByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "TOOLMAKETYPENAME", "TOOLMAKETYPE")
               .SetLabel("TOOLMAKETYPE")
               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
               .SetPosition(0.3)
               .SetEmptyItem("", "", true)
            ;
        }

        /// <summary>
        /// 품목코드 조회 설정
        /// </summary>
        private void InitializeCondition_ToolCodePopup()
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

        #region ValidateContent - 입력내용 점검
        private bool ValidateContent()
        {
            //각각의 상태에 따라 보여줄 메세지가 상이하다면 각각의 분기에서 메세지를 표현해야 한다.            

            //요청일자는 필수이다.
            if (!ValidateEditValue(deRequestDate.EditValue))
                return false;

            //요청순번은 필수이나 신규등록시에는 공란이어도 된다.
            if (_currentStatus != "added")
                if (_requestSequence == "")
                    return false;


            //품목코드는 필수이다.
            if (!ValidateEditValue(ucProductCode.ProductDefinitionCode))
                return false;

            //품목버전은 필수이다.
            if (!ValidateEditValue(ucProductCode.ProductDefinitionVersion))
                return false;

            //의뢰부서는 필수이다.
            //if (!ValidateEditValue(txtRequestDepartment.EditValue))
            //    return false;

            //의뢰자는 필수이다.
            if (UserInfo.Current.Id == "")
                return false;

            //제작구분은 필수이다.
            if (!ValidateEditValue(cboToolMakeType.EditValue))
                return false;

            //의뢰수량은 필수이다.
            if (!ValidateNumericBox(txtRequestQty, 0))
                return false;

            //회사아이디는 필수이다.
            if (UserInfo.Current.Enterprise == "")
                return false;

            //PlantID는 필수이다.
            if (!ValidateEditValue(Conditions.GetValue("P_PLANTID")))
                return false;

            //제작 및 수정에 따라 그리드 항목 입력을 체크
            string toolMakeType = cboToolMakeType.EditValue.ToString();

            if (toolMakeType.Equals("Add") || toolMakeType.Equals("New"))
            {
                //제작항목에 입력값이 없다면 진행하지 않는다.
                if (grdToolRequestDetail.View.RowCount == 0)
                    return false;
                if (grdToolRequestDetail.DataSource == null)
                    return false;


                if (!ValidateCellInGrid(grdToolRequestDetail.View.GetFocusedDataRow(), new string[] { "TOOLCODE", "TOOLVERSION", "QTY" }))
                    return false;
            }
            else if (toolMakeType.Equals("Modify") || toolMakeType.Equals("Repair"))
            {
                //수정항목에 입력값이 없다면 진행하지 않는다.
                if (grdToolRequestDetailLOT.View.RowCount == 0)
                    return false;
                if (grdToolRequestDetailLOT.DataSource == null)
                    return false;

                if (!ValidateCellInGrid(grdToolRequestDetailLOT.View.GetFocusedDataRow(), new string[] { "TOOLNUMBER" }))
                    return false;
            }

            return true;
        }
        #endregion

        #region ValidateApprovalUser - 결재내용 점검
        private bool ValidateApprovalUser(out string messageCode)
        {
            messageCode = "";
            //내역등록 상태이거나 Approval상태라면 데이터를 저장할 수 없다.
            if (_currentApprovalStatus.Equals("Approval") || _currentApprovalStatus.Equals("DetailRegist"))
            {
                messageCode = "ValidateTooRequestStatusRegistAndApproval";
                return false;
            }

            //결재사용이 설정된 경우에만 검증
            if (chkIsApprovalUse.Checked)
            {
                if (grdApprovalUsers.View.RowCount == 0)
                {
                    messageCode = "ValidationRequestApprovalUser";
                    return false;
                }

                DataTable allUserTable = (DataTable)grdApprovalUsers.DataSource;

                foreach (DataRow userRow in allUserTable.Rows)
                    if (!ValidateCellInGrid(userRow, new string[] { "APPROVALUSERID" }))
                    {
                        messageCode = "ValidationRequestApprovalUser";
                        return false;
                    }
            }

            return true;
        }
        #endregion

        #region ValidateApproval - 결재상태 점검
        private bool ValidateApproval(out string messageCode)
        {
            messageCode = "";

            //이미 결재 진행중이라면 무조건 창을 열고 결재사용이 되지 않았을 경우에는 결재진행상태체크를 진행한다.
            if (!chkIsApprovalUse.Checked)
                if (_currentApprovalStatus.Equals("Approval") || _currentApprovalStatus.Equals("DetailRegist"))
                {
                    messageCode = "ValidateToolRequestForApproval";
                    return false;
                }

            return true;
        }
        #endregion

        #region ValidateComboBoxEqualValue - 2개의 콤보박스를 비교
        private bool ValidateComboBoxEqualValue(SmartComboBox originBox, SmartComboBox targetBox)
        {
            //두 콤보박스의 값이 같으면 안된다.
            if (originBox.EditValue.Equals(targetBox.EditValue))
                return false;
            return true;
        }
        #endregion

        #region ValidateNumericBox - 숫자형텍스트박스를 Validate - 기준숫자보다 높은지 점검
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

        #region ValidateEditValue - 데이터 점검
        private bool ValidateEditValue(object editValue)
        {
            if (editValue == null)
                return false;

            if (editValue.ToString() == "")
                return false;

            return true;
        }
        #endregion

        #region ValidateCellInGrid - 그리드내 특정컬럼의 Validation
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

        #region SetRequiredValidationControl - 필수입력항목설정
        private void SetRequiredValidationControl(Control requiredControl)
        {
            requiredControl.ForeColor = Color.Red;
        }
        #endregion

        #region ValidateProductCode : 품목코드와 관계된 치공구를 부르기 위해 품목코드가 입력되었는지 확인한다.
        private bool ValidateProductCode()
        {
            if (ucProductCode.ProductDefinitionCode == "")
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
                _requestSequence = null;            //RequestSequence는 공값이어야 한다.
                _currentStatus = "added";           //현재 화면의 상태는 입력중이어야 한다.
                _currentApprovalStatus = "";

                //의뢰일은 오늘날짜를 기본으로 한다.
                DateTime dateNow = DateTime.Now;
                deRequestDate.EditValue = dateNow;

                //의뢰순번은 공값으로 한다.
                txtRequestNo.EditValue = null;

                //의뢰부서는 사용자의 부서를 기본으로 한다.
                txtRequestDepartment.EditValue = UserInfo.Current.Department;

                //제작구분은 선택없이 한다.                
                cboToolMakeType.EditValue = "1";

                //품목코드와 버전역시 비운다.
                ucProductCode.ProductDefinitionCode = "";
                ucProductCode.ProductDefinitionVersion = "";

                //품목명 역시 비운다.
                txtProductDeptName.EditValue = null;

                //의뢰수량은 0으로 잡는다.
                txtRequestQty.EditValue = "0";

                //납기일자역시 오늘날짜를 기본으로 한다.
                deDeliveryDate.EditValue = dateNow;

                //승인사용여부는 체크없음으로 한다.
                chkIsApprovalUse.Checked = false;

                //제작사유는 공값으로 한다.
                txtRequestComment.EditValue = null;

                //비고란은 공값으로 한다.
                txtDescription.EditValue = null;

                //진행상태는 공값으로 한다.
                txtToolProgressStatus.EditValue = null;

                txtApprovalNo.EditValue = null;

                //컨트롤 접근여부는 작성상태로 변경한다.
                ControlEnableProcess("added");

                grdToolRequestDetail.DataSource = null;
                grdToolRequestDetailLOT.DataSource = null;
                grdApprovalUsers.DataSource = null;

                grdToolRequestDetail.View.SetIsReadOnly(false);
                grdToolRequestDetailLOT.View.SetIsReadOnly(false);


                //ReadOnly 처리된 DataGrid를 
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
                deRequestDate.ReadOnly = false;
                cboToolMakeType.ReadOnly = false;
                txtRequestDepartment.ReadOnly = true;
                ucProductCode.ReadOnly = false;
                txtProductDeptName.ReadOnly = true;
                txtRequestQty.ReadOnly = true;
                deDeliveryDate.ReadOnly = false;
                chkIsApprovalUse.ReadOnly = false;
                txtRequestComment.ReadOnly = false;
                txtDescription.ReadOnly = false;
                txtToolProgressStatus.ReadOnly = true;
                txtRequestNo.ReadOnly = true;
                txtApprovalNo.ReadOnly = true;
            }
            else if (currentStatus == "modified") //
            {
                if (_currentStatus.Equals("Approval"))
                {
                    deRequestDate.ReadOnly = true;
                    cboToolMakeType.ReadOnly = true;
                    txtRequestDepartment.ReadOnly = true;
                    ucProductCode.ReadOnly = true;
                    txtProductDeptName.ReadOnly = true;
                    txtRequestQty.ReadOnly = true;
                    deDeliveryDate.ReadOnly = true;
                    chkIsApprovalUse.ReadOnly = true;
                    txtRequestComment.ReadOnly = true;
                    txtDescription.ReadOnly = true;
                    txtToolProgressStatus.ReadOnly = true;
                    txtRequestNo.ReadOnly = true;
                    txtApprovalNo.ReadOnly = true;
                }
                else
                {
                    deRequestDate.ReadOnly = true;
                    cboToolMakeType.ReadOnly = true;
                    txtRequestDepartment.ReadOnly = true;
                    ucProductCode.ReadOnly = true;
                    txtProductDeptName.ReadOnly = true;
                    txtRequestQty.ReadOnly = true;
                    deDeliveryDate.ReadOnly = false;
                    chkIsApprovalUse.ReadOnly = false;
                    txtRequestComment.ReadOnly = false;
                    txtDescription.ReadOnly = false;
                    txtToolProgressStatus.ReadOnly = true;
                    txtRequestNo.ReadOnly = true;
                    txtApprovalNo.ReadOnly = true;
                }
            }
            else
            {
                deRequestDate.ReadOnly = true;
                cboToolMakeType.ReadOnly = true;
                txtRequestDepartment.ReadOnly = true;
                ucProductCode.ReadOnly = true;
                txtProductDeptName.ReadOnly = true;
                txtRequestQty.ReadOnly = true;
                deDeliveryDate.ReadOnly = true;
                chkIsApprovalUse.ReadOnly = true;
                txtRequestComment.ReadOnly = true;
                txtDescription.ReadOnly = true;
                txtToolProgressStatus.ReadOnly = true;
                txtRequestNo.ReadOnly = true;
                txtApprovalNo.ReadOnly = true;
            }
        }
        #endregion

        #region GridEnableProcess : 제작 및 수정그리드에 대하여 해당그리드에 해당하는 치공구에 대하여 제작입고/수정출고 데이터가 있다면 수정불가
        /// <summary>
        /// 제작 및 수정그리드에 대하여 해당그리드에 해당하는 치공구에 대하여 제작입고/수정출고 데이터가 있다면 수정불가
        /// </summary>
        private void GridEnableProcess()
        {
            //치공구 제작입고, 수정출고된 데이터라면 수정이 불가능하므로 모든 컨트롤을 잠근다(그리드 포함)
            if (tabRequestToolInfo.SelectedTabPage == tabPageToolRequestDetail)
            {
                DataRow parentRow = grdToolRequest.View.GetFocusedDataRow();
                DataRow currentRow = grdToolRequestDetail.View.GetFocusedDataRow();

                //현재로선 등록상태일때만 수정이 가능하다.
                if (currentRow != null)
                {
                    if (GetQualification("", currentRow.GetString("TOOLCODE"), currentRow.GetString("TOOLVERSION"), parentRow.GetString("REQUESTDATE"), parentRow.GetString("REQUESTSEQUENCE")) > 0
                        || parentRow.GetString("TOOLPROGRESSSTATUS") != "Create")
                    {
                        grdToolRequestDetail.View.SetIsReadOnly(true);
                        grdToolRequestDetailLOT.View.SetIsReadOnly(false);
                    }
                    else
                    {
                        grdToolRequestDetail.View.SetIsReadOnly(false);
                        grdToolRequestDetailLOT.View.SetIsReadOnly(true);
                    }
                }
            }
            else if (tabRequestToolInfo.SelectedTabPage == tabPageToolRequestLot)
            {
                DataRow parentRow = grdToolRequest.View.GetFocusedDataRow();
                DataRow currentRow = grdToolRequestDetailLOT.View.GetFocusedDataRow();

                if (currentRow != null)
                {
                    if (GetQualification(currentRow.GetString("TOOLNUMBER"), currentRow.GetString("TOOLCODE"), currentRow.GetString("TOOLVERSION"), parentRow.GetString("REQUESTDATE"), parentRow.GetString("REQUESTSEQUENCE")) > 0
                        || parentRow.GetString("TOOLPROGRESSSTATUS") != "Create")
                    {
                        grdToolRequestDetailLOT.View.SetIsReadOnly(true);
                        grdToolRequestDetail.View.SetIsReadOnly(false);
                    }
                    else
                    {
                        grdToolRequestDetailLOT.View.SetIsReadOnly(false);
                        grdToolRequestDetail.View.SetIsReadOnly(true);
                    }
                }
            }
        }
        #endregion

        #region CreateSaveDatatable : ToolRequest 입력/수정/삭제를 위한 DataTable의 Template 생성
        /// <summary>
        /// ToolRequest 입력/수정/삭제를 위한 DataTable의 Template 생성
        /// 입력시에는 ToolRequest와 ToolRequestDetail에 입력하고
        /// 수정시에는 ToolRequest와 ToolRequestDetail 및 ToolRequestDetailLot에 까지 데이터를 기입한다.
        /// </summary>
        /// <returns></returns>
        private DataTable CreateSaveDatatable()
        {
            DataTable dt = new DataTable();
            dt.TableName = "toolRequest";
            //===================================================================================
            //Key값 ToolRequest 및 ToolRequestDetail
            dt.Columns.Add("REQUESTDATE");
            dt.Columns.Add("REQUESTSEQUENCE");
            dt.Columns.Add("PRODUCTDEFID");
            dt.Columns.Add("PRODUCTDEFVERSION");
            dt.Columns.Add("ENTERPRISEID");
            dt.Columns.Add("PLANTID");
            dt.Columns.Add("REQUESTDEPARTMENT");
            dt.Columns.Add("REQUESTUSER");
            dt.Columns.Add("DELIVERYDATE");
            dt.Columns.Add("REQUESTQTY");
            dt.Columns.Add("REQUESTCOMMENT");
            dt.Columns.Add("ISAPPROVALUSE");
            dt.Columns.Add("REQUESTAPPROVALNO");
            dt.Columns.Add("TOOLPROGRESSSTATUS");
            dt.Columns.Add("TOOLMAKETYPE");
            dt.Columns.Add("DESCRIPTION");

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
            dt.TableName = "toolRequestDetail";
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
            dt.Columns.Add("REMARK");
            dt.Columns.Add("_STATE_");

            return dt;
        }
        #endregion

        #region CreateSearchToolRequestDetailDataTable : ToolRequestDetail 검색을 위한 DataTable의 Template생성
        private DataTable CreateSearchToolRequestDetailDataTable()
        {
            DataTable dt = new DataTable();
            dt.TableName = "toolRequestDetail";

            dt.Columns.Add("TOOLCODE");
            dt.Columns.Add("TOOLVERSION");
            dt.Columns.Add("TOOLNAME");
            dt.Columns.Add("QTY");
            dt.Columns.Add("TOOLCATEGORYID");
            dt.Columns.Add("TOOLCATEGORY");
            dt.Columns.Add("TOOLCATEGORYDETAILID");
            dt.Columns.Add("TOOLCATEGORYDETAIL");
            dt.Columns.Add("TOOLDETAILID");
            dt.Columns.Add("TOOLDETAIL");
            dt.Columns.Add("ROWSTATUS");
            dt.Columns.Add("PRODUCTDEFID");
            dt.Columns.Add("USELAYER");
            dt.Columns.Add("REMARK");

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
            dt.Columns.Add("REMARK");
            dt.Columns.Add("_STATE_");

            return dt;
        }
        #endregion

        #region CreateSearchToolRequestDetailLotDataTable : ToolRequestDetailLot 검색을 위한 DataTable의 Template생성
        /// <summary>
        /// ToolRequestDetailLot 검색을 위한 DataTable의 Template생성
        /// </summary>
        /// <returns></returns>
        private DataTable CreateSearchToolRequestDetailLotDataTable()
        {
            DataTable dt = new DataTable();
            dt.TableName = "toolRequestDetailLot";

            dt.Columns.Add("TOOLCODE");
            dt.Columns.Add("TOOLVERSION");
            dt.Columns.Add("TOOLNUMBER");
            dt.Columns.Add("TOOLNAME");
            dt.Columns.Add("AREAID");
            dt.Columns.Add("AREANAME");
            dt.Columns.Add("TOOLCATEGORYID");
            dt.Columns.Add("TOOLCATEGORY");
            dt.Columns.Add("TOOLCATEGORYDETAILID");
            dt.Columns.Add("TOOLCATEGORYDETAIL");
            dt.Columns.Add("TOOLDETAILID");
            dt.Columns.Add("TOOLDETAIL");
            dt.Columns.Add("REMARK");

            return dt;
        }
        #endregion

        #region CreateRequestApprovalUserDataTable : RequestApprovalUser 입력/수정/삭제를 위한 DataTable의 Template생성
        /// <summary>
        /// RequestApprovalUser 입력/수정/삭제를 위한 DataTable의 Template생성
        /// </summary>
        /// <returns></returns>
        private DataTable CreateRequestApprovalUserDataTable()
        {
            DataTable dt = new DataTable();
            dt.TableName = "toolRequestApprovalUsers";

            dt.Columns.Add("USERID");
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

        #region SaveData : 입력/수정을 수행
        private void SaveData()
        {
            //Validation 체크 부분 작성 필요
            this.ShowWaitArea();
            grdToolRequestDetail.View.FocusedRowHandle = grdToolRequestDetail.View.FocusedRowHandle;
            grdToolRequestDetail.View.FocusedColumn = grdToolRequestDetail.View.Columns["TOOLNAME"];
            grdToolRequestDetail.View.ShowEditor();
            grdToolRequestDetailLOT.View.FocusedRowHandle = grdToolRequestDetailLOT.View.FocusedRowHandle;
            grdToolRequestDetailLOT.View.FocusedColumn = grdToolRequestDetailLOT.View.Columns["TOOLNAME"];
            grdToolRequestDetailLOT.View.ShowEditor();
            //저장 로직
            try
            {
                btnInitialize.Enabled = false;
                btnModify.Enabled = false;
                btnErase.Enabled = false;
                string messageCode = "";
                //Validation Check시 입력/수정시 검사할 값이 다르다면 수정필요
                if (ValidateContent())
                {
                    if (ValidateApprovalUser(out messageCode))
                    {
                        DataSet toolRequestSet = new DataSet();
                        //치공구 제작의뢰를 입력
                        DataTable toolRequestData = CreateSaveDatatable();

                        DateTime requestDate = Convert.ToDateTime(deRequestDate.EditValue.ToString());
                        //ToolRequest 입력
                        toolRequestData.Rows.Add(GetToolRequestRow(toolRequestData, requestDate.ToString("yyyy-MM-dd"), txtRequestNo.Text, ucProductCode.ProductDefinitionCode, ucProductCode.ProductDefinitionVersion
                            , ((DateTime)deDeliveryDate.EditValue).ToString("yyyy-MM-dd"), txtRequestQty.Text, txtRequestComment.Text, chkIsApprovalUse.Checked ? "Y" : "N"
                            , cboToolMakeType.EditValue.ToString(), txtDescription.Text, "")
                            );

                        toolRequestSet.Tables.Add(toolRequestData);

                        //제작구분에 따라 제작탭의 그리드, 수정탭의 그리드를 각각 따로 입력이 된다.
                        string toolMakeType = cboToolMakeType.EditValue.ToString();         //Add, New = 제작     Modify, Repair = 수정

                        //제작일 경우
                        if (toolMakeType.Equals("Add") || toolMakeType.Equals("New"))
                        {
                            DataTable toolRequestDetailData = CreateSaveToolRequestDetailDatatable();
                            DataTable changedDetailDataTable = grdToolRequestDetail.GetChangedRows();
                            foreach (DataRow currentRow in changedDetailDataTable.Rows)
                            {
                                toolRequestDetailData.Rows.Add(GetToolRequestDetailDataRow(toolRequestDetailData, currentRow.GetString("_STATE_"), requestDate.ToString("yyyy-MM-dd"), txtRequestNo.Text
                                    , currentRow.GetString("TOOLCODE"), currentRow.GetString("TOOLVERSION"), currentRow.GetInteger("QTY"), currentRow.GetString("REMARK"))
                                    );
                            }
                            toolRequestSet.Tables.Add(toolRequestDetailData);

                            //결재사용자입력
                            DataTable approvalUserTable = GetRequestApprovalUserDataRow(requestDate.ToString("yyyy-MM-dd"), "");
                            if (approvalUserTable != null)
                                toolRequestSet.Tables.Add(approvalUserTable);
                        }
                        else if (toolMakeType.Equals("Modify") || toolMakeType.Equals("Repair")) //수정일 경우 -- 입력된 ToolRequestDetail정보의 경우 복수일경우 수량을 복수에 맞추어 
                        {
                            DataTable toolRequestLotData = CreateSaveToolRequestDetailLotDatatable();
                            DataTable toolRequestDetailData = CreateSaveToolRequestDetailDatatable();
                            DataTable changeLotDataTable = grdToolRequestDetailLOT.GetChangedRows();

                            if (changeLotDataTable != null)
                            {
                                foreach (DataRow currentRow in changeLotDataTable.Rows)
                                {
                                    //Lot Data를 입력한다.==========================================================================
                                    toolRequestLotData.Rows.Add(GetToolRequestDetailLOTRow(toolRequestLotData, currentRow.GetString("_STATE_"), requestDate.ToString("yyyy-MM-dd"), txtRequestNo.Text
                                        , currentRow.GetString("TOOLCODE"), currentRow.GetString("TOOLVERSION"), currentRow.GetString("TOOLNUMBER"), currentRow.GetString("REMARK"))
                                        );
                                }
                                //Lot Data를 바탕으로 Request Detail 데이터를 입력한다.
                                toolRequestDetailData = GetChangedToolRequestDetailDataRow((DataTable)grdToolRequestDetailLOT.DataSource, requestDate.ToString("yyyy-MM-dd"), txtRequestNo.Text);

                                toolRequestSet.Tables.Add(toolRequestDetailData);
                                toolRequestSet.Tables.Add(toolRequestLotData);

                                //결재사용자입력
                                DataTable approvalUserTable = GetRequestApprovalUserDataRow(requestDate.ToString("yyyy-MM-dd"), txtRequestNo.Text);
                                if (approvalUserTable != null)
                                    toolRequestSet.Tables.Add(approvalUserTable);
                            }
                        }

                        DataTable resultTable = this.ExecuteRule<DataTable>("ToolRequest", toolRequestSet);

                        ////Server에서 결과값을 현재는 반환하지 않음 만약 반환해야 한다면 아래로직 구현
                        DataRow resultRow = resultTable.Rows[0];

                        ControlEnableProcess("modified");

                        ////가져온 결과값에 따라 재검색한 후 가져온 아이디에 맞는 행을 선택으로 변경한다.
                        Research(resultRow["REQUESTSEQUENCE"].ToString(), requestDate.ToString("yyyy-MM-dd"));
                    }
                    else
                    {
                        this.ShowMessage(MessageBoxButtons.OK, messageCode, "");
                    }
                }
                else
                {
                    this.ShowMessage(MessageBoxButtons.OK, "ToolDetailValidation", "");
                }
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
            finally
            {
                this.CloseWaitArea();
                btnInitialize.Enabled = true;
                btnModify.Enabled = true;
                btnErase.Enabled = true;
            }
        }
        #endregion

        #region ApprovalData : 결재를 승인
        private void ApprovalData()
        {
            //Validation 체크 부분 작성 필요
            this.ShowWaitArea();

            //저장 로직
            try
            {
                btnInitialize.Enabled = false;
                btnModify.Enabled = false;
                btnErase.Enabled = false;

                string messageCode = "";
                //Validation Check시 입력/수정시 검사할 값이 다르다면 수정필요
                if (ValidateContent())
                {
                    if (ValidateApprovalUser(out messageCode))
                    {
                        DataSet toolRequestSet = new DataSet();
                        //치공구 제작의뢰를 입력
                        DataTable toolRequestData = CreateSaveDatatable();
                        DateTime requestDate = Convert.ToDateTime(deRequestDate.EditValue.ToString());

                        string requestDateStr = requestDate.ToString("yyyy-MM-dd");
                        string requestNoStr = txtRequestNo.Text;

                        //ToolRequest 입력
                        toolRequestData.Rows.Add(GetToolRequestRow(toolRequestData, requestDateStr, requestNoStr, ucProductCode.ProductDefinitionCode, ucProductCode.ProductDefinitionVersion
                            , ((DateTime)deDeliveryDate.EditValue).ToString("yyyy-MM-dd"), txtRequestQty.Text, txtRequestComment.Text, chkIsApprovalUse.Checked ? "Y" : "N"
                            , cboToolMakeType.EditValue.ToString(), txtDescription.Text, "Approval")
                            );

                        toolRequestSet.Tables.Add(toolRequestData);

                        ExecuteRule<DataTable>("ToolRequestApprovalUser", toolRequestSet);

                        ShowMessage(MessageBoxButtons.OK, "ApprovalComplete", "");

                        ControlEnableProcess("modified");

                        ////가져온 결과값에 따라 재검색한 후 가져온 아이디에 맞는 행을 선택으로 변경한다.
                        Research(requestNoStr, requestDateStr);
                    }
                    else
                    {
                        ShowMessage(MessageBoxButtons.OK, messageCode, "");
                    }
                }
                else
                {
                    ShowMessage(MessageBoxButtons.OK, "ToolDetailValidation", "");
                }
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
            finally
            {
                this.CloseWaitArea();
                btnInitialize.Enabled = true;
                btnModify.Enabled = true;
                btnErase.Enabled = true;
            }
        }

        private void DisplayApprovalPopup()
        {
            //Validation 체크 부분 작성 필요
            this.ShowWaitArea();

            //저장 로직
            try
            {
                btnInitialize.Enabled = false;
                btnModify.Enabled = false;
                btnErase.Enabled = false;

                if (!txtRequestNo.Text.Equals(""))
                {
                    string messageCode = "";
                    if (ValidateApproval(out messageCode))
                    {
                        Popup.ToolApproval appPopup = new Popup.ToolApproval(
                            txtApprovalNo.Text//  grdToolRequest.View.GetFocusedDataRow().GetString("APPROVALNO")
                            , Convert.ToDateTime(deRequestDate.EditValue).ToString("yyyy-MM-dd")   //, grdToolRequest.View.GetFocusedDataRow().GetString("REQUESTDATE")
                            , txtRequestNo.Text //, grdToolRequest.View.GetFocusedDataRow().GetString("REQUESTSEQUENCE")
                        , _currentApprovalStatus
                        );
                        appPopup.reSearchHandler += Research;
                        appPopup.ShowDialog();
                    }
                    else
                    {
                        ShowMessage(MessageBoxButtons.OK, messageCode, "");
                    }
                }
                else
                {
                    //ShowMessage(MessageBoxButtons.OK, String.Format("SelectItem", Language.GetDictionary("DURABLE")), "");
                    ShowMessage(MessageBoxButtons.OK, "SelectItem", Language.Get("TOOLREQUEST"));
                }
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
            finally
            {
                this.CloseWaitArea();
                btnInitialize.Enabled = true;
                btnModify.Enabled = true;
                btnErase.Enabled = true;
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
                btnInitialize.Enabled = false;
                btnModify.Enabled = false;
                btnErase.Enabled = false;

                if (grdToolRequest.View.GetFocusedDataRow() != null)
                {
                    if (grdToolRequest.View.GetFocusedDataRow().GetString("TOOLPROGRESSSTATUS").Equals("Create"))
                    {
                        DialogResult result = this.ShowMessage(MessageBoxButtons.YesNo, "OspDoProcessWant", btnErase.Text);//삭제하시겠습니까? 

                        if (result == System.Windows.Forms.DialogResult.Yes)
                        {
                            DataSet toolRequestSet = new DataSet();

                            DataTable toolRequestData = CreateSaveDatatable();
                            DataRow toolRequestRow = toolRequestData.NewRow();

                            DateTime requestDate = Convert.ToDateTime(deRequestDate.EditValue.ToString());

                            toolRequestRow["REQUESTDATE"] = requestDate.ToString("yyyy-MM-dd");
                            toolRequestRow["REQUESTSEQUENCE"] = txtRequestNo.EditValue;
                            toolRequestRow["PRODUCTDEFID"] = ucProductCode.ProductDefinitionCode;
                            toolRequestRow["PRODUCTDEFVERSION"] = ucProductCode.ProductDefinitionVersion;
                            toolRequestRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                            toolRequestRow["PLANTID"] = Conditions.GetValue("P_PLANTID");
                            toolRequestRow["REQUESTDEPARTMENT"] = UserInfo.Current.Department;
                            toolRequestRow["REQUESTUSER"] = UserInfo.Current.Id;
                            toolRequestRow["DELIVERYDATE"] = ((DateTime)deDeliveryDate.EditValue).ToString("yyyy-MM-dd");
                            toolRequestRow["REQUESTQTY"] = txtRequestQty.EditValue;
                            toolRequestRow["REQUESTCOMMENT"] = txtRequestComment.EditValue;
                            toolRequestRow["ISAPPROVALUSE"] = chkIsApprovalUse.Checked ? "Y" : "N";
                            toolRequestRow["REQUESTAPPROVALNO"] = "";
                            toolRequestRow["TOOLMAKETYPE"] = cboToolMakeType.EditValue;
                            toolRequestRow["DESCRIPTION"] = txtDescription.EditValue;
                            toolRequestRow["CREATOR"] = UserInfo.Current.Id;
                            toolRequestRow["MODIFIER"] = UserInfo.Current.Id;
                            toolRequestRow["VALIDSTATE"] = "Invalid";
                            toolRequestRow["_STATE_"] = "deleted";


                            toolRequestData.Rows.Add(toolRequestRow);

                            toolRequestSet.Tables.Add(toolRequestData);

                            DataTable resultTable = this.ExecuteRule<DataTable>("ToolRequest", toolRequestSet);

                            ControlEnableProcess("");

                            ////가져온 결과값에 따라 재검색한 후 가져온 아이디에 맞는 행을 선택으로 변경한다.
                            Research(null, null);
                        }
                    }
                    else
                    {
                        ShowMessage(MessageBoxButtons.OK, "CanDeleteToolCreateStatus", Language.Get("TOOLREQUEST")); //등록상태일때만 삭제가 가능합니다.
                    }
                }
                else
                {
                    //ShowMessage(MessageBoxButtons.OK, String.Format("SelectItem", Language.GetDictionary("DURABLE")), "");
                    ShowMessage(MessageBoxButtons.OK, "SelectItem", Language.Get("TOOLREQUEST"));
                }

            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
            finally
            {
                this.CloseWaitArea();
                btnInitialize.Enabled = true;
                btnModify.Enabled = true;
                btnErase.Enabled = true;
            }
        }
        #endregion

        #region GetIndexFromToolRequestDetailTable : ToolRequestDetailTable에서 DurableDefID와 DurableDefVersion이 같은 행의 Index를 반환한다.
        private int GetIndexFromToolRequestDetailTable(DataTable detailTable, string durableDefID, string durableDefVersion)
        {
            int index = 0;
            for (; index < detailTable.Rows.Count; index++)
            {
                if (detailTable.Rows[index].GetString("DURABLEDEFID").Equals(durableDefID) && detailTable.Rows[index].GetString("DURABLEDEFVERSION").Equals(durableDefVersion))
                    return index;
            }
            return -1;
        }
        #endregion

        #region SetValueToToolRequestDetailTable : ToolRequestDetailTable에서 DurableDefID와 DurableDefVersion이 일치하는 행을 찾아 특정 Cell의 값을 입력한다.
        private void SetValueToToolRequestDetailTable(DataTable detailTable, string durableDefID, string durableDefVersion, string columnName, string dataValue)
        {
            foreach (DataRow detailRow in detailTable.Rows)
            {
                if (detailRow.GetString("DURABLEDEFID").Equals(durableDefID) && detailRow.GetString("DURABLEDEFVERSION").Equals(durableDefVersion))
                    detailRow[columnName] = dataValue;
            }
        }
        private void SetValueToToolRequestDetailTable(DataTable detailTable, string durableDefID, string durableDefVersion, string columnName, int dataValue)
        {
            foreach (DataRow detailRow in detailTable.Rows)
            {
                if (detailRow.GetString("DURABLEDEFID").Equals(durableDefID) && detailRow.GetString("DURABLEDEFVERSION").Equals(durableDefVersion))
                    detailRow[columnName] = detailRow.GetInteger(columnName) + dataValue;
            }
        }
        #endregion

        #region GetToolRequestRow : ToolRequest의 DataRow를 입력한 후 반환한다.
        private DataRow GetToolRequestRow(DataTable requestTable, string requestDate, string requestNo, string productCode, string productVersion, string deliveryDate
            , string qty, string requestComment, string isApproval, string toolMakeType, string description, string toolProgressStatus)
        {

            DataRow reqRow = requestTable.NewRow();

            reqRow["REQUESTDATE"] = requestDate;
            reqRow["REQUESTSEQUENCE"] = requestNo;
            reqRow["PRODUCTDEFID"] = productCode;
            reqRow["PRODUCTDEFVERSION"] = productVersion;
            reqRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
            reqRow["PLANTID"] = Conditions.GetValue("P_PLANTID");
            reqRow["REQUESTDEPARTMENT"] = UserInfo.Current.Department;
            reqRow["REQUESTUSER"] = UserInfo.Current.Id;
            reqRow["DELIVERYDATE"] = deliveryDate;
            reqRow["REQUESTQTY"] = qty;
            reqRow["REQUESTCOMMENT"] = requestComment;
            reqRow["ISAPPROVALUSE"] = isApproval;
            reqRow["REQUESTAPPROVALNO"] = "";
            reqRow["TOOLMAKETYPE"] = toolMakeType;
            reqRow["DESCRIPTION"] = description;
            reqRow["CREATOR"] = UserInfo.Current.Id;
            reqRow["MODIFIER"] = UserInfo.Current.Id;
            reqRow["VALIDSTATE"] = "Valid";

            if (toolProgressStatus.Equals(""))
                reqRow["TOOLPROGRESSSTATUS"] = GetCurrentApprovalState();
            else
                reqRow["TOOLPROGRESSSTATUS"] = toolProgressStatus;

            //현재 화면의 상태에 따라 생성/수정으로 분기된다.
            if (_currentStatus == "added")
            {
                reqRow["_STATE_"] = "added";
            }
            else
            {
                reqRow["_STATE_"] = "modified";
            }

            return reqRow;
        }
        #endregion

        #region GetToolRequestDetailDataRow : ToolRequestDetail의 DataRow를 입력한 후 반환한다.
        private DataRow GetToolRequestDetailDataRow(DataTable detailTable, string changedStatus, string requestDate, string requestNo, string toolCode, string toolVersion, int detailQty, string remark)
        {

            DataRow detailRow = detailTable.NewRow();

            detailRow["REQUESTDATE"] = requestDate;
            detailRow["REQUESTSEQUENCE"] = requestNo;
            detailRow["DURABLEDEFID"] = toolCode;
            detailRow["DURABLEDEFVERSION"] = toolVersion;
            detailRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
            detailRow["PLANTID"] = Conditions.GetValue("P_PLANTID");
            detailRow["QTY"] = detailQty;                                      //복수개의 경우 수량을 입력
                                                                               //내역등록에서 데이터를 입력하나 빈값으로 설정
            detailRow["REQUESTDELIVERYDATE"] = "";
            detailRow["PLANDELIVERYDATE"] = "";
            detailRow["VENDORID"] = "";
            detailRow["AREAID"] = "";
            detailRow["OWNSHIPTYPE"] = "";
            detailRow["REMARK"] = remark;
            //내역등록입력종료
            detailRow["CREATOR"] = UserInfo.Current.Id;
            detailRow["MODIFIER"] = UserInfo.Current.Id;

            if (changedStatus.Equals("added"))
            {
                detailRow["_STATE_"] = "added";
                detailRow["VALIDSTATE"] = "Valid";
            }
            else if (changedStatus.Equals("modified") && detailQty > 0)                      //modified상태에서 수량이 하나라도 있다면 수정의뢰 건수가 있으므로 수정
            {
                detailRow["_STATE_"] = "modified";
                detailRow["VALIDSTATE"] = "Valid";
            }
            else if (changedStatus.Equals("modified") && detailQty == 0)                     //modified상태에서 수량이 하나도 없다면 수정의뢰 건수가 없는것이므로 삭제
            {
                detailRow["_STATE_"] = "deleted";
                detailRow["VALIDSTATE"] = "Invalid";
            }
            else if (changedStatus.Equals("deleted"))
            {
                detailRow["_STATE_"] = "deleted";
                detailRow["VALIDSTATE"] = "Invalid";
            }

            return detailRow;
        }
        #endregion

        #region GetToolRequestDetailLOTRow : ToolRequestDetailLot의 DataRow를 입력한 후 반환한다.
        private DataRow GetToolRequestDetailLOTRow(DataTable lotTable, string changedStatus, string requestDate, string requestNo, string toolCode, string toolVersion, string toolNo, string remark)
        {
            DataRow lotRow = lotTable.NewRow();

            lotRow["REQUESTDATE"] = requestDate;
            lotRow["REQUESTSEQUENCE"] = requestNo;
            lotRow["DURABLEDEFID"] = toolCode;
            lotRow["DURABLEDEFVERSION"] = toolVersion;
            lotRow["TOOLNUMBER"] = toolNo;
            lotRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
            lotRow["PLANTID"] = Conditions.GetValue("P_PLANTID");
            lotRow["CREATOR"] = UserInfo.Current.Id;
            lotRow["MODIFIER"] = UserInfo.Current.Id;
            lotRow["REMARK"] = remark;

            if (changedStatus.Equals("added"))
            {
                lotRow["_STATE_"] = "added";
                lotRow["VALIDSTATE"] = "Valid";
            }
            else if (changedStatus.Equals("modified"))
            {
                lotRow["_STATE_"] = "modified";
                lotRow["VALIDSTATE"] = "Valid";
            }
            else if (changedStatus.Equals("deleted"))
            {
                lotRow["_STATE_"] = "deleted";
                lotRow["VALIDSTATE"] = "Invalid";
            }

            return lotRow;
        }
        #endregion

        #region GetChangedToolRequestDetailDataRow : ToolRequestDetail의 DataRow를 입력한 후 반환한다.
        private DataTable GetChangedToolRequestDetailDataRow(DataTable allTable, string requestDate, string requestNo)
        {
            //RequestDate, RequestNo, DurableDefID, DurableDefVersion, EnterpriseID, PlantID, AllQty, ModifiedQty, IsModified, _STATE_, ValidState
            DataTable changedDetailTable = CreateSaveToolRequestDetailDatatable();

            SetChangedDataStateToAllData(allTable, grdToolRequestDetailLOT.GetChangedRows());

            //각각의 유일한 DurableDefID와 DurableDefVersion을 입력한다.
            foreach (DataRow changedRow in allTable.Rows)
            {
                if (GetIndexFromToolRequestDetailTable(changedDetailTable, changedRow.GetString("TOOLCODE"), changedRow.GetString("TOOLVERSION")).Equals(-1))
                {
                    //입력된 값중 DurableDefID와 DurableDefVersion이 일치하는 값이 없으므로 새로이 입력
                    changedDetailTable.Rows.Add(GetToolRequestDetailDataRow(changedDetailTable, "added", requestDate, requestNo, changedRow.GetString("TOOLCODE")
                        , changedRow.GetString("TOOLVERSION"), 0, changedRow.GetString("REMARK")));
                }
            }

            //유일한 Data를 추출하였으므로 가각의 QTY를 계산한다.
            foreach (DataRow allRow in allTable.Rows)
            {
                if (!allRow.GetString("_STATE_").Equals("deleted"))
                    SetValueToToolRequestDetailTable(changedDetailTable, allRow.GetString("TOOLCODE"), allRow.GetString("TOOLVERSION"), "QTY", 1);
            }

            //Qty가 0인 항목은 _STATUS_가 deleted이며 나머지는 서버에서 판단하므로 큰 의미가 없으므로 added로 설정한다.
            foreach (DataRow chanedRow in changedDetailTable.Rows)
            {
                if (chanedRow.GetInteger("QTY") > 0)
                {
                    chanedRow["_STATE_"] = "added";
                    chanedRow["VALIDSTATE"] = "Valid";
                }
                else
                {
                    chanedRow["_STATE_"] = "deleted";
                    chanedRow["VALIDSTATE"] = "Invalid";
                }
            }

            return changedDetailTable;
        }
        #endregion

        #region GetRequestApprovalUserDataRow : RequestApprovalUser의 Data를 반환한다.
        private DataTable GetRequestApprovalUserDataRow(string requestDate, string requestNo)
        {
            if (grdApprovalUsers.View.RowCount > 0)
            {
                DataTable approvalUserTable = CreateRequestApprovalUserDataTable();
                DataTable changedUserTable = null; grdApprovalUsers.GetChangedRows();
                if (chkIsApprovalUse.Checked)
                {
                    changedUserTable = grdApprovalUsers.GetChangedRows();
                }
                else
                {
                    changedUserTable = (DataTable)grdApprovalUsers.DataSource;
                }
                //유일한 Data를 추출하였으므로 가각의 QTY를 계산한다.
                foreach (DataRow changedUserRow in changedUserTable.Rows)
                {
                    DataRow userRow = approvalUserTable.NewRow();
                    //checkbox를 선택하지 않았다면 모두 삭제
                    if (chkIsApprovalUse.Checked)
                    {
                        //checkbox를 선택한 상황에서도 현재 Row의 상태가 deleted라면 삭제로 지정한다.
                        if (changedUserRow.GetString("_STATE_").Equals("deleted"))
                        {
                            userRow["_STATE_"] = "deleted";
                            userRow["VALIDSTATE"] = "Invalid";
                        }
                        else
                        {
                            userRow["_STATE_"] = "added";    //입력과 수정은 동시에 동작하도록 한다.
                            userRow["VALIDSTATE"] = "Valid";
                        }
                    }
                    else
                    {
                        userRow["_STATE_"] = "deleted";
                        userRow["VALIDSTATE"] = "Invalid";
                    }
                    userRow["USERID"] = changedUserRow.GetString("APPROVALUSERID");
                    userRow["REQUESTDATE"] = requestDate;
                    userRow["REQUESTSEQUENCE"] = requestNo;
                    userRow["CREATOR"] = UserInfo.Current.Id;
                    userRow["MODIFIER"] = UserInfo.Current.Id;

                    approvalUserTable.Rows.Add(userRow);
                }

                return approvalUserTable;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region SetChangedDataStateToAllData : 변경된 테이블의 State값을 전체 테이블의 State값으로 추가
        private void SetChangedDataStateToAllData(DataTable allTable, DataTable changedTable)
        {
            if (allTable.Columns["_STATE_"] == null)
                allTable.Columns.Add("_STATE_");
            foreach (DataRow allRow in allTable.Rows)
            {
                foreach (DataRow changedRow in changedTable.Rows)
                {
                    if (allRow.GetString("TOOLNUMBER").Equals(changedRow.GetString("TOOLNUMBER")))
                    {
                        allRow["_STATE_"] = changedRow.GetString("_STATE_");
                    }
                }
            }
        }
        #endregion

        #region ToolMakeTypeProcess - 제작구분의 선택에 따라 탭페이지를 제어한다.
        private void ProcessToolMake(string toolMakeType)
        {
            //Add, Modify, New, Repair
            if (toolMakeType.Equals("Add") || toolMakeType.Equals("New"))
            {
                if (tabPageToolRequestLot.PageVisible)
                {
                    //제작의뢰의 경우 ToolRequestDetail 탭을 보여준다.
                    tabRequestToolInfo.TabPages[0].PageVisible = true;
                    //tabRequestToolInfo.TabPages[0].Select();
                    tabRequestToolInfo.TabPages[1].PageVisible = false;
                }
            }
            else if (toolMakeType.Equals("Modify") || toolMakeType.Equals("Repair"))
            {
                if (tabPageToolRequestDetail.PageVisible)
                {
                    //수정의뢰의 경우 ToolRequestDetailLot 탭을 보여준다.
                    //수정의뢰의 경우 ToolRequestDetail탭은 보여주되 ReadOnly로 설정한다.
                    tabRequestToolInfo.TabPages[1].PageVisible = true;
                    //tabRequestToolInfo.TabPages[1].Select();
                    tabRequestToolInfo.TabPages[0].PageVisible = false;
                }
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

            DisplayToolRequestInfoDetail(grdToolRequest.View.GetFocusedDataRow());
        }
        #endregion

        #region ViewSavedData : 새로 입력된 데이터를 화면에 바인딩
        /// <summary>
        /// 새로 입력된 데이터를 화면에 바인딩
        /// </summary>
        private void ViewSavedData(string requestDate, string requestSequence)
        {
            #region 기간 검색형 전환 처리 - 만약 시간까지 포함하여 검색한다면 아래 문구는 필요없음            
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("REQUESTDATE", Convert.ToDateTime(requestDate).ToString("yyyy-MM-dd"));
            values.Add("REQUESTSEQUENCE", Int32.Parse(requestSequence));
            #endregion
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable savedResult = SqlExecuter.Query("GetToolRequestListByTool", "10001", values);

            if (savedResult.Rows.Count > 0)
            {
                grdToolRequest.DataSource = savedResult;

                grdToolRequest.View.FocusedRowHandle = GetRowHandleInGrid(grdToolRequest, "REQUESTDATE", "REQUESTSEQUENCE", requestDate, requestSequence);

                DisplayToolRequestInfoDetail(savedResult.Rows[0]);
            }
        }
        #endregion

        #region DisplaySparePartInboundInfoDetail : 입력받은 인덱스의 그리드의 내용을 화면에 바인딩
        /// <summary>
        /// 입력받은 그리드내의 인덱스에 대한 내용을 화면에 표시한다.
        /// </summary>
        /// <param name="rowHandle"></param>
        private void DisplayToolRequestInfoDetail(DataRow toolReqInfo)
        {
            //해당 업무에 맞는 Enable체크 수행
            //DataRow toolReqInfo = grdToolRequest.View.GetDataRow(rowHandle);
            //grdToolRequest.View.FocusedRowHandle = rowHandle;

            //전역변수로 _inboundNo 할당
            _requestSequence = toolReqInfo["REQUESTSEQUENCE"].ToString();
            _requestDate = toolReqInfo["REQUESTDATE"].ToString();

            deRequestDate.EditValue = toolReqInfo["REQUESTDATE"];
            txtRequestNo.EditValue = toolReqInfo["REQUESTSEQUENCE"];
            cboToolMakeType.EditValue = toolReqInfo["TOOLMAKETYPE"];
            txtRequestDepartment.EditValue = toolReqInfo["REQUESTDEPARTMENT"];
            ucProductCode.ProductDefinitionCode = toolReqInfo["PRODUCTDEFID"].ToString();
            ucProductCode.ProductDefinitionVersion = toolReqInfo["PRODUCTDEFVERSION"].ToString();
            //텍스트가 바뀌어서 유저컨트롤의 자동검색이 실행될 수 있으므로 임의로 막는다.
            ucProductCode.IsSearch = true;
            txtProductDeptName.EditValue = toolReqInfo["PRODUCTDEFNAME"];
            txtRequestQty.EditValue = toolReqInfo["REQUESTQTY"];
            chkIsApprovalUse.Checked = toolReqInfo["ISAPPROVALUSE"].ToString() == "Y" ? true : false;
            txtRequestComment.EditValue = toolReqInfo["REQUESTCOMMENT"];
            txtDescription.EditValue = toolReqInfo["DESCRIPTION"];
            txtToolProgressStatus.EditValue = toolReqInfo["TOOLPROGRESSSTATUSNAME"];
            txtApprovalNo.EditValue = toolReqInfo["APPROVALNO"];

            //수정상태라 판단하여 화면 제어
            ControlEnableProcess("modified");

            //화면상태에 따른 탭및 그리드 제어

            //그리드 데이터 바인딩
            if (toolReqInfo.GetString("TOOLPROGRESSSTATUS") == "Create")
                _currentStatus = "modified";
            else
                _currentStatus = "notModified";

            _currentApprovalStatus = toolReqInfo.GetString("TOOLPROGRESSSTATUS");

            //각 순서에 따라 바인딩
            SearchDetailList();
            SearchLotList();
            if (chkIsApprovalUse.Checked)
                SearchRequestApprovalUser();
            else
                grdApprovalUsers.DataSource = null;

            ProcessToolMake(toolReqInfo["TOOLMAKETYPE"].ToString());

            //그리드의 수정가능 여부
            GridEnableProcess();

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
        #endregion

        #region GetQualification - 제작입고 / 수정출고된 데이터라면 수정이 불가하므로 해당 테이블의 관련건수를 알아온다.
        /// <summary>
        /// 제작입고 / 수정출고된 데이터라면 수정이 불가하므로 해당 테이블의 관련건수를 알아온다.
        /// </summary>
        private int GetQualification(string durableLotID, string durableDefID, string durableDefVersion, string requestDate, string requestSequence)
        {
            Dictionary<string, object> Param = new Dictionary<string, object>();

            DataRow currentRow = grdToolRequest.View.GetFocusedDataRow();

            if (durableLotID != "")
                Param.Add("DURABLELOTID", durableLotID);

            Param.Add("DURABLEDEFID", durableDefID);
            Param.Add("DURABLEDEFVERSION", durableDefVersion);
            Param.Add("REQUESTDATE", requestDate);
            Param.Add("REQUESTSEQUENCE", requestSequence);
            Param = Commons.CommonFunction.ConvertParameter(Param);
            DataTable resultTable = SqlExecuter.Query("GetQualificationUpdateToolRequestByTool", "10001", Param);

            return resultTable.Rows[0].GetInteger("TOOLCOUNT");
        }
        #endregion

        #region DisplayMakeToolPopup : 제작내용을 입력하기 위한 팝업창 호출
        private void DisplayMakeToolPopup()
        {
            if (cboToolMakeType.EditValue.ToString().Equals("1"))
            {
                ShowMessage(MessageBoxButtons.OK, "ToolDetailCodeValidation");
            }
            else if (cboToolMakeType.EditValue.ToString().Equals("Modify") || cboToolMakeType.EditValue.ToString().Equals("Repair"))
            {
                ShowMessage(MessageBoxButtons.OK, "ToolRequestMakeType");
            }
            else if (_currentStatus != "added" && _currentStatus != "modified")
            {
                ShowMessage(MessageBoxButtons.OK, "ToolRequestStatusValidation");
            }
            else
            {
                //의뢰상태가 등록일때와 최초 입력시에만 버튼이 동작해야 한다.                  
                if (ValidateProductCode())
                {
                    //grdToolRequestDetail.View.SetFocusedRowCellValue("ROWSTATUS", "added");// 회사코드
                    //grdToolRequestDetail.View.SetFocusedRowCellValue("PRODUCTDEFID", ucProductCode.ProductDefinitionCode);// 회사코드

                    Popup.RequestToolMakingDetailPopup makePopup = new Popup.RequestToolMakingDetailPopup(Conditions.GetValue("P_PLANTID").ToString());

                    makePopup.ProductDefID = ucProductCode.ProductDefinitionCode;
                    makePopup.dataInputHandler += InputDataToDetailGrid;

                    makePopup.ShowDialog();
                }
                else
                {
                    ShowMessage(MessageBoxButtons.OK, "ToolRequestProductCodeValidation");
                }
            }
        }
        #endregion

        #region InputDataToDetailGrid : 팝업에서 선택한 내용을 제작그리드에 바인딩
        /// <summary>
        /// 팝업에서 선택한 내용을 제작그리드에 바인딩
        /// </summary>
        /// <param name="inputDataTable"></param>
        private void InputDataToDetailGrid(DataTable inputDataTable)
        {
            //같은 값이 있는지 검사
            if (CompareDataInGrid(grdToolRequestDetail.View, inputDataTable, "TOOLCODE", "TOOLVERSION", "TOOLCODE", "TOOLVERSION"))
            {
                //값이 없을 경우 빈 테이블을 바인딩
                DataTable originTable = (DataTable)grdToolRequestDetail.DataSource;

                if (originTable == null)
                {
                    originTable = CreateSearchToolRequestDetailDataTable();

                    //값이 없을 경우 빈 테이블을 바인딩
                    grdToolRequestDetail.DataSource = originTable;
                }
                foreach (DataRow inputRow in inputDataTable.Rows)
                {
                    grdToolRequestDetail.View.AddNewRow();
                    grdToolRequestDetail.View.FocusedRowHandle = grdToolRequestDetail.View.RowCount - 1;

                    grdToolRequestDetail.View.GetFocusedDataRow()["TOOLCODE"] = inputRow["TOOLCODE"];
                    grdToolRequestDetail.View.GetFocusedDataRow()["TOOLVERSION"] = inputRow["TOOLVERSION"];
                    grdToolRequestDetail.View.GetFocusedDataRow()["TOOLCATEGORYID"] = inputRow["TOOLCATEGORYID"];
                    grdToolRequestDetail.View.GetFocusedDataRow()["TOOLCATEGORY"] = inputRow["TOOLCATEGORY"];
                    grdToolRequestDetail.View.GetFocusedDataRow()["TOOLCATEGORYDETAILID"] = inputRow["TOOLCATEGORYDETAILID"];
                    grdToolRequestDetail.View.GetFocusedDataRow()["TOOLCATEGORYDETAIL"] = inputRow["TOOLCATEGORYDETAIL"];
                    grdToolRequestDetail.View.GetFocusedDataRow()["TOOLDETAILID"] = inputRow["TOOLDETAILID"];
                    grdToolRequestDetail.View.GetFocusedDataRow()["TOOLDETAIL"] = inputRow["TOOLDETAIL"];
                    grdToolRequestDetail.View.GetFocusedDataRow()["USELAYER"] = inputRow["FILMUSELAYER1"];
                    grdToolRequestDetail.View.GetFocusedDataRow()["TOOLNAME"] = inputRow["TOOLNAME"];
                    grdToolRequestDetail.View.GetFocusedDataRow()["PRODUCTDEFID"] = inputRow["PRODUCTDEFID"];
                    grdToolRequestDetail.View.GetFocusedDataRow()["REMARK"] = "";
                }

                //첫번째 행에서 포커스를 띄워놓아야 X표시가 안뜬다.
                grdToolRequestDetail.View.AddNewRow();

                grdToolRequestDetail.View.DeleteRow(grdToolRequestDetail.View.RowCount - 1);
            }
            else
            {
                ShowMessage(MessageBoxButtons.OK, "DuplicateData");
            }
        }
        #endregion

        #region CompareDataInGrid : 그리드내의 키값 데이터를 비교하여 중복값이 있는지 검사
        private bool CompareDataInGrid(SmartBandedGridView targetView, DataTable inputTable
            , string targetFirstKeyColumnName, string targetSecondKeyColumnName
            , string inputFirstKeyColumnName, string inputSecondKeyColumnName)
        {
            foreach (DataRow inputRow in inputTable.Rows)
            {
                for (int i = 0; i < targetView.RowCount; i++)
                {
                    if (targetView.GetRowCellValue(i, targetFirstKeyColumnName).Equals(inputRow.GetString(inputFirstKeyColumnName))
                        && targetView.GetRowCellValue(i, targetSecondKeyColumnName).Equals(inputRow.GetString(inputSecondKeyColumnName)))
                        return false;
                }
            }

            return true;
        }

        private bool CompareDataInGrid(SmartBandedGridView targetView, DataTable inputTable, string targetFirstKeyColumnName, string inputFirstKeyColumnName)
        {
            foreach (DataRow inputRow in inputTable.Rows)
            {
                for (int i = 0; i < targetView.RowCount; i++)
                {
                    if (targetView.GetRowCellValue(i, targetFirstKeyColumnName).Equals(inputRow.GetString(inputFirstKeyColumnName)))
                        return false;
                }
            }

            return true;
        }
        #endregion

        #region DisplayUpdateToolPopup : 수정내용을 입력하기 위한 팝업창 호출
        private void DisplayUpdateToolPopup()
        {
            if (cboToolMakeType.EditValue.ToString().Equals("1"))
            {
                ShowMessage(MessageBoxButtons.OK, "ToolDetailCodeValidation");
            }
            else if (cboToolMakeType.EditValue.ToString().Equals("Add") || cboToolMakeType.EditValue.ToString().Equals("New"))
            {
                ShowMessage(MessageBoxButtons.OK, "ToolRequestMakeTypeRepair");
            }
            else if (_currentStatus != "added" && _currentStatus != "modified")
            {
                ShowMessage(MessageBoxButtons.OK, "ToolRequestStatusValidation");
            }
            else
            {
                if (ValidateProductCode())
                {
                    Popup.RequestToolMakingLotPopup updatePopup = new Popup.RequestToolMakingLotPopup(Conditions.GetValue("P_PLANTID").ToString()
                        , ucProductCode.ProductDefinitionCode);

                    updatePopup.dataInputHandler += InputDataToLotGrid;

                    updatePopup.ShowDialog();
                }
                else
                {
                    ShowMessage(MessageBoxButtons.OK, "ToolRequestProductCodeValidation");
                }
            }
        }
        #endregion

        #region InputDataToDetailGrid : 팝업에서 선택한 내용을 수정그리드에 바인딩
        /// <summary>
        /// 팝업에서 선택한 내용을 수정그리드에 바인딩
        /// </summary>
        /// <param name="inputDataTable"></param>
        private void InputDataToLotGrid(DataTable inputDataTable)
        {
            if (CompareDataInGrid(grdToolRequestDetailLOT.View, inputDataTable, "TOOLNUMBER", "TOOLNUMBER"))
            {
                DataTable originTable = (DataTable)grdToolRequestDetailLOT.DataSource;

                if (originTable == null)
                {
                    originTable = CreateSearchToolRequestDetailLotDataTable();

                    //값이 없을 경우 빈 테이블을 바인딩
                    grdToolRequestDetailLOT.DataSource = originTable;
                }

                foreach (DataRow inputRow in inputDataTable.Rows)
                {
                    grdToolRequestDetailLOT.View.AddNewRow();

                    grdToolRequestDetailLOT.View.FocusedRowHandle = grdToolRequestDetail.View.RowCount - 1;

                    grdToolRequestDetailLOT.View.SetFocusedRowCellValue("TOOLCODE", inputRow["TOOLCODE"]);
                    grdToolRequestDetailLOT.View.SetFocusedRowCellValue("TOOLVERSION", inputRow["TOOLVERSION"]);
                    grdToolRequestDetailLOT.View.SetFocusedRowCellValue("TOOLNUMBER", inputRow["TOOLNUMBER"]);
                    grdToolRequestDetailLOT.View.SetFocusedRowCellValue("TOOLNAME", inputRow["TOOLNAME"]);
                    grdToolRequestDetailLOT.View.SetFocusedRowCellValue("AREAID", inputRow["AREAID"]);
                    grdToolRequestDetailLOT.View.SetFocusedRowCellValue("AREANAME", inputRow["AREANAME"]);
                    grdToolRequestDetailLOT.View.SetFocusedRowCellValue("TOOLCATEGORYID", inputRow["TOOLCATEGORYID"]);
                    grdToolRequestDetailLOT.View.SetFocusedRowCellValue("TOOLCATEGORY", inputRow["TOOLCATEGORY"]);
                    grdToolRequestDetailLOT.View.SetFocusedRowCellValue("TOOLCATEGORYDETAILID", inputRow["TOOLCATEGORYDETAILID"]);
                    grdToolRequestDetailLOT.View.SetFocusedRowCellValue("TOOLCATEGORYDETAIL", inputRow["TOOLCATEGORYDETAIL"]);
                    grdToolRequestDetailLOT.View.SetFocusedRowCellValue("TOOLDETAILID", inputRow["TOOLDETAILID"]);
                    grdToolRequestDetailLOT.View.SetFocusedRowCellValue("TOOLDETAIL", inputRow["TOOLDETAIL"]);
                    //grdToolRequestDetailLOT.View.SetFocusedRowCellValue("USELAYER", inputRow["USELAYER"]);
                    grdToolRequestDetailLOT.View.SetFocusedRowCellValue("REMARK", "");
                }

                //grdToolRequestDetailLOT.View.RaiseValidateRow();

                //첫번째 행에서 포커스를 띄워놓아야 X표시가 안뜬다.
                grdToolRequestDetailLOT.View.AddNewRow();

                grdToolRequestDetailLOT.View.DeleteRow(grdToolRequestDetailLOT.View.RowCount - 1);
            }
            else
            {
                ShowMessage(MessageBoxButtons.OK, "DuplicateData");
            }
        }
        #endregion

        #region DeleteDetailGridRow : Detail Grid View의 특정행을 삭제
        private void DelteDetailGridRow()
        {
            if (cboToolMakeType.EditValue.ToString().Equals("1"))
            {
                ShowMessage(MessageBoxButtons.OK, "ToolDetailCodeValidation");
            }
            else if (cboToolMakeType.EditValue.ToString().Equals("Modify") || cboToolMakeType.EditValue.ToString().Equals("Repair"))
            {
                ShowMessage(MessageBoxButtons.OK, "ToolRequestMakeType");
            }
            else if (_currentStatus != "added" && _currentStatus != "modified")
            {
                ShowMessage(MessageBoxButtons.OK, "ToolRequestStatusValidation");
            }
            else
            {
                grdToolRequestDetail.View.DeleteCheckedRows();
            }
        }
        #endregion

        #region DeleteLotGridRow : LOT Grid View의 특정행을 삭제
        private void DeleteLotGridRow()
        {
            if (cboToolMakeType.EditValue.ToString().Equals("1"))
            {
                ShowMessage(MessageBoxButtons.OK, "ToolDetailCodeValidation");
            }
            else if (cboToolMakeType.EditValue.ToString().Equals("Add") || cboToolMakeType.EditValue.ToString().Equals("New"))
            {
                ShowMessage(MessageBoxButtons.OK, "ToolRequestMakeType");
            }
            else if (_currentStatus != "added" && _currentStatus != "modified")
            {
                ShowMessage(MessageBoxButtons.OK, "ToolRequestStatusValidation");
            }
            else
            {
                txtRequestQty.EditValue = Int32.Parse(txtRequestQty.Text) - grdToolRequestDetailLOT.View.GetCheckedRows().Rows.Count;

                grdToolRequestDetailLOT.View.DeleteCheckedRows();
            }
        }
        #endregion

        #region CalculateQtyInGridCell : 특정그리드의 수량을 합산하여 계산
        private void CalculateQtyInGridCell(SmartBandedGrid grdTarget, SmartTextBox txtQty, string gridColumn)
        {
            DataTable qtyTable = grdTarget.DataSource as DataTable;

            int sumQty = 0;

            foreach (DataRow qtyRow in qtyTable.Rows)
            {
                sumQty += qtyRow.GetInteger(gridColumn);
            }

            txtQty.EditValue = sumQty;
        }
        #endregion

        #region GetCurrentApprovalState : 결재를 위한 현재 상태값을 가져온다.
        private string GetCurrentApprovalState()
        {
            if (chkIsApprovalUse.Checked && !_currentApprovalStatus.Equals("Approval"))
            {
                if (_currentStatus.Equals("added"))
                {
                    return "Request";
                }
                else if (_currentApprovalStatus.Equals("Approval"))
                {
                    return "Approval";
                }
                else
                {
                    return "Request";
                }
            }
            else
            {
                return "Create";
            }
        }
        #endregion

        #region IsApprovalUser : 현재로그인한 사용자가 결재가능한 사용자인지 검증
        private bool IsApprovalUser(string userID)
        {
            DataTable approvalTable = (DataTable)grdApprovalUsers.DataSource;

            foreach (DataRow approvalRow in approvalTable.Rows)
            {
                if (approvalRow.GetString("APPROVALUSERID").Equals(userID))
                    return true;
            }
            return false;
        }
        #endregion

        #region ShowMessageInfo : 메세지를 출력한다.
        private void ShowMessageInfo(string messageID)
        {
            ShowMessage(MessageBoxButtons.OK, messageID, "");
        }
        #endregion

        #region ShowPrintData : 의뢰서를 발행전 출력
        private void ShowPrintData()
        {
            //Validation 체크 부분 작성 필요
            this.ShowWaitArea();

            //저장 로직
            try
            {
                btnReport.Enabled = false;
                DataRow originRow = grdToolRequest.View.GetFocusedDataRow();

                if (originRow != null)
                {
                    //if (ValidateSingleContent("Accept", insertRow))
                    //{

                    Dictionary<string, object> values = Conditions.GetValues();

                    #region 기간 검색형 전환 처리 - 만약 시간까지 포함하여 검색한다면 아래 문구는 필요없음            
                    values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                    values.Add("REQUESTDATE", originRow.GetString("REQUESTDATE"));
                    values.Add("REQUESTSEQUENCE", originRow.GetString("REQUESTSEQUENCE"));
                    #endregion
                    values = Commons.CommonFunction.ConvertParameter(values);
                    if (UserInfo.Current.Enterprise.Equals("INTERFLEX"))
                    {
                        DataTable headerTable = SqlExecuter.Query("GetProductInfoForRequestReportWithIFC", "10001", values);
                        DataTable toolTable = SqlExecuter.Query("GetToolRequestInfoForRequestReportWithIFC", "10001", values);
                        DataTable summaryTable = SqlExecuter.Query("GetDurableLotSummaryForRequestReportWithIFC", "10001", values);

                        Popup.PrintRequestToolDocument toolDoc = new Popup.PrintRequestToolDocument();
                        toolDoc.HeaderDataTable = headerTable;
                        toolDoc.ToolInfoTable = toolTable;
                        toolDoc.DurableSummaryTable = summaryTable;

                        //filmDoc.CurrentDataRow = grdFilmRequest.View.GetFocusedDataRow();
                        //filmDoc.SaveData += PrintData;
                        toolDoc.ShowDialog();
                    }
                    else
                    {
                        DataTable toolTable = SqlExecuter.Query("GetToolRequestInfoForRequestReportWithYP", "10001", values);

                        Popup.PrintRequestToolDocumentYP toolDoc = new Popup.PrintRequestToolDocumentYP();
                        toolDoc.ToolInfoTable = toolTable;

                        //filmDoc.CurrentDataRow = grdFilmRequest.View.GetFocusedDataRow();
                        //filmDoc.SaveData += PrintData;
                        toolDoc.ShowDialog();
                    }

                }
                else
                {
                    ShowMessage(MessageBoxButtons.OK, "ValidateRequestToolStatusSelectOne", "");
                }
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
            finally
            {
                this.CloseWaitArea();
                btnReport.Enabled = true;
            }
        }
        #endregion

        #region ChangeSiteCondition - 검색조건의 Site변경시 관련된 쿼리들을 변경
        private void ChangeSiteCondition()
        {
            if (productPopup != null)
                productPopup.SearchQuery = new SqlQuery("GetProductdefidPoplistByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}", $"PRODUCTDEFTYPE=Product");


            if (popupAreaControl != null)
                popupAreaControl.Query = new SqlQuery("GetAreaIDListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}");


            if (popupGridApprovalUser != null)
                popupGridApprovalUser.SearchQuery = new SqlQuery("GetUserListForPopupByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}");

            if (ucProductCode != null)
                ucProductCode.PlantID = Conditions.GetValue("P_PLANTID").ToString();

            //화면초기화
            InitializeInsertForm();
        }
        #endregion
        #endregion
    }
}
