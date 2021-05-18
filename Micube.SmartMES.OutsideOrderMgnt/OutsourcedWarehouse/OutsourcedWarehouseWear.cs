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

#endregion

namespace Micube.SmartMES.OutsideOrderMgnt
{
    /// <summary>
    /// 프 로 그 램 명  :  외주관리> 외주창고 > 외주창고입고
    /// 업  무  설  명  : 외주창고입고 등록한다.
    /// 생    성    자  : CHOISSTAR
    /// 생    성    일  : 2019-07-05
    /// 수  정  이  력  : 
    /// 
    /// 
    /// 
    /// </summary>
    public partial class OutsourcedWarehouseWear : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가



        #endregion

        #region 생성자

        public OutsourcedWarehouseWear()
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

            InitializeComboBox();

            InitializeEvent();

            // TODO : 컨트롤 초기화 로직 구성
            InitializeGrid();
            // 입고자명 셋팅 
            txtReceipterName.EditValue = UserInfo.Current.Id.ToString();
        }
        #region ComboBox  

        /// <summary>
        /// SmartComboBox 컨트롤 추가
        /// </summary>
        private void InitializeComboBox()
        {
            // 작업구분값 정의 
            cboPlantid.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboPlantid.ValueMember = "PLANTID";
            cboPlantid.DisplayMember = "PLANTID";
            cboPlantid.EditValue = UserInfo.Current.Plant;

            cboPlantid.DataSource = SqlExecuter.Query("GetPlantList", "00001"
             , new Dictionary<string, object>() { { "ENTERPRISEID", UserInfo.Current.Enterprise }, { "LANGUAGETYPE", UserInfo.Current.LanguageType } });

            cboPlantid.ShowHeader = false;

        }
        #endregion
        /// <summary>        
        /// 코드그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가

            grdOutsourcedWarehouseWear.GridButtonItem = GridButtonItem.Delete | GridButtonItem.Export;

            grdOutsourcedWarehouseWear.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdOutsourcedWarehouseWear.View.AddTextBoxColumn("ENTERPRISEID", 120)
                .SetIsHidden();                                                               //  회사 ID
            grdOutsourcedWarehouseWear.View.AddTextBoxColumn("PLANTID", 120)
                .SetIsHidden();                                                               //  공장 ID
            grdOutsourcedWarehouseWear.View.AddTextBoxColumn("OSPRECEIPTUSER", 120)
                .SetIsHidden();
            grdOutsourcedWarehouseWear.View.AddTextBoxColumn("LOTHISTKEY", 120)
                .SetIsHidden();                                                               //  LOTHISTKEY
            grdOutsourcedWarehouseWear.View.AddTextBoxColumn("LOTID", 200)
                .SetIsReadOnly();                                                             //  LOTID
            grdOutsourcedWarehouseWear.View.AddComboBoxColumn("LOTTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=LotType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsReadOnly();                                                             //  양산구분               
            grdOutsourcedWarehouseWear.View.AddTextBoxColumn("PRODUCTDEFID", 150)
                .SetIsReadOnly();                                                             //  제품 정의 ID
            grdOutsourcedWarehouseWear.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80)
                .SetIsReadOnly();                                                             //  제품 정의 Version
            grdOutsourcedWarehouseWear.View.AddTextBoxColumn("PRODUCTDEFNAME", 150)
                .SetIsReadOnly();                                                             //  제품명
            grdOutsourcedWarehouseWear.View.AddTextBoxColumn("USERSEQUENCE", 80)
                .SetIsReadOnly();                                                             //  공순
            grdOutsourcedWarehouseWear.View.AddTextBoxColumn("PROCESSSEGMENTID", 120)
                .SetIsHidden();                                                             //  공정 ID
            grdOutsourcedWarehouseWear.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150)
                .SetIsReadOnly();                                                             //  공정명
            grdOutsourcedWarehouseWear.View.AddTextBoxColumn("AREAID", 120)
                .SetIsHidden();                                                             //  작업장 AREAID
            grdOutsourcedWarehouseWear.View.AddTextBoxColumn("AREANAME", 120)
                .SetIsReadOnly();                                                               //  작업장 AREAID
            grdOutsourcedWarehouseWear.View.AddTextBoxColumn("PREVPROCESSSEGMENTID", 120)
                .SetIsHidden();                                                              //  이전공정 ID
            grdOutsourcedWarehouseWear.View.AddTextBoxColumn("PREVPROCESSSEGMENTNAME", 150)
                .SetIsReadOnly();                                                             //  이전공정명
            grdOutsourcedWarehouseWear.View.AddTextBoxColumn("PREVAREAID", 120)
                .SetIsHidden();                                                             //  이전 작업장 PREVAREAID
            grdOutsourcedWarehouseWear.View.AddTextBoxColumn("PREVAREANAME", 120)
              .SetIsReadOnly();                                                               //  이전 작업장 PREVAREAID

            grdOutsourcedWarehouseWear.View.AddTextBoxColumn("PCSQTY", 120)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);                                //  pcsqty

            grdOutsourcedWarehouseWear.View.AddTextBoxColumn("PANELQTY", 120)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);                                //  panelqty
            grdOutsourcedWarehouseWear.View.AddTextBoxColumn("OSPMM", 120)
                .SetIsHidden()
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0.##", MaskTypes.Numeric);                                //  panelqty
            grdOutsourcedWarehouseWear.View.AddTextBoxColumn("PATHSEQUENCESTART", 120)
              .SetIsHidden();                                                              //  이전공정 ID
            grdOutsourcedWarehouseWear.View.AddTextBoxColumn("PATHSEQUENCEEND", 120)
             .SetIsHidden();
            grdOutsourcedWarehouseWear.View.PopulateColumns();


        }

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가

            txtLotid.KeyDown += txtLotid_KeyDown;
        }

        /// <summary>
        /// 코드그룹 리스트 그리드에서 추가 버튼 클릭 시 호출
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {

        }

        /// <summary>
        /// lot id  key down 이벤트 처리 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtLotid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtLotid.EditValue.ToString().Equals("")) return;
                //1. 무결성 확인 (그리드 상)

                for (int i = 0; i < grdOutsourcedWarehouseWear.View.DataRowCount; i++)
                {
                    if (grdOutsourcedWarehouseWear.View.GetRowCellValue(i, "LOTID").ToString().Equals((txtLotid.EditValue.ToString())))
                    {
                        //포커스 이동 처리  OspDataOverlapCheck
                        this.ShowMessage(MessageBoxButtons.OK, "OspDataOverlapCheck"); //메세지 
                        //다국어 입력된 내용이 있습니다.
                        grdOutsourcedWarehouseWear.View.FocusedRowHandle = i;
                        txtLotid.SelectionStart = 0;
                        txtLotid.SelectionLength = txtLotid.Text.Trim().ToString().Length;
                        txtLotid.Focus();
                        return;
                    }
                }
                Dictionary<string, object> Param = new Dictionary<string, object>();
                Param.Add("P_PLANTID", cboPlantid.EditValue);
                Param.Add("P_LOTID", txtLotid.EditValue.ToString());
                Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                Param = Commons.CommonFunction.ConvertParameter(Param);
                DataTable dtLotid = SqlExecuter.Query("GetOutsourcedWarehouseWear", "10001", Param);
                //자료존재시
                if (dtLotid.Rows.Count > 0)
                {
                    DataRow row = dtLotid.Rows[0];
                    string strWearOkCheck = row["WEAROKCHECK"].ToString();
                    if (strWearOkCheck.Equals("OK"))
                    {
                        //WEAROKCHECK
                        grdOutsourcedWarehouseWear.View.AddNewRow();
                        grdOutsourcedWarehouseWear.View.SetFocusedRowCellValue("ENTERPRISEID", UserInfo.Current.Enterprise.ToString());// 회사코드
                        grdOutsourcedWarehouseWear.View.SetFocusedRowCellValue("PLANTID", cboPlantid.EditValue);// plantid
                        grdOutsourcedWarehouseWear.View.SetFocusedRowCellValue("LOTHISTKEY", row["LOTHISTKEY"]);// LOTHISTKEY
                        grdOutsourcedWarehouseWear.View.SetFocusedRowCellValue("LOTID", row["LOTID"]);// LOTID
                        grdOutsourcedWarehouseWear.View.SetFocusedRowCellValue("LOTTYPE", row["LOTTYPE"]); //  양산구분       
                        grdOutsourcedWarehouseWear.View.SetFocusedRowCellValue("PRODUCTDEFID", row["PRODUCTDEFID"]);// 제품 정의 ID
                        grdOutsourcedWarehouseWear.View.SetFocusedRowCellValue("PRODUCTDEFVERSION", row["PRODUCTDEFVERSION"]);// 제품 정의 Version
                        grdOutsourcedWarehouseWear.View.SetFocusedRowCellValue("PRODUCTDEFNAME", row["PRODUCTDEFNAME"]);// 제품명
                        grdOutsourcedWarehouseWear.View.SetFocusedRowCellValue("PROCESSSEGMENTID", row["PROCESSSEGMENTID"]);// 공정 ID
                        grdOutsourcedWarehouseWear.View.SetFocusedRowCellValue("PROCESSSEGMENTNAME", row["PROCESSSEGMENTNAME"]);// 공정명
                        grdOutsourcedWarehouseWear.View.SetFocusedRowCellValue("USERSEQUENCE", row["USERSEQUENCE"]);// 공순 USERSEQUENCE
                        grdOutsourcedWarehouseWear.View.SetFocusedRowCellValue("AREAID", row["AREAID"]);// 작업장 AREAID
                        grdOutsourcedWarehouseWear.View.SetFocusedRowCellValue("AREANAME", row["AREANAME"]);// 작업장 AREAID
                        grdOutsourcedWarehouseWear.View.SetFocusedRowCellValue("PREVPROCESSSEGMENTID", row["PREVPROCESSSEGMENTID"]);//이전공정 ID
                        grdOutsourcedWarehouseWear.View.SetFocusedRowCellValue("PREVPROCESSSEGMENTNAME", row["PREVPROCESSSEGMENTNAME"]);// 이전공정명
                        grdOutsourcedWarehouseWear.View.SetFocusedRowCellValue("PREVAREAID", row["PREVAREAID"]);// PREVAREAID
                        grdOutsourcedWarehouseWear.View.SetFocusedRowCellValue("PREVAREANAME", row["PREVAREANAME"]);// PREVAREANAME
                        grdOutsourcedWarehouseWear.View.SetFocusedRowCellValue("PCSQTY", row["PCSQTY"]);// LOTHISTKEY

                        grdOutsourcedWarehouseWear.View.SetFocusedRowCellValue("PANELQTY", row["PANELQTY"]);// PANELQTY
                        grdOutsourcedWarehouseWear.View.SetFocusedRowCellValue("OSPMM", row["OSPMM"]);// LOTHISTKEY
                        grdOutsourcedWarehouseWear.View.SetFocusedRowCellValue("PATHSEQUENCESTART", row["PATHSEQUENCESTART"]);// PANELQTY
                        grdOutsourcedWarehouseWear.View.SetFocusedRowCellValue("PATHSEQUENCEEND", row["PATHSEQUENCEEND"]);// PANELQTY
                        grdOutsourcedWarehouseWear.View.SetFocusedRowCellValue("OSPRECEIPTUSER", UserInfo.Current.Id.ToString());// 

                        txtLotid.Text = "";

                        txtLotid.Focus();
                    }
                    else
                    {
                        this.ShowMessage(MessageBoxButtons.OK, DialogResult.None, "InValidOspData018", txtLotid.Text); //메세지 해당
                        txtLotid.SelectionStart = 0;
                        txtLotid.SelectionLength = txtLotid.Text.Trim().ToString().Length;
                        txtLotid.Focus();
                        return;
                    }
                }
                else
                {
                    // 다국어  해당 lot no에 해당하는 정보가 없습니다. 메세지 처리 
                    this.ShowMessage(MessageBoxButtons.OK, DialogResult.None, "InValidOspData002", txtLotid.Text); //메세지 
                    txtLotid.SelectionStart = 0;
                    txtLotid.SelectionLength = txtLotid.Text.Trim().ToString().Length;
                    txtLotid.Focus();
                    return;
                }

            }
        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            //base.OnToolbarSaveClick();

            //// TODO : 저장 Rule 변경
            //DataTable changed = grdOutsourcedWarehouseWear.GetChangedRows();

            //ExecuteRule("OutsourcedWarehouseWear", changed);
        }
        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            //base.OnToolbarClick(sender, e);
            SmartButton btn = sender as SmartButton;

            if (btn.Name.ToString().Equals("Save"))
            {

                ProcSave(btn.Text);
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
            txtLotid.EditValue = "";
            //조회용 임시 테이블 생성 null 이면 에러 발생 
            DataTable dtSearch = (grdOutsourcedWarehouseWear.DataSource as DataTable).Clone();

            grdOutsourcedWarehouseWear.DataSource = dtSearch;
            // TODO : 조회 SP 변경

        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

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

            // TODO : 유효성 로직 변경
            grdOutsourcedWarehouseWear.View.CheckValidation();

            DataTable changed = grdOutsourcedWarehouseWear.GetChangedRows();

            if (changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }

        #endregion

        #region Private Function

        // TODO : 화면에서 사용할 내부 함수 추가
        /// <summary>
        /// 마스터 정보를 조회한다.
        /// </summary>
        private void LoadData()
        {

        }
        private void ProcSave(string strtitle)
        {


            // TODO : 유효성 로직 변경
            grdOutsourcedWarehouseWear.View.CheckValidation();

            DataTable changed = grdOutsourcedWarehouseWear.DataSource as DataTable;


            if (changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                this.ShowMessage(MessageBoxButtons.OK, "NoSaveData");
                return;
            }


            DialogResult result = System.Windows.Forms.DialogResult.No;

            result = this.ShowMessage(MessageBoxButtons.YesNo, "OspDoProcessWant", strtitle);//저장하시겠습니까? 

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    this.ShowWaitArea();

                    // TODO : 저장 Rule 변경
                    DataTable dtSave = grdOutsourcedWarehouseWear.DataSource as DataTable;

                    ExecuteRule("OutsourcedWarehouseWear", dtSave);
                    ShowMessage("SuccessOspProcess");

                    grdOutsourcedWarehouseWear.View.ClearDatas();
                }
                catch (Exception ex)
                {
                    this.ShowError(ex);
                }
                finally
                {
                    this.CloseWaitArea();


                }
            }
        }
        #endregion
    }   
}
