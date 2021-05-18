using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Micube.SmartMES.QualityAnalysis.Helper
{
    /// <summary>
    /// 프 로 그 램 명  : 이미지 관련 함수 정의
    /// 업  무  설  명  : 이미지 관련 함수 정의
    /// 생    성    자  : 강유라
    /// 생    성    일  : 2020-01-28
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    class QcmImageHelper
    {

        /// <summary>
        /// 파일을 업로드할 데이터 테이블
        /// </summary>
        /// <returns></returns>
        public static DataTable GetImageFileTable()
        {
            DataTable fileTable = new DataTable("");

            fileTable.Columns.Add("FILEID");
            fileTable.Columns.Add("FILENAME");
            fileTable.Columns.Add("FILEEXT");
            fileTable.Columns.Add("FILEPATH");
            fileTable.Columns.Add("SAFEFILENAME");
            fileTable.Columns.Add("FILESIZE");
            fileTable.Columns.Add("SEQUENCE");
            fileTable.Columns.Add("LOCALFILEPATH");
            fileTable.Columns.Add("RESOURCETYPE");
            fileTable.Columns.Add("RESOURCEID");
            fileTable.Columns.Add("RESOURCEVERSION");
            fileTable.Columns.Add("PROCESSINGSTATUS");
     
            return fileTable;
        }

        /// <summary>
        /// 이메일에 첨부할 파일 데이터를 담을 테이블
        /// </summary>
        /// <returns></returns>
        public static DataTable GetImageFileTableToSendEmail()
        {
            DataTable fileTable = new DataTable("");

            fileTable.Columns.Add("FILEDATA", typeof(byte[]));
            fileTable.Columns.Add("FILENAME", typeof(string));
 
            return fileTable;
        }

        /// <summary>
        /// filePath로 이미지 미리보기
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static Image GetImageFromFile(string filePath)
        {
            return Image.FromFile(filePath);
        }
    }
}
