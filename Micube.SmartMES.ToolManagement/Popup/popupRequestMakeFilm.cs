#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

#endregion using

namespace Micube.SmartMES.ToolManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 치공구관리 > 필름관리 > 필름제작요청 복사
    /// 업  무  설  명  : 필름제작 요청 등록화면 복사 팝업
    /// 생    성    자  : 전우성
    /// 생    성    일  : 2021-04-02
    /// 수  정  이  력  :
    ///
    ///
    /// </summary>
    public partial class popupRequestMakeFilm : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region Interface

        public DataRow CurrentDataRow { get; set; }

        #endregion Interface

        #region Local Variables

        /// <summary>
        /// CheckRow 전달
        /// </summary>
        /// <param name="dt"></param>
        public delegate void SelectedRowTableHandler(DataTable dt);

        public event SelectedRowTableHandler SelectedRowTableEvent;

        #endregion Local Variables

        #region 생성자

        public popupRequestMakeFilm()
        {
            InitializeComponent();

            InitializeLanguageKey();
            InitializeGrid();
            InitializeEvent();
            InitializeControls();
        }

        #endregion 생성자

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// Controler 초기화
        /// </summary>
        private void InitializeControls()
        {
            this.AcceptButton = btnOK;
            this.CancelButton = btnCancel;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            #region 날짜

            dtpFrom.Properties.EditMask = "yyyy-MM-dd HH:mm:ss";
            dtpFrom.Properties.Mask.UseMaskAsDisplayFormat = true;
            dtpFrom.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd HH:mm:ss"); 

            dtpTo.Properties.EditMask = "yyyy-MM-dd HH:mm:ss";
            dtpTo.Properties.Mask.UseMaskAsDisplayFormat = true;
            dtpTo.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            #endregion 날짜

            #region 품목코드

            ConditionItemSelectPopup condition = new ConditionItemSelectPopup();
            condition.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            condition.SetPopupLayout("PRODUCTDEFID", PopupButtonStyles.Ok_Cancel);

            condition.Id = "PRODUCTDEFID";
            condition.LabelText = "PRODUCTDEFID";
            condition.SearchQuery = new SqlQuery("GetProductList", "10001",
                                                new Dictionary<string, object> {    { "ENTERPRISEID", UserInfo.Current.Enterprise },
                                                                                    { "PLANTID", UserInfo.Current.Plant },
                                                                                    { "LANGUAGETYPE", UserInfo.Current.LanguageType }});
            condition.IsMultiGrid = false;
            condition.DisplayFieldName = "PRODUCTDEFID";
            condition.ValueFieldName = "PRODUCTDEFID";
            condition.LanguageKey = "PRODUCTDEFID";
            condition.Conditions.AddTextBox("PRODUCTDEFNAME").SetLabel("PRODUCTNAMEANDNO");
            condition.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 120);
            condition.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 60);
            condition.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 180);
            condition.GridColumns.AddTextBoxColumn("PRODUCTIONTYPE", 120);
            condition.GridColumns.AddTextBoxColumn("JOBTYPE", 100);
            condition.SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
            {
                selectedRow.ForEach(row =>
                {
                    sspProduct.Text = Format.GetString(row["PRODUCTDEFID"], string.Empty);
                    txtProductRev.Text = Format.GetString(row["PRODUCTDEFVERSION"], string.Empty);
                    txtProductName.Text = Format.GetString(row["PRODUCTDEFNAME"], string.Empty);
                });
            });

            sspProduct.SelectPopupCondition = condition;
            txtProductRev.ReadOnly = true;
            txtProductName.ReadOnly = true;

            #endregion 품목코드

            #region Combo

            // 진행상태
            cboFilmProgressStatus.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboFilmProgressStatus.ShowHeader = false;
            cboFilmProgressStatus.DisplayMember = "CODENAME";
            cboFilmProgressStatus.ValueMember = "CODEID";
            cboFilmProgressStatus.UseEmptyItem = true;
            cboFilmProgressStatus.EmptyItemCaption = Language.Get("ALLVIEWS");
            cboFilmProgressStatus.EmptyItemValue = "*";
            cboFilmProgressStatus.DataSource = SqlExecuter.Query("GetCodeList", "00001",
                                                new Dictionary<string, object> {    { "CODECLASSID", "FilmProgressStatus" },
                                                                                    { "LANGUAGETYPE", UserInfo.Current.LanguageType }});
            cboFilmProgressStatus.EditValue = "*";

            // 필름유형1
            cboFilmDetailType.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboFilmDetailType.ShowHeader = false;
            cboFilmDetailType.DisplayMember = "CODENAME";
            cboFilmDetailType.ValueMember = "CODEID";
            cboFilmDetailType.UseEmptyItem = true;
            cboFilmDetailType.EmptyItemCaption = Language.Get("ALLVIEWS");
            cboFilmDetailType.EmptyItemValue = "*";
            cboFilmDetailType.DataSource = SqlExecuter.Query("GetCodeList", "00001",
                                                new Dictionary<string, object> {    { "CODECLASSID", "ToolTypeA" },
                                                                                    { "LANGUAGETYPE", UserInfo.Current.LanguageType }});
            cboFilmDetailType.EditValue = "*";

            #endregion Combo
        }

        /// <summary>
        /// Language Key 초기화
        /// </summary>
        private void InitializeLanguageKey()
        {
            this.LanguageKey = "COPY";
            btnOK.LanguageKey = "OK";
            btnCancel.LanguageKey = "CANCEL";
            btnSearch.LanguageKey = "SEARCH";
            grdMain.LanguageKey = "LIST";
            layoutMain.SetLanguageKey(layoutControlItem4, "DATEFROM");
            layoutMain.SetLanguageKey(layoutControlItem5, "DATETO");
            layoutMain.SetLanguageKey(layoutControlItem6, "PRODUCTDEFID");
            layoutMain.SetLanguageKey(layoutControlItem7, "PRODUCTDEFVERSION");
            layoutMain.SetLanguageKey(layoutControlItem8, "PRODUCTDEFNAME");
            layoutMain.SetLanguageKey(layoutControlItem9, "FILMPROGRESSSTATUS");
            layoutMain.SetLanguageKey(layoutControlItem10, "FILMCATEGORY");
        }

        /// <summary>
        /// Grid 초기화
        /// </summary>
        private void InitializeGrid()
        {
            grdMain.GridButtonItem = GridButtonItem.Export;
            grdMain.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdMain.View.AddTextBoxColumn("FILMSEQUENCE").SetIsHidden();                                //필름시퀀스
            grdMain.View.AddTextBoxColumn("PLANTID").SetIsHidden();                                     //PLANT
            grdMain.View.AddTextBoxColumn("ENTERPRISEID").SetIsHidden();                                //ENTERPRISEID
            grdMain.View.AddTextBoxColumn("REQUESTUSERID").SetIsHidden();                                 //요청자ID
            grdMain.View.AddTextBoxColumn("FILMCATEGORYFIRST").SetIsHidden();                           //필름유형 앞자리 : 필름유형2에 사용하기 위한 Relation

            grdMain.View.AddTextBoxColumn("PRODUCTIONTYPE", 80);                                        //생산구분
            grdMain.View.AddTextBoxColumn("REQUESTDATE", 160).SetDisplayFormat("yyyy-MM-dd HH:mm:ss");  //의뢰일
            grdMain.View.AddTextBoxColumn("REQUESTDEPARTMENT", 100);                                    //의뢰부서
            grdMain.View.AddTextBoxColumn("REQUESTUSERNAME", 80);                                       //요청자명
            grdMain.View.AddTextBoxColumn("PRODUCTDEFID", 100);                                         //품목코드
            grdMain.View.AddTextBoxColumn("PRODUCTDEFNAME", 200).SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("PRODUCTDEFVERSION", 200).SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("FILMVERSION", 120);                                          //CAM Rev
            grdMain.View.AddTextBoxColumn("FILMNAME", 350).SetIsReadOnly();                             //필름명

            grdMain.View.AddSpinEditColumn("CONTRACTIONX", 100)
                   .SetDisplayFormat("###,###,##0.#########", MaskTypes.Numeric)
                   .SetTextAlignment(TextAlignment.Right);                                              //수축률X

            grdMain.View.AddSpinEditColumn("CONTRACTIONY", 100)
                   .SetDisplayFormat("###,###,##0.#########", MaskTypes.Numeric)
                   .SetTextAlignment(TextAlignment.Right);                                              //수축률Y

            grdMain.View.AddComboBoxColumn("TOOLTYPE", 120, new SqlQuery("GetCodeList", "00001", $"CODECLASSID=ToolTypeA", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                   .SetLabel("FILMCATEGORY");                                                          //필름유형1

            grdMain.View.AddComboBoxColumn("TOOLDETAILTYPE", 120, new SqlQuery("GetCodeListByFilm", "10001", "CODECLASSID=ToolDetailType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                   .SetLabel("FILMDETAILCATEGORY")
                   .SetRelationIds("FILMCATEGORYFIRST");                                               //필름유형2

            grdMain.View.AddComboBoxColumn("FILMUSELAYER1", 120, new SqlQuery("GetCodeListByFilm", "10001", "CODECLASSID=FilmUseLayer1", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                   .SetRelationIds("FILMCATEGORYFIRST");                                                //Layer1

            grdMain.View.AddSpinEditColumn("QTY", 80).SetTextAlignment(TextAlignment.Right);            //수량

            grdMain.View.AddTextBoxColumn("REQUESTCOMMENT", 200);                                       //의뢰사유
            grdMain.View.AddTextBoxColumn("JOBTYPE", 100).SetIsReadOnly();                              //작업구분

            grdMain.View.AddComboBoxColumn("DURABLECLASSID", 120, new SqlQuery("GetCodeList", "00001", $"CODECLASSID=DurableClass", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));  //필름구분
            grdMain.View.AddTextBoxColumn("FILMCODE", 150);                                             //필름코드
            grdMain.View.AddTextBoxColumn("CHANGECONTRACTIONX", 120);                                   //요청수축률(%)X
            grdMain.View.AddTextBoxColumn("CHANGECONTRACTIONY", 120);                                   //요청수축률(%)Y
            grdMain.View.AddTextBoxColumn("RESOLUTION", 80);                                            //해상도
            grdMain.View.AddComboBoxColumn("ISCOATING", 80, new SqlQuery("GetCodeListByToolWithLanguage", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CODECLASSID=YesNo")); // 코팅유무
            grdMain.View.AddComboBoxColumn("PRIORITYID", 80, new SqlQuery("GetCodeListByToolWithLanguage", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CODECLASSID=FilmPriorityCode")).SetLabel("PRIORITY"); // 우선순위
            grdMain.View.AddTextBoxColumn("AREAID", 100);                                               //입고작업장
            grdMain.View.AddTextBoxColumn("AREANAME", 160);

            grdMain.View.PopulateColumns();

            grdMain.View.SetIsReadOnly();
            grdMain.ShowStatusBar = true;
            grdMain.View.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
        }

        #endregion 컨텐츠 영역 초기화

        #region Event

        /// <summary>
        /// Event 초기화
        /// </summary>
        private void InitializeEvent()
        {
            // 조회 버튼 클릭
            btnSearch.Click += (s, e) =>
            {
                try
                {
                    DialogManager.ShowWaitArea(this.pnlMain);

                    grdMain.View.ClearDatas();

                    Dictionary<string, object> param = new Dictionary<string, object>
                    {
                        { "P_REQUESTDATE_PERIODFR", dtpFrom.Text },
                        { "P_REQUESTDATE_PERIODTO", dtpTo.Text },
                        { "P_PRODUCTDEFID", sspProduct.EditValue  },
                        { "P_PRODUCTDEFVERSION", txtProductRev.Text },
                        { "FILMPROGRESSSTATUS", cboFilmProgressStatus.EditValue },
                        { "P_FILMDETAILTYPE", cboFilmDetailType.EditValue },
                        { "LANGUAGETYPE", UserInfo.Current.LanguageType },
                        { "ENTERPRISEID", UserInfo.Current.Enterprise },
                        { "P_PLANTID", UserInfo.Current.Plant },
                        { "USERID", UserInfo.Current.Id }
                    };

                    if (SqlExecuter.Query("GetRequestMakingFilmListByTool", "10001", param) is DataTable dt)
                    {
                        if (dt.Rows.Count.Equals(0))
                        {
                            ShowMessage("NoSelectData");
                            return;
                        }

                        grdMain.DataSource = dt;
                    }
                }
                catch (Exception ex)
                {
                    throw MessageException.Create(Format.GetString(ex));
                }
                finally
                {
                    DialogManager.CloseWaitArea(this.pnlMain);
                }
            };

            // 확인 버튼 클릭 이벤트
            btnOK.Click += (s, e) =>
            {
                if (!grdMain.View.GetCheckedRows().Rows.Count.Equals(0))
                {
                    SelectedRowTableEvent(grdMain.View.GetCheckedRows());
                }

                this.Close();
            };
        }

        #endregion Event
    }
}