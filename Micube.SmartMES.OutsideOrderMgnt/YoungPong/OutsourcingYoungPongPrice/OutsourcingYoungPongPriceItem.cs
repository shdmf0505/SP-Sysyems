#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons.Controls;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion using

namespace Micube.SmartMES.OutsideOrderMgnt
{
    /// <summary>
    /// 프 로 그 램 명  : 외주관리> 외주 단가 관리>외주단가항목관리
    /// 업  무  설  명  : 외주단가항목관리
    /// 생    성    자  : CHOISSTAR
    /// 생    성    일  : 2020-02-11
    /// 수  정  이  력  :
    ///     2021.05.17 전우성 : 코드 최적화 및 정리
    ///
    ///
    /// </summary>
    public partial class OutsourcingYoungPongPriceItem : SmartConditionManualBaseForm
    {
        #region 생성자

        public OutsourcingYoungPongPriceItem()
        {
            InitializeComponent();
        }

        #endregion 생성자

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 화면의 컨텐츠 영역을 초기화한다.
        /// </summary>
        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeLanguageKey();
            InitializeGrid();
            InitialControler();
            InitializeEvent();
        }

        /// <summary>
        /// Language Key 초기화
        /// </summary>
        private void InitializeLanguageKey()
        {
            grdMain.LanguageKey = "OUTSOURCINGYOUNGPONGPRICEITEMLIST";
        }

        /// <summary>
        /// 컨트롤러 초기화
        /// </summary>
        private void InitialControler()
        {
            // 검색조건 항목 숨김
            ConditionsVisible = false;
        }

        /// <summary>
        /// 코드그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            grdMain.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export;

            grdMain.View.AddTextBoxColumn("PLANTID", 200).SetIsHidden();
            grdMain.View.AddTextBoxColumn("ENTERPRISEID", 200).SetIsHidden();
            grdMain.View.AddTextBoxColumn("PRICEITEMIDUSECNT", 200).SetIsHidden();

            grdMain.View.AddTextBoxColumn("PRICEITEMID", 100).SetValidationKeyColumn().SetValidationIsRequired();
            grdMain.View.AddLanguageColumn("PRICEITEMNAME", 150);
            grdMain.View.AddComboBoxColumn("COMPONENTTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ComponentType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdMain.View.AddComboBoxColumn("DATASETTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OSPDataSetType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetEmptyItem("", "");

            grdMain.View.AddTextBoxColumn("DATASET", 150);
            grdMain.View.AddTextBoxColumn("FORMATMASK", 100);
            grdMain.View.AddTextBoxColumn("DESCRIPTION", 200);
            grdMain.View.AddComboBoxColumn("VALIDSTATE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetDefault("Valid");

            //grdMain.View.SetKeyColumn("PRICEITEMID");
            grdMain.View.PopulateColumns();
        }

        #endregion 컨텐츠 영역 초기화

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            Shown += (s, e) => SendKeys.Send("{F5}");

            grdMain.View.AddingNewRow += (s, e) =>
            {
                grdMain.View.SetFocusedRowCellValue("ENTERPRISEID", UserInfo.Current.Enterprise.ToString());
                grdMain.View.SetFocusedRowCellValue("PLANTID", UserInfo.Current.Plant.ToString());
                grdMain.View.SetFocusedRowCellValue("COMPONENTTYPE", "ComboBox");
                grdMain.View.SetFocusedRowCellValue("PRICEITEMIDUSECNT", "0");
            };

            grdMain.View.ShowingEditor += (s, e) =>
            {
                if (Convert.ToDecimal(Format.GetString(grdMain.View.GetFocusedRowCellValue("PRICEITEMIDUSECNT"), "0")) > 0)
                {
                    if (grdMain.View.FocusedColumn.FieldName.Equals("DATASETTYPE") ||
                        grdMain.View.FocusedColumn.FieldName.Equals("DATASET") ||
                        grdMain.View.FocusedColumn.FieldName.Equals("FORMATMASK"))
                    {
                        e.Cancel = true;
                    }
                }
            };

            grdMain.ToolbarDeleteRow += (s, e) =>
            {
                if (grdMain.View.FocusedRowHandle < 0)
                {
                    return;
                }

                if (grdMain.View.GetFocusedDataRow() is DataRow row)
                {
                    if (row.RowState != DataRowState.Added)
                    {
                        if (Convert.ToDecimal(Format.GetString(grdMain.View.GetFocusedRowCellValue("PRICEITEMIDUSECNT"), "0")).Equals(0))
                        {
                            row["VALIDSTATE"] = "Invalid";
                            (grdMain.View.DataSource as DataView).Table.AcceptChanges();
                        }
                    }
                }
            };
        }

        #endregion Event

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            ExecuteRule("OutsourcingYoungPongPriceItem", grdMain.GetChangedRows());
        }

        #endregion 툴바

        #region 검색

        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            try
            {
                DialogManager.ShowWaitArea(this.pnlContent);

                await base.OnSearchAsync();

                grdMain.View.ClearDatas();

                var values = Conditions.GetValues();
                values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                if (await SqlExecuter.QueryAsync("GetOutsourcingYoungPongPriceItem", "10001", values) is DataTable dt)
                {
                    if (dt.Rows.Count.Equals(0))
                    {
                        ShowMessage("NoSelectData");
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
        /// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
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