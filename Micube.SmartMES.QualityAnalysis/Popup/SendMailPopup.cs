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
    /// 프 로 그 램 명  : 품질관리 > 메일보내기 팝업
    /// 업  무  설  명  : 유저를 검색하여 메일과 SMS를 보낸다.
    /// 생    성    자  : 유태근
    /// 생    성    일  : 2019-06-17
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class SendMailPopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region Local Variables

        /// <summary>
        /// 팝업 그리드와 원래 그리드를 비교하기 위한 변수
        /// </summary>
        private DataTable _mappingDataSource = new DataTable();

        /// <summary>
        /// 팝업진입전 그리드의 Row
        /// </summary>
        public DataRow CurrentDataRow { get; set; }

        #endregion

        #region 생성자

        /// <summary>
        /// 생성자
        /// </summary>
        public SendMailPopup()
        {
            InitializeComponent();

            InitializeEvent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// Mail로 보낼 데이터 MemoEdit에 바인딩
        /// </summary>
        private void InitializeCurrentData()
        {
            //grbIssueInformation.GridButtonItem = GridButtonItem.None;

            //txtAnalysisDate.EditValue = CurrentDataRow["ANALYSISDATE"]; // 분석일
            //txtEquipmentName.EditValue = CurrentDataRow["EQUIPMENTNAME"]; // 설비명
            //txtChemicalLevel.EditValue = CurrentDataRow["CHEMICALLEVEL"]; // 약품등급
            //txtDegree.EditValue = CurrentDataRow["DEGREE"]; // 차수
            //txtChildEquipmentName.EditValue = CurrentDataRow["CHILDEQUIPMENTNAME"]; // 설비단명
            //txtManagementScope.EditValue = CurrentDataRow["MANAGEMENTSCOPE"]; // 관리범위
            //txtProcesssegmentclassName.EditValue = CurrentDataRow["PROCESSSEGMENTCLASSNAME"]; // 대공정명
            //txtChemicalName.EditValue = CurrentDataRow["CHEMICALNAME"]; // 약품
            //txtAnalysisValue.EditValue = CurrentDataRow["ANALYSISVALUE"]; // 분석치
        }

        /// <summary>
        /// 유저 리스트 그리드 초기화
        /// </summary>
        private void InitializeUserList()
        {
            grdUserList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdUserList.View.SetIsReadOnly();

            grdUserList.View.AddTextBoxColumn("USERID", 100); // 유저 ID
            grdUserList.View.AddTextBoxColumn("USERNAME", 100); // 유저 Name
            grdUserList.View.AddTextBoxColumn("EMAILADDRESS", 220); // Email
            grdUserList.View.AddTextBoxColumn("CELLPHONENUMBER", 150); // 핸드폰

            grdUserList.View.PopulateColumns();
        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            this.Load += ChemicalissuePopup_Load;

            btnAdd.Click += BtnAdd_Click;
            btnDelete.Click += BtnDelete_Click;
            btnSend.Click += BtnSend_Click;
            btnClose.Click += BtnClose_Click;
        }

        /// <summary>
        /// 메일 송신
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSend_Click(object sender, EventArgs e)
        {
            DataTable dt = grdUserList.DataSource as DataTable;

            if (dt.Rows.Count == 0)
            {
                throw MessageException.Create("NoSaveData");
            }

            if (ShowMessage(MessageBoxButtons.YesNo, "IsSendMail") == DialogResult.Yes)
            {
                DataTable sendTable = new DataTable();
                sendTable.TableName = "list";
                DataRow row = null;

                sendTable.Columns.Add(new DataColumn("USERID", typeof(string)));
                sendTable.Columns.Add(new DataColumn("EMAILADDRESS", typeof(string)));
                sendTable.Columns.Add(new DataColumn("TITLE", typeof(string)));
                sendTable.Columns.Add(new DataColumn("CONTENT", typeof(string)));

                foreach (DataRow dr in dt.Rows)
                {
                    row = sendTable.NewRow();
                    row["USERID"] = UserInfo.Current.Id;
                    row["EMAILADDRESS"] = dr["EMAILADDRESS"];
                    row["TITLE"] = txtTitle.EditValue;
                    row["CONTENT"] = memoEmail.EditValue;

                    sendTable.Rows.Add(row);
                }

                DataSet rullSet = new DataSet();
                rullSet.Tables.Add(sendTable);
                ExecuteRule("SendMailChemicalAnalysis", rullSet);

                ShowMessage("SuccessSendMail");
                this.Close();
            }
        }

        /// <summary>
        /// 유저 삭제
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (grdUserList.View.RowCount == 0) return;

            DataTable userList = (grdUserList.DataSource as DataTable).Clone();

            for (int i = 0; i < grdUserList.View.RowCount; i++)
            {
                if (!grdUserList.View.IsRowChecked(i))
                {
                    userList.ImportRow((grdUserList.DataSource as DataTable).Rows[i]);
                }
            }

            grdUserList.View.ClearDatas();
            grdUserList.DataSource = userList;
        }

        /// <summary>
        /// 유저 추가
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            selectUserPopup();
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

            InitializeUserList();
            InitializeCurrentData();            
        }

        #endregion
 
        #region Private Function

        #endregion

        #region Popup

        /// <summary>
        /// 유저 선택팝업
        /// </summary>
        private void selectUserPopup()
        {
            var userPopup = this.CreateSelectPopup("USERID", new SqlQuery("GetUserList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetPopupLayout("USERLIST", PopupButtonStyles.Ok_Cancel, true, true)
                .SetPopupResultCount(0)
                .SetPopupLayoutForm(700, 600, System.Windows.Forms.FormBorderStyle.FixedToolWindow);

            userPopup.Conditions.AddTextBox("USERIDNAME");

            userPopup.GridColumns.AddTextBoxColumn("USERID", 100);
            userPopup.GridColumns.AddTextBoxColumn("USERNAME", 100)
                .SetTextAlignment(TextAlignment.Center);
            userPopup.GridColumns.AddTextBoxColumn("EMAILADDRESS", 200); 
            userPopup.GridColumns.AddTextBoxColumn("CELLPHONENUMBER", 150);

            DataTable mappingTable = grdUserList.DataSource as DataTable;
            var filter = (grdUserList.View.DataSource as DataView).RowStateFilter;

            IEnumerable<DataRow> selectedDatas = this.ShowPopup(userPopup, mappingTable.Rows.Cast<DataRow>()
                                                                                            .Where(m => !grdUserList.View.IsDeletedRow(m)));

            if (selectedDatas == null)
            {
                return;
            }
            else
            {
                if (_mappingDataSource.Columns.Count == 0) // 최초 검색했을때 변수에 담아두어 유지하고 있어야함.
                {
                    _mappingDataSource.Columns.Add("USERID", typeof(string));
                    _mappingDataSource.Columns.Add("USERNAME", typeof(string));
                    _mappingDataSource.Columns.Add("EMAILADDRESS", typeof(string));
                    _mappingDataSource.Columns.Add("CELLPHONENUMBER", typeof(string));
                }

                DataTable mdt = (grdUserList.DataSource as DataTable).Clone();
                
                foreach (DataRow row in selectedDatas)
                {
                    mdt.ImportRow(row);
                }

               (grdUserList.DataSource as DataTable).Merge(mdt, true, MissingSchemaAction.Ignore);
                grdUserList.DataSource = (grdUserList.DataSource as DataTable).DefaultView.ToTable(true);

                // DataTable의 RowStatus 즉 추가, 삭제, 수정 형태를 유지해주는 메소드
                // 원래것과 매핑결과를 비교하여 새로운 DataSource를 생성해 준다
                //grdUserList.SetDataSourceRemainRowStatus(DataSourceHelper.MappingChanged(_mappingDataSource, selectedDatas, new List<string>() { "USERID" }));
            }
        }

        #endregion
    }
}
