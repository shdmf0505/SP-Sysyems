#region using

using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;

using Micube.SmartMES.Commons;
using Micube.SmartMES.Commons.Controls;

using System;
using System.IO;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

#endregion

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질 > 품질규격 > 품질규격 측정값 등록 Popup
    /// 업  무  설  명  : 품질 규격 측정값을 등록 Popup
    /// 생    성    자  : 전우성
    /// 생    성    일  : 2019-08-07
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class popupMeasureValueRegistration : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region Interface

        /// <summary>
        /// 부모의 선택된 Row
        /// </summary>
        public DataRow CurrentDataRow { get; set; }

        #endregion

        #region Local Variables

        /// <summary>
        /// override from 오픈시에 받는 lot
        /// </summary>
        public string LotId = string.Empty;

        /// <summary>
        /// 인계대기상태에 따른 입력 여부
        /// </summary>
        private bool _isEdit = false;

        /// <summary>
        /// 검사원본 파일 정보
        /// </summary>
        private DataTable _inspectionFile;

        /// <summary>
        /// 재측정 횟수 
        /// </summary>
        private string _reworkCount = string.Empty;

        #endregion

        #region 생성자

        public popupMeasureValueRegistration()
        {
            InitializeComponent();

            InitializeContent();
            InitializeLanguageKey();
            InitializeGrid();
            InitializeEvent();
        }

        #endregion

        #region 컨텐츠 영역 초기화
        
        /// <summary>
        /// Controler 초기화
        /// </summary>
        private void InitializeContent()
        {
            this.AcceptButton = btnSave;
            this.CancelButton = btnCancle;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            txtProduct.ReadOnly = true;
            txtProductName.ReadOnly = true;
            txtProductVersion.ReadOnly = true;
            txtSite.ReadOnly = true;
            txtArea.ReadOnly = true;
            txtFactory.ReadOnly = true;
            txtEquipment.ReadOnly = true;
            txtMassProduce.ReadOnly = true;
            txtProcess.ReadOnly = true;
            txtMeasurer.ReadOnly = true;

            btnOriginalFile.ReadOnly = true;
            btnOriginalFile.Properties.Buttons.Add(new EditorButton(ButtonPredefines.Down));
            btnOriginalFile.Properties.Buttons.Add(new EditorButton(ButtonPredefines.Delete));

            SetImageTable();

            layoutMassProduceID.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            popLot.SelectPopupCondition = SetPopupByLotId();
        }

        /// <summary>
        /// Language Key 초기화
        /// </summary>
        private void InitializeLanguageKey()
        {
            this.LanguageKey = "MEASURVALUEREGISTER";
            grpBaseInformation.LanguageKey = "DEFAULTINFO";
            grdInspection.LanguageKey = "MEASUREITEMLIST";
            grdMeasuredValue.LanguageKey = "MEASUREDVALUE";
            btnImageSave.LanguageKey = "EXPORT";
            btnSave.LanguageKey = "SAVE";
            btnCancle.LanguageKey = "CANCEL";

            layoutInfoGroup.SetLanguageKey(layoutLot, "LOTID");
            layoutInfoGroup.SetLanguageKey(layoutProduct, "PRODUCTDEFID");
            layoutInfoGroup.SetLanguageKey(layoutProductName, "PRODUCTDEFNAME");
            layoutInfoGroup.SetLanguageKey(layoutProductRev, "PRODUCTDEFVERSION");
            layoutInfoGroup.SetLanguageKey(layoutSite, "SITE");
            layoutInfoGroup.SetLanguageKey(layoutFactory, "FACTORY");
            layoutInfoGroup.SetLanguageKey(layoutProcess, "STANDARDOPERATIONID");
            layoutInfoGroup.SetLanguageKey(layoutArea, "AREAID");
            layoutInfoGroup.SetLanguageKey(layoutEquipment, "EQUIPMENTUNIT");
            layoutInfoGroup.SetLanguageKey(layoutMassProduce, "PROCESSPRICETYPE");
            layoutInfoGroup.SetLanguageKey(layoutMeasurer, "MEASURER");
            layoutInfoGroup.SetLanguageKey(layoutOriginalFile, "INSPECTIONORIGINAL");

            layoutControlMain.SetLanguageKey(layoutReworkCount, "REWORKCOUNT");
        }

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            #region 측정항목

            grdInspection.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;

            grdInspection.View.AddTextBoxColumn("LOTID").SetIsHidden();
            grdInspection.View.AddTextBoxColumn("PRODUCTDEFID").SetIsHidden();
            grdInspection.View.AddTextBoxColumn("PRODUCTDEFNAME").SetIsHidden();
            grdInspection.View.AddTextBoxColumn("PRODUCTDEFVERSION").SetIsHidden();
            grdInspection.View.AddTextBoxColumn("PROCESSSEGMENTID").SetIsHidden();
            grdInspection.View.AddTextBoxColumn("PROCESSSEGMENTVERSION").SetIsHidden();
            grdInspection.View.AddTextBoxColumn("PROCESSDEFID").SetIsHidden();
            grdInspection.View.AddTextBoxColumn("PROCESSDEFVERSION").SetIsHidden();
            grdInspection.View.AddTextBoxColumn("ENTERPRISEID").SetIsHidden();
            grdInspection.View.AddTextBoxColumn("PLANTID").SetIsHidden();
            grdInspection.View.AddTextBoxColumn("AREAID").SetIsHidden();
            grdInspection.View.AddTextBoxColumn("EQUIPMENTID").SetIsHidden();
            grdInspection.View.AddTextBoxColumn("LOTTYPE").SetIsHidden();
            grdInspection.View.AddTextBoxColumn("CUSTOMERID").SetIsHidden();
            grdInspection.View.AddTextBoxColumn("MEASURER").SetIsHidden();
            grdInspection.View.AddTextBoxColumn("INSPITEMID").SetIsHidden();
            grdInspection.View.AddTextBoxColumn("INSPITEMVERSION").SetIsHidden();
            grdInspection.View.AddTextBoxColumn("CONTROLTYPE").SetIsHidden();
            grdInspection.View.AddTextBoxColumn("USL").SetIsHidden();
            grdInspection.View.AddTextBoxColumn("SL").SetIsHidden();
            grdInspection.View.AddTextBoxColumn("LSL").SetIsHidden();
            grdInspection.View.AddTextBoxColumn("INSPECTIONDEFID").SetIsHidden();
            grdInspection.View.AddTextBoxColumn("INSPECTIONDEFVERSION").SetIsHidden();
            grdInspection.View.AddTextBoxColumn("INSPECTIONTYPE").SetIsHidden();
            grdInspection.View.AddTextBoxColumn("NCRDECISIONDEGREE").SetIsHidden();
            grdInspection.View.AddTextBoxColumn("DESCRIPTION").SetIsHidden();

            grdInspection.View.AddTextBoxColumn("INSPITEMNAME", 120).SetLabel("MEASUREITEM").SetTextAlignment(TextAlignment.Left);
            grdInspection.View.AddTextBoxColumn("SPECRANGE", 80).SetLabel("SPECRANGESPC").SetTextAlignment(TextAlignment.Left);

            grdInspection.View.PopulateColumns();
            grdInspection.View.BestFitColumns();
            grdInspection.View.SetIsReadOnly();

            grdInspection.ShowStatusBar = true;
            grdInspection.GridButtonItem = GridButtonItem.None;

            #endregion

            #region 측정값 등록

            grdMeasuredValue.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdMeasuredValue.View.AddTextBoxColumn("NO").SetIsHidden();
            grdMeasuredValue.View.AddTextBoxColumn("INSPITEMID").SetIsHidden();
            grdMeasuredValue.View.AddTextBoxColumn("INSPITEMVERSION").SetIsHidden();
            grdMeasuredValue.View.AddTextBoxColumn("FILEDATA").SetIsHidden();
            grdMeasuredValue.View.AddTextBoxColumn("FILESIZE").SetIsHidden();
            grdMeasuredValue.View.AddTextBoxColumn("FILEEXT").SetIsHidden();
            grdMeasuredValue.View.AddTextBoxColumn("FILEPATH").SetIsHidden();
            grdMeasuredValue.View.AddTextBoxColumn("FILETYPE").SetIsHidden();
            grdMeasuredValue.View.AddTextBoxColumn("LOCALFILEPATH").SetIsHidden();
            grdMeasuredValue.View.AddTextBoxColumn("RESULT").SetIsHidden();
            grdMeasuredValue.View.AddTextBoxColumn("SPECRANGE").SetIsHidden();
            grdMeasuredValue.View.AddTextBoxColumn("REWORKCOUNT").SetIsHidden();

            grdMeasuredValue.View.AddSpinEditColumn("MEASURVALUE", 80).SetDisplayFormat("{0:f4}", MaskTypes.Custom)
                                                                      .SetTextAlignment(TextAlignment.Right);
            grdMeasuredValue.View.AddTextBoxColumn("FILENAME", 110).SetIsReadOnly().SetTextAlignment(TextAlignment.Left);
            grdMeasuredValue.View.AddButtonColumn("SKIPMARK", 20).SetTextAlignment(TextAlignment.Center);
            grdMeasuredValue.View.AddDateEditColumn("MEASUREDATETIME", 120).SetIsReadOnly().SetTextAlignment(TextAlignment.Left);

            grdMeasuredValue.View.PopulateColumns();
            grdMeasuredValue.View.BestFitColumns();

            grdMeasuredValue.ShowStatusBar = false;
            grdMeasuredValue.GridButtonItem = GridButtonItem.None;
            grdMeasuredValue.View.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            grdMeasuredValue.View.GridControl.ToolTipController = new ToolTipController();

            #endregion
        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            //! 화면 로드 이벤트
            this.Load += (s, e) =>
            {
                if (!DefectMapHelper.IsNull(CurrentDataRow))
                {
                    popLot.EditValue = DefectMapHelper.StringByDataRowObejct(CurrentDataRow, "LOTID");
                    popLot.Enabled = false;
                    SetDisplayData(CurrentDataRow);
                }
                else if (!string.IsNullOrEmpty(LotId))
                {
                    popLot.EditValue = LotId;
                    popLot.Enabled = true;
                    OverrideSetDataByLot();
                }
            };

            //! 검사항목 리스트에서 항목 클릭 이벤트
            grdInspection.View.SelectionChanged += (s, e) => SetMesureFiltering();

            //! 재측정 값 변경 이벤트
            cboReworkCount.TextChanged += (s, e) => SetMesureFiltering();

            //! 엔터 이벤트
            grdMeasuredValue.View.KeyDown += (s, e) =>
            {
                if (!e.KeyCode.Equals(Keys.Enter) ||
                    grdMeasuredValue.View.DataRowCount.Equals(0) ||
                    !grdMeasuredValue.View.GetDataRow(0).RowState.Equals(DataRowState.Added) ||
                    grdMeasuredValue.View.FocusedRowHandle >= 59)
                {
                    return;
                }

                grdMeasuredValue.View.CloseEditor();
                grdMeasuredValue.View.UpdateCurrentRow();
                e.Handled = true;

                if (grdMeasuredValue.View.RowCount.Equals(grdMeasuredValue.View.FocusedRowHandle + 1))
                {
                    grdMeasuredValue.View.AddNewRow();
                    grdMeasuredValue.View.MoveNext();
                }
                else
                {
                    grdMeasuredValue.View.MoveNext();
                }
            };

            //! New Row 발생 전 이벤트
            grdMeasuredValue.ToolbarAddingRow += (s, e) =>
            {
                if (grdMeasuredValue.View.RowCount is int rowCount)
                {
                    if (rowCount.Equals(0))
                    {
                        return;
                    }

                    if (grdMeasuredValue.View.GetRowCellValue(rowCount - 1, "MEASURVALUE") is object value)
                    {
                        if (DefectMapHelper.IsNull(value) || DBNull.Value.Equals(value) || rowCount.Equals(60))
                        {
                            e.Cancel = true;
                        }
                    }
                }
            };

            //! New Row 발생 이벤트
            grdMeasuredValue.View.InitNewRow += (s, e) =>
            {
                grdMeasuredValue.View.SetRowCellValue(e.RowHandle, "NO", grdMeasuredValue.View.DataRowCount + 1);
                grdMeasuredValue.View.SetRowCellValue(e.RowHandle, "REWORKCOUNT", _reworkCount); // Filter에 사용되는 값
                grdMeasuredValue.View.SetRowCellValue(e.RowHandle, "INSPITEMID", DefectMapHelper.StringByDataRowObejct(grdInspection.View.GetFocusedDataRow(), "INSPITEMID"));
            };

            //! 연속된 Row가 입력되도록 처리 이벤트
            grdMeasuredValue.View.RowCellClick += (s, e) =>
            {
                if (!e.Column.FieldName.Equals("MEASURVALUE"))
                {
                    return;
                }

                if (grdMeasuredValue.View.RowCount.Equals(0))
                {
                    grdMeasuredValue.View.Columns["MEASURVALUE"].OptionsColumn.ReadOnly = false;
                }
                else
                {
                    if (grdMeasuredValue.View.GetDataRow(0).RowState.Equals(DataRowState.Added))
                    {
                        grdMeasuredValue.View.Columns["MEASURVALUE"].OptionsColumn.ReadOnly = false;
                    }
                    else
                    {
                        grdMeasuredValue.View.Columns["MEASURVALUE"].OptionsColumn.ReadOnly = true;
                    }
                }
            };

            //! 신규 검사에 대한 측정값에 대해서 Add 버튼 추가
            grdMeasuredValue.View.FocusedRowChanged += (s, e) =>
            {
                if (grdMeasuredValue.View.GetFocusedDataRow() is DataRow row)
                {
                    grdMeasuredValue.GridButtonItem = row.RowState.Equals(DataRowState.Added)
                                                        ? GridButtonItem.Add | GridButtonItem.Delete
                                                        : GridButtonItem.None;
                    grdMeasuredValue.Refresh();
                }
            };

            //! 측정값 등록 첨부파일 등록 이벤트
            grdMeasuredValue.View.GridCellButtonClickEvent += (s, e) =>
            {
                try
                {
                    if (e.CurrentRow.RowState.Equals(DataRowState.Unchanged) ||
                       !e.FieldName.Equals("SKIPMARK") ||
                        string.IsNullOrEmpty(DefectMapHelper.StringByDataRowObejct(e.CurrentRow, "MEASURVALUE")))
                    {
                        return;
                    }

                    DialogManager.ShowWaitArea(this);

                    OpenFileDialog dialog = new OpenFileDialog
                    {
                        Multiselect = false,
                        Filter = "Image Files (*.bmp, *.jpg, *.jpeg, *.png)|*.BMP;*.JPG;*.JPEG;*.PNG",
                        FilterIndex = 0
                    };

                    if (dialog.ShowDialog().Equals(DialogResult.OK))
                    {
                        e.CurrentRow["FILENAME"] = Path.GetFileNameWithoutExtension(dialog.SafeFileName);
                        e.CurrentRow["FILEEXT"] = Path.GetExtension(dialog.FileName).Substring(1);
                        e.CurrentRow["FILESIZE"] = new FileInfo(dialog.FileName).Length;
                        e.CurrentRow["FILEDATA"] = (byte[])new ImageConverter().ConvertTo(new Bitmap(dialog.FileName), typeof(byte[]));
                        e.CurrentRow["FILEPATH"] = string.Join("/", "MeasureItem", popLot.Text, txtProductVersion.Text);
                        e.CurrentRow["LOCALFILEPATH"] = dialog.FileName;
                    }
                }
                catch (Exception ex)
                {
                    throw MessageException.Create(Format.GetString(ex));
                }
                finally
                {
                    DialogManager.CloseWaitArea(this);
                }
            };

            //! 측정 값 리스트에서 측정값이 규격 범위에 벗어났을 경우 처리 이벤트
            grdMeasuredValue.View.RowCellStyle += (s, e) =>
            {
                if (Format.GetString(grdMeasuredValue.View.GetRowCellValue(e.RowHandle, "RESULT")) is string result)
                {
                    if (string.IsNullOrEmpty(result))
                    {
                        return;
                    }

                    if (e.Column.FieldName.Equals("MEASURVALUE"))
                    {
                        e.Appearance.BackColor = result.Equals("OK") ? e.Appearance.BackColor : Color.Red;
                        e.Appearance.ForeColor = result.Equals("OK") ? e.Appearance.ForeColor : Color.White;
                    }
                }
            };

            //! 측정 값 입력시에 판정, 측정일시 입력 이벤트
            grdMeasuredValue.View.CellValueChanged += (s, e) =>
            {
                if (!e.Column.FieldName.Equals("MEASURVALUE"))
                {
                    return;
                }

                if (grdMeasuredValue.View.GetRowCellValue(e.RowHandle, "MEASURVALUE") is var measuredValue)
                {
                    if (DefectMapHelper.IsNull(measuredValue) || DBNull.Value.Equals(measuredValue))
                    {
                        return;
                    }

                    double usl = Format.GetDouble(grdInspection.View.GetDataRow(grdInspection.View.GetFocusedDataSourceRowIndex()).GetObject("USL"),
                                                  double.NaN);
                    double lsl = Format.GetDouble(grdInspection.View.GetDataRow(grdInspection.View.GetFocusedDataSourceRowIndex()).GetObject("LSL"),
                                                  double.NaN);

                    if (usl.Equals(double.NaN) && !lsl.Equals(double.NaN))
                    {
                        grdMeasuredValue.View.GetDataRow(e.RowHandle)["RESULT"] = DefectMapHelper.IsMeasureValueCheck(Format.GetDouble(e.Value, double.NaN), lsl, true);
                    }
                    else if (!usl.Equals(double.NaN) && lsl.Equals(double.NaN))
                    {
                        grdMeasuredValue.View.GetDataRow(e.RowHandle)["RESULT"] = DefectMapHelper.IsMeasureValueCheck(Format.GetDouble(e.Value, double.NaN), usl, false);
                    }
                    else if (usl.Equals(double.NaN) && lsl.Equals(double.NaN))
                    {
                        grdMeasuredValue.View.GetDataRow(e.RowHandle)["RESULT"] = "NG";
                    }
                    else
                    {
                        grdMeasuredValue.View.GetDataRow(e.RowHandle)["RESULT"] = DefectMapHelper.IsMeasureValueCheck(Format.GetDouble(e.Value, double.NaN), usl, lsl);
                    }

                    grdMeasuredValue.View.GetDataRow(e.RowHandle)["MEASUREDATETIME"] = DateTime.Now;
                }
            };

            //! 측정값 첨부파일 툴팁 이벤트
            grdMeasuredValue.View.GridControl.ToolTipController.GetActiveObjectInfo += (s, e) =>
            {
                if (grdMeasuredValue.View.CalcHitInfo(e.ControlMousePosition) is var hitInfo)
                {
                    if (!hitInfo.InRowCell || !hitInfo.Column.FieldName.Equals("FILENAME"))
                    {
                        return;
                    }

                    try
                    {
                        if ((grdMeasuredValue.View.GetRow(hitInfo.RowHandle) as DataRowView).Row is DataRow dr)
                        {
                            if (string.IsNullOrEmpty(DefectMapHelper.StringByDataRowObejct(dr, "FILENAME")))
                            {
                                return;
                            }

                            Image image = null;

                            if (string.IsNullOrEmpty(DefectMapHelper.StringByDataRowObejct(dr, "FILEDATA")))
                            {
                                if (DefectMapHelper.StringByDataRowObejct(dr, "FILETYPE").Equals("Interface"))
                                {
                                    if (DefectMapHelper.StringByDataRowObejct(dr, "FILEPATH") is string path)
                                    {
                                        image = CommonFunction.GetFtpImageFileToBitmap(path, false);
                                    }
                                }
                                else
                                {
                                    string file = string.Join(".",
                                                              DefectMapHelper.StringByDataRowObejct(dr, "FILENAME"),
                                                              DefectMapHelper.StringByDataRowObejct(dr, "FILEEXT"));

                                    image = CommonFunction.GetFtpImageFileToBitmap(DefectMapHelper.FTPFilePathCheck(DefectMapHelper.StringByDataRowObejct(dr, "FILEPATH")), file);
                                }
                            }
                            else
                            {
                                image = (Bitmap)new ImageConverter().ConvertFrom((byte[])dr.GetObject("FILEDATA"));
                            }

                            ToolTipControlInfo toolTipControlInfo = new ToolTipControlInfo(hitInfo.RowHandle, "FILEDATA");
                            SuperToolTip superToolTip = new SuperToolTip();
                            superToolTip.Items.AddTitle(DefectMapHelper.StringByDataRowObejct(dr, "FILENAME"));
                            superToolTip.Items.Add(new ToolTipItem()
                            {
                                Image = new Bitmap(image, new Size(1000, 800))
                            });

                            toolTipControlInfo.SuperTip = superToolTip;
                            e.Info = toolTipControlInfo;
                        }
                    }
                    catch
                    {

                    }
                }
            };

            //! 검사원본 파일 등록/다운로드/삭제
            btnOriginalFile.ButtonClick += (s, e) =>
            {
                switch ((s as ButtonEdit).Properties.Buttons.IndexOf(e.Button))
                {
                    case 0:
                        OpenFileDialog dialog = new OpenFileDialog
                        {
                            Multiselect = false,
                            Filter = "ALL File|*.xlsx; *.xls; *.docx; *.doc; *.txt |Excel File|*.xlsx|Excel 97~2003 |*.xls|Word |*.docx|Word 97~2003 |*.doc|Text File |*.txt",
                            FilterIndex = 0
                        };

                        if (dialog.ShowDialog().Equals(DialogResult.OK))
                        {
                            _inspectionFile.Rows.Clear();

                            btnOriginalFile.Text = dialog.SafeFileName;

                            DataRow dr = _inspectionFile.NewRow();

                            dr["FILENAME"] = Path.GetFileNameWithoutExtension(dialog.SafeFileName);
                            dr["FILEDATA"] = null;
                            dr["FILESIZE"] = new FileInfo(dialog.FileName).Length;
                            dr["FILEEXT"] = Path.GetExtension(dialog.FileName).Substring(1);
                            dr["FILETYPE"] = null;
                            dr["FILEPATH"] = string.Join("/", "MeasureItem", popLot.Text, txtProductVersion.Text);
                            dr["SAFEFILENAME"] = dialog.SafeFileName;
                            dr["PROCESSINGSTATUS"] = Constants.Wait;
                            dr["LOCALFILEPATH"] = dialog.FileName;

                            _inspectionFile.Rows.Add(dr);
                        };
                        break;
                    case 1:
                        if (string.IsNullOrEmpty(btnOriginalFile.Text))
                        {
                            return;
                        }

                        FolderBrowserDialog sdialog = new FolderBrowserDialog();

                        if (sdialog.ShowDialog().Equals(DialogResult.OK))
                        {
                            DataRow dr = _inspectionFile.Rows[0];

                            //! DB 조회후 다운로드
                            if (string.IsNullOrEmpty(DefectMapHelper.StringByDataRowObejct(dr, "LOCALFILEPATH")))
                            {
                                if (!string.IsNullOrEmpty(DefectMapHelper.StringByDataRowObejct(dr, "FILENAME")))
                                {
                                    string file = string.Join(".",
                                                              DefectMapHelper.StringByDataRowObejct(dr, "FILENAME"),
                                                              DefectMapHelper.StringByDataRowObejct(dr, "FILEEXT"));

                                    if (DefectMapHelper.StringByDataRowObejct(dr, "FILEPATH") is string path)
                                    {
                                        DefectMapHelper.FTPInterfaceFileDownload(path, file, sdialog.SelectedPath, false);
                                    }

                                    ////if (DefectMapHelper.StringByDataRowObejct(dr, "FILETYPE").Equals("Interface"))
                                    ////{
                                    ////    if (DefectMapHelper.StringByDataRowObejct(dr, "FILEPATH") is string path)
                                    ////    {
                                    ////        DefectMapHelper.FTPInterfaceFileDownload(path, file, sdialog.SelectedPath, false);
                                    ////    }
                                    ////}
                                    ////else
                                    ////{
                                    ////    DefectMapHelper.FTPUIFileDownload(DefectMapHelper.FTPFilePathCheck(DefectMapHelper.StringByDataRowObejct(dr, "FILEPATH")),
                                    ////                                    file, sdialog.SelectedPath, false);
                                    ////}
                                }
                            }
                            else
                            {
                                File.Copy(DefectMapHelper.StringByDataRowObejct(dr, "LOCALFILEPATH"), string.Join("\\", sdialog.SelectedPath, btnOriginalFile.Text));
                            }

                            ShowMessage("SuccedSave");
                        }
                        break;
                    case 2:
                        btnOriginalFile.Text = string.Empty;
                        _inspectionFile.Rows.Clear();
                        break;
                }
            };

            //! 이미지 저장 이벤트
            btnImageSave.Click += (s, e) =>
            {
                if (grdMeasuredValue.View.GetCheckedRows() is DataTable dt)
                {
                    if (dt.Rows.Count.Equals(0))
                    {
                        return;
                    }

                    // 2020.05.08-유석진-저장할 데이터 존재여부 체크하여 저장할 이미지가 없는 경우 메시지 뿌려 줌.
                    int i = 0;
                    foreach(DataRow chkDr in dt.Rows)
                    {
                        if(!string.IsNullOrEmpty(chkDr["FILENAME"].ToString()))
                        {
                            i++;
                        }
                    }

                    if(i == 0)
                    {
                        ShowMessage("NoSaveImageData");
                        return;
                    }

                    try
                    {
                        DialogManager.ShowWaitArea(this);
                        FolderBrowserDialog dialog = new FolderBrowserDialog
                        {
                            SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                        };

                        if (dialog.ShowDialog().Equals(DialogResult.OK))
                        {
                            dt.AsEnumerable().ForEach(dr =>
                            {
                                string file = string.Join(".",
                                                          DefectMapHelper.StringByDataRowObejct(dr, "FILENAME"),
                                                          DefectMapHelper.StringByDataRowObejct(dr, "FILEEXT"));

                                if (string.IsNullOrEmpty(DefectMapHelper.StringByDataRowObejct(dr, "FILEDATA")))
                                {
                                    if (!string.IsNullOrEmpty(DefectMapHelper.StringByDataRowObejct(dr, "FILENAME")))
                                    {
                                        if (DefectMapHelper.StringByDataRowObejct(dr, "FILETYPE").Equals("Interface"))
                                        {
                                            if (DefectMapHelper.StringByDataRowObejct(dr, "FILEPATH") is string path)
                                            {
                                                DefectMapHelper.FTPInterfaceFileDownload(path, file, dialog.SelectedPath, false);
                                            }
                                        }
                                        else
                                        {
                                            DefectMapHelper.FTPUIFileDownload(DefectMapHelper.FTPFilePathCheck(DefectMapHelper.StringByDataRowObejct(dr, "FILEPATH")),
                                                                            file, dialog.SelectedPath, true);
                                        }
                                    }
                                }
                                else
                                {
                                    Bitmap image = (Bitmap)new ImageConverter().ConvertFrom((byte[])dr.GetObject("FILEDATA"));
                                    image.Save(string.Concat(dialog.SelectedPath, "\\", file));
                                }
                            });

                            ShowMessage("SuccedSave");
                        }
                    }
                    catch (Exception ex)
                    {
                        throw MessageException.Create(Format.GetString(ex));
                    }
                    finally
                    {
                        DialogManager.CloseWaitArea(this);
                    }
                }
            };

            //! 저장버튼 클릭 이벤트
            btnSave.Click += (s, e) =>
            {
                try
                {
                    //! 검사항목이 없는 경우, 등록한 측정값이 없는 경우, 추가된 측정값이 없는경우 pass
                    if (grdInspection.View.DataRowCount.Equals(0) ||
                        grdMeasuredValue.GetChangesAdded().Rows.Count.Equals(0))
                    {
                        ShowMessage("NoDataChanged"); // 변경내용이 없습니다.
                        return;
                    }

                    if (grdMeasuredValue.GetChangedRows().Rows.Count.Equals(1) &&
                        grdMeasuredValue.GetChangedRows().Rows[0]["MEASURVALUE"] == DBNull.Value)
                    {
                        ShowMessage("NeedToInputEtchingValue"); // 측정값을 입력해야 합니다.
                        return;
                    }

                    //! FTP upload 처리
                    if (!FileUpload())
                    {
                        return;
                    }

                    DataSet ds = new DataSet();
                    DataTable inspFile = _inspectionFile.Copy();
                    inspFile.TableName = "original";

                    ds.Tables.Add(inspFile);
                    ds.Tables.Add(SetAnalysis());

                    if (ExecuteRule<DataTable>("QualitySpecification", ds) is DataTable workerDt)
                    {
                        if (!workerDt.Rows.Count.Equals(0))
                        {
                            SendMail(workerDt);
                        }
                    }

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MSGBox.Show(MessageBoxType.Warning, ex.Message);
                }
            };

            //! 취소버튼 클릭 이벤트
            btnCancle.Click += (s, e) => this.DialogResult = DialogResult.Cancel;
        }

        #endregion

        #region Private Function

        /// <summary>
        /// 메일 발송
        /// </summary>
        /// <param name="workerDt"></param>
        private void SendMail(DataTable workerDt)
        {
            DataTable dt = CommonFunction.CreateMeasureValueRegistrationDt();

            foreach (DataRow dr in workerDt.Rows)
            {
                DataRow newRow = dt.NewRow();
                newRow["PRODUCTDEFNAME"] = DefectMapHelper.StringByDataRowObejct(dr, "PRODUCTDEFNAME");
                newRow["PRODUCTDEFID"] = DefectMapHelper.StringByDataRowObejct(dr, "PRODUCTDEFID");
                newRow["PRODUCTDEFVERSION"] = DefectMapHelper.StringByDataRowObejct(dr, "PRODUCTDEFVERSION");
                newRow["RESOURCEID"] = DefectMapHelper.StringByDataRowObejct(dr, "RESOURCEID");
                newRow["MEASUREDATETIME"] = DefectMapHelper.StringByDataRowObejct(dr, "MEASUREDATETIME");
                newRow["INSPITEMID"] = DefectMapHelper.StringByDataRowObejct(dr, "INSPITEMID");
                newRow["MEASURER"] = DefectMapHelper.StringByDataRowObejct(dr, "MEASURER");
                newRow["PLANTID"] = DefectMapHelper.StringByDataRowObejct(dr, "PLANTID");
                newRow["PROCESSSEGMENTID"] = DefectMapHelper.StringByDataRowObejct(dr, "PROCESSSEGMENTID");
                newRow["AREAID"] = DefectMapHelper.StringByDataRowObejct(dr, "AREAID");
                newRow["EQUIPMENTID"] = DefectMapHelper.StringByDataRowObejct(dr, "EQUIPMENTID");
                newRow["MEASUREVALUELIST"] = DefectMapHelper.StringByDataRowObejct(dr, "MEASUREVALUELIST");
                newRow["SPECRANGE"] = DefectMapHelper.StringByDataRowObejct(dr, "SPECRANGE");
                newRow["REMARK"] = "";
                newRow["USERID"] = UserInfo.Current.Id;
                newRow["TITLE"] = Language.Get("QUALITYABNORMALTITLE");
                newRow["INSPECTION"] = "OperationInspection";
                newRow["LANGUAGETYPE"] = UserInfo.Current.LanguageType;
                dt.Rows.Add(newRow);
            }

            CommonFunction.ShowSendEmailPopupDataTable(dt);
        }

        /// <summary>
        /// 저장 시 파일 업로드
        /// </summary>
        /// <returns></returns>
        private bool FileUpload()
        {
            try
            {
                int totalSize = _inspectionFile.Rows.Count.Equals(1)
                                    ? DefectMapHelper.IntByDataRowObject(_inspectionFile.Rows[0], "FILESIZE")
                                    : 0;

                foreach (DataRow imageDr in grdMeasuredValue.GetChangesAdded().Rows)
                {
                    if (string.IsNullOrEmpty(DefectMapHelper.StringByDataRowObejct(imageDr, "FILENAME")))
                    {
                        continue;
                    }

                    DataRow dr = _inspectionFile.NewRow();
                    dr["FILENAME"] = DefectMapHelper.StringByDataRowObejct(imageDr, "FILENAME");
                    dr["FILESIZE"] = DefectMapHelper.StringByDataRowObejct(imageDr, "FILESIZE");
                    dr["FILEEXT"] = DefectMapHelper.StringByDataRowObejct(imageDr, "FILEEXT");
                    dr["FILEPATH"] = DefectMapHelper.StringByDataRowObejct(imageDr, "FILEPATH");
                    dr["SAFEFILENAME"] = string.Join(".",
                                                     DefectMapHelper.StringByDataRowObejct(imageDr, "FILENAME"),
                                                     DefectMapHelper.StringByDataRowObejct(imageDr, "FILEEXT"));
                    dr["PROCESSINGSTATUS"] = Constants.Wait;
                    dr["LOCALFILEPATH"] = DefectMapHelper.StringByDataRowObejct(imageDr, "LOCALFILEPATH");

                    _inspectionFile.Rows.Add(dr);

                    totalSize += DefectMapHelper.IntByDataRowObject(imageDr, "FILESIZE");
                }

                if (_inspectionFile.Rows.Count.Equals(0))
                {
                    return true;
                }

                FileProgressDialog dialog = new FileProgressDialog(_inspectionFile, UpDownType.Upload, string.Empty, totalSize);
                if (!dialog.ShowDialog().Equals(DialogResult.OK))
                {
                    return false;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 검사항목에 측정값에 대한 Analysis 처리
        /// </summary>
        private DataTable SetAnalysis()
        {
            DataTable dt = SetDaResult();

            // 신뢰성 의뢰를 하기 위한 체크 true이면 신뢰성 의뢰를 신청한다
            bool isAllMeasureCheck = true;

            // 측정값 중 NG가 하나라도 있으면 NG
            string result = (grdMeasuredValue.DataSource as DataTable).AsEnumerable()
                                                                      .Where(m => m.Field<string>("RESULT") == "NG")
                                                                      .Count().Equals(0) ? "OK" : "NG";

            foreach (DataRow dr in (grdInspection.DataSource as DataTable).Rows)
            {
                // 검사항목에 등록된 측정 값 Count
                List<object> measureValue = (grdMeasuredValue.DataSource as DataTable).AsEnumerable()
                                                             .Where(m => m.Field<string>("INSPITEMID").Equals(DefectMapHelper.StringByDataRowObejct(dr, "INSPITEMID"))
                                                                         && !DefectMapHelper.IsNull(m.Field<object>("MEASURVALUE")))
                                                             .Select(x => x.Field<object>("MEASURVALUE")).ToList();

                if (measureValue.Count.Equals(0))
                {
                    isAllMeasureCheck = false;
                    ////measureSize = measureSize - 1;
                    //// 검사항목이 이미 등록이 되어있었다면 pass
                    ////if (DefectMapHelper.StringByDataRowObejct(dr, "DESCRIPTION").Equals("NODATA"))
                    ////{
                    ////    continue;
                    ////}

                    ////DataRow newRow = dt.NewRow();
                    ////SetAnalysisDataRow(ref newRow, dr);
                    ////newRow["LOTINSPECTIONRESULT"] = result;
                    ////newRow["DESCRIPTION"] = "NODATA";

                    ////dt.Rows.Add(newRow);
                }
                else
                {
                    string measureValueList = string.Join(", ", measureValue);

                    List<DataRow> list = grdMeasuredValue.GetChangesAdded().AsEnumerable()
                                                         .Where(m => m.Field<string>("INSPITEMID").Equals(DefectMapHelper.StringByDataRowObejct(dr, "INSPITEMID"))
                                                                     && !DefectMapHelper.IsNull(m.Field<object>("MEASURVALUE")))
                                                         .OrderBy(m => m.Field<int>("NO")).ToList();

                    if (list.Count().Equals(0))
                    {
                        continue;
                    }

                    var analysis = list.AsEnumerable()
                                       .GroupBy(m => m.Field<string>("INSPITEMID"))
                                       .Select(grp => new
                                       {
                                           AVERAGEVALUE = grp.Average(x => x.Field<object>("MEASURVALUE").ToSafeDecimal()),
                                           MAXVALUE = grp.Max(x => x.Field<object>("MEASURVALUE").ToSafeDecimal()),
                                           MINVALUE = grp.Min(x => x.Field<object>("MEASURVALUE").ToSafeDecimal()),
                                           DEVIATION = grp.Max(x => x.Field<object>("MEASURVALUE").ToSafeDecimal())
                                                        - grp.Min(x => x.Field<object>("MEASURVALUE").ToSafeDecimal()),
                                           INSPECTIONRESULT = (grp.Count(x => x.Field<string>("RESULT").Equals("NG")).Equals(0) ? "OK" : "NG")
                                       }).ToList();

                    for (int i = 0; i < list.Count(); i++)
                    {
                        DataRow newRow = dt.NewRow();
                        SetAnalysisDataRow(ref newRow, dr);

                        newRow["INSPECTIONRESULT"] = analysis.FirstOrDefault().INSPECTIONRESULT;
                        newRow["AVERAGEVALUE"] = analysis.FirstOrDefault().AVERAGEVALUE;
                        newRow["MAXVALUE"] = analysis.FirstOrDefault().MAXVALUE;
                        newRow["MINVALUE"] = analysis.FirstOrDefault().MINVALUE;
                        newRow["DEVIATION"] = analysis.FirstOrDefault().DEVIATION;

                        newRow["MEASUREVALUELIST"] = measureValueList;

                        newRow["DAPOINTID"] = (i + 1); // + rowNum;
                        newRow["VALUE"] = DefectMapHelper.StringByDataRowObejct(list[i], "MEASURVALUE");
                        newRow["MEASUREDATETIME"] = list[i]["MEASUREDATETIME"];
                        newRow["RESULT"] = DefectMapHelper.StringByDataRowObejct(list[i], "RESULT");

                        if (!string.IsNullOrEmpty(DefectMapHelper.StringByDataRowObejct(list[i], "FILENAME")))
                        {
                            newRow["FILENAME"] = DefectMapHelper.StringByDataRowObejct(list[i], "FILENAME");
                            newRow["FILEEXT"] = DefectMapHelper.StringByDataRowObejct(list[i], "FILEEXT");
                            newRow["FILESIZE"] = DefectMapHelper.IntByDataRowObject(list[i], "FILESIZE");
                            newRow["FILETYPE"] = DefectMapHelper.StringByDataRowObejct(list[i], "FILETYPE");
                            newRow["FILEDATA"] = null;
                            //// newRow["FILEDATA"] = DefectMapHelper.StringByDataRowObejct(list[i], "FILEDATA");
                            newRow["FILEPATH"] = DefectMapHelper.StringByDataRowObejct(list[i], "FILEPATH");
                        }

                        newRow["LOTINSPECTIONRESULT"] = result;

                        dt.Rows.Add(newRow);
                    }
                }
            }

            ////if (!dtInspection.Rows.Count.Equals(measureSize))
            ////{
            ////    dt.Rows[0]["ISALLMEASURECHECK"] = false;
            ////}

            if (!dt.Rows.Count.Equals(0))
            {
                dt.Rows[0]["ISALLMEASURECHECK"] = isAllMeasureCheck;
            }

            return dt;
        }

        /// <summary>
        /// Lot Popup Setting
        /// </summary>
        /// <returns></returns>
        private ConditionItemSelectPopup SetPopupByLotId()
        {
            Dictionary<string, object> param = new Dictionary<string, object>
            {
                { "LANGUAGETYPE", UserInfo.Current.LanguageType },
                { "P_USERID", UserInfo.Current.Id }
            };

            ConditionItemSelectPopup popup = new ConditionItemSelectPopup
            {
                Id = "LOTSEARCH",
                DisplayFieldName = "LOTID",
                ValueFieldName = "LOTID",
                IsMultiGrid = false,
                SearchQuery = new SqlQuery("GetSFLotList", "10001", param)
            };

            popup.SetPopupLayoutForm(600, 500, FormBorderStyle.FixedDialog);
            popup.SetPopupLayout("SELECTLOTNO", PopupButtonStyles.Ok_Cancel, true, false);
            popup.SetPopupAutoFillColumns("LOTID");
            popup.SetPopupResultCount(1);

            popup.Conditions.AddTextBox("P_LOTID").SetLabel("LOTID");
            popup.Conditions.AddTextBox("P_PRODUCTDEFID").SetLabel("PRODUCTDEFID");

            popup.GridColumns.AddTextBoxColumn("LOTID", 200).SetIsReadOnly().SetTextAlignment(TextAlignment.Left);
            popup.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 120).SetIsReadOnly().SetTextAlignment(TextAlignment.Left);
            popup.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 60).SetIsReadOnly().SetTextAlignment(TextAlignment.Left);
            popup.GridColumns.AddTextBoxColumn("REWORKCOUNT", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Left);

            popup.SetPopupApplySelection((selectedRows, dataGridRow) =>
            {
                try
                {
                    foreach (DataRow dr in selectedRows)
                    {
                        SetDisplayData(dr);
                    }
                }
                catch (Exception ex)
                {
                    throw MessageException.Create(Format.GetString(ex));
                }
            });

            return popup;
        }

        /// <summary>
        /// 화면 Data 출력
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="isParamLot">override로 화면 출력시</param>
        private void SetDisplayData(DataRow dr, bool isParamLot = false)
        {
            if (DefectMapHelper.IsNull(dr))
            {
                return;
            }

            btnOriginalFile.Properties.Buttons[0].Enabled = popLot.Enabled;
            btnOriginalFile.Properties.Buttons[2].Enabled = popLot.Enabled;

            _reworkCount = DefectMapHelper.StringByDataRowObejct(dr, "REWORKCOUNT");

            SetClose();

            Dictionary<string, object> value = new Dictionary<string, object>()
            {
                {"P_LOTID", DefectMapHelper.StringByDataRowObejct(dr, "LOTID") },
                {"P_AREAID", DefectMapHelper.StringByDataRowObejct(dr, "AREAID") },
                {"P_EQUIPMENTID", DefectMapHelper.StringByDataRowObejct(dr, "EQUIPMENTID") },
                {"P_PLANTID", DefectMapHelper.StringByDataRowObejct(dr, "PLANTID") },
                {"P_LOTTYPE", DefectMapHelper.StringByDataRowObejct(dr, "LOTTYPE") },
                {"P_MEASURER", UserInfo.Current.Id },
                {"P_PROCESSSEGMENTID", DefectMapHelper.StringByDataRowObejct(dr, "PROCESSSEGMENTID") },
                {"P_PRODUCTDEFID", DefectMapHelper.StringByDataRowObejct(dr, "PRODUCTDEFID") },
                {"P_PRODUCTDEFVERSION", DefectMapHelper.StringByDataRowObejct(dr, "PRODUCTDEFVERSION") },
                {"P_INSPECTIONCLASSID", "OperationInspection" },
                {"P_REWORKCOUNT", _reworkCount }
            };

            txtProductName.Text = DefectMapHelper.StringByDataRowObejct(dr, "PRODUCTDEFNAME");          // 품목명
            txtProduct.Text = DefectMapHelper.StringByDataRowObejct(dr, "PRODUCTDEFID");                // 품목
            txtProductVersion.Text = DefectMapHelper.StringByDataRowObejct(dr, "PRODUCTDEFVERSION");    // 품목버젼
            txtSite.Text = DefectMapHelper.StringByDataRowObejct(dr, "PLANTID");                        // site
            txtFactory.Text = DefectMapHelper.StringByDataRowObejct(dr, "FACTORYID");                   // 공장
            txtProcess.Text = DefectMapHelper.StringByDataRowObejct(dr, "PROCESSSEGMENTNAME");          // 공정
            txtArea.Text = DefectMapHelper.StringByDataRowObejct(dr, "AREANAME");                       // 작업장
            txtEquipment.Text = DefectMapHelper.StringByDataRowObejct(dr, "EQUIPMENTUNIT");             // 설비(호기)
            txtMassProduce.Text = DefectMapHelper.StringByDataRowObejct(dr, "SUBNAME");                 // 양산구분
            txtMeasurer.Text = popLot.Enabled ? UserInfo.Current.Name :
                                                DefectMapHelper.StringByDataRowObejct(dr, "MEASURER", UserInfo.Current.Name); // 측정자
            txtMassProduceID.Text = DefectMapHelper.StringByDataRowObejct(dr, "LOTTYPE");               // 양산구분ID

            SetReworkCombo();

            // 인계대기 상태 확인
            _isEdit = SqlExecuter.Query("GetLotWaitForSend", "10001", value).Rows.Count.Equals(0);

            // 검사원본 조회
            if (SqlExecuter.Query("GetInspectionOriginalFile", "10001", value) is DataTable fileDt)
            {
                if (!fileDt.Rows.Count.Equals(0))
                {
                    btnOriginalFile.Text = DefectMapHelper.StringByDataRowObejct(fileDt.Rows[0], "FILENAME");
                }

                _inspectionFile = fileDt;
            }

            // 측정항목 조회
            if (SqlExecuter.Query("GetInsepctionSpecListByItem", popLot.Enabled ? "10001" : "10002"
                                , DefectMapHelper.AddLanguageTypeToConditions(value)) is DataTable dt)
            {
                if (dt.Rows.Count.Equals(0))
                {
                    ShowMessage("NoInspectionSpec"); // 등록된 SPEC이 없습니다.

                    if (isParamLot)
                    {
                        this.DialogResult = DialogResult.Cancel;
                        this.Close();
                    }

                    grdMeasuredValue.GridButtonItem = GridButtonItem.None;
                    return;
                }

                //! Measured Value List 조회
                grdMeasuredValue.DataSource = SqlExecuter.Query("GetQualitySpecificationValue", "10001", value);
                grdInspection.DataSource = dt;

                if (!string.IsNullOrEmpty(DefectMapHelper.StringByDataRowObejct(dr, "DAITEMID")))
                {
                    grdInspection.View.ClearSelection();
                    //// int handle = grdInspection.View.GetRowHandleByValue("INSPITEMID", DefectMapHelper.StringByDataRowObejct(dr, "DAITEMID"));
                    grdInspection.View.FocusedRowHandle = grdInspection.View.GetRowHandleByValue("INSPITEMID", DefectMapHelper.StringByDataRowObejct(dr, "DAITEMID"));
                    grdInspection.View.SelectRow(grdInspection.View.FocusedRowHandle);
                }
            }
        }

        /// <summary>
        /// 측정항목 및 재측정 값 변경시 필터링
        /// </summary>
        private void SetMesureFiltering()
        {
            if (grdInspection.View.GetFocusedDataRow() is DataRow dr)
            {
                grdMeasuredValue.View.ActiveFilterString =
                    string.Concat("[INSPITEMID] = '", DefectMapHelper.StringByDataRowObejct(dr, "INSPITEMID"),
                                  "' AND [REWORKCOUNT] ='", cboReworkCount.Text, "'");

                if (_isEdit || !_reworkCount.Equals(cboReworkCount.Text))
                {
                    grdMeasuredValue.GridButtonItem = GridButtonItem.None;
                    return;
                }

                if (grdMeasuredValue.View.DataRowCount.Equals(0))
                {
                    grdMeasuredValue.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete;
                }
                else
                {
                    if (grdMeasuredValue.View.GetDataRow(0).RowState.Equals(DataRowState.Added))
                    {
                        grdMeasuredValue.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete;
                    }
                    else
                    {
                        grdMeasuredValue.GridButtonItem = GridButtonItem.None;
                    }
                }
            }
        }

        /// <summary>
        /// 재측정 Combo 설정
        /// </summary>
        /// <param name="degree"></param>
        private void SetReworkCombo()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CODEID", typeof(string));
            dt.Columns.Add("CODENAME", typeof(string));

            cboReworkCount.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboReworkCount.ShowHeader = false;
            cboReworkCount.DisplayMember = "CODENAME";
            cboReworkCount.ValueMember = "CODEID";

            for (int i = 0; i <= Format.GetInteger(_reworkCount); i++)
            {
                DataRow newRow = dt.NewRow();
                newRow["CODEID"] = i;
                newRow["CODENAME"] = i;
                dt.Rows.Add(newRow);
            }

            cboReworkCount.DataSource = dt;
            cboReworkCount.EditValue = _reworkCount;
            cboReworkCount.Enabled = !_reworkCount.Equals("0");
        }

        /// <summary>
        /// override로 Lot을 받아 화면 출력한다
        /// </summary>
        private void OverrideSetDataByLot()
        {
            Dictionary<string, object> value = new Dictionary<string, object>()
            {
                {"P_LOTID", LotId },
                {"P_USERID", UserInfo.Current.Id }
            };

            SetDisplayData(SqlExecuter.Query("GetSFLotList", "10001", DefectMapHelper.AddLanguageTypeToConditions(value))?.Rows[0], true);
        }

        /// <summary>
        /// 원본이미지 저장할 DataTable 구성
        /// </summary>
        /// <returns></returns>
        private void SetImageTable()
        {
            _inspectionFile = new DataTable("original");

            _inspectionFile.Columns.Add("FILENAME", typeof(string));
            _inspectionFile.Columns.Add("FILEDATA", typeof(byte[]));
            _inspectionFile.Columns.Add("FILESIZE", typeof(string));
            _inspectionFile.Columns.Add("FILEEXT", typeof(string));
            _inspectionFile.Columns.Add("FILETYPE", typeof(string));
            _inspectionFile.Columns.Add("FILEPATH", typeof(string));
            _inspectionFile.Columns.Add("SAFEFILENAME", typeof(string));
            _inspectionFile.Columns.Add("PROCESSINGSTATUS", typeof(string)); // Wait
            _inspectionFile.Columns.Add("LOCALFILEPATH", typeof(string));
        }

        /// <summary>
        /// Analysis Data Row에 값 입력하기
        /// </summary>
        /// <param name="newRow"></param>
        /// <param name="dr"></param>
        private void SetAnalysisDataRow(ref DataRow newRow, DataRow dr)
        {
            newRow["RESOURCETYPE"] = "OperationInspection";
            newRow["RESOURCEID"] = DefectMapHelper.StringByDataRowObejct(dr, "LOTID");
            newRow["DADEFID"] = "OperationInspection";
            newRow["DADEFVERSION"] = "*";
            newRow["INSPITEMID"] = DefectMapHelper.StringByDataRowObejct(dr, "INSPITEMID");
            newRow["INSPITEMNAME"] = DefectMapHelper.StringByDataRowObejct(dr, "INSPITEMNAME");
            newRow["INSPITEMVERSION"] = DefectMapHelper.StringByDataRowObejct(dr, "INSPITEMVERSION");
            newRow["PRODUCTDEFID"] = DefectMapHelper.StringByDataRowObejct(dr, "PRODUCTDEFID");
            newRow["PRODUCTDEFNAME"] = DefectMapHelper.StringByDataRowObejct(dr, "PRODUCTDEFNAME");
            newRow["PRODUCTDEFVERSION"] = DefectMapHelper.StringByDataRowObejct(dr, "PRODUCTDEFVERSION");
            newRow["PROCESSDEFID"] = DefectMapHelper.StringByDataRowObejct(dr, "PROCESSDEFID");
            newRow["PROCESSDEFVERSION"] = DefectMapHelper.StringByDataRowObejct(dr, "PROCESSDEFVERSION");
            newRow["PROCESSPATHID"] = "*";
            newRow["PROCESSSEGMENTID"] = DefectMapHelper.StringByDataRowObejct(dr, "PROCESSSEGMENTID");
            newRow["PROCESSSEGMENTVERSION"] = DefectMapHelper.StringByDataRowObejct(dr, "PROCESSSEGMENTVERSION");
            newRow["ENTERPRISEID"] = DefectMapHelper.StringByDataRowObejct(dr, "ENTERPRISEID");
            newRow["PLANTID"] = DefectMapHelper.StringByDataRowObejct(dr, "PLANTID");
            newRow["AREAID"] = DefectMapHelper.StringByDataRowObejct(dr, "AREAID");
            newRow["EQUIPMENTID"] = DefectMapHelper.StringByDataRowObejct(dr, "EQUIPMENTID");
            newRow["REWORKCOUNT"] = _reworkCount;
            ////newRow["REWORKCOUNT"] = DefectMapHelper.StringByDataRowObejct(dr, "REWORKCOUNT");
            newRow["LOTTYPE"] = DefectMapHelper.StringByDataRowObejct(dr, "LOTTYPE");
            newRow["CUSTOMERID"] = DefectMapHelper.StringByDataRowObejct(dr, "CUSTOMERID");
            newRow["MEASURER"] = DefectMapHelper.StringByDataRowObejct(dr, "MEASURER");
            newRow["SPECRANGE"] = DefectMapHelper.StringByDataRowObejct(dr, "SPECRANGE");
            newRow["CONTROLTYPE"] = DefectMapHelper.StringByDataRowObejct(dr, "CONTROLTYPE");
            newRow["TARGET"] = DefectMapHelper.StringByDataRowObejct(dr, "SL");
            newRow["LOWERSPECLIMIT"] = DefectMapHelper.StringByDataRowObejct(dr, "LSL");
            newRow["UPPERSPECLIMIT"] = DefectMapHelper.StringByDataRowObejct(dr, "USL");

            newRow["INSPECTIONDEFID"] = DefectMapHelper.StringByDataRowObejct(dr, "INSPECTIONDEFID");
            newRow["INSPECTIONDEFVERSION"] = DefectMapHelper.StringByDataRowObejct(dr, "INSPECTIONDEFVERSION");
            newRow["INSPECTIONTYPE"] = DefectMapHelper.StringByDataRowObejct(dr, "INSPECTIONTYPE");

            newRow["NCRDECISIONDEGREE"] = DefectMapHelper.StringByDataRowObejct(dr, "NCRDECISIONDEGREE");
            newRow["ISALLMEASURECHECK"] = true;
        }

        /// <summary>
        /// Da Result DataTable 구성
        /// </summary>
        /// <returns></returns>
        private DataTable SetDaResult()
        {
            DataTable dt = new DataTable("analysis");

            dt.Columns.Add("RESOURCETYPE", typeof(string));
            dt.Columns.Add("RESOURCEID", typeof(string));
            dt.Columns.Add("DADEFID", typeof(string));
            dt.Columns.Add("DADEFVERSION", typeof(string));
            dt.Columns.Add("INSPITEMID", typeof(string));
            dt.Columns.Add("INSPITEMNAME", typeof(string));
            dt.Columns.Add("INSPITEMVERSION", typeof(string));
            dt.Columns.Add("DAPOINTID", typeof(string));
            dt.Columns.Add("PRODUCTDEFNAME", typeof(string));
            dt.Columns.Add("PRODUCTDEFID", typeof(string));
            dt.Columns.Add("PRODUCTDEFVERSION", typeof(string));
            dt.Columns.Add("PROCESSDEFID", typeof(string));
            dt.Columns.Add("PROCESSDEFVERSION", typeof(string));
            dt.Columns.Add("PROCESSPATHID", typeof(string));
            dt.Columns.Add("PROCESSSEGMENTID", typeof(string));
            dt.Columns.Add("PROCESSSEGMENTVERSION", typeof(string));
            dt.Columns.Add("ENTERPRISEID", typeof(string));
            dt.Columns.Add("PLANTID", typeof(string));
            dt.Columns.Add("AREAID", typeof(string));
            dt.Columns.Add("EQUIPMENTID", typeof(string));
            dt.Columns.Add("VALUE", typeof(float));
            dt.Columns.Add("REWORKCOUNT", typeof(string));
            dt.Columns.Add("LOTTYPE", typeof(string));
            dt.Columns.Add("CUSTOMERID", typeof(string));
            dt.Columns.Add("MEASURER", typeof(string));
            dt.Columns.Add("SPECRANGE", typeof(string));
            dt.Columns.Add("CONTROLTYPE", typeof(string));
            dt.Columns.Add("TARGET", typeof(string));
            dt.Columns.Add("LOWERSPECLIMIT", typeof(string));
            dt.Columns.Add("UPPERSPECLIMIT", typeof(string));
            dt.Columns.Add("INSPECTIONRESULT", typeof(string));
            dt.Columns.Add("AVERAGEVALUE", typeof(float));
            dt.Columns.Add("MAXVALUE", typeof(float));
            dt.Columns.Add("MINVALUE", typeof(float));
            dt.Columns.Add("DEVIATION", typeof(float));
            dt.Columns.Add("MEASUREDATETIME", typeof(DateTime));
            dt.Columns.Add("RESULT", typeof(string));
            dt.Columns.Add("FILENAME", typeof(string));
            dt.Columns.Add("FILESIZE", typeof(int));
            dt.Columns.Add("FILEEXT", typeof(string));
            dt.Columns.Add("FILETYPE", typeof(string));
            dt.Columns.Add("FILEDATA", typeof(byte[]));
            dt.Columns.Add("FILEPATH", typeof(string));
            dt.Columns.Add("INSPECTIONDEFID", typeof(string));
            dt.Columns.Add("INSPECTIONDEFVERSION", typeof(string));
            dt.Columns.Add("INSPECTIONTYPE", typeof(string));
            dt.Columns.Add("LOTINSPECTIONRESULT", typeof(string));
            dt.Columns.Add("NCRDECISIONDEGREE", typeof(string));
            dt.Columns.Add("DESCRIPTION", typeof(string));
            dt.Columns.Add("MEASUREVALUELIST", typeof(string));
            dt.Columns.Add("ISALLMEASURECHECK", typeof(bool));

            return dt;
        }

        /// <summary>
        /// 화면 내용 삭제
        /// </summary>
        private void SetClose()
        {
            txtProduct.Text = string.Empty;
            txtProductName.Text = string.Empty;
            txtProductVersion.Text = string.Empty;
            txtSite.Text = string.Empty;
            txtFactory.Text = string.Empty;
            txtProcess.Text = string.Empty;
            txtArea.Text = string.Empty;
            txtEquipment.Text = string.Empty;
            txtMassProduce.Text = string.Empty;
            txtMeasurer.Text = string.Empty;
            txtMassProduceID.Text = string.Empty;
            btnOriginalFile.Text = string.Empty;

            grdInspection.DataSource = null;
            grdMeasuredValue.DataSource = null;
            cboReworkCount.ClearContent();
        }

        #endregion
    }
}