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
    /// 프 로 그 램 명  : 치공구관리 > 필름관리 > 필름폐기등록
    /// 업  무  설  명  : 사용종료된 필름에 대하여 폐기를 진행한다.
    /// 생    성    자  : 김용조
    /// 생    성    일  : 2019-08-21
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class RegistScrapFilm : SmartConditionManualBaseForm
    {
        #region Local Variables
        string _searchAreaID;

        ConditionItemSelectPopup areaCondition;
        ConditionItemSelectPopup filmCodeCondition;
        ConditionItemComboBox segmentCondition;
        #endregion

        #region 생성자

        public RegistScrapFilm()
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

        #region InitializeGrid : 폐기등록 리스트를 초기화한다.
        /// <summary>        
        /// 폐기등록 리스트를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            grdFilmRequest.GridButtonItem = GridButtonItem.Export;
            grdFilmRequest.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdFilmRequest.View.AddTextBoxColumn("DURABLESTATEID").SetIsHidden();                           //상태아이디
            grdFilmRequest.View.AddTextBoxColumn("AREAID", 10).SetIsHidden();                               //작업장아이디
            grdFilmRequest.View.AddTextBoxColumn("ISMODIFY", 10).SetIsHidden();                             //작업장권한
            grdFilmRequest.View.AddTextBoxColumn("JOBTYPEID").SetIsHidden();                                //작업구분아이디
            grdFilmRequest.View.AddTextBoxColumn("PRODUCTIONTYPEID").SetIsHidden();                         //생산구분아이디
            grdFilmRequest.View.AddTextBoxColumn("FILMTYPEID").SetIsHidden();                               //필름구분아이디
            grdFilmRequest.View.AddTextBoxColumn("FILMCATEGORYID").SetIsHidden();                           //필름유형1아이디
            grdFilmRequest.View.AddTextBoxColumn("FILMDETAILCATEGORYID").SetIsHidden();                     //필름유형2아이디
            grdFilmRequest.View.AddTextBoxColumn("CUSTOMERID").SetIsHidden();                               //고객아이디
            grdFilmRequest.View.AddTextBoxColumn("MAKEVENDORID").SetIsHidden();                             //제작업체아이디
            grdFilmRequest.View.AddTextBoxColumn("MAKEVENDOR", 150).SetIsHidden();                          //제작업체
            grdFilmRequest.View.AddTextBoxColumn("FILMUSELAYER2", 120).SetIsHidden();                 //Layer2
            grdFilmRequest.View.AddTextBoxColumn("CUSTOMER", 150).SetIsHidden();                      //고객

            grdFilmRequest.View.AddTextBoxColumn("DURABLESTATE", 80).SetIsReadOnly();                   //상태  
            grdFilmRequest.View.AddTextBoxColumn("ISHOLD", 60).SetIsReadOnly();                          //보류 여부
            grdFilmRequest.View.AddTextBoxColumn("FILMNO", 180).SetIsReadOnly();                        //필름번호    
            grdFilmRequest.View.AddTextBoxColumn("FILMCODE", 150).SetIsReadOnly();                      //필름코드                                           
            grdFilmRequest.View.AddTextBoxColumn("FILMVERSION", 80).SetIsReadOnly();                    //CAM Rev.
            grdFilmRequest.View.AddTextBoxColumn("FILMNAME", 350).SetIsReadOnly();                      //필름명
            grdFilmRequest.View.AddTextBoxColumn("AREA", 150).SetIsReadOnly();                          //작업장
            grdFilmRequest.View.AddTextBoxColumn("SCRAPCOMMENT", 200).SetValidationIsRequired();            //폐기사유
            grdFilmRequest.View.AddTextBoxColumn("PRODUCTDEFID", 120).SetIsReadOnly();                  //품목코드
            grdFilmRequest.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetIsReadOnly();              //내부 Rev
            grdFilmRequest.View.AddTextBoxColumn("PRODUCTDEFNAME", 250).SetIsReadOnly();                //품목명
            grdFilmRequest.View.AddTextBoxColumn("FILMCATEGORY", 120).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();   //필름유형1
            grdFilmRequest.View.AddTextBoxColumn("FILMDETAILCATEGORY", 150).SetTextAlignment(TextAlignment.Center).SetIsReadOnly(); //필름유형2
            grdFilmRequest.View.AddTextBoxColumn("FILMUSELAYER1", 100).SetIsReadOnly();                 //Layer1
            grdFilmRequest.View.AddTextBoxColumn("CONTRACTIONX", 120).SetIsReadOnly();                   //수출률X
            grdFilmRequest.View.AddTextBoxColumn("CONTRACTIONY", 120).SetIsReadOnly();                   //수출률Y
            grdFilmRequest.View.AddTextBoxColumn("QTY", 80).SetIsReadOnly();                            //수량
            grdFilmRequest.View.AddTextBoxColumn("JOBTYPE", 80).SetTextAlignment(TextAlignment.Center).SetIsReadOnly(); //작업구분
            grdFilmRequest.View.AddTextBoxColumn("PRODUCTIONTYPE", 80).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();  //생산구분
            grdFilmRequest.View.AddTextBoxColumn("FILMTYPE", 80).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();    //필름구분
            grdFilmRequest.View.AddTextBoxColumn("RESOLUTION", 80).SetIsReadOnly();                     //해상도
            grdFilmRequest.View.AddTextBoxColumn("ISCOATING", 80).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();   //코팅유무
            grdFilmRequest.View.AddTextBoxColumn("CHANGECONTRACTIONX", 120).SetIsReadOnly();            //요청수출률X(%)
            grdFilmRequest.View.AddTextBoxColumn("CHANGECONTRACTIONY", 120).SetIsReadOnly();            //요청수출률Y(%)
            grdFilmRequest.View.AddSpinEditColumn("MEASURECONTRACTIONX", 120).SetDisplayFormat("#,###.#####").SetIsReadOnly();  //실측수출률X
            grdFilmRequest.View.AddSpinEditColumn("MEASURECONTRACTIONY", 120).SetDisplayFormat("#,###.#####").SetIsReadOnly();  //실측수출률Y            
            
            grdFilmRequest.View.PopulateColumns();
        }
        #endregion

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            btnSave.Click += BtnSave_Click;
            Shown += RegistScrapFilm_Shown;
            grdFilmRequest.View.ShowingEditor += grdFilmRequest_ShowingEditor;
        }

        private void grdFilmRequest_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (!grdFilmRequest.View.IsRowChecked(grdFilmRequest.View.FocusedRowHandle))
                e.Cancel = true;
        }

        #region RegistScrapFilm_Shown - Site관련정보를 화면로딩후 설정한다.
        private void RegistScrapFilm_Shown(object sender, EventArgs e)
        {
            ChangeSiteCondition();

            ((SmartComboBox)Conditions.GetControl("P_PLANTID")).EditValueChanged += (s, arg) => ChangeSiteCondition();
        }
        #endregion

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }


        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            //SaveData();
        }

        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            //base.OnToolbarClick(sender, e);
            SmartButton btn = sender as SmartButton;

            if (btn.Name.ToString().Equals("Scrap"))
                BtnSave_Click(sender, e);
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
            values.Add("USERID", UserInfo.Current.Id);
            values.Add("CURRENTLOGINID", UserInfo.Current.Id);
            #endregion

            if (Conditions.GetValue("AREANAME").ToString() != "")
                values.Add("AREAID", _searchAreaID);

            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable toolReqSearchResult = SqlExecuter.Query("GetScrapRequestMakingFilmListByTool", "10001", values);

            grdFilmRequest.DataSource = toolReqSearchResult;

            if (toolReqSearchResult.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }
        }

        /// <summary>
        /// 재검색
        /// </summary>
        private void Research()
        {
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

            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable toolReqSearchResult = SqlExecuter.Query("GetScrapRequestMakingFilmListByTool", "10001", values);

            grdFilmRequest.DataSource = toolReqSearchResult;

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

            areaCondition = Conditions.AddSelectPopup("AREANAME", new SqlQuery("GetAreaListByTool", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CURRENTLOGINID={UserInfo.Current.Id}"))
                                      .SetPopupLayout("AREA", PopupButtonStyles.Ok_Cancel, true, true)
                                      .SetPopupLayoutForm(600, 800, FormBorderStyle.FixedToolWindow)
                                      .SetPopupAutoFillColumns("AREANAME")
                                      .SetLabel("AREA")
                                      .SetPopupResultCount(1)
                                      .SetPosition(3.4)
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

        #region ValidateContent - 상태에 따른 Validation을 진행한다.
        private bool ValidateContent(string filmProgressStatus, out string messageCode)
        {
            messageCode = "";
            DataTable checkedTable = grdFilmRequest.View.GetCheckedRows();
            foreach (DataRow checkedRow in checkedTable.Rows)
            {
                if (checkedRow.GetString("SCRAPCOMMENT").Equals(""))
                {
                    messageCode = "FILMREGISTSCRAP";
                    return false;
                }

                if (checkedRow.GetString("ISMODIFY").Equals("N"))
                {
                    messageCode = "ValidateFilmScrapReqArea"; //폐기할 필름중 작업장권한이 없는 필름이 있습니다.
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region ValidateComboBoxEqulValue - 2개의 콤보박스의 값이 동일한지 검사
        private bool ValidateComboBoxEqualValue(SmartComboBox originBox, SmartComboBox targetBox)
        {
            //두 콤보박스의 값이 같으면 안된다.
            if (originBox.EditValue.Equals(targetBox.EditValue))
                return false;
            return true;
        }
        #endregion

        #region ValidateNumericBox - 숫자형 텍스트박스가 기준숫자보다 낮은지 검사
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

        #region ValidateEditValue - 특정 값에 대한 Validation
        private bool ValidateEditValue(object editValue)
        {
            if (editValue == null)
                return false;

            if (editValue.ToString() == "")
                return false;

            return true;
        }
        #endregion

        #region ValidationCellInGrid - 그리드내의 특정 컬럼에 대한 Validation
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
            dt.TableName = "filmScrapList";
            //===================================================================================
            dt.Columns.Add("FILMNO");
            dt.Columns.Add("SCRAPDATE");
            dt.Columns.Add("SCRAPUSERID");
            dt.Columns.Add("REASONCOMMENT");

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
                if (ValidateContent("", out messageCode))
                {
                    DataSet filmReqSet = new DataSet();
                    //치공구 제작의뢰를 입력
                    DataTable filmReqTable = CreateSaveDatatable();


                    DataTable checkedTable = grdFilmRequest.View.GetCheckedRows();

                    foreach (DataRow checkedRow in checkedTable.Rows)
                    {
                        DataRow filmReqRow = filmReqTable.NewRow();

                        DateTime scrapDate = DateTime.Now;

                        filmReqRow["FILMNO"] = checkedRow.GetString("FILMNO");
                        filmReqRow["SCRAPDATE"] = scrapDate.ToString("yyyy-MM-dd HH:mm:ss");
                        filmReqRow["SCRAPUSERID"] = UserInfo.Current.Id;
                        filmReqRow["REASONCOMMENT"] = checkedRow.GetString("SCRAPCOMMENT");

                        filmReqRow["VALIDSTATE"] = "Valid";
                        filmReqRow["MODIFIER"] = UserInfo.Current.Id;
                        filmReqRow["CREATOR"] = UserInfo.Current.Id;
                        filmReqRow["_STATE_"] = "modified";

                        filmReqTable.Rows.Add(filmReqRow);
                    }

                    filmReqSet.Tables.Add(filmReqTable);

                    this.ExecuteRule<DataTable>("FilmRegistScrap", filmReqSet);

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
            
            if (filmCodeCondition != null)
                filmCodeCondition.SearchQuery = new SqlQuery("GetFilmCodePopupListByTool", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CURRENTLOGINID={UserInfo.Current.Id}");

            //if (productPopup != null)
            //    productPopup.SearchQuery = new SqlQuery("GetProductdefidPoplistByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}", $"PRODUCTDEFTYPE=Product");

            //작업장 검색조건 초기화
            ((SmartSelectPopupEdit)Conditions.GetControl("AREANAME")).ClearValue();
        }
        #endregion
        #endregion
    }
}
