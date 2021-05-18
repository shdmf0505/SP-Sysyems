#region using
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Micube.Framework.Net;
using Micube.Framework;
using Micube.Framework.SmartControls;
using System.Text.RegularExpressions;
using Micube.Framework.SmartControls.Validations;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using System.IO;
using DevExpress.Utils.Drawing;
using DevExpress.XtraEditors.Repository;
using DevExpress.Utils;
using System.Net;
using Micube.SmartMES.Commons;
#endregion

namespace Micube.SmartMES.ProcessManagement
{
	/// <summary>
	/// 프로그램명 :  공정관리 > 공정작업 > 입고검사등록
	/// 업 무 설명 :  공정 불량 리스트 UserControl
	/// 생  성  자 :  정승원
	/// 생  성  일 :  2019-06-10
	/// 수정 이 력 : 
	/// 
	/// 
	/// </summary>
	public partial class DefectProcessListControl : UserControl
	{
		//PNL, PCS 수량 변경 핸들러
		private delegate bool QtyHandler(decimal inputQty);

		/// <summary>
		/// PNL, PCS 수량 변경 이벤트
		/// </summary>
		class Quantity
		{
			public event QtyHandler ChangeQtyValue;

			public bool Change(decimal inputQty)
			{
				bool isSuccess = ChangeQtyValue(inputQty);
				return isSuccess;
			}
		}

		#region Local Variables
		private DataTable _fileData = new DataTable();
		//검사정의 ID
		private string _inspectionDefId = string.Empty;
		//Selected Lot ID
		private string _selectedLotId = string.Empty;
		//기준 (PCS / PNL) 수량
		private decimal _pcsPnl = 0;
		//불량 Lot 수량(Calculate Defective Rate)
		private int _pcsQty = 0;
		//수량 변경 이벤트 객체
		private Quantity _quantity = null;
		
		//화면에서 수량 입력 후 focuse out 되지 않은 상태에서 저장 눌렀을때 저장 탈출용
		private bool _isSaved = true;
		public bool IsSaved
		{
			get { return _isSaved; }
			set { _isSaved = value; }
		}
		/// <summary>
		/// PNL, PCS 입력 수량 핸들러 
		/// </summary>
		/// <param name="pnlQty">PNL 수량</param>
		/// <param name="pcsQty">PCS 수량</param>
		/// <param name="e"></param>
		/// <returns></returns>
		public delegate void EditValueChangeHandler(decimal pnlQty , int pcsQty, out CancelEventArgs e);
		public event EditValueChangeHandler EditValueChange;

		#endregion

		#region 생성자
		public DefectProcessListControl()
		{
			InitializeComponent();

			if (!this.IsDesignMode())
			{
                picDefectPhoto.Properties.ShowMenu = false;

                InitializeEvent();
				InitializeGrid();
				InitializeFileGrid();
			}
		}
		#endregion

		#region 컨텐츠 영역 초기화
		/// <summary>
		/// 그리드 초기화
		/// </summary>
		private void InitializeGrid()
		{
			if (!this.IsDesignMode())
			{
				grdInspectionList.VisibleLotId = false;
				grdInspectionList.VisibleFileUpLoad = true;
				grdInspectionList.VisibleDefectDgree = true;
				grdInspectionList.InitializeControls();
				grdInspectionList.InitializeEvent();

			}
		}

		/// <summary>
		/// 파일 그리드 초기화
		/// </summary>
		private void InitializeFileGrid()
		{
			grdFileList.GridButtonItem = GridButtonItem.None;
			grdFileList.View.SetIsReadOnly();
			grdFileList.ShowStatusBar = false;

			grdFileList.View.AddTextBoxColumn("FILENAME", 150);
			grdFileList.View.AddTextBoxColumn("FILEEXT", 60).SetTextAlignment(TextAlignment.Center);
			grdFileList.View.AddSpinEditColumn("FILESIZE", 60).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetTextAlignment(TextAlignment.Right);
			grdFileList.View.AddTextBoxColumn("FILEPATH", 100).SetIsHidden();
			grdFileList.View.AddTextBoxColumn("URL").SetIsHidden();

			grdFileList.View.PopulateColumns();

			_fileData = (grdFileList.DataSource as DataTable).Clone();

            grdInspectionList.VisibleDefectDgree = true;
            grdInspectionList.DefectUOM = "ALL";
		}
        #endregion

        #region Event

        /// <summary>
        /// 이벤트 초기화
        /// </summary>
        private void InitializeEvent()
		{
            _quantity = new Quantity();
            _quantity.ChangeQtyValue += ChangePcsQty;
            _quantity.ChangeQtyValue += ChangePnlQty;

            //grdInspectionList.View.GridCellButtonClickEvent += View_GridCellButtonClickEvent;
            //grdInspectionList.View.CellValueChanged += View_CellValueChanged;
            grdInspectionList.DefectFileInfoChanged += GrdInspectionList_DefectFileInfoChanged;
            grdInspectionList.DefectQtyChanged += GrdDefect_DefectQtyChanged;
            grdInspectionList.View.AddingNewRow += View_AddingNewRow;
			//grdInspectionList.View.ShowingEditor += View_ShowingEditor;
            grdInspectionList.View.CustomDrawFooterCell += View_CustomDrawFooterCell;
			grdInspectionList.View.FocusedRowChanged += View_FocusedRowChanged1;

			//파일
			grdFileList.View.FocusedRowChanged += View_FocusedRowChanged;

			grdInspectionList.View.GridControl.ToolTipController = new ToolTipController();
			grdInspectionList.View.GridControl.ToolTipController.GetActiveObjectInfo += ToolTipController_GetActiveObjectInfo;
		}

        private void GrdInspectionList_DefectFileInfoChanged(object sender, EventArgs e)
        {
            if (grdInspectionList.View.FocusedRowHandle < 0) return;

            FocusedDataBindOfFileInfo();
        }

        #region - ToolTipController_GetActiveObjectInfo :: 미리보기 VIEW |
        /// <summary>
        /// 미리보기 VIEW
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolTipController_GetActiveObjectInfo(object sender, DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventArgs e)
        {
            if (!e.SelectedControl.Equals(grdInspectionList))
            {
                var hitInfo = grdInspectionList.View.CalcHitInfo(e.ControlMousePosition);
                if (!hitInfo.InRowCell)
                {
                    return;
                }

                if (hitInfo.Column.FieldName.Equals("IMAGE") 
				&& (grdInspectionList.View.GetRow(hitInfo.RowHandle) as DataRowView).Row["ISUPLOAD"].Equals("Y")
				&& !string.IsNullOrWhiteSpace(Format.GetTrimString((grdInspectionList.View.GetRow(hitInfo.RowHandle) as DataRowView).Row["FILENAME"])) 
				&& !string.IsNullOrWhiteSpace(Format.GetTrimString((grdInspectionList.View.GetRow(hitInfo.RowHandle) as DataRowView).Row["FILEEXT"]))
				&& Format.GetDecimal((grdInspectionList.View.GetRow(hitInfo.RowHandle) as DataRowView).Row["QTY"]) > 0)
                {
                    DataRow dr = (grdInspectionList.View.GetRow(hitInfo.RowHandle) as DataRowView).Row;

                    try
                    {
                        string[] fileNames = Format.GetString(dr["FILENAME"]).Split(',');
                        string[] fileExts = Format.GetString(dr["FILEEXT"]).Split(',');

                        ToolTipControlInfo toolTipControlInfo = new ToolTipControlInfo(hitInfo.RowHandle, "FILENAME");
                        SuperToolTip superToolTip = new SuperToolTip();

						//FILE NAME이 인덱스 1부터 시작함
						if(fileNames.Length < 1 || string.IsNullOrWhiteSpace(fileNames[1])) return;

						//superToolTip.Items.AddTitle(fileNames[1]);
                        for (int i = 1; i < fileNames.Length; i++)
                        {
							if (string.IsNullOrEmpty(fileNames[i]) || string.IsNullOrWhiteSpace(fileNames[i])) return;
							if (string.IsNullOrEmpty(fileExts[i]) || string.IsNullOrWhiteSpace(fileExts[i])) return;

							//string url = AppConfiguration.GetString("Application.SmartDeploy.Url") + Format.GetString(dr["FILEPATH"]) + "/" + fileNames[i] + "." + fileExts[i];
							//WebClient wc = new WebClient();
							//byte[] bytes = wc.DownloadData(url);
							// TODO : 파일 업로드 경로 변경 (SmartDeploy -> NAS FTP) 시 수정
							string filePath = Format.GetString(dr["FILEPATH"]);
							if(string.IsNullOrWhiteSpace(filePath)) return;

							if(fileNames.Length < i || fileExts.Length < i) return;

                            string fileName = string.Join(".", fileNames[i], fileExts[i]);
                            byte[] bytes = CommonFunction.GetFtpImageFileToByte(filePath, fileName);

                            List<string> imgTypes = new List<string>{ "jpg", "jpeg", "bmp", "gif", "png" };

							superToolTip.Items.AddTitle(fileNames[i]);
							if (imgTypes.Contains(fileExts[i]))
                            {
								superToolTip.Items.Add(new ToolTipItem()
                                {
                                    Image = new Bitmap((Bitmap)new ImageConverter().ConvertFrom(bytes), new Size(300, 300))
                                });
                            }
                        }

                        toolTipControlInfo.SuperTip = superToolTip;
                        e.Info = toolTipControlInfo;
                    }
                    catch (Exception ex)
                    {
                        throw MessageException.Create(ex.ToString());
                    }
                }


            }
        }
        #endregion

        #region - View_FocusedRowChanged1 :: 해당 ROW 포커스 시 파일 VIEW 그리드에 FILE 정보 바인드 |
        /// <summary>
        /// 해당 ROW 포커스 시 파일 VIEW 그리드에 FILE 정보 바인드
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_FocusedRowChanged1(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grdInspectionList.View.FocusedRowHandle < 0) return;

            FocusedDataBindOfFileInfo();
        }
        #endregion

        #region - View_FocusedRowChanged :: 파일 View |
        // <summary>
        /// 파일 View
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grdFileList.View.FocusedRowHandle < 0) return;

            //picDefectPhoto.LoadAsync(Format.GetString(grdFileList.View.GetFocusedRowCellValue("URL")));
            // TODO : 파일 업로드 경로 변경 (SmartDeploy -> NAS FTP) 시 수정
            string filePath = Format.GetString(grdFileList.View.GetFocusedRowCellValue("FILEPATH"));
            string fileName = string.Join(".", Format.GetString(grdFileList.View.GetFocusedRowCellValue("FILENAME")), Format.GetString(grdFileList.View.GetFocusedRowCellValue("FILEEXT")));

            picDefectPhoto.EditValue = CommonFunction.GetFtpImageFileToByte(filePath, fileName);
        }
        #endregion

        #region - View_CustomDrawFooterCell :: PNL QTY 소수점 반올림 |
        /// <summary>
        /// PNL QTY 소수점 반올림
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CustomDrawFooterCell(object sender, FooterCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName == "PNLQTY")
            {
                e.Info.DisplayText = Math.Ceiling(Format.GetDouble(e.Info.Value, 0.00)).ToString();
            }
        }
        #endregion

        #region - Defect 수량 Changed Event |
        /// <summary>
        /// Defect 수량 Changed Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdDefect_DefectQtyChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName.Equals("QTY"))
            {
                decimal pcsQty = Format.GetDecimal(e.Value, 0);

                grdInspectionList.DefectQtyChanged -= GrdDefect_DefectQtyChanged;
                _quantity.ChangeQtyValue -= ChangePnlQty;

                bool isCalculationSuccess = _quantity.Change(pcsQty);
                //row["QTY"] = isCalculationSuccess ? pcsQty : 0;

                grdInspectionList.DefectQtyChanged += GrdDefect_DefectQtyChanged;
                _quantity.ChangeQtyValue += ChangePnlQty;
            }
            //변경 셀 = PNL
            if (e.Column.FieldName.Equals("PNLQTY"))
            {
                decimal pnlQty = Format.GetDecimal(e.Value, 0);

                grdInspectionList.DefectQtyChanged -= GrdDefect_DefectQtyChanged;
                _quantity.ChangeQtyValue -= ChangePcsQty;

                bool isCalculationSuccess = _quantity.Change(pnlQty);

                grdInspectionList.DefectQtyChanged += GrdDefect_DefectQtyChanged;
                _quantity.ChangeQtyValue += ChangePcsQty;

            }
        }
        #endregion

        /// <summary>
        /// 셀 ReadOnly 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_ShowingEditor(object sender, CancelEventArgs e)
		{
			DataRow row = grdInspectionList.View.GetFocusedDataRow();
			if(row.RowState != DataRowState.Added)
			{
				GridView view = sender as GridView;

				if (view.FocusedColumn.FieldName.Equals("DEFECTCODENAME") ||
				view.FocusedColumn.FieldName.Equals("PROCESSSEGMENTCLASSID") ||
				view.FocusedColumn.FieldName.Equals("UNIT"))
				{
					e.Cancel = true;
				}
			}
		}

        #region - View_GridCellButtonClickEvent :: 사진 등록 이벤트 |
        /// <summary>
        /// 사진 등록 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_GridCellButtonClickEvent(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.GridCellButtonClickEventArgs args)
        {
            DataRow row = grdInspectionList.View.GetFocusedDataRow();

            int iQty = Format.GetInteger(row["QTY"]);

            if (iQty == 0)
            {
                throw MessageException.Create("CheckRegDefectQty");
            }

            if (row["ISUPLOAD"].ToString().Equals("N"))
            {
                AddImagePopup imagePopup = new AddImagePopup(_inspectionDefId);
                imagePopup.FileInfo += (dtFileInfo) =>
                {
                    string imageName = string.Empty;
                    string imageExt = string.Empty;
                    string imageSize = string.Empty;

                    foreach (DataRow fileRow in dtFileInfo.Rows)
                    {
                        row["IMAGERESOURCEID"] = fileRow["RESOURCEID"].ToString();
                        row["FILEPATH"] = fileRow["FILEPATH"].ToString();
                        imageName += "," + fileRow["FILENAME"].ToString();
                        imageExt += "," + fileRow["FILEEXT"].ToString();
                        imageSize += "," + fileRow["FILESIZE"].ToString();

                        //파일 뷰
                        DataRow addRow = _fileData.NewRow();
                        addRow["FILENAME"] = fileRow["FILENAME"];
                        addRow["FILEEXT"] = fileRow["FILEEXT"];
                        addRow["FILESIZE"] = fileRow["FILESIZE"];
                        addRow["FILEPATH"] = fileRow["FILEPATH"];

                        addRow["URL"] = AppConfiguration.GetString("Application.SmartDeploy.Url") + fileRow["FILEPATH"].ToString() + "/" + fileRow["FILENAME"].ToString() + "." + fileRow["FILEEXT"].ToString();
                        _fileData.Rows.Add(addRow);

                        grdFileList.DataSource = _fileData;
                    }

                    row["FILENAME"] = imageName;
                    row["FILEEXT"] = imageExt;
                    row["FILESIZE"] = imageSize;
                    row["ISUPLOAD"] = "Y";

                };
                imagePopup.ShowDialog();
            }
            else
            {
                //업로드된 사진 view
                //추가 수정 삭제 가능
                DataRow selectRow = grdInspectionList.View.GetFocusedDataRow();

                ModifiyImagePopup imagePopup = new ModifiyImagePopup(_inspectionDefId, selectRow);
                imagePopup.FileInfo += (dtFileInfo, isAdd) =>
                {
                    string fileName = string.Empty;
                    string fileExt = string.Empty;
                    string fileSize = string.Empty;
                    DataRow select = grdInspectionList.View.GetFocusedDataRow();
                    foreach (DataRow r in dtFileInfo.Rows)
                    {
                        if (isAdd)
                        {
                            select["FILENAME"] += "," + Format.GetString(r["FILENAME"]);
                            select["FILEEXT"] += "," + Format.GetString(r["FILEEXT"]);
                            select["FILESIZE"] += "," + Format.GetString(r["FILESIZE"]);
                        }
                        else
                        {
                            fileName += "," + Format.GetString(r["FILENAME"]);
                            fileExt += "," + Format.GetString(r["FILEEXT"]);
                            fileSize += "," + Format.GetString(r["FILESIZE"]);
                        }
                    }

                    if (dtFileInfo.Rows.Count < 1)
                    {
                        select["ISUPLOAD"] = "N";
                        select["FILENAME"] = "";
                        select["FILEEXT"] = "";
                        select["FILESIZE"] = "";
                        select["FILEPATH"] = "";
                        select["IMAGERESOURCEID"] = "";
                    }
                    else if (!isAdd && dtFileInfo.Rows.Count >= 1)
                    {
                        select["FILENAME"] = fileName;
                        select["FILEEXT"] = fileExt;
                        select["FILESIZE"] = fileSize;
                    }

                    FocusedDataBindOfFileInfo();
                };
                imagePopup.ShowDialog();
            }
        }
        #endregion

        #region - View_CustomSummaryCalculate :: 불량률 계산 |
        /// <summary>
        /// 불량률 계산
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            if (e.IsTotalSummary)
            {
                GridSummaryItem item = e.Item as GridSummaryItem;
                if (item.FieldName == "DEFECTRATE")
                {
                    decimal pnlQty = 0;
                    decimal pcsQty = 0;
                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                    {
                        pnlQty = Format.GetDecimal((sender as GridView).Columns["PNLQTY"].SummaryItem.SummaryValue);
                        pcsQty = Format.GetDecimal((sender as GridView).Columns["QTY"].SummaryItem.SummaryValue);
                        if (pnlQty != 0 && pcsQty != 0)
                        {
                            e.TotalValue = SetDefectiveRate(pcsQty);
                        }
                    }
                }
            }
        }
        #endregion

		/// <summary>
		/// 그리드 Row 추가 시 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
		{
			if(string.IsNullOrEmpty(_selectedLotId) || _pcsPnl.Equals(0))
			{
				throw MessageException.Create("NotSeletedLot");
			}
			
			decimal panelQty = 0;
			decimal qty = 0;

			args.NewRow["LOTID"] = _selectedLotId;
			args.NewRow["UNIT"] = "PCS";
			args.NewRow["PNLQTY"] = panelQty;
			args.NewRow["QTY"] = qty;
			args.NewRow["DEFECTRATE"] = 0;
			args.NewRow["ISUPLOAD"] = "N";

            //grdInspectionList.SetConsumableDefComboBox();
        }

        #endregion

        #region Public Function

        public void SetDefectGridComboData(string lotId)
        {
            grdInspectionList.SetConsumableDefComboBox(lotId);
        }

        #endregion

        #region Private Function

        /// <summary>
        /// Get Grid Datasource
        /// </summary>
        /// <returns></returns>
        public DataTable GetGridDataSource()
		{
			DataTable dt = grdInspectionList.DataSource as DataTable;

			if (dt.Rows.Count < 0)
			{
				return null;
			}

			return dt;
		}

		/// <summary>
		/// Grid 클리어 
		/// </summary>
		public void SetGridClear()
		{
			grdInspectionList.View.ClearDatas();
			grdFileList.View.ClearDatas();
			picDefectPhoto.EditValue = null;
		}

		/// <summary>
		/// 
		/// </summary>
		public void SetGridAllQtyClear()
		{
			DataTable dataTable = grdInspectionList.DataSource as DataTable;
			dataTable.Rows.Cast<DataRow>().ForEach(row =>
			{
				row["PNLQTY"] = 0;
				row["QTY"] = 0;
				row["DEFECTRATE"] = 0;
			});
		}

		/// <summary>
		/// Grid Qty만 클리어
		/// </summary>
		public void SetGridQtyClear()
		{
			DataTable dataTable = grdInspectionList.DataSource as DataTable;
			dataTable.Rows.Cast<DataRow>().ForEach(row => 
			{
				row["PNLQTY"] = 0;
				row["QTY"] = 0;
				row["DEFECTRATE"] = 0;
			});
		}

		/// <summary>
		/// 메인 그리드에서 선택한 LOT ID로 조회한 공정불량 리스트 bind
		/// </summary>
		/// <param name="dtResult"></param>
		/// <param name="lotId"></param>
		public void SetResultData(string lotId, decimal pcsqty, decimal pcspnl, DataTable dtResult, string inspectionDefId)
		{
			_selectedLotId = lotId;
			_pcsPnl = pcspnl;
			_inspectionDefId = inspectionDefId;

            grdInspectionList.SetInspectionDefinitionId(_inspectionDefId);
            grdInspectionList.VisibleFileUpLoad = true;

            

			DataTable dt = new DataTable();
			dt = dtResult.Copy();

            //원인품목
			dt.Columns.Add("SPLITCONSUMABLEDEFIDVERSION", typeof(string));
            //dt.Columns.Add("REASONCONSUMABLEDEFIDVERSION", typeof(string));
			dt.Columns.Add("REASONCONSUMABLEDEFID", typeof(string));
            dt.Columns.Add("REASONCONSUMABLEDEFVERSION", typeof(string));
            //원인자재LOTID
            dt.Columns.Add("REASONCONSUMABLELOTID", typeof(string));
            //원인공정
            dt.Columns.Add("REASONPROCESSSEGMENTID", typeof(string));
            //원인작업장
            dt.Columns.Add("REASONAREAID", typeof(string));
            dt.Columns.Add("REASONAREANAME", typeof(string));
            //이미지
            dt.Columns.Add("IMAGE", typeof(string));
			//이미지 DATA
			dt.Columns.Add("IMAGEDATA", typeof(string));
			//이미지 PATH
			dt.Columns.Add("FILEPATH", typeof(string));
			//이미지 NAME
			dt.Columns.Add("FILENAME", typeof(string));
			//이미지 EXT
			dt.Columns.Add("FILEEXT", typeof(string));
			//이미지 SIZE
			dt.Columns.Add("FILESIZE", typeof(string));
			//이미지 ResourceId
			dt.Columns.Add("IMAGERESOURCEID", typeof(string));

			grdInspectionList.DataSource = dt;

            

            grdInspectionList.SetInfo(pcspnl, pcsqty);
            grdInspectionList.SetConsumableDefComboBox(lotId);
        }

		/// <summary>
		/// 검사 수량(pcs)바인드
		/// </summary>
		public void SetInspectQtyDataBind(int inspectQty)
		{
			_pcsQty = inspectQty;
		}

		/// <summary>
		/// 숫자, 소수점 유효성 검사
		/// </summary>
		/// <returns></returns>
		private IEnumerable<ValidationResult> DecimalPointValidation(int row)
		{
			List<ValidationResult> result = new List<ValidationResult>();

			var currentRow = grdInspectionList.View.GetFocusedDataRow();

			string pnlQty = currentRow["PNLQTY"].ToString();
			string pcsQty = currentRow["QTY"].ToString();
			Regex regex = new Regex(@"^([0-9]*)[\.]?([0-9])?$");

			if (!regex.IsMatch(pcsQty))
			{
				ValidationResult resultLsl = new ValidationResult();
				resultLsl.Caption = Language.Get("PCS");
				resultLsl.FailMessage = Language.GetMessage("OnlyDecimal").Message;
				resultLsl.Id = "PCS";
				resultLsl.IsSucced = false;

				result.Add(resultLsl);
			}


			if (!regex.IsMatch(pnlQty))
			{
				ValidationResult resultLsl = new ValidationResult();
				resultLsl.Caption = Language.Get("PNL");
				resultLsl.FailMessage = Language.GetMessage("OnlyDecimal").Message;
				resultLsl.Id = "PNL";
				resultLsl.IsSucced = false;

				result.Add(resultLsl);
			}

			return result;
		}

		/// <summary>
		/// 불량율 계산
		/// </summary>
		/// <param name="pcsQty"></param>
		/// <returns></returns>
		private decimal SetDefectiveRate(decimal pcsQty)
		{
			decimal rate = 0;
			if ( !((double.IsNaN((double)pcsQty) || pcsQty < 0) && !(double.IsNaN((double)_pcsQty) || _pcsQty < 0)) )
			{
				rate = (pcsQty / _pcsQty) * 100;
			}
			
			return rate;
		}

        /// <summary>
		/// PCS 수량 변경
		/// </summary>
		/// <param name="pcsQty">PCS 변경 수량</param>
		private bool ChangePcsQty(decimal pcsQty)
        {
            if (string.IsNullOrEmpty(_selectedLotId) || _pcsPnl.Equals(0)) return false;

            decimal pnlQty = (int)pcsQty / _pcsPnl;

            grdInspectionList.View.SetFocusedRowCellValue("PNLQTY", pnlQty);

            DataTable currentTable = grdInspectionList.DataSource as DataTable;

            decimal sumPnlQty = currentTable.AsEnumerable().Sum(x => Format.GetDecimal(x["PNLQTY"]));
            int sumPcsQty = currentTable.AsEnumerable().Sum(x => Format.GetInteger(x["QTY"]));

            sumPnlQty = Math.Ceiling(sumPnlQty);

            CancelEventArgs args = new CancelEventArgs();
            this.EditValueChange(sumPnlQty, sumPcsQty, out args);
            if (args.Cancel)
            {
				grdInspectionList.View.SetFocusedRowCellValue("PNLQTY", 0);
                grdInspectionList.View.SetFocusedRowCellValue("QTY", 0);
                grdInspectionList.View.SetFocusedRowCellValue("DEFECTRATE", 0);

				_isSaved = false;

				return false;
            }
            else
            {
                //불량률 추가
                decimal defectiveRate = SetDefectiveRate(pcsQty);
                grdInspectionList.View.SetFocusedRowCellValue("DEFECTRATE", defectiveRate);

				_isSaved = true;

				return true;
            }

        }

        /// <summary>
        /// PNL 수량 변경
        /// </summary>
        /// <param name="pnlQty">PNL 변경 수량</param>
        private bool ChangePnlQty(decimal pnlQty)
        {
            if (string.IsNullOrEmpty(_selectedLotId) || _pcsPnl.Equals(0)) return false;

            int pcsQty = (int)(pnlQty * _pcsPnl);

            grdInspectionList.View.SetFocusedRowCellValue("QTY", pcsQty);

            DataTable currentTable = grdInspectionList.DataSource as DataTable;

            decimal sumPnlQty = currentTable.AsEnumerable().Sum(x => Format.GetDecimal(x["PNLQTY"]));
            int sumPcsQty = currentTable.AsEnumerable().Sum(x => Format.GetInteger(x["QTY"]));

            sumPnlQty = Math.Ceiling(sumPnlQty);

            CancelEventArgs args = new CancelEventArgs();
            this.EditValueChange(sumPnlQty, sumPcsQty, out args);
            if (args.Cancel)
            {
                grdInspectionList.View.SetFocusedRowCellValue("PNLQTY", 0);
                grdInspectionList.View.SetFocusedRowCellValue("QTY", 0);
                grdInspectionList.View.SetFocusedRowCellValue("DEFECTRATE", 0);

				_isSaved = false;

				return false;
            }
            else
            {
                //불량률 추가
                decimal defectiveRate = SetDefectiveRate(pcsQty);
                grdInspectionList.View.SetFocusedRowCellValue("DEFECTRATE", defectiveRate);

				_isSaved = true;

				return true;
            }
        }

        /// <summary>
        /// 이미지 Byte 변환
        /// </summary>
        /// <param name="fileFullPath"></param>
        /// <returns></returns>
        private byte[] GetFileBytes(string fileFullPath)
		{
			byte[] bytes;
			using (FileStream fs = new FileStream(fileFullPath, FileMode.Open, FileAccess.Read))
			{
				bytes = new byte[fs.Length];
				fs.Read(bytes, 0, bytes.Length);
				fs.Close();

				return bytes;
			}
		}

		/// <summary>
		/// 파일 정보 바인드
		/// </summary>
		private void FocusedDataBindOfFileInfo()
		{
			grdFileList.View.ClearDatas();
			picDefectPhoto.EditValue = null;

			DataRow selectRow = grdInspectionList.View.GetFocusedDataRow();
			if (selectRow == null) return;

			DataTable dt = grdFileList.DataSource as DataTable;
			string[] fileNames = Format.GetString(selectRow["FILENAME"]).Split(',');
			string[] fileExts = Format.GetString(selectRow["FILEEXT"]).Split(',');
			string[] fileSizes = Format.GetString(selectRow["FILESIZE"]).Split(',');
			string url = AppConfiguration.GetString("Application.SmartDeploy.Url") + selectRow["FILEPATH"].ToString();

			for (int i = 1; i < fileNames.Length; i++)
			{
				DataRow newRow = dt.NewRow();

				string fileUrl = url + "/" + fileNames[i] + "." + fileExts[i];
				dt.Rows.Add(fileNames[i], fileExts[i], fileSizes[i], selectRow["FILEPATH"], fileUrl);

			}

			grdFileList.DataSource = dt;
		}

		/// <summary>
		/// 화면에서 저장해도 되는지 여부 체크
		/// </summary>
		/// <returns></returns>
		public bool IsSavedCheck()
		{
			grdInspectionList.View.PostEditor();
			grdInspectionList.View.UpdateCurrentRow();

			return _isSaved;
		}

		#endregion
	}
}
