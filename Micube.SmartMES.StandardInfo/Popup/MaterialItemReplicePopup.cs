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

using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.BandedGrid;

#endregion

namespace Micube.SmartMES.StandardInfo.Popup
{
    /// <summary>
    /// 프 로 그 램 명  : 기준정보 > Setup > 작업장 관리 > 설비 유형 팝업
    /// 업  무  설  명  : 설비 유형(EquipmentClass)을 선택
    /// 생    성    자  :  정승원
    /// 생    성    일  : 2019-05-22
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class MaterialItemReplicePopup : SmartPopupBaseForm, ISmartCustomPopup
	{
        public DataRow CurrentDataRow { get; set; }

        #region Local Variables
        DataTable _dt = null;
        DataRow _row = null;

        /// <summary>
        ///  선택한 list를 보내기 위한 Handler
        /// </summary>
        /// <param name="dt"></param>
        public delegate void chat_room_evecnt_handler(DataRow row);
        #endregion

        #region 생성자
        public MaterialItemReplicePopup()
        {
            InitializeComponent();
			InitializeEvent();
            InitializeCondition();
        }
        public MaterialItemReplicePopup(DataTable dt)
        {
            InitializeComponent();

            _dt = dt;

            InitializeEvent();
            InitializeCondition();
            InitializeGridIdDefinitionManagement();
            //txtItemCode.Text = sCD_ITEM;
            //Search(txtSearch.Text,"", "", "", "", "");
            SetControlsFrom set = new SetControlsFrom();
            set.SetControlsFromTable(smartSplitTableLayoutPanel4, _dt);

            Search();
        }

        public MaterialItemReplicePopup(DataRow row)
        {
            InitializeComponent();

            _row = row;

            InitializeEvent();
            InitializeCondition();
            InitializeGridIdDefinitionManagement();

            txtItemCode.EditValue = row["ITEMID"].ToString();
            txtItemNm.EditValue = row["ITEMNAME"].ToString();

            //txtItemCode.Text = sCD_ITEM;
            //Search(txtSearch.Text,"", "", "", "", "");
            SetControlsFrom set = new SetControlsFrom();
            set.SetControlsFromRow(smartSplitTableLayoutPanel4, _row);

            Search();
        }

        #endregion

        #region 초기화
        /// <summary>
        /// 조회 조건 초기화
        /// </summary>
        private void InitializeCondition()
        {
        } 

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGridIdDefinitionManagement()
        {
            // GRID 초기화
            grdAlterNativeMaterIa.GridButtonItem = GridButtonItem.Import | GridButtonItem.Export;   // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기
            grdAlterNativeMaterIa.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;


            grdAlterNativeMaterIa.View.AddTextBoxColumn("ITEMID", 150).SetIsHidden();

            grdAlterNativeMaterIa.View.AddSpinEditColumn("SEQUENCE", 60).SetIsReadOnly()
                .SetLabel("SEQ")
                .SetTextAlignment(TextAlignment.Center);

            InitializeGrid_ItemMasterPopup();

            grdAlterNativeMaterIa.View.AddTextBoxColumn("ALTERNATIVEITEMNAME", 200).SetIsReadOnly();

            grdAlterNativeMaterIa.View.AddComboBoxColumn("ITEMUOM", new SqlQuery("GetUOMList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "UOMDEFNAME", "UOMDEFID")
                .SetLabel("UNIT")
                .SetTextAlignment(TextAlignment.Center);
            grdAlterNativeMaterIa.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            grdAlterNativeMaterIa.View.PopulateColumns();
        }
        #endregion
        private void InitializeGrid_ItemMasterPopup()
        {
            string sPopupSql = "";
            string sPopupVersion = "";
            Dictionary<string, object> param = new Dictionary<string, object>();

            if (_row["MATERIALCLASS"].ToString() == "SubAssembly")
            {
                sPopupSql = "GetSubAssemblyItemPopup";
                sPopupVersion = "10001";
  
            }
            else
            {
                sPopupSql = "GetMaterialItemPopup";
                sPopupVersion = "10002";
                
            }

            var parentItem = this.grdAlterNativeMaterIa.View.AddSelectPopupColumn("ALTERNATIVEITEMID", new SqlQuery(sPopupSql, sPopupVersion, $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"ORIGINITEMID={_row["ITEMID"]}"))
            // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
            .SetPopupLayout("MATERIALLIST", PopupButtonStyles.Ok_Cancel, true, false)
            // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
            .SetPopupResultCount(1)
            // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
            .SetPopupResultMapping("ALTERNATIVEITEMID", "ITEMID")
            // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
            .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow)
            // 그리드의 남은 영역을 채울 컬럼 설정
            //.SetPopupAutoFillColumns("CODECLASSNAME")
            // Validation 이 필요한 경우 호출할 Method 지정

            .SetPopupValidationCustom(ValidationItemMasterPopup);

            // 팝업에서 사용할 조회조건 항목 추가
            parentItem.Conditions.AddTextBox("ITEMID")
                .SetLabel("CONSUMABLEDEFID");
            //parentItem.Conditions.AddTextBox("ITEMVERSION");
            parentItem.Conditions.AddTextBox("ITEMNAME")
                .SetLabel("CONSUMABLEDEFNAME");

            // 팝업에서 사용할 조회조건 항목 추가
            parentItem.Conditions.AddTextBox("MASTERDATACLASSID")
            .SetPopupDefaultByGridColumnId("MASTERDATACLASSID")
            .SetIsHidden();

            // 팝업 그리드 설정
            parentItem.GridColumns.AddTextBoxColumn("ITEMID", 150)
                .SetLabel("CONSUMABLEDEFID");
            //parentItem.GridColumns.AddTextBoxColumn("ITEMVERSION", 100);
            parentItem.GridColumns.AddTextBoxColumn("ITEMNAME", 250)
                .SetLabel("CONSUMABLEDEFNAME");
            parentItem.GridColumns.AddComboBoxColumn("ITEMUOM", 100, new SqlQuery("GetUOMList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "UOMDEFNAME", "UOMDEFID")
                .SetLabel("UNIT")
                .SetTextAlignment(TextAlignment.Center);
           // parentItem.GridColumns.AddTextBoxColumn("UOMDEFNAME", 250);

        }
        #region  이벤트
        private void InitializeEvent()
        {
            btnSave.Click += BtnSave_Click;
            btnCancel.Click += BtnCancel_Click;

            grdAlterNativeMaterIa.View.AddingNewRow += grdAlterNativeMaterIa_AddingNewRow;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            DataTable dt = grdAlterNativeMaterIa.GetChangedRows();

            
            if(dt != null)
            {

                dt.Columns.Add("PLANTID");
                foreach(DataRow dr in dt.Rows)
                {
                    dr["PLANTID"] = UserInfo.Current.Plant;

                }
                if (dt.Rows.Count !=0)
                {
                    ExecuteRule("Alternativematerial", dt);

                    ShowMessage("SuccessSave");

                    Search();
                }
            }
        }

        private void grdAlterNativeMaterIa_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            args.NewRow["SEQUENCE"] = 0;
            DataTable dt = (DataTable)grdAlterNativeMaterIa.DataSource;
            if(dt !=null)
            {
                if(dt.Rows.Count !=0)
                {
                    DataRow[] row = dt.Select("1=1", "SEQUENCE DESC");
                    args.NewRow["SEQUENCE"] =  Int32.Parse(row[0]["SEQUENCE"].ToString()) + 1;
                }
                else
                {
                    args.NewRow["SEQUENCE"] = 1;
                }
            }
            else
            {
                args.NewRow["SEQUENCE"] = 1;
            }

            args.NewRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
            args.NewRow["ITEMID"] = txtItemCode.Text;
            args.NewRow["VALIDSTATE"] = "Valid";
            //args.NewRow["MASTERDATACLASSID"] =  cboMasterDataClass.GetDataValue();
        }

        void Search()
        {
            Dictionary<string, object> Param = new Dictionary<string, object>();
            Param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            Param.Add("ITEMID", txtItemCode.Text );
            //Param.Add("ITEMVERSION", "*");
            DataTable dt = SqlExecuter.Query("GetAlterNativeMaterIalList", "10001", Param);
            grdAlterNativeMaterIa.DataSource = dt;
            
        }

        /// <summary>
        /// 취소 버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        /// <summary>
        /// 팝업 컬럼에서 확인 클릭 시 다른 데이터 매핑 ( 작업장)
        /// </summary>
        /// <param name="currentGridRow">화면 grid row</param>
        /// <param name="popupSelections">팝업에서 선택한 row</param>
        /// <returns></returns>
        private Micube.Framework.SmartControls.Validations.ValidationResultCommon ValidationItemMasterPopup(DataRow currentGridRow, IEnumerable<DataRow> popupSelections)
        {
            Micube.Framework.SmartControls.Validations.ValidationResultCommon result = new Framework.SmartControls.Validations.ValidationResultCommon();

            foreach (DataRow row in popupSelections)
            {
                currentGridRow["ALTERNATIVEITEMNAME"] = row["ITEMNAME"];
                currentGridRow["ALTERNATIVEITEMVERSION"] = row["ITEMVERSION"];
                currentGridRow["ITEMUOM"] = row["ITEMUOM"];
                currentGridRow["MASTERDATACLASSID"] = row["MASTERDATACLASSID"];
            }
            return result;
        }
    }
}
