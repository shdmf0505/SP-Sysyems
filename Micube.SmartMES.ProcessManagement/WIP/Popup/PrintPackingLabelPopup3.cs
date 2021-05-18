#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.SmartMES.Commons;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons.Controls;

using DevExpress.XtraVerticalGrid.Rows;
using DevExpress.XtraReports.UI;
using DevExpress.XtraTab;

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Xml;

#endregion

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 공정관리 > 공정작업 > 입고검사등록
    /// 업  무  설  명  : 사진등록 팝업
    /// 생    성    자  : 정승원
    /// 생    성    일  : 2019-06-20
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class PrintPackingLabelPopup3 : SmartPopupBaseForm, ISmartCustomPopup
    {
        public DataRow CurrentDataRow { get; set; }
        private List<LabelInfo> viewList;

        private readonly string IMG_FIELD_PREFIX = @"{!#";
        private readonly string IMG_FIELD_SUFFIX = @"}";

        #region 생성자
        public PrintPackingLabelPopup3(List<LabelInfo> viewList)
        {
            InitializeComponent();

            this.viewList = viewList;

            if (!this.IsDesignMode())
            {
                InitializeEvent();
            }
        }
        #endregion

        #region Event
        private void InitializeEvent()
        {
            this.Load += PrintPackingLabelPopup_Load;
            this.btnPrint.Click += BtnPrint_Click;
            this.btnCancel.Click += BtnCancel_Click;
        }

        private void PrintPackingLabelPopup_Load(object sender, EventArgs e)
        {

            UsingLoadXmlBarcodeScript();
            //UsingLoadDBBarcodeScript();

            var itemList = this.viewList.Select(s => s.LabelName).ToList().Distinct();

            DataTable dt = new DataTable();
            dt.Columns.Add("LABELDEFNAME");

            foreach (string label in itemList)
            {
                dt.Rows.Add(new object[] { label });
            }

            this.slcbPrintBoxLabel.Editor.DisplayMember = "LABELDEFNAME";
            this.slcbPrintBoxLabel.Editor.ValueMember = "LABELDEFNAME";
            this.slcbPrintBoxLabel.Editor.DataSource = dt;

            if (itemList.Count() == 1)
                this.slcbPrintBoxLabel.EditValue = itemList.ElementAt(0);

        }

        private void UsingLoadDBBarcodeScript()
        {

            foreach (LabelInfo labelInfo in viewList)
            {
                XtraTabPage tab = new XtraTabPage();
                tab.Text = labelInfo.LabelName;

                ucLabelViewer2 labelViewer = new ucLabelViewer2();
                labelViewer.OnPropertyGridDataChange += LabelViewer_OnPropertyGridDataChange;
                tab.Controls.Add(labelViewer);
                labelViewer.LabelInfo = labelInfo;
                labelViewer.Dock = DockStyle.Fill;
                labelViewer.SetBindingPropertyGrid(labelInfo.LabelDataTable);
                this.xtraTabControl1.TabPages.Add(tab);


                SetVisibleColumns(labelViewer, labelInfo.XmlBarcodeScript);
            }
        }
        private void UsingLoadXmlBarcodeScript()
        {
            Assembly assembly = Assembly.GetAssembly(this.GetType());
            Stream stream = assembly.GetManifestResourceStream("Micube.SmartMES.ProcessManagement.Report.LabelScript.xml");

            XmlDocument document = new XmlDocument();
            document.Load(stream);

            foreach (LabelInfo labelInfo in viewList)
            {
                XtraTabPage tab = new XtraTabPage();
                tab.Text = labelInfo.LabelName;

                ucLabelViewer2 labelViewer = new ucLabelViewer2();
                labelViewer.OnPropertyGridDataChange += LabelViewer_OnPropertyGridDataChange;
                tab.Controls.Add(labelViewer);
                labelViewer.LabelInfo = labelInfo;
                labelViewer.Dock = DockStyle.Fill;
                labelViewer.SetBindingPropertyGrid(labelInfo.LabelDataTable);
                this.xtraTabControl1.TabPages.Add(tab);

                
                SetVisibleColumns(labelViewer, document);


            }

        }

    private void SetVisibleColumns(ucLabelViewer2 labelViewer, XmlDocument document)
    {
        if (document != null)
        {
            XmlNode node = document.SelectSingleNode("//ZPL[@name='" + labelViewer.LabelInfo.LabelID + "']");
            //XmlNode node = document.SelectSingleNode("//ZPL[@name='" + "001" + "']");
            //XmlNode node = document.SelectSingleNode("//ZPL[@name='" + "004" + "']");

            if (node != null && node.Attributes["hiddenColums"] != null)
            {
                List<string> visibleColumnns = node.Attributes["hiddenColums"].Value.Split(',').Select(s => string.Format("row{0}", s)).ToList();
                List<BaseRow> rows = labelViewer.GetPropertyGridData;
                foreach (BaseRow row in rows)
                {
                    if (visibleColumnns.Contains(row.Name))
                        row.Visible = false;
                }
            }
        }
    }

    private void LabelViewer_OnPropertyGridDataChange(ucLabelViewer2 viewer, string fieldName, string value)
        {
            // GIS
            if (viewer.LabelInfo.LabelID == "015" || viewer.LabelInfo.LabelID == "016" || viewer.LabelInfo.LabelID == "017")
            {
                string productdefid = string.Empty;
                string productdefversion = string.Empty;
                string boxweek = string.Empty;
                string uom = Format.GetString(viewer.GetCellValue("UOM"));

                if (fieldName.Equals("rowNOWDATE") || fieldName.Equals("rowBOXLOTID") || fieldName.Equals("rowQTY"))
                {
                    //*113700YP0970371080*
                    //*113700YP097037999*
                    string mainBarcode = viewer.GetCellValue("BARCODE");
                    string nowDate = viewer.GetCellValue("NOWDATE");
                    string boxLotid = viewer.GetCellValue("BOXLOTID");
                    string qty = viewer.GetCellValue("QTY");

                    viewer.SetCellValue("BARCODE", string.Format("{0}{1}{2}", nowDate, boxLotid, qty));
                }
                if (fieldName.Equals("rowBLOCK") || fieldName.Equals("rowPCSARY"))
                {
                    int block = Format.GetInteger(viewer.GetCellValue("BLOCK"));
                    string tpTemp = Format.GetString(viewer.GetCellValue("DEFECTQTY")).TrimEnd(')').TrimStart('(');
                    int defectqty = Format.GetInteger(tpTemp);
                    int pcsary = Format.GetInteger(viewer.GetCellValue("PCSARY"));
                    int qty = Format.GetInteger(viewer.GetCellValue("QTY"));
                    int setvalue = (block * pcsary) - qty;
                    uom = Format.GetString(viewer.GetCellValue("UOM"));

                    string tmpDefectQty = string.Empty;
                    string setDefect = string.Empty;
                    if (uom.Equals("BLK"))
                    {
                        tmpDefectQty = "(" + Format.GetString(setvalue) + ")";
                        setDefect = Format.GetString(setvalue);
                    }
                    else
                    {
                        if (setvalue.Equals(0))
                        {
                            tmpDefectQty = string.Empty;
                            setDefect = string.Empty;
                        }
                        tmpDefectQty = "(" + Format.GetString(setvalue) + ")";
                        setDefect = Format.GetString(setvalue);

                    }
                    viewer.SetCellValue("DEFECTQTY2", tmpDefectQty);
                    viewer.SetCellValue("DEFECTQTY", setDefect);
                    productdefid = Format.GetString(viewer.GetCellValue("PRODUCTDEFID"));
                    productdefversion = Format.GetString(viewer.GetCellValue("PRODUCTDEFVERSION"));
                    boxweek = Format.GetString(viewer.GetCellValue("BOXWEEK"));


                    viewer.SetCellValue("PRODUCTWEEK", productdefid + "/" + boxweek + "/" + setDefect);

                }
                if (fieldName.Equals("rowDEFECTQTY") || fieldName.Equals("rowBOXWEEK"))
                {
                    productdefid = Format.GetString(viewer.GetCellValue("PRODUCTDEFID"));
                    productdefversion = Format.GetString(viewer.GetCellValue("PRODUCTDEFVERSION"));
                    boxweek = Format.GetString(viewer.GetCellValue("BOXWEEK"));

                    string boxWeek = Format.GetString(viewer.GetCellValue("BOXWEEK"));
                    string defectqty = Format.GetString(viewer.GetCellValue("DEFECTQTY"));

                    if (string.IsNullOrWhiteSpace(defectqty) || defectqty.Equals("0"))
                    {
                        if (uom.Equals("BLK"))
                        {
                            viewer.SetCellValue("PRODUCTWEEK", productdefid + "/" + boxweek + "/" + "0");
                            viewer.SetCellValue("DEFECTQTY2", "(0)");
                        }
                        else
                        {
                            viewer.SetCellValue("PRODUCTWEEK", productdefid + "/" + boxweek + "/");
                            viewer.SetCellValue("DEFECTQTY2", "");
                        }
                    }
                    else
                    {
                        viewer.SetCellValue("PRODUCTWEEK", productdefid + "/" + boxweek + "/" + defectqty);
                        viewer.SetCellValue("DEFECTQTY2", "(" + defectqty + ")");
                    }

                }
            }

            // LGD
            if (viewer.LabelInfo.LabelID == "024")
            {
                if (fieldName.Equals("rowPARTNO") || fieldName.Equals("rowQTY") || fieldName.Equals("rowBOXLOTID") || fieldName.Equals("rowBOXWEEK"))
                {
                    string partNo = Format.GetString(viewer.GetCellValue("PARTNO"));
                    int qty = Format.GetInteger(viewer.GetCellValue("QTY"));
                    string boxLotId = Format.GetString(viewer.GetCellValue("BOXLOTID"));
                    string boxWeek = Format.GetString(viewer.GetCellValue("BOXWEEK"));
                    if(fieldName.Equals("rowQTY"))
                    {
                        viewer.SetCellValue("QTY2", qty.ToString("D5"));
                    }
                    viewer.SetCellValue("BARCODE", partNo + " " + qty.ToString("D5") + " YP 0 " + boxLotId + " " + boxWeek);
                }
                if (fieldName.Equals("rowBARCODE"))
                {
                    string barcode = Format.GetString(viewer.GetCellValue("BARCODE"));
                    string[] barcodeSplit = barcode.Split(' ');
                    viewer.SetCellValue("PARTNO", barcodeSplit[0]);
                    viewer.SetCellValue("QTY", barcodeSplit[1].TrimStart('0'));
                    viewer.SetCellValue("BOXLOTID", barcodeSplit[4]);
                    viewer.SetCellValue("BOXWEEK", barcodeSplit[5]);
                }
            }

            // BOE_P BOX, BOE_B BOX
            if (viewer.LabelInfo.LabelID == "036" || viewer.LabelInfo.LabelID == "038")
            {
                if (fieldName.Equals("rowBOXLOTID") || fieldName.Equals("rowPARTNO") || fieldName.Equals("rowQTY") || fieldName.Equals("rowVENDORCODE")
                    || fieldName.Equals("rowBOXDATE") || fieldName.Equals("rowEXPIREDDATE"))
                {
                    string boxLotId = Format.GetString(viewer.GetCellValue("BOXLOTID"));
                    string partNo = Format.GetString(viewer.GetCellValue("PARTNO"));
                    string qty = Format.GetString(viewer.GetCellValue("QTY"));
                    string vendorCode = Format.GetString(viewer.GetCellValue("VENDORCODE"));
                    string boxDate = Format.GetString(viewer.GetCellValue("BOXDATE"));
                    string expiredDate = Format.GetString(viewer.GetCellValue("EXPIREDDATE"));
                    viewer.SetCellValue("QRCODE", boxLotId + " " + partNo + " " + qty + " " + vendorCode + " " + boxDate + " " + expiredDate);
                }
                if (fieldName.Equals("rowQRCODE"))
                {
                    string qrCode = Format.GetString(viewer.GetCellValue("QRCODE"));
                    string[] qrCodeSplit = qrCode.Split(' ');
                    viewer.SetCellValue("BOXLOTID", qrCodeSplit[0]);
                    viewer.SetCellValue("PARTNO", qrCodeSplit[1]);
                    viewer.SetCellValue("QTY", qrCodeSplit[2]);
                    viewer.SetCellValue("VENDORCODE", qrCodeSplit[3]);
                    viewer.SetCellValue("BOXDATE", qrCodeSplit[4]);
                    viewer.SetCellValue("EXPIREDDATE", qrCodeSplit[5]);
                }
            }

            // BOE_TMP P BOX
            if (viewer.LabelInfo.LabelID == "039")
            {
                if (fieldName.Equals("rowSN"))
                {
                    string sn = Format.GetString(viewer.GetCellValue("SN"));
                    viewer.SetCellValue("BOXDATE", sn.Substring(0, sn.Length - 4));
                }
                if (fieldName.Equals("rowBOXDATE"))
                {
                    string sn = Format.GetString(viewer.GetCellValue("SN"));
                    string seq = sn.Substring(sn.Length - 4, 4);
                    viewer.SetCellValue("SN", Format.GetString(viewer.GetCellValue("BOXDATE")) + seq);
                }
                if (fieldName.Equals("rowSN") || fieldName.Equals("rowPARTNO") || fieldName.Equals("rowQTY") || fieldName.Equals("rowVENDORCODE")
                    || fieldName.Equals("rowBOXDATE") || fieldName.Equals("rowEXPIREDDATE"))
                {
                    string sn = Format.GetString(viewer.GetCellValue("SN"));
                    string partNo = Format.GetString(viewer.GetCellValue("PARTNO"));
                    string qty = Format.GetString(viewer.GetCellValue("QTY"));
                    string vendorCode = Format.GetString(viewer.GetCellValue("VENDORCODE"));
                    string boxDate = Format.GetString(viewer.GetCellValue("BOXDATE")); 
                    string expiredDate = Format.GetString(viewer.GetCellValue("EXPIREDDATE"));
                    viewer.SetCellValue("QRCODE1", sn + "|" + partNo + "|" + qty + "|" + vendorCode + "|" + boxDate + "|" + expiredDate);
                }
                if(fieldName.Equals("rowQRCODE1"))
                {
                    // SN || '|' || PARTNO || '|' || QTY || '|' || VENDORCODE || '|' || BOXDATE || '|' || COALESCE(EXPIREDDATE, '') AS QRCODE1
                    string qrCode1 = Format.GetString(viewer.GetCellValue("QRCODE1"));
                    string[] qrCode1Split = qrCode1.Split('|');
                    viewer.SetCellValue("SN", qrCode1Split[0]);
                    viewer.SetCellValue("PARTNO", qrCode1Split[1]);
                    viewer.SetCellValue("QTY", qrCode1Split[2]);
                    viewer.SetCellValue("VENDORCODE", qrCode1Split[3]);
                    viewer.SetCellValue("BOXDATE", qrCode1Split[4]);
                    viewer.SetCellValue("EXPIREDDATE", qrCode1Split[5]);
                }
            }

            // BOE_TMP B BOX
            if (viewer.LabelInfo.LabelID == "042")
            {
                if (fieldName.Equals("rowSN"))
                {
                    string sn = Format.GetString(viewer.GetCellValue("SN"));
                    viewer.SetCellValue("BOXDATE", sn.Substring(0, sn.Length - 4));
                }
                if (fieldName.Equals("rowBOXDATE"))
                {
                    string sn = Format.GetString(viewer.GetCellValue("SN"));
                    string seq = sn.Substring(sn.Length - 4, 4);
                    viewer.SetCellValue("SN", Format.GetString(viewer.GetCellValue("BOXDATE")) + seq);
                }
                if (fieldName.Equals("rowBOXWEEK"))
                {
                    string lotNo2 = Format.GetString(viewer.GetCellValue("LOTNO2"));
                    string boxWeek = Format.GetString(viewer.GetCellValue("BOXWEEK"));
                    viewer.SetCellValue("LOTNO2", lotNo2.Split('/')[0] + "/" + boxWeek);
                }
                if (fieldName.Equals("rowLOTNO2"))
                {
                    string lotNo2 = Format.GetString(viewer.GetCellValue("LOTNO2"));
                    viewer.SetCellValue("BOXWEEK", lotNo2.Split('/')[1]);
                }
                if (fieldName.Equals("rowQTY") || fieldName.Equals("rowCASEQTY"))
                { 
                    string productText = Format.GetString(viewer.GetCellValue("PRODUCTTEXT"));
                    string qty = Format.GetString(viewer.GetCellValue("QTY"));
                    string caseQty = Format.GetString(viewer.GetCellValue("CASEQTY"));
                    viewer.SetCellValue("PRODUCTTEXT", productText.Split('/')[0] + "/" + qty + "/" + caseQty);
                }
                if (fieldName.Equals("rowPRODUCTTEXT"))
                {
                    string productText = Format.GetString(viewer.GetCellValue("PRODUCTTEXT"));
                    viewer.SetCellValue("QTY", productText.Split('/')[1]);
                    viewer.SetCellValue("CASEQTY", productText.Split('/')[2]);
                }
                if (fieldName.Equals("SN") || fieldName.Equals("PARTNO") || fieldName.Equals("QTY") || fieldName.Equals("VENDORCODE")
                    || fieldName.Equals("BOXDATE") || fieldName.Equals("EXPIREDDATE"))
                {
                    string sn = Format.GetString(viewer.GetCellValue("SN"));
                    string partNo = Format.GetString(viewer.GetCellValue("PARTNO"));
                    string qty = Format.GetString(viewer.GetCellValue("QTY"));
                    string vendorCode = Format.GetString(viewer.GetCellValue("VENDORCODE"));
                    string boxDate = Format.GetString(viewer.GetCellValue("BOXDATE"));
                    string expiredDate = Format.GetString(viewer.GetCellValue("EXPIREDDATE"));
                    viewer.SetCellValue("QRCODE1", sn + "|" + partNo + "|" + qty + "|" + vendorCode + "|" + boxDate + "|" + expiredDate);
                }
                if (fieldName.Equals("rowQRCODE1"))
                {
                    string qrCode1 = Format.GetString(viewer.GetCellValue("QRCODE1"));
                    string[] qrCode1Split = qrCode1.Split('|');
                    viewer.SetCellValue("SN", qrCode1Split[0]);
                    viewer.SetCellValue("PARTNO", qrCode1Split[1]);
                    viewer.SetCellValue("QTY", qrCode1Split[2]);
                    viewer.SetCellValue("VENDORCODE", qrCode1Split[3]);
                    viewer.SetCellValue("BOXDATE", qrCode1Split[4]);
                    viewer.SetCellValue("EXPIREDDATE", qrCode1Split[5]);
                }
                if (fieldName.Equals("rowSN") || fieldName.Equals("rowINSPECTOR") || fieldName.Equals("rowQTY") || fieldName.Equals("rowDEFECTQTY"))
                {
                    string sn = Format.GetString(viewer.GetCellValue("SN"));
                    string inspector = Format.GetString(viewer.GetCellValue("INSPECTOR"));
                    string qty = Format.GetString(viewer.GetCellValue("QTY"));
                    string defectQty = Format.GetString(viewer.GetCellValue("DEFECTQTY")).Substring(0, Format.GetString(viewer.GetCellValue("DEFECTQTY")).Length - 4);
                    viewer.SetCellValue("QRCODE2", sn + "|" + inspector + "|" + qty + "|" + defectQty);
                }
                if (fieldName.Equals("rowQRCODE2"))
                {
                    string qrCode2 = Format.GetString(viewer.GetCellValue("QRCODE2"));
                    string[] qrCode2Split = qrCode2.Split('|');
                    viewer.SetCellValue("SN", qrCode2Split[0]);
                    viewer.SetCellValue("INSPECTOR", qrCode2Split[1]);
                    viewer.SetCellValue("QTY", qrCode2Split[2]);
                    viewer.SetCellValue("DEFECTQTY", qrCode2Split[3] + " pcs");
                }
            }
        }

        /// <summary>
        /// Cancel 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Apply 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            if (this.slcbPrintBoxLabel.EditValue == null || string.IsNullOrWhiteSpace(this.slcbPrintBoxLabel.EditValue.ToString()))
            {
                ShowMessage("선택 된 라벨이 없습니다. 선택 후 출력 하세요.");
                return;
            }

            //UsingPrintXmlBarcodeScript();

            UsingPrintBarcodeScript();

            //     UsingPrintDBBarcodeScript();
        }

        private void SetBindingLabelValue(ucLabelViewer2 viewer, XmlDocument document)
        {
            if (document != null)
            {
                XmlNode node = document.SelectSingleNode("//ZPL[@name='" + viewer.LabelInfo.LabelID + "']");
                //XmlNode node = document.SelectSingleNode("//ZPL[@name='" + "004" + "']");

                if (node != null)
                {
                    var properties = viewer.GetConditions;
                    // TODO : 삭제
                    if (!properties.ContainsKey("YP"))
                    {
                        properties.Add("YP", "영풍전자");

                    }

                    string text = node.InnerText;
                    foreach (var property in properties)
                    {
                        string findProperty = string.Format("{{!@{0}}}", property.Key);

                        text = text.Replace(findProperty, property.Value.ToString());
                    }
                    text = InsertImages(text, properties);

                    int printCnt = Format.GetInteger(ssePrintCount.Text);
                    for (int i = 0; i < printCnt; i++)
                    {
                        Commons.CommonFunction.PrintLabel(text);
                    }
                }
                else
                    MessageBox.Show(string.Format("등록된 스크립트가 없습니다 Label ID : {0}", viewer.LabelInfo.LabelName));
            }
            else
                MessageBox.Show(string.Format("등록된 스크립트가 없습니다 Label ID : {0}", viewer.LabelInfo.LabelName));

        }

        private void UsingPrintDBBarcodeScript()
        {
            List<string> selectLabelList = this.slcbPrintBoxLabel.EditValue.ToString().Split(',').ToList();


            foreach (string selectLabel in selectLabelList)
            {
                List<XtraTabPage> pageList = this.xtraTabControl1.TabPages.AsEnumerable().Where(s => s.Text.Equals(selectLabel)).ToList();

                foreach (XtraTabPage page in pageList)
                {
                    ucLabelViewer2 viewer = page.Controls.OfType<ucLabelViewer2>().FirstOrDefault();

                    SetBindingLabelValue(viewer, viewer.LabelInfo.XmlBarcodeScript);


                }
            }
        }

        private void UsingPrintXmlBarcodeScript()
        {
            List<string> selectLabelList = this.slcbPrintBoxLabel.EditValue.ToString().Split(',').ToList();

            Assembly assembly = Assembly.GetAssembly(this.GetType());
            Stream stream = assembly.GetManifestResourceStream("Micube.SmartMES.ProcessManagement.Report.LabelScript.xml");

            XmlDocument document = new XmlDocument();
            document.Load(stream);

            foreach (string selectLabel in selectLabelList)
            {
                List<XtraTabPage> pageList = this.xtraTabControl1.TabPages.AsEnumerable().Where(s => s.Text.Equals(selectLabel)).ToList();

                foreach (XtraTabPage page in pageList)
                {
                    ucLabelViewer2 viewer = page.Controls.OfType<ucLabelViewer2>().FirstOrDefault();

                    SetBindingLabelValue(viewer, document);
                }
            }
        }

        /// <summary>
        /// LABEL DATA 조회
        /// </summary>
        /// <param name="labelDefName"></param>
        private void UsingPrintBarcodeScript()
        {
            List<string> selectLabelList = this.slcbPrintBoxLabel.EditValue.ToString().Replace("\"", "").Split(',').ToList();

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LABELDEFNAME", string.Join(",", selectLabelList));

            DataTable dt = SqlExecuter.Query("GetLabelInfo", "10001", param);

            foreach (DataRow dr in dt.Rows)
            {
                List<XtraTabPage> pageList = this.xtraTabControl1.TabPages.AsEnumerable().Where(s => s.Text.Equals(dr["LABELDEFNAME"].ToString())).ToList();

                foreach (XtraTabPage page in pageList)
                {
                    ucLabelViewer2 viewer = page.Controls.OfType<ucLabelViewer2>().FirstOrDefault();

                    SetBindingLabelValue(viewer, dr["BARCODESCRIPT"].ToString());
                }
            }
        }

        private void SetBindingLabelValue(ucLabelViewer2 viewer, string document)
        {
            if (document != null)
            {
                var properties = viewer.GetConditions;
                // TODO : 삭제
                if (!properties.ContainsKey("YP"))
                {
                    properties.Add("YP", "영풍전자");

                }

                foreach (var property in properties)
                {
                    string findProperty = string.Format("{{!@{0}}}", property.Key);

                    document = document.Replace(findProperty, property.Value.ToString());
                }
                document = InsertImages(document, properties);

                int printCnt = Format.GetInteger(ssePrintCount.Text);
                for (int i = 0; i < printCnt; i++)
                {
                    Commons.CommonFunction.PrintLabel(document);
                }
            }
            else
                MessageBox.Show(string.Format("등록된 스크립트가 없습니다 Label ID : {0}", viewer.LabelInfo.LabelName));

        }

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
            foreach(string field in fields)
            {
                string[] values = field.Replace(IMG_FIELD_PREFIX, "").Replace(IMG_FIELD_SUFFIX, "").Split(':');
                string fieldName = values[0];
                string fontFamily = values[1];
                string fontSize= values[2];
                string style = (values.Length >= 4) ? values[3] : "R";

                using (Font font = CreateFont(fontFamily, GetFloat(fontSize, 40), style))
                {
                    string fileName = FILE_PREFIX + fileSeq++;
                    string imageCode = ZPLUtil.CreateZPLImageCodeFromText((string)properties[fieldName], fileName, font);
                    result.Append(imageCode);
                    zplCommand = zplCommand.Replace(field, string.Format("^XGR:{0}.GRF,1,1^FS", fileName));
                }
            }
            result.Append(zplCommand);
            return result.ToString();
        }

        private Font CreateFont(string fontFamily, float fontSize, string style)
        {
            FontStyle fontStyle = FontStyle.Regular;
            switch(style)
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
            if(parsed)
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
                if(first < 0)
                {
                    break;
                }
                last = zplCommand.IndexOf(IMG_FIELD_SUFFIX, first + IMG_FIELD_PREFIX.Length);
                if(last < 0)
                {
                    throw new SystemException("라벨형식오류");
                }
                string field = zplCommand.Substring(first, last - first + IMG_FIELD_SUFFIX.Length);
                result.Add(field);
                first = last + IMG_FIELD_SUFFIX.Length;
            }
            return result;
        }
        #endregion

        #endregion
        #region ◆ Function |



        #endregion
    }
}
