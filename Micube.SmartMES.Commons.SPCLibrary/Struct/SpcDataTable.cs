#region using

using System.Data;

#endregion

namespace Micube.SmartMES.Commons.SPCLibrary
{
    /// <summary>
    /// Spc 통계 분석용 Table 정의.
    /// </summary>
    public class SpcDataTable
    {
        /// <summary>
        /// SPC SubGroup 통계 테이블.
        /// </summary>
        public DataTable tableSubGroupSta;

        /// <summary>
        /// SPC Navigator 테이블.
        /// </summary>
        public DataTable tableNavigator;

        /// <summary>
        /// 초기화
        /// </summary>
        /// <returns></returns>
        public static SpcDataTable Create() => new SpcDataTable
        {
            tableSubGroupSta = SpcDataTable.CreatetableSubGroupSta("spcSubGroupFirst"),
            tableNavigator = SpcDataTable.CreatetableNavigator("spcNavigatorFirst"),
            
        };


        /// <summary>
        /// Class 복사.
        /// </summary>
        /// <returns></returns>
        public SpcDataTable CopyDeep()
        {
            SpcDataTable cs = SpcDataTable.Create();
            foreach (DataRow rowdata in this.tableSubGroupSta.Rows)
            {
                cs.tableSubGroupSta.ImportRow(rowdata);
            }

            foreach (DataRow rowdata in this.tableNavigator.Rows)
            {
                cs.tableNavigator.ImportRow(rowdata);
            }

            return cs;
        }

        /// <summary>
        /// SPC Subgroup 통계 Table 초기화
        /// </summary>
        /// <returns></returns>
        public static DataTable CreatetableSubGroupSta(string tableName = "spcSubGroup01")
        {
            DataTable c = new DataTable
            {
                TableName = tableName
            };

            c.Columns.Add("TEMPID", typeof(long));
            c.Columns.Add("PAGEID", typeof(long));
            c.Columns.Add("GROUPID", typeof(long));
            c.Columns.Add("SUBGROUP", typeof(string));
            c.Columns.Add("XBAR", typeof(double));
            c.Columns.Add("RBAR", typeof(double));
            c.Columns.Add("GROUPNN", typeof(double));

            return c;
        }

        /// <summary>
        /// SPC Table Navigator 초기화
        /// </summary>
        /// <returns></returns>
        public static DataTable CreatetableNavigator(string tableName = "spcNavigator01")
        {
            DataTable c = new DataTable
            {
                TableName = tableName
            };

            c.Columns.Add("TEMPID", typeof(long));
            c.Columns.Add("SEQIDSTART", typeof(long));
            c.Columns.Add("SEQIDEND", typeof(long));
            c.Columns.Add("GROUPIDSTART", typeof(long));
            c.Columns.Add("GROUPIDEND", typeof(long));
            c.Columns.Add("SUBGROUPSTART", typeof(string));
            c.Columns.Add("SUBGROUPEND", typeof(string));
            c.Columns.Add("XBARSTART", typeof(double));
            c.Columns.Add("XBAREND", typeof(double));
            c.Columns.Add("RBARSTART", typeof(double));
            c.Columns.Add("RBAREND", typeof(double));
            c.Columns.Add("GROUPNN", typeof(double));

            return c;
        }

        /// <summary>
        /// SPC Table SubGroup별 Spec 값 초기화 8/2
        /// </summary>
        /// <returns></returns>
        public static DataTable CreateTableSubgroupSpec(string tableName = "spcSubgroupSpec")
        {
            DataTable c = new DataTable
            {
                TableName = tableName
            };

            c.Columns.Add("TEMPID", typeof(long));
            c.Columns.Add("SUBGROUP", typeof(string));
            c.Columns.Add("CTYPE", typeof(string));
            c.Columns.Add("USL", typeof(double));
            c.Columns.Add("CSL", typeof(double));
            c.Columns.Add("LSL", typeof(double));
            c.Columns.Add("UCL", typeof(double));
            c.Columns.Add("CCL", typeof(double));
            c.Columns.Add("LCL", typeof(double));
            c.Columns.Add("UOL", typeof(double));
            c.Columns.Add("LOL", typeof(double));

            return c;
        }

        /// <summary>
        /// SPC Page 필드 설정.
        /// </summary>
        public class SpcPageField
        {
            public SpcGroupItem Start;
            public SpcGroupItem End;
            public SpcGroupItem Temp;

            /// <summary>
            /// 초기화
            /// </summary>
            /// <returns></returns>
            public static SpcPageField Create() => new SpcPageField
            {
                Start = SpcGroupItem.Create,
                End = SpcGroupItem.Create,
                Temp = SpcGroupItem.Create
            };
        }

        /// <summary>
        /// SPC 통계 Group Item 설정.
        /// </summary>
        public class SpcGroupItem
        {
            public long nSEQID;
            public long nGROUPID;
            public string SUBGROUP;
            public double nXBAR;
            public double nRBAR;

            /// <summary>
            /// 초기화
            /// </summary>
            /// <returns></returns>
            public static SpcGroupItem Create => new SpcGroupItem();
        }

        /// <summary>
        /// Chart Point Raw Data테이블 구조
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static DataTable CreateDataTableChartPointRawData(string tableName = "tableChartPointRawData")
        {
            DataTable dt = new DataTable();
            dt.TableName = tableName;
            //Filed
            dt.Columns.Add("GROUPID", typeof(long));
            dt.Columns.Add("SAMPLEID", typeof(long));
            dt.Columns.Add("SUBGROUP", typeof(string));
            dt.Columns.Add("SUBGROUPNAME", typeof(string));
            dt.Columns.Add("SAMPLING", typeof(string));
            dt.Columns.Add("SAMPLINGNAME", typeof(string));
            dt.Columns.Add("LOTID", typeof(string));
            dt.Columns.Add("NVALUE", typeof(double));
            return dt;
        }

    }
}
