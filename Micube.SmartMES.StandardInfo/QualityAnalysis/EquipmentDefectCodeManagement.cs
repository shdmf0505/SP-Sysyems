#region using

using DevExpress.XtraEditors.Repository;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons.Controls;
using System;
using System.Data;
using System.Threading.Tasks;

#endregion using

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프 로 그 램 명  : 기준 정보 > 품질기준정보 > 설비불량코드 연계
    /// 업  무  설  명  : 설비에서 넘어오는 Defect Code를 관리 한다.
    /// 생    성    자  : 전우성
    /// 생    성    일  : 2019-05-23
    /// 수  정  이  력  :
    ///
    ///
    /// </summary>
    public partial class EquipmentDefectCodeManagement : SmartConditionManualBaseForm
    {
        #region 생성자

        public EquipmentDefectCodeManagement()
        {
            InitializeComponent();
        }

        #endregion 생성자

        #region 컨텐츠 영역 초기화

        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeLanguageKey();
            InitializeGrid();
        }

        /// <summary>
        /// Language Key 초기화
        /// </summary>
        private void InitializeLanguageKey()
        {
            grdMain.LanguageKey = "GRIDDEFECTCODELIST";
        }

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid(string equipmentType = "VRS")
        {
            grdMain.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdMain.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Copy | GridButtonItem.Export;

            //! 설비 타입에 따라 Column 설정 변경
            if (equipmentType.Equals("VRS"))
            {
                #region VRS

                grdMain.View.AddComboBoxColumn("DEFECTCODECLASSID", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=DefectMapDefectGroup", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                    .SetLabel("DEFECTGROUP")
                    .SetValidationKeyColumn()
                    .SetValidationIsRequired()
                    .SetTextAlignment(TextAlignment.Center);

                grdMain.View.AddComboBoxColumn("DEFECTITEMCODE", 120, new SqlQuery("GetDefectGroupCodeList", "10001", "CODECLASSID=DefectMapDefectGroupSub", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                    .SetLabel("DEFECTITEM")
                    .SetRelationIds("DEFECTCODECLASSID")
                    .SetValidationKeyColumn()
                    .SetValidationIsRequired()
                    .SetTextAlignment(TextAlignment.Center);

                grdMain.View.AddTextBoxColumn("EQUIPMENTID", 250)
                    .SetValidationKeyColumn()
                    .SetValidationIsRequired()
                    .SetTextAlignment(TextAlignment.Center);

                grdMain.View.AddSpinEditColumn("DEFECTCODE", 50)
                    .SetValidationKeyColumn()
                    .SetValidationIsRequired()
                    .SetDisplayFormat("#,##0", MaskTypes.Numeric, false)
                    .SetTextAlignment(TextAlignment.Center);

                #endregion VRS
            }
            else if (equipmentType.Equals("BBT"))
            {
                #region BBT

                grdMain.View.AddTextBoxColumn("DEFECTCODECLASSID", 80)
                    .SetLabel("DEFECTGROUP")
                    .SetDefault("BBT")
                    .SetIsHidden()
                    .SetValidationKeyColumn()
                    .SetValidationIsRequired()
                    .SetTextAlignment(TextAlignment.Center);

                grdMain.View.AddComboBoxColumn("DEFECTITEMCODE", 120, new SqlQuery("GetCodeList", "00001", "CODECLASSID=DefectMapBBTDefectCode", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                    .SetLabel("DEFECTITEM")
                    .SetValidationKeyColumn()
                    .SetValidationIsRequired()
                    .SetTextAlignment(TextAlignment.Center);

                grdMain.View.AddTextBoxColumn("EQUIPMENTID", 250)
                    .SetValidationKeyColumn()
                    .SetValidationIsRequired()
                    .SetTextAlignment(TextAlignment.Center);

                grdMain.View.AddTextBoxColumn("DEFECTCODE", 50)
                    .SetValidationKeyColumn()
                    .SetValidationIsRequired()
                    .SetTextAlignment(TextAlignment.Center);

                #endregion BBT
            }
            else // HOLE
            {
                #region HOLE

                grdMain.View.AddTextBoxColumn("DEFECTCODECLASSID", 80)
                    .SetLabel("DEFECTGROUP")
                    .SetDefault("HOLE")
                    .SetIsHidden()
                    .SetValidationKeyColumn()
                    .SetValidationIsRequired()
                    .SetTextAlignment(TextAlignment.Center);

                grdMain.View.AddComboBoxColumn("DEFECTITEMCODE", 120, new SqlQuery("GetCodeList", "00001", "CODECLASSID=DefectMapHoleDefectCode", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                    .SetLabel("DEFECTITEM")
                    .SetValidationKeyColumn()
                    .SetValidationIsRequired()
                    .SetTextAlignment(TextAlignment.Center);

                grdMain.View.AddTextBoxColumn("EQUIPMENTID", 250)
                    .SetValidationKeyColumn()
                    .SetValidationIsRequired()
                    .SetTextAlignment(TextAlignment.Center);

                grdMain.View.AddSpinEditColumn("DEFECTCODE", 50)
                    .SetValidationKeyColumn()
                    .SetValidationIsRequired()
                    .SetDisplayFormat("#,##0", MaskTypes.Numeric, false)
                    .SetTextAlignment(TextAlignment.Center);

                #endregion HOLE
            }

            grdMain.View.AddTextBoxColumn("DESCRIPTION", 250).SetTextAlignment(TextAlignment.Center);

            grdMain.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Valid")
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Center);

            grdMain.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdMain.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdMain.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdMain.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdMain.View.PopulateColumns();
            grdMain.View.BestFitColumns();

            // EQUIPMENTID는 영문대문자와 - _ 만 입력가능하도록 설정
            RepositoryItemTextEdit columnEdit = new RepositoryItemTextEdit();
            columnEdit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            columnEdit.Mask.EditMask = "[A-Z0-9\\-\\_]+";
            columnEdit.Mask.UseMaskAsDisplayFormat = true;

            grdMain.View.Columns["EQUIPMENTID"].ColumnEdit = columnEdit;
        }

        #endregion 컨텐츠 영역 초기화

        #region 툴바

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            ExecuteRule("EquipmentDefectCodeManagement", grdMain.GetChangedRows());
        }

        #endregion 툴바

        #region 검색

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            try
            {
                DialogManager.ShowWaitArea(this.pnlContent);

                await base.OnSearchAsync();

                grdMain.View.ClearColumns();
                grdMain.View.ClearDatas();

                // 조회조건 타입에 따라 Grid Columns 설정
                InitializeGrid(Format.GetString(Conditions.GetValues()["P_EQUIPMENTTYPE"]));

                if (await SqlExecuter.QueryAsync("GetEquipmentDefectCodeList", "10001", Conditions.GetValues()) is DataTable dt)
                {
                    if (dt.Rows.Count.Equals(0))
                    {
                        ShowMessage("NoSelectData");
                        return;
                    }

                    grdMain.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                throw MessageException.Create(Format.GetString(ex));
            }
            finally
            {
                DialogManager.CloseWaitArea(this.pnlContent);
            }
        }

        #endregion 검색

        #region 유효성 검사

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            grdMain.View.CheckValidation();

            if (grdMain.GetChangedRows().Rows.Count.Equals(0))
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }

        #endregion 유효성 검사
    }
}