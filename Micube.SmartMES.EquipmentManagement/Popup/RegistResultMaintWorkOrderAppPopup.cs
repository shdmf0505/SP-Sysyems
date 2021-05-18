#region using
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Validations;

using Micube.SmartMES.Commons.Controls;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
#endregion

namespace Micube.SmartMES.EquipmentManagement.Popup
{
    /// <summary>
    /// 프 로 그 램 명  : 설비PM실적등록팝업
    /// 업  무  설  명  : 설비PM의 실적을 등록하는 팝업
    /// 생    성    자  : 김용조
    /// 생    성    일  : 2019-09-16
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class RegistResultMaintWorkOrderAppPopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region Local Variables
        public DataRow CurrentDataRow { get; set; }
        private string _workOrderID;
        private string _workOrderStatusID;
        private string _workOrderTypeID;
        private string _areaID;
        private string _repairUserID;
        private string _processSegment;
        private string _maintItemID;
        private string _currentStatus;
        private string _equipmentClassID;
        private string _plantID;
        private string _isModify;
        private string _factoryid;
        public delegate void reSearchEvent();
        public event reSearchEvent SearchHandler;

        public delegate void reBindhEvent(string workOrderID, string workOrderStatus);
        public event reBindhEvent BindHandler;

        ConditionItemSelectPopup popupGridSparepart;
        #endregion

        #region 생성자
        public RegistResultMaintWorkOrderAppPopup(string workOrderID, string plantID, string isModify,string factoryid) :this()
        {
            _workOrderID = workOrderID;
            _plantID = plantID;
            _isModify = isModify;
            _factoryid = factoryid;
        }

        private RegistResultMaintWorkOrderAppPopup()
        {
            InitializeComponent();
            InitializeEvent();
            InitializeContent();
        }
        #endregion

        #region  이벤트
        private void InitializeEvent()
        {
            Shown += RegistResultMaintWorkOrderProductPopup_Shown;
            btnClose.Click += BtnClose_Click;
            btnSave.Click += BtnSave_Click;
            btnOmissionPM.Click += BtnOmissionPM_Click;
            btnAddAttachment.Click += BtnAddAttachment_Click;
            btnDownloadTemplate.Click += BtnDownloadTemplate_Click;
            btnPrint.Click += BtnPrint_Click;
            grdPartItemList.View.AddingNewRow += grdPartItemList_AddingNewRow;
            grdPMList.View.ShowingEditor += grdPMList_ShowingEditor;
        }
        private void grdPartItemList_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {

            grdPartItemList.View.SetFocusedRowCellValue("FACTORYID", _factoryid);// 회사코드
            grdPartItemList.View.SetFocusedRowCellValue("OLDQTY", "0");
        }
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            Popup.PrintEquipmentDetailInfo printInfo = new PrintEquipmentDetailInfo();

            printInfo.CurrentDataRow = CurrentDataRow;
            if(grdPMList.DataSource != null)
                printInfo.InfoList = (DataTable)grdPMList.DataSource;

            printInfo.ShowDialog();
        }

        private void grdPMList_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (_currentStatus == "Create") //생성된 상태
            {
                ;
            }
            else if (_currentStatus == "Start") //
            {
                ;
            }
            else if (_currentStatus == "Omit") //
            {
                e.Cancel = true;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void BtnDownloadTemplate_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BtnAddAttachment_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BtnOmissionPM_Click(object sender, EventArgs e)
        {
            string messageCode = "";
            if (ValidateContent("Omit", out messageCode))
                DisplayOmitScreen();
            else
                ShowMessage(MessageBoxButtons.OK, messageCode, "");
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SaveData();            
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void RegistResultMaintWorkOrderProductPopup_Shown(object sender, EventArgs e)
        {
            //화면 갱신후 서버로부터 데이터를 가져온다.
            Search();

            popupGridSparepart.SearchQuery = new SqlQuery("GetSparePartStockListForPMByEqp", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={_plantID}", $"FACTORYID={_factoryid}");

            if (_isModify.Equals("Y"))
            {
                btnSave.Enabled = true;
                btnOmissionPM.Enabled = true;
            }
            else
            {
                btnSave.Enabled = false;
                btnOmissionPM.Enabled = false;
            }
        }

        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 화면의 컨텐츠 영역을 초기화한다.
        /// </summary>
        private void InitializeContent()
        {
            SetMaintItemGrid();
            SetSparePartGrid();
        }

        #region SetMaintItemGrid : 점검결과입력
        private void SetMaintItemGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdPMList.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Export | GridButtonItem.Restore;  // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기      

            grdPMList.View.AddTextBoxColumn("MAINTITEMID", 60)                 //아이디
                .SetIsHidden();
            grdPMList.View.AddTextBoxColumn("MAINTPOSITION", 80)               //점검부위
                .SetIsReadOnly(true);
            grdPMList.View.AddTextBoxColumn("MAINTDETAILITEM", 230)             //점검항목
                .SetIsReadOnly(true);
            grdPMList.View.AddTextBoxColumn("MAINTMETHOD", 120)                 //점검방법
                .SetIsReadOnly(true);
            grdPMList.View.AddTextBoxColumn("MAINTRESULT", 80)                 //점검결과
                ;
            grdPMList.View.AddTextBoxColumn("ACTIONDESCRIPTION", 130)           //조치내용
                ;

            grdPMList.View.PopulateColumns();
        }
        #endregion

        #region SetSparePartGrid : 부품사용
        private void SetSparePartGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdPartItemList.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export;  // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기      

            //grdPartItemList.View.AddTextBoxColumn("SPAREPARTID", 180)                 //아이디
            //    .SetValidationIsRequired();
            InitializeSparePartPopupColumnInGrid();
            grdPartItemList.View.AddTextBoxColumn("SPAREPARTNAME", 200)               //점검부위
                .SetIsReadOnly(true);
            grdPartItemList.View.AddTextBoxColumn("CURRENTQTY", 60)           //조치내용
                .SetTextAlignment(TextAlignment.Right)
                .SetIsReadOnly(true);
            grdPartItemList.View.AddSpinEditColumn("QTY", 60)
               .SetDisplayFormat("#,##0", MaskTypes.Numeric)
               .SetValidationIsRequired();
            //grdPartItemList.View.AddTextBoxColumn("PARENTDURABLECLASSID", 100)               //점검부위
            //    .SetIsHidden();
            //grdPartItemList.View.AddTextBoxColumn("PARENTDURABLECLASSNAME", 150)             //점검항목
            //    .SetTextAlignment(TextAlignment.Center)
            //    .SetIsReadOnly(true);
            //grdPartItemList.View.AddTextBoxColumn("DURABLECLASSID", 150)                 //점검방법
            //    .SetIsHidden();
            //grdPartItemList.View.AddTextBoxColumn("DURABLECLASSNAME", 150)                 //점검결과
            //    .SetTextAlignment(TextAlignment.Center)
            //    .SetIsReadOnly(true);
            grdPartItemList.View.AddTextBoxColumn("MODELNAME", 120)           //조치내용
                .SetIsReadOnly(true);
            grdPartItemList.View.AddTextBoxColumn("MAKER", 100)           //조치내용
                .SetIsReadOnly(true);
            grdPartItemList.View.AddTextBoxColumn("SPEC", 100)           //조치내용
                .SetIsReadOnly(true);
            grdPartItemList.View.AddTextBoxColumn("MODEL", 200)           //조치내용
                .SetIsHidden();
            grdPartItemList.View.AddSpinEditColumn("OLDQTY", 60)
               .SetDisplayFormat("#,##0", MaskTypes.Numeric)
               .SetIsHidden();
            grdPartItemList.View.AddTextBoxColumn("FACTORYID", 100)               //점검부위
                .SetIsHidden();
           
            grdPartItemList.View.PopulateColumns();
        }
        #endregion

        #region InitializeSparePartPopupColumnInGrid - 부품사용목록의 팝업창
        /// <summary>
        /// grid ProcesssegmentidLis
        /// </summary>
        private void InitializeSparePartPopupColumnInGrid()
        {
            popupGridSparepart = grdPartItemList.View.AddSelectPopupColumn("SPAREPARTID", 150, new SqlQuery("GetSparePartStockListForPMByEqp", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"FACTORYID={_factoryid}"))
                // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
                .SetPopupLayout("SPAREPARTID", PopupButtonStyles.Ok_Cancel, true, false)
                // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
                .SetPopupResultCount(1)
                //필수값설정
                .SetValidationIsRequired()
                // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
                .SetPopupResultMapping("SPAREPARTID", "SPAREPARTID")
                // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
                .SetPopupLayoutForm(900, 400, FormBorderStyle.FixedToolWindow)
                // 그리드의 남은 영역을 채울 컬럼 설정                
                //.SetPopupAutoFillColumns("SPAREPARTID")
                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {
                    foreach (DataRow row in selectedRows)
                    {
                        grdPartItemList.View.GetFocusedDataRow()["SPAREPARTID"] = row["SPAREPARTID"];
                        grdPartItemList.View.GetFocusedDataRow()["SPAREPARTNAME"] = row["SPAREPARTNAME"];
                        //grdPartItemList.View.GetFocusedDataRow()["PARENTDURABLECLASSID"] = row["PARENTDURABLECLASSID"];
                        //grdPartItemList.View.GetFocusedDataRow()["PARENTDURABLECLASSNAME"] = row["PARENTDURABLECLASSNAME"];
                        //grdPartItemList.View.GetFocusedDataRow()["DURABLECLASSID"] = row["DURABLECLASSID"];
                        //grdPartItemList.View.GetFocusedDataRow()["DURABLECLASSNAME"] = row["DURABLECLASSNAME"];
                        grdPartItemList.View.GetFocusedDataRow()["MODELNAME"] = row["MODELNAME"];
                        grdPartItemList.View.GetFocusedDataRow()["MAKER"] = row["MAKER"];
                        grdPartItemList.View.GetFocusedDataRow()["SPEC"] = row["SPEC"];
                        grdPartItemList.View.GetFocusedDataRow()["CURRENTQTY"] = row["QTY"];
                        grdPartItemList.View.GetFocusedDataRow()["FACTORYID"] = row["FACTORYID"];
                    }
                })
            //.SetPopupValidationCustom(ValidationParentAreaIdPopup);
            ;
            // 팝업에서 사용할 조회조건 항목 추가
            //popupGridSparepart.Conditions.AddComboBox("FACTORYID", new SqlQuery("GetFactoryListByEqp", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "FACTORYNAME", "FACTORYID")
            //    ;

            popupGridSparepart.Conditions.AddTextBox("SPAREPARTNAME");
            popupGridSparepart.Conditions.AddTextBox("SPAREPARTID");
            popupGridSparepart.Conditions.AddTextBox("MODELNAME");
            // 팝업 그리드 설정
            popupGridSparepart.GridColumns.AddTextBoxColumn("SPAREPARTID", 80)
                .SetIsReadOnly();
            popupGridSparepart.GridColumns.AddTextBoxColumn("SPAREPARTNAME", 80)
                .SetIsReadOnly();
            //popupGridSparepart.GridColumns.AddTextBoxColumn("PARENTDURABLECLASSID", 300)
            //    .SetIsHidden();
            //popupGridSparepart.GridColumns.AddTextBoxColumn("PARENTDURABLECLASSNAME", 150)
            //    .SetIsReadOnly()
            //    .SetIsHidden();
            //popupGridSparepart.GridColumns.AddTextBoxColumn("DURABLECLASSID", 80)
            //    .SetIsHidden();
            //popupGridSparepart.GridColumns.AddTextBoxColumn("DURABLECLASSNAME", 150)
            //    .SetIsReadOnly()
            //    .SetIsHidden();
            popupGridSparepart.GridColumns.AddTextBoxColumn("MODELNAME", 100)
                .SetIsReadOnly();
            popupGridSparepart.GridColumns.AddTextBoxColumn("MAKER", 80)
                .SetIsReadOnly();
            popupGridSparepart.GridColumns.AddTextBoxColumn("SPEC", 80)
                .SetIsReadOnly();
            popupGridSparepart.GridColumns.AddTextBoxColumn("QTY", 80)
                .SetIsReadOnly();
            popupGridSparepart.GridColumns.AddTextBoxColumn("DESCRIPTION", 200)
                .SetIsReadOnly();
            popupGridSparepart.GridColumns.AddTextBoxColumn("FACTORYID", 250)
                .SetIsHidden();
        }
        #endregion

        #region InitializeFileControl - 파일업로드 컨트롤 초기화
        private void InitializeFileControl(string resourceID, string resourceVersion)
        {
            //grdAttachment.UploadPath = "";
            //grdAttachment.Resource = new ResourceInfo()
            //{
            //    Type = "MaintWorkOrder",
            //    Id = resourceID,
            //    Version = resourceVersion
            //};
            //grdAttachment.UseCommentsColumn = true;
        }
        #endregion

        #endregion

        #region 검색
        /// <summary>
        /// 검색을 수행한다. 각 컨트롤에 입력된 값을 파라미터로 받아들인다.
        /// </summary>
        /// <param name="durableDefName"></param>
        /// <param name="durableDefID"></param>
        /// <param name="durableDefVersion"></param>
        void Search()
        {
            Dictionary<string, object> Param = new Dictionary<string, object>();
            Param.Add("WORKORDERID", _workOrderID);
            Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            Param = Commons.CommonFunction.ConvertParameter(Param);
            DataTable dt = SqlExecuter.Query("GetMaintWorkOrderPMResultDetailByEqp", "10001", Param);

            if (dt.Rows.Count > 0)
            {
                DisplayMaintWorkOrderInfo(dt.Rows[0]);
                MaintItemSearch();
                SparePartSearch();
                InitializeInsertForm(_workOrderStatusID);
                //첨부파일업로드
                InitializeFileControl(_workOrderID, "*");
                //
                //AttachmentSearch();
            }
        }
        #endregion

        #region MaintItemSearch : 점검항목검색
        /// <summary>
        /// 검색을 수행한다. 각 컨트롤에 입력된 값을 파라미터로 받아들인다.
        /// </summary>
        void MaintItemSearch()
        {
            Dictionary<string, object> Param = new Dictionary<string, object>();
            Param.Add("MAINTITEMID", _maintItemID);
            Param.Add("WORKORDERID", _workOrderID);
            Param.Add("EQUIPMENTCLASSID", _equipmentClassID);
            Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            Param = Commons.CommonFunction.ConvertParameter(Param);
            DataTable dt = SqlExecuter.Query("GetMaintWorkOrderPMItemListByEqp", "10001", Param);

            grdPMList.DataSource = dt;
        }
        #endregion

        #region SparePartSearch : 부품사용검색
        /// <summary>
        /// 검색을 수행한다. 각 컨트롤에 입력된 값을 파라미터로 받아들인다.
        /// </summary>
        void SparePartSearch()
        {
            Dictionary<string, object> Param = new Dictionary<string, object>();
            Param.Add("WORKORDERID", _workOrderID);
            Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable dt = SqlExecuter.Query("GetMaintWorkOrderPMSparePartListByEqp", "10001", Param);

            grdPartItemList.DataSource = dt;
        }
        #endregion

        #region AttachmentSearch : 첨부파일검색
        void AttachmentSearch()
        {
            //Dictionary<string, object> Param = new Dictionary<string, object>();
            //Param.Add("P_RESOURCETYPE", grdAttachment.Resource.Type);
            //Param.Add("P_RESOURCEID", grdAttachment.Resource.Id);
            //Param.Add("P_RESOURCEVERSION", grdAttachment.Resource.Version);

            //DataTable objectFileTable = this.Procedure("usp_com_selectObjectFile", Param);

            //grdAttachment.DataSource = objectFileTable;
        }
        #endregion

        #region 유효성 검사
        /// <summary>
        /// 입력/수정 시의 Validation 체크
        /// 입력/수정 각각의 체크값이 서로 다르다면 상태에 따른 분기가 있어야 한다.
        /// </summary>
        /// <param name="messageCode">에러메세지를 표현할 각각의 코드값</param>
        /// <returns></returns>
        private bool ValidateContent(string action, out string messageCode)
        {
            messageCode = "";
            if (action.Equals("Omit")) //점검생략을 진행하고자 하는 경우
            {
                if (_workOrderStatusID.Equals("Create"))
                    return true;
                else
                {
                    messageCode = "ValidateOmitMaintWorkOrderStatusModified";
                    return false;
                }
            }
            else if (action.Equals("OmitSave")) //점검생략을 저장하고자 하는 경우
            {
                if (txtOmissionReason.Text.Equals(""))
                {
                    messageCode = "ValidateOmitMaintWorkOrderStatusValue";
                    return false;
                }
                if (!ValidateEditValue(txtRepairUser.EditValue))
                {
                    messageCode = "ValidateRequiredData";
                    return false;
                }

                return true;
            }
            else if (action.Equals("Save")) //저장을 하고자 하는 경우
            {
                if (_workOrderStatusID.Equals("Finish") || _workOrderStatusID.Equals("Omit"))
                {
                    messageCode = "ValidateMaintWorkOrderStatusModified";
                    return false;
                }
                else //저장이 가능한 상태이므로 필수값 체크를 진행한다.
                {
                    if (_workOrderID.Equals(""))
                    {
                        messageCode = "ValidateRequiredData";
                        return false;
                    }
                    if (!ValidateEditValue(deStartCheckDate.EditValue))
                    {
                        messageCode = "ValidateRequiredData";
                        return false;
                    }
                    if (!_workOrderStatusID.Equals("Create"))
                        if (!ValidateEditValue(deFinishCheckDate.EditValue))
                        {
                            messageCode = "ValidateRequiredData";
                            return false;
                        }
                    if (!ValidateEditValue(txtRepairUser.EditValue))
                    {
                        messageCode = "ValidateRequiredData";
                        return false;
                    }
                    if (!ValidateNumericBox(txtUserCount, 1))
                    {
                        messageCode = "ValidateMaintWorkOrderUserCountModified";
                        return false;
                    }

                    DataTable sparePartTable = grdPartItemList.GetChangedRows();

                    foreach (DataRow sparePartRow in sparePartTable.Rows)
                    {
                        if (!ValidateCellInGrid(sparePartRow, new string[] { "SPAREPARTID", "QTY" }))
                        {
                            messageCode = "ValidateMaintWorkOrderSparePartModified";
                            return false;
                        }
                        string strConsumabledefid = sparePartRow["SPAREPARTID"].ToString();
                        string strQty = sparePartRow["QTY"].ToString();
                        double dblQty = (strQty.ToString().Equals("") ? 0 : Convert.ToDouble(strQty)); //
                        string strStockQty = sparePartRow["CURRENTQTY"].ToString();
                        double dblStockQty = (strStockQty.ToString().Equals("") ? 0 : Convert.ToDouble(strStockQty)); //
                        if (dblQty.Equals(0))
                        {
                            string lblQty = grdPartItemList.View.Columns["QTY"].Caption.ToString();
                            messageCode = String.Format("InValidOspRequiredField", lblQty);
                            return false;
                        }
                        if (dblQty > dblStockQty)
                        {
                            messageCode = String.Format("InValidCsmData004", strConsumabledefid); //다국어 메세지 추가 

                            return false;
                        }
                    }
                }
            }
            return true;
        }

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
            {
                if (resultValue < ruleValue)
                    return false;
            }
            else
            {
                return false;
            }
            //모두 통과했으므로 Validation Check완료
            return true;
        }

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

        private void SetRequiredValidationControl(Control requiredControl, bool isRequired)
        {
            if (isRequired)
                requiredControl.ForeColor = Color.Red;
            else
                requiredControl.ForeColor = Color.Black;
        }

        #endregion

        #region Private Functions
        #region InitializeInsertForm : 입고등록하기 위한 화면 초기화
        /// <summary>
        /// 입고등록정보를 입력하기 위해 화면을 초기화 한다.
        /// </summary>
        private void InitializeInsertForm(string workOrderStatus)
        {
            try
            {
                if (workOrderStatus.Equals("Create"))
                {
                    DateTime dateNow = DateTime.Now;
                    deStartCheckDate.EditValue = dateNow;
                }
                else if (workOrderStatus.Equals("Start"))
                {
                    DateTime dateNow = DateTime.Now;
                    deFinishCheckDate.EditValue = dateNow;
                }
                //_repairUserID = UserInfo.Current.Id;
                //txtRepairUser.EditValue = UserInfo.Current.Name;

                //컨트롤 접근여부는 작성상태로 변경한다.
                ControlEnableProcess(workOrderStatus);
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
            _currentStatus = currentStatus;

            if (currentStatus == "Create") //생성된 상태
            {
                txtWorkOrderID.ReadOnly = true;
                txtProgressStatus.ReadOnly = true;
                txtWorkOrderStatus.ReadOnly = true;
                txtEquipmentID.ReadOnly = true;
                txtEquipmentName.ReadOnly = true;
                txtPlanDate.ReadOnly = true;
                txtArea.ReadOnly = true;
                txtProcessSegment.ReadOnly = true;
                txtPMItem.ReadOnly = true;
                chkIsUse.ReadOnly = false;
                txtPMComment.ReadOnly = true;
                txtDelayReason.ReadOnly = true;
                txtOmissionReason.ReadOnly = true;
                deStartCheckDate.ReadOnly = false;
                deFinishCheckDate.ReadOnly = true;
                txtRepairUser.ReadOnly = false;
                txtUserCount.ReadOnly = false;

                //상태에 따른 필수값 선정 변경
                SetRequiredValidationControl(lblStartCheckDate, true);
                SetRequiredValidationControl(lblFinishCheckDate, false);
                SetRequiredValidationControl(lblRepairUser, true);
                SetRequiredValidationControl(lblUserCount, true);
                SetRequiredValidationControl(lblOmissionReason, false);

                grdPartItemList.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export;
                //grdAttachment.ButtonVisible = true;

                btnSave.Text = Language.Get("STARTDOREPAIR");
            }
            else if (currentStatus == "Start") //
            {
                txtWorkOrderID.ReadOnly = true;
                txtProgressStatus.ReadOnly = true;
                txtWorkOrderStatus.ReadOnly = true;
                txtEquipmentID.ReadOnly = true;
                txtEquipmentName.ReadOnly = true;
                txtPlanDate.ReadOnly = true;
                txtArea.ReadOnly = true;
                txtProcessSegment.ReadOnly = true;
                txtPMItem.ReadOnly = true;
                chkIsUse.ReadOnly = true;
                txtPMComment.ReadOnly = false;
                txtDelayReason.ReadOnly = true;
                txtOmissionReason.ReadOnly = true;
                deStartCheckDate.ReadOnly = true;
                deFinishCheckDate.ReadOnly = false;
                txtRepairUser.ReadOnly = false;
                txtUserCount.ReadOnly = false;

                //상태에 따른 필수값 선정 변경
                SetRequiredValidationControl(lblStartCheckDate, true);
                SetRequiredValidationControl(lblFinishCheckDate, true);
                SetRequiredValidationControl(lblRepairUser, true);
                SetRequiredValidationControl(lblUserCount, true);
                SetRequiredValidationControl(lblOmissionReason, false);

                grdPartItemList.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export;
                //grdAttachment.ButtonVisible = true;
                grdPartItemList.View.SetIsReadOnly(false);

                btnSave.Text = Language.Get("FINISHDOREPAIR");
            }
            else if (currentStatus == "Omit") //
            {
                txtWorkOrderID.ReadOnly = true;
                txtProgressStatus.ReadOnly = true;
                txtWorkOrderStatus.ReadOnly = true;
                txtEquipmentID.ReadOnly = true;
                txtEquipmentName.ReadOnly = true;
                txtPlanDate.ReadOnly = true;
                txtArea.ReadOnly = true;
                txtProcessSegment.ReadOnly = true;
                txtPMItem.ReadOnly = true;
                chkIsUse.ReadOnly = true;
                txtPMComment.ReadOnly = true;
                txtDelayReason.ReadOnly = true;
                txtOmissionReason.ReadOnly = false;
                deStartCheckDate.ReadOnly = true;
                deFinishCheckDate.ReadOnly = true;
                txtRepairUser.ReadOnly = false;
                txtUserCount.ReadOnly = true;

                //상태에 따른 필수값 선정 변경
                SetRequiredValidationControl(lblStartCheckDate, false);
                SetRequiredValidationControl(lblFinishCheckDate, false);
                SetRequiredValidationControl(lblRepairUser, true);
                SetRequiredValidationControl(lblUserCount, false);
                SetRequiredValidationControl(lblOmissionReason, true);

                grdPartItemList.GridButtonItem = GridButtonItem.Export;
                //grdAttachment.ButtonVisible = false;
                grdPartItemList.View.SetIsReadOnly(true);

                btnSave.Text = Language.Get("FINISHDOREPAIR");
            }
            else
            {
                txtWorkOrderID.ReadOnly = true;
                txtProgressStatus.ReadOnly = true;
                txtWorkOrderStatus.ReadOnly = true;
                txtEquipmentID.ReadOnly = true;
                txtEquipmentName.ReadOnly = true;
                txtPlanDate.ReadOnly = true;
                txtArea.ReadOnly = true;
                txtProcessSegment.ReadOnly = true;
                txtPMItem.ReadOnly = true;
                chkIsUse.ReadOnly = true;
                txtPMComment.ReadOnly = true;
                txtDelayReason.ReadOnly = true;
                txtOmissionReason.ReadOnly = true;
                deStartCheckDate.ReadOnly = true;
                deFinishCheckDate.ReadOnly = true;
                txtRepairUser.ReadOnly = true;
                txtUserCount.ReadOnly = true;

                SetRequiredValidationControl(lblStartCheckDate, false);
                SetRequiredValidationControl(lblFinishCheckDate, false);
                SetRequiredValidationControl(lblRepairUser, false);
                SetRequiredValidationControl(lblUserCount, false);
                SetRequiredValidationControl(lblOmissionReason, false);

                grdPartItemList.GridButtonItem = GridButtonItem.Export;
                //grdAttachment.ButtonVisible = false;
                grdPartItemList.View.SetIsReadOnly(true);

                btnSave.Text = Language.Get("SAVE");
            }
        }
        #endregion

        #region DisplayMaintWorkOrderInfo : 상세정보 화면 바인딩
        void DisplayMaintWorkOrderInfo(DataRow workOrderRow)
        {
            txtWorkOrderID.EditValue = workOrderRow.GetString("WORKORDERID");
            txtProgressStatus.EditValue = "";
            _workOrderStatusID = workOrderRow.GetString("WORKORDERSTATUSID");
            _workOrderTypeID = workOrderRow.GetString("WORKORDERTYPE");
            txtWorkOrderStatus.EditValue = workOrderRow.GetString("WORKORDERSTATUS");
            txtEquipmentID.EditValue = workOrderRow.GetString("EQUIPMENTID");
            txtEquipmentName.EditValue = workOrderRow.GetString("EQUIPMENTNAME");
            txtPlanDate.EditValue = workOrderRow.GetString("SCHEDULETIME");
            txtArea.EditValue = workOrderRow.GetString("AREANAME");
            _areaID = workOrderRow.GetString("AREAID");
            txtProcessSegment.EditValue = workOrderRow.GetString("PROCESSSEGMENTCLASSNAME");
            _processSegment = workOrderRow.GetString("PROCESSSEGMENTCLASSID");
            txtPMItem.EditValue = workOrderRow.GetString("MAINTITEMNAME");
            _maintItemID = workOrderRow.GetString("MAINTITEMID");
            _equipmentClassID = workOrderRow.GetString("EQUIPMENTCLASSID");
            txtRepairUser.EditValue = workOrderRow.GetString("REPAIRUSER");
            //_repairUserID = workOrderRow.GetString("REPAIRUSERID");
            txtUserCount.EditValue = workOrderRow.GetString("REPIARUSERCNT");
            if (workOrderRow.GetString("ISEQUIPMENTDOWN").Equals("Y"))
                chkIsUse.Checked = true;
            else
                chkIsUse.Checked = false;
            txtPMComment.EditValue = workOrderRow.GetString("REPAIRCOMMENT");
            txtDelayReason.EditValue = workOrderRow.GetString("DELAYMAINTREASON");
            txtOmissionReason.EditValue = workOrderRow.GetString("DONOTMAINTREASON");
            if(!workOrderRow.GetString("STARTTIME").Equals(""))
                deStartCheckDate.EditValue = Convert.ToDateTime(workOrderRow.GetString("STARTTIME")).ToString("yyyy-MM-dd HH:mm:ss");
            if(!workOrderRow.GetString("FINISHTIME").Equals(""))
                deFinishCheckDate.EditValue = Convert.ToDateTime(workOrderRow.GetString("FINISHTIME")).ToString("yyyy-MM-dd HH:mm:ss");
            txtAttachment.EditValue = "";
            grdPMList.DataSource = null;
            grdPartItemList.DataSource = null;

            CurrentDataRow = workOrderRow;
        }
        #endregion

        #region GetMaintWorkOrderTable : MaintWorkOrder 데이터저장용 테이블 
        private DataTable GetMaintWorkOrderTable()
        {
            DataTable returnTable = new DataTable("maintWorkOrderList");
            
            returnTable.Columns.Add("WORKORDERID");
            returnTable.Columns.Add("WORKORDERSTATUS");
            returnTable.Columns.Add("WORKORDERTYPE");
            returnTable.Columns.Add("STARTTIME");
            returnTable.Columns.Add("REPAIRUSERID");
            returnTable.Columns.Add("REPAIRUSERCNT");
            returnTable.Columns.Add("ISEQUIPMENTDOWN");
            returnTable.Columns.Add("FINISHDATE");
            returnTable.Columns.Add("REPAIRCOMMENT");
            returnTable.Columns.Add("DELAYMAINTREASON");
            returnTable.Columns.Add("DONOTMAINTREASON");
            returnTable.Columns.Add("EQUIPMENTID");

            returnTable.Columns.Add("ENTERPRISEID");
            returnTable.Columns.Add("PLANTID");

            returnTable.Columns.Add("CREATOR");
            returnTable.Columns.Add("CREATEDTIME");
            returnTable.Columns.Add("MODIFIER");
            returnTable.Columns.Add("MODIFIEDTIME");
            returnTable.Columns.Add("LASTTXNHISTKEY");
            returnTable.Columns.Add("LASTTXNID");
            returnTable.Columns.Add("LASTTXNUSER");
            returnTable.Columns.Add("LASTTXNTIME");
            returnTable.Columns.Add("LASTTXNCOMMENT");
            returnTable.Columns.Add("VALIDSTATE");

            returnTable.Columns.Add("_STATE_");

            return returnTable;
        }
        #endregion

        #region GetMaintWoSparePartTable : MaintWoSparePart 데이터저장용 테이블 
        private DataTable GetMaintWoSparePartTable()
        {
            DataTable returnTable = new DataTable("maintWoSparePartList");

            returnTable.Columns.Add("SPAREPARTID");
            returnTable.Columns.Add("WORKORDERID");
            returnTable.Columns.Add("EQUIPMENTCLASSID");
            returnTable.Columns.Add("QTY", typeof(int));
            returnTable.Columns.Add("OLDQTY", typeof(int));
            returnTable.Columns.Add("FACTORYID");
                        
            returnTable.Columns.Add("ENTERPRISEID");
            returnTable.Columns.Add("PLANTID");

            returnTable.Columns.Add("CREATOR");
            returnTable.Columns.Add("CREATEDTIME");
            returnTable.Columns.Add("MODIFIER");
            returnTable.Columns.Add("MODIFIEDTIME");
            returnTable.Columns.Add("LASTTXNHISTKEY");
            returnTable.Columns.Add("LASTTXNID");
            returnTable.Columns.Add("LASTTXNUSER");
            returnTable.Columns.Add("LASTTXNTIME");
            returnTable.Columns.Add("LASTTXNCOMMENT");
            returnTable.Columns.Add("VALIDSTATE");

            returnTable.Columns.Add("_STATE_");

            return returnTable;
        }
        #endregion

        #region GetMaintWoDetailResultTable : MaintWoDetailResultTable 데이터저장용 테이블 
        private DataTable GetMaintWoDetailResultTable()
        {
            DataTable returnTable = new DataTable("maintWoDetailResultList");

            returnTable.Columns.Add("MAINTITEMID");
            returnTable.Columns.Add("MAINTDETAILSEQUENCE");
            returnTable.Columns.Add("WORKORDERID");
            returnTable.Columns.Add("ACTIONDESCRIPTION");
            returnTable.Columns.Add("MAINTRESULT");
            returnTable.Columns.Add("EQUIPMENTCLASSID");

            returnTable.Columns.Add("ENTERPRISEID");
            returnTable.Columns.Add("PLANTID");

            returnTable.Columns.Add("CREATOR");
            returnTable.Columns.Add("CREATEDTIME");
            returnTable.Columns.Add("MODIFIER");
            returnTable.Columns.Add("MODIFIEDTIME");
            returnTable.Columns.Add("LASTTXNHISTKEY");
            returnTable.Columns.Add("LASTTXNID");
            returnTable.Columns.Add("LASTTXNUSER");
            returnTable.Columns.Add("LASTTXNTIME");
            returnTable.Columns.Add("LASTTXNCOMMENT");
            returnTable.Columns.Add("VALIDSTATE");

            returnTable.Columns.Add("_STATE_");

            return returnTable;
        }
        #endregion

        #region SaveData : 입력/수정을 수행
        private void SaveData()
        {
            //Validation 체크 부분 작성 필요
            this.ShowWaitArea();
            //저장 로직
            grdPartItemList.View.FocusedRowHandle = grdPartItemList.View.FocusedRowHandle;
            grdPartItemList.View.FocusedColumn = grdPartItemList.View.Columns["SPAREPARTNAME"];
            grdPartItemList.View.ShowEditor();
            try
            {
                btnAddAttachment.Enabled = false;
                btnClose.Enabled = false;
                btnDownloadTemplate.Enabled = false;
                btnOmissionPM.Enabled = false;
                btnSave.Enabled = false;

                string messageCode = "";
                if (lblOmissionReason.ForeColor.Equals(Color.Red))
                {
                    #region 점검생략코드
                    if (ValidateContent("OmitSave", out messageCode))
                    {
                        DataSet maintWorkOrderSet = new DataSet();
                        //치공구 제작의뢰를 입력
                        DataTable maintWorkOrderTable = GetMaintWorkOrderTable();

                        DataRow maintWorkOrderRow = maintWorkOrderTable.NewRow();

                        maintWorkOrderRow["WORKORDERID"] = _workOrderID;
                        maintWorkOrderRow["WORKORDERSTATUS"] = "Omit";
                        maintWorkOrderRow["WORKORDERTYPE"] = _workOrderTypeID;
                        maintWorkOrderRow["REPAIRUSERID"] = txtRepairUser.EditValue;
                        maintWorkOrderRow["DONOTMAINTREASON"] = txtOmissionReason.EditValue;

                        maintWorkOrderRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                        maintWorkOrderRow["PLANTID"] = _plantID;

                        maintWorkOrderRow["MODIFIER"] = UserInfo.Current.Id;
                        maintWorkOrderRow["VALIDSTATE"] = "Valid";
                        maintWorkOrderRow["_STATE_"] = "modified";

                        maintWorkOrderTable.Rows.Add(maintWorkOrderRow);

                        maintWorkOrderSet.Tables.Add(maintWorkOrderTable);

                        //TODO : 첨부파일을 사용한다면 아래의 주석을 제거후 사용
                        //데이터 저장전 첨부파일의 저장을 진행
                        //if (grdAttachment.Resource.Type.Equals("MaintWorkOrder") && grdAttachment.Resource.Id.Equals(_workOrderID))
                        //{
                        //    if (grdAttachment.GetChangedRows().Rows.Count > 0)
                        //    {
                        //        grdAttachment.SaveChangedFiles();

                        //        DataTable changed = grdAttachment.GetChangedRows();

                        //        ExecuteRule("SaveObjectFile", changed);
                        //    }
                        //}

                        ExecuteRule<DataTable>("MaintWorkOrder", maintWorkOrderSet);
                        SearchHandler?.Invoke();
                        BindHandler?.Invoke(_workOrderID, "Omit");
                        Dispose();
                    }
                    else
                    {
                        this.ShowMessage(MessageBoxButtons.OK, messageCode, "");
                    }
                    #endregion
                }
                else
                {
                    //Validation Check시 입력/수정시 검사할 값이 다르다면 수정필요
                    if (ValidateContent("Save", out messageCode))
                    {
                        #region 설비PM실적정보입력
                        DataSet maintWorkOrderSet = new DataSet();
                        //치공구 제작의뢰를 입력
                        DataTable maintWorkOrderTable = GetMaintWorkOrderTable();

                        DataRow maintWorkOrderRow = maintWorkOrderTable.NewRow();

                        DateTime startDate = new DateTime();
                        DateTime endDate = new DateTime();

                        maintWorkOrderRow["WORKORDERID"] = _workOrderID;
                        maintWorkOrderRow["WORKORDERSTATUS"] = _workOrderStatusID;
                        maintWorkOrderRow["WORKORDERTYPE"] = _workOrderTypeID;

                        if (deStartCheckDate.EditValue != null && deStartCheckDate.EditValue.ToString() != "")
                        {
                            startDate = Convert.ToDateTime(deStartCheckDate.EditValue.ToString());
                            maintWorkOrderRow["STARTTIME"] = startDate.ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        maintWorkOrderRow["REPAIRUSERID"] = txtRepairUser.EditValue;
                        maintWorkOrderRow["REPAIRUSERCNT"] = txtUserCount.EditValue;
                        maintWorkOrderRow["ISEQUIPMENTDOWN"] = chkIsUse.Checked ? "Y" : "N";

                        //=======================================================================================================================================================================
                        //IsEquipmentDown 정보가 Y라면 EES연동 데이터를 할당한다.
                        //transfer.sendEquipmentStateToEes(row.getString("EQUIPMENTID"), row.getString("EQUIPMENTSTATE"), row.getString("EQUIPMENTDESCRIPTION"), row.getString("MODIFIER"));
                        maintWorkOrderRow["EQUIPMENTID"] = txtEquipmentID.EditValue;
                        //=======================================================================================================================================================================

                        if (deFinishCheckDate.EditValue != null && deFinishCheckDate.EditValue.ToString() != "")
                        {
                            endDate = Convert.ToDateTime(deFinishCheckDate.EditValue.ToString());
                            maintWorkOrderRow["FINISHDATE"] = endDate.ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        maintWorkOrderRow["REPAIRCOMMENT"] = txtPMComment.EditValue;
                        maintWorkOrderRow["DELAYMAINTREASON"] = txtDelayReason.EditValue;

                        maintWorkOrderRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                        maintWorkOrderRow["PLANTID"] = _plantID;

                        maintWorkOrderRow["MODIFIER"] = UserInfo.Current.Id;
                        maintWorkOrderRow["VALIDSTATE"] = "Valid";
                        maintWorkOrderRow["_STATE_"] = "modified";

                        maintWorkOrderTable.Rows.Add(maintWorkOrderRow);

                        maintWorkOrderSet.Tables.Add(maintWorkOrderTable);
                        #endregion

                        #region 점검항목부분입력
                        //점검항목데이터입력
                        if (grdPMList.View.RowCount > 0)
                        {
                            DataTable detailResultTable = GetMaintWoDetailResultTable();
                            DataTable changedDetailResultTable = grdPMList.GetChangedRows();

                            foreach (DataRow currentRow in changedDetailResultTable.Rows)
                            {
                                if (!currentRow.GetString("MAINTITEMID").Equals(""))
                                {
                                    DataRow detailResultRow = detailResultTable.NewRow();

                                    detailResultRow["MAINTITEMID"] = currentRow.GetString("MAINTITEMID");
                                    detailResultRow["MAINTDETAILSEQUENCE"] = currentRow.GetString("MAINTDETAILSEQUENCE");
                                    detailResultRow["WORKORDERID"] = _workOrderID;
                                    detailResultRow["EQUIPMENTCLASSID"] = _equipmentClassID;
                                    detailResultRow["ACTIONDESCRIPTION"] = currentRow.GetString("ACTIONDESCRIPTION");
                                    detailResultRow["MAINTRESULT"] = currentRow.GetString("MAINTRESULT");

                                    detailResultRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                                    detailResultRow["PLANTID"] = _plantID;

                                    detailResultRow["CREATOR"] = UserInfo.Current.Id;
                                    detailResultRow["MODIFIER"] = UserInfo.Current.Id;

                                    detailResultRow["_STATE_"] = "added";
                                    detailResultRow["VALIDSTATE"] = "Valid";

                                    detailResultTable.Rows.Add(detailResultRow);
                                }
                            }
                            maintWorkOrderSet.Tables.Add(detailResultTable);
                        }
                        #endregion

                        #region 부품사용부분 입력
                        //부품사용 데이터입력
                        if (grdPartItemList.View.RowCount > 0)
                        {
                            DataTable sparePartTable = GetMaintWoSparePartTable();
                            DataTable changedSparePartTable = grdPartItemList.GetChangedRows();

                            foreach (DataRow currentRow in changedSparePartTable.Rows)
                            {
                                if (!currentRow.GetString("SPAREPARTID").Equals(""))
                                {
                                    DataRow sparePartRow = sparePartTable.NewRow();

                                    sparePartRow["SPAREPARTID"] = currentRow.GetString("SPAREPARTID");
                                    sparePartRow["WORKORDERID"] = _workOrderID;
                                    sparePartRow["EQUIPMENTCLASSID"] = _equipmentClassID;
                                    sparePartRow["QTY"] = currentRow.GetString("QTY");
                                    sparePartRow["FACTORYID"] = currentRow.GetString("FACTORYID");

                                    sparePartRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                                    sparePartRow["PLANTID"] = _plantID;

                                    sparePartRow["CREATOR"] = UserInfo.Current.Id;
                                    sparePartRow["MODIFIER"] = UserInfo.Current.Id;

                                    if (currentRow.GetString("_STATE_").Equals("deleted"))
                                    {
                                        sparePartRow["_STATE_"] = "deleted";
                                        sparePartRow["VALIDSTATE"] = "Invalid";
                                    }
                                    else
                                    {
                                        sparePartRow["_STATE_"] = "added";
                                        sparePartRow["VALIDSTATE"] = "Valid";
                                    }
                                    sparePartTable.Rows.Add(sparePartRow);
                                }
                            }
                            maintWorkOrderSet.Tables.Add(sparePartTable);
                        }
                        #endregion

                        #region 첨부파일 사용부분
                        //TODO : 첨부파일을 사용한다면 아래의 주석을 제거후 사용
                        //데이터 저장전 첨부파일의 저장을 진행
                        //if (grdAttachment.Resource.Type.Equals("MaintWorkOrder") && grdAttachment.Resource.Id.Equals(_workOrderID))
                        //{
                        //    if (grdAttachment.GetChangedRows().Rows.Count > 0)
                        //    {
                        //        grdAttachment.SaveChangedFiles();

                        //        DataTable changed = grdAttachment.GetChangedRows();

                        //        ExecuteRule("SaveObjectFile", changed);
                        //    }
                        //}
                        #endregion

                        ExecuteRule<DataTable>("MaintWorkOrder", maintWorkOrderSet);
                        SearchHandler?.Invoke();
                        if(_workOrderStatusID.Equals("Create"))
                            BindHandler?.Invoke(_workOrderID, "Start");
                        else
                            BindHandler?.Invoke(_workOrderID, "Finish");
                        Dispose();
                    }
                    else
                    {
                        this.ShowMessage(MessageBoxButtons.OK, messageCode, "");
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
            finally
            {
                this.CloseWaitArea();
                btnAddAttachment.Enabled = true;
                btnClose.Enabled = true;
                btnDownloadTemplate.Enabled = true;
                btnOmissionPM.Enabled = true;
                btnSave.Enabled = true;
            }
        }
        #endregion

        #region DisplayOmitScreen : 점검생략화면구성
        private void DisplayOmitScreen()
        {
            //btnOmissionPM.ForeColor = Color.Red;
            ControlEnableProcess("Omit");
        }
        #endregion

        #region AddAttachment : 첨부파일을 추가한다.
        private void AddAttachment()
        {

        }
        #endregion
        #endregion
    }
}
