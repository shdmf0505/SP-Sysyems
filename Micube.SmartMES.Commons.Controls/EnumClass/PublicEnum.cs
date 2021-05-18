using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Micube.SmartMES.Commons.Controls
{
    /// <summary>
    /// 다운로드 구분
    /// </summary>
    public enum DownloadType
    {
        // 단일 다운로드 (리스트 더블클릭 시 사용)
        Single = 0,
        // 복수 다운로드 (다운로드 버튼 클릭 시 사용)
        Multi = 1
    }

    /// <summary>
    /// 업로드/다운로드 구분
    /// </summary>
    public enum UpDownType
    {
        // 업로드
        Upload = 0,
        // 다운로드
        Download = 1
    }
}