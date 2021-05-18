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

namespace Micube.SmartMES.EquipmentManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 설비관리 > Spare Part 관리 > S/P 입고등록
    /// 업  무  설  명  : Spare Part 입고정보에 대한 조회/등록/수정/삭제
    /// 생    성    자  : 김용조
    /// 생    성    일  : 2019-07-03
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class SparePartInbound : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가        

        //InBoundNo를 표현 데이터가 존재한다면 조회상태이므로 수정, 아니라면 등록으로 진행한다.
        private string _inboundNo;
        //수정시 Stock정보에 정확한 계산을 위해 이전수량을 기억
        private string _oldQty;
        #endregion

        #region 생성자

        public SparePartInbound()
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
            InitializeUserControl();
            var values = Conditions.GetValues();
            string plantID = values["P_PLANTID"].ToString();
            string factoryid = values["FACTORYID"].ToString();
           
            InitializeFactoryComboBox(plantID, factoryid);
            

            //컨트롤들을 최초에는 사용할 수 없게 설정
            controlEnableProcess("");
            
            InitRequiredControl();
        }

        #region InitRequiredControl - 필수입력항목들을 체크한다.
        private void InitRequiredControl()
        {
            SetRequiredValidationControl(lblInboundDate);
            SetRequiredValidationControl(lblFactory);
            SetRequiredValidationControl(lblQty);
            //SetRequiredValidationControl(lblPrice);
        }
        #endregion

        /// <summary>        
        /// 코드그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            // GRID 초기화
            grdSparePartInbound.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Export | GridButtonItem.Restore; ;   // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기
            grdSparePartInbound.View.SetIsReadOnly();
            grdSparePartInbound.View.AddTextBoxColumn("INBOUNDNO")
                .SetIsHidden();
            grdSparePartInbound.View.AddTextBoxColumn("INBOUNDDATE", 150)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss");                                        //입고일자
            grdSparePartInbound.View.AddTextBoxColumn("SPAREPARTID", 100);              //부품코드
            grdSparePartInbound.View.AddTextBoxColumn("SPAREPARTVERSION")
                .SetIsHidden();              //부품코드
            grdSparePartInbound.View.AddTextBoxColumn("SPAREPARTNAME", 250);           //부품명
            grdSparePartInbound.View.AddTextBoxColumn("QTY", 50)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);                          //입고수량
            grdSparePartInbound.View.AddTextBoxColumn("PRICE", 120)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);                          //입고단가            
            grdSparePartInbound.View.AddTextBoxColumn("FACTORYID")
                .SetIsHidden();                                                         //공장(아이디)
            grdSparePartInbound.View.AddTextBoxColumn("FACTORYNAME", 200);              //공ㅇ장
            grdSparePartInbound.View.AddTextBoxColumn("DESCRIPTION", 250);              //비고
            //grdSparePartInbound.View.AddTextBoxColumn("PARENTDURABLECLASSID")
            //    .SetIsHidden();                                                         //중분류코드
            //grdSparePartInbound.View.AddTextBoxColumn("PARENTDURABLECLASSNAME", 300)     //중분류
            //    .SetTextAlignment(TextAlignment.Center)
            //    ;
            //grdSparePartInbound.View.AddTextBoxColumn("DURABLECLASSID")
            //    .SetIsHidden();                                                         //소분류코드
            //grdSparePartInbound.View.AddTextBoxColumn("DURABLECLASSNAME", 200)           //소분류
            //    .SetTextAlignment(TextAlignment.Center)
            //    ;
            grdSparePartInbound.View.AddTextBoxColumn("MAKER", 120);                     //Maker
            grdSparePartInbound.View.AddTextBoxColumn("SAFETYSTOCK", 80);               //적정재고
            grdSparePartInbound.View.AddTextBoxColumn("SPEC", 80);                      //규격            
            grdSparePartInbound.View.AddTextBoxColumn("MODELNAME", 150);                //모델명
            grdSparePartInbound.View.AddTextBoxColumn("CREATOR", 80);                   //작성자 아이디
            grdSparePartInbound.View.AddTextBoxColumn("USERNAME", 80);                  //작성자
            grdSparePartInbound.View.AddTextBoxColumn("CREATEDTIME", 150)               //작성일자            
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                ;
            grdSparePartInbound.View.PopulateColumns();
        }

        /// <summary>
        /// 부품코드를 조회할 유저컨트롤을 초기화한다.
        /// </summary>
        private void InitializeUserControl()
        {
            ucDurableCode.SetSmartTextBoxForSearchData(txtParentDurableClassCode, txtParentDurableClassName, txtDurableClassCode, txtDurableClassName, txtSpec, txtSafetyStock, txtDurableName, txtPrice, txtModelName, speImage);
            ucDurableCode.DurableDefinitionCode = "";
        }

        #region ComboBox  초기화

        /// <summary>
        /// Factory ComboBox를 초기화한다. 검색조건의 Site에 따라 값이 변경되어야 하므로
        /// 초기화 버튼 클릭시 재로딩하는 것으로 한다.
        /// </summary>
        /// <param name="plantID"></param>
        private void InitializeFactoryComboBox(string plantID, string factoryID)
        {
            // 검색조건에 정의된 공장을 정리
            cboFactory.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboFactory.ValueMember = "FACTORYID";
            cboFactory.DisplayMember = "FACTORYNAME";
            

            if (plantID.Equals(""))
            {
                cboFactory.DataSource = SqlExecuter.Query("GetFactoryListByEqp", "10001"
                 , new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType } });
            }
            else
            {
                cboFactory.DataSource = SqlExecuter.Query("GetFactoryListByEqp", "10001"
                 , new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "PLANTID", plantID } });
            }

            cboFactory.ShowHeader = false;
            cboFactory.ReadOnly = true;

            if (factoryID != null && factoryID != "")
            {
                cboFactory.EditValue = factoryID;
            }


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
            btnModify.Click += BtnModify_Click;
            btnInitialize.Click += BtnInitialize_Click;
            btnErase.Click += BtnErase_Click;

            grdSparePartInbound.View.FocusedRowChanged += View_FocusedRowChanged;
            ucDurableCode.msgHandler += DisplayMessageBox;

            Shown += SparePartInbound_Shown;
        }

        #region SparePartInbound_Shown - Site관련정보를 화면로딩후 설정한다.
        private void SparePartInbound_Shown(object sender, EventArgs e)
        {
            ChangeSiteCondition();

            ((SmartComboBox)Conditions.GetControl("P_PLANTID")).EditValueChanged += ConditionPlant_EditValueChanged;
            ((SmartComboBox)Conditions.GetControl("FACTORYID")).EditValueChanged += SparePartInbound_EditValueChanged;

        }

        private void SparePartInbound_EditValueChanged(object sender, EventArgs e)
        {
            cboFactory.EditValue = Conditions.GetValue("FACTORYID");
        }
        #endregion

        #region ConditionPlant_EditValueChanged - 검색조건의 Site정보 변경시 관련 쿼리들을 일괄 변경한다.
        private void ConditionPlant_EditValueChanged(object sender, EventArgs e)
        {
            ChangeSiteCondition();
        }
        #endregion

        /// <summary>
        /// 삭제버튼 클릭시 수행할 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnErase_Click(object sender, EventArgs e)
        {
            DeleteData();     
        }

        /// <summary>
        /// 그리드에서 특정 행 선택시 수행할 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DisplaySparePartInboundInfo();
        }

        /// <summary>
        /// 초기화버튼 클릭시 수행할 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnInitialize_Click(object sender, EventArgs e)
        {
            InitializeInsertForm();
        }

        /// <summary>
        /// 저장버튼 클릭시 수행할 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnModify_Click(object sender, EventArgs e)
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
            else if (btn.Name.ToString().Equals("Initialization"))
                BtnInitialize_Click(sender, e);
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
            DataTable sparePartSearchResult = SqlExecuter.Query("GetSparePartInboundListByEqp", "10001", values);

            

            grdSparePartInbound.DataSource = sparePartSearchResult;

            if (sparePartSearchResult.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                InitializeInsertForm();
                controlEnableProcess("");
            }
            else
            {
                //DisplaySparePartInboundInfoDetail(0);
                grdSparePartInbound.View.FocusedRowHandle = 0;
                DisplaySparePartInboundInfo();
            }
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            //InitializeCondition_Plant();
            InitializeCondition_Factory();
            //InitializeCondition_MiddleClassCode();
            //InitializeCondition_BottomClassCode();
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
            SmartComboBox cboFactoryid = Conditions.GetControl<SmartComboBox>("FACTORYID");
            cboFactoryid.EditValueChanged += cboFactoryid_EditValueChanged;
            DataTable dtfactory = cboFactoryid.DataSource as DataTable;
            if (dtfactory.Rows.Count > 0)
            {
                DataRow dr = dtfactory.Rows[0];
                cboFactoryid.EditValue = dr["FACTORYID"];

            }
        }
        private void cboFactoryid_EditValueChanged(object sender, EventArgs e)
        {
            var values = Conditions.GetValues();

            string factoryid = values["FACTORYID"].ToString();
            cboFactory.EditValue = factoryid;

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
               .SetDefault(UserInfo.Current.Plant, "PLANTID") //기본값 설정 UserInfo.Current.Plant
               .SetValidationIsRequired()
               .SetIsReadOnly(true)
            ;
        }

        /// <summary>
        /// site에 따른 공장 설정 
        /// </summary>
        private void InitializeCondition_Factory()
        {

            var planttxtbox = Conditions.AddComboBox("FACTORYID", new SqlQuery("GetFactoryListByEqp", "10001"), "FACTORYNAME", "FACTORYID")
               .SetLabel("FACTORY")
               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
               .SetPosition(0.2)
               .SetRelationIds("P_PLANTID") //사이트에 설정된 값에 따라 공장을 바인딩
               .SetValidationIsRequired()
            ;
        }

        /// <summary>
        /// 중분류 설정
        /// </summary>
        private void InitializeCondition_MiddleClassCode()
        {

            var middleClassCodeBox = Conditions.AddComboBox("DURABLECLASSID", new SqlQuery("GetClassCodeListByEqp", "10001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "DURABLECLASSID", "9" } }), "DURABLECLASSNAME", "DURABLECLASSID")
               .SetLabel("중분류")
               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
               .SetPosition(0.4)
               .SetEmptyItem("전체조회", "", true)
            ;
        }

        /// <summary>
        /// 소분류 설정
        /// </summary>
        private void InitializeCondition_BottomClassCode()
        {
            var bottomClassCodeBox = Conditions.AddComboBox("BOTTOMDURABLECLASSID", new SqlQuery("GetClassCodeListByEqp", "10001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType } }), "DURABLECLASSNAME", "DURABLECLASSID")
               .SetLabel("소분류")
               .SetEmptyItem("전체조회", "", true)
               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
               .SetPosition(0.5)
               .SetRelationIds("DURABLECLASSID")//중분류 선택된 값에 따라 소분류를 바인딩
               ; 
        }

        /// <summary>
        /// 저장 삭제후 재 검색시 사용
        /// 생성 및 수정시 발생한 아이디를 통해 자동 선택되도록 유도하며 삭제시에는 null로 매개변수를 받아서 첫번째 행을 보여준다. 행이 없다면 빈 값으로 설정
        /// </summary>
        /// <param name="inboudNo">입력 및 수정시 발생한 값의 아이디.</param>
        private void Research(string inboundNo)
        {
            // TODO : 조회 SP 변경
            InitializeInsertForm();

            //FactoryID가 입력되어 있어야만 진행할 수 있다.
            if (Conditions.GetValue("FACTORYID").ToString() != "")
            {
                var values = Conditions.GetValues();

                #region 기간 검색형 전환 처리 - 만약 시간까지 포함하여 검색한다면 아래 문구는 필요없음            
                values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                #endregion

                values = Commons.CommonFunction.ConvertParameter(values);
                DataTable sparePartSearchResult = SqlExecuter.Query("GetSparePartInboundListByEqp", "10001", values);

                if (sparePartSearchResult.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
                {
                    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                }

                grdSparePartInbound.DataSource = sparePartSearchResult;

                //현재 작업한 결과를 선택할 수 있도록 한다.
                if (inboundNo == null) //삭제 작업후
                {
                    //값이 하나도 없다면 빈 화면
                    if (grdSparePartInbound.View.RowCount == 0)
                    {
                        //입력대기 화면으로 설정
                        InitializeInsertForm();
                        controlEnableProcess("");
                    }
                    else
                    {
                        //첫번재 값으로 설정
                        //DisplaySparePartInboundInfoDetail(0);
                        if (grdSparePartInbound.View.RowCount > 0)
                        {
                            grdSparePartInbound.View.FocusedRowHandle = 0;
                            DisplaySparePartInboundInfo();
                        }
                        else
                        {
                            InitializeInsertForm();
                            controlEnableProcess("");
                        }
                    }
                }
            }

            //재검색은 수행하지 않더라도 입력받은 InboundNo가 있다면 상세정보 쿼리는 진행한다.
            if (inboundNo != null && inboundNo != "")
            {
                //검색결과에서 데이터를 조회한 후 일치하는 데이터가 있다면 선택행으로 하고 아니라면 추가 후 바인딩한다.
                int currentIndex = GetRowHandleInGrid(grdSparePartInbound, "INBOUNDNO", inboundNo);
                if (currentIndex > -1)
                {
                    grdSparePartInbound.View.FocusedRowHandle = currentIndex;
                    DisplaySparePartInboundInfo();
                }
                else
                {
                    AddSparePartDataInGrid(inboundNo, true);
                }
            }
        }

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            // TODO : 유효성 로직 변경

            //
        }
        /// <summary>
        /// 입력/수정 시의 Validation 체크
        /// 입력/수정 각각의 체크값이 서로 다르다면 상태에 따른 분기가 있어야 한다.
        /// </summary>
        /// <returns></returns>
        private bool ValidateContent()
        {
            //각각의 상태에 따라 보여줄 메세지가 상이하다면 각각의 분기에서 메세지를 표현해야 한다.
            //From공장과 To공장의 값이 일치하면 안되며 두 공장모두 지정되어 있어야 한다.

            //부품코드없이 데이터를 입력/수정 할 수 없다.
            if (!ValidateEditValue(ucDurableCode.DurableDefinitionCode))
            {
                ;//부품코드를 입력하라는 메세지

                return false;
            }

            //이동수량은 1이상이 되어야 한다.
            if (!ValidateNumericBox(txtQty, 0))
            {
                //this.ShowMessage(MessageBoxButtons.OK, "SparePartMoveValidation", "");
                return false;
            }

            //To공장과 From공장은 같아선 안된다.
            if (!ValidateEditValue(cboFactory.EditValue))
            {
                //From공장을 선택하라는 메세지
                //this.ShowMessage(MessageBoxButtons.OK, "SparePartMoveValidation", "");
                return false;
            }

            //입고단가를 입력해야 하며 금액이므로 0 이상이 되어야 한다.
            //if (!ValidateNumericBox(txtPrice, 1))
            //{
            //    //this.ShowMessage(MessageBoxButtons.OK, "SparePartMoveValidation", "");
            //    return false;
            //}
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
                if (resultValue <= ruleValue)
                    return false;

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

        /// <summary>
        /// 필수값 색상 변경
        /// </summary>
        /// <param name="requiredControl"></param>
        private void SetRequiredValidationControl(Control requiredControl)
        {
            requiredControl.ForeColor = Color.Red;
        }

        #endregion

        #region Private Function

        // TODO : 화면에서 사용할 내부 함수 추가
        /// <summary>
        /// 마스터 정보를 조회한다.
        /// </summary>
        private void LoadData()
        {

        }

        #region InitializeInsertForm : 입고등록하기 위한 화면 초기화
        /// <summary>
        /// 입고등록정보를 입력하기 위해 화면을 초기화 한다.
        /// </summary>
        private void InitializeInsertForm()
        {
            try
            {
                //최초등록이라면 InboundNo 변수는 NULL이어야 한다.
                _inboundNo = null;

                //공장의 경우 검색조건의 Site설정에 따라 값을 변경해야 한다.
                if (null != Conditions.GetValue("P_PLANTID"))
                {
                    if(null != Conditions.GetValue("FACTORYID"))
                        InitializeFactoryComboBox(Conditions.GetValue("P_PLANTID").ToString(), Conditions.GetValue("FACTORYID").ToString());
                    else
                        InitializeFactoryComboBox(Conditions.GetValue("P_PLANTID").ToString(), "");
                }
                else
                {
                    InitializeFactoryComboBox("", "");
                }

                //입고일은 오늘날짜로 설정
                DateTime dateNow = DateTime.Now;
                deInboundDate.EditValue = dateNow;//.ToString("yyyy-MM-dd"); 
                                                  //유저컨트롤을 초기화한다.
                ucDurableCode.DurableDefinitionCode = "";
                ucDurableCode.DurableDefinitionVersion = "";
                txtSpec.EditValue = "";
                txtSafetyStock.EditValue = "";
                txtDurableName.EditValue = "";
                txtParentDurableClassName.EditValue = "";
                txtDurableClassName.EditValue = "";
                txtQty.EditValue = "0";
                txtPrice.EditValue = "0";
                txtDescription.EditValue = "";
                txtModelName.EditValue = "";
                _oldQty = null;
                speImage.Image = null;

                controlEnableProcess("added");                
            }
            catch(Exception err)
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
        private void controlEnableProcess(string currentStatus)
        {
            if(currentStatus == "added") //초기화 버튼을 클릭한 경우
            {
                deInboundDate.ReadOnly = true;
                cboFactory.ReadOnly = true;
                ucDurableCode.ReadOnly = false;
                txtSpec.ReadOnly = true;
                txtSafetyStock.ReadOnly = true;
                txtDurableName.ReadOnly = true;
                txtParentDurableClassName.ReadOnly = true;
                txtDurableClassName.ReadOnly = true;
                txtQty.ReadOnly = false;
                txtPrice.ReadOnly = true;
                txtDescription.ReadOnly = false;
                txtModelName.ReadOnly = true;
            }
            else if(currentStatus == "modified") //
            {
                deInboundDate.ReadOnly = true;
                cboFactory.ReadOnly = true;
                ucDurableCode.ReadOnly = true;
                txtSpec.ReadOnly = true;
                txtSafetyStock.ReadOnly = true;
                txtDurableName.ReadOnly = true;
                txtParentDurableClassName.ReadOnly = true;
                txtDurableClassName.ReadOnly = true;
                txtQty.ReadOnly = false;
                txtPrice.ReadOnly = true;
                txtDescription.ReadOnly = false;
                txtModelName.ReadOnly = true;
            }
            else
            {
                deInboundDate.ReadOnly = true;
                cboFactory.ReadOnly = true;
                ucDurableCode.ReadOnly = true;
                txtSpec.ReadOnly = true;
                txtSafetyStock.ReadOnly = true;
                txtDurableName.ReadOnly = true;
                txtParentDurableClassName.ReadOnly = true;
                txtDurableClassName.ReadOnly = true;
                txtQty.ReadOnly = true;
                txtPrice.ReadOnly = true;
                txtDescription.ReadOnly = true;
                txtModelName.ReadOnly = true;
            }
        }
        #endregion

        #region CreateSaveDatatable : 입력/수정/삭제를 위한 DataTable의 Template 생성
        /// <summary>
        /// 데이터 입력/수정/삭제를 위해 서버로 전송하는 데이터테이블을 반환한다.
        /// </summary>
        /// <returns></returns>
        private DataTable CreateSaveDatatable()
        {
            DataTable dt = new DataTable();
            dt.TableName = "list";
            dt.Columns.Add("INBOUNDNO");
            dt.Columns.Add("ENTERPRISEID");
            dt.Columns.Add("PLANTID");
            dt.Columns.Add("SPAREPARTID");
            dt.Columns.Add("DURABLEDEFVERSION");
            dt.Columns.Add("FACTORYID");
            dt.Columns.Add("INBOUNDDATE");
            dt.Columns.Add("QTY");
            dt.Columns.Add("OLDQTY");       //수량변경시 정확한 수량계산을 위한 OLDQTY
            dt.Columns.Add("PRICE");
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

                if (ValidateContent())
                {
                    DataTable sparePartTable = CreateSaveDatatable();

                    //DataTable에 화면의 값을 지정
                    DataRow sparePartInfo = sparePartTable.NewRow();

                    sparePartInfo["ENTERPRISEID"] = UserInfo.Current.Enterprise.ToString();
                    sparePartInfo["PLANTID"] = Conditions.GetValue("P_PLANTID").ToString();
                    sparePartInfo["FACTORYID"] = cboFactory.EditValue;
                    sparePartInfo["SPAREPARTID"] = ucDurableCode.DurableDefinitionCode;
                    sparePartInfo["DURABLEDEFVERSION"] = ucDurableCode.DurableDefinitionVersion;
                    sparePartInfo["INBOUNDDATE"] = ((DateTime)deInboundDate.EditValue).ToString("yyyy-MM-dd HH:mm:ss");
                    sparePartInfo["QTY"] = GetNumericFromTextBox(txtQty);
                    sparePartInfo["PRICE"] = GetNumericFromTextBox(txtPrice);
                    sparePartInfo["DESCRIPTION"] = txtDescription.EditValue;
                    sparePartInfo["LASTTXNHISTKEY"] = "";
                    sparePartInfo["LASTTXNID"] = "";
                    sparePartInfo["LASTTXNUSER"] = "";
                    sparePartInfo["LASTTXNTIME"] = "";
                    sparePartInfo["LASTTXNCOMMENT"] = "";
                    sparePartInfo["VALIDSTATE"] = "Valid";

                    //_inboundNo가 null이라면 데이터는 등록이며 아니라면 수정
                    if (_inboundNo == null)
                    {
                        sparePartInfo["CREATOR"] = UserInfo.Current.Id;
                        sparePartInfo["CREATEDTIME"] = DateTime.Now;
                        sparePartInfo["_STATE_"] = "added";
                    }
                    else
                    {
                        sparePartInfo["INBOUNDNO"] = _inboundNo;
                        sparePartInfo["OLDQTY"] = _oldQty;              //수량변경시 정확한 수량계산을 위한 이전수량

                        sparePartInfo["_STATE_"] = "modified";
                        sparePartInfo["MODIFIER"] = UserInfo.Current.Id;
                        sparePartInfo["MODIFIEDTIME"] = DateTime.Now;
                    }

                    sparePartTable.Rows.Add(sparePartInfo);

                    DataTable resultTable = this.ExecuteRule<DataTable>("SparePartInbound", sparePartTable);
                    //Server에서 현재 결과값을 반환하지 않음. 결과값 반환후 특정 로직을 처리해야 한다면 아래로직을 수행
                    DataRow resultRow = resultTable.Rows[0];

                    controlEnableProcess("modified");

                    //가져온 결과값에 따라 재검색한 후 가져온 아이디에 맞는 행을 선택으로 변경한다.
                    Research(resultRow.GetString("INBOUNDNO"));

                    //AddSparePartDataInGrid(resultRow.GetString("INBOUNDNO"));
                }
                else
                {
                    this.ShowMessage(MessageBoxButtons.OK, "SparePartMoveValidation", "");
                }
            }
            catch(Exception ex)
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

                if (grdSparePartInbound.View.GetFocusedDataRow() != null)
                {
                    DialogResult result = this.ShowMessage(MessageBoxButtons.YesNo, "OspDoProcessWant", btnErase.Text);//삭제하시겠습니까? 

                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        DataTable sparePartTable = CreateSaveDatatable();

                        //DataTable에 화면의 값을 지정
                        DataRow sparePartInfo = sparePartTable.NewRow();

                        //Inbound정보 삭제를 위한 것이 아닌 Stock정보를 업데이트 하기 위해 모든 정보를 전달한다.
                        sparePartInfo["INBOUNDNO"] = _inboundNo;
                        sparePartInfo["ENTERPRISEID"] = UserInfo.Current.Enterprise.ToString();
                        sparePartInfo["PLANTID"] = Conditions.GetValue("P_PLANTID").ToString();
                        sparePartInfo["FACTORYID"] = cboFactory.EditValue;
                        sparePartInfo["SPAREPARTID"] = ucDurableCode.DurableDefinitionCode;
                        sparePartInfo["INBOUNDDATE"] = deInboundDate.EditValue;
                        sparePartInfo["QTY"] = txtQty.EditValue;
                        sparePartInfo["PRICE"] = txtPrice.EditValue;
                        sparePartInfo["DESCRIPTION"] = txtDescription.EditValue;
                        sparePartInfo["MODIFIER"] = UserInfo.Current.Id;
                        sparePartInfo["MODIFIEDTIME"] = DateTime.Now;
                        sparePartInfo["LASTTXNHISTKEY"] = "";
                        sparePartInfo["LASTTXNID"] = "";
                        sparePartInfo["LASTTXNUSER"] = "";
                        sparePartInfo["LASTTXNTIME"] = "";
                        sparePartInfo["LASTTXNCOMMENT"] = "";
                        sparePartInfo["VALIDSTATE"] = "Invalid";
                        sparePartInfo["_STATE_"] = "deleted";

                        sparePartTable.Rows.Add(sparePartInfo);

                        DataTable resultTable = this.ExecuteRule<DataTable>("SparePartInbound", sparePartTable);

                        controlEnableProcess("");
                        //가져온 결과값에 따라 재검색한 후 가져온 아이디에 맞는 행을 선택으로 변경한다.
                        Research(null);
                    }
                }
                else
                {
                    //ShowMessage(MessageBoxButtons.OK, String.Format("SelectItem", Language.GetDictionary("DURABLE")), "");
                    ShowMessage(MessageBoxButtons.OK, "SelectItem", Language.Get("SPAREPART"));
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

        #region DisplaySparePartInboundInfo : 그리드에서 행 선택시 상세정보를 표시
        /// <summary>
        /// 그리드에서 행 선택시 상세정보를 표시하는 메소드
        /// </summary>
        private void DisplaySparePartInboundInfo()
        {
            //포커스 행 체크 
            if (grdSparePartInbound.View.FocusedRowHandle < 0) return;

            if (grdSparePartInbound.View.FocusedRowHandle == grdSparePartInbound.View.RowCount) return;

            AddSparePartDataInGrid(grdSparePartInbound.View.GetFocusedDataRow().GetString("INBOUNDNO"), false);
        }
        #endregion

        #region AddSparePartDataInGrid : 그리드에 새로운 데이터를 입력
        /// <summary>
        /// 그리드에 새로운 데이터를 입력
        /// </summary>
        private void AddSparePartDataInGrid(string inboundNo, bool isAddRowToGrid)
        {
            #region 기간 검색형 전환 처리 - 만약 시간까지 포함하여 검색한다면 아래 문구는 필요없음            
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            values.Add("PLANTID", Conditions.GetValue("P_PLANTID"));
            values.Add("INBOUNDNO", Int32.Parse(inboundNo));
            #endregion

            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable sparePartSearchResult = SqlExecuter.Query("GetSparePartInboundDetailInfoByEqp", "10001", values);

            if (sparePartSearchResult.Rows.Count > 0)
            {
                DisplaySparePartInboundInfoDetail(sparePartSearchResult.Rows[0]);
            }
            else
            {
                InitializeInsertForm(); //조회된 데이터가 없으므로 빈값으로 정렬한다.
                controlEnableProcess("");
            }

            //검색값에 해당 행이 존재하지 않으므로 그리드에 넣어준다.  검색에서 데이터가 조회되지 않았더라도 데이터를 입력해준다.
            //if (isAddRowToGrid)
            //{
            //    DataTable gridTable = (DataTable)grdSparePartInbound.DataSource;

            //    DataRow appendRow = gridTable.NewRow();

            //    appendRow["INBOUNDNO"] = sparePartSearchResult.Rows[0]["INBOUNDNO"];
            //    appendRow["INBOUNDDATE"] = sparePartSearchResult.Rows[0]["INBOUNDDATE"];
            //    appendRow["SPAREPARTID"] = sparePartSearchResult.Rows[0]["SPAREPARTID"];
            //    appendRow["SPAREPARTVERSION"] = sparePartSearchResult.Rows[0]["SPAREPARTVERSION"];
            //    appendRow["SPAREPARTNAME"] = sparePartSearchResult.Rows[0]["SPAREPARTNAME"];
            //    appendRow["QTY"] = sparePartSearchResult.Rows[0]["QTY"];
            //    appendRow["PRICE"] = sparePartSearchResult.Rows[0]["PRICE"];
            //    appendRow["FACTORYID"] = sparePartSearchResult.Rows[0]["FACTORYID"];
            //    appendRow["FACTORYNAME"] = sparePartSearchResult.Rows[0]["FACTORYNAME"];
            //    appendRow["DESCRIPTION"] = sparePartSearchResult.Rows[0]["DESCRIPTION"];
            //    appendRow["PARENTDURABLECLASSID"] = sparePartSearchResult.Rows[0]["PARENTDURABLECLASSID"];
            //    appendRow["PARENTDURABLECLASSNAME"] = sparePartSearchResult.Rows[0]["PARENTDURABLECLASSNAME"];
            //    appendRow["DURABLECLASSID"] = sparePartSearchResult.Rows[0]["DURABLECLASSID"];
            //    appendRow["DURABLECLASSNAME"] = sparePartSearchResult.Rows[0]["DURABLECLASSNAME"];
            //    appendRow["MAKER"] = sparePartSearchResult.Rows[0]["MAKER"];
            //    appendRow["SPEC"] = sparePartSearchResult.Rows[0]["SPEC"];
            //    appendRow["SAFETYSTOCK"] = sparePartSearchResult.Rows[0]["SAFETYSTOCK"];
            //    appendRow["CREATOR"] = sparePartSearchResult.Rows[0]["CREATOR"];
            //    appendRow["USERNAME"] = sparePartSearchResult.Rows[0]["USERNAME"];
            //    appendRow["CREATEDTIME"] = sparePartSearchResult.Rows[0]["CREATEDTIME"];

            //    gridTable.Rows.InsertAt(appendRow, 0);

            //    grdSparePartInbound.DataSource = gridTable;
            //    grdSparePartInbound.View.FocusedRowHandle = 0;
            //}
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
        private int GetRowHandleInGrid(SmartBandedGrid targetGrid, string columnName, string findValue)
        {
            for (int i = 0; i < targetGrid.View.RowCount; i++)
            {
                if (findValue.Equals(targetGrid.View.GetDataRow(i)[columnName].ToString()))
                {
                    return i;
                }
            }
            return -1;
        }
        #endregion

        #region DisplaySparePartInboundInfoDetail : 입력받은 인덱스의 그리드의 내용을 화면에 바인딩
        /// <summary>
        /// 입력받은 그리드내의 인덱스에 대한 내용을 화면에 표시한다.
        /// </summary>
        /// <param name="rowHandle"></param>
        private void DisplaySparePartInboundInfoDetail(DataRow sparePartInfo)
        {
            //해당 업무에 맞는 Enable체크 수행
            //DataRow sparePartInfo = grdSparePartInbound.View.GetDataRow(rowHandle);

            //전역변수로 _inboundNo 할당
            _inboundNo = sparePartInfo.GetString("INBOUNDNO");

            deInboundDate.EditValue = sparePartInfo["INBOUNDDATE"];
            cboFactory.EditValue = sparePartInfo["FACTORYID"];
            ucDurableCode.DurableDefinitionCode = sparePartInfo.GetString("SPAREPARTID");
            ucDurableCode.DurableDefinitionVersion = sparePartInfo.GetString("SPAREPARTVERSION");
            //유저컨트롤의 자동검색을 막기위한 제어
            ucDurableCode.IsSearch = true;

            txtSpec.EditValue = sparePartInfo["SPEC"];
            txtSafetyStock.EditValue = sparePartInfo["SAFETYSTOCK"];
            txtDurableName.EditValue = sparePartInfo["SPAREPARTNAME"];
            txtParentDurableClassName.EditValue = sparePartInfo["PARENTDURABLECLASSNAME"];
            txtDurableClassName.EditValue = sparePartInfo["DURABLECLASSNAME"];
            txtQty.EditValue = sparePartInfo["QTY"];
            txtPrice.EditValue = sparePartInfo["PRICE"];
            txtDescription.EditValue = sparePartInfo["DESCRIPTION"];
            txtModelName.EditValue = sparePartInfo["MODELNAME"];

            //수량변경시 STOCK정보에 정확한 수량 계산을 위한 이전수량을 기억
            _oldQty = sparePartInfo.GetString("QTY");

            if (sparePartInfo["IMAGEDATA"] != null && !Format.GetString(sparePartInfo["IMAGEDATA"]).Equals(string.Empty))
            {
                speImage.Image = (Bitmap)new ImageConverter().ConvertFrom(sparePartInfo["IMAGEDATA"]);                
            }
            else
            {
                speImage.Image = null;
            }

            //수정상태라 판단하여 화면 제어
            controlEnableProcess("modified");
        }
        #endregion

        #region DisplayMessageBox : 메세지 팝업창을 호출
        /// <summary>
        /// 메세지 팝업창을 호출
        /// </summary>
        private void DisplayMessageBox(string messageCode)
        {
            try
            {
                //코드값에 맞는 메세지 출력
                ShowMessage(messageCode);
                
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
            finally
            {
                
            }
        }
        #endregion
        
        #region 숫자값만 반환받기
        private string GetNumericFromTextBox(SmartTextBox targetText)
        {
            if (targetText.Text != "")
            {
                StringBuilder valueBuilder = new StringBuilder();
                valueBuilder.Append(targetText.Text);

                return valueBuilder.Replace(",", "").ToString();
            }
            else
            {
                return "0";
            }
        }
        #endregion

        #region ChangeSiteCondition - 검색조건의 Site변경시 관련된 쿼리들을 변경
        private void ChangeSiteCondition()
        {   
            ucDurableCode.PlantID = Conditions.GetValue("P_PLANTID").ToString();

            InitializeInsertForm();

            InitializeFactoryComboBox(ucDurableCode.PlantID, "");
        }
        #endregion
        #endregion
    }
}
