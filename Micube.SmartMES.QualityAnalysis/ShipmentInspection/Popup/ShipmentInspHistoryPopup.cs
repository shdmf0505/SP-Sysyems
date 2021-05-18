using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Grid.BandedGrid;
using Micube.SmartMES.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 출하검사 > 출하검사 이력 조회 팝업
    /// 업  무  설  명  : 출하검사 이력 조회 팝업
    /// 생    성    자  : 강유라
    /// 생    성    일  : 2019-09-30
    /// 수  정  이  력  :
    ///     1. 2021.01.20 전우성 : 이미지 다운로드 기능 추가 및 코드 정리
    ///
    /// </summary>
    ///
    public partial class ShipmentInspHistoryPopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region 인터페이스

        public DataRow CurrentDataRow { get; set; }

        #endregion 인터페이스

        #region Local Variables

        private DataTable _fileDt;
        private DataTable _messageDt;
        public DataTable _currentDt;
        private DataRow _processRow;
        private DataRow _creatorRow;

        private decimal _sampleQty;

        #endregion Local Variables

        #region 생성자

        public ShipmentInspHistoryPopup()
        {
            InitializeComponent();
            InitializeEvent();
            InitializeGrid();
            InitializationSummaryRow();
            InitializeControl();
        }

        #endregion 생성자

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 컨트롤 초기화한다.
        /// </summary>
        private void InitializeControl()
        {
            // LOT 정보 GRID
            grdLotComInfo.ColumnCount = 5;
            grdLotComInfo.LabelWidthWeight = "40%";
            grdLotComInfo.SetInvisibleFields("PROCESSSTATE", "PROCESSPATHID", "PROCESSSEGMENTID", "PROCESSSEGMENTVERSION", "NEXTPROCESSSEGMENTID",
                "NEXTPROCESSSEGMENTVERSION", "PRODUCTDEFVERSION", "PRODUCTTYPE", "ISHOLD", "ISREWORK", "DEFECTUNIT",
                "PANELPERQTY", "PROCESSSEGMENTTYPE", "STEPTYPE", "ISPRINTLOTCARD", "ISPRINTRCLOTCARD", "TRACKINUSER",
                "TRACKINUSERNAME", "MATERIALCLASS");

            grdLotComInfo.Enabled = false;

            picBox1.Properties.ShowMenu = false;
            picBox2.Properties.ShowMenu = false;
            picBox3.Properties.ShowMenu = false;
        }

        public void InitializeGrid()
        {
            #region 검사결과 그리드 초기화

            grdHistory.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdHistory.View.SetIsReadOnly();

            //childLot 정보
            var childLotInfo = grdHistory.View.AddGroupColumn("");
            childLotInfo.AddTextBoxColumn("RESOURCEID", 250)
                .SetLabel("CHILDLOTID");

            childLotInfo.AddTextBoxColumn("DEGREE", 100);
            //.SetIsHidden();

            //전체수량
            var allQty = grdHistory.View.AddGroupColumn("ALL");
            allQty.AddTextBoxColumn("ALLQTYPNL", 80)
                 .SetLabel("PNL");
            allQty.AddTextBoxColumn("ALLQTYPCS", 80)
                .SetLabel("PCS");

            //양품
            var goodQty = grdHistory.View.AddGroupColumn("GOODQTY");
            goodQty.AddTextBoxColumn("GOODQTYPNL", 80)
                 .SetLabel("PNL");
            goodQty.AddTextBoxColumn("GOODQTYPCS", 80)
                .SetLabel("PCS");

            //불량
            var defectQty = grdHistory.View.AddGroupColumn("DEFECTCOUNT");
            defectQty.AddTextBoxColumn("DEFECTQTYPNL", 80)
                 .SetLabel("PNL");
            defectQty.AddTextBoxColumn("SPECOUTQTY", 80)
                .SetLabel("PCS");
            defectQty.AddTextBoxColumn("DEFECTRATE", 80);

            //시료량
            var sampleQty = grdHistory.View.AddGroupColumn("INSPECTIONQTY");
            sampleQty.AddTextBoxColumn("INSPECTIONQTY", 80)
                 .SetLabel("PCS");

            //검사결과
            var inspInfo = grdHistory.View.AddGroupColumn("");
            inspInfo.AddTextBoxColumn("INSPECTORNAME", 100);
            inspInfo.AddTextBoxColumn("INSPECTIONRESULT", 100);

            grdHistory.View.PopulateColumns();

            #endregion 검사결과 그리드 초기화

            #region 불량코드 그리드 초기화

            grdHistoryDetail.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdHistoryDetail.View.SetIsReadOnly();

            grdHistoryDetail.View.AddTextBoxColumn("RESOURCEID", 250)
               .SetLabel("LOTID")
               .SetIsHidden();

            grdHistoryDetail.View.AddTextBoxColumn("DEGREE", 150);

            grdHistoryDetail.View.AddTextBoxColumn("DEFECTCODENAME", 150);

            grdHistoryDetail.View.AddTextBoxColumn("QCSEGMENTNAME", 150);

            grdHistoryDetail.View.AddTextBoxColumn("DEFECTQTY", 150);

            grdHistoryDetail.View.AddTextBoxColumn("DEFECTQTYPNL", 150)
                .SetIsHidden();

            grdHistoryDetail.View.AddTextBoxColumn("DEFECTRATE", 150);

            grdHistoryDetail.View.PopulateColumns();

            #endregion 불량코드 그리드 초기화

            #region 불량처리 그리드 초기화

            grdDefectDisposal.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdDefectDisposal.View.SetIsReadOnly();

            grdDefectDisposal.View.AddTextBoxColumn("RESOURCEID", 250)
               .SetLabel("LOTID")
               .SetIsHidden();

            grdDefectDisposal.View.AddTextBoxColumn("DEGREE", 150);

            grdDefectDisposal.View.AddTextBoxColumn("DEFECTCODENAME", 150);

            grdDefectDisposal.View.AddTextBoxColumn("QCSEGMENTNAME", 150);

            grdDefectDisposal.View.AddTextBoxColumn("DEFECTQTY", 150);

            grdDefectDisposal.View.AddTextBoxColumn("DEFECTQTYPNL", 150)
                .SetIsHidden();

            grdDefectDisposal.View.AddTextBoxColumn("DEFECTRATE", 150);

            grdDefectDisposal.View.PopulateColumns();

            #endregion 불량처리 그리드 초기화

            #region 메세지 관련 그리드 초기화

            grdProcessSegmentList.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;
            grdProcessSegmentList.View.SetIsReadOnly();
            grdProcessSegmentList.View.SetAutoFillColumn("PROCESSSEGMENTNAME");

            grdProcessSegmentList.View.AddTextBoxColumn("PROCESSSEGMENTNAME");

            grdProcessSegmentList.View.PopulateColumns();

            grdCreatorList.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;
            grdCreatorList.View.SetIsReadOnly();
            grdCreatorList.View.SetAutoFillColumn("CREATORNAME");

            grdCreatorList.View.AddTextBoxColumn("CREATORNAME");

            grdCreatorList.View.PopulateColumns();

            #endregion 메세지 관련 그리드 초기화
        }

        #region Row 합계 초기화

        /// <summary>
        /// 합계 Row 초기화
        /// </summary>
        private void InitializationSummaryRow()
        {
            #region grdChildLot Row 합계 초기화

            grdHistoryDetail.View.Columns["DEGREE"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdHistoryDetail.View.Columns["DEGREE"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));

            grdHistoryDetail.View.Columns["DEFECTQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdHistoryDetail.View.Columns["DEFECTQTY"].SummaryItem.DisplayFormat = "{0}";

            grdHistoryDetail.View.Columns["DEFECTQTYPNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdHistoryDetail.View.Columns["DEFECTQTYPNL"].SummaryItem.DisplayFormat = "{0}"; ;

            grdHistoryDetail.View.Columns["DEFECTRATE"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom;//***불량율 합계는 100넘을 수있음 다시계산?***
            grdHistoryDetail.View.Columns["DEFECTRATE"].SummaryItem.DisplayFormat = "{0:f2} %";

            grdHistoryDetail.View.OptionsView.ShowFooter = true;
            grdHistoryDetail.ShowStatusBar = false;

            #endregion grdChildLot Row 합계 초기화
        }

        #endregion Row 합계 초기화

        #endregion 컨텐츠 영역 초기화

        #region Event

        public void InitializeEvent()
        {
            //로드 이벤트
            this.Load += ShipmentInspHistoryPopup_Load;
            //닫기 버튼 이벤트
            btnClose.Click += (s, e) =>
            {
                this.Close();
            };

            //grdHistoryDetail focusedRow 체인지 이벤트
            grdHistoryDetail.View.FocusedRowChanged += View_FocusedRowChanged;

            //grdDefectDisposal focusedRow 체인지 이벤트
            grdDefectDisposal.View.FocusedRowChanged += View_FocusedRowChanged;

            //grdHistoryDetail 불량율 Custom 계산 이벤트 (Footer)
            grdHistoryDetail.View.CustomSummaryCalculate += GrdChildLotDetail_CustomSummaryCalculate;

            //grdProcessSegmentList focusedRow 체인지 이벤트
            grdProcessSegmentList.View.FocusedRowChanged += (s, e) =>
            {
                _processRow = grdProcessSegmentList.View.GetDataRow(e.FocusedRowHandle);
                SearchCreatorList(_processRow, CurrentDataRow);
            };

            //grdCreatorList focusedRow 체인지 이벤트
            grdCreatorList.View.FocusedRowChanged += (s, e) =>
            {
                _processRow = grdProcessSegmentList.View.GetFocusedDataRow();
                _creatorRow = grdCreatorList.View.GetDataRow(e.FocusedRowHandle);
                SearchMessageList(_creatorRow, _processRow, CurrentDataRow);
            };

            tabResult.SelectedPageChanged += TabResult_SelectedPageChanged;
        }

        /// <summary>
        /// 팝업을 열때 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShipmentInspHistoryPopup_Load(object sender, EventArgs e)
        {
            _currentDt.Clear();

            #region lotgrd

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            param.Add("PLANTID", CurrentDataRow["PLANTID"]);
            param.Add("AREAID", CurrentDataRow["LOTAREAID"]);
            param.Add("LOTID", CurrentDataRow["RESOURCEID"]);
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            ///lot 정보조회
            //DataTable lotInfo = SqlExecuter.Query("SelectLotInfoByProcess", "10001", param);
            DataTable lotInfo = SqlExecuter.Query("SelectLotInfoByProcessShipment", "10001", param);

            if (lotInfo.Rows.Count < 1)
            {
                // 조회할 데이터가 없습니다.
                ShowMessage("NoSelectData");
                return;
            }

            #endregion lotgrd

            #region Lot통합 정보

            _sampleQty = CurrentDataRow["INSPECTIONQTY"].ToSafeDecimal();
            grdLotComInfo.Enabled = true;
            grdLotComInfo.DataSource = lotInfo;

            _currentDt.ImportRow(CurrentDataRow);

            #endregion Lot통합 정보

            #region Lot상세 불량 정보

            Dictionary<string, object> values = new Dictionary<string, object>()
            {
                {"LANGUAGETYPE" ,Framework.UserInfo.Current.LanguageType},
                {"ENTERPRISEID" ,Framework.UserInfo.Current.Enterprise},
                {"PLANTID" ,CurrentDataRow["PLANTID"]},
                {"RESOURCEID" ,CurrentDataRow["RESOURCEID"]},
                {"TXNGROUPHISTKEY" ,CurrentDataRow["TXNGROUPHISTKEY"]},
                {"DEGREE",CurrentDataRow["DEGREE"] },
                {"INDEX","0" }
            };

            DataTable dt = SqlExecuter.Query("SelectShipmentInspHistoryDetailExterior", "10001", values);

            grdHistoryDetail.DataSource = dt;

            DataRow row = grdHistoryDetail.View.GetFocusedDataRow();

            values.Remove("INDEX");
            values.Add("INDEX", "1");

            DataTable dt2 = SqlExecuter.Query("SelectShipmentInspHistoryDetailExterior", "10001", values);

            grdDefectDisposal.DataSource = dt2;

            SearchImage(row);

            #endregion Lot상세 불량 정보

            SearchProcessSegmentList(CurrentDataRow);
        }

        /// <summary>
        /// 포커스된 row에 등록된 이미지를 보여주는 이벤트(저장후)
        /// lotid+inspItem 으로 파일정보 셀렉트하여 이미지 보여주기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            SmartBandedGridView view = sender as SmartBandedGridView;
            DataRow row = view.GetDataRow(e.FocusedRowHandle);

            SearchImage(row);
        }

        /// <summary>
        /// 탭 전환시 이미지 바인딩 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabResult_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            DataRow row = null; ;

            if (tabResult.SelectedTabPageIndex == 0)
            {
                row = grdHistoryDetail.View.GetFocusedDataRow();
            }
            else if (tabResult.SelectedTabPageIndex == 1)
            {
                row = grdDefectDisposal.View.GetFocusedDataRow();
            }

            SearchImage(row);
        }

        /// <summary>
        /// 불량률 계산 불량명 별
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdChildLotDetail_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            if (e.IsTotalSummary)
            {
                GridSummaryItem item = e.Item as GridSummaryItem;
                if (item.FieldName == "DEFECTRATE")
                {
                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                    {   //개별 lot의 불량 PCS 수량 합
                        decimal defectQtySumPCS = Convert.ToInt16((sender as GridView).Columns["DEFECTQTY"].SummaryItem.SummaryValue);
                        //개별 lot의 불량 PNL 수량 합
                        decimal defectQtySumPNL = Convert.ToInt16((sender as GridView).Columns["DEFECTQTYPNL"].SummaryItem.SummaryValue);

                        if (_sampleQty != 0 && defectQtySumPCS != 0)
                        {//개별 lot의 전체 수량과 개별 lot의 불량 수량 합이 0이 아닐때 (PCS로 계산 한 불량률)
                            decimal defectRate = Math.Round((defectQtySumPCS.ToSafeDecimal() / _sampleQty * 100).ToSafeDecimal(), 1);
                            e.TotalValue = defectRate;

                            DataRow row = _currentDt.Rows[0];
                            row["SPECOUTQTY"] = defectQtySumPCS;
                            row["DEFECTQTYPNL"] = defectQtySumPNL;
                            row["DEFECTRATE"] = defectRate + "%";

                            decimal goodQtyPCS = row["ALLQTYPCS"].ToSafeDecimal() - defectQtySumPCS;
                            decimal goodQtyPNL = row["ALLQTYPNL"].ToSafeDecimal() - defectQtySumPNL;

                            if (goodQtyPCS < 0)
                            {
                                row["GOODQTYPCS"] = 0;
                            }
                            else
                            {
                                row["GOODQTYPCS"] = goodQtyPCS;
                            }

                            if (goodQtyPNL < 0)
                            {
                                row["GOODQTYPNL"] = 0;
                            }
                            else
                            {
                                row["GOODQTYPNL"] = goodQtyPNL;
                            }
                        }

                        grdHistory.DataSource = _currentDt;
                    }
                }
            }
        }

        #endregion Event

        #region Public Function

        /// <summary>
        /// List를 dataTable로 반환하는 함수
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(IList<T> list)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in list)
            {
                for (int i = 0; i < values.Length; i++)
                    values[i] = props[i].GetValue(item) ?? DBNull.Value;
                table.Rows.Add(values);
            }
            return table;
        }

        #endregion Public Function

        #region Private Function

        /// <summary>
        /// LotList에 선택한 Lot의 메세지가 등록된 공정 조회함수
        /// </summary>
        /// <param name="row"></param>
        private void SearchProcessSegmentList(DataRow row)
        {
            if (row == null) return;

            Dictionary<string, object> values = new Dictionary<string, object>()
            {
                {"LANGUAGETYPE",Framework.UserInfo.Current.LanguageType },
                {"ENTERPRISE",Framework.UserInfo.Current.Enterprise},
                {"PLANT",CurrentDataRow["PLANTID"]},
                {"LOTID",row["RESOURCEID"] },
                {"PRODUCTDEFID" ,row["PRODUCTDEFID"]},
                {"PRODUCTDEFVERSION",row["PRODUCTDEFVERSION"] }
            };

            _messageDt = SqlExecuter.Query("SelectLotMessageShipmentInsp", "10001", values);

            var processSegmentList = _messageDt.AsEnumerable()
                .Where(r => r["RESOURCEID"].ToString().Equals(row["RESOURCEID"]) && r["PRODUCTDEFID"].ToString().Equals(row["PRODUCTDEFID"]) && r["PRODUCTDEFVERSION"].ToString().Equals(row["PRODUCTDEFVERSION"]))
                .Select(r => new { PROCESSSEGMENTID = r["PROCESSSEGMENTID"], PROCESSSEGMENTVERSION = r["PROCESSSEGMENTVERSION"], PROCESSSEGMENTNAME = r["PROCESSSEGMENTNAME"] })
                .Distinct().ToList();

            DataTable bindingDt = ToDataTable(processSegmentList);

            if (bindingDt.Rows.Count < 1)
            {
                grdProcessSegmentList.DataSource = null;
                grdCreatorList.DataSource = null;
            }
            else
            {
                grdProcessSegmentList.DataSource = null;
                grdProcessSegmentList.DataSource = bindingDt;
                _processRow = grdProcessSegmentList.View.GetFocusedDataRow();
                SearchCreatorList(_processRow, row);
            }
        }

        /// <summary>
        /// grdProcessSegmentList에서 선택한 공정의 메세지를 등록한 등록자 조회함수
        /// </summary>
        /// <param name="processRow"></param>
        private void SearchCreatorList(DataRow processRow, DataRow lotRow)
        {
            if (processRow == null || lotRow == null) return;

            grdCreatorList.DataSource = null;
            txtTitle.Text = string.Empty;
            memoBox.Text = string.Empty;

            var creatorList = _messageDt.AsEnumerable().
                OrderBy(r => r["SEQUENCE"])
                .Where(r => r["PROCESSSEGMENTID"].ToString().Equals(processRow["PROCESSSEGMENTID"]) && r["PROCESSSEGMENTVERSION"].ToString().Equals(processRow["PROCESSSEGMENTVERSION"])
                && r["PRODUCTDEFID"].ToString().Equals(lotRow["PRODUCTDEFID"]) && r["PRODUCTDEFVERSION"].ToString().Equals(lotRow["PRODUCTDEFVERSION"])
                && r["RESOURCEID"].ToString().Equals(lotRow["RESOURCEID"]))
                .Select(r => new { CREATOR = r["CREATOR"], SEQUENCE = r["SEQUENCE"], CREATORNAME = r["CREATORNAME"], })
                .ToList();

            DataTable creatorDt = ToDataTable(creatorList);

            if (creatorDt.Rows.Count < 1)
            {
                grdCreatorList.DataSource = null;
                txtTitle.Text = string.Empty;
                memoBox.Text = string.Empty;
            }
            else
            {
                grdCreatorList.DataSource = null;
                grdCreatorList.DataSource = creatorDt;
                _processRow = grdProcessSegmentList.View.GetFocusedDataRow();
                _creatorRow = grdCreatorList.View.GetFocusedDataRow();
                SearchMessageList(_creatorRow, _processRow, CurrentDataRow);
            }
        }

        /// <summary>
        /// grdCreatorList 에서 선택한 공정의 메세지를 등록한 등록자 조회함수
        /// </summary>
        /// <param name="creatorRow"></param>
        private void SearchMessageList(DataRow creatorRow, DataRow processRow, DataRow lotRow)
        {
            if (creatorRow == null || processRow == null || lotRow == null) return;

            txtTitle.Text = string.Empty;
            memoBox.Rtf = string.Empty;

            var message = _messageDt.AsEnumerable()
                .Where(r => r["PROCESSSEGMENTID"].ToString().Equals(processRow["PROCESSSEGMENTID"]) && r["PROCESSSEGMENTVERSION"].ToString().Equals(processRow["PROCESSSEGMENTVERSION"])
                && r["PRODUCTDEFID"].ToString().Equals(lotRow["PRODUCTDEFID"]) && r["PRODUCTDEFVERSION"].ToString().Equals(lotRow["PRODUCTDEFVERSION"])
                && r["RESOURCEID"].ToString().Equals(lotRow["RESOURCEID"]) && r["CREATOR"].ToString().Equals(creatorRow["CREATOR"])
                && Convert.ToInt32(r["SEQUENCE"]) == Convert.ToInt32(creatorRow["SEQUENCE"]))
                .Select(r => new { TITLE = r["TITLE"], MESSAGE = r["MESSAGE"] })
                .ToList();

            DataTable messageDt = ToDataTable(message);

            if (messageDt.Rows.Count < 1)
            {
                txtTitle.Text = string.Empty;
                memoBox.Rtf = string.Empty;
            }
            else
            {
                txtTitle.Text = messageDt.Rows[0]["TITLE"].ToString();
                memoBox.Rtf = messageDt.Rows[0]["MESSAGE"].ToString();
            }
        }

        /// <summary>
        /// 포커스된 row가 바뀔때 이미지 찾는 함수
        /// </summary>
        private void SearchImage(DataRow row)
        {
            picBox1.Image = null;
            picBox2.Image = null;
            picBox3.Image = null;

            if (row == null) return;

            Dictionary<string, object> values = new Dictionary<string, object>()
                {
                    {"RESOURCETYPE", "ShipmentInspection" },
                    {"RESOURCEVERSION", "*"}
                };

            if (tabResult.SelectedTabPageIndex == 0)
            {
                values.Add("RESOURCEID", row["RESOURCEID"] + row["DEGREE"].ToString() + row["DEFECTCODE"] + row["QCSEGMENTID"]);
            }
            else if (tabResult.SelectedTabPageIndex == 1)
            {
                values.Add("RESOURCEID", row["RESOURCEID"] + row["DEGREE"].ToString() + row["DEFECTCODE"] + row["QCSEGMENTID"] + "D");
            }

            _fileDt = SqlExecuter.Query("GetFileHttpPathFromObjectFileByStandardInfo", "10001", values);

            foreach (DataRow fileRow in _fileDt.Rows)
            {
                string filenameAndExt = fileRow.GetString("FILENAME") + "." + fileRow.GetString("FILEEXT");
                MenuItem context = new MenuItem();
                context.Text = "Image Download";
                context.Click += ImageDownLoad;

                if (picBox1.Image == null)
                {
                    //2020-01-28 파일 경로변경
                    picBox1.Image = CommonFunction.GetFtpImageFileToBitmap(fileRow.GetString("FILEPATH"), filenameAndExt);

                    context.Name = "1";
                    picBox1.ContextMenu = new ContextMenu(new MenuItem[] { context });
                }
                else if (picBox2.Image == null)
                {
                    picBox2.Image = CommonFunction.GetFtpImageFileToBitmap(fileRow.GetString("FILEPATH"), filenameAndExt);

                    context.Name = "2";
                    picBox2.ContextMenu = new ContextMenu(new MenuItem[] { context });
                }
                else if (picBox3.Image == null)
                {
                    picBox3.Image = CommonFunction.GetFtpImageFileToBitmap(fileRow.GetString("FILEPATH"), filenameAndExt);

                    context.Name = "3";
                    picBox3.ContextMenu = new ContextMenu(new MenuItem[] { context });
                }
            }
        }

        private void ImageDownLoad(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog
            {
                SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };

            if (dialog.ShowDialog().Equals(DialogResult.OK))
            {
                switch ((sender as Menu).Name)
                {
                    case "1":
                        picBox1.Image.Save(string.Concat(dialog.SelectedPath, "\\", DateTime.Now.ToString("yyyyMMddHHmmss"), ".png"));
                        break;

                    case "2":
                        picBox2.Image.Save(string.Concat(dialog.SelectedPath, "\\", DateTime.Now.ToString("yyyyMMddHHmmss"), ".png"));
                        break;

                    case "3":
                        picBox3.Image.Save(string.Concat(dialog.SelectedPath, "\\", DateTime.Now.ToString("yyyyMMddHHmmss"), ".png"));
                        break;
                }

                ShowMessage("SuccedSave");
            }
        }

        #endregion Private Function
    }
}