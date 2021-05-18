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
    /// <summary>
    /// 프로그램명 : 기준정보 > 사양진행관리 > CNC Data관리 - 권한 관리 POPUP
    /// 업 무 설명 : CNC Data관리
    /// 생  성  자 : 윤성원
    /// 생  성  일 : 2019-12-06
    /// 수정 이 력 : 
    /// 
    /// 
    /// </summary> 
    public partial class CNCAuthority : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region Interface

        public DataRow CurrentDataRow { get; set; }

        #endregion

        #region Local Variables
        public DataTable checkTable;
        #endregion

        #region 생성자
        public CNCAuthority()
        {
            InitializeComponent();
            InitializeGrid();
            InitializeEvent();
            InitializeControl();
        }
        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// Grid 초기화
        /// </summary>
        private void InitializeGrid()
        {
            grdUserListPopup.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdUserListPopup.View.AddTextBoxColumn("USERNAME", 100)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left);

            InitializeGrd_UserListPopup();
            ConditionItemComboBox comboBox = new ConditionItemComboBox();

            grdUserListPopup.View.AddComboBoxColumn("PRODUCTIONAUTHORITY", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProdAuthorityType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("PRODUCTAUTHORITY")
                .SetTextAlignment(TextAlignment.Center);
            grdUserListPopup.View.AddComboBoxColumn("SAMPLEAUTHORITY", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=SampleAuthorityType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("SAMPLEAUTHORITY")
                .SetTextAlignment(TextAlignment.Center);
            grdUserListPopup.View.AddComboBoxColumn("PROCESSTYPE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=CNCType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetEmptyItem("ALL", "All", true)
                .SetResultCount(0)
                .SetLabel("PROCTYPE")
                .SetTextAlignment(TextAlignment.Center);

            grdUserListPopup.View.AddTextBoxColumn("EMAILADDRESS", 180)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left);
             
            grdUserListPopup.View.AddTextBoxColumn("CELLPHONENUMBER", 100)
                .SetLabel("CONTACT")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left);

            grdUserListPopup.View.PopulateColumns();
        }
        #endregion

        #region 컨텐츠 영역 초기화
        private void InitializeGrd_UserListPopup()
        {
            //팝업 컬럼 설정
            var UserIdName = grdUserListPopup.View.AddSelectPopupColumn("USERID", 100, new SqlQuery("GetUserList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"))
                                                       .SetPopupLayout("USERLIST", PopupButtonStyles.Ok_Cancel, true, false)
                                                       .SetPopupResultCount(0)
                                                       .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow)
                                                       .SetValidationKeyColumn()
                                                       .SetPopupApplySelection((selectedRows, dataGridRow) =>
                                                       {

                                                           DataTable dt = grdUserListPopup.DataSource as DataTable;
                                                           int handle = grdUserListPopup.View.FocusedRowHandle;


                                                           dt.Rows.RemoveAt(handle);
                                                           // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
                                                           // dataGridRow : 현재 Focus가 있는 그리드의 DataRow
                                                           foreach (DataRow row in selectedRows)
                                                           {
                                                               DataRow newrow = dt.NewRow();


                                                               newrow["USERNAME"] = row["USERNAME"].ToString();
                                                               newrow["EMAILADDRESS"] = row["EMAILADDRESS"].ToString();
                                                               newrow["CELLPHONENUMBER"] = row["CELLPHONENUMBER"].ToString();
                                                               newrow["USERID"] = row["USERID"].ToString();

                                                               dt.Rows.Add(newrow);

                                                           }

                                                       });

            UserIdName.Conditions.AddTextBox("USERIDNAME");

            UserIdName.GridColumns.AddTextBoxColumn("USERID", 80);
            UserIdName.GridColumns.AddTextBoxColumn("USERNAME", 80);
            UserIdName.GridColumns.AddTextBoxColumn("DEPARTMENT", 100);
            UserIdName.GridColumns.AddTextBoxColumn("EMAILADDRESS", 80);
            UserIdName.GridColumns.AddTextBoxColumn("CELLPHONENUMBER", 80);
        }

        private void InitializeControl()
        {
            // 사용자 검색
            ConditionItemSelectPopup userList = new ConditionItemSelectPopup();
            userList.SetPopupLayoutForm(800, 600, FormBorderStyle.FixedDialog);
            userList.SetPopupLayout("USERID", PopupButtonStyles.Ok_Cancel);

            userList.Id = "USERID";
            userList.LabelText = "USERNAME";
            userList.SearchQuery = new SqlQuery("GetUserList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}");
            userList.IsMultiGrid = false;
            userList.DisplayFieldName = "USERNAME";
            userList.ValueFieldName = "USERID";
            userList.LanguageKey = "USERID";

            userList.Conditions.AddTextBox("USERIDNAME");

            userList.GridColumns.AddTextBoxColumn("USERID", 80);
            userList.GridColumns.AddTextBoxColumn("USERNAME", 80);
            userList.GridColumns.AddTextBoxColumn("DEPARTMENT", 100);
            userList.GridColumns.AddTextBoxColumn("EMAILADDRESS", 80);
            userList.GridColumns.AddTextBoxColumn("CELLPHONENUMBER", 80);

            sspUser.SelectPopupCondition = userList;
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
            //닫기버튼 클릭 이벤트
            btn_close.Click += Btn_close_Click;
            //취소버튼 클릭 이벤트
            btnSave.Click += BtnSave_Click;
        }

        private void Btn_close_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();

        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            DataTable changeRows = grdUserListPopup.GetChangedRows();

            if (changeRows.Rows.Count != 0)
            {
                ExecuteRule("SaveCNCDataAuthority", changeRows);

                //this.DialogResult = DialogResult.Cancel;
                //this.Close();

                SearchAuthorityList();
                ShowMessage("SuccessSave");
            }
            else
            {
                //저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }

        /// <summary>
        /// 검색 버튼 클릭시 조회하는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            SearchAuthorityList();
        }

        #endregion

        #region Public Function

        /// <summary>
        /// 권한 사용자 조회하는 함수
        /// </summary>
        private void SearchAuthorityList()
        {

            try
            {
                this.ShowWaitArea();
                btnSearch.Enabled = false;

                Dictionary<string, object> values = new Dictionary<string, object>();
                values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                values.Add("PLANTID", UserInfo.Current.Plant);
                values.Add("USERIDNAME", sspUser.GetValue());

                DataTable dt = SqlExecuter.Query("GetCNCUserAuthorityList", "10001", values);

                grdUserListPopup.DataSource = dt;

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
