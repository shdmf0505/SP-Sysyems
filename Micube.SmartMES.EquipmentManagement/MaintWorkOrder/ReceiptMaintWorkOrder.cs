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
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion

namespace Micube.SmartMES.EquipmentManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 설비관리 > 보전관리 > 설비수리요청
    /// 업  무  설  명  : 관리되는 설비의 수리를 요청한다.
    /// 생    성    자  : 김용조
    /// 생    성    일  : 2019-09-09
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ReceiptMaintWorkOrder : SmartConditionManualBaseForm
    {
        #region Local Variables
        DataTable _maintWorkOrderTable;
        #endregion

        #region 생성자
        public ReceiptMaintWorkOrder()
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

            // TODO : 컨트롤 초기화 로직 구성
            InitializeGrid();
        }

        #region InitializeGrid - 제작입고목록을 초기화한다.
        /// <summary>        
        /// 치공구내역목록을 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdMaintWorkOrder.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Export | GridButtonItem.Restore;  // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기            
            grdMaintWorkOrder.View.SetIsReadOnly();

            grdMaintWorkOrder.View.AddTextBoxColumn("REQUESTTIME", 150)             //설명
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss");
            grdMaintWorkOrder.View.AddTextBoxColumn("WORKORDERID", 130)               //출소상태
                ;
            grdMaintWorkOrder.View.AddTextBoxColumn("WORKORDERSTATUSID", 60)             //의뢰일자
                .SetIsHidden();
            grdMaintWorkOrder.View.AddTextBoxColumn("WORKORDERSTATUS", 60)           //의뢰순번
                .SetTextAlignment(TextAlignment.Center)
                ;
            grdMaintWorkOrder.View.AddTextBoxColumn("WORKORDERSTEP", 100)
                .SetTextAlignment(TextAlignment.Center)
                ;                                                                   //제작구분명
            grdMaintWorkOrder.View.AddTextBoxColumn("WORKORDERSTEPID")
                .SetIsHidden();                                                     //제작구분명
            grdMaintWorkOrder.View.AddTextBoxColumn("REQUESTDEPARTMENT", 100)
               ;
            grdMaintWorkOrder.View.AddTextBoxColumn("REQUESTUSERID", 80)
               .SetIsHidden();
            grdMaintWorkOrder.View.AddTextBoxColumn("REQUESTUSER", 80)
               ;
            grdMaintWorkOrder.View.AddTextBoxColumn("EQUIPMENTID", 120);         //품목명            
            grdMaintWorkOrder.View.AddTextBoxColumn("EQUIPMENTNAME", 250)
                ;
            grdMaintWorkOrder.View.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 120)             //설명
                .SetIsHidden();
            grdMaintWorkOrder.View.AddTextBoxColumn("PROCESSSEGMENTCLASS", 120)             //설명
                .SetTextAlignment(TextAlignment.Center)
                ;
            grdMaintWorkOrder.View.AddTextBoxColumn("AREAID", 100)
               .SetIsHidden();                                                                   //작업장           
            grdMaintWorkOrder.View.AddTextBoxColumn("AREANAME", 150);             //의뢰자
            grdMaintWorkOrder.View.AddTextBoxColumn("DOWNTYPEID")
               .SetIsHidden();                                                     //의뢰자
            grdMaintWorkOrder.View.AddTextBoxColumn("DOWNTYPE", 150)          //의뢰부서
                .SetTextAlignment(TextAlignment.Center)
                ;
            grdMaintWorkOrder.View.AddTextBoxColumn("DOWNCODEID", 10)          //제작사유
                .SetIsHidden();
            grdMaintWorkOrder.View.AddTextBoxColumn("DOWNCODE", 100)             //설명
                .SetTextAlignment(TextAlignment.Center)
                ;
            grdMaintWorkOrder.View.AddTextBoxColumn("ISEQUIPMENTDOWNREQUEST", 110)             //설명
                .SetTextAlignment(TextAlignment.Center);
            grdMaintWorkOrder.View.AddTextBoxColumn("ISEQUIPMENTDOWN", 60)             //설명
                .SetTextAlignment(TextAlignment.Center)
                .SetIsHidden();
            grdMaintWorkOrder.View.AddTextBoxColumn("REQUESTCOMMENT", 200)             //설명
                ;
            grdMaintWorkOrder.View.AddTextBoxColumn("ISEMERGENCY", 80)             //설명
                .SetTextAlignment(TextAlignment.Center)
                ;
            grdMaintWorkOrder.View.AddTextBoxColumn("EMERGENCYREASON", 200)             //설명
                ;
            grdMaintWorkOrder.View.AddTextBoxColumn("ACCEPTCOMMENTS", 10)             //설명
                .SetIsHidden();


            grdMaintWorkOrder.View.PopulateColumns();
        }
        #endregion

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            chkRefresh.CheckStateChanged += ChkRefresh_CheckStateChanged;            
            btnAccept.Click += BtnAccept_Click;
            grdMaintWorkOrder.View.DoubleClick += grdMaintWorkOrder_DoubleClick;

            Shown += ReceiptMaintWorkOrder_Shown;
        }

        #region ReceiptMaintWorkOrder_Shown - Site관련정보를 화면로딩후 설정한다.
        private void ReceiptMaintWorkOrder_Shown(object sender, EventArgs e)
        {
            ChangeSiteCondition();

            ((SmartComboBox)Conditions.GetControl("P_PLANTID")).EditValueChanged += ConditionPlant_EditValueChanged;
        }
        #endregion

        #region ConditionPlant_EditValueChanged - 검색조건의 Site정보 변경시 관련 쿼리들을 일괄 변경한다.
        private void ConditionPlant_EditValueChanged(object sender, EventArgs e)
        {
            ChangeSiteCondition();
        }
        #endregion

        #region grdMaintWorkOrder_DoubleClick - 그리드 더블클릭이벤트
        private void grdMaintWorkOrder_DoubleClick(object sender, EventArgs e)
        {
            if (grdMaintWorkOrder.View.GetFocusedDataRow() != null)
            {
                OpenPopup();
            }
        }
        #endregion

        #region ChkRefresh_CheckStateChanged - 체크박스 변경이벤트
        private void ChkRefresh_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkRefresh.Checked)
            {
                tmMaintWorkOrder.Tick += TmMaintWorkOrder_Tick;
                tmMaintWorkOrder.Start();
            }
            else
            {
                tmMaintWorkOrder.Stop();
                tmMaintWorkOrder.Tick -= TmMaintWorkOrder_Tick;
            }
        }
        #endregion

        #region BtnAccept_Click - 등록버튼이벤트
        private void BtnAccept_Click(object sender, EventArgs e)
        {
            OpenPopup();
        }
        #endregion

        #region TmMaintWorkOrder_Tick - 타이머이벤트
        private void TmMaintWorkOrder_Tick(object sender, EventArgs e)
        {
            if (UserInfo.Current.ServiceId.Equals("MES"))
            {
                Research();
            }
        }
        #endregion

        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();
        }

        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            //base.OnToolbarClick(sender, e);
            SmartButton btn = sender as SmartButton;

            if (btn.Name.ToString().Equals("Accept"))
                BtnAccept_Click(sender, e);
        }

        #endregion

        #region 검색

        #region OnSearchAsync - 검색
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            //InitializeInsertForm();
            // TODO : 조회 SP 변경
            var values = Conditions.GetValues();

            #region 기간 검색형 전환 처리 - 만약 시간까지 포함하여 검색한다면 아래 문구는 필요없음            
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            if (Conditions.GetValue("p_WORKORDERSTEP").ToString() != "")
                values.Add("WORKORDERSTEP", GetConditionStringValue(Conditions.GetValue("p_WORKORDERSTEP").ToString()));
            #endregion
            //values = Commons.CommonFunction.ConvertParameter(values);
            _maintWorkOrderTable = SqlExecuter.Query("GetRequestMaintWorkOrderListForReceiptByEqp", "10001", values);

            grdMaintWorkOrder.DataSource = _maintWorkOrderTable;

            if (_maintWorkOrderTable.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }
            else
            {
                grdMaintWorkOrder.View.FocusedRowHandle = 0;
            }
        }
        #endregion

        #region Research - 재검색
        private void Research()
        {
            //InitializeInsertForm();
            // TODO : 조회 SP 변경
           
           
            var values = Conditions.GetValues();

            #region 기간 검색형 전환 처리 - 만약 시간까지 포함하여 검색한다면 아래 문구는 필요없음            
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            if (Conditions.GetValue("p_WORKORDERSTEP").ToString() != "")
                values.Add("WORKORDERSTEP", GetConditionStringValue(Conditions.GetValue("p_WORKORDERSTEP").ToString()));
            #endregion
            //values = Commons.CommonFunction.ConvertParameter(values);
            _maintWorkOrderTable = SqlExecuter.Query("GetRequestMaintWorkOrderListForReceiptByEqp", "10001", values);
            
            if (_maintWorkOrderTable.Rows.Count == 0)
            {
                //for (int i = ((DataTable)grdMaintWorkOrder.DataSource).Rows.Count - 1; i >= 0; i--)
                //    ((DataTable)grdMaintWorkOrder.DataSource).Rows.RemoveAt(i);
                grdMaintWorkOrder.View.ClearDatas();
            }
            else
            {
                try
                {
                    grdMaintWorkOrder.DataSource = _maintWorkOrderTable;
                }
                catch(DevExpress.Utils.HideException)
                {
                    return;
                }
            }
        }
        #endregion

        #region GetConditionStringValue - 다중선택한 조회조건에 '', ''로 감싸주는 함수
        string GetConditionStringValue(string originCondition)
        {
            if (originCondition.IndexOf(",") > -1)
            {
                string[] conditions = originCondition.Split(',');
                string returnStr = "";
                // ' 기호 추가
                for (int i = 0; i < conditions.Length; i++)
                {
                    conditions[i] = "'" + conditions[i].Trim() + "'";
                }

                // ,로 구분하여 합산
                for (int i = 0; i < conditions.Length; i++)
                {
                    if (i == 0)
                        returnStr = conditions[i];
                    else
                        returnStr += "," + conditions[i];
                }

                return returnStr;
            }
            else
            {
                return "'" + originCondition.Trim() + "'";
            }
        }
        #endregion

        #region SetDataTAbleForAccept - workOrderID가 같은 행을 그리드내에서 제거 - ClearDatas의 기능을 모를때 특정 행을 제거하기 위해 사용
        public void SetDataTableForAccept(string workOrderID)
        {
            for (int i = ((DataTable)grdMaintWorkOrder.DataSource).Rows.Count - 1; i >= 0; i--)
            {
                if(((DataTable)grdMaintWorkOrder.DataSource).Rows[i].GetString("WORKORDERID").Equals(workOrderID))
                    ((DataTable)grdMaintWorkOrder.DataSource).Rows.RemoveAt(i);
            }
        }
        #endregion

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            //InitializeConditionPlant();
            InitializeConditionFactory();
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            //기존으로 신청접수를 체크
            ((SmartCheckedComboBox)Conditions.GetControl("p_WORKORDERSTEP")).EditValue = "Request";
        }


        #region InitializeConditionPlant : 사이트 검색조건
        /// <summary>
        /// site 설정 
        /// </summary>
        private void InitializeConditionPlant()
        {
            var planttxtbox = Conditions.AddComboBox("PLANTID", new SqlQuery("GetPlantList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PLANTID", "PLANTID")
               .SetLabel("PLANTBLANK")
               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
               .SetPosition(0.1)
               .SetIsReadOnly(true)               
               .SetDefault(UserInfo.Current.Plant, "PLANTID") //기본값 설정 UserInfo.Current.Plant               
            ;
        }
        #endregion

        #region InitializeConditionFactory : 공장 검색조건
        /// <summary>
        /// site 설정 
        /// </summary>
        private void InitializeConditionFactory()
        {
            var planttxtbox = Conditions.AddComboBox("FACTORYID", new SqlQuery("GetFactoryListByEqp", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "FACTORYNAME", "FACTORYID")
               .SetLabel("FACTORY")
               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
               .SetPosition(0.2)
               .SetEmptyItem("", "", true)
               .SetRelationIds("P_PLANTID")
            ;
        }
        #endregion

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();
        }

        #endregion

        #region Private Function

        #region OpenPopup - 수리요청 접수 등록 팝업창을 오픈한다.
        private void OpenPopup()
        {
            if (grdMaintWorkOrder.View.FocusedRowHandle > -1)
            {
                Popup.RegistRequestMaintWorkOrderPopup orderPopup = new Popup.RegistRequestMaintWorkOrderPopup(grdMaintWorkOrder.View.GetFocusedDataRow(), Conditions.GetValue("P_PLANTID").ToString());
                orderPopup.SearchHandler += SetDataTableForAccept;
                orderPopup.ShowDialog();
            }
        }
        #endregion

        #region ChangeSiteCondition - 검색조건의 Site변경시 관련된 쿼리들을 변경
        private void ChangeSiteCondition()
        {
           
        }
        #endregion

        #region GetButtonWidth - 글자수에 따라 버튼의 크기를 결정
        private int GetButtonWidth(string caption)
        {
            return caption.Length * 20;
        }
        #endregion

        #endregion
    }
}
