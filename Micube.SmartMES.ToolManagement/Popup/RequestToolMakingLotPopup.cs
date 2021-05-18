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
    /// 프 로 그 램 명  : 치공구 수정조회팝업
    /// 업  무  설  명  : 치공구 수정/수리의뢰를 위한 제품코드와 관련된 치공구를 조회할 때 사용
    /// 생    성    자  : 김용조
    /// 생    성    일  : 2019-08-01
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class RequestToolMakingLotPopup : SmartPopupBaseForm, ISmartCustomPopup
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

        public RequestToolMakingLotPopup(string plantID, string productDefID)
        {
            InitializeComponent();

            InitializeEvent();

            InitializeCondition();

            InitializeComboBox();

            _plantID = plantID;
            _productDefID = productDefID;
        }

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
            cboToolCategory.EmptyItemValue = "";

            DataTable toolCategoryTable = SqlExecuter.Query("GetDurableClassIDByTool", "10001"
             , new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "ENTERPRISEID", UserInfo.Current.Enterprise }, { "DURABLECLASSTYPE", "Tool" }});

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
             , new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "ENTERPRISEID", UserInfo.Current.Enterprise }});

            cboToolCategoryDetail.DataSource = toolCategoryDetailTable;

            cboToolCategoryDetail.ShowHeader = false;

            //작업장
            cboArea.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboArea.ValueMember = "AREAID";
            cboArea.DisplayMember = "AREANAME";
            cboArea.UseEmptyItem = true;
            cboArea.EmptyItemCaption = "";
            cboArea.EmptyItemValue = "";

            DataTable areaTable = SqlExecuter.Query("GetAreaIDListByTool", "10001"
             , new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "ENTERPRISEID", UserInfo.Current.Enterprise }, { "PLANTID", _plantID } });

            cboArea.DataSource = areaTable;

            cboArea.ShowHeader = false;
        }

        #endregion

        #region  이벤트
        private void InitializeEvent()
        {
            btnSearch.Click += BtnSearch_Click;
            btnConfirm.Click += BtnConfirm_Click;
            btnCancel.Click += BtnCancel_Click;
            grdToolRequestLotList.View.DoubleClick += grdProductItem_DoubleClick;
            Load += RequestToolMakingLotPopup_Load;

            //txtItemNm.KeyPress += TxtItemNm_KeyPress;
            //txtItemCode.KeyPress += TxtItemVer_KeyPress;
        }

        #region RequestToolMakingLotPopup_Load - 화면로딩 이벤트
        private void RequestToolMakingLotPopup_Load(object sender, EventArgs e)
        {
            Search();
        }
        #endregion

        #region BtnConfirm_Click - 확인버튼 이벤트
        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            DataTable table = grdToolRequestLotList.View.GetCheckedRows();

            dataInputHandler(table);
            Close();
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
            grdToolRequestLotList.GridButtonItem = GridButtonItem.None;   // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기
            grdToolRequestLotList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            
            grdToolRequestLotList.View.AddTextBoxColumn("TOOLCODE", 150).SetIsHidden();
            
            grdToolRequestLotList.View.AddTextBoxColumn("TOOLNUMBER", 150);
            grdToolRequestLotList.View.AddTextBoxColumn("TOOLVERSION", 80);
            grdToolRequestLotList.View.AddTextBoxColumn("TOOLNAME", 350);
            grdToolRequestLotList.View.AddComboBoxColumn("TOOLFORMCODE", 150, new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CODECLASSID=ToolForm"), "CODENAME", "CODEID")
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(true);                                                  //상태 - (이 회면에서 입력해야 한다면 SetIsReadOnly를 풀어준다.
            grdToolRequestLotList.View.AddTextBoxColumn("TOOLCATEGORYID", 100)
                .SetIsHidden();
            grdToolRequestLotList.View.AddTextBoxColumn("TOOLCATEGORY", 100)
                .SetIsReadOnly(true);
            grdToolRequestLotList.View.AddTextBoxColumn("TOOLCATEGORYDETAILID", 100)
                .SetIsHidden();
            grdToolRequestLotList.View.AddTextBoxColumn("TOOLCATEGORYDETAIL", 100)
                .SetIsReadOnly(true);
            grdToolRequestLotList.View.AddTextBoxColumn("TOOLDETAILID", 100)
                .SetIsHidden();
            grdToolRequestLotList.View.AddTextBoxColumn("TOOLDETAIL", 100)
                .SetIsReadOnly(true);
            grdToolRequestLotList.View.AddTextBoxColumn("USECOUNT", 80)
                ;
            grdToolRequestLotList.View.AddTextBoxColumn("TOTALUSEDCOUNT", 80)
                ;
            grdToolRequestLotList.View.AddTextBoxColumn("TOTALCLEANCOUNT", 80)
                ;
            grdToolRequestLotList.View.AddTextBoxColumn("TOTALREPAIRCOUNT", 80)
                ;
            grdToolRequestLotList.View.AddTextBoxColumn("AREAID", 0)
                .SetIsHidden();
            grdToolRequestLotList.View.AddTextBoxColumn("AREANAME", 150)
                .SetIsReadOnly(true);
            grdToolRequestLotList.View.AddTextBoxColumn("DURABLESTATE", 100)
                .SetIsHidden();
            grdToolRequestLotList.View.AddTextBoxColumn("STATENAME", 100)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(true);            
            grdToolRequestLotList.View.AddTextBoxColumn("USELAYER", 100)
                .SetIsHidden();
            grdToolRequestLotList.View.PopulateColumns();
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
            
            Param.Add("AREAID", cboArea.EditValue);
            Param.Add("TOOLCATEGORY", cboToolCategory.EditValue);
            Param.Add("TOOLCATEGORYDETAIL", cboToolCategoryDetail.EditValue);
            Param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            Param.Add("PLANTID", _plantID);
            if (UserInfo.Current.Enterprise.ToUpper().Equals("INTERFLEX"))
            {
                if (_productDefID.Length > 7)
                    Param.Add("PRODUCTDEFID", _productDefID.Substring(0, 7));
                else
                    Param.Add("PRODUCTDEFID", _productDefID);
            }
            else
            {
                if (_productDefID.Length > 8)
                    Param.Add("PRODUCTDEFID", _productDefID.Substring(0, 8));
                else
                    Param.Add("PRODUCTDEFID", _productDefID);
            }
            Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            Param.Add("TOOLNUMBER", txtToolNumber.EditValue);

            Param = Commons.CommonFunction.ConvertParameter(Param);
            DataTable dt = SqlExecuter.Query("GetDurableLotToolListByTool", "10001", Param);

            grdToolRequestLotList.DataSource = dt;

        }
        #endregion
    }
}
