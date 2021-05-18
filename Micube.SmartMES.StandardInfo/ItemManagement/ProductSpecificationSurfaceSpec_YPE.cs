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
    public partial class ProductSpecificationSurfaceSpec_YPE : UserControl
    {
        DataTable dtProductDEF = new DataTable();
        DataTable dtProcess = new DataTable();
        string processid = "";

        public DataTable DataSource
        {

            get; private set;
        }
        public ProductSpecificationSurfaceSpec_YPE()
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

            TextBoxHelper.SetUnitMask(Unit.um, txtnilow1);
            TextBoxHelper.SetUnitMask(Unit.um, txtnilow2);
            TextBoxHelper.SetUnitMask(Unit.um, txtnilow3);
            TextBoxHelper.SetUnitMask(Unit.um, txtaulow1);
            TextBoxHelper.SetUnitMask(Unit.um, txtaulow2);
            TextBoxHelper.SetUnitMask(Unit.um, txtaulow3);
            TextBoxHelper.SetUnitMask(Unit.um, txtpdsnlow1);
            TextBoxHelper.SetUnitMask(Unit.um, txtpdsnlow2);
            TextBoxHelper.SetUnitMask(Unit.um, txtpdsnlow3);
            TextBoxHelper.SetUnitMask(Unit.um, txtnihigh1);
            TextBoxHelper.SetUnitMask(Unit.um, txtnihigh2);
            TextBoxHelper.SetUnitMask(Unit.um, txtnihigh3);
            TextBoxHelper.SetUnitMask(Unit.um, txtauhigh1);
            TextBoxHelper.SetUnitMask(Unit.um, txtauhigh2);
            TextBoxHelper.SetUnitMask(Unit.um, txtauhigh3);
            TextBoxHelper.SetUnitMask(Unit.um, txtpdsnhigh1);
            TextBoxHelper.SetUnitMask(Unit.um, txtpdsnhigh2);
            TextBoxHelper.SetUnitMask(Unit.um, txtpdsnhigh3);
            TextBoxHelper.SetUnitMask(Unit.sqmm, txtscraplow1);
            TextBoxHelper.SetUnitMask(Unit.sqmm, txtscraplow2);
            TextBoxHelper.SetUnitMask(Unit.sqmm, txtscraplow3);
            TextBoxHelper.SetUnitMask(Unit.sqmm, txtscraphigh1);
            TextBoxHelper.SetUnitMask(Unit.sqmm, txtscraphigh2);
            TextBoxHelper.SetUnitMask(Unit.sqmm, txtscraphigh3);
            TextBoxHelper.SetUnitMask(Unit.sqmm, txtproducthigh1);
            TextBoxHelper.SetUnitMask(Unit.sqmm, txtproducthigh2);
            TextBoxHelper.SetUnitMask(Unit.sqmm, txtproducthigh3);
            TextBoxHelper.SetUnitMask(Unit.sqmm, txtproductlow1);
            TextBoxHelper.SetUnitMask(Unit.sqmm, txtproductlow2);
            TextBoxHelper.SetUnitMask(Unit.sqmm, txtproductlow3);

            // ENEPIG1
            ConditionItemSelectPopup enepig1 = new ConditionItemSelectPopup();
            enepig1.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            enepig1.SetPopupLayout("PROCESSSEGMENTID", PopupButtonStyles.Ok_Cancel);
            enepig1.Id = "PROCESSSEGMENTID";
            enepig1.LabelText = "PROCESSSEGMENTID";
            enepig1.SearchQuery = new SqlQuery("GetPlatingTypeSegment_YPE", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"PLANTID={UserInfo.Current.Plant}",
                        $"RESOURCEID={dtProductDEF.Rows[0]["ITEMID"]}", $"RESOURCEVERSION={dtProductDEF.Rows[0]["ITEMVERSION"]}");
            enepig1.IsMultiGrid = false;
            enepig1.DisplayFieldName = "PROCESSSEGMENTNAME";
            enepig1.ValueFieldName = "PROCESSSEGMENTID";
            enepig1.LanguageKey = "PROCESSSEGMENTNAME";
            enepig1.Conditions.AddTextBox("PROCESSSEGMENTIDNAME");
            enepig1.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150);
            enepig1.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
            popendpig1.SelectPopupCondition = enepig1;
            popendpig1.SelectPopupCondition.SetPopupApplySelection((selectRow, gridRow) => {


                selectRow.AsEnumerable().ForEach(r => {

                    processid = Format.GetString(r["PROCESSSEGMENTID"]);


                });
            });





            // ENEPIG2
            ConditionItemSelectPopup enepig2 = new ConditionItemSelectPopup();
            enepig2.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            enepig2.SetPopupLayout("PROCESSSEGMENTID", PopupButtonStyles.Ok_Cancel);
            enepig2.Id = "PROCESSSEGMENTID";
            enepig2.LabelText = "PROCESSSEGMENTID";
            enepig2.SearchQuery = new SqlQuery("GetPlatingTypeSegment_YPE", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"PLANTID={UserInfo.Current.Plant}",
                        $"RESOURCEID={dtProductDEF.Rows[0]["ITEMID"]}", $"RESOURCEVERSION={dtProductDEF.Rows[0]["ITEMVERSION"]}");
            enepig2.IsMultiGrid = false;
            enepig2.DisplayFieldName = "PROCESSSEGMENTNAME";
            enepig2.ValueFieldName = "PROCESSSEGMENTID";
            enepig2.LanguageKey = "PROCESSSEGMENTNAME";
            enepig2.Conditions.AddTextBox("PROCESSSEGMENTIDNAME");
            enepig2.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150);
            enepig2.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
            popendpig2.SelectPopupCondition = enepig2;
            popendpig2.SelectPopupCondition.SetPopupApplySelection((selectRow, gridRow) => {


                selectRow.AsEnumerable().ForEach(r => {

                    processid = Format.GetString(r["PROCESSSEGMENTID"]);


                });
            });



            // ENEPIG3
            ConditionItemSelectPopup enepig3 = new ConditionItemSelectPopup();
            enepig3.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            enepig3.SetPopupLayout("PROCESSSEGMENTID", PopupButtonStyles.Ok_Cancel);
            enepig3.Id = "PROCESSSEGMENTID";
            enepig3.LabelText = "PROCESSSEGMENTID";
            enepig3.SearchQuery = new SqlQuery("GetPlatingTypeSegment_YPE", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"PLANTID={UserInfo.Current.Plant}",
                        $"RESOURCEID={dtProductDEF.Rows[0]["ITEMID"]}", $"RESOURCEVERSION={dtProductDEF.Rows[0]["ITEMVERSION"]}");
            enepig3.IsMultiGrid = false;
            enepig3.DisplayFieldName = "PROCESSSEGMENTNAME";
            enepig3.ValueFieldName = "PROCESSSEGMENTID";
            enepig3.LanguageKey = "PROCESSSEGMENTNAME";
            enepig3.Conditions.AddTextBox("PROCESSSEGMENTIDNAME");
            enepig3.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150);
            enepig3.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
            popendpig3.SelectPopupCondition = enepig3;
            popendpig3.SelectPopupCondition.SetPopupApplySelection((selectRow, gridRow) => {


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
        /// 조회 데이터 바인드
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
                    popendpig1.SetValue(dt.Rows[0]["PROCESSSEGMENTID"]);
                    popendpig1.EditValue = dt.Rows[0]["SPECDETAILFROM"];


                }
                else if (i == 2)
                {
                    processid = dt.Rows[1]["PROCESSSEGMENTID"].ToString();
                    popendpig2.SetValue(dt.Rows[1]["PROCESSSEGMENTID"]);
                    popendpig2.EditValue = dt.Rows[1]["SPECDETAILFROM"];

                }
                else if (i == 3)
                {
                    processid = dt.Rows[2]["PROCESSSEGMENTID"].ToString();
                    popendpig3.SetValue(dt.Rows[2]["PROCESSSEGMENTID"]);
                    popendpig3.EditValue = dt.Rows[2]["SPECDETAILFROM"];

                }

            }
        }



        public DataTable SurFacereturn()
        {

            DataSource = new DataTable();
            ReportTableReturn.GetLabelDataTable(smartSplitTableLayoutPanel1, DataSource);


            DataRow row = DataSource.NewRow();
            ReportTableReturn.GetDataRow(row, smartSplitTableLayoutPanel1);
            DataSource.Rows.Add(row);
            return DataSource;

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
            for (int i = 1; i <= 3; i++)
            {
                DataRow drCopperPlating = dtRtn.NewRow();
                drCopperPlating["DETAILTYPE"] = "SurfaceSpec";
                drCopperPlating["SEQUENCE"] = i.ToString();

                if (i == 1)
                {
                    drCopperPlating["DETAILNAME"] = "SurfaceSpec";
                    drCopperPlating["SPECDETAILFROM"] = popendpig1.GetValue();
                }
                else if (i == 2)
                {
                    drCopperPlating["DETAILNAME"] = "SurfaceSpec";
                    drCopperPlating["SPECDETAILFROM"] = popendpig2.GetValue();
                }
                else if (i == 3)
                {
                    drCopperPlating["DETAILNAME"] = "SurfaceSpec";
                    drCopperPlating["SPECDETAILFROM"] = popendpig3.GetValue();
                }


                dtRtn.Rows.Add(drCopperPlating);
            }




            return dtRtn;

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

            popendpig1.SelectPopupCondition.SearchQuery = new SqlQuery("GetPlatingTypeSegment_YPE", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}",
                        $"RESOURCEID={dtProductDEF.Rows[0]["ITEMID"]}", $"RESOURCEVERSION={dtProductDEF.Rows[0]["ITEMVERSION"]}", $"PROCESSSEGID=5514");
            popendpig1.SelectPopupCondition.SetPopupApplySelection((selectRow, gridRow) => {


                selectRow.AsEnumerable().ForEach(r => {

                    popendpig1.SetValue(Format.GetString(r["PROCESSSEGMENTID"]));
                    processid = Format.GetString(r["PROCESSSEGMENTID"]);

                });
            });

            popendpig2.SelectPopupCondition.SearchQuery = new SqlQuery("GetPlatingTypeSegment_YPE", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}",
                        $"RESOURCEID={dtProductDEF.Rows[0]["ITEMID"]}", $"RESOURCEVERSION={dtProductDEF.Rows[0]["ITEMVERSION"]}", $"PROCESSSEGID=5514");
            popendpig2.SelectPopupCondition.SetPopupApplySelection((selectRow, gridRow) => {


                selectRow.AsEnumerable().ForEach(r => {

                    popendpig2.SetValue(Format.GetString(r["PROCESSSEGMENTID"]));
                    processid = Format.GetString(r["PROCESSSEGMENTID"]);

                });
            });

            popendpig3.SelectPopupCondition.SearchQuery = new SqlQuery("GetPlatingTypeSegment_YPE", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}",
                        $"RESOURCEID={dtProductDEF.Rows[0]["ITEMID"]}", $"RESOURCEVERSION={dtProductDEF.Rows[0]["ITEMVERSION"]}", $"PROCESSSEGID=5514");
            popendpig3.SelectPopupCondition.SetPopupApplySelection((selectRow, gridRow) => {


                selectRow.AsEnumerable().ForEach(r => {

                    popendpig3.SetValue(Format.GetString(r["PROCESSSEGMENTID"]));
                    processid = Format.GetString(r["PROCESSSEGMENTID"]);

                });
            });

            popendpig1.EditValueChanged += Popviafill1_1_EditValueChanged;
            popendpig2.EditValueChanged += Popviafill2_1_EditValueChanged;
            popendpig3.EditValueChanged += Popviafill3_1_EditValueChanged;
 


        }



        private void Popviafill3_1_EditValueChanged(object sender, EventArgs e)
        {
            if (popendpig3.EditValue.Equals(""))
            {
                txtnilow3.EditValue = string.Empty;
                txtnihigh3.EditValue = string.Empty;
                txtaulow3.EditValue = string.Empty;
                txtauhigh3.EditValue = string.Empty;
                txtnihigh3.EditValue = string.Empty;
                txtpdsnlow3.EditValue = string.Empty;
                txtpdsnhigh3.EditValue = string.Empty;
                txtproductlow3.EditValue = string.Empty;
                txtproducthigh3.EditValue = string.Empty;
                txtscraplow3.EditValue = string.Empty;
                txtscraphigh3.EditValue = string.Empty;
                processid = "";
            }
            DataTable dtsemgment = imspitem(processid);
            foreach (DataRow dr in dtsemgment.Rows)
            {
                if (dr["INSPITEMID"].ToString().Equals("0381"))
                {
                    txtnilow3.EditValue = Format.GetString(dr["LSL"]);
                    txtnihigh3.EditValue = Format.GetString(dr["USL"]);
                }
                if (dr["INSPITEMID"].ToString().Equals("0108"))
                {
                    txtaulow3.EditValue = Format.GetString(dr["LSL"]);
                    txtauhigh3.EditValue = Format.GetString(dr["USL"]);
                }
                if (dr["INSPITEMID"].ToString().Equals("0378"))
                {
                    txtscraplow3.EditValue = Format.GetString(dr["LSL"]);
                    txtscraphigh3.EditValue = Format.GetString(dr["USL"]);
                }
                if (dr["INSPITEMID"].ToString().Equals("0379"))
                {
                    txtproductlow3.EditValue = Format.GetString(dr["LSL"]);
                    txtproducthigh3.EditValue = Format.GetString(dr["USL"]);
                }
                if (dr["INSPITEMID"].ToString().Equals("0380"))
                {
                    txtpdsnlow3.EditValue = Format.GetString(dr["LSL"]);
                    txtpdsnhigh3.EditValue = Format.GetString(dr["USL"]);
                }
            }
        }


        private void Popviafill2_1_EditValueChanged(object sender, EventArgs e)
        {
            if (popendpig2.EditValue.Equals(""))
            {
                txtnilow2.EditValue = string.Empty;
                txtnihigh2.EditValue = string.Empty;
                txtaulow2.EditValue = string.Empty;
                txtauhigh2.EditValue = string.Empty;
                txtpdsnlow2.EditValue = string.Empty;
                txtpdsnhigh2.EditValue = string.Empty;
                txtproductlow2.EditValue = string.Empty;
                txtproducthigh2.EditValue = string.Empty;
                txtscraplow2.EditValue = string.Empty;
                txtscraphigh2.EditValue = string.Empty;
                processid = "";
            }
            DataTable dtsemgment = imspitem(processid);
            foreach (DataRow dr in dtsemgment.Rows)
            {
                if (dr["INSPITEMID"].ToString().Equals("0381"))
                {
                    txtnilow2.EditValue = Format.GetString(dr["LSL"]);
                    txtnihigh2.EditValue = Format.GetString(dr["USL"]);
                }
                if (dr["INSPITEMID"].ToString().Equals("0108"))
                {
                    txtaulow2.EditValue = Format.GetString(dr["LSL"]);
                    txtauhigh2.EditValue = Format.GetString(dr["USL"]);
                }
                if (dr["INSPITEMID"].ToString().Equals("0378"))
                {
                    txtscraplow2.EditValue = Format.GetString(dr["LSL"]);
                    txtscraphigh2.EditValue = Format.GetString(dr["USL"]);
                }
                if (dr["INSPITEMID"].ToString().Equals("0379"))
                {
                    txtproductlow2.EditValue = Format.GetString(dr["LSL"]);
                    txtproducthigh2.EditValue = Format.GetString(dr["USL"]);
                }
                if (dr["INSPITEMID"].ToString().Equals("0380"))
                {
                    txtpdsnlow2.EditValue = Format.GetString(dr["LSL"]);
                    txtpdsnhigh2.EditValue = Format.GetString(dr["USL"]);
                }
            }
        }

        

        private void Popviafill1_1_EditValueChanged(object sender, EventArgs e)
        {
            if (popendpig1.EditValue.Equals(""))
            {

                txtnilow1.EditValue = string.Empty;
                txtnihigh1.EditValue = string.Empty;
                txtaulow1.EditValue = string.Empty;
                txtauhigh1.EditValue = string.Empty;
                txtpdsnlow1.EditValue = string.Empty;
                txtpdsnhigh1.EditValue = string.Empty;
                txtproductlow1.EditValue = string.Empty;
                txtproducthigh1.EditValue = string.Empty;
                txtscraplow1.EditValue = string.Empty;
                txtscraphigh1.EditValue = string.Empty;
                processid = "";
            }
            DataTable dtsemgment = imspitem(processid);
            foreach (DataRow dr in dtsemgment.Rows)
            {
                if (dr["INSPITEMID"].ToString().Equals("0381"))
                {
                    txtnilow1.EditValue = Format.GetString(dr["LSL"]);
                    txtnihigh1.EditValue = Format.GetString(dr["USL"]);
                }
                if (dr["INSPITEMID"].ToString().Equals("0108"))
                {
                    txtaulow1.EditValue = Format.GetString(dr["LSL"]);
                    txtauhigh1.EditValue = Format.GetString(dr["USL"]);
                }
                if (dr["INSPITEMID"].ToString().Equals("0378"))
                {
                    txtscraplow1.EditValue = Format.GetString(dr["LSL"]);
                    txtscraphigh1.EditValue = Format.GetString(dr["USL"]);

                }
                if (dr["INSPITEMID"].ToString().Equals("0379"))
                {
                    txtproductlow1.EditValue = Format.GetString(dr["LSL"]);
                    txtproducthigh1.EditValue = Format.GetString(dr["USL"]);
                }
                if (dr["INSPITEMID"].ToString().Equals("0380"))
                {
                    txtpdsnlow1.EditValue = Format.GetString(dr["LSL"]);
                    txtpdsnhigh1.EditValue = Format.GetString(dr["USL"]);
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
                dtsemgment = SqlExecuter.Query("GetProcessSpec", "10004", Param);
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

        private void smartLabel18_Click(object sender, EventArgs e)
        {

        }

        private void txtnilow1_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
