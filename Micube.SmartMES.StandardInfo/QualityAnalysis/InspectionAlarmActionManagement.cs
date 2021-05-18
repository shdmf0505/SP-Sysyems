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

#endregion

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프 로 그 램 명  : 기준정보 > 품질 기준 정보 > 검사조치 및 실행
    /// 업  무  설  명  : 품질 기준 정보의 검사조치 및 실행을 관리한다.
    /// 생    성    자  : 강유라
    /// 생    성    일  : 2019-06-28
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class InspectionAlarmActionManagement : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가


        #endregion

        #region 생성자

        public InspectionAlarmActionManagement()
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

            InitializeEvent();

            //Conditions.SetValue("p_plantid", 0, UserInfo.Current.Plant);
            //Conditions.GetControl<SmartTextBox>("p_plantid").Enabled = false;

            // TODO : 컨트롤 초기화 로직 구성
            InitializeGrdQCInterlockAction();
            InitializeGrdQCInterlockGrade();
            InitializeGridQCInterlock();
        }

        #region 판정 등급 리스트 그리드를 초기화한다.
        /// <summary>        
        /// 판정 등급 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGridQCInterlock()
        {
            // TODO : 그리드 초기화 로직 추가
            grdQcInterlock.GridButtonItem = GridButtonItem.Add | GridButtonItem.Export;
            grdQcInterlock.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            grdQcInterlock.View.AddTextBoxColumn("INSPECTIONCLASSID", 80)
                .SetIsHidden()
                ;
            grdQcInterlock.View.AddTextBoxColumn("INSPECTIONCLASS", 120)
                .SetValidationIsRequired()
                .SetIsHidden()
                ;
            grdQcInterlock.View.AddComboBoxColumn("DECISIONDEGREE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=DecisionDegree", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetValidationIsRequired()
                ;
            grdQcInterlock.View.AddComboBoxColumn("QCDECISIONTYPE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=QCDecisionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetValidationIsRequired()
                ;
            grdQcInterlock.View.AddSpinEditColumn("PRIORITY", 100)
                .SetValidationIsRequired()
                ;
            grdQcInterlock.View.AddSpinEditColumn("SEQ", 100)
                .SetIsHidden()
                ;
            grdQcInterlock.View.AddComboBoxColumn("QCGRADE", 80, new SqlQuery("GetQCGradeListForComboBoxByStandardInfo", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"P_ENTERPRISEID={UserInfo.Current.Enterprise}", $"P_PLANTID={UserInfo.Current.Plant}"), "QCGRADE", "QCGRADE")
               .SetValidationIsRequired()
               ;
            //InitializeQCGradePopupColumn();
            grdQcInterlock.View.AddComboBoxColumn("NGCONDITION", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=Condition", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetTextAlignment(TextAlignment.Center)
                .SetEmptyItem("", "", true)
                ;
            grdQcInterlock.View.AddComboBoxColumn("QTYORRATE", 120, new SqlQuery("GetCodeList", "00001", "CODECLASSID=QtyDefectRateType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("QTYORRATE")
                .SetEmptyItem("", "", true)
                //.SetDefault("NCR")
                ;
            grdQcInterlock.View.AddTextBoxColumn("FROMRATE", 100)
                .SetDisplayFormat("#,###,###.##")
                .MaskType = MaskTypes.Numeric
                ;
            grdQcInterlock.View.AddTextBoxColumn("TORATE", 100)
                .SetDisplayFormat("#,###,###.##")
                .MaskType = MaskTypes.Numeric
                ;
            grdQcInterlock.View.AddTextBoxColumn("NGQUANTITY", 80)
                .SetDisplayFormat("#,###,###.##")                                
                .MaskType = MaskTypes.Numeric
                ;
            grdQcInterlock.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetTextAlignment(TextAlignment.Center)
                    .SetDefault("Valid");   //유효여부
            grdQcInterlock.View.AddTextBoxColumn("ENTERPRISEID", 100)
                .SetIsHidden()
                ;
            grdQcInterlock.View.AddTextBoxColumn("PLANTID", 100)
                .SetIsHidden()
                ;
            grdQcInterlock.View.SetKeyColumn("DECISIONDEGREE", "SEQ");
            grdQcInterlock.View.PopulateColumns();
        }
        #endregion

        #region InitializeQCGradePopupColumn - 조치등급을 팝업검색하기 위한 팝업창 설정
        private void InitializeQCGradePopupColumn()
        {
            ConditionItemSelectPopup popupQCGrade = grdQcInterlock.View.AddSelectPopupColumn("QCGRADE", 150
                , new SqlQuery("GetQCGradeListForInterLockByStandardInfo", "10001", $"P_VALIDSTATE={"Valid"}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"))
                // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
                .SetPopupLayout("QCGRADE", PopupButtonStyles.Ok_Cancel, true, false)
                // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
                .SetPopupResultCount(1)
                //필수값설정
                .SetValidationIsRequired()
                // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
                .SetPopupResultMapping("QCGRADE", "QCGRADE")
                // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
                .SetPopupLayoutForm(500, 600, FormBorderStyle.FixedToolWindow)
                // 그리드의 남은 영역을 채울 컬럼 설정                
                .SetPopupAutoFillColumns("QCGRADE")
            //.SetPopupApplySelection((selectedRows, dataGridRow) =>
            //{
            //    int i = 0;
            //    int currentIndex = grdFilmRequest.View.GetFocusedDataSourceRowIndex();

            //    foreach (DataRow row in selectedRows)
            //    {
            //        if (i == 0)
            //        {
            //            grdFilmRequest.View.GetFocusedDataRow()["RECEIVEAREAID"] = row["AREAID"];
            //            grdFilmRequest.View.GetFocusedDataRow()["RECEIVEAREA"] = row["AREANAME"];
            //        }
            //        i++;
            //    }
            //})
            //.SetPopupValidationCustom(ValidationParentAreaIdPopup);
            ;
            // 팝업에서 사용할 조회조건 항목 추가
            //popupGridToolArea.Conditions.AddTextBox("AREANAME");
            //popupGridToolArea.Conditions.AddComboBox("FACTORYID", new SqlQuery("GetFactoryListByEqp", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "FACTORYNAME", "FACTORYID")
            //    ;

            // 팝업 그리드 설정
            popupQCGrade.GridColumns.AddTextBoxColumn("QCGRADE", 100)
                .SetIsReadOnly();
            popupQCGrade.GridColumns.AddTextBoxColumn("DESCRIPTION", 300)
                .SetIsReadOnly();
            popupQCGrade.GridColumns.AddTextBoxColumn("ACTIONID", 100)
                .SetIsReadOnly();
        }
        #endregion

        #region Alarm Grade 리스트 그리드를 초기화한다.
        private void InitializeGrdQCInterlockGrade()
        {
            // TODO : 그리드 초기화 로직 추가
            grdQCInterlockGrade.GridButtonItem = GridButtonItem.Add | GridButtonItem.Export;
            grdQCInterlockGrade.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            grdQCInterlockGrade.View.AddTextBoxColumn("QCGRADE", 150)
                ;
            //grdQCInterlockGrade.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
            //  .SetDefault("Valid")   //유효여부
            //  .SetTextAlignment(TextAlignment.Center);
            grdQCInterlockGrade.View.SetKeyColumn("QCGRADE");
            grdQCInterlockGrade.View.PopulateColumns();
        }
        #endregion

        #region Alarm 리스트 그리드를 초기화한다.
        private void InitializeGrdQCInterlockAction()
        {
            // TODO : 그리드 초기화 로직 추가
            grdQCInterLockAction.GridButtonItem = GridButtonItem.Export;
            grdQCInterLockAction.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdQCInterLockAction.View.AddTextBoxColumn("CHECKEDACTION", 30).SetIsHidden();
            grdQCInterLockAction.View.AddTextBoxColumn("ACTIONID", 80).SetIsHidden();
            //InitializeQCActionPopupColumn();
            grdQCInterLockAction.View.AddTextBoxColumn("ACTIONNAME", 200);
            grdQCInterLockAction.View.AddTextBoxColumn("ACTIONTYPE", 150);

            //grdQCInterlockGrade.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
            //  .SetDefault("Valid")   //유효여부
            //  .SetTextAlignment(TextAlignment.Center);

            grdQCInterLockAction.View.AddTextBoxColumn("ENTERPRISEID", 80).SetIsHidden();
            grdQCInterLockAction.View.AddTextBoxColumn("PLANTID", 80).SetIsHidden();
            grdQCInterLockAction.View.SetKeyColumn("ACTIONID");
            grdQCInterLockAction.View.PopulateColumns();
        }
        #endregion

        #region InitializeQCActionPopupColumn - Action을 팝업검색하기 위한 팝업창 설정
        private void InitializeQCActionPopupColumn()
        {
            ConditionItemSelectPopup popupQCGrade = grdQCInterlockGrade.View.AddSelectPopupColumn("ACTIONNAME", 150, new SqlQuery("SelectSFAction", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"))
                // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
                .SetPopupLayout("ACTIONNAME", PopupButtonStyles.Ok_Cancel, true, false)
                // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
                .SetPopupResultCount(1)
                //필수값설정
                .SetValidationIsRequired()
                // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
                .SetPopupResultMapping("ACTIONNAME", "ACTIONNAME")
                // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
                .SetPopupLayoutForm(500, 600, FormBorderStyle.FixedToolWindow)
                // 그리드의 남은 영역을 채울 컬럼 설정                
                .SetPopupAutoFillColumns("ACTIONNAME")
                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {
                    int i = 0;
                    int currentIndex = grdQCInterlockGrade.View.GetFocusedDataSourceRowIndex();

                    foreach (DataRow row in selectedRows)
                    {
                        if (i == 0)
                        {
                            grdQCInterlockGrade.View.GetFocusedDataRow()["ACTIONID"] = row["ACTIONID"];
                            grdQCInterlockGrade.View.GetFocusedDataRow()["ACTIONTYPE"] = row["ACTIONTYPE"];
                        }
                        i++;
                    }
                })
            ;
            // 팝업에서 사용할 조회조건 항목 추가
            //popupGridToolArea.Conditions.AddTextBox("AREANAME");
            //popupGridToolArea.Conditions.AddComboBox("FACTORYID", new SqlQuery("GetFactoryListByEqp", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "FACTORYNAME", "FACTORYID")
            //    ;

            // 팝업 그리드 설정
            popupQCGrade.GridColumns.AddTextBoxColumn("ACTIONID", 100)
                .SetIsReadOnly();
            popupQCGrade.GridColumns.AddTextBoxColumn("ACTIONNAME", 300)
                .SetIsReadOnly();
            popupQCGrade.GridColumns.AddTextBoxColumn("ACTIONTYPE", 100)
                .SetIsReadOnly();
        }
        #endregion
        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            //화면 Load이벤트
            Load += InspectionActionManagement_Load;
            //판정등급 그리드의 새ROW 추가 이벤트
            grdQcInterlock.View.AddingNewRow += (s, e) =>
            {
                e.NewRow["ENTERPRISEID"] = Framework.UserInfo.Current.Enterprise;
                e.NewRow["PLANTID"] = Framework.UserInfo.Current.Plant;
                e.NewRow["INSPECTIONCLASSID"] = Conditions.GetValue("P_INSPECTIONCLASSID").ToString();
                e.NewRow["INSPECTIONCLASS"] = ((SmartComboBox)Conditions.GetControl("P_INSPECTIONCLASSID")).GetDisplayText();

                //시퀀스를 할당한다.
                e.NewRow["SEQ"] = grdQcInterlock.View.RowCount;
            };

            //grdQCInterlockAction.View.AddingNewRow += (s, e) =>
            //{
            //    string inspectionDefId = this.grdQcInterlock.View.GetFocusedRowCellValue("INSPECTIONDEFID").ToString();
            //    string qcgrade = this.grdQcInterlock.View.GetFocusedRowCellValue("DECISIONDEGREE").ToString();
            //    string qcgradesequence = this.grdQcInterlock.View.GetFocusedRowCellValue("SEQ").ToString();

            //    e.NewRow["ENTERPRISEID"] = Framework.UserInfo.Current.Enterprise;
            //    e.NewRow["PLANTID"] = Framework.UserInfo.Current.Plant;
            //    e.NewRow["INSPECTIONDEFID"] = inspectionDefId;
            //    e.NewRow["QCGRADE"] = qcgrade;
            //    e.NewRow["QCGRADESEQUENCE"] = qcgradesequence;

            //};
            //판정기준 그리드의 새ROW 추가 이벤트
            grdQCInterlockGrade.View.FocusedRowChanged += grdQCInterlockGrade_FocusedRowChanged;
            grdQcInterlock.View.CellValueChanged += grdQcInterlock_CellValueChanged;
            grdQcInterlock.View.ShowingEditor += grdQcInterlockView_ShowingEditor;
        }

        private void grdQCInterlockGrade_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            SearchGradeAndAction();
        }

        private void grdQcInterlock_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            grdQcInterlock.View.CellValueChanged -= grdQcInterlock_CellValueChanged;
            //to 불량율은 from 불량율보다 클수 없다. (NG조건이 [사이]인 경우만 From 불량율을 입력할 수 있다.)
            if (e.Column.FieldName.Equals("TORATE"))
            {
                if (grdQcInterlock.View.GetDataRow(e.RowHandle).GetString("NGCONDITION").Equals("BT"))
                {
                    if (grdQcInterlock.View.GetDataRow(e.RowHandle).GetInteger("FROMRATE") > Convert.ToInt32(e.Value))
                    {
                        grdQcInterlock.View.SetRowCellValue(e.RowHandle, "TORATE", 0);
                        ShowMessage("ValidateInterLockFromMoreTo");
                    }
                }
            }
            //NGCondition선택시 상황에 맞추어 값을 재정렬
            else if(e.Column.FieldName.Equals("NGCONDITION"))
            {
                if(!Convert.ToString(e.Value).Equals("BT") && grdQcInterlock.View.GetDataRow(e.RowHandle).GetString("QTYORRATE").Equals("RATE"))
                {
                    grdQcInterlock.View.SetRowCellValue(e.RowHandle, "FROMRATE", "");
                    grdQcInterlock.View.SetRowCellValue(e.RowHandle, "NGQUANTITY", "");
                }
                else if(Convert.ToString(e.Value).Equals("BT") && grdQcInterlock.View.GetDataRow(e.RowHandle).GetString("QTYORRATE").Equals("RATE"))
                {
                    grdQcInterlock.View.SetRowCellValue(e.RowHandle, "NGQUANTITY", "");
                }
                else if (grdQcInterlock.View.GetDataRow(e.RowHandle).GetString("QTYORRATE").Equals("QTY"))
                {
                    grdQcInterlock.View.SetRowCellValue(e.RowHandle, "FROMRATE", "");
                    grdQcInterlock.View.SetRowCellValue(e.RowHandle, "TORATE", "");
                }
            }
            //QtyOrRate를 선택시 상황에 맞추어 값을 재정렬
            if(e.Column.FieldName.Equals("QTYORRATE"))
            {
                if (e.Value.ToString().Equals("RATE"))
                {
                    grdQcInterlock.View.SetRowCellValue(e.RowHandle, "NGQUANTITY", "");
                }
                else if (e.Value.ToString().Equals("QTY"))
                {
                    grdQcInterlock.View.SetRowCellValue(e.RowHandle, "FROMRATE", "");
                    grdQcInterlock.View.SetRowCellValue(e.RowHandle, "TORATE", "");
                }
            }
            //조치등급 선택시 AQL의 경우 하나의 판정등급에 2개의 AQL이 있으면 안된다.
            if(e.Column.FieldName.Equals("QCDECISIONTYPE"))
            {
                if (!IsUniqQCGrade((DataTable)grdQcInterlock.DataSource))
                {
                    grdQcInterlock.View.SetRowCellValue(e.RowHandle, "QCDECISIONTYPE", null);
                    ShowMessage("ValidationQCDecisionTypeUniq");
                }
                else
                {
                    if (e.Value.ToString().Equals("AQL"))
                    {
                        //다른 모든 값을 null처리
                        grdQcInterlock.View.SetRowCellValue(e.RowHandle, "PRIORITY", 1);
                        grdQcInterlock.View.SetRowCellValue(e.RowHandle, "FROMRATE", "");
                        grdQcInterlock.View.SetRowCellValue(e.RowHandle, "TORATE", "");
                        grdQcInterlock.View.SetRowCellValue(e.RowHandle, "NGQUANTITY", "");
                        grdQcInterlock.View.SetRowCellValue(e.RowHandle, "QTYORRATE", null);
                        grdQcInterlock.View.SetRowCellValue(e.RowHandle, "NGCONDITION", null);
                    }
                }
            }
            if (e.Column.FieldName.Equals("DECISIONDEGREE"))
            {
                if (!IsUniqQCGrade((DataTable)grdQcInterlock.DataSource))
                {
                    grdQcInterlock.View.SetRowCellValue(e.RowHandle, "DECISIONDEGREE", null);
                    ShowMessage("ValidationQCDecisionTypeUniq");
                }
            }
            grdQcInterlock.View.CellValueChanged += grdQcInterlock_CellValueChanged;
        }

        private void grdQcInterlockView_ShowingEditor(object sender, CancelEventArgs e)
        {
            string currentColumnName = grdQcInterlock.View.FocusedColumn.FieldName;

            if (currentColumnName.Equals("PRIORITY"))
            {
                if (grdQcInterlock.View.GetFocusedDataRow().GetString("QCDECISIONTYPE").Equals("AQL"))
                {
                    e.Cancel = true;
                }
            }
            //수량/불량율 구분은 NG조건이 선택되어져야 한다.
            if (currentColumnName.Equals("NGCONDITION"))
            {
                if (grdQcInterlock.View.GetFocusedDataRow().GetString("QCDECISIONTYPE").Equals("AQL"))
                {
                    e.Cancel = true;
                }
            }
            //수량/불량율 구분은 NG조건이 선택되어져야 한다.
            if (currentColumnName.Equals("QTYORRATE"))
            {
                if (grdQcInterlock.View.GetFocusedDataRow().GetString("QCDECISIONTYPE").Equals("AQL"))
                {
                    e.Cancel = true;
                }
                if (grdQcInterlock.View.GetFocusedDataRow().GetString("NGCONDITION").Equals(""))
                {
                    e.Cancel = true;
                }
            }
            //From 불량율은 NG조건이 BT이며 수량/불량율이 불량율일때만 입력가능하다.
            if (currentColumnName.Equals("FROMRATE"))
            {
                if (grdQcInterlock.View.GetFocusedDataRow().GetString("QCDECISIONTYPE").Equals("AQL"))
                {
                    e.Cancel = true;
                }

                if (!grdQcInterlock.View.GetFocusedDataRow().GetString("NGCONDITION").Equals("BT") || !grdQcInterlock.View.GetFocusedDataRow().GetString("QTYORRATE").Equals("RATE"))
                {
                    e.Cancel = true;
                }
            }
            //To 불량율은 수량/불량율이 불량율일때만 입력가능하다.
            if (currentColumnName.Equals("TORATE"))
            {
                if (grdQcInterlock.View.GetFocusedDataRow().GetString("QCDECISIONTYPE").Equals("AQL"))
                {
                    e.Cancel = true;
                }

                if (!grdQcInterlock.View.GetFocusedDataRow().GetString("QTYORRATE").Equals("RATE"))
                {
                    e.Cancel = true;
                }
            }
            //불량수량의 경우 수량/불량율이 수량일 때만 입력이 가능하다.
            if (currentColumnName.Equals("NGQUANTITY"))
            {
                if (grdQcInterlock.View.GetFocusedDataRow().GetString("QCDECISIONTYPE").Equals("AQL"))
                {
                    e.Cancel = true;
                }

                if (!grdQcInterlock.View.GetFocusedDataRow().GetString("QTYORRATE").Equals("QTY"))
                {
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// Load 이벤트 - 팝업 초기화, Control 초기화, Combobox 바인딩
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InspectionActionManagement_Load(object sender, EventArgs e)
        {
        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            DataTable dtChangeQcInterlock = null;

            try
            {
                dtChangeQcInterlock = this.grdQcInterlock.GetChangedRows();
            }
            catch
            { }

            DataTable dtChangeQcInterlockAction = null;
            try
            {
                dtChangeQcInterlockAction = GetInterLockData(GetInterLockTemplate());
            }
            catch
            { }

            if (dtChangeQcInterlock.Rows.Count == 0 && dtChangeQcInterlockAction.Rows.Count == 0)
                throw MessageException.Create("NoSaveData");
            else
            {
                DataTable dtOriQcInterlock = this.grdQcInterlock.DataSource as DataTable;
                DataTable dtOriQcInterlockAction = this.grdQCInterlockGrade.DataSource as DataTable;

                string messageCode = "";
                foreach(DataRow row in dtOriQcInterlock.Rows)
                {
                    if (!ValidateQCInterLock(row, out messageCode))
                    {
                        throw MessageException.Create(messageCode);
                    }
                }
                MessageWorker worker = new MessageWorker("SaveInspectionAlarmActionDegree");
                worker.SetBody(new MessageBody()
                        {
                            { "qcinterlockList", dtChangeQcInterlock },
                            { "qcInterlockactionList", dtChangeQcInterlockAction }
                        });

                worker.Execute();

                grdQcInterlock.View.RefreshComboBoxDataSource("QCGRADE", new SqlQuery("GetQCGradeListForComboBoxByStandardInfo", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"P_ENTERPRISEID={UserInfo.Current.Enterprise}", $"P_PLANTID={UserInfo.Current.Plant}"));
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


            this.grdQcInterlock.DataSource = null;
            this.grdQCInterlockGrade.DataSource = null;

            values.Add("P_ENTERPRISEID", Framework.UserInfo.Current.Enterprise);
            //values.Add("PLANTID", Framework.UserInfo.Current.Plant);
            values.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);

            DataTable dtDegri = await SqlExecuter.QueryAsync("GetQCInterLockListByStandardInfo", "10001", values);

            //if (dtDegri.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            //{
            //    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            //}

            this.grdQcInterlock.DataSource = dtDegri;

            DataTable dtGrade = await SqlExecuter.QueryAsync("GetQCGradeListForComboBoxByStandardInfo", "10001", values);

            //if (dtAction.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            //{
            //    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            //}

            grdQCInterlockGrade.DataSource = dtGrade;

            SearchGradeAndAction();
        }

        #region SearchGradeAndAction
        private void SearchGradeAndAction()
        {
            if (grdQCInterlockGrade.View.GetFocusedDataRow() != null)
            {
                var values = Conditions.GetValues();

                values.Add("P_ENTERPRISEID", Framework.UserInfo.Current.Enterprise);
                //values.Add("PLANTID", Framework.UserInfo.Current.Plant);
                values.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);
                values.Add("QCGRADE", grdQCInterlockGrade.View.GetFocusedDataRow().GetString("QCGRADE"));

                DataTable dtAction = SqlExecuter.Query("GetActionListForInterLockByStandardInfo", "10001", values);

                this.grdQCInterLockAction.DataSource = dtAction;

                for (int i = 0; i < grdQCInterLockAction.View.RowCount; i++)
                {
                    if (grdQCInterLockAction.View.GetRowCellValue(i, "CHECKEDACTION").ToString().Equals("True"))
                        grdQCInterLockAction.View.CheckRow(i, true);
                    else
                        grdQCInterLockAction.View.CheckRow(i, false);
                }
            }
        }
        #endregion

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            InitializeConditionInspectionClass();
            InitializeConditionValidState();
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
        }

        #region InitializeConditionInspectionClass : 검사종류 조회
        /// <summary>
        /// site 설정 
        /// </summary>
        private void InitializeConditionInspectionClass()
        {
            var inspectionClassBox = Conditions.AddComboBox("P_INSPECTIONCLASSID", new SqlQuery("SelectInspectionClassListForCombo", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"P_ENTERPRISEID={UserInfo.Current.Enterprise}", $"P_PLANTID={UserInfo.Current.Plant}"), "INSPECTIONCLASSNAME", "INSPECTIONCLASSID")
               .SetLabel("INSPECTIONCLASSID")
               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
               .SetPosition(1.1)
               .SetEmptyItem("", "", true)
               .SetValidationIsRequired()
            //.SetDefault(UserInfo.Current.Plant, "PLANTID") //기본값 설정 UserInfo.Current.Plant
            ;
        }
        #endregion

        #region InitializeConditionValidState : 유효여부
        /// <summary>
        /// site 설정 
        /// </summary>
        private void InitializeConditionValidState()
        {
            //var inspectionClassBox = Conditions.AddComboBox("P_VALIDSTATE", new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CODECLASSID=ValidState", $"P_ENTERPRISEID={UserInfo.Current.Enterprise}", $"P_PLANTID={UserInfo.Current.Plant}"), "CODEID", "CODENAME")
            //   .SetLabel("VALIDSTATE")
            //   .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
            //   .SetPosition(1.2)
            //   .SetEmptyItem("", "", true)
            //.SetDefault(UserInfo.Current.Plant, "PLANTID") //기본값 설정 UserInfo.Current.Plant
            ;
        }
        #endregion

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            // TODO : 유효성 로직 변경
        }

        #region ValidateQCInterLock : QCInterLock의 입력/수정시 한행의 유효성을 검증한다.
        private bool ValidateQCInterLock(DataRow currentRow, out string messageCode)
        {
            messageCode = "";

            //검사종류, 판정등급, 순번, 조치등급은 필수값이다.
            if (currentRow.GetString("INSPECTIONCLASSID").Equals(""))
            {
                messageCode = "ValidateInterLockINSPClass"; //검사종류가 지정되어 있지 않습니다.
                return false;
            }

            if (currentRow.GetString("DECISIONDEGREE").Equals(""))
            {
                messageCode = "ValidateInterLockDECISIONGrade"; //판정등급을 입력하세요.
                return false;
            }

            if (currentRow.GetString("SEQ").Equals(""))
            {
                messageCode = "ValidateInterLockSEQ"; //순번을 입력하세요.
                return false;
            }

            if (currentRow.GetString("QCGRADE").Equals(""))
            {
                messageCode = "ValidateInterLockQCGrade"; //조치등급을 입력하세요.
                return false;
            }

            if (currentRow.GetString("VALIDSTATE").Equals(""))
            {
                messageCode = "ValidateInterLockVALIDSTATE"; //유효상태를 입력하세요.
                return false;
            }

            //NG조건과 수량/불량율 구분이 선택이 되지 않았을 경우에는 입력이 가능하지만 From불량율, To불량율, 불량수량들도 모두 입력이 되어서는 안된다.
            if (currentRow.GetString("NGCONDITION").Equals("") && currentRow.GetString("QTYORRATE").Equals("") && currentRow.GetString("FROMRATE").Equals("")
                && currentRow.GetString("TORATE").Equals("") && currentRow.GetString("NGQUANTITY").Equals(""))
            {
                //이하 행들이 모두 입력되지 않았다면 입력이 가능하다.
                return true;
            }

            //NG조건은 필수값이다.
            if (currentRow.GetString("NGCONDITION").Equals("") && !currentRow.GetString("QCDECISIONTYPE").Equals("AQL"))
            {
                messageCode = "ValidateInterLockNGCondition"; //NG조건을 입력하세요.
                return false;
            }

            //수량/불량율구분은 필수값이다.
            if (currentRow.GetString("QTYORRATE").Equals("") && !currentRow.GetString("QCDECISIONTYPE").Equals("AQL"))
            {
                messageCode = "ValidateInterLockQTYORRATE"; //수량/불량율 구분을 입력하세요.
                return false;
            }

            //NG조건이 BT이며 수량/불량율 구분이 불량율인경우 From불량율과 To불량율 모두 입력해야 한다.
            if (currentRow.GetString("NGCONDITION").Equals("BT") && currentRow.GetString("QTYORRATE").Equals("RATE"))
            {
                //From불량율, To불량율 모두 입력해야 한다.
                if (currentRow.GetString("FROMRATE").Equals("") || currentRow.GetString("TORATE").Equals(""))
                {
                    messageCode = "ValidateInterLockFROMandTO"; //NG조건이 [사이]인경우 From 불량율과 To 불량율을 모두 입력하셔야 합니다.
                    return false;
                }

                //From불량율이 To불량율보다 크면 안된다.
                if (!IsBiggerThanToRate(currentRow) && !currentRow.GetString("QCDECISIONTYPE").Equals("AQL") && currentRow.GetString("NGCONDITION").Equals("BT"))
                {
                    messageCode = "ValidateInterLockFromMoreTo"; //From 불량율은 To 불량율보다 클 수 없습니다.
                    return false;
                }
            }
            //NG조건이 BT가 아니며 수량/불량율 구분이 불량율인경우 To불량율만 입력해야 한다.
            else if (!currentRow.GetString("NGCONDITION").Equals("BT") && currentRow.GetString("QTYORRATE").Equals("RATE") && !currentRow.GetString("QCDECISIONTYPE").Equals("AQL"))
            {
                //From 불량율을 입력하면 안된다.
                if (!currentRow.GetString("FROMRATE").Equals(""))
                {
                    messageCode = "ValidateInterLockNGFrom"; //NG조건이 [사이]가 아닌 경우에는 From 불량율을 입력하면 안됩니다.
                    return false;
                }
                //To불량율을 입력해야 한다.
                if (currentRow.GetString("TORATE").Equals(""))
                {
                    messageCode = "ValidateInterLockTORATE"; //To 불량율을 입력하세요.
                    return false;
                }
            }
            //위의 2경우를 제외하면 나머지는 수량/불량율 구분에서 수량을 선택한 것으로 판단하며, 이 때 불량수량은 반드시 입력되어야 한다.
            else
            {
                //To불량율을 입력해야 한다.
                if (currentRow.GetString("NGQUANTITY").Equals("") && !currentRow.GetString("QCDECISIONTYPE").Equals("AQL"))
                {
                    messageCode = "ValidateInterLockQCQuantity"; //불량수량을 입력하세요.
                    return false;
                }
            }
            //위의 2경우를 제외하면 나머지는 수량/불량율 구분에서 수량을 선택한 것으로 판단하며, 이 때 불량수량은 From 불량율보다 크면 안된다.
            return true;
        }
        #endregion

        #region IsInsertFromToRate : NG조건에 따라 From To 불량율의 입력여부를 판단
        private int IsInsertFromToRate(DataRow currentRow)
        {
            if (currentRow.GetString("NGCONDITION").Equals(""))
                return 0;
            else if (currentRow.GetString("NGCONDITION").Equals("BT"))
                return 2;
            else
                return 1;
        }
        #endregion

        #region IsInsertQuantity : 수량/불량율 구분에 근거하여 불량수량을 입력할 수 있는지 판단
        private bool IsInsertQuantity(DataRow currentRow)
        {
            if (currentRow.GetString("QTYORRATE").Equals("QTY"))
                return true;
            return false;
        }
        #endregion

        #region IsBiggerThanToRate : From불량율은 To불량율보다 크면 안됨
        private bool IsBiggerThanToRate(DataRow currentRow)
        {
            if (currentRow.GetInteger("TORATE") >= currentRow.GetInteger("FROMRATE"))
                return true;
            return false;
        }
        #endregion

        #region IsBiggerThanFromRate : 불량수량은 From불량율보다 크면 안됨
        private bool IsBiggerThanFromRate(DataRow currentRow)
        {
            if (currentRow.GetInteger("NGQUANTITY") > currentRow.GetInteger("FROMRATE"))
                return false;
            return true;
        }
        #endregion

        #endregion

        #region Private Function
        private DataTable GetInterLockTemplate()
        {
            DataTable dtTemp = new DataTable("qcInterlockactionList");

            dtTemp.Columns.Add("QCGRADE");
            dtTemp.Columns.Add("ACTIONID");
            dtTemp.Columns.Add("ENTERPRISEID");
            dtTemp.Columns.Add("PLANTID");
            dtTemp.Columns.Add("VALIDSTATE");
            dtTemp.Columns.Add("_STATE_");

            return dtTemp;
        }

        private DataTable GetInterLockData(DataTable template)
        {
            if (grdQCInterlockGrade.View.GetFocusedDataRow() != null)
            {
                string qcGrade = grdQCInterlockGrade.View.GetFocusedDataRow().GetString("QCGRADE");

                for(int i = 0; i < grdQCInterLockAction.View.RowCount; i++)
                {
                    DataRow newRow = template.NewRow();
                    DataRow currentRow = grdQCInterLockAction.View.GetDataRow(i);
                    newRow["QCGRADE"] = qcGrade;
                    newRow["ACTIONID"] = currentRow.GetString("ACTIONID");
                    newRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                    newRow["PLANTID"] = UserInfo.Current.Plant;

                    if (grdQCInterLockAction.View.IsRowChecked(i))
                        newRow["VALIDSTATE"] = "Valid";
                    else
                        newRow["VALIDSTATE"] = "Invalid";

                    //Insert구문에서 자동으로 Update로 변환되므로 무조건 Insert로 입력
                    newRow["_STATE_"] = "added";
                    template.Rows.Add(newRow);
                }
            }
            return template;
        }

        #region IsUniqQCGrade - 조치유형이 AQL인 경우 판정등급이 둘이상 존재하는지 검사
        public bool IsUniqQCGrade(DataTable originTable)
        {
            int sameCount = 0;
            foreach (DataRow originRow in originTable.Rows)
            {
                if (originRow.GetString("QCDECISIONTYPE").Equals("AQL"))
                    foreach (DataRow compareRow in originTable.Rows)
                    {
                        if (compareRow.GetString("DECISIONDEGREE").Equals(originRow.GetString("DECISIONDEGREE")) && compareRow.GetString("QCDECISIONTYPE").Equals("AQL"))
                        {
                            sameCount++;
                        }
                    }

                if (sameCount > 1)
                    return false;

                sameCount = 0;
            }
            return true;
        }
        #endregion
        #endregion
    }
}
