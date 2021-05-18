#region using
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons;
using DevExpress.XtraReports.UI;
using DevExpress.XtraVerticalGrid.Rows;
using DevExpress.XtraEditors.Repository;
using System.Collections;
using System.Reflection;

#endregion

namespace Micube.SmartMES.Commons.Controls
{
    /// <summary>
    /// 프 로 그 램 명  : 라벨 출력 미리 보기 기능
    /// 업  무  설  명  : 
    /// 생    성    자  : 박윤신
    /// 생    성    일  : 2019-07-25
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ucLabelViewer : UserControl
    {
        public delegate void OnPropertyGridDataChangeEventHandler(ucLabelViewer viewer, string fieldName, string value);
        public event OnPropertyGridDataChangeEventHandler OnPropertyGridDataChange;

        private DataTable labelDataTable;

        public DataTable LabelDataTable { get => labelDataTable; set => labelDataTable = value; }

        private bool propertyGridVisible;
        public bool PropertyGridVisible
        {
            get
            {
                return propertyGridVisible;
            }
            set
            {
                propertyGridVisible = value;
                if (value)
                    this.smartSpliterContainer1.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Both;
                else
                    this.smartSpliterContainer1.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1;
            }

        }
       
        public ucLabelViewer()
        {
            InitializeComponent();

            this.smartPropertyGrid1.OptionsMenu.EnableContextMenu = false;
            
            this.propertyGridVisible = true;

            this.labelDataTable = null;

            if (!this.IsDesignMode())
            {
                InitializeEvent();
            }

        }
        private void InitializeEvent()
        {
            this.Load += UcLabelViewer_Load;
            this.smartPropertyGrid1.CustomPropertyDescriptors += SmartPropertyGrid1_CustomPropertyDescriptors;
            this.smartPropertyGrid1.CellValueChanged += SmartPropertyGrid1_CellValueChanged;

         

        }

        private void SmartPropertyGrid1_CellValueChanged(object sender, DevExpress.XtraVerticalGrid.Events.CellValueChangedEventArgs e)
        {
            if (OnPropertyGridDataChange != null)
                OnPropertyGridDataChange(this, e.Row.Name, e.Value.ToString());
        }

        public void SetCellValue(string columnName, string value)
        {
            this.smartPropertyGrid1.CellValueChanged -= SmartPropertyGrid1_CellValueChanged;

            var controls = this.smartPropertyGrid1.SelectedObjects;

            Micube.SmartMES.Commons.Controls.Conditionals control = controls[0] as Micube.SmartMES.Commons.Controls.Conditionals;



            BaseRow row = smartPropertyGrid1.Rows[0].ChildRows.AsEnumerable().Where(s => s.Name.Equals(string.Format("row{0}", columnName))).FirstOrDefault();

            if(row != null)
            {

                smartPropertyGrid1.SetCellValue(row, 0, value);

            }

            this.smartPropertyGrid1.CellValueChanged += SmartPropertyGrid1_CellValueChanged;
        }


        public string GetCellValue(string columnName)
        {
            var controls = this.smartPropertyGrid1.SelectedObjects;

            Micube.SmartMES.Commons.Controls.Conditionals control = controls[0] as Micube.SmartMES.Commons.Controls.Conditionals;

            if (!control.dynamicProperties.ContainsKey(columnName))
                return null;
            return control.dynamicProperties[columnName].ToString();

        }


        private void SmartPropertyGrid1_CustomPropertyDescriptors(object sender, DevExpress.XtraVerticalGrid.Events.CustomPropertyDescriptorsEventArgs e)
        {

            PropertyDescriptorCollection properties = e.Properties;
            ArrayList list = new ArrayList(properties);
            Conditionals cd = e.Source as Conditionals;

            foreach (KeyValuePair<string, object> keyValue in cd.dynamicProperties)
            {
                List<Attribute> attributes = new List<Attribute>();
                //attributes.Add(new DisplayNameAttribute(Language.Get(keyValue.Key)));
                attributes.Add(new DisplayNameAttribute(Language.Get(keyValue.Key) + "(" + keyValue.Key + ")"));
                //     attributes.Add(new BrowsableAttribute(false));
                DynamicPropertyDescriptor property = new DynamicPropertyDescriptor(cd, keyValue.Key, typeof(string), attributes.ToArray());

           //     BrowsableAttribute attrib =
           //(BrowsableAttribute)property.Attributes[typeof(BrowsableAttribute)];
           //     FieldInfo isBrow =
           //       attrib.GetType().GetField("browsable", BindingFlags.NonPublic | BindingFlags.Instance);
           //     isBrow.SetValue(attrib, false);

                list.Add(property);
            }

            PropertyDescriptor[] result = new PropertyDescriptor[list.Count];
            list.ToArray().CopyTo(result, 0);

            e.Properties = new PropertyDescriptorCollection(result);
             XtraReport report = this.documentViewer1.DocumentSource as XtraReport;

            Band detailBand = report.Bands.GetBandByType(typeof(DetailBand));

            //detailBand.Controls
            foreach (XRControl control in detailBand.Controls)
            {
                if (control is DevExpress.XtraReports.UI.XRLabel || control is DevExpress.XtraReports.UI.XRBarCode)
                {
                    if (!string.IsNullOrEmpty(control.Tag.ToString()))
                    {
                        control.Text = cd.dynamicProperties[control.Tag.ToString()].ToString();
                    }
                }

                else if (control is DevExpress.XtraReports.UI.XRTable)
                {
                    XRTable xt = control as XRTable;

                    foreach (XRTableRow tr in xt.Rows)
                    {
                        for (int i = 0; i < tr.Cells.Count; i++)
                        {
                            if (!string.IsNullOrEmpty(tr.Cells[i].Tag.ToString()))
                            {
                                tr.Cells[i].Text = cd.dynamicProperties[tr.Cells[i].Tag.ToString()].ToString();
                            }
                        }
                    }
                }
            }

            report.CreateDocument();
            this.documentViewer1.DocumentSource = report;

            this.documentViewer1.PrintingSystem.ExecCommand(DevExpress.XtraPrinting.PrintingSystemCommand.ZoomToPageWidth);


    

        }




        #region ◆ Event |

        private void UcLabelViewer_Load(object sender, EventArgs e)
        {
            //var cd = this.smartPropertyGrid1.SelectedObject as Conditionals;

            //this.smartPropertyGrid1.Rows[0].ChildRows.ForEach(delegate (BaseRow row)
            //{
            //    if (cd.controlsVisibles.ContainsKey(row.Name))
            //        row.Visible = cd.controlsVisibles[row.Name];
            //});

        }

            #endregion

        #region ◆ Public Event Handler |

        #endregion

        #region ◆ Function |

    public void SetBindingPreview(XtraReport report)
    {

        report.CreateDocument();
        this.documentViewer1.DocumentSource = report;
        this.documentViewer1.Zoom = 0.5f;

        this.documentViewer1.PrintingSystem.ExecCommand(DevExpress.XtraPrinting.PrintingSystemCommand.ZoomToPageWidth);


        Band topBand = report.Bands.GetBandByType(typeof(TopMarginBand));
        Band bottomBand = report.Bands.GetBandByType(typeof(BottomMarginBand));

        if (topBand != null) report.Bands.Remove(topBand);
        if (bottomBand != null) report.Bands.Remove(bottomBand);

        Band detailBand = report.Bands.GetBandByType(typeof(DetailBand));


        Conditionals cd = new Conditionals();
        //detailBand.Controls
        foreach (XRControl control in detailBand.Controls)
        {
            if (control is DevExpress.XtraReports.UI.XRLabel)
            {
                if (!string.IsNullOrEmpty(control.Tag.ToString()))
                {
                    if (!cd.dynamicProperties.ContainsKey(control.Tag.ToString()))
                    {
                        cd.dynamicProperties.Add(control.Tag.ToString(), control.Text);
                    //    cd.controlsVisibles.Add(string.Format("row{0}", control.Tag.ToString()), control.Visible);
                    }
                }


            }
            else if (control is DevExpress.XtraReports.UI.XRBarCode)
            {
                if (!string.IsNullOrEmpty(control.Tag.ToString()))
                {
                    if (!cd.dynamicProperties.ContainsKey(control.Tag.ToString()))
                    {
                        cd.dynamicProperties.Add(control.Tag.ToString(), control.Text);
                    //     cd.controlsVisibles.Add(string.Format("row{0}", control.Tag.ToString()), control.Visible);
                    }
                }

            }


            else if (control is DevExpress.XtraReports.UI.XRTable)
            {
                XRTable xt = control as XRTable;

                foreach (XRTableRow tr in xt.Rows)
                {
                    for (int i = 0; i < tr.Cells.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(tr.Cells[i].Tag.ToString()))
                        {
                            if (!cd.dynamicProperties.ContainsKey(control.Tag.ToString()))
                            {
                                cd.dynamicProperties.Add(tr.Cells[i].Tag.ToString(), tr.Cells[i].Text);
                            //     cd.controlsVisibles.Add(string.Format("row{0}", control.Tag.ToString()), control.Visible);
                            }
                        }
                    }
                }
            }
        }


        smartPropertyGrid1.SelectedObject = cd;

    }

    public XtraReport GetLabelReport()
    {
        return this.documentViewer1.DocumentSource as XtraReport;
    }

    #endregion

        private void propertyGridControl1_CellValueChanged(object sender, DevExpress.XtraVerticalGrid.Events.CellValueChangedEventArgs e)
        {

        }
    }


}
