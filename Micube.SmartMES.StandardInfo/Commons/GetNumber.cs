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

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프 로 그 램 명  : 테이블레이아웃
    /// 업  무  설  명  : 테이블레이아웃 데이터를 테이블에 등록
    /// 생    성    자  : 윤성원
    /// 생    성    일  : 2019-07-05
    /// 수  정  이  력  : 
    /// </summary>
    /// 
    public class GetNumber
    {
        public GetNumber()
        {

        }
        /// <summary>
        /// 컨트롤 테이블 바인딩
        /// </summary>
        /// <param name="panl">테이블레이아웃판넬</param>

        /// <returns></returns>
        public string GetStdNumber(string sIDCLASSID, string sPREFIX)
        {
            // 채번 시리얼 존재 유무 체크
            Dictionary<string, object> parampf = new Dictionary<string, object>();
            parampf.Add("IDCLASSID", sIDCLASSID);
            parampf.Add("PREFIX", sPREFIX);
            DataTable dtItemserialChk = SqlExecuter.Query("GetProductitemserial", "10001", parampf);

            DataTable dtItemserialI = dtItemserialChk.Clone();
            dtItemserialI.Columns.Add("_STATE_");


            if (dtItemserialChk != null)
            {
                if (dtItemserialChk.Rows.Count == 0)
                {
                    DataRow rowItemserialI = dtItemserialI.NewRow();
                    rowItemserialI["IDCLASSID"] = sIDCLASSID;
                    rowItemserialI["PREFIX"] = sPREFIX;
                    if(sPREFIX == "")
                    {
                        rowItemserialI["LASTSERIALNO"] = "1";
                    }
                    else
                    {
                        rowItemserialI["LASTSERIALNO"] = "00001";
                    }
                    
                    rowItemserialI["_STATE_"] = "added";
                    dtItemserialI.Rows.Add(rowItemserialI);


                }
                else
                {
                    DataRow rowItemserialI = dtItemserialI.NewRow();
                    rowItemserialI["IDCLASSID"] = sIDCLASSID;
                    rowItemserialI["PREFIX"] = sPREFIX;

                    int ilastserialno = 0;
                    ilastserialno = Int32.Parse(dtItemserialChk.Rows[0]["LASTSERIALNO"].ToString());
                    ilastserialno = ilastserialno + 1;

                    if (sPREFIX == "")
                    {
                        rowItemserialI["LASTSERIALNO"] = ilastserialno.ToString();
                    }
                    else
                    {
                        rowItemserialI["LASTSERIALNO"] = ("0000" + ilastserialno.ToString()).Substring(("0000" + ilastserialno.ToString()).Length - 5, 5);
                    }
                        
                    rowItemserialI["_STATE_"] = "modified";
                    dtItemserialI.Rows.Add(rowItemserialI);
                }
            }
            else
            {
                DataRow rowItemserialI = dtItemserialI.NewRow();
                rowItemserialI["IDCLASSID"] = sIDCLASSID;
                rowItemserialI["PREFIX"] = sPREFIX;
                rowItemserialI["LASTSERIALNO"] = "00001";
                rowItemserialI["_STATE_"] = "added";
                dtItemserialI.Rows.Add(rowItemserialI);
            }

            MessageWorker saveWorker = new MessageWorker("Idclassserial");
            saveWorker.SetBody(new MessageBody()
                {
                    { "list", dtItemserialI }
                });

            saveWorker.Execute();
              

            return dtItemserialI.Rows[0]["PREFIX"].ToString() + dtItemserialI.Rows[0]["LASTSERIALNO"].ToString();

        }

        public string GetStdNumber(string sIDCLASSID, string sPREFIX,int icipher)
        {
            // 채번 시리얼 존재 유무 체크
            Dictionary<string, object> parampf = new Dictionary<string, object>();
            parampf.Add("IDCLASSID", sIDCLASSID);
            parampf.Add("PREFIX", sPREFIX);
            DataTable dtItemserialChk = SqlExecuter.Query("GetProductitemserial", "10001", parampf);

            DataTable dtItemserialI = dtItemserialChk.Clone();
            dtItemserialI.Columns.Add("_STATE_");


            if (dtItemserialChk != null)
            {
                if (dtItemserialChk.Rows.Count == 0)
                {
                    DataRow rowItemserialI = dtItemserialI.NewRow();
                    rowItemserialI["IDCLASSID"] = sIDCLASSID;
                    rowItemserialI["PREFIX"] = sPREFIX;
                    if (sPREFIX == "")
                    {
                        rowItemserialI["LASTSERIALNO"] = "1";
                    }
                    else
                    {
                        string sLASTSERIALNO = "0";
                        for (int cipher = 2; cipher < icipher; cipher++)
                        {
                            sLASTSERIALNO = sLASTSERIALNO + "0";
                        }

                        rowItemserialI["LASTSERIALNO"] = sLASTSERIALNO + "1";
                    }

                    rowItemserialI["_STATE_"] = "added";
                    dtItemserialI.Rows.Add(rowItemserialI);


                }
                else
                {
                    DataRow rowItemserialI = dtItemserialI.NewRow();
                    rowItemserialI["IDCLASSID"] = sIDCLASSID;
                    rowItemserialI["PREFIX"] = sPREFIX;

                    int ilastserialno = 0;
                    ilastserialno = Int32.Parse(dtItemserialChk.Rows[0]["LASTSERIALNO"].ToString());
                    ilastserialno = ilastserialno + 1;

                    if (sPREFIX == "")
                    {
                        rowItemserialI["LASTSERIALNO"] = ilastserialno.ToString();
                    }
                    else
                    {
                        string sLASTSERIALNO = "0";
                        for (int cipher = 2; cipher < icipher; cipher++)
                        {
                            sLASTSERIALNO = sLASTSERIALNO + "0";
                        }

                        rowItemserialI["LASTSERIALNO"] = (sLASTSERIALNO + ilastserialno.ToString()).Substring((sLASTSERIALNO + ilastserialno.ToString()).Length - icipher, icipher);
                    }

                    rowItemserialI["_STATE_"] = "modified";
                    dtItemserialI.Rows.Add(rowItemserialI);
                }
            }
            else
            {
                DataRow rowItemserialI = dtItemserialI.NewRow();
                rowItemserialI["IDCLASSID"] = sIDCLASSID;
                rowItemserialI["PREFIX"] = sPREFIX;

                string sLASTSERIALNO = "0";
                for (int cipher = 2; cipher < icipher; cipher++)
                {
                    sLASTSERIALNO = sLASTSERIALNO + "0";
                }

                rowItemserialI["LASTSERIALNO"] = sLASTSERIALNO + "1";
                rowItemserialI["_STATE_"] = "added";
                dtItemserialI.Rows.Add(rowItemserialI);
            }

            MessageWorker saveWorker = new MessageWorker("Idclassserial");
            saveWorker.SetBody(new MessageBody()
                {
                    { "list", dtItemserialI }
                });

            saveWorker.Execute();


            return dtItemserialI.Rows[0]["PREFIX"].ToString() + dtItemserialI.Rows[0]["LASTSERIALNO"].ToString();

        }


    }



}
