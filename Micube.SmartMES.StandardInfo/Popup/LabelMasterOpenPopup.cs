#region using
using DevExpress.XtraReports.UI;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
#endregion

namespace Micube.SmartMES.StandardInfo.Popup
{
    /// <summary>
    /// 프 로 그 램 명  : 기준정보 > 품질 기준 정보 > SPEC 등록 화면내의 팝업
    /// 업  무  설  명  : SPEC 등록 화면의 그리드를 더블 클릭하여 상세 스펙을 관리한다.
    /// 생    성    자  : 강유라
    /// 생    성    일  : 2019-05-14
    /// 수  정  이  력  : 윤성원 2019-07-05 using 에 #region #endregion 추가
    /// 
    /// 
    /// </summary>
    public partial class LabelMasterOpenPopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region Local Variables
        public DataRow CurrentDataRow { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public delegate void OnLoadLabelDataEventHandler(DataRow loadDataRow);
        public event OnLoadLabelDataEventHandler OnLoadLabel;

        #endregion

        #region 생성자
        public LabelMasterOpenPopup()
        {
            InitializeComponent();

            if (!this.IsDesignMode())
            {
                InitializeControl();
                InitializeGrid();
                InitializeEvent();
            }

        }
        #endregion

        #region 컨텐츠 영역 초기화


        #endregion

        #region Event
        private void InitializeControl()
        {
            this.grdLabelDefList.ButtonBarVisible(false);
        }
        private void InitializeGrid()
        {
            this.grdLabelDefList.View.SetIsReadOnly(true);
            this.grdLabelDefList.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;
            this.grdLabelDefList.View.OptionsSelection.EnableAppearanceFocusedRow = false;

            //라벨명
            this.grdLabelDefList.View.AddTextBoxColumn("LABELDEFNAME", 250);
            //라벨 그룹

            this.grdLabelDefList.View.PopulateColumns();
        }
        private void InitializeEvent()
        {
            this.Load += LabelMasterOpenPopup_Load;
            this.btnLoad.Click += BtnLoad_Click;
            this.btnCancel.Click += BtnCancel_Click;
            this.grdLabelDefList.View.FocusedRowChanged += View_FocusedRowChanged;
        }

        private void BtnLoad_Click(object sender, EventArgs e)
        {
            if (this.OnLoadLabel != null)
            {
                DataRow focusedRow = this.grdLabelDefList.View.GetFocusedDataRow();

                this.OnLoadLabel(focusedRow);

                this.DialogResult = DialogResult.OK;
            }

            this.Close();

        }

        private void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if(e.FocusedRowHandle > -1)
            {
                DataTable dt = this.grdLabelDefList.DataSource as DataTable;

                if (dt != null)
                    SetBindingPreview(dt.Rows[e.FocusedRowHandle]);
            }
        }

        private void LabelMasterOpenPopup_Load(object sender, EventArgs e)
        {


        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        #endregion

        #region 검색


        #endregion

        #region Private Function


        private void SetBindingPreview(DataRow selectRow)
        {


            XtraReport report = new XtraReport();

            this.documentViewer1.DocumentSource = null;


            byte[] labelData = selectRow["LABELDATA"] as byte[];

            using (MemoryStream ms = new MemoryStream(labelData))
            {

                ms.Write(labelData, 0, labelData.Length);

                report = XtraReport.FromStream(ms);
            }

            Band topBand = report.Bands.GetBandByType(typeof(TopMarginBand));
            Band bottomBand = report.Bands.GetBandByType(typeof(BottomMarginBand));

            if(topBand != null)
                report.Bands.Remove(topBand);
            if(bottomBand != null)
                report.Bands.Remove(bottomBand);

            Band detailBand = report.Bands.GetBandByType(typeof(DetailBand));


            Conditionals cd = new Conditionals();
            //detailBand.Controls
            foreach (XRControl control in detailBand.Controls)
            {
                if (control is DevExpress.XtraReports.UI.XRLabel)
                {
                    if (!string.IsNullOrEmpty(control.Tag.ToString()))
                    {
                        cd.dynamicProperties.Add(control.Tag.ToString(), control.Text);
                    }


                }
                else if (control is DevExpress.XtraReports.UI.XRBarCode)
                {
                    if (!string.IsNullOrEmpty(control.Tag.ToString()))
                    {
                        cd.dynamicProperties.Add(control.Tag.ToString(), control.Text);
                    }

                }
                else if (control is DevExpress.XtraReports.UI.XRTable)
                {
                    XRTable xt = control as XRTable;

                    foreach (XRTableRow tr in xt.Rows)
                    {
                        for (int i = 0; i < tr.Cells.Count; i++)
                        {
                            if (!string.IsNullOrEmpty(tr.Cells[i].Tag.ToString()))
                            {
                                cd.dynamicProperties.Add(tr.Cells[i].Tag.ToString(), tr.Cells[i].Text);
                            }
                        }
                    }
                }
            }



            report.CreateDocument();
            this.documentViewer1.DocumentSource = report;
            this.documentViewer1.Zoom = 0.5f;

            this.documentViewer1.PrintingSystem.ExecCommand(DevExpress.XtraPrinting.PrintingSystemCommand.ZoomToPageWidth);


        }


        #endregion

        #region Public function

        public void SetLabelDefDataTable(DataTable dtLabelDefList)
        {
            this.grdLabelDefList.DataSource = dtLabelDefList;
        }

        #region ◆ Function |



        #endregion

        #endregion
    }
}
