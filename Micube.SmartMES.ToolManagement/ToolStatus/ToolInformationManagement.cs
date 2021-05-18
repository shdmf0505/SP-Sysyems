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
    /// 프 로 그 램 명  : 치공구 정보관리
    /// 업  무  설  명  : 치공구관리 > 치공구 현황관리 > 치공구 정보관리
    /// 생    성    자  : 김용조
    /// 생    성    일  : 2019-08-16
    /// 수  정  이  력  : 
    ///        1. 2021.01.22 전우성 : 누적타수가 보증타수보다 크다면 붉은색 표시 및 화면 정리
    /// 
    /// </summary>
    public partial class ToolInformationManagement : SmartConditionManualBaseForm
    {
        #region Global Variables...

        ConditionItemSelectPopup areaCondition;
        #endregion

        #region 생성자
        public ToolInformationManagement()
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

        #region InitializeGrid - 치공구이동출고내역목록을 초기화한다.
        /// <summary>        
        /// 치공구내역목록을 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdToolInformationList.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Export | GridButtonItem.Restore;  // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기            
            grdToolInformationList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdToolInformationList.View.AddTextBoxColumn("TOOLNUMBER", 150)            //Tool번호
                .SetIsReadOnly(true);
            grdToolInformationList.View.AddTextBoxColumn("TOOLCODE", 120)
                .SetIsReadOnly(true);                                                  //Tool코드
            grdToolInformationList.View.AddTextBoxColumn("TOOLVERSION", 80)
                .SetIsReadOnly(true);                                                  //Tool Version
            grdToolInformationList.View.AddTextBoxColumn("TOOLNAME", 400)
                .SetIsReadOnly(true);                                                  //Tool Version
            grdToolInformationList.View.AddComboBoxColumn("PRODUCTIONTYPE", 90, new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CODECLASSID=YieldProductionType"))
                .SetIsReadOnly(true);
            grdToolInformationList.View.AddTextBoxColumn("PRODUCTDEFID", 120)
                .SetIsReadOnly(true);                                                  //품목코드
            grdToolInformationList.View.AddTextBoxColumn("PRODUCTDEFNAME", 280)
                .SetIsReadOnly(true);                                                  //품목명
            grdToolInformationList.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80)
                .SetIsReadOnly(true);                                                  //품목명
            grdToolInformationList.View.AddTextBoxColumn("AREAID")
                .SetIsHidden();                                                        //작업장아이디
            grdToolInformationList.View.AddTextBoxColumn("AREANAME", 180)
                .SetIsReadOnly(true);                                                  //작업장       
            grdToolInformationList.View.AddComboBoxColumn("TOOLFORMCODE", 150, new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CODECLASSID=ToolForm"), "CODENAME", "CODEID")
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(false);                                                  //상태 - (이 회면에서 입력해야 한다면 SetIsReadOnly를 풀어준다.
            grdToolInformationList.View.AddTextBoxColumn("TOOLCATEGORYID")
                .SetIsHidden();                                                        //Tool구분아이디
            grdToolInformationList.View.AddTextBoxColumn("TOOLCATEGORY", 120)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(true);                                                  //Tool구분
            grdToolInformationList.View.AddTextBoxColumn("TOOLCATEGORYDETAILID")
                .SetIsHidden();                                                        //Tool유형아이디
            grdToolInformationList.View.AddTextBoxColumn("TOOLCATEGORYDETAIL", 120)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(true);                                                  //Tool유형
            grdToolInformationList.View.AddTextBoxColumn("TOOLDETAILID")
                .SetIsHidden();                                                        //Tool유형아이디
            grdToolInformationList.View.AddTextBoxColumn("TOOLDETAIL", 150)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(true);                                                  //Tool유형
            grdToolInformationList.View.AddComboBoxColumn("DURABLESTATEID", 120, new SqlQuery("GetStateListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"STATEMODELID=DurableState"), "STATENAME", "STATEID" )
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(false);                                                 //상태
            grdToolInformationList.View.AddTextBoxColumn("DURABLESTATE")
                .SetIsHidden();                                                        //상태
            grdToolInformationList.View.AddCheckBoxColumn("ISHOLD", 60)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(false);                                                 //Hold여부
            grdToolInformationList.View.AddTextBoxColumn("DURABLECLEANSTATEID")
                .SetIsHidden();                                                        //연마상태아이디
            grdToolInformationList.View.AddTextBoxColumn("DURABLECLEANSTATE", 80)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(true);                                                  //연마상태
            grdToolInformationList.View.AddTextBoxColumn("USEDCOUNT", 60)                
                .SetIsReadOnly(true)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);                                               //사용타수
          
            grdToolInformationList.View.AddTextBoxColumn("TOTALUSEDCOUNT", 60)
                .SetIsReadOnly(true)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);                                                   //누적사용타수
            grdToolInformationList.View.AddTextBoxColumn("USEDLIMIT", 60)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);                                          //보증타수
            grdToolInformationList.View.AddTextBoxColumn("CLEANLIMIT", 60)
               .SetTextAlignment(TextAlignment.Right)
               .SetDisplayFormat("#,##0", MaskTypes.Numeric);                                                  //연마기준타수
            grdToolInformationList.View.AddTextBoxColumn("TOTALCLEANCOUNT", 60)
                .SetIsReadOnly(true)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);                                                   //연마횟수
            grdToolInformationList.View.AddTextBoxColumn("TOTALREPAIRCOUNT", 60)
                .SetIsReadOnly(true)
                  .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);                                                   //수리횟수
            grdToolInformationList.View.AddTextBoxColumn("WEIGHT", 80)
                .SetDisplayFormat("#,###.####")
                .SetTextAlignment(TextAlignment.Right)
                .SetIsReadOnly(false)
                .IsFloatValue = true;                                                  //무게
            grdToolInformationList.View.AddTextBoxColumn("HORIZONTAL", 80)
                .SetDisplayFormat("#,###.####")
                .SetTextAlignment(TextAlignment.Right)
                .SetIsReadOnly(false)
                .IsFloatValue = true;                                                  //가로
            grdToolInformationList.View.AddTextBoxColumn("VERTICAL", 80)
                .SetDisplayFormat("#,###.####")
                .SetTextAlignment(TextAlignment.Right)
                .SetIsReadOnly(false)
                .IsFloatValue = true;                                                  //세로
            grdToolInformationList.View.AddTextBoxColumn("THEIGHT", 80)
                .SetDisplayFormat("#,###.####")
                .SetTextAlignment(TextAlignment.Right)
                .SetIsReadOnly(false)
                .IsFloatValue = true;                                                  //높이
            grdToolInformationList.View.AddTextBoxColumn("USEDFACTOR")
                .SetIsReadOnly(true)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric); ;                                                  //의뢰자이이디
            grdToolInformationList.View.AddTextBoxColumn("POLISHTHICKNESS", 80)
                .SetDisplayFormat("#,###.####")
                .SetTextAlignment(TextAlignment.Right)
                .SetIsHidden()
                .IsFloatValue = true;                                                  //연마두께
            grdToolInformationList.View.AddTextBoxColumn("TOTALPOLISHTHICKNESS", 80)
                .SetDisplayFormat("#,###.####")
                .SetTextAlignment(TextAlignment.Right)
                .SetIsReadOnly(false)
                .IsFloatValue = true;                                                  //누적연마두께
            grdToolInformationList.View.AddTextBoxColumn("ORIGINTHICKNESS", 80)
                .SetDisplayFormat("#,###.####")
                .SetTextAlignment(TextAlignment.Right)
                .SetIsHidden()
                .IsFloatValue = true;                                                  //누적연마두께
            grdToolInformationList.View.AddTextBoxColumn("CREATEDTHICKNESS", 80)
                .SetDisplayFormat("#,###.####")
                .SetTextAlignment(TextAlignment.Right)
                .SetIsReadOnly(true)
                .IsFloatValue = true;                                                  //최초두께
            grdToolInformationList.View.AddTextBoxColumn("TOOLTHICKNESSLIMIT")
                .SetDisplayFormat("#,###.####")
                .SetTextAlignment(TextAlignment.Right)
                .SetIsReadOnly(true);                                                  //두께기준
            grdToolInformationList.View.AddTextBoxColumn("UPDATEREASON", 250)
                .SetIsReadOnly(false)
                .SetValidationIsRequired();                                            //의뢰자
            grdToolInformationList.View.AddCheckBoxColumn("ORIGINISHOLD", 150)
               .SetIsHidden();                                                        //변경여부파악용 ISHOLD
            grdToolInformationList.View.AddTextBoxColumn("ORIGINDURABLESTATEID", 150)
               .SetIsHidden();                                                        //변경여부파악용 DurableStateID    
            grdToolInformationList.View.AddTextBoxColumn("MSTCLEANLIMIT", 60)
               .SetTextAlignment(TextAlignment.Right)
               .SetDisplayFormat("#,##0", MaskTypes.Numeric)
               .SetIsHidden();                                          //보증타수
            grdToolInformationList.View.AddTextBoxColumn("MSTUSEDLIMIT", 60)
               .SetTextAlignment(TextAlignment.Right)
               .SetDisplayFormat("#,##0", MaskTypes.Numeric)
               .SetIsHidden();                                                  //연마기준타수
            grdToolInformationList.View.PopulateColumns();
        }
        #endregion
        #endregion

        #region Event
        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // 그리드의 입력제어
            grdToolInformationList.View.ShowingEditor += (s, e) =>
            {
                if (!grdToolInformationList.View.FocusedColumn.FieldName.Equals("ISHOLD") && !grdToolInformationList.View.FocusedColumn.FieldName.Equals("UPDATEREASON"))
                {
                    if (grdToolInformationList.View.GetFocusedDataRow().GetBoolean("ISHOLD"))
                    {
                        this.ShowMessage(MessageBoxButtons.OK, "ToolInformationPendingState", "");
                        e.Cancel = true;
                    }
                }
            };

            // 그리드의 행 변경이벤트
            grdToolInformationList.View.CellValueChanged += (s, e) =>
            {
                if (e.Column.FieldName.Equals("POLISHTHICKNESS"))
                {
                    grdToolInformationList.View.SetRowCellValue(e.RowHandle, "TOTALPOLISHTHICKNESS", grdToolInformationList.View.GetDataRow(e.RowHandle).GetInteger("ORIGINTHICKNESS") + Convert.ToInt32(e.Value));
                }
            };

            // 누적타수, 보증타수에 따른 색상 변경
            grdToolInformationList.View.RowCellStyle += (s, e) =>
            {
                if (e.RowHandle < 0)
                {
                    return;
                }

                int totalusedcnt = Format.GetInteger(grdToolInformationList.View.GetRowCellValue(e.RowHandle, "TOTALUSEDCOUNT"), int.MinValue);
                int usedLimit = Format.GetInteger(grdToolInformationList.View.GetRowCellValue(e.RowHandle, "USEDLIMIT"), int.MinValue);

                if (totalusedcnt.Equals(int.MinValue) || usedLimit.Equals(int.MinValue))
                {
                    return;
                }

                e.Appearance.ForeColor = totalusedcnt >= usedLimit ? Color.Red : e.Appearance.ForeColor;
            };

            // ToolBar 및 Site관련 설정을 화면 로딩후에 일괄 적용
            Shown += (s, e) =>
            {
                ChangeSiteCondition();

                //다중 Site 권한을 가진 사용자가 Site를 변경시 환경을 변경해줘야한다.
                ((SmartComboBox)Conditions.GetControl("P_PLANTID")).EditValueChanged += (sender, args) => ChangeSiteCondition();
            };
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

            if (btn.Name.ToString().Equals("Save"))
            {
                SaveData();
            }
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
            
            // TODO : 조회 SP 변경
            var values = Conditions.GetValues();

            #region 기간 검색형 전환 처리 - 만약 시간까지 포함하여 검색한다면 아래 문구는 필요없음            
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            #endregion

            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable toolRepairResult = SqlExecuter.Query("GetToolInformationListByTool", "10001", values);

            grdToolInformationList.DataSource = toolRepairResult;

            if (toolRepairResult.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
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
            //InitializePlant();
            InitializeDurableState();
            InitializeAreaPopup();
            InitializeToolCodePopup();
            InitializeIsHold();
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
        }

        #region Site설정
        /// <summary>
        /// site 설정 
        /// </summary>
        private void InitializePlant()
        {

            var planttxtbox = Conditions.AddComboBox("PLANTID", new SqlQuery("GetPlantList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PLANTID", "PLANTID")
               .SetLabel("PLANT")
               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
               .SetPosition(0.1)
               .SetIsReadOnly(true)
               .SetDefault(UserInfo.Current.Plant, "PLANTID") //기본값 설정 UserInfo.Current.Plant
            ;
        }
        #endregion

        #region DurableState설정
        /// <summary>
        /// site 설정 
        /// </summary>
        private void InitializeDurableState()
        {

            var planttxtbox = Conditions.AddComboBox("DURABLESTATE", new SqlQuery("GetStateListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"STATEMODELID=DurableState"), "STATENAME", "STATEID")
               .SetLabel("DURABLESTATE")
               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
               .SetPosition(0.4)
               .SetEmptyItem("", "", true)
            ;
        }
        #endregion

        #region IsHold설정
        /// <summary>
        /// site 설정 
        /// </summary>
        private void InitializeIsHold()
        {

            var planttxtbox = Conditions.AddComboBox("ISHOLD", new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CODECLASSID=YesNo"), "CODENAME", "CODEID")
               .SetLabel("ISHOLD")
               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
               .SetPosition(0.5)
               .SetEmptyItem("", "", true)
            ;
        }
        #endregion

        #region InitializeAreaPopup : 팝업창 제어
        /// <summary>
        /// 팝업창 제어
        /// </summary>
        private void InitializeAreaPopup()
        {
            areaCondition = Conditions.AddSelectPopup("AREAID", new SqlQuery("GetAreaListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "AREANAME", "AREAID")
            .SetPopupLayout("AREANAME", PopupButtonStyles.Ok_Cancel, true, false)
            .SetPopupLayoutForm(500, 600, FormBorderStyle.FixedToolWindow)
            .SetLabel("AREANAME")
            .SetPopupResultCount(1)
            //.SetValidationIsRequired()
            .SetPosition(0.2);

            // 팝업에서 사용할 조회조건 항목 추가
            areaCondition.Conditions.AddTextBox("AREANAME");

            // 팝업 그리드 설정
            areaCondition.GridColumns.AddTextBoxColumn("AREAID", 300)
                .SetIsHidden();
            areaCondition.GridColumns.AddTextBoxColumn("AREANAME", 300)
                .SetIsReadOnly();

        }
        #endregion

        #region 품목코드 설정
        /// <summary>
        /// 품목코드 조회 설정
        /// </summary>
        private void InitializeToolCodePopup()
        {
            ConditionItemSelectPopup productPopup = Conditions.AddSelectPopup("PRODUCTDEFID", new SqlQuery("GetProductdefidPoplistByTool", "10001", $"LANGUAGETYPE={ UserInfo.Current.LanguageType }", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"PRODUCTDEFTYPE=Product"))
            .SetPopupLayout("PRODUCTDEFIDPOPUP", PopupButtonStyles.Ok_Cancel, true, false)
            .SetPopupLayoutForm(984, 465, FormBorderStyle.FixedToolWindow)
            .SetPopupResultMapping("PRODUCTDEFID", "PRODUCTDEFID")
            .SetLabel("PRODUCTDEF")
            .SetPopupResultCount(1)
            .SetPosition(2.5)
            ;

            productPopup.ValueFieldName = "PRODUCTDEFID";
            productPopup.DisplayFieldName = "PRODUCTDEFNAME";

            productPopup.Conditions.AddTextBox("PRODUCTDEFID")
                .SetLabel("PRODUCTDEFID");

            // 팝업 조회조건
            //productPopup.Conditions.AddTextBox("PRODUCTDEFNAME")
            //    .SetLabel("PRODUCTDEFNAME");
            //productPopup.Conditions.AddComboBox("PRODUCTDEFTYPE", new SqlQuery("GetCodeListByOspProductdeftype", "10001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType } }), "CODENAME", "CODEID")
            //    .SetEmptyItem("", "", true)
            //    .SetLabel("PRODUCTDEFTYPE")
            //    .ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly
            //    ;

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

        #region ValidateContent - 내용점검
        private bool ValidateContent()
        {
            //각각의 상태에 따라 보여줄 메세지가 상이하다면 각각의 분기에서 메세지를 표현해야 한다.
           

            //체크된 값이 없으면 false
            if (grdToolInformationList.GetChangedRows().Rows.Count == 0)
                return false;
      
            DataTable updateTable = grdToolInformationList.GetChangedRows();

            foreach (DataRow updateRow in updateTable.Rows)
                if (updateRow["UPDATEREASON"].ToString().Equals(""))
                {
                    string lblConsumabledefid = grdToolInformationList.View.Columns["UPDATEREASON"].Caption.ToString();
                    this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblConsumabledefid); //메세지
                    return false;
                }
                 

            return true;
        }
        #endregion

        #region ValidateComboBoxEqualValue - 2개의 콤보박스에 대한 값 비교
        private bool ValidateComboBoxEqualValue(SmartComboBox originBox, SmartComboBox targetBox)
        {
            //두 콤보박스의 값이 같으면 안된다.
            if (originBox.EditValue.Equals(targetBox.EditValue))
                return false;
            return true;
        }
        #endregion

        #region ValidateNumeridBox - 숫자형 텍스트박스의 값 점검 및 기준숫자보다 높은지 점검
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

        #region ValidateEditValue - 입력된 값의 점검
        private bool ValidateEditValue(object editValue)
        {
            if (editValue == null)
                return false;

            if (editValue.ToString() == "")
                return false;

            return true;
        }
        #endregion

        #region ValidateCellInGrid - 그리드내의 특정컬럼에 대한 값 점검
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

        #region SetRequiredValidationControl - 필수입력항목
        private void SetRequiredValidationControl(Control requiredControl)
        {
            requiredControl.ForeColor = Color.Red;
        }
        #endregion

        #region ValidateProductCode : 품목코드와 관계된 치공구를 부르기 위해 품목코드가 입력되었는지 확인한다.
        private bool ValidateProductCode()
        {
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
                
            }
            else if (currentStatus == "modified") //
            {
                
            }
            else
            {
                
            }
        }
        #endregion

        #region CreateSaveDatatable : ToolMove 입력/수정/삭제를 위한 DataTable의 Template 생성
        /// <summary>
        /// ToolMove 입력/수정/삭제를 위한 DataTable의 Template 생성
        /// 입력시에는 ToolRequest와 ToolRequestDetail에 입력하고
        /// 수정시에는 ToolRequest와 ToolRequestDetail 및 ToolRequestDetailLot에 까지 데이터를 기입한다.
        /// </summary>
        /// <returns></returns>
        private DataTable CreateSaveDatatable(bool useState)
        {
            DataTable dt = new DataTable();
            dt.TableName = "toolInfoList";
            //===================================================================================
            dt.Columns.Add("TOOLNUMBER");
            dt.Columns.Add("TOOLCODE");
            dt.Columns.Add("TOOLVERSION");
            dt.Columns.Add("ISHOLD");
            dt.Columns.Add("DURABLESTATEID");
            dt.Columns.Add("WEIGHT");
            dt.Columns.Add("HORIZONTAL");
            dt.Columns.Add("VERTICAL");
            dt.Columns.Add("HEIGHT");
            dt.Columns.Add("THICKNESS");
            dt.Columns.Add("UPDATEREASON");
            dt.Columns.Add("TOOLFORMCODE");
            dt.Columns.Add("CLEANLIMIT");
            dt.Columns.Add("USEDLIMIT");
            dt.Columns.Add("ISHOLDYN");
            dt.Columns.Add("ISSTATEYN");

            dt.Columns.Add("LOTHISTKEY");

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

            if (useState)
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
                //Validation Check시 입력/수정시 검사할 값이 다르다면 수정필요
                if (grdToolInformationList.View.RowCount > 0)
                {
                    if (ValidateContent())
                    {
                        DataSet toolMoveSet = new DataSet();
                        //치공구 제작의뢰를 입력
                        DataTable toolInfoTable = CreateSaveDatatable(true);

                        DataTable infoTargetTable = grdToolInformationList.GetChangedRows();

                        foreach (DataRow infoTargetRow in infoTargetTable.Rows)
                        {
                            DataRow toolInfoRow = toolInfoTable.NewRow();

                            toolInfoRow["TOOLNUMBER"] = infoTargetRow.GetString("TOOLNUMBER");

                            if (infoTargetRow.GetBoolean("ISHOLD").Equals(infoTargetRow.GetBoolean("ORIGINISHOLD")))
                                toolInfoRow["ISHOLDYN"] = "N";
                            else
                                toolInfoRow["ISHOLDYN"] = "Y";
                            toolInfoRow["ISHOLD"] = infoTargetRow.GetBoolean("ISHOLD") ? "Y" : "N";

                            if (infoTargetRow.GetString("DURABLESTATEID").Equals(infoTargetRow.GetString("ORIGINDURABLESTATEID")))
                                toolInfoRow["ISSTATEYN"] = "N";
                            else
                                toolInfoRow["ISSTATEYN"] = "Y";
                            toolInfoRow["DURABLESTATEID"] = infoTargetRow.GetString("DURABLESTATEID");
                            toolInfoRow["TOOLFORMCODE"] = infoTargetRow.GetString("TOOLFORMCODE");
                            toolInfoRow["WEIGHT"] = infoTargetRow.GetString("WEIGHT");
                            toolInfoRow["HEIGHT"] = infoTargetRow.GetString("THEIGHT");
                            toolInfoRow["HORIZONTAL"] = infoTargetRow.GetString("HORIZONTAL");
                            toolInfoRow["VERTICAL"] = infoTargetRow.GetString("VERTICAL");
                            toolInfoRow["THICKNESS"] = infoTargetRow.GetString("TOTALPOLISHTHICKNESS");
                            toolInfoRow["UPDATEREASON"] = infoTargetRow.GetString("UPDATEREASON");
                            if (!(infoTargetRow.GetString("CLEANLIMIT").Equals(infoTargetRow.GetString("MSTCLEANLIMIT"))))
                            {
                                toolInfoRow["CLEANLIMIT"] = infoTargetRow.GetString("CLEANLIMIT");
                            }
                            else
                            {
                                toolInfoRow["CLEANLIMIT"] = "0";
                               
                            }
                            if (!(infoTargetRow.GetString("USEDLIMIT").Equals(infoTargetRow.GetString("MSTUSEDLIMIT"))))
                            {
                                toolInfoRow["USEDLIMIT"] = infoTargetRow.GetString("USEDLIMIT");
                            }
                            else
                            {
                                toolInfoRow["USEDLIMIT"] = "0";

                            }
                           
                            //DurableDefinition
                            toolInfoRow["TOOLCODE"] = infoTargetRow.GetString("TOOLCODE");
                            toolInfoRow["TOOLVERSION"] = infoTargetRow.GetString("TOOLVERSION");

                            toolInfoRow["CREATOR"] = UserInfo.Current.Id;
                            toolInfoRow["MODIFIER"] = UserInfo.Current.Id;

                            //로직상 무조건 추가만 진행된다.
                            toolInfoRow["_STATE_"] = "modified"; //무조건 수정만 진행한다.
                            toolInfoRow["VALIDSTATE"] = "Valid";

                            toolInfoTable.Rows.Add(toolInfoRow);
                        }

                        toolMoveSet.Tables.Add(toolInfoTable);

                        this.ExecuteRule<DataTable>("ToolInformation", toolMoveSet);

                        ControlEnableProcess("modified");

                        ////가져온 결과값에 따라 재검색한 후 가져온 아이디에 맞는 행을 선택으로 변경한다.
                        SendKeys.Send("{F5}");
                    }
                    //else
                    //{
                    //    this.ShowMessage(MessageBoxButtons.OK, "ToolDetailValidation", "");
                    //}
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
            }
        }
        #endregion

        #region ChangeSiteCondition - 검색조건의 Site변경시 관련된 쿼리들을 변경
        private void ChangeSiteCondition()
        {
            if (areaCondition != null)
                areaCondition.SearchQuery = new SqlQuery("GetAreaIDListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}");

            //화면초기화
            //InitializeInsertForm();
            //작업장 검색조건 초기화
            ((SmartSelectPopupEdit)Conditions.GetControl("AREAID")).ClearValue();//VENDORNAME
        }
        #endregion

        #endregion
    }
}
