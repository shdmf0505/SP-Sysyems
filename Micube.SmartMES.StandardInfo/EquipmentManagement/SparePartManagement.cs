#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons.Controls;
using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion using

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프 로 그 램 명  : 기준정보 > 설비 기준 정보 > SparePart 관리
    /// 업  무  설  명  : Spare Part 정보를 관리한다.
    /// 생    성    자  : 장선미
    /// 생    성    일  : 2019-12-03
    /// 수  정  이  력  :
    ///     2021.02.15 전우성 코드 최적화 및 오류 수정
    ///
    /// </summary>
    public partial class SparePartManagement : SmartConditionManualBaseForm
    {
        #region 생성자

        public SparePartManagement()
        {
            InitializeComponent();
        }

        #endregion 생성자

        #region 컨텐츠 영역 초기화

        protected override void InitializeContent()
        {
            base.InitializeContent();
            InitializeGrid();
            InitializeEvent();
        }

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            grdSparePart.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export;
            grdSparePart.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdSparePart.View.AddTextBoxColumn("ENTERPRISEID", 200).SetIsHidden().SetDefault(UserInfo.Current.Enterprise);
            grdSparePart.View.AddTextBoxColumn("SPAREPARTVERSION", 150).SetIsHidden();
            grdSparePart.View.AddMemoEditColumn("IMAGE", 200).SetIsHidden();

            grdSparePart.View.AddTextBoxColumn("SPAREPARTID", 100).SetValidationKeyColumn();
            grdSparePart.View.AddLanguageColumn("SPAREPARTNAME", 150);

            //대분류
            grdSparePart.View.AddComboBoxColumn("TOPSPAREPARTCLASSID", 40, new SqlQuery("GetSparePartClass", "10001", "SPAREPARTCLASSTYPE=TopSparePart", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "SPAREPARTCLASSNAME", "SPAREPARTCLASSID")
                        .SetLabel("LARGECLASS")
                        .SetValidationIsRequired();

            //중분류
            grdSparePart.View.AddComboBoxColumn("MIDDLESPAREPARTCLASSID", 120, new SqlQuery("GetSparePartClass", "10001"
                                                                                                , "SPAREPARTCLASSTYPE=MiddleSparePart"
                                                                                                , "MIDDLESPAREPARTCLASSID=TOPSPAREPARTCLASSID"
                                                                                                , $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "SPAREPARTCLASSNAME", "SPAREPARTCLASSID")
                        .SetLabel("MIDDLECLASS")
                        .SetRelationIds("TOPSPAREPARTCLASSID")
                        .SetValidationIsRequired();

            //소분류
            grdSparePart.View.AddComboBoxColumn("SMALLSPAREPARTCLASSID", 120, new SqlQuery("GetSparePartClass", "10001"
                                                                                                , "SPAREPARTCLASSTYPE=SmallSparePart"
                                                                                                , "MIDDLESPAREPARTCLASSID=MIDDLESPAREPARTCLASSID"
                                                                                                , "TOPSPAREPARTCLASSID=TOPSPAREPARTCLASSID"
                                                                                                , $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "SPAREPARTCLASSNAME", "SPAREPARTCLASSID")
                        .SetLabel("SMALLCLASS")
                        .SetRelationIds("TOPSPAREPARTCLASSID", "MIDDLESPAREPARTCLASSID")
                        .SetValidationIsRequired();

            grdSparePart.View.AddTextBoxColumn("MODEL", 150).SetLabel("MODELID");
            grdSparePart.View.AddTextBoxColumn("MAKER", 150);
            grdSparePart.View.AddTextBoxColumn("UNITPRICE", 80).SetDisplayFormat("#,##0", MaskTypes.Numeric);
            grdSparePart.View.AddTextBoxColumn("SPEC", 150);
            grdSparePart.View.AddSpinEditColumn("NORMALLEADTIME", 100);
            grdSparePart.View.AddSpinEditColumn("SAFETYSTOCK", 100);

            #region 대체품

            //팝업 컬럼 설정
            var control = grdSparePart.View.AddSelectPopupColumn("ALTERNATEITEMID", 150, new SqlQuery("GetSparePart", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                      .SetPopupLayout("ALTERNATEITEMID", PopupButtonStyles.Ok_Cancel, true, false)
                                      .SetPopupResultCount(1)
                                      .SetPopupLayoutForm(1000, 700, FormBorderStyle.FixedToolWindow)
                                      .SetPopupResultMapping("ALTERNATEITEMID", "SPAREPARTID")
                                      .SetPopupApplySelection((selectedRows, dataGridRow) =>
                                      {
                                          foreach (DataRow row in selectedRows)
                                          {
                                              dataGridRow["ALTERNATEITEMNAME"] = row["SPAREPARTNAME"].ToString();
                                              dataGridRow["ALTERNATEITEMMODEL"] = row["MODEL"].ToString();
                                          }
                                      });

            control.Conditions.AddTextBox("SPAREPARTID").SetLabel("SPAREPARTIDNAME");
            control.Conditions.AddTextBox("NOTSPAREPARTID").SetPopupDefaultByGridColumnId("SPAREPARTID").SetIsHidden();

            control.GridColumns.AddTextBoxColumn("SPAREPARTID", 150).SetIsReadOnly();
            control.GridColumns.AddTextBoxColumn("SPAREPARTNAME", 200).SetIsReadOnly();
            control.GridColumns.AddComboBoxColumn("TOPSPAREPARTCLASSID", 100, new SqlQuery("GetSparePartClass", "10001", "SPAREPARTCLASSTYPE=TopSparePart", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "SPAREPARTCLASSNAME", "SPAREPARTCLASSID").SetLabel("LARGECLASS").SetIsReadOnly();
            control.GridColumns.AddComboBoxColumn("MIDDLESPAREPARTCLASSID", 100, new SqlQuery("GetSparePartClass", "10001", "SPAREPARTCLASSTYPE=MiddleSparePart", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "SPAREPARTCLASSNAME", "SPAREPARTCLASSID").SetLabel("MIDDLECLASS").SetIsReadOnly();
            control.GridColumns.AddComboBoxColumn("SMALLSPAREPARTCLASSID", 100, new SqlQuery("GetSparePartClass", "10001", "SPAREPARTCLASSTYPE=SmallSparePart", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "SPAREPARTCLASSNAME", "SPAREPARTCLASSID").SetLabel("SMALLCLASS").SetIsReadOnly();
            control.GridColumns.AddTextBoxColumn("MODEL", 100).SetLabel("MODELID").SetIsReadOnly();

            #endregion 대체품

            grdSparePart.View.AddTextBoxColumn("ALTERNATEITEMNAME", 200);
            grdSparePart.View.AddTextBoxColumn("ALTERNATEITEMMODEL", 200);

            #region 이미지 팝업

            control = grdSparePart.View.AddSelectPopupColumn("ISIMAGE", 100, new SparepartImagePopup())
                                  .SetLabel("IMAGE")
                                  .SetTextAlignment(TextAlignment.Center)
                                  .SetClearButtonInvisible()
                                  .SetPopupCustomParameter((popup, dataRow) =>
                                  {
                                      (popup as SparepartImagePopup).CurrentDataRow = dataRow;
                                  }, (popup, dataRow) =>
                                  {
                                      if ((popup as SparepartImagePopup).Image == null)
                                      {
                                          dataRow["ISIMAGE"] = "N";
                                          dataRow["IMAGE"] = null;
                                      }
                                      else
                                      {
                                          dataRow["ISIMAGE"] = "Y";
                                          Image img = (popup as SparepartImagePopup).Image;
                                          dataRow["IMAGE"] = (byte[])new ImageConverter().ConvertTo(new Bitmap(img), typeof(byte[]));
                                      }
                                  });

            control.SearchQuery = new SqlQuery("GetCodeYN", "10001");
            control.DisplayFieldName = "ISIAMGE";
            control.ValueFieldName = "CODEID";

            #endregion 이미지 팝업

            grdSparePart.View.AddTextBoxColumn("DESCRIPTION", 150);
            grdSparePart.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                        .SetDefault("Valid")
                        .SetTextAlignment(TextAlignment.Center);

            grdSparePart.View.AddTextBoxColumn("CREATOR", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdSparePart.View.AddTextBoxColumn("CREATEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdSparePart.View.AddTextBoxColumn("MODIFIER", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdSparePart.View.AddTextBoxColumn("MODIFIEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly().SetTextAlignment(TextAlignment.Center);

            grdSparePart.View.PopulateColumns();

            grdSparePart.ShowStatusBar = true;
        }

        #endregion 컨텐츠 영역 초기화

        #region Event

        /// <summary>
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            // 대체품 팝업 컬럼 x버튼 누를 때 이벤트
            (grdSparePart.View.Columns["ALTERNATEITEMID"].ColumnEdit as DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit).ButtonClick += (s, e) =>
            {
                if (e.Button.Kind.Equals(DevExpress.XtraEditors.Controls.ButtonPredefines.Clear))
                {
                    grdSparePart.View.SetFocusedRowCellValue("ALTERNATEITEMNAME", string.Empty);
                    grdSparePart.View.SetFocusedRowCellValue("ALTERNATEITEMMODEL", string.Empty);
                }
            };
        }

        #endregion Event

        #region 툴바

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            ExecuteRule("SaveSparePartManagement", grdSparePart.GetChangedRows());
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

                var values = Conditions.GetValues();
                values.Add("P_ENTERPRISEID", UserInfo.Current.Enterprise);
                values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                grdSparePart.View.ClearDatas();

                if (await SqlExecuter.QueryAsync("SelectSparePartManagement", "10001", values) is DataTable dt)
                {
                    if (dt.Rows.Count.Equals(0))
                    {
                        ShowMessage("NoSelectData");
                        return;
                    }

                    grdSparePart.DataSource = dt;
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

        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            Conditions.AddTextBox("P_MODELNAME").SetLabel("MODELNAME").SetPosition(4.1);
        }

        #endregion 검색

        #region 유효성 검사

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            grdSparePart.View.CheckValidation();

            if (grdSparePart.GetChangedRows().Rows.Count.Equals(0))
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }

        #endregion 유효성 검사
    }
}