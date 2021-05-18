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
using Micube.SmartMES.Commons;
using Micube.SmartMES.Commons.Controls;

#endregion

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 공정관리 > LOT관리 > Lot 예약 Locking 해제
    /// 업  무  설  명  : Lot 예약 Locking 해제
    /// 생    성    자  : 조혜인
    /// 생    성    일  : 2020-01-14
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class LotLockingReserveRelease : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가

        #endregion

        #region 생성자

        public LotLockingReserveRelease()
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
            base.InitializeContent();

            this.ucDataUpDownBtnCtrl.SourceGrid = this.grdWIP;
            this.ucDataUpDownBtnCtrl.TargetGrid = this.grdLocking;

            InitializeEvent();

            // TODO : 컨트롤 초기화 로직 구성
            
            InitializeGrid();
        }

        /// <summary>        
        /// 코드그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdWIP.GridButtonItem = GridButtonItem.None;

            grdWIP.View.SetIsReadOnly();
            grdWIP.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            var groupDefaultCol = grdWIP.View.AddGroupColumn("WIPLIST");
            //양산구분
            groupDefaultCol.AddTextBoxColumn("PRODUCTIONTYPE", 80).SetTextAlignment(TextAlignment.Center);
            //품목코드
            groupDefaultCol.AddTextBoxColumn("PRODUCTDEFID", 130).SetTextAlignment(TextAlignment.Center); 
            //REV.
            groupDefaultCol.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetTextAlignment(TextAlignment.Center);
            //품목명
            groupDefaultCol.AddTextBoxColumn("PRODUCTDEFNAME", 250);
            //LOT NO.
            groupDefaultCol.AddTextBoxColumn("LOTID", 200).SetTextAlignment(TextAlignment.Center);
            //공정수순
            groupDefaultCol.AddTextBoxColumn("USERSEQUENCE", 70).SetTextAlignment(TextAlignment.Center);
            //공정명
            groupDefaultCol.AddTextBoxColumn("PROCESSSEGMENTNAME", 170);
            //작업장
            // groupDefaultCol.AddTextBoxColumn("PLANTID", 70).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("AREANAME", 120);
           

            //groupDefaultCol.AddTextBoxColumn("RTRSHT", 70).SetTextAlignment(TextAlignment.Center);
            
            //groupDefaultCol.AddTextBoxColumn("UNIT", 60).SetTextAlignment(TextAlignment.Center);
            
            var groupSendCol = grdWIP.View.AddGroupColumn("RESERVELOCKINGCURRENT");
            //LOCKING
            groupSendCol.AddTextBoxColumn("LOCKING", 100).SetTextAlignment(TextAlignment.Center).SetLabel("PROCESSSEGMENTID");
            //대분류
            groupSendCol.AddTextBoxColumn("LOCKINGGROUP", 100).SetLabel("LARGECLASS");
            //중분류
            groupSendCol.AddTextBoxColumn("LOCKINGTYPE", 100).SetLabel("MIDDLECLASS");
            //사유
            groupSendCol.AddTextBoxColumn("LOCKINGCODE", 100).SetLabel("PCRNO");
            //지정일자
            groupSendCol.AddTextBoxColumn("DESIGNATEDDATE", 150).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetTextAlignment(TextAlignment.Center);
            //담당자
            groupSendCol.AddTextBoxColumn("OWNER", 80).SetTextAlignment(TextAlignment.Center);

            var groupWIPCol = grdWIP.View.AddGroupColumn("WIPLIST");
            //상태
            groupWIPCol.AddTextBoxColumn("STATUS", 80).SetTextAlignment(TextAlignment.Center);
            //PCS
            groupWIPCol.AddTextBoxColumn("PCS", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            //PNL
            groupWIPCol.AddTextBoxColumn("PNL", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");

            groupWIPCol.AddTextBoxColumn("TXNHISTKEY", 80).SetIsHidden();


            //var groupReceiveCol = grdWIP.View.AddGroupColumn("ACCEPT");
            //groupReceiveCol.AddTextBoxColumn("RECEIVEPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            //groupReceiveCol.AddTextBoxColumn("RECEIVEPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");

            //var groupWorkStartCol = grdWIP.View.AddGroupColumn("WIPSTARTQTY");
            //groupWorkStartCol.AddTextBoxColumn("WORKSTARTPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            //groupWorkStartCol.AddTextBoxColumn("WORKSTARTPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");

            //var groupWorkEndCol = grdWIP.View.AddGroupColumn("WIPENDQTY");
            //groupWorkEndCol.AddTextBoxColumn("WORKENDPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            //groupWorkEndCol.AddTextBoxColumn("WORKENDPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");

            //var groupWIPCol = grdWIP.View.AddGroupColumn("WIPLIST");
            //groupWIPCol.AddTextBoxColumn("LEADTIME", 80).SetTextAlignment(TextAlignment.Right);
            //groupWIPCol.AddTextBoxColumn("LOTINPUTDATE", 150).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("{0:yyyy-MM-dd HH:mm:ss}");
            //groupWIPCol.AddTextBoxColumn("DELIVERYDATE", 150).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("{0:yyyy-MM-dd HH:mm:ss}");
            //groupWIPCol.AddTextBoxColumn("LEFTDATE", 80).SetTextAlignment(TextAlignment.Center);

            grdWIP.View.PopulateColumns();

            grdLocking.GridButtonItem = GridButtonItem.None;

            grdLocking.View.SetIsReadOnly();

            // CheckBox 설정
            this.grdLocking.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdLocking.View.AddTextBoxColumn("PRODUCTIONTYPE", 80).SetTextAlignment(TextAlignment.Center);
            grdLocking.View.AddTextBoxColumn("PRODUCTDEFID", 130).SetTextAlignment(TextAlignment.Center);
            grdLocking.View.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            grdLocking.View.AddTextBoxColumn("LOTID", 200).SetTextAlignment(TextAlignment.Center);
            grdLocking.View.AddTextBoxColumn("USERSEQUENCE", 70).SetTextAlignment(TextAlignment.Center);
            grdLocking.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 170);
            grdLocking.View.AddTextBoxColumn("AREANAME", 120);

            grdLocking.View.AddTextBoxColumn("LOCKING", 100).SetTextAlignment(TextAlignment.Center).SetLabel("PROCESSSEGMENTNAME");

            //대분류
            grdLocking.View.AddTextBoxColumn("LOCKINGGROUP", 100).SetLabel("LARGECLASS");
            //중분류
            grdLocking.View.AddTextBoxColumn("LOCKINGTYPE", 100).SetLabel("MIDDLECLASS");
            //사유
            grdLocking.View.AddTextBoxColumn("LOCKINGCODE", 100).SetLabel("PCRNO");
            //지정일자
            grdLocking.View.AddTextBoxColumn("DESIGNATEDDATE", 150).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetTextAlignment(TextAlignment.Center);
            //담당자
            grdLocking.View.AddTextBoxColumn("OWNER", 80).SetTextAlignment(TextAlignment.Center);
            //상태
            grdLocking.View.AddTextBoxColumn("STATUS", 80).SetTextAlignment(TextAlignment.Center);
            //PCS
            grdLocking.View.AddTextBoxColumn("PCS", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            //PNL
            grdLocking.View.AddTextBoxColumn("PNL", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            
            grdLocking.View.AddTextBoxColumn("TXNHISTKEY", 80).SetTextAlignment(TextAlignment.Center).SetIsHidden();
            grdLocking.View.PopulateColumns();
        }

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            grdWIP.View.RowStyle += WIP_RowStyle;
            //cboClass.Editor.EditValueChanged += cboClass_EditValueChanged;

            grdWIP.View.CheckStateChanged += View_CheckStateChanged;
            grdWIP.View.DoubleClick += View_DoubleClick;

            this.ucDataUpDownBtnCtrl.buttonClick += UcDataUpDownBtnCtrl_buttonClick;
            //grdLocking.View.FocusedRowChanged += View_FocusedRowChanged;
            grdLocking.View.RowStyle += Target_RowStyle;
        }
        
        private void UcDataUpDownBtnCtrl_buttonClick(object sender, EventArgs e)
        {
            if (!this.ucDataUpDownBtnCtrl.ButtonState.Equals("Down")) return;

            DataTable dt = grdWIP.View.GetCheckedRows();
            DataTable tgdt = grdLocking.DataSource as DataTable;
            if (tgdt == null || tgdt.Rows.Count <= 0) return;

            //품목
            string productDefId = dt.AsEnumerable().Select(r => Format.GetString(r["PRODUCTDEFID"])).Distinct().FirstOrDefault();
            string tgProductDefId = tgdt.AsEnumerable().Select(r => Format.GetString(r["PRODUCTDEFID"])).Distinct().FirstOrDefault();

            if (!productDefId.Equals(tgProductDefId))
            {
                grdWIP.View.CheckedAll(false);

                // 다른 품목 ID는 선택할 수 없습니다.
                throw MessageException.Create("MixSelectProductDefID");
            }

            //품목버전
            string productDefVer = dt.AsEnumerable().Select(r => Format.GetString(r["PRODUCTDEFVERSION"])).Distinct().FirstOrDefault();
            string tgProductDefVer = tgdt.AsEnumerable().Select(r => Format.GetString(r["PRODUCTDEFVERSION"])).Distinct().FirstOrDefault();

            if (!productDefVer.Equals(tgProductDefVer))
            {
                grdWIP.View.CheckedAll(false);

                //다른 품목 버전은 선택할 수 없습니다.
                throw MessageException.Create("MixSelectProductDefVersion");
            }
        }

        private void WIP_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            CommonFunction.SetGridRowStyle(grdWIP, e);
        }

        private void Target_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            CommonFunction.SetGridRowStyle(grdLocking, e);
        }

        private void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle < 0)
                return;

            if (e.FocusedRowHandle >= 0)
            {
                DataTable dt = grdLocking.DataSource as DataTable;

                if (dt == null || dt.Rows.Count <= 0) return;

                try
                {
                    string strSegmentSeq = dt.AsEnumerable().Max(r => r["USERSEQUENCE"]).ToString();
                    string strProductID = dt.AsEnumerable().Select(r => r.Field<string>("PRODUCTDEFID")).Distinct().ToList()[0].ToString();
                    string strProductVer = dt.AsEnumerable().Select(r => r.Field<string>("PRODUCTDEFVERSION")).Distinct().ToList()[0].ToString();
                    
                    //cboSegment.Editor.ShowHeader = false;
                }
                catch
                {

                }
            }
        }

        /// <summary>
        /// 재공 Grid 더블클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_DoubleClick(object sender, EventArgs e)
        {
            CommonFunction.SetGridDoubleClickCheck(grdWIP, sender);
        }

        /// <summary>
        /// 재공 Grid Check Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CheckStateChanged(object sender, EventArgs e)
        {
            DataTable dt = grdWIP.View.GetCheckedRows();

            int productCount = dt.AsEnumerable().Select(r => r.Field<string>("PRODUCTDEFID")).Distinct().Count();

            if (productCount > 1)
            {
                //grdWIP.View.CheckedAll(false);
                grdWIP.View.CheckMarkSelection.SelectRow(grdWIP.View.FocusedRowHandle, false);

                // 다른 품목 ID는 선택할 수 없습니다.
                throw MessageException.Create("MixSelectProductDefID");
            }

            int productVerCount = dt.AsEnumerable().Select(r => r.Field<string>("PRODUCTDEFVERSION")).Distinct().Count();

            if (productVerCount > 1)
            {
                grdWIP.View.CheckMarkSelection.SelectRow(grdWIP.View.FocusedRowHandle, false);

                //다른 품목 버전은 선택할 수 없습니다.
                throw MessageException.Create("MixSelectProductDefVersion");
            }
        }
        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();
            DataTable lockList = grdLocking.DataSource as DataTable;

            //grdLocking.View.GetCheckedRows()

            if (lockList == null || lockList.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
            
            MessageWorker worker = new MessageWorker("SaveLotLocking");

            //TODO : 파라미터 바꿔야함
            worker.SetBody(new MessageBody()
            {
                { "TransactionType", "SetReleaseLotLockingReserve" },
                { "EnterpriseID", UserInfo.Current.Enterprise },
                { "PlantID", UserInfo.Current.Plant },
                { "Comments", txtComment.Text },
                { "Lotlist", lockList }
            });

            worker.Execute();

            // Data 초기화
            SetInitControl();
        }

        #endregion

        #region 검색

        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();


            // 기존 Grid Data 초기화
            this.grdWIP.DataSource = null;
            this.grdLocking.DataSource = null;

            // TODO : 조회 SP 변경
            var values = Conditions.GetValues();

            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable dtCodeClass = await SqlExecuter.QueryAsync("SelectLotLockingReserve", "10001", values);

            if (dtCodeClass.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdWIP.DataSource = dtCodeClass;
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            CommonFunction.AddConditionBriefLotPopup("P_LOTID", 0.1, false, Conditions);                    // LOT No.

            InitializeCondition_ProductPopup();

            //작업장
            Commons.CommonFunction.AddConditionAreaByAuthorityPopup("P_AREAID", 2.5, true, Conditions, false, false);
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
            Conditions.GetControl<SmartComboBox>("P_PRODUCTDEFTYPE").EditValue = "Product";
            Conditions.GetControl<SmartComboBox>("P_LOTPRODUCTTYPESTATUS").EditValue = "Production";
        }

        /// <summary>
		/// 팝업형 조회조건 생성 - 품목
		/// </summary>
		private void InitializeCondition_ProductPopup()
        {
            // SelectPopup 항목 추가
            var conditionProductId = Conditions.AddSelectPopup("P_PRODUCTDEFID", new SqlQuery("GetProductDefinitionList", "10002", $"PLANTID={UserInfo.Current.Plant}"), "PRODUCTDEF", "PRODUCTDEF")
                .SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, true)
                .SetPopupLayoutForm(800, 800)
                .SetPopupAutoFillColumns("PRODUCTDEFNAME")
                .SetLabel("PRODUCTDEFID")
                .SetPosition(0.5)
                .SetPopupResultCount(0)
                .SetPopupApplySelection((selectRow, gridRow) => {
                    string productDefName = "";

                    selectRow.AsEnumerable().ForEach(r => {
                        productDefName += Format.GetString(r["PRODUCTDEFNAME"]) + ",";
                    });

                    Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").EditValue = productDefName;
                });

            // 팝업에서 사용되는 검색조건 (품목코드/명)
            conditionProductId.Conditions.AddTextBox("TXTPRODUCTDEFNAME");
            //제품 구분 추가 2019.08.14 배선용, 노석안 대리 요청
            conditionProductId.Conditions.AddComboBox("PRODUCTDIVISION", new SqlQuery("GetTypeList", "10001", $"CODECLASSID=ProductDivision2", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                .SetDefault("Product");

            // 품목코드
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            // 품목명
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            // 품목버전
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 100);
            // 품목유형구분
            conditionProductId.GridColumns.AddComboBoxColumn("PRODUCTDEFTYPE", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductDefType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            // 생산구분
            conditionProductId.GridColumns.AddComboBoxColumn("PRODUCTIONTYPE", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            // 단위
            conditionProductId.GridColumns.AddComboBoxColumn("UNIT", 90, new SqlQuery("GetUomDefinitionList", "10001", "UOMCLASSID=Segment"), "UOMDEFNAME", "UOMDEFID");
        }

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            // TODO : 유효성 로직 변경
           
        }

        #endregion

        #region Private Function

        // TODO : 화면에서 사용할 내부 함수 추가
        // <summary>
        /// Control Data 초기화
        /// </summary>
        private void SetInitControl()
        {
            // Data 초기화
           
            this.txtComment.Text = string.Empty;
        }

        #endregion
    }
}