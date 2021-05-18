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
    /// 프 로 그 램 명  : 치공구관리 > 필름관리 > 필름요청접수
    /// 업  무  설  명  : 제작요청된 필름을 접수 혹은 취소한다.
    /// 생    성    자  : 김용조
    /// 생    성    일  : 2019-08-20
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class RegistRequestMakeFilm : SmartConditionManualBaseForm
    {
        #region Local Variables        
        private string _searchAreaID = "";
        private string _searchVendorID = "";

        //Site 및 작업장 권한에 따른 팝업창제어
        ConditionItemSelectPopup vendorCondition;
        ConditionItemSelectPopup areaCondition;
        ConditionItemSelectPopup filmCodeCondition;
        ConditionItemComboBox segmentCondition;
        #endregion

        #region 생성자

        public RegistRequestMakeFilm()
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
            
            ControlEnableProcess("");
        }

        private void InitialInputControls()
        {
            //필수항목 등록
        }

        #region InitRequiredControl - 필수입력항목들을 체크한다.
        private void InitRequiredControl()
        {
        }
        #endregion

        #region InitializeGrid - 필름요청접수목록을 초기화한다.
        private void InitializeGrid()
        {
            grdMain.GridButtonItem = GridButtonItem.Export;
            grdMain.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdMain.View.AddTextBoxColumn("FILMSEQUENCE").SetIsHidden();                             //필름시퀀스
            grdMain.View.AddTextBoxColumn("FILMPROGRESSSTATUSID").SetIsHidden();                     //진행상태아이디
            grdMain.View.AddTextBoxColumn("REQUESTUSERID").SetIsHidden();                            //요청자아이디
            grdMain.View.AddTextBoxColumn("JOBTYPEID").SetIsHidden();                                //작업구분아이디
            grdMain.View.AddTextBoxColumn("PRODUCTIONTYPEID").SetIsHidden();                         //생산구분아이디
            grdMain.View.AddTextBoxColumn("FILMTYPEID").SetIsHidden();                               //필름구분아이디
            grdMain.View.AddTextBoxColumn("FILMCATEGORYID").SetIsHidden();                           //필름유형아이디
            grdMain.View.AddTextBoxColumn("FILMDETAILCATEGORYID").SetIsHidden();                     //상세유형아이디
            grdMain.View.AddTextBoxColumn("CUSTOMERID").SetIsHidden();                               //작업구분아이디
            grdMain.View.AddTextBoxColumn("USEPROCESSSEGMENT", 80).SetIsHidden();                    //사용공정
            grdMain.View.AddTextBoxColumn("PRIORITYID").SetIsHidden();                               //우선순위아이디
            grdMain.View.AddTextBoxColumn("MAKEVENDORID").SetIsHidden();                             //제작업체아이디
            grdMain.View.AddTextBoxColumn("MAKEVENDOR", 150).SetIsHidden();                         //제작업체
            grdMain.View.AddTextBoxColumn("RECEIPTAREAID").SetIsHidden();                            //입고작업장아이디
            grdMain.View.AddTextBoxColumn("ACCEPTUSERID").SetIsHidden();                             //접수자아이디
            grdMain.View.AddTextBoxColumn("USEPLANDATE", 100).SetIsHidden();                         //사용예정일
            grdMain.View.AddTextBoxColumn("RELEASEDATE", 150).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsHidden();    //출고일시
            grdMain.View.AddTextBoxColumn("RELEASEUSERID").SetIsHidden();                            //출고자아이디
            grdMain.View.AddTextBoxColumn("RELEASEUSER", 100).SetIsHidden();                         //출고자
            grdMain.View.AddTextBoxColumn("MEASURECONTRACTIONX", 80).SetIsHidden();                  //실측수축율X
            grdMain.View.AddTextBoxColumn("MEASURECONTRACTIONY", 80).SetIsHidden();                  //실측수축율Y
            grdMain.View.AddTextBoxColumn("RECEIVEDATE", 160).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsHidden();    //인수일시
            grdMain.View.AddTextBoxColumn("RECEIVEUSERID").SetIsHidden();                            //인수자아이디
            grdMain.View.AddTextBoxColumn("RECEIVEUSER", 100).SetIsHidden();                         //인수자
            grdMain.View.AddTextBoxColumn("CHECKEDOPTION").SetIsHidden();                            //체크옵션
            grdMain.View.AddTextBoxColumn("FILMUSELAYER2", 120).SetIsHidden();                      //Layer2
            grdMain.View.AddTextBoxColumn("CUSTOMER", 120).SetIsHidden();                           //고객

            grdMain.View.AddTextBoxColumn("FILMPROGRESSSTATUS", 80).SetTextAlignment(TextAlignment.Center).SetIsReadOnly(); //진행상태
            grdMain.View.AddTextBoxColumn("REQUESTDATE", 150).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly();  //의뢰일
            grdMain.View.AddTextBoxColumn("REQUESTDEPARTMENT", 100).SetIsReadOnly(); //의뢰부서ID
            grdMain.View.AddTextBoxColumn("REQUESTUSER", 80).SetIsReadOnly();   //요청자
            grdMain.View.AddTextBoxColumn("PRODUCTDEFID", 120).SetIsReadOnly();     //품목코드
            grdMain.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetIsReadOnly(); //내부 Rev
            grdMain.View.AddTextBoxColumn("PRODUCTDEFNAME", 250).SetIsReadOnly();   //품목명
            grdMain.View.AddTextBoxColumn("FILMVERSION", 80).SetIsReadOnly();   //CAM Rev.
            grdMain.View.AddTextBoxColumn("FILMNAME", 350).SetIsReadOnly();     //필름명
            grdMain.View.AddTextBoxColumn("CONTRACTIONX", 120).SetIsReadOnly();  //수축률X
            grdMain.View.AddTextBoxColumn("CONTRACTIONY", 120).SetIsReadOnly();  //수축률Y
            grdMain.View.AddTextBoxColumn("FILMCATEGORY", 120).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();  //필름유형1
            grdMain.View.AddTextBoxColumn("FILMDETAILCATEGORY", 150).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();    //필름유형2
            grdMain.View.AddTextBoxColumn("FILMUSELAYER1", 100).SetIsReadOnly();    //Layer1
            grdMain.View.AddTextBoxColumn("QTY", 80).SetIsReadOnly();   //수량
            grdMain.View.AddTextBoxColumn("REQUESTCOMMENT", 200).SetIsReadOnly();   //의뢰사유
            grdMain.View.AddTextBoxColumn("WAITREASON", 250);   //발행대기사유
            grdMain.View.AddTextBoxColumn("RECEIPTAREA", 150).SetIsReadOnly();  //입고작업장
            grdMain.View.AddTextBoxColumn("JOBTYPE", 80).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();    //작업구분
            grdMain.View.AddTextBoxColumn("PRODUCTIONTYPE", 80).SetTextAlignment(TextAlignment.Center).SetIsReadOnly(); //생산구분
            grdMain.View.AddTextBoxColumn("FILMTYPE", 100).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();  //필름구분
            grdMain.View.AddTextBoxColumn("FILMCODE", 150).SetIsReadOnly(); //필름코드
            grdMain.View.AddTextBoxColumn("CHANGECONTRACTIONX", 120).SetIsReadOnly();   //요청수축률(%)X
            grdMain.View.AddTextBoxColumn("CHANGECONTRACTIONY", 120).SetIsReadOnly();   //요청수축률(%)Y
            grdMain.View.AddTextBoxColumn("RESOLUTION", 80).SetIsReadOnly();    //해상도
            grdMain.View.AddTextBoxColumn("ISCOATING", 80).SetIsReadOnly(); //코팅유무
            grdMain.View.AddTextBoxColumn("PRIORITY", 80).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();   //우선순위
            grdMain.View.AddTextBoxColumn("ACCEPTDEPARTMENT", 100).SetIsReadOnly(); //접수부서
            grdMain.View.AddTextBoxColumn("ACCEPTMAINTWORKORDERUSER", 100).SetIsReadOnly(); //접수자
            grdMain.View.AddTextBoxColumn("RECEPTDATE", 150).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly();   //접수일

            grdMain.View.PopulateColumns();
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
            btnRegist.Click += btnRegist_Click;
            btnCancel.Click += btnCancel_Click;
            btnReport.Click += btnReport_Click;
            btnSave.Click += BtnSave_Click;

            //grdFilmRequest.View.MouseUp += grdFilmRequest_MouseUp;            
            grdMain.View.RowCellStyle += grdFilmRequest_RowCellStyle;
            //grdFilmRequest.View.CheckStateChanged += grdFilmRequest_CheckStateChanged;

            Shown += RegistRequestMakeFilm_Shown;
        }
        
        #region RegistRequestMakeFilm_Shown - Site관련정보를 화면로딩후 설정한다.
        private void RegistRequestMakeFilm_Shown(object sender, EventArgs e)
        {
            ChangeSiteCondition();

            ((SmartComboBox)Conditions.GetControl("P_PLANTID")).EditValueChanged += (s, arg) => ChangeSiteCondition(); 
        }
        #endregion

        #region BtnSave_Click - 저장버튼 이벤트
        private void BtnSave_Click(object sender, EventArgs e)
        {
            SaveWaitReasonData();
        }
        #endregion

        #region grdFilmRequest_RowCellStyle - 조건에 따라 그리드내의 행 배경색 제어
        private void grdFilmRequest_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (!grdMain.View.GetDataRow(e.RowHandle).GetString("WAITREASON").Equals(""))
            {
                e.Appearance.BackColor = Color.Orange;
                e.Appearance.BackColor2 = Color.Orange;
                e.Appearance.ForeColor = Color.Black;
            }

            if (grdMain.View.GetDataRow(e.RowHandle).GetString("FILMPROGRESSSTATUSID").Equals("CancelAccept"))
            {
                e.Appearance.BackColor = Color.Gold;
                e.Appearance.BackColor2 = Color.Gold;
                e.Appearance.ForeColor = Color.Black;
            }
            else if (grdMain.View.GetDataRow(e.RowHandle).GetString("FILMPROGRESSSTATUSID").Equals("RequestAgain"))
            {
                e.Appearance.BackColor = Color.LimeGreen;
                e.Appearance.BackColor2 = Color.LimeGreen;
                e.Appearance.ForeColor = Color.Black;
            }            
        }
        #endregion

        #region grdFilmRequest_CheckStateChanged - 체크박스 상태변화에 따른 로직
        private void grdFilmRequest_CheckStateChanged(object sender, EventArgs e)
        {
            //Release상태가 아니면 선택할 수 없다.
            int rowHandle = grdMain.View.FocusedRowHandle;

            if (grdMain.View.GetDataRow(rowHandle).GetString("FILMPROGRESSSTATUSID").Equals("Receive") || grdMain.View.GetDataRow(rowHandle).GetString("FILMPROGRESSSTATUSID").Equals("Release"))
            {
                grdMain.View.CheckRow(rowHandle, false);
            }
        }
        #endregion

        #region grdFilmRequest_MouseUp - 셀에 마우스커서가 이동했을시의 이벤트 - 체크박스 헤더의 동작제어등에 쓰임
        private void grdFilmRequest_MouseUp(object sender, MouseEventArgs e)
        {
            DevExpress.XtraGrid.Views.BandedGrid.ViewInfo.BandedGridHitInfo hi = grdMain.View.CalcHitInfo(e.Location);
            if (hi.InColumn && !hi.InRow)
            {
                if (hi.Column.FieldName.Equals("_INTERNAL_CHECKMARK_SELECTION_"))
                {
                    grdMain.View.CheckStateChanged -= grdFilmRequest_CheckStateChanged;

                    for (int i = 0; i < grdMain.View.RowCount; i++)
                    {
                        if (grdMain.View.GetDataRow(i).GetString("FILMPROGRESSSTATUSID").Equals("Accept") 
                            || grdMain.View.GetDataRow(i).GetString("FILMPROGRESSSTATUSID").Equals("Request"))
                        {
                            if (grdMain.View.GetRowCellValue(i, "CHECKEDOPTION").Equals("Y"))
                            {
                                grdMain.View.SetRowCellValue(i, "_INTERNAL_CHECKMARK_SELECTION_", false);
                                grdMain.View.SetRowCellValue(i, "CHECKEDOPTION", "");
                            }
                            else
                            {
                                grdMain.View.SetRowCellValue(i, "_INTERNAL_CHECKMARK_SELECTION_", true);
                                grdMain.View.SetRowCellValue(i, "CHECKEDOPTION", "Y");
                            }
                        }
                        else
                        {
                            grdMain.View.CheckRow(i, false);
                        }
                    }

                    grdMain.View.CheckStateChanged += grdFilmRequest_CheckStateChanged;
                }
            }
        }
        #endregion

        #region btnReport_Click - 리포트 출력
        private void btnReport_Click(object sender, EventArgs e)
        {
            //PrintData();
            ShowPrintData();
        }
        #endregion

        #region btnCancel_Click - 취소 버튼 이벤트
        private void btnCancel_Click(object sender, EventArgs e)
        {
            CancelData();
        }
        #endregion

        #region btnRegist_Click - 접수버튼 이벤트
        private void btnRegist_Click(object sender, EventArgs e)
        {
            SaveData();
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
            else if (btn.Name.ToString().Equals("Accept"))
                btnRegist_Click(sender, e);
            else if (btn.Name.ToString().Equals("Cancel2"))
                btnCancel_Click(sender, e);
            else if (btn.Name.ToString().Equals("RequestReport"))
                btnReport_Click(sender, e);
        }
        #endregion

        #region 검색
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            InitializeInsertForm();
            // TODO : 조회 SP 변경
            var values = Conditions.GetValues();
       
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            values.Add("USERID", UserInfo.Current.Id);

            if (Conditions.GetValue("AREANAME").ToString() != "")
                values.Add("AREAID", _searchAreaID);

            //if (Conditions.GetValue("VENDORNAME").ToString() != "")
            //    values.Add("VENDORID", _searchVendorID);

            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable toolReqSearchResult = SqlExecuter.Query("GetRegistRequestMakingFilmListByTool", "10001", values);

            grdMain.DataSource = toolReqSearchResult;

            if (toolReqSearchResult.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }
        }

        /// <summary>
        /// 재검색(데이터 입력후와 같은 경우에 사용)
        /// </summary>
        private void Research()
        {
            // TODO : 조회 SP 변경
            InitializeInsertForm();
            // TODO : 조회 SP 변경
            var values = Conditions.GetValues();
      
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            values.Add("USERID", UserInfo.Current.Id);

            if (Conditions.GetValue("AREANAME").ToString() != "")
                values.Add("AREAID", _searchAreaID);

            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable toolReqSearchResult = SqlExecuter.Query("GetRegistRequestMakingFilmListByTool", "10001", values);

            grdMain.DataSource = toolReqSearchResult;

            if (toolReqSearchResult.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }
        }

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

            #region 작업장

            areaCondition = Conditions.AddSelectPopup("AREANAME", new SqlQuery("GetAreaListByTool", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
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

            // 진행상태 조회
            Conditions.AddComboBox("FILMPROGRESSSTATUS", new SqlQuery("GetFilmProgressStatusCodeListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"FILMSCREENSTATUS=AcceptFilm"), "CODENAME", "CODEID")
                      .SetLabel("FILMPROGRESSSTATUS")
                      .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                      .SetPosition(3.4)
                      .SetEmptyItem("", "", true);
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

        #region ValidateContent - 내용 점검 - 선택된 행의 현재 상태를 검증한다. (접수할 수 없는 상태를 점검)
        private bool ValidateContent(string filmProgressStatus)
        {
            DataTable checkedTable = grdMain.View.GetCheckedRows();
            foreach(DataRow checkedRow in checkedTable.Rows)
            {
                if (!checkedRow.GetString("FILMPROGRESSSTATUSID").Equals(filmProgressStatus))
                    return false;
            }
            return true;
        }
        private bool ValidateContent(DataRow currentRow, string[] filmProgressStatus)
        {
            DataTable checkedTable = grdMain.View.GetCheckedRows();

            foreach (string status in filmProgressStatus)
                if (currentRow.GetString("FILMPROGRESSSTATUSID").Equals(status))
                    return true;

            return false;
        }
        #endregion

        #region ValidateSingleContent - DataRow에 대한 점검
        private bool ValidateSingleContent(string filmProgressStatus, DataRow insertRow)
        {
                if (!insertRow.GetString("FILMPROGRESSSTATUSID").Equals(filmProgressStatus))
                    return false;           
            return true;
        }
        #endregion

        #region ValidateComboBoxEqualValue - 2개의 콤보박스의 데이터를 비교
        private bool ValidateComboBoxEqualValue(SmartComboBox originBox, SmartComboBox targetBox)
        {
            //두 콤보박스의 값이 같으면 안된다.
            if (originBox.EditValue.Equals(targetBox.EditValue))
                return false;
            return true;
        }
        #endregion

        #region ValidateNumericBox - 숫자형 텍스트박스의 데이터 점검, 기준데이터와 값비교
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

        #region ValidateEditValue - 데이터에 대한 기본검증
        private bool ValidateEditValue(object editValue)
        {
            if (editValue == null)
                return false;

            if (editValue.ToString() == "")
                return false;

            return true;
        }
        #endregion

        #region ValidateCellInGrid - 그리드내 특정 셀에 대한 검증
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

        #region SetRequiredValidationControl - 필수입력컨트롤 설정
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
            dt.TableName = "filmRegistRequestMakingList";
            //===================================================================================
            dt.Columns.Add("FILMSEQUENCE");
            dt.Columns.Add("ACCEPTDATE");
            dt.Columns.Add("ACCEPTDEPARTMENT");
            dt.Columns.Add("ACCEPTUSERID");

            dt.Columns.Add("WAITREASON");

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

        #region CreateSaveWaitReasonDatatable : ToolRequest 입력/수정/삭제를 위한 DataTable의 Template 생성
        /// <summary>
        /// ToolRequest 입력/수정/삭제를 위한 DataTable의 Template 생성
        /// 입력시에는 ToolRequest와 ToolRequestDetail에 입력하고
        /// 수정시에는 ToolRequest와 ToolRequestDetail 및 ToolRequestDetailLot에 까지 데이터를 기입한다.
        /// </summary>
        /// <returns></returns>
        private DataTable CreateSaveWaitReasonDatatable()
        {
            DataTable dt = new DataTable();
            dt.TableName = "toolInfoList";
            //===================================================================================
            dt.Columns.Add("FILMSEQUENCE");
            dt.Columns.Add("WAITREASON");            

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

        #region CreatePrintDatatable : 의뢰서발행을 위한 DataTable생성
        private DataTable CreatePrintDatatable()
        {
            DataTable dt = new DataTable();
            dt.TableName = "filmRegistRequestMakingList";
            //===================================================================================
            dt.Columns.Add("FILMSEQUENCE");
            dt.Columns.Add("PRINTDATE");
            dt.Columns.Add("PRINTUSER");

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
                btnRegist.Enabled = false;
                btnCancel.Enabled = false;
                btnReport.Enabled = false;

                //Validation Check시 입력/수정시 검사할 값이 다르다면 수정필요
                if (ValidateContent("Request") || ValidateContent("RequestAgain"))
                {
                    DataSet filmReqSet = new DataSet();
                    //치공구 제작의뢰를 입력
                    DataTable filmReqTable = CreateSaveDatatable();
                    

                    DataTable checkedTable = grdMain.View.GetCheckedRows();

                    foreach(DataRow checkedRow in checkedTable.Rows)
                    {
                        DataRow filmReqRow = filmReqTable.NewRow();

                        DateTime acceptDate = DateTime.Now;

                        filmReqRow["FILMSEQUENCE"] = checkedRow.GetString("FILMSEQUENCE");
                        filmReqRow["ACCEPTDATE"] = acceptDate.ToString("yyyy-MM-dd HH:mm:ss");
                        filmReqRow["ACCEPTDEPARTMENT"] = UserInfo.Current.Department;
                        filmReqRow["ACCEPTUSERID"] = UserInfo.Current.Id;

                        filmReqRow["VALIDSTATE"] = "Valid";
                        filmReqRow["MODIFIER"] = UserInfo.Current.Id;
                        filmReqRow["_STATE_"] = "added";

                        filmReqTable.Rows.Add(filmReqRow);
                    }

                    filmReqSet.Tables.Add(filmReqTable);

                    this.ExecuteRule<DataTable>("FilmRegistRequestMaking", filmReqSet);

                    ControlEnableProcess("modified");
                    
                    Research();
                }
                else
                {
                    this.ShowMessage(MessageBoxButtons.OK, "FILMREGISTREQUEST", "");
                }
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
            finally
            {
                this.CloseWaitArea();
                btnRegist.Enabled = true;
                btnCancel.Enabled = true;
                btnReport.Enabled = true;
            }
        }
        #endregion

        #region SaveWaitReasonData : 입력/수정을 수행
        private void SaveWaitReasonData()
        {
            //Validation 체크 부분 작성 필요
            this.ShowWaitArea();

            //저장 로직
            try
            {
                btnRegist.Enabled = false;
                btnCancel.Enabled = false;
                btnReport.Enabled = false;

                //Validation Check시 입력/수정시 검사할 값이 다르다면 수정필요
                DataSet filmReqSet = new DataSet();
                //치공구 제작의뢰를 입력
                DataTable filmReqTable = CreateSaveWaitReasonDatatable();

                DataTable checkedTable = grdMain.GetChangedRows();

                foreach (DataRow checkedRow in checkedTable.Rows)
                {
                    DataRow filmReqRow = filmReqTable.NewRow();

                    DateTime acceptDate = DateTime.Now;

                    filmReqRow["FILMSEQUENCE"] = checkedRow.GetString("FILMSEQUENCE");
                    filmReqRow["WAITREASON"] = checkedRow.GetString("WAITREASON");

                    filmReqRow["VALIDSTATE"] = "Valid";
                    filmReqRow["MODIFIER"] = UserInfo.Current.Id;
                    filmReqRow["_STATE_"] = "added";

                    filmReqTable.Rows.Add(filmReqRow);
                }

                filmReqSet.Tables.Add(filmReqTable);

                this.ExecuteRule<DataTable>("ToolInformation", filmReqSet);

                ControlEnableProcess("modified");

                Research();              
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
            finally
            {
                this.CloseWaitArea();
                btnRegist.Enabled = true;
                btnCancel.Enabled = true;
                btnReport.Enabled = true;
            }
        }
        #endregion

        #region CancelData : 삭제를 수행
        private void CancelData()
        {
            //Validation 체크 부분 작성 필요
            this.ShowWaitArea();

            //저장 로직
            try
            {
                btnRegist.Enabled = false;
                btnCancel.Enabled = false;
                btnReport.Enabled = false;

                //Validation Check시 입력/수정시 검사할 값이 다르다면 수정필요

                DataSet filmReqSet = new DataSet();
                //치공구 제작의뢰를 입력
                DataTable filmReqTable = CreateSaveDatatable();

                DataTable checkedTable = grdMain.View.GetCheckedRows();

                foreach (DataRow checkedRow in checkedTable.Rows)
                {
                    if (ValidateContent(checkedRow, new string[] { "Accept", "Request", "RequestAgain" }))
                    {
                        DataRow filmReqRow = filmReqTable.NewRow();

                        //DateTime acceptDate = Convert.ToDateTime(checkedRow.GetString("ACCEPTDATE"));

                        filmReqRow["FILMSEQUENCE"] = checkedRow.GetString("FILMSEQUENCE");
                        filmReqRow["WAITREASON"] = checkedRow.GetString("WAITREASON");

                        filmReqRow["VALIDSTATE"] = "Valid";
                        filmReqRow["MODIFIER"] = UserInfo.Current.Id;
                        filmReqRow["_STATE_"] = "modified";

                        filmReqTable.Rows.Add(filmReqRow);
                    }
                    else
                    {
                        this.ShowMessage(MessageBoxButtons.OK, "FILMCANCELACEEPT", "");
                        return;//로직종료
                    }
                }

                filmReqSet.Tables.Add(filmReqTable);

                this.ExecuteRule<DataTable>("FilmRegistRequestMaking", filmReqSet);

                ControlEnableProcess("modified");

                Research();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
            finally
            {
                this.CloseWaitArea();
                btnRegist.Enabled = true;
                btnCancel.Enabled = true;
                btnReport.Enabled = true;
            }
        }
        #endregion

        #region ShowPrintData : 의뢰서를 발행전 출력
        private void ShowPrintData()
        {
            //저장 로직
            try
            {
                btnRegist.Enabled = false;
                btnCancel.Enabled = false;
                btnReport.Enabled = false;

                DataTable insertTable = grdMain.View.GetCheckedRows();

                if (insertTable.Rows.Count > 0)
                {
                    string sequenceParam = "";

                    foreach (DataRow insertRow in insertTable.Rows)
                    {
                        if (!ValidateSingleContent("Accept", insertRow))
                        {
                            ShowMessage(MessageBoxButtons.OK, "ValidateRequestToolStatusAcceptReport", "");
                            return;
                        }

                        if (sequenceParam.Equals(""))
                            sequenceParam += insertRow.GetString("FILMSEQUENCE");
                        else
                            sequenceParam += ", " + insertRow.GetString("FILMSEQUENCE");
                    }


                    Dictionary<string, object> values = Conditions.GetValues();
                    values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                    values.Add("FILMSEQUENCES", sequenceParam);

                    values = Commons.CommonFunction.ConvertParameter(values);
                    DataTable infoTable = SqlExecuter.Query("GetFilmInfoForRequestReportByTool", "10001", values);
                    if (infoTable != null)
                    {
                        Popup.PrintRequestFilmDocument filmDoc = new Popup.PrintRequestFilmDocument();
                        filmDoc.CurrentDataTable = infoTable;
                        filmDoc.SaveData += PrintData;
                        filmDoc.ShowDialog();
                    }
                    else
                    {
                        ShowMessage(MessageBoxButtons.OK, "ValidateRequestToolStatusSelectOne", "");
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
                btnRegist.Enabled = true;
                btnCancel.Enabled = true;
                btnReport.Enabled = true;
            }
        }
        #endregion

        #region PrintData : 의뢰서를 발행
        private void PrintData(DataRow insertRow)
        {
            //Validation 체크 부분 작성 필요
            this.ShowWaitArea();

            //저장 로직
            try
            {
                btnRegist.Enabled = false;
                btnCancel.Enabled = false;
                btnReport.Enabled = false;

                //Validation Check시 입력/수정시 검사할 값이 다르다면 수정필요
                if (ValidateSingleContent("Accept", insertRow))
                {
                    DataSet filmReqSet = new DataSet();
                    //치공구 제작의뢰를 입력
                    DataTable filmReqTable = CreatePrintDatatable();

                    DataRow filmReqRow = filmReqTable.NewRow();

                    DateTime printDate = DateTime.Now;

                    filmReqRow["FILMSEQUENCE"] = insertRow.GetString("FILMSEQUENCE");
                    filmReqRow["PRINTDATE"] = printDate.ToString("yyyy-MM-dd HH:mm:ss");
                    filmReqRow["PRINTUSER"] = UserInfo.Current.Id;

                    filmReqRow["VALIDSTATE"] = "Valid";
                    filmReqRow["MODIFIER"] = UserInfo.Current.Id;
                    filmReqRow["_STATE_"] = "added";

                    filmReqTable.Rows.Add(filmReqRow);

                    filmReqSet.Tables.Add(filmReqTable);

                    this.ExecuteRule<DataTable>("PrintFilmRequestMaking", filmReqSet);

                    //ControlEnableProcess("modified");

                    //Research();
                }
                else
                {
                    this.ShowMessage(MessageBoxButtons.OK, "ValidateFilmAcceptPrintStatus", "");
                }
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
            finally
            {
                this.CloseWaitArea();
                btnRegist.Enabled = true;
                btnCancel.Enabled = true;
                btnReport.Enabled = true;
            }
        }
        #endregion

        #region ChangeSiteCondition - 검색조건의 Site변경시 관련된 쿼리들을 변경
        private void ChangeSiteCondition()
        {
            if (segmentCondition != null)
                segmentCondition.Query = new SqlQuery("GetProcessSegmentByTool", "10001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "ENTERPRISEID", UserInfo.Current.Enterprise }, { "PLANTID", Conditions.GetValue("P_PLANTID") } });

            if (areaCondition != null)
                areaCondition.SearchQuery = new SqlQuery("GetAreaListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}");

            if (filmCodeCondition != null)
                filmCodeCondition.SearchQuery = new SqlQuery("GetFilmCodePopupListByTool", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}");

            if (vendorCondition != null)
                vendorCondition.SearchQuery = new SqlQuery("GetVendorListByTool", "10001", $"LANGUAGETYPE={ UserInfo.Current.LanguageType }", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}");

            //if (productPopup != null)
            //    productPopup.SearchQuery = new SqlQuery("GetProductdefidPoplistByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}", $"PRODUCTDEFTYPE=Product");

            //작업장 검색조건 초기화
            ((SmartSelectPopupEdit)Conditions.GetControl("AREANAME")).ClearValue();
        }
        #endregion
        #endregion
    }
}
