#region using

using DevExpress.XtraEditors.Repository;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;

using System;
using Micube.SmartMES.Commons.Controls;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Micube.SmartMES.Commons;

#endregion

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프 로 그 램 명  : 기준정보 > 품질 기준 정보 > 검사종류관리
    /// 업  무  설  명  : 품질 기준 정보의 검사 종류, 검사 정의, 검사 통제를 관리한다.
    /// 생    성    자  : 강유라
    /// 생    성    일  : 2019-06-12
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class InspectionManagement : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가
        object _inspectionClassId, _nameKO, _nameEN, _nameZH, _nameVI;//검사종류 + 팝업결과로 다국어를 조합하기 위한 변수 
                                                                      // 팝업 결과가 바뀔 때 처리하기 위함

        #endregion

        #region 생성자

        public InspectionManagement()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeEvent();
            // TODO : 컨트롤 초기화 로직 구성
        }

        /// <summary>        
        /// 그리드 초기화
        /// </summary>

      

        #region grdInspectionDef초기화
        private void InitializeGridInspectionDefinition()
        {
            // TODO : 그리드 초기화 로직 추가
            // grdInspectionDef.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            // grdInspectionDef.GridButtonItem -= GridButtonItem.Delete;
            grdInspectionDef.GridButtonItem = GridButtonItem.Export;
            grdInspectionDef.View.AddTextBoxColumn("INSPECTIONCLASSID", 150)
                .SetValidationKeyColumn();
            grdInspectionDef.View.AddLanguageColumn("INSPECTIONCLASSNAME", 200);

            grdInspectionDef.View.AddComboBoxColumn("INSPECTIONCLASSTYPE", 150, new SqlQuery("GetCodeList", "00001", "CODECLASSID=InspectionClassType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsReadOnly()
                .SetIsRefreshByOpen(true);
            //.SetIsReadOnly();
            grdInspectionDef.View.AddComboBoxColumn("INSPECTIONRESOURCETYPE", 150, new SqlQuery("GetCodeList", "00001", "CODECLASSID=InspectionResourceType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsReadOnly()
                .SetIsRefreshByOpen(true);
           grdInspectionDef.View.AddTextBoxColumn("ISUSEITEM", 150)

                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(); 

            grdInspectionDef.View.AddTextBoxColumn("DESCRIPTION", 250);
            
            grdInspectionDef.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
               .SetDefault("Valid")
               .SetValidationIsRequired()
               .SetTextAlignment(TextAlignment.Center);


            grdInspectionDef.View.AddTextBoxColumn("CREATORNAME", 100)
                .SetIsReadOnly();

            grdInspectionDef.View.AddTextBoxColumn("CREATEDTIME", 150)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetDefault("")
                .SetIsReadOnly();
            grdInspectionDef.View.AddTextBoxColumn("MODIFIERNAME", 100)
                .SetIsReadOnly();
            grdInspectionDef.View.AddTextBoxColumn("MODIFIEDTIME", 150)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetDefault("")
                .SetIsReadOnly();
            grdInspectionDef.View.SetKeyColumn("INSPECTIONCLASSID");
            grdInspectionDef.View.PopulateColumns();
        }

        #endregion

        #region grdtopProcessSegment초기화
        private void InitializeGridProcessSegment()
        {
            // TODO : 그리드 초기화 로직 추가
            grdtopProcessSegment.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;            
            grdtopProcessSegment.GridButtonItem = GridButtonItem.Export;
            grdtopProcessSegment.View.SetIsReadOnly();
            grdtopProcessSegment.View.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 110);

            grdtopProcessSegment.View.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 150);

            grdtopProcessSegment.View.PopulateColumns();
        }
        #endregion

        #region grdtopInspectionControl초기화
        private void InitializeGridInspectionControl()
        {

            // TODO : 그리드 초기화 로직 추가
            grdtopInspectionControl.GridButtonItem = GridButtonItem.Export;
          

            grdtopInspectionControl.View.AddTextBoxColumn("INSPECTIONCLASSID", 150)
               .SetIsHidden();

            grdtopInspectionControl.View.AddTextBoxColumn("INSPECTIONCLASSNAME", 200)
               .SetIsReadOnly(); 

            grdtopInspectionControl.View.AddComboBoxColumn("ISREQUIRED", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
               //.SetValidationIsRequired()
               .SetEmptyItem("", "", true)
               .SetTextAlignment(TextAlignment.Center);

            grdtopInspectionControl.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
               .SetValidationIsRequired()
               .SetTextAlignment(TextAlignment.Center);

            grdtopInspectionControl.View.AddTextBoxColumn("CREATORNAME", 100)
                .SetIsReadOnly();

            grdtopInspectionControl.View.AddTextBoxColumn("CREATEDTIME", 150)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetDefault("")
                .SetIsReadOnly();

            grdtopInspectionControl.View.AddTextBoxColumn("MODIFIERNAME", 100)
                .SetIsReadOnly();

            grdtopInspectionControl.View.AddTextBoxColumn("MODIFIEDTIME", 150)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetDefault("")
                .SetIsReadOnly();


            grdtopInspectionControl.View.PopulateColumns();
        }
        #endregion

        #region grdmidProcessSegment초기화
        private void InitializeGridmidProcessSegment()
        {
            // TODO : 그리드 초기화 로직 추가
            grdmidProcessSegment.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdmidProcessSegment.GridButtonItem = GridButtonItem.Export;
            grdmidProcessSegment.View.SetIsReadOnly();

            grdmidProcessSegment.View.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 110);

            grdmidProcessSegment.View.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 150);

            grdmidProcessSegment.View.PopulateColumns();
        }
        #endregion

        #region grdmidInspectionControl초기화
        private void InitializeGridmidInspectionControl()
        {

            // TODO : 그리드 초기화 로직 추가
            grdmidInspectionControl.GridButtonItem = GridButtonItem.Export;
           

            grdmidInspectionControl.View.AddTextBoxColumn("INSPECTIONCLASSID", 150)
               .SetIsHidden();

            grdmidInspectionControl.View.AddTextBoxColumn("INSPECTIONCLASSNAME", 200)
                .SetIsReadOnly(); 

            grdmidInspectionControl.View.AddComboBoxColumn("ISREQUIRED", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
               //.SetValidationIsRequired()
               .SetEmptyItem("", "", true)
               .SetTextAlignment(TextAlignment.Center);

            grdmidInspectionControl.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
               .SetValidationIsRequired()
               .SetTextAlignment(TextAlignment.Center);

            grdmidInspectionControl.View.AddTextBoxColumn("CREATORNAME", 100)
                .SetIsReadOnly();

            grdmidInspectionControl.View.AddTextBoxColumn("CREATEDTIME", 150)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetDefault("")
                .SetIsReadOnly();

            grdmidInspectionControl.View.AddTextBoxColumn("MODIFIERNAME", 100)
                .SetIsReadOnly();

            grdmidInspectionControl.View.AddTextBoxColumn("MODIFIEDTIME", 150)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetDefault("")
                .SetIsReadOnly();    


            grdmidInspectionControl.View.PopulateColumns();
        }
        #endregion

        #region grdstdProcessSegment초기화
        private void InitializeGridstdProcessSegment()
        {
            // TODO : 그리드 초기화 로직 추가
            grdstdProcessSegment.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdstdProcessSegment.GridButtonItem = GridButtonItem.Export;
            grdstdProcessSegment.View.SetIsReadOnly();

            grdstdProcessSegment.View.AddTextBoxColumn("PROCESSSEGMENTID", 110)
              .SetLabel("PROCESSSEGMENTCLASSID");

            grdstdProcessSegment.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150)
                .SetLabel("PROCESSSEGMENTCLASSNAME");

            grdstdProcessSegment.View.PopulateColumns();
        }
        #endregion

        #region grdstdInspectionControl초기화
        private void InitializeGridstdInspectionControl()
        {

            // TODO : 그리드 초기화 로직 추가
            grdstdInspectionControl.GridButtonItem = GridButtonItem.Export;
           

            grdstdInspectionControl.View.AddTextBoxColumn("INSPECTIONCLASSID", 150)
                .SetIsHidden();

            grdstdInspectionControl.View.AddTextBoxColumn("INSPECTIONCLASSNAME", 200)
                .SetIsReadOnly();

            grdstdInspectionControl.View.AddComboBoxColumn("ISREQUIRED", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
               //.SetValidationIsRequired()
               .SetEmptyItem("", "", true)
               .SetTextAlignment(TextAlignment.Center);

            grdstdInspectionControl.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
               .SetValidationIsRequired()
               .SetTextAlignment(TextAlignment.Center);

            grdstdInspectionControl.View.AddTextBoxColumn("CREATORNAME", 100)
                .SetIsReadOnly();

            grdstdInspectionControl.View.AddTextBoxColumn("CREATEDTIME", 150)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetDefault("")
                .SetIsReadOnly();

            grdstdInspectionControl.View.AddTextBoxColumn("MODIFIERNAME", 100)
                .SetIsReadOnly();

            grdstdInspectionControl.View.AddTextBoxColumn("MODIFIEDTIME", 150)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetDefault("")
                .SetIsReadOnly();

            grdstdInspectionControl.View.PopulateColumns();
        }
        #endregion


        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            //화면로드 이벤트
            this.Load += InspectionManagement_Load;
            //탭체인지 이벤트
            tabInspection.SelectedPageChanged += TabInspection_SelectedPageChanged;
          
            //새로운 row추가 이벤트
            grdInspectionDef.View.AddingNewRow += View_AddingNewRow;
            //새로운 row추가 이벤트
            //grdInspectionDef.ToolbarAddingRow += GrdInspectionDef_ToolbarAddingRow;
            //grdTopProcessSegment의 포커스된 row가 바뀔때 이벤트
            grdtopProcessSegment.View.FocusedRowChanged += View_FocusedRowChanged;
            grdmidProcessSegment.View.FocusedRowChanged += View_MIDFocusedRowChanged;
            grdstdProcessSegment.View.FocusedRowChanged += View_StdFocusedRowChanged;
            //grdTopInspectionControl의 검사 수행컬럼 값이 바뀔때 이벤트
            grdtopInspectionControl.View.CellValueChanged += View_CellValueChanged;
           
           // new SetGridDeleteButonVisible(grdInspectionDef);
            btnSave.Click += BtnSave_Click;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (CheckSave() == false) return;
            DialogResult result = System.Windows.Forms.DialogResult.No;

            result = this.ShowMessage(MessageBoxButtons.YesNo, "OspDoProcessWant", btnSave.Text);//저장하시겠습니까? 

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    this.ShowWaitArea();
                    btnSave.Enabled = false;
                    DataTable changed = null;
                    if (tabInspection.SelectedTabPage.Name.Equals("tpgInspectionDef"))
                    {
                        changed = grdInspectionDef.GetChangedRows();
                        ExecuteRule("SaveInspectionDef", changed);
                        ShowMessage("SuccessOspProcess");
                        //다시 조회 하기...
                        OnSaveConfrimSearch();
                    }
                    else
                    {
                        DataRow topfocusedRow = this.grdtopProcessSegment.View.GetFocusedDataRow();
                        DataTable topchanged = grdtopInspectionControl.GetChangedRows();
                        DataRow midfocusedRow = this.grdmidProcessSegment.View.GetFocusedDataRow();
                        DataTable midchanged = grdmidInspectionControl.GetChangedRows();
                        DataRow stdfocusedRow = this.grdstdProcessSegment.View.GetFocusedDataRow();
                        DataTable stdchanged = grdstdInspectionControl.GetChangedRows();

                        if (topchanged != null)
                        {
                            topchanged.Columns.Add("PROCESSSEGMENTID");
                            topchanged.Columns.Add("PROCESSSEGMENTTYPE");
                            topchanged.Columns.Add("PROCESSSEGMENTVERSION");

                            foreach (DataRow dr in topchanged.Rows)
                            {
                                dr["PROCESSSEGMENTID"] = topfocusedRow["PROCESSSEGMENTCLASSID"];
                                dr["PROCESSSEGMENTTYPE"] = "TopProcessSegmentClass";
                                dr["PROCESSSEGMENTVERSION"] = "*";
                                if (dr["VALIDSTATE"].ToString().Equals(""))
                                {
                                    dr["VALIDSTATE"] = "Valid";
                                }
                            }
                            ExecuteRule("SaveInspectionSegment", topchanged);

                        }

                        if (midchanged != null)
                        {
                            midchanged.Columns.Add("PROCESSSEGMENTID");
                            midchanged.Columns.Add("PROCESSSEGMENTTYPE");
                            midchanged.Columns.Add("PROCESSSEGMENTVERSION");

                            foreach (DataRow dr in midchanged.Rows)
                            {
                                dr["PROCESSSEGMENTID"] = midfocusedRow["PROCESSSEGMENTCLASSID"];
                                dr["PROCESSSEGMENTTYPE"] = "MiddleProcessSegmentClass";
                                dr["PROCESSSEGMENTVERSION"] = "*";
                                if (dr["VALIDSTATE"].ToString().Equals(""))
                                {
                                    dr["VALIDSTATE"] = "Valid";
                                }

                            }

                            ExecuteRule("SaveInspectionSegment", midchanged);

                        }

                        if (stdchanged != null)
                        {
                            stdchanged.Columns.Add("PROCESSSEGMENTID");
                            stdchanged.Columns.Add("PROCESSSEGMENTTYPE");
                            stdchanged.Columns.Add("PROCESSSEGMENTVERSION");
                            stdchanged.Columns.Add("STDCHECK");
                            foreach (DataRow dr in stdchanged.Rows)
                            {
                                dr["PROCESSSEGMENTID"] = stdfocusedRow["PROCESSSEGMENTID"];
                                dr["PROCESSSEGMENTTYPE"] = "ProcessSegmentID";
                                dr["PROCESSSEGMENTVERSION"] = "*";
                                if (dr["VALIDSTATE"].ToString().Equals(""))
                                {
                                    dr["VALIDSTATE"] = "Valid";
                                }
                            }


                            ExecuteRule("SaveInspectionSegment", stdchanged);
                        }
                        ShowMessage("SuccessOspProcess");

                        SearchInspectionSegment();
                    }
                }
                catch (Exception ex)
                {
                    this.ShowError(ex);
                }
                finally
                {
                    this.CloseWaitArea();
                   
                    btnSave.Enabled = true;

                }
            }

        }

        /// <summary>
        /// 단가 기준  key 중복 체크 
        /// </summary>
        /// <returns></returns>
        private bool CheckSave()
        {
            bool blcheck = true;
            // TODO : 유효성 로직 변경
            DataTable changed = null;
            DataTable topchanged = null;
            DataTable midchanged = null;
            DataTable stdchanged = null;
            if (tabInspection.SelectedTabPage.Name.Equals("tpgInspectionDef"))
            {
                grdInspectionDef.View.CheckValidation();
                changed = grdInspectionDef.GetChangedRows();
                if (changed != null && changed.Rows.Count == 0)
                {
                    // 저장할 데이터가 존재하지 않습니다.
                    blcheck = false;
                    this.ShowMessage(MessageBoxButtons.OK, "NoSaveData"); //메세지 
                }

            }
            else
            {
                topchanged = grdtopInspectionControl.GetChangedRows();
                midchanged = grdmidInspectionControl.GetChangedRows();
                stdchanged = grdstdInspectionControl.GetChangedRows();
                if (topchanged.Rows.Count == 0 && midchanged.Rows.Count == 0 && stdchanged.Rows.Count == 0)
                {
                    // 저장할 데이터가 존재하지 않습니다.
                    blcheck = false;
                    this.ShowMessage(MessageBoxButtons.OK, "NoSaveData"); //메세지 

                }
            }

            return blcheck;
        }
        /// <summary>
        /// ISREQUIRED 컬럼의 값이 등록 될 때 grdProcessSegment의 PROCESSSEGMENTID를 grdInspectionControl의 PROCESSSEGMENTID 컬럼에 값을 넣어주는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //ISREQUIRED 컬럼의 값이 바뀌었을때
            if (e.Column.FieldName == "ISREQUIRED")
            {
                DataRow row = grdtopInspectionControl.View.GetFocusedDataRow();
                grdtopProcessSegment.View.SetFocusedRowCellValue("ENTERPRISEID", Framework.UserInfo.Current.Enterprise);
                grdtopProcessSegment.View.SetFocusedRowCellValue("PLANTID", "*");

            }
        }

        /// <summary>
        /// grdProcessSegment의 포커스된 row가 바뀔때 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if(e.PrevFocusedRowHandle > -1)
                SearchInspectionSegment();
            LoadmidProcessSegmentData();
        }

        /// <summary>
        /// grdmidProcessSegment의 포커스된 row가 바뀔때 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_MIDFocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.PrevFocusedRowHandle > -1)
                SearchInspectionSegment();
            LoadstdProcessSegmentData();
        }

        /// <summary>
        /// grdmidProcessSegment의 포커스된 row가 바뀔때 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_StdFocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.PrevFocusedRowHandle > -1)
                SearchInspectionSegment();
        }



        /// <summary>
        /// 검사종류, 검사정의를 등록 할 때 자동으로 ENTERPRISEID, PLANTID를 입력해 주는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            args.NewRow["ENTERPRISEID"] = Framework.UserInfo.Current.Enterprise;
            args.NewRow["PLANTID"] = "*";
       
        }

        /// <summary>
        /// 탭체인지 이벤트
        /// 선택된 탭이 바뀔때 마다 다른 조회조건 바인딩.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabInspection_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
        

        }

        //화면 로드 이벤트
        private void InspectionManagement_Load(object sender, EventArgs e)
        {
           
            InitializeGridInspectionDefinition();
            InitializeGridProcessSegment();
            InitializeGridInspectionControl();
            InitializeGridmidProcessSegment();
            InitializeGridmidInspectionControl();
            InitializeGridstdProcessSegment();
            InitializeGridstdInspectionControl();
            LoadProcessSegmentData();
        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장버튼 클릭
        /// 선택된 탭에 따라 다른 룰 실행
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            //base.OnToolbarSaveClick();

            //DataTable changed = null;
            //if (tabInspection.SelectedTabPage.Name.Equals("tpgInspectionDef"))
            //{
            //    changed = grdInspectionDef.GetChangedRows();
            //    ExecuteRule("SaveInspectionDef", changed);

            //}
            //else
            //{
            //    DataRow topfocusedRow = this.grdtopProcessSegment.View.GetFocusedDataRow();
            //    DataTable topchanged = grdtopInspectionControl.GetChangedRows();
            //    DataRow midfocusedRow = this.grdmidProcessSegment.View.GetFocusedDataRow();
            //    DataTable midchanged = grdmidInspectionControl.GetChangedRows();
            //    DataRow stdfocusedRow = this.grdstdProcessSegment.View.GetFocusedDataRow();
            //    DataTable stdchanged = grdstdInspectionControl.GetChangedRows();

            //    if (topchanged != null)
            //    {
            //        topchanged.Columns.Add("PROCESSSEGMENTID");
            //        topchanged.Columns.Add("PROCESSSEGMENTTYPE");
            //        topchanged.Columns.Add("PROCESSSEGMENTVERSION");

            //        foreach (DataRow dr in topchanged.Rows)
            //        {
            //            dr["PROCESSSEGMENTID"] = topfocusedRow["PROCESSSEGMENTCLASSID"];
            //            dr["PROCESSSEGMENTTYPE"] = "TopProcessSegmentClass";
            //            dr["PROCESSSEGMENTVERSION"] = "*";
            //            if (dr["VALIDSTATE"].ToString().Equals(""))
            //            {
            //                dr["VALIDSTATE"] = "Valid";
            //            }
            //        }
            //        ExecuteRule("SaveInspectionSegment", topchanged);
                   
            //    }

            //    if (midchanged != null)
            //    {
            //        midchanged.Columns.Add("PROCESSSEGMENTID");
            //        midchanged.Columns.Add("PROCESSSEGMENTTYPE");
            //        midchanged.Columns.Add("PROCESSSEGMENTVERSION");

            //        foreach (DataRow dr in midchanged.Rows)
            //        {
            //            dr["PROCESSSEGMENTID"] = midfocusedRow["PROCESSSEGMENTCLASSID"];
            //            dr["PROCESSSEGMENTTYPE"] = "MiddleProcessSegmentClass";
            //            dr["PROCESSSEGMENTVERSION"] = "*";
            //            if (dr["VALIDSTATE"].ToString().Equals(""))
            //            {
            //                dr["VALIDSTATE"] = "Valid";
            //            }

            //        }

            //        ExecuteRule("SaveInspectionSegment", midchanged);
                    
            //    }

            //    if (stdchanged != null)
            //    {
            //        stdchanged.Columns.Add("PROCESSSEGMENTID");
            //        stdchanged.Columns.Add("PROCESSSEGMENTTYPE");
            //        stdchanged.Columns.Add("PROCESSSEGMENTVERSION");
            //        stdchanged.Columns.Add("STDCHECK");
            //        foreach (DataRow dr in stdchanged.Rows)
            //        {
            //            dr["PROCESSSEGMENTID"] = stdfocusedRow["PROCESSSEGMENTID"];
            //            dr["PROCESSSEGMENTTYPE"] = "ProcessSegmentID";
            //            dr["PROCESSSEGMENTVERSION"] = "*";
            //            if (dr["VALIDSTATE"].ToString().Equals(""))
            //            {
            //                dr["VALIDSTATE"] = "Valid";
            //            }
            //        }


            //        ExecuteRule("SaveInspectionSegment", stdchanged);
            //    }
            //    if (topchanged.Rows.Count == 0 && midchanged.Rows.Count == 0 && stdchanged.Rows.Count == 0)
            //    {
            //        SearchInspectionSegment();
            //    }
            //}

        }
        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            //base.OnToolbarClick(sender, e);
            SmartButton btn = sender as SmartButton;

            if (btn.Name.ToString().Equals("Save"))
            {
                BtnSave_Click(null, null);
            }
        }
        #endregion

        #region 검색

        /// <summary>
        /// 비동기 override 모델
        /// 선택된 탭에 따라 다른 쿼리 실행
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();


       
            // TODO : 조회 SP 변경
            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);
            DataTable dt = null;
            if (tabInspection.SelectedTabPage.Name.Equals("tpgInspectionDef"))
            {
                grdInspectionDef.DataSource = null;

                dt = await SqlExecuter.QueryAsync("GetSelectInspectionDefList", "10001", values);
                CheckHasData(dt);
                grdInspectionDef.DataSource = dt;
               
            }
            else
            {
                grdtopInspectionControl.DataSource = null;
                grdtopProcessSegment.DataSource = null;


                LoadProcessSegmentData();

            }
           
        }

        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
        }

        /// <summary>
        /// 최초에 첫번째 탭에 조회조건 바인딩
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
            /*
            Conditions.GetControl<SmartComboBox>("p_inspectionclassid").EditValue = "*";
            Conditions.GetControl<SmartComboBox>("p_conditionitem").Enabled = false;
            Conditions.GetControl<SmartTextBox>("p_conditionvalue").Enabled = false;
            */    
        }

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// 선택된 탭에 따라 다른 그리드 유효성
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            // TODO : 유효성 로직 변경
            DataTable changed = null;
            DataTable topchanged = null;
            DataTable midchanged = null;
            DataTable stdchanged = null;
            if (tabInspection.SelectedTabPage.Name.Equals("tpgInspectionDef"))
            {
                grdInspectionDef.View.CheckValidation();
                changed = grdInspectionDef.GetChangedRows();
                if (changed != null && changed.Rows.Count == 0)
                {
                    // 저장할 데이터가 존재하지 않습니다.
                    throw MessageException.Create("NoSaveData");
                }

            }
            else
            {
                topchanged = grdtopInspectionControl.GetChangedRows();
                midchanged = grdmidInspectionControl.GetChangedRows();
                stdchanged = grdstdInspectionControl.GetChangedRows();
                if (topchanged.Rows.Count == 0 && midchanged.Rows.Count == 0 && stdchanged.Rows.Count == 0)
                {
                    // 저장할 데이터가 존재하지 않습니다.
                    throw MessageException.Create("NoSaveData");
                }
            }
         
        }

        private void smartPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tlpInspectionControl_Paint(object sender, PaintEventArgs e)
        {
        }

        private void smartBandedGrid2_Load(object sender, EventArgs e)
        {

        }

        #endregion

        #region Private Function

      

        /// <summary>
        /// 바인딩 할 데이터 테이블에 데이터있는지 확인하는 함수
        /// </summary>
        /// <param name="table"></param>
        private void CheckHasData(DataTable table)
        {
            if (table.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }
        }

        /// <summary>
        /// 화면 로드시 표준공정을 바인딩 시켜주는 함수
        /// </summary>
        private void LoadProcessSegmentData()
        {
            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            DataTable dt = SqlExecuter.Query("GetProcessSegMentTop", "10001", values);

            CheckHasData(dt);

            grdtopProcessSegment.DataSource = dt;
            if (dt.Rows.Count > 0)
            {
                grdtopProcessSegment.View.FocusedRowHandle = 0;
                grdtopProcessSegment.View.SelectRow(0);
                
                SearcTopInspectionSegment();
                LoadmidProcessSegmentData();

            }
            //if (dt.Rows.Count < 1)
            //{

            //}
            //else
            //{
            //    SearchInspectionSegment();
            //}
           
        }

        /// <summary>
        /// top 공정 클릭시 mid공정을 보여주는 함수
        /// </summary>
        private void LoadmidProcessSegmentData()
        {
            var values = Conditions.GetValues();
            DataRow toprow = grdtopProcessSegment.View.GetFocusedDataRow();
            if (toprow != null)
            {
                values.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);
                values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                values.Add("PARENTPROCESSSEGMENTCLASSID", toprow["PROCESSSEGMENTCLASSID"]);
                DataTable dt = SqlExecuter.Query("GetProcessSegMentMiddle", "10001", values);
                CheckHasData(dt);
                grdmidProcessSegment.DataSource = dt;
                if (dt.Rows.Count > 0)
                {
                    grdmidProcessSegment.View.FocusedRowHandle = 0;
                    grdmidProcessSegment.View.SelectRow(0);
                    SearcMidInspectionSegment();
                    LoadstdProcessSegmentData();
                }
          
            }
            else
            {
                grdmidProcessSegment.DataSource = null;
                grdstdInspectionControl.DataSource = null;
                grdstdProcessSegment.DataSource = null;
            }
        }

        /// <summary>
        /// mid 공정 클릭시 표준공정을 보여주는 함수
        /// </summary>
        private void LoadstdProcessSegmentData()
        {
            var values = Conditions.GetValues();
            DataRow midrow = grdmidProcessSegment.View.GetFocusedDataRow();
            if (midrow != null)
            {

                values.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);
                values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                values.Add("MIDDLEPROCESSSEGMENTCLASSID", midrow["PROCESSSEGMENTCLASSID"]);
                DataTable dt = SqlExecuter.Query("GetStdProcessSegmentInfo", "10001", values);
                CheckHasData(dt);
                grdstdProcessSegment.DataSource = dt;
                if (dt.Rows.Count > 0)
                {
                    grdstdProcessSegment.View.FocusedRowHandle = 0;
                    grdstdProcessSegment.View.SelectRow(0);
                    SearcStdInspectionSegment();
                   
                }
              
            }
            else
            {
                grdstdInspectionControl.DataSource = null;
            }
            
        }
        private void SearcTopInspectionSegment()
        {
            if (grdtopProcessSegment.View.FocusedRowHandle > -1)
            {
                DataRow row = grdtopProcessSegment.View.GetFocusedDataRow();

                Dictionary<string, object> values = new Dictionary<string, object>();
                values.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);
                values.Add("P_PROCESSSEGMENTID", row["PROCESSSEGMENTCLASSID"].ToString());
                DataTable dt = SqlExecuter.Query("GetSelectInspectionSegmentRel", "10001", values);

                grdtopInspectionControl.DataSource = dt;
            }

        }
        private void SearcMidInspectionSegment()
        {
            if (grdmidProcessSegment.View.FocusedRowHandle > -1)
            {
                DataRow row = grdmidProcessSegment.View.GetFocusedDataRow();
                if (row != null)
                {
                    Dictionary<string, object> values = new Dictionary<string, object>();
                    values.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);
                    values.Add("P_PROCESSSEGMENTID", row["PROCESSSEGMENTCLASSID"].ToString());
                    DataTable dt = SqlExecuter.Query("GetSelectInspectionSegmentRel", "10001", values);

                    grdmidInspectionControl.DataSource = dt;

                }
            }

        }

        private void SearcStdInspectionSegment()
        {
            if (grdstdProcessSegment.View.FocusedRowHandle > -1)
            {
                DataRow row = grdstdProcessSegment.View.GetFocusedDataRow();
                if (row != null)
                {
                    Dictionary<string, object> values = new Dictionary<string, object>();
                    values.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);
                    values.Add("P_PROCESSSEGMENTID", row["PROCESSSEGMENTID"].ToString());
                    DataTable dt = SqlExecuter.Query("GetSelectInspectionSegmentRel", "10001", values);

                    grdstdInspectionControl.DataSource = dt;
                }
            }
       

        }
        /// <summary>
        /// 표준공정에 매핑된 검사종류를 조회하는 함수
        /// </summary>
        private void SearchInspectionSegment()
        {
            if (grdtopProcessSegment.View.FocusedRowHandle > -1)
            {
                DataRow row = grdtopProcessSegment.View.GetFocusedDataRow();

                Dictionary<string, object> values = new Dictionary<string, object>();
                values.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);
                values.Add("P_PROCESSSEGMENTID", row["PROCESSSEGMENTCLASSID"].ToString());
                DataTable dt = SqlExecuter.Query("GetSelectInspectionSegmentRel", "10001", values);

                grdtopInspectionControl.DataSource = dt;
            }

            if (grdmidProcessSegment.View.FocusedRowHandle > -1)
            {
                DataRow row = grdmidProcessSegment.View.GetFocusedDataRow();
                if (row != null)
                {
                    Dictionary<string, object> values = new Dictionary<string, object>();
                    values.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);
                    values.Add("P_PROCESSSEGMENTID", row["PROCESSSEGMENTCLASSID"].ToString());
                    DataTable dt = SqlExecuter.Query("GetSelectInspectionSegmentRel", "10001", values);

                    grdmidInspectionControl.DataSource = dt;

                }
            }
            if (grdstdProcessSegment.View.FocusedRowHandle > -1)
            {
                DataRow row = grdstdProcessSegment.View.GetFocusedDataRow();
                if (row != null)
                {
                    Dictionary<string, object> values = new Dictionary<string, object>();
                    values.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);
                    values.Add("P_PROCESSSEGMENTID", row["PROCESSSEGMENTID"].ToString());
                    DataTable dt = SqlExecuter.Query("GetSelectInspectionSegmentRel", "10001", values);

                    grdstdInspectionControl.DataSource = dt;
                }
            }
        }
        #endregion

        private void OnSaveConfrimSearch()
        {

            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);
            DataTable dt = null;
            if (tabInspection.SelectedTabPage.Name.Equals("tpgInspectionDef"))
            {
                grdInspectionDef.DataSource = null;

                dt =  SqlExecuter.Query("GetSelectInspectionDefList", "10001", values);
                CheckHasData(dt);
                grdInspectionDef.DataSource = dt;

            }

        }




    }
}
#endregion
