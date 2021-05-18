#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
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

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프 로 그 램 명  : 기준정보 > Setup > 작업장 사용자관리
    /// 업  무  설  명  : 사람마다 사용하는 작업장을 등록, 조회한다.
    /// 생    성    자  : 한주석
    /// 생    성    일  : 2019-12-11
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class UserAreaStatus : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가
        string _Text;                                       // 설명
        SmartBandedGrid grdList = new SmartBandedGrid();    // Main Grid
        private string _focusUserId = "";
        #endregion

        #region 생성자

        public UserAreaStatus()
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
            InitializeUserGrid();
            InitializeAreaGrid();
        }

        /// <summary>        
        /// 코드그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeUserGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdUserlist.GridButtonItem -= GridButtonItem.CRUD;

            grdUserlist.View.AddTextBoxColumn("USERID", 100)
                .SetIsReadOnly();
            grdUserlist.View.AddTextBoxColumn("USERNAME", 100)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdUserlist.View.AddTextBoxColumn("DEPARTMENT", 150)
                .SetIsReadOnly();
            grdUserlist.View.AddComboBoxColumn("CONTROLRIGHT", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=AreaAuthority", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdUserlist.View.AddTextBoxColumn("CREATOR", 80)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();
            grdUserlist.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly()
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss");
            grdUserlist.View.AddTextBoxColumn("MODIFIER", 80)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();
            grdUserlist.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly()
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss");

            grdUserlist.View.PopulateColumns();
        }


        private void InitializeAreaGrid()
        {
            // TODO : 그리드 초기화 로직 추가


            grdAreaList.View.AddTextBoxColumn("USERID", 150)
                .SetIsHidden();
            InitializeGrid_AreaListPopup();
            grdAreaList.View.AddTextBoxColumn("AREANAME", 200)
                .SetIsReadOnly();
            //자사구분
            grdAreaList.View.AddTextBoxColumn("OWNTYPE", 80)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();
            //창고
            grdAreaList.View.AddTextBoxColumn("WAREHOUSEID", 90)
                .SetIsHidden()
                .SetIsReadOnly();
            grdAreaList.View.AddTextBoxColumn("WAREHOUSENAME", 110)
                .SetIsReadOnly();
            //거래처
            grdAreaList.View.AddTextBoxColumn("VENDORID", 90)
                .SetIsHidden()
                .SetIsReadOnly();
            grdAreaList.View.AddTextBoxColumn("VENDORNAME", 110)
                .SetIsReadOnly();

            grdAreaList.View.AddTextBoxColumn("CREATOR", 80)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();
            grdAreaList.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly()
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss");

            grdAreaList.View.PopulateColumns();
        }

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            grdUserlist.View.FocusedRowChanged += View_FocusedRowChanged;
        }

        private void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            User_View_FocusedRowChanged();

          

        }




        /// <summary>
		/// 창고 컬럼 팝업
		/// </summary>
		private void InitializeGrid_AreaListPopup()
        {
   
            //string plantId = values["P_PLANTID"].ToString();

            var areaPopupColumn = grdAreaList.View.AddSelectPopupColumn("AREAID", new SqlQuery("GetAreaList", "10005", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetPopupLayout("SELECTWAREHOUSEID", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupResultCount(0)
                .SetPopupLayoutForm(800, 800, FormBorderStyle.FixedToolWindow)
                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {
          
                    DataTable dt2 = grdAreaList.DataSource as DataTable;
                    int handle = grdAreaList.View.FocusedRowHandle;
                    DataRow dr = grdUserlist.View.GetFocusedDataRow();


                    dt2.Rows.RemoveAt(handle);
                    // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
                    // dataGridRow : 현재 Focus가 있는 그리드의 DataRow
                    foreach (DataRow row in selectedRows)
                    {

                         DataRow newrow = dt2.NewRow();
                        newrow["AREAID"] = row["AREAID"];
                        newrow["AREANAME"] = row["AREANAME"];
                        newrow["OWNTYPE"] = row["OWNTYPE"];
                        newrow["VENDORNAME"] = row["VENDORNAME"];
                        newrow["WAREHOUSENAME"] = row["WAREHOUSENAME"];
                        dt2.Rows.Add(newrow);

                    }
                    
                   
                    
                });



            areaPopupColumn.Conditions.AddTextBox("TXTAREA");
            areaPopupColumn.GridColumns.AddTextBoxColumn("AREAID", 120);
            areaPopupColumn.GridColumns.AddTextBoxColumn("AREANAME", 200);
            areaPopupColumn.GridColumns.AddTextBoxColumn("OWNTYPE", 80)
                .SetTextAlignment(TextAlignment.Center);
            areaPopupColumn.GridColumns.AddTextBoxColumn("WAREHOUSEID", 90);
            areaPopupColumn.GridColumns.AddTextBoxColumn("WAREHOUSENAME", 110);
            areaPopupColumn.GridColumns.AddTextBoxColumn("VENDORID", 90);
            areaPopupColumn.GridColumns.AddTextBoxColumn("VENDORNAME", 110);

        }

        #endregion

        private void InitializeGrid_UserListPopup()
        {
            //추후 변경 예정
            //var parentCodeClassPopupColumn = this.grdAreaPerson.View.AddSelectPopupColumn("USERID", new SqlQuery("GetUserAreaPerson", "10001", $"PLANTID={UserInfo.Current.Plant}"))

            var parentCodeClassPopupColumn = Conditions.AddSelectPopup("USERID", new SqlQuery("GetUserAreaPerson", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "USERID", "USERID")
               .SetPopupLayout("USERNAME", PopupButtonStyles.Ok_Cancel, true, false)
               .SetPopupResultCount(0)
               .SetPopupLayoutForm(800, 800, FormBorderStyle.FixedToolWindow)
               .SetPosition(0)
               .SetPopupAutoFillColumns("USERNAME")
               .SetPopupApplySelection((selectRow, gridRow) => {

                    List<string> usernameList = new List<string>();
                    List<string> useridList = new List<string>();

                    selectRow.AsEnumerable().ForEach(r => {
                        usernameList.Add(Format.GetString(r["USERNAME"]));
                    });

                   Conditions.GetControl<SmartTextBox>("P_USERNAME").EditValue = string.Join(",", usernameList);
               });

            // 팝업에서 사용할 조회조건 항목 추가
            parentCodeClassPopupColumn.Conditions.AddTextBox("USERID");
            parentCodeClassPopupColumn.Conditions.AddTextBox("USERNAME");

            // 팝업 그리드 설정
            parentCodeClassPopupColumn.GridColumns.AddTextBoxColumn("USERID", 150);
            parentCodeClassPopupColumn.GridColumns.AddTextBoxColumn("USERNAME", 200);
        }


        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            // TODO : 저장 Rule 변경
            DataTable UserChanged = grdUserlist.GetChangedRows();
            DataTable Areachanged = grdAreaList.GetChangedRows();
            DataRow user = grdUserlist.View.GetFocusedDataRow();
            Areachanged.Columns.Add("ENTERPRISEDID");
            Areachanged.Columns.Add("VALIDSTATE");

            foreach (DataRow dr in Areachanged.Rows)
            {
                dr["ENTERPRISEDID"] = UserInfo.Current.Enterprise;
                dr["PLANTID"] = UserInfo.Current.Plant;
                dr["VALIDSTATE"] = "Valid";
                dr["USERID"] = user["USERID"];
            }



            string usertable = "user";
            string areatable = "area";
            UserChanged.TableName = usertable;
            Areachanged.TableName = areatable;

     

            MessageWorker messageWorker = new MessageWorker("SaveUserArea");
            messageWorker.SetBody(new MessageBody()
            {
                 { usertable, UserChanged }
                , { areatable, Areachanged}
            });
            messageWorker.Execute();


        }
    

        #endregion

        #region 검색

        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            var values = Conditions.GetValues();

            values.Add("PLANTID", UserInfo.Current.Plant);
            DataTable dtuser = SqlExecuter.Query("SelectUserInfo", "10001", values);

            if (dtuser.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdUserlist.DataSource = dtuser;

            if(dtuser.Rows.Count<1)
            {
                grdAreaList.DataSource = null;

            }
            else
            {
                if (!string.IsNullOrEmpty(this._focusUserId))
                { 
                    grdUserlist.View.FocusedRowHandle = grdUserlist.View.GetRowHandleByValue("USERID", _focusUserId);
                    User_View_FocusedRowChanged();
                }
                else
                {
                    grdUserlist.View.FocusedRowHandle = 0;
                    User_View_FocusedRowChanged();
                }
            }
            
            
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();
            InitializeGrid_UserListPopup();
            CommonFunction.AddConditionAreaPopup("P_AREAID", 4, false, Conditions, false);
            // TODO : 조회조건 추가 구성이 필요한 경우 사용
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();
            Conditions.GetControl<SmartTextBox>("P_USERNAME").ReadOnly = true;
            Conditions.GetControl<SmartSelectPopupEdit>("USERID").EditValueChanged += USERIDChanged;
            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
        }

        private void USERIDChanged(object sender, EventArgs e)
        {
            SmartSelectPopupEdit PopProdutid = sender as SmartSelectPopupEdit;

            if (Format.GetFullTrimString(PopProdutid.EditValue).Equals(string.Empty))
            {
                Conditions.GetControl<SmartTextBox>("P_USERNAME").EditValue = string.Empty;
            }
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
            grdAreaList.View.CheckValidation();
            grdUserlist.View.CheckValidation();
            DataTable changed = grdAreaList.GetChangedRows();
            DataTable userchanged = grdUserlist.GetChangedRows();
            if (changed.Rows.Count == 0 && userchanged.Rows.Count==0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }

        #endregion

        #region Private Function


        private void User_View_FocusedRowChanged()
        {


            DataRow dr = grdUserlist.View.GetFocusedDataRow();
            if (dr == null)
            {
                return;
            }
            if(dr["CONTROLRIGHT"].ToString().Equals("ProductionManager"))
            {
                grdAreaList.GridButtonItem = GridButtonItem.All;
                grdAreaList.GridButtonItem -= GridButtonItem.Add;
                grdAreaList.GridButtonItem -= GridButtonItem.Delete;
       

            }
            else
            {
                grdAreaList.GridButtonItem = GridButtonItem.All;

            }
            _focusUserId = Format.GetString(dr["USERID"].ToString());

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("p_userid", dr["USERID"].ToString());
            param.Add("p_validstate", "Valid");
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("PLANTID", UserInfo.Current.Plant);


            DataTable dtarealist = SqlExecuter.Query("GetUserUseArea", "10001", param);//Procedure("usp_com_selectuomdefinition", param);


            if (dtarealist == null)
            {
                return;
            }
            grdAreaList.DataSource = dtarealist;
        }

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
