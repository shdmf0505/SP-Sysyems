#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;

using System;
using System.Data;
using System.Drawing;
using System.Collections.Generic;
#endregion

namespace Micube.SmartMES.StandardInfo.Popup
{
    /// <summary>
    /// 프 로 그 램 명  : PCS Image 조회 조건에 들어가는 팝업
    /// 업  무  설  명  : productdefid에 pcsimageId를 연계하기 위한 popup
    /// 생    성    자  : 전우성
    /// 생    성    일  : 2019-07-08
    /// 수  정  이  력  : 
    /// 
    /// </summary>
    public partial class GerverImageCustomerPopup : SmartPopupBaseForm
    {
        #region Local Variables
        public string sIMAGEID = "";
        #endregion

        #region 생성자
        /// <summary>
        ///  선택한 설비 list를 보내기 위한 Handler
        /// </summary>
        /// <param name="dt"></param>
        public delegate void evecnt_handler(DataRow row);
        public event evecnt_handler write_handler;

        public GerverImageCustomerPopup()
        {
            InitializeComponent();

            InitializeGrid();
            InitializeEvent();
            InitializeLanguageKey();

            this.AcceptButton = btnOK;
            picMain.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;

            SetData();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            grdMain.GridButtonItem = GridButtonItem.None;
            grdMain.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;

            grdMain.View.AddTextBoxColumn("IMAGEID", 100)
                   .SetLabel("ID")
                   .SetValidationKeyColumn()
                   .SetTextAlignment(TextAlignment.Left);

            grdMain.View.AddTextBoxColumn("ENTERPRISEID", 10)
                   .SetDefault(UserInfo.Current.Enterprise ?? "*")
                   .SetIsHidden();

            grdMain.View.AddTextBoxColumn("PLANTID", 10)
                   .SetDefault(UserInfo.Current.Plant)
                   .SetIsHidden();

            grdMain.View.AddTextBoxColumn("WIDTH", 80)
                   .SetIsReadOnly()
                   .SetIsHidden();

            grdMain.View.AddTextBoxColumn("HEIGHT", 80)
                   .SetIsReadOnly()
                   .SetIsHidden();

            grdMain.View.AddTextBoxColumn("IMAGE", 80)
                   .SetIsHidden();

            grdMain.View.AddTextBoxColumn("DESCRIPTION", 50)
                   .SetDefault("")
                   .SetIsHidden();

            grdMain.View.AddTextBoxColumn("VALIDSTATE", 50)
                   .SetDefault("Valid")
                   .SetIsHidden();

            grdMain.View.PopulateColumns();
            grdMain.View.BestFitColumns();
        }

        /// <summary>
        /// Language Key 초기화
        /// </summary>
        private void InitializeLanguageKey()
        {
            grdMain.LanguageKey = "IMAGELIST";
            btnOK.LanguageKey = "OK";
        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {

            // Grid 클릭 이벤트
            grdMain.View.FocusedRowChanged += (s, e) =>
            {
                try
                {
                    if (e.FocusedRowHandle < 0)
                    {
                        return;
                    }

                  // DialogManager.ShowWaitArea(this);
                    DataRow dr = (grdMain.DataSource as DataTable).Rows[e.FocusedRowHandle];

                    picMain.Image = null;

                    if (dr["IMAGE"] != null)
                    {
                        if(dr["IMAGE"].ToString() != "")
                        {
                            picMain.Image = (Bitmap)new ImageConverter().ConvertFrom(Convert.FromBase64String(dr["IMAGE"].ToString()));
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw MessageException.Create(ex.ToString());
                }
                finally
                {
                  //  DialogManager.CloseWaitArea(this);
                }
            };

            // Grid에 Data가 없으면 상세의 내용 삭제
            grdMain.ToolbarDeleteRow += (s, e) =>
            {
                if (grdMain.View.DataRowCount == 0)
                {
                    picMain.Image = null;
                }
            };

            btnOK.Click += (s, e) =>
            {
                DataRow row = grdMain.View.GetFocusedDataRow();
                write_handler(row);
             
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            };
        }

        #endregion

        #region Private Function

        private void SetData()
        {
            try
            {
               // DialogManager.ShowWaitArea(this);

                grdMain.DataSource = null;

                Dictionary<string, object> Param = new Dictionary<string, object>();
                Param.Add("P_ID", "");

                if (SqlExecuter.Query("GetPCSImage", "10001", Param) is DataTable dt)
                {
                    if (dt == null || dt.Rows.Count < 1)
                    {
                        ShowMessage("NoSelectData");
                        picMain.Image = null;
                        return;
                    }

                    grdMain.DataSource = dt;
                }

           


            }
            catch (Exception ex)
            {
                throw Framework.MessageException.Create(ex.ToString());
            }
            finally
            {
              //  DialogManager.CloseWaitArea(this);
            }
        }

        #endregion

        #region Public Function

        #endregion
    }
}