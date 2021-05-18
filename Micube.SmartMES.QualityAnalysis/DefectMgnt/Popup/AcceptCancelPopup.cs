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
    /// 프 로 그 램 명  : 품질관리 > 승인/반려 Comment 작성 Popup
    /// 업  무  설  명  : Lot 병합요청건에 대해 승인이나 반려 Comment를 저장한다.
    /// 생    성    자  : 유태근
    /// 생    성    일  : 2019-07-25
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class AcceptCancelPopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region Local Variables

        /// <summary>
        /// 팝업진입전 그리드의 Row
        /// </summary>
        public DataRow CurrentDataRow { get; set; }

        /// <summary>
        /// 승인인지 반려인지 체크하는 변수
        /// </summary>
        public string approvalFlag;

        #endregion

        #region 생성자

        /// <summary>
        /// 생성자
        /// </summary>
        public AcceptCancelPopup()
        {
            InitializeComponent();

            InitializeEvent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 화면에 데이터 바인딩
        /// </summary>
        private void InitializeCurrentData()
        {

        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            this.Load += CommentPopup_Load;

            btnSave.Click += BtnSave_Click;
            btnClose.Click += BtnClose_Click;
        }

        /// <summary>
        /// 승인/반려 Comment 저장
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;

            if (this.ShowMessage(MessageBoxButtons.YesNo, "병합 승인/반려 하시겠습니까?") == DialogResult.Yes)
            {
                MessageWorker worker = new MessageWorker("SaveLotDefectMergeApproval");
                worker.SetBody(new MessageBody()
                    {
                        { "approvalUser", UserInfo.Current.Id }, // 승인자
                        { "approvalDate", DateTime.Now }, // 승인일시   
                        { "approvalComment", memoApprovalComment.Text }, // 승인/반려 Comment
                        { "requestNo", CurrentDataRow["REQUESTNO"]}, // 요청번호
                        { "productDefId", CurrentDataRow["PRODUCTDEFID"] }, // 품목 ID
                        { "productDefVersion", CurrentDataRow["PRODUCTDEFVERSION"]}, // 품목 Version
                        { "approvalFlag", approvalFlag} // 승인/반려플래그
                    });

                worker.Execute();

                this.ShowMessage("저장되었습니다."); // 병합 요청되었습니다.
                this.Close();
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
        private void CommentPopup_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            InitializeCurrentData();            
        }

        #endregion
 
        #region Private Function

        #endregion
    }
}
