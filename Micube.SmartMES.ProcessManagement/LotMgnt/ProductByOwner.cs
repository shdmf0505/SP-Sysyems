#region using

using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraLayout.Utils;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion using

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 공정관리 > 재공관리 > 담당자별 모델 등록
    /// 업  무  설  명  : 담당자별 모델 등록/수정/수정, 품목별 진행현황의 담당자와 연계
    /// 생    성    자  : 오근영
    /// 생    성    일  : 2021-02-01
    /// 수  정  이  력  :
    ///
    ///
    /// </summary>
    public partial class ProductByOwner : SmartConditionManualBaseForm
    {
        #region Local Variables

        /// <summary>
        /// SelectPopup에 들어가는 parameter 전역 변수
        /// </summary>
        private readonly Dictionary<string, object> _param = new Dictionary<string, object>
        {
            { "LANGUAGETYPE", UserInfo.Current.LanguageType },
            { "ENTERPRISEID", UserInfo.Current.Enterprise },
            { "PLANTID", UserInfo.Current.Plant },
            { "P_PRODUCTDEFTYPE", "Product" }
        };

        #endregion Local Variables

        #region 생성자

        public ProductByOwner()
        {
            InitializeComponent();
        }

        #endregion 생성자

        #region 컨텐츠 영역 초기화

        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeGrid();
            InitializeEvent();
            InitializeLanguageKey();

            // 초기 기준정보 Tab일 경우 조회기간은 표시하지 않는다.
            SetConditionVisiblility("P_PERIOD", LayoutVisibility.Never);
            tabMain.TabPages[0].PageVisible = true;
            tabMain.TabPages[1].PageVisible = false;
        }

        /// <summary>
        /// Language Key 초기화
        /// </summary>
        private void InitializeLanguageKey()
        {
            tabMain.SetLanguageKey(tabPageStandard, "STANDARD");
            tabMain.SetLanguageKey(tabPageHistory, "CHANGECONTENTS");
            grdMain.LanguageKey = "STANDARD";
            grdHistory.LanguageKey = "CHANGECONTENTS";
        }

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            #region    기준정보

            grdMain.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export;
            grdMain.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            #region    01.품목 코드(PRODUCTDEFID) 선택 팝업

            var condition = grdMain.View.AddSelectPopupColumn("PRODUCTDEFID", 100, new SqlQuery("SelectProductByOwnerListProductPopup", "10001", _param))
                                    .SetPopupLayout("PRODUCTNAMEANDNO", PopupButtonStyles.Ok_Cancel, true, false)
                                    .SetPopupResultCount(0)
                                    .SetPopupLayoutForm(900, 600, FormBorderStyle.FixedToolWindow)
                                    .SetValidationKeyColumn()
                                    .SetValidationIsRequired()
                                    .SetTextAlignment(TextAlignment.Left)
                                    .SetPopupAutoFillColumns("PRODUCTDEFNAME")
                                    .SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
                                    {
                                        if (selectedRow.Count() > 0)
                                        {
                                            DataTable dt2 = this.grdMain.DataSource as DataTable;
                                            int handle = this.grdMain.View.FocusedRowHandle;

                                            DataRow operationRow = this.grdMain.View.GetFocusedDataRow();

                                            // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
                                            // dataGridRow : 현재 Focus가 있는 그리드의 DataRow
                                            foreach (DataRow row in selectedRow)
                                            {
                                                DataRow dr = null;


                                                DataTable dtResource = this.grdMain.DataSource as DataTable;
                                                DataRow[] rowR = dtResource.Select("1=1", "");


                                                if (dt2.Rows.Count < handle + 1)
                                                {
                                                    dr = dt2.NewRow();
                                                    dt2.Rows.Add(dr);
                                                }
                                                else
                                                {
                                                    dr = this.grdMain.View.GetDataRow(handle);
                                                }

                                                dr["PRODUCTDEFID"] = row["PRODUCTDEFID"].ToString();
                                                dr["PRODUCTDEFVERSION"] = row["PRODUCTDEFVERSION"].ToString();
                                                dr["PRODUCTDEFNAME"] = row["PRODUCTDEFNAME"].ToString();
                                                dr["PRODUCTIONTYPE"] = row["PRODUCTIONTYPE"].ToString();
                                                dr["PRODUCTIONTYPENAME"] = row["PRODUCTIONTYPENAME"].ToString();
                                                dr["PRODUCTSHAPE"] = row["PRODUCTSHAPE"].ToString();
                                                dr["LAYER"] = row["LAYER"].ToString();
                                                dr["LAYER_NAME"] = row["LAYER_NAME"].ToString();
                                                if (Conditions.GetControl<SmartSelectPopupEdit>("P_OWNER").GetValue().ToString() == "")
                                                {
                                                    dr["OWNER"] = UserInfo.Current.Id;
                                                    dr["OWNERNAME"] = UserInfo.Current.Name;
                                                }
                                                else
                                                {
                                                    dr["OWNER"] = Conditions.GetControl<SmartSelectPopupEdit>("P_OWNER").GetValue();
                                                    dr["OWNERNAME"] = Conditions.GetControl<SmartSelectPopupEdit>("P_OWNER").Text;
                                                }
                                                dr["CREATEDTIME"] = row["CREATEDTIME"].ToString();

                                                this.grdMain.View.RaiseValidateRow(handle);

                                                handle++;
                                            }
                                        }
                                    });
            #region    팝업검색조건
            Dictionary<string, object> _paramCombo = new Dictionary<string, object>
            {
                { "CODECLASSID", "ProductDivision2" },
                { "LANGUAGETYPE", UserInfo.Current.LanguageType },
                { "CODEID", "Product,SubAssembly" }
            };

            condition.Conditions.AddComboBox("PRODUCTDEFTYPE", new SqlQuery("SelectProductByOwnerListProductDefTypeCombo", "00001", _paramCombo), "CODENAME", "CODEID")
                .SetDefault("Product");                                         //팝업검색조건 : 1.품목구분 ==> 제품, 반제품
            condition.Conditions.AddTextBox("PRODUCTDEFID");                    //팝업검색조건 : 2.품목코드 ==> A300025-C
            condition.Conditions.AddTextBox("PRODUCTDEFNAME");                  //팝업검색조건 : 3.품목명   ==> E1-R
            #endregion 팝업검색조건
            #region    팝업검색결과 그리드
            //condition.GridColumns.AddTextBoxColumn("ROWNUM", 100)
            //    .SetIsReadOnly()
            //    ;
            condition.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 100);        //팝업검색결과그리드 : 1.품목코드   ==> A300025-C
            condition.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 80);    //팝업검색결과그리드 : 2.내부Rev    ==> 4
            condition.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 150);      //팝업검색결과그리드 : 3.품목명     ==> E1-R
            condition.GridColumns.AddTextBoxColumn("PRODUCTIONTYPE", 150)
                .SetIsHidden()
                //.SetIsReadOnly()
                ;
            condition.GridColumns.AddTextBoxColumn("PRODUCTIONTYPENAME", 80)
                .SetLabel("LOTPRODUCTTYPE");                                    //팝업검색결과그리드 : 4.양산구분   ==> 양산
            condition.GridColumns.AddTextBoxColumn("PRODUCTSHAPE", 150)
                .SetIsHidden()
                //.SetIsReadOnly()
                ;
            condition.GridColumns.AddTextBoxColumn("LAYER", 150)
                .SetIsHidden()
                //.SetIsReadOnly()
                ;
            condition.GridColumns.AddTextBoxColumn("LAYER_NAME", 80)
                .SetLabel("LAYER");                                             //팝업검색결과그리드 : 5.층수       ==> RF 09
            condition.GridColumns.AddTextBoxColumn("OWNERFACTORYID", 100)
                .SetLabel("MAINFACTORY");                                       //팝업검색결과그리드 : 6.주제조공장 ==> K2
            condition.GridColumns.AddTextBoxColumn("PRODUCTDEFTYPE", 150)
                .SetIsHidden()
                //.SetIsReadOnly()
                ;
            condition.GridColumns.AddTextBoxColumn("PRODUCTDEFTYPENAME", 150)
                .SetIsHidden()
                //.SetIsReadOnly()
                .SetLabel("PRODUCTDEFTYPE")
                ;
            condition.GridColumns.AddTextBoxColumn("CREATEDTIME", 150)
                .SetIsHidden()
                //.SetIsReadOnly()
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                ;
            #endregion 팝업검색결과 그리드
            #endregion 01.품목코드(productdefid) 선택 팝업
            #region    02.내부Rev(PRODUCTDEFVERSION)
            grdMain.View.AddTextBoxColumn("PRODUCTDEFVERSION", 130)
                .SetIsReadOnly()
                .SetValidationIsRequired()
                ;
            #endregion 02.내부Rev(productdefversion)
            #region    03.품목명(PRODUCTDEFNAME)
            grdMain.View.AddTextBoxColumn("PRODUCTDEFNAME", 130)
                .SetIsReadOnly()
                ;
            #endregion 03.품목명(productdefname)
            #region    04.생산구분(producttype) hidden
            grdMain.View.AddTextBoxColumn("PRODUCTIONTYPE", 130)
                .SetIsHidden()
                //.SetIsReadOnly()
                ;
            #endregion 04.생산구분(producttype) hidden
            #region    05.생산구분명(producttypename)
            grdMain.View.AddTextBoxColumn("PRODUCTIONTYPENAME", 130)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("PRODUCTIONTYPE")
                ;
            #endregion 05.생산구분명(producttypename)
            #region    06.제품형태(productshape) hidden
            grdMain.View.AddTextBoxColumn("PRODUCTSHAPE", 130)
                .SetIsHidden()
                //.SetIsReadOnly()
                ;
            #endregion 06.제품형태(productshape) hidden
            #region    07.층수(layer) hidden
            grdMain.View.AddTextBoxColumn("LAYER", 130)
                .SetIsHidden()
                //.SetIsReadOnly()
                ;
            #endregion 07.층수(layer) hidden
            #region    08.층수명(layer_name)
            grdMain.View.AddTextBoxColumn("LAYER_NAME", 130)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("LAYER")
                ;
            #endregion 06.층수명(layer_name)
            #region    07.담당자(owner) hidden
            grdMain.View.AddTextBoxColumn("OWNER", 130)
                .SetIsHidden()
                .SetValidationIsRequired()
                //.SetIsReadOnly()
                ;
            #endregion 07.담당자(owner) hidden
            #region    08.담당자명(OWNERNAME)
            grdMain.View.AddTextBoxColumn("OWNERNAME", 130)
                .SetIsReadOnly()
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Center);
            #endregion 08.담당자명(username)
            #region    09.적용일자(createdtime)
            grdMain.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetIsReadOnly()
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetTextAlignment(TextAlignment.Center)
                ;
            #endregion 09.적용일자(createdtime)

            grdMain.View.PopulateColumns();
            grdMain.ShowStatusBar = true;

            #endregion 기준정보

            #region 변경 내용

            grdHistory.GridButtonItem = GridButtonItem.Export;
            grdHistory.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            grdHistory.View.AddComboBoxColumn("TXNID", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=TxnIDState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetTextAlignment(TextAlignment.Left);
            grdHistory.View.AddTextBoxColumn("PRODUCTDEFID", 100).SetTextAlignment(TextAlignment.Left);
            grdHistory.View.AddTextBoxColumn("PRODUCTDEFVERSION", 150).SetIsReadOnly().SetTextAlignment(TextAlignment.Left);
            grdHistory.View.AddTextBoxColumn("PRODUCTDEFNAME", 150).SetTextAlignment(TextAlignment.Left);
            grdHistory.View.AddTextBoxColumn("PRODUCTIONTYPE", 150).SetIsHidden();
            grdHistory.View.AddTextBoxColumn("PRODUCTIONTYPENAME", 100).SetLabel("CONSUMABLEDEFID").SetTextAlignment(TextAlignment.Left);
            grdHistory.View.AddTextBoxColumn("PRODUCTSHAPE", 150).SetIsHidden();
            grdHistory.View.AddTextBoxColumn("LAYER", 150).SetIsHidden();
            grdHistory.View.AddTextBoxColumn("LAYER_NAME", 120).SetTextAlignment(TextAlignment.Left);
            grdHistory.View.AddTextBoxColumn("OWNER", 150).SetIsHidden();
            grdHistory.View.AddComboBoxColumn("OWNERNAME", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ReceivePayOutUnit", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetTextAlignment(TextAlignment.Left);
            grdHistory.View.AddTextBoxColumn("CREATOR", 80).SetIsHidden().SetTextAlignment(TextAlignment.Center);
            grdHistory.View.AddTextBoxColumn("CREATEDTIME", 130).SetIsHidden().SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetTextAlignment(TextAlignment.Center);
            grdHistory.View.AddTextBoxColumn("MODIFIER", 80).SetTextAlignment(TextAlignment.Center);
            grdHistory.View.AddTextBoxColumn("MODIFIEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetTextAlignment(TextAlignment.Center);

            grdHistory.View.PopulateColumns();
            grdHistory.View.SetIsReadOnly();

            grdHistory.ShowStatusBar = true;

            #endregion 변경 내용
        }

        #endregion 컨텐츠 영역 초기화

        #region Event

        /// <summary>
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            // Tab Page 변경 이벤트
            tabMain.SelectedPageChanged += (s, e) =>
            {
                SetConditionVisiblility("P_PERIOD", e.Page.Equals(tabPageStandard) ? LayoutVisibility.Never : LayoutVisibility.Always);
                GetToolbarButtonById("Save").Visible = e.Page.Equals(tabPageStandard) ? true : false;
            };

            // Grid에 신규Row 발생시 이벤트
            grdMain.View.InitNewRow += (s, e) =>
            {
                grdMain.View.SetRowCellValue(e.RowHandle, "OWNER", UserInfo.Current.Id);
                grdMain.View.SetRowCellValue(e.RowHandle, "OWNERNAME", UserInfo.Current.Name);
            };

            //grdMain.View.Columns.ForEach(control =>
            //{
            //    if (!control.ColumnEdit.GetType().Name.Equals("RepositoryItemButtonEdit"))
            //    {
            //        return;
            //    }

            //    // Grid에 공정그룹, 품목코드, 설비ID, 자재ID 삭제버튼 클릭시 이벤트
            //    (control.ColumnEdit as RepositoryItemButtonEdit).ButtonClick += (s, e) =>
            //    {
            //        if (e.Button.Kind.Equals(ButtonPredefines.Clear))
            //        {
            //            switch (control.FieldName)
            //            {
            //                case "PROCESSSEGMENTCLASSID":
            //                    grdMain.View.SetRowCellValue(grdMain.View.GetFocusedDataSourceRowIndex(), "PROCESSSEGMENTCLASSNAME", string.Empty);
            //                    break;

            //                case "PRODUCTDEFID":
            //                    grdMain.View.SetRowCellValue(grdMain.View.GetFocusedDataSourceRowIndex(), "PRODUCTDEFNAME", string.Empty);
            //                    break;

            //                case "EQUIPMENTID":
            //                    grdMain.View.SetRowCellValue(grdMain.View.GetFocusedDataSourceRowIndex(), "EQUIPMENTNAME", string.Empty);
            //                    grdMain.View.SetRowCellValue(grdMain.View.GetFocusedDataSourceRowIndex(), "AREANAME", string.Empty);
            //                    break;

            //                case "MATERIALID":
            //                    grdMain.View.SetRowCellValue(grdMain.View.GetFocusedDataSourceRowIndex(), "CONSUMABLEDEFNAME", string.Empty);
            //                    grdMain.View.SetRowCellValue(grdMain.View.GetFocusedDataSourceRowIndex(), "STOCKUNIT", string.Empty);
            //                    break;
            //            }
            //        }
            //    };

            //    // Grid에 공정그룹, 품목코드, 설비ID, 자재ID text 삭제시 이벤트
            //    control.ColumnEdit.EditValueChanged += (s, e) =>
            //    {
            //        if (Format.GetFullTrimString((s as DevExpress.XtraEditors.ButtonEdit).EditValue).Equals(string.Empty))
            //        {
            //            switch (control.FieldName)
            //            {
            //                case "PROCESSSEGMENTCLASSID":
            //                    grdMain.View.SetRowCellValue(grdMain.View.GetFocusedDataSourceRowIndex(), "PROCESSSEGMENTCLASSNAME", string.Empty);
            //                    break;

            //                case "PRODUCTDEFID":
            //                    grdMain.View.SetRowCellValue(grdMain.View.GetFocusedDataSourceRowIndex(), "PRODUCTDEFNAME", string.Empty);
            //                    break;

            //                case "EQUIPMENTID":
            //                    grdMain.View.SetRowCellValue(grdMain.View.GetFocusedDataSourceRowIndex(), "EQUIPMENTNAME", string.Empty);
            //                    grdMain.View.SetRowCellValue(grdMain.View.GetFocusedDataSourceRowIndex(), "AREANAME", string.Empty);
            //                    break;

            //                case "MATERIALID":
            //                    grdMain.View.SetRowCellValue(grdMain.View.GetFocusedDataSourceRowIndex(), "CONSUMABLEDEFNAME", string.Empty);
            //                    grdMain.View.SetRowCellValue(grdMain.View.GetFocusedDataSourceRowIndex(), "STOCKUNIT", string.Empty);
            //                    break;
            //            }
            //        }
            //    };
            //});

            //Conditions.GetControls<SmartSelectPopupEdit>().ForEach(control =>
            //{
            //    // 조회조건의 ID 항목을 backSpace로 모두 삭제시에 이름 삭제
            //    control.EditValueChanged += (s, e) =>
            //    {
            //        if (Format.GetFullTrimString(control.EditValue).Equals(string.Empty))
            //        {
            //            switch (control.Name)
            //            {
            //                case "PROCESSSEGMENTCLASSID":
            //                    Conditions.GetControl<SmartTextBox>("PROCESSSEGMENTCLASSNAME").Text = string.Empty;
            //                    break;

            //                case "PRODUCTDEFID":
            //                    Conditions.GetControl<SmartTextBox>("PRODUCTDEFNAME").Text = string.Empty;
            //                    break;

            //                case "EQUIPMENTID":
            //                    Conditions.GetControl<SmartTextBox>("EQUIPMENTNAME").Text = string.Empty;
            //                    break;

            //                case "AREAID":
            //                    Conditions.GetControl<SmartTextBox>("AREANAME").Text = string.Empty;
            //                    break;

            //                case "MATERIALID":
            //                    Conditions.GetControl<SmartTextBox>("CONSUMABLEDEFNAME").Text = string.Empty;
            //                    break;

            //                default:
            //                    break;
            //            }
            //        }
            //    };
            //});
        }

        #endregion Event

        #region 툴바

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            ExecuteRule("Productbyowner", grdMain.GetChangedRows());
        }

        #endregion 툴바

        #region 검색

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            try
            {
                DialogManager.ShowWaitArea(this.pnlContent);

                await base.OnSearchAsync();

                Dictionary<string, object> searchParam = Conditions.GetValues();
                searchParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                //searchParam.Add("P_OWNER", UserInfo.Current.Id);

                //SmartBandedGrid grid = grdMain;
                SmartBandedGrid grid = tabMain.SelectedTabPageIndex.Equals(0) ? grdMain : grdHistory;

                grid.View.ClearDatas();

                if (await SqlExecuter.QueryAsync(tabMain.SelectedTabPageIndex.Equals(0) ? "SelectProductByOwnerList" : "SelectProductByOwnerHistoryList", "10001", searchParam) is DataTable dt)
                {
                    if (dt.Rows.Count.Equals(0))
                    {
                        ShowMessage("NoSelectData");
                        return;
                    }

                    grid.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                throw MessageException.Create(Format.GetString(ex));
            }
            finally
            {
                DialogManager.CloseWaitArea(this.pnlContent);
            }
        }

        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            #region  담당자

            Dictionary<string, object> dicParam;
            dicParam = new Dictionary<string, object> {
                { "ENTERPRISEID", UserInfo.Current.Enterprise }
                , { "PLANTID", UserInfo.Current.Plant }
            };
            var parentCodeClassPopupColumn = Conditions.AddSelectPopup("P_OWNER", new SqlQuery("GetUserList", "10001", dicParam), "USERNAME", "USERID")
                .SetPopupLayout("SELECTOWNERID", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupResultCount(1)
                .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                .SetPosition(12)
                .SetPopupAutoFillColumns("USERNAME")
                .SetPopupResultMapping("OWNERNAME", "USERNAME")
                .SetDefault(UserInfo.Current.Name, UserInfo.Current.Id)
                .SetLabel("OWNER")
            ;
            parentCodeClassPopupColumn.Conditions.AddTextBox("USERIDNAME").SetLabel("MANAGERIDNAME");

            parentCodeClassPopupColumn.GridColumns.AddTextBoxColumn("USERID", 150).SetLabel("MANAGERID");
            parentCodeClassPopupColumn.GridColumns.AddTextBoxColumn("USERNAME", 200).SetLabel("MANAGER");

            //Conditions.GetCondition<ConditionItemSelectPopup>("P_OWNER").SetValidationIsRequired();
            //Conditions.GetControl<SmartDateEdit>("P_SEARCHDATE").EditValue = DateTime.Today;

            #endregion 담당자

        }

        #endregion 검색

        #region 유효성 검사

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            grdMain.View.CheckValidation();

            if (grdMain.GetChangedRows().Rows.Count.Equals(0))
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }

            //foreach (DataRow dr in grdMain.GetChangedRows().Rows)
            //{
            //    if (Format.GetString(dr.GetObject("P_OWNER"), string.Empty).Equals(string.Empty))
            //    {
            //        dr.GetObject("P_OWNER") = UserInfo.Current.Id;
            //    }
            //}

            //foreach (DataRow dr in grdMain.GetChangedRows().Rows)
            //{
            //    if (Format.GetString(dr.GetObject("PRODUCTDEFID"), string.Empty).Equals(string.Empty))
            //    {
            //        if (!Convert.ToInt32(Format.GetDouble(dr.GetObject("CHANGECOUNT"), 0)).Equals(0))
            //        {
            //            // 수정근거를 입력해야 합니다.
            //            throw MessageException.Create("NoModifibasisMessage");
            //        }
            //    }
            //}
        }

        #endregion 유효성 검사
    }
}