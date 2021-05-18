#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons;
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

using DevExpress.Utils.Menu;

#endregion

namespace Micube.SmartMES.ProcessManagement
{
	/// <summary>
	/// 프 로 그 램 명  : 공정관리 > 생산실적 > 제조이력
	/// 업  무  설  명  : 제조이력 조회
	/// 생    성    자  : 박정훈
	/// 생    성    일  : 2020-01-21
	/// 수  정  이  력  : 
	/// 
	/// 
	/// </summary>
	public partial class ManufactureHistory : SmartConditionManualBaseForm
	{
        #region ◆ Local Variables |
        DXMenuItem _myContextMenu1, _myContextMenu2, _myContextMenu3, _myContextMenu4, _myContextMenu5, _myContextMenu6, _myContextMenu7;
        #endregion

        #region ◆ 생성자 |

        public ManufactureHistory()
		{
			InitializeComponent();
		}

        #endregion

        #region ◆ 컨텐츠 영역 초기화 |

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

        #region ▶ 그리드 초기화 |
        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            grdHistory.GridButtonItem = GridButtonItem.Export;
            grdHistory.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdHistory.View.SetIsReadOnly();

            grdHistory.View.AddTextBoxColumn("PRODUCTDEFID", 100).SetTextAlignment(TextAlignment.Center);
            grdHistory.View.AddTextBoxColumn("PRODUCTDEFVERSION", 60).SetTextAlignment(TextAlignment.Center);
            grdHistory.View.AddTextBoxColumn("PRODUCTDEFNAME", 170);
            grdHistory.View.AddTextBoxColumn("LOTID", 180).SetTextAlignment(TextAlignment.Center);
            grdHistory.View.AddTextBoxColumn("WORKTYPE", 80).SetTextAlignment(TextAlignment.Center);
            grdHistory.View.AddTextBoxColumn("PROCESSSEQUENCE", 80).SetTextAlignment(TextAlignment.Center);
            grdHistory.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 120);
            grdHistory.View.AddTextBoxColumn("PLANTID", 60).SetTextAlignment(TextAlignment.Center);
            grdHistory.View.AddTextBoxColumn("AREANAME", 100);
            grdHistory.View.AddTextBoxColumn("RESOURCEID", 100).SetTextAlignment(TextAlignment.Center).SetIsHidden();
            grdHistory.View.AddTextBoxColumn("RESOURCENAME", 180);
            grdHistory.View.AddTextBoxColumn("EQUIPMENTID", 100).SetTextAlignment(TextAlignment.Center).SetIsHidden();
            grdHistory.View.AddTextBoxColumn("EQUIPMENTNAME", 180);
            //grdHistory.View.AddTextBoxColumn("DURABLELOTID", 180).SetTextAlignment(TextAlignment.Center);
            //grdHistory.View.AddTextBoxColumn("DURABLEDEFNAME", 180);
            //grdHistory.View.AddTextBoxColumn("USEDQTY", 100).SetLabel("USECOUNT").SetTextAlignment(TextAlignment.Right);
            grdHistory.View.AddTextBoxColumn("RECIPEID", 100).SetTextAlignment(TextAlignment.Center);
            grdHistory.View.AddTextBoxColumn("ROOTLOTSTARTDATE", 160).SetLabel("LOTINPUTDATE").SetTextAlignment(TextAlignment.Center);
            grdHistory.View.AddTextBoxColumn("RECEIVEDATE", 160).SetTextAlignment(TextAlignment.Center);
            grdHistory.View.AddTextBoxColumn("STARTDATE", 160).SetTextAlignment(TextAlignment.Center);
            grdHistory.View.AddTextBoxColumn("WORKENDDATE", 160).SetTextAlignment(TextAlignment.Center);
            grdHistory.View.AddTextBoxColumn("LOTSENDDATE", 160).SetTextAlignment(TextAlignment.Center);
            grdHistory.View.AddTextBoxColumn("RECEIVEPCSQTY", 100).SetLabel("INPUTPCSQTY").SetDisplayFormat("#,##0", MaskTypes.Numeric).SetTextAlignment(TextAlignment.Right);
            grdHistory.View.AddTextBoxColumn("RECEIVEPANELQTY", 100).SetLabel("INPUTPNLQTY").SetDisplayFormat("#,##0", MaskTypes.Numeric).SetTextAlignment(TextAlignment.Right);
            grdHistory.View.AddTextBoxColumn("SENDPCSQTY", 100).SetLabel("ENDPCSQTY").SetDisplayFormat("#,##0", MaskTypes.Numeric).SetTextAlignment(TextAlignment.Right);
            grdHistory.View.AddTextBoxColumn("SENDPANELQTY", 100).SetLabel("ENDPNLQTY").SetDisplayFormat("#,##0", MaskTypes.Numeric).SetTextAlignment(TextAlignment.Right);
            grdHistory.View.AddTextBoxColumn("DEFECTQTY", 100).SetLabel("DEFECTPCSQTY").SetDisplayFormat("#,##0", MaskTypes.Numeric).SetTextAlignment(TextAlignment.Right);
            grdHistory.View.AddTextBoxColumn("DEFECTPANELQTY", 100).SetLabel("DEFECTPNLQTY").SetDisplayFormat("#,##0", MaskTypes.Numeric).SetTextAlignment(TextAlignment.Right);
            grdHistory.View.AddTextBoxColumn("RECEIVELEADTIME", 100).SetDisplayFormat("#,##0.00", MaskTypes.Numeric).SetTextAlignment(TextAlignment.Right);
            grdHistory.View.AddTextBoxColumn("WORKSTARTLEADTIME", 100).SetDisplayFormat("#,##0.00", MaskTypes.Numeric).SetTextAlignment(TextAlignment.Right);
            grdHistory.View.AddTextBoxColumn("WORKENDLEADTIME", 100).SetDisplayFormat("#,##0.00", MaskTypes.Numeric).SetTextAlignment(TextAlignment.Right);
            grdHistory.View.AddTextBoxColumn("SENDLEADTIME", 100).SetDisplayFormat("#,##0.00", MaskTypes.Numeric).SetTextAlignment(TextAlignment.Right);
            grdHistory.View.AddTextBoxColumn("LEADTIME", 100).SetDisplayFormat("#,##0.00", MaskTypes.Numeric).SetTextAlignment(TextAlignment.Right);
            grdHistory.View.AddTextBoxColumn("ROOTLOTID", 180).SetLabel("ROOTLOTNO").SetTextAlignment(TextAlignment.Center);
            grdHistory.View.AddTextBoxColumn("PARENTLOTID", 180).SetTextAlignment(TextAlignment.Center);

            grdHistory.View.PopulateColumns();
        }
        #endregion

        #region ▶ 조회조건 초기화 |
        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();


            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
            Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").EditValueChanged += ProductdefIDChanged;
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            // Lot
            CommonFunction.AddConditionLotPopup("P_LOTID", 3.5, true, Conditions);

            InitializeConditionProductOrderId_Popup();

            // 품목
            CommonFunction.AddConditionProductPopup("P_PRODUCTDEFID", 1.5, true, Conditions, "PRODUCTDEFID");
            Conditions.GetCondition<ConditionItemSelectPopup>("P_PRODUCTDEFID").SetValidationIsRequired();

            Conditions.GetCondition<ConditionItemSelectPopup>("P_PRODUCTDEFID").SetPopupApplySelection((selectedRows, dataGridRows) =>
            {
                string productDefName = Format.GetString(selectedRows.FirstOrDefault()["PRODUCTDEFNAME"]);

                List<DataRow> list = selectedRows.ToList<DataRow>();

                List<string> listRev = new List<string>();

                string productlist = string.Empty;
                selectedRows.ForEach(row =>
                {
                    string productid = Format.GetString(row["PRODUCTDEFID"]);
                    string revision = Format.GetString(row["PRODUCTDEFVERSION"]);
                    productlist = productlist + productid + ',';
                    listRev.Add(revision);
                }
                );

                productlist = productlist.TrimEnd(',');

                listRev = listRev.Distinct().ToList();
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                param.Add("P_PLANTID", UserInfo.Current.Plant);
                param.Add("P_PRODUCTDEFID", productlist);

                DataTable dt = SqlExecuter.Query("selectProductdefVesion", "10001", param);


                Conditions.GetControl<SmartComboBox>("P_PRODUCTDEFVERSION").DataSource = dt;
                if (listRev.Count > 0)
                {
                    if (listRev.Count == 1)
                        Conditions.GetControl<SmartComboBox>("P_PRODUCTDEFVERSION").EditValue = Format.GetFullTrimString(listRev[0]);
                    else
                        Conditions.GetControl<SmartComboBox>("P_PRODUCTDEFVERSION").EditValue = Format.GetFullTrimString('*');
                }

                Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").Text = productDefName;
            });
        }

        private void InitializeConditionProductOrderId_Popup()
        {
            var conditionProductionOrderId = Conditions.AddSelectPopup("P_PRODUCTIONORDERID"
                    , new SqlQuery("GetProductionOrderIdListOfLotInput", "10001"
                        , $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")
                    , "PRODUCTIONORDERID", "PRODUCTIONORDERID")
                .SetPopupLayout("SELECTPRODUCTIONORDER", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupResultCount(0)
                .SetPopupLayoutForm(1000, 800)
                .SetPopupAutoFillColumns("PRODUCTDEFNAME")
                .SetLabel("PRODUCTIONORDERID")
                .SetPosition(1.2)
                .SetPopupApplySelection((selectedRows, dataGridRows) =>
                {
                    // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
                    // dataGridRow : 현재 Focus가 있는 그리드의 DataRow
                });


            // 팝업에서 사용되는 검색조건 (P/O번호)
            conditionProductionOrderId.Conditions.AddTextBox("TXTPRODUCTIONORDERID");

            // 팝업 그리드에서 보여줄 컬럼 정의
            // P/O번호
            conditionProductionOrderId.GridColumns.AddTextBoxColumn("PRODUCTIONORDERID", 150)
                .SetValidationKeyColumn();
            // 수주라인
            conditionProductionOrderId.GridColumns.AddTextBoxColumn("LINENO", 50);
            // 수주량
            conditionProductionOrderId.GridColumns.AddSpinEditColumn("PLANQTY", 90);
            // 품목코드
            conditionProductionOrderId.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            // 품목명
            conditionProductionOrderId.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            // 품목버전
            conditionProductionOrderId.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 100);
            // 품목코드 | 품목버전
            conditionProductionOrderId.GridColumns.AddTextBoxColumn("PRODUCTDEF", 0).SetIsHidden();
        }

        #endregion

        #endregion

        #region ◆ 이벤트 초기화 |
        private void InitializeEvent()
        {
            grdHistory.InitContextMenuEvent += GrdHistory_InitContextMenuEvent;
        }

        private void GrdHistory_InitContextMenuEvent(SmartBandedGrid sender, Framework.SmartControls.Grid.InitContextMenuEventArgs args)
        {
            args.AddMenus.Add(_myContextMenu1 = new DXMenuItem(Language.Get("MENU_PG-SG-0340"), OpenForm) { BeginGroup = true, Tag = "PG-SG-0340" });
            args.AddMenus.Add(_myContextMenu2 = new DXMenuItem(Language.Get("MENU_PG-SG-0625"), OpenForm) { Tag = "PG-SG-0625" });
            args.AddMenus.Add(_myContextMenu3 = new DXMenuItem(Language.Get("MENU_PG-SG-0450"), OpenForm) { Tag = "PG-SG-0450" });
            args.AddMenus.Add(_myContextMenu4 = new DXMenuItem(Language.Get("MENU_PG-SD-0292"), OpenForm) { Tag = "PG-SD-0292" });
            args.AddMenus.Add(_myContextMenu5 = new DXMenuItem(Language.Get("MENU_PG-SG-0540"), OpenForm) { Tag = "PG-SG-0540" });
            args.AddMenus.Add(_myContextMenu6 = new DXMenuItem(Language.Get("MENU_DefectMapByLot"), OpenForm) { Tag = "DefectMapByLot" });
            args.AddMenus.Add(_myContextMenu7 = new DXMenuItem(Language.Get("MENU_DefectMapByItem"), OpenForm) { Tag = "DefectMapByItem" });
        }

        /// <summary>
        /// Menu Context로 다른 창 Open
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OpenForm(object sender, EventArgs args)
        {
            try
            {
                DialogManager.ShowWaitDialog();

                DataRow currentRow = grdHistory.View.GetFocusedDataRow();

                string menuId = (sender as DXMenuItem).Tag.ToString();

                var param = currentRow.Table.Columns
                    .Cast<DataColumn>()
                    .ToDictionary(col => col.ColumnName, col => currentRow[col.ColumnName]);

                OpenMenu(menuId, param); //다른창 호출..
            }
            catch (Exception ex)
            {
                this.ShowError(ex);
            }
            finally
            {
                DialogManager.Close();
            }
        }
        #endregion

        #region ◆ 검색 |

        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
		{
			await base.OnSearchAsync();

			// TODO : 조회 SP 변경
			var values = Conditions.GetValues();
			values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

    		DataTable dt = await QueryAsync("SelectManufactureHistory", "10001", values);

			grdHistory.DataSource = dt;
		}

        /// <summary>
        /// 조회조건 품목 관련 EVENT
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProductdefIDChanged(object sender, EventArgs e)
        {
            SmartSelectPopupEdit PopProdutid = sender as SmartSelectPopupEdit;

            if (Format.GetFullTrimString(PopProdutid.EditValue).Equals(string.Empty))
            {
                Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").Text = string.Empty;
                Conditions.GetControl<SmartComboBox>("P_PRODUCTDEFVERSION").EditValue = string.Empty;
            }
        }
        #endregion

        #region Private Function
        #endregion
    }
}