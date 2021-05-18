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
using DevExpress.XtraGrid.Views.Grid;

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
    public partial class ucLabelViewer2 : UserControl
    {
        public delegate void OnPropertyGridDataChangeEventHandler(ucLabelViewer2 viewer, string fieldName, string value);
        public event OnPropertyGridDataChangeEventHandler OnPropertyGridDataChange;

        private LabelInfo labelInfo;


        public Dictionary<string, object> GetConditions
        {
            get
            {


                var controls = this.smartPropertyGrid1.SelectedObjects;

                Micube.SmartMES.Commons.Controls.Conditionals control = controls[0] as Micube.SmartMES.Commons.Controls.Conditionals;


                return control.dynamicProperties as Dictionary<string, object>;


            }
        }


        public List<BaseRow> GetPropertyGridData
        {
            get
            {
                if (this.smartPropertyGrid1.Rows.Count == 0)
                    return null;

                return this.smartPropertyGrid1.Rows[0].ChildRows.ToList();
            }
        }

        public LabelInfo LabelInfo { get => labelInfo; set => labelInfo = value; }

        public ucLabelViewer2()
        {
            InitializeComponent();

            this.smartPropertyGrid1.OptionsMenu.EnableContextMenu = false;



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
            this.smartPropertyGrid1.CellValueChanging += SmartPropertyGrid1_CellValueChanging;
        }

        private void SmartPropertyGrid1_CellValueChanging(object sender, DevExpress.XtraVerticalGrid.Events.CellValueChangedEventArgs e)
        {
            BeginInvoke(new Action(delegate
            {
                var grid = sender as SmartPropertyGrid;
                grid.PostEditor();
            }));
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

            if (row != null)
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
                attributes.Add(new DisplayNameAttribute(Language.Get(keyValue.Key) + "(" + keyValue.Key + ")"));
                DynamicPropertyDescriptor property = new DynamicPropertyDescriptor(cd, keyValue.Key, typeof(string), attributes.ToArray());

                list.Add(property);
            }

            PropertyDescriptor[] result = new PropertyDescriptor[list.Count];
            list.ToArray().CopyTo(result, 0);

            e.Properties = new PropertyDescriptorCollection(result);



        }

        public void SetBindingCombo(string columnName, string[] comboDatas, DevExpress.XtraEditors.Controls.TextEditStyles textEditStyles = DevExpress.XtraEditors.Controls.TextEditStyles.Standard)
        {
            BaseRow row = smartPropertyGrid1.Rows[0].ChildRows.AsEnumerable().Where(s => s.Name.Equals(string.Format("row{0}", columnName))).FirstOrDefault();

            if (row != null)
            {

                RepositoryItemComboBox combo = new RepositoryItemComboBox();
                combo.Items.AddRange(comboDatas);
                combo.TextEditStyle = textEditStyles;
                row.Properties.RowEdit = combo;
            }
        }





        #region ◆ Event |

        private void UcLabelViewer_Load(object sender, EventArgs e)
        {


            Font font = new Font("Tahoma", 11);
            var cd = this.smartPropertyGrid1.SelectedObject as Conditionals;

            this.smartPropertyGrid1.Rows[0].ChildRows.ForEach(delegate (BaseRow row)
            {
                if (cd.controlsVisibles.ContainsKey(row.Name))
                    row.Visible = cd.controlsVisibles[row.Name];
                row.Appearance.Font = font;
            });


            //Combo Binding Sample 소스
            //List<string> aa = new List<string>();
            //aa.Add("String 1");
            //aa.Add("String 2");
            //aa.Add("String 3");
            //aa.Add("String 4");

            //SetBindingCombo("BOXNO", aa.ToArray(), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
       


            this.smartPropertyGrid1.Appearance.RowHeaderPanel.Font = font;
            this.smartPropertyGrid1.BestFit();

            this.smartPropertyGrid1.RowHeaderWidth = this.smartPropertyGrid1.RowHeaderWidth + 5;

        }

        #endregion

        #region ◆ Public Event Handler |

        #endregion

        #region ◆ Function |

        public void SetBindingPropertyGrid(DataTable dtLabelData)
        {

            Conditionals cd = new Conditionals();

            if (dtLabelData != null)

                foreach (DataColumn dc in dtLabelData.Columns)
                {
                    DataRow dr = dtLabelData.Rows[0];
                    cd.dynamicProperties.Add(dc.Caption, dr[dc].ToString());
                };


            smartPropertyGrid1.SelectedObject = cd;

        }


        #endregion

        private void propertyGridControl1_CellValueChanged(object sender, DevExpress.XtraVerticalGrid.Events.CellValueChangedEventArgs e)
        {

        }
    }




}
