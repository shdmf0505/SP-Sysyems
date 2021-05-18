using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Micube.SmartMES.Commons.SPCLibrary
{
    /// <summary>
    /// SPC Library Test Data.
    /// </summary>
    public class SPCTestData
    {
        /// <summary>
        /// I-MR Test Data
        /// </summary>
        /// <returns></returns>
        public string[,] IMR01()
        {
            string[,] pdata = new string[5, 11];
            pdata[1, 1] = "1"; pdata[2, 1] = "OP3#@E01#@JS04#@C9"; pdata[3, 1] = "OP3#@E01#@JS04#@C9#@1005002"; pdata[4, 1] = "45.099998";
            pdata[1, 2] = "1"; pdata[2, 2] = "OP3#@E01#@JS04#@C9"; pdata[3, 2] = "OP3#@E01#@JS04#@C9#@1005003"; pdata[4, 2] = "44.700001";
            pdata[1, 3] = "1"; pdata[2, 3] = "OP3#@E01#@JS04#@C9"; pdata[3, 3] = "OP3#@E01#@JS04#@C9#@1005006"; pdata[4, 3] = "44.799999";
            pdata[1, 4] = "1"; pdata[2, 4] = "OP3#@E01#@JS04#@C9"; pdata[3, 4] = "OP3#@E01#@JS04#@C9#@1005007"; pdata[4, 4] = "45.200001";
            pdata[1, 5] = "1"; pdata[2, 5] = "OP3#@E01#@JS04#@C9"; pdata[3, 5] = "OP3#@E01#@JS04#@C9#@1013002"; pdata[4, 5] = "44.799999";
            pdata[1, 6] = "1"; pdata[2, 6] = "OP3#@E01#@JS04#@C9"; pdata[3, 6] = "OP3#@E01#@JS04#@C9#@1013003"; pdata[4, 6] = "44.500000";
            pdata[1, 7] = "1"; pdata[2, 7] = "OP3#@E01#@JS04#@C9"; pdata[3, 7] = "OP3#@E01#@JS04#@C9#@1013005"; pdata[4, 7] = "45.099998";
            pdata[1, 8] = "1"; pdata[2, 8] = "OP3#@E01#@JS04#@C9"; pdata[3, 8] = "OP3#@E01#@JS04#@C9#@1013006"; pdata[4, 8] = "44.799999";
            pdata[1, 9] = "1"; pdata[2, 9] = "OP3#@E01#@JS04#@C9"; pdata[3, 9] = "OP3#@E01#@JS04#@C9#@1013007"; pdata[4, 9] = "45.599998";
            pdata[1, 10] = "1"; pdata[2, 10] = "OP3#@E01#@JS04#@C9"; pdata[3, 10] = "OP3#@E01#@JS04#@C9#@1013008"; pdata[4, 10] = "44.799999";
            return pdata;
        }

        /// <summary>
        /// /// CD Test Data
        /// </summary>
        /// <returns></returns>
        public string[,] Cd01()
        {
            string[,] pdata = new string[5, 19];
            pdata[1, 01] = "1"; pdata[2, 01] = "CL07"; pdata[3, 01] = "20170206LOT001"; pdata[4, 01] = "1.000"; //subgroup 1
            pdata[1, 02] = "1"; pdata[2, 02] = "CL07"; pdata[3, 02] = "20170206LOT001"; pdata[4, 02] = "2.000";
            pdata[1, 03] = "1"; pdata[2, 03] = "CL07"; pdata[3, 03] = "20170206LOT001"; pdata[4, 03] = "3.000";
            pdata[1, 04] = "1"; pdata[2, 04] = "CL07"; pdata[3, 04] = "20170206LOT001"; pdata[4, 04] = "4.000";
            pdata[1, 05] = "1"; pdata[2, 05] = "CL07"; pdata[3, 05] = "20170206LOT001"; pdata[4, 05] = "6.000";
            pdata[1, 06] = "1"; pdata[2, 06] = "CL07"; pdata[3, 06] = "20170206LOT001"; pdata[4, 06] = "4.000";
            pdata[1, 07] = "1"; pdata[2, 07] = "CL07"; pdata[3, 07] = "20170207LOT002"; pdata[4, 07] = "3.000"; //subgroup 2
            pdata[1, 08] = "1"; pdata[2, 08] = "CL07"; pdata[3, 08] = "20170207LOT002"; pdata[4, 08] = "4.000";
            pdata[1, 09] = "1"; pdata[2, 09] = "CL07"; pdata[3, 09] = "20170207LOT002"; pdata[4, 09] = "5.000";
            pdata[1, 10] = "1"; pdata[2, 10] = "CL07"; pdata[3, 10] = "20170207LOT002"; pdata[4, 10] = "4.000";
            pdata[1, 11] = "1"; pdata[2, 11] = "CL07"; pdata[3, 11] = "20170207LOT002"; pdata[4, 11] = "3.000";
            pdata[1, 12] = "1"; pdata[2, 12] = "CL07"; pdata[3, 12] = "20170207LOT002"; pdata[4, 12] = "4.000";
            pdata[1, 13] = "1"; pdata[2, 13] = "CL07"; pdata[3, 13] = "20170208LOT003"; pdata[4, 13] = "5.000"; //subgroup 3
            pdata[1, 14] = "1"; pdata[2, 14] = "CL07"; pdata[3, 14] = "20170208LOT003"; pdata[4, 14] = "6.000";
            pdata[1, 15] = "1"; pdata[2, 15] = "CL07"; pdata[3, 15] = "20170208LOT003"; pdata[4, 15] = "3.000";
            pdata[1, 16] = "1"; pdata[2, 16] = "CL07"; pdata[3, 16] = "20170208LOT003"; pdata[4, 16] = "4.000";
            pdata[1, 17] = "1"; pdata[2, 17] = "CL07"; pdata[3, 17] = "20170208LOT003"; pdata[4, 17] = "3.000";
            pdata[1, 18] = "1"; pdata[2, 18] = "CL07"; pdata[3, 18] = "20170208LOT003"; pdata[4, 18] = "4.000";
            return pdata;
        }

        /// <summary>
        /// R, S Test Data
        /// </summary>
        /// <returns></returns>
        public string[,] CTR01()
        {
            string[,] pdata = new string[5, 19];
            pdata[1, 01] = "1"; pdata[2, 01] = "CL07"; pdata[3, 01] = "LOTYYYYMMDD01"; pdata[4, 01] = "1.000"; //subgroup 1
            pdata[1, 02] = "1"; pdata[2, 02] = "CL07"; pdata[3, 02] = "LOTYYYYMMDD01"; pdata[4, 02] = "2.000";
            pdata[1, 03] = "1"; pdata[2, 03] = "CL07"; pdata[3, 03] = "LOTYYYYMMDD01"; pdata[4, 03] = "3.000";
            pdata[1, 04] = "1"; pdata[2, 04] = "CL07"; pdata[3, 04] = "LOTYYYYMMDD01"; pdata[4, 04] = "4.000";
            pdata[1, 05] = "1"; pdata[2, 05] = "CL07"; pdata[3, 05] = "LOTYYYYMMDD01"; pdata[4, 05] = "6.000";
            pdata[1, 06] = "1"; pdata[2, 06] = "CL07"; pdata[3, 06] = "LOTYYYYMMDD01"; pdata[4, 06] = "4.000";

            pdata[1, 07] = "1"; pdata[2, 07] = "CL07"; pdata[3, 07] = "LOTYYYYMMDD02"; pdata[4, 07] = "3.000"; //subgroup 2
            pdata[1, 08] = "1"; pdata[2, 08] = "CL07"; pdata[3, 08] = "LOTYYYYMMDD02"; pdata[4, 08] = "4.000";
            pdata[1, 09] = "1"; pdata[2, 09] = "CL07"; pdata[3, 09] = "LOTYYYYMMDD02"; pdata[4, 09] = "5.000";
            pdata[1, 10] = "1"; pdata[2, 10] = "CL07"; pdata[3, 10] = "LOTYYYYMMDD02"; pdata[4, 10] = "4.000";
            pdata[1, 11] = "1"; pdata[2, 11] = "CL07"; pdata[3, 11] = "LOTYYYYMMDD02"; pdata[4, 11] = "3.000";
            pdata[1, 12] = "1"; pdata[2, 12] = "CL07"; pdata[3, 12] = "LOTYYYYMMDD02"; pdata[4, 12] = "4.000";

            pdata[1, 13] = "1"; pdata[2, 13] = "CL07"; pdata[3, 13] = "LOTYYYYMMDD03"; pdata[4, 13] = "5.000"; //subgroup 3
            pdata[1, 14] = "1"; pdata[2, 14] = "CL07"; pdata[3, 14] = "LOTYYYYMMDD03"; pdata[4, 14] = "4.000";
            pdata[1, 15] = "1"; pdata[2, 15] = "CL07"; pdata[3, 15] = "LOTYYYYMMDD03"; pdata[4, 15] = "3.000";
            pdata[1, 16] = "1"; pdata[2, 16] = "CL07"; pdata[3, 16] = "LOTYYYYMMDD03"; pdata[4, 16] = "4.000";
            pdata[1, 17] = "1"; pdata[2, 17] = "CL07"; pdata[3, 17] = "LOTYYYYMMDD03"; pdata[4, 17] = "5.000";
            pdata[1, 18] = "1"; pdata[2, 18] = "CL07"; pdata[3, 18] = "LOTYYYYMMDD03"; pdata[4, 18] = "4.000";
            return pdata;
        }

        /// <summary>
        /// R, S Test Data - Sampling 같지 않음.
        /// </summary>
        /// <returns></returns>
        public string[,] CTR01Not()
        {
            string[,] pdata = new string[5, 6];
            pdata[1, 01] = "1"; pdata[2, 01] = "CL07"; pdata[3, 01] = "LOTYYYYMMDD01"; pdata[4, 01] = "2.000"; //subgroup 1
            pdata[1, 02] = "1"; pdata[2, 02] = "CL07"; pdata[3, 02] = "LOTYYYYMMDD01"; pdata[4, 02] = "3.000";
            pdata[1, 03] = "1"; pdata[2, 03] = "CL07"; pdata[3, 03] = "LOTYYYYMMDD01"; pdata[4, 03] = "4.000";

            pdata[1, 04] = "1"; pdata[2, 04] = "CL07"; pdata[3, 04] = "LOTYYYYMMDD02"; pdata[4, 04] = "3.000";
            pdata[1, 05] = "1"; pdata[2, 05] = "CL07"; pdata[3, 05] = "LOTYYYYMMDD02"; pdata[4, 05] = "5.000";
            return pdata;
        }

        public string[,] CTR01Digit()
        {
            string[,] pdata = new string[5, 6];
            pdata[1, 01] = "1"; pdata[2, 01] = "CL07"; pdata[3, 01] = "LOTYYYYMMDD01"; pdata[4, 01] = "0.004"; //subgroup 1
            pdata[1, 02] = "1"; pdata[2, 02] = "CL07"; pdata[3, 02] = "LOTYYYYMMDD01"; pdata[4, 02] = "0.003";
            pdata[1, 03] = "1"; pdata[2, 03] = "CL07"; pdata[3, 03] = "LOTYYYYMMDD01"; pdata[4, 03] = "0.001";

            pdata[1, 04] = "1"; pdata[2, 04] = "CL07"; pdata[3, 04] = "LOTYYYYMMDD02"; pdata[4, 04] = "0.003";
            pdata[1, 05] = "1"; pdata[2, 05] = "CL07"; pdata[3, 05] = "LOTYYYYMMDD02"; pdata[4, 05] = "0.002";
            return pdata;
        }

        /// <summary>
        /// R, S Test Data - Sampling 같지 않음.
        /// </summary>
        /// <returns></returns>
        public string[,] CTR01_ErrMode()
        {
            //string[,] pdata = new string[5, 6];
            //string[,] pdata = new string[5, 4];
            string[,] pdata = new string[5, 5];
            pdata[1, 01] = "1"; pdata[2, 01] = "CL07"; pdata[3, 01] = "LOTYYYYMMDD01"; pdata[4, 01] = "2.000"; //subgroup 1
            pdata[1, 02] = "1"; pdata[2, 02] = "CL07"; pdata[3, 02] = "LOTYYYYMMDD01"; pdata[4, 02] = "2.000";
            pdata[1, 03] = "1"; pdata[2, 03] = "CL07"; pdata[3, 03] = "LOTYYYYMMDD01"; pdata[4, 03] = "3.000";

            pdata[1, 04] = "1"; pdata[2, 04] = "CL07"; pdata[3, 04] = "LOTYYYYMMDD02"; pdata[4, 04] = "3.000";
            //pdata[1, 05] = "1"; pdata[2, 05] = "CL07"; pdata[3, 05] = "LOTYYYYMMDD02"; pdata[4, 05] = "5.000";
            return pdata;
        }


        /// <summary>
        /// R, S Test Data - Sampling 같지 않음.
        /// </summary>
        /// <returns></returns>
        public string[,] CTR02Not_Multi()
        {
            string[,] pdata = new string[5, 26];
            pdata[1, 01] = "1"; pdata[2, 01] = "CL07"; pdata[3, 01] = "LOTYYYYMMDD01"; pdata[4, 01] = "2.000"; //subgroup 1
            pdata[1, 02] = "1"; pdata[2, 02] = "CL07"; pdata[3, 02] = "LOTYYYYMMDD01"; pdata[4, 02] = "3.000";
            pdata[1, 03] = "1"; pdata[2, 03] = "CL07"; pdata[3, 03] = "LOTYYYYMMDD01"; pdata[4, 03] = "4.000";
            pdata[1, 04] = "1"; pdata[2, 04] = "CL07"; pdata[3, 04] = "LOTYYYYMMDD02"; pdata[4, 04] = "3.000";
            pdata[1, 05] = "1"; pdata[2, 05] = "CL07"; pdata[3, 05] = "LOTYYYYMMDD02"; pdata[4, 05] = "5.000";
            pdata[1, 06] = "1"; pdata[2, 06] = "DL08"; pdata[3, 06] = "LOTYYYYMMDD01"; pdata[4, 06] = "3.000"; //subgroup 2
            pdata[1, 07] = "1"; pdata[2, 07] = "DL08"; pdata[3, 07] = "LOTYYYYMMDD01"; pdata[4, 07] = "4.000";
            pdata[1, 08] = "1"; pdata[2, 08] = "DL08"; pdata[3, 08] = "LOTYYYYMMDD01"; pdata[4, 08] = "5.000";
            pdata[1, 09] = "1"; pdata[2, 09] = "DL08"; pdata[3, 09] = "LOTYYYYMMDD02"; pdata[4, 09] = "4.000";
            pdata[1, 10] = "1"; pdata[2, 10] = "DL08"; pdata[3, 10] = "LOTYYYYMMDD02"; pdata[4, 10] = "6.000";
            pdata[1, 11] = "1"; pdata[2, 11] = "DL08"; pdata[3, 11] = "LOTYYYYMMDD03"; pdata[4, 11] = "2.000";
            pdata[1, 12] = "1"; pdata[2, 12] = "DL08"; pdata[3, 12] = "LOTYYYYMMDD03"; pdata[4, 12] = "5.000";
            pdata[1, 13] = "1"; pdata[2, 13] = "DL08"; pdata[3, 13] = "LOTYYYYMMDD03"; pdata[4, 13] = "4.000";
            pdata[1, 14] = "1"; pdata[2, 14] = "DL08"; pdata[3, 14] = "LOTYYYYMMDD03"; pdata[4, 14] = "3.000";
            pdata[1, 15] = "1"; pdata[2, 15] = "EL09"; pdata[3, 15] = "LOTYYYYMMDD01"; pdata[4, 15] = "3.000"; //subgroup 3
            pdata[1, 16] = "1"; pdata[2, 16] = "EL09"; pdata[3, 16] = "LOTYYYYMMDD01"; pdata[4, 16] = "5.000";
            pdata[1, 17] = "1"; pdata[2, 17] = "EL09"; pdata[3, 17] = "LOTYYYYMMDD01"; pdata[4, 17] = "4.000";
            pdata[1, 18] = "1"; pdata[2, 18] = "EL09"; pdata[3, 18] = "LOTYYYYMMDD02"; pdata[4, 18] = "3.000";
            pdata[1, 19] = "1"; pdata[2, 19] = "EL09"; pdata[3, 19] = "LOTYYYYMMDD02"; pdata[4, 19] = "5.000";
            pdata[1, 20] = "1"; pdata[2, 20] = "EL09"; pdata[3, 20] = "LOTYYYYMMDD02"; pdata[4, 20] = "6.000";
            pdata[1, 21] = "1"; pdata[2, 21] = "EL09"; pdata[3, 21] = "LOTYYYYMMDD03"; pdata[4, 21] = "3.000";
            pdata[1, 22] = "1"; pdata[2, 22] = "EL09"; pdata[3, 22] = "LOTYYYYMMDD03"; pdata[4, 22] = "3.000";
            pdata[1, 23] = "1"; pdata[2, 23] = "EL09"; pdata[3, 23] = "LOTYYYYMMDD03"; pdata[4, 23] = "4.000";
            pdata[1, 24] = "1"; pdata[2, 24] = "EL09"; pdata[3, 24] = "LOTYYYYMMDD03"; pdata[4, 24] = "4.000";
            pdata[1, 25] = "1"; pdata[2, 25] = "EL09"; pdata[3, 25] = "LOTYYYYMMDD03"; pdata[4, 25] = "5.000";
            return pdata;
        }

        /// <summary>
        /// R, S Test Data - Sampling 혼합 않음.
        /// </summary>
        /// <returns></returns>
        public string[,] CTR03Not_Multi()
        {
            string[,] pdata = new string[5, 26];
            pdata[1, 01] = "1"; pdata[2, 01] = "CL07"; pdata[3, 01] = "LOTYYYYMMDD01"; pdata[4, 01] = "2.000"; //subgroup 1
            pdata[1, 02] = "1"; pdata[2, 02] = "CL07"; pdata[3, 02] = "LOTYYYYMMDD01"; pdata[4, 02] = "3.000";
            pdata[1, 03] = "1"; pdata[2, 03] = "CL07"; pdata[3, 03] = "LOTYYYYMMDD01"; pdata[4, 03] = "4.000";
            pdata[1, 04] = "1"; pdata[2, 04] = "CL07"; pdata[3, 04] = "LOTYYYYMMDD02"; pdata[4, 04] = "3.000";
            pdata[1, 05] = "1"; pdata[2, 05] = "CL07"; pdata[3, 05] = "LOTYYYYMMDD02"; pdata[4, 05] = "5.000";
            pdata[1, 06] = "1"; pdata[2, 06] = "DL08"; pdata[3, 06] = "LOTYYYYMMDD01"; pdata[4, 06] = "3.000"; //subgroup 2
            pdata[1, 07] = "1"; pdata[2, 07] = "DL08"; pdata[3, 07] = "LOTYYYYMMDD01"; pdata[4, 07] = "4.000";
            pdata[1, 08] = "1"; pdata[2, 08] = "DL08"; pdata[3, 08] = "LOTYYYYMMDD01"; pdata[4, 08] = "5.000";
            pdata[1, 09] = "1"; pdata[2, 09] = "DL08"; pdata[3, 09] = "LOTYYYYMMDD02"; pdata[4, 09] = "4.000";
            pdata[1, 10] = "1"; pdata[2, 10] = "DL08"; pdata[3, 10] = "LOTYYYYMMDD02"; pdata[4, 10] = "6.000";
            pdata[1, 11] = "1"; pdata[2, 11] = "DL08"; pdata[3, 11] = "LOTYYYYMMDD02"; pdata[4, 11] = "2.000";
            pdata[1, 12] = "1"; pdata[2, 12] = "DL08"; pdata[3, 12] = "LOTYYYYMMDD03"; pdata[4, 12] = "5.000";
            pdata[1, 13] = "1"; pdata[2, 13] = "DL08"; pdata[3, 13] = "LOTYYYYMMDD03"; pdata[4, 13] = "4.000";
            pdata[1, 14] = "1"; pdata[2, 14] = "DL08"; pdata[3, 14] = "LOTYYYYMMDD03"; pdata[4, 14] = "3.000";
            pdata[1, 15] = "1"; pdata[2, 15] = "EL09"; pdata[3, 15] = "LOTYYYYMMDD01"; pdata[4, 15] = "3.000"; //subgroup 3
            pdata[1, 16] = "1"; pdata[2, 16] = "EL09"; pdata[3, 16] = "LOTYYYYMMDD01"; pdata[4, 16] = "5.000";
            pdata[1, 17] = "1"; pdata[2, 17] = "EL09"; pdata[3, 17] = "LOTYYYYMMDD01"; pdata[4, 17] = "4.000";
            pdata[1, 18] = "1"; pdata[2, 18] = "EL09"; pdata[3, 18] = "LOTYYYYMMDD02"; pdata[4, 18] = "3.000";
            pdata[1, 19] = "1"; pdata[2, 19] = "EL09"; pdata[3, 19] = "LOTYYYYMMDD02"; pdata[4, 19] = "5.000";
            pdata[1, 20] = "1"; pdata[2, 20] = "EL09"; pdata[3, 20] = "LOTYYYYMMDD02"; pdata[4, 20] = "6.000";
            pdata[1, 21] = "1"; pdata[2, 21] = "EL09"; pdata[3, 21] = "LOTYYYYMMDD03"; pdata[4, 21] = "3.000";
            pdata[1, 22] = "1"; pdata[2, 22] = "EL09"; pdata[3, 22] = "LOTYYYYMMDD03"; pdata[4, 22] = "3.000";
            pdata[1, 23] = "1"; pdata[2, 23] = "EL09"; pdata[3, 23] = "LOTYYYYMMDD03"; pdata[4, 23] = "4.000";
            pdata[1, 24] = "1"; pdata[2, 24] = "EL09"; pdata[3, 24] = "LOTYYYYMMDD03"; pdata[4, 24] = "4.000";
            pdata[1, 25] = "1"; pdata[2, 25] = "EL09"; pdata[3, 25] = "LOTYYYYMMDD03"; pdata[4, 25] = "5.000";
            return pdata;
        }
        #region Rule Check Test Data
        /// <summary>
        /// Nelson rules 1번 Rule - Test Data (R, S)
        /// </summary>
        /// <returns></returns>
        public string[,] TestDataNelsonRules01()
        {
            //1번 Rule - 한 점은 평균에서 3 개 이상의 표준 편차입니다.
            string[,] pdata = new string[5, 43];
            pdata[1, 01] = "1"; pdata[2, 01] = "CL07"; pdata[3, 01] = "LOTYYYYMMDD01"; pdata[4, 01] = "1.000"; //subgroup 1
            pdata[1, 02] = "1"; pdata[2, 02] = "CL07"; pdata[3, 02] = "LOTYYYYMMDD01"; pdata[4, 02] = "1.500";
            pdata[1, 03] = "1"; pdata[2, 03] = "CL07"; pdata[3, 03] = "LOTYYYYMMDD01"; pdata[4, 03] = "3.000";
            pdata[1, 04] = "1"; pdata[2, 04] = "CL07"; pdata[3, 04] = "LOTYYYYMMDD02"; pdata[4, 04] = "9.000"; //subgroup 2
            pdata[1, 05] = "1"; pdata[2, 05] = "CL07"; pdata[3, 05] = "LOTYYYYMMDD02"; pdata[4, 05] = "4.000";
            pdata[1, 06] = "1"; pdata[2, 06] = "CL07"; pdata[3, 06] = "LOTYYYYMMDD02"; pdata[4, 06] = "5.000";
            pdata[1, 07] = "1"; pdata[2, 07] = "CL07"; pdata[3, 07] = "LOTYYYYMMDD03"; pdata[4, 07] = "2.000"; //subgroup 3
            pdata[1, 08] = "1"; pdata[2, 08] = "CL07"; pdata[3, 08] = "LOTYYYYMMDD03"; pdata[4, 08] = "1.500";
            pdata[1, 09] = "1"; pdata[2, 09] = "CL07"; pdata[3, 09] = "LOTYYYYMMDD03"; pdata[4, 09] = "1.100";
            pdata[1, 10] = "1"; pdata[2, 10] = "CL07"; pdata[3, 10] = "LOTYYYYMMDD04"; pdata[4, 10] = "6.000"; //subgroup 4
            pdata[1, 11] = "1"; pdata[2, 11] = "CL07"; pdata[3, 11] = "LOTYYYYMMDD04"; pdata[4, 11] = "4.000";
            pdata[1, 12] = "1"; pdata[2, 12] = "CL07"; pdata[3, 12] = "LOTYYYYMMDD04"; pdata[4, 12] = "3.000";
            pdata[1, 13] = "1"; pdata[2, 13] = "CL07"; pdata[3, 13] = "LOTYYYYMMDD05"; pdata[4, 13] = "7.000"; //subgroup 5
            pdata[1, 14] = "1"; pdata[2, 14] = "CL07"; pdata[3, 14] = "LOTYYYYMMDD05"; pdata[4, 14] = "4.000";
            pdata[1, 15] = "1"; pdata[2, 15] = "CL07"; pdata[3, 15] = "LOTYYYYMMDD05"; pdata[4, 15] = "3.000";
            pdata[1, 16] = "1"; pdata[2, 16] = "CL07"; pdata[3, 16] = "LOTYYYYMMDD06"; pdata[4, 16] = "8.000"; //subgroup 6
            pdata[1, 17] = "1"; pdata[2, 17] = "CL07"; pdata[3, 17] = "LOTYYYYMMDD06"; pdata[4, 17] = "5.000";
            pdata[1, 18] = "1"; pdata[2, 18] = "CL07"; pdata[3, 18] = "LOTYYYYMMDD06"; pdata[4, 18] = "4.000";
            pdata[1, 19] = "1"; pdata[2, 19] = "CL07"; pdata[3, 19] = "LOTYYYYMMDD07"; pdata[4, 19] = "7.000"; //subgroup 7
            pdata[1, 20] = "1"; pdata[2, 20] = "CL07"; pdata[3, 20] = "LOTYYYYMMDD07"; pdata[4, 20] = "4.000";
            pdata[1, 21] = "1"; pdata[2, 21] = "CL07"; pdata[3, 21] = "LOTYYYYMMDD07"; pdata[4, 21] = "5.000";
            pdata[1, 22] = "1"; pdata[2, 22] = "CL07"; pdata[3, 22] = "LOTYYYYMMDD08"; pdata[4, 22] = "8.000"; //subgroup 8
            pdata[1, 23] = "1"; pdata[2, 23] = "CL07"; pdata[3, 23] = "LOTYYYYMMDD08"; pdata[4, 23] = "4.000";
            pdata[1, 24] = "1"; pdata[2, 24] = "CL07"; pdata[3, 24] = "LOTYYYYMMDD08"; pdata[4, 24] = "3.000";
            pdata[1, 25] = "1"; pdata[2, 25] = "CL07"; pdata[3, 25] = "LOTYYYYMMDD09"; pdata[4, 25] = "8.000"; //subgroup 9
            pdata[1, 26] = "1"; pdata[2, 26] = "CL07"; pdata[3, 26] = "LOTYYYYMMDD09"; pdata[4, 26] = "4.000";
            pdata[1, 27] = "1"; pdata[2, 27] = "CL07"; pdata[3, 27] = "LOTYYYYMMDD09"; pdata[4, 27] = "3.000";
            pdata[1, 28] = "1"; pdata[2, 28] = "CL07"; pdata[3, 28] = "LOTYYYYMMDD10"; pdata[4, 28] = "5.000"; //subgroup 10
            pdata[1, 29] = "1"; pdata[2, 29] = "CL07"; pdata[3, 29] = "LOTYYYYMMDD10"; pdata[4, 29] = "6.500";
            pdata[1, 30] = "1"; pdata[2, 30] = "CL07"; pdata[3, 30] = "LOTYYYYMMDD10"; pdata[4, 30] = "7.300";
            pdata[1, 31] = "1"; pdata[2, 31] = "CL07"; pdata[3, 31] = "LOTYYYYMMDD11"; pdata[4, 31] = "5.000"; //subgroup 11
            pdata[1, 32] = "1"; pdata[2, 32] = "CL07"; pdata[3, 32] = "LOTYYYYMMDD11"; pdata[4, 32] = "4.500";
            pdata[1, 33] = "1"; pdata[2, 33] = "CL07"; pdata[3, 33] = "LOTYYYYMMDD11"; pdata[4, 33] = "5.300";
            pdata[1, 34] = "1"; pdata[2, 34] = "CL07"; pdata[3, 34] = "LOTYYYYMMDD12"; pdata[4, 34] = "6.000"; //subgroup 12
            pdata[1, 35] = "1"; pdata[2, 35] = "CL07"; pdata[3, 35] = "LOTYYYYMMDD12"; pdata[4, 35] = "7.500";
            pdata[1, 36] = "1"; pdata[2, 36] = "CL07"; pdata[3, 36] = "LOTYYYYMMDD12"; pdata[4, 36] = "5.300";
            pdata[1, 37] = "1"; pdata[2, 37] = "CL07"; pdata[3, 37] = "LOTYYYYMMDD13"; pdata[4, 37] = "1.000"; //subgroup 13
            pdata[1, 38] = "1"; pdata[2, 38] = "CL07"; pdata[3, 38] = "LOTYYYYMMDD13"; pdata[4, 38] = "1.500";
            pdata[1, 39] = "1"; pdata[2, 39] = "CL07"; pdata[3, 39] = "LOTYYYYMMDD13"; pdata[4, 39] = "1.300";
            pdata[1, 40] = "1"; pdata[2, 40] = "CL07"; pdata[3, 40] = "LOTYYYYMMDD14"; pdata[4, 40] = "1.000"; //subgroup 14
            pdata[1, 41] = "1"; pdata[2, 41] = "CL07"; pdata[3, 41] = "LOTYYYYMMDD14"; pdata[4, 41] = "1.500";
            pdata[1, 42] = "1"; pdata[2, 42] = "CL07"; pdata[3, 42] = "LOTYYYYMMDD14"; pdata[4, 42] = "1.300";

            return pdata;
        }
        /// <summary>
        /// Nelson rules 1번 Rule - Test Data (R, S)
        /// </summary>
        /// <returns></returns>
        public string[,] TestDataNelsonRules01_01()
        {
            string[,] pdata = new string[5, 57 + 1];
            pdata[1, 01] = "1"; pdata[2, 01] = "CL07"; pdata[3, 01] = "LOTYYYYMMDD01"; pdata[4, 01] = "0.100"; //subgroup 0
            pdata[1, 02] = "1"; pdata[2, 02] = "CL07"; pdata[3, 02] = "LOTYYYYMMDD01"; pdata[4, 02] = "0.200";
            pdata[1, 03] = "1"; pdata[2, 03] = "CL07"; pdata[3, 03] = "LOTYYYYMMDD01"; pdata[4, 03] = "0.300";
            pdata[1, 04] = "1"; pdata[2, 04] = "CL07"; pdata[3, 04] = "LOTYYYYMMDD02"; pdata[4, 04] = "1.100"; //subgroup 1
            pdata[1, 05] = "1"; pdata[2, 05] = "CL07"; pdata[3, 05] = "LOTYYYYMMDD02"; pdata[4, 05] = "2.500";
            pdata[1, 06] = "1"; pdata[2, 06] = "CL07"; pdata[3, 06] = "LOTYYYYMMDD02"; pdata[4, 06] = "2.300";
            pdata[1, 07] = "1"; pdata[2, 07] = "CL07"; pdata[3, 07] = "LOTYYYYMMDD03"; pdata[4, 07] = "1.300"; //subgroup 2
            pdata[1, 08] = "1"; pdata[2, 08] = "CL07"; pdata[3, 08] = "LOTYYYYMMDD03"; pdata[4, 08] = "2.500";
            pdata[1, 09] = "1"; pdata[2, 09] = "CL07"; pdata[3, 09] = "LOTYYYYMMDD03"; pdata[4, 09] = "3.100";
            pdata[1, 10] = "1"; pdata[2, 10] = "CL07"; pdata[3, 10] = "LOTYYYYMMDD04"; pdata[4, 10] = "4.100"; //subgroup 3 --> 1
            pdata[1, 11] = "1"; pdata[2, 11] = "CL07"; pdata[3, 11] = "LOTYYYYMMDD04"; pdata[4, 11] = "3.200";
            pdata[1, 12] = "1"; pdata[2, 12] = "CL07"; pdata[3, 12] = "LOTYYYYMMDD04"; pdata[4, 12] = "1.300";
            pdata[1, 13] = "1"; pdata[2, 13] = "CL07"; pdata[3, 13] = "LOTYYYYMMDD05"; pdata[4, 13] = "9.500";//subgroup 4 --> 1
            pdata[1, 14] = "1"; pdata[2, 14] = "CL07"; pdata[3, 14] = "LOTYYYYMMDD05"; pdata[4, 14] = "5.500";
            pdata[1, 15] = "1"; pdata[2, 15] = "CL07"; pdata[3, 15] = "LOTYYYYMMDD05"; pdata[4, 15] = "2.300";
            pdata[1, 16] = "1"; pdata[2, 16] = "CL07"; pdata[3, 16] = "LOTYYYYMMDD06"; pdata[4, 16] = "7.600"; //subgroup 5 --> 1
            pdata[1, 17] = "1"; pdata[2, 17] = "CL07"; pdata[3, 17] = "LOTYYYYMMDD06"; pdata[4, 17] = "4.500";
            pdata[1, 18] = "1"; pdata[2, 18] = "CL07"; pdata[3, 18] = "LOTYYYYMMDD06"; pdata[4, 18] = "8.100";
            pdata[1, 19] = "1"; pdata[2, 19] = "CL07"; pdata[3, 19] = "LOTYYYYMMDD07"; pdata[4, 19] = "1.100"; //subgroup 6 --> 1
            pdata[1, 20] = "1"; pdata[2, 20] = "CL07"; pdata[3, 20] = "LOTYYYYMMDD07"; pdata[4, 20] = "1.500";
            pdata[1, 21] = "1"; pdata[2, 21] = "CL07"; pdata[3, 21] = "LOTYYYYMMDD07"; pdata[4, 21] = "0.500";
            pdata[1, 22] = "1"; pdata[2, 22] = "CL07"; pdata[3, 22] = "LOTYYYYMMDD08"; pdata[4, 22] = "1.500"; //subgroup 7 --> 1
            pdata[1, 23] = "1"; pdata[2, 23] = "CL07"; pdata[3, 23] = "LOTYYYYMMDD08"; pdata[4, 23] = "4.400";
            pdata[1, 24] = "1"; pdata[2, 24] = "CL07"; pdata[3, 24] = "LOTYYYYMMDD08"; pdata[4, 24] = "3.300";
            pdata[1, 25] = "1"; pdata[2, 25] = "CL07"; pdata[3, 25] = "LOTYYYYMMDD09"; pdata[4, 25] = "2.800"; //subgroup 8 --> 1
            pdata[1, 26] = "1"; pdata[2, 26] = "CL07"; pdata[3, 26] = "LOTYYYYMMDD09"; pdata[4, 26] = "0.400";
            pdata[1, 27] = "1"; pdata[2, 27] = "CL07"; pdata[3, 27] = "LOTYYYYMMDD09"; pdata[4, 27] = "2.300";
            pdata[1, 28] = "1"; pdata[2, 28] = "CL07"; pdata[3, 28] = "LOTYYYYMMDD10"; pdata[4, 28] = "5.500"; //subgroup 9 --> 1
            pdata[1, 29] = "1"; pdata[2, 29] = "CL07"; pdata[3, 29] = "LOTYYYYMMDD10"; pdata[4, 29] = "2.600";
            pdata[1, 30] = "1"; pdata[2, 30] = "CL07"; pdata[3, 30] = "LOTYYYYMMDD10"; pdata[4, 30] = "1.700";
            pdata[1, 31] = "1"; pdata[2, 31] = "CL07"; pdata[3, 31] = "LOTYYYYMMDD11"; pdata[4, 31] = "0.100"; //subgroup 10 --> 1
            pdata[1, 32] = "1"; pdata[2, 32] = "CL07"; pdata[3, 32] = "LOTYYYYMMDD11"; pdata[4, 32] = "0.300";
            pdata[1, 33] = "1"; pdata[2, 33] = "CL07"; pdata[3, 33] = "LOTYYYYMMDD11"; pdata[4, 33] = "0.300";
            pdata[1, 34] = "1"; pdata[2, 34] = "CL07"; pdata[3, 34] = "LOTYYYYMMDD12"; pdata[4, 34] = "5.600"; //subgroup 11 --> 1
            pdata[1, 35] = "1"; pdata[2, 35] = "CL07"; pdata[3, 35] = "LOTYYYYMMDD12"; pdata[4, 35] = "2.700";
            pdata[1, 36] = "1"; pdata[2, 36] = "CL07"; pdata[3, 36] = "LOTYYYYMMDD12"; pdata[4, 36] = "3.000";
            pdata[1, 37] = "1"; pdata[2, 37] = "CL07"; pdata[3, 37] = "LOTYYYYMMDD13"; pdata[4, 37] = "2.100"; //subgroup 12 --> 1
            pdata[1, 38] = "1"; pdata[2, 38] = "CL07"; pdata[3, 38] = "LOTYYYYMMDD13"; pdata[4, 38] = "2.100";
            pdata[1, 39] = "1"; pdata[2, 39] = "CL07"; pdata[3, 39] = "LOTYYYYMMDD13"; pdata[4, 39] = "3.100";
            pdata[1, 40] = "1"; pdata[2, 40] = "CL07"; pdata[3, 40] = "LOTYYYYMMDD14"; pdata[4, 40] = "0.100"; //subgroup 13 --> 1
            pdata[1, 41] = "1"; pdata[2, 41] = "CL07"; pdata[3, 41] = "LOTYYYYMMDD14"; pdata[4, 41] = "0.200";
            pdata[1, 42] = "1"; pdata[2, 42] = "CL07"; pdata[3, 42] = "LOTYYYYMMDD14"; pdata[4, 42] = "0.300";
            pdata[1, 43] = "1"; pdata[2, 43] = "CL07"; pdata[3, 43] = "LOTYYYYMMDD15"; pdata[4, 43] = "1.600"; //subgroup 14 --> 1
            pdata[1, 44] = "1"; pdata[2, 44] = "CL07"; pdata[3, 44] = "LOTYYYYMMDD15"; pdata[4, 44] = "2.700";
            pdata[1, 45] = "1"; pdata[2, 45] = "CL07"; pdata[3, 45] = "LOTYYYYMMDD15"; pdata[4, 45] = "1.500";
            pdata[1, 46] = "1"; pdata[2, 46] = "CL07"; pdata[3, 46] = "LOTYYYYMMDD16"; pdata[4, 46] = "3.100"; //subgroup 15 --> 1
            pdata[1, 47] = "1"; pdata[2, 47] = "CL07"; pdata[3, 47] = "LOTYYYYMMDD16"; pdata[4, 47] = "3.100";
            pdata[1, 48] = "1"; pdata[2, 48] = "CL07"; pdata[3, 48] = "LOTYYYYMMDD16"; pdata[4, 48] = "1.100";
            pdata[1, 49] = "1"; pdata[2, 49] = "CL07"; pdata[3, 49] = "LOTYYYYMMDD17"; pdata[4, 49] = "1.100"; //subgroup 16 --> 1
            pdata[1, 50] = "1"; pdata[2, 50] = "CL07"; pdata[3, 50] = "LOTYYYYMMDD17"; pdata[4, 50] = "2.100";
            pdata[1, 51] = "1"; pdata[2, 51] = "CL07"; pdata[3, 51] = "LOTYYYYMMDD17"; pdata[4, 51] = "5.100";
            pdata[1, 52] = "1"; pdata[2, 52] = "CL07"; pdata[3, 52] = "LOTYYYYMMDD18"; pdata[4, 52] = "2.100"; //subgroup 17 --> 1
            pdata[1, 53] = "1"; pdata[2, 53] = "CL07"; pdata[3, 53] = "LOTYYYYMMDD18"; pdata[4, 53] = "2.100";
            pdata[1, 54] = "1"; pdata[2, 54] = "CL07"; pdata[3, 54] = "LOTYYYYMMDD18"; pdata[4, 54] = "1.100";
            pdata[1, 55] = "1"; pdata[2, 55] = "CL07"; pdata[3, 55] = "LOTYYYYMMDD19"; pdata[4, 55] = "6.100"; //subgroup 18 --> X
            pdata[1, 56] = "1"; pdata[2, 56] = "CL07"; pdata[3, 56] = "LOTYYYYMMDD19"; pdata[4, 56] = "5.100";
            pdata[1, 57] = "1"; pdata[2, 57] = "CL07"; pdata[3, 57] = "LOTYYYYMMDD19"; pdata[4, 57] = "5.100";

            return pdata;
        }
        /// <summary>
        /// Nelson rules 2번 Rule - Test Data (R, S) 상한
        /// </summary>
        /// <returns></returns>
        public string[,] TestDataNelsonRules02_Max()
        {
            //2번 Rule - 행에서 9 개 이상의 점이 평균의 같은쪽에 있습니다.(일부 장기적인 편견 이 존재합니다.)
            string[,] pdata = new string[5, 43];
            pdata[1, 01] = "1"; pdata[2, 01] = "CL07"; pdata[3, 01] = "LOTYYYYMMDD01"; pdata[4, 01] = "1.000"; //subgroup 1
            pdata[1, 02] = "1"; pdata[2, 02] = "CL07"; pdata[3, 02] = "LOTYYYYMMDD01"; pdata[4, 02] = "1.500";
            pdata[1, 03] = "1"; pdata[2, 03] = "CL07"; pdata[3, 03] = "LOTYYYYMMDD01"; pdata[4, 03] = "3.000";
            pdata[1, 04] = "1"; pdata[2, 04] = "CL07"; pdata[3, 04] = "LOTYYYYMMDD02"; pdata[4, 04] = "9.000"; //subgroup 2
            pdata[1, 05] = "1"; pdata[2, 05] = "CL07"; pdata[3, 05] = "LOTYYYYMMDD02"; pdata[4, 05] = "4.000";
            pdata[1, 06] = "1"; pdata[2, 06] = "CL07"; pdata[3, 06] = "LOTYYYYMMDD02"; pdata[4, 06] = "5.000";
            pdata[1, 07] = "1"; pdata[2, 07] = "CL07"; pdata[3, 07] = "LOTYYYYMMDD03"; pdata[4, 07] = "2.000"; //subgroup 3
            pdata[1, 08] = "1"; pdata[2, 08] = "CL07"; pdata[3, 08] = "LOTYYYYMMDD03"; pdata[4, 08] = "1.500";
            pdata[1, 09] = "1"; pdata[2, 09] = "CL07"; pdata[3, 09] = "LOTYYYYMMDD03"; pdata[4, 09] = "1.100";
            pdata[1, 10] = "1"; pdata[2, 10] = "CL07"; pdata[3, 10] = "LOTYYYYMMDD04"; pdata[4, 10] = "6.000"; //subgroup 4
            pdata[1, 11] = "1"; pdata[2, 11] = "CL07"; pdata[3, 11] = "LOTYYYYMMDD04"; pdata[4, 11] = "4.000";
            pdata[1, 12] = "1"; pdata[2, 12] = "CL07"; pdata[3, 12] = "LOTYYYYMMDD04"; pdata[4, 12] = "3.000";
            pdata[1, 13] = "1"; pdata[2, 13] = "CL07"; pdata[3, 13] = "LOTYYYYMMDD05"; pdata[4, 13] = "7.000"; //subgroup 5
            pdata[1, 14] = "1"; pdata[2, 14] = "CL07"; pdata[3, 14] = "LOTYYYYMMDD05"; pdata[4, 14] = "4.000";
            pdata[1, 15] = "1"; pdata[2, 15] = "CL07"; pdata[3, 15] = "LOTYYYYMMDD05"; pdata[4, 15] = "3.000";
            pdata[1, 16] = "1"; pdata[2, 16] = "CL07"; pdata[3, 16] = "LOTYYYYMMDD06"; pdata[4, 16] = "8.000"; //subgroup 6
            pdata[1, 17] = "1"; pdata[2, 17] = "CL07"; pdata[3, 17] = "LOTYYYYMMDD06"; pdata[4, 17] = "5.000";
            pdata[1, 18] = "1"; pdata[2, 18] = "CL07"; pdata[3, 18] = "LOTYYYYMMDD06"; pdata[4, 18] = "4.000";
            pdata[1, 19] = "1"; pdata[2, 19] = "CL07"; pdata[3, 19] = "LOTYYYYMMDD07"; pdata[4, 19] = "7.000"; //subgroup 7
            pdata[1, 20] = "1"; pdata[2, 20] = "CL07"; pdata[3, 20] = "LOTYYYYMMDD07"; pdata[4, 20] = "4.000";
            pdata[1, 21] = "1"; pdata[2, 21] = "CL07"; pdata[3, 21] = "LOTYYYYMMDD07"; pdata[4, 21] = "5.000";
            pdata[1, 22] = "1"; pdata[2, 22] = "CL07"; pdata[3, 22] = "LOTYYYYMMDD08"; pdata[4, 22] = "8.000"; //subgroup 8
            pdata[1, 23] = "1"; pdata[2, 23] = "CL07"; pdata[3, 23] = "LOTYYYYMMDD08"; pdata[4, 23] = "4.000";
            pdata[1, 24] = "1"; pdata[2, 24] = "CL07"; pdata[3, 24] = "LOTYYYYMMDD08"; pdata[4, 24] = "3.000";
            pdata[1, 25] = "1"; pdata[2, 25] = "CL07"; pdata[3, 25] = "LOTYYYYMMDD09"; pdata[4, 25] = "8.000"; //subgroup 9
            pdata[1, 26] = "1"; pdata[2, 26] = "CL07"; pdata[3, 26] = "LOTYYYYMMDD09"; pdata[4, 26] = "4.000";
            pdata[1, 27] = "1"; pdata[2, 27] = "CL07"; pdata[3, 27] = "LOTYYYYMMDD09"; pdata[4, 27] = "3.000";
            pdata[1, 28] = "1"; pdata[2, 28] = "CL07"; pdata[3, 28] = "LOTYYYYMMDD10"; pdata[4, 28] = "5.000"; //subgroup 10
            pdata[1, 29] = "1"; pdata[2, 29] = "CL07"; pdata[3, 29] = "LOTYYYYMMDD10"; pdata[4, 29] = "6.500";
            pdata[1, 30] = "1"; pdata[2, 30] = "CL07"; pdata[3, 30] = "LOTYYYYMMDD10"; pdata[4, 30] = "7.300";
            pdata[1, 31] = "1"; pdata[2, 31] = "CL07"; pdata[3, 31] = "LOTYYYYMMDD11"; pdata[4, 31] = "5.000"; //subgroup 11
            pdata[1, 32] = "1"; pdata[2, 32] = "CL07"; pdata[3, 32] = "LOTYYYYMMDD11"; pdata[4, 32] = "4.500";
            pdata[1, 33] = "1"; pdata[2, 33] = "CL07"; pdata[3, 33] = "LOTYYYYMMDD11"; pdata[4, 33] = "5.300";
            pdata[1, 34] = "1"; pdata[2, 34] = "CL07"; pdata[3, 34] = "LOTYYYYMMDD12"; pdata[4, 34] = "6.000"; //subgroup 12
            pdata[1, 35] = "1"; pdata[2, 35] = "CL07"; pdata[3, 35] = "LOTYYYYMMDD12"; pdata[4, 35] = "7.500";
            pdata[1, 36] = "1"; pdata[2, 36] = "CL07"; pdata[3, 36] = "LOTYYYYMMDD12"; pdata[4, 36] = "5.300";
            pdata[1, 37] = "1"; pdata[2, 37] = "CL07"; pdata[3, 37] = "LOTYYYYMMDD13"; pdata[4, 37] = "1.000"; //subgroup 13
            pdata[1, 38] = "1"; pdata[2, 38] = "CL07"; pdata[3, 38] = "LOTYYYYMMDD13"; pdata[4, 38] = "1.500";
            pdata[1, 39] = "1"; pdata[2, 39] = "CL07"; pdata[3, 39] = "LOTYYYYMMDD13"; pdata[4, 39] = "1.300";
            pdata[1, 40] = "1"; pdata[2, 40] = "CL07"; pdata[3, 40] = "LOTYYYYMMDD14"; pdata[4, 40] = "1.000"; //subgroup 14
            pdata[1, 41] = "1"; pdata[2, 41] = "CL07"; pdata[3, 41] = "LOTYYYYMMDD14"; pdata[4, 41] = "1.500";
            pdata[1, 42] = "1"; pdata[2, 42] = "CL07"; pdata[3, 42] = "LOTYYYYMMDD14"; pdata[4, 42] = "1.300";

            return pdata;
        }
        /// <summary>
        /// Nelson rules 2번 Rule - Test Data (R, S) 하한
        /// </summary>
        /// <returns></returns>
        public string[,] TestDataNelsonRules02_Min()
        {
            //2번 Rule - 행에서 9 개 이상의 점이 평균의 같은쪽에 있습니다.(일부 장기적인 편견 이 존재합니다.)
            string[,] pdata = new string[5, 43];
            pdata[1, 01] = "1"; pdata[2, 01] = "CL07"; pdata[3, 01] = "LOTYYYYMMDD01"; pdata[4, 01] = "1.000"; //subgroup 1
            pdata[1, 02] = "1"; pdata[2, 02] = "CL07"; pdata[3, 02] = "LOTYYYYMMDD01"; pdata[4, 02] = "1.500";
            pdata[1, 03] = "1"; pdata[2, 03] = "CL07"; pdata[3, 03] = "LOTYYYYMMDD01"; pdata[4, 03] = "3.000";
            pdata[1, 04] = "1"; pdata[2, 04] = "CL07"; pdata[3, 04] = "LOTYYYYMMDD02"; pdata[4, 04] = "9.000"; //subgroup 2
            pdata[1, 05] = "1"; pdata[2, 05] = "CL07"; pdata[3, 05] = "LOTYYYYMMDD02"; pdata[4, 05] = "4.000";
            pdata[1, 06] = "1"; pdata[2, 06] = "CL07"; pdata[3, 06] = "LOTYYYYMMDD02"; pdata[4, 06] = "5.000";
            pdata[1, 07] = "1"; pdata[2, 07] = "CL07"; pdata[3, 07] = "LOTYYYYMMDD03"; pdata[4, 07] = "8.000"; //subgroup 3
            pdata[1, 08] = "1"; pdata[2, 08] = "CL07"; pdata[3, 08] = "LOTYYYYMMDD03"; pdata[4, 08] = "4.500";
            pdata[1, 09] = "1"; pdata[2, 09] = "CL07"; pdata[3, 09] = "LOTYYYYMMDD03"; pdata[4, 09] = "5.100";
            pdata[1, 10] = "1"; pdata[2, 10] = "CL07"; pdata[3, 10] = "LOTYYYYMMDD04"; pdata[4, 10] = "3.000"; //subgroup 4
            pdata[1, 11] = "1"; pdata[2, 11] = "CL07"; pdata[3, 11] = "LOTYYYYMMDD04"; pdata[4, 11] = "4.000";
            pdata[1, 12] = "1"; pdata[2, 12] = "CL07"; pdata[3, 12] = "LOTYYYYMMDD04"; pdata[4, 12] = "3.000";
            pdata[1, 13] = "1"; pdata[2, 13] = "CL07"; pdata[3, 13] = "LOTYYYYMMDD05"; pdata[4, 13] = "1.000"; //subgroup 5
            pdata[1, 14] = "1"; pdata[2, 14] = "CL07"; pdata[3, 14] = "LOTYYYYMMDD05"; pdata[4, 14] = "4.000";
            pdata[1, 15] = "1"; pdata[2, 15] = "CL07"; pdata[3, 15] = "LOTYYYYMMDD05"; pdata[4, 15] = "3.000";
            pdata[1, 16] = "1"; pdata[2, 16] = "CL07"; pdata[3, 16] = "LOTYYYYMMDD06"; pdata[4, 16] = "1.000"; //subgroup 6
            pdata[1, 17] = "1"; pdata[2, 17] = "CL07"; pdata[3, 17] = "LOTYYYYMMDD06"; pdata[4, 17] = "5.000";
            pdata[1, 18] = "1"; pdata[2, 18] = "CL07"; pdata[3, 18] = "LOTYYYYMMDD06"; pdata[4, 18] = "4.000";
            pdata[1, 19] = "1"; pdata[2, 19] = "CL07"; pdata[3, 19] = "LOTYYYYMMDD07"; pdata[4, 19] = "1.000"; //subgroup 7
            pdata[1, 20] = "1"; pdata[2, 20] = "CL07"; pdata[3, 20] = "LOTYYYYMMDD07"; pdata[4, 20] = "4.000";
            pdata[1, 21] = "1"; pdata[2, 21] = "CL07"; pdata[3, 21] = "LOTYYYYMMDD07"; pdata[4, 21] = "5.000";
            pdata[1, 22] = "1"; pdata[2, 22] = "CL07"; pdata[3, 22] = "LOTYYYYMMDD08"; pdata[4, 22] = "1.000"; //subgroup 8
            pdata[1, 23] = "1"; pdata[2, 23] = "CL07"; pdata[3, 23] = "LOTYYYYMMDD08"; pdata[4, 23] = "4.000";
            pdata[1, 24] = "1"; pdata[2, 24] = "CL07"; pdata[3, 24] = "LOTYYYYMMDD08"; pdata[4, 24] = "3.000";
            pdata[1, 25] = "1"; pdata[2, 25] = "CL07"; pdata[3, 25] = "LOTYYYYMMDD09"; pdata[4, 25] = "1.000"; //subgroup 9
            pdata[1, 26] = "1"; pdata[2, 26] = "CL07"; pdata[3, 26] = "LOTYYYYMMDD09"; pdata[4, 26] = "4.000";
            pdata[1, 27] = "1"; pdata[2, 27] = "CL07"; pdata[3, 27] = "LOTYYYYMMDD09"; pdata[4, 27] = "3.000";
            pdata[1, 28] = "1"; pdata[2, 28] = "CL07"; pdata[3, 28] = "LOTYYYYMMDD10"; pdata[4, 28] = "5.000"; //subgroup 10
            pdata[1, 29] = "1"; pdata[2, 29] = "CL07"; pdata[3, 29] = "LOTYYYYMMDD10"; pdata[4, 29] = "2.500";
            pdata[1, 30] = "1"; pdata[2, 30] = "CL07"; pdata[3, 30] = "LOTYYYYMMDD10"; pdata[4, 30] = "1.300";
            pdata[1, 31] = "1"; pdata[2, 31] = "CL07"; pdata[3, 31] = "LOTYYYYMMDD11"; pdata[4, 31] = "2.000"; //subgroup 11
            pdata[1, 32] = "1"; pdata[2, 32] = "CL07"; pdata[3, 32] = "LOTYYYYMMDD11"; pdata[4, 32] = "4.500";
            pdata[1, 33] = "1"; pdata[2, 33] = "CL07"; pdata[3, 33] = "LOTYYYYMMDD11"; pdata[4, 33] = "3.300";
            pdata[1, 34] = "1"; pdata[2, 34] = "CL07"; pdata[3, 34] = "LOTYYYYMMDD12"; pdata[4, 34] = "1.000"; //subgroup 12
            pdata[1, 35] = "1"; pdata[2, 35] = "CL07"; pdata[3, 35] = "LOTYYYYMMDD12"; pdata[4, 35] = "2.500";
            pdata[1, 36] = "1"; pdata[2, 36] = "CL07"; pdata[3, 36] = "LOTYYYYMMDD12"; pdata[4, 36] = "3.300";
            pdata[1, 37] = "1"; pdata[2, 37] = "CL07"; pdata[3, 37] = "LOTYYYYMMDD13"; pdata[4, 37] = "8.000"; //subgroup 13
            pdata[1, 38] = "1"; pdata[2, 38] = "CL07"; pdata[3, 38] = "LOTYYYYMMDD13"; pdata[4, 38] = "4.500";
            pdata[1, 39] = "1"; pdata[2, 39] = "CL07"; pdata[3, 39] = "LOTYYYYMMDD13"; pdata[4, 39] = "1.300";
            pdata[1, 40] = "1"; pdata[2, 40] = "CL07"; pdata[3, 40] = "LOTYYYYMMDD14"; pdata[4, 40] = "9.000"; //subgroup 14
            pdata[1, 41] = "1"; pdata[2, 41] = "CL07"; pdata[3, 41] = "LOTYYYYMMDD14"; pdata[4, 41] = "1.500";
            pdata[1, 42] = "1"; pdata[2, 42] = "CL07"; pdata[3, 42] = "LOTYYYYMMDD14"; pdata[4, 42] = "1.300";

            return pdata;
        }

        /// <summary>
        /// Nelson rules 2번 Rule - Test Data (R, S)
        /// </summary>
        /// <returns></returns>
        public string[,] TestDataNelsonRules02_01()
        {
            //2번 Rule - 행에서 9 개 이상의 점이 평균의 같은쪽에 있습니다.(일부 장기적인 편견 이 존재합니다.)
            string[,] pdata = new string[5, 43];
            pdata[1, 01] = "1"; pdata[2, 01] = "CL07"; pdata[3, 01] = "LOTYYYYMMDD01"; pdata[4, 01] = "1.000"; //subgroup 1
            pdata[1, 02] = "1"; pdata[2, 02] = "CL07"; pdata[3, 02] = "LOTYYYYMMDD01"; pdata[4, 02] = "1.500";
            pdata[1, 03] = "1"; pdata[2, 03] = "CL07"; pdata[3, 03] = "LOTYYYYMMDD01"; pdata[4, 03] = "3.000";

            pdata[1, 04] = "1"; pdata[2, 04] = "CL07"; pdata[3, 04] = "LOTYYYYMMDD02"; pdata[4, 04] = "9.000"; //subgroup 2
            pdata[1, 05] = "1"; pdata[2, 05] = "CL07"; pdata[3, 05] = "LOTYYYYMMDD02"; pdata[4, 05] = "4.000";
            pdata[1, 06] = "1"; pdata[2, 06] = "CL07"; pdata[3, 06] = "LOTYYYYMMDD02"; pdata[4, 06] = "5.000";

            pdata[1, 07] = "1"; pdata[2, 07] = "CL07"; pdata[3, 07] = "LOTYYYYMMDD03"; pdata[4, 07] = "2.000"; //subgroup 3
            pdata[1, 08] = "1"; pdata[2, 08] = "CL07"; pdata[3, 08] = "LOTYYYYMMDD03"; pdata[4, 08] = "1.500";
            pdata[1, 09] = "1"; pdata[2, 09] = "CL07"; pdata[3, 09] = "LOTYYYYMMDD03"; pdata[4, 09] = "1.100";

            pdata[1, 10] = "1"; pdata[2, 10] = "CL07"; pdata[3, 10] = "LOTYYYYMMDD04"; pdata[4, 10] = "6.000"; //subgroup 4
            pdata[1, 11] = "1"; pdata[2, 11] = "CL07"; pdata[3, 11] = "LOTYYYYMMDD04"; pdata[4, 11] = "4.000";
            pdata[1, 12] = "1"; pdata[2, 12] = "CL07"; pdata[3, 12] = "LOTYYYYMMDD04"; pdata[4, 12] = "3.000";
            pdata[1, 13] = "1"; pdata[2, 13] = "CL07"; pdata[3, 13] = "LOTYYYYMMDD05"; pdata[4, 13] = "7.000"; //subgroup 5
            pdata[1, 14] = "1"; pdata[2, 14] = "CL07"; pdata[3, 14] = "LOTYYYYMMDD05"; pdata[4, 14] = "4.000";
            pdata[1, 15] = "1"; pdata[2, 15] = "CL07"; pdata[3, 15] = "LOTYYYYMMDD05"; pdata[4, 15] = "3.000";
            pdata[1, 16] = "1"; pdata[2, 16] = "CL07"; pdata[3, 16] = "LOTYYYYMMDD06"; pdata[4, 16] = "8.000"; //subgroup 6
            pdata[1, 17] = "1"; pdata[2, 17] = "CL07"; pdata[3, 17] = "LOTYYYYMMDD06"; pdata[4, 17] = "5.000";
            pdata[1, 18] = "1"; pdata[2, 18] = "CL07"; pdata[3, 18] = "LOTYYYYMMDD06"; pdata[4, 18] = "4.000";
            pdata[1, 19] = "1"; pdata[2, 19] = "CL07"; pdata[3, 19] = "LOTYYYYMMDD07"; pdata[4, 19] = "7.000"; //subgroup 7
            pdata[1, 20] = "1"; pdata[2, 20] = "CL07"; pdata[3, 20] = "LOTYYYYMMDD07"; pdata[4, 20] = "4.000";
            pdata[1, 21] = "1"; pdata[2, 21] = "CL07"; pdata[3, 21] = "LOTYYYYMMDD07"; pdata[4, 21] = "5.000";
            pdata[1, 22] = "1"; pdata[2, 22] = "CL07"; pdata[3, 22] = "LOTYYYYMMDD08"; pdata[4, 22] = "8.000"; //subgroup 8
            pdata[1, 23] = "1"; pdata[2, 23] = "CL07"; pdata[3, 23] = "LOTYYYYMMDD08"; pdata[4, 23] = "4.000";
            pdata[1, 24] = "1"; pdata[2, 24] = "CL07"; pdata[3, 24] = "LOTYYYYMMDD08"; pdata[4, 24] = "3.000";
            pdata[1, 25] = "1"; pdata[2, 25] = "CL07"; pdata[3, 25] = "LOTYYYYMMDD09"; pdata[4, 25] = "8.000"; //subgroup 9
            pdata[1, 26] = "1"; pdata[2, 26] = "CL07"; pdata[3, 26] = "LOTYYYYMMDD09"; pdata[4, 26] = "4.000";
            pdata[1, 27] = "1"; pdata[2, 27] = "CL07"; pdata[3, 27] = "LOTYYYYMMDD09"; pdata[4, 27] = "3.000";
            pdata[1, 28] = "1"; pdata[2, 28] = "CL07"; pdata[3, 28] = "LOTYYYYMMDD10"; pdata[4, 28] = "5.000"; //subgroup 10
            pdata[1, 29] = "1"; pdata[2, 29] = "CL07"; pdata[3, 29] = "LOTYYYYMMDD10"; pdata[4, 29] = "6.500";
            pdata[1, 30] = "1"; pdata[2, 30] = "CL07"; pdata[3, 30] = "LOTYYYYMMDD10"; pdata[4, 30] = "7.300";
            pdata[1, 31] = "1"; pdata[2, 31] = "CL07"; pdata[3, 31] = "LOTYYYYMMDD11"; pdata[4, 31] = "5.000"; //subgroup 11
            pdata[1, 32] = "1"; pdata[2, 32] = "CL07"; pdata[3, 32] = "LOTYYYYMMDD11"; pdata[4, 32] = "4.500";
            pdata[1, 33] = "1"; pdata[2, 33] = "CL07"; pdata[3, 33] = "LOTYYYYMMDD11"; pdata[4, 33] = "5.300";
            pdata[1, 34] = "1"; pdata[2, 34] = "CL07"; pdata[3, 34] = "LOTYYYYMMDD12"; pdata[4, 34] = "6.000"; //subgroup 12
            pdata[1, 35] = "1"; pdata[2, 35] = "CL07"; pdata[3, 35] = "LOTYYYYMMDD12"; pdata[4, 35] = "7.500";
            pdata[1, 36] = "1"; pdata[2, 36] = "CL07"; pdata[3, 36] = "LOTYYYYMMDD12"; pdata[4, 36] = "5.300";
            //pdata[1, 37] = "1"; pdata[2, 37] = "CL07"; pdata[3, 37] = "LOTYYYYMMDD13"; pdata[4, 37] = "1.000"; //subgroup 13
            //pdata[1, 38] = "1"; pdata[2, 38] = "CL07"; pdata[3, 38] = "LOTYYYYMMDD13"; pdata[4, 38] = "1.500";
            //pdata[1, 39] = "1"; pdata[2, 39] = "CL07"; pdata[3, 39] = "LOTYYYYMMDD13"; pdata[4, 39] = "1.300";
            //pdata[1, 40] = "1"; pdata[2, 40] = "CL07"; pdata[3, 40] = "LOTYYYYMMDD14"; pdata[4, 40] = "1.000"; //subgroup 14
            //pdata[1, 41] = "1"; pdata[2, 41] = "CL07"; pdata[3, 41] = "LOTYYYYMMDD14"; pdata[4, 41] = "1.500";
            //pdata[1, 42] = "1"; pdata[2, 42] = "CL07"; pdata[3, 42] = "LOTYYYYMMDD14"; pdata[4, 42] = "1.300";

            pdata[1, 37] = "1"; pdata[2, 37] = "CL07"; pdata[3, 37] = "LOTYYYYMMDD13"; pdata[4, 37] = "8.000"; //subgroup 13
            pdata[1, 38] = "1"; pdata[2, 38] = "CL07"; pdata[3, 38] = "LOTYYYYMMDD13"; pdata[4, 38] = "6.500";
            pdata[1, 39] = "1"; pdata[2, 39] = "CL07"; pdata[3, 39] = "LOTYYYYMMDD13"; pdata[4, 39] = "3.300";
            pdata[1, 40] = "1"; pdata[2, 40] = "CL07"; pdata[3, 40] = "LOTYYYYMMDD14"; pdata[4, 40] = "8.000"; //subgroup 14
            pdata[1, 41] = "1"; pdata[2, 41] = "CL07"; pdata[3, 41] = "LOTYYYYMMDD14"; pdata[4, 41] = "4.500";
            pdata[1, 42] = "1"; pdata[2, 42] = "CL07"; pdata[3, 42] = "LOTYYYYMMDD14"; pdata[4, 42] = "3.300";


            return pdata;
        }

        /// <summary>
        /// Nelson rules 3번 Rule - Test Data (R, S)
        /// </summary>
        /// <returns></returns>
        public string[,] TestDataNelsonRules03()
        {
            string[,] pdata = new string[5, 57+1];
            pdata[1, 01] = "1"; pdata[2, 01] = "CL07"; pdata[3, 01] = "LOTYYYYMMDD01"; pdata[4, 01] = "15.000"; //subgroup 1
            pdata[1, 02] = "1"; pdata[2, 02] = "CL07"; pdata[3, 02] = "LOTYYYYMMDD01"; pdata[4, 02] = "16.500";
            pdata[1, 03] = "1"; pdata[2, 03] = "CL07"; pdata[3, 03] = "LOTYYYYMMDD01"; pdata[4, 03] = "17.800";
            pdata[1, 04] = "1"; pdata[2, 04] = "CL07"; pdata[3, 04] = "LOTYYYYMMDD02"; pdata[4, 04] = "5.000"; //subgroup 2
            pdata[1, 05] = "1"; pdata[2, 05] = "CL07"; pdata[3, 05] = "LOTYYYYMMDD02"; pdata[4, 05] = "6.000";
            pdata[1, 06] = "1"; pdata[2, 06] = "CL07"; pdata[3, 06] = "LOTYYYYMMDD02"; pdata[4, 06] = "7.000";
            pdata[1, 07] = "1"; pdata[2, 07] = "CL07"; pdata[3, 07] = "LOTYYYYMMDD03"; pdata[4, 07] = "4.000"; //subgroup 3
            pdata[1, 08] = "1"; pdata[2, 08] = "CL07"; pdata[3, 08] = "LOTYYYYMMDD03"; pdata[4, 08] = "3.500";
            pdata[1, 09] = "1"; pdata[2, 09] = "CL07"; pdata[3, 09] = "LOTYYYYMMDD03"; pdata[4, 09] = "2.100";
            pdata[1, 10] = "1"; pdata[2, 10] = "CL07"; pdata[3, 10] = "LOTYYYYMMDD04"; pdata[4, 10] = "11.100"; //subgroup 4 - 1
            pdata[1, 11] = "1"; pdata[2, 11] = "CL07"; pdata[3, 11] = "LOTYYYYMMDD04"; pdata[4, 11] = "12.200";
            pdata[1, 12] = "1"; pdata[2, 12] = "CL07"; pdata[3, 12] = "LOTYYYYMMDD04"; pdata[4, 12] = "13.300";
            pdata[1, 13] = "1"; pdata[2, 13] = "CL07"; pdata[3, 13] = "LOTYYYYMMDD05"; pdata[4, 13] = "14.700"; //subgroup 5 2
            pdata[1, 14] = "1"; pdata[2, 14] = "CL07"; pdata[3, 14] = "LOTYYYYMMDD05"; pdata[4, 14] = "15.400";
            pdata[1, 15] = "1"; pdata[2, 15] = "CL07"; pdata[3, 15] = "LOTYYYYMMDD05"; pdata[4, 15] = "16.300";
            pdata[1, 16] = "1"; pdata[2, 16] = "CL07"; pdata[3, 16] = "LOTYYYYMMDD06"; pdata[4, 16] = "17.800"; //subgroup 6 3
            pdata[1, 17] = "1"; pdata[2, 17] = "CL07"; pdata[3, 17] = "LOTYYYYMMDD06"; pdata[4, 17] = "18.500";
            pdata[1, 18] = "1"; pdata[2, 18] = "CL07"; pdata[3, 18] = "LOTYYYYMMDD06"; pdata[4, 18] = "19.400";
            pdata[1, 19] = "1"; pdata[2, 19] = "CL07"; pdata[3, 19] = "LOTYYYYMMDD07"; pdata[4, 19] = "20.700"; //subgroup 7 4
            pdata[1, 20] = "1"; pdata[2, 20] = "CL07"; pdata[3, 20] = "LOTYYYYMMDD07"; pdata[4, 20] = "21.400";
            pdata[1, 21] = "1"; pdata[2, 21] = "CL07"; pdata[3, 21] = "LOTYYYYMMDD07"; pdata[4, 21] = "22.500";
            pdata[1, 22] = "1"; pdata[2, 22] = "CL07"; pdata[3, 22] = "LOTYYYYMMDD08"; pdata[4, 22] = "23.800"; //subgroup 8 5
            pdata[1, 23] = "1"; pdata[2, 23] = "CL07"; pdata[3, 23] = "LOTYYYYMMDD08"; pdata[4, 23] = "24.400";
            pdata[1, 24] = "1"; pdata[2, 24] = "CL07"; pdata[3, 24] = "LOTYYYYMMDD08"; pdata[4, 24] = "25.300";
            pdata[1, 25] = "1"; pdata[2, 25] = "CL07"; pdata[3, 25] = "LOTYYYYMMDD09"; pdata[4, 25] = "26.800"; //subgroup 9 6
            pdata[1, 26] = "1"; pdata[2, 26] = "CL07"; pdata[3, 26] = "LOTYYYYMMDD09"; pdata[4, 26] = "27.400";
            pdata[1, 27] = "1"; pdata[2, 27] = "CL07"; pdata[3, 27] = "LOTYYYYMMDD09"; pdata[4, 27] = "28.300";
            pdata[1, 28] = "1"; pdata[2, 28] = "CL07"; pdata[3, 28] = "LOTYYYYMMDD10"; pdata[4, 28] = "2.500"; //subgroup 10 c
            pdata[1, 29] = "1"; pdata[2, 29] = "CL07"; pdata[3, 29] = "LOTYYYYMMDD10"; pdata[4, 29] = "3.600";
            pdata[1, 30] = "1"; pdata[2, 30] = "CL07"; pdata[3, 30] = "LOTYYYYMMDD10"; pdata[4, 30] = "4.700";
            pdata[1, 31] = "1"; pdata[2, 31] = "CL07"; pdata[3, 31] = "LOTYYYYMMDD11"; pdata[4, 31] = "32.500"; //subgroup 11 c
            pdata[1, 32] = "1"; pdata[2, 32] = "CL07"; pdata[3, 32] = "LOTYYYYMMDD11"; pdata[4, 32] = "33.400";
            pdata[1, 33] = "1"; pdata[2, 33] = "CL07"; pdata[3, 33] = "LOTYYYYMMDD11"; pdata[4, 33] = "34.500";
            pdata[1, 34] = "1"; pdata[2, 34] = "CL07"; pdata[3, 34] = "LOTYYYYMMDD12"; pdata[4, 34] = "19.600"; //subgroup 12 Min 1
            pdata[1, 35] = "1"; pdata[2, 35] = "CL07"; pdata[3, 35] = "LOTYYYYMMDD12"; pdata[4, 35] = "18.700";
            pdata[1, 36] = "1"; pdata[2, 36] = "CL07"; pdata[3, 36] = "LOTYYYYMMDD12"; pdata[4, 36] = "17.500";
            pdata[1, 37] = "1"; pdata[2, 37] = "CL07"; pdata[3, 37] = "LOTYYYYMMDD13"; pdata[4, 37] = "16.100"; //subgroup 13 2
            pdata[1, 38] = "1"; pdata[2, 38] = "CL07"; pdata[3, 38] = "LOTYYYYMMDD13"; pdata[4, 38] = "15.100";
            pdata[1, 39] = "1"; pdata[2, 39] = "CL07"; pdata[3, 39] = "LOTYYYYMMDD13"; pdata[4, 39] = "14.100";
            pdata[1, 40] = "1"; pdata[2, 40] = "CL07"; pdata[3, 40] = "LOTYYYYMMDD14"; pdata[4, 40] = "13.100"; //subgroup 14 3
            pdata[1, 41] = "1"; pdata[2, 41] = "CL07"; pdata[3, 41] = "LOTYYYYMMDD14"; pdata[4, 41] = "12.100";
            pdata[1, 42] = "1"; pdata[2, 42] = "CL07"; pdata[3, 42] = "LOTYYYYMMDD14"; pdata[4, 42] = "11.100";
            pdata[1, 43] = "1"; pdata[2, 43] = "CL07"; pdata[3, 43] = "LOTYYYYMMDD15"; pdata[4, 43] = "10.600"; //subgroup 15 4
            pdata[1, 44] = "1"; pdata[2, 44] = "CL07"; pdata[3, 44] = "LOTYYYYMMDD15"; pdata[4, 44] = "09.700";
            pdata[1, 45] = "1"; pdata[2, 45] = "CL07"; pdata[3, 45] = "LOTYYYYMMDD15"; pdata[4, 45] = "08.500";
            pdata[1, 46] = "1"; pdata[2, 46] = "CL07"; pdata[3, 46] = "LOTYYYYMMDD16"; pdata[4, 46] = "07.100"; //subgroup 16 5
            pdata[1, 47] = "1"; pdata[2, 47] = "CL07"; pdata[3, 47] = "LOTYYYYMMDD16"; pdata[4, 47] = "06.100";
            pdata[1, 48] = "1"; pdata[2, 48] = "CL07"; pdata[3, 48] = "LOTYYYYMMDD16"; pdata[4, 48] = "05.100";
            pdata[1, 49] = "1"; pdata[2, 49] = "CL07"; pdata[3, 49] = "LOTYYYYMMDD17"; pdata[4, 49] = "04.100"; //subgroup 17 6
            pdata[1, 50] = "1"; pdata[2, 50] = "CL07"; pdata[3, 50] = "LOTYYYYMMDD17"; pdata[4, 50] = "03.100";
            pdata[1, 51] = "1"; pdata[2, 51] = "CL07"; pdata[3, 51] = "LOTYYYYMMDD17"; pdata[4, 51] = "02.100";
            pdata[1, 52] = "1"; pdata[2, 52] = "CL07"; pdata[3, 52] = "LOTYYYYMMDD18"; pdata[4, 52] = "01.100"; //subgroup 18 7
            pdata[1, 53] = "1"; pdata[2, 53] = "CL07"; pdata[3, 53] = "LOTYYYYMMDD18"; pdata[4, 53] = "9.100";
            pdata[1, 54] = "1"; pdata[2, 54] = "CL07"; pdata[3, 54] = "LOTYYYYMMDD18"; pdata[4, 54] = "0.100";
            pdata[1, 55] = "1"; pdata[2, 55] = "CL07"; pdata[3, 55] = "LOTYYYYMMDD19"; pdata[4, 55] = "1.100"; //subgroup 19 8
            pdata[1, 56] = "1"; pdata[2, 56] = "CL07"; pdata[3, 56] = "LOTYYYYMMDD19"; pdata[4, 56] = "2.100";
            pdata[1, 57] = "1"; pdata[2, 57] = "CL07"; pdata[3, 57] = "LOTYYYYMMDD19"; pdata[4, 57] = "3.100";

            return pdata;
        }

        /// <summary>
        /// Nelson rules 4번 Rule - Test Data (R, S)
        /// </summary>
        /// <returns></returns>
        public string[,] TestDataNelsonRules04()
        {
            string[,] pdata = new string[5, 57 + 1];
            pdata[1, 01] = "1"; pdata[2, 01] = "CL07"; pdata[3, 01] = "LOTYYYYMMDD01"; pdata[4, 01] = "15.000"; //subgroup 0
            pdata[1, 02] = "1"; pdata[2, 02] = "CL07"; pdata[3, 02] = "LOTYYYYMMDD01"; pdata[4, 02] = "16.500";
            pdata[1, 03] = "1"; pdata[2, 03] = "CL07"; pdata[3, 03] = "LOTYYYYMMDD01"; pdata[4, 03] = "17.800";
            pdata[1, 04] = "1"; pdata[2, 04] = "CL07"; pdata[3, 04] = "LOTYYYYMMDD02"; pdata[4, 04] = "15.000"; //subgroup 1
            pdata[1, 05] = "1"; pdata[2, 05] = "CL07"; pdata[3, 05] = "LOTYYYYMMDD02"; pdata[4, 05] = "16.000";
            pdata[1, 06] = "1"; pdata[2, 06] = "CL07"; pdata[3, 06] = "LOTYYYYMMDD02"; pdata[4, 06] = "18.000";
            pdata[1, 07] = "1"; pdata[2, 07] = "CL07"; pdata[3, 07] = "LOTYYYYMMDD03"; pdata[4, 07] = "4.000"; //subgroup 2
            pdata[1, 08] = "1"; pdata[2, 08] = "CL07"; pdata[3, 08] = "LOTYYYYMMDD03"; pdata[4, 08] = "3.500";
            pdata[1, 09] = "1"; pdata[2, 09] = "CL07"; pdata[3, 09] = "LOTYYYYMMDD03"; pdata[4, 09] = "2.100";
            pdata[1, 10] = "1"; pdata[2, 10] = "CL07"; pdata[3, 10] = "LOTYYYYMMDD04"; pdata[4, 10] = "1.100"; //subgroup 3 --> 1
            pdata[1, 11] = "1"; pdata[2, 11] = "CL07"; pdata[3, 11] = "LOTYYYYMMDD04"; pdata[4, 11] = "2.200";
            pdata[1, 12] = "1"; pdata[2, 12] = "CL07"; pdata[3, 12] = "LOTYYYYMMDD04"; pdata[4, 12] = "3.300";
            pdata[1, 13] = "1"; pdata[2, 13] = "CL07"; pdata[3, 13] = "LOTYYYYMMDD05"; pdata[4, 13] = "14.700";//subgroup 4 --> 1
            pdata[1, 14] = "1"; pdata[2, 14] = "CL07"; pdata[3, 14] = "LOTYYYYMMDD05"; pdata[4, 14] = "15.400";
            pdata[1, 15] = "1"; pdata[2, 15] = "CL07"; pdata[3, 15] = "LOTYYYYMMDD05"; pdata[4, 15] = "16.300";
            pdata[1, 16] = "1"; pdata[2, 16] = "CL07"; pdata[3, 16] = "LOTYYYYMMDD06"; pdata[4, 16] = "3.800"; //subgroup 5 --> 1
            pdata[1, 17] = "1"; pdata[2, 17] = "CL07"; pdata[3, 17] = "LOTYYYYMMDD06"; pdata[4, 17] = "5.500";
            pdata[1, 18] = "1"; pdata[2, 18] = "CL07"; pdata[3, 18] = "LOTYYYYMMDD06"; pdata[4, 18] = "4.400";
            pdata[1, 19] = "1"; pdata[2, 19] = "CL07"; pdata[3, 19] = "LOTYYYYMMDD07"; pdata[4, 19] = "20.700"; //subgroup 6 --> 1
            pdata[1, 20] = "1"; pdata[2, 20] = "CL07"; pdata[3, 20] = "LOTYYYYMMDD07"; pdata[4, 20] = "21.400";
            pdata[1, 21] = "1"; pdata[2, 21] = "CL07"; pdata[3, 21] = "LOTYYYYMMDD07"; pdata[4, 21] = "22.500";
            pdata[1, 22] = "1"; pdata[2, 22] = "CL07"; pdata[3, 22] = "LOTYYYYMMDD08"; pdata[4, 22] = "3.800"; //subgroup 7 --> 1
            pdata[1, 23] = "1"; pdata[2, 23] = "CL07"; pdata[3, 23] = "LOTYYYYMMDD08"; pdata[4, 23] = "4.400";
            pdata[1, 24] = "1"; pdata[2, 24] = "CL07"; pdata[3, 24] = "LOTYYYYMMDD08"; pdata[4, 24] = "5.300";
            pdata[1, 25] = "1"; pdata[2, 25] = "CL07"; pdata[3, 25] = "LOTYYYYMMDD09"; pdata[4, 25] = "26.800"; //subgroup 8 --> 1
            pdata[1, 26] = "1"; pdata[2, 26] = "CL07"; pdata[3, 26] = "LOTYYYYMMDD09"; pdata[4, 26] = "27.400";
            pdata[1, 27] = "1"; pdata[2, 27] = "CL07"; pdata[3, 27] = "LOTYYYYMMDD09"; pdata[4, 27] = "28.300";
            pdata[1, 28] = "1"; pdata[2, 28] = "CL07"; pdata[3, 28] = "LOTYYYYMMDD10"; pdata[4, 28] = "2.500"; //subgroup 9 --> 1
            pdata[1, 29] = "1"; pdata[2, 29] = "CL07"; pdata[3, 29] = "LOTYYYYMMDD10"; pdata[4, 29] = "3.600";
            pdata[1, 30] = "1"; pdata[2, 30] = "CL07"; pdata[3, 30] = "LOTYYYYMMDD10"; pdata[4, 30] = "4.700";
            pdata[1, 31] = "1"; pdata[2, 31] = "CL07"; pdata[3, 31] = "LOTYYYYMMDD11"; pdata[4, 31] = "12.500"; //subgroup 10 --> 1
            pdata[1, 32] = "1"; pdata[2, 32] = "CL07"; pdata[3, 32] = "LOTYYYYMMDD11"; pdata[4, 32] = "15.400";
            pdata[1, 33] = "1"; pdata[2, 33] = "CL07"; pdata[3, 33] = "LOTYYYYMMDD11"; pdata[4, 33] = "18.500";
            pdata[1, 34] = "1"; pdata[2, 34] = "CL07"; pdata[3, 34] = "LOTYYYYMMDD12"; pdata[4, 34] = "3.600"; //subgroup 11 --> 1
            pdata[1, 35] = "1"; pdata[2, 35] = "CL07"; pdata[3, 35] = "LOTYYYYMMDD12"; pdata[4, 35] = "5.700";
            pdata[1, 36] = "1"; pdata[2, 36] = "CL07"; pdata[3, 36] = "LOTYYYYMMDD12"; pdata[4, 36] = "6.500";
            pdata[1, 37] = "1"; pdata[2, 37] = "CL07"; pdata[3, 37] = "LOTYYYYMMDD13"; pdata[4, 37] = "16.100"; //subgroup 12 --> 1
            pdata[1, 38] = "1"; pdata[2, 38] = "CL07"; pdata[3, 38] = "LOTYYYYMMDD13"; pdata[4, 38] = "15.100";
            pdata[1, 39] = "1"; pdata[2, 39] = "CL07"; pdata[3, 39] = "LOTYYYYMMDD13"; pdata[4, 39] = "14.100";
            pdata[1, 40] = "1"; pdata[2, 40] = "CL07"; pdata[3, 40] = "LOTYYYYMMDD14"; pdata[4, 40] = "3.100"; //subgroup 13 --> 1
            pdata[1, 41] = "1"; pdata[2, 41] = "CL07"; pdata[3, 41] = "LOTYYYYMMDD14"; pdata[4, 41] = "5.100";
            pdata[1, 42] = "1"; pdata[2, 42] = "CL07"; pdata[3, 42] = "LOTYYYYMMDD14"; pdata[4, 42] = "4.100";
            pdata[1, 43] = "1"; pdata[2, 43] = "CL07"; pdata[3, 43] = "LOTYYYYMMDD15"; pdata[4, 43] = "15.600"; //subgroup 14 --> 1
            pdata[1, 44] = "1"; pdata[2, 44] = "CL07"; pdata[3, 44] = "LOTYYYYMMDD15"; pdata[4, 44] = "19.700";
            pdata[1, 45] = "1"; pdata[2, 45] = "CL07"; pdata[3, 45] = "LOTYYYYMMDD15"; pdata[4, 45] = "18.500";
            pdata[1, 46] = "1"; pdata[2, 46] = "CL07"; pdata[3, 46] = "LOTYYYYMMDD16"; pdata[4, 46] = "7.100"; //subgroup 15 --> 1
            pdata[1, 47] = "1"; pdata[2, 47] = "CL07"; pdata[3, 47] = "LOTYYYYMMDD16"; pdata[4, 47] = "6.100";
            pdata[1, 48] = "1"; pdata[2, 48] = "CL07"; pdata[3, 48] = "LOTYYYYMMDD16"; pdata[4, 48] = "5.100";
            pdata[1, 49] = "1"; pdata[2, 49] = "CL07"; pdata[3, 49] = "LOTYYYYMMDD17"; pdata[4, 49] = "14.100"; //subgroup 16 --> 1
            pdata[1, 50] = "1"; pdata[2, 50] = "CL07"; pdata[3, 50] = "LOTYYYYMMDD17"; pdata[4, 50] = "13.100";
            pdata[1, 51] = "1"; pdata[2, 51] = "CL07"; pdata[3, 51] = "LOTYYYYMMDD17"; pdata[4, 51] = "15.100";
            pdata[1, 52] = "1"; pdata[2, 52] = "CL07"; pdata[3, 52] = "LOTYYYYMMDD18"; pdata[4, 52] = "4.100"; //subgroup 17 --> 1
            pdata[1, 53] = "1"; pdata[2, 53] = "CL07"; pdata[3, 53] = "LOTYYYYMMDD18"; pdata[4, 53] = "9.100";
            pdata[1, 54] = "1"; pdata[2, 54] = "CL07"; pdata[3, 54] = "LOTYYYYMMDD18"; pdata[4, 54] = "0.100";
            pdata[1, 55] = "1"; pdata[2, 55] = "CL07"; pdata[3, 55] = "LOTYYYYMMDD19"; pdata[4, 55] = "1.100"; //subgroup 18 --> X
            pdata[1, 56] = "1"; pdata[2, 56] = "CL07"; pdata[3, 56] = "LOTYYYYMMDD19"; pdata[4, 56] = "2.100";
            pdata[1, 57] = "1"; pdata[2, 57] = "CL07"; pdata[3, 57] = "LOTYYYYMMDD19"; pdata[4, 57] = "3.100";

            return pdata;
        }

        /// <summary>
        /// Nelson rules 5번 Rule - Test Data (R, S)
        /// </summary>
        /// <returns></returns>
        public string[,] TestDataNelsonRules05()
        {
            //2번 Rule - 행에서 9 개 이상의 점이 평균의 같은쪽에 있습니다.(일부 장기적인 편견 이 존재합니다.)
            string[,] pdata = new string[5, 43];
            pdata[1, 01] = "1"; pdata[2, 01] = "CL07"; pdata[3, 01] = "LOTYYYYMMDD01"; pdata[4, 01] = "1.000"; //subgroup 1
            pdata[1, 02] = "1"; pdata[2, 02] = "CL07"; pdata[3, 02] = "LOTYYYYMMDD01"; pdata[4, 02] = "1.500";
            pdata[1, 03] = "1"; pdata[2, 03] = "CL07"; pdata[3, 03] = "LOTYYYYMMDD01"; pdata[4, 03] = "3.000";
            pdata[1, 04] = "1"; pdata[2, 04] = "CL07"; pdata[3, 04] = "LOTYYYYMMDD02"; pdata[4, 04] = "9.000"; //subgroup 2
            pdata[1, 05] = "1"; pdata[2, 05] = "CL07"; pdata[3, 05] = "LOTYYYYMMDD02"; pdata[4, 05] = "4.000";
            pdata[1, 06] = "1"; pdata[2, 06] = "CL07"; pdata[3, 06] = "LOTYYYYMMDD02"; pdata[4, 06] = "5.000";
            pdata[1, 07] = "1"; pdata[2, 07] = "CL07"; pdata[3, 07] = "LOTYYYYMMDD03"; pdata[4, 07] = "2.000"; //subgroup 3
            pdata[1, 08] = "1"; pdata[2, 08] = "CL07"; pdata[3, 08] = "LOTYYYYMMDD03"; pdata[4, 08] = "1.500";
            pdata[1, 09] = "1"; pdata[2, 09] = "CL07"; pdata[3, 09] = "LOTYYYYMMDD03"; pdata[4, 09] = "1.100";
            pdata[1, 10] = "1"; pdata[2, 10] = "CL07"; pdata[3, 10] = "LOTYYYYMMDD04"; pdata[4, 10] = "6.000"; //subgroup 4
            pdata[1, 11] = "1"; pdata[2, 11] = "CL07"; pdata[3, 11] = "LOTYYYYMMDD04"; pdata[4, 11] = "4.000";
            pdata[1, 12] = "1"; pdata[2, 12] = "CL07"; pdata[3, 12] = "LOTYYYYMMDD04"; pdata[4, 12] = "3.000";
            pdata[1, 13] = "1"; pdata[2, 13] = "CL07"; pdata[3, 13] = "LOTYYYYMMDD05"; pdata[4, 13] = "7.000"; //subgroup 5
            pdata[1, 14] = "1"; pdata[2, 14] = "CL07"; pdata[3, 14] = "LOTYYYYMMDD05"; pdata[4, 14] = "4.000";
            pdata[1, 15] = "1"; pdata[2, 15] = "CL07"; pdata[3, 15] = "LOTYYYYMMDD05"; pdata[4, 15] = "3.000";
            pdata[1, 16] = "1"; pdata[2, 16] = "CL07"; pdata[3, 16] = "LOTYYYYMMDD06"; pdata[4, 16] = "8.000"; //subgroup 6
            pdata[1, 17] = "1"; pdata[2, 17] = "CL07"; pdata[3, 17] = "LOTYYYYMMDD06"; pdata[4, 17] = "5.000";
            pdata[1, 18] = "1"; pdata[2, 18] = "CL07"; pdata[3, 18] = "LOTYYYYMMDD06"; pdata[4, 18] = "6.000";
            pdata[1, 19] = "1"; pdata[2, 19] = "CL07"; pdata[3, 19] = "LOTYYYYMMDD07"; pdata[4, 19] = "9.000"; //subgroup 7
            pdata[1, 20] = "1"; pdata[2, 20] = "CL07"; pdata[3, 20] = "LOTYYYYMMDD07"; pdata[4, 20] = "4.000";
            pdata[1, 21] = "1"; pdata[2, 21] = "CL07"; pdata[3, 21] = "LOTYYYYMMDD07"; pdata[4, 21] = "5.000";
            pdata[1, 22] = "1"; pdata[2, 22] = "CL07"; pdata[3, 22] = "LOTYYYYMMDD08"; pdata[4, 22] = "8.000"; //subgroup 8
            pdata[1, 23] = "1"; pdata[2, 23] = "CL07"; pdata[3, 23] = "LOTYYYYMMDD08"; pdata[4, 23] = "4.000";
            pdata[1, 24] = "1"; pdata[2, 24] = "CL07"; pdata[3, 24] = "LOTYYYYMMDD08"; pdata[4, 24] = "3.000";
            pdata[1, 25] = "1"; pdata[2, 25] = "CL07"; pdata[3, 25] = "LOTYYYYMMDD09"; pdata[4, 25] = "8.000"; //subgroup 9
            pdata[1, 26] = "1"; pdata[2, 26] = "CL07"; pdata[3, 26] = "LOTYYYYMMDD09"; pdata[4, 26] = "4.000";
            pdata[1, 27] = "1"; pdata[2, 27] = "CL07"; pdata[3, 27] = "LOTYYYYMMDD09"; pdata[4, 27] = "3.000";
            pdata[1, 28] = "1"; pdata[2, 28] = "CL07"; pdata[3, 28] = "LOTYYYYMMDD10"; pdata[4, 28] = "5.000"; //subgroup 10
            pdata[1, 29] = "1"; pdata[2, 29] = "CL07"; pdata[3, 29] = "LOTYYYYMMDD10"; pdata[4, 29] = "6.500";
            pdata[1, 30] = "1"; pdata[2, 30] = "CL07"; pdata[3, 30] = "LOTYYYYMMDD10"; pdata[4, 30] = "7.300";
            pdata[1, 31] = "1"; pdata[2, 31] = "CL07"; pdata[3, 31] = "LOTYYYYMMDD11"; pdata[4, 31] = "5.000"; //subgroup 11
            pdata[1, 32] = "1"; pdata[2, 32] = "CL07"; pdata[3, 32] = "LOTYYYYMMDD11"; pdata[4, 32] = "4.500";
            pdata[1, 33] = "1"; pdata[2, 33] = "CL07"; pdata[3, 33] = "LOTYYYYMMDD11"; pdata[4, 33] = "5.300";
            pdata[1, 34] = "1"; pdata[2, 34] = "CL07"; pdata[3, 34] = "LOTYYYYMMDD12"; pdata[4, 34] = "6.000"; //subgroup 12
            pdata[1, 35] = "1"; pdata[2, 35] = "CL07"; pdata[3, 35] = "LOTYYYYMMDD12"; pdata[4, 35] = "3.500";
            pdata[1, 36] = "1"; pdata[2, 36] = "CL07"; pdata[3, 36] = "LOTYYYYMMDD12"; pdata[4, 36] = "4.300";
            pdata[1, 37] = "1"; pdata[2, 37] = "CL07"; pdata[3, 37] = "LOTYYYYMMDD13"; pdata[4, 37] = "1.000"; //subgroup 13
            pdata[1, 38] = "1"; pdata[2, 38] = "CL07"; pdata[3, 38] = "LOTYYYYMMDD13"; pdata[4, 38] = "1.500";
            pdata[1, 39] = "1"; pdata[2, 39] = "CL07"; pdata[3, 39] = "LOTYYYYMMDD13"; pdata[4, 39] = "1.300";
            pdata[1, 40] = "1"; pdata[2, 40] = "CL07"; pdata[3, 40] = "LOTYYYYMMDD14"; pdata[4, 40] = "1.000"; //subgroup 14
            pdata[1, 41] = "1"; pdata[2, 41] = "CL07"; pdata[3, 41] = "LOTYYYYMMDD14"; pdata[4, 41] = "1.500";
            pdata[1, 42] = "1"; pdata[2, 42] = "CL07"; pdata[3, 42] = "LOTYYYYMMDD14"; pdata[4, 42] = "1.300";

            return pdata;
        }

        /// <summary>
        /// Nelson rules 6번 Rule - Test Data (R, S)
        /// </summary>
        /// <returns></returns>
        public string[,] TestDataNelsonRules06()
        {
            //2번 Rule - 행에서 9 개 이상의 점이 평균의 같은쪽에 있습니다.(일부 장기적인 편견 이 존재합니다.)
            string[,] pdata = new string[5, 43];
            pdata[1, 01] = "1"; pdata[2, 01] = "CL07"; pdata[3, 01] = "LOTYYYYMMDD01"; pdata[4, 01] = "1.000"; //subgroup 1
            pdata[1, 02] = "1"; pdata[2, 02] = "CL07"; pdata[3, 02] = "LOTYYYYMMDD01"; pdata[4, 02] = "1.500";
            pdata[1, 03] = "1"; pdata[2, 03] = "CL07"; pdata[3, 03] = "LOTYYYYMMDD01"; pdata[4, 03] = "3.000";
            pdata[1, 04] = "1"; pdata[2, 04] = "CL07"; pdata[3, 04] = "LOTYYYYMMDD02"; pdata[4, 04] = "9.000"; //subgroup 2
            pdata[1, 05] = "1"; pdata[2, 05] = "CL07"; pdata[3, 05] = "LOTYYYYMMDD02"; pdata[4, 05] = "4.000";
            pdata[1, 06] = "1"; pdata[2, 06] = "CL07"; pdata[3, 06] = "LOTYYYYMMDD02"; pdata[4, 06] = "5.000";
            pdata[1, 07] = "1"; pdata[2, 07] = "CL07"; pdata[3, 07] = "LOTYYYYMMDD03"; pdata[4, 07] = "2.000"; //subgroup 3
            pdata[1, 08] = "1"; pdata[2, 08] = "CL07"; pdata[3, 08] = "LOTYYYYMMDD03"; pdata[4, 08] = "1.500";
            pdata[1, 09] = "1"; pdata[2, 09] = "CL07"; pdata[3, 09] = "LOTYYYYMMDD03"; pdata[4, 09] = "1.100";
            pdata[1, 10] = "1"; pdata[2, 10] = "CL07"; pdata[3, 10] = "LOTYYYYMMDD04"; pdata[4, 10] = "6.000"; //subgroup 4
            pdata[1, 11] = "1"; pdata[2, 11] = "CL07"; pdata[3, 11] = "LOTYYYYMMDD04"; pdata[4, 11] = "4.000";
            pdata[1, 12] = "1"; pdata[2, 12] = "CL07"; pdata[3, 12] = "LOTYYYYMMDD04"; pdata[4, 12] = "3.000";
            pdata[1, 13] = "1"; pdata[2, 13] = "CL07"; pdata[3, 13] = "LOTYYYYMMDD05"; pdata[4, 13] = "7.000"; //subgroup 5
            pdata[1, 14] = "1"; pdata[2, 14] = "CL07"; pdata[3, 14] = "LOTYYYYMMDD05"; pdata[4, 14] = "4.000";
            pdata[1, 15] = "1"; pdata[2, 15] = "CL07"; pdata[3, 15] = "LOTYYYYMMDD05"; pdata[4, 15] = "3.000";
            pdata[1, 16] = "1"; pdata[2, 16] = "CL07"; pdata[3, 16] = "LOTYYYYMMDD06"; pdata[4, 16] = "8.000"; //subgroup 6
            pdata[1, 17] = "1"; pdata[2, 17] = "CL07"; pdata[3, 17] = "LOTYYYYMMDD06"; pdata[4, 17] = "5.000";
            pdata[1, 18] = "1"; pdata[2, 18] = "CL07"; pdata[3, 18] = "LOTYYYYMMDD06"; pdata[4, 18] = "6.000";
            pdata[1, 19] = "1"; pdata[2, 19] = "CL07"; pdata[3, 19] = "LOTYYYYMMDD07"; pdata[4, 19] = "9.000"; //subgroup 7
            pdata[1, 20] = "1"; pdata[2, 20] = "CL07"; pdata[3, 20] = "LOTYYYYMMDD07"; pdata[4, 20] = "4.000";
            pdata[1, 21] = "1"; pdata[2, 21] = "CL07"; pdata[3, 21] = "LOTYYYYMMDD07"; pdata[4, 21] = "5.000";
            pdata[1, 22] = "1"; pdata[2, 22] = "CL07"; pdata[3, 22] = "LOTYYYYMMDD08"; pdata[4, 22] = "8.000"; //subgroup 8
            pdata[1, 23] = "1"; pdata[2, 23] = "CL07"; pdata[3, 23] = "LOTYYYYMMDD08"; pdata[4, 23] = "4.000";
            pdata[1, 24] = "1"; pdata[2, 24] = "CL07"; pdata[3, 24] = "LOTYYYYMMDD08"; pdata[4, 24] = "3.000";
            pdata[1, 25] = "1"; pdata[2, 25] = "CL07"; pdata[3, 25] = "LOTYYYYMMDD09"; pdata[4, 25] = "8.000"; //subgroup 9
            pdata[1, 26] = "1"; pdata[2, 26] = "CL07"; pdata[3, 26] = "LOTYYYYMMDD09"; pdata[4, 26] = "4.000";
            pdata[1, 27] = "1"; pdata[2, 27] = "CL07"; pdata[3, 27] = "LOTYYYYMMDD09"; pdata[4, 27] = "3.000";
            pdata[1, 28] = "1"; pdata[2, 28] = "CL07"; pdata[3, 28] = "LOTYYYYMMDD10"; pdata[4, 28] = "5.000"; //subgroup 10
            pdata[1, 29] = "1"; pdata[2, 29] = "CL07"; pdata[3, 29] = "LOTYYYYMMDD10"; pdata[4, 29] = "6.500";
            pdata[1, 30] = "1"; pdata[2, 30] = "CL07"; pdata[3, 30] = "LOTYYYYMMDD10"; pdata[4, 30] = "7.300";
            pdata[1, 31] = "1"; pdata[2, 31] = "CL07"; pdata[3, 31] = "LOTYYYYMMDD11"; pdata[4, 31] = "5.000"; //subgroup 11
            pdata[1, 32] = "1"; pdata[2, 32] = "CL07"; pdata[3, 32] = "LOTYYYYMMDD11"; pdata[4, 32] = "4.500";
            pdata[1, 33] = "1"; pdata[2, 33] = "CL07"; pdata[3, 33] = "LOTYYYYMMDD11"; pdata[4, 33] = "5.300";
            pdata[1, 34] = "1"; pdata[2, 34] = "CL07"; pdata[3, 34] = "LOTYYYYMMDD12"; pdata[4, 34] = "6.000"; //subgroup 12
            pdata[1, 35] = "1"; pdata[2, 35] = "CL07"; pdata[3, 35] = "LOTYYYYMMDD12"; pdata[4, 35] = "3.500";
            pdata[1, 36] = "1"; pdata[2, 36] = "CL07"; pdata[3, 36] = "LOTYYYYMMDD12"; pdata[4, 36] = "4.300";
            pdata[1, 37] = "1"; pdata[2, 37] = "CL07"; pdata[3, 37] = "LOTYYYYMMDD13"; pdata[4, 37] = "1.000"; //subgroup 13
            pdata[1, 38] = "1"; pdata[2, 38] = "CL07"; pdata[3, 38] = "LOTYYYYMMDD13"; pdata[4, 38] = "1.500";
            pdata[1, 39] = "1"; pdata[2, 39] = "CL07"; pdata[3, 39] = "LOTYYYYMMDD13"; pdata[4, 39] = "1.300";
            pdata[1, 40] = "1"; pdata[2, 40] = "CL07"; pdata[3, 40] = "LOTYYYYMMDD14"; pdata[4, 40] = "1.000"; //subgroup 14
            pdata[1, 41] = "1"; pdata[2, 41] = "CL07"; pdata[3, 41] = "LOTYYYYMMDD14"; pdata[4, 41] = "1.500";
            pdata[1, 42] = "1"; pdata[2, 42] = "CL07"; pdata[3, 42] = "LOTYYYYMMDD14"; pdata[4, 42] = "1.300";

            return pdata;
        }

        /// <summary>
        /// Nelson rules 7번 Rule - Test Data (R, S)
        /// </summary>
        /// <returns></returns>
        public string[,] TestDataNelsonRules07()
        {
            string[,] pdata = new string[5, 57 + 1];
            pdata[1, 01] = "1"; pdata[2, 01] = "CL07"; pdata[3, 01] = "LOTYYYYMMDD01"; pdata[4, 01] = "5.000"; //subgroup 0
            pdata[1, 02] = "1"; pdata[2, 02] = "CL07"; pdata[3, 02] = "LOTYYYYMMDD01"; pdata[4, 02] = "2.500";
            pdata[1, 03] = "1"; pdata[2, 03] = "CL07"; pdata[3, 03] = "LOTYYYYMMDD01"; pdata[4, 03] = "3.800";
            pdata[1, 04] = "1"; pdata[2, 04] = "CL07"; pdata[3, 04] = "LOTYYYYMMDD02"; pdata[4, 04] = "2.000"; //subgroup 1
            pdata[1, 05] = "1"; pdata[2, 05] = "CL07"; pdata[3, 05] = "LOTYYYYMMDD02"; pdata[4, 05] = "1.000";
            pdata[1, 06] = "1"; pdata[2, 06] = "CL07"; pdata[3, 06] = "LOTYYYYMMDD02"; pdata[4, 06] = "1.000";
            pdata[1, 07] = "1"; pdata[2, 07] = "CL07"; pdata[3, 07] = "LOTYYYYMMDD03"; pdata[4, 07] = "1.000"; //subgroup 2
            pdata[1, 08] = "1"; pdata[2, 08] = "CL07"; pdata[3, 08] = "LOTYYYYMMDD03"; pdata[4, 08] = "2.000";
            pdata[1, 09] = "1"; pdata[2, 09] = "CL07"; pdata[3, 09] = "LOTYYYYMMDD03"; pdata[4, 09] = "3.100";
            pdata[1, 10] = "1"; pdata[2, 10] = "CL07"; pdata[3, 10] = "LOTYYYYMMDD04"; pdata[4, 10] = "3.100"; //subgroup 3 --> 1
            pdata[1, 11] = "1"; pdata[2, 11] = "CL07"; pdata[3, 11] = "LOTYYYYMMDD04"; pdata[4, 11] = "2.200";
            pdata[1, 12] = "1"; pdata[2, 12] = "CL07"; pdata[3, 12] = "LOTYYYYMMDD04"; pdata[4, 12] = "1.300";
            pdata[1, 13] = "1"; pdata[2, 13] = "CL07"; pdata[3, 13] = "LOTYYYYMMDD05"; pdata[4, 13] = "1.100";//subgroup 4 --> 1
            pdata[1, 14] = "1"; pdata[2, 14] = "CL07"; pdata[3, 14] = "LOTYYYYMMDD05"; pdata[4, 14] = "2.500";
            pdata[1, 15] = "1"; pdata[2, 15] = "CL07"; pdata[3, 15] = "LOTYYYYMMDD05"; pdata[4, 15] = "2.300";
            pdata[1, 16] = "1"; pdata[2, 16] = "CL07"; pdata[3, 16] = "LOTYYYYMMDD06"; pdata[4, 16] = "1.300"; //subgroup 5 --> 1
            pdata[1, 17] = "1"; pdata[2, 17] = "CL07"; pdata[3, 17] = "LOTYYYYMMDD06"; pdata[4, 17] = "2.500";
            pdata[1, 18] = "1"; pdata[2, 18] = "CL07"; pdata[3, 18] = "LOTYYYYMMDD06"; pdata[4, 18] = "3.100";
            pdata[1, 19] = "1"; pdata[2, 19] = "CL07"; pdata[3, 19] = "LOTYYYYMMDD07"; pdata[4, 19] = "2.100"; //subgroup 6 --> 1
            pdata[1, 20] = "1"; pdata[2, 20] = "CL07"; pdata[3, 20] = "LOTYYYYMMDD07"; pdata[4, 20] = "2.500";
            pdata[1, 21] = "1"; pdata[2, 21] = "CL07"; pdata[3, 21] = "LOTYYYYMMDD07"; pdata[4, 21] = "1.500";
            pdata[1, 22] = "1"; pdata[2, 22] = "CL07"; pdata[3, 22] = "LOTYYYYMMDD08"; pdata[4, 22] = "1.500"; //subgroup 7 --> 1
            pdata[1, 23] = "1"; pdata[2, 23] = "CL07"; pdata[3, 23] = "LOTYYYYMMDD08"; pdata[4, 23] = "2.400";
            pdata[1, 24] = "1"; pdata[2, 24] = "CL07"; pdata[3, 24] = "LOTYYYYMMDD08"; pdata[4, 24] = "3.300";
            pdata[1, 25] = "1"; pdata[2, 25] = "CL07"; pdata[3, 25] = "LOTYYYYMMDD09"; pdata[4, 25] = "2.800"; //subgroup 8 --> 1
            pdata[1, 26] = "1"; pdata[2, 26] = "CL07"; pdata[3, 26] = "LOTYYYYMMDD09"; pdata[4, 26] = "1.400";
            pdata[1, 27] = "1"; pdata[2, 27] = "CL07"; pdata[3, 27] = "LOTYYYYMMDD09"; pdata[4, 27] = "2.300";
            pdata[1, 28] = "1"; pdata[2, 28] = "CL07"; pdata[3, 28] = "LOTYYYYMMDD10"; pdata[4, 28] = "2.500"; //subgroup 9 --> 1
            pdata[1, 29] = "1"; pdata[2, 29] = "CL07"; pdata[3, 29] = "LOTYYYYMMDD10"; pdata[4, 29] = "2.600";
            pdata[1, 30] = "1"; pdata[2, 30] = "CL07"; pdata[3, 30] = "LOTYYYYMMDD10"; pdata[4, 30] = "1.700";
            pdata[1, 31] = "1"; pdata[2, 31] = "CL07"; pdata[3, 31] = "LOTYYYYMMDD11"; pdata[4, 31] = "2.500"; //subgroup 10 --> 1
            pdata[1, 32] = "1"; pdata[2, 32] = "CL07"; pdata[3, 32] = "LOTYYYYMMDD11"; pdata[4, 32] = "1.400";
            pdata[1, 33] = "1"; pdata[2, 33] = "CL07"; pdata[3, 33] = "LOTYYYYMMDD11"; pdata[4, 33] = "2.500";
            pdata[1, 34] = "1"; pdata[2, 34] = "CL07"; pdata[3, 34] = "LOTYYYYMMDD12"; pdata[4, 34] = "1.600"; //subgroup 11 --> 1
            pdata[1, 35] = "1"; pdata[2, 35] = "CL07"; pdata[3, 35] = "LOTYYYYMMDD12"; pdata[4, 35] = "2.700";
            pdata[1, 36] = "1"; pdata[2, 36] = "CL07"; pdata[3, 36] = "LOTYYYYMMDD12"; pdata[4, 36] = "3.000";
            pdata[1, 37] = "1"; pdata[2, 37] = "CL07"; pdata[3, 37] = "LOTYYYYMMDD13"; pdata[4, 37] = "1.100"; //subgroup 12 --> 1
            pdata[1, 38] = "1"; pdata[2, 38] = "CL07"; pdata[3, 38] = "LOTYYYYMMDD13"; pdata[4, 38] = "2.100";
            pdata[1, 39] = "1"; pdata[2, 39] = "CL07"; pdata[3, 39] = "LOTYYYYMMDD13"; pdata[4, 39] = "3.100";
            pdata[1, 40] = "1"; pdata[2, 40] = "CL07"; pdata[3, 40] = "LOTYYYYMMDD14"; pdata[4, 40] = "2.100"; //subgroup 13 --> 1
            pdata[1, 41] = "1"; pdata[2, 41] = "CL07"; pdata[3, 41] = "LOTYYYYMMDD14"; pdata[4, 41] = "1.100";
            pdata[1, 42] = "1"; pdata[2, 42] = "CL07"; pdata[3, 42] = "LOTYYYYMMDD14"; pdata[4, 42] = "3.100";
            pdata[1, 43] = "1"; pdata[2, 43] = "CL07"; pdata[3, 43] = "LOTYYYYMMDD15"; pdata[4, 43] = "2.600"; //subgroup 14 --> 1
            pdata[1, 44] = "1"; pdata[2, 44] = "CL07"; pdata[3, 44] = "LOTYYYYMMDD15"; pdata[4, 44] = "2.700";
            pdata[1, 45] = "1"; pdata[2, 45] = "CL07"; pdata[3, 45] = "LOTYYYYMMDD15"; pdata[4, 45] = "1.500";
            pdata[1, 46] = "1"; pdata[2, 46] = "CL07"; pdata[3, 46] = "LOTYYYYMMDD16"; pdata[4, 46] = "3.100"; //subgroup 15 --> 1
            pdata[1, 47] = "1"; pdata[2, 47] = "CL07"; pdata[3, 47] = "LOTYYYYMMDD16"; pdata[4, 47] = "3.100";
            pdata[1, 48] = "1"; pdata[2, 48] = "CL07"; pdata[3, 48] = "LOTYYYYMMDD16"; pdata[4, 48] = "1.100";
            pdata[1, 49] = "1"; pdata[2, 49] = "CL07"; pdata[3, 49] = "LOTYYYYMMDD17"; pdata[4, 49] = "1.100"; //subgroup 16 --> 1
            pdata[1, 50] = "1"; pdata[2, 50] = "CL07"; pdata[3, 50] = "LOTYYYYMMDD17"; pdata[4, 50] = "2.100";
            pdata[1, 51] = "1"; pdata[2, 51] = "CL07"; pdata[3, 51] = "LOTYYYYMMDD17"; pdata[4, 51] = "3.100";
            pdata[1, 52] = "1"; pdata[2, 52] = "CL07"; pdata[3, 52] = "LOTYYYYMMDD18"; pdata[4, 52] = "2.100"; //subgroup 17 --> 1
            pdata[1, 53] = "1"; pdata[2, 53] = "CL07"; pdata[3, 53] = "LOTYYYYMMDD18"; pdata[4, 53] = "2.100";
            pdata[1, 54] = "1"; pdata[2, 54] = "CL07"; pdata[3, 54] = "LOTYYYYMMDD18"; pdata[4, 54] = "1.100";
            pdata[1, 55] = "1"; pdata[2, 55] = "CL07"; pdata[3, 55] = "LOTYYYYMMDD19"; pdata[4, 55] = "2.100"; //subgroup 18 --> X
            pdata[1, 56] = "1"; pdata[2, 56] = "CL07"; pdata[3, 56] = "LOTYYYYMMDD19"; pdata[4, 56] = "1.100";
            pdata[1, 57] = "1"; pdata[2, 57] = "CL07"; pdata[3, 57] = "LOTYYYYMMDD19"; pdata[4, 57] = "2.100";

            return pdata;
        }

        /// <summary>
        /// Nelson rules 8번 Rule - Test Data (R, S)
        /// </summary>
        /// <returns></returns>
        public string[,] TestDataNelsonRules08()
        {
            string[,] pdata = new string[5, 57 + 1];
            pdata[1, 01] = "1"; pdata[2, 01] = "CL07"; pdata[3, 01] = "LOTYYYYMMDD01"; pdata[4, 01] = "3.100"; //subgroup 0
            pdata[1, 02] = "1"; pdata[2, 02] = "CL07"; pdata[3, 02] = "LOTYYYYMMDD01"; pdata[4, 02] = "2.200";
            pdata[1, 03] = "1"; pdata[2, 03] = "CL07"; pdata[3, 03] = "LOTYYYYMMDD01"; pdata[4, 03] = "1.300";
            pdata[1, 04] = "1"; pdata[2, 04] = "CL07"; pdata[3, 04] = "LOTYYYYMMDD02"; pdata[4, 04] = "1.100"; //subgroup 1
            pdata[1, 05] = "1"; pdata[2, 05] = "CL07"; pdata[3, 05] = "LOTYYYYMMDD02"; pdata[4, 05] = "2.500";
            pdata[1, 06] = "1"; pdata[2, 06] = "CL07"; pdata[3, 06] = "LOTYYYYMMDD02"; pdata[4, 06] = "2.300";
            pdata[1, 07] = "1"; pdata[2, 07] = "CL07"; pdata[3, 07] = "LOTYYYYMMDD03"; pdata[4, 07] = "1.300"; //subgroup 2
            pdata[1, 08] = "1"; pdata[2, 08] = "CL07"; pdata[3, 08] = "LOTYYYYMMDD03"; pdata[4, 08] = "2.500";
            pdata[1, 09] = "1"; pdata[2, 09] = "CL07"; pdata[3, 09] = "LOTYYYYMMDD03"; pdata[4, 09] = "3.100";
            pdata[1, 10] = "1"; pdata[2, 10] = "CL07"; pdata[3, 10] = "LOTYYYYMMDD04"; pdata[4, 10] = "4.100"; //subgroup 3 --> 1
            pdata[1, 11] = "1"; pdata[2, 11] = "CL07"; pdata[3, 11] = "LOTYYYYMMDD04"; pdata[4, 11] = "3.200";
            pdata[1, 12] = "1"; pdata[2, 12] = "CL07"; pdata[3, 12] = "LOTYYYYMMDD04"; pdata[4, 12] = "1.300";
            pdata[1, 13] = "1"; pdata[2, 13] = "CL07"; pdata[3, 13] = "LOTYYYYMMDD05"; pdata[4, 13] = "3.100";//subgroup 4 --> 1
            pdata[1, 14] = "1"; pdata[2, 14] = "CL07"; pdata[3, 14] = "LOTYYYYMMDD05"; pdata[4, 14] = "5.500";
            pdata[1, 15] = "1"; pdata[2, 15] = "CL07"; pdata[3, 15] = "LOTYYYYMMDD05"; pdata[4, 15] = "2.300";
            pdata[1, 16] = "1"; pdata[2, 16] = "CL07"; pdata[3, 16] = "LOTYYYYMMDD06"; pdata[4, 16] = "3.300"; //subgroup 5 --> 1
            pdata[1, 17] = "1"; pdata[2, 17] = "CL07"; pdata[3, 17] = "LOTYYYYMMDD06"; pdata[4, 17] = "4.500";
            pdata[1, 18] = "1"; pdata[2, 18] = "CL07"; pdata[3, 18] = "LOTYYYYMMDD06"; pdata[4, 18] = "3.100";
            pdata[1, 19] = "1"; pdata[2, 19] = "CL07"; pdata[3, 19] = "LOTYYYYMMDD07"; pdata[4, 19] = "1.100"; //subgroup 6 --> 1
            pdata[1, 20] = "1"; pdata[2, 20] = "CL07"; pdata[3, 20] = "LOTYYYYMMDD07"; pdata[4, 20] = "1.500";
            pdata[1, 21] = "1"; pdata[2, 21] = "CL07"; pdata[3, 21] = "LOTYYYYMMDD07"; pdata[4, 21] = "0.500";
            pdata[1, 22] = "1"; pdata[2, 22] = "CL07"; pdata[3, 22] = "LOTYYYYMMDD08"; pdata[4, 22] = "1.500"; //subgroup 7 --> 1
            pdata[1, 23] = "1"; pdata[2, 23] = "CL07"; pdata[3, 23] = "LOTYYYYMMDD08"; pdata[4, 23] = "4.400";
            pdata[1, 24] = "1"; pdata[2, 24] = "CL07"; pdata[3, 24] = "LOTYYYYMMDD08"; pdata[4, 24] = "3.300";
            pdata[1, 25] = "1"; pdata[2, 25] = "CL07"; pdata[3, 25] = "LOTYYYYMMDD09"; pdata[4, 25] = "2.800"; //subgroup 8 --> 1
            pdata[1, 26] = "1"; pdata[2, 26] = "CL07"; pdata[3, 26] = "LOTYYYYMMDD09"; pdata[4, 26] = "0.400";
            pdata[1, 27] = "1"; pdata[2, 27] = "CL07"; pdata[3, 27] = "LOTYYYYMMDD09"; pdata[4, 27] = "2.300";
            pdata[1, 28] = "1"; pdata[2, 28] = "CL07"; pdata[3, 28] = "LOTYYYYMMDD10"; pdata[4, 28] = "5.500"; //subgroup 9 --> 1
            pdata[1, 29] = "1"; pdata[2, 29] = "CL07"; pdata[3, 29] = "LOTYYYYMMDD10"; pdata[4, 29] = "2.600";
            pdata[1, 30] = "1"; pdata[2, 30] = "CL07"; pdata[3, 30] = "LOTYYYYMMDD10"; pdata[4, 30] = "1.700";
            pdata[1, 31] = "1"; pdata[2, 31] = "CL07"; pdata[3, 31] = "LOTYYYYMMDD11"; pdata[4, 31] = "0.500"; //subgroup 10 --> 1
            pdata[1, 32] = "1"; pdata[2, 32] = "CL07"; pdata[3, 32] = "LOTYYYYMMDD11"; pdata[4, 32] = "1.400";
            pdata[1, 33] = "1"; pdata[2, 33] = "CL07"; pdata[3, 33] = "LOTYYYYMMDD11"; pdata[4, 33] = "2.500";
            pdata[1, 34] = "1"; pdata[2, 34] = "CL07"; pdata[3, 34] = "LOTYYYYMMDD12"; pdata[4, 34] = "5.600"; //subgroup 11 --> 1
            pdata[1, 35] = "1"; pdata[2, 35] = "CL07"; pdata[3, 35] = "LOTYYYYMMDD12"; pdata[4, 35] = "2.700";
            pdata[1, 36] = "1"; pdata[2, 36] = "CL07"; pdata[3, 36] = "LOTYYYYMMDD12"; pdata[4, 36] = "3.000";
            pdata[1, 37] = "1"; pdata[2, 37] = "CL07"; pdata[3, 37] = "LOTYYYYMMDD13"; pdata[4, 37] = "2.100"; //subgroup 12 --> 1
            pdata[1, 38] = "1"; pdata[2, 38] = "CL07"; pdata[3, 38] = "LOTYYYYMMDD13"; pdata[4, 38] = "2.100";
            pdata[1, 39] = "1"; pdata[2, 39] = "CL07"; pdata[3, 39] = "LOTYYYYMMDD13"; pdata[4, 39] = "3.100";
            pdata[1, 40] = "1"; pdata[2, 40] = "CL07"; pdata[3, 40] = "LOTYYYYMMDD14"; pdata[4, 40] = "1.100"; //subgroup 13 --> 1
            pdata[1, 41] = "1"; pdata[2, 41] = "CL07"; pdata[3, 41] = "LOTYYYYMMDD14"; pdata[4, 41] = "1.100";
            pdata[1, 42] = "1"; pdata[2, 42] = "CL07"; pdata[3, 42] = "LOTYYYYMMDD14"; pdata[4, 42] = "1.100";
            pdata[1, 43] = "1"; pdata[2, 43] = "CL07"; pdata[3, 43] = "LOTYYYYMMDD15"; pdata[4, 43] = "1.600"; //subgroup 14 --> 1
            pdata[1, 44] = "1"; pdata[2, 44] = "CL07"; pdata[3, 44] = "LOTYYYYMMDD15"; pdata[4, 44] = "2.700";
            pdata[1, 45] = "1"; pdata[2, 45] = "CL07"; pdata[3, 45] = "LOTYYYYMMDD15"; pdata[4, 45] = "1.500";
            pdata[1, 46] = "1"; pdata[2, 46] = "CL07"; pdata[3, 46] = "LOTYYYYMMDD16"; pdata[4, 46] = "3.100"; //subgroup 15 --> 1
            pdata[1, 47] = "1"; pdata[2, 47] = "CL07"; pdata[3, 47] = "LOTYYYYMMDD16"; pdata[4, 47] = "3.100";
            pdata[1, 48] = "1"; pdata[2, 48] = "CL07"; pdata[3, 48] = "LOTYYYYMMDD16"; pdata[4, 48] = "1.100";
            pdata[1, 49] = "1"; pdata[2, 49] = "CL07"; pdata[3, 49] = "LOTYYYYMMDD17"; pdata[4, 49] = "1.100"; //subgroup 16 --> 1
            pdata[1, 50] = "1"; pdata[2, 50] = "CL07"; pdata[3, 50] = "LOTYYYYMMDD17"; pdata[4, 50] = "2.100";
            pdata[1, 51] = "1"; pdata[2, 51] = "CL07"; pdata[3, 51] = "LOTYYYYMMDD17"; pdata[4, 51] = "3.100";
            pdata[1, 52] = "1"; pdata[2, 52] = "CL07"; pdata[3, 52] = "LOTYYYYMMDD18"; pdata[4, 52] = "2.100"; //subgroup 17 --> 1
            pdata[1, 53] = "1"; pdata[2, 53] = "CL07"; pdata[3, 53] = "LOTYYYYMMDD18"; pdata[4, 53] = "2.100";
            pdata[1, 54] = "1"; pdata[2, 54] = "CL07"; pdata[3, 54] = "LOTYYYYMMDD18"; pdata[4, 54] = "1.100";
            pdata[1, 55] = "1"; pdata[2, 55] = "CL07"; pdata[3, 55] = "LOTYYYYMMDD19"; pdata[4, 55] = "2.100"; //subgroup 18 --> X
            pdata[1, 56] = "1"; pdata[2, 56] = "CL07"; pdata[3, 56] = "LOTYYYYMMDD19"; pdata[4, 56] = "1.100";
            pdata[1, 57] = "1"; pdata[2, 57] = "CL07"; pdata[3, 57] = "LOTYYYYMMDD19"; pdata[4, 57] = "2.100";

            return pdata;
        }

        #endregion Rule Check Test Data



        /// <summary>
        /// R, S Test Data
        /// </summary>
        /// <returns></returns>
        public string[,] CTR01_SubgroupOne()
        {
            string[,] pdata = new string[5, 7];
            pdata[1, 01] = "1"; pdata[2, 01] = "CL07"; pdata[3, 01] = "LOTYYYYMMDD01"; pdata[4, 01] = "1.000"; //subgroup 1
            pdata[1, 02] = "1"; pdata[2, 02] = "CL07"; pdata[3, 02] = "LOTYYYYMMDD01"; pdata[4, 02] = "2.000";
            pdata[1, 03] = "1"; pdata[2, 03] = "CL07"; pdata[3, 03] = "LOTYYYYMMDD01"; pdata[4, 03] = "3.000";
            pdata[1, 04] = "1"; pdata[2, 04] = "CL07"; pdata[3, 04] = "LOTYYYYMMDD01"; pdata[4, 04] = "4.000";
            pdata[1, 05] = "1"; pdata[2, 05] = "CL07"; pdata[3, 05] = "LOTYYYYMMDD01"; pdata[4, 05] = "6.000";
            pdata[1, 06] = "1"; pdata[2, 06] = "CL07"; pdata[3, 06] = "LOTYYYYMMDD01"; pdata[4, 06] = "4.000";

            return pdata;
        }

        /// <summary>
        /// BoxPlot Chart Test Data
        /// </summary>
        /// <returns></returns>
        public string[,] BoxPlot01_Single_TestData()
        {
            string[,] pdata = new string[5, 24];
            pdata[1, 01] = "1"; pdata[2, 01] = "CL07"; pdata[3, 01] = "LOTYYYYMMDD01"; pdata[4, 01] = "71.000"; //subgroup 1
            pdata[1, 02] = "1"; pdata[2, 02] = "CL07"; pdata[3, 02] = "LOTYYYYMMDD01"; pdata[4, 02] = "74.000";
            pdata[1, 03] = "1"; pdata[2, 03] = "CL07"; pdata[3, 03] = "LOTYYYYMMDD01"; pdata[4, 03] = "75.000";
            pdata[1, 04] = "1"; pdata[2, 04] = "CL07"; pdata[3, 04] = "LOTYYYYMMDD01"; pdata[4, 04] = "76.000";
            pdata[1, 05] = "1"; pdata[2, 05] = "CL07"; pdata[3, 05] = "LOTYYYYMMDD01"; pdata[4, 05] = "76.000";
            pdata[1, 06] = "1"; pdata[2, 06] = "CL07"; pdata[3, 06] = "LOTYYYYMMDD01"; pdata[4, 06] = "79.000";
            pdata[1, 07] = "1"; pdata[2, 07] = "CL07"; pdata[3, 07] = "LOTYYYYMMDD01"; pdata[4, 07] = "79.000";
            pdata[1, 08] = "1"; pdata[2, 08] = "CL07"; pdata[3, 08] = "LOTYYYYMMDD01"; pdata[4, 08] = "81.000";
            pdata[1, 09] = "1"; pdata[2, 09] = "CL07"; pdata[3, 09] = "LOTYYYYMMDD01"; pdata[4, 09] = "82.000";
            pdata[1, 10] = "1"; pdata[2, 10] = "CL07"; pdata[3, 10] = "LOTYYYYMMDD01"; pdata[4, 10] = "82.000";
            pdata[1, 11] = "1"; pdata[2, 11] = "CL07"; pdata[3, 11] = "LOTYYYYMMDD01"; pdata[4, 11] = "85.000";
            pdata[1, 12] = "1"; pdata[2, 12] = "CL07"; pdata[3, 12] = "LOTYYYYMMDD02"; pdata[4, 12] = "53.300"; //subgroup 2
            pdata[1, 13] = "1"; pdata[2, 13] = "CL07"; pdata[3, 13] = "LOTYYYYMMDD02"; pdata[4, 13] = "64.456";
            pdata[1, 14] = "1"; pdata[2, 14] = "CL07"; pdata[3, 14] = "LOTYYYYMMDD02"; pdata[4, 14] = "45.548";
            pdata[1, 15] = "1"; pdata[2, 15] = "CL07"; pdata[3, 15] = "LOTYYYYMMDD02"; pdata[4, 15] = "74.424";
            pdata[1, 16] = "1"; pdata[2, 16] = "CL07"; pdata[3, 16] = "LOTYYYYMMDD02"; pdata[4, 16] = "23.334";
            pdata[1, 17] = "1"; pdata[2, 17] = "CL07"; pdata[3, 17] = "LOTYYYYMMDD02"; pdata[4, 17] = "74.489";
            pdata[1, 18] = "1"; pdata[2, 18] = "CL07"; pdata[3, 18] = "LOTYYYYMMDD03"; pdata[4, 18] = "35.500"; //subgroup 3
            pdata[1, 19] = "1"; pdata[2, 19] = "CL07"; pdata[3, 19] = "LOTYYYYMMDD03"; pdata[4, 19] = "44.678";
            pdata[1, 20] = "1"; pdata[2, 20] = "CL07"; pdata[3, 20] = "LOTYYYYMMDD03"; pdata[4, 20] = "53.780";
            pdata[1, 21] = "1"; pdata[2, 21] = "CL07"; pdata[3, 21] = "LOTYYYYMMDD03"; pdata[4, 21] = "44.843";
            pdata[1, 22] = "1"; pdata[2, 22] = "CL07"; pdata[3, 22] = "LOTYYYYMMDD03"; pdata[4, 22] = "35.543";
            pdata[1, 23] = "1"; pdata[2, 23] = "CL07"; pdata[3, 23] = "LOTYYYYMMDD03"; pdata[4, 23] = "56.987";
            return pdata;
        }

        /// <summary>
        /// R, S 합동 Test Data
        /// </summary>
        /// <returns></returns>
        public string[,] CTR02_PL()
        {
            string[,] pdata = new string[5, 18];
            pdata[1, 01] = "1"; pdata[2, 01] = "CL07"; pdata[3, 01] = "LOTYYYYMMDD01"; pdata[4, 01] = "1.000"; //subgroup 1
            pdata[1, 02] = "1"; pdata[2, 02] = "CL07"; pdata[3, 02] = "LOTYYYYMMDD01"; pdata[4, 02] = "2.000";
            pdata[1, 03] = "1"; pdata[2, 03] = "CL07"; pdata[3, 03] = "LOTYYYYMMDD01"; pdata[4, 03] = "3.000";
            pdata[1, 04] = "1"; pdata[2, 04] = "CL07"; pdata[3, 04] = "LOTYYYYMMDD01"; pdata[4, 04] = "4.000";
            pdata[1, 05] = "1"; pdata[2, 05] = "CL07"; pdata[3, 05] = "LOTYYYYMMDD01"; pdata[4, 05] = "6.000";
            pdata[1, 06] = "1"; pdata[2, 06] = "CL07"; pdata[3, 06] = "LOTYYYYMMDD01"; pdata[4, 06] = "4.000";

            pdata[1, 07] = "1"; pdata[2, 07] = "CL07"; pdata[3, 07] = "LOTYYYYMMDD02"; pdata[4, 07] = "3.000"; //subgroup 2
            pdata[1, 08] = "1"; pdata[2, 08] = "CL07"; pdata[3, 08] = "LOTYYYYMMDD02"; pdata[4, 08] = "4.000";
            pdata[1, 09] = "1"; pdata[2, 09] = "CL07"; pdata[3, 09] = "LOTYYYYMMDD02"; pdata[4, 09] = "5.000";
            pdata[1, 10] = "1"; pdata[2, 10] = "CL07"; pdata[3, 10] = "LOTYYYYMMDD02"; pdata[4, 10] = "4.000";
            pdata[1, 11] = "1"; pdata[2, 11] = "CL07"; pdata[3, 11] = "LOTYYYYMMDD02"; pdata[4, 11] = "3.000";
            pdata[1, 12] = "1"; pdata[2, 12] = "CL07"; pdata[3, 12] = "LOTYYYYMMDD02"; pdata[4, 12] = "4.000";

            pdata[1, 13] = "1"; pdata[2, 13] = "CL07"; pdata[3, 13] = "LOTYYYYMMDD03"; pdata[4, 13] = "5.000"; //subgroup 3
            pdata[1, 14] = "1"; pdata[2, 14] = "CL07"; pdata[3, 14] = "LOTYYYYMMDD03"; pdata[4, 14] = "4.000";
            pdata[1, 15] = "1"; pdata[2, 15] = "CL07"; pdata[3, 15] = "LOTYYYYMMDD03"; pdata[4, 15] = "3.000";
            pdata[1, 16] = "1"; pdata[2, 16] = "CL07"; pdata[3, 16] = "LOTYYYYMMDD03"; pdata[4, 16] = "4.000";
            pdata[1, 17] = "1"; pdata[2, 17] = "CL07"; pdata[3, 17] = "LOTYYYYMMDD03"; pdata[4, 17] = "5.000";
            //pdata[1, 18] = "1"; pdata[2, 18] = "CL07"; pdata[3, 18] = "LOTYYYYMMDD03"; pdata[4, 18] = "4.000";
            return pdata;
        }

        /// <summary>
        /// R, S Test Data
        /// </summary>
        /// <returns></returns>
        public string[,] CTR02_IMR()
        {
            string[,] pdata = new string[5, 25];
            pdata[1, 01] = "1"; pdata[2, 01] = "CL07"; pdata[3, 01] = "LOTYYYYMMDD01"; pdata[4, 01] = "13.343"; //subgroup 1
            pdata[1, 02] = "1"; pdata[2, 02] = "CL07"; pdata[3, 02] = "LOTYYYYMMDD01"; pdata[4, 02] = "22.324";
            pdata[1, 03] = "1"; pdata[2, 03] = "CL07"; pdata[3, 03] = "LOTYYYYMMDD01"; pdata[4, 03] = "32.324";
            pdata[1, 04] = "1"; pdata[2, 04] = "CL07"; pdata[3, 04] = "LOTYYYYMMDD02"; pdata[4, 04] = "41.323";
            pdata[1, 05] = "1"; pdata[2, 05] = "CL07"; pdata[3, 05] = "LOTYYYYMMDD02"; pdata[4, 05] = "63.876";
            pdata[1, 06] = "1"; pdata[2, 06] = "CL07"; pdata[3, 06] = "LOTYYYYMMDD02"; pdata[4, 06] = "44.342";

            pdata[1, 07] = "1"; pdata[2, 07] = "CL07"; pdata[3, 07] = "LOTYYYYMMDD03"; pdata[4, 07] = "47.234"; //subgroup 2
            pdata[1, 08] = "1"; pdata[2, 08] = "CL07"; pdata[3, 08] = "LOTYYYYMMDD03"; pdata[4, 08] = "45.834";
            pdata[1, 09] = "1"; pdata[2, 09] = "CL07"; pdata[3, 09] = "LOTYYYYMMDD04"; pdata[4, 09] = "53.987";
            pdata[1, 10] = "1"; pdata[2, 10] = "CL07"; pdata[3, 10] = "LOTYYYYMMDD04"; pdata[4, 10] = "45.234";
            pdata[1, 11] = "1"; pdata[2, 11] = "CL07"; pdata[3, 11] = "LOTYYYYMMDD05"; pdata[4, 11] = "34.987";
            pdata[1, 12] = "1"; pdata[2, 12] = "CL07"; pdata[3, 12] = "LOTYYYYMMDD05"; pdata[4, 12] = "45.423";

            pdata[1, 13] = "1"; pdata[2, 13] = "CL07"; pdata[3, 13] = "LOTYYYYMMDD06"; pdata[4, 13] = "58.324"; //subgroup 3
            pdata[1, 14] = "1"; pdata[2, 14] = "CL07"; pdata[3, 14] = "LOTYYYYMMDD06"; pdata[4, 14] = "43.323";
            pdata[1, 15] = "1"; pdata[2, 15] = "CL07"; pdata[3, 15] = "LOTYYYYMMDD07"; pdata[4, 15] = "38.423";
            pdata[1, 16] = "1"; pdata[2, 16] = "CL07"; pdata[3, 16] = "LOTYYYYMMDD08"; pdata[4, 16] = "45.234";
            pdata[1, 17] = "1"; pdata[2, 17] = "CL07"; pdata[3, 17] = "LOTYYYYMMDD09"; pdata[4, 17] = "53.134";
            pdata[1, 18] = "1"; pdata[2, 18] = "CL07"; pdata[3, 18] = "LOTYYYYMMDD10"; pdata[4, 18] = "42.234";

            pdata[1, 19] = "1"; pdata[2, 19] = "CL08"; pdata[3, 19] = "LOTYYYYMMDD01"; pdata[4, 19] = "58.324"; //subgroup 3
            pdata[1, 20] = "1"; pdata[2, 20] = "CL08"; pdata[3, 20] = "LOTYYYYMMDD02"; pdata[4, 20] = "43.323";
            pdata[1, 21] = "1"; pdata[2, 21] = "CL08"; pdata[3, 21] = "LOTYYYYMMDD03"; pdata[4, 21] = "38.423";
            pdata[1, 22] = "1"; pdata[2, 22] = "CL08"; pdata[3, 22] = "LOTYYYYMMDD04"; pdata[4, 22] = "45.234";
            pdata[1, 23] = "1"; pdata[2, 23] = "CL08"; pdata[3, 23] = "LOTYYYYMMDD05"; pdata[4, 23] = "53.134";
            pdata[1, 24] = "1"; pdata[2, 24] = "CL08"; pdata[3, 24] = "LOTYYYYMMDD06"; pdata[4, 24] = "42.234";

            return pdata;
        }

        /// <summary>
        /// IMR 표준 Data. 
        /// </summary>
        /// <returns></returns>
        public string[,] CTR02_IMR_Standard_01()
        {
            string[,] pdata = new string[5, 25];
            pdata[1, 01] = "1"; pdata[2, 01] = "CL07"; pdata[3, 01] = "LOTYYYYMMDD01"; pdata[4, 01] = "13.343"; //subgroup 1
            pdata[1, 02] = "1"; pdata[2, 02] = "CL07"; pdata[3, 02] = "LOTYYYYMMDD02"; pdata[4, 02] = "22.324";
            pdata[1, 03] = "1"; pdata[2, 03] = "CL07"; pdata[3, 03] = "LOTYYYYMMDD03"; pdata[4, 03] = "32.324";
            pdata[1, 04] = "1"; pdata[2, 04] = "CL07"; pdata[3, 04] = "LOTYYYYMMDD04"; pdata[4, 04] = "41.323";
            pdata[1, 05] = "1"; pdata[2, 05] = "CL07"; pdata[3, 05] = "LOTYYYYMMDD05"; pdata[4, 05] = "63.876";
            pdata[1, 06] = "1"; pdata[2, 06] = "CL07"; pdata[3, 06] = "LOTYYYYMMDD06"; pdata[4, 06] = "44.342";
            pdata[1, 07] = "1"; pdata[2, 07] = "CL07"; pdata[3, 07] = "LOTYYYYMMDD07"; pdata[4, 07] = "47.234"; //subgroup 2
            pdata[1, 08] = "1"; pdata[2, 08] = "CL07"; pdata[3, 08] = "LOTYYYYMMDD08"; pdata[4, 08] = "45.834";
            pdata[1, 09] = "1"; pdata[2, 09] = "CL07"; pdata[3, 09] = "LOTYYYYMMDD09"; pdata[4, 09] = "53.987";
            pdata[1, 10] = "1"; pdata[2, 10] = "CL07"; pdata[3, 10] = "LOTYYYYMMDD10"; pdata[4, 10] = "45.234";
            pdata[1, 11] = "1"; pdata[2, 11] = "CL07"; pdata[3, 11] = "LOTYYYYMMDD11"; pdata[4, 11] = "34.987";
            pdata[1, 12] = "1"; pdata[2, 12] = "CL07"; pdata[3, 12] = "LOTYYYYMMDD12"; pdata[4, 12] = "45.423";
            pdata[1, 13] = "1"; pdata[2, 13] = "CL07"; pdata[3, 13] = "LOTYYYYMMDD13"; pdata[4, 13] = "58.324"; //subgroup 3
            pdata[1, 14] = "1"; pdata[2, 14] = "CL07"; pdata[3, 14] = "LOTYYYYMMDD14"; pdata[4, 14] = "43.323";
            pdata[1, 15] = "1"; pdata[2, 15] = "CL07"; pdata[3, 15] = "LOTYYYYMMDD15"; pdata[4, 15] = "38.423";
            pdata[1, 16] = "1"; pdata[2, 16] = "CL07"; pdata[3, 16] = "LOTYYYYMMDD16"; pdata[4, 16] = "45.234";
            pdata[1, 17] = "1"; pdata[2, 17] = "CL07"; pdata[3, 17] = "LOTYYYYMMDD17"; pdata[4, 17] = "53.134";
            pdata[1, 18] = "1"; pdata[2, 18] = "CL07"; pdata[3, 18] = "LOTYYYYMMDD18"; pdata[4, 18] = "42.234";
            pdata[1, 19] = "1"; pdata[2, 19] = "CL08"; pdata[3, 19] = "LOTYYYYMMDD19"; pdata[4, 19] = "58.324"; //subgroup 3
            pdata[1, 20] = "1"; pdata[2, 20] = "CL08"; pdata[3, 20] = "LOTYYYYMMDD20"; pdata[4, 20] = "43.323";
            pdata[1, 21] = "1"; pdata[2, 21] = "CL08"; pdata[3, 21] = "LOTYYYYMMDD21"; pdata[4, 21] = "38.423";
            pdata[1, 22] = "1"; pdata[2, 22] = "CL08"; pdata[3, 22] = "LOTYYYYMMDD22"; pdata[4, 22] = "45.234";
            pdata[1, 23] = "1"; pdata[2, 23] = "CL08"; pdata[3, 23] = "LOTYYYYMMDD23"; pdata[4, 23] = "53.134";
            pdata[1, 24] = "1"; pdata[2, 24] = "CL08"; pdata[3, 24] = "LOTYYYYMMDD24"; pdata[4, 24] = "42.234";

            return pdata;
        }

        public string[,] CTR02Group_01()
        {
            string[,] pdata = new string[5, 2];
            pdata[1, 1] = "1"; pdata[2, 1] = "ProductID-001"; pdata[3, 1] = "LotyyyyMMddID-001"; pdata[4, 1] = "37.42"; //Group-001 - Sub-001

            return pdata;
        }
        /// <summary>
        /// R, S Test Data - 3 Group별
        /// </summary>
        /// <returns></returns>
        public string[,] CTR02Group()
        {
            string[,] pdata = new string[5, 151];
            pdata[1, 1] = "1"; pdata[2, 1] = "ProductID-001"; pdata[3, 1] = "LotyyyyMMddID-001"; pdata[4, 1] = "37.42"; //Group-001 - Sub-001
            pdata[1, 2] = "1"; pdata[2, 2] = "ProductID-001"; pdata[3, 2] = "LotyyyyMMddID-001"; pdata[4, 2] = "1.2";
            pdata[1, 3] = "1"; pdata[2, 3] = "ProductID-001"; pdata[3, 3] = "LotyyyyMMddID-001"; pdata[4, 3] = "23.31";
            pdata[1, 4] = "1"; pdata[2, 4] = "ProductID-001"; pdata[3, 4] = "LotyyyyMMddID-001"; pdata[4, 4] = "76.19";
            pdata[1, 5] = "1"; pdata[2, 5] = "ProductID-001"; pdata[3, 5] = "LotyyyyMMddID-001"; pdata[4, 5] = "78.59";
            pdata[1, 6] = "1"; pdata[2, 6] = "ProductID-001"; pdata[3, 6] = "LotyyyyMMddID-001"; pdata[4, 6] = "15.38";
            pdata[1, 7] = "1"; pdata[2, 7] = "ProductID-001"; pdata[3, 7] = "LotyyyyMMddID-001"; pdata[4, 7] = "51.22";
            pdata[1, 8] = "1"; pdata[2, 8] = "ProductID-001"; pdata[3, 8] = "LotyyyyMMddID-001"; pdata[4, 8] = "4.29";
            pdata[1, 9] = "1"; pdata[2, 9] = "ProductID-001"; pdata[3, 9] = "LotyyyyMMddID-001"; pdata[4, 9] = "20.05";
            pdata[1, 10] = "1"; pdata[2, 10] = "ProductID-001"; pdata[3, 10] = "LotyyyyMMddID-001"; pdata[4, 10] = "43.64";
            pdata[1, 11] = "1"; pdata[2, 11] = "ProductID-001"; pdata[3, 11] = "LotyyyyMMddID-002"; pdata[4, 11] = "19.49"; //Group-001 - Sub-002
            pdata[1, 12] = "1"; pdata[2, 12] = "ProductID-001"; pdata[3, 12] = "LotyyyyMMddID-002"; pdata[4, 12] = "17.12";
            pdata[1, 13] = "1"; pdata[2, 13] = "ProductID-001"; pdata[3, 13] = "LotyyyyMMddID-002"; pdata[4, 13] = "30.58";
            pdata[1, 14] = "1"; pdata[2, 14] = "ProductID-001"; pdata[3, 14] = "LotyyyyMMddID-002"; pdata[4, 14] = "82.66";
            pdata[1, 15] = "1"; pdata[2, 15] = "ProductID-001"; pdata[3, 15] = "LotyyyyMMddID-002"; pdata[4, 15] = "45.34";
            pdata[1, 16] = "1"; pdata[2, 16] = "ProductID-001"; pdata[3, 16] = "LotyyyyMMddID-002"; pdata[4, 16] = "37.29";
            pdata[1, 17] = "1"; pdata[2, 17] = "ProductID-001"; pdata[3, 17] = "LotyyyyMMddID-002"; pdata[4, 17] = "98.37";
            pdata[1, 18] = "1"; pdata[2, 18] = "ProductID-001"; pdata[3, 18] = "LotyyyyMMddID-002"; pdata[4, 18] = "42.47";
            pdata[1, 19] = "1"; pdata[2, 19] = "ProductID-001"; pdata[3, 19] = "LotyyyyMMddID-002"; pdata[4, 19] = "3.78";
            pdata[1, 20] = "1"; pdata[2, 20] = "ProductID-001"; pdata[3, 20] = "LotyyyyMMddID-002"; pdata[4, 20] = "28.41";
            pdata[1, 21] = "1"; pdata[2, 21] = "ProductID-001"; pdata[3, 21] = "LotyyyyMMddID-003"; pdata[4, 21] = "2.5"; //Group-001 - Sub-003
            pdata[1, 22] = "1"; pdata[2, 22] = "ProductID-001"; pdata[3, 22] = "LotyyyyMMddID-003"; pdata[4, 22] = "19.8";
            pdata[1, 23] = "1"; pdata[2, 23] = "ProductID-001"; pdata[3, 23] = "LotyyyyMMddID-003"; pdata[4, 23] = "7.62";
            pdata[1, 24] = "1"; pdata[2, 24] = "ProductID-001"; pdata[3, 24] = "LotyyyyMMddID-003"; pdata[4, 24] = "45.54";
            pdata[1, 25] = "1"; pdata[2, 25] = "ProductID-001"; pdata[3, 25] = "LotyyyyMMddID-003"; pdata[4, 25] = "74.84";
            pdata[1, 26] = "1"; pdata[2, 26] = "ProductID-001"; pdata[3, 26] = "LotyyyyMMddID-003"; pdata[4, 26] = "15.08";
            pdata[1, 27] = "1"; pdata[2, 27] = "ProductID-001"; pdata[3, 27] = "LotyyyyMMddID-003"; pdata[4, 27] = "79.46";
            pdata[1, 28] = "1"; pdata[2, 28] = "ProductID-001"; pdata[3, 28] = "LotyyyyMMddID-003"; pdata[4, 28] = "58.88";
            pdata[1, 29] = "1"; pdata[2, 29] = "ProductID-001"; pdata[3, 29] = "LotyyyyMMddID-003"; pdata[4, 29] = "25.88";
            pdata[1, 30] = "1"; pdata[2, 30] = "ProductID-001"; pdata[3, 30] = "LotyyyyMMddID-003"; pdata[4, 30] = "90.64";
            pdata[1, 31] = "1"; pdata[2, 31] = "ProductID-001"; pdata[3, 31] = "LotyyyyMMddID-004"; pdata[4, 31] = "49.1"; //Group-001 - Sub-004
            pdata[1, 32] = "1"; pdata[2, 32] = "ProductID-001"; pdata[3, 32] = "LotyyyyMMddID-004"; pdata[4, 32] = "74.44";
            pdata[1, 33] = "1"; pdata[2, 33] = "ProductID-001"; pdata[3, 33] = "LotyyyyMMddID-004"; pdata[4, 33] = "22.85";
            pdata[1, 34] = "1"; pdata[2, 34] = "ProductID-001"; pdata[3, 34] = "LotyyyyMMddID-004"; pdata[4, 34] = "40.15";
            pdata[1, 35] = "1"; pdata[2, 35] = "ProductID-001"; pdata[3, 35] = "LotyyyyMMddID-004"; pdata[4, 35] = "80.57";
            pdata[1, 36] = "1"; pdata[2, 36] = "ProductID-001"; pdata[3, 36] = "LotyyyyMMddID-004"; pdata[4, 36] = "27.41";
            pdata[1, 37] = "1"; pdata[2, 37] = "ProductID-001"; pdata[3, 37] = "LotyyyyMMddID-004"; pdata[4, 37] = "76.03";
            pdata[1, 38] = "1"; pdata[2, 38] = "ProductID-001"; pdata[3, 38] = "LotyyyyMMddID-004"; pdata[4, 38] = "62.69";
            pdata[1, 39] = "1"; pdata[2, 39] = "ProductID-001"; pdata[3, 39] = "LotyyyyMMddID-004"; pdata[4, 39] = "30.36";
            pdata[1, 40] = "1"; pdata[2, 40] = "ProductID-001"; pdata[3, 40] = "LotyyyyMMddID-004"; pdata[4, 40] = "6.68";
            pdata[1, 41] = "1"; pdata[2, 41] = "ProductID-001"; pdata[3, 41] = "LotyyyyMMddID-005"; pdata[4, 41] = "14.29"; //Group-001 - Sub-005
            pdata[1, 42] = "1"; pdata[2, 42] = "ProductID-001"; pdata[3, 42] = "LotyyyyMMddID-005"; pdata[4, 42] = "92.6";
            pdata[1, 43] = "1"; pdata[2, 43] = "ProductID-001"; pdata[3, 43] = "LotyyyyMMddID-005"; pdata[4, 43] = "19.3";
            pdata[1, 44] = "1"; pdata[2, 44] = "ProductID-001"; pdata[3, 44] = "LotyyyyMMddID-005"; pdata[4, 44] = "50.52";
            pdata[1, 45] = "1"; pdata[2, 45] = "ProductID-001"; pdata[3, 45] = "LotyyyyMMddID-005"; pdata[4, 45] = "78.54";
            pdata[1, 46] = "1"; pdata[2, 46] = "ProductID-001"; pdata[3, 46] = "LotyyyyMMddID-005"; pdata[4, 46] = "22.14";
            pdata[1, 47] = "1"; pdata[2, 47] = "ProductID-001"; pdata[3, 47] = "LotyyyyMMddID-005"; pdata[4, 47] = "88.64";
            pdata[1, 48] = "1"; pdata[2, 48] = "ProductID-001"; pdata[3, 48] = "LotyyyyMMddID-005"; pdata[4, 48] = "92.92";
            pdata[1, 49] = "1"; pdata[2, 49] = "ProductID-001"; pdata[3, 49] = "LotyyyyMMddID-005"; pdata[4, 49] = "75.74";
            pdata[1, 50] = "1"; pdata[2, 50] = "ProductID-001"; pdata[3, 50] = "LotyyyyMMddID-005"; pdata[4, 50] = "58.21";
            pdata[1, 51] = "1"; pdata[2, 51] = "ProductID-002"; pdata[3, 51] = "LotyyyyMMddID-001"; pdata[4, 51] = "23.3"; //Group-002 - Sub-001
            pdata[1, 52] = "1"; pdata[2, 52] = "ProductID-002"; pdata[3, 52] = "LotyyyyMMddID-001"; pdata[4, 52] = "73.17";
            pdata[1, 53] = "1"; pdata[2, 53] = "ProductID-002"; pdata[3, 53] = "LotyyyyMMddID-001"; pdata[4, 53] = "58.73";
            pdata[1, 54] = "1"; pdata[2, 54] = "ProductID-002"; pdata[3, 54] = "LotyyyyMMddID-001"; pdata[4, 54] = "31.76";
            pdata[1, 55] = "1"; pdata[2, 55] = "ProductID-002"; pdata[3, 55] = "LotyyyyMMddID-001"; pdata[4, 55] = "84.88";
            pdata[1, 56] = "1"; pdata[2, 56] = "ProductID-002"; pdata[3, 56] = "LotyyyyMMddID-001"; pdata[4, 56] = "58.88";
            pdata[1, 57] = "1"; pdata[2, 57] = "ProductID-002"; pdata[3, 57] = "LotyyyyMMddID-001"; pdata[4, 57] = "83.22";
            pdata[1, 58] = "1"; pdata[2, 58] = "ProductID-002"; pdata[3, 58] = "LotyyyyMMddID-001"; pdata[4, 58] = "50.2";
            pdata[1, 59] = "1"; pdata[2, 59] = "ProductID-002"; pdata[3, 59] = "LotyyyyMMddID-001"; pdata[4, 59] = "17.15";
            pdata[1, 60] = "1"; pdata[2, 60] = "ProductID-002"; pdata[3, 60] = "LotyyyyMMddID-001"; pdata[4, 60] = "26.92";
            pdata[1, 61] = "1"; pdata[2, 61] = "ProductID-002"; pdata[3, 61] = "LotyyyyMMddID-002"; pdata[4, 61] = "35.87"; //Group-002 - Sub-002
            pdata[1, 62] = "1"; pdata[2, 62] = "ProductID-002"; pdata[3, 62] = "LotyyyyMMddID-002"; pdata[4, 62] = "36.27";
            pdata[1, 63] = "1"; pdata[2, 63] = "ProductID-002"; pdata[3, 63] = "LotyyyyMMddID-002"; pdata[4, 63] = "36.13";
            pdata[1, 64] = "1"; pdata[2, 64] = "ProductID-002"; pdata[3, 64] = "LotyyyyMMddID-002"; pdata[4, 64] = "53.13";
            pdata[1, 65] = "1"; pdata[2, 65] = "ProductID-002"; pdata[3, 65] = "LotyyyyMMddID-002"; pdata[4, 65] = "11.7";
            pdata[1, 66] = "1"; pdata[2, 66] = "ProductID-002"; pdata[3, 66] = "LotyyyyMMddID-002"; pdata[4, 66] = "93.56";
            pdata[1, 67] = "1"; pdata[2, 67] = "ProductID-002"; pdata[3, 67] = "LotyyyyMMddID-002"; pdata[4, 67] = "77.85";
            pdata[1, 68] = "1"; pdata[2, 68] = "ProductID-002"; pdata[3, 68] = "LotyyyyMMddID-002"; pdata[4, 68] = "45.83";
            pdata[1, 69] = "1"; pdata[2, 69] = "ProductID-002"; pdata[3, 69] = "LotyyyyMMddID-002"; pdata[4, 69] = "55.75";
            pdata[1, 70] = "1"; pdata[2, 70] = "ProductID-002"; pdata[3, 70] = "LotyyyyMMddID-002"; pdata[4, 70] = "3.45";
            pdata[1, 71] = "1"; pdata[2, 71] = "ProductID-002"; pdata[3, 71] = "LotyyyyMMddID-003"; pdata[4, 71] = "98.74"; //Group-002 - Sub-003
            pdata[1, 72] = "1"; pdata[2, 72] = "ProductID-002"; pdata[3, 72] = "LotyyyyMMddID-003"; pdata[4, 72] = "68.89";
            pdata[1, 73] = "1"; pdata[2, 73] = "ProductID-002"; pdata[3, 73] = "LotyyyyMMddID-003"; pdata[4, 73] = "95.33";
            pdata[1, 74] = "1"; pdata[2, 74] = "ProductID-002"; pdata[3, 74] = "LotyyyyMMddID-003"; pdata[4, 74] = "31.66";
            pdata[1, 75] = "1"; pdata[2, 75] = "ProductID-002"; pdata[3, 75] = "LotyyyyMMddID-003"; pdata[4, 75] = "14.72";
            pdata[1, 76] = "1"; pdata[2, 76] = "ProductID-002"; pdata[3, 76] = "LotyyyyMMddID-003"; pdata[4, 76] = "75.61";
            pdata[1, 77] = "1"; pdata[2, 77] = "ProductID-002"; pdata[3, 77] = "LotyyyyMMddID-003"; pdata[4, 77] = "49.66";
            pdata[1, 78] = "1"; pdata[2, 78] = "ProductID-002"; pdata[3, 78] = "LotyyyyMMddID-003"; pdata[4, 78] = "86.36";
            pdata[1, 79] = "1"; pdata[2, 79] = "ProductID-002"; pdata[3, 79] = "LotyyyyMMddID-003"; pdata[4, 79] = "93.47";
            pdata[1, 80] = "1"; pdata[2, 80] = "ProductID-002"; pdata[3, 80] = "LotyyyyMMddID-003"; pdata[4, 80] = "81.45";
            pdata[1, 81] = "1"; pdata[2, 81] = "ProductID-002"; pdata[3, 81] = "LotyyyyMMddID-004"; pdata[4, 81] = "21.18"; //Group-002 - Sub-004
            pdata[1, 82] = "1"; pdata[2, 82] = "ProductID-002"; pdata[3, 82] = "LotyyyyMMddID-004"; pdata[4, 82] = "66.15";
            pdata[1, 83] = "1"; pdata[2, 83] = "ProductID-002"; pdata[3, 83] = "LotyyyyMMddID-004"; pdata[4, 83] = "95.03";
            pdata[1, 84] = "1"; pdata[2, 84] = "ProductID-002"; pdata[3, 84] = "LotyyyyMMddID-004"; pdata[4, 84] = "11.97";
            pdata[1, 85] = "1"; pdata[2, 85] = "ProductID-002"; pdata[3, 85] = "LotyyyyMMddID-004"; pdata[4, 85] = "77.66";
            pdata[1, 86] = "1"; pdata[2, 86] = "ProductID-002"; pdata[3, 86] = "LotyyyyMMddID-004"; pdata[4, 86] = "64.41";
            pdata[1, 87] = "1"; pdata[2, 87] = "ProductID-002"; pdata[3, 87] = "LotyyyyMMddID-004"; pdata[4, 87] = "12.8";
            pdata[1, 88] = "1"; pdata[2, 88] = "ProductID-002"; pdata[3, 88] = "LotyyyyMMddID-004"; pdata[4, 88] = "93.61";
            pdata[1, 89] = "1"; pdata[2, 89] = "ProductID-002"; pdata[3, 89] = "LotyyyyMMddID-004"; pdata[4, 89] = "19.47";
            pdata[1, 90] = "1"; pdata[2, 90] = "ProductID-002"; pdata[3, 90] = "LotyyyyMMddID-004"; pdata[4, 90] = "31.03";
            pdata[1, 91] = "1"; pdata[2, 91] = "ProductID-002"; pdata[3, 91] = "LotyyyyMMddID-005"; pdata[4, 91] = "82.86"; //Group-002 - Sub-005
            pdata[1, 92] = "1"; pdata[2, 92] = "ProductID-002"; pdata[3, 92] = "LotyyyyMMddID-005"; pdata[4, 92] = "99.38";
            pdata[1, 93] = "1"; pdata[2, 93] = "ProductID-002"; pdata[3, 93] = "LotyyyyMMddID-005"; pdata[4, 93] = "94.33";
            pdata[1, 94] = "1"; pdata[2, 94] = "ProductID-002"; pdata[3, 94] = "LotyyyyMMddID-005"; pdata[4, 94] = "7.12";
            pdata[1, 95] = "1"; pdata[2, 95] = "ProductID-002"; pdata[3, 95] = "LotyyyyMMddID-005"; pdata[4, 95] = "98.09";
            pdata[1, 96] = "1"; pdata[2, 96] = "ProductID-002"; pdata[3, 96] = "LotyyyyMMddID-005"; pdata[4, 96] = "89.09";
            pdata[1, 97] = "1"; pdata[2, 97] = "ProductID-002"; pdata[3, 97] = "LotyyyyMMddID-005"; pdata[4, 97] = "94.58";
            pdata[1, 98] = "1"; pdata[2, 98] = "ProductID-002"; pdata[3, 98] = "LotyyyyMMddID-005"; pdata[4, 98] = "24.81";
            pdata[1, 99] = "1"; pdata[2, 99] = "ProductID-002"; pdata[3, 99] = "LotyyyyMMddID-005"; pdata[4, 99] = "8.54";
            pdata[1, 100] = "1"; pdata[2, 100] = "ProductID-002"; pdata[3, 100] = "LotyyyyMMddID-005"; pdata[4, 100] = "94.93";
            pdata[1, 101] = "1"; pdata[2, 101] = "ProductID-003"; pdata[3, 101] = "LotyyyyMMddID-001"; pdata[4, 101] = "23.33"; //Group-003 - Sub-001
            pdata[1, 102] = "1"; pdata[2, 102] = "ProductID-003"; pdata[3, 102] = "LotyyyyMMddID-001"; pdata[4, 102] = "89.47";
            pdata[1, 103] = "1"; pdata[2, 103] = "ProductID-003"; pdata[3, 103] = "LotyyyyMMddID-001"; pdata[4, 103] = "8.34";
            pdata[1, 104] = "1"; pdata[2, 104] = "ProductID-003"; pdata[3, 104] = "LotyyyyMMddID-001"; pdata[4, 104] = "50.68";
            pdata[1, 105] = "1"; pdata[2, 105] = "ProductID-003"; pdata[3, 105] = "LotyyyyMMddID-001"; pdata[4, 105] = "72.25";
            pdata[1, 106] = "1"; pdata[2, 106] = "ProductID-003"; pdata[3, 106] = "LotyyyyMMddID-001"; pdata[4, 106] = "17.47";
            pdata[1, 107] = "1"; pdata[2, 107] = "ProductID-003"; pdata[3, 107] = "LotyyyyMMddID-001"; pdata[4, 107] = "16.78";
            pdata[1, 108] = "1"; pdata[2, 108] = "ProductID-003"; pdata[3, 108] = "LotyyyyMMddID-001"; pdata[4, 108] = "63.35";
            pdata[1, 109] = "1"; pdata[2, 109] = "ProductID-003"; pdata[3, 109] = "LotyyyyMMddID-001"; pdata[4, 109] = "19.28";
            pdata[1, 110] = "1"; pdata[2, 110] = "ProductID-003"; pdata[3, 110] = "LotyyyyMMddID-001"; pdata[4, 110] = "20.62";
            pdata[1, 111] = "1"; pdata[2, 111] = "ProductID-003"; pdata[3, 111] = "LotyyyyMMddID-002"; pdata[4, 111] = "95.99"; //Group-003 - Sub-002
            pdata[1, 112] = "1"; pdata[2, 112] = "ProductID-003"; pdata[3, 112] = "LotyyyyMMddID-002"; pdata[4, 112] = "99.68";
            pdata[1, 113] = "1"; pdata[2, 113] = "ProductID-003"; pdata[3, 113] = "LotyyyyMMddID-002"; pdata[4, 113] = "76.56";
            pdata[1, 114] = "1"; pdata[2, 114] = "ProductID-003"; pdata[3, 114] = "LotyyyyMMddID-002"; pdata[4, 114] = "47.54";
            pdata[1, 115] = "1"; pdata[2, 115] = "ProductID-003"; pdata[3, 115] = "LotyyyyMMddID-002"; pdata[4, 115] = "56.13";
            pdata[1, 116] = "1"; pdata[2, 116] = "ProductID-003"; pdata[3, 116] = "LotyyyyMMddID-002"; pdata[4, 116] = "53.65";
            pdata[1, 117] = "1"; pdata[2, 117] = "ProductID-003"; pdata[3, 117] = "LotyyyyMMddID-002"; pdata[4, 117] = "52.38";
            pdata[1, 118] = "1"; pdata[2, 118] = "ProductID-003"; pdata[3, 118] = "LotyyyyMMddID-002"; pdata[4, 118] = "80.49";
            pdata[1, 119] = "1"; pdata[2, 119] = "ProductID-003"; pdata[3, 119] = "LotyyyyMMddID-002"; pdata[4, 119] = "96.53";
            pdata[1, 120] = "1"; pdata[2, 120] = "ProductID-003"; pdata[3, 120] = "LotyyyyMMddID-002"; pdata[4, 120] = "30.6";
            pdata[1, 121] = "1"; pdata[2, 121] = "ProductID-003"; pdata[3, 121] = "LotyyyyMMddID-003"; pdata[4, 121] = "82.39"; //Group-003 - Sub-003
            pdata[1, 122] = "1"; pdata[2, 122] = "ProductID-003"; pdata[3, 122] = "LotyyyyMMddID-003"; pdata[4, 122] = "39.74";
            pdata[1, 123] = "1"; pdata[2, 123] = "ProductID-003"; pdata[3, 123] = "LotyyyyMMddID-003"; pdata[4, 123] = "92.43";
            pdata[1, 124] = "1"; pdata[2, 124] = "ProductID-003"; pdata[3, 124] = "LotyyyyMMddID-003"; pdata[4, 124] = "93.16";
            pdata[1, 125] = "1"; pdata[2, 125] = "ProductID-003"; pdata[3, 125] = "LotyyyyMMddID-003"; pdata[4, 125] = "95.89";
            pdata[1, 126] = "1"; pdata[2, 126] = "ProductID-003"; pdata[3, 126] = "LotyyyyMMddID-003"; pdata[4, 126] = "62.8";
            pdata[1, 127] = "1"; pdata[2, 127] = "ProductID-003"; pdata[3, 127] = "LotyyyyMMddID-003"; pdata[4, 127] = "34.32";
            pdata[1, 128] = "1"; pdata[2, 128] = "ProductID-003"; pdata[3, 128] = "LotyyyyMMddID-003"; pdata[4, 128] = "98.24";
            pdata[1, 129] = "1"; pdata[2, 129] = "ProductID-003"; pdata[3, 129] = "LotyyyyMMddID-003"; pdata[4, 129] = "34.21";
            pdata[1, 130] = "1"; pdata[2, 130] = "ProductID-003"; pdata[3, 130] = "LotyyyyMMddID-003"; pdata[4, 130] = "70.52";
            pdata[1, 131] = "1"; pdata[2, 131] = "ProductID-003"; pdata[3, 131] = "LotyyyyMMddID-004"; pdata[4, 131] = "87.96"; //Group-003 - Sub-004
            pdata[1, 132] = "1"; pdata[2, 132] = "ProductID-003"; pdata[3, 132] = "LotyyyyMMddID-004"; pdata[4, 132] = "12.6";
            pdata[1, 133] = "1"; pdata[2, 133] = "ProductID-003"; pdata[3, 133] = "LotyyyyMMddID-004"; pdata[4, 133] = "71.52";
            pdata[1, 134] = "1"; pdata[2, 134] = "ProductID-003"; pdata[3, 134] = "LotyyyyMMddID-004"; pdata[4, 134] = "95.78";
            pdata[1, 135] = "1"; pdata[2, 135] = "ProductID-003"; pdata[3, 135] = "LotyyyyMMddID-004"; pdata[4, 135] = "98.14";
            pdata[1, 136] = "1"; pdata[2, 136] = "ProductID-003"; pdata[3, 136] = "LotyyyyMMddID-004"; pdata[4, 136] = "38.66";
            pdata[1, 137] = "1"; pdata[2, 137] = "ProductID-003"; pdata[3, 137] = "LotyyyyMMddID-004"; pdata[4, 137] = "97.6";
            pdata[1, 138] = "1"; pdata[2, 138] = "ProductID-003"; pdata[3, 138] = "LotyyyyMMddID-004"; pdata[4, 138] = "79.57";
            pdata[1, 139] = "1"; pdata[2, 139] = "ProductID-003"; pdata[3, 139] = "LotyyyyMMddID-004"; pdata[4, 139] = "60.25";
            pdata[1, 140] = "1"; pdata[2, 140] = "ProductID-003"; pdata[3, 140] = "LotyyyyMMddID-004"; pdata[4, 140] = "76.34";
            pdata[1, 141] = "1"; pdata[2, 141] = "ProductID-003"; pdata[3, 141] = "LotyyyyMMddID-005"; pdata[4, 141] = "63.31"; //Group-003 - Sub-005
            pdata[1, 142] = "1"; pdata[2, 142] = "ProductID-003"; pdata[3, 142] = "LotyyyyMMddID-005"; pdata[4, 142] = "58.67";
            pdata[1, 143] = "1"; pdata[2, 143] = "ProductID-003"; pdata[3, 143] = "LotyyyyMMddID-005"; pdata[4, 143] = "50.73";
            pdata[1, 144] = "1"; pdata[2, 144] = "ProductID-003"; pdata[3, 144] = "LotyyyyMMddID-005"; pdata[4, 144] = "31.2";
            pdata[1, 145] = "1"; pdata[2, 145] = "ProductID-003"; pdata[3, 145] = "LotyyyyMMddID-005"; pdata[4, 145] = "39.56";
            pdata[1, 146] = "1"; pdata[2, 146] = "ProductID-003"; pdata[3, 146] = "LotyyyyMMddID-005"; pdata[4, 146] = "15.76";
            pdata[1, 147] = "1"; pdata[2, 147] = "ProductID-003"; pdata[3, 147] = "LotyyyyMMddID-005"; pdata[4, 147] = "82.78";
            pdata[1, 148] = "1"; pdata[2, 148] = "ProductID-003"; pdata[3, 148] = "LotyyyyMMddID-005"; pdata[4, 148] = "73.86";
            pdata[1, 149] = "1"; pdata[2, 149] = "ProductID-003"; pdata[3, 149] = "LotyyyyMMddID-005"; pdata[4, 149] = "27.78";
            pdata[1, 150] = "1"; pdata[2, 150] = "ProductID-003"; pdata[3, 150] = "LotyyyyMMddID-005"; pdata[4, 150] = "3.4";

            return pdata;
        }

        /// <summary>
        /// R, S Test Data - 12 Group
        /// </summary>
        /// <returns></returns>
        public string[,] CTR03Group()
        {
            int y = 551; // 601
            string[,] pdata = new string[5, y];
            pdata[1, 1] = "1"; pdata[2, 1] = "ProductID-001"; pdata[3, 1] = "LotyyyyMMddID-001"; pdata[4, 1] = "12.6"; //Group-001 - Sub-001
            pdata[1, 2] = "1"; pdata[2, 2] = "ProductID-001"; pdata[3, 2] = "LotyyyyMMddID-001"; pdata[4, 2] = "58.44";
            pdata[1, 3] = "1"; pdata[2, 3] = "ProductID-001"; pdata[3, 3] = "LotyyyyMMddID-001"; pdata[4, 3] = "76.61";
            pdata[1, 4] = "1"; pdata[2, 4] = "ProductID-001"; pdata[3, 4] = "LotyyyyMMddID-001"; pdata[4, 4] = "50.15";
            pdata[1, 5] = "1"; pdata[2, 5] = "ProductID-001"; pdata[3, 5] = "LotyyyyMMddID-001"; pdata[4, 5] = "59.08";
            pdata[1, 6] = "1"; pdata[2, 6] = "ProductID-001"; pdata[3, 6] = "LotyyyyMMddID-001"; pdata[4, 6] = "2.2";
            pdata[1, 7] = "1"; pdata[2, 7] = "ProductID-001"; pdata[3, 7] = "LotyyyyMMddID-001"; pdata[4, 7] = "59.5";
            pdata[1, 8] = "1"; pdata[2, 8] = "ProductID-001"; pdata[3, 8] = "LotyyyyMMddID-001"; pdata[4, 8] = "55.3";
            pdata[1, 9] = "1"; pdata[2, 9] = "ProductID-001"; pdata[3, 9] = "LotyyyyMMddID-001"; pdata[4, 9] = "52.04";
            pdata[1, 10] = "1"; pdata[2, 10] = "ProductID-001"; pdata[3, 10] = "LotyyyyMMddID-001"; pdata[4, 10] = "27.92";
            pdata[1, 11] = "1"; pdata[2, 11] = "ProductID-001"; pdata[3, 11] = "LotyyyyMMddID-002"; pdata[4, 11] = "11.96"; //Group-001 - Sub-002
            pdata[1, 12] = "1"; pdata[2, 12] = "ProductID-001"; pdata[3, 12] = "LotyyyyMMddID-002"; pdata[4, 12] = "59.47";
            pdata[1, 13] = "1"; pdata[2, 13] = "ProductID-001"; pdata[3, 13] = "LotyyyyMMddID-002"; pdata[4, 13] = "16.43";
            pdata[1, 14] = "1"; pdata[2, 14] = "ProductID-001"; pdata[3, 14] = "LotyyyyMMddID-002"; pdata[4, 14] = "99.17";
            pdata[1, 15] = "1"; pdata[2, 15] = "ProductID-001"; pdata[3, 15] = "LotyyyyMMddID-002"; pdata[4, 15] = "79.19";
            pdata[1, 16] = "1"; pdata[2, 16] = "ProductID-001"; pdata[3, 16] = "LotyyyyMMddID-002"; pdata[4, 16] = "82.39";
            pdata[1, 17] = "1"; pdata[2, 17] = "ProductID-001"; pdata[3, 17] = "LotyyyyMMddID-002"; pdata[4, 17] = "2.61";
            pdata[1, 18] = "1"; pdata[2, 18] = "ProductID-001"; pdata[3, 18] = "LotyyyyMMddID-002"; pdata[4, 18] = "44.99";
            pdata[1, 19] = "1"; pdata[2, 19] = "ProductID-001"; pdata[3, 19] = "LotyyyyMMddID-002"; pdata[4, 19] = "11.82";
            pdata[1, 20] = "1"; pdata[2, 20] = "ProductID-001"; pdata[3, 20] = "LotyyyyMMddID-002"; pdata[4, 20] = "71.47";
            pdata[1, 21] = "1"; pdata[2, 21] = "ProductID-001"; pdata[3, 21] = "LotyyyyMMddID-003"; pdata[4, 21] = "55.91"; //Group-001 - Sub-003
            pdata[1, 22] = "1"; pdata[2, 22] = "ProductID-001"; pdata[3, 22] = "LotyyyyMMddID-003"; pdata[4, 22] = "6.37";
            pdata[1, 23] = "1"; pdata[2, 23] = "ProductID-001"; pdata[3, 23] = "LotyyyyMMddID-003"; pdata[4, 23] = "76.66";
            pdata[1, 24] = "1"; pdata[2, 24] = "ProductID-001"; pdata[3, 24] = "LotyyyyMMddID-003"; pdata[4, 24] = "89.24";
            pdata[1, 25] = "1"; pdata[2, 25] = "ProductID-001"; pdata[3, 25] = "LotyyyyMMddID-003"; pdata[4, 25] = "87.6";
            pdata[1, 26] = "1"; pdata[2, 26] = "ProductID-001"; pdata[3, 26] = "LotyyyyMMddID-003"; pdata[4, 26] = "25.49";
            pdata[1, 27] = "1"; pdata[2, 27] = "ProductID-001"; pdata[3, 27] = "LotyyyyMMddID-003"; pdata[4, 27] = "16.97";
            pdata[1, 28] = "1"; pdata[2, 28] = "ProductID-001"; pdata[3, 28] = "LotyyyyMMddID-003"; pdata[4, 28] = "77.16";
            pdata[1, 29] = "1"; pdata[2, 29] = "ProductID-001"; pdata[3, 29] = "LotyyyyMMddID-003"; pdata[4, 29] = "1.11";
            pdata[1, 30] = "1"; pdata[2, 30] = "ProductID-001"; pdata[3, 30] = "LotyyyyMMddID-003"; pdata[4, 30] = "28.33";
            pdata[1, 31] = "1"; pdata[2, 31] = "ProductID-001"; pdata[3, 31] = "LotyyyyMMddID-004"; pdata[4, 31] = "61.34"; //Group-001 - Sub-004
            pdata[1, 32] = "1"; pdata[2, 32] = "ProductID-001"; pdata[3, 32] = "LotyyyyMMddID-004"; pdata[4, 32] = "35.41";
            pdata[1, 33] = "1"; pdata[2, 33] = "ProductID-001"; pdata[3, 33] = "LotyyyyMMddID-004"; pdata[4, 33] = "25.63";
            pdata[1, 34] = "1"; pdata[2, 34] = "ProductID-001"; pdata[3, 34] = "LotyyyyMMddID-004"; pdata[4, 34] = "18.97";
            pdata[1, 35] = "1"; pdata[2, 35] = "ProductID-001"; pdata[3, 35] = "LotyyyyMMddID-004"; pdata[4, 35] = "7.55";
            pdata[1, 36] = "1"; pdata[2, 36] = "ProductID-001"; pdata[3, 36] = "LotyyyyMMddID-004"; pdata[4, 36] = "19.7";
            pdata[1, 37] = "1"; pdata[2, 37] = "ProductID-001"; pdata[3, 37] = "LotyyyyMMddID-004"; pdata[4, 37] = "33.8";
            pdata[1, 38] = "1"; pdata[2, 38] = "ProductID-001"; pdata[3, 38] = "LotyyyyMMddID-004"; pdata[4, 38] = "37.2";
            pdata[1, 39] = "1"; pdata[2, 39] = "ProductID-001"; pdata[3, 39] = "LotyyyyMMddID-004"; pdata[4, 39] = "90.23";
            pdata[1, 40] = "1"; pdata[2, 40] = "ProductID-001"; pdata[3, 40] = "LotyyyyMMddID-004"; pdata[4, 40] = "70.49";
            pdata[1, 41] = "1"; pdata[2, 41] = "ProductID-001"; pdata[3, 41] = "LotyyyyMMddID-005"; pdata[4, 41] = "54.76"; //Group-001 - Sub-005
            pdata[1, 42] = "1"; pdata[2, 42] = "ProductID-001"; pdata[3, 42] = "LotyyyyMMddID-005"; pdata[4, 42] = "30.2";
            pdata[1, 43] = "1"; pdata[2, 43] = "ProductID-001"; pdata[3, 43] = "LotyyyyMMddID-005"; pdata[4, 43] = "93.34";
            pdata[1, 44] = "1"; pdata[2, 44] = "ProductID-001"; pdata[3, 44] = "LotyyyyMMddID-005"; pdata[4, 44] = "23.05";
            pdata[1, 45] = "1"; pdata[2, 45] = "ProductID-001"; pdata[3, 45] = "LotyyyyMMddID-005"; pdata[4, 45] = "84.28";
            pdata[1, 46] = "1"; pdata[2, 46] = "ProductID-001"; pdata[3, 46] = "LotyyyyMMddID-005"; pdata[4, 46] = "99.99";
            pdata[1, 47] = "1"; pdata[2, 47] = "ProductID-001"; pdata[3, 47] = "LotyyyyMMddID-005"; pdata[4, 47] = "54.38";
            pdata[1, 48] = "1"; pdata[2, 48] = "ProductID-001"; pdata[3, 48] = "LotyyyyMMddID-005"; pdata[4, 48] = "85.22";
            pdata[1, 49] = "1"; pdata[2, 49] = "ProductID-001"; pdata[3, 49] = "LotyyyyMMddID-005"; pdata[4, 49] = "56.64";
            pdata[1, 50] = "1"; pdata[2, 50] = "ProductID-001"; pdata[3, 50] = "LotyyyyMMddID-005"; pdata[4, 50] = "12.14";
            pdata[1, 51] = "1"; pdata[2, 51] = "ProductID-002"; pdata[3, 51] = "LotyyyyMMddID-001"; pdata[4, 51] = "49.91"; //Group-002 - Sub-001
            pdata[1, 52] = "1"; pdata[2, 52] = "ProductID-002"; pdata[3, 52] = "LotyyyyMMddID-001"; pdata[4, 52] = "17.32";
            pdata[1, 53] = "1"; pdata[2, 53] = "ProductID-002"; pdata[3, 53] = "LotyyyyMMddID-001"; pdata[4, 53] = "41.54";
            pdata[1, 54] = "1"; pdata[2, 54] = "ProductID-002"; pdata[3, 54] = "LotyyyyMMddID-001"; pdata[4, 54] = "17.36";
            pdata[1, 55] = "1"; pdata[2, 55] = "ProductID-002"; pdata[3, 55] = "LotyyyyMMddID-001"; pdata[4, 55] = "60.58";
            pdata[1, 56] = "1"; pdata[2, 56] = "ProductID-002"; pdata[3, 56] = "LotyyyyMMddID-001"; pdata[4, 56] = "25.78";
            pdata[1, 57] = "1"; pdata[2, 57] = "ProductID-002"; pdata[3, 57] = "LotyyyyMMddID-001"; pdata[4, 57] = "41.79";
            pdata[1, 58] = "1"; pdata[2, 58] = "ProductID-002"; pdata[3, 58] = "LotyyyyMMddID-001"; pdata[4, 58] = "79.85";
            pdata[1, 59] = "1"; pdata[2, 59] = "ProductID-002"; pdata[3, 59] = "LotyyyyMMddID-001"; pdata[4, 59] = "4.16";
            pdata[1, 60] = "1"; pdata[2, 60] = "ProductID-002"; pdata[3, 60] = "LotyyyyMMddID-001"; pdata[4, 60] = "48.91";
            pdata[1, 61] = "1"; pdata[2, 61] = "ProductID-002"; pdata[3, 61] = "LotyyyyMMddID-002"; pdata[4, 61] = "40.14"; //Group-002 - Sub-002
            pdata[1, 62] = "1"; pdata[2, 62] = "ProductID-002"; pdata[3, 62] = "LotyyyyMMddID-002"; pdata[4, 62] = "14.78";
            pdata[1, 63] = "1"; pdata[2, 63] = "ProductID-002"; pdata[3, 63] = "LotyyyyMMddID-002"; pdata[4, 63] = "55.2";
            pdata[1, 64] = "1"; pdata[2, 64] = "ProductID-002"; pdata[3, 64] = "LotyyyyMMddID-002"; pdata[4, 64] = "16.94";
            pdata[1, 65] = "1"; pdata[2, 65] = "ProductID-002"; pdata[3, 65] = "LotyyyyMMddID-002"; pdata[4, 65] = "94.15";
            pdata[1, 66] = "1"; pdata[2, 66] = "ProductID-002"; pdata[3, 66] = "LotyyyyMMddID-002"; pdata[4, 66] = "63.27";
            pdata[1, 67] = "1"; pdata[2, 67] = "ProductID-002"; pdata[3, 67] = "LotyyyyMMddID-002"; pdata[4, 67] = "11.56";
            pdata[1, 68] = "1"; pdata[2, 68] = "ProductID-002"; pdata[3, 68] = "LotyyyyMMddID-002"; pdata[4, 68] = "1.62";
            pdata[1, 69] = "1"; pdata[2, 69] = "ProductID-002"; pdata[3, 69] = "LotyyyyMMddID-002"; pdata[4, 69] = "59.97";
            pdata[1, 70] = "1"; pdata[2, 70] = "ProductID-002"; pdata[3, 70] = "LotyyyyMMddID-002"; pdata[4, 70] = "79.39";
            pdata[1, 71] = "1"; pdata[2, 71] = "ProductID-002"; pdata[3, 71] = "LotyyyyMMddID-003"; pdata[4, 71] = "18.87"; //Group-002 - Sub-003
            pdata[1, 72] = "1"; pdata[2, 72] = "ProductID-002"; pdata[3, 72] = "LotyyyyMMddID-003"; pdata[4, 72] = "44.27";
            pdata[1, 73] = "1"; pdata[2, 73] = "ProductID-002"; pdata[3, 73] = "LotyyyyMMddID-003"; pdata[4, 73] = "3.22";
            pdata[1, 74] = "1"; pdata[2, 74] = "ProductID-002"; pdata[3, 74] = "LotyyyyMMddID-003"; pdata[4, 74] = "58.75";
            pdata[1, 75] = "1"; pdata[2, 75] = "ProductID-002"; pdata[3, 75] = "LotyyyyMMddID-003"; pdata[4, 75] = "59.01";
            pdata[1, 76] = "1"; pdata[2, 76] = "ProductID-002"; pdata[3, 76] = "LotyyyyMMddID-003"; pdata[4, 76] = "18.41";
            pdata[1, 77] = "1"; pdata[2, 77] = "ProductID-002"; pdata[3, 77] = "LotyyyyMMddID-003"; pdata[4, 77] = "17.21";
            pdata[1, 78] = "1"; pdata[2, 78] = "ProductID-002"; pdata[3, 78] = "LotyyyyMMddID-003"; pdata[4, 78] = "73.35";
            pdata[1, 79] = "1"; pdata[2, 79] = "ProductID-002"; pdata[3, 79] = "LotyyyyMMddID-003"; pdata[4, 79] = "78.39";
            pdata[1, 80] = "1"; pdata[2, 80] = "ProductID-002"; pdata[3, 80] = "LotyyyyMMddID-003"; pdata[4, 80] = "76.21";
            pdata[1, 81] = "1"; pdata[2, 81] = "ProductID-002"; pdata[3, 81] = "LotyyyyMMddID-004"; pdata[4, 81] = "39.22"; //Group-002 - Sub-004
            pdata[1, 82] = "1"; pdata[2, 82] = "ProductID-002"; pdata[3, 82] = "LotyyyyMMddID-004"; pdata[4, 82] = "41.46";
            pdata[1, 83] = "1"; pdata[2, 83] = "ProductID-002"; pdata[3, 83] = "LotyyyyMMddID-004"; pdata[4, 83] = "94.98";
            pdata[1, 84] = "1"; pdata[2, 84] = "ProductID-002"; pdata[3, 84] = "LotyyyyMMddID-004"; pdata[4, 84] = "67.84";
            pdata[1, 85] = "1"; pdata[2, 85] = "ProductID-002"; pdata[3, 85] = "LotyyyyMMddID-004"; pdata[4, 85] = "78.17";
            pdata[1, 86] = "1"; pdata[2, 86] = "ProductID-002"; pdata[3, 86] = "LotyyyyMMddID-004"; pdata[4, 86] = "26.07";
            pdata[1, 87] = "1"; pdata[2, 87] = "ProductID-002"; pdata[3, 87] = "LotyyyyMMddID-004"; pdata[4, 87] = "36.09";
            pdata[1, 88] = "1"; pdata[2, 88] = "ProductID-002"; pdata[3, 88] = "LotyyyyMMddID-004"; pdata[4, 88] = "74.53";
            pdata[1, 89] = "1"; pdata[2, 89] = "ProductID-002"; pdata[3, 89] = "LotyyyyMMddID-004"; pdata[4, 89] = "69.87";
            pdata[1, 90] = "1"; pdata[2, 90] = "ProductID-002"; pdata[3, 90] = "LotyyyyMMddID-004"; pdata[4, 90] = "75.34";
            pdata[1, 91] = "1"; pdata[2, 91] = "ProductID-002"; pdata[3, 91] = "LotyyyyMMddID-005"; pdata[4, 91] = "62.41"; //Group-002 - Sub-005
            pdata[1, 92] = "1"; pdata[2, 92] = "ProductID-002"; pdata[3, 92] = "LotyyyyMMddID-005"; pdata[4, 92] = "35.94";
            pdata[1, 93] = "1"; pdata[2, 93] = "ProductID-002"; pdata[3, 93] = "LotyyyyMMddID-005"; pdata[4, 93] = "96.22";
            pdata[1, 94] = "1"; pdata[2, 94] = "ProductID-002"; pdata[3, 94] = "LotyyyyMMddID-005"; pdata[4, 94] = "11.9";
            pdata[1, 95] = "1"; pdata[2, 95] = "ProductID-002"; pdata[3, 95] = "LotyyyyMMddID-005"; pdata[4, 95] = "82.65";
            pdata[1, 96] = "1"; pdata[2, 96] = "ProductID-002"; pdata[3, 96] = "LotyyyyMMddID-005"; pdata[4, 96] = "84.2";
            pdata[1, 97] = "1"; pdata[2, 97] = "ProductID-002"; pdata[3, 97] = "LotyyyyMMddID-005"; pdata[4, 97] = "21.58";
            pdata[1, 98] = "1"; pdata[2, 98] = "ProductID-002"; pdata[3, 98] = "LotyyyyMMddID-005"; pdata[4, 98] = "1.95";
            pdata[1, 99] = "1"; pdata[2, 99] = "ProductID-002"; pdata[3, 99] = "LotyyyyMMddID-005"; pdata[4, 99] = "46.98";
            pdata[1, 100] = "1"; pdata[2, 100] = "ProductID-002"; pdata[3, 100] = "LotyyyyMMddID-005"; pdata[4, 100] = "32.05";
            pdata[1, 101] = "1"; pdata[2, 101] = "ProductID-003"; pdata[3, 101] = "LotyyyyMMddID-001"; pdata[4, 101] = "54.74"; //Group-003 - Sub-001
            pdata[1, 102] = "1"; pdata[2, 102] = "ProductID-003"; pdata[3, 102] = "LotyyyyMMddID-001"; pdata[4, 102] = "97.43";
            pdata[1, 103] = "1"; pdata[2, 103] = "ProductID-003"; pdata[3, 103] = "LotyyyyMMddID-001"; pdata[4, 103] = "74.12";
            pdata[1, 104] = "1"; pdata[2, 104] = "ProductID-003"; pdata[3, 104] = "LotyyyyMMddID-001"; pdata[4, 104] = "6.08";
            pdata[1, 105] = "1"; pdata[2, 105] = "ProductID-003"; pdata[3, 105] = "LotyyyyMMddID-001"; pdata[4, 105] = "46.21";
            pdata[1, 106] = "1"; pdata[2, 106] = "ProductID-003"; pdata[3, 106] = "LotyyyyMMddID-001"; pdata[4, 106] = "66.91";
            pdata[1, 107] = "1"; pdata[2, 107] = "ProductID-003"; pdata[3, 107] = "LotyyyyMMddID-001"; pdata[4, 107] = "64.43";
            pdata[1, 108] = "1"; pdata[2, 108] = "ProductID-003"; pdata[3, 108] = "LotyyyyMMddID-001"; pdata[4, 108] = "59.98";
            pdata[1, 109] = "1"; pdata[2, 109] = "ProductID-003"; pdata[3, 109] = "LotyyyyMMddID-001"; pdata[4, 109] = "87.47";
            pdata[1, 110] = "1"; pdata[2, 110] = "ProductID-003"; pdata[3, 110] = "LotyyyyMMddID-001"; pdata[4, 110] = "49.73";
            pdata[1, 111] = "1"; pdata[2, 111] = "ProductID-003"; pdata[3, 111] = "LotyyyyMMddID-002"; pdata[4, 111] = "88.77"; //Group-003 - Sub-002
            pdata[1, 112] = "1"; pdata[2, 112] = "ProductID-003"; pdata[3, 112] = "LotyyyyMMddID-002"; pdata[4, 112] = "2.13";
            pdata[1, 113] = "1"; pdata[2, 113] = "ProductID-003"; pdata[3, 113] = "LotyyyyMMddID-002"; pdata[4, 113] = "33.06";
            pdata[1, 114] = "1"; pdata[2, 114] = "ProductID-003"; pdata[3, 114] = "LotyyyyMMddID-002"; pdata[4, 114] = "85.77";
            pdata[1, 115] = "1"; pdata[2, 115] = "ProductID-003"; pdata[3, 115] = "LotyyyyMMddID-002"; pdata[4, 115] = "8.78";
            pdata[1, 116] = "1"; pdata[2, 116] = "ProductID-003"; pdata[3, 116] = "LotyyyyMMddID-002"; pdata[4, 116] = "8.7";
            pdata[1, 117] = "1"; pdata[2, 117] = "ProductID-003"; pdata[3, 117] = "LotyyyyMMddID-002"; pdata[4, 117] = "55.71";
            pdata[1, 118] = "1"; pdata[2, 118] = "ProductID-003"; pdata[3, 118] = "LotyyyyMMddID-002"; pdata[4, 118] = "79.88";
            pdata[1, 119] = "1"; pdata[2, 119] = "ProductID-003"; pdata[3, 119] = "LotyyyyMMddID-002"; pdata[4, 119] = "43.92";
            pdata[1, 120] = "1"; pdata[2, 120] = "ProductID-003"; pdata[3, 120] = "LotyyyyMMddID-002"; pdata[4, 120] = "20.85";
            pdata[1, 121] = "1"; pdata[2, 121] = "ProductID-003"; pdata[3, 121] = "LotyyyyMMddID-003"; pdata[4, 121] = "17.03"; //Group-003 - Sub-003
            pdata[1, 122] = "1"; pdata[2, 122] = "ProductID-003"; pdata[3, 122] = "LotyyyyMMddID-003"; pdata[4, 122] = "44.62";
            pdata[1, 123] = "1"; pdata[2, 123] = "ProductID-003"; pdata[3, 123] = "LotyyyyMMddID-003"; pdata[4, 123] = "99.92";
            pdata[1, 124] = "1"; pdata[2, 124] = "ProductID-003"; pdata[3, 124] = "LotyyyyMMddID-003"; pdata[4, 124] = "56.78";
            pdata[1, 125] = "1"; pdata[2, 125] = "ProductID-003"; pdata[3, 125] = "LotyyyyMMddID-003"; pdata[4, 125] = "99.03";
            pdata[1, 126] = "1"; pdata[2, 126] = "ProductID-003"; pdata[3, 126] = "LotyyyyMMddID-003"; pdata[4, 126] = "9.98";
            pdata[1, 127] = "1"; pdata[2, 127] = "ProductID-003"; pdata[3, 127] = "LotyyyyMMddID-003"; pdata[4, 127] = "50.59";
            pdata[1, 128] = "1"; pdata[2, 128] = "ProductID-003"; pdata[3, 128] = "LotyyyyMMddID-003"; pdata[4, 128] = "16.76";
            pdata[1, 129] = "1"; pdata[2, 129] = "ProductID-003"; pdata[3, 129] = "LotyyyyMMddID-003"; pdata[4, 129] = "72.84";
            pdata[1, 130] = "1"; pdata[2, 130] = "ProductID-003"; pdata[3, 130] = "LotyyyyMMddID-003"; pdata[4, 130] = "10.69";
            pdata[1, 131] = "1"; pdata[2, 131] = "ProductID-003"; pdata[3, 131] = "LotyyyyMMddID-004"; pdata[4, 131] = "26.28"; //Group-003 - Sub-004
            pdata[1, 132] = "1"; pdata[2, 132] = "ProductID-003"; pdata[3, 132] = "LotyyyyMMddID-004"; pdata[4, 132] = "1.68";
            pdata[1, 133] = "1"; pdata[2, 133] = "ProductID-003"; pdata[3, 133] = "LotyyyyMMddID-004"; pdata[4, 133] = "14.95";
            pdata[1, 134] = "1"; pdata[2, 134] = "ProductID-003"; pdata[3, 134] = "LotyyyyMMddID-004"; pdata[4, 134] = "36.92";
            pdata[1, 135] = "1"; pdata[2, 135] = "ProductID-003"; pdata[3, 135] = "LotyyyyMMddID-004"; pdata[4, 135] = "63.71";
            pdata[1, 136] = "1"; pdata[2, 136] = "ProductID-003"; pdata[3, 136] = "LotyyyyMMddID-004"; pdata[4, 136] = "55.94";
            pdata[1, 137] = "1"; pdata[2, 137] = "ProductID-003"; pdata[3, 137] = "LotyyyyMMddID-004"; pdata[4, 137] = "27.63";
            pdata[1, 138] = "1"; pdata[2, 138] = "ProductID-003"; pdata[3, 138] = "LotyyyyMMddID-004"; pdata[4, 138] = "56.86";
            pdata[1, 139] = "1"; pdata[2, 139] = "ProductID-003"; pdata[3, 139] = "LotyyyyMMddID-004"; pdata[4, 139] = "34.4";
            pdata[1, 140] = "1"; pdata[2, 140] = "ProductID-003"; pdata[3, 140] = "LotyyyyMMddID-004"; pdata[4, 140] = "14.4";
            pdata[1, 141] = "1"; pdata[2, 141] = "ProductID-003"; pdata[3, 141] = "LotyyyyMMddID-005"; pdata[4, 141] = "49.08"; //Group-003 - Sub-005
            pdata[1, 142] = "1"; pdata[2, 142] = "ProductID-003"; pdata[3, 142] = "LotyyyyMMddID-005"; pdata[4, 142] = "78.06";
            pdata[1, 143] = "1"; pdata[2, 143] = "ProductID-003"; pdata[3, 143] = "LotyyyyMMddID-005"; pdata[4, 143] = "70.09";
            pdata[1, 144] = "1"; pdata[2, 144] = "ProductID-003"; pdata[3, 144] = "LotyyyyMMddID-005"; pdata[4, 144] = "21.96";
            pdata[1, 145] = "1"; pdata[2, 145] = "ProductID-003"; pdata[3, 145] = "LotyyyyMMddID-005"; pdata[4, 145] = "55.04";
            pdata[1, 146] = "1"; pdata[2, 146] = "ProductID-003"; pdata[3, 146] = "LotyyyyMMddID-005"; pdata[4, 146] = "16.59";
            pdata[1, 147] = "1"; pdata[2, 147] = "ProductID-003"; pdata[3, 147] = "LotyyyyMMddID-005"; pdata[4, 147] = "82.51";
            pdata[1, 148] = "1"; pdata[2, 148] = "ProductID-003"; pdata[3, 148] = "LotyyyyMMddID-005"; pdata[4, 148] = "59.89";
            pdata[1, 149] = "1"; pdata[2, 149] = "ProductID-003"; pdata[3, 149] = "LotyyyyMMddID-005"; pdata[4, 149] = "3.75";
            pdata[1, 150] = "1"; pdata[2, 150] = "ProductID-003"; pdata[3, 150] = "LotyyyyMMddID-005"; pdata[4, 150] = "49.04";
            pdata[1, 151] = "1"; pdata[2, 151] = "ProductID-004"; pdata[3, 151] = "LotyyyyMMddID-001"; pdata[4, 151] = "57.64"; //Group-004 - Sub-001
            pdata[1, 152] = "1"; pdata[2, 152] = "ProductID-004"; pdata[3, 152] = "LotyyyyMMddID-001"; pdata[4, 152] = "15.29";
            pdata[1, 153] = "1"; pdata[2, 153] = "ProductID-004"; pdata[3, 153] = "LotyyyyMMddID-001"; pdata[4, 153] = "48.14";
            pdata[1, 154] = "1"; pdata[2, 154] = "ProductID-004"; pdata[3, 154] = "LotyyyyMMddID-001"; pdata[4, 154] = "71.86";
            pdata[1, 155] = "1"; pdata[2, 155] = "ProductID-004"; pdata[3, 155] = "LotyyyyMMddID-001"; pdata[4, 155] = "3.3";
            pdata[1, 156] = "1"; pdata[2, 156] = "ProductID-004"; pdata[3, 156] = "LotyyyyMMddID-001"; pdata[4, 156] = "43.32";
            pdata[1, 157] = "1"; pdata[2, 157] = "ProductID-004"; pdata[3, 157] = "LotyyyyMMddID-001"; pdata[4, 157] = "71.69";
            pdata[1, 158] = "1"; pdata[2, 158] = "ProductID-004"; pdata[3, 158] = "LotyyyyMMddID-001"; pdata[4, 158] = "20.19";
            pdata[1, 159] = "1"; pdata[2, 159] = "ProductID-004"; pdata[3, 159] = "LotyyyyMMddID-001"; pdata[4, 159] = "49.94";
            pdata[1, 160] = "1"; pdata[2, 160] = "ProductID-004"; pdata[3, 160] = "LotyyyyMMddID-001"; pdata[4, 160] = "97.05";
            pdata[1, 161] = "1"; pdata[2, 161] = "ProductID-004"; pdata[3, 161] = "LotyyyyMMddID-002"; pdata[4, 161] = "74.39"; //Group-004 - Sub-002
            pdata[1, 162] = "1"; pdata[2, 162] = "ProductID-004"; pdata[3, 162] = "LotyyyyMMddID-002"; pdata[4, 162] = "38.6";
            pdata[1, 163] = "1"; pdata[2, 163] = "ProductID-004"; pdata[3, 163] = "LotyyyyMMddID-002"; pdata[4, 163] = "55.95";
            pdata[1, 164] = "1"; pdata[2, 164] = "ProductID-004"; pdata[3, 164] = "LotyyyyMMddID-002"; pdata[4, 164] = "13.76";
            pdata[1, 165] = "1"; pdata[2, 165] = "ProductID-004"; pdata[3, 165] = "LotyyyyMMddID-002"; pdata[4, 165] = "5.67";
            pdata[1, 166] = "1"; pdata[2, 166] = "ProductID-004"; pdata[3, 166] = "LotyyyyMMddID-002"; pdata[4, 166] = "83.58";
            pdata[1, 167] = "1"; pdata[2, 167] = "ProductID-004"; pdata[3, 167] = "LotyyyyMMddID-002"; pdata[4, 167] = "91.1";
            pdata[1, 168] = "1"; pdata[2, 168] = "ProductID-004"; pdata[3, 168] = "LotyyyyMMddID-002"; pdata[4, 168] = "83.85";
            pdata[1, 169] = "1"; pdata[2, 169] = "ProductID-004"; pdata[3, 169] = "LotyyyyMMddID-002"; pdata[4, 169] = "92.5";
            pdata[1, 170] = "1"; pdata[2, 170] = "ProductID-004"; pdata[3, 170] = "LotyyyyMMddID-002"; pdata[4, 170] = "57.57";
            pdata[1, 171] = "1"; pdata[2, 171] = "ProductID-004"; pdata[3, 171] = "LotyyyyMMddID-003"; pdata[4, 171] = "37.35"; //Group-004 - Sub-003
            pdata[1, 172] = "1"; pdata[2, 172] = "ProductID-004"; pdata[3, 172] = "LotyyyyMMddID-003"; pdata[4, 172] = "94.25";
            pdata[1, 173] = "1"; pdata[2, 173] = "ProductID-004"; pdata[3, 173] = "LotyyyyMMddID-003"; pdata[4, 173] = "60.84";
            pdata[1, 174] = "1"; pdata[2, 174] = "ProductID-004"; pdata[3, 174] = "LotyyyyMMddID-003"; pdata[4, 174] = "88.14";
            pdata[1, 175] = "1"; pdata[2, 175] = "ProductID-004"; pdata[3, 175] = "LotyyyyMMddID-003"; pdata[4, 175] = "32.41";
            pdata[1, 176] = "1"; pdata[2, 176] = "ProductID-004"; pdata[3, 176] = "LotyyyyMMddID-003"; pdata[4, 176] = "40.08";
            pdata[1, 177] = "1"; pdata[2, 177] = "ProductID-004"; pdata[3, 177] = "LotyyyyMMddID-003"; pdata[4, 177] = "78.44";
            pdata[1, 178] = "1"; pdata[2, 178] = "ProductID-004"; pdata[3, 178] = "LotyyyyMMddID-003"; pdata[4, 178] = "29.18";
            pdata[1, 179] = "1"; pdata[2, 179] = "ProductID-004"; pdata[3, 179] = "LotyyyyMMddID-003"; pdata[4, 179] = "27.55";
            pdata[1, 180] = "1"; pdata[2, 180] = "ProductID-004"; pdata[3, 180] = "LotyyyyMMddID-003"; pdata[4, 180] = "73.53";
            pdata[1, 181] = "1"; pdata[2, 181] = "ProductID-004"; pdata[3, 181] = "LotyyyyMMddID-004"; pdata[4, 181] = "2.95"; //Group-004 - Sub-004
            pdata[1, 182] = "1"; pdata[2, 182] = "ProductID-004"; pdata[3, 182] = "LotyyyyMMddID-004"; pdata[4, 182] = "81.35";
            pdata[1, 183] = "1"; pdata[2, 183] = "ProductID-004"; pdata[3, 183] = "LotyyyyMMddID-004"; pdata[4, 183] = "47.84";
            pdata[1, 184] = "1"; pdata[2, 184] = "ProductID-004"; pdata[3, 184] = "LotyyyyMMddID-004"; pdata[4, 184] = "41.61";
            pdata[1, 185] = "1"; pdata[2, 185] = "ProductID-004"; pdata[3, 185] = "LotyyyyMMddID-004"; pdata[4, 185] = "86.34";
            pdata[1, 186] = "1"; pdata[2, 186] = "ProductID-004"; pdata[3, 186] = "LotyyyyMMddID-004"; pdata[4, 186] = "27.99";
            pdata[1, 187] = "1"; pdata[2, 187] = "ProductID-004"; pdata[3, 187] = "LotyyyyMMddID-004"; pdata[4, 187] = "38.41";
            pdata[1, 188] = "1"; pdata[2, 188] = "ProductID-004"; pdata[3, 188] = "LotyyyyMMddID-004"; pdata[4, 188] = "67.39";
            pdata[1, 189] = "1"; pdata[2, 189] = "ProductID-004"; pdata[3, 189] = "LotyyyyMMddID-004"; pdata[4, 189] = "45.13";
            pdata[1, 190] = "1"; pdata[2, 190] = "ProductID-004"; pdata[3, 190] = "LotyyyyMMddID-004"; pdata[4, 190] = "99.71";
            pdata[1, 191] = "1"; pdata[2, 191] = "ProductID-004"; pdata[3, 191] = "LotyyyyMMddID-005"; pdata[4, 191] = "8.99"; //Group-004 - Sub-005
            pdata[1, 192] = "1"; pdata[2, 192] = "ProductID-004"; pdata[3, 192] = "LotyyyyMMddID-005"; pdata[4, 192] = "45.63";
            pdata[1, 193] = "1"; pdata[2, 193] = "ProductID-004"; pdata[3, 193] = "LotyyyyMMddID-005"; pdata[4, 193] = "28.76";
            pdata[1, 194] = "1"; pdata[2, 194] = "ProductID-004"; pdata[3, 194] = "LotyyyyMMddID-005"; pdata[4, 194] = "80.47";
            pdata[1, 195] = "1"; pdata[2, 195] = "ProductID-004"; pdata[3, 195] = "LotyyyyMMddID-005"; pdata[4, 195] = "80.65";
            pdata[1, 196] = "1"; pdata[2, 196] = "ProductID-004"; pdata[3, 196] = "LotyyyyMMddID-005"; pdata[4, 196] = "59.37";
            pdata[1, 197] = "1"; pdata[2, 197] = "ProductID-004"; pdata[3, 197] = "LotyyyyMMddID-005"; pdata[4, 197] = "77.05";
            pdata[1, 198] = "1"; pdata[2, 198] = "ProductID-004"; pdata[3, 198] = "LotyyyyMMddID-005"; pdata[4, 198] = "56.42";
            pdata[1, 199] = "1"; pdata[2, 199] = "ProductID-004"; pdata[3, 199] = "LotyyyyMMddID-005"; pdata[4, 199] = "53.6";
            pdata[1, 200] = "1"; pdata[2, 200] = "ProductID-004"; pdata[3, 200] = "LotyyyyMMddID-005"; pdata[4, 200] = "78.76";
            pdata[1, 201] = "1"; pdata[2, 201] = "ProductID-005"; pdata[3, 201] = "LotyyyyMMddID-001"; pdata[4, 201] = "43.28"; //Group-005 - Sub-001
            pdata[1, 202] = "1"; pdata[2, 202] = "ProductID-005"; pdata[3, 202] = "LotyyyyMMddID-001"; pdata[4, 202] = "27.97";
            pdata[1, 203] = "1"; pdata[2, 203] = "ProductID-005"; pdata[3, 203] = "LotyyyyMMddID-001"; pdata[4, 203] = "15.41";
            pdata[1, 204] = "1"; pdata[2, 204] = "ProductID-005"; pdata[3, 204] = "LotyyyyMMddID-001"; pdata[4, 204] = "70.37";
            pdata[1, 205] = "1"; pdata[2, 205] = "ProductID-005"; pdata[3, 205] = "LotyyyyMMddID-001"; pdata[4, 205] = "76.89";
            pdata[1, 206] = "1"; pdata[2, 206] = "ProductID-005"; pdata[3, 206] = "LotyyyyMMddID-001"; pdata[4, 206] = "73.14";
            pdata[1, 207] = "1"; pdata[2, 207] = "ProductID-005"; pdata[3, 207] = "LotyyyyMMddID-001"; pdata[4, 207] = "55.02";
            pdata[1, 208] = "1"; pdata[2, 208] = "ProductID-005"; pdata[3, 208] = "LotyyyyMMddID-001"; pdata[4, 208] = "45.03";
            pdata[1, 209] = "1"; pdata[2, 209] = "ProductID-005"; pdata[3, 209] = "LotyyyyMMddID-001"; pdata[4, 209] = "51.18";
            pdata[1, 210] = "1"; pdata[2, 210] = "ProductID-005"; pdata[3, 210] = "LotyyyyMMddID-001"; pdata[4, 210] = "8.7";
            pdata[1, 211] = "1"; pdata[2, 211] = "ProductID-005"; pdata[3, 211] = "LotyyyyMMddID-002"; pdata[4, 211] = "5.93"; //Group-005 - Sub-002
            pdata[1, 212] = "1"; pdata[2, 212] = "ProductID-005"; pdata[3, 212] = "LotyyyyMMddID-002"; pdata[4, 212] = "80.21";
            pdata[1, 213] = "1"; pdata[2, 213] = "ProductID-005"; pdata[3, 213] = "LotyyyyMMddID-002"; pdata[4, 213] = "75.89";
            pdata[1, 214] = "1"; pdata[2, 214] = "ProductID-005"; pdata[3, 214] = "LotyyyyMMddID-002"; pdata[4, 214] = "22.34";
            pdata[1, 215] = "1"; pdata[2, 215] = "ProductID-005"; pdata[3, 215] = "LotyyyyMMddID-002"; pdata[4, 215] = "85.26";
            pdata[1, 216] = "1"; pdata[2, 216] = "ProductID-005"; pdata[3, 216] = "LotyyyyMMddID-002"; pdata[4, 216] = "85.84";
            pdata[1, 217] = "1"; pdata[2, 217] = "ProductID-005"; pdata[3, 217] = "LotyyyyMMddID-002"; pdata[4, 217] = "34.24";
            pdata[1, 218] = "1"; pdata[2, 218] = "ProductID-005"; pdata[3, 218] = "LotyyyyMMddID-002"; pdata[4, 218] = "28.79";
            pdata[1, 219] = "1"; pdata[2, 219] = "ProductID-005"; pdata[3, 219] = "LotyyyyMMddID-002"; pdata[4, 219] = "72.47";
            pdata[1, 220] = "1"; pdata[2, 220] = "ProductID-005"; pdata[3, 220] = "LotyyyyMMddID-002"; pdata[4, 220] = "49.86";
            pdata[1, 221] = "1"; pdata[2, 221] = "ProductID-005"; pdata[3, 221] = "LotyyyyMMddID-003"; pdata[4, 221] = "7.43"; //Group-005 - Sub-003
            pdata[1, 222] = "1"; pdata[2, 222] = "ProductID-005"; pdata[3, 222] = "LotyyyyMMddID-003"; pdata[4, 222] = "71.91";
            pdata[1, 223] = "1"; pdata[2, 223] = "ProductID-005"; pdata[3, 223] = "LotyyyyMMddID-003"; pdata[4, 223] = "91.45";
            pdata[1, 224] = "1"; pdata[2, 224] = "ProductID-005"; pdata[3, 224] = "LotyyyyMMddID-003"; pdata[4, 224] = "81.75";
            pdata[1, 225] = "1"; pdata[2, 225] = "ProductID-005"; pdata[3, 225] = "LotyyyyMMddID-003"; pdata[4, 225] = "59.54";
            pdata[1, 226] = "1"; pdata[2, 226] = "ProductID-005"; pdata[3, 226] = "LotyyyyMMddID-003"; pdata[4, 226] = "90.35";
            pdata[1, 227] = "1"; pdata[2, 227] = "ProductID-005"; pdata[3, 227] = "LotyyyyMMddID-003"; pdata[4, 227] = "52.08";
            pdata[1, 228] = "1"; pdata[2, 228] = "ProductID-005"; pdata[3, 228] = "LotyyyyMMddID-003"; pdata[4, 228] = "72.5";
            pdata[1, 229] = "1"; pdata[2, 229] = "ProductID-005"; pdata[3, 229] = "LotyyyyMMddID-003"; pdata[4, 229] = "47.06";
            pdata[1, 230] = "1"; pdata[2, 230] = "ProductID-005"; pdata[3, 230] = "LotyyyyMMddID-003"; pdata[4, 230] = "23.25";
            pdata[1, 231] = "1"; pdata[2, 231] = "ProductID-005"; pdata[3, 231] = "LotyyyyMMddID-004"; pdata[4, 231] = "19.36"; //Group-005 - Sub-004
            pdata[1, 232] = "1"; pdata[2, 232] = "ProductID-005"; pdata[3, 232] = "LotyyyyMMddID-004"; pdata[4, 232] = "52.5";
            pdata[1, 233] = "1"; pdata[2, 233] = "ProductID-005"; pdata[3, 233] = "LotyyyyMMddID-004"; pdata[4, 233] = "5.88";
            pdata[1, 234] = "1"; pdata[2, 234] = "ProductID-005"; pdata[3, 234] = "LotyyyyMMddID-004"; pdata[4, 234] = "79.32";
            pdata[1, 235] = "1"; pdata[2, 235] = "ProductID-005"; pdata[3, 235] = "LotyyyyMMddID-004"; pdata[4, 235] = "73.66";
            pdata[1, 236] = "1"; pdata[2, 236] = "ProductID-005"; pdata[3, 236] = "LotyyyyMMddID-004"; pdata[4, 236] = "30.04";
            pdata[1, 237] = "1"; pdata[2, 237] = "ProductID-005"; pdata[3, 237] = "LotyyyyMMddID-004"; pdata[4, 237] = "69.21";
            pdata[1, 238] = "1"; pdata[2, 238] = "ProductID-005"; pdata[3, 238] = "LotyyyyMMddID-004"; pdata[4, 238] = "64.61";
            pdata[1, 239] = "1"; pdata[2, 239] = "ProductID-005"; pdata[3, 239] = "LotyyyyMMddID-004"; pdata[4, 239] = "23.89";
            pdata[1, 240] = "1"; pdata[2, 240] = "ProductID-005"; pdata[3, 240] = "LotyyyyMMddID-004"; pdata[4, 240] = "29.3";
            pdata[1, 241] = "1"; pdata[2, 241] = "ProductID-005"; pdata[3, 241] = "LotyyyyMMddID-005"; pdata[4, 241] = "8.47"; //Group-005 - Sub-005
            pdata[1, 242] = "1"; pdata[2, 242] = "ProductID-005"; pdata[3, 242] = "LotyyyyMMddID-005"; pdata[4, 242] = "74.32";
            pdata[1, 243] = "1"; pdata[2, 243] = "ProductID-005"; pdata[3, 243] = "LotyyyyMMddID-005"; pdata[4, 243] = "35.5";
            pdata[1, 244] = "1"; pdata[2, 244] = "ProductID-005"; pdata[3, 244] = "LotyyyyMMddID-005"; pdata[4, 244] = "33.27";
            pdata[1, 245] = "1"; pdata[2, 245] = "ProductID-005"; pdata[3, 245] = "LotyyyyMMddID-005"; pdata[4, 245] = "51.78";
            pdata[1, 246] = "1"; pdata[2, 246] = "ProductID-005"; pdata[3, 246] = "LotyyyyMMddID-005"; pdata[4, 246] = "33.66";
            pdata[1, 247] = "1"; pdata[2, 247] = "ProductID-005"; pdata[3, 247] = "LotyyyyMMddID-005"; pdata[4, 247] = "25.24";
            pdata[1, 248] = "1"; pdata[2, 248] = "ProductID-005"; pdata[3, 248] = "LotyyyyMMddID-005"; pdata[4, 248] = "68.7";
            pdata[1, 249] = "1"; pdata[2, 249] = "ProductID-005"; pdata[3, 249] = "LotyyyyMMddID-005"; pdata[4, 249] = "91.21";
            pdata[1, 250] = "1"; pdata[2, 250] = "ProductID-005"; pdata[3, 250] = "LotyyyyMMddID-005"; pdata[4, 250] = "86.04";
            pdata[1, 251] = "1"; pdata[2, 251] = "ProductID-006"; pdata[3, 251] = "LotyyyyMMddID-001"; pdata[4, 251] = "66.49"; //Group-006 - Sub-001
            pdata[1, 252] = "1"; pdata[2, 252] = "ProductID-006"; pdata[3, 252] = "LotyyyyMMddID-001"; pdata[4, 252] = "2.93";
            pdata[1, 253] = "1"; pdata[2, 253] = "ProductID-006"; pdata[3, 253] = "LotyyyyMMddID-001"; pdata[4, 253] = "24.87";
            pdata[1, 254] = "1"; pdata[2, 254] = "ProductID-006"; pdata[3, 254] = "LotyyyyMMddID-001"; pdata[4, 254] = "66.31";
            pdata[1, 255] = "1"; pdata[2, 255] = "ProductID-006"; pdata[3, 255] = "LotyyyyMMddID-001"; pdata[4, 255] = "43.11";
            pdata[1, 256] = "1"; pdata[2, 256] = "ProductID-006"; pdata[3, 256] = "LotyyyyMMddID-001"; pdata[4, 256] = "28.58";
            pdata[1, 257] = "1"; pdata[2, 257] = "ProductID-006"; pdata[3, 257] = "LotyyyyMMddID-001"; pdata[4, 257] = "77.92";
            pdata[1, 258] = "1"; pdata[2, 258] = "ProductID-006"; pdata[3, 258] = "LotyyyyMMddID-001"; pdata[4, 258] = "18.71";
            pdata[1, 259] = "1"; pdata[2, 259] = "ProductID-006"; pdata[3, 259] = "LotyyyyMMddID-001"; pdata[4, 259] = "62.21";
            pdata[1, 260] = "1"; pdata[2, 260] = "ProductID-006"; pdata[3, 260] = "LotyyyyMMddID-001"; pdata[4, 260] = "15.54";
            pdata[1, 261] = "1"; pdata[2, 261] = "ProductID-006"; pdata[3, 261] = "LotyyyyMMddID-002"; pdata[4, 261] = "56.53"; //Group-006 - Sub-002
            pdata[1, 262] = "1"; pdata[2, 262] = "ProductID-006"; pdata[3, 262] = "LotyyyyMMddID-002"; pdata[4, 262] = "81.95";
            pdata[1, 263] = "1"; pdata[2, 263] = "ProductID-006"; pdata[3, 263] = "LotyyyyMMddID-002"; pdata[4, 263] = "34.63";
            pdata[1, 264] = "1"; pdata[2, 264] = "ProductID-006"; pdata[3, 264] = "LotyyyyMMddID-002"; pdata[4, 264] = "78.45";
            pdata[1, 265] = "1"; pdata[2, 265] = "ProductID-006"; pdata[3, 265] = "LotyyyyMMddID-002"; pdata[4, 265] = "53.94";
            pdata[1, 266] = "1"; pdata[2, 266] = "ProductID-006"; pdata[3, 266] = "LotyyyyMMddID-002"; pdata[4, 266] = "70.02";
            pdata[1, 267] = "1"; pdata[2, 267] = "ProductID-006"; pdata[3, 267] = "LotyyyyMMddID-002"; pdata[4, 267] = "3.26";
            pdata[1, 268] = "1"; pdata[2, 268] = "ProductID-006"; pdata[3, 268] = "LotyyyyMMddID-002"; pdata[4, 268] = "63.58";
            pdata[1, 269] = "1"; pdata[2, 269] = "ProductID-006"; pdata[3, 269] = "LotyyyyMMddID-002"; pdata[4, 269] = "46.81";
            pdata[1, 270] = "1"; pdata[2, 270] = "ProductID-006"; pdata[3, 270] = "LotyyyyMMddID-002"; pdata[4, 270] = "8.48";
            pdata[1, 271] = "1"; pdata[2, 271] = "ProductID-006"; pdata[3, 271] = "LotyyyyMMddID-003"; pdata[4, 271] = "84.02"; //Group-006 - Sub-003
            pdata[1, 272] = "1"; pdata[2, 272] = "ProductID-006"; pdata[3, 272] = "LotyyyyMMddID-003"; pdata[4, 272] = "83.41";
            pdata[1, 273] = "1"; pdata[2, 273] = "ProductID-006"; pdata[3, 273] = "LotyyyyMMddID-003"; pdata[4, 273] = "51.74";
            pdata[1, 274] = "1"; pdata[2, 274] = "ProductID-006"; pdata[3, 274] = "LotyyyyMMddID-003"; pdata[4, 274] = "89.32";
            pdata[1, 275] = "1"; pdata[2, 275] = "ProductID-006"; pdata[3, 275] = "LotyyyyMMddID-003"; pdata[4, 275] = "7.96";
            pdata[1, 276] = "1"; pdata[2, 276] = "ProductID-006"; pdata[3, 276] = "LotyyyyMMddID-003"; pdata[4, 276] = "8.71";
            pdata[1, 277] = "1"; pdata[2, 277] = "ProductID-006"; pdata[3, 277] = "LotyyyyMMddID-003"; pdata[4, 277] = "6.33";
            pdata[1, 278] = "1"; pdata[2, 278] = "ProductID-006"; pdata[3, 278] = "LotyyyyMMddID-003"; pdata[4, 278] = "47.13";
            pdata[1, 279] = "1"; pdata[2, 279] = "ProductID-006"; pdata[3, 279] = "LotyyyyMMddID-003"; pdata[4, 279] = "68.07";
            pdata[1, 280] = "1"; pdata[2, 280] = "ProductID-006"; pdata[3, 280] = "LotyyyyMMddID-003"; pdata[4, 280] = "60.6";
            pdata[1, 281] = "1"; pdata[2, 281] = "ProductID-006"; pdata[3, 281] = "LotyyyyMMddID-004"; pdata[4, 281] = "9.22"; //Group-006 - Sub-004
            pdata[1, 282] = "1"; pdata[2, 282] = "ProductID-006"; pdata[3, 282] = "LotyyyyMMddID-004"; pdata[4, 282] = "78.49";
            pdata[1, 283] = "1"; pdata[2, 283] = "ProductID-006"; pdata[3, 283] = "LotyyyyMMddID-004"; pdata[4, 283] = "41.26";
            pdata[1, 284] = "1"; pdata[2, 284] = "ProductID-006"; pdata[3, 284] = "LotyyyyMMddID-004"; pdata[4, 284] = "56.51";
            pdata[1, 285] = "1"; pdata[2, 285] = "ProductID-006"; pdata[3, 285] = "LotyyyyMMddID-004"; pdata[4, 285] = "29.6";
            pdata[1, 286] = "1"; pdata[2, 286] = "ProductID-006"; pdata[3, 286] = "LotyyyyMMddID-004"; pdata[4, 286] = "26.81";
            pdata[1, 287] = "1"; pdata[2, 287] = "ProductID-006"; pdata[3, 287] = "LotyyyyMMddID-004"; pdata[4, 287] = "13.67";
            pdata[1, 288] = "1"; pdata[2, 288] = "ProductID-006"; pdata[3, 288] = "LotyyyyMMddID-004"; pdata[4, 288] = "69.55";
            pdata[1, 289] = "1"; pdata[2, 289] = "ProductID-006"; pdata[3, 289] = "LotyyyyMMddID-004"; pdata[4, 289] = "70.41";
            pdata[1, 290] = "1"; pdata[2, 290] = "ProductID-006"; pdata[3, 290] = "LotyyyyMMddID-004"; pdata[4, 290] = "44.59";
            pdata[1, 291] = "1"; pdata[2, 291] = "ProductID-006"; pdata[3, 291] = "LotyyyyMMddID-005"; pdata[4, 291] = "74.46"; //Group-006 - Sub-005
            pdata[1, 292] = "1"; pdata[2, 292] = "ProductID-006"; pdata[3, 292] = "LotyyyyMMddID-005"; pdata[4, 292] = "39.57";
            pdata[1, 293] = "1"; pdata[2, 293] = "ProductID-006"; pdata[3, 293] = "LotyyyyMMddID-005"; pdata[4, 293] = "86.99";
            pdata[1, 294] = "1"; pdata[2, 294] = "ProductID-006"; pdata[3, 294] = "LotyyyyMMddID-005"; pdata[4, 294] = "95.69";
            pdata[1, 295] = "1"; pdata[2, 295] = "ProductID-006"; pdata[3, 295] = "LotyyyyMMddID-005"; pdata[4, 295] = "78.51";
            pdata[1, 296] = "1"; pdata[2, 296] = "ProductID-006"; pdata[3, 296] = "LotyyyyMMddID-005"; pdata[4, 296] = "89.4";
            pdata[1, 297] = "1"; pdata[2, 297] = "ProductID-006"; pdata[3, 297] = "LotyyyyMMddID-005"; pdata[4, 297] = "21.48";
            pdata[1, 298] = "1"; pdata[2, 298] = "ProductID-006"; pdata[3, 298] = "LotyyyyMMddID-005"; pdata[4, 298] = "39.62";
            pdata[1, 299] = "1"; pdata[2, 299] = "ProductID-006"; pdata[3, 299] = "LotyyyyMMddID-005"; pdata[4, 299] = "24.34";
            pdata[1, 300] = "1"; pdata[2, 300] = "ProductID-006"; pdata[3, 300] = "LotyyyyMMddID-005"; pdata[4, 300] = "99.26";
            pdata[1, 301] = "1"; pdata[2, 301] = "ProductID-007"; pdata[3, 301] = "LotyyyyMMddID-001"; pdata[4, 301] = "18.38"; //Group-007 - Sub-001
            pdata[1, 302] = "1"; pdata[2, 302] = "ProductID-007"; pdata[3, 302] = "LotyyyyMMddID-001"; pdata[4, 302] = "3.46";
            pdata[1, 303] = "1"; pdata[2, 303] = "ProductID-007"; pdata[3, 303] = "LotyyyyMMddID-001"; pdata[4, 303] = "70.27";
            pdata[1, 304] = "1"; pdata[2, 304] = "ProductID-007"; pdata[3, 304] = "LotyyyyMMddID-001"; pdata[4, 304] = "58.39";
            pdata[1, 305] = "1"; pdata[2, 305] = "ProductID-007"; pdata[3, 305] = "LotyyyyMMddID-001"; pdata[4, 305] = "63.92";
            pdata[1, 306] = "1"; pdata[2, 306] = "ProductID-007"; pdata[3, 306] = "LotyyyyMMddID-001"; pdata[4, 306] = "43.28";
            pdata[1, 307] = "1"; pdata[2, 307] = "ProductID-007"; pdata[3, 307] = "LotyyyyMMddID-001"; pdata[4, 307] = "62.01";
            pdata[1, 308] = "1"; pdata[2, 308] = "ProductID-007"; pdata[3, 308] = "LotyyyyMMddID-001"; pdata[4, 308] = "85.63";
            pdata[1, 309] = "1"; pdata[2, 309] = "ProductID-007"; pdata[3, 309] = "LotyyyyMMddID-001"; pdata[4, 309] = "83.21";
            pdata[1, 310] = "1"; pdata[2, 310] = "ProductID-007"; pdata[3, 310] = "LotyyyyMMddID-001"; pdata[4, 310] = "62.41";
            pdata[1, 311] = "1"; pdata[2, 311] = "ProductID-007"; pdata[3, 311] = "LotyyyyMMddID-002"; pdata[4, 311] = "31.87"; //Group-007 - Sub-002
            pdata[1, 312] = "1"; pdata[2, 312] = "ProductID-007"; pdata[3, 312] = "LotyyyyMMddID-002"; pdata[4, 312] = "72.78";
            pdata[1, 313] = "1"; pdata[2, 313] = "ProductID-007"; pdata[3, 313] = "LotyyyyMMddID-002"; pdata[4, 313] = "70.86";
            pdata[1, 314] = "1"; pdata[2, 314] = "ProductID-007"; pdata[3, 314] = "LotyyyyMMddID-002"; pdata[4, 314] = "60.64";
            pdata[1, 315] = "1"; pdata[2, 315] = "ProductID-007"; pdata[3, 315] = "LotyyyyMMddID-002"; pdata[4, 315] = "29.08";
            pdata[1, 316] = "1"; pdata[2, 316] = "ProductID-007"; pdata[3, 316] = "LotyyyyMMddID-002"; pdata[4, 316] = "32.36";
            pdata[1, 317] = "1"; pdata[2, 317] = "ProductID-007"; pdata[3, 317] = "LotyyyyMMddID-002"; pdata[4, 317] = "41.19";
            pdata[1, 318] = "1"; pdata[2, 318] = "ProductID-007"; pdata[3, 318] = "LotyyyyMMddID-002"; pdata[4, 318] = "41.36";
            pdata[1, 319] = "1"; pdata[2, 319] = "ProductID-007"; pdata[3, 319] = "LotyyyyMMddID-002"; pdata[4, 319] = "43.92";
            pdata[1, 320] = "1"; pdata[2, 320] = "ProductID-007"; pdata[3, 320] = "LotyyyyMMddID-002"; pdata[4, 320] = "86.6";
            pdata[1, 321] = "1"; pdata[2, 321] = "ProductID-007"; pdata[3, 321] = "LotyyyyMMddID-003"; pdata[4, 321] = "41.57"; //Group-007 - Sub-003
            pdata[1, 322] = "1"; pdata[2, 322] = "ProductID-007"; pdata[3, 322] = "LotyyyyMMddID-003"; pdata[4, 322] = "6.86";
            pdata[1, 323] = "1"; pdata[2, 323] = "ProductID-007"; pdata[3, 323] = "LotyyyyMMddID-003"; pdata[4, 323] = "9.61";
            pdata[1, 324] = "1"; pdata[2, 324] = "ProductID-007"; pdata[3, 324] = "LotyyyyMMddID-003"; pdata[4, 324] = "77.21";
            pdata[1, 325] = "1"; pdata[2, 325] = "ProductID-007"; pdata[3, 325] = "LotyyyyMMddID-003"; pdata[4, 325] = "62.76";
            pdata[1, 326] = "1"; pdata[2, 326] = "ProductID-007"; pdata[3, 326] = "LotyyyyMMddID-003"; pdata[4, 326] = "79.03";
            pdata[1, 327] = "1"; pdata[2, 327] = "ProductID-007"; pdata[3, 327] = "LotyyyyMMddID-003"; pdata[4, 327] = "71.58";
            pdata[1, 328] = "1"; pdata[2, 328] = "ProductID-007"; pdata[3, 328] = "LotyyyyMMddID-003"; pdata[4, 328] = "95.31";
            pdata[1, 329] = "1"; pdata[2, 329] = "ProductID-007"; pdata[3, 329] = "LotyyyyMMddID-003"; pdata[4, 329] = "66.25";
            pdata[1, 330] = "1"; pdata[2, 330] = "ProductID-007"; pdata[3, 330] = "LotyyyyMMddID-003"; pdata[4, 330] = "76.84";
            pdata[1, 331] = "1"; pdata[2, 331] = "ProductID-007"; pdata[3, 331] = "LotyyyyMMddID-004"; pdata[4, 331] = "67.94"; //Group-007 - Sub-004
            pdata[1, 332] = "1"; pdata[2, 332] = "ProductID-007"; pdata[3, 332] = "LotyyyyMMddID-004"; pdata[4, 332] = "11.56";
            pdata[1, 333] = "1"; pdata[2, 333] = "ProductID-007"; pdata[3, 333] = "LotyyyyMMddID-004"; pdata[4, 333] = "61.07";
            pdata[1, 334] = "1"; pdata[2, 334] = "ProductID-007"; pdata[3, 334] = "LotyyyyMMddID-004"; pdata[4, 334] = "87.44";
            pdata[1, 335] = "1"; pdata[2, 335] = "ProductID-007"; pdata[3, 335] = "LotyyyyMMddID-004"; pdata[4, 335] = "59.49";
            pdata[1, 336] = "1"; pdata[2, 336] = "ProductID-007"; pdata[3, 336] = "LotyyyyMMddID-004"; pdata[4, 336] = "20.9";
            pdata[1, 337] = "1"; pdata[2, 337] = "ProductID-007"; pdata[3, 337] = "LotyyyyMMddID-004"; pdata[4, 337] = "34.03";
            pdata[1, 338] = "1"; pdata[2, 338] = "ProductID-007"; pdata[3, 338] = "LotyyyyMMddID-004"; pdata[4, 338] = "1.73";
            pdata[1, 339] = "1"; pdata[2, 339] = "ProductID-007"; pdata[3, 339] = "LotyyyyMMddID-004"; pdata[4, 339] = "81.85";
            pdata[1, 340] = "1"; pdata[2, 340] = "ProductID-007"; pdata[3, 340] = "LotyyyyMMddID-004"; pdata[4, 340] = "70.09";
            pdata[1, 341] = "1"; pdata[2, 341] = "ProductID-007"; pdata[3, 341] = "LotyyyyMMddID-005"; pdata[4, 341] = "9.4"; //Group-007 - Sub-005
            pdata[1, 342] = "1"; pdata[2, 342] = "ProductID-007"; pdata[3, 342] = "LotyyyyMMddID-005"; pdata[4, 342] = "3.52";
            pdata[1, 343] = "1"; pdata[2, 343] = "ProductID-007"; pdata[3, 343] = "LotyyyyMMddID-005"; pdata[4, 343] = "28.29";
            pdata[1, 344] = "1"; pdata[2, 344] = "ProductID-007"; pdata[3, 344] = "LotyyyyMMddID-005"; pdata[4, 344] = "65.82";
            pdata[1, 345] = "1"; pdata[2, 345] = "ProductID-007"; pdata[3, 345] = "LotyyyyMMddID-005"; pdata[4, 345] = "24.11";
            pdata[1, 346] = "1"; pdata[2, 346] = "ProductID-007"; pdata[3, 346] = "LotyyyyMMddID-005"; pdata[4, 346] = "70.19";
            pdata[1, 347] = "1"; pdata[2, 347] = "ProductID-007"; pdata[3, 347] = "LotyyyyMMddID-005"; pdata[4, 347] = "17.03";
            pdata[1, 348] = "1"; pdata[2, 348] = "ProductID-007"; pdata[3, 348] = "LotyyyyMMddID-005"; pdata[4, 348] = "92.47";
            pdata[1, 349] = "1"; pdata[2, 349] = "ProductID-007"; pdata[3, 349] = "LotyyyyMMddID-005"; pdata[4, 349] = "46.49";
            pdata[1, 350] = "1"; pdata[2, 350] = "ProductID-007"; pdata[3, 350] = "LotyyyyMMddID-005"; pdata[4, 350] = "26.02";
            pdata[1, 351] = "1"; pdata[2, 351] = "ProductID-008"; pdata[3, 351] = "LotyyyyMMddID-001"; pdata[4, 351] = "74.34"; //Group-008 - Sub-001
            pdata[1, 352] = "1"; pdata[2, 352] = "ProductID-008"; pdata[3, 352] = "LotyyyyMMddID-001"; pdata[4, 352] = "61.13";
            pdata[1, 353] = "1"; pdata[2, 353] = "ProductID-008"; pdata[3, 353] = "LotyyyyMMddID-001"; pdata[4, 353] = "57.88";
            pdata[1, 354] = "1"; pdata[2, 354] = "ProductID-008"; pdata[3, 354] = "LotyyyyMMddID-001"; pdata[4, 354] = "69.68";
            pdata[1, 355] = "1"; pdata[2, 355] = "ProductID-008"; pdata[3, 355] = "LotyyyyMMddID-001"; pdata[4, 355] = "58.22";
            pdata[1, 356] = "1"; pdata[2, 356] = "ProductID-008"; pdata[3, 356] = "LotyyyyMMddID-001"; pdata[4, 356] = "50.8";
            pdata[1, 357] = "1"; pdata[2, 357] = "ProductID-008"; pdata[3, 357] = "LotyyyyMMddID-001"; pdata[4, 357] = "54.67";
            pdata[1, 358] = "1"; pdata[2, 358] = "ProductID-008"; pdata[3, 358] = "LotyyyyMMddID-001"; pdata[4, 358] = "76.27";
            pdata[1, 359] = "1"; pdata[2, 359] = "ProductID-008"; pdata[3, 359] = "LotyyyyMMddID-001"; pdata[4, 359] = "91.58";
            pdata[1, 360] = "1"; pdata[2, 360] = "ProductID-008"; pdata[3, 360] = "LotyyyyMMddID-001"; pdata[4, 360] = "29.32";
            pdata[1, 361] = "1"; pdata[2, 361] = "ProductID-008"; pdata[3, 361] = "LotyyyyMMddID-002"; pdata[4, 361] = "42.05"; //Group-008 - Sub-002
            pdata[1, 362] = "1"; pdata[2, 362] = "ProductID-008"; pdata[3, 362] = "LotyyyyMMddID-002"; pdata[4, 362] = "20.49";
            pdata[1, 363] = "1"; pdata[2, 363] = "ProductID-008"; pdata[3, 363] = "LotyyyyMMddID-002"; pdata[4, 363] = "78.01";
            pdata[1, 364] = "1"; pdata[2, 364] = "ProductID-008"; pdata[3, 364] = "LotyyyyMMddID-002"; pdata[4, 364] = "74.32";
            pdata[1, 365] = "1"; pdata[2, 365] = "ProductID-008"; pdata[3, 365] = "LotyyyyMMddID-002"; pdata[4, 365] = "10.54";
            pdata[1, 366] = "1"; pdata[2, 366] = "ProductID-008"; pdata[3, 366] = "LotyyyyMMddID-002"; pdata[4, 366] = "27.32";
            pdata[1, 367] = "1"; pdata[2, 367] = "ProductID-008"; pdata[3, 367] = "LotyyyyMMddID-002"; pdata[4, 367] = "60.69";
            pdata[1, 368] = "1"; pdata[2, 368] = "ProductID-008"; pdata[3, 368] = "LotyyyyMMddID-002"; pdata[4, 368] = "35.74";
            pdata[1, 369] = "1"; pdata[2, 369] = "ProductID-008"; pdata[3, 369] = "LotyyyyMMddID-002"; pdata[4, 369] = "79.89";
            pdata[1, 370] = "1"; pdata[2, 370] = "ProductID-008"; pdata[3, 370] = "LotyyyyMMddID-002"; pdata[4, 370] = "95.39";
            pdata[1, 371] = "1"; pdata[2, 371] = "ProductID-008"; pdata[3, 371] = "LotyyyyMMddID-003"; pdata[4, 371] = "60.96"; //Group-008 - Sub-003
            pdata[1, 372] = "1"; pdata[2, 372] = "ProductID-008"; pdata[3, 372] = "LotyyyyMMddID-003"; pdata[4, 372] = "25.03";
            pdata[1, 373] = "1"; pdata[2, 373] = "ProductID-008"; pdata[3, 373] = "LotyyyyMMddID-003"; pdata[4, 373] = "60.9";
            pdata[1, 374] = "1"; pdata[2, 374] = "ProductID-008"; pdata[3, 374] = "LotyyyyMMddID-003"; pdata[4, 374] = "65.49";
            pdata[1, 375] = "1"; pdata[2, 375] = "ProductID-008"; pdata[3, 375] = "LotyyyyMMddID-003"; pdata[4, 375] = "27.66";
            pdata[1, 376] = "1"; pdata[2, 376] = "ProductID-008"; pdata[3, 376] = "LotyyyyMMddID-003"; pdata[4, 376] = "55.88";
            pdata[1, 377] = "1"; pdata[2, 377] = "ProductID-008"; pdata[3, 377] = "LotyyyyMMddID-003"; pdata[4, 377] = "21.94";
            pdata[1, 378] = "1"; pdata[2, 378] = "ProductID-008"; pdata[3, 378] = "LotyyyyMMddID-003"; pdata[4, 378] = "60.7";
            pdata[1, 379] = "1"; pdata[2, 379] = "ProductID-008"; pdata[3, 379] = "LotyyyyMMddID-003"; pdata[4, 379] = "14.12";
            pdata[1, 380] = "1"; pdata[2, 380] = "ProductID-008"; pdata[3, 380] = "LotyyyyMMddID-003"; pdata[4, 380] = "34.56";
            pdata[1, 381] = "1"; pdata[2, 381] = "ProductID-008"; pdata[3, 381] = "LotyyyyMMddID-004"; pdata[4, 381] = "14.37"; //Group-008 - Sub-004
            pdata[1, 382] = "1"; pdata[2, 382] = "ProductID-008"; pdata[3, 382] = "LotyyyyMMddID-004"; pdata[4, 382] = "59.04";
            pdata[1, 383] = "1"; pdata[2, 383] = "ProductID-008"; pdata[3, 383] = "LotyyyyMMddID-004"; pdata[4, 383] = "94.18";
            pdata[1, 384] = "1"; pdata[2, 384] = "ProductID-008"; pdata[3, 384] = "LotyyyyMMddID-004"; pdata[4, 384] = "20.85";
            pdata[1, 385] = "1"; pdata[2, 385] = "ProductID-008"; pdata[3, 385] = "LotyyyyMMddID-004"; pdata[4, 385] = "33.02";
            pdata[1, 386] = "1"; pdata[2, 386] = "ProductID-008"; pdata[3, 386] = "LotyyyyMMddID-004"; pdata[4, 386] = "48.03";
            pdata[1, 387] = "1"; pdata[2, 387] = "ProductID-008"; pdata[3, 387] = "LotyyyyMMddID-004"; pdata[4, 387] = "63.89";
            pdata[1, 388] = "1"; pdata[2, 388] = "ProductID-008"; pdata[3, 388] = "LotyyyyMMddID-004"; pdata[4, 388] = "71.46";
            pdata[1, 389] = "1"; pdata[2, 389] = "ProductID-008"; pdata[3, 389] = "LotyyyyMMddID-004"; pdata[4, 389] = "79.18";
            pdata[1, 390] = "1"; pdata[2, 390] = "ProductID-008"; pdata[3, 390] = "LotyyyyMMddID-004"; pdata[4, 390] = "88.88";
            pdata[1, 391] = "1"; pdata[2, 391] = "ProductID-008"; pdata[3, 391] = "LotyyyyMMddID-005"; pdata[4, 391] = "36.26"; //Group-008 - Sub-005
            pdata[1, 392] = "1"; pdata[2, 392] = "ProductID-008"; pdata[3, 392] = "LotyyyyMMddID-005"; pdata[4, 392] = "5.44";
            pdata[1, 393] = "1"; pdata[2, 393] = "ProductID-008"; pdata[3, 393] = "LotyyyyMMddID-005"; pdata[4, 393] = "99.39";
            pdata[1, 394] = "1"; pdata[2, 394] = "ProductID-008"; pdata[3, 394] = "LotyyyyMMddID-005"; pdata[4, 394] = "12.66";
            pdata[1, 395] = "1"; pdata[2, 395] = "ProductID-008"; pdata[3, 395] = "LotyyyyMMddID-005"; pdata[4, 395] = "10.65";
            pdata[1, 396] = "1"; pdata[2, 396] = "ProductID-008"; pdata[3, 396] = "LotyyyyMMddID-005"; pdata[4, 396] = "61.67";
            pdata[1, 397] = "1"; pdata[2, 397] = "ProductID-008"; pdata[3, 397] = "LotyyyyMMddID-005"; pdata[4, 397] = "55.39";
            pdata[1, 398] = "1"; pdata[2, 398] = "ProductID-008"; pdata[3, 398] = "LotyyyyMMddID-005"; pdata[4, 398] = "25.23";
            pdata[1, 399] = "1"; pdata[2, 399] = "ProductID-008"; pdata[3, 399] = "LotyyyyMMddID-005"; pdata[4, 399] = "38.21";
            pdata[1, 400] = "1"; pdata[2, 400] = "ProductID-008"; pdata[3, 400] = "LotyyyyMMddID-005"; pdata[4, 400] = "8.43";
            pdata[1, 401] = "1"; pdata[2, 401] = "ProductID-009"; pdata[3, 401] = "LotyyyyMMddID-001"; pdata[4, 401] = "71.79"; //Group-009 - Sub-001
            pdata[1, 402] = "1"; pdata[2, 402] = "ProductID-009"; pdata[3, 402] = "LotyyyyMMddID-001"; pdata[4, 402] = "17.26";
            pdata[1, 403] = "1"; pdata[2, 403] = "ProductID-009"; pdata[3, 403] = "LotyyyyMMddID-001"; pdata[4, 403] = "18.53";
            pdata[1, 404] = "1"; pdata[2, 404] = "ProductID-009"; pdata[3, 404] = "LotyyyyMMddID-001"; pdata[4, 404] = "25.31";
            pdata[1, 405] = "1"; pdata[2, 405] = "ProductID-009"; pdata[3, 405] = "LotyyyyMMddID-001"; pdata[4, 405] = "23.14";
            pdata[1, 406] = "1"; pdata[2, 406] = "ProductID-009"; pdata[3, 406] = "LotyyyyMMddID-001"; pdata[4, 406] = "90.96";
            pdata[1, 407] = "1"; pdata[2, 407] = "ProductID-009"; pdata[3, 407] = "LotyyyyMMddID-001"; pdata[4, 407] = "23.46";
            pdata[1, 408] = "1"; pdata[2, 408] = "ProductID-009"; pdata[3, 408] = "LotyyyyMMddID-001"; pdata[4, 408] = "21.88";
            pdata[1, 409] = "1"; pdata[2, 409] = "ProductID-009"; pdata[3, 409] = "LotyyyyMMddID-001"; pdata[4, 409] = "33.15";
            pdata[1, 410] = "1"; pdata[2, 410] = "ProductID-009"; pdata[3, 410] = "LotyyyyMMddID-001"; pdata[4, 410] = "5.55";
            pdata[1, 411] = "1"; pdata[2, 411] = "ProductID-009"; pdata[3, 411] = "LotyyyyMMddID-002"; pdata[4, 411] = "6.54"; //Group-009 - Sub-002
            pdata[1, 412] = "1"; pdata[2, 412] = "ProductID-009"; pdata[3, 412] = "LotyyyyMMddID-002"; pdata[4, 412] = "76.67";
            pdata[1, 413] = "1"; pdata[2, 413] = "ProductID-009"; pdata[3, 413] = "LotyyyyMMddID-002"; pdata[4, 413] = "40.8";
            pdata[1, 414] = "1"; pdata[2, 414] = "ProductID-009"; pdata[3, 414] = "LotyyyyMMddID-002"; pdata[4, 414] = "47.24";
            pdata[1, 415] = "1"; pdata[2, 415] = "ProductID-009"; pdata[3, 415] = "LotyyyyMMddID-002"; pdata[4, 415] = "65.49";
            pdata[1, 416] = "1"; pdata[2, 416] = "ProductID-009"; pdata[3, 416] = "LotyyyyMMddID-002"; pdata[4, 416] = "9.58";
            pdata[1, 417] = "1"; pdata[2, 417] = "ProductID-009"; pdata[3, 417] = "LotyyyyMMddID-002"; pdata[4, 417] = "10.46";
            pdata[1, 418] = "1"; pdata[2, 418] = "ProductID-009"; pdata[3, 418] = "LotyyyyMMddID-002"; pdata[4, 418] = "17.57";
            pdata[1, 419] = "1"; pdata[2, 419] = "ProductID-009"; pdata[3, 419] = "LotyyyyMMddID-002"; pdata[4, 419] = "9.79";
            pdata[1, 420] = "1"; pdata[2, 420] = "ProductID-009"; pdata[3, 420] = "LotyyyyMMddID-002"; pdata[4, 420] = "26.47";
            pdata[1, 421] = "1"; pdata[2, 421] = "ProductID-009"; pdata[3, 421] = "LotyyyyMMddID-003"; pdata[4, 421] = "15.81"; //Group-009 - Sub-003
            pdata[1, 422] = "1"; pdata[2, 422] = "ProductID-009"; pdata[3, 422] = "LotyyyyMMddID-003"; pdata[4, 422] = "43.95";
            pdata[1, 423] = "1"; pdata[2, 423] = "ProductID-009"; pdata[3, 423] = "LotyyyyMMddID-003"; pdata[4, 423] = "74.65";
            pdata[1, 424] = "1"; pdata[2, 424] = "ProductID-009"; pdata[3, 424] = "LotyyyyMMddID-003"; pdata[4, 424] = "45.1";
            pdata[1, 425] = "1"; pdata[2, 425] = "ProductID-009"; pdata[3, 425] = "LotyyyyMMddID-003"; pdata[4, 425] = "19.36";
            pdata[1, 426] = "1"; pdata[2, 426] = "ProductID-009"; pdata[3, 426] = "LotyyyyMMddID-003"; pdata[4, 426] = "90.23";
            pdata[1, 427] = "1"; pdata[2, 427] = "ProductID-009"; pdata[3, 427] = "LotyyyyMMddID-003"; pdata[4, 427] = "17.53";
            pdata[1, 428] = "1"; pdata[2, 428] = "ProductID-009"; pdata[3, 428] = "LotyyyyMMddID-003"; pdata[4, 428] = "38.17";
            pdata[1, 429] = "1"; pdata[2, 429] = "ProductID-009"; pdata[3, 429] = "LotyyyyMMddID-003"; pdata[4, 429] = "4.49";
            pdata[1, 430] = "1"; pdata[2, 430] = "ProductID-009"; pdata[3, 430] = "LotyyyyMMddID-003"; pdata[4, 430] = "85.38";
            pdata[1, 431] = "1"; pdata[2, 431] = "ProductID-009"; pdata[3, 431] = "LotyyyyMMddID-004"; pdata[4, 431] = "6.02"; //Group-009 - Sub-004
            pdata[1, 432] = "1"; pdata[2, 432] = "ProductID-009"; pdata[3, 432] = "LotyyyyMMddID-004"; pdata[4, 432] = "65.74";
            pdata[1, 433] = "1"; pdata[2, 433] = "ProductID-009"; pdata[3, 433] = "LotyyyyMMddID-004"; pdata[4, 433] = "6.32";
            pdata[1, 434] = "1"; pdata[2, 434] = "ProductID-009"; pdata[3, 434] = "LotyyyyMMddID-004"; pdata[4, 434] = "86.77";
            pdata[1, 435] = "1"; pdata[2, 435] = "ProductID-009"; pdata[3, 435] = "LotyyyyMMddID-004"; pdata[4, 435] = "29.64";
            pdata[1, 436] = "1"; pdata[2, 436] = "ProductID-009"; pdata[3, 436] = "LotyyyyMMddID-004"; pdata[4, 436] = "80.53";
            pdata[1, 437] = "1"; pdata[2, 437] = "ProductID-009"; pdata[3, 437] = "LotyyyyMMddID-004"; pdata[4, 437] = "88.58";
            pdata[1, 438] = "1"; pdata[2, 438] = "ProductID-009"; pdata[3, 438] = "LotyyyyMMddID-004"; pdata[4, 438] = "41.24";
            pdata[1, 439] = "1"; pdata[2, 439] = "ProductID-009"; pdata[3, 439] = "LotyyyyMMddID-004"; pdata[4, 439] = "11.81";
            pdata[1, 440] = "1"; pdata[2, 440] = "ProductID-009"; pdata[3, 440] = "LotyyyyMMddID-004"; pdata[4, 440] = "93.75";
            pdata[1, 441] = "1"; pdata[2, 441] = "ProductID-009"; pdata[3, 441] = "LotyyyyMMddID-005"; pdata[4, 441] = "36.38"; //Group-009 - Sub-005
            pdata[1, 442] = "1"; pdata[2, 442] = "ProductID-009"; pdata[3, 442] = "LotyyyyMMddID-005"; pdata[4, 442] = "5.29";
            pdata[1, 443] = "1"; pdata[2, 443] = "ProductID-009"; pdata[3, 443] = "LotyyyyMMddID-005"; pdata[4, 443] = "58.85";
            pdata[1, 444] = "1"; pdata[2, 444] = "ProductID-009"; pdata[3, 444] = "LotyyyyMMddID-005"; pdata[4, 444] = "42.57";
            pdata[1, 445] = "1"; pdata[2, 445] = "ProductID-009"; pdata[3, 445] = "LotyyyyMMddID-005"; pdata[4, 445] = "8.01";
            pdata[1, 446] = "1"; pdata[2, 446] = "ProductID-009"; pdata[3, 446] = "LotyyyyMMddID-005"; pdata[4, 446] = "53.59";
            pdata[1, 447] = "1"; pdata[2, 447] = "ProductID-009"; pdata[3, 447] = "LotyyyyMMddID-005"; pdata[4, 447] = "94.88";
            pdata[1, 448] = "1"; pdata[2, 448] = "ProductID-009"; pdata[3, 448] = "LotyyyyMMddID-005"; pdata[4, 448] = "41.13";
            pdata[1, 449] = "1"; pdata[2, 449] = "ProductID-009"; pdata[3, 449] = "LotyyyyMMddID-005"; pdata[4, 449] = "16.69";
            pdata[1, 450] = "1"; pdata[2, 450] = "ProductID-009"; pdata[3, 450] = "LotyyyyMMddID-005"; pdata[4, 450] = "89.43";
            pdata[1, 451] = "1"; pdata[2, 451] = "ProductID-010"; pdata[3, 451] = "LotyyyyMMddID-001"; pdata[4, 451] = "78.67"; //Group-010 - Sub-001
            pdata[1, 452] = "1"; pdata[2, 452] = "ProductID-010"; pdata[3, 452] = "LotyyyyMMddID-001"; pdata[4, 452] = "80.55";
            pdata[1, 453] = "1"; pdata[2, 453] = "ProductID-010"; pdata[3, 453] = "LotyyyyMMddID-001"; pdata[4, 453] = "56.38";
            pdata[1, 454] = "1"; pdata[2, 454] = "ProductID-010"; pdata[3, 454] = "LotyyyyMMddID-001"; pdata[4, 454] = "35.58";
            pdata[1, 455] = "1"; pdata[2, 455] = "ProductID-010"; pdata[3, 455] = "LotyyyyMMddID-001"; pdata[4, 455] = "13.14";
            pdata[1, 456] = "1"; pdata[2, 456] = "ProductID-010"; pdata[3, 456] = "LotyyyyMMddID-001"; pdata[4, 456] = "6.23";
            pdata[1, 457] = "1"; pdata[2, 457] = "ProductID-010"; pdata[3, 457] = "LotyyyyMMddID-001"; pdata[4, 457] = "56.1";
            pdata[1, 458] = "1"; pdata[2, 458] = "ProductID-010"; pdata[3, 458] = "LotyyyyMMddID-001"; pdata[4, 458] = "2.68";
            pdata[1, 459] = "1"; pdata[2, 459] = "ProductID-010"; pdata[3, 459] = "LotyyyyMMddID-001"; pdata[4, 459] = "96.37";
            pdata[1, 460] = "1"; pdata[2, 460] = "ProductID-010"; pdata[3, 460] = "LotyyyyMMddID-001"; pdata[4, 460] = "16.2";
            pdata[1, 461] = "1"; pdata[2, 461] = "ProductID-010"; pdata[3, 461] = "LotyyyyMMddID-002"; pdata[4, 461] = "89.3"; //Group-010 - Sub-002
            pdata[1, 462] = "1"; pdata[2, 462] = "ProductID-010"; pdata[3, 462] = "LotyyyyMMddID-002"; pdata[4, 462] = "69.29";
            pdata[1, 463] = "1"; pdata[2, 463] = "ProductID-010"; pdata[3, 463] = "LotyyyyMMddID-002"; pdata[4, 463] = "11.22";
            pdata[1, 464] = "1"; pdata[2, 464] = "ProductID-010"; pdata[3, 464] = "LotyyyyMMddID-002"; pdata[4, 464] = "59.99";
            pdata[1, 465] = "1"; pdata[2, 465] = "ProductID-010"; pdata[3, 465] = "LotyyyyMMddID-002"; pdata[4, 465] = "18.28";
            pdata[1, 466] = "1"; pdata[2, 466] = "ProductID-010"; pdata[3, 466] = "LotyyyyMMddID-002"; pdata[4, 466] = "9.42";
            pdata[1, 467] = "1"; pdata[2, 467] = "ProductID-010"; pdata[3, 467] = "LotyyyyMMddID-002"; pdata[4, 467] = "91.5";
            pdata[1, 468] = "1"; pdata[2, 468] = "ProductID-010"; pdata[3, 468] = "LotyyyyMMddID-002"; pdata[4, 468] = "97.68";
            pdata[1, 469] = "1"; pdata[2, 469] = "ProductID-010"; pdata[3, 469] = "LotyyyyMMddID-002"; pdata[4, 469] = "58.5";
            pdata[1, 470] = "1"; pdata[2, 470] = "ProductID-010"; pdata[3, 470] = "LotyyyyMMddID-002"; pdata[4, 470] = "73.21";
            pdata[1, 471] = "1"; pdata[2, 471] = "ProductID-010"; pdata[3, 471] = "LotyyyyMMddID-003"; pdata[4, 471] = "51.84"; //Group-010 - Sub-003
            pdata[1, 472] = "1"; pdata[2, 472] = "ProductID-010"; pdata[3, 472] = "LotyyyyMMddID-003"; pdata[4, 472] = "44.94";
            pdata[1, 473] = "1"; pdata[2, 473] = "ProductID-010"; pdata[3, 473] = "LotyyyyMMddID-003"; pdata[4, 473] = "94.31";
            pdata[1, 474] = "1"; pdata[2, 474] = "ProductID-010"; pdata[3, 474] = "LotyyyyMMddID-003"; pdata[4, 474] = "3.84";
            pdata[1, 475] = "1"; pdata[2, 475] = "ProductID-010"; pdata[3, 475] = "LotyyyyMMddID-003"; pdata[4, 475] = "87.73";
            pdata[1, 476] = "1"; pdata[2, 476] = "ProductID-010"; pdata[3, 476] = "LotyyyyMMddID-003"; pdata[4, 476] = "17.79";
            pdata[1, 477] = "1"; pdata[2, 477] = "ProductID-010"; pdata[3, 477] = "LotyyyyMMddID-003"; pdata[4, 477] = "54.7";
            pdata[1, 478] = "1"; pdata[2, 478] = "ProductID-010"; pdata[3, 478] = "LotyyyyMMddID-003"; pdata[4, 478] = "53.49";
            pdata[1, 479] = "1"; pdata[2, 479] = "ProductID-010"; pdata[3, 479] = "LotyyyyMMddID-003"; pdata[4, 479] = "97.51";
            pdata[1, 480] = "1"; pdata[2, 480] = "ProductID-010"; pdata[3, 480] = "LotyyyyMMddID-003"; pdata[4, 480] = "45.35";
            pdata[1, 481] = "1"; pdata[2, 481] = "ProductID-010"; pdata[3, 481] = "LotyyyyMMddID-004"; pdata[4, 481] = "78.35"; //Group-010 - Sub-004
            pdata[1, 482] = "1"; pdata[2, 482] = "ProductID-010"; pdata[3, 482] = "LotyyyyMMddID-004"; pdata[4, 482] = "41.84";
            pdata[1, 483] = "1"; pdata[2, 483] = "ProductID-010"; pdata[3, 483] = "LotyyyyMMddID-004"; pdata[4, 483] = "6.64";
            pdata[1, 484] = "1"; pdata[2, 484] = "ProductID-010"; pdata[3, 484] = "LotyyyyMMddID-004"; pdata[4, 484] = "32.06";
            pdata[1, 485] = "1"; pdata[2, 485] = "ProductID-010"; pdata[3, 485] = "LotyyyyMMddID-004"; pdata[4, 485] = "13.33";
            pdata[1, 486] = "1"; pdata[2, 486] = "ProductID-010"; pdata[3, 486] = "LotyyyyMMddID-004"; pdata[4, 486] = "11.47";
            pdata[1, 487] = "1"; pdata[2, 487] = "ProductID-010"; pdata[3, 487] = "LotyyyyMMddID-004"; pdata[4, 487] = "63.94";
            pdata[1, 488] = "1"; pdata[2, 488] = "ProductID-010"; pdata[3, 488] = "LotyyyyMMddID-004"; pdata[4, 488] = "69.06";
            pdata[1, 489] = "1"; pdata[2, 489] = "ProductID-010"; pdata[3, 489] = "LotyyyyMMddID-004"; pdata[4, 489] = "85.75";
            pdata[1, 490] = "1"; pdata[2, 490] = "ProductID-010"; pdata[3, 490] = "LotyyyyMMddID-004"; pdata[4, 490] = "34.79";
            pdata[1, 491] = "1"; pdata[2, 491] = "ProductID-010"; pdata[3, 491] = "LotyyyyMMddID-005"; pdata[4, 491] = "19.74"; //Group-010 - Sub-005
            pdata[1, 492] = "1"; pdata[2, 492] = "ProductID-010"; pdata[3, 492] = "LotyyyyMMddID-005"; pdata[4, 492] = "13.45";
            pdata[1, 493] = "1"; pdata[2, 493] = "ProductID-010"; pdata[3, 493] = "LotyyyyMMddID-005"; pdata[4, 493] = "12.29";
            pdata[1, 494] = "1"; pdata[2, 494] = "ProductID-010"; pdata[3, 494] = "LotyyyyMMddID-005"; pdata[4, 494] = "87.21";
            pdata[1, 495] = "1"; pdata[2, 495] = "ProductID-010"; pdata[3, 495] = "LotyyyyMMddID-005"; pdata[4, 495] = "97.49";
            pdata[1, 496] = "1"; pdata[2, 496] = "ProductID-010"; pdata[3, 496] = "LotyyyyMMddID-005"; pdata[4, 496] = "71.07";
            pdata[1, 497] = "1"; pdata[2, 497] = "ProductID-010"; pdata[3, 497] = "LotyyyyMMddID-005"; pdata[4, 497] = "5.39";
            pdata[1, 498] = "1"; pdata[2, 498] = "ProductID-010"; pdata[3, 498] = "LotyyyyMMddID-005"; pdata[4, 498] = "42.16";
            pdata[1, 499] = "1"; pdata[2, 499] = "ProductID-010"; pdata[3, 499] = "LotyyyyMMddID-005"; pdata[4, 499] = "43.6";
            pdata[1, 500] = "1"; pdata[2, 500] = "ProductID-010"; pdata[3, 500] = "LotyyyyMMddID-005"; pdata[4, 500] = "88.31";
            pdata[1, 501] = "1"; pdata[2, 501] = "ProductID-011"; pdata[3, 501] = "LotyyyyMMddID-001"; pdata[4, 501] = "99.97"; //Group-011 - Sub-001
            pdata[1, 502] = "1"; pdata[2, 502] = "ProductID-011"; pdata[3, 502] = "LotyyyyMMddID-001"; pdata[4, 502] = "72.54";
            pdata[1, 503] = "1"; pdata[2, 503] = "ProductID-011"; pdata[3, 503] = "LotyyyyMMddID-001"; pdata[4, 503] = "63.69";
            pdata[1, 504] = "1"; pdata[2, 504] = "ProductID-011"; pdata[3, 504] = "LotyyyyMMddID-001"; pdata[4, 504] = "17.59";
            pdata[1, 505] = "1"; pdata[2, 505] = "ProductID-011"; pdata[3, 505] = "LotyyyyMMddID-001"; pdata[4, 505] = "1.48";
            pdata[1, 506] = "1"; pdata[2, 506] = "ProductID-011"; pdata[3, 506] = "LotyyyyMMddID-001"; pdata[4, 506] = "63.23";
            pdata[1, 507] = "1"; pdata[2, 507] = "ProductID-011"; pdata[3, 507] = "LotyyyyMMddID-001"; pdata[4, 507] = "17.66";
            pdata[1, 508] = "1"; pdata[2, 508] = "ProductID-011"; pdata[3, 508] = "LotyyyyMMddID-001"; pdata[4, 508] = "16.05";
            pdata[1, 509] = "1"; pdata[2, 509] = "ProductID-011"; pdata[3, 509] = "LotyyyyMMddID-001"; pdata[4, 509] = "23.96";
            pdata[1, 510] = "1"; pdata[2, 510] = "ProductID-011"; pdata[3, 510] = "LotyyyyMMddID-001"; pdata[4, 510] = "72.76";
            pdata[1, 511] = "1"; pdata[2, 511] = "ProductID-011"; pdata[3, 511] = "LotyyyyMMddID-002"; pdata[4, 511] = "76.11"; //Group-011 - Sub-002
            pdata[1, 512] = "1"; pdata[2, 512] = "ProductID-011"; pdata[3, 512] = "LotyyyyMMddID-002"; pdata[4, 512] = "9.63";
            pdata[1, 513] = "1"; pdata[2, 513] = "ProductID-011"; pdata[3, 513] = "LotyyyyMMddID-002"; pdata[4, 513] = "62.04";
            pdata[1, 514] = "1"; pdata[2, 514] = "ProductID-011"; pdata[3, 514] = "LotyyyyMMddID-002"; pdata[4, 514] = "43.24";
            pdata[1, 515] = "1"; pdata[2, 515] = "ProductID-011"; pdata[3, 515] = "LotyyyyMMddID-002"; pdata[4, 515] = "52.53";
            pdata[1, 516] = "1"; pdata[2, 516] = "ProductID-011"; pdata[3, 516] = "LotyyyyMMddID-002"; pdata[4, 516] = "62.26";
            pdata[1, 517] = "1"; pdata[2, 517] = "ProductID-011"; pdata[3, 517] = "LotyyyyMMddID-002"; pdata[4, 517] = "86.04";
            pdata[1, 518] = "1"; pdata[2, 518] = "ProductID-011"; pdata[3, 518] = "LotyyyyMMddID-002"; pdata[4, 518] = "79.22";
            pdata[1, 519] = "1"; pdata[2, 519] = "ProductID-011"; pdata[3, 519] = "LotyyyyMMddID-002"; pdata[4, 519] = "2.58";
            pdata[1, 520] = "1"; pdata[2, 520] = "ProductID-011"; pdata[3, 520] = "LotyyyyMMddID-002"; pdata[4, 520] = "82.43";
            pdata[1, 521] = "1"; pdata[2, 521] = "ProductID-011"; pdata[3, 521] = "LotyyyyMMddID-003"; pdata[4, 521] = "13.28"; //Group-011 - Sub-003
            pdata[1, 522] = "1"; pdata[2, 522] = "ProductID-011"; pdata[3, 522] = "LotyyyyMMddID-003"; pdata[4, 522] = "21.49";
            pdata[1, 523] = "1"; pdata[2, 523] = "ProductID-011"; pdata[3, 523] = "LotyyyyMMddID-003"; pdata[4, 523] = "85.48";
            pdata[1, 524] = "1"; pdata[2, 524] = "ProductID-011"; pdata[3, 524] = "LotyyyyMMddID-003"; pdata[4, 524] = "89.39";
            pdata[1, 525] = "1"; pdata[2, 525] = "ProductID-011"; pdata[3, 525] = "LotyyyyMMddID-003"; pdata[4, 525] = "23.38";
            pdata[1, 526] = "1"; pdata[2, 526] = "ProductID-011"; pdata[3, 526] = "LotyyyyMMddID-003"; pdata[4, 526] = "93.47";
            pdata[1, 527] = "1"; pdata[2, 527] = "ProductID-011"; pdata[3, 527] = "LotyyyyMMddID-003"; pdata[4, 527] = "87.12";
            pdata[1, 528] = "1"; pdata[2, 528] = "ProductID-011"; pdata[3, 528] = "LotyyyyMMddID-003"; pdata[4, 528] = "54.89";
            pdata[1, 529] = "1"; pdata[2, 529] = "ProductID-011"; pdata[3, 529] = "LotyyyyMMddID-003"; pdata[4, 529] = "88.09";
            pdata[1, 530] = "1"; pdata[2, 530] = "ProductID-011"; pdata[3, 530] = "LotyyyyMMddID-003"; pdata[4, 530] = "92.59";
            pdata[1, 531] = "1"; pdata[2, 531] = "ProductID-011"; pdata[3, 531] = "LotyyyyMMddID-004"; pdata[4, 531] = "27.92"; //Group-011 - Sub-004
            pdata[1, 532] = "1"; pdata[2, 532] = "ProductID-011"; pdata[3, 532] = "LotyyyyMMddID-004"; pdata[4, 532] = "8.47";
            pdata[1, 533] = "1"; pdata[2, 533] = "ProductID-011"; pdata[3, 533] = "LotyyyyMMddID-004"; pdata[4, 533] = "85.37";
            pdata[1, 534] = "1"; pdata[2, 534] = "ProductID-011"; pdata[3, 534] = "LotyyyyMMddID-004"; pdata[4, 534] = "36.14";
            pdata[1, 535] = "1"; pdata[2, 535] = "ProductID-011"; pdata[3, 535] = "LotyyyyMMddID-004"; pdata[4, 535] = "87.94";
            pdata[1, 536] = "1"; pdata[2, 536] = "ProductID-011"; pdata[3, 536] = "LotyyyyMMddID-004"; pdata[4, 536] = "3.64";
            pdata[1, 537] = "1"; pdata[2, 537] = "ProductID-011"; pdata[3, 537] = "LotyyyyMMddID-004"; pdata[4, 537] = "15.3";
            pdata[1, 538] = "1"; pdata[2, 538] = "ProductID-011"; pdata[3, 538] = "LotyyyyMMddID-004"; pdata[4, 538] = "64.49";
            pdata[1, 539] = "1"; pdata[2, 539] = "ProductID-011"; pdata[3, 539] = "LotyyyyMMddID-004"; pdata[4, 539] = "89.6";
            pdata[1, 540] = "1"; pdata[2, 540] = "ProductID-011"; pdata[3, 540] = "LotyyyyMMddID-004"; pdata[4, 540] = "77.14";
            pdata[1, 541] = "1"; pdata[2, 541] = "ProductID-011"; pdata[3, 541] = "LotyyyyMMddID-005"; pdata[4, 541] = "15.04"; //Group-011 - Sub-005
            pdata[1, 542] = "1"; pdata[2, 542] = "ProductID-011"; pdata[3, 542] = "LotyyyyMMddID-005"; pdata[4, 542] = "2.15";
            pdata[1, 543] = "1"; pdata[2, 543] = "ProductID-011"; pdata[3, 543] = "LotyyyyMMddID-005"; pdata[4, 543] = "60.15";
            pdata[1, 544] = "1"; pdata[2, 544] = "ProductID-011"; pdata[3, 544] = "LotyyyyMMddID-005"; pdata[4, 544] = "39.74";
            pdata[1, 545] = "1"; pdata[2, 545] = "ProductID-011"; pdata[3, 545] = "LotyyyyMMddID-005"; pdata[4, 545] = "49.89";
            pdata[1, 546] = "1"; pdata[2, 546] = "ProductID-011"; pdata[3, 546] = "LotyyyyMMddID-005"; pdata[4, 546] = "33.92";
            pdata[1, 547] = "1"; pdata[2, 547] = "ProductID-011"; pdata[3, 547] = "LotyyyyMMddID-005"; pdata[4, 547] = "66.23";
            pdata[1, 548] = "1"; pdata[2, 548] = "ProductID-011"; pdata[3, 548] = "LotyyyyMMddID-005"; pdata[4, 548] = "16.21";
            pdata[1, 549] = "1"; pdata[2, 549] = "ProductID-011"; pdata[3, 549] = "LotyyyyMMddID-005"; pdata[4, 549] = "21.73";
            pdata[1, 550] = "1"; pdata[2, 550] = "ProductID-011"; pdata[3, 550] = "LotyyyyMMddID-005"; pdata[4, 550] = "63.49";
            //pdata[1, 551] = "1"; pdata[2, 551] = "ProductID-012"; pdata[3, 551] = "LotyyyyMMddID-001"; pdata[4, 551] = "13.76"; //Group-012 - Sub-001
            //pdata[1, 552] = "1"; pdata[2, 552] = "ProductID-012"; pdata[3, 552] = "LotyyyyMMddID-001"; pdata[4, 552] = "51.28";
            //pdata[1, 553] = "1"; pdata[2, 553] = "ProductID-012"; pdata[3, 553] = "LotyyyyMMddID-001"; pdata[4, 553] = "35.29";
            //pdata[1, 554] = "1"; pdata[2, 554] = "ProductID-012"; pdata[3, 554] = "LotyyyyMMddID-001"; pdata[4, 554] = "33.57";
            //pdata[1, 555] = "1"; pdata[2, 555] = "ProductID-012"; pdata[3, 555] = "LotyyyyMMddID-001"; pdata[4, 555] = "48.06";
            //pdata[1, 556] = "1"; pdata[2, 556] = "ProductID-012"; pdata[3, 556] = "LotyyyyMMddID-001"; pdata[4, 556] = "1.29";
            //pdata[1, 557] = "1"; pdata[2, 557] = "ProductID-012"; pdata[3, 557] = "LotyyyyMMddID-001"; pdata[4, 557] = "31.78";
            //pdata[1, 558] = "1"; pdata[2, 558] = "ProductID-012"; pdata[3, 558] = "LotyyyyMMddID-001"; pdata[4, 558] = "45.23";
            //pdata[1, 559] = "1"; pdata[2, 559] = "ProductID-012"; pdata[3, 559] = "LotyyyyMMddID-001"; pdata[4, 559] = "91.93";
            //pdata[1, 560] = "1"; pdata[2, 560] = "ProductID-012"; pdata[3, 560] = "LotyyyyMMddID-001"; pdata[4, 560] = "87.71";
            //pdata[1, 561] = "1"; pdata[2, 561] = "ProductID-012"; pdata[3, 561] = "LotyyyyMMddID-002"; pdata[4, 561] = "98.61"; //Group-012 - Sub-002
            //pdata[1, 562] = "1"; pdata[2, 562] = "ProductID-012"; pdata[3, 562] = "LotyyyyMMddID-002"; pdata[4, 562] = "64.97";
            //pdata[1, 563] = "1"; pdata[2, 563] = "ProductID-012"; pdata[3, 563] = "LotyyyyMMddID-002"; pdata[4, 563] = "61.1";
            //pdata[1, 564] = "1"; pdata[2, 564] = "ProductID-012"; pdata[3, 564] = "LotyyyyMMddID-002"; pdata[4, 564] = "98.91";
            //pdata[1, 565] = "1"; pdata[2, 565] = "ProductID-012"; pdata[3, 565] = "LotyyyyMMddID-002"; pdata[4, 565] = "15.44";
            //pdata[1, 566] = "1"; pdata[2, 566] = "ProductID-012"; pdata[3, 566] = "LotyyyyMMddID-002"; pdata[4, 566] = "28.16";
            //pdata[1, 567] = "1"; pdata[2, 567] = "ProductID-012"; pdata[3, 567] = "LotyyyyMMddID-002"; pdata[4, 567] = "96.29";
            //pdata[1, 568] = "1"; pdata[2, 568] = "ProductID-012"; pdata[3, 568] = "LotyyyyMMddID-002"; pdata[4, 568] = "2.39";
            //pdata[1, 569] = "1"; pdata[2, 569] = "ProductID-012"; pdata[3, 569] = "LotyyyyMMddID-002"; pdata[4, 569] = "53.73";
            //pdata[1, 570] = "1"; pdata[2, 570] = "ProductID-012"; pdata[3, 570] = "LotyyyyMMddID-002"; pdata[4, 570] = "79.31";
            //pdata[1, 571] = "1"; pdata[2, 571] = "ProductID-012"; pdata[3, 571] = "LotyyyyMMddID-003"; pdata[4, 571] = "82.81"; //Group-012 - Sub-003
            //pdata[1, 572] = "1"; pdata[2, 572] = "ProductID-012"; pdata[3, 572] = "LotyyyyMMddID-003"; pdata[4, 572] = "27.44";
            //pdata[1, 573] = "1"; pdata[2, 573] = "ProductID-012"; pdata[3, 573] = "LotyyyyMMddID-003"; pdata[4, 573] = "89.05";
            //pdata[1, 574] = "1"; pdata[2, 574] = "ProductID-012"; pdata[3, 574] = "LotyyyyMMddID-003"; pdata[4, 574] = "62.87";
            //pdata[1, 575] = "1"; pdata[2, 575] = "ProductID-012"; pdata[3, 575] = "LotyyyyMMddID-003"; pdata[4, 575] = "78.93";
            //pdata[1, 576] = "1"; pdata[2, 576] = "ProductID-012"; pdata[3, 576] = "LotyyyyMMddID-003"; pdata[4, 576] = "29.28";
            //pdata[1, 577] = "1"; pdata[2, 577] = "ProductID-012"; pdata[3, 577] = "LotyyyyMMddID-003"; pdata[4, 577] = "86.92";
            //pdata[1, 578] = "1"; pdata[2, 578] = "ProductID-012"; pdata[3, 578] = "LotyyyyMMddID-003"; pdata[4, 578] = "50.51";
            //pdata[1, 579] = "1"; pdata[2, 579] = "ProductID-012"; pdata[3, 579] = "LotyyyyMMddID-003"; pdata[4, 579] = "12.54";
            //pdata[1, 580] = "1"; pdata[2, 580] = "ProductID-012"; pdata[3, 580] = "LotyyyyMMddID-003"; pdata[4, 580] = "66.26";
            //pdata[1, 581] = "1"; pdata[2, 581] = "ProductID-012"; pdata[3, 581] = "LotyyyyMMddID-004"; pdata[4, 581] = "31.42"; //Group-012 - Sub-004
            //pdata[1, 582] = "1"; pdata[2, 582] = "ProductID-012"; pdata[3, 582] = "LotyyyyMMddID-004"; pdata[4, 582] = "43.05";
            //pdata[1, 583] = "1"; pdata[2, 583] = "ProductID-012"; pdata[3, 583] = "LotyyyyMMddID-004"; pdata[4, 583] = "77.84";
            //pdata[1, 584] = "1"; pdata[2, 584] = "ProductID-012"; pdata[3, 584] = "LotyyyyMMddID-004"; pdata[4, 584] = "32.03";
            //pdata[1, 585] = "1"; pdata[2, 585] = "ProductID-012"; pdata[3, 585] = "LotyyyyMMddID-004"; pdata[4, 585] = "76.07";
            //pdata[1, 586] = "1"; pdata[2, 586] = "ProductID-012"; pdata[3, 586] = "LotyyyyMMddID-004"; pdata[4, 586] = "70.18";
            //pdata[1, 587] = "1"; pdata[2, 587] = "ProductID-012"; pdata[3, 587] = "LotyyyyMMddID-004"; pdata[4, 587] = "15.56";
            //pdata[1, 588] = "1"; pdata[2, 588] = "ProductID-012"; pdata[3, 588] = "LotyyyyMMddID-004"; pdata[4, 588] = "89.17";
            //pdata[1, 589] = "1"; pdata[2, 589] = "ProductID-012"; pdata[3, 589] = "LotyyyyMMddID-004"; pdata[4, 589] = "35.21";
            //pdata[1, 590] = "1"; pdata[2, 590] = "ProductID-012"; pdata[3, 590] = "LotyyyyMMddID-004"; pdata[4, 590] = "9.57";
            //pdata[1, 591] = "1"; pdata[2, 591] = "ProductID-012"; pdata[3, 591] = "LotyyyyMMddID-005"; pdata[4, 591] = "48.11"; //Group-012 - Sub-005
            //pdata[1, 592] = "1"; pdata[2, 592] = "ProductID-012"; pdata[3, 592] = "LotyyyyMMddID-005"; pdata[4, 592] = "13.22";
            //pdata[1, 593] = "1"; pdata[2, 593] = "ProductID-012"; pdata[3, 593] = "LotyyyyMMddID-005"; pdata[4, 593] = "15.01";
            //pdata[1, 594] = "1"; pdata[2, 594] = "ProductID-012"; pdata[3, 594] = "LotyyyyMMddID-005"; pdata[4, 594] = "30.04";
            //pdata[1, 595] = "1"; pdata[2, 595] = "ProductID-012"; pdata[3, 595] = "LotyyyyMMddID-005"; pdata[4, 595] = "79.51";
            //pdata[1, 596] = "1"; pdata[2, 596] = "ProductID-012"; pdata[3, 596] = "LotyyyyMMddID-005"; pdata[4, 596] = "27.98";
            //pdata[1, 597] = "1"; pdata[2, 597] = "ProductID-012"; pdata[3, 597] = "LotyyyyMMddID-005"; pdata[4, 597] = "7.54";
            //pdata[1, 598] = "1"; pdata[2, 598] = "ProductID-012"; pdata[3, 598] = "LotyyyyMMddID-005"; pdata[4, 598] = "1.4";
            //pdata[1, 599] = "1"; pdata[2, 599] = "ProductID-012"; pdata[3, 599] = "LotyyyyMMddID-005"; pdata[4, 599] = "39.23";
            //pdata[1, 600] = "1"; pdata[2, 600] = "ProductID-012"; pdata[3, 600] = "LotyyyyMMddID-005"; pdata[4, 600] = "66.05";


            return pdata;
        }

        /// <summary>
        /// P Test Data
        /// </summary>
        /// <returns></returns>
        public string[,] CTR_P01()
        {
            string[,] pdata = new string[6, 4];
            pdata[1, 01] = "1"; pdata[2, 01] = "CL07"; pdata[3, 01] = "2019-08-01"; pdata[4, 01] = "45"; pdata[5, 01] = "3000";//subgroup 1
            pdata[1, 02] = "1"; pdata[2, 02] = "CL07"; pdata[3, 02] = "2019-08-02"; pdata[4, 02] = "50"; pdata[5, 02] = "5000";
            pdata[1, 03] = "1"; pdata[2, 03] = "CL07"; pdata[3, 03] = "2019-08-03"; pdata[4, 03] = "60"; pdata[5, 03] = "7000";
            return pdata;
        }
        public string[,] CTR_P02()
        {
            string[,] pdata = new string[6, 6];
            pdata[1, 01] = "1"; pdata[2, 01] = "CL06"; pdata[3, 01] = "2019-10-01"; pdata[4, 01] = "245"; pdata[5, 01] = "300";//subgroup 1
            pdata[1, 02] = "1"; pdata[2, 02] = "CL06"; pdata[3, 02] = "2019-10-02"; pdata[4, 02] = "252"; pdata[5, 02] = "500";
            pdata[1, 03] = "1"; pdata[2, 03] = "CL06"; pdata[3, 03] = "2019-10-03"; pdata[4, 03] = "262"; pdata[5, 03] = "700";
            pdata[1, 04] = "1"; pdata[2, 04] = "CL06"; pdata[3, 04] = "2019-10-04"; pdata[4, 04] = "262"; pdata[5, 04] = "600";
            pdata[1, 05] = "1"; pdata[2, 05] = "CL06"; pdata[3, 05] = "2019-10-05"; pdata[4, 05] = "272"; pdata[5, 05] = "700";
            return pdata;
        }

        public string[,] CTR_P04_NP()
        {
            string[,] pdata = new string[6, 6];
            pdata[1, 01] = "1"; pdata[2, 01] = "CL06"; pdata[3, 01] = "2019-10-01"; pdata[4, 01] = "245"; pdata[5, 01] = "560";//subgroup 1
            pdata[1, 02] = "1"; pdata[2, 02] = "CL06"; pdata[3, 02] = "2019-10-02"; pdata[4, 02] = "252"; pdata[5, 02] = "560";
            pdata[1, 03] = "1"; pdata[2, 03] = "CL06"; pdata[3, 03] = "2019-10-03"; pdata[4, 03] = "262"; pdata[5, 03] = "560";
            pdata[1, 04] = "1"; pdata[2, 04] = "CL06"; pdata[3, 04] = "2019-10-04"; pdata[4, 04] = "262"; pdata[5, 04] = "560";
            pdata[1, 05] = "1"; pdata[2, 05] = "CL06"; pdata[3, 05] = "2019-10-05"; pdata[4, 05] = "272"; pdata[5, 05] = "560";
            return pdata;
        }

        public string[,] CTR_P03()
        {
            string[,] pdata = new string[6, 31];
            pdata[1, 01] = "1"; pdata[2, 01] = "CL06"; pdata[3, 01] = "2019-10-01"; pdata[4, 01] = "245"; pdata[5, 01] = "300";//subgroup 1
            pdata[1, 02] = "1"; pdata[2, 02] = "CL06"; pdata[3, 02] = "2019-10-02"; pdata[4, 02] = "252"; pdata[5, 02] = "500";
            pdata[1, 03] = "1"; pdata[2, 03] = "CL06"; pdata[3, 03] = "2019-10-03"; pdata[4, 03] = "262"; pdata[5, 03] = "700";
            pdata[1, 04] = "1"; pdata[2, 04] = "CL06"; pdata[3, 04] = "2019-10-04"; pdata[4, 04] = "262"; pdata[5, 04] = "600";
            pdata[1, 05] = "1"; pdata[2, 05] = "CL06"; pdata[3, 05] = "2019-10-05"; pdata[4, 05] = "272"; pdata[5, 05] = "700";
            pdata[1, 06] = "1"; pdata[2, 06] = "CL07"; pdata[3, 06] = "2019-10-06"; pdata[4, 06] = "282"; pdata[5, 06] = "800";
            pdata[1, 07] = "1"; pdata[2, 07] = "CL07"; pdata[3, 07] = "2019-10-07"; pdata[4, 07] = "222"; pdata[5, 07] = "700";
            pdata[1, 08] = "1"; pdata[2, 08] = "CL07"; pdata[3, 08] = "2019-10-08"; pdata[4, 08] = "232"; pdata[5, 08] = "900";
            pdata[1, 09] = "1"; pdata[2, 09] = "CL07"; pdata[3, 09] = "2019-10-09"; pdata[4, 09] = "142"; pdata[5, 09] = "300";
            pdata[1, 10] = "1"; pdata[2, 10] = "CL07"; pdata[3, 10] = "2019-10-10"; pdata[4, 10] = "188"; pdata[5, 10] = "400";
            pdata[1, 11] = "1"; pdata[2, 11] = "CL07"; pdata[3, 11] = "2019-10-11"; pdata[4, 11] = "198"; pdata[5, 11] = "500";
            pdata[1, 12] = "1"; pdata[2, 12] = "CL07"; pdata[3, 12] = "2019-10-12"; pdata[4, 12] = "188"; pdata[5, 12] = "600";
            pdata[1, 13] = "1"; pdata[2, 13] = "CL07"; pdata[3, 13] = "2019-10-13"; pdata[4, 13] = "198"; pdata[5, 13] = "700";
            pdata[1, 14] = "1"; pdata[2, 14] = "CL07"; pdata[3, 14] = "2019-10-14"; pdata[4, 14] = "118"; pdata[5, 14] = "800";
            pdata[1, 15] = "1"; pdata[2, 15] = "CL07"; pdata[3, 15] = "2019-10-15"; pdata[4, 15] = "138"; pdata[5, 15] = "200";
            pdata[1, 16] = "1"; pdata[2, 16] = "CL07"; pdata[3, 16] = "2019-10-16"; pdata[4, 16] = "138"; pdata[5, 16] = "300";
            pdata[1, 17] = "1"; pdata[2, 17] = "CL07"; pdata[3, 17] = "2019-10-17"; pdata[4, 17] = "147"; pdata[5, 17] = "400";
            pdata[1, 18] = "1"; pdata[2, 18] = "CL07"; pdata[3, 18] = "2019-10-18"; pdata[4, 18] = "157"; pdata[5, 18] = "800";
            pdata[1, 19] = "1"; pdata[2, 19] = "CL07"; pdata[3, 19] = "2019-10-19"; pdata[4, 19] = "137"; pdata[5, 19] = "900";
            pdata[1, 20] = "1"; pdata[2, 20] = "CL07"; pdata[3, 20] = "2019-10-20"; pdata[4, 20] = "177"; pdata[5, 20] = "800";
            pdata[1, 21] = "1"; pdata[2, 21] = "CL07"; pdata[3, 21] = "2019-10-21"; pdata[4, 21] = "137"; pdata[5, 21] = "900";
            pdata[1, 22] = "1"; pdata[2, 22] = "CL07"; pdata[3, 22] = "2019-10-22"; pdata[4, 22] = "147"; pdata[5, 22] = "100";
            pdata[1, 23] = "1"; pdata[2, 23] = "CL07"; pdata[3, 23] = "2019-10-23"; pdata[4, 23] = "127"; pdata[5, 23] = "300";
            pdata[1, 24] = "1"; pdata[2, 24] = "CL07"; pdata[3, 24] = "2019-10-24"; pdata[4, 24] = "137"; pdata[5, 24] = "300";
            pdata[1, 25] = "1"; pdata[2, 25] = "CL07"; pdata[3, 25] = "2019-10-25"; pdata[4, 25] = "127"; pdata[5, 25] = "400";
            pdata[1, 26] = "1"; pdata[2, 26] = "CL07"; pdata[3, 26] = "2019-10-26"; pdata[4, 26] = "157"; pdata[5, 26] = "500";
            pdata[1, 27] = "1"; pdata[2, 27] = "CL07"; pdata[3, 27] = "2019-10-27"; pdata[4, 27] = "127"; pdata[5, 27] = "300";
            pdata[1, 28] = "1"; pdata[2, 28] = "CL07"; pdata[3, 28] = "2019-10-28"; pdata[4, 28] = "167"; pdata[5, 28] = "700";
            pdata[1, 29] = "1"; pdata[2, 29] = "CL07"; pdata[3, 29] = "2019-10-29"; pdata[4, 29] = "183"; pdata[5, 29] = "900";
            pdata[1, 30] = "1"; pdata[2, 30] = "CL07"; pdata[3, 30] = "2019-10-30"; pdata[4, 30] = "193"; pdata[5, 30] = "200";

            return pdata;
        }
    }
}
