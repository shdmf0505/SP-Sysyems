#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons;
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

#endregion

namespace Micube.SmartMES.ProductManagement
{
	/// <summary>
	/// 프 로 그 램 명  : 생산관리 > 생산계획 > Tack Time 등록
	/// 업  무  설  명  : 품목별 공정에 대한 Tack Time을 등록
	/// 생    성    자  : 정승원
	/// 생    성    일  : 2019-09-17
	/// 수  정  이  력  : 2019-11-06 표준 LEAD TIME 추가
	/// 
	/// 
	/// </summary>
	public partial class ProcessTimePerPlan : SmartConditionManualBaseForm
	{
		#region Local Variables

		// TODO : 화면에서 사용할 내부 변수 추가

		#endregion

		#region 생성자

		public ProcessTimePerPlan()
		{
			InitializeComponent();
		}

		#endregion

		#region 컨텐츠 영역 초기화

		/// <summary>
		/// 화면의 컨텐츠 영역을 초기화한다.
		/// </summary>
		protected override void InitializeContent()
		{
			base.InitializeContent();

			InitializeEvent();

			// TODO : 컨트롤 초기화 로직 구성
			InitializeGrid();
			InitializeComboBox();
		}

		/// <summary>        
		/// 코드그룹 리스트 그리드를 초기화한다.
		/// </summary>
		private void InitializeGrid()
		{
			grdTackTime.GridButtonItem = GridButtonItem.Delete | GridButtonItem.Export | GridButtonItem.Import;
			grdTackTime.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

			var groupDefaultCol = grdTackTime.View.AddGroupColumn("");
			//품목코드
			groupDefaultCol.AddTextBoxColumn("PRODUCTDEFID", 100).SetIsReadOnly();
			//품목버전
			groupDefaultCol.AddTextBoxColumn("PRODUCTDEFVERSION", 50).SetIsReadOnly().SetLabel("ITEMVERSION");
			//품목명
			groupDefaultCol.AddTextBoxColumn("PRODUCTDEFNAME", 180).SetIsReadOnly();
			//공정순번
			groupDefaultCol.AddTextBoxColumn("USERSEQUENCE", 60).SetIsReadOnly();
			//공정코드
			groupDefaultCol.AddTextBoxColumn("PROCESSSEGMENTID", 80).SetIsReadOnly();
			//공정버전
			groupDefaultCol.AddTextBoxColumn("PROCESSSEGMENTVERSION", 50).SetIsReadOnly().SetIsHidden();
			//공정명
			groupDefaultCol.AddTextBoxColumn("PROCESSSEGMENTNAME", 150).SetIsReadOnly();

			//Tack Time(초/PNL)
			var groupTackTimeCol = grdTackTime.View.AddGroupColumn("TACKTIMEINFO");
			//이론값
			groupTackTimeCol.AddSpinEditColumn("THEORYTACKTIME", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true);
			//실적값(3개월평균)
			groupTackTimeCol.AddTextBoxColumn("RESULT3TACKTIME", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Right);
            //실적값(6개월평균)
            groupTackTimeCol.AddTextBoxColumn("RESULT6TACKTIME", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Right);
            
            //목표값
            groupTackTimeCol.AddSpinEditColumn("TARGETTACKTIME", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true);
            //적용값
            groupTackTimeCol.AddSpinEditColumn("APPLICATIONTACKTIME", 80).SetDisplayFormat("###,###0.##",MaskTypes.Numeric,true);
			//표준 LEAD TIME
			groupTackTimeCol.AddSpinEditColumn("STDLEADTIME", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true);

			var groupDefaultCol2 = grdTackTime.View.AddGroupColumn("");
			//유효상태
			groupDefaultCol2.AddComboBoxColumn("VALIDSTATE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetDefault("Valid").SetValidationIsRequired().SetTextAlignment(TextAlignment.Center);
			//생성자
			groupDefaultCol2.AddTextBoxColumn("CREATOR", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
			//생성시간
			groupDefaultCol2.AddTextBoxColumn("CREATEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
			//수정자
			groupDefaultCol2.AddTextBoxColumn("MODIFIER", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
			//수정시간
			groupDefaultCol2.AddTextBoxColumn("MODIFIEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly().SetTextAlignment(TextAlignment.Center);

			grdTackTime.View.PopulateColumns();

            grdApply.GridButtonItem = GridButtonItem.Export | GridButtonItem.Import;

            grdApply.View.AddTextBoxColumn("PRODUCTDEFID", 100).SetIsReadOnly();
            //품목버전
            grdApply.View.AddTextBoxColumn("PRODUCTDEFVERSION", 50).SetIsReadOnly().SetLabel("ITEMVERSION");
            //품목명
            grdApply.View.AddTextBoxColumn("PRODUCTDEFNAME", 180).SetIsReadOnly();
            //적용타입
            grdApply.View.AddComboBoxColumn("APPLICATIONTACKTIME", 80, new SqlQuery("GetTypeList", "10001", "CODECLASSID=ApplicationTackTimeType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            grdApply.View.PopulateColumns();

        }

		/// <summary>
		/// 적용값 Combo 초기화
		/// </summary>
		private void InitializeComboBox()
		{

		}

		#endregion

		#region Event

		/// <summary>
		/// 화면에서 사용되는 이벤트를 초기화한다.
		/// </summary>
		public void InitializeEvent()
		{
			// TODO : 화면에서 사용할 이벤트 추가
		}


		#endregion

		#region 툴바

		/// <summary>
		/// 저장 버튼을 클릭하면 호출한다.
		/// </summary>
		protected override void OnToolbarSaveClick()
		{
			base.OnToolbarSaveClick();

            // TODO : 저장 Rule 변경
            DataTable changed = null;
            MessageWorker worker = null;
            /*
            MessageWorker worker = new MessageWorker("SaveCalInputQty");
            worker.SetBody(new MessageBody()
            {
                { "EnterpriseID", UserInfo.Current.Enterprise },
                { "PlantID", UserInfo.Current.Plant },
                { "UserId", UserInfo.Current.Id },
                { "Productlist", dt },
            });

            worker.Execute();
            */
            String isApplyType = "N";
            if (tabTactTime.SelectedTabPage.Name == "tabpageTactTime")
            {

                changed = grdTackTime.GetChangedRows();
                worker = new MessageWorker("SaveTackTime");
            }
            else
            {
                changed = grdApply.GetChangedRows();
                worker = new MessageWorker("SaveTackTime");
                isApplyType = "Y";

            }
			worker.SetBody(new MessageBody()
			{
				{ "enterpriseId", UserInfo.Current.Enterprise },
				{ "plantId", UserInfo.Current.Plant },
                { "isapplyType", isApplyType },
                { "tackTimeList", changed }
			});

			worker.Execute();
		}

		#endregion

		#region 검색

		/// <summary>
		/// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
		/// </summary>
		protected async override Task OnSearchAsync()
		{
			await base.OnSearchAsync();

			// TODO : 조회 SP 변경
			var values = Conditions.GetValues();
			values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable dt = null;


            if (tabTactTime.SelectedTabPage.Name == "tabpageTactTime" )
            {
                dt = await QueryAsync("SelectTackTimeList", "10001", values);
                grdTackTime.DataSource = dt;
            }
            else
            {
                dt = await QueryAsync("SelectTactimeApplySTD", "10001", values);
                grdApply.DataSource = dt;
            }
            
			if (dt == null  || dt.Rows.Count < 1)
			{
				// 조회할 데이터가 없습니다.
				ShowMessage("NoSelectData");
            }

			
		}

		/// <summary>
		/// 조회조건 항목을 추가한다.
		/// </summary>
		protected override void InitializeCondition()
		{
			base.InitializeCondition();

			// TODO : 조회조건 추가 구성이 필요한 경우 사용

			//품목코드
			CommonFunction.AddConditionProductPopup("P_PRODUCTDEFID", 0.1, true, Conditions);
			//공정
			CommonFunction.AddConditionProcessSegmentPopup("P_PROCESSSEGMENTID", 1.1, true, Conditions);

            Conditions.GetCondition<ConditionItemSelectPopup>("P_PRODUCTDEFID").SetPopupApplySelection((selectedRows, dataGridRows) =>
            {
                string productDefName = Format.GetString(selectedRows.FirstOrDefault()["PRODUCTDEFNAME"]);

                List<DataRow> list = selectedRows.ToList<DataRow>();

                List<string> listRev = new List<string>();

                string productlist = string.Empty;
                selectedRows.ForEach(row =>
                {
                    string productid = Format.GetString(row["PRODUCTDEFID"]);
                    string revision = Format.GetString(row["PRODUCTDEFVERSION"]);
                    productlist = productlist + productid + ',';
                    listRev.Add(revision);
                }
                );

                productlist = productlist.TrimEnd(',');

                listRev = listRev.Distinct().ToList();
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                param.Add("P_PLANTID", UserInfo.Current.Plant);
                param.Add("P_PRODUCTDEFID", productlist);

                DataTable dt = SqlExecuter.Query("selectProductdefVesion", "10001", param);


                Conditions.GetControl<SmartComboBox>("P_PRODUCTDEFVERSION").DataSource = dt;
                if (listRev.Count > 0)
                {
                    if (listRev.Count == 1)
                        Conditions.GetControl<SmartComboBox>("P_PRODUCTDEFVERSION").EditValue = Format.GetFullTrimString(listRev[0]);
                    else
                        Conditions.GetControl<SmartComboBox>("P_PRODUCTDEFVERSION").EditValue = Format.GetFullTrimString('*');
                }

               // Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").Text = productDefName;
            });

        }

		/// <summary>
		/// 조회조건의 컨트롤을 추가한다.
		/// </summary>
		protected override void InitializeConditionControls()
		{
			base.InitializeConditionControls();
            Conditions.GetControl<SmartComboBox>("P_PRODUCTDEFTYPE").EditValue = "Product";
            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
        }

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
        /// </summary>
        protected override void OnValidateContent()
		{
			base.OnValidateContent();

			// TODO : 유효성 로직 변경
			grdTackTime.View.CheckValidation();

            DataTable changed = null;
            if (tabTactTime.SelectedTabPage.Name == "tabpageTactTime")
            {
                changed =  grdTackTime.GetChangedRows();
            }
            else
            {
                changed = grdApply.GetChangedRows();
            }
                
			if (changed.Rows.Count == 0)
			{
				//저장할 데이터가 존재하지 않습니다.
				throw MessageException.Create("NoSaveData");
			}

		}

		#endregion

		#region Private Function

		#endregion
	}
}
