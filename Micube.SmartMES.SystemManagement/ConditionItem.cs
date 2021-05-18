#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons.Controls;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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
    /// 프 로 그 램 명  : 시스템 관리 > 조건 관리 > 조회조건 정보
    /// 업  무  설  명  : 조회조건 정보를 관리한다.
    /// 생    성    자  : 강호윤
    /// 생    성    일  : 2019-04-01
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ConditionItem : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가

        #endregion

        #region 생성자

        public ConditionItem()
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
            InitializeGrid();
        }

        /// <summary>
        /// 조회조건 항목 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdConditionItem.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdConditionItem.View.SetSortOrder("CONDITIONITEMID");

            // 서비스
            grdConditionItem.View.AddTextBoxColumn("UIID", 80)
                .SetValidationKeyColumn()
                .SetIsHidden()
                .SetIsReadOnly()
                .SetDefault(UserInfo.Current.Uiid);
            // 조회조건항목ID
            grdConditionItem.View.AddTextBoxColumn("CONDITIONITEMID", 150)
                .SetValidationKeyColumn();
            // 조회조건항목명
            grdConditionItem.View.AddLanguageColumn("CONDITIONITEMNAME", 200);
            // 설명
            grdConditionItem.View.AddTextBoxColumn("DESCRIPTION", 200);
            // 컬럼명
            grdConditionItem.View.AddTextBoxColumn("COLUMNNAME", 120)
                .SetValidationIsRequired();
            // 항목타입
            grdConditionItem.View.AddComboBoxColumn("ITEMTYPE", 150, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ConditionItemType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                .SetEmptyItem("", "")
                .SetDefault("")
                .SetValidationIsRequired();
            // 문자열형식
            grdConditionItem.View.AddTextBoxColumn("DATAFORMAT", 130);
            // 기본값
            grdConditionItem.View.AddTextBoxColumn("DEFAULTVALUE", 150);
            // 상수데이터
            grdConditionItem.View.AddSelectPopupColumn("CONSTANTDATA", 200, new ConditionInput_Popup())
                .SetPopupCustomParameter((popup, dataRow) =>
                {
                    (popup as ConditionInput_Popup)._Type = "CONSTANTDATA";
                }, (popup, dataRow) =>
                {
                    dataRow["CONSTANTDATA"] = (popup as ConditionInput_Popup)._Result;
                });
            // 필수여부
            grdConditionItem.View.AddComboBoxColumn("ISREQUIRED", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                .SetTextAlignment(TextAlignment.Center)
                .SetDefault("N");
            // 숨김여부
            grdConditionItem.View.AddComboBoxColumn("ISHIDDEN", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                .SetTextAlignment(TextAlignment.Center)
                .SetDefault("N");
            // 전체여부
            grdConditionItem.View.AddComboBoxColumn("ISALL", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                .SetTextAlignment(TextAlignment.Center)
                .SetDefault("N");
            // 실행타입
            grdConditionItem.View.AddComboBoxColumn("EXECUTETYPE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ExecuteType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                .SetTextAlignment(TextAlignment.Center)
                .SetEmptyItem("", "")
                .SetDefault("");
            // 실행ID
            grdConditionItem.View.AddTextBoxColumn("EXECUTEID", 120);
            // 실행버전
            grdConditionItem.View.AddTextBoxColumn("EXECUTEVERSION", 60);
            // 실행파라미터
            grdConditionItem.View.AddSelectPopupColumn("EXECUTEPARAMETER", 200, new ConditionInput_Popup())
                .SetPopupCustomParameter((popup, dataRow) =>
                {
                    (popup as ConditionInput_Popup)._Type = "EXECUTEPARAMETER";
                }, (popup, dataRow) =>
                {
                    dataRow["EXECUTEPARAMETER"] = (popup as ConditionInput_Popup)._Result;
                });
            // Display Member
            grdConditionItem.View.AddTextBoxColumn("DISPLAYMEMBER", 120);
            // Value Member
            grdConditionItem.View.AddTextBoxColumn("VALUEMEMBER", 120);
            // 유효상태
            grdConditionItem.View.AddComboBoxColumn("VALIDSTATE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                .SetTextAlignment(TextAlignment.Center)
                .SetDefault("Valid")
                .SetValidationIsRequired();
            // 생성자
            grdConditionItem.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly();
            // 생성일
            grdConditionItem.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetTextAlignment(TextAlignment.Center)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly();
            // 수정자
            grdConditionItem.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly();
            // 수정일
            grdConditionItem.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetTextAlignment(TextAlignment.Center)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly();

            grdConditionItem.View.PopulateColumns();
        }

        #endregion

        #region Event

        /// <summary>        
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        private void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
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
            DataTable changed = grdConditionItem.GetChangedRows();

            foreach (DataRow row in changed.Rows)
            {
                // 상수데이터에 등록된 값이 있는 경우 Json 문자열로 Serialize
                if (!string.IsNullOrEmpty(row["CONSTANTDATA"].ToString()))
                {
                    List<Dictionary<string, object>> dicConstantData = new List<Dictionary<string, object>>();

                    string[] strConstantDataList = row["CONSTANTDATA"].ToString().Split(';');
                    for (int i = 0; i < strConstantDataList.Length; i++)
                    {
                        if (string.IsNullOrEmpty(strConstantDataList[i]))
                            break;

                        string[] strConstantDataSplit = strConstantDataList[i].Split('=');

                        dicConstantData.Add(new Dictionary<string, object>
                        {
                            { row["VALUEMEMBER"].ToString(), strConstantDataSplit[0].Trim() },
                            { row["DISPLAYMEMBER"].ToString(), strConstantDataSplit[1].Trim() }
                        });
                    }

                    string strConstantData = JsonConvert.SerializeObject(dicConstantData, new JsonSerializerSettings { ContractResolver = new CamelCaseExceptDictionaryResolver() });

                    row["CONSTANTDATA"] = strConstantData;
                }

                // 실행파라미터에 등록된 값이 있는 경우 Json 문자열로 Serialize
                if (!string.IsNullOrEmpty(row["EXECUTEPARAMETER"].ToString()))
                {
                    Dictionary<string, object> dicExecuteParameter = new Dictionary<string, object>();

                    string[] strExecuteParameterList = row["EXECUTEPARAMETER"].ToString().Split(';');
                    for (int i = 0; i < strExecuteParameterList.Length; i++)
                    {
                        if (string.IsNullOrEmpty(strExecuteParameterList[i]))
                            break;

                        string[] strSplit = strExecuteParameterList[i].Split('=');
                        dicExecuteParameter.Add(strSplit[0].Trim(), strSplit[1].Trim());
                    }

                    string strExecuteParameter = JsonConvert.SerializeObject(dicExecuteParameter, new JsonSerializerSettings { ContractResolver = new CamelCaseExceptDictionaryResolver() });

                    row["EXECUTEPARAMETER"] = strExecuteParameter;
                }
            }

            ExecuteRule("SaveConditionItem", changed);
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
            var values = Conditions.GetValues();

            //DataTable dtResult = await SqlExecuter.ProcedureAsync("usp_com_selectConditionItem", values);
            DataTable dtResult = await QueryAsync("SelectConditionItem", "00001", values);

            if (dtResult.Rows.Count < 1)
            {
                // 조회할 데이터가 없습니다.
                ShowMessage("NoSelectData");
            }

            foreach (DataRow row in dtResult.Rows)
            {
                // 상수데이터 필드에 값이 있는 경우 Json 문자열 Deserialize
                if (!string.IsNullOrEmpty(row["CONSTANTDATA"].ToString()))
                {
                    var constantData = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(row["CONSTANTDATA"].ToString());

                    string strConstantData = "";
                    constantData.ForEach(e => strConstantData += e[row["VALUEMEMBER"].ToString()] + "=" + e[row["DISPLAYMEMBER"].ToString()] + ";");
                    row["CONSTANTDATA"] = strConstantData.Substring(0, strConstantData.Length - 1);
                }

                // 실행파라미터 필드에 값이 있는 경우 Json 문자열 Deserialize
                if (!string.IsNullOrEmpty(row["EXECUTEPARAMETER"].ToString()))
                {
                    var executeParameter = JsonConvert.DeserializeObject<Dictionary<string, object>>(row["EXECUTEPARAMETER"].ToString());

                    string strExecuteParameter = "";

                    List<string> listExecuteParameter = new List<string>(executeParameter.Keys);
                    for (int i = 0; i < listExecuteParameter.Count; i++)
                    {
                        strExecuteParameter += listExecuteParameter[i] + "=" + executeParameter[listExecuteParameter[i].ToString()] + ";";
                    }
                    row["EXECUTEPARAMETER"] = strExecuteParameter.Substring(0, strExecuteParameter.Length - 1);
                }
            }

            grdConditionItem.DataSource = dtResult;
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
            grdConditionItem.View.CheckValidation();

            DataTable changed = grdConditionItem.GetChangedRows();


            if (changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
            
            foreach (DataRow row in changed.Rows)
            {
                if (string.IsNullOrEmpty(row["CONDITIONITEMID"].ToString()))
                    // 조회조건 항목 ID는 필수 입력 항목입니다.
                    throw MessageException.Create("RequireColumn", Language.Get("CONDITIONITEMID"));

                // 항목타입이 Combo 인 경우 Validation Check
                if (!string.IsNullOrEmpty(row["ITEMTYPE"].ToString()) && !(row.Field<string>("ITEMTYPE").IndexOf("Combo") < 0))
                {
                    if (!string.IsNullOrEmpty(row["CONSTANTDATA"].ToString()) && !string.IsNullOrEmpty(row["EXECUTETYPE"].ToString()))
                        // Message : 상수데이터와 실행타입은 동시에 등록 할 수 없습니다.
                        throw MessageException.Create("00001", Language.Get("CONSTANTDATA"), Language.Get("EXECUTETYPE"));

                    if (string.IsNullOrEmpty(row["CONSTANTDATA"].ToString()) && string.IsNullOrEmpty(row["EXECUTETYPE"].ToString()))
                        // Message : 항목타입이 Combo 인 경우 상수데이터 또는 실행타입을 등록하여야 합니다.
                        throw MessageException.Create("00006", Language.Get("ITEMTYPE"),
                                                               grdConditionItem.View.Columns["ITEMTYPE"].ColumnEdit.GetDisplayText(row["ITEMTYPE"]),
                                                               Language.Get("CONSTANTDATA"),
                                                               Language.Get("EXECUTETYPE"));

                    if (row.Field<string>("EXECUTETYPE") == "Query" && string.IsNullOrEmpty(row["EXECUTEID"].ToString()) && string.IsNullOrEmpty(row["EXECUTEVERSION"].ToString()))
                        // Message : 실행타입이 Query인 경우 실행ID와 실행버전은 필수로 입력하여야 합니다.
                        throw MessageException.Create("00003", Language.Get("EXECUTETYPE"),
                                                               grdConditionItem.View.Columns["EXECUTETYPE"].ColumnEdit.GetDisplayText(row["EXECUTETYPE"]),
                                                               Language.Get("EXECUTEID"),
                                                               Language.Get("EXECUTETYPE"));

                    if (string.IsNullOrEmpty(row["DISPLAYMEMBER"].ToString()) && string.IsNullOrEmpty(row["VALUEMEMBER"].ToString()))
                        // Message : 항목타입이 Combo 인 경우 ValueMember와 DisplayMember는 필수로 입력하여야 합니다.
                        throw MessageException.Create("00002", Language.Get("ITEMTYPE"),
                                                               grdConditionItem.View.Columns["ITEMTYPE"].ColumnEdit.GetDisplayText(row["ITEMTYPE"]),
                                                               Language.Get("VALUEMEMBER"),
                                                               Language.Get("DISPLAYMEMBER"));
                }

                // 항목타입이 Tree 인 경우 Validation Check
                if (!string.IsNullOrEmpty(row["ITEMTYPE"].ToString()) && !(row.Field<string>("ITEMTYPE").IndexOf("Tree") < 0))
                {
                    if (string.IsNullOrEmpty(row["CONSTANTDATA"].ToString()) && string.IsNullOrEmpty(row["EXECUTETYPE"].ToString()))
                        // Message : 항목타입이 Tree 인 경우 실행타입을 등록하여야 합니다.
                        throw MessageException.Create("00007", Language.Get("ITEMTYPE"),
                                                               grdConditionItem.View.Columns["ITEMTYPE"].ColumnEdit.GetDisplayText(row["ITEMTYPE"]),
                                                               Language.Get("EXECUTETYPE"));

                    if (row.Field<string>("EXECUTETYPE") == "Query" && string.IsNullOrEmpty(row["EXECUTEID"].ToString()) && string.IsNullOrEmpty(row["EXECUTEVERSION"].ToString()))
                        // Message : 실행타입이 Query인 경우 실행ID와 실행버전은 필수로 입력하여야 합니다.
                        throw MessageException.Create("00003", Language.Get("EXECUTETYPE"),
                                                               grdConditionItem.View.Columns["EXECUTETYPE"].ColumnEdit.GetDisplayText(row["EXECUTETYPE"]),
                                                               Language.Get("EXECUTEID"),
                                                               Language.Get("EXECUTETYPE"));

                    if (string.IsNullOrEmpty(row["DISPLAYMEMBER"].ToString()) && string.IsNullOrEmpty(row["VALUEMEMBER"].ToString()))
                        // Message : 항목타입이 Tree 인 경우 ValueMember와 DisplayMember는 필수로 입력하여야 합니다.
                        throw MessageException.Create("00002", Language.Get("ITEMTYPE"),
                                                               grdConditionItem.View.Columns["ITEMTYPE"].ColumnEdit.GetDisplayText(row["ITEMTYPE"]),
                                                               Language.Get("VALUEMEMBER"),
                                                               Language.Get("DISPLAYMEMBER"));
                }

                // 항목타입이 DateRange 인 경우 Validation Check
                if (!string.IsNullOrEmpty(row["ITEMTYPE"].ToString()) && !(row.Field<string>("ITEMTYPE").IndexOf("DateRange") < 0))
                {
                    if (!string.IsNullOrEmpty(row["DEFAULTVALUE"].ToString()) && row["DEFAULTVALUE"].ToString().Split(',').Length != 2)
                        // Message : 항목타입이 DateRange 인 경우 기본값은 "From Date,To Date" 형식으로 입력하여야 합니다.
                        throw MessageException.Create("00008", Language.Get("ITEMTYPE"),
                                                               row["ITEMTYPE"].ToString(),
                                                               Language.Get("DEFAULTVALUE"));
                }
            }
        }

        #endregion

        #region Private Function

        // TODO : 화면에서 사용할 내부 함수 추가

        #endregion
    }

    /// <summary>
    /// Keep Casing When Json Serializing Dictionary
    /// </summary>
    class CamelCaseExceptDictionaryResolver : CamelCasePropertyNamesContractResolver
    {
        #region Overrides of DefaultContractResolver

        protected override string ResolveDictionaryKey(string dictionaryKey)
        {
            return dictionaryKey;
        }

        #endregion
    }
}