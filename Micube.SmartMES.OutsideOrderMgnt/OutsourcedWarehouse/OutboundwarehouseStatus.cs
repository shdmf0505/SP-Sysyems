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
using System.Reflection;
using DevExpress.XtraReports.UI;
using System.IO;
#endregion

namespace Micube.SmartMES.OutsideOrderMgnt
{
    /// <summary>
    /// 프 로 그 램 명  :  외주관리> 외주창고 > 외주창고입출고 현황
    /// 업  무  설  명  : 외주창고입출고 현황한다.
    /// 생    성    자  : CHOISSTAR
    /// 생    성    일  : 2019-07-05
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class OutboundwarehouseStatus : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가
      
        #endregion

        #region 생성자

        public OutboundwarehouseStatus()
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

            InitializeEvent();

            // TODO : 컨트롤 초기화 로직 구성
            InitializeGrid();

            if (pnlToolbar.Controls["layoutToolbar"] != null)
            {
                if (pnlToolbar.Controls["layoutToolbar"].Controls["Slipoutput"] != null)
                    pnlToolbar.Controls["layoutToolbar"].Controls["Slipoutput"].Visible = false;
            }
        }

        /// <summary>        
        /// 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            //수입검사 그리드
            InitializeGridImportInspect();
            
            //입출고 L/T그리드
            InitializeGridInOUtBound();
            //출고전표 리스트 그리드
            InitializeGridOutputslip();
        }

        /// <summary>        
        /// 수입검사 그리드를 초기화한다.
        /// </summary>
        private void InitializeGridImportInspect()
        {
            grdImportInspect.GridButtonItem = GridButtonItem.Export;
            grdImportInspect.View.SetIsReadOnly();

            grdImportInspect.View.AddTextBoxColumn("REQUESTDATE", 150)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss");
            
            grdImportInspect.View.AddComboBoxColumn("REQUESTSTATUS", 100
                , new SqlQuery("GetCodeList", "00001", "CODECLASSID=RequestStatus", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));  // 
            grdImportInspect.View.AddTextBoxColumn("PRODUCTDEFID", 150)
                .SetIsReadOnly();                                                                 //  제품 정의 ID
            grdImportInspect.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80)
                .SetIsReadOnly();                                                             //  제품 정의 Version
            grdImportInspect.View.AddTextBoxColumn("PRODUCTDEFNAME", 150)
                .SetIsReadOnly();                                                             //  제품명    
            grdImportInspect.View.AddTextBoxColumn("PROCESSSEGMENTID", 120)
                .SetIsHidden();                                                                //  공정 ID
            grdImportInspect.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 200)
                .SetIsReadOnly();
            grdImportInspect.View.AddTextBoxColumn("AREANAME", 80)
              .SetIsReadOnly(); //
            grdImportInspect.View.AddTextBoxColumn("LOTID", 200)
                .SetIsReadOnly();                                                             //공정명
            grdImportInspect.View.AddTextBoxColumn("PCSQTY", 120)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);                                //  pcsqty
            
            grdImportInspect.View.AddTextBoxColumn("PANELQTY", 120)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);                                //  panelqty
            grdImportInspect.View.AddTextBoxColumn("OSPMM", 120)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0.##", MaskTypes.Numeric);                             //  panelqty
            grdImportInspect.View.AddTextBoxColumn("INSPECTIONDATE", 150)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly();                                                            //검사일
            grdImportInspect.View.AddTextBoxColumn("INSPECTIONRESULT", 150);//검사결과 
            grdImportInspect.View.PopulateColumns();
        }

       

        /// <summary>        
        /// 입출고 L/T그리드를 초기화한다.
        /// </summary>
        private void InitializeGridInOUtBound()
        {
            grdInOUtBound.GridButtonItem = GridButtonItem.Export;
            grdInOUtBound.View.SetIsReadOnly();
          
            grdInOUtBound.View.AddTextBoxColumn("RECEIPTTIME", 120)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime);//입고시간
            
            grdInOUtBound.View.AddTextBoxColumn("SENDTIME", 120)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime);//출고시간
            grdInOUtBound.View.AddTextBoxColumn("WAITMINTIME", 100)
                .SetTextAlignment(TextAlignment.Center);
           
            grdInOUtBound.View.AddTextBoxColumn("PREVAREAID", 120)
                .SetIsHidden();                                                             //  이전 작업장 PREVAREAID
            grdInOUtBound.View.AddTextBoxColumn("PREVAREANAME", 120)
                .SetIsReadOnly();                                                               //  입고작업장(이전 작업장 PREVAREAID)
            grdInOUtBound.View.AddTextBoxColumn("SENDAEAID", 120)
                .SetIsHidden();                                                             //  출고작업장
            grdInOUtBound.View.AddTextBoxColumn("SENDAREANAME", 120)
                .SetIsReadOnly();                                                               //  출고작업장                                                                            //
            grdInOUtBound.View.AddTextBoxColumn("LOTID", 200)
                .SetIsReadOnly();                                                               //  LOTID  
            grdInOUtBound.View.AddTextBoxColumn("PRODUCTDEFID", 150)
                .SetIsReadOnly();                                                             //  제품 정의 ID
            grdInOUtBound.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80)
                .SetIsReadOnly();                                                             //  제품 정의 Version
            grdInOUtBound.View.AddTextBoxColumn("PRODUCTDEFNAME", 200)
                .SetIsReadOnly();                                                             //  제품명    
            grdInOUtBound.View.AddTextBoxColumn("PROCESSSEGMENTID", 120)
                .SetIsHidden();                                                                //  공정 ID
            grdInOUtBound.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150)
                .SetIsReadOnly();                                                             //공정명
            grdInOUtBound.View.AddTextBoxColumn("PCSQTY", 120)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);                                //  pcsqty
            
            grdInOUtBound.View.AddTextBoxColumn("PANELQTY", 120)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);                                //  panelqty
            grdInOUtBound.View.AddTextBoxColumn("OSPMM", 120)
                .SetIsHidden()
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0.##", MaskTypes.Numeric);                             //  panelqty

          
            grdInOUtBound.View.PopulateColumns();
        }
        /// <summary>        
        /// 출고전표 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGridOutputslip()
        {
            // TODO : 그리드 초기화 로직 추가

            grdOutputslip.GridButtonItem = GridButtonItem.Export;

            grdOutputslip.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdOutputslip.View.AddTextBoxColumn("ENTERPRISEID", 120)
                .SetIsHidden();                                                               //  회사 ID
            grdOutputslip.View.AddTextBoxColumn("PLANTID", 120)
                .SetIsHidden();                                                               //  공장 ID
            grdOutputslip.View.AddTextBoxColumn("RECEIPTUSER", 120)
                .SetIsHidden();
            grdOutputslip.View.AddTextBoxColumn("OSPSENDUSER", 120)
                .SetIsHidden();
            grdOutputslip.View.AddTextBoxColumn("PRINTUSER", 120)
                .SetIsHidden();
            grdOutputslip.View.AddTextBoxColumn("RECEIPTSEQUENCE", 120)
                .SetIsHidden();
            grdOutputslip.View.AddTextBoxColumn("LOTHISTKEY", 120)
                .SetIsHidden();                                                               //  LOTHISTKEY
            grdOutputslip.View.AddTextBoxColumn("PRINTDATE", 80)
                .SetDisplayFormat("yyyy-MM-dd")
                .SetIsReadOnly();                                                              // 출고일자 
            grdOutputslip.View.AddTextBoxColumn("PRODUCTDEFID", 150)
                .SetIsReadOnly();                                                             //  제품 정의 ID
            grdOutputslip.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80)
                .SetIsReadOnly();                                                             //  제품 정의 Version
            grdOutputslip.View.AddTextBoxColumn("PRODUCTDEFNAME", 200)
                .SetIsReadOnly();                                                             //  제품명
            grdOutputslip.View.AddTextBoxColumn("PROCESSSEGMENTID", 120)
               .SetIsHidden();                                                                //  공정 ID
            grdOutputslip.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 200)
                .SetIsReadOnly();
            grdOutputslip.View.AddTextBoxColumn("SENDAEAID", 120)
                .SetIsHidden();                                                             //  출고작업장
            grdOutputslip.View.AddTextBoxColumn("SENDAREANAME", 120)
                .SetIsReadOnly();                                                               //  출고작업장     
            grdOutputslip.View.AddTextBoxColumn("LOTID", 200)
                .SetIsReadOnly();                                                             //  LOTID
            grdOutputslip.View.AddTextBoxColumn("PRINTUSERNAME", 120);                        //  출력자
            grdOutputslip.View.AddTextBoxColumn("PRINTCOUNT", 120)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);                                  //인쇄 횟수

         

            grdOutputslip.View.PopulateColumns();

        }

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            btnOutputslip.Click += BtnOutputslip_Click;
            tabInoutInquiry.SelectedPageChanged += tabInoutInquiry_SelectedPageChanged;
        }
        private void tabInoutInquiry_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (e.Page == tapOutputslip)
            {
                if (pnlToolbar.Controls["layoutToolbar"] != null)
                {
                    if (pnlToolbar.Controls["layoutToolbar"].Controls["Slipoutput"] != null)
                        pnlToolbar.Controls["layoutToolbar"].Controls["Slipoutput"].Visible = false;
                }
            }
            else
            {
                if (pnlToolbar.Controls["layoutToolbar"] != null)
                {
                    if (pnlToolbar.Controls["layoutToolbar"].Controls["Slipoutput"] != null)
                        pnlToolbar.Controls["layoutToolbar"].Controls["Slipoutput"].Visible = false;


                }

            }

        }
        /// <summary>
        /// 저장 -기타 외주 작업 내역을 등록
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOutputslip_Click(object sender, EventArgs e)
        {
            // 선택값 그리드 
           
            DataTable dtcheck = grdOutputslip.View.GetCheckedRows();

            if (dtcheck.Rows.Count == 0)
            {
                ShowMessage("GridNoChecked"); //(선택된 내역이 없습니다)/
                return;
            }

            // 건수 재비교 처리해야함.
            DialogResult result = System.Windows.Forms.DialogResult.No;

            result = this.ShowMessage(MessageBoxButtons.YesNo, "OspDoProcessWant", btnOutputslip.Text);//저장하시겠습니까? 

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    this.ShowWaitArea();
                    btnOutputslip.Enabled = false;


                    DataTable dt = (grdOutputslip.DataSource as DataTable).Clone();

                    for (int irow = 0; irow < dtcheck.Rows.Count; irow++)
                    {
                        DataRow dr = dtcheck.Rows[irow];
                        dr["PRINTUSER"] = UserInfo.Current.Id.ToString();
                        dt.ImportRow(dr);

                    }
                    this.ExecuteRule("OutsourcingIssueOutboundWarehouseInquiry", dt);
                    DataTable dtprint = createSaveDatatable();
                   
                    for (int irow = 0; irow < dtcheck.Rows.Count; irow++)
                    {
                        DataRow dr = dtcheck.Rows[irow];
                        Dictionary<string, object> Param = new Dictionary<string, object>();

                        Param.Add("P_LOTHISTKEY", dr["LOTHISTKEY"]);
                        Param.Add("P_LOTID", dr["LOTID"]);
                        Param.Add("P_RECEIPTSEQUENCE", dr["RECEIPTSEQUENCE"]);
                        Param.Add("P_PATHSEQUENCESTART", dr["PATHSEQUENCESTART"]);
                        Param.Add("P_PATHSEQUENCEEND", dr["PATHSEQUENCEEND"]);
                     
                        Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                        Param = Commons.CommonFunction.ConvertParameter(Param);
                        DataTable dtSlip = SqlExecuter.Query("GetIssueOutboundWarehouseInquiryOutputslip", "10001", Param);

                        dtprint.Merge(dtSlip);
                    }
                    OutputslipSub(dtprint);
                   

                }
                catch (Exception ex)
                {
                    this.ShowError(ex);
                }
                finally
                {
                    this.CloseWaitArea();

                    btnOutputslip.Enabled = true;

                }
            }

        }
        /// <summary>
        /// 그리드에서 추가 버튼 클릭 시 호출
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 그리드의 포커스 행 변경 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
           
        }
        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

          
        }
        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            //base.OnToolbarClick(sender, e);
            SmartButton btn = sender as SmartButton;

            if (btn.Name.ToString().Equals("Slipoutput"))
            {

                BtnOutputslip_Click(null, null);
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
            values.Add("USERID", UserInfo.Current.Id.ToString());
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            #region 품목코드 전환 처리 
            string sproductcode = "";
            if (!(values["P_PRODUCTCODE"] == null))
            {
                sproductcode = values["P_PRODUCTCODE"].ToString();
            }
            // 품목코드값이 있으면
            if (!(sproductcode.Equals("")))
            {
                string[] sproductd = sproductcode.Split('|');
                // plant 정보 다시 가져오기 
                values.Add("P_PRODUCTDEFID", sproductd[0].ToString());
                values.Add("P_PRODUCTDEFVERSION", sproductd[1].ToString());
            }
            #endregion

            #region 기간 검색형 전환 처리 

            if (!(values["P_SEARCHDATE_PERIODFR"].ToString().Equals("")))
            {
                DateTime requestDateFr = Convert.ToDateTime(values["P_SEARCHDATE_PERIODFR"]);
                values.Remove("P_SEARCHDATE_PERIODFR");
                values.Add("P_SEARCHDATE_PERIODFR", string.Format("{0:yyyy-MM-dd}", requestDateFr));
            }
            if (!(values["P_SEARCHDATE_PERIODTO"].ToString().Equals("")))
            {
                DateTime requestDateTo = Convert.ToDateTime(values["P_SEARCHDATE_PERIODTO"]);
                //requestDateTo = requestDateTo.AddDays(1);
                values.Remove("P_SEARCHDATE_PERIODTO");
                values.Add("P_SEARCHDATE_PERIODTO", string.Format("{0:yyyy-MM-dd}", requestDateTo));
            }

            #endregion
            values = Commons.CommonFunction.ConvertParameter(values);
            if (tabInoutInquiry.SelectedTabPage.Name.Equals("tapImportInspect"))
            {
                DataTable dt = await SqlExecuter.QueryAsync("GetOutboundwarehouseStatusInspect", "10001", values);
             
                if (dt.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
                {
                    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                }
                
                grdImportInspect.DataSource = dt;
                if (dt.Rows.Count > 0)
                {
                    grdImportInspect.View.FocusedRowHandle = 0;
                    grdImportInspect.View.SelectRow(0);
                    focusedRowChanged();
                    
                }

            }
            else if (tabInoutInquiry.SelectedTabPage.Name.Equals("tapInOutBound"))
            {
                DataTable dt = await SqlExecuter.QueryAsync("GetOutboundwarehouseStatusInOut", "10001", values);

                if (dt.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
                {
                    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                }

                grdInOUtBound.DataSource = dt;
            }
            else
            {
                DataTable dt = await SqlExecuter.QueryAsync("GetOutboundwarehouseStatusOutputSlip", "10001", values);

                if (dt.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
                {
                    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                }

                grdOutputslip.DataSource = dt;
            }
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            // SITE
            //InitializeConditionPopup_Plant();
            // 일자 1
            // 작업장
            InitializeConditionPopup_Areaid();
            // 공정 
            InitializeConditionPopup_Processsegmentid();
            // 품목 
            InitializeConditionPopup_Product();
            //LOT NO 추가 
            InitializeConditionPopup_Lotno();
        }

        /// <summary>
        /// site 설정 
        /// </summary>
        private void InitializeConditionPopup_Plant()
        {

            var planttxtbox = Conditions.AddComboBox("p_plantid", new SqlQuery("GetPlantList", "00001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PLANTID", "PLANTID")
               .SetLabel("PLANT")
               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false)
               .SetPosition(0.1)
               .SetIsReadOnly(true)
               .SetDefault(UserInfo.Current.Plant, "p_plantId") //기본값 설정 UserInfo.Current.Plant
            ;


        }

        /// <summary>
        /// 작업장 설정 
        /// </summary>
        private void InitializeConditionPopup_Areaid()
        {

            var popupProduct = Conditions.AddSelectPopup("p_areaid",
                                                                 new SqlQuery("GetAreaidListAuthorityByOsp", "10001"
                                                                                 , $"LANGUAGETYPE={ UserInfo.Current.LanguageType }"
                                                                                 , $"USERID={UserInfo.Current.Id}"
                                                                                 , $"ENTERPRISEID={UserInfo.Current.Enterprise}"
                                                                                  , $"P_OWNTYPE={"Y"}"
                                                                                 ), "AREANAME", "AREAID")
              .SetPopupLayout("AREAID", PopupButtonStyles.Ok_Cancel, true, false)
              .SetPopupLayoutForm(450, 600)
              .SetLabel("AREANAME")
              .SetPopupResultCount(1)
              .SetRelationIds("p_plantid")
              .SetPosition(1.1);
            // 팝업 조회조건
            popupProduct.Conditions.AddTextBox("AREANAME")
                .SetLabel("AREANAME");
            popupProduct.GridColumns.AddTextBoxColumn("AREAID", 100)
                .SetValidationKeyColumn();
            popupProduct.GridColumns.AddTextBoxColumn("AREANAME", 200);

        }

        /// <summary>
        /// 공정 선택팝업
        /// </summary>
        private void InitializeConditionPopup_Processsegmentid()
        {
            var popupProcesssegmentid = Conditions.AddSelectPopup("p_processsegmentid",
                                                               new SqlQuery("GetProcessSegmentListByOsp", "10001"
                                                                               , $"LANGUAGETYPE={ UserInfo.Current.LanguageType }"
                                                                               , $"ENTERPRISEID={UserInfo.Current.Enterprise}"
                                                                               ), "PROCESSSEGMENTNAME", "PROCESSSEGMENTID")
            .SetPopupLayout("PROCESSSEGMENTID", PopupButtonStyles.Ok_Cancel, true, false)
            .SetPopupLayoutForm(500, 600)
            .SetLabel("PROCESSSEGMENTNAME")
            .SetPopupResultCount(1)
            .SetPosition(2.3);
            // 팝업 조회조건                                
            popupProcesssegmentid.Conditions.AddTextBox("PROCESSSEGMENTNAME")
                 .SetLabel("PROCESSSEGMENT");
            popupProcesssegmentid.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 100)
                .SetValidationKeyColumn();
            popupProcesssegmentid.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 250);
       
        }

        /// <summary>
        /// Product 설정 
        /// </summary>
        private void InitializeConditionPopup_Product()
        {
            var popupProduct = Conditions.AddSelectPopup("p_productcode",
                                                               new SqlQuery("GetProductdefidlistByOsp", "10001"
                                                                               , $"LANGUAGETYPE={ UserInfo.Current.LanguageType }"
                                                                               , $"ENTERPRISEID={UserInfo.Current.Enterprise}"
                                                                               ), "PRODUCTDEFNAME", "PRODUCTDEFCODE")
                .SetPopupLayout("PRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupLayoutForm(650, 600)
                .SetLabel("PRODUCTDEFID")
                .SetPopupResultCount(1)
                .SetPosition(2.2);

            // 팝업 조회조건
            popupProduct.Conditions.AddComboBox("P_PRODUCTDEFTYPE", new SqlQuery("GetCodeListByOspProductdeftype", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("PRODUCTDEFTYPE")
                .SetDefault("Product");
            popupProduct.Conditions.AddTextBox("PRODUCTDEFNAME")
                .SetLabel("PRODUCTDEFID");
            // 팝업 그리드
            popupProduct.GridColumns.AddTextBoxColumn("PRODUCTDEFCODE", 150)
                .SetIsHidden();
            popupProduct.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150)
                .SetValidationKeyColumn();
            popupProduct.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 100)
               .SetValidationKeyColumn();
            popupProduct.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);

        }

        private void InitializeConditionPopup_Lotno()
        {
            // 팝업 컬럼설정
            

            var txtOspVendor = Conditions.AddTextBox("P_LOTID")
               .SetLabel("LOTID")
               .SetPosition(2.5);

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
        /// <summary>
        /// 그리드의 포커스 행 변경시 행의 작업목록의 내용을 매핑처리 함.
        /// </summary>
        private void focusedRowChanged()
        {
           
        }
        /// <summary>
        /// 출고전표 메인 
        /// </summary>
        /// <param name="dtprint"></param>
        private void OutputslipSub(DataTable dtprint)
        {
            Assembly assembly = Assembly.GetAssembly(this.GetType());
            Stream stream = assembly.GetManifestResourceStream("Micube.SmartMES.OutsideOrderMgnt.report.IssueOutboundWarehouseInquiry.repx");
            DataSet dsReport = new DataSet();
            DataTable header = new DataTable();
            header.Columns.Add(new DataColumn("LBLTITLE", typeof(string)));
            DataRow headerRow = header.NewRow();
            //headerRow["LBLTITLE"] =  Language.Get("RAWASSYIMPORTREPORTTITLE");
            header.Rows.Add(headerRow);


            dsReport.Tables.Add(header);
            dsReport.Tables.Add(dtprint);
            XtraReport report = XtraReport.FromStream(stream);
            report.DataSource = dsReport;
            report.DataMember = dsReport.Tables[1].TableName;

            Band band = report.Bands["Detail"];
            SetReportControlDataBinding(band.Controls, dsReport.Tables[1]);
            setLabelLaungage(band);

            //report.Print();
            //report.PrintingSystem.EndPrint += PrintingSystem_EndPrint1; ;
            ReportPrintTool printTool = new ReportPrintTool(report);
            printTool.ShowRibbonPreview();

        }
        /// <summary>
        /// Report 파일의 컨트롤 중 Tag(FieldName) 값이 있는 컨트롤에 DataBinding(Text)를 추가한다.
        /// </summary>
        private void SetReportControlDataBinding(XRControlCollection controls, DataTable dataSource)
        {
            if (controls.Count > 0)
            {
                foreach (XRControl control in controls)
                {
                    if (!string.IsNullOrWhiteSpace(control.Tag.ToString()) && !(control.Name.Substring(0, 3).Equals("lbl")))
                        control.DataBindings.Add("Text", dataSource, control.Tag.ToString());

                    SetReportControlDataBinding(control.Controls, dataSource);
                }
            }
        }
        /// <summary>
        /// 저장및  삭제용 data table 생성 
        /// </summary>
        /// <returns></returns>
        private DataTable createSaveDatatable()
        {
            DataTable dt = new DataTable();
            dt.TableName = "list";
            dt.Columns.Add("LOTID");
            dt.Columns.Add("PRODUCTDEFID");
            dt.Columns.Add("PRODUCTDEFVERSION");
            dt.Columns.Add("PRODUCTDEFNAME");
            dt.Columns.Add("PCSQTY", typeof(decimal));
            dt.Columns.Add("PNLQTY", typeof(decimal));
            dt.Columns.Add("SENDUSERNAME");
            dt.Columns.Add("PROCESSSEGMENTNAME_01");
            dt.Columns.Add("PROCESSSEGMENTNAME_02");
            dt.Columns.Add("PROCESSSEGMENTNAME_03");
            dt.Columns.Add("PROCESSSEGMENTNAME_04");
            dt.Columns.Add("PROCESSSEGMENTNAME_05");

            dt.Columns.Add("CONSUMABLEDEFNAME_01");
            dt.Columns.Add("CONSUMABLEDEFNAME_02");
            dt.Columns.Add("CONSUMABLEDEFNAME_03");
            dt.Columns.Add("CONSUMABLEDEFNAME_04");
            dt.Columns.Add("CONSUMABLEDEFNAME_05");
            dt.Columns.Add("CONSUMABLEDEFNAME_06");
            dt.Columns.Add("CONSUMABLEDEFNAME_07");
            dt.Columns.Add("CONSUMABLEDEFNAME_08");
            dt.Columns.Add("CONSUMABLEDEFNAME_09");

            dt.Columns.Add("CONSUMEDQTY_01", typeof(decimal));
            dt.Columns.Add("CONSUMEDQTY_02", typeof(decimal));
            dt.Columns.Add("CONSUMEDQTY_03", typeof(decimal));
            dt.Columns.Add("CONSUMEDQTY_04", typeof(decimal));
            dt.Columns.Add("CONSUMEDQTY_05", typeof(decimal));
            dt.Columns.Add("CONSUMEDQTY_06", typeof(decimal));
            dt.Columns.Add("CONSUMEDQTY_07", typeof(decimal));
            dt.Columns.Add("CONSUMEDQTY_08", typeof(decimal));
            dt.Columns.Add("CONSUMEDQTY_09", typeof(decimal));

            dt.Columns.Add("_STATE_");
            return dt;
        }

        /// <summary>
        /// 다국어 명 적용
        /// </summary>
        /// <param name="band"></param>
        private void setLabelLaungage(object band)
        {
            if (band is DetailBand)
            {
                DetailBand detailband = band as DetailBand;
                //Band groupHeader = detailReport.Bands[strGroupHeader];

                foreach (XRControl control in detailband.Controls)
                {
                    if (control is DevExpress.XtraReports.UI.XRLabel)
                    {
                        if (!string.IsNullOrEmpty(control.Tag.ToString()))
                        {
                            if (control.Name.Substring(0, 3).Equals("lbl"))
                            {
                                string bindText = Language.Get(control.Tag.ToString());
                                Font ft = BestSizeEstimator.GetFontToFitBounds(control as XRLabel, bindText);
                                if (ft.Size < control.Font.Size)
                                {
                                    control.Font = ft;
                                }

                                control.Text = bindText;
                            }
                        }
                    }
                    else if (control is DevExpress.XtraReports.UI.XRTable)
                    {
                        XRTable xt = control as XRTable;

                        foreach (XRTableRow tr in xt.Rows)
                        {
                            for (int i = 0; i < tr.Cells.Count; i++)
                            {
                                if (!string.IsNullOrEmpty(tr.Cells[i].Tag.ToString()) && (tr.Cells[i].Name.Substring(0, 3).Equals("lbl")))
                                {
                                    tr.Cells[i].Text = Language.Get(tr.Cells[i].Tag.ToString());

                                }

                            }
                        }

                    }
                }

            }

        }
        #endregion
    }
}
