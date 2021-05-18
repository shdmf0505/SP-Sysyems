#region using
using DevExpress.Utils.Menu;
using DevExpress.XtraGrid.Menu;
using DevExpress.XtraGrid.Views.Grid;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons;
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

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 이상발생 현황
    /// 업  무  설  명  : 이상발생 현황
    /// 생    성    자  : 박정훈
    /// 생    성    일  : 2019-09-16
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class AbnormalOccurence : SmartConditionManualBaseForm
    {
		#region ◆ Local Variables |
		//퀵 메뉴 리스트
		List<DXMenuItem> menuList = new List<DXMenuItem>();
		#endregion

		#region ◆ 생성자 |
		/// <summary>
		/// 생성자
		/// </summary>
		public AbnormalOccurence()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Form Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Form_Load(object sender, EventArgs e)
        {
            InitializeComboBox();
            InitializeGrid();
			InitializeQuickMenuList();

		}
        public override void LoadForm(Dictionary<string, object> parameters)
        {
            base.LoadForm(parameters);

            if (parameters == null) return;

            Conditions.GetControl<SmartSelectPopupEdit>("P_LOTID").SetValue(Format.GetFullTrimString(parameters["LOTID"]));
            OnSearchAsync();

        }
        #endregion

        #region ◆ 컨텐츠 영역 초기화 |
        /// <summary>
        /// Control 설정
        /// </summary>
        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeEvent();
        }

        #region ▶ 조회조건 설정 |
        /// <summary>
        /// 검색조건 설정
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // Lot
            CommonFunction.AddConditionLotPopup("P_LOTID", 2.1, true, Conditions);
			// 품목
			//CommonFunction.AddConditionProductPopup("P_PRODUCTDEFID", 0.5, true, Conditions);
			InitializeCondition_ProductPopup();
			//발생작업장
			CommonFunction.AddConditionAreaPopup("P_OCCURAREAID", 2.2, true, Conditions);
			//중지작업장
			CommonFunction.AddConditionAreaPopup("P_STOPAREAID", 2.3, true, Conditions);
		}

		/// <summary>
		/// 팝업형 조회조건 생성 - 품목
		/// </summary>
		private void InitializeCondition_ProductPopup()
		{
			// SelectPopup 항목 추가
			var conditionProductId = Conditions.AddSelectPopup("P_PRODUCTDEFID", new SqlQuery("GetProductDefinitionList", "10002", $"PLANTID={UserInfo.Current.Plant}"), "PRODUCTDEF", "PRODUCTDEF")
				.SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, true)
				.SetPopupLayoutForm(800, 800)
				.SetPopupAutoFillColumns("PRODUCTDEFNAME")
				.SetLabel("PRODUCTDEFID")
				.SetPosition(1.2)
				.SetPopupResultCount(0)
				.SetPopupApplySelection((selectRow, gridRow) => {
					string productDefName = "";

					selectRow.AsEnumerable().ForEach(r => {
						productDefName += Format.GetString(r["PRODUCTDEFNAME"]) + ",";
					});

					Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").EditValue = productDefName;
				});

			// 팝업에서 사용되는 검색조건 (품목코드/명)
			conditionProductId.Conditions.AddTextBox("TXTPRODUCTDEFNAME");
			//제품 구분 추가 2019.08.14 배선용, 노석안 대리 요청
			conditionProductId.Conditions.AddComboBox("PRODUCTDIVISION", new SqlQuery("GetTypeList", "10001", $"CODECLASSID=ProductDivision2", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
				.SetDefault("Product");

			// 품목코드
			conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
			// 품목명
			conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);
			// 품목버전
			conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 100);
			// 품목유형구분
			conditionProductId.GridColumns.AddComboBoxColumn("PRODUCTDEFTYPE", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductDefType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
			// 생산구분
			conditionProductId.GridColumns.AddComboBoxColumn("PRODUCTIONTYPE", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
			// 단위
			conditionProductId.GridColumns.AddComboBoxColumn("UNIT", 90, new SqlQuery("GetUomDefinitionList", "10001", "UOMCLASSID=Segment"), "UOMDEFNAME", "UOMDEFID");
		}

		/// <summary>
		/// 조회조건 설정
		/// </summary>
		protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

			SmartSelectPopupEdit popOccurArea = Conditions.GetControl<SmartSelectPopupEdit>("P_OCCURAREAID");
			popOccurArea.LanguageKey = "OCCURAREA";

			SmartSelectPopupEdit popStopArea = Conditions.GetControl<SmartSelectPopupEdit>("P_STOPAREAID");
			popStopArea.LanguageKey = "STOPAREA";
		} 
        #endregion

        #region ▶ ComboBox 초기화 |
        /// <summary>
        /// 콤보박스를 초기화 하는 함수
        /// </summary>
        private void InitializeComboBox()
        {
            
        }
        #endregion

        #region ▶ Grid 설정 |
        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            #region - 이상발생 Grid 설정 |
            grdMain.GridButtonItem = GridButtonItem.Export;

			grdMain.SetIsUseContextMenu(true);
            grdMain.View.SetIsReadOnly();
			grdMain.SetIsUseContextMenu(false);

            var grpDefaultCol = grdMain.View.AddGroupColumn("ABNORMALOCCURRENCE"); // 이상발생
            grpDefaultCol.AddTextBoxColumn("ABNORMALTYPE", 80).SetTextAlignment(TextAlignment.Center); 
            grpDefaultCol.AddTextBoxColumn("LOTPROCESS", 80).SetTextAlignment(TextAlignment.Center);
            grpDefaultCol.AddTextBoxColumn("STATUS", 80).SetTextAlignment(TextAlignment.Center);
            grpDefaultCol.AddTextBoxColumn("PRODUCTDEFID", 120).SetTextAlignment(TextAlignment.Center);
            grpDefaultCol.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            grpDefaultCol.AddTextBoxColumn("LOTID", 170).SetTextAlignment(TextAlignment.Center);

            var grpSegmentCol = grdMain.View.AddGroupColumn("OCCURESEGMENT"); // 발생 공정
            grpSegmentCol.AddTextBoxColumn("JOBTYPE", 60).SetTextAlignment(TextAlignment.Center);
            grpSegmentCol.AddTextBoxColumn("USERSEQUENCE", 70).SetTextAlignment(TextAlignment.Center);
            grpSegmentCol.AddTextBoxColumn("PROCESSSEGMENTNAME", 120).SetTextAlignment(TextAlignment.Center);
            grpSegmentCol.AddTextBoxColumn("SITE", 60).SetTextAlignment(TextAlignment.Center);
            grpSegmentCol.AddTextBoxColumn("AREANAME", 120);

            var grpQTYCol = grdMain.View.AddGroupColumn("QTY"); // 수량
            grpQTYCol.AddSpinEditColumn("PCS", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric, true);
            grpQTYCol.AddSpinEditColumn("PNL", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true);

			var grpAbnormalHistCol = grdMain.View.AddGroupColumn("ABNORMALHIST"); // 이상발생내역
            grpAbnormalHistCol.AddTextBoxColumn("OCCURDATE", 120).SetTextAlignment(TextAlignment.Center);
			grpAbnormalHistCol.AddTextBoxColumn("REASONCODECLASS", 100).SetTextAlignment(TextAlignment.Center);
			grpAbnormalHistCol.AddTextBoxColumn("REASONCODE", 150).SetTextAlignment(TextAlignment.Center);
            grpAbnormalHistCol.AddTextBoxColumn("CREATEUSER", 80).SetTextAlignment(TextAlignment.Center);
            grpAbnormalHistCol.AddTextBoxColumn("REMARK", 150);

            var grpStopHistCol = grdMain.View.AddGroupColumn("STOPHIST"); // 중지이력
            grpStopHistCol.AddTextBoxColumn("STOPUSERSEQUENCE", 70).SetTextAlignment(TextAlignment.Center);
            grpStopHistCol.AddTextBoxColumn("STOPPROCESSSEGMENTNAME", 120).SetTextAlignment(TextAlignment.Center);
            grpStopHistCol.AddTextBoxColumn("STOPSITE", 60).SetTextAlignment(TextAlignment.Center);
			grpStopHistCol.AddTextBoxColumn("STOPAREA", 120).SetTextAlignment(TextAlignment.Center);
			grpStopHistCol.AddTextBoxColumn("STOPDATE", 120).SetTextAlignment(TextAlignment.Center);
            grpStopHistCol.AddTextBoxColumn("STOPRELEASEDATE", 120).SetTextAlignment(TextAlignment.Center);
            grpStopHistCol.AddSpinEditColumn("STAYINGTIME", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true);

			var grpLotCol = grdMain.View.AddGroupColumn("LOTINFO"); // LOT정보
            grpLotCol.AddTextBoxColumn("LOTINPUTDATE", 120).SetTextAlignment(TextAlignment.Center);
            grpLotCol.AddSpinEditColumn("CUM_LEADTIME", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true);
			grpLotCol.AddTextBoxColumn("LOTPRODUCTTYPE", 60).SetTextAlignment(TextAlignment.Center);

            grdMain.View.PopulateColumns();

            #endregion
        }
		#endregion

		#region ▶ 퀵 메뉴 |
		/// <summary>
		/// 퀵 메뉴 리스트 등록
		/// </summary>
		private void InitializeQuickMenuList()
		{
			menuList.Add(new DXMenuItem(Language.Get("MENU_PG-SG-0340"), OpenForm) { BeginGroup = true, Tag = "PG-SG-0340" });
			menuList.Add(new DXMenuItem(Language.Get("MENU_PG-SG-0460"), OpenForm) { Tag = "PG-SG-0460" });
			menuList.Add(new DXMenuItem(Language.Get("MENU_PG-SG-0490"), OpenForm) { Tag = "PG-SG-0490" });
			menuList.Add(new DXMenuItem(Language.Get("MENU_CHEMICALISSUE"), OpenForm) { Tag = "Chemicalissue" });
			menuList.Add(new DXMenuItem(Language.Get("MENU_RELIAVERIFIRESULTREGULAR"), OpenForm) { Tag = "ReliaVerifiResultRegular" });
		}
		#endregion

		#endregion

		#region ◆ Event |

		/// <summary>        
		/// 이벤트 초기화
		/// </summary>
		public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            this.Load += Form_Load;

			grdMain.InitContextMenuEvent += GrdMain_InitContextMenuEvent;
			grdMain.View.RowCellStyle += View_RowCellStyle;
            // ComboBox Event
        }

		/// <summary>
		/// Context Menu
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		private void GrdMain_InitContextMenuEvent(SmartBandedGrid sender, Framework.SmartControls.Grid.InitContextMenuEventArgs args)
		{
			for(int i = 0; i < menuList.Count; i++)
			{
				args.AddMenus.Add(menuList[i]);
			}
		}

		/// <summary>
		/// Customizing Context Menu Item Click 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		private void OpenForm(object sender, EventArgs args)
		{
			try
			{
				DialogManager.ShowWaitDialog();

				DataRow currentRow = grdMain.View.GetFocusedDataRow();

				string menuId = (sender as DXMenuItem).Tag.ToString();

				var param = currentRow.Table.Columns
					.Cast<DataColumn>()
					.ToDictionary(col => col.ColumnName, col => currentRow[col.ColumnName]);

				OpenMenu(menuId, param); //다른창 호출..
			}
			catch (Exception ex)
			{
				this.ShowError(ex);
			}
			finally
			{
				DialogManager.Close();
			}
		}

		/// <summary>
		/// Cell Style
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void View_RowCellStyle(object sender, RowCellStyleEventArgs e)
		{
			if(e.Column.FieldName == "STATUS")
			{ 
				if(Format.GetString(grdMain.View.GetRowCellValue(e.RowHandle, "STATE")) == "Stop")
				{
					e.Appearance.BackColor = Color.LightCoral;
				}
				if (Format.GetString(grdMain.View.GetRowCellValue(e.RowHandle, "STATE")) == "Complete")
				{
					e.Appearance.BackColor = Color.LightSkyBlue;
				}
			}
		}

		#region ▶ ComboBox Event |

		#endregion

		#endregion

		#region ◆ 툴바 |

		/// <summary>
		/// 저장버튼 클릭
		/// </summary>
		protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            // TODO : 저장 Rule 변경
            /*
            DataTable lockList = grdHold.DataSource as DataTable;

            if (lockList == null || lockList.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }

            // 대분류선택 체크
            if(this.cboTopClass.EditValue == null || string.IsNullOrWhiteSpace(this.cboTopClass.EditValue.ToString()))
            {
                // 대분류선택은 필수 선택입니다.
                throw MessageException.Create("NoExistTopClass");
            }


            // 사유코드선택 체크
            if (this.cboReason.EditValue == null || string.IsNullOrWhiteSpace(this.cboReason.EditValue.ToString()))
            {
                // 사유코드 선택은 필수 선택입니다.
                throw MessageException.Create("NoExistReasonCode");
            }

            // Locking 분류
            string holdTopClass = (string)cboTopClass.GetValue();
            //string holdMiddleClass = (string)cboMiddleClass.GetValue();
            string holdkReason = (string)cboReason.GetValue();

            MessageWorker worker = new MessageWorker("SaveLotHold");
            worker.SetBody(new MessageBody()
            {
                { "TransactionType", "SetLotHold" },
                { "EnterpriseID", UserInfo.Current.Enterprise },
                { "PlantID", UserInfo.Current.Plant },
                { "HoldTopClass", holdTopClass },
                //{ "HoldMiddleClass", holdMiddleClass },
                { "ReasonCode", holdkReason },
                { "Comments", txtComment.Text },
                { "UserId", UserInfo.Current.Id },
                { "Lotlist", lockList }
            });

            worker.Execute();

            // Data 초기화
            this.cboTopClass.EditValue = null;
            //this.cboMiddleClass.EditValue = null;
            this.cboReason.EditValue = null;
            this.txtComment.Text = string.Empty;
            */
        }

        #endregion

        #region ◆ 검색 |

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            // 기존 Grid Data 초기화

            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable dt = await SqlExecuter.QueryAsync("SelectAbnormalOccurenceList", "10001", values);

            if (dt.Rows.Count < 1)
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdMain.DataSource = dt;


        }

        #endregion

        #region ◆ 유효성 검사 |

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();
        }

		#endregion

		#region ◆ Private Function |

		// TODO : 화면에서 사용할 내부 함수 추가

		/// <summary>
		/// Quick Menu 등록
		/// </summary>
		/// <param name="caption"></param>
		/// <returns></returns>
		private DXMenuCheckItem CreateCheckItem(string caption, string menuId)
		{
			DXMenuCheckItem item = new DXMenuCheckItem(caption, false, null, new EventHandler(OnQuickMenuItemClick));
			item.Tag = menuId;
			return item;
		}

		/// <summary>
		/// Quick Menu 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnQuickMenuItemClick(object sender, EventArgs e)
		{
			DXMenuCheckItem item = sender as DXMenuCheckItem;
			string menuId = (string)item.Tag;

			DataRow row = grdMain.View.GetFocusedDataRow();
			var param = row.Table.Columns
				.Cast<DataColumn>()
				.ToDictionary(col => col.ColumnName, col => row[col.ColumnName]);

			//메뉴 호출			
			OpenMenu(menuId, param);
		}
		#endregion
	}
}
