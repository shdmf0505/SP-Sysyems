#region using

using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;

using Micube.SmartMES.Commons;
using Micube.SmartMES.Commons.Controls;
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

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 공정 관리 > 투입관리 > LOT 생성
    /// 업  무  설  명  : EPR에서 Interface 된 Product Order 정보로 Lot을 생성 한다.
    /// 생    성    자  : 강호윤
    /// 생    성    일  : 2019-06-17
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ConsumeSNMapping : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가

        #endregion

        #region 생성자

        public ConsumeSNMapping()
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

            UseAutoWaitArea = false;

            this.ucUpDown.SourceGrid = this.grdWip;
            this.ucUpDown.TargetGrid = this.grdTarget;

            InitializeEvent();

            // TODO : 컨트롤 초기화 로직 구성
            InitializeProductionOrderGrid();
            
        }

        /// <summary>        
        /// 수주 리스트 그리드를 초기화 한다.
        /// </summary>
        private void InitializeProductionOrderGrid()
        {
            grdWip.GridButtonItem = GridButtonItem.None;
            grdWip.ShowButtonBar = false;
            grdWip.ShowStatusBar = false;

            grdWip.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdWip.View.SetIsReadOnly();
            grdWip.View.EnableRowStateStyle = false;

            // 품목
            grdWip.View.AddTextBoxColumn("PRODUCTDEFID", 120).SetTextAlignment(TextAlignment.Left);
            //REV
            grdWip.View.AddTextBoxColumn("PRODUCTREVISION", 70).SetTextAlignment(TextAlignment.Left);
            // 품목명
            grdWip.View.AddTextBoxColumn("PRODUCTDEFNAME", 270).SetTextAlignment(TextAlignment.Left);
            // lotid
            grdWip.View.AddSpinEditColumn("LOTID", 200).SetTextAlignment(TextAlignment.Center);
            //PLNATID
            grdWip.View.AddSpinEditColumn("PLANTID", 150).SetTextAlignment(TextAlignment.Center).SetIsHidden();
            // 작업장
            grdWip.View.AddTextBoxColumn("AREAID", 100).SetTextAlignment(TextAlignment.Left).SetIsHidden();
            grdWip.View.AddTextBoxColumn("AREANAME", 160).SetTextAlignment(TextAlignment.Left);
            // 품목명
            grdWip.View.AddTextBoxColumn("PROCESSSEGMENTID", 150).SetTextAlignment(TextAlignment.Left).SetIsHidden();
            grdWip.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 160).SetTextAlignment(TextAlignment.Left);
            // 물류상태
            grdWip.View.AddSpinEditColumn("TRANSITSTATUSCODE", 70).SetTextAlignment(TextAlignment.Center).SetIsHidden();
            grdWip.View.AddSpinEditColumn("TRANSITSTATUS", 70).SetTextAlignment(TextAlignment.Center).SetLabel("TRANSITSTATE");

            grdWip.View.AddSpinEditColumn("PANELQTY", 70).SetTextAlignment(TextAlignment.Right);

            grdWip.View.AddSpinEditColumn("QTY", 70).SetTextAlignment(TextAlignment.Right);

            grdWip.View.AddSpinEditColumn("EXTENT", 70).SetTextAlignment(TextAlignment.Right);
            grdWip.View.PopulateColumns();
            grdWip.View.OptionsView.ShowIndicator = false;


            grdTarget.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdTarget.View.SetIsReadOnly();
            grdTarget.View.AddTextBoxColumn("PRODUCTDEFID", 120).SetTextAlignment(TextAlignment.Left);
            //REV
            grdTarget.View.AddTextBoxColumn("PRODUCTREVISION", 70).SetTextAlignment(TextAlignment.Left);
            // 품목명
            grdTarget.View.AddTextBoxColumn("PRODUCTDEFNAME", 270).SetTextAlignment(TextAlignment.Left);
            // lotid
            grdTarget.View.AddTextBoxColumn("LOTID", 200).SetTextAlignment(TextAlignment.Center);
            //PLNATID
            grdTarget.View.AddTextBoxColumn("PLANTID", 150).SetTextAlignment(TextAlignment.Center).SetIsHidden();
            // 품목코드
            grdTarget.View.AddTextBoxColumn("AREAID", 100).SetTextAlignment(TextAlignment.Left).SetIsHidden();
            grdTarget.View.AddTextBoxColumn("AREANAME", 160).SetTextAlignment(TextAlignment.Left);
            // 품목명
            grdTarget.View.AddTextBoxColumn("PROCESSSEGMENTID", 150).SetTextAlignment(TextAlignment.Left).SetIsHidden();
            grdTarget.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 160).SetTextAlignment(TextAlignment.Left);
            // 물류상태
            grdTarget.View.AddTextBoxColumn("TRANSITSTATUSCODE", 70).SetTextAlignment(TextAlignment.Center).SetIsHidden();
            grdTarget.View.AddTextBoxColumn("TRANSITSTATUS", 70).SetTextAlignment(TextAlignment.Center).SetIsHidden();

            grdTarget.View.AddSpinEditColumn("PANELQTY", 70).SetTextAlignment(TextAlignment.Right);

            grdTarget.View.AddSpinEditColumn("QTY", 70).SetTextAlignment(TextAlignment.Right);

            grdTarget.View.AddSpinEditColumn("EXTENT", 70).SetTextAlignment(TextAlignment.Right);
            grdTarget.View.PopulateColumns();
            grdTarget.View.OptionsView.ShowIndicator = false;
        }



        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
           // grdWip.View.FocusedRowChanged += View_FocusedRowChanged;
            ucUpDown.buttonClick += UcUpDown_buttonClick;
            
            //grdTargetLotList.View.Click += View_Click;

            //Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTIONORDERID").EditValueChanged += LotCreateSheet_EditValueChanged;
        }

        private void UcUpDown_buttonClick(object sender, EventArgs e)
        {
            string btnState = ucUpDown.ButtonState;

            if (btnState.Equals("Down"))
            {
                DataTable dt = grdWip.View.GetCheckedRows();

                if (dt == null || dt.Rows.Count == 0)
                {
                    throw MessageException.Create("GridNoChecked");
                }

                DataTable dtTarget = grdTarget.DataSource as DataTable;

                string AreaID = Format.GetFullTrimString(dt.Rows[0]["AREAID"]);
                string SegmentID = Format.GetFullTrimString(dt.Rows[0]["PROCESSSEGMENTID"]);
                string productDefid = Format.GetFullTrimString(dt.Rows[0]["PRODUCTDEFID"]);
                string productDefVersion = Format.GetFullTrimString(dt.Rows[0]["PRODUCTDEFVERSION"]);
                string plantid = Format.GetFullTrimString(dt.Rows[0]["PLANTID"]);

                //체크된 Row에서 중복확인, 작업장, 공정,품목코드,Rev
                DataTable dtCheck = new DataTable();
                
                dtCheck.Merge(dt);
                if (dtTarget != null && dtTarget.Rows.Count >0)
                    dtCheck.Merge(dtTarget);

                int areaCnt = dtCheck.AsEnumerable().Where(c => !c.Field<string>("AREAID").Equals(AreaID)).Count();
                int segCnt = dtCheck.AsEnumerable().Where(c => !c.Field<string>("PROCESSSEGMENTID").Equals(SegmentID)).Count();
                int prodCnt = dtCheck.AsEnumerable().Where(c => !c.Field<string>("PRODUCTDEFID").Equals(productDefid) && !c.Field<string>("PRODUCTDEFVERSION").Equals(productDefVersion)).Count();
                int checkLogistic = dtCheck.AsEnumerable().Where(c => !c.Field<string>("TRANSITSTATUSCODE").Equals("InStock")).Count();
                //CheckLogisticStatus
                if (areaCnt > 0)
                {
                    throw MessageException.Create("SelectOnlyOneArea");
                }
                if (segCnt > 0)
                {
                    throw MessageException.Create("CheckDupliSegment");
                }
                if (prodCnt > 0)
                {
                    throw MessageException.Create("SameProductDefinition", productDefid);
                }
                if (checkLogistic > 0)
                {
                    throw MessageException.Create("CheckLogisticStatus", productDefid);
                }

                string productDefName = Format.GetFullTrimString(dt.Rows[0]["PRODUCTDEFNAME"]);

                SetReadOnly(false);
                //SetRevComboBox(productDefid);
                SetRevComboBox(productDefid, productDefVersion);    // TODO : 임시코드
                setPorductionOrderPopup(productDefid, productDefVersion, plantid);
                txtProduct.EditValue = productDefName;

            }
            else
            {
                DataTable  dtTargetChecked = grdTarget.View.GetCheckedRows();

                DataTable dtTarget = grdTarget.DataSource as DataTable;

                if(dtTargetChecked.Rows.Count == dtTarget.Rows.Count)
                {
                    txtComment.Text = string.Empty;
                    txtProduct.Text = string.Empty;
                    txtSalseOrderNo.Editor.ClearValue();
                    cboProductRevision.Editor.Text = string.Empty;
                    SetReadOnly(true);
                }


            }
        }

        private void View_Click(object sender, EventArgs e)
        {

            int FocusedRowHandle = grdWip.View.FocusedRowHandle;
            FocusedRowChanged(FocusedRowHandle);
        }

        /// <summary>
        /// 수주 리스트 그리드의 선택된 행이 변경될 경우 호출 한다.
        /// 순수주, 순투입, 기준투입(PCS), 기준로스 값을 다시 계산 한다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            FocusedRowChanged(e.FocusedRowHandle);
        }




        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();


            // TODO : 저장 Rule 변경

            DataTable dtTarget = grdTarget.DataSource as DataTable;

            if (dtTarget.Rows.Count == 0 || dtTarget is null)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }

            string[] strSalseOrder = Format.GetString(txtSalseOrderNo.EditValue).Split('|');
            string strRev = Format.GetString(cboProductRevision.EditValue);

            try
            {
                pnlContent.ShowWaitArea();

                MessageWorker worker = new MessageWorker("SaveConsumConnect");
                worker.SetBody(new MessageBody()
                {

                    { "ProductionOrderId", strSalseOrder[0].Trim().ToString() },
                    { "LineNo", strSalseOrder[1].Trim().ToString() },
                    { "PRODUCTDEFVERSION", strRev },
                    { "UserId", UserInfo.Current.Id },
                    { "PrintLotCard", chkPrintLotCard.Checked ? "Y" : "N" },
                    { "Lotlist", dtTarget },

                });

                worker.Execute();
            }
            catch (Exception ex)
            {
                throw MessageException.Create(ex.Message);
            }
            finally
            {
                pnlContent.CloseWaitArea();
            }

            //LOT CARD 출력
            if (chkPrintLotCard.Checked)
            {
                string lotId = string.Join(",", dtTarget.AsEnumerable().Select(row => Format.GetString(row["LOTID"])));
                CommonFunction.PrintLotCard(lotId, LotCardType.Normal);
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
            ClearALLData();

            DataTable dt = await QueryAsync("GetConsumableLotConnectTarget", "10001", values);
            
            grdWip.DataSource = dt;

            // 검색 결과에 데이터가 없는 경우
            if (dt.Rows.Count < 1)
            {
                // 조회할 데이터가 없습니다.
                ShowMessage("NoSelectData");
            }
       
            
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            CommonFunction.AddConditionLotPopup("P_LOTID", 0.1, true, Conditions);
            // 품목
            CommonFunction.AddConditionProductPopup("P_PRODUCTDEFID", 0.2, true, Conditions);
            CommonFunction.AddConditionAreaPopup("P_AREAID", 0.3, false, Conditions);
            
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();
            SetReadOnly(true);
            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
        }

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            /*
            // TODO : 유효성 로직 변경
            if (!CheckStandardLoss())
            {
                // 기준로스는 0 보다 큰 값이어야 합니다.
                throw MessageException.Create("InValidStandardLoss");
            }
            */
            if (string.IsNullOrEmpty(cboProductRevision.EditValue.ToString()))
            {
                //Revision 선택
                throw MessageException.Create("CheckInputProductVersion");
            }
            if (string.IsNullOrEmpty(txtSalseOrderNo.EditValue.ToString()))
            {

                //Salse Order 선택
                throw MessageException.Create("CheckSelectSalseOrder");
            }
            if (string.IsNullOrEmpty(txtProduct.EditValue.ToString()))
            {
                //ChecSelectLot
                //Lot 선택여부
                throw MessageException.Create("ChecSelectLot");
            }
        }

        #endregion

        #region Private Function

        // TODO : 화면에서 사용할 내부 함수 추가

        private void ClearInputData()
        {
            txtProduct.Text = string.Empty;
            txtSalseOrderNo.EditValue = string.Empty;
            cboProductRevision.EditValue = string.Empty;
            txtComment.EditValue = string.Empty;
            grdTarget.DataSource = null;

        }

        

        private void ClearALLData()
        {
            ClearInputData();
            grdWip.View.ClearDatas();

        }
        private void setPorductionOrderPopup(string strProductdefid,string strProductdefversion,string plantid)
        {
            ConditionItemSelectPopup POCondition = new ConditionItemSelectPopup();
            POCondition.Id = "PRODUCTIONORDER";
            POCondition.SearchQuery = new SqlQuery("GetProductionOrderIdList", "10002", $"PLANTID={plantid}", $"PRODUCTDEFID={strProductdefid}", $"PRODUCTDEFVERSION={strProductdefversion}");
            POCondition.ValueFieldName = "VALUEFIELD";
            POCondition.DisplayFieldName = "VALUEFIELD";
            POCondition.SetPopupLayout("SELECTPRODUCTIONORDER", PopupButtonStyles.Ok_Cancel, true, true);
            POCondition.SetPopupResultCount(1);
            POCondition.SetPopupLayoutForm(600, 800, FormBorderStyle.FixedToolWindow);
            
            POCondition.GridColumns.AddTextBoxColumn("PRODUCTIONORDERID", 150);
            POCondition.GridColumns.AddTextBoxColumn("VALUEFIELD", 150).SetIsHidden();
            POCondition.GridColumns.AddTextBoxColumn("LINENO", 150);
          
            txtSalseOrderNo.Editor.SelectPopupCondition = POCondition;
            
        }
        private void SetRevComboBox(string strProductDefid, string strProductDefVersion)
        {

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("PRODUCTDEFID", strProductDefid);

            // UOM
            cboProductRevision.Editor.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboProductRevision.Editor.ShowHeader = false;
            cboProductRevision.Editor.ValueMember = "PRODUCTDEFVERSION";
            cboProductRevision.Editor.DisplayMember = "PRODUCTDEFVERSIONNAME";
            cboProductRevision.Editor.UseEmptyItem = true;
            cboProductRevision.Editor.EmptyItemValue = "";
            cboProductRevision.Editor.EmptyItemCaption = "";
            cboProductRevision.Editor.DataSource = SqlExecuter.Query("GetProductDefVersion", "10002", param);

            DataTable dt = cboProductRevision.Editor.DataSource as DataTable;

            if (dt.Rows.Count > 0)
            {
                // cboProductRevision.Editor.ItemIndex = dt.Rows.Count;
                cboProductRevision.Editor.EditValue = strProductDefVersion; // TODO : 임시코드
            }
        }
        private void SetReadOnly(bool bReadonly)
        {
           // txtProduct.Properties.ReadOnly = bReadonly;
            txtSalseOrderNo.Properties.ReadOnly = bReadonly;
            txtComment.Properties.ReadOnly = bReadonly;
            cboProductRevision.Properties.ReadOnly = bReadonly;
        }
        /// <summary>
        /// 수주 리스트 그리드의 선택된 행이 변경된 경우 다시 계산하는 로직을 호출한다.
        /// </summary>
        /// <param name="focusedRowHandle">그리드에 선택된 행 Index</param>
        private void FocusedRowChanged(int focusedRowHandle)
        {
            
            if (focusedRowHandle < 0)
            {
         
                return;
            }

            ClearInputData();

            DataRow row = grdWip.View.GetDataRow(focusedRowHandle);

            SetReadOnly(false);
            SetRevComboBox(Format.GetString(row["PRODUCTDEFID"]), Format.GetString(row["PRODUCTREVISION"]));
            setPorductionOrderPopup(Format.GetString(row["PRODUCTDEFID"]), Format.GetString(row["PRODUCTREVISION"]), Format.GetString(row["PLANTID"]));
            txtProduct.EditValue = Format.GetString(row["PRODUCTDEFNAME"]);
        }


        #endregion
    }
}