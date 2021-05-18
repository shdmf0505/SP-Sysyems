using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Utils;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Mask;
using Micube.Framework;
using Micube.Framework.SmartControls;

namespace Micube.SmartMES.StandardInfo
{
    public enum MaterialType
    {
        Product,
        SubAssembly
    }

    public static class BOMCalculatorList
    {
        public static Dictionary<MaterialType, List<BOMInfo>> BOMCalculators = new Dictionary<MaterialType, List<BOMInfo>>();

        public static void CreateCalculators()
        {
            BOMCalculators = new Dictionary<MaterialType, List<BOMInfo>>();

            List<BOMInfo> productBomList = new List<BOMInfo>();
            List<BOMInfo> subassemblyList = new List<BOMInfo>();

            if (UserInfo.Current.Enterprise == "INTERFLEX")
            {
                productBomList.Add(new BOMInfo("BOM_P_0000001", MaterialType.Product, 365, false, 3, true, 392, false, 1, true, null, false, "(v1 + v2) / 1000 / v3 / v4"));
                productBomList.Add(new BOMInfo("BOM_P_0000002", MaterialType.Product, 365, false, 3, true, 392, false, 1, true, null, false, "(v1 + v2) / 1000 / v3 / v4"));
                productBomList.Add(new BOMInfo("BOM_P_0000003", MaterialType.Product, 365, false, 3, true, 392, false, 1, true, null, false, "(v1 + v2) / 1000 / v3 / v4"));
                productBomList.Add(new BOMInfo("BOM_P_0000004", MaterialType.Product, 365, false, 0, true, 392, false, 1, true, null, false, "(v1 + v2) / 1000 / v3 / v4"));
                productBomList.Add(new BOMInfo("BOM_P_0000005", MaterialType.Product, 1, true, null, false, null, false, null, false, 0, true, "v1 / v5"));
                productBomList.Add(new BOMInfo("BOM_P_0000006", MaterialType.Product, null, false, null, false, null, false, null, false, null, false, ""));
                productBomList.Add(new BOMInfo("BOM_P_0000007", MaterialType.Product, 1, true, null, false, 392, false, null, false, null, false, " v1 / v3"));
                productBomList.Add(new BOMInfo("BOM_P_0000008", MaterialType.Product, 1, true, null, false, null, false, null, false, 0, true, " v1 / v5"));
                productBomList.Add(new BOMInfo("BOM_P_0000009", MaterialType.Product, 1, true, null, false, 392, false, null, false, null, false, " v1 / v3"));
                productBomList.Add(new BOMInfo("BOM_P_0000010", MaterialType.Product, 1, true, null, false, null, false, null, false, 0, true, " v1 / v5"));
                productBomList.Add(new BOMInfo("BOM_P_0000011", MaterialType.Product, 1, true, null, false, 392, false, null, false, null, false, " v1 / v3"));
                productBomList.Add(new BOMInfo("BOM_P_0000012", MaterialType.Product, 1, true, null, false, 392, false, null, false, null, false, " v1 / v3"));
                productBomList.Add(new BOMInfo("BOM_P_0000013", MaterialType.Product, 0, true, null, false, 392, false, null, false, null, false, " v1 / v3"));
                productBomList.Add(new BOMInfo("BOM_P_0000014", MaterialType.Product, 0, true, null, false, 392, false, null, false, null, false, " v1 / v3"));
                productBomList.Add(new BOMInfo("BOM_P_0000015", MaterialType.Product, null, false, null, false, null, false, null, false, null, false, "보류"));
                productBomList.Add(new BOMInfo("BOM_P_0000016", MaterialType.Product, 0, true, null, false, 392, false, null, false, null, false, "v1 / v3"));
                productBomList.Add(new BOMInfo("BOM_P_0000017", MaterialType.Product, 0, true, null, false, 392, false, null, false, null, false, "v1 / v3"));
                productBomList.Add(new BOMInfo("BOM_P_0000018", MaterialType.Product, 0, true, null, false, 392, false, null, false, null, false, "v1 / v3"));
                productBomList.Add(new BOMInfo("BOM_P_0000019", MaterialType.Product, 7000, false, null, false, 392, false, null, false, 0.03, false, "보류"));
                productBomList.Add(new BOMInfo("BOM_P_0000020", MaterialType.Product, 7000, false, null, false, 392, false, null, false, 0.1, false, "보류"));
                productBomList.Add(new BOMInfo("BOM_P_0000021", MaterialType.Product, 7000, false, null, false, 392, false, null, false, 0.5, false, "보류"));
                productBomList.Add(new BOMInfo("BOM_P_0000022", MaterialType.Product, 0, true, null, false, 392, false, null, false, null, false, "v1 / v3"));
                productBomList.Add(new BOMInfo("BOM_P_0000023", MaterialType.Product, 0, true, null, false, 392, false, null, false, null, false, "v1 / v3"));
                productBomList.Add(new BOMInfo("BOM_P_0000024", MaterialType.Product, 1, true, null, false, 392, false, null, false, null, false, "v1"));

                BOMCalculators.Add(MaterialType.Product, productBomList);

                subassemblyList.Add(new BOMInfo("BOM_SA_0000001", MaterialType.SubAssembly, 365, false, 4, true, 1, true, null, false, null, false, "(v1 + v2) / 1000 / v3"));
                subassemblyList.Add(new BOMInfo("BOM_SA_0000002", MaterialType.SubAssembly, 365, false, 4, true, 1, true, null, false, null, false, "(v1 + v2) / 1000 / v3"));
                subassemblyList.Add(new BOMInfo("BOM_SA_0000003", MaterialType.SubAssembly, 1, true, null, false, null, false, 0, true, null, false, "v1 / v4"));
                subassemblyList.Add(new BOMInfo("BOM_SA_0000004", MaterialType.SubAssembly, 365, false, 5, true, 1, true, null, false, null, false, "(v1 + v2) / 1000 / v3"));
                subassemblyList.Add(new BOMInfo("BOM_SA_0000005", MaterialType.SubAssembly, 0, true, 5, true, 1, true, null, false, null, false, "(v1 + v2) / 1000 / v3"));
                subassemblyList.Add(new BOMInfo("BOM_SA_0000006", MaterialType.SubAssembly, 0, true, 5, true, 1, true, null, false, null, false, "(v1 + v2) / 1000 / v3"));
                subassemblyList.Add(new BOMInfo("BOM_SA_0000007", MaterialType.SubAssembly, 1, true, null, false, null, false, null, false, null, false, "v1"));
                subassemblyList.Add(new BOMInfo("BOM_SA_0000008", MaterialType.SubAssembly, 365, false, 4, true, 1, true, null, false, null, false, "(v1 + v2) / 1000 / v3"));
                subassemblyList.Add(new BOMInfo("BOM_SA_0000009", MaterialType.SubAssembly, 365, false, 7, true, 1, true, null, false, null, false, "(v1 + v2) / 1000 / v3"));
                subassemblyList.Add(new BOMInfo("BOM_SA_0000010", MaterialType.SubAssembly, 0, true, 3, true, 2, true, 0, true, 0, true, "(v1 + v2) / 1000 / v3 / v4 / v5"));
                subassemblyList.Add(new BOMInfo("BOM_SA_0000011", MaterialType.SubAssembly, 0, true, 3, true, 2, true, 0, true, 0, true, "(v1 + v2) / 1000 / v3 / v4 / v5"));
                subassemblyList.Add(new BOMInfo("BOM_SA_0000012", MaterialType.SubAssembly, null, false, null, false, null, false, null, false, null, false, "보류"));
                subassemblyList.Add(new BOMInfo("BOM_SA_0000013", MaterialType.SubAssembly, 0, true, 4, true, null, false, 0, true, null, false, "(v1 + v2) / 1000 / v4"));
                subassemblyList.Add(new BOMInfo("BOM_SA_0000014", MaterialType.SubAssembly, 0, true, 3, true, null, false, null, false, null, false, "(v1 + v2) / 1000"));
                subassemblyList.Add(new BOMInfo("BOM_SA_0000015", MaterialType.SubAssembly, 0, true, 3, true, null, false, 0, true, null, false, "(v1 + v2) / 1000 / v4"));
                subassemblyList.Add(new BOMInfo("BOM_SA_0000016", MaterialType.SubAssembly, 0.00257, false, null, false, 0, true, null, false, null, false, "v1 / v3"));
                subassemblyList.Add(new BOMInfo("BOM_SA_0000017", MaterialType.SubAssembly, 1, true, null, false, 0, true, 0, true, null, false, "v1 / v3 / v4"));
                subassemblyList.Add(new BOMInfo("BOM_SA_0000018", MaterialType.SubAssembly, 0, true, null, false, 2, true, 0, true, null, false, "v1 / 1000 / v4 / v3"));
                subassemblyList.Add(new BOMInfo("BOM_SA_0000019", MaterialType.SubAssembly, 0, true, 3, true, 0, true, null, false, null, false, "(v1 + v2) / 1000 / v3"));
                subassemblyList.Add(new BOMInfo("BOM_SA_0000020", MaterialType.SubAssembly, 365, false, null, false, 0, true, 0, true, null, false, "v1 / 1000 / v4 / v3"));

                BOMCalculators.Add(MaterialType.SubAssembly, subassemblyList);
            }
            else if (UserInfo.Current.Enterprise == "YOUNGPOONG")
            {
                productBomList.Add(new BOMInfo("BOM_P_0000001", MaterialType.Product, null, true, null, true, null, false, null, false, null, false, "(v1 / 1000) *  (v2 / 1000)"));

                BOMCalculators.Add(MaterialType.Product, productBomList);

                subassemblyList.Add(new BOMInfo("BOM_SA_0000001", MaterialType.SubAssembly, null, true, null, true, null, false, null, false, null, false, "(v1 / 1000) *  (v2 / 1000)"));

                BOMCalculators.Add(MaterialType.SubAssembly, subassemblyList);
            }
        }
    }

    public class BOMInfo
    {
        private string id;

        private MaterialType materialType = StandardInfo.MaterialType.Product;

        private bool isEditVariable1;

        private double? defaultVariable1;

        private bool isEditVariable2;

        private double? defaultVariable2;

        private bool isEditVariable3;

        private double? defaultVariable3;

        private bool isEditVariable4;

        private double? defaultVariable4;

        private bool isEditVariable5;

        private double? defaultVariable5;

        private string formula;

        public MaterialType MaterialType { get => materialType; set => materialType = value; }
        public bool IsEditVariable1 { get => isEditVariable1; set => isEditVariable1 = value; }
        public double? DefaultVariable1 { get => defaultVariable1;
            set => defaultVariable1 = value; }
        public bool IsEditVariable2 { get => isEditVariable2; set => isEditVariable2 = value; }
        public double? DefaultVariable2 { get => defaultVariable2; set => defaultVariable2 = value; }
        public bool IsEditVariable3 { get => isEditVariable3;
            set => isEditVariable3 = value; }
        public double? DefaultVariable3 { get => defaultVariable3;
            set => defaultVariable3 = value; }
        public bool IsEditVariable4 { get => isEditVariable4; set => isEditVariable4 = value; }
        public double? DefaultVariable4 { get => defaultVariable4; set => defaultVariable4 = value; }
        public bool IsEditVariable5 { get => isEditVariable5; set => isEditVariable5 = value; }
        public double? DefaultVariable5 { get => defaultVariable5; set => defaultVariable5 = value; }
        public string Formula { get => formula; set => formula = value; }
        public string Id { get => id; set => id = value; }

        public BOMInfo(string id, MaterialType materialType,
            double? defaultVariable1, bool isEditVariable1,
            double? defaultVariable2, bool isEditVariable2, 
            double? defaultVariable3, bool isEditVariable3, 
            double? defaultVariable4, bool isEditVariable4,
            double? defaultVariable5, bool isEditVariable5, string formula)
        {
            this.id = id;
            this.materialType = materialType;
            this.defaultVariable1 = defaultVariable1;
            this.isEditVariable1 = isEditVariable1;
            this.defaultVariable2 = defaultVariable2;
            this.isEditVariable2 = isEditVariable2;
            this.defaultVariable3 = defaultVariable3;
            this.isEditVariable3 = isEditVariable3;
            this.defaultVariable4 = defaultVariable4;
            this.isEditVariable4 = isEditVariable4;
            this.defaultVariable5 = defaultVariable5;
            this.isEditVariable5 = isEditVariable5;
            this.formula = formula;


        }

        public BOMInfo()
        {

        }
    }
}