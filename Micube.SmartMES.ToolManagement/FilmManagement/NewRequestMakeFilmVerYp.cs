#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using DevExpress.XtraEditors.Repository;

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

namespace Micube.SmartMES.ToolManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 치공구관리 > 필름관리 > 필름제작요청 (새버전 - 
    /// 업  무  설  명  : 초안작성 - 필름제작요청
    /// 생    성    자  : 김용조
    /// 생    성    일  : 2020-01-02
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class NewRequestMakeFilmVerYp : SmartConditionManualBaseForm
    {
        #region Local Variables
        //화면의 현재 상태  added, modified, browse;
        private string _currentStatus = "browse";
        private string _filmSequence = "";
        private string _requestUserID = "";
        private string _makeVendorID = "";
        private string _receiptAreaID = "";
        private string _searchAreaID = "";
        private string _isModify = "";
        string[] _clipDatas;
        int _clipIndex = 1;
        IDataObject _clipBoardData;
        bool _isGoodCopy = true;

        //DESCRIPTION : 작업장 및 Site 권한 관련하여 Site 선택시 작업장 혹은 벤더와 같은 Site 종속적인 항목을 제어하기 위해 전역변수로 설정
        ConditionItemSelectPopup makeVendorPopup;
        ConditionItemSelectPopup areaCondition;
        ConditionItemSelectPopup receiptAreaPopup;
        ConditionItemSelectPopup filmCodeCondition;
        ConditionItemComboBox segmentCondition;
        ConditionItemComboBox _toolDetailTypeBox;
        ConditionItemComboBox _filmUserLayerBox;

        DataTable _userInfo;
        #endregion

        #region 생성자

        public NewRequestMakeFilmVerYp()
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
            InitRequiredControl();
            InitializeProductGrid();
            InitializeFilmGrid();
            InitializeUserInfoGrid();
            InitializeActionGrid();

            InitializeInsertForm();

            //팝업창 오픈후 입력받은 컨트롤들을 설정
            //ucFilmCode.msgHandler += ShowMessageInfo;
            //ucFilmCode.SetSmartTextBoxForSearchData(txtProductDefID, txtProductDefName, txtFilmType, txtFilmCategory, txtFilmDetailCategory, txtLayer, txtJobType
            //    , txtUseProcess, "PRODUCTDEFID");
        }

        #region InitRequiredControl - 필수입력항목들을 체크한다.
        private void InitRequiredControl()
        {
            //SetRequiredValidationControl(lblRequestDate);
            //SetRequiredValidationControl(lblReceiptArea);
            //SetRequiredValidationControl(lblQty);
            //SetRequiredValidationControl(lblRequestQty);
        }
        #endregion

        #region InitializeProductGrid - 품목그리드를 초기화한다.
        /// <summary>        
        /// 필름내역목록을 초기화한다.
        /// </summary>
        private void InitializeProductGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdProduct.ColumnCount = 3;
            grdProduct.SetInvisibleFields("PRODUCTDEFCODE", "JOBTYPE", "PRODUCTDEFTYPE", "PRODUCTDEFTYPENAME", "PRODUCTIONTYPE");
        }
        #endregion

        #region InitializeFilmGrid - 필름내역목록을 초기화한다.
        /// <summary>        
        /// 필름내역목록을 초기화한다.
        /// </summary>
        private void InitializeFilmGrid()
        {
            grdFilm.GridButtonItem = GridButtonItem.Export;       
            grdFilm.View.SetIsReadOnly();

            grdFilm.View.AddTextBoxColumn("FILMSEQUENCE").SetIsHidden();                             //필름시퀀스
            grdFilm.View.AddTextBoxColumn("FILMPROGRESSSTATUSID").SetIsHidden();                     //진행상태아이디
            grdFilm.View.AddTextBoxColumn("REQUESTUSERID").SetIsHidden();                            //요청자아이디   
            grdFilm.View.AddTextBoxColumn("JOBTYPEID", 80).SetIsHidden();                            //작업구분아이디
            grdFilm.View.AddTextBoxColumn("PRODUCTIONTYPEID", 80).SetIsHidden();                     //생산구분아이디
            grdFilm.View.AddTextBoxColumn("FILMTYPEID").SetIsHidden();                               //필름구분아이디
            grdFilm.View.AddTextBoxColumn("FILMCATEGORYID").SetIsHidden();                           //필름유형아이디
            grdFilm.View.AddTextBoxColumn("FILMDETAILCATEGORYID").SetIsHidden();                     //상세유형아이디   
            grdFilm.View.AddTextBoxColumn("CUSTOMERID").SetIsHidden();                               //고객사아이디
            grdFilm.View.AddTextBoxColumn("USEPROCESSSEGMENT", 80).SetIsHidden();                    //사용공정
            grdFilm.View.AddTextBoxColumn("USEPLANDATE", 100).SetIsHidden();                         //사용예정일
            grdFilm.View.AddTextBoxColumn("PRIORITYID").SetIsHidden();                               //우선순위아이디
            grdFilm.View.AddTextBoxColumn("MAKEVENDORID").SetIsHidden();                             //제작업체아이디
            grdFilm.View.AddTextBoxColumn("MAKEVENDOR", 150).SetIsHidden();                          //제작업체
            grdFilm.View.AddTextBoxColumn("RECEIPTAREAID").SetIsHidden();                            //입고작업장아이디
            grdFilm.View.AddTextBoxColumn("ISMODIFY").SetIsHidden();                                 //작업장권한여부
            grdFilm.View.AddTextBoxColumn("ACCEPTDATE", 160).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsHidden(); //접수일시
            grdFilm.View.AddTextBoxColumn("ACCEPTDEPARTMENT", 100).SetIsHidden();                    //접수부서
            grdFilm.View.AddTextBoxColumn("ACCEPTUSERID").SetIsHidden();                             //접수자아이디
            grdFilm.View.AddTextBoxColumn("ACCEPTUSER", 100).SetIsHidden();                          //접수자
            grdFilm.View.AddTextBoxColumn("RELEASEDATE", 160).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsHidden();    //출고일시
            grdFilm.View.AddTextBoxColumn("RELEASEUSERID").SetIsHidden();                            //출고자아이디
            grdFilm.View.AddTextBoxColumn("RELEASEUSER", 100).SetIsHidden();                         //출고자
            grdFilm.View.AddTextBoxColumn("MEASURECONTRACTIONX", 80).SetIsHidden();                  //실측수축율X
            grdFilm.View.AddTextBoxColumn("MEASURECONTRACTIONY", 80).SetIsHidden();                  //실측수축율Y
            grdFilm.View.AddTextBoxColumn("RECEIVEDATE", 160).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsHidden();    //인수일시
            grdFilm.View.AddTextBoxColumn("RECEIVEUSERID").SetIsHidden();                            //인수자아이디
            grdFilm.View.AddTextBoxColumn("RECEIVEUSER", 100).SetIsHidden();                         //인수자
            grdFilm.View.AddTextBoxColumn("FILMUSELAYER2", 150).SetIsHidden();                       //Layer2       
            grdFilm.View.AddTextBoxColumn("CUSTOMER", 120).SetIsHidden();                            //고객

            grdFilm.View.AddTextBoxColumn("FILMPROGRESSSTATUS", 80).SetTextAlignment(TextAlignment.Center); //진행상태
            grdFilm.View.AddTextBoxColumn("REQUESTDATE", 160).SetDisplayFormat("yyyy-MM-dd HH:mm:ss");  //의뢰일
            grdFilm.View.AddTextBoxColumn("REQUESTDEPARTMENT", 100);                                 //의뢰부서ID                                            
            grdFilm.View.AddTextBoxColumn("REQUESTUSER", 80);                                        //요청자
            grdFilm.View.AddTextBoxColumn("PRODUCTDEFID", 120);                                      //품목코드
            grdFilm.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80);                                  //내부 Rev
            grdFilm.View.AddTextBoxColumn("PRODUCTDEFNAME", 280);                                    //품목명
            grdFilm.View.AddTextBoxColumn("FILMVERSION", 120);                                       //CAM Rev.
            grdFilm.View.AddTextBoxColumn("FILMNAME", 350);                                          //필름명   
            grdFilm.View.AddTextBoxColumn("CONTRACTIONX", 120);                                      //수축률X
            grdFilm.View.AddTextBoxColumn("CONTRACTIONY", 120);                                      //수축률Y 
            grdFilm.View.AddTextBoxColumn("FILMCATEGORY", 80).SetTextAlignment(TextAlignment.Center);    //필름유형1
            grdFilm.View.AddTextBoxColumn("FILMDETAILCATEGORY", 80).SetTextAlignment(TextAlignment.Center);    //필름유형2        
            grdFilm.View.AddTextBoxColumn("FILMUSELAYER1", 120);                                     //Layer1          
            grdFilm.View.AddTextBoxColumn("QTY", 80);                                                //수량
            grdFilm.View.AddTextBoxColumn("REQUESTCOMMENT", 200);                                    //의뢰사유
            grdFilm.View.AddTextBoxColumn("JOBTYPE", 100).SetTextAlignment(TextAlignment.Center);   //작업구분
            grdFilm.View.AddTextBoxColumn("PRODUCTIONTYPE", 80);                                     //생산구분
            grdFilm.View.AddTextBoxColumn("FILMTYPE", 80).SetTextAlignment(TextAlignment.Center);    //필름구분
            grdFilm.View.AddTextBoxColumn("FILMCODE", 150);                                          //필름코드 
            grdFilm.View.AddTextBoxColumn("CHANGECONTRACTIONX", 120);                                //요청수축률(%)X
            grdFilm.View.AddTextBoxColumn("CHANGECONTRACTIONY", 120);                                //요청수축률(%)Y                                          
            grdFilm.View.AddTextBoxColumn("RESOLUTION", 80);                                         //해상도
            grdFilm.View.AddTextBoxColumn("ISCOATING", 80).SetTextAlignment(TextAlignment.Center);    //코팅유무
            grdFilm.View.AddTextBoxColumn("PRIORITY", 80).SetTextAlignment(TextAlignment.Center);    //우선순위
            grdFilm.View.AddTextBoxColumn("RECEIPTAREA", 150);                                       //입고작업장

            grdFilm.View.SetKeyColumn("FILMVERSION", "FILMCATEGORY", "FILMDETAILCATEGORY", "FILMUSELAYER1", "FILMUSELAYER2");
            grdFilm.View.PopulateColumns();
        }
        #endregion

        #region InitializeUserInfoGrid - 사용자 정보 그리드를 초기화한다.
        /// <summary>        
        /// 필름내역목록을 초기화한다.
        /// </summary>
        private void InitializeUserInfoGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdUserInfo.ColumnCount = 3;
            grdUserInfo.SetInvisibleFields("REQUESTDATE2");

            _userInfo = new DataTable();
            _userInfo.Columns.Add("REQUESTDATE");
            _userInfo.Columns.Add("REQUESTDATE2");
            _userInfo.Columns.Add("REQUESTUSER");
            _userInfo.Columns.Add("REQUESTDEPARTMENT");

            DataRow newRow = _userInfo.NewRow();
            newRow["REQUESTDATE"] = DateTime.Now.ToString("yyyy-MM-dd");
            newRow["REQUESTDATE2"] = DateTime.Now.ToString("yyyy-MM-dd");
            newRow["REQUESTUSER"] = UserInfo.Current.Name;
            newRow["REQUESTDEPARTMENT"] = UserInfo.Current.Department;

            _userInfo.Rows.Add(newRow);

            grdUserInfo.DataSource = _userInfo;
        }


        private void InitializeUserInfoGridSetting(string RequestDate, string RequestUser, string RequestDepartment)
        {
            // TODO : 그리드 초기화 로직 추가
            grdUserInfo.ColumnCount = 3;
            grdUserInfo.SetInvisibleFields("REQUESTDATE2");

            _userInfo = new DataTable();
            _userInfo.Columns.Add("REQUESTDATE");
            _userInfo.Columns.Add("REQUESTDATE2");
            _userInfo.Columns.Add("REQUESTUSER");
            _userInfo.Columns.Add("REQUESTDEPARTMENT");

            DataRow newRow = _userInfo.NewRow();
            newRow["REQUESTDATE"] = RequestDate;
            newRow["REQUESTDATE2"] = RequestDate;
            newRow["REQUESTUSER"] = RequestUser;
            newRow["REQUESTDEPARTMENT"] = RequestDepartment;

            _userInfo.Rows.Add(newRow);

            grdUserInfo.DataSource = _userInfo;
        }
        #endregion

        #region InitializeActionGrid - 입력/수정용 그리드를 초기화한다.
        /// <summary>        
        /// 필름내역목록을 초기화한다.
        /// </summary>
        private void InitializeActionGrid()
        {
            grdActionFilm.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export;

            grdActionFilm.View.AddTextBoxColumn("FILMSEQUENCE").SetIsHidden();                             //필름시퀀스
            grdActionFilm.View.AddTextBoxColumn("FILMPROGRESSSTATUSID").SetIsHidden();                     //진행상태아이디
            grdActionFilm.View.AddTextBoxColumn("FILMCODE", 150).SetIsHidden();                             //필름코드      
            grdActionFilm.View.AddDateEditColumn("USEPLANDATE", 120).SetDisplayFormat("yyyy-MM-dd", MaskTypes.DateTime).SetIsHidden();  //사용예정일         
            grdActionFilm.View.AddTextBoxColumn("VENDORID").SetIsHidden();                                  //제작업체아이디 
            grdActionFilm.View.AddTextBoxColumn("AREAID").SetIsHidden();                                    //입고작업장아이디   
            grdActionFilm.View.AddTextBoxColumn("ISMODIFY").SetIsHidden();                                  //작업장권한여부    
            grdActionFilm.View.AddTextBoxColumn("DURABLECLASSID").SetIsHidden();                            //DurableClassID : ToolTypeA                    
            grdActionFilm.View.AddTextBoxColumn("FILMUSELAYER2", 120).SetIsHidden();  //Layer2

            grdActionFilm.View.AddTextBoxColumn("FILMPROGRESSSTATUS", 80).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();   //진행상태                        
            grdActionFilm.View.AddTextBoxColumn("FILMVERSION", 120).SetValidationIsRequired();  //Rev No.            
            grdActionFilm.View.AddComboBoxColumn("FILMCATEGORY", 120, new SqlQuery("GetCodeListByFilm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CODECLASSID=ToolTypeA"), "CODENAME", "CODEID")
                .SetTextAlignment(TextAlignment.Center).SetValidationIsRequired();      //필름유형
            grdActionFilm.View.AddTextBoxColumn("FILMDETAILCATEGORY", 150).SetValidationIsRequired();   //상세유형
            grdActionFilm.View.AddTextBoxColumn("FILMUSELAYER1", 120);  //Layer1
            grdActionFilm.View.AddTextBoxColumn("RESOLUTION", 80);                                          //해상도
            grdActionFilm.View.AddComboBoxColumn("ISCOATING", 80, new SqlQuery("GetCodeListByToolWithLanguage", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CODECLASSID=YesNo"), "CODENAME", "CODEID")
                .SetTextAlignment(TextAlignment.Center);    //코팅유무
            grdActionFilm.View.AddSpinEditColumn("CONTRACTIONX", 100).SetDisplayFormat("0.#####");   //요청수출율X
            grdActionFilm.View.AddSpinEditColumn("CONTRACTIONY", 100).SetDisplayFormat("0.#####");   //요청수축율Y
            grdActionFilm.View.AddTextBoxColumn("CHANGECONTRACTIONX", 80).SetIsReadOnly();   //요청수축율(%)X
            grdActionFilm.View.AddTextBoxColumn("CHANGECONTRACTIONY", 80).SetIsReadOnly();   //요청수축율(%)Y
            grdActionFilm.View.AddComboBoxColumn("PRIORITY", 80, new SqlQuery("GetCodeListByToolWithLanguage", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CODECLASSID=FilmPriorityCode"), "CODENAME", "CODEID")
                .SetTextAlignment(TextAlignment.Center);    //우선순위
            InitializeMakeVendorsPopup();                                                                   //제작업체
            InitializeReceiptAreaPopup();                                                                   //입고작업장
            grdActionFilm.View.AddSpinEditColumn("QTY", 80).SetValidationIsRequired();  //수량
            grdActionFilm.View.AddTextBoxColumn("REQUESTCOMMENT", 200);                                     //요청내용
            grdActionFilm.View.SetKeyColumn("FILMVERSION", "FILMCATEGORY", "FILMDETAILCATEGORY");
            grdActionFilm.View.PopulateColumns();

            //필름상세유형
            RepositoryItemLookUpEdit repositoryItems = new RepositoryItemLookUpEdit();
            repositoryItems.DisplayMember = "CODENAME";
            repositoryItems.ValueMember = "CODEID";
            repositoryItems.DataSource = GetToolDetailType("");
            repositoryItems.ShowHeader = false;
            repositoryItems.NullText = "";

            repositoryItems.NullValuePromptShowForEmptyValue = true;
            repositoryItems.PopulateColumns();
            repositoryItems.Columns[repositoryItems.ValueMember].Visible = false;

            grdActionFilm.View.Columns["FILMDETAILCATEGORY"].ColumnEdit = repositoryItems;

            //Layer2 : FilmUseCSLayer , FilmUseLayer1
            RepositoryItemLookUpEdit repositoryItems1 = new RepositoryItemLookUpEdit();
            repositoryItems1.DisplayMember = "CODENAME";
            repositoryItems1.ValueMember = "CODEID";
            repositoryItems1.DataSource = GetUseCSLayer("");
            repositoryItems1.ShowHeader = false;
            repositoryItems1.NullText = "";
            repositoryItems1.NullValuePromptShowForEmptyValue = true;
            repositoryItems1.PopulateColumns();
            repositoryItems1.Columns[repositoryItems1.ValueMember].Visible = false;
            repositoryItems1.EditValueChanged += FilmUseLayer1InGrid_EditValueChanged;

            grdActionFilm.View.Columns["FILMUSELAYER1"].ColumnEdit = repositoryItems1;

            //Layer2 : FilmUseSSLayer, FilmUseLayer2
            RepositoryItemLookUpEdit repositoryItems2 = new RepositoryItemLookUpEdit();
            repositoryItems2.DisplayMember = "CODENAME";
            repositoryItems2.ValueMember = "CODEID";
            repositoryItems2.DataSource = GetUseSSLayer("");
            repositoryItems2.ShowHeader = false;
            repositoryItems2.NullText = "";
            repositoryItems2.NullValuePromptShowForEmptyValue = true;
            repositoryItems2.PopulateColumns();
            repositoryItems2.Columns[repositoryItems2.ValueMember].Visible = false;

            grdActionFilm.View.Columns["FILMUSELAYER2"].ColumnEdit = repositoryItems2;
        }


        #endregion

        #region InitializeMakeVendorsPopup : 제작업체 팝업창
        /// <summary>
        /// 팝업창 제어
        /// </summary>
        private void InitializeMakeVendorsPopup()
        {
            makeVendorPopup = grdActionFilm.View.AddSelectPopupColumn("MAKEVENDOR", 150, new SqlQuery("GetVendorListByTool", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            makeVendorPopup.ValueFieldName = "VENDORID";
            makeVendorPopup.DisplayFieldName = "VENDORNAME";
            makeVendorPopup.SetPopupLayout("MAKEVENDOR", PopupButtonStyles.Ok_Cancel, true, true);
            makeVendorPopup.SetPopupResultCount(1);
            makeVendorPopup.SetPopupLayoutForm(600, 800, FormBorderStyle.FixedToolWindow);
            makeVendorPopup.SetPopupAutoFillColumns("VENDORNAME");
            makeVendorPopup.SetPopupResultMapping("MAKEVENDOR", "VENDORNAME");
            //히든으로 설정(요구사항에 의거)
            makeVendorPopup.SetIsHidden();
            makeVendorPopup.SetPopupApplySelection((selectedRows, dataGridRows) =>
            {
                //작업장 변경 시 작업자 조회
                selectedRows.Cast<DataRow>().ForEach(row =>
                {
                    grdActionFilm.View.SetFocusedRowCellValue("VENDORID", row.GetString("VENDORID"));
                });

            });
            makeVendorPopup.Conditions.AddTextBox("VENDORNAME");

            makeVendorPopup.GridColumns.AddTextBoxColumn("VENDORID", 150)
                .SetIsHidden();
            makeVendorPopup.GridColumns.AddTextBoxColumn("VENDORNAME", 200);
        }
        #endregion

        #region InitializeReceiptAreaPopup : 입고작업장 팝업창
        /// <summary>
        /// 팝업창 제어
        /// </summary>
        private void InitializeReceiptAreaPopup()
        {
            areaCondition = grdActionFilm.View.AddSelectPopupColumn("RECEIPTAREA", 150, new SqlQuery("GetAreaListByTool", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CURRENTLOGINID={UserInfo.Current.Id}", $"ISMOD=Y"));
            areaCondition.ValueFieldName = "AREAID";
            areaCondition.DisplayFieldName = "AREANAME";
            areaCondition.SetPopupLayout("RECEIPTAREA", PopupButtonStyles.Ok_Cancel, true, true);
            areaCondition.SetPopupResultCount(1);
            areaCondition.SetPopupLayoutForm(600, 800, FormBorderStyle.FixedToolWindow);
            areaCondition.SetPopupAutoFillColumns("AREANAME");
            areaCondition.SetPopupResultMapping("RECEIPTAREA", "AREANAME");
            areaCondition.SetValidationIsRequired();
            areaCondition.SetPopupApplySelection((selectedRows, dataGridRows) =>
            {
                //작업장 변경 시 작업자 조회
                selectedRows.Cast<DataRow>().ForEach(row =>
                {
                    //_receiptAreaID = row.GetString("AREAID");
                    grdActionFilm.View.SetFocusedRowCellValue("AREAID", row.GetString("AREAID"));
                    grdActionFilm.View.SetFocusedRowCellValue("ISMODIFY", row.GetString("ISMODIFY"));
                });

            });


            areaCondition.Conditions.AddTextBox("AREANAME");

            areaCondition.GridColumns.AddTextBoxColumn("AREAID", 150)
                .SetIsHidden();
            areaCondition.GridColumns.AddTextBoxColumn("AREANAME", 200);
        }
        #endregion

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            Shown += NewRequestMakeFilmVerINTER_Shown;

            grdActionFilm.View.CellValueChanged += grdActionFilm_CellValueChanged;
            grdActionFilm.View.ShownEditor += grdActionFilm_ShownEditor;
            grdActionFilm.View.RowCellStyle += grdActionFilm_RowCellStyle;
            grdActionFilm.View.ShowingEditor += grdAction_ShowingEditor;
            grdActionFilm.View.AddingNewRow += grdAction_AddingNewRow;
            grdFilm.View.FocusedRowChanged += grdFilm_FocusedRowChanged;
        }

        #region grdAction_AddingNewRow - 행추가시 기본값 입력
        private void grdAction_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            if (grdProduct.DataSource == null)
            {
                ShowMessage(MessageBoxButtons.OK, "InputProudctInfo", "");
                args.IsCancel = true;
            }
            else
            {
                args.NewRow["PRIORITY"] = "Normal";
                args.NewRow["FILMPROGRESSSTATUSID"] = "Request";
               
                //args.NewRow["FILMVERSION"] = GetNewVersion(grdProduct.GetFieldValue("PRODUCTDEFID").ToString(), grdProduct.GetFieldValue("PRODUCTDEFVERSION").ToString());
                SetDefaultArea(args.NewRow);
            }
        }
        #endregion

        #region grdAction_ShowingEditor - 입력그리드의 입력제한
        private void grdAction_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (grdActionFilm.View.GetFocusedDataRow() != null)
            {
                //최초생성시가 아니라면 아래의 컬럼들(PK)을 수정할 수 없다.
                //if (grdActionFilm.View.FocusedColumn.FieldName.Equals("FILMVERSION")
                //    || grdActionFilm.View.FocusedColumn.FieldName.Equals("FILMCATEGORY")
                //    || grdActionFilm.View.FocusedColumn.FieldName.Equals("FILMDETAILCATEGORY")
                //    || grdActionFilm.View.FocusedColumn.FieldName.Equals("FILMUSELAYER1")
                //    || grdActionFilm.View.FocusedColumn.FieldName.Equals("FILMUSELAYER2"))
                //{
                //    if (!grdActionFilm.View.GetFocusedDataRow().GetString("FILMPROGRESSSTATUSID").Equals(""))
                //    {
                //        e.Cancel = true;
                //    }
                //}
                if (!(grdActionFilm.View.GetFocusedDataRow().GetString("FILMPROGRESSSTATUSID").Equals("Request")
                     || grdActionFilm.View.GetFocusedDataRow().GetString("FILMPROGRESSSTATUSID").Equals("RequestAgain")
                     || grdActionFilm.View.GetFocusedDataRow().GetString("FILMPROGRESSSTATUSID").Equals("CancelAccept")))
                {
                    e.Cancel = true;
                }
            }
        }
        #endregion

        #region grdActionFilm_RowCellStyle - 입력그리드의 상태에 따른 행의 배경색제어
        private void grdActionFilm_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (grdActionFilm.View.GetDataRow(e.RowHandle) != null)
            {
                if (grdActionFilm.View.GetDataRow(e.RowHandle).GetString("FILMPROGRESSSTATUSID").Equals("CancelAccept"))
                {
                    e.Appearance.BackColor = Color.Gold;
                    e.Appearance.BackColor2 = Color.Gold;
                    e.Appearance.ForeColor = Color.Black;
                }
            }
        }
        #endregion

        #region grdActionFilm_ShownEditor - 실시간으로 데이터를 변경해야하는 그리드내의 콤보박스 제어
        private void grdActionFilm_ShownEditor(object sender, EventArgs e)
        {
            if (grdActionFilm.View.FocusedColumn.FieldName == "FILMDETAILCATEGORY")
            {
                if (!grdActionFilm.View.GetFocusedDataRow().GetString("FILMCATEGORY").Equals(""))
                {
                    string codeIDPart = grdActionFilm.View.GetFocusedDataRow().GetString("FILMCATEGORY").Substring(0, 1); //CODEID의 첫글자를 가져옴.

                    if (grdActionFilm.View.ActiveEditor != null)
                    {
                        ((DevExpress.XtraEditors.LookUpEdit)grdActionFilm.View.ActiveEditor).Properties.DataSource = GetToolDetailType(codeIDPart);
                    }
                }
            }
            else if (grdActionFilm.View.FocusedColumn.FieldName == "FILMUSELAYER1")
            {
                if (!grdActionFilm.View.GetFocusedDataRow().GetString("FILMCATEGORY").Equals(""))
                {
                    string codeIDPart = grdActionFilm.View.GetFocusedDataRow().GetString("FILMCATEGORY").Substring(0, 1); //CODEID의 첫글자를 가져옴.

                    if (grdActionFilm.View.ActiveEditor != null)
                    {
                        //_allUseTopLayer = GetUseCSLayer(codeIDPart);
                        ((DevExpress.XtraEditors.LookUpEdit)grdActionFilm.View.ActiveEditor).Properties.DataSource = GetUseCSLayer(codeIDPart);
                    }
                }
            }
            else if (grdActionFilm.View.FocusedColumn.FieldName == "FILMUSELAYER2")
            {
                //빈값을 설정했으면 같이 빈값을 설정
                if (grdActionFilm.View.GetFocusedDataRow().GetString("FILMUSELAYER1").Equals(""))
                {
                    if (grdActionFilm.View.ActiveEditor != null)
                    {
                        //_allUseLayer = GetEmptyUseSSLayer();
                        ((DevExpress.XtraEditors.LookUpEdit)grdActionFilm.View.ActiveEditor).Properties.DataSource = GetEmptyUseSSLayer();
                    }
                }
                else
                {
                    if (!grdActionFilm.View.GetFocusedDataRow().GetString("FILMUSELAYER1").Equals(""))
                    {
                        string codeIDPart = grdActionFilm.View.GetFocusedDataRow().GetString("FILMUSELAYER1").Substring(0, 1); //CODEID의 첫글자를 가져옴.

                        if (grdActionFilm.View.ActiveEditor != null)
                        {
                            //_allUseLayer = GetUseSSLayer(codeIDPart);
                            ((DevExpress.XtraEditors.LookUpEdit)grdActionFilm.View.ActiveEditor).Properties.DataSource = GetUseSSLayer(codeIDPart);
                        }
                    }
                }
            }
        }
        #endregion

        #region grdFilm_FocusedRowChanged - 필름그리드 선택시 입력용(상세)그리드 변경
        private void grdFilm_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle > -1)
            {
                DisplayInfoDetail(grdFilm.View.GetFocusedDataRow());
                SearchActionFilm(grdFilm.View.GetDataRow(e.FocusedRowHandle).GetString("REQUESTDEPARTMENT")
                               , grdFilm.View.GetDataRow(e.FocusedRowHandle).GetString("REQUESTUSERID")
                               , grdFilm.View.GetDataRow(e.FocusedRowHandle).GetString("REQUESTDATE")
                               , grdFilm.View.GetDataRow(e.FocusedRowHandle).GetString("FILMPROGRESSSTATUSID")
                               );
            }
        }
        #endregion

        #region grdActionFilm_CellValueChanged - 입력용 그리드 셀 값 변환이벤트
        private void grdActionFilm_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName.Equals("CONTRACTIONX"))
                grdActionFilm.View.SetRowCellValue(e.RowHandle, "CHANGECONTRACTIONX", GetContractionPercentage(e.Value.ToString()));
            else if (e.Column.FieldName.Equals("CONTRACTIONY"))
                grdActionFilm.View.SetRowCellValue(e.RowHandle, "CHANGECONTRACTIONY", GetContractionPercentage(e.Value.ToString()));
            else if (e.Column.FieldName.Equals("USEPLANDATE"))
            {
                grdActionFilm.View.CellValueChanged -= grdActionFilm_CellValueChanged;

                DataRow row = grdActionFilm.View.GetFocusedDataRow();

                DateTime dateBudget = new DateTime();

                if (DateTime.TryParse(row.GetString("USEPLANDATE"), out dateBudget))
                {
                    grdActionFilm.View.SetFocusedRowCellValue("USEPLANDATE", dateBudget.ToString("yyyy-MM-dd"));// 예산일자
                }

                grdActionFilm.View.CellValueChanged += grdActionFilm_CellValueChanged;
            }
            else if (e.Column.FieldName.Equals("MAKEVENDOR"))
            {
                grdActionFilm.View.CellValueChanged -= grdActionFilm_CellValueChanged;

                DataRow row = grdActionFilm.View.GetDataRow(e.RowHandle);

                if (row.GetString("MAKEVENDOR").Equals(""))
                {
                    grdActionFilm.View.CellValueChanged += grdActionFilm_CellValueChanged;
                    return;
                }
                GetSingleMakeVendor(row.GetString("MAKEVENDOR"), e.RowHandle);
                grdActionFilm.View.CellValueChanged += grdActionFilm_CellValueChanged;
            }
            else if (e.Column.FieldName.Equals("RECEIPTAREA"))
            {
                grdActionFilm.View.CellValueChanged -= grdActionFilm_CellValueChanged;

                DataRow row = grdActionFilm.View.GetDataRow(e.RowHandle);

                if (row["RECEIPTAREA"].ToString().Equals(""))
                {
                    grdActionFilm.View.CellValueChanged += grdActionFilm_CellValueChanged;
                    return;
                }
                GetSingleReceiptArea(row.GetString("RECEIPTAREA"), e.RowHandle);
                grdActionFilm.View.CellValueChanged += grdActionFilm_CellValueChanged;
            }
            else if (e.Column.FieldName.Equals("FILMCATEGORY"))
            {
                grdActionFilm.View.CellValueChanged -= grdActionFilm_CellValueChanged;

                grdActionFilm.View.SetFocusedRowCellValue("FILMDETAILCATEGORY", null);
                grdActionFilm.View.SetFocusedRowCellValue("FILMUSELAYER1", null);
                grdActionFilm.View.SetFocusedRowCellValue("FILMUSELAYER2", null);

                grdActionFilm.View.CellValueChanged += grdActionFilm_CellValueChanged;
            }

        }
        #endregion

        #region FilmUseLayer1InGrid_EditValueChanged - 그리드내 Layer1 콤보박스 값 변경시 발동
        private void FilmUseLayer1InGrid_EditValueChanged(object sender, EventArgs e)
        {
            grdActionFilm.View.SetFocusedRowCellValue("FILMUSELAYER2", null);
        }
        #endregion

        #region NewRequestMakeFilmVerINTER_Shown - 화면 로딩시 이벤트
        private void NewRequestMakeFilmVerINTER_Shown(object sender, EventArgs e)
        {
            //검색조건의 품목검색항목 이벤트 등록 및 버전, 명칭 컨트롤 읽기전용 설정
            ((Micube.Framework.SmartControls.SmartSelectPopupEdit)Conditions.GetControl("PRODUCTDEFID")).ButtonClick += SearchProductDefID_ButtonClick;
            ((Micube.Framework.SmartControls.SmartTextBox)Conditions.GetControl("p_filmVersion")).ReadOnly = true;
            ((Micube.Framework.SmartControls.SmartTextBox)Conditions.GetControl("p_productDefName")).ReadOnly = true;

            //Plant 변경 및 작업장 권한에 따른 작업수행
            ChangeSiteCondition();

            //검색조건의 Site항목 변경시 이벤트 등록
            ((SmartComboBox)Conditions.GetControl("P_PLANTID")).EditValueChanged += SearchPlantID_EditValueChanged;
        }
        #endregion

        #region SearchPlantID_EditValueChanged - 검색조건의 Site 콤보박스 변경이벤트
        private void SearchPlantID_EditValueChanged(object sender, EventArgs e)
        {
            ChangeSiteCondition();
        }
        #endregion

        #region SearchProductDefID_ButtonClick - 검색조건의 품목검색 버튼클릭 이벤트
        private void SearchProductDefID_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Clear)
            {
                Conditions.GetControl("p_filmVersion").Text = "";
                Conditions.GetControl("p_productDefName").Text = "";
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
            base.OnToolbarSaveClick();
        }

        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            //base.OnToolbarClick(sender, e);
            SmartButton btn = sender as SmartButton;

            if (btn.Name.ToString().Equals("Request"))
                SaveData();
            else if (btn.Name.ToString().Equals("RequestAgain"))
                ReSaveData();
            else if (btn.Name.ToString().Equals("Delete"))
                DeleteData();
            else if (btn.Name.ToString().Equals("Initialization"))
                InitializeInsertForm();
            else if (btn.Name.ToString().Equals("Copy"))
                DisplayCopyView();
        }

        #endregion

        #region 검색

        #region OnSearchAsync - 검색
        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            InitializeInsertForm();
            // TODO : 조회 SP 변경
            var values = Conditions.GetValues();

            #region 기간 검색형 전환 처리 - 만약 시간까지 포함하여 검색한다면 아래 문구는 필요없음            
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            values.Add("USERID", UserInfo.Current.Id);
            values.Add("CURRENTLOGINID", UserInfo.Current.Id);
            #endregion

            if (Conditions.GetValue("AREANAME").ToString() != "")
                values.Add("AREAID", _searchAreaID);

            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable productResult = SqlExecuter.Query("GetProductdefidPoplistByTool", "10001", values);

            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable filmResult = SqlExecuter.Query("GetRequestMakingFilmListByTool", "10001", values);

            grdProduct.DataSource = productResult;

            grdFilm.DataSource = filmResult;

            if (filmResult.Rows.Count < 1) // 필름제작의뢰검색결과가 0이상일 경우 바인딩
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }
            else
            {
                grdFilm.View.FocusedRowHandle = 0;

                DisplayFilmRequestInfo();

                //상세정보 및 입력그리드에 정보바인딩
                SearchActionFilm(grdFilm.View.GetDataRow(0).GetString("REQUESTDEPARTMENT")
                    , grdFilm.View.GetDataRow(0).GetString("REQUESTUSERID")
                    , grdFilm.View.GetDataRow(0).GetString("REQUESTDATE")
                    , grdFilm.View.GetDataRow(0).GetString("FILMPROGRESSSTATUSID"));
            }
        }
        #endregion

        #region Research - 재검색(데이터 입력후와 같은 경우에 사용)
        /// <summary>
        /// 저장 삭제후 재 검색시 사용
        /// 생성 및 수정시 발생한 아이디를 통해 자동 선택되도록 유도하며 삭제시에는 null로 매개변수를 받아서 첫번째 행을 보여준다. 행이 없다면 빈 값으로 설정
        /// </summary>
        /// <param name="inboudNo">입력 및 수정시 발생한 값의 아이디.</param>
        private void Research()
        {
            // TODO : 조회 SP 변경
            InitializeInsertForm();
            // TODO : 조회 SP 변경
            var values = Conditions.GetValues();

            #region 기간 검색형 전환 처리 - 만약 시간까지 포함하여 검색한다면 아래 문구는 필요없음            
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            values.Add("USERID", UserInfo.Current.Id);
            values.Add("CURRENTLOGINID", UserInfo.Current.Id);
            #endregion

            if (Conditions.GetValue("AREANAME").ToString() != "")
                values.Add("AREAID", _searchAreaID);

            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable productResult = SqlExecuter.Query("GetProductdefidPoplistByTool", "10001", values);

            //values = Commons.CommonFunction.ConvertParameter(values);
            DataTable filmResult = SqlExecuter.Query("GetRequestMakingFilmListByTool", "10001", values);

            grdProduct.DataSource = productResult;

            grdFilm.DataSource = filmResult;

            if (filmResult.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }
            //else
            //{
            //    grdFilm.View.FocusedRowHandle = 0;

            //    DisplayFilmRequestInfo();

            //    SearchActionFilm(grdFilm.View.GetDataRow(0).GetString("REQUESTDEPARTMENT")
            //        , grdFilm.View.GetDataRow(0).GetString("REQUESTUSERID")
            //        , grdFilm.View.GetDataRow(0).GetString("REQUESTDATE")
            //        , grdFilm.View.GetDataRow(0).GetString("FILMPROGRESSSTATUSID"));
            //}
        }
        #endregion

        #region SearchActionFilm - 입력용 그리드 조회
        private void SearchActionFilm(string departmentID, string userID, string requestDate, string filmprogressstatus)
        {
            // TODO : 조회 SP 변경
            Dictionary<string, object> values = new Dictionary<string, object>();

            DateTime requestDateTime = new DateTime();



            #region 기간 검색형 전환 처리 - 만약 시간까지 포함하여 검색한다면 아래 문구는 필요없음            
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            values.Add("PLANTID", UserInfo.Current.Plant);
            values.Add("USERID", UserInfo.Current.Id);
            values.Add("CURRENTLOGINID", UserInfo.Current.Id);

            values.Add("REQUESTUSER", userID);
            values.Add("REQUESTDEPARTMENT", departmentID);

            if (DateTime.TryParse(requestDate, out requestDateTime))
                values.Add("REQUESTDATE", requestDateTime.ToString("yyyy-MM-dd HH:mm:ss"));

            values.Add("PRODUCTDEFID", grdProduct.GetFieldValue("PRODUCTDEFID"));
            values.Add("PRODUCTDEFVERSION", grdProduct.GetFieldValue("PRODUCTDEFVERSION"));
            values.Add("FILMPROGRESSSTATUSID", filmprogressstatus);
            #endregion

            DataTable actionResult = SqlExecuter.Query("GetActionFilmInfoForInsertByTool", "10001", values);

            grdActionFilm.DataSource = actionResult;

            if (actionResult.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                grdActionFilm.View.ClearDatas();

                //데이터가 없으므로 입력상태로 변경
                InitializeInsertForm();
            }
            else
            {
                DataRow filmInfo = grdFilm.View.GetFocusedDataRow();
                //txtRequestUser.EditValue = filmInfo.GetString("REQUESTUSER");
                //txtRequestDate.EditValue = filmInfo.GetString("REQUESTDATE").Substring(0, 10);
                //txtRequestDepartment.EditValue = filmInfo.GetString("REQUESTDEPARTMENT");
                InitializeUserInfoGridSetting(filmInfo.GetString("REQUESTDATE").Substring(0, 10), filmInfo.GetString("REQUESTUSER"), filmInfo.GetString("REQUESTDEPARTMENT"));
                //요청 버튼을 숨기고 재요청 버튼을 조회한다.
                if (pnlToolbar.Controls["layoutToolbar"] != null)
                {
                    //if (pnlToolbar.Controls["layoutToolbar"].Controls["Request"] != null)
                    //    pnlToolbar.Controls["layoutToolbar"].Controls["Request"].Visible = false;
                    //if (pnlToolbar.Controls["layoutToolbar"].Controls["RequestAgain"] != null)
                    //    pnlToolbar.Controls["layoutToolbar"].Controls["RequestAgain"].Visible = true;

                    //그리드의 입력추가 버튼 제어
                    if ((pnlToolbar.Controls["layoutToolbar"].Controls["Request"] != null && pnlToolbar.Controls["layoutToolbar"].Controls["Request"].Visible == true)
                        || (pnlToolbar.Controls["layoutToolbar"].Controls["RequestAgain"] != null && pnlToolbar.Controls["layoutToolbar"].Controls["RequestAgain"].Visible == true))
                    {
                        grdActionFilm.GridButtonItem = GridButtonItem.Delete | GridButtonItem.Expand | GridButtonItem.Export | GridButtonItem.Restore;  // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기  
                    }
                    else
                    {
                        grdActionFilm.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Export | GridButtonItem.Restore;  // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기  
                    }


                }
            }
        }
        #endregion

        #region 조회조건 제어 - 검색조회조건을 설정
        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            InitializeConditionFilmProgressStatus();
            InitializeConditionProductPopup();
            InitializeReceiptArea();
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
        }

        #region InitializeConditionPlant : 사이트 검색조건 - 아래메소드 사용하지 않음(Site 권한에 맞추어 조회조건 추가후 사용)
        private void InitializeConditionPlant()
        {
            var planttxtbox = Conditions.AddComboBox("PLANTID", new SqlQuery("GetPlantList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PLANTID", "PLANTID")
               .SetLabel("PLANT")
               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
               .SetPosition(0.1)
               .SetIsReadOnly(true)
               .SetDefault(UserInfo.Current.Plant, "PLANTID") //기본값 설정 UserInfo.Current.Plant
            ;
        }
        #endregion

        #region InitializeConditionFilmProgressStatus : 진행상태 조회조건
        /// <summary>
        /// 진행상태 조회조건
        /// </summary>
        private void InitializeConditionFilmProgressStatus()
        {
            var planttxtbox = Conditions.AddComboBox("FILMPROGRESSSTATUS", new SqlQuery("GetFilmProgressStatusCodeListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"FILMSCREENSTATUS=MakeFilm"), "CODENAME", "CODEID")
               .SetLabel("FILMPROGRESSSTATUS")
               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
               .SetPosition(3.4)
               .SetEmptyItem("", "", true)
            ;
        }
        #endregion

        #region InitializeConditionJobType : 작업구분 조회조건
        /// <summary>
        /// 진행상태 조회조건
        /// </summary>
        private void InitializeConditionJobType()
        {
            var planttxtbox = Conditions.AddComboBox("JOBTYPEID", new SqlQuery("GetCodeListByToolWithLanguage", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CODECLASSID=JobType"), "CODENAME", "CODEID")
               .SetLabel("JOBTYPE")
               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
               .SetPosition(3.1)
               .SetEmptyItem("", "", true)
            ;
        }
        #endregion

        #region InitializeConditionProductionType : 생산구분 조회조건
        /// <summary>
        /// 진행상태 조회조건
        /// </summary>
        private void InitializeConditionProductionType()
        {
            var planttxtbox = Conditions.AddComboBox("PRODUCTIONTYPEID", new SqlQuery("GetCodeListByToolWithLanguage", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CODECLASSID=ProductionType"), "CODENAME", "CODEID")
               .SetLabel("PRODUCTIONTYPE")
               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
               .SetPosition(3.2)
               .SetEmptyItem("", "", true)
            ;
        }
        #endregion

        #region InitializeConditionToolCodePopup : 품목코드 검색조건
        /// <summary>
        /// 품목코드 조회 설정
        /// </summary>
        private void InitializeConditionProductPopup()
        {
            ConditionItemSelectPopup productPopup = Conditions.AddSelectPopup("PRODUCTDEFID", new SqlQuery("GetProductdefidPoplistByTool", "10001", $"LANGUAGETYPE={ UserInfo.Current.LanguageType }", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", "PRODUCTDEFTYPE=Product"))
            .SetPopupLayout("PRODUCTDEFIDPOPUP", PopupButtonStyles.Ok_Cancel, true, false)
            .SetPopupLayoutForm(984, 465, FormBorderStyle.FixedToolWindow)
            .SetPopupResultMapping("PRODUCTDEFID", "PRODUCTDEFID")
            .SetLabel("PRODUCTDEFID")
            .SetPopupResultCount(1)
            .SetPosition(0.9)
            .SetValidationIsRequired()
            .SetPopupApplySelection((selectedRows, dataGridRows) =>
            {
                //품목코드 변경시 조회조건에 데이터 변경
                selectedRows.Cast<DataRow>().ForEach(row =>
                {
                    Conditions.SetValue("p_filmVersion", 0, row.GetString("PRODUCTDEFVERSION"));
                    Conditions.SetValue("p_productDefName", 0, row.GetString("PRODUCTDEFNAME"));
                });

            });

            productPopup.ValueFieldName = "PRODUCTDEFID";
            productPopup.DisplayFieldName = "PRODUCTDEFID";

            // 팝업 조회조건
            productPopup.Conditions.AddTextBox("PRODUCTDEF")
                .SetLabel("PRODUCTDEF");
            //toolCodePopup.Conditions.AddComboBox("PRODUCTDEFTYPE", new SqlQuery("GetCodeListByOspProductdeftype", "10001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType } }), "CODENAME", "CODEID")
            //    .SetEmptyItem("", "", true)
            //    .SetLabel("PRODUCTDEFTYPE")
            //    .ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly                
            //    ;PRODUCTDEFVERSION

            // 팝업 그리드
            productPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 250)
                .SetIsReadOnly();
            productPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 80)
                .SetIsReadOnly();
            productPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 350)
                .SetIsReadOnly();
            productPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFCODE", 50)
                .SetIsHidden();
            productPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFTYPE", 200)
                .SetIsReadOnly();
        }
        #endregion

        #region InitializeReceiptArea : 작업장 검색조건
        /// <summary>
        /// 작업장 검색조건
        /// </summary>
        private void InitializeReceiptArea()
        {
            receiptAreaPopup = Conditions.AddSelectPopup("AREANAME", new SqlQuery("GetAreaListByTool", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CURRENTLOGINID={UserInfo.Current.Id}"))
            .SetPopupLayout("RECEIPTAREA", PopupButtonStyles.Ok_Cancel, true, true)
            .SetPopupLayoutForm(600, 800, FormBorderStyle.FixedToolWindow)
            .SetPopupAutoFillColumns("AREANAME")
            .SetLabel("RECEIPTAREA")
            .SetPopupResultCount(1)
            .SetPosition(3.3)
            .SetPopupResultMapping("AREANAME", "AREANAME")
            .SetPopupApplySelection((selectedRows, dataGridRows) =>
            {
                //작업장 변경 시 작업자 조회
                selectedRows.Cast<DataRow>().ForEach(row =>
                {
                    _searchAreaID = row.GetString("AREAID");
                });

            });


            receiptAreaPopup.Conditions.AddTextBox("AREANAME");

            receiptAreaPopup.GridColumns.AddTextBoxColumn("AREAID", 150)
                .SetIsHidden();
            receiptAreaPopup.GridColumns.AddTextBoxColumn("AREANAME", 200);
        }
        #endregion

        #region InitializeFilmCode : 필름코드 검색조건
        /// <summary>
        /// 작업장 검색조건
        /// </summary>
        private void InitializeFilmCode()
        {
            filmCodeCondition = Conditions.AddSelectPopup("FILMCODE", new SqlQuery("GetFilmCodePopupListByTool", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CURRENTLOGINID={UserInfo.Current.Id}"))
            .SetPopupLayout("FILMCODE", PopupButtonStyles.Ok_Cancel, true, true)
            .SetPopupLayoutForm(984, 465, FormBorderStyle.FixedToolWindow)
            //.SetPopupAutoFillColumns("FILMCODE")
            .SetLabel("FILMCODE")
            .SetPopupResultCount(1)
            .SetPosition(1.2)
            .SetPopupResultMapping("FILMCODE", "FILMCODE")
            ;

            filmCodeCondition.Conditions.AddTextBox("PRODUCTDEFNAME");
            filmCodeCondition.Conditions.AddTextBox("PRODUCTDEFID");
            segmentCondition = filmCodeCondition.Conditions.AddComboBox("PROCESSSEGMENTVERSION", new SqlQuery("GetProcessSegmentByTool", "10001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "ENTERPRISEID", UserInfo.Current.Enterprise }, { "PLANTID", UserInfo.Current.Plant } }), "PROCESSSEGEMENTNAME", "PROCESSSEGEMENTID");
            segmentCondition.SetEmptyItem("", "", true);
            segmentCondition.SetLabel("PROCESSSEGMENTVERSION");
            segmentCondition.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;

            filmCodeCondition.GridColumns.AddTextBoxColumn("FILMCODE", 150).SetIsReadOnly();
            filmCodeCondition.GridColumns.AddTextBoxColumn("FILMVERSION", 80).SetIsReadOnly();
            filmCodeCondition.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150).SetIsReadOnly();
            filmCodeCondition.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetIsHidden();
            filmCodeCondition.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 300).SetIsReadOnly();
            filmCodeCondition.GridColumns.AddTextBoxColumn("FILMCATEGORYID", 10).SetIsHidden();
            filmCodeCondition.GridColumns.AddTextBoxColumn("FILMCATEGORY", 100).SetIsReadOnly();
            filmCodeCondition.GridColumns.AddTextBoxColumn("FILMDETAILCATEGORYID", 10).SetIsHidden();
            filmCodeCondition.GridColumns.AddTextBoxColumn("FILMDETAILCATEGORY", 100).SetIsReadOnly();
            filmCodeCondition.GridColumns.AddTextBoxColumn("PROCESSSEGMENTVERSION", 100).SetIsReadOnly();
            filmCodeCondition.GridColumns.AddTextBoxColumn("FILMUSELAYER", 100).SetIsHidden();
            filmCodeCondition.GridColumns.AddTextBoxColumn("JOBTYPEID", 100).SetIsHidden();
            filmCodeCondition.GridColumns.AddTextBoxColumn("JOBTYPE", 100).SetIsHidden();
            filmCodeCondition.GridColumns.AddTextBoxColumn("CUSTOMERID", 100).SetIsHidden();
            filmCodeCondition.GridColumns.AddTextBoxColumn("CUSTOMERNAME", 100).SetIsHidden();
        }
        #endregion
        #endregion

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();
        }

        #region ValidateCurrentDeleteStatus : 현재 정보가 삭제 가능한 정보인지 검증
        private bool ValidateCurrentDeleteStatus(DataRow currentRow, out string messageCode)
        {
            messageCode = "";


            return true;
        }
        #endregion

        #region ValidateCurrentStatus : 현재 저장요청한 정보가 저장 가능한 정보인지 검증
        private bool ValidateCurrentStatus(string wishStatus, out string messageCode)
        {
            messageCode = "";

            DataTable changedTable = null;

            if (wishStatus.Equals("Request"))
                changedTable = ((DataTable)grdActionFilm.DataSource);
            else
                changedTable = grdActionFilm.GetChangedRows();


            foreach (DataRow row in changedTable.Rows)
            {
                //입력상태라면 저장가능
                if (wishStatus.Equals("Request"))
                {
                    //changedTable = ((DataTable)grdActionFilm.View.DataSource);
                    //if (row.GetString("FILMPROGRESSSTATUSID").Equals("") && !row.GetString("_STATE_").Equals("added"))
                    //{
                    //    messageCode = "ValidateFilmInitialStatusForRequest";
                    //    return false;
                    //}
                }
                else
                {
                    //재요청대상의 경우 재요청가능
                    if (!row.GetString("FILMPROGRESSSTATUSID").Equals("CancelAccept"))
                    {
                        messageCode = "ValidateFilmInitialStatusForRequest";
                        return false;
                    }
                }

                if (row.GetString("FILMVERSION").Equals(""))
                {
                    messageCode = "VALIDATEREQUIREDVALUES";
                    return false;
                }

                if (row.GetString("FILMCATEGORY").Equals(""))
                {
                    messageCode = "VALIDATEREQUIREDVALUES";
                    return false;
                }

                if (row.GetString("FILMDETAILCATEGORY").Equals(""))
                {
                    messageCode = "VALIDATEREQUIREDVALUES";
                    return false;
                }

                //Layer는 필수조건이 아님
                //if (row.GetString("FILMUSELAYER1").Equals(""))
                //{
                //    messageCode = "VALIDATEREQUIREDVALUES";
                //    return false;
                //}

                //if (row.GetString("FILMUSELAYER2").Equals(""))
                //{
                //    messageCode = "VALIDATEREQUIREDVALUES";
                //    return false;
                //}

                //품목검색이 되지 않았을 경우
                if (grdProduct.DataSource == null)
                {
                    messageCode = "VALIDATEREQUIREDVALUES"; //선택된 품목이 없습니다.
                    return false;
                }

                if (grdProduct.GetFieldValue("PRODUCTDEFID").Equals("") || grdProduct.GetFieldValue("PRODUCTDEFVERSION").Equals(""))
                {
                    messageCode = "VALIDATEREQUIREDVALUES"; //선택된 품목이 없습니다.
                    return false;
                }
                
                if (row.GetString("AREAID").Equals(""))
                {
                    messageCode = "VALIDATEREQUIREDVALUES";
                    return false;
                }
                
                if (row.GetString("QTY").Equals(""))
                {
                    messageCode = "VALIDATEREQUIREDVALUES";
                    return false;
                }
            }

            return true;
        }
        #endregion

        #region ValidateContent - 내용에 대한 유효성 검사 - 현재 사용하지 않음
        private bool ValidateContent()
        {
            //if (!ValidateEditValue(deRequestDate.EditValue))
            //    return false;

            //if (!ValidateEditValue(txtRequestUser.EditValue))
            //    return false;

            //if (!ValidateEditValue(txtRequestDepartment.EditValue))
            //    return false;

            //if (ucFilmCode.FilmCode == null && ucFilmCode.FilmCode == "")
            //    return false;

            //if (ucFilmCode.FilmVersion == null && ucFilmCode.FilmVersion == "")
            //    return false;

            //if (!ValidateEditValue(txtProductDefID.EditValue))
            //    return false;

            //if (!ValidateEditValue(popEditReceiptArea.EditValue))
            //    return false;

            //if (!ValidateNumericBox(txtRequestQty, 0))
            //    return false;

            return true;
        }
        #endregion

        #region ValidateComboBoxEqualValue - 2개의 콤보박스의 데이터를 비교한다.
        private bool ValidateComboBoxEqualValue(SmartComboBox originBox, SmartComboBox targetBox)
        {
            //두 콤보박스의 값이 같으면 안된다.
            if (originBox.EditValue.Equals(targetBox.EditValue))
                return false;
            return true;
        }
        #endregion

        #region ValidateNumericBox - 텍스트 박스중 숫자형에 대한 점검 (특정 숫자보다 작을 경우도 검증)
        private bool ValidateNumericBox(SmartTextBox originBox, int ruleValue)
        {
            //값이 없으면 안된다.
            if (originBox.EditValue == null)
                return false;

            int resultValue = 0;

            //입력받은 기준값(예를 들어 0)보다 작다면 false를 반환
            if (Int32.TryParse(originBox.EditValue.ToString(), out resultValue))
                if (resultValue <= ruleValue)
                    return false;

            //모두 통과했으므로 Validation Check완료
            return true;
        }
        #endregion

        #region ValidateEditValue - 데이터에 대한 기본검증
        private bool ValidateEditValue(object editValue)
        {
            if (editValue == null)
                return false;

            if (editValue.ToString() == "")
                return false;

            return true;
        }
        #endregion

        #region ValidateCellInGrid - 그리드내 특정 셀에 대한 검증
        private bool ValidateCellInGrid(DataRow currentRow, string[] columnNames)
        {
            foreach (string columnName in columnNames)
            {
                if (currentRow[columnName] == null)
                    return false;
                if (currentRow[columnName].ToString() == "")
                    return false;
            }

            return true;
        }
        #endregion

        #region SetRequiredValidationControl - 필수입력 컨트롤을 설정
        private void SetRequiredValidationControl(Control requiredControl)
        {
            requiredControl.ForeColor = Color.Red;
        }
        #endregion

        #region ValidateProductCode : 품목코드와 관계된 치공구를 부르기 위해 품목코드가 입력되었는지 확인한다.
        private bool ValidateProductCode()
        {
            //if (ucFilmCode.FilmCode.Equals(""))
            //    return false;

            return true;
        }
        #endregion

        #endregion

        #region Private Function
        #region InitializeInsertForm : 데이터를 입력하기 위한 화면 초기화
        private void InitializeInsertForm()
        {
            try
            {
                _filmSequence = null;            //RequestSequence는 공값이어야 한다.
                _currentStatus = "added";           //현재 화면의 상태는 입력중이어야 한다.

                //의뢰일은 오늘날짜를 기본으로 한다.
                DateTime dateNow = DateTime.Now;
                txtRequestDate.EditValue = dateNow.ToString("yyyy-MM-dd");

                //의뢰순번은 공값으로 한다.
                txtRequestUser.EditValue = UserInfo.Current.Name;
                _requestUserID = UserInfo.Current.Id;

                //의뢰부서는 사용자의 부서를 기본으로 한다.
                txtRequestDepartment.EditValue = UserInfo.Current.Department;
                InitializeUserInfoGrid();
                grdActionFilm.View.ClearDatas();
                grdFilm.View.ClearDatas();
                //컨트롤 접근여부는 작성상태로 변경한다.
                ControlEnableProcess("added");

                if (pnlToolbar.Controls["layoutToolbar"] != null)
                {
                    if (pnlToolbar.Controls["layoutToolbar"].Controls["Request"] != null)
                        pnlToolbar.Controls["layoutToolbar"].Controls["Request"].Visible = true;
                    if (pnlToolbar.Controls["layoutToolbar"].Controls["RequestAgain"] != null)
                        pnlToolbar.Controls["layoutToolbar"].Controls["RequestAgain"].Visible = false;
                    if (pnlToolbar.Controls["layoutToolbar"].Controls["Copy"] != null)
                        pnlToolbar.Controls["layoutToolbar"].Controls["Copy"].Visible = true;
                    if (pnlToolbar.Controls["layoutToolbar"].Controls["Delete"] != null)
                        pnlToolbar.Controls["layoutToolbar"].Controls["Delete"].Visible = true;
                }
                //btnReRequest.Visible = false;
                //btnModify.Visible = true;
                grdActionFilm.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Expand | GridButtonItem.Export | GridButtonItem.Restore;  // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기            
            }
            catch (Exception err)
            {
                ShowError(err);
            }
        }
        #endregion

        #region controlEnableProcess : 입력/수정/삭제를 위한 화면내 컨트롤들의 Enable 제어
        private void ControlEnableProcess(string currentStatus)
        {
            if (currentStatus == "added") //초기화 버튼을 클릭한 경우
            {
                txtRequestDate.ReadOnly = true;
                txtRequestUser.ReadOnly = true;
                txtRequestDepartment.ReadOnly = true;
            }
            else if (currentStatus == "modified") //
            {
                txtRequestDate.ReadOnly = true;
                txtRequestUser.ReadOnly = true;
                txtRequestDepartment.ReadOnly = true;
            }
            else
            {
                txtRequestDate.ReadOnly = true;
                txtRequestUser.ReadOnly = true;
                txtRequestDepartment.ReadOnly = true;
            }
        }
        #endregion

        #region CreateSaveDatatable : 데이터 입력/수정/삭제를 위한 DataTable의 Template 생성
        private DataTable CreateSaveDatatable()
        {
            DataTable dt = new DataTable();
            dt.TableName = "filmRequestMakingList";
            //===================================================================================
            dt.Columns.Add("FILMSEQUENCE");
            dt.Columns.Add("ENTERPRISEID");
            dt.Columns.Add("PLANTID");
            dt.Columns.Add("REQUESTDATE");
            dt.Columns.Add("REQUESTDEPARTMENT");
            dt.Columns.Add("REQUESTUSERID");
            dt.Columns.Add("FILMCODE");
            dt.Columns.Add("FILMVERSION");
            dt.Columns.Add("CONTRACTIONX");
            dt.Columns.Add("CONTRACTIONY");
            dt.Columns.Add("RESOLUTION");
            dt.Columns.Add("ISCOATING");
            dt.Columns.Add("QTY");
            dt.Columns.Add("PRIORITYID");
            dt.Columns.Add("USEPLANDATE");
            dt.Columns.Add("RECEIPTAREAID");
            dt.Columns.Add("MAKEVENDORID");
            dt.Columns.Add("REQUESTCOMMENT");
            dt.Columns.Add("FILMPROGRESSSTATUS");

            //DurableDefinition-------------------------------------------------------------
            dt.Columns.Add("DURABLEDEFNAME$$KO-KR");
            dt.Columns.Add("DURABLEDEFNAME$$EN-US");
            dt.Columns.Add("DURABLEDEFNAME$$ZH-CN");
            dt.Columns.Add("DURABLEDEFNAME$$VI-VN");
            dt.Columns.Add("DURABLECLASSID");
            dt.Columns.Add("TOOLTYPEID");
            dt.Columns.Add("TOOLDETAILTYPEID");
            dt.Columns.Add("PRODUCTDEFID");
            dt.Columns.Add("PRODUCTDEFVERSION");
            dt.Columns.Add("LAYER1");
            dt.Columns.Add("LAYER2");
            dt.Columns.Add("DESCRIPTION");
            //------------------------------------------------------------------------------

            dt.Columns.Add("CREATOR");
            dt.Columns.Add("CREATEDTIME");
            dt.Columns.Add("MODIFIER");
            dt.Columns.Add("MODIFIEDTIME");
            dt.Columns.Add("LASTTXNHISTKEY");
            dt.Columns.Add("LASTTXNID");
            dt.Columns.Add("LASTTXNUSER");
            dt.Columns.Add("LASTTXNTIME");
            dt.Columns.Add("LASTTXNCOMMENT");
            dt.Columns.Add("VALIDSTATE");
            dt.Columns.Add("_STATE_");

            return dt;
        }
        #endregion

        #region SaveData : 입력/수정을 수행 - 요청
        private void SaveData()
        {
            //Validation 체크 부분 작성 필요
            grdActionFilm.View.FocusedRowHandle = grdActionFilm.View.FocusedRowHandle;
            grdActionFilm.View.FocusedColumn = grdActionFilm.View.Columns["FILMPROGRESSSTATUS"];
            grdActionFilm.View.ShowEditor();
            this.ShowWaitArea();

            //저장 로직
            try
            {
                string messageCode = "";
                if (grdActionFilm.View.RowCount > 0)
                {
                    //신규 혹은 요청 상태만 변경가능하다.
                    if (ValidateCurrentStatus("Request", out messageCode))
                    {
                        DataSet filmReqSet = new DataSet();
                        //치공구 제작의뢰를 입력
                        DataTable filmReqTable = CreateSaveDatatable();

                        DateTime requestDate = DateTime.Now;

                        DataTable actionTable = ((DataTable)grdActionFilm.DataSource);

                        foreach (DataRow actionRow in actionTable.Rows)
                        {
                            DataRow filmReqRow = filmReqTable.NewRow();

                            filmReqRow["REQUESTDATE"] = requestDate.ToString("yyyy-MM-dd HH:mm:ss");

                            filmReqRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                            filmReqRow["PLANTID"] = Conditions.GetValue("P_PLANTID");

                            filmReqRow["REQUESTDEPARTMENT"] = UserInfo.Current.Department;
                            filmReqRow["REQUESTUSERID"] = UserInfo.Current.Id;

                            filmReqRow["FILMCODE"] = actionRow.GetString("FILMCODE");
                            filmReqRow["FILMVERSION"] = actionRow.GetString("FILMVERSION");

                            filmReqRow["CONTRACTIONX"] = actionRow.GetString("CONTRACTIONX");
                            filmReqRow["CONTRACTIONY"] = actionRow.GetString("CONTRACTIONY");

                            filmReqRow["RESOLUTION"] = actionRow.GetString("RESOLUTION");
                            filmReqRow["ISCOATING"] = actionRow.GetString("ISCOATING");
                            filmReqRow["QTY"] = actionRow.GetString("QTY");
                            filmReqRow["PRIORITYID"] = actionRow.GetString("PRIORITY");
                            filmReqRow["USEPLANDATE"] = actionRow.GetString("USEPLANDATE");
                            filmReqRow["RECEIPTAREAID"] = actionRow.GetString("AREAID");
                            filmReqRow["MAKEVENDORID"] = actionRow.GetString("VENDORID");
                            filmReqRow["REQUESTCOMMENT"] = actionRow.GetString("REQUESTCOMMENT");

                            filmReqRow["DURABLEDEFNAME$$KO-KR"] = actionRow.GetString("FILMVERSION") + "_" + actionRow.GetString("FILMCATEGORY") + "_" + actionRow.GetString("FILMDETAILCATEGORY");
                            filmReqRow["DURABLEDEFNAME$$EN-US"] = actionRow.GetString("FILMVERSION") + "_" + actionRow.GetString("FILMCATEGORY") + "_" + actionRow.GetString("FILMDETAILCATEGORY");
                            filmReqRow["DURABLEDEFNAME$$ZH-CN"] = actionRow.GetString("FILMVERSION") + "_" + actionRow.GetString("FILMCATEGORY") + "_" + actionRow.GetString("FILMDETAILCATEGORY");
                            filmReqRow["DURABLEDEFNAME$$VI-VN"] = actionRow.GetString("FILMVERSION") + "_" + actionRow.GetString("FILMCATEGORY") + "_" + actionRow.GetString("FILMDETAILCATEGORY");

                            filmReqRow["DURABLECLASSID"] = "ToolTypeA";//actionRow.GetString("DURABLECLASSID");
                            filmReqRow["TOOLTYPEID"] = actionRow.GetString("FILMCATEGORY");
                            filmReqRow["TOOLDETAILTYPEID"] = actionRow.GetString("FILMDETAILCATEGORY");
                            filmReqRow["PRODUCTDEFID"] = grdProduct.GetFieldValue("PRODUCTDEFID");
                            filmReqRow["PRODUCTDEFVERSION"] = grdProduct.GetFieldValue("PRODUCTDEFVERSION");
                            filmReqRow["LAYER1"] = actionRow.GetString("FILMUSELAYER1");
                            filmReqRow["LAYER2"] = actionRow.GetString("FILMUSELAYER2");
                            filmReqRow["DESCRIPTION"] = actionRow.GetString("REQUESTCOMMENT");

                            filmReqRow["VALIDSTATE"] = "Valid";

                            //현재 화면의 상태에 따라 생성/수정으로 분기된다. --무조건 초기화이후 작업해야 하므로 모두 입력대상
                            //if (actionRow.GetString("_STATE_").Equals("added")|| actionRow.GetString("_STATE_").Equals(""))
                            //{
                           if (actionRow.GetString("FILMSEQUENCE").Equals(""))
                           { 
                                filmReqRow["FILMPROGRESSSTATUS"] = "Request";
                                filmReqRow["CREATOR"] = UserInfo.Current.Id;
                                filmReqRow["_STATE_"] = "added";
                           }
                            else
                            {
                            //    //수정할 PK를 전달
                                filmReqRow["FILMSEQUENCE"] = actionRow.GetString("FILMSEQUENCE");
                                filmReqRow["MODIFIER"] = UserInfo.Current.Id;
                                filmReqRow["_STATE_"] = "modified";
                            }

                            //입력될 행은 항상 UNIQ해야 한다.
                            if (GetIsUniqData(filmReqTable, filmReqRow))
                                filmReqTable.Rows.Add(filmReqRow);
                            else
                            {
                                ShowMessage(MessageBoxButtons.OK, "FilmMultiUniqValidation", ""); //FilmMultiUniqValidation 입력될 필름의 목록에서 각각의 Version, 필름유형, 상세유형, CS사용층, SS사용층은 중복되지 않아야 합니다.
                                return;//로직 종료
                            }
                        }

                        filmReqSet.Tables.Add(filmReqTable);
                        DataTable saveResult = this.ExecuteRule<DataTable>("FilmRequestMaking", filmReqSet);
                        DataRow resultData = saveResult.Rows[0];
                        string strtempRequestno = resultData.ItemArray[0].ToString();
                        //ExecuteRule<DataTable>("FilmRequestMaking", filmReqSet);

                        ControlEnableProcess("modified");

                        ////가져온 결과값에 따라 재검색한 후 가져온 아이디에 맞는 행을 선택으로 변경한다.
                        Research();
                        int irow = GetGridRowSearch(strtempRequestno);
                        if (irow >= 0)
                        {
                            grdFilm.View.FocusedRowHandle = irow;
                            grdFilm.View.SelectRow(irow);
                            DataRow dr = grdFilm.View.GetDataRow(irow);
                            DisplayInfoDetail(dr);
                            SearchActionFilm(grdFilm.View.GetDataRow(irow).GetString("REQUESTDEPARTMENT")
                                           , grdFilm.View.GetDataRow(irow).GetString("REQUESTUSERID")
                                           , grdFilm.View.GetDataRow(irow).GetString("REQUESTDATE")
                                           , grdFilm.View.GetDataRow(irow).GetString("FILMPROGRESSSTATUSID")
                                           );
                                           
                        }
                        
                        //else
                        //{
                        //    //ShowMessage(MessageBoxButtons.OK, "VALIDATEREQUIREDVALUES", "");
                        //}
                    }
                    else
                    {
                        ShowMessage(MessageBoxButtons.OK, messageCode, "");
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
            finally
            {
                this.CloseWaitArea();
            }
        }

        private int GetGridRowSearch(string strRequestno)
        {
            int iRow = -1;
            if (grdFilm.View.DataRowCount == 0)
            {
                return iRow;
            }
            for (int i = 0; i < grdFilm.View.DataRowCount; i++)
            {
                if (grdFilm.View.GetRowCellValue(i, "FILMSEQUENCE").ToString().Equals(strRequestno))
                {
                    return i;
                }
            }
            return iRow;
        }
        #endregion

        #region ReSaveData : 재접수를 수행
        private void ReSaveData()
        {
            //Validation 체크 부분 작성 필요
            this.ShowWaitArea();
            grdActionFilm.View.FocusedRowHandle = grdActionFilm.View.FocusedRowHandle;
            grdActionFilm.View.FocusedColumn = grdActionFilm.View.Columns["FILMPROGRESSSTATUS"];
            grdActionFilm.View.ShowEditor();
            //저장 로직
            try
            {
                string messageCode = "";
                //신규 혹은 요청 상태만 변경가능하다.
                if (ValidateCurrentStatus("RequestAgain", out messageCode))
                {
                    DataSet filmReqSet = new DataSet();
                    //치공구 제작의뢰를 입력
                    DataTable filmReqTable = CreateSaveDatatable();

                    DateTime requestDate = DateTime.Now;

                    DataTable actionTable = grdActionFilm.GetChangedRows();

                    foreach (DataRow actionRow in actionTable.Rows)
                    {
                        if (actionRow.GetString("_STATE_").Equals("modified") && actionRow.GetString("FILMPROGRESSSTATUSID").Equals("CancelAccept"))
                        {
                            DataRow filmReqRow = filmReqTable.NewRow();

                            filmReqRow["REQUESTDATE"] = requestDate.ToString("yyyy-MM-dd HH:mm:ss");

                            filmReqRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                            filmReqRow["PLANTID"] = Conditions.GetValue("P_PLANTID");

                            filmReqRow["REQUESTDEPARTMENT"] = UserInfo.Current.Department;
                            filmReqRow["REQUESTUSERID"] = UserInfo.Current.Id;

                            filmReqRow["FILMCODE"] = actionRow.GetString("FILMCODE");
                            filmReqRow["FILMVERSION"] = actionRow.GetString("FILMVERSION");

                            filmReqRow["CONTRACTIONX"] = actionRow.GetString("CONTRACTIONX");
                            filmReqRow["CONTRACTIONY"] = actionRow.GetString("CONTRACTIONY");

                            filmReqRow["RESOLUTION"] = actionRow.GetString("RESOLUTION");
                            filmReqRow["ISCOATING"] = actionRow.GetString("ISCOATING");
                            filmReqRow["QTY"] = actionRow.GetString("QTY");
                            filmReqRow["PRIORITYID"] = actionRow.GetString("PRIORITY");
                            filmReqRow["USEPLANDATE"] = actionRow.GetString("USEPLANDATE");
                            filmReqRow["RECEIPTAREAID"] = actionRow.GetString("AREAID");
                            filmReqRow["MAKEVENDORID"] = actionRow.GetString("VENDORID");
                            filmReqRow["REQUESTCOMMENT"] = actionRow.GetString("REQUESTCOMMENT");

                            filmReqRow["DURABLEDEFNAME$$KO-KR"] = actionRow.GetString("FILMVERSION") + "_" + actionRow.GetString("FILMCATEGORY") + "_" + actionRow.GetString("FILMDETAILCATEGORY");
                            filmReqRow["DURABLEDEFNAME$$EN-US"] = actionRow.GetString("FILMVERSION") + "_" + actionRow.GetString("FILMCATEGORY") + "_" + actionRow.GetString("FILMDETAILCATEGORY");
                            filmReqRow["DURABLEDEFNAME$$ZH-CN"] = actionRow.GetString("FILMVERSION") + "_" + actionRow.GetString("FILMCATEGORY") + "_" + actionRow.GetString("FILMDETAILCATEGORY");
                            filmReqRow["DURABLEDEFNAME$$VI-VN"] = actionRow.GetString("FILMVERSION") + "_" + actionRow.GetString("FILMCATEGORY") + "_" + actionRow.GetString("FILMDETAILCATEGORY");

                            filmReqRow["DURABLECLASSID"] = "ToolTypeA";//actionRow.GetString("DURABLECLASSID");
                            filmReqRow["TOOLTYPEID"] = actionRow.GetString("FILMCATEGORY");
                            filmReqRow["TOOLDETAILTYPEID"] = actionRow.GetString("FILMDETAILCATEGORY");
                            filmReqRow["PRODUCTDEFID"] = grdProduct.GetFieldValue("PRODUCTDEFID");
                            filmReqRow["PRODUCTDEFVERSION"] = grdProduct.GetFieldValue("PRODUCTDEFVERSION");
                            filmReqRow["LAYER1"] = actionRow.GetString("FILMUSELAYER1");
                            filmReqRow["LAYER2"] = actionRow.GetString("FILMUSELAYER2");
                            filmReqRow["DESCRIPTION"] = actionRow.GetString("REQUESTCOMMENT");

                            filmReqRow["VALIDSTATE"] = "Valid";

                            //현재 화면의 상태에 따라 생성/수정으로 분기된다.

                            //수정할 PK를 전달
                            filmReqRow["FILMSEQUENCE"] = actionRow.GetString("FILMSEQUENCE");
                            filmReqRow["MODIFIER"] = UserInfo.Current.Id;
                            filmReqRow["_STATE_"] = "modified";

                            filmReqTable.Rows.Add(filmReqRow);
                        }
                    }

                    filmReqSet.Tables.Add(filmReqTable);

                    //ExecuteRule<DataTable>("FilmRequestMaking", filmReqSet);
                    DataTable saveResult = this.ExecuteRule<DataTable>("FilmRequestMaking", filmReqSet);
                    DataRow resultData = saveResult.Rows[0];
                    string strtempRequestno = resultData.ItemArray[0].ToString();
                    ControlEnableProcess("modified");

                    ////가져온 결과값에 따라 재검색한 후 가져온 아이디에 맞는 행을 선택으로 변경한다.
                    Research();
                    int irow = GetGridRowSearch(strtempRequestno);
                    if (irow >= 0)
                    {
                        grdFilm.View.FocusedRowHandle = irow;
                        grdFilm.View.SelectRow(irow);
                        DataRow dr = grdFilm.View.GetDataRow(irow);
                        DisplayInfoDetail(dr);
                        SearchActionFilm(grdFilm.View.GetDataRow(irow).GetString("REQUESTDEPARTMENT")
                                       , grdFilm.View.GetDataRow(irow).GetString("REQUESTUSERID")
                                       , grdFilm.View.GetDataRow(irow).GetString("REQUESTDATE")
                                       , grdFilm.View.GetDataRow(irow).GetString("FILMPROGRESSSTATUSID")
                                       );
                    }
                }
                else
                {
                    ShowMessage(MessageBoxButtons.OK, messageCode, "");
                }
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
            finally
            {
                this.CloseWaitArea();
            }
        }
        #endregion

        #region DeleteData : 삭제를 수행
        private void DeleteData()
        {
            //Validation 체크 부분 작성 필요

            this.ShowWaitArea();

            //저장 로직
            try
            {
                string messageCode = "";
                if (grdActionFilm.View.GetFocusedDataRow() != null)
                {
                    DialogResult result = this.ShowMessage(MessageBoxButtons.YesNo, "OspDoProcessWant", Language.Get("DELETE"));//삭제하시겠습니까? 

                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        if (ValidateCurrentDeleteStatus(grdActionFilm.View.GetFocusedDataRow(), out messageCode))
                        {
                            DataSet filmReqSet = new DataSet();
                            //치공구 제작의뢰를 입력
                            DataTable filmReqTable = CreateSaveDatatable();
                            DataRow filmReqRow = filmReqTable.NewRow();

                            filmReqRow["FILMSEQUENCE"] = grdActionFilm.View.GetFocusedDataRow().GetString("FILMSEQUENCE");
                            filmReqRow["MODIFIER"] = UserInfo.Current.Id;
                            filmReqRow["VALIDSTATE"] = "Invalid";
                            filmReqRow["_STATE_"] = "deleted";

                            filmReqTable.Rows.Add(filmReqRow);

                            filmReqSet.Tables.Add(filmReqTable);

                            //DataTable resultTable = this.ExecuteRule<DataTable>("FilmRequestMaking", filmReqSet);

                            ////Server에서 결과값을 현재는 반환하지 않음 만약 반환해야 한다면 아래로직 구현
                            //DataRow resultRow = resultTable.Rows[0];
                            DataTable saveResult = this.ExecuteRule<DataTable>("FilmRequestMaking", filmReqSet);
                            DataRow resultData = saveResult.Rows[0];
                            string strtempRequestno = resultData.ItemArray[0].ToString();
                            ControlEnableProcess("modified");

                            ////가져온 결과값에 따라 재검색한 후 가져온 아이디에 맞는 행을 선택으로 변경한다.
                            Research();
                            int irow = GetGridRowSearch(strtempRequestno);
                            if (irow >= 0)
                            {
                                grdFilm.View.FocusedRowHandle = irow;
                                grdFilm.View.SelectRow(irow);
                                DataRow dr = grdFilm.View.GetDataRow(irow);
                                DisplayInfoDetail(dr);
                                SearchActionFilm(grdFilm.View.GetDataRow(irow).GetString("REQUESTDEPARTMENT")
                                               , grdFilm.View.GetDataRow(irow).GetString("REQUESTUSERID")
                                               , grdFilm.View.GetDataRow(irow).GetString("REQUESTDATE")
                                               , grdFilm.View.GetDataRow(irow).GetString("FILMPROGRESSSTATUSID")
                                               );
                            }
                        }
                        else
                        {
                            ShowMessage(MessageBoxButtons.OK, messageCode, null);
                        }
                    }
                }
                else
                {
                    //ShowMessage(MessageBoxButtons.OK, String.Format("SelectItem", Language.GetDictionary("DURABLE")), "");
                    ShowMessage(MessageBoxButtons.OK, "SelectItem", Language.Get("FILM"));
                }
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
            finally
            {
                this.CloseWaitArea();
            }
        }
        #endregion

        #region DisplayFilmRequestInfo : 그리드에서 행 선택시 상세정보를 표시
        /// <summary>
        /// 그리드에서 행 선택시 상세정보를 표시하는 메소드
        /// </summary>
        private void DisplayFilmRequestInfo()
        {
            if (grdFilm.View == null) return;
            //포커스 행 체크 
            if (grdFilm.View.FocusedRowHandle < 0) return;

            DisplayInfoDetail(grdFilm.View.GetFocusedDataRow());
        }
        #endregion

        #region ViewSavedData : 새로 입력된 데이터를 화면에 바인딩
        /// <summary>
        /// 새로 입력된 데이터를 화면에 바인딩
        /// </summary>
        private void ViewSavedData(string filmSequence)
        {
            #region 기간 검색형 전환 처리 - 만약 시간까지 포함하여 검색한다면 아래 문구는 필요없음            
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("FILMSEQUENCE", Int32.Parse(filmSequence));
            #endregion

            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable savedResult = SqlExecuter.Query("GetRequestMakingFilmListByTool", "10001", values);

            if (savedResult.Rows.Count > 0)
            {
                grdFilm.View.FocusedRowHandle = GetRowHandleInGrid(grdFilm, "FILMSEQUENCE", filmSequence);

                DisplayInfoDetail(savedResult.Rows[0]);
            }
        }
        #endregion

        #region DisplayInfoDetail : 입력받은 인덱스의 그리드의 내용을 화면에 바인딩
        private void DisplayInfoDetail(DataRow filmInfo)
        {

            _filmSequence = filmInfo.GetString("FILMSEQUENCE");

            //버튼제어
            if (filmInfo.GetString("FILMPROGRESSSTATUSID").Equals("Request"))   //요청
            {
                //btnReRequest.Visible = true;
                //btnModify.Visible = false;
                if (pnlToolbar.Controls["layoutToolbar"] != null)
                {
                    if (pnlToolbar.Controls["layoutToolbar"].Controls["Request"] != null)
                        pnlToolbar.Controls["layoutToolbar"].Controls["Request"].Visible = true;   //요청
                    if (pnlToolbar.Controls["layoutToolbar"].Controls["RequestAgain"] != null)
                        pnlToolbar.Controls["layoutToolbar"].Controls["RequestAgain"].Visible = false;//재요청 
                    if (pnlToolbar.Controls["layoutToolbar"].Controls["Copy"] != null)
                        pnlToolbar.Controls["layoutToolbar"].Controls["Copy"].Visible = true;//복사
                    if (pnlToolbar.Controls["layoutToolbar"].Controls["Delete"] != null)
                        pnlToolbar.Controls["layoutToolbar"].Controls["Delete"].Visible = true;//삭제
                }
            }
            else if (filmInfo.GetString("FILMPROGRESSSTATUSID").Equals("RequestAgain"))//재요청
            {
                //btnReRequest.Visible = true;
                //btnModify.Visible = false;
                if (pnlToolbar.Controls["layoutToolbar"] != null)
                {
                    if (pnlToolbar.Controls["layoutToolbar"].Controls["Request"] != null)
                        pnlToolbar.Controls["layoutToolbar"].Controls["Request"].Visible = false;
                    if (pnlToolbar.Controls["layoutToolbar"].Controls["RequestAgain"] != null)
                        pnlToolbar.Controls["layoutToolbar"].Controls["RequestAgain"].Visible = true;
                    if (pnlToolbar.Controls["layoutToolbar"].Controls["Copy"] != null)
                        pnlToolbar.Controls["layoutToolbar"].Controls["Copy"].Visible = true;//복사
                    if (pnlToolbar.Controls["layoutToolbar"].Controls["Delete"] != null)
                        pnlToolbar.Controls["layoutToolbar"].Controls["Delete"].Visible = true;//삭제
                }
            }
            else if (filmInfo.GetString("FILMPROGRESSSTATUSID").Equals("CancelAccept"))//접수취소
            {
                //btnReRequest.Visible = true;
                //btnModify.Visible = false;
                if (pnlToolbar.Controls["layoutToolbar"] != null)
                {
                    if (pnlToolbar.Controls["layoutToolbar"].Controls["Request"] != null)
                        pnlToolbar.Controls["layoutToolbar"].Controls["Request"].Visible = false;
                    if (pnlToolbar.Controls["layoutToolbar"].Controls["RequestAgain"] != null)
                        pnlToolbar.Controls["layoutToolbar"].Controls["RequestAgain"].Visible = true;
                    if (pnlToolbar.Controls["layoutToolbar"].Controls["Copy"] != null)
                        pnlToolbar.Controls["layoutToolbar"].Controls["Copy"].Visible = true;//복사
                    if (pnlToolbar.Controls["layoutToolbar"].Controls["Delete"] != null)
                        pnlToolbar.Controls["layoutToolbar"].Controls["Delete"].Visible = true;//삭제
                }
            }
            else
            {
                //btnReRequest.Visible = false;
                //btnModify.Visible = true;
                if (pnlToolbar.Controls["layoutToolbar"] != null)
                {
                    if (pnlToolbar.Controls["layoutToolbar"].Controls["Request"] != null)
                        pnlToolbar.Controls["layoutToolbar"].Controls["Request"].Visible = false;
                    if (pnlToolbar.Controls["layoutToolbar"].Controls["RequestAgain"] != null)
                        pnlToolbar.Controls["layoutToolbar"].Controls["RequestAgain"].Visible = false;
                    if (pnlToolbar.Controls["layoutToolbar"].Controls["Copy"] != null)
                        pnlToolbar.Controls["layoutToolbar"].Controls["Copy"].Visible = false;//복사
                    if (pnlToolbar.Controls["layoutToolbar"].Controls["Delete"] != null)
                        pnlToolbar.Controls["layoutToolbar"].Controls["Delete"].Visible = false;//삭제
                    if (pnlToolbar.Controls["layoutToolbar"].Controls["RequestAgain"] != null)
                        pnlToolbar.Controls["layoutToolbar"].Controls["RequestAgain"].Visible = false;
                }
            }

            //작업장에 관련된 버튼제어
            if (filmInfo.GetString("ISMODIFY").Equals("Y"))
            {
                if (pnlToolbar.Controls["layoutToolbar"].Controls["Request"] != null && pnlToolbar.Controls["layoutToolbar"].Controls["Request"].Visible == true)
                    pnlToolbar.Controls["layoutToolbar"].Controls["Request"].Visible = true;
                if (pnlToolbar.Controls["layoutToolbar"].Controls["RequestAgain"] != null && pnlToolbar.Controls["layoutToolbar"].Controls["RequestAgain"].Visible == true)
                    pnlToolbar.Controls["layoutToolbar"].Controls["RequestAgain"].Visible = true;
                if (pnlToolbar.Controls["layoutToolbar"].Controls["Delete"] != null && pnlToolbar.Controls["layoutToolbar"].Controls["Delete"].Visible == true)
                    pnlToolbar.Controls["layoutToolbar"].Controls["Delete"].Visible = true;
                if (pnlToolbar.Controls["layoutToolbar"].Controls["Copy"] != null && pnlToolbar.Controls["layoutToolbar"].Controls["Copy"].Visible == true)
                    pnlToolbar.Controls["layoutToolbar"].Controls["Copy"].Visible = true;
            }
            else
            {
                if (pnlToolbar.Controls["layoutToolbar"].Controls["Request"] != null && pnlToolbar.Controls["layoutToolbar"].Controls["Request"].Visible == true)
                    pnlToolbar.Controls["layoutToolbar"].Controls["Request"].Visible = false;
                if (pnlToolbar.Controls["layoutToolbar"].Controls["RequestAgain"] != null && pnlToolbar.Controls["layoutToolbar"].Controls["RequestAgain"].Visible == true)
                    pnlToolbar.Controls["layoutToolbar"].Controls["RequestAgain"].Visible = false;
                if (pnlToolbar.Controls["layoutToolbar"].Controls["Delete"] != null && pnlToolbar.Controls["layoutToolbar"].Controls["Delete"].Visible == true)
                    pnlToolbar.Controls["layoutToolbar"].Controls["Delete"].Visible = true;
                if (pnlToolbar.Controls["layoutToolbar"].Controls["Copy"] != null && pnlToolbar.Controls["layoutToolbar"].Controls["Copy"].Visible == true)
                    pnlToolbar.Controls["layoutToolbar"].Controls["Copy"].Visible = false;
            }

            //수정상태라 판단하여 화면 제어
            ControlEnableProcess("modified");

            _currentStatus = "modified";
        }
        #endregion

        #region GetRowHandleInGrid : 특정아이디의 값을 바탕으로 해당 내용을 그리드에서 찾은후 결과값의 그리드내의 인덱스를 반환한다.
        /// <summary>
        /// 특정아이디의 값을 바탕으로 해당 내용을 그리드에서 찾은후 결과값의 그리드내의 인덱스를 반환한다.
        /// 현재로선 String비교만을 한다. DateTime및 기타 다른 값들에 대해선 지원하지 않음
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="findValue"></param>
        /// <returns></returns>
        private int GetRowHandleInGrid(SmartBandedGrid targetGrid, string firstColumnName, string firstFindValue)
        {
            for (int i = 0; i < targetGrid.View.RowCount; i++)
            {
                if (firstFindValue.Equals(targetGrid.View.GetDataRow(i)[firstColumnName].ToString()))
                {
                    return i;
                }
            }
            return -1;
        }
        #endregion

        #region GetContractionPercentage : 요청수축률을 구한다.
        private string GetContractionPercentage(string ruleValue)
        {
            if (!ruleValue.Equals(""))
            {
                //수축율(%) 산출 수식 :: (1 - 수축율) *100, 소수점세자리   수축율은 소수다섯자리까지 입력
                double contractionValue = Convert.ToDouble(ruleValue);

                double resultValue = Math.Round((contractionValue - 1) * 100, 3);

                return Convert.ToString(resultValue);
            }
            return "";
        }
        #endregion

        #region ShowMessageInfo : 메세지를 출력한다. - 팝업창에서 호출
        private void ShowMessageInfo(string messageID)
        {
            ShowMessage(MessageBoxButtons.OK, messageID, "");
        }
        #endregion

        #region ChangeSiteCondition - 검색조건의 Site변경시 관련된 쿼리들을 변경
        private void ChangeSiteCondition()
        {
            if (receiptAreaPopup != null)
                receiptAreaPopup.SearchQuery = new SqlQuery("GetAreaListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}", $"CURRENTLOGINID={UserInfo.Current.Id}");

            if (segmentCondition != null)
                segmentCondition.Query = new SqlQuery("GetProcessSegmentByTool", "10001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "ENTERPRISEID", UserInfo.Current.Enterprise }, { "PLANTID", Conditions.GetValue("P_PLANTID") }, { "CURRENTLOGINID", UserInfo.Current.Id } });

            if (makeVendorPopup != null)
                makeVendorPopup.SearchQuery = new SqlQuery("GetVendorListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}");

            if (areaCondition != null)
                areaCondition.SearchQuery = new SqlQuery("GetAreaListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}", $"CURRENTLOGINID={UserInfo.Current.Id}", $"ISMOD=Y");

            if (filmCodeCondition != null)
                filmCodeCondition.SearchQuery = new SqlQuery("GetFilmCodePopupListByTool", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CURRENTLOGINID={UserInfo.Current.Id}");

            //ucFilmCode.PlantID = Conditions.GetValue("P_PLANTID").ToString();

            //작업장 검색조건 초기화
            ((SmartSelectPopupEdit)Conditions.GetControl("AREANAME")).ClearValue();
        }
        #endregion

        #region DisplayCopyView : 데이터복사 팝업창오픈
        /// <summary>
        /// 새로 입력된 데이터를 화면에 바인딩
        /// </summary>
        private void DisplayCopyView()
        {
            if (grdProduct.DataSource == null)
            {
                ShowMessage(MessageBoxButtons.OK, "InputProudctInfo", "");
            }
            else
            {
                InitializeInsertForm();
                Popup.MultiFilmCodePopup multiPopup = new Popup.MultiFilmCodePopup(
                    grdProduct.GetFieldValue("PRODUCTDEFID").ToString()
                    , grdProduct.GetFieldValue("PRODUCTDEFNAME").ToString()
                    , grdProduct.GetFieldValue("PRODUCTDEFVERSION").ToString()
                    );
                multiPopup.loadDataHandler += LoadData;
                multiPopup.ShowDialog();
            }
        }
        #endregion

        #region LoadData : 복사된 데이터를 화면에 바인딩
        /// <summary>
        /// 새로 입력된 데이터를 화면에 바인딩
        /// </summary>
        private void LoadData(DataTable checkedTable)
        {
            //복사시에는 Priority를 입력하지 않고 기본작업장만 지정하므로 아래 이벤트를 잠시 종료
          //  grdActionFilm.View.AddingNewRow -= grdAction_AddingNewRow;
            int rowHandle = grdActionFilm.View.RowCount;

            foreach (DataRow checkedRow in checkedTable.Rows)
            {
                grdActionFilm.View.AddNewRow();

                //grdActionFilm.View.SetRowCellValue(rowHandle,"FILMSEQUENCE", checkedRow.GetString(""));
                //grdActionFilm.View.SetRowCellValue(rowHandle,"FILMPROGRESSSTATUSID", checkedRow.GetString(""));
                //grdActionFilm.View.SetRowCellValue(rowHandle,"FILMPROGRESSSTATUS", checkedRow.GetString(""));
                grdActionFilm.View.SetRowCellValue(rowHandle, "FILMCODE", checkedRow.GetString("FILMCODE"));
                grdActionFilm.View.SetRowCellValue(rowHandle, "FILMVERSION", checkedRow.GetString("FILMVERSION"));
                //grdActionFilm.View.SetRowCellValue(rowHandle,"FILMTYPE", checkedRow.GetString(""));
                grdActionFilm.View.SetRowCellValue(rowHandle, "FILMCATEGORY", checkedRow.GetString("FILMCATEGORYID"));
                grdActionFilm.View.SetRowCellValue(rowHandle, "FILMDETAILCATEGORY", checkedRow.GetString("FILMDETAILCATEGORYID"));
                //grdActionFilm.View.SetRowCellValue(rowHandle,"JOBTYPE", checkedRow.GetString(""));
                //grdActionFilm.View.SetRowCellValue(rowHandle,"PRODUCTIONTYPE", checkedRow.GetString(""));
                grdActionFilm.View.SetRowCellValue(rowHandle, "FILMUSELAYER1", checkedRow.GetString("LAYER1"));
                grdActionFilm.View.SetRowCellValue(rowHandle, "FILMUSELAYER2", checkedRow.GetString("LAYER2"));
                grdActionFilm.View.SetRowCellValue(rowHandle, "RESOLUTION", checkedRow.GetString("RESOLUTION"));
                grdActionFilm.View.SetRowCellValue(rowHandle, "ISCOATING", checkedRow.GetString("ISCOATING"));
                grdActionFilm.View.SetRowCellValue(rowHandle, "CONTRACTIONX", checkedRow.GetString("CONTRACTIONX"));
                grdActionFilm.View.SetRowCellValue(rowHandle, "CONTRACTIONY", checkedRow.GetString("CONTRACTIONY"));
                grdActionFilm.View.SetRowCellValue(rowHandle, "CHANGECONTRACTIONX", GetContractionPercentage(checkedRow.GetString("CONTRACTIONX")));
                grdActionFilm.View.SetRowCellValue(rowHandle, "CHANGECONTRACTIONY", GetContractionPercentage(checkedRow.GetString("CONTRACTIONY")));
                //grdActionFilm.View.SetRowCellValue(rowHandle,"USEPLANDATE", checkedRow.GetString(""));
                grdActionFilm.View.SetRowCellValue(rowHandle, "PRIORITY", checkedRow.GetString("PRIORITYCODE"));
                grdActionFilm.View.SetRowCellValue(rowHandle, "VENDORID", checkedRow.GetString("VENDORID"));
                grdActionFilm.View.SetRowCellValue(rowHandle, "MAKEVENDOR", checkedRow.GetString("MAKEVENDOR"));
                //grdActionFilm.View.SetRowCellValue(rowHandle, "RECEIPTAREA", checkedRow.GetString("RECEIVEAREA"));
                //grdActionFilm.View.SetRowCellValue(rowHandle, "AREAID", checkedRow.GetString("RECEIVEAREAID"));
                //grdActionFilm.View.SetRowCellValue(rowHandle,"ISMODIFY", checkedRow.GetString(""));
                grdActionFilm.View.SetRowCellValue(rowHandle, "QTY", checkedRow.GetString("QTY"));
                //grdActionFilm.View.SetRowCellValue(rowHandle, "REQUESTCOMMENT", checkedRow.GetString(""));

                //기본 작업장 지정
                SetDefaultArea(grdActionFilm.View.GetDataRow(rowHandle));

                rowHandle++;
            }

            grdActionFilm.View.RaiseValidateRow(rowHandle - 1);
            //grdActionFilm.View.AddingNewRow += grdAction_AddingNewRow;
        }
        #endregion

        #region GetToolDetailType : 툴의 상세유형을 반환
        private DataTable GetToolDetailType(string codeIDPart)
        {
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("CODECLASSID", "ToolDetailType");
            if (!codeIDPart.Equals(""))
                values.Add("CODEID", codeIDPart);

            return SqlExecuter.Query("GetCodeListByFilm", "10001", values);
        }
        #endregion

        #region GetUseCSLayer : 툴의 CS 사용층 코드를 반환
        private DataTable GetUseCSLayer(string codeIDPart)
        {
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("CODECLASSID", "FilmUseLayer1");
            if (!codeIDPart.Equals(""))
                values.Add("CODEID", codeIDPart);

            DataTable resultTable = SqlExecuter.Query("GetCodeListByFilm", "10001", values);

            DataRow emptyRow = resultTable.NewRow();
            emptyRow["CODEID"] = "";
            emptyRow["CODENAME"] = "";

            resultTable.Rows.InsertAt(emptyRow, 0);

            return resultTable;
        }
        #endregion

        #region GetUseSSLayer : 툴의 SS 사용층 코드를 반환
        private DataTable GetUseSSLayer(string codeIDPart)
        {
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("CODECLASSID", "FilmUseLayer2");
            if (!codeIDPart.Equals(""))
                values.Add("CODEID", codeIDPart);

            DataTable resultTable = SqlExecuter.Query("GetCodeListByFilm", "10001", values);

            DataRow emptyRow = resultTable.NewRow();
            emptyRow["CODEID"] = "";
            emptyRow["CODENAME"] = "";

            resultTable.Rows.InsertAt(emptyRow, 0);

            return resultTable;
        }
        #endregion

        #region GetEmptyUseSSLayer : 툴의 SS 사용층 코드를 반환
        private DataTable GetEmptyUseSSLayer()
        {

            DataTable resultTable = new DataTable();

            resultTable.Columns.Add("CODEID");
            resultTable.Columns.Add("CODENAME");

            DataRow emptyRow = resultTable.NewRow();
            emptyRow["CODEID"] = "";
            emptyRow["CODENAME"] = "";

            resultTable.Rows.Add(emptyRow);

            return resultTable;
        }
        #endregion

        #region GetSingleMakeVendor : 사용자가 입력한 제작업체명으로 검색 (클립보드의 데이터 이용) - 그리드내 복사 기능 구현
        /// <summary>
        /// 사용자가 입력한 제작업체명으로 검색
        /// </summary>
        private void GetSingleMakeVendor(string vendorName, int rowHandle)
        {
            if (_clipDatas == null)
            {
                _clipBoardData = Clipboard.GetDataObject();
                if (_clipBoardData != null)
                {
                    string[] clipFormats = _clipBoardData.GetFormats(true);


                    foreach (string format in clipFormats)
                    {
                        if (format.Equals("System.String"))
                        {
                            string tempStr = Convert.ToString(_clipBoardData.GetData(format));
                            tempStr = tempStr.Replace("\r\n", "\n");
                            if (tempStr.Substring(tempStr.Length - 1).Equals("\n"))
                                tempStr = tempStr.Substring(0, tempStr.Length - 1);
                            _clipDatas = tempStr.Split('\n');
                        }
                    }
                }
            }

            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            values.Add("VENDORNAME", vendorName);
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable savedResult = SqlExecuter.Query("GetVendorListByTool", "10001", values);

            if (savedResult.Rows.Count.Equals(1))
            {
                grdActionFilm.View.SetRowCellValue(rowHandle, "VENDORID", savedResult.Rows[0].GetString("VENDORID"));
                grdActionFilm.View.SetRowCellValue(rowHandle, "MAKEVENDOR", savedResult.Rows[0].GetString("VENDORNAME"));
            }
            else
            {
                grdActionFilm.View.SetRowCellValue(rowHandle, "MAKEVENDOR", "");
                grdActionFilm.View.SetRowCellValue(rowHandle, "VENDORID", "");

                _isGoodCopy = false; //무조건 메세지 출력
            }

            if (_clipDatas != null)
            {
                if (_clipIndex.Equals(_clipDatas.Length)) //마지막 행을 수행하고 난 이후 메세지 출력
                {
                    if (!_isGoodCopy)
                    {
                        ShowMessage(MessageBoxButtons.OK, "TOOLMAKEVENDORSELECT", "");
                    }

                    //초기화
                    _clipIndex = 1;
                    _clipDatas = null;
                    _isGoodCopy = true;
                }
                else
                {
                    _clipIndex++;
                }
            }
        }
        #endregion

        #region GetSingleReceiptArea : 사용자가 입력한 입고작업장으로 검색 (클립보드의 데이터를 이용) - 그리드내 복사 기능 구현
        private void GetSingleReceiptArea(string areaName, int rowHandle)
        {
            if (_clipDatas == null)
            {
                _clipBoardData = Clipboard.GetDataObject();
                if (_clipBoardData != null)
                {
                    string[] clipFormats = _clipBoardData.GetFormats(true);


                    foreach (string format in clipFormats)
                    {
                        if (format.Equals("System.String"))
                        {
                            string tempStr = Convert.ToString(_clipBoardData.GetData(format));
                            tempStr = tempStr.Replace("\r\n", "\n");
                            if (tempStr.Substring(tempStr.Length - 1).Equals("\n"))
                                tempStr = tempStr.Substring(0, tempStr.Length - 1);
                            _clipDatas = tempStr.Split('\n');
                        }
                    }
                }
            }

            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            values.Add("AREANAME", areaName);
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable savedResult = SqlExecuter.Query("GetAreaListByTool", "10001", values);

            if (savedResult.Rows.Count.Equals(1))
            {
                grdActionFilm.View.SetRowCellValue(rowHandle, "AREAID", savedResult.Rows[0].GetString("AREAID"));
                grdActionFilm.View.SetRowCellValue(rowHandle, "RECEIPTAREA", savedResult.Rows[0].GetString("AREANAME"));
            }
            else
            {
                grdActionFilm.View.SetRowCellValue(rowHandle, "AREAID", "");
                grdActionFilm.View.SetRowCellValue(rowHandle, "RECEIPTAREA", "");

                _isGoodCopy = false; //무조건 메세지 출력
            }

            if (_clipDatas != null)
            {
                if (_clipIndex.Equals(_clipDatas.Length)) //마지막 행을 수행하고 난 이후 메세지 출력
                {
                    if (!_isGoodCopy)
                    {
                        ShowMessage(MessageBoxButtons.OK, "TOOLMAKEVENDORSELECT", "");
                    }

                    //초기화
                    _clipIndex = 1;
                    _clipDatas = null;
                    _isGoodCopy = true;
                }
                else
                {
                    _clipIndex++;
                }
            }
        }
        #endregion

        #region GetIsUniqData
        public bool GetIsUniqData(DataTable ruleTable, DataRow newRow)
        {
            try
            {
                foreach (DataRow subRow in ruleTable.Rows)
                {
                    /*
                    if (newRow.GetString("FILMVERSION").Equals(subRow.GetString("FILMVERSION"))
                        && newRow.GetString("TOOLTYPEID").Equals(subRow.GetString("TOOLTYPEID"))
                        && newRow.GetString("TOOLDETAILTYPEID").Equals(subRow.GetString("TOOLDETAILTYPEID"))
                        && newRow.GetString("REQUESTCOMMENT").Equals(subRow.GetString("REQUESTCOMMENT"))
                        )
                        return false;
                        */
                }
                return true;
            }
            catch (NullReferenceException err)
            {
                return false;
            }
        }
        #endregion

        #region SetDefaultArea : 툴 입력시 고정작업장을 등록
        private void SetDefaultArea(DataRow newRow)
        {
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("CODECLASSID", "FilmDefaultAreaId");
            values.Add("CODEID", Conditions.GetValue("P_PLANTID"));

            DataTable resultTable = SqlExecuter.Query("GetCodeListByFilm", "10001", values);

            if (resultTable.Rows.Count > 0)
            {
                string defaultID = resultTable.Rows[0].GetString("CODEID");

                Dictionary<string, object> subValues = new Dictionary<string, object>();
                subValues.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                subValues.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                subValues.Add("AREAID", defaultID.Replace(Conditions.GetValue("P_PLANTID").ToString() + "-", ""));//Site와 - 를 제거하여 반환
                DataTable savedResult = SqlExecuter.Query("GetAreaListByTool", "10001", subValues);

                if (savedResult.Rows.Count > 0)
                {
                    newRow["AREAID"] = savedResult.Rows[0].GetString("AREAID");
                    newRow["RECEIPTAREA"] = savedResult.Rows[0].GetString("AREANAME");
                }
            }
        }
        #endregion

        #region GetNewVersion - 필름의 버전을 새롭게 생성
        private string GetNewVersion(string productDefID, string productDefVersion)
        {
            string filmVersion = productDefID.Substring(productDefID.Length - 2) + productDefVersion;

            return filmVersion;
        }
        #endregion
        #endregion
    }
}
