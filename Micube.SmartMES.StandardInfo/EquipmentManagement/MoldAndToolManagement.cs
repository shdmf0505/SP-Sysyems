#region using

using DevExpress.XtraEditors.Repository;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Grid;
using Micube.Framework.SmartControls.Grid.BandedGrid;
using Micube.SmartMES.Commons.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion using

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    ///     프 로 그 램 명  : 기준정보 > 설비 기준 정보 > 금형/치공구 Master
    ///     업  무  설  명  : 금형/치공구 정보를 관리한다.
    ///     생    성    자  : 강유라
    ///     생    성    일  : 2019-05-15
    ///     수  정  이  력  :
    ///     *************수정필요부분 :  테이블에 컬럼 추가 후 룰, 프로시저 컬럼수정
    /// </summary>
    public partial class MoldAndToolManagement : SmartConditionManualBaseForm
    {
        #region 생성자

        public MoldAndToolManagement()
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
        ///     그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            #region original

            grdMoldAndTool.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdMoldAndTool.View.AddTextBoxColumn("ENTERPRISEID", 150).SetIsHidden();
            grdMoldAndTool.View.AddTextBoxColumn("PLANTID", 55).SetLabel("SITE").SetTextAlignment(TextAlignment.Center).SetIsReadOnly();

            grdMoldAndTool.View.AddComboBoxColumn("DURABLECLASSID", 100, new SqlQuery("GetTypeList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "CODECLASSID=DurableClass"))
                               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false)
                               .SetTextAlignment(TextAlignment.Center)
                               .SetValidationKeyColumn();

            //Tool 구분 - 필수 선택 (ID채번에 사용)
            grdMoldAndTool.View.AddComboBoxColumn("TOOLTYPE", 130, new SqlQuery("GetToolTypeByClassId", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false)
                               .SetTextAlignment(TextAlignment.Center)
                               .SetRelationIds("DURABLECLASSID")
                               .SetValidationKeyColumn();

            //Tool 세부구분 (차수) - 필수 선택 (ID채번에 사용)
            grdMoldAndTool.View.AddComboBoxColumn("TOOLDETAILTYPE", 100, new SqlQuery("GetDurableClassType2", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false)
                               .SetTextAlignment(TextAlignment.Center)
                               .SetRelationIds("DURABLECLASSID", "TOOLTYPE")
                               .SetValidationKeyColumn();

            grdMoldAndTool.View.AddTextBoxColumn("DURABLEDEFID", 120);
            grdMoldAndTool.View.AddTextBoxColumn("DURABLEDEFVERSION", 100).SetTextAlignment(TextAlignment.Center);
            grdMoldAndTool.View.AddLanguageColumn("DURABLEDEFNAME", 200);

            // 품목코드
            Initialize_ProductPopup(grdMoldAndTool);

            grdMoldAndTool.View.AddTextBoxColumn("PRODUCTDEFNAME", 250).SetIsReadOnly();
            grdMoldAndTool.View.AddTextBoxColumn("PRODUCTDEFVERSION", 85).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();

            //Tool 형식
            grdMoldAndTool.View.AddComboBoxColumn("TOOLFORMCODE", 150, new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "CODECLASSID=ToolForm"))
                               .SetTextAlignment(TextAlignment.Center);

            grdMoldAndTool.View.AddSpinEditColumn("USEDLIMIT", 85).SetDisplayFormat("#,##0", MaskTypes.Numeric, false);
            grdMoldAndTool.View.AddSpinEditColumn("CLEANLIMIT", 110).SetDisplayFormat("#,##0", MaskTypes.Numeric, false);
            grdMoldAndTool.View.AddSpinEditColumn("THICKNESSLIMIT", 85);
            grdMoldAndTool.View.AddSpinEditColumn("USEDFACTOR", 60);

            grdMoldAndTool.View.AddComboBoxColumn("FILMUSELAYER1", 80, new SqlQuery("GetFilmLayer1", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                               .SetRelationIds("DURABLECLASSID", "TOOLTYPE");
            grdMoldAndTool.View.AddComboBoxColumn("FILMUSELAYER2", 80, new SqlQuery("GetFilmLayer2", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                               .SetRelationIds("DURABLECLASSID", "TOOLTYPE");
            grdMoldAndTool.View.AddTextBoxColumn("DESCRIPTION", 180).SetLabel("REMARK");

            //유효상태, 생성자, 수정자...
            grdMoldAndTool.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                               .SetDefault("Valid")
                               .SetTextAlignment(TextAlignment.Center)
                               .SetValidationIsRequired();

            grdMoldAndTool.View.AddTextBoxColumn("CREATOR", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdMoldAndTool.View.AddTextBoxColumn("CREATEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdMoldAndTool.View.AddTextBoxColumn("MODIFIER", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdMoldAndTool.View.AddTextBoxColumn("MODIFIEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly().SetTextAlignment(TextAlignment.Center);

            grdMoldAndTool.View.PopulateColumns();

            #endregion original

            #region 금형, 목형

            grdMold.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdMold.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export;

            grdMold.View.AddTextBoxColumn("ENTERPRISEID", 150).SetIsHidden();
            //SITE
            grdMold.View.AddTextBoxColumn("PLANTID", 50).SetLabel("SITE").SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            //치공구ID
            grdMold.View.AddTextBoxColumn("DURABLEDEFID", 120);
            //소분류ID
            grdMold.View.AddComboBoxColumn("DURABLECLASSID", 80, new SqlQuery("GetTypeList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "CODECLASSID=DurableClass"))
                        .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false)
                        .SetTextAlignment(TextAlignment.Center)
                        .SetValidationKeyColumn();
            //Tool 유형1
            //Tool 구분 - 필수 선택 (ID채번에 사용)
            grdMold.View.AddComboBoxColumn("TOOLTYPE", 100, new SqlQuery("GetToolTypeByClassId", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                        .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false)
                        .SetRelationIds("DURABLECLASSID")
                        .SetLabel("TOOLCATEGORYDETAIL")
                        .SetValidationKeyColumn();
            //Tool 유형2
            //Tool 세부구분 (차수) - 필수 선택 (ID채번에 사용)
            grdMold.View.AddComboBoxColumn("TOOLDETAILTYPE", 90, new SqlQuery("GetDurableClassType2", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                        .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false)
                        .SetTextAlignment(TextAlignment.Center)
                        .SetRelationIds("DURABLECLASSID", "TOOLTYPE").SetLabel("TOOLDETAIL")
                        .SetValidationKeyColumn();

            // 치공구 버전으로 ID 생성하기 때문에 필수 옵션 추가
            grdMold.View.AddTextBoxColumn("DURABLEDEFVERSION", 80)
                        .SetTextAlignment(TextAlignment.Center)
                        .SetValidationIsRequired();

            //TOOL 형식
            grdMold.View.AddComboBoxColumn("TOOLFORMCODE", 90, new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "CODECLASSID=ToolForm"))
                        .SetTextAlignment(TextAlignment.Center);
            //연배
            grdMold.View.AddSpinEditColumn("USEDFACTOR", 80);
            //Layer
            grdMold.View.AddComboBoxColumn("FILMUSELAYER1", 65, new SqlQuery("GetCodeList", "00001", "CODECLASSID=UserLayer", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetLabel("USERLAYER");
            //치공구명
            grdMold.View.AddTextBoxColumn("DURABLEDEFNAME$$KO-KR", 140);

            #region 품목 코드

            //품목코드
            var controls = grdMold.View.AddSelectPopupColumn("PRODUCTDEFID", 90, new SqlQuery("GetProductDefList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                       .SetPopupLayout("PRODUCT", PopupButtonStyles.Ok_Cancel, true, false)
                                       .SetPopupResultCount()
                                       .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                                       .SetPopupResultMapping("DURABLEDEFNAME", "PRODUCTDEFID")
                                       .SetLabel("PRODUCTDEFID")
                                       .SetPopupApplySelection((selectedRows, gridRow) =>
                                       {
                                           foreach (DataRow row in selectedRows)
                                           {
                                               gridRow["PRODUCTDEFNAME"] = row["PRODUCTDEFNAME"];
                                               gridRow["PRODUCTDEFVERSION"] = row["PRODUCTDEFVERSION"];
                                               gridRow["PCSPNL"] = row["PCSPNL"];

                                               if (!string.IsNullOrEmpty(gridRow["USEDFACTOR"].ToString()) && !string.IsNullOrEmpty(gridRow["PCSPNL"].ToString()))
                                               {
                                                   double usedfactor = Convert.ToDouble(gridRow["USEDFACTOR"].ToString());
                                                   double pcspnl = Convert.ToDouble(gridRow["PCSPNL"].ToString());

                                                   if (usedfactor > 0)
                                                   {
                                                       int hitcount = Convert.ToInt32(pcspnl / usedfactor);
                                                       gridRow["HITCOUNT"] = Convert.ToString(hitcount);
                                                   }
                                               }
                                           }
                                       });

            controls.Conditions.AddTextBox("PRODUCTDEF");
            // 팝업 그리드
            controls.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 120);
            controls.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 60);
            controls.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            controls.GridColumns.AddTextBoxColumn("PCSPNL", 80);

            #endregion 품목 코드

            //내부rev.
            grdMold.View.AddTextBoxColumn("PRODUCTDEFVERSION", 70).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            //품목명
            grdMold.View.AddTextBoxColumn("PRODUCTDEFNAME", 210).SetIsReadOnly();
            //타수(PNL기준)
            grdMold.View.AddSpinEditColumn("HITCOUNT", 90).SetDisplayFormat("#,##0", MaskTypes.Numeric, false);
            //비고
            grdMold.View.AddTextBoxColumn("DESCRIPTION", 120).SetLabel("REMARK");
            //보증타수
            grdMold.View.AddSpinEditColumn("USEDLIMIT", 80).SetDisplayFormat("#,##0", MaskTypes.Numeric, false);
            //연마기준타수
            grdMold.View.AddSpinEditColumn("CLEANLIMIT", 90).SetDisplayFormat("#,##0", MaskTypes.Numeric, false);
            //두께기준
            grdMold.View.AddSpinEditColumn("THICKNESSLIMIT", 80);
            // 합수 (삭제 요청에서 연배랑 동일하게 유지하는 걸로 요청사항 변경 - 2020/04/03 이용희)
            grdMold.View.AddSpinEditColumn("ARRAY", 60);
            //치공구명
            grdMold.View.AddTextBoxColumn("DURABLEDEFNAME$$EN-US", 140);
            //치공구명
            grdMold.View.AddTextBoxColumn("DURABLEDEFNAME$$ZH-CN", 140);
            //치공구명
            grdMold.View.AddTextBoxColumn("DURABLEDEFNAME$$VI-VN", 140);
            // 배열수  - PNL 당 타수를 위해 가지고 있음
            grdMold.View.AddSpinEditColumn("PCSPNL", 90).SetIsHidden();
            //유효상태
            grdMold.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                        .SetDefault("Valid")
                        .SetTextAlignment(TextAlignment.Center)
                        .SetValidationIsRequired();
            //생성자
            grdMold.View.AddTextBoxColumn("CREATOR", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            //생성일
            grdMold.View.AddTextBoxColumn("CREATEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            //수정자
            grdMold.View.AddTextBoxColumn("MODIFIER", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            //수정일
            grdMold.View.AddTextBoxColumn("MODIFIEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly().SetTextAlignment(TextAlignment.Center);

            grdMold.View.PopulateColumns();

            #endregion 금형, 목형

            #region BBT,JIG

            grdBBT.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdBBT.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export;

            grdBBT.View.AddTextBoxColumn("ENTERPRISEID", 150).SetIsHidden();
            //SITE
            grdBBT.View.AddTextBoxColumn("PLANTID", 50).SetLabel("SITE").SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            //치공구ID
            grdBBT.View.AddTextBoxColumn("DURABLEDEFID", 120);
            //소분류ID
            grdBBT.View.AddComboBoxColumn("DURABLECLASSID", 80, new SqlQuery("GetTypeList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "CODECLASSID=DurableClass"))
                       .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false)
                       .SetTextAlignment(TextAlignment.Center)
                       .SetValidationKeyColumn();
            //Tool 유형1
            //Tool 구분 - 필수 선택 (ID채번에 사용)
            grdBBT.View.AddComboBoxColumn("TOOLTYPE", 100, new SqlQuery("GetToolTypeByClassId", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                       .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false)
                       .SetRelationIds("DURABLECLASSID")
                       .SetLabel("TOOLCATEGORYDETAIL")
                       .SetValidationKeyColumn();
            //Tool 유형2
            //Tool 세부구분 (차수) - 필수 선택 (ID채번에 사용)
            grdBBT.View.AddComboBoxColumn("TOOLDETAILTYPE", 90, new SqlQuery("GetDurableClassType2", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                       .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false)
                       .SetRelationIds("DURABLECLASSID", "TOOLTYPE").SetLabel("TOOLDETAIL")
                       .SetTextAlignment(TextAlignment.Center)
                       .SetValidationKeyColumn();

            // 치공구 버전으로 ID 생성하기 때문에 필수 옵션 추가
            grdBBT.View.AddTextBoxColumn("DURABLEDEFVERSION", 80)
                       .SetTextAlignment(TextAlignment.Center)
                       .SetValidationIsRequired();
            //품목코드
            Initialize_ProductPopup(grdBBT);

            //내부rev.
            grdBBT.View.AddTextBoxColumn("PRODUCTDEFVERSION", 70).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            //품목명
            grdBBT.View.AddTextBoxColumn("PRODUCTDEFNAME", 210).SetIsReadOnly();
            //TOOL 형식
            grdBBT.View.AddComboBoxColumn("TOOLFORMCODE", 90, new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "CODECLASSID=ToolForm"))
                       .SetTextAlignment(TextAlignment.Center);
            //TOOL 종류 - 2021.01.25 전우성 TOOL 종류 숨김. 굳이 콤포필요가 없어 text로 변경
            grdBBT.View.AddTextBoxColumn("TOOLKIND").SetIsHidden();
            //grdBBT.View.AddComboBoxColumn("TOOLKIND", 100, new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "CODECLASSID = TRIMMING"));
            //연배
            grdBBT.View.AddSpinEditColumn("USEDFACTOR", 60);
            //SCALE
            grdBBT.View.AddSpinEditColumn("SCALEX", 80).SetDisplayFormat("#,##0.00", MaskTypes.Numeric, true);
            grdBBT.View.AddSpinEditColumn("SCALEY", 80).SetDisplayFormat("#,##0.00", MaskTypes.Numeric, true);

            #region 제작처

            controls = grdMold.View.AddSelectPopupColumn("MANUFACTURER", 120, new SqlQuery("GetVendorList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                   .SetPopupLayout("MANUFACTURER", PopupButtonStyles.Ok_Cancel, true, false)
                                   .SetPopupResultCount()
                                   .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                                   .SetPopupResultMapping("MANUFACTURER", "");

            controls.Conditions.AddTextBox("VENDORID");

            controls.GridColumns.AddTextBoxColumn("VENDORID", 100);
            controls.GridColumns.AddTextBoxColumn("VENDORNAME", 150);

            #endregion 제작처

            #region 입고작업장

            grdBBT.View.AddTextBoxColumn("RECEIPTAREAID", 100).SetIsHidden();
            //controls = grdBBT.View.AddSelectPopupColumn("RECEIPTAREA", 120, new SqlQuery("GetAreaListByTool", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
            //                      .SetPopupLayout("RECEIPTAREA", PopupButtonStyles.Ok_Cancel, true, false)
            //                      .SetPopupResultCount()
            //                      .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
            //                      .SetPopupAutoFillColumns("AREANAME")
            //                      .SetPopupResultMapping("RECEIPTAREA", "AREANAME")
            //                      .SetPopupApplySelection((selectedRows, gridRow) =>
            //                      {
            //                          foreach (DataRow dataRow in selectedRows)
            //                          {
            //                              gridRow["RECEIPTAREAID"] = dataRow["AREAID"].ToString();
            //                          }
            //                      });

            //controls.Conditions.AddTextBox("AREANAME");

            //controls.GridColumns.AddTextBoxColumn("AREAID", 150);
            //controls.GridColumns.AddTextBoxColumn("AREANAME", 200);

            #endregion 입고작업장

            //비고
            grdBBT.View.AddTextBoxColumn("DESCRIPTION", 180).SetLabel("REMARK");
            //치공구명
            grdBBT.View.AddLanguageColumn("DURABLEDEFNAME", 140);
            //유효상태
            grdBBT.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                       .SetDefault("Valid")
                       .SetValidationIsRequired()
                       .SetTextAlignment(TextAlignment.Center);
            //생성자
            grdBBT.View.AddTextBoxColumn("CREATOR", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            //생성일
            grdBBT.View.AddTextBoxColumn("CREATEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            //수정자
            grdBBT.View.AddTextBoxColumn("MODIFIER", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            //수정일
            grdBBT.View.AddTextBoxColumn("MODIFIEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly().SetTextAlignment(TextAlignment.Center);

            grdBBT.View.PopulateColumns();

            #endregion BBT,JIG

            #region 필름

            grdFilm.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdFilm.GridButtonItem = GridButtonItem.Export;

            grdFilm.View.AddTextBoxColumn("ENTERPRISEID", 150).SetIsHidden();
            //SITE
            grdFilm.View.AddTextBoxColumn("PLANTID", 50).SetLabel("SITE").SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            //필름ID
            grdFilm.View.AddTextBoxColumn("DURABLEDEFID", 120).SetLabel("FILMCODE");
            //구분
            grdFilm.View.AddComboBoxColumn("DURABLECLASSID", 80, new SqlQuery("GetTypeList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "CODECLASSID=DurableClass"))
                        .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false)
                        .SetTextAlignment(TextAlignment.Center)
                        .SetValidationKeyColumn().SetIsReadOnly();
            //필름 유형1
            //Tool 구분 - 필수 선택 (ID채번에 사용)
            grdFilm.View.AddComboBoxColumn("TOOLTYPE", 100, new SqlQuery("GetToolTypeByClassId", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                        .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false)
                        .SetRelationIds("DURABLECLASSID")
                        .SetLabel("FILMDCATEGORY")
                        .SetValidationKeyColumn();
            //필름 유형2
            //Tool 세부구분 (차수) - 필수 선택 (ID채번에 사용)
            grdFilm.View.AddComboBoxColumn("TOOLDETAILTYPE", 90, new SqlQuery("GetDurableClassType2", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                        .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false)
                        .SetTextAlignment(TextAlignment.Center)
                        .SetRelationIds("DURABLECLASSID", "TOOLTYPE")
                        .SetLabel("FILMDETAILCATEGORY")
                        .SetValidationKeyColumn();
            //품목코드
            Initialize_ProductPopup(grdFilm);
            //품목명
            grdFilm.View.AddTextBoxColumn("PRODUCTDEFNAME", 210).SetIsReadOnly();
            //내부rev.
            grdFilm.View.AddTextBoxColumn("PRODUCTDEFVERSION", 70).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            //필름명
            grdFilm.View.AddLanguageColumn("DURABLEDEFNAME", 140).SetLabel("FILMNAME");
            //Layer1
            grdFilm.View.AddComboBoxColumn("FILMUSELAYER1", 80, new SqlQuery("GetFilmLayer1", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                        .SetRelationIds("DURABLECLASSID", "TOOLTYPE");
            //Layer2
            grdFilm.View.AddComboBoxColumn("FILMUSELAYER2", 80, new SqlQuery("GetFilmLayer2", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                        .SetRelationIds("DURABLECLASSID", "TOOLTYPE");
            //해상도
            grdFilm.View.AddTextBoxColumn("RESOLUTION", 100).SetIsReadOnly();
            //코팅유무
            grdFilm.View.AddTextBoxColumn("ISCOATING", 80).SetIsReadOnly();
            //수축률X
            grdFilm.View.AddTextBoxColumn("CONTRACTIONX", 80).SetDisplayFormat("0.#####").SetIsReadOnly().IsFloatValue = true;
            //수축률Y
            grdFilm.View.AddTextBoxColumn("CONTRACTIONY", 80).SetDisplayFormat("0.#####").SetIsReadOnly().IsFloatValue = true;
            //요청수축률(%)X
            grdFilm.View.AddTextBoxColumn("CHANGECONTRACTIONX", 80).SetIsReadOnly();
            //요청수축률(%)Y
            grdFilm.View.AddTextBoxColumn("CHANGECONTRACTIONY", 80).SetIsReadOnly();
            //수량
            grdFilm.View.AddSpinEditColumn("QTY", 80).SetIsReadOnly();
            //우선순위
            grdFilm.View.AddComboBoxColumn("PRIORITY", 80, new SqlQuery("GetCodeListByToolWithLanguage", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CODECLASSID=FilmPriorityCode"), "CODENAME", "CODEID")
                        .SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            //입고작업장
            grdFilm.View.AddTextBoxColumn("RECEIPTAREA", 150).SetIsReadOnly();
            //비고
            grdFilm.View.AddTextBoxColumn("DESCRIPTION", 180).SetLabel("REMARK");
            //유효상태
            grdFilm.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                        .SetDefault("Valid")
                        .SetTextAlignment(TextAlignment.Center)
                        .SetValidationIsRequired();
            //생성자
            grdFilm.View.AddTextBoxColumn("CREATOR", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            //생성일
            grdFilm.View.AddTextBoxColumn("CREATEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            //수정자
            grdFilm.View.AddTextBoxColumn("MODIFIER", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            //수정일
            grdFilm.View.AddTextBoxColumn("MODIFIEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly().SetTextAlignment(TextAlignment.Center);

            grdFilm.View.PopulateColumns();

            #endregion 필름
        }

        /// <summary>
        /// 품목코드 팝업 생성
        /// </summary>
        /// <param name="grid"></param>
        private void Initialize_ProductPopup(SmartBandedGrid grid)
        {
            //팝업 컬럼 설정
            var control = grid.View.AddSelectPopupColumn("PRODUCTDEFID", 90, new SqlQuery("GetProductDefList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                    .SetPopupLayout("PRODUCT", PopupButtonStyles.Ok_Cancel, true, false)
                                    .SetPopupResultCount()
                                    .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                                    .SetPopupResultMapping("DURABLEDEFNAME", "PRODUCTDEFID")
                                    //.SetRelationIds("PLANTID")
                                    .SetLabel("PRODUCTDEFID")
                                    .SetPopupApplySelection((selectedRows, gridRow) =>
                                    {
                                        foreach (DataRow row in selectedRows)
                                        {
                                            gridRow["PRODUCTDEFNAME"] = row["PRODUCTDEFNAME"].ToString();
                                            gridRow["PRODUCTDEFVERSION"] = row["PRODUCTDEFVERSION"].ToString();
                                        }
                                    });

            control.Conditions.AddTextBox("PRODUCTDEF");

            // 팝업 그리드
            control.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            control.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            control.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 200);
        }

        #endregion 컨텐츠 영역 초기화

        #region Event

        /// <summary>
        ///     이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            grdMold.View.CellValueChanged += View_CellValueChanged;
            grdBBT.View.CellValueChanged += View_CellValueChanged;

            grdMoldAndTool.View.AddingNewRow += View_AddingNewRow;
            grdMold.View.AddingNewRow += View_AddingNewRow;
            grdBBT.View.AddingNewRow += View_AddingNewRow;
            grdFilm.View.AddingNewRow += View_AddingNewRow;

            grdMoldAndTool.View.ShowingEditor += View_ShowingEditor;

            (grdMold.View.Columns["PRODUCTDEFID"].ColumnEdit as RepositoryItemButtonEdit).ButtonClick += MoldAndToolManagement_ButtonClick;
            (grdBBT.View.Columns["PRODUCTDEFID"].ColumnEdit as RepositoryItemButtonEdit).ButtonClick += MoldAndToolManagement_ButtonClick;
            (grdFilm.View.Columns["PRODUCTDEFID"].ColumnEdit as RepositoryItemButtonEdit).ButtonClick += MoldAndToolManagement_ButtonClick;

            // File Tab에는 Save 기능 없음
            tabMoldAndToolManager.SelectedPageChanged += (s, e) => GetToolbarButtonById("Save").Visible = e.Page.Equals(pageFilm) ? false : true;
        }

        /// <summary>
        /// [금형, 목형], [BBT, JIG] 값변경시 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                return;
            }

            SmartBandedGridView view = sender as SmartBandedGridView;
            DataRow selectRow = view.GetFocusedDataRow();

            RepositoryItemLookUpEdit[] prevEdit = new RepositoryItemLookUpEdit[3];

            prevEdit[0] = view.Columns["DURABLECLASSID"].ColumnEdit as RepositoryItemLookUpEdit;
            prevEdit[1] = view.Columns["TOOLTYPE"].ColumnEdit as RepositoryItemLookUpEdit;
            prevEdit[2] = view.Columns["TOOLDETAILTYPE"].ColumnEdit as RepositoryItemLookUpEdit;

            string durableClassId = Format.GetString(prevEdit[0].GetDataSourceValue("CODENAME", prevEdit[0].GetDataSourceRowIndex("CODEID", selectRow["DURABLECLASSID"])));
            string toolType = Format.GetString(prevEdit[1].GetDataSourceValue("CODENAME", prevEdit[1].GetDataSourceRowIndex("CODEID", selectRow["TOOLTYPE"])));
            string toolDetailType = Format.GetString(prevEdit[2].GetDataSourceValue("CODENAME", prevEdit[2].GetDataSourceRowIndex("CODEID", selectRow["TOOLDETAILTYPE"])));
            string durableDefVer = Format.GetString(selectRow["DURABLEDEFVERSION"]);

            RepositoryItemLookUpEdit edit = e.Column.ColumnEdit as RepositoryItemLookUpEdit;

            switch (e.Column.FieldName)
            {
                case "DURABLECLASSID"://소분류ID
                    durableClassId = Format.GetString(edit.GetDataSourceValue("CODENAME", edit.GetDataSourceRowIndex("CODEID", e.Value)));
                    break;

                case "TOOLTYPE"://TOOL유형1
                    toolType = Format.GetString(edit.GetDataSourceValue("CODENAME", edit.GetDataSourceRowIndex("CODEID", e.Value)));
                    break;

                case "TOOLDETAILTYPE"://TOOL유형2
                    toolDetailType = Format.GetString(edit.GetDataSourceValue("CODENAME", edit.GetDataSourceRowIndex("CODEID", e.Value)));
                    break;

                case "DURABLEDEFVERSION"://치공구버전
                    durableDefVer = Format.GetString(e.Value);
                    break;
            }

            view.CellValueChanged -= View_CellValueChanged;
            string languageValue;

            if (tabMoldAndToolManager.SelectedTabPageIndex.Equals(0))
            {
                if (e.Column.FieldName.Equals("USEDFACTOR"))
                {
                    view.SetFocusedRowCellValue("ARRAY", Format.GetString(e.Value));
                    
                    if (!string.IsNullOrEmpty(e.Value.ToString()) && !string.IsNullOrEmpty(view.GetFocusedRowCellValue("PCSPNL").ToString()))
                    {
                        double usedfactor = Convert.ToDouble(e.Value.ToString());
                        double pcspnl = Convert.ToDouble(view.GetFocusedRowCellValue("PCSPNL").ToString());

                        if (usedfactor > 0)
                        {
                            int hitcount = Convert.ToInt32(pcspnl / usedfactor);
                            view.SetFocusedRowCellValue("HITCOUNT", Convert.ToString(hitcount));
                        }
                    }
                }
            }

            languageValue = string.Join("-", durableClassId, toolType, toolDetailType, durableDefVer);

            view.SetFocusedRowCellValue("DURABLEDEFNAME$$KO-KR", languageValue);
            view.SetFocusedRowCellValue("DURABLEDEFNAME$$EN-US", languageValue);
            view.SetFocusedRowCellValue("DURABLEDEFNAME$$ZH-CN", languageValue);
            view.SetFocusedRowCellValue("DURABLEDEFNAME$$VI-VN", languageValue);
            view.CellValueChanged += View_CellValueChanged;
        }

        /// <summary>
        /// ReadOnly 컬럼이 추가 일경우만 Edit이 되도록
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (grdMoldAndTool.View.GetFocusedDataRow().RowState.Equals(DataRowState.Added))
            {
                return;
            }

            var currentColumnName = grdMoldAndTool.View.FocusedColumn.FieldName;

            if (currentColumnName.Equals("DURABLEDEFID") || currentColumnName.Equals("DURABLEDEFVERSION"))
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 새로운 ROW 추가시 PLANTID,ENTERPRISEID를 자동 등록해주는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRow(SmartBandedGridView sender, AddNewRowArgs args)
        {
            args.NewRow["PLANTID"] = UserInfo.Current.Plant;
            args.NewRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;

            switch (tabMoldAndToolManager.SelectedTabPage.AccessibleName)
            {
                case "MOLD":
                    args.NewRow["DURABLECLASSID"] = "ToolType1";
                    break;

                case "FILM":
                    args.NewRow["DURABLECLASSID"] = "ToolTypeA";
                    break;
            }
        }

        /// <summary>
        /// 품목 x버튼 삭제 시
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoldAndToolManagement_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            SmartBandedGrid grid = new SmartBandedGrid();
            switch (tabMoldAndToolManager.SelectedTabPageIndex)
            {
                case 0:
                    grid = grdMold;
                    break;
                case 1:
                    grid = grdBBT;
                    break;
                case 2:
                    grid = grdFilm;
                    break;
            }

            if (e.Button.Kind.Equals(DevExpress.XtraEditors.Controls.ButtonPredefines.Clear))
            {
                grid.View.SetFocusedRowCellValue("PRODUCTDEFVERSION", string.Empty);
                grid.View.SetFocusedRowCellValue("PRODUCTDEFNAME", string.Empty);
            }
        }

        #endregion Event

        #region 툴바

        /// <summary>
        ///     저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            var changed = new DataTable();

            switch (tabMoldAndToolManager.SelectedTabPage.AccessibleName)
            {
                case "MOLD":
                    changed = grdMold.GetChangedRows();
                    break;

                case "BBT":
                    changed = grdBBT.GetChangedRows();
                    break;

                case "FILM":
                    changed = grdFilm.GetChangedRows();
                    break;

                default:
                    changed = grdMoldAndTool.GetChangedRows();
                    break;
            }

            ExecuteRule("SaveMoldAndToolMaster", changed);
        }

        #endregion 툴바

        #region 검색

        /// <summary>
        ///     비동기 override 모델
        /// </summary>
        protected override async Task OnSearchAsync()
        {
            try
            {
                DialogManager.ShowWaitArea(this.pnlContent);

                await base.OnSearchAsync();

                Dictionary<string, object> values = Conditions.GetValues();
                values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                DataTable dt;

                switch (tabMoldAndToolManager.SelectedTabPage.AccessibleName)
                {
                    case "MOLD":
                        dt = await SqlExecuter.QueryAsync("SelectMoldAndToolManagement_Mold", "10001", values);
                        if (dt.Rows.Count.Equals(0))
                        {
                            ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                            return;
                        }

                        grdMold.DataSource = dt;
                        break;

                    case "BBT":
                        dt = await SqlExecuter.QueryAsync("SelectMoldAndToolManagement_BBTJIT", "10001", values);
                        if (dt.Rows.Count.Equals(0))
                        {
                            ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                            return;
                        }

                        grdBBT.DataSource = dt;
                        break;

                    case "FILM":
                        dt = await SqlExecuter.QueryAsync("SelectMoldAndToolManagement_Film", "10001", values);
                        if (dt.Rows.Count.Equals(0))
                        {
                            ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                            return;
                        }

                        grdFilm.DataSource = dt;
                        break;

                    default:
                        dt = await SqlExecuter.QueryAsync("SelectMoldAndToolManagement", "10001", values);
                        if (dt.Rows.Count.Equals(0))
                        {
                            ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                            return;
                        }

                        grdMoldAndTool.DataSource = dt;
                        break;
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

            // 품목 SelectPopup 항목 추가
            var conditionProductId = Conditions.AddSelectPopup("P_PRODUCTDEFID", new SqlQuery("GetProductDefinitionList", "10002", $"PLANTID={UserInfo.Current.Plant}"), "PRODUCTDEFID", "PRODUCTDEFID")
                .SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupLayoutForm(800, 800)
                .SetLabel("PRODUCTDEFID")
                .SetPosition(2.2)
                .SetPopupResultCount(1)
                .SetPopupApplySelection((selectRow, gridRow) =>
                {
                    List<string> productDefnameList = new List<string>();
                    List<string> productRevisionList = new List<string>();

                    selectRow.AsEnumerable().ForEach(r =>
                    {
                        productDefnameList.Add(Format.GetString(r["PRODUCTDEFNAME"]));
                        productRevisionList.Add(Format.GetString(r["PRODUCTDEFVERSION"]));
                    });

                    Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").EditValue = string.Join(",", productDefnameList);
                    Conditions.GetControl<SmartTextBox>("P_PRODUCTDEFVERSION").EditValue = string.Join(",", productRevisionList);
                });

            // 팝업에서 사용되는 검색조건 (품목코드/명)
            conditionProductId.Conditions.AddTextBox("TXTPRODUCTDEFNAME");
            //제품 구분 추가 2019.08.14 배선용, 노석안 대리 요청
            conditionProductId.Conditions.AddComboBox("PRODUCTDIVISION", new SqlQuery("GetTypeList", "10001", $"CODECLASSID=ProductDivision2", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                .SetEmptyItem(Language.Get("ALLVIEWS"), "", true);

            // 품목코드
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            // 품목명
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            // 품목버전
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 100);
            // 품목유형구분
            conditionProductId.GridColumns.AddComboBoxColumn("PRODUCTDEFTYPE", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductDefType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            // 생산구분
            conditionProductId.GridColumns.AddComboBoxColumn("PRODUCTIONTYPE", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            // 단위
            conditionProductId.GridColumns.AddComboBoxColumn("UNIT", 90, new SqlQuery("GetUomDefinitionList", "10001", "UOMCLASSID=Segment"), "UOMDEFNAME", "UOMDEFID");
        }

        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").ReadOnly = true;
            Conditions.GetControl<SmartTextBox>("P_PRODUCTDEFVERSION").ReadOnly = true;

            Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").EditValueChanged += (s, e) =>
            {
                SmartSelectPopupEdit PopProdutid = s as SmartSelectPopupEdit;

                if (Format.GetFullTrimString(PopProdutid.EditValue).Equals(string.Empty))
                {
                    Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").EditValue = string.Empty;
                    Conditions.GetControl<SmartTextBox>("P_PRODUCTDEFVERSION").EditValue = string.Empty;
                }
            };
        }

        #endregion 검색

        #region 유효성 검사

        /// <summary>
        ///     데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            // TODO : 유효성 로직 변경
            grdMoldAndTool.View.CheckValidation();
            DataTable changed;

            switch (tabMoldAndToolManager.SelectedTabPage.AccessibleName)
            {
                case "MOLD":
                    changed = grdMold.GetChangedRows();
                    break;

                case "BBT":
                    changed = grdBBT.GetChangedRows();
                    break;

                case "FILM":
                    changed = grdFilm.GetChangedRows();
                    break;

                default:
                    changed = grdMoldAndTool.GetChangedRows();
                    break;
            }

            // 아래는 다시한번 체크 필요
            foreach (DataRow row in changed.Rows)
            {
                //2020-03-06 강유라 삭제하는 경우만 체크
                if (Format.GetString(row["_STATE_"]).Equals("deleted"))
                {
                    Dictionary<string, object> values = new Dictionary<string, object>
                    {
                        { "DURABLEDEFID", Format.GetString(row["DURABLEDEFID"], string.Empty) },
                        { "DURABLEDEFVERSION", Format.GetString(row["DURABLEDEFVERSION"], string.Empty) }
                    };

                    DataTable dtRequestedTool = SqlExecuter.Query("CheckToolRequestHistory", "00001", values);
                    if (dtRequestedTool != null && dtRequestedTool.Rows.Count > 0)
                    {
                        throw MessageException.Create("AlreadyRequestProduction", dtRequestedTool.Rows[0]["REQUESTDATE"].ToString(), dtRequestedTool.Rows[0]["USERNAME"].ToString());
                    }

                    var ParamResource = new Dictionary<string, object>
                    {
                        { "ENTERPRISEID", UserInfo.Current.Enterprise },
                        { "RESOURCEID", Format.GetString(row["DURABLEDEFID"], string.Empty) }
                    };

                    var dtResource = SqlExecuter.Query("GetOperationResourceChk", "10001", ParamResource);
                    foreach (DataRow resourcedr in dtResource.Rows)
                    {
                        if (resourcedr["RESOURCEID"].Equals(row["DURABLEDEFID"]) && resourcedr["RESOURCEIDVERSION"].Equals(row["DURABLEDEFVERSION"]))
                        {
                            throw MessageException.Create("ResourceToolFilm");
                        }
                    }
                }
            }

            if (changed.Rows.Count == 0)
            {
                throw MessageException.Create("NoSaveData"); // 저장할 데이터가 존재하지 않습니다.
            }
        }

        #endregion 유효성 검사
    }
}