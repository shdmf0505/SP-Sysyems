#region using
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
using System.Diagnostics;
using Micube.Framework;
using SmartDeploy.Common;
using Micube.SmartMES.Commons.Controls;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors;
#endregion

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질 > 레칭레이트 > 에칭레이트 측정값 등록
    /// 업  무  설  명  : 에칭레이트 측정값 등록 화면의 이상발생 그리드를 더블 클릭하여 재 측정값을 관리한다.
    /// 생    성    자  : 강유라
    /// 생    성    일  : 2019-07-17
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class EtchingRateReMeasurePopup : SmartPopupBaseForm, ISmartCustomPopup
    {
   
        #region Interface

        public DataRow CurrentDataRow { get; set; }

        #endregion

        #region Local Variables
        private string _result = "";
        #endregion

        #region 생성자
        public EtchingRateReMeasurePopup()
        {
            InitializeComponent();
            InitializeContent();
            InitializeGrid();
            InitializeEvent();
            SetPopup();
        }
        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 2019-12-24 컨트롤 변경 추가
        /// Controler 초기화
        /// </summary>
        private void InitializeContent()
        {
            btnOriginalFile.ReadOnly = true;
            btnOriginalFile.Properties.Buttons.Add(new EditorButton(ButtonPredefines.Down));
            btnOriginalFile.Properties.Buttons.Add(new EditorButton(ButtonPredefines.Delete));
        }
            private void InitializeGrid()
        {
            grdReSpec.GridButtonItem = GridButtonItem.None;
            grdReSpec.View.SetIsReadOnly();

            var specInfo = this.grdReSpec.View.AddGroupColumn("");
            specInfo.AddTextBoxColumn("MEASUREDATE",150);

            specInfo.AddTextBoxColumn("PLANTID", 150)
                .SetLabel("PLANT");

            specInfo.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 150)
                .SetLabel("TOPPROCESSSEGMENTCLASS");

            specInfo.AddTextBoxColumn("AREANAME", 150)
                .SetLabel("AREA");

            specInfo.AddTextBoxColumn("EQUIPMENTNAME", 150)
                .SetLabel("EQUIPMENT");

            specInfo.AddTextBoxColumn("CHILDEQUIPMENTNAME", 150)
                .SetLabel("CHILDEQUIPMENT");

            specInfo.AddTextBoxColumn("MEASUREDEGREE", 150);

            specInfo.AddTextBoxColumn("WORKTYPE", 150)
                .SetLabel("TYPECONDITION");

            specInfo.AddTextBoxColumn("SPECRANGE", 150);

            specInfo.AddTextBoxColumn("CONTROLRANGE", 150);


            var weightValue = this.grdReSpec.View.AddGroupColumn("WEIGHTVALUE");
            weightValue.AddTextBoxColumn("ETCHRATEBEFOREVALUE", 150);

            weightValue.AddTextBoxColumn("ETCHRATEAFTERVALUE", 150);

            var otherInfo = this.grdReSpec.View.AddGroupColumn("");
            otherInfo.AddTextBoxColumn("ETCHRATEVALUE", 150);

            otherInfo.AddTextBoxColumn("MEASURER", 150);

            grdReSpec.View.PopulateColumns();
        }

        #endregion

        #region Event
        private void InitializeEvent()
        {
            Load += (s, e) =>
            {
                numReAfter.Properties.MinValue = 0;
                numReBefore.Properties.MinValue = 0;

                numReAfter.Properties.MaxValue = decimal.MaxValue;
                numReBefore.Properties.MaxValue = decimal.MaxValue;

                numReAfter.Properties.EditMask = "#,###,###.##";
                numReBefore.Properties.EditMask = "#,###,###.##";
            };

            btnCancel.Click += (s, e) =>
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            };

            //OK버튼 클릭 이벤트
            btnOK.Click += BtnOK_Click;

            //에칭 전, 후 값이 바뀔 때 이벤트
            numReBefore.EditValueChanged += Num_EditValueChanged;
            numReAfter.EditValueChanged += Num_EditValueChanged;
            //파일 추가 버튼 클릭
            //2019-12-24 컨트롤변경 삭제 예정
            //btnFileAdd.Click += BtnFildAdd_Click;
            //파일 삭제 버튼 클릭
            //2019-12-24 컨트롤변경 삭제 예정
            //btnFileDelete.Click += BtnFileDelete_Click;

            //파일 다운로드 이벤트(파일명 클릭)
            //2019-12-24 컨트롤변경 삭제 예정
            //txtFileName.Click += TxtFileName_Click;

            // 검사원본 파일 등록/다운로드/삭제
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
                            btnOriginalFile.Text = new FileInfo(dialog.FileName).FullName;

                            CurrentDataRow["FILEID"] = "FILE-ReEtching" + DateTime.Now.ToString("yyyyMMddHHmmssffff");
                            CurrentDataRow["FILENAME"] = Path.GetFileNameWithoutExtension(dialog.SafeFileName);
                            CurrentDataRow["FILESIZE"] = new FileInfo(dialog.FileName).Length;
                            CurrentDataRow["FILEEXT"] = Path.GetExtension(dialog.FileName).Substring(1);
                            CurrentDataRow["FILEPATH"] = "EtchingRate/ReMeasure";
                            CurrentDataRow["SAFEFILENAME"] = dialog.SafeFileName;
                            CurrentDataRow["LOCALFILEPATH"] = new FileInfo(dialog.FileName).FullName;
                            CurrentDataRow["FILEFULLPATH"] = dialog.FileName; //파일의 전체경로 저장
                            CurrentDataRow["PROCESSINGSTATUS"] = "Wait";
                        };
                        break;
                    case 1:
                        if (btnOriginalFile.Text.Equals(string.Empty))
                        {
                            return;
                        }

                        FolderBrowserDialog sdialog = new FolderBrowserDialog();

                        // DB 조회후 다운로드
                        if (Format.GetString(CurrentDataRow["LOCALFILEPATH"]).Equals(string.Empty))
                        {
                            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                            folderBrowserDialog.SelectedPath = Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments); ;

                            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                            {
                                try
                                {
                                    /*2020-01-28
                                    string serverPath = AppConfiguration.GetString("Application.SmartDeploy.Url") + Format.GetString(CurrentDataRow["FILEPATH"]) + (Format.GetString(CurrentDataRow["FILEPATH"]).EndsWith("/") ? "" : "/");
                                    DeployCommonFunction.DownLoadFile(serverPath, folderBrowserDialog.SelectedPath, Format.GetString(CurrentDataRow["SAFEFILENAME"]));
                                    ShowMessage("SuccedSave");
                                    */

                                    string ftpServerPath = Cryptography.AESDecrypt(AppConfiguration.GetString("Application.Ftp.Url"));
                                    string ftpServerUserId = Cryptography.AESDecrypt(AppConfiguration.GetString("Application.Ftp.Id"));
                                    string ftpServerPassword = Cryptography.AESDecrypt(AppConfiguration.GetString("Application.Ftp.Password"));

                                        try
                                        {
                                            string filePath = Format.GetString(CurrentDataRow["FILEPATH"]);
                                            string safeFileName = Format.GetString(CurrentDataRow["SAFEFILENAME"]);
                                            string downloadPath = folderBrowserDialog.SelectedPath;

                                            using (System.Net.WebClient client = new System.Net.WebClient())
                                            {
                                                string serverPath = ftpServerPath + Format.GetString(CurrentDataRow["FILEPATH"]);
                                                serverPath = serverPath + ((serverPath.EndsWith("/")) ? "" : "/") + Format.GetString(CurrentDataRow["SAFEFILENAME"]);

                                                client.Credentials = new System.Net.NetworkCredential(ftpServerUserId, ftpServerPassword);

                                                client.DownloadFile(serverPath, string.Join("\\", downloadPath, Format.GetString(CurrentDataRow["SAFEFILENAME"])));
                         
                                            }
                                        }
                                        catch (Exception e1)
                                        {   
                                            throw MessageException.Create(e1.Message);
                                        }
                                    
                                    
                                }
                                catch (Exception ex)
                                {
                                    throw MessageException.Create(ex.Message);
                                }

                                ShowMessage("SuccedSave");
                            }
                        }
                        else
                        {
                            //File.Copy(Format.GetString(CurrentDataRow["LOCALFILEPATH"]), string.Join("\\", sdialog.SelectedPath, btnOriginalFile.Text));
                            if (ShowMessage(MessageBoxButtons.YesNo, "DoYouExecuteFile") == DialogResult.Yes)
                            {

                                Process p = new Process();
                                ProcessStartInfo pi = new ProcessStartInfo();
                                pi.UseShellExecute = true;
                                pi.FileName = @btnOriginalFile.EditValue.ToString();
                                p.StartInfo = pi;

                                try
                                {
                                    p.Start();
                                }
                                catch (Exception Ex)
                                {
                                    throw MessageException.Create("CanNotExecuteFile", Ex.Message);
                                }
                            }
                        }

                        
                        break;
                    case 2:

                        btnOriginalFile.Text = string.Empty;

                        CurrentDataRow["FILEID"] = string.Empty;
                        CurrentDataRow["FILENAME"] = string.Empty;
                        CurrentDataRow["FILESIZE"] = DBNull.Value;
                        CurrentDataRow["FILEEXT"] = string.Empty;
                        CurrentDataRow["FILEPATH"] = string.Empty;
                        CurrentDataRow["SAFEFILENAME"] = string.Empty;
                        CurrentDataRow["LOCALFILEPATH"] = string.Empty;
                        CurrentDataRow["FILEFULLPATH"] = string.Empty;
                        CurrentDataRow["PROCESSINGSTATUS"] = string.Empty;
                        break;
                }
            };
        }

        /*
        /// <summary>
        /// //2019-12-24 컨트롤변경 삭제 예정
        /// 파일명 텍스트를 클릭하여 다운로드하는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtFileName_Click(object sender, EventArgs e)
        {
            if (txtFileName.EditValue == null) return;

            if (string.IsNullOrWhiteSpace(CurrentDataRow["FILEID"].ToString()))
            {// 저장 전
                if (ShowMessage(MessageBoxButtons.YesNo, "DoYouExecuteFile") == DialogResult.Yes)
                {
                    
                    Process p = new Process();
                    ProcessStartInfo pi = new ProcessStartInfo();
                    pi.UseShellExecute = true;
                    pi.FileName = @txtFileName.EditValue.ToString();
                    p.StartInfo = pi;

                    try
                    {
                        p.Start();
                    }
                    catch (Exception Ex)
                    {
                        throw MessageException.Create("CanNotExecuteFile",Ex.Message);
                    }
                }
            }
            else
            {// 저장 후

                if (ShowMessage(MessageBoxButtons.YesNo, "DoYouDownloadFile") == DialogResult.Yes)
                {
                    FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                    folderBrowserDialog.SelectedPath = Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);

                    if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            string serverPath = AppConfiguration.GetString("Application.SmartDeploy.Url") + Format.GetString(CurrentDataRow["FILEPATH"]) + (Format.GetString(CurrentDataRow["FILEPATH"]).EndsWith("/") ? "" : "/");
                            DeployCommonFunction.DownLoadFile(serverPath, folderBrowserDialog.SelectedPath, Format.GetString(CurrentDataRow["SAFEFILENAME"]));
                        }
                        catch (Exception ex)
                        {
                            throw MessageException.Create(ex.Message);
                        }
                    }
                }
            }
        }
        */
        //2019-12-24 컨트롤변경 삭제 예정
        /*
        /// <summary>
        /// 파일 삭제 버튼을 클릭 했을때 추가한 파일을 삭제하는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFileDelete_Click(object sender, EventArgs e)
        {
            CurrentDataRow["FILEID"] = null;
            CurrentDataRow["FILENAME"] = null;
            CurrentDataRow["FILESIZE"] = DBNull.Value;
            CurrentDataRow["FILEEXT"] = null;
            CurrentDataRow["FILEPATH"] = null;
            CurrentDataRow["SAFEFILENAME"] = null;
            CurrentDataRow["LOCALFILEPATH"] = null;

            txtFileName.EditValue = null;
        }
        */
        /*
        //2019-12-24 컨트롤변경 삭제 예정
        /// <summary>
        /// 파일 추가 버튼을 클릭 했을 때 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFildAdd_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "All Files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string[] fullFileName = openFileDialog.FileNames; 
                string[] safeFileName = openFileDialog.SafeFileNames;

                FileInfo fileInfo = new FileInfo(fullFileName[0].ToString());

                CurrentDataRow["FILEID"] = "";
                CurrentDataRow["FILENAME"] = fileInfo.Name.Substring(0, fileInfo.Name.Length - fileInfo.Extension.Length);
                CurrentDataRow["FILESIZE"] = fileInfo.Length;
                CurrentDataRow["FILEEXT"] = fileInfo.Extension.Replace(".", "");
                CurrentDataRow["FILEPATH"] = "EtchingRate/ReMeasure";
                CurrentDataRow["SAFEFILENAME"] = fileInfo.Name.Substring(0, fileInfo.Name.Length - fileInfo.Extension.Length) + fileInfo.Extension;
                CurrentDataRow["LOCALFILEPATH"] = fileInfo.FullName;

                txtFileName.EditValue = fileInfo.FullName;
            }

        }*/

        /// <summary>
        /// OK버튼 클릭 이벤트 
        /// _result 값이 ""인지 확인 
        /// 입력값 CurrentRow에 담기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_result))
            {
                ShowMessage("NeedToInputEtchingValue");//측정값을 입력해야합니다.
                return;
            }

            this.DialogResult = DialogResult.OK;

            DataRow row = grdReSpec.View.GetFocusedDataRow();
            CurrentDataRow["REMEASUREDATE"] = txtReMeasureDate.EditValue;
            CurrentDataRow["REMEASURER"] = txtReMeasurer.EditValue;
            CurrentDataRow["REETCHRATEVALUE"] = txtReValue.EditValue;
            CurrentDataRow["REETCHRATEBEFOREVALUE"] = numReBefore.EditValue;
            CurrentDataRow["REETCHRATEAFTERVALUE"] = numReAfter.EditValue;
            CurrentDataRow["RERESULT"] = _result;
            CurrentDataRow["ACTIONRESULT"] = memoActionResult.EditValue;
        }

        /// <summary>
        /// 에칭후 값이 바뀔때 에칭레이트 값과 판정결과를 판정해주는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Num_EditValueChanged(object sender, EventArgs e)
        {
            //에칭전, 에칭 후 값이 0일 때 return
            if (numReBefore.EditValue.ToString().Equals("0") || numReAfter.EditValue.ToString().Equals("0"))
                return;

            Double value = ((numReBefore.EditValue.ToSafeDoubleNaN() - numReAfter.EditValue.ToSafeDoubleNaN()) * 10000 / (10 * 10 * 2 * 8.93).ToSafeDoubleNaN()).ToSafeDoubleNaN();

            //확인후 수정 필요
            if (CurrentDataRow["LSL"].ToSafeDoubleNaN() <= value && value <= CurrentDataRow["USL"].ToSafeDoubleNaN())
            {
                _result = "OK";
            }
            else
            {
                _result = "NG";
            }

            txtReValue.EditValue = value;
            txtReResult.EditValue = _result;
        }
        #endregion

        #region Private Function


        #endregion

        #region Public Function
        /// <summary>
        /// 팝업을 열때 팝업에(상단 그리드) 데이터를 세팅 해주는 함수
        /// </summary>
        public void AddNewRow()
        {
            DataTable dt = grdReSpec.DataSource as DataTable;
            DataRow row = dt.NewRow();

            row["MEASUREDATE"] = CurrentDataRow["MEASUREDATE"];
            row["PLANTID"] = CurrentDataRow["PLANTID"];
            row["PROCESSSEGMENTCLASSNAME"] = CurrentDataRow["PROCESSSEGMENTCLASSNAME"];
            row["EQUIPMENTNAME"] = CurrentDataRow["EQUIPMENTNAME"];
            row["CHILDEQUIPMENTNAME"] = CurrentDataRow["CHILDEQUIPMENTNAME"];
            row["MEASUREDEGREE"] = CurrentDataRow["MEASUREDEGREE"];
            row["WORKTYPE"] = CurrentDataRow["WORKTYPE"];
            row["SPECRANGE"] = CurrentDataRow["SPECRANGE"];
            row["CONTROLRANGE"] = CurrentDataRow["CONTROLRANGE"];
            row["ETCHRATEBEFOREVALUE"] = CurrentDataRow["ETCHRATEBEFOREVALUE"];
            row["ETCHRATEAFTERVALUE"] = CurrentDataRow["ETCHRATEAFTERVALUE"];
            row["ETCHRATEVALUE"] = CurrentDataRow["ETCHRATEVALUE"];
            row["MEASURER"] = CurrentDataRow["MEASURER"];
            row["AREANAME"] = CurrentDataRow["AREANAME"];
            dt.Rows.Add(row);
        }

        /// <summary>
        /// 측정일자를 입력해주는 함수
        /// </summary>
        public void SetReMeasureDate()
        {
            txtReMeasureDate.EditValue = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
        }

        /// <summary>
        /// 컨트롤에 데이터를 바인딩 해주는 함수
        /// 파라미터로 컨트롤 Enable 관리
        /// 저장된 row 수정불가
        /// </summary>
        /// <param name="isEnable"></param>
        public void UpdateReMeasure(Boolean isEnable)
        {
            txtReMeasureDate.EditValue = CurrentDataRow["REMEASUREDATE"];
            txtReMeasureDegree.EditValue = CurrentDataRow["DEGREE"];
            txtReMeasurer.EditValue = CurrentDataRow["REMEASURER"];
            txtReResult.EditValue = CurrentDataRow["RERESULT"];
            numReBefore.EditValue = CurrentDataRow["REETCHRATEBEFOREVALUE"];
            numReAfter.EditValue = CurrentDataRow["REETCHRATEAFTERVALUE"];
            txtReValue.EditValue = CurrentDataRow["REETCHRATEVALUE"];
            memoActionResult.EditValue = CurrentDataRow["ACTIONRESULT"];

            //2019-12-24 컨트롤변경

            if (isEnable == false)
            {
                txtReMeasurer.Enabled = false;
                numReBefore.Enabled = false;
                numReAfter.Enabled = false;
                memoActionResult.Enabled = false;

                //2019-12-24 컨트롤변경 
                btnOriginalFile.Properties.Buttons[0].Enabled = false;
                btnOriginalFile.Properties.Buttons[2].Enabled = false;
                btnOriginalFile.EditValue = CurrentDataRow["SAFEFILENAME"].ToString();
            }
        }

        private void SetPopup()
        {
            //2019-12-24 컨트롤변경 삭제 예정
            //txtFileName.ReadOnly = true;
            //txtFileName.Properties.Appearance.FontStyleDelta = FontStyle.Underline;
            //txtFileName.ForeColor = Color.Blue;
        }

        #endregion
    }
}
