#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;

using Micube.SmartMES.Commons;
using Micube.SmartMES.Commons.Gerber;
using Micube.SmartMES.Commons.Controls;
using Micube.SmartMES.Commons.Gerber.Core;

using DevExpress.XtraEditors.Controls;

using System;
using System.IO;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;

#endregion

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프 로 그 램 명  : 기준 정보 > 품질관리 > PCS 이미지 관리
    /// 업  무  설  명  : Defect Map PCS 도면 등록화면
    /// 생    성    자  : 전우성
    /// 생    성    일  : 2019-07-02
    /// 수  정  이  력  : 
    ///     2019-12-19  전우성 Table 형식 변경으로 전면 수정
    /// 
    /// </summary>
    public partial class GerberDataConversionToPng_YPE : SmartConditionManualBaseForm
    {
        #region Local Variables

        /// <summary>
        /// app.yml 파일에 설정된 로컬패스
        /// </summary>
        private readonly string _LOCALFILEPATH = Path.Combine(Application.StartupPath, "PCSIMAGE");

        /// <summary>
        /// 자른 사각형 위치와 크기 정보
        /// </summary>
        private Rectangle cropRacktangle = Rectangle.Empty;

        /// <summary>
        /// 마우스 다운 포인트
        /// </summary>
        private Point mouseDownPoint = Point.Empty;

        #endregion

        #region 생성자

        public GerberDataConversionToPng_YPE()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeEvent();
            InitializeGrid();
            InitializeLanguageKey();

            txtWidth.Editor.ReadOnly = true;
            txtHeight.Editor.ReadOnly = true;
            txtPath.ReadOnly = true;

            btnConversion.Enabled = false;

            picMain.Properties.SizeMode = PictureSizeMode.Clip;
            picMain.Properties.ZoomingOperationMode = DevExpress.XtraEditors.Repository.ZoomingOperationMode.ControlMouseWheel;
            picMain.Properties.ShowScrollBars = true;

            picMain.Properties.ShowZoomSubMenu = DevExpress.Utils.DefaultBoolean.True;
            picMain.Properties.AllowScrollViaMouseDrag = false;
        }

        /// <summary>
        /// Language Key 초기화
        /// </summary>
        private void InitializeLanguageKey()
        {
            grdMain.LanguageKey = "IMAGELIST";
            grpMain.LanguageKey = "DETAIL";
            txtWidth.LanguageKey = "WIDTH";
            txtHeight.LanguageKey = "HEIGHT";
            btnConversion.LanguageKey = "IMAGEPROCESSING";
            btnCrop.LanguageKey = "CROP";
        }

        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            grdMain.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            #region 품목 코드/version 가져오기

            var productDefPopup = grdMain.View.AddSelectPopupColumn("PRODUCTDEFID", new SqlQuery("GetProductDefList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"PRODUCTDEFTYPE=Product"), "PRODUCTDEFID")
                                         .SetValidationKeyColumn()
                                         .SetValidationIsRequired()
                                         .SetTextAlignment(TextAlignment.Left)
                                         .SetPopupAutoFillColumns("PRODUCTDEFID")
                                         .SetDefault(null, null, null)
                                         .SetPopupResultCount(1)
                                         .SetPopupLayout("PRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)  //팝업창 UI 설정.
                                         .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                                         .SetPopupApplySelection((selectedRow, dataGirdRow) =>
                                         {
                                             foreach (DataRow dr in selectedRow)
                                             {
                                                 grdMain.View.SetRowCellValue(grdMain.View.GetFocusedDataSourceRowIndex(), "PRODUCTDEFVERSION", Format.GetString(dr["PRODUCTDEFVERSION"]));
                                             }
                                         });

            productDefPopup.Conditions.AddTextBox("PRODUCTDEF");

            productDefPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150).SetLabel("PRODUCTDEFID");
            productDefPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 150).SetLabel("PRODUCTDEFNAME");
            productDefPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 100).SetLabel("PRODUCTDEFVERSION");

            #endregion

            grdMain.View.AddTextBoxColumn("PRODUCTDEFVERSION", 60)
                   .SetValidationKeyColumn()
                   .SetValidationIsRequired()
                   .SetIsReadOnly()
                   .SetTextAlignment(TextAlignment.Left);

            grdMain.View.AddTextBoxColumn("WIDTH", 60)
                   .SetDisplayFormat("{0:f2}", MaskTypes.Custom)
                   .SetIsReadOnly()
                   .SetTextAlignment(TextAlignment.Right);

            grdMain.View.AddTextBoxColumn("HEIGHT", 60)
                   .SetDisplayFormat("{0:f2}", MaskTypes.Custom)
                   .SetIsReadOnly()
                   .SetTextAlignment(TextAlignment.Right);

            grdMain.View.AddTextBoxColumn("FILENAME", 110)
                   .SetIsReadOnly()
                   .SetIsHidden()
                   .SetTextAlignment(TextAlignment.Left);

            grdMain.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                   .SetDefault("Valid")
                   .SetTextAlignment(TextAlignment.Center);

            grdMain.View.AddTextBoxColumn("FILEDATA", 10).SetIsHidden();
            grdMain.View.AddTextBoxColumn("FILESIZE", 10).SetIsHidden();
            grdMain.View.AddTextBoxColumn("FILEEXT", 10).SetIsHidden();
            grdMain.View.AddTextBoxColumn("FILEPATH", 10).SetIsHidden();
            grdMain.View.AddTextBoxColumn("SAFEFILENAME", 10).SetIsHidden();
            grdMain.View.AddTextBoxColumn("LOCALFILEPATH", 10).SetIsHidden();
            grdMain.View.AddTextBoxColumn("PROCESSINGSTATUS", 10).SetIsHidden();

            grdMain.View.PopulateColumns();
            grdMain.View.BestFitColumns();

            grdMain.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export;
        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            //! 기준정보 품목항목 삭제시 버젼도 삭제
            Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").EditValueChanged += (s, e) =>
            {
                if (string.IsNullOrEmpty(Format.GetFullTrimString((s as SmartSelectPopupEdit).EditValue)))
                {
                    Conditions.GetControl<SmartTextBox>("P_PRODUCTDEFVERSION").EditValue = string.Empty;
                }
            };

            //! 변환 버튼 이벤트
            btnConversion.Click += (s, e) =>
            {
                OpenFileDialog dialog = new OpenFileDialog()
                {
                    DefaultExt = "gbr",
                    Filter = "GBR File(*.gbr)|*.gbr"
                };

                if (dialog.ShowDialog().Equals(DialogResult.OK))
                {
                    try
                    {
                        DialogManager.ShowWaitArea(this.pnlContent);

                        if (Gerber.SaveGerberFileToImage(dialog.FileName, dialog.FileName, 200, Color.DarkGray, Color.White) is ParsedGerber parsedGerber)
                        {
                            if (grdMain.View.GetDataRow(grdMain.View.GetFocusedDataSourceRowIndex()) is DataRow dr)
                            {
                                string createFilePath = FileCreate(parsedGerber.TopBitmap);

                                dr["FILENAME"] = Path.GetFileNameWithoutExtension(createFilePath);
                                dr["FILEDATA"] = Convert.ToBase64String((byte[])new ImageConverter().ConvertTo(parsedGerber.TopBitmap, typeof(byte[])));
                                dr["FILESIZE"] = new FileInfo(createFilePath).Length;
                                dr["FILEEXT"] = "jpg";
                                dr["FILEPATH"] = "PCSIMAGE";
                                dr["SAFEFILENAME"] = Path.GetFileName(createFilePath);
                                dr["PROCESSINGSTATUS"] = "Wait";
                                dr["LOCALFILEPATH"] = createFilePath;

                                dr["WIDTH"] = Format.GetString(parsedGerber.PanelWidth);
                                dr["HEIGHT"] = Format.GetString(parsedGerber.PanelHeight);

                                SetImageView(dr);

                                txtPath.Text = dialog.SafeFileName;
                            }
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
            };

            //! Grid 클릭 이벤트
            grdMain.View.FocusedRowChanged += (s, e) =>
            {
                if (e.FocusedRowHandle < 0)
                {
                    return;
                }

                try
                {
                    DialogManager.ShowWaitArea(this.pnlContent);
                    SetImageView(grdMain.View.GetDataRow(grdMain.View.GetFocusedDataSourceRowIndex()));

                    btnConversion.Enabled = true;
                }
                catch (Exception ex)
                {
                    throw MessageException.Create(Format.GetString(ex));
                }
                finally
                {
                    DialogManager.CloseWaitArea(this.pnlContent);
                }
            };

            //! Grid에 Data가 없으면 상세의 내용 삭제
            grdMain.ToolbarDeleteRow += (s, e) => SetClear();

            //! PictureEdit에서 마우스 버튼 다운 이벤트
            picMain.MouseDown += (s, e) =>
            {
                ResetRectangle();

                if (!e.Button.Equals(MouseButtons.Left))
                {
                    return;
                }

                mouseDownPoint = e.Location;
                (s as SmartPictureEdit).Refresh();
            };

            // PictureEdit에서 마우스 이동 이벤트
            picMain.MouseMove += (s, e) =>
            {
                if (mouseDownPoint.IsEmpty)
                {
                    return;
                }

                if (!e.Button.Equals(MouseButtons.Left))
                {
                    return;
                }

                Point mouseMovePoint = e.Location;
                SmartPictureEdit edit = s as SmartPictureEdit;
                Point ptMove = edit.ViewportToImage(mouseMovePoint);

                cropRacktangle = new Rectangle(mouseDownPoint.X, mouseDownPoint.Y, mouseMovePoint.X - mouseDownPoint.X, mouseMovePoint.Y - mouseDownPoint.Y);

                edit.Refresh();
            };

            //! PictureEdit Paint 이벤트
            picMain.Paint += (s, e) => e.Graphics.DrawRectangle(new Pen(Color.Red), cropRacktangle);

            //! PictureEdit에서 줌 퍼센트 변경 이벤트
            picMain.ZoomPercentChanged += (s, e) => ResetRectangle();

            //! 스크롤 이동시 발생 이벤트
            picMain.HScrollBar.ValueChanged += (s, e) => ResetRectangle();
            picMain.VScrollBar.ValueChanged += (s, e) => ResetRectangle();

            //! 자르기 버튼 이벤트
            btnCrop.Click += (s, e) =>
            {
                if (picMain.Image == null)
                {
                    return;
                }

                if (ShowMessage(MessageBoxButtons.YesNo, "CropPCSImage").Equals(DialogResult.Yes))
                {
                    Point start = picMain.ViewportToImage(cropRacktangle.Location);
                    Point end = picMain.ViewportToImage(new Point(cropRacktangle.Right, cropRacktangle.Bottom));
                    Rectangle selectedImageRectangle = new Rectangle(start.X, start.Y, end.X - start.X, end.Y - start.Y);

                    if (start.X < 0 || start.Y < 0)
                    {
                        ShowMessage("NoSelectCropArea");
                        ResetRectangle();

                        return;
                    }

                    if (end.X > picMain.Image.Size.Width || end.Y > picMain.Image.Size.Height)
                    {
                        ShowMessage("WrongCropArea");
                        ResetRectangle();

                        return;
                    }

                    if (selectedImageRectangle.Size.Width <= 0 || selectedImageRectangle.Size.Height <= 0)
                    {
                        ShowMessage("WrongCropArea");
                        ResetRectangle();

                        return;
                    }

                    Bitmap selectedImage = new Bitmap(picMain.Image);
                    selectedImage = selectedImage.Clone(selectedImageRectangle, selectedImage.PixelFormat);
                    picMain.Image = selectedImage;

                    if (grdMain.View.GetDataRow(grdMain.View.GetFocusedDataSourceRowIndex()) is DataRow dr)
                    {
                        if (Gerber.SaveExternalImage(selectedImage, 200, selectedImageRectangle.Size.Width, selectedImageRectangle.Size.Height) is ParsedGerber parsedGerber)
                        {
                            string createFilePath = FileCreate(selectedImage);

                            dr["FILENAME"] = Path.GetFileNameWithoutExtension(createFilePath);
                            dr["FILEDATA"] = Convert.ToBase64String((byte[])new ImageConverter().ConvertTo(this.picMain.Image, typeof(byte[])));
                            dr["FILESIZE"] = new FileInfo(createFilePath).Length;
                            dr["FILEEXT"] = "jpg";
                            dr["FILEPATH"] = "PCSIMAGE";
                            dr["SAFEFILENAME"] = Path.GetFileName(createFilePath);
                            dr["PROCESSINGSTATUS"] = "Wait";
                            dr["LOCALFILEPATH"] = createFilePath;

                            dr["WIDTH"] = Format.GetString(parsedGerber.PanelWidth);
                            dr["HEIGHT"] = Format.GetString(parsedGerber.PanelHeight);

                            SetImageView(dr);
                        }
                    }

                    ResetRectangle();
                }
            };
        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            if (!FileUpload())
            {
                return;
            }

            ExecuteRule("QcpcsimageManagement", grdMain.GetChangedRows());

            // 생성한 폴더가 존재하면 삭제한다.
            if (new DirectoryInfo(_LOCALFILEPATH).Exists)
            {
                Directory.Delete(_LOCALFILEPATH, true);
            }
        }

        #endregion

        #region 검색

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            try
            {
                ResetRectangle();

                DialogManager.ShowWaitArea(this.pnlContent);

                await base.OnSearchAsync();

                if (await SqlExecuter.QueryAsync("GetPCSImage", "10001", Conditions.GetValues()) is DataTable dt)
                {
                    if (dt.Rows.Count.Equals(0))
                    {
                        grdMain.DataSource = null;
                        SetClear();

                        ShowMessage("NoSelectData");
                        return;
                    }

                    grdMain.DataSource = dt;
                    grdMain.View.FocusedRowHandle = 0;
                    SetImageView(grdMain.View.GetDataRow(0));

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

        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            #region 품목

            var productDefPopup = Conditions.AddSelectPopup("P_PRODUCTDEFID", new SqlQuery("GetProductDefList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"PRODUCTDEFTYPE=Product"), "PRODUCTDEFID", "PRODUCTDEFID")
                                            .SetLabel("PRODUCTDEFID")
                                            .SetPosition(0)
                                            .SetDefault(null, null, null)
                                            .SetPopupResultCount(1)
                                            .SetPopupLayout("PRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)
                                            .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                                            .SetPopupApplySelection((selectedRow, dataGirdRow) =>
                                            {
                                                foreach (DataRow row in selectedRow)
                                                {
                                                    if (selectedRow.Count() > 0)
                                                    {
                                                        this.Conditions.GetControl<SmartTextBox>("P_PRODUCTDEFVERSION").EditValue = row["PRODUCTDEFVERSION"].ToString();
                                                    }
                                                }
                                            });

            productDefPopup.Conditions.AddTextBox("PRODUCTDEF");

            productDefPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150).SetLabel("PRODUCTDEFID");
            productDefPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 150).SetLabel("PRODUCTDEFNAME");
            productDefPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 100).SetLabel("PRODUCTDEFVERSION");

            Conditions.AddTextBox("P_PRODUCTDEFVERSION").SetLabel("PRODUCTDEFVERSION").SetPosition(0.1).SetIsReadOnly();

            #endregion
        }

        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();
        }

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            grdMain.View.CheckValidation();

            if (grdMain.DataSource == null)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }

            if (grdMain.GetChangedRows().Rows.Count.Equals(0))
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }

        #endregion

        #region Private Function

        /// <summary>
        /// 상세(Detail) 화면의 내용을 초기화 한다.
        /// </summary>
        private void SetClear()
        {
            txtWidth.Text = "";
            txtHeight.Text = "";
            txtPath.Text = "";
            picMain.Image = null;
        }

        /// <summary>
        /// 자르기 전역변수 초기화
        /// </summary>
        private void ResetRectangle()
        {
            cropRacktangle = Rectangle.Empty;
            mouseDownPoint = Point.Empty;
        }

        /// <summary>
        /// 거버 파일 로컬에 생성 후 경로 리턴
        /// </summary>
        /// <param name="parsedGerber"></param>
        /// <returns></returns>
        private string FileCreate(Bitmap bmp)
        {
            DirectoryInfo dir = new DirectoryInfo(_LOCALFILEPATH);
            if (!dir.Exists)
            {
                dir.Create();
            }

            Random rand = new Random();
            string cyInput = "abcdefghijklmnopqrstuvwxyz0123456789";
            string fileName = new string(Enumerable.Range(0, 30).Select(x => cyInput[rand.Next(0, cyInput.Length)]).ToArray());

            string createFilePath = Path.Combine(_LOCALFILEPATH, string.Join(".", fileName, "jpg"));

            using (FileStream fs = File.Create(createFilePath))
            {
                byte[] bt = (byte[])new ImageConverter().ConvertTo(bmp, typeof(byte[]));
                fs.Write(bt, 0, bt.Length);
            }

            return createFilePath;
        }

        /// <summary>
        /// 저장 시 파일 업로드
        /// </summary>
        /// <returns></returns>
        private bool FileUpload()
        {
            int totalSize = 0;
            DataTable finalDt = (grdMain.DataSource as DataTable).Clone();

            foreach (DataRow dr in grdMain.GetChangedRows().Rows)
            {
                if (Format.GetString(dr["_STATE_"]).Equals("deleted"))
                {
                    continue;
                }

                if (string.IsNullOrEmpty(Format.GetString(dr["FILENAME"])))
                {
                    throw MessageException.Create("ValidationImage");
                }

                finalDt.ImportRow(dr);
                totalSize += Format.GetInteger(dr["FILESIZE"]);
            }

            if (totalSize.Equals(0))
            {
                return true;
            }

            FileProgressDialog dialog = new FileProgressDialog(finalDt, UpDownType.Upload, string.Empty, totalSize);

            if (!dialog.ShowDialog().Equals(DialogResult.OK))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Grid에 선택한 Row의 이미지를 보여준다.
        /// </summary>
        /// <param name="dr"></param>
        private void SetImageView(DataRow dr)
        {
            SetClear();
            ResetRectangle();

            txtWidth.Text = Format.GetString(dr["WIDTH"]) ?? string.Empty;
            txtHeight.Text = Format.GetString(dr["HEIGHT"]) ?? string.Empty;

            if (string.IsNullOrEmpty(Format.GetString(dr["FILENAME"])))
            {
                return;
            }

            if (string.IsNullOrEmpty(Format.GetString(dr["FILEDATA"])))
            {
                string serverPath = Format.GetString(dr.GetObject("FILEPATH"));
                string file = string.Join(".", Format.GetString(dr.GetObject("FILENAME")), Format.GetString(dr.GetObject("FILEEXT")));

                picMain.Image = CommonFunction.GetFtpImageFileToBitmap(serverPath, file, (int)Format.GetDouble(dr["WIDTH"], 0), (int)Format.GetDouble(dr["HEIGHT"], 0));
            }
            else
            {
                picMain.Image = (Bitmap)new ImageConverter().ConvertFrom(Convert.FromBase64String(Format.GetString(dr["FILEDATA"])));
            }

            // Calculate an initial zoom factor so the whole image is displayed. The factors 12 and 2  
            //  were determined empirically, and I have no idea what they represent.  
            picMain.Properties.ZoomPercent = Math.Min(100, 
                                                      Math.Min((picMain.Width * 100) / picMain.Image.Width, 
                                                               (picMain.Height * 100) / picMain.Image.Height));
        }

        #endregion
    }
}