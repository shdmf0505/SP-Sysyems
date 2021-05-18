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
using DevExpress.XtraGrid.Views.Base;
using Micube.SmartMES.OutsideOrderMgnt.Popup;

#endregion

namespace Micube.SmartMES.OutsideOrderMgnt
{
    /// <summary>
    /// 프 로 그 램 명  :  외주관리> 외주비현황 > 외주비 현황 
    /// 업  무  설  명  :  외주비정산 Err List
    /// 생    성    자  : CHOISSTAR
    /// 생    성    일  : 2019-09-17
    /// 
    /// 
    /// 
    /// </summary>
    public partial class OutsourcingYoungPongCostStatusList : SmartConditionManualBaseForm
    {
        #region Local Variables

        string _ISERROR = "N";
        #endregion

        #region 생성자

        public OutsourcingYoungPongCostStatusList()
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
        private void InitializeGrid()
        {
            InitializeGrid_Master();
            InitializeGrid_Detail();
            InitializeGrid_SubDetail();
            InitializeGrid_Reduce();
        }

        private void InitializeGrid_SubDetail()
        {
            // TODO : 그리드 초기화 로직 추가
            grdSearchDetail.GridButtonItem = GridButtonItem.Export | GridButtonItem.Expand | GridButtonItem.Restore;
            grdSearchDetail.View.SetIsReadOnly();
            grdSearchDetail.View.AddTextBoxColumn("PLANTID", 200)
                .SetIsHidden();
            grdSearchDetail.View.AddTextBoxColumn("LOTHISTKEY", 200)
               .SetIsHidden();

            grdSearchDetail.View.AddTextBoxColumn("OSPPRICECODE", 100)
                .SetIsHidden();
            grdSearchDetail.View.AddTextBoxColumn("OSPPRICENAME", 150)
                .SetIsReadOnly();
            //  
            grdSearchDetail.View.AddTextBoxColumn("LOTID", 200)
                .SetIsHidden();

            grdSearchDetail.View.AddTextBoxColumn("SPECUNIT", 80)
                .SetLabel("CALCULATEUNIT")
                .SetIsReadOnly();  //  //사양단위
            grdSearchDetail.View.AddTextBoxColumn("DESCRIPTION", 200)
                .SetLabel("SPECVALUE")
                .SetIsReadOnly();
            //사양값
            grdSearchDetail.View.AddTextBoxColumn("CALCULATEUNIT", 100)
                .SetIsReadOnly()
                .SetLabel("PRICEUNIT");  // //계산단위

            grdSearchDetail.View.AddTextBoxColumn("CALCULATEQTY", 120)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);  //계산값
            grdSearchDetail.View.AddTextBoxColumn("OSPPRICE", 100)
               .SetIsReadOnly()
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            grdSearchDetail.View.AddTextBoxColumn("ACTUALAMOUNT", 100)
               .SetIsReadOnly()
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            grdSearchDetail.View.AddTextBoxColumn("ISERROR", 80)
                .SetIsReadOnly(); //오류여부
            grdSearchDetail.View.AddTextBoxColumn("ERRORCOMMENT", 400)
                .SetIsReadOnly();//
            grdSearchDetail.View.PopulateColumns();
        }


        private void InitializeGrid_Reduce()
        {
            grdSearchReduce.GridButtonItem = GridButtonItem.Export | GridButtonItem.Expand | GridButtonItem.Restore;
            grdSearchReduce.View.AddTextBoxColumn("OSPAMOUNT", 100)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            grdSearchReduce.View.AddTextBoxColumn("REDUCERATE", 100)
               .SetIsReadOnly()
               .SetTextAlignment(TextAlignment.Right)
               .SetDisplayFormat("#,##0.##", MaskTypes.Numeric);
            grdSearchReduce.View.AddTextBoxColumn("REDUCEAMOUNT", 120)
               .SetIsReadOnly()
               .SetTextAlignment(TextAlignment.Right)
               .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            grdSearchReduce.View.AddTextBoxColumn("SETTLEAMOUNT", 120)
               .SetIsReadOnly()
               .SetTextAlignment(TextAlignment.Right)
               .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            grdSearchReduce.View.AddTextBoxColumn("PLANTID", 200)
                .SetIsHidden();
            grdSearchReduce.View.AddTextBoxColumn("LOTHISTKEY", 200)
               .SetIsHidden();
            grdSearchReduce.View.AddTextBoxColumn("ENTERPRISEID", 200)
                .SetIsHidden();                                                           //  공장 ID
            grdSearchReduce.View.AddTextBoxColumn("PROCESSSEGMENTID", 100)
                .SetIsReadOnly();
            grdSearchReduce.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150)
                .SetIsReadOnly();

            grdSearchReduce.View.AddTextBoxColumn("OSPVENDORID", 80)
                .SetIsReadOnly();
            grdSearchReduce.View.AddTextBoxColumn("OSPVENDORNAME", 100)
                .SetIsReadOnly();
            grdSearchReduce.View.AddTextBoxColumn("PRODUCTDEFID", 150)
                .SetIsHidden();
            grdSearchReduce.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80)
                .SetIsHidden();
            grdSearchReduce.View.AddTextBoxColumn("PRODUCTDEFNAME", 200)
                .SetIsHidden();
            grdSearchReduce.View.AddTextBoxColumn("MODELID", 100)
               .SetIsReadOnly();
            grdSearchReduce.View.PopulateColumns();
        }
        private void InitializeGrid_Master()
        {
            // TODO : 그리드 초기화 로직 추가
            grdMaster.GridButtonItem = GridButtonItem.Export | GridButtonItem.Expand | GridButtonItem.Restore;
            grdMaster.View.SetIsReadOnly();
            var costprocess = grdMaster.View.AddGroupColumn("OSPCOSTSBYDATE");//공정및 협력사정보
            costprocess.AddTextBoxColumn("OSPVENDORID", 80).SetIsHidden();
            costprocess.AddTextBoxColumn("OSPVENDORNAME", 100);
            //costprocess.AddTextBoxColumn("AREAID", 80).SetIsHidden();
            //costprocess.AddTextBoxColumn("AREANAME", 100);
            costprocess.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 120);
            costprocess.AddTextBoxColumn("PROCESSSEGMENTID", 150).SetIsHidden();
            costprocess.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 120);
            costprocess.AddTextBoxColumn("PROCESSSEGMENTNAME", 150);
            var costtotlotprd = grdMaster.View.AddGroupColumn("OSPLOTTYPEPRD");
            costtotlotprd.AddTextBoxColumn("OSPPRODUCTIONCNT", 80)
              .SetLabel("OSPCLOSECOUNT")
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costtotlotprd.AddTextBoxColumn("OSPPRODUCTIONPNLQTY", 80)
              .SetLabel("PNL")
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costtotlotprd.AddTextBoxColumn("OSPPRODUCTIONPCSQTY", 80)
              .SetLabel("PCS")
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costtotlotprd.AddTextBoxColumn("OSPPRODUCTIONM2QTY", 80)
              .SetLabel("M2")
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            costtotlotprd.AddTextBoxColumn("OSPPRODUCTIONDEFECTQTY", 80)
              .SetLabel("DEFECT")
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);

            costtotlotprd.AddTextBoxColumn("OSPPRODUCTIONDSETTLEAMOUNT", 120)
              .SetLabel("OSPAMOUNT")
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);                        //양산 외주비

            costtotlotprd.AddTextBoxColumn("OSPPRODUCTIONDSETTLEAMOUNTETC", 120)
              .SetLabel("OEAAMOUNT")
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);                        //양산 기타외주비

            costtotlotprd.AddTextBoxColumn("OSPPRODUCTIONDSETTLEAMOUNTCLAIM", 120)
              .SetLabel("OSPLOTTYPEPRDCLAIM")
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);                        //양산 CLAIM

            //양산(pnl,pcs,mm ,외주비 ,기타외주비 )
            var costtotlotsim = grdMaster.View.AddGroupColumn("OSPLOTTYPESIM");

            costtotlotsim.AddTextBoxColumn("OSPSAMPLECNT", 80)
              .SetLabel("OSPCLOSECOUNT")
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costtotlotsim.AddTextBoxColumn("OSPSAMPLEPNLQTY", 80)
              .SetLabel("PNL")
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);

            costtotlotsim.AddTextBoxColumn("OSPSAMPLEPCSQTY", 80)
              .SetLabel("PCS")
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);

            costtotlotsim.AddTextBoxColumn("OSPSAMPLEM2QTY", 80)
              .SetLabel("M2")
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);

            costtotlotsim.AddTextBoxColumn("OSPSAMPLEDEFECTQTY", 80)
              .SetLabel("DEFECT")
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);

            costtotlotsim.AddTextBoxColumn("OSPSAMPLESETTLEAMOUNT", 100)
              .SetLabel("OSPAMOUNT")
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);                        //샘플 외주비

            costtotlotsim.AddTextBoxColumn("OSPSAMPLESETTLEAMOUNTETC", 120)
              .SetLabel("OEAAMOUNT")
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);                        //샘플 기타외주비

            costtotlotsim.AddTextBoxColumn("OSPSAMPLESETTLEAMOUNTCLAIM", 120)
              .SetLabel("OSPLOTTYPESIMCLAIM")
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);                        //샘플 CLAIM


            grdMaster.View.PopulateColumns();
            InitializationSummaryRow();
        }
        private void InitializationSummaryRow()
        {
            grdMaster.View.Columns["PROCESSSEGMENTNAME"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdMaster.View.Columns["PROCESSSEGMENTNAME"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));

            grdMaster.View.Columns["OSPPRODUCTIONCNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdMaster.View.Columns["OSPPRODUCTIONCNT"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdMaster.View.Columns["OSPPRODUCTIONPNLQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdMaster.View.Columns["OSPPRODUCTIONPNLQTY"].SummaryItem.DisplayFormat = "{0:###,###.####}";

            grdMaster.View.Columns["OSPPRODUCTIONPCSQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdMaster.View.Columns["OSPPRODUCTIONPCSQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdMaster.View.Columns["OSPPRODUCTIONM2QTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdMaster.View.Columns["OSPPRODUCTIONM2QTY"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdMaster.View.Columns["OSPPRODUCTIONDEFECTQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdMaster.View.Columns["OSPPRODUCTIONDEFECTQTY"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdMaster.View.Columns["OSPPRODUCTIONDSETTLEAMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdMaster.View.Columns["OSPPRODUCTIONDSETTLEAMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdMaster.View.Columns["OSPPRODUCTIONDSETTLEAMOUNTETC"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdMaster.View.Columns["OSPPRODUCTIONDSETTLEAMOUNTETC"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdMaster.View.Columns["OSPPRODUCTIONDSETTLEAMOUNTCLAIM"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdMaster.View.Columns["OSPPRODUCTIONDSETTLEAMOUNTCLAIM"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdMaster.View.Columns["OSPSAMPLECNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdMaster.View.Columns["OSPSAMPLECNT"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdMaster.View.Columns["OSPSAMPLEPNLQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdMaster.View.Columns["OSPSAMPLEPNLQTY"].SummaryItem.DisplayFormat = "{0:###,###.####}";

            grdMaster.View.Columns["OSPSAMPLEPCSQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdMaster.View.Columns["OSPSAMPLEPCSQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdMaster.View.Columns["OSPSAMPLEM2QTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdMaster.View.Columns["OSPSAMPLEM2QTY"].SummaryItem.DisplayFormat = "{0:###,###.####}";

            grdMaster.View.Columns["OSPSAMPLEDEFECTQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdMaster.View.Columns["OSPSAMPLEDEFECTQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdMaster.View.Columns["OSPSAMPLESETTLEAMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdMaster.View.Columns["OSPSAMPLESETTLEAMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdMaster.View.Columns["OSPSAMPLESETTLEAMOUNTETC"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdMaster.View.Columns["OSPSAMPLESETTLEAMOUNTETC"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdMaster.View.Columns["OSPSAMPLESETTLEAMOUNTCLAIM"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdMaster.View.Columns["OSPSAMPLESETTLEAMOUNTCLAIM"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdMaster.View.OptionsView.ShowFooter = true;
            grdMaster.ShowStatusBar = false;
        }
        /// <summary>        
        /// 코드그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid_Detail()
        {
            // TODO : 그리드 초기화 로직 추가
            grdSearch.GridButtonItem = GridButtonItem.Export | GridButtonItem.Expand | GridButtonItem.Restore;
            //  grdSearch.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdSearch.View.SetIsReadOnly();
            grdSearch.View.AddTextBoxColumn("PLANTID", 200)
                .SetIsHidden();
            grdSearch.View.AddTextBoxColumn("LOTHISTKEY", 200)
               .SetIsHidden();
            grdSearch.View.AddTextBoxColumn("ENTERPRISEID", 200)
                .SetIsHidden();
            grdSearch.View.AddTextBoxColumn("PERIODSTATE", 100);

            grdSearch.View.AddTextBoxColumn("PROCESSSEGMENTID", 100)
                .SetIsReadOnly();
            grdSearch.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150)
                .SetIsReadOnly();
            grdSearch.View.AddTextBoxColumn("AREAID", 100)
                .SetIsHidden();
            grdSearch.View.AddTextBoxColumn("AREANAME", 100)
                .SetIsReadOnly();

            grdSearch.View.AddTextBoxColumn("OSPVENDORID", 80)
                .SetIsReadOnly();
            grdSearch.View.AddTextBoxColumn("OSPVENDORNAME", 100)
                .SetIsReadOnly();
            grdSearch.View.AddTextBoxColumn("PRODUCTDEFID", 150)
                .SetIsReadOnly();
            grdSearch.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80)
                .SetIsReadOnly();
            grdSearch.View.AddTextBoxColumn("PRODUCTDEFNAME", 200)
                .SetIsReadOnly();
            grdSearch.View.AddTextBoxColumn("PRODUCTIONTYPENAME", 80)
                .SetIsReadOnly();
            grdSearch.View.AddTextBoxColumn("OSPPRICE", 100)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0.###", MaskTypes.Numeric);
            grdSearch.View.AddTextBoxColumn("PERFORMANCEDATE", 100)
                .SetIsReadOnly();                              //  
            grdSearch.View.AddTextBoxColumn("LOTID", 200)
                .SetIsReadOnly();
            grdSearch.View.AddTextBoxColumn("PCSQTY", 120)
               .SetIsReadOnly()
               .SetTextAlignment(TextAlignment.Right)
               .SetDisplayFormat("#,##0", MaskTypes.Numeric);                                //  pcsqty

            grdSearch.View.AddTextBoxColumn("PANELQTY", 120)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);                                //  panelqty
            grdSearch.View.AddTextBoxColumn("OSPMM", 120)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);                                //  panelqty
            grdSearch.View.AddTextBoxColumn("DEFECTPCSQTY", 120)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);                                //  pcsqty
            grdSearch.View.AddTextBoxColumn("SETTLEAMOUNT", 120)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);                                //  pcsqty
            grdSearch.View.PopulateColumns();
            InitializationSummaryRowSearch();
        }
        private void InitializationSummaryRowSearch()
        {
            grdSearch.View.Columns["LOTID"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdSearch.View.Columns["LOTID"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));
            grdSearch.View.Columns["PCSQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdSearch.View.Columns["PCSQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdSearch.View.Columns["PANELQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdSearch.View.Columns["PANELQTY"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdSearch.View.Columns["OSPMM"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdSearch.View.Columns["OSPMM"].SummaryItem.DisplayFormat = "{0:###,###.####}";

            grdSearch.View.Columns["DEFECTPCSQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdSearch.View.Columns["DEFECTPCSQTY"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdSearch.View.Columns["SETTLEAMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdSearch.View.Columns["SETTLEAMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdSearch.View.OptionsView.ShowFooter = true;
            grdSearch.ShowStatusBar = false;
        }
        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            btnAggregate.Click += BtnAggregate_Click;
            grdMaster.View.FocusedRowChanged += GrdMaster_FocusedRowChanged;

            grdSearch.View.FocusedRowChanged += GrdSearch_FocusedRowChanged;
            //grdSearch.View.ColumnFilterChanged += View_ColumnFilterChanged;
            grdSearch.View.RowCellClick += View_RowCellClick;
        }
        /// <summary>
        /// 그리드의 Row Click시 조회
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            focusedRowChangedDetail();
        }
        private void View_ColumnFilterChanged(object sender, EventArgs e)
        {

            focusedRowChangedMaster();
        }
        private void GrdSearch_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

            focusedRowChangedDetail();
        }
        /// <summary>
        /// 외주비 계산 행 ㄷ이동 처리 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdMaster_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

            focusedRowChangedMaster();
        }
        #endregion
        /// <summary>
        /// 외주비집계 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAggregate_Click(object sender, EventArgs e)
        {
            if (blAggregateCheck() == false) return;

            //_strPeriodid 
            DialogResult result = System.Windows.Forms.DialogResult.No;

            result = this.ShowMessage(MessageBoxButtons.YesNo, "OspDoProcessWant", btnAggregate.Text);//저장하시겠습니까? 

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    this.ShowWaitArea();

                    btnAggregate.Enabled = false;
                    DataTable dt = grdSearch.View.GetCheckedRows();

                    if (dt.Rows.Count > 0)
                    {
                        DataTable dtsave = createSaveDatatable();
                        dt.DefaultView.Sort = "LOTID ASC ,LOTHISTKEY ASC";
                        string strLotid = "";
                        string strLothistkey = "";
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DataRow row = dt.Rows[i];
                            if (!(strLotid.Equals(row["LOTID"].ToString()) && strLothistkey.Equals(row["LOTHISTKEY"].ToString())))
                            {
                                DataRow dr = dtsave.NewRow();
                                dr["ENTERPRISEID"] = row["ENTERPRISEID"].ToString();
                                dr["LOTID"] = row["LOTID"].ToString();
                                dr["LOTHISTKEY"] = row["LOTHISTKEY"].ToString();
                                dr["PLANTID"] = row["PLANTID"].ToString();
                                dr["AREAID"] = row["AREAID"].ToString();
                                dr["VENDORID"] = row["OSPVENDORID"].ToString();
                                dr["VENDORNAME"] = row["OSPVENDORNAME"].ToString();
                                dr["PERFORMANCEDATE"] = row["PERFORMANCEDATE"].ToString();
                                dr["ENTERPRISEID"] = row["ENTERPRISEID"].ToString();
                                dr["USERID"] = UserInfo.Current.Id.ToString();
                                strLotid = row["LOTID"].ToString();
                                strLothistkey = row["LOTHISTKEY"].ToString();
                                dtsave.Rows.Add(dr);
                            }
                        }
                        ExecuteRule("OutsourcedSettlementErrList", dtsave);
                        ShowMessage("SuccessOspProcess");
                        OnSaveConfrimSearch();
                    }
                }
                catch (Exception ex)
                {
                    this.ShowError(ex);
                }
                finally
                {
                    this.CloseWaitArea();

                    btnAggregate.Enabled = true;


                }
            }

        }
        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            //base.OnToolbarSaveClick();
            ////

            //DataTable dt = grdSearch.View.GetCheckedRows();

            //if (dt.Rows.Count > 0)
            //{
            //    DataTable dtsave = createSaveDatatable();
            //    dt.DefaultView.Sort = "LOTID ASC ,LOTHISTKEY ASC";
            //    string strLotid = "";
            //    string strLothistkey = "";
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        DataRow row = dt.Rows[i];
            //        if (!(strLotid.Equals(row["LOTID"].ToString()) && strLothistkey.Equals(row["LOTHISTKEY"].ToString())))
            //        {
            //            DataRow dr = dtsave.NewRow();
            //            dr["LOTID"] = row["LOTID"].ToString();
            //            dr["LOTHISTKEY"] = row["LOTHISTKEY"].ToString();
            //            dr["PLANTID"] = row["PLANTID"].ToString();
            //            dr["AREAID"] = row["AREAID"].ToString();
            //            dr["VENDORID"] = row["OSPVENDORID"].ToString();
            //            dr["VENDORNAME"] = row["OSPVENDORNAME"].ToString();
            //            dr["PERFORMANCEDATE"] = row["PERFORMANCEDATE"].ToString();
            //            strLotid = row["LOTID"].ToString();
            //            strLothistkey = row["LOTHISTKEY"].ToString();
            //            dtsave.Rows.Add(dr);

            //        }

            //    }

            //    ExecuteRule("OutsourcedSettlementErrList", dtsave);
            //}

        }
        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            //base.OnToolbarClick(sender, e);
            SmartButton btn = sender as SmartButton;

            if (btn.Name.ToString().Equals("Recalculation"))
            {

                BtnAggregate_Click(null, null);
            }
            if (btn.Name.ToString().Equals("BatchRecalculation"))
            {

                ProcBatchRecalculation(btn.Text);
            }
        }
        private void ProcBatchRecalculation(string strtitle)
        {
            var values = Conditions.GetValues();
            string strPlantid = values["P_PLANTID"].ToString();
            OutsideFinishingEexpensePopup itemPopup = new OutsideFinishingEexpensePopup();
            itemPopup.ShowDialog(this);
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
            if (values["P_SEARCHDATE_PERIODFR"].ToString().Equals("") && values["P_SEARCHDATE_PERIODFR"].ToString().Equals("")
                && values["P_YEARMONTH"].ToString().Equals(""))
            {
                ShowMessage("NoConditions_03"); // 검색조건중 (정산기간은 입력해야합니다.다국어
                return;
            }
            
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
                values.Remove("P_SEARCHDATE_PERIODTO");
                //requestDateTo = requestDateTo.AddDays(1);
                values.Add("P_SEARCHDATE_PERIODTO", string.Format("{0:yyyy-MM-dd}", requestDateTo));
            }
            #endregion
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("P_ISERROR", _ISERROR);
           
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

            values = Commons.CommonFunction.ConvertParameter(values);
            //DataTable dtSearchMaster = await SqlExecuter.QueryAsync("GetOutsourcingYoungPongCostStatusList", "10001", values);
            DataTable dtSearchMaster = await SqlExecuter.QueryAsync("GetOutsourcingYoungPongCostStatusList2", "10002", values);
            grdSearch.View.ClearDatas();
            grdSearchDetail.View.ClearDatas();
            grdSearchReduce.View.ClearDatas();

            grdMaster.DataSource = dtSearchMaster;
            if (dtSearchMaster.Rows.Count < 1)
            {
                // 조회할 데이터가 없습니다.
                ShowMessage("NoSelectData");
               
            }

            if (dtSearchMaster.Rows.Count == 1)
            {
                grdMaster.View.FocusedRowHandle = 0;
                grdMaster.View.SelectRow(0);
                focusedRowChangedMaster();
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
            // InitializeConditionPopup_Plant();

            InitializeConditionPopup_PeriodTypeOSP();
            //2.마감년월 필수
            InitializeCondition_Yearmonth();
            //InitializeConditionPopup_OspAreaid();
            //3.협력사 
            InitializeConditionPopup_OspVendorid();
            //품목코드
            InitializeConditionPopup_ProductDefId();
            // 중공정 추가 
            InitializeConditionPopup_Processsegmentclassid();
            //공정명 
            InitializeConditionPopup_ProcessSegment();
          
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
                .SetDefault("")
            //.SetDefault("OutSourcing")
            //.SetValidationIsRequired()
            //
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
               .SetPosition(1.1)
              // .SetDefault(strym)
               //.SetValidationIsRequired()
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
               .SetPosition(1.2);
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
               .SetRelationIds("p_plantid")
               .SetPosition(1.6);

            // 팝업 조회조건
            vendoridPopupColumn.Conditions.AddTextBox("OSPVENDORNAME")
                .SetLabel("OSPVENDORNAME");

            // 팝업 그리드
            vendoridPopupColumn.GridColumns.AddTextBoxColumn("OSPVENDORID", 150)
                .SetValidationKeyColumn();
            vendoridPopupColumn.GridColumns.AddTextBoxColumn("OSPVENDORNAME", 200);

            var txtOspVendor = Conditions.AddTextBox("P_OSPVENDORNAME")
               .SetLabel("OSPVENDORNAME")
               .SetPosition(1.7);

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
                 .SetPosition(2.1);
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
                .SetPosition(2.2);
        }
        private void InitializeConditionPopup_Processsegmentclassid()
        {

            // 팝업 컬럼설정
            var processsegmentclassidPopupColumn = Conditions.AddSelectPopup("p_processsegmentclassid", new SqlQuery("GetProcesssegmentclassidListByOsp", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "MIDDLEPROCESSSEGMENTCLASSNAME", "MIDDLEPROCESSSEGMENTCLASSID")
               .SetPopupLayout("MIDDLEPROCESSSEGMENTCLASS", PopupButtonStyles.Ok_Cancel, true, false)
               .SetPopupLayoutForm(400, 600)
               .SetLabel("MIDDLEPROCESSSEGMENTCLASS")
               .SetPopupResultCount(1)
               .SetPosition(3.2);

            // 팝업 조회조건
            processsegmentclassidPopupColumn.Conditions.AddTextBox("MIDDLEPROCESSSEGMENTCLASSNAME")
                .SetLabel("MIDDLEPROCESSSEGMENTCLASSNAME");

            // 팝업 그리드
            processsegmentclassidPopupColumn.GridColumns.AddTextBoxColumn("MIDDLEPROCESSSEGMENTCLASSID", 150)
                .SetValidationKeyColumn();
            processsegmentclassidPopupColumn.GridColumns.AddTextBoxColumn("MIDDLEPROCESSSEGMENTCLASSNAME", 200);

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
            .SetRelationIds("P_PROCESSSEGMENTCLASSID")
            .SetPopupResultCount(1)
            .SetPosition(3.4);

            // 팝업 조회조건
            ProcessSegmentPopupColumn.Conditions.AddTextBox("PROCESSSEGMENTNAME")
                .SetLabel("PROCESSSEGMENT");

            // 팝업 그리드
            ProcessSegmentPopupColumn.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150)
                .SetValidationKeyColumn();
            ProcessSegmentPopupColumn.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);

        }

        private void InitializeConditionPopup_ISERROR()
        {
            var plantCbobox = Conditions.AddComboBox("p_ISERROR", new SqlQuery("GetCodeListNotCodebyStd", "10001", new Dictionary<string, object>() { { "CODECLASSID", "YesNo" }, { "LANGUAGETYPE", UserInfo.Current.LanguageType } }), "CODENAME", "CODEID")
              .SetLabel("ISERROR")
              .SetPosition(3.8)
              .SetDefault("Y")
               ;
        }
        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
            Conditions.GetControl<SmartPeriodEdit>("P_SEARCHDATE").LanguageKey = "EXPSETTLEDATE";
        }

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
        /// </summary>
        protected override void OnValidateContent()
        {
            //base.OnValidateContent();

            //// TODO : 유효성 로직 변경
            //grdSearch.View.FocusedRowHandle = grdSearch.View.FocusedRowHandle;
            //grdSearch.View.FocusedColumn = grdSearch.View.Columns["OSPPRICENAME"];
            //grdSearch.View.ShowEditor();
            //DataTable dtcheck = grdSearch.View.GetCheckedRows();

            //if (dtcheck.Rows.Count == 0)
            //{

            //    throw MessageException.Create("GridNoChecked");

            //}


            //string strPeriodstate = "";
            //int idatacount = 0;
            //DataRow dr = null;
            ////진행상태값 체크 처리 
            //for (int irow = 0; irow < dtcheck.Rows.Count; irow++)
            //{
            //    dr = dtcheck.Rows[irow];
            //    strPeriodstate = dr["PERIODSTATE"].ToString();

            //    if (!(strPeriodstate.Equals("Open")))
            //    {
            //        throw MessageException.Create("InValidOspData010","Lot No. : "+ dr["LOTID"].ToString());


            //    }
            //    else
            //    {
            //        idatacount = idatacount + 1;
            //    }
            //}
            ////승인 대상 건 유무확인 처리 
            //if (idatacount == 0)
            //{
            //    throw MessageException.Create ("InValidOspData011");   //다국어 메세지 처리 

            //}
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
        /// 저장및  삭제용 data table 생성 
        /// </summary>
        /// <returns></returns>
        private DataTable createSaveDatatable()
        {
            DataTable dt = new DataTable();
            dt.TableName = "list";
            dt.Columns.Add("ENTERPRISEID");
            dt.Columns.Add("PLANTID");
            dt.Columns.Add("AREAID");
            dt.Columns.Add("VENDORID");
            dt.Columns.Add("VENDORNAME");
            dt.Columns.Add("LOTID");
            dt.Columns.Add("LOTHISTKEY");
            dt.Columns.Add("PERFORMANCEDATE");
            dt.Columns.Add("USERID");
            dt.Columns.Add("_STATE_");
            return dt;
        }
        private void OnSaveConfrimSearch()
        {

            focusedRowChangedMaster();

        }
        private bool blAggregateCheck()
        {
            int idatacount = 0;
            DataRow dr = null;
            grdSearch.View.FocusedRowHandle = grdSearch.View.FocusedRowHandle;
            grdSearch.View.FocusedColumn = grdSearch.View.Columns["OSPPRICENAME"];
            grdSearch.View.ShowEditor();
            DataTable dtcheck = grdSearch.View.GetCheckedRows();

            if (dtcheck.Rows.Count == 0)
            {

                this.ShowMessage(MessageBoxButtons.OK, "GridNoChecked");
                return false;
            }


            string strPeriodstate = "";

            //진행상태값 체크 처리 
            for (int irow = 0; irow < dtcheck.Rows.Count; irow++)
            {
                dr = dtcheck.Rows[irow];
                strPeriodstate = dr["PERIODSTATE"].ToString();

                if (!(strPeriodstate.Equals("Open")))
                {
                    this.ShowMessage(MessageBoxButtons.OK, "InValidOspData010", "Lot No. : " + dr["LOTID"].ToString());
                    return false;

                }
                else
                {
                    idatacount = idatacount + 1;
                }
            }

            if (idatacount == 0)
            {
                this.ShowMessage(MessageBoxButtons.OK, "OspDataCountCheck", btnAggregate.Text);   //다국어 메세지 처리 
                return false;
            }
            return true;
        }


        /// <summary>
        /// 그리드의 포커스 행 변경시 행의 작업목록의 내용을 매핑처리 함.
        /// </summary>
        private void focusedRowChangedMaster()
        {
            //포커스 행 체크 
            grdSearch.View.ClearDatas();
            grdSearchDetail.View.ClearDatas();
            grdSearchReduce.View.ClearDatas();
            if (grdMaster.View.FocusedRowHandle < 0)
            {

                return;
            }
            var values = Conditions.GetValues();
            Dictionary<string, object> Param = new Dictionary<string, object>();
            Param.Add("P_PLANTID", values["P_PLANTID"].ToString());
            #region 기간 검색형 전환 처리 
            if (!(values["P_SEARCHDATE_PERIODFR"].ToString().Equals("")))
            {
                DateTime requestDateFr = Convert.ToDateTime(values["P_SEARCHDATE_PERIODFR"]);
                values.Remove("P_SEARCHDATE_PERIODFR");
                Param.Add("P_SEARCHDATE_PERIODFR", string.Format("{0:yyyy-MM-dd}", requestDateFr));
            }
            if (!(values["P_SEARCHDATE_PERIODTO"].ToString().Equals("")))
            {
                DateTime requestDateTo = Convert.ToDateTime(values["P_SEARCHDATE_PERIODTO"]);
                values.Remove("P_SEARCHDATE_PERIODTO");
                // requestDateTo = requestDateTo.AddDays(1);
                Param.Add("P_SEARCHDATE_PERIODTO", string.Format("{0:yyyy-MM-dd}", requestDateTo));
            }
            Param.Add("P_ISERROR", _ISERROR);
            #endregion

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
                Param.Add("P_PRODUCTDEFID", sproductd[0].ToString());
                Param.Add("P_PRODUCTDEFVERSION", sproductd[1].ToString());
            }

            #endregion

            Param.Add("P_PERIODTYPE", values["P_PERIODTYPE"].ToString());
            Param.Add("P_OSPVENDORID", grdMaster.View.GetFocusedRowCellValue("OSPVENDORID"));
           // Param.Add("P_AREAID", grdMaster.View.GetFocusedRowCellValue("AREAID"));
            Param.Add("P_PROCESSSEGMENTCLASSID", grdMaster.View.GetFocusedRowCellValue("PROCESSSEGMENTCLASSID"));
            Param.Add("P_PROCESSSEGMENTID", grdMaster.View.GetFocusedRowCellValue("PROCESSSEGMENTID"));

            Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            Param = Commons.CommonFunction.ConvertParameter(Param);

            DataTable dtDetail = SqlExecuter.Query("GetOutsourcingYoungPongSettlementErrList", "10001", Param);
            grdSearch.DataSource = dtDetail;

            if (dtDetail.Rows.Count > 0)
            {
                grdSearch.View.FocusedRowHandle = 0;
                grdSearch.View.SelectRow(0);
                focusedRowChangedDetail();
            }
        }

        private void focusedRowChangedDetail()
        {
            //포커스 행 체크 
            if (grdSearch.View.FocusedRowHandle < 0)
            {
                grdSearchDetail.View.ClearDatas();
                grdSearchReduce.View.ClearDatas();
                return;
            }
            Dictionary<string, object> Param = new Dictionary<string, object>();
            Param.Add("P_PLANTID", grdSearch.View.GetFocusedRowCellValue("PLANTID"));
            Param.Add("P_LOTHISTKEY", grdSearch.View.GetFocusedRowCellValue("LOTHISTKEY"));
            Param.Add("P_LOTID", grdSearch.View.GetFocusedRowCellValue("LOTID"));
            Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
           
            Param = Commons.CommonFunction.ConvertParameter(Param);
            DataTable dtDetail = SqlExecuter.Query("GetOutsourcingYoungPongCostStatusListDetail", "10001", Param);
            grdSearchDetail.DataSource = dtDetail;
            DataTable dtReduce = SqlExecuter.Query("GetOutsourcinggYoungPongCostStatusListReduce", "10001", Param);
            grdSearchReduce.DataSource = dtReduce;

        }
        private DataTable createSavErrListDatatable()
        {
            DataTable dt = new DataTable();
            dt.TableName = "list";
            dt.Columns.Add("ENTERPRISEID");
            dt.Columns.Add("PLANTID");
            dt.Columns.Add("LOTID");
            dt.Columns.Add("LOTHISTKEY");
            dt.Columns.Add("USERID");

            dt.Columns.Add("_STATE_");
            return dt;
        }
        #endregion
    }
}
