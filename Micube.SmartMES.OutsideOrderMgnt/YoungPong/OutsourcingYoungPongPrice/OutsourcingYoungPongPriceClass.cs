#region using

using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion using

namespace Micube.SmartMES.OutsideOrderMgnt
{
    /// <summary>
    /// 프 로 그 램 명  :  외주관리> 외주 단가 관리>외주단가분류관리
    /// 업  무  설  명  : 외주단가분류관리
    /// 생    성    자  : CHOISSTAR
    /// 생    성    일  : 2020-02-11
    /// 수  정  이  력  :
    ///     2021-05-17  전우성 : 코드 최적화 및 개선
    ///
    ///
    /// </summary>
    public partial class OutsourcingYoungPongPriceClass : SmartConditionManualBaseForm
    {
        #region Global Variable

        /// <summary>
        /// 저장의 여부 확인
        /// </summary>
        private bool _isSave = false;

        #endregion Global Variable

        #region 생성자

        public OutsourcingYoungPongPriceClass()
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
            InitialControler();
            InitializeGrid();
            InitializeEvent();
        }

        /// <summary>
        /// Language Key 초기화
        /// </summary>
        private void InitializeLanguageKey()
        {
            grdMain.LanguageKey = "OUTSOURCINGYOUNGPONGPRICECLASSLIST";
            grdSub.LanguageKey = "OUTSOURCINGYOUNGPONGPRICECLASSDETAILLIST";
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
            #region 외주단가분류 목록

            grdMain.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export;
            grdMain.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            grdMain.View.AddTextBoxColumn("PLANTID", 200).SetIsHidden();
            grdMain.View.AddTextBoxColumn("ENTERPRISEID", 200).SetIsHidden();
            grdMain.View.AddTextBoxColumn("PRICECLASSIDCNT", 200).SetIsHidden();

            grdMain.View.AddTextBoxColumn("PRICECLASSID", 110).SetValidationKeyColumn().SetValidationIsRequired();
            grdMain.View.AddTextBoxColumn("PRICECLASSNAME", 200);
            grdMain.View.AddTextBoxColumn("DESCRIPTION", 200);
            grdMain.View.AddComboBoxColumn("VALIDSTATE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                     .SetDefault("Valid")
                     .SetIsReadOnly();

            grdMain.View.PopulateColumns();

            #endregion 외주단가분류 목록

            #region 외주단가 분류별 항목

            grdSub.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export;
            grdSub.View.AddTextBoxColumn("PLANTID", 200).SetIsHidden();
            grdSub.View.AddTextBoxColumn("ENTERPRISEID", 200).SetIsHidden();
            grdSub.View.AddTextBoxColumn("PRICECLASSID", 100).SetIsHidden();

            #region 단가항목 코드

            var control = grdSub.View.AddSelectPopupColumn("PRICEITEMID", 120, new SqlQuery("GetPriceitemidByOsp", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                   .SetPopupLayout("PRICEITEMPOPUP", PopupButtonStyles.Ok_Cancel, true, false)
                                   .SetPopupResultCount(1)
                                   .SetNotUseMultiColumnPaste()
                                   .SetPopupLayoutForm(700, 600)
                                   .SetValidationKeyColumn()
                                   .SetValidationIsRequired()
                                   .SetPopupApplySelection((selectedRows, dataGridRow) =>
                                   {
                                       int irow = 0;
                                       DataRow classRow = grdSub.View.GetFocusedDataRow();
                                       int crow = grdSub.View.FocusedRowHandle;

                                       foreach (DataRow row in selectedRows)
                                       {
                                           if (irow == 0)
                                           {
                                               string sPriceitemid = row["PRICEITEMID"].ToString();

                                               int icheck = CheckPriceitemid(sPriceitemid, crow);
                                               if (icheck == -1)
                                               {
                                                   classRow["PRICEITEMID"] = row["PRICEITEMID"];
                                                   classRow["PRICEITEMNAME"] = row["PRICEITEMNAME"];

                                                   grdSub.View.RaiseValidateRow(crow);
                                               }
                                               else
                                               {
                                                   classRow["PRICEITEMID"] = "";
                                                   classRow["PRICEITEMNAME"] = "";
                                                   irow = irow - 1;
                                               }
                                           }

                                           irow = irow + 1;
                                       }
                                   });

            control.Conditions.AddTextBox("P_PRICEITEMNAME").SetLabel("PRICEITEMNAME");

            control.GridColumns.AddTextBoxColumn("PRICEITEMID", 80);
            control.GridColumns.AddTextBoxColumn("PRICEITEMNAME", 150);
            control.GridColumns.AddComboBoxColumn("DATASETTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OSPDataSetType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            control.GridColumns.AddTextBoxColumn("DATASET", 150);

            #endregion 단가항목 코드

            grdSub.View.AddTextBoxColumn("PRICEITEMNAME", 150).SetIsReadOnly();
            grdSub.View.AddSpinEditColumn("PRIORITY", 100).SetDisplayFormat("#,##0", MaskTypes.Numeric)
                       .SetValidationKeyColumn()
                       .SetValidationIsRequired();

            grdSub.View.AddComboBoxColumn("ISRANGE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetDefault("N");
            grdSub.View.AddComboBoxColumn("RANGEUNIT", 100, new SqlQuery("GetUomDefinitionMapListByOsp", "10001", "UOMCATEGORY=OSPRANGE", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "UOMDEFID", "UOMDEFID")
                     .SetEmptyItem("", "")
                     .SetDefault("");
            grdSub.View.AddComboBoxColumn("VALIDSTATE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetDefault("Valid");
            grdSub.View.AddTextBoxColumn("DESCRIPTION", 200);

            grdSub.View.SetKeyColumn("PLANTID", "PRICECLASSID", "PRICEITEMID");
            grdSub.View.PopulateColumns();

            #endregion 외주단가 분류별 항목
        }

        #endregion 컨텐츠 영역 초기화

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            Shown += (s, e) => SendKeys.Send("{F5}");

            grdMain.View.FocusedRowChanged += (s, e) =>
            {
                if (grdMain.View.FocusedRowHandle < 0)
                {
                    return;
                }

                grdSub.View.ClearDatas();

                Dictionary<string, object> Param = new Dictionary<string, object>
                {
                    { "P_PLANTID", grdMain.View.GetFocusedRowCellValue("PLANTID") },
                    { "P_PRICECLASSID", grdMain.View.GetFocusedRowCellValue("PRICECLASSID") },
                    { "P_VALIDSTATE", "Valid" },
                    { "LANGUAGETYPE", UserInfo.Current.LanguageType }
                };

                if (SqlExecuter.Query("GetOutsourcingYoungPongPriceClassDetail", "10001", Param) is DataTable dtDetail)
                {
                    grdSub.DataSource = dtDetail;
                }
            };

            grdMain.View.AddingNewRow += (s, e) =>
            {
                grdMain.View.SetFocusedRowCellValue("ENTERPRISEID", UserInfo.Current.Enterprise);
                grdMain.View.SetFocusedRowCellValue("PLANTID", UserInfo.Current.Plant);
            };

            grdSub.View.AddingNewRow += (s, e) =>
            {
                if (grdMain.View.DataRowCount == 0)
                {
                    int intfocusRow = grdSub.View.FocusedRowHandle;
                    grdSub.View.DeleteRow(intfocusRow);
                    return;
                }

                if (grdMain.GetChangedRows().Rows.Count != 0)
                {
                    int intfocusRow = grdSub.View.FocusedRowHandle;
                    grdSub.View.DeleteRow(intfocusRow);
                    return;
                }

                //string strPriceclassidusecnt = grdMaster.View.GetFocusedRowCellValue("PRICECLASSIDUSECNT").ToString();
                //decimal decPriceclassidusecnt = (strPriceclassidusecnt.ToString().Equals("") ? 0 : Convert.ToDecimal(strPriceclassidusecnt)); //

                // 2021-05-12 전우성 '외주단가 등록' > '공정별 단가분류 목록' 에 하나라도 등록되어있으면 추가가안되는 로직 주석
                //if (decPriceclassidusecnt > 0)
                //{
                //    int intfocusRow = grdDetail.View.FocusedRowHandle;
                //    grdDetail.View.DeleteRow(intfocusRow);
                //    return;
                //}

                grdSub.View.SetFocusedRowCellValue("ENTERPRISEID", UserInfo.Current.Enterprise);
                grdSub.View.SetFocusedRowCellValue("PLANTID", UserInfo.Current.Plant);
                grdSub.View.SetFocusedRowCellValue("PRICECLASSID", grdMain.View.GetFocusedRowCellValue("PRICECLASSID"));
                grdSub.View.SetFocusedRowCellValue("ISRANGE", "N");
            };

            // Main Grid Row 변경 전 이벤트
            grdMain.View.BeforeLeaveRow += (s, e) =>
            {
                if (e.RowHandle < 0 || _isSave)
                {
                    return;
                }

                if (grdSub.GetChangedRows() is DataTable dt)
                {
                    if (dt.Rows.Count.Equals(0))
                    {
                        return;
                    }

                    if (DialogResult.No.Equals(MSGBox.Show(MessageBoxType.Information, "ThereIsChangeMove", MessageBoxButtons.YesNo)))
                    {
                        e.Allow = false;
                        return;
                    }
                    else
                    {
                        return;
                    }
                }
            };

            grdSub.View.ShowingEditor += (s, e) =>
            {
                if (grdMain.GetChangedRows().Rows.Count != 0)
                {
                    e.Cancel = true;
                }
            };

            grdMain.ToolbarDeleteRow += (s, e) =>
            {
                if (grdMain.View.GetFocusedDataRow() is DataRow row)
                {
                    if (row.RowState != DataRowState.Added)
                    {
                        if (Convert.ToDecimal(Format.GetString(grdMain.View.GetFocusedRowCellValue("PRICECLASSIDCNT"), "0")).Equals(0))
                        {
                            row["VALIDSTATE"] = "Invalid";
                            (grdMain.View.DataSource as DataView).Table.AcceptChanges();
                        }
                    }
                }
            };

            grdSub.ToolbarDeleteRow += (s, e) =>
            {
                if (grdSub.View.GetFocusedDataRow() is DataRow row)
                {
                    if (row.RowState != DataRowState.Added)
                    {
                        if (Convert.ToDecimal(Format.GetString(grdMain.View.GetFocusedRowCellValue("PRICECLASSIDCNT"), "0")).Equals(0))
                        {
                            row["VALIDSTATE"] = "Invalid";
                            (grdSub.View.DataSource as DataView).Table.AcceptChanges();
                        }
                    }
                }
            };

            grdMain.View.Columns.ForEach(control =>
            {
                if (!control.ColumnEdit.GetType().Name.Equals("RepositoryItemButtonEdit"))
                {
                    return;
                }

                // Grid에 설비ID, 자재ID 삭제버튼 클릭시 이벤트
                (control.ColumnEdit as RepositoryItemButtonEdit).ButtonClick += (s, e) =>
                {
                    if (e.Button.Kind.Equals(ButtonPredefines.Clear))
                    {
                        switch (control.FieldName)
                        {
                            case "PRICEITEMID":
                                grdMain.View.SetRowCellValue(grdMain.View.GetFocusedDataSourceRowIndex(), "PRICEITEMNAME", string.Empty);
                                break;
                        }
                    }
                };

                // Grid에 설비ID, 자재ID text 삭제시 이벤트
                control.ColumnEdit.EditValueChanged += (s, e) =>
                {
                    if (Format.GetFullTrimString((s as DevExpress.XtraEditors.ButtonEdit).EditValue).Equals(string.Empty))
                    {
                        switch (control.FieldName)
                        {
                            case "PRICEITEMID":
                                grdMain.View.SetRowCellValue(grdMain.View.GetFocusedDataSourceRowIndex(), "PRICEITEMNAME", string.Empty);
                                break;
                        }
                    }
                };
            });
        }

        #endregion Event

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            //우선 순위 중복값 체크
            CheckPriceDateKeyColumns();

            DataTable dtSaveMaster = grdMain.GetChangedRows();
            dtSaveMaster.TableName = "listMain";
            DataTable dtSaveDetail = grdSub.GetChangedRows();
            dtSaveDetail.TableName = "listDetail";
            DataSet dsSave = new DataSet();
            dsSave.Tables.Add(dtSaveMaster);
            dsSave.Tables.Add(dtSaveDetail);
            ExecuteRule("OutsourcingYoungPongPriceClass", dsSave);
            _isSave = true;
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
                grdSub.View.ClearDatas();

                var values = Conditions.GetValues();
                values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                if (await SqlExecuter.QueryAsync("GetOutsourcingYoungPongPriceClass", "10001", values) is DataTable dt)
                {
                    if (dt.Rows.Count.Equals(0))
                    {
                        ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
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

                _isSave = false;
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
            grdSub.View.CheckValidation();

            if (grdMain.GetChangedRows().Rows.Count.Equals(0) && grdSub.GetChangedRows().Rows.Count.Equals(0))
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }

        #endregion 유효성 검사

        #region Private Function

        /// <summary>
        /// sPriceitemid 체크 처리
        /// </summary>
        /// <param name="sLotId"></param>
        /// <returns></returns>
        private int CheckPriceitemid(string sPriceitemid, int crow)
        {
            for (int i = 0; i < grdSub.View.DataRowCount; i++)
            {
                if (grdSub.View.GetRowCellValue(i, "PRICEITEMID").ToString().Equals(sPriceitemid) && i != crow)
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// 단가 기준  key 중복 체크
        /// </summary>
        /// <returns></returns>
        private void CheckPriceDateKeyColumns()
        {
            DataRow row;

            for (int irow = 0; irow < grdSub.View.DataRowCount; irow++)
            {
                row = grdSub.View.GetDataRow(irow);

                if (row.RowState == DataRowState.Added || row.RowState == DataRowState.Modified)
                {
                    string strValue = row["PRICEITEMID"].ToString();

                    if (SearchPeriodidKey(strValue, "PRICEITEMID", irow) == 0)
                    {
                        // 에러 외주단가 항목
                        throw MessageException.Create("InValidOspData007", grdSub.View.Columns["PRICEITEMID"].Caption);
                    }

                    strValue = row["PRIORITY"].ToString();

                    if (SearchPeriodidKey(strValue, "PRIORITY", irow) == 0)
                    {
                        // 에러 우선순위 중복
                        throw MessageException.Create("InValidOspData007", grdSub.View.Columns["PRIORITY"].Caption);
                    }
                }
            }
        }

        /// <summary>
        ///  중복 체크 함수
        /// </summary>
        /// <param name="dateStartdate"></param>
        /// <param name="dateEnddate"></param>
        /// <param name="icurRow"></param>
        /// <returns></returns>
        private int SearchPeriodidKey(string strValue, string colstringName, int icurRow)
        {
            int iresultRow = -1;

            for (int irow = 0; irow < grdSub.View.DataRowCount; irow++)
            {
                if (icurRow != irow)
                {
                    DataRow row = grdSub.View.GetDataRow(irow);

                    // 행 삭제만 제외
                    if (grdSub.View.IsDeletedRow(row) == false)
                    {
                        string strTemnpValue = row[colstringName].ToString();

                        if (strValue.Equals(strTemnpValue))
                        {
                            return irow;
                        }
                    }
                }
            }

            return iresultRow;
        }

        #endregion Private Function
    }
}