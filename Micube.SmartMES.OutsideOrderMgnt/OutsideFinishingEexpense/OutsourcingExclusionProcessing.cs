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
    /// 프 로 그 램 명  :  외주관리> 외주 가공비마감 > 외주비 제외 처리
    /// 업  무  설  명  :  외주비 집계 및 마감한다.
    /// 생    성    자  : CHOISSTAR
    /// 생    성    일  : 2019-08-28
    /// 수  정  이  력  : 
    /// 
    /// 
    /// 
    /// </summary>
    public partial class OutsourcingExclusionProcessing : SmartConditionManualBaseForm
    {
        #region Local Variables
        string _strPeriodid ="";

        #endregion

        #region 생성자

        public OutsourcingExclusionProcessing()
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
        }

        /// <summary>        
        /// 코드그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {


            InitializeGridOspActual();
            InitializeGridOspEtcWork();
            InitializeGridOspEtcAmount();
        }
        private void InitializeGridOspActual()
        {
            grdOspActual.GridButtonItem = GridButtonItem.Export | GridButtonItem.Expand | GridButtonItem.Restore;
            grdOspActual.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdOspActual.View.SetIsReadOnly();
            grdOspActual.View.AddTextBoxColumn("WORKGUBUN", 120).SetIsHidden();
            grdOspActual.View.AddTextBoxColumn("PERIODID", 120).SetIsHidden();
            grdOspActual.View.AddTextBoxColumn("ISEXCEPT", 80)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(); //
            grdOspActual.View.AddTextBoxColumn("EXCEPTTIME", 120)
                .SetDisplayFormat("yyyy-MM-dd");                                                     //  
            grdOspActual.View.AddTextBoxColumn("EXCEPTUSER", 80)
                .SetIsHidden();
            grdOspActual.View.AddTextBoxColumn("EXCEPTUSERNAME", 80)
                .SetIsReadOnly();
            grdOspActual.View.AddComboBoxColumn("PERIODSTATE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=PeriodState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Open")
                .SetIsReadOnly();
            grdOspActual.View.AddTextBoxColumn("PLANTID", 200)
                .SetIsHidden();
            grdOspActual.View.AddTextBoxColumn("LOTHISTKEY", 200)
                .SetIsHidden();
            grdOspActual.View.AddTextBoxColumn("ENTERPRISEID", 200)
                .SetIsHidden();                                                           //  공장 ID
            grdOspActual.View.AddTextBoxColumn("PROCESSSEGMENTID", 100)
                .SetIsReadOnly();
            grdOspActual.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150)
                .SetIsReadOnly();

            grdOspActual.View.AddComboBoxColumn("OWNTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OwnType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsReadOnly();  // 
            grdOspActual.View.AddComboBoxColumn("PROCESSPRICETYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProcessPriceType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetEmptyItem("*", "*")
                .SetIsReadOnly();  // 
            grdOspActual.View.AddTextBoxColumn("AREAID", 100)
                .SetIsHidden();
            grdOspActual.View.AddTextBoxColumn("AREANAME", 100)
                .SetIsReadOnly();
            grdOspActual.View.AddTextBoxColumn("OSPVENDORID", 80)
                .SetIsReadOnly();
            grdOspActual.View.AddTextBoxColumn("OSPVENDORNAME", 100)
                .SetIsReadOnly();
            grdOspActual.View.AddTextBoxColumn("PRODUCTDEFID", 150)
                .SetIsReadOnly();
            grdOspActual.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80)
                .SetIsReadOnly();
            grdOspActual.View.AddTextBoxColumn("PRODUCTDEFNAME", 200)
                .SetIsReadOnly();

            grdOspActual.View.AddTextBoxColumn("PERFORMANCEDATE", 100)
                .SetIsReadOnly();                              //  
            grdOspActual.View.AddTextBoxColumn("LOTID", 200)
                .SetIsReadOnly();
            grdOspActual.View.AddTextBoxColumn("PCSQTY", 120)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);                                //  pcsqty

            grdOspActual.View.AddTextBoxColumn("PANELQTY", 120)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);                                //  panelqty
            grdOspActual.View.AddTextBoxColumn("DEFECTPCSQTY", 120)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);                                //  pcsqty

            grdOspActual.View.AddSpinEditColumn("OSPPRICE", 100)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0.##", MaskTypes.Numeric);
            grdOspActual.View.AddTextBoxColumn("ACTUALAMOUNT", 120)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);

            grdOspActual.View.AddTextBoxColumn("ISERROR", 80)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(); //오류여부
            grdOspActual.View.AddTextBoxColumn("ISMAJORCLOSE", 80)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(); //
            
            grdOspActual.View.PopulateColumns();
        }
        private void InitializeGridOspEtcWork()
        {
            grdOspEtcWork.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Export | GridButtonItem.Restore;
            grdOspEtcWork.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdOspEtcWork.View.SetIsReadOnly();
            grdOspEtcWork.View.AddTextBoxColumn("WORKGUBUN", 120).SetIsHidden();
            grdOspEtcWork.View.AddTextBoxColumn("PERIODID", 120).SetIsHidden();
            grdOspEtcWork.View.AddTextBoxColumn("ISEXCEPT", 80)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(); //
            grdOspEtcWork.View.AddTextBoxColumn("EXCEPTTIME", 120)
                .SetDisplayFormat("yyyy-MM-dd")
                .SetIsReadOnly();                                                     //  
            grdOspEtcWork.View.AddTextBoxColumn("EXCEPTUSER", 80)
                .SetIsHidden();
            grdOspEtcWork.View.AddTextBoxColumn("EXCEPTUSERNAME", 80)
                .SetIsReadOnly();
            grdOspEtcWork.View.AddComboBoxColumn("PERIODSTATE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=PeriodState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Open")
                .SetIsReadOnly();
            grdOspEtcWork.View.AddTextBoxColumn("ENTERPRISEID", 120)
                .SetIsHidden();                                                          //  회사 ID
            grdOspEtcWork.View.AddTextBoxColumn("PLANTID", 120)
                .SetIsHidden();                                                          //  공장 ID
            grdOspEtcWork.View.AddTextBoxColumn("REQUESTNO", 120)
                .SetIsReadOnly();               // 의뢰번호
            grdOspEtcWork.View.AddComboBoxColumn("OSPETCTYPE", 100
                , new SqlQuery("GetCodeList", "00001", "CODECLASSID=OSPEtcType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsReadOnly(); //작업구분        작업구분           

            grdOspEtcWork.View.AddTextBoxColumn("CUSTOMERID", 120)
                .SetIsHidden();              //  고객사 ID
            grdOspEtcWork.View.AddTextBoxColumn("CUSTOMERNAME", 120).SetIsReadOnly();            //  고객사 명
            grdOspEtcWork.View.AddTextBoxColumn("AREAID", 100)
                .SetIsHidden();
            grdOspEtcWork.View.AddTextBoxColumn("AREANAME", 100)
                .SetIsReadOnly();
            grdOspEtcWork.View.AddTextBoxColumn("OSPVENDORID", 120)
                .SetIsHidden();                //  협력사 ID
            grdOspEtcWork.View.AddTextBoxColumn("OSPVENDORNAME", 120).SetIsReadOnly();              //  협력사 명
            grdOspEtcWork.View.AddTextBoxColumn("REQUESTDEPARTMENT", 120)
                .SetIsHidden();   //    요청부서ID
            grdOspEtcWork.View.AddTextBoxColumn("OSPREQUESTUSER", 120)
                .SetIsHidden();                                                                      //    요청자ID
            grdOspEtcWork.View.AddComboBoxColumn("LOTPRODUCTTYPE", 100
                , new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsReadOnly();   //   양산구분
            grdOspEtcWork.View.AddTextBoxColumn("PRODUCTDEFID", 120)
                .SetIsReadOnly();            //  제품 정의 ID
            grdOspEtcWork.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80)
                .SetIsReadOnly();       //  제품 정의 Version
            grdOspEtcWork.View.AddTextBoxColumn("PRODUCTDEFNAME", 200)
                .SetIsReadOnly();          //  제품명
            grdOspEtcWork.View.AddTextBoxColumn("PROCESSSEGMENTID", 120)
                .SetIsHidden();        //  공정 ID
            grdOspEtcWork.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 120)
                .SetIsReadOnly();    //  공정명
            grdOspEtcWork.View.AddComboBoxColumn("UNIT", 60
                 , new SqlQuery("GetUomDefinitionMapListByOsp", "10001", "UOMCATEGORY=OSP"
                 , $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "UOMDEFID", "UOMDEFID")
                .SetIsReadOnly(); //  단위
            grdOspEtcWork.View.AddTextBoxColumn("SETTLEUSER", 120)
                .SetIsHidden();                                                                          //확정자ID
            grdOspEtcWork.View.AddTextBoxColumn("SETTLEUSERNAME", 120)
                .SetIsHidden();
            grdOspEtcWork.View.AddTextBoxColumn("SETTLEDATE", 120)
                .SetDisplayFormat("yyyy-MM-dd")
                .SetIsReadOnly();                                                        // 확정일자
            grdOspEtcWork.View.AddSpinEditColumn("SETTLEQTY", 120)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsReadOnly();                                          //수량_확정
            grdOspEtcWork.View.AddSpinEditColumn("SETTLEDEFECTQTY", 120)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsReadOnly();                                         //  불량수량_확정
            grdOspEtcWork.View.AddSpinEditColumn("SETTLEPRICE", 120)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsReadOnly();                                         //  단가_확정
            grdOspEtcWork.View.AddTextBoxColumn("ISSETTLEDEFECTACCEPT", 80)
                .SetIsHidden();       //  불량수량적용여부_확정
            grdOspEtcWork.View.AddSpinEditColumn("SETTLEAMOUNT", 120)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsReadOnly();                                        //  금액_확정
            grdOspEtcWork.View.AddTextBoxColumn("SETTLEDESCRIPTION", 200)
                .SetIsReadOnly();
            grdOspEtcWork.View.AddSpinEditColumn("REQUESTQTY", 120)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);                                       // 의뢰수량
            grdOspEtcWork.View.AddSpinEditColumn("REQUESTPRICE", 120)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsHidden();                                        // 의뢰단가
            grdOspEtcWork.View.AddSpinEditColumn("REQUESTAMOUNT", 120)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsHidden();                                        // 의뢰금액

            grdOspEtcWork.View.AddTextBoxColumn("REQUESTDESCRIPTION", 200).SetIsHidden();       // 의뢰설명
            grdOspEtcWork.View.AddTextBoxColumn("OSPREQUESTUSERNAME", 120);           //    요청자명
            grdOspEtcWork.View.AddTextBoxColumn("REQUESTDATE", 120)
                .SetDisplayFormat("yyyy-MM-dd");                                                     //  의뢰일자
            grdOspEtcWork.View.AddTextBoxColumn("ISCHARGE", 80);                  // 유상여부 Check AddCheckBoxColumn
            grdOspEtcWork.View.AddTextBoxColumn("ISREQUESTDEFECTACCEPT", 80);    // 불량적용여부_의뢰
            grdOspEtcWork.View.AddTextBoxColumn("ETCLOTID", 150);                 // 기타작업_LOT_ID

            grdOspEtcWork.View.AddTextBoxColumn("ACTUALDATE", 120)
                .SetDisplayFormat("yyyy-MM-dd"); //   작업일자
            grdOspEtcWork.View.AddTextBoxColumn("ACTUALUSERNAME", 120); // 작업자
            grdOspEtcWork.View.AddSpinEditColumn("ACTUALQTY", 120)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);                                        // 수량_실적
            grdOspEtcWork.View.AddSpinEditColumn("ACTUALDEFECTQTY", 120)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);                                        //  불량수량_실적
            grdOspEtcWork.View.AddSpinEditColumn("ACTUALPRICE", 120)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);                                        //  단가_실적
            grdOspEtcWork.View.AddSpinEditColumn("ACTUALAMOUNT", 120)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);                                         // 금액_실적
            grdOspEtcWork.View.AddTextBoxColumn("ISACTUALDEFECTACCEPT", 80);       // 불량적용여부_실적
            grdOspEtcWork.View.AddTextBoxColumn("ACTUALUSER", 120)
                .SetIsHidden();                                                                        // 작업자

            grdOspEtcWork.View.AddTextBoxColumn("ACTUALDESCRIPTION", 200).SetIsHidden();             // 작업설명

            grdOspEtcWork.View.AddTextBoxColumn("APPROVALUSERNAME", 120)
                .SetIsHidden();                                                                        // 승인자
            grdOspEtcWork.View.AddTextBoxColumn("APPROVALDATE", 120)
                .SetDisplayFormat("yyyy-MM-dd")
                .SetIsHidden();                                                         // 승인일
            grdOspEtcWork.View.AddTextBoxColumn("ISAPPROVALDEFECTACCEP", 80)
                .SetIsHidden();       // 불량수량적용여부_승인

            grdOspEtcWork.View.PopulateColumns();
        }
        private void InitializeGridOspEtcAmount()
        {
            // TODO : 그리드 초기화 로직 추가
            grdOspEtcAmount.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Export | GridButtonItem.Restore;
            grdOspEtcAmount.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdOspEtcAmount.View.SetIsReadOnly();
            grdOspEtcAmount.View.AddTextBoxColumn("WORKGUBUN", 120).SetIsHidden();
            grdOspEtcAmount.View.AddTextBoxColumn("PERIODID", 120).SetIsHidden();
            grdOspEtcAmount.View.AddTextBoxColumn("ISEXCEPT", 80)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(); //
            grdOspEtcAmount.View.AddTextBoxColumn("EXCEPTTIME", 120)
                .SetDisplayFormat("yyyy-MM-dd")
                .SetIsReadOnly();                                                     //  
            grdOspEtcAmount.View.AddTextBoxColumn("EXCEPTUSER", 80)
                .SetIsHidden();
            grdOspEtcAmount.View.AddTextBoxColumn("EXCEPTUSERNAME", 80)
                .SetIsReadOnly();
            grdOspEtcAmount.View.AddComboBoxColumn("PERIODSTATE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=PeriodState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Open")
                .SetIsReadOnly();
            grdOspEtcAmount.View.AddTextBoxColumn("ENTERPRISEID", 120)
                .SetIsHidden();                                                               //  회사 ID
            grdOspEtcAmount.View.AddTextBoxColumn("PLANTID", 120)
                .SetIsHidden();
            grdOspEtcAmount.View.AddTextBoxColumn("EXPSETTLEDATE", 120)
                .SetDisplayFormat("yyyy-MM-dd");                                                //  정산일자
            grdOspEtcAmount.View.AddTextBoxColumn("SEQ", 120);              // 정산번호
            grdOspEtcAmount.View.AddComboBoxColumn("OSPETCTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OSPEtcType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));          //  작업구분           
            grdOspEtcAmount.View.AddTextBoxColumn("EXPSETTLEDEPARTMENT", 120);              // 정산부서
            grdOspEtcAmount.View.AddTextBoxColumn("EXPSETTLEUSER", 120);                 // 정산자ID
            grdOspEtcAmount.View.AddTextBoxColumn("EXPSETTLEUSERNAME", 120);               // 정산자명
            grdOspEtcAmount.View.AddTextBoxColumn("AREAID", 100)
                .SetIsHidden();
            grdOspEtcAmount.View.AddTextBoxColumn("AREANAME", 100)
                .SetIsReadOnly();
            grdOspEtcAmount.View.AddTextBoxColumn("OSPVENDORID", 80);                     // 협력사
            grdOspEtcAmount.View.AddTextBoxColumn("OSPVENDORNAME", 120);                   // 협력사
            grdOspEtcAmount.View.AddTextBoxColumn("ACTUALDATE", 120)
                .SetDisplayFormat("yyyy-MM-dd");                                                                  // 작업일자

            grdOspEtcAmount.View.AddTextBoxColumn("PROCESSSEGMENTID", 80);        //  공정 ID
            grdOspEtcAmount.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 120);    //  공정명
            grdOspEtcAmount.View.AddTextBoxColumn("PRODUCTDEFID", 120);                    // 제품 정의 ID
            grdOspEtcAmount.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80);               // 제품 정의 Version
            grdOspEtcAmount.View.AddTextBoxColumn("PRODUCTDEFNAME", 200);                  // 제품명
            grdOspEtcAmount.View.AddSpinEditColumn("EXPSETTLEAMOUNT", 120)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);                                                   // 정산금액
            grdOspEtcAmount.View.AddTextBoxColumn("ETCDESCRIPTION", 200);                  //  설명
            //grdOspEtcAmount.View.SetAutoFillColumn("ETCDESCRIPTION");
            grdOspEtcAmount.View.PopulateColumns();

        }
        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            btnExclusion.Click += BtnExclusion_Click;
            btnExclusionCancel.Click += BtnExclusionCancel_Click;
        }

        private void BtnExclusionCancel_Click(object sender, EventArgs e)
        {
            if (blExclusionCancelCheck() == false) return;
            DialogResult result = System.Windows.Forms.DialogResult.No;
            DataRow dr = null;
            DataTable dtSave = null;
            result = this.ShowMessage(MessageBoxButtons.YesNo, "OspDoProcessWant", btnExclusionCancel.Text);//저장하시겠습니까? 

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    this.ShowWaitArea();
                    btnExclusion.Enabled = false;
                    btnExclusionCancel.Enabled = false;
                    if (tapIsExcept.SelectedTabPage.Name.Equals("tapOspActual"))
                    {
                        dtSave = (grdOspActual.DataSource as DataTable).Clone();
                        DataTable dtcheck = grdOspActual.View.GetCheckedRows();

                        for (int irow = 0; irow < dtcheck.Rows.Count; irow++)
                        {
                            dr = dtcheck.Rows[irow];
                          
                            if (dr["ISEXCEPT"].ToString().Equals("Y"))
                            {
                                dr["EXCEPTUSER"] = "";
                                dr["ISEXCEPT"] ="N";
                                dtSave.ImportRow(dr);
                            }
                        }
                        this.ExecuteRule("OutsourcingExclusionProcessing", dtSave);
                    }
                    else if (tapIsExcept.SelectedTabPage.Name.Equals("tapOspEtcWork"))
                    {
                        DataTable dtcheck = grdOspEtcWork.View.GetCheckedRows();
                        dtSave = (grdOspEtcWork.DataSource as DataTable).Clone();
                        for (int irow = 0; irow < dtcheck.Rows.Count; irow++)
                        {
                            dr = dtcheck.Rows[irow];

                            if (dr["ISEXCEPT"].ToString().Equals("Y"))
                            {
                                dr["EXCEPTUSER"] = "";
                                dr["ISEXCEPT"] = "N";
                                dtSave.ImportRow(dr);
                            }
                        }
                        this.ExecuteRule("OutsourcingExclusionProcessing", dtSave);
                    }
                    else if (tapIsExcept.SelectedTabPage.Name.Equals("tapOspEtcAmount"))
                    {
                        DataTable dtcheck = grdOspEtcAmount.View.GetCheckedRows();
                        dtSave = (grdOspEtcAmount.DataSource as DataTable).Clone();
                      
                        for (int irow = 0; irow < dtcheck.Rows.Count; irow++)
                        {
                            dr = dtcheck.Rows[irow];

                            if (dr["ISEXCEPT"].ToString().Equals("Y"))
                            {
                                dr["EXCEPTUSER"] = "";
                                dr["ISEXCEPT"] = "N";
                                dtSave.ImportRow(dr);
                            }
                        }
                        this.ExecuteRule("OutsourcingExclusionProcessing", dtSave);
                    }
  
                    ShowMessage("SuccessOspProcess");
                    OnSaveConfrimSearch();

                }
                catch (Exception ex)
                {
                    this.ShowError(ex);
                }
                finally
                {
                    this.CloseWaitArea();

                    btnExclusion.Enabled = true;
                    btnExclusionCancel.Enabled = true;
                    // 재조회 


                }
            }

        }

        private void BtnExclusion_Click(object sender, EventArgs e)
        {
            if (blExclusionCheck()==false) return;
            DialogResult result = System.Windows.Forms.DialogResult.No;
            DataRow dr = null;
            DataTable dtSave = null;
            result = this.ShowMessage(MessageBoxButtons.YesNo, "OspDoProcessWant", btnExclusion.Text);//저장하시겠습니까? 

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    this.ShowWaitArea();
                    btnExclusion.Enabled = false;
                    btnExclusionCancel.Enabled = false;
                    if (tapIsExcept.SelectedTabPage.Name.Equals("tapOspActual"))
                    {
                        dtSave = (grdOspActual.DataSource as DataTable).Clone();
                        DataTable dtcheck = grdOspActual.View.GetCheckedRows();

                        for (int irow = 0; irow < dtcheck.Rows.Count; irow++)
                        {
                            dr = dtcheck.Rows[irow];

                            if (dr["ISEXCEPT"].ToString().Equals("N"))
                            {
                                dr["EXCEPTUSER"] = UserInfo.Current.Id.ToString();
                                dr["ISEXCEPT"] = "Y";
                                dtSave.ImportRow(dr);
                            }
                        }
                        this.ExecuteRule("OutsourcingExclusionProcessing", dtSave);
                    }
                    else if (tapIsExcept.SelectedTabPage.Name.Equals("tapOspEtcWork"))
                    {
                        DataTable dtcheck = grdOspEtcWork.View.GetCheckedRows();
                        dtSave = (grdOspEtcWork.DataSource as DataTable).Clone();
                        for (int irow = 0; irow < dtcheck.Rows.Count; irow++)
                        {
                            dr = dtcheck.Rows[irow];

                            if (dr["ISEXCEPT"].ToString().Equals("N"))
                            {
                                dr["EXCEPTUSER"] = UserInfo.Current.Id.ToString();
                                dr["ISEXCEPT"] = "Y";
                                dtSave.ImportRow(dr);
                            }
                        }
                        this.ExecuteRule("OutsourcingExclusionProcessing", dtSave);
                    }
                    else if (tapIsExcept.SelectedTabPage.Name.Equals("tapOspEtcAmount"))
                    {
                        DataTable dtcheck = grdOspEtcAmount.View.GetCheckedRows();
                        dtSave = (grdOspEtcAmount.DataSource as DataTable).Clone();

                        for (int irow = 0; irow < dtcheck.Rows.Count; irow++)
                        {
                            dr = dtcheck.Rows[irow];

                            if (dr["ISEXCEPT"].ToString().Equals("N"))
                            {
                                dr["EXCEPTUSER"] = UserInfo.Current.Id.ToString();
                                dr["ISEXCEPT"] = "Y";
                                dtSave.ImportRow(dr);
                            }
                        }
                        this.ExecuteRule("OutsourcingExclusionProcessing", dtSave);
                    }

                    ShowMessage("SuccessOspProcess");
                    OnSaveConfrimSearch();

                }
                catch (Exception ex)
                {
                    this.ShowError(ex);
                }
                finally
                {
                    this.CloseWaitArea();

                    btnExclusion.Enabled = true;
                    btnExclusionCancel.Enabled = true;
                    // 재조회 


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
            base.OnToolbarSaveClick();

           
        }
        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            //base.OnToolbarClick(sender, e);
            SmartButton btn = sender as SmartButton;

            if (btn.Name.ToString().Equals("Ospexclusion"))
            {
                
                BtnExclusion_Click(null,null);
            }
            if (btn.Name.ToString().Equals("Ospexclcancel"))
            {
               
                BtnExclusionCancel_Click(null, null);
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
            if (!(values["P_PRODUCTCODE"] == null))
            {
                string sproductcode = values["P_PRODUCTCODE"].ToString();
                // 품목코드값이 있으면
                if (!(sproductcode.Equals("")))
                {
                    string[] sproductd = sproductcode.Split('|');
                    // plant 정보 다시 가져오기 
                    values.Add("P_PRODUCTDEFID", sproductd[0].ToString());
                    values.Add("P_PRODUCTDEFVERSION", sproductd[1].ToString());
                }
            }
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            string stryearmonth = values["P_YEARMONTH"].ToString();
            DateTime dtyearmonth = Convert.ToDateTime(stryearmonth);
            values.Add("P_PERIODNAME", dtyearmonth.ToString("yyyy-MM"));
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable dtPeriod = await SqlExecuter.QueryAsync("GetOutsourcedClaimPeriod", "10001", values);
            if (dtPeriod.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                // 다국어 마감년 정보가 없습니다.
                ShowMessage("InValidOspData008"); // 
                if (tapIsExcept.SelectedTabPage.Name.Equals("tapOspActual"))
                {
                    DataTable dtSearch = (grdOspActual.DataSource as DataTable).Clone();
                    grdOspActual.DataSource = dtSearch;
                }
                else if (tapIsExcept.SelectedTabPage.Name.Equals("tapOspEtcWork"))
                {
                    DataTable dtSearch = (grdOspEtcWork.DataSource as DataTable).Clone();
                    grdOspEtcWork.DataSource = dtSearch;
                }
                else if (tapIsExcept.SelectedTabPage.Name.Equals("tapOspEtcAmount"))
                {
                    DataTable dtSearch = (grdOspEtcAmount.DataSource as DataTable).Clone();
                    grdOspEtcAmount.DataSource = dtSearch;
                }
                
                txtperiod.Text = "";
               
                _strPeriodid = "";
                return;
            }

            string strWorktime = dtPeriod.Rows[0]["WORKTIME"].ToString();
            txtperiod.Text = dtPeriod.Rows[0]["PERIODDESCRIPTION"].ToString();
           
            _strPeriodid = dtPeriod.Rows[0]["PERIODID"].ToString();
            values.Add("P_PERIODID", _strPeriodid);
            values = Commons.CommonFunction.ConvertParameter(values);
            if (tapIsExcept.SelectedTabPage.Name.Equals("tapOspActual"))
            {
                DataTable dt = await SqlExecuter.QueryAsync("GetOutsourcingExclusionProcessingOspActual", "10001", values);

                if (dt.Rows.Count < 1) // 검색 조건에 해당하는 DATA가 없는 경우 
                {
                    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                }

                grdOspActual.DataSource = dt;
            }
            else if (tapIsExcept.SelectedTabPage.Name.Equals("tapOspEtcWork"))
            {
                DataTable dt = await SqlExecuter.QueryAsync("GetOutsourcingExclusionProcessingOspEtcWork", "10001", values);

                if (dt.Rows.Count < 1) // 검색 조건에 해당하는 DATA가 없는 경우 
                {
                    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                }

                grdOspEtcWork.DataSource = dt;
            }
            else   //tapOspEtcAmount
            {
                DataTable dt = await SqlExecuter.QueryAsync("GetOutsourcingExclusionProcessingOspEtcAmount", "10001", values);

                if (dt.Rows.Count < 1) // 검색 조건에 해당하는 DATA가 없는 경우 
                {
                    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                }

                grdOspEtcAmount.DataSource = dt;
            }
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            //1.site
          //  InitializeConditionPopup_Plant();

            InitializeConditionPopup_PeriodTypeOSP();
            //2.마감년월 필수
            InitializeCondition_Yearmonth();
            //공정명 
            InitializeConditionPopup_ProcessSegment();
            //품목코드
            InitializeConditionPopup_ProductDefId();
            //lot id
            InitializeCondition_Lotid();
            //양산구분
            InitializeConditionPopup_ProcessPriceType();
            InitializeConditionPopup_OspAreaid();
            //3.협력사 
            InitializeConditionPopup_OspVendorid();
            //오류여부
            InitializeCondition_YesNo();

        }
        /// <summary>
        /// site 설정 
        /// </summary>
        private void InitializeConditionPopup_Plant()
        {

            var planttxtbox = Conditions.AddComboBox("p_plantid", new SqlQuery("GetPlantList", "00001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PLANTID", "PLANTID")
               .SetLabel("PLANT")
               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false)
               .SetPosition(0.2)
               .SetDefault(UserInfo.Current.Plant, "p_plantId") //기본값 설정 UserInfo.Current.Plant
               .SetIsReadOnly(true)
               .SetValidationIsRequired()
            ;
            //   

        }
        /// <summary>
        ///외주실적
        /// </summary>
        private void InitializeConditionPopup_PeriodTypeOSP()
        {

            var owntypecbobox = Conditions.AddComboBox("p_PeriodType", new SqlQuery("GetCodeList", "00001", "CODECLASSID=PeriodTypeOSP", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
               .SetLabel("PERIODTYPEOSP")
               .SetPosition(0.3)
               .SetValidationIsRequired()
               .SetDefault("OutSourcing") //
            ;
        }
        /// <summary>
        /// 마감년월
        /// </summary>
        private void InitializeCondition_Yearmonth()
        {
            DateTime dateNow = DateTime.Now;
            string strym = dateNow.ToString("yyyy-MM");
            var YearmonthDT = Conditions.AddDateEdit("p_yearmonth")
               .SetLabel("CLOSEYM")
               .SetDisplayFormat("yyyy-MM")
               .SetPosition(0.4)
               .SetDefault(strym)
               .SetValidationIsRequired()
            ;

        }
        /// <summary>
        /// 품목코드 조회조건
        /// </summary>
        private void InitializeConditionPopup_ProductDefId()
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
                 .SetPosition(1.1);
            // 팝업 조회조건
            popupProduct.Conditions.AddComboBox("P_PRODUCTDEFTYPE", new SqlQuery("GetCodeListByOspProductdeftype", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("PRODUCTDEFTYPE")
                .SetDefault("Product");

            popupProduct.Conditions.AddTextBox("PRODUCTDEFNAME")
                .SetLabel("PRODUCTDEFID");


            popupProduct.GridColumns.AddTextBoxColumn("PRODUCTDEFCODE", 150)
                .SetIsHidden();
            popupProduct.GridColumns.AddComboBoxColumn("PRODUCTDEFTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductDefType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsReadOnly();
            popupProduct.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150).SetIsReadOnly();
            popupProduct.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetIsReadOnly();
            popupProduct.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200).SetIsReadOnly();

            var txtProductName = Conditions.AddTextBox("P_PRODUCTDEFNAME")
                .SetLabel("PRODUCTDEFNAME")
                .SetPosition(1.2);
        }
        /// <summary>
        /// ProcessSegment 설정 
        /// </summary>
        private void InitializeConditionPopup_ProcessSegment()
        {
            var ProcessSegmentPopupColumn = Conditions.AddSelectPopup("p_processsegmentid",
                                                               new SqlQuery("GetProcessSegmentListByOsp", "10001"
                                                                               , $"LANGUAGETYPE={ UserInfo.Current.LanguageType }"
                                                                               , $"ENTERPRISEID={UserInfo.Current.Enterprise}"
                                                                               ), "PROCESSSEGMENTNAME", "PROCESSSEGMENTID")
            .SetPopupLayout("PROCESSSEGMENTID", PopupButtonStyles.Ok_Cancel, true, false)
            .SetPopupLayoutForm(500, 600)
            .SetLabel("PROCESSSEGMENTID")
            .SetPopupResultCount(1)
            .SetPosition(2.1);

            // 팝업 조회조건
            ProcessSegmentPopupColumn.Conditions.AddTextBox("PROCESSSEGMENTNAME")
                 .SetLabel("PROCESSSEGMENT");

            // 팝업 그리드
            ProcessSegmentPopupColumn.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150)
                .SetValidationKeyColumn();
            ProcessSegmentPopupColumn.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);

        }
        /// <summary>
        /// lotid 조회조건
        /// </summary>
        private void InitializeCondition_Lotid()
        {
            var txtLotid = Conditions.AddTextBox("p_lotid")
               .SetLabel("LOTID")
               .SetPosition(2.4);
        }
        /// <summary>
        ///양산구분
        /// </summary>
        private void InitializeConditionPopup_ProcessPriceType()
        {

            var owntypecbobox = Conditions.AddComboBox("p_ProcessPriceType", new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProcessPriceType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
               .SetLabel("PROCESSPRICETYPE")
               .SetPosition(2.6)
               .SetEmptyItem("", "")
            ;
           
        }
        /// <summary>
        /// 작업장
        /// </summary>
        private void InitializeConditionPopup_OspAreaid()
        {
            // 팝업 컬럼설정
            var vendoridPopupColumn = Conditions.AddSelectPopup("p_areaid", new SqlQuery("GetAreaidPopupListByOsp", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "AREANAME", "AREAID")
               .SetPopupLayout("AREANAME", PopupButtonStyles.Ok_Cancel, true, false)
               .SetPopupLayoutForm(520, 600)
               .SetLabel("AREANAME")
               .SetRelationIds("p_plantId")
               .SetPopupResultCount(1)
               .SetPosition(2.8);
            // 팝업 조회조건
            vendoridPopupColumn.Conditions.AddTextBox("P_AREANAME")
                .SetLabel("AREANAME");

            // 팝업 그리드
            vendoridPopupColumn.GridColumns.AddTextBoxColumn("AREAID", 120);
            vendoridPopupColumn.GridColumns.AddTextBoxColumn("AREANAME", 200);


        }
        /// <summary>
        /// 작업업체 .고객 조회조건
        /// </summary>
        private void InitializeConditionPopup_OspVendorid()
        {
            // 팝업 컬럼설정
            var vendoridPopupColumn = Conditions.AddSelectPopup("p_ospvendorid", new SqlQuery("GetVendorListByOsp", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "OSPVENDORNAME", "OSPVENDORID")
               .SetPopupLayout("OSPVENDORID", PopupButtonStyles.Ok_Cancel, true, false)
               .SetPopupLayoutForm(500, 600)
               .SetLabel("OSPVENDORID")
               .SetPopupResultCount(1)
                .SetRelationIds("p_plantid", "p_areaid")
               .SetPosition(3.2);

            // 팝업 조회조건
            vendoridPopupColumn.Conditions.AddTextBox("OSPVENDORNAME")
                .SetLabel("OSPVENDORNAME");

            // 팝업 그리드
            vendoridPopupColumn.GridColumns.AddTextBoxColumn("OSPVENDORID", 150)
                .SetValidationKeyColumn();
            vendoridPopupColumn.GridColumns.AddTextBoxColumn("OSPVENDORNAME", 200);

            var txtOspVendor = Conditions.AddTextBox("P_OSPVENDORNAME")
               .SetLabel("OSPVENDORNAME")
               .SetPosition(3.4);

        }

        /// <summary>
        /// 에러여부
        /// </summary>
        private void InitializeCondition_YesNo()
        {
            var YesNobox = Conditions.AddComboBox("p_isexcept", new SqlQuery("GetCodeList", "00001", $"CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                 .SetLabel("ISEXCEPT")
                 .SetPosition(4.0)
                 .SetEmptyItem("전체");

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
        private bool blExclusionCheck()
        {
            int idatacount = 0;
            string strIsexcept = "";
            string strPeriodstate = "";
            DataRow dr = null;
            if (tapIsExcept.SelectedTabPage.Name.Equals("tapOspActual"))
            {
                DataTable dtcheck = grdOspActual.View.GetCheckedRows();

                if (dtcheck.Rows.Count == 0)
                {
                    ShowMessage("GridNoChecked"); //(선택된 내역이 없습니다)/
                    return false;
                }
                for (int irow = 0; irow < dtcheck.Rows.Count; irow++)
                {
                    dr = dtcheck.Rows[irow];
                    strIsexcept = dr["ISEXCEPT"].ToString();
                    strPeriodstate = dr["PERIODSTATE"].ToString();
                    if (!(strPeriodstate.Equals("Open")))
                    {
                        string lblConsumabledefid = grdOspActual.View.Columns["OSPVENDORNAME"].Caption.ToString();

                        this.ShowMessage(MessageBoxButtons.OK, "InValidOspData010", lblConsumabledefid + dr["OSPVENDORNAME"].ToString());
                        return false;

                    }
                    if (strIsexcept.Equals("N"))
                    {
                        idatacount = idatacount + 1;
                    }

                }
            }
            else if (tapIsExcept.SelectedTabPage.Name.Equals("tapOspEtcWork"))
            {
                DataTable dtcheck = grdOspEtcWork.View.GetCheckedRows();

                if (dtcheck.Rows.Count == 0)
                {
                    ShowMessage("GridNoChecked"); //(선택된 내역이 없습니다)/
                    return false;
                }
                for (int irow = 0; irow < dtcheck.Rows.Count; irow++)
                {
                    dr = dtcheck.Rows[irow];
                    strPeriodstate = dr["PERIODSTATE"].ToString();
                    if (!(strPeriodstate.Equals("Open")))
                    {
                        string lblConsumabledefid = grdOspEtcWork.View.Columns["OSPVENDORNAME"].Caption.ToString();

                        this.ShowMessage(MessageBoxButtons.OK, "InValidOspData010", lblConsumabledefid + dr["OSPVENDORNAME"].ToString());
                        return false;

                    }
                    strIsexcept = dr["ISEXCEPT"].ToString();
                    if (strIsexcept.Equals("N"))
                    {
                        idatacount = idatacount + 1;
                    }
                }
            }
            else if (tapIsExcept.SelectedTabPage.Name.Equals("tapOspEtcAmount"))
            {
                DataTable dtcheck = grdOspEtcAmount.View.GetCheckedRows();

                if (dtcheck.Rows.Count == 0)
                {
                    ShowMessage("GridNoChecked"); //(선택된 내역이 없습니다)/
                    return false;
                }
                for (int irow = 0; irow < dtcheck.Rows.Count; irow++)
                {
                    dr = dtcheck.Rows[irow];
                    strPeriodstate = dr["PERIODSTATE"].ToString();
                    if (!(strPeriodstate.Equals("Open")))
                    {
                        string lblConsumabledefid = grdOspEtcAmount.View.Columns["OSPVENDORNAME"].Caption.ToString();

                        this.ShowMessage(MessageBoxButtons.OK, "InValidOspData010", lblConsumabledefid + dr["OSPVENDORNAME"].ToString());
                        return false;

                    }
                    strIsexcept = dr["ISEXCEPT"].ToString();
                    if (strIsexcept.Equals("N"))
                    {
                        idatacount = idatacount + 1;
                    }
                }
            }
            if (idatacount == 0)
            {
                this.ShowMessage(MessageBoxButtons.OK, "OspDataCountCheck", btnExclusion.Text);   //다국어 메세지 처리 
                return false;
            }
            return true;
        }
        private bool blExclusionCancelCheck()
        {
            int idatacount = 0;
            string strIsexcept = "";
            string strPeriodstate = "";
            DataRow dr = null;
            if (tapIsExcept.SelectedTabPage.Name.Equals("tapOspActual"))
            {
                DataTable dtcheck = grdOspActual.View.GetCheckedRows();

                if (dtcheck.Rows.Count == 0)
                {
                    ShowMessage("GridNoChecked"); //(선택된 내역이 없습니다)/
                    return false;
                }
                for (int irow = 0; irow < dtcheck.Rows.Count; irow++)
                {
                    dr = dtcheck.Rows[irow];
                    strPeriodstate = dr["PERIODSTATE"].ToString();
                    if (!(strPeriodstate.Equals("Open")))
                    {
                        string lblConsumabledefid = grdOspActual.View.Columns["OSPVENDORNAME"].Caption.ToString();

                        this.ShowMessage(MessageBoxButtons.OK, "InValidOspData010", lblConsumabledefid + dr["OSPVENDORNAME"].ToString());
                        return false;

                    }
                    strIsexcept = dr["ISEXCEPT"].ToString();
                    if (strIsexcept.Equals("Y"))
                    {
                        idatacount = idatacount + 1;
                    }
                }
            }
            else if (tapIsExcept.SelectedTabPage.Name.Equals("tapOspEtcWork"))
            {
                DataTable dtcheck = grdOspEtcWork.View.GetCheckedRows();

                if (dtcheck.Rows.Count == 0)
                {
                    ShowMessage("GridNoChecked"); //(선택된 내역이 없습니다)/
                    return false;
                }
                for (int irow = 0; irow < dtcheck.Rows.Count; irow++)
                {
                    dr = dtcheck.Rows[irow];
                    strPeriodstate = dr["PERIODSTATE"].ToString();
                    if (!(strPeriodstate.Equals("Open")))
                    {
                        string lblConsumabledefid = grdOspEtcWork.View.Columns["OSPVENDORNAME"].Caption.ToString();

                        this.ShowMessage(MessageBoxButtons.OK, "InValidOspData010", lblConsumabledefid + dr["OSPVENDORNAME"].ToString());
                        return false;

                    }
                    strIsexcept = dr["ISEXCEPT"].ToString();
                    if (strIsexcept.Equals("Y"))
                    {
                        idatacount = idatacount + 1;
                    }
                }
            }
            else if (tapIsExcept.SelectedTabPage.Name.Equals("tapOspEtcAmount"))
            {
                DataTable dtcheck = grdOspEtcAmount.View.GetCheckedRows();

                if (dtcheck.Rows.Count == 0)
                {
                    ShowMessage("GridNoChecked"); //(선택된 내역이 없습니다)/
                    return false;
                }
                for (int irow = 0; irow < dtcheck.Rows.Count; irow++)
                {
                    dr = dtcheck.Rows[irow];
                    strPeriodstate = dr["PERIODSTATE"].ToString();
                    if (!(strPeriodstate.Equals("Open")))
                    {
                        string lblConsumabledefid = grdOspEtcAmount.View.Columns["OSPVENDORNAME"].Caption.ToString();
                       
                        this.ShowMessage(MessageBoxButtons.OK, "InValidOspData010", lblConsumabledefid + dr["OSPVENDORNAME"].ToString());
                        return false;

                    }
                    strIsexcept = dr["ISEXCEPT"].ToString();
                    if (strIsexcept.Equals("Y"))
                    {
                        idatacount = idatacount + 1;
                    }
                }
            }
            if (idatacount==0)
            {
                this.ShowMessage(MessageBoxButtons.OK, "OspDataCountCheck", btnExclusionCancel.Text);   //다국어 메세지 처리 
                return false;
            }
            return true;
        }
        /// <summary>
        /// 저장 후 재조회용 
        /// </summary>

        private void OnSaveConfrimSearch()
        {

            // TODO : 조회 SP 변경
            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            string stryearmonth = values["P_YEARMONTH"].ToString();
            DateTime dtyearmonth = Convert.ToDateTime(stryearmonth);
            values.Add("P_PERIODNAME", dtyearmonth.ToString("yyyy-MM"));
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable dtPeriod = SqlExecuter.Query("GetOutsourcedClaimPeriod", "10001", values);
            if (dtPeriod.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                // 다국어 마감년 정보가 없습니다.
                ShowMessage("InValidOspData008"); // 
                if (tapIsExcept.SelectedTabPage.Name.Equals("tapOspActual"))
                {
                    DataTable dtSearch = (grdOspActual.DataSource as DataTable).Clone();
                    grdOspActual.DataSource = dtSearch;
                }
                else if (tapIsExcept.SelectedTabPage.Name.Equals("tapOspEtcWork"))
                {
                    DataTable dtSearch = (grdOspEtcWork.DataSource as DataTable).Clone();
                    grdOspEtcWork.DataSource = dtSearch;
                }
                else if (tapIsExcept.SelectedTabPage.Name.Equals("tapOspEtcAmount"))
                {
                    DataTable dtSearch = (grdOspEtcAmount.DataSource as DataTable).Clone();
                    grdOspEtcAmount.DataSource = dtSearch;
                }

                txtperiod.Text = "";
               
                _strPeriodid = "";
                return;
            }

            string strWorktime = dtPeriod.Rows[0]["WORKTIME"].ToString();
            txtperiod.Text = dtPeriod.Rows[0]["PERIODDESCRIPTION"].ToString();
           
            _strPeriodid = dtPeriod.Rows[0]["PERIODID"].ToString();
            values.Add("P_PERIODID", _strPeriodid);
            values = Commons.CommonFunction.ConvertParameter(values);
            if (tapIsExcept.SelectedTabPage.Name.Equals("tapOspActual"))
            {
                DataTable dt =  SqlExecuter.Query("GetOutsourcingExclusionProcessingOspActual", "10001", values);

                if (dt.Rows.Count < 1) // 검색 조건에 해당하는 DATA가 없는 경우 
                {
                    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                }

                grdOspActual.DataSource = dt;
            }
            else if (tapIsExcept.SelectedTabPage.Name.Equals("tapOspEtcWork"))
            {
                DataTable dt =  SqlExecuter.Query("GetOutsourcingExclusionProcessingOspEtcWork", "10001", values);

                if (dt.Rows.Count < 1) // 검색 조건에 해당하는 DATA가 없는 경우 
                {
                    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                }

                grdOspEtcWork.DataSource = dt;
            }
            else   //tapOspEtcAmount
            {
                DataTable dt =  SqlExecuter.Query("GetOutsourcingExclusionProcessingOspEtcAmount", "10001", values);

                if (dt.Rows.Count < 1) // 검색 조건에 해당하는 DATA가 없는 경우 
                {
                    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                }

                grdOspEtcAmount.DataSource = dt;
            }


        }

        #endregion
    }
}
