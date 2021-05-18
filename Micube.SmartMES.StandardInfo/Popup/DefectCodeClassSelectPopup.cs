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

namespace Micube.SmartMES.StandardInfo
{
    public partial class DefectCodeClassSelectPopup : SmartPopupBaseForm, ISmartCustomPopup
    {

        #region Interface

        public DataRow CurrentDataRow { get; set; }

        #endregion

        #region Local Variables
        public DataTable checkTable;
        #endregion

        #region 생성자
        public DefectCodeClassSelectPopup()
        {
            InitializeComponent();
            InitializeGrid();
            InitializeEvent();
        }
        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// Grid 초기화
        /// </summary>
        private void InitializeGrid()
        {
            grdDefectCodeClassPopup.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdDefectCodeClassPopup.GridButtonItem = GridButtonItem.None;
            grdDefectCodeClassPopup.View.SetIsReadOnly();

            grdDefectCodeClassPopup.View.AddTextBoxColumn("DEFECTCODECLASSID", 150);

            grdDefectCodeClassPopup.View.AddTextBoxColumn("DEFECTCODECLASSNAME", 200);

            grdDefectCodeClassPopup.View.AddLanguageColumn("DEFECTCODECLASSNAME", 150);

            grdDefectCodeClassPopup.View.AddTextBoxColumn("DESCRIPTION", 150);

            grdDefectCodeClassPopup.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Valid")
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Center);
            grdDefectCodeClassPopup.View.PopulateColumns();
        }


        #endregion

        #region Event

        /// <summary>
        /// Event 초기화 
        /// </summary>
        private void InitializeEvent()
        {
            //검색버튼 클릭 이벤트
            btnSearch.Click += BtnSearch_Click;

            txtClassId.Editor.KeyDown += TxtClassId_KeyDown;
            txtCodeName.Editor.KeyDown += TxtClassId_KeyDown;

            //취소버튼 클릭 이벤트
            btnCancel.Click += (s, e) =>
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            };

            //확인버튼 클릭 이벤트
            btnOK.Click += BtnOK_Click;
        }

        private void TxtClassId_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode== Keys.Enter)
            SearchDefectCodeClass();
        }

        /// <summary>
        /// 확인 버튼 클릭시 체크된 행들을 checkTable에 넘기는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOK_Click(object sender, EventArgs e)
        {
            DialogResult result = System.Windows.Forms.DialogResult.No;

            result = this.ShowMessage(MessageBoxButtons.YesNo, "SelectDefectCodeClassConfirm");//선택한 불량명을 적용 하시겠습니까?

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    this.ShowWaitArea();
                    btnOK.Enabled = false;
                    btnCancel.Enabled = false;
                    //팝업 확인버튼 클릭시 DialogResult를 OK로 설정 부모 창에서 쓰기 위함
                    this.DialogResult = DialogResult.OK;

                    checkTable = grdDefectCodeClassPopup.View.GetCheckedRows();
                }
                catch (Exception ex)
                {
                    this.ShowError(ex);
                }
                finally
                {
                    this.CloseWaitArea();
                    btnOK.Enabled = true;
                    btnCancel.Enabled = true;
                    this.Close();
                }
            }
     
        }

        /// <summary>
        /// 검색 버튼 클릭시 불량명을 조회하는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            SearchDefectCodeClass();
        }

        #endregion

        #region Public Function

        /// <summary>
        /// 불량명을 조회하는 함수
        /// </summary>
        private void SearchDefectCodeClass()
        {

            try
            {
                this.ShowWaitArea();
                btnSearch.Enabled = false;

                Dictionary<string, object> values = new Dictionary<string, object>();
                values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                values.Add("DEFECTCODECLASSID",txtClassId.EditValue);
                values.Add("DEFECTCODECLASSNAME", txtCodeName.EditValue);
                values.Add("NOTINRESOURCEID", CurrentDataRow["QCSEGMENTID"]);


                DataTable dt = SqlExecuter.Query("SelectDefectCodeClass", "10001", values);

                grdDefectCodeClassPopup.DataSource = dt;

            }
            catch (Exception ex)
            {
                this.ShowError(ex);
            }
            finally
            {
                this.CloseWaitArea();
                btnSearch.Enabled = true;
            }
        }
        #endregion
    }
}
