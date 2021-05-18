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

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 포장관리 > 인계등록 취소
    /// 업  무  설  명  : 인계등록 취소
    /// 생    성    자  : 박정훈
    /// 생    성    일  : 2019-08-01
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class PackingTransitCancel : SmartConditionManualBaseForm
    {
        #region ◆ Local Variables |

        #endregion

        #region ◆ 생성자 |
        /// <summary>
        /// 생성자
        /// </summary>
        public PackingTransitCancel()
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
            InitializeGrid();
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

			// 품목
			InitializeCondition_ProductPopup();
            //CommonFunction.AddConditionProductPopup("P_PRODUCTDEFID", 1.5, true, Conditions);

            // 작업장
            CommonFunction.AddConditionAreaByAuthorityPopup("P_AREAID", 2.5, false, Conditions, false, true);

            // Lot
            CommonFunction.AddConditionLotPopup("P_LOTID", 3.5, true, Conditions);
        }

        /// <summary>
        /// 조회조건 설정
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();
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
		#endregion

		#region ▶ Grid 설정 |
		/// <summary>        
		/// 그리드 초기화
		/// </summary>
		private void InitializeGrid()
        {
            #region - 포장실적 Grid 설정 |
            grdPacking.GridButtonItem = GridButtonItem.Export;

            // CheckBox 설정
            this.grdPacking.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdPacking.View.AddTextBoxColumn("BOXNO", 150).SetTextAlignment(TextAlignment.Center).SetIsReadOnly(); 
            grdPacking.View.AddTextBoxColumn("PRODUCTDEFID", 150).SetIsReadOnly(); 
            grdPacking.View.AddTextBoxColumn("PRODUCTDEFNAME", 300).SetIsReadOnly();
            grdPacking.View.AddSpinEditColumn("PCSQTY", 100).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}").SetIsReadOnly();
            grdPacking.View.AddTextBoxColumn("WORKER", 150).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdPacking.View.AddTextBoxColumn("PACKINGDATE", 200).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("{0:yyyy-MM-dd HH:mm:ss}").SetIsReadOnly();
            grdPacking.View.AddTextBoxColumn("REASONCANCEL", 200);

            grdPacking.View.PopulateColumns();

            #endregion

            #region - Lot 정보 설정 |
            grdLotList.GridButtonItem = GridButtonItem.None;
            grdLotList.View.SetIsReadOnly();

            grdLotList.View.AddTextBoxColumn("BOXNO", 150).SetTextAlignment(TextAlignment.Center);
			grdLotList.View.AddTextBoxColumn("PARENTLOTID", 250).SetTextAlignment(TextAlignment.Center);
			grdLotList.View.AddTextBoxColumn("LOTID", 250).SetTextAlignment(TextAlignment.Center);
            grdLotList.View.AddTextBoxColumn("PRODUCTDEFID", 100).SetTextAlignment(TextAlignment.Left);
            grdLotList.View.AddTextBoxColumn("PRODUCTDEFNAME", 250).SetTextAlignment(TextAlignment.Left);
            grdLotList.View.AddSpinEditColumn("PCSQTY", 70).SetTextAlignment(TextAlignment.Right);
            grdLotList.View.AddTextBoxColumn("WEEK", 70).SetTextAlignment(TextAlignment.Center);
            grdLotList.View.AddTextBoxColumn("PACKINGWEEK", 70).SetTextAlignment(TextAlignment.Center);

            grdLotList.View.PopulateColumns();
            #endregion
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

            this.grdPacking.View.RowCellClick += PackingListView_RowCellClick; 
        }

        #region - 포장 목록 클릭 이벤트 |
        /// <summary>
        /// 포장 목록 클릭 이벤트 |
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PackingListView_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            DataRow dr = grdPacking.View.GetFocusedDataRow();

            if (dr == null) return;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("P_BOXNO", dr["BOXNO"].ToString());

            DataTable dt = SqlExecuter.Query("SelectPackingLotList", "10001", param);

            if (dt == null || dt.Rows.Count == 0)
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                return;
            }

            grdLotList.DataSource = dt;
        }
        #endregion
        #endregion

        #region ◆ 툴바 |

        #region ▶ ToolBar 저장버튼 클릭 |
        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            // TODO : 저장 Rule 변경
            DataTable cancelList = grdPacking.View.GetCheckedRows();

            if (cancelList == null || cancelList.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }

            MessageWorker worker = new MessageWorker("SaveBoxPackingDispatch");
            worker.SetBody(new MessageBody()
            {
                { "TransactionType", "SetBoxPackingDispatchCancel" },
                { "EnterpriseID", UserInfo.Current.Enterprise },
                { "PlantID", UserInfo.Current.Plant },
                { "UserId", UserInfo.Current.Id },
                { "Lotlist", cancelList }
            });

            worker.Execute();

            SetInitControl();
        }
        #endregion

        #endregion

        #region ◆ 검색 |

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            SetInitControl();

            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable dt = await SqlExecuter.QueryAsync("SelectPackingFinishedList", "10002", values);

            if (dt.Rows.Count < 1)
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdPacking.DataSource = dt;
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

        #region ▶ Control Data 초기화 |
        /// <summary>
        /// Control 초기화
        /// </summary>
        private void SetInitControl()
        {
            // 기존 Grid Data 초기화
            this.grdPacking.View.ClearDatas();
            this.grdLotList.View.ClearDatas();
        } 
        #endregion

        #endregion
    }
}
