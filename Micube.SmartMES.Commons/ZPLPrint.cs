#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Sockets;

#endregion

namespace Micube.SmartMES.Commons
{
    public class ZPLPrint
    {
        const int PRINTER_ACCESS_USE = 0x00000008;
        const int PRINTER_ACCESS_ADMINISTER = 0x00000004;

        #region DLLImport

        [DllImport("winspool.Drv", EntryPoint = "OpenPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)] string szPrinter, out IntPtr hPrinter, IntPtr pd);

        [DllImport("winspool.Drv", EntryPoint = "ClosePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool ClosePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartDocPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartDocPrinter(IntPtr hPrinter, Int32 level, [In, MarshalAs(UnmanagedType.LPStruct)] DOCINFOA di);

        [DllImport("winspool.Drv", EntryPoint = "EndDocPrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndDocPrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "EndPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "WritePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, Int32 dwCount, out Int32 dwWritten);

        #endregion

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public class DOCINFOA
        {
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDocName;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pOutputFile;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDataType;

            [MarshalAs(UnmanagedType.LPTStr)]
            public String pDatatype;
            public IntPtr pDevMode;
            [MarshalAs(UnmanagedType.I4)]
            public int DesiredAccess;
        }

        public static bool SendStringToPrinter(string szString)
        {
            bool result = true;

            //string szPrinterName = "ZDesigner ZM400 300 dpi (ZPL)";
            if (UserInfo.Current.PrinterType == "IP")
            {
                result = PrintToTCP(szString);
            }
            else
            {
                string szPrinterName = UserInfo.Current.Printer;

                IntPtr pBytes;
                Int32 dwCount;
                // How many characters are in the string?
                dwCount = (szString.Length + 1) * System.Runtime.InteropServices.Marshal.SystemMaxDBCSCharSize;//szString.Length;
                                                                                                               // Assume that the printer is expecting ANSI text, and then convert
                                                                                                               // the string to ANSI text.
                pBytes = System.Runtime.InteropServices.Marshal.StringToCoTaskMemAnsi(szString);
                // Send the converted ANSI string to the printer.            
                result = SendBytesToPrinter(szPrinterName, pBytes, dwCount);
                System.Runtime.InteropServices.Marshal.FreeCoTaskMem(pBytes);
            }

            return result;
        }

        private static bool SendBytesToPrinter(string szPrinterName, IntPtr pBytes, Int32 dwCount)
        {
            Int32 dwError = 0, dwWritten = 0;
            IntPtr hPrinter = new IntPtr(0);
            DOCINFOA di = new DOCINFOA();
            di.DesiredAccess = PRINTER_ACCESS_ADMINISTER | PRINTER_ACCESS_USE;
            bool bSuccess = false; // Assume failure unless you specifically succeed.

            di.pDocName = "My C#.NET RAW Document";
            di.pDataType = "RAW";
            DOCINFOA defaults = new DOCINFOA();
            // Open the printer.
            if (OpenPrinter(szPrinterName.Normalize(), out hPrinter, IntPtr.Zero))
            {
                // Start a document.
                if (StartDocPrinter(hPrinter, 1, di))
                {
                    // Start a page.
                    if (StartPagePrinter(hPrinter))
                    {
                        // Write your bytes.
                        bSuccess = WritePrinter(hPrinter, pBytes, dwCount, out dwWritten);
                        EndPagePrinter(hPrinter);
                    }
                    EndDocPrinter(hPrinter);
                }
                ClosePrinter(hPrinter);
            }
            // If you did not succeed, GetLastError may give more information
            // about why not.
            if (bSuccess == false)
            {
                dwError = Marshal.GetLastWin32Error();
            }
            return bSuccess;
        }


        public static string GetZPLIIImage(Bitmap srcbitmap, int posx, int posy)
        {

            Rectangle dim = new Rectangle(Point.Empty, srcbitmap.Size);
            int rowdata = ((dim.Width + 7) / 8);
            int bytes = rowdata * dim.Height;

            using (Bitmap bmpCompressed = srcbitmap.Clone(dim, PixelFormat.Format1bppIndexed))
            {
                StringBuilder result = new StringBuilder();

                result.AppendFormat("^FO{0},{1}^GFA,{2},{2},{3},", posx, posy, rowdata * dim.Height, rowdata);
                byte[][] imageData = ConvertImageBinary(dim, rowdata, bmpCompressed);

                byte[] previousRow = null;
                foreach (byte[] row in imageData)
                {
                    AppendLine(row, previousRow, result);
                    previousRow = row;
                }
                result.Append(@"^FS");

                return result.ToString();
            }
        }

        /// <summary>
        /// Converts the image into a byte array (pointer) and converts the image byte-by-byte while inverting the color of the image color for printing.
        /// </summary>
        /// <param name="dim"></param>
        /// <param name="stride"></param>
        /// <param name="bmpimage"></param>
        /// <returns></returns>
        private static byte[][] ConvertImageBinary(Rectangle dim, int stride, Bitmap bmpimage)
        {
            byte[][] imagebytes;
            var data = bmpimage.LockBits(dim, ImageLockMode.ReadOnly, PixelFormat.Format1bppIndexed);
            try
            {
                // This is required to perform operations with pointers. This is only working with a locked bitmap in memory so it is "safe".
               unsafe
               {
                    byte* pixelData = (byte*)data.Scan0.ToPointer();
                    byte mask = (byte)(0xff << (data.Stride * 8 - dim.Width));
                    imagebytes = new byte[dim.Height][];

                    for (int x = 0; x < dim.Height; x++)
                    {
                        byte* rowStart = pixelData + x * data.Stride;
                        imagebytes[x] = new byte[stride];

                        for (int y = 0; y < stride; y++)
                        {
                            byte invert = (byte)(0xff ^ rowStart[y]);
                            invert = (y == stride - 1) ? (byte)(invert & mask) : invert;
                            imagebytes[x][y] = invert;
                        }
                    }
                }
            }
            finally
            {
                bmpimage.UnlockBits(data);
            }
            return imagebytes;
        }

        /// <summary>
        /// Converts byte to ZB64 and appends to current string.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="previousRow"></param>
        /// <param name="zb64stream"></param>
        private static void AppendLine(byte[] row, byte[] previousRow, StringBuilder zb64stream)
        {
            if (row.All(r => r == 0))
            {
                zb64stream.Append(",");
                return;
            }

            if (row.All(r => r == 0xff))
            {
                zb64stream.Append("!");
                return;
            }

            if (previousRow != null && MatchByteArray(row, previousRow))
            {
                zb64stream.Append(":");
                return;
            }

            byte[] nibbles = new byte[row.Length * 2];
            for (int i = 0; i < row.Length; i++)
            {
                nibbles[i * 2] = (byte)(row[i] >> 4);
                nibbles[i * 2 + 1] = (byte)(row[i] & 0x0f);
            }

            for (int i = 0; i < nibbles.Length; i++)
            {
                byte pixel = nibbles[i];

                int repcount = 0;
                for (int j = i; j < nibbles.Length && repcount <= 400; j++)
                {
                    if (pixel == nibbles[j])
                    {
                        repcount++;
                    }
                    else
                    {
                        break;
                    }
                }

                if (repcount > 2)
                {
                    if (repcount == nibbles.Length - i
                        && (pixel == 0 || pixel == 0xf))
                    {
                        if (pixel == 0)
                        {
                            if (i % 2 == 1)
                            {
                                zb64stream.Append("0");
                            }
                            zb64stream.Append(",");
                            return;
                        }
                        else if (pixel == 0xf)
                        {
                            if (i % 2 == 1)
                            {
                                zb64stream.Append("F");
                            }
                            zb64stream.Append("!");
                            return;
                        }
                    }
                    else
                    {
                        zb64stream.Append(ConvertZB64(repcount));
                        i += repcount - 1;
                    }
                }
                zb64stream.Append(pixel.ToString("X"));
            }
        }

        /// <summary>
        /// Converts to ZB64 format specified in the ZPLII Programming document.
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        private static string ConvertZB64(int count)
        {
            if (count > 419)
                throw new ArgumentOutOfRangeException();

            int high = count / 20;
            int low = count % 20;

            const string lowString = " GHIJKLMNOPQRSTUVWXY";
            const string highString = " ghijklmnopqrstuvwxyz";

            string repeatSequence = "";
            if (high > 0)
            {
                repeatSequence += highString[high];
            }
            if (low > 0)
            {
                repeatSequence += lowString[low];
            }

            return repeatSequence;
        }

        private static bool MatchByteArray(byte[] row, byte[] lastrow)
        {
            for (int i = 0; i < row.Length; i++)
            {
                if (row[i] != lastrow[i])
                {
                    return false;
                }
            }
            return true;
        }

        #region TCP통신으로 Print하기위한 함수 모음

        public static bool PrintToTCP(string szString)
        {
            TcpClient client = new TcpClient();

            string[] printerInfo = UserInfo.Current.Printer.Split(':');

            try
            {
                client.Connect(printerInfo[0], Format.GetInteger(printerInfo[1]));
            }
            catch (Exception ex)
            {
                MSGBox.Show(MessageBoxType.Error, "ERROR", "Connention is Down. Pleas check pirnter");
                return false;
            }

            StreamWriter writer = new System.IO.StreamWriter(client.GetStream());

            if (!client.Connected)
            {
                MSGBox.Show(MessageBoxType.Error, "ERROR", "Connention is Down. Pleas check pirnter");
                CloseConnetion(client, writer);
                return false;
            }

            //List<string> errorList = IsRun(client, client.GetStream(), writer);
            //if (errorList.Count > 0)
            //{
            //    string errorstring = "";
            //    foreach (string item in errorList)
            //    {
            //        errorstring += "\r\n" + item;
            //    }
            //    MSGBox.Show(MessageBoxType.Error, "ERROR", "Printer Errer. Pleas check pirnter {0}", errorstring);
            //    CloseConnetion(client, writer);
            //    return false;
            //}

            writer.Write(szString);
            writer.Flush();

            CloseConnetion(client, writer);

            return true;
        }

        public bool PrintToTCP(ref List<Dictionary<string, object>> printScriptList)
        {
            bool isCompliet = false;

            // Open connection
            TcpClient client = new TcpClient();

            try
            {
                string[] printerInfo = UserInfo.Current.Printer.Split(':');

                client.Connect(printerInfo[0], Format.GetInteger(printerInfo[1]));
            }
            catch (Exception ex)
            {
                MSGBox.Show(MessageBoxType.Error, "ERROR", "Connention is Down. Pleas check pirnter");
                return isCompliet;
            }

            // Write ZPL String to connection
            StreamWriter writer = new System.IO.StreamWriter(client.GetStream());
            LingerOption tempData = client.LingerState;

            List<string> statuses = new List<string>();

            foreach (Dictionary<string, object> printScriptData in printScriptList)
            {
                if (!client.Connected)
                {
                    MSGBox.Show(MessageBoxType.Error, "ERROR", "Connention is Down. Pleas check pirnter");
                    CloseConnetion(client, writer);
                    return isCompliet;
                }
                List<string> errorList = IsRun(client, client.GetStream(), writer);
                if (errorList.Count > 0)
                {
                    string errorstring = "";
                    foreach (string item in errorList)
                    {
                        errorstring += "\r\n" + item;
                    }
                    MSGBox.Show(MessageBoxType.Error, "ERROR", "Printer Errer. Pleas check pirnter {0}", errorstring);
                    CloseConnetion(client, writer);
                    return isCompliet;
                }
                writer.Write(printScriptData["LABELSCRIPT"].ToString());
                writer.Flush();
                printScriptData["ISPRINT"] = true;
            }
            CloseConnetion(client, writer);
            isCompliet = true;

            return isCompliet;
        }

        private static void CloseConnetion(TcpClient client, StreamWriter writer)
        {
            // Close Connection
            writer.Close();
            client.Close();
        }

        //private void SendZplOverTcp(string label)
        //{
        //    // Instantiate connection for ZPL TCP port at given address
        //    Connection thePrinterConn = new TcpConnection(PrintInfo.PrintIP, PrintInfo.PrintPort);

        //    try
        //    {
        //        // Open the connection - physical connection is established here.
        //        thePrinterConn.Open();
        //        if (thePrinterConn.Connected)
        //        {
        //            MSGBox.Show(MessageBoxType.Error, "ERROR", "Printer is Down");
        //        }

        //        // This example prints "This is a ZPL test." near the top of the label.
        //        string zplData = label;

        //        // Send the data to printer as a byte array.
        //        thePrinterConn.Write(Encoding.UTF8.GetBytes(zplData));
        //    }
        //    catch (ConnectionException e)
        //    {
        //        // Handle communications error here.
        //        Console.WriteLine(e.ToString());
        //    }
        //    finally
        //    {
        //        // Close the connection to release resources.
        //        thePrinterConn.Close();
        //    }
        //}    

        public static List<string> IsRun(TcpClient clinet, NetworkStream stream, StreamWriter sw)
        {
            // create tcp client and connect server on port 9100
            TcpClient client = clinet;
            List<string> statuses = new List<string>();

            // create tcp stream
            byte[] buffer = new byte[client.ReceiveBufferSize];

            // send Host Status Return request
            sw.Write("~HQES");
            sw.Flush();

            // read response, Sometimes it may not pull in the entire
            //    response, so keep trying till you get everything ~144 bytes
            StreamReader sr = new StreamReader(stream);
            int length = 0;
            while ((stream.DataAvailable) || (length < 140))
            {
                length += stream.Read(buffer, length, (buffer.Length - length));
            }

            // parse results
            string results = (Encoding.UTF8.GetString(buffer, 0, length));
            GetAll(results, out statuses);
            return statuses;
        }

        public static bool GetAll(string message, out List<string> status)
        {
            // initially, no errors
            status = new List<string>();
            bool ok = false;

            try
            {
                // get the flags for different status conditions
                int is_error;
                int media;
                int head;
                int pause;
                string CH = "印表機狀態";
                string CH_SIMPLE = "打印机状态";
                string EN = "PRINTER STATUS";
                string KR = "프린터 상태";
                if (message.Substring(7, 5).Equals(CH_SIMPLE))
                {
                    //중국버전_간체
                    is_error = Convert.ToInt32(message.Substring(56, 1));
                    media = Convert.ToInt32(message.Substring(74, 1));
                    head = Convert.ToInt32(message.Substring(73, 1));
                    pause = Convert.ToInt32(message.Substring(70, 1));
                }
                else if (message.Substring(6, 6).Equals(CH))
                {
                    //중국버전_번체
                    is_error = Convert.ToInt32(message.Substring(56, 1));
                    media = Convert.ToInt32(message.Substring(74, 1));
                    head = Convert.ToInt32(message.Substring(73, 1));
                    pause = Convert.ToInt32(message.Substring(70, 1));
                }
                else if (message.Substring(7, 14).Equals(EN))
                { // 영어버전
                    is_error = Convert.ToInt32(message.Substring(70, 1));
                    media = Convert.ToInt32(message.Substring(88, 1));
                    head = Convert.ToInt32(message.Substring(87, 1));
                    pause = Convert.ToInt32(message.Substring(84, 1));
                }
                else if (message.Substring(7, 6).Equals(KR))
                { // 한글버전
                    is_error = Convert.ToInt32(message.Substring(56, 1));
                    media = Convert.ToInt32(message.Substring(74, 1));
                    head = Convert.ToInt32(message.Substring(73, 1));
                    pause = Convert.ToInt32(message.Substring(70, 1));
                }
                else
                {
                    status.Add("Unexpected response from printer, unable to get status");
                    return ok;
                }

                // check each flag that prevents printing
                if (is_error == 0)
                {
                    ok = true;
                    //status.Add("Ready to Print");
                }
                //에러 코드에 따른 다국어 등록 필요. LanguageManager.LanguageProvider.GetDictionary("");
                if (media == 1)
                    status.Add("Error: Paper out");
                if (media == 2)
                    status.Add("Error: Ribbon Out");
                if (media == 4)
                    status.Add("Error: Media Door Open");
                if (media == 8)
                    status.Add("Error: Cutter Fault");
                if (head == 1)
                    status.Add("Error: Printhead Overheating");
                if (head == 2)
                    status.Add("Error: Motor Overheating");
                if (head == 4)
                    status.Add("Error: Printheat Fault");
                if (head == 8)
                    status.Add("Error: Incorrect Printhead");
                if (pause == 1)
                    status.Add("Printer Paused");
                if ((!ok))
                    status.Add("Error: Unknown Error");
            }
            catch (FormatException)
            {
                status.Add("Unexpected response from printer, unable to get status");
                status.Add(message);
            }
            return ok;
        }
        #endregion
    }
}
