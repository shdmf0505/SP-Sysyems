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
    /// 프 로 그 램 명  : 공정관리 > 투입관리 > 공정별 Roll Loss현황
    /// 업  무  설  명  : 공정별 Roll Loss 현황을 조회한다
    /// 생    성    자  : 한주석
    /// 생    성    일  : 2019-11-07
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class RollLossPerSegment : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가
        string _Text;                                       // 설명
        SmartBandedGrid grdList = new SmartBandedGrid();    // Main Grid

        #endregion

        #region 생성자

        public RollLossPerSegment()
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
        }

        /// <summary>        
        /// 공정별 Roll Loss현황 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
			#region 품목
			grdRollLossByProduct.GridButtonItem = GridButtonItem.Export;
			grdRollLossByProduct.View.SetIsReadOnly();

			var grditeminfo = grdRollLossByProduct.View.AddGroupColumn("");
			// 품목코드
			grditeminfo.AddTextBoxColumn("PRODUCTDEFID", 120);
			// 품목버전
			grditeminfo.AddTextBoxColumn("PRODUCTDEFVERSION", 90).SetTextAlignment(TextAlignment.Center);
			// 품목명
			grditeminfo.AddTextBoxColumn("PRODUCTDEFNAME", 250);


			var grditeminput = grdRollLossByProduct.View.AddGroupColumn("ASSIGNEDUNITS");
			// 투입량 ROLL
			grditeminput.AddSpinEditColumn("INPUTROLL", 70).SetLabel("ROLL").SetDisplayFormat("#,##0", MaskTypes.Numeric);
			// 투입량 PNL
			grditeminput.AddSpinEditColumn("INPUTPNL", 70).SetLabel("PNL").SetDisplayFormat("#,##0", MaskTypes.Numeric);
			// 투입량 M
			grditeminput.AddSpinEditColumn("INPUTM", 70).SetLabel("M").SetDisplayFormat("#,##0.#", MaskTypes.Numeric, true);


			var grditembasiclossroll = grdRollLossByProduct.View.AddGroupColumn("BASICLOSSROLL");
			// 기준LOSS/ROLL PNL
			grditembasiclossroll.AddSpinEditColumn("LOSSPNL", 70).SetLabel("PNL").SetDisplayFormat("#,##0", MaskTypes.Numeric);
			// 기준LOSS/ROLL M
			grditembasiclossroll.AddSpinEditColumn("LOSSM", 70).SetLabel("M").SetDisplayFormat("#,##0.###", MaskTypes.Numeric, true);


			var grditemadmitloss = grdRollLossByProduct.View.AddGroupColumn("ADMITLOSS");
			// 인정LOSS PNL
			grditemadmitloss.AddSpinEditColumn("RECOLOSSPNL", 70).SetLabel("PNL").SetDisplayFormat("#,##0", MaskTypes.Numeric);
			// 인정LOSS M
			grditemadmitloss.AddSpinEditColumn("RECOLOSSM", 70).SetLabel("M").SetDisplayFormat("#,##0.#", MaskTypes.Numeric, true);
			// 인정LOSS율
			grditemadmitloss.AddSpinEditColumn("RECOLOSSRATE", 70).SetLabel("LOSSRATE").SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true);

			var grditemdefectloss = grdRollLossByProduct.View.AddGroupColumn("ROLLLOSSDEFECTCODE");
			// ROLL LOSS(불량코드) PNL
			grditemdefectloss.AddSpinEditColumn("ROLLDEFECTPNL", 70).SetLabel("PNL").SetDisplayFormat("#,##0", MaskTypes.Numeric);
			// ROLL LOSS(불량코드) PCS
			grditemdefectloss.AddSpinEditColumn("ROLLDEFECTPCS", 70).SetLabel("PCS").SetDisplayFormat("#,##0", MaskTypes.Numeric);
			// ROLL LOSS(불량코드) M
			grditemdefectloss.AddSpinEditColumn("ROLLOSSDEFECTM", 70).SetLabel("M").SetDisplayFormat("#,##0.#", MaskTypes.Numeric, true);
			// ROLL LOSS(불량코드) 금액
			grditemdefectloss.AddSpinEditColumn("ROLLLOSSDEFECTPRICE", 70).SetLabel("WIPPRICE").SetDisplayFormat("#,##0", MaskTypes.Numeric);
			// ROLL LOSS(불량코드) LOSS율
			grditemdefectloss.AddSpinEditColumn("ROLLLOSSDEFECTRATE", 70).SetLabel("LOSSRATE").SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true);

			grditemdefectloss.AddSpinEditColumn("DIFLOSS", 100).SetLabel("OVERLOSS").SetDisplayFormat("#,##0", MaskTypes.Numeric);


			var grditemdefectetc = grdRollLossByProduct.View.AddGroupColumn("DEFECTETC");
			// 기타불량 PNL
			grditemdefectetc.AddSpinEditColumn("ETCDEFECTPNL", 70).SetLabel("PNL").SetDisplayFormat("#,##0", MaskTypes.Numeric);
			// 기타불량 PCS
			grditemdefectetc.AddSpinEditColumn("ETCDEFECTPCS", 70).SetLabel("PCS").SetDisplayFormat("#,##0", MaskTypes.Numeric);
			// 기타불량 M
			grditemdefectetc.AddSpinEditColumn("ETCM", 70).SetLabel("M").SetDisplayFormat("#,##0.#", MaskTypes.Numeric, true);
			// 기타불량 금액
			grditemdefectetc.AddSpinEditColumn("ETCLOSSPRICE", 70).SetLabel("WIPPRICE").SetDisplayFormat("#,##0", MaskTypes.Numeric);
			// 기타불량 LOSS율
			grditemdefectetc.AddSpinEditColumn("ETCLOSSRATE", 70).SetLabel("LOSSRATE").SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true);


			var grditemdefectroll = grdRollLossByProduct.View.AddGroupColumn("DEFECTROLL");
			// ROLL불량 PNL
			grditemdefectroll.AddSpinEditColumn("TOTALDEFECTPNL", 70).SetLabel("PNL").SetDisplayFormat("#,##0", MaskTypes.Numeric);
			// ROLL불량 PCS
			grditemdefectroll.AddSpinEditColumn("TOTALDEFECTPCS", 70).SetLabel("PCS").SetDisplayFormat("#,##0", MaskTypes.Numeric);
			// ROLL불량 M
			grditemdefectroll.AddSpinEditColumn("TOTALDEFECTM", 70).SetLabel("M").SetDisplayFormat("#,##0.#", MaskTypes.Numeric, true);
			// ROLL불량 금액
			grditemdefectroll.AddSpinEditColumn("TOTALDEFECTPRICE", 70).SetLabel("WIPPRICE").SetDisplayFormat("#,##0", MaskTypes.Numeric);
			// ROLL불량 LOSS율
			grditemdefectroll.AddSpinEditColumn("TOTALLOSSRATE", 70).SetLabel("LOSSRATE").SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true);


			grdRollLossByProduct.View.PopulateColumns();

			#endregion

			#region 공정
			grdRollLossBySegment.GridButtonItem = GridButtonItem.Export;
			grdRollLossBySegment.View.SetIsReadOnly();

			var grdinfo = grdRollLossBySegment.View.AddGroupColumn("");
            // 품목코드
            grdinfo.AddTextBoxColumn("PRODUCTDEFID", 120);
            // 품목버전
            grdinfo.AddTextBoxColumn("PRODUCTDEFVERSION", 60).SetTextAlignment(TextAlignment.Center);
            // 품목명
            grdinfo.AddTextBoxColumn("PRODUCTDEFNAME", 250);
              
            // 공정 ID
            grdinfo.AddTextBoxColumn("PROCESSSEGMENTID", 250).SetIsHidden();
            // 공정 명
            grdinfo.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
            // 공정순서
            grdinfo.AddTextBoxColumn("USERSEQUENCE", 60).SetTextAlignment(TextAlignment.Center);
   

            var grdinput = grdRollLossBySegment.View.AddGroupColumn("ASSIGNEDUNITS");
            // 투입량 ROLL
            grdinput.AddSpinEditColumn("INPUTROLL", 70).SetLabel("ROLL").SetDisplayFormat("#,##0", MaskTypes.Numeric);
            // 투입량 PNL
            grdinput.AddSpinEditColumn("INPUTPNL", 70).SetLabel("PNL").SetDisplayFormat("#,##0", MaskTypes.Numeric);
            // 투입량 M
            grdinput.AddSpinEditColumn("INPUTM", 70).SetLabel("M").SetDisplayFormat("#,##0.#", MaskTypes.Numeric, true);


            var grdbasiclossroll = grdRollLossBySegment.View.AddGroupColumn("BASICLOSSROLL");
            // 기준LOSS/ROLL PNL
            grdbasiclossroll.AddSpinEditColumn("LOSSPNL", 70).SetLabel("PNL").SetDisplayFormat("#,##0", MaskTypes.Numeric);
            // 기준LOSS/ROLL M
            grdbasiclossroll.AddSpinEditColumn("LOSSM", 70).SetLabel("M").SetDisplayFormat("#,##0.###", MaskTypes.Numeric, true);


			var grdadmitloss = grdRollLossBySegment.View.AddGroupColumn("ADMITLOSS");
            // 인정LOSS PNL
            grdadmitloss.AddSpinEditColumn("RECOLOSSPNL", 70).SetLabel("PNL").SetDisplayFormat("#,##0", MaskTypes.Numeric);
            // 인정LOSS M
            grdadmitloss.AddSpinEditColumn("RECOLOSSM", 70).SetLabel("M").SetDisplayFormat("#,##0.#", MaskTypes.Numeric, true);
			// 인정LOSS율
			grdadmitloss.AddSpinEditColumn("RECOLOSSRATE", 70).SetLabel("LOSSRATE").SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true);

			var grddefectloss = grdRollLossBySegment.View.AddGroupColumn("ROLLLOSSDEFECTCODE");
            // ROLL LOSS(불량코드) PNL
            grddefectloss.AddSpinEditColumn("ROLLDEFECTPNL", 70).SetLabel("PNL").SetDisplayFormat("#,##0", MaskTypes.Numeric);
            // ROLL LOSS(불량코드) PCS
            grddefectloss.AddSpinEditColumn("ROLLDEFECTPCS", 70).SetLabel("PCS").SetDisplayFormat("#,##0", MaskTypes.Numeric);
            // ROLL LOSS(불량코드) M
            grddefectloss.AddSpinEditColumn("ROLLOSSDEFECTM", 70).SetLabel("M").SetDisplayFormat("#,##0.#", MaskTypes.Numeric, true);
            // ROLL LOSS(불량코드) 금액
            grddefectloss.AddSpinEditColumn("ROLLLOSSDEFECTPRICE", 70).SetLabel("WIPPRICE").SetDisplayFormat("#,##0", MaskTypes.Numeric);
            // ROLL LOSS(불량코드) LOSS율
            grddefectloss.AddSpinEditColumn("ROLLLOSSDEFECTRATE", 70).SetLabel("LOSSRATE").SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true);

			grddefectloss.AddSpinEditColumn("DIFLOSS", 100).SetLabel("OVERLOSS").SetDisplayFormat("#,##0", MaskTypes.Numeric);


            var grddefectetc = grdRollLossBySegment.View.AddGroupColumn("DEFECTETC");
            // 기타불량 PNL
            grddefectetc.AddSpinEditColumn("ETCDEFECTPNL", 70).SetLabel("PNL").SetDisplayFormat("#,##0", MaskTypes.Numeric);
            // 기타불량 PCS
            grddefectetc.AddSpinEditColumn("ETCDEFECTPCS", 70).SetLabel("PCS").SetDisplayFormat("#,##0", MaskTypes.Numeric);
            // 기타불량 M
            grddefectetc.AddSpinEditColumn("ETCM", 70).SetLabel("M").SetDisplayFormat("#,##0.#", MaskTypes.Numeric, true);
            // 기타불량 금액
            grddefectetc.AddSpinEditColumn("ETCLOSSPRICE", 70).SetLabel("WIPPRICE").SetDisplayFormat("#,##0", MaskTypes.Numeric);
            // 기타불량 LOSS율
            grddefectetc.AddSpinEditColumn("ETCLOSSRATE", 70).SetLabel("LOSSRATE").SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true);


            var grddefectroll= grdRollLossBySegment.View.AddGroupColumn("DEFECTROLL");
            // ROLL불량 PNL
            grddefectroll.AddSpinEditColumn("TOTALDEFECTPNL", 70).SetLabel("PNL").SetDisplayFormat("#,##0", MaskTypes.Numeric);
            // ROLL불량 PCS
            grddefectroll.AddSpinEditColumn("TOTALDEFECTPCS", 70).SetLabel("PCS").SetDisplayFormat("#,##0", MaskTypes.Numeric);
            // ROLL불량 M
            grddefectroll.AddSpinEditColumn("TOTALDEFECTM", 70).SetLabel("M").SetDisplayFormat("#,##0.#", MaskTypes.Numeric, true);
            // ROLL불량 금액
            grddefectroll.AddSpinEditColumn("TOTALDEFECTPRICE", 70).SetLabel("WIPPRICE").SetDisplayFormat("#,##0", MaskTypes.Numeric);
            // ROLL불량 LOSS율
            grddefectroll.AddSpinEditColumn("TOTALLOSSRATE", 70).SetLabel("LOSSRATE").SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true);


            grdRollLossBySegment.View.PopulateColumns();

            #endregion
        }
        #endregion


        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            grdList.View.AddingNewRow += View_AddingNewRow;
        }

        /// <summary>
        /// 코드그룹 리스트 그리드에서 추가 버튼 클릭 시 호출
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            // TODO : 저장 Rule 변경
            DataTable changed = grdList.GetChangedRows();

            ExecuteRule("SaveCodeClass", changed);
        }

        #endregion

        #region 검색

        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            if (this.itemtab.SelectedTabPageIndex == 0)
            {
                DataTable dtrolllosslist = await SqlExecuter.QueryAsync("SelectRollLossStateByProduct", "10001", values);

                if (dtrolllosslist.Rows.Count < 1)
                {
                    ShowMessage("NoSelectData");
                }
				grdRollLossByProduct.DataSource = dtrolllosslist;


            }
            else
            {
				string queryVersion = values["P_INPUTSTATE"].Equals("Input") ? "10001" : "10002";

                DataTable dtrolllosslist2 = await SqlExecuter.QueryAsync("SelectRollLossStateBySegment", queryVersion, values);
                if (dtrolllosslist2.Rows.Count < 1)
                {
                    ShowMessage("NoSelectData");
                }
				grdRollLossBySegment.DataSource = dtrolllosslist2;

            }
        }
          
            
            

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();
            CommonFunction.AddConditionProductPopup("P_PRODUCTDEFID", 3.1, true, Conditions);
         
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
        }

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            // TODO : 유효성 로직 변경
            grdList.View.CheckValidation();

            DataTable changed = grdList.GetChangedRows();

            if (changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }

        #endregion

        #region Private Function

        // TODO : 화면에서 사용할 내부 함수 추가
        /// <summary>
        /// 마스터 정보를 조회한다.
        /// </summary>
        private void LoadData()
        {

        }

        #endregion

        private void smartBandedGrid1_Load(object sender, EventArgs e)
        {

        }
    }
}
