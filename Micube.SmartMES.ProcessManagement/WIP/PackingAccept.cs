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

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 포장관리 > 인수등록
    /// 업  무  설  명  : 인수등록
    /// 생    성    자  : 박정훈
    /// 생    성    일  : 2019-09-24
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class PackingAccept : SmartConditionManualBaseForm
    {
        #region ◆ Local Variables |

        #endregion

        #region ◆ 생성자 |
        /// <summary>
        /// 생성자
        /// </summary>
        public PackingAccept()
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

			txtLotId.Text = string.Empty;
			txtLotId.ImeMode = ImeMode.Alpha;
		}

        #region ▶ 조회조건 설정 |
        /// <summary>
        /// 검색조건 설정
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            //작업장
            CommonFunction.AddConditionAreaByAuthorityPopup("P_AREAID", 0, false, Conditions, true, true);
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
            #region - 포장인수 Grid 설정 |
            grdPacking.GridButtonItem = GridButtonItem.Export;

            grdPacking.View.SetIsReadOnly();

            // CheckBox 설정
            this.grdPacking.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdPacking.View.AddTextBoxColumn("LOTID", 180).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdPacking.View.AddTextBoxColumn("PRODUCTDEFID", 150).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdPacking.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdPacking.View.AddTextBoxColumn("PRODUCTDEFNAME", 250).SetIsReadOnly();
            grdPacking.View.AddTextBoxColumn("PROCESSSEGMENTID", 150).SetIsHidden();
            grdPacking.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150).SetIsReadOnly();
            grdPacking.View.AddTextBoxColumn("USERSEQUENCE", 70).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdPacking.View.AddTextBoxColumn("UNIT", 60).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdPacking.View.AddTextBoxColumn("PANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}").SetIsReadOnly();
            grdPacking.View.AddTextBoxColumn("QTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}").SetIsReadOnly();
            grdPacking.View.AddTextBoxColumn("CUSTOMERNAME", 100).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdPacking.View.AddTextBoxColumn("INPUTDATE", 100).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdPacking.View.AddTextBoxColumn("PRODUCTIONORDERID", 120).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdPacking.View.AddTextBoxColumn("DUEDATE", 120).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdPacking.View.AddTextBoxColumn("PRODUCTIONTYPE", 80).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();

            grdPacking.View.PopulateColumns();

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
			this.txtLotId.Click += TxtLotId_Click;
			this.txtLotId.KeyDown += TxtLotId_KeyDown;
			this.grdPacking.View.RowStyle += View_RowStyle;
        }

        /// <summary>
        /// 체크된 row에 스타일 적용
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
		{
			if (e.RowHandle < 0) return;

			bool isChecked = grdPacking.View.IsRowChecked(e.RowHandle);

			if (isChecked)
			{
				e.Appearance.BackColor = Color.FromArgb(254, 254, 16);
				e.HighPriority = true;
			}
		}

		/// <summary>
		/// LOT ID 클릭 시 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TxtLotId_Click(object sender, EventArgs e)
		{
			txtLotId.SelectAll();	
		}

		/// <summary>
		/// LOT ID KeyDown 시 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TxtLotId_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Enter)
			{
				if(string.IsNullOrWhiteSpace(txtLotId.Text)) return;

				DataTable dt = grdPacking.DataSource as DataTable;
                DataRow row = null;
                int count = 0;
                for(int i=0;i<dt.Rows.Count;i++)
                {
                    if(txtLotId.Text.Equals(dt.Rows[i]["LOTID"]))
                    {
                        row = dt.Rows[i];
                        count = i; ;
   
                    }

                }

				if(row ==null) return;

                ValidateIsSameProductAndVersion(row["PRODUCTDEFID"].ToString(), row["PRODUCTDEFVERSION"].ToString());

                DataRow newRow = dt.NewRow();

				newRow.ItemArray = row.ItemArray;

                dt.Rows.Remove(row);
                grdPacking.View.FocusedRowHandle = 0;
                dt.Rows.InsertAt(newRow, grdPacking.View.FocusedRowHandle);

            


                grdPacking.View.CheckRow(grdPacking.View.FocusedRowHandle, true);

                dt.AcceptChanges();
                
				
				txtLotId.SelectAll();
				txtLotId.Focus();

                txtLotId.EditValue = string.Empty;

            }
		}

        private void ValidateIsSameProductAndVersion(string productDefId, string productDefVersion)
        {
            DataTable dt = grdPacking.View.GetCheckedRows();
            foreach(DataRow each in dt.Rows)
            {
                if(each["PRODUCTDEFID"].ToString() != productDefId
                    || each["PRODUCTDEFVERSION"].ToString() != productDefVersion)
                {
                    txtLotId.EditValue = string.Empty;
                    throw MessageException.Create("SameProductDefinition", each["PRODUCTDEFID"].ToString() + each["PRODUCTDEFVERSION"].ToString());
                }
            }
        }
		#endregion

		#region ◆ 툴바 |

		#region ▶ ToolBar 저장버튼 클릭 |
		/// <summary>
		/// 저장버튼 클릭
		/// </summary>
		protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            // TODO : 저장 Rule 변경
            DataTable packingList = grdPacking.View.GetCheckedRows();

            if (packingList == null || packingList.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }

            ValidateIsSameProductAndVersionForSave();

            MessageWorker worker = new MessageWorker("SaveBoxPackingAccept");
            worker.SetBody(new MessageBody()
            {
                { "EnterpriseID", UserInfo.Current.Enterprise },
                { "PlantID", UserInfo.Current.Plant },
                { "UserId", UserInfo.Current.Id },
                { "Lotlist", packingList }
            });

            worker.Execute();
        }

        private void ValidateIsSameProductAndVersionForSave()
        {
            DataTable dt = grdPacking.View.GetCheckedRows();
            if(dt.Rows.Count == 0)
            {
                return;
            }
            DataRow standard = dt.Rows[0];
            foreach (DataRow each in dt.Rows)
            {
                if (each["PRODUCTDEFID"].ToString() != standard["PRODUCTDEFID"].ToString()
                    || each["PRODUCTDEFVERSION"].ToString() != standard["PRODUCTDEFVERSION"].ToString())
                {
                    throw MessageException.Create("SameProductDefinition", string.Format("LotId={0}", each["LOTID"].ToString()));
                }
            }
        }
        #endregion

        #endregion

        #region ◆ 검색 |

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

			// 기존 Grid Data 초기화
			txtLotId.Text = string.Empty;
			grdPacking.View.ClearDatas();

            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable dt = await SqlExecuter.QueryAsync("SelectPackingAcceptList", "10001", values);

            if (dt.Rows.Count < 1)
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdPacking.DataSource = dt;
        }

        #endregion

        #region ◆ 유효성 검사 |

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            grdPacking.View.CheckValidation();

            if(grdPacking.View.GetCheckedRows().Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }

        #endregion

        #region ◆ Private Function |

        // TODO : 화면에서 사용할 내부 함수 추가

        #region ▶ Control Data 초기화 |
        /// <summary>
        /// Control Data 초기화
        /// </summary>
        private void SetInitControl()
        {
            // Data 초기화
            grdPacking.View.ClearDatas();
        }
        #endregion

        #endregion
    }
}
