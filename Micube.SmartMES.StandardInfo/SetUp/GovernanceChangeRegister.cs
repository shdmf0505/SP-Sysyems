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
    /// 프 로 그 램 명  : 기준정보 > 사양변경관리 > 사양변경의뢰서 등록
    /// 업  무  설  명  : 사양변경의뢰서를 등록한다
    /// 생    성    자  : 한주석
    /// 생    성    일  : 2020-01-09
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class GovernanceChangeRegister : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가
        string _Text;                                       // 설명
        SmartBandedGrid grdList = new SmartBandedGrid();    // Main Grid

        #endregion

        #region 생성자

        public GovernanceChangeRegister()
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
            //의뢰번호
            grdmodifi.View.AddTextBoxColumn("REQUESTNO", 120)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            //회사
            grdmodifi.View.AddTextBoxColumn("ENTERPRISEID", 100)
                .SetDefault(UserInfo.Current.Enterprise)
                .SetIsReadOnly();
            //모델코드
            InitializeProductId_Popup();
            //모델버전
            grdmodifi.View.AddTextBoxColumn("ITEMVERSION", 70)
                .SetLabel("MODELVERSION")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            //모델버전
            grdmodifi.View.AddTextBoxColumn("ITEMNAME", 160)
                .SetLabel("MODELNAME")
                .SetIsReadOnly();
            //의뢰부서
            grdmodifi.View.AddTextBoxColumn("REQUESTTEAM", 100)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            //의뢰자
            grdmodifi.View.AddTextBoxColumn("CREATOR", 70)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("REQUESTER")
                .SetIsReadOnly();
            //의뢰일자
            grdmodifi.View.AddTextBoxColumn("CREATEDTIME", 160)
                .SetLabel("REQUESTDATE")
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();
            //업체담당자
            grdmodifi.View.AddTextBoxColumn("VENDOROWNER", 100)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("COMPANYUSER");
            //Tel
            grdmodifi.View.AddTextBoxColumn("VENDORTELNO", 150)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("TELNUMBER");

            grdmodifi.View.PopulateColumns();



            // 품목
            ConditionItemSelectPopup decideModel = new ConditionItemSelectPopup();
            decideModel.SetPopupLayoutForm(600, 600, FormBorderStyle.FixedDialog);
            decideModel.SetPopupLayout("PRODUCTDEFID", PopupButtonStyles.Ok_Cancel);
            decideModel.Id = "PRODUCTDEFID";
            decideModel.LabelText = "PRODUCTDEF";
            decideModel.SearchQuery = new SqlQuery("GetProductDefList", "10003", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}");
            decideModel.IsMultiGrid = false;
            decideModel.DisplayFieldName = "PRODUCTDEFID";
            decideModel.ValueFieldName = "PRODUCTDEFID";
            decideModel.LanguageKey = "PRODUCTDEF";
            decideModel.SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
            {
                foreach (DataRow row in selectedRow)
                {
                    if (selectedRow.Count() > 0)
                    {

                        txtmodelrev.EditValue = row["PRODUCTDEFVERSION"].ToString();
                        txtmodelname.EditValue = row["PRODUCTDEFNAME"].ToString();

                    }

                    //txtProductDefVersionPro.EditValue = prodDefVersion;
                    //txtProductDefNamePro.EditValue = prodDefName;
                }
            });
            decideModel.Conditions.AddTextBox("PRODUCTDEF");

            decideModel.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            decideModel.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            decideModel.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 60);
            
            popaftermodel.SelectPopupCondition = decideModel;


        }

        #endregion

        /// <summary>
        /// 팝업형 컬럼 초기화-품목코드
        /// </summary>
        private void InitializeProductId_Popup()
        {
            var productColumn = grdmodifi.View.AddSelectPopupColumn("ITEMID", 150, new SqlQuery("GetItemId", "10002", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"))
                .SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, true)
                .SetPopupResultCount(1)
                .SetLabel("MODELCODE")
                .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                .SetPopupApplySelection((selectedRows, dataGridRows) =>
                {
                    foreach (DataRow row in selectedRows)
                    {
                        dataGridRows["ITEMVERSION"] = row["ITEMVERSION"];
                        dataGridRows["ITEMNAME"] = row["ITEMNAME"];
                    }
                });


            productColumn.Conditions.AddTextBox("PRODUCTDEF")
                .SetLabel("MODELID");

            productColumn.GridColumns.AddTextBoxColumn("ITEMID", 150)
                .SetLabel("MODELCODE");
            productColumn.GridColumns.AddTextBoxColumn("ITEMVERSION", 50)
                .SetLabel("MODELVERSION");
            productColumn.GridColumns.AddTextBoxColumn("ITEMNAME", 200)
                .SetLabel("MODELNAME");
        }

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            grdmodifi.View.AddingNewRow += View_AddingNewRow;
            grdmodifi.View.FocusedRowChanged += View_FocusedRowChanged;
            memoresult.TextChanged += Memoresult_TextChanged;
        }

        private void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            Request_FocusedRowChanged();
        }

        private void Memoresult_TextChanged(object sender, EventArgs e)
        {
          


        }



        /// <summary>
        /// 코드그룹 리스트 그리드에서 추가 버튼 클릭 시 호출
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            memomodifibefore.EditValue = null;
            memomodifiafter.EditValue = null;
            popaftermodel.EditValue = null;
            txtmodelrev.EditValue = null;
            memoresult.EditValue = null;
            memocontent.EditValue = null;
            txtmodelname.EditValue = null;
        }


        private void Request_FocusedRowChanged()
        {

            DataRow focusRow = grdmodifi.View.GetFocusedDataRow();
            if (focusRow == null || focusRow["REQUESTNO"].ToString().Equals(""))
            {
                memomodifibefore.EditValue = null;
                memomodifiafter.EditValue = null;
                popaftermodel.EditValue = null;
                txtmodelrev.EditValue = null;
                memoresult.EditValue = null;
                memocontent.EditValue = null;
                txtmodelname.EditValue = null;

            }
            else
            {


                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("REQUESTNO", focusRow["REQUESTNO"].ToString());
                DataTable dt = SqlExecuter.Query("GetRequestInfo", "10001", param) as DataTable;

                memomodifibefore.EditValue = dt.Rows[0]["BEFORECOMMENTS"];
                memomodifiafter.EditValue = dt.Rows[0]["AFTERCOMMENTS"];
                popaftermodel.EditValue = dt.Rows[0]["CONFIRMITEMID"];
                txtmodelrev.EditValue = dt.Rows[0]["CONFIRMITEMVERSION"];
                memoresult.EditValue = dt.Rows[0]["RESULTCOMMENTS"];
                memocontent.EditValue = dt.Rows[0]["ETCCOMMENTS"];
                txtmodelname.EditValue = dt.Rows[0]["CONFIRMITEMNAME"];

            }
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
            DataTable changed = grdmodifi.GetChangedRows();
            changed.Columns.Add(new DataColumn("BEFORECOMMENTS", typeof(string)));
            changed.Columns.Add(new DataColumn("AFTERCOMMENTS", typeof(string)));
            changed.Columns.Add(new DataColumn("CONFIRMITEMID", typeof(string)));
            changed.Columns.Add(new DataColumn("CONFIRMITEMVERSION", typeof(string)));
            changed.Columns.Add(new DataColumn("CONFIRMITEMNAME", typeof(string)));
            changed.Columns.Add(new DataColumn("RESULTCOMMENTS", typeof(string)));
            changed.Columns.Add(new DataColumn("ETCCOMMENTS", typeof(string)));
            if (changed.Rows.Count==0)
            {
                DataRow newrow = changed.NewRow();
                DataRow dr = grdmodifi.View.GetFocusedDataRow();

                newrow.ItemArray = dr.ItemArray.Clone() as object[];
                changed.Rows.Add(newrow);
                changed.Rows[0]["_STATE_"] = "modified";
            }
            if (memomodifibefore.EditValue != null)
            {
                changed.Rows[0]["BEFORECOMMENTS"] = memomodifibefore.EditValue;
            }
            if (memomodifiafter.EditValue != null)
            {
                changed.Rows[0]["AFTERCOMMENTS"] = memomodifiafter.EditValue;
            }
            if (popaftermodel.EditValue != null)
            {
                changed.Rows[0]["CONFIRMITEMID"] = popaftermodel.EditValue;
            }
            if (txtmodelrev.EditValue != null)
            {
                changed.Rows[0]["CONFIRMITEMVERSION"] = txtmodelrev.EditValue;
            }
            if (memoresult.EditValue != null)
            {
                changed.Rows[0]["RESULTCOMMENTS"] = memoresult.EditValue;
            }
            if (memocontent.EditValue != null)
            {
                changed.Rows[0]["ETCCOMMENTS"] = memocontent.EditValue;
            }
            if (txtmodelname.EditValue != null)
            {
                changed.Rows[0]["CONFIRMITEMNAME"] = memocontent.EditValue;
            }




            ExecuteRule("SaveGovernanceChange", changed);
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

            DataTable dtCodeClass = SqlExecuter.Query("SelectGovernanceChange", "10001", values);

            if (dtCodeClass.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdmodifi.DataSource = dtCodeClass;
            Request_FocusedRowChanged();

        }



        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            //품목코드
            InitializeCondition_ProductPopup();
            InitializeGrid_UserListPopup();
        }

        private void InitializeGrid_UserListPopup()
        {
            //추후 변경 예정
            //var parentCodeClassPopupColumn = this.grdAreaPerson.View.AddSelectPopupColumn("USERID", new SqlQuery("GetUserAreaPerson", "10001", $"PLANTID={UserInfo.Current.Plant}"))

            var parentCodeClassPopupColumn = Conditions.AddSelectPopup("CREATOR", new SqlQuery("GetUserAreaPerson", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "USERNAME", "USERNAME")
               .SetPopupLayout("REQUESTER", PopupButtonStyles.Ok_Cancel, true, false)
               .SetPopupResultCount(0)
               .SetPopupLayoutForm(300, 800, FormBorderStyle.FixedToolWindow)
               .SetPosition(1.3)
               .SetLabel("REQUESTER");

            // 팝업에서 사용할 조회조건 항목 추가
            parentCodeClassPopupColumn.Conditions.AddTextBox("USERNAME")
                .SetLabel("REQUESTER");

            // 팝업 그리드 설정
            parentCodeClassPopupColumn.GridColumns.AddTextBoxColumn("USERNAME", 200)
                .SetLabel("REQUESTER");
        }
        /// <summary>
        /// 팝업형 조회조건 생성 - 품목
        /// </summary>
        private void InitializeCondition_ProductPopup()
        {
            // SelectPopup 항목 추가
            var conditionProductId = Conditions.AddSelectPopup("P_ITEMID", new SqlQuery("GetItemMasterList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "ITEMID", "ITEMID")
                .SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupLayoutForm(800, 800)
                .SetPopupAutoFillColumns("ITEMNAME")
                .SetLabel("MODELCODE")
                .SetPosition(1.2)
                .SetPopupResultCount(1);

            // 팝업에서 사용되는 검색조건 (품목코드/명)
            conditionProductId.Conditions.AddTextBox("TXTITEM")
                .SetLabel("MODELID");


            // 품목코드
            conditionProductId.GridColumns.AddTextBoxColumn("ITEMID", 150)
                .SetLabel("MODELCODE");
            // 품목명
            conditionProductId.GridColumns.AddTextBoxColumn("ITEMNAME", 200)
                .SetLabel("MODELNAME");
            // 품목버전
            conditionProductId.GridColumns.AddTextBoxColumn("ITEMVERSION", 100)
                .SetLabel("MODELVERSION");
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
        }

        #endregion

        #region 유효성 검사



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

        private void smartSplitTableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void smartLabel4_Click(object sender, EventArgs e)
        {

        }

        private void smartLabel5_Click(object sender, EventArgs e)
        {

        }
    }
}
