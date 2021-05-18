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
using System.IO;

using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.BandedGrid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Menu;
using DevExpress.Utils.Menu;
using DevExpress.XtraPivotGrid;
using DevExpress.XtraBars;
#endregion

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 공정관리 > 투입관리 > LOT 투입 실적 조회
    /// 업  무  설  명  : 모델별 재공 조회
    /// 생    성    자  : 배선용
    /// 생    성    일  : 2019-08-15
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class InputLotRecord : SmartConditionManualBaseForm
    {
        #region ◆ Local Variables |
        PivotGridHitInfo hit = null;

        #endregion

        #region ◆ 생성자 |
        /// <summary>
        /// 생성자
        /// </summary>
        public InputLotRecord()
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
            InitializationSummaryRow();
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
        }

        #region ▶ 조회조건 설정 |
        /// <summary>
        /// 검색조건 설정
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // Lot
            CommonFunction.AddConditionLotPopup("P_LOTID", 7, true, Conditions);
			// 품목
			InitializeCondition_ProductPopup();
			//CommonFunction.AddConditionProductPopup("P_PRODUCTDEFID", 6, true, Conditions);
			//공정
			CommonFunction.AddConditionProcessSegmentPopup("P_PROCESSSEGMENTID", 8,true, Conditions);
			//고객
			InitializeConditionCustomerId_Popup();
        }

        /// <summary>
        /// 조회조건 설정
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();
        }

		/// <summary>
		/// 팝업형 조회조건 생성 - 품목
		/// </summary>
		private void InitializeCondition_ProductPopup()
		{
			// SelectPopup 항목 추가
			var conditionProductId = Conditions.AddSelectPopup("P_PRODUCTDEFID", new SqlQuery("GetProductDefinitionList", "10002", $"PLANTID={UserInfo.Current.Plant}"), "PRODUCTDEF", "PRODUCTDEF")
				.SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)
				.SetPopupLayoutForm(800, 800)
				.SetPopupAutoFillColumns("PRODUCTDEFNAME")
				.SetLabel("PRODUCTDEFID")
				.SetPosition(6)
				.SetPopupResultCount(0)
				.SetPopupApplySelection((selectRow, gridRow) => {
					/*
					string productDefName = "";

					selectRow.AsEnumerable().ForEach(r => {
						productDefName += Format.GetString(r["PRODUCTDEFNAME"]) + ",";
					});

					Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").EditValue = productDefName;
					*/
				});

			// 팝업에서 사용되는 검색조건 (품목코드/명)
			conditionProductId.Conditions.AddTextBox("TXTPRODUCTDEFNAME");
			//제품 구분 추가 2019.08.14 배선용, 노석안 대리 요청
			conditionProductId.Conditions.AddComboBox("PRODUCTDIVISION", new SqlQuery("GetTypeList", "10001", $"CODECLASSID=ProductDivision2", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
				.SetDefault("Product");

			// 품목코드
			conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
			// 품목명
			conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);
			// 품목버전
			conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 100);
			// 품목유형구분
			conditionProductId.GridColumns.AddComboBoxColumn("PRODUCTDEFTYPE", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductDefType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
			// 생산구분
			conditionProductId.GridColumns.AddComboBoxColumn("PRODUCTIONTYPE", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
			// 단위
			conditionProductId.GridColumns.AddComboBoxColumn("UNIT", 90, new SqlQuery("GetUomDefinitionList", "10001", "UOMCLASSID=Segment"), "UOMDEFNAME", "UOMDEFID");
		}

		/// <summary>
		/// 팝업형 조회조건 생성 - 고객
		/// </summary>
		private void InitializeConditionCustomerId_Popup()
		{
			var customerIdPopup = this.Conditions.AddSelectPopup("P_CUSTOMERID", new SqlQuery("GetCustomerList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "CUSTOMERNAME", "CUSTOMERID")
				.SetPopupLayout(Language.Get("SELECTCUSTOMERID"), PopupButtonStyles.Ok_Cancel, true, true)
				.SetPopupResultCount(0)
				.SetPopupLayoutForm(800, 800, FormBorderStyle.FixedToolWindow)
				.SetPopupAutoFillColumns("CUSTOMERNAME")
				.SetPosition(9)
				.SetLabel("CUSTOMER");

			customerIdPopup.Conditions.AddTextBox("TXTCUSTOMERID");

			customerIdPopup.GridColumns.AddTextBoxColumn("CUSTOMERID", 150);
			customerIdPopup.GridColumns.AddTextBoxColumn("CUSTOMERNAME", 200);

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
        private void InitializationSummaryRow()
        {

            grdProduct.View.Columns["PRODUCTDEFNAME"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdProduct.View.Columns["PRODUCTDEFNAME"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));

            grdProduct.View.Columns["INPUTPCSQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdProduct.View.Columns["INPUTPCSQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdProduct.View.Columns["INPUTPNLQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdProduct.View.Columns["INPUTPNLQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdProduct.View.Columns["INPUTMMQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdProduct.View.Columns["INPUTMMQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdProduct.View.Columns["INPUTPRICE"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdProduct.View.Columns["INPUTPRICE"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdProduct.View.Columns["SMTPRICE"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdProduct.View.Columns["SMTPRICE"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdProduct.View.OptionsView.ShowFooter = true;
            grdProduct.ShowStatusBar = false;
        }
        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            #region - 재공 Grid 설정 
            grdInputResultByDay.GridButtonItem = GridButtonItem.Export;

            grdInputResultByDay.View.SetIsReadOnly();
            grdInputResultByDay.SetIsUseContextMenu(false);
            // CheckBox 설정

            grdInputResultByDay.View.AddTextBoxColumn("INPUTDAY", 100).SetTextAlignment(TextAlignment.Center);
            grdInputResultByDay.View.AddTextBoxColumn("WEEK", 50).SetTextAlignment(TextAlignment.Center).SetIsHidden();
            grdInputResultByDay.View.AddTextBoxColumn("WEEKNAME", 50).SetTextAlignment(TextAlignment.Center);
            grdInputResultByDay.View.AddSpinEditColumn("INPUTPCSQTY", 100).SetTextAlignment(TextAlignment.Right);
            grdInputResultByDay.View.AddSpinEditColumn("INPUTPNLQTY", 100).SetTextAlignment(TextAlignment.Right);
            grdInputResultByDay.View.AddSpinEditColumn("INPUTMMQTY", 100).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true);
            grdInputResultByDay.View.AddSpinEditColumn("INPUTPRICE", 100).SetTextAlignment(TextAlignment.Right);
            grdInputResultByDay.View.AddSpinEditColumn("BAREPRICE", 100).SetTextAlignment(TextAlignment.Right);
            grdInputResultByDay.View.AddSpinEditColumn("SMTPRICE", 100).SetTextAlignment(TextAlignment.Right);
            grdInputResultByDay.View.AddSpinEditColumn("STACKPCSQTY", 100).SetTextAlignment(TextAlignment.Right);
            grdInputResultByDay.View.AddSpinEditColumn("STACKPNLQTY", 100).SetTextAlignment(TextAlignment.Right); 
            grdInputResultByDay.View.AddSpinEditColumn("STACKMMQTY", 100).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true);
            grdInputResultByDay.View.AddSpinEditColumn("STACKPRICE", 140).SetTextAlignment(TextAlignment.Right);
            grdInputResultByDay.View.AddSpinEditColumn("STACKBAREPRICE", 140).SetTextAlignment(TextAlignment.Right);
            grdInputResultByDay.View.AddSpinEditColumn("STACKSMTPRICE", 140).SetTextAlignment(TextAlignment.Right);

            
            grdInputResultByDay.View.PopulateColumns();

            #endregion

            #region - 품목별 투입 실적

            grdProduct.GridButtonItem = GridButtonItem.Export;
            grdProduct.View.SetIsReadOnly();
            grdProduct.SetIsUseContextMenu(false);

            grdProduct.View.AddTextBoxColumn("INPUTDAY", 100).SetTextAlignment(TextAlignment.Center);
            grdProduct.View.AddTextBoxColumn("SALESORDERDATE", 120).SetTextAlignment(TextAlignment.Center).SetLabel("SALSEORDERINPUTTIME");
            grdProduct.View.AddTextBoxColumn("SPECAPPROVALDATE", 120).SetTextAlignment(TextAlignment.Center);
            grdProduct.View.AddTextBoxColumn("LOTCREATEDTIME", 120).SetTextAlignment(TextAlignment.Center);
            grdProduct.View.AddTextBoxColumn("INPUTTIME", 120).SetTextAlignment(TextAlignment.Center);
            grdProduct.View.AddTextBoxColumn("INPUTLT", 60).SetTextAlignment(TextAlignment.Center);

            grdProduct.View.AddTextBoxColumn("SALESORDERID", 100).SetTextAlignment(TextAlignment.Center);
            grdProduct.View.AddTextBoxColumn("LINENO", 60).SetTextAlignment(TextAlignment.Center).SetIsHidden();
            grdProduct.View.AddTextBoxColumn("INPUTCOUNT", 70).SetTextAlignment(TextAlignment.Center);
            grdProduct.View.AddTextBoxColumn("CUSTOMERNAME", 100).SetTextAlignment(TextAlignment.Left);
            grdProduct.View.AddTextBoxColumn("LOTTYPENAME", 60).SetTextAlignment(TextAlignment.Left).SetLabel("LOTTYPE");
            grdProduct.View.AddTextBoxColumn("LOTINPUTTYPE", 60).SetTextAlignment(TextAlignment.Left);
            grdProduct.View.AddTextBoxColumn("PRODUCTDEFID", 100).SetTextAlignment(TextAlignment.Left);
            grdProduct.View.AddTextBoxColumn("PRODUCTDEFVERSION", 60).SetTextAlignment(TextAlignment.Left);
            grdProduct.View.AddTextBoxColumn("PRODUCTDEFNAME", 60).SetTextAlignment(TextAlignment.Left);
            grdProduct.View.AddSpinEditColumn("INPUTPCSQTY", 60).SetTextAlignment(TextAlignment.Left);
            grdProduct.View.AddSpinEditColumn("INPUTPNLQTY", 60).SetTextAlignment(TextAlignment.Left);
            grdProduct.View.AddSpinEditColumn("INPUTMMQTY", 60).SetTextAlignment(TextAlignment.Left);
            grdProduct.View.AddSpinEditColumn("INPUTPRICE", 60).SetLabel("BAREPRICE").SetTextAlignment(TextAlignment.Left);
            grdProduct.View.AddSpinEditColumn("SMTPRICE", 60).SetTextAlignment(TextAlignment.Left);
            grdProduct.View.AddSpinEditColumn("TOTALPRICE", 60).SetTextAlignment(TextAlignment.Left).SetLabel("RESULTPRICE");

            grdProduct.View.PopulateColumns();

            grdProduct.View.OptionsView.AllowCellMerge = true;

            for(int i =0; i< grdProduct.View.Columns.Count; i++)
            {
                grdProduct.View.Columns[i].OptionsColumn.AllowMerge = DefaultBoolean.False;
            }

            grdProduct.View.Columns["INPUTDAY"].OptionsColumn.AllowMerge = DefaultBoolean.True;

            #endregion

            #region LOT그리드 설정


            grdLot.GridButtonItem = GridButtonItem.Export;
            grdLot.View.SetIsReadOnly();
            grdLot.SetIsUseContextMenu(false);

            grdLot.View.AddTextBoxColumn("INPUTDAY", 100).SetTextAlignment(TextAlignment.Center);
            grdLot.View.AddTextBoxColumn("SALESORDERDATE", 120).SetTextAlignment(TextAlignment.Center).SetLabel("SALSEORDERINPUTTIME");
            grdLot.View.AddTextBoxColumn("SPECAPPROVALDATE", 120).SetTextAlignment(TextAlignment.Center);
            grdLot.View.AddTextBoxColumn("LOTCREATEDTIME", 120).SetTextAlignment(TextAlignment.Center);
            grdLot.View.AddTextBoxColumn("INPUTTIME", 120).SetTextAlignment(TextAlignment.Center);
            grdLot.View.AddTextBoxColumn("INPUTLT", 60).SetTextAlignment(TextAlignment.Center);

            grdLot.View.AddTextBoxColumn("SALESORDERID", 100).SetTextAlignment(TextAlignment.Center);
            grdLot.View.AddTextBoxColumn("LINENO", 60).SetTextAlignment(TextAlignment.Center).SetIsHidden();
            grdLot.View.AddTextBoxColumn("CUSTOMERNAME", 100).SetTextAlignment(TextAlignment.Left);
            grdLot.View.AddTextBoxColumn("LOTTYPENAME", 60).SetTextAlignment(TextAlignment.Left).SetLabel("LOTTYPE");
            grdLot.View.AddTextBoxColumn("LOTINPUTTYPE", 60).SetTextAlignment(TextAlignment.Left);
            grdLot.View.AddTextBoxColumn("LOTID", 170).SetTextAlignment(TextAlignment.Left);
            grdLot.View.AddTextBoxColumn("PRODUCTDEFID", 100).SetTextAlignment(TextAlignment.Left);
            grdLot.View.AddTextBoxColumn("PRODUCTDEFVERSION", 60).SetTextAlignment(TextAlignment.Left);
            grdLot.View.AddTextBoxColumn("PRODUCTDEFNAME", 60).SetTextAlignment(TextAlignment.Left);
            grdLot.View.AddSpinEditColumn("INPUTPCSQTY", 60).SetTextAlignment(TextAlignment.Left);
            grdLot.View.AddSpinEditColumn("INPUTPNLQTY", 60).SetTextAlignment(TextAlignment.Left);
            grdLot.View.AddSpinEditColumn("INPUTMMQTY", 60).SetTextAlignment(TextAlignment.Left);
            grdLot.View.AddSpinEditColumn("INPUTPRICE", 60).SetLabel("BAREPRICE").SetTextAlignment(TextAlignment.Left);
            grdLot.View.AddSpinEditColumn("SMTPRICE", 60).SetTextAlignment(TextAlignment.Left);
            grdLot.View.AddSpinEditColumn("TOTALPRICE", 60).SetTextAlignment(TextAlignment.Left).SetLabel("RESULTPRICE");

            grdLot.View.PopulateColumns();

            grdLot.View.OptionsView.AllowCellMerge = true;

            for (int i = 0; i < grdLot.View.Columns.Count; i++)
            {
                grdLot.View.Columns[i].OptionsColumn.AllowMerge = DefaultBoolean.False;
            }

            grdLot.View.Columns["INPUTDAY"].OptionsColumn.AllowMerge = DefaultBoolean.True;


            #endregion

            #region 피벗 그리드 설정
            pvtInputResult.AddRowField("INPUTDAY", 120);
            pvtInputResult.AddRowField("SALESORDERID", 120);
            pvtInputResult.AddRowField("LINENO", 120);
            pvtInputResult.AddRowField("CUSTOMERNAME", 120);
            pvtInputResult.AddRowField("LOTINPUTTYPE", 120);
            pvtInputResult.AddRowField("PRODUCTDEFID", 120);
            pvtInputResult.AddRowField("PRODUCTDEFNAME", 120);

            pvtInputResult.AddDataField("INPUTPCSQTY", 120).SetCellFormat(FormatType.Numeric,"###,###.##");
            pvtInputResult.AddDataField("INPUTPNLQTY", 200).SetCellFormat(FormatType.Numeric, "###,###.##");
            pvtInputResult.AddDataField("INPUTMMQTY", 200).SetCellFormat(FormatType.Numeric, "###,###.##");
            pvtInputResult.AddDataField("INPUTPRICE", 200).SetCellFormat(FormatType.Numeric, "###,###.##");
            pvtInputResult.AddDataField("FBCBPRICE").SetCellFormat(FormatType.Numeric, "###,###.##");
            pvtInputResult.AddDataField("ASSYPRICE").SetCellFormat(FormatType.Numeric, "###,###.##");
            pvtInputResult.AddDataField("ETCPRICE").SetCellFormat(FormatType.Numeric, "###,###.##");

            pvtInputResult.AddFilterField("PANELSIZE");
            pvtInputResult.AddFilterField("INPUTTIME").SetCellFormat(FormatType.DateTime);
            pvtInputResult.AddFilterField("SALSEORDERINPUTTIME").SetCellFormat(FormatType.DateTime);
            pvtInputResult.AddFilterField("SPECAPPROVALDATE");
            pvtInputResult.AddFilterField("LOTCREATEDTIME").SetCellFormat(FormatType.DateTime);
            pvtInputResult.AddFilterField("INPUTLT").SetCellFormat(FormatType.Numeric, "###,###.##"); 
            pvtInputResult.AddFilterField("LOTID", 120);
            pvtInputResult.AddFilterField("REPLOTID", 120).SetCellFormat(FormatType.Numeric, "###,###.##");
            pvtInputResult.AddFilterField("REPINPUTPCSQTY", 120).SetCellFormat(FormatType.Numeric, "###,###.##"); 
            pvtInputResult.AddFilterField("REPINPUTPNLQTY", 120).SetCellFormat(FormatType.Numeric, "###,###.##"); 
            pvtInputResult.AddFilterField("REPINPUTMMQTY", 120).SetCellFormat(FormatType.Numeric, "###,###.##"); 
            pvtInputResult.AddFilterField("REPINPUTPRICE", 120).SetCellFormat(FormatType.Numeric, "###,###.##"); 

            pvtInputResult.PopulateFields();
            #endregion

        }

        //private void PvtInputResult_KeyUp(object sender, KeyEventArgs e)
        //{
        //    if (e.Modifiers == Keys.Control && e.KeyCode == Keys.C)
        //    {
        //         if (this.hit != null)
        //        {
        //            if (this.hit.HitTest == PivotGridHitTest.HeadersArea)
        //            {
        //                if (this.hit.HeaderField != null)
        //                {
        //                    Clipboard.SetText(this.hit.HeaderField.Caption);
        //                }
        //            }
        //            else if (this.hit.HitTest == PivotGridHitTest.Cell)
        //            {

        //            }
        //            else if (this.hit.HitTest == PivotGridHitTest.Value)
        //            {
        //                if (this.hit.ValueInfo != null)
        //                {
        //                    Clipboard.SetText(this.hit.ValueInfo.Value.ToString());
        //                }
        //            }

        //        }
        //    }
        //}

        //private void PvtInputResult_KeyDown(object sender, KeyEventArgs e)
        //{
          
        //}


        //private void PvtInputResult_MouseDown(object sender, MouseEventArgs e)
        //{
        //    this.hit = pvtInputResult.CalcHitInfo(new Point(e.X, e.Y));
        //}

        //private void PvtInputResult_ShowingEditor(object sender, DevExpress.XtraPivotGrid.CancelPivotCellEditEventArgs e)
        //{
        //    foreach (var field in pvtInputResult.GetFieldsByArea(PivotArea.RowArea))
        //    {
        //        object value = e.GetFieldValue(field);
        //    }
        //    foreach (var field in pvtInputResult.GetFieldsByArea(PivotArea.ColumnArea))
        //    {
        //        object value = e.GetFieldValue(field);
        //    }
        //}

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

            pvtInputResult.PopupMenuShowing += PvtInputResult_PopupMenuShowing;
            grdInputResultByDay.View.RowCellStyle += View_RowCellStyle;
            gbPivot.HeaderButtonClickEvent += GbPivot_HeaderButtonClickEvent;
            grdProduct.View.CustomDrawFooterCell += View_CustomDrawFooterCell;
        }

        private void View_CustomDrawFooterCell(object sender, FooterCellCustomDrawEventArgs e)
        {
            Font ft = new Font("Mangul Gothic", 10F, FontStyle.Bold);
            e.Appearance.BackColor = Color.LightBlue;
            e.Appearance.FillRectangle(e.Cache, e.Bounds);
            e.Info.AllowDrawBackground = false;
            e.Appearance.Font = ft;
        }

        private void PvtInputResult_PopupMenuShowing(object sender, DevExpress.XtraPivotGrid.PopupMenuShowingEventArgs e)
        {

            if (e.HitInfo != null)
            {
                DXMenuItem copyMenu = new DXMenuItem("Copy Data", null, imageCollection1.Images[0]);

                copyMenu.Tag = e.HitInfo;

                if (e.HitInfo.HitTest == PivotGridHitTest.HeadersArea)
                {
                    if (e.HitInfo.HeaderField != null)
                    {
                        copyMenu.Click += Copy_Click;
                        e.Menu.Items.Add(copyMenu);
                    }
                }
                else if (e.HitInfo.HitTest == PivotGridHitTest.Cell)
                {
                    copyMenu.Click += Copy_Click;
                    e.Menu.Items.Add(copyMenu);
                }
                else if (e.HitInfo.HitTest == PivotGridHitTest.Value)
                {
                    if (e.HitInfo.ValueInfo != null)
                    {
                        if (e.HitInfo.ValueInfo.ValueType == PivotGridValueType.Value)
                        {
                            copyMenu.Click += Copy_Click;
                            e.Menu.Items.Add(copyMenu);
                        }
                    }
                }
            }
        }


        private void Copy_Click(object sender, EventArgs e)
        {
            DXMenuItem menu = sender as DXMenuItem;
            if(menu != null)
            {
                PivotGridHitInfo hitInfo =  menu.Tag as PivotGridHitInfo;
                if (hitInfo != null)
                {

                    if (hitInfo.HitTest == PivotGridHitTest.HeadersArea)
                    {
                        if (hitInfo.HeaderField != null)
                        {
                            Clipboard.SetText(hitInfo.HeaderField.Caption);
                        }
                    }
                    else if (hitInfo.HitTest == PivotGridHitTest.Cell)
                    {
                        pvtInputResult.Cells.CopySelectionToClipboard();
                    }
                    else if (hitInfo.HitTest == PivotGridHitTest.Value)
                    {
                        if (hitInfo.ValueInfo != null)
                        {
                            if(hitInfo.ValueInfo.Value != null)
                                Clipboard.SetText(hitInfo.ValueInfo.Value.ToString());
                            else
                                Clipboard.SetText(hitInfo.ValueInfo.DataField.Caption);
                        }
                    }

                }
            }
        }

        private void GbPivot_HeaderButtonClickEvent(object sender, HeaderButtonClickArgs args)
        {
            string shtName = this.Text;

            string filePathName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), this.Text + DateTime.Now.ToString("yyyy-MM-dd_HHmmss") + ".xlsx");

            DevExpress.XtraPivotGrid.PivotXlsxExportOptions exOpt = new DevExpress.XtraPivotGrid.PivotXlsxExportOptions();
            exOpt.ExportType = DevExpress.Export.ExportType.WYSIWYG;
            exOpt.ShowGridLines = true;
            exOpt.SheetName = shtName;
            pvtInputResult.ExportToXlsx(filePathName, exOpt);
            System.Diagnostics.Process.Start(filePathName);
            /*
            if (args.ClickItem.Equals(GridButtonItem.Export))
            {

                SaveFileDialog opf = new SaveFileDialog();
                opf.Filter = "Excel Files(*.xlsx)|*.xlsx;*.xls";

                DevExpress.XtraPivotGrid.PivotXlsxExportOptions exOpt = new DevExpress.XtraPivotGrid.PivotXlsxExportOptions();
                exOpt.ExportType = DevExpress.Export.ExportType.WYSIWYG;
                exOpt.ShowGridLines = true;
                
                
                opf.ShowDialog();
                if(opf.FileName.Length > 0)
                {
                    pvtInputResult.ExportToXlsx(opf.FileName, exOpt);
                }
               
            }
            */
        }

        private void View_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {

            GridView gr = sender as GridView;
            if (e.RowHandle < 0) return;

            DataRow dr =(DataRow)gr.GetDataRow(e.RowHandle);

            string week = Format.GetFullTrimString(dr["WEEK"]);

            if(week.Equals("Monday"))
            {
                e.Appearance.BackColor = Color.FromArgb(183, 240, 177);
            }
        }



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
            this.grdInputResultByDay.DataSource = null;
           

            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable dtInputResult = null;
            DataTable dtInputResultPVT = null;
            DataTable dtInputResultProduct = null;
            DataTable dtInputResultLot = null;

            if (tabMain.SelectedTabPage.Name.Equals("tbInputResult"))
            {
                dtInputResult = await SqlExecuter.QueryAsync("SelectInputResultByInputDay", "10001", values);
                if (dtInputResult.Rows.Count < 1)
                {
                    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                }
                grdInputResultByDay.DataSource = dtInputResult;
            }
            else if (tabMain.SelectedTabPage.Name.Equals("tbPivot"))
            {
                dtInputResultPVT = await SqlExecuter.QueryAsync("SelectInputResultForPivot", "10001", values);
                if (dtInputResultPVT.Rows.Count < 1)
                {
                    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                }
                pvtInputResult.DataSource = dtInputResultPVT;
            }
            else if (tabMain.SelectedTabPage.Name.Equals("tbLot"))
            {
                dtInputResultLot = await SqlExecuter.QueryAsync("SelectInputResultbyLot", "10001", values);
                if (dtInputResultLot.Rows.Count < 1)
                {
                    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                }
                grdLot.DataSource = dtInputResultLot;
            }
            else
            {
                dtInputResultProduct = await SqlExecuter.QueryAsync("SelectInputResultbyProduct", "10001", values);
                if (dtInputResultProduct.Rows.Count < 1)
                {
                    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                }
                grdProduct.DataSource = dtInputResultProduct;
            }



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
        private DXMenuCheckItem CreateCheckItem(string caption, string menuId)
        {
            DXMenuCheckItem item = new DXMenuCheckItem(caption, false, null, new EventHandler(OnQuickMenuItemClick));
            item.Tag = menuId;
            return item;
        }
        private void OnQuickMenuItemClick(object sender, EventArgs e)
        {
            DXMenuCheckItem item = sender as DXMenuCheckItem;
            string menuId = (string)item.Tag;

            DataRow row = grdInputResultByDay.View.GetFocusedDataRow();
            var param = row.Table.Columns
                .Cast<DataColumn>()
                .ToDictionary(col => col.ColumnName, col => row[col.ColumnName]);

            //메뉴 호출			
            OpenMenu(menuId, param);
        }
        #endregion
    }
}
