#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons;
//using Micube.SmartMES.ProcessManagement;
using System;
using Micube.SmartMES.Commons.Controls;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;
using System.IO;
using DevExpress.LookAndFeel;
using System.Reflection;

#endregion

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 신뢰성검증 > BBT 검증 의뢰
    /// 업  무  설  명  :  
    /// 생    성    자  : 유호석
    /// 생    성    일  : 2019-12-23
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ReliabilityVerificationRequestBBT : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가
        DataTable _dtVerifiCount = new DataTable();
        #endregion

        #region 생성자

        /// <summary>
        /// 생성자
        /// </summary>
        public ReliabilityVerificationRequestBBT()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeGridReliabilityVerificationRequestRgisterRegular(); // 신뢰성 의뢰접수(정기) 그리드 초기화
            InitializeEvent();

            Dictionary<string, object> radioParam = new Dictionary<string, object>();
            radioParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            radioParam.Add("CODECLASSID", "VerifiCount");

            _dtVerifiCount = SqlExecuter.Query("GetCodeList", "00001", radioParam);
        }

        /// <summary>        
        /// 그리드 초기화(신뢰서 의뢰 접수(정기))
        /// </summary>
        private void InitializeGridReliabilityVerificationRequestRgisterRegular()
        {
            grdReliabiVerifiReqRgistRegular.GridButtonItem -= GridButtonItem.Delete; // 삭제 버튼 비활성화
            grdReliabiVerifiReqRgistRegular.GridButtonItem -= GridButtonItem.Add; // 추가 버튼 비활성화
            grdReliabiVerifiReqRgistRegular.GridButtonItem -= GridButtonItem.Copy; // 복사 버튼 비활성화
            grdReliabiVerifiReqRgistRegular.GridButtonItem -= GridButtonItem.Import; // Import 버튼 비활성화

            grdReliabiVerifiReqRgistRegular.View.SetSortOrder("WORKENDTIME");
            grdReliabiVerifiReqRgistRegular.View.SetSortOrder("LOTID");

            var DEFAULTINFO = grdReliabiVerifiReqRgistRegular.View.AddGroupColumn("DEFAULTINFO");
            DEFAULTINFO.AddCheckEditColumn("RECEIVE", 70).SetDefault(false).SetTextAlignment(TextAlignment.Center); // 접수
            DEFAULTINFO.AddTextBoxColumn("WORKENDTIME", 180).SetIsReadOnly().SetTextAlignment(TextAlignment.Center); // 작업종료시간
            DEFAULTINFO.AddTextBoxColumn("SAMPLERECEIVEDATE", 180).SetIsReadOnly().SetTextAlignment(TextAlignment.Center); // 시료접수일시
            DEFAULTINFO.AddSpinEditColumn("TRANSITIONDATE", 70).SetIsReadOnly().SetTextAlignment(TextAlignment.Right); // 경과일
            //grdReliabiVerifiReqRgistRegular.View.AddTextBoxColumn("REQUESTDEPT", 210).SetIsReadOnly().SetTextAlignment(TextAlignment.Left); // 의뢰부서(공정)
            DEFAULTINFO.AddTextBoxColumn("PRODUCTDEFID", 100).SetIsReadOnly().SetTextAlignment(TextAlignment.Center); // 품목코드
            DEFAULTINFO.AddTextBoxColumn("PRODUCTDEFNAME", 230).SetIsReadOnly().SetTextAlignment(TextAlignment.Left); // 품목명
            DEFAULTINFO.AddTextBoxColumn("PRODUCTDEFVERSION", 70).SetIsReadOnly().SetTextAlignment(TextAlignment.Center); // Rev
            DEFAULTINFO.AddTextBoxColumn("LAYER", 70).SetIsReadOnly().SetTextAlignment(TextAlignment.Center); // 층수
            DEFAULTINFO.AddTextBoxColumn("LOTID", 170).SetIsReadOnly().SetTextAlignment(TextAlignment.Center); // LOT NO
            //grdReliabiVerifiReqRgistRegular.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 210).SetIsReadOnly().SetTextAlignment(TextAlignment.Left); // 표준공정
            DEFAULTINFO.AddTextBoxColumn("AREANAME", 120).SetIsReadOnly().SetTextAlignment(TextAlignment.Left); // 작업장
            DEFAULTINFO.AddSpinEditColumn("WORKENDPCSQTY", 70)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right)
                .SetLabel("TOTALQTY"); // 총수량(작업완료 수량)

            var FLOOR2 = grdReliabiVerifiReqRgistRegular.View.AddGroupColumn("FLOOR2");
            FLOOR2.AddTextBoxColumn("SNDNGCOUNT", 100).SetIsReadOnly().SetTextAlignment(TextAlignment.Right).SetLabel("OPENDEFECTQTY"); // 불량수
            FLOOR2.AddTextBoxColumn("SNDNGRATE", 100).SetIsReadOnly().SetTextAlignment(TextAlignment.Right).SetLabel("OPENDEFECTRATE"); // 불량율
            var FLOOR4 = grdReliabiVerifiReqRgistRegular.View.AddGroupColumn("FLOOR4");
            FLOOR4.AddTextBoxColumn("TRDNGCOUNT", 100).SetIsReadOnly().SetTextAlignment(TextAlignment.Right).SetLabel("OPENDEFECTQTY"); // 불량수
            FLOOR4.AddTextBoxColumn("TRDNGRATE", 100).SetIsReadOnly().SetTextAlignment(TextAlignment.Right).SetLabel("OPENDEFECTRATE"); // 불량율
            var SUM = grdReliabiVerifiReqRgistRegular.View.AddGroupColumn("SUM");
            SUM.AddTextBoxColumn("TOTNGCOUNT", 100).SetIsReadOnly().SetTextAlignment(TextAlignment.Right).SetLabel("OPENDEFECTQTY"); // 불량수
            SUM.AddTextBoxColumn("TOTNGRATE", 100).SetIsReadOnly().SetTextAlignment(TextAlignment.Right).SetLabel("OPENDEFECTRATE"); // 불량율

            //grdReliabiVerifiReqRgistRegular.View.AddTextBoxColumn("EQUIPMENTNAME", 210).SetIsReadOnly().SetTextAlignment(TextAlignment.Left); // 설비(호기)
            //grdReliabiVerifiReqRgistRegular.View.AddTextBoxColumn("ISREREQUEST", 150).SetIsReadOnly().SetTextAlignment(TextAlignment.Center); // 재의뢰여부

            grdReliabiVerifiReqRgistRegular.View.PopulateColumns();
        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            txtLotId2.EnterKeyDown += TxtLotId2_EnterKeyDown;

            btnRecieve1.Click += BtnRecieve1_Click;
        }

        /// <summary>        
        /// 의뢰접수/재접수 접수 처리
        /// </summary>
        private void BtnRecieve1_Click(object sender, EventArgs e)
        {
            DialogResult result = System.Windows.Forms.DialogResult.No;

            result = this.ShowMessage(MessageBoxButtons.YesNo, "RegisterNote");//접수 하시겠습니까?

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    this.ShowWaitArea();
                    btnRecieve1.Enabled = false;

                    SaveRecept(grdReliabiVerifiReqRgistRegular);// 의뢰 접수

                    ShowMessage("SuccessSave");

                    Search(); // 조회
                }
                catch (Exception ex)
                {
                    this.ShowError(ex);
                }
                finally
                {
                    this.CloseWaitArea();
                    btnRecieve1.Enabled = true;
                }
            }
        }

        /// <summary>        
        /// 의뢰서 접수 lot 번호 입력 후 Enter 입력 시 해당 그리드 행 접수 선택(체크)
        /// </summary>
        private void TxtLotId2_EnterKeyDown(object sender, KeyEventArgs e)
        {
            DataView dv = grdReliabiVerifiReqRgistRegular.View.DataSource as DataView;
            DataTable dt = dv.Table;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];

                if (dr["LOTID"].ToString().Equals(txtLotId2.EditValue))
                {
                    dr["RECEIVE"] = true;
                }
            }
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
        /// <summary>	
        /// 툴바버튼 클릭	
        /// </summary>	
        /// <param name="sender"></param>	
        /// <param name="e"></param>	
        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            SmartButton btn = sender as SmartButton;

            if (btn.Name.ToString().Equals("Save"))//저장
            {
                BtnRecieve1_Click(null, null);
            }
        }
        #endregion

        #region 검색

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dt = null;

            dt = await SqlExecuter.QueryAsync("GetReliabilityVerificationRequestBBTRgisterList", "10001", values);

            if (dt.Rows.Count < 1)
            {
                ShowMessage("NoSelectData");
            }

            grdReliabiVerifiReqRgistRegular.DataSource = dt;
        }

        #endregion

        #region 조회조건 설정

        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            //Site
            //var cboPlant = Conditions.AddComboBox("P_PLANTID", new SqlQuery("GetPlantList", "10019", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            //cboPlant.SetLabel("PLANTID");
            //cboPlant.SetPosition(1);
            //cboPlant.SetDefault("");
            //cboPlant.SetEmptyItem("", "");

            InitializeConditionPopup_Product();
            CommonFunction.AddConditionLotHistPopup("P_LOTID", 2.2, false, Conditions);
        }

        /// <summary>
        /// 품목 조회조건
        /// </summary>
        private void InitializeConditionPopup_Product()
        {
            // 팝업 컬럼설정
            var productPopup = Conditions.AddSelectPopup("p_productdefId", new SqlQuery("GetProductListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PRODUCTDEFNAME", "PRODUCTDEFIDVERSION")
               .SetPopupLayout("PRODUCT", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupLayoutForm(600, 800)
               .SetLabel("PRODUCT")
               .SetPopupAutoFillColumns("PRODUCTDEFNAME")
               .SetPopupResultCount(1)
               .SetPosition(2.1)
               .SetPopupResultCount(0);

            // 팝업 조회조건
            productPopup.Conditions.AddTextBox("PRODUCTDEFIDNAME");

            // 팝업 그리드
            productPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 120)
                .SetValidationKeyColumn();
            productPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 220);
            productPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 80);
            productPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFIDVERSION", 100)
                .SetIsHidden();
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


        /// <summary>
        /// 의뢰서 접수 조회
        /// </summary>
        private void SearchReliabiVerifiReqRgistRegular()
        {
            try
            {
                this.ShowWaitArea();

                var values = Conditions.GetValues();
                values.Add("P_ISSECOND", "R"); // 의뢰서 접수
                values.Add("P_LANGUAGETYPE", UserInfo.Current.LanguageType);

                DataTable dt = SqlExecuter.Query("GetReliabilityVerificationRequestBBTRgisterList", "10002", values);

                if (dt.Rows.Count < 1)
                {
                    ShowMessage("NoSelectData");
                }

                grdReliabiVerifiReqRgistRegular.DataSource = dt;
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

        /// <summary>
        /// 의뢰서 접수/재접수 접수 저장
        /// </summary>
        private void SaveRecept(SmartBandedGrid grid)
        {
            int i = 0;

            DataTable dt = grid.DataSource as DataTable;

            foreach (DataRow dr in dt.Rows)
            {
                if (dr["RECEIVE"].ToString() == "True" && (dr["SAMPLERECEIVEDATE"] == DBNull.Value || dr["SAMPLERECEIVEDATE"].ToString().Length == 0))
                {
                    i++;
                }
            }

            if (i == 0)
            {
                throw MessageException.Create("NoSaveData");
            }

            // CT_RELIABILITYREFMANUFACTURING Table에 저장될 Data
            DataTable reliabilityVerificationTable = new DataTable();
            reliabilityVerificationTable.TableName = "list";
            DataRow row = null;

            reliabilityVerificationTable.Columns.Add(new DataColumn("ENTERPRISEID", typeof(string)));
            reliabilityVerificationTable.Columns.Add(new DataColumn("PLANTID", typeof(string)));
            reliabilityVerificationTable.Columns.Add(new DataColumn("LOTID", typeof(string)));
            reliabilityVerificationTable.Columns.Add(new DataColumn("PRODUCTDEFID", typeof(string)));
            reliabilityVerificationTable.Columns.Add(new DataColumn("PRODUCTDEFVERSION", typeof(string)));
            reliabilityVerificationTable.Columns.Add(new DataColumn("WORKCOUNT", typeof(string)));

            reliabilityVerificationTable.Columns.Add(new DataColumn("PROCESSSEGMENTID", typeof(string)));
            reliabilityVerificationTable.Columns.Add(new DataColumn("PROCESSSEGMENTVERSION", typeof(string)));
            reliabilityVerificationTable.Columns.Add(new DataColumn("AREAID", typeof(string)));
            reliabilityVerificationTable.Columns.Add(new DataColumn("PROCESSDEFID", typeof(string)));
            reliabilityVerificationTable.Columns.Add(new DataColumn("PROCESSDEFVERSION", typeof(string)));
            reliabilityVerificationTable.Columns.Add(new DataColumn("USERSEQUENCE", typeof(string)));
            reliabilityVerificationTable.Columns.Add(new DataColumn("VERIFICOUNT", typeof(string)));

            string sVerifiCount = string.Empty;
            if (_dtVerifiCount != null && _dtVerifiCount.Rows.Count > 0)
            {
                sVerifiCount = _dtVerifiCount.Rows[0]["CODENAME"].ToString();
            }

            foreach (DataRow dr in dt.Rows)
            {
                if (dr["RECEIVE"].ToString() == "True" && (dr["SAMPLERECEIVEDATE"] == DBNull.Value || dr["SAMPLERECEIVEDATE"].ToString().Length == 0))
                {
                    row = reliabilityVerificationTable.NewRow();
                    row["ENTERPRISEID"] = dr["ENTERPRISEID"];
                    row["PLANTID"] = dr["PLANTID"];
                    row["LOTID"] = dr["LOTID"];
                    row["PRODUCTDEFID"] = dr["PRODUCTDEFID"];
                    row["PRODUCTDEFVERSION"] = dr["PRODUCTDEFVERSION"];
                    row["WORKCOUNT"] = dr["WORKCOUNT"];


                    row["PROCESSSEGMENTID"] = dr["PROCESSSEGMENTID"];
                    row["PROCESSSEGMENTVERSION"] = dr["PROCESSSEGMENTVERSION"];
                    row["AREAID"] = dr["AREAID"];
                    row["PROCESSDEFID"] = dr["PROCESSDEFID"];
                    row["PROCESSDEFVERSION"] = dr["PROCESSDEFVERSION"];
                    row["USERSEQUENCE"] = dr["USERSEQUENCE"];
                    row["VERIFICOUNT"] = dr["VERIFICOUNT"] == DBNull.Value || dr["VERIFICOUNT"].ToString().Length == 0 ? sVerifiCount : dr["VERIFICOUNT"];
                    reliabilityVerificationTable.Rows.Add(row);
                }
            }

            DataSet rullSet = new DataSet();
            rullSet.Tables.Add(reliabilityVerificationTable);

            ExecuteRule("SaveReliabilityVerificationRequestBBTRecept", rullSet);
        }
        #endregion

        #region Global Function

        /// <summary>
        /// Popup 닫혔을때 재검색하기 위한 함수
        /// </summary>
        public void Search()
        {
            OnSearchAsync();
        }

        #endregion
    }
}
