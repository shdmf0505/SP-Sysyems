#region using
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


#endregion

namespace Micube.SmartMES.Commons
{
    /// <summary>
    /// 프 로 그 램 명  : 계산식
    /// 업  무  설  명  : 계산식 룰에에 따라 값을 전달
    /// 생    성    자  : 윤성원
    /// 생    성    일  : 2019-07-05
    /// 수  정  이  력  : 
    /// </summary>
    /// 
    public class SetCalculationRule
    {
        Micube.Framework.SmartControls.SmartBandedGrid _grid;
        string _sRULEID = "";
        string[] _areaRULEID;
        string _colValue = "";
        DataTable dtResult = null;
        public SetCalculationRule()
        {

        }

        /// <summary>
        /// 룰타입에 따라 계산로직 변경
        /// </summary>
        /// <param name="sRULEID">배열 룰ID</param>
        /// <param name="grid">그리드</param>

        /// <returns></returns>
        public void SetCalculationRule_grid(string[] areaRULEID, Micube.Framework.SmartControls.SmartBandedGrid grid, string colValue)
        {

            _grid = grid;
            _areaRULEID = areaRULEID;
            _colValue = colValue;
            foreach (string sRULEID in areaRULEID)
            {
                _sRULEID = _sRULEID + "'" + sRULEID + "',";
            }

            _sRULEID = _sRULEID.Substring(0, _sRULEID.Length - 1);

            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            //dicParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            dicParam.Add("RULEID", _sRULEID);
            dtResult = SqlExecuter.Query("GetCalculationRule", "10001", dicParam);

            //현재행 변경
           // _grid.View.ValidateRow += _grid_ValidateRow;
            _grid.View.CellValueChanged += View_CellValueChanged;
         


        }
        
        /// <summary>
        /// 룰타입에 따라 계산로직 변경
        /// </summary>
        /// <param name="sRULEID">배열 룰ID</param>
        /// <param name="grid">그리드</param>

        /// <returns></returns>
        public void SetCalculationRuleType_grid(string RuleType, Micube.Framework.SmartControls.SmartBandedGrid grid, string colValue)
        {

            _grid = grid;
            //_areaRULEID = areaRULEID;
            _colValue = colValue;

            DataRow paramrow = this._grid.View.GetFocusedDataRow();

            //foreach (D sRULEID in paramrow)
            //{
            // _sRULEID = _sRULEID + "'" + sRULEID + "',";
            //}

            _sRULEID = "'" + paramrow["CALCULATIONTYPE"] + "','" + paramrow["FOMULATYPE"] + "'";
            _areaRULEID = _sRULEID.Split(new char[] { ',' });

            //_sRULEID = _sRULEID.Substring(0, _sRULEID.Length - 1);

            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            //dicParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            dicParam.Add("RULEID", _sRULEID);
            dtResult = SqlExecuter.Query("GetCalculationRule", "10001", dicParam);

            //현재행 변경
            // _grid.View.ValidateRow += _grid_ValidateRow;
            _grid.View.CellValueChanged += View_CellValueChanged;



        }


        private void View_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            DataRow paramrow = this._grid.View.GetFocusedDataRow();

            if (paramrow[_colValue].ToString() != "Y")
            {
                // 수정된 행이 존재 할경우 로직 실행
                DataRow[] rowChk = dtResult.Select("CODEID = '" + _grid.View.FocusedColumn.FieldName + "'");

                if (rowChk.Length != 0)
                {

                    foreach (string sRULEID in _areaRULEID)
                    {
                        foreach (DataRow row in dtResult.DefaultView.ToTable(true, new string[] { "RULEID", "TARGETATTRIBUTE" }).Select("RULEID = '" + sRULEID.Replace("'","") + "'"))
                        {
                            String sAdd = "";


                            foreach (DataRow rowAdd in dtResult.Select("TARGETATTRIBUTE = '" + row["TARGETATTRIBUTE"].ToString() + "'", "SEQUENCE"))
                            {
                                String scodeid = "";
                                if (rowAdd["CODEID"].ToString() != "")
                                {
                                    if (paramrow.Table.Columns.IndexOf(rowAdd["CODEID"].ToString()) != -1)
                                    {
                                        if (string.IsNullOrEmpty(paramrow[rowAdd["CODEID"].ToString()].ToString()))
                                        {
                                            scodeid = "0";
                                        }
                                        else
                                        {
                                            scodeid = paramrow[rowAdd["CODEID"].ToString()].ToString();
                                        }

                                    }
                                    else
                                    {
                                        if (string.IsNullOrEmpty(rowAdd["CODEID"].ToString()))
                                        {
                                            scodeid = "0";
                                        }
                                        else
                                        {
                                            scodeid = rowAdd["CODEID"].ToString();
                                        }

                                    }

                                }

                                sAdd = sAdd + rowAdd["FRONTBRACKET"].ToString() + scodeid + rowAdd["OPERATOR"].ToString() + rowAdd["BACKBRACKET"].ToString();
                                sAdd = sAdd.Replace(",", "");
                            }

                            foreach (DataColumn col in paramrow.Table.Columns)
                            {
                                sAdd = sAdd.Replace(col.ColumnName, paramrow[col.ColumnName].ToString());

                                if (col.ColumnName == row["TARGETATTRIBUTE"].ToString())
                                {
                                    if (col.ColumnName == "SUPPLEMENTQTY")
                                    {
                                        string[] splitStr = sAdd.Split('/');

                                        if (splitStr[1].Equals("0"))
                                        {
                                            return;
                                        }
                                    }

                                    DataTable dtCalulation = new DataTable();
                                    paramrow[col.ColumnName] = dtCalulation.Compute(sAdd, "").ToString();
                                }

                            }

                        }
                    }



                }
            }
        }

        public  Decimal SetCalculationRule_decimal(string sRULEID, Micube.Framework.SmartControls.SmartBandedGrid grid)
        {

            _grid = grid;
            _sRULEID = sRULEID;
          
            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            //dicParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            dicParam.Add("RULEID", _sRULEID);
            dtResult = SqlExecuter.Query("GetCalculationRule", "10001", dicParam);

            

            DataRow[] rowChk = dtResult.Select("CODEID = '" + _grid.View.FocusedColumn.FieldName + "'");

            if (rowChk.Length != 0)
            {
                String sAdd = "";
                DataRow paramrow = this._grid.View.GetFocusedDataRow();
                foreach (DataRow rowAdd in dtResult.Rows)
                {
                    String scodeid = "";
                    if (paramrow[rowAdd["CODEID"].ToString()].ToString() == "")
                    {
                        scodeid = "0";
                    }
                    else
                    {
                        scodeid = paramrow[rowAdd["CODEID"].ToString()].ToString();
                    }


                    sAdd = sAdd + rowAdd["FRONTBRACKET"].ToString() + scodeid + rowAdd["OPERATOR"].ToString() + rowAdd["BACKBRACKET"].ToString();
                }
                DataTable dtCalulation = new DataTable();
                return decimal.Parse(dtCalulation.Compute(sAdd, "").ToString());
            }
           

            return 0;

           
        }
     

        private void _grid_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            DataRow paramrow = this._grid.View.GetFocusedDataRow();
  
            if (paramrow[_colValue].ToString() != "Y")
            {
                // 수정된 행이 존재 할경우 로직 실행
                DataRow[] rowChk = dtResult.Select("CODEID = '" + _grid.View.FocusedColumn.FieldName + "'");

                if (rowChk.Length != 0)
                {

                    foreach (string sRULEID in _areaRULEID)
                    {
                        foreach (DataRow row in dtResult.DefaultView.ToTable(true, new string[] { "RULEID", "TARGETATTRIBUTE" }).Select("RULEID = '" + sRULEID + "'"))
                        {
                            String sAdd = "";
                           

                            foreach (DataRow rowAdd in dtResult.Select("TARGETATTRIBUTE = '" + row["TARGETATTRIBUTE"].ToString() + "'", "SEQUENCE"))
                            {
                                String scodeid = "";
                                if (rowAdd["CODEID"].ToString() != "")
                                {
                                    if (paramrow.Table.Columns.IndexOf(rowAdd["CODEID"].ToString()) != -1)
                                    {
                                        if (string.IsNullOrEmpty(paramrow[rowAdd["CODEID"].ToString()].ToString()))
                                        {
                                            scodeid = "0";
                                        }
                                        else
                                        {
                                            scodeid = paramrow[rowAdd["CODEID"].ToString()].ToString();
                                        }
                                        
                                    }
                                    else
                                    { 
                                        if (string.IsNullOrEmpty(rowAdd["CODEID"].ToString()))
                                        {
                                            scodeid = "0";
                                        }
                                        else
                                        {
                                            scodeid = rowAdd["CODEID"].ToString();
                                        }
                                      
                                    }

                                }
                                sAdd = sAdd + rowAdd["FRONTBRACKET"].ToString() + scodeid + rowAdd["OPERATOR"].ToString() + rowAdd["BACKBRACKET"].ToString();
                            }

                            foreach (DataColumn col in paramrow.Table.Columns)
                            {
                                sAdd = sAdd.Replace(col.ColumnName, paramrow[col.ColumnName].ToString());

                                if (col.ColumnName == row["TARGETATTRIBUTE"].ToString())
                                {
                                    DataTable dtCalulation = new DataTable();
                                    paramrow[col.ColumnName] = dtCalulation.Compute(sAdd, "").ToString();
                                }

                            }

                        }
                    }



                }
            }
        }
    }
}
