#region using

using DevExpress.XtraTreeList;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion

namespace Micube.SmartMES.SystemManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 시스템관리 > 코드 관리 > 요청정보
    /// 업  무  설  명  : 요청정보를 조회한다.
    /// 생    성    자  : 한주석
    /// 생    성    일  : 2019-10-18
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class RequestInfo : SmartConditionManualBaseForm
    {

        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가
        int adding_check = 0;                                 // 설명
        SmartBandedGrid grdList = new SmartBandedGrid();    // Main Grid

        #endregion

        #region 생성자

        public RequestInfo()
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
            InitializeGrid();
            InitializeMenu();
            GetCodeListByState();
            InitializeCommentGrid();
            GetCodeListByWorkType();
            GetCodeListByRequestType();
            fileSr.btnFileDownload.Enabled = false;
        }


        /// <summary>
        /// 트리 초기화
        /// </summary>
        private void InitializeMenu()
        {
            treeMenu.SetResultCount(1);
            treeMenu.SetIsReadOnly();
            treeMenu.SetEmptyRoot("Root", "*");
            treeMenu.SetMember("MENUNAME", "MENUID", "PARENTMENUID");
            treeMenu.SetSortColumn("DISPLAYSEQUENCE", SortOrder.Ascending);

            string strMenuId = "";
            if (treeMenu.FocusedNode != null)
                strMenuId = treeMenu.GetRowCellValue(treeMenu.FocusedNode, treeMenu.Columns["MENUID_COPY"]).ToString();

            //int itemIndex = treeMenu.GetVisibleIndexByNode(treeMenu.FocusedNode);

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("p_menuId", "*"); // focus root value
            param.Add("p_languageType", UserInfo.Current.LanguageType);
            //treeMenu.DataSource = Procedure("usp_com_selectMenu_tree", param);
            treeMenu.DataSource = SqlExecuter.Query("SelectMenuTree", "10001", param);

            treeMenu.PopulateColumns();

            treeMenu.ExpandToLevel(1);
            //treeMenu.FocusedNode = treeMenu.GetNodeByVisibleIndex(itemIndex);
            treeMenu.SetFocusedNode(treeMenu.FindNodeByFieldValue("MENUID_COPY", strMenuId));
        }



        /// <summary>        
        /// 요청 정보 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            grdrequest.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdrequest.GridButtonItem -= GridButtonItem.Add;
            //SRNO
            grdrequest.View.AddTextBoxColumn("SRNO", 80)
               .SetIsReadOnly();
            //SR작업메뉴 ID
            grdrequest.View.AddTextBoxColumn("SRWORKTYPEID", 120)
               .SetIsHidden();
            //SR작업메뉴 명
            grdrequest.View.AddTextBoxColumn("SRWORKTYPENAME", 120)
               .SetTextAlignment(TextAlignment.Center)
               .SetIsReadOnly();
            //메뉴ID
            grdrequest.View.AddTextBoxColumn("MENUID", 150)
                .SetIsReadOnly();
            //메뉴명
            grdrequest.View.AddTextBoxColumn("MENUNAME", 150)
                .SetIsReadOnly();
            //제목
            grdrequest.View.AddTextBoxColumn("TITLE", 200)
                .SetIsReadOnly();
            //SR 유형 ID
            grdrequest.View.AddTextBoxColumn("SRREQUESTTYPEID", 120)
               .SetIsHidden();
            //SR 유형 명
            grdrequest.View.AddTextBoxColumn("SRREQUESTTYPENAME", 120)
               .SetTextAlignment(TextAlignment.Center)
               .SetIsReadOnly();
            //요청자
            grdrequest.View.AddTextBoxColumn("CREATOR", 100)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("REQUESTUSER")
                .SetIsReadOnly();
            //요청일
            grdrequest.View.AddTextBoxColumn("CREATEDTIME", 250)
                .SetLabel("REQDATE")
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();
            //상태 ID
            grdrequest.View.AddTextBoxColumn("SRSTATEID", 200)
                .SetIsHidden();
            //상태 명
            grdrequest.View.AddTextBoxColumn("SRSTATENAME", 100)
                .SetLabel("DURABLESTATE")
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();
            //처리자
            grdrequest.View.AddTextBoxColumn("WORKER", 100)
                .SetLabel("ACTOR")
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();
            //완료일자
            grdrequest.View.AddTextBoxColumn("ENDDATE", 250)
                .SetLabel("COMPLETIONDATE")
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();
            //내용
            grdrequest.View.AddTextBoxColumn("CONTENTS", 390)
                .SetLabel("COMMENTS")
                .SetIsHidden()
                .SetIsReadOnly();

            grdrequest.View.PopulateColumns();
        }


        /// <summary>        
        /// 요청 정보 내용 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeCommentGrid()
        {
            grdcomment.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;
            grdcomment.GridButtonItem -= GridButtonItem.Add;
            //SRNO
            grdcomment.View.AddTextBoxColumn("SRNO", 100)
                .SetIsHidden();
            //순서
            grdcomment.View.AddTextBoxColumn("SEQUENCE", 100)
                .SetIsHidden();
            //등록자
            grdcomment.View.AddTextBoxColumn("CREATOR", 150)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();
            //등록일
            grdcomment.View.AddTextBoxColumn("CREATEDTIME", 250)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();
            //내용
            grdcomment.View.AddTextBoxColumn("COMMENTS", 1300)
                .SetIsReadOnly();
   
            grdcomment.View.PopulateColumns();
        }

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            treeMenu.FocusedNodeChanged += treeMenu_FocusedNodeChanged;
            grdrequest.View.FocusedRowChanged += View_RowChanged;
            btn_commentsave.Click += BtnSave_Click;
            btn_add.Click += Btn_ClickByAdd;
            this.Shown += RequestInfo_Shown;
        }

        private void RequestInfo_Shown(object sender, EventArgs e)
        {
            RequestRowChanged();

        }

        /// <summary>
        ///  버튼 클릭시 메시지 창 초기화 및 저장 버튼 시 신규로 되게 설정
        /// </summary>

        private void Btn_ClickByAdd(object sender, EventArgs e)
        {
            DataRow treeRow = treeMenu.GetFocusedDataRow();

            if (treeRow["MENUID"].Equals("*") || treeRow["MENUTYPE"].ToString().ToUpper().Equals("FOLDER"))
            {
                ShowMessage("MENUCLICK");
                return;
            }
            adding_check = 1;
            fileSr.ClearData();
            ucMessageInfo.Clear();
            grdcomment.DataSource = null;
        }

        /// <summary>
        /// Code List에서 상태 Combo code를 가져온다
        /// </summary>
        private void GetCodeListByState()
        {
            Dictionary<string, object> param = new Dictionary<string, object>
            {
                { "ENTERPRISEID", UserInfo.Current.Enterprise },
                { "LANGUAGETYPE", UserInfo.Current.LanguageType },
                { "CODECLASSID", "RequestState"}
            };

            ucMessageInfo.Statecombobox.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            ucMessageInfo.Statecombobox.ValueMember = "CODEID";
            ucMessageInfo.Statecombobox.DisplayMember = "CODENAME";
            //combostate.EditValue = "Y";
            ucMessageInfo.Statecombobox.DataSource = SqlExecuter.Query("GetCodeList", "00001", param);
            ucMessageInfo.Statecombobox.ShowHeader = false;

        }


        /// <summary>
        /// Code List에서 SR 요쳥유형 Combo code를 가져온다
        /// </summary>
        private void GetCodeListByRequestType()
        {
            Dictionary<string, object> param = new Dictionary<string, object>
            {
                { "ENTERPRISEID", UserInfo.Current.Enterprise },
                { "LANGUAGETYPE", UserInfo.Current.LanguageType },
                { "CODECLASSID", "SRRequestType"}
            };

            ucMessageInfo.RequestTypecombobox.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            ucMessageInfo.RequestTypecombobox.ValueMember = "CODEID";
            ucMessageInfo.RequestTypecombobox.DisplayMember = "CODENAME";
            //combostate.EditValue = "Y";
            ucMessageInfo.RequestTypecombobox.DataSource = SqlExecuter.Query("GetCodeList", "00001", param);
            ucMessageInfo.RequestTypecombobox.ShowHeader = false;

        }

        /// <summary>
        /// Code List에서 SR 작업메뉴 Combo code를 가져온다
        /// </summary>
        private void GetCodeListByWorkType()
        {
            Dictionary<string, object> param = new Dictionary<string, object>
            {
                { "ENTERPRISEID", UserInfo.Current.Enterprise },
                { "LANGUAGETYPE", UserInfo.Current.LanguageType },
                { "CODECLASSID", "SRWorkType"}
            };

            ucMessageInfo.WorkTypecombobox.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            ucMessageInfo.WorkTypecombobox.ValueMember = "CODEID";
            ucMessageInfo.WorkTypecombobox.DisplayMember = "CODENAME";
            //combostate.EditValue = "Y";
            ucMessageInfo.WorkTypecombobox.DataSource = SqlExecuter.Query("GetCodeList", "00001", param);
            ucMessageInfo.WorkTypecombobox.ShowHeader = false;

        }



        /// <summary>
        /// 요청 grid row 클릭시 해당 정보와 파일정보 출력
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_RowChanged(object sender, EventArgs e)
        {
            RequestRowChanged();
        }

        /// <summary>
        ///  트리 메뉴 클릭시 해당하는 요청정보 그리드에 조회
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeMenu_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            treeMenu_FocusedNodeChanged1();
        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장 - Comment 그리드에서 저장 버튼 클릭시 호출
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, EventArgs e)
        {

            DataRow row = grdrequest.View.GetFocusedDataRow();
          
            String comment = txtcomment.Text;
            if (row == null)
            {
                return;
            }

            DataTable commentdt = grdcomment.DataSource as DataTable;
               int srno = int.Parse(row["SRNO"].ToString());
            
            int sequence = 0;
            for(int i=0;i<grdcomment.View.RowCount;i++)
            {
                if(int.Parse(commentdt.Rows[i]["SEQUENCE"].ToString())> sequence)
                {
                    sequence = int.Parse(commentdt.Rows[i]["SEQUENCE"].ToString());
                }


            }

            DataTable grdcomments = grdcomment.GetChangedRows();
            DataTable maxcomments = grdcomment.DataSource as DataTable;
            int max = 0;

            foreach(DataRow dr in maxcomments.Rows)
            {
                if(Convert.ToInt32(dr["SEQUENCE"].ToString())>max)
                {
                    max = Convert.ToInt32(dr["SEQUENCE"].ToString());
                }

            }

            if (grdcomments.Rows.Count == 0 && comment.Equals("")) // 
            {
                throw MessageException.Create("NoSaveData"); // 저장할 데이터가 없습니다.
            }

            MessageWorker worker = new MessageWorker("SaveRequestComment");
            worker.SetBody(new MessageBody()
            {
                { "VALIDSTATE", "Valid" },
                { "list", grdcomments },
                { "COMMENT", comment },
                { "SRNO", srno },
                { "SEQUENCE", max+1 },
            });
            worker.Execute();
            this.ShowMessage("SuccessSave");
            RequestRowChanged();

        }

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            DataTable filedt = fileSr.GetChangedRows();
            MessageWorker worker = new MessageWorker("SaveFileInfo");
            DataRow dr = this.grdrequest.View.GetFocusedDataRow();
            if (filedt != null || dr != null) // 
            {
                fileSr.SaveChangedFiles();
                if (dr == null || adding_check == 1)
                {
                    worker.SetBody(new MessageBody()
                 {
                { "VALIDSTATE", "Valid" },
                {"RESOURCEVERSION", "1" },
                { "filelist", filedt }
                    });
                    worker.Execute();
                }
                else
                {
                    worker.SetBody(new MessageBody()
            {
                { "VALIDSTATE", "Valid" },
                { "SRNO", dr["SRNO"].ToString() },
                {"RESOURCEVERSION", "1" },
                { "filelist", filedt }
            });
                    worker.Execute();
                }
                
            }

                DataRow treeRow = treeMenu.GetFocusedDataRow();


            if (adding_check == 1)
            {
                dr = grdrequest.View.GetFocusedDataRow();
                DataTable statedt = ucMessageInfo.Statecombobox.DataSource as DataTable;
                DataTable workdt = ucMessageInfo.WorkTypecombobox.DataSource as DataTable;
                DataTable requestdt = ucMessageInfo.RequestTypecombobox.DataSource as DataTable;
                String srstate = "";
                String srworktype = "";
                String srrequesttype = "";
                String menuid = treeRow["MENUID"].ToString();
                String title = ucMessageInfo.TitleText;
                String contents = ucMessageInfo.CommentText;

                if (ucMessageInfo.Statecombobox.EditValue != null)
                {
                    for (int i = 0; i < statedt.Rows.Count; i++)
                    {
                        if (statedt.Rows[i][0].ToString().Equals(ucMessageInfo.Statecombobox.EditValue.ToString()))
                        {
                            srstate = statedt.Rows[i]["CODEID"].ToString();
                        }
                    }
                }
                if (ucMessageInfo.RequestTypecombobox.EditValue != null)
                {
                    for (int i = 0; i < requestdt.Rows.Count; i++)
                    {
                        if (requestdt.Rows[i][0].ToString().Equals(ucMessageInfo.RequestTypecombobox.EditValue.ToString()))
                        {
                            srrequesttype = requestdt.Rows[i]["CODEID"].ToString();
                        }
                    }
                }

                    if (ucMessageInfo.WorkTypecombobox.EditValue != null)
                    {
                        for (int i = 0; i < workdt.Rows.Count; i++)
                        {
                            if (workdt.Rows[i][0].ToString().Equals(ucMessageInfo.WorkTypecombobox.EditValue.ToString()))
                            {
                                srworktype = workdt.Rows[i]["CODEID"].ToString();
                            }
                        }
                    }

                worker = new MessageWorker("SaveRequestInfo");
                worker.SetBody(new MessageBody()
            {
                { "VALIDSTATE", "Valid" },
                { "COMMENT", contents },
                {"SRWORKTYPE", srworktype },
                {"SRREQUESTTYPE",srrequesttype },
                {"MENUID", menuid },
                {"TITLE", title },
                {"SRSTATE", srstate },
                 {"adding_check", adding_check }

            });
                worker.Execute();
                adding_check = 0;
            }

            else if (adding_check == 0)
            {
                grdrequest.View.CheckValidation();
                DataTable deletegrd = grdrequest.GetChangedRows();
                DataRow updaterow = grdrequest.View.GetFocusedDataRow();
                if(deletegrd.Rows.Count==0 && updaterow==null)
                {
                    throw MessageException.Create("NoSaveData");
                }
                worker = new MessageWorker("SaveRequestInfo");
                worker.SetBody(new MessageBody()
            {
                { "VALIDSTATE", "Valid" },
                 {"deletelist", deletegrd},
                {"SRSTATEID", ucMessageInfo.Statecombobox.EditValue.ToString() },
                {"SRWORKTYPEID",ucMessageInfo.WorkTypecombobox.EditValue.ToString() },
                {"SRREQUESTTYPEID", ucMessageInfo.RequestTypecombobox.EditValue.ToString() },
                {"CONTENTS", ucMessageInfo.CommentText },
                {"TITLE", ucMessageInfo.TitleText },
                { "SRNO", dr["SRNO"].ToString() }

            });
                worker.Execute();
                treeMenu_FocusedNodeChanged1();
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
            treeMenu_FocusedNodeChanged1();
            RequestRowChanged();
        }

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
        /// </summary>
        protected override void OnValidateContent()
        {

        }

        #endregion

        #region Private Function

        // TODO : 화면에서 사용할 내부 함수 추가
        /// <summary>
        /// Request GRID에서 다른 ROW를 선택했을시 조회
        /// </summary>
        private void RequestRowChanged()
        {
            DataRow requestrow = grdrequest.View.GetFocusedDataRow();
            if (requestrow == null || requestrow["SRNO"].ToString().Equals(""))
            {
                return;
            }
            ucMessageInfo.Statecombobox.EditValue = requestrow["SRSTATEID"].ToString();
            ucMessageInfo.WorkTypecombobox.EditValue = requestrow["SRWORKTYPEID"].ToString();
            ucMessageInfo.RequestTypecombobox.EditValue = requestrow["SRREQUESTTYPEID"].ToString();
            ucMessageInfo.CommentText = requestrow["CONTENTS"].ToString();
            ucMessageInfo.TitleText = requestrow["TITLE"].ToString();
            String srno = requestrow["SRNO"].ToString();
            txtcomment.Text = null;

            Dictionary<string, object> param = new Dictionary<string, object>()
            {
                { "RESOURCEID", srno}
            };

            Dictionary<string, object> commentparam = new Dictionary<string, object>();
            commentparam.Add("SRNO", srno);

            DataTable filedt = SqlExecuter.Query("GetRequestFile", "10001", param);
            DataTable commentdt = SqlExecuter.Query("GetRequestSrComment", "10001", commentparam);
            if (filedt.Rows.Count == 0 || commentdt.Rows.Count == 0)
            {
                if (filedt.Rows.Count == 0 && commentdt.Rows.Count > 0)
                {
                    grdcomment.DataSource = commentdt;
                }
                else if (filedt.Rows.Count > 0 && commentdt.Rows.Count == 0)
                {
                    fileSr.DataSource = filedt;
                }
                else
                {
                    fileSr.ClearData();
                    grdcomment.DataSource = null;
                }       
            }
            grdcomment.DataSource = commentdt;
            fileSr.DataSource = filedt;
            adding_check = 0;
        }

        /// <summary>
        ///  트리 메뉴 클릭시 해당하는 요청정보 그리드에 조회
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeMenu_FocusedNodeChanged1()
        {

            DataRow focusRow = treeMenu.GetFocusedDataRow();
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("p_menuId", focusRow["MENUID"].ToString()); // 
            param.Add("p_validState", "Valid");
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
        
            if (focusRow["MENUID"].ToString().Equals("*"))
            {

                // TODO : 조회 SP 변경
                var values = Conditions.GetValues();
                values.Add("P_VALIDSTATE", "Valid");
                values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                DataTable dt = SqlExecuter.Query("SelectAllRequestSearch", "10001", values);

                grdrequest.DataSource = dt;
                RequestRowChanged();
            }
            else if (!focusRow["MENUTYPE"].ToString().ToUpper().Equals("FOLDER"))
            {

                //grdMenu.DataSource = Procedure("usp_com_selectMenu_grid", param);
                grdrequest.DataSource = SqlExecuter.Query("SelectUserClassRequestSearch", "10001", param);

                if (grdrequest.View.RowCount > 0)
                {

                    ucMessageInfo.Statecombobox.EditValue = (grdrequest.DataSource as DataTable).Rows[0]["SRSTATEID"].ToString();
                    String srno = (grdrequest.DataSource as DataTable).Rows[0]["SRNO"].ToString();

                    ucMessageInfo.TitleText = (grdrequest.DataSource as DataTable).Rows[0]["TITLE"].ToString();
                    ucMessageInfo.WorkTypecombobox.EditValue = (grdrequest.DataSource as DataTable).Rows[0]["SRWORKTYPEID"].ToString();
                    ucMessageInfo.RequestTypecombobox.EditValue = (grdrequest.DataSource as DataTable).Rows[0]["SRREQUESTTYPEID"].ToString();
                    ucMessageInfo.CommentText = (grdrequest.DataSource as DataTable).Rows[0]["CONTENTS"].ToString();
                    Dictionary<string, object> fileparam = new Dictionary<string, object>()
                     {
                          { "RESOURCEID", srno}
                     };

                    Dictionary<string, object> commentparam = new Dictionary<string, object>();
                    commentparam.Add("SRNO", srno);
                    DataTable filedt = SqlExecuter.Query("GetRequestFile", "10001", fileparam);
                    DataTable commnetdt = SqlExecuter.Query("GetRequestSrComment", "10001", commentparam);
                    if (filedt.Rows.Count == 0 || commnetdt.Rows.Count == 0)
                    {
                        if (filedt.Rows.Count == 0 && commnetdt.Rows.Count > 0)
                        {
                            grdcomment.DataSource = commnetdt;
                        }
                        else if (filedt.Rows.Count > 0 && commnetdt.Rows.Count == 0)
                        {
                            fileSr.DataSource = filedt;
                        }
                        else
                        {
                            fileSr.ClearData();
                            grdcomment.DataSource = null;
                        }
                    }
                    grdcomment.DataSource = commnetdt;
                    fileSr.DataSource = filedt;


                }

                else if (grdrequest.View.RowCount == 0)
                {

                    ucMessageInfo.Statecombobox.EditValue = null;
                    fileSr.ClearData();
                    grdcomment.DataSource = null;
                    txtcomment.Text = null;
                    ucMessageInfo.CommentText = null;
                    ucMessageInfo.TitleText = null;
                    ucMessageInfo.WorkTypecombobox.EditValue = null; 
                    ucMessageInfo.RequestTypecombobox.EditValue = null;
                }
            }
            else if(focusRow ==null)
            {
                var values = Conditions.GetValues();
                values.Add("P_VALIDSTATE", "Valid");
                values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                DataTable dt = SqlExecuter.Query("SelectAllRequestSearch", "10001", values);

                grdrequest.DataSource = dt;
                RequestRowChanged();
            }
            else
            {
                grdrequest.DataSource = null;
                ucMessageInfo.Statecombobox.EditValue = null;
                ucMessageInfo.WorkTypecombobox.EditValue = null;
                ucMessageInfo.RequestTypecombobox.EditValue = null;
                ucMessageInfo.CommentText = null;
                ucMessageInfo.TitleText = null;
                fileSr.ClearData();
                grdcomment.DataSource = null;

                txtcomment.Text = null;
            }
            pnlContent.CloseWaitArea();
        }

        #endregion

        private void smartSplitTableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void smartLabelComboBox1_Load(object sender, EventArgs e)
        {

        }

        private void smartLabel6_Click(object sender, EventArgs e)
        {

        }

        private void smartSpliterContainer3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtcomment_Click(object sender, EventArgs e)
        {

        }

        private void btn_filesave_Click(object sender, EventArgs e)
        {

        }

        private void smartSplitTableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ucMessageInfo_Load(object sender, EventArgs e)
        {

        }

        private void btn_add_Click(object sender, EventArgs e)
        {

        }
    }
}