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

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프 로 그 램 명  : 기준정보 > Setup > 표준공정 사용조회
    /// 업  무  설  명  : 표준공정을 사용하는 품목을 조회한다.
    /// 생    성    자  : 한주석
    /// 생    성    일  : 2019-12-06
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ProcessSegmentUseStatus : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가
        string _Text;                                       // 설명
        SmartBandedGrid grdList = new SmartBandedGrid();    // Main Grid

        #endregion

        #region 생성자

        public ProcessSegmentUseStatus()
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
        }

        /// <summary>        
        /// 코드그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdProcessSegment.GridButtonItem = GridButtonItem.Export;

            // 품목 ID
            grdProcessSegment.View.AddTextBoxColumn("PROCESSDEFID", 100)
                .SetLabel("ITEMCODE")
                .SetTextAlignment(TextAlignment.Center);
            // 품목 version  
            grdProcessSegment.View.AddTextBoxColumn("PROCESSDEFVERSION", 50)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("ITEMVERSION");
            // 품목 명
            grdProcessSegment.View.AddTextBoxColumn("PRODUCTDEFNAME", 200)
                 .SetLabel("ITEMNAME");
            // SITE
            grdProcessSegment.View.AddTextBoxColumn("PLANTID", 60)
                .SetTextAlignment(TextAlignment.Center);
            // 공정수순
            grdProcessSegment.View.AddTextBoxColumn("USERSEQUENCE", 70)
                .SetTextAlignment(TextAlignment.Right);
            // 공정 ID
            grdProcessSegment.View.AddTextBoxColumn("PROCESSSEGMENTID", 80)
                .SetTextAlignment(TextAlignment.Center);
            // 공정 명
            grdProcessSegment.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
                //.SetTextAlignment(TextAlignment.Center);
            // UOM
            //grdProcessSegment.View.AddTextBoxColumn("PROCESSUOM", 60)
            //    .SetTextAlignment(TextAlignment.Center);
			//고객사
			grdProcessSegment.View.AddTextBoxColumn("CUSTOMER", 80);
			// DESCRIPTION
			grdProcessSegment.View.AddTextBoxColumn("DESCRIPTION", 200)
                .SetLabel("SPECIALNOTE");

            grdProcessSegment.View.PopulateColumns();
        }

        #endregion


        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            grdList.View.AddingNewRow += View_AddingNewRow;
        }

        /// <summary>
        /// 코드그룹 리스트 그리드에서 추가 버튼 클릭 시 호출
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            throw new NotImplementedException();
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
            DataTable changed = grdList.GetChangedRows();

            ExecuteRule("SaveCodeClass", changed);
        }

        #endregion

        #region 검색

        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {

            await base.OnSearchAsync();

            var values = Conditions.GetValues();

            object processsegmentid = Conditions.GetControl<SmartSelectPopupEdit>("p_processsegmentid").EditValue;
            object productid = Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").EditValue;

            // TODO : 조회 SP 변경

            if (productid == null)
            {
                if (processsegmentid == null)
                {
                    throw MessageException.Create("RequiredSearch");
                }
                else if (processsegmentid.Equals(""))
                {
                    throw MessageException.Create("RequiredSearch");
                }
            }
            else if (productid.Equals(""))
            {
                if (processsegmentid == null)
                {
                    throw MessageException.Create("RequiredSearch");
                }
                else if (processsegmentid.Equals(""))
                {
                    throw MessageException.Create("RequiredSearch");
                }
            }



            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtCodeClass = SqlExecuter.Query("SelectProcessUseList", "10002", values);

            if (dtCodeClass.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdProcessSegment.DataSource = dtCodeClass;
        }




        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();
           // InitializeConditionPopup_ProcessSegment();
            InitializeConditionPopup_ProcessSegment2();
            // TODO : 조회조건 추가 구성이 필요한 경우 사용
        }


        private void InitializeConditionPopup_ProcessSegment()
        {
            var popupProcessSegment = Conditions.AddSelectPopup("p_processsegmentid",
                                                               new SqlQuery("GetProcessSegmentListByOsp", "10001"
                                                                               , $"LANGUAGETYPE={ UserInfo.Current.LanguageType }"
                                                                               , $"ENTERPRISEID={UserInfo.Current.Enterprise}"
                                                                               ), "PROCESSSEGMENTNAME", "PROCESSSEGMENTID")
            .SetPopupLayout("PROCESSSEGMENTID", PopupButtonStyles.Ok_Cancel, true, false)
            .SetPopupLayoutForm(600, 800)
            .SetLabel("PROCESSSEGMENTID")
            .SetPopupResultCount(1)
            .SetPosition(1.0);

            // 팝업 조회조건
            popupProcessSegment.Conditions.AddTextBox("PROCESSSEGMENTNAME")
                .SetLabel("PROCESSSEGMENTIDNAME");

            // 팝업 그리드
            popupProcessSegment.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150)
                .SetValidationKeyColumn();
            popupProcessSegment.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);

        }

        private void InitializeConditionPopup_ProcessSegment2()
        {

            //팝업 컬럼 설정
            var parentPopupColumn = Conditions.AddSelectPopup("P_PRODUCTDEFID", new SqlQuery("GetProductItemGroup", "10002", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "ITEMID", "ITEMID")
               .SetPopupLayout("ITEMID", PopupButtonStyles.Ok_Cancel, true, false)  //팝업창 UI 설정.
               .SetPopupResultCount(1)  //팝업창 선택가능한 개수
               .SetPosition(0.1)
               .SetLabel("ITEMID")
               .SetPopupApplySelection((selectRow, gridRow) => {

                   List<string> productVersionList = new List<string>();
                   List<string> productNameList = new List<string>();

                   selectRow.AsEnumerable().ForEach(r => {
                       productVersionList.Add(Format.GetString(r["ITEMVERSION"]));
                       productNameList.Add(Format.GetString(r["ITEMNAME"]));
                   });

                   Conditions.GetControl<SmartTextBox>("P_PRODUCTDEFVERSION").EditValue = string.Join(",", productVersionList);
                   Conditions.GetControl<SmartTextBox>("P_PRODUCTDEFNAME").EditValue = string.Join(",", productNameList);
               });

            parentPopupColumn.Conditions.AddComboBox("MASTERDATACLASSID", new SqlQuery("GetTypeList", "10001", $"CODECLASSID=ProductDivision2", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                .SetDefault("Product");
            parentPopupColumn.Conditions.AddTextBox("ITEMID");
            parentPopupColumn.Conditions.AddTextBox("ITEMNAME");

            //팝업 그리드
            parentPopupColumn.GridColumns.AddTextBoxColumn("ITEMID", 150);
            parentPopupColumn.GridColumns.AddTextBoxColumn("ITEMNAME", 200);
            parentPopupColumn.GridColumns.AddTextBoxColumn("ITEMVERSION", 100);
            parentPopupColumn.GridColumns.AddTextBoxColumn("SPEC", 250);

            var popupProcessSegment = Conditions.AddSelectPopup("p_processsegmentid", new SelectWIPPopup(), "p_processsegmentid", "p_processsegmentid")
               // .SetPopupLayout("PROCESSSEGMENTID", PopupButtonStyles.Ok_Cancel, true, false)
                //.SetPopupLayoutForm(600, 800)
                .SetLabel("PROCESSSEGMENTID")
                .SetPopupCustomParameter(
                    (ISmartCustomPopup sender, DataRow currentRow) => //초기화 작업
                    {
                        (sender as SelectWIPPopup).SetParams(
                            Conditions.GetControl("p_processsegmentid").Text
                        );
                        (sender as SelectWIPPopup).SelectedRowEvent += (dr) => SetCondition(dr);
                    })
                //.SetPopupResultCount(1)
                .SetPosition(4); ;
            
        }

        private void SetCondition(DataRow dr)
        {
            Conditions.GetControl<SmartSelectPopupEdit>("p_processsegmentid").SetValue(dr[0].ToString());
        }
        
        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
            Conditions.GetControl<SmartTextBox>("P_PRODUCTDEFNAME").ReadOnly = true;
            Conditions.GetControl<SmartTextBox>("P_PRODUCTDEFVERSION").ReadOnly = true;

            Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").EditValueChanged += ProductDefId_EditValueChanged;
        }

        private void ProductDefId_EditValueChanged(object sender, EventArgs e)
        {
            SmartSelectPopupEdit PopProdutid = sender as SmartSelectPopupEdit;

            if (Format.GetFullTrimString(PopProdutid.EditValue).Equals(string.Empty))
            {
                Conditions.GetControl<SmartTextBox>("P_PRODUCTDEFNAME").EditValue = string.Empty;
                Conditions.GetControl<SmartTextBox>("P_PRODUCTDEFVERSION").EditValue = string.Empty;
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
            grdList.View.CheckValidation();

            DataTable changed = grdList.GetChangedRows();

            if (changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
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

        #endregion
    }
}
