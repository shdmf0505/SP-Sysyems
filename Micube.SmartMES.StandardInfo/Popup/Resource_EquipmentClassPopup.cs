#region using
using Micube.Framework;
using Micube.Framework.Net;
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
    public partial class Resource_EquipmentClassPopup : SmartPopupBaseForm, ISmartCustomPopup
	{
        public DataRow CurrentDataRow { get; set; }

        #region Local Variables
        private string _plantId = ""; //plantId Set 변수
        private string _areaId = ""; //areaId Set 변수
		private DataTable _resourceData = new DataTable(); //resource 데이터 Set 변수

		//Resource Type = Equipment
		private const string RESOURCE_TYPE_EQUIPMENT = "Production";//"Equipment";//ResourceType = Equipment 고정 값
		private const string RESOURCE_CLASSID = "Machine";//ResourceClassId = Machine 고정 값

		/// <summary>
		///  선택한 설비 list를 보내기 위한 Handler
		/// </summary>
		/// <param name="dt"></param>
		public delegate void ResultDataHandler(DataTable dt);
		public event ResultDataHandler ResultDataEvent;
		#endregion

		#region 생성자
		public Resource_EquipmentClassPopup(string plantId, string areaId, DataTable resourceData)
        {
            InitializeComponent();

            _plantId = plantId;
            _areaId = areaId;
			_resourceData = resourceData;

			InitializeEvent();
            InitializeGird();
            InitializeCondition();

        }

        public Resource_EquipmentClassPopup()
        {
            InitializeComponent();
        }
        #endregion

        #region 초기화
        /// <summary>
        /// 조회 조건 초기화
        /// </summary>
        private void InitializeCondition()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LanguageType", UserInfo.Current.LanguageType);
            //param.Add("PlantId", _plantId);
            param.Add("CodeClassId", "EquipmentType");

            //대분류
            layout.Conditions.AddComboBox("EQUIPMENTCLASSID_TOPLEVEL", new SqlQuery("GetEquipmentClassList_TopLevel", "10001", param), "EQUIPMENTCLASSNAME", "EQUIPMENTCLASSID_TOPLEVEL")
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                .SetLabel("PARENTEQUIPMENTCLASSID_TOPLEVEL");

            //중분류
            layout.Conditions.AddComboBox("EQUIPMENTCLASSID_MIDDLELEVEL", new SqlQuery("GetEquipmentClassList_MiddleLevel", "10001", param), "EQUIPMENTCLASSNAME", "EQUIPMENTCLASSID_MIDDLELEVEL")
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                .SetLabel("PARENTEQUIPMENTCLASSID_MIDDLELEVEL")
                .SetRelationIds("EQUIPMENTCLASSID_TOPLEVEL");

            //설비 Type
            layout.Conditions.AddComboBox("EQUIPMENTCLASSTYPE", new SqlQuery("GetTypeList", "10001", param))
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                .SetLabel("EQUIPMENTCLASSTYPE")
                .SetDefault("Production")
                .SetIsReadOnly();

            layout.PopulateColumns();
        }

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGird()
        {
			grdEquipmentClassList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
			grdEquipmentClassList.View.AddTextBoxColumn("EQUIPMENTCLASSTYPE", 100)
				.SetIsReadOnly();
			grdEquipmentClassList.View.AddTextBoxColumn("EQUIPMENTCLASSID", 150)
				.SetIsReadOnly();
			grdEquipmentClassList.View.AddTextBoxColumn("EQUIPMENTCLASSNAME", 200)
				.SetIsReadOnly();
			grdEquipmentClassList.View.AddTextBoxColumn("DESCRIPTION", 150)
				.SetIsReadOnly();
			grdEquipmentClassList.View.PopulateColumns();
		}

        #endregion

        #region  이벤트
        private void InitializeEvent()
        {
            btnSave.Click += BtnSave_Click;
            btnCancel.Click += BtnCancel_Click;
            btnSearch.Click += BtnSearch_Click;
        }

		/// <summary>
		/// 확인 클릭 - 메인 grid에 체크 데이터 전달
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnSave_Click(object sender, EventArgs e)
		{
			DataTable dtChecked = grdEquipmentClassList.View.GetCheckedRows();

			dtChecked.Columns.Add("RESOURCEID", typeof(string));
			dtChecked.Columns.Add("RESOURCETYPE", typeof(string));			
			dtChecked.Columns.Add("RESOURCECLASSID", typeof(string));
			dtChecked.Columns.Add("ENTERPRISEID", typeof(string));
			dtChecked.Columns.Add("PLANTID", typeof(string));
			dtChecked.Columns.Add("AREAID", typeof(string));
			dtChecked.Columns.Add("VALIDSTATE", typeof(string));


			foreach (DataRow row in dtChecked.Rows)
			{
				row["RESOURCEID"] = _areaId + row["EQUIPMENTCLASSID"].ToString();
				row["RESOURCETYPE"] = RESOURCE_TYPE_EQUIPMENT;//row["EQUIPMENTCLASSTYPE"].ToString();
				row["RESOURCECLASSID"] = RESOURCE_CLASSID;
				row["ENTERPRISEID"] = UserInfo.Current.Enterprise;
				row["PLANTID"] = _plantId;
				row["AREAID"] = _areaId;
				row["VALIDSTATE"] = "Valid";
			}

			_resourceData.Merge(dtChecked);
			this.ResultDataEvent(_resourceData);

			DialogResult = DialogResult.OK;
			this.Close();
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

        /// <summary>
        /// 조회 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
			param = layout.Conditions.GetValues();
			//param.Add("PlantId", _plantId);
			param.Add("AreaId", _areaId);
			param.Add("LanguageType", UserInfo.Current.LanguageType);

			DataTable dtResult = SqlExecuter.Query("GetEquipmentClassList", "10002", param);
			if(dtResult.Rows.Count < 1)
			{
				ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
			}
			grdEquipmentClassList.DataSource = dtResult;
        }

        #endregion


    }
}
