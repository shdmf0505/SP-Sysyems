#region using
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion

namespace Micube.SmartMES.Commons
{
    /// <summary>
    /// 프 로 그 램 명  : 그리드 툴 삭제 버튼 제어
    /// 업  무  설  명  : 그리드 추가 시에만 삭제 버튼 활성화 
    /// 생    성    자  : 윤성원
    /// 생    성    일  : 2019-07-05
    /// 수  정  이  력  : 
    /// </summary>
    public class SetGridDeleteButonVisible
    {
        Micube.Framework.SmartControls.SmartBandedGrid _grid;
        /// <summary>
        /// 그리드 마이너스버튼숨기기
        /// </summary>
        /// <param name="grid">그리드</param>

        /// <returns></returns>
        public SetGridDeleteButonVisible(Micube.Framework.SmartControls.SmartBandedGrid grid)
        {
            //현재행
            _grid = grid;
            _grid.View.FocusedRowChanged += grid_FocusedRowChanged;
        }

        private void grid_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            // 추가인경우만 delete 버튼을 활성화.
            if (_grid.View.GetFocusedDataRow() != null)
            {
                DataRow row = _grid.View.GetFocusedDataRow();
                if (row.RowState == DataRowState.Added)
                {
                    //_grid.GridButtonItem |= GridButtonItem.Delete; 20191019 윤성원 수정
                    _grid.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Import
                    | GridButtonItem.Export | GridButtonItem.Copy;
                    _grid.Refresh();
                }
                else
                {
                    //_grid.GridButtonItem -= GridButtonItem.Delete; 20191019 윤성원 수정
                    _grid.GridButtonItem = GridButtonItem.Add | GridButtonItem.Import
                     | GridButtonItem.Export | GridButtonItem.Copy;
                    _grid.Refresh();
                }
            }
        }
    }

    public class SetGridDeleteButonVisibleSimple
    {
        Micube.Framework.SmartControls.SmartBandedGrid _grid;
        /// <summary>
        /// 그리드 마이너스버튼숨기기, add,copy,delete 버튼
        /// </summary>
        /// <param name="grid">그리드</param>

        /// <returns></returns>
        public SetGridDeleteButonVisibleSimple(Micube.Framework.SmartControls.SmartBandedGrid grid)
        {
            //현재행
            _grid = grid;
            _grid.View.FocusedRowChanged += grid_FocusedRowChanged;
        }

        private void grid_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

            // 추가인경우만 delete 버튼을 활성화.
            if (_grid.View.GetFocusedDataRow() != null)
            {
                DataRow row = _grid.View.GetFocusedDataRow();
                if (row.RowState == DataRowState.Added)
                {
                    _grid.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export;
                    _grid.Refresh();
                }
                else
                {
                    _grid.GridButtonItem = GridButtonItem.Add | GridButtonItem.Export;
                    _grid.Refresh();
                }
            }
        }
    }

    public class SetGridDeleteButonVisibleOnly
    {
        Micube.Framework.SmartControls.SmartBandedGrid _grid;
        /// <summary>
        /// 그리드 마이너스버튼숨기기, add,copy,delete 버튼
        /// </summary>
        /// <param name="grid">그리드</param>

        /// <returns></returns>
        public SetGridDeleteButonVisibleOnly(Micube.Framework.SmartControls.SmartBandedGrid grid)
        {
            //현재행
            _grid = grid;
            _grid.View.FocusedRowChanged += grid_FocusedRowChanged;
        }

        private void grid_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

            // 추가인경우만 delete 버튼을 활성화.
            if (_grid.View.GetFocusedDataRow() != null)
            {
                DataRow row = _grid.View.GetFocusedDataRow();
                if (row.RowState == DataRowState.Added)
                {
                    _grid.GridButtonItem = GridButtonItem.Delete;
                    _grid.Refresh();
                }
                else
                {
                    _grid.GridButtonItem = GridButtonItem.None;
                    _grid.Refresh();
                }
            }
        }
    }

    public class SetGridDeleteButonVisibleResizeExport
    {
        Micube.Framework.SmartControls.SmartBandedGrid _grid;
        /// <summary>
        /// 그리드 마이너스버튼숨기기, add,copy,delete 버튼
        /// </summary>
        /// <param name="grid">그리드</param>

        /// <returns></returns>
        public SetGridDeleteButonVisibleResizeExport(Micube.Framework.SmartControls.SmartBandedGrid grid)
        {
            //현재행
            _grid = grid;
            _grid.View.FocusedRowChanged += grid_FocusedRowChanged;
        }

        private void grid_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

            // 추가인경우만 delete 버튼을 활성화.
            if (_grid.View.GetFocusedDataRow() != null)
            {
                DataRow row = _grid.View.GetFocusedDataRow();
                if (row.RowState == DataRowState.Added)
                {
                    _grid.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete;
                    _grid.Refresh();
                }
                else
                {
                    _grid.GridButtonItem = _grid.GridButtonItem = GridButtonItem.Add | GridButtonItem.Export | GridButtonItem.Expand | GridButtonItem.Restore;
                    _grid.Refresh();
                }
            }
        }
    }
}
