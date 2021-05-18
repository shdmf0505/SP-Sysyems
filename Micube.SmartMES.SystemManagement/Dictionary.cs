#region using

using Micube.Framework.Net;
using Micube.Framework;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Grid;
using Micube.Framework.SmartControls.Grid.BandedGrid;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Base;
using Micube.SmartMES.Commons.Controls;

#endregion

namespace Micube.SmartMES.SystemManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 시스템 관리 > 다국어 관리 > 사전 정보
    /// 업  무  설  명  : 사전 정보를 관리한다.
    /// 생    성    자  : 강호윤
    /// 생    성    일  : 2019-04-01
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class Dictionary : SmartConditionManualBaseForm
    {
        #region Local Variables

        #endregion

        #region 생성자

        public Dictionary()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeEvent();

            InitializeDictionaryClass();
            InitializeDictionary();
            LoadDataDictionaryClass();
        }

        /// <summary>
        /// 사전그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeDictionaryClass()
        {
            grdDictionaryClass.GridButtonItem -= GridButtonItem.All;

            grdDictionaryClass.View.SetIsReadOnly();
            grdDictionaryClass.View.SetAutoFillColumn("DICTIONARYCLASSNAME");
            grdDictionaryClass.View.SetSortOrder("DICTIONARYCLASSID");

            grdDictionaryClass.View.AddTextBoxColumn("DICTIONARYCLASSID", 150);
            grdDictionaryClass.View.AddTextBoxColumn("DICTIONARYCLASSNAME", 200);
            grdDictionaryClass.View.PopulateColumns();

            grdDictionaryClass.GridButtonItem = GridButtonItem.Refresh;
        }

        /// <summary>
        /// 사전 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeDictionary()
        {
            grdDictionary.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdDictionary.View.AddingNewRow += View_AddingNewRow;

            grdDictionary.View.SetSortOrder("DICTIONARYID");

            grdDictionary.View.AddTextBoxColumn("DICTIONARYCLASSID", 150)
                .SetValidationIsRequired()
                .SetIsReadOnly();
            grdDictionary.View.AddTextBoxColumn("DICTIONARYID", 150)
                .SetValidationIsRequired();
            grdDictionary.View.AddLanguageColumn("DICTIONARYNAME", 200);
            grdDictionary.View.AddTextBoxColumn("DESCRIPTION", 200);
            grdDictionary.View.AddComboBoxColumn("VALIDSTATE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetTextAlignment(TextAlignment.Center)
                .SetDefault("Valid")
                .SetValidationIsRequired();
            grdDictionary.View.AddTextBoxColumn("CREATOR", 80)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();
            grdDictionary.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly()
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss");
            grdDictionary.View.AddTextBoxColumn("MODIFIER", 80)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();
            grdDictionary.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly()
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss");

            grdDictionary.View.SetKeyColumn("DICTIONARYID");
            grdDictionary.View.PopulateColumns();
        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        private void InitializeEvent()
        {
            grdDictionaryClass.View.FocusedRowChanged += View_FocusedRowChanged;
            grdDictionaryClass.ToolbarRefresh += GrdDictionaryClass_ToolbarRefresh;
        }

        /// <summary>
        /// 사전그룹 리스트 그리드 Refresh 버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdDictionaryClass_ToolbarRefresh(object sender, EventArgs e)
        {
            try
            {
                grdDictionaryClass.ShowWaitArea();

                LoadDataDictionaryClass();
                focusedRowChanged();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
            finally
            {
                grdDictionaryClass.CloseWaitArea();
            }
        }

        /// <summary>
        /// 사전그룹 리스트 그리드 Focused Row 변경 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            focusedRowChanged();
        }

        /// <summary>
        /// 사전 리스트 그리드 행 추가 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRow(SmartBandedGridView sender, AddNewRowArgs args)
        {
            DataRow dictionaryClass = grdDictionaryClass.View.GetFocusedDataRow();
            args.NewRow["DICTIONARYCLASSID"] = dictionaryClass["DICTIONARYCLASSID"];
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

            DataTable changed = grdDictionary.GetChangedRows();//변경된 row

            ExecuteRule("SaveDictionary", changed);
        }

        #endregion

        #region 검색

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        /// <returns></returns>
        /// 
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            if (grdDictionaryClass.View.DataRowCount <= 0)
                return;

            int beforeHandle = grdDictionaryClass.View.FocusedRowHandle;

            DataRow row = grdDictionaryClass.View.GetFocusedDataRow();
            string bfDictionaryClassId = row["DICTIONARYCLASSID"].ToString();

            var values = Conditions.GetValues();
            values.Add("p_LANGUAGETYPE", UserInfo.Current.LanguageType);

            //DataTable dtDictionaryClass = await ProcedureAsync("usp_com_selectDictionaryClassOnMapping", values);
            DataTable dtDictionaryClass = await QueryAsync("SelectDictionaryClassOnMapping", "10001", values);

            if (dtDictionaryClass.Rows.Count < 1)
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                dtDictionaryClass.Clear();
            }
            else if (dtDictionaryClass.Rows.Count <= beforeHandle)
            {
                grdDictionaryClass.DataSource = dtDictionaryClass;

                grdDictionaryClass.View.FocusedRowHandle = 0;

                DataRow currentRow = grdDictionaryClass.View.GetFocusedDataRow();

                grdDictionary.View.FocusedRowHandle = 0;

                var param = Conditions.GetValues();
                param.Add("P_DICTIONARYCLASSID", currentRow["DICTIONARYCLASSID"].ToString());
                //grdDictionary.DataSource = SqlExecuter.Procedure("usp_com_selectDictionary", param); // 수정 필요
                grdDictionary.DataSource = SqlExecuter.Query("SelectDictionary", "10001", param);

                grdDictionaryClass.View.FocusedRowHandle = 0;
                grdDictionaryClass.View.SelectRow(0);
                focusedRowChanged();
            }
            else
            {
                grdDictionaryClass.DataSource = dtDictionaryClass;

                int afterHandle = grdDictionaryClass.View.FocusedRowHandle;

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
                    var param = Conditions.GetValues();
                    param.Add("P_DICTIONARYCLASSID", row["DICTIONARYCLASSID"].ToString());
                    //grdDictionary.DataSource = SqlExecuter.Procedure("usp_com_selectDictionary", param); // 수정 필요
                    grdDictionary.DataSource = SqlExecuter.Query("SelectDictionary", "10001", param);

                    if (beforeHandle < dtDictionaryClass.Rows.Count)
                        grdDictionaryClass.View.FocusedRowHandle = beforeHandle;
                    else
                        grdDictionaryClass.View.FocusedRowHandle = 0; 
                    grdDictionaryClass.View.UnselectRow(afterHandle);
                    grdDictionaryClass.View.SelectRow(beforeHandle);
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
            grdDictionary.View.CheckValidation();

            DataTable changed = grdDictionary.GetChangedRows();//변경된 row

            if (changed.Rows.Count == 0)
            {
                throw MessageException.Create("NoSaveData");
            }
        }

        #endregion

        #region Private Function

        /// <summary>
        /// 사전그룹 리스트를 조회한다.
        /// </summary>
        private void LoadDataDictionaryClass()
        {
            var values = Conditions.GetValues();
            values.Add("p_languageType", UserInfo.Current.LanguageType);

            int iBeforeRowHandle = grdDictionaryClass.View.FocusedRowHandle;

            //grdDictionaryClass.DataSource = Procedure("usp_com_selectDictionaryClassOnMapping", values);
            grdDictionaryClass.DataSource = SqlExecuter.Query("SelectDictionaryClassOnMapping", "10001", values);

            grdDictionaryClass.View.FocusedRowHandle = 0;
            grdDictionaryClass.View.SelectRow(0);


            if (iBeforeRowHandle == 0 && grdDictionaryClass.View.FocusedRowHandle == 0)
                focusedRowChanged();
        }

        /// <summary>
        /// 사전그룹 리스트의 Focused Row 변경 시 사전 정보를 조회한다.
        /// </summary>
        private void focusedRowChanged()
        {
            var row = grdDictionaryClass.View.GetDataRow(grdDictionaryClass.View.FocusedRowHandle);
            var cond = Conditions.GetValues();
            cond.Add("P_DICTIONARYCLASSID", row["DICTIONARYCLASSID"].ToString());

            if (string.IsNullOrEmpty(row["DICTIONARYCLASSID"].ToString()))
            {
                ShowMessage("NoSelectData");
            }
            //grdDictionary.DataSource = SqlExecuter.Procedure("usp_com_selectDictionary", cond);
            grdDictionary.DataSource = SqlExecuter.Query("SelectDictionary", "10001", cond);
        }

        #endregion
    }
}