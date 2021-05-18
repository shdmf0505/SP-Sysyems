#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraReports.UI;

using System;
using Micube.SmartMES.Commons.Controls;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows.Forms;

#endregion

namespace Micube.SmartMES.ToolManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 치공구관리 > 필름관리 > 필름발행/출고등록
    /// 업  무  설  명  : 접수된 필름에 대하여 발행 및 출고등록을 진행한다.
    /// 생    성    자  : 김용조
    /// 생    성    일  : 2019-08-21
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class RegistSendFilm : SmartConditionManualBaseForm
    {
        #region Local Variables
        private string _searchAreaID = "";
        private string _searchVendorID = "";

        ConditionItemSelectPopup areaCondition;
        ConditionItemSelectPopup vendorCondition;
        ConditionItemSelectPopup filmCodeCondition;
        ConditionItemComboBox segmentCondition;
        #endregion

        #region 생성자

        public RegistSendFilm()
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
            InitializeGrid();
            InitializeLotGrid();

            ControlEnableProcess("");
        }

        #region InitializeGrid - 필름관리목록을 초기화한다.
        private void InitializeGrid()
        {
            grdFilmRequest.GridButtonItem = GridButtonItem.Export;
            grdFilmRequest.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdFilmRequest.View.AddTextBoxColumn("FILMSEQUENCE").SetIsHidden();                             //필름시퀀스
            grdFilmRequest.View.AddTextBoxColumn("FILMPROGRESSSTATUSID").SetIsHidden();                     //진행상태아이디
            grdFilmRequest.View.AddTextBoxColumn("ACCEPTUSERID").SetIsHidden();                             //접수자아이디
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
            grdFilmRequest.View.AddTextBoxColumn("RECEIPTAREAID").SetIsHidden();                            //입고작업장아이디
            grdFilmRequest.View.AddTextBoxColumn("RECEIVEDATE", 160).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsHidden(); //인수일시
            grdFilmRequest.View.AddTextBoxColumn("RECEIVEUSERID").SetIsHidden();                            //인수자아이디
            grdFilmRequest.View.AddTextBoxColumn("RECEIVEUSER", 100).SetIsHidden();                         //인수자
            grdFilmRequest.View.AddTextBoxColumn("REQUESTUSERID").SetIsHidden();                            //요청자아이디 
            grdFilmRequest.View.AddTextBoxColumn("REQUESTDATE", 160).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsHidden(); //요청일시
            grdFilmRequest.View.AddTextBoxColumn("RELEASEUSERID").SetIsHidden();                            //출고자아이디
            grdFilmRequest.View.AddTextBoxColumn("USEPLANDATE", 100).SetIsHidden();                         //사용예정일
            grdFilmRequest.View.AddTextBoxColumn("CHECKEDOPTION").SetIsHidden();                            //인수자
            grdFilmRequest.View.AddTextBoxColumn("FILMUSELAYER2", 120).SetIsHidden(); //Layer2
            grdFilmRequest.View.AddTextBoxColumn("CUSTOMER", 120).SetIsHidden();  //고객

            grdFilmRequest.View.AddTextBoxColumn("FILMPROGRESSSTATUS", 80).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();  //진행상태
            grdFilmRequest.View.AddTextBoxColumn("RECEPTDATE", 160).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly();    //접수일
            grdFilmRequest.View.AddTextBoxColumn("REQUESTDEPARTMENT", 100).SetIsReadOnly(); //의뢰부서ID
            grdFilmRequest.View.AddTextBoxColumn("REQUESTUSER", 80).SetIsReadOnly();    //요청자
            grdFilmRequest.View.AddTextBoxColumn("PRODUCTDEFID", 120).SetIsReadOnly();  //품목코드
            grdFilmRequest.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetIsReadOnly();  //내부 Rev
            grdFilmRequest.View.AddTextBoxColumn("PRODUCTDEFNAME", 280).SetIsReadOnly();    //품목명
            grdFilmRequest.View.AddTextBoxColumn("FILMVERSION", 120).SetIsReadOnly();   //CAM Rev.
            grdFilmRequest.View.AddTextBoxColumn("FILMNAME", 400).SetIsReadOnly();  //필름명
            grdFilmRequest.View.AddTextBoxColumn("CONTRACTIONX", 120).SetIsReadOnly();  //수축률X
            grdFilmRequest.View.AddTextBoxColumn("CONTRACTIONY", 120).SetIsReadOnly();  //수축률Y
            grdFilmRequest.View.AddTextBoxColumn("FILMCATEGORY", 120).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();   //필름유형1
            grdFilmRequest.View.AddTextBoxColumn("FILMDETAILCATEGORY", 150).SetTextAlignment(TextAlignment.Center).SetIsReadOnly(); //필름유형2
            grdFilmRequest.View.AddTextBoxColumn("FILMUSELAYER1", 100).SetIsReadOnly(); //Layer1
            grdFilmRequest.View.AddTextBoxColumn("QTY", 80).SetIsReadOnly();    //수량
            grdFilmRequest.View.AddTextBoxColumn("REQUESTCOMMENT", 200).SetIsReadOnly();    //의뢰사유
            grdFilmRequest.View.AddTextBoxColumn("RECEIPTAREA", 180).SetIsReadOnly();   //입고작업장
            grdFilmRequest.View.AddTextBoxColumn("JOBTYPE", 80).SetTextAlignment(TextAlignment.Center).SetIsReadOnly(); //작업구분
            grdFilmRequest.View.AddTextBoxColumn("PRODUCTIONTYPE", 80).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();  //생산구분
            grdFilmRequest.View.AddTextBoxColumn("FILMTYPE", 80).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();    //필름구분
            grdFilmRequest.View.AddTextBoxColumn("FILMCODE", 150).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();   //필름코드
            grdFilmRequest.View.AddTextBoxColumn("CHANGECONTRACTIONX", 120).SetIsReadOnly();    //요청수축률X(%)
            grdFilmRequest.View.AddTextBoxColumn("CHANGECONTRACTIONY", 120).SetIsReadOnly();    //요청수축률Y(%)
            grdFilmRequest.View.AddSpinEditColumn("MEASURECONTRACTIONX", 120).SetDisplayFormat("#,###.#####");  //실측수축률X
            grdFilmRequest.View.AddSpinEditColumn("MEASURECONTRACTIONY", 120).SetDisplayFormat("#,###.#####");  //실측수축률Y
            grdFilmRequest.View.AddTextBoxColumn("RESOLUTION", 80).SetIsReadOnly(); //해상도
            grdFilmRequest.View.AddTextBoxColumn("ISCOATING", 80).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();   //코팅유무
            grdFilmRequest.View.AddTextBoxColumn("PRIORITY", 80).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();    //우선순위
            grdFilmRequest.View.AddTextBoxColumn("ACCEPTDEPARTMENT", 100).SetIsReadOnly();  //접수부서
            grdFilmRequest.View.AddTextBoxColumn("ACCEPTMAINTWORKORDERUSER", 100).SetIsReadOnly();  //접수자
            grdFilmRequest.View.AddTextBoxColumn("RELEASEDEPARTMENT", 100).SetIsReadOnly(); //출고부서
            grdFilmRequest.View.AddTextBoxColumn("RELEASEUSER", 100).SetIsReadOnly();   //출고자
            grdFilmRequest.View.AddTextBoxColumn("RELEASEDATE", 160).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly();   //출고일시

            grdFilmRequest.View.PopulateColumns();
        }
        #endregion

        #region InitializeLotGrid - 필름Lot목록을 초기화한다.
        private void InitializeLotGrid()
        {
            grdLotList.GridButtonItem = GridButtonItem.Export;

            grdLotList.View.AddTextBoxColumn("FILMNO", 400);                              //FilmLotNo

            grdLotList.View.SetIsReadOnly();
            grdLotList.View.PopulateColumns();
        }
        #endregion
        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            btnPrintLabel.Click += BtnPrintLabel_Click;
            btnSave.Click += BtnSave_Click;

            grdFilmRequest.View.FocusedRowChanged += grdFilmRequest_FocusedRowChanged;
            //grdFilmRequest.View.CheckStateChanged += grdFilmRequest_CheckStateChanged;
            //grdFilmRequest.View.MouseUp += grdFilmRequest_MouseUp;
            grdFilmRequest.View.ShowingEditor += grdFilmRequest_ShowingEditor;

            Shown += RegistSendFilm_Shown;
        }

        #region grdFilmRequest_ShowingEditor - 체크되어있지 않은 항목에 대해 제어
        private void grdFilmRequest_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (!grdFilmRequest.View.IsRowChecked(grdFilmRequest.View.FocusedRowHandle))
                e.Cancel = true;

            //진행상태가 접수상태일때만 실측수축률이 입력가능하다.
            if(grdFilmRequest.View.FocusedColumn.FieldName.Equals("MEASURECONTRACTIONX") || grdFilmRequest.View.FocusedColumn.FieldName.Equals("MEASURECONTRACTIONY"))
                if (!grdFilmRequest.View.GetFocusedDataRow().GetString("FILMPROGRESSSTATUSID").Equals("Accept"))
                    e.Cancel = true;
        }
        #endregion

        #region RegistSendFilm_Shown - Site관련정보를 화면로딩후 설정한다.
        private void RegistSendFilm_Shown(object sender, EventArgs e)
        {
            ChangeSiteCondition();

            ((SmartComboBox)Conditions.GetControl("P_PLANTID")).EditValueChanged += (s, arg) => ChangeSiteCondition();
        }
        #endregion

        #region ConditionPlant_EditValueChanged - 검색조건의 Site정보 변경시 관련 쿼리들을 일괄 변경한다.
        private void ConditionPlant_EditValueChanged(object sender, EventArgs e)
        {
            ChangeSiteCondition();
        }
        #endregion

        #region grdFilmRequest_MouseUp - 그리드에 마우스커서가 위치했을때의 이벤트 (전체 체크항목에 대해 작업)
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
                        //접수상태만 전체선택에서 선택이 가능하다.
                        if (grdFilmRequest.View.GetDataRow(i).GetString("FILMPROGRESSSTATUSID").Equals("Accept"))
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

        #region grdFilmRequest_CheckStateChanged - 체크박스선택에 대해 제어
        private void grdFilmRequest_CheckStateChanged(object sender, EventArgs e)
        {
            int rowHandle = grdFilmRequest.View.FocusedRowHandle;

            if (!grdFilmRequest.View.GetDataRow(rowHandle).GetString("FILMPROGRESSSTATUSID").Equals("Accept"))
            {
                grdFilmRequest.View.CheckRow(rowHandle, false);
            }
        }
        #endregion

        #region grdFilmRequest_FocusedRowChanged - 필름그리드에 대해 행변경시 이벤트
        private void grdFilmRequest_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            SearchLotData();
        }
        #endregion

        #region BtnSave_Click - 저장버튼 이벤트
        private void BtnSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }
        #endregion

        private void BtnPrintLabel_Click(object sender, EventArgs e)
        {
            PrintData();
        }
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

            if (btn.Name.ToString().Equals("Publish"))
                BtnSave_Click(sender, e);
            else if (btn.Name.ToString().Equals("PrintLabel"))
                BtnPrintLabel_Click(sender, e);
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
            #endregion

            if (Conditions.GetValue("AREANAME").ToString() != "")
                values.Add("AREAID", _searchAreaID);

            //if (Conditions.GetValue("VENDORNAME").ToString() != "")
            //    values.Add("VENDORID", _searchVendorID);

            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable toolReqSearchResult = SqlExecuter.Query("GetSendPublishRequestMakingFilmListByTool", "10001", values);

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
        private void Research(string filmSequence)
        {
            // TODO : 조회 SP 변경
            InitializeInsertForm();
            // TODO : 조회 SP 변경
            var values = Conditions.GetValues();

            #region 기간 검색형 전환 처리 - 만약 시간까지 포함하여 검색한다면 아래 문구는 필요없음            
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            values.Add("USERID", UserInfo.Current.Id);
            #endregion

            if (Conditions.GetValue("AREANAME").ToString() != "")
                values.Add("AREAID", _searchAreaID);

            //if (Conditions.GetValue("VENDORNAME").ToString() != "")
            //    values.Add("VENDORID", _searchVendorID);

            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable toolReqSearchResult = SqlExecuter.Query("GetSendPublishRequestMakingFilmListByTool", "10001", values);

            grdFilmRequest.DataSource = toolReqSearchResult;

            
            if (toolReqSearchResult.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                grdLotList.DataSource = null;
            }
            else
            {
                if (GetRowHandleInGrid(grdFilmRequest, "FILMSEQUENCE", filmSequence) > -1)
                {
                    grdFilmRequest.View.FocusedRowHandle = GetRowHandleInGrid(grdFilmRequest, "FILMSEQUENCE", filmSequence);
                    SearchLotData();
                }
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
                                          selectedRows.Cast<DataRow>().ForEach(row =>
                                          {
                                              _searchAreaID = row.GetString("AREAID");
                                          });
                                      });

            areaCondition.Conditions.AddTextBox("AREANAME");

            areaCondition.GridColumns.AddTextBoxColumn("AREAID", 150).SetIsHidden();
            areaCondition.GridColumns.AddTextBoxColumn("AREANAME", 200);

            #endregion

            // 진행상태
            Conditions.AddComboBox("FILMPROGRESSSTATUS", new SqlQuery("GetFilmProgressStatusCodeListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"FILMSCREENSTATUS=PublishFilm"), "CODENAME", "CODEID")
                       .SetLabel("FILMPROGRESSSTATUS")
                       .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                       .SetPosition(3.4)
                       .SetEmptyItem("", "", true);
        }

        #endregion

        #region 유효성 검사

        protected override void OnValidateContent()
        {
            base.OnValidateContent();
        }

        #region ValidateContent - 내용점검 - 현재 행의 상태에 따른 점검
        private bool ValidateContent(string filmProgressStatus, out string messageCode)
        {
            messageCode = "";

            DataTable checkedTable = grdFilmRequest.View.GetCheckedRows();

            if (checkedTable.Rows.Count > 0)
            {
                foreach (DataRow checkedRow in checkedTable.Rows)
                {
                    if (!checkedRow.GetString("FILMPROGRESSSTATUSID").Equals(filmProgressStatus))
                    {
                        messageCode = "FILMREGISTACCEPT";
                        return false;
                    }

                    //수량이 없거나 0이면 DurableLot을 생성할 수 없으므로 작업을 수행할 수 없다.
                    if (checkedRow.GetString("QTY").Equals("") || checkedRow.GetString("QTY").Equals("0"))
                    {
                        messageCode = "FILMREGISTACCEPT";
                        return false;
                    }
                }
            }
            else
            {
                messageCode = "ValidateSelectedToolRequired";
                return false;
            }
            return true;
        }
        #endregion

        #region ValidateContent - 내용점검 - 2개의 상태에 따른 점검
        private bool ValidateContent(string filmProgressStatus, string otherFilmProgressStatus, out string messageCode)
        {
            messageCode = "";
            DataTable checkedTable = grdFilmRequest.View.GetCheckedRows();

            if (checkedTable.Rows.Count > 0)
            {
                foreach (DataRow checkedRow in checkedTable.Rows)
                {
                    if (!checkedRow.GetString("FILMPROGRESSSTATUSID").Equals(filmProgressStatus) && !checkedRow.GetString("FILMPROGRESSSTATUSID").Equals(otherFilmProgressStatus))
                    {
                        messageCode = "ValidateStatusIsReceiveAndRelease";
                        return false;
                    }
                }
            }
            else
            {
                messageCode = "ValidateSelectedToolRequired";
                return false;
            }
            return true;
        }
        #endregion

        #region ValidateComboBoxEqualValue - 2개의 콤보박스에 대한 데이터 점검
        private bool ValidateComboBoxEqualValue(SmartComboBox originBox, SmartComboBox targetBox)
        {
            //두 콤보박스의 값이 같으면 안된다.
            if (originBox.EditValue.Equals(targetBox.EditValue))
                return false;
            return true;
        }
        #endregion

        #region ValidateNumericBox - 숫자 텍스트박스에 대한 데이터 점검
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

        #region ValidateEditValue - 데이터값에 대한 점검
        private bool ValidateEditValue(object editValue)
        {
            if (editValue == null)
                return false;

            if (editValue.ToString() == "")
                return false;

            return true;
        }
        #endregion

        #region ValidateCellInGrid - 그리드내의 특정셀에 대한 점검
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
            dt.TableName = "filmPublishSendList";
            //===================================================================================
            dt.Columns.Add("FILMSEQUENCE");
            dt.Columns.Add("FILMCODE");
            dt.Columns.Add("FILMVERSION");
            dt.Columns.Add("RELEASEDATE");
            dt.Columns.Add("RELEASEUSERID");
            dt.Columns.Add("MEASURECONTRACTIONX");
            dt.Columns.Add("MEASURECONTRACTIONY");
            dt.Columns.Add("QTY");

            dt.Columns.Add("RESOLUTION");
            dt.Columns.Add("ISCOATING");
            dt.Columns.Add("RECEIPTAREAID");
            dt.Columns.Add("REQUESTCOMMENT");
            dt.Columns.Add("ENTERPRISEID");
            dt.Columns.Add("PLANTID");
            dt.Columns.Add("MAKEVENDORID");

            dt.Columns.Add("PRINTTIME");

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
                btnPrintLabel.Enabled = false;
                btnSave.Enabled = false;
                string lastSequence = "";
                string messageCode = "";
                //Accept상태만 저장이 가능하다.
                if (ValidateContent("Accept", out messageCode))
                {
                    DataSet filmReqSet = new DataSet();
                    //치공구 제작의뢰를 입력
                    DataTable filmReqTable = CreateSaveDatatable();


                    DataTable checkedTable = grdFilmRequest.View.GetCheckedRows();

                    foreach (DataRow checkedRow in checkedTable.Rows)
                    {
                        DataRow filmReqRow = filmReqTable.NewRow();

                        DateTime releaseDate = DateTime.Now;

                        lastSequence = checkedRow.GetString("FILMSEQUENCE");
                        filmReqRow["FILMSEQUENCE"] = checkedRow.GetString("FILMSEQUENCE");
                        filmReqRow["RELEASEDATE"] = releaseDate.ToString("yyyy-MM-dd HH:mm:ss");                        
                        filmReqRow["RELEASEUSERID"] = UserInfo.Current.Id;
                        filmReqRow["MEASURECONTRACTIONX"] = checkedRow.GetString("MEASURECONTRACTIONX");
                        filmReqRow["MEASURECONTRACTIONY"] = checkedRow.GetString("MEASURECONTRACTIONY");
                        filmReqRow["RESOLUTION"] = checkedRow.GetString("RESOLUTION");
                        filmReqRow["ISCOATING"] = checkedRow.GetString("ISCOATING");
                        filmReqRow["QTY"] = checkedRow.GetString("QTY");
                        filmReqRow["MAKEVENDORID"] = checkedRow.GetString("MAKEVENDORID");
                        filmReqRow["RECEIPTAREAID"] = checkedRow.GetString("RECEIPTAREAID");
                        filmReqRow["FILMCODE"] = checkedRow.GetString("FILMCODE");
                        filmReqRow["FILMVERSION"] = checkedRow.GetString("FILMVERSION");
                        filmReqRow["REQUESTCOMMENT"] = checkedRow.GetString("REQUESTCOMMENT");
                        filmReqRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                        filmReqRow["PLANTID"] = Conditions.GetValue("P_PLANTID");

                        filmReqRow["VALIDSTATE"] = "Valid";
                        filmReqRow["MODIFIER"] = UserInfo.Current.Id;
                        filmReqRow["CREATOR"] = UserInfo.Current.Id;
                        filmReqRow["_STATE_"] = "added";

                        filmReqTable.Rows.Add(filmReqRow);
                    }

                    filmReqSet.Tables.Add(filmReqTable);

                    this.ExecuteRule<DataTable>("FilmPublishSend", filmReqSet);

                    ControlEnableProcess("modified");

                    Research(lastSequence);
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
                btnPrintLabel.Enabled = true;
                btnSave.Enabled = true;
            }
        }
        #endregion

        #region PrintData : 라벨인쇄를 진행
        private void PrintData()
        {
            //Validation 체크 부분 작성 필요
            this.ShowWaitArea();

            //저장 로직
            try
            {
                btnSave.Enabled = false;
                btnPrintLabel.Enabled = false;
                string messageCode = "";
                //Validation Check시 입력/수정시 검사할 값이 다르다면 수정필요
                if (ValidateContent("Release", "Receive", out messageCode))
                {
                    DataSet filmReqSet = new DataSet();
                    //치공구 제작의뢰를 입력
                    DataTable filmReqTable = CreateSaveDatatable();

                    string lastSequence = "";
                    DataTable checkedTable = grdFilmRequest.View.GetCheckedRows();

                    foreach (DataRow checkedRow in checkedTable.Rows)
                    {
                        DataRow filmReqRow = filmReqTable.NewRow();

                        DateTime printDate = DateTime.Now;

                        lastSequence = checkedRow.GetString("FILMSEQUENCE"); 
                        filmReqRow["FILMSEQUENCE"] = checkedRow.GetString("FILMSEQUENCE");
                        filmReqRow["PRINTTIME"] = printDate.ToString("yyyy-MM-dd");
                        filmReqRow["VALIDSTATE"] = "Valid";
                        filmReqRow["MODIFIER"] = UserInfo.Current.Id;
                        filmReqRow["_STATE_"] = "modified";

                        filmReqTable.Rows.Add(filmReqRow);
                    }
                    filmReqSet.Tables.Add(filmReqTable);
                    this.ExecuteRule<DataTable>("FilmPublishSend", filmReqSet);

                    DisplayLabelReport(checkedTable);

                    ControlEnableProcess("modified");                    

                    Research(lastSequence);
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
                btnPrintLabel.Enabled = true;
                btnSave.Enabled = true;
            }
        }
        #endregion

        #region DisplayLabelReport
        private void DisplayLabelReport(DataTable checkedTable)
        {
            Assembly assembly = Assembly.GetAssembly(this.GetType());
            Stream stream = assembly.GetManifestResourceStream("Micube.SmartMES.ToolManagement.Report.filmLabel.repx");
            DataSet dsReport = new DataSet();

            DataTable header = new DataTable();
            header.Columns.Add(new DataColumn("LBLTITLE", typeof(string)));
            DataRow headerRow = header.NewRow();
            //headerRow["LBLTITLE"] =  Language.Get("RAWASSYIMPORTREPORTTITLE");
            header.Rows.Add(headerRow);


            dsReport.Tables.Add(header);
            dsReport.Tables.Add(checkedTable.Copy());

            XtraReport report = XtraReport.FromStream(stream);
            report.DataSource = dsReport;
            report.DataMember = dsReport.Tables[1].TableName;

            Band band = report.Bands["Detail"];
            SetReportControlDataBinding(band.Controls, dsReport.Tables[1]);
            setLabelLaungage(band);

            //report.Print();
            //report.PrintingSystem.EndPrint += PrintingSystem_EndPrint1; ;
            ReportPrintTool printTool = new ReportPrintTool(report);
            printTool.ShowRibbonPreview();
        }
        /// <summary>
        /// Report 파일의 컨트롤 중 Tag(FieldName) 값이 있는 컨트롤에 DataBinding(Text)를 추가한다.
        /// </summary>
        private void SetReportControlDataBinding(XRControlCollection controls, DataTable dataSource)
        {
            if (controls.Count > 0)
            {
                foreach (XRControl control in controls)
                {
                    if (!string.IsNullOrWhiteSpace(control.Tag.ToString()) && !(control.Name.Substring(0, 3).Equals("lbl")))
                        control.DataBindings.Add("Text", dataSource, control.Tag.ToString());

                    SetReportControlDataBinding(control.Controls, dataSource);
                }
            }
        }
        private void setLabelLaungage(object band)
        {
            if (band is DetailBand)
            {
                DetailBand detailband = band as DetailBand;
                //Band groupHeader = detailReport.Bands[strGroupHeader];

                foreach (XRControl control in detailband.Controls)
                {
                    if (control is DevExpress.XtraReports.UI.XRLabel)
                    {
                        if (!string.IsNullOrEmpty(control.Tag.ToString()))
                        {
                            if (control.Name.Substring(0, 3).Equals("lbl"))
                            {
                                string bindText = Language.Get(control.Tag.ToString());
                                Font ft = BestSizeEstimator.GetFontToFitBounds(control as XRLabel, bindText);
                                if (ft.Size < control.Font.Size)
                                {
                                    control.Font = ft;
                                }

                                control.Text = bindText;
                            }
                        }
                    }
                    else if (control is DevExpress.XtraReports.UI.XRTable)
                    {
                        XRTable xt = control as XRTable;

                        foreach (XRTableRow tr in xt.Rows)
                        {
                            for (int i = 0; i < tr.Cells.Count; i++)
                            {
                                if (!string.IsNullOrEmpty(tr.Cells[i].Tag.ToString()) && (tr.Cells[i].Name.Substring(0, 3).Equals("lbl")))
                                {
                                    tr.Cells[i].Text = Language.Get(tr.Cells[i].Tag.ToString());

                                }

                            }
                        }

                    }
                }

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
        private int GetRowHandleInGrid(SmartBandedGrid targetGrid, string firstColumnName, string firstFindValue)
        {
            for (int i = 0; i < targetGrid.View.RowCount; i++)
            {
                if (firstFindValue.Equals(targetGrid.View.GetDataRow(i)[firstColumnName].ToString()))
                {
                    return i;
                }
            }
            return -1;
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
