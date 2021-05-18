using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Micube.SmartMES.Commons.Controls
{
    public class LabelInfo
    {
        private string labelID;
        private string labelName;
        private string labelType;
        private DataTable labelDataTable;
        private XmlDocument xmlBarcodeScript;
        private string zplBarcodeScript;

        public string LabelID { get => labelID; set => labelID = value; }
        public string LabelName { get => labelName; set => labelName = value; }
        public string LabelType { get => labelType; set => labelType = value; }
        public DataTable LabelDataTable { get => labelDataTable; set => labelDataTable = value; }
        public XmlDocument XmlBarcodeScript { get => xmlBarcodeScript; set => xmlBarcodeScript = value; }
        public string ZplBarcodeScript { get => zplBarcodeScript; set => zplBarcodeScript = value; }
        public string RdParameters { get; set; }
        public string RdFileName { get; set; }
        public string BarcodeOption { get; set; }   // SF_IDCLASS의 ID
        public string QueryId { get; set; }
        public string QueryVersion { get; set; }
        public string BoxNo { get; set; }
        public bool IsYApply { get; set; }
        public string BarcodeType { get; set; }
    }
}
