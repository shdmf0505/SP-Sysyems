#region using
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons;
using Micube.SmartMES.Commons.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
#endregion

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 공정관리 > 투입관리 > 반품 LOT 현황
    /// 업  무  설  명  : 반품 LOT 현황
    /// 생    성    자  : 박정훈
    /// 생    성    일  : 2019-11-30
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class LotReturnList : SmartConditionManualBaseForm
    {
        #region ◆ Local Variables |

        #endregion

        #region ◆ 생성자 |
        /// <summary>
        /// 생성자
        /// </summary>
        public LotReturnList()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Form Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Form_Load(object sender, EventArgs e)
        {
            InitializeComboBox();
            InitializeGrid();
        }
        #endregion

        #region ◆ 컨텐츠 영역 초기화 |
        /// <summary>
        /// Control 설정
        /// </summary>
        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeEvent();
        }

        #region ▶ 조회조건 설정 |
        /// <summary>
        /// 검색조건 설정
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // 품목
            CommonFunction.AddConditionProductPopup("P_PRODUCTDEFID", 0.5, true, Conditions);
        }

        /// <summary>
        /// 조회조건 설정
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();
        } 
        #endregion

        #region ▶ ComboBox 초기화 |
        /// <summary>
        /// 콤보박스를 초기화 하는 함수
        /// </summary>
        private void InitializeComboBox()
        {
        }
        #endregion

        #region ▶ Grid 설정 |
        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            #region - 반품 LOT List Grid 설정 |
            grdReturnLot.GridButtonItem = GridButtonItem.Export;

            grdReturnLot.View.SetIsReadOnly();

            // CheckBox 설정

            grdReturnLot.View.AddTextBoxColumn("RETURNCHITNO", 150).SetTextAlignment(TextAlignment.Center); 
            grdReturnLot.View.AddTextBoxColumn("PRODUCTDEFID", 150).SetTextAlignment(TextAlignment.Center);
            grdReturnLot.View.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            grdReturnLot.View.AddTextBoxColumn("DESCRIPTION", 400).SetLabel("REASON");
            grdReturnLot.View.AddTextBoxColumn("STATENAME", 80).SetLabel("STATE").SetTextAlignment(TextAlignment.Center);
            grdReturnLot.View.AddTextBoxColumn("SPEC", 150);
            grdReturnLot.View.AddTextBoxColumn("LAYER", 80).SetTextAlignment(TextAlignment.Center);
            grdReturnLot.View.AddTextBoxColumn("CUSTOMERID", 120).SetTextAlignment(TextAlignment.Center);
            grdReturnLot.View.AddTextBoxColumn("CUSTOMERNAME", 150);
            grdReturnLot.View.AddTextBoxColumn("QTY", 120).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            grdReturnLot.View.AddTextBoxColumn("DEFECTQTY", 120).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            grdReturnLot.View.AddTextBoxColumn("AMOUNTS", 120).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            grdReturnLot.View.AddTextBoxColumn("LOTID", 170).SetTextAlignment(TextAlignment.Center);
            grdReturnLot.View.AddTextBoxColumn("INPUTDATE", 120).SetTextAlignment(TextAlignment.Center);
            grdReturnLot.View.AddTextBoxColumn("CSMANAGER", 120).SetTextAlignment(TextAlignment.Center);
            grdReturnLot.View.AddTextBoxColumn("REQUESTUSER", 120).SetTextAlignment(TextAlignment.Center);

            grdReturnLot.View.PopulateColumns();
            #endregion

            #region - 재공현황 Grid |
            grdLotList.GridButtonItem = GridButtonItem.None;

            grdLotList.View.SetIsReadOnly();

            grdLotList.View.AddTextBoxColumn("PROCESSSEQUENCE", 70).SetTextAlignment(TextAlignment.Center);
            grdLotList.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
            grdLotList.View.AddTextBoxColumn("AREAID", 70).SetTextAlignment(TextAlignment.Center);
            grdLotList.View.AddTextBoxColumn("AREANAME", 120).SetTextAlignment(TextAlignment.Center);
            grdLotList.View.AddTextBoxColumn("LOTID", 170).SetTextAlignment(TextAlignment.Center);
            grdLotList.View.AddTextBoxColumn("RECEIVEDATE", 170).SetTextAlignment(TextAlignment.Center);
            grdLotList.View.AddTextBoxColumn("RECEIVEPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            grdLotList.View.AddTextBoxColumn("RECEIVEPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            grdLotList.View.AddTextBoxColumn("LOTSENDDATE", 170).SetTextAlignment(TextAlignment.Center);
            grdLotList.View.AddTextBoxColumn("SENDPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            grdLotList.View.AddTextBoxColumn("SENDPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            grdLotList.View.AddTextBoxColumn("INSPECTIONDATE", 120).SetTextAlignment(TextAlignment.Center).SetLabel("INSPECTDATE");
            grdLotList.View.AddTextBoxColumn("DEFECTQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");

            grdLotList.View.PopulateColumns();
            #endregion
        }
        #endregion

        #endregion

        #region ◆ Event |

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            this.Load += Form_Load;

            // Grid Event
            grdReturnLot.View.DoubleClick += View_DoubleClick;
        }

        #region ▶ ComboBox Event |

        #endregion

        #region ▶ Grid Event |

        #region - 재공 Grid 더블클릭 |
        /// <summary>
        /// 재공 Grid 더블클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_DoubleClick(object sender, EventArgs e)
        {
            if (grdReturnLot.View.FocusedRowHandle < 0)
                return;

            //var strLotId = grdReturnLot.View.GetFocusedRow();
            DataRow row = grdReturnLot.View.GetFocusedDataRow();

            string strLotId = Format.GetString(row["LOTID"]);

            if (strLotId == null || string.IsNullOrWhiteSpace(strLotId))
                return;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("LOTID", strLotId.ToString());

            DataTable dt = SqlExecuter.Query("SelectReturnLotWIPStatus", "10001", param);

            if (dt.Rows.Count < 1)
            {
                return;
            }

            grdLotList.View.ClearDatas();

            grdLotList.DataSource = dt;
        }
        #endregion

        #endregion
        #endregion

        #region ◆ 툴바 |

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();
        }

        #endregion

        #region ◆ 검색 |

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            // 기존 Grid Data 초기화
            this.grdReturnLot.DataSource = null;
            this.grdLotList.DataSource = null;

            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable dt = await SqlExecuter.QueryAsync("SelectReturnLotList", "10001", values);

            if (dt.Rows.Count < 1)
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdReturnLot.DataSource = dt;
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

        #region ◆ Private Function |

        // TODO : 화면에서 사용할 내부 함수 추가

        #region ▶ Control Data 초기화 |
        /// <summary>
        /// Control Data 초기화
        /// </summary>
        private void SetClearControl()
        {
        }
        #endregion

        #endregion
    }
}
