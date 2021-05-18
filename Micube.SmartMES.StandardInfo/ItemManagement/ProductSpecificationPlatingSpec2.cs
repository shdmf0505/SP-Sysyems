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
    public partial class ProductSpecificationPlatingSpec2 : UserControl
    {
        DataTable dtProductDEF = new DataTable();
        string processid = "";
        DataTable dtProcess = new DataTable();
        DataTable DataSource = new DataTable();
        #region 생성자

        public ProductSpecificationPlatingSpec2()
        {
            InitializeComponent();

            dtProductDEF.Columns.Add("ITEMID");
            dtProductDEF.Columns.Add("ITEMVERSION");
            DataRow dr = dtProductDEF.NewRow();
            dr["ITEMID"] = "*";
            dr["ITEMVERSION"] = "*";
            dtProductDEF.Rows.Add(dr);

            if (!this.IsDesignMode())
            {
                InitializeComboControl();


            }
        }

        #endregion

        #region 컨트롤 초기화

        /// <summary>
        /// ComboBox 컨트롤 초기화
        /// </summary>
        private void InitializeComboControl()
        {
        
            // 진행구분

            Dictionary<string, object> ParamISHF = new Dictionary<string, object>();
            ParamISHF.Add("CODECLASSID", "UserLayer");
            ParamISHF.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtISHF = SqlExecuter.Query("GetCodeList", "00001", ParamISHF);

            cbPlatingType1_4.DisplayMember = "CODENAME";
            cbPlatingType1_4.ValueMember = "CODEID";
            cbPlatingType1_4.ShowHeader = false;
            cbPlatingType1_4.DataSource = dtISHF;
            cbPlatingType1_4.EditValue = "CS";
            cbPlatingType1_5.DisplayMember = "CODENAME";
            cbPlatingType1_5.ValueMember = "CODEID";
            cbPlatingType1_5.ShowHeader = false;
            cbPlatingType1_5.DataSource = dtISHF;
            cbPlatingType1_5.EditValue = "SS";
            cbPlatingType2_4.DisplayMember = "CODENAME";
            cbPlatingType2_4.ValueMember = "CODEID";
            cbPlatingType2_4.ShowHeader = false;
            cbPlatingType2_4.DataSource = dtISHF;
            cbPlatingType2_4.EditValue = "CS";
            cbPlatingType2_5.DisplayMember = "CODENAME";
            cbPlatingType2_5.ValueMember = "CODEID";
            cbPlatingType2_5.ShowHeader = false;
            cbPlatingType2_5.DataSource = dtISHF;
            cbPlatingType2_5.EditValue = "SS";
            cbPlatingType3_4.DisplayMember = "CODENAME";
            cbPlatingType3_4.ValueMember = "CODEID";
            cbPlatingType3_4.ShowHeader = false;
            cbPlatingType3_4.DataSource = dtISHF;
            cbPlatingType3_4.EditValue = "CS";
            cbPlatingType3_5.DisplayMember = "CODENAME";
            cbPlatingType3_5.ValueMember = "CODEID";
            cbPlatingType3_5.ShowHeader = false;
            cbPlatingType3_5.DataSource = dtISHF;
            cbPlatingType3_5.EditValue = "SS";


            // 외층동도금1
            ConditionItemSelectPopup outplating = new ConditionItemSelectPopup();
            outplating.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            outplating.SetPopupLayout("PROCESSSEGMENTID", PopupButtonStyles.Ok_Cancel);
            outplating.Id = "PROCESSSEGMENTID";
            outplating.LabelText = "PROCESSSEGMENTID";
            outplating.SearchQuery = new SqlQuery("GetPlatingTypeSegment", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"PLANTID={UserInfo.Current.Plant}", $"PLATINGTYPE={"OuterCopperPlating"}");
            outplating.IsMultiGrid = false;
            outplating.DisplayFieldName = "PROCESSSEGMENTNAME";
            outplating.ValueFieldName = "PROCESSSEGMENTID";
            outplating.LanguageKey = "PROCESSSEGMENTNAME";
            outplating.Conditions.AddTextBox("PROCESSSEGMENTIDNAME");
            outplating.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150);
            outplating.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
            popoutplating1.SelectPopupCondition = outplating;


            // 외층동도금2
            ConditionItemSelectPopup outplating2 = new ConditionItemSelectPopup();
            outplating2.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            outplating2.SetPopupLayout("PROCESSSEGMENTID", PopupButtonStyles.Ok_Cancel);
            outplating2.Id = "PROCESSSEGMENTID";
            outplating2.LabelText = "PROCESSSEGMENTID";
            outplating2.SearchQuery = new SqlQuery("GetPlatingTypeSegment", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"PLANTID={UserInfo.Current.Plant}", $"PLATINGTYPE={"OuterCopperPlating"}");
            outplating2.IsMultiGrid = false;
            outplating2.DisplayFieldName = "PROCESSSEGMENTNAME";
            outplating2.ValueFieldName = "PROCESSSEGMENTID";
            outplating2.LanguageKey = "PROCESSSEGMENTNAME";
            outplating2.Conditions.AddTextBox("PROCESSSEGMENTIDNAME");
            outplating2.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150);
            outplating2.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
            popoutplating2.SelectPopupCondition = outplating2;

            // 내층동도금1-1
            ConditionItemSelectPopup innerplating1_1 = new ConditionItemSelectPopup();
            innerplating1_1.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            innerplating1_1.SetPopupLayout("PROCESSSEGMENTID", PopupButtonStyles.Ok_Cancel);
            innerplating1_1.Id = "PROCESSSEGMENTID";
            innerplating1_1.LabelText = "PROCESSSEGMENTID";
            innerplating1_1.SearchQuery = new SqlQuery("GetPlatingTypeSegment", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"PLANTID={UserInfo.Current.Plant}", $"PLATINGTYPE={"InnerCopperPlating1"}");
            innerplating1_1.IsMultiGrid = false;
            innerplating1_1.DisplayFieldName = "PROCESSSEGMENTNAME";
            innerplating1_1.ValueFieldName = "PROCESSSEGMENTID";
            innerplating1_1.LanguageKey = "PROCESSSEGMENTNAME";
            innerplating1_1.Conditions.AddTextBox("PROCESSSEGMENTIDNAME");
            innerplating1_1.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150);
            innerplating1_1.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
            popinnerplating1_1.SelectPopupCondition = innerplating1_1;


            // 내층동도금1-2
            ConditionItemSelectPopup innerplating1_2 = new ConditionItemSelectPopup();
            innerplating1_2.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            innerplating1_2.SetPopupLayout("PROCESSSEGMENTID", PopupButtonStyles.Ok_Cancel);
            innerplating1_2.Id = "PROCESSSEGMENTID";
            innerplating1_2.LabelText = "PROCESSSEGMENTID";
            innerplating1_2.SearchQuery = new SqlQuery("GetPlatingTypeSegment", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"PLANTID={UserInfo.Current.Plant}", $"PLATINGTYPE={"InnerCopperPlating1"}");
            innerplating1_2.IsMultiGrid = false;
            innerplating1_2.DisplayFieldName = "PROCESSSEGMENTNAME";
            innerplating1_2.ValueFieldName = "PROCESSSEGMENTID";
            innerplating1_2.LanguageKey = "PROCESSSEGMENTNAME";
            innerplating1_2.Conditions.AddTextBox("PROCESSSEGMENTIDNAME");
            innerplating1_2.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150);
            innerplating1_2.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
            popinnerplating1_2.SelectPopupCondition = innerplating1_2;

   

            // 내층동도금2-1
            ConditionItemSelectPopup innerplating2_1 = new ConditionItemSelectPopup();
            innerplating2_1.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            innerplating2_1.SetPopupLayout("PROCESSSEGMENTID", PopupButtonStyles.Ok_Cancel);
            innerplating2_1.Id = "PROCESSSEGMENTID";
            innerplating2_1.LabelText = "PROCESSSEGMENTID";
            innerplating2_1.SearchQuery = new SqlQuery("GetPlatingTypeSegment", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"PLANTID={UserInfo.Current.Plant}", $"PLATINGTYPE={"InnerCopperPlating2"}");
            innerplating2_1.IsMultiGrid = false;
            innerplating2_1.DisplayFieldName = "PROCESSSEGMENTNAME";
            innerplating2_1.ValueFieldName = "PROCESSSEGMENTID";
            innerplating2_1.LanguageKey = "PROCESSSEGMENTNAME";
            innerplating2_1.Conditions.AddTextBox("PROCESSSEGMENTIDNAME");
            innerplating2_1.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150);
            innerplating2_1.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
            popinnerplating2_1.SelectPopupCondition = innerplating2_1;


            // 내층동도금2-2
            ConditionItemSelectPopup innerplating2_2 = new ConditionItemSelectPopup();
            innerplating2_2.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            innerplating2_2.SetPopupLayout("PROCESSSEGMENTID", PopupButtonStyles.Ok_Cancel);
            innerplating2_2.Id = "PROCESSSEGMENTID";
            innerplating2_2.LabelText = "PROCESSSEGMENTID";
            innerplating2_2.SearchQuery = new SqlQuery("GetPlatingTypeSegment", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"PLANTID={UserInfo.Current.Plant}", $"PLATINGTYPE={"InnerCopperPlating2"}");
            innerplating2_2.IsMultiGrid = false;
            innerplating2_2.DisplayFieldName = "PROCESSSEGMENTNAME";
            innerplating2_2.ValueFieldName = "PROCESSSEGMENTID";
            innerplating2_2.LanguageKey = "PROCESSSEGMENTNAME";
            innerplating2_2.Conditions.AddTextBox("PROCESSSEGMENTIDNAME");
            innerplating2_2.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150);
            innerplating2_2.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
            popinnerplating2_2.SelectPopupCondition = innerplating2_2;


            // 내층동도금3-1
            ConditionItemSelectPopup innerplating3_1 = new ConditionItemSelectPopup();
            innerplating3_1.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            innerplating3_1.SetPopupLayout("PROCESSSEGMENTID", PopupButtonStyles.Ok_Cancel);
            innerplating3_1.Id = "PROCESSSEGMENTID";
            innerplating3_1.LabelText = "PROCESSSEGMENTID";
            innerplating3_1.SearchQuery = new SqlQuery("GetPlatingTypeSegment", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"PLANTID={UserInfo.Current.Plant}", $"PLATINGTYPE={"InnerCopperPlating3"}");
            innerplating3_1.IsMultiGrid = false;
            innerplating3_1.DisplayFieldName = "PROCESSSEGMENTNAME";
            innerplating3_1.ValueFieldName = "PROCESSSEGMENTID";
            innerplating3_1.LanguageKey = "PROCESSSEGMENTNAME";
            innerplating3_1.Conditions.AddTextBox("PROCESSSEGMENTIDNAME");
            innerplating3_1.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150);
            innerplating3_1.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
            popinnerplating3_1.SelectPopupCondition = innerplating3_1;
 


            // 내층동도금3-2
            ConditionItemSelectPopup innerplating3_2 = new ConditionItemSelectPopup();
            innerplating3_2.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            innerplating3_2.SetPopupLayout("PROCESSSEGMENTID", PopupButtonStyles.Ok_Cancel);
            innerplating3_2.Id = "PROCESSSEGMENTID";
            innerplating3_2.LabelText = "PROCESSSEGMENTID";
            innerplating3_2.SearchQuery = new SqlQuery("GetPlatingTypeSegment", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"PLANTID={UserInfo.Current.Plant}",
                $"RESOURCEID={dtProductDEF.Rows[0]["ITEMID"]}", $"RESOURCEVERSION={dtProductDEF.Rows[0]["ITEMVERSION"]}");
            innerplating3_2.IsMultiGrid = false;
            innerplating3_2.DisplayFieldName = "PROCESSSEGMENTNAME";
            innerplating3_2.ValueFieldName = "PROCESSSEGMENTID";
            innerplating3_2.LanguageKey = "PROCESSSEGMENTNAME";
            innerplating3_2.Conditions.AddTextBox("PROCESSSEGMENTIDNAME");
            innerplating3_2.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150);
            innerplating3_2.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
            popinnerplating3_2.SelectPopupCondition = innerplating3_2;




            // 표면도금 1
            ConditionItemSelectPopup faceplating1 = new ConditionItemSelectPopup();
            faceplating1.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            faceplating1.SetPopupLayout("PROCESSSEGMENTID", PopupButtonStyles.Ok_Cancel);
            faceplating1.Id = "PROCESSSEGMENTID";
            faceplating1.LabelText = "PROCESSSEGMENTID";
            faceplating1.SearchQuery = new SqlQuery("GetPlatingTypeSegment", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"PLANTID={UserInfo.Current.Plant}",
                        $"RESOURCEID={dtProductDEF.Rows[0]["ITEMID"]}", $"RESOURCEVERSION={dtProductDEF.Rows[0]["ITEMVERSION"]}");
            faceplating1.IsMultiGrid = false;
            faceplating1.DisplayFieldName = "PROCESSSEGMENTNAME";
            faceplating1.ValueFieldName = "PROCESSSEGMENTID";
            faceplating1.LanguageKey = "PROCESSSEGMENTNAME";
            faceplating1.Conditions.AddTextBox("PROCESSSEGMENTIDNAME");
            faceplating1.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150);
            faceplating1.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
            popfaceplating1.SelectPopupCondition = faceplating1;


            // 표면도금 2
            ConditionItemSelectPopup faceplating2 = new ConditionItemSelectPopup();
            faceplating2.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            faceplating2.SetPopupLayout("PROCESSSEGMENTID", PopupButtonStyles.Ok_Cancel);
            faceplating2.Id = "PROCESSSEGMENTID";
            faceplating2.LabelText = "PROCESSSEGMENTID";
            faceplating2.SearchQuery = new SqlQuery("GetPlatingTypeSegment", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"PLANTID={UserInfo.Current.Plant}",
                        $"RESOURCEID={dtProductDEF.Rows[0]["ITEMID"]}", $"RESOURCEVERSION={dtProductDEF.Rows[0]["ITEMVERSION"]}");
            faceplating1.IsMultiGrid = false;
            faceplating2.IsMultiGrid = false;
            faceplating2.DisplayFieldName = "PROCESSSEGMENTNAME";
            faceplating2.ValueFieldName = "PROCESSSEGMENTID";
            faceplating2.LanguageKey = "PROCESSSEGMENTNAME";
            faceplating2.Conditions.AddTextBox("PROCESSSEGMENTIDNAME");
            faceplating2.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150);
            faceplating2.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
            popfaceplating2.SelectPopupCondition = faceplating2;
    



            // 표면도금 3
            ConditionItemSelectPopup faceplating3 = new ConditionItemSelectPopup();
            faceplating3.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            faceplating3.SetPopupLayout("PROCESSSEGMENTID", PopupButtonStyles.Ok_Cancel);
            faceplating3.Id = "PROCESSSEGMENTID";
            faceplating3.LabelText = "PROCESSSEGMENTID";
            faceplating3.SearchQuery = new SqlQuery("GetPlatingTypeSegment", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"PLANTID={UserInfo.Current.Plant}", $"PLATINGTYPE={"SurfacePlating"}");
            faceplating3.IsMultiGrid = false;
            faceplating3.DisplayFieldName = "PROCESSSEGMENTNAME";
            faceplating3.ValueFieldName = "PROCESSSEGMENTID";
            faceplating3.LanguageKey = "PROCESSSEGMENTNAME";
            faceplating3.Conditions.AddTextBox("PROCESSSEGMENTIDNAME");
            faceplating3.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150);
            faceplating3.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
            popfaceplating3.SelectPopupCondition = faceplating3;
            popfaceplating3.SelectPopupCondition.SetPopupApplySelection((selectRow, gridRow) => {


                selectRow.AsEnumerable().ForEach(r => {
                    Dictionary<string, object> param = new Dictionary<string, object>();
                    string productDefId = dtProductDEF.Rows[0]["ITEMID"].ToString();
                    string productDefVersion = dtProductDEF.Rows[0]["ITEMVERSION"].ToString();

                    //공정스펙
                    param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                    param.Add("RESOURCEID", productDefId);
                    param.Add("RESOURCEVERSION", productDefVersion);
                    param.Add("PROCESSSEGMENTID", r["PROCESSSEGMENTID"]);
                    param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                    param.Add("SPECCLASSID", "OperationSpec");

                    DataTable dt = SqlExecuter.Query("GetRoutingInspectionItemList", "10002", param);
                    int count = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (count == 3)
                        {
                            break;
                        }
                        if (count == 0)
                        {
                            txtfaceplating3_1.EditValue = Format.GetString(dr["INSPITEMNAME"]);
                            txtfacelow3_1.EditValue = Format.GetString(dr["LSL"]);
                            txtfacehigh3_1.EditValue = Format.GetString(dr["USL"]);
                        }
                        if (count == 1)
                        {
                            txtfaceplating3_2.EditValue = Format.GetString(dr["INSPITEMNAME"]);
                            txtfacelow3_2.EditValue = Format.GetString(dr["LSL"]);
                            txtfacehigh3_2.EditValue = Format.GetString(dr["USL"]);
                        }

                        if (count == 2)
                        {
                            txtfaceplating3_3.EditValue = Format.GetString(dr["INSPITEMNAME"]);
                            txtfacelow3_3.EditValue = Format.GetString(dr["LSL"]);
                            txtfacehigh3_3.EditValue = Format.GetString(dr["USL"]);
                        }
                        count++;
                    }
                });
            });
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
                    if (lb.Name.Contains(strName))
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
                    if (lb.Name.Contains(strName))
                        lb.ForeColor = Color.Red;
                }
            }
        }



        /// <summary>
        /// Combo 초기화 공통
        /// </summary>
        /// <param name="comboBox"></param>
        private void SetSmartComboBox(SmartComboBox comboBox)
        {
            comboBox.DisplayMember = "CODENAME";
            comboBox.ValueMember = "CODEID";
            comboBox.ShowHeader = false;
            comboBox.UseEmptyItem = true;
            comboBox.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
        }

        #endregion


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
                    txtcustomlow1.EditValue = dt.Rows[0]["SPECDETAILFROM"];
                    txtcustomstd1.EditValue = dt.Rows[0]["SPECDETAILTO"];
                    txtcustomhigh1.EditValue = dt.Rows[0]["FROMORIGINAL"];


                }
                else if (i == 2)
                {

                    processid = dt.Rows[1]["PROCESSSEGMENTID"].ToString();
                    popoutplating1.SetValue(dt.Rows[1]["PROCESSSEGMENTID"]);
                    popoutplating1.EditValue = dt.Rows[1]["DESCRIPTION"];

                }
                else if (i == 3)
                {

                    processid = dt.Rows[2]["PROCESSSEGMENTID"].ToString();
                    popoutplating2.SetValue(dt.Rows[2]["PROCESSSEGMENTID"]);
                    popoutplating2.EditValue = dt.Rows[2]["DESCRIPTION"];
                }
                else if (i == 4)
                {
                    txtcustomlow2.EditValue = dt.Rows[3]["SPECDETAILFROM"];
                    txtcustomstd2.EditValue = dt.Rows[3]["SPECDETAILTO"];
                    txtcustomhigh2.EditValue = dt.Rows[3]["FROMORIGINAL"];
                }
                else if (i == 5)
                {

                    processid = dt.Rows[4]["PROCESSSEGMENTID"].ToString();
                    popinnerplating1_1.SetValue(dt.Rows[4]["PROCESSSEGMENTID"]);
                    popinnerplating1_1.EditValue = dt.Rows[4]["DESCRIPTION"];

                }
                else if (i == 6)
                {

                    processid = dt.Rows[5]["PROCESSSEGMENTID"].ToString();
                    popinnerplating1_2.SetValue(dt.Rows[5]["PROCESSSEGMENTID"]);
                    popinnerplating1_2.EditValue = dt.Rows[5]["DESCRIPTION"];

                }
                else if (i == 7)
                {

                    txtcustomlow3.EditValue = dt.Rows[6]["SPECDETAILFROM"];
                    txtcustomstd3.EditValue = dt.Rows[6]["SPECDETAILTO"];
                    txtcustomhigh3.EditValue = dt.Rows[6]["FROMORIGINAL"];
                }
                else if (i == 8)
                {
                    processid = dt.Rows[7]["PROCESSSEGMENTID"].ToString();
                    popinnerplating2_1.SetValue(dt.Rows[7]["PROCESSSEGMENTID"]);
                    popinnerplating2_1.EditValue = dt.Rows[7]["DESCRIPTION"];

                }
                else if (i == 9)
                {
                    processid = dt.Rows[8]["PROCESSSEGMENTID"].ToString();
                    popinnerplating2_2.SetValue(dt.Rows[8]["PROCESSSEGMENTID"]);
                    popinnerplating2_2.EditValue = dt.Rows[8]["DESCRIPTION"];

                }
                else if (i == 10)
                {
                    txtcustomlow4.EditValue = dt.Rows[9]["SPECDETAILFROM"];
                    txtcustomstd4.EditValue = dt.Rows[9]["SPECDETAILTO"];
                    txtcustomhigh4.EditValue = dt.Rows[9]["FROMORIGINAL"];
                }
                else if (i == 11)
                {
                    processid = dt.Rows[10]["PROCESSSEGMENTID"].ToString();
                    popinnerplating3_1.SetValue(dt.Rows[10]["PROCESSSEGMENTID"]);
                    popinnerplating3_1.EditValue = dt.Rows[10]["DESCRIPTION"];
                }
                else if (i == 12)
                {
                    processid = dt.Rows[11]["PROCESSSEGMENTID"].ToString();
                    popinnerplating3_2.SetValue(dt.Rows[11]["PROCESSSEGMENTID"]);
                    popinnerplating3_2.EditValue = dt.Rows[11]["DESCRIPTION"];

                }
                else if (i == 13)
                {
                    processid = dt.Rows[12]["PROCESSSEGMENTID"].ToString();
                    popfaceplating1.SetValue(dt.Rows[12]["PROCESSSEGMENTID"]);
                    popfaceplating1.EditValue = dt.Rows[12]["DESCRIPTION"];
                    cbPlatingType1_4.EditValue = dt.Rows[12]["SPECDETAILFROM"];
                    cbPlatingType1_5.EditValue = dt.Rows[12]["SPECDETAILTO"];
                    txtface1_1.EditValue = dt.Rows[12]["FROMORIGINAL"];
                    txtface1_2.EditValue = dt.Rows[12]["TOORIGINAL"];

                }
                else if (i == 14)
                {
                    processid = dt.Rows[13]["PROCESSSEGMENTID"].ToString();
                    popfaceplating2.SetValue(dt.Rows[13]["PROCESSSEGMENTID"]);
                    popfaceplating2.EditValue = dt.Rows[13]["DESCRIPTION"];
                    cbPlatingType2_4.EditValue = dt.Rows[13]["SPECDETAILFROM"];
                    cbPlatingType2_5.EditValue = dt.Rows[13]["SPECDETAILTO"];
                    txtface2_1.EditValue = dt.Rows[13]["FROMORIGINAL"];
                    txtface2_2.EditValue = dt.Rows[13]["TOORIGINAL"];
                }
                else if (i == 15)
                {
                    processid = dt.Rows[14]["PROCESSSEGMENTID"].ToString();
                    popfaceplating3.SetValue(dt.Rows[14]["PROCESSSEGMENTID"]);
                    popfaceplating3.EditValue = dt.Rows[14]["DESCRIPTION"];
                    cbPlatingType3_4.EditValue = dt.Rows[14]["SPECDETAILFROM"];
                    cbPlatingType3_5.EditValue = dt.Rows[14]["SPECDETAILTO"];
                    txtface3_1.EditValue = dt.Rows[14]["FROMORIGINAL"];
                    txtface3_2.EditValue = dt.Rows[14]["TOORIGINAL"];
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
                for (int i = 1; i <= 15; i++)
                {
                    DataRow drCopperPlating = dtRtn.NewRow();
                    drCopperPlating["DETAILTYPE"] = "CopperPlating";
                    drCopperPlating["SEQUENCE"] = i.ToString();

                    if (i == 1)
                    {
                    drCopperPlating["DETAILNAME"] = "PLATINGSPEC" + i.ToString();
                    drCopperPlating["SPECDETAILFROM"] = txtcustomlow1.EditValue;
                    drCopperPlating["SPECDETAILTO"] = txtcustomstd1.EditValue;
                    drCopperPlating["FROMORIGINAL"] = txtcustomhigh1.EditValue;

                    }
                    else if (i == 2)
                    {
                    drCopperPlating["DETAILNAME"] = "PLATINGSPEC" + i.ToString();
                    drCopperPlating["DESCRIPTION"] = popoutplating1.GetValue();

                    }
                    else if (i == 3)
                    {



                    drCopperPlating["DETAILNAME"] = "PLATINGSPEC" + i.ToString();
                    drCopperPlating["DESCRIPTION"] = popoutplating2.GetValue();

                    }
                    else if (i == 4)
                    {

                    drCopperPlating["DETAILNAME"] = "PLATINGSPEC" + i.ToString();
                    drCopperPlating["SPECDETAILFROM"] = txtcustomlow2.EditValue;
                    drCopperPlating["SPECDETAILTO"] = txtcustomstd2.EditValue;
                    drCopperPlating["FROMORIGINAL"] = txtcustomhigh2.EditValue;

                    }
                    else if (i == 5)
                    {

                    drCopperPlating["DETAILNAME"] = "PLATINGSPEC" + i.ToString();
                    drCopperPlating["DESCRIPTION"] = popinnerplating1_1.GetValue();

                    }
                    else if (i == 6)
                    {

                    drCopperPlating["DETAILNAME"] = "PLATINGSPEC" + i.ToString();
                    drCopperPlating["DESCRIPTION"] = popinnerplating1_2.GetValue();

                    }
                    else if (i == 7)
                    {
                    drCopperPlating["DETAILNAME"] = "PLATINGSPEC" + i.ToString();
                    drCopperPlating["SPECDETAILFROM"] = txtcustomlow3.EditValue;
                    drCopperPlating["SPECDETAILTO"] = txtcustomstd3.EditValue;
                    drCopperPlating["FROMORIGINAL"] = txtcustomhigh3.EditValue;

                    }
                    else if (i == 8)
                    
                    {

                    drCopperPlating["DETAILNAME"] = "PLATINGSPEC" + i.ToString();
                    drCopperPlating["DESCRIPTION"] = popinnerplating2_1.GetValue();



                    }
                    else if (i == 9)
                    {

                    drCopperPlating["DETAILNAME"] = "PLATINGSPEC" + i.ToString();
                    drCopperPlating["DESCRIPTION"] = popinnerplating2_2.GetValue();



                        
                    }
                    else if (i == 10)
                    {

                    drCopperPlating["DETAILNAME"] = "PLATINGSPEC" + i.ToString();
                    drCopperPlating["SPECDETAILFROM"] = txtcustomlow4.EditValue;
                    drCopperPlating["SPECDETAILTO"] = txtcustomstd4.EditValue;
                    drCopperPlating["FROMORIGINAL"] = txtcustomhigh4.EditValue;

                    }
                    else if (i == 11)
                    {

                    drCopperPlating["DETAILNAME"] = "PLATINGSPEC" + i.ToString();
                    drCopperPlating["DESCRIPTION"] = popinnerplating3_1.GetValue();



                    }
                    else if (i == 12)
                {
                    drCopperPlating["DETAILNAME"] = "PLATINGSPEC" + i.ToString();
                    drCopperPlating["DESCRIPTION"] = popinnerplating3_2.GetValue();
                }
                    else if (i == 13)
                    {

                    drCopperPlating["DETAILNAME"] = "PLATINGSPEC" + i.ToString();
                    drCopperPlating["DESCRIPTION"] = popfaceplating1.GetValue();
                    drCopperPlating["SPECDETAILFROM"] = cbPlatingType1_4.EditValue;
                    drCopperPlating["SPECDETAILTO"] = cbPlatingType1_5.EditValue;
                    drCopperPlating["FROMORIGINAL"] = txtface1_1.EditValue;
                    drCopperPlating["TOORIGINAL"] = txtface1_2.EditValue;


                }
                    else if (i == 14)
                    {

                    drCopperPlating["DETAILNAME"] = "PLATINGSPEC" + i.ToString();
                    drCopperPlating["DESCRIPTION"] = popfaceplating2.GetValue();
                    drCopperPlating["SPECDETAILFROM"] = cbPlatingType2_4.EditValue;
                    drCopperPlating["SPECDETAILTO"] = cbPlatingType2_5.EditValue;
                    drCopperPlating["FROMORIGINAL"] = txtface2_1.EditValue;
                    drCopperPlating["TOORIGINAL"] = txtface2_2.EditValue;

                }
                    else if (i == 15)
                    {

                    drCopperPlating["DETAILNAME"] = "PLATINGSPEC" + i.ToString();
                    drCopperPlating["DESCRIPTION"] = popfaceplating3.GetValue();
                    drCopperPlating["SPECDETAILFROM"] = cbPlatingType3_4.EditValue;
                    drCopperPlating["SPECDETAILTO"] = cbPlatingType3_4.EditValue;
                    drCopperPlating["FROMORIGINAL"] = txtface3_1.EditValue;
                    drCopperPlating["TOORIGINAL"] = txtface3_2.EditValue;

                }

                dtRtn.Rows.Add(drCopperPlating);

                }
                return dtRtn;

            
        }

        #region event

        #endregion

        public void parameterreturn(Dictionary<string, object> values)
        {
            dtProductDEF = new DataTable();

            dtProductDEF.Columns.Add("ITEMID");
            dtProductDEF.Columns.Add("ITEMVERSION");
            DataRow dr =  dtProductDEF.NewRow();
            dr["ITEMID"] = values["ITEMID"];
            dr["ITEMVERSION"] = values["ITEMVERSION"];
   
            dtProductDEF.Rows.Add(dr);

            popinnerplating3_1.SelectPopupCondition.SearchQuery = new SqlQuery("GetPlatingTypeSegment", "10002", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"PLANTID={UserInfo.Current.Plant}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLATINGTYPE={"InnerCopperPlating3"}",
                    $"PLATINGTYPE2={"Inner2AndInner3Plating"}", $"RESOURCEID={dtProductDEF.Rows[0]["ITEMID"]}", $"RESOURCEVERSION={dtProductDEF.Rows[0]["ITEMVERSION"]}");
            popinnerplating3_1.SelectPopupCondition.SetPopupApplySelection((selectRow, gridRow) => {


                selectRow.AsEnumerable().ForEach(r => {

                    popinnerplating3_1.SetValue(Format.GetString(r["PROCESSSEGMENTID"]));
                    processid = Format.GetString(r["PROCESSSEGMENTID"]);

                });
            });
        
            popinnerplating3_2.SelectPopupCondition.SearchQuery = new SqlQuery("GetPlatingTypeSegment", "10002", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"PLANTID={UserInfo.Current.Plant}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLATINGTYPE={"InnerCopperPlating3"}",
                    $"PLATINGTYPE2={"Inner2AndInner3Plating"}", $"RESOURCEID={dtProductDEF.Rows[0]["ITEMID"]}", $"RESOURCEVERSION={dtProductDEF.Rows[0]["ITEMVERSION"]}");
            popinnerplating3_2.SelectPopupCondition.SetPopupApplySelection((selectRow, gridRow) => {


                selectRow.AsEnumerable().ForEach(r => {

                    popinnerplating3_2.SetValue(Format.GetString(r["PROCESSSEGMENTID"]));
                    processid = Format.GetString(r["PROCESSSEGMENTID"]);

                });
            });
            popinnerplating2_1.SelectPopupCondition.SearchQuery = new SqlQuery("GetPlatingTypeSegment", "10002", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"PLANTID={UserInfo.Current.Plant}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLATINGTYPE={"InnerCopperPlating2"}",
                    $"PLATINGTYPE2={"Inner2AndInner3Plating"}", $"RESOURCEID={dtProductDEF.Rows[0]["ITEMID"]}", $"RESOURCEVERSION={dtProductDEF.Rows[0]["ITEMVERSION"]}");
            popinnerplating2_1.SelectPopupCondition.SetPopupApplySelection((selectRow, gridRow) => {


                selectRow.AsEnumerable().ForEach(r => {

                    popinnerplating2_1.SetValue(Format.GetString(r["PROCESSSEGMENTID"]));
                    processid = Format.GetString(r["PROCESSSEGMENTID"]);

                });
            });
            popinnerplating2_2.SelectPopupCondition.SearchQuery = new SqlQuery("GetPlatingTypeSegment", "10002", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"PLANTID={UserInfo.Current.Plant}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLATINGTYPE={"InnerCopperPlating2"}",
                    $"PLATINGTYPE2={"Inner2AndInner3Plating"}", $"RESOURCEID={dtProductDEF.Rows[0]["ITEMID"]}", $"RESOURCEVERSION={dtProductDEF.Rows[0]["ITEMVERSION"]}");
            popinnerplating2_2.SelectPopupCondition.SetPopupApplySelection((selectRow, gridRow) => {


                selectRow.AsEnumerable().ForEach(r => {

                    popinnerplating2_2.SetValue(Format.GetString(r["PROCESSSEGMENTID"]));
                    processid = Format.GetString(r["PROCESSSEGMENTID"]);

                });
            });
            popinnerplating1_1.SelectPopupCondition.SearchQuery = new SqlQuery("GetPlatingTypeSegment", "10002", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"PLANTID={UserInfo.Current.Plant}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLATINGTYPE={"InnerCopperPlating1"}",
                    $"PLATINGTYPE2={"OuterAndInner1Plating"}", $"RESOURCEID={dtProductDEF.Rows[0]["ITEMID"]}", $"RESOURCEVERSION={dtProductDEF.Rows[0]["ITEMVERSION"]}");
            popinnerplating1_1.SelectPopupCondition.SetPopupApplySelection((selectRow, gridRow) => {


                selectRow.AsEnumerable().ForEach(r => {

                    popinnerplating1_1.SetValue(Format.GetString(r["PROCESSSEGMENTID"]));
                    processid = Format.GetString(r["PROCESSSEGMENTID"]);

                });
            });
            popinnerplating1_2.SelectPopupCondition.SearchQuery = new SqlQuery("GetPlatingTypeSegment", "10002", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"PLANTID={UserInfo.Current.Plant}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLATINGTYPE={"InnerCopperPlating1"}",
                    $"PLATINGTYPE2={"InnerCopperPlaOuterAndInner1Platingting3"}", $"RESOURCEID={dtProductDEF.Rows[0]["ITEMID"]}", $"RESOURCEVERSION={dtProductDEF.Rows[0]["ITEMVERSION"]}");
            popinnerplating1_2.SelectPopupCondition.SetPopupApplySelection((selectRow, gridRow) => {


                selectRow.AsEnumerable().ForEach(r => {

                    popinnerplating1_2.SetValue(Format.GetString(r["PROCESSSEGMENTID"]));
                    processid = Format.GetString(r["PROCESSSEGMENTID"]);

                });
            });
            popoutplating1.SelectPopupCondition.SearchQuery = new SqlQuery("GetPlatingTypeSegment", "10002", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"PLANTID={UserInfo.Current.Plant}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLATINGTYPE={"OuterCopperPlating"}",
                    $"PLATINGTYPE2={"OuterAndInner1Plating"}", $"RESOURCEID={dtProductDEF.Rows[0]["ITEMID"]}", $"RESOURCEVERSION={dtProductDEF.Rows[0]["ITEMVERSION"]}");
            popoutplating1.SelectPopupCondition.SetPopupApplySelection((selectRow, gridRow) => {


                selectRow.AsEnumerable().ForEach(r => {

                    popoutplating1.SetValue(Format.GetString(r["PROCESSSEGMENTID"]));
                    processid = Format.GetString(r["PROCESSSEGMENTID"]);

                });
            });
            popoutplating2.SelectPopupCondition.SearchQuery = new SqlQuery("GetPlatingTypeSegment", "10002", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"PLANTID={UserInfo.Current.Plant}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLATINGTYPE={"OuterCopperPlating"}",
                    $"PLATINGTYPE2={"OuterAndInner1Plating"}", $"RESOURCEID={dtProductDEF.Rows[0]["ITEMID"]}", $"RESOURCEVERSION={dtProductDEF.Rows[0]["ITEMVERSION"]}");
            popoutplating2.SelectPopupCondition.SetPopupApplySelection((selectRow, gridRow) => {


                selectRow.AsEnumerable().ForEach(r => {

                popoutplating2.SetValue(Format.GetString(r["PROCESSSEGMENTID"]));
                    processid = Format.GetString(r["PROCESSSEGMENTID"]);


                });
            });
            popfaceplating1.SelectPopupCondition.SearchQuery = new SqlQuery("GetPlatingTypeSegment", "10002", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"PLANTID={UserInfo.Current.Plant}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLATINGTYPE={"SurfacePlating"}",
                    $"RESOURCEID={dtProductDEF.Rows[0]["ITEMID"]}", $"RESOURCEVERSION={dtProductDEF.Rows[0]["ITEMVERSION"]}");
            popfaceplating1.SelectPopupCondition.SetPopupApplySelection((selectRow, gridRow) => {


                selectRow.AsEnumerable().ForEach(r => {

                    popfaceplating1.SetValue(Format.GetString(r["PROCESSSEGMENTID"]));
                    processid = Format.GetString(r["PROCESSSEGMENTID"]);

                });
            });
            popfaceplating2.SelectPopupCondition.SearchQuery = new SqlQuery("GetPlatingTypeSegment", "10002", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"PLANTID={UserInfo.Current.Plant}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLATINGTYPE={"SurfacePlating"}",
                    $"RESOURCEID={dtProductDEF.Rows[0]["ITEMID"]}", $"RESOURCEVERSION={dtProductDEF.Rows[0]["ITEMVERSION"]}");
            popfaceplating2.SelectPopupCondition.SetPopupApplySelection((selectRow, gridRow) => {


                selectRow.AsEnumerable().ForEach(r => {

                    popfaceplating2.SetValue(Format.GetString(r["PROCESSSEGMENTID"]));
                    processid = Format.GetString(r["PROCESSSEGMENTID"]);

                });
            });
            popfaceplating3.SelectPopupCondition.SearchQuery = new SqlQuery("GetPlatingTypeSegment", "10002", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"PLANTID={UserInfo.Current.Plant}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLATINGTYPE={"SurfacePlating"}",
                    $"RESOURCEID={dtProductDEF.Rows[0]["ITEMID"]}", $"RESOURCEVERSION={dtProductDEF.Rows[0]["ITEMVERSION"]}");
            popfaceplating3.SelectPopupCondition.SetPopupApplySelection((selectRow, gridRow) => {


                selectRow.AsEnumerable().ForEach(r => {

                    popfaceplating3.SetValue(Format.GetString(r["PROCESSSEGMENTID"]));
                    processid = Format.GetString(r["PROCESSSEGMENTID"]);

                });
            });
            popoutplating2.EditValueChanged += Popoutplating2_EditValueChanged;
            popoutplating1.EditValueChanged += Popoutplating1_EditValueChanged;
          
            popinnerplating3_1.EditValueChanged += popinnerplating3_1_EditValueChanged;
            popinnerplating3_2.EditValueChanged += popinnerplating3_2_EditValueChanged;
            popinnerplating2_1.EditValueChanged += popinnerplating2_1_EditValueChanged;
            popinnerplating2_2.EditValueChanged += popinnerplating2_2_EditValueChanged;
            popinnerplating1_1.EditValueChanged += popinnerplating1_1_EditValueChanged;
            popinnerplating1_2.EditValueChanged += popinnerplating1_2_EditValueChanged;
            popfaceplating1.EditValueChanged += Popfaceplating1_EditValueChanged;
            popfaceplating2.EditValueChanged += Popfaceplating2_EditValueChanged;
            popfaceplating3.EditValueChanged += Popfaceplating3_EditValueChanged;
        }

        #region 표면도금

        private void Popfaceplating3_EditValueChanged(object sender, EventArgs e)
        {
            if (popfaceplating3.EditValue.Equals(""))
            {
                txtfaceplating3_1.EditValue = string.Empty;
                txtfacelow3_1.EditValue = string.Empty;
                txtfacehigh3_1.EditValue = string.Empty;
                txtfaceplating3_2.EditValue = string.Empty;
                txtfacelow3_2.EditValue = string.Empty;
                txtfacehigh3_2.EditValue = string.Empty;
                txtfaceplating3_3.EditValue = string.Empty;
                txtfacelow3_3.EditValue = string.Empty;
                txtfacehigh3_3.EditValue = string.Empty;
       
            }
            else
            {

                Dictionary<string, object> Param = new Dictionary<string, object>();
                Param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                Param.Add("PLATINGTYPE", "SurfacePlating");
                Param.Add("RESOURCEID", dtProductDEF.Rows[0]["ITEMID"]);
                Param.Add("RESOURCEVERSION", dtProductDEF.Rows[0]["ITEMVERSION"]);
                Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                Param.Add("PROCESSSEGID", popfaceplating3.GetValue());
                DataTable dtCopperPlatingType = SqlExecuter.Query("GetPlatingTypeSegment", "10001", Param);
                for (int i = 0; i < dtCopperPlatingType.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        txtfaceplating3_1.EditValue = Format.GetString(dtCopperPlatingType.Rows[i]["INSPITEMNAME"]);
                        txtfacelow3_1.EditValue = Format.GetString(dtCopperPlatingType.Rows[i]["LSL"]);
                        txtfacehigh3_1.EditValue = Format.GetString(dtCopperPlatingType.Rows[i]["USL"]);
                    }
                    if (i == 1)
                    {
                        txtfaceplating3_2.EditValue = Format.GetString(dtCopperPlatingType.Rows[i]["INSPITEMNAME"]);
                        txtfacelow3_2.EditValue = Format.GetString(dtCopperPlatingType.Rows[i]["LSL"]);
                        txtfacehigh3_2.EditValue = Format.GetString(dtCopperPlatingType.Rows[i]["USL"]);
                    }
                    if (i == 2)
                    {
                        txtfaceplating3_3.EditValue = Format.GetString(dtCopperPlatingType.Rows[i]["INSPITEMNAME"]);
                        txtfacelow3_3.EditValue = Format.GetString(dtCopperPlatingType.Rows[i]["LSL"]);
                        txtfacehigh3_3.EditValue = Format.GetString(dtCopperPlatingType.Rows[i]["USL"]);
                    }

                }
            }

        }

        private void Popfaceplating2_EditValueChanged(object sender, EventArgs e)
        {
            if (popfaceplating2.EditValue.Equals(""))
            {
                txtfaceplating2_1.EditValue = string.Empty;
                txtfacelow2_1.EditValue = string.Empty;
                txtfacehigh2_1.EditValue = string.Empty;
                txtfaceplating2_2.EditValue = string.Empty;
                txtfacelow2_2.EditValue = string.Empty;
                txtfacehigh2_2.EditValue = string.Empty;
                txtfaceplating2_3.EditValue = string.Empty;
                txtfacelow2_3.EditValue = string.Empty;
                txtfacehigh2_3.EditValue = string.Empty;
       

            }
            else
            {
                Dictionary<string, object> Param = new Dictionary<string, object>();
                Param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                Param.Add("PLATINGTYPE", "SurfacePlating");
                Param.Add("RESOURCEID", dtProductDEF.Rows[0]["ITEMID"]);
                Param.Add("RESOURCEVERSION", dtProductDEF.Rows[0]["ITEMVERSION"]);
                Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                Param.Add("PROCESSSEGID", popfaceplating2.GetValue());
                DataTable dtCopperPlatingType = SqlExecuter.Query("GetPlatingTypeSegment", "10001", Param);
                for (int i = 0; i < dtCopperPlatingType.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        txtfaceplating2_1.EditValue = Format.GetString(dtCopperPlatingType.Rows[i]["INSPITEMNAME"]);
                        txtfacelow2_1.EditValue = Format.GetString(dtCopperPlatingType.Rows[i]["LSL"]);
                        txtfacehigh2_1.EditValue = Format.GetString(dtCopperPlatingType.Rows[i]["USL"]);
                    }
                    if (i == 1)
                    {
                        txtfaceplating2_2.EditValue = Format.GetString(dtCopperPlatingType.Rows[i]["INSPITEMNAME"]);
                        txtfacelow2_2.EditValue = Format.GetString(dtCopperPlatingType.Rows[i]["LSL"]);
                        txtfacehigh2_2.EditValue = Format.GetString(dtCopperPlatingType.Rows[i]["USL"]);
                    }
                    if (i == 2)
                    {
                        txtfaceplating2_3.EditValue = Format.GetString(dtCopperPlatingType.Rows[i]["INSPITEMNAME"]);
                        txtfacelow2_3.EditValue = Format.GetString(dtCopperPlatingType.Rows[i]["LSL"]);
                        txtfacehigh2_3.EditValue = Format.GetString(dtCopperPlatingType.Rows[i]["USL"]);
                    }

                }
            }

        }

        private void Popfaceplating1_EditValueChanged(object sender, EventArgs e)
        {
            if (popfaceplating1.EditValue.Equals(""))
            {
                txtfaceplating1_1.EditValue = string.Empty;
                txtfacelow1_1.EditValue = string.Empty;
                txtfacehigh1_1.EditValue = string.Empty;
                txtfaceplating1_2.EditValue = string.Empty;
                txtfacelow1_2.EditValue = string.Empty;
                txtfacehigh1_2.EditValue = string.Empty;
                txtfaceplating1_3.EditValue = string.Empty;
                txtfacelow1_3.EditValue = string.Empty;
                txtfacehigh1_3.EditValue = string.Empty;
      

            }
            else { 
            Dictionary<string, object> Param = new Dictionary<string, object>();
            Param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            Param.Add("PLATINGTYPE", "SurfacePlating");
            Param.Add("RESOURCEID", dtProductDEF.Rows[0]["ITEMID"]);
            Param.Add("RESOURCEVERSION", dtProductDEF.Rows[0]["ITEMVERSION"]);
            Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            Param.Add("PROCESSSEGID", popfaceplating1.GetValue());
            DataTable dtCopperPlatingType = SqlExecuter.Query("GetPlatingTypeSegment", "10001", Param);
                for (int i = 0; i < dtCopperPlatingType.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        txtfaceplating1_1.EditValue = Format.GetString(dtCopperPlatingType.Rows[i]["INSPITEMNAME"]);
                        txtfacelow1_1.EditValue = Format.GetString(dtCopperPlatingType.Rows[i]["LSL"]);
                        txtfacehigh1_1.EditValue = Format.GetString(dtCopperPlatingType.Rows[i]["USL"]);
                    }
                    if (i == 1)
                    {
                        txtfaceplating1_2.EditValue = Format.GetString(dtCopperPlatingType.Rows[i]["INSPITEMNAME"]);
                        txtfacelow1_2.EditValue = Format.GetString(dtCopperPlatingType.Rows[i]["LSL"]);
                        txtfacehigh1_2.EditValue = Format.GetString(dtCopperPlatingType.Rows[i]["USL"]);
                    }
                    if (i == 2)
                    {
                        txtfaceplating1_3.EditValue = Format.GetString(dtCopperPlatingType.Rows[i]["INSPITEMNAME"]);
                        txtfacelow1_3.EditValue = Format.GetString(dtCopperPlatingType.Rows[i]["LSL"]);
                        txtfacehigh1_3.EditValue = Format.GetString(dtCopperPlatingType.Rows[i]["USL"]);
                    }
                }
            }
        }
        #endregion

        #region 외층동도금
        private void Popoutplating2_EditValueChanged(object sender, EventArgs e)
        {



            if(popoutplating2.EditValue.Equals(""))
               {
                txthole1_1.EditValue = string.Empty;
                txthole1_2.EditValue = string.Empty;
                txtfoil1_1.EditValue = string.Empty;
                txtfoil1_2.EditValue = string.Empty;
                txtdimple1_2.EditValue = string.Empty;
                txtdimple1_1.EditValue = string.Empty;
                txtoverfill1_1.EditValue = string.Empty;
                txtoverfill1_2.EditValue = string.Empty;
                txtoutspeclow2.EditValue = string.Empty;
                txtoutspecstd2.EditValue = string.Empty;
                txtoutspechigh2.EditValue = string.Empty;
                processid = "";
            }
            
            else if(!popoutplating2.EditValue.Equals(""))
            {
                Dictionary<string, object> Param = new Dictionary<string, object>();
                Param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                Param.Add("RESOURCEID", dtProductDEF.Rows[0]["ITEMID"]);
                Param.Add("RESOURCEVERSION", dtProductDEF.Rows[0]["ITEMVERSION"]);
                Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                Param.Add("PROCESSSEGID", popoutplating2.GetValue());
                DataTable dtCopperPlatingType = SqlExecuter.Query("GetPlatingTypeSegment", "10001", Param);
                if (dtCopperPlatingType.Rows.Count > 0)
                {
                    txtoutspeclow2.EditValue = Format.GetString(dtCopperPlatingType.Rows[0]["LSL"]);
                    txtoutspecstd2.EditValue = Format.GetString(dtCopperPlatingType.Rows[0]["SL"]);
                    txtoutspechigh2.EditValue = Format.GetString(dtCopperPlatingType.Rows[0]["USL"]);
                }
                DataTable dtsemgment = imspitem(processid);
                foreach (DataRow dr in dtsemgment.Rows)
                {



                    if (dr["INSPITEMID"].ToString().Equals("0257"))
                    {
                        txthole1_1.EditValue = Format.GetString(dr["LSL"]);
                        txthole1_2.EditValue = Format.GetString(dr["USL"]);
                    }
                    if (dr["INSPITEMID"].ToString().Equals("0258"))
                    {
                        txtfoil1_1.EditValue = Format.GetString(dr["LSL"]);
                        txtfoil1_2.EditValue = Format.GetString(dr["USL"]);
                    }
                    if (dr["INSPITEMID"].ToString().Equals("0259"))
                    {
                        txtdimple1_1.EditValue = Format.GetString(dr["LSL"]);
                        txtdimple1_2.EditValue = Format.GetString(dr["USL"]);
                    }
                    if (dr["INSPITEMID"].ToString().Equals("0256"))
                    {
                        txtoverfill1_1.EditValue = Format.GetString(dr["LSL"]);
                        txtoverfill1_2.EditValue = Format.GetString(dr["USL"]);
                    }
                }
            }

        }

        private void Popoutplating1_EditValueChanged(object sender, EventArgs e)
        {
            if (popoutplating1.EditValue.Equals(""))
            {
                if (popoutplating2.EditValue.Equals(""))
                {
                    txthole1_1.EditValue = string.Empty;
                    txthole1_2.EditValue = string.Empty;
                    txtfoil1_1.EditValue = string.Empty;
                    txtfoil1_2.EditValue = string.Empty;
                    txtdimple1_2.EditValue = string.Empty;
                    txtdimple1_1.EditValue = string.Empty;
                    txtoverfill1_1.EditValue = string.Empty;
                    txtoverfill1_2.EditValue = string.Empty;

                }
                txtoutspeclow1.EditValue = string.Empty;
                txtoutspecstd1.EditValue = string.Empty;
                txtoutspechigh1.EditValue = string.Empty;
                processid = "";
            }
            else
            {
       

                Dictionary<string, object> Param = new Dictionary<string, object>();
                Param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                Param.Add("RESOURCEID", dtProductDEF.Rows[0]["ITEMID"]);
                Param.Add("RESOURCEVERSION", dtProductDEF.Rows[0]["ITEMVERSION"]);
                Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                Param.Add("PROCESSSEGID", popoutplating1.GetValue());
                DataTable dtCopperPlatingType = SqlExecuter.Query("GetPlatingTypeSegment", "10001", Param);
                if (dtCopperPlatingType.Rows.Count > 0)
                {
                    txtoutspeclow1.EditValue = Format.GetString(dtCopperPlatingType.Rows[0]["LSL"]);
                    txtoutspecstd1.EditValue = Format.GetString(dtCopperPlatingType.Rows[0]["SL"]);
                    txtoutspechigh1.EditValue = Format.GetString(dtCopperPlatingType.Rows[0]["USL"]);
                }
                DataTable dtsemgment = imspitem(processid);
                foreach (DataRow dr in dtsemgment.Rows)
                {
                    if (dr["INSPITEMID"].ToString().Equals("0257"))
                    {
                        txthole1_1.EditValue = Format.GetString(dr["LSL"]);
                        txthole1_2.EditValue = Format.GetString(dr["USL"]);
                    }
                    if (dr["INSPITEMID"].ToString().Equals("0258"))
                    {
                        txtfoil1_1.EditValue = Format.GetString(dr["LSL"]);
                        txtfoil1_2.EditValue = Format.GetString(dr["USL"]);
                    }
                    if (dr["INSPITEMID"].ToString().Equals("0259"))
                    {
                        txtdimple1_1.EditValue = Format.GetString(dr["LSL"]);
                        txtdimple1_2.EditValue = Format.GetString(dr["USL"]);
                    }
                    if (dr["INSPITEMID"].ToString().Equals("0256"))
                    {
                        txtoverfill1_1.EditValue = Format.GetString(dr["LSL"]);
                        txtoverfill1_2.EditValue = Format.GetString(dr["USL"]);
                    }
                }
            }
        }
        #endregion

        #region 내층동도금3
        private void popinnerplating3_1_EditValueChanged(object sender, EventArgs e)
        {

            if (popinnerplating3_1.EditValue.Equals(""))
            {
                if (popinnerplating3_2.EditValue.Equals(""))
                {
                    txthole4_1.EditValue = string.Empty;
                    txthole4_2.EditValue = string.Empty;
                    txtfoil4_1.EditValue = string.Empty;
                    txtfoil4_2.EditValue = string.Empty;
                    txtdimple4_2.EditValue = string.Empty;
                    txtdimple4_1.EditValue = string.Empty;
                    txtoverfill4_1.EditValue = string.Empty;
                    txtoverfill4_2.EditValue = string.Empty;
                }
                txtinner3speclow1.EditValue = string.Empty;
                txtinner3specstd1.EditValue = string.Empty;
                txtinner3spechigh1.EditValue = string.Empty;
                processid = "";
            }
            else
            {
                Dictionary<string, object> Param = new Dictionary<string, object>();
                Param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                Param.Add("RESOURCEID", dtProductDEF.Rows[0]["ITEMID"]);
                Param.Add("RESOURCEVERSION", dtProductDEF.Rows[0]["ITEMVERSION"]);
                Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                Param.Add("PROCESSSEGID", popinnerplating3_1.GetValue());
                DataTable dtCopperPlatingType = SqlExecuter.Query("GetPlatingTypeSegment", "10001", Param);
                if (dtCopperPlatingType.Rows.Count > 0)
                {
                    txtinner3speclow1.EditValue = Format.GetString(dtCopperPlatingType.Rows[0]["LSL"]);
                    txtinner3specstd1.EditValue = Format.GetString(dtCopperPlatingType.Rows[0]["SL"]);
                    txtinner3spechigh1.EditValue = Format.GetString(dtCopperPlatingType.Rows[0]["USL"]);
                }
                DataTable dtsemgment = imspitem(processid);
                foreach (DataRow dr in dtsemgment.Rows)
                {
                    if (dr["INSPITEMID"].ToString().Equals("0257"))
                    {
                        txthole4_1.EditValue = Format.GetString(dr["LSL"]);
                        txthole4_2.EditValue = Format.GetString(dr["USL"]);
                    }
                    if (dr["INSPITEMID"].ToString().Equals("0258"))
                    {
                        txtfoil4_1.EditValue = Format.GetString(dr["LSL"]);
                        txtfoil4_2.EditValue = Format.GetString(dr["USL"]);
                    }
                    if (dr["INSPITEMID"].ToString().Equals("0259"))
                    {
                        txtdimple4_1.EditValue = Format.GetString(dr["LSL"]);
                        txtdimple4_2.EditValue = Format.GetString(dr["USL"]);
                    }
                    if (dr["INSPITEMID"].ToString().Equals("0256"))
                    {
                        txtoverfill4_1.EditValue = Format.GetString(dr["LSL"]);
                        txtoverfill4_2.EditValue = Format.GetString(dr["USL"]);
                    }
                }
            }
        }

        private void popinnerplating3_2_EditValueChanged(object sender, EventArgs e)
        {


            if (popinnerplating3_2.EditValue.Equals(""))
            {
                txthole4_1.EditValue = string.Empty;
                txthole4_2.EditValue = string.Empty;
                txtfoil4_1.EditValue = string.Empty;
                txtfoil4_2.EditValue = string.Empty;
                txtdimple4_2.EditValue = string.Empty;
                txtdimple4_1.EditValue = string.Empty;
                txtoverfill4_1.EditValue = string.Empty;
                txtoverfill4_2.EditValue = string.Empty;
                txtinner3speclow2.EditValue = string.Empty;
                txtinner3specstd2.EditValue = string.Empty;
                txtinner3spechigh2.EditValue = string.Empty;
                processid = "";
            }
            else
            {


                Dictionary<string, object> Param = new Dictionary<string, object>();
                Param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                Param.Add("RESOURCEID", dtProductDEF.Rows[0]["ITEMID"]);
                Param.Add("RESOURCEVERSION", dtProductDEF.Rows[0]["ITEMVERSION"]);
                Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                Param.Add("PROCESSSEGID", popinnerplating3_2.GetValue());
                DataTable dtCopperPlatingType = SqlExecuter.Query("GetPlatingTypeSegment", "10001", Param);
                if (dtCopperPlatingType.Rows.Count > 0)
                {
                    txtinner3speclow2.EditValue = Format.GetString(dtCopperPlatingType.Rows[0]["LSL"]);
                    txtinner3specstd2.EditValue = Format.GetString(dtCopperPlatingType.Rows[0]["SL"]);
                    txtinner3spechigh2.EditValue = Format.GetString(dtCopperPlatingType.Rows[0]["USL"]);
                }
                DataTable dtsemgment = imspitem(processid);
                foreach (DataRow dr in dtsemgment.Rows)
                {
                    if (dr["INSPITEMID"].ToString().Equals("0257"))
                    {
                        txthole4_1.EditValue = Format.GetString(dr["LSL"]);
                        txthole4_2.EditValue = Format.GetString(dr["USL"]);
                    }
                    if (dr["INSPITEMID"].ToString().Equals("0258"))
                    {
                        txtfoil4_1.EditValue = Format.GetString(dr["LSL"]);
                        txtfoil4_2.EditValue = Format.GetString(dr["USL"]);
                    }
                    if (dr["INSPITEMID"].ToString().Equals("0259"))
                    {
                        txtdimple4_1.EditValue = Format.GetString(dr["LSL"]);
                        txtdimple4_2.EditValue = Format.GetString(dr["USL"]);
                    }
                    if (dr["INSPITEMID"].ToString().Equals("0256"))
                    {
                        txtoverfill4_1.EditValue = Format.GetString(dr["LSL"]);
                        txtoverfill4_2.EditValue = Format.GetString(dr["USL"]);
                    }
                }
            }
        }

        #endregion

        #region 내층동도금 2
        private void popinnerplating2_2_EditValueChanged(object sender, EventArgs e)

        {
            if (popinnerplating2_2.EditValue.Equals(""))
            {
                txthole3_1.EditValue = string.Empty;
                txthole3_2.EditValue = string.Empty;
                txtfoil3_1.EditValue = string.Empty;
                txtfoil3_2.EditValue = string.Empty;
                txtdimple3_2.EditValue = string.Empty;
                txtdimple3_1.EditValue = string.Empty;
                txtoverfill3_1.EditValue = string.Empty;
                txtoverfill3_2.EditValue = string.Empty;
                txtinner2speclow2.EditValue = string.Empty;
                txtinner2specstd2.EditValue = string.Empty;
                txtinner2spechigh2.EditValue = string.Empty;
                processid = "";
            }
            else
            {


                Dictionary<string, object> Param = new Dictionary<string, object>();
                Param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                Param.Add("RESOURCEID", dtProductDEF.Rows[0]["ITEMID"]);
                Param.Add("RESOURCEVERSION", dtProductDEF.Rows[0]["ITEMVERSION"]);
                Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                Param.Add("PROCESSSEGID", popinnerplating2_2.GetValue());
                DataTable dtCopperPlatingType = SqlExecuter.Query("GetPlatingTypeSegment", "10001", Param);
                if (dtCopperPlatingType.Rows.Count > 0)
                {
                    txtinner2speclow2.EditValue = Format.GetString(dtCopperPlatingType.Rows[0]["LSL"]);
                    txtinner2specstd2.EditValue = Format.GetString(dtCopperPlatingType.Rows[0]["SL"]);
                    txtinner2spechigh2.EditValue = Format.GetString(dtCopperPlatingType.Rows[0]["USL"]);
                }
                DataTable dtsemgment = imspitem(processid);
                foreach (DataRow dr in dtsemgment.Rows)
                {
                    if (dr["INSPITEMID"].ToString().Equals("0257"))
                    {
                        txthole3_1.EditValue = Format.GetString(dr["LSL"]);
                        txthole3_2.EditValue = Format.GetString(dr["USL"]);
                    }
                    if (dr["INSPITEMID"].ToString().Equals("0258"))
                    {
                        txtfoil3_1.EditValue = Format.GetString(dr["LSL"]);
                        txtfoil3_2.EditValue = Format.GetString(dr["USL"]);
                    }
                    if (dr["INSPITEMID"].ToString().Equals("0259"))
                    {
                        txtdimple3_1.EditValue = Format.GetString(dr["LSL"]);
                        txtdimple3_2.EditValue = Format.GetString(dr["USL"]);
                    }
                    if (dr["INSPITEMID"].ToString().Equals("0256"))
                    {
                        txtoverfill3_1.EditValue = Format.GetString(dr["LSL"]);
                        txtoverfill3_2.EditValue = Format.GetString(dr["USL"]);
                    }
                }
            }
        }

        private void popinnerplating2_1_EditValueChanged(object sender, EventArgs e)
        {

        if (popinnerplating2_1.EditValue.Equals(""))
        {
            if (popinnerplating2_2.EditValue.Equals(""))
            {
                txthole3_1.EditValue = string.Empty;
                txthole3_2.EditValue = string.Empty;
                txtfoil3_1.EditValue = string.Empty;
                txtfoil3_2.EditValue = string.Empty;
                txtdimple3_2.EditValue = string.Empty;
                txtdimple3_1.EditValue = string.Empty;
                txtoverfill3_1.EditValue = string.Empty;
                txtoverfill3_2.EditValue = string.Empty;
            }
            txtinner2speclow1.EditValue = string.Empty;
            txtinner2specstd1.EditValue = string.Empty;
            txtinner2spechigh1.EditValue = string.Empty;
                processid = "";
            }
        else
        {

                Dictionary<string, object> Param = new Dictionary<string, object>();
                Param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                Param.Add("RESOURCEID", dtProductDEF.Rows[0]["ITEMID"]);
                Param.Add("RESOURCEVERSION", dtProductDEF.Rows[0]["ITEMVERSION"]);
                Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                Param.Add("PROCESSSEGID", popinnerplating2_1.GetValue());
                DataTable dtCopperPlatingType = SqlExecuter.Query("GetPlatingTypeSegment", "10001", Param);
                if (dtCopperPlatingType.Rows.Count > 0)
                {
                    txtinner2speclow1.EditValue = Format.GetString(dtCopperPlatingType.Rows[0]["LSL"]);
                    txtinner2specstd1.EditValue = Format.GetString(dtCopperPlatingType.Rows[0]["SL"]);
                    txtinner2spechigh1.EditValue = Format.GetString(dtCopperPlatingType.Rows[0]["USL"]);
                }
                DataTable dtsemgment = imspitem(processid);
                foreach (DataRow dr in dtsemgment.Rows)
                {
                    if (dr["INSPITEMID"].ToString().Equals("0257"))
                    {
                        txthole3_1.EditValue = Format.GetString(dr["LSL"]);
                        txthole3_2.EditValue = Format.GetString(dr["USL"]);
                    }
                    if (dr["INSPITEMID"].ToString().Equals("0258"))
                    {
                        txtfoil3_1.EditValue = Format.GetString(dr["LSL"]);
                        txtfoil3_2.EditValue = Format.GetString(dr["USL"]);
                    }
                    if (dr["INSPITEMID"].ToString().Equals("0259"))
                    {
                        txtdimple3_1.EditValue = Format.GetString(dr["LSL"]);
                        txtdimple3_2.EditValue = Format.GetString(dr["USL"]);
                    }
                    if (dr["INSPITEMID"].ToString().Equals("0256"))
                    {
                        txtoverfill3_1.EditValue = Format.GetString(dr["LSL"]);
                        txtoverfill3_2.EditValue = Format.GetString(dr["USL"]);
                    }
                }
            }
        }
        #endregion

        #region 내층동도금 1
        private void popinnerplating1_2_EditValueChanged(object sender, EventArgs e)
        {

            if (popinnerplating1_2.EditValue.Equals(""))
            {
                txthole2_1.EditValue = string.Empty;
                txthole2_2.EditValue = string.Empty;
                txtfoil2_1.EditValue = string.Empty;
                txtfoil2_2.EditValue = string.Empty;
                txtdimple2_2.EditValue = string.Empty;
                txtdimple2_1.EditValue = string.Empty;
                txtoverfill2_1.EditValue = string.Empty;
                txtoverfill2_2.EditValue = string.Empty;
                txtinner1speclow2.EditValue = string.Empty;
                txtinner1specstd2.EditValue = string.Empty;
                txtinner1spechigh2.EditValue = string.Empty;
                processid = "";
            }
            else
            {

                Dictionary<string, object> Param = new Dictionary<string, object>();
                Param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                Param.Add("RESOURCEID", dtProductDEF.Rows[0]["ITEMID"]);
                Param.Add("RESOURCEVERSION", dtProductDEF.Rows[0]["ITEMVERSION"]);
                Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                Param.Add("PROCESSSEGID", popinnerplating1_2.GetValue());
                DataTable dtCopperPlatingType = SqlExecuter.Query("GetPlatingTypeSegment", "10001", Param);
                if (dtCopperPlatingType.Rows.Count > 0)
                {
                    txtinner1speclow2.EditValue = Format.GetString(dtCopperPlatingType.Rows[0]["LSL"]);
                    txtinner1specstd2.EditValue = Format.GetString(dtCopperPlatingType.Rows[0]["SL"]);
                    txtinner1spechigh2.EditValue = Format.GetString(dtCopperPlatingType.Rows[0]["USL"]);
                }
                DataTable dtsemgment = imspitem(processid);
                foreach (DataRow dr in dtsemgment.Rows)
                {
                    if (dr["INSPITEMID"].ToString().Equals("0257"))
                    {
                        txthole2_1.EditValue = Format.GetString(dr["LSL"]);
                        txthole2_2.EditValue = Format.GetString(dr["USL"]);
                    }
                    if (dr["INSPITEMID"].ToString().Equals("0258"))
                    {
                        txtfoil2_1.EditValue = Format.GetString(dr["LSL"]);
                        txtfoil2_2.EditValue = Format.GetString(dr["USL"]);
                    }
                    if (dr["INSPITEMID"].ToString().Equals("0259"))
                    {
                        txtdimple2_1.EditValue = Format.GetString(dr["LSL"]);
                        txtdimple2_2.EditValue = Format.GetString(dr["USL"]);
                    }
                    if (dr["INSPITEMID"].ToString().Equals("0256"))
                    {
                        txtoverfill2_1.EditValue = Format.GetString(dr["LSL"]);
                        txtoverfill2_2.EditValue = Format.GetString(dr["USL"]);
                    }
                }
            }
        }

        private void popinnerplating1_1_EditValueChanged(object sender, EventArgs e)
        {
            if (popinnerplating1_1.EditValue.Equals(""))
            {
                if (popinnerplating1_2.EditValue.Equals(""))
                {
                    txthole2_1.EditValue = string.Empty;
                    txthole2_2.EditValue = string.Empty;
                    txtfoil2_1.EditValue = string.Empty;
                    txtfoil2_2.EditValue = string.Empty;
                    txtdimple2_2.EditValue = string.Empty;
                    txtdimple2_1.EditValue = string.Empty;
                    txtoverfill2_1.EditValue = string.Empty;
                    txtoverfill2_2.EditValue = string.Empty;
                }
                txtinner1speclow1.EditValue = string.Empty;
                txtinner1specstd1.EditValue = string.Empty;
                txtinner1spechigh1.EditValue = string.Empty;
                processid = "";
            }
            else
            {
                Dictionary<string, object> Param = new Dictionary<string, object>();
                Param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                Param.Add("RESOURCEID", dtProductDEF.Rows[0]["ITEMID"]);
                Param.Add("RESOURCEVERSION", dtProductDEF.Rows[0]["ITEMVERSION"]);
                Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                Param.Add("PROCESSSEGID", popinnerplating1_1.GetValue());
                DataTable dtCopperPlatingType = SqlExecuter.Query("GetPlatingTypeSegment", "10001", Param);
                if (dtCopperPlatingType.Rows.Count > 0)
                {
                    txtinner1speclow1.EditValue = Format.GetString(dtCopperPlatingType.Rows[0]["LSL"]);
                    txtinner1specstd1.EditValue = Format.GetString(dtCopperPlatingType.Rows[0]["SL"]);
                    txtinner1spechigh1.EditValue = Format.GetString(dtCopperPlatingType.Rows[0]["USL"]);
                }
                DataTable dtsemgment = imspitem(processid);
                foreach (DataRow dr in dtsemgment.Rows)
                {
                    if (dr["INSPITEMID"].ToString().Equals("0257"))
                    {
                        txthole2_1.EditValue = Format.GetString(dr["LSL"]);
                        txthole2_2.EditValue = Format.GetString(dr["USL"]);
                    }
                    if (dr["INSPITEMID"].ToString().Equals("0258"))
                    {
                        txtfoil2_1.EditValue = Format.GetString(dr["LSL"]);
                        txtfoil2_2.EditValue = Format.GetString(dr["USL"]);
                    }
                    if (dr["INSPITEMID"].ToString().Equals("0259"))
                    {
                        txtdimple2_1.EditValue = Format.GetString(dr["LSL"]);
                        txtdimple2_2.EditValue = Format.GetString(dr["USL"]);
                    }
                    if (dr["INSPITEMID"].ToString().Equals("0256"))
                    {
                        txtoverfill2_1.EditValue = Format.GetString(dr["LSL"]);
                        txtoverfill2_2.EditValue = Format.GetString(dr["USL"]);
                    }
                }
            }

        }
        #endregion



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
                dtsemgment = SqlExecuter.Query("GetProcessSpec", "10003", Param);
                return dtsemgment;
            }
            return dtsemgment;       
        }


        public DataTable Platingreturn()
        {
            DataSource = new DataTable();
            ReportTableReturn.GetLabelDataTable(smartSplitTableLayoutPanel1, DataSource);
            DataRow row = DataSource.NewRow();
            ReportTableReturn.GetDataRow(row, smartSplitTableLayoutPanel1);
            DataSource.Rows.Add(row);
            return DataSource;

        }
        private void smartSplitTableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void smartSelectPopupEdit3_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void txtcustomstd2_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void popfaceplating1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void txtinner3speclow2_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void smartTextBox38_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void popfaceplating2_EditValueChanged_1(object sender, EventArgs e)
        {

        }

        private void cbPlatingType1_5_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
