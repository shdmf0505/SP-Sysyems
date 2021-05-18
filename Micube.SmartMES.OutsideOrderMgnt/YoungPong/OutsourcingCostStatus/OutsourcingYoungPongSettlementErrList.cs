#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons.Controls;
using Micube.SmartMES.OutsideOrderMgnt.Popup;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion using

namespace Micube.SmartMES.OutsideOrderMgnt
{
    /// <summary>
    /// 프 로 그 램 명  :  외주관리> 외주비현황 > 외주비정산 Err List
    /// 업  무  설  명  :  외주비정산 Err List
    /// 생    성    자  : CHOISSTAR
    /// 생    성    일  : 2019-09-17
    /// 수  정  사  항  : 2021-05-14 전우성 전면 개편 및 화면 재구성
    ///
    ///
    /// </summary>
    public partial class OutsourcingYoungPongSettlementErrList : SmartConditionManualBaseForm
    {
        #region 생성자

        public OutsourcingYoungPongSettlementErrList()
        {
            InitializeComponent();
        }

        #endregion 생성자

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 화면의 컨텐츠 영역을 초기화한다.
        /// </summary>
        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeLanguageKey();
            InitializeGrid();
            InitializeEvent();
        }

        /// <summary>
        /// Language Key 초기화
        /// </summary>
        private void InitializeLanguageKey()
        {
            grdMaster.LanguageKey = "OUTSOURCINGSETTLEMENTERRMASTER";
            grdSearch.LanguageKey = "OUTSOURCINGSETTLEMENTERRDETAIL";
            grdSearchDetail.LanguageKey = "OUTSOURCINGCOSTSTATUSLISTDETAIL";
            grdSearchReduce.LanguageKey = "OUTSOURCINGCOSTSTATUSLISTREDUCE";
        }

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            #region 협력사별 외주비 목록

            grdMaster.GridButtonItem = GridButtonItem.Export;
            grdMaster.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            var group = grdMaster.View.AddGroupColumn("OSPCOSTSBYDATE");

            group.AddTextBoxColumn("OSPVENDORID", 80).SetIsHidden();
            group.AddTextBoxColumn("OSPVENDORNAME", 100);
            group.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 120);
            group.AddTextBoxColumn("PROCESSSEGMENTID", 150).SetIsHidden();
            group.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 120);
            group.AddTextBoxColumn("PROCESSSEGMENTNAME", 150);

            group = grdMaster.View.AddGroupColumn("OSPLOTTYPEPRD");
            group.AddSpinEditColumn("OSPPRODUCTIONCNT", 120).SetLabel("OSPCLOSECOUNT");
            group.AddSpinEditColumn("OSPPRODUCTIONPNLQTY", 120).SetLabel("PNL");
            group.AddSpinEditColumn("OSPPRODUCTIONPCSQTY", 120).SetLabel("PCS");
            group.AddSpinEditColumn("OSPPRODUCTIONM2QTY", 120).SetLabel("M2");
            group.AddSpinEditColumn("OSPPRODUCTIONDEFECTQTY", 120).SetLabel("DEFECT");
            group.AddSpinEditColumn("OSPPRODUCTIONDSETTLEAMOUNT", 120).SetLabel("OSPAMOUNT");

            group = grdMaster.View.AddGroupColumn("OSPLOTTYPESIM");

            group.AddSpinEditColumn("OSPSAMPLECNT", 120).SetLabel("OSPCLOSECOUNT");
            group.AddSpinEditColumn("OSPSAMPLEPNLQTY", 120).SetLabel("PNL");
            group.AddSpinEditColumn("OSPSAMPLEPCSQTY", 120).SetLabel("PCS");
            group.AddSpinEditColumn("OSPSAMPLEM2QTY", 120).SetLabel("M2");
            group.AddSpinEditColumn("OSPSAMPLEDEFECTQTY", 120).SetLabel("DEFECT");
            group.AddSpinEditColumn("OSPSAMPLESETTLEAMOUNT", 120).SetLabel("OSPAMOUNT");

            grdMaster.View.PopulateColumns();

            grdMaster.View.SetIsReadOnly();
            grdMaster.View.OptionsView.ShowFooter = true;
            grdMaster.ShowStatusBar = false;

            #region 협력사별 외주비 목록 Summary

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

            #endregion 협력사별 외주비 목록 Summary



            #endregion 협력사별 외주비 목록

            #region LOT별 외주비 목록

            grdSearch.GridButtonItem = GridButtonItem.Export;
            grdSearch.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdSearch.View.AddTextBoxColumn("PLANTID").SetIsHidden();
            grdSearch.View.AddTextBoxColumn("LOTHISTKEY").SetIsHidden();
            grdSearch.View.AddTextBoxColumn("ENTERPRISEID").SetIsHidden();
            grdSearch.View.AddTextBoxColumn("SENDTIME").SetIsHidden();
            grdSearch.View.AddTextBoxColumn("AREAID").SetIsHidden();
            grdSearch.View.AddTextBoxColumn("USERID").SetIsHidden();

            grdSearch.View.AddTextBoxColumn("PERIODSTATE", 100);
            grdSearch.View.AddTextBoxColumn("PROCESSSEGMENTID", 100);
            grdSearch.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150);
            grdSearch.View.AddTextBoxColumn("AREANAME", 100);
            grdSearch.View.AddTextBoxColumn("OSPVENDORID", 80);
            grdSearch.View.AddTextBoxColumn("OSPVENDORNAME", 100);
            grdSearch.View.AddTextBoxColumn("PRODUCTDEFID", 150);
            grdSearch.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80);
            grdSearch.View.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            grdSearch.View.AddTextBoxColumn("PRODUCTIONTYPENAME", 80);
            grdSearch.View.AddSpinEditColumn("OSPPRICE", 100);
            grdSearch.View.AddTextBoxColumn("PERFORMANCEDATE", 100);
            grdSearch.View.AddTextBoxColumn("LOTID", 200);
            grdSearch.View.AddSpinEditColumn("PCSQTY", 120);
            grdSearch.View.AddSpinEditColumn("PANELQTY", 120);
            grdSearch.View.AddSpinEditColumn("OSPMM", 120);
            grdSearch.View.AddSpinEditColumn("DEFECTQTY", 120).SetLabel("DEFECTPCSQTY");
            grdSearch.View.AddSpinEditColumn("SETTLEAMOUNT", 120);

            grdSearch.View.PopulateColumns();

            grdSearch.View.SetIsReadOnly();
            grdSearch.View.OptionsView.ShowFooter = true;
            grdSearch.ShowStatusBar = false;

            #region LOT별 외주비 목록 Summary

            grdSearch.View.Columns["LOTID"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdSearch.View.Columns["LOTID"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));

            grdSearch.View.Columns["PCSQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdSearch.View.Columns["PCSQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdSearch.View.Columns["PANELQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdSearch.View.Columns["PANELQTY"].SummaryItem.DisplayFormat = "{0:###,###.####}";

            grdSearch.View.Columns["OSPMM"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdSearch.View.Columns["OSPMM"].SummaryItem.DisplayFormat = "{0:###,###.####}";

            grdSearch.View.Columns["DEFECTQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdSearch.View.Columns["DEFECTQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdSearch.View.Columns["SETTLEAMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdSearch.View.Columns["SETTLEAMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";

            #endregion LOT별 외주비 목록 Summary

            #endregion LOT별 외주비 목록

            #region 외주비 산출내역

            grdSearchDetail.GridButtonItem = GridButtonItem.Export;
            grdSearchDetail.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            grdSearchDetail.View.AddTextBoxColumn("PLANTID", 200).SetIsHidden();
            grdSearchDetail.View.AddTextBoxColumn("LOTHISTKEY", 200).SetIsHidden();
            grdSearchDetail.View.AddTextBoxColumn("OSPPRICECODE", 100).SetIsHidden();
            grdSearchDetail.View.AddTextBoxColumn("OSPPRICENAME", 150).SetIsReadOnly();
            grdSearchDetail.View.AddTextBoxColumn("LOTID", 200).SetIsHidden();

            grdSearchDetail.View.AddTextBoxColumn("SPECUNIT", 80).SetLabel("CALCULATEUNIT");
            grdSearchDetail.View.AddTextBoxColumn("DESCRIPTION", 200).SetLabel("SPECVALUE");
            grdSearchDetail.View.AddTextBoxColumn("CALCULATEUNIT", 100).SetLabel("PRICEUNIT");
            grdSearchDetail.View.AddSpinEditColumn("CALCULATEQTY", 120);
            grdSearchDetail.View.AddSpinEditColumn("OSPPRICE", 100).SetDisplayFormat("{0:N4}");
            grdSearchDetail.View.AddSpinEditColumn("ACTUALAMOUNT", 100);
            grdSearchDetail.View.AddTextBoxColumn("ISERROR", 80);
            grdSearchDetail.View.AddTextBoxColumn("ERRORCOMMENT", 400);
            grdSearchDetail.View.PopulateColumns();

            grdSearchDetail.View.SetIsReadOnly();

            #endregion 외주비 산출내역

            #region 외주비 할인 내역

            grdSearchReduce.GridButtonItem = GridButtonItem.Export;
            grdSearchReduce.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            grdSearchReduce.View.AddTextBoxColumn("PLANTID", 200).SetIsHidden();
            grdSearchReduce.View.AddTextBoxColumn("LOTHISTKEY", 200).SetIsHidden();
            grdSearchReduce.View.AddTextBoxColumn("ENTERPRISEID", 200).SetIsHidden();
            grdSearchReduce.View.AddTextBoxColumn("PRODUCTDEFID", 150).SetIsHidden();
            grdSearchReduce.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetIsHidden();
            grdSearchReduce.View.AddTextBoxColumn("PRODUCTDEFNAME", 200).SetIsHidden();

            grdSearchReduce.View.AddSpinEditColumn("OSPAMOUNT", 100);
            grdSearchReduce.View.AddSpinEditColumn("REDUCERATE", 100);
            grdSearchReduce.View.AddSpinEditColumn("REDUCEAMOUNT", 120);
            grdSearchReduce.View.AddSpinEditColumn("SETTLEAMOUNT", 120);
            grdSearchReduce.View.AddTextBoxColumn("PROCESSSEGMENTID", 100);
            grdSearchReduce.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150);
            grdSearchReduce.View.AddTextBoxColumn("OSPVENDORID", 80);
            grdSearchReduce.View.AddTextBoxColumn("OSPVENDORNAME", 100);
            grdSearchReduce.View.AddTextBoxColumn("MODELID", 100);

            grdSearchReduce.View.PopulateColumns();

            grdSearchReduce.View.SetIsReadOnly();

            #endregion 외주비 할인 내역
        }

        #endregion 컨텐츠 영역 초기화

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            grdMaster.View.FocusedRowChanged += (s, e) =>
            {
                grdSearch.View.ClearDatas();
                grdSearchDetail.View.ClearDatas();
                grdSearchReduce.View.ClearDatas();

                if (grdMaster.View.FocusedRowHandle < 0)
                {
                    return;
                }

                var values = Conditions.GetValues();
                Dictionary<string, object> Param = new Dictionary<string, object>
                {
                    { "P_PLANTID", values["P_PLANTID"] },
                    { "P_PERIODTYPE", values["P_PERIODTYPE"] },
                    { "P_ISERROR", values["P_ISERROR"] },
                    { "P_OSPVENDORID", grdMaster.View.GetFocusedRowCellValue("OSPVENDORID") },
                    { "P_PROCESSSEGMENTCLASSID", grdMaster.View.GetFocusedRowCellValue("PROCESSSEGMENTCLASSID") },
                    { "P_PROCESSSEGMENTID", grdMaster.View.GetFocusedRowCellValue("PROCESSSEGMENTID") },
                    { "LANGUAGETYPE", UserInfo.Current.LanguageType },
                    { "USERID", UserInfo.Current.Id}
                };

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

                #endregion 기간 검색형 전환 처리

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

                #endregion 품목코드 전환 처리

                Param = Commons.CommonFunction.ConvertParameter(Param);

                if (SqlExecuter.Query("GetOutsourcingYoungPongSettlementErrList", "10001", Param) is DataTable dt)
                {
                    grdSearch.DataSource = dt;
                }
            };

            grdSearch.View.FocusedRowChanged += (s, e) =>
            {
                grdSearchDetail.View.ClearDatas();
                grdSearchReduce.View.ClearDatas();

                if (grdSearch.View.FocusedRowHandle < 0)
                {
                    return;
                }

                Dictionary<string, object> Param = new Dictionary<string, object>
                {
                    { "P_PLANTID", grdSearch.View.GetFocusedRowCellValue("PLANTID") },
                    { "P_LOTHISTKEY", grdSearch.View.GetFocusedRowCellValue("LOTHISTKEY") },
                    { "P_LOTID", grdSearch.View.GetFocusedRowCellValue("LOTID") },
                    { "LANGUAGETYPE", UserInfo.Current.LanguageType }
                };

                Param = Commons.CommonFunction.ConvertParameter(Param);

                if (SqlExecuter.Query("GetOutsourcingYoungPongCostStatusListDetail", "10001", Param) is DataTable dtDetail)
                {
                    grdSearchDetail.DataSource = dtDetail;
                }

                if (SqlExecuter.Query("GetOutsourcinggYoungPongCostStatusListReduce", "10001", Param) is DataTable dtReduce)
                {
                    grdSearchReduce.DataSource = dtReduce;
                }
            };
        }

        #endregion Event

        #region 툴바

        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            SmartButton btn = sender as SmartButton;

            if (btn.Name.Equals("Recalculation"))
            {
                DataTable dt = grdSearch.View.GetCheckedRows();

                if (dt.Rows.Count.Equals(0))
                {
                    ShowMessage(MessageBoxButtons.OK, "GridNoChecked");
                    return;
                }

                dt.AsEnumerable().Where(x => x.Field<string>("PERIODSTATE") != "Open").ForEach(x =>
                {
                    ShowMessage(MessageBoxButtons.OK, "InValidOspData010", "Lot No. : " + x["LOTID"]);
                    return;
                });

                if (ShowMessage(MessageBoxButtons.YesNo, "OspDoProcessWant", Language.Get("RECALCULATION")).Equals(DialogResult.Yes)) // {0} 처리하시겠습니까? 재계산
                {
                    try
                    {
                        this.ShowWaitArea();

                        MessageWorker worker = new MessageWorker("OutsourcedSettlementErrList");
                        worker.SetBody(new MessageBody()
                        {
                            { "list", dt }
                        });

                        worker.ExecuteWithTimeout(300);
                        ShowMessage("SuccessOspProcess");

                        SendKeys.Send("{F5}");
                    }
                    catch (Exception ex)
                    {
                        throw MessageException.Create(Format.GetString(ex));
                    }
                    finally
                    {
                        this.CloseWaitArea();
                    }
                }
            }
            else if (btn.Name.ToString().Equals("BatchRecalculation"))
            {
                OutsideFinishingEexpensePopup itemPopup = new OutsideFinishingEexpensePopup();
                itemPopup.ShowDialog();
            }
        }

        #endregion 툴바

        #region 검색

        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            try
            {
                DialogManager.ShowWaitArea(this.pnlContent);

                await base.OnSearchAsync();

                var values = Conditions.GetValues();
                values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

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

                #endregion 기간 검색형 전환 처리

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

                #endregion 품목코드 전환 처리

                values = Commons.CommonFunction.ConvertParameter(values);

                grdMaster.View.ClearDatas();
                grdSearch.View.ClearDatas();
                grdSearchDetail.View.ClearDatas();
                grdSearchReduce.View.ClearDatas();

                if (await SqlExecuter.QueryAsync("GetOutsourcingYoungPongSettlementErrMaster", "10001", values) is DataTable dt)
                {
                    if (dt.Rows.Count.Equals(0))
                    {
                        ShowMessage("NoSelectData");
                        return;
                    }

                    grdMaster.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                throw MessageException.Create(Format.GetString(ex));
            }
            finally
            {
                DialogManager.CloseWaitArea(this.pnlContent);
            }
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            Dictionary<string, object> param = new Dictionary<string, object>()
            {
                { "CODECLASSID", "PeriodTypeOSP" },
                { "LANGUAGETYPE", UserInfo.Current.LanguageType },
                { "ENTERPRISEID", UserInfo.Current.Enterprise },
            };

            // 외주실적
            Conditions.AddComboBox("p_PeriodType", new SqlQuery("GetCodeList", "00001", param))
                      .SetLabel("PERIODTYPEOSP")
                      .SetPosition(0.3)
                      .SetEmptyItem("", "")
                      .SetDefault("");

            #region 협력사

            var condition = Conditions.AddSelectPopup("p_ospvendorid", new SqlQuery("GetVendorListByOsp", "10001", param), "OSPVENDORNAME", "OSPVENDORID")
                                      .SetPopupLayout("OSPVENDORID", PopupButtonStyles.Ok_Cancel, true, false)
                                      .SetPopupLayoutForm(500, 600)
                                      .SetLabel("OSPVENDORID")
                                      .SetPopupResultCount(1)
                                      .SetRelationIds("p_plantid")
                                      .SetPosition(1.6);

            condition.Conditions.AddTextBox("OSPVENDORNAME").SetLabel("OSPVENDORNAME");

            condition.GridColumns.AddTextBoxColumn("OSPVENDORID", 150);
            condition.GridColumns.AddTextBoxColumn("OSPVENDORNAME", 200);

            Conditions.AddTextBox("P_OSPVENDORNAME").SetLabel("OSPVENDORNAME").SetPosition(1.7);

            #endregion 협력사

            #region 품목코드

            condition = Conditions.AddSelectPopup("p_productcode", new SqlQuery("GetProductdefidlistByOsp", "10001", param), "PRODUCTDEFNAME", "PRODUCTDEFCODE")
                                  .SetPopupLayout("PRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)
                                  .SetPopupLayoutForm(650, 600)
                                  .SetLabel("PRODUCTDEFID")
                                  .SetPopupResultCount(1)
                                  .SetPosition(2.1);

            condition.Conditions.AddComboBox("P_PRODUCTDEFTYPE", new SqlQuery("GetCodeListByOspProductdeftype", "10001", param))
                                .SetLabel("PRODUCTDEFTYPE")
                                .SetDefault("Product");

            condition.Conditions.AddTextBox("PRODUCTDEFNAME").SetLabel("PRODUCTDEFID");

            condition.GridColumns.AddTextBoxColumn("PRODUCTDEFCODE", 150);
            condition.GridColumns.AddComboBoxColumn("PRODUCTDEFTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductDefType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            condition.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            condition.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 80);
            condition.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);

            Conditions.AddTextBox("P_PRODUCTDEFNAME").SetLabel("PRODUCTDEFNAME").SetPosition(2.2);

            #endregion 품목코드

            #region 중공정

            condition = Conditions.AddSelectPopup("p_processsegmentclassid", new SqlQuery("GetProcesssegmentclassidListByOsp", "10001", param), "MIDDLEPROCESSSEGMENTCLASSNAME", "MIDDLEPROCESSSEGMENTCLASSID")
                                  .SetPopupLayout("MIDDLEPROCESSSEGMENTCLASS", PopupButtonStyles.Ok_Cancel, true, false)
                                  .SetPopupLayoutForm(400, 600)
                                  .SetLabel("MIDDLEPROCESSSEGMENTCLASS")
                                  .SetPopupResultCount(1)
                                  .SetPosition(3.2);

            condition.Conditions.AddTextBox("MIDDLEPROCESSSEGMENTCLASSNAME").SetLabel("MIDDLEPROCESSSEGMENTCLASSNAME");

            condition.GridColumns.AddTextBoxColumn("MIDDLEPROCESSSEGMENTCLASSID", 150);
            condition.GridColumns.AddTextBoxColumn("MIDDLEPROCESSSEGMENTCLASSNAME", 200);

            #endregion 중공정

            #region 공정

            condition = Conditions.AddSelectPopup("p_processsegmentid", new SqlQuery("GetProcessSegmentListByOsp", "10001", param), "PROCESSSEGMENTNAME", "PROCESSSEGMENTID")
                                  .SetPopupLayout("PROCESSSEGMENTID", PopupButtonStyles.Ok_Cancel, true, false)
                                  .SetPopupLayoutForm(500, 600)
                                  .SetLabel("PROCESSSEGMENTID")
                                  .SetRelationIds("P_PROCESSSEGMENTCLASSID")
                                  .SetPopupResultCount(1)
                                  .SetPosition(3.4);

            condition.Conditions.AddTextBox("PROCESSSEGMENTNAME").SetLabel("PROCESSSEGMENT");

            condition.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150);
            condition.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);

            #endregion 공정

            param["CODECLASSID"] = "YesNo";

            //에러여부
            Conditions.AddComboBox("p_ISERROR", new SqlQuery("GetCodeListNotCodebyStd", "10001", param))
                      .SetLabel("ISERROR")
                      .SetPosition(3.8)
                      .SetDefault("Y");
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            Conditions.GetControl<SmartPeriodEdit>("P_SEARCHDATE").LanguageKey = "SENDPERIOD";
        }

        #endregion 검색
    }
}