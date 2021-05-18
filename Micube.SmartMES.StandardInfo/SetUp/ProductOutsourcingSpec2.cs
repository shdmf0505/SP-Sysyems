#region using
using DevExpress.XtraTreeList.Nodes;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.StandardInfo.Popup;

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
    public partial class ProductOutsourcingSpec2 : SmartConditionManualBaseForm
    {
        #region Local Variables

        private string _focusProductID= string.Empty;
        private string _focusProductVersion = string.Empty;
        private string _focusProcessSegmentID = string.Empty;
        private string _strSpecType = string.Empty;
		private bool _isDelete = false;
        #endregion

        #region 생성자
        public ProductOutsourcingSpec2()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 외부에서 호출시 자동 조회
        /// </summary>
        /// <param name="parameters"></param>
        public override void LoadForm(Dictionary<string, object> parameters)
        {
            base.LoadForm(parameters);

            if (parameters != null)
            {
                _parameters = parameters;
                Conditions.SetValue("P_PRODUCTDEFID", 0, parameters["ITEMID"]);
                Conditions.SetValue("P_PRODUCTDEFVERSION", 0, parameters["ITEMVERSION"]);
                Conditions.SetValue("P_PRODUCTNAME", 0, parameters["ITEMNAME"]);
                OnSearchAsync();


            }
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
			InitializeFooterSummary();
        }
      
		/// <summary>
		/// Footer 합계 초기화
		/// </summary>
		private void InitializeFooterSummary()
		{
			switch(_strSpecType)
			{
				case "OutsourcingSpecType_Guide":
				case "OutsourcingSpecType_Drill":
					grdSpecList.View.Columns["SPECTYPE"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
					grdSpecList.View.Columns["SPECTYPE"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));
					//HOLE수
					grdSpecList.View.Columns["HOLEQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
					grdSpecList.View.Columns["HOLEQTY"].SummaryItem.DisplayFormat = "{0:#,##0}";
					//STACK수
					grdSpecList.View.Columns["STACKQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
					grdSpecList.View.Columns["STACKQTY"].SummaryItem.DisplayFormat = "{0:#,##0}";
					break;
				case "OutsourcingSpecType_Attach":
					grdSpecList.View.Columns["DETAILTYPE"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
					grdSpecList.View.Columns["DETAILTYPE"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("POINTSUM"));
					//부착 POINT수
					grdSpecList.View.Columns["ATTACHQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
					grdSpecList.View.Columns["ATTACHQTY"].SummaryItem.DisplayFormat = "{0:#,##0}";
					break;
                case "OutsourcingSpecType_Remove":
                    //외주사양-제거 내역			
                    grdSpecList.View.Columns["MATERIALTYPE"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                    grdSpecList.View.Columns["MATERIALTYPE"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("POINTSUM"));
                    //REMOVEQTY 제거 수
                    grdSpecList.View.Columns["REMOVEQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                    grdSpecList.View.Columns["REMOVEQTY"].SummaryItem.DisplayFormat = "{0:#,##0}";
                    break;
                case "OutsourcingSpecType_Cut":
                    //외주사양-CUT내역
                    grdSpecList.View.Columns["SPECSUBTYPE"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                    grdSpecList.View.Columns["SPECSUBTYPE"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));
                    //CUTLENGTH CUT 길이
                    grdSpecList.View.Columns["CUTLENGTH"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                    grdSpecList.View.Columns["CUTLENGTH"].SummaryItem.DisplayFormat = "{0:#,##0}";
                    break;
                //case "OutsourcingSpecType_Attach":
                //    //외주사양-부착내역
                //    //ATTACHQTY 부착POINT 수
                //    grdSpecList.View.Columns["ATTACHQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                //    grdSpecList.View.Columns["ATTACHQTY"].SummaryItem.DisplayFormat = "{0:#,##0}";
                //    break;
                case "OutsourcingSpecType_Plating":
                    //외주사양-표면처리내역
                    grdSpecList.View.Columns["MINVALUE"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                    grdSpecList.View.Columns["MINVALUE"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));
                    //WORKAREA 도금면적
                    grdSpecList.View.Columns["WORKAREA"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                    grdSpecList.View.Columns["WORKAREA"].SummaryItem.DisplayFormat = "{0:#,##0.00}";
                    //SCRAPAREA 스크랩면적
                    grdSpecList.View.Columns["SCRAPAREA"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                    grdSpecList.View.Columns["SCRAPAREA"].SummaryItem.DisplayFormat = "{0:#,##0.00}";
                    break;
                    ////도금면적
                    //grdSpecList.View.AddSpinEditColumn("WORKAREA", 80).SetDisplayFormat("#,##0.00", MaskTypes.Numeric, true).SetTextAlignment(TextAlignment.Right).SetLabel("PLATINGAREA");

            }
            grdSpecList.View.OptionsView.ShowFooter = true;
			grdSpecList.ShowStatusBar = false;
		}

		/// <summary>
		/// 공정 Grid 컬럼 초기화
		/// </summary>
		private void InitializeGridOperation()
        {
            grdOperation.GridButtonItem = GridButtonItem.None;
            grdOperation.View.SetIsReadOnly();

            //공정수순
            grdOperation.View.AddTextBoxColumn("USERSEQUENCE", 60).SetIsReadOnly().SetTextAlignment(TextAlignment.Right);

            grdOperation.View.AddTextBoxColumn("ITEMID", 120)
                .SetIsReadOnly();

            grdOperation.View.AddTextBoxColumn("ITEMVERSION", 50)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            //Site
            grdOperation.View.AddComboBoxColumn("PLANTID", 70, new SqlQuery("GetPlantList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PLANTNAME", "PLANTID")
                .SetIsHidden()
                .SetDefault(UserInfo.Current.Plant);
            //공정ID
            grdOperation.View.AddTextBoxColumn("PROCESSSEGMENTID", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            //공정명
            grdOperation.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 200).SetIsReadOnly();

            //grdOperation.View.AddTextBoxColumn("PRODUCTDEFID", 100).SetIsHidden();
            //grdOperation.View.AddTextBoxColumn("PRODUCTDEFVERSION", 100).SetIsHidden();
            grdOperation.View.AddTextBoxColumn("SUBSEGMENTID1", 100).SetIsHidden();


            grdOperation.View.PopulateColumns();
        }


        /// <summary>
        /// 외주사양정보 그리드 컬럼 초기화
        /// </summary>
        private void InitializeGridSpec()
        {
            grdSpecList.View.ClearColumns();

            switch (this._strSpecType)
            {
                case "OutsourcingSpecType_Guide":
					//GUIDE 내역 초기화
					grdSpecList.GridButtonItem = GridButtonItem.All;
					grdSpecList.LanguageKey = "OUTSOURCINGSPECGUIDE";
                    grdSpecList.Caption = Language.Get("OUTSOURCINGSPECGUIDE");

                    InitializeGridSpec_Guide();
                    break;

				case "OutsourcingSpecType_Drill":
					//DRILL 내역 초기화
					grdSpecList.GridButtonItem = GridButtonItem.All;
					grdSpecList.LanguageKey = "OUTSOURCINGSPECDRILL";
					grdSpecList.Caption = Language.Get("OUTSOURCINGSPECDRILL");

					InitializeGridSpec_Drill();
					break;

				case "OutsourcingSpecType_Attach":
					//부착 내역 초기화
					grdSpecList.GridButtonItem = GridButtonItem.All;
					grdSpecList.LanguageKey = "OUTSOURCINGSPECATTACH";
                    grdSpecList.Caption = Language.Get("OUTSOURCINGSPECATTACH");

                    InitializeGridSpec_Attach();
                    break;

                case "OutsourcingSpecType_Remove":
					//제거 내역 초기화
					grdSpecList.GridButtonItem = GridButtonItem.All;
					grdSpecList.LanguageKey = "OUTSOURCINGSPECREMOVE";
                    grdSpecList.Caption = Language.Get("OUTSOURCINGSPECREMOVE");

                    InitializeGridSpec_Remove();
                    break;

                case "OutsourcingSpecType_Cut":
					//Cut 내역 초기화
					grdSpecList.GridButtonItem = GridButtonItem.All;
					grdSpecList.LanguageKey = "OUTSOURCINGSPECCUT";
                    grdSpecList.Caption = Language.Get("OUTSOURCINGSPECCUT");

                    InitializeGridSpec_Cut();
                    break;

				case "OutsourcingSpecType_CuPlating":
					//동도금 내역 초기화
					grdSpecList.GridButtonItem = GridButtonItem.All;
					grdSpecList.LanguageKey = "OUTSOURCINGSPECCUPLATING";
					grdSpecList.Caption = Language.Get("OUTSOURCINGSPECCUPLATING");
					
					InitializeGridSpec_CuPlating();
					break;

				case "OutsourcingSpecType_Plating":
					//표면처리 내역 초기화
					grdSpecList.GridButtonItem = GridButtonItem.All;
					grdSpecList.LanguageKey = "OUTSOURCINGSPECPLATING";
                    grdSpecList.Caption = Language.Get("OUTSOURCINGSPECPLATING");

                    InitializeGridSpec_Plating();
                    break;

                case "OutsourcingSpecType_Inspection":
					//검사 내역 초기화
                    //BBT의 경우 사용자가 직접 등록 (2020-04-09)
					grdSpecList.GridButtonItem = GridButtonItem.All;
					grdSpecList.LanguageKey = "OUTSOURCINGSPECINSPECTION";
                    grdSpecList.Caption = Language.Get("OUTSOURCINGSPECINSPECTION");

                    InitializeGridSpec_Inspection();
                    break;

                case "OutsourcingSpecType_Print":
					//인쇄 내역 초기화
					grdSpecList.GridButtonItem = GridButtonItem.All;
					grdSpecList.LanguageKey = "OUTSOURCINGSPECPRINT";
                    grdSpecList.Caption = Language.Get("OUTSOURCINGSPECPRINT");

                    InitializeGridSpec_Print();
                    break;

                case "OutsourcingSpecType_Stack":
					//적층 내역 초기화
					grdSpecList.GridButtonItem = GridButtonItem.All;
					grdSpecList.LanguageKey = "OUTSOURCINGSPECSTACK";
                    grdSpecList.Caption = Language.Get("OUTSOURCINGSPECSTACK");

                    InitializeGridSpec_Stack();
                    break;

                case "OutsourcingSpecType_Mold":
					//타발 내역 초기화
					grdSpecList.GridButtonItem = GridButtonItem.Export;
					grdSpecList.LanguageKey = "OUTSOURCINGSPECMOLD";
                    grdSpecList.Caption = Language.Get("OUTSOURCINGSPECMOLD");

                    InitializeGridSpec_Mold();
                    break;

                default:
					grdSpecList.GridButtonItem = GridButtonItem.None;

					grdSpecList.LanguageKey = "OUTSOURCINGSPECINFO";
                    grdSpecList.Caption = Language.Get("OUTSOURCINGSPECINFO");

                    grdSpecList.View.PopulateColumns();
                    break;
            }
        }

		#region 외주사양정보 - GUIDE 내역 초기화
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
				.SetTextAlignment(TextAlignment.Center)
                .SetValidationIsRequired()
				.SetDefault("PNL");

			//HOLE ￠
			grdSpecList.View.AddTextBoxColumn("SPECTYPE", 80).SetLabel("HOLENAME").SetTextAlignment(TextAlignment.Right);
			//HOLE수
			grdSpecList.View.AddSpinEditColumn("HOLEQTY", 80).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetTextAlignment(TextAlignment.Right);
            //STACK 수
            grdSpecList.View.AddSpinEditColumn("STACKQTY", 80).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetTextAlignment(TextAlignment.Right);
            //구분 (수축분류/홀가공)
            grdSpecList.View.AddComboBoxColumn("SPECSUBTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OutsourcingSpecGuideType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                .SetLabel("TYPE");
            //비고
            grdSpecList.View.AddTextBoxColumn("DESCRIPTION", 180);

            //생성 / 수정
            grdSpecList.View.AddTextBoxColumn("CREATOR", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdSpecList.View.AddTextBoxColumn("CREATEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdSpecList.View.AddTextBoxColumn("MODIFIER", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdSpecList.View.AddTextBoxColumn("MODIFIEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly().SetTextAlignment(TextAlignment.Center);

            grdSpecList.View.PopulateColumns();


			DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repository = grdSpecList.View.Columns["SPECTYPE"].ColumnEdit as DevExpress.XtraEditors.Repository.RepositoryItemTextEdit;
			repository.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
			repository.Mask.EditMask = @"\d*(\.?\d*)";

		}
		#endregion

		#region 외주사양정보 - DRILL 내역 초기화
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
				.SetTextAlignment(TextAlignment.Center)
				.SetValidationIsRequired()
				.SetDefault("PNL");

			//HOLE ￠
			grdSpecList.View.AddTextBoxColumn("SPECTYPE", 80).SetLabel("HOLENAME").SetTextAlignment(TextAlignment.Right);
			//HOLE수
			grdSpecList.View.AddSpinEditColumn("HOLEQTY", 80).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetTextAlignment(TextAlignment.Right);
			//STACK 수
			grdSpecList.View.AddSpinEditColumn("STACKQTY", 80).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetTextAlignment(TextAlignment.Right);
			//양면 /멀티
			grdSpecList.View.AddComboBoxColumn("DETAILTYPE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OutsourcingSpecBothMulti", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
				.SetLabel("BOTHMULTI");
			//비고
			grdSpecList.View.AddTextBoxColumn("DESCRIPTION", 180);

			//생성 / 수정
			grdSpecList.View.AddTextBoxColumn("CREATOR", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
			grdSpecList.View.AddTextBoxColumn("CREATEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
			grdSpecList.View.AddTextBoxColumn("MODIFIER", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
			grdSpecList.View.AddTextBoxColumn("MODIFIEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly().SetTextAlignment(TextAlignment.Center);

			grdSpecList.View.PopulateColumns();

			DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repository = grdSpecList.View.Columns["SPECTYPE"].ColumnEdit as DevExpress.XtraEditors.Repository.RepositoryItemTextEdit;
			repository.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
			repository.Mask.EditMask = @"\d*(\.?\d*)";

		}
		#endregion

		#region 외주사양정보 - 부착 내역 초기화
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

			//사용층
			grdSpecList.View.AddComboBoxColumn("LAYER", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=Layer", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                .SetLabel("USERLAYER");
            //UOM
            grdSpecList.View.AddComboBoxColumn("UOMDEFID", 80, new SqlQuery("GetUomDefinitionList", "10001", "UOMCLASSID=OutsourcingSpec"), "UOMDEFNAME", "UOMDEFID")
                .SetLabel("UOM")
				.SetTextAlignment(TextAlignment.Center)
				.SetEmptyItem("PNL")
                .SetValidationIsRequired();
			//부착방식
			grdSpecList.View.AddComboBoxColumn("SPECSUBTYPE", 120, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OutsourcingSpecWorkType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
				.SetLabel("ATTACHTYPE2");
			//부착물형태
			grdSpecList.View.AddComboBoxColumn("DETAILTYPE", 120, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OutsourcingSpecAttachType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
				.SetLabel("ATTACHMENTTYPE");
			//부착 수
			grdSpecList.View.AddSpinEditColumn("ATTACHQTY", 80).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetTextAlignment(TextAlignment.Right).SetLabel("ATTACHQTY2");
            //비고
            grdSpecList.View.AddTextBoxColumn("DESCRIPTION", 180);

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
		#endregion

		#region 외주사양정보 - 제거 내역 초기화
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
				.SetTextAlignment(TextAlignment.Center)
				.SetValidationIsRequired()
				.SetDefault("PNL");
            //자재종류
            grdSpecList.View.AddComboBoxColumn("MATERIALTYPE", 120, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OutsourcingSpecRemoveMaterialType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID");
            //제거 수
            grdSpecList.View.AddSpinEditColumn("REMOVEQTY", 80).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetTextAlignment(TextAlignment.Right);
            //비고
            grdSpecList.View.AddTextBoxColumn("DESCRIPTION", 180);

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
		#endregion

		#region 외주사양정보 - Cut 내역 초기화
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
            grdSpecList.View.AddComboBoxColumn("SPECTYPE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=WorkSurface", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID").SetLabel("SIDE").SetValidationIsRequired();
            //UOM
            grdSpecList.View.AddComboBoxColumn("UOMDEFID", 80, new SqlQuery("GetUomDefinitionList", "10001", "UOMCLASSID=OutsourcingSpec"), "UOMDEFNAME", "UOMDEFID")
				.SetLabel("UOM")
				.SetTextAlignment(TextAlignment.Center)
				.SetValidationIsRequired()
				.SetDefault("PNL");
            //CUT 분류
            grdSpecList.View.AddComboBoxColumn("SPECSUBTYPE", 150, new SqlQuery("GetProcessSegmentClass", "10001", "PROCESSSEGMENTCLASSTYPE=MiddleProcessSegmentClass", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PROCESSSEGMENTCLASSNAME", "PROCESSSEGMENTCLASSID")
				.SetLabel("CUTTYPE")
				.SetIsReadOnly();
            //자재종류
            //grdSpecList.View.AddComboBoxColumn("MATERIALTYPE", 120, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OutsourcingSpecCutMaterialType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID");
            //CUT 길이
            grdSpecList.View.AddSpinEditColumn("CUTLENGTH", 80).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetTextAlignment(TextAlignment.Right);            
            //외곽CUT 유무
            grdSpecList.View.AddComboBoxColumn("ISYN", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                .SetLabel("ISOUTERCUT").SetTextAlignment(TextAlignment.Center);
			//비트사이즈
			grdSpecList.View.AddSpinEditColumn("BITSIZE", 80).SetDisplayFormat("#,##0.00", MaskTypes.Numeric, true).SetTextAlignment(TextAlignment.Right);
			//비고
			grdSpecList.View.AddTextBoxColumn("DESCRIPTION", 180);

            //생성 / 수정
            grdSpecList.View.AddTextBoxColumn("CREATOR", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdSpecList.View.AddTextBoxColumn("CREATEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdSpecList.View.AddTextBoxColumn("MODIFIER", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdSpecList.View.AddTextBoxColumn("MODIFIEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly().SetTextAlignment(TextAlignment.Center);

            grdSpecList.View.PopulateColumns();

        }
		#endregion

		#region 외주사양정보 - 동도금 내역 초기화
		/// <summary>
		/// 외주사양정보 - 동도금 내역 초기화
		/// </summary>
		private void InitializeGridSpec_CuPlating()
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
				.SetTextAlignment(TextAlignment.Center)
				.SetValidationIsRequired()
				.SetDefault("PNL");
			//도금Type
			grdSpecList.View.AddComboBoxColumn("SPECSUBTYPE", 80, new SqlQuery("GetProcessSegmentClass", "10001", "OPERATIONCLASS=25", "PROCESSSEGMENTCLASSTYPE=MiddleProcessSegmentClass", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PROCESSSEGMENTCLASSNAME", "PROCESSSEGMENTCLASSID").SetLabel("PLATINGTYPE");
			//PNL X
			grdSpecList.View.AddSpinEditColumn("PNLX", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric, false);
			//PNL Y
			grdSpecList.View.AddSpinEditColumn("PNLY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric, false);
			//비고
			grdSpecList.View.AddTextBoxColumn("DESCRIPTION", 200);

			//생성 / 수정
			grdSpecList.View.AddTextBoxColumn("CREATOR", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
			grdSpecList.View.AddTextBoxColumn("CREATEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
			grdSpecList.View.AddTextBoxColumn("MODIFIER", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
			grdSpecList.View.AddTextBoxColumn("MODIFIEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly().SetTextAlignment(TextAlignment.Center);

			grdSpecList.View.PopulateColumns();
		}

		#endregion

		#region 외주사양정보 - 표면처리 내역 초기화
		/// <summary>
		/// 외주사양정보 - 표면처리 내역 초기화
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
				.SetTextAlignment(TextAlignment.Center)
				.SetValidationIsRequired()
				.SetDefault("PNL");
            //도금Type
            grdSpecList.View.AddComboBoxColumn("SPECSUBTYPE", 80, new SqlQuery("GetProcessSegmentClass", "10001", "PROCESSSEGMENTCLASSTYPE=MiddleProcessSegmentClass", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PROCESSSEGMENTCLASSNAME", "PROCESSSEGMENTCLASSID")
				.SetLabel("PLATINGTYPE")
				.SetIsReadOnly();
            //Au두께 최소
            grdSpecList.View.AddSpinEditColumn("MINVALUE", 80).SetDisplayFormat("#,##0.00", MaskTypes.Numeric, true).SetTextAlignment(TextAlignment.Right).SetLabel("AUVALUE");
            //Au두께 최대
            //grdSpecList.View.AddSpinEditColumn("MAXVALUE", 80).SetDisplayFormat("#,##0.00", MaskTypes.Numeric, true).SetTextAlignment(TextAlignment.Right).SetLabel("AUMAXVALUE");
			//NI두께 최소
			//grdSpecList.View.AddSpinEditColumn("NIMINVALUE", 80).SetDisplayFormat("#,##0.00", MaskTypes.Numeric, true).SetTextAlignment(TextAlignment.Right).SetLabel("NIMINVALUE");
			//NI두께 최대
			//grdSpecList.View.AddSpinEditColumn("NIMAXVALUE", 80).SetDisplayFormat("#,##0.00", MaskTypes.Numeric, true).SetTextAlignment(TextAlignment.Right).SetLabel("NIMAXVALUE");
			//Pd두께 최소
			//grdSpecList.View.AddSpinEditColumn("PDMINVALUE", 80).SetDisplayFormat("#,##0.00", MaskTypes.Numeric, true).SetTextAlignment(TextAlignment.Right);
			//Sn두께 최소
			//grdSpecList.View.AddSpinEditColumn("SNMINVALUE", 80).SetDisplayFormat("#,##0.00", MaskTypes.Numeric, true).SetTextAlignment(TextAlignment.Right);
			//도금면적
			grdSpecList.View.AddSpinEditColumn("WORKAREA", 80).SetDisplayFormat("#,##0.00", MaskTypes.Numeric, true).SetTextAlignment(TextAlignment.Right).SetLabel("PLATINGAREA");
            //스크랩면적
            grdSpecList.View.AddSpinEditColumn("SCRAPAREA", 80).SetDisplayFormat("#,##0.00", MaskTypes.Numeric, true).SetTextAlignment(TextAlignment.Right);

            //비고
            grdSpecList.View.AddTextBoxColumn("DESCRIPTION", 200);

            //생성 / 수정
            grdSpecList.View.AddTextBoxColumn("CREATOR", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdSpecList.View.AddTextBoxColumn("CREATEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdSpecList.View.AddTextBoxColumn("MODIFIER", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdSpecList.View.AddTextBoxColumn("MODIFIEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly().SetTextAlignment(TextAlignment.Center);

            grdSpecList.View.PopulateColumns();
        }
		#endregion

		#region 외주사양정보 - 검사 내역 초기화
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
				.SetTextAlignment(TextAlignment.Center)
				.SetLabel("UOM")
                .SetValidationIsRequired()
				.SetDefault("PNL");
            //LAYER
            grdSpecList.View.AddComboBoxColumn("LAYER", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=Layer", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
				.SetTextAlignment(TextAlignment.Center)
				.SetIsReadOnly().SetLabel("LAYER2");
			//작업방식
			grdSpecList.View.AddComboBoxColumn("SPECSUBTYPE", 100, new SqlQuery("GetProcessSegmentClass", "10001", "PROCESSSEGMENTCLASSTYPE=MiddleProcessSegmentClass", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PROCESSSEGMENTCLASSNAME", "PROCESSSEGMENTCLASSID")
				.SetLabel("ISMAINSEGMENT2")
				.SetIsReadOnly();
			//자동/수동
			grdSpecList.View.AddComboBoxColumn("DETAILTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OutsourcingSpecAutoManual", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
				.SetLabel("AUTOMANUAL");

            //플라잉포인트(FLYINGPOINT)
            grdSpecList.View.AddTextBoxColumn("TOOLTYPE", 100)
                .SetLabel("FLYINGPOINT");

            //홀 유무
            grdSpecList.View.AddComboBoxColumn("ISYN", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                .SetLabel("ISHOLE")
				.SetTextAlignment(TextAlignment.Center);
            //연배
            grdSpecList.View.AddSpinEditColumn("USEDFACTOR", 80).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetTextAlignment(TextAlignment.Right);
            //OPEN / SHORT
			grdSpecList.View.AddComboBoxColumn("OPENSHORT", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OutsourcingSpecOpenShort", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
				.SetTextAlignment(TextAlignment.Center);

            //비고
            grdSpecList.View.AddTextBoxColumn("DESCRIPTION", 180);

            //생성 / 수정
            grdSpecList.View.AddTextBoxColumn("CREATOR", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdSpecList.View.AddTextBoxColumn("CREATEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdSpecList.View.AddTextBoxColumn("MODIFIER", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdSpecList.View.AddTextBoxColumn("MODIFIEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly().SetTextAlignment(TextAlignment.Center);

            grdSpecList.View.PopulateColumns();
        }
		#endregion

		#region 외주사양정보 - 인쇄 내역 초기화
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
				.SetTextAlignment(TextAlignment.Center)
				.SetValidationIsRequired()
				.SetDefault("PNL");
            //잉크종류
            grdSpecList.View.AddComboBoxColumn("SPECSUBTYPE", 110, new SqlQuery("GetProcessSegmentClass", "10001", "OPERATIONCLASS=50", "PROCESSSEGMENTCLASSTYPE=MiddleProcessSegmentClass", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PROCESSSEGMENTCLASSNAME", "PROCESSSEGMENTCLASSID")
                .SetLabel("INKTYPE")
				.SetIsReadOnly();
            //PNL X
            grdSpecList.View.AddSpinEditColumn("PNLX", 80).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetTextAlignment(TextAlignment.Right);
            //PNL Y
            grdSpecList.View.AddSpinEditColumn("PNLY", 80).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetTextAlignment(TextAlignment.Right);
			//INKt색상구분
			grdSpecList.View.AddComboBoxColumn("DETAILTYPE", new SqlQuery("GetCodeList", "00001", "CODECLASSID=InkColorType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetLabel("INKCOLORTYPE");
			//인쇄도수
			grdSpecList.View.AddSpinEditColumn("PRINTCOLORQTY", 80).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetTextAlignment(TextAlignment.Right);
            //판넬수
            grdSpecList.View.AddSpinEditColumn("PNLQTY", 80).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetTextAlignment(TextAlignment.Right);
            //투입방향
            grdSpecList.View.AddComboBoxColumn("INPUTTO", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OutsourcingSpecInputTo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID");

            //비고
            grdSpecList.View.AddTextBoxColumn("DESCRIPTION", 180);

            //생성 / 수정
            grdSpecList.View.AddTextBoxColumn("CREATOR", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
			grdSpecList.View.AddTextBoxColumn("CREATEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdSpecList.View.AddTextBoxColumn("MODIFIER", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
			grdSpecList.View.AddTextBoxColumn("MODIFIEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly().SetTextAlignment(TextAlignment.Center);

            grdSpecList.View.PopulateColumns();
        }
		#endregion

		#region 외주사양정보 - 적층 내역 초기화
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
				.SetTextAlignment(TextAlignment.Center)
				.SetValidationIsRequired()
				.SetDefault("PNL");
			//진공/일반
			grdSpecList.View.AddComboBoxColumn("DETAILTYPE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OutsourcingSpecVacuumNormal", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
				.SetLabel("VACUUMNORMAL");
			//LAYUP구조
			grdSpecList.View.AddComboBoxColumn("SPECSUBTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OutsourcingSpecStackMaterialType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
				.SetLabel("LAYUPSTRUCT");
			//자재코드
			InitializeGridSpec_MaterialPopup();
            grdSpecList.View.AddTextBoxColumn("MATERIALDEFVERSION", 80).SetIsHidden();
            //자재명
            grdSpecList.View.AddTextBoxColumn("MATERIALNAME", 150).SetIsReadOnly();
            //규격
            grdSpecList.View.AddTextBoxColumn("SPEC", 80).SetIsReadOnly();
			//COMPONENT UOM
			grdSpecList.View.AddComboBoxColumn("COMPONENTUOM", 80, new SqlQuery("GetUOMList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "UOMDEFID", "UOMDEFNAME").SetIsReadOnly();
			//소요량
			grdSpecList.View.AddSpinEditColumn("MINVALUE", 100).SetDisplayFormat("#,##0.000000000", MaskTypes.Numeric, true).SetTextAlignment(TextAlignment.Right).SetLabel("COMPONENTQTY");
			//PVC(t)
			//grdSpecList.View.AddSpinEditColumn("MAXVALUE", 100).SetDisplayFormat("#,##0.#########", MaskTypes.Numeric, true).SetTextAlignment(TextAlignment.Right).SetLabel("PVCT");
            //PVC(t)  2021-01-29 오근영 수정 두께 표시 변경
            grdSpecList.View.AddSpinEditColumn("THICK", 100).SetDisplayFormat("#,##0.#########", MaskTypes.Numeric, true).SetTextAlignment(TextAlignment.Right).SetLabel("PVCT");
            //쿠폰 유무
            grdSpecList.View.AddComboBoxColumn("ISYN", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID").SetLabel("ISCOUPON");
			//작업면적
			grdSpecList.View.AddSpinEditColumn("WORKAREA", 80).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetTextAlignment(TextAlignment.Right).SetLabel("WORKDIMENSIONS");
			//건수
			grdSpecList.View.AddSpinEditColumn("CASEQTY", 80).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetTextAlignment(TextAlignment.Right).SetLabel("OSPETCWORKCOUNT");

            //비고
            grdSpecList.View.AddTextBoxColumn("DESCRIPTION", 180);

            //생성 / 수정
            grdSpecList.View.AddTextBoxColumn("CREATOR", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
			grdSpecList.View.AddTextBoxColumn("CREATEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdSpecList.View.AddTextBoxColumn("MODIFIER", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
			grdSpecList.View.AddTextBoxColumn("MODIFIEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly().SetTextAlignment(TextAlignment.Center);

            grdSpecList.View.PopulateColumns();


            (grdSpecList.View.Columns["MATERIALDEFID"].ColumnEdit as DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit).ButtonClick +=Item_ButtonClick;

        }
		#endregion

		#region 외주사양정보 - 금형 타발 내역 초기화
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
				.SetTextAlignment(TextAlignment.Center)
				.SetValidationIsRequired()
				.SetDefault("PNL");

			//TOOL구분
			grdSpecList.View.AddComboBoxColumn("TOOLTYPE", 80, new SqlQuery("GetTypeList", "10001", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}", "CODECLASSID=DurableClass"), "CODENAME", "CODEID")
				.SetLabel("DURABLECATEGORYCODE")
				.SetIsReadOnly();
			//치공구ID
			//InitializeGridSpec_ToolPopup();
			grdSpecList.View.AddTextBoxColumn("TOOLID", 100).SetIsReadOnly().SetTextAlignment(TextAlignment.Center).SetLabel("DURABLEDEFID");
			grdSpecList.View.AddTextBoxColumn("TOOLVERSION", 60).SetIsReadOnly().SetTextAlignment(TextAlignment.Center).SetLabel("DURABLEDEFVERSION");
			//TOOL유형1
			grdSpecList.View.AddComboBoxColumn("SPECTYPE", 80, new SqlQuery("GetToolTypeByClassId", "10001", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
				.SetLabel("TOOLCATEGORYDETAIL")
				.SetIsReadOnly();
			//TOOLD유형2
			grdSpecList.View.AddComboBoxColumn("SPECSUBTYPE", 80, new SqlQuery("GetDurableClassType2", "10001", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
				.SetLabel("TOOLDETAIL")
				.SetIsReadOnly();
			//TOOL 형식
			grdSpecList.View.AddComboBoxColumn("TOOLFORM", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ToolForm", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
				.SetLabel("TOOLFORMCODE")
				.SetIsReadOnly();
			//합수
			grdSpecList.View.AddSpinEditColumn("SUMMARY", 70).SetIsReadOnly().SetLabel("ARRAY");
			//사용층
			grdSpecList.View.AddComboBoxColumn("LAYER", 70, new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CODECLASSID=FilmUseLayer1"), "CODENAME", "CODEID").SetIsReadOnly().SetLabel("USERLAYER");
			//타수(합수)
			grdSpecList.View.AddTextBoxColumn("TOOLHITCOUNT", 70).SetIsReadOnly().SetLabel("HITCOUNT");

			//아대/일반
			grdSpecList.View.AddComboBoxColumn("DETAILTYPE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OutsourcingSpecPanelGuide", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
				.SetLabel("PANELGUIDENORMAL");

			//비고
			grdSpecList.View.AddTextBoxColumn("DESCRIPTION", 180);

			//생성 / 수정
			grdSpecList.View.AddTextBoxColumn("CREATOR", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
			grdSpecList.View.AddTextBoxColumn("CREATEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
			grdSpecList.View.AddTextBoxColumn("MODIFIER", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
			grdSpecList.View.AddTextBoxColumn("MODIFIEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly().SetTextAlignment(TextAlignment.Center);

			grdSpecList.View.PopulateColumns();

			//(grdSpecList.View.Columns["TOOLID"].ColumnEdit as DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit).ButtonClick += Item_ButtonClick2;
		}

		#endregion


		/// <summary>
		/// 팝업형 그리드 컬럼 생성 - 자재코드
		/// </summary>
		private void InitializeGridSpec_MaterialPopup()
        {
            string productDefId = this._focusProductID;
            string productDefVersion = this._focusProductVersion;
            string segmentID = this._focusProcessSegmentID;


            var materialPopupColumn = this.grdSpecList.View.AddSelectPopupColumn("MATERIALDEFID", 100, new SqlQuery("GetRoutingBomListPopup", "10021", $"ENTERPRISEID={UserInfo.Current.Enterprise}"))
                .SetLabel("MATERIALDEF")
                .SetPopupLayout("SELECTCONSUMABLE", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupResultCount(1)
                .SetPopupResultMapping("MATERIALDEFID", "COMPONENTITEMID")
                .SetPopupLayoutForm(800, 800, FormBorderStyle.FixedToolWindow)
                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {
                    foreach (DataRow row in selectedRows)
                    {
                        dataGridRow["MATERIALDEFVERSION"] = Format.GetString(row["COMPONENTITEMVERSION"]);
                        dataGridRow["MATERIALNAME"] = Format.GetString(row["COMPONENTITEMNAME"]);
                        dataGridRow["COMPONENTUOM"] = Format.GetString(row["COMPONENTUOM"]);
                        //dataGridRow["MINVALUE"] = Format.GetDouble(row["COMPONENTQTY"], 0);
                        dataGridRow["SPEC"] = Format.GetString(row["SPEC"]);
					}
                })
				.SetPopupAutoFillColumns("COMPONENTITEMNAME");

            // 팝업에서 사용할 조회조건 항목 추가
            materialPopupColumn.Conditions.AddTextBox("P_ITEMID").SetLabel("CONSUMABLEIDNAME");
            materialPopupColumn.Conditions.AddTextBox("PRODUCTDEFID").SetPopupDefaultByGridColumnId("PRODUCTDEFID").SetIsHidden();
            materialPopupColumn.Conditions.AddTextBox("PRODUCTDEFVERSION").SetPopupDefaultByGridColumnId("PRODUCTDEFVERSION").SetIsHidden();

            // 팝업 그리드 설정
            materialPopupColumn.GridColumns.AddTextBoxColumn("COMPONENTITEMID", 100).SetLabel("ITEMID1");
            materialPopupColumn.GridColumns.AddTextBoxColumn("COMPONENTITEMVERSION", 80).SetLabel("ITEMVERSION");
            materialPopupColumn.GridColumns.AddTextBoxColumn("COMPONENTITEMNAME", 250).SetLabel("ITEMNAME1");
            materialPopupColumn.GridColumns.AddComboBoxColumn("COMPONENTUOM", 80, new SqlQuery("GetUOMList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "UOMDEFID", "UOMDEFNAME").SetIsReadOnly();
            //materialPopupColumn.GridColumns.AddSpinEditColumn("COMPONENTQTY", 100).SetDisplayFormat("#,##0.000000000", MaskTypes.Numeric, true).SetTextAlignment(TextAlignment.Right);
			//규격
			materialPopupColumn.GridColumns.AddTextBoxColumn("SPEC", 100);
		}

		/// <summary>
		/// 팝업형 그리드 컬럼 초기화 - 치공구ID
		/// </summary>
		private void InitializeGridSpec_ToolPopup()
		{
            string productDefId = this._focusProductID;
            string productDefVersion = this._focusProductVersion;
			string segmentID = this._focusProcessSegmentID;

			var toolPopupColumn = this.grdSpecList.View.AddSelectPopupColumn("TOOLID", new SqlQuery("GetDurableDefList", "10001", "DURABLETYPE=Tool", $"PLANTID={UserInfo.Current.Plant}"
																																					, $"LANGUAGETYPE={UserInfo.Current.LanguageType}"
																																					, $"PROCESSSEGMENTID={segmentID}"
																																					, $"PRODUCTDEFID={productDefId}"
																																					, $"PRODUCTDEFVERSION={productDefVersion}"))
				.SetPopupLayout("SELECTDURABLE", PopupButtonStyles.Ok_Cancel, true, false)
				.SetPopupResultCount(1)
				.SetPopupLayoutForm(1000, 800, FormBorderStyle.SizableToolWindow)
				.SetPopupApplySelection((selectedRows, dataGridRow) =>
				{
					DataRow row = selectedRows.FirstOrDefault();
					if(row == null) return;

					dataGridRow["SPECSUBTYPE"] = Format.GetString(row["TOOLTYPE"]);//유형1 = TOOLTYPE
					dataGridRow["MANUFACTURINGSPEC"] = Format.GetString(row["TOOLDETAILTYPE"]);//유형2 = TOOLDETAILTYPE
					dataGridRow["TOOLTYPE"] = Format.GetString(row["DURABLECLASSID"]);//TOOL 구분 = DURABLECLASSID
					dataGridRow["TOOLFORM"] = Format.GetString(row["FORM"]);//TOOL 형식 = FORM
					dataGridRow["TOOLVERSION"] = Format.GetString(row["TOOLVERSION"]);//치공구 Rev
					dataGridRow["SUMMARY"] = Format.GetString(row["SUMMARY"]);//합수
					dataGridRow["LAYER"] = Format.GetString(row["FILMUSELAYER1"]);//사용층
					dataGridRow["TOOLHITCOUNT"] = row["TOOLHITCOUNT"];//타수
				})
				.SetPopupAutoFillColumns("TOOLNAME")
				.SetLabel("DURABLEDEFID");

			// 팝업에서 사용할 조회조건 항목 추가
			toolPopupColumn.Conditions.AddComboBox("DURABLECLASSID", new SqlQuery("GetTypeList", "10001", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}", "CODECLASSID=DurableClass"), "CODENAME", "CODEID")
				.SetDefault("ToolType1");
			toolPopupColumn.Conditions.AddTextBox("TXTTOOLNAME");

			//TOOL 구분(DURABLECLASSID)
			toolPopupColumn.GridColumns.AddComboBoxColumn("DURABLECLASSID", 80, new SqlQuery("GetTypeList", "10001", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}", "CODECLASSID=DurableClass"), "CODENAME", "CODEID")
				.SetTextAlignment(TextAlignment.Center)
				.SetLabel("DURABLECATEGORYCODE");
			//TOOL ID
			toolPopupColumn.GridColumns.AddTextBoxColumn("TOOLID", 100).SetLabel("DURABLEDEFID");
			//TOOL VERSION
			toolPopupColumn.GridColumns.AddTextBoxColumn("TOOLVERSION", 50).SetTextAlignment(TextAlignment.Center).SetLabel("DURABLEDEFVERSION");
			//TOOL 명
			toolPopupColumn.GridColumns.AddTextBoxColumn("TOOLNAME", 100).SetLabel("DURABLEDEFNAME");
			//TOOL TYPE
			toolPopupColumn.GridColumns.AddComboBoxColumn("TOOLTYPE", 60, new SqlQuery("GetToolTypeByClassId", "10001", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
				.SetTextAlignment(TextAlignment.Center)
				.SetLabel("TOOLCATEGORYDETAIL");
			//TOOL DETAIL TYPE
			toolPopupColumn.GridColumns.AddComboBoxColumn("TOOLDETAILTYPE", 60, new SqlQuery("GetDurableClassType2", "10001", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
				.SetTextAlignment(TextAlignment.Center)
				.SetLabel("TOOLDETAIL");
			//TOOL 형식(FORM)
			toolPopupColumn.GridColumns.AddComboBoxColumn("FORM", 80, new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CODECLASSID=ToolForm"), "CODENAME", "CODEID")
				.SetLabel("TOOLFORMCODE");
			//합수
			toolPopupColumn.GridColumns.AddSpinEditColumn("SUMMARY", 70)
				.SetLabel("ARRAY");
			//사용층
			toolPopupColumn.GridColumns.AddComboBoxColumn("FILMUSELAYER1", 70, new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CODECLASSID=FilmUseLayer1"), "CODENAME", "CODEID")
				.SetLabel("USERLAYER");
			//타수
			toolPopupColumn.GridColumns.AddSpinEditColumn("TOOLHITCOUNT", 70).SetLabel("HITCOUNT");
		}

		/// <summary>
		/// 팝업 컬럼 x버튼 누를 때 이벤트 - 치공구
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Item_ButtonClick2(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			if (e.Button.Kind.Equals(DevExpress.XtraEditors.Controls.ButtonPredefines.Clear))
			{
				grdSpecList.View.SetFocusedRowCellValue("TOOLTYPE", string.Empty);
				grdSpecList.View.SetFocusedRowCellValue("SPECSUBTYPE", string.Empty);
				grdSpecList.View.SetFocusedRowCellValue("MANUFACTURINGSPEC", string.Empty);
				grdSpecList.View.SetFocusedRowCellValue("TOOLFORM", string.Empty);
				grdSpecList.View.SetFocusedRowCellValue("DURABLEDEFVERSION", string.Empty);
				grdSpecList.View.SetFocusedRowCellValue("SUMMARY", string.Empty);
				grdSpecList.View.SetFocusedRowCellValue("LAYER", string.Empty);
				grdSpecList.View.SetFocusedRowCellValue("HITCOUNT", string.Empty);
			}
		}

		/// <summary>
		/// 팝업 컬럼 x버튼 누를 때 이벤트 - 자재코드
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Item_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind.Equals(DevExpress.XtraEditors.Controls.ButtonPredefines.Clear))
            {
                grdSpecList.View.SetFocusedRowCellValue("MATERIALDEFVERSION", string.Empty);
                grdSpecList.View.SetFocusedRowCellValue("MATERIALNAME", string.Empty);
                grdSpecList.View.SetFocusedRowCellValue("COMPONENTUOM", string.Empty);
                //grdSpecList.View.SetFocusedRowCellValue("COMPONENTQTY", string.Empty);
				grdSpecList.View.SetFocusedRowCellValue("SPEC", string.Empty);
			}
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
			this.grdSpecList.View.CellValueChanged += View_CellValueChanged;
			this.grdSpecList.View.ShowingEditor += View_ShowingEditor;
			this.grdSpecList.ToolbarDeletingRow += GrdSpecList_ToolbarDeletingRow;
			grdSpecList.View.CustomDrawFooterCell += View_CustomDrawFooterCell;
		}

		/// <summary>
		/// Footer 합계 스타일
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void View_CustomDrawFooterCell(object sender, DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventArgs e)
		{
            //Font ft = new Font("Mangul Gothic", 10F, FontStyle.Bold);
            Font ft = new Font("Consolas", 10F, FontStyle.Bold);
            e.Appearance.BackColor = Color.LightBlue;
			e.Appearance.FillRectangle(e.Cache, e.Bounds);
			e.Info.AllowDrawBackground = false;
			e.Appearance.Font = ft;
		}

		/// <summary>
		/// row 삭제 시 이벤트 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GrdSpecList_ToolbarDeletingRow(object sender, CancelEventArgs e)
		{
			if(_strSpecType.Equals("OutsourcingSpecType_Inspection") || _strSpecType.Equals("OutsourcingSpecType_Print"))
			{
				_isDelete = true;
				DataTable dt = grdSpecList.DataSource as DataTable;
				//dt.Columns.Remove("_STATE_");
			}
		}

		/// <summary>
		/// 셀 ReadOnly 처리
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void View_ShowingEditor(object sender, CancelEventArgs e)
		{
			if(this._strSpecType.Equals("OutsourcingSpecType_Mold"))
			{
				string durableClass = Format.GetFullTrimString(grdSpecList.View.GetFocusedRowCellValue("DETAILTYPE"));
				string focusColumn = grdSpecList.View.FocusedColumn.FieldName;

				if(!string.IsNullOrWhiteSpace(durableClass) && !durableClass.Equals("ToolType4"))
				{
					if(focusColumn.Equals("DETAILTYPE"))
					{
						e.Cancel = true;
					}
				}
			}
		}

		/// <summary>
		/// cell value change 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void View_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
		{
			//GUIDE 내역, DRILL 내역 HOLE 값 변경 시
			/*
			if((this._strSpecType.Equals("OutsourcingSpecType_Guide") || this._strSpecType.Equals("OutsourcingSpecType_Drill")) 
				&& e.Column.FieldName.Equals("SPECTYPE") && e.Column.Caption.Equals(Language.Get("HOLE")))
			{
				DataTable dt = SqlExecuter.Query("GetTypeList", "10001", new Dictionary<string, object>() { { "CODECLASSID", "" }, { "CODEID", e.Value}, { "LANGUAGETYPE", UserInfo.Current.LanguageType } });
				if(dt.Rows.Count > 0)
				{
					this.grdSpecList.View.CellValueChanged -= View_CellValueChanged;
					grdSpecList.View.SetRowCellValue(e.RowHandle, "HOLENAME", dt.Rows[0]["CODENAME"]);
					this.grdSpecList.View.CellValueChanged += View_CellValueChanged;
				}
			}
			*/
			//CUT 내역 CUT분류 값 변경 시
			if(this._strSpecType.Equals("OutsourcingSpecType_Cut") 
				&& e.Column.FieldName.Equals("SPECSUBTYPE") && e.Column.Caption.Equals(Language.Get("CUTTYPE")))
			{
				this.grdSpecList.View.CellValueChanged -= View_CellValueChanged;
				if (e.Value.Equals("6038")) //중공정 PNL ROUTER만 Y
				{
					grdSpecList.View.SetRowCellValue(e.RowHandle, "ISYN", "Y");
				}
				else
				{
					grdSpecList.View.SetRowCellValue(e.RowHandle, "ISYN", "N");
				}
				this.grdSpecList.View.CellValueChanged += View_CellValueChanged;
			}

			//검사 내역 작업방법 값 변경 시
			if(this._strSpecType.Equals("OutsourcingSpecType_Inspection") 
				&& e.Column.FieldName.Equals("SPECSUBTYPE") && e.Column.Caption.Equals(Language.Get("ISMAINSEGMENT")))
			{
				this.grdSpecList.View.CellValueChanged -= View_CellValueChanged;
				if(e.Value.Equals("HOLEAOI"))
				{
					grdSpecList.View.SetRowCellValue(e.RowHandle, "ISYN", "Y");
				}
				else if(e.Value.Equals("AOI"))
				{
					grdSpecList.View.SetRowCellValue(e.RowHandle, "ISYN", "N");
				}
				this.grdSpecList.View.CellValueChanged += View_CellValueChanged;
			}
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

            int seqNo = Format.GetInteger(grdOperation.View.GetRowCellValue(e.RowHandle, "OUTSOURCINGSPECNO"), 0);

            string type = Format.GetFullTrimString(this.grdOperation.View.GetRowCellValue(e.RowHandle, "SUBSEGMENTID1"));

            if (!string.IsNullOrEmpty(type))
            {
                if (seqNo > 0)
                {
                    e.Appearance.BackColor = Color.Pink;
                }
                else
                {
                    e.Appearance.BackColor = Color.Yellow;
                    e.Appearance.ForeColor = Color.Black;
                }
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
            args.NewRow["PRODUCTDEFID"] = focusedRow["ITEMID"];
            args.NewRow["PRODUCTDEFVERSION"] = focusedRow["ITEMVERSION"];
            args.NewRow["PROCESSSEGMENTID"] = focusedRow["PROCESSSEGMENTID"];
            args.NewRow["OUTSOURCINGSPECNO"] = this.grdSpecList.View.RowCount;

			if(_strSpecType.Equals("OutsourcingSpecType_Cut")
			|| _strSpecType.Equals("OutsourcingSpecType_Plating")
			|| _strSpecType.Equals("OutsourcingSpecType_Inspection")
			|| _strSpecType.Equals("OutsourcingSpecType_Print")
			|| _strSpecType.Equals("OutsourcingSpecType_CuPlating"))
			{
				args.NewRow["SPECSUBTYPE"] = focusedRow["PROCESSSEGMENTCLASSID"];

				switch(_strSpecType)
				{
					case "OutsourcingSpecType_Cut":
						//PNL ROUTER만 Y 나머지는 N
						if (focusedRow["PROCESSSEGMENTCLASSID"].Equals("6038"))
						{
							args.NewRow["ISYN"] = "Y";
						}
						else
						{
							args.NewRow["ISYN"] = "N";
						}
						break;
					case "OutsourcingSpecType_Inspection":
						//HOLE AOI는 Y AOI는 N
						if (focusedRow["PROCESSSEGMENTCLASSID"].Equals("7014"))
						{
							args.NewRow["ISYN"] = "Y";
						}
						else if(focusedRow["PROCESSSEGMENTCLASSID"].Equals("7012"))
						{
							args.NewRow["ISYN"] = "N";
						}
						break;
					case "OutsourcingSpecType_Print":
					case "OutsourcingSpecType_CuPlating":
						args.NewRow["PNLX"] = focusedRow["PNLX"];
						args.NewRow["PNLY"] = focusedRow["PNLY"];
						break;
				}
			}
			
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


            this._focusProductID = this.grdOperation.View.GetFocusedRowCellValue("ITEMID").ToString();
            this._focusProductVersion = this.grdOperation.View.GetFocusedRowCellValue("ITEMVERSION").ToString();
            this._focusProcessSegmentID = this.grdOperation.View.GetFocusedRowCellValue("PROCESSSEGMENTID").ToString();

            if (this._strSpecType.Equals(this.grdOperation.View.GetFocusedRowCellValue("SUBSEGMENTID1").ToString()) == false)
            {
                this._strSpecType = this.grdOperation.View.GetFocusedRowCellValue("SUBSEGMENTID1").ToString();
                InitializeGridSpec();
				InitializeFooterSummary();

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
                this._focusProductID = string.Empty;
                this._focusProductVersion = string.Empty;
                this._focusProcessSegmentID = string.Empty;
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
            int cscount = 0;
            int sscount = 0;
            DataTable changed = new DataTable();
                changed = grdSpecList.GetChangedRows();
 
            switch (_strSpecType)
            {
                case "OutsourcingSpecType_Plating":
                    DataTable dt = grdSpecList.DataSource as DataTable;
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (Format.GetString(dr["SPECTYPE"]).Equals("CS"))
                        {
                            cscount++;
                        }
                        if (Format.GetString(dr["SPECTYPE"]).Equals("SS"))
                        {
                            sscount++;
                        }
                        if (cscount > 1 || sscount > 1)
                            throw MessageException.Create("NotTwoCSSS");

                    }
                    break;
            } 
            DataTable dtop = grdSpecList.DataSource as DataTable;

            DataRow process = grdOperation.View.GetFocusedDataRow();

            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("PROCESSSEGMENTID", process["PROCESSSEGMENTID"]);
            values.Add("P_PRODUCTDEFID", this._focusProductID);
            values.Add("P_PRODUCTDEFVERSION", this._focusProductVersion);
            values.Add("P_PROCESSSEGMENTID", this._focusProcessSegmentID);
            values.Add("P_SPECTYPE", this._strSpecType);
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("PLANTID", UserInfo.Current.Plant);
            DataTable operationtb = SqlExecuter.Query("GetRoutingOperationList", "10003", values);
            changed.Columns.Add("OPERATIONID");
            changed.Columns.Add("SEQUENCE");
            changed.Columns.Add("P_SPECTYPE");

            string operationId = Format.GetString(operationtb.Rows[0]["OPERATIONID"]);
            values.Add("OPERATIONID", operationId);
            string operationsequence = "";
            DataTable opspecdt = SqlExecuter.Query("GetOperationspecvalue", "10001", values);
            if(opspecdt.Rows.Count>0)
            {
                operationsequence = Format.GetString(opspecdt.Rows[0]["SEQUENCE"]);
            }


            foreach (DataRow dr in changed.Rows)
            {
                dr["OPERATIONID"] = operationId;
                dr["PROCESSSEGMENTID"] = this._focusProcessSegmentID;
                dr["P_SPECTYPE"] = this._strSpecType;
                // 2020.05.11-유석진-스크랩면적값이 없으면 0으로 등록
                if (string.IsNullOrEmpty(dr["SCRAPAREA"].ToString()))
                {
                    dr["SCRAPAREA"] = 0;
                }

                if (opspecdt.Rows.Count > 0)
                {
                    dr["SEQUENCE"] = operationsequence;
                }
            }

            ExecuteRule("SaveProductOutsourcingSpec", changed);
            int amcount = 0;
            int dcount = 0;
            foreach(DataRow dr in changed.Rows)
            {
                 if(Format.GetString(dr["_STATE_"]).Equals("added") || Format.GetString(dr["_STATE_"]).Equals("modified"))
                {
                    amcount++;

                }
                 else
                {
                    dcount++;
                }


            }
            

            if ((dcount == 1 && amcount == 1) || amcount == 2 || (amcount==1 && dcount==0) || (dcount==1 && dtop.Rows.Count==2))
            {




                DataTable dt2 = SqlExecuter.Query("SelectProductOutsourcingSpec", "10001", values);

                cscount = 0;
                sscount = 0;
                foreach (DataRow dr in dt2.Rows)
                {
                    if (Format.GetString(dr["SPECTYPE"]).Equals("CS"))
                    {
                        cscount++;
                    }
                    if (Format.GetString(dr["SPECTYPE"]).Equals("SS"))
                    {
                        sscount++;
                    }
                }
                switch (_strSpecType)
                {
                    case "OutsourcingSpecType_Plating":
                        if (cscount == 1 && sscount == 1)
                        {

                            values.Add("ITEMID", this._focusProductID);
                            values.Add("ITEMVERSION", this._focusProductVersion);

                            DataTable productspec = SqlExecuter.Query("SelectProductSpec", "10001", values);


                            DataTable bomtable = new DataTable();
                            bomtable.Columns.Add("PRODUCTDEFID", typeof(string));
                            bomtable.Columns.Add("PRODUCTDEFVERSION", typeof(string));
                            bomtable.Columns.Add("OPERATIONID", typeof(string));
                            bomtable.Columns.Add("ENTERPRISEID", typeof(string));
                            bomtable.Columns.Add("MATERIALDEFID", typeof(string));
                            bomtable.Columns.Add("MATERIALDEFVERSION", typeof(string));
                            bomtable.Columns.Add("MULTIPLE", typeof(double));
                            bomtable.Columns.Add("VALIDSTATE", typeof(string));
                            bomtable.Columns.Add("PROCESSSEGMENTID", typeof(string));
                            bomtable.Columns.Add("PROCESSSEGMENTVERSION", typeof(string));
                            bomtable.Columns.Add("PLANTID", typeof(string));
                            bomtable.Columns.Add("WORKAREA", typeof(double));
                            bomtable.Columns.Add("SCRAPAREA", typeof(double));
                            bomtable.Columns.Add("MINVALUE", typeof(double));
                            bomtable.Columns.Add("PCSPNL", typeof(double));
                            bomtable.Columns.Add("_STATE_", typeof(string));
                            GetNumber number = new GetNumber();





                            values.Add("PRODUCTDEFID", this._focusProductID);
                            values.Add("PROCESSDEFVERSION", this._focusProductVersion);


                            DataTable materialtb = SqlExecuter.Query("GetRoutingBomList", "10001", values);

                            DataRow row = bomtable.NewRow();
                            row["_STATE_"] = "added";
                            foreach (DataRow dr in materialtb.Rows)
                            {
                                if (Format.GetString(dr["COMPONENTITEMID"]).Equals("X360330001") || (dcount == 1 && dtop.Rows.Count == 2))
                                {
                                    bomtable.Columns.Add("SEQUENCE", typeof(double));
                                    row["SEQUENCE"] = dr["SEQUENCE"];
                                    row["_STATE_"] = "modified";
                                    break;
                                }

                            }
                            row["PRODUCTDEFID"] = this._focusProductID;
                            row["PRODUCTDEFVERSION"] = this._focusProductVersion;
                            row["OPERATIONID"] = operationId;
                            row["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                            row["MATERIALDEFID"] = "X360330001";
                            row["MATERIALDEFVERSION"] = "*";
                            row["MULTIPLE"] = 1;
                            row["PLANTID"] = UserInfo.Current.Plant;
                            row["VALIDSTATE"] = "Valid";
                            row["PROCESSSEGMENTID"] = process["PROCESSSEGMENTID"]; row["PROCESSSEGMENTID"] = process["PROCESSSEGMENTID"];
                            row["PROCESSSEGMENTVERSION"] = "*";
                            row["WORKAREA"] = Format.GetDouble(dt2.Rows[0]["WORKAREA"], 0) + Format.GetDouble(dt2.Rows[1]["WORKAREA"], 0);
                            row["SCRAPAREA"] = Format.GetDouble(dt2.Rows[0]["SCRAPAREA"], 0) + Format.GetDouble(dt2.Rows[1]["SCRAPAREA"], 0);
                            row["MINVALUE"] = Format.GetDouble(dt2.Rows[0]["MINVALUE"], 0) + Format.GetDouble(dt2.Rows[1]["MINVALUE"], 0);
                            row["PCSPNL"] = Convert.ToDouble(Format.GetString(productspec.Rows[0]["PCSPNL"]));
                            bomtable.Rows.Add(row);

                            MessageWorker worker = new MessageWorker("SaveProductOutsourcingSpec");
                            worker.SetBody(new MessageBody()
                        {
                             { "bom", bomtable}
                        });

                            var result = worker.Execute<DataTable>();
                            var resultData = result.GetResultSet();

                            if (Format.GetString(row["_STATE_"]).Equals("modified"))
                            {
                                this.ShowMessage("BOMPgcMaterialChange", "\r\n", "X360330001" + "\r\n"
                                     , "P.G.C (포타슘 골드 시아나이드)" + "\r\n", resultData.Rows[0]["QTY"].ToString());

                            }
                            else
                            {
                                this.ShowMessage("BOMPgcMaterialAdd", "\r\n", "X360330001" + "\r\n"
                                , "P.G.C (포타슘 골드 시아나이드)" + "\r\n", resultData.Rows[0]["QTY"].ToString());

                            }

                        }
                        else if (cscount == 1 || sscount == 1)
                        {

                            values.Add("ITEMID", this._focusProductID);
                            values.Add("ITEMVERSION", this._focusProductVersion);

                            DataTable productspec = SqlExecuter.Query("SelectProductSpec", "10001", values);



                            DataTable bomtable = new DataTable();
                            bomtable.Columns.Add("PRODUCTDEFID", typeof(string));
                            bomtable.Columns.Add("PRODUCTDEFVERSION", typeof(string));
                            bomtable.Columns.Add("OPERATIONID", typeof(string));
                            bomtable.Columns.Add("ENTERPRISEID", typeof(string));
                            bomtable.Columns.Add("MATERIALDEFID", typeof(string));
                            bomtable.Columns.Add("MATERIALDEFVERSION", typeof(string));
                            bomtable.Columns.Add("MULTIPLE", typeof(double));
                            bomtable.Columns.Add("VALIDSTATE", typeof(string));
                            bomtable.Columns.Add("PROCESSSEGMENTID", typeof(string));
                            bomtable.Columns.Add("PROCESSSEGMENTVERSION", typeof(string));
                            bomtable.Columns.Add("PLANTID", typeof(string));
                            bomtable.Columns.Add("WORKAREA", typeof(double));
                            bomtable.Columns.Add("SCRAPAREA", typeof(double));
                            bomtable.Columns.Add("MINVALUE", typeof(double));
                            bomtable.Columns.Add("PCSPNL", typeof(double));
                            bomtable.Columns.Add("_STATE_", typeof(string));
                            GetNumber number = new GetNumber();






                            values.Add("PRODUCTDEFID", this._focusProductID);
                            values.Add("PROCESSDEFVERSION",this._focusProductVersion);
                            DataTable materialtb = SqlExecuter.Query("GetRoutingBomList", "10001", values);



                            DataRow row = bomtable.NewRow();
                            row["_STATE_"] = "added";
                            foreach (DataRow dr in materialtb.Rows)
                            {
                                if (Format.GetString(dr["COMPONENTITEMID"]).Equals("X360330001"))
                                {
                                    bomtable.Columns.Add("SEQUENCE", typeof(double));
                                    row["SEQUENCE"] = dr["SEQUENCE"];
                                    row["_STATE_"] = "modified";
                                    break;
                                }

                            }
                            row["PRODUCTDEFID"] = this._focusProductID;
                            row["PRODUCTDEFVERSION"] = this._focusProductVersion;
                            row["OPERATIONID"] = operationId;
                            row["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                            row["MATERIALDEFID"] = "X360330001";
                            row["MATERIALDEFVERSION"] = "*";
                            row["MULTIPLE"] = 1;
                            row["PLANTID"] = UserInfo.Current.Plant;
                            row["VALIDSTATE"] = "Valid";
                            row["PROCESSSEGMENTID"] = process["PROCESSSEGMENTID"]; 
                            row["PROCESSSEGMENTVERSION"] = "*";
                            row["WORKAREA"] = Convert.ToDouble(Format.GetString(dt2.Rows[0]["WORKAREA"]));
                            row["SCRAPAREA"] = Convert.ToDouble(Format.GetString(dt2.Rows[0]["SCRAPAREA"]));
                            row["MINVALUE"] = Convert.ToDouble(Format.GetString(dt2.Rows[0]["MINVALUE"]));
                            row["PCSPNL"] = Convert.ToDouble(Format.GetString(productspec.Rows[0]["PCSPNL"]));
                            bomtable.Rows.Add(row);




                            MessageWorker worker = new MessageWorker("SaveProductOutsourcingSpec");
                            worker.SetBody(new MessageBody()
                        {
                             { "bom", bomtable}
                        });



                            var result = worker.Execute<DataTable>();
                            var resultData = result.GetResultSet();

                            if (Format.GetString(row["_STATE_"]).Equals("modified"))
                            {
                                this.ShowMessage("BOMPgcMaterialChange", "\r\n", "X360330001" + "\r\n"
                                     , "P.G.C (포타슘 골드 시아나이드)" + "\r\n", resultData.Rows[0]["QTY"].ToString());

                            }
                            else
                            {
                                this.ShowMessage("BOMPgcMaterialAdd", "\r\n", "X360330001" + "\r\n"
                                , "P.G.C (포타슘 골드 시아나이드)" + "\r\n", resultData.Rows[0]["QTY"].ToString());

                            }

                        }

                        break;

                }
            }
        }
        #endregion

        #region 검색
            /// <summary>^
            /// 비동기 override 모델
            /// </summary>
        protected async override Task OnSearchAsync()
		{
            await base.OnSearchAsync();

			//변수 초기화
			_isDelete = false;

			var values = Conditions.GetValues();
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable dtRouting = SqlExecuter.Query("GetAllRoutingOperationList", "10001", values);
            this.grdOperation.DataSource = dtRouting;

            if (!string.IsNullOrEmpty(this._focusProcessSegmentID))
                SearchSpecList();
        }

        private void SearchSpecList()
        {
            // 공정
            Dictionary<string, object> param = new Dictionary<string, object>();

            param.Add("P_PRODUCTDEFID", this._focusProductID);
            param.Add("P_PRODUCTDEFVERSION", this._focusProductVersion);
            param.Add("P_PROCESSSEGMENTID", this._focusProcessSegmentID);
            param.Add("P_SPECTYPE", this._strSpecType);
            param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            //DataTable dtOutsourcingSpec = SqlExecuter.Query("SelectProductOutsourcingSpec", "10001", param);
            DataTable dtOutsourcingSpec = SqlExecuter.Query("SelectProductOutsourcingSpec2", "10002", param);
            if (dtOutsourcingSpec.Columns.Contains("_STATE_"))
             {
                dtOutsourcingSpec.Columns.Remove("_STATE_");
            }
            
     /*
            Dictionary<string, object> param2 = new Dictionary<string, object>();
            param2.Add("P_PRODUCTDEFID", this._focusProductID);
            param2.Add("P_PRODUCTDEFVERSION", this._focusProductVersion);
            param2.Add("P_PROCESSSEGMENTID", this._focusProcessSegmentID);

            DataTable dtOutsourcingSpec2 = SqlExecuter.Query("SelectProductOutsourcingSpec", "10002", param);
            if (dtOutsourcingSpec.Rows.Count == 0 && dtOutsourcingSpec2.Rows.Count>0)
            {
                grdSpecList.DataSource = dtOutsourcingSpec2;


            }
            else
            {
            */
                grdSpecList.DataSource = dtOutsourcingSpec;

            



             


        }

        #endregion

        #region 유효성 검사
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

