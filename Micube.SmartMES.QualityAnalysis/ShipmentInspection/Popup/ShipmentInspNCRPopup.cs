using DevExpress.XtraEditors.Repository;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Grid.BandedGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 출하검사 > 출하검사 NCR 발행 팝업
    /// 업  무  설  명  : 출하검사 NCR 발행 팝업
    /// 생    성    자  : 강유라
    /// 생    성    일  : 2019-09-20
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    /// 
    public partial class ShipmentInspNCRPopup : SmartPopupBaseForm, ISmartCustomPopup
    {

        #region 인터페이스
        public DataRow CurrentDataRow { get; set; }
        public bool isEnable = true;
        #endregion

        #region Local Variables
        bool _isQueryInitialize = false;
        private string _lotId;
        #endregion

        #region 생성자
        public ShipmentInspNCRPopup(string lotid)
        {
            InitializeComponent();
            InitializeEvent();

            _lotId = lotid;

            if (UserInfo.Current.Enterprise.Equals("YOUNGPOONG"))
                InitializeGridYoungPoong(lotid);
            else
                InitializeGridInterflex();

                InitializeGrid();
        }
        #endregion

        #region 컨텐츠 영역 초기화

        #region 출하검사 유출공정 그리드

        #region 인터플렉스
        public void InitializeGridInterflex()
        {
            #region 최종검사 NCR 그리드 초기화
            grdFinishIssue.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdFinishIssue.View.AddTextBoxColumn("ABNOCRNO", 150)
                .SetIsHidden();

            grdFinishIssue.View.AddTextBoxColumn("ABNOCRTYPE", 150)
                .SetIsHidden();

            grdFinishIssue.View.AddTextBoxColumn("RESOURCEID", 250)
                .SetLabel("LOTID");

            grdFinishIssue.View.AddTextBoxColumn("DEGREE", 150);

            grdFinishIssue.View.AddTextBoxColumn("DEFECTCODENAME", 150);

            grdFinishIssue.View.AddTextBoxColumn("DEFECTCODE", 150)
                .SetIsHidden();

            grdFinishIssue.View.AddTextBoxColumn("QCSEGMENTNAME", 150);

            grdFinishIssue.View.AddTextBoxColumn("QCSEGMENTID", 150)
                .SetIsHidden();

            grdFinishIssue.View.AddTextBoxColumn("DEFECTQTY", 150);

            grdFinishIssue.View.AddTextBoxColumn("DEFECTRATE", 150);

            grdFinishIssue.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150);

            grdFinishIssue.View.AddTextBoxColumn("PROCESSSEGMENTID", 150)
                .SetIsHidden();

            grdFinishIssue.View.AddTextBoxColumn("AREANAME", 150);

            grdFinishIssue.View.AddTextBoxColumn("AREAID", 150)
                .SetIsHidden();

            grdFinishIssue.View.AddTextBoxColumn("NCRISSUEDATE", 150);

            grdFinishIssue.View.AddTextBoxColumn("ISNCRISSUE", 150);

            //grdFinishIssue.View.AddTextBoxColumn("DESCRIPTION", 150);

            grdFinishIssue.View.PopulateColumns();
            #endregion
        }
        #endregion


        #region 영풍
        public void InitializeGridYoungPoong(string lotid)
        {
            #region 최종검사 NCR 그리드 초기화
            grdFinishIssue.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdFinishIssue.View.AddTextBoxColumn("ABNOCRNO", 150)
                .SetIsHidden();

            grdFinishIssue.View.AddTextBoxColumn("ABNOCRTYPE", 150)
                .SetIsHidden();

            grdFinishIssue.View.AddTextBoxColumn("RESOURCEID", 250)
                .SetLabel("LOTID")
                .SetIsReadOnly();

            grdFinishIssue.View.AddTextBoxColumn("LOTID", 150)
              .SetIsHidden();

            grdFinishIssue.View.AddTextBoxColumn("DEGREE", 150)
                .SetIsReadOnly();

            grdFinishIssue.View.AddTextBoxColumn("DEFECTCODENAME", 150)
                .SetIsReadOnly();

            grdFinishIssue.View.AddTextBoxColumn("DEFECTCODE", 150)
                .SetIsHidden();

            grdFinishIssue.View.AddTextBoxColumn("QCSEGMENTNAME", 150)
                .SetIsReadOnly();

            grdFinishIssue.View.AddTextBoxColumn("QCSEGMENTID", 150)
                .SetIsHidden();

            grdFinishIssue.View.AddTextBoxColumn("DEFECTQTY", 150)
                .SetIsReadOnly();

            grdFinishIssue.View.AddTextBoxColumn("DEFECTRATE", 150)
                .SetIsReadOnly();

            Dictionary<string, object> param = new Dictionary<string, object>()
            {
                { "LANGUAGETYPE",UserInfo.Current.LanguageType},
                { "LOTID", lotid}
            };

            grdFinishIssue.View.AddComboBoxColumn("PROCESSSEGMENTAREA", 200, new SqlQuery("GetResourceListToFinish", "10001", param), "PROCESSSEGMENTNAME", "PROCESSSEGMENTAREA")
                            .SetLabel("PROCESSSEGMENTNAME")
                            .SetMultiColumns(ComboBoxColumnShowType.Custom, true)     
                            .SetPopupWidth(600)
                            .SetVisibleColumns("AREANAME", "RESOURCENAME")
            
                            .SetVisibleColumnsWidth(100, 150);

            grdFinishIssue.View.AddTextBoxColumn("PROCESSSEGMENTID", 150)
                .SetIsHidden();

            grdFinishIssue.View.AddTextBoxColumn("PROCESSSEGMENTVERSION", 150)
                .SetIsHidden();


            grdFinishIssue.View.AddTextBoxColumn("AREANAME", 150)
                .SetIsReadOnly();

            grdFinishIssue.View.AddTextBoxColumn("AREAID", 150)
                .SetIsHidden();

            grdFinishIssue.View.AddTextBoxColumn("NCRISSUEDATE", 150)
                .SetIsReadOnly();

            grdFinishIssue.View.AddTextBoxColumn("ISNCRISSUE", 150)
                .SetIsReadOnly();

            //grdFinishIssue.View.AddTextBoxColumn("DESCRIPTION", 150);

            grdFinishIssue.View.PopulateColumns();
            #endregion
        }
        #endregion
        #endregion

        public void InitializeGrid()
        {

            #region 원인공정 그리드 초기화
            grdCauseProcessSegmentIssue.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdCauseProcessSegmentIssue.View.AddTextBoxColumn("ABNOCRNO", 150)
                .SetIsHidden();

            grdCauseProcessSegmentIssue.View.AddTextBoxColumn("ABNOCRTYPE", 150)
                .SetIsHidden();

            grdCauseProcessSegmentIssue.View.AddTextBoxColumn("RESOURCEID", 250)
                .SetLabel("LOTID")
                .SetIsReadOnly();

            grdCauseProcessSegmentIssue.View.AddTextBoxColumn("LOTID", 250)
                .SetIsHidden();

            grdCauseProcessSegmentIssue.View.AddTextBoxColumn("DEGREE", 150)
                .SetIsReadOnly();

            grdCauseProcessSegmentIssue.View.AddTextBoxColumn("DEFECTCODENAME", 150)
                .SetIsReadOnly();

            grdCauseProcessSegmentIssue.View.AddTextBoxColumn("DEFECTCODE", 150)
                .SetIsHidden();

            grdFinishIssue.View.AddTextBoxColumn("QCSEGMENTNAME", 150);

            grdFinishIssue.View.AddTextBoxColumn("QCSEGMENTID", 150)
                .SetIsHidden();

            grdCauseProcessSegmentIssue.View.AddTextBoxColumn("DEFECTQTY", 150)
                .SetIsReadOnly();

            grdCauseProcessSegmentIssue.View.AddTextBoxColumn("DEFECTRATE", 150)
                .SetIsReadOnly();

            // 원인품목
            grdCauseProcessSegmentIssue.View.AddComboBoxColumn("REASONCONSUMABLEDEFIDVERSION", 200, new SqlQuery("GetReasonConsumableList", "10002"), "CONSUMABLEDEFNAME", "SPLITCONSUMABLEDEFIDVERSION")
                            .SetLabel("REASONCONSUMABLEDEFID")
                            .SetMultiColumns(ComboBoxColumnShowType.Custom, true)
                            .SetRelationIds("LOTID")
                            .SetPopupWidth(600)
                            .SetVisibleColumns("CONSUMABLEDEFID", "CONSUMABLEDEFVERSION", "CONSUMABLEDEFNAME", "MATERIALTYPE")
                            .SetVisibleColumnsWidth(90, 70, 200, 80);

            grdCauseProcessSegmentIssue.View.AddTextBoxColumn("SPLITCONSUMABLEDEFIDVERSION", 100).SetIsReadOnly().SetIsHidden();
            grdCauseProcessSegmentIssue.View.AddTextBoxColumn("REASONCONSUMABLEDEFID", 100).SetIsReadOnly().SetIsHidden();
            grdCauseProcessSegmentIssue.View.AddTextBoxColumn("REASONCONSUMABLEDEFVERSION", 100).SetIsReadOnly().SetIsHidden();
            // 원인자재LOT
            grdCauseProcessSegmentIssue.View.AddComboBoxColumn("REASONCONSUMABLELOTID", 180, new SqlQuery("GetDefectReasonConsumableLot", "10002"), "CONSUMABLELOTID", "CONSUMABLELOTID")
                            .SetRelationIds("LOTID", "SPLITCONSUMABLEDEFIDVERSION");

            // 원인공정
            grdCauseProcessSegmentIssue.View.AddComboBoxColumn("REASONSEGMENTID", 200, new SqlQuery("GetDefectReasonProcesssegment", "10002"), "PROCESSSEGMENTNAME", "PROCESSSEGMENTID")
                            .SetMultiColumns(ComboBoxColumnShowType.Custom, true)
                            .SetRelationIds("SPLITCONSUMABLEDEFIDVERSION", "REASONCONSUMABLELOTID")
                            .SetPopupWidth(600)
                            .SetLabel("REASONPROCESSSEGMENTID")
                            .SetVisibleColumns("PROCESSSEGMENTID", "PROCESSSEGMENTNAME", "USERSEQUENCE", "AREANAME")
                            .SetVisibleColumnsWidth(90, 150, 70, 100);

            // 원인작업장
            grdCauseProcessSegmentIssue.View.AddTextBoxColumn("REASONAREAID", 100).SetIsReadOnly().SetIsHidden();
            grdCauseProcessSegmentIssue.View.AddTextBoxColumn("REASONAREANAME", 150).SetIsReadOnly().SetLabel("REASONAREAID");

            grdCauseProcessSegmentIssue.View.AddTextBoxColumn("NCRISSUEDATE", 150)
                .SetIsReadOnly();

            grdCauseProcessSegmentIssue.View.AddTextBoxColumn("ISNCRISSUE", 150)
                .SetIsReadOnly();

            //grdCauseProcessSegmentIssue.View.AddTextBoxColumn("DESCRIPTION", 150);

            grdCauseProcessSegmentIssue.View.PopulateColumns();
            #endregion
        }
        #endregion

        #region 그리드 팝업 초기화

        #endregion

        #region Event
        public void InitializeEvent()
        {//팝업 로드 이벤트
            this.Load += ShipmentInspNCRPopup_Load;
            //최종검사 발행 버튼클릭 이벤트
            btnShipmentIssue.Click += BtnShipmentIssue_Click;
            //원인공정 발행 버튼클릭 이벤트
            btnCauseIssue.Click += BtnCauseIssue_Click;

            //닫기 버튼클릭 이벤트
            btnClose.Click += (s, e) => 
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            };

            if(UserInfo.Current.Enterprise.Equals("INTERFLEX"))
            grdFinishIssue.View.ShowingEditor += (s, e) => 
            {
                if (!grdFinishIssue.View.FocusedColumn.FieldName.Equals("DESCRIPTION"))
                { e.Cancel = true; };
            };

            //원인공정 cellvaluechange 이벤트 2019-1212
            grdCauseProcessSegmentIssue.View.CellValueChanged += View_CellValueChanged;

            /*
            grdCauseProcessSegmentIssue.View.ShowingEditor += (s, e) =>
            {
                var consum = grdCauseProcessSegmentIssue.View.GetRowCellValue(grdCauseProcessSegmentIssue.View.FocusedRowHandle, "CONSUMABLEDEFNAME");
                var processSegment = grdCauseProcessSegmentIssue.View.GetRowCellValue(grdCauseProcessSegmentIssue.View.FocusedRowHandle, "PROCESSSEGMENTNAME");

                if (grdCauseProcessSegmentIssue.View.FocusedColumn.FieldName.Equals("CONSUMABLEDEFNAME"))
                {
                    if (consum != DBNull.Value)
                    {
                        e.Cancel = true;
                    }
                };

                if (grdCauseProcessSegmentIssue.View.FocusedColumn.FieldName.Equals("PROCESSSEGMENTNAME"))
                {
                    if (processSegment != DBNull.Value)
                    {
                        e.Cancel = true;
                    }
                };
            };
            */

            //grdFinishIssue 체크박스 이벤트
            grdFinishIssue.View.CheckStateChanged += View_CheckStateChanged;

            //grdCauseProcessSegmentIssue 체크박스 이벤트
            grdCauseProcessSegmentIssue.View.CheckStateChanged += GrdCause_CheckStateChanged;


            //이미 발행된 내용 수정 불가
            grdFinishIssue.View.ShowingEditor += View_ShowingEditor;
            grdCauseProcessSegmentIssue.View.ShowingEditor += View_ShowingEditor;

            if (UserInfo.Current.Enterprise.Equals("YOUNGPOONG"))
                grdFinishIssue.View.CellValueChanged += View_CellValueChanged1;

        }

        private void View_CellValueChanged1(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "PROCESSSEGMENTAREA")
            {
                grdFinishIssue.View.CellValueChanged -= View_CellValueChanged1;

                RepositoryItemLookUpEdit edit = e.Column.ColumnEdit as RepositoryItemLookUpEdit;

                string areaId = Format.GetString(edit.GetDataSourceValue("AREAID", edit.GetDataSourceRowIndex("PROCESSSEGMENTAREA", e.Value)));
                string areaName = Format.GetString(edit.GetDataSourceValue("AREANAME", edit.GetDataSourceRowIndex("PROCESSSEGMENTAREA", e.Value)));
                string processsegmentId = Format.GetString(edit.GetDataSourceValue("PROCESSSEGMENTID", edit.GetDataSourceRowIndex("PROCESSSEGMENTAREA", e.Value)));
                string processsegmentVersion = Format.GetString(edit.GetDataSourceValue("PROCESSSEGMENTVERSION", edit.GetDataSourceRowIndex("PROCESSSEGMENTAREA", e.Value)));

                grdFinishIssue.View.SetFocusedRowCellValue("AREAID", areaId);
                grdFinishIssue.View.SetFocusedRowCellValue("AREANAME", areaName);
                grdFinishIssue.View.SetFocusedRowCellValue("PROCESSSEGMENTID", processsegmentId);
                grdFinishIssue.View.SetFocusedRowCellValue("PROCESSSEGMENTVERSION", processsegmentVersion);           

                grdFinishIssue.View.CellValueChanged += View_CellValueChanged1;
            }
        }

        private void View_ShowingEditor(object sender, CancelEventArgs e)
        {
            SmartBandedGridView view = sender as SmartBandedGridView;
            DataRow row = view.GetFocusedDataRow();

            if (Format.GetString(row["ISNCRISSUE"]).Equals("Y"))
            {
                e.Cancel = true;
            }

        }

        private void View_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            #region - 원인 품목 / 공정 / 자재등
            /*// 원인 품목
            if (e.Column.FieldName.Equals("CONSUMABLEDEFIDVERSION"))
            {
                grdCauseProcessSegmentIssue.View.SetFocusedRowCellValue("REASONCONSUMABLELOTID", "");
                grdCauseProcessSegmentIssue.View.SetFocusedRowCellValue("REASONSEGMENTID", "");
                grdCauseProcessSegmentIssue.View.SetFocusedRowCellValue("REASONAREAID", "");

                RepositoryItemLookUpEdit lookup = (RepositoryItemLookUpEdit)e.Column.ColumnEdit;
                string id = lookup.GetDataSourceValue("CONSUMABLEDEFID", lookup.GetDataSourceRowIndex("CONSUMABLEDEFIDVERSION", e.Value)).ToString();
                string version = lookup.GetDataSourceValue("CONSUMABLEDEFVERSION", lookup.GetDataSourceRowIndex("CONSUMABLEDEFIDVERSION", e.Value)).ToString();

                grdCauseProcessSegmentIssue.View.SetFocusedRowCellValue("REASONCONSUMABLEDEFID", id);
                grdCauseProcessSegmentIssue.View.SetFocusedRowCellValue("REASONCONSUMABLEDEFVERSION", version);
            }
            */
            if (e.Column.FieldName == "REASONCONSUMABLEDEFIDVERSION")
            {
                grdCauseProcessSegmentIssue.View.CellValueChanged -= View_CellValueChanged;

                RepositoryItemLookUpEdit edit = e.Column.ColumnEdit as RepositoryItemLookUpEdit;

                string consumableDefId = Format.GetString(edit.GetDataSourceValue("CONSUMABLEDEFID", edit.GetDataSourceRowIndex("SPLITCONSUMABLEDEFIDVERSION", e.Value)));
                string consumableDefVersion = Format.GetString(edit.GetDataSourceValue("CONSUMABLEDEFVERSION", edit.GetDataSourceRowIndex("SPLITCONSUMABLEDEFIDVERSION", e.Value)));

                grdCauseProcessSegmentIssue.View.SetFocusedRowCellValue("SPLITCONSUMABLEDEFIDVERSION", e.Value);
                grdCauseProcessSegmentIssue.View.SetFocusedRowCellValue("REASONCONSUMABLEDEFID", consumableDefId);
                grdCauseProcessSegmentIssue.View.SetFocusedRowCellValue("REASONCONSUMABLEDEFVERSION", consumableDefVersion);

                grdCauseProcessSegmentIssue.View.CellValueChanged += View_CellValueChanged;
            }

            if (e.Column.FieldName == "REASONSEGMENTID")
            {
                grdCauseProcessSegmentIssue.View.CellValueChanged -= View_CellValueChanged;

                RepositoryItemLookUpEdit edit = e.Column.ColumnEdit as RepositoryItemLookUpEdit;

                string areaId = Format.GetString(edit.GetDataSourceValue("AREAID", edit.GetDataSourceRowIndex("PROCESSSEGMENTID", e.Value)));
                string areaName = Format.GetString(edit.GetDataSourceValue("AREANAME", edit.GetDataSourceRowIndex("PROCESSSEGMENTID", e.Value)));

                grdCauseProcessSegmentIssue.View.SetFocusedRowCellValue("REASONAREAID", areaId);
                grdCauseProcessSegmentIssue.View.SetFocusedRowCellValue("REASONAREANAME", areaName);

                grdCauseProcessSegmentIssue.View.CellValueChanged += View_CellValueChanged;
            }
            #endregion
        }



        /// <summary>
        /// 최종검사 NCR발행 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnShipmentIssue_Click(object sender, EventArgs e)
        {
            DataTable checkedRows = grdFinishIssue.View.GetCheckedRows();

            SaveDataTable(checkedRows, "SpillShipmentInspection");
        }

        /// <summary>
        /// 원인공정 NCR발행 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCauseIssue_Click(object sender, EventArgs e)
        {
            DataTable checkedRows = grdCauseProcessSegmentIssue.View.GetCheckedRows();

            SaveDataTable(checkedRows, "ReasonShipmentInspection");
        }


        /// <summary>
        /// 로드시 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShipmentInspNCRPopup_Load(object sender, EventArgs e)
        {
            InitializeLanguageKey();

            btnShipmentIssue.Enabled = isEnable;
            btnCauseIssue.Enabled = isEnable;

            //SetConsumableDefComboBox(CurrentDataRow["RESOURCEID"].ToString());
            SearchNCRData();

        }

        /// <summary>
        /// grdFinishIssue 발행 여부가 Y인경우 체크 불가
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CheckStateChanged(object sender, EventArgs e)
        {
            DataRow row = grdFinishIssue.View.GetDataRow(grdFinishIssue.View.GetFocusedDataSourceRowIndex());
            if (row["ISNCRISSUE"].ToString().Equals("Y"))
            {
                grdFinishIssue.View.CheckRow(grdFinishIssue.View.GetFocusedDataSourceRowIndex(), false);
                throw MessageException.Create("AlreadyIssuedNCR");//이미 NCR 발행된 항목입니다.
            }
        }

        /// <summary>
        /// grdCauseProcessSegmentIssue 발행 여부가 Y인경우 체크 불가
        /// 원인품목, 공정 입력유효성 평가
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdCause_CheckStateChanged(object sender, EventArgs e)
        {
            DataRow row = grdCauseProcessSegmentIssue.View.GetDataRow(grdCauseProcessSegmentIssue.View.GetFocusedDataSourceRowIndex());
            if (row["ISNCRISSUE"].ToString().Equals("Y"))
            {
                grdCauseProcessSegmentIssue.View.CheckRow(grdCauseProcessSegmentIssue.View.GetFocusedDataSourceRowIndex(), false);
                throw MessageException.Create("AlreadyIssuedNCR");//이미 NCR 발행된 항목입니다.
            }
            
            if (String.IsNullOrEmpty( row["REASONCONSUMABLEDEFID"].ToString()))
            {
                grdCauseProcessSegmentIssue.View.CheckRow(grdCauseProcessSegmentIssue.View.GetFocusedDataSourceRowIndex(), false);
                throw MessageException.Create("MustInputCauseMaterialLotId");//원인 품목을 입력하세요.
            }

            if (String.IsNullOrEmpty(row["REASONSEGMENTID"].ToString()))
            {
                grdCauseProcessSegmentIssue.View.CheckRow(grdCauseProcessSegmentIssue.View.GetFocusedDataSourceRowIndex(), false);
                throw MessageException.Create("MustInputCauseProcesssegmentId");//원인 공정을 입력하세요.
            }

        }
        #endregion

        #region Public Function
        /// <summary>
        /// REASONCONSUMABLEDEFID 콤보초기화
        /// </summary>
        public void SetConsumableDefComboBox(string lotId)
        {
            if (!string.IsNullOrEmpty(lotId))
            {
                grdCauseProcessSegmentIssue.View.RefreshComboBoxDataSource("REASONCONSUMABLEDEFIDVERSION", new SqlQuery("GetReasonConsumableList", "10002", $"LOTID={lotId}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
                grdCauseProcessSegmentIssue.View.RefreshComboBoxDataSource("REASONCONSUMABLELOTID", new SqlQuery("GetDefectReasonConsumableLot", "10002", $"LOTID={lotId}"));
                grdCauseProcessSegmentIssue.View.RefreshComboBoxDataSource("REASONSEGMENTID", new SqlQuery("GetDefectReasonProcesssegment", "10002", $"LOTID={lotId}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            }

        }
        #endregion

        #region Private Function
        private void InitializeLanguageKey()
        {
            accGroup.Text = Language.Get("NCRBTN");
            accShipment.Text = Language.Get("SHIPMENTINSPECTIONCAPTION");
            accCause.Text = Language.Get("CAUSEPROCESS");
        }

        /// <summary>
        /// NCR List를 조회하는 함수
        /// </summary>
        private void SearchNCRData()
        {
            Dictionary<string, object> values = new Dictionary<string, object>()
            {
                {"LANGUAGETYPE" ,Framework.UserInfo.Current.LanguageType},
                {"ENTERPRISEID" ,Framework.UserInfo.Current.Enterprise},
                {"PLANTID" ,CurrentDataRow["PLANTID"]},
                {"RESOURCEID" ,CurrentDataRow["RESOURCEID"]},
                {"DEGREE",CurrentDataRow["DEGREE"] },
                {"TXNGROUPHISTKEY" ,CurrentDataRow["TXNGROUPHISTKEY"]},
                {"ABNOCRTYPE" , "SpillShipmentInspection"}
            };

            DataTable dtFinish = SqlExecuter.Query("SelectDefectToNCRShipment", "10001", values);

            values.Remove("ABNOCRTYPE");
            values.Add("ABNOCRTYPE", "ReasonShipmentInspection");

            DataTable dtCause = SqlExecuter.Query("SelectDefectToNCRShipment", "10001", values);

            if (dtFinish.Rows.Count < 1 && dtCause.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }


            grdFinishIssue.DataSource = dtFinish;
            grdCauseProcessSegmentIssue.DataSource = dtCause;
        }
        /// <summary>
        /// 체크된 Row저장 함수
        /// </summary>
        /// <param name="checkedRows"></param>
        private void SaveDataTable(DataTable checkedRows, string abnormalType)
        {

            if (checkedRows.Rows.Count < 1)
            {
                this.ShowMessage("MustCheckLotToNCR");//NCR을 발행할 항목을 체크하세요.
                return;
            }

            try
            {
                this.ShowWaitArea();
                btnShipmentIssue.Enabled = false;
                btnCauseIssue.Enabled = false;

                MessageWorker messageWorker = new MessageWorker("SaveShipmentInspNCRIssue");
                messageWorker.SetBody(new MessageBody()
            {
                { "ABNORMALTYPE", abnormalType },
                { "ENTERPRISEID", Framework.UserInfo.Current.Enterprise},
                { "PLANTID", Framework.UserInfo.Current.Plant},
                { "list", checkedRows },
            });

                messageWorker.Execute();


                ShowMessage("SuccessSave");
            }
            catch (Exception ex)
            {
                this.ShowError(ex);
            }
            finally
            {

                this.CloseWaitArea();
                btnShipmentIssue.Enabled = true;
                btnCauseIssue.Enabled = true;
                //OnSearchClick();
                this.Close();
            }
        }
        #endregion
    }
}
