#region using

using DevExpress.XtraGrid.Views.Base;

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons.Controls;
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

namespace Micube.SmartMES.SystemManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 시스템 관리 > 다국어 관리 > 메세지 정보
    /// 업  무  설  명  : 메세지 정보를 관리한다.
    /// 생    성    자  : 강호윤
    /// 생    성    일  : 2019-04-01
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class Message : SmartConditionManualBaseForm
    {
        #region Local Variables

        #endregion

        #region 생성자

        public Message()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화
        /// <summary>
        /// 우측 컨텐츠 영역에 초기화할 코드를 넣으세요.
        /// </summary>
        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeEvent();

            InitializeMessageClass();
            InitializeMessage();

            LoadDataMessageClass();
        }

        private void InitializeMessageClass()
        {
            grdMessageClass.View.SetIsReadOnly();

            grdMessageClass.GridButtonItem -= GridButtonItem.All;
            grdMessageClass.View.SetAutoFillColumn("MESSAGECLASSNAME");

            grdMessageClass.View.SetSortOrder("MESSAGECLASSID");

            grdMessageClass.View.AddTextBoxColumn("MESSAGECLASSID", 150);
            grdMessageClass.View.AddTextBoxColumn("MESSAGECLASSNAME", 200);

            grdMessageClass.View.PopulateColumns();
            grdMessageClass.GridButtonItem = GridButtonItem.Refresh;
        }

        private void InitializeMessage()
        {
            grdMessage.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdMessage.View.SetSortOrder("MESSAGEID");

            grdMessage.View.AddTextBoxColumn("MESSAGECLASSID", 150)
                .SetValidationIsRequired()
                .SetIsReadOnly();
            grdMessage.View.AddTextBoxColumn("MESSAGEID", 150)
                .SetValidationIsRequired();
            grdMessage.View.AddTextBoxColumn("TITLE", 200);
            grdMessage.View.AddTextBoxColumn("MESSAGENAME", 500);
            grdMessage.View.AddComboBoxColumn("LANGUAGETYPE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=LanguageType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetValidationIsRequired();
            grdMessage.View.AddComboBoxColumn("VALIDSTATE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetTextAlignment(TextAlignment.Center)
                .SetDefault("Valid")
                .SetValidationIsRequired();
            grdMessage.View.AddTextBoxColumn("CREATOR", 80)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();
            grdMessage.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly()
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss");
            grdMessage.View.AddTextBoxColumn("MODIFIER", 80)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();
            grdMessage.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly()
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss");

            grdMessage.View.SetKeyColumn("MESSAGEID");
            grdMessage.View.SetNotAllowNullColumn("VALIDSTATE");

            grdMessage.View.PopulateColumns();
        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        private void InitializeEvent()
        {
            grdMessageClass.ToolbarRefresh += GrdMeesageClass_ToolbarRefresh;
            grdMessageClass.View.FocusedRowChanged += View_FocusedRowChanged;
            grdMessage.View.AddingNewRow += View_AddingNewRow;
        }

        private void GrdMeesageClass_ToolbarRefresh(object sender, EventArgs e)
        {
            try
            {
                int beforeFocusUserClass = grdMessageClass.View.FocusedRowHandle;
                grdMessageClass.ShowWaitArea();
                LoadDataMessageClass();
                grdMessageClass.View.FocusedRowHandle = 0;
                grdMessageClass.View.UnselectRow(beforeFocusUserClass);
                grdMessageClass.View.SelectRow(0);
                focusedRowChanged();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
            finally
            {
                grdMessageClass.CloseWaitArea();
            }
        }

        private void View_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            var row = grdMessageClass.View.GetDataRow(e.FocusedRowHandle);
            var cond = Conditions.GetValues();
            cond["P_MESSAGECLASSID"] = row["MESSAGECLASSID"].ToString();
            //grdMessage.DataSource = SqlExecuter.Procedure("usp_com_selectMessage", cond);//메시지 리스트 조회
            grdMessage.DataSource = SqlExecuter.Query("SelectMessage", "10001", cond);
        }

        private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            DataRow messageClass = grdMessageClass.View.GetFocusedDataRow();
            args.NewRow["MESSAGECLASSID"] = messageClass["MESSAGECLASSID"];
        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        /// <returns></returns>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            DataTable changed = grdMessage.GetChangedRows();

            ExecuteRule("SaveMessage", changed);
        }

        #endregion

        #region 검색

        /// <summary>
        /// 비동기 override 모델, 비동기 모델은 검색에서만 제공합니다. ESC키로 취소 가능합니다.
        /// </summary>
        /// <returns></returns>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            if (grdMessageClass.View.DataRowCount <= 0)
                return;

            int beforeHandle = grdMessageClass.View.FocusedRowHandle;

            DataRow row = grdMessageClass.View.GetFocusedDataRow();
            string bfMessageClassId = row["MESSAGECLASSID"].ToString();

            var values = Conditions.GetValues();
            values.Add("p_LANGUAGETYPE", UserInfo.Current.LanguageType);

            //DataTable dtMessageClass = await ProcedureAsync("usp_com_selectMessageClassOnMapping", values);
            DataTable dtMessageClass = await QueryAsync("SelectMessageClassOnMapping", "10001", values);

            if (dtMessageClass.Rows.Count < 1)
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                dtMessageClass.Clear();
            }
            else if (dtMessageClass.Rows.Count <= beforeHandle)
            {
                grdMessageClass.DataSource = dtMessageClass;

                grdMessageClass.View.FocusedRowHandle = 0;

                DataRow currentRow = grdMessageClass.View.GetFocusedDataRow();

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("P_MESSAGECLASSID", currentRow["MESSAGECLASSID"].ToString());
                //grdMessage.DataSource = SqlExecuter.Procedure("usp_com_selectMessage", param); // 수정 필요
                grdMessage.DataSource = SqlExecuter.Query("SelectMessage", "10001", param);

                grdMessageClass.View.FocusedRowHandle = 0;
                grdMessageClass.View.SelectRow(0);
                focusedRowChanged();
            }
            else
            {
                grdMessageClass.DataSource = dtMessageClass;

                int afterHandle = grdMessageClass.View.FocusedRowHandle;

                // 조회 전 FocusedRowHandle이 조회 후 RowCount보다 크거나 같은 경우 처리
                //if (beforeHandle >= dtCodeClass.Rows.Count)
                //{
                //    beforeHandle = 0;
                //}

                if (beforeHandle == 0 && afterHandle == 0)
                {
                    focusedRowChanged();
                }

                if (beforeHandle > 0)
                {
                    Dictionary<string, object> param = new Dictionary<string, object>();
                    param.Add("P_MESSAGECLASSID", row["MESSAGECLASSID"].ToString());
                    grdMessage.DataSource = SqlExecuter.Procedure("usp_com_selectMessage", param); // 수정 필요

                    grdMessageClass.View.FocusedRowHandle = beforeHandle;
                    grdMessageClass.View.UnselectRow(afterHandle);
                    grdMessageClass.View.SelectRow(beforeHandle);
                    focusedRowChanged();
                }
            }
        }

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            //TODO : 그리드의 유효성 검사
            grdMessage.View.CheckValidation();

            DataTable changed = grdMessage.GetChangedRows();

            if (changed.Rows.Count == 0)
            {
                throw MessageException.Create("NoSaveData");
            }
        }

        #endregion

        #region Pivate Function

        private void focusedRowChanged()
        {
            var row = grdMessageClass.View.GetDataRow(grdMessageClass.View.FocusedRowHandle);
            if (string.IsNullOrEmpty(row["MESSAGECLASSID"].ToString()))
            {
                ShowMessage("NoSelectData");
            }

            var param = Conditions.GetValues();
            param["P_MESSAGECLASSID"] = row["MESSAGECLASSID"].ToString();

            //grdMessage.DataSource = SqlExecuter.Procedure("usp_com_selectMessage", param);
            grdMessage.DataSource = SqlExecuter.Query("SelectMessage", "10001", param);
        }

        private void LoadDataMessageClass()
        {
            var values = Conditions.GetValues();
            values.Add("p_languageType", UserInfo.Current.LanguageType);

            int iBeforeRowHandle = grdMessageClass.View.FocusedRowHandle;

            //grdMessageClass.DataSource = Procedure("usp_com_selectMessageClassOnMapping", values);
            grdMessageClass.DataSource = SqlExecuter.Query("SelectMessageClassOnMapping", "10001", values);

            grdMessageClass.View.FocusedRowHandle = 0;
            grdMessageClass.View.SelectRow(0);


            if (iBeforeRowHandle == 0 && grdMessageClass.View.FocusedRowHandle == 0)
                focusedRowChanged();
        }

        #endregion
    }
}