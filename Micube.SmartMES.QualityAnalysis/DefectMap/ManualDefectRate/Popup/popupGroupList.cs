#region using

using Micube.Framework;
using Micube.Framework.SmartControls;

using DevExpress.XtraEditors.Controls;

using System.Data;
using System.Linq;
using System.Windows.Forms;

#endregion

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 수율의 조회 조건에 Group Type이 사용되는 User Control
    /// 업  무  설  명  : Layer, 공정/설비, 작업조건(Recipe) 화면에 Sub List에 들어가는 Group 수집 공통 모듈
    /// 생    성    자  : 전우성
    /// 생    성    일  : 2019-05-27
    /// 필  수  처  리  :
    /// *** 2차 개발 기준 개발 필수
    /// ****** 1. 조회 조건 확립 후 수정
    /// ****** 2. 조회 조건에 따른 메인 검색 Query 변경
    /// 
    /// 수  정  이  력  : 
    /// 
    /// </summary>
    public partial class popupGroupList : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region Interface

        public DataRow CurrentDataRow { get; set; }

        public event SelectPopupApplyEventHandler Selected;

        #endregion

        #region Local Variables

        /// <summary>
        /// Group List (1, 2, 3) 정보를 보내기 위한 Handler
        /// </summary>
        /// <param name="dt"></param>
        public delegate void ResultGroupDataHandler(DataTable dt);
        public event ResultGroupDataHandler ResultGroupDataEvent;

        #endregion

        #region 생성자

        public popupGroupList()
        {
            InitializeComponent();

            InitializeLanguageKey();
            InitializeEvent();
            InitializeControls();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// Controler 초기화
        /// </summary>
        private void InitializeControls()
        {
            this.AcceptButton = btnOk;
            this.CancelButton = btnCancle;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            chkMain.CheckOnClick = true;
            chkMain.DisplayMember = "CODENAME";
            chkMain.ValueMember = "CODEID";
            chkMain.BorderStyle = BorderStyles.NoBorder;

            chkGroup1.CheckOnClick = true;
            chkGroup1.DisplayMember = "CODENAME";
            chkGroup1.ValueMember = "CODEID";
            chkGroup1.BorderStyle = BorderStyles.NoBorder;

            chkGroup2.CheckOnClick = true;
            chkGroup2.DisplayMember = "CODENAME";
            chkGroup2.ValueMember = "CODEID";
            chkGroup2.BorderStyle = BorderStyles.NoBorder;

            chkGroup3.CheckOnClick = true;
            chkGroup3.DisplayMember = "CODENAME";
            chkGroup3.ValueMember = "CODEID";
            chkGroup3.BorderStyle = BorderStyles.NoBorder;
        }

        /// <summary>
        /// Language Key 초기화
        /// </summary>
        private void InitializeLanguageKey()
        {
            btnOk.LanguageKey = "OK";
            btnCancle.LanguageKey = "CANCEL";
        }

        #endregion

        #region Event

        /// <summary>
        /// Event 초기화 
        /// </summary>
        private void InitializeEvent()
        {
            //! OK버튼 클릭 이벤트
            btnOk.Click += (s, e) =>
            {
                DataTable resultDataTable = new DataTable();
                resultDataTable.Columns.Add("GROUP", typeof(string));
                resultDataTable.Columns.Add("ITEM", typeof(string));
                resultDataTable.Columns.Add("NAME", typeof(string));

                if (!chkGroup1.Items.Count().Equals(0))
                {
                    chkGroup1.Items.ForEach(item =>
                    {
                        resultDataTable.Rows.Add("GROUP1", Format.GetString(item.Value), item.Description);
                    });
                }

                if (!chkGroup2.Items.Count().Equals(0))
                {
                    chkGroup2.Items.ForEach(item =>
                    {
                        resultDataTable.Rows.Add("GROUP2", Format.GetString(item.Value), item.Description);
                    });
                }

                if (!chkGroup3.Items.Count().Equals(0))
                {
                    chkGroup3.Items.ForEach(item =>
                    {
                        resultDataTable.Rows.Add("GROUP3", Format.GetString(item.Value), item.Description);
                    });
                }

                DataTable dt = CurrentDataRow.Table.Clone();
                DataRow dr = null;

                resultDataTable.AsEnumerable()
                               .GroupBy(x => x.Field<string>("ITEM"))
                               .ForEach(item =>
                                {
                                    dr = dt.NewRow();
                                    dr["ITEM"] = item.Key;
                                    dt.Rows.Add(dr);
                                });

                Selected(this, new SelectPopupApplyEventArgs()
                {
                    Selections = dt.Rows.Cast<DataRow>().OrderBy(x => x.Field<string>("ITEM"))
                });

                this.ResultGroupDataEvent(resultDataTable);
                this.Close();
            };

            btnCancle.Click += (s, e) => this.Close();
            btnGroup1In.Click += (s, e) => AddGroupList(chkGroup1);
            btnGroup2In.Click += (s, e) => AddGroupList(chkGroup2);
            btnGroup3In.Click += (s, e) => AddGroupList(chkGroup3);
            btnGroup1Out.Click += (s, e) => RemoveGroupList(chkGroup1);
            btnGroup2Out.Click += (s, e) => RemoveGroupList(chkGroup2);
            btnGroup3Out.Click += (s, e) => RemoveGroupList(chkGroup3);
        }

        #endregion

        #region Private Function

        /// <summary>
        /// Main List에서 선택한 Item Group에 추가한다
        /// </summary>
        /// <param name="checkedControl">Group Controler</param>
        private void AddGroupList(SmartCheckedListBox checkedControl)
        {
            if (chkMain.CheckedItemsCount.Equals(0))
            {
                return;
            }

            if ((checkedControl.ItemCount + chkMain.CheckedItemsCount) > 2)
            {
                ShowMessage("GroupRowCntCheck");
                return;
            }

            foreach (DataRowView item in chkMain.CheckedItems)
            {
                if (checkedControl.FindString(Format.GetString(item.Row.ItemArray[1])).Equals(-1))
                {
                    checkedControl.Items.Add(Format.GetString(item.Row.ItemArray[0]), Format.GetString(item.Row.ItemArray[1]));
                }
            }

            chkMain.UnCheckAll();
        }

        /// <summary>
        /// Group(1, 2, 3) Control에서 선택한 Item 을 삭제한다.
        /// </summary>
        /// <param name="checkedControl">Group Control</param>
        private void RemoveGroupList(SmartCheckedListBox checkedControl)
        {
            if (checkedControl.CheckedItemsCount.Equals(0))
            {
                return;
            }

            for (int i = checkedControl.CheckedItemsCount; i > 0; i--)
            {
                checkedControl.Items.RemoveAt(checkedControl.CheckedIndices[i - 1]);
            }
        }

        #endregion

        #region Public Function

        /// <summary>
        /// Row Data로 화면 설정한다
        /// </summary>
        /// <param name="title">화면 타이틀</param>
        /// <param name="dt">Row Data</param>
        public void SetData(string title, DataTable dt)
        {
            this.Text = Language.Get(title);
            chkMain.DataSource = dt;
        }

        #endregion
    }
}