#region using
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.Utils;
#endregion

namespace Micube.SmartMES.StandardInfo.Popup
{
    /// <summary>
    /// 프 로 그 램 명  : 기준정보 > 품질 기준 정보 > SPEC 등록 화면내의 팝업
    /// 업  무  설  명  : SPEC 등록 화면의 그리드를 더블 클릭하여 상세 스펙을 관리한다.
    /// 생    성    자  : 강유라
    /// 생    성    일  : 2019-05-14
    /// 수  정  이  력  : 윤성원 2019-07-05 using 에 #region #endregion 추가
    /// 
    /// 
    /// </summary>
    public partial class SpecDetailPopUp : SmartPopupBaseForm, ISmartCustomPopup
    {
       
        #region Local Variables
        public DataRow CurrentDataRow { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string _specSequence;//본 그리드에서 넘겨받는 specSequence
        public string _specClassId;//본 그리드에서 넘겨받는 specClassId
        public string _inspectionName;//본 그리드에서 넘겨받는 검사명
        public string _processsegmentclassid;

        public string _inspectiondefid;
        public string _equipmentid;
        public string _childequipmentid;
        public string _inspitemid;
        public string _enterpriseid;
        public string _plantid;
        public string _resourceid;
        public string _resourceversion;
        public string _productdefid;
        public string _consumabledefid;
        public bool buttonType = true;
        #endregion

        #region 생성자
        public SpecDetailPopUp()
        {
            InitializeComponent();
            InitializeEvent();
        }
        #endregion
        private void InitializeGrid()
        {
            if (buttonType == true)
            {
                grdSpecDetail.GridButtonItem = GridButtonItem.Add| GridButtonItem.Delete;
            }
            else
            {
                grdSpecDetail.GridButtonItem = GridButtonItem.None;
                grdSpecDetail.View.SetIsReadOnly();
            }

            // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기;   // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기
            grdSpecDetail.View.AddTextBoxColumn("INSPECTIONDEFID", 100).SetIsHidden();
            grdSpecDetail.View.AddTextBoxColumn("ENTERPRISEID", 100).SetIsHidden();
            grdSpecDetail.View.AddTextBoxColumn("PLANTID", 100).SetIsHidden();
            grdSpecDetail.View.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 100).SetIsHidden();
            grdSpecDetail.View.AddTextBoxColumn("PROCESSSEGMENTID", 100).SetIsHidden();
            grdSpecDetail.View.AddTextBoxColumn("EQUIPMENTID", 100).SetIsHidden();
            grdSpecDetail.View.AddTextBoxColumn("CHILDEQUIPMENTID", 100).SetIsHidden();
            grdSpecDetail.View.AddTextBoxColumn("PRODUCTDEFID", 100).SetIsHidden();
            grdSpecDetail.View.AddTextBoxColumn("CONSUMABLEDEFID", 100).SetIsHidden();
            grdSpecDetail.View.AddTextBoxColumn("CUSTOMERID", 100).SetIsHidden();
            grdSpecDetail.View.AddTextBoxColumn("VENDORID", 100).SetIsHidden();
            grdSpecDetail.View.AddTextBoxColumn("INSPITEMID", 100).SetIsHidden();
            grdSpecDetail.View.AddTextBoxColumn("WORKTYPE", 100).SetIsHidden();
            grdSpecDetail.View.AddTextBoxColumn("WORKCONDITION", 100).SetIsHidden();
            grdSpecDetail.View.AddTextBoxColumn("DEFAULTCHARTTYPE", 100).SetIsHidden();
            grdSpecDetail.View.AddTextBoxColumn("RESOURCEID", 100).SetIsHidden();
            grdSpecDetail.View.AddTextBoxColumn("RESOURCEVERSION", 100).SetIsHidden();
            grdSpecDetail.View.AddTextBoxColumn("CONTROLRANGE", 100).SetIsHidden();
            grdSpecDetail.View.AddTextBoxColumn("SPECVERSION", 100).SetIsHidden();
            grdSpecDetail.View.AddTextBoxColumn("UOMDEFID", 100).SetIsHidden();
            grdSpecDetail.View.AddTextBoxColumn("SPECSEQUENCE", 100).SetIsHidden();
            grdSpecDetail.View.AddTextBoxColumn("SPECCLASSID", 100).SetIsHidden();
            var CONTROLGUBUN = grdSpecDetail.View.AddGroupColumn("CONTROLGUBUN");
            CONTROLGUBUN.AddComboBoxColumn("CONTROLTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ControlType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            CONTROLGUBUN.AddTextBoxColumn("ISCHARTTYPE",100)
                .SetDefault(false);
            var LBLSPEC = grdSpecDetail.View.AddGroupColumn("LBLSPEC");
            LBLSPEC.AddSpinEditColumn("LSL", 80)
                .SetLabel("LBLLOWER")
                .SetDisplayFormat("#,###,###,##0.######");
            LBLSPEC.AddSpinEditColumn("SL", 80)
                .SetLabel("LBLCENTER")
                .SetDisplayFormat("#,###,###,##0.######");
            LBLSPEC.AddSpinEditColumn("USL", 80)
                .SetLabel("LBLUPPER")
                .SetDisplayFormat("#,###,###,##0.######");
            var LBLCONTROLLIMIT = grdSpecDetail.View.AddGroupColumn("LBLCONTROLLIMIT");
            LBLCONTROLLIMIT.AddSpinEditColumn("LCL", 80)
                .SetLabel("LBLLOWER")
                .SetDisplayFormat("#,###,###,##0.######");
            LBLCONTROLLIMIT.AddSpinEditColumn("CL", 80)
                .SetLabel("LBLCENTER")
                .SetDisplayFormat("#,###,###,##0.######");
            LBLCONTROLLIMIT.AddSpinEditColumn("UCL", 80)
                .SetLabel("LBLUPPER")
                .SetDisplayFormat("#,###,###,##0.######");
            var LBLOUTLIER = grdSpecDetail.View.AddGroupColumn("LBLOUTLIER");
            LBLOUTLIER.AddSpinEditColumn("LOL", 80)
                .SetLabel("LBLLOWER")
                .SetDisplayFormat("#,###,###,##0.######");
            LBLOUTLIER.AddSpinEditColumn("UOL", 80)
                .SetLabel("LBLUPPER")
                .SetDisplayFormat("#,###,###,##0.######");
          
            grdSpecDetail.View.SetKeyColumn("CONTROLTYPE");
            grdSpecDetail.View.PopulateColumns();

            RepositoryItemCheckEdit repositoryCheckEdit1 = grdSpecDetail.View.GridControl.RepositoryItems.Add("CheckEdit") as RepositoryItemCheckEdit;
            repositoryCheckEdit1.ValueChecked = "Y";
            repositoryCheckEdit1.ValueUnchecked = "N";
            repositoryCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            grdSpecDetail.View.Columns["ISCHARTTYPE"].ColumnEdit = repositoryCheckEdit1;
        }
        #region 컨텐츠 영역 초기화




        #endregion

        #region Event
        private void InitializeEvent()
        {
            this.Load += SpecDetailPopUp_Load;
            this.btnCancel.Click += BtnCancel_Click;
         
            this.btnSave.Click += BtnSave_Click;
            //행추가시 
            grdSpecDetail.View.AddingNewRow += View_AddingNewRow;

          //  grdSpecDetail.View.CellValueChanged += grdSpecDetail_CellValueChanged;
            grdSpecDetail.View.CellValueChanging += grdSpecDetail_CellValueChanging;
            //grdSpecDetail.View.MouseDown += grdSpecDetail_MouseDown;
        }
        
        private void grdSpecDetail_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            int irow = e.RowHandle;
            if (irow < 0) return;
            grdSpecDetail.View.CellValueChanging -= grdSpecDetail_CellValueChanging;
            if (e.Column.FieldName == "ISCHARTTYPE")
            {
                if (e.Value.ToString().Equals("Y"))
                {
                    for (int temprow = 0; temprow < grdSpecDetail.View.DataRowCount; temprow++)
                    {
                        if (irow != temprow)
                        {
                            grdSpecDetail.View.SetRowCellValue(temprow, "ISCHARTTYPE", "N");
                          
                        }
                    }
                }

            }

            grdSpecDetail.View.CellValueChanging += grdSpecDetail_CellValueChanging;
        }
       

        private void SpecDetailPopUp_Load(object sender, EventArgs e)
        {
            
            if (buttonType == false)
            {
                btnSave.Visible = false;
            }
            else
             {
                btnSave.Visible = true;
            }
            InitializeGrid();
            OnSearchClick();
        }

        /// <summary>
        /// 검색버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            OnSearchClick();
        }

        /// <summary>
        /// 저장 버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            //controltype c체크 중복 제외 처리
            grdSpecDetail.View.FocusedRowHandle = grdSpecDetail.View.FocusedRowHandle;
            grdSpecDetail.View.FocusedColumn = grdSpecDetail.View.Columns["CONTROLTYPE"];
            grdSpecDetail.View.ShowEditor();
            DataTable changed = grdSpecDetail.GetChangedRows();

            if (changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                this.ShowMessage(MessageBoxButtons.OK, "NoSaveData");
                return;
            }
            if (CheckPriceDateKeyColumns() == false)
            {
                string lblControltype = grdSpecDetail.View.Columns["CONTROLTYPE"].Caption.ToString();

                this.ShowMessage(MessageBoxButtons.OK, "InValidOspData007", lblControltype); //메세지 
                return;
            }
            //defatnu ISCHARTTYPE
            int intIscharttype = 0;
            for (int irow = 0; irow < grdSpecDetail.View.DataRowCount; irow++)
            {

                DataRow row = grdSpecDetail.View.GetDataRow(irow);

                if (!(row.RowState == DataRowState.Deleted))
                {
                    string Ischarttype = grdSpecDetail.View.GetRowCellValue(irow,"ISCHARTTYPE").ToString();
                    if (Ischarttype.Equals("Y"))
                    {
                        intIscharttype = intIscharttype + 1;
                    }
                }
            }
            //다국어 처리 예정 (??/
            if (intIscharttype !=1)
            {
                string lblConsumabledefid = grdSpecDetail.View.Columns["ISCHARTTYPE"].Caption.ToString();
                this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblConsumabledefid); //메세지
                return;
              
            }
            //숫자값 체크 
            for (int irow = 0; irow < grdSpecDetail.View.DataRowCount; irow++)
            {

                DataRow row = grdSpecDetail.View.GetDataRow(irow);

                if (row.RowState == DataRowState.Added || row.RowState == DataRowState.Modified)
                {
                    double lsl = row["LSL"].ToSafeDoubleNaN();
                    double sl = row["SL"].ToSafeDoubleNaN();
                     
                    double usl = row["USL"].ToSafeDoubleNaN();
                   

                    if (lsl > sl || sl > usl || lsl> usl)
                    {
                        
                        this.ShowMessage("ValidationSL");
                        return;
                    }
                    double lcl = row["LCL"].ToSafeDoubleNaN();
                    double cl = row["CL"].ToSafeDoubleNaN();
                    double ucl = row["UCL"].ToSafeDoubleNaN();
                   
                    if (lcl> cl || cl> ucl || lcl> ucl)
                    {
                        
                        this.ShowMessage("ValidationCL");
                        return;
                    }
                    double lol = row["LOL"].ToString().ToSafeDoubleNaN();
                    double uol = row["UOL"].ToString().ToSafeDoubleNaN();

                    if (lol > uol)
                    {
                       
                        this.ShowMessage("ValidationOL");
                        return;
                    }
                }
            }
            DialogResult result = System.Windows.Forms.DialogResult.No;

            result = this.ShowMessage(MessageBoxButtons.YesNo, "SpecRegisterConfirmNote");

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    this.ShowWaitArea();
                    btnSave.Enabled = false;
                    btnCancel.Enabled = false;
                    DataTable dtSave = grdSpecDetail.GetChangedRows();

                    ExecuteRule("SaveMmSpecDetailRegister", dtSave);
                   
                    this.DialogResult = DialogResult.OK;
                    ShowMessage("SuccessSave");
                    //재조회 처리 

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
                    OnSearchClick();
                    this.Close();
                }
            }

        }

        
        /// <summary>
        ///  추가 버튼 클릭 시 호출
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            grdSpecDetail.View.SetFocusedRowCellValue("INSPECTIONDEFID", _inspectiondefid);// plantid
            grdSpecDetail.View.SetFocusedRowCellValue("ENTERPRISEID", _enterpriseid);// plantid
            grdSpecDetail.View.SetFocusedRowCellValue("PLANTID", _plantid);// plantid
            grdSpecDetail.View.SetFocusedRowCellValue("PROCESSSEGMENTCLASSID",_processsegmentclassid);
            grdSpecDetail.View.SetFocusedRowCellValue("PROCESSSEGMENTID","*");
            grdSpecDetail.View.SetFocusedRowCellValue("EQUIPMENTID", _equipmentid);
            grdSpecDetail.View.SetFocusedRowCellValue("CHILDEQUIPMENTID", _childequipmentid);
            grdSpecDetail.View.SetFocusedRowCellValue("PRODUCTDEFID", _productdefid);
            grdSpecDetail.View.SetFocusedRowCellValue("CONSUMABLEDEFID", _consumabledefid);
            grdSpecDetail.View.SetFocusedRowCellValue("CUSTOMERID", "*");
            grdSpecDetail.View.SetFocusedRowCellValue("VENDORID", "*");
            grdSpecDetail.View.SetFocusedRowCellValue("INSPITEMID", _inspitemid);
            grdSpecDetail.View.SetFocusedRowCellValue("WORKTYPE", "*");
            grdSpecDetail.View.SetFocusedRowCellValue("WORKCONDITION", "*");
            grdSpecDetail.View.SetFocusedRowCellValue("DEFAULTCHARTTYPE", "*");
            grdSpecDetail.View.SetFocusedRowCellValue("RESOURCEID", _resourceid);
            grdSpecDetail.View.SetFocusedRowCellValue("RESOURCEVERSION", _resourceversion);
           
            grdSpecDetail.View.SetFocusedRowCellValue("SPECVERSION", 0);
            grdSpecDetail.View.SetFocusedRowCellValue("UOMDEFID", "*");
            grdSpecDetail.View.SetFocusedRowCellValue("SPECSEQUENCE", _specSequence);
            grdSpecDetail.View.SetFocusedRowCellValue("SPECCLASSID", _specClassId);
           
           
          

        }
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        #endregion

        #region 검색
        private void  OnSearchClick()
        {

            Dictionary<string, object> values = new Dictionary<string, object>();

            //  values.Add("P_CONTROLTYPE", this.cboChartType.GetDataValue());
            values.Add("P_INSPITEMID", this._inspitemid);
            values.Add("P_INSPECTIONDEFID", this._inspectiondefid);
            values.Add("P_PROCESSSEGID", "*");
            values.Add("P_RESOURCEID", _resourceid);
            values.Add("P_RESOURCEVERSION", _resourceversion);
         
            values.Add("P_SPECCLASSID", this._specClassId);
            values.Add("P_SPECSEQUENCE", this._specSequence);
            DataTable _specTable = null;
            if (_specClassId.Equals("SubassemblySpec"))
            {
                 _specTable = SqlExecuter.Query("SelectSubassemblySpecDetailPopUp", "10001", values);

            }
            else
            {
                 _specTable = SqlExecuter.Query("SelectSpecDetailPopUp", "10001", values);
            }
            grdSpecDetail.View.CellValueChanging -= grdSpecDetail_CellValueChanging;
            grdSpecDetail.DataSource = _specTable;
             grdSpecDetail.View.CellValueChanging += grdSpecDetail_CellValueChanging;
        }


        #endregion

        #region Private Function

        /// <summary>
        /// CONTROLTYPE 중복체크 
        /// </summary>
        /// <returns></returns>
        private bool CheckPriceDateKeyColumns()
        {
            bool blcheck = true;
            if (grdSpecDetail.View.DataRowCount == 0)
            {
                return blcheck;
            }


            for (int irow = 0; irow < grdSpecDetail.View.DataRowCount; irow++)
            {

                DataRow row = grdSpecDetail.View.GetDataRow(irow);

                //if (row.RowState == DataRowState.Added || row.RowState == DataRowState.Modified)
                {
                    string strControltype = row["CONTROLTYPE"].ToString();
                   

                    if (SearchPriceDateKey(strControltype, irow) < 0)
                    {
                        blcheck = true;

                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return blcheck;
        }
        /// <summary>
        /// 자재 중복 체크 함수
        /// </summary>
        /// <param name="dateStartdate"></param>
        /// <param name="dateEnddate"></param>
        /// <param name="icurRow"></param>
        /// <returns></returns>
        private int SearchPriceDateKey(string strControltype, int icurRow)
        {
            int iresultRow = -1;

            for (int irow = 0; irow < grdSpecDetail.View.DataRowCount; irow++)
            {
                if (icurRow != irow)
                {
                    DataRow row = grdSpecDetail.View.GetDataRow(irow);

                    // 행 삭제만 제외 
                    //if (grdSpecDetail.View.IsDeletedRow(row) == false)
                    {
                        string strTempControltype = row["CONTROLTYPE"].ToString();
                        if (strTempControltype.Equals(strControltype) )
                        {
                            return irow;
                        }

                    }
                }
            }
            return iresultRow;
        }
        #endregion


    }
}
