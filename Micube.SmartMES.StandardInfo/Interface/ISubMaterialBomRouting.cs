#region using

using System.Data;
using System.Threading.Tasks;

#endregion using

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프 로 그 램 명  : 기준정보 > 품목관리 > 부자재 routing BOM/표준 약품 부자재
    /// 업  무  설  명  : 공통 함수를 사용하기 위한 인터페이스
    /// 생    성    자  : 전우성
    /// 생    성    일  : 2021-01-04
    /// 필  수  처  리  :
    ///
    /// </summary>
    public abstract class ISubMaterialBomRouting : Framework.SmartControls.Forms.SmartUserControl
    {
        #region abstract

        /// <summary>
        /// 인터페이스 화면 조회
        /// </summary>
        /// <returns></returns>
        public abstract Task Search();

        /// <summary>
        /// Data 전달
        /// </summary>
        /// <returns>DataTable</returns>
        public abstract DataSet GetData();

        /// <summary>
        /// 가져오기, 복사 등 기능 발생시 이벤트
        /// </summary>
        /// <returns>Task</returns>
        public abstract void SetDateAsync();

        /// <summary>
        /// 변경된 내용이 있는지 체크
        /// </summary>
        /// <returns></returns>
        public abstract void Validation();

        #endregion abstract

        #region public function

        /// <summary>
        /// 부자재 키 속성 컬럼 생성
        /// </summary>
        /// <returns></returns>
        public DataTable SetDataTablePrimaryKey()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("_STATE_", typeof(string));
            dt.Columns.Add("MATERIALTYPE", typeof(string));
            dt.Columns.Add("PROCESSSEGMENTCLASSID", typeof(string));
            dt.Columns.Add("PROCESSSEGMENTID", typeof(string));
            dt.Columns.Add("PRODUCTDEFID", typeof(string));
            dt.Columns.Add("PRODUCTDEFVERSION", typeof(string));
            dt.Columns.Add("EQUIPMENTID", typeof(string));
            dt.Columns.Add("MATERIALID", typeof(string));
            dt.Columns.Add("ENTERPRISEID", typeof(string));
            dt.Columns.Add("PLANTID", typeof(string));

            return dt;
        }

        /// <summary>
        /// 부자재 타입이 약품, D/F인 경우
        /// </summary>
        /// <returns></returns>
        public DataTable SetDataTableChemical()
        {
            DataTable dt = SetDataTablePrimaryKey();
            dt.TableName = "info";

            dt.Columns.Add("ROOT_ASSEMBLYITEMID", typeof(string));
            dt.Columns.Add("ROOT_ASSEMBLYITEMVERSION", typeof(string));
            dt.Columns.Add("WORKPLANE", typeof(string));
            dt.Columns.Add("PNLAREA", typeof(double));
            dt.Columns.Add("AREACS", typeof(double));
            dt.Columns.Add("AREASS", typeof(double));

            return dt;
        }

        /// <summary>
        /// 부자재 타입이 H/P 인 경우
        /// </summary>
        /// <returns></returns>
        public DataTable SetDataTableHP()
        {
            DataTable dt = SetDataTablePrimaryKey();
            dt.TableName = "info";

            dt.Columns.Add("ROOT_ASSEMBLYITEMID", typeof(string));
            dt.Columns.Add("ROOT_ASSEMBLYITEMVERSION", typeof(string));
            dt.Columns.Add("RACKSIZE", typeof(string));
            dt.Columns.Add("TIMEMINUTE", typeof(double));
            dt.Columns.Add("PRESS", typeof(double));
            dt.Columns.Add("TEMPC", typeof(double));
            dt.Columns.Add("COOLINGTEMPC", typeof(double));
            dt.Columns.Add("GAONTIMEMINUTE", typeof(double));
            dt.Columns.Add("GAMONTIMEMINUTE", typeof(double));
            dt.Columns.Add("STACK", typeof(double));
            dt.Columns.Add("SINGLE", typeof(double));
            dt.Columns.Add("COL", typeof(double));
            dt.Columns.Add("BOX", typeof(string));
            dt.Columns.Add("BAKING", typeof(string));

            return dt;
        }

        #endregion public function
    }
}