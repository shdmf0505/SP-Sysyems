#region using

using Micube.Framework;
using Micube.Framework.SmartControls;

using DevExpress.XtraGrid.Views.Grid;

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
    /// 프 로 그 램 명  : Defect Map BasicForm Diagram User Control
    /// 업  무  설  명  : Defect Map 공통 모듈로 사용되는 Control
    /// 생    성    자  : 전우성
    /// 생    성    일  : 2019-05-01
    /// 필  수  처  리  :
    /// 수  정  이  력  : 
    /// 
    /// </summary>
    public partial class ucDefectMapBasicForm : DevExpress.XtraEditors.XtraUserControl
    {
        #region Global Variable

        /// <summary>
        /// 화면에 사용되는 Row DataTable
        /// </summary>
        private DataTable _totalDataTable = null;

        /// <summary>
        /// AOI / BBT 모든 DataTable
        /// </summary>
        private DataTable _mainDataTable = null;

        /// <summary>
        /// Filter dataTable
        /// </summary>
        private DataTable _filterDataTable = null;

        /// <summary>
        /// 분석되지 않은 Row Data Grid
        /// </summary>
        private SmartBandedGrid _rowDataGrid = null;

        /// <summary>
        /// Nail Map 기준 ID
        /// </summary>
        private string _sParam = string.Empty;

        /// <summary>
        /// AOI Diagram Size X
        /// </summary>
        private int _nXMax = 0;

        /// <summary>
        /// AOI Diagram Size Y
        /// </summary>
        private int _nYMax = 0;

        /// <summary>
        /// PCS 도면 이미지
        /// </summary>
        private Image _pcsImage = null;

        /// <summary>
        /// 품목별 조회시 true / lot별 조회시 false
        /// </summary>
        private bool _isItemType = true;

        /// <summary>
        /// 이중 작업 막기 위한 함수
        /// </summary>
        private bool _isRun = false;

        /// <summary>
        /// Equipment Type : AOI / BBT / HOLE
        /// </summary>
        private EquipmentType _equipmentType = EquipmentType.EQUIPMENTTYPE_AOI;

        /// <summary>
        /// 팝업 유무
        /// </summary>
        public bool IsPopupPresence = false;

        #endregion

        #region 생성자

        /// <summary>
        /// 생성자
        /// </summary>
        public ucDefectMapBasicForm()
        {
            InitializeComponent();
            InitializeLanguageKey();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 컨트롤러 초기화
        /// </summary>
        private void InitializeControls()
        {
            _rowDataGrid = new SmartBandedGrid();

            #region 네일뷰 타입

            if (DefectMapHelper.IsNull(cboNailViewType.Editor.DataSource))
            {
                flowNailMap.AutoScroll = true;

                DataTable dt = new DataTable();
                dt.Columns.Add("CODEID", typeof(string));
                dt.Columns.Add("CODENAME", typeof(string));

                if (_isItemType)
                {
                    dt.Rows.Add(Format.GetString(ComboType.LOTID), "Lot");
                }
                else
                {
                    dt.Rows.Add(Format.GetString(ComboType.PANELID), "Panel");

                    if (!_equipmentType.Equals(EquipmentType.EQUIPMENTTYPE_BBT))
                    {
                        dt.Rows.Add(Format.GetString(ComboType.LAYERID), "Layer");
                        layoutNailViewType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    }
                }

                cboNailViewType.Editor.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
                cboNailViewType.Editor.ShowHeader = false;
                cboNailViewType.Editor.DisplayMember = "CODENAME";
                cboNailViewType.Editor.ValueMember = "CODEID";
                cboNailViewType.Editor.DataSource = dt;
                cboNailViewType.Editor.ItemIndex = 0;

                _sParam = Format.GetString(cboNailViewType.Editor.EditValue);
            }
            else
            {
                layoutNailViewType.Visibility = _equipmentType.Equals(EquipmentType.EQUIPMENTTYPE_BBT) ?
                                                    DevExpress.XtraLayout.Utils.LayoutVisibility.Never :
                                                    DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }

            #endregion

            #region 설비 타입

            if (DefectMapHelper.IsNull(cboEquipmentType.Editor.DataSource))
            {
                cboEquipmentType.Editor.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
                cboEquipmentType.Editor.ShowHeader = false;
                cboEquipmentType.Editor.DisplayMember = "CODENAME";
                cboEquipmentType.Editor.ValueMember = "CODEID";
                cboEquipmentType.Editor.DataSource = DefectMapHelper.GetCodeListByClassID("DefectMapInspactionType");
                cboEquipmentType.Editor.ItemIndex = _equipmentType.Equals(EquipmentType.EQUIPMENTTYPE_AOI) ? 0 :
                                                    _equipmentType.Equals(EquipmentType.EQUIPMENTTYPE_BBT) ? 1 : 2;

                InitializeEvent();
            }
            else
            {
                cboEquipmentType.Editor.ItemIndex = _equipmentType.Equals(EquipmentType.EQUIPMENTTYPE_AOI) ? 0 :
                                                    _equipmentType.Equals(EquipmentType.EQUIPMENTTYPE_BBT) ? 1 : 2;
            }

            #endregion
        }

        /// <summary>
        /// Language Key 초기화
        /// </summary>
        private void InitializeLanguageKey()
        {
            grdDefectList.LanguageKey = "GRIDDEFECTLIST";
            chkAllCheck.LanguageKey = "GRIDSELECTALL";
            btnRun.LanguageKey = "APPLY";
            cboNailViewType.LanguageKey = "VIEWTYPE";
            gbDefectMap.LanguageKey = "DEFECTMAP";
            gbDefectNail.LanguageKey = "NAILMAP";

            btnFilter.LanguageKey = "FILTER";
            cboEquipmentType.LanguageKey = "INSPECTIONTYPE";
            layoutTop.SetLanguageKey(layoutDefectGroup, "DEFECTGROUP");
            layoutTop.SetLanguageKey(layoutDefectSub, "DEFECTCODE");
            layoutControlNailMap.SetLanguageKey(layoutMirrorLayer, "MIRRORPOINTLAYER");
            chkMirrorImage.LanguageKey = "MIRRORIMAGE";
        }

        /// <summary>
        /// AOI 일 경우 Combo Box 설정
        /// </summary>
        private void InitializeCombo()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CODEID", typeof(string));
            dt.Columns.Add("CODENAME", typeof(string));

            #region Defect Group Combo

            cboDefectGroup.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboDefectGroup.ShowHeader = false;
            cboDefectGroup.DisplayMember = "CODENAME";
            cboDefectGroup.ValueMember = "CODEID";
            cboDefectGroup.EmptyItemCaption = Language.Get("ALLVIEWS");
            cboDefectGroup.EmptyItemValue = "*";
            cboDefectGroup.UseEmptyItem = true;

            //! Real Defect Group Code List
            _totalDataTable.AsEnumerable()
                           .GroupBy(x => new { CODEID = x.Field<string>("GROUPCODE"), CODENAME = x.Field<string>("GROUPNAME") })
                           .OrderBy(x => x.Key.CODEID)
                           .Select(x => new { x.Key.CODEID, x.Key.CODENAME })
                           .ForEach(x =>
                           {
                               dt.Rows.Add(new object[] { x.CODEID, x.CODENAME });
                           });

            cboDefectGroup.DataSource = dt;
            cboDefectGroup.EditValue = "*";

            #endregion

            #region Defect Sub Combo

            DataTable subDt = dt.Clone();
            subDt.Rows.Clear();

            cboDefectSub.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboDefectSub.ShowHeader = false;
            cboDefectSub.DisplayMember = "CODENAME";
            cboDefectSub.ValueMember = "CODEID";
            cboDefectSub.UseEmptyItem = true;
            cboDefectSub.EmptyItemCaption = Language.Get("ALLVIEWS");
            cboDefectSub.EmptyItemValue = "*";
            cboDefectSub.DataSource = subDt;
            cboDefectSub.EditValue = "*";

            #endregion

            #region 좌표반전 Layer

            DataTable layerDt = dt.Clone();
            layerDt.Rows.Clear();

            cboMirrorLayer.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboMirrorLayer.ShowHeader = false;
            cboMirrorLayer.DisplayMember = "CODENAME";
            cboMirrorLayer.ValueMember = "CODEID";
            cboMirrorLayer.UseEmptyItem = true;
            cboMirrorLayer.EmptyItemCaption = Language.Get("NONE");
            cboMirrorLayer.EmptyItemValue = "None";

            _totalDataTable.AsEnumerable()
                           .GroupBy(x => x.Field<string>("LAYERID"))
                           .Select(x => new { CODEID = x.Key, CODENMAE = x.Key })
                           .ForEach(x =>
                           {
                               layerDt.Rows.Add(new object[] { x.CODEID, x.CODENMAE });
                           });

            cboMirrorLayer.DataSource = layerDt;
            cboMirrorLayer.EditValue = "None";

            #endregion
        }

        #endregion

        #region Event

        /// <summary>
        /// 이벤트 초기화
        /// </summary>
        private void InitializeEvent()
        {
            //! 설비 타입 Combo 값 변경시 이벤트
            cboEquipmentType.Editor.EditValueChanged += (s, e) =>
            {
                try
                {
                    DialogManager.ShowWaitArea(this);

                    cboNailViewType.Editor.EditValueChanged -= Editor_EditValueChanged;
                    cboNailViewType.Editor.ItemIndex = 0;
                    _sParam = Format.GetString(cboNailViewType.Editor.EditValue);

                    _equipmentType = Format.GetString(cboEquipmentType.EditValue).Equals("AOI") ? EquipmentType.EQUIPMENTTYPE_AOI :
                                     Format.GetString(cboEquipmentType.EditValue).Equals("BBT") ? EquipmentType.EQUIPMENTTYPE_BBT :
                                                                                                  EquipmentType.EQUIPMENTTYPE_HOLE;

                    SetClose();
                    SetData(_mainDataTable, _equipmentType, _isItemType);

                    cboNailViewType.Editor.EditValueChanged += Editor_EditValueChanged;
                }
                catch (Exception ex)
                {
                    throw MessageException.Create(Format.GetString(ex));
                }
                finally
                {
                    DialogManager.CloseWaitArea(this);
                }
            };

            //! Combo Defect Group 값 변경시 이벤트
            cboDefectGroup.EditValueChanged += (s, e) =>
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("CODEID", typeof(string));
                dt.Columns.Add("CODENAME", typeof(string));

                _totalDataTable.AsEnumerable()
                               .Where(x => cboDefectGroup.GetValuesByList().Contains(x.Field<string>("GROUPCODE")))
                               .GroupBy(x => new { CODEID = x.Field<string>("SUBCODE"), CODENAME = x.Field<string>("SUBNAME") })
                               .OrderBy(x => x.Key.CODEID)
                               .Select(x => new { x.Key.CODEID, x.Key.CODENAME })
                               .ForEach(x =>
                               {
                                   dt.Rows.Add(new object[] { x.CODEID, x.CODENAME });
                               });

                cboDefectSub.DataSource = dt;
            };

            //! Filer 버튼 클릭 이벤트
            btnFilter.Click += (s, e) =>
            {
                if (DefectMapHelper.IsNull(_totalDataTable))
                {
                    return;
                }

                _filterDataTable = _totalDataTable.Clone();

                if (_equipmentType.Equals(EquipmentType.EQUIPMENTTYPE_AOI))
                {
                    if (_equipmentType.Equals(EquipmentType.EQUIPMENTTYPE_AOI))
                    {
                        _filterDataTable = cboDefectGroup.EditValue.Equals("*") ?
                                               _totalDataTable :
                                               cboDefectSub.EditValue.Equals("*") ?
                                                   _totalDataTable.AsEnumerable()
                                                                  .Where(x => cboDefectGroup.GetValuesByList().Contains(x.Field<string>("GROUPCODE")))
                                                                  .CopyToDataTable() :
                                                   _totalDataTable.AsEnumerable()
                                                                  .Where(x => cboDefectGroup.GetValuesByList().Contains(x.Field<string>("GROUPCODE"))
                                                                           && cboDefectSub.GetValuesByList().Contains(x.Field<string>("SUBCODE")))
                                                                  .CopyToDataTable();
                    }
                    else
                    {
                        _filterDataTable = cboDefectSub.EditValue.Equals("*") ?
                                               _totalDataTable :
                                               _totalDataTable.AsEnumerable()
                                                               .Where(x => cboDefectSub.GetValuesByList().Contains(x.Field<string>("SUBCODE")))
                                                               .CopyToDataTable();
                    }
                }

                if (_filterDataTable.Rows.Count.Equals(0))
                {
                    MSGBox.Show(MessageBoxType.Information, "NoSelectData");
                    return;
                }

                chartControlMain.ClearSeries();
                gbDefectMap.Controls.Clear();
                flowNailMap.Controls.Clear();

                DrawingDefectMap(_filterDataTable);
                DrawingNailMap(_filterDataTable);
                DataTable dt = DefectMapHelper.GetDefectAnalysisOfEquipmentType(_filterDataTable, _equipmentType);
                grdDefectList.DataSource = dt;
                SetChart(dt);
            };

            //! Nail Map 전체 선택 체크박스 이벤트
            chkAllCheck.CheckedChanged += (s, e) =>
            {
                if (_equipmentType.Equals(EquipmentType.EQUIPMENTTYPE_BBT))
                {
                    flowNailMap.Controls.Cast<ucBBTDiagram>().ForEach(x => { x.SetCheckMode(chkAllCheck.Checked); });
                }
                else
                {
                    flowNailMap.Controls.Cast<ucAOIDiagram>().ForEach(x => { x.SetCheckMode(chkAllCheck.Checked); });
                }
            };

            //! 도면 이미지 반전 이벤트
            chkMirrorImage.CheckedChanged += (s, e) => SetPcsImageMirror();

            //! Combo View Type 값 변경시 이벤트
            cboNailViewType.Editor.EditValueChanged += Editor_EditValueChanged;

            //! Nail Map 적용 버튼 이벤트
            btnRun.Click += (s, e) =>
            {
                if (DefectMapHelper.IsNull(_totalDataTable))
                {
                    return;
                }

                Dictionary<string, Color> colorList = null;

                DataTable dt = _equipmentType.Equals(EquipmentType.EQUIPMENTTYPE_BBT) ?
                    DefectMapHelper.GetBBTCheckedInNailMap(flowNailMap, _filterDataTable ?? _totalDataTable, _sParam) :
                    DefectMapHelper.GetAOICheckedInNailMap(flowNailMap, _filterDataTable ?? _totalDataTable, _sParam, out colorList);

                if (DefectMapHelper.IsNull(dt))
                {
                    return;
                }

                chartControlMain.ClearSeries();
                gbDefectMap.Controls.Clear();

                _rowDataGrid.DataSource = dt;
                DrawingDefectMap(dt, colorList);
                dt = DefectMapHelper.GetDefectAnalysisOfEquipmentType(dt, _equipmentType);
                grdDefectList.DataSource = dt;
                SetChart(dt);
            };

            //! Defect List Grid Merge 이벤트
            grdDefectList.View.CellMerge += (s, e) =>
            {
                if (!(s is GridView view))
                {
                    return;
                }

                if (e.Column.FieldName.Equals("DEFECTGROUP"))
                {
                    string str1 = view.GetRowCellDisplayText(e.RowHandle1, e.Column);
                    string str2 = view.GetRowCellDisplayText(e.RowHandle2, e.Column);
                    e.Merge = str1.Equals(str2);
                }
                else if (e.Column.FieldName.Equals("DEFECTGROUPCOUNT") || e.Column.FieldName.Equals("DEFECTGROUPRATE"))
                {
                    string str1 = view.GetRowCellDisplayText(e.RowHandle1, view.Columns["DEFECTGROUP"]);
                    string str2 = view.GetRowCellDisplayText(e.RowHandle2, view.Columns["DEFECTGROUP"]);
                    e.Merge = str1.Equals(str2);
                }
                else
                {
                    e.Merge = false;
                }

                e.Handled = true;
            };
        }

        /// <summary>
        /// Nail View Combo 변경이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Editor_EditValueChanged(object sender, EventArgs e)
        {
            _sParam = Format.GetString(cboNailViewType.Editor.EditValue);
            flowNailMap.Controls.Clear();
            DrawingNailMap(_totalDataTable);
        }

        #endregion

        #region Private Function

        /// <summary>
        /// Chart Series 를 구성한다
        /// Chart ViewType이 SideBySideStackedBar으로 Series의 Group과 Item의 구분이 필요하다
        /// </summary>
        /// <param name="dt">DataTable</param>
        private void SetChart(DataTable dt)
        {
            if (DefectMapHelper.IsNull(dt))
            {
                return;
            }

            if (_equipmentType.Equals(EquipmentType.EQUIPMENTTYPE_AOI))
            {
                DefectMapHelper.DrawingSideBySideStackedBarChart(chartControlMain, dt, "DEFECTCODE", "DEFECTGROUP", "DEFECTCOUNT");
                DefectMapHelper.AddSecondarySeriesToLineChart(chartControlMain, dt, "DEFECTGROUP", "DEFECTGROUPRATE");
            }
            else
            {
                DefectMapHelper.DrawingBarChart(chartControlMain, dt, "DEFECTCODE", "DEFECTCOUNT");
                DefectMapHelper.AddSecondarySeriesToLineChart(chartControlMain, dt, "DEFECTCODE", "DEFECTRATE");
            }
        }

        /// <summary>
        /// Main View를 그려준다
        /// </summary>
        /// <param name="dt">Row Data</param>
        private void DrawingDefectMap(DataTable dt, Dictionary<string, Color> colorList = null)
        {
            if (DefectMapHelper.IsNull(dt))
            {
                return;
            }

            if (_equipmentType.Equals(EquipmentType.EQUIPMENTTYPE_BBT))
            {
                gbDefectMap.Controls.Add(DefectMapHelper.DrawingBBTDiagramByMain(dt));
            }
            else
            {
                ucAOIDiagram control = DefectMapHelper.DrawingAOIDiagramByMain(dt, _pcsImage, _nXMax, _nYMax, _sParam, colorList, cboMirrorLayer.GetValuesByList());
                control.PointSelectionEvent += (str) => SelectionPoint(str);
                control.InspectionTypeEvent += (str) => DefectMapHelper.IsDefectTypeIsVRS(_totalDataTable, str);
                gbDefectMap.Controls.Add(control);
                SetPcsImageMirror();
            }
        }

        /// <summary>
        /// Nail View를 그려준다
        /// </summary>
        /// <param name="dt">Row Data</param>
        private void DrawingNailMap(DataTable dt)
        {
            if (DefectMapHelper.IsNull(dt))
            {
                return;
            }

            if (_equipmentType.Equals(EquipmentType.EQUIPMENTTYPE_BBT))
            {
                dt.AsEnumerable()
                  .GroupBy(x => x[_sParam])
                  .OrderBy(x => DefectMapHelper.SortStrNum(Format.GetString(x.Key)))
                  .ForEach(x =>
                  {
                      int nColNum = x.First().Field<int>("ARRAYY");
                      int nRowNum = x.First().Field<int>("ARRAYX");

                      if (_sParam.Equals(Format.GetString(ComboType.LOTID)))
                      {
                          ucBBTDiagram control = new ucBBTDiagram(BBTMode.BBTMODE_LOTNAIL, Format.GetString(x.Key))
                          {
                              Size = new Size((DefectMapHelper.GetBBTWidth(nRowNum) * 20) + 20, (nColNum * 20) + 20),
                              Margin = new Padding(2, 2, 2, 2)
                          };

                          if (!IsPopupPresence)
                          {
                              control.SetContextMenu();
                              control.MapDetailsEvent += () => ShowDefectMapPupupByLot(Format.GetString(x.Key));
                          }

                          DataTable table = new DataTable();
                          for (int i = 0; i < nRowNum; i++)
                          {
                              table.Columns.Add(Format.GetString(i), typeof(string));
                          }

                          DataRow row = table.NewRow();
                          int nSum = 0;
                          int nYCheckPoint = 1;
                          int nXCheckPoint = 1;

                          if (table.Columns.Count.Equals(1))
                          {
                              x.AsEnumerable()
                               .OrderBy(dr => dr.Field<double>("Y"))
                               .ThenBy(dr => dr.Field<double>("X"))
                               .ForEach(dr =>
                               {
                                   if (!DefectMapHelper.StringByDataRowObejct(dr, "DEFECTCODE").Equals("X")
                                    && !DefectMapHelper.StringByDataRowObejct(dr, "DEFECTCODE").Equals("P"))
                                   {
                                       nSum = ++nSum;
                                   }

                                   if (!nYCheckPoint.Equals(DefectMapHelper.IntByDataRowObject(dr, "Y")))
                                   {
                                       row[nXCheckPoint - 1] = nSum;
                                       nYCheckPoint = DefectMapHelper.IntByDataRowObject(dr, "Y");
                                       table.Rows.Add(row);
                                       row = table.NewRow();
                                       nSum = 0;
                                   }
                               });
                          }
                          else
                          {
                              x.AsEnumerable()
                               .OrderBy(dr => dr.Field<double>("Y"))
                               .ThenBy(dr => dr.Field<double>("X"))
                               .ForEach(dr =>
                               {
                                   if (!nXCheckPoint.Equals(DefectMapHelper.IntByDataRowObject(dr, "X")))
                                   {
                                       row[nXCheckPoint - 1] = nSum;
                                       nXCheckPoint = DefectMapHelper.IntByDataRowObject(dr, "X");
                                       nSum = 0;
                                   }

                                   if (!DefectMapHelper.StringByDataRowObejct(dr, "DEFECTCODE").Equals("X")
                                    && !DefectMapHelper.StringByDataRowObejct(dr, "DEFECTCODE").Equals("P"))
                                   {
                                       nSum = ++nSum;
                                   }

                                   if (!nYCheckPoint.Equals(DefectMapHelper.IntByDataRowObject(dr, "Y")))
                                   {
                                       nYCheckPoint = DefectMapHelper.IntByDataRowObject(dr, "Y");
                                       table.Rows.Add(row);
                                       row = table.NewRow();
                                   }
                               });
                          }

                          row[nXCheckPoint - 1] = nSum;
                          table.Rows.Add(row);

                          control.SetData(table);
                          flowNailMap.Controls.Add(control);
                      }
                      else
                      {
                          string firstEquip = DefectMapHelper.StringByDataRowObejct(dt.Rows[0], "EQUIPMENTID");
                          DataTable bbtColumnData = DefectMapHelper.GetBBTDefectStateByEquip(firstEquip);

                          flowNailMap.Controls.Add(DefectMapHelper.DrawingBBTDiagramByNail(x, nRowNum, nColNum, bbtColumnData));
                      }
                  });
            }
            else
            {
                dt.AsEnumerable()
                  .GroupBy(x => x[_sParam])
                  .OrderBy(x => DefectMapHelper.SortStrNum(Format.GetString(x.Key)))
                  .ForEach(x =>
                  {
                      ucAOIDiagram control = DefectMapHelper.DrawingAOIDiagramByNail(x, _nXMax, _nYMax, Format.GetString(x.Key), AOIMode.AOIMODE_NAIL);
                      control.Size = new Size(230, 240);
                      control.Margin = new Padding(2, 2, 2, 2);

                      if (!IsPopupPresence)
                      {
                          control.SetContextMenu(_sParam);

                          if (_sParam.Equals(Format.GetString(ComboType.LOTID)))
                          {
                              control.ShowPopupEvent += () => ShowDefectMapPupupByLot(Format.GetString(x.Key));
                          }
                          else
                          {
                              control.ShowPopupEvent += () =>
                              {
                                  ShowNailViewByMode(dt.AsEnumerable()
                                                       .Where(r => r.Field<string>(_sParam) == Format.GetString(x.Key))
                                                       .CopyToDataTable());
                              };
                          }
                      }

                      flowNailMap.Controls.Add(control);
                  });
            }
        }

        /// <summary>
        /// Diagram에서 선택한 Point의 Data Selection
        /// </summary>
        /// <param name="str">Filter String</param>
        private void SelectionPoint(string str)
        {
            _rowDataGrid.View.ActiveFilterString = str;

            DataTable dt = (_rowDataGrid.DataSource as DataTable).Clone();
            for (int i = 0; i < _rowDataGrid.View.DataRowCount; i++)
            {
                dt.ImportRow(_rowDataGrid.View.GetDataRow(i));
            }

            SetChart(DefectMapHelper.GetDefectAnalysisByAOI(dt));
        }

        /// <summary>
        /// Lot Defect Map 팝업 생성
        /// </summary>
        /// <param name="key">Popup Title</param>
        private void ShowDefectMapPupupByLot(string key)
        {
            try
            {
                DialogManager.ShowWaitArea(this);

                SmartPopupBaseForm popup = new SmartPopupBaseForm()
                {
                    Size = new Size(1485, 800),
                    StartPosition = FormStartPosition.WindowsDefaultBounds,
                    AutoSizeMode = AutoSizeMode.GrowAndShrink,
                    WindowState = FormWindowState.Maximized,
                    Text = Language.Get("DEFECTMAPDETAILSVIEW") + " - " + key
                };

                ucLotDefetMap control = new ucLotDefetMap()
                {
                    Dock = DockStyle.Fill,
                    IsPopupPresence = this.IsPopupPresence
                };

                control.SetTabIndex();
                control.Run(_mainDataTable.AsEnumerable().Where(x => x.Field<string>(_sParam) == key).CopyToDataTable(), _equipmentType);

                popup.Control.Add(control);
                popup.ShowDialog(this);
            }
            catch (Exception ex)
            {
                throw MessageException.Create(Format.GetString(ex));
            }
            finally
            {
                DialogManager.CloseWaitArea(this);
            }
        }

        /// <summary>
        /// Controls View Type 기준으로
        /// Panel일 경우 선택한 Panel에 Layer를 Nail View로 보여준다.
        /// Layer일 경우 선택한 Layer의 Panel을 Nail View로 보여준다.
        /// </summary>
        /// <param name="dt">Row Data</param>
        private void ShowNailViewByMode(DataTable dt)
        {
            try
            {
                DialogManager.ShowWaitArea(this);

                string sFilterStr = _sParam.Equals(Format.GetString(ComboType.PANELID)) ? Format.GetString(ComboType.LAYERID) :
                                                                                          Format.GetString(ComboType.PANELID);
                var list = dt.AsEnumerable()
                             .GroupBy(x => x.Field<string>(sFilterStr))
                             .OrderBy(x => DefectMapHelper.SortStrNum(Format.GetString(x.Key)));

                FlowLayoutPanel layout = new FlowLayoutPanel()
                {
                    Dock = DockStyle.Fill,
                    AutoScroll = true
                };

                list.ForEach(item =>
                {
                    ucAOIDiagram control = DefectMapHelper.DrawingAOIDiagramByNail(item, _nXMax, _nYMax, Format.GetString(item.Key), AOIMode.AOIMODE_POPUPNAIL);
                    control.Size = new Size(280, 400);
                    control.Margin = new Padding(2, 2, 2, 2);
                    layout.Controls.Add(control);
                });

                SmartPopupBaseForm popup = new SmartPopupBaseForm()
                {
                    Size = new Size(1465, 850),
                    StartPosition = FormStartPosition.CenterParent,
                    AutoScroll = true,
                    Text = Language.Get("DEFECTMAPMODEVIEW") + " - " + sFilterStr
                };

                popup.Control.Add(layout);
                popup.ShowDialog(this);
            }
            catch (Exception ex)
            {
                throw MessageException.Create(Format.GetString(ex));
            }
            finally
            {
                DialogManager.CloseWaitArea(this);
            }
        }

        /// <summary>
        /// 상단의 Defect Group/Code 보이기/숨기기
        /// </summary>
        private void SetComboBoxVisibility()
        {
            switch (_equipmentType)
            {
                case EquipmentType.EQUIPMENTTYPE_AOI:
                    InitializeCombo();
                    layoutDefectGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    layoutDefectSub.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    layoutMirrorImage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    layoutMirrorLayer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    layoutFilter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    break;
                case EquipmentType.EQUIPMENTTYPE_BBT:
                    layoutDefectGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    layoutDefectSub.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    layoutMirrorImage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    layoutMirrorLayer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    layoutFilter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    break;
                case EquipmentType.EQUIPMENTTYPE_HOLE:
                    InitializeCombo();
                    layoutDefectGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    layoutDefectSub.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    layoutMirrorImage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    layoutMirrorLayer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    layoutFilter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    break;
            }

            layoutEmpty.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
        }

        /// <summary>
        /// 도면 이미지 반전 이벤트
        /// </summary>
        private void SetPcsImageMirror()
        {
            if (DefectMapHelper.IsNull(_pcsImage))
            {
                return;
            }

            Image img = (Image)_pcsImage.Clone();
            if (chkMirrorImage.Checked)
            {
                img.RotateFlip(RotateFlipType.RotateNoneFlipX);
            }

            (gbDefectMap.Controls[0] as ucAOIDiagram).SetDiagramBackGroundImage(img);
        }

        #endregion

        #region Public Function

        /// <summary>
        /// Row Data로 화면 설정한다
        /// </summary>
        /// <param name="dt">Row Data</param>
        /// <param name="equipmentType">설비 타입</param>
        /// <param name="itemType">품목별일 경우 True / Lot일경우 False</param>
        public void SetData(DataTable dt, EquipmentType equipmentType, bool itemType = false)
        {
            if (_isRun)
            {
                return;
            }

            _isRun = true;
            SetClose();

            if (DefectMapHelper.IsNull(dt) || dt.Rows.Count.Equals(0))
            {
                MSGBox.Show(MessageBoxType.Information, "NoSelectData");
                _isRun = false;
                return;
            }

            _equipmentType = equipmentType;
            _isItemType = itemType;
            _mainDataTable = dt;

            InitializeControls();

            if (DefectMapHelper.GetDefectDataOfEquipmentType(dt, _equipmentType) is DataTable queryDt)
            {
                _totalDataTable = queryDt;
            }
            else
            {
                _isRun = false;
                return;
            }

            SetComboBoxVisibility();

            _pcsImage = _equipmentType.Equals(EquipmentType.EQUIPMENTTYPE_BBT)
                            ? null
                            : DefectMapHelper.GetPcsImage(_totalDataTable, out _nXMax, out _nYMax);

            DataTable table = DefectMapHelper.GetDefectAnalysisOfEquipmentType(_totalDataTable, _equipmentType);

            DefectMapHelper.SetGridColumnByEquipmentType(grdDefectList, _equipmentType);
            grdDefectList.DataSource = table;
            _rowDataGrid.DataSource = _totalDataTable;
            SetChart(table);
            DrawingDefectMap(_totalDataTable);
            DrawingNailMap(_totalDataTable);
            _isRun = false;
        }

        /// <summary>
        /// 화면 초기화
        /// </summary>
        public void SetClose()
        {
            if (!DefectMapHelper.IsNull(_rowDataGrid))
            {
                _rowDataGrid.DataSource = null;
            }

            if (!DefectMapHelper.IsNull(grdDefectList))
            {
                grdDefectList.DataSource = null;
            }

            if (!DefectMapHelper.IsNull(cboDefectGroup.DataSource))
            {
                cboDefectGroup.SelectAll();
            }

            _pcsImage = null;
            _filterDataTable = null;
            grdDefectList.View.ClearColumns();
            chartControlMain.ClearSeries();
            flowNailMap.Controls.Clear();
            gbDefectMap.Controls.Clear();
        }

        #endregion
    }
}
