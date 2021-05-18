#region using

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
    /// 프 로 그 램 명  : 시스템 관리 > 코드 관리 > 코드 정보
    /// 업  무  설  명  : 코드 정보를 관리한다.
    /// 생    성    자  : 강호윤
    /// 생    성    일  : 2019-04-01
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class Code : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가

        #endregion

        #region 생성자

        public Code()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 화면의 컨텐츠 영역을 초기화한다.
        /// </summary>
        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeEvent();

            // TODO : 컨트롤 초기화 로직 구성
            InitializeGridCodeClass();
            InitializeGridCode();

            LoadDataGridCodeClass();
        }

        /// <summary>        
        /// 코드그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGridCodeClass()
        {
            // TODO : 그리드 초기화 로직 추가
            grdCodeClass.GridButtonItem = GridButtonItem.Refresh;
            grdCodeClass.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;
            grdCodeClass.View.SetIsReadOnly();
            grdCodeClass.View.SetSortOrder("CODECLASSID");

            grdCodeClass.View.SetAutoFillColumn("CODECLASSNAME");

            // 상위코드그룹ID
            grdCodeClass.View.AddTextBoxColumn("PARENTCODECLASSID", 150)
                .SetIsHidden();
            // 코드그룹ID
            grdCodeClass.View.AddTextBoxColumn("CODECLASSID", 150);
            // 코드그룹명
            grdCodeClass.View.AddTextBoxColumn("CODECLASSNAME", 200);

            grdCodeClass.View.PopulateColumns();

            grdCodeClass.View.OptionsCustomization.AllowFilter = false;
            grdCodeClass.View.OptionsCustomization.AllowSort = false;
        }

        /// <summary>        
        /// 코드 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGridCode()
        {
            // TODO : 그리드 초기화 로직 추가
            grdCode.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdCode.View.SetSortOrder("DISPLAYSEQUENCE");

            // 상위코드그룹ID
            grdCode.View.AddTextBoxColumn("PARENTCODECLASSID", 150)
                .SetIsHidden()
                .SetIsReadOnly();
            // 코드그룹ID
            grdCode.View.AddTextBoxColumn("CODECLASSID", 150)
               .SetValidationKeyColumn()
               .SetValidationIsRequired()
               .SetIsReadOnly();
            // 코드ID
            grdCode.View.AddTextBoxColumn("CODEID", 150)
                .SetValidationKeyColumn()
                .SetValidationIsRequired();
            // 코드명
            grdCode.View.AddLanguageColumn("CODENAME", 200);
            // 설명
            grdCode.View.AddTextBoxColumn("DESCRIPTION", 200);
            // 상위코드ID
            //grdCode.View.AddComboBoxColumn("PARENTCODEID", 200, new SqlQuery("GetParentCodeId", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
            //    .SetRelationIds("PARENTCODECLASSID");
            grdCode.View.AddComboBoxColumn("PARENTCODEID", 200, new SqlQueryAdapter());
            // 표시순서
            grdCode.View.AddSpinEditColumn("DISPLAYSEQUENCE", 70)
               .SetDisplayFormat("#,##0", MaskTypes.Numeric, false);// 소수점 표시 안함
            // 유효상태
            grdCode.View.AddComboBoxColumn("VALIDSTATE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
               .SetDefault("Valid")
               .SetValidationIsRequired()
               .SetTextAlignment(TextAlignment.Center);
            // 생성자
            grdCode.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            // 생성일
            grdCode.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            // 수정자
            grdCode.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            // 수정일
            grdCode.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdCode.View.PopulateColumns();
        }

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            grdCodeClass.ToolbarRefresh += GrdCodeClass_ToolbarRefresh;
            grdCodeClass.View.FocusedRowChanged += View_FocusedRowChanged;

            grdCode.View.AddingNewRow += View_AddingNewRow;
            //grdCode.View.AddNewRowChangeControlEvent += View_AddNewRowChangeControlEvent;
        }

        /// <summary>
        /// 코드그룹 리스트 그리드의 새로고침 버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdCodeClass_ToolbarRefresh(object sender, EventArgs e)
        {
            try
            {
                int beforeFocusCodeClass = grdCodeClass.View.FocusedRowHandle;

                grdCodeClass.ShowWaitArea();

                LoadDataGridCodeClass();

                grdCodeClass.View.FocusedRowHandle = 0;
                grdCodeClass.View.UnselectRow(beforeFocusCodeClass);
                grdCodeClass.View.SelectRow(0);

                focusedRowChanged();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
            finally
            {
                grdCodeClass.CloseWaitArea();
            }
        }

        /// <summary>
        /// 코드그룹 리스트 그리드의 포커스 행 변경 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            focusedRowChanged();
        }

        /// <summary>
        /// 코드 리스트 그리드의 추가 버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            //  코드 클래스 컬럼에 선택된 코드클래스 ID 자동 세팅
            DataRow focusRow = grdCodeClass.View.GetFocusedDataRow();
            string parentCodeClassId = focusRow["PARENTCODECLASSID"].ToString();
            string codeClassId = focusRow["CODECLASSID"].ToString();
            args.NewRow["PARENTCODECLASSID"] = parentCodeClassId;
            args.NewRow["CODECLASSID"] = codeClassId;

            decimal maxDisplaySequence = 0;

            //기존의 표시 순서 값들 중 "" or 공백 값이 아닌 행들만 가져옴
            var sequenceRows = args.NewRow.Table.Rows
                .Cast<DataRow>()
                .Where(r => r != args.NewRow && r.ToStringNullToEmpty("DISPLAYSEQUENCE") != "");

            // 기존의 표시 순서 값들이 있으면, 표시 순서 'Max' 처리
            if (sequenceRows.Count() > 0)
            {
                maxDisplaySequence = sequenceRows.Max(r => r.Field<int>("DISPLAYSEQUENCE"));
            }

            // 공백만 있을 경우와 새로 행 추가 될 때에는 값 1로 세팅
            args.NewRow["DISPLAYSEQUENCE"] = maxDisplaySequence + 1;
        }

        /// <summary>
        /// 코드 리스트 그리드의 행이 추가되면 상위코드ID 콤보의 DataSource 변경
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddNewRowChangeControlEvent(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.BandedGrid.AddNewRowChangeControlArgs args)
        {
            if (args.FieldName != "PARENTCODEID")
                return;

            args.ChangeComboBox("PARENTCODEID", new SqlQuery("GetParentCodeId", "10001", $"CODECLASSID={grdCodeClass.View.GetRowCellValue(grdCodeClass.View.FocusedRowHandle, "PARENTCODECLASSID").ToString()}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetEmptyItem("", "");
        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            // TODO : 저장 Rule 변경
            DataTable changed = grdCode.GetChangedRows();
          
            ExecuteRule("SaveCode", changed);
        }

        #endregion

        #region 검색

        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            // TODO : 조회 SP 변경
            if (grdCodeClass.View.DataRowCount <= 0)
                return;
            
            int beforeHandle = grdCodeClass.View.FocusedRowHandle;

            DataRow row = grdCodeClass.View.GetFocusedDataRow();
            string bfCodeClassId = row["CODECLASSID"].ToString();

            var values = Conditions.GetValues();
            values.Add("p_CODECLASSID","");
            values.Add("p_LANGUAGETYPE", UserInfo.Current.LanguageType);

            //DataTable dtCodeClass = await ProcedureAsync("usp_com_selectCodeClass_search", values);
            DataTable dtCodeClass = await QueryAsync("SelectCodeClassSearch", "00001", values);

            if (dtCodeClass.Rows.Count < 1)
            {
                // 조회할 데이터가 없습니다.
                ShowMessage("NoSelectData");
                dtCodeClass.Clear();
            }
            else if (dtCodeClass.Rows.Count <= beforeHandle)
            {
                grdCodeClass.DataSource = dtCodeClass;

                grdCodeClass.View.FocusedRowHandle = 0;

                DataRow currentRow = grdCodeClass.View.GetFocusedDataRow();

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("P_CODECLASSID", currentRow["CODECLASSID"].ToString());

                //grdCode.DataSource = SqlExecuter.Procedure("usp_com_selectCode", param);
                grdCode.DataSource = SqlExecuter.Query("SelectCode", "00001", param);

                grdCodeClass.View.FocusedRowHandle = 0;
                grdCodeClass.View.SelectRow(0);

                focusedRowChanged();
            }
            else
            {
                grdCodeClass.DataSource = dtCodeClass;

                int afterHandle = grdCodeClass.View.FocusedRowHandle;

                if (beforeHandle == 0 && afterHandle == 0)
                {
                    focusedRowChanged();
                }

                if (beforeHandle > 0)
                {
                    Dictionary<string, object> param = new Dictionary<string, object>();
                    param.Add("P_CODECLASSID", row["CODECLASSID"].ToString());

                    //grdCode.DataSource = SqlExecuter.Procedure("usp_com_selectCode", param);
                    grdCode.DataSource = SqlExecuter.Query("SelectCode", "00001", param);

                    grdCodeClass.View.FocusedRowHandle = beforeHandle;
                    grdCodeClass.View.UnselectRow(afterHandle);
                    grdCodeClass.View.SelectRow(beforeHandle);

                    focusedRowChanged();
                }
            }
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
        }

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            // TODO : 유효성 로직 변경
            grdCode.View.CheckValidation();

            DataTable changed = grdCode.GetChangedRows();

            if (changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
            
            // DisplaySequence Check - Screen
            //DataTable dtCode = grdCode.DataSource as DataTable;

            //var values = dtCode.Rows.Cast<DataRow>()
            //    .Where(r => r.ToStringNullToEmpty("DISPLAYSEQUENCE") != "")
            //    .Select(r => r["DISPLAYSEQUENCE"])
            //    .Distinct().ToList();

            //if (values.Count() != changed.Rows.Count)
            //{
            //    throw MessageException.Create("DuplicatedDisplaySequence");
            //}


            // DisplaySequence Check - db
            DataRow focusedCodeClassRow = grdCodeClass.View.GetFocusedDataRow();
            string codeClassId = focusedCodeClassRow["CODECLASSID"].ToString();
            string strModifiedOrDeletedCodeId = "";
            string strAddedOrModifiedDispalySequence = "";

            foreach (DataRow row in changed.Rows)
            {
                string state = row["_STATE_"].ToString();

                if (state == "added")
                {
                    strAddedOrModifiedDispalySequence += row["DISPLAYSEQUENCE"].ToString() + ";";
                }
                else if (state == "modified")
                {
                    strAddedOrModifiedDispalySequence += row["DISPLAYSEQUENCE"].ToString() + ";";
                    strModifiedOrDeletedCodeId += row["CODEID"].ToString() + ";";
                }

                else if (state == "deleted")
                {
                    //strAddedOrModifiedDispalySequence += row["DISPLAYSEQUENCE"].ToString() + ";";
                    strModifiedOrDeletedCodeId += row["CODEID"].ToString() + ";";
                }
            }

            if (strModifiedOrDeletedCodeId != "")
            {
                strModifiedOrDeletedCodeId = strModifiedOrDeletedCodeId.Remove(strModifiedOrDeletedCodeId.Length - 1, 1);
            }

            if (strAddedOrModifiedDispalySequence != "")
            {
                strAddedOrModifiedDispalySequence = strAddedOrModifiedDispalySequence.Remove(strAddedOrModifiedDispalySequence.Length - 1, 1);
            }
            
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("p_CODECLASSID", codeClassId);
            param.Add("p_MODIFIEDORDELETEDCODEID", strModifiedOrDeletedCodeId);
            param.Add("p_ADDEDORMODIFIEDDISPLAYSEQUENCE", strAddedOrModifiedDispalySequence);

            //DataTable dtDuplicated = SqlExecuter.Procedure("usp_com_selectDuplicatedDisplaySequence", param);
            DataTable dtDuplicated = SqlExecuter.Query("GetDuplicatedDisplaySequence", "10001", param);

            if (dtDuplicated.Rows.Count > 0)
            {
                foreach (DataRow row in dtDuplicated.Rows)
                {
                    throw MessageException.Create("DuplicatedDisplaySequence", row["CODEID"].ToString());
                }
            }
        }

        #endregion

        #region Private Function

        // TODO : 화면에서 사용할 내부 함수 추가
        /// <summary>
        /// 코드그룹 리스트 그리드의 데이터를 조회한다.
        /// </summary>
        private void LoadDataGridCodeClass()
        {
            var values = Conditions.GetValues();
            values.Add("p_CODECLASSID", "");
            values.Add("p_LANGUAGETYPE", UserInfo.Current.LanguageType);

            //grdCodeClass.DataSource = SqlExecuter.Procedure("usp_com_selectCodeClass_search", values);
            grdCodeClass.DataSource = SqlExecuter.Query("SelectCodeClassSearch", "00001", values);
        }

        /// <summary>
        /// 코드그룹 리스트 그리드의 포커스 행 변경 로직을 처리한다.
        /// </summary>
        private void focusedRowChanged()
        {
            var row = grdCodeClass.View.GetDataRow(grdCodeClass.View.FocusedRowHandle);

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("P_CODECLASSID", row["CODECLASSID"].ToString());

            if (string.IsNullOrEmpty(row["CODECLASSID"].ToString()))
            {
                ShowMessage("NoSelectData");
                grdCode.View.ClearDatas();

                return;
            }

            //grdCode.DataSource = SqlExecuter.Procedure("usp_com_selectCode", param);
            grdCode.DataSource = SqlExecuter.Query("SelectCode", "00001", param);


            string parentCodeClassId = Format.GetString(row["PARENTCODECLASSID"]);

            grdCode.View.RefreshComboBoxDataSource("PARENTCODEID", new SqlQuery("GetCodeList", "00001", $"CODECLASSID={parentCodeClassId}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
        }

        #endregion
    }
}