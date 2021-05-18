#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

#endregion using

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프 로 그 램 명  : 기준정보 > Setup > Bom 소요량조회
    /// 업  무  설  명  : Bom 소요량을 조회한다.
    /// 생    성    자  : 한주석
    /// 생    성    일  : 2019-12-10
    /// 수  정  이  력  :
    /// 2021.01.19 전우성 : Row중 자재타입이 원자재,부자재인 경우 Row 색상 변경 및 코드 정리
    ///
    /// </summary>
    public partial class BomQtyState : SmartConditionManualBaseForm
    {
        public BomQtyState()
        {
            InitializeComponent();
        }

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 화면의 컨텐츠 영역을 초기화한다.
        /// </summary>
        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeGrid();
            InitializeEvent();
        }

        /// <summary>
        /// 코드그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdBomList.GridButtonItem = GridButtonItem.Export;

            //SITE
            grdBomList.View.AddTextBoxColumn("SITE", 50).SetTextAlignment(TextAlignment.Center);
            //공정수순
            grdBomList.View.AddTextBoxColumn("USERSEQUENCE", 50).SetLabel("OPERATION").SetTextAlignment(TextAlignment.Right);
            //공정명
            grdBomList.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150);
            // 상위 품목코드
            grdBomList.View.AddTextBoxColumn("PARENTS_ASSEMBLYITEMID", 110).SetLabel("HIGHITEMCODE");
            // 상위 품목버전
            grdBomList.View.AddTextBoxColumn("PARENTS_ASSEMBLYITEMVERSION", 60).SetLabel("HIGHITEMVERSION");
            // 상위 품목명
            grdBomList.View.AddTextBoxColumn("MIDASSEMBLYITEMNAME", 200).SetLabel("HIGHITEMNAME");
            // 품목코드
            grdBomList.View.AddTextBoxColumn("ASSEMBLYITEMID", 110).SetLabel("ITEMCODE");
            // 품목버전
            grdBomList.View.AddTextBoxColumn("ASSEMBLYITEMVERSION", 60).SetLabel("ITEMVERSION");
            // 품목명
            grdBomList.View.AddTextBoxColumn("BOTASSEMBLYITEMNAME", 250).SetLabel("ITEMNAME");
            // 규격 SPEC
            grdBomList.View.AddTextBoxColumn("SPEC", 250);
            // 원단위
            grdBomList.View.AddSpinEditColumn("ASSEMBLYQTY", 90).SetDisplayFormat("#,##0.#########", MaskTypes.Numeric, true).SetLabel("REQUIREMENTQTY_ORG");
            // 소요량
            grdBomList.View.AddSpinEditColumn("ASSEMBLYCALCQTY", 90).SetDisplayFormat("#,##0.#########", MaskTypes.Numeric, true).SetLabel("REQUIREMENTQTY");
            // 규격 SPEC
            grdBomList.View.AddTextBoxColumn("PCSARY", 70).SetLabel("BLKPNL");
            // 생지배수
            grdBomList.View.AddSpinEditColumn("MULTIPLE", 60);
            // LEVEL
            grdBomList.View.AddTextBoxColumn("lvl", 50).SetLabel("Level").SetTextAlignment(TextAlignment.Center);
            //공정수순
            grdBomList.View.AddTextBoxColumn("BOMSEQUENCE", 120).SetLabel("USERSEQUENCE");
            // 최상위 품목코드
            grdBomList.View.AddTextBoxColumn("ROOT_ASSEMBLYITEMID", 110).SetLabel("HIGHESTITEMCODE");
            // 최상위 품목버전
            grdBomList.View.AddTextBoxColumn("ROOT_ASSEMBLYITEMVERSION", 60).SetLabel("HIGHESTITEMVERSION");
            // 최상위 품목명
            grdBomList.View.AddTextBoxColumn("TOPASSEMBLYITEMNAME", 150).SetLabel("HIGHESTITEMNAME");

            // 2021.01.19 전우성 : 자재항목 Text 색상을 변경하기 위한 필드추가
            grdBomList.View.AddTextBoxColumn("MASTERDATACLASSID").SetIsHidden();

            grdBomList.View.PopulateColumns();
            grdBomList.View.SetIsReadOnly();
        }

        #endregion 컨텐츠 영역 초기화

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // 자재타입이 원자재, 부자재인경우 색상변경
            grdBomList.View.RowStyle += (s, e) =>
            {
                if (Format.GetString(grdBomList.View.GetRowCellValue(e.RowHandle, "MASTERDATACLASSID")) is string result)
                {
                    e.Appearance.ForeColor = (result.Equals("RawMaterial") || result.Equals("Subsidiary")) ? Color.Blue : e.Appearance.ForeColor;
                }
            };

            // 조회조건에 품목코드 입력값 변경 이벤트
            Conditions.GetControls<SmartSelectPopupEdit>().ForEach(control =>
            {
                control.EditValueChanged += (s, e) =>
                {
                    if (Format.GetFullTrimString((s as SmartSelectPopupEdit).EditValue).Equals(string.Empty))
                    {
                        Conditions.GetControl<SmartTextBox>("P_PRODUCTDEFVERSION").EditValue = string.Empty;
                        Conditions.GetControl<SmartTextBox>("P_PRODUCTDEFNAME").EditValue = string.Empty;
                    }
                };
            });
        }

        #endregion Event

        #region 검색

        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            try
            {
                DialogManager.ShowWaitArea(this.pnlContent);

                await base.OnSearchAsync();

                var values = Conditions.GetValues();
                values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                string queryVersion = "10001";
                SmartComboBox cboDisplaySegment = Conditions.GetControl<SmartComboBox>("P_DISPLAYSEGMENT");
                if (cboDisplaySegment != null && cboDisplaySegment.EditValue.ToString().Equals("Y"))
                {
                    queryVersion = "10002";
                }

                grdBomList.View.ClearDatas();

                if (SqlExecuter.Query("SelectBOMTree", queryVersion, values) is DataTable dt)
                {
                    if (dt.Rows.Count.Equals(0))
                    {
                        ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                        return;
                    }

                    grdBomList.DataSource = dt;
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

            // SelectPopup 항목 추가
            var condition = Conditions.AddSelectPopup("P_PRODUCTDEFID", new SqlQuery("GetProductDefinitionList", "10002", $"PLANTID={UserInfo.Current.Plant}"), "PRODUCTDEFID", "PRODUCTDEFID")
                                      .SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)
                                      .SetPopupLayoutForm(800, 800)
                                      .SetPopupAutoFillColumns("PRODUCTDEFNAME")
                                      .SetLabel("PRODUCTDEFID")
                                      .SetPosition(1.5)
                                      .SetPopupResultCount(0)
                                      .SetPopupApplySelection((selectRow, gridRow) =>
                                      {
                                          List<string> productDefnameList = new List<string>();
                                          List<string> productRevisionList = new List<string>();

                                          selectRow.AsEnumerable().ForEach(r =>
                                          {
                                              productRevisionList.Add(Format.GetString(r["PRODUCTDEFVERSION"]));
                                              productDefnameList.Add(Format.GetString(r["PRODUCTDEFNAME"]));
                                          });

                                          Conditions.GetControl<SmartTextBox>("P_PRODUCTDEFVERSION").EditValue = string.Join(",", productRevisionList);
                                          Conditions.GetControl<SmartTextBox>("P_PRODUCTDEFNAME").EditValue = string.Join(",", productDefnameList);
                                      });

            // 팝업에서 사용되는 검색조건 (품목코드/명)
            condition.Conditions.AddTextBox("TXTPRODUCTDEFNAME");
            //제품 구분 추가 2019.08.14 배선용, 노석안 대리 요청
            //conditionProductId.Conditions.AddComboBox("PRODUCTDIVISION", new SqlQuery("GetTypeList", "10001", $"CODECLASSID=ProductDivision2", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
            //    .SetDefault("Product");

            // 품목코드
            condition.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 100);
            // 품목명
            condition.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            // 품목버전
            condition.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 60);
            // 품목유형구분
            condition.GridColumns.AddComboBoxColumn("PRODUCTDEFTYPE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductDefType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            // 생산구분
            condition.GridColumns.AddComboBoxColumn("PRODUCTIONTYPE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            // 단위
            condition.GridColumns.AddComboBoxColumn("UNIT", 60, new SqlQuery("GetUomDefinitionList", "10001", "UOMCLASSID=Segment"), "UOMDEFNAME", "UOMDEFID");
        }

        /// <summary>
        /// 조회조건 컨트롤 Init
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();
            Conditions.GetControl<SmartTextBox>("P_PRODUCTDEFNAME").ReadOnly = true;
            Conditions.GetControl<SmartTextBox>("P_PRODUCTDEFVERSION").ReadOnly = true;
        }

        #endregion 검색
    }
}