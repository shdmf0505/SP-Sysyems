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

using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.BandedGrid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Menu;
using DevExpress.Utils.Menu;
using Micube.SmartMES.Commons.Controls;
#endregion

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 공정관리 > 재공관리 > 모델별 재공 조회
    /// 업  무  설  명  : 모델별 재공 조회
    /// 생    성    자  : 배선용
    /// 생    성    일  : 2019-08-15
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class WorkInProcessByProduct : SmartConditionManualBaseForm
    {
        #region ◆ Local Variables |

        #endregion

        #region ◆ 생성자 |
        /// <summary>
        /// 생성자
        /// </summary>
        public WorkInProcessByProduct()
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
            InitializationSummaryRow();
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
            CommonFunction.AddConditionProductPopup("P_PRODUCTDEFID",0.1, true, Conditions);
            //작업장
            CommonFunction.AddConditionAreaByAuthorityPopup("P_AREAID", 3.5, true, Conditions, false, false);
            //공정
            InitializeConditionProcessSegmentId_Popup();
            // Rev별 집계 여부
            Conditions.AddComboBox("P_GROUPBYREVISION", new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("GROUPBYREVISION").SetDefault("Y").SetPosition(11.5);
        }

        /// <summary>
        /// 조회조건 설정
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();
            Conditions.GetControl<SmartComboBox>("P_PRODUCTDEFTYPE").EditValue = "Product";
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
            #region - 재공 Grid 설정 
            grdWIP.GridButtonItem = GridButtonItem.Export;

            grdWIP.View.SetIsReadOnly();
            grdWIP.SetIsUseContextMenu(false);
            // CheckBox 설정
            
            var groupDefaultCol = grdWIP.View.AddGroupColumn("DEFAULTINFO");
            groupDefaultCol.AddTextBoxColumn("STATE", 50).SetTextAlignment(TextAlignment.Center).SetIsHidden();
            groupDefaultCol.AddTextBoxColumn("CUSTOMER", 100).SetTextAlignment(TextAlignment.Left);
            groupDefaultCol.AddTextBoxColumn("LOTTYPE", 70).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("PRODUCTDEFID", 110).SetTextAlignment(TextAlignment.Left);
            groupDefaultCol.AddTextBoxColumn("PRODUCTDEFNAME", 150);
            groupDefaultCol.AddTextBoxColumn("ITEMVERSION", 50);
            groupDefaultCol.AddTextBoxColumn("USERSEQUENCE", 70).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("PROCESSSEGMENTNAME", 150);
            groupDefaultCol.AddTextBoxColumn("PLANTID", 50).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("AREANAME", 100).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("RTRSHT", 70).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("ISINTRANSIT", 70).SetTextAlignment(TextAlignment.Center).SetIsHidden();

            var groupWipCol = grdWIP.View.AddGroupColumn("WIPTOTAL");
            
            //groupWipCol.AddTextBoxColumn("WIPTOTALPCS", 70).SetTextAlignment(TextAlignment.Right);
            groupWipCol.AddSpinEditColumn("WIPTOTALPCS", 80).SetTextAlignment(TextAlignment.Right);
            groupWipCol.AddSpinEditColumn("WIPTOTALPNL", 70).SetTextAlignment(TextAlignment.Right);
            groupWipCol.AddSpinEditColumn("WIPTOTALMM", 100).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true).SetLabel("MM");

            var groupSendCol = grdWIP.View.AddGroupColumn("WAITFORRECEIVE");
            groupSendCol.AddSpinEditColumn("SENDPCSQTY", 50).SetTextAlignment(TextAlignment.Right);
            groupSendCol.AddSpinEditColumn("SENDPANELQTY", 50).SetTextAlignment(TextAlignment.Right);
            groupSendCol.AddSpinEditColumn("SENDMMQTY", 50).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true).SetLabel("MM");

			var groupReceiveCol = grdWIP.View.AddGroupColumn("ACCEPT");
            groupReceiveCol.AddSpinEditColumn("RECEIVEPCSQTY", 50).SetTextAlignment(TextAlignment.Right);
            groupReceiveCol.AddSpinEditColumn("RECEIVEPANELQTY", 50).SetTextAlignment(TextAlignment.Right);
            groupReceiveCol.AddSpinEditColumn("RECEIVEMMQTY", 50).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true).SetLabel("MM");

			var groupWorkStartCol = grdWIP.View.AddGroupColumn("WIPSTARTQTY");
            groupWorkStartCol.AddSpinEditColumn("WORKSTARTPCSQTY", 50).SetTextAlignment(TextAlignment.Right);
            groupWorkStartCol.AddSpinEditColumn("WORKSTARTPANELQTY", 50).SetTextAlignment(TextAlignment.Right);
            groupWorkStartCol.AddSpinEditColumn("WORKSTARTMMQTY", 50).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true).SetLabel("MM");

			var groupWorkEndCol = grdWIP.View.AddGroupColumn("WIPENDQTY");
            groupWorkEndCol.AddSpinEditColumn("WORKENDPCSQTY", 50).SetTextAlignment(TextAlignment.Right);
            groupWorkEndCol.AddSpinEditColumn("WORKENDPANELQTY", 50).SetTextAlignment(TextAlignment.Right);
            groupWorkEndCol.AddSpinEditColumn("WORKENDMMQTY", 50).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true).SetLabel("MM");

			var groupDefaultCol3 = grdWIP.View.AddGroupColumn("WIPAMOUNT");
            groupDefaultCol3.AddTextBoxColumn("CURRENCY", 50).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol3.AddSpinEditColumn("WIPPRICE", 110).SetTextAlignment(TextAlignment.Right);
           

            grdWIP.View.PopulateColumns();

            grdWIP.View.FixColumn(new string[] { "LOTID" });
            #endregion

        }
        #endregion
        private void InitializationSummaryRow()
        {
            grdWIP.View.Columns["PRODUCTDEFID"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdWIP.View.Columns["PRODUCTDEFID"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));

            grdWIP.View.Columns["PRODUCTDEFNAME"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            grdWIP.View.Columns["PRODUCTDEFNAME"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdWIP.View.Columns["WIPTOTALPCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdWIP.View.Columns["WIPTOTALPCS"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdWIP.View.Columns["WIPTOTALPNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdWIP.View.Columns["WIPTOTALPNL"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdWIP.View.Columns["WIPTOTALMM"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdWIP.View.Columns["WIPTOTALMM"].SummaryItem.DisplayFormat = "{0:###,###.00}";

            grdWIP.View.Columns["SENDPCSQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdWIP.View.Columns["SENDPCSQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdWIP.View.Columns["SENDPANELQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdWIP.View.Columns["SENDPANELQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdWIP.View.Columns["SENDMMQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdWIP.View.Columns["SENDMMQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdWIP.View.Columns["RECEIVEPCSQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdWIP.View.Columns["RECEIVEPCSQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdWIP.View.Columns["RECEIVEPANELQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdWIP.View.Columns["RECEIVEPANELQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdWIP.View.Columns["RECEIVEMMQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdWIP.View.Columns["RECEIVEMMQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdWIP.View.Columns["WORKSTARTPCSQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdWIP.View.Columns["WORKSTARTPCSQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdWIP.View.Columns["WORKSTARTPANELQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdWIP.View.Columns["WORKSTARTPANELQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdWIP.View.Columns["WORKSTARTMMQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdWIP.View.Columns["WORKSTARTMMQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdWIP.View.Columns["WORKENDPCSQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdWIP.View.Columns["WORKENDPCSQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdWIP.View.Columns["WORKENDPANELQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdWIP.View.Columns["WORKENDPANELQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdWIP.View.Columns["WORKENDMMQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdWIP.View.Columns["WORKENDMMQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdWIP.View.Columns["WIPPRICE"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdWIP.View.Columns["WIPPRICE"].SummaryItem.DisplayFormat = "{0:###,###}";


            grdWIP.View.OptionsView.ShowFooter = true;
            grdWIP.ShowStatusBar = false;
        }
        #endregion

        #region ◆ Event |

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            this.Load += Form_Load;
 
            grdWIP.View.PopupMenuShowing += View_PopupMenuShowing;
            grdWIP.View.CustomDrawFooterCell += View_CustomDrawFooterCell;
            grdWIP.View.RowCellStyle += View_RowCellStyle;
            grdWIP.View.DoubleClick += View_DoubleClick;


        }
        private void View_CustomDrawFooterCell(object sender, FooterCellCustomDrawEventArgs e)
        {
            Font ft = new Font("Mangul Gothic", 10F, FontStyle.Bold);
            e.Appearance.BackColor = Color.LightBlue;
            e.Appearance.FillRectangle(e.Cache, e.Bounds);
            e.Info.AllowDrawBackground = false;
            e.Appearance.Font = ft;
        }
        private void View_DoubleClick(object sender, EventArgs e)
        {
            DataRow dr = grdWIP.View.GetFocusedDataRow();
            if (Format.GetString(dr["ISINTRANSIT"]) == "Y")
            {
                string ProductDefid = Format.GetFullTrimString(dr["PRODUCTDEFID"]);
                string ProductDefVersion = Format.GetFullTrimString(dr["ITEMVERSION"]);
                string plantid = Format.GetFullTrimString(dr["PLANTID"]);

                DetailLogicStatePopup pop = new DetailLogicStatePopup(ProductDefid, ProductDefVersion, plantid);
                pop.ShowDialog(this);

            }
        }

        private void View_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {

            GridView gr = sender as GridView;
            if (e.RowHandle < 0) return;

            DataRow dr =(DataRow)gr.GetDataRow(e.RowHandle);

            Console.WriteLine(Format.GetString(dr["ISINTRANSIT"]));
            Console.WriteLine(Format.GetString(dr["PRODUCTDEFID"]));
            Console.WriteLine(Format.GetString(dr["USERSEQUENCE"]));

            if (Format.GetString(dr["ISINTRANSIT"]) =="Y")
            {
                e.Appearance.BackColor = Color.DarkKhaki;
            }
            else
            {

            }
           //DataRow dr = 
        }

        private void View_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (e.MenuType == GridMenuType.Row)
            {
                GridViewMenu menu = e.Menu as GridViewMenu;
                menu.Items.Clear();

                menu.Items.Add(CreateCheckItem(Language.Get("MENU_PG-SD-0180"), "PG-SD-0180"));
                menu.Items.Add(CreateCheckItem(Language.Get("MENU_PG-SD-0122"), "MENU_PG-SD-0122"));
                //menu.Items.Add(new DXMenuItem(""));
                //menu.Items.Add(new DXMenuItem(""));
                //menu.Items.Add(new DXMenuItem(""));
                //menu.Items.Add(new DXMenuItem(""));

            }
        }        

        #endregion

        #region ◆ 툴바 |

            /// <summary>
            /// 저장버튼 클릭
            /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            // TODO : 저장 Rule 변경
           
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
            this.grdWIP.DataSource = null;

            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            if (values["P_GROUPBYREVISION"].ToString() == "Y")
            {
                grdWIP.View.Columns["ITEMVERSION"].Visible = true;
            }
            else
            {
                grdWIP.View.Columns["ITEMVERSION"].Visible = false;
            }

            DataTable dt = await SqlExecuter.QueryAsync("SelectWipListByProduct", "10001", values);

            if (dt.Rows.Count < 1)
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdWIP.DataSource = dt;
        }
        /// <summary>
        /// 팝업형 조회조건 생성 - 공정
        /// </summary>
        private void InitializeConditionProcessSegmentId_Popup()
        {
            var processSegmentIdPopup = this.Conditions.AddSelectPopup("P_ProcessSegmentId", new SqlQuery("GetProcessSegmentList", "10002", $"PLANTID={UserInfo.Current.Plant}"
                , $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "PROCESSSEGMENTCLASSTYPE=MiddleProcessSegmentClass"), "PROCESSSEGMENTNAME", "PROCESSSEGMENTID")
                .SetPopupLayout(Language.Get("SELECTPROCESSSEGMENTID"), PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupResultCount(1)
                .SetPopupLayoutForm(800, 800, FormBorderStyle.FixedToolWindow)
                .SetPopupAutoFillColumns("PROCESSSEGMENTNAME")
                .SetLabel("PROCESSSEGMENT")
                .SetPosition(5.1);

            processSegmentIdPopup.Conditions.AddComboBox("PROCESSSEGMENTCLASSID_MIDDLE", new SqlQuery("GetProcessSegmentClassByType", "10001", $"PLANTID={UserInfo.Current.Plant}"
                , $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "PROCESSSEGMENTCLASSTYPE=MiddleProcessSegmentClass"), "PROCESSSEGMENTCLASSNAME", "PROCESSSEGMENTCLASSID")
                .SetLabel("MIDDLEPROCESSSEGMENT");
            processSegmentIdPopup.Conditions.AddTextBox("PROCESSSEGMENT");


            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME_TOP", 150)
                .SetLabel("LARGEPROCESSSEGMENT");
            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME_MIDDLE", 150)
                .SetLabel("MIDDLEPROCESSSEGMENT");
            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150);
            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
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
        private DXMenuCheckItem CreateCheckItem(string caption, string menuId)
        {
            DXMenuCheckItem item = new DXMenuCheckItem(caption, false, null, new EventHandler(OnQuickMenuItemClick));
            item.Tag = menuId;
            return item;
        }
        private void OnQuickMenuItemClick(object sender, EventArgs e)
        {
            DXMenuCheckItem item = sender as DXMenuCheckItem;
            string menuId = (string)item.Tag;

            DataRow row = grdWIP.View.GetFocusedDataRow();
            var param = row.Table.Columns
                .Cast<DataColumn>()
                .ToDictionary(col => col.ColumnName, col => row[col.ColumnName]);

            //메뉴 호출			
            OpenMenu(menuId, param);
        }
        #endregion
    }
}
