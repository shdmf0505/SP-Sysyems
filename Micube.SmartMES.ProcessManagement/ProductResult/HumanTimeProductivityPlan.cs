#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using System.Data;
using System.Threading.Tasks;
using Micube.SmartMES.Commons.Controls;

#endregion

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 공정관리 > 생산실적 > 인시생산성
    /// 업  무  설  명  : 
    /// 생    성    자  : 황유성
    /// 생    성    일  : 2019-11-11
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class HumanTimeProductivityPlan : SmartConditionManualBaseForm
	{
        #region ◆ Local Variables |
        private const int DAYS_IN_WEEK = 7;
        #endregion

        #region ◆ 생성자 |

        public HumanTimeProductivityPlan()
		{
			InitializeComponent();
		}

        #endregion

        #region ◆ 컨텐츠 영역 초기화 |

        /// <summary>
        /// 화면의 컨텐츠 영역을 초기화한다.
        /// </summary>
        protected override void InitializeContent()
		{
			base.InitializeContent();

			InitializeEvent();

			// TODO : 컨트롤 초기화 로직 구성
			InitializeGrid();
		}

        #region ▶ Grid 설정 |
        /// <summary>        
        /// 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            // 조회
            grdPlan.GridButtonItem = GridButtonItem.None;
            grdPlan.View.SetIsReadOnly();
            grdPlan.View.AddTextBoxColumn("PLANTID", 80).SetTextAlignment(TextAlignment.Center);
            grdPlan.View.AddTextBoxColumn("WORKFACTORY", 80).SetLabel("FACTORY").SetTextAlignment(TextAlignment.Center);
            grdPlan.View.AddTextBoxColumn("VENDORID", 90).SetTextAlignment(TextAlignment.Center);
            grdPlan.View.AddTextBoxColumn("VENDORNAME", 100);
            grdPlan.View.AddTextBoxColumn("PROCESSGROUPID", 90).SetTextAlignment(TextAlignment.Center);
            grdPlan.View.AddTextBoxColumn("PROCESSGROUPNAME", 160);
            grdPlan.View.AddTextBoxColumn("PERIOD", 80).SetTextAlignment(TextAlignment.Center);
            grdPlan.View.AddTextBoxColumn("YEAR", 60).SetTextAlignment(TextAlignment.Center);
            grdPlan.View.AddTextBoxColumn("MONTH", 60);
            grdPlan.View.AddSpinEditColumn("WORKRESULT", 80).SetLabel("WORKRESULTKM2");
            grdPlan.View.AddSpinEditColumn("WORKEFFORT", 80).SetLabel("WORKEFFORTKHR");
            grdPlan.View.AddSpinEditColumn("PERHUMANTIMEPRODUCTIVITY", 80).SetLabel("PERHUMANTIMEPRODUCTIVITYM2HR");
            grdPlan.View.AddTextBoxColumn("CREATOR", 90).SetTextAlignment(TextAlignment.Center);
            grdPlan.View.AddTextBoxColumn("CREATEDTIME", 140).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("yyyy-MM-dd HH:mm:ss");
            grdPlan.View.AddTextBoxColumn("MODIFIER", 90).SetTextAlignment(TextAlignment.Center);
            grdPlan.View.AddTextBoxColumn("MODIFIEDTIME", 140).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("yyyy-MM-dd HH:mm:ss");
            grdPlan.View.PopulateColumns();

            // 엑셀 업로드
            grdExcelUpLoad.GridButtonItem = GridButtonItem.Import | GridButtonItem.Export;
            grdExcelUpLoad.View.SetIsReadOnly();
            grdExcelUpLoad.View.AddTextBoxColumn("PLANTID", 80).SetTextAlignment(TextAlignment.Center);
            grdExcelUpLoad.View.AddTextBoxColumn("WORKFACTORY", 80).SetLabel("FACTORY").SetTextAlignment(TextAlignment.Center);
            grdExcelUpLoad.View.AddTextBoxColumn("VENDORID", 90).SetTextAlignment(TextAlignment.Center);
            grdExcelUpLoad.View.AddTextBoxColumn("VENDORNAME", 100);
            grdExcelUpLoad.View.AddTextBoxColumn("PROCESSGROUPID", 90).SetTextAlignment(TextAlignment.Center);
            grdExcelUpLoad.View.AddTextBoxColumn("PROCESSGROUPNAME", 160);
            grdExcelUpLoad.View.AddTextBoxColumn("PERIOD", 80).SetTextAlignment(TextAlignment.Center);
            grdExcelUpLoad.View.AddTextBoxColumn("YEAR", 60).SetTextAlignment(TextAlignment.Center);
            grdExcelUpLoad.View.AddTextBoxColumn("MONTH", 60).SetTextAlignment(TextAlignment.Center);
            grdExcelUpLoad.View.AddTextBoxColumn("WORKRESULT", 80).SetLabel("WORKRESULTKM2").SetDisplayFormat("", MaskTypes.Numeric);
            grdExcelUpLoad.View.AddTextBoxColumn("WORKEFFORT", 80).SetLabel("WORKEFFORTKHR").SetDisplayFormat("", MaskTypes.Numeric);
            grdExcelUpLoad.View.PopulateColumns();
        } 
        #endregion

        #endregion

        #region ◆ Event |

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
		{
            grdExcelUpLoad.HeaderButtonClickEvent += GrdExcelUpLoad_HeaderButtonClickEvent;
		}

        private void GrdExcelUpLoad_HeaderButtonClickEvent(object sender, HeaderButtonClickArgs args)
        {
            if (args.ClickItem == GridButtonItem.Import)
            {
                DataTable orgTable = grdExcelUpLoad.DataSource as DataTable;
                DataTable newTable = orgTable.Clone();
                newTable.Columns["VENDORID"].DataType = typeof(string);
                newTable.Columns["PROCESSGROUPID"].DataType = typeof(string);
                newTable.Columns["YEAR"].DataType = typeof(int);
                newTable.Columns["MONTH"].DataType = typeof(int);
                newTable.Columns["WORKRESULT"].DataType = typeof(decimal);
                newTable.Columns["WORKEFFORT"].DataType = typeof(decimal);

                foreach (DataRow row in orgTable.Rows)
                {
                    newTable.ImportRow(row);
                }

                grdExcelUpLoad.DataSource = newTable;
            }
        }

        #endregion

        #region ◆ 툴바 |

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        { 
            base.OnToolbarSaveClick();

            switch (tabPlan.SelectedTabPageIndex)
            {
                case 1:
                    DataTable dt = grdExcelUpLoad.DataSource as DataTable;

                    MessageWorker worker = new MessageWorker("SaveHumanTimeProductivityPlan");
                    worker.SetBody(new MessageBody()
                    {
                        { "enterpriseid", UserInfo.Current.Enterprise },
                        { "datalist", dt }
                    });

                    worker.Execute();

                    grdExcelUpLoad.View.ClearDatas();
                    break;
            }
        }
        #endregion

        #region ◆ 유효성 검사 |

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();
        }
    
        #endregion

        #region ◆ 검색 |

        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
		{
			await base.OnSearchAsync();

            var values = Conditions.GetValues();
            values["P_YEAR"] = values["P_YEAR"].ToString().Trim();
            values["P_MONTH"] = values["P_MONTH"].ToString().Trim();

            switch (tabPlan.SelectedTabPageIndex)
			{
				case 0://인시당 상세
                    // todo : 조회조건 추가
                    values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                    DataTable dtPlan = await SqlExecuter.QueryAsync("SelectLaborProductivityPlan", "10001", values);

					if (dtPlan.Rows.Count < 1)
					{
						// 조회할 데이터가 없습니다.
						ShowMessage("NoSelectData");
					}

					grdPlan.DataSource = dtPlan;
					break;
			}
		}

		/// <summary>
		/// 조회조건 항목을 추가한다.
		/// </summary>
		protected override void InitializeCondition()
		{
			base.InitializeCondition();
            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            Conditions.AddTextBox("P_YEAR").SetLabel("YEAR");
            Conditions.AddTextBox("P_MONTH").SetLabel("MONTH");
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
		{
			base.InitializeConditionControls();
            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
        }
        #endregion

        #region ◆ Private Function |

        #region ▶ SetInitControl :: Control 초기화 |
        /// <summary>
        /// Control 초기화
        /// </summary>
        private void SetInitControl()
        {
            grdPlan.View.ClearDatas();
            grdExcelUpLoad.View.ClearDatas();
        }
        #endregion

        #endregion
    }
}

