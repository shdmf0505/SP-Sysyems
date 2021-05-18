#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;

using DevExpress.XtraEditors.Controls;

using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;

#endregion

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 작업 조건별 수율 조회 화면에 작업조건 조회조건 Select Popup
    /// 업  무  설  명  : 작업조건(Recipe) 화면에 Sub List에 들어가는 Group 수집 공통 모듈
    /// 생    성    자  : 전우성
    /// 생    성    일  : 2020-01-07
    /// 수  정  이  력  : 
    /// 
    /// </summary>
    public partial class popupRecipeGroupList : SmartPopupBaseForm, ISmartCustomPopup
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

        public popupRecipeGroupList()
        {
            InitializeComponent();

            InitializeLanguageKey();
            InitializeEvent();
            InitializeControls();
            InitializeGrid();
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
            grdMain.LanguageKey = "RECIPELIST";
            grdSub.LanguageKey = "PARAMETERLIST";
        }

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            #region Recipe List

            grdMain.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdMain.View.AddTextBoxColumn("EQUIPMENTNAME", 120).SetTextAlignment(TextAlignment.Left);
            grdMain.View.AddTextBoxColumn("RECIPEID", 120).SetTextAlignment(TextAlignment.Left);
            grdMain.View.AddTextBoxColumn("PRODUCTTYPENAME", 80).SetLabel("PRODUCTSHAPE").SetTextAlignment(TextAlignment.Left);
            grdMain.View.AddTextBoxColumn("CREATEDTIME", 160).SetTextAlignment(TextAlignment.Left);
            grdMain.View.AddTextBoxColumn("EQUIPMENTID", 50).SetIsHidden();
            grdMain.View.AddTextBoxColumn("PRODUCTTYPE", 50).SetIsHidden();

            grdMain.View.PopulateColumns();
            grdMain.View.BestFitColumns();

            grdMain.View.SetIsReadOnly();
            grdMain.ShowStatusBar = true;
            grdMain.GridButtonItem = GridButtonItem.None;

            #endregion

            #region Parameter List

            grdSub.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            grdSub.View.AddTextBoxColumn("PARAMETERID", 80).SetTextAlignment(TextAlignment.Left);
            grdSub.View.AddTextBoxColumn("PARAMETERNAME", 100).SetTextAlignment(TextAlignment.Left);
            grdSub.View.AddTextBoxColumn("LOWERLIMIT", 50).SetLabel("MAXVALUE").SetTextAlignment(TextAlignment.Right);
            grdSub.View.AddTextBoxColumn("LSL", 50).SetTextAlignment(TextAlignment.Right);
            grdSub.View.AddTextBoxColumn("TARGET", 50).SetTextAlignment(TextAlignment.Right);
            grdSub.View.AddTextBoxColumn("USL", 50).SetTextAlignment(TextAlignment.Right);
            grdSub.View.AddTextBoxColumn("UPPERLIMIT", 50).SetLabel("MINVALUE").SetTextAlignment(TextAlignment.Right);
            grdSub.View.AddTextBoxColumn("VALIDATIONTYPE", 90).SetTextAlignment(TextAlignment.Left);

            grdSub.View.PopulateColumns();
            grdSub.View.BestFitColumns();

            grdSub.View.SetIsReadOnly();
            grdSub.ShowStatusBar = true;
            grdSub.GridButtonItem = GridButtonItem.None;

            #endregion
        }

        #endregion

        #region Event

        /// <summary>
        /// Event 초기화 
        /// </summary>
        private void InitializeEvent()
        {
            //! 레시피 Grid cell click 이벤트
            grdMain.View.FocusedRowChanged += (s, e) =>
            {
                if (grdMain.View.GetFocusedDataRow() is DataRow dr)
                {
                    if(SqlExecuter.Query("GetParamList", "10001", DefectMapHelper.AddLanguageTypeToConditions(new Dictionary<string, object>
                                                                            {
                                                                                {"P_EQUIPMENTID", DefectMapHelper.StringByDataRowObejct(dr, "EQUIPMENTID") },
                                                                                {"P_RECIPEID", DefectMapHelper.StringByDataRowObejct(dr, "RECIPEID") }
                                                                            })) is DataTable dt)
                    {
                        grdSub.DataSource = dt;
                    }
                }
            };

            //! OK버튼 클릭이벤트
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
            if(grdMain.View.GetCheckedRows().Rows.Count.Equals(0))
            {
                return;
            }

            if ((checkedControl.ItemCount + grdMain.View.GetCheckedRows().Rows.Count) > 2)
            {
                ShowMessage("GroupRowCntCheck");
                return;
            }

            foreach(DataRow dr in grdMain.View.GetCheckedRows().Rows)
            {
                if(checkedControl.FindString(DefectMapHelper.StringByDataRowObejct(dr, "RECIPEID")).Equals(-1))
                {
                    checkedControl.Items.Add(DefectMapHelper.StringByDataRowObejct(dr, "RECIPEID"), DefectMapHelper.StringByDataRowObejct(dr, "RECIPEID"));
                }
            }

            grdMain.View.UncheckedAll();
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
            grdMain.DataSource = dt;
        }

        #endregion
    }
}
