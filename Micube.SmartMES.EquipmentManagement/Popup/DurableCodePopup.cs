#region using
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Validations;
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

namespace Micube.SmartMES.EquipmentManagement.Popup
{
    /// <summary>
    /// 프 로 그 램 명  : 부품코드정보 팝업
    /// 업  무  설  명  : Spare Part 입고등록을 위해 부품코드를 조회할 때 사용
    /// 생    성    자  : 김용조
    /// 생    성    일  : 2019-07-04
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class DurableCodePopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        public DataRow CurrentDataRow { get; set; }

        #region Local Variables
        /// <summary>
        ///  선택한 부품 list를 보내기 위한 Handler
        /// </summary>
        /// <param name="dt"></param>
        public delegate void chat_room_evecnt_handler(DataRow row, Image imageData);
        public event chat_room_evecnt_handler write_handler;

        public string DurableDeptCode
        {
            get { return txtItemCode.Text; }
            set { txtItemCode.Text = value; }
        }

        public DataTable SearchResult
        {
            set
            {
                grdDurableCodeList.DataSource = value;
            }
        }

        string _plantID;
        #endregion

        #region 생성자
        public DurableCodePopup(string plantID)
        {
            InitializeComponent();

            InitializeEvent();
           
            InitializeCondition();

            _plantID = plantID;
        }        
        #endregion

        #region 초기화
        /// <summary>
        /// 조회 조건 초기화
        /// </summary>
        private void InitializeCondition()
        {
            //그리드 초기화
            InitializeGridIdDefinitionManagement();            
        }
        #endregion

        #region  이벤트
        private void InitializeEvent()
        {
            btnSearch.Click += BtnSearch_Click;
            btnConfirm.Click += BtnConfirm_Click;
            btnCancel.Click += BtnCancel_Click;
            grdDurableCodeList.View.DoubleClick += grdProductItem_DoubleClick;

            txtItemCode.KeyPress += TxtItemCode_KeyPress;
            txtItemNm.KeyPress += TxtItemNm_KeyPress;
            txtmodelname.KeyPress += TxtItemVer_KeyPress;

            grdDurableCodeList.View.FocusedRowChanged += grdDurableCodeList_FocusedRowChanged;
        }

        private void grdDurableCodeList_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DisplayImage(e.FocusedRowHandle);
        }

        private void TxtItemVer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                Search(txtItemNm.Text, txtItemCode.Text, txtmodelname.Text);
        }

        private void TxtItemNm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                Search(txtItemNm.Text, txtItemCode.Text, txtmodelname.Text);
        }

        private void TxtItemCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                Search(txtItemNm.Text, txtItemCode.Text, txtmodelname.Text);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            DataRow row = grdDurableCodeList.View.GetFocusedDataRow();
            CurrentDataRow = row;
            write_handler(row, speImage.Image);
            this.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdProductItem_DoubleClick(object sender, EventArgs e)
        {
            DataRow row = grdDurableCodeList.View.GetFocusedDataRow();
            CurrentDataRow = row;
            write_handler(row, speImage.Image);
            this.Close();
        }



        /// <summary>
        /// 조회 클릭 - 메인 grid에 체크 데이터 전달
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            Search( txtItemNm.Text, txtItemCode.Text, txtmodelname.Text);
        }

        void Search( string durableDefName, string durableDefID, string modelname)
        {
            Dictionary<string, object> Param = new Dictionary<string, object>();
            Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            Param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            //PLANTID를 입력할 시 데이터가 나오지 않으므로 현재 주석 처리
            Param.Add("PLANTID", _plantID);      
            Param.Add("DURABLEDEFNAME", durableDefName);
            Param.Add("DURABLEDEFID", durableDefID);
            Param.Add("MODELNAME", modelname);

            Param = Commons.CommonFunction.ConvertParameter(Param);
            DataTable dt = SqlExecuter.Query("GetDurableDefinitionListByEqp", "10001", Param);
            
            grdDurableCodeList.DataSource = dt;

        }

        /// <summary>
        /// 취소 버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Private Functions

        #region InitializeGridIdDefinitionManagement - 그리드 초기화
        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGridIdDefinitionManagement()
        {
            // GRID 초기화
            grdDurableCodeList.GridButtonItem = GridButtonItem.None;   // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기
                                                                   //grdProductItemSpec.View.AddTextBoxColumn("ENTERPIRSEID").SetIsHidden();
            grdDurableCodeList.View.AddTextBoxColumn("SPAREPARTID", 100).SetIsReadOnly();
            grdDurableCodeList.View.AddTextBoxColumn("SPAREPARTVERSION", 50).SetIsReadOnly();            
            grdDurableCodeList.View.AddTextBoxColumn("SPAREPARTNAME", 150).SetIsReadOnly();
            grdDurableCodeList.View.AddTextBoxColumn("SAFETYSTOCK", 80).SetIsReadOnly();
            grdDurableCodeList.View.AddTextBoxColumn("SPEC", 80).SetIsReadOnly();
            grdDurableCodeList.View.AddTextBoxColumn("MODELNAME", 80).SetIsReadOnly();
            //grdDurableCodeList.View.AddTextBoxColumn("PARENTDURABLECLASSID").SetIsHidden();
            //grdDurableCodeList.View.AddTextBoxColumn("PARENTDURABLECLASSNAME", 130)
            //    .SetTextAlignment(TextAlignment.Center)
            //    .SetIsReadOnly();
            //grdDurableCodeList.View.AddTextBoxColumn("DURABLECLASSID").SetIsHidden();
            //grdDurableCodeList.View.AddTextBoxColumn("DURABLECLASSNAME", 130)
            //    .SetTextAlignment(TextAlignment.Center)
            //    .SetIsReadOnly();
            grdDurableCodeList.View.AddTextBoxColumn("DESCRIPTION", 150).SetIsReadOnly();
            grdDurableCodeList.View.AddTextBoxColumn("PRICE").SetIsHidden();
            //grdDurableCodeList.View.SetAutoFillColumn("PRODUCTDEFNAME");
            grdDurableCodeList.View.PopulateColumns();
        }
        #endregion

        #region DisplayImage - 서버로부터 이미지를 가져와서 출력
        private void DisplayImage(int rowHandle)
        {
            DataRow currentRow = grdDurableCodeList.View.GetDataRow(rowHandle);

            #region 기간 검색형 전환 처리 - 만약 시간까지 포함하여 검색한다면 아래 문구는 필요없음            
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            values.Add("ITEMID", currentRow.GetString("SPAREPARTID"));
            values.Add("ITEMVERSION", currentRow.GetString("SPAREPARTVERSION"));
            #endregion

            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable sparePartSearchResult = SqlExecuter.Query("GetSparePartImageByEqp", "10001", values);

            if (sparePartSearchResult.Rows.Count > 0)
            {
                if (sparePartSearchResult.Rows[0]["IMAGE"] != null && !Format.GetString(sparePartSearchResult.Rows[0]["IMAGE"]).Equals(string.Empty))
                {
                    //speImage.BringToFront();
                    speImage.Image = (Bitmap)new ImageConverter().ConvertFrom(sparePartSearchResult.Rows[0]["IMAGE"]);
                }
                else
                {
                    //speImage.SendToBack();
                    speImage.Image = null;
                }
            }
            else
            {
                //speImage.SendToBack();
                speImage.Image = null;
            }
        }
        #endregion
        #endregion
    }
}
