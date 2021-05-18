#region using

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
using DevExpress.XtraEditors.Repository;

#endregion

namespace Micube.SmartMES.OutsideOrderMgnt
{
    /// <summary>
    /// 프 로 그 램 명  :  외주관리> 외주기준관리 > 품목별 ST
    /// 업  무  설  명  :   품목별 ST
    /// 생    성    자  : CHOISSTAR
    /// 생    성    일  : 2019-08-13
    /// 수  정  이  력  : 
    /// 
    /// 
    /// 
    /// </summary>
    public partial class OutsourcingYoungPongfinalinspst : SmartConditionManualBaseForm
    {
        #region Local Variables

        private string _strPlantid = "";   // plant 변경시 작업 

        #endregion

        #region 생성자

        public OutsourcingYoungPongfinalinspst()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 화면의 컨텐츠 영역을 초기화한다.
        /// </summary>
        protected override void InitializeContent()
        {
            base.InitializeContent();
            //이벤트 
            InitializeEvent();
            // TODO : 컨트롤 초기화 로직 구성
            InitializeGrid();
            // 작업용 plant 셋팅 (조회시 다시 셋팅)
            _strPlantid = UserInfo.Current.Plant.ToString();
        }

        private void InitializeGrid()
        {

            grdData.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export;
            //grdData.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdData.View.AddTextBoxColumn("ENTERPRISEID", 100)
                .SetIsHidden();
            grdData.View.AddTextBoxColumn("PLANTID", 100)
                .SetIsHidden();
            InitializeGrid_ModelcodePopup();
          
            grdData.View.AddTextBoxColumn("MODELNAME", 150)
                .SetIsReadOnly();
            //grdData.View.AddTextBoxColumn("ST", 100)
            //    .SetValidationIsRequired()
            //    .SetTextAlignment(TextAlignment.Right)
            //    .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);

            ////적용시작일 텍스트입력
            //grdData.View.AddTextBoxColumn("APPLYSTARTDATE", 100)
            //    .SetValidationIsRequired()
            //    .SetDisplayFormat("yyyy-MM-dd")
            //    .SetTextAlignment(TextAlignment.Center)
            //;

            //적용시작일 달력선택 가능
            grdData.View.AddDateEditColumn("APPLYSTARTDATE", 100)
                .SetValidationIsRequired()
                .SetDisplayFormat("yyyy-MM-dd")
                .SetTextAlignment(TextAlignment.Center)
            ;

            grdData.View.AddTextBoxColumn("ST", 100)
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            grdData.View.AddTextBoxColumn("DESCRIPTION", 250);


            grdData.View.AddComboBoxColumn("VALIDSTATE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
           .SetDefault("Valid")
           .SetValidationIsRequired()
           .SetIsReadOnly();


            grdData.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdData.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdData.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdData.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdData.View.SetKeyColumn("PLANTID", "ENTERPRISEID", "MODELCODE", "APPLYSTARTDATE");
            grdData.View.PopulateColumns();

            RepositoryItemTextEdit edit = new RepositoryItemTextEdit();

            edit.Mask.EditMask = "#,###,##0.####";
            edit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;

            grdData.View.Columns["ST"].ColumnEdit = edit;

            RepositoryItemDateEdit date = new RepositoryItemDateEdit();
            date.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            date.Mask.EditMask = "yyyy-MM-dd";
            date.Mask.UseMaskAsDisplayFormat = true;
            grdData.View.Columns["APPLYSTARTDATE"].ColumnEdit = date;
        }

        private void InitializeGrid_ModelcodePopup()
        {

            var popupGridProcessSegments = grdData.View.AddSelectPopupColumn("MODELCODE", 120,
               new SqlQuery("GetModelcodefinalinspstlistByOsp", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}"))
               // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
               .SetPopupLayout("PRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)
               // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
               .SetPopupResultCount(1)
               // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
               .SetPopupResultMapping("MODELCODE", "MODELCODE")
               // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
               .SetPopupLayoutForm(500, 600, FormBorderStyle.FixedToolWindow)
               // 그리드의 남은 영역을 채울 컬럼 설정

           
               .SetPopupApplySelection((selectedRows, dataGridRow) =>
               {
                   DataRow classRow = grdData.View.GetFocusedDataRow();

                   foreach (DataRow row in selectedRows)
                   {
                       classRow["MODELCODE"] = row["MODELCODE"];
                       classRow["MODELNAME"] = row["MODELNAME"];
                      
                   }
               })
           ;
            
            popupGridProcessSegments.Conditions.AddTextBox("MODELNAME");
            // 팝업 그리드 설정
            popupGridProcessSegments.GridColumns.AddTextBoxColumn("MODELCODE", 150);
          
            popupGridProcessSegments.GridColumns.AddTextBoxColumn("MODELNAME", 200);


        }
        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // 행 추가시 
            grdData.View.AddingNewRow += View_AddingNewRow;
          
            grdData.ToolbarDeleteRow += View_ToolbarDeleteRow;
        }
        /// <summary>
        /// 그리드에서 추가 버튼 클릭 시 호출
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            //회사 ,site ,factory 
            //먼저조회 여부 확인 처리 
            var values = Conditions.GetValues();

            _strPlantid = values["P_PLANTID"].ToString();

            grdData.View.SetFocusedRowCellValue("ENTERPRISEID", UserInfo.Current.Enterprise.ToString());// 회사코드
            grdData.View.SetFocusedRowCellValue("PLANTID", _strPlantid);// plantid
            

        }

        private void View_ToolbarDeleteRow(object sender, EventArgs e)
        {

            DataRow row = grdData.View.GetFocusedDataRow();
            if (row == null)
            {
                return;
            }
            if (row.RowState != DataRowState.Added)
            {
                row["VALIDSTATE"] = "Invalid";
                (grdData.View.DataSource as DataView).Table.AcceptChanges();
            }

        }
        
        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            //base.OnToolbarSaveClick();

            //// TODO : 저장 Rule 변경
            //DataTable changed = grdPeriod.GetChangedRows();

            //ExecuteRule("OutsourcingDeadline", changed);
        }

        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            //base.OnToolbarClick(sender, e);
            SmartButton btn = sender as SmartButton;

            if (btn.Name.ToString().Equals("Save"))
            {

                ProcSave(btn.Text);
            }


        }
        #endregion

        #region 검색

        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            // TODO : 조회 SP 변경
            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
           values = Commons.CommonFunction.ConvertParameter(values);
            DataTable dt = await SqlExecuter.QueryAsync("GetOutsourcingYoungPongfinalinspst2", "10002", values);
            if (dt.Rows.Count < 1) // 검색 조건에 해당하는 DATA가 없는 경우 
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.'

                DataTable dtPrice = (grdData.DataSource as DataTable).Clone();
                //grdDetail.View.ClearDatas();
                grdData.DataSource = dtPrice;
            }
            else
            {
                grdData.DataSource = dt;
            }
           


        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            InitializeConditionPopup_Modelcode();

        }
      

        /// <summary>
        /// site 설정 
        /// </summary>
        private void InitializeConditionPopup_Plant()
        {
            var plantCbobox = Conditions.AddComboBox("p_plantid", new SqlQuery("GetPlantList", "00001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PLANTID", "PLANTID")
              .SetLabel("PLANT")
              .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false)
              .SetPosition(0.1)
              .SetDefault(UserInfo.Current.Plant, "p_plantId") //기본값 설정 UserInfo.Current.Plant
              .SetValidationIsRequired()
              .SetIsReadOnly();

        }

        /// <summary>
        /// 품목코드 조회조건
        /// </summary>
        private void InitializeConditionPopup_Modelcode()
        {
            var popupProduct = Conditions.AddSelectPopup("P_MODELCODE",
                                                                new SqlQuery("GetModelcodefinalinspstlistByOsp", "10001"
                                                                                , $"LANGUAGETYPE={ UserInfo.Current.LanguageType }"
                                                                                , $"ENTERPRISEID={UserInfo.Current.Enterprise}"
                                                                                ), "MODELNAME", "MODELCODE")
                 .SetPopupLayout("MODELCODE", PopupButtonStyles.Ok_Cancel, true, false)
                 .SetPopupLayoutForm(650, 600)
                 .SetLabel("MODELCODE")
                 .SetPopupResultCount(1)
                 .SetPosition(0.1)
                 ;
                 
           

            popupProduct.Conditions.AddTextBox("MODELNAME")
                .SetLabel("MODELNAME");

            popupProduct.GridColumns.AddTextBoxColumn("MODELCODE", 150).SetIsReadOnly();
           
            popupProduct.GridColumns.AddTextBoxColumn("MODELNAME", 200).SetIsReadOnly();

        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
            //Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").ReadOnly = true;
            //Conditions.GetControl<SmartTextBox>("P_PRODUCTDEFVERSION").ReadOnly = true;
           
          // Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").EditValueChanged += ProductDefIDChanged;
        }

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
        /// </summary>
        protected override void OnValidateContent()
        {
         
        }

        #endregion

        #region Private Function

        // TODO : 화면에서 사용할 내부 함수 추가
        /// <summary>
        /// 마스터 정보를 조회한다.
        /// </summary>
        private void LoadData()
        {

        }
        /// <summary>
        /// 단가 기준  key 중복 체크 
        /// </summary>
        /// <returns></returns>
        private bool CheckPriceDateKeyColumns()
        {
            bool blcheck = true;
            
            if (grdData.View.DataRowCount == 0)
            {
                return blcheck;
            }

            //for (int irow = 0; irow < grdData.View.DataRowCount; irow++)
            //{
            //    DataRow row = grdData.View.GetDataRow(irow);
            //    if (row.RowState == DataRowState.Added || row.RowState == DataRowState.Modified)
            //    {
            //        string strModelcode = row["MODELCODE"].ToString();
            //        if (SearchPeriodidKey(strModelcode, irow) < 0)
            //        {
            //            blcheck = true;
            //        }
            //        else
            //        {
            //            string lblPeriodid = grdData.View.Columns["PRODUCTDEFNAME"].Caption.ToString();
            //            this.ShowMessage(MessageBoxButtons.OK, "InValidOspData007", lblPeriodid);
            //            return false;
            //        }
            //    }
            //}

            if (grdData.GetChangedRows().Rows.Count.Equals(0))
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }

            return blcheck;
        }
        /// <summary>
        /// Periodid 중복 체크 함수
        /// </summary>
        /// <param name="dateStartdate"></param>
        /// <param name="dateEnddate"></param>
        /// <param name="icurRow"></param>
        /// <returns></returns>
        private int SearchPeriodidKey(string strModelcode, int icurRow)
        {
            int iresultRow = -1;

            for (int irow = 0; irow < grdData.View.DataRowCount; irow++)
            {
                if (icurRow != irow)
                {
                    DataRow row = grdData.View.GetDataRow(irow);

                    // 행 삭제만 제외 
                    if (grdData.View.IsDeletedRow(row) == false)
                    {
                        string strTempModelcode = row["MODELCODE"].ToString();
                       
                        if (strTempModelcode.Equals(strModelcode))
                        {
                            return irow;
                        }

                    }
                }
            }
            return iresultRow;
        }
        private DataTable createSaveDatatable()
        {
            DataTable dt = new DataTable();
            dt.TableName = "list";
            dt.Columns.Add("ENTERPRISEID");
            dt.Columns.Add("PLANTID");
            dt.Columns.Add("MODELCODE");
            dt.Columns.Add("MODELNAME");
            dt.Columns.Add("ST");
            dt.Columns.Add("APPLYSTARTDATE");
            dt.Columns.Add("DESCRIPTION");
            dt.Columns.Add("VALIDSTATE");

            dt.Columns.Add("USERID");
            dt.Columns.Add("_STATE_");
            return dt;
        }

        private void ProcSave(string strtitle)
        {
            
            grdData.View.FocusedRowHandle = grdData.View.FocusedRowHandle;
            grdData.View.FocusedColumn = grdData.View.Columns["VALIDSTATE"];
            grdData.View.ShowEditor();
            for (int i = 0; i < grdData.View.DataRowCount; i++)
            {
                //PERIODID STARTDATE  ENDDATE Periodid Startdate  Enddate
                string strModelcode = grdData.View.GetRowCellValue(i, "MODELCODE").ToString();
                if (strModelcode.Equals(""))
                {
                    string lblPeriodid = grdData.View.Columns["MODELCODE"].Caption.ToString();
                    this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblPeriodid); //메세지
                    return;

                }
                string strST = grdData.View.GetRowCellValue(i, "ST").ToString();
                if (strST.Equals(""))
                {
                    string lblPeriodid = grdData.View.Columns["ST"].Caption.ToString();
                    this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblPeriodid); //메세지
                    return;

                }

            }

            //if (CheckPriceDateKeyColumns() == false)
            //{
            //    return;
            //}
            // TODO : 유효성 로직 변경
            
            grdData.View.CheckValidation();

            DataTable changed = grdData.GetChangedRows();

            if (changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                this.ShowMessage(MessageBoxButtons.OK, "NoSaveData");
                return;
            }


            //DataTable dt2 = grdData.View.GetCheckedRows();

            //if (dt2.Rows.Count == 0)
            //{
            //    ShowMessage("GridNoChecked");
            //    return;
            //}

            DialogResult result = System.Windows.Forms.DialogResult.No;

            result = this.ShowMessage(MessageBoxButtons.YesNo, "OspDoProcessWant", strtitle);//저장하시겠습니까? 

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    this.ShowWaitArea();

                    //DataTable dtSave = grdData.GetChangedRows();
                    //ExecuteRule("OutsourcingYoungPongfinalinspst", dtSave);

                    //체크박스 추가시 체크해주세요. _state_ 값처리 필요
                    //DataTable dt = grdData.View.GetCheckedRows();

                    //if (dt.Rows.Count == 0)
                    //{
                    //    ShowMessage("GridNoChecked");
                    //    return;
                    //}
                    //if (dt.Rows.Count > 0)
                    //else

                    //그리드 변경값으로 처리시...
                    DataTable dt = grdData.GetChangedRows();

                    // 수정된 데이터가 없는 경우 메시지 처리
                    if (dt.Rows.Count == 0)
                    {
                        throw MessageException.Create("NoSaveData"); //저장할 데이터 없음
                    }
                    else
                    {
                        DataTable dtsave = createSaveDatatable();
                        dt.DefaultView.Sort = "PLANTID ASC, ENTERPRISEID ASC, MODELCODE ASC";
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DataRow row = dt.Rows[i];
                            string strPlantid           = "";
                            string strEnterpriseid      = "";
                            string strModelcode         = "";
                            string strApplystartdate    = "";
                            if ( !( strPlantid.Equals(row["PLANTID"].ToString()) && strEnterpriseid.Equals(row["ENTERPRISEID"].ToString()) && strModelcode.Equals(row["MODELCODE"].ToString()) ) )
                            {
                                DataRow dr = dtsave.NewRow();
                                DateTime dtApplystartdate = Convert.ToDateTime(row["APPLYSTARTDATE"].ToString());
                                strApplystartdate = string.Format("{0:yyyy-MM-dd}", dtApplystartdate);
                                dr["ENTERPRISEID"   ]   = row["ENTERPRISEID"    ].ToString();
                                dr["PLANTID"        ]   = row["PLANTID"         ].ToString();
                                dr["MODELCODE"      ]   = row["MODELCODE"       ].ToString();
                                dr["ST"             ]   = row["ST"              ].ToString();
                                dr["APPLYSTARTDATE" ]   = strApplystartdate.ToString();         // row["APPLYSTARTDATE"  ].ToString();
                                dr["DESCRIPTION"    ]   = row["DESCRIPTION"     ].ToString();
                                dr["VALIDSTATE"     ]   = row["VALIDSTATE"      ].ToString();
                                dr["USERID"         ]   = UserInfo.Current.Id.ToString();
                                dr["_STATE_"        ]   = row["_STATE_"         ].ToString();
                                strPlantid              = row["PLANTID"         ].ToString();
                                strEnterpriseid         = row["ENTERPRISEID"    ].ToString();
                                strModelcode            = row["MODELCODE"       ].ToString();
                                dtsave.Rows.Add(dr);
                            }
                        }
                        ExecuteRule("OutsourcingYoungPongfinalinspst", dtsave);
                        //worker.ExecuteNoResponse();
                        ShowMessage("SuccessOspProcess");
                        OnSaveConfrimSearch();
                    }

                    //ShowMessage("SuccessOspProcess");
                    ////재조회 
                    //OnSaveConfrimSearch();
                }
                catch (Exception ex)
                {
                    this.ShowError(ex);
                }
                finally
                {
                    this.CloseWaitArea();


                }
            }
        }
        private void OnSaveConfrimSearch()
        {

            // TODO : 조회 SP 변경
            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                values = Commons.CommonFunction.ConvertParameter(values);
                DataTable dt = SqlExecuter.Query("GetOutsourcingYoungPongfinalinspst2", "10002", values);
                if (dt.Rows.Count < 1) // 검색 조건에 해당하는 DATA가 없는 경우 
                {
                    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.'

                    DataTable dtPrice = (grdData.DataSource as DataTable).Clone();
                    //grdDetail.View.ClearDatas();
                    grdData.DataSource = dtPrice;
                }
                else
                {
                    grdData.DataSource = dt;
                }
            
        }
        #endregion
    }
}
