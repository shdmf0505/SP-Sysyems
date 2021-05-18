#region using
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons;

#endregion

namespace Micube.SmartMES.Commons.Controls
{
    /// <summary>
    /// 프 로 그 램 명  : 공통 > Grid의 데이터 Up / Down할 수 있는 버튼 Control
    /// 업  무  설  명  : 
    /// 생    성    자  : 박정훈
    /// 생    성    일  : 2019-07-25
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ucDataLeftRightBtn : UserControl
    {
        // Button 상태
        private string _buttonState;

        public SmartBandedGrid SourceGrid { get; set; }
        public SmartBandedGrid TargetGrid { get; set; }

        /// <summary>
        /// 버튼 상태값
        /// </summary>
        public string ButtonState
        {
            get
            {
                return this._buttonState;
            }
        }

        public ucDataLeftRightBtn()
        {
            InitializeComponent();

            this.Load += UcDataLeftRightBtn_Load;
        }

        private void UcDataLeftRightBtn_Load(object sender, EventArgs e)
        {
            // Button Event
            btnDataRight.Click += BtnDataRight_Click;
            btnDataLeft.Click += BtnDataLeft_Click;
        }

        #region ◆ Event |

        #region ▶ Button Event |
        /// <summary>
        /// Data Down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDataRight_Click(object sender, EventArgs e)
        {
            this._buttonState = "Right";
            buttonClick?.Invoke(sender, e);

            SetDataMove(SourceGrid, TargetGrid);
        }

        /// <summary>
        /// Data Up (Remove)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDataLeft_Click(object sender, EventArgs e)
        {
            this._buttonState = "Left";
            buttonClick?.Invoke(sender, e);

            SetDataMove(TargetGrid, SourceGrid);
        }
        #endregion

        #endregion

        #region ◆ Public Event Handler |

        public event EventHandler buttonClick;
        #endregion

        #region ◆ Function |

        #region ▶ SetDataMove :: 선택된 데이터 데이터 복사 이동 |
        /// <summary>
        /// 선택된 데이터 데이터 복사 이동
        /// </summary>
        /// <param name="sourceGrid">원본 Grid</param>
        /// <param name="targetGrid">대상 Grid</param>
        public void SetDataMove(SmartBandedGrid sourceGrid, SmartBandedGrid targetGrid)
        {
            List<DataRowView> listAddRows = new List<DataRowView>();

            DataTable sourceData = (DataTable)sourceGrid.DataSource;
            DataTable targetData = (DataTable)targetGrid.DataSource;

            targetData = SetInitDataTable(sourceData);

            if (targetGrid.DataSource == null) targetGrid.DataSource = targetData;

            // Check Checked Data Row
            for (int i = sourceGrid.View.RowCount - 1; i >= 0; i--)
            {
                if (!sourceGrid.View.IsRowChecked(i))
                    continue;

                DataRowView row = sourceGrid.View.GetRow(i) as DataRowView;

                listAddRows.Add(row);
            }

            // Loop For data copy
            foreach (DataRowView row in listAddRows)
            {
                DataRowView newRow = (targetGrid.View.DataSource as DataView).AddNew();
                newRow.BeginEdit();
                foreach (DataColumn dCol in targetData.Columns)
                {
                    if (!newRow.DataView.Table.Columns.Contains(dCol.ColumnName)) continue;

                    newRow.Row[dCol.ColumnName] = row.Row[dCol.ColumnName];
                }
                //newRow.Row.ItemArray = row.Row.ItemArray.Clone() as object[];

                row.Delete();
                newRow.EndEdit();
            }

            sourceData.AcceptChanges();
            targetData.AcceptChanges();
        }
        #endregion

        #region ▶ SetInitDataTable :: Inittialize DataTable Column |
        /// <summary>
        /// Inittialize DataTable Column
        /// </summary>
        /// <param name="sourcedt">Source DataTable</param>
        /// <returns></returns>
        private DataTable SetInitDataTable(DataTable sourcedt)
        {
            DataTable dt = new DataTable();

            foreach (DataColumn col in sourcedt.Columns)
            {
                if (dt.Columns.Contains(col.ColumnName))
                    continue;

                dt.Columns.Add(col.ColumnName.ToString(), col.DataType);
            }

            return dt;
        }
        #endregion 
        #endregion
    }
}
