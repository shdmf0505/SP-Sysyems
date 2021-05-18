#region Using
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO.Compression;
using DevExpress.XtraPrinting;
using DevExpress.Spreadsheet;
using DevExpress.XtraSpreadsheet;
using Micube.Framework.SmartControls;
#endregion

namespace Micube.SmartMES.Commons
{

	public class GetExcelDocument
    {
		#region 생성자
		public GetExcelDocument()
        { }
		#endregion

		#region Public Function
		public static string GetXmlString(DataTable dtExcelDoc)
		{
			string xmlString = "";

			DataRow drExcelDoc = dtExcelDoc.Rows[0];
			byte[] fileData = drExcelDoc["filedata"] as byte[];

			MemoryStream memoryStream = new MemoryStream();
			memoryStream.Write(fileData, 0, fileData.Length);

			using (ZipArchive archive = new ZipArchive(memoryStream, ZipArchiveMode.Update, true))
			{
				//sharedStrings
				ZipArchiveEntry zipArchiveEntry = archive.GetEntry("xl/sharedStrings.xml");
				if (zipArchiveEntry == null)
				{
					return null;
				}

				using (StreamReader streamReader = new StreamReader(zipArchiveEntry.Open()))
				{
					xmlString = streamReader.ReadToEnd();
				}
			}

			return xmlString;
		}

		/// <summary>
		/// Excel 양식을 저장하고 프린트
		/// </summary>
		/// <param name="dtExcelDoc"></param>
		/// <param name="formatId"></param>
		/// <param name="param"></param>
		/// <param name="saveFilePath">파일 이름 포함, 확장자를 제외한 파일경로</param>
		public static void PrintExcelDocument(DataTable dtExcelDoc, string xml, string saveFilePath, bool isPreview = true)
		{
			MemoryStream memoryStream = ConvertXml(dtExcelDoc, xml);
			if (memoryStream == null)
			{
				MSGBox.Show(MessageBoxType.Warning, "경고", "변환 xml 대상이 없습니다.");
				return;
			}

			if (!String.IsNullOrEmpty(saveFilePath))
			{
				saveFilePath += ".xlsx";
				using (FileStream fileStream = new FileStream(saveFilePath, FileMode.Create, FileAccess.Write))
				{
					memoryStream.Seek(0, SeekOrigin.Begin);
					memoryStream.CopyTo(fileStream);
				}
			}

			ExecutePrint(memoryStream, isPreview);
			memoryStream.Dispose();
		}

		/// <summary>
		/// Excel 양식 프린트
		/// </summary>
		/// <param name="dtExcelDoc"></param>
		/// <param name="formatId"></param>
		/// <param name="param"></param>
		public static void PrintExcelDocument(DataTable dtExcelDoc, string xml, bool isPreview = true)
        {
			//파일 변환
			MemoryStream memoryStream = ConvertXml(dtExcelDoc, xml);
			if (memoryStream == null)
			{
				MSGBox.Show(MessageBoxType.Warning, "경고", "변환 xml 대상이 없습니다.");
				return;
			}

			ExecutePrint(memoryStream, isPreview);
			memoryStream.Dispose();

		}
		#endregion

		#region Private Function
		/// <summary>
		/// XML로 변환
		/// </summary>
		/// <param name="dtExcelDoc"></param>
		/// <param name="xmlString"></param>
		/// <returns></returns>
		private static MemoryStream ConvertXml(DataTable dtExcelDoc, string xmlString)
		{
			DataRow drExcelDoc = dtExcelDoc.Rows[0];
			byte[] fileData = drExcelDoc["filedata"] as byte[];

			MemoryStream memoryStream = new MemoryStream();
			memoryStream.Write(fileData, 0, fileData.Length);

			using (ZipArchive archive = new ZipArchive(memoryStream, ZipArchiveMode.Update, true))
			{
				ZipArchiveEntry zipArchiveEntry = archive.GetEntry("xl/sharedStrings.xml");
				if (zipArchiveEntry == null)
				{
					return null;
				}

				zipArchiveEntry.Delete();

				ZipArchiveEntry newZipArchiveEntry = archive.CreateEntry("xl/sharedStrings.xml");

				using (StreamWriter streamWriter = new StreamWriter(newZipArchiveEntry.Open()))
				{
					streamWriter.Write(xmlString);
				}

			}//using
			return memoryStream;
		}

		/// <summary>
		/// 프린트 실행
		/// </summary>
		/// <param name="stream"></param>
		/// <param name="isPreview"></param>
		private static void ExecutePrint(MemoryStream stream, bool isPreview)
		{
			PrintableComponentLink componentLink = new PrintableComponentLink(new PrintingSystem());
			SpreadsheetControl spreadsheetControl = new SpreadsheetControl();
			stream.Seek(0, SeekOrigin.Begin);
			spreadsheetControl.LoadDocument(stream, DocumentFormat.Xlsx);

			componentLink.Component = spreadsheetControl;
			componentLink.CreateDocument();

			PrintTool pt = new PrintTool(componentLink.PrintingSystemBase);

			if(isPreview)
			{
				pt.ShowPreviewDialog(); 
			}
			else
			{
				pt.Print();
			}
		}
		#endregion



	}
}
