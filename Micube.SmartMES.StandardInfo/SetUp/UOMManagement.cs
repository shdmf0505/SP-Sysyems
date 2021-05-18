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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Micube.SmartMES.Commons;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;

#endregion

namespace Micube.SmartMES.StandardInfo
{
	/// <summary>
	/// 프 로 그 램 명  : 기준정보 > Setup > UOM 관리
	/// 업  무  설  명  : Unit Of Measure ,  측정 단위를 저장하고 관리하는 화면
	/// 생    성    자  : 정승원
	/// 생    성    일  : 2019-05-17
	/// 수  정  이  력  : 한주석 - 2019-09-19 UOM MAP, UOM Conversion grid 추가 
	/// 
	/// 
	/// </summary>
	public partial class UOMManagement : SmartConditionManualBaseForm
	{
		#region Local Variables

		// TODO : 화면에서 사용할 내부 변수 추가

		#endregion

		#region 생성자

		public UOMManagement()
		{
			InitializeComponent();
		}

		#endregion

		#region 컨텐츠 영역 초기화
		/// <summary>
		/// 컨텐츠 영역 초기화 시작
		/// </summary>
		protected override void InitializeContent()
		{
			base.InitializeContent();

            InitializeEvent();

			// TODO : 컨트롤 초기화 로직 구성
			InitializeGrid();
            this.ucDataUpDownBtn.SourceGrid = this.grdUOMDefinitionList;
            this.ucDataUpDownBtn.TargetGrid = this.grdMapSave;

            //tab 다국어 초기화
            tabPartition.SetLanguageKey(xtraTabPage1, "UOMCLASS");
			tabPartition.SetLanguageKey(xtraTabPage2, "UOMDEFINITION");
        }

		/// <summary>        
		/// 그리드 초기화
		/// </summary>
		private void InitializeGrid()
		{
            // TODO : 그리드 초기화 로직 추가
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///TAB 0 : UOM Class
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if ( UserInfo.Current.Enterprise == "YOUNGPOONG")
                grdMainUOMClassList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            else
            {
                // 인터의 경우 UOM은 ERP에서 다운로드 함
                grdMainUOMClassList.GridButtonItem = GridButtonItem.Export;
            }

            grdMainUOMClassList.View.AddTextBoxColumn("UOMCLASSID", 150)
				.SetValidationKeyColumn()
				.SetValidationIsRequired();
			grdMainUOMClassList.View.AddTextBoxColumn("UOMCLASSNAME", 200);
			grdMainUOMClassList.View.AddTextBoxColumn("ENTERPRISEID")
				.SetIsHidden()
				.SetDefault("*", "*");
			grdMainUOMClassList.View.AddTextBoxColumn("PLANTID")
				.SetIsHidden()
				.SetDefault("*", "*");

			//유효상태, 생성자, 수정자...
			grdMainUOMClassList.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
				.SetDefault("Valid")
				.SetValidationIsRequired()
				.SetTextAlignment(TextAlignment.Center);
			grdMainUOMClassList.View.AddTextBoxColumn("CREATOR", 80)
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);
			grdMainUOMClassList.View.AddTextBoxColumn("CREATEDTIME", 130)
				.SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);
			grdMainUOMClassList.View.AddTextBoxColumn("MODIFIER", 80)
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);
			grdMainUOMClassList.View.AddTextBoxColumn("MODIFIEDTIME", 130)
				.SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);

			grdMainUOMClassList.View.PopulateColumns();


			//////////////////////////////////////////////////////////////////////////////////////////////////////////////
			///TAB 1 : UOM Definition
			//////////////////////////////////////////////////////////////////////////////////////////////////////////////
			//UOM Class
			//grdUOMClassList.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;
			grdUOMClassList.View.AddTextBoxColumn("UOMCLASSID", 150)
				.SetIsReadOnly();
			grdUOMClassList.View.AddTextBoxColumn("UOMCLASSNAME")
				.SetIsReadOnly();

			grdUOMClassList.View.PopulateColumns();


            //UOM Definition
            //grdUOMDefinitionList.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;
            if (UserInfo.Current.Enterprise == "YOUNGPOONG")
                grdUOMDefinitionList.GridButtonItem = GridButtonItem.CRUD | GridButtonItem.Export;
            else
            {
                // 인터의 경우 UOM은 ERP에서 다운로드 함
                grdUOMDefinitionList.GridButtonItem = GridButtonItem.Export;
            }
            grdUOMDefinitionList.View.AddTextBoxColumn("UOMCLASSID", 150)
				.SetIsReadOnly()
				.SetValidationIsRequired();
			grdUOMDefinitionList.View.AddTextBoxColumn("UOMDEFID", 150)
				.SetValidationIsRequired()
				.SetValidationKeyColumn();
			grdUOMDefinitionList.View.AddTextBoxColumn("UOMDEFNAME", 200);
			grdUOMDefinitionList.View.AddTextBoxColumn("DESCRIPTION", 150);
			grdUOMDefinitionList.View.AddTextBoxColumn("ENTERPRISEID")
				.SetIsHidden()
				.SetDefault("*", "*");
			grdUOMDefinitionList.View.AddTextBoxColumn("PLANTID")
				.SetIsHidden()
				.SetDefault("*", "*");

			//유효상태, 생성자, 수정자...
			grdUOMDefinitionList.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
				.SetDefault("Valid")
				.SetValidationIsRequired()
				.SetTextAlignment(TextAlignment.Center);
			grdUOMDefinitionList.View.AddTextBoxColumn("CREATOR", 80)
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);
			grdUOMDefinitionList.View.AddTextBoxColumn("CREATEDTIME", 130)
				.SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);
			grdUOMDefinitionList.View.AddTextBoxColumn("MODIFIER", 80)
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);
			grdUOMDefinitionList.View.AddTextBoxColumn("MODIFIEDTIME", 130)
				.SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);
            grdUOMDefinitionList.View.AddTextBoxColumn("ISADD", 100)
                .SetIsReadOnly()
                .SetIsHidden();

            grdUOMDefinitionList.View.PopulateColumns();



			//////////////////////////////////////////////////////////////////////////////////////////////////////////////
			///TAB 1-0 : UOM Map
			//////////////////////////////////////////////////////////////////////////////////////////////////////////////

			//UOM MapList
			//grdMapList.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;
			grdMapList.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export;
            grdMapList.View.AddTextBoxColumn("UOMCATEGORY", 250)
                .SetLabel("UOMMAP")
                .SetTextAlignment(TextAlignment.Center);
            grdMapList.View.AddTextBoxColumn("PLANTID", 100)
                .SetIsHidden();
            grdMapList.View.AddTextBoxColumn("ENTERPRISEID", 100)
                .SetIsHidden();
            grdMapList.View.AddTextBoxColumn("UOMDEFID", 10)
                .SetIsHidden();

            grdMapList.View.PopulateColumns();

            //UOM MapSave
            //grdMapSave.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;
            grdMapSave.GridButtonItem = GridButtonItem.Export;
            grdMapList.View.AddTextBoxColumn("UOMCATEGORY", 100)
                .SetLabel("UOMMAP")
                .SetIsHidden();
            grdMapSave.View.AddTextBoxColumn("UOMCLASSID", 150)
                .SetIsReadOnly();
            grdMapSave.View.AddTextBoxColumn("UOMDEFID", 150);
            grdMapSave.View.AddTextBoxColumn("UOMDEFNAME", 150);
            grdMapSave.View.AddTextBoxColumn("DESCRIPTION", 150);
            grdMapSave.View.AddTextBoxColumn("VALIDSTATE", 80)
                .SetIsReadOnly();
            grdMapSave.View.AddTextBoxColumn("PLANTID", 100)
                .SetIsHidden()
                .SetIsReadOnly();
            grdMapSave.View.AddTextBoxColumn("ENTERPRISEID", 100)
                .SetIsHidden()
                .SetIsReadOnly();
            grdMapSave.View.AddTextBoxColumn("ISADD", 100)
                .SetIsReadOnly()
                .SetIsHidden();


            grdMapSave.View.PopulateColumns();


            //////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///TAB 1-1 : UOM Conversion
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //UOM Conversion

            //InitializeGrid_ITEMPOPUP();
            //grdUOMConversion.View.AddTextBoxColumn("ITEMNAME", 200);
            //grdUOMConversion.View.AddTextBoxColumn("ITEMVERSION", 200);
            //grdUOMConversion.View.AddTextBoxColumn("UOMDEFID", 150);
            //grdUOMConversion.View.AddTextBoxColumn("CONVERSIONVALUE", 150)
            //   .SetValidationIsRequired();
            //InitializeGrid_UOMPopup();
            //grdUOMConversion.View.AddTextBoxColumn("DESCRIPTION", 150);
            //grdUOMConversion.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
            // .SetDefault("Valid");
            //grdUOMConversion.View.AddTextBoxColumn("PLANTID", 100)
            //    .SetIsHidden();
            //grdUOMConversion.View.AddTextBoxColumn("ENTERPRISEID", 100)
            //    .SetIsHidden();

            //grdUOMConversion.View.PopulateColumns();
        }

		#endregion

		#region comment
		/// <summary>
		/// ITEM POPUP
		/// </summary>
		//private void InitializeGrid_ITEMPOPUP()
		//{
		//    var itemPopupColumn = grdUOMConversion.View.AddSelectPopupColumn("ITEMID", new SqlQuery("GetUomItem", "10001"))
		//        .SetPopupLayout("ITEMID", PopupButtonStyles.Ok_Cancel, true, false)
		//        .SetPopupResultCount(1)
		//        .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
		//        .SetPopupAutoFillColumns("ITEMNAME")
		//        .SetPopupApplySelection((selectedRows, dataGridRow) =>
		//        {
		//            // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
		//            // dataGridRow : 현재 Focus가 있는 그리드의 DataRow
		//            foreach (DataRow row in selectedRows)
		//            {
		//                dataGridRow["ITEMID"] = row["ITEMID"].ToString();
		//                dataGridRow["ITEMNAME"] = row["ITEMNAME"].ToString();
		//                dataGridRow["ITEMVERSION"] = row["ITEMVERSION"].ToString();

		//            }
		//        });

		//    itemPopupColumn.Conditions.AddTextBox("TXTITEMNAME")
		//        .SetLabel("TXTPRODUCTDEFNAME");

		//    itemPopupColumn.GridColumns.AddTextBoxColumn("ITEMID", 150);
		//    itemPopupColumn.GridColumns.AddTextBoxColumn("ITEMNAME", 250);
		//    itemPopupColumn.GridColumns.AddTextBoxColumn("ITEMVERSION", 250);
		//}


		/// <summary>
		/// UOM POPUP
		/// </summary>
		//private void InitializeGrid_UOMPopup()
		//{
		//    var uomPopupColumn = grdUOMConversion.View.AddSelectPopupColumn("FROMUOM", new SqlQuery("GetConvUom", "10001"))
		//        .SetPopupLayout("FROMUOM", PopupButtonStyles.Ok_Cancel, true, false)
		//        .SetPopupResultCount(1)
		//        .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
		//        .SetPopupAutoFillColumns("FROMNAME")
		//        .SetPopupApplySelection((selectedRows, dataGridRow) =>
		//        {
		//            // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
		//            // dataGridRow : 현재 Focus가 있는 그리드의 DataRow
		//            foreach (DataRow row in selectedRows)
		//            {
		//                dataGridRow["FROMUOM"] = row["FROMUOM"].ToString();
		//            }
		//        });

		//    uomPopupColumn.Conditions.AddTextBox("TXTUOMMNAME")
		//        .SetLabel("TXTUOMCONVNAME");

		//    uomPopupColumn.GridColumns.AddTextBoxColumn("FROMUOM", 150);
		//    uomPopupColumn.GridColumns.AddTextBoxColumn("FROMNAME", 250);

		//}
		#endregion

		#region Event

		/// <summary>        
		/// 이벤트 초기화
		/// </summary>
		public void InitializeEvent()
		{
			// TODO : 화면에서 사용할 이벤트 추가


			tabPartition.SelectedPageChanged += TabPartition_SelectedPageChanged;
            //smartTabControl1.SelectedPageChanged += TabPartition_SelectedPageChanged2;
            grdMapList.View.FocusedRowChanged += MapView_FocusedRowChanged;
            //grdUOMConversion.View.AddingNewRow += View_AddingNewRow1;
            grdUOMClassList.View.FocusedRowChanged += View_FocusedRowChanged;
            grdUOMDefinitionList.View.AddingNewRow += View_AddingNewRow;
            //grdUOMDefinitionList.View.RowClick += View_RowChanged1;
            ucDataUpDownBtn.buttonClick += UcDataUpDownBtnCtrl_buttonClick;
            grdMapList.View.ShowingEditor += View_ShowingEditor;
            // 삭제버튼 숨기기            
            //new SetGridDeleteButonVisible(grdUOMDefinitionList);
        }

        private void View_AddingNewRow1(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            DataRow dr = grdUOMDefinitionList.View.GetFocusedDataRow();
            args.NewRow["UOMDEFID"] = dr["UOMDEFID"];
        }

        /// <summary>
        // UOM CLASS LIST에 해당되는 UOMDEF 조회
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //int page = smartTabControl1.SelectedTabPageIndex;
            //if (page == 0)
            //{
            View_UomDefRowChanged();
            //}
            //else
            //{
            //    DataRow uomclassrow = grdUOMClassList.View.GetFocusedDataRow();

            //    Dictionary<string, object> classparam = new Dictionary<string, object>();
            //    classparam.Add("p_uomclassid", uomclassrow["UOMCLASSID"].ToString());
            //    classparam.Add("p_validstate", "Valid");

            //    DataTable dtUomDefRefreshList = SqlExecuter.Query("SelectUomDefList", "10003", classparam);//Procedure("usp_com_selectuomdefinition", param);
            //    DataRow row = grdMapList.View.GetFocusedDataRow();

            //    if (row == null)
            //    {
            //        return;
            //    }

            //    grdUOMDefinitionList.DataSource = dtUomDefRefreshList;

            //}
        }
        /// <summary>
        /// UOM MAP LIST 로우 변경시 MAPSAVE 그리드와 DEFID가 겹치지 않는 UOM DEF 그리드조회
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void MapView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
             View_MAP_RowView();
             View_UomDefRowChanged();
 
        }

		/// <summary>
		/// 탭 전환 시 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TabPartition_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
		{
			string languageKey = tabPartition.GetLanguageKey(tabPartition.SelectedTabPage);
			if (languageKey != "UOMDEFINITION") return;
			LoadDataGridUOMClass();
			View_MAP_RowView();
			FocusedRowChanged();
		}

        /// <summary>
		/// CONV 탭 전환 시 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TabPartition_SelectedPageChanged2(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            //int page = smartTabControl1.SelectedTabPageIndex;

            //if(page==0)
            //{

            View_UomDefRowChanged();
            View_MAP_RowView();
               
                
            //}
            //else
            //{

            //    DataRow uomclassrow = grdUOMClassList.View.GetFocusedDataRow();

            //    Dictionary<string, object> classparam = new Dictionary<string, object>();
            //    classparam.Add("p_uomclassid", uomclassrow["UOMCLASSID"].ToString());
            //    classparam.Add("p_validstate", "Valid");

            //    DataTable dtUomDefRefreshList = SqlExecuter.Query("SelectUomDefList", "10003", classparam);//Procedure("usp_com_selectuomdefinition", param);
            //    DataRow row = grdMapList.View.GetFocusedDataRow();

            //    if (row == null)
            //    {
            //        return;
            //    }

            //    grdUOMDefinitionList.DataSource = dtUomDefRefreshList;
            //    View_ConvRowChanged();
            //}
        }

        /// <summary>
        /// UOM 리스트에서 row 클릭 시 UOM Conversion 조회 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void View_RowChanged1(object sender, EventArgs e)
        //{
        //    View_ConvRowChanged();
            
            
        //}


        /// <summary>
        /// UOM 리스트에서 Up Down 버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void UcDataUpDownBtnCtrl_buttonClick(object sender, EventArgs e)
        {   
            if(this.grdUOMDefinitionList.InvokeRequired)
            {
                this.grdUOMDefinitionList.Invoke(new EventHandler(UcDataUpDownBtnCtrl_buttonClick), sender, e);
                return;
            }
            string btnState = ucDataUpDownBtn.ButtonState;
            if (btnState.Equals("Down"))
            {
				DataTable addedDt = grdUOMDefinitionList.GetChangesAdded();
				if(addedDt.Rows.Count > 0)
				{
					//uom리스트를 먼저 저장 후 진행해주세요.
					throw MessageException.Create("UOMSAVEBEFOREMAPPING");
				}

                DataTable dt = (grdMapSave.DataSource as DataTable);
                DataTable dt2 = (grdUOMDefinitionList.DataSource as DataTable).Copy();
                DataRow dr = grdUOMDefinitionList.View.GetFocusedDataRow();
                DataRow dr2 = null;
                if(dr==null)
                {
                    return;
                }


                foreach (DataRow row in dt2.Rows)
                {   
                    if(dr["UOMDEFID"] == row["UOMDEFID"] && dr["UOMCLASSID"] == row["UOMCLASSID"])
                    {
                        dr2 = row;
                    }
                }

                dt2.Rows.Remove(dr2);
                dr["ISADD"] = "Down";
                dt.ImportRow(dr);
                grdUOMDefinitionList.DataSource = dt2;
            }
            else if (btnState.Equals("Up"))
            {


                DataTable dt = (grdMapSave.DataSource as DataTable).Copy();
                DataTable dt2 = (grdUOMDefinitionList.DataSource as DataTable);
                DataRow dr = grdMapSave.View.GetFocusedDataRow();
                DataRow dr2 = null;
                if (dr == null)
                {
                    return;
                }
                foreach (DataRow row in dt.Rows)
                {
                    if (dr["UOMDEFID"] == row["UOMDEFID"] && dr["UOMCLASSID"] == row["UOMCLASSID"])
                    {
                        dr2 = row;
                    }
                }

                dt.Rows.Remove(dr2);
                dr["ISADD"] = "Up";
                dt2.ImportRow(dr);
                grdMapSave.DataSource = dt;
            }

        }

        /// <summary>
        /// UOM Definitoin 그리드에서 행 추가 시 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
		{
			DataRow row = grdUOMClassList.View.GetFocusedDataRow();
			args.NewRow["UOMCLASSID"] = row["UOMCLASSID"].ToString();

		}

        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            if (tabPartition.SelectedTabPage == xtraTabPage1) //공정 CAPA 관리 탭 선택시
            { 
                DataTable dtuomClass = await SqlExecuter.QueryAsync("SelectUomClassList", "10001", values);

                if (dtuomClass.Rows.Count < 1)
                {
                    ShowMessage("NoSelectData");
                }
                grdMainUOMClassList.DataSource = dtuomClass;
            }
			else
			{
				LoadDataGridUOMClass();
				View_UomDefRowChanged();
				View_MAP_RowView();
			}
        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
		{
			base.OnToolbarSaveClick();

			// TODO : 저장 Rule 변경

			DataTable changed = new DataTable();
			switch (tabPartition.SelectedTabPageIndex)
			{
				case 0:
					changed = grdMainUOMClassList.GetChangedRows();
					ExecuteRule("UomClass", changed);
					break;
				case 1:
					changed = grdUOMDefinitionList.GetChangedRows();
					DataTable changedMapList = grdMapSave.GetChangedRows();
					DataTable changedMapclass = grdMapList.GetChangedRows();
					//List<string> stateList = changed.AsEnumerable().Select(r => Format.GetString(r["ISADD"])).Distinct().ToList();

					ExecuteRule("UomDefinition", changed);

					DataRow dr = grdMapList.View.GetFocusedDataRow();
					DataTable uommap = (grdMapSave.DataSource as DataTable);
					DataTable uommapclone = (grdMapSave.DataSource as DataTable).Clone();
					DataTable uomdef = (grdUOMDefinitionList.DataSource as DataTable);
					DataTable uomdefclone = (grdUOMDefinitionList.DataSource as DataTable).Clone();

					foreach (DataRow row in uomdef.Rows)
					{
						if (row["ISADD"].Equals("Up"))
						{
							uomdefclone.ImportRow(row);
						}
					}

					foreach (DataRow row in uommap.Rows)
					{
						if (row["ISADD"].Equals("Down"))
						{
							uommapclone.ImportRow(row);
						}
					}
					string maptable = "mapInfo";
					string deftable = "defInfo";
					uommapclone.TableName = maptable;
					uomdefclone.TableName = deftable;

					if (uommapclone == null && uomdefclone == null || dr == null) // 
					{
						throw MessageException.Create("NoSaveData"); // 저장할 데이터가 없습니다.
					}

					String uomcategoryid = dr["UOMCATEGORY"].ToString();

					MessageWorker messageWorker = new MessageWorker("UomMap");
					messageWorker.SetBody(new MessageBody()
					{
						{ "EnterpriseId", UserInfo.Current.Enterprise }
						, { "PlantId", UserInfo.Current.Plant }
						, { "UOMCATEGORY", uomcategoryid    }
						, { "Validstate", "Valid" } // 고정값
						, { maptable, uommapclone }
						, { deftable, uomdefclone}
					});
					messageWorker.Execute();

					FocusedRowChanged();
					LoadDataGridUOMClass();
					View_UomDefRowChanged();
					View_MAP_RowView();


					break;
			}
		}

        #endregion

        #region 검색

		/// <summary>
		/// 조회조건 추가 구성
		/// </summary>
		protected override void InitializeCondition()
		{
			base.InitializeCondition();
		}

		/// <summary>
		/// 조회조건 컨트롤에 기능 추가
		/// </summary>
		protected override void InitializeConditionControls()
		{
			base.InitializeConditionControls();

			// TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
		}

		#endregion

		#region 유효성 검사

		/// <summary>
		/// 데이터 저장할때 컨텐츠 영역의 유효성 검사
		/// </summary>
		protected override void OnValidateContent()
		{
			base.OnValidateContent();

            // TODO : 유효성 로직 변경

            switch (tabPartition.SelectedTabPageIndex)
			{
				case 0: //UOM Class
					grdUOMClassList.View.CheckValidation();
					DataTable changedUOMClass = grdMainUOMClassList.GetChangedRows();
					if (changedUOMClass.Rows.Count < 1)
					{
						//저장할 데이터가 존재하지 않습니다.
						throw MessageException.Create("NoSaveData");
					}

					foreach (DataRow row in changedUOMClass.Rows)
					{
						Regex regex = new Regex(@"^[a-zA-Z0-9]+$"); //영문 + 숫자 조합만 입력가능 정규식

						string uomClassId = row["UOMCLASSID"].ToString(); //영문 + 숫자 조합만 입력가능
						if (!regex.IsMatch(uomClassId))
						{
							throw MessageException.Create("UomClassIdInputError", Language.Get("UOMCLASSID"));
						}

					}//foreach
					break;
				case 1: //UOM Definition
					grdUOMDefinitionList.View.CheckValidation();

					DataTable changedUOMDefinition = grdUOMDefinitionList.GetChangedRows();
					DataTable changedMapList = grdMapSave.GetChangedRows();
					DataTable changedMapclass = grdMapList.GetChangedRows();

					if (changedUOMDefinition.Rows.Count < 1 && changedMapList.Rows.Count < 1 && changedMapclass.Rows.Count < 1)
					{
						//저장할 데이터가 존재하지 않습니다.
						throw MessageException.Create("NoSaveData");
					}

					List<string> stateList = changedUOMDefinition.AsEnumerable().Select(r => Format.GetString(r["ISADD"])).Distinct().ToList();
					if(!stateList.Contains("Up") && !stateList.Contains("Down"))
					{
						foreach (DataRow row in changedUOMDefinition.Rows)
						{
							Regex regex2 = new Regex(@"[a-zA-Z]+$"); //영문 Text만 입력가능 정규식

							string uomDefId = row["UOMDEFID"].ToString(); //영문 Text만 입력가능
							string uomDefName = row["UOMDEFNAME"].ToString(); //영문 Text만 입력가능

                            //if (!regex2.IsMatch(uomDefId) || !regex2.IsMatch(uomDefName))
                            if (!regex2.IsMatch(uomDefId))
                            {
								//메시지 처리
								throw MessageException.Create("UomDefIdAndNameInputError", Language.Get("UOMDEFID"), Language.Get("UOMDEFNAME"));
							}

						}//foreach
					}
					break;
			}//switch

		}//protected

		#endregion

		#region Private Function

		// TODO : 화면에서 사용할 내부 함수 추가
		/// <summary>
		/// Focused된 UOM 그룹에 해당하는 UOM Def 목록 조회
		/// </summary>
		private void FocusedRowChanged()
		{
            //int page = smartTabControl1.SelectedTabPageIndex;
            //if (page == 0)
            //{
                View_UomDefRowChanged();
            //}
            //else
            //{
            //    DataRow uomclassrow = grdUOMClassList.View.GetFocusedDataRow();

            //    Dictionary<string, object> classparam = new Dictionary<string, object>();
            //    classparam.Add("p_uomclassid", uomclassrow["UOMCLASSID"].ToString());
            //    classparam.Add("p_validstate", "Valid");

            //    DataTable dtUomDefRefreshList = SqlExecuter.Query("SelectUomDefList", "10003", classparam);//Procedure("usp_com_selectuomdefinition", param);
            //    DataRow row = grdMapList.View.GetFocusedDataRow();

            //    if (row == null)
            //    {
            //        return;
            //    }

            //    grdUOMDefinitionList.DataSource = dtUomDefRefreshList;
             
            //}
        
        }

		/// <summary>
		/// UOM Class 전체 조회(Query)
		/// </summary>
		private void LoadDataGridUOMClass()
		{
			var values = Conditions.GetValues();

			Dictionary<string, object> param = new Dictionary<string, object>();
			param.Add("UomClassId", "*");

			DataTable dt = SqlExecuter.Query("GetUomClassList", "10001", param);

            param.Add("EnterpriseId", UserInfo.Current.Enterprise);
            param.Add("PlantId", UserInfo.Current.Plant);
            param.Add("LanguageType", UserInfo.Current.LanguageType);
            DataTable dtUOMMap = SqlExecuter.Query("SelectUomMapList", "10001", param);

            if (dt.Rows.Count < 1 && dtUOMMap.Rows.Count<1) // 
			{
				ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
			}

			grdUOMClassList.DataSource = dt;
            grdMapList.DataSource = dtUOMMap;
        }
        #endregion

        /// <summary>
        /// PK 수정 방지
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DataRow row = null;
            int index = tabPartition.SelectedTabPageIndex;
            switch (index)
            {
                case 1://MAP LIST
                    row = grdMapList.View.GetFocusedDataRow();
                    if (row.RowState != DataRowState.Added)
                    {
                        GridView view = sender as GridView;

                        if (view.FocusedColumn.FieldName.Equals("UOMCATEGORY"))
                        {
                            e.Cancel = true;
                        }
               
                    }

                    break;

            }
        }

        /// <summary>
        ///  UOM DEF 그리드의 UOMDEFID 와 UOM MAP 그리드의 UOMDEFID가 겹치지 않게 조회해주는 함수 
        /// </summary>
        private void View_UomDefRowChanged()
        {

            DataRow uomclassrow = grdUOMClassList.View.GetFocusedDataRow();

            Dictionary<string, object> classparam = new Dictionary<string, object>();
            classparam.Add("p_uomclassid", uomclassrow["UOMCLASSID"].ToString());
            classparam.Add("p_validstate", "Valid");

            DataTable dtUomDefRefreshList = SqlExecuter.Query("SelectUomDefList", "10003", classparam);//Procedure("usp_com_selectuomdefinition", param);
            DataRow row = grdMapList.View.GetFocusedDataRow();

            if(row==null)
            {
                return;          
             }

            grdUOMDefinitionList.DataSource = dtUomDefRefreshList;
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("UOMCATEGORY", row["UOMCATEGORY"].ToString());
            DataTable dtMapSave = SqlExecuter.Query("SelectUomDefList", "10002", param);

            DataTable dtDefList = grdUOMDefinitionList.DataSource as DataTable;
            DataTable dtDefListClone = dtDefList.Clone();

            foreach (DataRow eachDef in dtDefList.Rows)
            {
                bool found = false;
                foreach (DataRow eachMap in dtMapSave.Rows)
                {
                    if (eachMap["UOMDEFID"].ToString() == eachDef["UOMDEFID"].ToString() && eachMap["UOMCLASSID"].ToString() == eachDef["UOMCLASSID"].ToString())
                    {
                        found = true;
                        break;
                    }
                }
                if(!found)
                {
                    dtDefListClone.ImportRow(eachDef);
                }
            }
            grdUOMDefinitionList.DataSource = dtDefListClone;

        }

        /// <summary>
        /// UOM MAP 그리드에서 해당 UOMCATEGORY에 맞는  UOM MAP 그리드를 UOM MAP SAVE 그리드에 조회
        /// </summary>
        private void View_MAP_RowView()
        {
            DataRow row = grdMapList.View.GetFocusedDataRow();
			if(row == null) return;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("UOMCATEGORY", row["UOMCATEGORY"].ToString());
            DataTable dt = SqlExecuter.Query("SelectUomDefList", "10002", param);

            grdMapSave.DataSource = dt;

        }


        /// <summary>
        /// UOM CALSS 그리드에서 해당 UOMCLASSID에 맞는 그리드를 UOM DEF 그리드에 조회
        /// </summary>
        private void View_ClassRowClick() 
        {
            DataRow row = grdUOMClassList.View.GetFocusedDataRow();

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("p_uomclassid", row["UOMCLASSID"].ToString());
            param.Add("p_validstate", "Valid");

            DataTable dt = SqlExecuter.Query("SelectUomDefList", "10003", param);//Procedure("usp_com_selectuomdefinition", param);
            grdUOMDefinitionList.DataSource = dt;
            
        }
        /// <summary>
        /// UOM 리스트에서 row 클릭 시 UOM Conversion 조회 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void View_ConvRowChanged()
        //{
       
        //    DataRow selectedRow = grdUOMDefinitionList.View.GetFocusedDataRow();

        //    string uomdefid = selectedRow["UOMDEFID"].ToString();

        //    Dictionary<string, object> param = new Dictionary<string, object>();
        //    param.Add("EnterpriseId", UserInfo.Current.Enterprise);
        //    param.Add("PlantId", UserInfo.Current.Plant);
        //    param.Add("LanguageType", UserInfo.Current.LanguageType);
        //    param.Add("Uomdefid", uomdefid);

        //    //LOT Conversion list 조회
        //    DataTable dtUOMConverList = SqlExecuter.Query("SelectUomConList", "10001", param);
        //    grdUOMConversion.DataSource = dtUOMConverList;

        //}

        private void smartBandedGrid2_Load(object sender, EventArgs e)
        {

        }

        private void smartBandedGrid1_Load(object sender, EventArgs e)
        {

        }
    }
}
