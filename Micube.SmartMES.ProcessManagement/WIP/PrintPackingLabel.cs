#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons;

using System;
using Micube.SmartMES.Commons.Controls;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.BandedGrid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Menu;
using DevExpress.Utils.Menu;
using DevExpress.XtraReports.UI;
#endregion

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 공정관리 > 포장관리 > 포장 라벨 재출력
    /// 업  무  설  명  : 포장 라벨 재출력
    /// 생    성    자  : 배선용
    /// 생    성    일  : 2019-07-11
    /// 수  정  이  력  : 2019-08-05 정승원 고객사, Site 조회조건 추가
    ///                  2019-10-24, 박정훈, 박스번호별 Grid 및 Lot정보 조회 Grid 상/하로 구성
    ///                  2019-11-23, 박정훈, X-OUT / Case 데이터 조회 및 출력 Grid 추가
    /// 
    /// 
    /// </summary>
    public partial class PrintPackingLabel : SmartConditionManualBaseForm
    {
        #region ◆ Local Variables |

        #endregion

        #region ◆ 생성자 |
        /// <summary>
        /// 생성자
        /// </summary>
        public PrintPackingLabel()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Form Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Form_Load(object sender, EventArgs e)
        {
            InitializeComboBox();
            InitializeGrid();
        }
        #endregion

        #region ◆ 컨텐츠 영역 초기화 |
        /// <summary>
        /// Control 설정
        /// </summary>
        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeEvent();

            btnPrintLabel2.Visible = true;
            /* NOTE : 현장테스트 위해 사용자 제한 주석처리
            if(UserInfo.Current.Id.Equals("yshwang") || UserInfo.Current.Id.Equals("jhpark"))
            {
                btnPrintLabel2.Visible = true;
            }
            */
        }

        #region ▶ 조회조건 설정 |
        /// <summary>
        /// 검색조건 설정
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // Lot
            CommonFunction.AddConditionLotPopup("P_LOTID", 1.1, true, Conditions, "Package");
            // 품목
            CommonFunction.AddConditionProductPopup("P_PRODUCTDEFID",1.2, true, Conditions);
			//고객사 ID
			InitializeConditionCustomerDefId_Popup();

		}

        /// <summary>
        /// 조회조건 설정
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();
        }

		/// <summary>
		/// 팝업형 조회조건 생성 - 고객사
		/// </summary>
		private void InitializeConditionCustomerDefId_Popup()
		{
			var conditionCustomerId = Conditions.AddSelectPopup("P_CUSTOMERID", new SqlQuery("GetCustomerList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "CUSTOMERNAME", "CUSTOMERID")
				.SetPopupLayout("SELECTCUSTOMERID", PopupButtonStyles.Ok_Cancel, true, false)
				.SetPopupResultCount(0)
				.SetLabel("CUSTOMERID")
				.SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
				.SetPopupAutoFillColumns("CUSTOMERNAME")
				.SetPosition(2.2);

			conditionCustomerId.Conditions.AddTextBox("TXTCUSTOMERID");

			//고객 ID
			conditionCustomerId.GridColumns.AddTextBoxColumn("CUSTOMERID", 150);
			//고객명
			conditionCustomerId.GridColumns.AddTextBoxColumn("CUSTOMERNAME", 200);
		}

		#endregion

		#region ▶ ComboBox 초기화 |
		/// <summary>
		/// 콤보박스를 초기화 하는 함수
		/// </summary>
		private void InitializeComboBox()
        {
            
        }
        #endregion

        #region ▶ Grid 설정 |
        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            #region - 포장 Grid 설정 |
            grdPackingList.GridButtonItem = GridButtonItem.None;
            grdPackingList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdPackingList.View.OptionsSelection.MultiSelect = false;
            grdPackingList.View.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.False;
            grdPackingList.View.CheckMarkSelection.ShowCheckBoxHeader = false;

            grdPackingList.View.SetIsReadOnly();
            grdPackingList.SetIsUseContextMenu(false);
            // CheckBox 설정

            grdPackingList.View.AddTextBoxColumn("PRODUCTDEFID", 100).SetTextAlignment(TextAlignment.Left);
            grdPackingList.View.AddTextBoxColumn("PRODUCTDEFNAME", 250).SetTextAlignment(TextAlignment.Left);
            grdPackingList.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150).SetTextAlignment(TextAlignment.Left);
            grdPackingList.View.AddTextBoxColumn("PATHTYPE", 70).SetTextAlignment(TextAlignment.Left).SetIsHidden();
            grdPackingList.View.AddTextBoxColumn("BOXNO", 120).SetTextAlignment(TextAlignment.Center);
            grdPackingList.View.AddTextBoxColumn("WORKER", 100).SetTextAlignment(TextAlignment.Center);
            grdPackingList.View.AddTextBoxColumn("PCSQTY", 70).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}").SetIsReadOnly();
            grdPackingList.View.AddTextBoxColumn("DEFECTQTY", 70).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}").SetIsReadOnly();
            grdPackingList.View.AddTextBoxColumn("PACKINGDATE", 180).SetTextAlignment(TextAlignment.Center);
            grdPackingList.View.AddTextBoxColumn("XOUT", 70).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}").SetIsReadOnly();
            grdPackingList.View.AddTextBoxColumn("CARD", 70).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}").SetIsReadOnly();
            grdPackingList.View.AddTextBoxColumn("PACK", 70).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}").SetIsReadOnly();
            grdPackingList.View.AddTextBoxColumn("CASECNT", 70).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}").SetIsReadOnly();
            grdPackingList.View.AddTextBoxColumn("PATHTYPE", 70).SetIsHidden();
            grdPackingList.View.AddTextBoxColumn("LOTIDS").SetIsHidden();

            grdPackingList.View.PopulateColumns();
            #endregion

            #region - Lot 정보 설정 |
            grdLotList.GridButtonItem = GridButtonItem.None;
            grdLotList.View.SetIsReadOnly();

            grdLotList.View.AddTextBoxColumn("BOXNO", 150).SetTextAlignment(TextAlignment.Center);
            grdLotList.View.AddTextBoxColumn("LOTID", 250).SetTextAlignment(TextAlignment.Center);
            grdLotList.View.AddTextBoxColumn("PRODUCTDEFID", 100).SetTextAlignment(TextAlignment.Left);
            grdLotList.View.AddTextBoxColumn("PRODUCTDEFNAME", 250).SetTextAlignment(TextAlignment.Left);
            grdLotList.View.AddTextBoxColumn("PCSQTY", 70).SetTextAlignment(TextAlignment.Right);
            grdLotList.View.AddTextBoxColumn("WEEK", 70).SetTextAlignment(TextAlignment.Center);
            grdLotList.View.AddTextBoxColumn("PACKINGWEEK", 70).SetTextAlignment(TextAlignment.Center);

            grdLotList.View.PopulateColumns();
            #endregion

            #region - X-OUT Grid |
            grdXOUT.GridButtonItem = GridButtonItem.None;

            // CheckBox 설정
            grdXOUT.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdXOUT.View.AddTextBoxColumn("XOUT", 60).SetTextAlignment(TextAlignment.Right);
            grdXOUT.View.AddTextBoxColumn("CARD", 60).SetTextAlignment(TextAlignment.Right);
            grdXOUT.View.AddTextBoxColumn("GOODQTY", 60).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}").SetIsReadOnly();
            grdXOUT.View.AddTextBoxColumn("DEFECTQTY", 60).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}").SetIsReadOnly();

            grdXOUT.View.PopulateColumns();

            InitializationXOUTSummaryRow();
            #endregion

            #region - Case Grid |
            grdCase.GridButtonItem = GridButtonItem.None;

            // CheckBox 설정
            grdCase.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdCase.View.AddTextBoxColumn("CASENO", 60).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdCase.View.AddTextBoxColumn("QTY", 60).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}").SetIsReadOnly();

            grdCase.View.PopulateColumns();

            InitializationCaseSummaryRow();
            #endregion
        }
        #endregion

        #endregion

        #region ◆ Event |

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            this.Load += Form_Load;
            this.btnPrintLabel.Click += BtnPrintLabel_Click;
            this.btnPrintTrayLabel.Click += BtnPrintTrayLabel_Click;
            this.btnPrintLabelCase.Click += BtnPrintLabelCase_Click;
            this.btnPrintXoutInnerLabel.Click += BtnPrintXoutInnerLabel_Click;
            this.btnPrintXoutOuterLabel.Click += BtnPrintXoutOuterLabel_Click;
            this.btnChangeCustomer.Click += BtnChangeCustomer_Click;

            this.grdPackingList.View.RowCellClick += PackingListView_RowCellClick;
            this.grdXOUT.View.CustomDrawFooterCell += GrdXOUTView_CustomDrawFooterCell;
            this.grdCase.View.CustomDrawFooterCell += GrdCaseView_CustomDrawFooterCell;

            this.grdPackingList.View.CheckStateChanged += View_CheckStateChanged;

            btnPrintLabel2.Click += BtnPrintLabel2_Click;
        }

        private void View_CheckStateChanged(object sender, EventArgs e)
        {
            DataTable checkedRows = grdPackingList.View.GetCheckedRows();
            int productDefIdCount = checkedRows.AsEnumerable().Select(r => Format.GetString(r["PRODUCTDEFID"])).Distinct().Count();
            if (productDefIdCount > 1)
            {
                this.grdPackingList.View.CheckStateChanged -= View_CheckStateChanged;
                grdPackingList.View.CheckRow(grdPackingList.View.FocusedRowHandle, false);
                this.grdPackingList.View.CheckStateChanged += View_CheckStateChanged;
            }
        }

        private void BtnPrintLabel2_Click(object sender, EventArgs e)
        {
            string boxnos = string.Empty;
            foreach (DataRow each in grdPackingList.View.GetCheckedRows().Rows)
            {
                if (each["BOXNO"] != DBNull.Value)
                {
                    boxnos += each["BOXNO"].ToString() + ",";
                }
            }
            if (boxnos.EndsWith(","))
            {
                boxnos = boxnos.Substring(0, boxnos.Length - 1);
            }
            PrintPackingLabelPopup4 pop = new PrintPackingLabelPopup4(boxnos);
            pop.ShowDialog();
        }

        #region ▶ Button Event |

        #region - Box 라벨 Print |
        /// <summary>
        /// 라벨 Print
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPrintLabel_Click(object sender, EventArgs e)
        {
            DataTable dt = grdPackingList.DataSource as DataTable;

            DataTable dtChange = grdPackingList.View.GetCheckedRows();

            List<LabelInfo> viewList = new List<LabelInfo>();
            //List<XtraReport> viewList = new List<XtraReport>();
            
            string isShipping = "N";
            for (int i = 0; i < dtChange.Rows.Count; i++)
            {
                if (dtChange.Rows[i]["PATHTYPE"].ToString().Equals("SHIPPING"))
                {
                    isShipping = "Y";
                }
                viewList.AddRange(SmartMES.Commons.CommonFunction.GetLabelData(dtChange.Rows[i]["BOXNO"].ToString(), "Box", isShipping, ""));

            }
        
            if (viewList.Count > 0)
            {
                PrintPackingLabelPopup3 pop = new PrintPackingLabelPopup3(viewList);
                pop.ShowDialog();
            }
            else
            {
                ShowMessage("NoLabelPrintInfo"); // 조회할 데이터가 없습니다.
            }
            /*

            for (int i = 0; i < dtChange.Rows.Count; i++)
            {
                if (dtChange.Rows[i]["PATHTYPE"].ToString().Equals("SHIPPING"))
                    viewList.AddRange(SmartMES.Commons.CommonFunction.GetExportLabel(dtChange.Rows[i]["BOXNO"].ToString()));
                else
                    viewList.AddRange(SmartMES.Commons.CommonFunction.GetBoxLabelType(dtChange.Rows[i]["BOXNO"].ToString(), "Box"));
                
            }
            if (viewList.Count > 0)
            {
                PrintPackingLabelPopup pop = new PrintPackingLabelPopup(viewList);
                pop.ShowDialog();
            }
            else
            {
                ShowMessage("NoLabelPrintInfo"); // 조회할 데이터가 없습니다.
            }
            */
            

        }
        #endregion

        #region - Tray 라벨 Print |
        /// <summary>
        /// 라벨 Print
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPrintTrayLabel_Click(object sender, EventArgs e)
        {
            DataTable dt = grdPackingList.DataSource as DataTable;

            DataTable dtChange = grdPackingList.View.GetCheckedRows();

            List<LabelInfo> viewList = new List<LabelInfo>();

            for (int i = 0; i < dtChange.Rows.Count; i++)
            {
                viewList.AddRange(SmartMES.Commons.CommonFunction.GetLabelData(dtChange.Rows[i]["BOXNO"].ToString(), "Tray", "Y", ""));
            }
            if (viewList.Count > 0)
            {
                PrintPackingLabelPopup3 pop = new PrintPackingLabelPopup3(viewList);
                pop.ShowDialog();
            }
            else
            {
                ShowMessage("NoLabelPrintInfo"); // 조회할 데이터가 없습니다.
            }

        }
        #endregion

        #region - Case 라벨 출력 |
        /// <summary>
        /// Case 라벨 출력
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPrintLabelCase_Click(object sender, EventArgs e)
        {
            DataRow dr = grdLotList.View.GetFocusedDataRow();

            DataTable caseDt = grdCase.View.GetCheckedRows();

            string strCaseNo = string.Join(",", caseDt.AsEnumerable().Select(c => Format.GetString(c["CASENO"])).Distinct());

            List<XtraReport> viewList = new List<XtraReport>();
            viewList.AddRange(SmartMES.Commons.CommonFunction.GetBoxLabelCase(dr["BOXNO"].ToString(), strCaseNo));

            if (viewList.Count > 0)
            {
                PrintPackingLabelPopup pop = new PrintPackingLabelPopup(viewList);
                pop.ShowDialog();
            }
            else
            {
                ShowMessage("NoLabelPrintInfo"); // 조회할 데이터가 없습니다.
            }
        }
        #endregion

        #region - X-Out 외부 라벨 출력 |
        /// <summary>
        /// X-Out 외부 라벨 출력
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPrintXoutOuterLabel_Click(object sender, EventArgs e)
        {
            DataRow dr = grdLotList.View.GetFocusedDataRow();

            List<XtraReport> viewList = new List<XtraReport>();
            viewList.AddRange(SmartMES.Commons.CommonFunction.GetBoxLabelXOUTOuter(dr["BOXNO"].ToString()));

            if (viewList.Count > 0)
            {
                PrintPackingLabelPopup pop = new PrintPackingLabelPopup(viewList);
                pop.ShowDialog();
            }
            else
            {
                ShowMessage("NoLabelPrintInfo"); // 조회할 데이터가 없습니다.
            }
        }
        #endregion

        #region - X-Out 내부 라벨 출력 |
        /// <summary>
        /// X-Out 내부 라벨 출력
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPrintXoutInnerLabel_Click(object sender, EventArgs e)
        {
            DataRow dr = grdLotList.View.GetFocusedDataRow();

            DataTable xoutDt = grdXOUT.View.GetCheckedRows();

            string strXoutSeq = string.Join(",", xoutDt.AsEnumerable().Select(c => Format.GetString(c["SERIALNO"])).Distinct());

            List<XtraReport> viewList = new List<XtraReport>();
            viewList.AddRange(SmartMES.Commons.CommonFunction.GetBoxLabelXOUTInner(dr["BOXNO"].ToString(), strXoutSeq));

            if (viewList.Count > 0)
            {
                PrintPackingLabelPopup pop = new PrintPackingLabelPopup(viewList);
                pop.ShowDialog();
            }
            else
            {
                ShowMessage("NoLabelPrintInfo"); // 조회할 데이터가 없습니다.
            }
        }
        #endregion

        #region - 고객사 변경 |
        /// <summary>
        /// 고객사 변경
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnChangeCustomer_Click(object sender, EventArgs e)
        {
            DataTable dt = grdPackingList.View.GetCheckedRows();

            if (dt == null || dt.Rows.Count == 0)
                return;

            ChangeCustomerPopup pop = new ChangeCustomerPopup(dt);
            pop.ShowDialog();
        }
        #endregion

        #endregion

        #region ▶ Grid Event |

        #region - 포장 목록 클릭 이벤트 |
        /// <summary>
        /// 포장 목록 클릭 이벤트 |
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PackingListView_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            DataRow dr = grdPackingList.View.GetFocusedDataRow();

            if (dr == null) return;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("P_BOXNO", dr["BOXNO"].ToString());

            DataTable dt = SqlExecuter.Query("SelectPackingLotList", "10001", param);

            if (dt == null || dt.Rows.Count == 0)
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                return;
            }

            grdLotList.DataSource = dt;

            // X-OUT 데이터 조회
            Dictionary<string, object> param2 = new Dictionary<string, object>();
            param2.Add("P_BOXNO", dr["BOXNO"].ToString());

            DataTable dt2 = SqlExecuter.Query("SelectPackingXOutList", "10001", param2);

            grdXOUT.DataSource = dt2;

            // Case 데이터 조회
            Dictionary<string, object> param3 = new Dictionary<string, object>();
            param3.Add("P_BOXNO", dr["BOXNO"].ToString());

            DataTable dt3 = SqlExecuter.Query("SelectPackingCaseList", "10001", param3);

            grdCase.DataSource = dt3;
        }
        #endregion

        #region - X-OUT Grid Footer Sum Event |
        /// <summary>
        /// Grid Footer Sum Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdXOUTView_CustomDrawFooterCell(object sender, DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventArgs e)
        {
            DataTable dt = grdXOUT.DataSource as DataTable;

            if (dt == null) return;

            if (dt.Rows.Count > 0)
            {
                int xoutSum = 0;
                int cardSum = 0;
                int qoodQtySum = 0;
                int defectSum = 0;

                dt.Rows.Cast<DataRow>().ForEach(row =>
                {
                    xoutSum += Format.GetInteger(row["XOUT"]);
                    cardSum += Format.GetInteger(row["CARD"]);
                    qoodQtySum += Format.GetInteger(row["GOODQTY"]);
                    defectSum += Format.GetInteger(row["DEFECTQTY"]);
                });

                if (e.Column.FieldName == "XOUT")
                {
                    e.Info.DisplayText = Format.GetString(xoutSum);
                }
                if (e.Column.FieldName == "CARD")
                {
                    e.Info.DisplayText = Format.GetString(cardSum);
                }
                if (e.Column.FieldName == "GOODQTY")
                {
                    e.Info.DisplayText = Format.GetString(qoodQtySum);
                }
                if (e.Column.FieldName == "DEFECTQTY")
                {
                    e.Info.DisplayText = Format.GetString(defectSum);
                }
            }
            else
            {
                grdXOUT.View.Columns["XOUT"].SummaryItem.DisplayFormat = "  ";
                grdXOUT.View.Columns["CARD"].SummaryItem.DisplayFormat = "  ";
                grdXOUT.View.Columns["GOODQTY"].SummaryItem.DisplayFormat = "  ";
                grdXOUT.View.Columns["DEFECTQTY"].SummaryItem.DisplayFormat = "  ";
            }
        }
        #endregion

        #region - Case Grid Footer Sum |
        /// <summary>
        /// Case Grid Footer Sum
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdCaseView_CustomDrawFooterCell(object sender, DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventArgs e)
        {
            DataTable dt = grdCase.DataSource as DataTable;

            if (dt == null) return;

            if (dt.Rows.Count > 0)
            {
                int QtySum = 0;

                dt.Rows.Cast<DataRow>().ForEach(row =>
                {
                    QtySum += Format.GetInteger(row["QTY"]);
                });

                if (e.Column.FieldName == "QTY")
                {
                    e.Info.DisplayText = Format.GetString(QtySum);
                }
            }
            else
            {
                grdCase.View.Columns["QTY"].SummaryItem.DisplayFormat = "  ";
            }
        }
        #endregion 

        #endregion

        #endregion

        #region ◆ 툴바 |

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            // TODO : 저장 Rule 변경
           
        }

        #endregion

        #region ◆ 검색 |

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            // 기존 Grid Data 초기화
            this.grdPackingList.View.ClearDatas();
            this.grdLotList.View.ClearDatas();

            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable dt = await SqlExecuter.QueryAsync("SelectPackingAndPackageWipList", "10001", values);

            if (dt.Rows.Count < 1)
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdPackingList.DataSource = dt;
        }
		#endregion

		#region ◆ 유효성 검사 |

		/// <summary>
		/// 데이터 저장할때 컨텐츠 영역의 유효성 검사
		/// </summary>
		protected override void OnValidateContent()
        {
            base.OnValidateContent();
        }

        #endregion

        #region ◆ Private Function |

        #region ▶ XOUT Grid Footer 추가 Panel, PCS 합계 표시 |
        /// <summary>

        /// Case Grid Footer 추가 Panel, PCS 합계 표시
        /// </summary>
        private void InitializationXOUTSummaryRow()
        {
            grdXOUT.View.Columns["XOUT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdXOUT.View.Columns["XOUT"].SummaryItem.DisplayFormat = " ";
            grdXOUT.View.Columns["CARD"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdXOUT.View.Columns["CARD"].SummaryItem.DisplayFormat = " ";
            grdXOUT.View.Columns["GOODQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdXOUT.View.Columns["GOODQTY"].SummaryItem.DisplayFormat = " ";
            grdXOUT.View.Columns["DEFECTQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdXOUT.View.Columns["DEFECTQTY"].SummaryItem.DisplayFormat = " ";

            grdXOUT.View.OptionsView.ShowFooter = true;
            grdXOUT.ShowStatusBar = false;
        }
        #endregion

        #region ▶ Case Grid Footer 추가 Panel, PCS 합계 표시 |
        /// <summary>
        /// Case Grid Footer 추가 Panel, PCS 합계 표시
        /// </summary>
        private void InitializationCaseSummaryRow()
        {
            grdCase.View.Columns["QTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdCase.View.Columns["QTY"].SummaryItem.DisplayFormat = " ";

            grdCase.View.OptionsView.ShowFooter = true;
            grdCase.ShowStatusBar = false;
        }
        #endregion

        #endregion
    }
}
