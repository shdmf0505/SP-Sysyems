#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using System;
using Micube.SmartMES.Commons.Controls;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils;

#endregion

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 출하검사 > 출하검사성적서 엑셀출력
    /// 업  무  설  명  : 미리 작성해야하는 Lot를 선택하여 저장하거나 출하검사가 완료된 Lot에 대해서 엑셀출력을 할 수 있게 해주는 화면
    /// 생    성    자  : JAR
    /// 생    성    일  : 2020-01-12
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ShipmentInspExport : SmartConditionManualBaseForm
    {
        #region Local Variables

        #endregion

        #region 생성자
        public ShipmentInspExport()
        {
            InitializeComponent();
        }
        #endregion

        #region 컨텐츠 영역 초기화
        /// <summary>
        /// 화면의 컨텐츠 영역을 초기화한다.
        /// </summary>
        protected override void InitializeContent()
        {
            // TODO : 컨트롤 초기화 로직 구성
            base.InitializeContent();
            InitializeEvent();
            InitializeGrid();
        }

        /// <summary>        
        /// 코드그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가

            #region 출하검사완료 그리드 초기화
            grdExport.GridButtonItem = GridButtonItem.None;
            grdExport.Caption = string.Empty;
            grdExport.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdExport.View.SetIsReadOnly();

            grdExport.View.AddTextBoxColumn("PLANTID", 150).IsHidden = true;
            grdExport.View.AddTextBoxColumn("LOTID", 180);
            grdExport.View.AddTextBoxColumn("PRODUCTIONTYPE", 100);
            grdExport.View.AddTextBoxColumn("PRODUCTDEFTYPE", 100);
            grdExport.View.AddTextBoxColumn("PRODUCTDEFID", 120);
            grdExport.View.AddTextBoxColumn("PRODUCTDEFNAME", 150);
            grdExport.View.AddTextBoxColumn("PRODUCTDEFVERSION", 100);
            grdExport.View.AddTextBoxColumn("REVISION", 80);
            grdExport.View.AddTextBoxColumn("CUSTOMERNAME", 150);
            grdExport.View.AddTextBoxColumn("ISSHIPMENT", 80);
            grdExport.View.AddTextBoxColumn("INSPECTDATE", 170);

            grdExport.View.PopulateColumns();
            #endregion
        }
        #endregion

        #region Event
        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            grdExport.View.DoubleClick += View_DoubleClick;
        }

        /// <summary>
        /// 조회된 리스트 그리드에서 Row DoubleClick시 POP창 호출
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_DoubleClick(object sender, EventArgs e)
        {
            //DoubleClick이 Row나 Cell이 아니면 Return
            GridHitInfo info = (sender as GridView).CalcHitInfo((e as DXMouseEventArgs).Location);
            if (!(info.InRow || info.InRowCell)) return;

            DataRow focusedRow = grdExport.View.GetFocusedDataRow();

            DialogManager.ShowWaitArea(pnlContent);
            ShipmentInspExportPopup ExportPopup = new ShipmentInspExportPopup();
            ExportPopup.WidthSize = Convert.ToInt32(this.Size.Width * 0.9);
            ExportPopup.HeightSize = Convert.ToInt32(this.Size.Height * 0.9);
            ExportPopup.CurrentDataRow = focusedRow;
            DialogManager.CloseWaitArea(pnlContent);
            ExportPopup.CurrentDataRow = grdExport.View.GetFocusedDataRow();
            ExportPopup.FormBorderStyle = FormBorderStyle.Sizable;
            ExportPopup.ShowDialog();
            ExportPopup.Dispose();
        }
        #endregion

        #region 툴바

        /// <summary>
        /// 등록 버튼 Toolbar 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            base.OnToolbarClick(sender, e);

            if (Format.GetString((sender as SmartButton).Name).Equals("Regist"))
            {
                ShipmentInspExportRegistPopup popup = new ShipmentInspExportRegistPopup()
                {
                    StartPosition = FormStartPosition.CenterParent
                };
                
                if (popup.ShowDialog().Equals(DialogResult.OK))
                {
                    AgainSearch();
                }
                popup.Dispose();
            }
        }
        #endregion

        #region 검색
        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            // TODO : 조회 SP 변경
            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            //임시방편

            DataTable dt = await SqlExecuter.QueryAsync("SelectShipmentExportList", "10001", values);

            if (dt.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdExport.DataSource = dt;
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            InitializeConditionPopup_Customer();
            InitializeConditionPopup_Product();
        }
      
        /// <summary>
        /// 고객사 조회조건
        /// </summary>
        private void InitializeConditionPopup_Customer()
        {
            // 팝업 컬럼 설정
            var customerPopup = Conditions.AddSelectPopup("p_Customer", new SqlQuery("GetCustomerList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "CUSTOMERNAME", "CUSTOMERID")
                                          .SetPopupLayout("COMPANYCLIENT", PopupButtonStyles.Ok_Cancel, true, true)
                                          .SetPopupResultCount(1)
                                          .SetPosition(1.1)
                                          .SetLabel("COMPANYCLIENT")
                                          .SetPopupAutoFillColumns("TXTCUSTOMERID");

            // 팝업 조회조건
            customerPopup.Conditions.AddTextBox("TXTCUSTOMERID")
                                    .SetLabel("COMPANYCLIENT");

            // 팝업 그리드
            customerPopup.GridColumns.AddTextBoxColumn("CUSTOMERID", 150);
            customerPopup.GridColumns.AddTextBoxColumn("CUSTOMERNAME", 200);
        }

        /// <summary>
        /// 품목 조회조건
        /// </summary>
        private void InitializeConditionPopup_Product()
        {
            // 팝업 컬럼설정
            var productPopup = Conditions.AddSelectPopup("p_productdefId", new SqlQuery("GetProductListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PRODUCTDEFNAME", "PRODUCTDEFID")
               .SetPopupLayout("PRODUCT", PopupButtonStyles.Ok_Cancel, true, true)
               .SetLabel("PRODUCT")
               .SetPopupResultCount(1)
               .SetPosition(1.1);

            // 팝업 조회조건
            productPopup.Conditions.AddTextBox("PRODUCTDEFIDNAME");

            // 팝업 그리드
            productPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            productPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            productPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 100);
            //.SetValidationKeyColumn();
        }
        #endregion

        #region Private Function
        /// <summary>
        /// 재조회 
        /// </summary>
        private async void AgainSearch()
        {
            await OnSearchAsync();
        }
        #endregion
    }
}
