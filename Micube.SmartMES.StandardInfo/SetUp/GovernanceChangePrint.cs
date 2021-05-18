#region using

using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Validations;
using System;
using Micube.SmartMES.Commons.Controls;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프 로 그 램 명  : 기준정보 > 사양변경관리 > 사양변경의뢰서 발행
    /// 업  무  설  명  : 사양변경의뢰서를 출력한다
    /// 생    성    자  : 한주석
    /// 생    성    일  : 2020-01-09
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class GovernanceChangePrint : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가
        string _Text;                                       // 설명
        SmartBandedGrid grdList = new SmartBandedGrid();    // Main Grid

        #endregion

        #region 생성자

        public GovernanceChangePrint()
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
            grdrequest.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Restore;
            grdrequest.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdrequest.View.SetIsReadOnly();

            // 의뢰번호
            grdrequest.View.AddTextBoxColumn("REQUESTNO", 100)
                .SetTextAlignment(TextAlignment.Center);
            // 의뢰일자
            grdrequest.View.AddTextBoxColumn("REQUESTDATE", 250)
                .SetTextAlignment(TextAlignment.Center);
            // 모델코드
            grdrequest.View.AddTextBoxColumn("MODELCODE", 150);
            // 모델버전
            grdrequest.View.AddTextBoxColumn("MODELVERSION", 70)
                .SetTextAlignment(TextAlignment.Center);
            // 모델코드
            grdrequest.View.AddTextBoxColumn("MODELNAME", 160);
            // 변경후 모델코드
            grdrequest.View.AddTextBoxColumn("AFTERMODELID")
                .SetIsHidden();
            // 변경후 모델REV
            grdrequest.View.AddTextBoxColumn("AFTERMODELREV")
                .SetIsHidden();
            // 변경후 모델명
            grdrequest.View.AddTextBoxColumn("AFTERMODELNAME")
                .SetIsHidden();
            // 업체담당자
            grdrequest.View.AddTextBoxColumn("COMPANYUSER")
                .SetIsHidden();
            // 의뢰자
            grdrequest.View.AddTextBoxColumn("REQUESTUSER")
                .SetIsHidden();
            // TEL
            grdrequest.View.AddTextBoxColumn("TELNUMBER")
                .SetIsHidden();
            // 기타
            grdrequest.View.AddTextBoxColumn("ETCSPEC")
                .SetIsHidden();
            // 수정전
            grdrequest.View.AddTextBoxColumn("MODIFIBEFORE")
                .SetIsHidden();
            // 수정후
            grdrequest.View.AddTextBoxColumn("MODIFIAFTER")
                .SetIsHidden();
            // 처리결과
            grdrequest.View.AddTextBoxColumn("RESULTS")
                .SetIsHidden();

            grdrequest.View.PopulateColumns();
        }

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            grdList.View.AddingNewRow += View_AddingNewRow;
        }

        /// <summary>
        /// 코드그룹 리스트 그리드에서 추가 버튼 클릭 시 호출
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 툴바

        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            SmartButton btn = sender as SmartButton;

            if (btn.Name.ToString().Equals("Print"))
            {

                if (grdrequest.View.GetFocusedDataRow() == null)
                    return;

                DataSet dsReport = new DataSet();
                DataTable requestdata = grdrequest.View.GetCheckedRows();

                


                #region 라벨

                requestdata.Columns.Add(new DataColumn("LBLNOTICE", typeof(string))); // 사양변경 통보서
                requestdata.Columns.Add(new DataColumn("LBLREQUESTTEAM", typeof(string))); // 의뢰부서
                requestdata.Columns.Add(new DataColumn("LBLREQUESTER", typeof(string))); // 의뢰자
                requestdata.Columns.Add(new DataColumn("LBLREQUESTNO", typeof(string))); // 의뢰 NO
                requestdata.Columns.Add(new DataColumn("LBLREQUESTDATE", typeof(string))); // 의뢰일자
                requestdata.Columns.Add(new DataColumn("LBLMODELCODE", typeof(string))); // 모델코드
                requestdata.Columns.Add(new DataColumn("LBLMODELVERSION", typeof(string))); // 모델REV
                requestdata.Columns.Add(new DataColumn("LBLMODELNAME", typeof(string))); // 모델명
                requestdata.Columns.Add(new DataColumn("LBLMODIFYCOMMENT", typeof(string))); // 수정내용
                requestdata.Columns.Add(new DataColumn("LBLCOMPANYUSER", typeof(string))); // 업체담당자
                requestdata.Columns.Add(new DataColumn("LBLMODIFIBEFORE", typeof(string))); // 수정전
                requestdata.Columns.Add(new DataColumn("LBLMODIFIAFTER", typeof(string))); // 수정후
                requestdata.Columns.Add(new DataColumn("LBLETCSPEC", typeof(string))); // 기타
                requestdata.Columns.Add(new DataColumn("LBLAFTERMODELID", typeof(string))); // 변경후 모델코드
                requestdata.Columns.Add(new DataColumn("LBLAFTERMODELREV", typeof(string))); // 변경후 모델REV
                requestdata.Columns.Add(new DataColumn("LBLAFTERMODELNAME", typeof(string))); // 변경후 모델명
                requestdata.Columns.Add(new DataColumn("LBLRESULTS", typeof(string))); // 처리결과
                requestdata.Columns.Add(new DataColumn("LBLMANUFACTURING", typeof(string))); // 제조
                requestdata.Columns.Add(new DataColumn("LBLMANUFACTURINGMNG", typeof(string))); // 제조관리
                requestdata.Columns.Add(new DataColumn("LBLQUALITYMNG", typeof(string))); // 품질관리
                requestdata.Columns.Add(new DataColumn("LBLOUTSOURCINGMNG", typeof(string))); // 외주관리
                requestdata.Columns.Add(new DataColumn("LBLDOMESTICSALES", typeof(string))); // 국내영업
                requestdata.Columns.Add(new DataColumn("LBLOVERSEASSALES", typeof(string))); // 해외영업
                requestdata.Columns.Add(new DataColumn("LBLPURCHASE", typeof(string))); // 구매
                requestdata.Columns.Add(new DataColumn("LBLMANUFACTURINGSKILL", typeof(string))); // 제조기술
                requestdata.Columns.Add(new DataColumn("CURRENTTIME", typeof(string))); // 제조기술
                #endregion

                foreach (DataRow row in requestdata.Rows)
                {

                    row["LBLNOTICE"] = Language.Get("SPECCHANGENOTICE"); // 사양변경통보서
                    row["LBLREQUESTTEAM"] = Language.Get("REQUESTTEAM"); // 의뢰부서
                    row["LBLREQUESTER"] = Language.Get("REQUESTER"); // 의뢰자
                    row["LBLREQUESTNO"] = Language.Get("REQUESTNO"); // 의뢰 no
                    row["LBLREQUESTDATE"] = Language.Get("REQUESTDATE"); // 의뢰일자
                    row["LBLMODELCODE"] = Language.Get("MODELCODE"); // 모델코드
                    row["LBLMODELVERSION"] = Language.Get("MODELVERSION"); // 모델 REV
                    row["LBLMODELNAME"] = Language.Get("MODELNAME"); // 모델명
                    row["LBLMODIFYCOMMENT"] = Language.Get("MODIFYCOMMENT"); // 수정내용
                    row["LBLCOMPANYUSER"] = Language.Get("COMPANYUSER"); // 업체담당자
                    row["LBLMODIFIBEFORE"] = Language.Get("MODIFIBEFORE"); // 수정전
                    row["LBLMODIFIAFTER"] = Language.Get("MODIFIAFTER"); // 수정후
                    row["LBLETCSPEC"] = Language.Get("ETCSPEC"); // 기타
                    row["LBLAFTERMODELID"] = Language.Get("AFTERMODELID"); // 변경후 모델코드
                    row["LBLAFTERMODELREV"] = Language.Get("AFTERMODELREV"); // 변경후모델REV
                    row["LBLAFTERMODELNAME"] = Language.Get("AFTERMODELNAME"); // 모델 REV
                    row["LBLRESULTS"] = Language.Get("RESULTS"); // 처리결과
                    row["LBLMANUFACTURING"] = Language.Get("MANUFACTURING"); // 제조
                    row["LBLMANUFACTURINGMNG"] = Language.Get("MANUFACTURINGMNG"); // 제조관리
                    row["LBLQUALITYMNG"] = Language.Get("QUALITYMNG"); // 품질관리
                    row["LBLOUTSOURCINGMNG"] = Language.Get("OUTSOURCINGMNG"); // 외주관리
                    row["LBLDOMESTICSALES"] = Language.Get("DOMESTICSALES"); // 국내영업
                    row["LBLOVERSEASSALES"] = Language.Get("OVERSEASSALES"); // 해외영업
                    row["LBLPURCHASE"] = Language.Get("PURCHASE"); // 구매
                    row["LBLMANUFACTURINGSKILL"] = Language.Get("MANUFACTURINGSKILL"); // 제조기술

                    DataTable dtCodeClass = SqlExecuter.Query("GetCurrentTime", "10001");
                    row["CURRENTTIME"] = dtCodeClass.Rows[0]["CURRENTTIME"];

                }

                dsReport.Tables.Add(requestdata);

                Assembly assembly = Assembly.GetAssembly(this.GetType());
                Stream stream = assembly.GetManifestResourceStream("Micube.SmartMES.StandardInfo.Report.GovernanceChangeReport.repx");
                XtraReport report = XtraReport.FromStream(stream);

                report.DataSource = dsReport;

                Band detailPage = report.Bands["Detail"];
                SetReportControlDataBinding(detailPage.Controls, dsReport.Tables[0]);

                ReportPrintTool printTool = new ReportPrintTool(report);
                printTool.ShowRibbonPreview();




            }
        }


        private void SetReportControlDataBinding(XRControlCollection controls, DataTable dataSource)
        {
            if (controls.Count > 0)
            {
                foreach (XRControl control in controls)
                {
                    if (!string.IsNullOrWhiteSpace(control.Tag.ToString()))
                        control.DataBindings.Add("Text", dataSource, control.Tag.ToString());

                    SetReportControlDataBinding(control.Controls, dataSource);
                }
            }
        }

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            // TODO : 저장 Rule 변경
            DataTable changed = grdList.GetChangedRows();

            ExecuteRule("SaveCodeClass", changed);
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
            var values = Conditions.GetValues();

            DataTable dtCodeClass = SqlExecuter.Query("SelectGovernanceChange", "10002");

            if (dtCodeClass.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdrequest.DataSource = dtCodeClass;
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
            grdrequest.View.CheckValidation();

            DataTable changed = grdrequest.GetChangedRows();

            if (changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }

        #endregion

        #region Private Function

        // TODO : 화면에서 사용할 내부 함수 추가
        /// <summary>
        /// 마스터 정보를 조회한다.
        /// </summary>
        private void LoadData()
        {

        }

        #endregion
    }
}
