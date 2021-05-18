#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System.Drawing;

#endregion using

namespace Micube.SmartMES.OutsideOrderMgnt
{
    /// <summary>
    /// 프 로 그 램 명  : 외주관리 > 외주가공비마감 > 이행율상세조회
    /// 업  무  설  명  : 외주가공비의 이행율을 상세조회한다.
    /// 생    성    자  : 김용조
    /// 생    성    일  : 2019-10-07
    /// 수  정  이  력  :
    ///
    ///
    /// </summary>
    public partial class OutsourcingFulfillmentRateBrowse : SmartConditionManualBaseForm
    {
        #region 생성자

        public OutsourcingFulfillmentRateBrowse()
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

            InitializeEvent();
        }

        private void InitializeGrid()
        {
            //grdMain
            grdMain.GridButtonItem = GridButtonItem.Export;
            // 검색조건에 따른 Column 구성 변경
            if (Format.GetString(Conditions.GetValues()["P_STANDARDMETHOD"], string.Empty).Equals("ByProcess"))
            {
                grdMain.View.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 120)
                            .SetIsHidden();   //  공정그룹ID
                grdMain.View.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 200);   //  공정그룹명
                grdMain.View.AddTextBoxColumn("SPECSUBTYPE", 120)
                            .SetIsHidden();
                grdMain.View.AddTextBoxColumn("SPECSUBTYPENAME", 200)
                            .SetIsHidden();
            }
            else
            {
                grdMain.View.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 120)
                            .SetIsHidden();   //  공정그룹ID
                grdMain.View.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 200)
                            .SetIsHidden();   //  공정그룹명
                grdMain.View.AddTextBoxColumn("SPECSUBTYPE", 120)
                            .SetIsHidden();
                grdMain.View.AddTextBoxColumn("SPECSUBTYPENAME", 200);
            }
            grdMain.View.AddTextBoxColumn("OSPVENDORID", 120)
                        .SetIsHidden();
            grdMain.View.AddTextBoxColumn("OSPVENDORNAME", 200);
            grdMain.View.AddComboBoxColumn("STANDARDUNIT", 120, new SqlQuery("GetCodeList", "00001", "CODECLASSID=StandardUnit", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                        .SetDefault(false);                 // PNL/M2/AMOUNT
            grdMain.View.AddComboBoxColumn("PRODUCTIONTYPE", 130, new SqlQuery("GetOutsourcedDistributionSearchConditionProductionType", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                        .SetDefault(false)
                        .SetLabel("PRODUCTIONTYPENAME");    // 양산/샘플
            grdMain.View.AddComboBoxColumn("MASTERDATACLASSID", 150, new SqlQuery("GetOutsourcedDistributionSearchConditionMasterdataclassid", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                        .SetDefault(false)
                        .SetLabel("PRODUCTDEFTYPENAME");    // 제품/반제품
            grdMain.View.AddSpinEditColumn("ALLOCATERATE", 120)
                        .SetTextAlignment(TextAlignment.Right)
                        .SetDisplayFormat("{0:f0} %", MaskTypes.Numeric);  // 배분율
            grdMain.View.AddSpinEditColumn("SENDPANELQTYMON", 120)
                        .SetTextAlignment(TextAlignment.Right)
                        .SetDisplayFormat("#,##0.##", MaskTypes.Numeric);
            grdMain.View.AddSpinEditColumn("SENDPANELQTYMONRATE", 120)
                        .SetTextAlignment(TextAlignment.Right)
                        .SetDisplayFormat("{0:f2} %", MaskTypes.Numeric);
            grdMain.View.AddSpinEditColumn("SENDPANELQTYDAY", 120)
                        .SetTextAlignment(TextAlignment.Right)
                        .SetDisplayFormat("#,##0.##", MaskTypes.Numeric);
            grdMain.View.AddSpinEditColumn("SENDPANELQTYDAYRATE", 120)
                        .SetTextAlignment(TextAlignment.Right)
                        .SetDisplayFormat("{0:f2} %", MaskTypes.Numeric);
            grdMain.View.AddSpinEditColumn("SENDPANELQTYMONSUM", 120)
                        .SetTextAlignment(TextAlignment.Right)
                        .SetDisplayFormat("#,##0.##", MaskTypes.Numeric)
                        .SetIsHidden();
            grdMain.View.AddSpinEditColumn("SENDPANELQTYDAYSUM", 120)
                        .SetTextAlignment(TextAlignment.Right)
                        .SetDisplayFormat("#,##0.##", MaskTypes.Numeric)
                        .SetIsHidden();
            grdMain.View.PopulateColumns();
            grdMain.View.SetIsReadOnly();

            //grdSub
            grdSub.GridButtonItem = GridButtonItem.Export;
            grdSub.View.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 120)
                       .SetIsHidden();
            grdSub.View.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 200);
            grdSub.View.AddTextBoxColumn("vendorid", 120)
                       .SetIsHidden();
            grdSub.View.AddTextBoxColumn("vendorname", 200);
            grdSub.View.AddTextBoxColumn("PRODUCTIONTYPE", 120)
                       .SetIsHidden();
            grdSub.View.AddTextBoxColumn("PRODUCTIONTYPENAME", 130);
            grdSub.View.AddTextBoxColumn("PRODUCTDEFTYPE", 100)
                       .SetIsHidden();
            grdSub.View.AddTextBoxColumn("PRODUCTDEFTYPENAME", 150);
            grdSub.View.AddSpinEditColumn("PANELQTY", 120);
            grdSub.View.AddSpinEditColumn("PCSQTY", 120);
            grdSub.View.AddSpinEditColumn("M2QTY", 120);
            grdSub.View.AddSpinEditColumn("SETTLEAMOUNT", 120);
            grdSub.View.PopulateColumns();
            grdSub.View.SetIsReadOnly();
        }

        #endregion 컨텐츠 영역 초기화

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // 검색조건 기준방식에 따른 ReadOnly 처리 이벤트
            ((SmartComboBox)Conditions.GetControl("P_STANDARDMETHOD")).EditValueChanged += (s, e) =>
            {
                SmartComboBox edit = s as SmartComboBox;

                if (Format.GetFullTrimString(edit.EditValue).Equals("ByProcess"))
                {
                    ((SmartSelectPopupEdit)Conditions.GetControl("p_TOPPROCESSSEGMENTCLASSID")).ReadOnly = false;
                    ((SmartSelectPopupEdit)Conditions.GetControl("p_processsegmentclassid")).ReadOnly = false;
                    ((SmartSelectPopupEdit)Conditions.GetControl("PROCESSSEGMENTID")).ReadOnly = false;
                    ((SmartComboBox)Conditions.GetControl("P_SPECSUBTYPENAME")).ReadOnly = true;
                    //((SmartComboBox)Conditions.GetControl("P_PRODUCTIONTYPE")).ReadOnly = false;
                    //((SmartComboBox)Conditions.GetControl("P_MASTERDATACLASSID")).ReadOnly = false;
                }
                else
                {
                    ((SmartSelectPopupEdit)Conditions.GetControl("p_TOPPROCESSSEGMENTCLASSID")).ReadOnly = true;
                    ((SmartSelectPopupEdit)Conditions.GetControl("p_processsegmentclassid")).ReadOnly = true;
                    ((SmartSelectPopupEdit)Conditions.GetControl("PROCESSSEGMENTID")).ReadOnly = true;
                    ((SmartComboBox)Conditions.GetControl("P_SPECSUBTYPENAME")).ReadOnly = false;
                    //((SmartComboBox)Conditions.GetControl("P_PRODUCTIONTYPE")).ReadOnly = true;
                    //((SmartComboBox)Conditions.GetControl("P_MASTERDATACLASSID")).ReadOnly = true;
                }

                ((SmartComboBox)Conditions.GetControl("P_SPECSUBTYPENAME")).EditValue = "*";
                ((SmartSelectPopupEdit)Conditions.GetControl("p_TOPPROCESSSEGMENTCLASSID")).EditValue = string.Empty;
                ((SmartSelectPopupEdit)Conditions.GetControl("p_processsegmentclassid")).EditValue = string.Empty;
                ((SmartSelectPopupEdit)Conditions.GetControl("PROCESSSEGMENTID")).EditValue = string.Empty;
            };

            grdMain.View.RowStyle += View_RowStyle;
            //grdMain.View.RowCellStyle += View_RowCellStyle;


            // 이행율 목록 포커스 이동시
            grdMain.View.FocusedRowChanged += (s, e) =>
            {
                if (grdMain.View.FocusedRowHandle < 0)
                {
                    return;
                }

                if (e.FocusedRowHandle.Equals(e.PrevFocusedRowHandle))
                {
                    return;
                }

                try
                {
                    //DialogManager.ShowWaitArea(this.pnlContent);

                    // TODO : 조회 SP 변경
                    var values = Conditions.GetValues();
                    DateTime standdate = Convert.ToDateTime(values["P_STANDARDDATE"]);
                    DateTime standlast = new DateTime(standdate.Year, standdate.Month, 1).AddMonths(1);

                    Dictionary<string, object> param = new Dictionary<string, object>
                    {
                        { "P_PROCESSSEGMENTCLASSID", grdMain.View.GetFocusedRowCellValue("PROCESSSEGMENTCLASSID") },
                        { "P_STARTMONTH", string.Format("{0:yyyy-MM-01}", standdate) },
                        { "P_ENDMONTH", string.Format("{0:yyyy-MM-01}", standlast) },
                        { "LANGUAGETYPE", UserInfo.Current.LanguageType },
                        { "P_STANDARDUNIT", grdMain.View.GetFocusedRowCellValue("STANDARDUNIT") },
                        { "P_PRODUCTIONTYPE", grdMain.View.GetFocusedRowCellValue("PRODUCTIONTYPE") },
                        { "P_PRODUCTDEFTYPE", grdMain.View.GetFocusedRowCellValue("MASTERDATACLASSID") },
                        { "P_VENDORID", grdMain.View.GetFocusedRowCellValue("OSPVENDORID") }
                    };

                    #region Sub Vendorid DataTable

                    if (Conditions.GetValues()["P_STANDARDMETHOD"].ToString().Equals("ByProcess"))
                    {
                        param["P_PROCESSSEGMENTCLASSID"] = Format.GetString(grdMain.View.GetFocusedRowCellValue("PROCESSSEGMENTCLASSID"), string.Empty);
                        grdSub.DataSource = SqlExecuter.Query("GetOutsourcingFulfillmentRateBrowseSub", "10001", param);
                    }
                    else
                    {
                        param.Add("P_SPECSUBTYPE", Format.GetString(grdMain.View.GetFocusedRowCellValue("SPECSUBTYPE"), string.Empty));
                        grdSub.DataSource = SqlExecuter.Query("GetOutsourcingFulfillmentRateBrowseSubByWorkMethod", "10001", param);
                    }

                    #endregion Sub Vendorid DataTable
                }
                catch (Exception ex)
                {
                    throw MessageException.Create(Format.GetString(ex));
                }
                finally
                {
                    //DialogManager.CloseWaitArea(this.pnlContent);
                }
            };


        }
        private void View_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (grdMain.View.GetRowCellValue(e.RowHandle, "STANDARDUNIT") == DBNull.Value)
            //if (grdMain.View.GetRowCellValue(e.RowHandle, "OSPVENDORNAME").ToString().Equals(Language.Get("SUBTOTAL")))
            {
                e.Appearance.BackColor = Color.LightYellow;
                e.Appearance.FontStyleDelta = FontStyle.Bold;
                e.HighPriority = true;
            }
        }

        //private void View_RowCellStyle(object sender, RowCellStyleEventArgs e)
        //{
        //    DataRowView row = (sender as GridView).GetRow(e.RowHandle) as DataRowView;

        //    if (Format.GetInteger(row["VENDORNAME"]).Equals(0))
        //    {
        //        e.Appearance.BackColor = ColorTranslator.FromHtml("#D8DADE");
        //        e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
        //        //e.Appearance.ForeColor = Color.Black;
        //    }
        //    else if (row["VENDORNAME"].ToString().ToInt32() > 1)
        //    {
        //        e.Appearance.BackColor = Color.LightSkyBlue;
        //        e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
        //    }
        //}
        #endregion Event

        #region 검색

        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            grdMain.View.ClearDatas();
            grdMain.View.ClearColumns();
            grdSub.View.ClearDatas();
            grdSub.View.ClearColumns();

            // TODO : 조회 SP 변경
            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("P_ENTERPRISEID", UserInfo.Current.Enterprise);

            DateTime standdate = Convert.ToDateTime(values["P_STANDARDDATE"]);
            values.Add("P_YEARMONTH", string.Format("{0:yyyy-MM}", standdate));
            values.Add("P_STARTMONTH", string.Format("{0:yyyy-MM-01}", standdate));
            values.Add("P_STARTDATE", string.Format("{0:yyyy-MM-dd}", standdate));
            values.Add("P_ENDDATE", string.Format("{0:yyyy-MM-dd}", standdate));

            DateTime standlast = new DateTime(standdate.Year, standdate.Month, 1).AddMonths(1);
            values.Add("P_ENDMONTH", string.Format("{0:yyyy-MM-01}", standlast));

            values = Commons.CommonFunction.ConvertParameter(values);

            string query = Format.GetString(values["P_STANDARDMETHOD"], string.Empty).Equals("ByProcess") ?
                            "GetOutsourcingFulfillmentRateBrowse" : "GetOutsourcingFulfillmentRateBrowseByWorkMethod";

            if (await SqlExecuter.QueryAsync(query, "10001", values) is DataTable dt)
            {
                InitializeGrid();

                if (dt.Rows.Count.Equals(0))
                {
                    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                }
                else
                {
                    double dblSendpanelqtymonsum = 0;
                    double dblSendpanelqtydaysum = 0;
                    double dblSendpanelqtymon = 0;
                    double dblSendpanelqtyday = 0;

                    string strOSPVENDORNAME = string.Empty;
                    double dblALLOCATERATE = 0;

                    //start of sum 

                    int row_start = 0;
                    int row_end = 0;

                    for(int row=0; row < dt.Rows.Count; row++)
                    {
                        DataRow dr = dt.Rows[row];

                        if (dr["OSPVENDORNAME"].ToString() == Language.Get("SUBTOTAL"))
                        {
                            row_end = row;

                            for(int row_sub= row_start; row_sub <= row_end; row_sub++)
                            {
                                DataRow dr_sub = dt.Rows[row_sub];

                                dr_sub["SENDPANELQTYMONSUM"] = dblSendpanelqtymonsum;
                                dr_sub["SENDPANELQTYDAYSUM"] = dblSendpanelqtydaysum;
                            }

                            row_start = row+1;
                            row_end = 0;
                            dblSendpanelqtymon = 0;
                            dblSendpanelqtyday = 0;
                            dblSendpanelqtymonsum = 0;
                            dblSendpanelqtydaysum = 0;
                        }
                        else
                        {
                            dblSendpanelqtymon = Format.GetDouble(dr["SENDPANELQTYMON"], 0);
                            dblSendpanelqtyday = Format.GetDouble(dr["SENDPANELQTYDAY"], 0);
                            dblSendpanelqtymonsum += dblSendpanelqtymon;
                            dblSendpanelqtydaysum += dblSendpanelqtyday;
                            dr["SENDPANELQTYMONSUM"] = 0;
                            dr["SENDPANELQTYDAYSUM"] = 0;
                        }
                    }

                    //foreach (DataRow dr in dt.Rows)
                    //{
                    //    if (dr["OSPVENDORNAME"].ToString() == Language.Get("SUBTOTAL"))
                    //    {
                    //        dr["SENDPANELQTYMONSUM"] = dblSendpanelqtymonsum;
                    //        dr["SENDPANELQTYDAYSUM"] = dblSendpanelqtydaysum;
                    //    }
                    //    else
                    //    {
                    //        dblSendpanelqtymon = Format.GetDouble(dr["SENDPANELQTYMON"], 0);
                    //        dblSendpanelqtyday = Format.GetDouble(dr["SENDPANELQTYDAY"], 0);
                    //        dblSendpanelqtymonsum += dblSendpanelqtymon;
                    //        dblSendpanelqtydaysum += dblSendpanelqtydaysum;
                    //        dr["SENDPANELQTYMONSUM"] = 0;
                    //        dr["SENDPANELQTYDAYSUM"] = 0;
                    //    }
                    //}


                    //end of sum
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["OSPVENDORNAME"].ToString() == Language.Get("SUBTOTAL"))
                        {
                            dr["ALLOCATERATE"] = dblALLOCATERATE;
                            strOSPVENDORNAME = string.Empty;
                            dblALLOCATERATE = 0;
                        }
                        else
                        {
                            if (dr["OSPVENDORNAME"].ToString() != strOSPVENDORNAME)
                            {
                                strOSPVENDORNAME = dr["OSPVENDORNAME"].ToString();
                                dblALLOCATERATE += Format.GetDouble(dr["ALLOCATERATE"], 0);
                            }
                            else
                            {
                                dr["OSPVENDORNAME"] = DBNull.Value;
                                dr["ALLOCATERATE"] = DBNull.Value;
                            }
                        }

                        if (dr["SENDPANELQTYMONSUM"] == DBNull.Value) {
                            dblSendpanelqtymonsum = 0;
                            dblSendpanelqtydaysum = 0;
                            dblSendpanelqtymon = 0;
                            dblSendpanelqtyday = 0;
                            continue;
                        }
                        dblSendpanelqtymonsum = Format.GetDouble(dr["SENDPANELQTYMONSUM"], 0);
                        dblSendpanelqtydaysum = Format.GetDouble(dr["SENDPANELQTYDAYSUM"], 0);
                        dblSendpanelqtymon = Format.GetDouble(dr["SENDPANELQTYMON"], 0);
                        dblSendpanelqtyday = Format.GetDouble(dr["SENDPANELQTYDAY"], 0);

                        if (dblSendpanelqtymonsum.Equals(0) || dblSendpanelqtymon.Equals(0))
                        {
                            dr["SENDPANELQTYMONRATE"] = 0;
                        }
                        else
                        {
                            double dblSendpanelqtymonRate = Math.Round(dblSendpanelqtymon / dblSendpanelqtymonsum * 100, 2);
                            dr["SENDPANELQTYMONRATE"] = dblSendpanelqtymonRate;
                        }

                        if (dblSendpanelqtydaysum.Equals(0) || dblSendpanelqtyday.Equals(0))
                        {
                            dr["SENDPANELQTYDAYRATE"] = 0;
                        }
                        else
                        {
                            double dblSendpanelqtydayRate = Math.Round(dblSendpanelqtyday / dblSendpanelqtydaysum * 100, 2);
                            dr["SENDPANELQTYDAYRATE"] = dblSendpanelqtydayRate;
                        }
                    }
                }

                grdMain.DataSource = dt;
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
                {"CODECLASSID", "StandardMethod" },
                {"LANGUAGETYPE", UserInfo.Current.LanguageType },
                {"ENTERPRISEID", UserInfo.Current.Enterprise }
            };

            #region (0.1) 기준일자 설정

            Conditions.AddDateEdit("P_STANDARDDATE")
                      .SetLabel("STANDARDDATE")
                      .SetDisplayFormat("yyyy-MM-dd")
                      .SetPosition(0.1)
                      .SetDefault(DateTime.Now.ToString("yyyy-MM-dd"))
                      .SetValidationIsRequired();

            #endregion (0.1) 기준일자 설정

            #region (0.2) 기준방식 설정

            Conditions.AddComboBox("p_standardmethod", new SqlQuery("GetCodeList", "00001", param))
                      .SetLabel("STANDARDMETHOD")
                      .SetValidationIsRequired()
                      .SetDefault("ByProcess")
                      .SetPosition(0.2);

            #endregion (0.2) 기준방식 설정

            #region (0.3) 대공정 설정

            var condition = Conditions.AddSelectPopup("p_TOPPROCESSSEGMENTCLASSID", new SqlQuery("GetParentProcesssegmentclassidListByOsp", "10001", param), "TOPPROCESSSEGMENTCLASSNAME", "TOPPROCESSSEGMENTCLASSID")
                                      .SetPopupLayout("TOPPROCESSSEGMENTCLASS", PopupButtonStyles.Ok_Cancel, true, false)
                                      .SetPopupLayoutForm(400, 600)
                                      .SetLabel("TOPPROCESSSEGMENTCLASS")
                                      .SetPopupResultCount(1)
                                      .SetPosition(0.3);

            condition.Conditions.AddTextBox("PROCESSSEGMENTCLASSNAME").SetLabel("TOPPROCESSSEGMENTCLASSNAME");

            condition.GridColumns.AddTextBoxColumn("TOPPROCESSSEGMENTCLASSID", 150);
            condition.GridColumns.AddTextBoxColumn("TOPPROCESSSEGMENTCLASSNAME", 200);

            #endregion (0.3) 대공정 설정

            #region (0.4) 중공정 설정

            condition = Conditions.AddSelectPopup("p_processsegmentclassid", new SqlQuery("GetProcesssegmentclassidListByOsp", "10001", param), "MIDDLEPROCESSSEGMENTCLASSNAME", "MIDDLEPROCESSSEGMENTCLASSID")
                                  .SetPopupLayout("MIDDLEPROCESSSEGMENTCLASS", PopupButtonStyles.Ok_Cancel, true, false)
                                  .SetPopupLayoutForm(400, 600)
                                  .SetLabel("MIDDLEPROCESSSEGMENTCLASS")
                                  .SetRelationIds("P_TOPPROCESSSEGMENTCLASSID")
                                  .SetPopupResultCount(1)
                                  .SetPosition(0.4);

            condition.Conditions.AddTextBox("MIDDLEPROCESSSEGMENTCLASSNAME").SetLabel("MIDDLEPROCESSSEGMENTCLASSNAME");

            condition.GridColumns.AddTextBoxColumn("MIDDLEPROCESSSEGMENTCLASSID", 150);
            condition.GridColumns.AddTextBoxColumn("MIDDLEPROCESSSEGMENTCLASSNAME", 200);

            #endregion (0.4) 중공정 설정

            #region (0.5) 공정 선택팝업

            condition = Conditions.AddSelectPopup("PROCESSSEGMENTID", new SqlQuery("GetProcessSegmentListByOsp", "10001", param), "PROCESSSEGMENTNAME", "PROCESSSEGMENTID")
                                  .SetPopupLayout("PROCESSSEGMENTID", PopupButtonStyles.Ok_Cancel, true, false)
                                  .SetPopupLayoutForm(500, 600)
                                  .SetLabel("PROCESSSEGMENTNAME")
                                  .SetRelationIds("P_TOPPROCESSSEGMENTCLASSID", "P_PROCESSSEGMENTCLASSID")
                                  .SetPopupResultCount(1)
                                  .SetPosition(0.5)
                                  .SetIsHidden();

            condition.ValueFieldName = "PROCESSSEGMENTID";
            condition.DisplayFieldName = "PROCESSSEGMENTNAME";

            condition.Conditions.AddTextBox("PROCESSSEGMENTNAME").SetLabel("PROCESSSEGMENT");

            condition.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 100).SetValidationKeyColumn();
            condition.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 250);

            #endregion (0.5) 공정 선택팝업

            #region (0.6) 작업방식 설정

            param.Add("P_CODECLASSID", "OutsourcingSpecWorkType");
            param.Add("P_CODENOTIN", "OutsourcingSpecWorkType008','OutsourcingSpecWorkType009");

            Conditions.AddComboBox("P_SPECSUBTYPENAME", new SqlQuery("GetOutsourcingDistributionRatioSegmentComboWorkMethod", "10001", param))
                      .SetLabel("SPECSUBTYPENAME")
                      .SetIsReadOnly()
                      .SetEmptyItem(Language.Get("ALLVIEWS"), "*", true)
                      .SetDefault("*")
                      .SetPosition(0.6);

            #endregion (0.6) 작업방식 설정

            #region (0.7) 집계코드 설정

            //param["CODECLASSID"] = "OSPSumCode";
            param["CODECLASSID"] = "StandardUnit";

            Conditions.AddComboBox("p_ospsumcode", new SqlQuery("GetCodeList", "00001", param))
                      .SetLabel("OSPSUMCODE")
                      .SetPosition(0.7)
                      .SetEmptyItem(Language.Get("ALLVIEWS"), "*", true)
                      .SetDefault("*");

            #endregion (0.7) 집계코드 설정

            // 양산/샘플 (Production/Sample)
            Conditions.AddComboBox("P_PRODUCTIONTYPE", new SqlQuery("GetOutsourcedDistributionSearchConditionProductionType", "10001", param))
                      .SetLabel("PRODUCTIONTYPE")
                      .SetEmptyItem(Language.Get("ALLVIEWS"), "*", true)
                      .SetDefault("Production")
                      .SetPosition(0.8);

            // 제품/반제품 (Product/SubAssembly)
            Conditions.AddComboBox("P_MASTERDATACLASSID", new SqlQuery("GetOutsourcedDistributionSearchConditionMasterdataclassid", "10001", param))
                      .SetLabel("MASTERDATACLASSID")
                      .SetEmptyItem(Language.Get("ALLVIEWS"), "*", true)
                      .SetDefault("Product")
                      .SetPosition(0.9);
        }

        #endregion 검색
    }
}