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
    /// 프 로 그 램 명  : 치공구 제작조회팝업
    /// 업  무  설  명  : 치공구 제작의뢰를 위한 제품코드와 관련된 치공구를 조회할 때 사용
    /// 생    성    자  : 김용조
    /// 생    성    일  : 2019-08-01
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class RequestToolMakingDetailPopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region Global Variables
        public DataRow CurrentDataRow { get; set; }

        /// <summary>
        ///  선택한 제품품 list를 보내기 위한 Handler
        /// </summary>
        /// <param name="dt"></param>
        public delegate void dataInputDelegate(DataTable table);
        public event dataInputDelegate dataInputHandler;
        string _plantID;
        string _productDefID;
        #endregion

        public RequestToolMakingDetailPopup(string plantID)
        {
            InitializeComponent();

            InitializeEvent();

            InitializeCondition();

            InitializeComboBox();

            _plantID = plantID;
        }

        #region ProductDefID - 품목코드 Property
        public string ProductDefID
        {
            get { return _productDefID; }
            set { _productDefID = value; }
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
        }
        #endregion

        #region ComboBox  

        /// <summary>
        /// SmartComboBox 컨트롤 추가
        /// </summary>
        private void InitializeComboBox()
        {
            //제품구분
            cboToolCategory.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboToolCategory.ValueMember = "DURABLECLASSID";
            cboToolCategory.DisplayMember = "DURABLECLASSNAME";
            cboToolCategory.UseEmptyItem = true;
            cboToolCategory.EmptyItemCaption = "";
            cboToolCategory.EmptyItemValue = "";//cboToolCategory.EditValue = "Product";
            DataTable toolCategoryTable = SqlExecuter.Query("GetDurableClassIDByTool", "10001"
             , new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType } , { "ENTERPRISEID", UserInfo.Current.Enterprise}, {"DURABLECLASSTYPE", "Tool" }, {"PLANTID", _plantID} });

            cboToolCategory.DataSource = toolCategoryTable;

            cboToolCategory.ShowHeader = false;

            //제품유형
            cboToolCategoryDetail.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboToolCategoryDetail.ValueMember = "CODEID";
            cboToolCategoryDetail.DisplayMember = "CODENAME";
            cboToolCategoryDetail.UseEmptyItem = true;
            cboToolCategoryDetail.EmptyItemCaption = "";
            cboToolCategoryDetail.EmptyItemValue = "";
            DataTable toolCategoryDetailTable = SqlExecuter.Query("GetCodeClassIDListByTool", "10001"
             , new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "ENTERPRISEID", UserInfo.Current.Enterprise },  { "PLANTID", _plantID } });

            cboToolCategoryDetail.DataSource = toolCategoryDetailTable;

            cboToolCategoryDetail.ShowHeader = false;
        }

        #endregion

        #region  이벤트
        private void InitializeEvent()
        {
            btnSearch.Click += BtnSearch_Click;
            btnConfirm.Click += BtnConfirm_Click;
            btnCancel.Click += BtnCancel_Click;
            grdToolRequestList.View.DoubleClick += grdProductItem_DoubleClick;

            Load += RequestToolMakingDetailPopup_Load;
            //txtItemNm.KeyPress += TxtItemNm_KeyPress;
            //txtItemCode.KeyPress += TxtItemVer_KeyPress;
        }

        #region RequestToolMakingDetailPopup_Load - 화면 로딩시의 이벤트
        private void RequestToolMakingDetailPopup_Load(object sender, EventArgs e)
        {
            Search();
        }
        #endregion

        #region BtnConfirm_Click - 확인버튼 이벤트
        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            DataTable table = grdToolRequestList.View.GetCheckedRows();
            
            dataInputHandler(table);
            this.Close();
        }
        #endregion

        #region grdProductItem_DoubleClick - 그리드 더블클릭이벤트
        private void grdProductItem_DoubleClick(object sender, EventArgs e)
        {
        }
        #endregion

        #region BtnSearch_Click - 검색버튼 이벤트
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }
        #endregion

        #region BtnCancel_Click - 취소버튼 이벤트
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
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
            grdToolRequestList.GridButtonItem = GridButtonItem.None;   // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기
            grdToolRequestList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdToolRequestList.View.AddTextBoxColumn("TOOLCODE", 150);
            grdToolRequestList.View.AddTextBoxColumn("TOOLVERSION", 80);
            grdToolRequestList.View.AddTextBoxColumn("TOOLNAME", 350);
            grdToolRequestList.View.AddTextBoxColumn("LOTID", 150).SetIsHidden();
            grdToolRequestList.View.AddTextBoxColumn("PRODUCTDEFID", 150);
            grdToolRequestList.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80);
            grdToolRequestList.View.AddTextBoxColumn("USECOUNT", 80)
                .SetIsHidden();
            grdToolRequestList.View.AddTextBoxColumn("TOTALUSEDCOUNT", 80)
                .SetIsHidden();
            grdToolRequestList.View.AddTextBoxColumn("TOTALCLEANCOUNT", 80)
                .SetIsHidden();
            grdToolRequestList.View.AddTextBoxColumn("TOTALREPAIRCOUNT", 80)
                .SetIsHidden();
            grdToolRequestList.View.AddTextBoxColumn("AREAID", 0)
                .SetIsHidden();
            grdToolRequestList.View.AddTextBoxColumn("AREANAME", 150)
                .SetIsHidden();
            grdToolRequestList.View.AddTextBoxColumn("DURABLESTATE", 100)
                .SetIsHidden();
            grdToolRequestList.View.AddComboBoxColumn("TOOLFORMCODE", 150, new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CODECLASSID=ToolForm"), "CODENAME", "CODEID")
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(true);                                                  //상태 - (이 회면에서 입력해야 한다면 SetIsReadOnly를 풀어준다.
            grdToolRequestList.View.AddTextBoxColumn("TOOLCATEGORYID", 100)
                .SetIsHidden();
            grdToolRequestList.View.AddTextBoxColumn("TOOLCATEGORY", 100)
                .SetTextAlignment(TextAlignment.Center)
                ;
            grdToolRequestList.View.AddTextBoxColumn("TOOLCATEGORYDETAILID", 100)
                .SetIsHidden();
            grdToolRequestList.View.AddTextBoxColumn("TOOLCATEGORYDETAIL", 100)
                .SetTextAlignment(TextAlignment.Center)
                ;
            grdToolRequestList.View.AddTextBoxColumn("TOOLDETAILID", 100)
                .SetIsHidden();
            grdToolRequestList.View.AddTextBoxColumn("TOOLDETAIL", 100)
                .SetTextAlignment(TextAlignment.Center)
                ;
            grdToolRequestList.View.AddTextBoxColumn("USELAYER", 100).SetIsHidden()
                ;
            grdToolRequestList.View.AddTextBoxColumn("REQUESTDATE", 120)
                .SetDisplayFormat("yyyy-MM-dd")
                ;
            grdToolRequestList.View.PopulateColumns();
        }
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

            if (UserInfo.Current.Enterprise.ToUpper().Equals("INTERFLEX"))
            {
                if (_productDefID.Length > 7)
                    Param.Add("PRODUCTDEFID", _productDefID.Substring(0, 7));
                else
                    Param.Add("PRODUCTDEFID", _productDefID);
            }
            else
            {
                if (_productDefID.Length > 7)
                    Param.Add("PRODUCTDEFID", _productDefID.Substring(0, 7));
                else
                    Param.Add("PRODUCTDEFID", _productDefID);
            }

            Param.Add("TOOLCATEGORY", cboToolCategory.EditValue);
            Param.Add("TOOLCATEGORYDETAIL", cboToolCategoryDetail.EditValue);
            Param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            Param.Add("PLANTID", _plantID);
            Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            if(deStartDate.EditValue != null)
                Param.Add("REQUESTDATE_PERIODFR", Convert.ToDateTime(deStartDate.EditValue).ToString("yyyy-MM-dd HH:mm:ss"));
            if(deEndDate.EditValue != null)
                Param.Add("REQUESTDATE_PERIODTO", Convert.ToDateTime(deEndDate.EditValue).ToString("yyyy-MM-dd HH:mm:ss"));
            Param.Add("TOOLCODE", txtToolCode.EditValue);

            Param = Commons.CommonFunction.ConvertParameter(Param);
            DataTable dt = SqlExecuter.Query("GetToolDetailCodeListByTool", "10001", Param);

            grdToolRequestList.DataSource = dt;

        }
        #endregion
    }
}
