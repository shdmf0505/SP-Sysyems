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
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class PrintPackingLabelPopup2 : SmartPopupBaseForm, ISmartCustomPopup
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

        private readonly string IMG_FIELD_PREFIX = @"{!#";
        private readonly string IMG_FIELD_SUFFIX = @"}";
        private List<Control> bindingControls;
        private DataTable currentBoxDataSource;

        #endregion

        #region ◆ 생성자 |
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="viewList"></param>
        public PrintPackingLabelPopup2(string lotIds)
		{
			InitializeComponent();
            this.bindingControls = new List<Control>();

            if (!this.IsDesignMode())
			{
                InitializeContent();
                InitializeEvent();
			}

            if (!string.IsNullOrWhiteSpace(lotIds))
            {
                Search(lotIds, true);
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

            cboShipTo.Editor.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboShipTo.Editor.ValueMember = "CODEID";
            cboShipTo.Editor.DisplayMember = "CODENAME";
            cboShipTo.Editor.UseEmptyItem = true;

            cboShipTo.Editor.DataSource = SqlExecuter.Query("GetCodeList", "00001"
                    , new Dictionary<string, object>() { { "CODECLASSID", "LABELSHIPTARGET" }, { "LANGUAGETYPE", UserInfo.Current.LanguageType } });
            
            cboShipTo.Editor.ShowHeader = false;
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
            this.schLotID.Editor.KeyDown += SchLotID_KeyDown;
            this.grdPackingList.View.FocusedRowChanged += View_FocusedRowChanged;
            this.grdPackingList.View.CheckStateChanged += View_CheckStateChanged;
            this.rdoLabelType.EditValueChanged += (s, e) =>
            {
                UpdateLabelType();
            };

            this.btnSearch.Click += (s, e) =>
             {
                 Search(this.schLotID.Text);
             };

            this.btnLabelPrint.Click += BtnLabelPrint_Click;
            //this.btnPackPrint.Click += BtnPackPrint_Click;
            this.btnClose.Click += (s, e) =>
            {
                this.Close();
            };

            this.KeyDown += (s, e) =>
            {
                if (e.KeyCode != Keys.F5) return;
                Search(this.schLotID.Text);
            };

            this.btnSave.Click += (s, e) =>
            {
                Save();
            };

            (FindControl("BLOCK") as SmartLabelTextBox).Editor.Leave += Editor_LeaveForXOut;
            (FindControl("PCSARY") as SmartLabelTextBox).Editor.Leave += Editor_LeaveForXOut;

            (FindControl("QTY") as SmartLabelTextBox).Editor.Leave += Editor_LeaveForQty;
            (FindControl("PCSTRAY") as SmartTextBox).Leave += Editor_LeaveForQty;
            (FindControl("TRAYBOX") as SmartTextBox).Leave += Editor_LeaveForQty;

            scbYDoesNotApply.CheckedChanged += ScbYDoesNotApply_CheckedChanged;
        }

        private void View_CheckStateChanged(object sender, EventArgs e)
        {
            DataTable dt = grdPackingList.View.GetCheckedRows();
            decimal sum = 0;
            foreach(DataRow each in dt.Rows)
            {
                sum += (decimal)each["PCSQTY"];
            }
            SetProperty("CHECKEDQTY", sum.ToString());
            UpdateQty();

            if (dt.Rows.Count > 0)
            {
                LoadBoxData(dt.Rows[0]);
            }
        }

        private void Editor_LeaveForQty(object sender, EventArgs e)
        {
            //UpdateQty();
        }

        private void ScbYDoesNotApply_CheckedChanged(object sender, EventArgs e)
        {
            UpdateALotNo(this.currentBoxDataSource);
        }

        private void Editor_LeaveForXOut(object sender, EventArgs e)
        {
            UpdateDefectAndXOut();
        }

        private void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //LoadBoxData();
            UpdateLabelType();
        }

        private void LoadBoxData(DataRow dr)
        {
            if (dr == null) return;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("BOXNO", dr["BOXNO"].ToString());

            DataTable dt = SqlExecuter.Query("SelectPackingLabelInfo", "10002", param);

            if (dt == null || dt.Rows.Count == 0)
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                return;
            }

            SetBindingData(dt);
        }

        private void Save()
        {
            DataTable dt = grdPackingList.View.GetCheckedRows();
            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }

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
                else
                {
                    param.Add(each.Tag.ToString(), each.Text);
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

        private void UpdateLabelType()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("LABELDEFNAME");
            dt.Columns.Add("LABELDEFID");

            this.slcbPrintBoxLabel.Editor.DataSource = dt;

            if (this.grdPackingList.View.FocusedRowHandle < 0)
                return;

            if (this.rdoLabelType.EditValue == null)
                return;

            DataRow selectedRow = this.grdPackingList.View.GetFocusedDataRow();
            List<LabelInfo> labelInfoList = SmartMES.Commons.CommonFunction.GetLabelData(selectedRow["BOXNO"].ToString()
                , rdoLabelType.EditValue.ToString(), selectedRow["PATHTYPE"].ToString().Equals("SHIPPING") ? "Y" : "N", "");

            foreach(LabelInfo each in labelInfoList)
            {
                DataRow newRow = dt.NewRow();
                newRow["LABELDEFNAME"] = each.LabelName;
                newRow["LABELDEFID"] = each.LabelID;
                dt.Rows.Add(newRow);
            }

            this.slcbPrintBoxLabel.Editor.DataSource = dt;
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
          
        }

        private void Search(string lotId, bool isInitialize = false)
        {
            this.schLotID.Text = lotId;
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LOTID", lotId);

            DataTable dt = SqlExecuter.Query("SelectPackingAndPackageWipList", "10002", param);
            grdPackingList.DataSource = dt;
            if (dt == null || dt.Rows.Count == 0)
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                return;
            }
        }
        // TODO : 이동
        private void BtnPackPrint_Click(object sender, EventArgs e)
        {
            if (this.grdPackingList.View.FocusedRowHandle < 0)
                return;

            DataRow selectedRow = this.grdPackingList.View.GetFocusedDataRow();
            LabelInfo labelInfo = SmartMES.Commons.CommonFunction.GetLabelData(selectedRow["BOXNO"].ToString()
                , "Tray", selectedRow["PATHTYPE"].ToString().Equals("SHIPPING") ? "Y" : "N", "").FirstOrDefault();
            // UsingPrintXmlBarcodeScript(labelInfo);
            UsingPrintDBBarcodeScript(labelInfo);
        }

        private void BtnLabelPrint_Click(object sender, EventArgs e)
        {
            DataTable checkedRows = this.grdPackingList.View.GetCheckedRows();
            if (checkedRows.Rows.Count == 0)
                return;

            if (this.slcbPrintBoxLabel.EditValue == null)
                return;

            Save();
            string[] labelIdList = this.slcbPrintBoxLabel.EditValue.ToString().Split(',');

            foreach (DataRow eachRow in checkedRows.Rows)
            {
                List<LabelInfo> labelInfoList = SmartMES.Commons.CommonFunction.GetLabelData(eachRow["BOXNO"].ToString()
                    , this.rdoLabelType.EditValue.ToString(), eachRow["PATHTYPE"].ToString().Equals("SHIPPING") ? "Y" : "N", "");

                foreach (LabelInfo each in labelInfoList)
                {
                    if (labelIdList.Contains(each.LabelID) || each.LabelID == "PackingExport")
                    {
                        UsingPrintDBBarcodeScript(each);
                    }
                }
            }
        }

        private void UsingPrintDBBarcodeScript(LabelInfo labelInfo)
        {
            SetBindingLabelValue(labelInfo);
        }

        private void UsingPrintXmlBarcodeScript(LabelInfo labelInfo)
        {
            Assembly assembly = Assembly.GetAssembly(this.GetType());
            Stream stream = assembly.GetManifestResourceStream("Micube.SmartMES.ProcessManagement.Report.LabelScript.xml");

            labelInfo.XmlBarcodeScript = new XmlDocument();
            labelInfo.XmlBarcodeScript.Load(stream);
            XmlNode node = labelInfo.XmlBarcodeScript.SelectSingleNode("//ZPL[@name='" + labelInfo.LabelID + "']");
            labelInfo.ZplBarcodeScript = node.InnerText;

            SetBindingLabelValue(labelInfo);
        }

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

        private void SetBindingLabelValue(LabelInfo labelInfo)
        {
            if (labelInfo.ZplBarcodeScript != null)
            {
                // NOTE : BY 세번째 파라미터(바코드 높이)가 설정된 상태로 2D 바코드를 인쇄하면 해당 수치만큼 바코드가 아래로 밀리는 현상이 있어
                //        매 라벨 인쇄시 마다 높이를 1로 설정
                string text = "^XA^BY2,1,1^XZ" + labelInfo.ZplBarcodeScript;

                /*
                if (this.bindingControls == null || this.bindingControls.Count <= 0)
                    GetAllControls(this.panel2, this.bindingControls);
                */

                Dictionary<string, object> properties = new Dictionary<string, object>();

                /*
                foreach (Control ctr in this.bindingControls)
                {

                    string id = ctr.Tag.ToString();
                    string value = ctr.Text;

                    /*  컨트롤 별 데이터를 가져오는 속성이 다르면 컨트롤별 Value 가져오는 로직 추가 필요.
                    //if (ctr is SmartTextBox || ctr is SmartLabelTextBox)
                    //    value = ctr.Text;
                    //else if (ctr is SmartLabelDateEdit)
                    //    value = ctr.Text;
                    if (!properties.ContainsKey(id))
                        properties.Add(id, value);

                }
                //라벨별로 화면에는 안보여주지만 데이터테이블에 있는데 데이터를 출력해할때 프로퍼트에 추가해준다.

                Dictionary<string, string> dicQty = new Dictionary<string, string>();
                if(labelInfo.LabelType.Equals("Tray"))
                {
                    double qty = Format.GetDouble(properties["QTY"],0);
                    double pcsTray = Format.GetDouble(properties["PCSTRAY"],0);

                    double value = qty / pcsTray;
                    double quotient = System.Math.Truncate(value);
                    double remain = qty % pcsTray;

                    for (int i = 0; i < value; i++)
                    {
                        if (i == quotient)
                        {
                            dicQty.Add((i+1).ToString().PadLeft(4, '0'), remain.ToString());
                        }
                        else
                        {
                            dicQty.Add((i + 1).ToString().PadLeft(4, '0'), pcsTray.ToString());
                        }
                    }
                }
                else
                {
                    dicQty.Add("001", Format.GetTrimString(properties["QTY"]));
                }

                foreach(string qtyKey in dicQty.Keys)
                {
                    text = labelInfo.ZplBarcodeScript;

                    AddLabelProperty(labelInfo, properties, qtyKey,dicQty[qtyKey]);
                    AddLabelPropertyDefault(labelInfo, properties);

                    foreach (var property in properties)
                    {
                        string findProperty = string.Format("{{!@{0}}}", property.Key);

                        try
                        {
                            if (!string.IsNullOrWhiteSpace(property.Value.ToString()))
                                text = text.Replace(findProperty, property.Value.ToString());
                            else
                                text = text.Replace(findProperty, "");
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    text = InsertImages(text, properties);
                    Commons.CommonFunction.PrintLabel(text);
                }
                */

                text = labelInfo.ZplBarcodeScript;

                //AddLabelProperty(labelInfo, properties, qtyKey, dicQty[qtyKey]);
                AddLabelPropertyDefault(labelInfo, properties);

                foreach (var property in properties)
                {
                    string findProperty = string.Format("{{!@{0}}}", property.Key);

                    try
                    {
                        if (!string.IsNullOrWhiteSpace(property.Value.ToString()))
                            text = text.Replace(findProperty, property.Value.ToString());
                        else
                            text = text.Replace(findProperty, "");
                    }
                    catch (Exception ex)
                    {

                    }
                }


                // 인쇄 테스트
                //bool result = axRdviewer501.FileOpen("http://172.16.4.58/ype_rd/prod/PKA10_BARCODE_01.mrd", "/rf [http://RD_REPORT.YPFPC.CO.KR:8080/RDServer/rdagent.jsp] /rp [I2013020607251] [I201302060725100001] [ZSI100108B10] [C.C.L (E 1820 D500NW)-C.C.L (E 1820 D500NW)-C.C.L (E 1820 D500NW)] [EA] [495*335 0.7T PS-495*335 0.7T PS-495*335 0.7T PS] [100] [10] /rwait");
                bool result = axRdviewer501.FileOpen("http://172.16.4.58/ype_rd/prod/PKA10_BARCODE_01.mrd", "/rf [http://121.139.148.132:8080/RDServer/rdagent.jsp] /rp [I2013020607251] [I201302060725100001] [ZSI100108B10] [C.C.L (E 1820 D500NW)-C.C.L (E 1820 D500NW)-C.C.L (E 1820 D500NW)] [EA] [495*335 0.7T PS-495*335 0.7T PS-495*335 0.7T PS] [100] [10] /rwait");
                MessageBox.Show("Test" + result.ToString());
                axRdviewer501.SetPrintInfo(UserInfo.Current.Printer, 1, 1, 4);
                axRdviewer501.CMPrint();

                //axRdviewer501.SetPrintInfo("BARCODE", 1, 1, 4);
                //axRdviewer501.CMPrint();
                //axRdviewer501.ShowPrintDlg(1);

                /*
                AxRDVIEWER50Lib.AxRdviewer50 m_var = new AxRDVIEWER50Lib.AxRdviewer50();
                m_var.CreateControl();
                //m_var.ApplyLicense("http://rd_report.ypfpc.co.kr:8080/RDServer/rdagent.jsp");
                bool result = m_var.FileOpen("http://172.16.4.58/ype_rd/prod/PKA10_BARCODE_01.mrd", "/rf [http://RD_REPORT.YPFPC.CO.KR:8080/RDServer/rdagent.jsp] /rp [I2013020607251] [I201302060725100001] [ZSI100108B10] [C.C.L (E 1820 D500NW)-C.C.L (E 1820 D500NW)-C.C.L (E 1820 D500NW)] [EA] [495*335 0.7T PS-495*335 0.7T PS-495*335 0.7T PS] [100] [10] /rwait");

                //m_var.SetPrintInfo(UserInfo.Current.Printer, 1, 1, 4);
                m_var.SetPrintInfo("BARCODE", 1, 1, 4);
                m_var.ViewPDF();
                m_var.ShowPrintDlg(0);
                m_var.CMPrint();
                */

                //text = InsertImages(text, properties);
                //Commons.CommonFunction.PrintLabel(text);
            }
            else
            {
                MessageBox.Show(string.Format("등록된 스크립트가 없습니다 Label ID : {0}", labelInfo.LabelName));
            }
        }

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

        private void AddLabelPropertyDefault(LabelInfo labelInfo, Dictionary<string, object> properties)
        {
            DataTable dt = labelInfo.LabelDataTable;
            if (dt.Rows.Count == 0)
            {
                return;
            }
            DataRow row = dt.Rows[0];
            foreach(DataColumn col in dt.Columns)
            {
                if(!properties.ContainsKey(col.ColumnName))
                {
                    properties.Add(col.ColumnName, row[col.ColumnName]);
                }
            }
        }

        private void ClearLabelValue()
        {
            foreach (Control ctr in this.bindingControls)
            {
                if (ctr is SmartTextBox || ctr is SmartLabelTextBox)
                    ctr.Text = string.Empty;
            }
        }

        private void SchLotID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            Search(this.schLotID.Text);
        }

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

        #region 텍스트 필드 이미지 변환
        /// <summary>
        /// 아래 형식으로 되어있는 필드를 이미지로 변환하여 출력한다.
        /// {!#필드명:폰트명:폰트크기:[스타일]}
        /// 예 : {!#PARTNO:굴림체:36.4:B}
        /// 스타일 : Bold/Italic/Strikeout/Underline/Regular (생략시 기본값 Regular)
        /// </summary>
        /// <param name="zplCommand"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        private string InsertImages(string zplCommand, Dictionary<string, object> properties)
        {
            StringBuilder result = new StringBuilder();
            const string FILE_PREFIX = "IMG";
            int fileSeq = 0;
            result.Append("^XA^IDR:*.GRF^FS^XZ");   // 기존 이미지 삭제
            List<string> fields = FindImageFields(zplCommand);
            foreach (string field in fields)
            {
                string[] values = field.Replace(IMG_FIELD_PREFIX, "").Replace(IMG_FIELD_SUFFIX, "").Split(':');
                string fieldName = values[0];
                string fontFamily = values[1];
                string fontSize = values[2];
                string style = (values.Length >= 4) ? values[3] : "R";

                using (Font font = CreateFont(fontFamily, GetFloat(fontSize, 40), style))
                {
                    if (properties.ContainsKey(fieldName))
                    {
                        string fileName = FILE_PREFIX + fileSeq++;
                        //if (string.IsNullOrWhiteSpace(properties[fieldName].ToString())) continue;

                        string imageCode = ZPLUtil.CreateZPLImageCodeFromText(properties[fieldName].ToString(), fileName, font);
                        result.Append(imageCode);
                        zplCommand = zplCommand.Replace(field, string.Format("^XGR:{0}.GRF,1,1^FS", fileName));
                    }
                }
            }
            result.Append(zplCommand);
            return result.ToString();
        }

        private Font CreateFont(string fontFamily, float fontSize, string style)
        {
            FontStyle fontStyle = FontStyle.Regular;
            switch (style)
            {
                case "B":
                    fontStyle = FontStyle.Bold;
                    break;
                case "I":
                    fontStyle = FontStyle.Italic;
                    break;
                case "S":
                    fontStyle = FontStyle.Strikeout;
                    break;
                case "U":
                    fontStyle = FontStyle.Underline;
                    break;
                default:
                    fontStyle = FontStyle.Regular;
                    break;
            }
            return new Font(fontFamily, fontSize, fontStyle);
        }

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

        private List<string> FindImageFields(string zplCommand)
        {
            List<string> result = new List<string>();

            int first = 0;
            int last = -1;

            while (true)
            {
                first = zplCommand.IndexOf(IMG_FIELD_PREFIX, first);
                if (first < 0)
                {
                    break;
                }
                last = zplCommand.IndexOf(IMG_FIELD_SUFFIX, first + IMG_FIELD_PREFIX.Length);
                if (last < 0)
                {
                    throw new SystemException("라벨형식오류");
                }
                string field = zplCommand.Substring(first, last - first + IMG_FIELD_SUFFIX.Length);
                result.Add(field);
                first = last + IMG_FIELD_SUFFIX.Length;
            }
            return result;
        }

        private void SetBindingData(DataTable dt)
        {
            if (this.bindingControls == null || this.bindingControls.Count < 0)
                GetAllControls(this.panel2, this.bindingControls);

            currentBoxDataSource = dt;

            if (dt == null || dt.Rows.Count < 0)
                return;

            foreach(Control ctr in this.bindingControls)
            {
                string column = ctr.Tag.ToString();

                if (dt.Columns.Contains(column))
                {

                    ctr.Text = dt.Rows[0][column].ToString();

                    /*  컨트롤 별 데이터를 가져오는 속성이 다르면 컨트롤별 Value 가져오는 로직 추가 필요.
                    if (ctr is SmartTextBox || ctr is SmartLabelTextBox)
                         ctr.Text = dt.Rows[0][column].ToString();
                    else if (ctr is SmartLabelDateEdit)
                         ctr.Text = dt.Rows[0][column].ToString();
                  */
                }
            }

            UpdateALotNo(dt);
            UpdateQty();
        }

        private void UpdateALotNo(DataTable dt)
        {
            // A사 LOT NO
            if(scbYDoesNotApply.Checked)    // Y 미적용
            {
                // LOTNO + RIGHT(LOTID, 1)
                SetProperty("LOTNOA", dt.Rows[0]["LOTNO"].ToString() + dt.Rows[0]["LOTID"].ToString().Substring(dt.Rows[0]["LOTID"].ToString().Length - 1));
            }
            else // Y 적용
            {
                // "Y" + LOTNO + RIGHT(LOTID, 1)
                string lotNoA = dt.Rows[0]["LOTNO"].ToString() + dt.Rows[0]["LOTID"].ToString().Substring(dt.Rows[0]["LOTID"].ToString().Length - 1);
                if (!lotNoA.StartsWith("Y"))
                {
                    lotNoA = "Y" + lotNoA;
                }
                SetProperty("LOTNOA", lotNoA);
            }
        }

        private void UpdateDefectAndXOut()
        {
            decimal block = SafeParseDecimal(GetProperty("BLOCK"));
            decimal pcsAry = SafeParseDecimal(GetProperty("PCSARY"));
            decimal pcsPnl = SafeParseDecimal(GetProperty("PCSPNL"));

            decimal xOut = 0;
            decimal defectQty = 0;

            if (pcsAry <= 0)
            {
                throw MessageException.Create("PCS/ARRAY 는 0이하일 수 없습니다.");
            }

            xOut = (pcsPnl / pcsAry) * block;
            defectQty = xOut * block;

            if (xOut < 0)
            {
                throw MessageException.Create("XOut 은 0이상 이어야 합니다.");
            }
            if ((xOut % 1) != 0)
            {
                throw MessageException.Create("XOut 은 정수값 이어야 합니다..");
            }

            SetProperty("XOUT", xOut.ToString("F0"));
            SetProperty("DEFECTQTY", defectQty.ToString("F0"));
        }

        private decimal SafeParseDecimal(string value)
        {
            decimal result;
            bool parsed = decimal.TryParse(value, out result);
            return parsed ? result : 0M;
        }

        private int SafeParseInt(string value)
        {
            int result;
            bool parsed = int.TryParse(value, out result);
            return parsed ? result : 0;
        }

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

        private void UpdateQty()
        {
            int pcsTray = Format.GetInteger(GetProperty("PCSTRAY"));    // Tray 당 Pcs 수
            int trayBox = Format.GetInteger(GetProperty("TRAYBOX"));    // Box 당 Tray 수
            int qty = Format.GetInteger(GetProperty("CHECKEDQTY"));            // 포장수량

            if (pcsTray == 0 || trayBox == 0)
            {
                return;
            }

            int trayQty;                                                // tray 수
            int trayRemainQty = qty % pcsTray;                          // tray pcs 잔량(마지막 tray에 들어간 pcs 수)

            if (trayRemainQty == 0)
            {
                trayQty = qty / pcsTray;
            }
            else
            {
                trayQty = (qty / pcsTray) + 1;
            }

            int boxQty;                                                 // box 수
            int boxRemainQty = qty % (pcsTray * trayBox);               // box pcs 잔량(마지막 box에 들어간 pcs 수)
            int trayBoxRemainQty = 0;                                   // box tray 잔량(마지막 box에 들어간 tray 수)

            if (boxRemainQty == 0)
            {
                boxQty = qty / (pcsTray * trayBox);
            }
            else
            {
                boxQty = qty / (pcsTray * trayBox) + 1;
                trayBoxRemainQty = trayQty % trayBox;
            }

            SetProperty("TRAYQTY", trayQty.ToString());
            SetProperty("BOXQTY", boxQty.ToString());
            SetProperty("TRAYREMAINQTY", trayRemainQty.ToString());
            SetProperty("TRAYBOXREMAINQTY", trayBoxRemainQty.ToString());
            SetProperty("PCSBOXREMAINQTY", boxRemainQty.ToString());
        }
    }
    #endregion

    #region ◆ Function |

    #endregion

}
