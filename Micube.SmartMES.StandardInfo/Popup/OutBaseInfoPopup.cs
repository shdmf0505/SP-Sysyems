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

namespace Micube.SmartMES.StandardInfo.Popup
{
    /// <summary>
    /// 프 로 그 램 명  : 기준정보 > 품목관리 > 품목등록(사양)
    /// 업  무  설  명  : YPE 등록
    /// 생    성    자  : 윤성원
    /// 생    성    일  : 2019-06-28    
    /// 수  정  이  력  : 
    /// </summary>
    public partial class OutBaseInfoPopup : SmartPopupBaseForm, ISmartCustomPopup
	{
        public DataRow CurrentDataRow { get; set; }

        #region Local Variables


        object[] _objparam;


        #endregion

        #region 생성자
        public OutBaseInfoPopup()
        {
            InitializeComponent();
			InitializeEvent();
            InitializeCondition();
        }
        public OutBaseInfoPopup(object[] objparam)
        {
            InitializeComponent();

            _objparam = objparam;

            InitializeEvent();
            InitializeCondition();
            InitializeGridInspectionpoint();
     
            Search(_objparam);
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
        private void InitializeGridInspectionpoint()
        {
            // GRID 초기화
            grdInspectionpoint.GridButtonItem = GridButtonItem.All;   // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기
            grdInspectionpoint.View.AddTextBoxColumn("INSPECTIONPOINTID", 150).SetIsHidden();
            //grdInspectionpoint.View.AddTextBoxColumn("INSPITEMCLASSID", 150).SetIsHidden();
            grdInspectionpoint.View.AddTextBoxColumn("INSPITEMID", 150).SetIsHidden();
            grdInspectionpoint.View.AddTextBoxColumn("INSPITEMVERSION", 150).SetIsHidden();
            grdInspectionpoint.View.AddTextBoxColumn("INSPECTIONDEFID", 150).SetIsHidden();
            grdInspectionpoint.View.AddTextBoxColumn("INSPECTIONDEFVERSION", 150).SetIsHidden();
            grdInspectionpoint.View.AddTextBoxColumn("RESOURCEID", 150).SetIsHidden();
            grdInspectionpoint.View.AddTextBoxColumn("RESOURCEVERSION", 150).SetIsHidden();
            grdInspectionpoint.View.AddTextBoxColumn("RESOURCETYPE", 150).SetIsHidden();
            grdInspectionpoint.View.AddComboBoxColumn("INSPECTIONPOINTNAME", 150, new SqlQuery("GetCodeList", "00001", "CODECLASSID=InspectionPointName", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Center);

            grdInspectionpoint.View.AddSpinEditColumn("INSPECTIONQTY", 150);
            grdInspectionpoint.View.AddSpinEditColumn("POINTQTY", 150);
            grdInspectionpoint.View.AddTextBoxColumn("PLANTID", 150).SetIsHidden();
            grdInspectionpoint.View.AddTextBoxColumn("ENTERPRISEID", 150).SetIsHidden();
            grdInspectionpoint.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Valid")
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Center);
            grdInspectionpoint.View.PopulateColumns();

        }
        #endregion

        #region  이벤트
        private void InitializeEvent()
        {
            btnSave.Click += BtnSave_Click;
            btnCancel.Click += BtnCancel_Click;
            grdInspectionpoint.View.AddingNewRow += grdInspectionpoint_AddingNewRow;
        }

        private void grdInspectionpoint_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            GetNumber number = new GetNumber();

            DataTable dtDate = SqlExecuter.Query("GetItemId", "10001");
            string sdate = dtDate.Rows[0]["ITEMID"].ToString().Substring(0, 8);

            args.NewRow["INSPECTIONPOINTID"] = number.GetStdNumber("Inspectionpoint", sdate);

            //args.NewRow["INSPITEMCLASSID"] = _objparam[0];
            args.NewRow["INSPITEMID"] = _objparam[1];
            args.NewRow["INSPITEMVERSION"] = _objparam[2];
            args.NewRow["INSPECTIONDEFID"] = _objparam[3];
            args.NewRow["INSPECTIONDEFVERSION"] = _objparam[4];
            args.NewRow["RESOURCETYPE"] = _objparam[6];
            args.NewRow["RESOURCEID"] = _objparam[5];
            args.NewRow["RESOURCEVERSION"] = _objparam[7];
            args.NewRow["ENTERPRISEID"] = _objparam[9];
            args.NewRow["PLANTID"] = _objparam[10];
            args.NewRow["VALIDSTATE"] = "Valid";
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {

            DataTable dsChange = grdInspectionpoint.GetChangedRows();

            ExecuteRule("Inspectionpoint", dsChange);

            ShowMessage("SuccedSave");

            Close();

        }

        

        /// <summary>
        /// 조회 클릭 - 메인 grid에 체크 데이터 전달
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Search(object[] paramobject)
        {
            
            Dictionary<string, object> Param = new Dictionary<string, object>();
           // Param.Add("MASTERDATACLASSID", sMASTERDATACLASSID);
            //Param.Add("P_INSPITEMCLASSID", paramobject[0]);
            Param.Add("P_INSPITEMID", paramobject[1]);
            Param.Add("P_INSPITEMVERSION", paramobject[2]);
            
            Param.Add("P_INSPECTIONDEFID", paramobject[3]);
            Param.Add("P_INSPECTIONDEFVERSION", paramobject[4]);
            Param.Add("P_RESOURCEID", paramobject[5]);
            Param.Add("P_RESOURCETYPE", paramobject[6]);
            Param.Add("P_RESOURCEVERSION", paramobject[7]);
            Param.Add("P_VALIDSTATE", paramobject[8]);

            Param.Add("ENTERPRISEID", paramobject[9]);
            Param.Add("PLANTID", paramobject[10]);


            DataTable dt = SqlExecuter.Query("SelectInspectionPointByRelInfo", "10001", Param);
            grdInspectionpoint.DataSource = dt;
            
        }

        /// <summary>
        /// 취소 버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion
    }
}

