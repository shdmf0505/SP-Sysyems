#region using
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Validations;
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

namespace Micube.SmartMES.ToolManagement.Popup
{
    /// <summary>
    /// 프 로 그 램 명  : 필름 제품코드 팝업(중복조회용)
    /// 업  무  설  명  : 필름제작요청화면 변경에 따른 다중선택용 필름코드조회
    /// 생    성    자  : 김용조
    /// 생    성    일  : 2020-01-06
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class MultiFilmCodePopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region Local Variables
        public DataRow CurrentDataRow { get; set; }
        public delegate void LoadDataEventHandler(DataTable checkedTable);
        public event LoadDataEventHandler loadDataHandler;

        string _plantID;
        #endregion

        public MultiFilmCodePopup(string productDefID, string productDefName, string productDefVersion)
        {
            InitializeComponent();
            InitializeCondition();
            InitializeEvent();

            popProductCode.Text = productDefID;
            txtProductDefName.Text = productDefName;
            txtProductVersion.Text = productDefVersion;

            if (popProductCode.Text != "")
                Search();
        }

        #region Properties
        /// <summary>
        /// 외부로부터 받은 데이터테이블을 바인딩한다.
        /// </summary>
        public DataTable SearchResult
        {
            set
            {
                grdFilmCodeList.DataSource = value;
            }
        }
        #endregion

        #region 초기화
        /// <summary>
        /// 조회 조건 초기화
        /// </summary>
        private void InitializeCondition()
        {
            //그리드 초기화
            InitializeGridIdDefinitionManagement();

            InitializeConditionProductPopup();
        }
        #endregion

        #region ComboBox  
        /// <summary>
        /// SmartComboBox 컨트롤 추가
        /// </summary>
        private void InitializeComboBox()
        {
            // 작업구분값 정의 
            cboProcessSegment.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboProcessSegment.ValueMember = "PROCESSSEGEMENTID";
            cboProcessSegment.DisplayMember = "PROCESSSEGEMENTNAME";
            cboProcessSegment.UseEmptyItem = true;
            cboProcessSegment.EmptyItemCaption = "";
            cboProcessSegment.EmptyItemValue = "";
            DataTable dtProgcessSegment = SqlExecuter.Query("GetProcessSegmentByTool", "10001"
             , new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "ENTERPRISEID", UserInfo.Current.Enterprise }, { "PLANTID", _plantID } });

            cboProcessSegment.DataSource = dtProgcessSegment;

            cboProcessSegment.ShowHeader = false;
        }
        #endregion

        #region  이벤트
        private void InitializeEvent()
        {
            btnSearch.Click += BtnSearch_Click;
            btnConfirm.Click += BtnConfirm_Click;
            btnCancel.Click += BtnCancel_Click;

            popProductCode.ButtonClick += PopProductCode_ButtonClick;

        }

        #region PopProductCode_ButtonClick - 품목코드 버튼클릭 이벤트
        private void PopProductCode_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Clear)
            {
                txtProductVersion.EditValue = "";
                txtProductDefName.EditValue = "";
            }
        }
        #endregion

        #region BtnConfirm_Click - 확인버튼 클릭이벤트
        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            ConfirmRows();
            Close();
        }
        #endregion

        #region BtnSearch_Click - 검색버튼 클릭이벤트
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }
        #endregion

        #region BtnCancel_Click - 취소버튼 클릭이벤트
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion
        #endregion

        #region 컨트롤 초기화
        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGridIdDefinitionManagement()
        {
            // GRID 초기화
            grdFilmCodeList.GridButtonItem = GridButtonItem.None;   // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기
            grdFilmCodeList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdFilmCodeList.View.SetIsReadOnly(true);

            grdFilmCodeList.View.AddTextBoxColumn("FILMCODE", 150).SetIsHidden();
            grdFilmCodeList.View.AddTextBoxColumn("FILMVERSION", 80);
            grdFilmCodeList.View.AddTextBoxColumn("FILMCATEGORYID").SetIsHidden();
            grdFilmCodeList.View.AddTextBoxColumn("FILMCATEGORY", 100)
                .SetTextAlignment(TextAlignment.Center);                
            grdFilmCodeList.View.AddTextBoxColumn("FILMDETAILCATEGORYID").SetIsHidden();
            grdFilmCodeList.View.AddTextBoxColumn("FILMDETAILCATEGORY", 100)
                .SetTextAlignment(TextAlignment.Center);                
            grdFilmCodeList.View.AddTextBoxColumn("LAYER1", 100).SetIsHidden();
            grdFilmCodeList.View.AddTextBoxColumn("FILMUSELAYER1", 100);
            grdFilmCodeList.View.AddTextBoxColumn("LAYER2", 120).SetIsHidden();
            grdFilmCodeList.View.AddTextBoxColumn("FILMUSELAYER2", 100);
            grdFilmCodeList.View.AddTextBoxColumn("RESOLUTION", 80);
            grdFilmCodeList.View.AddTextBoxColumn("ISCOATING", 80)
                .SetTextAlignment(TextAlignment.Center);
            grdFilmCodeList.View.AddTextBoxColumn("CONTRACTIONX", 80);
            grdFilmCodeList.View.AddTextBoxColumn("CONTRACTIONY", 80);
            grdFilmCodeList.View.AddTextBoxColumn("QTY", 60);
            grdFilmCodeList.View.AddTextBoxColumn("PRIORITYCODE", 60).SetIsHidden();
            grdFilmCodeList.View.AddTextBoxColumn("PRIORITY", 60);
            grdFilmCodeList.View.AddTextBoxColumn("VENDORID", 60).SetIsHidden();
            grdFilmCodeList.View.AddTextBoxColumn("MAKEVENDOR", 180);
            grdFilmCodeList.View.AddTextBoxColumn("RECEIVEAREAID", 60).SetIsHidden();
            grdFilmCodeList.View.AddTextBoxColumn("RECEIVEAREA", 180);
            grdFilmCodeList.View.AddTextBoxColumn("REQUESTDATE", 160)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss");
            grdFilmCodeList.View.AddTextBoxColumn("DURABLECLASSID").SetIsHidden();
            grdFilmCodeList.View.PopulateColumns();
        }

        #region InitializeConditionProductPopup : 품목코드 검색조건
        /// <summary>
        /// 품목코드 조회 설정
        /// </summary>
        private void InitializeConditionProductPopup()
        {
            ConditionItemSelectPopup productPopup = new ConditionItemSelectPopup();
            productPopup.Id = "PRODUCTDEFID";
            productPopup.SearchQuery = new SqlQuery("GetProductdefidPoplistByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", "PRODUCTDEFTYPE=Product");
            productPopup.SetPopupLayout("PRODUCTDEFIDPOPUP", PopupButtonStyles.Ok_Cancel, true, false);
            productPopup.SetPopupLayoutForm(984, 465, FormBorderStyle.FixedToolWindow);
            productPopup.SetPopupResultMapping("PRODUCTDEFID", "PRODUCTDEFID");
            productPopup.SetLabel("PRODUCTDEFID");
            productPopup.SetPopupResultCount(1);
            productPopup.SetPosition(0.9);
            productPopup.SetValidationIsRequired();
            productPopup.SetPopupApplySelection((selectedRows, dataGridRows) =>
            {
                //품목코드 변경시 조회조건에 데이터 변경
                selectedRows.Cast<DataRow>().ForEach(row =>
                {
                    txtProductVersion.EditValue = row.GetString("PRODUCTDEFVERSION");
                    txtProductDefName.EditValue = row.GetString("PRODUCTDEFNAME");
                });

            });

            productPopup.ValueFieldName = "PRODUCTDEFID";
            productPopup.DisplayFieldName = "PRODUCTDEFID";

            // 팝업 조회조건
            productPopup.Conditions.AddTextBox("PRODUCTDEF")
                .SetLabel("PRODUCTDEF");
            //toolCodePopup.Conditions.AddComboBox("PRODUCTDEFTYPE", new SqlQuery("GetCodeListByOspProductdeftype", "10001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType } }), "CODENAME", "CODEID")
            //    .SetEmptyItem("", "", true)
            //    .SetLabel("PRODUCTDEFTYPE")
            //    .ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly                
            //    ;PRODUCTDEFVERSION

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

            popProductCode.SelectPopupCondition = productPopup;                 
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
            if (!popProductCode.Text.Equals(""))
            {
                Dictionary<string, object> Param = new Dictionary<string, object>();
                Param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                Param.Add("PLANTID", _plantID);
                Param.Add("PRODUCTDEFID", popProductCode.EditValue);
                Param.Add("PRODUCTDEFVERSION", txtProductVersion.EditValue);
                Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                Param.Add("CURRENTLOGINID", UserInfo.Current.Id);

                Param = Commons.CommonFunction.ConvertParameter(Param);
                DataTable dt = SqlExecuter.Query("GetFilmInfoForCopyByTool", "10001", Param);

                grdFilmCodeList.DataSource = dt;
            }
            else
            {
                ShowMessage(MessageBoxButtons.OK, "InputProudctInfo", "");
            }
        }
        #endregion

        #region Private Function
        private void ConfirmRows()
        {
            loadDataHandler.Invoke(grdFilmCodeList.View.GetCheckedRows());
        }
        #endregion
    }
}
