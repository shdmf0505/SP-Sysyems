using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Micube.Framework.SmartControls;
using Micube.Framework.Net;
using Micube.Framework;

namespace Micube.SmartMES.QualityAnalysis
{
    public partial class ucShipmentInfo : UserControl
    {

        #region Local Variables
        private string _productInspGrade;
        private decimal _goodPCS = 0;
        private decimal _goodPNL = 0;
        private decimal _defectPCS = 0;
        private decimal _defectPNL = 0;
        private bool _matchingGrade = true;
        DataRow _standardRow; // 2020-01-07 강유라, lot의 품목 -> inspectionItemRel의 공통 기준도 적용하도록 수정
        DataRow _lotRow;// 유석진 수정-2019-12-10

        #endregion

        #region 생성자
        public ucShipmentInfo()
        {
            InitializeComponent();
            if (!smartPanel1.IsDesignMode())
            {
                InitializeEvent();
                InitializeControls();
            }
        }
        #endregion

        #region 컨텐츠 영역 초기화
        public void InitializeControls()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("UOMCLASSID", "Process");

            // UOM
            cboUOM.Editor.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboUOM.Editor.ShowHeader = false;
            cboUOM.Editor.ValueMember = "UOMDEFID";
            cboUOM.Editor.DisplayMember = "UOMDEFNAME";
            cboUOM.Editor.UseEmptyItem = false;
            cboUOM.Editor.DataSource = SqlExecuter.Query("GetUomDefinitionList", "10001", param);

            cboUOM.Editor.EditValue = "PCS";

            // 유석진 수정-2019-12-10
            //popInspector.SelectPopupCondition = Initialize_InspectorPopup();
        }

        #region 검사자 팝업초기화
        /// <summary>
        /// 작업장 팝업****내부 쿼리 / 항목수정
        /// </summary>
        /// <returns></returns>
        private ConditionItemSelectPopup Initialize_InspectorPopup(string inspectionClassId, string areaId)
        {
            ConditionItemSelectPopup popup = new ConditionItemSelectPopup();

            popup.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            popup.SetPopupLayout("INSPECTOR", PopupButtonStyles.Ok_Cancel, true, true);
            popup.Id = "USERID";
            // 유석진 수정-2019-12-10
            popup.SearchQuery = new SqlQuery("GetShipInspector", "10001", $"PRODUCTDEFID={_standardRow["RESOURCEID"].ToString()}", $"PRODUCTDEFVERSION={_standardRow["RESOURCEVERSION"].ToString()}", $"INSPECTIONCLASSID={inspectionClassId}", $"AREAID={areaId}");
            popup.IsMultiGrid = false;
            popup.DisplayFieldName = "USERNAME";
            popup.ValueFieldName = "INSPECTORID";
            popup.LanguageKey = "INSPECTOR";
            popup.IsRequired = true;

            /* 유석진 수정-2019-12-10
            popup.SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
            {
                foreach (DataRow row in selectedRow)
                {
                    string grade = row["GRADE"].ToString();
                    _matchingGrade = CheckInspectorGrade(grade);
                }

            });
            */

            popup.Conditions.AddTextBox("USERIDNAME");

            popup.GridColumns.AddTextBoxColumn("INSPECTORID", 150)
                .SetIsHidden();
            popup.GridColumns.AddTextBoxColumn("USERID", 150);
            popup.GridColumns.AddTextBoxColumn("USERNAME", 200);
            popup.GridColumns.AddTextBoxColumn("GRADE", 200);

            return popup;
        }
        #endregion

        #endregion

        #region Event
        public void InitializeEvent()
        {//UOM변경 이벤트
            cboUOM.Editor.EditValueChanged += Editor_EditValueChanged;
            popInspector.EditValueChanged += PopInspector_EditValueChanged;
        }

        private void PopInspector_EditValueChanged(object sender, EventArgs e)
        {
            if (_matchingGrade == false)
            {
                popInspector.SetValue(string.Empty);
                popInspector.Text = string.Empty;
            }
        }

        /// <summary>
        /// UOM에 따라 다른 값을 보여주는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Editor_EditValueChanged(object sender, EventArgs e)
        {
            SettingQty();
        }
        #endregion

        #region Public Function
        /// <summary>
        /// data clear public function
        /// </summary>
        public void ClearData(bool inspectorClear)
        {
            ControlCollection controls = tblGroup.Controls;
            foreach (Control con in controls)
            {
                AllClearControl(con, inspectorClear);
            }

            _goodPCS = 0;
            _goodPNL = 0;
            _defectPCS = 0;
            _defectPNL = 0;
        }

        /// <summary>
        /// 조회한 Lot 품목의 검사등급을 지정 하는 함수
        /// </summary>
        /// <param name="productInspGrade"></param>
        public void SetProductInspectionGrade(string productInspGrade)
        {
            _productInspGrade = productInspGrade;
        }

        /// <summary>
        /// 검사자 팝업을 초기화하는 함수
        /// </summary>
        /// <param name="row"></param>
        public void SetInspectorPopup(DataRow standardRow, DataRow lotRow)
        {
            _standardRow = standardRow;
            _lotRow = lotRow;
            string inspectionClassId = "ShipmentInspection";
            string areaId = Format.GetString(_lotRow["AREAID"]);
            if(UserInfo.Current.Enterprise.Equals("YOUNGPOONG"))
            {
                bool isNormal = true;
                isNormal = (_lotRow.GetString("LOTTYPE").Equals("Production") || _lotRow.GetString("LOTTYPE").Equals("FirstProduction")) ? true : false;
                inspectionClassId = isNormal ? "'ShipmentInspection'" : "'ShipmentInspection', 'DevelopmentInspection'";
                areaId = isNormal ? "'" + Format.GetString(_lotRow["AREAID"]) + "'" : "'" + Format.GetString(_lotRow["AREAID"]) + "', " + "'K2A07007'";
            }
            popInspector.SelectPopupCondition = Initialize_InspectorPopup(inspectionClassId, areaId);
        }

        /// <summary>
        /// 인계 작업장 List 콤보 데이터를 바인딩 하는 함수
        /// </summary>
        /// <param name="row"></param>
        public void SetTransitAreaList(DataRow row)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LOTID", row["LOTID"]);
            param.Add("PROCESSSEGMENTID", row["NEXTPROCESSSEGMENTID"]);
            param.Add("PROCESSSEGMENTVERSION", row["NEXTPROCESSSEGMENTVERSION"]);
            param.Add("RESOURCETYPE", "Resource");
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            param.Add("PLANTID", row["PLANTID"]);

            cboToArea.Editor.DataSource = SqlExecuter.Query("GetTransitAreaList", "10031", param);

            //투입 자원
            cboToArea.Editor.DisplayMember = "RESOURCENAME";
            cboToArea.Editor.ValueMember = "RESOURCEID";
            cboToArea.Editor.ShowHeader = false;
            //cboToArea.Editor.UseEmptyItem = true; // 유석진 수정-2019-12-10
            cboToArea.Editor.ItemIndex = 0; // 유석진 수정-2019-12-10
        }

        /// <summary>
        /// popInspector의 값을 전달하는 함수
        /// </summary>
        /// <returns></returns>
        public string GetInpsector()
        {
            if (popInspector.GetValue() == null)
            {
                return "";
            }
            else
            {
                return popInspector.GetValue().ToString();
            }
        }

        /// <summary>
        /// cboArea 값을 전달하는 함수
        /// </summary>
        /// <returns></returns>
        public string GetTransitArea()
        {
            if (cboToArea.Editor.EditValue == null)
            {
                return "";
            }
            else
            {
                string transitInfo = "";
                transitInfo = cboToArea.Editor.EditValue.ToString();
                transitInfo += "|" +cboToArea.Properties.GetDataSourceValue("AREAID", cboToArea.Properties.GetDataSourceRowIndex("RESOURCEID", cboToArea.Editor.EditValue)).ToString();
               
                return transitInfo;
            }
        }

        /// <summary>
        /// 전체 수량을 할당하는 함수
        /// </summary>
        public void SetQty(decimal goodPCS, decimal defectPCS , decimal goodPNL, decimal defectPNL)
        {
            _goodPCS = goodPCS;
            _defectPCS = defectPCS;
            _goodPNL = goodPNL;
            _defectPNL = defectPNL;

            SettingQty();
        }
        /// <summary>
        /// 인계 작업장 콤보 데이터를 초기화 하는 함수
        /// </summary>
        public void ClearTransitAreaList()
        {
            cboToArea.Editor.DataSource = null;
        }
        #endregion

        #region Private Function
        /// <summary>
        /// 모든 컬트롤 값을 empty 처리하는 함수
        /// </summary>
        /// <param name="con"></param>
        private void AllClearControl(Control con, bool inspectorClear)
        {
            if (con is SmartLabelTextBox txtBox)
            {
                txtBox.Editor.EditValue = string.Empty;
            }

            if (con is SmartLabelComboBox cboBox)
            {
                if (con.Name.Equals("cboUOM"))
                {
                    cboBox.Editor.EditValue = "PCS";
                }
                else
                { 
                    cboBox.Editor.ItemIndex = 0;
                }
            }
            // 2020-01-22 저장후 검사자 초기화 되지않도록
            if (con is SmartSelectPopupEdit popCon)
            {
                if(inspectorClear)
                {          
                    popCon.EditValue = string.Empty;
                    popCon.Text = string.Empty;
                }
            }
        }

        /// <summary>
        /// 검사 품목과 검사원 등급의 일치여부 확인
        /// </summary>
        private bool CheckInspectorGrade(string inspectorGrade)
        {
            if (string.IsNullOrWhiteSpace(_productInspGrade) || string.IsNullOrWhiteSpace(inspectorGrade))
                return false;

            int productInspGrad = MappingInspectorGrade(_productInspGrade);
            int inspectorGrad = MappingInspectorGrade(inspectorGrade);

            //품목의 검사 등급보다 검사자 등급이 낮을때 메세지 박스(검사자 지정 불가)
            if (inspectorGrad < productInspGrad)
            {
                MSGBox.Show(MessageBoxType.Information, "NotMatchProductInspectorGrade");//선택한 검사원은 해당 품목의 검사등급과 맞지 않습니다.

                return false;
            }
            else
            {
                return true;
            }

        }

        /// <summary>
        /// 검사품목 대상의 검사원 등급과 지정한 검사원등급을 비교하기 위해 등급을 int로 전환
        /// </summary>
        /// <param name="inputGrade"></param>
        /// <returns></returns>
        private int MappingInspectorGrade(string inputGrade)
        {
            int outInt = 0;

            switch (inputGrade)
            {
                case "B":
                    outInt = 4;
                    break;

                case "BG":
                    outInt = 3;
                    break;

                case "G":
                    outInt = 2;
                    break;

                case "W":
                    outInt = 1;
                    break;

                case "None":
                    outInt = 0;
                    break;
            }

            return outInt;
        }

        private void SettingQty()
        {
            string UOM = cboUOM.Editor.EditValue.ToString();

            if (UOM.Equals("PCS"))
            {
                txtGoodQty.EditValue = _goodPCS;
                txtDefectQty.EditValue = _defectPCS;

            }
            else if (UOM.Equals("PNL"))
            {
                txtGoodQty.EditValue = _goodPNL;
                txtDefectQty.EditValue = _defectPNL;
            }
            else if (UOM.Equals("EA"))
            {
                txtGoodQty.EditValue = "";
                txtDefectQty.EditValue = "";
            }
        }

        /// <summary>
        /// 표준공정의 step타입에 따라 컨트롤 visible 설정
        /// </summary>
        /// <param name="isShow"></param>
        public void SetReadOnly(bool isEnabled = false)
        {
           cboToArea.Editor.Enabled = isEnabled;
        }

        /// <summary>
        /// 검사자 컨트롤 visible 설정
        /// </summary>
        /// <param name="isEnabled"></param>
        public void SetInspectorReadOnly(bool isEnabled = false)
        {
            popInspector.Enabled = isEnabled;
        }
        #endregion
    }
}
