#region using

using DevExpress.XtraEditors.Repository;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons;
using Micube.SmartMES.Commons.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion using

namespace Micube.SmartMES.ToolManagement
{
    /// <summary>
    /// 프 로 그 램 명 : 치공구관리 > 필름관리 > 필름제작요청
    /// 업 무 설 명 : 필름제작 요청 등록화면 (New)
    /// 생 성 자 : 전우성
    /// 생 성 일 : 2021-04-01
    /// 수 정 이 력 :
    /// </summary>
    public partial class NewRequestMakeFilm : SmartConditionManualBaseForm
    {
        #region 생성자

        public NewRequestMakeFilm()
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
            InitializeEvent();
        }

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            #region 필름제작History

            grdHistory.GridButtonItem = GridButtonItem.Export;
            grdHistory.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdHistory.View.AddTextBoxColumn("FILMSEQUENCE").SetIsHidden();                                //필름시퀀스
            grdHistory.View.AddTextBoxColumn("PLANTID").SetIsHidden();                                     //PLANT
            grdHistory.View.AddTextBoxColumn("ENTERPRISEID").SetIsHidden();                                //ENTERPRISEID
            grdHistory.View.AddTextBoxColumn("REQUESTUSERID").SetIsHidden();                                 //요청자ID
            grdHistory.View.AddTextBoxColumn("FILMCATEGORYFIRST").SetIsHidden();                           //필름유형 앞자리 : 필름유형2에 사용하기 위한 Relation

            grdHistory.View.AddComboBoxColumn("FILMPROGRESSSTATUS", 80, new SqlQuery("GetCodeList", "00001", $"CODECLASSID=FilmProgressStatus", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdHistory.View.AddTextBoxColumn("PRODUCTIONTYPE", 80);                                        //생산구분
            grdHistory.View.AddTextBoxColumn("REQUESTDATE", 160).SetDisplayFormat("yyyy-MM-dd HH:mm:ss");  //의뢰일
            grdHistory.View.AddTextBoxColumn("REQUESTDEPARTMENT", 100);                                    //의뢰부서
            grdHistory.View.AddTextBoxColumn("REQUESTUSERNAME", 80);                                       //요청자명
            grdHistory.View.AddTextBoxColumn("PRODUCTDEFID", 100);                                         //품목코드
            grdHistory.View.AddTextBoxColumn("PRODUCTDEFNAME", 200).SetIsReadOnly();
            grdHistory.View.AddTextBoxColumn("PRODUCTDEFVERSION", 200).SetIsReadOnly();
            grdHistory.View.AddTextBoxColumn("FILMVERSION", 120);                                          //CAM Rev

            grdHistory.View.AddSpinEditColumn("CONTRACTIONX", 100)
                   .SetDisplayFormat("###,###,##0.#########", MaskTypes.Numeric)
                   .SetTextAlignment(TextAlignment.Right);                                              //수축률X

            grdHistory.View.AddSpinEditColumn("CONTRACTIONY", 100)
                   .SetDisplayFormat("###,###,##0.#########", MaskTypes.Numeric)
                   .SetTextAlignment(TextAlignment.Right);                                              //수축률Y

            grdHistory.View.AddComboBoxColumn("TOOLTYPE", 120, new SqlQuery("GetCodeList", "00001", $"CODECLASSID=ToolTypeA", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                   .SetLabel("FILMCATEGORY");                                                          //필름유형1

            grdHistory.View.AddComboBoxColumn("TOOLDETAILTYPE", 120, new SqlQuery("GetCodeListByFilm", "10001", "CODECLASSID=ToolDetailType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                   .SetLabel("FILMDETAILCATEGORY")
                   .SetRelationIds("FILMCATEGORYFIRST");                                               //필름유형2

            grdHistory.View.AddComboBoxColumn("FILMUSELAYER1", 120, new SqlQuery("GetCodeListByFilm", "10001", "CODECLASSID=FilmUseLayer1", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                   .SetRelationIds("FILMCATEGORYFIRST");                                                //Layer1

            grdHistory.View.AddSpinEditColumn("QTY", 80).SetDefault(0).SetValueRange(0, 999).SetTextAlignment(TextAlignment.Right);            //수량

            grdHistory.View.AddTextBoxColumn("REQUESTCOMMENT", 200);                                       //의뢰사유
            grdHistory.View.AddTextBoxColumn("AREAID", 100);                                               //입고작업장
            grdHistory.View.AddTextBoxColumn("AREANAME", 160);
            grdHistory.View.AddTextBoxColumn("JOBTYPE", 100).SetIsReadOnly();                              //작업구분

            grdHistory.View.AddComboBoxColumn("DURABLECLASSID", 120, new SqlQuery("GetCodeList", "00001", $"CODECLASSID=DurableClass", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));  //필름구분
            grdHistory.View.AddTextBoxColumn("FILMCODE", 150);                                             //필름코드
            grdHistory.View.AddTextBoxColumn("FILMNAME", 350).SetIsReadOnly();                             //필름명
            grdHistory.View.AddTextBoxColumn("CHANGECONTRACTIONX", 120);                                   //요청수축률(%)X
            grdHistory.View.AddTextBoxColumn("CHANGECONTRACTIONY", 120);                                   //요청수축률(%)Y
            grdHistory.View.AddTextBoxColumn("RESOLUTION", 80);                                            //해상도
            grdHistory.View.AddComboBoxColumn("ISCOATING", 80, new SqlQuery("GetCodeListByToolWithLanguage", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CODECLASSID=YesNo")); // 코팅유무
            grdHistory.View.AddComboBoxColumn("PRIORITYID", 80, new SqlQuery("GetCodeListByToolWithLanguage", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CODECLASSID=FilmPriorityCode")).SetLabel("PRIORITY"); // 우선순위

            grdHistory.View.PopulateColumns();
            grdHistory.View.SetIsReadOnly();

            grdHistory.ShowStatusBar = true;

            #endregion 필름제작History

            #region 필름제작요청

            grdMain.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export;
            grdMain.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            grdMain.View.AddTextBoxColumn("FILMSEQUENCE").SetIsHidden();                                //필름시퀀스
            grdMain.View.AddTextBoxColumn("PLANTID").SetIsHidden();                                     //PLANT
            grdMain.View.AddTextBoxColumn("ENTERPRISEID").SetIsHidden();                                //ENTERPRISEID
            grdMain.View.AddTextBoxColumn("REQUESTUSERID").SetIsHidden();                               //요청자ID
            grdMain.View.AddTextBoxColumn("FILMCATEGORYFIRST").SetIsHidden();                           //필름유형 앞자리 : 필름유형2에 사용하기 위한 Relation

            grdMain.View.AddTextBoxColumn("PRODUCTIONTYPE", 80).SetIsReadOnly();                        //생산구분
            grdMain.View.AddTextBoxColumn("REQUESTDATE", 160).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly();  //의뢰일
            grdMain.View.AddTextBoxColumn("REQUESTDEPARTMENT", 100).SetIsReadOnly();                    //의뢰부서
            grdMain.View.AddTextBoxColumn("REQUESTUSERNAME", 80).SetIsReadOnly();                       //요청자명

            #region Grid 품목코드

            var condition = grdMain.View.AddSelectPopupColumn("PRODUCTDEFID", 100, new SqlQuery("GetProductList", "10001", $"PLANTID={UserInfo.Current.Plant}"
                                                                                                                         , $"ENTERPRISEID={UserInfo.Current.Enterprise}"
                                                                                                                         , $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                   .SetValidationKeyColumn()
                                   .SetValidationIsRequired()
                                   .SetPopupLayout("PRODUCTNAMEANDNO", PopupButtonStyles.Ok_Cancel, true, true)
                                   .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow)
                                   .SetPopupResultCount(1)
                                   .SetPopupAutoFillColumns("PRODUCTDEFNAME")
                                   .SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGridRow) =>
                                   {
                                       selectedRow.ForEach(row =>
                                       {
                                           dataGridRow["PRODUCTDEFNAME"] = row["PRODUCTDEFNAME"];
                                           dataGridRow["PRODUCTDEFVERSION"] = row["PRODUCTDEFVERSION"];
                                           dataGridRow["PRODUCTIONTYPE"] = row["PRODUCTIONTYPE"];
                                           dataGridRow["JOBTYPE"] = row["JOBTYPE"];
                                       });
                                   });

            condition.Conditions.AddTextBox("PRODUCTDEFNAME").SetLabel("PRODUCTNAMEANDNO");

            condition.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 120);
            condition.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 60);
            condition.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 180);
            condition.GridColumns.AddTextBoxColumn("PRODUCTIONTYPE", 120);
            condition.GridColumns.AddTextBoxColumn("JOBTYPE", 100);

            #endregion Grid 품목코드

            grdMain.View.AddTextBoxColumn("PRODUCTDEFNAME", 200).SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("PRODUCTDEFVERSION", 200).SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("FILMVERSION", 120).SetValidationKeyColumn().SetValidationIsRequired();                //CAM Rev

            grdMain.View.AddSpinEditColumn("CONTRACTIONX", 100)
                   .SetDisplayFormat("###,###,##0.#########", MaskTypes.Numeric)
                   .SetTextAlignment(TextAlignment.Right)
                   .SetDefault(1);                                                                      //수축률X

            grdMain.View.AddSpinEditColumn("CONTRACTIONY", 100)
                   .SetDisplayFormat("###,###,##0.#########", MaskTypes.Numeric)
                   .SetTextAlignment(TextAlignment.Right)
                   .SetDefault(1);                                                                      //수축률Y

            grdMain.View.AddComboBoxColumn("TOOLTYPE", 120, new SqlQuery("GetCodeList", "00001", $"CODECLASSID=ToolTypeA", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                   .SetLabel("FILMCATEGORY")
                   .SetValidationKeyColumn()
                   .SetValidationIsRequired();                                                          //필름유형1

            grdMain.View.AddComboBoxColumn("TOOLDETAILTYPE", 120, new SqlQuery("GetCodeListByFilm", "10001", "CODECLASSID=ToolDetailType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                   .SetLabel("FILMDETAILCATEGORY")
                   .SetRelationIds("FILMCATEGORYFIRST")
                   .SetValidationKeyColumn()
                   .SetValidationIsRequired();                                                          //필름유형2

            grdMain.View.AddComboBoxColumn("FILMUSELAYER1", 120, new SqlQuery("GetCodeListByFilm", "10001", "CODECLASSID=FilmUseLayer1", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                   .SetLabel("LAYER2")
                   .SetRelationIds("FILMCATEGORYFIRST")
                   .SetValidationKeyColumn()
                   .SetValidationIsRequired();                                                          //Layer1

            grdMain.View.AddSpinEditColumn("QTY", 80).SetDefault(0).SetValueRange(0, 999)
                   .SetTextAlignment(TextAlignment.Right);                                              //수량

            grdMain.View.AddTextBoxColumn("REQUESTCOMMENT", 200);                                       //의뢰사유

            #region 입고작업장

            condition = grdMain.View.AddSelectPopupColumn("AREAID", 100, new SqlQuery("GetAreaListByTool", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CURRENTLOGINID={UserInfo.Current.Id}", $"ISMOD=Y"))
                               .SetPopupLayout("RECEIPTAREA", PopupButtonStyles.Ok_Cancel, true, true)
                               .SetPopupLayoutForm(400, 500, FormBorderStyle.FixedToolWindow)
                               .SetPopupResultCount(1)
                               .SetPopupAutoFillColumns("AREANAME")
                               .SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGridRow) =>
                               {
                                   selectedRow.ForEach(row =>
                                   {
                                       dataGridRow["AREANAME"] = row["AREANAME"];
                                   });
                               });

            condition.Conditions.AddTextBox("AREANAME");

            condition.GridColumns.AddTextBoxColumn("AREAID", 150);
            condition.GridColumns.AddTextBoxColumn("AREANAME", 200);

            #endregion 입고작업장

            grdMain.View.AddTextBoxColumn("AREANAME", 160).SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("JOBTYPE", 100).SetIsReadOnly();                              //작업구분

            grdMain.View.AddComboBoxColumn("DURABLECLASSID", 120, new SqlQuery("GetCodeList", "00001", $"CODECLASSID=DurableClass", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                   .SetIsReadOnly();                                                                    //필름구분

            grdMain.View.AddTextBoxColumn("FILMCODE", 150).SetIsReadOnly();                             //필름코드
            grdMain.View.AddTextBoxColumn("FILMNAME", 350).SetIsReadOnly();                             //필름명
            grdMain.View.AddTextBoxColumn("CHANGECONTRACTIONX", 120).SetIsReadOnly();                   //요청수축률(%)X
            grdMain.View.AddTextBoxColumn("CHANGECONTRACTIONY", 120).SetIsReadOnly();                   //요청수축률(%)Y
            grdMain.View.AddTextBoxColumn("RESOLUTION", 80);                                            //해상도
            grdMain.View.AddComboBoxColumn("ISCOATING", 80, new SqlQuery("GetCodeListByToolWithLanguage", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CODECLASSID=YesNo")); // 코팅유무
            grdMain.View.AddComboBoxColumn("PRIORITYID", 80, new SqlQuery("GetCodeListByToolWithLanguage", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CODECLASSID=FilmPriorityCode")).SetLabel("PRIORITY"); // 우선순위

            grdMain.View.AddTextBoxColumn("MODIFIER", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdMain.View.AddTextBoxColumn("MODIFIEDTIME", 130).SetIsReadOnly().SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetTextAlignment(TextAlignment.Center);

            grdMain.View.PopulateColumns();

            grdMain.ShowStatusBar = true;

            #endregion 필름제작요청
        }

        /// <summary>
        /// Language Key 초기화
        /// </summary>
        private void InitializeLanguageKey()
        {
            grdMain.LanguageKey = "REQUESTMAKINGFILM";
            grdHistory.LanguageKey = "FILMHISTORY";
        }

        #endregion 컨텐츠 영역 초기화

        #region Event

        /// <summary>
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            // Grid에 신규Row 발생시 이벤트
            grdMain.View.AddingNewRow += (s, e) =>
            {
                e.NewRow["REQUESTDATE"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                e.NewRow["PLANTID"] = UserInfo.Current.Plant;
                e.NewRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                e.NewRow["REQUESTUSERID"] = UserInfo.Current.Id;
                e.NewRow["REQUESTDEPARTMENT"] = UserInfo.Current.Department;
                e.NewRow["DURABLECLASSID"] = "ToolTypeA";
                e.NewRow["ISCOATING"] = "N";
                e.NewRow["PRIORITYID"] = "Normal";
            };

            // Grid 값 변경시 이벤트
            grdMain.View.CellValueChanged += (s, e) =>
            {
                if (e.RowHandle < 0)
                {
                    return;
                }

                DataRow dr = grdMain.View.GetDataRow(e.RowHandle);

                switch (e.Column.FieldName)
                {
                    case "TOOLTYPE":
                        // 필름유형1의 앞자리를 FILMCATEGORYFIRST에 입력하여 필름유형2, Layer1 Relation에 사용한다.
                        dr["FILMCATEGORYFIRST"] = Format.GetString(dr["TOOLTYPE"], string.Empty).Substring(0, 1);
                        dr["TOOLDETAILTYPE"] = null;
                        dr["FILMUSELAYER1"] = null;
                        break;

                    case "CONTRACTIONX":
                        dr["CHANGECONTRACTIONX"] = Math.Round((Format.GetDouble(dr["CONTRACTIONX"], 0) - 1) * 100, 2);
                        break;

                    case "CONTRACTIONY":
                        dr["CHANGECONTRACTIONY"] = Math.Round((Format.GetDouble(dr["CONTRACTIONY"], 0) - 1) * 100, 2);
                        break;
                }
            };

            grdMain.View.Columns.ForEach(control =>
            {
                if (!control.ColumnEdit.GetType().Name.Equals("RepositoryItemButtonEdit"))
                {
                    return;
                }

                // Grid에 품목ID, 작업장ID 삭제버튼 클릭시 이벤트
                (control.ColumnEdit as RepositoryItemButtonEdit).ButtonClick += (s, e) =>
                {
                    if (e.Button.Kind.Equals(DevExpress.XtraEditors.Controls.ButtonPredefines.Clear))
                    {
                        switch (control.FieldName)
                        {
                            case "PRODUCTDEFID":
                                grdMain.View.SetRowCellValue(grdMain.View.GetFocusedDataSourceRowIndex(), "PRODUCTDEFNAME", string.Empty);
                                grdMain.View.SetRowCellValue(grdMain.View.GetFocusedDataSourceRowIndex(), "PRODUCTDEFVERSION", string.Empty);
                                grdMain.View.SetRowCellValue(grdMain.View.GetFocusedDataSourceRowIndex(), "PRODUCTIONTYPE", string.Empty);
                                grdMain.View.SetRowCellValue(grdMain.View.GetFocusedDataSourceRowIndex(), "JOBTYPE", string.Empty);
                                break;

                            case "AREAID":
                                grdMain.View.SetRowCellValue(grdMain.View.GetFocusedDataSourceRowIndex(), "AREANAME", string.Empty);
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
                            case "PRODUCTDEFID":
                                grdMain.View.SetRowCellValue(grdMain.View.GetFocusedDataSourceRowIndex(), "PRODUCTDEFNAME", string.Empty);
                                grdMain.View.SetRowCellValue(grdMain.View.GetFocusedDataSourceRowIndex(), "PRODUCTDEFVERSION", string.Empty);
                                grdMain.View.SetRowCellValue(grdMain.View.GetFocusedDataSourceRowIndex(), "PRODUCTIONTYPE", string.Empty);
                                grdMain.View.SetRowCellValue(grdMain.View.GetFocusedDataSourceRowIndex(), "JOBTYPE", string.Empty);
                                break;

                            case "AREAID":
                                grdMain.View.SetRowCellValue(grdMain.View.GetFocusedDataSourceRowIndex(), "AREANAME", string.Empty);
                                break;
                        }
                    }
                };
            });

            btnInsert.Click += (s, e) =>
            {
                DataTable rootDt = grdMain.DataSource as DataTable;
                DataRow newRow;
                foreach (DataRow dr in grdHistory.View.GetCheckedRows().Rows)
                {
                    newRow = rootDt.NewRow();

                    newRow["REQUESTDATE"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    newRow["PLANTID"] = UserInfo.Current.Plant;
                    newRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                    newRow["REQUESTUSERID"] = UserInfo.Current.Id;
                    newRow["REQUESTDEPARTMENT"] = UserInfo.Current.Department;
                    newRow["FILMCATEGORYFIRST"] = dr["FILMCATEGORYFIRST"];
                    newRow["PRODUCTIONTYPE"] = dr["PRODUCTIONTYPE"];
                    newRow["REQUESTUSERNAME"] = "";
                    newRow["PRODUCTDEFID"] = dr["PRODUCTDEFID"];
                    newRow["PRODUCTDEFNAME"] = dr["PRODUCTDEFNAME"];
                    newRow["PRODUCTDEFVERSION"] = dr["PRODUCTDEFVERSION"];
                    newRow["FILMVERSION"] = dr["FILMVERSION"];
                    newRow["FILMNAME"] = dr["FILMNAME"];
                    newRow["CONTRACTIONX"] = dr["CONTRACTIONX"];
                    newRow["CONTRACTIONY"] = dr["CONTRACTIONY"];
                    newRow["TOOLTYPE"] = dr["TOOLTYPE"];
                    newRow["TOOLDETAILTYPE"] = dr["TOOLDETAILTYPE"];
                    newRow["FILMUSELAYER1"] = dr["FILMUSELAYER1"];
                    newRow["QTY"] = dr["QTY"];
                    newRow["REQUESTCOMMENT"] = "";
                    newRow["JOBTYPE"] = dr["JOBTYPE"];
                    newRow["DURABLECLASSID"] = "ToolTypeA";
                    newRow["FILMCODE"] = "";
                    newRow["CHANGECONTRACTIONX"] = dr["CHANGECONTRACTIONX"];
                    newRow["CHANGECONTRACTIONY"] = dr["CHANGECONTRACTIONY"];
                    newRow["RESOLUTION"] = dr["RESOLUTION"];
                    newRow["ISCOATING"] = dr["ISCOATING"];
                    newRow["PRIORITYID"] = dr["PRIORITYID"];
                    newRow["AREAID"] = dr["AREAID"];
                    newRow["AREANAME"] = dr["AREANAME"];

                    rootDt.Rows.Add(newRow.ItemArray);
                }

                grdHistory.View.UncheckedAll();
            };
        }

        #endregion Event

        #region 툴바

        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            ExecuteRule("FilmRequestMaking", grdMain.GetChangedRows());
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

                grdHistory.View.ClearDatas();
                grdMain.View.ClearDatas();

                Dictionary<string, object> param = Conditions.GetValues();
                param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                param.Add("USERID", UserInfo.Current.Id);

                grdMain.DataSource = await SqlExecuter.QueryAsync("GetRequestMakingFilmListByTool", "10001", param);
                grdHistory.DataSource = await SqlExecuter.QueryAsync("GetRequestMakingFilmListByTool", "10002", param);
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

            if(grdMain.GetChangedRows() is DataTable dt)
            {
                if(dt.Rows.Count.Equals(0))
                {
                    // 저장할 데이터가 존재하지 않습니다.
                    throw MessageException.Create("NoSaveData");
                }

                foreach(DataRow dr in dt.Rows)
                {
                    if(Format.GetInteger(dr["QTY"]) < 1)
                    {
                        // 의뢰수량은 0이상이어야 합니다.
                        throw MessageException.Create("ReliabilityVerificationRequestQty");
                    }
                }
            }
        }

        #endregion 유효성 검사
    }
}