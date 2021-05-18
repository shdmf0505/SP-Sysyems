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

namespace Micube.SmartMES.StandardInfo
{
	public partial class AreaOutPerson : SmartConditionManualBaseForm
	{
		#region 생성자
		public AreaOutPerson()
		{
			InitializeComponent();
         
            InitializeEvent();

        }
      
        #endregion

        private void InitializeEvent()
        {
            ////////grdAAGList.View.AddingNewRow += View_AddingNewRow;
            ///

            //grdCodeClass.ToolbarRefresh += GrdCodeClass_ToolbarRefresh;
            //grdCode.View.AddingNewRow += View_AddingNewRow;

            //grdAAGList.View.FocusedRowChanged += grdAAGList_FocusedRowChanged;
            //grdAAGList.View.AddingNewRow += grdAAGList_AddingNewRow;
            //grdAttribGList.View.AddingNewRow += grdAttribGList_AddingNewRow;

            //grdAttribGList.View.ShownEditor += grdAttribGList_ShownEditor;
            //grdAAGList.View.ShownEditor += grdAAGList_ShownEditor;

            treeParentArea.FocusedNodeChanged += TreeParentArea_FocusedNodeChanged;
            grdAreaPerson.View.AddingNewRow += grdAreaPerson_AddingNewRow;

        }

        private void grdAreaPerson_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            DataRow focusrow =  treeParentArea.GetFocusedDataRow();

            if(focusrow["AREATYPE"].ToString() != "Area")
            {
                sender.CancelAddNew();
            }

            // 채번 시리얼 존재 유무 체크


            //Dictionary<string, object> parampf = new Dictionary<string, object>();
            //parampf.Add("IDCLASSID", "OSPPerson");
            //parampf.Add("PREFIX", "OSP");

            //DataTable dtItemserialChk = SqlExecuter.Query("GetProductitemserial", "10001", parampf);

            //DataTable dtItemserialI = dtItemserialChk.Clone();
            //dtItemserialI.Columns.Add("_STATE_");


            //if (dtItemserialChk != null)
            //{
            //    if (dtItemserialChk.Rows.Count == 0)
            //    {
            //        DataRow rowItemserialI = dtItemserialI.NewRow();
            //        rowItemserialI["IDCLASSID"] = "OSPPerson";
            //        rowItemserialI["PREFIX"] = "OSP";
            //        rowItemserialI["LASTSERIALNO"] = "1";
            //        rowItemserialI["_STATE_"] = "added";
            //        dtItemserialI.Rows.Add(rowItemserialI);


            //    }
            //    else
            //    {
            //        DataRow rowItemserialI = dtItemserialI.NewRow();
            //        rowItemserialI["IDCLASSID"] = "OSPPerson";
            //        rowItemserialI["PREFIX"] = "OSP";

            //        int ilastserialno = 0;
            //        ilastserialno = Int32.Parse(dtItemserialChk.Rows[0]["LASTSERIALNO"].ToString());
            //        ilastserialno = ilastserialno + 1;


            //        rowItemserialI["LASTSERIALNO"] = ilastserialno.ToString();
            //        rowItemserialI["_STATE_"] = "modified";
            //        dtItemserialI.Rows.Add(rowItemserialI);
            //    }
            //}
            //else
            //{
            //    DataRow rowItemserialI = dtItemserialI.NewRow();
            //    rowItemserialI["IDCLASSID"] = "OSPPerson";
            //    rowItemserialI["PREFIX"] = "OSP";
            //    rowItemserialI["LASTSERIALNO"] = "1";
            //    rowItemserialI["_STATE_"] = "added";
            //    dtItemserialI.Rows.Add(rowItemserialI);
            //}

            //dtItemserialI.TableName = "idclassserial";
            //DataTable dtemp = new DataTable();
            //dtemp.TableName = "temp";
            //DataSet dsChang = new DataSet();
            //dsChang.Tables.Add(dtItemserialI);
            //dsChang.Tables.Add(dtemp);

            //ExecuteRule("AreaWorker", dsChang);


            args.NewRow["USERID"] = "*";


            args.NewRow["AREAID"] = focusrow["AREAID"];
            args.NewRow["OWNTYPE"] = focusrow["OWNTYPE"];
            args.NewRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
            args.NewRow["PLANTID"] = UserInfo.Current.Plant;
            args.NewRow["VALIDSTATE"] = "Valid";

        }

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
            param.Add("OWNTYPE", "'InHouseOSP','OutsideOSP'");

            treeParentArea.DataSource = SqlExecuter.Query("SelectAreaTreeList", "10002", param);//Procedure("usp_com_selectarea_tree", param);

            treeParentArea.PopulateColumns();

            treeParentArea.ExpandAll();

            treeParentArea.SetFocusedNode(treeParentArea.FindNodeByFieldValue("AREAID_COPY", areaId));

        }

    
   

 

        #region 툴바
        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {

            DataTable changed = new DataTable();

            changed = grdAreaPerson.GetChangedRows();


            base.OnToolbarSaveClick();

            changed.TableName = "areaWorker";
            DataTable dtemp = new DataTable();
            dtemp.TableName = "temp";
            DataSet dsChang = new DataSet();
            dsChang.Tables.Add(changed);
            dsChang.Tables.Add(dtemp);

            ExecuteRule("AreaWorker", dsChang);
          

            //grdAAGList.View.SetIsReadOnly(false);
            //grdAttribGList.View.SetIsReadOnly(false);

        }
        #endregion

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


        #region 검색

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




        #region 유효성 검사
        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            //DataTable changed = new DataTable();
            //grdAAGList.View.CheckValidation();
            //changed = grdAAGList.GetChangedRows();

            //DataTable changed1 = new DataTable();
            //grdAttribGList.View.CheckValidation();
            //changed1 = grdAttribGList.GetChangedRows();


            //if (changed.Rows.Count == 0 && changed1.Rows.Count == 0)
            //{
            //    // 저장할 데이터가 존재하지 않습니다.
            //    throw MessageException.Create("NoSaveData");
            //}

            DataTable changed = new DataTable();
            grdAreaPerson.View.CheckValidation();
            changed = grdAreaPerson.GetChangedRows();

            DataTable dtChk = (DataTable)grdAreaPerson.DataSource;

            foreach (DataRow row in changed.Rows)
            {
                DataRow[] rowChk = dtChk.Select("WORKERNAME = '" + row["WORKERNAME"] + "'");
                if (rowChk.Length > 1)
                {
                    //if (MSGBox.Show(MessageBoxType.Question, "WorkerName", MessageBoxButtons.YesNo) == DialogResult.No)
                    //{
                        throw MessageException.Create("WorkerName");
                    //}
                }
            }

            if (changed.Rows.Count == 0 )
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }


        }
        #endregion


        #region 컨텐츠 영역 초기화


        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGridIdDefinitionManagement()
        {
          
            //작업자
            grdAreaPerson.GridButtonItem = GridButtonItem.All;   // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기
            grdAreaPerson.View.AddTextBoxColumn("AREAID").SetIsHidden();
            grdAreaPerson.View.AddTextBoxColumn("ENTERPRISEID").SetIsHidden();
            grdAreaPerson.View.AddTextBoxColumn("PLANTID").SetIsHidden();

            grdAreaPerson.View.AddTextBoxColumn("USERID",100).SetIsHidden();
            grdAreaPerson.View.AddTextBoxColumn("WORKERNAME",150);

            grdAreaPerson.View.AddComboBoxColumn("OWNTYPE",100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OwnType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdAreaPerson.View.AddComboBoxColumn("WORKERTYPE",100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=WORKERTYPE", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
            .SetValidationIsRequired();
            grdAreaPerson.View.AddComboBoxColumn("ISMAINAREA", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
            .SetValidationIsRequired();
            grdAreaPerson.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));


            grdAreaPerson.View.PopulateColumns();
        }

       

        protected override void InitializeContent()
		{
			base.InitializeContent();
            InitializeTreeArea();
            InitializeGridIdDefinitionManagement();
        }



        #endregion

    }
}
