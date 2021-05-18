#region using

using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Grid.BandedGrid;
using Micube.SmartMES.Commons;
using System;
using Micube.SmartMES.Commons.Controls;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 수입 검사 관리 >공정 수입검사 결과 등록
    /// 업  무  설  명  : 공정 수입검사결과를 등록한다.
    /// 생    성    자  : 강유라
    /// 생    성    일  : 2019-09-03 -- 통합테스트 이전에 출하검사 먼저 개발 
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ProcessImportInspectionResult : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가
        DataTable _requested;
        DataRow _selectedLot = null;
        ProcessImportInspectionResultPopup ospResultPopup;


        #endregion

        #region 생성자

        public ProcessImportInspectionResult()
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
            grdProcessInspectionResult.GridButtonItem = GridButtonItem.Export;
            grdProcessInspectionResult.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdProcessInspectionResult.View.SetIsReadOnly();


            var item = grdProcessInspectionResult.View.AddGroupColumn("");

            item.AddTextBoxColumn("REQUESTDATE", 130)
                .SetLabel("REQUESTDATETIME");

            item.AddTextBoxColumn("RECEIVINGDATE",130)
                .SetLabel("TRANSACTIONDATE");

            item.AddTextBoxColumn("LOTID",200);


            grdProcessInspectionResult.View.AddTextBoxColumn("LOTHISTKEY")
                .SetIsHidden();

            grdProcessInspectionResult.View.AddTextBoxColumn("TXNHISTKEY")
                .SetIsHidden();

            grdProcessInspectionResult.View.AddTextBoxColumn("ISREWORK")
                .SetIsHidden();

            grdProcessInspectionResult.View.AddTextBoxColumn("RECEIVINGDATETIME")
                .SetIsHidden();

            /*
            var lotInfo = grdProcessInspectionResult.View.AddGroupColumn("LOTINFO");

            lotInfo.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME",200);

            lotInfo.AddTextBoxColumn("PROCESSSEGMENTNAME",200);

            lotInfo.AddTextBoxColumn("AREANAME",200);

            lotInfo.AddTextBoxColumn("PRODUCTDEFID",150);

            lotInfo.AddTextBoxColumn("PRODUCTDEFVERSION",150);

            lotInfo.AddTextBoxColumn("PRODUCTDEFNAME",200);
            */

            var InspectionInfo = grdProcessInspectionResult.View.AddGroupColumn("INSPECTIONINFO");
            
            InspectionInfo.AddTextBoxColumn("WRISREWORK", 150)
                .SetLabel("ISREWORK").SetIsHidden();

            InspectionInfo.AddComboBoxColumn("REWORKDIVISION", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ReworkDivision", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("TXNNAME");

            InspectionInfo.AddTextBoxColumn("DEGREE",100);

            InspectionInfo.AddTextBoxColumn("INSPECTIONDATE",130);

            InspectionInfo.AddTextBoxColumn("INSPECTIONRESULT",100);

            InspectionInfo.AddTextBoxColumn("TRANSITAREANAME", 100)
                .SetLabel("TRANSITAREA");

            InspectionInfo.AddTextBoxColumn("ISSENDNAME",150)
                .SetLabel("ISTAKEOVER");

            InspectionInfo.AddTextBoxColumn("WRPROCESSSEGMENTNAME", 150)
               .SetLabel("PROCESSSEGMENTNAME");

            InspectionInfo.AddTextBoxColumn("WRAREANAME", 150)
               .SetLabel("AREANAME");

            InspectionInfo.AddTextBoxColumn("WRPRODUCTDEFID", 150)
                .SetLabel("PRODUCTDEFID");

            InspectionInfo.AddTextBoxColumn("WRPRODUCTDEFVERSION", 150)
                .SetLabel("PRODUCTDEFVERSION");

            InspectionInfo.AddTextBoxColumn("WRPRODUCTDEFNAME", 200)
                .SetLabel("PRODUCTDEFNAME");

            grdProcessInspectionResult.View.PopulateColumns();
        }

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            //로드 이벤트 : 로드시 입고 처리 할 목록을 조회하는 이벤트
            Load += (s, e) => { SearchData(); };
            //그리드 더블클릭 이벤트
            grdProcessInspectionResult.View.DoubleClick += View_DoubleClick;
            //lotId enter 이벤트 : lotId를 입력 하면 입고일시를 입력하고 저장해주는 이벤트
            txtLotId.Editor.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter && txtLotId.EditValue != null)
                {
                    SelectLot();
                }
            };
            //grdProcessInspectionResult.View.AddingNewRow += (s, e) => { BindingRow(e.NewRow, _selectedLot); };

        }
        /// <summary>
        /// 검사결과를 등록하는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_DoubleClick(object sender, EventArgs e)
        {
            DXMouseEventArgs ea = e as DXMouseEventArgs;
            SmartBandedGridView view = sender as SmartBandedGridView;
            GridHitInfo info = view.CalcHitInfo(ea.Location);

            DataRow row = grdProcessInspectionResult.View.GetDataRow(info.RowHandle);

            if (row == null) return;

            if (string.IsNullOrWhiteSpace(row["RECEIVINGDATE"].ToString()))
            {
                ShowMessage("AcceptBeforeProcessInspection");//공정 수입 검사전 입고처리를 하세요.
                return;
            };

            if (string.IsNullOrWhiteSpace(row["TXNHISTKEY"].ToString()))
            { //검사전

                if (!row["PROCESSSTATE"].ToString().Equals("WaitForSend"))
                {
                    ShowMessage("IsWaitForSendOSPInspection");//인계 대기 상태인 LOT만 검사 가능 합니다.
                    return;
                };

                if (!row["LOTSTATE"].ToString().Equals("InProduction"))
                {
                    ShowMessage("IsProductionOSPInspection");//생산중인 LOT만 검사 가능 합니다.
                    return;
                };

                if (row["ISLOCKING"].ToString().Equals("Y"))
                {
                    ShowMessage("LotIsLocking", string.Format("LotId = {0}", row["LOTID"].ToString()));//Locking 상태의 Lot 입니다. {0}
                    return;
                };

            }
            string type;

            if (string.IsNullOrWhiteSpace(row["TXNHISTKEY"].ToString()))
            {//insert
                type = "insertData";
            }
            else
            {//update
                type = "updateData";
            }

            DialogManager.ShowWaitArea(pnlContent);

            //버튼의 enable
            bool isEnable = btnPopupFlag.Enabled;

            if (!Format.GetString(row["ISMODIFY"]).Equals("Y"))
            {
                isEnable = false;
            }

            ospResultPopup = new ProcessImportInspectionResultPopup(type, this);
            ospResultPopup.StartPosition = FormStartPosition.CenterParent;
            ospResultPopup.FormBorderStyle = FormBorderStyle.Sizable;
            ospResultPopup._type = type;
            ospResultPopup.Owner = this;
            ospResultPopup.CurrentDataRow = row;
            ospResultPopup.isEnable = isEnable;
            int rowCount  =ospResultPopup.SetControlSearchData(row);

            DialogManager.CloseWaitArea(pnlContent);

            if (rowCount > 0)
            {
                ospResultPopup.Show(); // 2020.02.24-유석진-팝업 미모달로 변경
                /* 2020.02.21-유석진-팝업을 미모달로 띄우면서 해당 로직이 안 타므로 주석처리
                if (ospResultPopup.DialogResult == DialogResult.OK)
                {
                    SearchMainGrd();
                }
                */
            }
        }

        #endregion

        #region 툴바
        
        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            //base.OnToolbarSaveClick();

            // TODO : 저장 Rule 변경
            //SaveAcceptDate();
        }

        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            //base.OnToolbarClick(sender, e);
            SmartButton btn = sender as SmartButton;

            if (btn.Name.ToString().Equals("Incoming"))
            {
                Btn_SaveClick(btn.Text);
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
            var values = Conditions.GetValues();
            values.Add("RESOURCETYPE", "ProcessInspection");
            values.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);
            //values.Add("PLANTID", Framework.UserInfo.Current.Plant);
            values.Add("ENTERPRISEID", Framework.UserInfo.Current.Enterprise);
            //values.Add("QUERYTYPE", "Grd");

            DataTable dt = await SqlExecuter.QueryAsync("SelectOSPInspResultGrd", "10001", values);

            if (dt.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdProcessInspectionResult.DataSource = dt;
            //SearchRequestedToAccept();
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            //InitializeConditionPopup_ProcessSegmentClassPopup();
            this.Conditions.AddComboBox("P_PROCESSSEGMENTCLASSID", new SqlQuery("GetProcessSegmentClass", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"PLANTID={UserInfo.Current.Plant}", "PROCESSSEGMENTCLASSTYPE=TopProcessSegmentClass"), "PROCESSSEGMENTCLASSNAME", "PROCESSSEGMENTCLASSID")
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                .SetEmptyItem()
                .SetLabel("LARGEPROCESSSEGMENT")
                .SetPosition(3.1);

            InitializeConditionProcessSegmentId_Popup();
            // 작업장
            //CommonFunction.AddConditionAreaPopup("P_AREAID", 3.3, false, Conditions);
            InitializeConditionAreaId_Popup();
            // 품목
            CommonFunction.AddConditionProductPopup("P_PRODUCTDEFID", 3.4, true, Conditions);
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
            this.Conditions.GetControl<SmartComboBox>("P_PLANTID").EditValue = Framework.UserInfo.Current.Plant;
            this.Conditions.GetControl<SmartComboBox>("P_PLANTID").Enabled = false;
        }

        #region 조회조건 팝업
        /*
        private void InitializeConditionPopup_ProcessSegmentClassPopup()
        {
            //팝업 컬럼 설정
            var ProcessSegmentClassId = Conditions.AddSelectPopup("P_PROCESSSEGMENTCLASSID", new SqlQuery("GetProcessSegmentClass", "10001", "PROCESSSEGMENTCLASSTYPE=TopProcessSegmentClass", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PROCESSSEGMENTCLASSNAME", "PROCESSSEGMENTCLASSNAME")
                                                       .SetPopupLayout("PROCESSSEGMENT", PopupButtonStyles.Ok_Cancel, true, false)
                                                       .SetPopupResultCount(1)
                                                       .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow)
                                                       .SetPopupAutoFillColumns("PROCESSSEGMENTCLASSNAME")
                                                       .SetPosition(3.1)
                                                       .SetLabel("PROCESSSEGMENTCLASSID");

            ProcessSegmentClassId.Conditions.AddTextBox("PROCESSSEGMENTCLASS")
                .SetLabel("TOPPROCESSSEGMENTCLASS");

            // 팝업 그리드
            ProcessSegmentClassId.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 150);
            ProcessSegmentClassId.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 200);
        }
        */

        /// <summary>
        /// 팝업형 조회조건 생성 - 작업장
        /// </summary>
        private void InitializeConditionAreaId_Popup()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("AreaType", "Area");
            //param.Add("PlantId", UserInfo.Current.Plant);
            param.Add("LanguageType", UserInfo.Current.LanguageType);
            param.Add("AREA", UserInfo.Current.Area);
            param.Add("P_USERID", UserInfo.Current.Id);

            var areaIdPopup = this.Conditions.AddSelectPopup("p_areaId", new SqlQuery("GetAuthorityUserUseArea", "10001", param), "AREANAME", "AREAID")
                .SetPopupLayout(Language.Get("SELECTAREAID"), PopupButtonStyles.Ok_Cancel, true, true)
                .SetPopupResultCount(1)
                .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                .SetPopupAutoFillColumns("AREANAME")
                .SetLabel("AREA")
                .SetRelationIds("P_PLANTID")
                .SetPosition(3.3);

            areaIdPopup.Conditions.AddTextBox("AREAIDNAME");

            areaIdPopup.GridColumns.AddTextBoxColumn("AREAID", 150);
            areaIdPopup.GridColumns.AddTextBoxColumn("AREANAME", 200);
        }

        private void InitializeConditionProcessSegmentId_Popup()
        {
            var processSegmentIdPopup = this.Conditions.AddSelectPopup("P_PROCESSSEGMENTID", new SqlQuery("GetProcessSegmentByTclass", "10001", $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PROCESSSEGMENTNAME", "PROCESSSEGMENTID")
                .SetPopupLayout(Language.Get("SELECTPROCESSSEGMENTID"), PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupResultCount(1)
                .SetPopupLayoutForm(800, 800, FormBorderStyle.FixedToolWindow)
                .SetLabel("PROCESSSEGMENT")
                .SetRelationIds("P_PROCESSSEGMENTCLASSID")
                .SetPosition(3.2);

            processSegmentIdPopup.Conditions.AddTextBox("PROCESSSEGMENT");

            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME_TOP", 150)
                .SetLabel("LARGEPROCESSSEGMENT");
            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME_MIDDLE", 150)
                .SetLabel("MIDDLEPROCESSSEGMENT");
            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150);
            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTVERSION", 200);
        }
        #endregion

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            // TODO : 유효성 로직 변경
            grdProcessInspectionResult.View.CheckValidation();

            DataTable changed = grdProcessInspectionResult.View.GetCheckedRows();

            if (changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }

        #endregion

        #region Private Function
        /// <summary>
        /// 자동 조회 함수
        /// </summary>
        private void SearchData()
        {
            var values = Conditions.GetValues();
            values.Add("RESOURCETYPE", "ProcessInspection");
            values.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);
            //values.Add("PLANTID", Framework.UserInfo.Current.Plant);
            values.Add("ENTERPRISEID", Framework.UserInfo.Current.Enterprise);
            //values.Add("QUERYTYPE", "Grd");

            DataTable dt =  SqlExecuter.Query("SelectOSPInspResultGrd", "10001", values);

            grdProcessInspectionResult.DataSource = dt;
        }

        // TODO : 화면에서 사용할 내부 함수 추가
        /*
        /// <summary>
        /// LOTID 입력후 엔터를 누르면 공정검사 의뢰 목록중 일치하는 LOTID를 메인그리드에 뿌려주기위한 목록 조회 함수
        /// </summary>
        private void SearchRequestedToAccept()
        {
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("P_PLANTID", Framework.UserInfo.Current.Plant);
            values.Add("RESOURCETYPE", "ProcessInspection");
            values.Add("QUERYTYPE", "Temp");
            values.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);

            _requested = SqlExecuter.Query("SelectOSPInspResultGrd", "10001",values);
        }
        */
        /// <summary>
        /// 메인그리드를 조회하는 함수
        /// </summary>
        public async void SearchMainGrd()
        {
            await OnSearchAsync();
        }

        /// <summary>
        /// 입력한 Lotid를 _requested테이블에서 찾아서 메인그리드에 add해주는 함수
        /// </summary>
        private void SelectLot()
        {
            DataTable dt = grdProcessInspectionResult.DataSource as DataTable;

            var selectLotList = dt.AsEnumerable()
                    .Where(r => string.IsNullOrWhiteSpace(r["RECEIVINGDATE"].ToString())
                    && string.IsNullOrWhiteSpace(r["ISSEND"].ToString())
                    //2019-12-27 작업장 제한
                    && r["ISMODIFY"].ToString().Equals("Y")
                    && r["LOTID"].ToString().Equals(txtLotId.EditValue.ToString().Trim()))
                    .ToList();

            if (selectLotList.Count < 1)
            {
                ShowMessage("NoDataToOSPInspection");//공정 수입검사에 의뢰된 내역이 없습니다.
                return;
            }

            string lotHistKey = selectLotList.CopyToDataTable().Rows[0]["LOTHISTKEY"].ToString();

            var handles = grdProcessInspectionResult.View.GetRowHandlesByValue("LOTHISTKEY", lotHistKey);
            int rowHandle = -1;

            foreach (var item in handles)
            {
                DataRow row = grdProcessInspectionResult.View.GetDataRow(item);

                if (string.IsNullOrWhiteSpace(row["RECEIVINGDATE"].ToString()))
                {
                    rowHandle = item;
                }
            }

            if (rowHandle != -1)
            {
                grdProcessInspectionResult.View.CheckRow(rowHandle, true);
            }
            else
            {
                ShowMessage("NoDataToOSPInspection");//공정 수입검사에 의뢰된 내역이 없습니다.
            }

        }
        /*
        /// <summary>
        /// 검색된 row를  그리드에 바인딩하는 함수
        /// 바인딩 후 입고일시 update 후 메인그리드 재조회
        /// </summary>
        /// <param name="row"></param>
        private void BindingRow(DataRow newRow, DataRow row)
        {
            if (row == null) return;

            newRow["RECEIVINGDATE"] = DateTime.Now.ToString("yyyy-MM-dd");
            newRow["RECEIVINGDATETIME"] = DateTime.Now;
            newRow["PROCESSSEGMENTCLASSNAME"] = row["PROCESSSEGMENTCLASSNAME"];
            newRow["PROCESSSEGMENTNAME"] = row["PROCESSSEGMENTNAME"];
            newRow["AREANAME"] = row["AREANAME"];
            newRow["PRODUCTDEFID"] = row["PRODUCTDEFID"];
            newRow["PRODUCTDEFNAME"] = row["PRODUCTDEFNAME"];
            newRow["LOTID"] = row["LOTID"];
            newRow["LOTHISTKEY"] = row["LOTHISTKEY"];
            newRow["DEGREE"] = row["DEGREE"];
            newRow["INSPECTIONDATE"] = row["INSPECTIONDATE"];
            newRow["INSPECTIONRESULT"] = row["INSPECTIONRESULT"];
            newRow["ISSENDNAME"] = row["ISSENDNAME"];

            SaveAcceptDate();
            SearchMainGrd();
        }
        */
        /// <summary>
        /// 입고일시가 입력되면 저장(update)하는 함수
        /// </summary>
        private void SaveAcceptDate()
        {
            DataTable dt = grdProcessInspectionResult.View.GetCheckedRows();
   
            ExecuteRule("SaveOSPInspectionAccept", dt);
        }


        /// <summary>
        /// 저장 함수
        /// </summary>
        /// <param name="strtitle"></param>
        private void Btn_SaveClick(string strtitle)
        {
            grdProcessInspectionResult.View.CloseEditor();
            grdProcessInspectionResult.View.CheckValidation();

            DataTable changed = grdProcessInspectionResult.View.GetCheckedRows();

            if (changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                this.ShowMessage(MessageBoxButtons.OK, "NoSaveData");
                return;
            }

            DialogResult result = System.Windows.Forms.DialogResult.No;

            result = this.ShowMessage(MessageBoxButtons.YesNo, "InfoSave", strtitle);//저장하시겠습니까? 

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    this.ShowWaitArea();

                    ExecuteRule("SaveOSPInspectionAccept", changed);
                    ShowMessage("SuccessSave");
                    //재조회 
                    SearchData();
                }
                catch (Exception ex)
                {
                    this.ShowError(ex);
                }
                finally
                {
                    this.CloseWaitArea();


                }
            }
        }
        #endregion

        #region Public Function
        /// <summary>
        /// 자주검사(입고,출하)결과조회-2020.02.24-유석진
        /// </summary>
        public void setOpenMenu()
        {
            var values = Conditions.GetValues();
            values.Add("LOTID", ospResultPopup.CurrentDataRow["LOTID"]);

            OpenMenu("PG-QC-0560", values); // 자주검사(입고,출하)화면 호출
        }
        #endregion
    }
}
