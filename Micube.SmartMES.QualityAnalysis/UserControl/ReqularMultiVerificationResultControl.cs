using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using DevExpress.Utils;
using System.Diagnostics;
using System.Windows.Forms;
using DevExpress.XtraEditors.Repository;
using Micube.SmartMES.QualityAnalysis.KeyboardClipboard;
using System.Drawing.Imaging;
using Micube.Framework.SmartControls;
using Micube.Framework.Net;
using Micube.Framework;

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 신뢰성 검증 결과 User Control
    /// 업  무  설  명  : 품질관리에서 사용되는 신뢰성 검증 결과 User Control이다.
    /// 생    성    자  : 유석진
    /// 생    성    일  : 2019-07-29 
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ReqularMultiVerificationResultControl : UserControl
    {
        #region Local Variables

        public Bitmap image { get; set; }

        public string strFile { get; set; }

        private string[] formats = Enum.GetNames(typeof(ClipboardFormat));

        private FlowLayoutPanel _parnet;

        private int _originWidth;
        private int _originHeight;
        private int _hScrollValue;

        #endregion
        public ReqularMultiVerificationResultControl()
        {
            InitializeComponent();

            picMeasurePrinted.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            //picMeasurePrinted.Size = picMeasurePrinted.Image.Size;

            CustomizedToolTip myToolTip1 = new CustomizedToolTip();
            myToolTip1.SetToolTip(picMeasurePrinted, @"picMeasurePrinted. Formatted string (with image)
                                                        Created using the verbatim character '@'. End");
            picMeasurePrinted.Tag = image;

            InitializeEvent();
            InitializeGrid();
        }

        public ReqularMultiVerificationResultControl(Bitmap image, string strFile)
        {
            InitializeComponent();

            /*
            RepositoryItemTextEdit edit = new RepositoryItemTextEdit();
            edit.Mask.EditMask = "\\d+";
            edit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            */
            //txtValue.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            //txtValue.Properties.Mask.EditMask = "#,###,##0.####";
            //txtValue.Properties.Mask.UseMaskAsDisplayFormat = true;

            picMeasurePrinted.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            //picMeasurePrinted.Size = picMeasurePrinted.Image.Size;

            picMeasurePrinted.Dock = DockStyle.Fill;
            picMeasurePrinted.Image = image;
            this.image = image;
            this.strFile = strFile;

            CustomizedToolTip myToolTip1 = new CustomizedToolTip();
            myToolTip1.SetToolTip(picMeasurePrinted, @"picMeasurePrinted. Formatted string (with image)
                                                        Created using the verbatim character '@'. End");
            picMeasurePrinted.Tag = image;

            InitializeEvent();
            InitializeGrid();
        }

        public ReqularMultiVerificationResultControl(Bitmap image, string strFile, FlowLayoutPanel panel)
        {
            _parnet = panel;

            InitializeComponent();

            /*
            RepositoryItemTextEdit edit = new RepositoryItemTextEdit();
            edit.Mask.EditMask = "\\d+";
            edit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            */
            //txtValue.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            //txtValue.Properties.Mask.EditMask = "#,###,##0.####";
            //txtValue.Properties.Mask.UseMaskAsDisplayFormat = true;

            picMeasurePrinted.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            //picMeasurePrinted.Size = picMeasurePrinted.Image.Size;

            picMeasurePrinted.Dock = DockStyle.Fill;
            picMeasurePrinted.Image = image;
            this.image = image;
            this.strFile = strFile;

            CustomizedToolTip myToolTip1 = new CustomizedToolTip();
            myToolTip1.SetToolTip(picMeasurePrinted, @"picMeasurePrinted. Formatted string (with image)
                                                        Created using the verbatim character '@'. End");
            picMeasurePrinted.Tag = image;

            InitializeEvent();
            InitializeGrid();
        }
        #region Event

        /// <summary>
        /// 이벤트 초기화
        /// </summary>
        private void InitializeEvent()
        {
            this.Load += MultiVerificationResultControl_Load;
            btnAdd.Click += BtnAdd_Click;
            btnDelete.Click += BtnDelete_Click;
            btnClose.Click += BtnClose_Click;
            smartGroupBox1.HeaderButtonClickEvent += SmartGroupBox1_HeaderButtonClickEvent;
           
            grdResultValue.HeaderButtonClickEvent += GrdResultValue_HeaderButtonClickEvent;
            grdMeasureValue.HeaderButtonClickEvent += GrdMeasureValue_HeaderButtonClickEvent;
        }

        // 측정값 그리드 확대, 축소 버튼 선택 시 실행
        private void GrdMeasureValue_HeaderButtonClickEvent(object sender, HeaderButtonClickArgs args)
        {
            if (args.ClickItem == GridButtonItem.Expand) // 확장
            {
                grdMeasureValue.Parent = smartGroupBox1;
                grdMeasureValue.BringToFront();
            }
            else if (args.ClickItem == GridButtonItem.Restore) // 축소
            {
                grdMeasureValue.Parent = smartSplitTableLayoutPanel1;
                grdMeasureValue.BringToFront();
            }
        }

        // 결과값 그리드 확대, 축소 버튼 선택 시 실행
        private void GrdResultValue_HeaderButtonClickEvent(object sender, HeaderButtonClickArgs args)
        {
            if (args.ClickItem == GridButtonItem.Expand) // 확장
            {
                grdResultValue.Parent = smartGroupBox1;
                grdResultValue.BringToFront();
            }
            else if (args.ClickItem == GridButtonItem.Restore) // 축소
            {
                grdResultValue.Parent = smartSplitTableLayoutPanel1;
                grdResultValue.BringToFront();
            }
        }

        private void MultiVerificationResultControl_Load(object sender, EventArgs e)
        {
            _originWidth = this.Width;
            _originHeight = this.Height;

            // 2020.04.24-유석진-결과값 검증항목 그리드 맵핑
            setInspItemId(grdResultValue, "HoleResultInspItemId");
            // 2020.04.24-유석진-측정값 검증항목 그리드 맵핑
            setInspItemId(grdMeasureValue, "HoleInspItemid");

        }

        // 클릭시 화면 확대, 축소
        private void SmartGroupBox1_HeaderButtonClickEvent(object sender, HeaderButtonClickArgs args)
        {
            if(args.ClickItem == GridButtonItem.Expand) // 확대
            {
                _hScrollValue = _parnet.VerticalScroll.Value;
                this.Parent = _parnet;
                this.Size = new Size(_parnet.Width - 10, _parnet.Height - 10);
                // 해당 폼을 제외한 다른 폼들 비활성화
                setControlVisible(sender, false);
            } else if (args.ClickItem == GridButtonItem.Restore) // 축소
            {
                this.Size = new Size(_originWidth, _originHeight);
                // 해당 폼, 다른 폼들 활성화
                setControlVisible(sender, true);
                _parnet.VerticalScroll.Value = _hScrollValue;
                _parnet.PerformLayout();
            }
        }

        private void GrdMeasureValue_ToolbarAddingRow(object sender, CancelEventArgs e)
        {
            if (grdMeasureValue.View.RowCount >= 10)
            {
                e.Cancel = true;
            }
        }

        private void GrdMeasureValue_ToolbarDeleteRow(object sender, EventArgs e)
        {
            try
            {
                //grdMeasureValue.View.GetFocusedDataRow().Delete();
                DataTable dt = grdMeasureValue.DataSource as DataTable;
                dt.Rows.Remove(grdMeasureValue.View.GetFocusedDataRow());                
            } catch(Exception ex)
            {
            }
        }

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            #region 결과값 그리드 초기화 
            grdResultValue.ShowStatusBar = false;
            grdResultValue.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Restore;

            grdResultValue.View.AddTextBoxColumn("MEASUREITEMID", 120)
                .SetIsHidden();

            grdResultValue.View.AddTextBoxColumn("MEASUREITEMNAME", 120)
                .SetLabel("INSPECTIONITEM");

            grdResultValue.View.AddComboBoxColumn("MEASUREVALUE", 80, new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "CODECLASSID=OKNGNAFLOAT"))
                .SetLabel("SPCRULESSTATUS")
                .SetTextAlignment(TextAlignment.Center);

            grdResultValue.View.PopulateColumns();
            #endregion

            #region 측정값 그리드 초기화            
            grdMeasureValue.ShowStatusBar = false;
            grdMeasureValue.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Restore;

            grdMeasureValue.View.AddTextBoxColumn("MEASUREITEMID", 120)
                .SetIsHidden();

            grdMeasureValue.View.AddTextBoxColumn("MEASUREITEMNAME", 120)
                .SetLabel("INSPECTIONITEM");

            grdMeasureValue.View.AddTextBoxColumn("MEASUREVALUE", 80)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,###,##0.####", MaskTypes.Numeric);

            grdMeasureValue.View.PopulateColumns();

            RepositoryItemTextEdit edit = new RepositoryItemTextEdit();
            edit.Mask.EditMask = "#,###,##0.####";
            edit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            edit.Mask.UseMaskAsDisplayFormat = true;

            grdMeasureValue.View.Columns["MEASUREVALUE"].ColumnEdit = edit;
            #endregion
        }

        /// <summary>
        /// 이미지 추가
        /// </summary>
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            //// Get the DataObject.
            //IDataObject data_object = Clipboard.GetDataObject();

            //// Look for a file drop.
            //if (data_object.GetDataPresent(DataFormats.FileDrop))
            //{
            //    string[] files = (string[])
            //        data_object.GetData(DataFormats.FileDrop);
            //    foreach (string file in files)
            //    {
            //        Bitmap image = new Bitmap(file);
            //        this.image = image;
            //        this.strFile = file;
            //        picMeasurePrinted.Image = image;
            //        picMeasurePrinted.Tag = image;
            //    }
            //}
            //----------------------------------------
            var iData = Clipboard.GetDataObject();

            ClipboardFormat? format = null;

            foreach (var f in formats)
                if (iData.GetDataPresent(f))
                {
                    format = (ClipboardFormat)Enum.Parse(typeof(ClipboardFormat), f);
                    break;
                }

            var data = iData.GetData(format.ToString());

            if (data == null || format == null)
                return;

            if ((ClipboardFormat)format == ClipboardFormat.FileDrop)
            {
                string[] files = (string[])data;
                //foreach (string file_name in files)
                for (int i = files.Length - 1; i >= 0; i--)
                {
                    Bitmap image = new Bitmap(files[i]);
                    this.image = image;
                    this.strFile = files[i];
                    picMeasurePrinted.Image = image;
                    picMeasurePrinted.Tag = image;
                }
            }
            else if ((ClipboardFormat)format == ClipboardFormat.Bitmap)
            {
                Bitmap image = (Bitmap)data;
                string sBitmapName = "ClipboardBitmap-" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".png";
                this.image = image;
                this.strFile = sBitmapName;
                picMeasurePrinted.Image = image;
                picMeasurePrinted.Tag = image;
                image.Save(sBitmapName, ImageFormat.Png);
            }

            this.Refresh();
        }

        /// <summary>
        /// 이미지 삭제
        /// </summary>
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            picMeasurePrinted.Image.Dispose();
            FileInfo fileInfo = new FileInfo(strFile);
            if (strFile.StartsWith("ClipboardBitmap-") && fileInfo.Exists)
                File.Delete(strFile);

            this.strFile = string.Empty;
            this.Refresh();
        }

        /// <summary>
        /// 화면 닫기
        /// </summary>
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        #endregion

        #region Public Function

        /// <summary>
        /// 타이틀 가져오기
        /// </summary>
        public string selectTitle()
        {
            return txtTitle.Text;
        }

        public void setTitle(string title)
        {
            txtTitle.Text = title;
        }

        /// <summary>
        /// 측정 값 가져오기
        /// </summary>
        public string selectValue()
        {
            string str = string.Empty;
            //if (txtValue.EditValue != null)
            //    str = txtValue.EditValue.ToString();

            return str;
        }

        public void setValue(string measureValue)
        {
            double v = 0;
            //if(double.TryParse(measureValue, out  v))
            //    txtValue.EditValue = v;
        }

        public Image selectImage()
        {
            return picMeasurePrinted.Image;
        }

        /// <summary>
        /// 파일 사이즈
        /// </summary>
        public long lngFileSize()
        {
            var fileInfo = new FileInfo(strFile);

            return fileInfo.Length;
        }

        /// <summary>
        /// 파일 이름(확장자 포함)
        /// </summary>
        public string strFileName()
        {
            var fileInfo = new FileInfo(strFile);

            return fileInfo.Name;
        }

        /// <summary>
        /// 파일 확장자
        /// </summary>
        public string strFileExtention()
        {
            var fileInfo = new FileInfo(strFile);

            return fileInfo.Extension;
        }

        /// <summary>
        /// 해당 그리드에 검사항목 맵핑
        /// </summary>
        public void setInspItemId(SmartBandedGrid grid, string codeclassid)
        {
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("P_CODECLASSID", codeclassid);
            DataTable dt = SqlExecuter.Query("GetHoleResultMeasureInspItemidList", "10001", values);

            grid.DataSource = dt;
        }

        /// <summary>
        /// 확장시 해당 폼 활성화, 다른 폼들 비활성화
        /// 축소시 해당 폼, 다른 폼들 활성화 하는 기능
        /// </summary>
        private void setControlVisible(object sender, bool _visible)
        {
            var enumerator = _parnet.Controls.GetEnumerator();

            while (enumerator.MoveNext())
            {
                ReqularMultiVerificationResultControl vr = enumerator.Current as ReqularMultiVerificationResultControl;

                if (vr != (sender as SmartGroupBox).Parent)
                {
                    vr.Visible = _visible;
                }
            }
        }

        #endregion  
    }
}
