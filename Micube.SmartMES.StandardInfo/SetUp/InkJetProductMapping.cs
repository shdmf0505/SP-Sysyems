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
    /// 프 로 그 램 명  : 기준정보 > Set Up > InkJet 품목 매핑
    /// 업  무  설  명  : 
    /// 생    성    자  : 조혜인
    /// 생    성    일  : 2019-12-18
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class InkJetProductMapping : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가
       

        #endregion

        #region 생성자

        public InkJetProductMapping()
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

        /// <summary>        
        /// 코드그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdInkJetMapping.GridButtonItem = GridButtonItem.All ;
            grdInkJetMapping.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            //Inkjet 코드
            grdInkJetMapping.View.AddComboBoxColumn("INKJETID", 150,
                new SqlQuery("GetCodeList", "00001", "CODECLASSID=InkJetID",
                    $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetValidationIsRequired();
            //품목 ID
            InitializeGrid_ProductDefNamePopup();
            grdInkJetMapping.View.AddTextBoxColumn("PRODUCTDEFNAME", 200).SetIsReadOnly();
            grdInkJetMapping.View.AddTextBoxColumn("PRODUCTDEFVERSION", 100).SetIsReadOnly().SetValidationIsRequired();
            //고객사
            grdInkJetMapping.View.AddTextBoxColumn("CUSTOMERID", 200).SetIsReadOnly();
            grdInkJetMapping.View.AddTextBoxColumn("CUSTOMERNAME", 200).SetIsReadOnly();
            //설명
            grdInkJetMapping.View.AddTextBoxColumn("DESCRIPTION", 200);
            //SITE
            grdInkJetMapping.View.AddTextBoxColumn("PLANTID", 200).SetIsHidden();
            //생성자, 수정자, 생성시간, 수정시간
            grdInkJetMapping.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(Framework.SmartControls.TextAlignment.Center);
            grdInkJetMapping.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(Framework.SmartControls.TextAlignment.Center);
            grdInkJetMapping.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(Framework.SmartControls.TextAlignment.Center);
            grdInkJetMapping.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(Framework.SmartControls.TextAlignment.Center);


            grdInkJetMapping.View.PopulateColumns();
        }

        /// <summary>
        /// 팝업형 그리드 컬럼 - 제품 명
        /// </summary>
        private void InitializeGrid_ProductDefNamePopup()
        {
            var productDefPopupColumn = grdInkJetMapping.View.AddSelectPopupColumn("PRODUCTDEFID", 250, new SqlQuery("GetProductDefList", "10006", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"))
                .SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupResultCount(1)
                .SetValidationKeyColumn()
                .SetValidationIsRequired()
                .SetPopupLayoutForm(800, 800, FormBorderStyle.FixedToolWindow)
                .SetPopupAutoFillColumns("PRODUCTDEFID")
                .SetPopupApplySelection((selectRow, gridRow) => {
                    DataRow selectedRow = selectRow.AsEnumerable().FirstOrDefault();
                    gridRow["PRODUCTDEFNAME"] = selectedRow == null ? "" : Format.GetString(selectedRow["PRODUCTDEFNAME"]);
                    gridRow["PRODUCTDEFVERSION"] = selectedRow == null ? "" : Format.GetString(selectedRow["PRODUCTDEFVERSION"]);
                    gridRow["CUSTOMERID"] = selectedRow == null ? "" : Format.GetString(selectedRow["CUSTOMERID"]);
                    gridRow["CUSTOMERNAME"] = selectedRow == null ? "" : Format.GetString(selectedRow["CUSTOMERNAME"]);
                });

            productDefPopupColumn.Conditions.AddTextBox("PRODUCTDEF");

            productDefPopupColumn.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            productDefPopupColumn.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 80);
            productDefPopupColumn.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 150);
            productDefPopupColumn.GridColumns.AddTextBoxColumn("CUSTOMERID", 80).SetIsHidden();
            productDefPopupColumn.GridColumns.AddTextBoxColumn("CUSTOMERNAME", 150);
        }


        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
           
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
            DataTable changed = grdInkJetMapping.GetChangedRows();
            foreach (DataRow changedRow in changed.Rows)
            {
                if (string.IsNullOrEmpty(changedRow["PLANTID"].ToString()))
                {
                    changedRow["PLANTID"] = UserInfo.Current.Plant;
                }

                if (changedRow["_STATE_"].Equals("modified"))
                {
                    throw MessageException.Create("CURRENTPROCESSUUPDATEDATA"); // 수정불가
                }
            }
            
            ExecuteRule("InkJetProduct", changed);
        }

        #endregion

        #region 검색

        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            // TODO : 조회 SP 변경
            var values = Conditions.GetValues();
            values.Add("LanguageType", UserInfo.Current.LanguageType);
            values.Add("PLANTID", UserInfo.Current.Plant);

            DataTable dtCodeClass = await QueryAsync("SelectInkJetProductMapping", "10001", values);

            if (dtCodeClass.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdInkJetMapping.DataSource = dtCodeClass;
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            //inkjet ID
            InitializeConditionInkJetIdListCombo();
            //고객사 ID
            InitializeConditionCustomerDefId_Popup();
            //품목 ID
            InitializeConditionProductDefId_Popup();
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
        }

        private void InitializeConditionInkJetIdListCombo()
        {
            var param = new Dictionary<string, object>();
            param.Add("CODECLASSID", "InkJetID");
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            var conditionCustomerId = Conditions.AddComboBox("P_INKJETID", new SqlQuery("GetCodeList", "00001", param)).SetEmptyItem()
                .SetLabel("INKJETID").SetPosition(0.1);
            
        }

        /// <summary>
        /// 팝업형 조회조건 생성 - 고객사
        /// </summary>
        private void InitializeConditionCustomerDefId_Popup()
        {
            var conditionCustomerId = Conditions.AddSelectPopup("P_CUSTOMERID", new SqlQuery("GetCustomerList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "CUSTOMERNAME", "CUSTOMERID")
                .SetPopupLayout("SELECTCUSTOMERID", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupResultCount(0)
                .SetLabel("CUSTOMERID")
                .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                .SetPopupAutoFillColumns("CUSTOMERNAME")
                .SetPosition(0.2);

            conditionCustomerId.Conditions.AddTextBox("TXTCUSTOMERID");

            //고객 ID
            conditionCustomerId.GridColumns.AddTextBoxColumn("CUSTOMERID", 80);
            //고객명
            conditionCustomerId.GridColumns.AddTextBoxColumn("CUSTOMERNAME", 150);
        }

        /// <summary>
        /// 팝업형 조회조건 생성 - 품목코드
        /// </summary>
        private void InitializeConditionProductDefId_Popup()
        {
            var conditionProductDefId = Conditions.AddSelectPopup("P_PRODUCTDEFID", new SqlQuery("GetProductDefList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PRODUCTDEFNAME", "PRODUCTDEFID")
                .SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupResultCount(1)
                .SetLabel("PRODUCTDEFID")
                .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                .SetPopupAutoFillColumns("PRODUCTDEFNAME")
                .SetPosition(0.3);

            conditionProductDefId.Conditions.AddTextBox("PRODUCTDEF");

            conditionProductDefId.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            conditionProductDefId.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 80);
            conditionProductDefId.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 150);
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
            grdInkJetMapping.View.CheckValidation();

            DataTable changed = grdInkJetMapping.GetChangedRows();

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
