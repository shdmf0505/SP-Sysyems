#region using

using DevExpress.XtraGrid.Views.Grid;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 검사원관리 > 검사원관리 등록팝업
    /// 업  무  설  명  : 검사원을 등록할 수 있고 이력을 볼 수 있는 팝업이다.
    /// 생    성    자  : 유태근
    /// 생    성    일  : 2019-08-08
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class InspectorPopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region Local Variables

        /// <summary>
        /// 팝업진입전 그리드의 Row
        /// </summary>
        public DataRow CurrentDataRow { get; set; }

        /// <summary>
        /// 저장할 변수를 담을 데이터테이블
        /// </summary>
        private DataTable _certSaveDt = new DataTable();

        /// <summary>
        /// Site ComboBox에서 선택된 PlantId
        /// </summary>
        private string _plantId;

        /// <summary>
        /// 신규등록인지 수정인지 판별하는 Flag
        /// </summary>
        public string _inputFlag = "";

        /// <summary>
        /// 작업장 ID, 유저 ID를 저장할 팝업조회조건
        /// </summary>
        private ConditionItemTextBox plantBox = new ConditionItemTextBox();

        #endregion

        #region 생성자

        /// <summary>
        /// 생성자
        /// </summary>
        public InspectorPopup()
        {
            InitializeComponent();

            InitializeEvent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            grdCertificationHistory.View.SetIsReadOnly();
            grdCertificationHistory.View.SetAutoFillColumn("CAPACITYTYPE");          
            grdCertificationHistory.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdCertificationHistory.GridButtonItem = GridButtonItem.Delete;

            grdCertificationHistory.View.AddTextBoxColumn("CREATEDTIME", 150)
                .SetLabel("REGISTRATIONDATE")
                .SetTextAlignment(TextAlignment.Center); // 등록일시

            grdCertificationHistory.View.AddTextBoxColumn("CAPACITYTYPE", 200); // 자격구분

            grdCertificationHistory.View.AddTextBoxColumn("CERTDATE", 150)
                .SetTextAlignment(TextAlignment.Center); // 인증평가일

            grdCertificationHistory.View.AddTextBoxColumn("SCORE", 100)
                .SetTextAlignment(TextAlignment.Center); // 점수

            grdCertificationHistory.View.AddTextBoxColumn("ISCERTFINISH", 150)
                .SetTextAlignment(TextAlignment.Center); // 완료여부

            grdCertificationHistory.View.AddTextBoxColumn("INSPECTORID", 100)
                .SetIsHidden(); // 검사원 ID
            grdCertificationHistory.View.AddTextBoxColumn("TXNHISTKEY", 100)
                .SetIsHidden(); // TxnHistKey

            grdCertificationHistory.View.PopulateColumns();
        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            this.Load += ChemicalissuePopup_Load;

            dtpEnterDate.EditValueChanged += CalcCareer;
            spinYear.EditValueChanged += CalcCareer;
            spinMonth.EditValueChanged += CalcCareer;

            dtpCertDate.EditValueChanged += DtpCertDate_EditValueChanged;

            spinScore.EditValueChanged += SpinScore_EditValueChanged;

            cboPlant.EditValueChanged += CboPlant_EditValueChanged;
            popupInspector.ButtonClick += PopupInspector_ButtonClick;

            btnSave.Click += BtnSave_Click;
            btnClose.Click += BtnClose_Click;
        }

        /// <summary>
        /// 검사자 Popup에서 x버튼 눌렀을때 검사자명과 사원번호 초기화
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PopupInspector_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind.ToString() == "Clear")
            {
                txtInspector.Text = string.Empty;
                txtEmpno.Text = string.Empty;
                txtArea.Text = string.Empty;
                txtArea.Tag = string.Empty;
            }
        }

        /// <summary>
        /// Site가 변경될때마다 검사자 팝업조건에 반영
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboPlant_EditValueChanged(object sender, EventArgs e)
        {
            _plantId = cboPlant.EditValue.ToString();
            plantBox.SetDefault(_plantId);

            if (!_inputFlag.Equals("Update"))
            {
                txtInspector.Text = string.Empty;
                txtEmpno.Text = string.Empty;
                txtArea.Text = string.Empty;
                txtArea.Tag = string.Empty;
            }
        }

        /// <summary>
        /// 입사일자에 따른 자회사경력, 총경력 계산
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalcCareer(object sender, EventArgs e)
        {
            if (dtpEnterDate.EditValue == null)
            {
                this.ShowMessage("NoSelectedEnterDate");
                return;
            }

            string yearLanguage = Language.Get("YEAR");
            string monthLanguage = Language.Get("MONTH");

            DateTime now = DateTime.Now;
            DateTime enterDate = (DateTime)dtpEnterDate.EditValue;

            int diffMonth = 12 * (now.Year - enterDate.Year) + (now.Month - enterDate.Month);
            int diffYear = 0;

            // 자회사 경력 계산
            if (diffMonth >= 12)
            {
                diffYear = diffMonth / 12;
                diffMonth = diffMonth % 12;
                txtCompanyCareer.EditValue = diffYear + yearLanguage + " " + diffMonth + monthLanguage;
            }
            else
            {
                txtCompanyCareer.EditValue = diffMonth + monthLanguage;
            }

            // 총경력 계산
            int diffPrevMonth = Convert.ToInt32(spinMonth.EditValue);
            int diffPrevYear = Convert.ToInt32(spinYear.EditValue);

            int totalCareerMonth = diffMonth + diffPrevMonth;
            int totalCareerYear = diffYear + diffPrevYear;

            if (totalCareerMonth >= 12)
            {
                totalCareerYear += totalCareerMonth / 12;
                totalCareerMonth = totalCareerMonth % 12;

                if (totalCareerMonth == 0)
                {
                    txtTotalCareer.EditValue = totalCareerYear + yearLanguage;
                }
                else
                {
                    txtTotalCareer.EditValue = totalCareerYear + yearLanguage + " " + totalCareerMonth + monthLanguage;
                }              
            }
            else
            {
                if (totalCareerYear == 0)
                {
                    txtTotalCareer.EditValue = totalCareerMonth + monthLanguage;
                }
                else
                {
                    txtTotalCareer.EditValue = totalCareerYear + yearLanguage + " " + totalCareerMonth + monthLanguage;
                }
            }
        }

        /// <summary>
        /// 인증평가일이 바뀔때 차기 인증평가일도 +1년으로 변경
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DtpCertDate_EditValueChanged(object sender, EventArgs e)
        {
            DateTime certDate = (DateTime)dtpCertDate.EditValue;         
            dtpNextCertDate.EditValue = certDate.AddYears(1); 
        }

        /// <summary>
        /// 점수가 입력될때 검사원 등급에 정의된 등급 조회
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpinScore_EditValueChanged(object sender, EventArgs e)
        {
            if (cboCapacityType.EditValue == null)
            {
                // 자격구분이 선택되지 않았습니다.
                this.ShowMessage("NoSelectedCapacity");
                return;
            }

            string inspectionClassId = cboCapacityType.EditValue.ToString();

            int score = Convert.ToInt32(spinScore.EditValue);

            Dictionary<string, object> param = new Dictionary<string, object>
            {
                { "ENTERPRISEID", UserInfo.Current.Enterprise},
                { "PLANTID", cboPlant.EditValue},
                { "INSPECTIONCLASSID", inspectionClassId },
                { "SCORE", score }
            };

            DataTable dt = SqlExecuter.Query("GetInspectionScore", "10001", param);

            if (dt.Rows.Count == 0)
            {
                txtGrade.EditValue = "None";
                return;
            }

            txtGrade.EditValue = dt.Rows[0]["GRADE"];
        }

        /// <summary>
        /// 저장
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            // 검사원명이 입력되지 않았다면 Exception
            if (string.IsNullOrEmpty(txtInspector.Text.ToString()))
            {
                // 검사자가 선택되지 않았습니다.
                throw MessageException.Create("NotSelectInspector");
            }
            else if (string.IsNullOrEmpty(dtpEnterDate.Text.ToString()))
            {
                // 입사일자가 선택되지 않았습니다.
                throw MessageException.Create("입사일자가 선택되지 않았습니다.");
            }
            else
            {
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("INSPECTORNAME", txtInspector.EditValue); // 검사자명
                param.Add("EMPNO", txtEmpno.EditValue); // 사원번호
                param.Add("PLANTID", cboPlant.EditValue); // 사이트

                DataTable dt = SqlExecuter.Query("GetExistInspector", "10001", param);

                // 사용자가 수정으로 팝업을 열었다면 
                if (_inputFlag.Equals("Update"))
                {
                    // 저장하시겠습니까?
                    if (this.ShowMessageBox("InfoPopupSave", "Information", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        SaveCertification(_inputFlag);
                        this.DialogResult = DialogResult.OK;
                    }
                }
                // 사용자가 입력으로 팝업을 열었다면
                else
                {
                    // 사용자가 입력한 사원번호와 검사자명이 이미 저장되어 있다면 Exception
                    if (dt.Rows.Count != 0)
                    {
                        // 이미 존재하는 검사원명, 사원번호입니다.
                        throw MessageException.Create("ExistInspector");
                    }
                    else
                    {
                        // 저장하시겠습니까?
                        if (this.ShowMessageBox("InfoPopupSave", "Information", MessageBoxButtons.OKCancel) == DialogResult.OK)
                        {
                            SaveCertification(_inputFlag);
                            this.DialogResult = DialogResult.OK;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 닫기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// Form Road시 초기화
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChemicalissuePopup_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            InitializeComboBox();
            InitializePopup();
            InitalizeDataTable();
            InitializeGrid();

            SettingNextCertDate();
            SearchCertificationHistory();
            SettingCurrentData();
        }

        #endregion
 
        #region Private Function

        /// <summary>
        /// 메인 그리드에서 검사원 ID를 가져와서 해당 검사원의 인증평가 이력조회
        /// </summary>
        private void SearchCertificationHistory()
        {
            if (CurrentDataRow == null) return;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("INSPECTORID", CurrentDataRow["INSPECTORID"]);
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            grdCertificationHistory.DataSource = SqlExecuter.Query("GetInspectorHistory", "10001", param);
        }

        /// <summary>
        /// 검사원 정보를 저장한다.
        /// </summary>
        private void SaveCertification(string inputFlag)
        {
            DataTable dt = grdCertificationHistory.GetChangedRows(); // 인증평가 이력을 삭제할 테이블
            dt.TableName = "deleteData";

            DataRow row = _certSaveDt.NewRow();

            if (inputFlag.Equals("Update"))
            {
                row["INSPECTORID"] = CurrentDataRow["INSPECTORID"]; // 검사원 ID
            }
            else
            {
                row["INSPECTORID"] = string.Empty; // 검사원 ID
            }
            row["EMPNO"] = txtEmpno.GetValue(); // 사원번호
            row["INSPECTORNAME"] = txtInspector.EditValue; // 검사자명
            row["AREAID"] = txtArea.Tag; // 작업장 ID
            row["INSPECTIONCLASSID"] = cboCapacityType.GetDataValue(); // 검사그룹 ID
            row["CERTDATE"] = dtpCertDate.Text; // 인증평가일
            row["NEXTCERTDATE"] = dtpNextCertDate.Text; // 차기 인증평가일
            row["ENTERDATE"] = dtpEnterDate.Text; // 입사일자
            row["RETIREDATE"] = dtpRetireDate.Text; // 퇴사일자
            row["PREVCAREERYEAR"] = spinYear.EditValue; // 이전경력(년)
            row["PREVCAREERMONTH"] = spinMonth.EditValue; // 이전경력(월)
            row["SCORE"] = spinScore.EditValue; // 점수
            row["GRADE"] = txtGrade.GetValue(); // 등급
            row["ISCERTFINISH"] = cboIsCertFinish.GetDataValue(); // 인증평가 완료여부
            row["ENTERPRISEID"] = UserInfo.Current.Enterprise; // 회사 ID
            row["PLANTID"] = cboPlant.EditValue; // Site ID
            row["EVALUATIONTYPE"] = cboEvaluationType.EditValue; // 평가구분
            row["INSPECTORNUMBER"] = txtInspectorNumber.GetValue(); // 검사원번호

            _certSaveDt.Rows.Add(row);

            // 입사일자가 입력됬다면 날짜 유효성 평가
            if (!string.IsNullOrEmpty(row["ENTERDATE"].ToString()))
            {
                DateTime certDate = Convert.ToDateTime(row["CERTDATE"]); // 인증평가일
                DateTime enterDate = Convert.ToDateTime(row["ENTERDATE"]); // 입사일자

                // 퇴사일자도 입력됬다면 포함해서 날짜 유효성 평가
                if (!string.IsNullOrEmpty(row["RETIREDATE"].ToString()))
                {
                    DateTime retireDate = Convert.ToDateTime(row["RETIREDATE"]); // 퇴사일자

                    if (enterDate > certDate || enterDate > retireDate || retireDate > certDate)
                    {
                        // 날짜를 확인해주세요.
                        throw MessageException.Create("CheckDate");
                    }
                }
                // 입사일자만 입력됬다면 퇴사일자 빼고 날짜 유효성 평가
                else
                {
                    if (enterDate > certDate)
                    {
                        // 날짜를 확인해주세요.
                        throw MessageException.Create("CheckDate");
                    }
                }
            }

            DataSet ds = new DataSet();
            ds.Tables.Add(_certSaveDt.Copy());
            ds.Tables.Add(dt.Copy());

            this.ExecuteRule("SaveInspector", ds);
        }

        /// <summary>
        /// 차기 인증평가일을 인증평가일 + 1년으로 설정
        /// </summary>
        private void SettingNextCertDate()
        {
            dtpCertDate.EditValue = DateTime.Now;
            DateTime certDate = (DateTime)dtpCertDate.EditValue;
            DateTime nextCertDate = certDate.AddYears(1);
            dtpNextCertDate.EditValue = nextCertDate;
            dtpRetireDate.EditValue = null;
        }

        #endregion

        #region InitalizeComboBox

        /// <summary>
        /// ComboBox 컨트롤 초기화
        /// </summary>
        private void InitializeComboBox()
        {
            Dictionary<string, object> param = new Dictionary<string, object>
            {
                { "ENTERPRISEID", UserInfo.Current.Enterprise },
                { "LANGUAGETYPE", UserInfo.Current.LanguageType },
                { "CODECLASSID", "YesNo"}
            };

            cboCapacityType.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboCapacityType.ValueMember = "INSPECTIONCLASSID";
            cboCapacityType.DisplayMember = "INSPECTIONCLASSNAME";
            cboCapacityType.DataSource = SqlExecuter.Query("GetCapacityType", "10001", param);
            cboCapacityType.ShowHeader = false;

            cboIsCertFinish.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboIsCertFinish.ValueMember = "CODEID";
            cboIsCertFinish.DisplayMember = "CODENAME";
            cboIsCertFinish.EditValue = "Y";
            cboIsCertFinish.DataSource = SqlExecuter.Query("GetCodeList", "00001", param);
            cboIsCertFinish.ShowHeader = false;

            cboPlant.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboPlant.ValueMember = "PLANTID";
            cboPlant.DisplayMember = "PLANTNAME";
            cboPlant.EditValue = UserInfo.Current.Plant;
            cboPlant.DataSource = SqlExecuter.Query("GetPlantListByQcm", "10001", param);
            cboPlant.ShowHeader = false;

            Dictionary<string, object> param2 = new Dictionary<string, object>
            {
                { "LANGUAGETYPE", UserInfo.Current.LanguageType },
                { "CODECLASSID", "EvaluationType"}
            };

            cboEvaluationType.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboEvaluationType.ValueMember = "CODEID";
            cboEvaluationType.DisplayMember = "CODENAME";
            cboEvaluationType.DataSource = SqlExecuter.Query("GetCodeList", "00001", param2);
            cboEvaluationType.ShowHeader = false;
        }

        #endregion

        #region InitalizePopup

        /// <summary>
        /// Popup 컨트롤 초기화
        /// </summary>
        private void InitializePopup()
        {
            popupInspector.SelectPopupCondition = UserPopup();
        }

        /// <summary>
        /// 검사자 팝업
        /// </summary>
        /// <returns></returns>
        private ConditionItemSelectPopup UserPopup()
        {
            ConditionItemSelectPopup popup = new ConditionItemSelectPopup();

            popup.SetPopupLayoutForm(900, 600, FormBorderStyle.FixedDialog);
            popup.SetPopupLayout("USERLIST", PopupButtonStyles.Ok_Cancel, true, true);
            popup.Id = "USERID";
            popup.SearchQuery = new SqlQuery("GetUserListByInspector", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}");
            popup.IsMultiGrid = false;
            popup.DisplayFieldName = "USERNAME";
            popup.ValueFieldName = "USERID";
            popup.LanguageKey = "USER";
            popup.IsRequired = true;
            popup.SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
            {
                foreach (DataRow row in selectedRow)
                {
                    if (selectedRow.Count() > 0)
                    {
                        txtInspector.EditValue = Format.GetString(row["USERNAME"]);
                        txtEmpno.EditValue = Format.GetString(row["USERID"]);
                        txtArea.Text = Format.GetString(row["AREANAME"]);
                        txtArea.Tag = Format.GetString(row["AREAID"]);
                    }
                }
            });

            popup.Conditions.AddTextBox("USERIDNAME");
            popup.Conditions.AddTextBox("AREAIDNAME");
            popup.Conditions.AddTextBox("ENTERPRISEID").SetDefault(UserInfo.Current.Enterprise).SetIsHidden();
            plantBox = popup.Conditions.AddTextBox("PLANTID").SetDefault(cboPlant.EditValue).SetIsHidden();

            popup.GridColumns.AddTextBoxColumn("AREAID", 100);
            popup.GridColumns.AddTextBoxColumn("AREANAME", 150);
            popup.GridColumns.AddTextBoxColumn("USERID", 100);
            popup.GridColumns.AddTextBoxColumn("USERNAME", 100);
            popup.GridColumns.AddTextBoxColumn("OWNTYPE", 80)
                .SetTextAlignment(TextAlignment.Center);
            popup.GridColumns.AddTextBoxColumn("EMAILADDRESS", 180);

            return popup;
        }

        /// <summary>
        /// 작업장 팝업
        /// </summary>
        /// <returns></returns>
        //private ConditionItemSelectPopup AreaPopup()
        //{
        //    ConditionItemSelectPopup popup = new ConditionItemSelectPopup();

        //    popup.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
        //    popup.SetPopupLayout("AREA", PopupButtonStyles.Ok_Cancel, true, true);
        //    popup.Id = "AREAID";
        //    popup.SearchQuery = new SqlQuery("GetAreaListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"P_PLANTID={_plantId}");
        //    popup.IsMultiGrid = false;
        //    popup.DisplayFieldName = "AREANAME";
        //    popup.ValueFieldName = "AREAID";
        //    popup.LanguageKey = "AREA";
        //    popup.SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
        //    {
        //        foreach (DataRow row in selectedRow)
        //        {
        //            if (selectedRow.Count() > 0)
        //            {
        //                areaIdBox.SetDefault(row["AREAID"]);

        //                popupInspector.SetValue("");
        //                popupInspector.EditValue = "";
        //                popupInspector.ClearValue();
        //                popupInspector.Text = null;
        //                txtInspector.Text = null;
        //                txtEmpno.Text = null;

        //                // 해당 작업장에 등록된 유저가 존재하는지 판별
        //                Dictionary<string, object> param = new Dictionary<string, object>();
        //                param.Add("AREAID", row["AREAID"]);

        //                DataTable dt = SqlExecuter.Query("GetIsUserByArea", "10001", param);

        //                if (dt.Rows.Count == 0) hasUserByArea.SetDefault("N");
        //                else hasUserByArea.SetDefault("Y");
        //            }
        //            else
        //            {
        //                areaIdBox.SetDefault(null);
        //                hasUserByArea.SetDefault(null);
        //                popupInspector.SetValue("");
        //                popupInspector.EditValue = "";
        //                popupInspector.ClearValue();
        //                popupInspector.Text = null;
        //                txtInspector.Text = null;
        //                txtEmpno.Text = null;
        //            }
        //        }
        //    });

        //    popup.Conditions.AddTextBox("AREAIDNAME");

        //    popup.GridColumns.AddTextBoxColumn("AREAID", 150);
        //    popup.GridColumns.AddTextBoxColumn("AREANAME", 200);

        //    return popup;
        //}

        #endregion

        #region InitalizeDataTable

        /// <summary>
        /// 저장할 데이터의 데이터테이블 컬럼생성
        /// </summary>
        private void InitalizeDataTable()
        {
            _certSaveDt.TableName = "list";

            _certSaveDt.Columns.Add("INSPECTORID", typeof(string)); // 검사자 ID
            _certSaveDt.Columns.Add("INSPECTORNAME", typeof(string)); // 검사자명
            _certSaveDt.Columns.Add("EMPNO", typeof(string)); // 사원번호
            _certSaveDt.Columns.Add("AREAID", typeof(string)); // 작업장 ID
            _certSaveDt.Columns.Add("INSPECTIONCLASSID", typeof(string)); // 검사그룹 ID
            _certSaveDt.Columns.Add("CERTDATE", typeof(string)); // 인증평가일
            _certSaveDt.Columns.Add("NEXTCERTDATE", typeof(string)); // 차기 인증평가일
            _certSaveDt.Columns.Add("ENTERDATE", typeof(string)); // 입사일자
            _certSaveDt.Columns.Add("RETIREDATE", typeof(string)); // 퇴사일자
            _certSaveDt.Columns.Add("PREVCAREERYEAR", typeof(string)); // 이전경력(년)
            _certSaveDt.Columns.Add("PREVCAREERMONTH", typeof(string)); // 이전경력(월)
            _certSaveDt.Columns.Add("SCORE", typeof(string)); // 점수
            _certSaveDt.Columns.Add("GRADE", typeof(string)); // 등급
            _certSaveDt.Columns.Add("ISCERTFINISH", typeof(string)); // 인증평가 완료여부
            _certSaveDt.Columns.Add("ENTERPRISEID", typeof(string)); // 회사 ID
            _certSaveDt.Columns.Add("PLANTID", typeof(string)); // Site ID
            _certSaveDt.Columns.Add("EVALUATIONTYPE", typeof(string)); // 평가구분
            _certSaveDt.Columns.Add("INSPECTORNUMBER", typeof(string)); // 검사원번호
        }

        #endregion

        #region InitalizeCurrentData

        /// <summary>
        /// 부모 그리드에서 Double Click으로 진입시 기존 데이터를 뿌려준다.
        /// </summary>
        private void SettingCurrentData()
        {
            if (CurrentDataRow == null) return;

            cboPlant.Enabled = false;
            txtInspector.Enabled = false;
            popupInspector.Enabled = false;
            txtEmpno.Enabled = false;
            txtInspectorNumber.Enabled = false;
            cboPlant.EditValue = CurrentDataRow["PLANTID"].ToString();
            txtInspector.EditValue = CurrentDataRow["INSPECTORNAME"].ToString();
            txtEmpno.EditValue = CurrentDataRow["EMPNO"];
            txtArea.Text = CurrentDataRow["AREANAME"].ToString();
            txtArea.Tag = CurrentDataRow["AREAID"];
            cboCapacityType.EditValue = CurrentDataRow["INSPECTIONCLASSID"];
            dtpCertDate.EditValue = CurrentDataRow["CERTDATE"];
            dtpNextCertDate.EditValue = CurrentDataRow["NEXTCERTDATE"];
            dtpEnterDate.EditValue = CurrentDataRow["ENTERDATE"];
            dtpRetireDate.EditValue = CurrentDataRow["RETIREDATE"];
            spinYear.EditValue = CurrentDataRow["PREVCAREERYEAR"];
            spinMonth.EditValue = CurrentDataRow["PREVCAREERMONTH"];
            spinScore.EditValue = CurrentDataRow["SCORE"];
            txtGrade.EditValue = string.IsNullOrWhiteSpace(Format.GetString(CurrentDataRow["GRADE"])) ? "None" : CurrentDataRow["GRADE"];
            cboIsCertFinish.EditValue = CurrentDataRow["ISCERTFINISH"];
            cboEvaluationType.EditValue = CurrentDataRow["EVALUATIONTYPE"];
            txtInspectorNumber.EditValue = CurrentDataRow["INSPECTORNUMBER"];
        }

        #endregion
    }
}
