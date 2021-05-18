#region using

using DevExpress.XtraEditors;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons.SPCLibrary;

#endregion

namespace Micube.SmartMES.SPC.UserControl
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ucChartDetail : DevExpress.XtraEditors.XtraUserControl
    {
        #region Local Variables

        public delegate void SpcDetailChartbtnCloseClickEventHandler(object sender, EventArgs e);
        public virtual event SpcDetailChartbtnCloseClickEventHandler SpcChartDetailbtnCloseClickEventHandler;

        private ControlSpec _controlSpec;
        string _titleObserve;
        string _titleWithin;
        string _titleOverall;
        string _titleSpcPpmLsl;
        string _titleSpcPpmUsl;
        string _titleSpcPpmTotal;
        #endregion

        #region 생성자
        public ucChartDetail()
        {
            InitializeComponent();
            InitializeContent();
        }

        #endregion        

        #region 컨텐츠 영역 초기화

        protected void InitializeContent()
        {
            InitializeEvent();
            InitializeGrid();

            this.ucXBar1.DetialChartControlClear();
        }

        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            #region RowData
            //grdRawData = null;
            grdRawData.View.ClearDatas();
            grdRawData.View.ClearColumns();
            grdRawData.GridButtonItem = GridButtonItem.Export;
            grdRawData.View.AddTextBoxColumn("FIELDNAME", 150)
                .SetIsReadOnly()
                .SetLabel("FIELDNAME");
            grdRawData.View.AddTextBoxColumn("FIELDDATA", 100)
                .SetIsReadOnly()
                .SetLabel("FIELDDATA");
            grdRawData.View.AddTextBoxColumn("FIELDDETAILDATA", 150)
                .SetIsReadOnly()
                .SetLabel("FIELDDETAILDATA");


            //grdRawData.View.AddTextBoxColumn("SEQNO", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("GROUPID", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("SUBGROUP", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("EXTRAID", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("EXTRACONDITIONS", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("LSL", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("CSL", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("USL", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("SPECMODE", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("SAMPLINGCOUNT", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("SUBGROUPCOUNT", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("PCOUNT", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("KCOUNT", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("NVALUE_TOL", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("NVALUE_AVG", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("SVALUE_AVG", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("SVALUE_RTD", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("SVALUE_RTDC4", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("SVALUE_STD", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("SVALUE_STDC4", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("SVALUE_PTD", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("SVALUE_PTDC4", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("CP", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("CPL", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("CPU", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("CPK", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("CPM", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("JUDGMENTCPK", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("PCISUBGROUP", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("PCI_d2", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("PCI_c4S", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("PCI_c4C", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("PVALUE_AVG", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("PVALUE_STD", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("PVALUE_STDC4", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("PP", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("PPL", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("PPU", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("PPK", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("JUDGMENTPPK", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("PPI_c4", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("TAUSL", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("TACSL", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("TALSL", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("STATUS", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("STATUSMESSAGE", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("ERRORNO", 120).SetIsReadOnly();
            //grdRawData.View.AddTextBoxColumn("ERRORMESSAGE", 120).SetIsReadOnly();


            //grdRawData.View.AddTextBoxColumn("CREATOR", 80)
            //    .SetIsReadOnly()
            //    .SetTextAlignment(TextAlignment.Center); // 생성자
            //grdRawData.View.AddTextBoxColumn("CREATEDTIME", 130)
            //    .SetDisplayFormat("yyyy-MM-dd HH;mm:ss")
            //    .SetIsReadOnly()
            //    .SetTextAlignment(TextAlignment.Center); // 생성시간
            //grdRawData.View.AddTextBoxColumn("MODIFIER", 80)
            //    .SetIsReadOnly()
            //    .SetTextAlignment(TextAlignment.Center); // 수정자
            //grdRawData.View.AddTextBoxColumn("MODIFIEDTIME", 130)
            //    .SetDisplayFormat("yyyy-MM-dd HH;mm:ss")
            //    .SetIsReadOnly()
            //    .SetTextAlignment(TextAlignment.Center); // 수정시간

            //grdRawData.View.AddTextBoxColumn("SEQUENCE")
            //    .SetIsHidden(); // 일련번호
            //grdRawData.View.AddTextBoxColumn("ISSPECOUT")
            //    .SetDefault("N")
            //    .SetIsHidden(); // SpecOut 여부

            grdRawData.View.PopulateColumns();

            //grdRawData.View.Columns[8].AppearanceCell.BackColor = Color.Moccasin; // 적정량 Column 색깔변경
            //grdRawData.View.OptionsView.AllowCellMerge = false; // CellMerge
            //grdRawData.View.FixColumn(new string[] { "PROCESSSEGMENTCLASSNAME", "EQUIPMENTNAME", "STATE", "DEGREE", "CHILDEQUIPMENTNAME", "CHEMICALNAME", "CHEMICALLEVEL", "MANAGEMENTSCOPE" });
            //RepositoryItemTimeEdit edit = new RepositoryItemTimeEdit();
            //edit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            //edit.Mask.EditMask = "([0-1]?[0-9]|2[0-3]):[0-5][0-9]";
            //edit.Mask.UseMaskAsDisplayFormat = true;
            //grdRawData.View.Columns["COLLECTIONTIME"].ColumnEdit = edit;
            #endregion


            #region 관측성능 Grid
            //grdRawData = null;
            grdPpmObserve.View.ClearDatas();
            grdPpmObserve.View.ClearColumns();
            grdPpmObserve.View.SetIsReadOnly();
            grdPpmObserve.GridButtonItem = GridButtonItem.Export;
            string _titleObserve;
            string _titleWithin;
            string _titleOverall;
            var groupSub01 = grdPpmObserve.View.AddGroupColumn("SPCPPMOBSERVE");//관측성능
            groupSub01.AddTextBoxColumn("TITLE", 130).SetLabel("TITLE");
            groupSub01.AddTextBoxColumn("NVALUE", 90).SetLabel("NVALUE");
            //groupSub01.AddTextBoxColumn("NVALUEDETAIL", 150).SetLabel("NVALUEDETAIL");
            grdPpmObserve.View.OptionsView.ShowColumnHeaders = false;
            grdPpmObserve.View.PopulateColumns();
            #endregion


            #region 기대성능(군내) Grid
            grdPpmWithin.View.ClearDatas();
            grdPpmWithin.View.ClearColumns();
            grdPpmWithin.View.SetIsReadOnly();
            grdPpmWithin.GridButtonItem = GridButtonItem.Export;

            var groupSub02 = grdPpmWithin.View.AddGroupColumn("SPCPPMWITHIN");//기대성능(군내)
            groupSub02.AddTextBoxColumn("TITLE", 130).SetLabel("TITLE");
            groupSub02.AddTextBoxColumn("NVALUE", 90).SetLabel("NVALUE");
            //grdPpmWithin.AddTextBoxColumn("NVALUEDETAIL", 150).SetLabel("NVALUEDETAIL");
            grdPpmWithin.View.OptionsView.ShowColumnHeaders = false;
            grdPpmWithin.View.PopulateColumns();
            #endregion

            #region 기대성능(전체) Grid
            grdPpmOverall.View.ClearDatas();
            grdPpmOverall.View.ClearColumns();
            grdPpmOverall.View.SetIsReadOnly();
            grdPpmOverall.GridButtonItem = GridButtonItem.Export;

            var groupSub03 = grdPpmOverall.View.AddGroupColumn("SPCPPMOVERALL");//기대성능(전체)
            groupSub03.AddTextBoxColumn("TITLE", 130).SetLabel("TITLE");
            groupSub03.AddTextBoxColumn("NVALUE", 90).SetLabel("NVALUE");
            //grdPpmOverall.AddTextBoxColumn("NVALUEDETAIL", 150).SetLabel("NVALUEDETAIL");
            grdPpmOverall.View.OptionsView.ShowColumnHeaders = false;
            grdPpmOverall.View.PopulateColumns();
            #endregion

        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            this.btnClose.Click += (s, e) => SpcChartDetailbtnCloseClickEventHandler?.Invoke(s, e);

            //gropup box 1 확대
            this.lblExtension1.Click += (s, e) =>
            {
                this.splMainPanel1.PanelVisibility = SplitPanelVisibility.Panel1;
                this.splMain.PanelVisibility = SplitPanelVisibility.Panel1;
            };

            //gropup box 1 축소
            this.lblReduction1.Click += (s, e) =>
            {
                this.splMainPanel1.PanelVisibility = SplitPanelVisibility.Both;
                this.splMain.PanelVisibility = SplitPanelVisibility.Both;
            };

            //gropup box 2 확대
            this.lblExtension2.Click += (s, e) =>
            {
                this.splMainPanel1.PanelVisibility = SplitPanelVisibility.Panel2;
                this.splMain.PanelVisibility = SplitPanelVisibility.Panel1;
            };

            //gropup box 2 축소
            this.lblReduction2.Click += (s, e) =>
            {
                this.splMainPanel1.PanelVisibility = SplitPanelVisibility.Both;
                this.splMain.PanelVisibility = SplitPanelVisibility.Both;
            };

            //gropup box 3 확대
            this.lblExtension3.Click += (s, e) => this.splMain.PanelVisibility = SplitPanelVisibility.Panel2;

            //gropup box 3 축소
            this.lblReduction3.Click += (s, e) => this.splMain.PanelVisibility = SplitPanelVisibility.Both;
        }
        private void grdRawData_Load(object sender, EventArgs e)
        {
            SpcClass.SpcDictionaryDataSetting();
            SpcDetailPopupGridDataSetting();
            this.JudgmentSetting(rdoJudgmentOption.SelectedIndex);
            _titleObserve = SpcDictionary.read(SpcDicClassId.GRID, "SPCPPMOBSERVE");
            _titleWithin = SpcDictionary.read(SpcDicClassId.GRID, "SPCPPMWITHIN");
            _titleOverall = SpcDictionary.read(SpcDicClassId.GRID, "SPCPPMOVERALL");
            _titleSpcPpmLsl = SpcDictionary.read(SpcDicClassId.GRID, "SPCPPMLSL");
            _titleSpcPpmUsl = SpcDictionary.read(SpcDicClassId.GRID, "SPCPPMUSL");
            _titleSpcPpmTotal = SpcDictionary.read(SpcDicClassId.GRID, "SPCPPMTOTAL");

            this.GridDataViewPpmObserve();
            this.GridDataViewPpmWithin();
            this.GridDataViewPpmOverall();

        }

        /// <summary>
        /// 공정능력 판정 설정 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdoJudgmentOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.JudgmentSetting(rdoJudgmentOption.SelectedIndex);
        }

        #endregion

        #region public Function
        public void SpcDetailPopupInitialize(ControlSpec controlSpec)
        {
            _controlSpec = controlSpec;
        }

        //subgroupID = ucChartDetail1._controlSpec.sigmaResult.subGroup;
        //       subgroupName = ucChartDetail1._controlSpec.sigmaResult.subGroupName;

        /// <summary>
        /// 상세 Popup의 SubGroup ID
        /// </summary>
        /// <returns></returns>
        public string GetControlSpecSigmaResultSubGroup()
        {
            string result = "";
            result = _controlSpec.sigmaResult.subGroup;
            return result;
        }
        /// <summary>
        /// 상세 Popup의 SubGroup Name
        /// </summary>
        /// <returns></returns>
        public string GetControlSpecSigmaResultsubGroupName()
        {
            string result = "";
            result = _controlSpec.sigmaResult.subGroupName;
            return result;
        }

        public void SpcDetailPopupGridDataSetting()
        {
            grdRawData.DataSource = null;
            if (_controlSpec != null && _controlSpec.sigmaResult != null && _controlSpec.sigmaResult.dtCpkResult != null)
            {
                if (_controlSpec.sigmaResult.dtCpkResult.Rows.Count > 0)
                {
                    InitializeGrid();
                    grdRawData.DataSource = _controlSpec.sigmaResult.dtCpkResult;
                }
            }

        }
        #endregion

        #region Private Function
        /// <summary>
        /// 공정능력 판정 Control 초기화
        /// </summary>
        private void ControlJudgmentInitialize(string pkTitle = "Cpk")
        {
            this.lblJudgmentValue.Text = "";
            chkClass01.Properties.Appearance.BackColor = Color.White;
            chkClass02.Properties.Appearance.BackColor = Color.White;
            chkClass03.Properties.Appearance.BackColor = Color.White;
            chkClass04.Properties.Appearance.BackColor = Color.White;
            chkClass05.Properties.Appearance.BackColor = Color.White;
            chkClass06.Properties.Appearance.BackColor = Color.White;

            this.chkClass01.Checked = false;
            this.chkClass02.Checked = false;
            this.chkClass03.Checked = false;
            this.chkClass04.Checked = false;
            this.chkClass05.Checked = false;
            this.chkClass06.Checked = false;

            chkClass01.ReadOnly = true;
            chkClass02.ReadOnly = true;
            chkClass03.ReadOnly = true;
            chkClass04.ReadOnly = true;
            chkClass05.ReadOnly = true;
            chkClass06.ReadOnly = true;

            //SPCJUDGMENTCPK
            Color colorLabelDefault = Color.Gray;
            lblClass01.Text = string.Format("2.00 <= {0}", pkTitle);
            lblClass02.Text = string.Format("1.67 <= {0} < 2.00", pkTitle);
            lblClass03.Text = string.Format("1.33 <= {0} < 1.67", pkTitle);
            lblClass04.Text = string.Format("1.00 <= {0} < 1.33", pkTitle);
            lblClass05.Text = string.Format("0.67 <= {0} < 1.00", pkTitle);
            lblClass06.Text = string.Format("0.00 <= {0} < 0.67", pkTitle);

            lblClass01.ForeColor = colorLabelDefault;
            lblClass02.ForeColor = colorLabelDefault;
            lblClass03.ForeColor = colorLabelDefault;
            lblClass04.ForeColor = colorLabelDefault;
            lblClass05.ForeColor = colorLabelDefault;
            lblClass06.ForeColor = colorLabelDefault;

            lblClassValue01.ForeColor = colorLabelDefault;
            lblClassValue02.ForeColor = colorLabelDefault;
            lblClassValue03.ForeColor = colorLabelDefault;
            lblClassValue04.ForeColor = colorLabelDefault;
            lblClassValue05.ForeColor = colorLabelDefault;
            lblClassValue06.ForeColor = colorLabelDefault;
        }
        /// <summary>
        /// 공정능력 판정 설정
        /// </summary>
        /// <param name="selectedIndex"></param>
        private void JudgmentSetting(int selectedIndex)
        {
            
            string result = "";
            string fieldID = "";

            switch (selectedIndex)
            {
                case 0://CPK
                    this.ControlJudgmentInitialize("Cpk");
                    fieldID = "SPCJUDGMENTCPK";
                    this.lblJudgmentValue.Text = ucCpk1.lblCpkValue.Text;
                    break;
                case 1://PPK
                    this.ControlJudgmentInitialize("Ppk");
                    fieldID = "SPCJUDGMENTPPK";
                    this.lblJudgmentValue.Text = ucCpk1.lblPpkValue.Text;
                    break;
            }

            try
            {
                if (_controlSpec != null && _controlSpec.sigmaResult != null && _controlSpec.sigmaResult.dtCpkResult != null)
                {
                    if (_controlSpec.sigmaResult.dtCpkResult.Rows.Count > 0)
                    {
                        var dtList = _controlSpec.sigmaResult.dtCpkResult.AsEnumerable().AsParallel();
                        var query = from r in dtList
                                    where r.Field<string>("FIELDID") == fieldID
                                    select new
                                    {
                                        judgmentValue = r.Field<string>("FIELDDATA")
                                    };
                        foreach (var rw in query)
                        {
                            result = rw.judgmentValue;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

           
            Color colorLabelFore = Color.Gray;
            switch (result)
            {
                case "Terrific":
                    colorLabelFore = Color.DarkViolet;
                    this.chkClass01.Properties.Appearance.BackColor = colorLabelFore;
                    this.chkClass01.Checked = true;
                    this.lblClass01.ForeColor = colorLabelFore;
                    this.lblClassValue01.ForeColor = colorLabelFore;

                    break;
                case "Excellent":
                    colorLabelFore = Color.Blue;
                    this.chkClass02.Properties.Appearance.BackColor = colorLabelFore;
                    this.chkClass02.Checked = true;
                    this.lblClass02.ForeColor = colorLabelFore;
                    this.lblClassValue02.ForeColor = colorLabelFore;
                    break;
                case "Good":
                    colorLabelFore = Color.Green;
                    this.chkClass03.Properties.Appearance.BackColor = colorLabelFore;
                    this.chkClass03.Checked = true;
                    this.lblClass03.ForeColor = colorLabelFore;
                    this.lblClassValue03.ForeColor = colorLabelFore;
                    break;
                case "Fair":
                    colorLabelFore = Color.DarkGoldenrod;
                    this.chkClass04.Properties.Appearance.BackColor = colorLabelFore;
                    this.chkClass04.Checked = true;
                    this.lblClass04.ForeColor = colorLabelFore;
                    this.lblClassValue04.ForeColor = colorLabelFore;
                    break;
                case "Poor":
                    colorLabelFore = Color.Orange;
                    this.chkClass05.Properties.Appearance.BackColor = colorLabelFore;
                    this.chkClass05.Checked = true;
                    this.lblClass05.ForeColor = colorLabelFore;
                    this.lblClassValue05.ForeColor = colorLabelFore;
                    break;
                case "Terrible":
                    colorLabelFore = Color.Red;
                    this.chkClass06.Properties.Appearance.BackColor = colorLabelFore;
                    this.chkClass06.Checked = true;
                    this.lblClass06.ForeColor = colorLabelFore;
                    this.lblClassValue06.ForeColor = colorLabelFore;
                    break;
            }
        }
        /// <summary>
        /// Grid에 표시자료 편집 - 관측자료.
        /// </summary>
        public void GridDataViewPpmObserve()
        {
            string viewData = "";
            if (_controlSpec!= null && _controlSpec.sigmaResult != null && _controlSpec.sigmaResult.cpkResult != null)
            {
            }
            else
            {
                return;
            }

            DataTable dtppmObserve = CreateDataTable("dtppmObserve");

            DataRow dataRow1 = dtppmObserve.NewRow();
            viewData = _controlSpec.sigmaResult.cpkResult.ppmObserveLSL.ToSafeRoundString();
            dataRow1["TITLE"] = _titleSpcPpmLsl; //"PPM < 규격 하한"
            dataRow1["NVALUE"] = viewData;
            dataRow1["NVALUEDETAIL"] = _controlSpec.sigmaResult.cpkResult.ppmObserveLSL.ToSafeDoubleNaN().ToString();
            dtppmObserve.Rows.Add(dataRow1);

            DataRow dataRow2 = dtppmObserve.NewRow();
            viewData = _controlSpec.sigmaResult.cpkResult.ppmObserveUSL.ToSafeRoundString();
            dataRow2["TITLE"] = _titleSpcPpmUsl; //"PPM > 규격 상한"
            dataRow2["NVALUE"] = viewData;
            dataRow2["NVALUEDETAIL"] = _controlSpec.sigmaResult.cpkResult.ppmObserveUSL.ToSafeDoubleNaN().ToString();
            dtppmObserve.Rows.Add(dataRow2);

            DataRow dataRow3 = dtppmObserve.NewRow();
            viewData = _controlSpec.sigmaResult.cpkResult.ppmObserveTOT.ToSafeRoundString();
            dataRow3["TITLE"] = _titleSpcPpmTotal; //"PPM 총계"
            dataRow3["NVALUE"] = viewData;
            dataRow3["NVALUEDETAIL"] = _controlSpec.sigmaResult.cpkResult.ppmObserveTOT.ToSafeDoubleNaN().ToString();
            dtppmObserve.Rows.Add(dataRow3);

            this.grdPpmObserve.DataSource = dtppmObserve;
        }
        /// <summary>
        /// Grid에 표시자료 편집 - 기대 성능(군내).
        /// </summary>
        public void GridDataViewPpmWithin()
        {
            string viewData = "";
            if (_controlSpec != null && _controlSpec.sigmaResult != null && _controlSpec.sigmaResult.cpkResult != null)
            {
            }
            else
            {
                return;
            }

            DataTable dtppmWithin = CreateDataTable("dtppmWithin");

            DataRow dataRow1 = dtppmWithin.NewRow();
            viewData = _controlSpec.sigmaResult.cpkResult.ppmWithinLSL.ToSafeRoundString();
            dataRow1["TITLE"] = _titleSpcPpmLsl; //"PPM < 규격 하한"
            dataRow1["NVALUE"] = viewData;
            dataRow1["NVALUEDETAIL"] = _controlSpec.sigmaResult.cpkResult.ppmWithinLSL.ToSafeDoubleNaN().ToString();
            dtppmWithin.Rows.Add(dataRow1);

            DataRow dataRow2 = dtppmWithin.NewRow();
            viewData = _controlSpec.sigmaResult.cpkResult.ppmWithinUSL.ToSafeRoundString();
            dataRow2["TITLE"] = _titleSpcPpmUsl; //"PPM > 규격 상한"
            dataRow2["NVALUE"] = viewData;
            dataRow2["NVALUEDETAIL"] = _controlSpec.sigmaResult.cpkResult.ppmWithinUSL.ToSafeDoubleNaN().ToString();
            dtppmWithin.Rows.Add(dataRow2);

            DataRow dataRow3 = dtppmWithin.NewRow();
            viewData = _controlSpec.sigmaResult.cpkResult.ppmWithinTOT.ToSafeRoundString();
            dataRow3["TITLE"] = _titleSpcPpmTotal; //"PPM 총계"
            dataRow3["NVALUE"] = viewData;
            dataRow3["NVALUEDETAIL"] = _controlSpec.sigmaResult.cpkResult.ppmWithinTOT.ToSafeDoubleNaN().ToString();
            dtppmWithin.Rows.Add(dataRow3);

            this.grdPpmWithin.DataSource = dtppmWithin;
        }

        /// <summary>
        /// Grid에 표시자료 편집 - 기대 성능(전체).
        /// </summary>
        public void GridDataViewPpmOverall()
        {
            string viewData = "";
            if (_controlSpec != null && _controlSpec.sigmaResult != null && _controlSpec.sigmaResult.cpkResult != null)
            {
            }
            else
            {
                return;
            }

            DataTable dtppmOverall = CreateDataTable("dtppmOverall");

            DataRow dataRow1 = dtppmOverall.NewRow();
            viewData = _controlSpec.sigmaResult.cpkResult.ppmOverallLSL.ToSafeRoundString();
            dataRow1["TITLE"] = _titleSpcPpmLsl; //"PPM < 규격 하한"
            dataRow1["NVALUE"] = viewData;
            dataRow1["NVALUEDETAIL"] = _controlSpec.sigmaResult.cpkResult.ppmOverallLSL.ToSafeDoubleNaN().ToString();
            dtppmOverall.Rows.Add(dataRow1);

            DataRow dataRow2 = dtppmOverall.NewRow();
            viewData = _controlSpec.sigmaResult.cpkResult.ppmOverallUSL.ToSafeRoundString();
            dataRow2["TITLE"] = _titleSpcPpmUsl; //"PPM > 규격 상한"
            dataRow2["NVALUE"] = viewData;
            dataRow2["NVALUEDETAIL"] = _controlSpec.sigmaResult.cpkResult.ppmOverallUSL.ToSafeDoubleNaN().ToString();
            dtppmOverall.Rows.Add(dataRow2);

            DataRow dataRow3 = dtppmOverall.NewRow();
            viewData = _controlSpec.sigmaResult.cpkResult.ppmOverallTOT.ToSafeRoundString();
            dataRow3["TITLE"] = _titleSpcPpmTotal; //"PPM 총계"
            dataRow3["NVALUE"] = viewData;
            dataRow3["NVALUEDETAIL"] = _controlSpec.sigmaResult.cpkResult.ppmOverallTOT.ToSafeDoubleNaN().ToString();
            dtppmOverall.Rows.Add(dataRow3);

            this.grdPpmOverall.DataSource = dtppmOverall;
        }
        /// <summary>
        /// data table
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataTable CreateDataTable(string tableName = "dtppmObserve")
        {
            DataTable dt = new DataTable();
            dt.TableName = tableName;
            //Filed
            dt.Columns.Add("TITLE", typeof(string));
            dt.Columns.Add("NVALUE", typeof(string));
            dt.Columns.Add("NVALUEDETAIL", typeof(string));
            return dt;
        }


        #endregion


    }
}
