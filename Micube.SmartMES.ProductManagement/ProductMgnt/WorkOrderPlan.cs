#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons;

using System;
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


using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Collections;
using System.Globalization;
using Micube.SmartMES.Commons.Controls;

#endregion

namespace Micube.SmartMES.ProductManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 공정관리 > 생산관리 > 생산 계획 조회
    /// 업  무  설  명  : 영업 FCST 수량 업로드
    /// 생    성    자  : 배선용
    /// 생    성    일  : 2019-09-16
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class WorkOrderPlan : SmartConditionManualBaseForm
    {
        #region ◆ Local Variables |
        private HSSFWorkbook hssfwb;
        private XSSFWorkbook xssfwb;
        private DataTable saveDataTable;
        ISheet sheet;
        #endregion

        #region ◆ 생성자 |
        /// <summary>
        /// 생성자
        /// </summary>
        public WorkOrderPlan()
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
        }

        #region ▶ 조회조건 설정 |
        /// <summary>
        /// 검색조건 설정
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            InitializeConditionWeekoftheYear_Popup();
            // Lot
            // CommonFunction.AddConditionLotPopup("P_LOTID", 7, true, Conditions);
            // 품목
            CommonFunction.AddConditionProductPopup("P_PRODUCTDEFID", 1.2, true, Conditions);
			//공정
			//CommonFunction.AddConditionProcessSegmentPopup("P_PROCESSSEGMENTID", 8,true, Conditions);
			//고객
			InitializeConditionCustomerId_Popup();
        }

        /// <summary>
        /// 조회조건 설정
        /// </summary>
        protected async override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            SmartSelectPopupEdit week = Conditions.GetControl<SmartSelectPopupEdit>("WEEK");

            //SelectSalespoforcastWeekoftheyear

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("TXT_DATE", DateTime.Now.ToString("yyyy-MM-dd"));

            DataTable dtDate = await SqlExecuter.QueryAsync("SelectSalespoforcastWeekoftheyear", "10001", param);


            if (dtDate == null || dtDate.Rows.Count == 0)
            {
                ShowMessage("CheckRegistWeek"); 
                //등록된 날짜가 없습니다.
            }
            week.SetValue(dtDate.Rows[0]["WEEK"].ToString());

            week.Text = dtDate.Rows[0]["YEARWEEK"].ToString();

            week.ButtonClick += Week_ButtonClick;

        }

        private void Week_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind.Equals(DevExpress.XtraEditors.Controls.ButtonPredefines.Search))
            {
                SmartSelectPopupEdit d = sender as SmartSelectPopupEdit;
                d.ClearValue();
                
            }
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
				.SetPosition(1.4)
				.SetLabel("CUSTOMER");

			customerIdPopup.Conditions.AddTextBox("TXTCUSTOMERID");

			customerIdPopup.GridColumns.AddTextBoxColumn("CUSTOMERID", 150);
			customerIdPopup.GridColumns.AddTextBoxColumn("CUSTOMERNAME", 200);

		}

        /// <summary>
		/// 팝업형 조회조건 생성 - 주차
		/// </summary>
		private void InitializeConditionWeekoftheYear_Popup()
        {
            var customerIdPopup = this.Conditions.AddSelectPopup("WEEK", new SqlQuery("SelectSalespoforcastWeekoftheyear", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "YEARWEEK", "WEEK")
                .SetPopupLayout(Language.Get("SELECTWEEK"), PopupButtonStyles.Ok_Cancel, true, true)
                .SetPopupResultCount(1)
                .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow)
                .SetPopupAutoFillColumns("TXT_WEEK")
                .SetPosition(1.5)
                .SetValidationIsRequired()
                .SetLabel("WEEK");


            DateTime dateNow = DateTime.Now;
            string strFirstDate = dateNow.ToString("yyyy-MM-dd");
            string strEndDate = dateNow.AddMonths(6).ToString("yyyy-MM-dd");

            var dtStart = customerIdPopup.Conditions.AddDateEdit("FROM_DATE")
               .SetLabel("WEEKOFTHEYEAR")
               .SetDisplayFormat("yyyy-MM-dd")
               .SetDefault(strFirstDate)
               .SetValidationIsRequired();
            var dtEnd = customerIdPopup.Conditions.AddDateEdit("TO_DATE")
               .SetLabel("")
               .SetDisplayFormat("yyyy-MM-dd")
               .SetDefault(strEndDate)
               .SetValidationIsRequired();

            ConditionItemTextBox txtWeeek = customerIdPopup.Conditions.AddTextBox("TXT_WEEK");

            txtWeeek.SetDisplayFormat("d", MaskTypes.Numeric);
            txtWeeek.SetLabel("WEEK");

            customerIdPopup.GridColumns.AddTextBoxColumn("YEARMONTH", 150);
            customerIdPopup.GridColumns.AddTextBoxColumn("WEEK", 100);

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
            #region - 재공 Grid 설정 
            grdFcst.GridButtonItem = GridButtonItem.None;

            grdFcst.View.SetIsReadOnly();
            grdFcst.SetIsUseContextMenu(false);
            // CheckBox 설정

            grdFcst.View.AddTextBoxColumn("LOTTYPE", 100).SetTextAlignment(TextAlignment.Center);
            grdFcst.View.AddTextBoxColumn("PRODUCTDEFID", 150).SetTextAlignment(TextAlignment.Center);
            grdFcst.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetTextAlignment(TextAlignment.Center);
            grdFcst.View.AddTextBoxColumn("PRODUCTDEFNAME", 150).SetTextAlignment(TextAlignment.Center);
            grdFcst.View.AddSpinEditColumn("CUSTOMER", 100).SetTextAlignment(TextAlignment.Right);
            
            grdFcst.View.PopulateColumns();

            #endregion

            #region 피벗 그리드 설정
            pvProductPlan.AddRowField("PRODUCTDEFID", 120);
            pvProductPlan.AddRowField("PRODUCTDEFNAME", 120);
            pvProductPlan.AddRowField("CUSTOMER", 120);
            pvProductPlan.AddRowField("LOTTYPE", 120);
            pvProductPlan.AddRowField("FCSTDATE", 120);
            pvProductPlan.AddRowField("WEEK", 120);
            pvProductPlan.AddRowField("MONTH", 120);
            pvProductPlan.AddRowField("PCSPNL", 120).SetCellFormat(FormatType.Numeric, "###,###.##");

            pvProductPlan.AddDataField("QTY", 120).SetCellFormat(FormatType.Numeric,"###,###.##");
            pvProductPlan.AddDataField("PANELQTY", 120).SetCellFormat(FormatType.Numeric, "###,###.##");


            pvProductPlan.PopulateFields();
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
 
            gbPivot.HeaderButtonClickEvent += GbPivot_HeaderButtonClickEvent;
        
        }


        private DataSet GetSheetDate(DataTable dtSheet, string fileName)
        {
            FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            string extension = Path.GetExtension(fileName);

            int ProductIdx = 0;
            int dateStartIdx = 0;
            int dateRow = 0;

            int[] UseSheet = null;

            if(UserInfo.Current.Enterprise.Equals(Constants.EnterPrise_YoungPoong))
            {
                ProductIdx = 2;
                dateStartIdx = 4;
                dateRow = 3;
                UseSheet = new int[] { 2, 3, 4, 5, 6 };
            }
            else
            {
                ProductIdx = 7;
                dateStartIdx = 10;
                dateRow = 2;
                UseSheet = new int[] { 0 };
            }
            DataSet ds = new DataSet();


            for (int i = 0; i < dtSheet.Rows.Count; i++)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("PRODUCTDEFID");

                string sheetName = Format.GetFullTrimString(dtSheet.Rows[i]["SheetName"]);

                if (!UseSheet.Contains(i)) continue;
                //if (i != 2 && i != 3 && i != 4 && i !=5) continue;


                //sheet = hssfwb.GetSheet(cboSheet.EditValue.ToString());
                if (file.CanRead == false)
                    file = new FileStream(fileName, FileMode.Open, FileAccess.Read);

                if (extension == ".xls")
                {
                    hssfwb = new HSSFWorkbook(file);

                    sheet = hssfwb.GetSheet(sheetName);
                }
                else if (extension == ".xlsx")
                {
                    xssfwb = new XSSFWorkbook(file);

                    sheet = xssfwb.GetSheet(sheetName);
                }

                int j = 0;
                IEnumerator rows = sheet.GetRowEnumerator();

                while (rows.MoveNext())
                {
                    j++;
                    IRow row = (IRow)rows.Current;
                    if (j == dateRow)
                    {
                        for (int k = 0; k < row.Count(); k++)
                        {
                            DateTime date = new DateTime();
                            string dateValue = ConertStringCellValue(row.Cells[k]);
                            if (DateTime.TryParse(dateValue, out date))
                            {
                                CultureInfo cultureInfo = CultureInfo.CurrentCulture;
                                CalendarWeekRule calendarWeekRule = cultureInfo.DateTimeFormat.CalendarWeekRule;
                                DayOfWeek firstDayOfWeek = cultureInfo.DateTimeFormat.FirstDayOfWeek;

                                int week = cultureInfo.Calendar.GetWeekOfYear(date, calendarWeekRule, firstDayOfWeek);
                                int month = date.Month;
                                dt.Columns.Add(date.ToString("yyyy-MM-dd"));
                                //dt.Rows.Add(null, date.ToString("yyyy-MM-dd"), week.ToString(), month.ToString(), null);
                            }
                        }
                    }
                    else if (j > dateRow + 1)
                    {
                        if (row.Cells.Count == 0|| string.IsNullOrWhiteSpace(row.Cells[0].StringCellValue))
                            break;
                        string productdefid = ConertStringCellValue(row.GetCell(ProductIdx, MissingCellPolicy.CREATE_NULL_AS_BLANK));
                        string FocastSalseOrder = ConertStringCellValue(row.GetCell(3, MissingCellPolicy.CREATE_NULL_AS_BLANK));
                        bool checkSo = true;

                        ////if (!FocastSalseOrder.Equals("수주예상") && UserInfo.Current.Enterprise.Equals(Constants.EnterPrise_InterFlex))
                        //checkSo = false;

                        if (!string.IsNullOrEmpty(productdefid) && checkSo)
                        {
                            dt.Rows.Add(productdefid);
                            for (int k = 1; k < dt.Columns.Count - 1; k++)
                            {
                                string value = ConertStringCellValue(row.GetCell(k + dateStartIdx, MissingCellPolicy.CREATE_NULL_AS_BLANK));
                                if (!string.IsNullOrWhiteSpace(value) && !value.Equals("0"))
                                {
                                    dt.Rows[dt.Rows.Count - 1][k] = value;
                                }
                            }
                        }
                    }
                }
                ds.Tables.Add(dt);
            }

            return ds;
        }
        private void ConertRuleMessage(DataSet ds)
        {
            DataTable dtSend = new DataTable();
            dtSend.Columns.Add("PRODUCTDEFID");
            dtSend.Columns.Add("DATE");
            dtSend.Columns.Add("WEEK");
            dtSend.Columns.Add("MONTH");
            dtSend.Columns.Add("QTY");

            foreach (DataTable dt in ds.Tables)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string productdefid = Format.GetFullTrimString(dt.Rows[i]["PRODUCTDEFID"]);

                    for (int j = 1; j < dt.Columns.Count; j++)
                    {

                        string date = dt.Columns[j].ColumnName;
                        string qty = Format.GetFullTrimString(dt.Rows[i][j]);
                        int week = 0;
                        int month = 0;
                        DateTime dtdate = new DateTime();
                        if (DateTime.TryParse(date, out dtdate))
                        {
                            CultureInfo cultureInfo = CultureInfo.CurrentCulture;
                            CalendarWeekRule calendarWeekRule = cultureInfo.DateTimeFormat.CalendarWeekRule;
                            DayOfWeek firstDayOfWeek = cultureInfo.DateTimeFormat.FirstDayOfWeek;

                            week = cultureInfo.Calendar.GetWeekOfYear(dtdate, calendarWeekRule, firstDayOfWeek);
                            month = dtdate.Month;
                        }
                        if (!qty.Equals(string.Empty) && !qty.Equals("0"))
                        {
                            dtSend.Rows.Add(productdefid, date, week.ToString(), month.ToString(), qty);
                        }
                    }
                }
            }

            saveExcelData(dtSend);
        }
        private void saveExcelData(DataTable dt)
        {
            MessageWorker worker = new MessageWorker("SaveProductPlan");
            worker.SetBody(new MessageBody()
            {
                { "EnterpriseID", UserInfo.Current.Enterprise },
                { "PlantID", UserInfo.Current.Plant },
                { "UserId", UserInfo.Current.Id },
                { "PlanList", dt },
            });

            worker.Execute();
        }
        private  string ConertStringCellValue(ICell cell)
        {
            object value = null;

            switch(cell.CellType)
            {
                case CellType.Unknown:
                    break;
                case CellType.Numeric:
                    if (cell.CellStyle.GetDataFormatString() == "m/d/yy")
                    {
                        value = cell.DateCellValue;
                    }
                    else
                    {
                        value = cell.NumericCellValue;
                    }
                    break;
                case CellType.String:
                    value = cell.StringCellValue;
                    break;
                case CellType.Formula:
                    break;
                case CellType.Blank:
                    break;
                case CellType.Boolean:
                    break;
                case CellType.Error:
                    break;

            }

            if (value != null)
                return value.ToString();
            else return string.Empty;

        }
        private void GbPivot_HeaderButtonClickEvent(object sender, HeaderButtonClickArgs args)
        {
            if(args.ClickItem.Equals(GridButtonItem.Export))
            {

                SaveFileDialog opf = new SaveFileDialog();
                opf.Filter = "Excel Files(*.xlsx)|*.xlsx;*.xls";

                DevExpress.XtraPivotGrid.PivotXlsxExportOptions exOpt = new DevExpress.XtraPivotGrid.PivotXlsxExportOptions();
                exOpt.ExportType = DevExpress.Export.ExportType.WYSIWYG;
                exOpt.ShowGridLines = true;

                opf.ShowDialog();
                if(opf.FileName.Length > 0)
                {
                    pvProductPlan.ExportToXlsx(opf.FileName, exOpt);
                }
               
            }
        }


        #endregion

        #region ◆ 툴바 |
        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            base.OnToolbarClick(sender, e);

            SmartButton btn = sender as SmartButton;
            if (btn == null)
                return;

            if (btn.Name.ToUpper().Equals("EXCELIMPORT"))
            {
                OpenFileDialog of = new OpenFileDialog();
                of.Filter = "Excel Files|*.xls;*.xlsx";
                of.ShowDialog();

                string connectionString = string.Empty;
                string fileName = of.FileName;

                if (!string.IsNullOrWhiteSpace(fileName))
                {
                    string extention = Path.GetExtension(fileName);

                    if (extention.Equals(".xls") || extention.Equals(".xlsx"))
                    {
                        this.ShowWaitArea();
                        try
                        {
                            DataTable dtSheet = new DataTable();
                            dtSheet.Columns.Add("SheetName", typeof(string));

                            switch (extention)
                            {
                                case ".xls":
                                    using (FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                                    {
                                        hssfwb = new HSSFWorkbook(file);
                                    }

                                    for (int i = 0; i < hssfwb.NumberOfSheets; i++)
                                    {
                                        string sheetName = hssfwb.GetSheetName(i);

                                        dtSheet.Rows.Add(sheetName);
                                    }
                                    break;
                                case ".xlsx":
                                    using (FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                                    {
                                        xssfwb = new XSSFWorkbook(file);
                                    }

                                    for (int i = 0; i < xssfwb.NumberOfSheets; i++)
                                    {
                                        string sheetName = xssfwb.GetSheetName(i);

                                        dtSheet.Rows.Add(sheetName);
                                    }
                                    break;
                                case ".xlsb":
                                    using (FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                                    {
                                        xssfwb = new XSSFWorkbook(file);
                                    }

                                    for (int i = 0; i < xssfwb.NumberOfSheets; i++)
                                    {
                                        string sheetName = xssfwb.GetSheetName(i);

                                        dtSheet.Rows.Add(sheetName);
                                    }
                                    break;
                            }

                            DataSet dsFcst = GetSheetDate(dtSheet, fileName);
                            ConertRuleMessage(dsFcst);
                            this.CloseWaitArea();
                            ShowMessage("SuccedSave");
                        }
                        finally
                        {
                            this.CloseWaitArea();
                        }
                    }
                }
            }
        }
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
            this.grdFcst.DataSource = null;

            SmartSelectPopupEdit pop = Conditions.GetControl<SmartSelectPopupEdit>("WEEK");

            string year = pop.EditValue.ToString().Split('-')[0];
            string week = pop.EditValue.ToString().Split('-')[1];

         


            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("YEAR", year);
            param.Add("WEEK", week);

            DataTable dtDate = await SqlExecuter.QueryAsync("SelectSalespoforcastRegWeekDay", "10001", param);

            if( dtDate == null || dtDate.Rows.Count == 0)
            {
                ShowMessage("CheckRegistWeek");

                return;

            }

            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("P_PERIOD_PERIODFR", dtDate.Rows[0]["DATE"].ToString());
            values.Add("P_PERIOD_PERIODTO", dtDate.Rows[dtDate.Rows.Count -1]["DATE"].ToString());


            List<string> listDate = new List<string>();

            foreach (DataRow dr in dtDate.Rows)
            {
                listDate.Add(dr["DATE"].ToString());
            }


            string valuePart = string.Empty;
            string fieldPart = string.Empty;
            string selectPart = string.Empty;

            grdFcst.View.ClearColumns();
            var group = grdFcst.View.AddGroupColumn("");
            group.AddTextBoxColumn("LOTTYPE", 70).SetTextAlignment(TextAlignment.Center);
            group.AddTextBoxColumn("PRODUCTDEFID", 130);
            group.AddTextBoxColumn("PRODUCTDEFNAME", 240);
            group.AddTextBoxColumn("PCSPNL", 90).SetTextAlignment(TextAlignment.Right);
            group.AddTextBoxColumn("CUSTOMER", 100);

            foreach (string date in listDate)
            {
                valuePart += "(''" + date + "''),";
                fieldPart += "\"" + date + "\" NUMERIC, ";
                selectPart += "\"" + date + "\" AS \"" + date + "_PCS\",";
                selectPart += "CEILING(\"" + date + "\" / NULLIF(PD.PCSPNL, 0)) AS \"" + date + "_PNL\",";

                var groupEachDay = grdFcst.View.AddGroupColumn(date);

                var pcsCol = groupEachDay.AddSpinEditColumn(date + "_PCS", 100)
                    .SetTextAlignment(TextAlignment.Right)
                    .SetDisplayFormat("#,##0")
                    .SetIsReadOnly();
                pcsCol.LanguageKey = "PCS";

                var pnlCol = groupEachDay.AddSpinEditColumn(date + "_PNL", 100)
                    .SetTextAlignment(TextAlignment.Right)
                    .SetDisplayFormat("#,##0")
                    .SetIsReadOnly();
                pnlCol.LanguageKey = "PNL";
            }

            grdFcst.View.PopulateColumns();

            valuePart = "values" + valuePart.TrimEnd(',');
            fieldPart = fieldPart.Trim().TrimEnd(',');
            selectPart = selectPart.Trim().TrimEnd(',');

            values.Add("P_VALUES", valuePart);
            values.Add("P_FIELD", fieldPart);
            values.Add("P_SELECT", selectPart);

            DataTable dtProductPlan = null;
            DataTable dtPivot = null;

     
            if (tabMain.SelectedTabPage.Name.Equals("tbProductPlan"))
            {
                dtProductPlan = await SqlExecuter.QueryAsync("selectProductPlan", "10001", values);
                if (dtProductPlan.Rows.Count < 1)
                {
                    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                }
                grdFcst.DataSource = dtProductPlan;
            }
            else
            {
                dtPivot = await SqlExecuter.QueryAsync("selectProductPlanForPivot", "10001", values);
                if (dtPivot.Rows.Count < 1)
                {
                    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                }
                pvProductPlan.DataSource = dtPivot;
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

            DataRow row = grdFcst.View.GetFocusedDataRow();
            var param = row.Table.Columns
                .Cast<DataColumn>()
                .ToDictionary(col => col.ColumnName, col => row[col.ColumnName]);

            //메뉴 호출			
            OpenMenu(menuId, param);
        }
        #endregion
    }
}
