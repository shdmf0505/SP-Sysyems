#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;

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
using DevExpress.XtraEditors.Repository;

using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Micube.SmartMES.Commons;
using Micube.Framework.SmartControls.Grid.BandedGrid;

#endregion

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 수율및 불량 현황 > Affect Lot산정(출)
    /// 업  무  설  명  : Affect LOT 지정한다.
    /// 생    성    자  : 유호석
    /// 생    성    일  : 2019-12-09
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class AffectLot : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가
        #endregion
        ConditionItemSelectPopup processsegmentPopup = null;
        Dictionary<string, object> _parameters = null;
        #region 생성자

        public AffectLot()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 외부에서 호출시 자동 조회
        /// </summary>
        /// <param name="parameters"></param>
        public override void LoadForm(Dictionary<string, object> parameters)
        {
            base.LoadForm(parameters);

            if (parameters != null)
            {
                _parameters = parameters;
                //다른 검색조건을 초기화 하기 떄문에 제일먼저 세팅 한다
                //Conditions.SetValue("P_LOTSTARTDATEFR", 0, string.Empty);
                //Conditions.SetValue("P_LOTSTARTDATETO", 0, string.Empty);
                //Conditions.SetValue("P_SENDTIMEFR", 0, string.Empty);
                //Conditions.SetValue("P_SENDTIMETO", 0, string.Empty);

                string sABNOCRNO = parameters.ContainsKey("ABNOCRNO") ? parameters["ABNOCRNO"].ToString().Replace("*","") : string.Empty;
                string sABNOCRTYPE = parameters.ContainsKey("ABNOCRTYPE") ? parameters["ABNOCRTYPE"].ToString().Replace("*", "") : string.Empty;

                string sCUSTOMER = parameters.ContainsKey("CUSTOMERID") ? parameters["CUSTOMERID"].ToString().Replace("*", "") : string.Empty;
                string sPRODUCTDEFID = parameters.ContainsKey("PRODUCTDEFID") ? parameters["PRODUCTDEFID"].ToString().Replace("*", "") : string.Empty;
                string sPRODUCTDEFVERSION = parameters.ContainsKey("PRODUCTDEFVERSION") ? parameters["PRODUCTDEFVERSION"].ToString().Replace("*", "") : string.Empty;
                string sLOTWORKRESULTDATE = parameters.ContainsKey("LOTWORKRESULTDATE") ? parameters["LOTWORKRESULTDATE"].ToString().Replace("*", "") : string.Empty;
                string sPLANTID = parameters.ContainsKey("PLANTID") ? parameters["PLANTID"].ToString().Replace("*", "") : string.Empty;
                string sPROCESSSEGMENTID = parameters.ContainsKey("PROCESSSEGMENTID") ? parameters["PROCESSSEGMENTID"].ToString().Replace("*", "") : string.Empty;
                string sAREAID = parameters.ContainsKey("AREAID") ? parameters["AREAID"].ToString().Replace("*", "") : string.Empty;
                string sEQUIPMENTCLASSID = parameters.ContainsKey("EQUIPMENTCLASSID") ? parameters["EQUIPMENTCLASSID"].ToString().Replace("*", "") : string.Empty;
                string sRESOURCEID = parameters.ContainsKey("RESOURCEID") ? parameters["RESOURCEID"].ToString().Replace("*", "") : string.Empty;
                string sEQUIPMENTID = parameters.ContainsKey("EQUIPMENTID") ? parameters["EQUIPMENTID"].ToString().Replace("*", "") : string.Empty;
                string sWORKSTARTUSER = parameters.ContainsKey("WORKSTARTUSER") ? parameters["WORKSTARTUSER"].ToString().Replace("*", "") : string.Empty;


                if (sCUSTOMER.Length > 0) Conditions.SetValue("P_CUSTOMER", 0, sCUSTOMER);
                if (sPRODUCTDEFID.Length > 0 && sPRODUCTDEFVERSION.Length > 0)
                {
                    Conditions.SetValue("P_PRODUCTDEFID", 0, sPRODUCTDEFID + "|" + sPRODUCTDEFVERSION);
                }
                DateTime date = DateTime.Now;
                DateTime.TryParse(sLOTWORKRESULTDATE, out date);
                if (sLOTWORKRESULTDATE.Length > 0)//작업일자
                {
                    Conditions.SetValue("P_WORKRESULTDATEFR", 0, date.ToShortDateString());
                    Conditions.SetValue("P_WORKRESULTDATETO", 0, date.ToShortDateString());
                }
                if (sPLANTID.Length > 0) Conditions.SetValue("P_PLANTID", 0, sPLANTID);
                if (sPROCESSSEGMENTID.Length > 0) Conditions.SetValue("P_PROCESSSEGMENTID", 0, sPROCESSSEGMENTID);
                if (sAREAID.Length > 0) Conditions.SetValue("P_AREAID", 0, sAREAID);
                if (sEQUIPMENTCLASSID.Length > 0) Conditions.SetValue("P_EQUIPMENTCLASSID", 0, sEQUIPMENTCLASSID);
                if (sRESOURCEID.Length > 0) Conditions.SetValue("P_RESOURCEID", 0, sRESOURCEID);
                if (sEQUIPMENTID.Length > 0) Conditions.SetValue("P_EQUIPMENTID", 0, sEQUIPMENTID);
                if (sWORKSTARTUSER.Length > 0) Conditions.SetValue("P_WORKSTARTUSER", 0, sWORKSTARTUSER);

                //Conditions.GetControl("P_CUSTOMER").Enabled = false;//고객사
                //Conditions.GetControl("P_PRODUCTDEFID").Enabled = false;//품목코드

                //Conditions.GetControl("P_LOTWORKRESULTDATETYPE").Enabled = false;//작업일자
                //Conditions.GetControl("P_WORKRESULTDATEFR").Enabled = false;//작업일자
                //Conditions.GetControl("P_WORKRESULTDATETO").Enabled = false;//작업일자

                //Conditions.GetControl("P_PLANTID").Enabled = false;
                //Conditions.GetControl("P_PROCESSSEGMENTID").Enabled = false;//표준공정
                //Conditions.GetControl("P_AREAID").Enabled = false;//작업장
                //Conditions.GetControl("P_EQUIPMENTCLASSID").Enabled = false;//설비그룹
                //Conditions.GetControl("P_RESOURCEID").Enabled = false;//자원
                //Conditions.GetControl("P_EQUIPMENTID").Enabled = false;//설비 호기
                //Conditions.GetControl("P_WORKSTARTUSER").Enabled = false;//작업자

                //Conditions.GetControl("P_DURABLECLASSID").Enabled = false;//치공구분류
                //Conditions.GetControl("P_DURABLEDEFID").Enabled = false;//치공구 명
                //Conditions.GetControl("P_DURABLELOTID").Enabled = false;//치공구 NO

                //Conditions.GetControl("P_MATERIALDEFID").Enabled = false;//원자재 명
                //Conditions.GetControl("P_MATERIALLOTID").Enabled = false;//원자재 LOT

                //Conditions.GetControl("P_MATERIALDEFIDPRODUCT").Enabled = false;//반제품 명
                //Conditions.GetControl("P_MATERIALLOTIDPRODUCT").Enabled = false;//반제품 LOT

                OnSearchAsync(); // 조회
                
            }
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
            grdAffectLot.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Restore;
            //grdAffectLot.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdAffectLot.View.AddCheckBoxColumn("AFFECT", 120).SetDefault(false).SetTextAlignment(TextAlignment.Center).SetLabel("ISAFFECTLOT");                           // Affect Lot 산정 여부
            grdAffectLot.View.AddTextBoxColumn("LOTID", 180).SetIsReadOnly();                           // LOT NO
            grdAffectLot.View.AddTextBoxColumn("ROOTLOTID", 130).SetIsReadOnly().SetIsHidden();                       // ROOT LOT NO
            grdAffectLot.View.AddTextBoxColumn("CUSTOMERID", 70).SetIsReadOnly().SetIsHidden();                      // 고객사 ID
            grdAffectLot.View.AddTextBoxColumn("CUSTOMERNAME", 80).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();                    // 고객사명
            grdAffectLot.View.AddTextBoxColumn("PRODUCTDEFNAME", 180).SetIsReadOnly();                  // 품목명
            grdAffectLot.View.AddTextBoxColumn("PRODUCTDEFID", 130).SetIsReadOnly().SetIsHidden();                    // 품목코드
            grdAffectLot.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();               // Rev
            grdAffectLot.View.AddTextBoxColumn("PROCESSDEFID", 80).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();          //라우팅ID
            grdAffectLot.View.AddTextBoxColumn("PROCESSDEFVERSION", 80).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdAffectLot.View.AddTextBoxColumn("PROCESSSEGMENTID", 80).SetIsHidden().SetIsReadOnly();                // 표준공정 ID
            grdAffectLot.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 80).SetIsReadOnly();              //표준공정명
            grdAffectLot.View.AddTextBoxColumn("USERSEQUENCE", 80).SetIsReadOnly();
            grdAffectLot.View.AddTextBoxColumn("SUBPROCESSSEGMENTID", 80).SetIsReadOnly().SetIsHidden();
            grdAffectLot.View.AddTextBoxColumn("SUBUSERSEQUENCE", 70).SetIsReadOnly().SetIsHidden();
            grdAffectLot.View.AddTextBoxColumn("PROCESSSEQUENCE", 70).SetTextAlignment(TextAlignment.Right).SetIsReadOnly();
            grdAffectLot.View.AddTextBoxColumn("PLANTID", 70).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdAffectLot.View.AddTextBoxColumn("AREAID", 80).SetTextAlignment(TextAlignment.Center).SetIsReadOnly().SetIsHidden();
            grdAffectLot.View.AddTextBoxColumn("AREANAME", 100).SetIsReadOnly();
            grdAffectLot.View.AddTextBoxColumn("WORKTYPE", 80).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdAffectLot.View.AddTextBoxColumn("REWORKTYPE", 70).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdAffectLot.View.AddTextBoxColumn("REWORKCOUNT", 80).SetIsReadOnly();
            grdAffectLot.View.AddTextBoxColumn("LOTSTARTDATE", 130).SetTextAlignment(TextAlignment.Center).SetLabel("SEGMENTINPUTDATE").SetIsReadOnly();                    // 투입일시
            grdAffectLot.View.AddTextBoxColumn("RECEIVEDATE", 130).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();                     // 인수일시
            grdAffectLot.View.AddTextBoxColumn("STARTDATE", 130).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();                       // 작업시작일시
            grdAffectLot.View.AddTextBoxColumn("WORKENDDATE", 130).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();                     // 작업완료일시
            grdAffectLot.View.AddTextBoxColumn("LOTSENDDATE", 130).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();                     // 인계일시
            grdAffectLot.View.AddTextBoxColumn("WEEK", 70).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();                            // 주차
            grdAffectLot.View.AddTextBoxColumn("BOXNO", 70).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();                           // BOX NO
            grdAffectLot.View.AddTextBoxColumn("EQUIPMENTCLASSID", 80).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();                // 설비그룹 ID
            grdAffectLot.View.AddTextBoxColumn("EQUIPMENTCLASSNAME", 100).SetIsReadOnly();              // 설비그룹명
            grdAffectLot.View.AddTextBoxColumn("RESOURCEID", 100).SetIsHidden().SetTextAlignment(TextAlignment.Center).SetIsReadOnly();                      // 자원 ID
            grdAffectLot.View.AddTextBoxColumn("RESOURCENAME", 180).SetIsReadOnly();                    // 자원명
            grdAffectLot.View.AddTextBoxColumn("WORKSTARTUSER", 80).SetTextAlignment(TextAlignment.Center).SetLabel("WORKMAN").SetIsReadOnly();                   // 작업자
            grdAffectLot.View.AddTextBoxColumn("DURABLELOTID", 150).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();                    //치공구 NO
            grdAffectLot.View.AddTextBoxColumn("DURABLEDEFID", 130).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();                    //치공구 ID      
            grdAffectLot.View.AddTextBoxColumn("DURABLEDEFNAME", 150).SetIsReadOnly();                  //치공구명
            grdAffectLot.View.AddTextBoxColumn("DURABLECLASSID", 80).SetIsHidden().SetIsReadOnly();                  // 치공구구분
            grdAffectLot.View.AddTextBoxColumn("DURABLECLASSNAME", 80).SetTextAlignment(TextAlignment.Center).SetLabel("DURABLECLASS").SetIsReadOnly();                // 치공구구분명
            grdAffectLot.View.AddTextBoxColumn("RAWMATERIALDEFID", 150).SetLabel("RAWMATERIAL").SetIsReadOnly();                //원자재 ID
            grdAffectLot.View.AddTextBoxColumn("RAWMATERIALDEFNAME", 150).SetIsReadOnly();              //원자재 명
            grdAffectLot.View.AddTextBoxColumn("RAWMATERIALLOTID", 130).SetLabel("RAWMATERIALLOT").SetIsReadOnly();                //원자재 LOT ID
            grdAffectLot.View.AddTextBoxColumn("SUBASSEMDEFID", 130).SetLabel("SEMIPRODUCTCODE").SetIsReadOnly();                   //반제품 ID
            grdAffectLot.View.AddTextBoxColumn("SUBASSEMDEFNAME", 150).SetLabel("SEMIPRODUCT").SetIsReadOnly();                 //반제품 명
            grdAffectLot.View.AddTextBoxColumn("SUBASSEMLOTID", 150).SetLabel("SEMIPRODUCTLOT").SetIsReadOnly();                   //반제품 LOT ID
            grdAffectLot.View.AddTextBoxColumn("EQUIPMENTID", 80).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();                     // 설비 ID
            grdAffectLot.View.PopulateColumns();
        }
        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            btnSave.Click += btnSave_Click;
            grdAffectLot.View.AddingNewRow += View_AddingNewRow;
            grdAffectLot.View.DoubleClick += grdAffectLot_DoubleClick;
            grdAffectLot.View.ShowingEditor += View_ShowingEditor;
        }
        private void View_ShowingEditor(object sender, CancelEventArgs e)
        {
            SmartBandedGridView gridView = sender as SmartBandedGridView;
            DataRow row = grdAffectLot.View.GetFocusedDataRow();
            GridView view = sender as GridView;
            //if (!view.FocusedColumn.FieldName.Equals("AFFECT") || row["AFFECT"].ToString() == "1")
            if (row["AFFECT"].ToString() == "True")
            {
                e.Cancel = true;
                return;
            }
        }
        /// <summary>        
        /// 의뢰접수/재접수 접수 처리
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            DialogResult result = System.Windows.Forms.DialogResult.No;
            
            try
            {
                this.ShowWaitArea();
                btnSave.Enabled = false;

                if (_parameters == null) return;

                string sABNOCRNO = _parameters.ContainsKey("ABNOCRNO") ? _parameters["ABNOCRNO"].ToString() : string.Empty;
                string sABNOCRTYPE = _parameters.ContainsKey("ABNOCRTYPE") ? _parameters["ABNOCRTYPE"].ToString() : string.Empty;

                //DataTable dtAffectLot = grid.View.GetCheckedRows();
                DataTable dtAffectLot = grdAffectLot.GetChangedRows();
                if (!dtAffectLot.Columns.Contains("ABNOCRNO")) dtAffectLot.Columns.Add(new DataColumn("ABNOCRNO", typeof(string)));
                if (!dtAffectLot.Columns.Contains("ABNOCRTYPE")) dtAffectLot.Columns.Add(new DataColumn("ABNOCRTYPE", typeof(string)));
                if (!dtAffectLot.Columns.Contains("ENTERPRISEID")) dtAffectLot.Columns.Add(new DataColumn("ENTERPRISEID", typeof(string)));
                int i = 0;
                foreach (DataRow dr in dtAffectLot.Rows)
                {
                    if (dr["AFFECT"].ToString() == "True")
                    {
                        dr["ABNOCRNO"] = sABNOCRNO;
                        dr["ABNOCRTYPE"] = sABNOCRTYPE;
                        dr["_STATE_"] = "added";
                        dr["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                        i++;
                    }
                }

                if (i == 0)
                {
                    throw MessageException.Create("NoSaveData");
                }

                // CT_RELIABILITYREFMANUFACTURING Table에 저장될 Data
                dtAffectLot.TableName = "list1";

                result = this.ShowMessage(MessageBoxButtons.YesNo, "RegisterAffectLot");//Affect Lot 등록 하시겠습니까?

                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    DataSet rullSet = new DataSet();
                    rullSet.Tables.Add(dtAffectLot);

                    ExecuteRule("SaveAffectLot", rullSet);

                    ShowMessage("SuccessSave");

                    OnSearchAsync(); // 조회
                }
            }
            catch (Exception ex)
            {
                this.ShowError(ex);
            }
            finally
            {
                this.CloseWaitArea();
                btnSave.Enabled = true;
            }
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
        #region 그리드이벤트
        private void grdAffectLot_DoubleClick(object sender, EventArgs e)
        {
            // 등록 팝업
            if (grdAffectLot.View.FocusedRowHandle < 0) return;

            DXMouseEventArgs args = e as DXMouseEventArgs;
            GridView view = sender as GridView;
            GridHitInfo info = view.CalcHitInfo(args.Location);
            DataRow row = this.grdAffectLot.View.GetFocusedDataRow();
            if (info.InRowCell && info.Column.FieldName == "VENDORNAME")
            {
                //AuditManageRegistPopup auditmanagePopup = new AuditManageRegistPopup(row["ENTERPRISEID"].ToString(), row["PLANTID"].ToString(), row["VENDORID"].ToString(), row["AREAID"].ToString(), row["PROCESSSEGMENTID"].ToString(), row["PROCESSSEGMENTVERSION"].ToString());
                AuditManageRegistPopup auditmanagePopup = new AuditManageRegistPopup(row["ENTERPRISEID"].ToString(), row["PLANTID"].ToString(), row["VENDORID"].ToString(), row["AREAID"].ToString(), row["VENDORNAME"].ToString(), row["AREANAME"].ToString());
                auditmanagePopup.StartPosition = FormStartPosition.CenterParent;
                auditmanagePopup.Owner = this;
                DialogManager.CloseWaitArea(pnlContent);

                auditmanagePopup.ShowDialog();
                if (auditmanagePopup.DialogResult == DialogResult.OK)
                {
                    Popup_FormClosed();
                }
            }
        }
        #endregion
        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            //base.OnToolbarSaveClick();

            // TODO : 저장 Rule 변경
            //DataTable changed = grdAffectLot.GetChangedRows();

            //ExecuteRule("SaveAffectLot", changed);

            //DialogManager.ShowWaitArea(pnlContent);
        }

        #endregion

        #region 검색
        private void RemoveColumns()
        {
            //bool check = true;
            //int cnt = grdAffectLot.View.Columns.Count;
            //while (check)
            //{
            //    for (int i = 0; i < grdAffectLot.View.Columns.Count; i++)
            //    {
            //        grdAffectLot.View.Columns.Remove(grdAffectLot.View.Columns[i]);
            //        //grdAffectLot.View.Columns[i].Visible = false;
            //    }
            //    if (grdAffectLot.View.Columns.Count == 0) check = false;
            //}
            for (int i = 0; i < grdAffectLot.View.Columns.Count; i++)
            {
                if(grdAffectLot.View.Columns[i].Name != "AFFECT")//
                    grdAffectLot.View.Columns[i].OwnerBand.Visible = false;
            }
            //grdAffectLot.View.Columns.Clear();
            //grdAffectLot.View.PopulateColumns();
            #region MyRegion
            //grdAffectLot.View.Columns.Remove(grdAffectLot.View.Columns["LOTID"]);              // LOT NO
            //grdAffectLot.View.Columns.Remove(grdAffectLot.View.Columns["ROOTLOTID"]);          // ROOT LOT NO
            //grdAffectLot.View.Columns.Remove(grdAffectLot.View.Columns["CUSTOMERID"]);         // 고객사 ID
            //grdAffectLot.View.Columns.Remove(grdAffectLot.View.Columns["CUSTOMERNAME"]);       // 고객사명
            //grdAffectLot.View.Columns.Remove(grdAffectLot.View.Columns["PRODUCTDEFNAME"]);     // 품목명
            //grdAffectLot.View.Columns.Remove(grdAffectLot.View.Columns["PRODUCTDEFID"]);       // 품목코드
            //grdAffectLot.View.Columns.Remove(grdAffectLot.View.Columns["PRODUCTDEFVERSION"]);  // Rev
            //grdAffectLot.View.Columns.Remove(grdAffectLot.View.Columns["PROCESSDEFID"]);       //라우팅ID
            //grdAffectLot.View.Columns.Remove(grdAffectLot.View.Columns["PROCESSDEFVERSION"]);
            //grdAffectLot.View.Columns.Remove(grdAffectLot.View.Columns["PROCESSSEGMENTID"]);   // 표준공정 ID
            //grdAffectLot.View.Columns.Remove(grdAffectLot.View.Columns["PROCESSSEGMENTNAME"]); //표준공정명
            //grdAffectLot.View.Columns.Remove(grdAffectLot.View.Columns["USERSEQUENCE"]);
            //grdAffectLot.View.Columns.Remove(grdAffectLot.View.Columns["SUBPROCESSSEGMENTID"]);
            //grdAffectLot.View.Columns.Remove(grdAffectLot.View.Columns["SUBUSERSEQUENCE"]);
            //grdAffectLot.View.Columns.Remove(grdAffectLot.View.Columns["PROCESSSEQUENCE"]);
            //grdAffectLot.View.Columns.Remove(grdAffectLot.View.Columns["PLANTID"]);
            //grdAffectLot.View.Columns.Remove(grdAffectLot.View.Columns["AREAID"]);
            //grdAffectLot.View.Columns.Remove(grdAffectLot.View.Columns["AREANAME"]);
            //grdAffectLot.View.Columns.Remove(grdAffectLot.View.Columns["WORKTYPE"]);
            //grdAffectLot.View.Columns.Remove(grdAffectLot.View.Columns["REWORKTYPE"]);
            //grdAffectLot.View.Columns.Remove(grdAffectLot.View.Columns["REWORKCOUNT"]);
            //grdAffectLot.View.Columns.Remove(grdAffectLot.View.Columns["LOTSTARTDATE"]);       // 투입일시
            //grdAffectLot.View.Columns.Remove(grdAffectLot.View.Columns["RECEIVEDATE"]);        // 인수일시
            //grdAffectLot.View.Columns.Remove(grdAffectLot.View.Columns["STARTDATE"]);          // 작업시작일시
            //grdAffectLot.View.Columns.Remove(grdAffectLot.View.Columns["WORKENDDATE"]);        // 작업완료일시
            //grdAffectLot.View.Columns.Remove(grdAffectLot.View.Columns["LOTSENDDATE"]);        // 인계일시
            //grdAffectLot.View.Columns.Remove(grdAffectLot.View.Columns["WEEK"]);               // 주차
            //grdAffectLot.View.Columns.Remove(grdAffectLot.View.Columns["BOXNO"]);              // BOX NO
            //grdAffectLot.View.Columns.Remove(grdAffectLot.View.Columns["EQUIPMENTCLASSID"]);   // 설비그룹 ID
            //grdAffectLot.View.Columns.Remove(grdAffectLot.View.Columns["EQUIPMENTCLASSNAME"]); // 설비그룹명
            //grdAffectLot.View.Columns.Remove(grdAffectLot.View.Columns["RESOURCEID"]);         // 자원 ID
            //grdAffectLot.View.Columns.Remove(grdAffectLot.View.Columns["RESOURCENAME"]);       // 자원명
            //grdAffectLot.View.Columns.Remove(grdAffectLot.View.Columns["WORKSTARTUSER"]);      // 작업자
            //grdAffectLot.View.Columns.Remove(grdAffectLot.View.Columns["DURABLELOTID"]);       //치공구 NO
            //grdAffectLot.View.Columns.Remove(grdAffectLot.View.Columns["DURABLEDEFID"]);       //치공구 ID      
            //grdAffectLot.View.Columns.Remove(grdAffectLot.View.Columns["DURABLEDEFNAME"]);     //치공구명
            //grdAffectLot.View.Columns.Remove(grdAffectLot.View.Columns["DURABLECLASSID"]);     // 치공구구분
            //grdAffectLot.View.Columns.Remove(grdAffectLot.View.Columns["DURABLECLASSNAME"]);   // 치공구구분명
            //grdAffectLot.View.Columns.Remove(grdAffectLot.View.Columns["RAWMATERIALDEFID"]);   //원자재 ID
            //grdAffectLot.View.Columns.Remove(grdAffectLot.View.Columns["RAWMATERIALDEFNAME"]); //원자재 명
            //grdAffectLot.View.Columns.Remove(grdAffectLot.View.Columns["RAWMATERIALLOTID"]);   //원자재 LOT ID
            //grdAffectLot.View.Columns.Remove(grdAffectLot.View.Columns["SUBASSEMDEFID"]);      //반제품 ID
            //grdAffectLot.View.Columns.Remove(grdAffectLot.View.Columns["SUBASSEMDEFNAME"]);    //반제품 명
            //grdAffectLot.View.Columns.Remove(grdAffectLot.View.Columns["SUBASSEMLOTID"]);      //반제품 LOT ID
            //grdAffectLot.View.Columns.Remove(grdAffectLot.View.Columns["EQUIPMENTID"]);        // 설비 ID 
            #endregion
        }
        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            // TODO : 조회 SP 변경
            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            string sP_AFFECTLOTGRIDSETISHIDDEN = values["P_AFFECTLOTGRIDSETISHIDDEN"].ToString();
            string[] sParam = sP_AFFECTLOTGRIDSETISHIDDEN.Split(',');
            if (sParam != null && sParam.Length > 0 && sParam.Where(r => r == "*").ToList().Count() == 0)
            {
                RemoveColumns();

                foreach (string c in sParam)
                {
                    #region MyRegion
                    switch (c)
                    {
                        case "LOTID":
                            grdAffectLot.View.Columns["LOTID"].OwnerBand.Visible = true;
                            break;
                        case "LOTSTARTDATE"://투입일
                            grdAffectLot.View.Columns["LOTSTARTDATE"].OwnerBand.Visible = true;
                            break;
                        case "SENDTIME"://생산입고일
                            break;
                        case "CUSTOMER":
                            grdAffectLot.View.Columns["CUSTOMERNAME"].OwnerBand.Visible = true;
                            break;
                        case "PRODUCTDEF"://품목
                            grdAffectLot.View.Columns["PRODUCTDEFNAME"].OwnerBand.Visible = true;
                            grdAffectLot.View.Columns["PRODUCTDEFVERSION"].OwnerBand.Visible = true;
                            break;
                        case "WEEK":
                            grdAffectLot.View.Columns["WEEK"].OwnerBand.Visible = true;
                            break;
                        case "BOX":
                            grdAffectLot.View.Columns["BOXNO"].OwnerBand.Visible = true;
                            break;
                        case "LOTWORKRESULTDATETYPE"://작업일자
                            grdAffectLot.View.Columns["RECEIVEDATE"].OwnerBand.Visible = true;// 인수일시
                            grdAffectLot.View.Columns["STARTDATE"].OwnerBand.Visible = true;  // 작업시작일시
                            grdAffectLot.View.Columns["WORKENDDATE"].OwnerBand.Visible = true;// 작업완료일시
                            grdAffectLot.View.Columns["LOTSENDDATE"].OwnerBand.Visible = true;// 인계일시

                            break;
                        case "PLANTID":
                            grdAffectLot.View.Columns["PLANTID"].OwnerBand.Visible = true;
                            break;
                        case "PROCESSSEGMENT":
                            grdAffectLot.View.Columns["PROCESSSEGMENTNAME"].OwnerBand.Visible = true;//표준공정명
                            break;
                        case "AREAID":
                            grdAffectLot.View.Columns["AREANAME"].OwnerBand.Visible = true;
                            break;
                        case "EQUIPMENTCLASSID":
                            grdAffectLot.View.Columns["EQUIPMENTCLASSID"].OwnerBand.Visible = true;// 설비그룹 ID
                            grdAffectLot.View.Columns["EQUIPMENTCLASSNAME"].OwnerBand.Visible = true;// 설비그룹명
                            break;
                        case "RESOURCEID":
                            grdAffectLot.View.Columns["RESOURCENAME"].OwnerBand.Visible = true;// 자원명
                            break;
                        case "EQUIPMENTID"://설비(호기)
                            grdAffectLot.View.Columns["EQUIPMENTID"].OwnerBand.Visible = true;
                            break;
                        case "WORKSTARTUSER"://작업자
                            grdAffectLot.View.Columns["WORKSTARTUSER"].OwnerBand.Visible = true;
                            break;
                        case "DURABLECLASSID":
                            grdAffectLot.View.Columns["DURABLECLASSNAME"].OwnerBand.Visible = true;// 치공구구분명
                            break;
                        case "DURABLEDEFID"://치공구 명
                            grdAffectLot.View.Columns["DURABLEDEFID"].OwnerBand.Visible = true;
                            grdAffectLot.View.Columns["DURABLEDEFNAME"].OwnerBand.Visible = true;
                            break;
                        case "DURABLELOTID"://치공구 NO
                            grdAffectLot.View.Columns["DURABLELOTID"].OwnerBand.Visible = true;
                            break;
                        case "MATERIALDEFID"://원자재
                            grdAffectLot.View.Columns["RAWMATERIALDEFID"].OwnerBand.Visible = true;//원자재 ID
                            grdAffectLot.View.Columns["RAWMATERIALDEFNAME"].OwnerBand.Visible = true; //원자재 명
                            break;
                        case "MATERIALLOTID"://원자재 LOT
                            grdAffectLot.View.Columns["RAWMATERIALLOTID"].OwnerBand.Visible = true;
                            break;
                        case "MATERIALDEFIDPRODUCT"://반제품명
                            grdAffectLot.View.Columns["SUBASSEMDEFID"].OwnerBand.Visible = true;                  //반제품 ID
                            grdAffectLot.View.Columns["SUBASSEMDEFNAME"].OwnerBand.Visible = true;                 //반제품 명
                            break;
                        case "MATERIALLOTIDPRODUCT"://반제품 LOT
                            grdAffectLot.View.Columns["SUBASSEMLOTID"].OwnerBand.Visible = true;//반제품 LOT ID
                            break;
                    }
                    #endregion
                }
            }
            else
            {
                grdAffectLot.View.Columns["LOTID"].OwnerBand.Visible = true;                          // LOT NO
                grdAffectLot.View.Columns["ROOTLOTID"].OwnerBand.Visible = false;                        // ROOT LOT NO
                grdAffectLot.View.Columns["CUSTOMERID"].OwnerBand.Visible = false;                       // 고객사 ID
                grdAffectLot.View.Columns["CUSTOMERNAME"].OwnerBand.Visible = true;                    // 고객사명
                grdAffectLot.View.Columns["PRODUCTDEFNAME"].OwnerBand.Visible = true;                  // 품목명
                grdAffectLot.View.Columns["PRODUCTDEFID"].OwnerBand.Visible = false;                      // 품목코드
                grdAffectLot.View.Columns["PRODUCTDEFVERSION"].OwnerBand.Visible = true;               // Rev
                grdAffectLot.View.Columns["PROCESSDEFID"].OwnerBand.Visible = true;          //라우팅ID
                grdAffectLot.View.Columns["PROCESSDEFVERSION"].OwnerBand.Visible = true;
                grdAffectLot.View.Columns["PROCESSSEGMENTID"].OwnerBand.Visible = false;                  // 표준공정 ID
                grdAffectLot.View.Columns["PROCESSSEGMENTNAME"].OwnerBand.Visible = true;              //표준공정명
                grdAffectLot.View.Columns["USERSEQUENCE"].OwnerBand.Visible = true;
                grdAffectLot.View.Columns["SUBPROCESSSEGMENTID"].OwnerBand.Visible = false;
                grdAffectLot.View.Columns["SUBUSERSEQUENCE"].OwnerBand.Visible = false;
                grdAffectLot.View.Columns["PROCESSSEQUENCE"].OwnerBand.Visible = true;
                grdAffectLot.View.Columns["PLANTID"].OwnerBand.Visible = true;
                grdAffectLot.View.Columns["AREAID"].OwnerBand.Visible = false;
                grdAffectLot.View.Columns["AREANAME"].OwnerBand.Visible = true;
                grdAffectLot.View.Columns["WORKTYPE"].OwnerBand.Visible = true;
                grdAffectLot.View.Columns["REWORKTYPE"].OwnerBand.Visible = true;
                grdAffectLot.View.Columns["REWORKCOUNT"].OwnerBand.Visible = false;
                grdAffectLot.View.Columns["LOTSTARTDATE"].OwnerBand.Visible = true;                    // 투입일시
                grdAffectLot.View.Columns["RECEIVEDATE"].OwnerBand.Visible = true;                     // 인수일시
                grdAffectLot.View.Columns["STARTDATE"].OwnerBand.Visible = true;                       // 작업시작일시
                grdAffectLot.View.Columns["WORKENDDATE"].OwnerBand.Visible = true;                    // 작업완료일시
                grdAffectLot.View.Columns["LOTSENDDATE"].OwnerBand.Visible = true;                     // 인계일시
                grdAffectLot.View.Columns["WEEK"].OwnerBand.Visible = true;                            // 주차
                grdAffectLot.View.Columns["BOXNO"].OwnerBand.Visible = true;                           // BOX NO
                grdAffectLot.View.Columns["EQUIPMENTCLASSID"].OwnerBand.Visible = true;                // 설비그룹 ID
                grdAffectLot.View.Columns["EQUIPMENTCLASSNAME"].OwnerBand.Visible = true;              // 설비그룹명
                grdAffectLot.View.Columns["RESOURCEID"].OwnerBand.Visible = false;                        // 자원 ID
                grdAffectLot.View.Columns["RESOURCENAME"].OwnerBand.Visible = true;                    // 자원명
                grdAffectLot.View.Columns["WORKSTARTUSER"].OwnerBand.Visible = true;                   // 작업자
                grdAffectLot.View.Columns["DURABLELOTID"].OwnerBand.Visible = true;                    //치공구 NO
                grdAffectLot.View.Columns["DURABLEDEFID"].OwnerBand.Visible = true;                    //치공구 ID      
                grdAffectLot.View.Columns["DURABLEDEFNAME"].OwnerBand.Visible = true;                  //치공구명
                grdAffectLot.View.Columns["DURABLECLASSID"].OwnerBand.Visible = false;                    // 치공구구분
                grdAffectLot.View.Columns["DURABLECLASSNAME"].OwnerBand.Visible = true;               // 치공구구분명
                grdAffectLot.View.Columns["RAWMATERIALDEFID"].OwnerBand.Visible = true;               //원자재 ID
                grdAffectLot.View.Columns["RAWMATERIALDEFNAME"].OwnerBand.Visible = true;             //원자재 명
                grdAffectLot.View.Columns["RAWMATERIALLOTID"].OwnerBand.Visible = true;               //원자재 LOT ID
                grdAffectLot.View.Columns["SUBASSEMDEFID"].OwnerBand.Visible = true;                  //반제품 ID
                grdAffectLot.View.Columns["SUBASSEMDEFNAME"].OwnerBand.Visible = true;                 //반제품 명
                grdAffectLot.View.Columns["SUBASSEMLOTID"].OwnerBand.Visible = true;                  //반제품 LOT ID
                grdAffectLot.View.Columns["EQUIPMENTID"].OwnerBand.Visible = true;                    // 설비 ID
            }
            if (_parameters != null)
                grdAffectLot.View.Columns["AFFECT"].OwnerBand.Visible = true;
            else
                grdAffectLot.View.Columns["AFFECT"].OwnerBand.Visible = false;
            DataTable dtAffectLot = await QueryAsyncDirect("SelectAffectLotList", "10001", values);
            if (dtAffectLot.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }
            grdAffectLot.DataSource = dtAffectLot;
        }
        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // 2020.03.17-유석진-품목이 조회되면 품목에 해당하는 고객 선택하여 설정 하기 위하여 주석 처리
            //Conditions.GetControl<SmartSelectPopupEdit>("P_CUSTOMER").EditValueChanged += cboCUSTOMER_EditValueChanged;
            //Conditions.GetControl("P_PRODUCTDEFID").Enabled = false;

            Conditions.GetControl("P_CUSTOMER").Enabled = false; // 2020.03.17-유석진-고객사 조건 비활성화
            // 2020.03.17-유석진-품목 정보가 변경되면 고객 정보 설정하기 위하여 이벤트 호출
            Conditions.GetCondition<ConditionItemSelectPopup>("P_PRODUCTDEFID").SetPopupApplySelection((selectedRows, dataGridRows) =>
            {
                List<DataRow> list = selectedRows.ToList<DataRow>();

                Dictionary<string, object> values = new Dictionary<string, object>();
                selectedRows.ForEach(row =>
                {
                    values.Add("P_PRODUCTDEFID", Format.GetString(row["PRODUCTDEFIDVERSION"]));
                }
                );

                DataTable dtCustomer = SqlExecuter.Query("GetAffectLotCustomer", "10001", values);

                if (dtCustomer.Rows.Count > 0)
                {
                    Conditions.GetControl<SmartSelectPopupEdit>("P_CUSTOMER").SetValue(dtCustomer.Rows[0]["CUSTOMERID"]);
                    Conditions.GetControl<SmartSelectPopupEdit>("P_CUSTOMER").EditValue = dtCustomer.Rows[0]["CUSTOMERNAME"];
                }
                else
                {
                    Conditions.GetControl<SmartSelectPopupEdit>("P_CUSTOMER").SetValue("");
                    Conditions.GetControl<SmartSelectPopupEdit>("P_CUSTOMER").EditValue = "";
                }
            });
            // 2020.03.19-유석진-품목정보를 삭제 했을 경우 고객 정보도 삭제하기 위한 이벤트 설정
            if (Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID") is var control)
            {
                control.Properties.ButtonClick += (s, e) =>
                {
                    if ((s as DevExpress.XtraEditors.ButtonEdit).Properties.Buttons.IndexOf(e.Button).Equals(1))
                    {
                        Conditions.GetControl<SmartSelectPopupEdit>("P_CUSTOMER").SetValue(string.Empty);
                        Conditions.GetControl<SmartSelectPopupEdit>("P_CUSTOMER").EditValue = string.Empty;
                    }
                };
            }

            SmartComboBox cboPlant = Conditions.GetControl<SmartComboBox>("P_PLANTID");
            cboPlant.EditValueChanged += cboPlant_EditValueChanged;

            //Conditions.GetControl("P_PLANTID").Enabled = false;
            Conditions.GetControl<SmartComboBox>("P_PLANTID").EditValueChanged += cboPlant_EditValueChanged;

            Conditions.GetControl("P_PROCESSSEGMENTID").Enabled = false;
            Conditions.GetControl<SmartSelectPopupEdit>("P_PROCESSSEGMENTID").EditValueChanged += cboPROCESSSEGMENTID_EditValueChanged;

            Conditions.GetControl("P_AREAID").Enabled = false;//작업장
            Conditions.GetControl<SmartSelectPopupEdit>("P_AREAID").EditValueChanged += cboAREAID_EditValueChanged;

            Conditions.GetControl("P_EQUIPMENTCLASSID").Enabled = false;//설비그룹
            Conditions.GetControl<SmartSelectPopupEdit>("P_EQUIPMENTCLASSID").EditValueChanged += cboEQUIPMENTCLASSID_EditValueChanged;

            Conditions.GetControl("P_RESOURCEID").Enabled = false;//자원
            Conditions.GetControl<SmartSelectPopupEdit>("P_RESOURCEID").EditValueChanged += cboRESOURCEID_EditValueChanged;

            Conditions.GetControl("P_EQUIPMENTID").Enabled = false;//설비 호기
            Conditions.GetControl<SmartSelectPopupEdit>("P_EQUIPMENTID").EditValueChanged += cboEQUIPMENTID_EditValueChanged;

            Conditions.GetControl("P_WORKSTARTUSER").Enabled = false;//작업자

            //치공구분류
            Conditions.GetControl<SmartComboBox>("P_DURABLECLASSID").EditValueChanged += cboDURABLECLASSID_EditValueChanged;

            Conditions.GetControl("P_DURABLEDEFID").Enabled = false;//치공구 명
            Conditions.GetControl<SmartSelectPopupEdit>("P_DURABLEDEFID").EditValueChanged += cboDURABLEDEFID_EditValueChanged;

            Conditions.GetControl("P_DURABLELOTID").Enabled = false;//치공구 NO

            //원자재 명
            Conditions.GetControl<SmartSelectPopupEdit>("P_MATERIALDEFID").EditValueChanged += cboMATERIALDEFID_EditValueChanged;

            Conditions.GetControl("P_MATERIALLOTID").Enabled = false;//원자재 LOT

            //반제품 명
            Conditions.GetControl<SmartSelectPopupEdit>("P_MATERIALDEFIDPRODUCT").EditValueChanged += cboMATERIALDEFIDPRODUCT_EditValueChanged;

            Conditions.GetControl("P_MATERIALLOTIDPRODUCT").Enabled = false;//반제품 LOT
            //P_DURABLECLASSID

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
        }
        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();
            // Lot
            CommonFunction.AddConditionLotHistPopup("P_LOTID", 1, false, Conditions);

            //Conditions.GetCondition<ConditionItemSelectPopup>("P_LOTID").SetValidationIsRequired();

            DateTime dateNow = DateTime.Now;
            string strFirstDate = dateNow.AddDays(-10).ToShortDateString();
            string strEndDate = dateNow.ToShortDateString();
            //투입일
            var deLotStartDateFr = Conditions.AddDateEdit("P_LOTSTARTDATEFR")
               .SetLabel("INPUTDAY")
               .SetDisplayFormat("yyyy-MM-dd")
               .SetPosition(2)
               .SetDefault(strFirstDate);
            //.SetValidationIsRequired();
            var deLotStartDateTo = Conditions.AddDateEdit("P_LOTSTARTDATETO")
               .SetLabel("")
               .SetDisplayFormat("yyyy-MM-dd")
               .SetPosition(3)
               .SetDefault(strEndDate);
            //.SetValidationIsRequired();
            //생산입고일
            var dtSendTimeFr = Conditions.AddDateEdit("P_SENDTIMEFR")
               .SetLabel("PRODUCTINCOMETIME")
               .SetDisplayFormat("yyyy-MM-dd")
               .SetPosition(4)
               .SetDefault(strFirstDate);
            //.SetValidationIsRequired();
            var dtSendTimeTo = Conditions.AddDateEdit("P_SENDTIMETO")
               .SetLabel("")
               .SetDisplayFormat("yyyy-MM-dd")
               .SetPosition(5)
               .SetDefault(strEndDate);
               //.SetValidationIsRequired();
            //고객사
            InitializeCondition_CustomerPopup();
            //품목
            InitializeConditionPopup_Product();
            //MK주차
            InitializeConditionPopup_MKWeek();
            //BOX NO
            InitializeConditionPopup_BoxNo();
            //작업일자
            var dateType = Conditions.AddComboBox("P_LOTWORKRESULTDATETYPE", new SqlQuery("GetCodeList", "00001", "CODECLASSID=LotWorkResultDateType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            dateType.SetLabel("ACTUALDATE");
            dateType.SetPosition(5.5);
            dateType.SetDefault("WORKENDTIME");

            //작업일자
            var deWorkResultDateFr = Conditions.AddDateEdit("P_WORKRESULTDATEFR")
               .SetLabel("")
               .SetDisplayFormat("yyyy-MM-dd")
               .SetPosition(5.6)
               //.SetValidationIsRequired()
               .SetDefault(strFirstDate);
            var deWorkResultDateTo = Conditions.AddDateEdit("P_WORKRESULTDATETO")
               .SetLabel("")
               .SetDisplayFormat("yyyy-MM-dd")
               .SetPosition(5.7)
               //.SetValidationIsRequired()
               .SetDefault(strEndDate);

            //Site
            var cboPlant = Conditions.AddComboBox("P_PLANTID", new SqlQuery("GetPlantList", "10019", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            cboPlant.SetLabel("PLANTID");
            cboPlant.SetPosition(6);
            cboPlant.SetDefault("");
            cboPlant.SetValidationIsRequired();
            cboPlant.SetEmptyItem("", "");

            //표준공정
            InitializeConditionPopup_ProcessSegment();
            //작업장
            InitializeConditionPopup_Area();
            //설비그룹
            InitializeConditionPopup_Equipment();
            //자원
            InitializeConditionPopup_ResourceId();
            //설비 호기
            InitializeConditionPopup_EquipmentId();
            //작업자
            InitializeConditionPopup_WORKSTARTUSER();
            //치공구분류
            InitializeConditionPopup_DURABLECLASSID();
            //치공구 명 
            InitializeConditionPopup_DURABLEDEFID();
            //치공구 명 
            InitializeConditionPopup_DURABLELOTID();
            //원자재 명
            InitializeConditionPopup_MATERIALDEFID();
            //원자재 LOT
            InitializeConditionPopup_MATERIALLOTID();
            //반재품 명
            InitializeConditionPopup_MATERIALDEFIDPRODUCT();
            //빈재품 LOT
            InitializeConditionPopup_MATERIALLOTIDPRODUCT();
        }
        #region InitializeCondition
        /// <summary>
        /// 고객사
        /// </summary>
        private void InitializeCondition_CustomerPopup()
        {
            var conditionCustomer = Conditions.AddSelectPopup("P_CUSTOMER", new SqlQuery("GetCustomerList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "CUSTOMERNAME", "CUSTOMERID")
                .SetPopupLayout("SELECTCUSTOMERID", PopupButtonStyles.Ok_Cancel, true, true)
                .SetPopupLayoutForm(800, 800)
                .SetPopupAutoFillColumns("CUSTOMERNAME")
                .SetLabel("CUSTOMERID")
                .SetPosition(5.1)
                .SetPopupResultCount(1);

            // 팝업 조회조건
            conditionCustomer.Conditions.AddTextBox("TXTCUSTOMERID");

            // 팝업 그리드
            conditionCustomer.GridColumns.AddTextBoxColumn("CUSTOMERID", 150);
            conditionCustomer.GridColumns.AddTextBoxColumn("CUSTOMERNAME", 200);
        }
        /// <summary>
        /// 품목 조회조건
        /// </summary>
        private void InitializeConditionPopup_Product()
        {
            // 팝업 컬럼설정
            var conditionsPopup = Conditions.AddSelectPopup("P_PRODUCTDEFID", new SqlQuery("GetProductListByCustomer", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"P_PLANTID={UserInfo.Current.Plant}"), "PRODUCTDEFNAME", "PRODUCTDEFIDVERSION")
               .SetPopupLayout("PRODUCT", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupLayoutForm(600, 800)
               .SetLabel("PRODUCT")
               .SetPopupAutoFillColumns("PRODUCTDEFNAME")
               .SetPopupResultCount(1)
               .SetPosition(5.2)
               .SetValidationIsRequired()
               .SetRelationIds("P_SENDTIMETO", "P_SENDTIMEFR", "P_LOTSTARTDATETO", "P_LOTSTARTDATEFR");

            // 팝업 조회조건
            conditionsPopup.Conditions.AddTextBox("P_PRODUCTDEFIDNAME").SetLabel("PRODUCTDEFIDNAME");

            // 팝업 그리드
            conditionsPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 120)
                .SetValidationKeyColumn();
            conditionsPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 220);
            conditionsPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 80);
            conditionsPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFIDVERSION", 100)
                .SetIsHidden();
        }
        /// <summary>
        /// MK주차
        /// </summary>
        private void InitializeConditionPopup_MKWeek()
        {
            // 팝업 컬럼설정
            var conditionsPopup = Conditions.AddSelectPopup("P_WEEK", new SqlQuery("GetProductMKWeekByProductdefid", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"P_PLANTID={UserInfo.Current.Plant}"), "WEEK", "WEEK")
               .SetPopupLayout("WEEK", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupLayoutForm(400, 800)
               .SetLabel("WEEK")
               .SetPopupAutoFillColumns("WEEK")
               .SetPopupResultCount(0)
               .SetPosition(5.3)
               .SetRelationIds("P_PRODUCTDEFID", "P_SENDTIMETO", "P_SENDTIMEFR", "P_LOTSTARTDATETO", "P_LOTSTARTDATEFR");

            // 팝업 조회조건
            conditionsPopup.Conditions.AddTextBox("P_WEEK").SetLabel("WEEK");

            // 팝업 그리드
            conditionsPopup.GridColumns.AddTextBoxColumn("WEEK", 200)
                .SetValidationKeyColumn();
        }
        /// <summary>
        /// BOXNO
        /// </summary>
        private void InitializeConditionPopup_BoxNo()
        {
            // 팝업 컬럼설정
            var conditionsPopup = Conditions.AddSelectPopup("P_BOXNO", new SqlQuery("GetProductBoxNoByProductdefid", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"P_PLANTID={UserInfo.Current.Plant}"), "BOXNO", "BOXNO")
               .SetPopupLayout("BOXNO", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupLayoutForm(300, 800)
               .SetLabel("BOXNO")
               .SetPopupAutoFillColumns("BOXNO")
               .SetPopupResultCount(0)
               .SetPosition(5.4)
               .SetRelationIds("P_PRODUCTDEFID", "P_SENDTIMETO", "P_SENDTIMEFR", "P_LOTSTARTDATETO", "P_LOTSTARTDATEFR");

            // 팝업 조회조건
            conditionsPopup.Conditions.AddTextBox("P_BOXNO").SetLabel("BOXNO");

            // 팝업 그리드
            conditionsPopup.GridColumns.AddTextBoxColumn("BOXNO", 120)
                .SetValidationKeyColumn();
        }
        /// <summary>
        /// 표준공정
        /// </summary>
        private void InitializeConditionPopup_ProcessSegment()
        {
            // 팝업 컬럼설정
            var conditionsPopup = Conditions.AddSelectPopup("P_PROCESSSEGMENTID", new SqlQuery("GetProcessSegmentIdByProductdefid", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PROCESSSEGMENNAME", "PROCESSSEGMENTID")
               .SetPopupLayout("ProcessSegment", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupLayoutForm(700, 800)
               .SetLabel("ProcessSegment")
               .SetPopupAutoFillColumns("PROCESSSEGMENNAME")
               .SetPopupResultCount(1)
               .SetPosition(6.1)
               .SetValidationIsRequired()
               .SetRelationIds("P_PRODUCTDEFID", "P_LOTSTARTDATEFR", "P_LOTSTARTDATETO", "P_LOTWORKRESULTDATETYPE", "P_SENDTIMEFR", "P_SENDTIMETO", "P_WORKRESULTDATEFR", "P_WORKRESULTDATETO", "P_PLANTID");//품목/버전,투입일,생산입고일,작업일자 타입,(인수일or작업시작일or작업완료일or인계일),공장

            // 팝업 그리드

            // 팝업 조회조건
            conditionsPopup.Conditions.AddTextBox("P_PROCESSSEGMENTID").SetLabel("ProcessSegment");

            conditionsPopup.GridColumns.AddTextBoxColumn("TOPPROCESSSEGMENTCLASSID", 70);//대공정 ID
            conditionsPopup.GridColumns.AddTextBoxColumn("TOPPROCESSSEGMENTCLASSNAME", 70);//대공정 명
            conditionsPopup.GridColumns.AddTextBoxColumn("MIDPROCESSSEGMENTCLASSID", 70).SetLabel("MIDDLEPROCESSSEGMENTCLASSID");//중공정 ID
            conditionsPopup.GridColumns.AddTextBoxColumn("MIDPROCESSSEGMENTCLASSNAME", 100).SetLabel("MIDDLEPROCESSSEGMENTCLASSNAME");//중공정 명
            conditionsPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 70).SetValidationKeyColumn();//표준공정 ID
            conditionsPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENNAME", 150).SetLabel("PROCESSSEGMENTEXTLIST");//표준공정 명
        }
        /// <summary>
        /// 작업장
        /// </summary>
        private void InitializeConditionPopup_Area()
        {
            // 팝업 컬럼설정
            var conditionsPopup = Conditions.AddSelectPopup("P_AREAID", new SqlQuery("GetAreaIdByProcesssegmentId", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "AREANAME", "AREAID")
               .SetPopupLayout("AREA", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupLayoutForm(430, 800)
               .SetLabel("AREA")
               .SetPopupAutoFillColumns("AREANAME")
               .SetPopupResultCount(1)
               .SetPosition(6.1)
               .SetRelationIds("P_PRODUCTDEFID", "P_LOTSTARTDATEFR", "P_LOTSTARTDATETO", "P_LOTWORKRESULTDATETYPE", "P_SENDTIMEFR", "P_SENDTIMETO", "P_WORKRESULTDATEFR", "P_WORKRESULTDATETO", "P_PLANTID", "P_PROCESSSEGMENTID");//품목/버전,투입일,생산입고일,작업일자 타입,(인수일or작업시작일or작업완료일or인계일),공장

            // 팝업 조회조건
            conditionsPopup.Conditions.AddTextBox("P_AREAID").SetLabel("AREA");

            // 팝업 그리드
            conditionsPopup.GridColumns.AddTextBoxColumn("AREAID", 100).SetValidationKeyColumn();
            conditionsPopup.GridColumns.AddTextBoxColumn("AREANAME", 200);
        }
        /// <summary>
        /// 설비그룹
        /// </summary>
        private void InitializeConditionPopup_Equipment()
        {
            // 팝업 컬럼설정
            var conditionsPopup = Conditions.AddSelectPopup("P_EQUIPMENTCLASSID", new SqlQuery("GetEquipmentClassIdByAreaId", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "EQUIPMENTCLASSNAME", "EQUIPMENTCLASSID")
               .SetPopupLayout("EQUIPMENTCLASSID", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupLayoutForm(430, 800)
               .SetLabel("EQUIPMENTCLASSID")
               .SetPopupAutoFillColumns("EQUIPMENTCLASSNAME")
               .SetPopupResultCount(1)
               .SetPosition(6.2)
               .SetRelationIds("P_PRODUCTDEFID", "P_LOTSTARTDATEFR", "P_LOTSTARTDATETO", "P_LOTWORKRESULTDATETYPE", "P_SENDTIMEFR", "P_SENDTIMETO", "P_WORKRESULTDATEFR", "P_WORKRESULTDATETO", "P_PLANTID", "P_PROCESSSEGMENTID", "P_AREAID");//품목/버전,투입일,생산입고일,작업일자 타입,(인수일or작업시작일or작업완료일or인계일),공장

            // 팝업 조회조건
            conditionsPopup.Conditions.AddTextBox("P_EQUIPMENTCLASSID").SetLabel("EQUIPMENTCLASSID");

            // 팝업 그리드
            conditionsPopup.GridColumns.AddTextBoxColumn("EQUIPMENTCLASSID", 100).SetValidationKeyColumn();
            conditionsPopup.GridColumns.AddTextBoxColumn("EQUIPMENTCLASSNAME", 200);
        }
        /// <summary>
        /// 자원
        /// </summary>
        private void InitializeConditionPopup_ResourceId()
        {
            // 팝업 컬럼설정
            var conditionsPopup = Conditions.AddSelectPopup("P_RESOURCEID", new SqlQuery("GetResourceIdByEquipmentClassId", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "RESOURCENAME", "RESOURCEID")
               .SetPopupLayout("RESOURCEID", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupLayoutForm(430, 800)
               .SetLabel("RESOURCEID")
               .SetPopupAutoFillColumns("RESOURCENAME")
               .SetPopupResultCount(1)
               .SetPosition(6.3)
               .SetRelationIds("P_PRODUCTDEFID", "P_LOTSTARTDATEFR", "P_LOTSTARTDATETO", "P_LOTWORKRESULTDATETYPE", "P_SENDTIMEFR", "P_SENDTIMETO", "P_WORKRESULTDATEFR", "P_WORKRESULTDATETO", "P_PLANTID", "P_PROCESSSEGMENTID", "P_AREAID", "P_EQUIPMENTCLASSID");//품목/버전,투입일,생산입고일,작업일자 타입,(인수일or작업시작일or작업완료일or인계일),공장

            // 팝업 조회조건
            conditionsPopup.Conditions.AddTextBox("P_RESOURCEID").SetLabel("RESOURCEID");

            // 팝업 그리드
            conditionsPopup.GridColumns.AddTextBoxColumn("RESOURCEID", 100).SetValidationKeyColumn();
            conditionsPopup.GridColumns.AddTextBoxColumn("RESOURCENAME", 200);
        }
        /// <summary>
        /// 설비호기
        /// </summary>
        private void InitializeConditionPopup_EquipmentId()
        {
            // 팝업 컬럼설정
            var conditionsPopup = Conditions.AddSelectPopup("P_EQUIPMENTID", new SqlQuery("GetEquipmentidByResourceId", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "EQUIPMENTIDNAME", "EQUIPMENTID")
               .SetPopupLayout("EQUIPMENTID", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupLayoutForm(430, 800)
               .SetLabel("EQUIPMENTID")
               .SetPopupAutoFillColumns("EQUIPMENTIDNAME")
               .SetPopupResultCount(0)
               .SetPosition(6.4)
               .SetRelationIds("P_PRODUCTDEFID", "P_LOTSTARTDATEFR", "P_LOTSTARTDATETO", "P_LOTWORKRESULTDATETYPE", "P_SENDTIMEFR", "P_SENDTIMETO", "P_WORKRESULTDATEFR", "P_WORKRESULTDATETO", "P_PLANTID", "P_PROCESSSEGMENTID", "P_AREAID", "P_EQUIPMENTCLASSID", "P_RESOURCEID");//품목/버전,투입일,생산입고일,작업일자 타입,(인수일or작업시작일or작업완료일or인계일),공장

            // 팝업 조회조건
            conditionsPopup.Conditions.AddTextBox("P_EQUIPMENTID").SetLabel("EQUIPMENTID");
            // 팝업 그리드
            conditionsPopup.GridColumns.AddTextBoxColumn("EQUIPMENTID", 100).SetValidationKeyColumn();
            conditionsPopup.GridColumns.AddTextBoxColumn("EQUIPMENTIDNAME", 200);
        }
        /// <summary>
        /// 작업자
        /// </summary>
        private void InitializeConditionPopup_WORKSTARTUSER()
        {
            // 팝업 컬럼설정
            var conditionsPopup = Conditions.AddSelectPopup("P_WORKSTARTUSER", new SqlQuery("GetWorkstartUserByEquipmentid", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "USERNAME", "WORKSTARTUSER")
               .SetPopupLayout("WORKMAN", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupLayoutForm(400, 800)
               .SetLabel("WORKMAN")
               .SetPopupAutoFillColumns("WORKSTARTUSER")
               .SetPopupResultCount(0)
               .SetPosition(6.5)
               .SetRelationIds("P_PRODUCTDEFID", "P_LOTSTARTDATEFR", "P_LOTSTARTDATETO", "P_LOTWORKRESULTDATETYPE", "P_SENDTIMEFR", "P_SENDTIMETO", "P_WORKRESULTDATEFR", "P_WORKRESULTDATETO", "P_PLANTID", "P_PROCESSSEGMENTID", "P_AREAID", "P_EQUIPMENTCLASSID", "P_RESOURCEID", "P_EQUIPMENTID");//품목/버전,투입일,생산입고일,작업일자 타입,(인수일or작업시작일or작업완료일or인계일),공장

            // 팝업 조회조건
            conditionsPopup.Conditions.AddTextBox("P_WORKSTARTUSER").SetLabel("TXTWORKERIDNAME");
            // 팝업 그리드
            conditionsPopup.GridColumns.AddTextBoxColumn("WORKSTARTUSER", 100).SetValidationKeyColumn().SetLabel("WORKMAN");
            conditionsPopup.GridColumns.AddTextBoxColumn("USERNAME", 100).SetValidationKeyColumn().SetLabel("WORKERNAME");
        }

        /// <summary>
        /// 차공구분류
        /// </summary>
        private void InitializeConditionPopup_DURABLECLASSID()
        {
            var cboDurableclassid = Conditions.AddComboBox("P_DURABLECLASSID", new SqlQuery("GetDurableClassId", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "DURABLECLASSNAME", "DURABLECLASSID");
            cboDurableclassid.SetLabel("DURABLECLASS");
            cboDurableclassid.SetPosition(6.6);
            cboDurableclassid.SetDefault("");
            cboDurableclassid.SetEmptyItem("", "");
            cboDurableclassid.SetRelationIds("P_LOTWORKRESULTDATETYPE", "P_WORKRESULTDATEFR", "P_WORKRESULTDATETO");
        }

        /// <summary>
        /// 치공구 명
        /// </summary>
        private void InitializeConditionPopup_DURABLEDEFID()
        {
            // 팝업 컬럼설정
            var conditionsPopup = Conditions.AddSelectPopup("P_DURABLEDEFID", new SqlQuery("GetdDurableDefIdByDurableClassId", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "DURABLEDEFNAME", "DURABLEDEFID")
               .SetPopupLayout("DURABLEDEFNAME", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupLayoutForm(430, 800)
               .SetLabel("DURABLEDEFNAME")
               .SetPopupAutoFillColumns("DURABLEDEFNAME")
               .SetPopupResultCount(1)
               .SetPosition(6.7)
               .SetRelationIds("P_LOTWORKRESULTDATETYPE", "P_WORKRESULTDATEFR", "P_WORKRESULTDATETO", "P_DURABLECLASSID");

            // 팝업 조회조건
            conditionsPopup.Conditions.AddTextBox("P_DURABLEDEFID").SetLabel("DURABLEDEFNAME");
            // 팝업 그리드
            conditionsPopup.GridColumns.AddTextBoxColumn("DURABLEDEFID", 100).SetValidationKeyColumn();
            conditionsPopup.GridColumns.AddTextBoxColumn("DURABLEDEFNAME", 100).SetValidationKeyColumn();

        }

        /// <summary>
        /// 치공구 NO
        /// </summary>
        private void InitializeConditionPopup_DURABLELOTID()
        {
            // 팝업 컬럼설정
            var conditionsPopup = Conditions.AddSelectPopup("P_DURABLELOTID", new SqlQuery("GetdurablelotidByDurableDefId", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "DURABLELOTID", "DURABLELOTID")
               .SetPopupLayout("DURABLELOTID", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupLayoutForm(430, 800)
               .SetLabel("DURABLELOTID")
               .SetPopupAutoFillColumns("DURABLELOTID")
               .SetPopupResultCount(0)
               .SetPosition(6.8)
               .SetRelationIds("P_LOTWORKRESULTDATETYPE", "P_WORKRESULTDATEFR", "P_WORKRESULTDATETO", "P_DURABLECLASSID", "P_DURABLEDEFID");

            // 팝업 조회조건
            conditionsPopup.Conditions.AddTextBox("P_DURABLELOTID").SetLabel("DURABLELOTIDNO");
            // 팝업 그리드
            conditionsPopup.GridColumns.AddTextBoxColumn("DURABLELOTID", 100).SetValidationKeyColumn().SetLabel("DURABLELOTID");
        }

        /// <summary>
        /// 원자재 명
        /// </summary>
        private void InitializeConditionPopup_MATERIALDEFID()
        {
            // 팝업 컬럼설정
            var conditionsPopup = Conditions.AddSelectPopup("P_MATERIALDEFID", new SqlQuery("GetMaterialDefId", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "MATERIALDEFNAME", "MATERIALDEFID")
               .SetPopupLayout("RAWMATERIAL", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupLayoutForm(430, 800)
               .SetLabel("RAWMATERIAL")
               .SetPopupAutoFillColumns("MATERIALDEFNAME")
               .SetPopupResultCount(1)
               .SetPosition(6.9)
               .SetRelationIds("P_PRODUCTDEFID", "P_LOTSTARTDATEFR", "P_LOTSTARTDATETO", "P_LOTWORKRESULTDATETYPE", "P_SENDTIMEFR", "P_SENDTIMETO", "P_WORKRESULTDATEFR", "P_WORKRESULTDATETO");

            // 팝업 조회조건
            conditionsPopup.Conditions.AddTextBox("P_MATERIALDEFID").SetLabel("RAWMATERIAL");
            // 팝업 그리드
            conditionsPopup.GridColumns.AddTextBoxColumn("MATERIALDEFID", 100).SetValidationKeyColumn();
            conditionsPopup.GridColumns.AddTextBoxColumn("MATERIALDEFNAME", 100).SetValidationKeyColumn();

        }

        /// <summary>
        /// 원자재 LOT
        /// </summary>
        private void InitializeConditionPopup_MATERIALLOTID()
        {
            // 팝업 컬럼설정
            var conditionsPopup = Conditions.AddSelectPopup("P_MATERIALLOTID", new SqlQuery("GetMateriallotidByMaterialDefId", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "MATERIALLOTID", "MATERIALLOTID")
               .SetPopupLayout("RAWMATERIALLOT", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupLayoutForm(430, 800)
               .SetLabel("RAWMATERIALLOT")
               .SetPopupAutoFillColumns("MATERIALLOTID")
               .SetPopupResultCount(0)
               .SetPosition(7)
               .SetRelationIds("P_PRODUCTDEFID", "P_LOTSTARTDATEFR", "P_LOTSTARTDATETO", "P_LOTWORKRESULTDATETYPE", "P_SENDTIMEFR", "P_SENDTIMETO", "P_WORKRESULTDATEFR", "P_WORKRESULTDATETO", "P_MATERIALDEFID");

            // 팝업 조회조건
            conditionsPopup.Conditions.AddTextBox("P_MATERIALLOTID").SetLabel("RAWMATERIALLOT");
            // 팝업 그리드
            conditionsPopup.GridColumns.AddTextBoxColumn("MATERIALLOTID", 100).SetValidationKeyColumn().SetLabel("RAWMATERIALLOT");
        }

        /// <summary>
        /// 반제품명(가공품)
        /// </summary>
        private void InitializeConditionPopup_MATERIALDEFIDPRODUCT()
        {
            // 팝업 컬럼설정
            var conditionsPopup = Conditions.AddSelectPopup("P_MATERIALDEFIDPRODUCT", new SqlQuery("GetProductMaterialLotId", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "MATERIALDEFNAME", "MATERIALDEFID")
               .SetPopupLayout("SEMIPRODUCT", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupLayoutForm(430, 800)
               .SetLabel("SEMIPRODUCT")
               .SetPopupAutoFillColumns("MATERIALDEFNAME")
               .SetPopupResultCount(1)
               .SetPosition(7.1)
               .SetRelationIds("P_PRODUCTDEFID", "P_LOTSTARTDATEFR", "P_LOTSTARTDATETO", "P_LOTWORKRESULTDATETYPE", "P_SENDTIMEFR", "P_SENDTIMETO", "P_WORKRESULTDATEFR", "P_WORKRESULTDATETO");

            // 팝업 조회조건
            conditionsPopup.Conditions.AddTextBox("P_MATERIALDEFIDPRODUCT").SetLabel("SEMIPRODUCT");
            // 팝업 그리드
            conditionsPopup.GridColumns.AddTextBoxColumn("MATERIALDEFID", 100).SetValidationKeyColumn().SetLabel("SEMIPRODUCTCODE");
            conditionsPopup.GridColumns.AddTextBoxColumn("MATERIALDEFNAME", 100).SetLabel("SEMIPRODUCT");

        }

        /// <summary>
        /// 반제품 LOT
        /// </summary>
        private void InitializeConditionPopup_MATERIALLOTIDPRODUCT()
        {
            // 팝업 컬럼설정
            var conditionsPopup = Conditions.AddSelectPopup("P_MATERIALLOTIDPRODUCT", new SqlQuery("GetMaterialLotIdByProductMaterialLotId", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "MATERIALLOTID", "MATERIALLOTID")
               .SetPopupLayout("SEMIPRODUCTLOT", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupLayoutForm(430, 800)
               .SetLabel("SEMIPRODUCTLOT")
               .SetPopupAutoFillColumns("MATERIALLOTID")
               .SetPopupResultCount(0)
               .SetPosition(7.2)
               .SetRelationIds("P_PRODUCTDEFID", "P_LOTSTARTDATEFR", "P_LOTSTARTDATETO", "P_LOTWORKRESULTDATETYPE", "P_SENDTIMEFR", "P_SENDTIMETO", "P_WORKRESULTDATEFR", "P_WORKRESULTDATETO", "P_MATERIALDEFIDPRODUCT");

            // 팝업 조회조건
            conditionsPopup.Conditions.AddTextBox("P_MATERIALLOTIDPRODUCT").SetLabel("SEMIPRODUCTLOT");
            // 팝업 그리드
            conditionsPopup.GridColumns.AddTextBoxColumn("MATERIALLOTID", 100).SetValidationKeyColumn().SetLabel("SEMIPRODUCTLOT");
        } 
        #endregion

        private void Conditions_ValueChanged(object sender, Framework.SmartControls.Condition.ConditionValueChangedEventArgs e)
        {
            processsegmentPopup.SetIsReadOnly(false);
            //throw new NotImplementedException();
        }

        #region EditValueChanged
        /// <summary>
        /// 표준공정 활성화/비활성화
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboPlant_EditValueChanged(object sender, EventArgs e)
        {
            //SmartComboBox cboPlant = sender as SmartComboBox;
            var values = Conditions.GetValues();
            //string sPLANTID = values["P_PLANTID"] == null ? string.Empty : values["P_PLANTID"].ToString();
            string sValue = Conditions.GetControl<SmartComboBox>("P_PLANTID").EditValue == null ? string.Empty : Conditions.GetControl<SmartComboBox>("P_PLANTID").EditValue.ToString();
            //SmartComboBox cboPLANTID = Conditions.GetControl("P_PLANTID") as SmartComboBox;

            if (sValue.Length > 0)
            {
                Conditions.GetControl("P_PROCESSSEGMENTID").Enabled = true;
            }
            else
            {
                Conditions.GetControl("P_PROCESSSEGMENTID").Enabled = false;
                Conditions.GetControl<SmartSelectPopupEdit>("P_PROCESSSEGMENTID").SetValue("");
                Conditions.GetControl<SmartSelectPopupEdit>("P_PROCESSSEGMENTID").EditValue = "";
            }
        }
        /// <summary>
        /// 작업장 활성화/비활성화
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboPROCESSSEGMENTID_EditValueChanged(object sender, EventArgs e)
        {
            var values = Conditions.GetValues();
            string sValue = Conditions.GetControl<SmartSelectPopupEdit>("P_PROCESSSEGMENTID").EditValue == null ? string.Empty : Conditions.GetControl<SmartSelectPopupEdit>("P_PROCESSSEGMENTID").EditValue.ToString();
            if (sValue.Length > 0)
            {
                Conditions.GetControl("P_AREAID").Enabled = true;
            }
            else
            {
                Conditions.GetControl("P_AREAID").Enabled = false;
                Conditions.GetControl<SmartSelectPopupEdit>("P_AREAID").SetValue("");
                Conditions.GetControl<SmartSelectPopupEdit>("P_AREAID").EditValue = "";
            }
        }
        /// <summary>
        ///설비그룹 활성화/비활성화
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboAREAID_EditValueChanged(object sender, EventArgs e)
        {
            var values = Conditions.GetValues();
            string sValue = Conditions.GetControl<SmartSelectPopupEdit>("P_AREAID").EditValue == null ? string.Empty : Conditions.GetControl<SmartSelectPopupEdit>("P_AREAID").EditValue.ToString();
            if (sValue.Length > 0)
            {
                Conditions.GetControl("P_EQUIPMENTCLASSID").Enabled = true;
            }
            else
            {
                Conditions.GetControl("P_EQUIPMENTCLASSID").Enabled = false;
                Conditions.GetControl<SmartSelectPopupEdit>("P_EQUIPMENTCLASSID").SetValue("");
                Conditions.GetControl<SmartSelectPopupEdit>("P_EQUIPMENTCLASSID").EditValue = "";
            }
        }
        /// <summary>
        ///자원 활성화/비활성화
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboEQUIPMENTCLASSID_EditValueChanged(object sender, EventArgs e)
        {
            var values = Conditions.GetValues();
            string sValue = Conditions.GetControl<SmartSelectPopupEdit>("P_EQUIPMENTCLASSID").EditValue == null ? string.Empty : Conditions.GetControl<SmartSelectPopupEdit>("P_EQUIPMENTCLASSID").EditValue.ToString();
            if (sValue.Length > 0)
            {
                Conditions.GetControl("P_RESOURCEID").Enabled = true;
            }
            else
            {
                Conditions.GetControl("P_RESOURCEID").Enabled = false;
                Conditions.GetControl<SmartSelectPopupEdit>("P_RESOURCEID").SetValue("");
                Conditions.GetControl<SmartSelectPopupEdit>("P_RESOURCEID").EditValue = "";
            }
        }

        /// <summary>
        ///설비(호기) 활성화/비활성화
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboRESOURCEID_EditValueChanged(object sender, EventArgs e)
        {
            var values = Conditions.GetValues();
            string sValue = Conditions.GetControl<SmartSelectPopupEdit>("P_RESOURCEID").EditValue == null ? string.Empty : Conditions.GetControl<SmartSelectPopupEdit>("P_RESOURCEID").EditValue.ToString();
            if (sValue.Length > 0)
            {
                Conditions.GetControl("P_EQUIPMENTID").Enabled = true;
            }
            else
            {
                Conditions.GetControl("P_EQUIPMENTID").Enabled = false;
                Conditions.GetControl<SmartSelectPopupEdit>("P_EQUIPMENTID").SetValue("");
                Conditions.GetControl<SmartSelectPopupEdit>("P_EQUIPMENTID").EditValue = "";
            }
        }
        /// <summary>
        ///작업자 활성화/비활성화
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboEQUIPMENTID_EditValueChanged(object sender, EventArgs e)
        {
            var values = Conditions.GetValues();
            string sValue = Conditions.GetControl<SmartSelectPopupEdit>("P_EQUIPMENTID").EditValue == null ? string.Empty : Conditions.GetControl<SmartSelectPopupEdit>("P_EQUIPMENTID").EditValue.ToString();
            if (sValue.Length > 0)
            {
                Conditions.GetControl("P_WORKSTARTUSER").Enabled = true;
            }
            else
            {
                Conditions.GetControl("P_WORKSTARTUSER").Enabled = false;
                Conditions.GetControl<SmartSelectPopupEdit>("P_WORKSTARTUSER").SetValue("");
                Conditions.GetControl<SmartSelectPopupEdit>("P_WORKSTARTUSER").EditValue = "";
            }
        }
        /// <summary>
        ///치공구 명 활성화/비활성화
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboDURABLECLASSID_EditValueChanged(object sender, EventArgs e)
        {
            var values = Conditions.GetValues();
            string sValue = Conditions.GetControl<SmartComboBox>("P_DURABLECLASSID").EditValue == null ? string.Empty : Conditions.GetControl<SmartComboBox>("P_DURABLECLASSID").EditValue.ToString();
            if (sValue.Length > 0)
            {
                Conditions.GetControl("P_DURABLEDEFID").Enabled = true;
            }
            else
            {
                Conditions.GetControl("P_DURABLEDEFID").Enabled = false;
                Conditions.GetControl<SmartSelectPopupEdit>("P_DURABLEDEFID").SetValue("");
                Conditions.GetControl<SmartSelectPopupEdit>("P_DURABLEDEFID").EditValue = "";
            }
        }
        /// <summary>
        ///치공구 NO 활성화/비활성화
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboDURABLEDEFID_EditValueChanged(object sender, EventArgs e)
        {
            var values = Conditions.GetValues();
            string sValue = Conditions.GetControl<SmartSelectPopupEdit>("P_DURABLEDEFID").EditValue == null ? string.Empty : Conditions.GetControl<SmartSelectPopupEdit>("P_DURABLEDEFID").EditValue.ToString();
            if (sValue.Length > 0)
            {
                Conditions.GetControl("P_DURABLELOTID").Enabled = true;
            }
            else
            {
                Conditions.GetControl("P_DURABLELOTID").Enabled = false;
                Conditions.GetControl<SmartSelectPopupEdit>("P_DURABLELOTID").SetValue("");
                Conditions.GetControl<SmartSelectPopupEdit>("P_DURABLELOTID").EditValue = "";
            }
        }

        /// <summary>
        ///원자재 LOT 활성화/비활성화
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboMATERIALDEFID_EditValueChanged(object sender, EventArgs e)
        {
            var values = Conditions.GetValues();
            string sValue = Conditions.GetControl<SmartSelectPopupEdit>("P_MATERIALDEFID").EditValue == null ? string.Empty : Conditions.GetControl<SmartSelectPopupEdit>("P_MATERIALDEFID").EditValue.ToString();
            if (sValue.Length > 0)
            {
                Conditions.GetControl("P_MATERIALLOTID").Enabled = true;
            }
            else
            {
                Conditions.GetControl("P_MATERIALLOTID").Enabled = false;
                Conditions.GetControl<SmartSelectPopupEdit>("P_MATERIALLOTID").SetValue("");
                Conditions.GetControl<SmartSelectPopupEdit>("P_MATERIALLOTID").EditValue = "";
            }
        }

        /// <summary>
        ///반재품 LOT 활성화/비활성화
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboMATERIALDEFIDPRODUCT_EditValueChanged(object sender, EventArgs e)
        {
            var values = Conditions.GetValues();
            string sValue = Conditions.GetControl<SmartSelectPopupEdit>("P_MATERIALDEFIDPRODUCT").EditValue == null ? string.Empty : Conditions.GetControl<SmartSelectPopupEdit>("P_MATERIALDEFIDPRODUCT").EditValue.ToString();
            if (sValue.Length > 0)
            {
                Conditions.GetControl("P_MATERIALLOTIDPRODUCT").Enabled = true;
            }
            else
            {
                Conditions.GetControl("P_MATERIALLOTIDPRODUCT").Enabled = false;
                Conditions.GetControl<SmartSelectPopupEdit>("P_MATERIALLOTIDPRODUCT").SetValue("");
                Conditions.GetControl<SmartSelectPopupEdit>("P_MATERIALLOTIDPRODUCT").EditValue = "";
            }
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
            grdAffectLot.View.CheckValidation();

            DataTable changed = grdAffectLot.GetChangedRows();

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
        
        //popup이 닫힐때 그리드를 재조회 하는 이벤트 (팝업결과 저장 후 재조회)
        private async void Popup_FormClosed()
        {
            await OnSearchAsync();
        }
        #endregion
    }
}
