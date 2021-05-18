#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons.Controls;
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

namespace Micube.SmartMES.SystemManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 시스템 관리 > 접속이력 관리 > 접속이력 조회
    /// 업  무  설  명  : 시스템 로그인, 화면 사용 이력을 조회한다.
    /// 생    성    자  : 강호윤
    /// 생    성    일  : 2019-05-14
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ConnectionHistory : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가

        #endregion

        #region 생성자

        public ConnectionHistory()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeEvent();

            // TODO : 컨트롤 초기화 로직 구성
            InitializeGrid();
        }

        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdConnectionHistory.GridButtonItem = GridButtonItem.Export;
            grdConnectionHistory.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;
            grdConnectionHistory.View.SetIsReadOnly();
            grdConnectionHistory.View.SetSortOrder("CONNECTIONTIME", DevExpress.Data.ColumnSortOrder.Descending);

            grdConnectionHistory.View.AddTextBoxColumn("TXNHISTKEY", 150)
                .SetIsHidden();
            grdConnectionHistory.View.AddTextBoxColumn("USERID", 90);
            grdConnectionHistory.View.AddTextBoxColumn("USERNAME", 100);
            grdConnectionHistory.View.AddTextBoxColumn("DEPARTMENT", 120);
            grdConnectionHistory.View.AddComboBoxColumn("CONNECTIONTYPE", 70, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ConnectionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetTextAlignment(TextAlignment.Center);
            grdConnectionHistory.View.AddTextBoxColumn("UIID", 70);
            grdConnectionHistory.View.AddTextBoxColumn("MENUID", 150);
            grdConnectionHistory.View.AddTextBoxColumn("MENUNAME", 200);
            grdConnectionHistory.View.AddTextBoxColumn("IPADDRESS", 120);
            grdConnectionHistory.View.AddTextBoxColumn("CONNECTIONTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetTextAlignment(TextAlignment.Center);
            grdConnectionHistory.View.AddTextBoxColumn("DISCONNECTIONTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetTextAlignment(TextAlignment.Center);

            grdConnectionHistory.View.PopulateColumns();
        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            // TODO : 저장 Rule 변경
            //DataTable changed = grdCodeClass.GetChangedRows();

            //ExecuteRule("SaveCodeClass", changed);
        }

        #endregion

        #region 검색

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            // TODO : 조회 SP 변경
            var values = Conditions.GetValues();
            values.Add("P_LANGUAGETYPE", UserInfo.Current.LanguageType);

            //DataTable dtConnectionHistory = await ProcedureAsync("usp_com_selectConnectionHistory", values);
            DataTable dtConnectionHistory = await QueryAsync("SelectConnectionHistory", "10001", values);

            if (dtConnectionHistory.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdConnectionHistory.DataSource = dtConnectionHistory;
        }

        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
        }

        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
        }

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            // TODO : 유효성 로직 변경
            //grdCodeClass.View.CheckValidation();

            //DataTable changed = grdCodeClass.GetChangedRows();

            //if (changed.Rows.Count == 0)
            //{
            //    // 저장할 데이터가 존재하지 않습니다.
            //    throw MessageException.Create("NoSaveData");
            //}
        }

        #endregion

        #region Private Function

        // TODO : 화면에서 사용할 내부 함수 추가

        #endregion
    }
}