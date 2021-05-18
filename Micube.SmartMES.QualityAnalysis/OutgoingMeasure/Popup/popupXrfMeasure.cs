#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;

using System;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

#endregion

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 출하검사 > XRF 측정값 상세 팝업
    /// 업  무  설  명  : XRF 설비 인퍼페이스 측정값 Popup 
    /// 생    성    자  : 전우성
    /// 생    성    일  : 2020-03-02
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class popupXRFMeasure : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region Interface

        /// <summary>
        /// 부모의 선택된 Row
        /// </summary>
        public DataRow CurrentDataRow { get; set; }

        #endregion

        #region Local Variables

        /// <summary>
        /// 검사항목 List (Code List : InspectionXrfItem)
        /// </summary>
        private DataTable _inspectionItemList = null;

        /// <summary>
        /// 규격 범위 다국어
        /// </summary>
        private string _specRange = string.Empty;

        #endregion

        #region 생성자

        public popupXRFMeasure()
        {
            InitializeComponent();

            InitializeContent();
            InitializeLanguageKey();
            InitializeEvent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// Controler 초기화
        /// </summary>
        private void InitializeContent()
        {
            this.CancelButton = btnOk;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            txtLot.ReadOnly = true;
            txtProduct.ReadOnly = true;
            txtProductName.ReadOnly = true;
            txtProductVersion.ReadOnly = true;
            txtSite.ReadOnly = true;
            txtArea.ReadOnly = true;
            txtFactory.ReadOnly = true;
            txtEquipment.ReadOnly = true;
            txtProcess.ReadOnly = true;

            // xrf 검사항목 List 가져오기
            _inspectionItemList = SqlExecuter.Query("GetCodeList", "00001",
                                                    new Dictionary<string, object>
                                                    {
                                                        { "CODECLASSID", "InspectionXrfItem" },
                                                        { "LANGUAGETYPE", UserInfo.Current.LanguageType }
                                                    });

            if (SqlExecuter.Query("GetSpecRangeString", "10001",
                                                    new Dictionary<string, object>
                                                    {
                                                        { "LANGUAGETYPE", UserInfo.Current.LanguageType }
                                                    }) is DataTable dt)
            {
                if (dt.Rows.Count.Equals(0))
                {
                    return;
                }

                _specRange = DefectMapHelper.StringByDataRowObejct(dt.Rows[0], "SPECRANGE");
            };
        }

        /// <summary>
        /// Language Key 초기화
        /// </summary>
        private void InitializeLanguageKey()
        {
            this.LanguageKey = "MEASURVALUE";
            grpInfo.LanguageKey = "DEFAULTINFO";
            grdMain.LanguageKey = "MEASUREDVALUE";
            btnOk.LanguageKey = "OK";
            btnDownload.LanguageKey = "FILEATTACH";

            layoutInfo.SetLanguageKey(layoutLot, "LOTID");
            layoutInfo.SetLanguageKey(layoutProduct, "PRODUCTDEFID");
            layoutInfo.SetLanguageKey(layoutProductName, "PRODUCTDEFNAME");
            layoutInfo.SetLanguageKey(layoutProductVersion, "PRODUCTDEFVERSION");
            layoutInfo.SetLanguageKey(layoutSite, "SITE");
            layoutInfo.SetLanguageKey(layoutFactory, "FACTORY");
            layoutInfo.SetLanguageKey(layoutProcess, "STANDARDOPERATIONID");
            layoutInfo.SetLanguageKey(layoutArea, "AREAID");
            layoutInfo.SetLanguageKey(layoutEquipment, "EQUIPMENTUNIT");
            layoutInfo.SetLanguageKey(layoutdegree, "DEGREE");
        }

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid(List<string> searchInspItemList)
        {
            grdMain.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdMain.View.AddTextBoxColumn("DEGREE", 10).SetIsHidden();
            grdMain.View.AddTextBoxColumn("FILEEXT", 10).SetIsHidden();
            grdMain.View.AddTextBoxColumn("FILEPATH", 10).SetIsHidden();
            grdMain.View.AddTextBoxColumn("RECIPEID", 80).SetTextAlignment(TextAlignment.Left);
            grdMain.View.AddTextBoxColumn("RECIPENAME", 120).SetTextAlignment(TextAlignment.Left);
            grdMain.View.AddTextBoxColumn("EQUIPMENTPRODUCTID", 80).SetTextAlignment(TextAlignment.Left);
            grdMain.View.AddTextBoxColumn("EQUIPMENTPRODUCTNAME", 100).SetTextAlignment(TextAlignment.Left);
            grdMain.View.AddTextBoxColumn("MEASURER", 60).SetTextAlignment(TextAlignment.Left);
            grdMain.View.AddTextBoxColumn("FILENAME", 100).SetTextAlignment(TextAlignment.Left);

            foreach (DataRow item in _inspectionItemList.Rows)
            {
                if (!searchInspItemList.Contains(DefectMapHelper.StringByDataRowObejct(item, "CODEID")))
                {
                    continue;
                }

                grdMain.View.AddTextBoxColumn(string.Concat("RANGE_", DefectMapHelper.StringByDataRowObejct(item, "CODEID")), 80)
                            .SetLabel(string.Concat(DefectMapHelper.StringByDataRowObejct(item, "CODEID"), " ", _specRange))
                            .SetTextAlignment(TextAlignment.Left);

                grdMain.View.AddTextBoxColumn(string.Concat("POINT_", DefectMapHelper.StringByDataRowObejct(item, "CODEID")), 60)
                            .SetLabel(DefectMapHelper.StringByDataRowObejct(item, "CODENAME"))
                            .SetTextAlignment(TextAlignment.Right);

                grdMain.View.AddTextBoxColumn(string.Concat("RESULT_", DefectMapHelper.StringByDataRowObejct(item, "CODEID")), 10)
                            .SetIsHidden();
            }

            grdMain.View.PopulateColumns();
            grdMain.View.SetIsReadOnly();
            grdMain.View.BestFitColumns();

            grdMain.GridButtonItem = GridButtonItem.Export;
            grdMain.View.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            grdMain.View.ActiveFilterString = string.Concat("[DEGREE] = ''");
        }

        #endregion

        #region Event

        /// <summary>
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            // 화면 로드 이벤트
            this.Load += (s, e) =>
            {
                txtLot.Text = DefectMapHelper.StringByDataRowObejct(CurrentDataRow, "LOTID");
                txtProduct.Text = DefectMapHelper.StringByDataRowObejct(CurrentDataRow, "PRODUCTDEFID");
                txtProductName.Text = DefectMapHelper.StringByDataRowObejct(CurrentDataRow, "PRODUCTDEFNAME");
                txtProductVersion.Text = DefectMapHelper.StringByDataRowObejct(CurrentDataRow, "PRODUCTDEFVERSION");
                txtSite.Text = DefectMapHelper.StringByDataRowObejct(CurrentDataRow, "PLANTID");
                txtFactory.Text = DefectMapHelper.StringByDataRowObejct(CurrentDataRow, "FACTORYNAME");
                txtProcess.Text = DefectMapHelper.StringByDataRowObejct(CurrentDataRow, "PROCESSSEGMENTNAME");
                txtArea.Text = DefectMapHelper.StringByDataRowObejct(CurrentDataRow, "AREANAME");
                txtEquipment.Text = DefectMapHelper.StringByDataRowObejct(CurrentDataRow, "EQUIPMENTUNIT");

                Dictionary<string, object> param = new Dictionary<string, object>()
                {
                    {"P_SPECCLASSID", "SAVE_MEASURE_XRF" },
                    {"P_LOTID", CurrentDataRow["LOTID"] },
                    {"P_PRODUCTDEFID", CurrentDataRow["PRODUCTDEFID"] },
                    {"P_PRODUCTDEFVERSION", CurrentDataRow["PRODUCTDEFVERSION"] },
                    {"P_PROCESSSEGMENTID", CurrentDataRow["PROCESSSEGMENTID"] },
                    {"P_AREAID", CurrentDataRow["AREAID"] },
                    {"P_PLANTID", CurrentDataRow["PLANTID"] }
                };

                SetData(SqlExecuter.Query("GetXRFMeasureByLot", "10001", param));
                DefectMapHelper.SetDegree(cboDegree, DefectMapHelper.StringByDataRowObejct(CurrentDataRow, "DEGREE"));
            };

            // 차수 변경 이벤트
            cboDegree.EditValueChanged += (s, e) => grdMain.View.ActiveFilterString = string.Concat("[DEGREE] = '", cboDegree.Text, "'");

            // NG 발생시 색상변경
            grdMain.View.RowCellStyle += (s, e) =>
            {
                if (!e.Column.FieldName.Substring(0, 5).Equals("POINT"))
                {
                    return;
                }

                string pointId = e.Column.FieldName.Substring(6, e.Column.FieldName.Length - 6);

                if (Format.GetString(grdMain.View.GetRowCellValue(e.RowHandle, string.Concat("RESULT_", pointId))) is string result)
                {
                    e.Appearance.BackColor = result.Equals("OK") ? e.Appearance.BackColor : Color.Red;
                    e.Appearance.ForeColor = result.Equals("OK") ? e.Appearance.ForeColor : Color.White;
                }
            };

            // 첨부파일 다운로드
            btnDownload.Click += (s, e) =>
            {
                if (grdMain.View.GetCheckedRows() is DataTable dt)
                {
                    if (dt.Rows.Count.Equals(0))
                    {
                        return;
                    }

                    try
                    {
                        DialogManager.ShowWaitArea(this);
                        FolderBrowserDialog dialog = new FolderBrowserDialog
                        {
                            SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                        };

                        if (dialog.ShowDialog().Equals(DialogResult.OK))
                        {
                            string folderPath = dialog.SelectedPath;
                            string serverPath = string.Empty, file = string.Empty;

                            dt.AsEnumerable().ForEach(dr =>
                            {
                                if (!string.IsNullOrEmpty(DefectMapHelper.StringByDataRowObejct(dr, "FILENAME")))
                                {
                                    serverPath = DefectMapHelper.FTPFilePathCheck(DefectMapHelper.StringByDataRowObejct(dr, "FILEPATH"));
                                    file = string.Join(".", DefectMapHelper.StringByDataRowObejct(dr, "FILENAME"), DefectMapHelper.StringByDataRowObejct(dr, "FILEEXT"));

                                    DefectMapHelper.FTPInterfaceFileDownload(serverPath, file, dialog.SelectedPath, false);
                                }
                            });

                            ShowMessage("SuccedSave");
                        }
                    }
                    catch (Exception ex)
                    {
                        MSGBox.Show(ex);
                    }
                    finally
                    {
                        DialogManager.CloseWaitArea(this);
                    }
                }
            };
        }

        #endregion

        #region Private Function

        /// <summary>
        /// Grid에 들어갈 Data Setting
        /// </summary>
        /// <param name="dt"></param>
        private void SetData(DataTable dt)
        {
            if (dt.Rows.Count.Equals(0))
            {
                return;
            }

            List<string> searchInspItemList = dt.AsEnumerable().GroupBy(x => x.Field<string>("DAITEMID")).Select(x => x.Key).ToList();
            InitializeGrid(searchInspItemList);

            DataTable finalDt = (grdMain.DataSource as DataTable);
            DataRow newRow = finalDt.NewRow();
            int pointId = 0;

            foreach (DataRow dr in dt.Rows)
            {
                if (DefectMapHelper.IntByDataRowObject(dr, "DAPOINTID") > pointId)
                {
                    if (!string.IsNullOrEmpty(DefectMapHelper.StringByDataRowObejct(newRow, "RECIPEID")))
                    {
                        SetInspItemMatching(newRow, dr, searchInspItemList);
                        continue;
                    }

                    SetDataRowCopy(newRow, dr);
                    SetInspItemMatching(newRow, dr, searchInspItemList);
                }
                else
                {
                    finalDt.Rows.Add(newRow);
                    newRow = finalDt.NewRow();
                    SetDataRowCopy(newRow, dr);
                    SetInspItemMatching(newRow, dr, searchInspItemList);
                }

                pointId = DefectMapHelper.IntByDataRowObject(dr, "DAPOINTID");
            }

            finalDt.Rows.Add(newRow);
            newRow = finalDt.NewRow();

            grdMain.DataSource = finalDt;
        }

        /// <summary>
        /// 검사항목 Field에 매칭
        /// </summary>
        /// <param name="newRow"></param>
        /// <param name="readRow"></param>
        /// <param name="inspItemList"></param>
        private void SetInspItemMatching(DataRow newRow, DataRow readRow, List<string> inspItemList)
        {
            string itemId = DefectMapHelper.StringByDataRowObejct(readRow, "DAITEMID");

            if (inspItemList.Contains(itemId))
            {
                newRow[string.Concat("POINT_", itemId)] = DefectMapHelper.StringByDataRowObejct(readRow, "VALUE");
                newRow[string.Concat("RANGE_", itemId)] = DefectMapHelper.StringByDataRowObejct(readRow, "SPECRANGE");
                newRow[string.Concat("RESULT_", itemId)] = DefectMapHelper.StringByDataRowObejct(readRow, "RESULT");
            }
        }

        /// <summary>
        /// NewRow에 Data Setting
        /// </summary>
        /// <param name="newRow"></param>
        /// <param name="readRow"></param>
        private void SetDataRowCopy(DataRow newRow, DataRow readRow)
        {
            newRow["DEGREE"] = DefectMapHelper.StringByDataRowObejct(readRow, "DEGREE");
            newRow["RECIPEID"] = DefectMapHelper.StringByDataRowObejct(readRow, "RECIPEID");
            newRow["RECIPENAME"] = DefectMapHelper.StringByDataRowObejct(readRow, "RECIPENAME");
            newRow["EQUIPMENTPRODUCTID"] = DefectMapHelper.StringByDataRowObejct(readRow, "EQUIPMENTPRODUCTID");
            newRow["EQUIPMENTPRODUCTNAME"] = DefectMapHelper.StringByDataRowObejct(readRow, "EQUIPMENTPRODUCTNAME");
            newRow["FILEEXT"] = DefectMapHelper.StringByDataRowObejct(readRow, "FILEEXT");
            newRow["FILEPATH"] = DefectMapHelper.StringByDataRowObejct(readRow, "FILEPATH");
            newRow["FILENAME"] = DefectMapHelper.StringByDataRowObejct(readRow, "FILENAME");
            newRow["MEASURER"] = DefectMapHelper.StringByDataRowObejct(readRow, "MEASURER");
        }

        #endregion
    }
}