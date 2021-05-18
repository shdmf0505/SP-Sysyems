#region using
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons;

using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Linq;
using System.Windows.Forms;
using Micube.SmartMES.Commons.Controls;
#endregion

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 공정관리 > R/C관리 > R/C LOT 적용
    /// 업  무  설  명  : R/C LOT 적용
    /// 생    성    자  : 황유성
    /// 생    성    일  : 2019-08-27
    /// 수  정  이  력  : 
    /// </summary>
    public partial class RCLotApply : SmartConditionManualBaseForm
    {
        #region ◆ Local Variables |

        /// <summary>
        /// PCNNO : PCN 번호로 조회/저장, PRODUCT : 품목으로 조회/저장
        /// </summary>
        private string transactionType;

        #endregion

        #region ◆ 생성자 |

        /// <summary>
        /// 생성자
        /// </summary>
        public RCLotApply()
        {
            InitializeComponent();
        }

        #endregion

        #region ◆ 컨텐츠 영역 초기화 |
        /// <summary>
        /// Control 설정
        /// </summary>
        protected override void InitializeContent()
        {
            base.InitializeContent();

            this.ucDataUpDownBtnCtrl.SourceGrid = this.grdWIP;
            this.ucDataUpDownBtnCtrl.TargetGrid = this.grdRCLot;
            splitContainerMain.SplitterPosition = 500;

            InitializeComboBox();
            InitializeEvent();

            setRCTargetProduct();
        }

        #region ▶ 조회조건 설정 |
        /// <summary>
        /// 검색조건 설정
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // PCN NO
            AddConditionPcnNoPopup("P_PCNNO", 1.1, false, Conditions);
            // 품목
            AddConditionProductPopup("P_PRODUCTDEFID", 1.2, false, Conditions);
        }

        /// <summary>
        /// 품목코드 조회 팝업
        /// </summary>
        /// <param name="id"></param>
        /// <param name="position"></param>
        /// <param name="isMultiSelect"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        private ConditionCollection AddConditionProductPopup(string id, double position, bool isMultiSelect, ConditionCollection conditions)
        {
            // SelectPopup 항목 추가
            var conditionProductId = conditions.AddSelectPopup(id, new SqlQuery("GetProductDefinitionList", "10004"), "PRODUCTDEFNAME", "PRODUCTDEF")
                .SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupLayoutForm(800, 800)
                .SetLabel("PRODUCTDEFID")
                .SetPosition(position)
                .SetPopupApplySelection((selectRow, gridRow) => {
                    Conditions.GetControl<SmartSelectPopupEdit>("P_PCNNO").ClearValue();
                });

            // 복수 선택 여부에 따른 Result Count 지정
            if (isMultiSelect)
            {
                conditionProductId.SetPopupResultCount(0);
            }
            else
            {
                conditionProductId.SetPopupResultCount(1);
            }

            // 팝업에서 사용되는 검색조건 (품목코드/명)
            conditionProductId.Conditions.AddTextBox("TXTPRODUCTDEFNAME");
            //제품 구분 추가 2019.08.14 배선용, 노석안 대리 요청
            conditionProductId.Conditions.AddComboBox("PRODUCTDIVISION", new SqlQuery("GetTypeList", "10001", $"CODECLASSID=ProductDivision2", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                                        .SetDefault("Product");

            // 팝업 그리드에서 보여줄 컬럼 정의
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 130)                // 품목 ID
                .SetTextAlignment(TextAlignment.Center);
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 90)            // 품목 Rev
                .SetTextAlignment(TextAlignment.Center);
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 250);             // 품목명
            conditionProductId.GridColumns.AddTextBoxColumn("CREATEDTIME", 100);                // 생성일
            return conditions;
        }

        /// <summary>
        /// PCN No. 조회 팝업
        /// </summary>
        /// <param name="id"></param>
        /// <param name="position"></param>
        /// <param name="isMultiSelect"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        private ConditionCollection AddConditionPcnNoPopup(string id, double position, bool isMultiSelect, ConditionCollection conditions)
        {
            // SelectPopup 항목 추가
            var conditionProductId = conditions.AddSelectPopup(id, new SqlQuery("GetPcnListForRC", "10001"), "PCNNO", "PCNNO")
                .SetPopupLayout("SELECTPCNNO", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupLayoutForm(800, 800)
                .SetLabel("PCNNO")
                .SetPosition(position)
                .SetPopupApplySelection((selectRow, gridRow) => {
                    Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").ClearValue();
                });

            // 복수 선택 여부에 따른 Result Count 지정
            if (isMultiSelect)
            {
                conditionProductId.SetPopupResultCount(0);
            }
            else
            {
                conditionProductId.SetPopupResultCount(1);
            }

            conditionProductId.Conditions.AddTextBox("PCNNO");
            // 팝업에서 사용되는 검색조건 (품목코드/명)
            conditionProductId.Conditions.AddTextBox("TXTPRODUCTDEFNAME");
            //제품 구분 추가 2019.08.14 배선용, 노석안 대리 요청
            conditionProductId.Conditions.AddComboBox("PRODUCTDIVISION", new SqlQuery("GetTypeList", "10001", $"CODECLASSID=ProductDivision2", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                                        .SetDefault("Product");

            // 팝업 그리드에서 보여줄 컬럼 정의
            conditionProductId.GridColumns.AddTextBoxColumn("PCNNO", 140)                       // PCN No.
                .SetTextAlignment(TextAlignment.Center);
            conditionProductId.GridColumns.AddTextBoxColumn("SUBJECT", 150);                    // 제목
            conditionProductId.GridColumns.AddTextBoxColumn("REQUESTDATE", 140)                 // 의뢰일
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetTextAlignment(TextAlignment.Center); 
            conditionProductId.GridColumns.AddTextBoxColumn("REQUESTDEPARTMENT", 90);           // 의뢰부서
            conditionProductId.GridColumns.AddTextBoxColumn("REASON", 130);                     // 사유
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 130)                // 품목 ID
                .SetTextAlignment(TextAlignment.Center);
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 90)            // 품목 Rev
                .SetTextAlignment(TextAlignment.Center);
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 250);             // 품목명
            conditionProductId.GridColumns.AddTextBoxColumn("RCPRODUCTDEFID", 90)               // R/C 품목 ID
                .SetTextAlignment(TextAlignment.Center);
            conditionProductId.GridColumns.AddTextBoxColumn("RCPRODUCTDEFVERSION", 90)          // R/C 품목 Rev.
                .SetTextAlignment(TextAlignment.Center);
            conditionProductId.GridColumns.AddTextBoxColumn("RCPRODUCTDEFNAME", 250);           // R/C 품목명
            return conditions;
        }

        /// <summary>
        /// 조회조건 설정
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();
            SetConditionVisiblility("P_PLANTID", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
            SetConditionVisiblility("P_PERIOD", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
        }

        #endregion

        #region ▶ ComboBox 초기화 |
        private void InitializeComboBox()
        {
            cboRcProductDefVersion.DisplayMember = "PRODUCTDEFVERSION";
            cboRcProductDefVersion.ValueMember = "PRODUCTDEFVERSION";
            cboRcProductDefVersion.ShowHeader = false;
            cboRcProductDefVersion.UseEmptyItem = true;
        }

        #endregion

        #region ▶ Grid 설정 |
        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            #region - 재공 Grid 설정 |
            grdWIP.GridButtonItem = GridButtonItem.Export;

            grdWIP.View.SetIsReadOnly();

            // CheckBox 설정
            grdWIP.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            var groupDefaultCol = grdWIP.View.AddGroupColumn("WIPLIST");
            groupDefaultCol.AddTextBoxColumn("PRODUCTIONTYPE", 70).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("PRODUCTDEFID", 140).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("LOTID", 190).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("PRODUCTDEFNAME", 210);
            groupDefaultCol.AddTextBoxColumn("USERSEQUENCE", 70).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("PROCESSSEGMENTNAME", 130);
            groupDefaultCol.AddTextBoxColumn("PLANTID", 70).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("AREANAME", 160).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("WIPPROCESSSTATE", 110).SetTextAlignment(TextAlignment.Center);

            groupDefaultCol.AddTextBoxColumn("CHECKLOT", 0).SetIsHidden();
            groupDefaultCol.AddTextBoxColumn("PATHSEQUENCE", 0).SetIsHidden();
            groupDefaultCol.AddTextBoxColumn("RCRESULT", 0).SetIsHidden();

            var groupWipCol = grdWIP.View.AddGroupColumn("WIPQTY");
            groupWipCol.AddTextBoxColumn("QTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            groupWipCol.AddTextBoxColumn("PANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");

            var groupLockingCol = grdWIP.View.AddGroupColumn("LOCKING");
            groupLockingCol.AddTextBoxColumn("ISLOCKING", 100).SetTextAlignment(TextAlignment.Center);

            var groupHoldCol = grdWIP.View.AddGroupColumn("HOLD");
            groupHoldCol.AddTextBoxColumn("ISHOLD", 100).SetTextAlignment(TextAlignment.Center);

            grdWIP.View.PopulateColumns();

            #endregion

            #region - R/C LOT Grid |
            grdRCLot.GridButtonItem = GridButtonItem.None;

            grdRCLot.View.SetIsReadOnly();

            // CheckBox 설정
            grdRCLot.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdRCLot.View.AddTextBoxColumn("RCRESULT", 70).SetTextAlignment(TextAlignment.Center).SetDefault("");
            grdRCLot.View.AddTextBoxColumn("PRODUCTIONTYPE", 70).SetTextAlignment(TextAlignment.Center);
            grdRCLot.View.AddTextBoxColumn("PRODUCTDEFID", 150).SetTextAlignment(TextAlignment.Center);
            grdRCLot.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetTextAlignment(TextAlignment.Center);
            grdRCLot.View.AddTextBoxColumn("LOTID", 200).SetTextAlignment(TextAlignment.Center);
            grdRCLot.View.AddTextBoxColumn("PRODUCTDEFNAME", 250);
            grdRCLot.View.AddTextBoxColumn("USERSEQUENCE", 70).SetTextAlignment(TextAlignment.Center);
            grdRCLot.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 130);
            grdRCLot.View.AddTextBoxColumn("PLANTID", 70).SetTextAlignment(TextAlignment.Center);
            grdRCLot.View.AddTextBoxColumn("AREANAME", 100).SetTextAlignment(TextAlignment.Center);
            grdRCLot.View.AddTextBoxColumn("WIPPROCESSSTATE", 100).SetTextAlignment(TextAlignment.Center);

            grdRCLot.View.AddTextBoxColumn("PATHSEQUENCE", 0).SetIsHidden();

            grdRCLot.View.AddTextBoxColumn("QTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            grdRCLot.View.AddTextBoxColumn("PANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");

            grdRCLot.View.AddTextBoxColumn("ISLOCKING", 100).SetTextAlignment(TextAlignment.Center);
            grdRCLot.View.AddTextBoxColumn("ISHOLD", 100).SetTextAlignment(TextAlignment.Center);

            grdRCLot.View.PopulateColumns();
            #endregion

            #region - 적용이력 GRID 설정

            grdApplyHistory.GridButtonItem = GridButtonItem.Export;

            grdApplyHistory.View.SetIsReadOnly();

            // CheckBox 설정
            grdApplyHistory.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            var groupChangedLot = grdApplyHistory.View.AddGroupColumn("CHANGEDLOT");

            groupChangedLot.AddTextBoxColumn("PCNNO", 160).SetTextAlignment(TextAlignment.Center);
            groupChangedLot.AddTextBoxColumn("APPLYTIME", 150).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetTextAlignment(TextAlignment.Center);
            groupChangedLot.AddTextBoxColumn("APPLYER", 100).SetTextAlignment(TextAlignment.Center);
            groupChangedLot.AddTextBoxColumn("LOTID", 210).SetTextAlignment(TextAlignment.Center);
            groupChangedLot.AddTextBoxColumn("PRODUCTDEFID", 140).SetTextAlignment(TextAlignment.Center);
            groupChangedLot.AddTextBoxColumn("PRODUCTDEFVERSION", 110).SetTextAlignment(TextAlignment.Center);
            groupChangedLot.AddTextBoxColumn("RCPRODUCTDEFID", 140).SetTextAlignment(TextAlignment.Center);
            groupChangedLot.AddTextBoxColumn("RCPRODUCTDEFVERSION", 110).SetTextAlignment(TextAlignment.Center);
            groupChangedLot.AddTextBoxColumn("USERSEQUENCE", 80).SetTextAlignment(TextAlignment.Center);
            groupChangedLot.AddTextBoxColumn("PROCESSSEGMENTID", 0).SetIsHidden();
            groupChangedLot.AddTextBoxColumn("PROCESSSEGMENTVERSION", 0).SetIsHidden();
            groupChangedLot.AddTextBoxColumn("PROCESSSEGMENTNAME", 140);
            groupChangedLot.AddTextBoxColumn("PLANTID", 80).SetTextAlignment(TextAlignment.Center);
            groupChangedLot.AddTextBoxColumn("AREAID", 0).SetIsHidden();
            groupChangedLot.AddTextBoxColumn("AREANAME", 170);
            groupChangedLot.AddTextBoxColumn("ISSPECCHANGE", 100).SetLabel("ISRC").SetTextAlignment(TextAlignment.Center);
            groupChangedLot.AddTextBoxColumn("ISSPECCHANGEPROCESSSEGMENT", 120).SetLabel("SPECCHANGEPROCESS").SetTextAlignment(TextAlignment.Center);
            groupChangedLot.AddTextBoxColumn("CHANGESTATE", 0).SetIsHidden();
            groupChangedLot.AddTextBoxColumn("CHANGESTATENAME", 90).SetTextAlignment(TextAlignment.Center);

            var groupCurrentWip = grdApplyHistory.View.AddGroupColumn("CURRENTWIP");
            groupCurrentWip.AddTextBoxColumn("LOTUSERSEQUENCE", 80).SetLabel("USERSEQUENCE").SetTextAlignment(TextAlignment.Center);
            groupCurrentWip.AddTextBoxColumn("LOTPROCESSSEGMENTID", 80).SetLabel("PROCESSSEGMENTID").SetTextAlignment(TextAlignment.Center);
            groupCurrentWip.AddTextBoxColumn("LOTPROCESSSEGMENTVERSION", 80).SetLabel("PROCESSSEGMENTVERSION").SetTextAlignment(TextAlignment.Center);
            groupCurrentWip.AddTextBoxColumn("LOTPROCESSSEGMENTNAME", 140).SetLabel("PROCESSSEGMENTNAME");
            groupCurrentWip.AddTextBoxColumn("LOTPCSQTY", 100).SetLabel("PCS").SetDisplayFormat("#,##0");
            groupCurrentWip.AddTextBoxColumn("LOTPNLQTY", 100).SetLabel("PNL").SetDisplayFormat("#,##0");

            grdApplyHistory.View.PopulateColumns();

            #endregion
        }
        #endregion

        #endregion

        #region ◆ Event |

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            this.Load += (s, e) => InitializeGrid();

            btnSave.Click += BtnSave_Click;
            grdWIP.View.DoubleClick += View_DoubleClick;
            cboRcProductDefVersion.EditValueChanged += CboRcProductDefVersion_EditValueChanged;
            tabMain.SelectedPageChanged += TabMain_SelectedPageChanged;
            txtToProductDefid.ButtonClick += TxtToProductDefid_ButtonClick;
        }

        private void TxtToProductDefid_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

            string Productdefid = Format.GetTrimString(txtProductDefId.EditValue);
            string PreoductDefVersion = Format.GetTrimString(txtProductDefVersion.EditValue);

            ConditionItemSelectPopup areaCondition = txtToProductDefid.SelectPopupCondition;

            areaCondition.SearchQuery = new SqlQuery("GetRCApplyTargetProduct", "10001", $"P_PRODUCTDEFID={Productdefid}", $"P_PRODUCTDEFVERSION={PreoductDefVersion}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}");
            areaCondition.SetPopupApplySelection((selectedRows, dataGridRows) =>
            {
                
                DataRow dr = selectedRows.ToList<DataRow>()[0];

                string ProductDefid = Format.GetTrimString(dr["PRODUCTDEFID"]) + '|' + Format.GetTrimString(dr["PRODUCTREVISION"]);
                if (dr == null) return;

                Dictionary<string, object> values = new Dictionary<string, object>();

                values.Add("P_PRODUCTDEFID", ProductDefid);

                DataTable dt = SqlExecuter.Query("GetProductDefinitionList", "10006", values);
                cboRcProductDefVersion.DataSource = dt;

                cboRcProductDefVersion.EditValueChanged -= CboRcProductDefVersion_EditValueChanged;

                cboRcProductDefVersion.EditValue = Format.GetTrimString(dr["PRODUCTREVISION"]);

                GetToProductRevision(Format.GetTrimString(dr["PRODUCTDEFID"]));

                cboRcProductDefVersion.EditValueChanged += CboRcProductDefVersion_EditValueChanged;

            });
        }

        private void TabMain_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (e.Page.Name == "pagRcLotApply")
            {
                SetConditionVisiblility("P_PLANTID", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
                SetConditionVisiblility("P_PERIOD", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
            }
            else if(e.Page.Name == "pagApplyHistory")
            {
                SetConditionVisiblility("P_PLANTID", DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
                SetConditionVisiblility("P_PERIOD", DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
            }
        }

        private void CboRcProductDefVersion_EditValueChanged(object sender, EventArgs e)
        {
            GetToProductRevision(Format.GetTrimString(txtToProductDefid.EditValue));
            /*
            txtRcRev.Text = cboRcProductDefVersion.EditValue.ToString();

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("PRODUCTDEFID", txtProductDefId.Text);
            param.Add("PRODUCTDEFVERSION", txtProductDefVersion.Text);
            param.Add("RCPRODUCTDEFID", txtToProductDefid.EditValue);
            param.Add("RCPRODUCTDEFVERSION", cboRcProductDefVersion.EditValue);
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);

            DataTable dtWip = SqlExecuter.Query("SelectRcTargetLotList", "10002", param);

            grdWIP.DataSource = null;
            grdRCLot.DataSource = null;

            if (dtWip.Rows.Count < 1)
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdWIP.DataSource = dtWip;
            */
        }

        /// <summary>
        /// 그리드 더블클릭 시 체크박스 체크
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_DoubleClick(object sender, EventArgs e)
        {
            if(grdWIP.View.FocusedRowHandle >= 0)
            {
                grdWIP.View.CheckRow(grdWIP.View.FocusedRowHandle, !grdWIP.View.IsRowChecked(grdWIP.View.FocusedRowHandle));
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            switch (tabMain.SelectedTabPage.Name)
            {
                case "pagRcLotApply":
                    if (IsShowingResult())
                    {
                        return;
                    }

                    DataTable rcLotList = grdRCLot.DataSource as DataTable;

                    if (rcLotList == null || rcLotList.Rows.Count == 0)
                    {
                        // 저장할 데이터가 존재하지 않습니다.
                        throw MessageException.Create("NoSaveData");
                    }

                    MessageWorker worker = new MessageWorker("SaveRCLot");
                    worker.SetBody(new MessageBody()
                    {
                        { "enterpriseid", UserInfo.Current.Enterprise },
                        { "plantid", UserInfo.Current.Plant },
                        { "transactiontype", this.transactionType },
                        { "pcnno", (this.transactionType == "PCNNO") ? txtPcnNo.Text : "*" },
                        { "productdefid", txtProductDefId.Text },
                        { "productdefversion", txtProductDefVersion.Text },
                        { "rcproductdefid", txtToProductDefid.EditValue },
                        { "rcproductdefversion", txtRcRev.Text },
                        { "islotcardprinted", (chkPrintLotCard.Checked) ? "Y" : "N" },
                        { "issendemail", (chkSendEmail.Checked) ? "Y" : "N" },
                        { "isspecChange", (chkSpecChange.Checked) ? "Y" : "N" },
                        { "isspecChangeProcessSegment", (chkSpecProcess.Checked) ? "Y" : "N" },
                        { "lotlist", rcLotList }
                    });

                    var saveResult = worker.Execute<DataTable>();
                    DataTable resultData = saveResult.GetResultSet();
                    FillRcResult(rcLotList, resultData);

                    ShowMessage("SuccessSave");

                    if (chkPrintLotCard.Checked)
                    {
                        var lotIdsToPrint = resultData.AsEnumerable()
                            .Where(row => row.Field<string>("RCRESULTID") == "PASS")
                            .Select(row => row.Field<string>("LOTID")).ToArray<string>();
                        if (lotIdsToPrint.Length > 0)
                        {
                            pnlContent.ShowWaitArea();
                            string lotIds = string.Join(",", lotIdsToPrint);
                            CommonFunction.PrintLotCard_Ver2(lotIds, LotCardType.Normal);  // TODO : RCChange 타입의 랏카드 발행 현재 구현되지 않았음.
                            pnlContent.CloseWaitArea();
                        }
                    }
                    break;
                case "pagApplyHistory":

                    // 이 탭페이지에는 저장기능이 없음

                    break;
            }
        }

        private bool IsShowingResult()
        {
            DataTable rcLotList = grdRCLot.DataSource as DataTable;
            foreach(DataRow each in rcLotList.Rows)
            {
                if(each["RCRESULT"].ToString() != string.Empty)
                {
                    return true;
                }
            }
            return false;
        }

        #region ▶ ComboBox Event |

        #endregion

        #endregion

        #region ◆ 툴바 |

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();
            // NOTE : 저장 후 자동으로 재조회 되는 문제 때문에 이 함수를 사용하지 않는다.
        }

        private void FillRcResult(DataTable rcLotList, DataTable result)
        {
            foreach(DataRow eachRcLot in rcLotList.Rows)
            {
                foreach(DataRow eachResult in result.Rows)
                {
                    if(eachRcLot["LOTID"].ToString() == eachResult["LOTID"].ToString())
                    {
                        eachRcLot["RCRESULT"] = eachResult["RCRESULT"].ToString();
                    }
                }
            }
        }

        #endregion

        #region ◆ 검색 |

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            switch (tabMain.SelectedTabPage.Name)
            {
                case "pagRcLotApply":
                    if (string.IsNullOrEmpty((string)values["P_PCNNO"]) && string.IsNullOrEmpty((string)values["P_PRODUCTDEFID"]))
                    {
                        txtPcnNo.Text = string.Empty;
                        txtRequestDate.Text = string.Empty;
                        txtChangeDate.Text = string.Empty;
                        txtProductDefName.Text = string.Empty;
                        txtProductDefId.Text = string.Empty;
                        txtProductDefVersion.Text = string.Empty;
                        txtToProductDefid.EditValue = null;
                        txtRcProduct.Text = string.Empty;
                        txtRcRev.Text = string.Empty;
                        txtPcnNo2.Text = string.Empty;
                        cboRcProductDefVersion.DataSource = null;
                        grdWIP.DataSource = null;
                        grdRCLot.DataSource = null;

                        // 검색할 PCN No. 또는 품목코드를 입력하세요.
                        ShowMessage("PcnNoOrProductDefIdRequired");
                        return;
                    }

                    if (!string.IsNullOrEmpty((string)values["P_PCNNO"]))
                    {
                        transactionType = "PCNNO";
                    }
                    else
                    {
                        transactionType = "PRODUCT";
                    }

                    if (transactionType == "PCNNO")
                    {
                        cboRcProductDefVersion.ReadOnly = true;

                        // 기존 Grid Data 초기화
                        this.grdWIP.DataSource = null;
                        this.grdRCLot.DataSource = null;

                        DataTable dtProductDefVersions = await SqlExecuter.QueryAsync("GetProductDefinitionList", "10004", values);
                        cboRcProductDefVersion.DataSource = dtProductDefVersions;

                        DataTable pcnInfo = await SqlExecuter.QueryAsync("GetPcnInfoForRC", "10001", values);
                        cboRcProductDefVersion.EditValueChanged -= CboRcProductDefVersion_EditValueChanged;
                        FillPcnInfo(pcnInfo);
                        cboRcProductDefVersion.EditValueChanged += CboRcProductDefVersion_EditValueChanged;
                        if (pcnInfo.Rows.Count == 0)
                        {
                            return;
                        }

                        Dictionary<string, object> param = new Dictionary<string, object>();
                        param.Add("PRODUCTDEFID", pcnInfo.Rows[0]["PRODUCTDEFID"].ToString());
                        param.Add("PRODUCTDEFVERSION", pcnInfo.Rows[0]["PRODUCTDEFVERSION"].ToString());
                        param.Add("RCPRODUCTDEFID", pcnInfo.Rows[0]["RCPRODUCTDEFID"].ToString());
                        param.Add("RCPRODUCTDEFVERSION", pcnInfo.Rows[0]["RCPRODUCTDEFVERSION"].ToString());
                        param.Add("PCNNO", txtPcnNo.Text);
                        param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                        DataTable dtWip = await SqlExecuter.QueryAsync("SelectRcTargetLotList", "10002", param);

                        if (dtWip.Rows.Count < 1)
                        {
                            ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                        }

                        grdWIP.DataSource = dtWip;
                        MoveRequestedLots();
                    }
                    else
                    {
                        cboRcProductDefVersion.ReadOnly = false;

                        DataTable dtProductList = await SqlExecuter.QueryAsync("GetProductDefinitionList", "10004", values);

                        if (dtProductList.Rows.Count < 1)
                        {
                            txtPcnNo.Text = string.Empty;
                            txtRequestDate.Text = string.Empty;
                            txtChangeDate.Text = string.Empty;
                            txtProductDefName.Text = string.Empty;
                            txtProductDefId.Text = string.Empty;
                            txtProductDefVersion.Text = string.Empty;
                            txtToProductDefid.EditValue = null;
                            txtRcProduct.Text = string.Empty;
                            txtRcRev.Text = string.Empty;
                            txtPcnNo2.Text = string.Empty;
                            cboRcProductDefVersion.DataSource = null;

                            ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                        }

                        txtPcnNo.Text = string.Empty;
                        txtRequestDate.Text = string.Empty;
                        txtChangeDate.Text = string.Empty;
                        txtProductDefName.Text = Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").EditValue.ToString();
                        txtProductDefId.Text = values["P_PRODUCTDEFID"].ToString().Split('|')[0];
                        txtProductDefVersion.Text = values["P_PRODUCTDEFID"].ToString().Split('|')[1];
                        
                        txtToProductDefid.EditValue = values["P_PRODUCTDEFID"].ToString().Split('|')[0];
                        txtToProductDefid.Text = values["P_PRODUCTDEFID"].ToString().Split('|')[0];

                        //txtRcProduct.Text = values["P_PRODUCTDEFID"].ToString().Split('|')[0];
                        txtRcRev.Text = string.Empty;
                        txtPcnNo2.Text = string.Empty;

                        cboRcProductDefVersion.DataSource = dtProductList;
                        cboRcProductDefVersion.EditValueChanged -= CboRcProductDefVersion_EditValueChanged;
                        cboRcProductDefVersion.EditValue = null;
                        cboRcProductDefVersion.EditValueChanged += CboRcProductDefVersion_EditValueChanged;
                    }
                    break;
                case "pagApplyHistory":

                    // 기존 Grid Data 초기화
                    this.grdApplyHistory.DataSource = null;

                    values.Add("P_USERID", UserInfo.Current.Id);
                    DataTable dtHistory = await SqlExecuter.QueryAsync("SelectRcApplyHistory", "10001", values);

                    if (dtHistory.Rows.Count < 1)
                    {
                        ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                    }

                    grdApplyHistory.DataSource = dtHistory;

                    break;
            }
        }

        private void MoveRequestedLots()
        {
            DataTable source = grdWIP.DataSource as DataTable;
            DataTable dtWip = source.Clone();
            DataTable dtRcLot = source.Clone();

            for (int i = 0; i < source.Rows.Count; i++)
            {
                DataRow each = source.Rows[i];
                if(each["CHECKLOT"].ToString() == "Y")
                {
                    dtRcLot.ImportRow(each);
                }
                else
                {
                    dtWip.ImportRow(each);
                }
            }

            grdWIP.DataSource = dtWip;
            grdRCLot.DataSource = dtRcLot;
        }

        private void FillPcnInfo(DataTable dataTable)
        {
            if(dataTable.Rows.Count > 0)
            {
                txtPcnNo.Text = dataTable.Rows[0]["PCRNO"].ToString();
                txtRequestDate.Text = dataTable.Rows[0]["REQUESTDATE"].ToString();
                txtChangeDate.Text = dataTable.Rows[0]["IMPLEMENTATIONDATE"].ToString();
                txtProductDefName.Text = dataTable.Rows[0]["PRODUCTDEFNAME"].ToString();
                txtProductDefId.Text = dataTable.Rows[0]["PRODUCTDEFID"].ToString();
                txtProductDefVersion.Text = dataTable.Rows[0]["PRODUCTDEFVERSION"].ToString();
                txtToProductDefid.EditValue = dataTable.Rows[0]["RCPRODUCTDEFID"].ToString();
                cboRcProductDefVersion.EditValue = dataTable.Rows[0]["RCPRODUCTDEFVERSION"].ToString();
                txtRcProduct.Text = dataTable.Rows[0]["RCPRODUCTDEFID"].ToString();
                txtRcRev.Text = dataTable.Rows[0]["RCPRODUCTDEFVERSION"].ToString();
                txtPcnNo2.Text = dataTable.Rows[0]["PCRNO"].ToString();
            }
            else
            {
                txtPcnNo.Text = string.Empty;
                txtRequestDate.Text = string.Empty;
                txtChangeDate.Text = string.Empty;
                txtProductDefName.Text = string.Empty;
                txtProductDefId.Text = string.Empty;
                txtProductDefVersion.Text = string.Empty;
                txtToProductDefid.EditValue = null;
                cboRcProductDefVersion.EditValue = string.Empty;
                txtRcProduct.Text = string.Empty;
                txtRcRev.Text = string.Empty;
                txtPcnNo2.Text = string.Empty;
            }
        }

        #endregion

        #region ◆ 유효성 검사 |

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();
        }

        #endregion

        #region ◆ Private Function |

        private void GetToProductRevision(string toProductdefID)
        {
            txtRcRev.Text = cboRcProductDefVersion.EditValue.ToString();

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("PRODUCTDEFID", txtProductDefId.Text);
            param.Add("PRODUCTDEFVERSION", txtProductDefVersion.Text);
            param.Add("RCPRODUCTDEFID", toProductdefID);
            param.Add("RCPRODUCTDEFVERSION", cboRcProductDefVersion.EditValue);
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);

            DataTable dtWip = SqlExecuter.Query("SelectRcTargetLotList", "10002", param);

            grdWIP.DataSource = null;
            grdRCLot.DataSource = null;

            if (dtWip.Rows.Count < 1)
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdWIP.DataSource = dtWip;
        }
        private void setRCTargetProduct()
        {

            string Productdefid = Format.GetTrimString(txtProductDefId.EditValue);
            string PreoductDefVersion = Format.GetTrimString(txtProductDefVersion.EditValue);

            ConditionItemSelectPopup areaCondition = new ConditionItemSelectPopup();
            areaCondition.Id = "PRODUCTDEFID";
            areaCondition.SearchQuery = new SqlQuery("GetRCApplyTargetProduct", "10001", $"P_PRODUCTDEFID={Productdefid}", $"P_PRODUCTDEFVERSION={PreoductDefVersion}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}");
            areaCondition.ValueFieldName = "PRODUCTDEFID";
            areaCondition.DisplayFieldName = "PRODUCTDEFID";
            areaCondition.SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true,true);
            areaCondition.SetPopupResultCount(1);
            areaCondition.SetPopupLayoutForm(600, 800, FormBorderStyle.FixedToolWindow);
            //areaCondition.SetPopupAutoFillColumns("AREANAME");

            areaCondition.Conditions.AddTextBox("PRODUCTDEFID");

            areaCondition.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            areaCondition.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            areaCondition.GridColumns.AddTextBoxColumn("PRODUCTREVISION", 70);

            txtToProductDefid.SelectPopupCondition = areaCondition;
        }
        #endregion
    }
}
