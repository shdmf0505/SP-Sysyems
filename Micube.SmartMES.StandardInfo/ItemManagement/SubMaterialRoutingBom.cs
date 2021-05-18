#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion using

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프 로 그 램 명  : 기준정보 > 품목 관리 > 부자재 Routing BOM
    /// 업  무  설  명  : 약품 BOM 소요량 기준정보 등록 및 관리
    /// 생    성    자  : 전우성
    /// 생    성    일  : 2020-12-14
    /// 수  정  이  력  :
    ///
    ///
    /// </summary>
    public partial class SubMaterialRoutingBom : SmartConditionManualBaseForm
    {
        #region Global Variable

        /// <summary>
        /// 공정에 따른 부자재 정보가 담긴 DataSet
        /// </summary>
        private DataSet _childDataSet;

        /// <summary>
        /// 저장의 여부 확인
        /// </summary>
        private bool _isSave = false;

        #endregion Global Variable

        #region 생성자

        public SubMaterialRoutingBom()
        {
            InitializeComponent();
        }

        #endregion 생성자

        #region 컨텐츠 영역 초기화

        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeEvent();
            InitializeGrid();
            InitializeLanguageKey();

            btnImport.Visible = false;
        }

        /// <summary>
        /// Language Key 초기화
        /// </summary>
        private void InitializeLanguageKey()
        {
            grdMain.LanguageKey = "OPERATION";
            grbRight.LanguageKey = "SUBSIDIARYINFO";
        }

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            grdMain.GridButtonItem = GridButtonItem.None;
            grdMain.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            grdMain.View.AddTextBoxColumn("USERSEQUENCE", 60).SetTextAlignment(TextAlignment.Right);
            grdMain.View.AddTextBoxColumn("ITEMID", 120);
            grdMain.View.AddTextBoxColumn("ITEMVERSION", 50).SetTextAlignment(TextAlignment.Center);
            grdMain.View.AddTextBoxColumn("PROCESSSEGMENTID", 60);
            grdMain.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);

            grdMain.View.AddTextBoxColumn("ROOT_ASSEMBLYITEMID").SetIsHidden();
            grdMain.View.AddTextBoxColumn("ROOT_ASSEMBLYITEMVERSION").SetIsHidden();
            grdMain.View.AddTextBoxColumn("PROCESSSEGMENTCLASSID").SetIsHidden();
            grdMain.View.AddTextBoxColumn("PROCESSUOM").SetIsHidden();
            grdMain.View.AddTextBoxColumn("PLANTID").SetIsHidden();
            grdMain.View.AddTextBoxColumn("PNLX").SetIsHidden();
            grdMain.View.AddTextBoxColumn("PNLY").SetIsHidden();
            grdMain.View.AddTextBoxColumn("SUBMATERIALTYPE").SetIsHidden();
            grdMain.View.AddTextBoxColumn("DATACHECK").SetIsHidden();

            grdMain.View.SetAutoFillColumn("PROCESSSEGMENTNAME");
            grdMain.View.PopulateColumns();
            grdMain.View.SetIsReadOnly();

            grdMain.ShowStatusBar = true;
        }

        #endregion 컨텐츠 영역 초기화

        #region Event

        /// <summary>
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            // 공정 Grid Row 이동전 변경사항 있는지 체크 후 있으면 미이동
            grdMain.View.BeforeLeaveRow += (s, e) =>
            {
                if (e.RowHandle < 0 || _isSave)
                {
                    return;
                }

                if (!pnlRight.Controls.Count.Equals(0) && pnlRight.Controls[0] is ISubMaterialBomRouting icontrol)
                {
                    foreach(DataTable dt in icontrol.GetData().Tables)
                    {
                        if(!dt.Rows.Count.Equals(0))
                        {
                            // 변경 내용이 있습니다. 이동하시겠습니까?
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
                    }
                }
            };

            // 공정 Grid Row 변경시 이벤트
            grdMain.View.FocusedRowChanged += (s, e) =>
            {
                if (e.FocusedRowHandle < 0)
                {
                    return;
                }

                if (!IsFocuseRowDataConvert(grdMain.View.GetFocusedDataRow(), out Dictionary<string, object> dic))
                {
                    ShowMessage(""); // 문제가 될 사항이 없음
                    return;
                }

                pnlRight.Controls.Clear();

                GetToolbarButtonById("Save").Visible = !Format.GetString(dic["SUBMATERIALTYPE"], "None").Equals("None");

                switch (Format.GetString(dic["SUBMATERIALTYPE"], "None"))
                {
                    case "HP":
                        btnImport.Text = Language.Get("COPY");
                        btnImport.Visible = true;
                        pnlRight.Controls.Add(new ucSubMaterialByHotPress(dic) { Dock = DockStyle.Fill });
                        break;

                    case "DF":
                        btnImport.Text = Language.Get("COPY");
                        btnImport.Visible = true;
                        pnlRight.Controls.Add(new ucSubMaterialByDryFilim(dic) { Dock = DockStyle.Fill });
                        break;

                    case "Chemical":
                        btnImport.Text = Language.Get("IMPORT");
                        btnImport.Visible = true;
                        // 약품인 경우에 중공정이 BUTTON 동도금 인지 확인하여 UserControl을 다르게 사용한다.
                        // BUTTON 동도금을 구분 할 수 있는 구조는 없음. 공정 그룹ID가 2518인 경우 처리.
                        if (Format.GetString(dic["PROCESSSEGMENTCLASSID"], string.Empty).Equals("2518"))
                        {
                            pnlRight.Controls.Add(new ucSubMaterialByButtonCuPlating(dic) { Dock = DockStyle.Fill });
                        }
                        else
                        {
                            pnlRight.Controls.Add(new ucSubMaterialByChemical(dic) { Dock = DockStyle.Fill });
                        }
                        break;

                    case "INK":
                        btnImport.Text = Language.Get("COPY");
                        btnImport.Visible = true;
                        pnlRight.Controls.Add(new ucSubMaterialByInk(dic) { Dock = DockStyle.Fill });
                        break;

                    default:
                        btnImport.Visible = false;
                        return;
                }

                (pnlRight.Controls[0] as ISubMaterialBomRouting).Search();
            };

            // 공정List에 부자재Type이 있는 경우 색상변경
            grdMain.View.RowCellStyle += (s, e) =>
            {
                if (e.RowHandle < 0)
                {
                    return;
                }

                if (Format.GetString(grdMain.View.GetRowCellValue(e.RowHandle, "SUBMATERIALTYPE")) is string result)
                {
                    e.Appearance.BackColor = result.Equals("None") ? e.Appearance.BackColor :
                                             Format.GetInteger(grdMain.View.GetRowCellValue(e.RowHandle, "DATACHECK")).Equals(0) ? Color.Pink : Color.Yellow;
                }
            };

            // 가져오기 버튼 클릭 이벤트
            btnImport.Click += (s, e) =>
            {
                if (grdMain.View.GetFocusedDataRow() is DataRow dr)
                {
                    if (!pnlRight.Controls.Count.Equals(0) && pnlRight.Controls[0] is ISubMaterialBomRouting subControl)
                    {
                        subControl.SetDateAsync();
                    }
                }
            };

            // 조회조건의 품목코드 x 버튼 클릭 이벤트
            if (Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID") is var control)
            {
                control.Properties.ButtonClick += (s, e) =>
                {
                    if ((s as DevExpress.XtraEditors.ButtonEdit).Properties.Buttons.IndexOf(e.Button).Equals(1))
                    {
                        Conditions.GetControl<SmartTextBox>("P_PRODUCTDEFVERSION").Text = string.Empty;
                        Conditions.GetControl<SmartTextBox>("PRODUCTDEFNAME").Text = string.Empty;
                    }
                };
            }
        }

        #endregion Event

        #region 툴바

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            try
            {
                base.OnToolbarSaveClick();

                ExecuteRule("SubMaterialBomRouting", _childDataSet);

                _isSave = true;
            }
            catch(Exception ex)
            {
                throw MessageException.Create(ex.Message);
            }
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

                int rowHandle = grdMain.View.FocusedRowHandle;

                await base.OnSearchAsync();

                grdMain.View.ClearDatas();
                pnlRight.Controls.Clear();

                Dictionary<string, object> searchParam = Conditions.GetValues();
                searchParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                if (await SqlExecuter.QueryAsync("GetAllRoutingOperationList", "10002", searchParam) is DataTable dt)
                {
                    if (dt.Rows.Count.Equals(0))
                    {
                        ShowMessage("NoSelectData");
                        return;
                    }

                    grdMain.DataSource = dt;
                    grdMain.View.FocusedRowHandle = rowHandle;
                    grdMain.View.SelectRow(rowHandle);

                    _isSave = false;
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

            var condition = Conditions.AddSelectPopup("P_PRODUCTDEFID", new SqlQuery("GetProductDefinitionList", "10002", $"PLANTID={UserInfo.Current.Plant}"), "PRODUCTDEFID", "PRODUCTDEFID")
                                      .SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)
                                      .SetPopupLayoutForm(800, 800)
                                      .SetLabel("PRODUCTDEFID")
                                      .SetPosition(2)
                                      .SetValidationIsRequired()
                                      .SetPopupResultCount(1)
                                      .SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
                                      {
                                          selectedRow.ForEach(row =>
                                          {
                                              Conditions.GetControl<SmartTextBox>("P_PRODUCTDEFVERSION").Text = Format.GetString(row.GetObject("PRODUCTDEFVERSION"), "");
                                              Conditions.GetControl<SmartTextBox>("PRODUCTDEFNAME").Text = Format.GetString(row.GetObject("PRODUCTDEFNAME"), "");
                                          });
                                      });

            condition.Conditions.AddTextBox("TXTPRODUCTDEFNAME");
            condition.Conditions.AddComboBox("PRODUCTDIVISION", new SqlQuery("GetTypeList", "10001", $"CODECLASSID=ProductDivision2", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                                .SetDefault("Product");

            condition.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            condition.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            condition.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 100);
            condition.GridColumns.AddComboBoxColumn("PRODUCTDEFTYPE", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductDefType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            condition.GridColumns.AddComboBoxColumn("PRODUCTIONTYPE", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            condition.GridColumns.AddComboBoxColumn("UNIT", 90, new SqlQuery("GetUomDefinitionList", "10001", "UOMCLASSID=OutsourcingSpec"), "UOMDEFNAME", "UOMDEFID");

            // 품목버젼, 품목명 추가
            Conditions.AddTextBox("P_PRODUCTDEFVERSION").SetLabel("PRODUCTDEFVERSION").SetIsReadOnly().SetPosition(3);
            Conditions.AddTextBox("PRODUCTDEFNAME").SetIsReadOnly().SetPosition(4);
        }

        #endregion 검색

        #region 유효성 검사

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            if (grdMain.View.GetFocusedDataRow() is DataRow dr)
            {
                if (!pnlRight.Controls.Count.Equals(0) && pnlRight.Controls[0] is ISubMaterialBomRouting control)
                {
                    control.Validation();

                    _childDataSet = control.GetData();

                    if(_childDataSet.Tables.Count.Equals(0))
                    {
                        // 저장할 데이터가 존재하지 않습니다.
                        throw MessageException.Create("NoSaveData");
                    }
                }
                else
                {
                    // 부자재 정보가 없습니다
                    throw MessageException.Create("");
                }
            }
            else
            {
                // 공정이 선택되지 않았습니다.
                throw MessageException.Create("CheckOperationByRework");
            }
        }

        #endregion 유효성 검사

        #region private fuction

        /// <summary>
        /// 선택한 Row Data를 Dictionary로 변경한다.
        /// </summary>
        /// <param name="dr">Focuse Row Data</param>
        /// <param name="dic">Create Dictionary</param>
        /// <returns>Success True</returns>
        private bool IsFocuseRowDataConvert(DataRow dr, out Dictionary<string, Object> dic)
        {
            dic = new Dictionary<string, object>();

            try
            {
                for (int i = 0; i < dr.Table.Columns.Count; i++)
                {
                    dic.Add(Format.GetString(dr.Table.Columns[i]), dr[i]);
                }

                dic.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            }
            catch
            {
                return false;
            }

            return true;
        }

        #endregion private fuction
    }
}