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

namespace Micube.SmartMES.StandardInfo
{
    public partial class ProductSpecificationPlatingSpec_YPE : UserControl
    {
        DataTable dtProductDEF = new DataTable();
        DataTable dtProcess = new DataTable();
        string processid = "";

        public DataTable DataSource
        {

            get; private set;
        }
        public ProductSpecificationPlatingSpec_YPE()
        {

            dtProductDEF.Columns.Add("ITEMID");
            dtProductDEF.Columns.Add("ITEMVERSION");
            DataRow dr = dtProductDEF.NewRow();
            dr["ITEMID"] = "*";
            dr["ITEMVERSION"] = "*";
            dtProductDEF.Rows.Add(dr);
            InitializeComponent();
            InitializeComboControl();
            InitializeTextControl();

        }

        private void InitializeComboControl()
        {

        }

        /// <summary>
        /// Combo를 선택했을 때 필수 체크 표시
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBoxValidation_EditValueChanged(object sender, EventArgs e)
        {
            SmartComboBox cmb = sender as SmartComboBox;
            string strName = cmb.Name.Substring(2);

            var lableControls = this.Controls.Find<SmartLabel>(true);

            if (string.IsNullOrEmpty(cmb.GetDataValue().ToString()))
            {
                foreach (SmartLabel lb in lableControls)
                {
                    if (lb.Name.StartsWith("lb" + strName))
                        lb.ForeColor = Color.Black;
                }

                // text 값 초기화
                var txtControls = this.Controls.Find<SmartTextBox>(true);
                foreach (SmartTextBox tb in txtControls)
                {
                    if (tb.Name.StartsWith("tb" + strName))
                        tb.EditValue = string.Empty;
                }
            }
            else
            {
                foreach (SmartLabel lb in lableControls)
                {
                    if (lb.Name.StartsWith("lb" + strName))
                        lb.ForeColor = Color.Red;
                }
            }
        }

        private void InitializeTextControl()
        {
            // VILFILL 팝업 1_1
            ConditionItemSelectPopup viaFill1_1 = new ConditionItemSelectPopup();
            viaFill1_1.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            viaFill1_1.SetPopupLayout("PROCESSSEGMENTID", PopupButtonStyles.Ok_Cancel);
            viaFill1_1.Id = "PROCESSSEGMENTID";
            viaFill1_1.LabelText = "PROCESSSEGMENTID";
            viaFill1_1.SearchQuery = new SqlQuery("GetPlatingTypeSegment_YPE", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"PLANTID={UserInfo.Current.Plant}",
                        $"RESOURCEID={dtProductDEF.Rows[0]["ITEMID"]}", $"RESOURCEVERSION={dtProductDEF.Rows[0]["ITEMVERSION"]}");
            viaFill1_1.IsMultiGrid = false;
            viaFill1_1.DisplayFieldName = "PROCESSSEGMENTNAME";
            viaFill1_1.ValueFieldName = "PROCESSSEGMENTID";
            viaFill1_1.LanguageKey = "PROCESSSEGMENTNAME";
            viaFill1_1.Conditions.AddTextBox("PROCESSSEGMENTIDNAME");
            viaFill1_1.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150);
            viaFill1_1.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
            popviafill1_1.SelectPopupCondition = viaFill1_1;
            popviafill1_1.SelectPopupCondition.SetPopupApplySelection((selectRow, gridRow) => {


                selectRow.AsEnumerable().ForEach(r => {

                    processid = Format.GetString(r["PROCESSSEGMENTID"]);


                });
            });


            // VILFILL 팝업 1_2
            ConditionItemSelectPopup viaFill1_2 = new ConditionItemSelectPopup();
            viaFill1_2.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            viaFill1_2.SetPopupLayout("PROCESSSEGMENTID", PopupButtonStyles.Ok_Cancel);
            viaFill1_2.Id = "PROCESSSEGMENTID";
            viaFill1_2.LabelText = "PROCESSSEGMENTID";
            viaFill1_2.SearchQuery = new SqlQuery("GetPlatingTypeSegment_YPE", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"PLANTID={UserInfo.Current.Plant}",
                        $"RESOURCEID={dtProductDEF.Rows[0]["ITEMID"]}", $"RESOURCEVERSION={dtProductDEF.Rows[0]["ITEMVERSION"]}");
            viaFill1_2.IsMultiGrid = false;
            viaFill1_2.DisplayFieldName = "PROCESSSEGMENTNAME";
            viaFill1_2.ValueFieldName = "PROCESSSEGMENTID";
            viaFill1_2.LanguageKey = "PROCESSSEGMENTNAME";
            viaFill1_2.Conditions.AddTextBox("PROCESSSEGMENTIDNAME");
            viaFill1_2.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150);
            viaFill1_2.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
            popviafill1_2.SelectPopupCondition = viaFill1_2;
            popviafill1_2.SelectPopupCondition.SetPopupApplySelection((selectRow, gridRow) => {


                selectRow.AsEnumerable().ForEach(r => {

                    processid = Format.GetString(r["PROCESSSEGMENTID"]);


                });
            });


            // VILFILL 팝업 2_1
            ConditionItemSelectPopup viaFill2_1 = new ConditionItemSelectPopup();
            viaFill2_1.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            viaFill2_1.SetPopupLayout("PROCESSSEGMENTID", PopupButtonStyles.Ok_Cancel);
            viaFill2_1.Id = "PROCESSSEGMENTID";
            viaFill2_1.LabelText = "PROCESSSEGMENTID";
            viaFill2_1.SearchQuery = new SqlQuery("GetPlatingTypeSegment_YPE", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"PLANTID={UserInfo.Current.Plant}",
                        $"RESOURCEID={dtProductDEF.Rows[0]["ITEMID"]}", $"RESOURCEVERSION={dtProductDEF.Rows[0]["ITEMVERSION"]}");
            viaFill2_1.IsMultiGrid = false;
            viaFill2_1.DisplayFieldName = "PROCESSSEGMENTNAME";
            viaFill2_1.ValueFieldName = "PROCESSSEGMENTID";
            viaFill2_1.LanguageKey = "PROCESSSEGMENTNAME";
            viaFill2_1.Conditions.AddTextBox("PROCESSSEGMENTIDNAME");
            viaFill2_1.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150);
            viaFill2_1.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
            popviafill2_1.SelectPopupCondition = viaFill2_1;
            popviafill2_1.SelectPopupCondition.SetPopupApplySelection((selectRow, gridRow) => {


                selectRow.AsEnumerable().ForEach(r => {

                    processid = Format.GetString(r["PROCESSSEGMENTID"]);


                });
            });


            // VILFILL 팝업 2_2
            ConditionItemSelectPopup viaFill2_2 = new ConditionItemSelectPopup();
            viaFill2_2.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            viaFill2_2.SetPopupLayout("PROCESSSEGMENTID", PopupButtonStyles.Ok_Cancel);
            viaFill2_2.Id = "PROCESSSEGMENTID";
            viaFill2_2.LabelText = "PROCESSSEGMENTID";
            viaFill2_2.SearchQuery = new SqlQuery("GetPlatingTypeSegment_YPE", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"PLANTID={UserInfo.Current.Plant}",
                        $"RESOURCEID={dtProductDEF.Rows[0]["ITEMID"]}", $"RESOURCEVERSION={dtProductDEF.Rows[0]["ITEMVERSION"]}");
            viaFill2_2.IsMultiGrid = false;
            viaFill2_2.DisplayFieldName = "PROCESSSEGMENTNAME";
            viaFill2_2.ValueFieldName = "PROCESSSEGMENTID";
            viaFill2_2.LanguageKey = "PROCESSSEGMENTNAME";
            viaFill2_2.Conditions.AddTextBox("PROCESSSEGMENTIDNAME");
            viaFill2_2.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150);
            viaFill2_2.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
            popviafill2_2.SelectPopupCondition = viaFill2_2;
            popviafill2_2.SelectPopupCondition.SetPopupApplySelection((selectRow, gridRow) => {


                selectRow.AsEnumerable().ForEach(r => {

                    processid = Format.GetString(r["PROCESSSEGMENTID"]);


                });
            });


            // VILFILL 팝업 3_1
            ConditionItemSelectPopup viaFill3_1 = new ConditionItemSelectPopup();
            viaFill3_1.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            viaFill3_1.SetPopupLayout("PROCESSSEGMENTID", PopupButtonStyles.Ok_Cancel);
            viaFill3_1.Id = "PROCESSSEGMENTID";
            viaFill3_1.LabelText = "PROCESSSEGMENTID";
            viaFill3_1.SearchQuery = new SqlQuery("GetPlatingTypeSegment_YPE", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"PLANTID={UserInfo.Current.Plant}",
                        $"RESOURCEID={dtProductDEF.Rows[0]["ITEMID"]}", $"RESOURCEVERSION={dtProductDEF.Rows[0]["ITEMVERSION"]}");
            viaFill3_1.IsMultiGrid = false;
            viaFill3_1.DisplayFieldName = "PROCESSSEGMENTNAME";
            viaFill3_1.ValueFieldName = "PROCESSSEGMENTID";
            viaFill3_1.LanguageKey = "PROCESSSEGMENTNAME";
            viaFill3_1.Conditions.AddTextBox("PROCESSSEGMENTIDNAME");
            viaFill3_1.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150);
            viaFill3_1.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
            popviafill3_1.SelectPopupCondition = viaFill3_1;
            popviafill3_1.SelectPopupCondition.SetPopupApplySelection((selectRow, gridRow) => {


                selectRow.AsEnumerable().ForEach(r => {

                    processid = Format.GetString(r["PROCESSSEGMENTID"]);


                });
            });


            // VILFILL 팝업 3_2
            ConditionItemSelectPopup viaFill3_2 = new ConditionItemSelectPopup();
            viaFill3_2.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            viaFill3_2.SetPopupLayout("PROCESSSEGMENTID", PopupButtonStyles.Ok_Cancel);
            viaFill3_2.Id = "PROCESSSEGMENTID";
            viaFill3_2.LabelText = "PROCESSSEGMENTID";
            viaFill3_2.SearchQuery = new SqlQuery("GetPlatingTypeSegment_YPE", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"PLANTID={UserInfo.Current.Plant}",
                        $"RESOURCEID={dtProductDEF.Rows[0]["ITEMID"]}", $"RESOURCEVERSION={dtProductDEF.Rows[0]["ITEMVERSION"]}");
            viaFill3_2.IsMultiGrid = false;
            viaFill3_2.DisplayFieldName = "PROCESSSEGMENTNAME";
            viaFill3_2.ValueFieldName = "PROCESSSEGMENTID";
            viaFill3_2.LanguageKey = "PROCESSSEGMENTNAME";
            viaFill3_2.Conditions.AddTextBox("PROCESSSEGMENTIDNAME");
            viaFill3_2.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150);
            viaFill3_2.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
            popviafill3_2.SelectPopupCondition = viaFill3_2;
            popviafill3_2.SelectPopupCondition.SetPopupApplySelection((selectRow, gridRow) => {


                selectRow.AsEnumerable().ForEach(r => {

                    processid = Format.GetString(r["PROCESSSEGMENTID"]);


                });
            });


        }
        private void SetSmartComboBox(SmartComboBox comboBox)
        {
            comboBox.DisplayMember = "CODENAME";
            comboBox.ValueMember = "CODEID";
            comboBox.ShowHeader = false;
            comboBox.UseEmptyItem = true;
            comboBox.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
        }

        /// <summary>
        /// 조회 
        /// </summary>
        /// <param name="dt"></param>
        public void DataBind(DataTable dt)
        {
            int count = dt.Rows.Count + 1;

            // 동도금 정보
            for (int i = 1; i <= dt.Rows.Count; i++)
            {

                if (i == 1)
                {
                    processid = dt.Rows[0]["PROCESSSEGMENTID"].ToString();
                    popviafill1_1.SetValue(dt.Rows[0]["PROCESSSEGMENTID"]);
                    popviafill1_1.EditValue = dt.Rows[0]["DESCRIPTION"];


                }
                else if (i == 2)
                {
                    processid = dt.Rows[1]["PROCESSSEGMENTID"].ToString();
                    popviafill1_2.SetValue(dt.Rows[1]["PROCESSSEGMENTID"]);
                    popviafill1_2.EditValue = dt.Rows[1]["DESCRIPTION"];

                }
                else if (i == 3)
                {
                    processid = dt.Rows[2]["PROCESSSEGMENTID"].ToString();
                    popviafill2_1.SetValue(dt.Rows[2]["PROCESSSEGMENTID"]);
                    popviafill2_1.EditValue = dt.Rows[2]["DESCRIPTION"];

                }
                else if (i == 4)
                {
                    processid = dt.Rows[3]["PROCESSSEGMENTID"].ToString();
                    popviafill2_2.SetValue(dt.Rows[3]["PROCESSSEGMENTID"]);
                    popviafill2_2.EditValue = dt.Rows[3]["DESCRIPTION"];
                }
                else if (i == 5)
                {
                    processid = dt.Rows[4]["PROCESSSEGMENTID"].ToString();
                    popviafill3_1.SetValue(dt.Rows[4]["PROCESSSEGMENTID"]);
                    popviafill3_1.EditValue = dt.Rows[4]["DESCRIPTION"];
                }
                else if (i == 6)
                {
                    processid = dt.Rows[5]["PROCESSSEGMENTID"].ToString();
                    popviafill3_2.SetValue(dt.Rows[5]["PROCESSSEGMENTID"]);
                    popviafill3_2.EditValue = dt.Rows[5]["DESCRIPTION"];
                }
                else if (i == 7)
                {
                    txthole1cs.EditValue = dt.Rows[6]["SPECDETAILFROM"];
                    txthole1ss.EditValue = dt.Rows[6]["SPECDETAILTO"];

                }
                else if (i == 8)
                {
                    txthole2cs.EditValue = dt.Rows[7]["SPECDETAILFROM"];
                    txthole2ss.EditValue = dt.Rows[7]["SPECDETAILTO"];

                }

            }
        }






        /// <summary>
        /// 데이터 초기화
        /// </summary>
        public void ClearData()
        {
            //데이터 초기화
            CommonFunctionProductSpec.ClearData(smartSplitTableLayoutPanel1);
        }

        /// <summary>
        /// 저장 
        /// </summary>
        //public Dictionary<string, object> Save()
        public DataTable Save()
        {
            //ValidationCheck();
            DataTable dtRtn = new DataTable();
            dtRtn.Columns.Add("DETAILTYPE");
            dtRtn.Columns.Add("SEQUENCE");
            dtRtn.Columns.Add("DETAILNAME");
            dtRtn.Columns.Add("SPECDETAILFROM");
            dtRtn.Columns.Add("SPECDETAILTO");
            dtRtn.Columns.Add("FROMORIGINAL");
            dtRtn.Columns.Add("TOORIGINAL");
            dtRtn.Columns.Add("DESCRIPTION");



            // 동도금 정보
            for (int i = 1; i <= 8; i++)
            {
                DataRow drCopperPlating = dtRtn.NewRow();
                drCopperPlating["DETAILTYPE"] = "HolePlating";
                drCopperPlating["SEQUENCE"] = i.ToString();

                if (i == 1)
                {
                    drCopperPlating["DETAILNAME"] = "PLATINGSPEC" + i.ToString();
                    drCopperPlating["DESCRIPTION"] = popviafill1_1.GetValue();
                }
                else if (i == 2)
                {
                    drCopperPlating["DETAILNAME"] = "PLATINGSPEC" + i.ToString();
                    drCopperPlating["DESCRIPTION"] = popviafill1_2.GetValue();
                }
                else if (i == 3)
                {
                    drCopperPlating["DETAILNAME"] = "PLATINGSPEC" + i.ToString();
                    drCopperPlating["DESCRIPTION"] = popviafill2_1.GetValue();
                }
                else if (i == 4)
                {
                    drCopperPlating["DETAILNAME"] = "PLATINGSPEC" + i.ToString();
                    drCopperPlating["DESCRIPTION"] = popviafill2_2.GetValue();
                }
                else if (i == 5)
                {
                    drCopperPlating["DETAILNAME"] = "PLATINGSPEC" + i.ToString();
                    drCopperPlating["DESCRIPTION"] = popviafill3_1.GetValue();
                }
                else if (i == 6)
                {
                    drCopperPlating["DETAILNAME"] = "PLATINGSPEC" + i.ToString();
                    drCopperPlating["DESCRIPTION"] = popviafill3_2.GetValue();
                }
                else if (i == 7)
                {
                    drCopperPlating["DETAILNAME"] = "HOLEPLATING1";
                    drCopperPlating["SPECDETAILFROM"] = txthole1cs.EditValue;
                    drCopperPlating["SPECDETAILTO"] = txthole1ss.EditValue;
                }
                else if (i == 8)
                {
                    drCopperPlating["DETAILNAME"] = "HOLEPLATING2";
                    drCopperPlating["SPECDETAILFROM"] = txthole2cs.EditValue;
                    drCopperPlating["SPECDETAILTO"] = txthole2ss.EditValue;
                }
                dtRtn.Rows.Add(drCopperPlating);
            }
            return dtRtn;

        }


        public DataTable Specreturn()
        {

            DataSource = new DataTable();
            ReportTableReturn.GetLabelDataTable(smartSplitTableLayoutPanel1, DataSource);


            DataRow row = DataSource.NewRow();
            ReportTableReturn.GetDataRow(row, smartSplitTableLayoutPanel1);
            DataSource.Rows.Add(row);
            return DataSource;

        }
        public void parameterreturn(Dictionary<string, object> values)
        {
            dtProductDEF = new DataTable();

            dtProductDEF.Columns.Add("ITEMID");
            dtProductDEF.Columns.Add("ITEMVERSION");
            DataRow dr = dtProductDEF.NewRow();
            dr["ITEMID"] = values["ITEMID"];
            dr["ITEMVERSION"] = values["ITEMVERSION"];

            dtProductDEF.Rows.Add(dr);

            popviafill1_1.SelectPopupCondition.SearchQuery = new SqlQuery("GetPlatingTypeSegment_YPE", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}",
                        $"RESOURCEID={dtProductDEF.Rows[0]["ITEMID"]}", $"RESOURCEVERSION={dtProductDEF.Rows[0]["ITEMVERSION"]}", $"PROCESSSEGID=2514");
            popviafill1_1.SelectPopupCondition.SetPopupApplySelection((selectRow, gridRow) => {


                selectRow.AsEnumerable().ForEach(r => {

                    popviafill1_1.SetValue(Format.GetString(r["PROCESSSEGMENTID"]));
                    processid = Format.GetString(r["PROCESSSEGMENTID"]);

                });
            });
            popviafill1_2.SelectPopupCondition.SearchQuery = new SqlQuery("GetPlatingTypeSegment_YPE", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}",
                        $"RESOURCEID={dtProductDEF.Rows[0]["ITEMID"]}", $"RESOURCEVERSION={dtProductDEF.Rows[0]["ITEMVERSION"]}", $"PROCESSSEGID=2514");
            popviafill1_2.SelectPopupCondition.SetPopupApplySelection((selectRow, gridRow) => {


                selectRow.AsEnumerable().ForEach(r => {

                    popviafill1_2.SetValue(Format.GetString(r["PROCESSSEGMENTID"]));
                    processid = Format.GetString(r["PROCESSSEGMENTID"]);

                });
            });

            popviafill2_1.SelectPopupCondition.SearchQuery = new SqlQuery("GetPlatingTypeSegment_YPE", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}",
                        $"RESOURCEID={dtProductDEF.Rows[0]["ITEMID"]}", $"RESOURCEVERSION={dtProductDEF.Rows[0]["ITEMVERSION"]}", $"PROCESSSEGID=2514");
            popviafill2_1.SelectPopupCondition.SetPopupApplySelection((selectRow, gridRow) => {


                selectRow.AsEnumerable().ForEach(r => {

                    popviafill2_1.SetValue(Format.GetString(r["PROCESSSEGMENTID"]));
                    processid = Format.GetString(r["PROCESSSEGMENTID"]);

                });
            });
            popviafill2_2.SelectPopupCondition.SearchQuery = new SqlQuery("GetPlatingTypeSegment_YPE", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}",
                        $"RESOURCEID={dtProductDEF.Rows[0]["ITEMID"]}", $"RESOURCEVERSION={dtProductDEF.Rows[0]["ITEMVERSION"]}", $"PROCESSSEGID=2514");
            popviafill2_2.SelectPopupCondition.SetPopupApplySelection((selectRow, gridRow) => {


                selectRow.AsEnumerable().ForEach(r => {

                    popviafill2_2.SetValue(Format.GetString(r["PROCESSSEGMENTID"]));
                    processid = Format.GetString(r["PROCESSSEGMENTID"]);

                });
            });
            popviafill3_1.SelectPopupCondition.SearchQuery = new SqlQuery("GetPlatingTypeSegment_YPE", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}",
                        $"RESOURCEID={dtProductDEF.Rows[0]["ITEMID"]}", $"RESOURCEVERSION={dtProductDEF.Rows[0]["ITEMVERSION"]}", $"PROCESSSEGID=2514");
            popviafill3_1.SelectPopupCondition.SetPopupApplySelection((selectRow, gridRow) => {


                selectRow.AsEnumerable().ForEach(r => {

                    popviafill3_1.SetValue(Format.GetString(r["PROCESSSEGMENTID"]));
                    processid = Format.GetString(r["PROCESSSEGMENTID"]);

                });
            });
            popviafill3_2.SelectPopupCondition.SearchQuery = new SqlQuery("GetPlatingTypeSegment_YPE", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}",
                        $"RESOURCEID={dtProductDEF.Rows[0]["ITEMID"]}", $"RESOURCEVERSION={dtProductDEF.Rows[0]["ITEMVERSION"]}", $"PROCESSSEGID=2514");
            popviafill3_2.SelectPopupCondition.SetPopupApplySelection((selectRow, gridRow) => {


                selectRow.AsEnumerable().ForEach(r => {

                    popviafill3_2.SetValue(Format.GetString(r["PROCESSSEGMENTID"]));
                    processid = Format.GetString(r["PROCESSSEGMENTID"]);

                });
            });

            popviafill1_1.EditValueChanged += Popviafill1_1_EditValueChanged;
            popviafill1_2.EditValueChanged += Popviafill1_2_EditValueChanged;
            popviafill2_1.EditValueChanged += Popviafill2_1_EditValueChanged;
            popviafill2_2.EditValueChanged += Popviafill2_2_EditValueChanged;
            popviafill3_1.EditValueChanged += Popviafill3_1_EditValueChanged;
            popviafill3_2.EditValueChanged += Popviafill3_2_EditValueChanged;


        }

        private void Popviafill3_2_EditValueChanged(object sender, EventArgs e)
        {
            if (popviafill3_2.EditValue.Equals(""))
            {
                txtplatinglow3_2.EditValue = string.Empty;
                txtplatingstd3_2.EditValue = string.Empty;
                txtplatinghigh3_2.EditValue = string.Empty;
                txtholelow3_2.EditValue = string.Empty;
                txtholestd3_2.EditValue = string.Empty;
                txtholehigh3_2.EditValue = string.Empty;
                txttotallow3_2.EditValue = string.Empty;
                txttotalstd3_2.EditValue = string.Empty;
                txttotalhigh3_2.EditValue = string.Empty;
                txtdimplelow3_2.EditValue = string.Empty;
                txtdimplestd3_2.EditValue = string.Empty;
                txtdimplehigh3_2.EditValue = string.Empty;
                txtoverlow3_2.EditValue = string.Empty;
                txtoverstd3_2.EditValue = string.Empty;
                txtoverhigh3_2.EditValue = string.Empty;
                processid = "";
            }
            DataTable dtsemgment = imspitem(processid);
            foreach (DataRow dr in dtsemgment.Rows)
            {
                if (dr["INSPITEMID"].ToString().Equals("0375"))
                {
                    txtplatinglow3_2.EditValue = Format.GetString(dr["LSL"]);
                    txtplatingstd3_2.EditValue = Format.GetString(dr["SL"]);
                    txtplatinghigh3_2.EditValue = Format.GetString(dr["USL"]);
                }
                if (dr["INSPITEMID"].ToString().Equals("0376"))
                {
                    txtholelow3_2.EditValue = Format.GetString(dr["LSL"]);
                    txtholestd3_2.EditValue = Format.GetString(dr["SL"]);
                    txtholehigh3_2.EditValue = Format.GetString(dr["USL"]);
                }
                if (dr["INSPITEMID"].ToString().Equals("0377"))
                {
                    txttotallow3_2.EditValue = Format.GetString(dr["LSL"]);
                    txttotalstd3_2.EditValue = Format.GetString(dr["SL"]);
                    txttotalhigh3_2.EditValue = Format.GetString(dr["USL"]);
                }
                if (dr["INSPITEMID"].ToString().Equals("0367"))
                {
                    txtdimplelow3_2.EditValue = Format.GetString(dr["LSL"]);
                    txtdimplestd3_2.EditValue = Format.GetString(dr["SL"]);
                    txtdimplehigh3_2.EditValue = Format.GetString(dr["USL"]);
                }
                if (dr["INSPITEMID"].ToString().Equals("0368"))
                {
                    txtoverlow3_2.EditValue = Format.GetString(dr["LSL"]);
                    txtoverstd3_2.EditValue = Format.GetString(dr["SL"]);
                    txtoverhigh3_2.EditValue = Format.GetString(dr["USL"]);
                }
            }
        }

        private void Popviafill3_1_EditValueChanged(object sender, EventArgs e)
        {
            if (popviafill3_1.EditValue.Equals(""))
            {
                txtplatinglow3_1.EditValue = string.Empty;
                txtplatingstd3_1.EditValue = string.Empty;
                txtplatinghigh3_1.EditValue = string.Empty;
                txtholelow3_1.EditValue = string.Empty;
                txtholestd3_1.EditValue = string.Empty;
                txtholehigh3_1.EditValue = string.Empty;
                txttotallow3_1.EditValue = string.Empty;
                txttotalstd3_1.EditValue = string.Empty;
                txttotalhigh3_1.EditValue = string.Empty;
                txtdimplelow3_1.EditValue = string.Empty;
                txtdimplestd3_1.EditValue = string.Empty;
                txtdimplehigh3_1.EditValue = string.Empty;
                txtoverlow3_1.EditValue = string.Empty;
                txtoverstd3_1.EditValue = string.Empty;
                txtoverhigh3_1.EditValue = string.Empty;
                processid = "";
            }
            DataTable dtsemgment = imspitem(processid);
            foreach (DataRow dr in dtsemgment.Rows)
            {
                if (dr["INSPITEMID"].ToString().Equals("0375"))
                {
                    txtplatinglow3_1.EditValue = Format.GetString(dr["LSL"]);
                    txtplatingstd3_1.EditValue = Format.GetString(dr["SL"]);
                    txtplatinghigh3_1.EditValue = Format.GetString(dr["USL"]);
                }
                if (dr["INSPITEMID"].ToString().Equals("0376"))
                {
                    txtholelow3_1.EditValue = Format.GetString(dr["LSL"]);
                    txtholestd3_1.EditValue = Format.GetString(dr["SL"]);
                    txtholehigh3_1.EditValue = Format.GetString(dr["USL"]);
                }
                if (dr["INSPITEMID"].ToString().Equals("0377"))
                {
                    txttotallow3_1.EditValue = Format.GetString(dr["LSL"]);
                    txttotalstd3_1.EditValue = Format.GetString(dr["SL"]);
                    txttotalhigh3_1.EditValue = Format.GetString(dr["USL"]);
                }
                if (dr["INSPITEMID"].ToString().Equals("0367"))
                {
                    txtdimplelow3_1.EditValue = Format.GetString(dr["LSL"]);
                    txtdimplestd3_1.EditValue = Format.GetString(dr["SL"]);
                    txtdimplehigh3_1.EditValue = Format.GetString(dr["USL"]);
                }
                if (dr["INSPITEMID"].ToString().Equals("0368"))
                {
                    txtoverlow3_1.EditValue = Format.GetString(dr["LSL"]);
                    txtoverstd3_1.EditValue = Format.GetString(dr["SL"]);
                    txtoverhigh3_1.EditValue = Format.GetString(dr["USL"]);
                }
            }
        }

        private void Popviafill2_2_EditValueChanged(object sender, EventArgs e)
        {

            if (popviafill2_2.EditValue.Equals(""))
            {
                txtplatinglow2_2.EditValue = string.Empty;
                txtplatingstd2_2.EditValue = string.Empty;
                txtplatinghigh2_2.EditValue = string.Empty;
                txtholelow2_2.EditValue = string.Empty;
                txtholestd2_2.EditValue = string.Empty;
                txtholehigh2_2.EditValue = string.Empty;
                txttotallow2_2.EditValue = string.Empty;
                txttotalstd2_2.EditValue = string.Empty;
                txttotalhigh2_2.EditValue = string.Empty;
                txtdimplelow2_2.EditValue = string.Empty;
                txtdimplestd2_2.EditValue = string.Empty;
                txtdimplehigh2_2.EditValue = string.Empty;
                txtoverlow2_2.EditValue = string.Empty;
                txtoverstd2_2.EditValue = string.Empty;
                txtoverhigh2_2.EditValue = string.Empty;
                processid = "";
            }
            DataTable dtsemgment = imspitem(processid);
            foreach (DataRow dr in dtsemgment.Rows)
            {
                if (dr["INSPITEMID"].ToString().Equals("0375"))
                {
                    txtplatinglow2_2.EditValue = Format.GetString(dr["LSL"]);
                    txtplatingstd2_2.EditValue = Format.GetString(dr["SL"]);
                    txtplatinghigh2_2.EditValue = Format.GetString(dr["USL"]);
                }
                if (dr["INSPITEMID"].ToString().Equals("0376"))
                {
                    txtholelow2_2.EditValue = Format.GetString(dr["LSL"]);
                    txtholestd2_2.EditValue = Format.GetString(dr["SL"]);
                    txtholehigh2_2.EditValue = Format.GetString(dr["USL"]);
                }
                if (dr["INSPITEMID"].ToString().Equals("0377"))
                {
                    txttotallow2_2.EditValue = Format.GetString(dr["LSL"]);
                    txttotalstd2_2.EditValue = Format.GetString(dr["SL"]);
                    txttotalhigh2_2.EditValue = Format.GetString(dr["USL"]);
                }
                if (dr["INSPITEMID"].ToString().Equals("0367"))
                {
                    txtdimplelow2_2.EditValue = Format.GetString(dr["LSL"]);
                    txtdimplestd2_2.EditValue = Format.GetString(dr["SL"]);
                    txtdimplehigh2_2.EditValue = Format.GetString(dr["USL"]);
                }
                if (dr["INSPITEMID"].ToString().Equals("0368"))
                {
                    txtoverlow2_2.EditValue = Format.GetString(dr["LSL"]);
                    txtoverstd2_2.EditValue = Format.GetString(dr["SL"]);
                    txtoverhigh2_2.EditValue = Format.GetString(dr["USL"]);
                }
            }
        }

        private void Popviafill2_1_EditValueChanged(object sender, EventArgs e)
        {
            if (popviafill2_1.EditValue.Equals(""))
            {
                txtplatinglow2_1.EditValue = string.Empty;
                txtplatingstd2_1.EditValue = string.Empty;
                txtplatinghigh2_1.EditValue = string.Empty;
                txtholelow2_1.EditValue = string.Empty;
                txtholestd2_1.EditValue = string.Empty;
                txtholehigh2_1.EditValue = string.Empty;
                txttotallow2_1.EditValue = string.Empty;
                txttotalstd2_1.EditValue = string.Empty;
                txttotalhigh2_1.EditValue = string.Empty;
                txtdimplelow2_1.EditValue = string.Empty;
                txtdimplestd2_1.EditValue = string.Empty;
                txtdimplehigh2_1.EditValue = string.Empty;
                txtoverlow2_1.EditValue = string.Empty;
                txtoverstd2_1.EditValue = string.Empty;
                txtoverhigh2_1.EditValue = string.Empty;
                processid = "";
            }
            DataTable dtsemgment = imspitem(processid);
            foreach (DataRow dr in dtsemgment.Rows)
            {
                if (dr["INSPITEMID"].ToString().Equals("0375"))
                {
                    txtplatinglow2_1.EditValue = Format.GetString(dr["LSL"]);
                    txtplatingstd2_1.EditValue = Format.GetString(dr["SL"]);
                    txtplatinghigh2_1.EditValue = Format.GetString(dr["USL"]);
                }
                if (dr["INSPITEMID"].ToString().Equals("0376"))
                {
                    txtholelow2_1.EditValue = Format.GetString(dr["LSL"]);
                    txtholestd2_1.EditValue = Format.GetString(dr["SL"]);
                    txtholehigh2_1.EditValue = Format.GetString(dr["USL"]);
                }
                if (dr["INSPITEMID"].ToString().Equals("0377"))
                {
                    txttotallow2_1.EditValue = Format.GetString(dr["LSL"]);
                    txttotalstd2_1.EditValue = Format.GetString(dr["SL"]);
                    txttotalhigh2_1.EditValue = Format.GetString(dr["USL"]);
                }
                if (dr["INSPITEMID"].ToString().Equals("0367"))
                {
                    txtdimplelow2_1.EditValue = Format.GetString(dr["LSL"]);
                    txtdimplestd2_1.EditValue = Format.GetString(dr["SL"]);
                    txtdimplehigh2_1.EditValue = Format.GetString(dr["USL"]);
                }
                if (dr["INSPITEMID"].ToString().Equals("0368"))
                {
                    txtoverlow2_1.EditValue = Format.GetString(dr["LSL"]);
                    txtoverstd2_1.EditValue = Format.GetString(dr["SL"]);
                    txtoverhigh2_1.EditValue = Format.GetString(dr["USL"]);
                }
            }
        }

        private void Popviafill1_2_EditValueChanged(object sender, EventArgs e)
        {
            if (popviafill1_2.EditValue.Equals(""))
            {

                txtplatinglow1_2.EditValue = string.Empty;
                txtplatingstd1_2.EditValue = string.Empty;
                txtplatinghigh1_2.EditValue = string.Empty;
                txtholelow1_2.EditValue = string.Empty;
                txtholestd1_2.EditValue = string.Empty;
                txtholehigh1_2.EditValue = string.Empty;
                txttotallow1_2.EditValue = string.Empty;
                txttotalstd1_2.EditValue = string.Empty;
                txttotalhigh1_2.EditValue = string.Empty;
                txtdimplelow1_2.EditValue = string.Empty;
                txtdimplestd1_2.EditValue = string.Empty;
                txtdimplehigh1_2.EditValue = string.Empty;
                txtoverlow1_2.EditValue = string.Empty;
                txtoverstd1_2.EditValue = string.Empty;
                txtoverhigh1_2.EditValue = string.Empty;
                processid = "";
            }
            DataTable dtsemgment = imspitem(processid);
            foreach (DataRow dr in dtsemgment.Rows)
            {
                if (dr["INSPITEMID"].ToString().Equals("0375"))
                {
                    txtplatinglow1_2.EditValue = Format.GetString(dr["LSL"]);
                    txtplatingstd1_2.EditValue = Format.GetString(dr["SL"]);
                    txtplatinghigh1_2.EditValue = Format.GetString(dr["USL"]);
                }
                if (dr["INSPITEMID"].ToString().Equals("0376"))
                {
                    txtholelow1_2.EditValue = Format.GetString(dr["LSL"]);
                    txtholestd1_2.EditValue = Format.GetString(dr["SL"]);
                    txtholehigh1_2.EditValue = Format.GetString(dr["USL"]);
                }
                if (dr["INSPITEMID"].ToString().Equals("0377"))
                {
                    txttotallow1_2.EditValue = Format.GetString(dr["LSL"]);
                    txttotalstd1_2.EditValue = Format.GetString(dr["SL"]);
                    txttotalhigh1_2.EditValue = Format.GetString(dr["USL"]);
                }
                if (dr["INSPITEMID"].ToString().Equals("0367"))
                {
                    txtdimplelow1_2.EditValue = Format.GetString(dr["LSL"]);
                    txtdimplestd1_2.EditValue = Format.GetString(dr["SL"]);
                    txtdimplehigh1_2.EditValue = Format.GetString(dr["USL"]);
                }
                if (dr["INSPITEMID"].ToString().Equals("0368"))
                {
                    txtoverlow1_2.EditValue = Format.GetString(dr["LSL"]);
                    txtoverstd1_2.EditValue = Format.GetString(dr["SL"]);
                    txtoverhigh1_2.EditValue = Format.GetString(dr["USL"]);
                }
            }
        }

        private void Popviafill1_1_EditValueChanged(object sender, EventArgs e)
        {
            if (popviafill1_1.EditValue.Equals(""))
            {

        
                txtplatinglow1_1.EditValue = string.Empty;
                txtplatingstd1_1.EditValue = string.Empty;
                txtplatinghigh1_1.EditValue = string.Empty;
                txtholelow1_1.EditValue = string.Empty;
                txtholestd1_1.EditValue = string.Empty;
                txtholehigh1_1.EditValue = string.Empty;
                txttotallow1_1.EditValue = string.Empty;
                txttotalstd1_1.EditValue = string.Empty;
                txttotalhigh1_1.EditValue = string.Empty;
                txtdimplelow1_1.EditValue = string.Empty;
                txtdimplestd1_1.EditValue = string.Empty;
                txtdimplehigh1_1.EditValue = string.Empty;
                txtoverlow1_1.EditValue = string.Empty;
                txtoverstd1_1.EditValue = string.Empty;
                txtoverhigh1_1.EditValue = string.Empty;
                processid = "";
            }

            DataTable dtsemgment = imspitem(processid);
            foreach (DataRow dr in dtsemgment.Rows)
            {
                if (dr["INSPITEMID"].ToString().Equals("0375"))
                {
                    txtplatinglow1_1.EditValue = Format.GetString(dr["LSL"]);
                    txtplatingstd1_1.EditValue = Format.GetString(dr["SL"]);
                    txtplatinghigh1_1.EditValue = Format.GetString(dr["USL"]);
                }
                if (dr["INSPITEMID"].ToString().Equals("0376"))
                {
                    txtholelow1_1.EditValue = Format.GetString(dr["LSL"]);
                    txtholestd1_1.EditValue = Format.GetString(dr["SL"]);
                    txtholehigh1_1.EditValue = Format.GetString(dr["USL"]);
                }
                if (dr["INSPITEMID"].ToString().Equals("0377"))
                {
                    txttotallow1_1.EditValue = Format.GetString(dr["LSL"]);
                    txttotalstd1_1.EditValue = Format.GetString(dr["SL"]);
                    txttotalhigh1_1.EditValue = Format.GetString(dr["USL"]);
                }
                if (dr["INSPITEMID"].ToString().Equals("0367"))
                {
                    txtdimplelow1_1.EditValue = Format.GetString(dr["LSL"]);
                    txtdimplestd1_1.EditValue = Format.GetString(dr["SL"]);
                    txtdimplehigh1_1.EditValue = Format.GetString(dr["USL"]);
                }
                if (dr["INSPITEMID"].ToString().Equals("0368"))
                {
                    txtoverlow1_1.EditValue = Format.GetString(dr["LSL"]);
                    txtoverstd1_1.EditValue = Format.GetString(dr["SL"]);
                    txtoverhigh1_1.EditValue = Format.GetString(dr["USL"]);
                }

            }
        }

        public DataTable imspitem(string processid)
        {
            DataTable dtsemgment = new DataTable();
            if (!processid.Equals(""))
            {
                Dictionary<string, object> Param = new Dictionary<string, object>();
                Param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                Param.Add("RESOURCEID", dtProductDEF.Rows[0]["ITEMID"]);
                Param.Add("RESOURCEVERSION", dtProductDEF.Rows[0]["ITEMVERSION"]);
                Param.Add("PROCESSSEGMENTID", processid);
                dtsemgment = SqlExecuter.Query("GetProcessSpec", "10001", Param);
                return dtsemgment;
            }

            return dtsemgment;


        }


        #region event


        #endregion

        private void tbLayer21_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void smartSplitTableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lbPlatingType3_6_Click(object sender, EventArgs e)
        {

        }

        private void lbPlatingType1_6_Click(object sender, EventArgs e)
        {

        }

        private void smartSplitTableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lbPlatingType1_Click(object sender, EventArgs e)
        {

        }

        private void smartTextBox4_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void smartTextBox23_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void smartSplitTableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtholestd3_1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void txttotalstd1_1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void txtholelow1_2_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
