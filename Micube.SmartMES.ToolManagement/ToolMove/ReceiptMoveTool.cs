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
    /// 프 로 그 램 명  : 치공구 이동 입고
    /// 업  무  설  명  : 치공구관리 > 치공구 이동관리 > 치공구 이동입고
    /// 생    성    자  : 김용조
    /// 생    성    일  : 2019-08-06
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ReceiptMoveTool : SmartConditionManualBaseForm
    {
        #region Local Variables
        string _currentStatus;
        string _searchReceiptAreaID;
        string _searchReceiptUserID;
        string _searchSendAreaID;
        string _isModify = "";
        bool _inSelectorColumnHeader;

        ConditionItemComboBox conditionFactoryBox;
        ConditionItemSelectPopup popupGridToolArea;
        ConditionItemSelectPopup areaCondition;
        ConditionItemSelectPopup receiptAreaCondition;
        ConditionItemSelectPopup sendAreaCondition;
        ConditionItemSelectPopup receiptUserCondition;
        ConditionItemSelectPopup productPopup;
        #endregion

        #region 생성자

        public ReceiptMoveTool()
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

        #region InitializeGrid - 이동입고목록을 초기화한다.
        /// <summary>        
        /// 치공구내역목록을 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdToolMoveList.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Export | GridButtonItem.Restore;  // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기            
            grdToolMoveList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdToolMoveList.View.AddTextBoxColumn("TOOLNUMBER", 150)                        //Tool번호
                .SetIsReadOnly(true);
            grdToolMoveList.View.AddTextBoxColumn("TOOLNAME", 350)                          //치공구명
                .SetIsReadOnly(true);
            grdToolMoveList.View.AddTextBoxColumn("PRODUCTDEFID", 120)
                .SetIsReadOnly(true);                                                       //품목코드
            grdToolMoveList.View.AddTextBoxColumn("PRODUCTDEFNAME", 300)
                .SetIsReadOnly(true);                                                       //품목명
            grdToolMoveList.View.AddTextBoxColumn("RECEIPTDATE", 150)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly(true);                                                       //입고일자
            grdToolMoveList.View.AddTextBoxColumn("RECEIPTUSERID")
                .SetIsHidden();                                                             //입고자아이디
            grdToolMoveList.View.AddTextBoxColumn("RECEIPTUSER", 100)
                .SetIsReadOnly(true);                                                       //입고자
            grdToolMoveList.View.AddTextBoxColumn("RECEIPTPLANTID")
                .SetIsHidden();                                                             //입고사이트아이디
            grdToolMoveList.View.AddTextBoxColumn("RECEIPTPLANT", 80)
                .SetIsReadOnly(true);                                                       //입고사이트
            grdToolMoveList.View.AddTextBoxColumn("RECEIPTAREAID")
                .SetIsHidden();                                                             //입고작업장아이디

            InitializeAreaPopupColumnInDetailGrid();
            //grdToolMoveList.View.AddTextBoxColumn("RECEIPTAREA", 120)
            //    .SetIsReadOnly(true);                                                     //입고작업장
            grdToolMoveList.View.AddTextBoxColumn("TOOLMOVETYPEID")
                .SetIsHidden();                                                             //이동구분아이디
            grdToolMoveList.View.AddTextBoxColumn("TOOLMOVETYPE", 80)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(true);                                                       //이동구분
            grdToolMoveList.View.AddComboBoxColumn("TOOLFORMCODE", 150, new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CODECLASSID=ToolForm"), "CODENAME", "CODEID")
               .SetTextAlignment(TextAlignment.Center)
               .SetIsReadOnly(true);                                                  //상태 - (이 회면에서 입력해야 한다면 SetIsReadOnly를 풀어준다.
            grdToolMoveList.View.AddTextBoxColumn("SENDDATE", 150)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly(true);                                                       //출고일자
            grdToolMoveList.View.AddTextBoxColumn("SENDPLANTID")
                .SetIsHidden();                                                             //출고사이트아이디 
            grdToolMoveList.View.AddTextBoxColumn("SENDPLANT", 80)
                .SetIsReadOnly(true);                                                       //출고사이트
            grdToolMoveList.View.AddTextBoxColumn("SENDAREAID")
                .SetIsHidden();                                                             //출고작업장아이디
            grdToolMoveList.View.AddTextBoxColumn("SENDAREA", 180)
                .SetIsReadOnly(true);                                                       //출고작업장
            grdToolMoveList.View.AddTextBoxColumn("SENDUSERID")
                .SetIsHidden();                                                             //출고자아이디
            grdToolMoveList.View.AddTextBoxColumn("SENDUSER", 80)
                .SetIsReadOnly(true);                                                       //출고자
            
            grdToolMoveList.View.PopulateColumns();
        }
        #endregion

        #region InitializeAreaPopupColumnInDetailGrid - 작업장을 팝업검색할 필드 설정
        /// <summary>
        /// grid ProcesssegmentidLis
        /// </summary>
        private void InitializeAreaPopupColumnInDetailGrid()
        {
            popupGridToolArea = grdToolMoveList.View.AddSelectPopupColumn("RECEIPTAREA", 150, new SqlQuery("GetAreaListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"CURRENTLOGINID={UserInfo.Current.Id}"))
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
                .SetIsReadOnly(true)
                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {
                    foreach (DataRow row in selectedRows)
                    {
                        grdToolMoveList.View.GetFocusedDataRow()["RECEIPTAREAID"] = row["AREAID"];
                        grdToolMoveList.View.GetFocusedDataRow()["RECEIPTAREA"] = row["AREANAME"];
                    }
                })
            //.SetPopupValidationCustom(ValidationParentAreaIdPopup);
            ;
            // 팝업에서 사용할 조회조건 항목 추가
            conditionFactoryBox = popupGridToolArea.Conditions.AddComboBox("FACTORYID", new SqlQuery("GetFactoryListByEqp", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "FACTORYNAME", "FACTORYID")
                ;

            popupGridToolArea.Conditions.AddTextBox("AREANAME");

            // 팝업 그리드 설정
            popupGridToolArea.GridColumns.AddTextBoxColumn("AREAID", 300)
                .SetIsHidden();
            popupGridToolArea.GridColumns.AddTextBoxColumn("AREANAME", 300)
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
            // TODO : 화면에서 사용할 이벤트 추가
            grdToolMoveList.View.ShowingEditor += grdToolMoveList_ShowingEditor;
            grdToolMoveList.View.CheckStateChanged += grdToolMoveList_CheckStateChanged;
            grdToolMoveList.View.MouseUp += grdToolMoveList_MouseUp;
            btnSave.Click += BtnSave_Click;

            Shown += ReceiptMoveTool_Shown;
        }

        #region ReceiptMoveTool_Shown - ToolBar 및 Site관련 설정을 화면 로딩후에 일괄 적용
        private void ReceiptMoveTool_Shown(object sender, EventArgs e)
        {
            ChangeSiteCondition();

            //다중 Site 권한을 가진 사용자가 Site를 변경시 환경을 변경해줘야한다.
            ((SmartComboBox)Conditions.GetControl("P_PLANTID")).EditValueChanged += ConditionPlant_EditValueChanged;
            ((SmartSelectPopupEdit)Conditions.GetControl("TOAREAID")).EditValueChanged += ToArea_EditValueChanged; ;
        }
        #endregion

        #region ToArea_EditValueChanged - 입고작업장 변경시 이벤트
        private void ToArea_EditValueChanged(object sender, EventArgs e)
        {
            grdToolMoveList.View.ClearDatas();

            if(_isModify.Equals("Y"))
            {
                if (pnlToolbar.Controls["layoutToolbar"] != null)
                {
                    if (pnlToolbar.Controls["layoutToolbar"].Controls["Save"] != null)
                        pnlToolbar.Controls["layoutToolbar"].Controls["Save"].Enabled = true;
                }
            }
            else
            {
                if (pnlToolbar.Controls["layoutToolbar"] != null)
                {
                    if (pnlToolbar.Controls["layoutToolbar"].Controls["Save"] != null)
                        pnlToolbar.Controls["layoutToolbar"].Controls["Save"].Enabled = false;
                }

            }
        }
        #endregion

        #region ConditionPlant_EditValueChanged - 조회조건내의 Site변경시 이벤트
        private void ConditionPlant_EditValueChanged(object sender, EventArgs e)
        {
            ChangeSiteCondition();
        }
        #endregion

        #region grdToolMoveList_MouseUp - 그리드 전체 체크박스 관련 이벤트
        private void grdToolMoveList_MouseUp(object sender, MouseEventArgs e)
        {
            DevExpress.XtraGrid.Views.BandedGrid.ViewInfo.BandedGridHitInfo hi = grdToolMoveList.View.CalcHitInfo(e.Location);
            _inSelectorColumnHeader = false;
            if (hi.InColumn && !hi.InRow)
            {
                if (hi.Column.FieldName.Equals("_INTERNAL_CHECKMARK_SELECTION_"))
                {
                    grdToolMoveList.View.CheckStateChanged -= grdToolMoveList_CheckStateChanged;
                    _inSelectorColumnHeader = true;

                    for (int i = 0; i < grdToolMoveList.View.RowCount; i++)
                    {
                        grdToolMoveList.View.FocusedRowHandle = i;
                        grdToolMoveList_CheckStateChanged(sender, new EventArgs());
                    }

                    grdToolMoveList.View.CheckStateChanged += grdToolMoveList_CheckStateChanged;
                }
            }
        }
        #endregion

        #region grdToolMoveList_CheckStateChanged : 체크 선택시 입고일 자동지정
        private void grdToolMoveList_CheckStateChanged(object sender, EventArgs e)
        {
            //이미 입고된 데이터는 체크할 수 없다.
            if (grdToolMoveList.View.GetFocusedDataRow().GetString("ISUPDATE").Equals("N"))
            {
                DataRow dataRow = grdToolMoveList.View.GetFocusedDataRow();
                if ((Boolean)grdToolMoveList.View.GetFocusedRowCellValue("_INTERNAL_CHECKMARK_SELECTION_"))
                {
                    grdToolMoveList.View.SetFocusedRowCellValue("RECEIPTDATE", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    grdToolMoveList.View.SetFocusedRowCellValue("RECEIPTUSERID", UserInfo.Current.Id);
                    grdToolMoveList.View.SetFocusedRowCellValue("RECEIPTUSER", UserInfo.Current.Name);
                    lblGroupTitle.Focus();
                }
                else
                {
                    grdToolMoveList.View.SetFocusedRowCellValue("RECEIPTDATE", "");
                    grdToolMoveList.View.SetFocusedRowCellValue("RECEIPTUSERID", "");
                    grdToolMoveList.View.SetFocusedRowCellValue("RECEIPTUSER", "");
                    lblGroupTitle.Focus();
                }
            }
            else
            {
                grdToolMoveList.View.SetFocusedRowCellValue("_INTERNAL_CHECKMARK_SELECTION_", false);
            }
        }
        #endregion

        #region BtnSave_Click - 저장버튼 이벤트
        private void BtnSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }
        #endregion

        #region grdToolMoveList_ShowingEditor : 입고작업장 입력제어
        private void grdToolMoveList_ShowingEditor(object sender, CancelEventArgs e)
        {
            //이미 입고된 데이터는 수정할 수 없다.
            if (grdToolMoveList.View.GetFocusedDataRow().GetString("ISUPDATE").Equals("Y"))
            {
                e.Cancel = true;
            }
            else if (grdToolMoveList.View.FocusedColumn.Name == "RECEIPTAREA") //이동 구분이 "이동"이라면 입고작업장을 지정할 수 없다. 
            {
                if (grdToolMoveList.View.GetFocusedDataRow().GetString("TOOLMOVETYPEID").Equals("Move"))
                {
                    e.Cancel = true;
                }
            }
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
                BtnSave_Click(sender, e);
        }
        #endregion

        #region 검색

        #region OnSearchAsync
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
            values.Add("CURRENTLOGINID", UserInfo.Current.Id);

            //if (Conditions.GetValue("AREANAME").ToString() != "")
            //    values.Add("TOAREAID", _searchReceiptAreaID);

            if (Conditions.GetValue("SENDAREANAME").ToString() != "")
                values.Add("AREAID", _searchSendAreaID);

            if (Conditions.GetValue("USERNAME").ToString() != "")
                values.Add("RECEIPTUSER", _searchReceiptUserID);
            #endregion
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable toolMoveList = SqlExecuter.Query("GetMoveToolReceiptListByTool", "10001", values);

            grdToolMoveList.DataSource = toolMoveList;

            if (toolMoveList.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }
        }
        #endregion

        #region Research : 재검색
        private void Research()
        {
            InitializeInsertForm();
            // TODO : 조회 SP 변경
            var values = Conditions.GetValues();

            #region 기간 검색형 전환 처리 - 만약 시간까지 포함하여 검색한다면 아래 문구는 필요없음            
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("CURRENTLOGINID", UserInfo.Current.Id);

            //if (Conditions.GetValue("AREANAME").ToString() != "")
            //    values.Add("TOAREAID", _searchReceiptAreaID);

            if (Conditions.GetValue("SENDAREANAME").ToString() != "")
                values.Add("AREAID", _searchSendAreaID);

            if (Conditions.GetValue("USERNAME").ToString() != "")
                values.Add("RECEIPTUSER", _searchReceiptUserID);
            #endregion
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable toolMoveList = SqlExecuter.Query("GetMoveToolReceiptListByTool", "10001", values);

            grdToolMoveList.DataSource = toolMoveList;

            if (toolMoveList.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
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
            //InitializeConditionToPlant();
            InitializeConditionPlant();
            InitializeConditionToolMoveType();
            InitializeConditionIsReceipt();
            //InitializeConditionReceiptArea();
            //InitializeConditionReceiptAreaCombo();
            InitializeConditionAreaPopup();
            InitializeConditionSendArea();
            InitializeConditionReceiptUser();
            InitializeConditionProductCodePopup();
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
        }

        #region 입고사이트를 설정
        /// <summary>
        /// site 설정 
        /// </summary>
        private void InitializeConditionToPlant()
        {
            var planttxtbox = Conditions.AddComboBox("TOPLANTID", new SqlQuery("GetPlantList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PLANTID", "PLANTID")
               .SetLabel("RECEIPTPLANT")
               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
               .SetPosition(0.1)
               .SetIsReadOnly(true)
               .SetDefault(UserInfo.Current.Plant, "PLANTID") //기본값 설정 UserInfo.Current.Plant
               .SetValidationIsRequired()
            ;
        }
        #endregion

        #region 출고사이트를 설정
        /// <summary>
        /// site 설정 
        /// </summary>
        private void InitializeConditionPlant()
        {
            var planttxtbox = Conditions.AddComboBox("PLANTID", new SqlQuery("GetPlantList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PLANTID", "PLANTID")
               .SetLabel("SENDPLANT")
               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
               .SetPosition(1.2)
               .SetEmptyItem("", "", true)
            ;
        }
        #endregion

        #region InitializeCondition_IsReceipt : 입고여부를 설정
        /// <summary>
        /// 제작구분 설정
        /// </summary>
        private void InitializeConditionIsReceipt()
        {
            var planttxtbox = Conditions.AddComboBox("ISRECEIPT", new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CODECLASSID=YesNo"), "CODENAME", "CODEID")
               .SetLabel("ISRECEIPT")
               .SetEmptyItem("", "", true)
               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
               .SetPosition(3.1)
               .SetEmptyItem("", "", true)
               .SetDefault("N")
            ;
        }
        #endregion

        #region InitializeCondition_ToolMoveType : 이동구분을 설정
        /// <summary>
        /// 이동구분을 설정
        /// </summary>
        private void InitializeConditionToolMoveType()
        {
            var planttxtbox = Conditions.AddComboBox("TOOLMOVETYPE", new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CODECLASSID=ToolMoveType"), "CODENAME", "CODEID")
               .SetLabel("TOOLMOVETYPE")
               .SetEmptyItem("", "", true)
               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
               .SetPosition(0.3)
               .SetEmptyItem("", "", true)               
            ;
        }
        #endregion

        #region InitializeConditionReceiptAreaCombo : 입고작업장을 설정
        /// <summary>
        /// 작업장 설정 
        /// </summary>
        private void InitializeConditionReceiptAreaCombo()
        {
            var areaBox = Conditions.AddComboBox("TOAREAID", new SqlQuery("GetAreaListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "AREANAME", "AREAID")
               .SetLabel("AREA")
               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
               .SetPosition(0.2)
               .SetValidationIsRequired()
               .SetDefault(UserInfo.Current.Area, "AREAID") //기본값 설정 UserInfo.Current.Plant
               .SetRelationIds("P_PLANTID")
            ;
        }

        #region InitializeConditionAreaPopup : 작업장 검색조건
        /// <summary>
        /// 작업장 검색조건
        /// </summary>
        private void InitializeConditionAreaPopup()
        {
            areaCondition = Conditions.AddSelectPopup("TOAREAID", new SqlQuery("GetAreaListByTool", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CURRENTLOGINID={UserInfo.Current.Id}"))
            .SetPopupLayout("AREAID", PopupButtonStyles.Ok_Cancel, true, true)
            .SetPopupLayoutForm(600, 800, FormBorderStyle.FixedToolWindow)
            .SetPopupAutoFillColumns("AREAID")
            .SetLabel("AREA")
            .SetPopupResultCount(1)
            .SetPosition(0.2)
            .SetPopupResultMapping("AREAID", "AREAID")
            .SetRelationIds("P_PLANTID")
            .SetPopupApplySelection((selectedRows, dataGridRow) =>
            {
                foreach (DataRow row in selectedRows)
                {
                    _isModify = row["ISMODIFY"].ToString();
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
        /// 입고작업장을 설정
        /// </summary>
        private void InitializeConditionReceiptArea()
        {
            receiptAreaCondition = Conditions.AddSelectPopup("TOAREAID", new SqlQuery("GetAreaListByTool", "10001", $"LANGUAGETYPE={ UserInfo.Current.LanguageType }", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"CURRENTLOGINID={UserInfo.Current.Id}"))
            .SetPopupLayout("RECEIPTAREA", PopupButtonStyles.Ok_Cancel, true, false)
            .SetPopupLayoutForm(500, 600, FormBorderStyle.FixedToolWindow)
            .SetPopupResultMapping("AREAID", "AREAID")
            .SetLabel("RECEIPTAREA")
            .SetPopupResultCount(1)
            .SetPosition(0.2)
            .SetValidationIsRequired()
            .SetPopupApplySelection((selectedRows, dataGridRow) =>
            {
                foreach (DataRow row in selectedRows)
                {
                    _isModify = row["ISMODIFY"].ToString();
                }
            })
            ;

            receiptAreaCondition.ValueFieldName = "AREAID";
            receiptAreaCondition.DisplayFieldName = "AREANAME";
            receiptAreaCondition.SetDefault(UserInfo.Current.Area, "AREAID");

            // 팝업에서 사용할 조회조건 항목 추가
            receiptAreaCondition.Conditions.AddTextBox("AREANAME");

            // 팝업 그리드 설정
            receiptAreaCondition.GridColumns.AddTextBoxColumn("AREAID", 300)
                .SetIsHidden();
            receiptAreaCondition.GridColumns.AddTextBoxColumn("AREANAME", 300)
                .SetIsReadOnly();
        }
        #endregion

        #region InitializeCondition_SendArea : 출고작업장을 설정
        /// <summary>
        /// 출고작업장을 설정
        /// </summary>
        private void InitializeConditionSendArea()
        {
            sendAreaCondition = Conditions.AddSelectPopup("SENDAREANAME", new SqlQuery("GetSendAreaListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"CURRENTLOGINID={UserInfo.Current.Id}"))
            .SetPopupLayout("SENDAREA", PopupButtonStyles.Ok_Cancel, true, false)
            .SetPopupLayoutForm(500, 600, FormBorderStyle.FixedToolWindow)
            .SetPopupResultMapping("SENDAREANAME", "SENDAREANAME")
            .SetLabel("SENDAREA")
            .SetPopupResultCount(1)
            .SetPosition(1.3)
            .SetRelationIds("PLANTID")
            .SetPopupApplySelection((selectedRows, dataGridRow) =>
            {
                int i = 0;

                foreach (DataRow row in selectedRows)
                {
                    if (i == 0)
                    {
                        _searchSendAreaID = row["SENDAREAID"].ToString();
                    }
                    i++;
                }
            })
            ;

            // 팝업에서 사용할 조회조건 항목 추가
            sendAreaCondition.Conditions.AddTextBox("AREANAME");

            // 팝업 그리드 설정
            sendAreaCondition.GridColumns.AddTextBoxColumn("SENDAREAID", 300)
                .SetIsHidden();
            sendAreaCondition.GridColumns.AddTextBoxColumn("SENDAREANAME", 300)
                .SetIsReadOnly();
        }
        #endregion

        #region InitializeConditionReceiptUser : 입고자를 설정
        /// <summary>
        /// 입고자를 설정
        /// </summary>
        private void InitializeConditionReceiptUser()
        {
            receiptUserCondition = Conditions.AddSelectPopup("USERNAME", new SqlQuery("GetUserListByTool", "10001", $"LANGUAGETYPE={ UserInfo.Current.LanguageType }", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"CURRENTLOGINID={UserInfo.Current.Id}"))
            .SetPopupLayout("RECEIPTUSER", PopupButtonStyles.Ok_Cancel, true, false)
            .SetPopupLayoutForm(500, 600, FormBorderStyle.FixedToolWindow)
            .SetPopupResultMapping("USERNAME", "USERNAME")
            .SetLabel("RECEIPTUSER")
            .SetPopupResultCount(1)
            .SetPosition(1.1)
            .SetPopupApplySelection((selectedRows, dataGridRow) =>
            {
                int i = 0;

                foreach (DataRow row in selectedRows)
                {
                    if (i == 0)
                    {
                        _searchReceiptUserID = row["USERID"].ToString();
                    }
                    i++;
                }
            })
            ;

            // 팝업에서 사용할 조회조건 항목 추가
            receiptUserCondition.Conditions.AddTextBox("USERNAME");

            // 팝업 그리드 설정
            receiptUserCondition.GridColumns.AddTextBoxColumn("USERID", 300)
                .SetIsReadOnly();
            receiptUserCondition.GridColumns.AddTextBoxColumn("USERNAME", 300)
                .SetIsReadOnly();
        }
        #endregion

        #region InitializeConditionProductCodePopup : 품목코드를 설정
        /// <summary>
        /// 품목코드 조회 설정
        /// </summary>
        private void InitializeConditionProductCodePopup()
        {
            ConditionItemSelectPopup productPopup = Conditions.AddSelectPopup("PRODUCTDEFID", new SqlQuery("GetProductdefidPoplistByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"PRODUCTDEFTYPE=Product"))
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
        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();
        }

        #region ValidateContent - Validation수행
        private bool ValidateContent()
        {
            //각각의 상태에 따라 보여줄 메세지가 상이하다면 각각의 분기에서 메세지를 표현해야 한다.    
            return true;
        }
        #endregion

        #region ValidateCellInGrid - 그리드내의 특정컬럼에 대한 Validation을 수행
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

        #region ValidateComboBoxEqualValue - 2개의 콤보박스에 대한 값 비교를 수행
        private bool ValidateComboBoxEqualValue(SmartComboBox originBox, SmartComboBox targetBox)
        {
            //두 콤보박스의 값이 같으면 안된다.
            if (originBox.EditValue.Equals(targetBox.EditValue))
                return false;
            return true;
        }
        #endregion

        #region ValidateNumericBox - 숫자형 텍스트박스의 값 점검 및 기준숫자보다 작은지 점검
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

        #region ValidateEditValue - 입력받은 값의 Validate
        private bool ValidateEditValue(object editValue)
        {
            if (editValue == null)
                return false;

            if (editValue.ToString() == "")
                return false;

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
                //의뢰일은 오늘날짜를 기본으로 한다.
                _currentStatus = "added";

                //컨트롤 접근여부는 작성상태로 변경한다.
                ControlEnableProcess("added");
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
            dt.TableName = "toolMoveList";
            //===================================================================================
            //Key값 ToolRequest 및 ToolRequestDetail
            dt.Columns.Add("TOOLNUMBER");
            dt.Columns.Add("AREAID");

            dt.Columns.Add("RECEIPTDATE");
            dt.Columns.Add("RECEIPTUSERID");
            dt.Columns.Add("TOAREAID");
            dt.Columns.Add("TOPLANTID");
            dt.Columns.Add("LOTHISTKEY");
            dt.Columns.Add("SENDDATE");

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
                btnSave.Enabled = false;

                if (grdToolMoveList.View.GetCheckedRows().Rows.Count > 0)
                {
                    //Validation Check시 입력/수정시 검사할 값이 다르다면 수정필요
                    if (ValidateContent())
                    {
                        DataSet toolMoveSet = new DataSet();
                        //치공구 제작의뢰를 입력
                        DataTable toolMoveTable = CreateSaveDatatable();

                        DataTable inputTable = grdToolMoveList.View.GetCheckedRows();

                        foreach (DataRow inputRow in inputTable.Rows)
                        {
                            DataRow moveRow = toolMoveTable.NewRow();

                            moveRow["TOOLNUMBER"] = inputRow.GetString("TOOLNUMBER");
                            moveRow["SENDDATE"] = Convert.ToDateTime(inputRow.GetString("SENDDATE")).ToString("yyyy-MM-dd HH:mm:ss");
                            moveRow["AREAID"] = inputRow.GetString("SENDAREAID");

                            DateTime tempTime = Convert.ToDateTime(inputRow.GetString("RECEIPTDATE"));
                            moveRow["RECEIPTDATE"] = tempTime.ToString("yyyy-MM-dd HH:mm:ss");
                            
                            moveRow["RECEIPTUSERID"] = inputRow.GetString("RECEIPTUSERID");
                            moveRow["TOAREAID"] = inputRow.GetString("RECEIPTAREAID");
                            moveRow["TOPLANTID"] = inputRow.GetString("RECEIPTPLANTID");
                            moveRow["LOTHISTKEY"] = "";

                            moveRow["CREATOR"] = UserInfo.Current.Id;
                            moveRow["MODIFIER"] = UserInfo.Current.Id;

                            moveRow["VALIDSTATE"] = "Valid";
                            moveRow["_STATE_"] = "modified";


                            toolMoveTable.Rows.Add(moveRow);
                        }
                        toolMoveSet.Tables.Add(toolMoveTable);


                        this.ExecuteRule<DataTable>("ToolMove", toolMoveSet);

                        ControlEnableProcess("modified");

                        Research();

                    }
                    else
                    {
                        this.ShowMessage(MessageBoxButtons.OK, "ToolMakeReceiptValidation", "");
                    }
                }
                else
                {
                    //ShowMessage(MessageBoxButtons.OK, String.Format("SelectItem", Language.GetDictionary("DURABLE")), "");
                    ShowMessage(MessageBoxButtons.OK, "SelectItem", Language.Get("DURABLE"));
                }
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
            finally
            {
                this.CloseWaitArea();
                btnSave.Enabled = true;
            }
        }
        #endregion

        #region ChangeSiteCondition - 검색조건의 Site변경시 관련된 쿼리들을 변경
        private void ChangeSiteCondition()
        {
            //if (productPopup != null)
            //    productPopup.SearchQuery = new SqlQuery("GetProductdefidPoplistByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}", $"PRODUCTDEFTYPE=Product");


            if (popupGridToolArea != null)
                popupGridToolArea.SearchQuery = new SqlQuery("GetAreaIDListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}", $"CURRENTLOGINID={UserInfo.Current.Id}");

            if (areaCondition != null)
                areaCondition.SearchQuery = new SqlQuery("GetAreaIDListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}", $"CURRENTLOGINID={UserInfo.Current.Id}");

            if (receiptAreaCondition != null)
                receiptAreaCondition.SearchQuery = new SqlQuery("GetAreaIDListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}", $"CURRENTLOGINID={UserInfo.Current.Id}");

            //if (sendAreaCondition != null)
            //    sendAreaCondition.SearchQuery = new SqlQuery("GetAreaIDListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}");

            if (conditionFactoryBox != null)
                conditionFactoryBox.Query = new SqlQuery("GetFactoryListByEqp", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}");

            //if (receiptUserCondition != null)
            //    receiptUserCondition.SearchQuery = new SqlQuery("GetUserListByTool", "10001", $"LANGUAGETYPE={ UserInfo.Current.LanguageType }", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}");

            //if (popupGridApprovalUser != null)
            //    popupGridApprovalUser.SearchQuery = new SqlQuery("GetUserListForPopupByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}");

                //if (ucProductCode != null)
                //    ucProductCode.PlantID = Conditions.GetValue("P_PLANTID").ToString();

                ////화면초기화
                //InitializeInsertForm();
        }
        #endregion

        #endregion
    }
}
