#region using
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons;

using System;
using Micube.SmartMES.Commons.Controls;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Xml;
using System.IO;
#endregion

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 공정관리 > 포장작업 > 라벨출력
    /// 업  무  설  명  : 라벨출력 팝업
    /// 생    성    자  : 박정훈
    /// 생    성    일  : 2020-03-05
    /// 수  정  이  력  : 2020-04-02 황유성 RD 라이브러리 사용
    /// 
    /// 
    /// </summary>
    public partial class PrintPackingLabelPopup4 : SmartPopupBaseForm, ISmartCustomPopup
	{
        #region ◆ Public Variables |
        /// <summary>
        /// DataRow
        /// </summary>
        public DataRow CurrentDataRow { get; set; }
        #endregion

        #region ◆ Private Variables |
        /// <summary>
        /// View List
        /// </summary>

        private List<Control> bindingControls;
        private DataTable currentBoxDataSource;
        private List<LabelInfo> labelInfoList;
        private readonly object locker = new object();
        private bool printing = false; 

        #endregion

        #region ◆ 생성자 |
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="viewList"></param>
        public PrintPackingLabelPopup4(string boxnos)
		{
			InitializeComponent();
            this.bindingControls = new List<Control>();

            if (!this.IsDesignMode())
			{
                InitializeContent();
                InitializeEvent();
			}

            if (!string.IsNullOrWhiteSpace(boxnos))
            {
                Search(boxnos, true);
            }
        }
        #endregion

        #region ◆ Control 초기화 |
        /// <summary>
        /// Control 초기화
        /// </summary>
        protected void InitializeContent()
        {
            InitializeControls();
            InitializeGrid();
        }

        #region ▶ Control Initialize |
        /// <summary>
        /// Control Initialize
        /// </summary>
        private void InitializeControls()
        {
            GetAllControls(this.panel2, this.bindingControls);

            // Ship To
            cboShipTo.Editor.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboShipTo.Editor.ValueMember = "CODEID";
            cboShipTo.Editor.DisplayMember = "CODENAME";
            cboShipTo.Editor.UseEmptyItem = true;

            cboShipTo.Editor.DataSource = SqlExecuter.Query("GetCodeList", "00001"
                    , new Dictionary<string, object>() { { "CODECLASSID", "LABELSHIPTARGET" }, { "LANGUAGETYPE", UserInfo.Current.LanguageType } });
            
            cboShipTo.Editor.ShowHeader = false;

            // 거래처
            cboCustomerId.Editor.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberAndValueMember;
            cboCustomerId.Editor.ValueMember = "CUSTOMERNAME";
            cboCustomerId.Editor.DisplayMember = "CUSTOMERID";
            cboCustomerId.Editor.UseEmptyItem = false;
            cboCustomerId.Editor.ShowHeader = false;
        }
        #endregion

        #region ▶ Grid 초기화 |
        /// <summary>
        /// Grid 초기화
        /// </summary>
        private void InitializeGrid()
        {
            #region - 포장 Grid 설정 |
            grdPackingList.GridButtonItem = GridButtonItem.None;
            grdPackingList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdPackingList.View.SetIsReadOnly();
            grdPackingList.SetIsUseContextMenu(false);
            grdPackingList.View.AddTextBoxColumn("PARENTLOTID", 160).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdPackingList.View.AddTextBoxColumn("LOTID", 160).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdPackingList.View.AddTextBoxColumn("BOXNO", 100).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdPackingList.View.AddTextBoxColumn("PCSQTY", 60).SetDisplayFormat("{0:#,###}").SetTextAlignment(TextAlignment.Right).SetIsReadOnly().SetLabel("QTY");
            grdPackingList.View.AddTextBoxColumn("WORKER", 80).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdPackingList.View.AddTextBoxColumn("PACKINGDATE", 120).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdPackingList.View.PopulateColumns();
            #endregion
        }
        #endregion

        #endregion

        #region ◆ Event |
        /// <summary>
        /// Event 초기화
        /// </summary>
        private void InitializeEvent()
		{
            this.Load += PrintPackingLabelPopup_Load;
            this.schBoxNo.Editor.KeyDown += SchBoxNo_KeyDown;
            this.grdPackingList.View.FocusedRowChanged += View_FocusedRowChanged;
            this.grdPackingList.View.CheckStateChanged += View_CheckStateChanged;
            this.cboCustomerId.Editor.EditValueChanged += Editor_EditValueChanged;
            this.rdoLabelType.EditValueChanged += (s, e) =>
            {
                UpdateLabelType();
            };

            this.btnSearch.Click += (s, e) =>
             {
                 Search(this.schBoxNo.Text);
             };

            this.btnLabelPrint.Click += BtnLabelPrint_Click;
            this.btnClose.Click += (s, e) =>
            {
                this.Close();
            };

            this.KeyDown += (s, e) =>
            {
                if (e.KeyCode != Keys.F5) return;
                Search(this.schBoxNo.Text);
            };

            this.btnSave.Click += (s, e) =>
            {
                Save();
            };

            axRdviewer501.VisibleChanged += AxRdviewer501_VisibleChanged;

            (FindControl("BLOCK") as SmartLabelTextBox).Editor.Leave += Editor_LeaveForXOut;
            (FindControl("PCSARY") as SmartLabelTextBox).Editor.Leave += Editor_LeaveForXOut;
            (FindControl("QTY") as SmartLabelTextBox).Editor.Leave += Editor_LeaveForXOut;

            (FindControl("QTY") as SmartLabelTextBox).Editor.Leave += Editor_LeaveForQty;
            (FindControl("PCSTRAY") as SmartTextBox).Leave += Editor_LeaveForQty;
            (FindControl("TRAYBOX") as SmartTextBox).Leave += Editor_LeaveForQty;

            scbYDoesNotApply.CheckedChanged += ScbYDoesNotApply_CheckedChanged;
            scbAutoLot1.CheckedChanged += ScbAutoLot1_CheckedChanged;

            // 출력라벨 선택 시 이벤트 발생
            slcbPrintBoxLabel.Editor.EditValueChanged += SlcbPrintBoxLabel_EditValueChanged;

            dtdPublishDate.Editor.EditValueChanged += Editor_EditValueChanged1;
        }

        private void Editor_EditValueChanged1(object sender, EventArgs e)
        {
            // todo here
            if (slcbPrintBoxLabel.Editor.EditValue == null)
            {
                return;
            }
            // SDC-C 박스/케이스 라벨의 발행일자를 수정하면 LOTNO1을 발행일자값으로 변경(케이스 라벨의 Packing No 때문)
            if (slcbPrintBoxLabel.Editor.EditValue.ToString().Equals("026") || slcbPrintBoxLabel.Editor.EditValue.ToString().Equals("027"))
            {
                SetProperty("LOTNO1", dtdPublishDate.EditValue.ToString().Replace("-", "").Substring(0, 8));
            }
        }

        #region ▶ Form_Load |
        /// <summary>
        /// Page Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintPackingLabelPopup_Load(object sender, EventArgs e)
        {
            this.slcbPrintBoxLabel.Editor.DisplayMember = "LABELDEFNAME";
            this.slcbPrintBoxLabel.Editor.ValueMember = "LABELDEFID";

            // 라벨타입 라디오버튼 초기화
            Dictionary<string, object> radioParam = new Dictionary<string, object>()
            {
                {"CODECLASSID", "PackingLabelType" },
                {"LANGUAGETYPE", UserInfo.Current.LanguageType}
            };

            DataTable radioDt = SqlExecuter.Query("GetCodeList", "00001", radioParam);

            foreach (DataRow dr in radioDt.Rows)
                rdoLabelType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.RadioGroupItem(dr["CODEID"], dr["CODENAME"].ToString()));

            rdoLabelType.EditValue = "Box";
            UpdateLabelType();
        }
        #endregion

        #region ▶ 출력라벨 선택 :: SlcbPrintBoxLabel_EditValueChanged |
        /// <summary>
        /// 출력라벨 선택
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SlcbPrintBoxLabel_EditValueChanged(object sender, EventArgs e)
        {
            if (slcbPrintBoxLabel.Editor.EditValue == null || string.IsNullOrWhiteSpace(slcbPrintBoxLabel.Editor.EditValue.ToString()))
                return;
             
            DataTable dt = grdPackingList.View.GetCheckedRows();
            if (dt.Rows.Count == 0)
            {
                return;
            }
            LoadBoxData(dt.Rows[0], cboCustomerId.Editor.GetDisplayText());

            // TSMT / LITE-ON 라벨인 경우
            if (slcbPrintBoxLabel.Editor.EditValue.ToString().Equals("063") || slcbPrintBoxLabel.Editor.EditValue.ToString().Equals("064")
                || slcbPrintBoxLabel.Editor.EditValue.ToString().Equals("068") || slcbPrintBoxLabel.Editor.EditValue.ToString().Equals("069"))
            {
                txtWarranty.Visible = false;
                txtLGDXOUT.Visible = false;

                scbLotFix.Visible = true;

                txtLotNo4.Location = new System.Drawing.Point(5, 237);
                txtLotNo4.Size = new Size(219, 20);
                txtLotNo4.Visible = true;
            }
            // LGD 인경우
            else if(slcbPrintBoxLabel.Editor.EditValue.ToString().Equals("024") || slcbPrintBoxLabel.Editor.EditValue.ToString().Equals("025")
                || slcbPrintBoxLabel.Editor.EditValue.ToString().Equals("060"))
            {
                txtWarranty.Visible = false;
                scbLotFix.Visible = false;
                txtLotNo4.Visible = false;

                txtLGDXOUT.Location = new System.Drawing.Point(5, 237);
                txtLGDXOUT.Size = new Size(219, 20);
                txtLGDXOUT.Visible = true;
            }
            else
            {
                txtWarranty.Visible = true;

                scbLotFix.Visible = false;
                txtLotNo4.Visible = false;
                txtLGDXOUT.Visible = false;
            }

            ShowNextSerialNo();
            UpdateLotNoASuffix();
        }

        private void ShowNextSerialNo()
        {
            if(slcbPrintBoxLabel.Editor.EditValue == null)
            {
                return;
            }

            // SDC 라벨인 경우
            if (slcbPrintBoxLabel.Editor.EditValue.ToString().Equals("026") || slcbPrintBoxLabel.Editor.EditValue.ToString().Equals("027")
                || slcbPrintBoxLabel.Editor.EditValue.ToString().Equals("028") || slcbPrintBoxLabel.Editor.EditValue.ToString().Equals("029"))
            {
                // LOT 자동이 아니면
                if (!scbAutoLot1.Checked)
                {
                    string idClassId = "";
                    string[] labelIdList = this.slcbPrintBoxLabel.EditValue.ToString().Split(',');

                    foreach (LabelInfo each in labelInfoList)
                    {
                        if (labelIdList.Contains(each.LabelID))
                        {
                            idClassId = each.BarcodeOption;
                            break;
                        }
                    }

                    if (!string.IsNullOrEmpty(idClassId))
                    {
                        DataTable dt = SqlExecuter.Query("GetNextSerialNo", "10001", new Dictionary<string, object>() { { "IDCLASSID", idClassId } });
                        if (dt.Rows.Count > 0)
                        {
                            SetProperty("BOXLOTNO1", dt.Rows[0]["NEXTSERIALNO"].ToString());
                        }
                    }
                }
            }
        }
        #endregion

        #region - AxRdviewer501_VisibleChanged |
        private void AxRdviewer501_VisibleChanged(object sender, EventArgs e)
        {
            axRdviewer501.VisibleChanged -= AxRdviewer501_VisibleChanged;
            axRdviewer501.Visible = false;
            axRdviewer501.VisibleChanged += AxRdviewer501_VisibleChanged;
        }
        #endregion

        #region - View_CheckStateChanged |
        private void View_CheckStateChanged(object sender, EventArgs e)
        {
            DataTable dt = grdPackingList.View.GetCheckedRows();
            if (dt.Rows.Count == 0)
            {
                cboCustomerId.EditValue = null;
                cboCustomerId.Editor.DataSource = null;
            }
            else
            {
                DataTable customers = SqlExecuter.Query("GetCustomerListOfProduct", "10001"
                        , new Dictionary<string, object>() { { "PRODUCTDEFID", dt.Rows[0]["PRODUCTDEFID"].ToString() } });
                cboCustomerId.Editor.DataSource = customers;

                foreach (DataRow each in customers.Rows)
                {
                    if (each["CUSTOMERID"].ToString() != "10000")
                    {
                        cboCustomerId.EditValue = each["CUSTOMERNAME"].ToString();
                        break;
                    }
                }

                // 수량 업데이트
                decimal goodSum = 0;
                decimal badSum = 0;
                foreach (DataRow each in dt.Rows)
                {
                    goodSum += (decimal)each["PCSQTY"];
                    badSum += (decimal)each["DEFECTQTY"];
                }

                if (rdoLabelType.EditValue == null || rdoLabelType.EditValue.ToString() != "Export")
                {
                    SetProperty("QTY", goodSum.ToString());
                    //SetProperty("DEFECTQTY", badSum.ToString());
                }

                SetProperty("BLOCK", "0");
                UpdateDefectAndXOut();
                UpdateQty();
            }
        }
        #endregion

        #region - Editor_EditValueChanged |
        private void Editor_EditValueChanged(object sender, EventArgs e)
        {
            if (cboCustomerId.Editor.EditValue == null)
            {
                txtCustomerName.Text = string.Empty;
                ClearLabelValue();
            }
            else
            {
                txtCustomerName.Text = cboCustomerId.Editor.GetDataValue().ToString();

                DataTable dt = grdPackingList.View.GetCheckedRows();
                if (dt.Rows.Count == 0)
                {
                    return;
                }

                LoadBoxData(dt.Rows[0], cboCustomerId.Editor.GetDisplayText(), true);

                ShowNextSerialNo();

                decimal goodSum = 0;
                decimal badSum = 0;
                foreach (DataRow each in dt.Rows)
                {
                    goodSum += (decimal)each["PCSQTY"];
                    badSum += (decimal)each["DEFECTQTY"];
                }

                if (rdoLabelType.EditValue == null || rdoLabelType.EditValue.ToString() != "Export")
                {
                    SetProperty("QTY", goodSum.ToString());
                    //SetProperty("DEFECTQTY", badSum.ToString());
                }

                SetProperty("BLOCK", "0");
                UpdateDefectAndXOut();
                UpdateQty();
                UpdateLabelType();
            }
        }
        #endregion

        #region - Editor_LeaveForQty |
        private void Editor_LeaveForQty(object sender, EventArgs e)
        {
            UpdateQty();
        }
        #endregion

        #region - ScbYDoesNotApply_CheckedChanged |
        private void ScbYDoesNotApply_CheckedChanged(object sender, EventArgs e)
        {
            UpdateALotNo(this.currentBoxDataSource);
        }
        #endregion

        #region - ScbAutoLot1_CheckedChanged |
        private void ScbAutoLot1_CheckedChanged(object sender, EventArgs e)
        {
            UpdateLotNo(this.currentBoxDataSource, "2");
            ShowNextSerialNo();
        }
        #endregion

        #region  - Editor_LeaveForXOut |
        private void Editor_LeaveForXOut(object sender, EventArgs e)
        {
            UpdateDefectAndXOut();
            UpdateQty();
        }
        #endregion

        #region - View_FocusedRowChanged |
        private void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //LoadBoxData();
            //UpdateLabelType();
        }
        #endregion

        #region - 라벨 출력 :: BtnLabelPrint_Click |
        /// <summary>
        /// 라벨 출력
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLabelPrint_Click(object sender, EventArgs e)
        {
            lock (locker)
            {
                if (printing)
                {
                    return;
                }
                else
                {
                    printing = true;
                }
            }

            try
            {
                // Validation Check
                CheckValidation();

                DataTable checkedRows = this.grdPackingList.View.GetCheckedRows();
                if (checkedRows.Rows.Count == 0)
                    return;

                if (this.slcbPrintBoxLabel.EditValue == null)
                    return;

                Save();
                string[] labelIdList = this.slcbPrintBoxLabel.EditValue.ToString().Split(',');

                //int export = checkedRows.AsEnumerable().Select(r => r.Field<string>("PATHTYPE").Equals("EXPORT")).Count();

                if (rdoLabelType.EditValue != null && rdoLabelType.EditValue.ToString() == "Export")
                {
                    foreach (DataRow eachRow in checkedRows.Rows)
                    {
                        PrintLable(labelIdList, eachRow);
                    }
                }
                else
                {
                    DataRow dr = checkedRows.Rows[0];

                    PrintLable(labelIdList, dr);
                }
            }
            finally
            {
                lock (locker)
                {
                    printing = false;
                }
            }
        }
        #endregion

        #region - SchBoxNo_KeyDown |
        private void SchBoxNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            Search(this.schBoxNo.Text);
        }
        #endregion
        #endregion

        #region ◆ Function |

        #region - 저장 :: Save() |
        /// <summary>
        /// 저장
        /// </summary>
        private void Save()
        {
            DataTable dt = grdPackingList.View.GetCheckedRows();
            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }

            ValidateLGDLabel();
            ValidateDefectAndXOut();
            UpdateQty();

            Dictionary<string, object> param = new Dictionary<string, object>();
            foreach (Control each in this.bindingControls)
            {
                if (each.Tag.ToString() == "LOTNOA")
                {
                    if (scbYDoesNotApply.Checked)
                    {
                        param.Add(each.Tag.ToString(), each.Text);
                    }
                    else
                    {
                        param.Add(each.Tag.ToString(), each.Text.Substring(1));
                    }
                }
                else if (each is SmartCheckEdit)
                {
                    SmartCheckEdit edit = each as SmartCheckEdit;
                    param.Add(each.Tag.ToString(), edit.Checked ? "Y" : "N");
                }
                else if (each is SmartCheckBox)
                {
                    SmartCheckBox edit = each as SmartCheckBox;
                    param.Add(each.Tag.ToString(), edit.Checked ? "Y" : "N");
                }
                else
                {
                    param.Add(each.Tag.ToString(), each.Text.Trim());
                }
            }

            MessageWorker worker = new MessageWorker("SavePackingLabelInfo");
            worker.SetBody(new MessageBody()
                {
                    { "LIST", dt },
                    { "PARAM", param },
                });

            worker.Execute();
            //LoadBoxData();
        }
        #endregion

        private void ValidateLGDLabel()
        {
            string[] labelIdList = this.slcbPrintBoxLabel.EditValue.ToString().Split(',');
            foreach(string each in labelIdList)
            {
                if (each == "024" || each == "025" || each == "060")
                {
                    if (GetProperty("BOXWEEK").Length != 4)
                    {
                        throw MessageException.Create("주차는 4자리이어야 합니다.");
                    }
                    if (GetProperty("LOTNOA").Length != 9)
                    {
                        throw MessageException.Create("LOT NO는 9자리이어야 합니다.");
                    }
                }
            }
        }

        #region - 조회 :: Search() |
        /// <summary>
        /// 조회
        /// </summary>
        /// <param name="boxNos"></param>
        /// <param name="isInitialize"></param>
        private void Search(string boxNos, bool isInitialize = false)
        {
            this.schBoxNo.Text = boxNos;
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("BOXNO", boxNos);

            DataTable dt = SqlExecuter.Query("SelectPackingAndPackageWipList", "10003", param);
            grdPackingList.DataSource = dt;
            if (dt == null || dt.Rows.Count == 0)
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                return;
            }
        }
        #endregion

        #region - UpdateLabelType() |
        private void UpdateLabelType()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("LABELDEFNAME");
            dt.Columns.Add("LABELDEFID");

            this.slcbPrintBoxLabel.Editor.DataSource = dt;

            /*
            if (this.grdPackingList.View.FocusedRowHandle < 0)
                return;
            */
            if (this.grdPackingList.View.GetCheckedRows().Rows.Count == 0)
            {
                return;
            }

            if (this.rdoLabelType.EditValue == null)
                return;

            DataRow selectedRow = this.grdPackingList.View.GetCheckedRows().Rows[0];
            this.labelInfoList = SmartMES.Commons.CommonFunction.GetLabelData(selectedRow["BOXNO"].ToString()
                , rdoLabelType.EditValue.ToString(), selectedRow["PATHTYPE"].ToString().Equals("SHIPPING") ? "Y" : "N"
                , cboCustomerId.Editor.GetDisplayText());

            foreach (LabelInfo each in labelInfoList)
            {
                DataRow newRow = dt.NewRow();
                newRow["LABELDEFNAME"] = each.LabelName;
                newRow["LABELDEFID"] = each.LabelID;
                dt.Rows.Add(newRow);
            }

            this.slcbPrintBoxLabel.Editor.DataSource = dt;
            if (rdoLabelType.EditValue != null && rdoLabelType.EditValue.ToString() == "Export")
            {
                if (dt.Rows.Count > 0)
                {
                    slcbPrintBoxLabel.EditValue = "PackingExport";
                }
            }
        }
        #endregion

        #region - LoadBoxData() |
        private void LoadBoxData(DataRow dr, string customerId, bool ignoreFixData = false)
        {
            if (chkFixData.Checked && ignoreFixData == false) return;
            if (dr == null) return;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("BOXNO", dr["BOXNO"].ToString());
            param.Add("CUSTOMERID", customerId);

            DataTable dt = SqlExecuter.Query("SelectPackingLabelInfo", "10002", param);

            if (dt == null || dt.Rows.Count == 0)
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                return;
            }

            SetBindingData(dt);
        }
        #endregion

        #region - UsingPrintDBBarcodeScript() |
        private void UsingPrintDBBarcodeScript(LabelInfo labelInfo)
        {
            SetBindingLabelValue(labelInfo);
        }
        #endregion

        #region - GetAllControls(Control container, List<Control> list) |
        private List<Control> GetAllControls(Control container, List<Control> list)
        {
            foreach (Control c in container.Controls)
            {
                if (c.Tag != null && !string.IsNullOrWhiteSpace(c.Tag.ToString()))
                    list.Add(c);
                if (c.HasChildren)
                    list = GetAllControls(c, list);
            }
            if (!list.Contains(smartMemoEdit1))
            {
                list.Add(smartMemoEdit1);
            }
            return list;
        }
        #endregion

        #region - SetBindingLabelValue(LabelInfo labelInfo) |
        private void SetBindingLabelValue(LabelInfo labelInfo)
        {
            string filePath = "http://172.16.4.58/ype_rd/sale/" + labelInfo.RdFileName;
            string rdAgent = "/rf [http://121.139.148.132:8080/RDServer/rdagent.jsp]";
            string option = "/rwait";

            if (!string.IsNullOrEmpty(labelInfo.RdParameters))
            {
                int numberOfCopies = 1;
                if (rdoLabelType.EditValue != null && rdoLabelType.EditValue.ToString() == "Box")
                {
                    int n = SafeParseInt(GetProperty("BOXQTY2"));
                    if (n > 0)
                    {
                        numberOfCopies = n;
                    }
                }
                else if (rdoLabelType.EditValue != null && rdoLabelType.EditValue.ToString() == "Tray")
                {
                    int n = SafeParseInt(GetProperty("TRAYQTY2"));
                    if (n > 0)
                    {
                        numberOfCopies = n;
                    }
                }

                List<Dictionary<string, object>> properties = new List<Dictionary<string, object>>();
                AddLabelPropertyDefault(labelInfo, properties, numberOfCopies);

                foreach (var property in properties)
                {
                    string parameters = labelInfo.RdParameters;
                    foreach (var item in property)
                    {
                        string findProperty = string.Format("{{!@{0}}}", item.Key);

                        try
                        {
                            if (!string.IsNullOrWhiteSpace(item.Value.ToString()))
                                parameters = parameters.Replace(findProperty, item.Value.ToString());
                            else
                                parameters = parameters.Replace(findProperty, "");
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    axRdviewer501.FileOpen(filePath, rdAgent + " " + parameters + " " + option);
                    axRdviewer501.SetPrintInfo(UserInfo.Current.Printer, 1, 1, 4);
                    axRdviewer501.CMPrint();
                }
            }
            else
            {
                MessageBox.Show(string.Format("등록된 파라미터가 없습니다 Label ID : {0}", labelInfo.LabelName));
            }
        }
        #endregion

        #region - AddLabelProperty(LabelInfo labelInfo, Dictionary<string, object> properties, string CaseSeq, string caseQty) |
        private void AddLabelProperty(LabelInfo labelInfo, Dictionary<string, object> properties, string CaseSeq, string caseQty)
        {
            switch (labelInfo.LabelID)
            {
                case "028": //SDC BOX
                    CommonFunction.AddLabePropertySDCBOX(currentBoxDataSource, properties);
                    break;
                case "027": //SDC U TYPE_CASE
                    CommonFunction.AddLabePropertySDCUtypeCase(currentBoxDataSource, properties, CaseSeq, caseQty);
                    break;
            }
        }
        #endregion

        #region - AddLabelPropertyDefault(LabelInfo labelInfo, List<Dictionary<string, object>> properties) |
        private void AddLabelPropertyDefault(LabelInfo labelInfo, List<Dictionary<string, object>> properties, int numberOfCopies)
        {
            DataTable dt = labelInfo.LabelDataTable;
            if (dt.Rows.Count == 0)
            {
                return;
            }

            if (!string.IsNullOrWhiteSpace(labelInfo.BarcodeOption)) // 포장일련번호를 채번하는 경우
            {
                MessageWorker worker = new MessageWorker("GetNextPackingSerials");

                // ZDT, SDC 라벨은 인쇄매수 설정시 일련번호 
                if (labelInfo.LabelID == "001" || labelInfo.LabelID == "002"
                    || labelInfo.LabelID == "026" || labelInfo.LabelID == "027" || labelInfo.LabelID == "028" || labelInfo.LabelID == "029")
                {
                    worker.SetBody(new MessageBody()
                    {
                        { "idclassid", labelInfo.BarcodeOption }
                        , { "numberofserials", Format.GetInteger(dt.Rows[0]["NUMBER_OF_SERIALS"]) }
                    });
                }
                else
                {
                    worker.SetBody(new MessageBody()
                    {
                        { "idclassid", labelInfo.BarcodeOption }
                        , { "numberofserials", Format.GetInteger(dt.Rows[0]["NUMBER_OF_SERIALS"]) * numberOfCopies }
                    });
                }

                var result = worker.Execute<DataTable>();
                DataTable serials = result.GetResultSet();

                List<string> serialList = new List<string>();
                foreach (DataRow each in serials.Rows)
                {
                    serialList.Add(each["SERIAL"].ToString());
                }

                string lastSerialNumbers = "";
                if (serialList.Count() > 0)
                {
                    if (labelInfo.LabelID == "001" || labelInfo.LabelID == "002"
                        || labelInfo.LabelID == "026" || labelInfo.LabelID == "027" || labelInfo.LabelID == "028" || labelInfo.LabelID == "029")
                    {
                        lastSerialNumbers = string.Format("'{0}'", serialList[serialList.Count - 1]);
                    }
                    else
                    {
                        for (int i = serialList.Count - numberOfCopies; i < serialList.Count; i++)
                        {
                            lastSerialNumbers += string.Format("'{0}',", serialList[i]);
                        }
                        lastSerialNumbers = lastSerialNumbers.TrimEnd(',');
                    }
                }
                else
                {
                    lastSerialNumbers = "''";
                }

                // p_serials 파라미터 다시주고 재쿼리
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                param.Add("PLANTID", UserInfo.Current.Plant);
                param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                param.Add("BOXNO", labelInfo.BoxNo);
                param.Add("CUSTOMERID", cboCustomerId.Editor.GetDisplayText());
                if (serialList.Count > 0)
                {
                    param.Add("P_SERIALS", "'" + string.Join("','", serialList) + "'");
                    param.Add("P_LASTSERIAL", lastSerialNumbers);
                }
                if (labelInfo.LabelID == "001" || labelInfo.LabelID == "002"
                    || labelInfo.LabelID == "026" || labelInfo.LabelID == "027" || labelInfo.LabelID == "028" || labelInfo.LabelID == "029")
                {
                    param.Add("NUMBEROFCOPIES", numberOfCopies);
                }
                dt = SqlExecuter.Query(labelInfo.QueryId, labelInfo.QueryVersion, param);

                // 일련번호 자동 채번하는 경우(라벨이 SDC 라벨이고 자동체크=N or 자동체크=Y) 마지막 번호를 화면에 표시
                if (scbAutoLot1.Checked)
                {
                    if (serialList.Count > 0)
                    {
                        string lastSerial = serialList[serialList.Count - 1];
                        SetProperty("BOXLOTNO1", lastSerial.Substring(8));
                    }
                }
            }
            else // 포장 일련번호를 채번하지 않는 경우
            {
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                param.Add("PLANTID", UserInfo.Current.Plant);
                param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                param.Add("BOXNO", labelInfo.BoxNo);
                param.Add("NUMBEROFCOPIES", numberOfCopies);
                param.Add("CUSTOMERID", cboCustomerId.Editor.GetDisplayText());
                dt = SqlExecuter.Query(labelInfo.QueryId, labelInfo.QueryVersion, param);
            }

            foreach (DataRow row in dt.Rows)
            {
                properties.Add(new Dictionary<string, object>());
                foreach (DataColumn col in dt.Columns)
                {
                    if (!properties[properties.Count - 1].ContainsKey(col.ColumnName))
                    {
                        properties[properties.Count - 1].Add(col.ColumnName, row[col.ColumnName]);
                    }
                }
            }
        }
        #endregion

        #region - ClearLabelValue() |
        private void ClearLabelValue()
        {
            foreach (Control ctr in this.bindingControls)
            {
                if (ctr is SmartTextBox || ctr is SmartLabelTextBox)
                    ctr.Text = string.Empty;
            }
        }
        #endregion

        #region - 텍스트 필드 이미지 변환 :: GetFloat(string f, float defaultValue) |
        private float GetFloat(string f, float defaultValue)
        {
            float value;
            bool parsed = float.TryParse(f, out value);
            if (parsed)
            {
                return value;
            }
            else
            {
                return defaultValue;
            }
        }
        #endregion

        #region - SetBindingData(DataTable dt) |
        private void SetBindingData(DataTable dt)
        {
            if (this.bindingControls == null || this.bindingControls.Count < 0)
                GetAllControls(this.panel2, this.bindingControls);

            currentBoxDataSource = dt;

            if (dt == null || dt.Rows.Count <= 0)
                return;

            foreach (Control ctr in this.bindingControls)
            {
                string column = ctr.Tag.ToString();

                if (dt.Columns.Contains(column))
                {
                    //  컨트롤 별 데이터를 가져오는 속성이 다르면 컨트롤별 Value 가져오는 로직 추가 필요.
                    if (ctr is SmartTextBox || ctr is SmartLabelTextBox)
                    {
                        ctr.Text = dt.Rows[0][column].ToString();
                    }
                    else if (ctr is SmartLabelDateEdit)
                    {
                        ctr.Text = dt.Rows[0][column].ToString();
                    }
                    else if (ctr is SmartCheckBox)
                    {
                        if (column != "ISYAPPLY")
                        {
                            (ctr as SmartCheckBox).Checked = dt.Rows[0][column].ToString() == "Y";
                        }
                        else
                        {
                            // Y 미적용 여부 라벨에 따라 자동체크
                            if (this.labelInfoList == null || this.labelInfoList.Count == 0)
                            {
                                (ctr as SmartCheckBox).Checked = dt.Rows[0][column].ToString() == "Y";
                            }
                            else
                            {
                                if (dt.Rows[0][column].ToString() == "")
                                {
                                    string[] labelIdList = this.slcbPrintBoxLabel.EditValue.ToString().Split(',');

                                    scbYDoesNotApply.Checked = false;
                                    foreach (LabelInfo eachLabel in this.labelInfoList)
                                    {
                                        if (eachLabel.LabelID == labelIdList[0])
                                        {
                                            scbYDoesNotApply.Checked = eachLabel.IsYApply;
                                        }
                                    }
                                }
                                else
                                {
                                    (ctr as SmartCheckBox).Checked = dt.Rows[0][column].ToString() == "Y";
                                }
                            }
                        }
                    }
                    else if (ctr is SmartCheckEdit)
                    {
                        (ctr as SmartCheckBox).Checked = dt.Rows[0][column].ToString() == "Y";
                    }
                    else
                    {
                        ctr.Text = dt.Rows[0][column].ToString();
                    }
                }
            }

            UpdateALotNo(dt);
            UpdateQty();
        }
        #endregion

        #region - UpdateALotNo(DataTable dt) |
        private void UpdateALotNo(DataTable dt)
        {
            string strLotNo = string.Empty;

            if (dt.Rows[0]["LOTNOA"] == null || string.IsNullOrWhiteSpace(dt.Rows[0]["LOTNOA"].ToString()))
                strLotNo = dt.Rows[0]["BOXLOTID"].ToString();
            else
                strLotNo = dt.Rows[0]["LOTNOA"].ToString();

            // A사 LOT NO
            if (scbYDoesNotApply.Checked)    // Y 미적용
            {
                // LOTNO + RIGHT(LOTID, 1)
                SetProperty("LOTNOA", strLotNo);
            }
            else // Y 적용
            {
                string lotNoA = string.Empty;
                if (!lotNoA.StartsWith("Y"))
                {
                    lotNoA = "Y" + strLotNo;
                }
                SetProperty("LOTNOA", lotNoA);
            }

            UpdateLotNoASuffix();
        }
        #endregion

        private void UpdateLotNoASuffix()
        {
            if (slcbPrintBoxLabel.Editor.EditValue == null || string.IsNullOrWhiteSpace(slcbPrintBoxLabel.Editor.EditValue.ToString()))
                return;

            // LGD 라벨 : A 사 LOT NO 뒤에 000 붙여 9자리로 만듦
            if (slcbPrintBoxLabel.Editor.EditValue.ToString().Equals("024") || slcbPrintBoxLabel.Editor.EditValue.ToString().Equals("025")
                || slcbPrintBoxLabel.Editor.EditValue.ToString().Equals("060"))
            {
                if (GetProperty("LOTNOA").Length < 9)
                {
                    SetProperty("LOTNOA", GetProperty("LOTNOA") + new string('0', 9 - GetProperty("LOTNOA").Length));
                }
            }

            // ZDT, Tianma, BOE, FY, Primax, IAC, FoxConn, FIT : A 사 LOT NO 뒤에 0 붙여 7자리로 만듦
            if (slcbPrintBoxLabel.Editor.EditValue.ToString().Equals("001") || slcbPrintBoxLabel.Editor.EditValue.ToString().Equals("002")
                 || slcbPrintBoxLabel.Editor.EditValue.ToString().Equals("006") || slcbPrintBoxLabel.Editor.EditValue.ToString().Equals("007")
                 || slcbPrintBoxLabel.Editor.EditValue.ToString().Equals("030") || slcbPrintBoxLabel.Editor.EditValue.ToString().Equals("039")
                 || slcbPrintBoxLabel.Editor.EditValue.ToString().Equals("040") || slcbPrintBoxLabel.Editor.EditValue.ToString().Equals("041")
                 || slcbPrintBoxLabel.Editor.EditValue.ToString().Equals("042") || slcbPrintBoxLabel.Editor.EditValue.ToString().Equals("035")
                 || slcbPrintBoxLabel.Editor.EditValue.ToString().Equals("088") || slcbPrintBoxLabel.Editor.EditValue.ToString().Equals("010")
                 || slcbPrintBoxLabel.Editor.EditValue.ToString().Equals("011") || slcbPrintBoxLabel.Editor.EditValue.ToString().Equals("046")
                 || slcbPrintBoxLabel.Editor.EditValue.ToString().Equals("053") || slcbPrintBoxLabel.Editor.EditValue.ToString().Equals("054"))
            {
                if (scbYDoesNotApply.Checked && GetProperty("LOTNOA").Length < 7)
                {
                    SetProperty("LOTNOA", GetProperty("LOTNOA") + new string('0', 7 - GetProperty("LOTNOA").Length));
                }
                else if (!scbYDoesNotApply.Checked && GetProperty("LOTNOA").Length < 8)
                {
                    SetProperty("LOTNOA", GetProperty("LOTNOA") + new string('0', 8 - GetProperty("LOTNOA").Length));
                }
            }

            // GORETEK 라벨 A 사 LOT NO 8자리
            if (slcbPrintBoxLabel.Editor.EditValue.ToString().Equals("004") || slcbPrintBoxLabel.Editor.EditValue.ToString().Equals("005"))
            {
                if (scbYDoesNotApply.Checked && GetProperty("LOTNOA").Length < 8)
                {
                    SetProperty("LOTNOA", GetProperty("LOTNOA") + new string('0', 8 - GetProperty("LOTNOA").Length));
                }
                else if (!scbYDoesNotApply.Checked && GetProperty("LOTNOA").Length < 9)
                {
                    SetProperty("LOTNOA", GetProperty("LOTNOA") + new string('0', 9 - GetProperty("LOTNOA").Length));
                }
            }

            // ICT 라벨 A 사 LOT NO 8자리
            if (slcbPrintBoxLabel.Editor.EditValue.ToString().Equals("008") || slcbPrintBoxLabel.Editor.EditValue.ToString().Equals("009"))
            {
                if (scbYDoesNotApply.Checked && GetProperty("LOTNOA").Length < 8)
                {
                    SetProperty("LOTNOA", GetProperty("LOTNOA") + new string('0', 8 - GetProperty("LOTNOA").Length));
                }
                else if (!scbYDoesNotApply.Checked && GetProperty("LOTNOA").Length < 9)
                {
                    SetProperty("LOTNOA", GetProperty("LOTNOA") + new string('0', 9 - GetProperty("LOTNOA").Length));
                }
            }
        }

        #region - UpdateLotNo(DataTable dt, string type) |
        private void UpdateLotNo(DataTable dt, string type)
        {
            string strLotNo = string.Empty;

            if (dt.Rows[0]["LOTNO1"] == null || string.IsNullOrWhiteSpace(dt.Rows[0]["LOTNO1"].ToString()))
                strLotNo = dt.Rows[0]["PUBLISHDATE"].ToString().Replace("-", "");
            else
                strLotNo = dt.Rows[0]["LOTNO1"].ToString();

            SetProperty("LOTNO1", strLotNo);
            /*
            // AUTO LOT
            if (scbAutoLot1.Checked)
            {
                MessageWorker worker = new MessageWorker("GetNextPackingSerials");
                worker.SetBody(new MessageBody()
                {
                    { "idclassid", "SUL010" }
                    , { "numberofserials", string.IsNullOrWhiteSpace(smartTextBox1.Text) ? 1 : Format.GetInteger(smartTextBox1.Text) }
                });
                var result = worker.Execute<DataTable>();
                DataTable serials = result.GetResultSet();

                SetProperty("LOTNO1", strLotNo);
                SetProperty("BOXLOTNO1", serials.Rows[0]["SERIAL"].ToString().Substring(8, 4));
            }
            */
        }
        #endregion

        #region -  UpdateDefectAndXOut() |
        private void UpdateDefectAndXOut()
        {
            decimal block = SafeParseDecimal(GetProperty("BLOCK"));             // Tray or Box 당 Block 수
            decimal pcsBlock = SafeParseDecimal(GetProperty("PCSBLOCK"));       // Pcs / Block
            decimal qty = SafeParseDecimal(GetProperty("QTY"));

            if (block > 0)
            {
                decimal xOut = 0;           // Block 당 불량
                decimal defectQty = 0;      // Tray or Box 당 불량

                if (pcsBlock <= 0)
                {
                    return;
                }

                xOut = pcsBlock - (qty / block);
                defectQty = xOut * block;

                if (xOut < 0)
                {
                    return;
                }
                if ((xOut % 1) != 0)
                {
                    return;
                }

                SetProperty("XOUT", xOut.ToString("F0"));
                SetProperty("DEFECTQTY", defectQty.ToString("F0"));
            }
            else
            {
                SetProperty("XOUT", "0");
                SetProperty("DEFECTQTY", "0");
            }
        }
        #endregion

        #region -  ValidateDefectAndXOut() |
        private void ValidateDefectAndXOut()
        {
            decimal block = SafeParseDecimal(GetProperty("BLOCK"));             // Tray or Box 당 Block 수
            decimal pcsBlock = SafeParseDecimal(GetProperty("PCSBLOCK"));       // Pcs / Block
            decimal qty = SafeParseDecimal(GetProperty("QTY"));

            if (block > 0)
            {
                decimal xOut = 0;           // Block 당 불량
                decimal defectQty = 0;      // Tray or Box 당 불량

                if (pcsBlock <= 0)
                {
                    throw MessageException.Create("PCS/BLOCK 는 0이하일 수 없습니다.");
                }

                xOut = pcsBlock - (qty / block);
                defectQty = xOut * block;

                if (xOut < 0)
                {
                    throw MessageException.Create("XOut 은 0이상 이어야 합니다.");
                }
                if ((xOut % 1) != 0)
                {
                    throw MessageException.Create("XOut 은 정수값 이어야 합니다.");
                }

                SetProperty("XOUT", xOut.ToString("F0"));
                SetProperty("DEFECTQTY", defectQty.ToString("F0"));
            }
            else
            {
                string[] labelIdList = this.slcbPrintBoxLabel.EditValue.ToString().Split(',');
                if (labelIdList.Length > 0)
                {
                    foreach (LabelInfo eachLabel in this.labelInfoList)
                    {
                        if (eachLabel.LabelID == labelIdList[0])
                        {
                            if(eachLabel.BarcodeType == "Block")
                            {
                                throw MessageException.Create("Block 은 0보다 커야합니다.");
                            }
                        }
                    }
                }

                SetProperty("XOUT", "0");
                SetProperty("DEFECTQTY", "0");
            }
        }
        #endregion

        #region - SafeParseDecimal(string value) |
        private decimal SafeParseDecimal(string value)
        {
            decimal result;
            bool parsed = decimal.TryParse(value, out result);
            return parsed ? result : 0M;
        }
        #endregion

        #region - SafeParseInt(string value) |
        private int SafeParseInt(string value)
        {
            int result;
            bool parsed = int.TryParse(value, out result);
            return parsed ? result : 0;
        }
        #endregion

        #region - SetProperty(string tagName, string value) |
        private void SetProperty(string tagName, string value)
        {
            Control control = FindControl(tagName);
            if (control is SmartLabelTextBox)
            {
                (control as SmartLabelTextBox).EditValue = value;
            }
            else if (control is SmartTextBox)
            {
                (control as SmartTextBox).EditValue = value;
            }
        }
        #endregion

        #region - GetProperty(string tagName) |
        private string GetProperty(string tagName)
        {
            Control control = FindControl(tagName);
            if (control is SmartLabelTextBox)
            {
                return Format.GetString((control as SmartLabelTextBox).EditValue);
            }
            else if (control is SmartTextBox)
            {
                return Format.GetString((control as SmartTextBox).EditValue);
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region - FindControl(string tagName) |
        private Control FindControl(string tagName)
        {
            foreach (Control ctr in this.bindingControls)
            {
                string column = ctr.Tag.ToString().ToUpper();

                if (column == tagName.ToUpper())
                {
                    return ctr;
                }
            }
            return null;
        } 
        #endregion

        #region -  UpdateQty() |
        private void UpdateQty()
        {
            int pcsTray = Format.GetInteger(GetProperty("PCSTRAY"));    // Tray 당 Pcs 수
            int trayBox = Format.GetInteger(GetProperty("TRAYBOX"));    // Box 당 Tray 수
            int qty = Format.GetInteger(GetProperty("QTY")); //Format.GetInteger(GetProperty("CHECKEDQTY"));     // 포장수량(양품)
            int defectqty = Format.GetInteger(GetProperty("DEFECTQTY")); // 포장수량(불량)  Format.GetInteger(GetProperty("CHECKEDDEFECTQTY"));
            int totalQty = qty + defectqty;

            if (pcsTray == 0 || trayBox == 0)
            {
                return;
            }

            int trayQty;                                                // tray 수
            int trayRemainQty = totalQty % pcsTray;                          // tray pcs 잔량(마지막 tray에 들어간 pcs 수)

            if (trayRemainQty == 0)
            {
                trayQty = totalQty / pcsTray;
            }
            else
            {
                trayQty = (totalQty / pcsTray) + 1;
            }

            int boxQty;                                                 // box 수
            int boxRemainQty = totalQty % (pcsTray * trayBox);               // box pcs 잔량(마지막 box에 들어간 pcs 수)
            int trayBoxRemainQty = 0;                                   // box tray 잔량(마지막 box에 들어간 tray 수)

            if (boxRemainQty == 0)
            {
                boxQty = totalQty / (pcsTray * trayBox);
            }
            else
            {
                boxQty = totalQty / (pcsTray * trayBox) + 1;
                trayBoxRemainQty = trayQty % trayBox;
            }

            SetProperty("TRAYQTY", trayQty.ToString());
            SetProperty("BOXQTY", boxQty.ToString());
            SetProperty("TRAYREMAINQTY", trayRemainQty.ToString());
            SetProperty("TRAYBOXREMAINQTY", trayBoxRemainQty.ToString());
            SetProperty("PCSBOXREMAINQTY", boxRemainQty.ToString());
        }
        #endregion

        #region - Validation 체크 :: CheckValidation() |
        /// <summary>
        /// Validation 체크
        /// </summary>
        /// <returns></returns>
        private void CheckValidation()
        {
            // TSMT / LITE-ON 라벨인 경우
            if (slcbPrintBoxLabel.Editor.EditValue.ToString().Equals("063") || slcbPrintBoxLabel.Editor.EditValue.ToString().Equals("064")
                || slcbPrintBoxLabel.Editor.EditValue.ToString().Equals("068") || slcbPrintBoxLabel.Editor.EditValue.ToString().Equals("069"))
            {
                // Lot고정 체크 & Lot No입력이 없을 경우
                if (scbLotFix.Checked && string.IsNullOrWhiteSpace(txtLotNo4.Text.Trim()))
                {
                    txtLotNo4.Focus();
                    // LOT No.는 필수 입력 항목입니다.
                    throw MessageException.Create("LotIdIsRequired");
                }
            }

            if (rdoLabelType.EditValue != null && rdoLabelType.EditValue.ToString() == "Bundle" && (GetProperty("BUNDLEBOX") == "0" || GetProperty("BUNDLEBOX") == ""))
            {
                throw MessageException.Create("묶음/Box 수량이 입력되지 않았습니다. 포장사양을 확인해주세요.");
            }
        } 
        #endregion

        private void PrintLable(string[] labelIdList, DataRow dr)
        {
            this.labelInfoList = SmartMES.Commons.CommonFunction.GetLabelData(dr["BOXNO"].ToString()
                        , this.rdoLabelType.EditValue.ToString(), dr["PATHTYPE"].ToString().Equals("SHIPPING") ? "Y" : "N"
                        , cboCustomerId.Editor.GetDisplayText());

            foreach (LabelInfo each in labelInfoList)
            {
                if (labelIdList.Contains(each.LabelID))
                {
                    UsingPrintDBBarcodeScript(each);
                }
            }
        }
        #endregion
    }
}
