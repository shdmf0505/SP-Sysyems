#region using

using DevExpress.XtraEditors.Controls;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons;
using Micube.SmartMES.Commons.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion using

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 출하검사 > 출하검사 결과 조회
    /// 업  무  설  명  : 출하검사 결과를 조회한다
    /// 생    성    자  : 강유라
    /// 생    성    일  : 2019-09-28 -- 통합테스트 이전에 출하검사 먼저 개발
    /// 수  정  이  력  :
    ///     1. 2021.01.20 전우성 그리드에 양품수량 컬럼 추가 및 코드 정리
    ///     2. 2021.03.04 전우성 화면 최적화 및 Grid Selection Mode를 CheckBoxSelect 변경 후 이메일 버튼을 통한 이메일 전송 기능 추가
    /// </summary>
    public partial class ShipmentInspHistory : SmartConditionManualBaseForm
    {
        #region 생성자

        public ShipmentInspHistory()
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
            grdMain.LanguageKey = "SHIPMENTINSPHISTORYLIST";
            btnMail.LanguageKey = "EMAIL";
        }

        /// <summary>
        /// 코드그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            grdMain.GridButtonItem = GridButtonItem.Export;
            grdMain.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdMain.View.AddTextBoxColumn("TXNHISTKEY", 100).SetIsHidden();
            grdMain.View.AddTextBoxColumn("TXNGROUPHISTKEY", 100).SetIsHidden();
            grdMain.View.AddTextBoxColumn("RESOURCETYPE", 100).SetIsHidden();
            grdMain.View.AddTextBoxColumn("PROCESSRELNO", 100).SetIsHidden();

            var group = grdMain.View.AddGroupColumn("SHIPMENTINSPHISTORY");
            group.AddTextBoxColumn("RESOURCEID", 200).SetLabel("LOTID");
            group.AddTextBoxColumn("DEGREE", 80);
            group.AddTextBoxColumn("LOTTYPENAME", 80).SetLabel("LOTTYPE");
            group.AddTextBoxColumn("PRODUCTDEFTYPENAME", 80).SetLabel("PRODUCTDEFTYPE");
            group.AddComboBoxColumn("INSPECTIONTYPE", 80, new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "CODECLASSID=ShipmentHistoryInspectionType"))
                 .SetLabel("FINISHINSPECTIONTYPE").SetTextAlignment(TextAlignment.Center);

            group.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            group.AddTextBoxColumn("PRODUCTDEFID", 100);
            group.AddTextBoxColumn("PRODUCTDEFVERSION", 80);
            group.AddTextBoxColumn("AREANAME", 150);
            group.AddTextBoxColumn("PROCESSSEGMENTNAME", 150);
            group.AddTextBoxColumn("TRANSITAREANAME", 150).SetLabel("TRANSITAREA");
            group.AddTextBoxColumn("CUSTOMERNAME", 100);
            group.AddTextBoxColumn("RECEIVEPCSQTYQCM", 80).SetLabel("TOTALQTY").SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric);
            group.AddTextBoxColumn("INSPECTIONQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric);
            group.AddTextBoxColumn("SPECOUTQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric);
            group.AddTextBoxColumn("GOODQTYPCS", 80).SetLabel("GOODQTY").SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric);
            group.AddTextBoxColumn("INSPECTIONDATE", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss");
            group.AddTextBoxColumn("INSPECTIONRESULT", 80);
            group.AddTextBoxColumn("INSPECTORNAME", 100);

            group = grdMain.View.AddGroupColumn("SHIPMENTINSPECTIONCAPTION");
            group.AddTextBoxColumn("FINALPROCESSSEGMENTNAME", 150).SetLabel("PROCESSSEGMENTNAME");
            group.AddTextBoxColumn("FINALAREANAME", 150).SetLabel("AREANAME");

            group = grdMain.View.AddGroupColumn("");
            group.AddComboBoxColumn("MAILSENDTYPE", 80, new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "CODECLASSID=NGMAILTYPE"));

            grdMain.View.PopulateColumns();

            grdMain.View.SetIsReadOnly();
            grdMain.ShowStatusBar = true;

            grdMain.View.SetSortOrder("INSPECTIONDATE", DevExpress.Data.ColumnSortOrder.Descending);
        }

        #endregion 컨텐츠 영역 초기화

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // Mail 버튼 클릭
            btnMail.Click += (s, e) =>
            {
                if (grdMain.View.GetCheckedRows() is DataTable dt)
                {
                    if (dt.Rows.Count.Equals(0))
                    {
                        ShowMessage("GridNoChecked"); // 체크된 행이 없습니다
                        return;
                    }

                    if (!dt.Select("INSPECTIONRESULT='OK'").Count().Equals(0))
                    {
                        ShowMessage("CantIssueMailResultOK"); // 판정결과가 OK인 경우 메일을 발송할 수 없습니다
                        return;
                    }

                    ShipmentInspectionMailPopup popup = new ShipmentInspectionMailPopup();
                    popup.SetMailData(dt);

                    if (popup.ShowDialog().Equals(DialogResult.OK))
                    {
                        string mailType = popup.GetMailType();

                        DataTable rootDt = (grdMain.DataSource as DataTable).Copy();
                        rootDt.PrimaryKey = new DataColumn[]
                        {
                            rootDt.Columns["TXNHISTKEY"],
                            rootDt.Columns["RESOURCETYPE"],
                            rootDt.Columns["RESOURCEID"]
                        };

                        DataRow modifyRow;
                        foreach (DataRow dr in dt.Rows)
                        {
                            modifyRow = rootDt.Rows.Find(new object[3] { dr["TXNHISTKEY"], dr["RESOURCETYPE"], dr["RESOURCEID"] });
                            modifyRow["MAILSENDTYPE"] = modifyRow == null ? dr["MAILSENDTYPE"] : mailType;
                        }

                        grdMain.DataSource = rootDt;
                    }
                }
            };

            //lot 검사결과 detail조회
            grdMain.View.DoubleClick += (s, e) =>
            {
                if (grdMain.View.GetFocusedDataRow()["INSPECTIONTYPE"].Equals("Inspection"))
                {
                    ShipmentInspHistoryPopup detailPopup = new ShipmentInspHistoryPopup
                    {
                        CurrentDataRow = grdMain.View.GetFocusedDataRow(),
                        _currentDt = (grdMain.DataSource as DataTable).Clone(),
                        StartPosition = FormStartPosition.CenterParent,
                        FormBorderStyle = FormBorderStyle.Sizable
                    };

                    detailPopup.ShowDialog();
                }
            };

            // 조회조건의 품목코드 x 버튼 클릭 이벤트
            if (Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID") is var control)
            {
                control.Properties.ButtonClick += (s, e) =>
                {
                    if (e.Button.Kind.Equals(ButtonPredefines.Clear))
                    {
                        Conditions.GetControl<SmartTextBox>("P_PRODUCTDEFVERSION").Text = string.Empty;
                        Conditions.GetControl<SmartTextBox>("PRODUCTDEFNAME").Text = string.Empty;
                    }
                };

                // 조회조건의 ID 항목을 backSpace로 모두 삭제시에 이름 삭제
                control.EditValueChanged += (s, e) =>
                {
                    if (Format.GetFullTrimString(control.EditValue).Equals(string.Empty))
                    {
                        if (control.Name.Equals("P_PRODUCTDEFID"))
                        {
                            Conditions.GetControl<SmartTextBox>("P_PRODUCTDEFVERSION").Text = string.Empty;
                            Conditions.GetControl<SmartTextBox>("PRODUCTDEFNAME").Text = string.Empty;
                        }
                    }
                };
            }
        }

        #endregion Event

        #region 툴바

        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            if (Format.GetString((sender as SmartButton).Name, string.Empty).Equals("NCRIssue"))
            {
                DataRow row = grdMain.View.GetFocusedDataRow();

                if (row == null)
                {
                    return;
                }

                if (!Format.GetString(row["INSPECTIONRESULT"], string.Empty).Equals("NG"))
                {
                    ShowMessage("CantIssueNCRResultOK");//판정결과가 OK인 경우 NCR 발행을 할 수 없습니다.
                    return;
                }

                ShipmentInspNCRPopup ncrPopup = new ShipmentInspNCRPopup(Format.GetString(row["RESOURCEID"]))
                {
                    CurrentDataRow = row,
                    StartPosition = FormStartPosition.CenterParent,
                    FormBorderStyle = FormBorderStyle.Sizable,
                    isEnable = Format.GetString(row["ISMODIFY"]).Equals("Y")
                };

                ncrPopup.SetConsumableDefComboBox(Format.GetString(row["RESOURCEID"], string.Empty));
                ncrPopup.ShowDialog();
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

                grdMain.View.ClearDatas();

                var values = Conditions.GetValues();
                values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);

                List<List<string>> rtnlstDay = QcmDateConditionList();

                DataTable dtInputFirstData = new DataTable();

                foreach (List<string> days in rtnlstDay)
                {
                    values["P_INSPECTIONDATE_PERIODFR"] = days[0];
                    values["P_INSPECTIONDATE_PERIODTO"] = days[1];

                    if (await SqlExecuter.QueryAsync("SelectShipmentInspHistory", "10001", values) is DataTable dtInputPartData)
                    {
                        if (!dtInputPartData.Rows.Count.Equals(0))
                        {
                            dtInputFirstData.Merge(dtInputPartData);
                        }
                    }
                }

                if (dtInputFirstData.Rows.Count.Equals(0))
                {
                    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                }

                grdMain.DataSource = dtInputFirstData;
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

            #region 작업장

            var condition = Conditions.AddSelectPopup("p_areaId", new SqlQuery("GetShipmentInspAreaList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"P_USERID={UserInfo.Current.Id}"), "AREANAME", "AREAID")
                                      .SetPopupLayout("AREA", PopupButtonStyles.Ok_Cancel, true, true)
                                      .SetPopupLayoutForm(500, 600)
                                      .SetLabel("AREA")
                                      .SetPopupResultCount(1)
                                      .SetRelationIds("P_PLANTID")
                                      .SetPosition(1.1);

            condition.Conditions.AddTextBox("AREA");

            condition.GridColumns.AddTextBoxColumn("AREAID", 150).SetValidationKeyColumn();
            condition.GridColumns.AddTextBoxColumn("AREANAME", 200);

            #endregion 작업장

            #region 고객사

            condition = Conditions.AddSelectPopup("p_customerId", new SqlQuery("GetCustomerList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "CUSTOMERNAME", "CUSTOMERID")
                                  .SetPopupLayout("CUSTOMER", PopupButtonStyles.Ok_Cancel, true, true)
                                  .SetPopupLayoutForm(500, 600)
                                  .SetLabel("CUSTOMER")
                                  .SetRelationIds("P_PLANTID")
                                  .SetPopupResultCount(1)
                                  .SetPosition(1.2);

            condition.Conditions.AddTextBox("TXTCUSTOMERID");

            condition.GridColumns.AddTextBoxColumn("CUSTOMERID", 150).SetValidationKeyColumn();
            condition.GridColumns.AddTextBoxColumn("CUSTOMERNAME", 200);

            #endregion 고객사

            #region 품목

            condition = Conditions.AddSelectPopup("P_PRODUCTDEFID", new SqlQuery("GetProductListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PRODUCTDEFID", "PRODUCTDEFID")
                                  .SetPopupLayout("PRODUCT", PopupButtonStyles.Ok_Cancel, true, true)
                                  .SetPopupLayoutForm(800, 700)
                                  .SetLabel("PRODUCT")
                                  .SetPopupResultCount(1)
                                  .SetPosition(1.3)
                                  .SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
                                  {
                                      selectedRow.ForEach(row =>
                                      {
                                          Conditions.GetControl<SmartTextBox>("P_PRODUCTDEFVERSION").Text = Format.GetString(row.GetObject("PRODUCTDEFVERSION"), "");
                                          Conditions.GetControl<SmartTextBox>("PRODUCTDEFNAME").Text = Format.GetString(row.GetObject("PRODUCTDEFNAME"), "");
                                      });
                                  });

            condition.Conditions.AddTextBox("PRODUCTDEFIDNAME");

            condition.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150).SetValidationKeyColumn();
            condition.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            condition.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetTextAlignment(TextAlignment.Center);

            // 품목버젼, 품목명 추가
            Conditions.AddTextBox("P_PRODUCTDEFVERSION").SetLabel("PRODUCTDEFVERSION").SetIsReadOnly().SetPosition(1.4);
            Conditions.AddTextBox("PRODUCTDEFNAME").SetIsReadOnly().SetPosition(1.5);

            #endregion 품목
        }

        #endregion 검색

        #region Private Function

        /// <summary>
        /// 조회조건 기간을 일별로 나누는 펑션
        /// </summary>
        /// <returns></returns>
        private List<List<string>> QcmDateConditionList()
        {
            List<List<string>> result = new List<List<string>>();

            try
            {
                DateTime dateStart = Convert.ToDateTime(Conditions.GetValues()["P_INSPECTIONDATE_PERIODFR"].ToSafeString());
                DateTime dateEnd = Convert.ToDateTime(Conditions.GetValues()["P_INSPECTIONDATE_PERIODTO"].ToSafeString());
                DateTime dateStartAdd;
                DateTime dateStartEnd;

                TimeSpan ts = dateEnd - dateStart;

                //날짜의 차이 구하기
                int diffDay = ts.Days <= 0 ? 1 : ts.Days;

                for (int i = 0; i < diffDay; i++)
                {
                    dateStartAdd = dateStart.AddDays(i);
                    dateStartEnd = dateStart.AddDays(i + 1);

                    List<string> subData = new List<string>
                    {
                        i != 0 ? dateStartAdd.ToString("yyyy-MM-dd 00:00:00") : dateStartAdd.ToString("yyyy-MM-dd HH:mm:ss"),
                        i < (diffDay - 1) ? dateStartEnd.ToString("yyyy-MM-dd 00:00:00") : dateEnd.ToString("yyyy-MM-dd HH:mm:ss")
                    };

                    result.Add(subData);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }

        #endregion Private Function
    }
}