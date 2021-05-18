#region using
using DevExpress.XtraTreeList.Nodes;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.StandardInfo.Popup;

using Newtonsoft.Json;
using System;
using Micube.SmartMES.Commons.Controls;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DevExpress.XtraCharts;
using DevExpress.Utils;
using Micube.Framework.SmartControls.Validations;
using Micube.SmartMES.Commons;

#endregion

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프로그램명 : 기준정보 > 외주사양정보 및 공정 SPEC 정보
    /// 업 무 설명 : 외주사양정보 및 공정 SPEC 정보
    /// 생  성  자 : 장선미 
    /// 생  성  일 : 2020-01-02
    /// 수정 이 력 : 
    /// </summary> 
    public partial class ProductOutsourcingSpec : SmartConditionManualBaseForm
    {
        #region Local Variables

        private string _focueSegmentID = string.Empty;
        private string _strSpecType = string.Empty;
        #endregion

        #region 생성자
        public ProductOutsourcingSpec()
        {
            InitializeComponent();
        }
        #endregion
        
        #region 컨텐츠 영역 초기화
        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGridList()
        {

            InitializeGridOperation();
            InitializeGridSpec();
        }
      
        /// <summary>
        /// 공정 Grid 컬럼 초기화
        /// </summary>
        private void InitializeGridOperation()
        {
            grdOperation.GridButtonItem = GridButtonItem.None;
            grdOperation.View.SetIsReadOnly();

            //Site
            grdOperation.View.AddComboBoxColumn("PLANTID", 80, new SqlQuery("GetPlantList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PLANTNAME", "PLANTID")
                .SetIsHidden()
                .SetDefault(UserInfo.Current.Plant);
            //공정수순
            grdOperation.View.AddTextBoxColumn("USERSEQUENCE", 80).SetIsReadOnly();
            //공정ID
            grdOperation.View.AddTextBoxColumn("PROCESSSEGMENTID", 100).SetIsReadOnly();
            //공정명
            grdOperation.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 300).SetIsReadOnly();

            grdOperation.View.AddTextBoxColumn("PRODUCTDEFID", 100).SetIsHidden();
            grdOperation.View.AddTextBoxColumn("PRODUCTDEFVERSION", 100).SetIsHidden();
            grdOperation.View.AddTextBoxColumn("SUBSEGMENTID1", 100).SetIsHidden();


            grdOperation.View.PopulateColumns();
        }


        /// <summary>
        /// 외주사양정보 그리드 컬럼 초기화
        /// </summary>
        private void InitializeGridSpec()
        {
            grdSpecList.GridButtonItem = GridButtonItem.All;
        
            grdSpecList.View.ClearColumns();

            switch (this._strSpecType)
            {
                case "OutsourcingSpecType_Guide":
                    //GUIDE 내역 초기화
                    grdSpecList.LanguageKey = "OUTSOURCINGSPECGUIDE";
                    grdSpecList.Caption = Language.Get("OUTSOURCINGSPECGUIDE");

                    InitializeGridSpec_Guide();
                    break;

                case "OutsourcingSpecType_Attach":
                    //제거 내역 초기화
                    grdSpecList.LanguageKey = "OUTSOURCINGSPECATTACH";
                    grdSpecList.Caption = Language.Get("OUTSOURCINGSPECATTACH");

                    InitializeGridSpec_Attach();
                    break;

                case "OutsourcingSpecType_Remove":
                    //제거 내역 초기화
                    grdSpecList.LanguageKey = "OUTSOURCINGSPECREMOVE";
                    grdSpecList.Caption = Language.Get("OUTSOURCINGSPECREMOVE");

                    InitializeGridSpec_Remove();
                    break;

                case "OutsourcingSpecType_Drill":
                    //DRILL 내역 초기화
                    grdSpecList.LanguageKey = "OUTSOURCINGSPECDRILL";
                    grdSpecList.Caption = Language.Get("OUTSOURCINGSPECDRILL");

                    InitializeGridSpec_Drill();
                    break;

                case "OutsourcingSpecType_Cut":
                    //Cut 내역 초기화
                    grdSpecList.LanguageKey = "OUTSOURCINGSPECCUT";
                    grdSpecList.Caption = Language.Get("OUTSOURCINGSPECCUT");

                    InitializeGridSpec_Cut();
                    break;

                case "OutsourcingSpecType_Plating":
                    //도금 내역 초기화
                    grdSpecList.LanguageKey = "OUTSOURCINGSPECPLATING";
                    grdSpecList.Caption = Language.Get("OUTSOURCINGSPECPLATING");

                    InitializeGridSpec_Plating();
                    break;

                case "OutsourcingSpecType_Inspection":
                    //검사 내역 초기화
                    grdSpecList.LanguageKey = "OUTSOURCINGSPECINSPECTION";
                    grdSpecList.Caption = Language.Get("OUTSOURCINGSPECINSPECTION");

                    InitializeGridSpec_Inspection();
                    break;

                case "OutsourcingSpecType_Print":
                    //인쇄 내역 초기화
                    grdSpecList.LanguageKey = "OUTSOURCINGSPECPRINT";
                    grdSpecList.Caption = Language.Get("OUTSOURCINGSPECPRINT");

                    InitializeGridSpec_Print();
                    break;

                case "OutsourcingSpecType_Stack":
                    //적층 내역 초기화
                    grdSpecList.LanguageKey = "OUTSOURCINGSPECSTACK";
                    grdSpecList.Caption = Language.Get("OUTSOURCINGSPECSTACK");

                    InitializeGridSpec_Stack();
                    break;

                case "OutsourcingSpecType_Mold":
                    //금형 내역 초기화
                    grdSpecList.LanguageKey = "OUTSOURCINGSPECMOLD";
                    grdSpecList.Caption = Language.Get("OUTSOURCINGSPECMOLD");

                    InitializeGridSpec_Mold();
                    break;

                default:
                    grdSpecList.LanguageKey = "OUTSOURCINGSPECINFO";
                    grdSpecList.Caption = Language.Get("OUTSOURCINGSPECINFO");

                    grdSpecList.View.PopulateColumns();
                    break;
            }
        }

        /// <summary>
        /// 외주사양정보 - GUIDE 내역 초기화
        /// </summary>
        private void InitializeGridSpec_Guide()
        {
            //ENTERPRISEID
            grdSpecList.View.AddTextBoxColumn("ENTERPRISEID", 80).SetIsHidden();

            //Site
            grdSpecList.View.AddComboBoxColumn("PLANTID", 80, new SqlQuery("GetPlantList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PLANTNAME", "PLANTID")
                .SetIsHidden();
            //품목코드
            grdSpecList.View.AddTextBoxColumn("PRODUCTDEFID", 80).SetIsHidden();
            grdSpecList.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetIsHidden();
            //공정ID
            grdSpecList.View.AddTextBoxColumn("PROCESSSEGMENTID", 80).SetIsHidden();
            //SPECNO
            grdSpecList.View.AddTextBoxColumn("OUTSOURCINGSPECNO", 80).SetIsHidden();

            //UOM
            grdSpecList.View.AddComboBoxColumn("UOMDEFID", 80, new SqlQuery("GetUomDefinitionList", "10001", "UOMCLASSID=OutsourcingSpec"), "UOMDEFNAME", "UOMDEFID")
                .SetLabel("UOM")
                .SetValidationIsRequired();

            //HOLE 선택
            //TODO : 쿼리 변경 필요
            grdSpecList.View.AddComboBoxColumn("SPECTYPE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                .SetLabel("HOLE")
                .SetValidationIsRequired();

            //HOLE수
            grdSpecList.View.AddTextBoxColumn("HOLEQTY", 80).SetTextAlignment(TextAlignment.Right);

            //STACK 수
            grdSpecList.View.AddTextBoxColumn("STACKQTY", 80).SetTextAlignment(TextAlignment.Right);

            //구분
            grdSpecList.View.AddComboBoxColumn("SPECSUBTYPE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OutsourcingSpecGuideType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                .SetLabel("TYPE");

            //자재종류
            grdSpecList.View.AddComboBoxColumn("MATERIALTYPE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID");

            //양면 /멀티
            grdSpecList.View.AddComboBoxColumn("DETAILTYPE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OutsourcingSpecBothMulti", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                .SetLabel("BOTHMULTI");

            //비고
            grdSpecList.View.AddTextBoxColumn("DESCRIPTION", 200);

            //생성 / 수정
            grdSpecList.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdSpecList.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdSpecList.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdSpecList.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdSpecList.View.PopulateColumns();


            //DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repository = grdSpecList.View.Columns["HOLEQTY"].ColumnEdit as DevExpress.XtraEditors.Repository.RepositoryItemTextEdit;
            //repository.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            //repository.Mask.EditMask = @"\d*";

            //repository = grdSpecList.View.Columns["HOLEQTY"].ColumnEdit as DevExpress.XtraEditors.Repository.RepositoryItemTextEdit;
            //repository.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            //repository.Mask.EditMask = @"\d*";

        }

        /// <summary>
        /// 외주사양정보 - 부착 내역 초기화
        /// </summary>
        private void InitializeGridSpec_Attach()
        {
            //ENTERPRISEID
            grdSpecList.View.AddTextBoxColumn("ENTERPRISEID", 80).SetIsHidden();
            //Site
            grdSpecList.View.AddComboBoxColumn("PLANTID", 80, new SqlQuery("GetPlantList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PLANTNAME", "PLANTID")
                .SetIsHidden();
            //품목코드
            grdSpecList.View.AddTextBoxColumn("PRODUCTDEFID", 80).SetIsHidden();
            grdSpecList.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetIsHidden();
            //공정ID
            grdSpecList.View.AddTextBoxColumn("PROCESSSEGMENTID", 80).SetIsHidden();
            //SPECNO
            grdSpecList.View.AddTextBoxColumn("OUTSOURCINGSPECNO", 80).SetIsHidden();

            //SIDE 선택
            grdSpecList.View.AddComboBoxColumn("SPECTYPE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=WorkSurface", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                .SetLabel("SIDE")
                .SetValidationIsRequired();

            //UOM
            grdSpecList.View.AddComboBoxColumn("UOMDEFID", 80, new SqlQuery("GetUomDefinitionList", "10001", "UOMCLASSID=OutsourcingSpec"), "UOMDEFNAME", "UOMDEFID")
                .SetLabel("UOM")
                .SetValidationIsRequired();

            //자재종류
            grdSpecList.View.AddComboBoxColumn("MATERIALTYPE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OutsourcingSpecAttachMaterialType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID");

            //작업방식
            grdSpecList.View.AddComboBoxColumn("SPECSUBTYPE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OutsourcingSpecWorkType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                .SetLabel("ISMAINSEGMENT");

            //부착물형태
            grdSpecList.View.AddComboBoxColumn("DETAILTYPE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OutsourcingSpecAttachType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                .SetLabel("ATTACHMENTTYPE");

            //부착 수
            grdSpecList.View.AddTextBoxColumn("ATTACHQTY", 80).SetTextAlignment(TextAlignment.Right);

            //비고
            grdSpecList.View.AddTextBoxColumn("DESCRIPTION", 200);

            //생성 / 수정
            grdSpecList.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdSpecList.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdSpecList.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdSpecList.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdSpecList.View.PopulateColumns();
        }

        /// <summary>
        /// 외주사양정보 - 제거 내역 초기화
        /// </summary>
        private void InitializeGridSpec_Remove()
        {
            //ENTERPRISEID
            grdSpecList.View.AddTextBoxColumn("ENTERPRISEID", 80).SetIsHidden();
            //Site
            grdSpecList.View.AddComboBoxColumn("PLANTID", 80, new SqlQuery("GetPlantList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PLANTNAME", "PLANTID")
                .SetIsHidden();
            //품목코드
            grdSpecList.View.AddTextBoxColumn("PRODUCTDEFID", 80).SetIsHidden();
            grdSpecList.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetIsHidden();
            //공정ID
            grdSpecList.View.AddTextBoxColumn("PROCESSSEGMENTID", 80).SetIsHidden();
            //SPECNO
            grdSpecList.View.AddTextBoxColumn("OUTSOURCINGSPECNO", 80).SetIsHidden();

            //SIDE 선택
            grdSpecList.View.AddComboBoxColumn("SPECTYPE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=WorkSurface", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                .SetLabel("SIDE")
                .SetValidationIsRequired();

            //UOM
            grdSpecList.View.AddComboBoxColumn("UOMDEFID", 80, new SqlQuery("GetUomDefinitionList", "10001", "UOMCLASSID=OutsourcingSpec"), "UOMDEFNAME", "UOMDEFID")
                .SetLabel("UOM")
                .SetValidationIsRequired();

            //자재종류
            grdSpecList.View.AddComboBoxColumn("MATERIALTYPE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OutsourcingSpecRemoveMaterialType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID");

            //제거 수
            grdSpecList.View.AddTextBoxColumn("REMOVEQTY", 80).SetTextAlignment(TextAlignment.Right);

            //비고
            grdSpecList.View.AddTextBoxColumn("DESCRIPTION", 200);

            //생성 / 수정
            grdSpecList.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdSpecList.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdSpecList.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdSpecList.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdSpecList.View.PopulateColumns();
        }
        /// <summary>
        /// 외주사양정보 - DRILL 내역 초기화
        /// </summary>
        private void InitializeGridSpec_Drill()
        {
            //ENTERPRISEID
            grdSpecList.View.AddTextBoxColumn("ENTERPRISEID", 80).SetIsHidden();
            //Site
            grdSpecList.View.AddComboBoxColumn("PLANTID", 80, new SqlQuery("GetPlantList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PLANTNAME", "PLANTID")
                .SetIsHidden();
            //품목코드
            grdSpecList.View.AddTextBoxColumn("PRODUCTDEFID", 80).SetIsHidden();
            grdSpecList.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetIsHidden();
            //공정ID
            grdSpecList.View.AddTextBoxColumn("PROCESSSEGMENTID", 80).SetIsHidden();
            //SPECNO
            grdSpecList.View.AddTextBoxColumn("OUTSOURCINGSPECNO", 80).SetIsHidden();

            //UOM
            grdSpecList.View.AddComboBoxColumn("UOMDEFID", 80, new SqlQuery("GetUomDefinitionList", "10001", "UOMCLASSID=OutsourcingSpec"), "UOMDEFNAME", "UOMDEFID")
                .SetLabel("UOM")
                .SetValidationIsRequired();

            //HOLE 선택
            grdSpecList.View.AddComboBoxColumn("SPECTYPE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                .SetLabel("HOLE")
                .SetValidationIsRequired();

            //HOLE수
            grdSpecList.View.AddTextBoxColumn("HOLEQTY", 80).SetTextAlignment(TextAlignment.Right);

            //STACK 수
            grdSpecList.View.AddTextBoxColumn("STACKQTY", 80).SetTextAlignment(TextAlignment.Right);

            //구분
            grdSpecList.View.AddComboBoxColumn("SPECSUBTYPE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                .SetLabel("TYPE");

            //자재종류
            grdSpecList.View.AddComboBoxColumn("MATERIALTYPE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OutsourcingSpecDrillMaterialType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID");

            //양면 /멀티
            grdSpecList.View.AddComboBoxColumn("DETAILTYPE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OutsourcingSpecBothMulti", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                .SetLabel("BOTHMULTI");

            //비고
            grdSpecList.View.AddTextBoxColumn("DESCRIPTION", 200);

            //생성 / 수정
            grdSpecList.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdSpecList.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdSpecList.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdSpecList.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdSpecList.View.PopulateColumns();

        }

        /// <summary>
        /// 외주사양정보 - Cut 내역 초기화
        /// </summary>
        private void InitializeGridSpec_Cut()
        {
            //ENTERPRISEID
            grdSpecList.View.AddTextBoxColumn("ENTERPRISEID", 80).SetIsHidden();
            //Site
            grdSpecList.View.AddComboBoxColumn("PLANTID", 80, new SqlQuery("GetPlantList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PLANTNAME", "PLANTID")
                .SetIsHidden();
            //품목코드
            grdSpecList.View.AddTextBoxColumn("PRODUCTDEFID", 80).SetIsHidden();
            grdSpecList.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetIsHidden();
            //공정ID
            grdSpecList.View.AddTextBoxColumn("PROCESSSEGMENTID", 80).SetIsHidden();
            //SPECNO
            grdSpecList.View.AddTextBoxColumn("OUTSOURCINGSPECNO", 80).SetIsHidden();

            //SIDE 선택
            grdSpecList.View.AddComboBoxColumn("SPECTYPE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=WorkSurface", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                .SetLabel("SIDE")
                .SetValidationIsRequired();

            //UOM
            grdSpecList.View.AddComboBoxColumn("UOMDEFID", 80, new SqlQuery("GetUomDefinitionList", "10001", "UOMCLASSID=OutsourcingSpec"), "UOMDEFNAME", "UOMDEFID")
                .SetLabel("UOM")
                .SetValidationIsRequired();

            //CUT 분류
            grdSpecList.View.AddComboBoxColumn("SPECSUBTYPE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OutsourcingSpecCutType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                .SetLabel("CUTTYPE");

            //자재종류
            grdSpecList.View.AddComboBoxColumn("MATERIALTYPE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OutsourcingSpecCutMaterialType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID");

            //CUT 길이
            grdSpecList.View.AddTextBoxColumn("CUTLENGTH", 80).SetTextAlignment(TextAlignment.Right);

            //STACK 수
            grdSpecList.View.AddTextBoxColumn("STACKQTY", 80).SetTextAlignment(TextAlignment.Right);
            
            //외곽CUT 유무
            grdSpecList.View.AddComboBoxColumn("ISYN", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                .SetLabel("ISOUTERCUT");

            //비고
            grdSpecList.View.AddTextBoxColumn("DESCRIPTION", 200);

            //생성 / 수정
            grdSpecList.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdSpecList.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdSpecList.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdSpecList.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdSpecList.View.PopulateColumns();

        }

        /// <summary>
        /// 외주사양정보 - 도금 내역 초기화
        /// </summary>
        private void InitializeGridSpec_Plating()
        {
            //ENTERPRISEID
            grdSpecList.View.AddTextBoxColumn("ENTERPRISEID", 80).SetIsHidden();
            //Site
            grdSpecList.View.AddComboBoxColumn("PLANTID", 80, new SqlQuery("GetPlantList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PLANTNAME", "PLANTID")
                .SetIsHidden();
            //품목코드
            grdSpecList.View.AddTextBoxColumn("PRODUCTDEFID", 80).SetIsHidden();
            grdSpecList.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetIsHidden();
            //공정ID
            grdSpecList.View.AddTextBoxColumn("PROCESSSEGMENTID", 80).SetIsHidden();
            //SPECNO
            grdSpecList.View.AddTextBoxColumn("OUTSOURCINGSPECNO", 80).SetIsHidden();

            //SIDE 선택
            grdSpecList.View.AddComboBoxColumn("SPECTYPE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=WorkSurface", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                .SetLabel("SIDE")
                .SetValidationIsRequired();

            //UOM
            grdSpecList.View.AddComboBoxColumn("UOMDEFID", 80, new SqlQuery("GetUomDefinitionList", "10001", "UOMCLASSID=OutsourcingSpec"), "UOMDEFNAME", "UOMDEFID")
                .SetLabel("UOM")
                .SetValidationIsRequired();

            //도금Type
            grdSpecList.View.AddComboBoxColumn("SPECSUBTYPE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OutsourcingSpecPlatingType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                .SetLabel("PLATINGTYPE");

            //도금두께 최소
            grdSpecList.View.AddTextBoxColumn("MINVALUE", 80).SetTextAlignment(TextAlignment.Right)
                .SetLabel("MINPLATINGTHICKNESS");

            //도금두께 최대
            grdSpecList.View.AddTextBoxColumn("MAXVALUE", 80).SetTextAlignment(TextAlignment.Right)
                .SetLabel("MAXPLATINGTHICKNESS");

            //제품면적
            grdSpecList.View.AddTextBoxColumn("PRODUCTAREA", 80).SetTextAlignment(TextAlignment.Right);

            //스크랩면적
            grdSpecList.View.AddTextBoxColumn("SCRAPAREA", 80).SetTextAlignment(TextAlignment.Right);

            //PNL X
            grdSpecList.View.AddTextBoxColumn("PNLX", 80).SetTextAlignment(TextAlignment.Right);

            //PNL Y
            grdSpecList.View.AddTextBoxColumn("PNLY", 80).SetTextAlignment(TextAlignment.Right);

            //비고
            grdSpecList.View.AddTextBoxColumn("DESCRIPTION", 200);

            //생성 / 수정
            grdSpecList.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdSpecList.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdSpecList.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdSpecList.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdSpecList.View.PopulateColumns();
        }

        /// <summary>
        /// 외주사양정보 - 검사 내역 초기화
        /// </summary>
        private void InitializeGridSpec_Inspection()
        {
            //ENTERPRISEID
            grdSpecList.View.AddTextBoxColumn("ENTERPRISEID", 80).SetIsHidden();
            //Site
            grdSpecList.View.AddComboBoxColumn("PLANTID", 80, new SqlQuery("GetPlantList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PLANTNAME", "PLANTID")
                .SetIsHidden();
            //품목코드
            grdSpecList.View.AddTextBoxColumn("PRODUCTDEFID", 80).SetIsHidden();
            grdSpecList.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetIsHidden();
            //공정ID
            grdSpecList.View.AddTextBoxColumn("PROCESSSEGMENTID", 80).SetIsHidden();
            //SPECNO
            grdSpecList.View.AddTextBoxColumn("OUTSOURCINGSPECNO", 80).SetIsHidden();

            //SIDE 선택
            grdSpecList.View.AddComboBoxColumn("SPECTYPE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=WorkSurface", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                .SetLabel("SIDE")
                .SetValidationIsRequired();

            //UOM
            grdSpecList.View.AddComboBoxColumn("UOMDEFID", 80, new SqlQuery("GetUomDefinitionList", "10001", "UOMCLASSID=OutsourcingSpec"), "UOMDEFNAME", "UOMDEFID")
                .SetLabel("UOM")
                .SetValidationIsRequired();

            //LAYER - 라우팅정보
            grdSpecList.View.AddComboBoxColumn("LAYER", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=Layer", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                .SetIsReadOnly();

            //자동/수동
            grdSpecList.View.AddComboBoxColumn("DETAILTYPE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OutsourcingSpecAutoManual", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                .SetLabel("AUTOMANUAL");

            //작업방식
            grdSpecList.View.AddComboBoxColumn("SPECSUBTYPE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OutsourcingSpecInspectionWorkType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                .SetLabel("ISMAINSEGMENT");

            //홀 유무
            grdSpecList.View.AddComboBoxColumn("ISYN", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                .SetLabel("ISHOLE");

            //연배
            grdSpecList.View.AddTextBoxColumn("USEDFACTOR", 80).SetTextAlignment(TextAlignment.Right);

            //PNL X
            grdSpecList.View.AddTextBoxColumn("PNLX", 80).SetTextAlignment(TextAlignment.Right);

            //PNL Y
            grdSpecList.View.AddTextBoxColumn("PNLY", 80).SetTextAlignment(TextAlignment.Right);

            //비고
            grdSpecList.View.AddTextBoxColumn("DESCRIPTION", 200);

            //생성 / 수정
            grdSpecList.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdSpecList.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdSpecList.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdSpecList.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdSpecList.View.PopulateColumns();
        }

        /// <summary>
        /// 외주사양정보 - 인쇄 내역 초기화
        /// </summary>
        private void InitializeGridSpec_Print()
        {
            //ENTERPRISEID
            grdSpecList.View.AddTextBoxColumn("ENTERPRISEID", 80).SetIsHidden();
            //Site
            grdSpecList.View.AddComboBoxColumn("PLANTID", 80, new SqlQuery("GetPlantList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PLANTNAME", "PLANTID")
                .SetIsHidden();
            //품목코드
            grdSpecList.View.AddTextBoxColumn("PRODUCTDEFID", 80).SetIsHidden();
            grdSpecList.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetIsHidden();
            //공정ID
            grdSpecList.View.AddTextBoxColumn("PROCESSSEGMENTID", 80).SetIsHidden();
            //SPECNO
            grdSpecList.View.AddTextBoxColumn("OUTSOURCINGSPECNO", 80).SetIsHidden();

            //SIDE 선택
            grdSpecList.View.AddComboBoxColumn("SPECTYPE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=WorkSurface", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                .SetLabel("SIDE")
                .SetValidationIsRequired();

            //UOM
            grdSpecList.View.AddComboBoxColumn("UOMDEFID", 80, new SqlQuery("GetUomDefinitionList", "10001", "UOMCLASSID=OutsourcingSpec"), "UOMDEFNAME", "UOMDEFID")
                .SetLabel("UOM")
                .SetValidationIsRequired();

            //잉크종류
            grdSpecList.View.AddComboBoxColumn("SPECSUBTYPE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OutsourcingSpecInkType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                .SetLabel("INKTYPE");

            ////자재코드
            //grdSpecList.View.AddTextBoxColumn("MATERIALDEF", 80)
            //    .SetIsReadOnly();

            ////자재명
            //grdSpecList.View.AddTextBoxColumn("MATERIALNAME", 150)
            //    .SetIsReadOnly();

            //PNL X
            grdSpecList.View.AddTextBoxColumn("PNLX", 80).SetTextAlignment(TextAlignment.Right);

            //PNL Y
            grdSpecList.View.AddTextBoxColumn("PNLY", 80).SetTextAlignment(TextAlignment.Right);

            //인쇄도수
            grdSpecList.View.AddTextBoxColumn("PRINTCOLORQTY", 80).SetTextAlignment(TextAlignment.Right);

            //판넬수
            grdSpecList.View.AddTextBoxColumn("PNLQTY", 80).SetTextAlignment(TextAlignment.Right);

            //투입방향
            grdSpecList.View.AddComboBoxColumn("INPUTTO", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OutsourcingSpecInputTo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID");

            //회전수
            grdSpecList.View.AddTextBoxColumn("TURNCOUNT", 80).SetTextAlignment(TextAlignment.Right);

            //비고
            grdSpecList.View.AddTextBoxColumn("DESCRIPTION", 200);

            //생성 / 수정
            grdSpecList.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdSpecList.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdSpecList.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdSpecList.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdSpecList.View.PopulateColumns();
        }

        /// <summary>
        /// 외주사양정보 - 적층 내역 초기화
        /// </summary>
        private void InitializeGridSpec_Stack()
        {
            //ENTERPRISEID
            grdSpecList.View.AddTextBoxColumn("ENTERPRISEID", 80).SetIsHidden();
            //Site
            grdSpecList.View.AddComboBoxColumn("PLANTID", 80, new SqlQuery("GetPlantList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PLANTNAME", "PLANTID")
                .SetIsHidden();
            //품목코드
            grdSpecList.View.AddTextBoxColumn("PRODUCTDEFID", 80).SetIsHidden();
            grdSpecList.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetIsHidden();
            //공정ID
            grdSpecList.View.AddTextBoxColumn("PROCESSSEGMENTID", 80).SetIsHidden();
            //SPECNO
            grdSpecList.View.AddTextBoxColumn("OUTSOURCINGSPECNO", 80).SetIsHidden();

            //UOM
            grdSpecList.View.AddComboBoxColumn("UOMDEFID", 80, new SqlQuery("GetUomDefinitionList", "10001", "UOMCLASSID=OutsourcingSpec"), "UOMDEFNAME", "UOMDEFID")
                .SetLabel("UOM")
                .SetValidationIsRequired();

            //자재종류
            grdSpecList.View.AddComboBoxColumn("MATERIALTYPE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OutsourcingSpecStackMaterialType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                .SetValidationIsRequired();

            //자재코드
            InitializeGridSpec_MaterialPopup();
            grdSpecList.View.AddTextBoxColumn("MATERIALDEFVERSION", 80)
                .SetIsHidden();

            //자재명
            grdSpecList.View.AddTextBoxColumn("MATERIALNAME", 150)
                .SetIsReadOnly();

            //규격
            grdSpecList.View.AddTextBoxColumn("SPEC", 150)
                .SetIsReadOnly();

            //소요량
            grdSpecList.View.AddTextBoxColumn("COMPONENTQTY", 80)
                .SetTextAlignment(TextAlignment.Right)
                .SetIsReadOnly();
            
            //UOM
            grdSpecList.View.AddComboBoxColumn("COMPONENTUOM", 80, new SqlQuery("GetUOMList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "UOMDEFID", "UOMDEFNAME")
                .SetIsReadOnly();
           
            //층수
            grdSpecList.View.AddComboBoxColumn("SPECTYPE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=Layer", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                .SetLabel("LAYER");

            //진공/일반
            grdSpecList.View.AddComboBoxColumn("DETAILTYPE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OutsourcingSpecVacuumNormal", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                .SetLabel("VACUUMNORMAL");

            //클리닝유형
            grdSpecList.View.AddComboBoxColumn("SPECSUBTYPE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OutsourcingSpecCleaningType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                .SetLabel("CLEANINGTYPE");

            //쿠폰 유무
            grdSpecList.View.AddComboBoxColumn("ISYN", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                .SetLabel("ISCOUPON");

            //작업면적
            grdSpecList.View.AddTextBoxColumn("WORKAREA", 80).SetTextAlignment(TextAlignment.Right)
                .SetLabel("WORKDIMENSIONS");

            //건수
            grdSpecList.View.AddTextBoxColumn("CASEQTY", 80).SetTextAlignment(TextAlignment.Right)
                .SetLabel("OSPETCWORKCOUNT");

            //비고
            grdSpecList.View.AddTextBoxColumn("DESCRIPTION", 200);

            //생성 / 수정
            grdSpecList.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdSpecList.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdSpecList.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdSpecList.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdSpecList.View.PopulateColumns();


            (grdSpecList.View.Columns["MATERIALDEFID"].ColumnEdit as DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit).ButtonClick +=Item_ButtonClick;

        }


        private void InitializeGridSpec_MaterialPopup()
        {
            string productDefId = this.Conditions.GetValues()["P_PRODUCTDEFID"].ToString();
            string productDefVersion = this.Conditions.GetValues()["P_PRODUCTDEFVERSION"].ToString();
            string segmentID = this._focueSegmentID;


            var materialPopupColumn = this.grdSpecList.View.AddSelectPopupColumn("MATERIALDEFID", new SqlQuery("GetRoutingBomListPopup", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}"
                                                                                                                                                                                                                    , $"LANGUAGETYPE={UserInfo.Current.LanguageType}"
                                                                                                                                                                                                                    , $"PROCESSSEGMENTID={segmentID}"
                                                                                                                                                                                                                    , $"PRODUCTDEFID={productDefId}"
                                                                                                                                                                                                                    , $"PROCESSDEFVERSION={productDefVersion}"))
                .SetLabel("MATERIALDEF")
                // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
                .SetPopupLayout("SELECTCONSUMABLE", PopupButtonStyles.Ok_Cancel, true, false)
                // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
                .SetPopupResultCount(1)
                // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
                .SetPopupResultMapping("MATERIALDEFID", "COMPONENTITEMID")
                // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
                .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow)
                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {
                    // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
                    // dataGridRow : 현재 Focus가 있는 그리드의 DataRow
                    foreach (DataRow row in selectedRows)
                    {
                        dataGridRow["MATERIALDEFVERSION"] = row["COMPONENTITEMVERSION"].ToString();
                        dataGridRow["MATERIALNAME"] = row["COMPONENTITEMNAME"].ToString();
                        dataGridRow["COMPONENTUOM"] = row["COMPONENTUOM"].ToString();
                        dataGridRow["COMPONENTQTY"] = row["COMPONENTQTY"].ToString();
                    }
                });

            // 팝업에서 사용할 조회조건 항목 추가
            materialPopupColumn.Conditions.AddTextBox("P_ITEMID").SetLabel("CONSUMABLEIDNAME");

            // 팝업 그리드 설정
            materialPopupColumn.GridColumns.AddTextBoxColumn("COMPONENTITEMID", 100).SetLabel("ITEMID1");
            materialPopupColumn.GridColumns.AddTextBoxColumn("COMPONENTITEMVERSION", 80).SetLabel("ITEMVERSION");
            materialPopupColumn.GridColumns.AddTextBoxColumn("COMPONENTITEMNAME", 250).SetLabel("ITEMNAME1");
            materialPopupColumn.GridColumns.AddComboBoxColumn("COMPONENTUOM", 80, new SqlQuery("GetUOMList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "UOMDEFID", "UOMDEFNAME").SetIsReadOnly();
            materialPopupColumn.GridColumns.AddTextBoxColumn("COMPONENTQTY", 100).SetTextAlignment(TextAlignment.Right);

        }

        /// <summary>
        /// 팝업 컬럼 x버튼 누를 때 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Item_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind.Equals(DevExpress.XtraEditors.Controls.ButtonPredefines.Clear))
            {
                DataRow row = grdSpecList.View.GetFocusedDataRow();
                grdSpecList.View.SetFocusedRowCellValue("MATERIALDEFVERSION", string.Empty);
                grdSpecList.View.SetFocusedRowCellValue("MATERIALNAME", string.Empty);
                grdSpecList.View.SetFocusedRowCellValue("COMPONENTUOM", string.Empty);
                grdSpecList.View.SetFocusedRowCellValue("COMPONENTQTY", string.Empty);
               // (grdSpecList.DataSource as DataTable).AcceptChanges();
            }
        }

        /// <summary>
        /// 외주사양정보 - 금형 타발 내역 초기화
        /// </summary>
        private void InitializeGridSpec_Mold()
        {
            //ENTERPRISEID
            grdSpecList.View.AddTextBoxColumn("ENTERPRISEID", 80).SetIsHidden();
            //Site
            grdSpecList.View.AddComboBoxColumn("PLANTID", 80, new SqlQuery("GetPlantList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PLANTNAME", "PLANTID")
                .SetIsHidden();
            //품목코드
            grdSpecList.View.AddTextBoxColumn("PRODUCTDEFID", 80).SetIsHidden();
            grdSpecList.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetIsHidden();
            //공정ID
            grdSpecList.View.AddTextBoxColumn("PROCESSSEGMENTID", 80).SetIsHidden();
            //SPECNO
            grdSpecList.View.AddTextBoxColumn("OUTSOURCINGSPECNO", 80).SetIsHidden();

            //UOM
            grdSpecList.View.AddComboBoxColumn("UOMDEFID", 80, new SqlQuery("GetUomDefinitionList", "10001", "UOMCLASSID=OutsourcingSpec"), "UOMDEFNAME", "UOMDEFID")
                .SetLabel("UOM")
                .SetValidationIsRequired();

            //금형종류
            grdSpecList.View.AddComboBoxColumn("MOLDTYPE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OutsourcingSpecMoldType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                .SetValidationIsRequired();

            //타수(합수)
            grdSpecList.View.AddTextBoxColumn("TOOLHITCOUNT", 80)
                .SetIsReadOnly();

            //TOOL 형식
            grdSpecList.View.AddComboBoxColumn("TOOLTYPE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OutsourcingSpecToolType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID");

            //제조사양
            grdSpecList.View.AddTextBoxColumn("MANUFACTURINGSPEC", 200);

            //아대/일반
            grdSpecList.View.AddComboBoxColumn("DETAILTYPE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OutsourcingSpecVacuumNormal", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                .SetLabel("VACUUMNORMAL");

            //쿠폰 유무
            grdSpecList.View.AddComboBoxColumn("ISYN", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                .SetLabel("ISCOUPON");

            ////작업면적
            //grdSpecList.View.AddTextBoxColumn("WORKAREA", 80).SetTextAlignment(TextAlignment.Right)
            //    .SetLabel("WORKDIMENSIONS");

            ////건수
            //grdSpecList.View.AddTextBoxColumn("CASEQTY", 80).SetTextAlignment(TextAlignment.Right)
            //    .SetLabel("OSPETCWORKCOUNT");

            ////회전수
            //grdSpecList.View.AddTextBoxColumn("TURNCOUNT", 80).SetTextAlignment(TextAlignment.Right);

            //비고
            grdSpecList.View.AddTextBoxColumn("DESCRIPTION", 200);

            //생성 / 수정
            grdSpecList.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdSpecList.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdSpecList.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdSpecList.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdSpecList.View.PopulateColumns();
        }

        /// <summary>
        /// 설정 초기화
        /// </summary>
        protected override void InitializeContent()
        {
            InitializeGridList();
            InitializeEvent();
        }


        #endregion

        #region 이벤트
        /// <summary>
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            this.grdOperation.View.FocusedRowChanged += grdOperationView_FocusedRowChanged;
            this.grdOperation.View.RowCellStyle += grdOperationView_RowCellStyle;

            this.grdSpecList.View.AddingNewRow += grdSpecList_AddingNewRow;
        }

        /// <summary>
        /// 외주사양이 입력될수 있는 row 생상 변경
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdOperationView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0)
                return;

            if (!string.IsNullOrEmpty(this.grdOperation.View.GetRowCellValue(e.RowHandle, "SUBSEGMENTID1").ToString()))
            {
                e.Appearance.BackColor = Color.Orange;
            }
        }

        /// <summary>
        /// 외주사양 등록시 key 컬럼 바인딩
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void grdSpecList_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            if (this.grdOperation.View.FocusedRowHandle < 0)
            {
                args.IsCancel = true;

                return;
            }

            DataRow focusedRow = this.grdOperation.View.GetFocusedDataRow();
            if (focusedRow == null)
            {
                args.IsCancel = true;

                return;
            }

            args.NewRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
            args.NewRow["PLANTID"] = focusedRow["PLANTID"];
            args.NewRow["PRODUCTDEFID"] = focusedRow["PRODUCTDEFID"];
            args.NewRow["PRODUCTDEFVERSION"] = focusedRow["PRODUCTDEFVERSION"];
            args.NewRow["PROCESSSEGMENTID"] = focusedRow["PROCESSSEGMENTID"];
            args.NewRow["OUTSOURCINGSPECNO"] = this.grdSpecList.View.RowCount;
        }

        /// <summary>
        /// 공정 선택시 외주사양 조회
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdOperationView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle < 0)
                return;

            this._focueSegmentID = this.grdOperation.View.GetFocusedRowCellValue("PROCESSSEGMENTID").ToString();

            if (this._strSpecType.Equals(this.grdOperation.View.GetFocusedRowCellValue("SUBSEGMENTID1").ToString()) == false)
            {
                this._strSpecType = this.grdOperation.View.GetFocusedRowCellValue("SUBSEGMENTID1").ToString();
                InitializeGridSpec();
            }

            SearchSpecList();
        }
        #endregion

        #region 조회조건 영역

        /// <summary>
        /// 조회조건 영역 초기화 시작
        /// </summary>
        protected override void InitializeCondition()
		{
			base.InitializeCondition();

            // 품목
            InitializeCondition_ProductPopup();

        }

        /// <summary>
        /// 팝업형 조회조건 생성 - 품목
        /// </summary>
        private void InitializeCondition_ProductPopup()
        {
            // SelectPopup 항목 추가
            var conditionProductId = Conditions.AddSelectPopup("P_PRODUCTDEFID", new SqlQuery("GetProductDefinitionList", "10002", $"PLANTID={UserInfo.Current.Plant}"), "PRODUCTDEFID", "PRODUCTDEFID")
                .SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupLayoutForm(800, 800)
                .SetLabel("PRODUCTDEFID")
                .SetPosition(1.2)
                .SetValidationIsRequired()
                .SetPopupResultCount(1)
                .SetPopupApplySelection((selectRow, gridRow) => {

                    List<string> productDefnameList = new List<string>();
                    List<string> productRevisionList = new List<string>();

                    selectRow.AsEnumerable().ForEach(r => {
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
                .SetDefault("Product");

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
            conditionProductId.GridColumns.AddComboBoxColumn("UNIT", 90, new SqlQuery("GetUomDefinitionList", "10001", "UOMCLASSID=OutsourcingSpec"), "UOMDEFNAME", "UOMDEFID");

        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();


            Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").ReadOnly = true;
            Conditions.GetControl<SmartTextBox>("P_PRODUCTDEFVERSION").ReadOnly = true;

            Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").EditValueChanged += ProductDefIDChanged;

        }

        private void ProductDefIDChanged(object sender, EventArgs e)
        {
            SmartSelectPopupEdit PopProdutid = sender as SmartSelectPopupEdit;

            if (Format.GetFullTrimString(PopProdutid.EditValue).Equals(string.Empty))
            {
                Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").EditValue = string.Empty;
                Conditions.GetControl<SmartTextBox>("P_PRODUCTDEFVERSION").EditValue = string.Empty;

                //초기화
                grdOperation.DataSource = null;
                grdSpecList.DataSource = null;
                this._strSpecType = string.Empty;
                this._focueSegmentID = string.Empty;
            }
        }

        #endregion

        #region 툴바
        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            DataTable changed = grdSpecList.GetChangedRows();
            ExecuteRule("SaveProductOutsourcingSpec", changed);
        }

        #endregion

        #region 검색
        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
		{

            await base.OnSearchAsync();

          
            var values = Conditions.GetValues();
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable dtRouting = SqlExecuter.Query("SelectProductRoutingOperation", "10001", values);
            this.grdOperation.DataSource = dtRouting;

            if (!string.IsNullOrEmpty(this._focueSegmentID))
                SearchSpecList();
        }

        private void SearchSpecList()
        {
            // 공정
            var values = Conditions.GetValues();
            values.Add("P_PROCESSSEGMENTID", this._focueSegmentID);
            values.Add("P_SPECTYPE", this._strSpecType);
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable dtOutsourcingSpec = SqlExecuter.Query("SelectProductOutsourcingSpec", "10001", values);
            grdSpecList.DataSource = dtOutsourcingSpec;
        }

        #endregion



        #region 유효성 검사i
        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();


            this.grdSpecList.View.CheckValidation();
        }
        #endregion

        #region private Fuction


        #endregion


    }
}

