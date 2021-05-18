#region using

using DevExpress.XtraGrid.Views.Grid;
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

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 불량관리 > 불량품 폐기취소
    /// 업  무  설  명  : 취소한 불량에 대해 Lot별로 어떤 라우팅을 태웠는지 확인하는 팝업
    /// 생    성    자  : 유태근
    /// 생    성    일  : 2019-09-23
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class DefectRoutingPathPopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region Local Variables

        /// <summary>
        /// 팝업진입전 그리드의 Row
        /// </summary>
        public DataRow CurrentDataRow { get; set; }

        /// <summary>
        /// 그리드에 보여줄 데이터테이블
        /// </summary>
        private DataTable _defectCancelProductRouting = new DataTable();
        private DataTable _defectCancelReworkRouting = new DataTable();

        #endregion

        #region 생성자

        /// <summary>
        /// 생성자
        /// </summary>
        public DefectRoutingPathPopup()
        {
            InitializeComponent();

            InitializeEvent();
        }

        #endregion

        #region 컨텐츠 영역 초기화
        
        /// <summary>
        /// TextBox 초기화
        /// </summary>
        private void InitializeControlSetting()
        {
            txtReasonCancel.EditValue = CurrentDataRow["CANCELREASON"]; // 취소사유
            txtRepairLotNo.EditValue = CurrentDataRow["REPAIRLOTNO"]; // Repair Lot No

            // 재작업라우팅일때 바인딩
            if (CurrentDataRow["CANCELROUTINGTYPE"].Equals("Rework"))
            {
                string defId = _defectCancelReworkRouting.Rows[0]["PROCESSDEFID"] == null ? null : Format.GetString(_defectCancelReworkRouting.Rows[0]["PROCESSDEFID"]);
                string defName = _defectCancelReworkRouting.Rows[0]["PROCESSDEFNAME"] == null ? null : Format.GetString(_defectCancelReworkRouting.Rows[0]["PROCESSDEFNAME"]);

                txtReworkRoutingId.EditValue = defId; // 재작업 라우팅ID
                txtReworkRoutingName.EditValue = defName; // 재작업 라우팅명
                txtReworkResource.EditValue = CurrentDataRow["RESOURCENAME"]; // 재작업 공정 자원명
                txtProductResource.EditValue = CurrentDataRow["RETURNRESOURCENAME"]; // 재작업 후 공정 자원명
                grdProductRouting.DataSource = _defectCancelProductRouting;
                grdReworkRouting.DataSource = _defectCancelReworkRouting;
            }
            // 품목라우팅일때 재작업라우팅 그리드 ReadOnly
            else if (CurrentDataRow["CANCELROUTINGTYPE"].Equals("Product"))
            {
                txtProductResource.EditValue = CurrentDataRow["RESOURCENAME"]; // 돌아올 공정의 자원명
                grdProductRouting.DataSource = _defectCancelProductRouting;
                gbxReworkRouting.Enabled = false;
            }
        }

        /// <summary>
        /// 품목라우팅 그리드 초기화
        /// </summary>
        private void InitializeProductRouting()
        {
            grdProductRouting.View.SetIsReadOnly();
            grdProductRouting.View.SetAutoFillColumn("PROCESSSEGMENTNAME");

            grdProductRouting.View.AddTextBoxColumn("USERSEQUENCE", 80); // 순서
            grdProductRouting.View.AddTextBoxColumn("PROCESSSEGMENTID", 150); // 공정 ID
            grdProductRouting.View.AddTextBoxColumn("PROCESSSEGMENTVERSION", 80)
                .SetTextAlignment(TextAlignment.Center); // 공정 Version
            grdProductRouting.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 220); // 공정명
            grdProductRouting.View.AddTextBoxColumn("PROCESSPATHID", 100)
                .SetIsHidden(); // 라우팅 상세 ID

            grdProductRouting.View.PopulateColumns();

            grdProductRouting.DataSource = _defectCancelProductRouting;
        }

        /// <summary>
        /// 재작업라우팅 그리드 초기화
        /// </summary>
        private void InitializeReworkRouting()
        {
            grdReworkRouting.View.SetIsReadOnly();
            grdReworkRouting.View.SetAutoFillColumn("PROCESSSEGMENTNAME");

            grdReworkRouting.View.AddTextBoxColumn("USERSEQUENCE", 80); // 순서
            grdReworkRouting.View.AddTextBoxColumn("PROCESSSEGMENTID", 150); // 공정 ID
            grdReworkRouting.View.AddTextBoxColumn("PROCESSSEGMENTVERSION", 80)
                .SetTextAlignment(TextAlignment.Center); // 공정 Version
            grdReworkRouting.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 220); // 공정명
            grdReworkRouting.View.AddTextBoxColumn("PROCESSPATHID", 100)
                .SetIsHidden(); // 라우팅 상세 ID

            grdReworkRouting.View.PopulateColumns();

            grdReworkRouting.DataSource = _defectCancelReworkRouting;
        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            this.Load += ChemicalissuePopup_Load;

            btnClose.Click += BtnClose_Click;
            grdProductRouting.View.RowStyle += View_RowStyle;
        }

        /// <summary>
        /// 품목라우팅일 경우 시작점을 표시해주는 Row 색깔
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_RowStyle(object sender, RowStyleEventArgs e)
        {
            // 품목라우팅일 경우(사용자가 선택한곳을 표시)
            if (CurrentDataRow["CANCELROUTINGTYPE"].Equals("Product"))
            {
                if (e.RowHandle != -1)
                {
                    // ProcessPathID로 비교시 이후 샘플라우팅 로직을 타면 기준정보가 변하기때문에 공정ID와 공정수순으로 비교
                    if (grdProductRouting.View.GetRowCellValue(e.RowHandle, "PROCESSSEGMENTID").Equals(CurrentDataRow["INPUTSEGMENTID"])
                        && grdProductRouting.View.GetRowCellValue(e.RowHandle, "USERSEQUENCE").Equals(CurrentDataRow["INPUTUSERSEQUENCE"]))
                    {
                        e.HighPriority = true;
                        e.Appearance.BackColor = Color.Yellow;
                    }
                    //if (grdProductRouting.View.GetRowCellValue(e.RowHandle, "PROCESSPATHID").Equals(CurrentDataRow["PROCESSPATHID"]))
                    //{
                    //    e.HighPriority = true;
                    //    e.Appearance.BackColor = Color.Yellow;
                    //}                    
                }      
            }
            // 재작업라우팅일 경우(재작업 후 사용자가 돌아올 곳을 표시)
            else if (CurrentDataRow["CANCELROUTINGTYPE"].Equals("Rework"))
            {
                if (e.RowHandle != -1)
                {
                    // ProcessPathID로 비교시 이후 샘플라우팅 로직을 타면 기준정보가 변하기때문에 공정ID와 공정수순으로 비교
                    if (grdProductRouting.View.GetRowCellValue(e.RowHandle, "PROCESSSEGMENTID").Equals(CurrentDataRow["RETURNPROCESSSEGMENTID"])
                        && grdProductRouting.View.GetRowCellValue(e.RowHandle, "USERSEQUENCE").Equals(CurrentDataRow["RETURNUSERSEQUENCE"]))
                    {
                        e.HighPriority = true;
                        e.Appearance.BackColor = Color.Yellow;
                    }
                    //if (grdProductRouting.View.GetRowCellValue(e.RowHandle, "PROCESSPATHID").Equals(CurrentDataRow["RETURNPROCESSPATHID"]))
                    //{
                    //    e.HighPriority = true;
                    //    e.Appearance.BackColor = Color.Yellow;
                    //}
                }
            }
        }

        /// <summary>
        /// 닫기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// Form Road시 초기화
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChemicalissuePopup_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            SearchProductRouting();
            SearchReworkRouting();

            InitializeControlSetting();
            InitializeProductRouting();
            InitializeReworkRouting();
        }

        #endregion
 
        #region Private Function

        /// <summary>
        /// 품목라우팅 검색
        /// </summary>
        private void SearchProductRouting()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LOTID", CurrentDataRow["REPAIRLOTNO"]);
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            _defectCancelProductRouting = SqlExecuter.Query("GetProductRoutingPreviousProcessPaths", "10003", param);
        }

        /// <summary>
        /// 재작업라우팅 검색
        /// </summary>
        private void SearchReworkRouting()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("LOTID", CurrentDataRow["LOTID"].ToString());
            param.Add("TXNHISTKEY", CurrentDataRow["TXNHISTKEY"].ToString());
            param.Add("REQUESTNO", CurrentDataRow["REQUESTNO"].ToString());

            _defectCancelReworkRouting = SqlExecuter.Query("GetDefectCancelReworkRouting", "10001", param);
        }

        #endregion
    }
}
