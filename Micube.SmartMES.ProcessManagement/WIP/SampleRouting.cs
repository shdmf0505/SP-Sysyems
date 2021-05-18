#region using
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons;
using Micube.Framework.SmartControls.Grid.BandedGrid;
using Micube.Framework.SmartControls.Grid;

using System;
using Micube.SmartMES.Commons.Controls;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Controls;
#endregion

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 공정관리 > Lot관리 > Sample Routing
    /// 업  무  설  명  : Sample Routing
    /// 생    성    자  : 황유성
    /// 생    성    일  : 2019-11-18
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class SampleRouting : SmartConditionManualBaseForm
    {
        #region ◆ Local Variables |
        private int currentUserSequence;    // LOT의 현재 진행중인 공정의 UserSequence
        private readonly int MAX_PATHSEQUENCE = 99999;
        #endregion

        #region ◆ 생성자 |
        /// <summary>
        /// 생성자
        /// </summary>
        public SampleRouting()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Form Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Form_Load(object sender, EventArgs e)
        {
            InitializeGrid();
            InitializeControls();
        }
        #endregion

        #region ◆ 컨텐츠 영역 초기화 |
        /// <summary>
        /// Control 설정
        /// </summary>
        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeEvent();
        }

        #region ▶ 조회조건 설정 |
        /// <summary>
        /// 검색조건 설정
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // Lot
            CommonFunction.AddConditionSampleLotPopup("P_LOTID", 0.1, false, Conditions);
        }

        /// <summary>
        /// 조회조건 설정
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();
        }
        #endregion

        #region ▶ Grid 설정 |
        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            #region 라우팅 그리드 초기화
            grdRouting.GridButtonItem = GridButtonItem.Delete;
            grdRouting.View.AddComboBoxColumn("PLANTID", 60             // SITE 코드
                , new SqlQuery("GetPlantList", "10002", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PLANTNAME", "PLANTID")
                .SetDefault(UserInfo.Current.Plant)
                .SetTextAlignment(TextAlignment.Center);
            grdRouting.View.AddSpinEditColumn("USERSEQUENCE", 70)       // 공정순서
                .SetTextAlignment(TextAlignment.Center);
            grdRouting.View.AddTextBoxColumn("PROCESSSEGMENTID", 80)    // 공정 ID
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();
            grdRouting.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150) // 공정명
                .SetIsReadOnly();
            grdRouting.View.AddComboBoxColumn("PROCESSUOM", 70          // 공정UOM
                , new SqlQuery("GetUomDefinitionList", "10001", "UOMCLASSID=Segment"), "UOMDEFNAME", "UOMDEFID")
                .SetTextAlignment(TextAlignment.Center)
                .SetEmptyItem(string.Empty, string.Empty, true)
                .SetValidationIsRequired();
            grdRouting.View.AddComboBoxColumn("ISWEEKMNG", 70           // 주차 관리여부
                , new SqlQuery("GetCodeList", "00001", "codeClassId=YesNo"
                , $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetTextAlignment(TextAlignment.Center)
                .SetDefault("N")
                .SetValidationIsRequired();
            grdRouting.View.AddTextBoxColumn("PROCESSSEGMENTVERSION")   // 공정버전
                .SetIsHidden();
            grdRouting.View.AddTextBoxColumn("PROCESSTYPE")             // 공정진행여부(PAST, CURRENT, FUTURE). FUTURE 만 수정가능하다
                .SetIsHidden();
            grdRouting.View.AddTextBoxColumn("ENTERPRISEID")            // 회사 코드
                .SetIsHidden();
            grdRouting.View.AddTextBoxColumn("ISFINALTEST")             // 최종검사 여부(공정진행 여부가 PAST 여도 자원 추가 가능)
                .SetIsHidden();
            grdRouting.View.AddTextBoxColumn("DESCRIPTION", 400);             // 특이사항
            grdRouting.View.SetSortOrder("USERSEQUENCE", DevExpress.Data.ColumnSortOrder.Ascending);    // 공정순서대로 정렬하여 화면에 표시
            grdRouting.View.PopulateColumns();
            #endregion

            #region 자원 그리드 초기화
            grdResource.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete;
            grdResource.View.AddTextBoxColumn("USERSEQUENCE", 85)           // 공정순서
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdResource.View.AddTextBoxColumn("PROCESSSEGMENTID", 80)       // 공정 ID
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdResource.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150)    // 공정명
                .SetIsReadOnly();
            InitializeResourceIdPopup();                                    // 자원 ID
            grdResource.View.AddTextBoxColumn("AREANAME", 170)              // 작업장명
                .SetIsReadOnly();
            grdResource.View.AddTextBoxColumn("EQUIPMENTCLASSNAME", 140)    // 설비그룹명
                .SetIsReadOnly();
            grdResource.View.AddComboBoxColumn("ISPRIMARYRESOURCE", 85      // 주자원여부(Y, N)
                , new SqlQuery("GetCodeList", "00001", "codeClassId=YesNo"
                , $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Center);
            grdResource.View.AddTextBoxColumn("RESOURCEVERSION")            // 자원 버전
                .SetIsHidden();
            grdResource.View.AddTextBoxColumn("AREAID")                     // 작업장 ID
                .SetIsHidden();
            grdResource.View.AddTextBoxColumn("EQUIPMENTCLASSID")           // 설비그룹 ID
                .SetIsHidden();
            grdResource.View.AddTextBoxColumn("PROCESSSEGMENTVERSION")      // 공정 버전
                .SetIsHidden();
            grdResource.View.AddTextBoxColumn("PROCESSTYPE")                // 공정진행여부(PAST, CURRENT, FUTURE). FUTURE 만 수정가능하다
                .SetIsHidden();
            grdResource.View.AddTextBoxColumn("ENTERPRISEID")               // 회사 코드
                .SetIsHidden();
            grdResource.View.AddTextBoxColumn("PLANTID")                    // SITE 코드
                .SetIsHidden();
            grdResource.View.AddTextBoxColumn("ISFINALTEST")                // 최종검사 여부
                .SetIsHidden();
            grdResource.View.SetSortOrder("USERSEQUENCE", DevExpress.Data.ColumnSortOrder.Ascending);   // 공정순서대로 정렬하여 화면에 표시
            grdResource.View.PopulateColumns();
            #endregion

            #region 자재 그리드 초기화
            grdMaterial.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete;
            grdMaterial.View.AddTextBoxColumn("USERSEQUENCE", 85)           // 공정 순서
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();
            grdMaterial.View.AddTextBoxColumn("PROCESSSEGMENTID", 80)       // 공정 ID
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();
            grdMaterial.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150)    // 공정명
                .SetIsReadOnly();
            InitializeMaterialDefIdPopup();                                 // 자재 ID
            grdMaterial.View.AddTextBoxColumn("MATERIALDEFVERSION", 65)     // 자재 버전
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();
            grdMaterial.View.AddTextBoxColumn("CONSUMABLEDEFNAME", 220)     // 자재명
                .SetIsReadOnly();
            grdMaterial.View.AddTextBoxColumn("QTY", 100)                   // BOM 수량
                .SetDisplayFormat("#,##0.################")
                .SetTextAlignment(TextAlignment.Right)
                .SetValidationIsRequired();
            grdMaterial.View.AddComboBoxColumn("UNIT", 85                   // BOM 수량 단위
                , new SqlQuery("GetUomDefinitionList", "10001", "UOMCLASSID=Segment"), "UOMDEFNAME", "UOMDEFID")
                .SetTextAlignment(TextAlignment.Center)
                .SetEmptyItem(string.Empty, string.Empty, true)
                .SetValidationIsRequired();
            grdMaterial.View.AddComboBoxColumn("ISREQUIRED", 80             // 필수투입여부
                , new SqlQuery("GetCodeList", "00001", "codeClassId=YesNo"
                , $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetTextAlignment(TextAlignment.Center)
                .SetEmptyItem(string.Empty, string.Empty, true)
                .SetValidationIsRequired();
            grdMaterial.View.AddComboBoxColumn("WIPSUPPLYTYPE", 110         // 공급 구분
                , new SqlQuery("GetCodeList", "00001", "codeClassId=WipSupplyType"
                , $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetTextAlignment(TextAlignment.Center)
                .SetEmptyItem(string.Empty, string.Empty, true)
                .SetValidationIsRequired();
            grdMaterial.View.AddTextBoxColumn("MATERIALTYPE")               // 원자재/반제품 구분(Consumable, Product)
                .SetIsHidden();
            grdMaterial.View.AddTextBoxColumn("PROCESSSEGMENTVERSION")      // 공정 버전
                .SetIsHidden();
            grdMaterial.View.AddTextBoxColumn("PROCESSTYPE")                // 공정진행여부(PAST, CURRENT, FUTURE). FUTURE 만 수정가능하다
                .SetIsHidden();
            grdMaterial.View.AddTextBoxColumn("ENTERPRISEID")               // 회사 코드
                .SetIsHidden();
            grdMaterial.View.AddTextBoxColumn("PLANTID")                    // SITE 코드
                .SetIsHidden();
            grdMaterial.View.SetSortOrder("USERSEQUENCE", DevExpress.Data.ColumnSortOrder.Ascending);   // 공정순서대로 정렬하여 화면에 표시
            grdMaterial.View.PopulateColumns();
            #endregion

            #region 치공구 그리드 초기화
            grdDurable.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete;
            grdDurable.View.AddTextBoxColumn("USERSEQUENCE", 85)           // 공정순서
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdDurable.View.AddTextBoxColumn("PROCESSSEGMENTID", 80)       // 공정 ID
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdDurable.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150)    // 공정명
                .SetIsReadOnly();
            grdDurable.View.AddComboBoxColumn("DURABLETYPE", 80            // 치공구유형(Tool, Film)
                , new SqlQuery("GetCodeList", "00001", "codeClassId=DurableClassType"
                , $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetTextAlignment(TextAlignment.Center)
                .SetValidationIsRequired()
                .SetEmptyItem(string.Empty, string.Empty, true);
            InitializeDurableIdPopup();                                    // 치공구 ID
            grdDurable.View.AddTextBoxColumn("DURABLEDEFVERSION", 80)      // 치공구 버전
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();
            grdDurable.View.AddTextBoxColumn("DURABLEDEFNAME", 190)        // 치공구명
                .SetIsReadOnly();
            grdDurable.View.AddComboBoxColumn("ISPRIMARYRESOURCE", 85      // 주자원여부(Y, N)
                , new SqlQuery("GetCodeList", "00001", "codeClassId=YesNo"
                , $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Center);
            grdDurable.View.AddTextBoxColumn("PROCESSSEGMENTVERSION")      // 공정 버전
                .SetIsHidden();
            grdDurable.View.AddTextBoxColumn("PROCESSTYPE")                // 공정진행여부(PAST, CURRENT, FUTURE). FUTURE 만 수정가능하다
                .SetIsHidden();
            grdDurable.View.AddTextBoxColumn("ENTERPRISEID")               // 회사 코드
                .SetIsHidden();
            grdDurable.View.AddTextBoxColumn("PLANTID")                    // SITE 코드
                .SetIsHidden();
            grdDurable.View.SetSortOrder("USERSEQUENCE", DevExpress.Data.ColumnSortOrder.Ascending);   // 공정순서대로 정렬하여 화면에 표시
            grdDurable.View.PopulateColumns();
            #endregion
        }

        /// <summary>
        /// 자원ID SelectPopup 컬럼 추가
        /// </summary>
        private void InitializeResourceIdPopup()
        {
            grdResource.View.AddSelectPopupColumn("RESOURCEID", 125, new SampleRoutingResourcePopup())
                .SetPopupCustomParameter(
                    (ISmartCustomPopup sender, DataRow currentRow) => //초기화 작업
                        {
                            SampleRoutingResourcePopup popup = sender as SampleRoutingResourcePopup;
                            popup.EnterpriseId = currentRow["ENTERPRISEID"].ToString();
                            popup.PlantId = currentRow["PLANTID"].ToString();
                            popup.ProcessSegmentId = currentRow["PROCESSSEGMENTID"].ToString();
                        },
                    (ISmartCustomPopup sender, DataRow currentRow) => //결과 리턴 작업
                        {
                            SampleRoutingResourcePopup popup = sender as SampleRoutingResourcePopup;
                            currentRow["RESOURCEID"] = popup.CurrentDataRow["RESOURCEID"];
                            currentRow["RESOURCEVERSION"] = popup.CurrentDataRow["RESOURCEVERSION"];
                            currentRow["AREAID"] = popup.CurrentDataRow["AREAID"];
                            currentRow["AREANAME"] = popup.CurrentDataRow["AREANAME"];
                            currentRow["EQUIPMENTCLASSID"] = popup.CurrentDataRow["EQUIPMENTCLASSID"];
                            currentRow["EQUIPMENTCLASSNAME"] = popup.CurrentDataRow["EQUIPMENTCLASSNAME"];
                        }
                )
            //조건에 따라 팝업을 띄우지 않을수 있습니다.
            .SetPopupQueryPopup((DataRow currentrow) =>
                {
                    return true;        // 팝업을 띄웁니다.
                })
            .SetValidationIsRequired();
        }

        /// <summary>
        /// 치공구ID SelectPopup 컬럼 추가  
        /// </summary>
        private void InitializeDurableIdPopup()
        {
            grdDurable.View.AddSelectPopupColumn("DURABLEDEFID", 125, new SampleRoutingDurablePopup())
                .SetPopupCustomParameter(
                    (ISmartCustomPopup sender, DataRow currentRow) => //초기화 작업
                    {
                        SampleRoutingDurablePopup popup = sender as SampleRoutingDurablePopup;
                        popup.EnterpriseId = currentRow["ENTERPRISEID"].ToString();
                        popup.PlantId = currentRow["PLANTID"].ToString();
                        popup.DurableType = currentRow["DURABLETYPE"].ToString();
                    },
                    (ISmartCustomPopup sender, DataRow currentRow) => //결과 리턴 작업
                    {
                        SampleRoutingDurablePopup popup = sender as SampleRoutingDurablePopup;
                        currentRow["DURABLEDEFID"] = popup.CurrentDataRow["DURABLEDEFID"];
                        currentRow["DURABLEDEFVERSION"] = popup.CurrentDataRow["DURABLEDEFVERSION"];
                        currentRow["DURABLEDEFNAME"] = popup.CurrentDataRow["DURABLEDEFNAME"];
                    }
                )
            //조건에 따라 팝업을 띄우지 않을수 있습니다.
            .SetPopupQueryPopup((DataRow currentrow) =>
            {
                if (currentrow["DURABLETYPE"] == DBNull.Value || string.IsNullOrEmpty((string)currentrow["DURABLETYPE"]))
                {
                    return false;   // 팝업을 띄우지 않음
                }
                return true;        // 팝업을 띄웁니다.
            })
            .SetValidationIsRequired();
        }

        /// <summary>
        /// 자재ID SelectPopup 컬럼 추가
        /// </summary>
        private void InitializeMaterialDefIdPopup()
        {
            var parentItem = this.grdMaterial.View.AddSelectPopupColumn("MATERIALDEFID", 120
                , new SqlQuery("GetBomCompPopup", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}"))
            // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
            .SetPopupLayout("CONSUMABLE", PopupButtonStyles.Ok_Cancel, true, false)
            // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
            .SetPopupResultCount(1)
            // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
            .SetPopupResultMapping("MATERIALDEFID", "ITEMID")
            // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
            .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow)
            // 그리드의 남은 영역을 채울 컬럼 설정
            //.SetPopupAutoFillColumns("CODECLASSNAME")
            // Validation 이 필요한 경우 호출할 Method 지정
            .SetValidationIsRequired()
            .SetPopupValidationCustom(ValidationItemMasterPopup);

            // 팝업에서 사용할 조회조건 항목 추가
            parentItem.Conditions.AddTextBox("ITEMID");
            parentItem.Conditions.AddTextBox("ITEMVERSION");
            parentItem.Conditions.AddTextBox("ITEMNAME");

            // 팝업 그리드 설정
            parentItem.GridColumns.AddTextBoxColumn("ITEMID", 150);
            parentItem.GridColumns.AddTextBoxColumn("ITEMVERSION", 100);
            parentItem.GridColumns.AddTextBoxColumn("ITEMNAME", 250);
            parentItem.GridColumns.AddComboBoxColumn("UOMDEFID", 80, new SqlQuery("GetUOMList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "UOMDEFNAME", "UOMDEFID");
        }

        /// <summary>
        /// 팝업 컬럼에서 확인 클릭 시 다른 데이터 매핑 (자재)
        /// </summary>
        /// <param name="currentGridRow">화면 grid row</param>
        /// <param name="popupSelections">팝업에서 선택한 row</param>
        /// <returns></returns>
        private Micube.Framework.SmartControls.Validations.ValidationResultCommon ValidationItemMasterPopup(DataRow currentGridRow, IEnumerable<DataRow> popupSelections)
        {
            Micube.Framework.SmartControls.Validations.ValidationResultCommon result = new Framework.SmartControls.Validations.ValidationResultCommon();

            foreach (DataRow row in popupSelections)
            {
                currentGridRow["MATERIALDEFVERSION"] = row["ITEMVERSION"];
                currentGridRow["CONSUMABLEDEFNAME"] = row["ITEMNAME"];
                currentGridRow["UNIT"] = row["UOMDEFID"];
                if (row["MASTERDATACLASSID"].ToString() == "SubAssembly")
                {
                    currentGridRow["MATERIALTYPE"] = "Product";
                }
                else
                {
                    currentGridRow["MATERIALTYPE"] = "Consumable";
                }
            }
            return result;
        }
        #endregion

        #region ▶ 화면 Control 설정 |
        /// <summary>
        /// 화면 Control 설정
        /// </summary>
        private void InitializeControls()
        {
            grdLotInfo.ColumnCount = 5;
            grdLotInfo.LabelWidthWeight = "40%";
            grdLotInfo.SetInvisibleFields("PROCESSPATHID", "PROCESSSEGMENTID", "PROCESSSEGMENTVERSION", "PRODUCTDEFVERSION", "PRODUCTTYPE", "DEFECTUNIT", "LOTTYPE"
                , "PANELPERQTY", "PCSPNL", "PROCESSSEGMENTTYPE", "STEPTYPE", "DURABLEDEFID", "PROCESSSTATE", "ISREWORK", "ISLOCKING", "ISHOLD", "RESOURCEID", "PROUCTIONTYPE");

            ConditionItemSelectPopup processCond = new ConditionItemSelectPopup();
            processCond.SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow);
            processCond.SetPopupLayout("PROCESSSEGMENTID", PopupButtonStyles.Ok_Cancel);
            processCond.Id = "PROCESSSEGMENTID";
            processCond.LabelText = "WAREHOUSEID";
            processCond.SearchQuery = new SqlQuery("GetProcessSegmentExtPupop", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"VALIDSTATE={"Valid"}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}");
            processCond.IsMultiGrid = false;
            processCond.SetPopupResultCount(0);
            processCond.DisplayFieldName = "PROCESSSEGMENTNAME";
            processCond.ValueFieldName = "PROCESSSEGMENTID";
            processCond.LanguageKey = "PROCESSSEGMENTID";
            processCond.Conditions.AddTextBox("PROCESSSEGMENTNAME");
            processCond.Conditions.AddTextBox("PROCESSSEGMENTID");
            processCond.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150);
            processCond.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
            processCond.SetPopupApplySelection((selectedRows, dataGridRow) =>
            {
                // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
                // dataGridRow : 현재 Focus가 있는 그리드의 DataRow

                if (grdRouting.View.FocusedRowHandle < 0)
                {
                    selProcessSegment.Editor.ClearValue();
                    return;
                }

                DataTable dt = grdRouting.DataSource as DataTable;
                int rowIndex = grdRouting.View.GetDataSourceRowIndex(grdRouting.View.FocusedRowHandle);
                if (dt.Rows[rowIndex]["PROCESSTYPE"].ToString() == "PAST")
                {
                    selProcessSegment.Editor.ClearValue();
                    return;
                }

                ValidateDuplicatedProcess(dt, selectedRows);

                DataRow rowToFocus = null;
                foreach (DataRow each in selectedRows.Reverse<DataRow>())
                {
                    DataRow newRow = dt.NewRow();
                    newRow["ENTERPRISEID"] = dt.Rows[rowIndex]["ENTERPRISEID"];
                    newRow["PLANTID"] = dt.Rows[rowIndex]["PLANTID"];
                    newRow["PROCESSSEGMENTID"] = each["PROCESSSEGMENTID"];
                    newRow["PROCESSSEGMENTVERSION"] = "*";
                    newRow["PROCESSSEGMENTNAME"] = each["PROCESSSEGMENTNAME"];
                    newRow["USERSEQUENCE"] = int.Parse(dt.Rows[rowIndex]["USERSEQUENCE"].ToString()) + 5;
                    newRow["OLDUSERSEQUENCE"] = MAX_PATHSEQUENCE;
                    newRow["PROCESSTYPE"] = "FUTURE";
                    newRow["PROCESSUOM"] = null;
                    dt.Rows.Add(newRow);
                    if (rowToFocus == null)
                    {
                        rowToFocus = newRow;
                    }
                    grdRouting.View.RaiseValidateRow(grdRouting.View.GetRowHandle(dt.Rows.IndexOf(newRow)));
                }
                grdRouting.View.BeginSort();
                grdRouting.View.EndSort();

                // 추가된 공정으로 포커스 및 스크롤 이동
                if (rowToFocus != null)
                {
                    int dataSourceIndex = dt.Rows.IndexOf(rowToFocus);
                    int rowHandle = grdRouting.View.GetRowHandle(dataSourceIndex);
                    grdRouting.View.FocusedRowHandle = rowHandle;
                    grdRouting.View.MakeRowVisible(rowHandle);
                }
            });

            selProcessSegment.Editor.SelectPopupCondition = processCond;
        }

        /// <summary>
        /// 중복공정 등록 방지
        /// </summary>
        /// <param name="currentProcess"></param>
        /// <param name="newProcess"></param>
        private void ValidateDuplicatedProcess(DataTable currentProcess, IEnumerable<DataRow> newProcess)
        {
            foreach (DataRow each in newProcess)
            {
                foreach (DataRow other in newProcess)
                {
                    if (each != other && each["PROCESSSEGMENTID"].ToString() == other["PROCESSSEGMENTID"].ToString())
                    {
                        // 중복된 공정을 등록할 수 없습니다. {0}
                        throw MessageException.Create("ExistsDuplicateProcess"
                            , string.Format("{0}({1})", each["PROCESSSEGMENTNAME"].ToString(), each["PROCESSSEGMENTID"].ToString()));
                    }
                }
            }

            foreach (DataRow each in currentProcess.Rows)
            {
                foreach (DataRow other in newProcess)
                {
                    if (each != other && each["PROCESSSEGMENTID"].ToString() == other["PROCESSSEGMENTID"].ToString())
                    {
                        // 중복된 공정을 등록할 수 없습니다. {0}
                        throw MessageException.Create("ExistsDuplicateProcess"
                            , string.Format("{0}({1})", each["PROCESSSEGMENTNAME"].ToString(), each["PROCESSSEGMENTID"].ToString()));
                    }
                }
            }
        }
        #endregion

        #endregion

        #region ◆ Event |

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            this.Load += Form_Load;
            //Conditions.GetControl<SmartSelectPopupEdit>("P_LOTID").EditValueChanged += SampleRouting_EditValueChanged;
            grdRouting.View.RowStyle += View_RowStyle;
            grdRouting.View.ShowingEditor += View_ShowingEditor;
            grdRouting.View.CellValueChanged += grdRoutingView_CellValueChanged;
            grdRouting.View.ValidatingEditor += grdRoutingView_ValidatingEditor;
            grdRouting.View.InvalidValueException += grdRoutingView_InvalidValueException;
            grdRouting.ToolbarDeletingRow += GrdRouting_ToolbarDeletingRow;

            grdResource.View.RowStyle += View_RowStyle;
            grdResource.View.AddingNewRow += grdResourceView_AddingNewRow;
            grdResource.View.ShowingEditor += View_ShowingEditor;
            grdResource.View.CellValueChanged += grdResourceView_CellValueChanged;
            grdResource.ToolbarDeletingRow += GrdResource_ToolbarDeletingRow;
            grdResource.ToolbarAddingRow += GrdResource_ToolbarAddingRow;


            grdMaterial.View.RowStyle += View_RowStyle;
            grdMaterial.View.AddingNewRow += grdMaterialView_AddingNewRow;
            grdMaterial.View.ShowingEditor += View_ShowingEditor;
            grdMaterial.ToolbarDeletingRow += GrdMaterial_ToolbarDeletingRow;
            grdMaterial.ToolbarAddingRow += GrdResource_ToolbarAddingRow;

            grdDurable.View.RowStyle += View_RowStyle;
            grdDurable.View.AddingNewRow += grdDurableView_AddingNewRow;
            grdDurable.View.ShowingEditor += View_ShowingEditor;
            grdDurable.View.CellValueChanged += grdDurableView_CellValueChanged;
            grdDurable.ToolbarDeletingRow += GrdDurable_ToolbarDeletingRow;
            grdDurable.ToolbarAddingRow += GrdResource_ToolbarAddingRow;
        }

        private void GrdResource_ToolbarAddingRow(object sender, CancelEventArgs e)
        {
            CheckEnableSave(sender,e);
        }

        /// <summary>
        /// 라우팅그리드에서 USERSEQUENCE 변경시 USERSEQUENCE 순으로 다시 정렬
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdRoutingView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "USERSEQUENCE")
            {
                grdRouting.View.BeginSort();
                grdRouting.View.EndSort();
            }
        }

        private void grdResourceView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "ISPRIMARYRESOURCE" && e.Value.ToString() == "Y")    // 동일 공정/자원유형/치공구유형 내에서 주자원여부 "Y" 는 한개만 가능
            {
                int index = grdResource.View.GetDataSourceRowIndex(e.RowHandle);
                DataTable dataTable = grdResource.DataSource as DataTable;

                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    if (i == index)
                    {
                        continue;
                    }
                    if (dataTable.Rows[i]["PROCESSSEGMENTID"].ToString() == dataTable.Rows[index]["PROCESSSEGMENTID"].ToString()
                        && dataTable.Rows[i]["ISPRIMARYRESOURCE"].ToString() == "Y")
                    {
                        dataTable.Rows[i]["ISPRIMARYRESOURCE"] = "N";
                    }
                }
            }
        }

        private void grdDurableView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "DURABLETYPE")   // 치공구 유형 변경시 데이터 클리어
            {
                grdDurable.View.SetRowCellValue(e.RowHandle, "DURABLEDEFID", string.Empty);
                grdDurable.View.SetRowCellValue(e.RowHandle, "DURABLEDEFVERSION", string.Empty);
                grdDurable.View.SetRowCellValue(e.RowHandle, "DURABLEDEFNAME", string.Empty);
            }
            else if (e.Column.FieldName == "ISPRIMARYRESOURCE" && e.Value.ToString() == "Y")    // 동일 공정/자원유형/치공구유형 내에서 주자원여부 "Y" 는 한개만 가능
            {
                int index = grdDurable.View.GetDataSourceRowIndex(e.RowHandle);
                DataTable dataTable = grdDurable.DataSource as DataTable;

                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    if (i == index)
                    {
                        continue;
                    }
                    if (dataTable.Rows[i]["PROCESSSEGMENTID"].ToString() == dataTable.Rows[index]["PROCESSSEGMENTID"].ToString()
                        && dataTable.Rows[i]["DURABLETYPE"].ToString() == dataTable.Rows[index]["DURABLETYPE"].ToString()
                        && dataTable.Rows[i]["ISPRIMARYRESOURCE"].ToString() == "Y")
                    {
                        dataTable.Rows[i]["ISPRIMARYRESOURCE"] = "N";
                    }
                }
            }
        }

        /// <summary>
        /// 현재공정, 과거공정, 미래공정 여부에 따라 그리드에서 ROW의 배경 색상 다르게 표시
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                return;
            }
            SmartBandedGridView view = sender as SmartBandedGridView;
            switch (view.GetRowCellValue(e.RowHandle, "PROCESSTYPE").ToString())
            {
                case "PAST":
                    e.Appearance.BackColor = Color.Gray;
                    e.HighPriority = true;
                    break;
                case "CURRENT":
                    e.Appearance.BackColor = Color.Yellow;
                    e.HighPriority = true;
                    break;
            }

            if (view == grdRouting.View && view.GetRowCellValue(e.RowHandle, "ISFINALTEST").ToString() == "Y")
            {
                e.Appearance.BackColor = Color.LightGreen;
                e.HighPriority = true;
            }
        }

        /// <summary>
        /// 자원 추가시 라우팅에서 선택된 공정에 맵핑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void grdResourceView_AddingNewRow(SmartBandedGridView sender, AddNewRowArgs args)
        {
            DataRow routingRow = grdRouting.View.GetFocusedDataRow();
            if (UserInfo.Current.Id.ToString() != "sybae" && UserInfo.Current.Id.ToString() != "hyochul.an")
            {
                if (routingRow == null || (routingRow["PROCESSTYPE"].ToString() == "PAST" && routingRow["ISFINALTEST"].ToString() != "Y"))
                {
                    args.IsCancel = true;
                    return;
                }
            }
            args.NewRow["USERSEQUENCE"] = routingRow["USERSEQUENCE"];
            args.NewRow["PROCESSSEGMENTID"] = routingRow["PROCESSSEGMENTID"];
            args.NewRow["PROCESSSEGMENTNAME"] = routingRow["PROCESSSEGMENTNAME"];
            args.NewRow["PROCESSSEGMENTVERSION"] = routingRow["PROCESSSEGMENTVERSION"];
            args.NewRow["PROCESSTYPE"] = routingRow["PROCESSTYPE"];
            args.NewRow["ENTERPRISEID"] = routingRow["ENTERPRISEID"];
            args.NewRow["PLANTID"] = routingRow["PLANTID"];
            args.NewRow["ISFINALTEST"] = routingRow["ISFINALTEST"];

            grdResource.View.BeginSort();
            grdResource.View.EndSort();

            // 추가된 자원으로 포커스 및 스크롤 이동
            int dataSourceIndex = (grdResource.DataSource as DataTable).Rows.IndexOf(args.NewRow);
            int rowHandle = grdResource.View.GetRowHandle(dataSourceIndex);
            grdResource.View.FocusedRowHandle = rowHandle;
            grdResource.View.MakeRowVisible(rowHandle);
        }

        /// <summary>
        /// 자재 추가시 라우팅에서 선택된 공정에 맵핑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void grdMaterialView_AddingNewRow(SmartBandedGridView sender, AddNewRowArgs args)
        {
            DataRow routingRow = grdRouting.View.GetFocusedDataRow();
            if (routingRow == null || routingRow["PROCESSTYPE"].ToString() == "PAST")
            {
                args.IsCancel = true;
                return;
            }
            args.NewRow["USERSEQUENCE"] = routingRow["USERSEQUENCE"];
            args.NewRow["PROCESSSEGMENTID"] = routingRow["PROCESSSEGMENTID"];
            args.NewRow["PROCESSSEGMENTNAME"] = routingRow["PROCESSSEGMENTNAME"];
            args.NewRow["PROCESSSEGMENTVERSION"] = routingRow["PROCESSSEGMENTVERSION"];
            args.NewRow["PROCESSTYPE"] = routingRow["PROCESSTYPE"];
            args.NewRow["ENTERPRISEID"] = routingRow["ENTERPRISEID"];
            args.NewRow["PLANTID"] = routingRow["PLANTID"];

            grdMaterial.View.BeginSort();
            grdMaterial.View.EndSort();

            // 추가된 자원으로 포커스 및 스크롤 이동
            int dataSourceIndex = (grdMaterial.DataSource as DataTable).Rows.IndexOf(args.NewRow);
            int rowHandle = grdMaterial.View.GetRowHandle(dataSourceIndex);
            grdMaterial.View.FocusedRowHandle = rowHandle;
            grdMaterial.View.MakeRowVisible(rowHandle);
        }

        private void grdDurableView_AddingNewRow(SmartBandedGridView sender, AddNewRowArgs args)
        {
            DataRow routingRow = grdRouting.View.GetFocusedDataRow();
            if (routingRow == null/* || routingRow["PROCESSTYPE"].ToString() == "PAST"*/)
            {
                args.IsCancel = true;
                return;
            }
            args.NewRow["USERSEQUENCE"] = routingRow["USERSEQUENCE"];
            args.NewRow["PROCESSSEGMENTID"] = routingRow["PROCESSSEGMENTID"];
            args.NewRow["PROCESSSEGMENTNAME"] = routingRow["PROCESSSEGMENTNAME"];
            args.NewRow["PROCESSSEGMENTVERSION"] = routingRow["PROCESSSEGMENTVERSION"];
            args.NewRow["PROCESSTYPE"] = routingRow["PROCESSTYPE"];
            args.NewRow["ENTERPRISEID"] = routingRow["ENTERPRISEID"];
            args.NewRow["PLANTID"] = routingRow["PLANTID"];

            grdDurable.View.BeginSort();
            grdDurable.View.EndSort();

            // 추가된 자원으로 포커스 및 스크롤 이동
            int dataSourceIndex = (grdDurable.DataSource as DataTable).Rows.IndexOf(args.NewRow);
            int rowHandle = grdDurable.View.GetRowHandle(dataSourceIndex);
            grdDurable.View.FocusedRowHandle = rowHandle;
            grdDurable.View.MakeRowVisible(rowHandle);
        }

        /// <summary>
        /// 라우팅, 자원, 자재 그리드에서 현재/과거 공정 데이터 수정 금지
        /// 자원그리드에서 자원유형=자원 일때 치공구 유형 변경불가
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_ShowingEditor(object sender, CancelEventArgs e)
        {
            SmartBandedGridView gridView = sender as SmartBandedGridView;
            DataRow row = gridView.GetFocusedDataRow();
            if (gridView == grdResource.View)
            {
                if (row["PROCESSTYPE"].ToString() == "PAST" && row["ISFINALTEST"].ToString() != "Y")
                {
                    e.Cancel = true;
                    return;
                }
            }
            else if (gridView == grdRouting.View)
            {
                if (row["PROCESSTYPE"].ToString() != "FUTURE" && gridView.FocusedColumn.FieldName.Equals("USERSEQUENCE"))
                {
                    e.Cancel = true;
                    return;
                }
            }
            else
            {
                //if (row["PROCESSTYPE"].ToString() == "PAST")
                //{
                //    e.Cancel = true;
                //    return;
                //}
            }
            if (gridView == grdResource.View && gridView.FocusedColumn.FieldName.Equals("DURABLETYPE") && row["RESOURCETYPE"].ToString() == "Resource")
            {
                e.Cancel = true;
                return;
            }
        }

        /// <summary>
        /// 라우팅에서 현재/과거 공정보다 같거나 작은 UserSequence 등록 불가
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdRoutingView_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            ColumnView view = sender as ColumnView;
            GridColumn column = (e as EditFormValidateEditorEventArgs)?.Column ?? view.FocusedColumn;
            if (column.FieldName != "USERSEQUENCE")
            {
                return;
            }
            if (Convert.ToInt32(e.Value) <= currentUserSequence)
            {
                e.Valid = false;
            }
        }

        /// <summary>
        /// 라우팅에서 현재/과거 공정보다 같거나 작은 UserSequence 등록 불가 (InvalidValueException 처리)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdRoutingView_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            ColumnView view = sender as ColumnView;
            if (view == null)
            {
                return;
            }
            e.ExceptionMode = ExceptionMode.Ignore;
        }

        private void GrdRouting_ToolbarDeletingRow(object sender, CancelEventArgs e)
        {
            DataRow routingRow = grdRouting.View.GetFocusedDataRow();
            if (routingRow == null || routingRow["PROCESSTYPE"].ToString() != "FUTURE")
            {
                e.Cancel = true;
            }
        }

        private void GrdResource_ToolbarDeletingRow(object sender, CancelEventArgs e)
        {
            DataRow resourceRow = grdResource.View.GetFocusedDataRow();
            //2020-03-06 강유라 현재공정중 인수/인수대시 생태에서는 삭제가능
            if (resourceRow == null || resourceRow["PROCESSTYPE"].ToString() == "PAST" ||
                resourceRow["PROCESSTYPE"].ToString() == "CURRENT"
                &&  (resourceRow["PROCESSSTATE"].ToString() != "Wait"
                && resourceRow["PROCESSSTATE"].ToString() != "WaitForReceive"))
            {
                e.Cancel = true;
            }
        }

        private void GrdMaterial_ToolbarDeletingRow(object sender, CancelEventArgs e)
        {
            DataRow materialRow = grdMaterial.View.GetFocusedDataRow();
            //2020-03-06 강유라 현재공정중 인수/인수대시 생태에서는 삭제가능
            if (materialRow == null || materialRow["PROCESSTYPE"].ToString() == "PAST" ||
                materialRow["PROCESSTYPE"].ToString() == "CURRENT"
            && (materialRow["PROCESSSTATE"].ToString() != "Wait"
            && materialRow["PROCESSSTATE"].ToString() != "WaitForReceive"))
                    {
                e.Cancel = true;
            }
        }

        private void GrdDurable_ToolbarDeletingRow(object sender, CancelEventArgs e)
        {
            DataRow durableRow = grdDurable.View.GetFocusedDataRow();
            //2020-03-06 강유라 현재공정중 인수/인수대시 생태에서는 삭제가능
            if (durableRow == null /*|| durableRow["PROCESSTYPE"].ToString() == "PAST"*/ ||
                     durableRow["PROCESSTYPE"].ToString() == "CURRENT"
                 && (durableRow["PROCESSSTATE"].ToString() != "Wait"
                 && durableRow["PROCESSSTATE"].ToString() != "WaitForReceive"))
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// LOT ID 선택 시 자동조회
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void SampleRouting_EditValueChanged(object sender, EventArgs e)
        {
            await this.OnSearchAsync();
        }

        #region ▶ Grid Event |

        #endregion

        #endregion

        #region ◆ 툴바 |

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            SaveRule();
        }

        #region ▶ 데이터 저장 |
        /// <summary>
        /// 데이터 저장
        /// </summary>
        private void SaveRule()
        {
            DataTable lotInfo = grdLotInfo.DataSource as DataTable;
            if (lotInfo == null || lotInfo.Rows.Count == 0)
            {
                // LOT을 선택하여 주십시오.
                throw MessageException.Create("NoSeletedLot");
            }



            MessageWorker worker = new MessageWorker("SaveSampleRouting");
            worker.SetBody(new MessageBody()
            {
                { "LotId", lotInfo.Rows[0]["LOTID"].ToString() },
                { "ProcessSegmentId", lotInfo.Rows[0]["PROCESSSEGMENTID"].ToString() },
                { "FirstChangedUserSequence", GetFirstChangedUserSequence() },
                { "Routing", GetChangedRoutingToSave() },
                { "Resource", GetChangedResourceToSave() },
                { "Material", GetChangedMaterialToSave() },
                { "Durable", GetChangedDurableToSave() }
            });

            worker.Execute();
            pnlContent.CloseWaitArea();
        }

        // 변경된 공정중 가장 빠른공정 이후의 모든 공정들을 정렬하여 반환한다. (삭제된 공정 제외)
        private DataTable GetChangedRoutingToSave()
        {
            DataTable changedRouting = grdRouting.GetChangedRows();
            if (changedRouting.Rows.Count == 0)
            {
                return new DataTable();
            }
            int firstChangedUserSequence = GetFirstChangedUserSequence();

            // TODO : 소팅안됨
            DataTable routing = grdRouting.DataSource as DataTable;
            DataTable result = routing.Clone();
            foreach (DataRow each in routing.Rows)
            {
                if (Convert.ToInt32(each["USERSEQUENCE"].ToString()) >= firstChangedUserSequence && !IsDeletedProcess(changedRouting, each))
                {
                    result.ImportRow(each);
                }
            }
            return SortDataTable(result, "USERSEQUENCE");
        }

        private DataTable SortDataTable(DataTable dataTable, string orderBy)
        {
            var sorted = dataTable.Rows.Cast<DataRow>()
                .OrderBy(row => int.Parse(row[orderBy].ToString()));
            DataTable result = dataTable.Clone();
            foreach(DataRow each in sorted)
            {
                result.ImportRow(each);
            }
            return result;
        }

        // 삭제된 공정인지 확인
        private bool IsDeletedProcess(DataTable changed, DataRow row)
        {
            foreach (DataRow eachChange in changed.Rows)
            {
                if (eachChange["_STATE_"].ToString() == "deleted"
                    && eachChange["PROCESSSEGMENTID"].ToString() == row["PROCESSSEGMENTID"].ToString()
                    && eachChange["PROCESSSEGMENTVERSION"].ToString() == row["PROCESSSEGMENTVERSION"].ToString())
                {
                    return true;
                }
            }
            return false;
        }

        // 변경된 라우팅중 첫번째 공정의 Usersequence를 반환
        private int GetFirstChangedUserSequence()
        {
            DataTable changedRouting = grdRouting.GetChangedRows();
            if (changedRouting.Rows.Count == 0)
            {
                return MAX_PATHSEQUENCE;
            }
            int min_userSequence = Convert.ToInt32(changedRouting.Compute("MIN([USERSEQUENCE])", string.Empty));
            int min_oldUserSequence = Convert.ToInt32(changedRouting.Compute("MIN([OLDUSERSEQUENCE])", string.Empty));
            return Math.Min(min_userSequence, min_oldUserSequence);
        }

        // 추가/변경된 행과 동일한 공정의 자원들도 추가/변경된 행과 함게 반환한다.
        private DataTable GetChangedResourceToSave()
        {
            DataTable changedResource = grdResource.GetChangedRows();
            if (changedResource.Rows.Count == 0)
            {
                return new DataTable();
            }

            DataTable resource = grdResource.DataSource as DataTable;
            DataTable result = resource.Clone();
            result.Columns.Add("_STATE_");
            foreach (DataRow each in resource.Rows)
            {
                if (IsProcessChanged(changedResource, each))
                {
                    result.ImportRow(each);
                    if (IsDeletedResource(changedResource, each))
                    {
                        result.Rows[result.Rows.Count - 1]["_STATE_"] = "deleted";
                    }
                }
            }
            return result;
        }

        // 자재/자원이 변경된 공정인지 확인
        private bool IsProcessChanged(DataTable changed, DataRow row)
        {
            foreach (DataRow eachChange in changed.Rows)
            {
                if (eachChange["PROCESSSEGMENTID"].ToString() == row["PROCESSSEGMENTID"].ToString()
                    && eachChange["PROCESSSEGMENTVERSION"].ToString() == row["PROCESSSEGMENTVERSION"].ToString())
                {
                    return true;
                }
            }
            return false;
        }

        // 삭제된 자원인지 확인
        private bool IsDeletedResource(DataTable changed, DataRow row)
        {
            foreach (DataRow eachChange in changed.Rows)
            {
                if (eachChange["_STATE_"].ToString() == "deleted"
                    && eachChange["PROCESSSEGMENTID"].ToString() == row["PROCESSSEGMENTID"].ToString()
                    && eachChange["PROCESSSEGMENTVERSION"].ToString() == row["PROCESSSEGMENTVERSION"].ToString()
                    && eachChange["RESOURCEID"].ToString() == row["RESOURCEID"].ToString()
                    && eachChange["RESOURCEVERSION"].ToString() == row["RESOURCEVERSION"].ToString())
                {
                    return true;
                }
            }
            return false;
        }

        // 삭제된 치공구인지 확인
        private bool IsDeletedDurable(DataTable changed, DataRow row)
        {
            foreach (DataRow eachChange in changed.Rows)
            {
                if (eachChange["_STATE_"].ToString() == "deleted"
                    && eachChange["PROCESSSEGMENTID"].ToString() == row["PROCESSSEGMENTID"].ToString()
                    && eachChange["PROCESSSEGMENTVERSION"].ToString() == row["PROCESSSEGMENTVERSION"].ToString()
                    && eachChange["DURABLEDEFID"].ToString() == row["DURABLEDEFID"].ToString()
                    && eachChange["DURABLEDEFVERSION"].ToString() == row["DURABLEDEFVERSION"].ToString())
                {
                    return true;
                }
            }
            return false;
        }

        // 추가/변경된 행과 동일한 공정의 자재들도 추가/변경된 행과 함게 반환한다.
        private DataTable GetChangedMaterialToSave()
        {
            DataTable changedMaterial = grdMaterial.GetChangedRows();
            if (changedMaterial.Rows.Count == 0)
            {
                return new DataTable();
            }

            DataTable material = grdMaterial.DataSource as DataTable;
            DataTable result = material.Clone();
            result.Columns.Add("_STATE_");
            foreach (DataRow each in material.Rows)
            {
                if (IsProcessChanged(changedMaterial, each))
                {
                    result.ImportRow(each);
                    if (IsDeletedMaterial(changedMaterial, each))
                    {
                        result.Rows[result.Rows.Count - 1]["_STATE_"] = "deleted";
                    }
                }
            }
            return result;
        }

        // 추가/변경된 행과 동일한 공정의 치공구들도 추가/변경된 행과 함게 반환한다.
        private DataTable GetChangedDurableToSave()
        {
            DataTable changedDurable = grdDurable.GetChangedRows();
            if (changedDurable.Rows.Count == 0)
            {
                return new DataTable();
            }

            DataTable durable = grdDurable.DataSource as DataTable;
            DataTable result = durable.Clone();
            result.Columns.Add("_STATE_");
            foreach (DataRow each in durable.Rows)
            {
                if (IsProcessChanged(changedDurable, each))
                {
                    result.ImportRow(each);
                    if (IsDeletedDurable(changedDurable, each))
                    {
                        result.Rows[result.Rows.Count - 1]["_STATE_"] = "deleted";
                    }
                }
            }
            return result;
        }

        // 삭제된 자재인지 확인
        private bool IsDeletedMaterial(DataTable changed, DataRow row)
        {
            foreach (DataRow eachChange in changed.Rows)
            {
                if (eachChange["_STATE_"].ToString() == "deleted"
                    && eachChange["PROCESSSEGMENTID"].ToString() == row["PROCESSSEGMENTID"].ToString()
                    && eachChange["PROCESSSEGMENTVERSION"].ToString() == row["PROCESSSEGMENTVERSION"].ToString()
                    && eachChange["MATERIALDEFID"].ToString() == row["MATERIALDEFID"].ToString()
                    && eachChange["MATERIALDEFVERSION"].ToString() == row["MATERIALDEFVERSION"].ToString())
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #endregion

        #region ◆ 검색 |

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("PLANTID", UserInfo.Current.Plant);

            if (string.IsNullOrEmpty(values["P_LOTID"].ToString()))
            {
                return;
            }

            SearchLotInfo(values["P_LOTID"].ToString());

            DataTable dtRouting = await SqlExecuter.QueryAsync("SelectLotRouting", "10001", values);    // 라우팅 그리드 조회
            grdRouting.DataSource = dtRouting;

            this.currentUserSequence = GetCurrentUserSequence(dtRouting);

            DataTable dtResource = await SqlExecuter.QueryAsync("SelectLotRoutingResource", "10001", values);   // 자원 그리드 조회
            grdResource.DataSource = dtResource;

            DataTable dtMaterial = await SqlExecuter.QueryAsync("SelectLotRoutingMaterial", "10001", values);   // 자재 그리드 조회
            grdMaterial.DataSource = dtMaterial;

            DataTable dtDurable = await SqlExecuter.QueryAsync("SelectLotRoutingDurable", "10001", values);     // 치공구 그리드 조회
            grdDurable.DataSource = dtDurable;
        }

        /// <summary>
        /// LOT이 현재 진행중인 공정의 UserSequence를 가져온다.
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        private int GetCurrentUserSequence(DataTable dataTable)
        {
            foreach (DataRow each in dataTable.Rows)
            {
                if (each["PROCESSTYPE"].ToString() == "CURRENT")
                {
                    return int.Parse(each["USERSEQUENCE"].ToString());
                }
            }
            return 0;
        }

        /// <summary>
        /// LOT 정보 조회
        /// </summary>
        private void SearchLotInfo(string inputLotId)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            param.Add("PLANTID", UserInfo.Current.Plant);
            param.Add("LOTID", inputLotId);
            param.Add("ISREWORK", "N");
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable lotInfo = SqlExecuter.Query("SelectLotInfoByLotID", "10004", param);

            grdLotInfo.DataSource = lotInfo;

            if (lotInfo.Rows.Count == 0)
            {
                // 해당 Lot이 존재하지 않습니다. {0}
                this.ShowMessage("NotExistLot", string.Format("LotId = {0}", inputLotId));
                return;
            }

            if (lotInfo.Rows[0]["PRODUCTIONTYPECODE"].ToString() == "Production" && !lotInfo.Rows[0]["DESCRIPTION"].ToString().Equals("MIG")
            //2020-03-06 강유라 임시 수정 스플릿 경우 모LOT의 LOTTYPE 따라가는 문제
                && !Format.GetString(lotInfo.Rows[0]["LOTID"]).Equals(Format.GetString(lotInfo.Rows[0]["PROCESSDEFID"])))
            {
                Conditions.GetControl<SmartSelectPopupEdit>("P_LOTID").ClearValue();
                grdLotInfo.ClearData();
                // 양산 LOT 은 Sample Routing 을 생성할 수 없습니다.
                throw MessageException.Create("LotTypeIsProduction");
            }
        }

        #endregion

        #region ◆ 유효성 검사 |

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();
            this.grdRouting.View.CheckValidation();
            this.grdResource.View.CheckValidation();
            this.grdMaterial.View.CheckValidation();
            this.grdDurable.View.CheckValidation();
            ValidateDuplicatedUserSequence();
            ValidateDuplicatedResource();
            ValidateDuplicatedMaterial();
            ValidateDuplicatedDurable();
        }

        /// <summary>
        /// 중복된 공정순서 등록 방지
        /// </summary>
        private void ValidateDuplicatedUserSequence()
        {
            DataTable routing = grdRouting.DataSource as DataTable;
            if (routing == null)
            {
                return;
            }

            foreach (DataRow each in routing.Rows)
            {
                foreach (DataRow other in routing.Rows)
                {
                    if (each != other && each["USERSEQUENCE"].ToString() == other["USERSEQUENCE"].ToString())
                    {
                        // 중복된 공정순서를 등록할 수 없습니다. {0}
                        throw MessageException.Create("DuplicatedUsersequence", each["USERSEQUENCE"].ToString());
                    }
                }
            }
        }

        /// <summary>
        /// 중복된 자원 등록 방지
        /// </summary>
        private void ValidateDuplicatedResource()
        {
            DataTable resource = grdResource.DataSource as DataTable;
            if (resource == null)
            {
                return;
            }

            foreach (DataRow each in resource.Rows)
            {
                foreach (DataRow other in resource.Rows)
                {
                    if (each != other
                        && each["PROCESSSEGMENTID"].ToString() == other["PROCESSSEGMENTID"].ToString()
                        && each["PROCESSSEGMENTVERSION"].ToString() == other["PROCESSSEGMENTVERSION"].ToString()
                        && each["RESOURCEID"].ToString() == other["RESOURCEID"].ToString()
                        && each["RESOURCEVERSION"].ToString() == other["RESOURCEVERSION"].ToString())
                    {
                        // 중복된 자원을 등록할 수 없습니다. {0}
                        throw MessageException.Create("DuplicatedResource", string.Format("ResourceId = {0}", each["RESOURCEID"].ToString()));
                    }
                }
            }
        }

        /// <summary>
        /// 중복된 자재 등록 방지
        /// </summary>
        private void ValidateDuplicatedMaterial()
        {
            DataTable material = grdMaterial.DataSource as DataTable;
            if (material == null)
            {
                return;
            }

            foreach (DataRow each in material.Rows)
            {
                foreach (DataRow other in material.Rows)
                {
                    if (each != other
                        && each["PROCESSSEGMENTID"].ToString() == other["PROCESSSEGMENTID"].ToString()
                        && each["PROCESSSEGMENTVERSION"].ToString() == other["PROCESSSEGMENTVERSION"].ToString()
                        && each["MATERIALDEFID"].ToString() == other["MATERIALDEFID"].ToString()
                        && each["MATERIALDEFVERSION"].ToString() == other["MATERIALDEFVERSION"].ToString())
                    {
                        // 중복된 자재를 등록할 수 없습니다. {0}
                        throw MessageException.Create("DuplicatedMaterial", string.Format("MaterialDefId = {0}, MaterialDefVersion = {1}"
                            , each["MATERIALDEFID"].ToString(), each["MATERIALDEFVERSION"].ToString()));
                    }
                }
            }
        }

        private void ValidateDuplicatedDurable()
        {
            DataTable durable = grdDurable.DataSource as DataTable;
            if (durable == null)
            {
                return;
            }

            foreach (DataRow each in durable.Rows)
            {
                foreach (DataRow other in durable.Rows)
                {
                    if (each != other
                        && each["PROCESSSEGMENTID"].ToString() == other["PROCESSSEGMENTID"].ToString()
                        && each["PROCESSSEGMENTVERSION"].ToString() == other["PROCESSSEGMENTVERSION"].ToString()
                        && each["DURABLEDEFID"].ToString() == other["DURABLEDEFID"].ToString()
                        && each["DURABLEDEFVERSION"].ToString() == other["DURABLEDEFVERSION"].ToString())
                    {
                        // 중복된 치공구를 등록할 수 없습니다. {0}
                        throw MessageException.Create("DuplicatedDurable", string.Format("DurableDefId = {0}, DurableDefVersion = {1}"
                            , each["DURABLEDEFID"].ToString(), each["DURABLEDEFVERSION"].ToString()));
                    }
                }
            }
        }

        #endregion

        #region ◆ Private Function |

        // TODO : 화면에서 사용할 내부 함수 추가
        /// <summary>
        /// 라우팅그리드에 insertData가 있으면 먼저 저장 하라는 메세지
        /// </summary>
        private void CheckEnableSave(object sender, CancelEventArgs e)
        {
            //라우팅 newData 갯수
            var newRoutingCount = grdRouting.GetChangedRows().AsEnumerable()
                .Where(r => Format.GetString(r["_STATE_"]).Equals("added"))
                .ToList().Count();

            if (newRoutingCount > 0 )
            {
                e.Cancel = true;
                //새로 추가된 라우팅을 먼저 등록 해주세요.
                ShowMessage("NeedToSaveNewRouting");
            }
        }

        #endregion
    }
}
