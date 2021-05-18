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
    public partial class VerificationResultControl : UserControl
    {
        #region Local Variables

        public Bitmap image { get; set; }

        public string strFile { get; set; }

        private string[] formats = Enum.GetNames(typeof(ClipboardFormat));

        #endregion
        public VerificationResultControl()
        {
            InitializeComponent();

            picMeasurePrinted.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            //picMeasurePrinted.Size = picMeasurePrinted.Image.Size;

            CustomizedToolTip myToolTip1 = new CustomizedToolTip();
            myToolTip1.SetToolTip(picMeasurePrinted, @"picMeasurePrinted. Formatted string (with image)
                                                        Created using the verbatim character '@'. End");
            picMeasurePrinted.Tag = image;

            InitializeEvent();
        }

        public VerificationResultControl(Bitmap image, string strFile)
        {
            InitializeComponent();

            /*
            RepositoryItemTextEdit edit = new RepositoryItemTextEdit();
            edit.Mask.EditMask = "\\d+";
            edit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            */
            txtValue.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txtValue.Properties.Mask.EditMask = "#,###,##0.####";
            //txtValue.Properties.DisplayFormat.FormatString = "#,###,###";
            //txtValue.Properties.DisplayFormat.FormatType = FormatType.Numeric;
            //txtValue.Properties.EditFormat.FormatString = "#,###,###";
            //txtValue.Properties.EditFormat.FormatType = FormatType.Numeric;
            //txtValue.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            txtValue.Properties.Mask.UseMaskAsDisplayFormat = true;

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
        }

        #region Event

        /// <summary>
        /// 이벤트 초기화
        /// </summary>
        private void InitializeEvent()
        {            
            btnAdd.Click += BtnAdd_Click;
            btnDelete.Click += BtnDelete_Click;
            btnClose.Click += BtnClose_Click;
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
            if (txtValue.EditValue != null)
                str = txtValue.EditValue.ToString();

            return str;
        }

        public void setValue(string measureValue)
        {
            double v = 0;
            if(double.TryParse(measureValue, out  v))
                txtValue.EditValue = v;
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

        #endregion  
    }
}
