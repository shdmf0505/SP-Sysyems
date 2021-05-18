#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.Net.Data;
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

namespace Micube.SmartMES.OutsideOrderMgnt
{
    /// <summary>
    /// 프 로 그 램 명  :  외주관리 > 외주가공비마감 > 기타외주작업실적등록
    /// 업  무  설  명  :  기타 외주작업실적을 등록한다..
    /// 생    성    자  : choisstar
    /// 생    성    일  : 2019-06-13
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class OtherSubcontractingResultsConfirmed : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가
        private bool _blchang = false; //임시작업그리드 통제용 
        private string _strPlantid = "";
        private string _strOspVendorid = ""; //
        private bool _blSaveAuth = false;   //저장버튼 권한 체크 처리 
        #endregion

        #region 생성자

        public OtherSubcontractingResultsConfirmed()
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
            // TODO : 컨트롤 초기화 로직 구성
            _strPlantid = UserInfo.Current.Plant.ToString();
            InitializeComboBox();  // 콤보박스 셋팅 
            _blSaveAuth = true;
            InitializeEvent();    // 이벤트 

            // 기타외주작업 확정 목록 grid 셋팅
            InitializeOtherSubcontractingResultsConfirmedGrid();

            //기본 화면 lock 처리 
            controlEnableProcess(false);
        }

        #region ComboBox  

        /// <summary>
        /// SmartComboBox 컨트롤 추가
        /// </summary>
        private void InitializeComboBox()
        {
            // 작업구분값 정의 
            cboOspetctype.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboOspetctype.ValueMember = "CODEID";
            cboOspetctype.DisplayMember = "CODENAME";
            cboOspetctype.EditValue = "1";

            cboOspetctype.DataSource = SqlExecuter.Query("GetCodeList", "00001"
             , new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "CODECLASSID", "OSPEtcType" } });

            cboOspetctype.ShowHeader = false;
            //단위 콤보 값 저의
            cboUnit.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboUnit.ValueMember = "UOMDEFID";
            cboUnit.DisplayMember = "UOMDEFNAME";
            cboUnit.EditValue = "PCS";

            cboUnit.DataSource = SqlExecuter.Query("GetUomDefinitionMapListByOsp", "10001"
             , new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "UOMCATEGORY", "OSP" } });

            cboUnit.ShowHeader = false;
            // 양산구분값
            cboLotproducttype.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboLotproducttype.ValueMember = "CODEID";
            cboLotproducttype.DisplayMember = "CODENAME";
            //cboLotproducttype.EditValue = "1";    // 2021-03-15 오근영 주석처리 278건이 ct_ospetcwork.lotproducttype 컬럼에 1로 들어갔음.

            cboLotproducttype.DataSource = SqlExecuter.Query("GetCodeList", "00001"
              , new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "CODECLASSID", "ProductionType" } });

            cboLotproducttype.ShowHeader = false;
            //진행상태콤보값 정의
            cboOspetcprogressstatus.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboOspetcprogressstatus.ValueMember = "CODEID";
            cboOspetcprogressstatus.DisplayMember = "CODENAME";
            cboOspetcprogressstatus.EditValue = "1";
            cboOspetcprogressstatus.DataSource = SqlExecuter.Query("GetCodeList", "00001"
                , new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "CODECLASSID", "OSPEtcProgressStatus" } });

            cboOspetcprogressstatus.ShowHeader = false;
        }

        #endregion

        #region 외주작업확정 목록 리스트 그리
        /// <summary>        
        /// 외주작업 확정 목록 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeOtherSubcontractingResultsConfirmedGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdOtherSubcontractingResultsConfirmed.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Export | GridButtonItem.Restore;
            grdOtherSubcontractingResultsConfirmed.View.SetIsReadOnly();
            grdOtherSubcontractingResultsConfirmed.View.AddTextBoxColumn("ENTERPRISEID", 120)
                .SetIsHidden()
                .SetIsReadOnly();                                                          //  회사 ID
            grdOtherSubcontractingResultsConfirmed.View.AddTextBoxColumn("PLANTID", 120)
                .SetIsHidden()
                .SetIsReadOnly();                                                          //  공장 ID
            grdOtherSubcontractingResultsConfirmed.View.AddComboBoxColumn("OSPETCPROGRESSSTATUS", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OSPEtcProgressStatus", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsReadOnly();   // 진행상태
            grdOtherSubcontractingResultsConfirmed.View.AddTextBoxColumn("REQUESTNO", 120)
                .SetIsReadOnly();                // 의뢰번호
            grdOtherSubcontractingResultsConfirmed.View.AddComboBoxColumn("OSPETCTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OSPEtcType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsReadOnly();               //  작업구분           

            grdOtherSubcontractingResultsConfirmed.View.AddTextBoxColumn("CUSTOMERID", 120)
                .SetIsHidden();              //  고객사 ID
            grdOtherSubcontractingResultsConfirmed.View.AddTextBoxColumn("CUSTOMERNAME", 120);
            grdOtherSubcontractingResultsConfirmed.View.AddTextBoxColumn("AREAID", 120)
                .SetIsHidden();                 // AREAID
            grdOtherSubcontractingResultsConfirmed.View.AddTextBoxColumn("AREANAME", 120);             //  AREANAME 명//  고객사 명
            grdOtherSubcontractingResultsConfirmed.View.AddTextBoxColumn("OSPVENDORID", 120)
                .SetIsHidden();                //  협력사 ID
            grdOtherSubcontractingResultsConfirmed.View.AddTextBoxColumn("OSPVENDORNAME", 120);              //  협력사 명
            grdOtherSubcontractingResultsConfirmed.View.AddTextBoxColumn("REQUESTDEPARTMENT", 120)
                .SetIsHidden();   //    요청부서ID
            grdOtherSubcontractingResultsConfirmed.View.AddTextBoxColumn("OSPREQUESTUSER", 120)
                .SetIsHidden();                                                                      //    요청자ID

            grdOtherSubcontractingResultsConfirmed.View.AddComboBoxColumn("LOTPRODUCTTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));   //   양산구분
            grdOtherSubcontractingResultsConfirmed.View.AddTextBoxColumn("PRODUCTDEFID", 120);            //  제품 정의 ID
            grdOtherSubcontractingResultsConfirmed.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80);       //  제품 정의 Version
            grdOtherSubcontractingResultsConfirmed.View.AddTextBoxColumn("PRODUCTDEFNAME", 200);          //  제품명
            grdOtherSubcontractingResultsConfirmed.View.AddTextBoxColumn("PROCESSSEGMENTID", 120)
                .SetIsHidden();        //  공정 ID
            grdOtherSubcontractingResultsConfirmed.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 120);    //  공정명
            grdOtherSubcontractingResultsConfirmed.View.AddComboBoxColumn("UNIT", 60
                , new SqlQuery("GetUomDefinitionMapListByOsp", "10001", "UOMCATEGORY=OSP", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "UOMDEFID", "UOMDEFID"); //  단위

            grdOtherSubcontractingResultsConfirmed.View.AddSpinEditColumn("REQUESTQTY", 120)
                .SetDisplayFormat("#,##0.##", MaskTypes.Numeric);                                       // 의뢰수량
            grdOtherSubcontractingResultsConfirmed.View.AddSpinEditColumn("REQUESTPRICE", 120)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsHidden();                                        // 의뢰단가
            grdOtherSubcontractingResultsConfirmed.View.AddSpinEditColumn("REQUESTAMOUNT", 120)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsHidden();                                        // 의뢰금액
            grdOtherSubcontractingResultsConfirmed.View.AddTextBoxColumn("ISCHARGE", 80);                  // 유상여부 Check AddCheckBoxColumn
            grdOtherSubcontractingResultsConfirmed.View.AddTextBoxColumn("ISREQUESTDEFECTACCEPT", 80);    // 불량적용여부_의뢰
            grdOtherSubcontractingResultsConfirmed.View.AddTextBoxColumn("REQUESTDESCRIPTION", 200);       // 의뢰설명
            grdOtherSubcontractingResultsConfirmed.View.AddTextBoxColumn("OSPREQUESTUSERNAME", 120);           //    요청자명
            grdOtherSubcontractingResultsConfirmed.View.AddTextBoxColumn("REQUESTDATE", 120)
                .SetDisplayFormat("yyyy-MM-dd");                                                     //  의뢰일자

            grdOtherSubcontractingResultsConfirmed.View.AddTextBoxColumn("ETCLOTID", 150);                 // 기타작업_LOT_ID
            grdOtherSubcontractingResultsConfirmed.View.AddTextBoxColumn("ACTUALDATE", 120)
                .SetDisplayFormat("yyyy-MM-dd"); //   작업일자
            grdOtherSubcontractingResultsConfirmed.View.AddTextBoxColumn("ACTUALUSERNAME", 120); // 작업자
            grdOtherSubcontractingResultsConfirmed.View.AddSpinEditColumn("ACTUALQTY", 120)
                .SetDisplayFormat("#,##0.##", MaskTypes.Numeric);                                        // 수량_실적
            grdOtherSubcontractingResultsConfirmed.View.AddSpinEditColumn("ACTUALDEFECTQTY", 120)
                .SetDisplayFormat("#,##0.##", MaskTypes.Numeric);                                        //  불량수량_실적
            grdOtherSubcontractingResultsConfirmed.View.AddSpinEditColumn("ACTUALPRICE", 120)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);                                        //  단가_실적
            grdOtherSubcontractingResultsConfirmed.View.AddSpinEditColumn("ACTUALAMOUNT", 120)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);                                         // 금액_실적
            grdOtherSubcontractingResultsConfirmed.View.AddTextBoxColumn("ISACTUALDEFECTACCEPT", 80);       // 불량적용여부_실적
            grdOtherSubcontractingResultsConfirmed.View.AddTextBoxColumn("ACTUALUSER", 120)
                .SetIsHidden();                                                                        // 작업자

            grdOtherSubcontractingResultsConfirmed.View.AddTextBoxColumn("ACTUALDESCRIPTION", 200);          // 작업설명
            grdOtherSubcontractingResultsConfirmed.View.AddTextBoxColumn("APPROVALDATE", 120)
                .SetDisplayFormat("yyyy-MM-dd");                                                         // 승인일
            grdOtherSubcontractingResultsConfirmed.View.AddTextBoxColumn("APPROVALUSERNAME", 120);                                                                        // 승인자
            grdOtherSubcontractingResultsConfirmed.View.AddTextBoxColumn("ISAPPROVALDEFECTACCEP", 80)
                .SetIsHidden();       // 불량수량적용여부_승인
            grdOtherSubcontractingResultsConfirmed.View.AddTextBoxColumn("SETTLEUSER", 120)
                .SetIsHidden();                                                                          //확정자ID

            grdOtherSubcontractingResultsConfirmed.View.AddSpinEditColumn("SETTLEQTY", 120)
                .SetDisplayFormat("#,##0.##", MaskTypes.Numeric);          //수량_확정
            grdOtherSubcontractingResultsConfirmed.View.AddSpinEditColumn("SETTLEDEFECTQTY", 120)
                .SetDisplayFormat("#,##0.##", MaskTypes.Numeric);        //  불량수량_확정
            grdOtherSubcontractingResultsConfirmed.View.AddSpinEditColumn("SETTLEPRICE", 120)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);       //  단가_확정

            grdOtherSubcontractingResultsConfirmed.View.AddTextBoxColumn("SETTLEDATE", 120)
                .SetDisplayFormat("yyyy-MM-dd");                                         // 확정일자
            grdOtherSubcontractingResultsConfirmed.View.AddTextBoxColumn("SETTLEUSERNAME", 120);// 확정자ID

            grdOtherSubcontractingResultsConfirmed.View.AddTextBoxColumn("ISSETTLEDEFECTACCEPT", 80)
                .SetIsHidden();       //  불량수량적용여부_확정
            grdOtherSubcontractingResultsConfirmed.View.AddSpinEditColumn("SETTLEAMOUNT", 120)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsHidden();                                                                        //  금액_확정
            grdOtherSubcontractingResultsConfirmed.View.AddTextBoxColumn("SETTLEDESCRIPTION", 200);    //  확정설명

            grdOtherSubcontractingResultsConfirmed.View.AddTextBoxColumn("PERIODID", 80)
                .SetIsHidden();       // PERIODID
            grdOtherSubcontractingResultsConfirmed.View.PopulateColumns();

        }
        #endregion

        #region 외주작업의뢰 작업용 임시 그리드 초기화
        /// <summary>        
        /// 외주작업의뢰 목록 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializegOtherSubcontractWorkTempGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdWorkTemp.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Export | GridButtonItem.Restore;


            grdWorkTemp.View.AddTextBoxColumn("ENTERPRISEID", 120)
                .SetIsHidden();                                                          //  회사 ID
            grdWorkTemp.View.AddTextBoxColumn("PLANTID", 120)
                .SetIsHidden();                                                          //  공장 ID
            grdWorkTemp.View.AddComboBoxColumn("OSPETCPROGRESSSTATUS", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OSPEtcProgressStatus", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));  // 진행상태
            grdWorkTemp.View.AddTextBoxColumn("REQUESTNO", 120);              // 의뢰번호
            grdWorkTemp.View.AddComboBoxColumn("OSPETCTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OSPEtcType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")); //작업구분                
            grdWorkTemp.View.AddTextBoxColumn("CUSTOMERID", 120);             //  고객사 ID
            grdWorkTemp.View.AddTextBoxColumn("CUSTOMERNAME", 120);           //  고객사 명
            grdWorkTemp.View.AddTextBoxColumn("AREAID", 120);               // AREAID
            grdWorkTemp.View.AddTextBoxColumn("AREANAME", 120);             //  AREANAME 명
            grdWorkTemp.View.AddTextBoxColumn("OSPVENDORID", 120);               //  협력사 ID
            grdWorkTemp.View.AddTextBoxColumn("OSPVENDORNAME", 120);             //  협력사 명
            grdWorkTemp.View.AddTextBoxColumn("REQUESTDEPARTMENT", 120);     //    요청부서ID
            grdWorkTemp.View.AddTextBoxColumn("OSPREQUESTUSER", 120)
                .SetIsHidden();                                               //    요청자ID
            grdWorkTemp.View.AddTextBoxColumn("OSPREQUESTUSERNAME", 120);          //    요청자명
            grdWorkTemp.View.AddTextBoxColumn("REQUESTDATE", 120)
                .SetDisplayFormat("yyyy-MM-dd");                              //  의뢰일자
            grdWorkTemp.View.AddComboBoxColumn("LOTPRODUCTTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));   //   양산구분
            grdWorkTemp.View.AddTextBoxColumn("PRODUCTDEFID", 120);            //     제품 정의 ID
            grdWorkTemp.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80);        //    제품 정의 Version
            grdWorkTemp.View.AddTextBoxColumn("PRODUCTDEFNAME", 200);           //    제품명
            grdWorkTemp.View.AddTextBoxColumn("PROCESSSEGMENTID", 120);         //     공정 ID
            grdWorkTemp.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 120);     //     공정명
            grdWorkTemp.View.AddSpinEditColumn("REQUESTQTY", 120)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);                                         //   의뢰수량
            grdWorkTemp.View.AddComboBoxColumn("UNIT", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=Unit", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")); //  단위
            grdWorkTemp.View.AddSpinEditColumn("REQUESTPRICE", 120)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);                                          //  의뢰단가
            grdWorkTemp.View.AddSpinEditColumn("REQUESTAMOUNT", 120)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);                                         //  의뢰금액
            grdWorkTemp.View.AddCheckBoxColumn("ISCHARGE", 80);                  // 유상여부 Check
            grdWorkTemp.View.AddTextBoxColumn("REQUESTDESCRIPTION", 200);        //  의뢰설명
            grdWorkTemp.View.AddTextBoxColumn("ISREQUESTDEFECTACCEPT", 80);     // 불량적용여부_의뢰
            grdWorkTemp.View.AddTextBoxColumn("ETCLOTID", 150);                  //     기타작업_LOT_ID
            grdWorkTemp.View.AddSpinEditColumn("ACTUALQTY", 120)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);                                     //    수량_실적
            grdWorkTemp.View.AddSpinEditColumn("ACTUALDEFECTQTY", 120)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);                                   //  불량수량_실적
            grdWorkTemp.View.AddSpinEditColumn("ACTUALPRICE", 120)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);                                 //  단가_실적
            grdWorkTemp.View.AddSpinEditColumn("ACTUALAMOUNT", 120)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);                           //     금액_실적
            grdWorkTemp.View.AddTextBoxColumn("ISACTUALDEFECTACCEPT", 80); // 불량적용여부_실적
            grdWorkTemp.View.AddTextBoxColumn("ACTUALUSER", 120)
                .SetIsHidden();   //  작업자
            grdWorkTemp.View.AddTextBoxColumn("ACTUALDATE", 120)
                .SetDisplayFormat("yyyy-MM-dd"); //   작업일자
            grdWorkTemp.View.AddTextBoxColumn("ACTUALDESCRIPTION", 200);     //    작업설명
            grdWorkTemp.View.AddTextBoxColumn("APPROVALDATE", 120)
                .SetDisplayFormat("yyyy-MM-dd");                            //      승인일
            grdWorkTemp.View.AddTextBoxColumn("ISAPPROVALDEFECTACCEP", 80);  // 불량수량적용여부_승인
            grdWorkTemp.View.AddTextBoxColumn("SETTLEUSER", 120)
                .SetIsHidden();                                                                  //     확정자ID
            grdWorkTemp.View.AddTextBoxColumn("SETTLEDATE", 120)
                .SetDisplayFormat("yyyy-MM-dd");                                                //   확정일자
            grdWorkTemp.View.AddSpinEditColumn("SETTLEQTY", 120)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);                            //     수량_확정
            grdWorkTemp.View.AddSpinEditColumn("SETTLEDEFECTQTY", 120)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);                    //  불량수량_확정
            grdWorkTemp.View.AddSpinEditColumn("SETTLEPRICE", 120)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);                             //  단가_확정
            grdWorkTemp.View.AddTextBoxColumn("ISSETTLEDEFECTACCEPT", 80); //  불량수량적용여부_확정
            grdWorkTemp.View.AddSpinEditColumn("SETTLEAMOUNT", 120)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsHidden();   //     금액_확정
            grdWorkTemp.View.AddTextBoxColumn("SETTLEDESCRIPTION", 200)
                .SetIsHidden();  //    확정설명
            grdWorkTemp.View.AddTextBoxColumn("PERIODID", 80)
                .SetIsHidden();       // PERIODID
            grdWorkTemp.View.PopulateColumns();

        }
        #endregion

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            // grdList.View.AddingNewRow += View_AddingNewRow;
            // 버튼 이벤트 처리 
            btnSave.Click += BtnSave_Click;
            btnCancel.Click += BtnCancel_Click;

            grdOtherSubcontractingResultsConfirmed.View.FocusedRowChanged += View_FocusedRowChanged;
            // 그리드 클릭 이벤트 (작업실적부분을 대체예정)
            txtSettleqty.EditValueChanged += txtSettleqty_EditValueChanged;
            txtSettledefectqty.EditValueChanged += txtSettledefectqty_EditValueChanged;
            txtSettleprice.EditValueChanged += DetailControl_EditValueChanged;
            dtpSettledate.EditValueChanged += DetailControl_EditValueChanged;
            chkIssettledefectaccept.EditValueChanged += DetailControl_EditValueChanged;
           
        }
        /// <summary>
        /// 양품 수량 변경시 수량 변경 처리 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSettleqty_EditValueChanged(object sender, EventArgs e)
        {
            if (txtSettleqty.Enabled == false) return;
            // 작업실적 내역 관리 
            decimal decRequestqty = Convert.ToDecimal(txtRequestqty.EditValue);  //의뢰수량
            decimal decSettleqty = Convert.ToDecimal(((txtSettleqty.EditValue.ToString().Equals("")) ? 0 : txtSettleqty.EditValue)); //
            if (decSettleqty > decRequestqty)
            {
                txtSettleqty.EditValue = decRequestqty;
                decSettleqty = decRequestqty;
                //메세지 박스 추가 처리 
            }
            decimal decSettledefectqty = 0;//불량수량 
            decimal decSettleprice = Convert.ToDecimal(((txtSettleprice.EditValue.ToString().Equals("")) ? 0 : txtSettleprice.EditValue)); //
            decimal decSettleamount = 0;// 작업금액
            decSettledefectqty = decRequestqty - decSettleqty;
            txtSettledefectqty.EditValue = decSettledefectqty;

            if (chkIsactualdefectaccept.Checked)
            {
                decSettleamount = (decSettledefectqty + decSettleqty) * decSettleprice;
            }
            else
            {
                decSettleamount = decSettleqty * decSettleprice;

            }
            txtSettleamount.EditValue = decSettleamount;
        }
        /// <summary>
        ///  불량 수정 변경시 수정 처리 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSettledefectqty_EditValueChanged(object sender, EventArgs e)
        {
            // 작업실적 내역 관리 
            if (txtSettledefectqty.Enabled == false) return;
            decimal decRequestqty = Convert.ToDecimal(txtRequestqty.EditValue);  //의뢰수량
            decimal decSettledefectqty = Convert.ToDecimal(((txtSettledefectqty.EditValue.ToString().Equals("")) ? 0 : txtSettledefectqty.EditValue)); //
            decimal decSettleqty = Convert.ToDecimal(((txtSettleqty.EditValue.ToString().Equals("")) ? 0 : txtSettleqty.EditValue)); //
            if (decSettledefectqty > decRequestqty)
            {
                txtSettleqty.EditValue = decRequestqty;
                decSettledefectqty = decRequestqty;
                //메세지 박스 추가 처리 
            }
           
            decimal decSettleprice = Convert.ToDecimal(((txtSettleprice.EditValue.ToString().Equals("")) ? 0 : txtSettleprice.EditValue)); //
            decimal decSettleamount = 0;// 작업금액
            decSettleqty = decRequestqty - decSettledefectqty;
            txtSettleqty.EditValue = decSettleqty;
           
            if (chkIsactualdefectaccept.Checked)
            {
                decSettleamount = (decSettledefectqty + decSettleqty) * decSettleprice;
            }
            else
            {
                decSettleamount = decSettleqty * decSettleprice;

            }
            txtSettleamount.EditValue = decSettleamount;
        }
        /// <summary>
        /// 기타외주작업목록의 수정여부및 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailControl_EditValueChanged(object sender, EventArgs e)
         {

            if (_blchang == false) return;
            if (grdWorkTemp.View.DataRowCount != 1)
            {
                DataTable dt = createSaveDatatable();
                grdWorkTemp.DataSource = dt;
                grdWorkTemp.View.AddNewRow();
            }
            DataRow dr = grdWorkTemp.View.GetDataRow(grdWorkTemp.View.FocusedRowHandle);
            var values = Conditions.GetValues();
            string _strPlantid = values["P_PLANTID"].ToString();
            dr["ENTERPRISEID"] = UserInfo.Current.Enterprise.ToString();

            dr["PLANTID"] = _strPlantid;
            dr["REQUESTNO"] = txtRequestno.EditValue;
            dr["REQUESTDATE"] = "";
            dr["REQUESTDEPARTMENT"] = UserInfo.Current.Department.ToString();

            dr["OSPREQUESTUSER"] = UserInfo.Current.Plant.ToString();
            dr["OSPREQUESTUSERNAME"] = txtRequestorname.Text;
            dr["OSPETCTYPE"] = cboOspetctype.EditValue;
            dr["PRODUCTDEFID"] = usrProductdefid.CODE.EditValue;
            dr["PRODUCTDEFVERSION"] = usrProductdefid.VERSION.EditValue;

           
            dr["CUSTOMERNAME"] = txtCustomerid.Text;
          
            dr["LOTPRODUCTTYPE"] = cboLotproducttype.EditValue;
            dr["OSPVENDORID"] = _strOspVendorid;
            dr["OSPVENDORNAME"] = txtOspVendorName.EditValue;

            dr["REQUESTQTY"] = txtRequestqty.EditValue;
            dr["UNIT"] = cboUnit.EditValue;
            dr["REQUESTPRICE"] = txtRequestprice.EditValue;

            dr["ETCLOTID"] = txtEtclotid.EditValue;
            dr["REQUESTDESCRIPTION"] = txtRequestdescription.EditValue;
            dr["OSPETCPROGRESSSTATUS"] = cboOspetcprogressstatus.EditValue;

            //유무상여부 체크 박스값 셋팅 
            if (chkIscharge.Checked)
            {
                dr["ISCHARGE"] = "Y";

            }
            else
            {
                dr["ISCHARGE"] = "N";

            }
            dr["REQUESTAMOUNT"] = txtRequestamount.EditValue;

            //불량포함여부 체크박스값 셋팅 
            if (chkIsrequestdefectaccept.Checked)
            {
                dr["ISREQUESTDEFECTACCEPT"] = "Y";
            }
            else
            {
                dr["ISREQUESTDEFECTACCEPT"] = "N";
            }

          
            dr["ACTUALQTY"] = txtActualqty.EditValue;
            dr["ACTUALDEFECTQTY"] = txtActualdefectqty.EditValue;
            dr["ACTUALPRICE"] = txtActualqty.EditValue;
            //불량포함여부 체크박스값 셋팅 
            if (chkIsactualdefectaccept.Checked)
            {
                dr["ISACTUALDEFECTACCEPT"] = "Y";
              
            }
            else
            {
                dr["ISACTUALDEFECTACCEPT"] = "N";
              
            }
          
            dr["ACTUALAMOUNT"] = txtActualamount.EditValue;
            dr["ACTUALDESCRIPTION"] = txtActualdescription.EditValue;
            // 작업실적 내역 관리 
            decimal decRequestqty = Convert.ToDecimal(txtRequestqty.EditValue);  //의뢰수량
            decimal decSettleqty = Convert.ToDecimal(((txtSettleqty.EditValue.ToString().Equals("")) ? 0 : txtSettleqty.EditValue)); //
            if (decSettleqty > decRequestqty)
            {
                txtSettleqty.EditValue = decRequestqty;
                decSettleqty = decRequestqty;
                //메세지 박스 추가 처리 
            }
            decimal decSettledefectqty = 0;//불량수량 
            decimal decSettleprice = Convert.ToDecimal(((txtSettleprice.EditValue.ToString().Equals("")) ? 0 : txtSettleprice.EditValue)); //
            decimal decSettleamount = 0;// 작업금액
            decSettledefectqty = decRequestqty - decSettleqty;
            txtSettledefectqty.EditValue = decSettledefectqty;
            dr["SETTLEUSER"] = txtSettleuserid.EditValue;// 추후 수정 해야함. 
            dr["SETTLEDATE"] = dtpSettledate.EditValue;
            dr["SETTLEQTY"] = txtSettleqty.EditValue;
            dr["SETTLEDEFECTQTY"] = txtSettledefectqty.EditValue;
            dr["SETTLEPRICE"] = txtSettleprice.EditValue;
            if (chkIssettledefectaccept.Checked)
            {
                dr["ISSETTLEDEFECTACCEPT"] = "Y";
                decSettleamount = Math.Truncate((decSettledefectqty + decSettleqty) * decSettleprice);
            }
            else
            {
                dr["ISSETTLEDEFECTACCEPT"] = "N";
                decSettleamount = Math.Truncate(decSettleqty * decSettleprice);

            }
            dr["SETTLEAMOUNT"] = decSettleamount;
            txtSettleamount.EditValue = decSettleamount;

            dr["SETTLEDESCRIPTION"] = txtSettledescription.EditValue;
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

        /// <summary>
        /// 코드그룹 리스트 그리드의 포커스 행 변경 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            focusedRowChanged();
        }


        /// <summary>
        /// 저장 -기타 외주 작업 내역을 등록
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            string strRequestno = txtRequestno.Text;  // 추후 의뢰번호 Search용
            if (strRequestno.Equals("")) return;
            // 필수값 체크 로직 추가 처리 
            //1. 수량 체크 

            //2.수량체크(의뢰수량)
            // 상태값 체크 (의뢰 ,실적)
            DataRow drTemp = grdWorkTemp.View.GetDataRow(grdWorkTemp.View.FocusedRowHandle);
            string periodid = drTemp["PERIODID"].ToString();
            if (!(periodid.Equals("")))
            {
                this.ShowMessage(MessageBoxButtons.OK, "InValidOspData021"); //메세지 
                return;
            }
            DialogResult result = System.Windows.Forms.DialogResult.No;
           
            result = this.ShowMessage(MessageBoxButtons.YesNo, "OspDoProcessWant", btnSave.Text);//저장하시겠습니까? 

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    this.ShowWaitArea();
                    btnSave.Enabled = false;

                    btnCancel.Enabled = false;
                    //datatable 생성 
                    DataTable dt = createSaveDatatable();
                    _blchang = true;
                    DetailControl_EditValueChanged(null, null);
                    DataRow dr = grdWorkTemp.View.GetDataRow(grdWorkTemp.View.FocusedRowHandle);

                    dr["SETTLEUSER"] = UserInfo.Current.Id.ToString();
                    dr["_STATE_"] = "modified";
                    dr["VALIDSTATE"] = "Valid";
                    dt.ImportRow(dr);

                    this.ExecuteRule("OtherSubcontractingResultsConfirmed", dt);

                    ShowMessage("SuccessOspProcess");
                    //재조회하기..
                    OnSaveConfrimSearch();
                    //해당row 값 가져오기 
                    int irow = GetGridRowSearch(strRequestno);
                    if (irow >= 0)
                    {
                        grdOtherSubcontractingResultsConfirmed.View.FocusedRowHandle = irow;
                        grdOtherSubcontractingResultsConfirmed.View.SelectRow(irow);
                        focusedRowChanged();

                    }
                    else if (grdOtherSubcontractingResultsConfirmed.View.DataRowCount > 0)
                    {
                        grdOtherSubcontractingResultsConfirmed.View.FocusedRowHandle = 0;
                        grdOtherSubcontractingResultsConfirmed.View.SelectRow(0);
                        focusedRowChanged();
                    }
                    else
                    {
                        ClearEditValueControl();
                    }
                }
                catch (Exception ex)
                {
                    this.ShowError(ex);
                }
                finally
                {
                    this.CloseWaitArea();

                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;
                    
                }
            }
          
        }

        /// <summary>
        ///  취소 -기타 외주 작업 실적 취소 처리 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            string strRequestno = txtRequestno.Text; // 추후 의뢰번호 Search용
            if (strRequestno.Equals("")) return;
            DataRow drTemp = grdWorkTemp.View.GetDataRow(grdWorkTemp.View.FocusedRowHandle);
            string periodid = drTemp["PERIODID"].ToString();
            if (!(periodid.Equals("")))
            {
                this.ShowMessage(MessageBoxButtons.OK, "InValidOspData021"); //메세지 
                return;
            }
            DialogResult result = System.Windows.Forms.DialogResult.No;
            //string strRequestno = txtRequestno.Text; // 추후 의뢰번호 Search용
            result = this.ShowMessage(MessageBoxButtons.YesNo, "OspDoProcessWant", btnCancel.Text);// 확정 취소 처리 하시겠습니까?

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    this.ShowWaitArea();
                    btnCancel.Enabled = false;

                    btnSave.Enabled = false;
                    //삭제여부 메세지 포함 
                    DataTable dt = createSaveDatatable();

                    DataRow dr = grdWorkTemp.View.GetDataRow(grdWorkTemp.View.FocusedRowHandle);
                    dr["_STATE_"] = "deleted";
                    dr["VALIDSTATE"] = "Valid";

                    dt.ImportRow(dr);
                    this.ExecuteRule("OtherSubcontractingResultsConfirmed", dt);
                    ShowMessage("SuccessOspProcess"); //삭제 처리 하고 
                    //재조회하기..
                    OnSaveConfrimSearch();
                    //해당row 값 가져오기 
                    int irow = GetGridRowSearch(strRequestno);
                    if (irow >= 0)
                    {
                        grdOtherSubcontractingResultsConfirmed.View.FocusedRowHandle = irow;
                        grdOtherSubcontractingResultsConfirmed.View.SelectRow(irow);
                        focusedRowChanged();

                    }
                    else if (grdOtherSubcontractingResultsConfirmed.View.DataRowCount > 0)
                    {
                        grdOtherSubcontractingResultsConfirmed.View.FocusedRowHandle = 0;
                        grdOtherSubcontractingResultsConfirmed.View.SelectRow(0);
                        focusedRowChanged();
                    }
                    else
                    {
                        ClearEditValueControl();
                    }


                }
                catch (Exception ex)
                {
                    this.ShowError(ex);
                }
                finally
                {
                    this.CloseWaitArea();
                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;
                   
                }
            }
           
        }
        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            //  base.OnToolbarSaveClick();

            // TODO : 저장 Rule 변경
            //  DataTable changed = grdList.GetChangedRows();

            // ExecuteRule("SaveCodeClass", changed);
        }
        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            //base.OnToolbarClick(sender, e);
            SmartButton btn = sender as SmartButton;

            
            if (btn.Name.ToString().Equals("Confirmation"))
            {

                BtnSave_Click(null, null);
            }
            if (btn.Name.ToString().Equals("Cancel2"))
            {

                BtnCancel_Click(null, null);
            }

        }
        #endregion

        #region 검색

        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            // TODO : 조회 SP 변경

            var values = Conditions.GetValues();

            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            #region 품목코드 전환 처리 
            if (!(values["P_PRODUCTCODE"] == null))
            {
                string sproductcode = values["P_PRODUCTCODE"].ToString();
                // 품목코드값이 있으면
                if (!(sproductcode.Equals("")))
                {
                    string[] sproductd = sproductcode.Split('|');
                    // plant 정보 다시 가져오기 
                    values.Add("P_PRODUCTDEFID", sproductd[0].ToString());
                    values.Add("P_PRODUCTDEFVERSION", sproductd[1].ToString());
                }
            }
            #endregion

            #region 기간 검색형 전환 처리 
            if (!(values["P_REQUESTDATE_PERIODFR"].ToString().Equals("")))
            {
                DateTime requestDateFr = Convert.ToDateTime(values["P_REQUESTDATE_PERIODFR"]);
                values.Remove("P_REQUESTDATE_PERIODFR");
                values.Add("P_REQUESTDATE_PERIODFR", string.Format("{0:yyyy-MM-dd}", requestDateFr));
            }
            if (!(values["P_REQUESTDATE_PERIODTO"].ToString().Equals("")))
            {
                DateTime requestDateTo = Convert.ToDateTime(values["P_REQUESTDATE_PERIODTO"]);
                values.Remove("P_REQUESTDATE_PERIODTO");
                //requestDateTo = requestDateTo.AddDays(1);
                values.Add("P_REQUESTDATE_PERIODTO", string.Format("{0:yyyy-MM-dd}", requestDateTo));
            }

            if (!(values["P_ACTUALDATE_PERIODFR"].ToString().Equals("")))
            {
                DateTime actualDateFr = Convert.ToDateTime(values["P_ACTUALDATE_PERIODFR"]);
                values.Remove("P_ACTUALDATE_PERIODFR");
                values.Add("P_ACTUALDATE_PERIODFR", string.Format("{0:yyyy-MM-dd}", actualDateFr));
            }
            if (!(values["P_ACTUALDATE_PERIODTO"].ToString().Equals("")))
            {
                DateTime actualDateTo = Convert.ToDateTime(values["P_ACTUALDATE_PERIODTO"]);
                values.Remove("P_ACTUALDATE_PERIODTO");
                //actualDateTo = actualDateTo.AddDays(1);
                values.Add("P_ACTUALDATE_PERIODTO", string.Format("{0:yyyy-MM-dd}", actualDateTo));
            }
            if (!(values["P_APPROVALDATE_PERIODFR"].ToString().Equals("")))
            {
                DateTime approvalDateFr = Convert.ToDateTime(values["P_APPROVALDATE_PERIODFR"]);
                values.Remove("P_APPROVALDATE_PERIODFR");
                values.Add("P_APPROVALDATE_PERIODFR", string.Format("{0:yyyy-MM-dd}", approvalDateFr));
            }
            if (!(values["P_APPROVALDATE_PERIODTO"].ToString().Equals("")))
            {
                DateTime approvalDateTo = Convert.ToDateTime(values["P_APPROVALDATE_PERIODTO"]);
                values.Remove("P_APPROVALDATE_PERIODTO");
                //approvalDateTo = approvalDateTo.AddDays(1);
                values.Add("P_APPROVALDATE_PERIODTO", string.Format("{0:yyyy-MM-dd}", approvalDateTo));
            }
            if (!(values["P_SETTLEDATE_PERIODFR"].ToString().Equals("")))
            {
                DateTime settleDateFr = Convert.ToDateTime(values["P_SETTLEDATE_PERIODFR"]);
                values.Remove("P_SETTLEDATE_PERIODFR");
                values.Add("P_SETTLEDATE_PERIODFR", string.Format("{0:yyyy-MM-dd}", settleDateFr));
            }
            if (!(values["P_SETTLEDATE_PERIODTO"].ToString().Equals("")))
            {
                DateTime settleDateTo = Convert.ToDateTime(values["P_SETTLEDATE_PERIODTO"]);
                values.Remove("P_SETTLEDATE_PERIODTO");
                values.Add("P_SETTLEDATE_PERIODTO", string.Format("{0:yyyy-MM-dd}", settleDateTo));
            }
            #endregion

            _strPlantid = values["P_PLANTID"].ToString();
            //컨트롤 초기화
            ClearEditValueControl();
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable dt = await SqlExecuter.QueryAsync("GetOtherSubcontractingResultsConfirmed", "10001", values);

            if (dt.Rows.Count < 1)
            {
                // 조회할 데이터가 없습니다.
                ShowMessage("NoSelectData");

            }
            grdOtherSubcontractingResultsConfirmed.DataSource = dt;
            if (dt.Rows.Count > 0)
            {
                grdOtherSubcontractingResultsConfirmed.View.FocusedRowHandle = 0;
                grdOtherSubcontractingResultsConfirmed.View.SelectRow(0);
                focusedRowChanged();
            }
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            //InitializeConditionPopup_Plant();
            // 작업자 
            InitializeConditionPopup_Requestor();

            InitializeConditionPopup_OspAreaid();
            // 작업업체 
            InitializeConditionPopup_OspVendorid();
            // 진행상태
            InitializeConditionPopup_Ospetcprogressstatus();
            // 품목코드 
            InitializeConditionPopup_ProductDefId();
        }
        /// <summary>
        /// site 설정 
        /// </summary>
        private void InitializeConditionPopup_Plant()
        {
            var planttxtbox = Conditions.AddComboBox("p_plantid", new SqlQuery("GetPlantList", "00001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PLANTID", "PLANTID")
              .SetLabel("PLANT")
              .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false)
              .SetPosition(0.1)
              .SetDefault(UserInfo.Current.Plant, "p_plantId") //기본값 설정 UserInfo.Current.Plant
              .SetIsReadOnly(true);
        }

        /// <summary>
        /// 의뢰자 조회조건
        /// </summary>
        private void InitializeConditionPopup_Requestor()
        {
            // 팝업 컬럼설정
            var requesterPopupColumn = Conditions.AddSelectPopup("p_requestuser", new SqlQuery("GetRequestorListByOsp", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "REQUESTUSERNAME", "REQUESTUSER")
               .SetPopupLayout("OSPREQUESTUSER", PopupButtonStyles.Ok_Cancel, true, false)
               .SetPopupLayoutForm(500, 600)
               .SetLabel("OSPREQUESTUSER")
               .SetPopupResultCount(1)
               .SetRelationIds("p_plantid")
               .SetPosition(2.2);
            requesterPopupColumn.Conditions.AddTextBox("REQUESTUSERNAME")
                         .SetLabel("OSPREQUESTUSERNAME");

            // 팝업 그리드
            requesterPopupColumn.GridColumns.AddTextBoxColumn("REQUESTUSER", 150)
                .SetLabel("OSPREQUESTUSER")
                .SetValidationKeyColumn();
            requesterPopupColumn.GridColumns.AddTextBoxColumn("REQUESTUSERNAME", 200)
                .SetLabel("OSPREQUESTUSERNAME");
            //// 팝업 조회조건
            //requesterPopupColumn.Conditions.AddTextBox("OSPREQUESTUSERNAME")
            //    .SetLabel("OSPREQUESTUSERNAME");

            //// 팝업 그리드
            //requesterPopupColumn.GridColumns.AddTextBoxColumn("OSPREQUESTUSER", 150)
            //    .SetValidationKeyColumn();
            //requesterPopupColumn.GridColumns.AddTextBoxColumn("OSPREQUESTUSERNAME", 200);
        }
        /// <summary>
        /// 작업장
        /// </summary>
        private void InitializeConditionPopup_OspAreaid()
        {
            // 팝업 컬럼설정
            var popupProduct = Conditions.AddSelectPopup("p_areaid",
                                                                new SqlQuery("GetAreaidPopupListByOsp", "10001"
                                                                                , $"LANGUAGETYPE={ UserInfo.Current.LanguageType }"
                                                                                , $"USERID={UserInfo.Current.Id}"
                                                                                , $"ENTERPRISEID={UserInfo.Current.Enterprise}"
                                                                                ), "AREANAME", "AREAID")
             .SetPopupLayout("AREAID", PopupButtonStyles.Ok_Cancel, true, false)
             .SetPopupLayoutForm(450, 600)
             .SetLabel("AREANAME")
             .SetPopupResultCount(1)
             .SetRelationIds("p_plantid")
                .SetPosition(2.3);
            // 팝업 조회조건
            popupProduct.Conditions.AddTextBox("P_AREANAME")
                .SetLabel("AREANAME");

            // 팝업 그리드
            popupProduct.GridColumns.AddTextBoxColumn("AREAID", 120);
            popupProduct.GridColumns.AddTextBoxColumn("AREANAME", 200);


        }
        /// <summary>
        /// 작업업체 .고객 조회조건
        /// </summary>
        private void InitializeConditionPopup_OspVendorid()
        {
            // 팝업 컬럼설정
            var vendoridPopupColumn = Conditions.AddSelectPopup("p_ospvendorid", new SqlQuery("GetVendorListAuthorityByOsp", "10001"
                                                     , $"ENTERPRISEID={UserInfo.Current.Enterprise}"
                                                     , $"USERID={UserInfo.Current.Id}"
                                                     ), "OSPVENDORNAME", "OSPVENDORID")
               .SetPopupLayout("OSPVENDORID", PopupButtonStyles.Ok_Cancel, true, false)
               .SetPopupLayoutForm(500, 600)
               .SetLabel("OSPVENDORID")
               .SetPopupResultCount(1)
               .SetRelationIds("p_plantid", "p_areaid")
               .SetPosition(2.4);

            // 팝업 조회조건
            vendoridPopupColumn.Conditions.AddTextBox("OSPVENDORNAME")
                .SetLabel("OSPVENDORNAME");

            // 팝업 그리드
            vendoridPopupColumn.GridColumns.AddTextBoxColumn("OSPVENDORID", 150)
                .SetValidationKeyColumn();
            vendoridPopupColumn.GridColumns.AddTextBoxColumn("OSPVENDORNAME", 200);

            var txtOspVendor = Conditions.AddTextBox("P_OSPVENDORNAME")
               .SetLabel("OSPVENDORNAME")
               .SetPosition(2.5);

        }
        /// <summary>
        /// 진행상태
        /// </summary>
        private void InitializeConditionPopup_Ospetcprogressstatus()
        {

            var Ospetcprogressstatuscbobox = Conditions.AddComboBox("p_ospetcprogressstatus", new SqlQuery("GetCodeList", "00001", "CODECLASSID=OSPEtcProgressStatus", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
               .SetLabel("OSPETCPROGRESSSTATUS")
               // .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false)
               .SetPosition(4.5)
               .SetEmptyItem("", "")
               .SetDefault("Approval") //기본값 설정 UserInfo.Current.Plant

            ;
            //   .SetIsReadOnly(true);

        }
        /// <summary>
        /// 품목코드 조회조건
        /// </summary>
        private void InitializeConditionPopup_ProductDefId()
        {
            var popupProduct = Conditions.AddSelectPopup("p_productcode",
                                                                new SqlQuery("GetProductdefidlistByOsp", "10001"
                                                                                , $"LANGUAGETYPE={ UserInfo.Current.LanguageType }"
                                                                                , $"ENTERPRISEID={UserInfo.Current.Enterprise}"
                                                                                ), "PRODUCTDEFNAME", "PRODUCTDEFCODE")
             .SetPopupLayout("PRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)
             .SetPopupLayoutForm(650, 600)
             .SetLabel("PRODUCTDEFID")
             .SetPopupResultCount(1)
             .SetPosition(5.1);

            // 팝업 조회조건
            popupProduct.Conditions.AddComboBox("P_PRODUCTDEFTYPE", new SqlQuery("GetCodeListByOspProductdeftype", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("PRODUCTDEFTYPE")
                .SetDefault("Product");
            popupProduct.Conditions.AddTextBox("PRODUCTDEFNAME")
                .SetLabel("PRODUCTDEFID");

            // 팝업 그리드
            popupProduct.GridColumns.AddTextBoxColumn("PRODUCTDEFCODE", 150)
                .SetIsHidden();
            popupProduct.GridColumns.AddComboBoxColumn("PRODUCTDEFTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductDefType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsReadOnly();
            popupProduct.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150).SetIsReadOnly();
            popupProduct.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetIsReadOnly();
            popupProduct.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200).SetIsReadOnly();

            var txtProductName = Conditions.AddTextBox("P_PRODUCTDEFNAME")
                .SetLabel("PRODUCTDEFNAME")
                .SetPosition(5.2);
        }
        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
            // 기간 포맷 용
            InitializeDatePeriod();
            SmartComboBox cboPlaint = Conditions.GetControl<SmartComboBox>("p_plantid");
            cboPlaint.EditValueChanged += cboPlaint_EditValueChanged;
        }
        private void cboPlaint_EditValueChanged(object sender, EventArgs e)
        {
            var values = Conditions.GetValues();

            _strPlantid = values["P_PLANTID"].ToString();

            ClearEditValueControl();
            if (btnSave.Enabled == false)
            {
                _blSaveAuth = false;
            }
            else
            {
                _blSaveAuth = true;
            }
        }
        /// <summary>
        /// 기간 포맷 재정의 
        /// </summary>
        private void InitializeDatePeriod()
        {

            InitializeDatePeriodSetting("P_REQUESTDATE");

            InitializeDatePeriodSetting("P_ACTUALDATE");

            InitializeDatePeriodSetting("P_SETTLEDATE");

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

        }

        #endregion

        #region Private Function
        /// <summary>
        /// 그리드의 포커스 행 변경시 행의 작업목록의 내용을 매핑처리 함.
        /// </summary>
        private void focusedRowChanged()
        {
            //포커스 행 체크 
            if (grdOtherSubcontractingResultsConfirmed.View.FocusedRowHandle < 0) return;

            var row = grdOtherSubcontractingResultsConfirmed.View.GetDataRow(grdOtherSubcontractingResultsConfirmed.View.FocusedRowHandle);
            _blchang = false;
            txtRequestno.EditValue = row["REQUESTNO"].ToString();
            DateTime dateRequestdat = Convert.ToDateTime(row["REQUESTDATE"].ToString());
            dtpRequestdate.EditValue = dateRequestdat.ToString("yyyy-MM-dd"); 
            txtRequestdepartment.EditValue = row["REQUESTDEPARTMENT"].ToString();
            txtRequestorname.EditValue = row["OSPREQUESTUSERNAME"].ToString();
            cboOspetctype.EditValue = row["OSPETCTYPE"].ToString();

            usrProductdefid.CODE.EditValue = row["PRODUCTDEFID"].ToString();
            usrProductdefid.VERSION.EditValue = row["PRODUCTDEFVERSION"].ToString();
            usrProductdefid.NAME.EditValue = row["PRODUCTDEFNAME"].ToString();

            txtCustomerid.EditValue = row["CUSTOMERNAME"].ToString();
            txtProcesssegmentid.EditValue = row["PROCESSSEGMENTNAME"].ToString();
            cboLotproducttype.EditValue = row["LOTPRODUCTTYPE"].ToString();
            txtOspAreaid.Text = row["AREANAME"].ToString();
            txtOspVendorName.EditValue = row["OSPVENDORNAME"].ToString();
            if (row["ISCHARGE"].ToString().Equals("Y"))
            {
                chkIscharge.Checked = true;
            }
            else
            {
                chkIscharge.Checked = false;
            }
            if (row["ISREQUESTDEFECTACCEPT"].ToString().Equals("Y"))
            {
                chkIsrequestdefectaccept.Checked = true;
            }
            else
            {
                chkIsrequestdefectaccept.Checked = false;
            }

            txtRequestqty.EditValue = row["REQUESTQTY"].ToString();
            cboUnit.EditValue = row["UNIT"].ToString();
            txtRequestprice.EditValue = row["REQUESTPRICE"].ToString();
            txtRequestamount.EditValue = row["REQUESTAMOUNT"].ToString();
            txtEtclotid.EditValue = row["ETCLOTID"].ToString();
            txtRequestdescription.EditValue = row["REQUESTDESCRIPTION"].ToString();
            cboOspetcprogressstatus.EditValue = row["OSPETCPROGRESSSTATUS"].ToString();

            txtActualqty.EditValue = row["ACTUALQTY"].ToString();
            txtActualdefectqty.EditValue = row["ACTUALDEFECTQTY"].ToString();
            txtActualprice.EditValue = row["ACTUALPRICE"].ToString();
            txtActualamount.EditValue = row["ACTUALAMOUNT"].ToString();

            if (row["ISACTUALDEFECTACCEPT"].ToString().Equals("Y"))
            {
                chkIsactualdefectaccept.Checked = true;
            }
            else
            {
                chkIsactualdefectaccept.Checked = false;
            }
            txtActualuser.EditValue = row["ACTUALUSERNAME"].ToString();
           // 실적일자 셋팅
            if (row["ACTUALDATE"].ToString().Equals("") || row["ACTUALDATE"].ToString().Equals(null))
            {
                dtpActualdate.EditValue = "";
            }
            else
            {
                DateTime dateActualdate = Convert.ToDateTime(row["ACTUALDATE"].ToString());
                dtpActualdate.EditValue = dateActualdate.ToString("yyyy-MM-dd"); 
            }
            txtActualdescription.EditValue = row["ACTUALDESCRIPTION"].ToString();
            
           
           
            // 승인일자
            if (row["APPROVALDATE"].ToString().Equals("") || row["APPROVALDATE"].ToString().Equals(null))
            {
                dtpApprovaldate.EditValue = "";
            }
            else
            {
                DateTime dateActualdate = Convert.ToDateTime(row["APPROVALDATE"].ToString());
                dtpApprovaldate.EditValue = dateActualdate.ToString("yyyy-MM-dd"); 
            }
            txtApprovaluserid.EditValue = row["APPROVALUSERNAME"].ToString();
            // 확정일자 셋팅
            if (row["SETTLEDATE"].ToString().Equals("") || row["SETTLEDATE"].ToString().Equals(null))
            {
                dtpSettledate.EditValue = null;
                if (row["OSPETCPROGRESSSTATUS"].ToString().Equals("Approval"))
                {
                    txtSettleqty.EditValue = row["ACTUALQTY"].ToString();
                    txtSettledefectqty.EditValue = row["ACTUALDEFECTQTY"].ToString();

                    txtSettleprice.EditValue = row["ACTUALPRICE"].ToString();
                    txtSettleamount.EditValue = row["ACTUALAMOUNT"].ToString();
                    if (row["ISACTUALDEFECTACCEPT"].ToString().Equals("Y"))
                    {
                        chkIssettledefectaccept.Checked = true;
                    }
                    else
                    {
                        chkIssettledefectaccept.Checked = false;
                    }
                }
                else
                {
                    txtSettleqty.EditValue =0D;
                    txtSettledefectqty.EditValue = 0D;
                    txtSettleprice.EditValue = 0;
                    txtSettleamount.EditValue =0;
                }
            }
            else
            {
                DateTime dateSettledate      = Convert.ToDateTime(row["SETTLEDATE"].ToString());
                dtpSettledate.EditValue      = dateSettledate.ToString("yyyy-MM-dd");
                txtSettleqty.EditValue       = row["SETTLEQTY"].ToString();
               
                txtSettledefectqty.EditValue = row["SETTLEDEFECTQTY"].ToString();
                txtSettleprice.EditValue     = row["SETTLEPRICE"].ToString();
                txtSettleamount.EditValue    = row["SETTLEAMOUNT"].ToString();
                if (row["ISSETTLEDEFECTACCEPT"].ToString().Equals("Y"))
                {
                    chkIssettledefectaccept.Checked = true;
                }
                else
                {
                    chkIssettledefectaccept.Checked = false;
                }
            }
         
            txtSettledescription.EditValue = row["SETTLEDESCRIPTION"].ToString();
            DataTable dtWorkTemp = createSaveDatatable();
            dtWorkTemp.ImportRow(row);
            grdWorkTemp.DataSource = dtWorkTemp;
            // 진행상태값 확정 Settle 4 , 승인일때 수정 Approval  3불가
            if (row["OSPETCPROGRESSSTATUS"].ToString().Equals("Approval") || row["OSPETCPROGRESSSTATUS"].ToString().Equals("Settle"))
            {
                controlEnableProcess(true);

                if (row["OSPETCPROGRESSSTATUS"].ToString().Equals("Approval"))
                {
                    btnCancel.Enabled = false;
                }
            }
            else
            {
                controlEnableProcess(false);
                
            }
            _blchang = true;
        }

        /// <summary>
        /// 저장 후 재조회용 
        /// </summary>

        private void OnSaveConfrimSearch()
        {
           
            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            #region 품목코드 전환 처리 
            if (!(values["P_PRODUCTCODE"] == null))
            {
                string sproductcode = values["P_PRODUCTCODE"].ToString();
                // 품목코드값이 있으면
                if (!(sproductcode.Equals("")))
                {
                    string[] sproductd = sproductcode.Split('|');
                    // plant 정보 다시 가져오기 
                    values.Add("P_PRODUCTDEFID", sproductd[0].ToString());
                    values.Add("P_PRODUCTDEFVERSION", sproductd[1].ToString());
                }
            }
            #endregion

            #region 기간 검색형 전환 처리 
            if (!(values["P_REQUESTDATE_PERIODFR"].ToString().Equals("")))
            {
                DateTime requestDateFr = Convert.ToDateTime(values["P_REQUESTDATE_PERIODFR"]);
                values.Remove("P_REQUESTDATE_PERIODFR");
                values.Add("P_REQUESTDATE_PERIODFR", string.Format("{0:yyyy-MM-dd}", requestDateFr));
            }
            if (!(values["P_REQUESTDATE_PERIODTO"].ToString().Equals("")))
            {
                DateTime requestDateTo = Convert.ToDateTime(values["P_REQUESTDATE_PERIODTO"]);
                values.Remove("P_REQUESTDATE_PERIODTO");
                //requestDateTo = requestDateTo.AddDays(1);
                values.Add("P_REQUESTDATE_PERIODTO", string.Format("{0:yyyy-MM-dd}", requestDateTo));
            }

            if (!(values["P_ACTUALDATE_PERIODFR"].ToString().Equals("")))
            {
                DateTime actualDateFr = Convert.ToDateTime(values["P_ACTUALDATE_PERIODFR"]);
                values.Remove("P_ACTUALDATE_PERIODFR");
                values.Add("P_ACTUALDATE_PERIODFR", string.Format("{0:yyyy-MM-dd}", actualDateFr));
            }
            if (!(values["P_ACTUALDATE_PERIODTO"].ToString().Equals("")))
            {
                DateTime actualDateTo = Convert.ToDateTime(values["P_ACTUALDATE_PERIODTO"]);
                values.Remove("P_ACTUALDATE_PERIODTO");
               // actualDateTo = actualDateTo.AddDays(1);
                values.Add("P_ACTUALDATE_PERIODTO", string.Format("{0:yyyy-MM-dd}", actualDateTo));
            }
            if (!(values["P_APPROVALDATE_PERIODFR"].ToString().Equals("")))
            {
                DateTime approvalDateFr = Convert.ToDateTime(values["P_APPROVALDATE_PERIODFR"]);
                values.Remove("P_APPROVALDATE_PERIODFR");
                values.Add("P_APPROVALDATE_PERIODFR", string.Format("{0:yyyy-MM-dd}", approvalDateFr));
            }
            if (!(values["P_APPROVALDATE_PERIODTO"].ToString().Equals("")))
            {
                DateTime approvalDateTo = Convert.ToDateTime(values["P_APPROVALDATE_PERIODTO"]);
                values.Remove("P_APPROVALDATE_PERIODTO");
               // approvalDateTo = approvalDateTo.AddDays(1);
                values.Add("P_APPROVALDATE_PERIODTO", string.Format("{0:yyyy-MM-dd}", approvalDateTo));
            }
            if (!(values["P_SETTLEDATE_PERIODFR"].ToString().Equals("")))
            {
                DateTime settleDateFr = Convert.ToDateTime(values["P_SETTLEDATE_PERIODFR"]);
                values.Remove("P_SETTLEDATE_PERIODFR");
                values.Add("P_SETTLEDATE_PERIODFR", string.Format("{0:yyyy-MM-dd}", settleDateFr));
            }
            if (!(values["P_SETTLEDATE_PERIODTO"].ToString().Equals("")))
            {
                DateTime settleDateTo = Convert.ToDateTime(values["P_SETTLEDATE_PERIODTO"]);
                values.Remove("P_SETTLEDATE_PERIODTO");
                values.Add("P_SETTLEDATE_PERIODTO", string.Format("{0:yyyy-MM-dd}", settleDateTo));
            }
            #endregion

            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable dt = SqlExecuter.Query("GetOtherSubcontractingResultsConfirmed", "10001", values);

            grdOtherSubcontractingResultsConfirmed.DataSource = dt;

            
        }
     
        /// <summary>
        /// 그리드 이동에 필요한 row 찾기
        /// </summary>
        /// <param name="strRequestno"></param>
        private int GetGridRowSearch(string strRequestno)
        {
            int iRow = -1;
            if (grdOtherSubcontractingResultsConfirmed.View.DataRowCount == 0)
            {
                return iRow;
            }
            for (int i = 0; i < grdOtherSubcontractingResultsConfirmed.View.DataRowCount; i++)
            {
                if (grdOtherSubcontractingResultsConfirmed.View.GetRowCellValue(i, "REQUESTNO").ToString().Equals(strRequestno))
                {
                    return i;
                }
            }
            return iRow;
        }
        /// <summary>
        /// 진행상태값에 따른 입력 항목 lock 처리
        /// </summary>
        /// <param name="blProcess"></param>
        private void controlEnableProcess(bool blProcess)
        {
            dtpRequestdate.Enabled = false;
            dtpActualdate.Enabled = false;
            dtpApprovaldate.Enabled = false;
            dtpSettledate.Enabled = blProcess;
            txtSettleqty.Enabled = blProcess;
            txtSettledefectqty.Enabled = blProcess;
            chkIssettledefectaccept.Enabled = blProcess;
            txtSettleprice.Enabled = blProcess;
            txtSettledescription.Enabled = blProcess;
            if (_blSaveAuth == true)
            {
                btnCancel.Enabled = blProcess;
                btnSave.Enabled = blProcess;
            }
        }

        /// <summary>
        /// 저장및  삭제용 data table 생성 
        /// </summary>
        /// <returns></returns>
        private DataTable createSaveDatatable()
        {
            DataTable dt = new DataTable();
            dt.TableName = "list";
            dt.Columns.Add("ENTERPRISEID");
            dt.Columns.Add("PLANTID");
            dt.Columns.Add("REQUESTNO");
            dt.Columns.Add("REQUESTDATE");
            dt.Columns.Add("REQUESTDEPARTMENT");

            dt.Columns.Add("OSPREQUESTUSER");
            dt.Columns.Add("OSPREQUESTUSERNAME");
            dt.Columns.Add("OSPETCTYPE");
            dt.Columns.Add("PRODUCTDEFID");
            dt.Columns.Add("PRODUCTDEFVERSION");
            dt.Columns.Add("PRODUCTDEFNAME");
            dt.Columns.Add("CUSTOMERID");
            dt.Columns.Add("CUSTOMERNAME");
            dt.Columns.Add("PROCESSSEGMENTID");
            dt.Columns.Add("PROCESSSEGMENTNAME");
            dt.Columns.Add("LOTPRODUCTTYPE");
            dt.Columns.Add("OSPVENDORID");
            dt.Columns.Add("OSPVENDORNAME");
            dt.Columns.Add("ISCHARGE");
            dt.Columns.Add("ISREQUESTDEFECTACCEPT");
            dt.Columns.Add("REQUESTQTY");
            dt.Columns.Add("UNIT");
            dt.Columns.Add("REQUESTPRICE");
            dt.Columns.Add("REQUESTAMOUNT");
            dt.Columns.Add("ETCLOTID");
            dt.Columns.Add("REQUESTDESCRIPTION");
            dt.Columns.Add("OSPETCPROGRESSSTATUS");
            dt.Columns.Add("VALIDSTATE");
            dt.Columns.Add("PERIODID");
            dt.Columns.Add("ACTUALQTY");
            dt.Columns.Add("ACTUALDEFECTQTY");
            dt.Columns.Add("ACTUALPRICE");
            dt.Columns.Add("ACTUALAMOUNT");
            dt.Columns.Add("ISACTUALDEFECTACCEPT");
            dt.Columns.Add("ACTUALUSER");
            dt.Columns.Add("ACTUALDATE");
            dt.Columns.Add("ACTUALDESCRIPTION");
            dt.Columns.Add("SETTLEUSER");
            dt.Columns.Add("SETTLEDATE");
            dt.Columns.Add("SETTLEQTY");
            dt.Columns.Add("SETTLEDEFECTQTY");
            dt.Columns.Add("SETTLEPRICE");
            dt.Columns.Add("ISSETTLEDEFECTACCEPT");
            dt.Columns.Add("SETTLEAMOUNT");
            dt.Columns.Add("SETTLEDESCRIPTION");

            dt.Columns.Add("_STATE_");
            return dt;
        }

        /// <summary>
        /// 기간 포맷 재정의 
        /// </summary>
        private void InitializeDatePeriodSetting(string sPeriodname)
        {
            // 기간 포맷 재정의 
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add(sPeriodname, "CUSTOM");
            values.Add(sPeriodname + "_PERIODFR", "");
            values.Add(sPeriodname + "_PERIODTO", "");

            Conditions.GetControl<SmartPeriodEdit>(sPeriodname).SetValue(values);

        }

        /// <summary>
        ///  목록 내역 clear (조회 건수가 없을때)
        /// </summary>
        private void ClearEditValueControl()
        {
            txtRequestno.EditValue ="";
            dtpRequestdate.EditValue = "";
            txtRequestdepartment.EditValue = "";
            txtRequestorname.EditValue = "";
            usrProductdefid.CODE.EditValue = "";
            usrProductdefid.VERSION.EditValue = "";
            usrProductdefid.NAME.EditValue = "";
            txtCustomerid.EditValue = "";
            txtProcesssegmentid.EditValue = "";
            txtOspAreaid.EditValue = "";
            txtOspVendorName.EditValue = "";
            chkIscharge.Checked = false;
            chkIsrequestdefectaccept.Checked = false;
            txtRequestqty.EditValue = 0D;
            txtRequestprice.EditValue = 0D;
            txtRequestamount.EditValue = 0;
            txtEtclotid.EditValue = "";
            txtRequestdescription.EditValue = "";
            txtActualqty.EditValue = 0D;
            txtActualdefectqty.EditValue = 0D;
            txtActualprice.EditValue = 0D;
            txtActualamount.EditValue =0;
            chkIsactualdefectaccept.Checked = false;
            txtActualuser.EditValue = "";  
            dtpActualdate.EditValue = "";
            txtActualdescription.EditValue = "";
            chkIssettledefectaccept.Checked = false;
            dtpApprovaldate.EditValue = "";
            txtApprovaluserid.EditValue = "";
            txtSettleqty.EditValue =0D;
            txtSettledefectqty.EditValue = 0D;
            txtSettleprice.EditValue =0D;
            txtSettleamount.EditValue = 0;
            txtSettledescription.EditValue = "";
        }
        #endregion

    }
}
