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
    public partial class ReworkRoutingPopupVer1 : SmartPopupBaseForm, ISmartCustomPopup
	{
        public DataRow CurrentDataRow { get; set; }

        #region Local Variables
        private string plantId = string.Empty;
      

		//Resource Type = Equipment
		
        #endregion

        #region 생성자
        public ReworkRoutingPopupVer1(string plantId)
        {
            InitializeComponent();
            InitializeEvent();
            InitializeCondition();
            InitializeGridIdDefinitionManagement();
            this.plantId = plantId;
        }

        #endregion

        #region 초기화
        /// <summary>
        /// 조회 조건 초기화
        /// </summary>
        private void InitializeCondition()
        {

            //적용구분
            cboPROCESSCLASSTYPE.DisplayMember = "CODENAME";
            cboPROCESSCLASSTYPE.ValueMember = "CODEID";
            cboPROCESSCLASSTYPE.ShowHeader = false;

            cboPROCESSCLASSTYPE.EmptyItemValue = 0;
            cboPROCESSCLASSTYPE.EmptyItemCaption = "전체";
            cboPROCESSCLASSTYPE.UseEmptyItem = true;

            Dictionary<string, object> ParamConsumableType = new Dictionary<string, object>();
            ParamConsumableType.Add("CODECLASSID", "ProcessClassType");
            ParamConsumableType.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtConsumableType = SqlExecuter.Query("GetCodeList", "00001", ParamConsumableType);
            cboPROCESSCLASSTYPE.DataSource = dtConsumableType;



            //재작업구분
            cboPROCESSCLASSID.DisplayMember = "PROCESSCLASSNAME";
            cboPROCESSCLASSID.ValueMember = "PROCESSCLASSID";
            cboPROCESSCLASSID.ShowHeader = false;

            cboPROCESSCLASSID.EmptyItemValue = 0;
            cboPROCESSCLASSID.EmptyItemCaption = "전체";
            cboPROCESSCLASSID.UseEmptyItem = true;

            Dictionary<string, object> ParamPROCESSCLASSID = new Dictionary<string, object>();
            ParamPROCESSCLASSID.Add("ENTERPRISEID", UserInfo.Current.Enterprise );
            ParamPROCESSCLASSID.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtPROCESSCLASSID = SqlExecuter.Query("GetProcessclassCombo", "10001", ParamPROCESSCLASSID);
            cboPROCESSCLASSID.DataSource = dtPROCESSCLASSID;

            //대공정
            cboTOPPROCESSSEGMENTID.DisplayMember = "PROCESSSEGMENTCLASSNAME";
            cboTOPPROCESSSEGMENTID.ValueMember = "PROCESSSEGMENTCLASSID";
            cboTOPPROCESSSEGMENTID.ShowHeader = false;

            cboTOPPROCESSSEGMENTID.EmptyItemValue = 0;
            cboTOPPROCESSSEGMENTID.EmptyItemCaption = "전체";
            cboTOPPROCESSSEGMENTID.UseEmptyItem = true;

            Dictionary<string, object> ParamTOPPROCESSSEGMENTID = new Dictionary<string, object>();
            ParamTOPPROCESSSEGMENTID.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            ParamTOPPROCESSSEGMENTID.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtTOPPROCESSSEGMENTID = SqlExecuter.Query("GetProcessSegMentTop", "10001", ParamTOPPROCESSSEGMENTID);
            cboTOPPROCESSSEGMENTID.DataSource = dtTOPPROCESSSEGMENTID;

        }

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGridIdDefinitionManagement()
        {
            // GRID 초기화
            grdReworkRoutingCopy.GridButtonItem = GridButtonItem.None;   // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기

            
            grdReworkRoutingCopy.View.AddComboBoxColumn("PROCESSCLASSTYPE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProcessClassType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsReadOnly();
            grdReworkRoutingCopy.View.AddComboBoxColumn("PROCESSCLASSID_R", 100, new SqlQuery("GetProcessclassCombo", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PROCESSCLASSNAME", "PROCESSCLASSID").SetIsReadOnly();
            grdReworkRoutingCopy.View.AddComboBoxColumn("TOPPROCESSSEGMENTID", 100, new SqlQuery("GetProcessSegMentTop", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PROCESSSEGMENTCLASSNAME", "PROCESSSEGMENTCLASSID").SetIsReadOnly();
            grdReworkRoutingCopy.View.AddTextBoxColumn("PROCESSDEFID_R", 150).SetIsReadOnly();
            grdReworkRoutingCopy.View.AddTextBoxColumn("PROCESSDEFVERSION_R", 150).SetIsReadOnly();
            grdReworkRoutingCopy.View.AddTextBoxColumn("PROCESSDEFNAME_R", 150).SetIsReadOnly();
            grdReworkRoutingCopy.View.AddTextBoxColumn("DESCRIPTION", 250).SetIsReadOnly();
            grdReworkRoutingCopy.View.PopulateColumns();


        }
        #endregion

        #region  이벤트
        private void InitializeEvent()
        {
            btnSearch.Click += BtnSearch_Click;
            btnCopy.Click += BtnCopy_Click;
            btnVersionup.Click += BtnVersionup_Click;
            btnCancel.Click += BtnCancel_Click;

            grdReworkRoutingCopy.View.RowCellStyle += grdReworkRoutingCopy_RowCellStyle;


        }

        private void BtnCopy_Click(object sender, EventArgs e)
        {
            SaveData("COPY");

        }

        private void grdReworkRoutingCopy_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if(grdReworkRoutingCopy.View.IsRowSelected(e.RowHandle))
            {
                e.Appearance.BackColor = Color.LightBlue;
            }
        }

        private void SaveData(string state)
        {
            if (grdReworkRoutingCopy.View.FocusedRowHandle < 0)
                return;

            DataRow rowFocusedData = grdReworkRoutingCopy.View.GetFocusedDataRow();


            DataTable dtRework = new DataTable();

            dtRework.Columns.Add("PROCESSDEFID_R");
            dtRework.Columns.Add("PROCESSDEFVERSION_R");
            dtRework.Columns.Add("TOPPROCESSSEGMENTID");
            dtRework.Columns.Add("ENTERPRISEID");
            dtRework.Columns.Add("PLANTID");
            dtRework.Columns.Add("_STATE_");
            dtRework.Rows.Add(new object[]
            {
                  rowFocusedData["PROCESSDEFID_R"].ToString()
                , rowFocusedData["PROCESSDEFVERSION_R"].ToString()
                , rowFocusedData["TOPPROCESSSEGMENTID"].ToString()
                , rowFocusedData["ENTERPRISEID"].ToString()
                , UserInfo.Current.Plant
                , state

            });

            MessageWorker worker = new MessageWorker("ReworkRoutingCopy");
            worker.SetBody(new MessageBody()
                        {
                             { "productDefinitionList", dtRework }
                        });

            worker.Execute();

            MSGBox.Show(MessageBoxType.Information, "SuccedSave");
        }
        private void BtnVersionup_Click(object sender, EventArgs e)
        {

            SaveData("VERSIONUP");

        }
        /// <summary>
        /// 조회 클릭 - 메인 grid에 체크 데이터 전달
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearch_Click(object sender, EventArgs e)
        {

            string sConsumableType = "";
            if ( cboPROCESSCLASSTYPE.GetDataValue() != null)
            {
                sConsumableType = cboPROCESSCLASSTYPE.GetDataValue().ToString();
            }

            string sMasterDataClassId = "";
            if (cboPROCESSCLASSID.GetDataValue() != null)
            {
                sMasterDataClassId = cboPROCESSCLASSID.GetDataValue().ToString();
            }

            

            Search(cboPROCESSCLASSTYPE.GetDataValue() == null ? "" : cboPROCESSCLASSTYPE.GetDataValue() == null ? "" : cboPROCESSCLASSTYPE.GetDataValue().ToString(), cboPROCESSCLASSID.GetDataValue() == null ? "" : cboPROCESSCLASSID.GetDataValue().ToString(), cboTOPPROCESSSEGMENTID.GetDataValue() == null ? "" : cboTOPPROCESSSEGMENTID.GetDataValue().ToString(), txtPROCESSDEFID.Text, txtPROCESSDEFNAME.Text);
        }

        void Search(string sPROCESSCLASSTYPE, string sPROCESSCLASSID, string sTOPPROCESSSEGMENTID, string sPROCESSDEFID, string sPROCESSDEFNAME)
        {
            Dictionary<string, object> Param = new Dictionary<string, object>();
            Param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            Param.Add("P_PROCESSCLASSTYPE", sPROCESSCLASSTYPE);
            Param.Add("PROCESSCLASSID_R", sPROCESSCLASSID);
            Param.Add("TOPPROCESSSEGMENTID", sTOPPROCESSSEGMENTID);

            Param.Add("PROCESSDEFID_R", sPROCESSDEFID);
            Param.Add("PROCESSDEFNAME_R", sPROCESSDEFNAME);
            Param.Add("P_PLANTID", this.plantId);

            DataTable dt = SqlExecuter.Query("GetProcessdefinitionList", "10001", Param);
            grdReworkRoutingCopy.DataSource = dt;

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


        protected override bool HotKeyDown(Keys keyData)
        {
            if (base.HotKeyDown(keyData)) return true;

            bool isProcessed = false;

            if (keyData == Keys.F5)
            {
                BtnSearch_Click(this.btnSearch, null);
                   isProcessed = true;
            }

            return isProcessed;
        }


        #endregion
    }
}
