#region using
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.Net.Data;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Grid;
using Micube.SmartMES.Commons;

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

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 포장관리 > 인계등록
    /// 업  무  설  명  : 인계등록
    /// 생    성    자  : 박정훈
    /// 생    성    일  : 2019-07-31
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class PackingTransit : SmartConditionManualBaseForm
    {
        #region ◆ Local Variables |

        #endregion

        #region ◆ 생성자 |
        /// <summary>
        /// 생성자
        /// </summary>
        public PackingTransit()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Form Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Form_Load(object sender, EventArgs e)
        {
            InitializeComboBox();
            InitializeGrid();
        }
        #endregion

        #region ◆ 컨텐츠 영역 초기화 |
        /// <summary>
        /// Control 설정
        /// </summary>
        protected override void InitializeContent()
        {
            base.InitializeContent();

            this.ucDataUpDownBtnCtrl.SourceGrid = this.grdExportPackingList;
            this.ucDataUpDownBtnCtrl.TargetGrid = this.grdExportLotList;

            InitializeEvent();
        }

        #region ▶ 조회조건 설정 |
        /// <summary>
        /// 검색조건 설정
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // 품목
            CommonFunction.AddConditionProductPopup("P_PRODUCTDEFID", 1.5, true, Conditions);

            // 작업장
            CommonFunction.AddConditionAreaByAuthorityPopup("P_AREAID", 2.5, false, Conditions, false, true);

            // Lot
            CommonFunction.AddConditionLotPopup("P_LOTID", 3.5, true, Conditions);
        }

        /// <summary>
        /// 조회조건 설정
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();
        } 
        #endregion

        #region ▶ ComboBox 초기화 |
        /// <summary>
        /// 콤보박스를 초기화 하는 함수
        /// </summary>
        private void InitializeComboBox()
        {
            
        }
        #endregion

        #region ▶ Grid 설정 |
        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            #region - 포장 Grid 설정 |
            grdPackingList.GridButtonItem = GridButtonItem.None;
            grdPackingList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdPackingList.View.SetIsReadOnly();
            grdPackingList.SetIsUseContextMenu(false);
            // CheckBox 설정

            grdPackingList.View.AddTextBoxColumn("PRODUCTDEFID", 100).SetTextAlignment(TextAlignment.Left);
            grdPackingList.View.AddTextBoxColumn("PRODUCTDEFNAME", 250).SetTextAlignment(TextAlignment.Left);
            grdPackingList.View.AddTextBoxColumn("BOXNO", 150).SetTextAlignment(TextAlignment.Center);
            grdPackingList.View.AddTextBoxColumn("WORKER", 150).SetTextAlignment(TextAlignment.Left);
            grdPackingList.View.AddSpinEditColumn("PCSQTY", 70).SetTextAlignment(TextAlignment.Right);
            grdPackingList.View.AddTextBoxColumn("PACKINGDATE", 150).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetTextAlignment(TextAlignment.Center);
            grdPackingList.View.AddTextBoxColumn("PROCESSSEGMENTID", 150).SetTextAlignment(TextAlignment.Left).SetIsHidden();

            grdPackingList.View.PopulateColumns();
            #endregion

            #region - Lot 정보 설정 |
            grdLotList.GridButtonItem = GridButtonItem.None;
            grdLotList.View.SetIsReadOnly();

            grdLotList.View.AddTextBoxColumn("BOXNO", 150).SetTextAlignment(TextAlignment.Center);
			grdLotList.View.AddTextBoxColumn("PARENTLOTID", 250).SetTextAlignment(TextAlignment.Center);
			grdLotList.View.AddTextBoxColumn("LOTID", 250).SetTextAlignment(TextAlignment.Center);
            grdLotList.View.AddTextBoxColumn("PRODUCTDEFID", 100).SetTextAlignment(TextAlignment.Left);
            grdLotList.View.AddTextBoxColumn("PRODUCTDEFNAME", 250).SetTextAlignment(TextAlignment.Left);
            grdLotList.View.AddSpinEditColumn("PCSQTY", 70).SetTextAlignment(TextAlignment.Right);
            grdLotList.View.AddTextBoxColumn("WEEK", 70).SetTextAlignment(TextAlignment.Center);
            grdLotList.View.AddTextBoxColumn("PACKINGWEEK", 70).SetTextAlignment(TextAlignment.Center);

            grdLotList.View.PopulateColumns();
            #endregion

            #region - 수출 포장 Grid 설정 |
            grdExportPackingList.GridButtonItem = GridButtonItem.None;
            grdExportPackingList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdExportPackingList.View.SetIsReadOnly();
            grdExportPackingList.SetIsUseContextMenu(false);
            // CheckBox 설정

            grdExportPackingList.View.AddTextBoxColumn("PRODUCTDEFID", 100).SetTextAlignment(TextAlignment.Center);
            grdExportPackingList.View.AddTextBoxColumn("PRODUCTDEFVERSION", 70).SetTextAlignment(TextAlignment.Center);
            grdExportPackingList.View.AddTextBoxColumn("PRODUCTDEFNAME", 250).SetTextAlignment(TextAlignment.Left);
            grdExportPackingList.View.AddTextBoxColumn("BOXNO", 150).SetTextAlignment(TextAlignment.Center);
            grdExportPackingList.View.AddTextBoxColumn("LOTID", 150).SetTextAlignment(TextAlignment.Center);
            grdExportPackingList.View.AddTextBoxColumn("WORKER", 150).SetTextAlignment(TextAlignment.Left);
            grdExportPackingList.View.AddSpinEditColumn("PCSQTY", 70).SetTextAlignment(TextAlignment.Right);
            grdExportPackingList.View.AddTextBoxColumn("PACKINGDATE", 150).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetTextAlignment(TextAlignment.Center);
            grdExportPackingList.View.AddSpinEditColumn("PROCESSDEFID", 70).SetTextAlignment(TextAlignment.Center).SetIsHidden();
            grdExportPackingList.View.AddSpinEditColumn("PROCESSDEFVERSION", 70).SetTextAlignment(TextAlignment.Center).SetIsHidden();
            grdExportPackingList.View.AddSpinEditColumn("NEXTSEGMENT", 70).SetTextAlignment(TextAlignment.Center).SetIsHidden();
            grdExportPackingList.View.AddSpinEditColumn("NEXTSEGMENTNAME", 70).SetTextAlignment(TextAlignment.Center)
                .SetLabel("NEXTPROCESSSEGMENTNAME");

            grdExportPackingList.View.AddTextBoxColumn("PROCESSSEGMENTID", 150).SetTextAlignment(TextAlignment.Left).SetIsHidden();
            grdExportPackingList.View.PopulateColumns();
            #endregion

            #region - 수출포장 Lot 정보 설정 |
            grdExportLotList.GridButtonItem = GridButtonItem.None;
            grdExportLotList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdExportLotList.View.SetIsReadOnly();

            grdExportLotList.View.AddTextBoxColumn("PRODUCTDEFID", 100).SetTextAlignment(TextAlignment.Center);
            grdExportLotList.View.AddTextBoxColumn("PRODUCTDEFVERSION", 70).SetTextAlignment(TextAlignment.Center);
            grdExportLotList.View.AddTextBoxColumn("PRODUCTDEFNAME", 250).SetTextAlignment(TextAlignment.Left);
            grdExportLotList.View.AddTextBoxColumn("BOXNO", 150).SetTextAlignment(TextAlignment.Center);
            grdExportLotList.View.AddTextBoxColumn("LOTID", 150).SetTextAlignment(TextAlignment.Center);
            grdExportLotList.View.AddTextBoxColumn("WORKER", 150).SetTextAlignment(TextAlignment.Left);
            grdExportLotList.View.AddSpinEditColumn("PCSQTY", 70).SetTextAlignment(TextAlignment.Right);
            grdExportLotList.View.AddTextBoxColumn("PACKINGDATE", 150).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetTextAlignment(TextAlignment.Center);
            grdExportLotList.View.AddSpinEditColumn("PROCESSDEFID", 70).SetTextAlignment(TextAlignment.Center).SetIsHidden();
            grdExportLotList.View.AddSpinEditColumn("PROCESSDEFVERSION", 70).SetTextAlignment(TextAlignment.Center).SetIsHidden();
            grdExportLotList.View.AddSpinEditColumn("NEXTSEGMENT", 70).SetTextAlignment(TextAlignment.Center).SetIsHidden();
            grdExportLotList.View.AddSpinEditColumn("NEXTSEGMENTNAME", 70).SetTextAlignment(TextAlignment.Center)
                .SetLabel("NEXTPROCESSSEGMENTNAME");
            grdExportLotList.View.AddTextBoxColumn("PROCESSSEGMENTID", 150).SetTextAlignment(TextAlignment.Left).SetIsHidden();
            grdExportLotList.View.PopulateColumns();
            #endregion
        }
        #endregion

        #endregion

        #region ◆ Event |

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            this.Load += Form_Load;

            this.grdPackingList.View.FocusedRowChanged += PackingListView_FocusedRowChanged;
            this.grdExportPackingList.View.CheckStateChanged += View_CheckStateChanged;

            // Button Event
            this.ucDataUpDownBtnCtrl.buttonClick += UcDataUpDownBtnCtrl_buttonClick;
        }

        #region ▶ 포장 목록 Focused Changed Event |
        /// <summary>
        /// 포장 목록 Focused Changed Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PackingListView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle < 0)
                return;

            DataRow dr = grdPackingList.View.GetFocusedDataRow();

            if (dr == null) return;

            getPackingLotList(grdLotList, dr);
        }
        #endregion

        #region ▶ Export 포장 Grid Check Event |
        /// <summary>
        /// 재공 Grid Check Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CheckStateChanged(object sender, EventArgs e)
        {
            GridCheckMarksSelection view = (GridCheckMarksSelection)sender;

            DataTable dt = grdExportPackingList.View.GetCheckedRows();

            // 제품 ID 체크
            int productCount = dt.AsEnumerable().Select(r => r.Field<string>("PRODUCTDEFID")).Distinct().Count();

            if (productCount > 1)
            {
                grdExportPackingList.View.CheckRow(grdExportPackingList.View.GetFocusedDataSourceRowIndex(), false);

                // 다른 품목 ID는 선택할 수 없습니다.
                throw MessageException.Create("MixSelectProductDefID");
            }

            // Version 체크
            int versionCount = dt.AsEnumerable().Select(r => r.Field<string>("PRODUCTDEFVERSION")).Distinct().Count();
            if (versionCount > 1)
            {
                grdExportPackingList.View.CheckRow(grdExportPackingList.View.GetFocusedDataSourceRowIndex(), false);

                // 다른 품목 ID는 선택할 수 없습니다.
                throw MessageException.Create("MixSelectProductDefVersion");
            }

            // 후공정 체크
            int plntCount = dt.AsEnumerable().Select(r => r.Field<string>("NEXTSEGMENT")).Distinct().Count();
            if (plntCount > 1)
            {
                grdExportPackingList.View.CheckRow(grdExportPackingList.View.GetFocusedDataSourceRowIndex(), false);

                // 후공정이 일치하지 않습니다.
                throw MessageException.Create("NotSameNextSegment");
            }
        }
        #endregion

        #region ▶ Button Event |
        /// <summary>
        /// Up / Down UserControl Button Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UcDataUpDownBtnCtrl_buttonClick(object sender, EventArgs e)
        {
            if (!this.ucDataUpDownBtnCtrl.ButtonState.Equals("Down")) return;

            DataTable dt = grdExportPackingList.View.GetCheckedRows();
            DataTable tgdt = grdExportLotList.DataSource as DataTable;

            SetTransitArea();

            if (tgdt == null || tgdt.Rows.Count <= 0) return;

            // 제품 ID 체크
            string productdefid = dt.AsEnumerable().Select(r => r.Field<string>("PRODUCTDEFID")).Distinct().FirstOrDefault().ToString();
            string tgproductdefid = tgdt.AsEnumerable().Select(r => r.Field<string>("PRODUCTDEFID")).Distinct().FirstOrDefault().ToString();

            if (!productdefid.Equals(tgproductdefid))
            {
                grdExportPackingList.View.CheckRow(grdExportPackingList.View.GetFocusedDataSourceRowIndex(), false);

                // 다른 품목 ID는 선택할 수 없습니다.
                throw MessageException.Create("MixSelectProductDefID");
            }

            // Version 체크
            string defVersion = dt.AsEnumerable().Select(r => r.Field<string>("PRODUCTDEFVERSION")).Distinct().FirstOrDefault().ToString();
            string tgdefVersion = tgdt.AsEnumerable().Select(r => r.Field<string>("PRODUCTDEFVERSION")).Distinct().FirstOrDefault().ToString();

            if (!defVersion.Equals(tgdefVersion))
            {
                grdExportPackingList.View.CheckRow(grdExportPackingList.View.GetFocusedDataSourceRowIndex(), false);

                // 다른 품목 ID는 선택할 수 없습니다.
                throw MessageException.Create("MixSelectProductDefVersion");
            }

            // 후공정 일치여부
            string nextSegment = dt.AsEnumerable().Select(r => r.Field<string>("NEXTSEGMENT")).Distinct().FirstOrDefault().ToString();
            string tgnextSegment = tgdt.AsEnumerable().Select(r => r.Field<string>("NEXTSEGMENT")).Distinct().FirstOrDefault().ToString();
            if (!nextSegment.Equals(tgnextSegment))
            {
                grdExportPackingList.View.CheckRow(grdExportPackingList.View.GetFocusedDataSourceRowIndex(), false);

                // 후공정이 일치하지 않습니다.
                throw MessageException.Create("NotSameNextSegment");
            }
        }
        #endregion

        #endregion

        #region ◆ 툴바 |

        #region ▶ ToolBar 저장버튼 클릭 |
        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            // 재공실사 진행 여부 체크
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            param.Add("PLANTID", UserInfo.Current.Plant);

            DataTable isWipSurveyResult = SqlExecuter.Query("GetPlantIsWipSurvey", "10001", param);

            if (isWipSurveyResult.Rows.Count > 0)
            {
                DataRow row = isWipSurveyResult.AsEnumerable().FirstOrDefault();

                string isWipSurvey = Format.GetString(row["ISWIPSURVEY"]);

                if (isWipSurvey == "Y")
                {
                    // 재공실사가 진행 중 입니다. {0}을 진행할 수 없습니다.
                    ShowMessage("PLANTINWIPSURVEY", Language.Get(string.Join("_", "MENU", MenuId)));

                    return;
                }
            }

            // TODO : 저장 Rule 변경
            DataTable packingList;

            if (this.tabPacking.SelectedTabPageIndex == 0)
                packingList = grdPackingList.View.GetCheckedRows();
            else
                packingList = grdExportLotList.DataSource as DataTable;

            if (packingList == null || packingList.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }

            string strTransitArea = Format.GetFullTrimString(cboTransitArea.Editor.GetColumnValue("AREAID"));
            string strResourceid = Format.GetFullTrimString(cboTransitArea.Editor.GetColumnValue("RESOURCEID"));

            if (this.tabPacking.SelectedTabPageIndex == 1 && string.IsNullOrWhiteSpace(strTransitArea))
            {
                // 인계처리시 인계작업장을 입력해야합니다.
                throw MessageException.Create("NeedToInputAreaWhenTakeOver");
            }

            ValidateIsSameProductAndVersionForSave(packingList);

            MessageWorker worker = new MessageWorker("SaveBoxPackingDispatch");
            worker.SetBody(new MessageBody()
            {
                { "TransactionType", "SetBoxPackingDispatch" },
                { "EnterpriseID", UserInfo.Current.Enterprise },
                { "PlantID", UserInfo.Current.Plant },
                { "UserId", UserInfo.Current.Id },
                { "TransitArea", strTransitArea },
                { "Resourceid", strResourceid },
                { "Lotlist", packingList }
            });

			IResponse<DataTable> dtResult = worker.Execute<DataTable>();
			DataTable dt = dtResult.GetResultSet();

            //전표
            if (tabPacking.TabIndex == 0)
            {
                PackingPrintDocument(Format.GetString(dt.Rows[0]["DOCUMENTNO"]));
            }
        }

        private void ValidateIsSameProductAndVersionForSave(DataTable dt)
        {
            if (dt.Rows.Count == 0)
            {
                return;
            }
            DataRow standard = dt.Rows[0];
            foreach (DataRow each in dt.Rows)
            {
                if (each["PRODUCTDEFID"].ToString() != standard["PRODUCTDEFID"].ToString())
                {
                    throw MessageException.Create("SameProductDefinition", string.Format("BoxNo={0}", each["BOXNO"].ToString()));
                }
            }
        }
        #endregion

        #region ▶ ToolBar Button Click |
        /// <summary>
        /// ToolBar Button Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            base.OnToolbarClick(sender, e);

            //PackingPrintDocument();
        } 
        #endregion
        #endregion

        #region ◆ 검색 |

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            // 기존 Grid Data 초기화
            grdPackingList.View.ClearDatas();
			grdLotList.View.ClearDatas();
            grdExportPackingList.View.ClearDatas();
            grdExportLotList.View.ClearDatas();

            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable dt;
            
            if(this.tabPacking.SelectedTabPageIndex == 0)
                dt = await SqlExecuter.QueryAsync("SelectPackingList", "10001", values);
            else
                dt = await SqlExecuter.QueryAsync("SelectExportPackingList", "10001", values);

            if (dt.Rows.Count < 1)
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            if (this.tabPacking.SelectedTabPageIndex == 0)
                grdPackingList.DataSource = dt;
            else
                grdExportPackingList.DataSource = dt;
        }

        #endregion

        #region ◆ 유효성 검사 |

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();
        }

        #endregion

        #region ◆ Private Function |

        // TODO : 화면에서 사용할 내부 함수 추가

        #region ▶ 전표출력 |
        /// <summary>
        /// 전표출력
        /// </summary>
        /// <param name="documentNo"></param>
        private void PackingPrintDocument(string documentNo)
        {
            Assembly assembly = Assembly.GetAssembly(this.GetType());
            Stream stream = assembly.GetManifestResourceStream("Micube.SmartMES.ProcessManagement.Report.BoxPackingPrint.repx");

            DataTable dtChecked = grdPackingList.View.GetCheckedRows();
            dtChecked.Columns.Add("DOCUMENTNO", typeof(string));
            dtChecked.Columns.Add("SEQ", typeof(int));

            int seq = 1;
            foreach (DataRow row in dtChecked.Rows)
            {
                row["DOCUMENTNO"] = documentNo;
                row["SEQ"] = seq;

                seq++;
            }

            CommonFunction.PrintBoxPacking(stream, dtChecked);
        }
        #endregion

        #region ▶ Control Data 초기화 |
        /// <summary>
        /// Control Data 초기화
        /// </summary>
        private void SetInitControl()
        {
            // Data 초기화
        }
        #endregion

        #region ▶ 포장 LOT 정보 조회 |
        /// <summary>
        /// 포장 LOT 정보 조회
        /// </summary>
        /// <param name="dr"></param>
        private void getPackingLotList(SmartBandedGrid grd, DataRow dr)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("P_BOXNO", dr["BOXNO"].ToString());

            DataTable dt = SqlExecuter.Query("SelectPackingLotList", "10001", param);

            if (dt == null || dt.Rows.Count == 0)
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                return;
            }

            grd.DataSource = dt;
        }
        #endregion

        #region ▶ 인계작업장 설정
        private void SetTransitArea()
        {
            DataTable dt = grdExportPackingList.View.GetCheckedRows();

            if (dt == null || dt.Rows.Count <= 0) return;

            try
            {
                string strLotId = string.Join(",", dt.AsEnumerable().Select(c => Format.GetString(c["LOTID"])).Distinct());
                string strNextSegment = dt.AsEnumerable().Select(r => r.Field<string>("NEXTSEGMENT")).Distinct().ToList()[0].ToString();

                // 인계작업장
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                param.Add("PLANTID", UserInfo.Current.Plant);
                param.Add("LOTID", strLotId);
                param.Add("PROCESSSEGMENTID", strNextSegment);
                param.Add("RESOURCETYPE", "Resource");
                param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                DataTable transitAreaList = new DataTable();

                cboTransitArea.Visible = true;

                transitAreaList = SqlExecuter.Query("GetTransitAreaList", "10062", param);

                string primaryAreaId = "";

                //후공정이 없으면 현재공정의 작업장을 보여준다.

                for (int i = 0; i < transitAreaList.Rows.Count; i++)
                {
                    DataRow areaRow = transitAreaList.Rows[i];

                    //if (areaRow["ISPRIMARYRESOURCE"].ToString() == "Y")
                    //{
                    //    primaryAreaId = areaRow["AREAID"].ToString();
                    //    break;
                    //}
                }

                cboTransitArea.Editor.PopupWidth = 300;
                cboTransitArea.Editor.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
                cboTransitArea.Editor.ShowHeader = false;
                cboTransitArea.Editor.ValueMember = "RESOURCEID";
                cboTransitArea.Editor.DisplayMember = "RESOURCENAME";
                cboTransitArea.Editor.UseEmptyItem = true;
                cboTransitArea.Editor.EmptyItemValue = "";
                cboTransitArea.Editor.EmptyItemCaption = "";
                cboTransitArea.Editor.DataSource = transitAreaList;
                //cboTransitArea.EditValue = string.IsNullOrEmpty(primaryAreaId) ? cboTransitArea.Editor.EmptyItemValue : primaryAreaId;
                cboTransitArea.EditValue = cboTransitArea.Editor.EmptyItemValue;
            }
            catch
            {

            }
        }
        #endregion
        #endregion
    }
}
