#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Validations;
using static Micube.SmartMES.Commons.SPCLibrary.DataSets.SpcDataSet;

using DevExpress.XtraEditors.Repository;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Grid;
using System.Drawing;
using Micube.SmartMES.Commons.SPCLibrary;

#endregion

namespace Micube.SmartMES.SPC
{
    /// <summary>
    /// 프 로 그 램 명  : SPC> SPC 현황 관리도 화면> Rule Check결과
    /// 업  무  설  명  : SPC 관리도에서 Rule Check 결과.
    /// 생    성    자  : 이승우
    /// 생    성    일  : 2019-12-01
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class SpcControlRulesResultPopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region Local Variables
        /// <summary>
        /// 팝업진입전 그리드의 Row
        /// </summary>
        public DataRow CurrentDataRow { get; set; }
        /// <summary>
        /// 룰 Check 결과 값.
        /// </summary>
        public RulesCheck rulesCheckData;
        /// <summary>
        /// 룰 Check 결과 값 DataTable형으로 저장.
        /// </summary>
        private DataTable _dtRulesResult;
        /// <summary>
        /// Rules Check 필요한 정보 저장.
        /// </summary>
        private DataTable _dtRulesInfo;
        /// <summary>
        /// 그리드 Click 유무 확인.
        /// </summary>
        private bool _isGridMainViewClick = false;
        /// <summary>
        /// 관리도 Spec 정의
        /// </summary>
        public ControlSpec ControlSpecData;
        /// <summary>
        /// 룰 Over Message
        /// </summary>
        string _messageRuleResultOver = "";
        /// <summary>
        /// 룰 Over 없음 Message
        /// </summary>
        string _messageRuleResultNormality = "";
        /// <summary>
        /// Spec Option.
        /// </summary>
        public SPCOption spcOption;
        #endregion

        #region 생성자

        /// <summary>
        /// 생성자
        /// </summary>
        public SpcControlRulesResultPopup()
        {
            InitializeComponent();
            InitializeEvent();
            InitializeGrid();

            //this.CancelButton = btnClose;
        }

        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// Chart Raw Data 
        /// </summary>
        private void InitializeGrid()
        {
            #region RowData
            //grdRawData = null;
            grdRuleResult.View.ClearDatas();
            grdRuleResult.View.ClearColumns();
            grdRuleResult.GridButtonItem = GridButtonItem.Export;
            grdRuleResult.View.SetIsReadOnly();
            grdRuleResult.View.AddTextBoxColumn("COLOR", 60).SetLabel("SPCRULESCOLOR");
            grdRuleResult.View.AddTextBoxColumn("RULENO", 60).SetLabel("SPCRULESRULENO").SetTextAlignment(TextAlignment.Center);
            grdRuleResult.View.AddTextBoxColumn("STATUS", 60).SetLabel("SPCRULESSTATUS").SetTextAlignment(TextAlignment.Center);
            grdRuleResult.View.AddTextBoxColumn("POINTSTART", 100).SetLabel("SPCRULESPOINTSTART").SetTextAlignment(TextAlignment.Center);
            grdRuleResult.View.AddTextBoxColumn("POINTEND", 100).SetLabel("SPCRULESPOINTEND").SetTextAlignment(TextAlignment.Center);
            grdRuleResult.View.AddTextBoxColumn("RULECHECKCOUNT", 100).SetLabel("SPCRULESRULECHECKCOUNT").SetTextAlignment(TextAlignment.Center);
            grdRuleResult.View.AddTextBoxColumn("MESSAGE", 300).SetLabel("SPCRULESMESSAGE");
            grdRuleResult.View.AddTextBoxColumn("DISCRIPTION", 500).SetLabel("SPCRULESDISCRIPTION");
            grdRuleResult.View.AddTextBoxColumn("COMMENT", 500).SetLabel("SPCRULESCOMMANT");

            grdRuleResult.View.PopulateColumns();

            //grdRawData.View.Columns[8].AppearanceCell.BackColor = Color.Moccasin; // 적정량 Column 색깔변경
            //grdRawData.View.OptionsView.AllowCellMerge = false; // CellMerge
            //grdRawData.View.FixColumn(new string[] { "PROCESSSEGMENTCLASSNAME", "EQUIPMENTNAME", "STATE", "DEGREE", "CHILDEQUIPMENTNAME", "CHEMICALNAME", "CHEMICALLEVEL", "MANAGEMENTSCOPE" });
            //RepositoryItemTimeEdit edit = new RepositoryItemTimeEdit();
            //edit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            //edit.Mask.EditMask = "([0-1]?[0-9]|2[0-3]):[0-5][0-9]";
            //edit.Mask.UseMaskAsDisplayFormat = true;
            //grdRawData.View.Columns["COLLECTIONTIME"].ColumnEdit = edit;
            #endregion

            #region Detail
            grdRuleDetail.View.ClearDatas();
            grdRuleDetail.View.ClearColumns();
            grdRuleDetail.GridButtonItem = GridButtonItem.Export;
            grdRuleDetail.View.SetIsReadOnly();
            grdRuleDetail.View.AddTextBoxColumn("CHECKNO", 130).SetLabel("SPCRULESVERIFICATIONNO").SetTextAlignment(TextAlignment.Center);
            grdRuleDetail.View.AddTextBoxColumn("INDEX", 130).SetLabel("SPCRULESPOINTNO").SetTextAlignment(TextAlignment.Center);
            grdRuleDetail.View.AddTextBoxColumn("NVALUE", 150).SetLabel("SPCRULESNVALUE").SetTextAlignment(TextAlignment.Right);
            grdRuleDetail.View.AddTextBoxColumn("NVALUEDETAIL", 200).SetLabel("SPCRULESNVALUEDETAIL").SetTextAlignment(TextAlignment.Right);
                                                                               
            grdRuleDetail.View.PopulateColumns();
            #endregion

        
        }
        /// <summary>
        /// Grid 초기화 - 계량형 관리도
        /// </summary>
        private void InitializeGridInfoXBar()
        {
            #region Info
            grdRuleInfo.View.ClearDatas();
            grdRuleInfo.View.ClearColumns();
            grdRuleInfo.GridButtonItem = GridButtonItem.Export;
            grdRuleInfo.View.SetIsReadOnly();
            grdRuleInfo.View.AddTextBoxColumn("MODE", 150).SetLabel("SPCRULESMODE");
            grdRuleInfo.View.AddTextBoxColumn("SIGMA1", 100).SetLabel("SPCRULESSIGMA1").SetTextAlignment(TextAlignment.Right);
            grdRuleInfo.View.AddTextBoxColumn("SIGMA2", 100).SetLabel("SPCRULESSIGMA2").SetTextAlignment(TextAlignment.Right);
            grdRuleInfo.View.AddTextBoxColumn("SIGMA3", 100).SetLabel("SPCRULESSIGMA3").SetTextAlignment(TextAlignment.Right);
            grdRuleInfo.View.AddTextBoxColumn("SIGMADETAIL1", 150).SetLabel("SPCRULESSIGMADETAIL1").SetTextAlignment(TextAlignment.Right);
            grdRuleInfo.View.AddTextBoxColumn("SIGMADETAIL2", 150).SetLabel("SPCRULESSIGMADETAIL2").SetTextAlignment(TextAlignment.Right);
            grdRuleInfo.View.AddTextBoxColumn("SIGMADETAIL3", 150).SetLabel("SPCRULESSIGMADETAIL3").SetTextAlignment(TextAlignment.Right);
            grdRuleInfo.View.AddTextBoxColumn("INFO01", 150).SetLabel("SPCRULESINFO01");
            grdRuleInfo.View.AddTextBoxColumn("INFO02", 150).SetLabel("SPCRULESINFO02");
            grdRuleInfo.View.PopulateColumns();
            #endregion
        }

        /// <summary>
        /// Grid 초기화 - 계수형 관리도
        /// </summary>
        private void InitializeGridInfoPCU()
        {
            #region Info
            grdRuleInfo.View.ClearDatas();
            grdRuleInfo.View.ClearColumns();
            grdRuleInfo.GridButtonItem = GridButtonItem.Export;
            grdRuleInfo.View.SetIsReadOnly();
            grdRuleInfo.View.AddTextBoxColumn("INFO01", 150).SetLabel("SPCRULESINFO01");
            grdRuleInfo.View.AddTextBoxColumn("INFO02", 150).SetLabel("SPCRULESINFO02");
            grdRuleInfo.View.PopulateColumns();
            #endregion
        }
        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            grdRuleResult.View.RowCellStyle += grdRuleResult_RowCellStyle;
            grdRuleResult.View.Click += grdRuleResult_View_Click;
        }
        /// <summary>
        /// 폼 Load 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpcControlRulesResultPopup_Load(object sender, EventArgs e)
        {
            SpcClass.SpcDictionaryDataSetting();
            MessageRead();
            ResultView();
        }
        /// <summary>
        /// 결과 grid Row Click 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdRuleResult_View_Click(object sender, EventArgs e)
        {
            grdRuleResultViewClick();
        }

        private void grdRuleResult_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            string ruleNo = "";
            string status = "";
            GridView view = sender as GridView;

            if (e.RowHandle == view.FocusedRowHandle)
            {
                e.Appearance.ForeColor = Color.Black;
            }
            
            if (e.Column.FieldName == "COLOR")
            {
                ruleNo = grdRuleResult.View.GetRowCellValue(e.RowHandle, "RULENO").ToString();
                switch (ruleNo)
                {
                    case "1":
                        e.Appearance.BackColor = rulesCheckData.Rule01Color;
                        break;
                    case "2":
                        e.Appearance.BackColor = rulesCheckData.Rule02Color;
                        break;
                    case "3":
                        e.Appearance.BackColor = rulesCheckData.Rule03Color;
                        break;
                    case "4":
                        e.Appearance.BackColor = rulesCheckData.Rule04Color;
                        break;
                    case "5":
                        e.Appearance.BackColor = rulesCheckData.Rule05Color;
                        break;
                    case "6":
                        e.Appearance.BackColor = rulesCheckData.Rule06Color;
                        break;
                    case "7":
                        e.Appearance.BackColor = rulesCheckData.Rule07Color;
                        break;
                    case "8":
                        e.Appearance.BackColor = rulesCheckData.Rule08Color;
                        break;
                    default:
                        e.Appearance.BackColor = Color.White;
                        break;
                }
                
            }

            if (e.Column.FieldName == "STATUS")
            {
                status = grdRuleResult.View.GetRowCellValue(e.RowHandle, "STATUS").ToString();
                if (status.ToUpper() == "OVER")
                {
                    e.Appearance.BackColor = Color.Red;
                    e.Appearance.ForeColor = Color.Yellow;
                }

            }
        }

        //private void grdRuleResult_RowCellStyle(object sender, RowCellStyleEventArgs e)
        //{
        //    GridView view = sender as GridView;

        //    if (e.RowHandle == view.FocusedRowHandle)
        //    {
        //        e.Appearance.ForeColor = Color.Black;
        //    }

        //    if (e.Column.FieldName == "PROCESSSEGMENTCLASSNAME" || e.Column.FieldName == "EQUIPMENTNAME"
        //        || e.Column.FieldName == "STATENAME" || e.Column.FieldName == "CHILDEQUIPMENTNAME"
        //        || e.Column.FieldName == "DEGREE" || e.Column.FieldName == "ANALYSISDATE")
        //    {
        //        e.Appearance.BackColor = Color.White;
        //    }
        //    // 규격범위 벗어났을때 색깔
        //    if (grdGenerationHistory.View.GetRowCellValue(e.RowHandle, "ISSPECOUT").Equals("Y"))
        //    {
        //        if (e.Column.FieldName == "TITRATIONQTY" || e.Column.FieldName == "ANALYSISVALUE"
        //            || e.Column.FieldName == "SUPPLEMENTQTY" || e.Column.FieldName == "CHEMICALNAME"
        //            || e.Column.FieldName == "CHEMICALLEVEL" || e.Column.FieldName == "MANAGEMENTSCOPE"
        //            || e.Column.FieldName == "SPECSCOPE")
        //        {
        //            e.Appearance.BackColor = Color.PaleVioletRed;
        //        }
        //    }
        //}


        /// <summary>
        /// 저장
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 닫기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        #endregion

        #region Private Function
        /// <summary>
        /// Meessage 조회.
        /// </summary>
        private void MessageRead()
        {
            //SpcClass.SpcDictionaryDataSetting();
            bool isCheck = SpcDictionary.CaptionDtCheck(SpcDicClassId.CONTROLLABEL);
            if (isCheck)
            {
                _messageRuleResultOver = SpcDictionary.readControl(SpcDicClassId.CONTROLLABEL, "SPCRULESVERIFICATIONRESULTOVER");
                _messageRuleResultNormality = SpcDictionary.readControl(SpcDicClassId.CONTROLLABEL, "SPCRULESVERIFICATIONRESULTNORMALITY");
            }
        }
        /// <summary>
        /// 결과 표시.
        /// </summary>
        private void ResultView()
        {
            int nTotalPointCount = 0;
            int nTotalOverCount = 0;
            double nCenterValue = 0;

            this.lblVerificationCountValue.Text = "0";
            this.lblCheckOverCountValue.Text = "0";
            this.lblXBarValue.Text = "0";
            this.lblXBarValue.ToolTip = "0";
            this.lblTotalPointCountValue.Text = "0";

            _dtRulesResult = CreateDataTable();

            if (rulesCheckData != null)
            {
            }
            else
            {
                return;
            }

            this.lblTotalPointCountValue.Text = rulesCheckData.nPointMaxCount.ToSafeString();

            bool isRuleOverCheck = false;

            foreach (RuleCheckResult item in rulesCheckData.lstRuleCheckResult)
            {
                nTotalPointCount += 1;
                DataRow dataRaw = _dtRulesResult.NewRow();
                dataRaw["COLOR"] = "";
                dataRaw["RULENO"] = item.RuleNo;
                if (item.isRuleOver)
                {
                    isRuleOverCheck = true;
                    nTotalOverCount += 1;
                    dataRaw["STATUS"] = "Over";
                    foreach (RuleCheckSerialsPoint pointValue in item.listPoint)
                    {
                        dataRaw["POINTSTART"] = pointValue.nStartPoint;
                        dataRaw["POINTEND"] = pointValue.nEndPoint;
                    }
                }
                else
                {
                    dataRaw["STATUS"] = "";
                }

                //dataRaw["POINTSTART"] = item.nStartPoint;
                //dataRaw["POINTEND"] = item.nEndPoint;
                //dataRaw["POINTSTARTDATA"] = item.RuleNo;
                dataRaw["RULECHECKCOUNT"] = item.nRuleCheckCount;
                dataRaw["MESSAGE"] = item.message;
                dataRaw["DISCRIPTION"] = item.discription;
                dataRaw["COMMENT"] = item.comment;

                _dtRulesResult.Rows.Add(dataRaw);
                //item.RuleNo = 0;
                //item.isRuleOver = false;
                ////item.listPoint = new List<RuleCheckSerialsPoint>();
                //item.nStartPoint = 0;
                //item.nEndPoint = 0;
                //item.nStartPoint01 = 0;
                //item.nEndPoint01 = 0;

                //item.nRuleCheckCount = 0;
                //item.nRuleCheckCount01 = 0;

                ////item.RuleColor = Color.DarkViolet;

                //item.status = -1;
                //item.message = "";
            }

            //Rules Over Message 표시.
            if (isRuleOverCheck)
            {
                if (this._messageRuleResultOver != "")
                {
                    this.lblVerificationResultsValue.Text = this._messageRuleResultOver;
                }
                else
                {
                    this.lblVerificationResultsValue.Text = "있음";
                }
                this.lblVerificationResultsValue.ForeColor = Color.Red;
                this.lblCheckOverCountValue.ForeColor = Color.Red;
            }
            else
            {
                if (this._messageRuleResultNormality != "")
                {
                    this.lblVerificationResultsValue.Text = this._messageRuleResultNormality;
                }
                else
                {
                    this.lblVerificationResultsValue.Text = "없음";
                }
                this.lblVerificationResultsValue.ForeColor = Color.Black;
                this.lblCheckOverCountValue.ForeColor = Color.Black;
            }


            this.grdRuleResult.DataSource = _dtRulesResult;

            #region Rules 정보 편집
            this._dtRulesInfo = this.CreateDataTableRulesInfo();
            DataRow dataRowInfo01 = _dtRulesInfo.NewRow();
            DataRow dataRowInfo02 = _dtRulesInfo.NewRow();
            DataRow dataRowInfo03 = _dtRulesInfo.NewRow();
            dataRowInfo01["MODE"] = "Sigma";
            dataRowInfo02["MODE"] = "Sigma 상한";
            dataRowInfo03["MODE"] = "Sigma 하한";
            //sia확인 : Rules 정보 편집
            if (ControlSpecData != null && ControlSpecData.sigmaResult != null)
            {
                //중앙값 (XBar)
                if (ControlSpecData.sigmaResult.XBAR != SpcLimit.MIN && ControlSpecData.sigmaResult.XBAR != SpcLimit.MAX)
                {
                    nCenterValue = ControlSpecData.sigmaResult.XBAR;
                    //nCenterValue = ControlSpecData.
                }

                //Sigma 1
                if (ControlSpecData.sigmaResult.nSigma1 != SpcLimit.MIN && ControlSpecData.sigmaResult.nSigma1 != SpcLimit.MAX)
                {
                    dataRowInfo01["SIGMA1"] = ControlSpecData.sigmaResult.nSigma1Round;
                    dataRowInfo01["SIGMADETAIL1"] = ControlSpecData.sigmaResult.nSigma1;
                    if (ControlSpecData.sigmaResult.nSigma1Max != SpcLimit.MIN && ControlSpecData.sigmaResult.nSigma1Max != SpcLimit.MAX)
                    {
                        dataRowInfo02["SIGMA1"] = ControlSpecData.sigmaResult.nSigma1MaxRound;
                        dataRowInfo02["SIGMADETAIL1"] = ControlSpecData.sigmaResult.nSigma1Max;
                    }
                    if (ControlSpecData.sigmaResult.nSigma1Min != SpcLimit.MIN && ControlSpecData.sigmaResult.nSigma1Min != SpcLimit.MAX)
                    {
                        dataRowInfo03["SIGMA1"] = ControlSpecData.sigmaResult.nSigma1MinRound;
                        dataRowInfo03["SIGMADETAIL1"] = ControlSpecData.sigmaResult.nSigma1Min;
                    }
                }

                //Sigma 2
                if (ControlSpecData.sigmaResult.nSigma2 != SpcLimit.MIN && ControlSpecData.sigmaResult.nSigma2 != SpcLimit.MAX)
                {
                    dataRowInfo01["SIGMA2"] = ControlSpecData.sigmaResult.nSigma2Round;
                    dataRowInfo01["SIGMADETAIL2"] = ControlSpecData.sigmaResult.nSigma2;
                    if (ControlSpecData.sigmaResult.nSigma2Max != SpcLimit.MIN && ControlSpecData.sigmaResult.nSigma2Max != SpcLimit.MAX)
                    {
                        dataRowInfo02["SIGMA2"] = ControlSpecData.sigmaResult.nSigma2MaxRound;
                        dataRowInfo02["SIGMADETAIL2"] = ControlSpecData.sigmaResult.nSigma2Max;
                    }
                    if (ControlSpecData.sigmaResult.nSigma2Min != SpcLimit.MIN && ControlSpecData.sigmaResult.nSigma2Min != SpcLimit.MAX)
                    {
                        dataRowInfo03["SIGMA2"] = ControlSpecData.sigmaResult.nSigma2MinRound;
                        dataRowInfo03["SIGMADETAIL2"] = ControlSpecData.sigmaResult.nSigma2Min;
                    }
                }

                //Sigma 3
                if (ControlSpecData.sigmaResult.nSigma3 != SpcLimit.MIN && ControlSpecData.sigmaResult.nSigma3 != SpcLimit.MAX)
                {
                    dataRowInfo01["SIGMA3"] = ControlSpecData.sigmaResult.nSigma3Round;
                    dataRowInfo01["SIGMADETAIL3"] = ControlSpecData.sigmaResult.nSigma3;
                    if (ControlSpecData.sigmaResult.nSigma3Max != SpcLimit.MIN && ControlSpecData.sigmaResult.nSigma3Max != SpcLimit.MAX)
                    {
                        dataRowInfo02["SIGMA3"] = ControlSpecData.sigmaResult.nSigma3MaxRound;
                        dataRowInfo02["SIGMADETAIL3"] = ControlSpecData.sigmaResult.nSigma3Max;
                    }
                    if (ControlSpecData.sigmaResult.nSigma3Min != SpcLimit.MIN && ControlSpecData.sigmaResult.nSigma3Min != SpcLimit.MAX)
                    {
                        dataRowInfo03["SIGMA3"] = ControlSpecData.sigmaResult.nSigma3MinRound;
                        dataRowInfo03["SIGMADETAIL3"] = ControlSpecData.sigmaResult.nSigma3Min;
                    }
                }

                _dtRulesInfo.Rows.Add(dataRowInfo01);
                _dtRulesInfo.Rows.Add(dataRowInfo02);
                _dtRulesInfo.Rows.Add(dataRowInfo03);
                

                this.lblVerificationCountValue.Text = nTotalPointCount.ToSafeString();
                this.lblCheckOverCountValue.Text = nTotalOverCount.ToSafeString();
                this.lblXBarValue.ToolTip = nCenterValue.ToSafeString();
                this.lblXBarValue.Text = Math.Round(nCenterValue,6).ToSafeString();

                _dtRulesInfo.Rows[0]["INFO01"] = string.Format("{0} {1}", lblVerificationCount.Text, nTotalPointCount.ToSafeString());
                _dtRulesInfo.Rows[1]["INFO01"] = string.Format("{0} {1}", lblXBar.Text, nCenterValue.ToSafeString());
                _dtRulesInfo.Rows[2]["INFO01"] = string.Format("{0} {1}", lblTotalPointCount.Text, lblTotalPointCountValue.Text);

                _dtRulesInfo.Rows[0]["INFO02"] = string.Format("{0} {1}", lblCheckOverCount.Text, nTotalOverCount.ToSafeString());
                _dtRulesInfo.Rows[1]["INFO02"] = string.Format("{0}", lblXBarValue.ToolTip);

                this.grdRuleInfo.DataSource = _dtRulesInfo;

            }
            #endregion Rules 정보
            //this.grdRuleResult.View.RowCellStyle
            //_dtRulesResult = _dtRulesResult;

        }
        /// <summary>
        /// Option 처리.
        /// </summary>
        public void RulesModeCheck()
        {
            if (spcOption == null)
            {
                return;
            }

            switch (spcOption.chartType)
            {
                case ControlChartType.XBar_R:
                case ControlChartType.XBar_S:
                case ControlChartType.I_MR:
                case ControlChartType.Merger:
                    this.Text = SpcLibMessage.common.comCpk1047;//Rules 분석 결과 - Nelson rules
                    InitializeGridInfoXBar();
                    break;
                case ControlChartType.np:
                case ControlChartType.p:
                case ControlChartType.c:
                case ControlChartType.u:
                default:
                    this.Text = SpcLibMessage.common.comCpk1048;//Rules 분석 결과 - Western Electric rules
                    InitializeGridInfoPCU();
                    break;
            }
        }
        /// <summary>
        /// 결과 DataTable
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataTable CreateDataTable(string tableName = "dtRulesResult")
        {
            DataTable dt = new DataTable();
            dt.TableName = tableName;
            //Filed
            dt.Columns.Add("COLOR", typeof(string));
            dt.Columns.Add("RULENO", typeof(int));
            dt.Columns.Add("STATUS", typeof(string));
            dt.Columns.Add("POINTSTART", typeof(int));
            dt.Columns.Add("POINTEND", typeof(int));
            dt.Columns.Add("RULECHECKCOUNT", typeof(int));
            //dt.Columns.Add("POINTSTARTDATA", typeof(double));
            //dt.Columns.Add("POINTENDDATA", typeof(double));
            dt.Columns.Add("MESSAGE", typeof(string));
            dt.Columns.Add("DISCRIPTION", typeof(string));
            dt.Columns.Add("COMMENT", typeof(string));
            return dt;
        }
        /// <summary>
        /// Rules 상세 정보 Grid DataTable
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataTable CreateDataTableDetail(string tableName = "dtRulesDetail")
        {
            DataTable dt = new DataTable();
            dt.TableName = tableName;
            //Filed
            dt.Columns.Add("CHECKNO", typeof(int));
            dt.Columns.Add("INDEX", typeof(int));
            dt.Columns.Add("NVALUE", typeof(double));
            dt.Columns.Add("NVALUEDETAIL", typeof(double));
            return dt;
        }
        /// <summary>
        /// Rules 정보 Grid DataTable
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataTable CreateDataTableRulesInfo(string tableName = "dtRulesRulesInfo")
        {
            DataTable dt = new DataTable();
            dt.TableName = tableName;
            //Filed
            dt.Columns.Add("MODE", typeof(string));
            dt.Columns.Add("SIGMA1", typeof(double));
            dt.Columns.Add("SIGMA2", typeof(double));
            dt.Columns.Add("SIGMA3", typeof(double));
            dt.Columns.Add("SIGMADETAIL1", typeof(double));
            dt.Columns.Add("SIGMADETAIL2", typeof(double));
            dt.Columns.Add("SIGMADETAIL3", typeof(double));
            dt.Columns.Add("INFO01", typeof(string));
            dt.Columns.Add("INFO02", typeof(string));

            return dt;
        }
        /// <summary>
        /// 룰 Check Data List
        /// </summary>
        private void RuleCheckDetailDataList()
        {
            if (grdRuleResult.View.FocusedRowHandle < 0)
            {
                this.grdRuleDetail.DataSource = null;
                return;
            }

            int nRuleNo = 0;
            int nPointNo = 0;
            double nPValue = 0;
            DataTable dtDetail = CreateDataTableDetail();
            try
            {
                DataRow rowMain = this.grdRuleResult.View.GetFocusedDataRow();
                nRuleNo = rowMain["RULENO"].ToSafeInt32();
                foreach (RuleCheckResult item in rulesCheckData.lstRuleCheckResult)
                {
                    if (nRuleNo == item.RuleNo)
                    {
                        foreach (RuleCheckSerialsPoint pointValue in item.listPoint)
                        {
                            foreach (RuleCheckSerialsPointData pointData in pointValue.listPointData)
                            {
                                nPointNo = pointData.nindex;
                                nPValue = pointData.nCheckValue.ToSafeDoubleStaMin();
                                DataRow rowDetail = dtDetail.NewRow();
                                rowDetail["CHECKNO"] = nRuleNo;
                                rowDetail["INDEX"] = nPointNo;
                                if (nPValue != SpcLimit.MIN)
                                {
                                    rowDetail["NVALUE"] = Math.Round(nPValue,3);
                                    rowDetail["NVALUEDETAIL"] = nPValue;
                                }
                                dtDetail.Rows.Add(rowDetail);
                            }
                        }
                    }
                }

                this.grdRuleDetail.DataSource = dtDetail;

            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }


        /// <summary>
        /// 그리드 Cell Click시 자료 조회 (발생, 증상, 조치)
        /// </summary>
        private void grdRuleResultViewClick()
        {
            if (_isGridMainViewClick) return;

            _isGridMainViewClick = true;

            try
            {
                if (grdRuleResult.View.FocusedRowHandle < 0) return;

                this.RuleCheckDetailDataList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _isGridMainViewClick = false;
            }
        }
        #endregion



    }



}
