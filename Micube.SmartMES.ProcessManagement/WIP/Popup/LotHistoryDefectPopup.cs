#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons;
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
	/// 프 로 그 램 명  : 공정관리 > 공정작업 > Lot 이력 (불량 상세 팝업)
	/// 업  무  설  명  : Lot 이력 화면에서 상세하게 불량이력을 보여주기 위한 화면
	/// 생    성    자  : 박정훈
	/// 생    성    일  : 2020-01-15
	/// 수  정  이  력  : 
	/// 
	/// 
	/// </summary>
	public partial class LotHistoryDefectPopup : SmartPopupBaseForm, ISmartCustomPopup
	{
		public DataRow CurrentDataRow { get; set; }

		#region ◆ Public Variables |
		// 공정 ID
		public string ProcessSegmentId { get; set; }
		#endregion

		#region ◆ Local Variables |

		#endregion

		#region ◆ 생성자 |
		/// <summary>
		/// 생성자
		/// </summary>
		public LotHistoryDefectPopup()
		{
			InitializeComponent();

			InitializeEvent();
			InitializeGrid();
		}

		public LotHistoryDefectPopup(string lotid)
		{
			InitializeComponent();

			if (!this.IsDesignMode())
			{
				InitializeEvent();
				InitializeGrid();

				SearchData(lotid);
			}
		}
		#endregion

		#region ◆ 컨텐츠 영역 초기화 |

		#region ▶ 그리드 초기화 |
		/// <summary>
		/// 그리드 초기화
		/// </summary>
		private void InitializeGrid()
		{
			#region - DEFECT |
			grdLotDefect.GridButtonItem = GridButtonItem.Export;
			grdLotDefect.View.SetIsReadOnly();

			grdLotDefect.View.AddTextBoxColumn("LOTID", 180).SetTextAlignment(TextAlignment.Center);
			grdLotDefect.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 120);
			grdLotDefect.View.AddTextBoxColumn("USERSEQUENCE", 80).SetTextAlignment(TextAlignment.Center);
			grdLotDefect.View.AddTextBoxColumn("AREANAME", 150);
			grdLotDefect.View.AddTextBoxColumn("DEFECTCODE", 80).SetTextAlignment(TextAlignment.Center);
			grdLotDefect.View.AddTextBoxColumn("DEFECTCODENAME", 100);
			grdLotDefect.View.AddTextBoxColumn("QCSEGMENTID", 80).SetTextAlignment(TextAlignment.Center);
			grdLotDefect.View.AddTextBoxColumn("QCSEGMENTNAME", 100);
			grdLotDefect.View.AddTextBoxColumn("DEFECTQTY", 100).SetDisplayFormat("{0:#,###}").SetTextAlignment(TextAlignment.Right);
			grdLotDefect.View.AddTextBoxColumn("REASONCONSUMABLEDEFID", 100).SetTextAlignment(TextAlignment.Center);
			grdLotDefect.View.AddTextBoxColumn("REASONCONSUMABLEDEFNAME", 150).SetLabel("REASONPRODUCTDEFNAME");
			grdLotDefect.View.AddTextBoxColumn("REASONCONSUMABLELOTID", 180).SetTextAlignment(TextAlignment.Center);
			grdLotDefect.View.AddTextBoxColumn("REASONSEGMENT", 120);
			grdLotDefect.View.AddTextBoxColumn("REASONAREA", 150);

			grdLotDefect.View.PopulateColumns();

			// 하단 Summary
			InitializeDefectSummary();
            #endregion

            usInspectionResult.InitializeControls();

            usInspectionResult.DefectSplitContainer.Height = 400;
            usInspectionResult.DefectSplitContainer.Panel1.Width = 1100;
        } 
		#endregion

		#endregion

		#region ◆ Event |

		/// <summary>
		/// 화면에서 사용되는 이벤트를 초기화한다.
		/// </summary>
		public void InitializeEvent()
		{
			grdLotDefect.View.CustomDrawFooterCell += GrdDefectView_CustomDrawFooterCell;

            usInspectionResult.InitializeEvent();

            btnClose.Click += BtnClose_Click;
		}

		#region ▶ Button Event |
		/// <summary>
		/// 닫기 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}
		#endregion

		#region ▶ Grid Event |
		
		#region - Defect Grid Footer Sum Event |
		/// <summary>
		/// Grid Footer Sum Event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GrdDefectView_CustomDrawFooterCell(object sender, DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventArgs e)
		{
			DataTable dt = grdLotDefect.DataSource as DataTable;

			if (dt == null) return;

			if (dt.Rows.Count > 0)
			{
				int defectQty = 0;

				dt.Rows.Cast<DataRow>().ForEach(row =>
				{
					defectQty += Format.GetInteger(row["DEFECTQTY"]);
				});

				if (e.Column.FieldName == "DEFECTQTY")
				{
					e.Info.DisplayText = Format.GetString(defectQty);
				}
			}
			else
			{
				grdLotDefect.View.Columns["DEFECTQTY"].SummaryItem.DisplayFormat = "  ";
			}
		}
		#endregion

		#endregion

		#endregion

		#region ◆ 검색
		/// <summary>
		/// 데이터 검색
		/// </summary>
		/// <param name="lotid"></param>
		private void SearchData(string lotid)
		{
			if (string.IsNullOrWhiteSpace(lotid))
				return;

			// Lot Defect 조회
			Dictionary<string, object> param = new Dictionary<string, object>();
			param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
			param.Add("LOTID", lotid);

			grdLotDefect.DataSource = SqlExecuter.Query("SelectLotHistoryDefectPop", "10001", param);

            usInspectionResult.SearchInspectionData(lotid);
        }
		#endregion

		#region ◆ Private Function |

		#region ▶ Defect Grid Footer 추가 합계 표시 |
		/// <summary>
		/// Lot Grid Footer 추가 Panel, PCS 합계 표시
		/// </summary>
		private void InitializeDefectSummary()
		{
			grdLotDefect.View.Columns["LOTID"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			grdLotDefect.View.Columns["LOTID"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));
			grdLotDefect.View.Columns["DEFECTQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			grdLotDefect.View.Columns["DEFECTQTY"].SummaryItem.DisplayFormat = " ";

			grdLotDefect.View.OptionsView.ShowFooter = true;
			grdLotDefect.ShowStatusBar = false;
		}
		#endregion

		#endregion
	}
}