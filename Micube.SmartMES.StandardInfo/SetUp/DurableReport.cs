#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

#endregion using

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프 로 그 램 명  : 기준정보 > Setup > 치공구 조회
    /// 업  무  설  명  :
    /// 생    성    자  : 조혜인
    /// 생    성    일  : 2019-12-11
    /// 수  정  이  력  :
    ///     2021.01.19 전우성 :
    ///         1. 조회조건 치공구ID Popup 품목코드, rev 추가
    ///         2. Grid에 layer1, layer2 주석
    ///
    /// </summary>
    public partial class DurableReport : SmartConditionManualBaseForm
    {
        #region 생성자

        public DurableReport()
        {
            InitializeComponent();
        }

        #endregion 생성자

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 화면의 컨텐츠 영역을 초기화한다.
        /// </summary>
        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeGrid();
        }

        /// <summary>
        /// 코드그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdDurable.GridButtonItem = GridButtonItem.Export;
            //grdDurable.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;

            // 품목 ID
            grdDurable.View.AddTextBoxColumn("PRODUCTDEFID", 120).SetLabel("ITEMCODE");
            // 품목 version
            grdDurable.View.AddTextBoxColumn("PRODUCTDEFVERSION", 50).SetTextAlignment(TextAlignment.Center).SetLabel("ITEMVERSION");
            // 품목 명
            grdDurable.View.AddTextBoxColumn("PRODUCTDEFNAME", 200).SetLabel("ITEMNAME");
            // SITE
            grdDurable.View.AddTextBoxColumn("PLANTID", 60).SetTextAlignment(TextAlignment.Center);
            // 공정수순
            grdDurable.View.AddTextBoxColumn("USERSEQUENCE", 70).SetTextAlignment(TextAlignment.Right);
            // 공정 ID
            grdDurable.View.AddTextBoxColumn("PROCESSSEGMENTID", 70).SetTextAlignment(TextAlignment.Center);
            // 공정 명
            grdDurable.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150);
            // UOM
            //grdDurable.View.AddTextBoxColumn("PROCESSUOM", 60).SetTextAlignment(TextAlignment.Center);
            // DESCRIPTION
            //grdDurable.View.AddTextBoxColumn("DESCRIPTION", 200).SetLabel("SPECIALNOTE");

            //자원관리 팝업
            grdDurable.View.AddTextBoxColumn("RESOURCEID", 120).SetLabel("DURABLEDEFID").SetIsReadOnly();
            grdDurable.View.AddTextBoxColumn("DURABLENAME", 250).SetLabel("TOOLNAME").SetIsReadOnly();
            grdDurable.View.AddTextBoxColumn("RESOURCEVERSION", 90).SetLabel("DURABLEDEFVERSION").SetTextAlignment(TextAlignment.Center);

            //this.grdDurable.View.AddComboBoxColumn("FILMUSELAYER1", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=FilmUseLayer1", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsReadOnly();
            //this.grdDurable.View.AddComboBoxColumn("FILMUSELAYER2", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=FilmUseLayer2", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsReadOnly();
            //this.grdDurable.View.AddTextBoxColumn("DESCRIPTION", 300);

            grdDurable.View.AddTextBoxColumn("CREATOR", 80).SetTextAlignment(TextAlignment.Center);
            grdDurable.View.AddTextBoxColumn("CREATEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime).SetTextAlignment(TextAlignment.Center);

            grdDurable.View.PopulateColumns();
            grdDurable.View.SetIsReadOnly();
        }

        #endregion 컨텐츠 영역 초기화

        #region 검색

        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            object productid = Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").EditValue;
            object durableid = Conditions.GetControl<SmartSelectPopupEdit>("P_DURABLEID").EditValue;

            if (productid == null)
            {
                if (durableid == null)
                {
                    throw MessageException.Create("RequiredSearch");
                }
                else if (durableid.Equals(""))
                {
                    throw MessageException.Create("RequiredSearch");
                }
            }
            else if (productid.Equals(""))
            {
                if (durableid == null)
                {
                    throw MessageException.Create("RequiredSearch");
                }
                else if (durableid.Equals(""))
                {
                    throw MessageException.Create("RequiredSearch");
                }
            }

            try
            {
                DialogManager.ShowWaitArea(this.pnlContent);

                await base.OnSearchAsync();

                grdDurable.View.ClearDatas();

                Dictionary<string, object> searchParam = Conditions.GetValues();
                searchParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                if (await SqlExecuter.QueryAsync("SelectDurableReport", "10001", searchParam) is DataTable dt)
                {
                    if (dt.Rows.Count.Equals(0))
                    {
                        ShowMessage("NoSelectData");
                        return;
                    }

                    grdDurable.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                throw MessageException.Create(Format.GetString(ex));
            }
            finally
            {
                DialogManager.CloseWaitArea(this.pnlContent);
            }
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            #region 품목 코드

            //팝업 컬럼 설정
            var condition = Conditions.AddSelectPopup("P_PRODUCTDEFID", new SqlQuery("GetProductItemGroup", "10002", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "ITEMID", "ITEMID")
                                      .SetPopupLayout("ITEMID", PopupButtonStyles.Ok_Cancel, true, false)  //팝업창 UI 설정.
                                      .SetPopupResultCount(1)  //팝업창 선택가능한 개수
                                      .SetPosition(2.1)
                                      .SetLabel("ITEMID")
                                      .SetPopupApplySelection((selectRow, gridRow) =>
                                      {
                                          List<string> productVersionList = new List<string>();
                                          List<string> productNameList = new List<string>();

                                          selectRow.AsEnumerable().ForEach(r =>
                                          {
                                              productVersionList.Add(Format.GetString(r["ITEMVERSION"]));
                                              productNameList.Add(Format.GetString(r["ITEMNAME"]));
                                          });

                                          Conditions.GetControl<SmartTextBox>("P_PRODUCTDEFVERSION").EditValue = string.Join(",", productVersionList);
                                          Conditions.GetControl<SmartTextBox>("P_PRODUCTDEFNAME").EditValue = string.Join(",", productNameList);
                                      });

            condition.Conditions.AddComboBox("MASTERDATACLASSID", new SqlQuery("GetTypeList", "10001", $"CODECLASSID=ProductDivision2", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                .SetDefault("Product");
            condition.Conditions.AddTextBox("ITEMID");
            condition.Conditions.AddTextBox("ITEMNAME");

            //팝업 그리드
            condition.GridColumns.AddTextBoxColumn("ITEMID", 150);
            condition.GridColumns.AddTextBoxColumn("ITEMNAME", 200);
            condition.GridColumns.AddTextBoxColumn("ITEMVERSION", 100);
            condition.GridColumns.AddTextBoxColumn("SPEC", 250);

            #endregion 품목 코드

            #region 치공구ID

            condition = Conditions.AddSelectPopup("P_DURABLEID", new SqlQuery("GetDurableList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "DURABLENAME", "DURABLEDEFID")
                .SetPopupLayout("DURABLEDEFID", PopupButtonStyles.Ok_Cancel, true, true)
                .SetPopupLayoutForm(940, 800)
                .SetPopupAutoFillColumns("DURABLEDEFNAME")
                .SetLabel("DURABLEDEFID")
                .SetPosition(4)
                .SetPopupResultCount(0);

            // 팝업 조회조건
            condition.Conditions.AddTextBox("DURABLEDEFID").SetLabel("TOOLCHANGETYPE");
            condition.Conditions.AddTextBox("DURABLEDEFNAME");

            // 팝업 그리드
            condition.GridColumns.AddTextBoxColumn("DURABLEDEFID", 120);
            condition.GridColumns.AddTextBoxColumn("DURABLENAME", 250).SetLabel("DURABLEDEFNAME");
            condition.GridColumns.AddTextBoxColumn("DURABLEDEFVERSION", 90);
            condition.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 80);
            condition.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 60);
            condition.GridColumns.AddTextBoxColumn("DURABLECLASS", 150);

            #endregion 치공구ID
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
            Conditions.GetControl<SmartTextBox>("P_PRODUCTDEFNAME").ReadOnly = true;
            Conditions.GetControl<SmartTextBox>("P_PRODUCTDEFVERSION").ReadOnly = true;

            Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").EditValueChanged += (s, e) =>
            {
                if (Format.GetFullTrimString((s as SmartSelectPopupEdit).EditValue).Equals(string.Empty))
                {
                    Conditions.GetControl<SmartTextBox>("P_PRODUCTDEFNAME").EditValue = string.Empty;
                    Conditions.GetControl<SmartTextBox>("P_PRODUCTDEFVERSION").EditValue = string.Empty;
                }
            };

            Conditions.GetControl<SmartComboBox>("P_PRODUCTDEFTYPE").EditValueChanged += (s, e) =>
            {
                string productDefType = Conditions.GetControl<SmartComboBox>("P_PRODUCTDEFTYPE").EditValue.ToString();
                if (!Format.GetFullTrimString(productDefType).Equals("*"))
                {
                    var popupControl = Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID");
                    popupControl.SelectPopupCondition.Conditions.GetCondition<ConditionItemComboBox>("MASTERDATACLASSID").SetDefault(productDefType);
                }
            };
        }

        #endregion 검색
    }
}