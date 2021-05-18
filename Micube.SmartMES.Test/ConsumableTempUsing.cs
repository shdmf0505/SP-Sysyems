#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;

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

namespace Micube.SmartMES.Test
{
    /// <summary>
    /// 프 로 그 램 명  : ex> 시스템관리 > 코드 관리 > 코드그룹 정보
    /// 업  무  설  명  : ex> 시스템에서 공통으로 사용되는 코드그룹 정보를 관리한다.
    /// 생    성    자  : 홍길동
    /// 생    성    일  : 2019-05-14
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ConsumableTempUsing : SmartConditionBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가

        #endregion

        #region 생성자

        public ConsumableTempUsing()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 화면의 컨텐츠 영역을 초기화한다.
        /// </summary>
        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeEvent();

            // TODO : 컨트롤 초기화 로직 구성
            InitializeGrid();
        }

        /// <summary>        
        /// 코드그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdLotList.GridButtonItem = GridButtonItem.None;
            grdLotList.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            grdLotList.View.SetIsReadOnly();

            grdLotList.View.AddTextBoxColumn("LOTID", 180);
            grdLotList.View.AddTextBoxColumn("PROCESSSEGMENTID", 120);
            grdLotList.View.AddTextBoxColumn("ENTERPRISEID", 100);
            grdLotList.View.AddTextBoxColumn("PLANTID", 80);

            grdLotList.View.PopulateColumns();
        }

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            
        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            int len = Math.Min(500, grdLotList.View.RowCount);

            // TODO : 저장 Rule 변경
            for (int i = 0; i < len; i++)
            {
                DataRow row = grdLotList.View.GetDataRow(i);

                string lotId = Format.GetString(row["LOTID"]);
                string processSegmentId = Format.GetString(row["PROCESSSEGMENTID"]);
                string enterpriseId = Format.GetString(row["ENTERPRISEID"]);
                string plantId = Format.GetString(row["PLANTID"]);

                MessageWorker worker = new MessageWorker("SaveConsumableTempUsing");
                worker.SetBody(new MessageBody()
            {
                { "LotId", lotId },
                { "ProcessSegmentId", processSegmentId },
                { "EnterpriseId", enterpriseId },
                { "PlantId", plantId }
            });

                worker.Execute();
            }
        }

        #endregion

        #region 검색

        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            // TODO : 조회 SP 변경
            DataTable dtList = await QueryAsync("SelectLotIdAndProcessSegmentIdList", "99999");

            grdLotList.DataSource = dtList;
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
        }

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            // TODO : 유효성 로직 변경
        }

        #endregion

        #region Private Function

        #endregion
    }
}