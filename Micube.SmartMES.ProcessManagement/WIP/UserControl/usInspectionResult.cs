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

using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
#endregion

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 공통 > 자주검사 이력 조회용 공통 사용자 정의 Control
    /// 업  무  설  명  : 
    /// 생    성    자  : 박정훈
    /// 생    성    일  : 2020-02-14
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class usInspectionResult : UserControl
    {
        #region ◆ Variables
        /// <summary>
        /// LotID
        /// </summary>
        private string _lotid;
        #endregion
        
        #region ◆ Public Properties
        
        /// <summary>
        /// LotID
        /// </summary>
        public string LotID
        {
            get
            {
                return _lotid;
            }
            set
            {
                _lotid = value;
            }
        }
        /// <summary>
        /// 자주검사 이력 정보
        /// </summary>
        public DataTable InspectionData
        {
            get
            {
                return this.grdInspect.DataSource as DataTable;
            }
            set
            {
                this.grdInspect.DataSource = value;
            }
        }

        /// <summary>
        /// Defect Split Container
        /// </summary>
        public SmartSpliterContainer DefectSplitContainer
        {
            get
            {
                return defectSpliterContainer;
            }
        }
        #endregion

        #region ◆ 생성자 |
        /// <summary>
        /// 생성자
        /// </summary>
        public usInspectionResult()
        {
            InitializeComponent();
        }
        #endregion

        #region ◆ Control 초기화 |
        /// <summary>
        /// Control 초기화
        /// </summary>
        public void InitializeControls()
        {
            InitializeGrid();

            InitializeEvent();
        }

        #region ▶ Grid Control 초기화 |
        /// <summary>
        /// Grid Control 초기화
        /// </summary>
        private void InitializeGrid()
        {
            #region - 자주검사 이력 |
            grdInspect.GridButtonItem = GridButtonItem.None;

            grdInspect.View.SetIsReadOnly();

            grdInspect.View.AddTextBoxColumn("LOTID", 170).SetTextAlignment(TextAlignment.Center);
            grdInspect.View.AddTextBoxColumn("USERSEQUENCE", 80).SetTextAlignment(TextAlignment.Center);
            grdInspect.View.AddTextBoxColumn("PROCESSSEGMENTID", 150).SetIsHidden();
            grdInspect.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 180);
            grdInspect.View.AddTextBoxColumn("WORKCOUNT", 80).SetIsHidden();
            grdInspect.View.AddTextBoxColumn("AREAID", 70).SetIsHidden();
            grdInspect.View.AddTextBoxColumn("AREANAME", 120);
            grdInspect.View.AddTextBoxColumn("INSPECTIONDEFID", 70).SetIsHidden();
            grdInspect.View.AddTextBoxColumn("INSPECTIONDEFNAME", 120);
            grdInspect.View.AddTextBoxColumn("DEGREE", 60).SetTextAlignment(TextAlignment.Center);
            grdInspect.View.AddTextBoxColumn("INSPECTIONDATE", 150).SetTextAlignment(TextAlignment.Center);
            grdInspect.View.AddTextBoxColumn("INSPECTIONUSER", 100).SetTextAlignment(TextAlignment.Center);
            grdInspect.View.AddTextBoxColumn("INSPECTIONQTY", 100).SetDisplayFormat("{0:#,###}").SetTextAlignment(TextAlignment.Right);
            grdInspect.View.AddTextBoxColumn("INSPECTPNLQTY", 100).SetIsHidden();
            grdInspect.View.AddTextBoxColumn("DEFECTQTY", 100).SetDisplayFormat("{0:#,###}").SetTextAlignment(TextAlignment.Right);
            grdInspect.View.AddTextBoxColumn("DEFECTPICTURE", 80);

            grdInspect.View.PopulateColumns();
            #endregion

            #region - 자주검사 불량 정보
            grdInspDefect.GridButtonItem = GridButtonItem.None;

            grdInspDefect.View.SetIsReadOnly();

            grdInspDefect.View.AddTextBoxColumn("LOTID", 170).SetTextAlignment(TextAlignment.Center);
            grdInspDefect.View.AddTextBoxColumn("USERSEQUENCE", 80).SetTextAlignment(TextAlignment.Center);
            grdInspDefect.View.AddTextBoxColumn("PROCESSDEFID", 150).SetIsHidden();
            grdInspDefect.View.AddTextBoxColumn("PROCESSDEFVERSION", 150).SetIsHidden();
            grdInspDefect.View.AddTextBoxColumn("WORKCOUNT", 150).SetIsHidden();
            grdInspDefect.View.AddTextBoxColumn("PROCESSSEGMENTID", 150).SetIsHidden();
            grdInspDefect.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150);
            grdInspDefect.View.AddTextBoxColumn("INSPECTIONDEFID", 120).SetIsHidden();
            grdInspDefect.View.AddTextBoxColumn("INSPECTIONDEFNAME", 120);
            grdInspDefect.View.AddTextBoxColumn("DEGREE", 60).SetTextAlignment(TextAlignment.Center);
            grdInspDefect.View.AddTextBoxColumn("DEFECTCODE", 60).SetTextAlignment(TextAlignment.Center);
            grdInspDefect.View.AddTextBoxColumn("DEFECTCODENAME", 100);
            grdInspDefect.View.AddTextBoxColumn("QCSEGMENTID", 60).SetTextAlignment(TextAlignment.Center);
            grdInspDefect.View.AddTextBoxColumn("QCSEGMENTNAME", 100);
            grdInspDefect.View.AddTextBoxColumn("DEFECTQTY", 100).SetDisplayFormat("{0:#,###}").SetTextAlignment(TextAlignment.Right);
            grdInspDefect.View.AddTextBoxColumn("REASONCONSUMABLEDEFID", 100).SetTextAlignment(TextAlignment.Center);
            grdInspDefect.View.AddTextBoxColumn("REASONCONSUMABLEDEFNAME", 150).SetLabel("REASONPRODUCTDEFNAME");
            grdInspDefect.View.AddTextBoxColumn("REASONCONSUMABLELOTID", 180).SetTextAlignment(TextAlignment.Center);
            grdInspDefect.View.AddTextBoxColumn("REASONSEGMENT", 120);
            grdInspDefect.View.AddTextBoxColumn("REASONAREA", 150);
            grdInspDefect.View.AddTextBoxColumn("FILERESOURCEID", 100).SetIsHidden();

            grdInspDefect.View.PopulateColumns();
            #endregion

            #region - File Grid |
            grdFile.GridButtonItem = GridButtonItem.None;
            grdFile.View.SetIsReadOnly();
            grdFile.ShowStatusBar = false;

            grdFile.View.AddTextBoxColumn("FILENAME", 150);
            grdFile.View.AddTextBoxColumn("FILEEXT", 60).SetTextAlignment(TextAlignment.Center);
            grdFile.View.AddSpinEditColumn("FILESIZE", 60).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetTextAlignment(TextAlignment.Right);
            grdFile.View.AddTextBoxColumn("FILEPATH", 100).SetIsHidden();
            grdFile.View.AddTextBoxColumn("URL").SetIsHidden();

            grdFile.View.PopulateColumns();
            #endregion
        }
        #endregion

        #region ▶ Event 설정 |
        /// <summary>
        /// Control Event 설정
        /// </summary>
        public void InitializeEvent()
        {
            grdInspect.View.FocusedRowChanged += GrdInspView_FocusedRowChanged;
            grdInspect.View.RowCellClick += GrdInspView_RowCellClick;
            grdInspDefect.View.RowCellClick += GrdInspDefectView_RowCellClick;
            grdInspDefect.View.FocusedRowChanged += GrdInspDefectView_FocusedRowChanged;
            grdFile.View.FocusedRowChanged += GrdFileView_FocusedRowChanged;
            grdFile.View.RowCellClick += GrdFileView_RowCellClick;
        }
        #endregion

        #endregion

        #region ◆ Event |

        #region - 검사 정보 선택 :: FocusedRowChanged |
        /// <summary>
        /// 검사 정보 선택
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdInspView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = grdInspect.View.GetFocusedDataRow();

            if (dr == null) return;

            GetInspectionDefect(dr);
        }
        #endregion

        #region - 검사 데이터 Row click :: RowCellClick |
        /// <summary>
        /// 검사 데이터 Row click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdInspView_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            DataRow dr = grdInspect.View.GetFocusedDataRow();

            if (dr == null) return;

            GetInspectionDefect(dr);
        }
        #endregion

        #region - 불량 Grid RowCellClick |
        /// <summary>
        /// 불량 Grid RowCellClick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdInspDefectView_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            DataRow dr = grdInspDefect.View.GetFocusedDataRow();

            if (dr == null)
            {
                grdFile.View.ClearDatas();
                return;
            }

            getFileData(dr);
        }
        #endregion

        #region - 불량 Grid FocusedRowChanged |
        /// <summary>
        /// 불량 Grid FocusedRowChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdInspDefectView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = grdInspDefect.View.GetFocusedDataRow();

            if (dr == null)
            {
                grdFile.View.ClearDatas();
                return;
            }

            getFileData(dr);
        }
        #endregion

        #region - File Grid FocusedRowChanged Event |
        /// <summary>
        /// File Grid FocusedRowChanged Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdFileView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            getFile();
        }
        #endregion

        #region - File Grid RowCellClick Event |
        /// <summary>
        /// File Grid RowCellClick Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdFileView_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            getFile();
        }
        #endregion

        #endregion

        #region ◆ Public Function |

        /// <summary>
        /// 검사 데이터 조회 및 Data Binding
        /// </summary>
        public void SearchInspectionData(string lotid = "")
        {
            if (!string.IsNullOrWhiteSpace(lotid))
                _lotid = lotid;

            if (string.IsNullOrWhiteSpace(_lotid))
                return;

            // Lot Defect 조회
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("LOTID", _lotid);

            grdInspect.DataSource = SqlExecuter.Query("SelectSelfInspectHisForFinalInspect", "10001", param);
        }

        #endregion


        #region ◆ Private Function |

        #region ▶ 검사데이터 불량 정보 조회 :: GetInspectionDefect |
        /// <summary>
        /// 검사데이터 불량 정보 조회
        /// </summary>
        /// <param name="dr"></param>
        private void GetInspectionDefect(DataRow dr)
        {
            grdInspDefect.View.ClearDatas();

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("LOTID", dr["LOTID"].ToString());
            param.Add("PROCESSDEFID", dr["PROCESSDEFID"].ToString());
            param.Add("PROCESSDEFVERSION", dr["PROCESSDEFVERSION"].ToString());
            param.Add("PROCESSSEGMENTID", dr["PROCESSSEGMENTID"].ToString());
            param.Add("WORKCOUNT", dr["WORKCOUNT"].ToString());
            param.Add("INSPECTIONDEFID", dr["INSPECTIONDEFID"].ToString());
            param.Add("DEGREE", dr["DEGREE"].ToString());

            if (dr["INSPECTIONDEFID"].ToString() == "BBTInspection" || dr["INSPECTIONDEFID"].ToString() == "AOIInspection")
                grdInspDefect.DataSource = SqlExecuter.Query("SelectLotHistoryAOIBBTInspectionDefectPop", "10001", param);
            else
                grdInspDefect.DataSource = SqlExecuter.Query("SelectLotHistoryInspectionDefectPop", "10001", param);
        }
        #endregion

        #region ▶ File 정보 조회 :: getFileData ||
        /// <summary>
        /// File 정보 조회
        /// </summary>
        /// <param name="dr"></param>
        private void getFileData(DataRow dr)
        {
            //사진 정보 가져오기 
            Dictionary<string, object> Param = new Dictionary<string, object>();

            Param.Add("FILERESOURCETYPE", dr["INSPECTIONDEFID"]);
            Param.Add("FILERESOURCEID", dr["FILERESOURCEID"]);
            DataTable dtFile = SqlExecuter.Query("SelectSelfInspMeasureImage", "10001", Param);

            grdFile.DataSource = dtFile;
        }
        #endregion

        #region ▶ getFile |
        /// <summary>
        /// File을 사진 Object에 보이도록 설정
        /// </summary>
        private void getFile()
        {
            //포커스 행 체크 
            DataRow dr = grdFile.View.GetFocusedDataRow();

            if (dr == null)
            {
                picBox.EditValue = null;
                return;
            }
            string fileURL = Format.GetString(dr["URL"]);

            picBox.EditValue = CommonFunction.GetFtpImageFileToByte(fileURL);
        } 
        #endregion

        #endregion
    }
}
