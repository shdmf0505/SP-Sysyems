using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Micube.SmartMES.StandardInfo
{
    public partial class SparepartImagePopup : SmartPopupBaseForm, ISmartCustomPopup
    {

        #region Interface

        public DataRow CurrentDataRow { get; set; }

        public Image Image { get; set; }

        #endregion

        #region Local Variables
        public DataTable checkTable;
        #endregion

        #region 생성자
        public SparepartImagePopup()
        {
            InitializeComponent();
            InitializeControl();
            InitializeEvent();
        }
        #endregion

        #region 컨텐츠 영역 초기화
        private void InitializeControl()
        {

        }
        #endregion

        #region Event

        /// <summary>
        /// Event 초기화 
        /// </summary>
        private void InitializeEvent()
        {
            Shown += SparepartImagePopup_Shown;

            //검색버튼 클릭 이벤트
            btnFileRegister.Click += BtnFileRegister_Click;

            //파일 삭제
            btnFileDel.Click += BtnFileDel_Click;

            //취소버튼 클릭 이벤트
            btnCancel.Click += (s, e) =>
            {
                this.DialogResult = DialogResult.Cancel;

                if (!string.IsNullOrEmpty(this.CurrentDataRow.GetString("IMAGE")))
                {
                    ImageConverter converter = new ImageConverter();
                    this.Image = (Image)converter.ConvertFrom(this.CurrentDataRow["IMAGE"]);
                }
                else
                {
                    this.Image = null;

                }

                this.Close();
            };

            //확인버튼 클릭 이벤트
            btnOK.Click += BtnOK_Click;
        }

        private void SparepartImagePopup_Shown(object sender, EventArgs e)
        {
            this.txtSparepartID.Text = this.CurrentDataRow.GetString("SPAREPARTID");

            string strSparepartName = string.Empty;
            switch (UserInfo.Current.LanguageType)
            {
                case "en-US":
                    strSparepartName = this.CurrentDataRow.GetString("SPAREPARTNAME$$EN-US");
                    break;
                case "zh-CN":
                    strSparepartName = this.CurrentDataRow.GetString("SPAREPARTNAME$$ZH-CN");
                    break;
                case "vi-VN":
                    strSparepartName = this.CurrentDataRow.GetString("SPAREPARTNAME$$VI-VN");
                    break;
                case "ko-KR":
                default:
                    strSparepartName = this.CurrentDataRow.GetString("SPAREPARTNAME$$KO-KR");
                    break;
            }
            this.txtSparepartName.Text = strSparepartName; 

            if (!string.IsNullOrEmpty(this.CurrentDataRow.GetString("IMAGE")))
            {
                ImageConverter converter = new ImageConverter();
                picbox.Image = (Image)converter.ConvertFrom(this.CurrentDataRow["IMAGE"]);
            }
        }

        /// <summary>
        /// 확인 버튼 클릭시 체크된 행들을 checkTable에 넘기는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOK_Click(object sender, EventArgs e)
        {
            DialogResult result = System.Windows.Forms.DialogResult.No;
            //선택된 이미지를 적용하시겠습니까?
            result = this.ShowMessage(MessageBoxButtons.YesNo, "SelectImageConfirm");

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    this.ShowWaitArea();
                    btnOK.Enabled = false;
                    btnCancel.Enabled = false;
                    //팝업 확인버튼 클릭시 DialogResult를 OK로 설정 부모 창에서 쓰기 위함
                    this.DialogResult = DialogResult.OK;

                    this.Image = this.picbox.Image;

                    if (this.picbox.Image != null)
                    {
                        this.CurrentDataRow["ISIMAGE"] = "Y";
                        this.CurrentDataRow["IMAGE"] = (byte[])new ImageConverter().ConvertTo(new Bitmap(this.picbox.Image), typeof(byte[]));
                    }
                    else
                    {
                        this.CurrentDataRow["ISIMAGE"] = "N";
                        this.CurrentDataRow["IMAGE"] = null;
                    }
                }
                catch (Exception ex)
                {
                    this.ShowError(ex);
                }
                finally
                {
                    this.CloseWaitArea();
                    btnOK.Enabled = true;
                    btnCancel.Enabled = true;
                    this.Close();
                }
            }
     
        }

        /// <summary>
        /// 파일 등록 클릭시 파일 선택 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFileRegister_Click(object sender, EventArgs e)
        {
            if (this.picbox.EditValue != null)
            {
                // 이미 등록된 이미지를 변경하시겠습니까??
                if (this.ShowMessage(MessageBoxButtons.YesNo, "ChangeImage") == DialogResult.No)
                    return;
            }

            ImageFileRegister();
        }

        /// <summary>
        /// 파일 삭제 클릭시 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFileDel_Click(object sender, EventArgs e)
        {
            if (this.picbox.EditValue != null)
            {
                // 이미 등록된 이미지를 변경하시겠습니까??
                if (this.ShowMessage(MessageBoxButtons.YesNo, "DeleteImage") == DialogResult.No)
                    return;
            }
        }
        #endregion

        #region Public Function

        /// <summary>
        /// 파일 첨부
        /// </summary>
        private void ImageFileRegister()
        {
            try
            {
                DialogManager.ShowWaitArea(this);

                string imageFile = string.Empty;

                OpenFileDialog dialog = new OpenFileDialog
                {
                    Multiselect = false,
                    Filter = "Image Files (*.bmp, *.jpg, *.jpeg, *.png)|*.BMP;*.JPG;*.JPEG;*.PNG",
                    FilterIndex = 0
                };

                if (dialog.ShowDialog() == DialogResult.OK)
                {

                    imageFile = dialog.FileName;
                    FileInfo fileInfo = new FileInfo(dialog.FileName);
                    using (FileStream fs = new FileStream(dialog.FileName, FileMode.Open, FileAccess.Read))
                    {
                        byte[] data = new byte[fileInfo.Length];
                        fs.Read(data, 0, (int)fileInfo.Length);

                        MemoryStream ms = new MemoryStream(Convert.FromBase64String(Convert.ToBase64String(data).ToString()));
                        picbox.Image = Image.FromStream(ms);
                    }
                }
            }
            catch (Exception ex)
            {
                throw Framework.MessageException.Create(ex.ToString());
            }
            finally
            {
                DialogManager.CloseWaitArea(this);
            }

        }
        #endregion
    }
}
