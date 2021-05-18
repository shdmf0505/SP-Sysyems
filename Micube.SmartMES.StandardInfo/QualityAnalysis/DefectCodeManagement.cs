#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons;
using System;
using Micube.SmartMES.Commons.Controls;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프 로 그 램 명  : 기준정보 > 품질기준정보 > 불량코드
    /// 업  무  설  명  : 불량코드(DefectCode) 정보를 관리한다.
    /// 생    성    자  : 강유라
    /// 생    성    일  : 2019-05-20
    /// 수  정  이  력  : 2019-07-09, 강유라, 불량코드 탭 수정(품질공정, 중공정 연계)
    /// 
    /// 
    /// </summary>
    public partial class DefectCodeManagement : SmartConditionManualBaseForm
    {
        #region Local Variables
        private DataRow _defectclassRow = null;//팝업에서 받아온 데이터 row
        private SmartButton _button = null;

        #endregion

        #region 생성자

        public DefectCodeManagement()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeEvent();
            InitializeGridDefectCodeClass();
            InitializeGridQCSegment();
            InitializeGridDefectCode();
            InitializeGridProcessSegmentClass();
        }

        #region DefectCodeClass 탭 초기화
        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGridDefectCodeClass()
        {
            grdDefectClass.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdDefectClass.GridButtonItem = GridButtonItem.Add| GridButtonItem.Copy | GridButtonItem.Export;

            grdDefectClass.View.AddTextBoxColumn("DEFECTCODE", 100)
                .SetValidationKeyColumn();

            grdDefectClass.View.AddLanguageColumn("DEFECTCODENAME", 200);

            grdDefectClass.View.AddComboBoxColumn("DEFECTCODECLASSID", 100, new SqlQuery("GetDefectCodeClassList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "DEFECTCODECLASSNAME", "DEFECTCODECLASSID")
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("DEFECTCODECLASSNAME");

       

            grdDefectClass.View.AddTextBoxColumn("DESCRIPTION", 150);

            grdDefectClass.View.AddTextBoxColumn("ENTERPRISEID", 150)
                .SetIsHidden();

            grdDefectClass.View.AddTextBoxColumn("PLANTID", 100)
                .SetIsHidden();

            grdDefectClass.View.AddComboBoxColumn("CONFIRMSITE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                .SetTextAlignment(TextAlignment.Center);

            grdDefectClass.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Valid")
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Center);

            grdDefectClass.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdDefectClass.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdDefectClass.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdDefectClass.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdDefectClass.View.PopulateColumns();
        }
        #endregion

        #region 불량코드 탭 초기화
        private void InitializeGridQCSegment()
        {
            grdQCSegment.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdQCSegment.GridButtonItem = GridButtonItem.Add| GridButtonItem.Export;
            grdQCSegment.View.SetSortOrder("QCSEGMENTID");

            grdQCSegment.View.AddTextBoxColumn("QCSEGMENTID", 150)
                .SetValidationKeyColumn();

            grdQCSegment.View.AddLanguageColumn("QCSEGMENTNAME", 200);
            grdQCSegment.View.AddComboBoxColumn("RECEIPTFLAG", 80, new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "CODECLASSID=YesNo"))
                 .SetTextAlignment(TextAlignment.Center)
                 .SetLabel("INISSEGMENT");
                
            grdQCSegment.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Valid")
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Center);

            grdQCSegment.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdQCSegment.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdQCSegment.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdQCSegment.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdQCSegment.View.AddTextBoxColumn("ENTERPRISEID", 150)
                .SetIsHidden();

            grdQCSegment.View.AddTextBoxColumn("PLANTID", 150)
                .SetIsHidden();

            grdQCSegment.View.PopulateColumns();
            		
        }

        private void InitializeGridDefectCode()
        {
            grdDefectCode.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdDefectCode.GridButtonItem =  GridButtonItem.Export;

            grdDefectCode.View.AddTextBoxColumn("QCSEGMENTID", 100)
                .SetIsHidden();

            grdDefectCode.View.AddTextBoxColumn("DEFECTCODE", 100)
                .SetIsReadOnly();

            grdDefectCode.View.AddTextBoxColumn("DEFECTCODENAME", 100)
                .SetIsReadOnly();

            grdDefectCode.View.AddComboBoxColumn("DECISIONDEGREE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=DecisionDegree", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Center);

            grdDefectCode.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
            .SetDefault("Valid")
            .SetValidationIsRequired()
            .SetTextAlignment(TextAlignment.Center);

            //grdDefectCode.View.AddTextBoxColumn("DECISIONDEGREE", 100)
            //    .SetIsReadOnly();

            grdDefectCode.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdDefectCode.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdDefectCode.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdDefectCode.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdDefectCode.View.PopulateColumns();
        }

        private void InitializeGridProcessSegmentClass()
        {
            grdProcessSegment.View.ClearColumns();
            grdProcessSegment.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdProcessSegment.GridButtonItem = GridButtonItem.Add | GridButtonItem.Export;

            grdProcessSegment.View.AddTextBoxColumn("QCSEGMENTID", 150)
                .SetIsHidden();

            Initialize_ProcessSegmentClassPopup();

            grdProcessSegment.View.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 200)
                .SetLabel("MIDDLEPROCESSSEGMENTCLASSNAME")
                .SetIsReadOnly();

            grdProcessSegment.View.AddTextBoxColumn("PARENTPROCESSSEGMENTCLASSID", 200)
                .SetLabel("TOPPROCESSSEGMENTCLASSID")
                .SetIsReadOnly();

            grdProcessSegment.View.AddTextBoxColumn("PARENTPROCESSSEGMENTCLASSNAME", 200)
                .SetLabel("TOPPROCESSSEGMENTCLASSNAME")
                .SetIsReadOnly();

            //grdProcessSegment.View.AddTextBoxColumn("QCSEGMENTTYPE", 200)
            //    .SetIsHidden();

            grdProcessSegment.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
               .SetDefault("Valid")
               .SetValidationIsRequired()
               .SetTextAlignment(TextAlignment.Center);

            grdProcessSegment.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdProcessSegment.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdProcessSegment.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdProcessSegment.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdProcessSegment.View.PopulateColumns();

        }

        /// <summary>
        /// 중공정 선택하는 팝업
        /// </summary>
        private void Initialize_ProcessSegmentClassPopup()
        {
            DataRow qcSegmentRow = grdQCSegment.View.GetFocusedDataRow();
            string resourceId = string.Empty;
            if (qcSegmentRow != null)
            {
                resourceId = qcSegmentRow["QCSEGMENTID"].ToString();
            }

            //팝업 컬럼 설정
            var ProcessSegmentClassId = grdProcessSegment.View.AddSelectPopupColumn("PROCESSSEGMENTCLASSID"
                , new SqlQuery("GetQCSegmentProcessSegmentClass"
                    , "10001"
                    , "PROCESSSEGMENTCLASSTYPE=MiddleProcessSegmentClass"
                    , $"LANGUAGETYPE={UserInfo.Current.LanguageType}"
                    , $"RESOURCEID={resourceId}"))
                .SetLabel("MIDDLEPROCESSSEGMENTCLASSID")
                .SetPopupLayout("PROCESSSEGMENT", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupResultCount(1)
                .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow)
                .SetValidationKeyColumn()
                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {
                    // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
                    // dataGridRow : 현재 Focus가 있는 그리드의 DataRow
                    foreach (DataRow row in selectedRows)
                    {
                        dataGridRow["PARENTPROCESSSEGMENTCLASSID"] = row["PARENTPROCESSSEGMENTCLASSID"].ToString();
                        dataGridRow["PARENTPROCESSSEGMENTCLASSNAME"] = row["PARENTPROCESSSEGMENTCLASSNAME"].ToString();
                        dataGridRow["PROCESSSEGMENTCLASSNAME"] = row["PROCESSSEGMENTCLASSNAME"].ToString();
                    }

                });

            ProcessSegmentClassId.Conditions.AddTextBox("PROCESSSEGMENTCLASS");
            ProcessSegmentClassId.Conditions.AddTextBox("MIDDLEPROCESSSEGMENTCLASS");            

            // 팝업 그리드
            ProcessSegmentClassId.GridColumns.AddTextBoxColumn("PARENTPROCESSSEGMENTCLASSID", 150).SetLabel("TOPPROCESSSEGMENTCLASSID");
            ProcessSegmentClassId.GridColumns.AddTextBoxColumn("PARENTPROCESSSEGMENTCLASSNAME", 200).SetLabel("TOPPROCESSSEGMENTCLASSNAME");
            ProcessSegmentClassId.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 150).SetLabel("MIDDLEPROCESSSEGMENTCLASSID");
            ProcessSegmentClassId.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 200).SetLabel("MIDDLEPROCESSSEGMENTCLASSNAME");

        }
        #endregion


        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            //로드 이벤트 : 화면 로드시 툴바의 저장버튼을 찾아 _button에 담는다
            // 탭 체인지시 버튼 visible설정 위해
            this.Load += (s, e) =>
            {
                Control[] controls = pnlToolbar.Controls.Find("Save", true);
                if (controls.Count() > 0 && controls[0] is SmartButton)
                {
                    _button = controls[0] as SmartButton;
                }
            };

            //탭 체인지 이벤트
            tabDefect.SelectedPageChanged += SmartTabControl1_SelectedPageChanged;

           // grdDefectClass row 추가시 이벤트
             grdDefectClass.View.AddingNewRow += View_AddingNewRow;

            //grdQCSegment row 추가시 이벤트
            //grdQCSegment.View.AddingNewRow += View_AddingNewRow;

            //grdQCSegment focused row 체인지 이벤트 : 매핑된 불량코드와 중공정을 조회한다
            grdQCSegment.View.FocusedRowChanged += (s, e) =>
            {
                if (grdQCSegment.View.DataRowCount==0)
                {
                    grdDefectCode.View.ClearDatas();
                    grdProcessSegment.View.ClearDatas();
                    return;
                }
                if (string.IsNullOrWhiteSpace(grdQCSegment.View.GetFocusedDataRow()["QCSEGMENTID"].ToString()))
                {
                    grdDefectCode.View.ClearDatas();
                    grdProcessSegment.View.ClearDatas();
                    return;
                }
                    
                try
                {
                    this.ShowWaitArea();
                    SearchDefectCode();
                    SearchProcessSegmentClass();
                }
                catch (Exception ex)
                {
                    this.ShowError(ex);
                }
                finally
                {
                    this.CloseWaitArea();
                }
            };

            //grdDefectCode row 추가시 이벤트
            grdDefectCode.View.AddingNewRow += View_AddingNewRow_DefectCode;
            //grdProcessSegment row 추가시 이벤트
            grdProcessSegment.View.AddingNewRow += (s, e) => 
            {
                if (grdQCSegment.View.DataRowCount == 0)
                {
                    grdProcessSegment.View.DeleteRow(grdProcessSegment.View.FocusedRowHandle);

                    return;
                }
                DataRow row = grdQCSegment.View.GetFocusedDataRow();
                e.NewRow["QCSEGMENTID"] = row["QCSEGMENTID"];
                //e.NewRow["QCSEGMENTTYPE"] = "MiddleProcessSegmentClass";
            };
            //grdProcessSegment 에 새row 추가 이벤트
            grdProcessSegment.ToolbarAddingRow += (s, e) =>
            {
                DataRow row = grdQCSegment.View.GetFocusedDataRow();

                if (row == null || row.RowState.Equals(DataRowState.Added))
                {
                    e.Cancel = true;
                    ShowMessage("SelectQCSegmentBeforeSelectProcessSegment"); // 중공정을 선택하기 전에 품질공정을 선택해야합니다.
                    return;
                }
            };

            //new SetGridDeleteButonVisible(grdDefectClass);
            //new SetGridDeleteButonVisible(grdQCSegment);
            //new SetGridDeleteButonVisibleOnly(grdDefectCode);
            //new SetGridDeleteButonVisible(grdProcessSegment);

            //btndefectselect버튼 클릭
            btnSelectDefect.Click += BtnSelectDefect_Click;

            //품질공정 저장버튼 클릭 이벤트
            btnSaveQcSegment.Click += (s, e) =>
            {
                SaveQcSegment();
            };
            //불량명 저장버튼 클릭 이벤트
            btnSaveDefectCode.Click += (s, e) =>
            {
                SaveDefectCode();
            };
            //중공정 저장버튼 클릭 이벤트
            btnSaveProcessSegmentClass.Click += (s, e) =>
            {
                SaveProcessSegmentClass();
            };
        }

        //불량명 적용버튼을 클릭했을 때 불량명 조회 팝업을 띄운다
        private void BtnSelectDefect_Click(object sender, EventArgs e)
        {
            DataRow row = grdQCSegment.View.GetFocusedDataRow();

            if (row == null || row.RowState.Equals(DataRowState.Added))
            {
                ShowMessage("SelectQCSegmentBeforeSelectDefectCodeClass"); // 불량명을 선택하기 전에 등록된 품질공정을 선택해야합니다.
                return;
            }

            DialogManager.ShowWaitArea(pnlContent);

            DefectCodeClassSelectPopup popup = new DefectCodeClassSelectPopup();
            popup.CurrentDataRow = row;
            popup.Owner = this;
            popup.ShowDialog();

            DialogManager.CloseWaitArea(pnlContent);

            if (popup.DialogResult == DialogResult.OK)
            {
                DataTable defectclassTable = popup.checkTable;

                foreach (DataRow chekedRow in defectclassTable.Rows)
                {
                    _defectclassRow = chekedRow;
                    grdDefectCode.View.AddNewRow();

                }

            }
        }

        /// <summary>
        /// grdDefectCode에 새 row가 생길 때 품질공정 정보, 불량코드조합 (불량명ID + 품질공정ID)을 해주는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRow_DefectCode(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            if(grdQCSegment.View.DataRowCount ==0)
            {
                grdDefectCode.View.DeleteRow(grdDefectCode.View.FocusedRowHandle);

                return;
            }
            args.NewRow["ENTERPRISEID"] = Framework.UserInfo.Current.Enterprise;
            //args.NewRow["PLANTID"] = Framework.UserInfo.Current.Plant;

            DataRow row = grdQCSegment.View.GetFocusedDataRow();
            args.NewRow["RESOURCEID"] = row["QCSEGMENTID"];
            args.NewRow["QCSEGMENTID"] = row["QCSEGMENTID"];
            args.NewRow["RESOURCEVERSION"] = "*";
            args.NewRow["RESOURCETYPE"] = "QCSegmentID";

            args.NewRow["DEFECTCODECLASSID"] = _defectclassRow["DEFECTCODECLASSID"];
            args.NewRow["DEFECTCODECLASSNAME"] = _defectclassRow["DEFECTCODECLASSNAME"];
            //args.NewRow["DEFECTCODE"] = _defectclassRow["DEFECTCODECLASSID"].ToString() + row["QCSEGMENTID"].ToString();
            args.NewRow["DEFECTCODE"] = _defectclassRow["DEFECTCODE"].ToString();
            args.NewRow["DEFECTCODENAME"] = _defectclassRow["DEFECTCODENAME"].ToString();
            args.NewRow["DEFECTCODENAME$$KO-KR"] = _defectclassRow["DEFECTCODECLASSNAME$$KO-KR"].ToString() + row["QCSEGMENTNAME$$KO-KR"].ToString();
            args.NewRow["DEFECTCODENAME$$EN-US"] = _defectclassRow["DEFECTCODECLASSNAME$$EN-US"].ToString() + row["QCSEGMENTNAME$$EN-US"].ToString();
            args.NewRow["DEFECTCODENAME$$ZH-CN"] = _defectclassRow["DEFECTCODECLASSNAME$$ZH-CN"].ToString() + row["QCSEGMENTNAME$$ZH-CN"].ToString();
            args.NewRow["DEFECTCODENAME$$VI-VN"] = _defectclassRow["DEFECTCODECLASSNAME$$VI-VN"].ToString() + row["QCSEGMENTNAME$$VI-VN"].ToString();
        }

        /// <summary>
        /// grdQCSegment ROW 추가시 PLANTID, ENTERPRISEID 자동 입력
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            args.NewRow["ENTERPRISEID"] = Framework.UserInfo.Current.Enterprise;
            args.NewRow["PLANTID"] = Framework.UserInfo.Current.Plant;
        }

        /// <summary>
        /// 탭이 바뀔 때 툴바의 저장 버튼 Visible을 바꿔 주는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SmartTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {

            if (tabDefect.SelectedTabPageIndex == 0)
            {
                _button.Visible = true;              
            }
            else
            {                
                _button.Visible = false;
                //SearchQcSegment();
            }
        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            DataTable changed = null;

            changed = grdDefectClass.GetChangedRows();


              ExecuteRule("SaveDefectCodeClass", changed);


                


             

           
      
        }

        #endregion

        #region 검색

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();


            if (tabDefect.SelectedTabPageIndex == 0)
            {//불량명 검색
                DataTable dt = null;
                var values = Conditions.GetValues();
                values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                //불량코드
                dt = await SqlExecuter.QueryAsync("SelectDefectCodeNewVersion", "10001", values);
                CheckData(dt);

                grdDefectClass.DataSource = dt;
            }
            else if (tabDefect.SelectedTabPageIndex == 1)
            {//불량코드 탭의 품질공정 검색
                SearchQcSegment();
            }
        }

        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
        }

        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
        }

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            DataTable changed = null;

            grdDefectClass.View.CheckValidation();

            changed = grdDefectClass.GetChangedRows();

            CheckChanged(changed);

        }

        #endregion

        #region Private Function

        // TODO : 화면에서 사용할 내부 함수 추가
        private void CheckChanged(DataTable table)
        {
            if (table.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }

        private void CheckData(DataTable table)
        {
            if (table.Rows.Count < 1)
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }
        }

        /// <summary>
        /// 품질공정을 저장하는 함수
        /// </summary>
        private void SaveQcSegment()
        {
            try
            {
                this.ShowWaitArea();
                btnSaveQcSegment.Enabled = false;

                DataTable changed = grdQCSegment.GetChangedRows();
                CheckChanged(changed);

                string ruleName = "SaveQcsegmentdefinition";
                string tableName = "list";
                changed.TableName = tableName;

                MessageWorker worker = new MessageWorker(ruleName);
                worker.SetBody(new MessageBody()
            {
                { "enterpriseId", UserInfo.Current.Enterprise },
                { "plantId", UserInfo.Current.Plant },
                { tableName, changed }

            });
                worker.Execute();
                ShowMessage("SuccessSave");

                SearchQcSegment();

            }


        
            catch (Exception ex)
            {
                this.ShowError(ex);
            }
            finally
            {
                this.CloseWaitArea();
                btnSaveQcSegment.Enabled = true;
            }
        }

        /// <summary>
        /// 품질공정을 검색하는 함수
        /// </summary>
        private void SearchQcSegment()
        {
            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable dt = SqlExecuter.Query("SelectQCSegment", "10001", values);
            CheckData(dt);

            grdQCSegment.DataSource = dt;
            if (dt.Rows.Count==0)
            {
                grdDefectCode.View.ClearDatas();
                grdProcessSegment.View.ClearDatas();
            }
            else
            {
                grdQCSegment.View.ClearSelection();
                grdQCSegment.View.FocusedRowHandle = 0;
                grdQCSegment.View.SelectRow(0);
            }
        }

        /// <summary>
        /// 불량코드를 저장하는 함수
        /// </summary>
        private void SaveDefectCode()
        {
            try
            {
                this.ShowWaitArea();
                btnSaveDefectCode.Enabled = false;

                DataTable changed = grdDefectCode.GetChangedRows();
                CheckChanged(changed);

                ExecuteRule("SaveDefectCode", changed);

                ShowMessage("SuccessSave");

                SearchDefectCode();
            }
            catch (Exception ex)
            {
                this.ShowError(ex);
            }
            finally
            {
                this.CloseWaitArea();
                btnSaveDefectCode.Enabled = true;
            }
        }

        /// <summary>
        /// 불량코드를 검색하는 함수
        /// </summary>
        private void SearchDefectCode()
        {
            DataRow row = grdQCSegment.View.GetFocusedDataRow();

            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("RESOURCEID", row["QCSEGMENTID"]);

            DataTable dt = SqlExecuter.Query("SelectDefectCodeByQcSegId", "10001", values);
            grdDefectCode.DataSource = dt;
        }

        /// <summary>
        /// 중공정을 저장하는 함수
        /// </summary>
        private void SaveProcessSegmentClass()
        {
            try
            {
                this.ShowWaitArea();
                btnSaveProcessSegmentClass.Enabled = false;

                DataTable changed = grdProcessSegment.GetChangedRows();
                CheckChanged(changed);

                ExecuteRule("SaveQCSegmentClassRel", changed);

                ShowMessage("SuccessSave");

                SearchProcessSegmentClass();
            }
            catch (Exception ex)
            {
                this.ShowError(ex);
            }
            finally
            {
                this.CloseWaitArea();
                btnSaveProcessSegmentClass.Enabled = true;
            }
        }

        /// <summary>
        /// 중공정을 검색하는 함수
        /// </summary>
        private void SearchProcessSegmentClass()
        {
            DataRow row = grdQCSegment.View.GetFocusedDataRow();

            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("RESOURCEID", row["QCSEGMENTID"]);

            DataTable dt = SqlExecuter.Query("SelectQCSegmentClassRel", "10001", values);
            InitializeGridProcessSegmentClass();
            grdProcessSegment.DataSource = dt;
        }
        #endregion

    }
}
