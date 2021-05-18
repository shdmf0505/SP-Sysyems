#region using

using DevExpress.Data;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.Utils;

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion

namespace Micube.SmartMES.Commons.Controls
{
    public partial class SmartLotInfoGrid : UserControl, ISupportMultiLanguage
    {
        #region Local Variables

        // DataSource
        private object _dataSource;
        // Label Column Width Weight
        private int _labelColumnWidth = 40;

        #endregion

        #region 생성자

        public SmartLotInfoGrid()
        {
            InitializeComponent();
            //gvwMain = new MainView();

            InitializeEvent();

            SetGridColumns(0);
        }

        #endregion

        #region Event

        /// <summary>
        /// SmartLotInfoGrid 컨트롤 내에서 사용하는 이벤트를 초기화한다.
        /// </summary>
        private void InitializeEvent()
        {
            SizeChanged += CustomGrid_SizeChanged;

            gvwMain.MouseWheel += GvwGrid_MouseWheel;
            gvwMain.CustomColumnDisplayText += GvwGrid_CustomColumnDisplayText;
            gvwMain.RowCellStyle += GvwGrid_RowCellStyle;
            gvwMain.MouseDown += GvwMain_MouseDown;
        }

        // Value Field 선택 시 Editor 활성화, 마지막 Row 선택 시 Entire Row 보이지 않게 처리
        private void GvwMain_MouseDown(object sender, MouseEventArgs e)
        {
            GridView view = sender as GridView;
            GridHitInfo info = view.CalcHitInfo(e.Location);

            if (info.InRow && info.RowHandle == view.RowCount - 1)
            {
                view.BeginUpdate();
                view.FocusedRowHandle = info.RowHandle;
                view.FocusedColumn = info.Column;
                view.EndUpdate();

                ((DXMouseEventArgs)e).Handled = true;
            }

            if (info.Column == null) return;

            if (info.Column.FieldName.EndsWith("Value") &&
                !string.IsNullOrWhiteSpace(Format.GetString(view.GetRowCellValue(info.RowHandle, info.Column.FieldName.Replace("Value", "Caption")))))
            {
                view.ShowEditor();
            }
        }

        // 컨트롤 사이즈 변경 시 Row Height 및 Column Width 재 계산
        private void CustomGrid_SizeChanged(object sender, EventArgs e)
        {
            if ((grdMain.DataSource as DataTable).Rows.Count > 0)
            {
                CalcRowHeight();
                CalcColumnWidth();
            }
        }

        // 그리드의 마우스 휠 이벤트 제거
        private void GvwGrid_MouseWheel(object sender, MouseEventArgs e)
        {
            (e as DXMouseEventArgs).Handled = true;
        }

        // 캡션 영역 컬럼에 보여주는 Text 다국어 처리
        private void GvwGrid_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.Name.EndsWith("Caption") && !string.IsNullOrWhiteSpace(e.Value.ToString()))
            {
                e.DisplayText = Language.Get(e.Value.ToString());
            }
        }

        // 캡션 영역 컬럼 색상 변경
        private void GvwGrid_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.Column.Name.EndsWith("Caption") && !string.IsNullOrWhiteSpace(e.CellValue.ToString()))
                e.Appearance.BackColor = Color.FromArgb(218, 238, 243);
        }

        #endregion

        #region Property

        /// <summary>
        /// 컬럼 개수를 몇 개 씩 보여줄 것인지 설정한다.
        /// </summary>
        [DefaultValue(5)]
        public int ColumnCount { get; set; } = 5;

        /// <summary>
        /// 라벨 컬럼의 넓이 비율을 설정한다.
        /// </summary>
        [DefaultValue("40%")]
        public string LabelWidthWeight
        {
            get
            {
                return GetColumnWidth();
            }
            set
            {
                SetColumnWidth(value);
            }
        }

        /// <summary>
        /// SmartLotInfoGrid 컨트롤의 데이터를 설정한다.
        /// </summary>
        [DefaultValue(null)]
        public object DataSource
        {
            get
            {
                return _dataSource;
            }
            set
            {
                SetGridColumns(ColumnCount);


                DataTable gridDataSource = grdMain.DataSource as DataTable;

                if (value is DataTable dataSource)
                {
                    DataRow newRow = gridDataSource.NewRow();

                    int cntVisibleFields = 0;

                    for (int i = 0; i < dataSource.Columns.Count; i++)
                    {
                        if (InvisibleFields.Contains(dataSource.Columns[i].ColumnName))
                            continue;

                        string captionName = "Column" + ((cntVisibleFields % ColumnCount) + 1).ToString() + "Caption";
                        string valueName = "Column" + ((cntVisibleFields % ColumnCount) + 1).ToString() + "Value";

                        newRow[captionName] = dataSource.Columns[i].ColumnName;

                        object gridValue;

                        if (dataSource.Rows.Count > 0)
                        {
                            Type type = dataSource.Columns[i].DataType;

                            if (type == typeof(double) || type == typeof(decimal))
                                gridValue = Format.GetDecimal(dataSource.Rows[0][dataSource.Columns[i]]).ToString("#,##0.00");
                            else if (type == typeof(int))
                                gridValue = Format.GetInteger(dataSource.Rows[0][dataSource.Columns[i]]).ToString("#,##0");
                            else
                                gridValue = dataSource.Rows[0][dataSource.Columns[i]];
                        }
                        else
                        {
                            gridValue = null;
                        }

                        newRow[valueName] = gridValue;

                        cntVisibleFields++;

                        if (cntVisibleFields % ColumnCount == 0)
                        {
                            gridDataSource.Rows.Add(newRow);

                            newRow = gridDataSource.NewRow();
                        }
                    }

                    if (cntVisibleFields % ColumnCount != 0)
                        gridDataSource.Rows.Add(newRow);

                    gridDataSource.AcceptChanges();
                }

                grdMain.DataSource = gridDataSource;

                CalcRowHeight();
                CalcColumnWidth();

                _dataSource = value;
            }
        }

        private List<string> InvisibleFields { get; set; } = new List<string>();

        #endregion

        #region Public Function

        /// <summary>
        /// 특정 필드의 값을 반환한다.
        /// </summary>
        /// <param name="fieldName">필드명</param>
        /// <returns>필드 값</returns>
        public object GetFieldValue(string fieldName)
        {
            object value = new object();

            DataTable dataSource = _dataSource as DataTable;

            if (dataSource.Rows.Count > 0 && dataSource.Columns.Contains(fieldName))
            {
                return dataSource.Rows[0][fieldName];
            }

            //for (int i = 0; i < gvwMain.RowCount; i++)
            //{
            //    foreach (GridColumn column in gvwMain.Columns)
            //    {
            //        if (gvwMain.GetRowCellValue(i, column).Equals(fieldName))
            //            return gvwMain.GetRowCellValue(i, column.Name.Replace("Caption", "Value"));
            //    }
            //}

            throw MessageException.Create($"{fieldName} 필드는 DataSource에 존재하지 않습니다.");
        }

        public void ClearData()
        {
            DataTable dataSource = grdMain.DataSource as DataTable;
            dataSource.Rows.Clear();
            dataSource.AcceptChanges();

            gvwMain.Columns.Clear();
        }

        public void SetInvisibleFields(params string[] fieldNames)
        {
            InvisibleFields.Clear();

            fieldNames.ForEach(value =>
            {
                InvisibleFields.Add(value);
            });
        }

        #endregion

        #region Private Function

        /// <summary>
        /// 컬럼 개수에 따라 컬럼을 구성한다.
        /// </summary>
        /// <param name="count">컬럼 개수</param>
        private void SetGridColumns(int count)
        {
            gvwMain.Columns.Clear();
            DataTable table = new DataTable();

            int columnsCount = count * 2;

            for (int i = 0; i < columnsCount; i++)
            {
                string fieldName = "Column" + ((i / 2) + 1).ToString() + (i % 2 == 0 ? "Caption" : "Value");

                GridColumn column = new GridColumn();
                column.Name = fieldName;
                column.FieldName = fieldName;
                column.VisibleIndex = i;
                column.Visible = true;

                if (fieldName.EndsWith("Caption"))
                    column.OptionsColumn.AllowEdit = false;
                else if (fieldName.EndsWith("Value"))
                    column.OptionsColumn.AllowEdit = true;

                gvwMain.Columns.Add(column);

                table.Columns.Add(fieldName, typeof(string));
            }

            table.AcceptChanges();

            grdMain.DataSource = table;
        }

        /// <summary>
        /// 라벨 컬럼 넓이 비율을 반환한다.
        /// </summary>
        /// <returns>넓이 비율</returns>
        private string GetColumnWidth()
        {
            return _labelColumnWidth + "%";
        }

        /// <summary>
        /// 라벨 컬럼 넓이 비율을 설정한다.
        /// </summary>
        /// <param name="value">넓이 비율</param>
        private void SetColumnWidth(string value)
        {
            int width;

            int.TryParse(value.TrimEnd('%'), out width);

            _labelColumnWidth = width;
        }

        /// <summary>
        /// 컨트롤 크기에 따른 행 높이를 계산하여 설정한다.
        /// </summary>
        private void CalcRowHeight()
        {
            SuspendLayout();

            int rowCount = (grdMain.DataSource as DataTable).Rows.Count;
            int height = (grdMain.Bounds.Height - (rowCount + 1)) / rowCount;

            gvwMain.RowHeight = height;

            ResumeLayout();
        }

        /// <summary>
        /// 컨트롤 크기와 라벨 컬럼 넓이 비율에 따라 컬럼 별 넓이를 계산하여 설정한다.
        /// </summary>
        private void CalcColumnWidth()
        {
            SuspendLayout();

            int width = grdMain.Bounds.Width / ColumnCount;

            int captionWidth = (int)(width * ((double)_labelColumnWidth / 100));
            int valueWidth = width - captionWidth;

            foreach (GridColumn column in gvwMain.Columns)
            {
                if (column.Name.EndsWith("Caption"))
                    column.Width = captionWidth;
                else if (column.Name.EndsWith("Value"))
                    column.Width = valueWidth;
            }

            ResumeLayout();
        }

        #endregion

        #region ISupportMultiLanguage

        public void ChangeLanguage()
        {
            gvwMain.LayoutChanged();
        }

        #endregion
    }

    class MainView : GridView
    {
        private bool lockMakeRowVisible = false;

        protected override void OnCurrentControllerRowChanged(CurrentRowEventArgs e)
        {
            if (!IsInListChangedEvent) lockMakeRowVisible = true;
            base.OnCurrentControllerRowChanged(e);
            lockMakeRowVisible = false;
        }

        protected override void MakeRowVisibleCore(int rowHandle, bool invalidate)
        {
            if (lockMakeRowVisible) return;
            base.MakeRowVisibleCore(rowHandle, invalidate);
        }
    }
}