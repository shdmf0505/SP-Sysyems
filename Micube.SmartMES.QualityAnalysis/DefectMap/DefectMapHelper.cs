#region using

using Micube.SmartMES.Commons;

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;

using DevExpress.XtraGrid;
using DevExpress.XtraCharts;
using DevExpress.XtraGrid.Views.Grid;

using System;
using System.IO;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text.RegularExpressions;

#endregion

namespace Micube.SmartMES.QualityAnalysis
{
    #region enum

    /// <summary>
    /// 수율 화면 Type - 수율 화면을 공통으로 사용하기 위한 Enum
    /// 상수 string 으로 쓰기 위해 개발룰에 따르지 않음
    /// LAYERID : Layer 별 수율 현황
    /// EQUIPMENTID : 공정/설비 별 수율 현황
    /// RECIPE : 작업조건(Recipe) 별 수율 현황
    /// </summary>
    public enum RateGroupType { LAYERID, EQUIPMENTID, RECIPEID }

    /// <summary>
    /// AOI Nail View 기준
    /// 상수 string 으로 쓰기 위해 개발룰에 따르지 않음
    /// </summary>
    public enum ComboType { LOTID, PANELID, LAYERID }

    /// <summary>
    /// AOI DIagram Type
    /// AOIMODE_MAIN : Marge 되는 메인 Defect Map
    /// AOIMODE_NAIL : Panel/Layer 별 낱게 Defect Map
    /// AOIMODE_POPUPNAIL : Popup에 표시될 때 보여지는 Defect Map
    /// </summary>
    public enum AOIMode { AOIMODE_MAIN, AOIMODE_NAIL, AOIMODE_POPUPNAIL }

    /// <summary>
    /// BBT Diagram Type
    /// BBTMODE_MAIN : Marge 되는 메인 Defect Map
    /// BBTMODE_NAIL : Panel/Layer 별 낱게 Defect Map
    /// BBTMODE_LOTNAIL : 품목 별 Defect Map 조회시 Nail Map type
    /// </summary>
    public enum BBTMode { BBTMODE_MAIN, BBTMODE_NAIL, BBTMODE_LOTNAIL }

    /// <summary>
    /// Equipment Type
    /// AOI / BBT / HOLE 구분 타입
    /// </summary>
    public enum EquipmentType { EQUIPMENTTYPE_AOI, EQUIPMENTTYPE_BBT, EQUIPMENTTYPE_HOLE }

    /// <summary>
    /// 수율화면에 View Type
    /// </summary>
    public enum SubViewType { SUBVIEWTYPE_PRODUCT, SUBVIEWTYPE_LOT }

    #endregion

    #region so

    /// <summary>
    /// AOI에 사용되는 Data SO
    /// </summary>
    public class AOISheet
    {
        public string EQUIPMENTID { get; set; }
        public string LOTID { get; set; }
        public string MODELID { get; set; }
        public string LAYERID { get; set; }
        public string PANELID { get; set; }
        public int DEFECTNO { get; set; } // 조회된 Defect들 RowNum Filering 하기 위한 No
        public int PCSCOUNT { get; set; }
        public int INSPECTIONQTY { get; set; }
        public int ARRAYX { get; set; }
        public int ARRAYY { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public double AOIDEFECTCODE { get; set; }
        public double VRSDEFECTCODE { get; set; }
        public string DEFECTTYPE { get; set; }
        public string IMAGE { get; set; }
        public string GROUPCODE { get; set; }
        public string GROUPNAME { get; set; }
        public string GROUPCOLOR { get; set; }
        public string SUBDEFECTCODE { get; set; }
        public string SUBDEFECTNAME { get; set; }
        public DateTime EVENTTIME { get; set; }
    }

    /// <summary>
    /// BBT에 사용되는 Data SO
    /// </summary>
    public class BBTSheet
    {
        public string EQUIPMENTID { get; set; }
        public string LOTID { get; set; }
        public string MODELID { get; set; }
        public string MODELIDREV { get; set; }
        public string PANELID { get; set; }
        public int DEFECTNO { get; set; }
        public int PCSCOUNT { get; set; }
        public int INSPECTIONQTY { get; set; }
        public int ARRAYX { get; set; }
        public int ARRAYY { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public string DEFECTCODE { get; set; }
        public string DEFECTNAME { get; set; }
        public string DEFECTTYPE { get; set; }
        public string COLOR { get; set; }
        public DateTime EVENTTIME { get; set; }
    }

    /// <summary>
    /// 검사공정에 따른 Data SO
    /// </summary>
    public class InspectionRateSheet
    {
        public string EQUIPMENTTYPE { get; set; }
        public string PRODUCTDEFID { get; set; }
        public string PRODUCTDEFVERSION { get; set; }
        public string LOTID { get; set; }
        public int INSPECTIONQTY { get; set; }
        public int REPAIRTARGETQTY { get; set; }
        public int REPAIRRESULTQTY { get; set; }
        public string GROUPCODE { get; set; }
        public string GROUPNAME { get; set; }
        public string GROUPCOLOR { get; set; }
        public string SUBDEFECTCODE { get; set; }
        public string SUBDEFECTNAME { get; set; }
        public int DEFECTCOUNT { get; set; }
        public int SUMQTY { get; set; }
        public DateTime EVENTTIME { get; set; }
    }

    /// <summary>
    /// AOI Defect Map SO
    /// </summary>
    public class AOIDefectAnalysis
    {
        public string LOTID { get; set; }
        public double SUMRATEBYLOT { get; set; }
        public string DEFECTGROUP { get; set; }
        public int DEFECTGROUPCOUNT { get; set; }
        public double DEFECTGROUPRATE { get; set; }
        public string DEFECTCODE { get; set; }
        public int DEFECTCOUNT { get; set; }
        public double DEFECTRATE { get; set; }
        public string COLOR { get; set; }
        public int INSPECTIONQTY { get; set; }
        public DateTime EVENTTIME { get; set; }
    }

    /// <summary>
    /// BBT Defect Map SO
    /// </summary>
    public class BBTDefectAnalysis
    {
        public string DEFECTCODE { get; set; }
        public int DEFECTCOUNT { get; set; }
        public double DEFECTRATE { get; set; }
        public string COLOR { get; set; }
        public int INSPECTIONQTY { get; set; }
    }

    /// <summary>
    /// 검사공정 최종 불량 SO
    /// </summary>
    public class InspectionRateAnalysis
    {
        public string PRODUCTDEFVERSION { get; set; }
        public string LOTID { get; set; }
        public double AOIDEFECTRATE { get; set; }
        public double BBTDEFECTRATE { get; set; }
        public double HOLEDEFECTRATE { get; set; }
        public double AOIQTY { get; set; }
        public double BBTQTY { get; set; }
        public double HOLEQTY { get; set; }
        public double AOICOUNT { get; set; }
        public double BBTCOUNT { get; set; }
        public double HOLECOUNT { get; set; }
    }

    /// <summary>
    /// AOI 분석 전/후 비교 SO
    /// </summary>
    public class AOIDefectRateRepair
    {
        public string PRODUCTDEFVERSION { get; set; }
        public string LOTID { get; set; }
        public double INSPECTIONQTY { get; set; }
        public int BEFOREDEFECTCNT { get; set; }
        public double BEFOREDEFECTRATE { get; set; }
        public int ANALYSISTARGET { get; set; }
        public int ANALYSISRESULT { get; set; }
        public int AFTERDEFECTCNT { get; set; }
        public double AFTERDEFECTRATE { get; set; }
        public double ANALYSISRATE { get; set; }
    }

    /// <summary>
    /// Check Sheet AOI Daily
    /// </summary>
    public class DefectManualTable
    {
        public string INSPECTDATE { get; set; }                 // 01 INSPECTDATE
        public string PRODUCTREVISIONINPUT { get; set; }        // 02 PRODUCTREVISIONINPUT
        public string MODELPRODUCT { get; set; }                // 03 MODELPRODUCT
        public string INSPECTIONDEGREEPROCESS { get; set; }     // 00 
        public string LOTID { get; set; }                       // 04 LOTID
        public string INSPECTIONWORKAREA { get; set; }          // 05 INSPECTIONWORKAREA
        public string SCANSIDE { get; set; }                    // 00 
        public string WORKSTARTPCSQTY { get; set; }             // 07 WORKSTARTPCSQTY
        public string WORKSTARTPANELQTY { get; set; }           // 06 WORKSTARTPANELQTY
        public string USEDFACTOR { get; set; }                  // 08 USEDFACTOR
        public string FRONTLAYER { get; set; }                  // 00 
        public string BACKLAYER { get; set; }                   // 00 
        public string EQUIPMENTNAME { get; set; }               // 11 EQUIPMENTNAME
        public string WORKER { get; set; }                      // 12 WORKER
        public string WORKSTARTTIME { get; set; }               // 13 WORKSTARTTIME
        public string WORKENDTIME { get; set; }                 // 14 WORKENDTIME
        public string DURABLEDEFID { get; set; }                // 15 DURABLEDEFID
        public string DURABLEDEFVERSION { get; set; }           // 16 DURABLEDEFVERSION
        public string DURABLELOTID { get; set; }                // 17 DURABLELOTID              //2020-12-21 (109) 그리드 헤더명 변경(치공구 ID => Tool No.)(DURABLEDEFID => DURABLELOTID)
        public string PRODUCTDEFVERSION { get; set; }          // 18 PRODUCTDEFVERSION2        //2021-01-08 (110) 내부 Rev 추가
        public string ANALYSISVENDOR { get; set; }              // 리페어 작업장
    }

    #endregion

    /// <summary>
    /// QualityAnalysis Defect Map에서 사용되는 공통 함수
    /// </summary>
    public class DefectMapHelper
    {
        #region Global Variable

        /// <summary>
        /// AOI Diagram None Shape Color
        /// </summary>
        private static readonly Color NONE_COLOR = Color.LightGray;

        #endregion

        /// <summary>
        /// Equipment Type에 따라 불량Data를 분류한다
        /// </summary>
        /// <param name="dt">Row Data</param>
        /// <param name="type">Equipment Type</param>
        /// <returns></returns>
        public static DataTable GetDefectDataOfEquipmentType(DataTable dt, EquipmentType type)
        {
            string sEqpType = type.Equals(EquipmentType.EQUIPMENTTYPE_AOI) ? "AOI" :
                              type.Equals(EquipmentType.EQUIPMENTTYPE_BBT) ? "BBT" : "HOLE";

            var list = dt.AsEnumerable().Where(x => x.Field<string>("EQUIPMENTTYPE").Equals(sEqpType));

            return list.Count().Equals(0) ? null : list.CopyToDataTable();
        }

        /// <summary>
        /// Eqpuipment Type에 따라 불량분석한다
        /// </summary>s
        /// <param name="dt">Row Data</param>
        /// <param name="type">Equipment Type</param>
        /// <returns></returns>
        public static DataTable GetDefectAnalysisOfEquipmentType(DataTable dt, EquipmentType type)
            => type.Equals(EquipmentType.EQUIPMENTTYPE_AOI) ? GetDefectAnalysisByAOI(dt) :
               type.Equals(EquipmentType.EQUIPMENTTYPE_BBT) ? GetDefectAnalysisByBBT(dt) :
                                                              GetDefectAnalysisByHOLE(dt);

        /// <summary>
        /// 분석대상에 대한 처리
        /// 회사별로 로직이 다름
        /// </summary>
        /// <param name="dt">raw Data</param>
        /// <returns></returns>
        public static DataTable GetRepairAnalysisByEnterpriseid(DataTable dt)
        {
            //! AOI만 처리 2020/02/20 (양사 확인)
            if (!dt.Rows[0]["EQUIPMENTTYPE"].Equals("AOI"))
            {
                return dt;
            }

            return UserInfo.Current.Enterprise.Equals("INTERFLEX") ? RepairAnalsysByIFC(dt) : RepairAnalysisByYPE(dt);
        }

        /// <summary>
        /// AOI별 불량분석 한다
        /// </summary>
        /// <param name="dt">Row Data</param>
        /// <returns></returns>
        public static DataTable GetDefectAnalysisByAOI(DataTable dt)
        {
            if (IsNull(dt))
            {
                return null;
            }

            List<AOISheet> sheet = ConversionToAOISheet(dt);

            List<AOIDefectAnalysis> groupList = sheet.GroupBy(m => new { m.LOTID, m.GROUPNAME })
                                                     .Select(grp => new AOIDefectAnalysis
                                                     {
                                                         DEFECTGROUP = grp.Key.GROUPNAME,
                                                         DEFECTGROUPCOUNT = grp.Count(),
                                                         INSPECTIONQTY = grp.First().INSPECTIONQTY,
                                                         COLOR = grp.First().GROUPCOLOR
                                                     })
                                                     .GroupBy(g => g.DEFECTGROUP)
                                                     .Select(grp => new AOIDefectAnalysis
                                                     {
                                                         DEFECTGROUP = grp.Key,
                                                         DEFECTGROUPCOUNT = grp.Sum(x => x.DEFECTGROUPCOUNT),
                                                         INSPECTIONQTY = grp.Sum(x => x.INSPECTIONQTY),
                                                         COLOR = grp.First().COLOR
                                                     }).ToList();

            List<AOIDefectAnalysis> defectList = sheet.GroupBy(m => new { m.LOTID, m.GROUPNAME, m.SUBDEFECTNAME })
                                                      .Select(grp => new AOIDefectAnalysis
                                                      {
                                                          LOTID = grp.Key.LOTID,
                                                          DEFECTGROUP = grp.Key.GROUPNAME,
                                                          DEFECTCODE = grp.Key.SUBDEFECTNAME,
                                                          DEFECTCOUNT = grp.Count()
                                                      })
                                                      .GroupBy(d => new { d.DEFECTGROUP, d.DEFECTCODE })
                                                      .Select(grp => new AOIDefectAnalysis
                                                      {
                                                          DEFECTGROUP = grp.Key.DEFECTGROUP,
                                                          DEFECTCODE = grp.Key.DEFECTCODE,
                                                          DEFECTCOUNT = grp.Sum(x => x.DEFECTCOUNT)
                                                      }).ToList();

            if (groupList.Count().Equals(0))
            {
                return null;
            }

            int nMaxInspectionQty = groupList.Max(x => x.INSPECTIONQTY);

            List<AOIDefectAnalysis> list = (from main in groupList
                                            join sub in defectList
                                              on main.DEFECTGROUP equals sub.DEFECTGROUP
                                            orderby main.DEFECTGROUP, sub.DEFECTCODE
                                            select new AOIDefectAnalysis
                                            {
                                                DEFECTGROUP = main.DEFECTGROUP,
                                                DEFECTGROUPCOUNT = main.DEFECTGROUPCOUNT,
                                                DEFECTGROUPRATE = SetDefectRate(main.DEFECTGROUPCOUNT, nMaxInspectionQty),
                                                DEFECTCODE = sub.DEFECTCODE,
                                                DEFECTCOUNT = sub.DEFECTCOUNT,
                                                DEFECTRATE = SetDefectRate(sub.DEFECTCOUNT, nMaxInspectionQty),
                                                INSPECTIONQTY = nMaxInspectionQty,
                                                COLOR = main.COLOR
                                            }).ToList();

            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(AOIDefectAnalysis));
            DataTable table = new DataTable();

            foreach (PropertyDescriptor prop in props)
            {
                table.Columns.Add(prop.Name, prop.PropertyType);
            }

            object[] values = new object[props.Count];
            list.ForEach(item =>
            {
                //values[0] = props["LOTID"].GetValue(item);
                //values[1] = props["SUMRATEBYLOT"].GetValue(item);
                values[2] = props["DEFECTGROUP"].GetValue(item);
                values[3] = props["DEFECTGROUPCOUNT"].GetValue(item);
                values[4] = props["DEFECTGROUPRATE"].GetValue(item);
                values[5] = props["DEFECTCODE"].GetValue(item);
                values[6] = props["DEFECTCOUNT"].GetValue(item);
                values[7] = props["DEFECTRATE"].GetValue(item);
                values[8] = props["COLOR"].GetValue(item);
                values[9] = props["INSPECTIONQTY"].GetValue(item);

                table.Rows.Add(values);
            });

            return table;
        }

        /// <summary>
        /// BBT별 불량분석 한다
        /// </summary>
        /// <param name="dt">Row Data</param>
        /// <returns></returns>
        public static DataTable GetDefectAnalysisByBBT(DataTable dt)
        {
            if (IsNull(dt))
            {
                return null;
            }

            List<BBTDefectAnalysis> list = ConversionToBBTSheet(dt).GroupBy(m => new { m.DEFECTNAME, m.INSPECTIONQTY, m.COLOR })
                                                                   .OrderBy(grp => grp.Key.DEFECTNAME)
                                                                   .Select(grp => new BBTDefectAnalysis
                                                                   {
                                                                       DEFECTCODE = grp.Key.DEFECTNAME,
                                                                       DEFECTCOUNT = grp.Count(),
                                                                       DEFECTRATE = SetDefectRate(grp.Count(), grp.Key.INSPECTIONQTY),
                                                                       COLOR = grp.Key.COLOR,
                                                                       INSPECTIONQTY = grp.Key.INSPECTIONQTY
                                                                   }).ToList();

            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(BBTDefectAnalysis));
            DataTable table = new DataTable();

            foreach (PropertyDescriptor prop in props)
            {
                table.Columns.Add(prop.Name, prop.PropertyType);
            }

            object[] values = new object[props.Count];

            list.ForEach(item =>
            {
                values[0] = props["DEFECTCODE"].GetValue(item);
                values[1] = props["DEFECTCOUNT"].GetValue(item);
                values[2] = props["DEFECTRATE"].GetValue(item);
                values[3] = props["COLOR"].GetValue(item);
                values[4] = props["INSPECTIONQTY"].GetValue(item);

                table.Rows.Add(values);
            });

            return table;
        }

        /// <summary>
        /// Hole별 불량분석 한다
        /// BBTDefectAnalysis 구조체를 사용해도 무관하여 사용함
        /// </summary>
        /// <param name="dt">Row Data</param>
        /// <returns></returns>
        public static DataTable GetDefectAnalysisByHOLE(DataTable dt)
        {
            if (IsNull(dt))
            {
                return null;
            }

            List<BBTDefectAnalysis> list = ConversionToAOISheet(dt).GroupBy(m => new { m.SUBDEFECTNAME, m.INSPECTIONQTY, m.GROUPCOLOR })
                                                                   .OrderBy(grp => grp.Key.SUBDEFECTNAME)
                                                                   .Select(grp => new BBTDefectAnalysis
                                                                   {
                                                                       DEFECTCODE = grp.Key.SUBDEFECTNAME,
                                                                       DEFECTCOUNT = grp.Count(),
                                                                       DEFECTRATE = SetDefectRate(grp.Count(), grp.Key.INSPECTIONQTY),
                                                                       COLOR = grp.Key.GROUPCOLOR,
                                                                       INSPECTIONQTY = grp.Key.INSPECTIONQTY
                                                                   }).ToList();

            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(BBTDefectAnalysis));
            DataTable table = new DataTable();

            foreach (PropertyDescriptor prop in props)
            {
                table.Columns.Add(prop.Name, prop.PropertyType);
            }

            object[] values = new object[props.Count];

            list.ForEach(item =>
            {
                values[0] = props["DEFECTCODE"].GetValue(item);
                values[1] = props["DEFECTCOUNT"].GetValue(item);
                values[2] = props["DEFECTRATE"].GetValue(item);
                values[3] = props["COLOR"].GetValue(item);
                values[4] = props["INSPECTIONQTY"].GetValue(item);

                table.Rows.Add(values);
            });

            return table;
        }

        /// <summary>
        /// BBT Main Defect Map에 Cell ToolTip Data 생성
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DataTable GetBBTToolTipByMain(DataTable dt)
        {
            if (IsNull(dt))
            {
                return null;
            }

            var list = (from m in ConversionToBBTSheet(dt, false)
                        group m by new { m.X, m.Y, m.DEFECTNAME } into grp
                        orderby grp.Key.Y, grp.Key.X, grp.Key.DEFECTNAME
                        select new
                        {
                            grp.Key.X,
                            grp.Key.Y,
                            DEFECTCODE = grp.Key.DEFECTNAME,
                            DEFECTCOUNT = grp.Count()
                        }).ToList();

            DataTable table = new DataTable();

            for (int i = 0; i < dt.Rows[0].Field<int>("ARRAYX"); i++)
            {
                table.Columns.Add(Format.GetString(i), typeof(string));
            }

            DataRow row = table.NewRow();
            string total = string.Empty;
            int nYCheckPoint = 1;
            int nXCheckPoint = 1;

            if (table.Columns.Count == 1)
            {
                list.ForEach(dr =>
                {
                    if (nYCheckPoint != dr.Y)
                    {
                        row[nXCheckPoint - 1] = total;
                        nYCheckPoint = dr.Y;
                        table.Rows.Add(row);
                        row = table.NewRow();
                        total = string.Empty;
                    }

                    total += dr.DEFECTCODE + " : " + dr.DEFECTCOUNT + Environment.NewLine;

                    //if (dr.DEFECTCODE != "SKIP" && dr.DEFECTCODE != "PASS")
                    //{
                    //    total += dr.DEFECTCODE + " : " + dr.DEFECTCOUNT + Environment.NewLine;
                    //}
                });
            }
            else
            {
                list.ForEach(dr =>
                {
                    if (nXCheckPoint != dr.X)
                    {
                        if (table.Columns.Count < nXCheckPoint) // 2020.06.25-유석진-컬럼 추가 로직 추가
                        {
                            table.Columns.Add(Format.GetString(nXCheckPoint - 1), typeof(string));
                        }
                        row[nXCheckPoint - 1] = total;
                        nXCheckPoint = dr.X;
                        total = string.Empty;
                    }

                    total += dr.DEFECTCODE + " : " + dr.DEFECTCOUNT + Environment.NewLine;

                    //if (dr.DEFECTCODE != "SKIP" && dr.DEFECTCODE != "PASS")
                    //{
                    //    total += dr.DEFECTCODE + " : " + dr.DEFECTCOUNT + Environment.NewLine;
                    //}

                    if (nYCheckPoint != dr.Y)
                    {
                        nYCheckPoint = dr.Y;
                        table.Rows.Add(row);
                        row = table.NewRow();
                    }
                });
            }

            row[nXCheckPoint - 1] = total;
            table.Rows.Add(row);

            return table;
        }

        /// <summary>
        /// 불량률별 불량분석 한다
        /// </summary>
        /// <param name="dt">Row Data</param>
        /// <returns></returns>
        public static DataTable GetDefectAnalysisByLotRate(DataTable dt, EquipmentType type)
        {
            if (IsNull(dt))
            {
                return null;
            }

            List<InspectionRateSheet> sheet = ConversionToEquipSheet(dt, false);

            if (type.Equals(EquipmentType.EQUIPMENTTYPE_AOI))
            {
                int insepctionQty = sheet.GroupBy(x => new { x.LOTID, x.INSPECTIONQTY }).Sum(x => x.Key.INSPECTIONQTY);

                List<AOIDefectAnalysis> list = (from gr in (from m in sheet
                                                            group m by new { m.LOTID, m.GROUPNAME, m.SUBDEFECTNAME, m.GROUPCOLOR } into grp
                                                            select new AOIDefectAnalysis
                                                            {
                                                                LOTID = grp.Key.LOTID,
                                                                DEFECTGROUP = grp.Key.GROUPNAME,
                                                                DEFECTCODE = grp.Key.SUBDEFECTNAME,
                                                                DEFECTCOUNT = grp.Sum(x => x.DEFECTCOUNT),
                                                                DEFECTRATE = SetDefectRate(grp.Sum(x => x.DEFECTCOUNT), grp.FirstOrDefault().SUMQTY),
                                                                COLOR = grp.Key.GROUPCOLOR
                                                            })
                                                join de in (from m in sheet
                                                            group m by m.LOTID into grp
                                                            select new AOIDefectAnalysis
                                                            {
                                                                LOTID = grp.Key,
                                                                INSPECTIONQTY = insepctionQty,
                                                                //INSPECTIONQTY = grp.FirstOrDefault().SUMQTY,
                                                                SUMRATEBYLOT = SetDefectRate(grp.Sum(x => x.DEFECTCOUNT), grp.FirstOrDefault().SUMQTY),
                                                                EVENTTIME = grp.Max(x => x.EVENTTIME)
                                                            })
                                                            on gr.LOTID equals de.LOTID
                                                orderby de.EVENTTIME, gr.LOTID, gr.DEFECTGROUP
                                                select new AOIDefectAnalysis
                                                {
                                                    LOTID = gr.LOTID,
                                                    SUMRATEBYLOT = de.SUMRATEBYLOT,
                                                    DEFECTGROUP = gr.DEFECTGROUP,
                                                    DEFECTCODE = gr.DEFECTCODE,
                                                    DEFECTCOUNT = gr.DEFECTCOUNT,
                                                    DEFECTRATE = gr.DEFECTRATE,
                                                    COLOR = gr.COLOR,
                                                    EVENTTIME = de.EVENTTIME,
                                                    INSPECTIONQTY = de.INSPECTIONQTY
                                                }).ToList();

                PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(AOIDefectAnalysis));
                DataTable table = new DataTable();

                foreach (PropertyDescriptor prop in props)
                {
                    table.Columns.Add(prop.Name, prop.PropertyType);
                }

                object[] values = new object[props.Count];

                list.ForEach(item =>
                {
                    values[0] = props["LOTID"].GetValue(item);
                    values[1] = props["SUMRATEBYLOT"].GetValue(item);
                    values[2] = props["DEFECTGROUP"].GetValue(item);
                    //values[3] = props["DEFECTGROUPCOUNT"].GetValue(item);
                    //values[4] = props["DEFECTGROUPRATE"].GetValue(item);
                    values[5] = props["DEFECTCODE"].GetValue(item);
                    values[6] = props["DEFECTCOUNT"].GetValue(item);
                    values[7] = props["DEFECTRATE"].GetValue(item);
                    values[8] = props["COLOR"].GetValue(item);
                    values[9] = props["INSPECTIONQTY"].GetValue(item);
                    values[10] = props["EVENTTIME"].GetValue(item);

                    table.Rows.Add(values);
                });

                return table;
            }
            else
            {
                List<AOIDefectAnalysis> list = (from gr in (from m in sheet
                                                            group m by new { m.LOTID, m.SUBDEFECTNAME, m.GROUPCOLOR } into grp
                                                            select new AOIDefectAnalysis
                                                            {
                                                                LOTID = grp.Key.LOTID,
                                                                DEFECTCODE = grp.Key.SUBDEFECTNAME,
                                                                DEFECTCOUNT = grp.Sum(x => x.DEFECTCOUNT),
                                                                DEFECTRATE = SetDefectRate(grp.Sum(x => x.DEFECTCOUNT), grp.Sum(x => x.INSPECTIONQTY)),
                                                                COLOR = grp.Key.GROUPCOLOR
                                                            })
                                                join de in (from m in sheet
                                                            group m by new { m.LOTID } into grp
                                                            select new AOIDefectAnalysis
                                                            {
                                                                LOTID = grp.Key.LOTID,
                                                                SUMRATEBYLOT = SetDefectRate(grp.Sum(x => x.DEFECTCOUNT), grp.Sum(x => x.INSPECTIONQTY)),
                                                                EVENTTIME = grp.Max(x => x.EVENTTIME)
                                                            })
                                                            on gr.LOTID equals de.LOTID
                                                orderby de.EVENTTIME, gr.LOTID
                                                select new AOIDefectAnalysis
                                                {
                                                    LOTID = gr.LOTID,
                                                    SUMRATEBYLOT = de.SUMRATEBYLOT,
                                                    DEFECTCODE = gr.DEFECTCODE,
                                                    DEFECTCOUNT = gr.DEFECTCOUNT,
                                                    DEFECTRATE = gr.DEFECTRATE,
                                                    COLOR = gr.COLOR,
                                                    EVENTTIME = de.EVENTTIME
                                                }).ToList();

                PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(AOIDefectAnalysis));
                DataTable table = new DataTable();

                foreach (PropertyDescriptor prop in props)
                {
                    table.Columns.Add(prop.Name, prop.PropertyType);
                }

                object[] values = new object[props.Count];

                list.ForEach(item =>
                {
                    values[0] = props["LOTID"].GetValue(item);
                    values[1] = props["SUMRATEBYLOT"].GetValue(item);
                    //values[2] = props["DEFECTGROUP"].GetValue(item);
                    //values[3] = props["DEFECTGROUPCOUNT"].GetValue(item);
                    //values[4] = props["DEFECTGROUPRATE"].GetValue(item);
                    values[5] = props["DEFECTCODE"].GetValue(item);
                    values[6] = props["DEFECTCOUNT"].GetValue(item);
                    values[7] = props["DEFECTRATE"].GetValue(item);
                    values[8] = props["COLOR"].GetValue(item);
                    //values[9] = props["INSPECTIONQTY"].GetValue(item);
                    values[10] = props["EVENTTIME"].GetValue(item);

                    table.Rows.Add(values);
                });

                return table;
            }
        }

        /// <summary>
        /// Rate SubList 분석
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DataTable GetDefectAnalysisByProductRate(DataTable dt, EquipmentType type)
        {
            if (IsNull(dt))
            {
                return null;
            }

            List<InspectionRateSheet> sheet = ConversionToEquipSheet(dt, false);

            if (type.Equals(EquipmentType.EQUIPMENTTYPE_AOI))
            {
                int insepctionQty = sheet.GroupBy(x => new { x.LOTID, x.INSPECTIONQTY }).Sum(x => x.Key.INSPECTIONQTY);

                List<AOIDefectAnalysis> groupList = sheet.GroupBy(m => m.GROUPNAME)
                                                         .Select(grp => new AOIDefectAnalysis
                                                         {
                                                             DEFECTGROUP = grp.Key,
                                                             DEFECTGROUPCOUNT = grp.Sum(x => x.DEFECTCOUNT),
                                                             //INSPECTIONQTY = grp.FirstOrDefault().SUMQTY,
                                                             INSPECTIONQTY = insepctionQty,
                                                             COLOR = grp.First().GROUPCOLOR
                                                         }).ToList();

                List<AOIDefectAnalysis> defectList = sheet.GroupBy(m => new { m.GROUPNAME, m.SUBDEFECTNAME })
                                                          .Select(grp => new AOIDefectAnalysis
                                                          {
                                                              DEFECTGROUP = grp.Key.GROUPNAME,
                                                              DEFECTCODE = grp.Key.SUBDEFECTNAME,
                                                              DEFECTCOUNT = grp.Sum(x => x.DEFECTCOUNT)
                                                          }).ToList();

                if (groupList.Count().Equals(0))
                {
                    return null;
                }

                List<AOIDefectAnalysis> list = (from main in groupList
                                                join sub in defectList
                                                  on main.DEFECTGROUP equals sub.DEFECTGROUP
                                                orderby main.DEFECTGROUP, sub.DEFECTCODE
                                                select new AOIDefectAnalysis
                                                {
                                                    DEFECTGROUP = main.DEFECTGROUP,
                                                    DEFECTGROUPCOUNT = main.DEFECTGROUPCOUNT,
                                                    DEFECTGROUPRATE = SetDefectRate(main.DEFECTGROUPCOUNT, main.INSPECTIONQTY),
                                                    DEFECTCODE = sub.DEFECTCODE,
                                                    DEFECTCOUNT = sub.DEFECTCOUNT,
                                                    DEFECTRATE = SetDefectRate(sub.DEFECTCOUNT, main.INSPECTIONQTY),
                                                    INSPECTIONQTY = main.INSPECTIONQTY,
                                                    COLOR = main.COLOR
                                                }).ToList();

                PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(AOIDefectAnalysis));
                DataTable table = new DataTable();

                foreach (PropertyDescriptor prop in props)
                {
                    table.Columns.Add(prop.Name, prop.PropertyType);
                }

                object[] values = new object[props.Count];

                list.ForEach(item =>
                {
                    //values[0] = props["LOTID"].GetValue(item);
                    //values[1] = props["SUMRATEBYLOT"].GetValue(item);
                    values[2] = props["DEFECTGROUP"].GetValue(item);
                    values[3] = props["DEFECTGROUPCOUNT"].GetValue(item);
                    values[4] = props["DEFECTGROUPRATE"].GetValue(item);
                    values[5] = props["DEFECTCODE"].GetValue(item);
                    values[6] = props["DEFECTCOUNT"].GetValue(item);
                    values[7] = props["DEFECTRATE"].GetValue(item);
                    values[8] = props["COLOR"].GetValue(item);
                    values[9] = props["INSPECTIONQTY"].GetValue(item);

                    table.Rows.Add(values);
                });

                return table;
            }
            else
            {
                List<BBTDefectAnalysis> list = sheet.GroupBy(m => new { m.SUBDEFECTNAME, m.GROUPCOLOR })
                                                    .OrderBy(grp => grp.Key.SUBDEFECTNAME)
                                                    .Select(grp => new BBTDefectAnalysis
                                                    {
                                                        DEFECTCODE = grp.Key.SUBDEFECTNAME,
                                                        DEFECTCOUNT = grp.Sum(x => x.DEFECTCOUNT),
                                                        DEFECTRATE = SetDefectRate(grp.Sum(x => x.DEFECTCOUNT), grp.Sum(x => x.INSPECTIONQTY)),
                                                        COLOR = grp.Key.GROUPCOLOR,
                                                        INSPECTIONQTY = grp.Sum(x => x.INSPECTIONQTY)
                                                    }).ToList();

                PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(BBTDefectAnalysis));
                DataTable table = new DataTable();

                foreach (PropertyDescriptor prop in props)
                {
                    table.Columns.Add(prop.Name, prop.PropertyType);
                }

                object[] values = new object[props.Count];

                list.ForEach(item =>
                {
                    values[0] = props["DEFECTCODE"].GetValue(item);
                    values[1] = props["DEFECTCOUNT"].GetValue(item);
                    values[2] = props["DEFECTRATE"].GetValue(item);
                    values[3] = props["COLOR"].GetValue(item);
                    values[4] = props["INSPECTIONQTY"].GetValue(item);

                    table.Rows.Add(values);
                });

                return table;
            }
        }

        /// <summary>
        /// 품목 Lot별에 따른 검사타입에 따른 개수
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DataTable GetEquipmentTypeByTypeCount(DataTable dt)
        {
            if (IsNull(dt))
            {
                return null;
            }

            List<InspectionRateAnalysis> list = ConversionToEquipSheet(dt, false)
                       .GroupBy(x => new { x.PRODUCTDEFVERSION, x.LOTID })
                       .Select(x => new InspectionRateAnalysis
                       {
                           PRODUCTDEFVERSION = x.Key.PRODUCTDEFVERSION,
                           LOTID = x.Key.LOTID,
                           AOIQTY = x.Where(dr => dr.EQUIPMENTTYPE.Equals("AOI")).Count().Equals(0) ?
                                                  0 : x.Where(dr => dr.EQUIPMENTTYPE.Equals("AOI")).FirstOrDefault().INSPECTIONQTY,
                           AOICOUNT = x.Where(dr => dr.EQUIPMENTTYPE.Equals("AOI")).Sum(dr => dr.DEFECTCOUNT),
                           AOIDEFECTRATE = SetDefectRate(x.Where(dr => dr.EQUIPMENTTYPE.Equals("AOI")).Sum(dr => dr.DEFECTCOUNT)
                                                       , x.Where(dr => dr.EQUIPMENTTYPE.Equals("AOI")).Count().Equals(0) ?
                                                                       0 : x.Where(dr => dr.EQUIPMENTTYPE.Equals("AOI")).FirstOrDefault().INSPECTIONQTY),
                           BBTQTY = x.Where(dr => dr.EQUIPMENTTYPE.Equals("BBT")).Count().Equals(0) ?
                                                  0 : x.Where(dr => dr.EQUIPMENTTYPE.Equals("BBT")).FirstOrDefault().INSPECTIONQTY,
                           BBTCOUNT = x.Where(dr => dr.EQUIPMENTTYPE.Equals("BBT")).Sum(dr => dr.DEFECTCOUNT),
                           BBTDEFECTRATE = SetDefectRate(x.Where(dr => dr.EQUIPMENTTYPE.Equals("BBT")).Sum(dr => dr.DEFECTCOUNT)
                                                       , x.Where(dr => dr.EQUIPMENTTYPE.Equals("BBT")).Count().Equals(0) ?
                                                                       0 : x.Where(dr => dr.EQUIPMENTTYPE.Equals("BBT")).FirstOrDefault().INSPECTIONQTY),
                           HOLEQTY = x.Where(dr => dr.EQUIPMENTTYPE.Equals("HOLE")).Count().Equals(0) ?
                                                   0 : x.Where(dr => dr.EQUIPMENTTYPE.Equals("HOLE")).FirstOrDefault().INSPECTIONQTY,
                           HOLECOUNT = x.Where(dr => dr.EQUIPMENTTYPE.Equals("HOLE")).Sum(dr => dr.DEFECTCOUNT),
                           HOLEDEFECTRATE = SetDefectRate(x.Where(dr => dr.EQUIPMENTTYPE.Equals("HOLE")).Sum(dr => dr.DEFECTCOUNT)
                                                        , x.Where(dr => dr.EQUIPMENTTYPE.Equals("HOLE")).Count().Equals(0) ?
                                                                        0 : x.Where(dr => dr.EQUIPMENTTYPE.Equals("HOLE")).FirstOrDefault().INSPECTIONQTY)
                       }).ToList();

            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(InspectionRateAnalysis));
            DataTable table = new DataTable();

            foreach (PropertyDescriptor prop in props)
            {
                table.Columns.Add(prop.Name, prop.PropertyType);
            }

            object[] values = new object[props.Count];

            list.ForEach(item =>
            {
                values[0] = props["PRODUCTDEFVERSION"].GetValue(item);
                values[1] = props["LOTID"].GetValue(item);
                values[2] = props["AOIDEFECTRATE"].GetValue(item);
                values[3] = props["BBTDEFECTRATE"].GetValue(item);
                values[4] = props["HOLEDEFECTRATE"].GetValue(item);
                values[5] = props["AOIQTY"].GetValue(item);
                values[6] = props["BBTQTY"].GetValue(item);
                values[7] = props["HOLEQTY"].GetValue(item);
                values[8] = props["AOICOUNT"].GetValue(item);
                values[9] = props["BBTCOUNT"].GetValue(item);
                values[10] = props["HOLECOUNT"].GetValue(item);

                table.Rows.Add(values);
            });

            return table;
        }

        /// <summary>
        /// AOI 분석 전/후 비교
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DataTable GetDefectAnalysisByRepair(DataTable dt)
        {
            if (IsNull(dt))
            {
                return null;
            }

            List<AOIDefectRateRepair> list = ConversionToEquipSheet(dt, false)
                                      .GroupBy(x => new { x.PRODUCTDEFVERSION, x.LOTID })
                                      .Select(x => new AOIDefectRateRepair()
                                      {
                                          PRODUCTDEFVERSION = x.Key.PRODUCTDEFVERSION,
                                          LOTID = x.Key.LOTID,
                                          INSPECTIONQTY = x.FirstOrDefault().INSPECTIONQTY,
                                          BEFOREDEFECTCNT = x.Sum(dr => dr.DEFECTCOUNT),
                                          BEFOREDEFECTRATE = SetDefectRate(x.Sum(dr => dr.DEFECTCOUNT), x.FirstOrDefault().INSPECTIONQTY),
                                          ANALYSISTARGET = x.Sum(dr => dr.REPAIRTARGETQTY),
                                          ANALYSISRESULT = x.Sum(dr => dr.REPAIRRESULTQTY),
                                          AFTERDEFECTCNT = x.Sum(dr => dr.DEFECTCOUNT) - x.Sum(dr => dr.REPAIRRESULTQTY),
                                          AFTERDEFECTRATE = SetDefectRate(x.Sum(dr => dr.DEFECTCOUNT) - x.Sum(dr => dr.REPAIRRESULTQTY), x.FirstOrDefault().INSPECTIONQTY),
                                          ANALYSISRATE = SetDefectRate(x.Sum(dr => dr.REPAIRRESULTQTY), x.Sum(dr => dr.REPAIRTARGETQTY))
                                      }).ToList();

            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(AOIDefectRateRepair));
            DataTable table = new DataTable();

            foreach (PropertyDescriptor prop in props)
            {
                table.Columns.Add(prop.Name, prop.PropertyType);
            }

            object[] values = new object[props.Count];

            list.ForEach(item =>
            {
                values[0] = props["PRODUCTDEFVERSION"].GetValue(item);
                values[1] = props["LOTID"].GetValue(item);
                values[2] = props["INSPECTIONQTY"].GetValue(item);
                values[3] = props["BEFOREDEFECTCNT"].GetValue(item);
                values[4] = props["BEFOREDEFECTRATE"].GetValue(item);
                values[5] = props["ANALYSISTARGET"].GetValue(item);
                values[6] = props["ANALYSISRESULT"].GetValue(item);
                values[7] = props["AFTERDEFECTCNT"].GetValue(item);
                values[8] = props["AFTERDEFECTRATE"].GetValue(item);
                values[9] = props["ANALYSISRATE"].GetValue(item);

                table.Rows.Add(values);
            });

            return table;
        }

        /// <summary>
        /// AOI 분석 전/후 비교에 선택한 Row Summary
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DataTable GetDefectAnalysisByRepairSummary(DataTable dt)
        {
            if (IsNull(dt))
            {
                return null;
            }

            List<AOIDefectRateRepair> list = ConversionToRepairSheet(dt);

            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(AOIDefectRateRepair));
            DataTable table = new DataTable();

            foreach (PropertyDescriptor prop in props)
            {
                table.Columns.Add(prop.Name, prop.PropertyType);
            }

            DataRow dr = table.NewRow();
            dr["INSPECTIONQTY"] = list.Sum(x => x.INSPECTIONQTY);
            dr["BEFOREDEFECTCNT"] = list.Sum(x => x.BEFOREDEFECTCNT);
            dr["BEFOREDEFECTRATE"] = SetDefectRate(list.Sum(x => x.BEFOREDEFECTCNT), Format.GetInteger(list.Sum(x => x.INSPECTIONQTY)));
            dr["ANALYSISTARGET"] = list.Sum(x => x.ANALYSISTARGET);
            dr["ANALYSISRESULT"] = list.Sum(x => x.ANALYSISRESULT);
            dr["AFTERDEFECTCNT"] = list.Sum(x => x.AFTERDEFECTCNT);
            dr["AFTERDEFECTRATE"] = SetDefectRate(list.Sum(x => x.AFTERDEFECTCNT), Format.GetInteger(list.Sum(x => x.INSPECTIONQTY)));
            dr["ANALYSISRATE"] = SetDefectRate(list.Sum(x => x.ANALYSISRESULT), list.Sum(x => x.ANALYSISTARGET));
            table.Rows.Add(dr);

            return table;
        }

        /// <summary>
        /// Row Data를 AOISheet SO List로 변환
        /// </summary>
        /// <param name="dt">Row Data</param>
        /// <returns></returns>
        /// [2019.06.24] 조회 조건 "AOI 제외" 일 경우 VRS에서 Panel의 Max 값을 가져오는데, 
        ///              Panel의 Max는 AOI 포함의 Max값을 가져와야 한다. Query에서 처리.
        public static List<AOISheet> ConversionToAOISheet(DataTable dt)
        {
            List<AOISheet> sheet = new List<AOISheet>();

            foreach (DataRow dr in dt.Rows)
            {
                if (StringByDataRowObejct(dr, "DEFECTTYPE").Equals("AOI"))
                {
                    continue;
                }

                sheet.Add(new AOISheet()
                {
                    EQUIPMENTID = StringByDataRowObejct(dr, "EQUIPMENTID"),
                    LOTID = StringByDataRowObejct(dr, "LOTID"),
                    MODELID = StringByDataRowObejct(dr, "PRODUCTDEFID"),
                    LAYERID = StringByDataRowObejct(dr, "LAYERID"),
                    PANELID = StringByDataRowObejct(dr, "PANELID"),
                    DEFECTNO = IntByDataRowObject(dr, "DEFECTNO", default(int)),
                    PCSCOUNT = IntByDataRowObject(dr, "PCSCOUNT", default(int)),
                    INSPECTIONQTY = IntByDataRowObject(dr, "MAX") * IntByDataRowObject(dr, "PCSCOUNT"),
                    ARRAYX = IntByDataRowObject(dr, "ARRAYX", default(int)),
                    ARRAYY = IntByDataRowObject(dr, "ARRAYY", default(int)),
                    X = float.Parse(StringByDataRowObejct(dr, "X")),
                    Y = float.Parse(StringByDataRowObejct(dr, "Y")),
                    GROUPCODE = StringByDataRowObejct(dr, "GROUPCODE"),
                    GROUPNAME = StringByDataRowObejct(dr, "GROUPNAME"),
                    GROUPCOLOR = StringByDataRowObejct(dr, "COLOR"),
                    SUBDEFECTCODE = StringByDataRowObejct(dr, "SUBCODE"),
                    SUBDEFECTNAME = StringByDataRowObejct(dr, "SUBNAME"),
                    DEFECTTYPE = StringByDataRowObejct(dr, "DEFECTTYPE"), // AOI or VRS
                    AOIDEFECTCODE = Format.GetDouble(dr.GetObject("AOIDEFECTCODE"), double.NaN),
                    VRSDEFECTCODE = Format.GetDouble(dr.GetObject("VRSDEFECTCODE"), double.NaN),
                    EVENTTIME = DateTime.Parse(StringByDataRowObejct(dr, "EVENTTIME"))
                });
            }

            return sheet;
        }

        /// <summary>
        /// Row Data를 BBTSheet SO List로 변환
        /// </summary>
        /// <param name="dt">Raw Data</param>
        /// <param name="isPass">pass와 Skip 제외여부</param>
        /// <returns></returns>
        public static List<BBTSheet> ConversionToBBTSheet(DataTable dt, bool isPass = true)
        {
            List<BBTSheet> sheet = new List<BBTSheet>();

            string sLot = string.Empty;
            //int nMaxPanel = 0;

            foreach (DataRow dr in dt.Rows)
            {
                if (!sLot.Equals(StringByDataRowObejct(dr, "LOTID")))
                {
                    sLot = StringByDataRowObejct(dr, "LOTID");
                    //nMaxPanel = Convert.ToInt16(dt.AsEnumerable().Where(x => x.Field<string>("LOTID") == sLot).Max(x => x.Field<string>("PANELID")));
                }

                // 영풍은 SKIP, PASS는 미포함. 인터는 아직 미정.
                // 인터는 x와 p를 제외하지 않는다면 x/p code를 사용하지 말아야 한다.
                // 제외하고 싶은 코드가 있다면 개발 추가
                if (isPass)
                {
                    if (StringByDataRowObejct(dr, "DEFECTCODE").Equals("X") || StringByDataRowObejct(dr, "DEFECTCODE").Equals("P"))
                    {
                        continue;
                    }
                }

                sheet.Add(new BBTSheet()
                {
                    EQUIPMENTID = StringByDataRowObejct(dr, "EQUIPMENTID"),
                    LOTID = StringByDataRowObejct(dr, "LOTID"),
                    MODELID = StringByDataRowObejct(dr, "PRODUCTDEFID"),
                    MODELIDREV = StringByDataRowObejct(dr, "PRODUCTDEFVERSION"),
                    PANELID = StringByDataRowObejct(dr, "PANELID"),
                    DEFECTNO = IntByDataRowObject(dr, "DEFECTNO", default(int)),
                    PCSCOUNT = IntByDataRowObject(dr, "PCSCOUNT", default(int)),
                    INSPECTIONQTY = IntByDataRowObject(dr, "MAX") * IntByDataRowObject(dr, "PCSCOUNT"),
                    ARRAYX = IntByDataRowObject(dr, "ARRAYX", default(int)),
                    ARRAYY = IntByDataRowObject(dr, "ARRAYY", default(int)),
                    X = IntByDataRowObject(dr, "X"),
                    Y = IntByDataRowObject(dr, "Y"),
                    DEFECTCODE = StringByDataRowObejct(dr, "DEFECTCODE"),
                    DEFECTNAME = StringByDataRowObejct(dr, "SUBNAME"),
                    DEFECTTYPE = StringByDataRowObejct(dr, "DEFECTTYPE"),
                    COLOR = StringByDataRowObejct(dr, "COLOR"),
                    EVENTTIME = DateTime.Parse(StringByDataRowObejct(dr, "EVENTTIME"))
                });
            }

            return sheet;
        }

        /// <summary>
        /// 분석대상에 대한 row에 적용 영풍
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DataTable RepairAnalysisByYPE(DataTable dt)
        {
            DataTable analysisDt = dt.Copy();

            foreach (DataRow dr in analysisDt.Rows)
            {
                if (IntByDataRowObject(dr, "REPAIRRESULTQTY") is int repairresultqty)
                {
                    if (repairresultqty.Equals(0))
                    {
                        continue;
                    }

                    dr["DEFECTCOUNT"] = IntByDataRowObject(dr, "DEFECTCOUNT") - repairresultqty;
                }
            }

            var filterDt = analysisDt.AsEnumerable().Where(x => x.Field<int>("DEFECTCOUNT") > 0);

            return filterDt.Count().Equals(0) ? dt.Clone() : filterDt.CopyToDataTable();
        }

        /// <summary>
        /// 분석대상에 대한 row에 적용 인터
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DataTable RepairAnalsysByIFC(DataTable dt)
        {
            DataTable defectList = GetAOIDefectCodeInfo;

            var analysis = dt.AsEnumerable().Where(x => x.Field<string>("GROUPCODE") != "005");
            var repair = dt.AsEnumerable().Where(x => x.Field<string>("GROUPCODE") == "005");

            DataTable analysisDt = analysis.Count().Equals(0) ? dt.Clone() : analysis.CopyToDataTable();
            DataTable repairDt = repair.Count().Equals(0) ? dt.Clone() : repair.CopyToDataTable();
            DataRow newRow = null;
            string subCode = string.Empty;

            foreach (DataRow dr in repairDt.Rows)
            {
                if (IntByDataRowObject(dr, "REPAIRRESULTQTY") is int repairresultqty)
                {
                    if (repairresultqty.Equals(0))
                    {
                        analysisDt.ImportRow(dr);
                        continue;
                    }

                    if ((IntByDataRowObject(dr, "DEFECTCOUNT") - repairresultqty) is int defectCount)
                    {
                        if (defectCount.Equals(0))
                        {
                            continue;
                        }

                        subCode = StringByDataRowObejct(dr, "SUBCODE").Equals("5001") ? "1001" : "2001";

                        if (analysisDt.Select(string.Concat("LOTID='", StringByDataRowObejct(dr, "LOTID"), "' AND SUBCODE='", subCode, "'")) is DataRow[] selectedRow)
                        {
                            if (selectedRow.Count().Equals(0))
                            {
                                if (defectList.Select(string.Concat("SUBCODE='", subCode, "'")) is DataRow[] defectCode)
                                {
                                    if (defectCode.Count().Equals(0))
                                    {
                                        continue;
                                    }

                                    newRow = dr;
                                    newRow["GROUPCODE"] = defectCode[0]["GROUPCODE"];
                                    newRow["GROUPNAME"] = defectCode[0]["GROUPNAME"];
                                    newRow["SUBCODE"] = defectCode[0]["SUBCODE"];
                                    newRow["SUBNAME"] = defectCode[0]["SUBNAME"];
                                    newRow["COLOR"] = defectCode[0]["COLOR"];
                                    newRow["DEFECTCOUNT"] = defectCount;
                                    analysisDt.Rows.Add(newRow.ItemArray);
                                }
                            }
                            else
                            {
                                selectedRow[0]["DEFECTCOUNT"] = IntByDataRowObject(selectedRow[0], "DEFECTCOUNT") + defectCount;
                            }
                        }
                    }
                }
            }

            return analysisDt;
        }

        /// <summary>
        /// Row Data를 InspectionRateSheet SO List로 변환
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="isRepair">리페어 진행</param>
        /// <returns></returns>
        public static List<InspectionRateSheet> ConversionToEquipSheet(DataTable dt, bool isRepair)
        {
            List<InspectionRateSheet> sheet = new List<InspectionRateSheet>();

            int total = dt.AsEnumerable().GroupBy(x => x.Field<int>("INSPECTIONQTY")).Sum(x => x.Key);

            if (isRepair)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    sheet.Add(new InspectionRateSheet()
                    {
                        EQUIPMENTTYPE = StringByDataRowObejct(dr, "EQUIPMENTTYPE"),
                        PRODUCTDEFID = StringByDataRowObejct(dr, "PRODUCTDEFID"),
                        PRODUCTDEFVERSION = StringByDataRowObejct(dr, "PRODUCTDEFVERSION"),
                        LOTID = StringByDataRowObejct(dr, "LOTID"),
                        INSPECTIONQTY = IntByDataRowObject(dr, "INSPECTIONQTY"),
                        REPAIRTARGETQTY = IntByDataRowObject(dr, "REPAIRTARGETQTY"),
                        REPAIRRESULTQTY = IntByDataRowObject(dr, "REPAIRRESULTQTY"),
                        GROUPCODE = StringByDataRowObejct(dr, "GROUPCODE"),
                        GROUPNAME = StringByDataRowObejct(dr, "GROUPNAME"),
                        GROUPCOLOR = StringByDataRowObejct(dr, "COLOR"),
                        SUBDEFECTCODE = StringByDataRowObejct(dr, "SUBCODE"),
                        SUBDEFECTNAME = StringByDataRowObejct(dr, "SUBNAME"),
                        DEFECTCOUNT = IntByDataRowObject(dr, "DEFECTCOUNT") - IntByDataRowObject(dr, "REPAIRRESULTQTY"),
                        EVENTTIME = DateTime.Parse(StringByDataRowObejct(dr, "EVENTTIME")),
                        SUMQTY = total
                    });
                }
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    sheet.Add(new InspectionRateSheet()
                    {
                        EQUIPMENTTYPE = StringByDataRowObejct(dr, "EQUIPMENTTYPE"),
                        PRODUCTDEFID = StringByDataRowObejct(dr, "PRODUCTDEFID"),
                        PRODUCTDEFVERSION = StringByDataRowObejct(dr, "PRODUCTDEFVERSION"),
                        LOTID = StringByDataRowObejct(dr, "LOTID"),
                        INSPECTIONQTY = IntByDataRowObject(dr, "INSPECTIONQTY"),
                        REPAIRTARGETQTY = IntByDataRowObject(dr, "REPAIRTARGETQTY"),
                        REPAIRRESULTQTY = IntByDataRowObject(dr, "REPAIRRESULTQTY"),
                        GROUPCODE = StringByDataRowObejct(dr, "GROUPCODE"),
                        GROUPNAME = StringByDataRowObejct(dr, "GROUPNAME"),
                        GROUPCOLOR = StringByDataRowObejct(dr, "COLOR"),
                        SUBDEFECTCODE = StringByDataRowObejct(dr, "SUBCODE"),
                        SUBDEFECTNAME = StringByDataRowObejct(dr, "SUBNAME"),
                        DEFECTCOUNT = IntByDataRowObject(dr, "DEFECTCOUNT"),
                        EVENTTIME = DateTime.Parse(StringByDataRowObejct(dr, "EVENTTIME")),
                        SUMQTY = total
                    });
                }
            }

            return sheet;
        }

        /// <summary>
        /// Row Data를 AOIDefectRateRepair SO List로 변환
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<AOIDefectRateRepair> ConversionToRepairSheet(DataTable dt)
        {
            List<AOIDefectRateRepair> sheet = new List<AOIDefectRateRepair>();

            foreach (DataRow dr in dt.Rows)
            {
                sheet.Add(new AOIDefectRateRepair()
                {
                    PRODUCTDEFVERSION = StringByDataRowObejct(dr, "PRODUCTDEFVERSION"),
                    LOTID = StringByDataRowObejct(dr, "LOTID"),
                    INSPECTIONQTY = IntByDataRowObject(dr, "INSPECTIONQTY"),
                    BEFOREDEFECTCNT = IntByDataRowObject(dr, "BEFOREDEFECTCNT"),
                    BEFOREDEFECTRATE = Format.GetDouble(dr.GetObject("BEFOREDEFECTRATE"), double.NaN),
                    ANALYSISTARGET = IntByDataRowObject(dr, "ANALYSISTARGET"),
                    ANALYSISRESULT = IntByDataRowObject(dr, "ANALYSISRESULT"),
                    AFTERDEFECTCNT = IntByDataRowObject(dr, "AFTERDEFECTCNT"),
                    AFTERDEFECTRATE = Format.GetDouble(dr.GetObject("AFTERDEFECTRATE"), double.NaN),
                    ANALYSISRATE = Format.GetDouble(dr.GetObject("ANALYSISRATE"), double.NaN)
                });
            }

            return sheet;
        }

        /// <summary>
        /// Defect Map AOI Diagram 그리기. 기능이 없이 diagram만 그린다.
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static ucAOIDiagram DrawingAOIDiagramByMain(DataTable dt)
        {
            if (IsNull(dt))
            {
                return null;
            }

            Image image = GetPcsImage(dt, out int nXMax, out int nYMax);

            ucAOIDiagram control = new ucAOIDiagram(nXMax, nYMax, AOIMode.AOIMODE_MAIN, image)
            {
                Dock = DockStyle.Fill
            };

            control.InspectionTypeEvent += (str) => IsDefectTypeIsVRS(dt, str);
            control.PointSelectionEvent += (str) => { };

            foreach (DataRow row in dt.Rows)
            {
                Color color = StringByDataRowObejct(row, "DEFECTTYPE").Equals("AOI") ? Color.Gray : ColorTranslator.FromHtml(StringByDataRowObejct(row, "COLOR"));

                control.AddXYPoint(float.Parse(StringByDataRowObejct(row, "X")), float.Parse(StringByDataRowObejct(row, "Y")),
                                    color, StringByDataRowObejct(row, "DEFECTNO"));
            }

            return control;
        }

        /// <summary>
        /// Defect Map AOI Diagram 그리기
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="image"></param>
        /// <param name="xMax"></param>
        /// <param name="yMax"></param>
        /// <param name="param"></param>
        /// <param name="colorList"></param>
        /// <returns></returns>
        public static ucAOIDiagram DrawingAOIDiagramByMain(DataTable dt, Image image, int xMax, int yMax, string param, Dictionary<string, Color> colorList, List<string> mirrorList)
        {
            if (IsNull(dt) || dt.Rows.Count.Equals(0))
            {
                return null;
            }

            ucAOIDiagram control = new ucAOIDiagram(xMax, yMax, AOIMode.AOIMODE_MAIN, image)
            {
                Dock = DockStyle.Fill
            };

            dt.AsEnumerable()
              .OrderBy(x => x.Field<string>("DEFECTTYPE"))
              .ForEach(row =>
              {
                  Color color = Color.Empty;

                  if (IsNull(colorList) || colorList.Count().Equals(0))
                  {
                      color = StringByDataRowObejct(row, "DEFECTTYPE").Equals("AOI") ?
                                NONE_COLOR : ColorTranslator.FromHtml(StringByDataRowObejct(row, "COLOR"));
                  }
                  else
                  {
                      if (colorList.TryGetValue(StringByDataRowObejct(row, param), out color))
                      {
                          if (color.Equals(Color.Empty))
                          {
                              color = StringByDataRowObejct(row, "DEFECTTYPE").Equals("AOI") ?
                                        NONE_COLOR : ColorTranslator.FromHtml(StringByDataRowObejct(row, "COLOR"));
                          }
                      }
                  }

                  if (!mirrorList.Count.Equals(0) && !mirrorList[0].Equals("None"))
                  {
                      if (mirrorList.Contains(StringByDataRowObejct(row, "LAYERID")))
                      {
                          row["X"] = (xMax / 2) + ((xMax / 2) - IntByDataRowObject(row, "X"));
                      }
                  }

                  control.AddXYPoint(float.Parse(StringByDataRowObejct(row, "X")), float.Parse(StringByDataRowObejct(row, "Y")),
                                     color, StringByDataRowObejct(row, "DEFECTNO"));
              });

            return control;
        }

        /// <summary>
        /// Nail Map AOI Diagram 그리기
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="xMax"></param>
        /// <param name="yMax"></param>
        /// <param name="title"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static ucAOIDiagram DrawingAOIDiagramByNail(IGrouping<object, DataRow> dt, int xMax, int yMax, string title, AOIMode type)
        {
            if (IsNull(dt) || dt.Count().Equals(0))
            {
                return null;
            }

            ucAOIDiagram control = new ucAOIDiagram(xMax, yMax, type, null, title);

            foreach (DataRow row in dt)
            {
                Color color = StringByDataRowObejct(row, "DEFECTTYPE").Equals("AOI") ?
                                NONE_COLOR : ColorTranslator.FromHtml(StringByDataRowObejct(row, "COLOR"));

                control.AddXYPoint(float.Parse(StringByDataRowObejct(row, "X", "0")), float.Parse(StringByDataRowObejct(row, "Y", "0")),
                                    color, StringByDataRowObejct(row, "DEFECTNO"));
            }

            return control;
        }

        /// <summary>
        /// BBT Nail View 그리기
        /// </summary>
        /// <param name="item">BBT Data</param>
        /// <param name="rowNum">BBT Col 정보</param>
        /// <param name="colNum">BBT Row 정보</param>
        /// <returns></returns>
        public static ucBBTDiagram DrawingBBTDiagramByNail(IGrouping<object, DataRow> item, int rowNum, int colNum, DataTable dt)
        {
            ucBBTDiagram control = new ucBBTDiagram(BBTMode.BBTMODE_NAIL, Format.GetString(item.Key), dt)
            {
                Size = new Size((GetBBTWidth(rowNum) * 20) + 20, (colNum * 20) + 20),
                Margin = new Padding(2, 2, 2, 2)
            };

            DataTable table = new DataTable();
            for (int i = 0; i < rowNum; i++)
            {
                table.Columns.Add(Format.GetString(i), typeof(string));
            }

            string[] values = new string[rowNum];

            item.AsEnumerable()
                .OrderBy(x => x.Field<double>("Y"))
                .ThenBy(x => x.Field<double>("X"))
                .ForEach(dr =>
                {
                    values[Convert.ToInt32(dr.Field<double>("X")) - 1] = dr.Field<string>("DEFECTCODE");
                    if (rowNum.Equals(Convert.ToInt32(dr.Field<double>("X"))))
                    {
                        table.Rows.Add(values);
                    }
                });

            control.SetData(table);

            return control;
        }

        /// <summary>
        /// BBT Main View 그리기
        /// </summary>
        /// <param name="dt">Row Data</param>
        /// <returns></returns>
        public static ucBBTDiagram DrawingBBTDiagramByMain(DataTable dt)
        {
            if (IsNull(dt))
            {
                return null;
            }

            ucBBTDiagram control = new ucBBTDiagram(BBTMode.BBTMODE_MAIN)
            {
                Dock = DockStyle.Fill
            };

            DataTable table = new DataTable();

            for (int i = 0; i < dt.Rows[0].Field<int>("ARRAYX"); i++)
            {
                table.Columns.Add(Format.GetString(i), typeof(int));
            }

            DataRow row = table.NewRow();
            int nSum = 0;
            int nYCheckPoint = 1;
            int nXCheckPoint = 1;

            if (table.Columns.Count.Equals(1))
            {
                dt.AsEnumerable()
                  .OrderBy(x => x.Field<double>("Y"))
                  .ThenBy(x => x.Field<double>("X"))
                  .ForEach(dr =>
                  {
                      if (!StringByDataRowObejct(dr, "DEFECTCODE").Equals("X")
                       && !StringByDataRowObejct(dr, "DEFECTCODE").Equals("P"))
                      {
                          nSum = ++nSum;
                      }

                      if (!nYCheckPoint.Equals(IntByDataRowObject(dr, "Y")))
                      {
                          row[nXCheckPoint - 1] = nSum;
                          nYCheckPoint = IntByDataRowObject(dr, "Y");
                          table.Rows.Add(row);
                          row = table.NewRow();
                          nSum = 0;
                      }
                  });
            }
            else
            {
                dt.AsEnumerable()
                    .OrderBy(x => x.Field<double>("Y"))
                    .ThenBy(x => x.Field<double>("X"))
                    .ForEach(dr =>
                    {
                        if (!nXCheckPoint.Equals(IntByDataRowObject(dr, "X")))
                        {
                            if (table.Columns.Count < nXCheckPoint) // 2020.06.25-유석진-컬럼 추가 로직 추가
                            {
                                table.Columns.Add(Format.GetString(nXCheckPoint-1), typeof(int));
                            }
                            row[nXCheckPoint - 1] = nSum;
                            nXCheckPoint = IntByDataRowObject(dr, "X");
                            nSum = 0;

                        }

                        if (!StringByDataRowObejct(dr, "DEFECTCODE").Equals("X")
                        && !StringByDataRowObejct(dr, "DEFECTCODE").Equals("P"))
                        {
                            nSum = ++nSum;
                        }

                        if (!nYCheckPoint.Equals(IntByDataRowObject(dr, "Y")))
                        {
                            nYCheckPoint = IntByDataRowObject(dr, "Y");
                            table.Rows.Add(row);
                            row = table.NewRow();
                        }
                    });
            }

            row[nXCheckPoint - 1] = nSum;
            table.Rows.Add(row);

            control.SetData(table);

            // 각 cell에 Defect 상세 내용 표기
            control.SetToolTip(GetBBTToolTipByMain(dt));

            return control;
        }

        /// <summary>
        /// 품목별 Line Chart 그리기
        /// </summary>
        /// <param name="chartControl"></param>
        /// <param name="dt"></param>
        public static void DrawingLineChartByItem(SmartChart chartControl, DataTable dt)
        {
            chartControl.Series.Clear();

            Series aoiSeries = new Series("AOI", ViewType.Line) { View = SetLineSeriesView("AOI") };
            Series bbtSeries = new Series("BBT", ViewType.Line) { View = SetLineSeriesView("BBT") };
            Series holeSeries = new Series("HOLE", ViewType.Line) { View = SetLineSeriesView("HOLE") };

            foreach (DataRow dr in dt.Rows)
            {
                aoiSeries.Points.Add(SetSeriesPoint(StringByDataRowObejct(dr, "KEY"), EquipmentType.EQUIPMENTTYPE_AOI, Format.GetDouble(dr.GetObject("AOITOTALRATE"), double.NaN), Color.Blue));
                bbtSeries.Points.Add(SetSeriesPoint(StringByDataRowObejct(dr, "KEY"), EquipmentType.EQUIPMENTTYPE_BBT, Format.GetDouble(dr.GetObject("BBTDEFECTRATE"), double.NaN), Color.Green));
                holeSeries.Points.Add(SetSeriesPoint(StringByDataRowObejct(dr, "KEY"), EquipmentType.EQUIPMENTTYPE_HOLE, Format.GetDouble(dr.GetObject("HOLETOTALRATE"), double.NaN), Color.Gray));
            }

            chartControl.Series.AddRange(new Series[] { aoiSeries, bbtSeries, holeSeries });

            chartControl.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
            chartControl.Legend.AlignmentVertical = LegendAlignmentVertical.Bottom;
            chartControl.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.LeftOutside;
            chartControl.Legend.MarkerMode = LegendMarkerMode.CheckBoxAndMarker;

            SetChartZoom(chartControl);
        }

        /// <summary>
        /// AOI Line Chart 그리기
        /// Color를 지정함에 AOI/BBT 구분
        /// </summary>
        /// <param name="chartControl">Chart Control</param>
        /// <param name="dt">Row Data</param>
        /// <param name="x">X Argument</param>
        /// <param name="y">Y Argument</param>
        public static void DrawingLineChartByAOI(SmartChart chartControl, DataTable dt, string x, string y)
        {
            chartControl.Series.Clear();

            LineSeriesView view = new LineSeriesView()
            {
                MarkerVisibility = DevExpress.Utils.DefaultBoolean.True
            };
            view.LineMarkerOptions.Size = 4;

            Series series = new Series(x, ViewType.Line)
            {
                View = view
            };

            if (!IsNull(dt.Columns["COLOR"]))
            {
                foreach (DataRow dr in dt.Rows)
                {
                    series.Points.Add(new SeriesPoint(StringByDataRowObejct(dr, x), dr.GetObject(y))
                    {
                        Color = ColorTranslator.FromHtml(StringByDataRowObejct(dr, "COLOR"))
                    });
                }
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    series.Points.Add(new SeriesPoint(StringByDataRowObejct(dr, x), dr.GetObject(y)));
                }
            }

            chartControl.Series.Add(series);

            chartControl.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            ((XYDiagram)chartControl.Diagram).AxisX.Label.Visible = false;
            ((XYDiagram)chartControl.Diagram).AxisX.VisualRange.Auto = true;
            //((XYDiagram)chartControl.Diagram).AxisX.VisualRange.MaxValueSerializable = "9";
            //((XYDiagram)chartControl.Diagram).AxisX.VisualRange.MinValueSerializable = "0";
            //((XYDiagram)chartControl.Diagram).AxisY.AutoScaleBreaks.Enabled = true;
            //((XYDiagram)chartControl.Diagram).AxisY.AutoScaleBreaks.MaxCount = 2;
        }

        /// <summary>
        /// Bar Chart 그리기
        /// Color를 지정함
        /// </summary>
        /// <param name="chartControl">Chart Control</param>
        /// <param name="dt">Row Data</param>
        /// <param name="x">X Argument</param>
        /// <param name="y">Y Argument</param>
        public static void DrawingBarChart(SmartChart chartControl, DataTable dt, string x, string y)
        {
            chartControl.Series.Clear();
            Series series = new Series(x, ViewType.Bar);

            foreach (DataRow dr in dt.Rows)
            {
                series.Points.Add(new SeriesPoint(StringByDataRowObejct(dr, x), dr.GetObject(y))
                {
                    Color = ColorTranslator.FromHtml(StringByDataRowObejct(dr, "COLOR"))
                });
            }

            chartControl.Series.Add(series);

            chartControl.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            ((XYDiagram)chartControl.Diagram).AxisY.AutoScaleBreaks.Enabled = true;
            ((XYDiagram)chartControl.Diagram).AxisY.AutoScaleBreaks.MaxCount = 2;
        }

        /// <summary>
        /// AOI 분석 전/후 Bar Chart 그리기
        /// </summary>
        /// <param name="chartControl"></param>
        /// <param name="dt"></param>
        /// <param name="before"></param>
        /// <param name="after"></param>
        public static void DrawingBarChartByRepair(SmartChart chartControl, DataTable dt, string before, string after)
        {
            chartControl.Series.Clear();

            if (dt.Rows.Count.Equals(0))
            {
                return;
            }

            Series series = new Series("Analysis", ViewType.Bar)
            {
                LabelsVisibility = DevExpress.Utils.DefaultBoolean.True
            };

            series.Points.Add(new SeriesPoint(before, Format.GetDouble(dt.Rows[0].GetObject("BEFOREDEFECTRATE"), double.NaN)));
            series.Points.Add(new SeriesPoint(after, Format.GetDouble(dt.Rows[0].GetObject("AFTERDEFECTRATE"), double.NaN)));

            chartControl.Series.Add(series);
            chartControl.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
        }

        /// <summary>
        /// Side By Side Stacked Bar Chart 그리기
        /// </summary>
        /// <param name="chartControl">Chart Control</param>
        /// <param name="dt">Row Data</param>
        /// <param name="group">Series</param>
        /// <param name="groupItem">X Argument</param>
        /// <param name="value">Point Value</param>
        public static void DrawingSideBySideStackedBarChart(SmartChart chartControl, DataTable dt, string group, string groupItem, string value)
        {
            chartControl.Series.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                Series series = new Series(StringByDataRowObejct(dr, group), ViewType.SideBySideStackedBar)
                {
                    LabelsVisibility = DevExpress.Utils.DefaultBoolean.False
                };

                series.Points.Add(new SeriesPoint(StringByDataRowObejct(dr, groupItem), dr.GetObject(value)));
                series.View.Color = ColorTranslator.FromHtml(StringByDataRowObejct(dr, "COLOR"));
                chartControl.Series.Add(series);
            }

            chartControl.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            //((XYDiagram)chartControl.Diagram).DefaultPane.StackedBarTotalLabel.Visible = true;
            ((XYDiagram)chartControl.Diagram).AxisY.AutoScaleBreaks.Enabled = true;
            ((XYDiagram)chartControl.Diagram).AxisY.AutoScaleBreaks.MaxCount = 2;
        }

        /// <summary>
        /// AOI/BBT 비교 Bar Chart
        /// </summary>
        /// <param name="chartControl">Smart Chart</param>
        /// <param name="leftDt">AOI Row Data</param>
        /// <param name="rightDt">BBT Row Data</param>
        public static void DrawingBarChartByComparison(SmartChart chartControl, DataTable leftDt, DataTable rightDt, string left = "AOI", string right = "BBT")
        {
            Series series = new Series("DEFECT", ViewType.Bar)
            {
                LabelsVisibility = DevExpress.Utils.DefaultBoolean.True
            };

            if (!IsNull(leftDt))
            {
                series.Points.Add(new SeriesPoint(left, leftDt.AsEnumerable().Sum(x => x.Field<double>("DEFECTRATE"))));
            }

            if (!IsNull(rightDt))
            {
                series.Points.Add(new SeriesPoint(right, rightDt.AsEnumerable().Sum(x => x.Field<double>("DEFECTRATE"))));
            }

            chartControl.Series.Add(series);
            chartControl.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
        }

        /// <summary>
        /// AOI BBT HOLE 비교 Bar Chart 그리기
        /// </summary>
        /// <param name="chartControl"></param>
        /// <param name="list"></param>
        public static void DrawingComparisonBarChart(SmartChart chartControl, Dictionary<string, List<int>> list)
        {
            chartControl.Series.Clear();

            Series series = new Series("DEFECT", ViewType.Bar)
            {
                LabelsVisibility = DevExpress.Utils.DefaultBoolean.True
            };

            list.ForEach(item =>
                series.Points.Add(new SeriesPoint(item.Key, SetDefectRate(item.Value[1], item.Value[0])))
            );

            chartControl.Series.Add(series);
            chartControl.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
        }

        /// <summary>
        /// Chart Control에 Secondary Series 그리기
        /// </summary>
        /// <param name="chartControl">Control</param>
        /// <param name="dt">Row Data</param>
        /// <param name="x">X Value</param>
        /// <param name="y">Y Value</param>
        public static void AddSecondarySeriesToLineChart(SmartChart chartControl, DataTable dt, string x, string y)
        {
            SecondaryAxisY axisy;

            if (((XYDiagram)chartControl.Diagram).SecondaryAxesY.Count.Equals(0))
            {
                axisy = new SecondaryAxisY("Rate");
                ((XYDiagram)chartControl.Diagram).SecondaryAxesY.Add(axisy);
            }
            else
            {
                axisy = ((XYDiagram)chartControl.Diagram).SecondaryAxesY[0];
            }

            LineSeriesView view = new LineSeriesView()
            {
                MarkerVisibility = DevExpress.Utils.DefaultBoolean.False,
                Color = ColorTranslator.FromHtml("#404040"),
                AxisY = axisy
            };
            view.LineMarkerOptions.Size = 5;

            Series series = new Series(x, ViewType.Line)
            {
                View = view
            };

            foreach (DataRow dr in dt.Rows)
            {
                series.Points.Add(new SeriesPoint(StringByDataRowObejct(dr, x), dr.GetObject(y)));
                series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            }

            chartControl.Series.Add(series);
        }

        /// <summary>
        /// Add RateMain UserControl
        /// </summary>
        /// <param name="title"></param>
        /// <param name="dt"></param>
        /// <param name="type"></param>
        /// <param name="viewType"></param>
        /// <returns></returns>
        public static ucRateDefault AddRateControl(string title, DataTable dt, EquipmentType type, ViewType viewType)
        {
            ucRateDefault control = new ucRateDefault() { Size = new Size(1350, 300) };
            control.SetData(title, dt, type, viewType);
            return control;
        }

        /// <summary>
        /// Add RateEquipmentType UserControl
        /// </summary>
        /// <param name="str"></param>
        /// <param name="dt"></param>
        /// <param name="type"></param>
        /// <param name="viewType"></param>
        /// <returns></returns>
        public static ucRateEquipmentType AddRateControlByEquipment(string str, DataTable dt, EquipmentType type, SubViewType viewType)
            => new ucRateEquipmentType(str, dt, type, viewType) { Size = new Size(1350, 300) };

        /// <summary>
        /// Chart 확대 옵션 설정
        /// </summary>
        /// <param name="chart"></param>
        public static void SetChartZoom(SmartChart chart)
        {
            XYDiagram diagram = (XYDiagram)chart.Diagram;

            diagram.DefaultPane.EnableAxisXScrolling = DevExpress.Utils.DefaultBoolean.True;
            diagram.DefaultPane.EnableAxisYScrolling = DevExpress.Utils.DefaultBoolean.False;

            diagram.EnableAxisXZooming = true;
            diagram.EnableAxisYZooming = false;

            diagram.AxisX.Label.Visible = false;
        }

        /// <summary>
        /// SeriesPoint 생성
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public static SeriesPoint SetSeriesPoint(string key, EquipmentType equipmentType, object value, Color? color = null)
        {
            SeriesPoint sp = new SeriesPoint(key, value)
            {
                Tag = equipmentType
            };

            if (!IsNull(color))
            {
                sp.Color = color ?? Color.Blue;
            }

            return sp;
        }

        /// <summary>
        /// Line Chart LineSeriesView 설정
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static LineSeriesView SetLineSeriesView(string str)
        {
            LineSeriesView view = new LineSeriesView
            {
                MarkerVisibility = DevExpress.Utils.DefaultBoolean.True,
                Color = str.Equals("AOI") ? Color.SkyBlue :
                        str.Equals("BBT") ? Color.ForestGreen :
                                            Color.LightSalmon
            };
            view.LineMarkerOptions.Size = 4;

            return view;
        }

        /// <summary>
        /// Grid Footer Rate 계산
        /// </summary>
        /// <param name="sender">Grid View</param>
        /// <param name="e"></param>
        public static void GetCustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            if (e.IsTotalSummary)
            {
                GridSummaryItem item = e.Item as GridSummaryItem;
                if (item.FieldName.Equals("DEFECTRATE"))
                {
                    if (e.SummaryProcess.Equals(DevExpress.Data.CustomSummaryProcess.Finalize))
                    {
                        if ((sender as GridView).Columns["INSPECTIONQTY"].SummaryItem.SummaryValue is int total)
                        {
                            if ((sender as GridView).Columns["DEFECTCOUNT"].SummaryItem.SummaryValue is int value)
                            {
                                e.TotalValue = SetDefectRate(value, total);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// AOI Diagram에 Defect Point(Defect No)의 VRS/AOI 여부
        /// VRS의 경우에만 ToolTip을 보여주기 위해 쓰인 함수
        /// </summary>
        /// <param name="dt">Row Data</param>
        /// <param name="str">Defect No</param>
        /// <returns></returns>
        public static bool IsDefectTypeIsVRS(DataTable dt, string str)
            => dt.AsEnumerable()
                 .Where(x => Format.GetString(x.Field<int>("DEFECTNO")) == str)
                 .Select(x => x.Field<string>("DEFECTTYPE")).ToArray()[0].Equals("AOI") ? false : true;

        /// <summary>
        /// AOI Defect Map Nail View에서 Check 된 항목을 리스트한다
        /// </summary>
        /// <param name="flow">FlowLayout Control</param>
        /// <param name="totalData">Row Data</param>
        /// <param name="param">화면 기준 Param</param>
        /// <param name="colorList">Nail 별 Color 지정시 담아둘 List </param>
        /// <returns></returns>
        public static DataTable GetAOICheckedInNailMap(FlowLayoutPanel flow, DataTable totalData, string param, out Dictionary<string, Color> colorList)
        {
            List<string> list = new List<string>();
            colorList = new Dictionary<string, Color>();

            foreach (ucAOIDiagram control in flow.Controls)
            {
                if (control.IsCheckState)
                {
                    list.Add(control.GetDiagramTitle);

                    if (!control.GetNailColor.Equals(Color.Transparent))
                    {
                        colorList.Add(control.GetDiagramTitle, control.GetNailColor);
                    }
                }
            }

            return list.Count.Equals(0) ? null : totalData.AsEnumerable()
                                                          .Where(x => list.Contains(Format.GetString(x.Field<object>(param))))
                                                          .CopyToDataTable();
        }

        /// <summary>
        /// BBT Defect Map Nail View에서 Check 된 항목을 리스트한다
        /// </summary>
        /// <param name="flow">FlowLayout Control</param>
        /// <param name="totalData">Row Data</param>
        /// <param name="param">화면 기준 Param</param>
        /// <returns></returns>
        public static DataTable GetBBTCheckedInNailMap(FlowLayoutPanel flow, DataTable totalData, string param)
        {
            List<string> list = new List<string>();

            foreach (ucBBTDiagram control in flow.Controls)
            {
                if (control.IsCheckState)
                {
                    list.Add(control.GetDiagramTitle);
                }
            }

            return list.Count.Equals(0) ? null : totalData.AsEnumerable()
                                                          .Where(x => list.Contains(Format.GetString(x.Field<object>(param))))
                                                          .CopyToDataTable();
        }

        /// <summary>
        /// Row Data에서 가장큰 X 좌표를 구한다.
        /// </summary>
        /// <param name="dt">Row Data</param>
        /// <returns></returns>
        public static int GetDiagramXSize(DataTable dt)
        {
            int arrayx = dt.AsEnumerable().Max(x => x.Field<int>("ARRAYX"));

            return arrayx.Equals(0) ? (int)Math.Round(dt.AsEnumerable().Max(x => x.Field<double>("X")), 0) + 5 : arrayx;
        }

        /// <summary>
        /// Row Data에서 가장큰 Y 좌표를 구한다.
        /// </summary>
        /// <param name="dt">Row Data</param>
        /// <returns></returns>
        public static int GetDiagramYSize(DataTable dt)
        {
            int arrayy = dt.AsEnumerable().Max(x => x.Field<int>("ARRAYY"));

            return arrayy.Equals(0) ? (int)Math.Round(dt.AsEnumerable().Max(x => x.Field<double>("Y")), 0) + 5 : arrayy;
        }

        /// <summary>
        /// Defect Manual Type 간 비교
        /// </summary>
        /// <param name="TypeA">DefectManualTable OS</param>
        /// <param name="TypeB">DefectManualTable OS</param>
        /// <returns></returns>
        public static bool IsComparisonDefectManualTable(DefectManualTable TypeA, DefectManualTable TypeB)
            => (    TypeA.INSPECTDATE               == TypeB.INSPECTDATE
                 && TypeA.PRODUCTREVISIONINPUT      == TypeB.PRODUCTREVISIONINPUT
                 && TypeA.MODELPRODUCT              == TypeB.MODELPRODUCT
                 && TypeA.INSPECTIONDEGREEPROCESS   == TypeB.INSPECTIONDEGREEPROCESS
                 && TypeA.LOTID                     == TypeB.LOTID
                 && TypeA.INSPECTIONWORKAREA        == TypeB.INSPECTIONWORKAREA
                 && TypeA.WORKSTARTPCSQTY           == TypeB.WORKSTARTPCSQTY
                 && TypeA.WORKSTARTPANELQTY         == TypeB.WORKSTARTPANELQTY
                 && TypeA.FRONTLAYER                == TypeB.FRONTLAYER
                 && TypeA.BACKLAYER                 == TypeB.BACKLAYER
                 && TypeA.USEDFACTOR                == TypeB.USEDFACTOR
                 && TypeA.DURABLEDEFID              == TypeB.DURABLEDEFID
//               && TypeA.EQUIPMENTNAME             == TypeB.EQUIPMENTNAME
                 && TypeA.DURABLEDEFVERSION         == TypeB.DURABLEDEFVERSION
                 && TypeA.PRODUCTDEFVERSION        == TypeB.PRODUCTDEFVERSION
            );

        /// <summary>
        /// 조회 조건에 Language Type 추가하기
        /// </summary>
        /// <param name="values">조회 조건 Dictionary</param>
        /// <returns></returns>
        public static Dictionary<string, object> AddLanguageTypeToConditions(Dictionary<string, object> values)
        {
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            return values;
        }

        /// <summary>
        /// 연계된 BBT Defect 리스트 가져오기
        /// </summary>
        /// <param name="equip"></param>
        /// <returns></returns>
        public static DataTable GetBBTDefectStateByEquip(string equip)
        {
            Dictionary<string, object> param = new Dictionary<string, object>()
            {
                { "P_EQUIPMENTID", equip }
            };

            return SqlExecuter.Query("GetBBTDefectCodeList",
                                    "10002", AddLanguageTypeToConditions(param));
        }

        /// <summary>
        /// CodeList table에 ClassID로 CODEID/CODENAME 으로 받아오기
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
        public static DataTable GetCodeListByClassID(string classId)
        {
            Dictionary<string, object> param = new Dictionary<string, object>()
            {
                { "CODECLASSID", classId }
            };

            return SqlExecuter.Query("GetCodeList", "00001", AddLanguageTypeToConditions(param));
        }

        /// <summary>
        /// 차수 Combo 설정
        /// </summary>
        /// <param name="degree"></param>
        public static void SetDegree(SmartComboBox combo, string degree)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CODEID", typeof(string));
            dt.Columns.Add("CODENAME", typeof(string));

            combo.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            combo.ShowHeader = false;
            combo.DisplayMember = "CODENAME";
            combo.ValueMember = "CODEID";

            for (int i = 0; i < Format.GetInteger(degree); i++)
            {
                DataRow newRow = dt.NewRow();
                newRow["CODEID"] = i + 1;
                newRow["CODENAME"] = i + 1;
                dt.Rows.Add(newRow);
            }

            combo.DataSource = dt;
            combo.EditValue = degree;
        }

        /// <summary>
        /// Defect Map Grid 설비 타입에 따른 Column 설정
        /// </summary>
        /// <param name="grid">Grid Controler</param>
        /// <param name="type">Equipment Type [AOI/BBT]</param>
        public static void SetGridColumnByEquipmentType(SmartBandedGrid grid, EquipmentType type)
        {
            if (type.Equals(EquipmentType.EQUIPMENTTYPE_AOI))
            {
                grid.View.AddTextBoxColumn("DEFECTGROUP", 80).SetTextAlignment(TextAlignment.Center);
                grid.View.AddTextBoxColumn("DEFECTGROUPCOUNT", 100).SetDisplayFormat("{0:n0}", MaskTypes.Custom).SetTextAlignment(TextAlignment.Right);
                grid.View.AddTextBoxColumn("DEFECTGROUPRATE", 100).SetDisplayFormat("{0:f2}%", MaskTypes.Custom).SetTextAlignment(TextAlignment.Right);
            }

            grid.View.AddTextBoxColumn("DEFECTCODE", 120).SetTextAlignment(TextAlignment.Center);
            grid.View.AddTextBoxColumn("DEFECTCOUNT", 100).SetDisplayFormat("{0:n0}", MaskTypes.Custom).SetTextAlignment(TextAlignment.Right);
            grid.View.AddTextBoxColumn("DEFECTRATE", 100).SetDisplayFormat("{0:f2}%", MaskTypes.Custom).SetTextAlignment(TextAlignment.Right);
            grid.View.AddTextBoxColumn("INSPECTIONQTY").SetTextAlignment(TextAlignment.Right).SetIsHidden();

            grid.View.PopulateColumns();
            grid.View.BestFitColumns();
            grid.View.SetIsReadOnly();

            if (grid.View.Columns["DEFECTGROUP"] is DevExpress.XtraGrid.Columns.GridColumn groupColumn)
            {
                groupColumn.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
            }

            grid.View.Columns["DEFECTCODE"].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
            grid.View.Columns["INSPECTIONQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Max;
            grid.View.Columns["DEFECTCOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grid.View.Columns["DEFECTCOUNT"].SummaryItem.DisplayFormat = "{0:n0}";
            grid.View.Columns["DEFECTRATE"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom;
            grid.View.Columns["DEFECTRATE"].SummaryItem.DisplayFormat = "{0:f2} %";

            grid.View.OptionsView.ShowFooter = true;
            grid.View.FooterPanelHeight = 10;
            grid.ShowStatusBar = false;

            grid.View.OptionsView.AllowCellMerge = true;
            grid.GridButtonItem = GridButtonItem.Export;
            grid.View.OptionsBehavior.Editable = false;
            grid.View.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;

            grid.View.CustomSummaryCalculate += (s, e) => GetCustomSummaryCalculate(s, e);
        }

        /// <summary>
        /// 수율 Top에 Grid Column 설정
        /// </summary>
        /// <param name="grid"></param>
        public static void SetGridColumnByRateLot(SmartBandedGrid grid, EquipmentType type)
        {
            grid.View.AddTextBoxColumn("SUMRATEBYLOT").SetIsHidden(); //chart 그리기 위한 값
            grid.View.AddTextBoxColumn("EVENTTIME", 160).SetTextAlignment(TextAlignment.Left);
            grid.View.AddTextBoxColumn("LOTID", 170).SetTextAlignment(TextAlignment.Left);

            if (type.Equals(EquipmentType.EQUIPMENTTYPE_AOI))
            {
                grid.View.AddTextBoxColumn("DEFECTGROUP", 80).SetTextAlignment(TextAlignment.Left);
            }

            grid.View.AddTextBoxColumn("DEFECTCODE", 120).SetTextAlignment(TextAlignment.Left);
            grid.View.AddTextBoxColumn("DEFECTCOUNT", 80).SetDisplayFormat("{0:n0}", MaskTypes.Custom).SetTextAlignment(TextAlignment.Right);
            grid.View.AddTextBoxColumn("DEFECTRATE", 80).SetDisplayFormat("{0:f2}%", MaskTypes.Custom).SetTextAlignment(TextAlignment.Right);
            grid.View.AddTextBoxColumn("INSPECTIONQTY").SetIsHidden();

            grid.View.PopulateColumns();
            grid.View.BestFitColumns();
            grid.View.SetIsReadOnly();

            if (grid.View.Columns["DEFECTGROUP"] is DevExpress.XtraGrid.Columns.GridColumn groupColumn)
            {
                groupColumn.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
            }

            grid.View.Columns["LOTID"].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
            grid.View.Columns["DEFECTCODE"].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
            grid.View.Columns["INSPECTIONQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Max;
            grid.View.Columns["DEFECTCOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grid.View.Columns["DEFECTCOUNT"].SummaryItem.DisplayFormat = "{0:n0}";
            grid.View.Columns["DEFECTRATE"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grid.View.Columns["DEFECTRATE"].SummaryItem.DisplayFormat = "{0:f2} %";
            //grid.View.Columns["DEFECTRATE"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //grid.View.Columns["DEFECTRATE"].SummaryItem.DisplayFormat = "{0}%";

            grid.View.OptionsView.ShowFooter = true;
            grid.View.FooterPanelHeight = 10;
            grid.ShowStatusBar = false;

            grid.View.OptionsView.AllowCellMerge = true;
            grid.GridButtonItem = GridButtonItem.Export;
            grid.View.OptionsBehavior.Editable = false;
            grid.View.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;

            grid.View.CustomSummaryCalculate += (s, e) => GetCustomSummaryCalculate(s, e);
        }

        /// <summary>
        /// Defect Map Row Data Column 설정
        /// </summary>
        /// <param name="grid">Grid Controler</param>
        public static void SetGridColumnByRowData(SmartBandedGrid grid)
        {
            grid.View.AddTextBoxColumn("SEQ").SetIsHidden();
            grid.View.AddTextBoxColumn("DEFECTNO").SetIsHidden();
            grid.View.AddTextBoxColumn("IMAGE").SetIsHidden();
            grid.View.AddTextBoxColumn("COLOR", 50).SetIsHidden();

            grid.View.AddTextBoxColumn("EQUIPMENTTYPE", 50).SetTextAlignment(TextAlignment.Left);
            grid.View.AddTextBoxColumn("EQUIPMENTID", 150).SetTextAlignment(TextAlignment.Left);
            grid.View.AddTextBoxColumn("LOTID", 200).SetTextAlignment(TextAlignment.Left);
            grid.View.AddTextBoxColumn("PRODUCTDEFID", 100).SetLabel("ITEMNAME").SetTextAlignment(TextAlignment.Left);
            grid.View.AddTextBoxColumn("PRODUCTDEFVERSION", 50).SetTextAlignment(TextAlignment.Left);
            grid.View.AddTextBoxColumn("LAYERID", 50).SetTextAlignment(TextAlignment.Left);
            grid.View.AddTextBoxColumn("DEGREE", 50).SetTextAlignment(TextAlignment.Right);
            grid.View.AddTextBoxColumn("PANELID", 50).SetTextAlignment(TextAlignment.Right);
            grid.View.AddTextBoxColumn("PCSCOUNT", 50).SetLabel("INSPECTIONQTY").SetTextAlignment(TextAlignment.Right);
            grid.View.AddTextBoxColumn("ARRAYX", 50).SetTextAlignment(TextAlignment.Right);
            grid.View.AddTextBoxColumn("ARRAYY", 50).SetTextAlignment(TextAlignment.Right);
            grid.View.AddTextBoxColumn("X", 100).SetTextAlignment(TextAlignment.Right);
            grid.View.AddTextBoxColumn("Y", 100).SetTextAlignment(TextAlignment.Right);
            grid.View.AddTextBoxColumn("DEFECTCODE", 50).SetTextAlignment(TextAlignment.Right);
            grid.View.AddTextBoxColumn("DEFECTTYPE", 50).SetTextAlignment(TextAlignment.Left);
            grid.View.AddTextBoxColumn("AOIDEFECTCODE", 50).SetTextAlignment(TextAlignment.Left);
            grid.View.AddTextBoxColumn("VRSDEFECTCODE", 50).SetTextAlignment(TextAlignment.Left);
            grid.View.AddTextBoxColumn("EVENTTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetTextAlignment(TextAlignment.Left);
            grid.View.AddTextBoxColumn("GROUPCODE", 50).SetTextAlignment(TextAlignment.Left);
            grid.View.AddTextBoxColumn("GROUPNAME", 80).SetTextAlignment(TextAlignment.Left);
            grid.View.AddTextBoxColumn("SUBCODE", 50).SetTextAlignment(TextAlignment.Left);
            grid.View.AddTextBoxColumn("SUBNAME", 80).SetTextAlignment(TextAlignment.Left);
        }

        /// <summary>
        /// 수율 계산 ( value / totla ) * 100
        /// </summary>
        /// <param name="value">점유율</param>
        /// <param name="total">전체</param>
        /// <returns></returns>
        public static double SetDefectRate(int value, int total)
        {
            double rate = Math.Round(Format.GetDouble(value, 0) / Format.GetDouble(total, 0) * 100, 2);

            return double.IsInfinity(rate) ? -1 : rate.Equals(double.NaN) ? 0 : rate;
        }

        /// <summary>
        /// PCS 이미지 가져오기
        /// </summary>
        /// <param name="rowDataTable"></param>
        /// <param name="widht"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static Image GetPcsImage(DataTable rowDataTable, out int widht, out int height)
        {
            Dictionary<string, object> param = new Dictionary<string, object>()
            {
                { "P_PRODUCTDEFID", StringByDataRowObejct(rowDataTable.AsEnumerable().FirstOrDefault(), "PRODUCTDEFID") },
                { "P_PRODUCTDEFVERSION", StringByDataRowObejct(rowDataTable.AsEnumerable().FirstOrDefault(), "PRODUCTDEFVERSION") },
                { "P_VALIDSTATE", "Valid" }
            };

            DataTable dt = SqlExecuter.Query("GetPcsImage", "10001", param);

            if (IsNull(dt) || dt.Rows.Count.Equals(0))
            {
                widht = GetDiagramXSize(rowDataTable);
                height = GetDiagramYSize(rowDataTable);
                return null;
            }

            DataRow dr = dt.Rows[0];

            widht = IntByDataRowObject(dr, "WIDTH", double.NaN) + 5;
            height = IntByDataRowObject(dr, "HEIGHT", double.NaN) + 5;

            try
            {
                string serverPath = StringByDataRowObejct(dr, "FILEPATH");
                string file = string.Join(".", StringByDataRowObejct(dr, "FILENAME"), StringByDataRowObejct(dr, "FILEEXT"));

                return CommonFunction.GetFtpImageFileToBitmap(serverPath, file, widht, height);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// FTP 인터페이스에서 저장한 파일 다운로드
        /// </summary>
        /// <param name="serverPath">파일경로</param>
        /// <param name="file">파일명</param>
        /// <param name="selectedPath">저장할 파일명</param>
        /// <param name="isServerPath">true : yml SerarPath / false : ftpFilePath 포함</param>
        public static void FTPInterfaceFileDownload(string serverPath, string file, string selectedPath, bool isServerPath)
        {
            using (FileStream fileStream = new FileStream(string.Join("\\", selectedPath, file), FileMode.Create))
            {
                byte[] byteFile = CommonFunction.GetFtpImageFileToByte(serverPath, isServerPath);
                fileStream.Write(byteFile, 0, byteFile.Length);
                fileStream.Close();
            }
        }
        /// <summary>
        /// FTP UI에서 저장한 파일 다운로드
        /// </summary>
        /// <param name="serverPath">파일경로</param>
        /// <param name="file">파일명</param>
        /// <param name="selectedPath">저장할 파일명</param>
        /// <param name="isServerPath">true : yml SerarPath / false : ftpFilePath 포함</param>
        public static void FTPUIFileDownload(string serverPath, string file, string selectedPath, bool isServerPath)
        {
            using (FileStream fileStream = new FileStream(string.Join("\\", selectedPath, file), FileMode.Create))
            {
                byte[] byteFile = CommonFunction.GetFtpImageFileToByte(string.Concat(serverPath, file), isServerPath);
                fileStream.Write(byteFile, 0, byteFile.Length);
                fileStream.Close();
            }
        }

        /// <summary>
        /// 리스트 컨트롤에 동일한 Value가 존재하지 않을 때 value를 등록한다
        /// </summary>
        /// <param name="list"></param>
        /// <param name="value"></param>
        public static void SetContainsKeyCheck(List<string> list, string value)
        {
            if (!list.Contains(value))
            {
                list.Add(value);
            }
        }

        /// <summary>
        /// AOI Defect 기준 정보 가져오기
        /// GROUPCODE, GROUPNAME, SUBCODE, SUBNAME, COLOR
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAOIDefectCodeInfo => SqlExecuter.Query("GetAOIDefectCodeList", "10001", AddLanguageTypeToConditions(new Dictionary<string, object> { }));

        /// <summary>
        /// BBT Defect 기준 정보 가져오기
        /// CODEID, CODENAME, COLOR
        /// </summary>
        /// <returns></returns>
        public static DataTable GetBBTDefectCodeInfo => SqlExecuter.Query("GetBBTDefectCodeList", "10001", AddLanguageTypeToConditions(new Dictionary<string, object> { }));

        /// <summary>
        /// 문자열 숫자 정렬한다
        /// </summary>
        /// <param name="str">Sort Key</param>
        /// <returns></returns>
        public static string SortStrNum(string str) => Regex.Replace(str, "[0-9]+", x => x.Value.PadLeft(10, '0'));

        /// <summary>
        /// BBT nRowNum이 4개 미만일 경우에 width 조정하기 위한 함수
        /// </summary>
        /// <param name="rowNum"></param>
        /// <returns></returns>
        public static int GetBBTWidth(int rowNum) => rowNum < 4 ? 8 : rowNum;

        /// <summary>
        /// Null Check
        /// </summary>
        /// <param name="T"></param>
        /// <returns></returns>
        public static bool IsNull(object T) => T == null;

        /// <summary>
        /// DataRow 필드체크 후 Null이 아니면 문자열로 추출한다
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="fildId"></param>
        /// <param name="defaultStr"></param>
        /// <returns></returns>
        public static string StringByDataRowObejct(DataRow dr, string fildId, string defaultStr = "") => Format.GetString(dr.GetObject(fildId), defaultStr);

        /// <summary>
        /// DataRow 필드체크 후 Null이 아니면 Int로 추출한다
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="fildId"></param>
        /// <param name="defaultDouble"></param>
        /// <returns></returns>
        public static int IntByDataRowObject(DataRow dr, string fildId, double defaultDouble = 0) => Convert.ToInt32(Format.GetDouble(dr.GetObject(fildId), defaultDouble));

        /// <summary>
        /// FTP path endswith check
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string FTPFilePathCheck(string filePath) => filePath + ((filePath.EndsWith("/")) ? string.Empty : "/");

        /// <summary>
        /// 측정값이 규격 범위(SPEC)에 포함여부
        /// </summary>
        /// <param name="value"></param>
        /// <param name="USL"></param>
        /// <param name="LSL"></param>
        /// <returns></returns>
        public static string IsMeasureValueCheck(double value, double usl, double lsl) => value >= lsl && value <= usl ? "OK" : "NG";

        /// <summary>
        /// 측정값이 규격 범위(SPEC)에 포함여부
        /// </summary>
        /// <param name="value">측정값</param>
        /// <param name="spce"></param>
        /// <param name="check">true : USL이 Null인 경우, false : LSL이 Null인 경우</param>
        /// <returns></returns>
        public static string IsMeasureValueCheck(double value, double spce, bool check) => check ? (value >= spce ? "OK" : "NG") : (value <= spce ? "OK" : "NG");
    }
}
