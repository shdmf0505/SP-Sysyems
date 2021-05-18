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
    /// 프 로 그 램 명  : 치공구관리 > 필름관리 > 필름인수등록
    /// 업  무  설  명  : 발행 및 출고등록된 필름에 대하여 인수등록을 진행한다.
    /// 생    성    자  : 김용조
    /// 생    성    일  : 2019-08-21
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class RegistReceiveFilm : SmartConditionManualBaseForm
    {
        #region Local Variables
        private string _searchAreaID = "";
        private string _searchVendorID = "";

        ConditionItemSelectPopup popupGridToolArea;
        ConditionItemComboBox factoryPopup;
        ConditionItemSelectPopup areaCondition;
        ConditionItemSelectPopup receiptAreaCondition;
        ConditionItemSelectPopup vendorCondition;
        ConditionItemSelectPopup filmCodeCondition;
        ConditionItemComboBox segmentCondition;
        #endregion

        #region 생성자

        public RegistReceiveFilm()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화
        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeEvent();

            InitializeGrid();
            InitializeLotGrid();

            ControlEnableProcess("");
        }

        #region InitializeGrid - 필름관리목록을 초기화한다.
        /// <summary>        
        /// 필름관리목록을 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            grdFilmRequest.GridButtonItem = GridButtonItem.Export;
            grdFilmRequest.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdFilmRequest.View.AddTextBoxColumn("FILMSEQUENCE").SetIsHidden();                             //필름시퀀스
            grdFilmRequest.View.AddTextBoxColumn("FILMPROGRESSSTATUSID").SetIsHidden();                     //진행상태아이디
            grdFilmRequest.View.AddTextBoxColumn("RELEASEUSERID").SetIsHidden();                            //출고자아이디
            grdFilmRequest.View.AddTextBoxColumn("RELEASEAREAID").SetIsHidden();                            //입고작업장아이디
            grdFilmRequest.View.AddTextBoxColumn("RECEIVEAREAID").SetIsHidden();                            //입고작업장아이디
            grdFilmRequest.View.AddTextBoxColumn("ISMODIFY").SetIsHidden();                                 //입고작업장아이디
            grdFilmRequest.View.AddTextBoxColumn("JOBTYPEID").SetIsHidden();                                //작업구분아이디
            grdFilmRequest.View.AddTextBoxColumn("PRODUCTIONTYPEID").SetIsHidden();                         //생산구분아이디
            grdFilmRequest.View.AddTextBoxColumn("FILMTYPEID").SetIsHidden();                               //필름구분아이디
            grdFilmRequest.View.AddTextBoxColumn("FILMCATEGORYID").SetIsHidden();                           //필름유형아이디
            grdFilmRequest.View.AddTextBoxColumn("FILMDETAILCATEGORYID").SetIsHidden();                     //상세유형아이디
            grdFilmRequest.View.AddTextBoxColumn("CUSTOMERID").SetIsHidden();                               //작업구분아이디
            grdFilmRequest.View.AddTextBoxColumn("USEPROCESSSEGMENT", 80).SetIsHidden();                    //사용공정
            grdFilmRequest.View.AddTextBoxColumn("PRIORITYID").SetIsHidden();                               //우선순위아이디
            grdFilmRequest.View.AddTextBoxColumn("MAKEVENDORID").SetIsHidden();                             //제작업체아이디
            grdFilmRequest.View.AddTextBoxColumn("MAKEVENDOR", 150).SetIsHidden();                          //제작업체
            grdFilmRequest.View.AddTextBoxColumn("REQUESTUSERID").SetIsHidden();                            //요청자아이디
            grdFilmRequest.View.AddTextBoxColumn("ACCEPTUSERID").SetIsHidden();                             //접수자아이디
            grdFilmRequest.View.AddTextBoxColumn("RECEIVEUSERID").SetIsHidden();                            //인수자아이디
            grdFilmRequest.View.AddTextBoxColumn("USEPLANDATE", 100).SetIsHidden();                         //사용예정일            
            grdFilmRequest.View.AddTextBoxColumn("CHECKEDOPTION").SetIsHidden();                            //체크옵션
            grdFilmRequest.View.AddTextBoxColumn("FILMUSELAYER2", 120).SetIsHidden(); //Layer2
            grdFilmRequest.View.AddTextBoxColumn("CUSTOMER", 120).SetIsHidden();  //고객

            grdFilmRequest.View.AddTextBoxColumn("FILMPROGRESSSTATUS", 80).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();  //진행상태  
            grdFilmRequest.View.AddTextBoxColumn("REQUESTDATE", 160).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly();   //의뢰일  
            grdFilmRequest.View.AddTextBoxColumn("RELEASEDATE", 160).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly();   //출고일시
            grdFilmRequest.View.AddTextBoxColumn("REQUESTDEPARTMENT", 100).SetIsReadOnly(); //의뢰부서ID
            grdFilmRequest.View.AddTextBoxColumn("REQUESTUSER", 80).SetIsReadOnly();    //요청자
            grdFilmRequest.View.AddTextBoxColumn("PRODUCTDEFID", 120).SetIsReadOnly();  //품목코드
            grdFilmRequest.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetIsReadOnly();  //내부 Rev
            grdFilmRequest.View.AddTextBoxColumn("PRODUCTDEFNAME", 250).SetIsReadOnly();    //품목명
            grdFilmRequest.View.AddTextBoxColumn("FILMVERSION", 120).SetIsReadOnly();   //CAM Rev.
            grdFilmRequest.View.AddTextBoxColumn("FILMNAME", 400).SetIsReadOnly();  //필름명
            grdFilmRequest.View.AddTextBoxColumn("CONTRACTIONX", 120).SetIsReadOnly();  //수축률X
            grdFilmRequest.View.AddTextBoxColumn("CONTRACTIONY", 120).SetIsReadOnly();  //수축률Y
            grdFilmRequest.View.AddTextBoxColumn("FILMCATEGORY", 120).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();   //필름유형1
            grdFilmRequest.View.AddTextBoxColumn("FILMDETAILCATEGORY", 150).SetTextAlignment(TextAlignment.Center).SetIsReadOnly(); //필름유형2
            grdFilmRequest.View.AddTextBoxColumn("FILMUSELAYER1", 100).SetIsReadOnly(); //Layer1
            grdFilmRequest.View.AddTextBoxColumn("QTY", 80).SetIsReadOnly();    //수량 
            grdFilmRequest.View.AddTextBoxColumn("REQUESTCOMMENT", 200).SetIsReadOnly();    //의뢰사유
            grdFilmRequest.View.AddTextBoxColumn("RELEASEAREA", 180).SetIsReadOnly();   //출고작업장
            InitializeAreaPopupColumnInDetailGrid();    //인수작업장
            grdFilmRequest.View.AddTextBoxColumn("JOBTYPE", 80).SetTextAlignment(TextAlignment.Center).SetIsReadOnly(); //작업구분
            grdFilmRequest.View.AddTextBoxColumn("PRODUCTIONTYPE", 80).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();  //생산구분
            grdFilmRequest.View.AddTextBoxColumn("FILMTYPE", 80).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();    //필름구분
            grdFilmRequest.View.AddTextBoxColumn("FILMCODE", 150).SetIsReadOnly();  //필름코드
            grdFilmRequest.View.AddTextBoxColumn("CHANGECONTRACTIONX", 120).SetIsReadOnly();    //요청수축률X(%)
            grdFilmRequest.View.AddTextBoxColumn("CHANGECONTRACTIONY", 120).SetIsReadOnly();    //요청수축률Y(%)
            grdFilmRequest.View.AddSpinEditColumn("MEASURECONTRACTIONX", 120).SetDisplayFormat("#,###.#####").SetIsReadOnly();  //실측수축률X
            grdFilmRequest.View.AddSpinEditColumn("MEASURECONTRACTIONY", 120).SetDisplayFormat("#,###.#####").SetIsReadOnly();  //실측수축률Y          
            grdFilmRequest.View.AddTextBoxColumn("RESOLUTION", 80).SetIsReadOnly(); //해상도
            grdFilmRequest.View.AddTextBoxColumn("ISCOATING", 80).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();   //코팅유무      
            grdFilmRequest.View.AddTextBoxColumn("PRIORITY", 80).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();    //우선순위
            grdFilmRequest.View.AddTextBoxColumn("ACCEPTDEPARTMENT", 100).SetIsReadOnly();  //접수부서
            grdFilmRequest.View.AddTextBoxColumn("ACCEPTUSER", 100).SetIsReadOnly().SetLabel("ACCEPTMAINTWORKORDERUSER");   //접수자
            grdFilmRequest.View.AddTextBoxColumn("ACCEPTDATE", 160).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly().SetLabel("RECEPTDATE");    //접수일
            grdFilmRequest.View.AddTextBoxColumn("RELEASEDEPARTMENT").SetIsReadOnly();  //출고부서
            grdFilmRequest.View.AddTextBoxColumn("RELEASEUSER", 100).SetIsReadOnly();   //출고자
            grdFilmRequest.View.AddTextBoxColumn("RECEIVEDATE", 160).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly();   //인수일
            grdFilmRequest.View.AddTextBoxColumn("RECEIVEUSER", 100).SetIsReadOnly();   //인수자
            grdFilmRequest.View.AddTextBoxColumn("RECEIVEDEPARTMENT", 100).SetIsReadOnly(); //인수부서

            grdFilmRequest.View.PopulateColumns();
        }
        #endregion

        #region InitializeReceiveAreaPopupColumnInDetailGrid - 작업장을 팝업검색할 필드 설정
        /// <summary>
        /// grid ProcesssegmentidLis
        /// </summary>
        private void InitializeAreaPopupColumnInDetailGrid()
        {
            popupGridToolArea = grdFilmRequest.View.AddSelectPopupColumn("RECEIVEAREA", 180, new SqlQuery("GetAreaListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"ISMOD=Y"))
                // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
                .SetPopupLayout("RECEIVEAREA", PopupButtonStyles.Ok_Cancel, true, false)
                // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
                .SetPopupResultCount(1)
                //필수값설정
                .SetValidationIsRequired()
                // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
                .SetPopupResultMapping("RECEIVEAREA", "AREANAME")
                // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
                .SetPopupLayoutForm(500, 600, FormBorderStyle.FixedToolWindow)
                // 그리드의 남은 영역을 채울 컬럼 설정                
                .SetPopupAutoFillColumns("AREANAME")
                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {
                    int i = 0;
                    int currentIndex = grdFilmRequest.View.GetFocusedDataSourceRowIndex();

                    foreach (DataRow row in selectedRows)
                    {
                        if (i == 0)
                        {
                            grdFilmRequest.View.GetFocusedDataRow()["RECEIVEAREAID"] = row["AREAID"];
                            grdFilmRequest.View.GetFocusedDataRow()["RECEIVEAREA"] = row["AREANAME"];
                            grdFilmRequest.View.GetFocusedDataRow()["ISMODIFY"] = row["ISMODIFY"];
                        }
                        i++;
                    }
                })
            //.SetPopupValidationCustom(ValidationParentAreaIdPopup);
            ;
            // 팝업에서 사용할 조회조건 항목 추가
            popupGridToolArea.Conditions.AddTextBox("AREANAME");
            factoryPopup = popupGridToolArea.Conditions.AddComboBox("FACTORYID", new SqlQuery("GetFactoryListByEqp", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "FACTORYNAME", "FACTORYID")
                ;

            // 팝업 그리드 설정
            popupGridToolArea.GridColumns.AddTextBoxColumn("AREAID", 300)
                .SetIsHidden();
            popupGridToolArea.GridColumns.AddTextBoxColumn("AREANAME", 300)
                .SetIsReadOnly();
        }
        #endregion

        #region InitializeLotGrid - 필름Lot목록을 초기화한다.
        /// <summary>        
        /// 필름Lot목록을 초기화한다.
        /// </summary>
        private void InitializeLotGrid()
        {
            grdLotList.GridButtonItem = GridButtonItem.Export;
            grdLotList.View.SetIsReadOnly();

            grdLotList.View.AddTextBoxColumn("FILMNO", 250);                              //FilmLotNo

            grdLotList.View.PopulateColumns();
        }
        #endregion
        #endregion

        #region Event
        public void InitializeEvent()
        {
            btnSave.Click += BtnSave_Click;
            grdFilmRequest.View.FocusedRowChanged += grdFilmRequest_FocusedRowChanged;
            grdFilmRequest.View.ShowingEditor += grdFilmRequest_ShowingEditor;
            //grdFilmRequest.View.CheckStateChanged += grdFilmRequest_CheckStateChanged;
            //grdFilmRequest.View.MouseUp += grdFilmRequest_MouseUp;

            Shown += RegistReceiveFilm_Shown;
        }

        #region grdFilmRequest_ShowingEditor - 행의 체크여부에 따라 수정제어
        private void grdFilmRequest_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (!grdFilmRequest.View.IsRowChecked(grdFilmRequest.View.FocusedRowHandle))
                e.Cancel = true;
        }
        #endregion

        #region RegistReceiveFilm_Shown - Site관련정보를 화면로딩후 설정한다.
        private void RegistReceiveFilm_Shown(object sender, EventArgs e)
        {
            ChangeSiteCondition();

            ((SmartComboBox)Conditions.GetControl("P_PLANTID")).EditValueChanged += (s, arg) => ChangeSiteCondition();
        }
        #endregion

        #region grdFilmRequest_CheckStateChanged - 행이 특정상태일때만 선택이 가능
        private void grdFilmRequest_CheckStateChanged(object sender, EventArgs e)
        {
            //Release상태가 아니면 선택할 수 없다.
            int rowHandle = grdFilmRequest.View.FocusedRowHandle;

            if (!grdFilmRequest.View.GetDataRow(rowHandle).GetString("FILMPROGRESSSTATUSID").Equals("Release"))
            {
                grdFilmRequest.View.CheckRow(rowHandle, false);
            }
        }
        #endregion

        #region grdFilmRequest_MouseUp - 그리드 제목부분 작업(체크박스)를 위한 마우스 이벤트
        private void grdFilmRequest_MouseUp(object sender, MouseEventArgs e)
        {
            DevExpress.XtraGrid.Views.BandedGrid.ViewInfo.BandedGridHitInfo hi = grdFilmRequest.View.CalcHitInfo(e.Location);
            if (hi.InColumn && !hi.InRow)
            {
                if (hi.Column.FieldName.Equals("_INTERNAL_CHECKMARK_SELECTION_"))
                {
                    grdFilmRequest.View.CheckStateChanged -= grdFilmRequest_CheckStateChanged;

                    for (int i = 0; i < grdFilmRequest.View.RowCount; i++)
                    {
                        //Release상태만 선택가능하다.
                        if (grdFilmRequest.View.GetDataRow(i).GetString("FILMPROGRESSSTATUSID").Equals("Release"))
                        {
                            if (grdFilmRequest.View.GetRowCellValue(i, "CHECKEDOPTION").Equals("Y"))
                            {
                                grdFilmRequest.View.SetRowCellValue(i, "_INTERNAL_CHECKMARK_SELECTION_", false);
                                grdFilmRequest.View.SetRowCellValue(i, "CHECKEDOPTION", "");
                            }
                            else
                            {
                                grdFilmRequest.View.SetRowCellValue(i, "_INTERNAL_CHECKMARK_SELECTION_", true);
                                grdFilmRequest.View.SetRowCellValue(i, "CHECKEDOPTION", "Y");
                            }
                        }
                        else
                        {
                            grdFilmRequest.View.CheckRow(i, false);
                        }
                    }

                    grdFilmRequest.View.CheckStateChanged += grdFilmRequest_CheckStateChanged;
                }
            }
        }
        #endregion

        #region BtnSave_Click - 저장버튼 이벤트
        private void BtnSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }
        #endregion

        #region grdFilmRequest_FocusedRowChanged - 그리드내 항목변경 이벤트
        private void grdFilmRequest_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            SearchLotData();
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

            if (btn.Name.ToString().Equals("Receive"))
                BtnSave_Click(sender, e);
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
            values.Add("USERID", UserInfo.Current.Id);
            values.Add("CURRENTLOGINID", UserInfo.Current.Id);
            #endregion

            if (Conditions.GetValue("AREANAME").ToString() != "")
                values.Add("AREAID", _searchAreaID);

            //if (Conditions.GetValue("VENDORNAME").ToString() != "")
            //    values.Add("VENDORID", _searchVendorID);

            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable toolReqSearchResult = SqlExecuter.Query("GetReceiveRequestMakingFilmListByTool", "10001", values);

            grdFilmRequest.DataSource = toolReqSearchResult;

            if (toolReqSearchResult.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                grdLotList.DataSource = null;
            }
        }
        #endregion

        #region Research - 재검색(데이터 입력후와 같은 경우에 사용)
        /// <summary>
        /// 저장 삭제후 재 검색시 사용
        /// 생성 및 수정시 발생한 아이디를 통해 자동 선택되도록 유도하며 삭제시에는 null로 매개변수를 받아서 첫번째 행을 보여준다. 행이 없다면 빈 값으로 설정
        /// </summary>
        /// <param name="inboudNo">입력 및 수정시 발생한 값의 아이디.</param>
        private void Research()
        {
            // TODO : 조회 SP 변경
            InitializeInsertForm();
            // TODO : 조회 SP 변경
            var values = Conditions.GetValues();

            #region 기간 검색형 전환 처리 - 만약 시간까지 포함하여 검색한다면 아래 문구는 필요없음            
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            values.Add("USERID", UserInfo.Current.Id);
            values.Add("CURRENTLOGINID", UserInfo.Current.Id);
            #endregion

            if (Conditions.GetValue("AREANAME").ToString() != "")
                values.Add("AREAID", _searchAreaID);

            //if (Conditions.GetValue("VENDORNAME").ToString() != "")
            //    values.Add("VENDORID", _searchVendorID);

            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable toolReqSearchResult = SqlExecuter.Query("GetReceiveRequestMakingFilmListByTool", "10001", values);

            grdFilmRequest.DataSource = toolReqSearchResult;

            if (toolReqSearchResult.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                grdLotList.DataSource = null;
            }
        }
        #endregion

        #region SearchLotData - Lot 데이터를 검색
        /// <summary>
        /// 저장 삭제후 재 검색시 사용
        /// 생성 및 수정시 발생한 아이디를 통해 자동 선택되도록 유도하며 삭제시에는 null로 매개변수를 받아서 첫번째 행을 보여준다. 행이 없다면 빈 값으로 설정
        /// </summary>
        /// <param name="inboudNo">입력 및 수정시 발생한 값의 아이디.</param>
        private void SearchLotData()
        {
            if (grdFilmRequest.View.GetFocusedDataRow() != null)
            {
                Dictionary<string, object> values = new Dictionary<string, object>();

                values.Add("FILMSEQUENCE", grdFilmRequest.View.GetFocusedDataRow().GetString("FILMSEQUENCE"));

                values = Commons.CommonFunction.ConvertParameter(values);
                DataTable rotTable = SqlExecuter.Query("GetFilmLotListByTool", "10001", values);

                grdLotList.DataSource = rotTable;
            }
        }
        #endregion

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // 작업구분
            Conditions.AddComboBox("JOBTYPEID", new SqlQuery("GetCodeListByToolWithLanguage", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CODECLASSID=JobType"), "CODENAME", "CODEID")
                      .SetLabel("JOBTYPE")
                      .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                      .SetPosition(3.1)
                      .SetEmptyItem("", "", true);

            // 생산구분
            Conditions.AddComboBox("PRODUCTIONTYPEID", new SqlQuery("GetCodeListByToolWithLanguage", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CODECLASSID=ProductionType"), "CODENAME", "CODEID")
                      .SetLabel("PRODUCTIONTYPE")
                      .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                      .SetPosition(3.2)
                      .SetEmptyItem("", "", true);

            #region 입고작업장

            areaCondition = Conditions.AddSelectPopup("AREANAME", new SqlQuery("GetAreaListByTool", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CURRENTLOGINID={UserInfo.Current.Id}"))
                                      .SetPopupLayout("RECEIPTAREA", PopupButtonStyles.Ok_Cancel, true, true)
                                      .SetPopupLayoutForm(600, 800, FormBorderStyle.FixedToolWindow)
                                      .SetPopupAutoFillColumns("AREANAME")
                                      .SetLabel("RECEIPTAREA")
                                      .SetPopupResultCount(1)
                                      .SetPosition(3.3)
                                      .SetPopupResultMapping("AREANAME", "AREANAME")
                                      .SetPopupApplySelection((selectedRows, dataGridRows) =>
                                      {
                                          //작업장 변경 시 작업자 조회
                                          selectedRows.Cast<DataRow>().ForEach(row =>
                                          {
                                              _searchAreaID = row.GetString("AREAID");
                                          });
                                      });


            areaCondition.Conditions.AddTextBox("AREANAME");

            areaCondition.GridColumns.AddTextBoxColumn("AREAID", 150).SetIsHidden();
            areaCondition.GridColumns.AddTextBoxColumn("AREANAME", 200);

            #endregion

            #region 인수작업장

            receiptAreaCondition = Conditions.AddSelectPopup("RECEIVEAREAID", new SqlQuery("GetAreaListByTool", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CURRENTLOGINID={UserInfo.Current.Id}"))
                                             .SetPopupLayout("RECEIVEAREA", PopupButtonStyles.Ok_Cancel, true, true)
                                             .SetPopupLayoutForm(600, 800, FormBorderStyle.FixedToolWindow)
                                             .SetPopupAutoFillColumns("RECEIVEAREAID")
                                             .SetLabel("RECEIVEAREA")
                                             .SetPopupResultCount(1)
                                             .SetPosition(3.4)
                                             .SetPopupResultMapping("RECEIVEAREAID", "AREAID");

            receiptAreaCondition.ValueFieldName = "AREAID";
            receiptAreaCondition.DisplayFieldName = "AREANAME";

            receiptAreaCondition.Conditions.AddTextBox("AREANAME");

            receiptAreaCondition.GridColumns.AddTextBoxColumn("AREAID", 150).SetIsHidden();
            receiptAreaCondition.GridColumns.AddTextBoxColumn("AREANAME", 200);

            #endregion

            // 진행상태
            Conditions.AddComboBox("FILMPROGRESSSTATUS", new SqlQuery("GetFilmProgressStatusCodeListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"FILMSCREENSTATUS=ReceiveFilm"), "CODENAME", "CODEID")
                      .SetLabel("FILMPROGRESSSTATUS")
                      .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                      .SetPosition(3.5)
                      .SetEmptyItem("", "", true);
        }

        #endregion

        #region 유효성 검사

        protected override void OnValidateContent()
        {
            base.OnValidateContent();
        }

        #region ValidateContent - 입력시의 Validation을 점검한다.
        private bool ValidateContent(string filmProgressStatus, out string messageCode)
        {
            messageCode = "";
            DataTable checkedTable = grdFilmRequest.View.GetCheckedRows();
            foreach (DataRow checkedRow in checkedTable.Rows)
            {
                if (!checkedRow.GetString("FILMPROGRESSSTATUSID").Equals(filmProgressStatus))
                {
                    messageCode = "ValidateReleaseStatusForReceipt";
                    return false;
                }

                if (checkedRow.GetString("RECEIVEAREAID").Equals("") && checkedRow.GetString("RECEIVEAREA").Equals(""))
                {
                    messageCode = "ValidateReceiveAreaStatusForReceipt";
                    return false;
                }

                if (checkedRow.GetString("ISMODIFY").Equals("N"))
                {
                    messageCode = "ValidationFilmReceiveModify";   //선택한 필름인수들의 작업장중 권한이 없는 작업장이 있습니다. 
                    return false;
                }

                //if (checkedRow.GetString("MEASURECONTRACTIONY").Equals(""))
                //{
                //    messageCode = "ValidationFilmMeasureContraction";
                //    return false;
                //}
            }
            return true;
        }
        #endregion

        #region ValidateCotnent - 각각 다른 상태에 관련해 Validation을 수행한다.
        private bool ValidateContent(string filmProgressStatus, string otherFilmProgressStatus)
        {
            DataTable checkedTable = grdFilmRequest.View.GetCheckedRows();
            foreach (DataRow checkedRow in checkedTable.Rows)
            {
                if (checkedRow.GetString("FILMPROGRESSSTATUSID").Equals(filmProgressStatus) || checkedRow.GetString("FILMPROGRESSSTATUSID").Equals(otherFilmProgressStatus))
                    return true;
            }
            return false;
        }
        #endregion

        #region ValidateComboBoxEqualValue - 2개의 콤보박스의 값이 동일한지 검사
        private bool ValidateComboBoxEqualValue(SmartComboBox originBox, SmartComboBox targetBox)
        {
            //두 콤보박스의 값이 같으면 안된다.
            if (originBox.EditValue.Equals(targetBox.EditValue))
                return false;
            return true;
        }
        #endregion

        #region ValidateNumericBox - 숫자형 텍스트 박스를 검사 - 기준숫자보다 높은지 작은지 검증
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

        #region ValidateEditValue - 데이터를 검증
        private bool ValidateEditValue(object editValue)
        {
            if (editValue == null)
                return false;

            if (editValue.ToString() == "")
                return false;

            return true;
        }
        #endregion

        #region ValidateCellInGrid - 그리드내의 특정 컬럼에 대한 Validation을 진행
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

        #region SetRequiredValidationControl - 필수입력항목을 정의
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
            dt.TableName = "filmReceiveList";
            //===================================================================================
            dt.Columns.Add("FILMSEQUENCE");
            dt.Columns.Add("RECEIVEDATE");
            dt.Columns.Add("RECEIVEUSERID");
            dt.Columns.Add("RECEIVEAREAID");

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
                string messageCode = "";
                //Release상태만 저장이 가능하다.
                if (ValidateContent("Release", out messageCode))
                {
                    DataSet filmReqSet = new DataSet();
                    //치공구 제작의뢰를 입력
                    DataTable filmReqTable = CreateSaveDatatable();


                    DataTable checkedTable = grdFilmRequest.View.GetCheckedRows();

                    foreach (DataRow checkedRow in checkedTable.Rows)
                    {
                        DataRow filmReqRow = filmReqTable.NewRow();

                        DateTime receiveDate = DateTime.Now;

                        filmReqRow["FILMSEQUENCE"] = checkedRow.GetString("FILMSEQUENCE");
                        filmReqRow["RECEIVEDATE"] = receiveDate.ToString("yyyy-MM-dd HH:mm:ss");
                        filmReqRow["RECEIVEUSERID"] = UserInfo.Current.Id;
                        filmReqRow["RECEIVEAREAID"] = checkedRow.GetString("RECEIVEAREAID");

                        filmReqRow["VALIDSTATE"] = "Valid";
                        filmReqRow["MODIFIER"] = UserInfo.Current.Id;
                        filmReqRow["_STATE_"] = "modified";

                        filmReqTable.Rows.Add(filmReqRow);
                    }

                    filmReqSet.Tables.Add(filmReqTable);

                    this.ExecuteRule<DataTable>("FilmReceiveRequestMaking", filmReqSet);

                    ControlEnableProcess("modified");

                    Research();
                }
                else
                {
                    this.ShowMessage(MessageBoxButtons.OK, messageCode, "");
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
            if (segmentCondition != null)
                segmentCondition.Query = new SqlQuery("GetProcessSegmentByTool", "10001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "ENTERPRISEID", UserInfo.Current.Enterprise }, { "PLANTID", Conditions.GetValue("P_PLANTID") } });

            if (areaCondition != null)
                areaCondition.SearchQuery = new SqlQuery("GetAreaListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}", $"CURRENTLOGINID={UserInfo.Current.Id}");

            if (receiptAreaCondition != null)
                receiptAreaCondition.SearchQuery = new SqlQuery("GetAreaListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}", $"CURRENTLOGINID={UserInfo.Current.Id}");

            if (filmCodeCondition != null)
                filmCodeCondition.SearchQuery = new SqlQuery("GetFilmCodePopupListByTool", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CURRENTLOGINID={UserInfo.Current.Id}");

            if (vendorCondition != null)
                vendorCondition.SearchQuery = new SqlQuery("GetVendorListByTool", "10001", $"LANGUAGETYPE={ UserInfo.Current.LanguageType }", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}");

            if(popupGridToolArea != null)
                popupGridToolArea.SearchQuery = new SqlQuery("GetAreaListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}", $"CURRENTLOGINID={UserInfo.Current.Id}", $"ISMOD=Y");

            if (factoryPopup != null)
                factoryPopup.Query = new SqlQuery("GetFactoryListByEqp", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}");

            //if (productPopup != null)
            //    productPopup.SearchQuery = new SqlQuery("GetProductdefidPoplistByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}", $"PRODUCTDEFTYPE=Product");

            //작업장 검색조건 초기화
            ((SmartSelectPopupEdit)Conditions.GetControl("AREANAME")).ClearValue(); //RECEIVEAREAID
            ((SmartSelectPopupEdit)Conditions.GetControl("RECEIVEAREAID")).ClearValue(); //RECEIVEAREAID
        }
        #endregion
        #endregion
    }
}
