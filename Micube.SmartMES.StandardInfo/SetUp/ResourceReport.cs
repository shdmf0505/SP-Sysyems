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
    /// 프 로 그 램 명  : 기준정보 > 품목관리 > 자원 사용 품목조회
    /// 업  무  설  명  : 
    /// 생    성    자  : 신상철
    /// 생    성    일  : 2020-01-09
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ResourceReport : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가
       

        #endregion

        #region 생성자

        public ResourceReport()
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
            grdResource.GridButtonItem = GridButtonItem.Export ;
            grdResource.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            // 품목 ID
            grdResource.View.AddTextBoxColumn("PRODUCTDEFID", 100)
                .SetLabel("ITEMCODE")
                .SetTextAlignment(TextAlignment.Center);
            // 품목 version  
            grdResource.View.AddTextBoxColumn("PRODUCTDEFVERSION", 50)
                .SetLabel("ITEMVERSION")
                .SetTextAlignment(TextAlignment.Center);
            // 품목 명
            grdResource.View.AddTextBoxColumn("PRODUCTDEFNAME", 200)
                .SetLabel("ITEMNAME");
            // SITE
            grdResource.View.AddTextBoxColumn("PLANTID", 60)
                .SetTextAlignment(TextAlignment.Center);
            // 공정수순
            grdResource.View.AddTextBoxColumn("USERSEQUENCE", 70)
                .SetTextAlignment(TextAlignment.Right);
            // 공정 ID
            grdResource.View.AddTextBoxColumn("PROCESSSEGMENTID", 70)
                .SetTextAlignment(TextAlignment.Center);
            // 공정 명
            grdResource.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150);
            //// UOM
            //grdResource.View.AddTextBoxColumn("PROCESSUOM", 60)
            //    .SetTextAlignment(TextAlignment.Center);
            //// DESCRIPTION
            //grdResource.View.AddTextBoxColumn("DESCRIPTION", 200)
            //    .SetLabel("SPECIALNOTE");

            //자원관리 팝업
            grdResource.View.AddTextBoxColumn("RESOURCEID", 110)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();
            this.grdResource.View.AddTextBoxColumn("RESOURCENAME", 250).SetIsReadOnly();

            //this.grdResource.View.AddTextBoxColumn("DESCRIPTION", 300);

            //주작업 여부
            this.grdResource.View.AddTextBoxColumn("ISPRIMARYRESOURCE", 80)
                .SetTextAlignment(TextAlignment.Center);

            this.grdResource.View.AddTextBoxColumn("CREATOR", 80)
                // ReadOnly 컬럼 지정
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            this.grdResource.View.AddTextBoxColumn("CREATEDTIME", 130)
                // Display Format 지정
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdResource.View.PopulateColumns();
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

            object productid = Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").EditValue;
            object resourceid = Conditions.GetControl<SmartSelectPopupEdit>("RESOURCEIDNAME").EditValue;

            if (productid == null)
            {
                if(resourceid ==null)
                {
                    throw MessageException.Create("RequiredSearch");
                }
                else if(resourceid.Equals(""))
                {
                    throw MessageException.Create("RequiredSearch");
                }               
            }
            else if (productid.Equals(""))
            {
                if (resourceid == null)
                {
                    throw MessageException.Create("RequiredSearch");
                }
                else if (resourceid.Equals(""))
                {
                    throw MessageException.Create("RequiredSearch");
                }
            }



            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable dtCodeClass = SqlExecuter.Query("SelectResourceReport", "10001", values);


            if (dtCodeClass.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdResource.DataSource = dtCodeClass;
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            InitializeDurableConditionPopup();
        }

        private void InitializeDurableConditionPopup()
        {
            //팝업 컬럼 설정
            var parentPopupColumn = Conditions.AddSelectPopup("P_PRODUCTDEFID", new SqlQuery("GetProductItemGroup", "10002", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "ITEMID", "ITEMID")
               .SetPopupLayout("ITEMID", PopupButtonStyles.Ok_Cancel, true, false)  //팝업창 UI 설정.
               .SetPopupResultCount(1)  //팝업창 선택가능한 개수
               .SetPosition(1.1)
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

            var conditionCustomer = Conditions.AddSelectPopup("RESOURCEIDNAME", new SqlQuery("GetResourcePopup", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "DESCRIPTION", "RESOURCEID")
                .SetPopupLayout("RESOURCEIDNAME", PopupButtonStyles.Ok_Cancel, true, true)
                .SetPopupLayoutForm(800, 800)
                .SetLabel("RESOURCEIDNAME")
                .SetPosition(3.1)
                .SetPopupResultCount(0);

            // 팝업 조회조건
            conditionCustomer.Conditions.AddTextBox("RESOURCEID");
            conditionCustomer.Conditions.AddTextBox("DESCRIPTION").SetLabel("RESOURCENAME");

            // 팝업 그리드
            conditionCustomer.GridColumns.AddTextBoxColumn("RESOURCEID", 150);
            conditionCustomer.GridColumns.AddTextBoxColumn("DESCRIPTION", 200).SetLabel("RESOURCENAME");
            conditionCustomer.GridColumns.AddTextBoxColumn("AREANAME", 200);
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
