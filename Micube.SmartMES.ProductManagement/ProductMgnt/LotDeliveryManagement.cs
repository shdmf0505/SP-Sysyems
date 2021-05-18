#region using

using DevExpress.XtraGrid.Views.Grid;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons;
using System;
using Micube.SmartMES.Commons.Controls;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
#endregion

namespace Micube.SmartMES.ProductManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 생산관리 > 납기관리 > LOT별 납기 진척 현황
    /// 업  무  설  명  : 공정부하예측
    /// 생    성    자  : 황유성
    /// 생    성    일  : 2019-09-16
    /// 수  정  이  력  : 2019-10-14 황유성 스프레드시트를 그리드로 변경
    /// 
    /// 
    /// </summary>
    public partial class LotDeliveryManagement : SmartConditionManualBaseForm
    {
        #region ◆ Local Variables |

        #endregion

        #region ◆ 생성자 |
        /// <summary>
        /// 생성자
        /// </summary>
        public LotDeliveryManagement()
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

            //고객사
            InitializeCondition_CustomerPopup();
            // Lot
            CommonFunction.AddConditionLotPopup("P_LOTID", 1.5, true, Conditions);


            CommonFunction.AddConditionProcessSegmentPopup("P_PROCESSSEGMENTID", 5.5, true, Conditions);
        

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
                .SetPosition(0.2)
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
        /// 팝업형 조회조건 생성 - 고객사
        /// </summary>
        private void InitializeCondition_CustomerPopup()
        {
            var conditionCustomer = Conditions.AddSelectPopup("P_CUSTOMER", new SqlQuery("GetCustomerList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "CUSTOMERNAME", "CUSTOMERID")
                .SetPopupLayout("SELECTCUSTOMERID", PopupButtonStyles.Ok_Cancel, true, true)
                .SetPopupLayoutForm(800, 800)
                .SetPopupAutoFillColumns("CUSTOMERNAME")
                .SetLabel("CUSTOMERID")
                .SetPosition(1.6)
                .SetPopupResultCount(0);

            // 팝업 조회조건
            conditionCustomer.Conditions.AddTextBox("TXTCUSTOMERID");

            // 팝업 그리드
            conditionCustomer.GridColumns.AddTextBoxColumn("CUSTOMERID", 150);
            conditionCustomer.GridColumns.AddTextBoxColumn("CUSTOMERNAME", 200);
        }





        /// <summary>
        /// 조회조건 설정
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();
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
            grdLotDelivery.GridButtonItem = GridButtonItem.Export;

            #region - 공정 부하 Grid 설정 
            grdLotDelivery.View.ClearColumns();
            grdLotDelivery.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            // TODO : 그리드 초기화 로직 추가
            grdLotDelivery.GridButtonItem = GridButtonItem.Export;
            grdLotDelivery.SetIsUseContextMenu(true);
            grdLotDelivery.View.SetIsReadOnly();

            grdLotDelivery.View.AddTextBoxColumn("PLANTID", 60).SetIsReadOnly().SetTextAlignment(TextAlignment.Center); //Site
            grdLotDelivery.View.AddTextBoxColumn("LOTTYPE", 100).SetIsReadOnly().SetTextAlignment(TextAlignment.Center); //양산구분
            grdLotDelivery.View.AddTextBoxColumn("PRODUCTDEFID", 150).SetIsReadOnly(); //품목코드
            grdLotDelivery.View.AddTextBoxColumn("PRODUCTDEFVERSION", 60).SetIsReadOnly().SetLabel("ITEMVERSION"); //품목코드
            grdLotDelivery.View.AddTextBoxColumn("PRODUCTDEFNAME", 200).SetIsReadOnly(); //품목명
            grdLotDelivery.View.AddTextBoxColumn("LOTID", 240).SetIsReadOnly().SetTextAlignment(TextAlignment.Center); //LOTNO
            grdLotDelivery.View.AddTextBoxColumn("USERSEQUENCE", 60).SetIsReadOnly().SetTextAlignment(TextAlignment.Center); //공정수순
            grdLotDelivery.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 200).SetIsReadOnly(); //공정명
            grdLotDelivery.View.AddTextBoxColumn("AREANAME", 150).SetIsReadOnly(); //작업장명
            grdLotDelivery.View.AddTextBoxColumn("RTRSHT", 90).SetIsReadOnly().SetTextAlignment(TextAlignment.Center); //R/S
            grdLotDelivery.View.AddTextBoxColumn("ISHOLD", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center); //보류여부
            grdLotDelivery.View.AddTextBoxColumn("QTY", 80).SetIsReadOnly(); //재공(PCS)
            grdLotDelivery.View.AddTextBoxColumn("PANELQTY", 80).SetIsReadOnly(); //재공(PNL)
            grdLotDelivery.View.AddTextBoxColumn("MM", 100).SetDisplayFormat("#,##0.##", MaskTypes.Numeric).SetIsReadOnly(); //재공(M2) 소수점 2자리
            grdLotDelivery.View.AddTextBoxColumn("DUEDATE", 130).SetIsReadOnly().SetTextAlignment(TextAlignment.Center); //생산완료예정일
            grdLotDelivery.View.AddTextBoxColumn("INPUTDAY", 130).SetIsReadOnly().SetTextAlignment(TextAlignment.Center); //투입일
            grdLotDelivery.View.AddTextBoxColumn("LEFTDATE", 60).SetIsReadOnly(); //잔여일
            grdLotDelivery.View.AddTextBoxColumn("STANDARD_CUM_LEADTIME", 100).SetIsReadOnly(); //표준누적L/T
            grdLotDelivery.View.AddTextBoxColumn("WORK_CUM_LEADTIME", 100).SetIsReadOnly(); //작업누적L/T
            grdLotDelivery.View.AddTextBoxColumn("DIFFERENCE_LEADTIME", 80).SetIsReadOnly(); //차이L/T
            grdLotDelivery.View.AddTextBoxColumn("REMAINSEGMENT_COUNT", 80).SetIsReadOnly(); //잔여공정수
            grdLotDelivery.View.AddTextBoxColumn("REMAIN_LEADTIME", 80).SetIsReadOnly(); //잔여L/T
            grdLotDelivery.View.AddComboBoxColumn("STATUS", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=EmergencyDegree", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);

            grdLotDelivery.View.PopulateColumns();
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
           // this.grdLotDelivery.View.RowCellStyle += View_RowCellStyle;
            this.grdLotDelivery.View.RowStyle += View_RowStyle;
        }

        private void View_RowStyle(object sender, RowStyleEventArgs e)
        {
            if(e.RowHandle > -1)
            {
                
               if(this.grdLotDelivery.View.GetRowCellValue(e.RowHandle, "STATUS").ToString().ToUpper().Equals("URGENCY"))
                {
                    e.Appearance.ForeColor = Color.Red;
                    e.HighPriority = true;
                }
            }




        }

        private void View_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.RowHandle > -1)
            {
                if(e.Column.FieldName.ToUpper().Equals("STATUS") && e.CellValue.ToString().ToUpper().Equals("URGENCY"))
                {
                    e.Appearance.BackColor = Color.Red;
                    e.Appearance.ForeColor = Color.White;
                }
                //if (e.Column.FieldName.ToUpper().Equals(loadDate + "_LOADQTY"))
                ////    if (e.Column.FieldName.ToUpper().EndsWith("_LOADQTY"))
                //{
                //    e.Appearance.BackColor = Color.Yellow;
                //}
            }
        }

        #endregion

        #region ◆ 툴바 |


        #endregion

        #region ◆ 검색 |

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            // 기존 Grid Data 초기화

            // p_productionDivision

            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);


            if (values.ContainsKey("P_PRODUCTNAME"))
                values.Remove("P_PRODUCTNAME");
            if(values["P_PRODUCTDEFID"] == null)
            {
                values["P_PRODUCTDEFID"] = string.Empty;
            }

            InitializeGrid();

            DataTable dtResult = await QueryAsync("SelectDeliveryStatusByLot", "10001", values);


            if (dtResult.Rows.Count < 1)
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }
            else
            {
                //txtConfirmDate.Text = dtResult.Rows[0]["CONFIRMDATE"].ToString();
                //txtConfirmUser.Text = dtResult.Rows[0]["CONFIRMUSERNAME"].ToString();
            }
            grdLotDelivery.DataSource = dtResult;
          
      

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
        /// <summary>
        /// from 부터 to까지의 일자들의 컬렉션을 반환한다.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        private IEnumerable<DateTime> EachDay(DateTime from, DateTime to)
        {
            for (var day = from.Date; day.Date <= to.Date; day = day.AddDays(1))
            {
                yield return day;
            }
        }
        #endregion
    }
}
