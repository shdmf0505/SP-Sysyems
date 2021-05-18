#region using
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;

//using Excel =  Microsoft.Office.Interop.Excel;


using System;
using Micube.SmartMES.Commons.Controls;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel =  Microsoft.Office.Interop.Excel;
#endregion

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프 로 그 램 명  : 기준정보 > Setup >  고객 Import 및 조회
    /// 업  무  설  명  : 고객정보를 Import및 관리한다.
    /// 생    성    자  : 강유라
    /// 생    성    일  : 2019-05-17
    /// 수  정  이  력  : 윤성원 2019-07-05 using 에 #region #endregion 추가
    /// 
    /// 
    /// </summary>
    public partial class StdMigration : SmartConditionManualBaseForm
	{
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가

        #endregion

        #region 생성자

        public StdMigration()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeEvent();

            // TODO : 컨트롤 초기화 로직 구성
            InitializeGrid();
        }

        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
           

        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            //Import 버튼 클릭시 이벤트
            this.btnProductItemUpload.Click += btnProductItemUpload_Click;

            btnMaterialitemUpload.Click += BtnMaterialitemUpload_Click;

            btnRouting.Click += BtnRouting_Click;
            btnBom.Click += BtnBom_Click;

        }

        private void BtnBom_Click(object sender, EventArgs e)
        {
            Excel.Application xlApp = null;
            Excel.Workbook xlWorkbook = null;
            Excel.Worksheet xlWorksheet = null;


            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Excel File (*.xlsx)|*.xlsx|Excel File 97!2003 (*.xls)|*.xls|All Files (*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                DataTable dtBomcnv = new DataTable();

                dtBomcnv.Columns.Add("ASSEMBLYITEMID");
                dtBomcnv.Columns.Add("ASSEMBLYITEMVERSION");
                dtBomcnv.Columns.Add("OPERATIONSEQUENCE");
                dtBomcnv.Columns.Add("PROCESSSEGMENTID");
                dtBomcnv.Columns.Add("COMPONENTITEMID");
                dtBomcnv.Columns.Add("COMPONENTITEMVERSION");
                dtBomcnv.Columns.Add("ASSEMBLYBOMID");
                dtBomcnv.Columns.Add("COMPONENTBOMID");
                dtBomcnv.Columns.Add("COMPONENTSEQUENCE");
                dtBomcnv.Columns.Add("ENTERPRISEID");
                dtBomcnv.Columns.Add("PLANTID");
                dtBomcnv.Columns.Add("ASSEMBLYITEMCLASS");
                dtBomcnv.Columns.Add("ASSEMBLYTYPE");
                dtBomcnv.Columns.Add("ASSEMBLYITEMUOM");
                dtBomcnv.Columns.Add("NEWREQUESTNO");
                dtBomcnv.Columns.Add("ENGINEERINGCHANGE");
                dtBomcnv.Columns.Add("IMPLEMENTATIONDATE");
                dtBomcnv.Columns.Add("OPERATIONID");
                dtBomcnv.Columns.Add("COMPONENTUOM");
                dtBomcnv.Columns.Add("MATERIALTYPE");
                dtBomcnv.Columns.Add("COMPONENTQTY");
                dtBomcnv.Columns.Add("REGISTEREDHANDBOOKQTY");
                dtBomcnv.Columns.Add("PLANNINGFACTOR");
                dtBomcnv.Columns.Add("COMPONENTYIELDFACTOR");
                dtBomcnv.Columns.Add("WIPSUPPLYTYPE");
                dtBomcnv.Columns.Add("USERLAYER");
                dtBomcnv.Columns.Add("SUPPLYWAREHOUSEID");
                dtBomcnv.Columns.Add("SUPPLYLOCATIONID");
                dtBomcnv.Columns.Add("ISREQUIREDMATERIAL");
                dtBomcnv.Columns.Add("ISAUTOREQUESTMATERIAL");
                dtBomcnv.Columns.Add("INCLUDEINCOSTROLLUP");
                dtBomcnv.Columns.Add("FROMOPERATIONID");
                dtBomcnv.Columns.Add("TOOPERATIONID");
                dtBomcnv.Columns.Add("OPTIONAL");
                dtBomcnv.Columns.Add("CHECKATP");
                dtBomcnv.Columns.Add("DESCRIPTION");

                xlApp = new Excel.Application();
                xlWorkbook = xlApp.Workbooks.Open(ofd.FileName);
                xlWorksheet = (Excel.Worksheet)xlWorkbook.Worksheets.Item[1];


                Excel.Range range = xlWorksheet.UsedRange;

                object[,] data = range.Value;

                for (int r = 3; r <= range.Rows.Count; r++)
                {
                    DataRow rowBomcnv = dtBomcnv.NewRow();
                    for (int c = 1; c <= range.Columns.Count; c++)
                    {
                        rowBomcnv[data[2, c].ToString().ToUpper()] = data[r, c];
                    }
                    rowBomcnv["VALIDSTATE"] = "Valid";
                    rowBomcnv["_STATE_"] = "added";
                    dtBomcnv.Rows.Add(rowBomcnv);
                }

                xlWorkbook.Close(true);
                xlApp.Quit();
                ExecuteRule("Bomcnv", dtBomcnv);


                DataTable dtRutingcnv = SqlExecuter.Query("GetRutingcnv", "10001");

                DataTable dtassemblybillofmaterial = new DataTable();
                dtassemblybillofmaterial.Columns.Add("ASSEMBLYBOMID");
                dtassemblybillofmaterial.Columns.Add("ENTERPRISEID");
                dtassemblybillofmaterial.Columns.Add("PLANTID");
                dtassemblybillofmaterial.Columns.Add("ASSEMBLYITEMID");
                dtassemblybillofmaterial.Columns.Add("ASSEMBLYITEMVERSION");
                dtassemblybillofmaterial.Columns.Add("ASSEMBLYITEMCLASS");
                dtassemblybillofmaterial.Columns.Add("ASSEMBLYTYPE");
                dtassemblybillofmaterial.Columns.Add("ASSEMBLYITEMUOM");
                dtassemblybillofmaterial.Columns.Add("NEWREQUESTNO");
                dtassemblybillofmaterial.Columns.Add("ENGINEERINGCHANGE");
                dtassemblybillofmaterial.Columns.Add("IMPLEMENTATIONDATE");
                dtassemblybillofmaterial.Columns.Add("DESCRIPTION");
                dtassemblybillofmaterial.Columns.Add("VALIDSTATE");

                DataTable dtgroup = dtRutingcnv.DefaultView.ToTable(
                true, new string[] { "ENTERPRISEID", "PLANTID", "ASSEMBLYITEMID", "ASSEMBLYITEMVERSION", "MASTERDATACLASSID", "UOMDEFID", "IMPLEMENTATIONDATE", "VALIDSTATE" });

                foreach (DataRow rowgroup in dtgroup.Rows)
                {
                    DataRow rowassemblybillofmaterial = dtassemblybillofmaterial.NewRow();
                    GetNumber number = new GetNumber();
                    rowassemblybillofmaterial["ASSEMBLYBOMID"] = number.GetStdNumber("AssemblyBomId", "");
                    rowassemblybillofmaterial["ENTERPRISEID"] = rowgroup["ENTERPRISEID"];
                    rowassemblybillofmaterial["PLANTID"] = rowgroup["PLANTID"];

                    rowassemblybillofmaterial["ASSEMBLYITEMID"] = rowgroup["ASSEMBLYITEMID"];
                    rowassemblybillofmaterial["ASSEMBLYITEMVERSION"] = rowgroup["ASSEMBLYITEMVERSION"];
                    rowassemblybillofmaterial["ASSEMBLYITEMCLASS"] = rowgroup["MASTERDATACLASSID"];

                    if (rowgroup["MASTERDATACLASSID"].ToString() == "")
                        rowassemblybillofmaterial["ASSEMBLYITEMCLASS"] = rowgroup["MASTERDATACLASSID"];

                }


            }

        }
        private void BtnRouting_Click(object sender, EventArgs e)
        {
            Excel.Application xlApp = null;
            Excel.Workbook xlWorkbook = null;
            Excel.Worksheet xlWorksheet = null;


            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Excel File (*.xlsx)|*.xlsx|Excel File 97!2003 (*.xls)|*.xls|All Files (*.*)|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                DataTable dtRoutingcnv = new DataTable();
                dtRoutingcnv.Columns.Add("ASSEMBLYITEMID");
                dtRoutingcnv.Columns.Add("ASSEMBLYITEMVERSION");
                dtRoutingcnv.Columns.Add("USERSEQUENCE");
                dtRoutingcnv.Columns.Add("PROCESSSEGMENTID");
                dtRoutingcnv.Columns.Add("ENTERPRISEID");
                dtRoutingcnv.Columns.Add("PLANTID");
                dtRoutingcnv.Columns.Add("OPERATIONID");
                dtRoutingcnv.Columns.Add("OPERATIONSEQUENCE");
                dtRoutingcnv.Columns.Add("ASSEMBLYROUTINGID");
                dtRoutingcnv.Columns.Add("NEWREQUESTNO");
                dtRoutingcnv.Columns.Add("ENGINEERINGCHANGE");
                dtRoutingcnv.Columns.Add("IMPLEMENTATIONDATE");
                dtRoutingcnv.Columns.Add("COMMONROUTINGID");
                dtRoutingcnv.Columns.Add("ASSEMBLYITEMCLASS");
                dtRoutingcnv.Columns.Add("COMPLETIONWAREHOUSEID");
                dtRoutingcnv.Columns.Add("COMPLETIONLOCATIONID");
                dtRoutingcnv.Columns.Add("TOTALLEADTIME");
                dtRoutingcnv.Columns.Add("OPERATIONLEADTIMEPERCENT");
                dtRoutingcnv.Columns.Add("MINIMUMTRANSFERQTY");
                dtRoutingcnv.Columns.Add("COUNTPOINTTYPE");
                dtRoutingcnv.Columns.Add("ISBACKFLUSH");
                dtRoutingcnv.Columns.Add("ISOPTIONDEPENDENT");
                dtRoutingcnv.Columns.Add("OPERATIONTYPE");
                dtRoutingcnv.Columns.Add("YIELD");
                dtRoutingcnv.Columns.Add("WORKTIME");
                dtRoutingcnv.Columns.Add("EQUIPMENTTIME");
                dtRoutingcnv.Columns.Add("TACTTIME");
                dtRoutingcnv.Columns.Add("LEADTIME");
                dtRoutingcnv.Columns.Add("STEPTYPE");
                dtRoutingcnv.Columns.Add("STEPCLASS");
                dtRoutingcnv.Columns.Add("RECEIVETIME");
                dtRoutingcnv.Columns.Add("STARTTIME");
                dtRoutingcnv.Columns.Add("ENDTIME");
                dtRoutingcnv.Columns.Add("SENDTIME");
                dtRoutingcnv.Columns.Add("MOVETIME");
                dtRoutingcnv.Columns.Add("COMPLETELOCATIONID");
                dtRoutingcnv.Columns.Add("NETPLANNINGPERCENT");
                dtRoutingcnv.Columns.Add("INCLUDEINROLLUP");
                dtRoutingcnv.Columns.Add("SHUTDOWNTYPE");
                dtRoutingcnv.Columns.Add("COSTCENTER");
                dtRoutingcnv.Columns.Add("AREAID");
                dtRoutingcnv.Columns.Add("DEPARTMENT");
                dtRoutingcnv.Columns.Add("DESCRIPTION");
                dtRoutingcnv.Columns.Add("VALIDSTATE");

                xlApp = new Excel.Application();
                xlWorkbook = xlApp.Workbooks.Open(ofd.FileName);
                xlWorksheet = (Excel.Worksheet)xlWorkbook.Worksheets.Item[1];


                Excel.Range range = xlWorksheet.UsedRange;

                object[,] data = range.Value;

                for (int r = 3; r <= range.Rows.Count; r++)
                {
                    DataRow rowProductCNV = dtRoutingcnv.NewRow();
                    for (int c = 1; c <= range.Columns.Count; c++)
                    {
                        rowProductCNV[data[2, c].ToString().ToUpper()] = data[r, c];
                    }
                    rowProductCNV["VALIDSTATE"] = "Valid";
                    rowProductCNV["_STATE_"] = "added";
                    dtRoutingcnv.Rows.Add(rowProductCNV);
                }

                xlWorkbook.Close(true);
                xlApp.Quit();
                ExecuteRule("Routingcnv", dtRoutingcnv);

             

                        //  , 

                        //OperationItem
                        //Phantom

               

                
              

            }
        }

        private void BtnMaterialitemUpload_Click(object sender, EventArgs e)
        {
            Excel.Application xlApp = null;
            Excel.Workbook xlWorkbook = null;
            Excel.Worksheet xlWorksheet = null;


            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Excel File (*.xlsx)|*.xlsx|Excel File 97!2003 (*.xls)|*.xls|All Files (*.*)|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                DataTable dtMaterialCNV = new DataTable();
                dtMaterialCNV.Columns.Add("ITEMID");
                dtMaterialCNV.Columns.Add("ITEMVERSION");
                dtMaterialCNV.Columns.Add("ENTERPRISEID");
                dtMaterialCNV.Columns.Add("MASTERDATACLASSID");
                dtMaterialCNV.Columns.Add("PLANTID");
                dtMaterialCNV.Columns.Add("ITEMCODE");
                dtMaterialCNV.Columns.Add("ITEMNAME");
                dtMaterialCNV.Columns.Add("DUPLICATION");
                dtMaterialCNV.Columns.Add("UOMDEFID");
                dtMaterialCNV.Columns.Add("NEWREQUEST");
                dtMaterialCNV.Columns.Add("ENGINEERINGCHANGE");
                dtMaterialCNV.Columns.Add("IMPLEMENTATIONDATE");
                dtMaterialCNV.Columns.Add("STATUS");
                dtMaterialCNV.Columns.Add("CONSUMABLETYPE");
                dtMaterialCNV.Columns.Add("PRODUCTIONTYPE");
                dtMaterialCNV.Columns.Add("LEADTIME");
                dtMaterialCNV.Columns.Add("TACTTIME");
                dtMaterialCNV.Columns.Add("ITEMTYPE");
                dtMaterialCNV.Columns.Add("COSTCATEGORY");
                dtMaterialCNV.Columns.Add("SALEORDERCATEGORY");
                dtMaterialCNV.Columns.Add("INVENTORYCATEGORY");
                dtMaterialCNV.Columns.Add("MAKEBUYTYPE");
                dtMaterialCNV.Columns.Add("PLANNER");
                dtMaterialCNV.Columns.Add("UNITOFPURCHASING");
                dtMaterialCNV.Columns.Add("UNITOFSTOCK");
                dtMaterialCNV.Columns.Add("SPEC");
                dtMaterialCNV.Columns.Add("PRICE");
                dtMaterialCNV.Columns.Add("AGING");
                dtMaterialCNV.Columns.Add("AGINGDAY");
                dtMaterialCNV.Columns.Add("CYCLECOUNT");
                dtMaterialCNV.Columns.Add("ENDTYPE");
                dtMaterialCNV.Columns.Add("LOTCONTROL");
                dtMaterialCNV.Columns.Add("ISINVENTORY");
                dtMaterialCNV.Columns.Add("ISSALES");
                dtMaterialCNV.Columns.Add("ISSHIPPING");
                dtMaterialCNV.Columns.Add("ISBOM");
                dtMaterialCNV.Columns.Add("ISLOTMERGE");
                dtMaterialCNV.Columns.Add("ISPARAMETER");
                dtMaterialCNV.Columns.Add("ISPURCHASE");
                dtMaterialCNV.Columns.Add("ISINTERNALPURCHASE");
                dtMaterialCNV.Columns.Add("CURRENCY");
                dtMaterialCNV.Columns.Add("MATERIALCLASS");
                dtMaterialCNV.Columns.Add("SUBCLASS");
                dtMaterialCNV.Columns.Add("MINORDERQTY");
                dtMaterialCNV.Columns.Add("PROCUREMENT");
                dtMaterialCNV.Columns.Add("LMECLASS");
                dtMaterialCNV.Columns.Add("PURCHASEMAN");
                dtMaterialCNV.Columns.Add("ORDERPOLICY");
                dtMaterialCNV.Columns.Add("SAFETYSTOCK");
                dtMaterialCNV.Columns.Add("RECEIPTWAREHOUSEID");
                dtMaterialCNV.Columns.Add("RECEIPTLOCATOR");
                dtMaterialCNV.Columns.Add("RECEIPTROUTE");
                dtMaterialCNV.Columns.Add("VENDORID");
                dtMaterialCNV.Columns.Add("MAKER");
                dtMaterialCNV.Columns.Add("ORIGIN");
                dtMaterialCNV.Columns.Add("NORMALLEADTIME");
                dtMaterialCNV.Columns.Add("URGENCYLEADTIME");
                dtMaterialCNV.Columns.Add("TARIFFRATE");
                dtMaterialCNV.Columns.Add("BASEBANDRATE");
                dtMaterialCNV.Columns.Add("DOUBLESINGLESIDED");
                dtMaterialCNV.Columns.Add("PITHICKNESS");
                dtMaterialCNV.Columns.Add("THICKNESSCOPPER");
                dtMaterialCNV.Columns.Add("COPPERSPEC");
                dtMaterialCNV.Columns.Add("MATERIALWIDTH");
                dtMaterialCNV.Columns.Add("MATERIALLENGTH");
                dtMaterialCNV.Columns.Add("EXPIRATIONDATE");
                dtMaterialCNV.Columns.Add("MAKERECEIPTTYPE");
                dtMaterialCNV.Columns.Add("COLOR");
                dtMaterialCNV.Columns.Add("ISCONTAINHALOGEN");
                dtMaterialCNV.Columns.Add("THICKNESSADH");
                dtMaterialCNV.Columns.Add("MATERIALTHICKNESS");
                dtMaterialCNV.Columns.Add("MATERIALWEIGHT");
                dtMaterialCNV.Columns.Add("DESCRIPTION");
                dtMaterialCNV.Columns.Add("VALIDSTATE");
                dtMaterialCNV.Columns.Add("_STATE_");
                

                DataTable dtitemmaster = new DataTable();
                dtitemmaster.Columns.Add("MASTERDATACLASSID");
                dtitemmaster.Columns.Add("ITEMID");
                dtitemmaster.Columns.Add("ITEMVERSION");
                dtitemmaster.Columns.Add("ENTERPRISEID");
                dtitemmaster.Columns.Add("PLANTID");
                dtitemmaster.Columns.Add("ITEMCODE");
                dtitemmaster.Columns.Add("ITEMNAME");
                dtitemmaster.Columns.Add("DUPLICATION");
                dtitemmaster.Columns.Add("ITEMUOM");
                dtitemmaster.Columns.Add("NEWREQUEST");
                dtitemmaster.Columns.Add("ENGINEERINGCHANGE");
                dtitemmaster.Columns.Add("IMPLEMENTATIONDATE");
                dtitemmaster.Columns.Add("ITEMSTATUS");
                dtitemmaster.Columns.Add("CONSUMABLETYPE");
                dtitemmaster.Columns.Add("PRODUCTIONTYPE");
                dtitemmaster.Columns.Add("LEADTIME");
                dtitemmaster.Columns.Add("TACTTIME");
                dtitemmaster.Columns.Add("ITEMTYPE");
                dtitemmaster.Columns.Add("COSTCATEGORY");
                dtitemmaster.Columns.Add("SALEORDERCATEGORY");
                dtitemmaster.Columns.Add("INVENTORYCATEGORY");
                dtitemmaster.Columns.Add("MAKEBUYTYPE");
                dtitemmaster.Columns.Add("PLANNER");
                dtitemmaster.Columns.Add("UNITOFPURCHASING");
                dtitemmaster.Columns.Add("UNITOFSTOCK");
                dtitemmaster.Columns.Add("SPEC");
                dtitemmaster.Columns.Add("PRICE");
                dtitemmaster.Columns.Add("AGING");
                dtitemmaster.Columns.Add("AGINGDAY");
                dtitemmaster.Columns.Add("CYCLECOUNT");
                dtitemmaster.Columns.Add("ENDTYPE");
                dtitemmaster.Columns.Add("LOTCONTROL");
                dtitemmaster.Columns.Add("ISINVENTORY");
                dtitemmaster.Columns.Add("ISSALES");
                dtitemmaster.Columns.Add("ISSHIPPING");
                dtitemmaster.Columns.Add("ISBOM");
                dtitemmaster.Columns.Add("ISLOTMERGE");
                dtitemmaster.Columns.Add("ISPARAMETER");
                dtitemmaster.Columns.Add("ISPURCHASE");
                dtitemmaster.Columns.Add("ISINTERNALPURCHASE");
                dtitemmaster.Columns.Add("CURRENCY");
                dtitemmaster.Columns.Add("DESCRIPTION");
                dtitemmaster.Columns.Add("PRODUCTTYPE");
                dtitemmaster.Columns.Add("VALIDSTATE");
                dtitemmaster.Columns.Add("_STATE_");

                DataTable dtmaterialitemspec = new DataTable();
                dtmaterialitemspec.Columns.Add("ITEMID");
                dtmaterialitemspec.Columns.Add("ITEMVERSION");
                dtmaterialitemspec.Columns.Add("ENTERPRISEID");
                dtmaterialitemspec.Columns.Add("PLANTID");
                dtmaterialitemspec.Columns.Add("MATERIALTYPE");
                dtmaterialitemspec.Columns.Add("MATERIALCLASS");
                dtmaterialitemspec.Columns.Add("SUBCLASS");
                dtmaterialitemspec.Columns.Add("MINORDERQTY");
                dtmaterialitemspec.Columns.Add("PROCUREMENT");
                dtmaterialitemspec.Columns.Add("LMECLASS");
                dtmaterialitemspec.Columns.Add("PURCHASEMAN");
                dtmaterialitemspec.Columns.Add("ORDERPOLICY");
                dtmaterialitemspec.Columns.Add("SAFETYSTOCK");
                dtmaterialitemspec.Columns.Add("RECEIPTWAREHOUSEID");
                dtmaterialitemspec.Columns.Add("RECEIPTLOCATOR");
                dtmaterialitemspec.Columns.Add("RECEIPTROUTE");
                dtmaterialitemspec.Columns.Add("VENDORID");
                dtmaterialitemspec.Columns.Add("MAKER");
                dtmaterialitemspec.Columns.Add("ORIGIN");
                dtmaterialitemspec.Columns.Add("NORMALLEADTIME");
                dtmaterialitemspec.Columns.Add("URGENCYLEADTIME");
                dtmaterialitemspec.Columns.Add("TARIFFRATE");
                dtmaterialitemspec.Columns.Add("BASEBANDRATE");
                dtmaterialitemspec.Columns.Add("DOUBLESINGLESIDED");
                dtmaterialitemspec.Columns.Add("PITHICKNESS");
                dtmaterialitemspec.Columns.Add("THICKNESSCOPPER");
                dtmaterialitemspec.Columns.Add("COPPERSPEC");
                dtmaterialitemspec.Columns.Add("MATERIALWIDTH");
                dtmaterialitemspec.Columns.Add("MATERIALLENGTH");
                dtmaterialitemspec.Columns.Add("EXPIRATIONDATE");
                dtmaterialitemspec.Columns.Add("MAKERECEIPTTYPE");
                dtmaterialitemspec.Columns.Add("COLOR");
                dtmaterialitemspec.Columns.Add("ISCONTAINHALOGEN");
                dtmaterialitemspec.Columns.Add("THICKNESSADH");
                dtmaterialitemspec.Columns.Add("MATERIALTHICKNESS");
                dtmaterialitemspec.Columns.Add("MATERIALWEIGHT");
                dtmaterialitemspec.Columns.Add("DESCRIPTION");
                dtmaterialitemspec.Columns.Add("VALIDSTATE");
                dtmaterialitemspec.Columns.Add("_STATE_");

                // 자재 정의 정보
                DataTable dtconsumabledefinition = new DataTable();
                dtconsumabledefinition.Columns.Add("CONSUMABLEDEFID");
                dtconsumabledefinition.Columns.Add("CONSUMABLEDEFVERSION");
                dtconsumabledefinition.Columns.Add("CONSUMABLECLASSID");
                dtconsumabledefinition.Columns.Add("CONSUMABLEDEFNAME");
                dtconsumabledefinition.Columns.Add("ENTERPRISEID");
                dtconsumabledefinition.Columns.Add("CONSUMABLETYPE");
                dtconsumabledefinition.Columns.Add("UNIT");
                dtconsumabledefinition.Columns.Add("VALIDSTATE");
                dtconsumabledefinition.Columns.Add("DESCRIPTION");
                dtconsumabledefinition.Columns.Add("_STATE_");
                dtconsumabledefinition.Columns.Add("ISLOTMNG");

                // 자재 그룹 정보
                DataTable dtConsumableclass = new DataTable();
                dtConsumableclass.Columns.Add("CONSUMABLECLASSID");
                dtConsumableclass.Columns.Add("CONSUMABLECLASSNAME");
                dtConsumableclass.Columns.Add("DESCRIPTION");
                dtConsumableclass.Columns.Add("ENTERPRISEID");
                dtConsumableclass.Columns.Add("CONSUMABLECLASSTYPE");
                dtConsumableclass.Columns.Add("VALIDSTATE");
                dtConsumableclass.Columns.Add("_STATE_");

                xlApp = new Excel.Application();
                xlWorkbook = xlApp.Workbooks.Open(ofd.FileName);
                xlWorksheet = (Excel.Worksheet)xlWorkbook.Worksheets.Item[1];


                Excel.Range range = xlWorksheet.UsedRange;

                object[,] data = range.Value;

                for (int r = 3; r <= range.Rows.Count; r++)
                {
                    DataRow rowProductCNV = dtMaterialCNV.NewRow();
                    for (int c = 1; c <= range.Columns.Count; c++)
                    {
                        rowProductCNV[data[2, c].ToString().ToUpper()] = data[r, c];
                    }
                    rowProductCNV["VALIDSTATE"] = "Valid";
                    rowProductCNV["_STATE_"] = "added";
                    dtMaterialCNV.Rows.Add(rowProductCNV);
                }

                xlWorkbook.Close(true);
                xlApp.Quit();

                //ExecuteRule("Materialcnv", dtMaterialCNV);


                DataTable dtmaterialcnv = SqlExecuter.Query("Getmaterialcnv", "10001");

                foreach (DataRow rowproductcnv in dtmaterialcnv.Rows)
                {
                    DataRow rowitemmaster = dtitemmaster.NewRow();

                    foreach (DataColumn colitemmaster in dtmaterialcnv.Columns)
                    {
                        if (dtitemmaster.Columns.IndexOf(colitemmaster.ColumnName) != -1)
                        {
                            if(colitemmaster.ColumnName != "IMPLEMENTATIONDATE")
                            {
                                rowitemmaster[colitemmaster.ColumnName] = rowproductcnv[colitemmaster.ColumnName];
                            }
                            
                        }

                    }

                    DateTime dtIMPLEMENTATIONDATE = new DateTime();
                    dtIMPLEMENTATIONDATE = DateTime.Parse(rowproductcnv["IMPLEMENTATIONDATE"].ToString());

                    rowitemmaster["IMPLEMENTATIONDATE"] = dtIMPLEMENTATIONDATE.ToString("yyyy-MM-dd HH:ss:ss");

                    rowitemmaster["ITEMUOM"] = rowproductcnv["UOMDEFID"];
                    rowitemmaster["ITEMSTATUS"] = rowproductcnv["STATUS"];


                    rowitemmaster["_STATE_"] = "added";

                    dtitemmaster.Rows.Add(rowitemmaster);
                }



                foreach (DataRow rowitemmaster in dtitemmaster.Rows)
                {
                    //자재 그룹  정보 조회
                    Dictionary<string, object> paramConsum = new Dictionary<string, object>();
                    paramConsum.Add("CONSUMABLECLASSID", rowitemmaster["MASTERDATACLASSID"].ToString());
                    DataTable dtConsum = SqlExecuter.Query("GetConsumableclassList", "10001", paramConsum);

                    if (dtConsum != null)
                    {
                        if (dtConsum.Rows.Count == 0)
                        {
                            // 자재 그룹 정보  등록
                            DataRow rowCclNew = dtConsumableclass.NewRow();
                            rowCclNew["CONSUMABLECLASSID"] = rowitemmaster["MASTERDATACLASSID"].ToString();
                            //rowCclNew["CONSUMABLECLASSNAME"] = rowMDC["MASTERDATACLASSNAME"].ToString();
                            rowCclNew["DESCRIPTION"] = rowitemmaster["DESCRIPTION"].ToString();
                            rowCclNew["ENTERPRISEID"] = rowitemmaster["ENTERPRISEID"].ToString();
                            rowCclNew["CONSUMABLECLASSTYPE"] = rowitemmaster["MASTERDATACLASSID"].ToString();
                            rowCclNew["VALIDSTATE"] = "Valid";
                            rowCclNew["_STATE_"] = "added";
                            dtConsumableclass.Rows.Add(rowCclNew);
                        }
                        else
                        {
                            // 자재 그룹 정보  등록
                            DataRow rowCclNew = dtConsumableclass.NewRow();
                            rowCclNew["CONSUMABLECLASSID"] = rowitemmaster["MASTERDATACLASSID"].ToString();
                            //rowCclNew["CONSUMABLECLASSNAME"] = rowMDC["MASTERDATACLASSNAME"].ToString();
                            rowCclNew["DESCRIPTION"] = rowitemmaster["DESCRIPTION"].ToString();
                            rowCclNew["ENTERPRISEID"] = rowitemmaster["ENTERPRISEID"].ToString();
                            rowCclNew["CONSUMABLECLASSTYPE"] = rowitemmaster["MASTERDATACLASSID"].ToString();
                            rowCclNew["VALIDSTATE"] = "Valid";
                            rowCclNew["_STATE_"] = "modified";
                            dtConsumableclass.Rows.Add(rowCclNew);
                        }
                    }

                    //정보 등록
                    DataRow rowconsumabledefinition = dtconsumabledefinition.NewRow();
                    rowconsumabledefinition["CONSUMABLEDEFID"] = rowitemmaster["ITEMID"];
                    rowconsumabledefinition["CONSUMABLEDEFVERSION"] = rowitemmaster["ITEMVERSION"];
                    rowconsumabledefinition["CONSUMABLECLASSID"] = rowitemmaster["MASTERDATACLASSID"];
                    rowconsumabledefinition["CONSUMABLEDEFNAME"] = rowitemmaster["ITEMNAME"];
                    rowconsumabledefinition["ENTERPRISEID"] = rowitemmaster["ENTERPRISEID"];
                    rowconsumabledefinition["CONSUMABLETYPE"] = rowitemmaster["CONSUMABLETYPE"];
                    rowconsumabledefinition["UNIT"] = rowitemmaster["ITEMUOM"];
                    rowconsumabledefinition["DESCRIPTION"] = rowitemmaster["DESCRIPTION"];
                    rowconsumabledefinition["_STATE_"] = "added";
                    rowconsumabledefinition["VALIDSTATE"] = "Valid";

                    rowconsumabledefinition["ISLOTMNG"] = rowitemmaster["LOTCONTROL"];

                    dtconsumabledefinition.Rows.Add(rowconsumabledefinition);
                }
                
                DataSet dsMaterialCNV = new DataSet();
                // 품목
                dtitemmaster.TableName = "itemmaster";
                dsMaterialCNV.Tables.Add(dtitemmaster);

                // 제품유형
                dtConsumableclass.TableName = "consumableclass";
                dsMaterialCNV.Tables.Add(dtConsumableclass);

                //// 품목스펙
                //dtproductitemspec.TableName = "productitemspec";
                //dsProductCNV.Tables.Add(dtproductitemspec);

                // 제품 정보
                //dtproductdefinition.TableName = "productdefinition";
                //dsProductCNV.Tables.Add(dtproductdefinition);

                // 자재 그룹 정보
                dtconsumabledefinition.TableName = "consumabledefinition";
                dsMaterialCNV.Tables.Add(dtconsumabledefinition);

                ExecuteRule("ItemMaster", dsMaterialCNV);


                foreach (DataRow rowmaterialcnv in dtmaterialcnv.Rows)
                {
                    DataRow rowmaterialitemspec = dtmaterialitemspec.NewRow();

                    foreach (DataColumn colmmproductitemspec in dtmaterialcnv.Columns)
                    {
                        if (dtmaterialitemspec.Columns.IndexOf(colmmproductitemspec.ColumnName) != -1)
                        {
                            rowmaterialitemspec[colmmproductitemspec.ColumnName] = rowmaterialcnv[colmmproductitemspec.ColumnName];
                        }
                    }

                    rowmaterialitemspec["_STATE_"] = "added";

                    dtmaterialitemspec.Rows.Add(rowmaterialitemspec);
                }
                ExecuteRule("MaterialItemSpec", dtmaterialitemspec);

                
                ShowMessage("SuccedSave");
            }






        }

        private void btnProductItemUpload_Click(object sender, EventArgs e)
        {

            Excel.Application xlApp = null;
            Excel.Workbook xlWorkbook = null;
            Excel.Worksheet xlWorksheet = null;


            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Excel File (*.xlsx)|*.xlsx|Excel File 97!2003 (*.xls)|*.xls|All Files (*.*)|*.*";


            if (ofd.ShowDialog() == DialogResult.OK)
            {
                

                DataTable dtProductCNV = new DataTable();
                dtProductCNV.Columns.Add("ITEMID");
                dtProductCNV.Columns.Add("ITEMVERSION");
                dtProductCNV.Columns.Add("ENTERPRISEID");
                dtProductCNV.Columns.Add("MASTERDATACLASSID");
                dtProductCNV.Columns.Add("PLANTID");
                dtProductCNV.Columns.Add("ITEMCODE");
                dtProductCNV.Columns.Add("ITEMNAME");
                dtProductCNV.Columns.Add("DUPLICATION");
                dtProductCNV.Columns.Add("UOMDEFID");
                dtProductCNV.Columns.Add("NEWREQUEST");
                dtProductCNV.Columns.Add("ENGINEERINGCHANGE");
                dtProductCNV.Columns.Add("IMPLEMENTATIONDATE");
                dtProductCNV.Columns.Add("STATUS");
                dtProductCNV.Columns.Add("CONSUMABLETYPE");
                dtProductCNV.Columns.Add("PRODUCTIONTYPE");
                dtProductCNV.Columns.Add("LEADTIME");
                dtProductCNV.Columns.Add("TACTTIME");
                dtProductCNV.Columns.Add("ITEMTYPE");
                dtProductCNV.Columns.Add("COSTCATEGORY");
                dtProductCNV.Columns.Add("SALEORDERCATEGORY");
                dtProductCNV.Columns.Add("INVENTORYCATEGORY");
                dtProductCNV.Columns.Add("MAKEBUYTYPE");
                dtProductCNV.Columns.Add("PLANNER");
                dtProductCNV.Columns.Add("UNITOFPURCHASING");
                dtProductCNV.Columns.Add("UNITOFSTOCK");
                dtProductCNV.Columns.Add("SPEC");
                dtProductCNV.Columns.Add("PRICE");
                dtProductCNV.Columns.Add("AGING");
                dtProductCNV.Columns.Add("AGINGDAY");
                dtProductCNV.Columns.Add("CYCLECOUNT");
                dtProductCNV.Columns.Add("ENDTYPE");
                dtProductCNV.Columns.Add("LOTCONTROL");
                dtProductCNV.Columns.Add("ISINVENTORY");
                dtProductCNV.Columns.Add("ISSALES");
                dtProductCNV.Columns.Add("ISSHIPPING");
                dtProductCNV.Columns.Add("ISBOM");
                dtProductCNV.Columns.Add("ISLOTMERGE");
                dtProductCNV.Columns.Add("ISPARAMETER");
                dtProductCNV.Columns.Add("ISPURCHASE");
                dtProductCNV.Columns.Add("ISINTERNALPURCHASE");
                dtProductCNV.Columns.Add("CURRENCY");
                dtProductCNV.Columns.Add("PRODUCTTYPE");
                dtProductCNV.Columns.Add("FACTORYID");
                dtProductCNV.Columns.Add("CUSTOMERID");
                dtProductCNV.Columns.Add("CUSTOMERNAME");
                dtProductCNV.Columns.Add("CUSTOMERITEMID");
                dtProductCNV.Columns.Add("CUSTOMERITEMVERSION");
                dtProductCNV.Columns.Add("CUSTOMERITEMNAME");
                dtProductCNV.Columns.Add("CUSTOMERSPEC");
                dtProductCNV.Columns.Add("HSCODE");
                dtProductCNV.Columns.Add("LAYER");
                dtProductCNV.Columns.Add("USELAYER");
                dtProductCNV.Columns.Add("COPPERTYPE");
                dtProductCNV.Columns.Add("PACKINGQTY");
                dtProductCNV.Columns.Add("PROJECTNAME");
                dtProductCNV.Columns.Add("ENDUSER");
                dtProductCNV.Columns.Add("COPPERPLATINGTYPE");
                dtProductCNV.Columns.Add("PRODUCTTHICKNESS");
                dtProductCNV.Columns.Add("UL_MARK");
                dtProductCNV.Columns.Add("PRODUCTRATING");
                dtProductCNV.Columns.Add("ISWEEKMNG");
                dtProductCNV.Columns.Add("MANUFACTUREDDATE");
                dtProductCNV.Columns.Add("HG_FR");
                dtProductCNV.Columns.Add("ASSY");
                dtProductCNV.Columns.Add("OXIDE");
                dtProductCNV.Columns.Add("SEPARATINGPORTION");
                dtProductCNV.Columns.Add("RTRSHT");
                dtProductCNV.Columns.Add("IMPEDANCE");
                dtProductCNV.Columns.Add("INPUTTYPE");
                dtProductCNV.Columns.Add("PCSSIZEXAXIS");
                dtProductCNV.Columns.Add("PCSSIZEYAXIS");
                dtProductCNV.Columns.Add("ARYSIZEXAXIS");
                dtProductCNV.Columns.Add("ARYSIZEYAXIS");
                dtProductCNV.Columns.Add("PNLSIZEXAXIS");
                dtProductCNV.Columns.Add("PNLSIZEYAXIS");
                dtProductCNV.Columns.Add("PCSPNL");
                dtProductCNV.Columns.Add("PNLMM");
                dtProductCNV.Columns.Add("PCSMM");
                dtProductCNV.Columns.Add("PCSARY");
                dtProductCNV.Columns.Add("INPUTSIZEXAXIS");
                dtProductCNV.Columns.Add("HOLEPLATINGAREA");
                dtProductCNV.Columns.Add("INNERLAYER");
                dtProductCNV.Columns.Add("OUTERLAYER");
                dtProductCNV.Columns.Add("INNERLAYERCIRCUIT");
                dtProductCNV.Columns.Add("OUTERLAYERCIRCUIT");
                dtProductCNV.Columns.Add("COPPERFOILUPLAYER");
                dtProductCNV.Columns.Add("COPPERFOILDOWNLAYER");
                dtProductCNV.Columns.Add("INNERLAYERTO");
                dtProductCNV.Columns.Add("OUTERLAYERTO");
                dtProductCNV.Columns.Add("JOBTYPE");
                dtProductCNV.Columns.Add("CONNECTORDISTANCE");
                dtProductCNV.Columns.Add("CONNECTORTILTING");
                dtProductCNV.Columns.Add("DUMMY");
                dtProductCNV.Columns.Add("INNERCIRCUITDISTANCE");
                dtProductCNV.Columns.Add("OUTERCIRCUITDISTANCE");
                dtProductCNV.Columns.Add("INNERCIRCUITCOPPER");
                dtProductCNV.Columns.Add("OUTERCIRCUITCOPPER");
                dtProductCNV.Columns.Add("INPUTSCALE");
                dtProductCNV.Columns.Add("RELIABILITY");
                dtProductCNV.Columns.Add("HAZARDOUSSUBSTANCES");
                dtProductCNV.Columns.Add("MEASUREMENT");
                dtProductCNV.Columns.Add("INKSPECIFICATION");
                dtProductCNV.Columns.Add("OLBCIRCUIT");
                dtProductCNV.Columns.Add("ELONGATION");
                dtProductCNV.Columns.Add("PITCHBEFORE");
                dtProductCNV.Columns.Add("PITCHAFTER");
                dtProductCNV.Columns.Add("PCSIMAGEID");
                dtProductCNV.Columns.Add("MINCL");
                dtProductCNV.Columns.Add("MINPSR");
                dtProductCNV.Columns.Add("SMD");
                dtProductCNV.Columns.Add("SALESMAN");
                dtProductCNV.Columns.Add("SPECIFICATIONMAN");
                dtProductCNV.Columns.Add("CAMMAN");
                dtProductCNV.Columns.Add("SURFACEPLATINGTYPE");
                dtProductCNV.Columns.Add("PRODUCTDIMENSIONS");
                dtProductCNV.Columns.Add("XOUT");
                dtProductCNV.Columns.Add("ORDERDATE");
                dtProductCNV.Columns.Add("DELIVERYDATE");
                dtProductCNV.Columns.Add("INVALIDDATE");
                dtProductCNV.Columns.Add("DESCRIPTION");
                dtProductCNV.Columns.Add("VALIDSTATE");
                dtProductCNV.Columns.Add("_STATE_");


                // 제품 그룹 정보
                DataTable dtProductclass = new DataTable();
                dtProductclass.Columns.Add("PRODUCTCLASSID");
                dtProductclass.Columns.Add("PRODUCTCLASSTYPE");
                dtProductclass.Columns.Add("_STATE_");

                // 사양 스펙
                DataTable dtproductitemspec = new DataTable();
                dtproductitemspec.Columns.Add("MASTERDATACLASSID");
                dtproductitemspec.Columns.Add("ENTERPRISEID");
                dtproductitemspec.Columns.Add("ITEMID");
                dtproductitemspec.Columns.Add("ITEMVERSION");
                dtproductitemspec.Columns.Add("PRODUCTTYPE");
                dtproductitemspec.Columns.Add("VALIDSTATE");
                dtproductitemspec.Columns.Add("_STATE_");


                // 제품 정보
                DataTable dtproductdefinition = new DataTable();
                dtproductdefinition.Columns.Add("PRODUCTDEFID");
                dtproductdefinition.Columns.Add("PRODUCTDEFVERSION");

                dtproductdefinition.Columns.Add("PROCESSDEFID");
                dtproductdefinition.Columns.Add("PROCESSDEFVERSION");

                dtproductdefinition.Columns.Add("PRODUCTCLASSID");
                dtproductdefinition.Columns.Add("PRODUCTDEFNAME");
                dtproductdefinition.Columns.Add("ENTERPRISEID");
                dtproductdefinition.Columns.Add("PRODUCTDEFTYPE");
                dtproductdefinition.Columns.Add("PRODUCTIONTYPE");
                dtproductdefinition.Columns.Add("UNIT");
                dtproductdefinition.Columns.Add("LEADTIME");
                dtproductdefinition.Columns.Add("DESCRIPTION");
                dtproductdefinition.Columns.Add("_STATE_");
                dtproductdefinition.Columns.Add("VALIDSTATE");
                dtproductdefinition.Columns.Add("MATERIALCLASS");
                dtproductdefinition.Columns.Add("PLANTID");
                dtproductdefinition.Columns.Add("PRODUCTSHAPE");

                dtproductdefinition.Columns.Add("OWNER");
                dtproductdefinition.Columns.Add("CUSTOMERID");
                dtproductdefinition.Columns.Add("LAYER");
                dtproductdefinition.Columns.Add("ISWEEKMNG");
                dtproductdefinition.Columns.Add("RTRSHT");
                dtproductdefinition.Columns.Add("INPUTTYPE");
                dtproductdefinition.Columns.Add("PCSSIZEXAXIS");
                dtproductdefinition.Columns.Add("PCSSIZEYAXIS");
                dtproductdefinition.Columns.Add("ARYSIZEXAXIS"); 
                dtproductdefinition.Columns.Add("ARYSIZEYAXIS"); 
                dtproductdefinition.Columns.Add("PNLSIZEXAXIS"); 
                dtproductdefinition.Columns.Add("PNLSIZEYAXIS");
                dtproductdefinition.Columns.Add("PCSPNL");
                dtproductdefinition.Columns.Add("PNLMM");
                dtproductdefinition.Columns.Add("PCSMM");
                dtproductdefinition.Columns.Add("PCSARY");
                dtproductdefinition.Columns.Add("XOUT");


                // 자재 그룹 정보
                DataTable dtConsumableclass = new DataTable();
                dtConsumableclass.Columns.Add("CONSUMABLECLASSID");
                dtConsumableclass.Columns.Add("CONSUMABLECLASSNAME");
                dtConsumableclass.Columns.Add("DESCRIPTION");
                dtConsumableclass.Columns.Add("ENTERPRISEID");
                dtConsumableclass.Columns.Add("CONSUMABLECLASSTYPE");
                dtConsumableclass.Columns.Add("VALIDSTATE");
                dtConsumableclass.Columns.Add("_STATE_");

                // 자재 정의 정보
                DataTable dtconsumabledefinition = new DataTable();
                dtconsumabledefinition.Columns.Add("CONSUMABLEDEFID");
                dtconsumabledefinition.Columns.Add("CONSUMABLEDEFVERSION");
                dtconsumabledefinition.Columns.Add("CONSUMABLECLASSID");
                dtconsumabledefinition.Columns.Add("CONSUMABLEDEFNAME");
                dtconsumabledefinition.Columns.Add("ENTERPRISEID");
                dtconsumabledefinition.Columns.Add("CONSUMABLETYPE");
                dtconsumabledefinition.Columns.Add("UNIT");
                dtconsumabledefinition.Columns.Add("VALIDSTATE");
                dtconsumabledefinition.Columns.Add("DESCRIPTION");
                dtconsumabledefinition.Columns.Add("_STATE_");
                dtconsumabledefinition.Columns.Add("ISLOTMNG");


                DataTable dtitemmaster = new DataTable();
                dtitemmaster.Columns.Add("MASTERDATACLASSID");
                dtitemmaster.Columns.Add("ITEMID");
                dtitemmaster.Columns.Add("ITEMVERSION");
                dtitemmaster.Columns.Add("ENTERPRISEID");
                dtitemmaster.Columns.Add("PLANTID");
                dtitemmaster.Columns.Add("ITEMCODE");
                dtitemmaster.Columns.Add("ITEMNAME");
                dtitemmaster.Columns.Add("DUPLICATION");
                dtitemmaster.Columns.Add("ITEMUOM");  
                dtitemmaster.Columns.Add("NEWREQUEST");
                dtitemmaster.Columns.Add("ENGINEERINGCHANGE");
                dtitemmaster.Columns.Add("IMPLEMENTATIONDATE");
                dtitemmaster.Columns.Add("ITEMSTATUS");
                dtitemmaster.Columns.Add("CONSUMABLETYPE");
                dtitemmaster.Columns.Add("PRODUCTIONTYPE");
                dtitemmaster.Columns.Add("LEADTIME");
                dtitemmaster.Columns.Add("TACTTIME");
                dtitemmaster.Columns.Add("ITEMTYPE");
                dtitemmaster.Columns.Add("COSTCATEGORY");
                dtitemmaster.Columns.Add("SALEORDERCATEGORY");
                dtitemmaster.Columns.Add("INVENTORYCATEGORY");
                dtitemmaster.Columns.Add("MAKEBUYTYPE");
                dtitemmaster.Columns.Add("PLANNER");
                dtitemmaster.Columns.Add("UNITOFPURCHASING");
                dtitemmaster.Columns.Add("UNITOFSTOCK");
                dtitemmaster.Columns.Add("SPEC");
                dtitemmaster.Columns.Add("PRICE");
                dtitemmaster.Columns.Add("AGING");
                dtitemmaster.Columns.Add("AGINGDAY");
                dtitemmaster.Columns.Add("CYCLECOUNT");
                dtitemmaster.Columns.Add("ENDTYPE");
                dtitemmaster.Columns.Add("LOTCONTROL");
                dtitemmaster.Columns.Add("ISINVENTORY");
                dtitemmaster.Columns.Add("ISSALES");
                dtitemmaster.Columns.Add("ISSHIPPING");
                dtitemmaster.Columns.Add("ISBOM");
                dtitemmaster.Columns.Add("ISLOTMERGE");
                dtitemmaster.Columns.Add("ISPARAMETER");
                dtitemmaster.Columns.Add("ISPURCHASE");
                dtitemmaster.Columns.Add("ISINTERNALPURCHASE");
                dtitemmaster.Columns.Add("CURRENCY");
                dtitemmaster.Columns.Add("DESCRIPTION");
                dtitemmaster.Columns.Add("PRODUCTTYPE");
                
                dtitemmaster.Columns.Add("VALIDSTATE");
                dtitemmaster.Columns.Add("_STATE_");


                DataTable dtmmproductitemspec = new DataTable();
                dtmmproductitemspec.Columns.Add("ITEMID");
                dtmmproductitemspec.Columns.Add("ITEMVERSION");
                dtmmproductitemspec.Columns.Add("ENTERPRISEID");
                dtmmproductitemspec.Columns.Add("PLANTID");
                dtmmproductitemspec.Columns.Add("PRODUCTTYPE");
                dtmmproductitemspec.Columns.Add("FACTORYID");
                dtmmproductitemspec.Columns.Add("CUSTOMERID");
                dtmmproductitemspec.Columns.Add("CUSTOMERNAME");
                dtmmproductitemspec.Columns.Add("CUSTOMERITEMID");
                dtmmproductitemspec.Columns.Add("CUSTOMERITEMVERSION");
                dtmmproductitemspec.Columns.Add("CUSTOMERITEMNAME");
                dtmmproductitemspec.Columns.Add("CUSTOMERSPEC");
                dtmmproductitemspec.Columns.Add("HSCODE");
                dtmmproductitemspec.Columns.Add("LAYER");
                dtmmproductitemspec.Columns.Add("USELAYER");
                dtmmproductitemspec.Columns.Add("COPPERTYPE");
                dtmmproductitemspec.Columns.Add("PACKINGQTY");
                dtmmproductitemspec.Columns.Add("PROJECTNAME");
                dtmmproductitemspec.Columns.Add("ENDUSER");
                dtmmproductitemspec.Columns.Add("COPPERPLATINGTYPE");
                dtmmproductitemspec.Columns.Add("PRODUCTTHICKNESS");
                dtmmproductitemspec.Columns.Add("UL_MARK");
                dtmmproductitemspec.Columns.Add("PRODUCTRATING");
                dtmmproductitemspec.Columns.Add("ISWEEKMNG");
                dtmmproductitemspec.Columns.Add("MANUFACTUREDDATE");
                dtmmproductitemspec.Columns.Add("HG_FR");
                dtmmproductitemspec.Columns.Add("ASSY");
                dtmmproductitemspec.Columns.Add("OXIDE");
                dtmmproductitemspec.Columns.Add("SEPARATINGPORTION");
                dtmmproductitemspec.Columns.Add("RTRSHT");
                dtmmproductitemspec.Columns.Add("IMPEDANCE");
                dtmmproductitemspec.Columns.Add("INPUTTYPE");
                dtmmproductitemspec.Columns.Add("PCSSIZEXAXIS");
                dtmmproductitemspec.Columns.Add("PCSSIZEYAXIS");
                dtmmproductitemspec.Columns.Add("ARYSIZEXAXIS");
                dtmmproductitemspec.Columns.Add("ARYSIZEYAXIS");
                dtmmproductitemspec.Columns.Add("PNLSIZEXAXIS");
                dtmmproductitemspec.Columns.Add("PNLSIZEYAXIS");
                dtmmproductitemspec.Columns.Add("PCSPNL");
                dtmmproductitemspec.Columns.Add("PNLMM");
                dtmmproductitemspec.Columns.Add("PCSMM");
                dtmmproductitemspec.Columns.Add("PCSARY");
                dtmmproductitemspec.Columns.Add("INPUTSIZEXAXIS");
                dtmmproductitemspec.Columns.Add("HOLEPLATINGAREA");
                dtmmproductitemspec.Columns.Add("INNERLAYER");
                dtmmproductitemspec.Columns.Add("OUTERLAYER");
                dtmmproductitemspec.Columns.Add("INNERLAYERCIRCUIT");
                dtmmproductitemspec.Columns.Add("OUTERLAYERCIRCUIT");
                dtmmproductitemspec.Columns.Add("COPPERFOILUPLAYER");
                dtmmproductitemspec.Columns.Add("COPPERFOILDOWNLAYER");
                dtmmproductitemspec.Columns.Add("INNERLAYERTO");
                dtmmproductitemspec.Columns.Add("OUTERLAYERTO");
                dtmmproductitemspec.Columns.Add("JOBTYPE");
                dtmmproductitemspec.Columns.Add("PRODUCTIONTYPE");
                dtmmproductitemspec.Columns.Add("CONNECTORDISTANCE");
                dtmmproductitemspec.Columns.Add("CONNECTORTILTING");
                dtmmproductitemspec.Columns.Add("DUMMY");
                dtmmproductitemspec.Columns.Add("INNERCIRCUITDISTANCE");
                dtmmproductitemspec.Columns.Add("OUTERCIRCUITDISTANCE");
                dtmmproductitemspec.Columns.Add("INNERCIRCUITCOPPER");
                dtmmproductitemspec.Columns.Add("OUTERCIRCUITCOPPER");
                dtmmproductitemspec.Columns.Add("INPUTSCALE");
                dtmmproductitemspec.Columns.Add("RELIABILITY");
                dtmmproductitemspec.Columns.Add("HAZARDOUSSUBSTANCES");
                dtmmproductitemspec.Columns.Add("MEASUREMENT");
                dtmmproductitemspec.Columns.Add("INKSPECIFICATION");
                dtmmproductitemspec.Columns.Add("OLBCIRCUIT");
                dtmmproductitemspec.Columns.Add("ELONGATION");
                dtmmproductitemspec.Columns.Add("PITCHBEFORE");
                dtmmproductitemspec.Columns.Add("PITCHAFTER");
                dtmmproductitemspec.Columns.Add("PCSIMAGEID");
                dtmmproductitemspec.Columns.Add("MINCL");
                dtmmproductitemspec.Columns.Add("MINPSR");
                dtmmproductitemspec.Columns.Add("SMD");
                dtmmproductitemspec.Columns.Add("SALESMAN");
                dtmmproductitemspec.Columns.Add("SPECIFICATIONMAN");
                dtmmproductitemspec.Columns.Add("CAMMAN");
                dtmmproductitemspec.Columns.Add("SURFACEPLATINGTYPE");
                dtmmproductitemspec.Columns.Add("PRODUCTDIMENSIONS");
                dtmmproductitemspec.Columns.Add("XOUT");
                dtmmproductitemspec.Columns.Add("ORDERDATE");
                dtmmproductitemspec.Columns.Add("DELIVERYDATE");
                dtmmproductitemspec.Columns.Add("INVALIDDATE");
                dtmmproductitemspec.Columns.Add("DESCRIPTION");
                dtmmproductitemspec.Columns.Add("VALIDSTATE");
                dtmmproductitemspec.Columns.Add("_STATE_");

               


                ////xlApp = new Excel.Application();
                ////xlWorkbook = xlApp.Workbooks.Open(ofd.FileName);
                ////xlWorksheet = (Excel.Worksheet)xlWorkbook.Worksheets.Item[1];


                ////Excel.Range range = xlWorksheet.UsedRange;

                ////object[,] data = range.Value;

                ////for(int r=3;r<=range.Rows.Count;r++)
                ////{
                ////    DataRow rowProductCNV = dtProductCNV.NewRow();
                ////    for (int c = 1; c <= range.Columns.Count;c++)
                ////    {
                ////        rowProductCNV[data[2, c].ToString().ToUpper()] = data[r, c];
                ////    }
                ////    rowProductCNV["VALIDSTATE"] = "Valid";
                ////    rowProductCNV["_STATE_"] = "added";
                ////    dtProductCNV.Rows.Add(rowProductCNV);
                ////}

                ////xlWorkbook.Close(true);
                ////xlApp.Quit();

                ////ExecuteRule("Productcnv", dtProductCNV);

                DataTable dtproductcnv = SqlExecuter.Query("Getproductcnv", "10001");

                foreach(DataRow rowproductcnv in  dtproductcnv.Rows)
                {
                    DataRow rowitemmaster = dtitemmaster.NewRow();

                    foreach (DataColumn colitemmaster in dtproductcnv.Columns)
                    {
                        if(dtitemmaster.Columns.IndexOf(colitemmaster.ColumnName) != -1)
                        {
                            rowitemmaster[colitemmaster.ColumnName] = rowproductcnv[colitemmaster.ColumnName];
                        }
                        
                    }

                    rowitemmaster["ITEMUOM"] = rowproductcnv["UOMDEFID"];
                    rowitemmaster["ITEMSTATUS"] = rowproductcnv["STATUS"];
                    

                    rowitemmaster["_STATE_"] = "added";

                    dtitemmaster.Rows.Add(rowitemmaster);
                }




                foreach (DataRow rowitemmaster in dtitemmaster.Rows)
                {
                    // 제품 그룹 정보 유무 체크
                    Dictionary<string, object> paramPcl = new Dictionary<string, object>();
                    paramPcl.Add("PRODUCTCLASSID", rowitemmaster["PRODUCTTYPE"].ToString());
                    DataTable dtPclChk = SqlExecuter.Query("GetProductclassList", "10001", paramPcl);

                    if (dtPclChk != null)
                    {
                        if (dtPclChk.Rows.Count == 0)
                        {
                            // 제품 그룹 정보  등록
                            DataRow rowPclNew = dtProductclass.NewRow();
                            rowPclNew["PRODUCTCLASSID"] = rowitemmaster["PRODUCTTYPE"].ToString();
                            rowPclNew["PRODUCTCLASSTYPE"] = rowitemmaster["MASTERDATACLASSID"].ToString();
                            dtProductclass.Rows.Add(rowPclNew);
                        }
                    }
                    else
                    {
                        // 제품 그룹 정보  등록
                        DataRow rowPclNew = dtProductclass.NewRow();
                        rowPclNew["PRODUCTCLASSID"] = rowitemmaster["PRODUCTTYPE"].ToString();
                        rowPclNew["PRODUCTCLASSTYPE"] = rowitemmaster["MASTERDATACLASSID"].ToString();
                        dtProductclass.Rows.Add(rowPclNew);
                    }


                    // 품목 스펙 등록
                    DataRow rowproductitemspec = dtproductitemspec.NewRow();

                    rowproductitemspec["MASTERDATACLASSID"] = rowitemmaster["MASTERDATACLASSID"];
                    rowproductitemspec["ENTERPRISEID"] = rowitemmaster["ENTERPRISEID"];
                    rowproductitemspec["ITEMID"] = rowitemmaster["ITEMID"];
                    rowproductitemspec["ITEMVERSION"] = rowitemmaster["ITEMVERSION"];
                    rowproductitemspec["PRODUCTTYPE"] = rowitemmaster["PRODUCTTYPE"];
                    rowproductitemspec["VALIDSTATE"] = "Valid";
                    rowproductitemspec["_STATE_"] = "added";
                    dtproductitemspec.Rows.Add(rowproductitemspec);





                    //제품 정보 등록
                    DataRow rowproductdefinition = dtproductdefinition.NewRow();
                    rowproductdefinition["PRODUCTDEFID"] = rowitemmaster["ITEMID"];
                    rowproductdefinition["PRODUCTDEFVERSION"] = rowitemmaster["ITEMVERSION"];

                    rowproductdefinition["PROCESSDEFID"] = rowitemmaster["ITEMID"];
                    rowproductdefinition["PROCESSDEFVERSION"] = rowitemmaster["ITEMVERSION"];

                    rowproductdefinition["PRODUCTDEFNAME"] = rowitemmaster["ITEMNAME"];
                    rowproductdefinition["ENTERPRISEID"] = rowitemmaster["ENTERPRISEID"];
                    rowproductdefinition["PLANTID"] = UserInfo.Current.Plant;

                    rowproductdefinition["PRODUCTDEFTYPE"] = rowitemmaster["MASTERDATACLASSID"].ToString();


                    if (rowitemmaster["MASTERDATACLASSID"].ToString() == "SubAssembly")
                    {
                        rowproductdefinition["PRODUCTCLASSID"] = "SemiProduct";
                        
                    }
                    else
                    {
                        //인터인경우
                        if (rowitemmaster["ENTERPRISEID"].ToString() == "INTERFLEX")
                        {
                            // 상품경우
                            if (rowitemmaster["MASTERDATACLASSID"].ToString() == "Commodity")
                            {
                                rowproductdefinition["PRODUCTDEFTYPE"] = "";
                                rowproductdefinition["PRODUCTCLASSID"] = rowitemmaster["MASTERDATACLASSID"].ToString();
                            }
                            else
                            {
                                rowproductdefinition["PRODUCTDEFTYPE"] = rowitemmaster["MASTERDATACLASSID"].ToString();
                                rowproductdefinition["PRODUCTCLASSID"] = rowitemmaster["MASTERDATACLASSID"].ToString();
                            }
                        }
                        else
                        {
                            //영품인경우
                            if (rowitemmaster["MASTERDATACLASSID"].ToString() == "Commodity")
                            {
                                rowproductdefinition["PRODUCTDEFTYPE"] = "Product";
                                rowproductdefinition["PRODUCTCLASSID"] = rowitemmaster["MASTERDATACLASSID"].ToString();
                            }
                            else
                            {
                                rowproductdefinition["PRODUCTDEFTYPE"] = rowitemmaster["MASTERDATACLASSID"].ToString();
                                rowproductdefinition["PRODUCTCLASSID"] = rowitemmaster["MASTERDATACLASSID"].ToString();
                            }


                        }

                    }

                    rowproductdefinition["PRODUCTSHAPE"] = rowitemmaster["PRODUCTTYPE"];

                    //rowproductdefinition["PRODUCTDEFTYPE"] = row["MASTERDATACLASSID"];
                    rowproductdefinition["PRODUCTIONTYPE"] = rowitemmaster["PRODUCTIONTYPE"];
                    //rowproductdefinition["PRODUCTIONTYPE"] = "Production";

                    rowproductdefinition["UNIT"] = rowitemmaster["ITEMUOM"];
                    rowproductdefinition["LEADTIME"] = rowitemmaster["LEADTIME"];
                    rowproductdefinition["DESCRIPTION"] = rowitemmaster["DESCRIPTION"];
                    rowproductdefinition["VALIDSTATE"] = "Valid";
                    rowproductdefinition["_STATE_"] = "added";


                    rowproductdefinition["MATERIALCLASS"] = rowitemmaster["CONSUMABLETYPE"];

                    dtproductdefinition.Rows.Add(rowproductdefinition);

                    //반제품

                    if (rowitemmaster["MASTERDATACLASSID"].ToString() == "SubAssembly")
                    {
                        //반제품 정보 등록
                        DataRow rowconsumabledefinition = dtconsumabledefinition.NewRow();
                        rowconsumabledefinition["CONSUMABLEDEFID"] = rowitemmaster["ITEMID"];
                        rowconsumabledefinition["CONSUMABLEDEFVERSION"] = rowitemmaster["ITEMVERSION"];
                        rowconsumabledefinition["CONSUMABLECLASSID"] = rowitemmaster["MASTERDATACLASSID"];
                        rowconsumabledefinition["CONSUMABLEDEFNAME"] = rowitemmaster["ITEMNAME"];
                        rowconsumabledefinition["ENTERPRISEID"] = rowitemmaster["ENTERPRISEID"];
                        rowconsumabledefinition["CONSUMABLETYPE"] = rowitemmaster["CONSUMABLETYPE"];
                        rowconsumabledefinition["UNIT"] = rowitemmaster["ITEMUOM"];
                        rowconsumabledefinition["DESCRIPTION"] = rowitemmaster["DESCRIPTION"];
                        rowconsumabledefinition["_STATE_"] = "added";
                        rowconsumabledefinition["VALIDSTATE"] = "Valid";

                        rowconsumabledefinition["ISLOTMNG"] = rowitemmaster["LOTCONTROL"];

                        dtconsumabledefinition.Rows.Add(rowconsumabledefinition);

                    }


                }

                DataSet dsProductCNV = new DataSet();
                // 품목
                dtitemmaster.TableName = "itemmaster";
                dsProductCNV.Tables.Add(dtitemmaster);

                // 제품유형
                dtProductclass.TableName = "productclass";
                dsProductCNV.Tables.Add(dtProductclass);

                //// 품목스펙
                //dtproductitemspec.TableName = "productitemspec";
                //dsProductCNV.Tables.Add(dtproductitemspec);

                // 제품 정보
                dtproductdefinition.TableName = "productdefinition";
                dsProductCNV.Tables.Add(dtproductdefinition);

                // 자재 그룹 정보
                dtconsumabledefinition.TableName = "consumabledefinition";
                dsProductCNV.Tables.Add(dtconsumabledefinition);
               
                ExecuteRule("ItemMaster", dsProductCNV);

                foreach (DataRow rowproductcnv in dtproductcnv.Rows)
                {
                    DataRow rowmmproductitemspec = dtmmproductitemspec.NewRow();

                    foreach (DataColumn colmmproductitemspec in dtproductcnv.Columns)
                    {
                        if (dtmmproductitemspec.Columns.IndexOf(colmmproductitemspec.ColumnName) != -1)
                        {
                            rowmmproductitemspec[colmmproductitemspec.ColumnName] = rowproductcnv[colmmproductitemspec.ColumnName];
                        }
                    }

                    rowmmproductitemspec["_STATE_"] = "added";

                    dtmmproductitemspec.Rows.Add(rowmmproductitemspec);
                }


                DataTable dtproductdefinitionUp = dtproductdefinition.Clone();

                foreach (DataRow row in dtmmproductitemspec.Rows)
                {
                    DataRow rowNew = dtproductdefinitionUp.NewRow();
                    rowNew["PRODUCTDEFID"] = row["ITEMID"];
                    rowNew["PRODUCTDEFVERSION"] = row["ITEMVERSION"];
                    rowNew["PRODUCTSHAPE"] = row["PRODUCTTYPE"];
                    rowNew["OWNER"] = row["SPECIFICATIONMAN"];
                    rowNew["CUSTOMERID"] = row["CUSTOMERID"];
                    rowNew["LAYER"] = row["LAYER"];
                    rowNew["ISWEEKMNG"] = row["ISWEEKMNG"];
                    rowNew["RTRSHT"] = row["RTRSHT"];
                    rowNew["INPUTTYPE"] = row["INPUTTYPE"];
                    rowNew["PCSSIZEXAXIS"] = row["PCSSIZEXAXIS"];
                    rowNew["PCSSIZEYAXIS"] = row["PCSSIZEYAXIS"];
                    rowNew["ARYSIZEXAXIS"] = row["ARYSIZEXAXIS"];
                    rowNew["ARYSIZEYAXIS"] = row["ARYSIZEYAXIS"];
                    rowNew["PNLSIZEXAXIS"] = row["PNLSIZEXAXIS"];
                    rowNew["PNLSIZEYAXIS"] = row["PNLSIZEYAXIS"];
                    rowNew["PCSPNL"] = row["PCSPNL"];
                    rowNew["PNLMM"] = row["PNLMM"];
                    rowNew["PCSMM"] = row["PCSMM"];
                    rowNew["PCSARY"] = row["PCSARY"];
                    rowNew["XOUT"] = row["XOUT"];
                    rowNew["_STATE_"] = "modified";
                    dtproductdefinitionUp.Rows.Add(rowNew);
                }


                //dtproductdefinition.TableName = "productdefinition";


                DataSet dsmmProductItemSpec = new DataSet();
                dtmmproductitemspec.TableName = "productItemSpec";

                // 제품 정보
                dtproductdefinitionUp.TableName = "productdefinition";
                dsmmProductItemSpec.Tables.Add(dtproductdefinitionUp);
                dsmmProductItemSpec.Tables.Add(dtmmproductitemspec);

                ExecuteRule("ProductItemSpec", dsmmProductItemSpec);

                ShowMessage("SuccedSave");
            }

        }

      

        #endregion

        #region 툴바

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            // TODO : 저장 Rule 변경
           // DataTable changed = new DataTable();

          //  ExecuteRule("RULEID", changed);

        }

        #endregion

        #region 검색

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            // TODO : 조회 SP 변경
            var values = Conditions.GetValues();
            values.Add("p_languagetype", Framework.UserInfo.Current.LanguageType);

            //DataTable dt = await SqlExecuter.QueryAsync("SelectCustomerManagement", "10001", values);

            //if (dt.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            //{
            //    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            //}

            //grdCustomer.DataSource = dt;
        }

        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
        }

        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
        }

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            // TODO : 유효성 로직 변경
            //grdCustomer.View.CheckValidation();

            //DataTable changed = grdCustomer.GetChangedRows();
            
            //if (changed.Rows.Count == 0)
            //{
            //    // 저장할 데이터가 존재하지 않습니다.
            //    throw MessageException.Create("NoSaveData");
            //}
        }

        #endregion

        #region Private Function

        // TODO : 화면에서 사용할 내부 함수 추가

        #endregion

    }
}
