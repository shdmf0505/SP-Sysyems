#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Grid;
using Micube.Framework.SmartControls.Grid.BandedGrid;
using Micube.SmartMES.Commons.Controls;
using Newtonsoft.Json;

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
    /// 프 로 그 램 명  : 시스템 관리 > 메뉴 관리 > 툴바 정보
    /// 업  무  설  명  : 툴바 정보를 관리한다.
    /// 생    성    자  : 강호윤
    /// 생    성    일  : 2019-04-01
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class Toolbar : SmartConditionManualBaseForm
    {
        #region Local Variables

        #endregion

        #region 생성자

        public Toolbar()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화
        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeEvent();
        }

        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGridToolbar()
        {
            grdToolbar.GridButtonItem -= GridButtonItem.Copy;

            grdToolbar.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdToolbar.View.SetSortOrder("DISPLAYSEQUENCE");

            grdToolbar.View.AddTextBoxColumn("UIID", 80)
                .SetValidationKeyColumn()
                .SetIsHidden()
                .SetIsReadOnly()
                .SetDefault(UserInfo.Current.Uiid);

            grdToolbar.View.AddTextBoxColumn("TOOLBARID", 150)
                .SetValidationKeyColumn()
                .SetValidationIsRequired();

            grdToolbar.View.AddLanguageColumn("TOOLBARNAME", 200);
            //grdToolbar.View.AddTextBoxColumn("KTOOLBARNAME", 200);
            //grdToolbar.View.AddTextBoxColumn("ETOOLBARNAME", 200);
            //grdToolbar.View.AddTextBoxColumn("CTOOLBARNAME", 200);
            //grdToolbar.View.AddTextBoxColumn("LTOOLBARNAME", 200);

            grdToolbar.View.AddTextBoxColumn("DESCRIPTION", 200);
            grdToolbar.View.AddComboBoxColumn("TOOLBARTYPE", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ToolbarType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                 .SetDefault("ImageButton")
                 .SetValidationIsRequired()
                 .SetTextAlignment(TextAlignment.Center);
            grdToolbar.View.AddMemoEditColumn("OPTIONS", 100);
            //grdToolbar.View.AddSelectPopupColumn("OPTIONS", 120, new OptionsInput_Popup())
            //                .SetPopupCustomParameter((popup, dataRow) =>
            //                {
            //                    (popup as OptionsInput_Popup)._Type = "CONSTANTDATA";
            //                }, (popup, dataRow) =>
            //                {
            //                    dataRow["CONSTANTDATA"] = (popup as OptionsInput_Popup)._Result;
            //                });


            //smjang - 쓰기 권한 컬럼 추가
            grdToolbar.View.AddComboBoxColumn("ISWRITE", 70, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetTextAlignment(TextAlignment.Center)
                .SetDefault("Y");
            //.SetIsReadOnly(false)
            //.SetDefault(false);



            grdToolbar.View.AddSpinEditColumn("DISPLAYSEQUENCE", 70)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric, false)
              .SetValueRange(1, int.MaxValue)
              .SetDefault("1");

            //grdToolbar.View.AddSpinEditColumn("DISPLAYSEQUENCE", 100)
            //    .SetValueRange(1, decimal.MaxValue)
            //    .SetDefault(1)
            //    .SetDisplayFormat();

            grdToolbar.View.AddLanguageColumn("MESSAGENAME", 200);

            grdToolbar.View.AddComboBoxColumn("VALIDSTATE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Valid")
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Center);

            grdToolbar.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdToolbar.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdToolbar.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdToolbar.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetIsReadOnly()
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetTextAlignment(TextAlignment.Center);

            grdToolbar.View.PopulateColumns();
        }

        //private void LoadDataGridToolbar()
        //{
        //    Dictionary<string, object> values = Conditions.GetValues();
        //    grdToolbar.DataSource = SqlExecuter.Procedure("usp_com_selectToolbar", values);
        //    if (grdToolbar.View.DataRowCount > 0)
        //        grdToolbar.View.SelectRow(0);
        //}

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        private void InitializeEvent()
        {
            Load += Toolbar_Load;
            grdToolbar.View.AddingNewRow += View_AddingNewRow;
        }

        private void Toolbar_Load(object sender, EventArgs e)
        {
            InitializeGridToolbar();
        }

        private void View_AddingNewRow(SmartBandedGridView sender, AddNewRowArgs args)
        {
            args.NewRow["DISPLAYSEQUENCE"] = args.NewRow.Table.Rows.Count == 0 ? 1 :
                  args.NewRow.Table.Rows.Cast<DataRow>()
                      .Where(r => r != args.NewRow)
                      .Max(r => decimal.Parse(r["DISPLAYSEQUENCE"].ToString())) + 1;
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

            DataTable changed = grdToolbar.GetChangedRows();

            foreach (DataRow row in changed.Rows)
            {
                if (!string.IsNullOrEmpty(row["OPTIONS"].ToString()))
                {
                    Dictionary<string, object> dicOptions = new Dictionary<string, object>();

                    string[] strOptionsList = row["OPTIONS"].ToString().Split(';');
                    for (int i = 0; i < strOptionsList.Length; i++)
                    {
                        if (string.IsNullOrEmpty(strOptionsList[i]))
                            break;

                        string[] strOptionsSplit = strOptionsList[i].Split('=');

                        if (strOptionsSplit.Length == 2)
                            dicOptions.Add(strOptionsSplit[0].Trim(), strOptionsSplit[1].Trim());
                    }

                    string stOptionsData = JsonConvert.SerializeObject(dicOptions, new JsonSerializerSettings { ContractResolver = new CamelCaseExceptDictionaryResolver() });

                    row["OPTIONS"] = stOptionsData;
                }
            }

            ExecuteRule("SaveToolbar", changed);
        }

        #endregion

        #region 검색

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            var values = Conditions.GetValues();
            await base.OnSearchAsync();

            values.Add("P_LANGUAGETYPE", UserInfo.Current.LanguageType);
            //DataTable dtToolbar = await SqlExecuter.ProcedureAsync("usp_com_selectToolbar", values);
            //smjang - 쿼리 추가 (iswrite 컬럼 조회 추가)
            //DataTable dtToolbar = await QueryAsync("SelectToolbar", "10001", values);
            DataTable dtToolbar = await QueryAsync("SelectToolbar", "10002", values);

            if (dtToolbar.Rows.Count < 1)
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            foreach (DataRow row in dtToolbar.Rows)
            {
                if (!string.IsNullOrEmpty(row["OPTIONS"].ToString()))
                {
                    var options = JsonConvert.DeserializeObject<Dictionary<string, object>>(row["OPTIONS"].ToString());

                    string strOptions = "";

                    List<string> listOptions = new List<string>(options.Keys);
                    for (int i = 0; i < listOptions.Count; i++)
                    {
                        strOptions += listOptions[i] + "=" + options[listOptions[i].ToString()] + ";";
                    }

                    if (strOptions.Length > 0)
                        row["OPTIONS"] = strOptions.Substring(0, strOptions.Length - 1);
                }
            }

            grdToolbar.DataSource = dtToolbar;
        }

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();
            grdToolbar.View.CheckValidation();

            DataTable changed = grdToolbar.GetChangedRows();

            if (changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }

            foreach (DataRow row in changed.Rows)
            {
                if (string.IsNullOrEmpty(row["TOOLBARID"].ToString()))
                {
                    // 툴바 ID는 필수 입력 항목입니다.
                    throw MessageException.Create("RequireColumn", Language.Get("TOOLBARID"));
                }
                //else if (string.IsNullOrEmpty(row["OPTIONS"].ToString()))
                //{
                //    // 옵션은 필수 입력 항목입니다.
                //    throw MessageException.Create("RequireColumn", Language.Get("OPTIONS"));
                //}
            }

            //DisplaySequence Check -db

            string strModifiedOrDeletedToolbarId = "";
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
                    strModifiedOrDeletedToolbarId += row["TOOLBARID"].ToString() + ";";
                }

                else if (state == "deleted")
                {
                    strModifiedOrDeletedToolbarId += row["TOOLBARID"].ToString() + ";";
                }
            }

            if (strModifiedOrDeletedToolbarId != "")
            {
                strModifiedOrDeletedToolbarId = strModifiedOrDeletedToolbarId.Remove(strModifiedOrDeletedToolbarId.Length - 1, 1);
            }

            if (strAddedOrModifiedDispalySequence != "")
            {
                strAddedOrModifiedDispalySequence = strAddedOrModifiedDispalySequence.Remove(strAddedOrModifiedDispalySequence.Length - 1, 1);
            }

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("p_MODIFIEDORDELETEDTOOLBARID", strModifiedOrDeletedToolbarId);
            param.Add("p_ADDEDORMODIFIEDDISPLAYSEQUENCE", strAddedOrModifiedDispalySequence);

            //DataTable dtDuplicated = SqlExecuter.Procedure("usp_com_selectDuplicatedDisplaySequenceToolbar", param) as DataTable;
            DataTable dtDuplicated = SqlExecuter.Query("GetDuplicatedToolbarDisplaySequence", "10001", param);

            if (dtDuplicated.Rows.Count > 0)
            {
                foreach (DataRow row in dtDuplicated.Rows)
                {
                    throw MessageException.Create("DuplicatedDisplaySequence");
                }
            }
        }

        #endregion

        #region Private Function

        #endregion
    }
}