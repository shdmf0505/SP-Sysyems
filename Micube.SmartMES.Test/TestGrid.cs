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
using DevExpress.XtraGrid.Views.Base;

#endregion

namespace Micube.SmartMES.Test
{
    /// <summary>
    /// 프 로 그 램 명  : 교육 > TEST > 테스트그리드 
    /// 업  무  설  명  : 사용자마다 사용하는 작업장을 등록, 조회한다.
    /// 생    성    자  : 정수현
    /// 생    성    일  : 2021-04-07
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class TestGrid : SmartConditionManualBaseForm
    {
        #region Local Variables

        string _Text;                                       
        SmartBandedGrid grdList = new SmartBandedGrid();    
        private string focusUserId = "";
        #endregion

        #region 생성자

        public TestGrid()
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

            InitializeUserGrid();
            InitializeAreaGrid();
        }

        /// <summary>        
        /// 코드그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeUserGrid() //grdUserlist
        {

            grdUserlist.GridButtonItem -= GridButtonItem.CRUD;

            //사용자iD
            grdUserlist.View.AddTextBoxColumn("USERID", 90)
                .SetIsReadOnly();
            //사용자명
            grdUserlist.View.AddTextBoxColumn("USERNAME", 100)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();
            //부서
            grdUserlist.View.AddTextBoxColumn("DEPARTMENT", 140)
                .SetIsReadOnly();
            //통제권한
            grdUserlist.View.AddComboBoxColumn("CONTROLRIGHT", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=AreaAuthority", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            //생성자
            grdUserlist.View.AddTextBoxColumn("CREATOR", 80)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();
            //생성일
            grdUserlist.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly()
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss"); //포맷맞추기
            //수정자
            grdUserlist.View.AddTextBoxColumn("MODIFIER", 80)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();
            //수정일자
            grdUserlist.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly()
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss"); //포맷맞추기

            grdUserlist.View.PopulateColumns();
        }

        /// <summary>        
        /// 작업장 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeAreaGrid() //grdAreaList
        {

            //사용자ID
            grdAreaList.View.AddTextBoxColumn("USERID", 150)
                .SetIsHidden();
            //작업장이름
            InitializeGrid_AreaListPopup();
            grdAreaList.View.AddTextBoxColumn("AREANAME", 200)
                .SetTextAlignment(TextAlignment.Center)
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
            //생성자
            grdAreaList.View.AddTextBoxColumn("CREATOR", 80)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();
            //생성일자
            grdAreaList.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly()
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss"); //포맷

            grdAreaList.View.PopulateColumns();
        }


      

        #endregion

        #region

        private void InitializeEvent()
        {
            grdUserlist.View.FocusedRowChanged += View_FocusedRowChanged;
        }

        private void View_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            UserList_View_FocusedRowChanged();

        }

        private void UserList_View_FocusedRowChanged()
        {

            DataRow dr = grdUserlist.View.GetFocusedDataRow();

            if (dr == null)
            {
                return;
            }

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("USERID", dr["USERID"].ToString());
            param.Add("validstate", "Valid");
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("plantid", UserInfo.Current.Plant);

            DataTable dt = SqlExecuter.Query("GetUserArea_Test", "00001", param);

            if (dt == null)
            {
                return;
            }

            grdAreaList.DataSource = dt;
        }
        #endregion


        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            DataTable userdt = grdUserlist.GetChangedRows();
            DataTable areadt = grdAreaList.GetChangedRows();
            DataRow user = grdUserlist.View.GetFocusedDataRow(); //USERID 가져가야됨

            areadt.Columns.Add("ENTERPRISEDID"); //회사
            areadt.Columns.Add("VALIDSTATE"); //유효

            foreach (DataRow dr in areadt.Rows)
            {

                dr["ENTERPRISEDID"] = UserInfo.Current.Enterprise;
                dr["PLANTID"] = UserInfo.Current.Plant;
                dr["VALIDSTATE"] = "Valid";
                dr["USERID"] = user["USERID"];//유저그리드에서 가져감
            }

            string usertable = "userList";
            string areatable = "areaList";
            userdt.TableName = usertable;
            areadt.TableName = areatable;

            MessageWorker worker = new MessageWorker("TestGrid");
            worker.SetBody(new MessageBody()
            {
                { usertable, userdt},
                { areatable, areadt}

            });

            worker.Execute();


        }


        #endregion

        #region 검색조건 추가
        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        /// 
        protected override void InitializeCondition()
        {
            base.InitializeCondition();
            InitializeGrid_UserListPopup();
            CommonFunction.AddConditionAreaPopup("P_AREAID", 4, false, Conditions, false);
        }

        /// <summary>
        /// 작업장 팝업
        /// </summary>
        private void InitializeGrid_AreaListPopup()
        {

            var areaPopupColumn = grdAreaList.View.AddSelectPopupColumn("AREAID", new SqlQuery("GetAreaPopupList", "00001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
               .SetPopupLayout("작업장 팝업", PopupButtonStyles.Ok_Cancel, true, false)
               .SetPopupResultCount(0)
               .SetPopupLayoutForm(800, 800, FormBorderStyle.FixedToolWindow)
               .SetPopupApplySelection((selectedRows, dataGridRow) =>
               {

                   DataTable dt = grdAreaList.DataSource as DataTable;
                   int handle = grdAreaList.View.FocusedRowHandle;
                   DataRow dr = grdUserlist.View.GetFocusedDataRow();


                   dt.Rows.RemoveAt(handle);

                   foreach (DataRow row in selectedRows)
                   {

                       DataRow newrow = dt.NewRow();
                       newrow["AREAID"] = row["AREAID"];
                       newrow["AREANAME"] = row["AREANAME"];
                       newrow["OWNTYPE"] = row["OWNTYPE"];
                       newrow["VENDORNAME"] = row["VENDORNAME"];
                       newrow["WAREHOUSENAME"] = row["WAREHOUSENAME"];
                       dt.Rows.Add(newrow);

                   }



               });



            areaPopupColumn.Conditions.AddTextBox("TXTAREA");

            //팝업 컬럼
            areaPopupColumn.GridColumns.AddTextBoxColumn("AREAID", 120);
            areaPopupColumn.GridColumns.AddTextBoxColumn("AREANAME", 200);
            areaPopupColumn.GridColumns.AddTextBoxColumn("OWNTYPE", 80)
                            .SetTextAlignment(TextAlignment.Center);
            areaPopupColumn.GridColumns.AddTextBoxColumn("WAREHOUSEID", 90);
            areaPopupColumn.GridColumns.AddTextBoxColumn("WAREHOUSENAME", 110);
            areaPopupColumn.GridColumns.AddTextBoxColumn("VENDORID", 90);
            areaPopupColumn.GridColumns.AddTextBoxColumn("VENDORNAME", 110);


        }

        /// <summary>
        /// 사용자 팝업
        /// </summary>
        private void InitializeGrid_UserListPopup() 
        {

            var parentCodeClassPopupColumn = Conditions.AddSelectPopup("USERID", new SqlQuery("GetAreaUser", "00001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "USERID", "USERID")
               .SetPopupLayout("USERNAME", PopupButtonStyles.Ok_Cancel, true, false)
               .SetPopupResultCount(0)
               .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
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

            // 팝업 조회조건
            parentCodeClassPopupColumn.Conditions.AddTextBox("USERID");
            parentCodeClassPopupColumn.Conditions.AddTextBox("USERNAME");

            // 팝업 컬럼
            parentCodeClassPopupColumn.GridColumns.AddTextBoxColumn("USERID", 150);
            parentCodeClassPopupColumn.GridColumns.AddTextBoxColumn("USERNAME", 150);
        }

        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();
            Conditions.GetControl<SmartTextBox>("P_USERNAME").ReadOnly = true;
            Conditions.GetControl<SmartSelectPopupEdit>("USERID").EditValueChanged += USERIDChanged;

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



        #region 검색

        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            var values = Conditions.GetValues();

            
            values.Add("PLANTID", UserInfo.Current.Plant);
            DataTable dt = SqlExecuter.Query("SelectUserInfo_Test", "00001", values);

            if (dt.Rows.Count < 1) 
            {
                ShowMessage("NoSelectData"); 
            }

            grdUserlist.DataSource = dt;

            if (dt.Rows.Count < 1)
            {
                grdAreaList.DataSource = null;

            }
            else
            {
                if (!string.IsNullOrEmpty(this.focusUserId))
                {
                    grdUserlist.View.FocusedRowHandle = grdUserlist.View.GetRowHandleByValue("USERID", focusUserId);
                    UserList_View_FocusedRowChanged();
                }
                else
                {
                    grdUserlist.View.FocusedRowHandle = 0;
                    UserList_View_FocusedRowChanged();
                }
            }

        }
        #endregion

      
    }
}
