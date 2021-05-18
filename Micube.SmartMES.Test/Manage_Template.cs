#region using

using Micube.Framework.Net;
using Micube.Framework;
using Micube.Framework.SmartControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DevExpress.XtraCharts;
using DevExpress.Utils;
using Micube.Framework.SmartControls.Validations;
using Micube.SmartMES.Commons.Controls;

#endregion

namespace Micube.SmartMES.Test
{
    /* MainForm.App.Config 파일에 아래 내용을 변경하여 디버깅용 폼을 바로 시작할 수 있습니다.
     *   <appSettings>
            <add key="StartUp" value="MenuId=Manage_CodeClass;ProgramId=Micube.SmartFactory.SystemManagement.Manage_CodeClass;Caption=코드그룹관리" /> <!--기본 시작 폼-->
          </appSettings>
    */

    /* 버튼 Click 이벤트 표준 try, catch 가이드
        try
        {
            DialogManager.ShowWaitDialog(); //전역 Waiting 표시
            toolbarButton.Enabled = false; //버튼 재실행 방지

            //작업내용
        }
        catch (Exception ex)
        {
            this.ShowError(ex); //표준 에러처리. 로그, 서버로 내용전송, 메세지 처리등 처리합니다.
        }
        finally
        {
            DialogManager.Close(); //Waiting 제거
            toolbarButton.Enabled = true; //버튼 활성화
        }
    */

    /*     기본 가이드
     * 1. 비동기 :  검색, 엑셀출력만 비동기를 적용합니다.
     * 2. 툴바 : 저장, Import, Export 만 적용이 되어 있습니다. 공통 툴바 기능이 추가되면 메소드가 더 추가될 예정입니다.
     * 3. 코드 구성 : InitializeCondition() 에서 검색조건을 설정하고,  InitializeContent()에서 우측 컨텐츠 영역을 초기화 합니다.
     * 4. VisualStudio의 보기/작업목록 메뉴를 클릭하시면 템플릿에서 작업해야할 항목이 표시됩니다.
    */

    /// <summary>
    /// 프 로 그 램 명  : ex> 시스템관리 > 코드 관리 > 코드그룹 정보
    /// 업  무  설  명  : ex> 시스템에서 공통으로 사용되는 코드그룹 정보를 관리한다.
    /// 생    성    자  : 홍길동
    /// 생    성    일  : 2019-05-14
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class Manage_Template : SmartConditionManualBaseForm
    {
        #region Local Variables

        private SmartBandedGrid grdGrid = new SmartBandedGrid();                // 예제 작성을 위한 그리드 컨트롤
        private SmartChart chart = new SmartChart();                            // 예제 작성을 위한 차트 컨트롤
        private SmartPivotGridControl pivotGrid = new SmartPivotGridControl();  // 예제 작성을 위한 피벗 그리드 컨트롤

        #endregion

        #region 생성자

        public Manage_Template()
        {
            InitializeComponent();
        }

        #endregion

        #region 그리드 ContextMenu
        /// <summary>
        /// 메뉴를 화면에 표시할때 호출됨. 여기서 활성, 비활성 처리 가능
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void GrdGrid_ShowContextMenuEvent(SmartBandedGrid sender, Framework.SmartControls.Grid.ShowContextMenuEventArgs args)
        {
            DataRow currentEquipmentRow = grdGrid.View.GetFocusedDataRow();
            if (currentEquipmentRow.RowState == DataRowState.Added) //DB에 저장안된것일때는 비활성화
            {
                if (_myContextMenu1 != null) _myContextMenu1.Enabled = false;
            }
        }

        DevExpress.Utils.Menu.DXMenuItem _myContextMenu1, _myContextMenu2;
        /// <summary>
        /// 컨텍스트 메뉴 초기화
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void GrdGrid_InitContextMenuEvent(SmartBandedGrid sender, Framework.SmartControls.Grid.InitContextMenuEventArgs args)
        {
            args.IsCancel = false; //메뉴를 보여주지 않음  , true이면 메뉴를 보여주지 않음.
            
            //args.ContextMenus //기본 메뉴            
            args.AddMenus.Add(_myContextMenu1 = new DevExpress.Utils.Menu.DXMenuItem(Language.Get("LanguageKey"), this.OpenForm) { BeginGroup = true }); //BeginGroup : 새로운 그룹 생성
            args.AddMenus.Add(_myContextMenu2 = new DevExpress.Utils.Menu.DXMenuItem(Language.Get("LanguageKey"), this.CustomContextMenu2)) ; //BeginGroup : 새로운 그룹 생성
        }

        private void OpenForm(object sender, EventArgs args)
        {
            try
            {
                DialogManager.ShowWaitDialog();

                DataRow currentEquipmentRow = grdGrid.View.GetFocusedDataRow();
                this.OpenMenu("LayoutEdit", "Equipment", currentEquipmentRow); //다른창 호출..
            }
            catch (Exception ex)
            {
                this.ShowError(ex);
            }
            finally
            {
                DialogManager.Close();
            }
        }

        private void CustomContextMenu2(object sender, EventArgs args)
        {

        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼 클릭
        /// </summary>
        /// <returns></returns>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            //TODO :  RuleName을 수정하세요 (저장기능이 없다면 현재 함수를 삭제하세요.)
            // 그리드에서 추가, 수정, 삭제 된 행의 데이터만 가져온다.
            //DataTable changed = gridMain.GetChangedRows();

            // 서버의 "RuleName"과 일치하는 Rule을 실행한다.
            //this.ExecuteRule("RuleName", changed);
        }

        /// <summary>
        /// 화면 별 추가 버튼 클릭
        /// </summary>        
        protected override void OnToolbarCustomClick(ToolbarClickEventArgs e)
        {
            base.OnToolbarCustomClick(e);

            //id를 체크해서 작업 하세요.
            if (e.Is("id"))
            {

            }
        }

        #endregion

        #region 검색

        /// <summary>
        /// 비동기 override 모델, 비동기 모델은 검색에서만 제공합니다. ESC키로 취소 가능합니다.
        /// </summary>
        /// <returns></returns>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            // 조회조건 항목들의 값을 Dictionary<string, object> 타입으로 가져온다.
            var values = this.Conditions.GetValues();

            //TODO : Id를 수정하세요
            // "Id" 이름으로 생성된 StoredProcedure(PostgreSQL Function)를 호출하고 결과를 DataTable로 저장한다.
            DataTable dataSource = await this.ProcedureAsync("Id", values);

            SearchChart(dataSource);

            //ConditionItemTextBox autoColumnInfo = new ConditionItemTextBox();
            //autoColumnInfo
            //    .SetValidationIsRequired() //필수
            //    .SetDefault(1);             //기본값 등 설정이 가능합니다.

            /// <param name="conditionItem">자동으로 생성할 필드형식</param>
            /// <param name="columnWidth">자동으로 생성할 필드 넓이</param>
            /// <param name="parentBandFieldName">밴드가 있다면 밴드 이름 없으면 빈문자열</param>
            /// <param name="beforeFieldName">필드 이름 다음에 컬럼위치함</param>
            /// <param name="noPopulateColumns">자동 생성하지 않을 컬럼이름, 없으면 생략가능</param>
            //grdGrid.View.AutoPopulateColumns(autoColumnInfo, 100, "", "VALIDSTATE");
        }


        /// <summary>
        /// 검색조건 초기화. 
        /// 조회조건 정보, 메뉴 - 조회조건 매핑 화면에 등록된 정보를 기준으로 구성됩니다.
        /// DB에 등록한 정보를 제외한 추가 조회조건 구성이 필요한 경우 사용합니다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            Conditions.AddCheckEdit("CheckEdit");
            Conditions.AddComboBox("ComboBox", new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            Conditions.AddDateEdit("DateEdit");
            Conditions.AddDateRangeEdit("DataRangeEdit");
            Conditions.AddPeriodEdit("PeriodEdit");
            //Conditions.AddSelectPopup("SelectPopup", new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID");
            InitializeCondition_Popup();
            Conditions.AddSpinEdit("SpinEdit");
            Conditions.AddTextBox("TextBox");
            Conditions.AddTreeList("TreeList", new SqlQuery("GetEquipmentTree", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "NAME", "ID", "PARENTID");


            //필요한 것만 사용, 나머지는 기본값에 의존함.

            /* 조회조건 항목 추가
             * AddCheckEdit : 체크 박스
             * AddComboBox : 콤보 박스
             * AddDateEdit : 날짜
             * AddDateRangeEdit : 날짜 범위 (사용 안함)
             * AddPeriodEdit : 기간
             * AddSelectPopup : 팝업
             * AddSpinEdit : 숫자
             * AddTextBox : 텍스트
             * AddTreeList : 트리
            */

            /*
             * SetValidation~ : 유효성 검사 관련 함수 (현재 조건에서 벗어나는 경우 UI 보여주는 작업 진행중)
             * SetValidationCustom : 사용자 정의 유효성 검사 루틴. ValidationResult를 리턴해야함.
             * SetValidationGreatThen : 타겟보다 값이 커야 함.
             * SetValidationIsRequired : 필수 입력값
             * SetValidationIsRequiredCondition : 타겟에 값이 있는경우 내가 필수조건으로 처리.
             * SetValidationLessThen : 타겟보다 값이 작아야 함.
            */

            /*
             * Set~ : 검색 조건 설정
             * SetDefault : 기본값
             * SetDisplayFormat : yyyy-MM-dd HH:mm:ss 날짜 포맷 지정 가능. (날짜 형식은 시분초 까지 기본으로 지정되어 있음)
             * SetEmptyItem : 전체검색과 같은 빈내용
             * SetHidden : 숨기기 기능
             * SetIsReadOnly : 읽기전용
             * SetLabel : 다국어용 DictionaryId 입력. Id와 값이 같을때는 생략가능, 툴팁 설정 (MessageId 지정)
             * SetMaxLength : 최대입력길이, byte단위 옵션있음                    
             * SetMultiColumns : 콤보박스 여러컬럼 보여주기
             * SetPosition : 위치지정 (1.5 이면 DB에서 설정한 1번과 2번 사이에 위치)
             * SqlQueryAdapter : 전체조회 기능과 같은 상수값 제공 기능
             *                  예시 :  SqlQueryAdapter sqlQuery = new SqlQueryAdapter();
                                        sqlQuery.AddData("validState", "validState");
                                        sqlQuery.AddData("codeClassType", "codeClassType");
                                        this.Conditions.AddComboBox("p_condAllCond", sqlQuery)
             * SetRelationIds : 콤보박스간 종속관계 설정. Master의 Id를 입력함.
             * SetResultCount : 콤보박스 항목 선택 개수 (1 = 단일, 0 = 복수)
             * SetTextAlignment : 좌, 우, 가운데, 기본값(숫자는 오른쪽, 문자는 왼쪽) 정렬
             * SetTreeLayout : 트리 표시 설정 (팝업 오픈 시 검색 여부, 세로 크기)
            */

            // 설비트리 예제
            //Conditions.AddTreeList("Id", new SqlQuery("GetEquipmentTree", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "NAME", "ID", "PARENTID")
            //.SetEmptyItem("Root", "*") // 루트캡션, 루트Value                
            //.SetResultCount(0)      // 0이면 체크박스로 무한대 선택
            //.SetPosition(0)         // 맨 처음 상단에 표시됨
            //.SetTreeLayout(true, 250);   // 검색창을 화면에 표시, 세로크기 설정(기본값 250)

            // DB의 내용으로 자동 생성된 검색조건을 코드에서 수정하고싶은 경우
            //Conditions.GetCondition<ConditionItemTextBox>("txtAllCond")
            //    .SetDefault("*");
        }

        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // 조회조건에 구성된 Control에 대한 처리가 필요한 경우
            SmartTextBox textBox = Conditions.GetControl<SmartTextBox>("id");
            textBox.Text = "";
            textBox.EditValueChanged += (sender, args) =>
            {

            };
        }

        /// <summary>
        /// 검색조건 팝업 예제
        /// </summary>
        private void InitializeCondition_Popup()
        {
            //팝업 컬럼 설정
            var parentAreaPopupColumn = Conditions.AddSelectPopup("AREAID", new SqlQuery("GetAreaList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "AREANAME", "AREAID")
               .SetPopupLayout("PARENTAREA", PopupButtonStyles.Ok_Cancel, false, true)  //팝업창 UI 설정.
               .SetPopupResultCount(1)  //팝업창 선택가능한 개수
               ;

            //팝업 조회조건
            parentAreaPopupColumn.Conditions.AddComboBox("PLANTID", new SqlQuery("GetCondCodePlant", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetEmptyItem()
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                .SetLabel("PLANT")
                ;
            
            //팝업 그리드
            parentAreaPopupColumn.GridColumns.AddTextBoxColumn("AREAID", 150)
                .SetValidationKeyColumn();
            parentAreaPopupColumn.GridColumns.AddTextBoxColumn("AREANAME", 200);
        }

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            //TODO : 그리드의 유효성 검사
            grdGrid.View.CheckValidation();

            DataTable changed = grdGrid.View.GetCheckedRows();
            
            // 수정된 데이터가 없는 경우 메시지 처리
            if (changed.Rows.Count == 0)
                throw MessageException.Create("NoSaveData"); //저장할 데이터 없음
        }

        #endregion

        #region Event

        private void InitializeEvent()
        {
            grdGrid.InitContextMenuEvent += GrdGrid_InitContextMenuEvent;
            grdGrid.ShowContextMenuEvent += GrdGrid_ShowContextMenuEvent;
        }

        // 신규 행 추가 시 호출 이벤트
        private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            SmartTreeList equiptmentTree = args.GetControlInSearchCondition<SmartTreeList>("p_equipmentId");

            /*          SmartTreeList 제공 함수
                 *          GetFocusedNodeLevel() // 포커스 노드의 레벨  Plant(1), Area(2), Equiptment(3)
                 *          GetFocusedDisplayTextByParentLevel(parentLevel) :  포커스가 있는 노드에서 해당 레벨의 표시이름을 찾아온다. 주로 설비를 선택한 경우 Plant를 얻어오기 위해 사용함.
                 *          GetFocusedValueByParentLevel(parentLevel) : 포커스가 있는 노드에서 해당 레벨의 값을 찾아온다. 주로 설비를 선택한 경우 Plant를 얻어오기 위해 사용함.
                 *          HasCheckedOrFocusedNodeLevel(level) : (사용안함) 체크박스가 표시된경우는 체크된것 아니면 포커스 노드에 해당 레벨이 있나? 주소 설비까지 선택했는지 확인하기 위해 사용 
                 *          HasCheckedOrFocusedNodeType("Equipment") : 체크박스가 표시된경우는 체크된것 아니면 포커스 노드에 해당 타입이 있나? 주로 설비까지 선택했는지 확인하기 위해 사용
                 *          GetCheckedOrFocusedLeafValue() :  체크박스가 표시된경우는 체크된것 아니면 포커스 노드에서 마지막 단계에 있는 Value값들을 가져온다. 설비ID를 얻어오기 위해 사용.
                 *          GetFocusedValue() : 포커스가 있는 노드의 Value값.
                 *            */

            //설비단계를 선택하지 않았으면 추가가 취소된다. (nodeType : Plant, Area, Equiptment)
            if (!equiptmentTree.HasCheckedOrFocusedNodeType("Equipment"))
            {
                args.IsCancel = true; //취소
                this.ShowMessage("설비트리에서 설비까지 선택해야 합니다"); //화면마다 적당한 메세지를 표시해 주세요.
                return;
            }

            //그리드의 입력값 초기화
            args.NewRow["EQUIPMENTID"] = equiptmentTree.GetFocusedValue();
            args.NewRow["EQUIPMENTNAME"] = equiptmentTree.GetFocusedDisplayText();

            //선택한 노드에서 PlantId를 얻어오고 싶은 경우
            if (equiptmentTree.GetFocusedNodeLevel() >= 1)
            {
                string focusedPlantName = equiptmentTree.GetFocusedDisplayTextByParentLevel(parentLevel: 1); //포커스가 있는 노드의 PlantId 가져오기 : parentLevel=1이 Plant의 레벨
                string focusedPlantId = equiptmentTree.GetFocusedValueByParentLevel(parentLevel: 1); //포커스가 있는 노드의 PlantId 가져오기 : parentLevel=1이 Plant의 레벨
            }
        }

        // 차트 Series 또는 SeriesPoint 선택 시 호출 이벤트
        private void Chart_SelectedItemsChanged(object sender, SelectedItemsChangedEventArgs e)
        {
            // SeriesSelectionMode = Argument/Point 인 경우
            foreach (DataRowView view in e.NewItems)
            {
                string argumentName = view.Row["fieldName"].ToString();
            }

            // SeriesSelectionMode = Series 인 경우
            foreach (Series series in e.NewItems)
            {
                string seriesName = series.Name;
            }
        }

        /// <summary>
        /// TextEdit를 ComboBox로 변환하는 예제
        /// </summary>
        private void View_AddNewRowChangeControlEvent(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.BandedGrid.AddNewRowChangeControlArgs args)
        {
            if (args.FieldName != "AREAID") return;

            SqlQueryAdapter sqlQuery = new SqlQueryAdapter();
            sqlQuery.AddData("테스트", "Area10");
            sqlQuery.AddData("테스트2", "Area11");
            sqlQuery.AddData("테스트3", "Area12");

            args.ChangeComboBox("AREAID", sqlQuery)
                .SetDefault("1");
        }

        // 그리드 캡션 버튼 클릭 시 호출 이벤트
        private void View_GridCellButtonClickEvent(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.GridCellButtonClickEventArgs args)
        {
            if (args.FieldName == "mybutton")
            {
                //args.CurrentRow // 현재 행
                MessageBox.Show(string.Join(",", args.CurrentRow.ItemArray));
            }
        }

        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 우측 컨텐츠 영역에 초기화할 코드를 넣으세요.
        /// </summary>
        protected override void InitializeContent()
        {
            base.InitializeContent();

            //InitializeCondition_Popup();

            InitializeGrid();
            chart.SetEmptyChart();
        }

        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            //TODO : 그리드를 초기화 하세요
            grdGrid.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdGrid.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Import | GridButtonItem.Export;    // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기
            grdGrid.View.SetKeyColumn("CODEID");
            grdGrid.View.SetSortOrder("CODEID");
            grdGrid.View.SetAutoFillColumn("CODENAME");

            List<(string, string)> valueList = new List<(string, string)>
            {
                ("CODEID", "Valid")
            };
            grdGrid.View.SetFocuseRowByKeyColumn(valueList);
            grdGrid.View.SetIsReadOnly();
            grdGrid.View.SetNotAllowNullColumn("CODENAME");
            grdGrid.View.SetSearchCondition(Conditions);

            grdGrid.View.AddingNewRow += View_AddingNewRow;     // 행추가 시 처리 이벤트
            grdGrid.View.AddNewRowChangeControlEvent += View_AddNewRowChangeControlEvent;       // 행추가할때 특정 컬럼만 컨트롤 형태 변경


            grdGrid.View.AddBand("CODEID", "CODEID");
            grdGrid.View.AddCheckBoxColumn("CheckBox", 80);
            grdGrid.View.AddColorEditColumn("ColorEdit", 110);
            grdGrid.View.AddComboBoxColumn("ComboBox", 120, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsRefreshByOpen(true);
            grdGrid.View.AddDateEditColumn("DateEdit", 130);
            var group = grdGrid.View.AddGroupColumn("GroupColumn");
            group.AddTextBoxColumn("Text", 120);    // GroupColumn 밴드에 Text 컬럼 추가
            grdGrid.View.AddLanguageColumn("MESSAGENAME", 110);
            grdGrid.View.AddMemoEditColumn("MemoEdit", 110);
            grdGrid.View.AddSelectPopupColumn("SearchPopup", 150, new SqlQuery("GetCodeList", "00001", "CODECLASSID=LanguageType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdGrid.View.AddSpinEditColumn("SpinEdit", 90);
            grdGrid.View.AddTextBoxColumn("TextBox", 110);
            InitializeGrid_Popup();

            grdGrid.View.PopulateColumns();         // 그리드 설정 후 호출 (설정에 기반하여 UI 구성, 반드시 호출해야 함)


            //필요한 것만 사용, 나머지는 기본값에 의존함.
            //다국어 : id와 다국어 id가 같은경우 라벨 설정 불필요함.(COM_M_DICTIONARY 테이블에 id와 같은 값으로 DictionaryId Row 추가 필요)

            /* 그리드의 툴바메뉴 컨트롤
             * Bit OR, - 연산
             * this.grdCodeClass.GridButtonItem |= GridButtonItem.Add; // 추가버튼 표시한다
             * this.grdCodeClass.GridButtonItem -= GridButtonItem.CRUD; //추가,수정,삭제 버튼 숨긴다
            */

            /* 그리드 뷰 기본 설정
             * GridMultiSelectionMode : 멀티 선택 타입 설정 (Cell, CheckBox, Row)
             * SetAutoFillColumn : 그리드의 남은 영역을 채울 컬럼을 설정
             * SetFocusedRowByKeyColumn : 키 컬럼이 갖는 값을 기준으로 포커스 된 행을 설정
             * SetIsReadOnly : 그리드를 ReadOnly로 설정
             * SetKeyColumn : 키 컬럼 지정
             * SetNotAllowNullColumn : Null을 허용하지 않는 컬럼 설정
             * SetSearchCondition : 그리드에서 검색조건을 연결하여 검색조건을 기반으로 그리드의 기본값으로 사용함.
             * SetSortOrder : 기본 Sorting 컬럼 지정
            */

            /* 컬럼 추가 
             * AddBand : 컬럼 헤더를 그룹지어 2개 이상 보여지는 경우
             * AddCheckBoxColumn : 체크박스 컬럼
             * AddColorEditColumn : 색상 선택 컬럼
             * AddComboBoxColumn : 콥보박스 컬럼
             * AddDateEditColumn : 날짜 컬럼
             * AddGroupColumn : 컬럼을 Band로 그룹지어 표현하는 컬럼
             * AddLanguageColumn : 시스템 상 등록된 LanguageType 별 컬럼을 자동으로 생성하는 컬럼
             * AddMemoEditColumn : 메모 필드 추가
             * AddSelectPopupColumn : 검색가능한 팝업이 표시되는 컬럼 (기본 팝업 또는 Custom 팝업 사용 가능)
             *     Custom 팝업 예시 : AddSelectPopupColumn("ACTIONCODE", 100, new EquipmentAlarmPopUp());
             *                        EquipmentAlarmPopUp 폼은 ISmartCustomPopup 인터페이스를 구현해야 합니다.
             * AddSpinEditColumn : 숫자형식 컬럼
             * AddTextBoxColumn : 문자형식 컬럼             
            */

            /* 유효성 검사 함수
             * SetValidation~ : 유효성 검사 관련 함수
             * SetValidationCustom : 사용자 정의 유효성 검사 루틴. ValidationResult를 리턴해야함.
             * SetValidationGreatThen : 타겟보다 값이 커야 함.
             * SetValidationIsRequired : 필수 입력값
             * SetValidationIsRequiredCondition : 타겟에 값이 있는경우 내가 필수조건으로 처리.
             * SetValidationLessThen : 타겟보다 값이 작아야 함.
            */

            /* 컬럼 기본 설정
             * Set~ : 컬럼 설정
             * SetDefault : 기본값, 그리드에서 검색조건을 기반으로 기본값 설정 가능. (검색조건의 ID를 입력함)
             * SetDisplayFormat : yyyy-MM-dd HH:mm:ss 날짜 포맷 지정 가능. (날짜 형식은 시분초 까지 기본으로 지정되어 있음)
             * SetEmptyItem : 전체검색과 같은 빈내용
             * SetHidden : 숨기기 기능
             * SetIsReadOnly : 읽기전용
             * SetIsRefreshByOpen : 콤보박스 팝업이 오픈될때마다 데이터를 다시 읽어옵니다. parentCodeGroupId 와 같이 데이터 추가될때 콤보박스 데이터 Refresh가 필요할때 사용합니다.
             * SetLabel : 다국어용 DictionaryId 입력. Id와 값이 같을때는 생략가능, 툴팁 설정 (MessageId 지정)
             * SetMaxLength : 최대입력길이, byte단위 옵션있음
             * SetMultiColumns : 콤보박스 여러컬럼 보여주기
             * SetPosition : 컬럼 위치 설정
             * SqlQueryAdapter : 전체조회 기능과 같은 상수값 제공 기능
             *                  예시 :  SqlQueryAdapter sqlQuery = new SqlQueryAdapter();
                                        sqlQuery.AddData("validState", "validState");
                                        sqlQuery.AddData("codeClassType", "codeClassType");
                                        this.Conditions.AddComboBox("p_condAllCond", sqlQuery) 
             * SetRelationIds : 콤보박스간 종속관계 설정. Master의 Id를 입력함.
             * SetResultColumn : 콤보박스 항목 선택 개수 (1 = 단일, 0 = 복수)
             * SetSortOrder : 그리드의 초기 기본 정렬
             * SetTextAlignment : 좌, 우, 가운데, 기본값(숫자는 오른쪽, 문자는 왼쪽) 정렬
             * SetValidationCustom : 사용자 정의 Validation 처리
            */

            /* 팝업 컬럼 설정
             * SetPopupLayout : 팝업 화면 구성 
             * SetPopupResultCount : 팝업의 선택가능 개수 1:1개, 0:무한대, 나머지는 숫자대로
             * SetPopupResultMapping : 팝업창 그리드의 컬럼이름과 현재 그리드의 컬럼 이름이 다르면 매핑 필요. (예 팝업 : AreaId, 그리드 : ParentAreaId)
             * SetPopupDefaultByGridColumnId : 팝업의 검색조건 기본값을 현재 그리드의 컬럼 값으로 설정
             */
        }

        // 사용자 정의 Validation 함수
        private IEnumerable<ValidationResult> CustomValidation(int rowHandle)
        {
            var currentRow = grdGrid.View.GetDataRow(rowHandle);
            List<ValidationResult> result = new List<ValidationResult>();


            if (currentRow.Field<decimal>("ValueFieldName") > currentRow.Field<decimal>("MaxValueFieldName"))
            {
                ValidationResult resultLsl = new ValidationResult();

                resultLsl.Caption = Language.Get("ValueFieldName"); //위반한 컬럼의 다국어
                resultLsl.FailMessage = Language.GetMessage("LessThen", resultLsl.Caption, Language.Get("MaxValueFieldName")).Message;
                resultLsl.Id = "ValueFieldName";
                resultLsl.IsSucced = false;

                result.Add(resultLsl);
            }

            return result;
        }

        /// <summary>
        /// 그리드 팝업 설정 예제
        /// </summary>
        private void InitializeGrid_Popup()
        {
            //팝업 컬럼 설정
            var parentAreaPopupColumn = this.grdGrid.View.AddSelectPopupColumn("parentAreaName", 100, new SqlQuery("GetAreaList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "parentAreaId")
               .SetPopupLayout("parentAreaName", PopupButtonStyles.Ok_Cancel, false, true)  // 팝업창 UI 설정. 
               .SetPopupResultCount(1)      // 팝업창 결과 선택 가능한 개수
               .SetPopupResultMapping("parentAreaName", "DICTIONARYNAME")   // 팝업창 그리드의 컬럼이름과 현재 그리드의 컬럼 이름이 다르면 매핑해줘야한다.
               .SetPopupResultMapping("parentAreaId", "AREAID")
               .SetPopupApplySelection((selectedRows, dataGridRow) =>
               {
                   // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
                   // dataGridRow : 현재 Focus가 있는 그리드의 DataRow
               })
               .SetPopupValidationCustom(ValidationParentAreaIdPopup);
            ;

            //팝업 조회조건
            parentAreaPopupColumn.Conditions.AddComboBox("plantId", new SqlQuery("GetCondCodePlant", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetEmptyItem()
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                .SetLabel("plant")
                .SetPopupDefaultByGridColumnId("PLANTID");

            //팝업 그리드
            parentAreaPopupColumn.GridColumns.AddTextBoxColumn("AREAID", 150)
                .SetValidationKeyColumn()
                .SetLabel("areaId");
            parentAreaPopupColumn.GridColumns.AddTextBoxColumn("DICTIONARYNAME", 300)
                .SetLabel("areaName");

        }

        /// <summary>
        /// 팝업에서 선택할때 유효성 검사루틴
        /// </summary>
        /// <param name="currentGridRow">그리드의 현재 ROW</param>
        /// <param name="popupSelections">팝업에서 선택한 ROW</param>
        /// <returns></returns>
        private ValidationResultCommon ValidationParentAreaIdPopup(DataRow currentGridRow, IEnumerable<DataRow> popupSelections)
        {
            ValidationResultCommon result = new ValidationResultCommon();

            //DataRow.ToStringNullToEmpty("FieldName") : null, DBNull.Value 일 경우 빈문자열을 리턴해주는 함수입니다.

            string myAreaId = currentGridRow.ToStringNullToEmpty("AREAID");
            if (popupSelections.Any(s => s.ToStringNullToEmpty("AREAID") == myAreaId))
            {
                Language.LanguageMessageItem item = Language.GetMessage("MESSAGEID", myAreaId);
                result.IsSucced = false;
                result.FailMessage = item.Message;
                result.Caption = item.Title;
            }

            return result;
        }

        /// <summary>
        /// 차트 초기화
        /// </summary>
        private void SearchChart(DataTable dataSource)
        {
            //TODO : 차트를 초기화 하세요
            chart.ClearSeries();

            chart.AddBarSeries("AlarmCount", dataSource)
                .SetX_DataMember(ScaleType.Qualitative, "EQUIPMENTID")
                .SetY_DataMember(ScaleType.Numerical, "ALARMCOUNT")
                .SetCrosshairPattern(true, "{A} : {V}");

            chart.AddLineSeries("Alarm", new DataTable())
                .SetX_DataMember(ScaleType.Qualitative, "EQUIPMENTID")
                .SetY_DataMember(ScaleType.Numerical, "ALARMCOUNT")
                .SetMultiSeries(true, "ALARMID")
                .SetCrosshairPattern(false)
                .AddSecondaryAxisY();

            chart.SetCrosshairLineVisiblity(false, false)
                .SetTitle("DICTIONARYID");

            chart.PopulateSeries();         // 차트 설정 후 호출 (설정에 기반하여 UI 구성, 반드시 호출해야 함)

            chart.SetDynamicTitle("DICTIONARYID");

            // 차트 선택 시 처리 (참고 사이트 : https://documentation.devexpress.com/WindowsForms/DevExpress.XtraCharts.ChartControl.SelectedItemsChanged.event)
            chart.SeriesSelectionMode = SeriesSelectionMode.Point;      // Argument : X축 전체, Point : 특정 포인트, Series : 특정 시리즈
            chart.SelectionMode = ElementSelectionMode.Single;          // Single : 단일, Multiple : 복수, Extended : 복수(Shift 키 조합), None : 사용안함
            chart.SelectedItemsChanged += Chart_SelectedItemsChanged;

            /* 시리즈 추가 
             * AddBarSeries : 막대형 그래프로 표현하는 경우
             * AddGanttSeries : 간트형 그래프로 표현하는 경우
             * AddLineSeries : 선형 그래프로 표현하는 경우
             * AddPie3DSeries : 3D 원형 그래프로 표현하는 경우
             * AddPieSeries : 원형 그래프로 표현하는 경우
             * AddRangeBarSeries : 여러 상태를 하나의 라인으로 보여주는 간트형 그래프로 표현하는 경우
             * AddStackedBarSeries : 누적 막대형 그래프로 표현하는 경우
            */

            /* 시리즈 기본 설정 
             * Set~ : 시리즈 구성 조건 설정
             * AddSecondaryAxisY : 차트에 시리즈 종류가 여러개인 경우 Y축 정보를 추가하는 함수 (Bar, StackedBar, Line 인 경우 사용)
             * SetCrosshairPattern : Crosshair 표시 형식 설정
             * SetEachColor : X축 항목 별 다른 색 사용 설정 (Bar 인 경우 사용)
             * SetExplodeMode : 데이터 분리 모드 설정 (Pie, 3DPie 인 경우 사용)
             * SetLegendTextPattern : 범례 표시 형식 설정
             * SetLineMarker : 포인트에 마커 표시 설정 (Line 인 경우 사용)
             * SetMultiSeries : 여러개의 시리즈를 생성하기 위한 설정
             * SetSeriesColor : 색상을 지정된 색상으로 설정 (HTML HEX 값으로 데이터 전달)
             * SetSeriesRandomColor : RGB 값을 임의로 생성한 색상으로 설정
             * SetToolTipPattern : 툴팁 표시 형식 설정
             * SetX_DataMember : X축 설정
             * SetY_DataMember : Y축 설정
            */

            /* 차트 기본 설정
             * ClearSeries : 시리즈 초기화
             * GetSelectionPoints : 선택 영역 선택 기능 사용 시 선택한 Series Point의 값을 가져옴
             * SetAxisInteger : Y축 값 Scale 정수형 설정
             * SetAxisXCustomSort : 조회한 데이터의 특정 필드 값 기준 정렬 사용 설정 (Rank 개념이 들어가는 차트 등)
             * SetAxisXSort : X축 항목 값이 문자열 인 경우 정렬 사용 설정
             * SetAxisXTitle : X축 타이틀 설정
             * SetAxisYTitle : Y축 타이틀 설정
             * SetCrosshairLineVisibility : Crosshair 라인 표시 여부 설정
             * SetCustomSecondaryAxisLabel : Secondary Axis를 사용하는 경우 Label Pattern 설정
             * SetDynamicTitle : 차트 타이틀 동적 설정 (PopuplateSeries 호출 후 사용 가능, 타이틀을 사용하도록 설정된 경우에만 사용 가능)
             * SetEmptyChart : 데이터를 조회하긴 전 초기 빈 차트 구성
             * SetRotated : 차트 회전 여부 설정
             * SetTitle : 차트 타이틀 설정 (DictionaryId 사용 시 다국어 적용)
             * SetUseRectangleSelection : 선택 영역 선택 기능 사용 여부 설정
             * SetUseSeriesPointSelection : 선택한 Series Point의 정보를 Label로 보여줄지 여부 설정
             * SetUseZoomAndScroll : 확대 및 스크롤 사용 여부 설정 (Ctrl + MouseWheel : 확대/축소, MouseWheel : 세로 스크롤, Shift + MouseWheel : 가로 스크롤)
             * SetVisibleOptions : 차트의 X축/Y축 라벨, 범례를 보여줄지 여부 설정
             * SetAxisXZoom : X축 확대 설정 (사용 안함)
             * SetAxisYZoom : Y축 확대 설정 (사용 안함)
            */

            /* Crosshair, ToolTip, LegendText Pattern 예약어
             * {A} : X축 이름
             * {S} : 시리즈 이름
             * {V} : 값
             * {V1} : 첫번째 값(Range인 경우)
             * {V2} : 두번째 값(Range인 경우)
             * {FieldName} : Series.DataSource의 특정 필드 값
            */
        }

        private void InitializePivotGrid()
        {
            //TODO : 피벗 그리드를 초기화 하세요
            pivotGrid.AddRowField("ALARMEQUIPMENTID", 120)
                .SetDefaultSortOrder("ALARMCOUNT");
            pivotGrid.AddRowField("ALARMEQUIPMENTNAME", 200);
            pivotGrid.AddColumnField("REPORTTIME");
            pivotGrid.AddFilterField("STARTTIME");
            pivotGrid.AddFilterField("ENDTIME");

            // SummaryField, UnboundField 예제
            // ALARMCOUNT 값을 기준으로 합계를 보여주는 Summary 필드 구성
            this.pivotGrid.AddSummaryField("ALARMCOUNT", "ALARMCOUNTSUM", DevExpress.Data.PivotGrid.PivotSummaryType.Sum)
                .SetCellFormat(FormatType.Numeric, "n0");

            // STARTTIME, ENDTIME 컬럼이 조회한 데이터 테이블에 있는 경우 두 시간의 차이를 Unbound 필드로 구성
            this.pivotGrid.AddUnboundField("ELAPSEDTIME", DevExpress.Data.UnboundColumnType.Integer, DevExpress.XtraPivotGrid.PivotArea.DataArea)
                .SetUnboundFieldAction((UnboundColumnArgs args) =>
                {
                    DateTime startTime = args.GetColumnValue<DateTime>("STARTTIME");
                    DateTime endTime = args.GetColumnValue<DateTime>("ENDTIME");

                    int elapsedTime = (int)(endTime - startTime).TotalMinutes;


                    args.Value = elapsedTime;
                })
                .SetCellFormat(FormatType.Numeric, "n0");
            

            pivotGrid.SetDataField(DevExpress.XtraPivotGrid.PivotDataArea.RowArea, "INDEX")
                .SetShowRowTotal()
                .SetShowColumnTotal()
                .SetGrandTotalCaption("Total")
                .SetShowTotalField(new string[] { "ALARMEQUIPMENTID" });

            pivotGrid.PopulateFields();

            /* 필드 추가
             * AddRowField : Row Area에 필드 추가
             * AddColumnField : Column Area에 필드 추가
             * AddDataField : Data Area에 필드 추가
             * AddFilterField : Filter Area에 필드 추가
             * AddSummaryField : 특정 데이터 필드의 집계 정보를 보여줄 필드
             * AddUnboundField : DB에서 조회하여 바인딩한 데이터를 별도로 가공하여 그리드에 보여줄 필드
            */

            /* 필드 기본 설정
             * SetCellFormat : Cell 데이터의 포맷 셋팅 (DataField에 속한 경우 사용)
             * SetValueFormat : Field Value의 포맷 셋팅 (RowField, ColumnField에 속한 경우 사용)
             * SetGroupInterval : 필드의 값을 특정 간격으로 그룹핑 설정 (RowField, ColumnField에 속한 경우 사용)
             * SetUnboundFieldAction : Unbound 필드에 보여줄 데이터를 가공하는 로직 설정
             * SetDefaultSortOrder : 기본 정렬 설정 (RowField에 속한 필드에서 호출, 파라미터로 정렬 할 DataField 필드명 사용)
            */

            /* 그리드 기본 설정
             * SetDataField : 현재 설정된 데이터 필드의 그룹핑 설정
             * SetShowRowTotal : Row 영역의 합계를 보여줄지 여부 설정
             * SetShowColumnTotal : Column 영역의 합계를 보여줄지 여부 설정
             * SetGrandTotalCaption : 전체 합계의 캡션 및 캡션 변경 여부 설정
             * SetShowTotalField : 합계의 기준이 되는 필드 설정
            */
        }

        /// <summary>
        /// 커스텀 팝업 초기화, 결과값 추가작업 예시
        /// </summary>
        private void InitializeGrid_CustomPopupParamter()
        {
            //this.grdEventParameter.View.AddSelectPopupColumn("CONDITIONFORMULA", 120, new Fdc.ConditionFormulaPopup())
            //   .SetPopupCustomParameter(
            //       (ISmartCustomPopup sender, DataRow currentRow) => //초기화 작업
            //        {
            //           Fdc.ConditionFormulaPopup formulaPopup = (Fdc.ConditionFormulaPopup)sender;
            //            //formulaPopup에 하고싶은 작업을 하세요.
            //        },
            //       (ISmartCustomPopup sender, DataRow currentRow) => //결과 리턴 작업
            //        {
            //           Fdc.ConditionFormulaPopup formulaPopup = (Fdc.ConditionFormulaPopup)sender;
            //            //formulaPopup에 하고싶은 작업을 하세요.
            //        }
            //   )
            //조건에 다라 팝업을 띄우지 않을수 있습니다.
            //.SetPopupQueryPopup((DataRow currentrow) =>
            // {
            //     if (string.IsNullOrWhiteSpace(currentrow.ToStringNullToEmpty("EQUIPMENTID")))
            //     {
            //         this.ShowMessage("설비를 먼저 선택하세요"); //다국어 처리
            //         return false; //팝업을 띄우지 않음
            //     }

            //     return true; //팝업을 띄웁니다.
            // });

        }

        // 그리드 그룹 구성 예제
        private void InitializeGrid_Group()
        {
            this.grdGrid.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect; // 다중체크박스처리
            this.grdGrid.View.SetSearchCondition(this.Conditions);

            //헤더 RowCount = 3개인 예제
            var mainGroup = this.grdGrid.View.AddGroupColumn("mainGroup"); //1Row, All Column

            //2Row, 1Column 
            var pkGroup = mainGroup.AddGroupColumn("PkGroup")
                .SetLabel("AREAID", "");

            //2Row, 1Column 의 실제 컬럼들
            pkGroup.AddTextBoxColumn("AREAID", 100)
                             .SetValidationKeyColumn()
                             .SetValidationIsRequired()
                             .SetMaxLength(50, true);

            pkGroup.AddTextBoxColumn("AREANAME", 100)
                             .SetIsHidden();

            pkGroup.AddLanguageColumn("AREANAME", 160)
                             .SetMaxLength(160);

            pkGroup.AddTextBoxColumn("DESCRIPTION", 200)
                             .SetMaxLength(2500);

            //2Row, 2Column 
            var plnatGroup = mainGroup.AddGroupColumn("PlnatGroup")
                .SetLabel("PLANTID", "");

            //2Row, 2Column 의 실제 컬럼들
            plnatGroup.AddComboBoxColumn("PLANTID", 100, new SqlQuery("GetPlantList", "00001", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}"), "PLANTNAME", "PLANTID")
                             .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false)
                             .SetDefault(UserInfo.Current.Plant, "p_plantId") //기본값 설정 UserInfo.Current.Plant
                             .SetLabel("plant");

            plnatGroup.AddComboBoxColumn("AREATYPE", 100, new SqlQuery("GetCodeList", "00001", "codeClassId=AREATYPE", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "DICTIONARYNAME");

            
            plnatGroup.AddComboBoxColumn("VALIDSTATE", 100, new SqlQuery("GetCodeList", "00001", "codeClassId=VALIDSTATE", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "DICTIONARYNAME")
                             .SetDefault("Valid")
                             .SetValidationIsRequired();

            //1~2Row, 3Column : RowCount(2)로 Row Merge가 발생
            var etcGroup = this.grdGrid.View.AddGroupColumn("EtcGroup")
              .SetLabel("PLANTID")
              .SetRowSpan(2);

            etcGroup.AddTextBoxColumn("CREATOR", 80)
                             .SetIsReadOnly();
            etcGroup.AddTextBoxColumn("CREATEDTIME", 150)
                             .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                             .SetDefault("")
                             .SetIsReadOnly();
            etcGroup.AddTextBoxColumn("MODIFIER", 80)
                             .SetIsReadOnly();
            etcGroup.AddTextBoxColumn("MODIFIEDTIME", 150)
                             .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                             .SetDefault("")
                             .SetIsReadOnly();

            this.grdGrid.View.PopulateColumns();
        }

        #endregion

        #region Private Function

        #endregion

        #region 공통팝업 버튼에서 직접 사용

        private void ButtonPopup(Dictionary<string, object> param)
        {
            // 팝업 컬럼 설정
            //var traceParameterListPopup = this.CreateSelectPopup("PARAMETERID", new SqlQuery("GetTraceParameterByEqupment", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"EQUIPMENTID={param["equipmentId"]}", $"EQUIPMENTNAME={param["equipmentName"]}"))
            //                                    .SetMultiGrid(true, new SqlQuery("GetTraceParameterByEquipmentMapping", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"EQUIPMENTID={param["equipmentId"]}", $"EQUIPMENTNAME={param["equipmentName"]}"))
            //                                    .SetPopupLayout("TRACEPARAMETERLIST", PopupButtonStyles.Apply_Close, true, false)
            //                                    .SetPopupResultCount(0)
            //                                    .SetPopupLayoutForm(1200, 700, System.Windows.Forms.FormBorderStyle.FixedToolWindow);

            //// 팝업 조회조건
            //traceParameterListPopup.Conditions.AddTextBox("EQUIPMENTID")
            //                         .SetLabel("EQUIPMENTID")
            //                         .SetDefault(param["equipmentId"])
            //                         .SetIsReadOnly();

            //traceParameterListPopup.Conditions.AddTextBox("EQUIPMENTNAME")
            //                         .SetLabel("EQUIPMENTNAME")
            //                         .SetDefault(param["equipmentName"])
            //                         .SetIsReadOnly();

            //traceParameterListPopup.Conditions.AddComboBox("PARAMETERLEVEL", new SqlQuery("GetCodeList", "00001", "CODECLASSID=ParameterLevel", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "DICTIONARYNAME", "CODEID")
            //                         .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
            //                         .SetEmptyItem();

            //traceParameterListPopup.Conditions.AddTextBox("PARAMETERIDORNAME");

            //// 팝업 그리드
            //traceParameterListPopup.SetIsReadOnly();
            //traceParameterListPopup.SetPopupAutoFillColumns("PARAMETERNAME");

            //traceParameterListPopup.GridColumns.AddTextBoxColumn("PARAMETERID", 160)
            //                                   .SetValidationKeyColumn();
            //traceParameterListPopup.GridColumns.AddTextBoxColumn("PARAMETERNAME", 160);
            //traceParameterListPopup.GridColumns.AddTextBoxColumn("PARAMETERLEVEL", 150);
            //traceParameterListPopup.GridColumns.AddTextBoxColumn("DATAUNIT", 50)
            //                                   .SetIsHidden();
            //traceParameterListPopup.GridColumns.AddTextBoxColumn("PARAMETERUNIT", 50)
            //                                   .SetIsHidden();
            //traceParameterListPopup.GridColumns.AddTextBoxColumn("CREATOR", 50)
            //                                   .SetIsHidden();
            //traceParameterListPopup.GridColumns.AddTextBoxColumn("CREATEDTIME", 50)
            //                                   .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
            //                                   .SetIsHidden();
            //traceParameterListPopup.GridColumns.AddTextBoxColumn("MODIFIER", 50)
            //                                   .SetIsHidden();
            //traceParameterListPopup.GridColumns.AddTextBoxColumn("MODIFIEDTIME", 50)
            //                                   .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
            //                                   .SetIsHidden();

            //DataTable mappingTable = grdTraceGroupMapping.DataSource as DataTable;
            //var filter = (grdTraceGroupMapping.View.DataSource as DataView).RowStateFilter;

            //IEnumerable<DataRow> selectedDatas = this.ShowPopup(traceParameterListPopup, mappingTable.Rows.Cast<DataRow>().Where(m => m.RowState != DataRowState.Deleted));
            //if (selectedDatas == null) return; //null이면 취소 또는 닫기 클릭한것임.

            //_traceGroupMappingDataSource은 최초 검색했을때 변수에 담아두어 유지하고 잇어야함. 
            //grdTraceGroupMapping.SetDataSourceRemainRowStatus( //DataTable의 RowStatus 즉 추가, 삭제, 수정 형태를 유지해주는 메소드
            //    DataSourceHelper.MappingChanged(_traceGroupMappingDataSource, selectedDatas, new List<string>() { "PARAMETERID" }) //원래것과 매핑결과를 비교하여 새로운 DataSource를 생성해 준다
            //);


        }

        #endregion
    }
}
