#region using
using Micube.Framework.Net;
using Micube.Framework;
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

using DevExpress.XtraCharts;
using DevExpress.Utils;
using Micube.Framework.SmartControls.Validations;
#endregion

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프 로 그 램 명  : 기준정보 > Setup > 작업자 관리
    /// 업  무  설  명  : 작업자 관리
    /// 생    성    자  : 한주석
    /// 생    성    일  : 2019-12-11
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class AreaPerson : SmartConditionManualBaseForm
    {
        #region ◆ 생성자 |
        public AreaPerson()
        {
            InitializeComponent();

            InitializeEvent();

        }
        #endregion

        #region ◆ 컨텐츠 영역 초기화 |
        /// <summary>
        /// Control 초기화
        /// </summary>
        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeTreeArea();
            InitializeGridIdDefinitionManagement();
        }

        #region ▶ 조회조건의 컨트롤을 추가 |
        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            SmartComboBox combo = Conditions.GetControl<SmartComboBox>("P_PLANTID");
            combo.EditValue = UserInfo.Current.Plant;
            combo.ReadOnly = true;

        }
        #endregion

        #region ▶ Grid 초기화 |
        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGridIdDefinitionManagement()
        {
            //속성
            InitializeGrid_UserListPopup();

            grdAreaPerson.GridButtonItem = GridButtonItem.All;   // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기
            grdAreaPerson.View.AddTextBoxColumn("ENTERPRISEID").SetIsHidden();

            grdAreaPerson.View.AddTextBoxColumn("WORKERNAME")
                .SetValidationKeyColumn();
            grdAreaPerson.View.AddComboBoxColumn("OWNTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OwnType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdAreaPerson.View.AddComboBoxColumn("WORKERTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=WORKERTYPE", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetValidationIsRequired();
            grdAreaPerson.View.AddComboBoxColumn("ISMAINAREA", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetValidationIsRequired();
            grdAreaPerson.View.AddComboBoxColumn("PLANTID", 70, new SqlQuery("GetPlantList", "00001", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}"), "PLANTNAME", "PLANTID")
                .SetValidationKeyColumn();
            grdAreaPerson.View.AddComboBoxColumn("AREAID", 200, new SqlQuery("GetAreaList", "10003", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "AREANAME", "AREAID").SetLabel("AREANAME2")
                .SetValidationKeyColumn();

            grdAreaPerson.View.AddTextBoxColumn("WORKERNO", 100);
            grdAreaPerson.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            grdAreaPerson.View.PopulateColumns();
        }

        private void InitializeGrid_UserListPopup()
        {
            //추후 변경 예정
            //var parentCodeClassPopupColumn = this.grdAreaPerson.View.AddSelectPopupColumn("USERID", new SqlQuery("GetUserAreaPerson", "10001", $"PLANTID={UserInfo.Current.Plant}"))

            var parentCodeClassPopupColumn = this.grdAreaPerson.View.AddSelectPopupColumn("USERID", new SqlQuery("GetUserAreaPerson", "10001", $"PLANTID={UserInfo.Current.Plant}"))
                // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
                .SetPopupLayout("USERID", PopupButtonStyles.Ok_Cancel, true, false)
                // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
                .SetPopupResultCount(0)
                .SetValidationKeyColumn()
                // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
                //.SetPopupResultMapping("CODECLASSID", "CODECLASSID")
                // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
                .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow)
                // 그리드의 남은 영역을 채울 컬럼 설정
                //.SetPopupAutoFillColumns("CODECLASSNAME")
                // Validation 이 필요한 경우 호출할 Method 지정
                .SetPopupValidationCustom(ValidationCodeClassIdPopup)
                                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                                {

                                    DataRow focusrow = treeParentArea.GetFocusedDataRow();

                                    DataTable dt2 = grdAreaPerson.DataSource as DataTable;
                                    int handle = grdAreaPerson.View.FocusedRowHandle;
                                    dt2.Rows.RemoveAt(handle);

                                    // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
                                    // dataGridRow : 현재 Focus가 있는 그리드의 DataRow
                                    foreach (DataRow row in selectedRows)
                                    {
                                        DataRow newrow = dt2.NewRow();

                                        newrow["USERID"] = row["USERID"];
                                        newrow["WORKERNAME"] = row["USERNAME"];
                                        newrow["AREAID"] = focusrow["AREAID"];
                                        newrow["OWNTYPE"] = focusrow["OWNTYPE"];
                                        newrow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                                        newrow["PLANTID"] = UserInfo.Current.Plant;
                                        newrow["VALIDSTATE"] = "Valid";


                                        dt2.Rows.Add(newrow);
                                    }
                                });

            // 팝업에서 사용할 조회조건 항목 추가
            parentCodeClassPopupColumn.Conditions.AddTextBox("USERID");
            parentCodeClassPopupColumn.Conditions.AddTextBox("USERNAME");


            // 팝업 그리드 설정
            parentCodeClassPopupColumn.GridColumns.AddTextBoxColumn("USERID", 150);
            parentCodeClassPopupColumn.GridColumns.AddTextBoxColumn("USERNAME", 200);
            parentCodeClassPopupColumn.GridColumns.AddTextBoxColumn("DEPARTMENT", 150);
        }

        private ValidationResultCommon ValidationCodeClassIdPopup(DataRow currentRow, IEnumerable<DataRow> popupSelections)
        {

            ValidationResultCommon result = new ValidationResultCommon();

            object obj = grdAreaPerson.DataSource;
            DataTable dt = (DataTable)obj;

            foreach (DataRow row in popupSelections)
            {
                if (dt.Select("USERID = '" + row["USERID"].ToString() + "'").Length != 0)
                {
                    Language.LanguageMessageItem item = Language.GetMessage("SelectOverlap", row["USERID"].ToString());
                    result.IsSucced = false;
                    result.FailMessage = item.Message;
                    result.Caption = item.Title;
                }
                else
                {
                    currentRow["WORKERNAME"] = row["USERNAME"];
                    currentRow["DEPARTMENT"] = row["DEPARTMENT"];
                }

            }
            return result;
        }
        #endregion

        #region ▶ TreeList 초기화 |

        /// <summary>
        /// Area Tree 초기화
        /// </summary>
        private void InitializeTreeArea()
        {
            treeParentArea.SetResultCount(1);
            treeParentArea.SetIsReadOnly();
            treeParentArea.SetEmptyRoot("Root", "*");
            treeParentArea.SetMember("AREANAME", "AREAID", "PARENTAREAID");

            string areaId = "";
            if (treeParentArea.FocusedNode != null)
            {
                areaId = treeParentArea.GetRowCellValue(treeParentArea.FocusedNode, treeParentArea.Columns["AREAID_COPY"]).ToString();
            }

            var values = Conditions.GetValues();

            string plantId = UserInfo.Current.Plant;
            if (!values["P_PLANTID"].Equals("*") && !values["P_PLANTID"].Equals(UserInfo.Current.Plant))
            {
                plantId = values["P_PLANTID"].ToString();
            }

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("plantId", plantId);
            param.Add("languageType", UserInfo.Current.LanguageType);
            param.Add("AREATYPE", "Area");
            param.Add("OWNTYPE", "'OurCompany','MajorSuppliers'");

            treeParentArea.DataSource = SqlExecuter.Query("SelectAreaTreeList", "10002", param);//Procedure("usp_com_selectarea_tree", param);

            treeParentArea.PopulateColumns();

            treeParentArea.ExpandAll();

            treeParentArea.SetFocusedNode(treeParentArea.FindNodeByFieldValue("AREAID_COPY", areaId));
        }

        #endregion

        #endregion

        #region ◆ Event |
        /// <summary>
        /// 이벤트 설정
        /// </summary>
        private void InitializeEvent()
        {
            // TreeList
            treeParentArea.FocusedNodeChanged += TreeParentArea_FocusedNodeChanged;

            // Grid
            grdAreaPerson.View.AddingNewRow += grdAreaPerson_AddingNewRow;
        }

        #region ▶ Grid Event |
        /// <summary>
        /// Grid AddNewRow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void grdAreaPerson_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            DataRow focusrow = treeParentArea.GetFocusedDataRow();

            if (focusrow["AREATYPE"].ToString() != "Area")
            {
                sender.CancelAddNew();
            }

            args.NewRow["AREAID"] = focusrow["AREAID"];
            args.NewRow["OWNTYPE"] = focusrow["OWNTYPE"];
            args.NewRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
            args.NewRow["PLANTID"] = UserInfo.Current.Plant;
            args.NewRow["VALIDSTATE"] = "Valid";
        }
        #endregion

        #region ▶ TreeList Event |
        /// <summary>
        /// TreeList Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeParentArea_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (e.Node == null)
            {
                return;
            }
            DataRow focusrow = treeParentArea.GetFocusedDataRow();
            var values = Conditions.GetValues();

            Dictionary<string, object> param = new Dictionary<string, object>();

            param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            param.Add("PLANTID", UserInfo.Current.Plant);
            param.Add("AREAID", focusrow["AREAID"].ToString());
            param.Add("VALIDSTATE", values["P_VALIDSTATE"].ToString());

            grdAreaPerson.DataSource = SqlExecuter.Query("GetAreaworkerList", "10001", param);
        }
        #endregion

        #endregion

        #region ◆ 툴바 |
        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            DataTable changed = new DataTable();
            changed = grdAreaPerson.GetChangedRows();
            changed.TableName = "areaWorker";


            DataRow focusrow = treeParentArea.GetFocusedDataRow();
            var values = Conditions.GetValues();

            Dictionary<string, object> param = new Dictionary<string, object>();

            param.Add("AREAID", focusrow["AREAID"].ToString());
            param.Add("VALIDSTATE", values["P_VALIDSTATE"].ToString());

            DataTable dt = SqlExecuter.Query("GetAreaworkerList", "10002", param);




            foreach (DataRow dr in changed.Rows)
            {
                if (dr["_STATE_"].Equals("added"))
                {
                    foreach (DataRow dr2 in dt.Rows)
                    {
                        if (dr["USERID"].Equals(dr2["USERID"]) && dr["WORKERNAME"].Equals(dr2["WORKERNAME"]) && dr["PLANTID"].Equals(dr2["PLANTID"])
                             && dr["ENTERPRISEID"].Equals(dr2["ENTERPRISEID"]) && dr["AREAID"].Equals(dr2["AREAID"]))
                        {
                            throw MessageException.Create("SAMEUSEREXIST");
                        }
                    }
                }
            }


            
            DataSet dsChang = new DataSet();
            dsChang.Tables.Add(changed);

            ExecuteRule("AreaWorker", dsChang);
        }
        #endregion

        #region ◆ 검색 |
        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            //tree 조회
            InitializeTreeArea();
        }
        #endregion

        #region ◆ 유효성 검사 |
        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            this.grdAreaPerson.View.CheckValidation();
        }
        #endregion

        #region ◆ Private Function |


        #endregion
    }
}
