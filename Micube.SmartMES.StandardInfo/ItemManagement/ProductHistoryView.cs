#region using

using DevExpress.XtraTreeList.Nodes;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Validations;
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
    /// 프 로 그 램 명  : 기준정보 > 품목관리 > 품목이력조회
    /// 업  무  설  명  : 품목 이력을 조회 한다.
    /// 생    성    자  : 장선미
    /// 생    성    일  : 2019-12-16
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ProductHistoryView : SmartConditionManualBaseForm
    {
        #region 생성자

        public ProductHistoryView()
        {
            InitializeComponent();
        }
        #endregion

        #region 컨텐츠 영역 초기화
        protected override void InitializeContent()
        {
            base.InitializeContent();
            InitializeGrid();
            InitializeEvent();
        }

        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            this.grdProductHist.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            this.grdProductHist.GridButtonItem = GridButtonItem.Export;
            this.grdProductHist.View.SetIsReadOnly();

            //this.grdProductHist.View.AddTextBoxColumn("PLANTID", 60)
            //    .SetTextAlignment(TextAlignment.Center);

            this.grdProductHist.View.AddTextBoxColumn("PRODUCTDEFID", 100)
                .SetIsHidden();

            this.grdProductHist.View.AddTextBoxColumn("ITEMID", 80)
                .SetTextAlignment(TextAlignment.Center);

            this.grdProductHist.View.AddTextBoxColumn("CUSTOMERREV", 50)
                .SetTextAlignment(TextAlignment.Center);

            this.grdProductHist.View.AddTextBoxColumn("PRODUCTDEFVERSION", 50)
               .SetTextAlignment(TextAlignment.Center);

            this.grdProductHist.View.AddTextBoxColumn("PRODUCTNAME", 200)
                .SetLabel("PRODUCTDEFNAME");


            this.grdProductHist.View.AddTextBoxColumn("SPECOWNERNAME", 70)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("OWNER");

            this.grdProductHist.View.AddTextBoxColumn("CREATEDTIME", 80)
                .SetDisplayFormat("yyyy-MM-dd")
               .SetTextAlignment(TextAlignment.Center);

            this.grdProductHist.View.AddTextBoxColumn("GOVERNANCENO", 100)
                .SetIsHidden();
            this.grdProductHist.View.AddTextBoxColumn("RCPRODUCTDEFID", 100)
                .SetIsHidden();
            this.grdProductHist.View.AddTextBoxColumn("RCPRODUCTDEFVERSION", 100)
                .SetIsHidden();

            this.grdProductHist.View.AddTextBoxColumn("ISRC", 70)
                .SetTextAlignment(TextAlignment.Center);
                

            this.grdProductHist.View.AddTextBoxColumn("ISPCN", 70)
                .SetTextAlignment(TextAlignment.Center);

            this.grdProductHist.View.AddTextBoxColumn("PCNREQUESTDATE", 100)
                .SetIsHidden();


            this.grdProductHist.View.AddTextBoxColumn("CHANGECOMMENT", 300)
                .SetLabel("SPECCHANGE"); 
            this.grdProductHist.View.AddTextBoxColumn("CHANGENOTE", 300)
                .SetLabel("COMMENT");
            this.grdProductHist.View.AddTextBoxColumn("CHANGEPOINTNO", 100)
                .SetIsHidden();

            this.grdProductHist.View.PopulateColumns();

            this.grdProductHist.View.OptionsView.RowAutoHeight = false;

            DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit memoEdit = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            memoEdit.AutoHeight = false;
            memoEdit.WordWrap = true;


            this.grdProductHist.View.Columns["CHANGECOMMENT"].ColumnEdit = memoEdit;            
            this.grdProductHist.View.Columns["CHANGENOTE"].ColumnEdit = memoEdit;

            this.grdProductHist.View.RowHeight = 60;
            //this.grdProductHist.View.Columns["CHANGECOMMENT"].BestFit();
            //this.grdProductHist.View.Columns["CHANGENOTE"].BestFit();
        }

        #endregion

        #region 조회조건 초기화

        /// <summary>
        /// 조회 조건 초기화
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            //품목코드
            InitializeCondition_ProductPopup();

            //고객Rev
            Conditions.AddTextBox("P_CUSTOMERREV")
                .SetPosition(4)
                .SetLabel("CUSTOMERREV");

            ////내부Rev
            //Conditions.AddTextBox("P_PRODUCTDEFVERSION")
            //    .SetPosition(5)
            //    .SetLabel("INNERREVISION");
            
            //사양담당
            InitializeCondition_Customer();
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            //다국어 변경
            SmartSelectPopupEdit specOwner = Conditions.GetControl<SmartSelectPopupEdit>("P_SPECOWNER");
            specOwner.LanguageKey = "SPECOWNER";
            specOwner.LabelText = Language.Get(specOwner.LanguageKey);

            SmartPeriodEdit period = Conditions.GetControl<SmartPeriodEdit>("P_PERIOD");
            period.LanguageKey = "CREATEPERIOD";
            period.LabelText = Language.Get(specOwner.LanguageKey);

			//3개월 설정
			DateTime toDate = Convert.ToDateTime(period.datePeriodTo.EditValue);
			period.datePeriodFr.EditValue = toDate.AddMonths(-3);
		}

        /// <summary>
        /// 팝업형 조회조건 생성 - 품목
        /// </summary>
        private void InitializeCondition_ProductPopup()
        {
            // SelectPopup 항목 추가
            var conditionProductId = Conditions.AddSelectPopup("P_ITEMID"
                                                , new SqlQuery("GetProductDefList", "10005", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "PRODUCTCLASSID=Product")
                                                , "ITEMID", "ITEMID")
                .SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupLayoutForm(800, 800)
                .SetPopupAutoFillColumns("ITEMNAME")
                .SetLabel("ITEMID")
                .SetPosition(3)
                .SetPopupResultCount(0);

            // 팝업에서 사용되는 검색조건 (품목코드/명)
            conditionProductId.Conditions.AddTextBox("ITEMID").SetLabel("ITEMID");
            conditionProductId.Conditions.AddTextBox("ITEMNAME").SetLabel("ITEMNAME");


            // 품목코드
            conditionProductId.GridColumns.AddTextBoxColumn("ITEMID", 150);
            // 품목명
            conditionProductId.GridColumns.AddTextBoxColumn("ITEMNAME", 200);
        }

        /// <summary>
        /// 팝업형 조회조건 생성 - 사양담당
        /// </summary>
        private void InitializeCondition_Customer()
        {
            var conditionCustomer = Conditions.AddSelectPopup("P_SPECOWNER", new SqlQuery("GetUserList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "USERNAME", "USERID")
                .SetPopupLayout("SELECTSPECOWNER", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupLayoutForm(800, 800)
                .SetPopupAutoFillColumns("DEPARTMENT")
                .SetLabel("USERID")
                .SetPosition(6)
                .SetPopupResultCount(0);

            // 팝업 조회조건
            conditionCustomer.Conditions.AddTextBox("USERIDNAME");

            // 팝업 그리드
            conditionCustomer.GridColumns.AddTextBoxColumn("USERID", 150);
            conditionCustomer.GridColumns.AddTextBoxColumn("USERNAME", 200);
            conditionCustomer.GridColumns.AddTextBoxColumn("DEPARTMENT", 200);
        }
        #endregion


        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            this.grdProductHist.View.DoubleClick += View_DoubleClick;
            this.grdProductHist.View.CustomRowCellEdit += View_CustomRowCellEdit;    
        }

        /// <summary>
        /// ISRC / ISPCN 컬럼 Y일 때 하이퍼링크로 표시 요청
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName.Equals("ISRC") || e.Column.FieldName.Equals("ISPCN"))
            {
                if (e.CellValue.ToString().Equals("Y"))
                {
                    DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit editHyper = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
                    editHyper.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                    e.RepositoryItem = editHyper;
                }
                else
                {
                    DevExpress.XtraEditors.Repository.RepositoryItemTextEdit editText = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
                    editText.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                    e.RepositoryItem = editText;
                }
            }
        }


        /// <summary>
        /// RC여부 / PCN 여부 더블 클릭시 메뉴 이동
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_DoubleClick(object sender, EventArgs e)
        {
            if (this.grdProductHist.View.FocusedRowHandle < 0)
                return;

            if (this.grdProductHist.View.FocusedValue.ToString().Equals("Y") == false)
                return;

            string strFocusColumnName = this.grdProductHist.View.FocusedColumn.FieldName;

            Dictionary<string, object> param = new Dictionary<string, object>();

            switch (strFocusColumnName)
            {
                case "ISRC":

                    param.Clear();
                    param.Add("P_GOVERNANCENO", this.grdProductHist.View.GetFocusedRowCellValue("GOVERNANCENO"));
                    param.Add("PRODUCTDEFID", this.grdProductHist.View.GetFocusedRowCellValue("RCPRODUCTDEFID"));
                    param.Add("PRODUCTDEFVERSION", this.grdProductHist.View.GetFocusedRowCellValue("RCPRODUCTDEFVERSION"));
                    param.Add("RCPRODUCTDEFID", this.grdProductHist.View.GetFocusedRowCellValue("PRODUCTDEFID"));
                    param.Add("RCPRODUCTDEFVERSION", this.grdProductHist.View.GetFocusedRowCellValue("PRODUCTDEFVERSION"));
                    param.Add("CALLMENU", "ProductHistoryView");

					//RunningChange 등록
					try
					{
						DialogManager.ShowWaitDialog();
						this.OpenMenu("PG-SD-0260", param);
					}catch (Exception ex)
					{
						ShowMessage(ex.ToString());
					}
					finally {
						DialogManager.Close();
					}
					
					break;

                case "ISPCN":
                    param.Clear();
                    param.Add("PRODUCTDEFID", this.grdProductHist.View.GetFocusedRowCellValue("PRODUCTDEFID"));
                    param.Add("PRODUCTDEFVERSION", this.grdProductHist.View.GetFocusedRowCellValue("PRODUCTDEFVERSION"));
                    param.Add("PRODUCTNAME", this.grdProductHist.View.GetFocusedRowCellValue("PRODUCTNAME"));
                    param.Add("PCNREQUESTDATE", this.grdProductHist.View.GetFocusedRowCellValue("PCNREQUESTDATE"));
                    param.Add("CHANGEPOINTNO", this.grdProductHist.View.GetFocusedRowCellValue("CHANGEPOINTNO"));
                    param.Add("CALLMENU", "ProductHistoryView");

					//변경점 신청서 등록/이력조회
					try
					{
						DialogManager.ShowWaitDialog();
						this.OpenMenu("PG-QC-0350", param);
					}
					catch (Exception ex)
					{
						ShowMessage(ex.ToString());
					}
					finally
					{
						DialogManager.Close();
					}

                    break;

                default:
                    break;
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
        }
        #endregion

        #region 검색

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            var values = Conditions.GetValues();

            //UserEnterprise 설정안되어있어서 임의로 지정 나중에 삭제 임시
            if (UserInfo.Current.Enterprise.Equals(""))
            {
                values.Add("p_enterpriseid", "INTERFLEX");//임시
            }
            else
            {
                values.Add("p_enterpriseid", UserInfo.Current.Enterprise);
            }

            values.Add("p_languagetype", UserInfo.Current.LanguageType);

            DataTable dt = await SqlExecuter.QueryAsync("SelectProductHistoryView", "10001", values);
            grdProductHist.DataSource = dt;

            // 사양변경내용, 특이사항은 컬럼 너비 자동 으로....
            //this.grdProductHist.View.Columns["CHANGECOMMENT"].BestFit();
            //this.grdProductHist.View.Columns["CHANGENOTE"].BestFit();

            this.grdProductHist.GridButtonItem = GridButtonItem.Export;

        }

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();
        }

        #endregion

        #region Private Function
        

        #endregion
    }
}
