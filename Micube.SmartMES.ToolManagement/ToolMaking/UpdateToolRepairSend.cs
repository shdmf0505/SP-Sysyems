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

namespace Micube.SmartMES.ToolManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 치공구관리 > 치공구수리관리 > 치공구 수정출고
    /// 업  무  설  명  : 치공구 수정의뢰된 항목을 수정출고한다.
    /// 생    성    자  : 김용조
    /// 생    성    일  : 2019-07-25
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class UpdateToolRepairSend : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가
        string _currentStatus;                                      // 현재상태
        string _sendUserID;                                         // 출고자아이디
        string _searchVendorID;
        bool _inSelectorColumnHeader;
        string[] _clipDatas;
        int _clipIndex = 1;
        IDataObject _clipBoardData;
        bool _isGoodCopy = true;

        string _isModify = "N";

        ConditionItemSelectPopup popupGridToolArea;
        ConditionItemComboBox conditionFactoryBox;
        ConditionItemSelectPopup popupGridToolVendor;
        ConditionItemSelectPopup areaCondition;
        ConditionItemSelectPopup productPopup;
        ConditionItemSelectPopup vendorPopup;
        #endregion

        #region 생성자

        public UpdateToolRepairSend()
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

            InitRequiredControl();

            // TODO : 컨트롤 초기화 로직 구성
            InitializeGrid();

            InitializeGridUpdateSend();
        }

        #region InitRequiredControl - 필수입력항목들을 체크한다.
        private void InitRequiredControl()
        {
            SetRequiredValidationControl(lblSendDate);
        }
        #endregion

        #region InitializeGrid - 제작입고목록을 초기화한다.
        /// <summary>        
        /// 치공구내역목록을 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdToolUpdateSend.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Export | GridButtonItem.Restore;  // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기            
            grdToolUpdateSend.View.SetIsReadOnly();
            grdToolUpdateSend.View.AddTextBoxColumn("SENDSTATUS", 100)               //출소상태
                .SetTextAlignment(TextAlignment.Center)
                ;
            grdToolUpdateSend.View.AddTextBoxColumn("REQUESTDATE", 120)             //의뢰일자
                ;                                                                  
            grdToolUpdateSend.View.AddTextBoxColumn("REQUESTSEQUENCE", 80);         //의뢰순번
            grdToolUpdateSend.View.AddTextBoxColumn("TOOLMAKETYPEID")
                .SetIsHidden();                                                     //제작구분명
            grdToolUpdateSend.View.AddTextBoxColumn("TOOLMAKETYPE", 100)
                .SetTextAlignment(TextAlignment.Center)
                ;                                                                   //제작구분명           
            grdToolUpdateSend.View.AddTextBoxColumn("PRODUCTDEFID", 150)
                ;                                                                   //품목코드
            grdToolUpdateSend.View.AddTextBoxColumn("PRODUCTDEFVERSION", 100)
                ;                                                                   //품목코드
            grdToolUpdateSend.View.AddTextBoxColumn("PRODUCTDEFNAME", 350);         //품목명            
            grdToolUpdateSend.View.AddTextBoxColumn("REQUESTQTY", 100)
                ;                                                                   //제작업체
            grdToolUpdateSend.View.AddTextBoxColumn("SENDQTY", 100)
                ;                                                                   //의뢰수량
            grdToolUpdateSend.View.AddTextBoxColumn("DELIVERYDATE", 100)
                ;                                                                   //작업장           
            grdToolUpdateSend.View.AddTextBoxColumn("REQUESTUSER", 80);             //의뢰자
            grdToolUpdateSend.View.AddTextBoxColumn("REQUESTUSERID")
                .SetIsHidden();                                                     //의뢰자
            grdToolUpdateSend.View.AddTextBoxColumn("REQUESTDEPARTMENT", 150);      //의뢰부서
            grdToolUpdateSend.View.AddTextBoxColumn("REQUESTCOMMENT", 300)          //제작사유
                ;
            grdToolUpdateSend.View.AddTextBoxColumn("DESCRIPTION", 300)             //설명
                ;
            //grdToolUpdateSend.View.SetAutoFillColumn("PRODUCTDEFNAME");
            grdToolUpdateSend.View.PopulateColumns();
        }
        #endregion

        #region InitializeGridDetail - 제작입고목록입력을 위한 그리드를 초기화한다.
        /// <summary>        
        /// 제작입고목록입력을 위한 그리드를 초기화한다.
        /// </summary>
        private void InitializeGridUpdateSend()
        {
            // TODO : 그리드 초기화 로직 추가
            grdInputToolUpdateSend.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Export | GridButtonItem.Restore;  // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기       
            grdInputToolUpdateSend.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
                       
            grdInputToolUpdateSend.View.AddTextBoxColumn("TOOLNUMBER", 180)         //Tool 번호
                .SetIsReadOnly(true);
            grdInputToolUpdateSend.View.AddTextBoxColumn("TOOLCODE", 150)           //Tool 코드
                .SetIsReadOnly(true);                                                                               
            grdInputToolUpdateSend.View.AddTextBoxColumn("TOOLVERSION", 100)         //Tool 버전
                .SetIsReadOnly(true);
            grdInputToolUpdateSend.View.AddTextBoxColumn("TOOLNAME", 400)           //출고자
                .SetIsReadOnly(true);
            grdInputToolUpdateSend.View.AddTextBoxColumn("REVDURABLEDEFVERSION", 110)
                .SetIsReadOnly(true);                                               //변경 Tool 버전
            //grdInputToolUpdateSend.View.AddTextBoxColumn("MAKEVENDOR", 150)
            //    .SetIsHidden();                                                     //제작업체
            InitializeVendorPopupColumnInDetailGrid();
            grdInputToolUpdateSend.View.AddTextBoxColumn("VENDORID", 150)
                .SetIsHidden();                                                     //제작업체아이디
            //grdInputToolUpdateSend.View.AddSpinEditColumn("RECEIPTAREA", 150)
            //    ;                                                                 //입고작업장
            grdInputToolUpdateSend.View.AddTextBoxColumn("RECEIPTAREAID")
                .SetIsHidden();                                                     //입고작업장아이디
            InitializeAreaPopupColumnInDetailGrid();

            grdInputToolUpdateSend.View.AddTextBoxColumn("SENDDATE", 180)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly(true);                                               //출고일
            grdInputToolUpdateSend.View.AddTextBoxColumn("SENDUSERID")
                .SetIsHidden();                                                     //출고자아이디
            grdInputToolUpdateSend.View.AddTextBoxColumn("SENDUSER", 120)
                .SetIsReadOnly(true);                                               //출고자
            grdInputToolUpdateSend.View.AddTextBoxColumn("DURABLESTATE", 80)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(true);                                               //Durable State
            grdInputToolUpdateSend.View.AddTextBoxColumn("REQUESTDELIVERYDATE", 120)
                .SetIsReadOnly(true)
                .SetDisplayFormat("yyyy-MM-dd");                                               //입고요청일
            grdInputToolUpdateSend.View.AddTextBoxColumn("PLANDELIVERYDATE", 120)
                .SetIsReadOnly(true)
                .SetDisplayFormat("yyyy-MM-dd");                                               //입고예정일
            grdInputToolUpdateSend.View.AddComboBoxColumn("TOOLFORMCODE", 150, new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CODECLASSID=ToolForm"), "CODENAME", "CODEID")
               .SetTextAlignment(TextAlignment.Center)
               .SetIsReadOnly(true);                                                  //상태 - (이 회면에서 입력해야 한다면 SetIsReadOnly를 풀어준다.
            grdInputToolUpdateSend.View.AddTextBoxColumn("TOOLTYPE", 120)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(true);                                               //Tool 구분
            grdInputToolUpdateSend.View.AddTextBoxColumn("TOOLCATEGORYDETAIL", 180)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(true);                                               //유형1
            grdInputToolUpdateSend.View.AddTextBoxColumn("TOOLDETAIL", 180)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(true);                                               //유형2
            grdInputToolUpdateSend.View.AddTextBoxColumn("ISSENDED")
                .SetIsHidden();                                                    //유형2
            grdInputToolUpdateSend.View.AddTextBoxColumn("RESULTSTATUS")
                .SetIsHidden();                                                    //수리결과등록여부
            grdInputToolUpdateSend.View.AddTextBoxColumn("REPAIRSENDDATE")
               .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
               .SetIsHidden();                                                    //출고일(CT_TOOLREPAIRSENDLOT - CT_TOOLREQUESTDETAILLOT 테이블 연결용)
            grdInputToolUpdateSend.View.AddTextBoxColumn("REPAIRSENDSEQUENCE")
               .SetIsHidden();                                                    //출고순서(CT_TOOLREPAIRSENDLOT - CT_TOOLREQUESTDETAILLOT 테이블 연결용)



            //grdInputToolUpdateSend.View.SetAutoFillColumn("PRODUCTDEFNAME");
            grdInputToolUpdateSend.View.PopulateColumns();
        }
        #endregion

        #region InitializeAreaPopupColumnInDetailGrid - 작업장을 팝업검색할 필드 설정
        /// <summary>
        /// grid ProcesssegmentidLis
        /// </summary>
        private void InitializeAreaPopupColumnInDetailGrid()
        {
            popupGridToolArea = this.grdInputToolUpdateSend.View.AddSelectPopupColumn("RECEIPTAREA", 150, new SqlQuery("GetAreaListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"))
                // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
                .SetPopupLayout("RECEIPTAREA", PopupButtonStyles.Ok_Cancel, true, false)
                // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
                .SetPopupResultCount(1)
                //필수값설정
                .SetValidationIsRequired()
                // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
                .SetPopupResultMapping("RECEIPTAREA", "AREANAME")
                // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
                .SetPopupLayoutForm(500, 600, FormBorderStyle.FixedToolWindow)
                // 그리드의 남은 영역을 채울 컬럼 설정                
                .SetPopupAutoFillColumns("AREANAME")
                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {
                    int i = 0;
                    int currentIndex = grdInputToolUpdateSend.View.GetFocusedDataSourceRowIndex();

                    foreach (DataRow row in selectedRows)
                    {
                        if (i == 0)
                        {
                            grdInputToolUpdateSend.View.GetFocusedDataRow()["RECEIPTAREAID"] = row["AREAID"];
                            grdInputToolUpdateSend.View.GetFocusedDataRow()["RECEIPTAREA"] = row["AREANAME"];
                        }
                        i++;
                    }
                })
            //.SetPopupValidationCustom(ValidationParentAreaIdPopup);
            ;
            // 팝업에서 사용할 조회조건 항목 추가
            popupGridToolArea.Conditions.AddTextBox("AREANAME");
            conditionFactoryBox = popupGridToolArea.Conditions.AddComboBox("FACTORYID", new SqlQuery("GetFactoryListByEqp", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "FACTORYNAME", "FACTORYID")
                ;            

            // 팝업 그리드 설정
            popupGridToolArea.GridColumns.AddTextBoxColumn("AREAID", 300)
                .SetIsHidden();
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
            popupGridToolVendor = grdInputToolUpdateSend.View.AddSelectPopupColumn("MAKEVENDOR", 150, new SqlQuery("GetVendorListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"))
                // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
                .SetPopupLayout("MAKEVENDOR", PopupButtonStyles.Ok_Cancel, true, false)
                // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
                .SetPopupResultCount(1)
                //필수값설정
                .SetValidationIsRequired()
                // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
                .SetPopupResultMapping("MAKEVENDOR", "VENDORNAME")
                // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
                .SetPopupLayoutForm(400, 600, FormBorderStyle.FixedToolWindow)
                // 그리드의 남은 영역을 채울 컬럼 설정                
                .SetPopupAutoFillColumns("VENDORNAME")
                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {
                    int i = 0;
                    int currentIndex = grdInputToolUpdateSend.View.GetFocusedDataSourceRowIndex();

                    foreach (DataRow row in selectedRows)
                    {
                        if (i == 0)
                        {
                            grdInputToolUpdateSend.View.GetFocusedDataRow()["MAKEVENDOR"] = row["VENDORNAME"];
                            grdInputToolUpdateSend.View.GetFocusedDataRow()["VENDORID"] = row["VENDORID"];
                        }
                        i++;
                    }
                })
            //.SetPopupValidationCustom(ValidationParentAreaIdPopup);
            ;
            // 팝업에서 사용할 조회조건 항목 추가

            popupGridToolVendor.Conditions.AddTextBox("VENDORNAME");

            // 팝업 그리드 설정
            popupGridToolVendor.GridColumns.AddTextBoxColumn("VENDORID", 300)
                .SetIsHidden();
            popupGridToolVendor.GridColumns.AddTextBoxColumn("VENDORNAME", 300)
                .SetIsReadOnly();
        }
        #endregion
        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            btnSearchTool.Click += BtnSearchTool_Click;            
            btnInitialize.Click += BtnInitialize_Click;
            btnModify.Click += BtnModify_Click;
            btnErase.Click += BtnErase_Click;

            grdToolUpdateSend.View.FocusedRowChanged += grdToolUpdateSend_FocusedRowChanged;
            grdInputToolUpdateSend.View.ShowingEditor += grdInputToolUpdateSend_ShowingEditor;
            grdInputToolUpdateSend.View.CheckStateChanged += grdInputToolUpdateSend_CheckStateChanged;
            grdInputToolUpdateSend.View.MouseUp += View_MouseUp;
            grdInputToolUpdateSend.View.CellValueChanged += grdInputToolUpdateSend_CellValueChanged;

            Shown += UpdateToolRepairSend_Shown;
        }

        #region UpdateToolRepairSend_Shown - Site관련정보를 화면로딩후 설정한다.
        private void UpdateToolRepairSend_Shown(object sender, EventArgs e)
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

        #region grdInputToolUpdateSend_CellValueChanged - 그리드의 셀 변경이벤트
        private void grdInputToolUpdateSend_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "MAKEVENDOR")
            {
                grdInputToolUpdateSend.View.CellValueChanged -= grdInputToolUpdateSend_CellValueChanged;

                DataRow row = grdInputToolUpdateSend.View.GetDataRow(e.RowHandle);

                if (row.GetString("MAKEVENDOR").Equals(""))
                {
                    grdInputToolUpdateSend.View.CellValueChanged += grdInputToolUpdateSend_CellValueChanged;
                    return;
                }
                GetSingleMakeVendor(row.GetString("MAKEVENDOR"), e.RowHandle);
                grdInputToolUpdateSend.View.CellValueChanged += grdInputToolUpdateSend_CellValueChanged;
            }
            if (e.Column.FieldName == "RECEIPTAREA")
            {
                grdInputToolUpdateSend.View.CellValueChanged -= grdInputToolUpdateSend_CellValueChanged;

                DataRow row = grdInputToolUpdateSend.View.GetDataRow(e.RowHandle);

                if (row["RECEIPTAREA"].ToString().Equals(""))
                {
                    grdInputToolUpdateSend.View.CellValueChanged += grdInputToolUpdateSend_CellValueChanged;
                    return;
                }
                GetSingleReceiptArea(row.GetString("RECEIPTAREA"), e.RowHandle);
                grdInputToolUpdateSend.View.CellValueChanged += grdInputToolUpdateSend_CellValueChanged;
            }
        }
        #endregion

        #region View_MouseUp - 체크박스전체선택 관련이벤트
        private void View_MouseUp(object sender, MouseEventArgs e)
        {
            DevExpress.XtraGrid.Views.BandedGrid.ViewInfo.BandedGridHitInfo hi = grdInputToolUpdateSend.View.CalcHitInfo(e.Location);
            _inSelectorColumnHeader = false;
            if (hi.InColumn && !hi.InRow)
            {
                if (hi.Column.FieldName.Equals("_INTERNAL_CHECKMARK_SELECTION_"))
                {
                    grdInputToolUpdateSend.View.CheckStateChanged -= grdInputToolUpdateSend_CheckStateChanged;
                    _inSelectorColumnHeader = true;

                    for (int i = 0; i < grdInputToolUpdateSend.View.RowCount; i++)
                    {
                        grdInputToolUpdateSend.View.FocusedRowHandle = i;
                        grdInputToolUpdateSend_CheckStateChanged(sender, new EventArgs());
                    }

                    grdInputToolUpdateSend.View.CheckStateChanged += grdInputToolUpdateSend_CheckStateChanged;
                }
            }
        }
        #endregion

        #region grdInputToolUpdateSend_CheckStateChanged - 그리드내의 체크박스 변경이벤트
        private void grdInputToolUpdateSend_CheckStateChanged(object sender, EventArgs e)
        {
            if (grdInputToolUpdateSend.View.GetFocusedDataRow().GetString("ISSENDED").Equals("Y"))
                grdInputToolUpdateSend.View.SetFocusedRowCellValue("_INTERNAL_CHECKMARK_SELECTION_", true);// .CheckRow(grdInputToolUpdateSend.View.FocusedRowHandle, true);
            else
            {
                if (grdInputToolUpdateSend.View.IsRowChecked(grdInputToolUpdateSend.View.FocusedRowHandle))
                {
                    DateTime sendDate = DateTime.Now;
                    grdInputToolUpdateSend.View.SetFocusedRowCellValue("SENDDATE", sendDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    grdInputToolUpdateSend.View.SetFocusedRowCellValue("SENDUSERID", UserInfo.Current.Id);
                    grdInputToolUpdateSend.View.SetFocusedRowCellValue("SENDUSER", UserInfo.Current.Name);
                }
                else
                {
                    grdInputToolUpdateSend.View.SetFocusedRowCellValue("SENDDATE", "");
                    grdInputToolUpdateSend.View.SetFocusedRowCellValue("SENDUSERID", "");
                    grdInputToolUpdateSend.View.SetFocusedRowCellValue("SENDUSER", "");
                }
            }
        }
        #endregion

        #region grdInputToolUpdateSend_ShowingEditor - 그리드 입력제어이벤트
        private void grdInputToolUpdateSend_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (grdInputToolUpdateSend.View.GetFocusedDataRow().GetString("ISSENDED").Equals("Y"))
            {
                e.Cancel = true;
            }            
        }
        #endregion

        #region BtnErase_Click - 제거버튼이벤트
        private void BtnErase_Click(object sender, EventArgs e)
        {
            if (IsCurrentProcess())
                DeleteLotGridRow();
            else
                this.ShowMessage(MessageBoxButtons.OK, "CURRENTPROCESSREMOVEDATA", "");
        }
        #endregion

        #region BtnModify_Click - 수정버튼이벤트
        private void BtnModify_Click(object sender, EventArgs e)
        {
            if (IsCurrentProcess())
                SaveData();
            else
                this.ShowMessage(MessageBoxButtons.OK, "CURRENTPROCESSUUPDATEDATA", "");
        }
        #endregion

        #region grdToolUpdateSend_FocusedRowChanged - 그리드의 행변경이벤트
        private void grdToolUpdateSend_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DisplayToolRepairSendInfo();
        }
        #endregion

        #region BtnInitialize_Click - 초기화버튼이벤트
        private void BtnInitialize_Click(object sender, EventArgs e)
        {
            InitializeInsertForm();
        }
        #endregion

        #region BtnSave_Click - 저장버튼이벤트
        private void BtnSave_Click(object sender, EventArgs e)
        {
            
        }
        #endregion

        #region BtnSearchTool_Click - 툴선택이벤트
        private void BtnSearchTool_Click(object sender, EventArgs e)
        {
            if(IsCurrentProcess())
                DisplayUpdateToolPopup();
            else
                this.ShowMessage(MessageBoxButtons.OK, "AfterInitValidation", "");
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

        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            //base.OnToolbarClick(sender, e);
            SmartButton btn = sender as SmartButton;

            if (btn.Name.ToString().Equals("Save"))
                BtnModify_Click(sender, e);
            else if (btn.Name.ToString().Equals("Delete"))
                BtnErase_Click(sender, e);
            else if (btn.Name.ToString().Equals("SelectTool"))
                BtnSearchTool_Click(sender, e);
            else if (btn.Name.ToString().Equals("Initialization"))
                BtnInitialize_Click(sender, e);
            else if (btn.Name.ToString().Equals("Cancel"))
                CancelData();
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
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            #endregion
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable toolRepairResult = SqlExecuter.Query("GetNewSendToolListByTool", "10001", values);

            grdToolUpdateSend.DataSource = toolRepairResult;

            if (toolRepairResult.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }
            else
            {
                grdToolUpdateSend.View.FocusedRowHandle = 0;
                DisplayToolRepairSendInfo();
            }

            _currentStatus = "modified";
        }
        #endregion

        #region Research - 재검색(데이터 입력후와 같은 경우에 사용)
        /// <summary>
        /// 저장 삭제후 재 검색시 사용
        /// 생성 및 수정시 발생한 아이디를 통해 자동 선택되도록 유도하며 삭제시에는 null로 매개변수를 받아서 첫번째 행을 보여준다. 행이 없다면 빈 값으로 설정
        /// </summary>
        /// <param name="inboudNo">입력 및 수정시 발생한 값의 아이디.</param>
        private void Research(string sendSequence, string sendDate)
        {
            // TODO : 조회 SP 변경
            InitializeInsertForm();

            var values = Conditions.GetValues();

            #region 기간 검색형 전환 처리 - 만약 시간까지 포함하여 검색한다면 아래 문구는 필요없음            
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            #endregion
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable sparePartSearchResult = SqlExecuter.Query("GetNewSendToolListByTool", "10001", values);

            if (sparePartSearchResult.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdToolUpdateSend.DataSource = sparePartSearchResult;

            //현재 작업한 결과를 선택할 수 있도록 한다.
            if (sendSequence == null && sendDate == null) //삭제 작업후
            {
                //값이 하나도 없다면 빈 화면
                if (grdToolUpdateSend.View.RowCount == 0)
                {
                    //입력대기 화면으로 설정
                    InitializeInsertForm();
                }
                else
                {
                    //첫번재 값으로 설정
                    grdToolUpdateSend.View.FocusedRowHandle = 0;
                    DisplayToolRepairSendInfo();
                }
            }
            else //값이 있다면 해당 값에 맞는 행을 찾아서 선택
            {
                ViewSavedData(sendDate, sendSequence);
            }

            _currentStatus = "modified";
        }
        #endregion

        #region SearchLotList - 수정출고정보의 LOT을 조회한다.
        /// <summary>
        /// SearchLotList - 수정출고정보의 LOT을 조회한다.
        /// </summary>
        private void SearchLotList(string requestDate, string requestSequence)
        {
            DataRow currentRow = grdToolUpdateSend.View.GetFocusedDataRow();
            if ( currentRow != null)
            {
                Dictionary<string, object> values = new Dictionary<string, object>();

                #region 기간 검색형 전환 처리 - 만약 시간까지 포함하여 검색한다면 아래 문구는 필요없음            
                values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                values.Add("REQUESTDATE", Convert.ToDateTime(requestDate).ToString("yyyy-MM-dd"));
                values.Add("REQUESTSEQUENCE", requestSequence);
                #endregion
                values = Commons.CommonFunction.ConvertParameter(values);
                DataTable searchResult = SqlExecuter.Query("GetNewSendToolDetailListByTool", "10001", values);

                if (searchResult.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
                {
                    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                }

                grdInputToolUpdateSend.DataSource = searchResult;
            }
            _currentStatus = "modified";
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
            InitializeConditionSendStatus();
            InitializeCondition_ToolCodePopup();
            InitializeRepairVendors();
            //InitializeConditionArea();
            InitializeConditionAreaPopup();
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
               .SetValidationIsRequired()
               .SetDefault(UserInfo.Current.Plant, "PLANTID") //기본값 설정 UserInfo.Current.Plant
            ;
        }

        /// <summary>
        /// 작업장 설정 
        /// </summary>
        private void InitializeConditionArea()
        {
            var areaBox = Conditions.AddComboBox("AREAID", new SqlQuery("GetAreaListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "AREANAME", "AREAID")
               .SetLabel("AREA")
               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
               .SetPosition(0.2)
               .SetValidationIsRequired()
               .SetDefault(UserInfo.Current.Area, "AREAID") 
            ;
        }

        #region InitializeConditionAreaPopup : 작업장 검색조건
        /// <summary>
        /// 작업장 검색조건
        /// </summary>
        private void InitializeConditionAreaPopup()
        {
            areaCondition = Conditions.AddSelectPopup("AREAID", new SqlQuery("GetAreaListByTool", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CURRENTLOGINID={UserInfo.Current.Id}"))
            .SetPopupLayout("AREAID", PopupButtonStyles.Ok_Cancel, true, true)
            .SetPopupLayoutForm(600, 800, FormBorderStyle.FixedToolWindow)
            .SetPopupAutoFillColumns("AREAID")
            .SetLabel("AREAID")
            .SetPopupResultCount(1)
            .SetPosition(0.2)
            .SetPopupResultMapping("AREAID", "AREAID")
             .SetPopupApplySelection((selectedRows, dataGridRow) =>
             {
                 foreach (DataRow row in selectedRows)
                 {
                     if (row.GetString("ISMODIFY").Equals("Y"))
                     {
                         if (pnlToolbar.Controls["layoutToolbar"] != null)
                         {
                             if (pnlToolbar.Controls["layoutToolbar"].Controls["Save"] != null)pnlToolbar.Controls["layoutToolbar"].Controls["Save"].Enabled = true;
                             if (pnlToolbar.Controls["layoutToolbar"].Controls["Delete"] != null) pnlToolbar.Controls["layoutToolbar"].Controls["Delete"].Enabled = true;
                         }
                     }                     
                     else
                     {
                         if (pnlToolbar.Controls["layoutToolbar"] != null)
                         {
                             if (pnlToolbar.Controls["layoutToolbar"].Controls["Save"] != null) pnlToolbar.Controls["layoutToolbar"].Controls["Save"].Enabled = false;
                             if (pnlToolbar.Controls["layoutToolbar"].Controls["Delete"] != null) pnlToolbar.Controls["layoutToolbar"].Controls["Delete"].Enabled = false;
                         }
                     }
                 }
             })
            ;

            areaCondition.SetValidationIsRequired();
            areaCondition.ValueFieldName = "AREAID";
            areaCondition.DisplayFieldName = "AREANAME";
            //areaCondition.SetDefault(UserInfo.Current.Area, "AREAID");


            areaCondition.Conditions.AddTextBox("AREANAME");

            areaCondition.GridColumns.AddTextBoxColumn("AREAID", 150)
                .SetIsHidden();
            areaCondition.GridColumns.AddTextBoxColumn("AREANAME", 200);
        }
        #endregion

        /// <summary>
        /// 제작업체 설정
        /// </summary>
        private void InitializeConditionSendStatus()
        {
            var planttxtbox = Conditions.AddComboBox("SENDSTATUS", new SqlQuery("GetCodeListByToolWithLanguage", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CODECLASSID=OutboundYN"), "CODENAME", "CODEID")
               .SetLabel("SENDSTATUS")
               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
               .SetPosition(2.1)
               .SetEmptyItem("", "", true)
               .SetDefault("N")
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

        #region InitializeRepairVendors : 팝업창 제어
        /// <summary>
        /// 팝업창 제어
        /// </summary>
        private void InitializeRepairVendors()
        {
            vendorPopup = Conditions.AddSelectPopup("VENDORNAME", new SqlQuery("GetVendorListByTool", "10001", $"LANGUAGETYPE={ UserInfo.Current.LanguageType }", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"))
            .SetPopupLayout("MAKEVENDOR", PopupButtonStyles.Ok_Cancel, true, false)
            .SetPopupLayoutForm(500, 600, FormBorderStyle.FixedToolWindow)
            .SetPopupResultMapping("VENDORNAME", "VENDORNAME")
            .SetLabel("MAKEVENDOR")
            .SetPopupResultCount(1)
            .SetPosition(1.2)
            .SetPopupApplySelection((selectedRows, dataGridRow) =>
            {
                int i = 0;

                foreach (DataRow row in selectedRows)
                {
                    if (i == 0)
                    {
                        _searchVendorID = row["VENDORID"].ToString();
                    }
                    i++;
                }
            })
            ;

            // 팝업에서 사용할 조회조건 항목 추가
            vendorPopup.Conditions.AddTextBox("VENDORNAME");

            // 팝업 그리드 설정
            vendorPopup.GridColumns.AddTextBoxColumn("VENDORID", 300)
                .SetIsHidden();
            vendorPopup.GridColumns.AddTextBoxColumn("VENDORNAME", 300)
                .SetIsReadOnly();

        }
        #endregion
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


        #region ValidateContent - 입력 Validation
        private bool ValidateContent(out string messageCode)
        {
            //각각의 상태에 따라 보여줄 메세지가 상이하다면 각각의 분기에서 메세지를 표현해야 한다.            
            messageCode = "";

            if (grdInputToolUpdateSend.View.RowCount == 0)
            {
                messageCode = "ValidateSelectToolForUpdateTool";
                return false;
            }
            
            DataTable inputTable = grdInputToolUpdateSend.View.GetCheckedRows();

            if (inputTable == null || inputTable.Rows.Count == 0)
            {
                messageCode = "ValidateSelectToolForUpdateTool";
                return false;
            }

            foreach(DataRow inputRow in inputTable.Rows)
            {
                if (!ValidateCellInGrid(inputRow, new string[] { "RECEIPTAREAID", "RECEIPTAREA" }))
                {
                    messageCode = "ValidateReceiveAreaStatusForReceipt";
                    return false;
                }
            }

            return true;
        }
        #endregion

        #region ValidateCancelContent - 출고취소 Validation
        private bool ValidateCancelContent(out string messageCode)
        {
            //각각의 상태에 따라 보여줄 메세지가 상이하다면 각각의 분기에서 메세지를 표현해야 한다.            
            messageCode = "";

            if (grdInputToolUpdateSend.View.RowCount == 0)
            {
                messageCode = "ValidateSelectToolForUpdateTool"; //선택된 항목이 없다는 메세지
                return false;
            }

            DataTable inputTable = grdInputToolUpdateSend.View.GetCheckedRows();

            if (inputTable == null || inputTable.Rows.Count == 0)
            {
                messageCode = "ValidateSelectToolForUpdateTool"; //선택된 항목이 없다는 메세지
                return false;
            }

            foreach (DataRow inputRow in inputTable.Rows)
            {
                //NonResult상태 즉 수리결과등록이전 상태만 취소할 수 있다.
                if (!inputRow.GetString("ResultStatus").Equals("NonResult"))
                {
                    messageCode = "ValidateCancelRepairToolRequest"; //수리결과등록되거나 입고된 치공구는 출고취소할 수 없습니다.
                    return false;
                }
            }

            return true;
        }
        #endregion

        #region ValidateComboBoxEqualValue - 2개의 콤보박스 값 비교
        private bool ValidateComboBoxEqualValue(SmartComboBox originBox, SmartComboBox targetBox)
        {
            //두 콤보박스의 값이 같으면 안된다.
            if (originBox.EditValue.Equals(targetBox.EditValue))
                return false;
            return true;
        }
        #endregion

        #region ValidateNumericBox - 숫자형텍스트박스의 Validation - 기준숫자보다 높은지 점검
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

        #region ValidateEditValue - 값점검이벤트
        private bool ValidateEditValue(object editValue)
        {
            if (editValue == null)
                return false;

            if (editValue.ToString() == "")
                return false;

            return true;
        }
        #endregion

        #region ValidateCellInGrid - 그리드내의 특정 컬럼 Validation
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
                _currentStatus = "added";           //현재 화면의 상태는 입력중이어야 한다.

                //의뢰일은 오늘날짜를 기본으로 한다.
                DateTime dateNow = DateTime.Now;
                deSendDate.EditValue = dateNow;

                //의뢰순번은 공값으로 한다.
                txtSendSequence.EditValue = null;

                //의뢰부서는 사용자의 부서를 기본으로 한다.
                txtSendor.EditValue = UserInfo.Current.Name;

                _sendUserID = UserInfo.Current.Id;

                grdInputToolUpdateSend.DataSource = null;

                //컨트롤 접근여부는 작성상태로 변경한다.
                ControlEnableProcess("added");                

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
                deSendDate.ReadOnly = false;
                txtSendSequence.ReadOnly = true;
                txtSendor.ReadOnly = true;
            }
            else if (currentStatus == "modified") //
            {
                deSendDate.ReadOnly = true;
                txtSendSequence.ReadOnly = true;
                txtSendor.ReadOnly = true;
            }
            else
            {
                deSendDate.ReadOnly = true;
                txtSendSequence.ReadOnly = true;
                txtSendor.ReadOnly = true;
            }
        }
        #endregion

        #region LoadData - 팝업창에서 호출하는 메소드
        /// <summary>
        /// 팝업창에서 호출하는 메소드
        /// </summary>
        private void LoadData(DataTable table)
        {
            DataTable originTable = (DataTable)grdInputToolUpdateSend.DataSource;

            if (originTable == null)
            {
                originTable = CreateSaveDatatable(false);

                //값이 없을 경우 빈 테이블을 바인딩
                grdInputToolUpdateSend.DataSource = originTable;
            }
            DateTime sendDate = (DateTime)deSendDate.EditValue;
            foreach (DataRow inputRow in table.Rows)
            {
                grdInputToolUpdateSend.View.AddNewRow();

                grdInputToolUpdateSend.View.FocusedRowHandle = grdInputToolUpdateSend.View.RowCount - 1;

                grdInputToolUpdateSend.View.SetFocusedRowCellValue("REQUESTDATE", inputRow["REQUESTDATE"]);
                grdInputToolUpdateSend.View.SetFocusedRowCellValue("REQUESTSEQUENCE", inputRow["REQUESTSEQUENCE"]);
                grdInputToolUpdateSend.View.SetFocusedRowCellValue("SENDDATE", sendDate.ToString("yyyy-MM-dd HH:mm:ss"));
                grdInputToolUpdateSend.View.SetFocusedRowCellValue("SENDSEQUENCE", txtSendSequence.EditValue);
                grdInputToolUpdateSend.View.SetFocusedRowCellValue("USERID", _sendUserID);
                grdInputToolUpdateSend.View.SetFocusedRowCellValue("SENDOR", txtSendor.EditValue);
                grdInputToolUpdateSend.View.SetFocusedRowCellValue("TOOLNUMBER", inputRow["TOOLNUMBER"]);
                grdInputToolUpdateSend.View.SetFocusedRowCellValue("RECEIPTAREA", inputRow["RECEIPTAREA"]);
                grdInputToolUpdateSend.View.SetFocusedRowCellValue("RECEIPTAREAID", inputRow["AREAID"]);
                grdInputToolUpdateSend.View.SetFocusedRowCellValue("PRODUCTDEFID", inputRow["PRODUCTDEFID"]);
                grdInputToolUpdateSend.View.SetFocusedRowCellValue("PRODUCTDEFNAME", inputRow["PRODUCTDEFNAME"]);
                grdInputToolUpdateSend.View.SetFocusedRowCellValue("USEDCOUNT", inputRow["USEDCOUNT"]);
                grdInputToolUpdateSend.View.SetFocusedRowCellValue("CLEANLIMIT", inputRow["CLEANLIMIT"]);
                grdInputToolUpdateSend.View.SetFocusedRowCellValue("TOTALCLEANCOUNT", inputRow["TOTALCLEANCOUNT"]);
                grdInputToolUpdateSend.View.SetFocusedRowCellValue("TOTALUSEDCOUNT", inputRow["TOTALUSEDCOUNT"]);
                grdInputToolUpdateSend.View.SetFocusedRowCellValue("USEDLIMIT", inputRow["USEDLIMIT"]);
                grdInputToolUpdateSend.View.SetFocusedRowCellValue("TOTALREPAIRCOUNT", inputRow["TOTALREPAIRCOUNT"]);
                grdInputToolUpdateSend.View.SetFocusedRowCellValue("MAKEVENDOR", inputRow["MAKEVENDOR"]);
                grdInputToolUpdateSend.View.SetFocusedRowCellValue("VENDORID", inputRow["VENDORID"]);
                grdInputToolUpdateSend.View.SetFocusedRowCellValue("TOOLCODE", inputRow["TOOLCODE"]);
                grdInputToolUpdateSend.View.SetFocusedRowCellValue("DURABLEDEFVERSION", inputRow["DURABLEDEFVERSION"]);
                grdInputToolUpdateSend.View.SetFocusedRowCellValue("SENDAREA", inputRow["SENDAREANAME"]);
                grdInputToolUpdateSend.View.SetFocusedRowCellValue("SENDAREAID", inputRow["SENDAREAID"]);
                grdInputToolUpdateSend.View.SetFocusedRowCellValue("RESULTSTATUS", "BeforeSend");
            }

            //첫번째 행에서 포커스를 띄워놓아야 X표시가 안뜬다.
            grdInputToolUpdateSend.View.AddNewRow();

            grdInputToolUpdateSend.View.DeleteRow(grdInputToolUpdateSend.View.RowCount - 1);            
        }
        #endregion

        #region CreateSaveParentDatatable : ToolRepairSend 입력/수정/삭제를 위한 DataTable의 Template 생성
        /// <summary>
        /// ToolRequest 입력/수정/삭제를 위한 DataTable의 Template 생성
        /// 입력시에는 ToolRequest와 ToolRequestDetail에 입력하고
        /// 수정시에는 ToolRequest와 ToolRequestDetail 및 ToolRequestDetailLot에 까지 데이터를 기입한다.
        /// </summary>
        /// <returns></returns>
        private DataTable CreateSaveParentDatatable(bool useState)
        {
            DataTable dt = new DataTable();
            dt.TableName = "toolRepairSendList";
            //===================================================================================
            //Key값 ToolRequest 및 ToolRequestDetail
            dt.Columns.Add("SENDDATE");
            dt.Columns.Add("SENDSEQUENCE");
            dt.Columns.Add("SENDOR");
            dt.Columns.Add("TOOLNUMBER");
            dt.Columns.Add("RECEIPTAREA");
            dt.Columns.Add("SENDAREA");
            dt.Columns.Add("PRODUCTDEFID");
            dt.Columns.Add("PRODUCTDEFNAME");
            dt.Columns.Add("REQUESTDATE");
            dt.Columns.Add("REQUESTSEQUENCE");
            dt.Columns.Add("USEDCOUNT");
            dt.Columns.Add("CLEANLIMIT");
            dt.Columns.Add("TOTALCLEANCOUNT");
            dt.Columns.Add("TOTALUSEDCOUNT");
            dt.Columns.Add("USEDLIMIT");
            dt.Columns.Add("TOTALREPAIRCOUNT");
            dt.Columns.Add("MAKEVENDOR");
            dt.Columns.Add("ENTERPRISEID");
            dt.Columns.Add("PLANTID");

            dt.Columns.Add("DESCRIPTION");
            dt.Columns.Add("TOOLREPAIRTYPE");
            dt.Columns.Add("INSERTTYPE");


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

            if(useState)
                dt.Columns.Add("_STATE_");

            return dt;
        }
        #endregion

        #region CreateSaveDatatable : ToolRepairSendLot 입력/수정/삭제를 위한 DataTable의 Template 생성
        /// <summary>
        /// ToolRequest 입력/수정/삭제를 위한 DataTable의 Template 생성
        /// 입력시에는 ToolRequest와 ToolRequestDetail에 입력하고
        /// 수정시에는 ToolRequest와 ToolRequestDetail 및 ToolRequestDetailLot에 까지 데이터를 기입한다.
        /// </summary>
        /// <returns></returns>
        private DataTable CreateSaveDatatable(bool useState)
        {
            DataTable dt = new DataTable();
            dt.TableName = "toolRepairSendLotList";
            //===================================================================================
            //Key값 ToolRequest 및 ToolRequestDetail
            dt.Columns.Add("SENDDATE");
            dt.Columns.Add("SENDSEQUENCE");
            dt.Columns.Add("SENDOR");
            dt.Columns.Add("USERID");
            dt.Columns.Add("TOOLNUMBER");
            dt.Columns.Add("RECEIPTAREA");
            dt.Columns.Add("RECEIPTAREAID");
            dt.Columns.Add("AREAID");
            dt.Columns.Add("SENDAREA");
            dt.Columns.Add("PRODUCTDEFID");
            dt.Columns.Add("PRODUCTDEFNAME");
            dt.Columns.Add("REQUESTDATE");
            dt.Columns.Add("REQUESTSEQUENCE");
            dt.Columns.Add("TOOLCODE");
            dt.Columns.Add("DURABLEDEFVERSION");
            dt.Columns.Add("USEDCOUNT");
            dt.Columns.Add("CLEANLIMIT");
            dt.Columns.Add("TOTALCLEANCOUNT");
            dt.Columns.Add("TOTALUSEDCOUNT");
            dt.Columns.Add("USEDLIMIT");
            dt.Columns.Add("TOTALREPAIRCOUNT");
            dt.Columns.Add("MAKEVENDOR");
            dt.Columns.Add("VENDORID");
            dt.Columns.Add("ENTERPRISEID");
            dt.Columns.Add("PLANTID");

            dt.Columns.Add("TOOLREPAIRTYPE");
            dt.Columns.Add("FINISHDATE");
            dt.Columns.Add("FINISHER");
            dt.Columns.Add("RECEIPTDATE");
            dt.Columns.Add("RECEIPTSEQUENCE");
            dt.Columns.Add("LOTHISTKEY");
            dt.Columns.Add("DESCRIPTION");

            dt.Columns.Add("WEIGHT");
            dt.Columns.Add("HEIGHT");
            dt.Columns.Add("HORIZONTAL");
            dt.Columns.Add("VERTICAL");
            dt.Columns.Add("THICKNESS");

            dt.Columns.Add("INSERTTYPE");


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
            
            if(useState)
                dt.Columns.Add("_STATE_");

            return dt;
        }
        #endregion

        #region DisplayUpdateToolPopup
        private void DisplayUpdateToolPopup()
        {
            Popup.ToolNumberForUpdatePopup toolNumberPopup = new Popup.ToolNumberForUpdatePopup();
            toolNumberPopup.choiceHandler += LoadData;

            toolNumberPopup.ShowDialog();
        }
        #endregion

        #region DisplayToolRepairSendInfo : 그리드에서 행 선택시 상세정보를 표시
        /// <summary>
        /// 그리드에서 행 선택시 상세정보를 표시하는 메소드
        /// </summary>
        private void DisplayToolRepairSendInfo()
        {
            //포커스 행 체크 
            if (grdToolUpdateSend.View.FocusedRowHandle < 0) return;

            DisplayToolRepairSendInfoDetail(grdToolUpdateSend.View.GetFocusedDataRow());
        }
        #endregion

        #region ViewSavedData : 새로 입력된 데이터를 화면에 바인딩
        /// <summary>
        /// 새로 입력된 데이터를 화면에 바인딩
        /// </summary>
        private void ViewSavedData(string sendDate, string sendSequence)
        {
            #region 기간 검색형 전환 처리 - 만약 시간까지 포함하여 검색한다면 아래 문구는 필요없음            
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            //values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            //values.Add("PLANTID", UserInfo.Current.Plant);
            values.Add("SENDDATE", Convert.ToDateTime(sendDate).ToString("yyyy-MM-dd HH:mm:ss"));
            values.Add("SENDSEQUENCE", Int32.Parse(sendSequence));
            #endregion
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable savedResult = SqlExecuter.Query("GetToolUpdateSendListByTool", "10001", values);

            if (savedResult.Rows.Count > 0)
            {
                DisplayToolRepairSendInfoDetail(savedResult.Rows[0]);
            }
        }
        #endregion

        #region DisplayToolRepairSendInfoDetail : 입력받은 인덱스의 그리드의 내용을 화면에 바인딩
        /// <summary>
        /// 입력받은 그리드내의 인덱스에 대한 내용을 화면에 표시한다.
        /// </summary>
        /// <param name="rowHandle"></param>
        private void DisplayToolRepairSendInfoDetail(DataRow currentRow)
        {
            //이벤트를 제거
            grdInputToolUpdateSend.View.CheckStateChanged -= grdInputToolUpdateSend_CheckStateChanged;

            //수정상태라 판단하여 화면 제어
            ControlEnableProcess("modified");

            //화면상태에 따른 탭및 그리드 제어
            deSendDate.EditValue = currentRow.GetString("REQUESTDATE");
            txtSendSequence.EditValue = currentRow.GetString("REQUESTSEQUENCE");
            txtSendor.EditValue = currentRow.GetString("REQUESTUSER");
            _sendUserID = currentRow.GetString("REQUESTUSERID");

            //그리드 데이터 바인딩

            SearchLotList(currentRow.GetString("REQUESTDATE"), currentRow.GetString("REQUESTSEQUENCE"));

            _currentStatus = "modified";
            //각 순서에 따라 바인딩

            //이미 출고된 데이터 체크 처리
            for (int index = 0; index < grdInputToolUpdateSend.View.RowCount; index++)
                if (grdInputToolUpdateSend.View.GetRowCellValue(index, "ISSENDED").ToString().Equals("Y"))
                    grdInputToolUpdateSend.View.CheckRow(index, true);

            //이벤트를 추가
            grdInputToolUpdateSend.View.CheckStateChanged += grdInputToolUpdateSend_CheckStateChanged;
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

        #region SaveData : 입력/수정을 수행
        private void SaveData()
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
                if (ValidateContent(out messageCode))
                {
                    DataSet toolRepairSet = new DataSet();
                    //치공구 제작의뢰를 입력
                    DataTable toolRepairTable = CreateSaveParentDatatable(true);                    

                    DataRow toolRepairRow = toolRepairTable.NewRow();

                    DateTime sendDate = DateTime.Now;

                    DataRow repairRow = grdToolUpdateSend.View.GetFocusedDataRow();
                                        
                    toolRepairRow["SENDDATE"] = sendDate.ToString("yyyy-MM-dd HH:mm:ss");
                    toolRepairRow["SENDOR"] = UserInfo.Current.Id;
                    toolRepairRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                    toolRepairRow["PLANTID"] = Conditions.GetValue("P_PLANTID");
                    toolRepairRow["TOOLREPAIRTYPE"] = "Repair";
                    //수정출고인지 수리출고 인지 구분
                    toolRepairRow["INSERTTYPE"] = "UPDATE";
                    toolRepairRow["VALIDSTATE"] = "Valid";

                    //현재 화면의 상태에 따라 생성/수정으로 분기된다.
                    toolRepairRow["SENDSEQUENCE"] = "";
                    //수정출고의 첫번째 업체를 선정한다.
                    toolRepairRow["MAKEVENDOR"] = ((DataTable)grdInputToolUpdateSend.DataSource).Rows[0].GetString("VENDORID");
                    toolRepairRow["CREATOR"] = UserInfo.Current.Id;
                    toolRepairRow["_STATE_"] = "added";
                    //if (_currentStatus == "added")
                    //{
                    //    toolRepairRow["SENDSEQUENCE"] = "";
                    //    //수정출고의 첫번째 업체를 선정한다.
                    //    toolRepairRow["MAKEVENDOR"] = ((DataTable)grdInputToolUpdateSend.DataSource).Rows[0].GetString("VENDORID");
                    //    toolRepairRow["CREATOR"] = UserInfo.Current.Id;
                    //    toolRepairRow["_STATE_"] = "added";
                    //}
                    //else
                    //{
                    //    toolRepairRow["SENDSEQUENCE"] = repairRow.GetString("SENDSEQUENCE");
                    //    toolRepairRow["MAKEVENDOR"] = repairRow.GetString("VENDORID");
                    //    toolRepairRow["MODIFIER"] = UserInfo.Current.Id;
                    //    toolRepairRow["_STATE_"] = "modified";
                    //}

                    toolRepairTable.Rows.Add(toolRepairRow);

                    toolRepairSet.Tables.Add(toolRepairTable);

                    //제작구분에 따라 제작탭의 그리드, 수정탭의 그리드를 각각 따로 입력이 된다.
                    
                    //제작일 경우
                    if (grdInputToolUpdateSend.View.RowCount > 0)
                    {
                        DataTable repairSendLotTable = CreateSaveDatatable(true);
                        DataTable changedRepairSendLotTable = grdInputToolUpdateSend.View.GetCheckedRows();

                        foreach (DataRow currentRow in changedRepairSendLotTable.Rows)
                        {
                            if (currentRow.GetString("ISSENDED").Equals("N"))
                            {
                                DataRow repairSendLotRow = repairSendLotTable.NewRow();

                                repairSendLotRow["TOOLNUMBER"] = currentRow.GetString("TOOLNUMBER");
                                repairSendLotRow["SENDDATE"] = sendDate.ToString("yyyy-MM-dd HH:mm:ss");

                                repairSendLotRow["FINISHDATE"] = "";
                                repairSendLotRow["FINISHER"] = "";
                                //repairSendLotRow["WEIGHT"] = currentRow.GetString("WEIGHT");
                                //repairSendLotRow["HEIGHT"] = currentRow.GetString("HEIGHT");
                                //repairSendLotRow["LENGTH"] = currentRow.GetString("LENGTH");
                                //repairSendLotRow["THICKNESS"] = currentRow.GetString("THICKNESS");
                                repairSendLotRow["AREAID"] = currentRow.GetString("RECEIPTAREAID");
                                repairSendLotRow["RECEIPTDATE"] = "";
                                repairSendLotRow["RECEIPTSEQUENCE"] = "";
                                repairSendLotRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                                repairSendLotRow["PLANTID"] = Conditions.GetValue("P_PLANTID");
                                repairSendLotRow["LOTHISTKEY"] = "";
                                repairSendLotRow["DESCRIPTION"] = "";

                                //DB변경에 따른 데이터 수정
                                repairSendLotRow["TOOLREPAIRTYPE"] = "Repair";
                                repairSendLotRow["MAKEVENDOR"] = currentRow.GetString("VENDORID");
                                repairSendLotRow["SENDOR"] = UserInfo.Current.Id;

                                repairSendLotRow["REQUESTDATE"] = currentRow.GetString("REQUESTDATE");
                                repairSendLotRow["REQUESTSEQUENCE"] = currentRow.GetString("REQUESTSEQUENCE");
                                repairSendLotRow["TOOLCODE"] = currentRow.GetString("TOOLCODE");
                                repairSendLotRow["DURABLEDEFVERSION"] = currentRow.GetString("TOOLVERSION");
                                //내역등록입력종료

                                repairSendLotRow["CREATOR"] = UserInfo.Current.Id;
                                repairSendLotRow["MODIFIER"] = UserInfo.Current.Id;

                                //수정출고과 수리출고를 구분하기 위한 구분자 입력
                                repairSendLotRow["INSERTTYPE"] = "UPDATE";

                                repairSendLotRow["SENDSEQUENCE"] = "";
                                repairSendLotRow["_STATE_"] = "added";
                                repairSendLotRow["VALIDSTATE"] = "Valid";

                                //if (currentRow["_STATE_"].ToString() == "added")
                                //{
                                //    repairSendLotRow["SENDSEQUENCE"] = "";
                                //    repairSendLotRow["_STATE_"] = "added";
                                //    repairSendLotRow["VALIDSTATE"] = "Valid";
                                //}
                                //else if (currentRow["_STATE_"].ToString() == "modified")
                                //{
                                //    repairSendLotRow["SENDSEQUENCE"] = repairRow.GetString("SENDSEQUENCE");
                                //    repairSendLotRow["_STATE_"] = "modified";
                                //    repairSendLotRow["VALIDSTATE"] = "Valid";
                                //}
                                //else if (currentRow["_STATE_"].ToString() == "deleted")
                                //{
                                //    repairSendLotRow["SENDSEQUENCE"] = repairRow.GetString("SENDSEQUENCE");
                                //    repairSendLotRow["_STATE_"] = "deleted";
                                //    repairSendLotRow["VALIDSTATE"] = "InValid";
                                //}

                                repairSendLotTable.Rows.Add(repairSendLotRow);
                            }
                        }

                        toolRepairSet.Tables.Add(repairSendLotTable);
                    }
                    DataTable resultTable = this.ExecuteRule<DataTable>("ToolRepairSend", toolRepairSet);

                    ////Server에서 결과값을 현재는 반환하지 않음 만약 반환해야 한다면 아래로직 구현
                    DataRow resultRow = resultTable.Rows[0];

                    ControlEnableProcess("modified");

                    ////가져온 결과값에 따라 재검색한 후 가져온 아이디에 맞는 행을 선택으로 변경한다.
                    Research(resultRow.GetString("SENDSEQUENCE"), resultRow.GetString("SENDDATE"));
                }
                else
                {
                    ShowMessage(MessageBoxButtons.OK, messageCode, "");
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

        #region CancelData : 출고취소를 진행
        private void CancelData()
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
                if (ValidateCancelContent(out messageCode))
                {
                    DataSet toolRepairSet = new DataSet();
                    //치공구 제작의뢰를 입력
                    DataTable toolRepairTable = CreateSaveParentDatatable(true);

                    DataRow toolRepairRow = toolRepairTable.NewRow();

                    DateTime sendDate = DateTime.Now;

                    DataRow repairRow = grdToolUpdateSend.View.GetFocusedDataRow();

                    toolRepairRow["SENDDATE"] = sendDate.ToString("yyyy-MM-dd HH:mm:ss");
                    toolRepairRow["SENDOR"] = UserInfo.Current.Id;
                    toolRepairRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                    toolRepairRow["PLANTID"] = Conditions.GetValue("P_PLANTID");
                    toolRepairRow["TOOLREPAIRTYPE"] = "Repair";
                    //수정출고인지 수리출고 인지 구분
                    toolRepairRow["INSERTTYPE"] = "UPDATE";
                    toolRepairRow["VALIDSTATE"] = "Invalid";

                    //현재 화면의 상태에 따라 생성/수정으로 분기된다.
                    toolRepairRow["SENDSEQUENCE"] = repairRow.GetString("SENDSEQUENCE");
                    //수정출고의 첫번째 업체를 선정한다.
                    //toolRepairRow["MAKEVENDOR"] = ((DataTable)grdInputToolUpdateSend.DataSource).Rows[0].GetString("VENDORID");
                    toolRepairRow["CREATOR"] = UserInfo.Current.Id;
                    toolRepairRow["_STATE_"] = "deleted";


                    toolRepairTable.Rows.Add(toolRepairRow);

                    toolRepairSet.Tables.Add(toolRepairTable);

                    //제작구분에 따라 제작탭의 그리드, 수정탭의 그리드를 각각 따로 입력이 된다.

                    //제작일 경우
                    if (grdInputToolUpdateSend.View.RowCount > 0)
                    {
                        DataTable repairSendLotTable = CreateSaveDatatable(true);
                        DataTable changedRepairSendLotTable = grdInputToolUpdateSend.View.GetCheckedRows();

                        foreach (DataRow currentRow in changedRepairSendLotTable.Rows)
                        {
                            if (currentRow.GetString("ISSENDED").Equals("Y"))
                            {
                                DataRow repairSendLotRow = repairSendLotTable.NewRow();

                                repairSendLotRow["TOOLNUMBER"] = currentRow.GetString("TOOLNUMBER");
                                repairSendLotRow["SENDDATE"] = Convert.ToDateTime(currentRow.GetString("REPAIRSENDDATE")).ToString("yyyy-MM-dd HH:mm:ss");

                                repairSendLotRow["FINISHDATE"] = "";
                                repairSendLotRow["FINISHER"] = "";

                                repairSendLotRow["AREAID"] = currentRow.GetString("RECEIPTAREAID");
                                repairSendLotRow["RECEIPTDATE"] = "";
                                repairSendLotRow["RECEIPTSEQUENCE"] = "";
                                repairSendLotRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                                repairSendLotRow["PLANTID"] = Conditions.GetValue("P_PLANTID");
                                repairSendLotRow["LOTHISTKEY"] = "";
                                repairSendLotRow["DESCRIPTION"] = "";

                                //DB변경에 따른 데이터 수정
                                repairSendLotRow["TOOLREPAIRTYPE"] = "Repair";
                                repairSendLotRow["MAKEVENDOR"] = currentRow.GetString("VENDORID");
                                repairSendLotRow["SENDOR"] = UserInfo.Current.Id;

                                repairSendLotRow["REQUESTDATE"] = currentRow.GetString("REQUESTDATE");
                                repairSendLotRow["REQUESTSEQUENCE"] = currentRow.GetString("REQUESTSEQUENCE");
                                repairSendLotRow["TOOLCODE"] = currentRow.GetString("TOOLCODE");
                                repairSendLotRow["DURABLEDEFVERSION"] = currentRow.GetString("TOOLVERSION");
                                //내역등록입력종료

                                repairSendLotRow["CREATOR"] = UserInfo.Current.Id;
                                repairSendLotRow["MODIFIER"] = UserInfo.Current.Id;

                                //수정출고과 수리출고를 구분하기 위한 구분자 입력
                                repairSendLotRow["INSERTTYPE"] = "UPDATE";

                                repairSendLotRow["SENDSEQUENCE"] = currentRow.GetString("REPAIRSENDSEQUENCE");
                                repairSendLotRow["_STATE_"] = "deleted";
                                repairSendLotRow["VALIDSTATE"] = "Invalid";

                                repairSendLotTable.Rows.Add(repairSendLotRow);
                            }
                        }

                        toolRepairSet.Tables.Add(repairSendLotTable);
                    }
                    this.ExecuteRule<DataTable>("ToolRepairSend", toolRepairSet);

                    ControlEnableProcess("modified");

                    ////가져온 결과값에 따라 재검색한 후 가져온 아이디에 맞는 행을 선택으로 변경한다.
                    Research(null, null);
                }
                else
                {
                    ShowMessage(MessageBoxButtons.OK, messageCode, "");
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

        #region DeleteLotGridRow : 삭제버튼을 통해 선택한 행을 삭제한다.
        private void DeleteLotGridRow()
        {
            grdInputToolUpdateSend.View.DeleteCheckedRows();
        }
        #endregion

        #region IsCurrentProcess : 현재 화면 상태에 따라 버튼 동작유무를 결정한다.
        /// <summary>
        /// 현재 화면 상태에 따라 버튼 동작유무를 결정한다.
        /// </summary>
        /// <returns></returns>
        private bool IsCurrentProcess()
        {
            //if (_currentStatus == "added")
            //    return true;
            //return false;
            return true;
        }
        #endregion

        #region GetSingleMakeVendor : 사용자가 입력한 제작업체명으로 검색
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
                grdInputToolUpdateSend.View.SetRowCellValue(rowHandle, "VENDORID", savedResult.Rows[0].GetString("VENDORID"));
                grdInputToolUpdateSend.View.SetRowCellValue(rowHandle, "MAKEVENDOR", savedResult.Rows[0].GetString("VENDORNAME"));
            }
            else
            {
                grdInputToolUpdateSend.View.SetRowCellValue(rowHandle, "MAKEVENDOR", "");
                grdInputToolUpdateSend.View.SetRowCellValue(rowHandle, "VENDORID", "");

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
                grdInputToolUpdateSend.View.SetRowCellValue(rowHandle, "RECEIPTAREAID", savedResult.Rows[0].GetString("AREAID"));
                grdInputToolUpdateSend.View.SetRowCellValue(rowHandle, "RECEIPTAREA", savedResult.Rows[0].GetString("AREANAME"));
            }
            else
            {
                grdInputToolUpdateSend.View.SetRowCellValue(rowHandle, "RECEIPTAREAID", "");
                grdInputToolUpdateSend.View.SetRowCellValue(rowHandle, "RECEIPTAREA", "");

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
                popupGridToolVendor.SearchQuery = new SqlQuery("GetVendorListByTool", "10001", $"LANGUAGETYPE={ UserInfo.Current.LanguageType }", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}");

            if(areaCondition != null)
                areaCondition.SearchQuery = new SqlQuery("GetAreaListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}", $"CURRENTLOGINID={UserInfo.Current.Id}");

            if (vendorPopup != null)
                vendorPopup.SearchQuery = new SqlQuery("GetVendorListByTool", "10001", $"LANGUAGETYPE={ UserInfo.Current.LanguageType }", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}");

            if (productPopup != null)
                productPopup.SearchQuery = new SqlQuery("GetProductdefidPoplistByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}", $"PRODUCTDEFTYPE=Product");

            //작업장 검색조건 초기화
            ((SmartSelectPopupEdit)Conditions.GetControl("AREAID")).ClearValue(); //VENDORNAME
            ((SmartSelectPopupEdit)Conditions.GetControl("VENDORNAME")).ClearValue(); //VENDORNAME
        }
        #endregion

        #endregion
    }
}
