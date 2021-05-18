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
    public partial class ProductSpecManagementPlatingSpec_YPE : UserControl
    {

        public DataTable DataSource
        {
            
            get; private set;
        }
        public ProductSpecManagementPlatingSpec_YPE()
        {
            InitializeComponent();
            InitializeComboControl();
            InitializeTextControl();
        }

        private void InitializeComboControl()
        {
            SetSmartComboBox(cbInnerCopper1);
            SetSmartComboBox(cbInnerCopper2);
            SetSmartComboBox(cbInnerCopper3);
            SetSmartComboBox(cbOuterCopper);
            SetSmartComboBox(cbPlatingType1);
            SetSmartComboBox(cbPlatingType2);
            SetSmartComboBox(cbPlatingType3);
            SetSmartComboBox(cbLayer11);
            SetSmartComboBox(cbLayer12);
            SetSmartComboBox(cbLayer21);
            SetSmartComboBox(cbLayer22);
            
            Dictionary<string, object> ParamCopperPlatingType = new Dictionary<string, object>();
            ParamCopperPlatingType.Add("CODECLASSID", "CopperPlatingType");
            ParamCopperPlatingType.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtCopperPlatingType = SqlExecuter.Query("GetTypeList", "10001", ParamCopperPlatingType);

            cbInnerCopper1.DataSource = dtCopperPlatingType.Copy();
            cbInnerCopper2.DataSource = dtCopperPlatingType.Copy();
            cbInnerCopper3.DataSource = dtCopperPlatingType.Copy();
            cbOuterCopper.DataSource = dtCopperPlatingType.Copy();

            //cbInnerCopper1.EditValueChanged += ComboBoxValidation_EditValueChanged;
            //cbInnerCopper2.EditValueChanged += ComboBoxValidation_EditValueChanged;
            //cbInnerCopper3.EditValueChanged += ComboBoxValidation_EditValueChanged;
            //cbOuterCopper.EditValueChanged += ComboBoxValidation_EditValueChanged;

            Dictionary<string, object> ParamPlatingType = new Dictionary<string, object>();
            ParamPlatingType.Add("CODECLASSID", "PlatingType");
            ParamPlatingType.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtPlatingType = SqlExecuter.Query("GetTypeList", "10001", ParamPlatingType);
            cbPlatingType1.DataSource = dtPlatingType.Copy();
            cbPlatingType2.DataSource = dtPlatingType.Copy();
            cbPlatingType3.DataSource = dtPlatingType.Copy();

            //cbPlatingType1.EditValueChanged += ComboBoxValidation_EditValueChanged;
            //cbPlatingType2.EditValueChanged += ComboBoxValidation_EditValueChanged;
            //cbPlatingType3.EditValueChanged += ComboBoxValidation_EditValueChanged;

            Dictionary<string, object> ParamLayer = new Dictionary<string, object>();
            ParamLayer.Add("CODECLASSID", "Layer");
            ParamLayer.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtLayer = SqlExecuter.Query("GetTypeList", "10001", ParamLayer);
            
            cbLayer11.DataSource = dtLayer.Copy();
            cbLayer12.DataSource = dtLayer.Copy();
            cbLayer21.DataSource = dtLayer.Copy();
            cbLayer22.DataSource = dtLayer.Copy();
           
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
            TextBoxHelper.SetUnitMask(Unit.umWithplusminus, tbOuterCopper_1);
            TextBoxHelper.SetUnitMask(Unit.umWithplusminus, tbInnerCopper1_1);
            TextBoxHelper.SetUnitMask(Unit.umWithplusminus, tbInnerCopper2_1);
            TextBoxHelper.SetUnitMask(Unit.umWithplusminus, tbInnerCopper3_1);
            TextBoxHelper.SetUnitMask(Unit.umWithplusminus, tbOuterCopper_2);
            TextBoxHelper.SetUnitMask(Unit.umWithplusminus, tbInnerCopper1_2);
            TextBoxHelper.SetUnitMask(Unit.umWithplusminus, tbInnerCopper2_2);
            TextBoxHelper.SetUnitMask(Unit.umWithplusminus, tbInnerCopper3_2);
            TextBoxHelper.SetUnitMask(Unit.umWithplusminus, tbOuterCopper_11);
            TextBoxHelper.SetUnitMask(Unit.umWithplusminus, tbInnerCopper1_11);
            TextBoxHelper.SetUnitMask(Unit.umWithplusminus, tbInnerCopper2_11);
            TextBoxHelper.SetUnitMask(Unit.umWithplusminus, tbInnerCopper3_11);
            TextBoxHelper.SetUnitMask(Unit.umWithplusminus, tbOuterCopper_12);
            TextBoxHelper.SetUnitMask(Unit.umWithplusminus, tbInnerCopper1_12);
            TextBoxHelper.SetUnitMask(Unit.umWithplusminus, tbInnerCopper2_12);
            TextBoxHelper.SetUnitMask(Unit.umWithplusminus, tbInnerCopper3_12);
            TextBoxHelper.SetUnitMask(Unit.umWithplusminus, tbOuterCopper_21);
            TextBoxHelper.SetUnitMask(Unit.umWithplusminus, tbInnerCopper1_21);
            TextBoxHelper.SetUnitMask(Unit.umWithplusminus, tbInnerCopper2_21);
            TextBoxHelper.SetUnitMask(Unit.umWithplusminus, tbInnerCopper3_21);
            TextBoxHelper.SetUnitMask(Unit.umWithplusminus, tbOuterCopper_22);
            TextBoxHelper.SetUnitMask(Unit.umWithplusminus, tbInnerCopper1_22);
            TextBoxHelper.SetUnitMask(Unit.umWithplusminus, tbInnerCopper2_22);
            TextBoxHelper.SetUnitMask(Unit.umWithplusminus, tbInnerCopper3_22);
            TextBoxHelper.SetUnitMask(Unit.umWithplusminus, tbLayer11);
            TextBoxHelper.SetUnitMask(Unit.umWithplusminus, tbLayer12);
            TextBoxHelper.SetUnitMask(Unit.umWithplusminus, tbLayer21);
            TextBoxHelper.SetUnitMask(Unit.umWithplusminus, tbLayer22);

            TextBoxHelper.SetUnitMask(Unit.umWithplusminus, tbPlatingType1_1);
            TextBoxHelper.SetUnitMask(Unit.umWithplusminus, tbPlatingType1_2);
            TextBoxHelper.SetUnitMask(Unit.umWithplusminus, tbPlatingType1_3);
            TextBoxHelper.SetUnitMask(Unit.mm2pnlWithplusminus, tbPlatingType1_4);
            TextBoxHelper.SetUnitMask(Unit.mm2pnlWithplusminus, tbPlatingType1_5);
            TextBoxHelper.SetUnitMask(Unit.mm2pnlWithplusminus, tbPlatingType1_6);
            TextBoxHelper.SetUnitMask(Unit.mm2pnlWithplusminus, tbPlatingType1_7);

            TextBoxHelper.SetUnitMask(Unit.umWithplusminus, tbPlatingType2_1);
            TextBoxHelper.SetUnitMask(Unit.umWithplusminus, tbPlatingType2_2);
            TextBoxHelper.SetUnitMask(Unit.umWithplusminus, tbPlatingType2_3);
            TextBoxHelper.SetUnitMask(Unit.mm2pnlWithplusminus, tbPlatingType2_4);
            TextBoxHelper.SetUnitMask(Unit.mm2pnlWithplusminus, tbPlatingType2_5);
            TextBoxHelper.SetUnitMask(Unit.mm2pnlWithplusminus, tbPlatingType2_6);
            TextBoxHelper.SetUnitMask(Unit.mm2pnlWithplusminus, tbPlatingType2_7);

            TextBoxHelper.SetUnitMask(Unit.umWithplusminus, tbPlatingType3_1);
            TextBoxHelper.SetUnitMask(Unit.umWithplusminus, tbPlatingType3_2);
            TextBoxHelper.SetUnitMask(Unit.umWithplusminus, tbPlatingType3_3);
            TextBoxHelper.SetUnitMask(Unit.mm2pnlWithplusminus, tbPlatingType3_4);
            TextBoxHelper.SetUnitMask(Unit.mm2pnlWithplusminus, tbPlatingType3_5);
            TextBoxHelper.SetUnitMask(Unit.mm2pnlWithplusminus, tbPlatingType3_6);
            TextBoxHelper.SetUnitMask(Unit.mm2pnlWithplusminus, tbPlatingType3_7);

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

            if (dt == null || dt.Rows.Count <= 0) return;

            foreach (DataRow dr in dt.Rows)
            {
                string strDetailType = string.Empty;
                if (dr["DETAILTYPE"].ToString().Equals("CopperPlating"))
                    strDetailType = "COPPER" + dr["SEQUENCE"].ToString();
                else if (dr["DETAILTYPE"].ToString().Equals("SurfacePlating"))
                    strDetailType = "SURFACE" + dr["SEQUENCE"].ToString(); 
                else if (dr["DETAILTYPE"].ToString().Equals("HolePlating"))
                    strDetailType = "HOLE" + dr["SEQUENCE"].ToString();

                Dictionary<string, object> dicData = new Dictionary<string, object>();
                dicData.Add(strDetailType, dr["DETAILNAME"]);
                dicData.Add(strDetailType + "_SPECDETAILFROM", dr["SPECDETAILFROM"]);
                dicData.Add(strDetailType + "_SPECDETAILTO", dr["SPECDETAILTO"]);

                if (dr["FROMORIGINAL"].ToString().Contains("|"))
                {
                    string[] strValues = dr["FROMORIGINAL"].ToString().Split('|');
                    for (int i = 0; i < strValues.Length; i++)
                    {
                        dicData.Add(strDetailType + "_FROMORIGINAL" + (i + 1).ToString(), strValues[i]);
                    }
                }
                else
                    dicData.Add(strDetailType + "_FROMORIGINAL", dr["FROMORIGINAL"]);

                if (dr["TOORIGINAL"].ToString().Contains("|"))
                {
                    string[] strValues = dr["TOORIGINAL"].ToString().Split('|');
                    for (int i = 0; i < strValues.Length; i++)
                    {
                        dicData.Add(strDetailType + "_TOORIGINAL" + (i + 1).ToString(), strValues[i]);
                    }
                }
                else
                    dicData.Add(strDetailType + "_TOORIGINAL", dr["TOORIGINAL"]);

                if (dr["DESCRIPTION"].ToString().Contains("|"))
                {
                    string[] strValues = dr["DESCRIPTION"].ToString().Split('|');
                    for (int i = 0; i < strValues.Length; i++)
                    {
                        dicData.Add(strDetailType + "_DESCRIPTION" + (i + 1).ToString(), strValues[i]);
                    }
                }
                else
                    dicData.Add(strDetailType + "_DESCRIPTION", dr["DESCRIPTION"]);

                CommonFunctionProductSpec.SearchDataBind(dicData, smartSplitTableLayoutPanel1);
            }

        }


        public DataTable Specreturn()
        {
            DataSource = new DataTable();
            DataSource.Columns.Add(new DataColumn("OUTERCOPPERPLATING", typeof(string))); // 외층동도금
            DataSource.Columns.Add(new DataColumn("INNERCOPPERPLATING1", typeof(string))); // 내층동도금1
            DataSource.Columns.Add(new DataColumn("INNERCOPPERPLATING2", typeof(string))); // 내층동도금2
            DataSource.Columns.Add(new DataColumn("INNERCOPPERPLATING3", typeof(string))); // 내층동도금3
            DataSource.Columns.Add(new DataColumn("PLATINGCONDITION1", typeof(string))); // 도금조건1
            DataSource.Columns.Add(new DataColumn("CUSTOMCONDITION1", typeof(string))); // 고객조건1
            DataSource.Columns.Add(new DataColumn("PLATINGCONDITION2", typeof(string))); // 도금조건2
            DataSource.Columns.Add(new DataColumn("CUSTOMCONDITION2", typeof(string))); // 고객조건2
            DataSource.Columns.Add(new DataColumn("PLATINGCONDITION3", typeof(string))); // 도금조건3
            DataSource.Columns.Add(new DataColumn("CUSTOMCONDITION3", typeof(string))); // 고객조건3
            DataSource.Columns.Add(new DataColumn("PLATINGCONDITION4", typeof(string))); // 도금조건4
            DataSource.Columns.Add(new DataColumn("CUSTOMCONDITION4", typeof(string))); // 고객조건4
            DataSource.Columns.Add(new DataColumn("HOLEINSIDEWALL1", typeof(string))); // 홀내벽1
            DataSource.Columns.Add(new DataColumn("PLANECUFOIL1", typeof(string))); // 면동박1
            DataSource.Columns.Add(new DataColumn("DIMPLE1", typeof(string))); // DIMPLE1
            DataSource.Columns.Add(new DataColumn("OVERFILL1", typeof(string))); // OVERFILL1
            DataSource.Columns.Add(new DataColumn("HOLEINSIDEWALL2", typeof(string))); // 홀내벽2
            DataSource.Columns.Add(new DataColumn("PLANECUFOIL2", typeof(string))); // 면동박2
            DataSource.Columns.Add(new DataColumn("DIMPLE2", typeof(string))); // DIMPLE2
            DataSource.Columns.Add(new DataColumn("OVERFILL2", typeof(string))); // OVERFILL2
            DataSource.Columns.Add(new DataColumn("HOLEINSIDEWALL3", typeof(string))); // 홀내벽3
            DataSource.Columns.Add(new DataColumn("PLANECUFOIL3", typeof(string))); // 면동박3
            DataSource.Columns.Add(new DataColumn("DIMPLE3", typeof(string))); // DIMPLE3
            DataSource.Columns.Add(new DataColumn("OVERFILL3", typeof(string))); // OVERFILL3
            DataSource.Columns.Add(new DataColumn("HOLEINSIDEWALL4", typeof(string))); // 홀내벽4
            DataSource.Columns.Add(new DataColumn("PLANECUFOIL4", typeof(string))); // 면동박4
            DataSource.Columns.Add(new DataColumn("DIMPLE4", typeof(string))); // DIMPLE4
            DataSource.Columns.Add(new DataColumn("OVERFILL4", typeof(string))); // OVERFILL4
            DataSource.Columns.Add(new DataColumn("HOLEPLATINGAREA1", typeof(string))); // HOLE 도금면적1
            DataSource.Columns.Add(new DataColumn("HOLEPLATINGAREA2", typeof(string))); // HOLE 도금면적2
            DataSource.Columns.Add(new DataColumn("HOLE1USERLAYER1", typeof(string))); // 사용층1
            DataSource.Columns.Add(new DataColumn("HOLE1USERLAYER2", typeof(string))); // 사용층2
            DataSource.Columns.Add(new DataColumn("HOLE1USERLAYER3", typeof(string))); // 사용층3
            DataSource.Columns.Add(new DataColumn("HOLE1USERLAYER4", typeof(string))); // 사용층4
            DataSource.Columns.Add(new DataColumn("HOLE2USERLAYER1", typeof(string))); // 사용층1
            DataSource.Columns.Add(new DataColumn("HOLE2USERLAYER2", typeof(string))); // 사용층2
            DataSource.Columns.Add(new DataColumn("HOLE2USERLAYER3", typeof(string))); // 사용층3
            DataSource.Columns.Add(new DataColumn("HOLE2USERLAYER4", typeof(string))); // 사용층4
            DataRow row = DataSource.NewRow();

            DataTable dt = cbOuterCopper.DataSource as DataTable;
            if (cbOuterCopper.EditValue != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][0].ToString().Equals(cbOuterCopper.EditValue.ToString()))
                    {
                        row["OUTERCOPPERPLATING"] = dt.Rows[i]["CODENAME"].ToString();
                    }
                }
            }
            dt = cbInnerCopper1.DataSource as DataTable;
            if (cbInnerCopper1.EditValue != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][0].ToString().Equals(cbInnerCopper1.EditValue.ToString()))
                    {
                        row["INNERCOPPERPLATING1"] = dt.Rows[i]["CODENAME"].ToString();
                    }
                }
            }
            dt = cbInnerCopper2.DataSource as DataTable;
            if (cbInnerCopper2.EditValue != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][0].ToString().Equals(cbInnerCopper2.EditValue.ToString()))
                    {
                        row["INNERCOPPERPLATING2"] = dt.Rows[i]["CODENAME"].ToString();
                    }
                }
            }
            dt = cbInnerCopper3.DataSource as DataTable;
            if (cbInnerCopper3.EditValue != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][0].ToString().Equals(cbInnerCopper3.EditValue.ToString()))
                    {
                        row["INNERCOPPERPLATING3"] = dt.Rows[i]["CODENAME"].ToString();
                    }
                }
            }


            row["PLATINGCONDITION1"] = tbOuterCopper_1.EditValue;
            row["CUSTOMCONDITION1"] = tbOuterCopper_2.EditValue;
            row["PLATINGCONDITION2"] = tbInnerCopper1_1.EditValue;
            row["CUSTOMCONDITION2"] = tbInnerCopper1_2.EditValue;
            row["PLATINGCONDITION3"] = tbInnerCopper2_1.EditValue;
            row["CUSTOMCONDITION3"] = tbInnerCopper2_2.EditValue;
            row["PLATINGCONDITION4"] = tbInnerCopper3_1.EditValue;
            row["CUSTOMCONDITION4"] = tbInnerCopper3_2.EditValue;
            row["HOLEINSIDEWALL1"] = tbOuterCopper_11.EditValue;
            row["PLANECUFOIL1"] = tbOuterCopper_12.EditValue;
            row["DIMPLE1"] = tbOuterCopper_21.EditValue;
            row["OVERFILL1"] = tbOuterCopper_22.EditValue;
            row["HOLEINSIDEWALL2"] = tbInnerCopper1_11.EditValue;
            row["PLANECUFOIL2"] = tbInnerCopper1_12.EditValue;
            row["DIMPLE2"] = tbInnerCopper1_21.EditValue;
            row["OVERFILL2"] = tbInnerCopper1_22.EditValue;
            row["HOLEINSIDEWALL3"] = tbInnerCopper2_11.EditValue;
            row["PLANECUFOIL3"] = tbInnerCopper2_12.EditValue;
            row["DIMPLE3"] = tbInnerCopper2_21.EditValue;
            row["OVERFILL3"] = tbInnerCopper2_22.EditValue;
            row["HOLEINSIDEWALL4"] = tbInnerCopper3_11.EditValue;
            row["PLANECUFOIL4"] = tbInnerCopper3_12.EditValue;
            row["DIMPLE4"] = tbInnerCopper3_21.EditValue;
            row["OVERFILL4"] = tbInnerCopper3_22.EditValue;
            row["HOLE1USERLAYER1"] = cbLayer11.EditValue;
            row["HOLE1USERLAYER2"] = cbLayer12.EditValue;
            row["HOLE1USERLAYER3"] = tbLayer11.EditValue;
            row["HOLE1USERLAYER4"] = tbLayer12.EditValue;
            row["HOLE2USERLAYER1"] = cbLayer21.EditValue;
            row["HOLE2USERLAYER2"] = cbLayer22.EditValue;
            row["HOLE2USERLAYER3"] = tbLayer21.EditValue;
            row["HOLE2USERLAYER4"] = tbLayer22.EditValue;
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
            DataTable dtRtn = new DataTable("PlatingInfo");
            dtRtn.Columns.Add("DETAILTYPE");
            dtRtn.Columns.Add("SEQUENCE");
            dtRtn.Columns.Add("DETAILNAME");
            dtRtn.Columns.Add("SPECDETAILFROM");
            dtRtn.Columns.Add("SPECDETAILTO");
            dtRtn.Columns.Add("FROMORIGINAL");
            dtRtn.Columns.Add("TOORIGINAL");
            dtRtn.Columns.Add("DESCRIPTION");

            Dictionary<string, object> dataDictionary = new Dictionary<string, object>();
            CommonFunctionProductSpec.GetSaveDataDictionary(smartSplitTableLayoutPanel1, dataDictionary);

            // 동도금 정보
            for (int i = 1; i <= 4; i++)
            {
                DataRow drCopperPlating = dtRtn.NewRow();
                drCopperPlating["DETAILTYPE"] = "CopperPlating";
                drCopperPlating["SEQUENCE"] = i.ToString();

                if (dataDictionary.ContainsKey("COPPER" + i.ToString()))
                    drCopperPlating["DETAILNAME"] = dataDictionary["COPPER" + i.ToString()];

                if (dataDictionary.ContainsKey("COPPER" + i.ToString() + "_SPECDETAILFROM"))    //도금조건
                    drCopperPlating["SPECDETAILFROM"] = dataDictionary["COPPER" + i.ToString() + "_SPECDETAILFROM"];

                if (dataDictionary.ContainsKey("COPPER" + i.ToString() + "_SPECDETAILTO"))          //고객조건
                    drCopperPlating["SPECDETAILTO"] = dataDictionary["COPPER" + i.ToString() + "_SPECDETAILTO"];

                var vFormOrig = from p in dataDictionary.AsEnumerable()
                                where p.Key.StartsWith("COPPER" + i.ToString() + "_FROMORIGINAL")
                                orderby p.Key
                                select p.Value.ToString().Split(' ')[0];

                if (vFormOrig != null && vFormOrig.Count() > 0)
                    drCopperPlating["FROMORIGINAL"] = string.Join("|", vFormOrig);

                var vToOrig = from p in dataDictionary.AsEnumerable()
                              where p.Key.StartsWith("COPPER" + i.ToString() + "_TOORIGINAL")
                              orderby p.Key
                              select p.Value.ToString().Split(' ')[0];

                if (vToOrig != null && vToOrig.Count() > 0)
                    drCopperPlating["TOORIGINAL"] = string.Join("|", vToOrig);

                dtRtn.Rows.Add(drCopperPlating);
            }

            // 표면도금 정보
            for (int i = 1; i <= 3; i++)
            {
                DataRow drCopperPlating = dtRtn.NewRow();
                drCopperPlating["DETAILTYPE"] = "SurfacePlating";
                drCopperPlating["SEQUENCE"] = i.ToString();

                if (dataDictionary.ContainsKey("SURFACE" + i.ToString()))
                    drCopperPlating["DETAILNAME"] = dataDictionary["SURFACE" + i.ToString()];

                if (dataDictionary.ContainsKey("SURFACE" + i.ToString() + "_SPECDETAILFROM"))    //도금면적 C 콤보
                    drCopperPlating["SPECDETAILFROM"] = dataDictionary["SURFACE" + i.ToString() + "_SPECDETAILFROM"];

                if (dataDictionary.ContainsKey("SURFACE" + i.ToString() + "_SPECDETAILTO"))          //도금면적 S 콤보
                    drCopperPlating["SPECDETAILTO"] = dataDictionary["SURFACE" + i.ToString() + "_SPECDETAILTO"];

                if (dataDictionary.ContainsKey("SURFACE" + i.ToString() + "_FROMORIGINAL"))    //도금면적 C 값
                    drCopperPlating["FROMORIGINAL"] = dataDictionary["SURFACE" + i.ToString() + "_FROMORIGINAL"];

                if (dataDictionary.ContainsKey("SURFACE" + i.ToString() + "_TOORIGINAL"))          //도금면적 S 값
                    drCopperPlating["TOORIGINAL"] = dataDictionary["SURFACE" + i.ToString() + "_TOORIGINAL"];

                var vDesc = from p in dataDictionary.AsEnumerable()
                            where p.Key.StartsWith("SURFACE" + i.ToString() + "_DESCRIPTION")
                            orderby p.Key
                            select p.Value.ToString().Split(' ')[0];

                if (vDesc != null && vDesc.Count() > 0)
                    drCopperPlating["DESCRIPTION"] = string.Join("|", vDesc);

                dtRtn.Rows.Add(drCopperPlating);
            }

            // HOLE 도금 면적 정보
            for (int i = 1; i <= 2; i++)
            {
                DataRow drHolePlating = dtRtn.NewRow();
                drHolePlating["DETAILTYPE"] = "HolePlating";
                drHolePlating["SEQUENCE"] = i.ToString();

                if (dataDictionary.ContainsKey("HOLE" + i.ToString() + "_SPECDETAILFROM"))    
                    drHolePlating["SPECDETAILFROM"] = dataDictionary["HOLE" + i.ToString() + "_SPECDETAILFROM"];

                if (dataDictionary.ContainsKey("HOLE" + i.ToString() + "_SPECDETAILTO"))         
                    drHolePlating["SPECDETAILTO"] = dataDictionary["HOLE" + i.ToString() + "_SPECDETAILTO"];

                if (dataDictionary.ContainsKey("HOLE" + i.ToString() + "_FROMORIGINAL"))   
                    drHolePlating["FROMORIGINAL"] = dataDictionary["HOLE" + i.ToString() + "_FROMORIGINAL"];

                if (dataDictionary.ContainsKey("HOLE" + i.ToString() + "_TOORIGINAL"))       
                    drHolePlating["TOORIGINAL"] = dataDictionary["HOLE" + i.ToString() + "_TOORIGINAL"];

                dtRtn.Rows.Add(drHolePlating);
            }
            return dtRtn;
        }


        private void ValidationCheck()
        {
            string groupName = this.smartGroupBox1.Text;

            if (!string.IsNullOrEmpty(cbOuterCopper.GetDataValue().ToString()))
            {
                if (string.IsNullOrWhiteSpace(tbOuterCopper_1.Text)
                    || string.IsNullOrWhiteSpace(tbOuterCopper_2.Text)
                    || string.IsNullOrWhiteSpace(tbOuterCopper_11.Text)
                    || string.IsNullOrWhiteSpace(tbOuterCopper_12.Text)
                    || string.IsNullOrWhiteSpace(tbOuterCopper_21.Text)
                    || string.IsNullOrWhiteSpace(tbOuterCopper_22.Text))
                    throw MessageException.Create("InValidOspRequiredField", groupName + lbOuterCopper.Text);
            }

            if (!string.IsNullOrEmpty(cbInnerCopper1.GetDataValue().ToString()))
            {
                if (string.IsNullOrWhiteSpace(tbInnerCopper1_1.Text)
                    || string.IsNullOrWhiteSpace(tbInnerCopper1_2.Text)
                    || string.IsNullOrWhiteSpace(tbInnerCopper1_11.Text)
                    || string.IsNullOrWhiteSpace(tbInnerCopper1_12.Text)
                    || string.IsNullOrWhiteSpace(tbInnerCopper1_21.Text)
                    || string.IsNullOrWhiteSpace(tbInnerCopper1_22.Text))
                    throw MessageException.Create("InValidOspRequiredField", groupName + lbInnerCopper1.Text);
            }

            if (!string.IsNullOrEmpty(cbInnerCopper2.GetDataValue().ToString()))
            {
                if (string.IsNullOrWhiteSpace(tbInnerCopper2_1.Text)
                    || string.IsNullOrWhiteSpace(tbInnerCopper2_2.Text)
                    || string.IsNullOrWhiteSpace(tbInnerCopper2_11.Text)
                    || string.IsNullOrWhiteSpace(tbInnerCopper2_12.Text)
                    || string.IsNullOrWhiteSpace(tbInnerCopper2_21.Text)
                    || string.IsNullOrWhiteSpace(tbInnerCopper2_22.Text))
                    throw MessageException.Create("InValidOspRequiredField", groupName + lbInnerCopper2.Text);
            }

            if (!string.IsNullOrEmpty(cbInnerCopper3.GetDataValue().ToString()))
            {
                if (string.IsNullOrWhiteSpace(tbInnerCopper3_1.Text)
                    || string.IsNullOrWhiteSpace(tbInnerCopper3_2.Text)
                    || string.IsNullOrWhiteSpace(tbInnerCopper3_11.Text)
                    || string.IsNullOrWhiteSpace(tbInnerCopper3_12.Text)
                    || string.IsNullOrWhiteSpace(tbInnerCopper3_21.Text)
                    || string.IsNullOrWhiteSpace(tbInnerCopper3_22.Text))
                    throw MessageException.Create("InValidOspRequiredField", groupName + lbInnerCopper3.Text);
            }

            if (!string.IsNullOrEmpty(cbPlatingType1.GetDataValue().ToString()))
            {
                if (string.IsNullOrWhiteSpace(tbPlatingType1_1.Text)
                    || string.IsNullOrWhiteSpace(tbPlatingType1_2.Text)
                    || string.IsNullOrWhiteSpace(tbPlatingType1_3.Text)
                    || string.IsNullOrWhiteSpace(tbPlatingType1_4.Text) || string.IsNullOrWhiteSpace(tbPlatingType1_5.Text)
                    || string.IsNullOrWhiteSpace(tbPlatingType1_6.Text) || string.IsNullOrWhiteSpace(tbPlatingType1_7.Text))
                    throw MessageException.Create("InValidOspRequiredField", groupName + lbPlatingType1.Text);
            }

            if (!string.IsNullOrEmpty(cbPlatingType2.GetDataValue().ToString()))
            {
                if (string.IsNullOrWhiteSpace(tbPlatingType2_1.Text)
                    || string.IsNullOrWhiteSpace(tbPlatingType2_2.Text)
                    || string.IsNullOrWhiteSpace(tbPlatingType2_3.Text)
                    || string.IsNullOrWhiteSpace(tbPlatingType2_4.Text) || string.IsNullOrWhiteSpace(tbPlatingType2_5.Text)
                    || string.IsNullOrWhiteSpace(tbPlatingType2_6.Text) || string.IsNullOrWhiteSpace(tbPlatingType2_7.Text))
                    throw MessageException.Create("InValidOspRequiredField", groupName + lbPlatingType2.Text);
            }

            if (!string.IsNullOrEmpty(cbPlatingType3.GetDataValue().ToString()))
            {
                if (string.IsNullOrWhiteSpace(tbPlatingType3_1.Text)
                    || string.IsNullOrWhiteSpace(tbPlatingType3_2.Text)
                    || string.IsNullOrWhiteSpace(tbPlatingType3_3.Text)
                    || string.IsNullOrWhiteSpace(tbPlatingType3_4.Text) || string.IsNullOrWhiteSpace(tbPlatingType3_5.Text)
                    || string.IsNullOrWhiteSpace(tbPlatingType3_6.Text) || string.IsNullOrWhiteSpace(tbPlatingType3_7.Text))
                    throw MessageException.Create("InValidOspRequiredField", groupName + lbPlatingType3.Text);
            }


            #region 콤보선택과 상관없이 무조건 체크 - 주석처리
            if (string.IsNullOrWhiteSpace(tbOuterCopper_1.Text) ||
                string.IsNullOrWhiteSpace(tbInnerCopper1_1.Text) ||
                string.IsNullOrWhiteSpace(tbInnerCopper2_1.Text) ||
                string.IsNullOrWhiteSpace(tbInnerCopper3_1.Text))
                throw MessageException.Create("InValidOspRequiredField", groupName + Language.Get("PLATINGCONDITION"));

            if (string.IsNullOrWhiteSpace(tbOuterCopper_2.Text) ||
                string.IsNullOrWhiteSpace(tbInnerCopper1_2.Text) ||
                string.IsNullOrWhiteSpace(tbInnerCopper2_2.Text) ||
                string.IsNullOrWhiteSpace(tbInnerCopper3_2.Text))
                throw MessageException.Create("InValidOspRequiredField", groupName + Language.Get("CUSTOMERCRITERIA"));

            if (string.IsNullOrWhiteSpace(tbPlatingType1_2.Text) ||
                string.IsNullOrWhiteSpace(tbPlatingType2_2.Text) ||
                string.IsNullOrWhiteSpace(tbPlatingType3_2.Text))
                throw MessageException.Create("InValidOspRequiredField", groupName + Language.Get("Au"));

            if (string.IsNullOrWhiteSpace(tbPlatingType1_1.Text) ||
                string.IsNullOrWhiteSpace(tbPlatingType2_1.Text) ||
                string.IsNullOrWhiteSpace(tbPlatingType3_1.Text))
                throw MessageException.Create("InValidOspRequiredField", groupName + Language.Get("Ni"));

            if (string.IsNullOrWhiteSpace(tbPlatingType1_3.Text) ||
                string.IsNullOrWhiteSpace(tbPlatingType2_3.Text) ||
                string.IsNullOrWhiteSpace(tbPlatingType3_3.Text))
                throw MessageException.Create("InValidOspRequiredField", groupName + Language.Get("Pd"));

            if (string.IsNullOrWhiteSpace(tbPlatingType1_4.Text) ||
                string.IsNullOrWhiteSpace(tbPlatingType1_5.Text) ||
                string.IsNullOrWhiteSpace(tbPlatingType2_4.Text) ||
                string.IsNullOrWhiteSpace(tbPlatingType2_5.Text) ||
                string.IsNullOrWhiteSpace(tbPlatingType3_4.Text) ||
                string.IsNullOrWhiteSpace(tbPlatingType3_5.Text))
                throw MessageException.Create("InValidOspRequiredField", groupName + Language.Get("PLATINGAREA"));

            if (string.IsNullOrWhiteSpace(tbPlatingType1_6.Text) ||
                string.IsNullOrWhiteSpace(tbPlatingType1_7.Text) ||
                string.IsNullOrWhiteSpace(tbPlatingType2_6.Text) ||
                string.IsNullOrWhiteSpace(tbPlatingType2_7.Text) ||
                string.IsNullOrWhiteSpace(tbPlatingType3_6.Text) ||
                string.IsNullOrWhiteSpace(tbPlatingType3_7.Text))
                throw MessageException.Create("InValidOspRequiredField", groupName + Language.Get("SCRAPAREA"));

            #endregion
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
    }
}
