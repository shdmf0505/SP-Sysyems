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
using DevExpress.XtraReports.UI;
using Micube.SmartMES.Commons.Controls;
#endregion

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 공정관리 > 투입관리 > 투입량 산출
    /// 업  무  설  명  : 투입량 산출
    /// 생    성    자  : 배선용
    /// 생    성    일  : 2019-07-11
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class CaculationInputQty : SmartConditionManualBaseForm
    {
        #region ◆ Local Variables |

        #endregion

        #region ◆ 생성자 |
        /// <summary>
        /// 생성자
        /// </summary>
        public CaculationInputQty()
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

			//화면 뜰 때 삭제 버튼 숨기기
			var buttons = pnlToolbar.Controls.Find<SmartButton>(true);
			buttons.AsEnumerable().ForEach(b =>
			{
				if (b.Name.Equals("Delete")) b.Visible = false;
				else b.Visible = true;
			});
		}

        #region ▶ 조회조건 설정 |
        /// <summary>
        /// 검색조건 설정
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();
            
            // 품목
            CommonFunction.AddConditionProductPopupPrefixSearch("P_PRODUCTDEFID",1.2, true, Conditions);

            Conditions.GetCondition<ConditionItemSelectPopup>("P_PRODUCTDEFID").SetPopupApplySelection((selectedRows, dataGridRows) =>
            {
                List<DataRow> list = selectedRows.ToList<DataRow>();

                List<string> listRev = new List<string>();

                string productlist = string.Empty;
                selectedRows.ForEach(row =>
                {
                    string productid = Format.GetString(row["PRODUCTDEFID"]);
                    string revision = Format.GetString(row["PRODUCTDEFVERSION"]);
                    productlist = productlist + productid + ',';
                    listRev.Add(revision);
                });

                productlist = productlist.TrimEnd(',');

                listRev = listRev.Distinct().ToList();
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                param.Add("P_PLANTID", UserInfo.Current.Plant);
                param.Add("P_PRODUCTDEFID", productlist);

                DataTable dt = SqlExecuter.Query("selectProductdefVesion", "10001", param);

                Conditions.GetControl<SmartComboBox>("P_PRODUCTDEFVERSION").DataSource = dt;
                if (listRev.Count > 0)
                {
                    if (listRev.Count == 1)
                        Conditions.GetControl<SmartComboBox>("P_PRODUCTDEFVERSION").EditValue = Format.GetFullTrimString(listRev[0]);
                    else
                        Conditions.GetControl<SmartComboBox>("P_PRODUCTDEFVERSION").EditValue = Format.GetFullTrimString('*');
                }
            });
        }

        /// <summary>
        /// 조회조건 설정
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();
            Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").EditValueChanged += ProductdefIDChanged;
        }

        private void ProductdefIDChanged(object sender, EventArgs e)
        {
            SmartSelectPopupEdit PopProdutid = sender as SmartSelectPopupEdit;

            if (Format.GetFullTrimString(PopProdutid.EditValue).Equals(string.Empty))
            {
                Conditions.GetControl<SmartComboBox>("P_PRODUCTDEFVERSION").EditValue = string.Empty;
            }
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
            #region - 투입량 산출 리스트 |
            grdProductList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            
            grdProductList.SetIsUseContextMenu(false);
            // CheckBox 설정

            //수주번호
            grdProductList.View.AddTextBoxColumn("PRODUCTIONORDERID", 120).SetTextAlignment(TextAlignment.Left)
                                                                         .SetIsReadOnly()
                                                                         .SetLabel("SALSEORDERNO");
            //라인
            grdProductList.View.AddTextBoxColumn("LINENO", 60).SetTextAlignment(TextAlignment.Center)
                                                              .SetIsReadOnly().SetIsHidden()
                                                              .SetLabel("LINENO");
            //수주사양결재일
            grdProductList.View.AddTextBoxColumn("SPECAPPROVALDATE", 140).SetTextAlignment(TextAlignment.Center).SetLabel("AACBB93F86014630ABEFDBABEF378892")
                                                              .SetIsReadOnly().SetDisplayFormat("yyyy-MM-dd HH:mm:ss");
            //수주일
            grdProductList.View.AddTextBoxColumn("PRODUCTIONTYPENAME", 80).SetTextAlignment(TextAlignment.Center)
                                                                       .SetIsReadOnly()
                                                                       .SetLabel("ORDERTYPE");
            //PLANT
            grdProductList.View.AddTextBoxColumn("PLANTID", 60).SetTextAlignment(TextAlignment.Center)
                                                                       .SetIsReadOnly();
            //납기일
            grdProductList.View.AddTextBoxColumn("OWNERFACTORYNAME", 60).SetTextAlignment(TextAlignment.Center).SetIsHidden()
                                                                     .SetIsReadOnly()
                                                                     .SetLabel("FACTORY");
            //품목코드
            grdProductList.View.AddTextBoxColumn("PRODUCTDEFID", 90).SetTextAlignment(TextAlignment.Left)
                                                                     .SetIsReadOnly()
                                                                     .SetLabel("ITEMCODE");
            //rev
            grdProductList.View.AddTextBoxColumn("PRODUCTDEFVERSION", 60).SetTextAlignment(TextAlignment.Center)
                                                                         .SetIsReadOnly()
                                                                         .SetLabel("ITEMVERSION");
            //품목명
            grdProductList.View.AddTextBoxColumn("PRODUCTDEFNAME", 150).SetTextAlignment(TextAlignment.Left)
                                                                       .SetIsReadOnly()
                                                                       .SetLabel("PRODUCTDEFNAME");
            //rtrsht
            grdProductList.View.AddTextBoxColumn("RTRSHT", 60).SetTextAlignment(TextAlignment.Left)
                                                              .SetIsReadOnly()
                                                              .SetLabel("RTRSHT");
            //배열수
            grdProductList.View.AddSpinEditColumn("PCSPNL", 60).SetTextAlignment(TextAlignment.Right)
                                                               .SetIsReadOnly()
                                                               .SetLabel("ARRAY");
            //산출수
            grdProductList.View.AddSpinEditColumn("PCSMM", 60).SetTextAlignment(TextAlignment.Right)
                                                               .SetIsReadOnly()
                                                               .SetLabel("CALCULATION");
            //단가
            grdProductList.View.AddSpinEditColumn("UNITPRICE", 60).SetTextAlignment(TextAlignment.Right)
                                                               .SetIsReadOnly()
                                                               .SetDisplayFormat("#,##0.######",MaskTypes.Numeric,true)
                                                               .SetLabel("UNITPRICE");
            //투입대기
            grdProductList.View.AddSpinEditColumn("NOTNPUTQTY", 60).SetTextAlignment(TextAlignment.Right)
                                                               .SetLabel("INPUTREADY")
                                                               .SetValidationIsRequired();
            //수주량
            grdProductList.View.AddSpinEditColumn("PLANQTY", 80).SetTextAlignment(TextAlignment.Right)
                                                                  .SetLabel("ORDERQTY")
                                                                  .SetValidationIsRequired();
            //수주잔량
            grdProductList.View.AddSpinEditColumn("POSUM", 80).SetTextAlignment(TextAlignment.Right)
                                                                  .SetLabel("POREMAINQTY")
                                                                  .SetValidationIsRequired(); 
            //재공
            grdProductList.View.AddSpinEditColumn("WIPQTY", 80).SetTextAlignment(TextAlignment.Right)
                                                               .SetIsReadOnly()
                                                               .SetLabel("WIPSTOCK");
            //정상
            grdProductList.View.AddSpinEditColumn("NORMALWIP", 80).SetTextAlignment(TextAlignment.Right)
                                                               .SetLabel("NORMAL")
                                                               .SetValidationIsRequired();
            //재검
            grdProductList.View.AddSpinEditColumn("RETURNLOT", 80).SetTextAlignment(TextAlignment.Right)
                                                               .SetValidationIsRequired()
                                                               .SetLabel("REINSPECT");
            //MRB
            grdProductList.View.AddSpinEditColumn("MRBLOT", 80).SetTextAlignment(TextAlignment.Right)
                                                               .SetValidationIsRequired()
                                                               .SetLabel("MRB");
            //보류
            grdProductList.View.AddSpinEditColumn("HOLDLOT", 80).SetTextAlignment(TextAlignment.Right)
                                                               .SetValidationIsRequired()
                                                               .SetLabel("HOLD");
            //현재수율
            grdProductList.View.AddSpinEditColumn("NOWYIELD", 60).SetTextAlignment(TextAlignment.Right)
                                                                    .SetLabel("NOWYIELD").SetDisplayFormat()
                                                                    .SetValidationIsRequired();
            //양품환산
            grdProductList.View.AddSpinEditColumn("CHGOOD", 60).SetTextAlignment(TextAlignment.Right)
                                                               .SetIsReadOnly()
                                                               .SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true)
                                                               .SetLabel("CHANGEGOOD");
            //현재고
            grdProductList.View.AddSpinEditColumn("WHQTY", 60).SetTextAlignment(TextAlignment.Right)
                                                               .SetIsReadOnly()
                                                               .SetLabel("CURRENTSTOCK");
            //예상재고
            grdProductList.View.AddSpinEditColumn("WIPWHNOTINPUT", 60).SetTextAlignment(TextAlignment.Right)
                                                               .SetIsReadOnly()
                                                               .SetLabel("EXPECTSTOCK");
            //투입대상수량
            grdProductList.View.AddSpinEditColumn("INPUTTARGET", 60).SetTextAlignment(TextAlignment.Right)
                                                               .SetIsReadOnly()
                                                               .SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true)
                                                               .SetLabel("INPUTTARGETQY");
            //목표수율
            grdProductList.View.AddSpinEditColumn("INPUTYIELD", 60).SetTextAlignment(TextAlignment.Right)
                                                                    .SetLabel("INPUTYIELD")
                                                                    .SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true)
                                                                    .SetValidationIsRequired();
            //투입예상(PCS)
            grdProductList.View.AddSpinEditColumn("EXPECTINPUTPCS", 60).SetTextAlignment(TextAlignment.Right)
                                                                  .SetIsReadOnly()
                                                                  .SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true)
                                                                  .SetLabel("EXPECTPCS");
            //투입예상(PNL)
            grdProductList.View.AddSpinEditColumn("EXPECTINPUTPNL", 60).SetTextAlignment(TextAlignment.Right)
                                                                  .SetIsReadOnly()
                                                                  .SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true)
                                                                  .SetLabel("EXPECTPNL");
            //실투입량(PCS)
            grdProductList.View.AddSpinEditColumn("REALINPUTPCS", 60).SetTextAlignment(TextAlignment.Right)
                                                                     .SetLabel("ACTUALINPUTPCS")
                                                                     .SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true)
                                                                     .SetIsReadOnly();
            //실투입량(PNL)
            grdProductList.View.AddSpinEditColumn("REALINPUTPNL", 60).SetTextAlignment(TextAlignment.Right)
                                                                     .SetLabel("ACTUALINPUTPNL")
                                                                     .SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true)
                                                                     .SetIsReadOnly();
            //PCS 투입률
            grdProductList.View.AddSpinEditColumn("PCSINPUTRATE", 60).SetTextAlignment(TextAlignment.Right)
                                                                     .SetIsReadOnly().SetIsHidden()
                                                                     .SetLabel("PCSINPUTRATE");
            //금액투입률
            grdProductList.View.AddSpinEditColumn("PRICEINPUTRATE", 60).SetTextAlignment(TextAlignment.Right)
                                                                     .SetIsReadOnly().SetIsHidden()
                                                                     .SetLabel("PRICEINPUTRATE");
            //투입금액(억)
            grdProductList.View.AddSpinEditColumn("INPUTPRICE", 60).SetTextAlignment(TextAlignment.Right)
                                                                     .SetIsReadOnly().SetIsHidden()
                                                                     .SetLabel("INPUTPRICEM");



            grdProductList.View.PopulateColumns();
            #endregion

            #region - 투입량 산출 리스트
            grdCalList.View.SetIsReadOnly();
            grdCalList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

         //   grdCalList.View.AddTextBoxColumn("", 20).SetTextAlignment(TextAlignment.Left);
            grdCalList.View.AddTextBoxColumn("PRODUCTIONORDERID", 120).SetTextAlignment(TextAlignment.Left).SetLabel("SALSEORDERNO");
            grdCalList.View.AddTextBoxColumn("LINENO", 60).SetTextAlignment(TextAlignment.Center).SetLabel("LINENO").SetIsHidden();
            grdCalList.View.AddTextBoxColumn("SPECAPPROVALDATE", 140).SetTextAlignment(TextAlignment.Center).SetIsReadOnly().SetLabel("AACBB93F86014630ABEFDBABEF378892").SetDisplayFormat("yyyy -MM-dd HH:mm:ss");
            grdCalList.View.AddTextBoxColumn("PRODUCTIONTYPENAME", 80).SetTextAlignment(TextAlignment.Center).SetLabel("ORDERTYPE");
            grdCalList.View.AddTextBoxColumn("PLANTID", 60).SetTextAlignment(TextAlignment.Center).SetLabel("PLANTID").SetIsHidden();
            grdCalList.View.AddTextBoxColumn("OWNERFACTORYNAME", 60).SetTextAlignment(TextAlignment.Center).SetLabel("FACTORY").SetIsHidden();
            grdCalList.View.AddTextBoxColumn("PRODUCTDEFID", 90).SetTextAlignment(TextAlignment.Left).SetLabel("ITEMCODE");
            grdCalList.View.AddTextBoxColumn("PRODUCTDEFVERSION", 60).SetTextAlignment(TextAlignment.Center).SetLabel("ITEMVERSION");
            grdCalList.View.AddTextBoxColumn("PRODUCTDEFNAME", 150).SetTextAlignment(TextAlignment.Left).SetLabel("PRODUCTDEFNAME");
            grdCalList.View.AddTextBoxColumn("RTRSHT", 60).SetTextAlignment(TextAlignment.Left).SetLabel("RTRSHT");
            grdCalList.View.AddSpinEditColumn("PCSPNL", 60).SetTextAlignment(TextAlignment.Right).SetLabel("ARRAY");
            grdCalList.View.AddSpinEditColumn("PCSMM", 60).SetTextAlignment(TextAlignment.Right).SetLabel("CALCULATION");
            grdCalList.View.AddSpinEditColumn("UNITPRICE", 60).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.######", MaskTypes.Numeric, true).SetLabel("UNITPRICE");
            grdCalList.View.AddSpinEditColumn("NOTINPUTQTY", 60).SetTextAlignment(TextAlignment.Right).SetLabel("INPUTREADY");
            grdCalList.View.AddSpinEditColumn("PLANQTY", 80).SetTextAlignment(TextAlignment.Right).SetLabel("ORDERQTY");
            grdCalList.View.AddSpinEditColumn("POSUM", 80).SetTextAlignment(TextAlignment.Right).SetLabel("POREMAINQTY");
            grdCalList.View.AddSpinEditColumn("WIPQTY", 80).SetTextAlignment(TextAlignment.Right).SetLabel("WIPSTOCK");
            grdCalList.View.AddSpinEditColumn("NORMALWIP", 80).SetTextAlignment(TextAlignment.Right).SetLabel("NORMAL");
            grdCalList.View.AddSpinEditColumn("RETURNLOT", 80).SetTextAlignment(TextAlignment.Right).SetLabel("REINSPECT");
            grdCalList.View.AddSpinEditColumn("MRBLOT", 80).SetTextAlignment(TextAlignment.Right).SetLabel("MRB");
            grdCalList.View.AddSpinEditColumn("HOLDLOT", 80).SetTextAlignment(TextAlignment.Right).SetLabel("HOLD");
            grdCalList.View.AddSpinEditColumn("NOWYIELD", 60).SetTextAlignment(TextAlignment.Right).SetLabel("NOWYIELD").SetDisplayFormat();
            grdCalList.View.AddSpinEditColumn("CHGOOD", 60).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true).SetLabel("CHANGEGOOD");
            grdCalList.View.AddSpinEditColumn("WHQTY", 60).SetTextAlignment(TextAlignment.Right).SetLabel("CURRENTSTOCK");
            grdCalList.View.AddSpinEditColumn("WIPWHNOTINPUT", 60).SetTextAlignment(TextAlignment.Right).SetLabel("EXPECTSTOCK");
            grdCalList.View.AddSpinEditColumn("INPUTTARGET", 60).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true).SetLabel("INPUTTARGETQY");
            grdCalList.View.AddSpinEditColumn("INPUTYIELD", 60).SetTextAlignment(TextAlignment.Right).SetLabel("INPUTYIELD").SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true);
            grdCalList.View.AddSpinEditColumn("EXPECTINPUTPCS", 60).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true).SetLabel("EXPECTPCS");
            grdCalList.View.AddSpinEditColumn("EXPECTINPUTPNL", 60).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true).SetLabel("EXPECTPNL");
            grdCalList.View.AddSpinEditColumn("REALINPUTPCS", 60).SetTextAlignment(TextAlignment.Right).SetLabel("ACTUALINPUTPCS").SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true);
            grdCalList.View.AddSpinEditColumn("REALINPUTPNL", 60).SetTextAlignment(TextAlignment.Right).SetLabel("ACTUALINPUTPNL").SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true);
            grdCalList.View.AddSpinEditColumn("PRICEINPUTRATE", 60).SetTextAlignment(TextAlignment.Right).SetLabel("PRICEINPUTRATE").SetIsHidden(); ;
            grdCalList.View.AddSpinEditColumn("INPUTPRICE", 60).SetTextAlignment(TextAlignment.Right).SetLabel("INPUTPRICEM").SetIsHidden(); ;
            grdCalList.View.AddTextBoxColumn("SAVETIME", 140).SetTextAlignment(TextAlignment.Center).SetIsReadOnly().SetDisplayFormat("yyyy-MM-dd HH:mm:ss");

            grdCalList.View.PopulateColumns();
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

			tabMain.SelectedPageChanged += TabMain_SelectedPageChanged;
            grdProductList.View.CellValueChanged += View_CellValueChanged;
            //grdProductList.View.FocusedRowChanged += View_FocusedRowChanged;
            grdProductList.View.LeftCoordChanged += View_LeftCoordChanged;
            grdCalList.View.LeftCoordChanged += View_LeftCoordChanged1;
        }

		/// <summary>
		/// 탭 변경 시 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TabMain_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
		{
			int index = tabMain.SelectedTabPageIndex;
			var buttons = pnlToolbar.Controls.Find<SmartButton>(true);
			switch (index)
			{
				case 0:

					buttons.AsEnumerable().ForEach(b =>
					{
						if (b.Name.Equals("Delete")) b.Visible = false;
						else b.Visible = true;
					});

					break;
				case 1:

					buttons.AsEnumerable().ForEach(b =>
					{
						if (b.Name.Equals("Delete")) b.Visible = true;
						else b.Visible = false;
					});

					break;
			}
		}

		private void View_LeftCoordChanged1(object sender, EventArgs e)
        {
            grdProductList.View.LeftCoord = grdCalList.View.LeftCoord;
        }

        private void View_LeftCoordChanged(object sender, EventArgs e)
        {
            grdCalList.View.LeftCoord = grdProductList.View.LeftCoord;
        }

        private void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle < 0) return;

            DataRow dr = grdProductList.View.GetFocusedDataRow();

            string ProductDefid = Format.GetFullTrimString(dr["PRODUCTDEFID"]);
            string ProductDefversion = Format.GetFullTrimString(dr["PRODUCTDEFVERSION"]);

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("PLANTID", UserInfo.Current.Plant);
            param.Add("PRODUCTDEFID", ProductDefid);
            param.Add("PRODUCTDEFVERSION", ProductDefversion);

            DataTable dt = SqlExecuter.Query("SelectRegisteCaculationExpectInputQty", "10001", param);

            grdCalList.DataSource = dt;
        }

        private void View_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
          
            double changeValue = 0;
            //현재 수율
            //양품 환산 = (정상+재검) * (현재수율/100)
            string changedColumn = Format.GetFullTrimString(e.Column.FieldName);
            if (changedColumn.Equals("NOWYIELD") || changedColumn.Equals("NORMALWIP") || changedColumn.Equals("RETURNLOT") || changedColumn.Equals("MRBLOT") || changedColumn.Equals("HOLDLOT"))
            {
                double normalWip = Format.GetDouble(grdProductList.View.GetRowCellValue(e.RowHandle, "NORMALWIP"), 0);
                double ReturnLot = Format.GetDouble(grdProductList.View.GetRowCellValue(e.RowHandle, "RETURNLOT"), 0);
                double mrbLot = Format.GetDouble(grdProductList.View.GetRowCellValue(e.RowHandle, "MRBLOT"), 0);
                double holdLot = Format.GetDouble(grdProductList.View.GetRowCellValue(e.RowHandle, "HOLDLOT"), 0);
                double nowYield = Format.GetDouble(grdProductList.View.GetRowCellValue(e.RowHandle, "NOWYIELD"), 0);

                double whQty = Format.GetDouble(grdProductList.View.GetRowCellValue(e.RowHandle, "WHQTY"), 0);

                changeValue = (normalWip + ReturnLot) * (nowYield / 100);
                //양품환산 = (정상+재검) * (현재수율/100 )
                grdProductList.View.SetRowCellValue(e.RowHandle, "CHGOOD", Math.Round(changeValue, 2));
                //재공 (정상+재검+MRB+보류)) --
                grdProductList.View.SetRowCellValue(e.RowHandle, "WIPQTY", normalWip + ReturnLot + mrbLot + holdLot);
                //예상재고 (양품환산 + 현재고)
                grdProductList.View.SetRowCellValue(e.RowHandle, "WIPWHNOTINPUT", changeValue + whQty);
            }
            //미투입 수량 변경시 재공+재고+미투입
            else if (changedColumn.Equals("NOTNPUTQTY") || changedColumn.Equals("CHGOOD"))
            {
                //미투입
                double notInputQty = Format.GetDouble(grdProductList.View.GetRowCellValue(e.RowHandle, "NOTNPUTQTY"), 0);
                //재고
                double whQty = Format.GetDouble(grdProductList.View.GetRowCellValue(e.RowHandle, "WHQTY"), 0);
                //재공
                double wipQty = Format.GetDouble(grdProductList.View.GetRowCellValue(e.RowHandle, "CHGOOD"), 0);

                changeValue = notInputQty + whQty + wipQty;

                grdProductList.View.SetRowCellValue(e.RowHandle, "WIPWHNOTINPUT", Math.Round(changeValue, 2));
            }
            else if (changedColumn.Equals("WIPWHNOTINPUT") || changedColumn.Equals("POSUM"))
            {
                //수주잔량
                double remainPO = Format.GetDouble(grdProductList.View.GetRowCellValue(e.RowHandle, "POSUM"), 0);
                double WipWhNotInput = Format.GetDouble(grdProductList.View.GetRowCellValue(e.RowHandle, "WIPWHNOTINPUT"), 0);

                changeValue = remainPO - WipWhNotInput;
                changeValue = changeValue < 0 ? 0 : changeValue;

                grdProductList.View.SetRowCellValue(e.RowHandle, "INPUTTARGET", changeValue);

            }
            else if (changedColumn.Equals("INPUTTARGET") || changedColumn.Equals("INPUTYIELD"))
            {
                //투입예상PCS = 투입대상수량/(투입수율/100)
                double inputTargetQty = Format.GetDouble(grdProductList.View.GetRowCellValue(e.RowHandle, "INPUTTARGET"), 0);
                double inputYield = Format.GetDouble(grdProductList.View.GetRowCellValue(e.RowHandle, "INPUTYIELD"), 0);

                changeValue = inputYield ==0 ? 0 :  inputTargetQty / (inputYield / 100);

                grdProductList.View.SetRowCellValue(e.RowHandle, "EXPECTINPUTPCS", Math.Round(changeValue,2));
            }
            else if (changedColumn.Equals("EXPECTINPUTPCS"))
            {
                double expectInputPCS = Format.GetDouble(grdProductList.View.GetRowCellValue(e.RowHandle, "EXPECTINPUTPCS"), 0);
                double pcspln = Format.GetDouble(grdProductList.View.GetRowCellValue(e.RowHandle, "PCSPNL"), 0);

                changeValue = expectInputPCS / pcspln;
                //투입예상(PNL) 투입예상(PCS) * 합수
                grdProductList.View.SetRowCellValue(e.RowHandle, "EXPECTINPUTPNL", changeValue);
                //실투입(PCS) 실투입(PNL) * 합수
                grdProductList.View.SetRowCellValue(e.RowHandle, "REALINPUTPCS", Math.Ceiling(changeValue)*pcspln);
                //실투입(PNL) 투입판넬 올림
                grdProductList.View.SetRowCellValue(e.RowHandle, "REALINPUTPNL", Math.Ceiling(changeValue));
            }
            //(실투입(PCS)*단가)/(투입대상사수량*단가)
            else if (changedColumn.Equals("REALINPUTPCS") || changedColumn.Equals("INPUTTARGET"))
            {
                double inputTargetPCS = Format.GetDouble(grdProductList.View.GetRowCellValue(e.RowHandle, "INPUTTARGET"), 0);
                double realInputPCS = Format.GetDouble(grdProductList.View.GetRowCellValue(e.RowHandle, "REALINPUTPCS"), 0);
                double unitPrice = Format.GetDouble(grdProductList.View.GetRowCellValue(e.RowHandle, "UNITPRICE"), 0);

                changeValue = (realInputPCS * unitPrice) / (inputTargetPCS * unitPrice) * 100;

                double pcsinputRate = inputTargetPCS ==0 ? 0 : realInputPCS / inputTargetPCS * 100;

                double inputPrice = (realInputPCS * unitPrice) / 100000000;
                /*
                grdProductList.View.SetRowCellValue(e.RowHandle, "PCSINPUTRATE", Math.Round(pcsinputRate, 2));
                grdProductList.View.SetRowCellValue(e.RowHandle, "PRICEINPUTRATE", Math.Round(changeValue,2));
                grdProductList.View.SetRowCellValue(e.RowHandle, "INPUTPRICE", Math.Round(inputPrice, 2));
                */
            }

        }


        #region ▶ Button Event |

        #endregion

        #region ▶ Grid Event |


        #endregion

        #endregion

        #region ◆ 툴바 |

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        //protected override void OnToolbarSaveClick()
        //{
        //    base.OnToolbarSaveClick();
        //}

		/// <summary>
		/// 저장 / 삭제 버튼 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected override void OnToolbarClick(object sender, EventArgs e)
		{
			SmartButton btn = sender as SmartButton;

			DataTable dt = new DataTable();
			MessageWorker worker = null;
			switch (btn.Name)
			{
				case "Save":

					dt = grdProductList.GetChangedRows();

					worker = new MessageWorker("SaveCalInputQty");
					worker.SetBody(new MessageBody()
					{
						{ "EnterpriseID", UserInfo.Current.Enterprise },
						{ "PlantID", UserInfo.Current.Plant },
						{ "UserId", UserInfo.Current.Id },
						{ "Productlist", dt },
					});

					break;
				case "Delete":

					dt = grdCalList.View.GetCheckedRows();

					worker = new MessageWorker("CancelCalInputQty");
					worker.SetBody(new MessageBody()
					{
						{ "EnterpriseID", UserInfo.Current.Enterprise },
						{ "PlantID", UserInfo.Current.Plant },
						{ "UserId", UserInfo.Current.Id },
						{ "Productlist", dt },
					});

					break;
			}

			worker.Execute();
			ShowMessage("SuccessSave");

			OnSearchAsync();
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

            if(!values.ContainsKey("ISLIKE"))
            {
                values.Add("ISLIKE", "");
            }

            string productdefidValue = Format.GetTrimString(Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").GetValue());
            string productdefid = Format.GetTrimString(Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").EditValue);

            //set value가 없고 text에 입력이 있으면 입력된 문자로 like 검색
            if(string.IsNullOrEmpty(productdefidValue) && !string.IsNullOrEmpty(productdefid))
            {
                values["ISLIKE"] = "Y";
            }

            if (tabMain.SelectedTabPageIndex == 0)
            {
                this.grdProductList.View.ClearDatas();

                DataTable dt = await SqlExecuter.QueryAsync("SelectCaculationExpectInputQty", "10001", values);

                if (dt.Rows.Count < 1)
                {
                    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                }
                grdProductList.DataSource = dt;
            }
            else if (tabMain.SelectedTabPageIndex == 1)
            {
                this.grdCalList.View.ClearDatas();
                values["P_PERIOD_PERIODFR"] = values["P_PERIOD_PERIODFR"].ToString().Substring(0, 10) + " 00:00:00";
                values["P_PERIOD_PERIODTO"] = values["P_PERIOD_PERIODTO"].ToString().Substring(0, 10) + " 23:59:59";

                DataTable dt = SqlExecuter.Query("SelectRegisteCaculationExpectInputQty", "10001", values);

                if (dt.Rows.Count < 1)
                {
                    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                }
                grdCalList.DataSource = dt;
            }
        }
		#endregion

		#region ◆ 유효성 검사 |

		/// <summary>
		/// 데이터 저장할때 컨텐츠 영역의 유효성 검사
		/// </summary>
		protected override void OnValidateContent()
        {
            base.OnValidateContent();

            if (tabMain.SelectedTabPageIndex == 0)
            {
                if (grdProductList.View.GetCheckedRows().Rows.Count == 0)
                {
                    // 저장할 데이터가 존재하지 않습니다.
                    throw MessageException.Create("NoSaveData");
                }

                DataTable dt = grdProductList.View.GetCheckedRows();

                List<DataRow> listdr = dt.AsEnumerable().Where(c => Format.GetDouble(c.Field<object>("NOWYIELD"), 0) == 0 || Format.GetDouble(c.Field<object>("INPUTYIELD"), 0) == 0).ToList<DataRow>();

                if (listdr.Count > 0)
                {
                    throw MessageException.Create("NOTINPUTYIELD");
                }

                ValidateNumber(dt);
            }
            else if (tabMain.SelectedTabPageIndex == 1)
            {
                if (grdCalList.View.GetCheckedRows().Rows.Count == 0)
                {
                    // 저장할 데이터가 존재하지 않습니다.
                    throw MessageException.Create("NoSaveData");
                }
            }
        }

        private void ValidateNumber(DataTable dt)
        {
            string[] numColumns = {
                    "PCSPNL"
                ,   "PCSMM"
                ,   "UNITPRICE"
                ,   "NOTNPUTQTY"
                ,   "PLANQTY"
                ,   "POSUM"
                ,   "WIPQTY"
                ,   "NORMALWIP"
                ,   "RETURNLOT"
                ,   "MRBLOT"
                ,   "HOLDLOT"
                ,   "NOWYIELD"
                ,   "CHGOOD"
                ,   "WHQTY"
                ,   "WIPWHNOTINPUT"
                ,   "INPUTTARGET"
                ,   "INPUTYIELD"
                ,   "EXPECTINPUTPCS"
                ,   "EXPECTINPUTPNL"
                ,   "REALINPUTPCS"
                ,   "REALINPUTPNL"
                /*
                ,   "PCSINPUTRATE"
                ,   "PRICEINPUTRATE"
                ,   "INPUTPRICE"
                */
            };

            foreach(DataRow row in dt.Rows)
            {
                foreach(string col in numColumns)
                {
                    if (row[col] != DBNull.Value)
                    {
                        decimal temp;
                        bool parsed = decimal.TryParse(row[col].ToString(), out temp);
                        if (!parsed)
                        {
                            // 유효하지 않은 숫자입니다. {0}
                            throw MessageException.Create("InvalidNumber", string.Format("S/O={0}, LineNo={1}, Column={2}"
                                , row["PRODUCTIONORDERID"], row["LINENO"], Language.Get(col)));
                        }
                    }
                }
            }
        }

        #endregion

        #region ◆ Private Function |

        #endregion
    }
}
