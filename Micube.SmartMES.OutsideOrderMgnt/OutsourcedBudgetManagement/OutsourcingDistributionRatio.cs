#region using

using DevExpress.XtraEditors.Repository;
using DevExpress.XtraLayout.Utils;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion using

namespace Micube.SmartMES.OutsideOrderMgnt
{
    /// <summary>
    /// 프 로 그 램 명  :  외주관리> 외주창고 > 협력사별 배분율
    /// 업  무  설  명  :  협력사별 배분율 등록한다.
    /// 생    성    자  : CHOISSTAR
    /// 생    성    일  : 2019-08-13
    /// 수  정  이  력  :
    ///
    ///
    ///
    /// </summary>
    public partial class OutsourcingDistributionRatio : SmartConditionManualBaseForm
    {
        #region Local Variables

        /// <summary>
        /// 재조회시 변경사항 체크 여부
        /// </summary>
        private bool _IsSaveReSearch = false;

        /// <summary>
        /// Tool bar Button Name
        /// </summary>
        private string _StrToolBtn = string.Empty;

        #endregion Local Variables

        #region 생성자

        public OutsourcingDistributionRatio()
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

            DateTime dateNow = DateTime.Now;
            dtpFromYm.EditValue = dateNow.ToString("yyyy-MM");
            dtpToYm.EditValue = dateNow.AddMonths(1).ToString("yyyy-MM");
        }

        /// <summary>
        ///  그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            //배분률 적용 공정목록
            InitializeGrid_grdLeft();

            //협력사별 배분률
            InitializeGrid_grdRight();
        }

        #region 배분률 적용 공정목록
        /// <summary>
        /// 배분률 적용 공정목록
        /// </summary>
        private void InitializeGrid_grdLeft()
        {
            grdLeft.GridButtonItem = GridButtonItem.Export;
            grdLeft.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            grdLeft.View.AddTextBoxColumn("STANDARDYM", 70).SetIsReadOnly().SetDisplayFormat("yyyy-MM");

            // 검색조건에 따른 Column 구성 변경
            if (Format.GetString(Conditions.GetValues()["P_STANDARDMETHOD"], string.Empty).Equals("ByWorkMethod"))
            {
                grdLeft.View.AddTextBoxColumn("TOPPROCESSSEGMENTCLASSID").SetIsHidden();
                grdLeft.View.AddTextBoxColumn("TOPPROCESSSEGMENTCLASSNAME", 80).SetIsHidden();
                grdLeft.View.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 60).SetIsHidden();
                grdLeft.View.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 150).SetIsHidden();
                grdLeft.View.AddTextBoxColumn("SPECSUBTYPEID", 150).SetIsHidden();
                grdLeft.View.AddTextBoxColumn("SPECSUBTYPENAME", 150).SetIsReadOnly();
            }
            else
            {
                grdLeft.View.AddTextBoxColumn("TOPPROCESSSEGMENTCLASSID").SetIsHidden();
                grdLeft.View.AddTextBoxColumn("TOPPROCESSSEGMENTCLASSNAME", 80).SetIsReadOnly();
                grdLeft.View.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 60).SetIsReadOnly();
                grdLeft.View.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 150).SetIsReadOnly();
                grdLeft.View.AddTextBoxColumn("SPECSUBTYPEID", 150).SetIsHidden();
                grdLeft.View.AddTextBoxColumn("SPECSUBTYPENAME", 150).SetIsHidden();
            }

            grdLeft.View.AddTextBoxColumn("ENTERPRISEID").SetIsHidden();
            grdLeft.View.AddTextBoxColumn("PLANTID").SetIsHidden();
            grdLeft.View.AddTextBoxColumn("ISALLOCATE", 70).SetDefault(false);

            grdLeft.View.AddComboBoxColumn("STANDARDUNIT", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=StandardUnit", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                        .SetDefault(false)
                        .SetEmptyItem("", "*", true);

            grdLeft.View.AddTextBoxColumn("APPLYCNTAREID", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,###", MaskTypes.Numeric);
            grdLeft.View.AddTextBoxColumn("ALLOCATERATESUM", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,###", MaskTypes.Numeric);
            grdLeft.View.AddTextBoxColumn("WORKTYPE", 120).SetIsHidden();

            grdLeft.View.PopulateColumns();

            RepositoryItemCheckEdit repositoryCheckEdit1 = grdLeft.View.GridControl.RepositoryItems.Add("CheckEdit") as RepositoryItemCheckEdit;
            repositoryCheckEdit1.ValueChecked = "Y";
            repositoryCheckEdit1.ValueUnchecked = "N";
            repositoryCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            grdLeft.View.Columns["ISALLOCATE"].ColumnEdit = repositoryCheckEdit1;

        }
        #endregion 배분률 적용 공정목록

        #region 협력사별 배분률
        /// <summary>
        /// 협력사별 배분률
        /// </summary>
        private void InitializeGrid_grdRight()
        {
            //grdRight.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export;
            grdRight.GridButtonItem = GridButtonItem.Export;
            //grdRight.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdRight.View.AddTextBoxColumn("STANDARDYM", 120).SetIsHidden();
            grdRight.View.AddTextBoxColumn("PLANTID", 120).SetIsHidden();
            grdRight.View.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 120).SetIsHidden();
            grdRight.View.AddTextBoxColumn("ENTERPRISEID", 120).SetIsHidden();
            grdRight.View.AddTextBoxColumn("SPECSUBTYPE", 120).SetIsHidden();
            grdRight.View.AddTextBoxColumn("STANDARDMETHOD", 120).SetIsHidden();
            grdRight.View.AddTextBoxColumn("STANDARDUNIT", 120).SetIsHidden();
            grdRight.View.AddTextBoxColumn("AREAID", 120).SetIsHidden();

            //셀렉트팝업시 아래 주석 처리 2021-04-26 오근영
            grdRight.View.AddComboBoxColumn("VENDORID", 150, new SqlQueryAdapter(), "VENDORNAME", "VENDORID")
                         .SetLabel("OSPVENDORNAME")
                         .SetValidationKeyColumn()
                         .SetValidationIsRequired();

            //셀렉트팝업시 아래 주석 제거 2021-04-26 오근영
            //if (Conditions.GetValues()["P_STANDARDMETHOD"].ToString().Equals("ByProcess"))
            //{
            //    Dictionary<string, object> dicParam;
            //    dicParam = new Dictionary<string, object> {
            //        { "P_PLANTID"               , UserInfo.Current.Plant },
            //        { "P_PROCESSSEGMENTCLASSID" , Format.GetString(grdLeft.View.GetFocusedRowCellValue("PROCESSSEGMENTCLASSID"), string.Empty) },
            //        { "LANGUAGETYPE"            , UserInfo.Current.LanguageType }
            //    };
            //    var condition = grdRight.View.AddSelectPopupColumn("VENDORID", 100, new SqlQuery("GetVendoridAllocaterateByOspProcesssegmentid", "10001", dicParam))
            //        .SetPopupLayout("VENDORID", PopupButtonStyles.Ok_Cancel, true, false)
            //        .SetPopupResultCount(0)
            //        .SetPopupLayoutForm(900, 600, FormBorderStyle.FixedToolWindow)
            //        .SetValidationKeyColumn()
            //        .SetValidationIsRequired()
            //        .SetTextAlignment(TextAlignment.Left)
            //        .SetPopupAutoFillColumns("VENDORNAME")
            //    ;
            //    condition.Conditions.AddTextBox("VENDORID");                    //팝업검색조건 : 1.VENDORID
            //    condition.Conditions.AddTextBox("VENDORNAME");                  //팝업검색조건 : 2.VENDORNAME
            //    condition.GridColumns.AddTextBoxColumn("VENDORID", 80);         //팝업검색결과그리드 : 1.VENDORID
            //    condition.GridColumns.AddTextBoxColumn("VENDORNAME", 150);      //팝업검색결과그리드 : 2.VENDORNAME
            //}
            //else
            //{
            //    Dictionary<string, object> dicParam2;
            //    dicParam2 = new Dictionary<string, object> {
            //        { "P_PLANTID"               , UserInfo.Current.Plant },
            //        { "P_SPECSUBTYPEID"         , Format.GetString(grdLeft.View.GetFocusedRowCellValue("SPECSUBTYPEID"), string.Empty) }
            //    };
            //    var condition2 = grdRight.View.AddSelectPopupColumn("VENDORID", 100, new SqlQuery("GetVendoridAllocaterateByOspProcesssegmentidByWorkMethod", "10001", dicParam2))
            //        .SetPopupLayout("VENDORID", PopupButtonStyles.Ok_Cancel, true, false)
            //        .SetPopupResultCount(0)
            //        .SetPopupLayoutForm(900, 600, FormBorderStyle.FixedToolWindow)
            //        .SetValidationKeyColumn()
            //        .SetValidationIsRequired()
            //        .SetTextAlignment(TextAlignment.Left)
            //        .SetPopupAutoFillColumns("VENDORNAME")
            //    ;
            //    condition2.Conditions.AddTextBox("VENDORID");                    //팝업검색조건 : 1.VENDORID
            //    condition2.Conditions.AddTextBox("VENDORNAME");                  //팝업검색조건 : 2.VENDORNAME
            //    condition2.GridColumns.AddTextBoxColumn("VENDORID", 80);         //팝업검색결과그리드 : 1.VENDORID
            //    condition2.GridColumns.AddTextBoxColumn("VENDORNAME", 150);      //팝업검색결과그리드 : 2.VENDORNAME
            //}

            grdRight.View.AddSpinEditColumn("ALLOCATERATE", 120).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,###.##", MaskTypes.Numeric);
            grdRight.View.AddTextBoxColumn("DESCRIPTION", 200);

            grdRight.View.PopulateColumns();

            grdRight.View.SetKeyColumn("PLANTID", "ENTERPRISEID", "PROCESSSEGMENTCLASSID", "STANDARDYM", "VENDORID", "SPECSUBTYPE");

        }
        #endregion 협력사별 배분률

        /// <summary>
        /// 협력업체ID 컬럼 팝업
        /// </summary>
        private void InitializeGrid_VendorListPopup(string standardmethod, string processsegmentclassid, string specsubtype)
        {
            Dictionary<string, object> dicParam;
            dicParam = new Dictionary<string, object> {
                { "P_PLANTID"               , grdLeft.View.GetFocusedRowCellValue("PLANTID") },
                { "P_PROCESSSEGMENTCLASSID" , grdLeft.View.GetFocusedRowCellValue("PROCESSSEGMENTCLASSID") },
                { "LANGUAGETYPE"            , UserInfo.Current.LanguageType },
                { "P_SPECSUBTYPEID"         , UserInfo.Current.Enterprise }
            };
            var oVendorPopupColumn = Conditions.AddSelectPopup("VENDORID", new SqlQuery("GetAreaList", "10006", dicParam), "VENDORNAME", "VENDORID")
                .SetPopupLayout("VENDORID", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupResultCount(1)
                .SetPopupLayoutForm(800, 800, FormBorderStyle.FixedToolWindow)
                .SetPosition(13)
                .SetLabel("VENDORNAME")
            ;
            oVendorPopupColumn.Conditions.AddTextBox("VENDORID");
            oVendorPopupColumn.Conditions.AddTextBox("VENDORNAME");

            oVendorPopupColumn.GridColumns.AddTextBoxColumn("VENDORID", 120);
            oVendorPopupColumn.GridColumns.AddTextBoxColumn("VENDORNAME", 200);

        }
        #endregion 컨텐츠 영역 초기화

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // 협력사별 배분률목록 행추가 이벤트
            grdRight.View.AddingNewRow += (s, e) =>
            {
                if (grdLeft.View.RowCount.Equals(0))
                {
                    e.IsCancel = true;
                }

                grdRight.View.SetFocusedRowCellValue("ENTERPRISEID", UserInfo.Current.Enterprise);
                grdRight.View.SetFocusedRowCellValue("STANDARDYM", Format.GetString(grdLeft.View.GetFocusedRowCellValue("STANDARDYM"), string.Empty));
                grdRight.View.SetFocusedRowCellValue("PLANTID", Format.GetString(grdLeft.View.GetFocusedRowCellValue("PLANTID"), string.Empty));
                grdRight.View.SetFocusedRowCellValue("PROCESSSEGMENTCLASSID", Format.GetString(grdLeft.View.GetFocusedRowCellValue("PROCESSSEGMENTCLASSID"), string.Empty));
                grdRight.View.SetFocusedRowCellValue("AREAID", "*");
                grdRight.View.SetFocusedRowCellValue("VENDORID", "");
                grdRight.View.SetFocusedRowCellValue("SPECSUBTYPE", Format.GetString(grdLeft.View.GetFocusedRowCellValue("SPECSUBTYPE"), string.Empty));
                grdRight.View.SetFocusedRowCellValue("STANDARDMETHOD", Format.GetString(grdLeft.View.GetFocusedRowCellValue("STANDARDMETHOD"), string.Empty));
                grdRight.View.SetFocusedRowCellValue("STANDARDUNIT", Format.GetString(grdLeft.View.GetFocusedRowCellValue("STANDARDUNIT"), string.Empty));
            };

            // 공정 Grid Row 이동전 변경사항 있는지 체크 후 있으면 미이동
            grdLeft.View.BeforeLeaveRow += (s, e) =>
            {
                if (e.RowHandle < 0)
                {
                    return;
                }

                if (grdRight.GetChangedRows().Rows.Count > 0 && !_IsSaveReSearch)
                {
                    // 변경 내용이 있습니다. 이동하시겠습니까?
                    if (DialogResult.No.Equals(MSGBox.Show(MessageBoxType.Information, "ThereIsChangeMove", MessageBoxButtons.YesNo)))
                    {
                        e.Allow = false;
                        return;
                    }
                }
            };

            // 배분율 적용 공정목록 포커스 이동시
            grdLeft.View.FocusedRowChanged += (s, e) =>
            {
                if (grdLeft.View.FocusedRowHandle < 0)
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

                    Dictionary<string, object> param = new Dictionary<string, object>
                    {
                        { "P_ENTERPRISEID", UserInfo.Current.Enterprise },
                        { "P_PLANTID", grdLeft.View.GetFocusedRowCellValue("PLANTID") },
                        { "P_YEARMONTH", grdLeft.View.GetFocusedRowCellValue("STANDARDYM") },
                        { "P_PROCESSSEGMENTCLASSID", grdLeft.View.GetFocusedRowCellValue("PROCESSSEGMENTCLASSID") },
                        { "LANGUAGETYPE", UserInfo.Current.LanguageType },
                        { "P_STANDARDUNIT", grdLeft.View.GetFocusedRowCellValue("STANDARDUNIT") }
                    };

                    #region Right Vendorid combo DataTable

                    if (Conditions.GetValues()["P_STANDARDMETHOD"].ToString().Equals("ByProcess"))
                    {
                        param["P_PROCESSSEGMENTCLASSID"] = Format.GetString(grdLeft.View.GetFocusedRowCellValue("PROCESSSEGMENTCLASSID"), string.Empty);
                        //grdRight.DataSource = SqlExecuter.Query("GetOutsourcingDistributionRatioAreaid", "10001", param);
                        grdRight.DataSource = SqlExecuter.Query("GetOutsourcingDistributionRatioVendorid", "10001", param);

                        //셀렉트팝업시 아래 주석 처리 2021-04-26 오근영
                        grdRight.View.RefreshComboBoxDataSource("VENDORID", new SqlQuery("GetVendoridAllocaterateByOspProcesssegmentid", "10001", param));
                    }
                    else
                    {
                        param.Add("P_SPECSUBTYPEID", Format.GetString(grdLeft.View.GetFocusedRowCellValue("SPECSUBTYPEID"), string.Empty));
                        //grdRight.DataSource = SqlExecuter.Query("GetOutsourcingDistributionRatioAreaidByWorkMethod", "10001", param);
                        grdRight.DataSource = SqlExecuter.Query("GetOutsourcingDistributionRatioVendoridByWorkMethod", "10001", param);

                        //셀렉트팝업시 아래 주석 처리 2021-04-26 오근영
                        grdRight.View.RefreshComboBoxDataSource("VENDORID", new SqlQuery("GetVendoridAllocaterateByOspProcesssegmentidByWorkMethod", "10001", param));
                    }

                    #endregion Right Vendorid combo DataTable
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

            // 확인 버튼 이벤트 : 복사
            btnOk.Click += (s, e) => SaveContent();

            // 검색조건 기준방식에 따른 ReadOnly 처리 이벤트
            ((SmartComboBox)Conditions.GetControl("P_STANDARDMETHOD")).EditValueChanged += (s, e) =>
            {
                SmartComboBox edit = s as SmartComboBox;

                if (Format.GetFullTrimString(edit.EditValue).Equals("ByProcess"))
                {
                    ((SmartSelectPopupEdit)Conditions.GetControl("P_TOPPROCESSSEGMENTCLASSID")).ReadOnly = false;
                    ((SmartSelectPopupEdit)Conditions.GetControl("P_PROCESSSEGMENTCLASSID")).ReadOnly = false;
                    ((SmartComboBox)Conditions.GetControl("P_SPECSUBTYPENAME")).ReadOnly = true;
                }
                else
                {
                    ((SmartSelectPopupEdit)Conditions.GetControl("P_TOPPROCESSSEGMENTCLASSID")).ReadOnly = true;
                    ((SmartSelectPopupEdit)Conditions.GetControl("P_PROCESSSEGMENTCLASSID")).ReadOnly = true;
                    ((SmartComboBox)Conditions.GetControl("P_SPECSUBTYPENAME")).ReadOnly = false;
                }

                ((SmartSelectPopupEdit)Conditions.GetControl("P_TOPPROCESSSEGMENTCLASSID")).EditValue = string.Empty;
                ((SmartSelectPopupEdit)Conditions.GetControl("P_PROCESSSEGMENTCLASSID")).EditValue = string.Empty;
                ((SmartComboBox)Conditions.GetControl("P_SPECSUBTYPENAME")).EditValue = "*";
            };
        }

        #endregion Event

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            Dictionary<string, object> param = new Dictionary<string, object>
                        {
                            { "P_ENTERPRISEID", UserInfo.Current.Enterprise },
                            { "P_PLANTID", grdLeft.View.GetFocusedRowCellValue("PLANTID") },
                            { "P_YEARMONTH", grdLeft.View.GetFocusedRowCellValue("STANDARDYM") }
                        };
            DataTable dtConfirmYearMonth = SqlExecuter.Query("GetOutsourcingDistributionRatioSegmentlistConfirm", "10001", param);

            if (dtConfirmYearMonth.Rows.Count > 0)
            {
                throw MessageException.Create("InValidOspData026", grdLeft.View.GetFocusedRowCellValue("STANDARDYM").ToString()); //메세지
            }
            else
            {

                base.OnToolbarSaveClick();

                _StrToolBtn = "Save";

                SaveContent();
            }
        }

        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            base.OnToolbarClick(sender, e);

            _StrToolBtn = (sender as SmartButton).Name;

            if (_StrToolBtn.Equals("Save"))
            {
                return;
            }

            layoutFrom.Text = _StrToolBtn.Equals("Copy") ? Language.Get("FROMYEARMONTH") :
                              _StrToolBtn.Equals("Confirmation") ? Language.Get("CONFIRMATION") : Language.Get("DEFINITECANCELLATION");

            if (layoutEmpty.Visibility.Equals(LayoutVisibility.Always))
            {
                layoutEmpty.Visibility = LayoutVisibility.Never;
                layoutFrom.Visibility = LayoutVisibility.Never;
                layoutTo.Visibility = LayoutVisibility.Never;
                layoutOk.Visibility = LayoutVisibility.Never;
            }
            else
            {
                switch (_StrToolBtn)
                {
                    case "Copy":
                        layoutEmpty.Visibility = LayoutVisibility.Always;
                        layoutFrom.Visibility = LayoutVisibility.Always;
                        layoutTo.Visibility = LayoutVisibility.Always;
                        layoutOk.Visibility = LayoutVisibility.Always;
                        break;

                    case "Confirmation":
                    case "CancelConfirmation":
                        layoutEmpty.Visibility = LayoutVisibility.Always;
                        layoutFrom.Visibility = LayoutVisibility.Always;
                        layoutTo.Visibility = LayoutVisibility.Never;
                        layoutOk.Visibility = LayoutVisibility.Always;
                        break;
                }
            }
        }

        #endregion 툴바

        #region 검색

        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            grdLeft.View.ClearDatas();
            grdLeft.View.ClearColumns();
            grdRight.View.ClearDatas();
            grdRight.View.ClearColumns();

            var values = Conditions.GetValues();

            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("P_ENTERPRISEID", UserInfo.Current.Enterprise);
            if (!values["P_YEARMONTH"].ToString().Equals(""))
            {
                DateTime requestDateFr = Convert.ToDateTime(values["P_YEARMONTH"]);
                values.Remove("P_YEARMONTH");
                values.Add("P_YEARMONTH", string.Format("{0:yyyy-MM}", requestDateFr));
            }

            string query = Format.GetString(values["P_STANDARDMETHOD"], string.Empty).Equals("ByProcess") ?
                            "GetOutsourcingDistributionRatioSegment" : "GetOutsourcingDistributionRatioSegmentByWorkMethod";

            if (await SqlExecuter.QueryAsync(query, "10001", values) is DataTable dtSegment)
            {
                InitializeGrid();

                grdLeft.DataSource = dtSegment;

                if (dtSegment.Rows.Count.Equals(0)) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
                {
                    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                }
                else
                {
                    grdLeft.View.FocusedRowHandle = 0;
                    grdLeft.View.SelectRow(0);
                }
            }
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            DateTime dateNow = DateTime.Now;
            string strym = dateNow.ToString("yyyy-MM");

            Dictionary<string, object> param = new Dictionary<string, object>
            {
                { "CODECLASSID", "StandardMethod" },
                { "LANGUAGETYPE", UserInfo.Current.LanguageType }
            };

            Conditions.AddDateEdit("p_yearmonth").SetLabel("STANDARDYM").SetDisplayFormat("yyyy-MM")
                      .SetValidationIsRequired()
                      .SetPosition(0.1)
                      .SetDefault(strym);

            // 기준방식
            Conditions.AddComboBox("P_STANDARDMETHOD", new SqlQuery("GetCodeList", "00001", param))
                      .SetLabel("STANDARDMETHOD")
                      .SetValidationIsRequired()
                      .SetDefault("ByProcess")
                      .SetPosition(0.2);

            #region 대공정

            var condition = Conditions.AddSelectPopup("p_TOPPROCESSSEGMENTCLASSID", new SqlQuery("GetParentProcesssegmentclassidListByOsp", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "TOPPROCESSSEGMENTCLASSNAME", "TOPPROCESSSEGMENTCLASSID")
                                      .SetPopupLayout("TOPPROCESSSEGMENTCLASS", PopupButtonStyles.Ok_Cancel, true, false)
                                      .SetPopupLayoutForm(400, 600)
                                      .SetLabel("TOPPROCESSSEGMENTCLASS")
                                      .SetPopupResultCount(1)
                                      .SetPosition(0.3);

            condition.Conditions.AddTextBox("PROCESSSEGMENTCLASSNAME").SetLabel("TOPPROCESSSEGMENTCLASSNAME");

            condition.GridColumns.AddTextBoxColumn("TOPPROCESSSEGMENTCLASSID", 150);
            condition.GridColumns.AddTextBoxColumn("TOPPROCESSSEGMENTCLASSNAME", 200);

            #endregion 대공정

            #region 중공정

            condition = Conditions.AddSelectPopup("p_processsegmentclassid", new SqlQuery("GetProcesssegmentclassidListByOsp", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "MIDDLEPROCESSSEGMENTCLASSNAME", "MIDDLEPROCESSSEGMENTCLASSID")
                                  .SetPopupLayout("MIDDLEPROCESSSEGMENTCLASS", PopupButtonStyles.Ok_Cancel, true, false)
                                  .SetPopupLayoutForm(400, 600)
                                  .SetLabel("MIDDLEPROCESSSEGMENTCLASS")
                                  .SetRelationIds("P_TOPPROCESSSEGMENTCLASSID")
                                  .SetPopupResultCount(1)
                                  .SetPosition(0.4);

            condition.Conditions.AddTextBox("MIDDLEPROCESSSEGMENTCLASSNAME").SetLabel("MIDDLEPROCESSSEGMENTCLASSNAME");

            condition.GridColumns.AddTextBoxColumn("MIDDLEPROCESSSEGMENTCLASSID", 150);
            condition.GridColumns.AddTextBoxColumn("MIDDLEPROCESSSEGMENTCLASSNAME", 200);

            #endregion 중공정

            param.Add("P_CODECLASSID", "OutsourcingSpecWorkType");
            param.Add("P_CODENOTIN", "OutsourcingSpecWorkType008','OutsourcingSpecWorkType009");

            // 작업방식
            Conditions.AddComboBox("P_SPECSUBTYPENAME", new SqlQuery("GetOutsourcingDistributionRatioSegmentComboWorkMethod", "10001", param))
                      .SetLabel("SPECSUBTYPENAME")
                      .SetIsReadOnly()
                      .SetEmptyItem(Language.Get("ALLVIEWS"), "*", true)
                      .SetPosition(0.5);

            param["CODECLASSID"] = "StandardUnit";

            // 기준단위
            Conditions.AddComboBox("P_STANDARDUNIT", new SqlQuery("GetCodeList", "00001", param))
                      .SetLabel("STANDARDUNIT")
                      .SetEmptyItem(Language.Get("ALLVIEWS"), "*", true)
                      .SetPosition(0.6);

            param["CODECLASSID"] = "YesNo";

            // 적용공정여부
            Conditions.AddComboBox("p_yesno", new SqlQuery("GetCodeList", "00001", param))
                      .SetLabel("ISALLOCATE")
                      .SetEmptyItem(Language.Get("ALLVIEWS"), "*", true)
                      .SetPosition(0.7);
        }

        #endregion 검색

        #region 유효성 검사

        /// <summary>
        /// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            ValidateContent();
        }

        #endregion 유효성 검사

        #region Private Function

        /// <summary>
        ///  data  AreaidDatatable 생성
        /// </summary>
        /// <returns></returns>
        private DataTable CreateCopyDatatable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("FROMYEARMONTH");
            dt.Columns.Add("TOYEARMONTH");
            dt.Columns.Add("YEARMONTH");
            dt.Columns.Add("ENTERPRISEID");
            dt.Columns.Add("PLANTID");

            return dt;
        }

        /// <summary>
        /// 단가 기준  key 중복 체크
        /// </summary>
        /// <returns></returns>
        private bool CheckPriceDateKeyColumns()
        {
            DataRow row;

            for (int irow = 0; irow < grdRight.View.DataRowCount; irow++)
            {
                row = grdRight.View.GetDataRow(irow);

                if (row.RowState.Equals(DataRowState.Added) || row.RowState.Equals(DataRowState.Modified))
                {
                    if (SearchPriceDateKey(Format.GetString(row["VENDORID"], string.Empty), irow) > 0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// 기간 중복 체크 함수
        /// </summary>
        /// <param name="dateStartdate"></param>
        /// <param name="dateEnddate"></param>
        /// <param name="icurRow"></param>
        /// <returns></returns>
        private int SearchPriceDateKey(string vendorId, int icurRow)
        {
            int iresultRow = -1;

            for (int irow = 0; irow < grdRight.View.DataRowCount; irow++)
            {
                if (icurRow != irow)
                {
                    DataRow row = grdRight.View.GetDataRow(irow);

                    if (!grdRight.View.IsDeletedRow(row))
                    {
                        //협력업체 비교
                        if (vendorId.Equals(Format.GetString(row["VENDORID"], string.Empty)))
                        {
                            return irow;
                        }
                    }
                }
            }

            return iresultRow;
        }

        /// <summary>
        /// 유효성 체크
        /// 복사 기능에서도 사용하여 Function으로 처리
        /// </summary>
        private void ValidateContent()
        {
            if (grdLeft.GetChangedRows().Rows.Count.Equals(0) && grdRight.GetChangedRows().Rows.Count.Equals(0))
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }

            if (grdRight.GetChangedRows().Rows.Count > 0)
            {
                if (!CheckPriceDateKeyColumns())
                {
                    //다국어 처리  중복된 VENDORID가 있습니다.
                    throw MessageException.Create("InValidOspData007", grdRight.View.Columns["VENDORID"].Caption);
                }

                foreach (DataRow row in (grdRight.DataSource as DataTable).Rows)
                {
                    if (Format.GetString(row["VENDORID"], string.Empty).Equals(string.Empty))
                    {
                        throw MessageException.Create("InValidOspRequiredField", grdRight.View.Columns["VENDORID"].Caption);
                    }

                    if (Format.GetString(row["ALLOCATERATE"], string.Empty).Equals(string.Empty))
                    {
                        throw MessageException.Create("InValidOspRequiredField", grdRight.View.Columns["ALLOCATERATE"].Caption);
                    }
                }
            }
        }

        /// <summary>
        ///
        ///  저장 함수
        ///  복사 기능에 저장기능이 있어 Function으로 처리
        /// </summary>
        private void SaveContent()
        {
            try
            {
                //this.ShowWaitArea();
                
                DataTable dt = CreateCopyDatatable();
                dt.TableName = "listCopy";

                DataTable dtConfirm = CreateCopyDatatable();
                dtConfirm.TableName = "listConfirm";

                DataTable dtConfirmCancel = CreateCopyDatatable();
                dtConfirmCancel.TableName = "listConfirmCancel";

                DataTable dtSegment = new DataTable
                {
                    TableName = "listHead"
                };

                DataTable dtAllocateRate = new DataTable
                {
                    TableName = "listDetail"
                };

                var values = Conditions.GetValues();
                DataRow dr = null;

                switch (_StrToolBtn)
                {
                    case "Copy":
                        if (dtpFromYm.Text.Equals(string.Empty))
                        {
                            throw MessageException.Create("InValidOspRequiredField", Language.Get("FROMYEARMONTH")); //메세지
                        }

                        if (dtpToYm.Text.Equals(string.Empty))
                        {
                            throw MessageException.Create("InValidOspRequiredField", Language.Get("TOYEARMONTH")); //메세지
                        }

                        string strFromYm = Convert.ToDateTime(dtpFromYm.EditValue).ToString("yyyy-MM");
                        string strToYm = Convert.ToDateTime(dtpToYm.EditValue).ToString("yyyy-MM");

                        if (strFromYm.Equals(strToYm))
                        {
                            throw MessageException.Create("InValidOspData006");
                        }

                        dr = dt.NewRow();

                        dr["PLANTID"] = values["P_PLANTID"];
                        dr["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                        dr["FROMYEARMONTH"] = strFromYm;
                        dr["TOYEARMONTH"] = strToYm;
                        dt.Rows.Add(dr);
                        break;

                    case "Confirmation":
                        if (dtpFromYm.Text.Equals(string.Empty))
                        {
                            throw MessageException.Create("InValidOspRequiredField", Language.Get("CONFIRMATION")); //메세지
                        }

                        dr = dtConfirm.NewRow();

                        dr["PLANTID"] = values["P_PLANTID"];
                        dr["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                        dr["YEARMONTH"] = Convert.ToDateTime(dtpFromYm.EditValue).ToString("yyyy-MM");
                        dtConfirm.Rows.Add(dr);
                        break;

                    case "CancelConfirmation":
                        if (dtpFromYm.Text.Equals(string.Empty))
                        {
                            throw MessageException.Create("InValidOspRequiredField", Language.Get("DEFINITECANCELLATION")); //메세지
                        }

                        dr = dtConfirmCancel.NewRow();

                        dr["PLANTID"] = values["P_PLANTID"];
                        dr["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                        dr["YEARMONTH"] = Convert.ToDateTime(dtpFromYm.EditValue).ToString("yyyy-MM");
                        dtConfirmCancel.Rows.Add(dr);
                        break;
                    case "Save":
                        if (grdLeft.View.RowCount > 0)
                        {
                            dtSegment = grdLeft.GetChangedRows();
                        }

                        if (grdRight.View.RowCount > 0)
                        {
                            dtAllocateRate = grdRight.GetChangedRows();
                        }
                        break;
                }

                DataSet ds = new DataSet();
                ds.Tables.Add(dt);
                ds.Tables.Add(dtConfirm);
                ds.Tables.Add(dtConfirmCancel);
                dtSegment.TableName = "listHead";
                ds.Tables.Add(dtSegment);
                dtAllocateRate.TableName = "listDetail";
                ds.Tables.Add(dtAllocateRate);

                ExecuteRule("OutsourcingDistributionRatio", ds);
                //ShowMessage("SuccessOspProcess");
                switch (_StrToolBtn)
                {
                    case "Copy":
                        ShowMessage("SuccessOspProcessCopy", "[" + dtpFromYm.Text + " -> " + dtpToYm.Text + "] " + Language.Get("COPY") + " ");
                        break;

                    case "Confirmation":
                        ShowMessage("SuccessOspProcessCopy", "[" + dtpFromYm.Text + "] " + Language.Get("CONFIRMATION") + " ");
                        break;

                    case "CancelConfirmation":
                        ShowMessage("SuccessOspProcessCopy", "[" + dtpFromYm.Text + "] " + Language.Get("DEFINITECANCELLATION") + " ");
                        break;
                }
                layoutEmpty.Visibility = LayoutVisibility.Never;
                layoutFrom.Visibility = LayoutVisibility.Never;
                layoutTo.Visibility = LayoutVisibility.Never;
                layoutOk.Visibility = LayoutVisibility.Never;

                _IsSaveReSearch = true;
            }
            catch (Exception ex)
            {
                this.ShowError(ex);
            }
            finally
            {
                //this.CloseWaitArea();
            }
        }

        #endregion Private Function
    }
}