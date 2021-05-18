#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
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

namespace Micube.SmartMES.SystemManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 시스템 관리 > 접속이력 관리 > 시스템 로그
    /// 업  무  설  명  : 시스템 로그 테이블 조회
    /// 생    성    자  : 박정훈
    /// 생    성    일  : 2020-01-03
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class SystemLog : SmartConditionManualBaseForm
    {
        #region ◆ Local Variables |

        // TODO : 화면에서 사용할 내부 변수 추가

        #endregion

        #region ◆ 생성자 |

        public SystemLog()
        {
            InitializeComponent();
        }

        #endregion

        #region ◆ 컨텐츠 영역 초기화 |

        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeEvent();

            // TODO : 컨트롤 초기화 로직 구성
            InitializeGrid();
        }

        #region ▶ Grid 초기화 |
        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdLog.GridButtonItem = GridButtonItem.Export;
            grdLog.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;

            grdLog.View.SetIsReadOnly();

            grdLog.View.SetSortOrder("CREATEDATE", DevExpress.Data.ColumnSortOrder.Descending);
            grdLog.View.AddTextBoxColumn("REQUESTIPADDRESS", 100).SetTextAlignment(TextAlignment.Center);
            grdLog.View.AddTextBoxColumn("RESULTCOMPLETETIME", 150).SetLabel("OCCURDATE");
            grdLog.View.AddTextBoxColumn("REQUESTRULENAME", 120);
            grdLog.View.AddTextBoxColumn("REQUESTCLASSNAME", 200);
            grdLog.View.AddTextBoxColumn("REQUESTMESSAGESET", 250);
            grdLog.View.AddTextBoxColumn("QUERYID", 100).SetTextAlignment(TextAlignment.Center);
            grdLog.View.AddTextBoxColumn("QUERYVERSION", 100).SetTextAlignment(TextAlignment.Center);
            grdLog.View.AddTextBoxColumn("QUERYPARAMETER", 100);
            grdLog.View.AddTextBoxColumn("ELAPSEDTIME", 100).SetTextAlignment(TextAlignment.Right);
            grdLog.View.AddTextBoxColumn("RESULTMESSAGE", 250); 
            grdLog.View.AddTextBoxColumn("RETURNMESSAGESET", 100);
            grdLog.View.AddTextBoxColumn("CREATOR", 100).SetTextAlignment(TextAlignment.Center);

            grdLog.View.PopulateColumns();
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
            grdLog.View.RowCellClick += GrdLogView_RowCellClick;
        }

        /// <summary>
        /// Cell Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdLogView_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            DataRow dr = grdLog.View.GetFocusedDataRow();

            if (dr == null) return;

            txtRequestMessageSet.Text = dr["REQUESTMESSAGESET"].ToString();
            txtQueryID.Text = dr["QUERYID"].ToString();
            txtQueryVersion.Text = dr["QUERYVERSION"].ToString();
            txtQueryParameter.Text = dr["QUERYPARAMETER"].ToString();

            txtResultMessage.Text = dr["RESULTMESSAGE"].ToString();
        }

        #endregion

        #region ◆ 툴바 |

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            // TODO : 저장 Rule 변경
            //DataTable changed = grdCodeClass.GetChangedRows();

            //ExecuteRule("SaveCodeClass", changed);
        }

        #endregion

        #region ◆ 검색 |

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            // TODO : 조회 SP 변경
            var values = Conditions.GetValues();

            DataTable dtLog = await QueryAsync("SelectSystemLog", "10001", values);

            if (dtLog.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdLog.DataSource = dtLog;
        }

        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
        }

        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
        }

        #endregion

        #region ◆ 유효성 검사 |

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            // TODO : 유효성 로직 변경
            //grdCodeClass.View.CheckValidation();

            //DataTable changed = grdCodeClass.GetChangedRows();

            //if (changed.Rows.Count == 0)
            //{
            //    // 저장할 데이터가 존재하지 않습니다.
            //    throw MessageException.Create("NoSaveData");
            //}
        }

        #endregion

        #region ◆ Private Function |

        // TODO : 화면에서 사용할 내부 함수 추가

        #endregion
    }
}