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
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons;
using Micube.Framework;

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 자주/최종/AOI 이상발생
    /// 업  무  설  명  : 자주검사의 사진 userControl
    /// 생    성    자  : 강유라
    /// 생    성    일  : 2019-11-07
    /// 수  정  이  력  : 2019-12-19 유태근 이미지위에 특정 텍스트 입력하는 함수 추가
    /// 
    /// 
    /// </summary>
    public partial class ucSelfImageControl : UserControl
    {
        #region Local Variables
        public string strFile { get; set; }
        private int reSize = 100;

        #endregion

        #region 생성자

        public ucSelfImageControl(string filePath, string strFile)
        {
            InitializeComponent();

            picMeasurePrinted.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            //picMeasurePrinted.Size = picMeasurePrinted.Image.Size;

            try
            {
                picMeasurePrinted.Image = CommonFunction.GetFtpImageFileToBitmap(filePath, strFile);
            } catch(Exception e)
            {
                MSGBox.Show(MessageBoxType.Information, "SmartMES", "NoImageFormat");
            }

            this.strFile = strFile;

            picMeasurePrinted.Dock = DockStyle.Fill;

            /*
            CustomizedToolTip myToolTip1 = new CustomizedToolTip();
            myToolTip1.SetToolTip(picMeasurePrinted, @"picMeasurePrinted. Formatted string (with image)
                                                        Created using the verbatim character '@'. End");

            //Object oj = picMeasurePrinted.SvgImage
            picMeasurePrinted.Tag = picMeasurePrinted.Image;
            */
            InitializeEvent();
        }

        #endregion

        #region Event

        /// <summary>
        /// 이벤트 초기화
        /// </summary>
        private void InitializeEvent()
        {
            //마우스 호버이벤트
            picMeasurePrinted.MouseEnter += PicMeasurePrinted_MouseHover;

            picMeasurePrinted.MouseLeave += PicMeasurePrinted_MouseLeave;
        }

        /// <summary>
        /// 이미지 크기 작게하는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PicMeasurePrinted_MouseLeave(object sender, EventArgs e)
        {
            SmartPictureEdit edit = sender as SmartPictureEdit;
            int controlWidth = edit.Parent.Parent.Width;
            int controlHeight = edit.Parent.Parent.Height;

            edit.Parent.Parent.Width = controlWidth - reSize;
            edit.Parent.Parent.Height = controlHeight - reSize;
        }

        /// <summary>
        /// 이미지 크기 크게하는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PicMeasurePrinted_MouseHover(object sender, EventArgs e)
        {
            SmartPictureEdit edit = sender as SmartPictureEdit;
            int controlWidth = edit.Parent.Parent.Width;
            int controlHeight = edit.Parent.Parent.Height;

            edit.Parent.Parent.Width = controlWidth + reSize;
            edit.Parent.Parent.Height = controlHeight + reSize;
        }

        #endregion

        #region Public Function
        public Image selectImage()
        {
            return picMeasurePrinted.Image;
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
        /// 이미지 위에 특정한 텍스트를 입력한다.
        /// </summary>
        public void SetImageTextValue(string text)
        {
            lblValue.Text = text;
        }

        #endregion  
    }
}
