#region using
using DevExpress.XtraReports.UI;
using DevExpress.XtraTab;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Micube.Framework;
using System.Windows.Forms;
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
	public partial class PrintPackingLabelPopup : SmartPopupBaseForm, ISmartCustomPopup
	{
		public DataRow CurrentDataRow { get; set; }
     

        private List<XtraReport> viewList;
        
        #region 생성자
        public PrintPackingLabelPopup(List<XtraReport> viewList)
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
            foreach (XtraReport report in this.viewList)
            {
                XtraTabPage tab = new XtraTabPage();
                tab.Text = report.DisplayName;

                ucLabelViewer labelViewer = new ucLabelViewer();
                labelViewer.OnPropertyGridDataChange += LabelViewer_OnPropertyGridDataChange;
                tab.Controls.Add(labelViewer);
                labelViewer.Dock = DockStyle.Fill;
                labelViewer.SetBindingPreview(report);
                labelViewer.Tag = report.Tag.ToString();
                this.xtraTabControl1.TabPages.Add(tab);

            }

            var itemList = this.viewList.Select(s => s.DisplayName).ToList().Distinct();

            DataTable dt = new DataTable();
            dt.Columns.Add("LABELDEFNAME");

            foreach (string label in itemList)
            {
                dt.Rows.Add(new object[] { label });
            }

            this.slcbPrintBoxLabel.Editor.DisplayMember = "LABELDEFNAME";
            this.slcbPrintBoxLabel.Editor.ValueMember = "LABELDEFNAME";


            this.slcbPrintBoxLabel.Editor.DataSource = dt;

        }

        private void LabelViewer_OnPropertyGridDataChange(ucLabelViewer viewer, string fieldName, string value)
        {
            string setValue = string.Empty;

            if (viewer.Tag.ToString() == "001" )
            {
                if (fieldName.Equals("rowBLOCK") && !string.IsNullOrWhiteSpace(value))
                {
                    string Qty = viewer.GetCellValue("QTY");

                    string[] QtyDefect = Qty.Split('/');

                    int Bloc = Format.GetInteger(viewer.GetCellValue("BLOCK"));
                    int GoodQty = Format.GetInteger(QtyDefect[0]);
                    int DefectQty = Format.GetInteger(QtyDefect[1]);

                    if (GoodQty.Equals(0) && DefectQty.Equals(0)) return;


                    int pcsArray = Format.GetInteger(viewer.GetCellValue("PCSARY"));

                    int calValue = (Bloc * pcsArray) - GoodQty;
                    //DEFECTQTY
                    setValue = string.Empty;
                    if (viewer.Tag.ToString() == "001")
                    {
                        setValue = GoodQty.ToString() + "/" + calValue.ToString();
                        viewer.SetCellValue("QTY", setValue);
                    }
                    else
                    {
                        viewer.SetCellValue("DEFECTQTY", Format.GetString(calValue));
                    }
                    
                    
                    //  if(fieldName.)
                }
            }
            if (viewer.Tag.ToString() == "015")
            {
                SetGisLabelField(viewer, fieldName, value);
                /*  NOTE : 기존 로직
                if (fieldName.Equals("rowBLOCK") && !string.IsNullOrWhiteSpace(value))
                {
                    string Qty = viewer.GetCellValue("QTY");

                    

                    int Bloc = Format.GetInteger(viewer.GetCellValue("BLOCK"));
                    int GoodQty = Format.GetInteger(viewer.GetCellValue("QTY"));

                    if (GoodQty.Equals(0)) return;


                    int pcsArray = Format.GetInteger(viewer.GetCellValue("PCSARY"));

                    int calValue = (Bloc * pcsArray) - GoodQty;
                    //DEFECTQTY
                    setValue = string.Empty;
 
                    viewer.SetCellValue("DEFECTQTY", Format.GetString(calValue));
 
                    //  if(fieldName.)
                }
                if (fieldName.Equals("rowBOXLOTID") || fieldName.Equals("rowQTY"))
                {
                    setValue = "*" + viewer.GetCellValue("BOXLOTID") + viewer.GetCellValue("QTY") + "*";
                    viewer.SetCellValue("BARCODE", setValue);
                }
                if (fieldName.Equals("rowBOXWEEK") || fieldName.Equals("rowDEFECTQTY"))
                {
                    string[] productWeek = viewer.GetCellValue("PRODUCTWEEK").Split('/');
                    setValue = productWeek[0] + viewer.GetCellValue("BOXWEEK") + viewer.GetCellValue("DEFECTQTY");
                    viewer.SetCellValue("PRODUCTWORKER", setValue);
                }
                */
            }
            // LGD BOX
            if (viewer.Tag.ToString().Equals("024"))
            {
                SetLgdLabelField(viewer, fieldName, value);
            }
            // BOE_TMP P BOX
            if (viewer.Tag.ToString().Equals("039"))
            {
                // QRCode
                if (!string.IsNullOrWhiteSpace(value) && (fieldName.Equals("rowSN") || fieldName.Equals("rowPARTNO") ||
                    fieldName.Equals("rowQTY") || fieldName.Equals("rowVENDORCODE") || fieldName.Equals("rowMAKEDATE") ||
                    fieldName.Equals("rowEXPIREDDATE")))
                {
                    setValue = string.Format("{0}|{1}|{2}|{3}|{4}|{5}", viewer.GetCellValue("SN"), viewer.GetCellValue("PARTNO")
                        , viewer.GetCellValue("QTY") , viewer.GetCellValue("VENDORCODE"), viewer.GetCellValue("MAKEDATE")
                        , viewer.GetCellValue("EXPIREDDATE"));
                    viewer.SetCellValue("QRCODE1", setValue);
                }

                // PRODUCTTEXT
                if (!string.IsNullOrWhiteSpace(value) && (fieldName.Equals("rowPRODUCTTEXT") || fieldName.Equals("rowPACKAGEQTY")))
                {
                    string[] tmp = viewer.GetCellValue("PRODUCTTEXT").Split('/');

                    if(fieldName.Equals("rowPACKAGEQTY"))
                        viewer.SetCellValue("PACKAGEQTY", viewer.GetCellValue("PACKAGEQTY"));
                    else
                        viewer.SetCellValue("PACKAGEQTY", tmp[1]);

                    setValue = string.Format("{0}/{1}", tmp[0], string.Format("{0:0,0}", viewer.GetCellValue("PACKAGEQTY")));
                    viewer.SetCellValue("PRODUCTTEXT", setValue);
                }
            }
            // BOE_TMP P CASE
            if (viewer.Tag.ToString().Equals("040"))
            {
                // QRCode
                if (!string.IsNullOrWhiteSpace(value) && (fieldName.Equals("rowSN") || fieldName.Equals("rowPARTNO") ||
                    fieldName.Equals("rowCASEPCSQTY") || fieldName.Equals("rowVENDORCODE") || fieldName.Equals("rowMAKEDATE") ||
                    fieldName.Equals("rowEXPIREDDATE") || fieldName.Equals("rowLOTNO")))
                {
                    setValue = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}", viewer.GetCellValue("SN"), viewer.GetCellValue("PARTNO")
                        , viewer.GetCellValue("CASEPCSQTY"), viewer.GetCellValue("VENDORCODE"), viewer.GetCellValue("MAKEDATE")
                        , viewer.GetCellValue("EXPIREDDATE"), viewer.GetCellValue("LOTNO"));
                    viewer.SetCellValue("QRCODE1", setValue);
                }

                // PRODUCTTEXT
                if (!string.IsNullOrWhiteSpace(value) && (fieldName.Equals("rowPRODUCTTEXT") || fieldName.Equals("rowCASEPCSQTY")))
                {
                    string[] tmp = viewer.GetCellValue("PRODUCTTEXT").Split('/');

                    if (fieldName.Equals("rowCASEPCSQTY"))
                        viewer.SetCellValue("CASEPCSQTY", viewer.GetCellValue("CASEPCSQTY"));
                    else
                        viewer.SetCellValue("CASEPCSQTY", tmp[1]);

                    setValue = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}", viewer.GetCellValue("SN"), viewer.GetCellValue("PARTNO")
                        , viewer.GetCellValue("CASEPCSQTY"), viewer.GetCellValue("VENDORCODE"), viewer.GetCellValue("MAKEDATE")
                        , viewer.GetCellValue("EXPIREDDATE"), viewer.GetCellValue("LOTNO"));
                    viewer.SetCellValue("QRCODE1", setValue);

                    setValue = string.Format("{0}/{1}", tmp[0], string.Format("{0:0,0}", viewer.GetCellValue("CASEPCSQTY")));
                    viewer.SetCellValue("PRODUCTTEXT", setValue);
                }
            }

            // BOE_TMP B CASE
            if (viewer.Tag.ToString().Equals("041"))
            {
                // QRCode
                if (!string.IsNullOrWhiteSpace(value) && (fieldName.Equals("rowSN") || fieldName.Equals("rowPARTNO") ||
                    fieldName.Equals("rowQTY") || fieldName.Equals("rowVENDORCODE") || fieldName.Equals("rowMAKEDATE") ||
                    fieldName.Equals("rowEXPIREDDATE") || fieldName.Equals("rowLOTNO")))
                {
                    setValue = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}", viewer.GetCellValue("SN"), viewer.GetCellValue("PARTNO")
                        , viewer.GetCellValue("QTY"), viewer.GetCellValue("VENDORCODE"), viewer.GetCellValue("MAKEDATE")
                        , viewer.GetCellValue("EXPIREDDATE"), viewer.GetCellValue("LOTNO"));
                    viewer.SetCellValue("QRCODE1", setValue);

                    // PRODUCTTEXT
                    string[] tmp = viewer.GetCellValue("PRODUCTTEXT").Split('/');

                    setValue = string.Format("{0}/{1}/{2}", tmp[0], viewer.GetCellValue("QTY"), tmp[2]);
                    viewer.SetCellValue("PRODUCTTEXT", setValue);
                }

                // PRODUCTTEXT
                if (!string.IsNullOrWhiteSpace(value) && (fieldName.Equals("rowPRODUCTTEXT") || fieldName.Equals("rowBLOCKQTY") ||
                    fieldName.Equals("rowQTY")))
                {
                    string[] tmp = viewer.GetCellValue("PRODUCTTEXT").Split('/');

                    if (fieldName.Equals("rowBLOCKQTY"))
                        viewer.SetCellValue("BLOCKQTY", viewer.GetCellValue("BLOCKQTY"));
                    else
                        viewer.SetCellValue("BLOCKQTY", tmp[2]);

                    setValue = string.Format("{0}/{1}/{2}", tmp[0], fieldName.Equals("rowQTY"), viewer.GetCellValue("BLOCKQTY"));
                    viewer.SetCellValue("PRODUCTTEXT", setValue);
                }
            }
            // BOE_TMP B BOX
            if (viewer.Tag.ToString().Equals("042"))
            {
                // QRCode
                if (!string.IsNullOrWhiteSpace(value) && (fieldName.Equals("rowSN") || fieldName.Equals("rowPARTNO") ||
                    fieldName.Equals("rowQTY") || fieldName.Equals("rowVENDORCODE") || fieldName.Equals("rowMAKEDATE") ||
                    fieldName.Equals("rowEXPIREDDATE") || fieldName.Equals("rowINSPECTOR") || fieldName.Equals("rowDEFECTQTY")))
                {
                    setValue = string.Format("{0}|{1}|{2}|{3}|{4}|{5}", viewer.GetCellValue("SN"), viewer.GetCellValue("PARTNO")
                        , viewer.GetCellValue("QTY"), viewer.GetCellValue("VENDORCODE"), viewer.GetCellValue("MAKEDATE")
                        , viewer.GetCellValue("EXPIREDDATE"));
                    viewer.SetCellValue("QRCODE1", setValue);

                    // QRCOD2
                    string defectqty = viewer.GetCellValue("DEFECTQTY").Replace("pcs","").Trim();

                    setValue = string.Format("{0}|{1}|{2}|{3}", viewer.GetCellValue("SN"), viewer.GetCellValue("INSPECTOR").Replace("[", "").Replace("]","")
                        , viewer.GetCellValue("QTY"), defectqty);
                    viewer.SetCellValue("QRCODE2", setValue);
                }

                // PRODUCTTEXT
                if (!string.IsNullOrWhiteSpace(value) && (fieldName.Equals("rowPRODUCTTEXT") || fieldName.Equals("rowCASEQTY") ||
                    fieldName.Equals("rowQTY")))
                {
                    string[] tmp = viewer.GetCellValue("PRODUCTTEXT").Split('/');

                    if (fieldName.Equals("rowCASEQTY"))
                        viewer.SetCellValue("CASEQTY", viewer.GetCellValue("CASEQTY"));
                    else
                        viewer.SetCellValue("CASEQTY", tmp[1]);

                    setValue = string.Format("{0}/{1}/{2}", tmp[0], fieldName.Equals("rowQTY"), viewer.GetCellValue("CASEQTY"));
                    viewer.SetCellValue("PRODUCTTEXT", setValue);
                }
            }
        }

        #region GIS LABEL (015)
        private void SetGisLabelField(ucLabelViewer viewer, string fieldName, string value)
        {
            try
            {
                switch (fieldName)
                {
                    case "rowNOWDATE":
                        UpdateGisLabelBarcode(viewer);
                        break;
                    case "rowBOXLOTID":
                        UpdateGisLabelBarcode(viewer);
                        break;
                    case "rowBARCODE":
                        SplitGisLabelBarcode(viewer);
                        break;
                    case "rowBLOCK":
                    case "rowPCSARY":
                        UpdateGisLabelDefectQty(viewer);
                        break;
                    case "rowQTY":
                        UpdateGisLabelDefectQty(viewer);
                        UpdateGisLabelBarcode(viewer);
                        break;
                    case "rowBOXWEEK":
                        UpdateGisLabelProductWeek(viewer);
                        break;
                    case "rowDEFECTQTY":
                        UpdateGisLabelProductWeek(viewer);
                        break;
                    case "rowPRODUCTWEEK":
                        // TODO : productdefid, ver  추출 후 partno2, pcsary 수정
                        SplitGisLabelProductWeek(viewer);
                        break;
                    case "rowPRODUCTWORKER":
                        UpdateGisLabelProductWeek(viewer);
                        break;
                    case "rowPARTNO2":
                        UpdateGisLabelBarcode(viewer);
                        break;
                }
            }
            catch (System.ArgumentOutOfRangeException)
            {
                // TODO : 다국어 처리
                ShowMessage("값이 없거나 길이가 짧은 항목이 있습니다.");
            }
        }

        private void UpdateGisLabelBarcode(ucLabelViewer viewer)
        {
            string oldBarcode = Format.GetString(viewer.GetCellValue("BARCODE"));
            string newBarcode = "*" + viewer.GetCellValue("NOWDATE")
                + viewer.GetCellValue("PARTNO2").Substring(0, 4)
                + viewer.GetCellValue("BOXLOTID")
                + Format.GetInteger(viewer.GetCellValue("QTY")).ToString() + "*";
            if (oldBarcode != newBarcode)
            {
                viewer.SetCellValue("BARCODE", newBarcode);
                SetGisLabelField(viewer, "rowBARCODE", newBarcode);
            }
        }

        private void SplitGisLabelBarcode(ucLabelViewer viewer)
        {
            string barcode = Format.GetString(viewer.GetCellValue("BARCODE"));

            string newNowDate = barcode.Substring(1, 4);
            string newBoxLotId = barcode.Substring(9, 10);
            int newGoodQty = Format.GetInteger(barcode.Substring(19, barcode.Length - 19 - 1));

            string oldNowDate = viewer.GetCellValue("NOWDATE");
            string oldBoxLotId = viewer.GetCellValue("BOXLOTID");
            int oldGoodQty = Format.GetInteger(viewer.GetCellValue("QTY"));

            if (newNowDate != oldNowDate)
            {
                viewer.SetCellValue("NOWDATE", newNowDate);
                SetGisLabelField(viewer, "rowNOWDATE", newNowDate);
            }
            if (newBoxLotId != oldBoxLotId)
            {
                viewer.SetCellValue("BOXLOTID", newBoxLotId);
                SetGisLabelField(viewer, "rowBOXLOTID", newBoxLotId);
            }
            if (newGoodQty != oldGoodQty)
            {
                viewer.SetCellValue("QTY", newGoodQty.ToString());
                SetGisLabelField(viewer, "rowQTY", newGoodQty.ToString());
            }
        }

        private void UpdateGisLabelDefectQty(ucLabelViewer viewer)
        {
            int block = Format.GetInteger(viewer.GetCellValue("BLOCK"));
            int goodQty = Format.GetInteger(viewer.GetCellValue("QTY"));
            int pcsArray = Format.GetInteger(viewer.GetCellValue("PCSARY"));

            if (goodQty == 0)
            {
                return;
            }

            int oldDefectQty = Format.GetInteger(Format.GetString(viewer.GetCellValue("DEFECTQTY")).TrimStart('(').TrimEnd(')'));
            int newDefectQty = (block * pcsArray) - goodQty;

            if (oldDefectQty != newDefectQty)
            {
                viewer.SetCellValue("DEFECTQTY", "(" + Format.GetString(newDefectQty) + ")");
                SetGisLabelField(viewer, "rowDEFECTQTY", "(" + Format.GetString(newDefectQty) + ")");
            }
        }

        private void UpdateGisLabelProductWeek(ucLabelViewer viewer)
        {
            string oldProductWeek = Format.GetString(viewer.GetCellValue("PRODUCTWEEK"));

            string productDefIdVer = oldProductWeek.Split('/')[0];
            string boxWeek = Format.GetString(viewer.GetCellValue("BOXWEEK"));
            string defectQty = Format.GetString(viewer.GetCellValue("DEFECTQTY")).TrimStart('(').TrimEnd(')');

            string newProductWeek = productDefIdVer + "/" + boxWeek + "/" + defectQty;

            if (oldProductWeek != newProductWeek)
            {
                viewer.SetCellValue("PRODUCTWEEK", newProductWeek);
                SetGisLabelField(viewer, "rowPRODUCTWEEK", newProductWeek);
            }
        }

        private void SplitGisLabelProductWeek(ucLabelViewer viewer)
        {
            string productWeek = Format.GetString(viewer.GetCellValue("PRODUCTWEEK"));

            string newProductDefId = productWeek.Split('/')[0].Substring(0, productWeek.Split('/')[0].Length - 1);
            string newBoxWeek = productWeek.Split('/')[1];
            string newDefectQty = productWeek.Split('/')[2] == "" ? "" : "(" + productWeek.Split('/')[2] + ")";

            string oldProductWorker = Format.GetString(viewer.GetCellValue("PRODUCTWORKER"));
            string oldBoxWeek = Format.GetString(viewer.GetCellValue("BOXWEEK"));
            string oldDefectQty = Format.GetString(viewer.GetCellValue("DEFECTQTY"));

            if (newProductDefId != oldProductWorker.Split('[')[0])
            {
                viewer.SetCellValue("PRODUCTWORKER", newProductDefId + "[" + oldProductWorker.Split('[')[1]);
                SetGisLabelField(viewer, "rowPRODUCTWORKER", newProductDefId + "[" + oldProductWorker.Split('[')[1]);
            }
            if (newBoxWeek != oldBoxWeek)
            {
                viewer.SetCellValue("BOXWEEK", newBoxWeek);
                SetGisLabelField(viewer, "rowBOXWEEK", newBoxWeek);
            }
            if (newDefectQty != oldDefectQty)
            {
                viewer.SetCellValue("DEFECTQTY", newDefectQty);
                SetGisLabelField(viewer, "rowDEFECTQTY", newDefectQty);
            }
        }
        #endregion

        #region LGD LABEL (024)
        private void SetLgdLabelField(ucLabelViewer viewer, string fieldName, string value)
        {
            try
            {
                switch (fieldName)
                {
                    case "rowBOXLOTID":
                        UpdateLgdLabelBarcode(viewer);
                        break;
                    case "rowPARTNO":
                        UpdateLgdLabelBarcode(viewer);
                        break;
                    case "rowQTY":
                        UpdateLgdLabelBarcode(viewer);
                        break;
                    case "rowBARCODE":
                        SplitLgdLabelBarcode(viewer);
                        break;
                }
            }
            catch (System.ArgumentOutOfRangeException)
            {
                // TODO : 다국어 처리
                ShowMessage("값이 없거나 길이가 짧은 항목이 있습니다.");
            }
        }

        private void UpdateLgdLabelBarcode(ucLabelViewer viewer)
        {
            string oldBarcode = Format.GetString(viewer.GetCellValue("BARCODE"));

            string partNo = Format.GetString(viewer.GetCellValue("PARTNO"));
            string qty = Format.GetInteger(viewer.GetCellValue("QTY")).ToString("D5");
            string boxLotId = Format.GetString(viewer.GetCellValue("BOXLOTID"));
            string boxWeek = oldBarcode.Split(' ')[5];

            string newBarcode = partNo + " " + qty + " YP 0 " + boxLotId + " " + boxWeek;

            if (oldBarcode != newBarcode)
            {
                viewer.SetCellValue("BARCODE", newBarcode);
                SetLgdLabelField(viewer, "rowBARCODE", newBarcode);
            }
        }

        private void SplitLgdLabelBarcode(ucLabelViewer viewer)
        {
            string barcode = Format.GetString(viewer.GetCellValue("BARCODE"));

            string newPartNo = barcode.Split(' ')[0];
            int newQty = Format.GetInteger(barcode.Split(' ')[1]);
            string newBoxLotId = barcode.Split(' ')[4];

            string oldPartNo = Format.GetString(viewer.GetCellValue("PARTNO"));
            int oldQty = Format.GetInteger(viewer.GetCellValue("QTY"));
            string oldBoxLotId = Format.GetString(viewer.GetCellValue("BOXLOTID"));

            if (newPartNo != oldPartNo)
            {
                viewer.SetCellValue("PARTNO", newPartNo);
                SetLgdLabelField(viewer, "rowPARTNO", newPartNo);
            }
            if (newQty != oldQty)
            {
                viewer.SetCellValue("QTY", newQty.ToString());
                SetLgdLabelField(viewer, "rowQTY", newQty.ToString());
            }
            if (newBoxLotId != oldBoxLotId)
            {
                viewer.SetCellValue("BOXLOTID", newBoxLotId);
                SetLgdLabelField(viewer, "rowBOXLOTID", newBoxLotId);
            }
        }
        #endregion

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
            if(this.slcbPrintBoxLabel.EditValue == null ||  string.IsNullOrWhiteSpace(this.slcbPrintBoxLabel.EditValue.ToString()))
            {
                ShowMessage("선택 된 라벨이 없습니다. 선택 후 출력 하세요.");
                return;
            }
            List<string> selectLabelList = this.slcbPrintBoxLabel.EditValue.ToString().Split(',').ToList();

            foreach (string selectLabel in selectLabelList)
            {
                List<XtraTabPage> pageList = this.xtraTabControl1.TabPages.AsEnumerable().Where(s => s.Text.Equals(selectLabel)).ToList();

                foreach (XtraTabPage page in pageList)
                {
                    ucLabelViewer viewer = page.Controls.OfType<ucLabelViewer>().FirstOrDefault();

                    if (Format.GetString(viewer.Tag) == "001")
                    {
                        int Xout = Format.GetInteger(viewer.GetCellValue("XOUT"));
                        int Block = Format.GetInteger(viewer.GetCellValue("BLOCK"));

                        if (Block == 0)
                        {
                            //다국어 등록 해야함
                            ShowMessage("블럭수를 입력해주세요");
                            return;

                        }
                        string[] tmp = Format.GetString(viewer.GetCellValue("QTY")).Split('/');
                        int goodQty = Format.GetInteger(tmp[0]);
                        int defectQty = Format.GetInteger(tmp[1]);
                        int pcsaray = Format.GetInteger(viewer.GetCellValue("PCSARY"));

                        if ((Xout * Block) != ((pcsaray * Block) - goodQty))
                        {
                            //불량수가 잘못 되었습니다(Xout*불량수 <> (블럭당PCS수*블럭수)-양품수
                            ShowMessage("불량수가 잘못 되었습니다(Xout*불량수 <> (블럭당PCS수*블럭수)-양품수");
                            return;
                        }
                        if (Xout < 0)
                        {
                            ShowMessage("Xout은 영보다 작을수 없습니다");
                            return;
                        }
                        if (Xout > goodQty)
                        {
                            ShowMessage("Xout은 양품수 보다 클 수 없습니다.");
                        }
                    }
                    XtraReport report = viewer.GetLabelReport();

                    for (int i = 0; i < this.ssePrintCount.Value; i++)
                    {
                        Commons.CommonFunction.PrintLabel(report);
                    }

                }
            }

        }
        #endregion

        #region ◆ Function |



        #endregion

    }
}
