#region using

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


#endregion

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 공정관리 > 출하검사 > 출하검사성적서 엑셀출력
    /// 업  무  설  명  : 출하검사가 끝나지 않았지만 미리 성적서를 작성해야하는 Lot를 등록하는 화면
    /// 생    성    자  : JAR
    /// 생    성    일  : 2020-01-12
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ShipmentInspExportRegistPopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        DataRow ISmartPopupCurrentRow.CurrentDataRow { get; set; }
        #region Local Variables
        private string _LotNo = "";
        private string _ProductDefID = "";
        private string _ProductDefVersion = "";
        private bool _isSelectPopup = false;
        public string LotNo
        {
            get { return _LotNo; }
        }
        // TODO : 화면에서 사용할 내부 변수 추가
        #endregion

        #region 생성자

        public ShipmentInspExportRegistPopup()
        {
            InitializeComponent();
            InitializeGrid();
            InitializeEvent();
            InitializeLanguageKey();
            //SearchLotNoPopUP();
        }
        public ShipmentInspExportRegistPopup(string ProductDefID, string ProductDefVersion)
        {
            InitializeComponent();
            InitializeGrid();
            InitializeEvent();
            InitializeLanguageKey();
            btnSave.LanguageKey = "OK";

            _ProductDefID = ProductDefID;
            _ProductDefVersion = ProductDefVersion;
            _isSelectPopup = true;

            txtProductDefID.Text = ProductDefID;
            txtProductDefID.Enabled = false;
            //SearchLotNoPopUP();
        }
        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 화면의 컨텐츠 영역을 초기화한다.
        /// </summary>

        /// <summary>        
        /// 코드그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdLot.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdLot.GridButtonItem = GridButtonItem.None;
            grdLot.Caption = string.Empty;
            grdLot.View.SetIsReadOnly();

            grdLot.View.AddTextBoxColumn("LOTID", 180);
            grdLot.View.AddTextBoxColumn("PRODUCTDEFID", 150);
            grdLot.View.AddTextBoxColumn("PRODUCTDEFNAME", 150);
            grdLot.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80);
            grdLot.View.AddTextBoxColumn("PRODUCTIONTYPE", 100);

            grdLot.View.PopulateColumns();
        }

        private void InitializeLanguageKey()
        {
            lblLotNo.LanguageKey = "LOTID";
            lblProductDefID.LanguageKey = "PRODUCTDEF";
            btnSearch.LanguageKey = "SEARCH";
            btnSave.LanguageKey = "SAVE";
            btnClose.LanguageKey = "CANCEL";
        }
        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가

            btnClose.Click += (s, e) =>
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            };

            btnSearch.Click += (s, e) =>
            {
                SearchLotNoPopUP();
            };

            btnSave.Click += (s, e) =>
            {
                if(!_isSelectPopup)
                    SaveExportInspectionReportLotNo();
                else
                {
                    if (grdLot.View.GetCheckedRows().Rows.Count > 0)
                    {
                        _LotNo = grdLot.View.GetCheckedRows().Rows[0]["LOTID"].ToString();
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            };

            this.KeyDown += (s, e) =>
            {
                if (e.KeyCode != Keys.F5) return;
                if(!_isSelectPopup)
                    SaveExportInspectionReportLotNo();
            };

            txtLotNo.KeyDown += Txt_KeyDown;
            txtProductDefID.KeyDown += Txt_KeyDown;
        }

        private void Txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;

            SearchLotNoPopUP();
        }
        #endregion

        #region Private Function
        // TODO : 화면에서 사용할 내부 함수 추가
        /// <summary>
        /// LotNo 조회
        /// </summary>
        private void SearchLotNoPopUP()
        {
            var values = new Dictionary<string, object>()
            {
                { "LOTID", txtLotNo.Text},
                { "PRODUCTDEFID", txtProductDefID.Text},
                { "PRODUCTDEFVERSION", _ProductDefVersion},
                { "LANGUAGETYPE" , Framework.UserInfo.Current.LanguageType},
                { "PLANTID" , Framework.UserInfo.Current.Plant}
            };

            DataTable dt = SqlExecuter.Query("SelectShipmentExportInspectionPOPList", "10001", values);

            if (dt.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdLot.DataSource = dt;
        }

        /// <summary>
        /// 선택한 RowData 저장
        /// </summary>
        private void SaveExportInspectionReportLotNo()
        {
            DialogResult dr = new DialogResult();
            dr = this.ShowMessage(MessageBoxButtons.YesNo, "InfoPopupSave");
            if (dr == DialogResult.No) return;

            DataTable dt = (grdLot.View.GetCheckedRows()).Copy();
            dt.TableName = "list";
            try
            {
                this.ShowWaitArea();
                btnSave.Enabled = false;
                btnClose.Enabled = false;

                ExecuteRule("SaveShipmentExportList", dt);
                ShowMessage("SuccessSave");
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                this.ShowError(ex);
                this.DialogResult = DialogResult.Cancel;
            }
            finally
            {
                this.CloseWaitArea();
                btnSave.Enabled = true;
                btnClose.Enabled = true;
                this.Close();
            }
        }
        #endregion
    }
}