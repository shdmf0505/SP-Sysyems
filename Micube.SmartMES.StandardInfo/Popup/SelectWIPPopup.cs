using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Micube.SmartMES.Commons;

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프로그램명 : 기준정보 > 사양관리 >  표준공정 사용조회 - 공정 선택 POPUP
    /// 업 무 설명 : 공정 멀티 선택
    /// 생  성  자 : 조혜인
    /// 생  성  일 : 2020-02-13
    /// 수정 이 력 : 
    /// 
    /// 
    /// </summary> 
    public partial class SelectWIPPopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region Interface

        public DataRow CurrentDataRow { get; set; }

        

        /// <summary>
        /// 선택한 Row 전달
        /// </summary>
        /// <param name="dr"></param>
        public delegate void SelectedRowHandler(DataRow dr);
        public event SelectedRowHandler SelectedRowEvent;

        #endregion

        #region Local Variables
        #endregion

        #region 생성자
        public SelectWIPPopup()
        {
            InitializeComponent();
            InitializeGrid();
            InitializeEvent();
            InitializeControl();
        }
        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// Grid 초기화
        /// </summary>
        private void InitializeGrid()
        {
            grdWip.GridButtonItem = GridButtonItem.None;
            grdWip.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            
            grdWip.View.AddTextBoxColumn("PROCESSSEGMENTID", 100)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left);
            
            grdWip.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            
            grdWip.View.PopulateColumns();

            grdSelectedWIP.GridButtonItem = GridButtonItem.None;
            grdSelectedWIP.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdSelectedWIP.View.AddTextBoxColumn("PROCESSSEGMENTID", 100)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left);

            grdSelectedWIP.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdSelectedWIP.View.PopulateColumns();

        }
        #endregion

        #region 컨텐츠 영역 초기화
       
        private void InitializeControl()
        {
            ucDataLeftRightBtn1.SourceGrid = grdWip;
            ucDataLeftRightBtn1.TargetGrid = grdSelectedWIP;
            this.AcceptButton = btnOK;

            Search();
        }
        #endregion

        #region Event

        /// <summary>
        /// Event 초기화 
        /// </summary>
        private void InitializeEvent()
        {

            this.grdWip.View.DoubleClick += grdWipView_DoubleClick;
            this.grdSelectedWIP.View.DoubleClick += grdSelectedWIPView_DoubleClick;
            btnOK.Click += (s, e) =>
            {
                var dv = grdSelectedWIP.View.DataSource as DataView;
                if (dv == null) return;
                List<string> psList = new List<string>();
                foreach (DataRow dataRow in dv.Table.Rows)
                {
                    psList.Add(dataRow[0].ToString());
                }
                DataRow newRow = CurrentDataRow.Table.NewRow();
                newRow[0] = string.Join(",", psList);
                CurrentDataRow = newRow;

                this.DialogResult = DialogResult.OK;
                this.FireSelected(this.grdSelectedWIP.View);
                this.SelectedRowEvent(CurrentDataRow);
                
                this.Close();
            };
        }

        private void grdSelectedWIPView_DoubleClick(object sender, EventArgs e)
        {
            CommonFunction.SetGridDoubleClickCheck(grdSelectedWIP, sender);
            ucDataLeftRightBtn1.SetDataMove(grdSelectedWIP, grdWip);
        }

        private void grdWipView_DoubleClick(object sender, EventArgs e)
        {
            CommonFunction.SetGridDoubleClickCheck(grdWip, sender);
            ucDataLeftRightBtn1.SetDataMove(grdWip, grdSelectedWIP);
        }



        #endregion

        #region Public Function


        #endregion

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                param.Add("PROCESSSEGMENTID", txtSearch.Text);

                var dt = SqlExecuter.Query("GetProcessSegmentListByOsp", "10002", param);
                grdWip.DataSource = dt;
            }
            else
            {
                Search();
            }
            
        }

        private void Search()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);

            var dt = SqlExecuter.Query("GetProcessSegmentListByOsp", "10002", param);
            grdWip.DataSource = dt;
        }

        public void SetParams(string param)
        {
            if (string.IsNullOrEmpty(param))
                return;

            Search();

            txtSearch.Text = param;

            var splitParams = param.Split(',');

            foreach (string s in splitParams)
            {
                int idx = grdWip.View.GetRowHandleByValue("PROCESSSEGMENTID", s);
                if (idx >= 0) grdWip.View.RemoveRow(idx);
            }
            
            Dictionary<string, object> paramDictionary = new Dictionary<string, object>();
            paramDictionary.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            paramDictionary.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            paramDictionary.Add("PROCESSSEGMENTID", txtSearch.Text);

            var dt = SqlExecuter.Query("GetProcessSegmentListByOsp", "10002", paramDictionary);
            grdSelectedWIP.DataSource = dt;

           // Search();
            


        }
    }
}
