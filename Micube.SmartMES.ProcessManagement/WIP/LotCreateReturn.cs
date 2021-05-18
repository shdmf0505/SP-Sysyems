#region using
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons;

using System;
using Micube.SmartMES.Commons.Controls;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Linq;
#endregion

// TODO : ct_returnlot 테이블 스키마 변경되었음

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 공정관리 > 투입관리 > 반품 LOT 생성
    /// 업  무  설  명  : 반품 LOT 생성 및 투입
    /// 생    성    자  : 황유성
    /// 생    성    일  : 2019-09-25
    /// 수  정  이  력  : 2019-11-30, 박정훈, 반품사유 및 CS담당자 추가 / Form Layout 수정
    /// 
    /// 
    /// </summary>
    public partial class LotCreateReturn : SmartConditionManualBaseForm
    {
        #region ◆ Local Variables |
        private string productDefIdToCreate = null;             // 생성 대상 LOT의 품목 ID
        private string productDefVersionToCreate = null;        // 생성 대상 LOT의 품목 버전
        #endregion

        #region ◆ 생성자 |
        /// <summary>
        /// 생성자
        /// </summary>
        public LotCreateReturn()
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

            UseAutoWaitArea = false;

            this.ucDataUpDownBtnCtrl.SourceGrid = this.grdReturnLots;
            this.ucDataUpDownBtnCtrl.TargetGrid = this.grdLotsToCreate;

            InitializeEvent();
            txtQty.ReadOnly = true;
        }

        #region ▶ 조회조건 설정 |
        /// <summary>
        /// 검색조건 설정
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();
            // 품목
            CommonFunction.AddConditionProductPopup("P_PRODUCTDEFID", 0.5, false, Conditions);
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
            #region - 라우팅 |
            // 라우팅
            cboRouting.Editor.DisplayMember = "PROCESSDEFNAME";
            cboRouting.Editor.ValueMember = "KEY";
            cboRouting.Editor.ShowHeader = false;
            cboRouting.Editor.UseEmptyItem = true;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            param.Add("PLANTID", UserInfo.Current.Plant);
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            cboRouting.Editor.DataSource = SqlExecuter.Query("GetReturnProcess", "10001", param);
            #endregion

            #region - 투입 자원 |
            //투입 자원
            cboArea.Editor.DisplayMember = "RESOURCENAME";
            cboArea.Editor.ValueMember = "RESOURCE";
            cboArea.Editor.ShowHeader = false;
            cboArea.Editor.UseEmptyItem = true;
            #endregion

            #region - 반품 사유 |

            cboReason.Editor.DisplayMember = "REASONCODENAME";
            cboReason.Editor.ValueMember = "REASONCODEID";
            cboReason.Editor.ShowHeader = false;
            cboReason.Editor.UseEmptyItem = true;

            Dictionary<string, object> param2 = new Dictionary<string, object>();
            param2.Add("P_REASONCODECLASSID", "ReturnReason");
            param2.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            cboReason.Editor.DataSource = SqlExecuter.Query("GetReasonCodeList", "10001", param2);
            #endregion
        }
        #endregion

        #region ▶ Grid 설정 |
        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            #region - 반품 LOT  그리드 |
            // 반품 LOT  그리드
            grdReturnLots.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdReturnLots.GridButtonItem = GridButtonItem.None;
            grdReturnLots.View.SetIsReadOnly();

            grdReturnLots.View.AddTextBoxColumn("RETURNCHITNO", 170).SetTextAlignment(TextAlignment.Center);
            grdReturnLots.View.AddTextBoxColumn("WAREHOUSENAME", 125);
            grdReturnLots.View.AddTextBoxColumn("PRODUCTDEFID", 120).SetTextAlignment(TextAlignment.Center);
            grdReturnLots.View.AddTextBoxColumn("PRODUCTDEFVERSION").SetIsHidden();
            grdReturnLots.View.AddTextBoxColumn("PRODUCTDEFNAME", 280);
            grdReturnLots.View.AddTextBoxColumn("RETURNLOTID", 200).SetTextAlignment(TextAlignment.Center);
            grdReturnLots.View.AddTextBoxColumn("QTY", 90).SetDisplayFormat("#,##0");
            grdReturnLots.View.AddTextBoxColumn("LOTID", 200).SetTextAlignment(TextAlignment.Center);

            grdReturnLots.View.PopulateColumns();
            #endregion

            #region - 생성 대상 LOT 그리드 |
            // 생성 대상 LOT 그리드
            grdLotsToCreate.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdLotsToCreate.GridButtonItem = GridButtonItem.None;
            grdLotsToCreate.View.SetIsReadOnly();

            grdLotsToCreate.View.AddTextBoxColumn("RETURNCHITNO", 170).SetTextAlignment(TextAlignment.Center);
            grdLotsToCreate.View.AddTextBoxColumn("WAREHOUSENAME", 125);
            grdLotsToCreate.View.AddTextBoxColumn("PRODUCTDEFID", 120).SetTextAlignment(TextAlignment.Center);
            grdLotsToCreate.View.AddTextBoxColumn("PRODUCTDEFVERSION").SetIsHidden();
            grdLotsToCreate.View.AddTextBoxColumn("PRODUCTDEFNAME", 280);
            grdLotsToCreate.View.AddTextBoxColumn("RETURNLOTID", 200).SetTextAlignment(TextAlignment.Center);
            grdLotsToCreate.View.AddTextBoxColumn("QTY", 90).SetDisplayFormat("#,##0");
            grdLotsToCreate.View.AddTextBoxColumn("LOTID");

            grdLotsToCreate.View.PopulateColumns();
            #endregion

            #region - 공정 그리드 |
            // 공정 그리드
            grdProcessPath.GridButtonItem = GridButtonItem.None;
            grdProcessPath.View.SetIsReadOnly();

            grdProcessPath.View.AddTextBoxColumn("USERSEQUENCE", 70).SetTextAlignment(TextAlignment.Center);
            grdProcessPath.View.AddTextBoxColumn("PROCESSSEGMENTID", 90).SetTextAlignment(TextAlignment.Center);
            grdProcessPath.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 160);
            grdProcessPath.View.AddTextBoxColumn("RESOURCENAME", 160);
            //grdProcessPath.View.AddTextBoxColumn("AREANAME", 160);

            grdProcessPath.View.PopulateColumns(); 
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
            this.Load += Form_Load;

            // ComboBox Event
            cboRouting.Editor.EditValueChanged += Editor_EditValueChanged;

            // DataUpDwownBtnCtrl Event
            ucDataUpDownBtnCtrl.buttonClick += UcDataUpDownBtnCtrl_buttonClick;
        }

        #region ▶ Up / Down Button Event |
        /// <summary>
        /// 반품 LOT 그리드 <-> 생성 대상 LOT 그리드 사이의 데이터 이동 버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UcDataUpDownBtnCtrl_buttonClick(object sender, EventArgs e)
        {
            RefreshTxtQty();
            ValidateIsAllSameProductAndSetProductDefIdToCreate();
            RefreshCboArea();
        }
        #endregion

        #region ▶ ComboBox Event |
        /// <summary>
        /// 라우팅 변경 시 작업장 콤보박스와 작업공정 그리드를 재조회 한다
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Editor_EditValueChanged(object sender, EventArgs e)
        {
            Dictionary<string, object> Param = new Dictionary<string, object>();
            Param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            Param.Add("PROCESSDEFID", cboRouting.Properties.GetDataSourceValue("PROCESSDEFID", cboRouting.Properties.GetDataSourceRowIndex("KEY", cboRouting.EditValue)));
            Param.Add("PROCESSDEFVERSION", cboRouting.Properties.GetDataSourceValue("PROCESSDEFVERSION", cboRouting.Properties.GetDataSourceRowIndex("KEY", cboRouting.EditValue)));
            Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            //DataTable dtProcessPath = SqlExecuter.Query("GetProcessPathList", "10001", Param);
            DataTable dtProcessPath = SqlExecuter.Query("GetProcessPathList", "10021", Param);
            grdProcessPath.DataSource = dtProcessPath;

            RefreshCboArea();
        } 
        #endregion

        #endregion

        #region ◆ 툴바 |

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            // 재공실사 진행 여부 체크
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            param.Add("PLANTID", UserInfo.Current.Plant);

            DataTable isWipSurveyResult = SqlExecuter.Query("GetPlantIsWipSurvey", "10001", param);

            if (isWipSurveyResult.Rows.Count > 0)
            {
                DataRow row = isWipSurveyResult.AsEnumerable().FirstOrDefault();

                string isWipSurvey = Format.GetString(row["ISWIPSURVEY"]);

                if (isWipSurvey == "Y")
                {
                    // 재공실사가 진행 중 입니다. {0}을 진행할 수 없습니다.
                    ShowMessage("PLANTINWIPSURVEY", Language.Get(string.Join("_", "MENU", MenuId)));

                    return;
                }
            }

            DataTable lotsToCreate = grdLotsToCreate.DataSource as DataTable;
            if(lotsToCreate == null || lotsToCreate.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }

            // 라우팅 선택 체크
            if (this.cboRouting.EditValue == null || string.IsNullOrWhiteSpace(this.cboRouting.EditValue.ToString()))
            {
                // 라우팅이 선택되어야합니다.
                throw MessageException.Create("NecessaryRouting");
            }

            // 작업장
            if (this.cboArea.EditValue == null || string.IsNullOrWhiteSpace(this.cboArea.EditValue.ToString()))
            {
                // 투입 작업장을 선택하여 주세요.
                throw MessageException.Create("NoInputArea");
            }

            // 반품사유
            if(this.cboReason.EditValue == null || string.IsNullOrWhiteSpace(cboReason.EditValue.ToString()))
            {
                // 투입 작업장을 선택하여 주세요.
                throw MessageException.Create("NoInputArea");
            }

            // CS 담당자
            if(txtCSManager.Text == null || string.IsNullOrWhiteSpace(txtCSManager.Text))
            {
                txtCSManager.Focus();
                // CS 담당자명을 입력하여 주십시오.
                throw MessageException.Create("NoCSManager"); 
            }

            DataTable processPath = grdProcessPath.DataSource as DataTable;

			string resourceId = cboArea.EditValue.ToString().Split('|')[0];
			string areaId = cboArea.EditValue.ToString().Split('|')[1];

            DataTable createData = null;

            try
            {
                pnlContent.ShowWaitArea();

                MessageWorker worker = new MessageWorker("SaveCreateReturnLot");
                worker.SetBody(new MessageBody()
                {
                    { "ENTERPRISEID", UserInfo.Current.Enterprise },
                    { "PLANTID", UserInfo.Current.Plant },
                    { "PROCESSDEFID", processPath.Rows[0]["PROCESSDEFID"].ToString() },
                    { "PROCESSDEFVERSION", processPath.Rows[0]["PROCESSDEFVERSION"].ToString() },
                    { "PROCESSPATHID", processPath.Rows[0]["PROCESSPATHID"].ToString() },
                    { "RESOURCEID", resourceId },
				    { "AREAID", areaId },
				    { "COMMENT", txtSpecialNote.Text },
                    { "RETURNREASON", cboReason.EditValue },
                    { "CSMANAGER", txtCSManager.Text },
                    { "LOTLIST", grdLotsToCreate.DataSource as DataTable }
                });

                var createResult = worker.Execute<DataTable>();
                createData = createResult.GetResultSet();
            }
            catch (Exception ex)
            {
                throw MessageException.Create(ex.Message);
            }
            finally
            {
                pnlContent.CloseWaitArea();
            }


            if (chkPrintLotCard.Checked)
            {
                string lotId = createData.Rows[0]["LOTID"].ToString();
                PrintLotCard(lotId);
            }


			//컨트롤 초기화
			ClearControls();

		}

        /// <summary>
        /// LOT 카드 출력
        /// </summary>
        /// <param name="lotId"></param>
        private void PrintLotCard(string lotId)
        {
            //pnlContent.ShowWaitArea();
            CommonFunction.PrintLotCard_Ver2(lotId, LotCardType.Return);
            //pnlContent.CloseWaitArea();
        }

        #endregion

        #region ◆ 검색 |

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

			//컨트롤 초기화
			txtSpecialNote.Text = string.Empty;
			txtQty.Text = string.Empty;
			grdProcessPath.View.ClearDatas();
			grdLotsToCreate.View.ClearDatas();

			var values = Conditions.GetValues();
            DataTable dt = await SqlExecuter.QueryAsync("SelectReturnLot", "10001", values);
            if (dt.Rows.Count < 1)
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }
            grdReturnLots.DataSource = dt;
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

        #region ◆ Function |

        #region ▶ ClearControls :: Control 초기화 |
        /// <summary>
        /// Control 초기화
        /// </summary>
        private void ClearControls()
        {
            txtQty.Text = string.Empty;
            cboArea.Editor.DataSource = null;
            txtSpecialNote.Text = string.Empty;
            grdProcessPath.View.ClearDatas();
            grdLotsToCreate.View.ClearDatas();
        }
        #endregion

        #region ▶ RefreshTxtQty :: 생성될 LOT의 수량을 다시 계산하여 보여준다 |
        /// <summary>
        /// 생성될 LOT의 수량을 다시 계산하여 보여준다
        /// </summary>
        private void RefreshTxtQty()
        {
            DataTable dataTable = grdLotsToCreate.DataSource as DataTable;
            decimal currentQty = 0;
            if (dataTable != null)
            {
                foreach (DataRow each in dataTable.Rows)
                {
                    currentQty += decimal.Parse(each["QTY"].ToString());
                }
            }

            if (this.ucDataUpDownBtnCtrl.ButtonState.Equals("Down"))
            {
                decimal addQty = 0;
                foreach (DataRow each in grdReturnLots.View.GetCheckedRows().Rows)
                {
                    addQty += decimal.Parse(each["QTY"].ToString());
                }
                txtQty.Text = (currentQty + addQty).ToString("N0");
            }
            else
            {
                decimal subtractQty = 0;
                foreach (DataRow each in grdLotsToCreate.View.GetCheckedRows().Rows)
                {
                    subtractQty += decimal.Parse(each["QTY"].ToString());
                }
                txtQty.Text = (currentQty - subtractQty).ToString("N0");
            }
        }
        #endregion

        #region ▶ ValidateIsAllSameProductAndSetProductDefIdToCreate :: 생성 대상 LOT 에 들어있는 LOT들이 모두 같은 품목코드인지 유효성 검사 |
        /// <summary>
        /// 생성 대상 LOT 에 들어있는 LOT들이 모두 같은 품목코드인지 유효성 검사
        /// </summary>
        private void ValidateIsAllSameProductAndSetProductDefIdToCreate()
        {
            DataTable returnLots = grdReturnLots.View.GetCheckedRows();
            DataTable lotsToCreate = grdLotsToCreate.DataSource as DataTable;

            productDefIdToCreate = null;
            productDefVersionToCreate = null;
            if (this.ucDataUpDownBtnCtrl.ButtonState.Equals("Down"))
            {
                string productDefId = null;
                string productDefVersion = null;

                if (lotsToCreate != null && lotsToCreate.Rows.Count > 0)
                {
                    productDefId = lotsToCreate.Rows[0]["PRODUCTDEFID"].ToString();
                    productDefVersion = lotsToCreate.Rows[0]["PRODUCTDEFVERSION"].ToString();
                }

                foreach (DataRow each in returnLots.Rows)
                {
                    if (productDefId == null)
                    {
                        productDefId = each["PRODUCTDEFID"].ToString();
                        productDefVersion = each["PRODUCTDEFVERSION"].ToString();
                    }
                    if (productDefId != each["PRODUCTDEFID"].ToString() || productDefVersion != each["PRODUCTDEFVERSION"].ToString())
                    {
                        // 같은 품목만 선택 가능합니다.
                        ShowMessage("SameProductDefinition", string.Format("ProductDefId = {0}, ProductDefVersion = {1}", productDefId, productDefVersion));
                        grdReturnLots.View.CheckedAll(false);
                        return;
                    }
                }
                productDefIdToCreate = productDefId;
                productDefVersionToCreate = productDefVersion;
            }
            else
            {
                if (lotsToCreate.Rows.Count > grdLotsToCreate.View.GetCheckedRows().Rows.Count)
                {
                    productDefIdToCreate = lotsToCreate.Rows[0]["PRODUCTDEFID"].ToString();
                    productDefVersionToCreate = lotsToCreate.Rows[0]["PRODUCTDEFVERSION"].ToString();
                }
            }
        }
        #endregion

        #region ▶  RefreshCboArea :: 작업장 콤보박스를 재조회 한다 |
        /// <summary>
        /// 작업장 콤보박스를 재조회 한다
        /// </summary>
        private void RefreshCboArea()
        {
            // 공정
            DataTable processPath = grdProcessPath.DataSource as DataTable;
            if (processPath == null || processPath.Rows.Count == 0)
            {
                cboArea.Editor.DataSource = null;
                return;
            }
            string processDefId = processPath.Rows[0]["PROCESSDEFID"].ToString();
            string processDefVersion = processPath.Rows[0]["PROCESSDEFVERSION"].ToString();
            string processSegmentId = processPath.Rows[0]["PROCESSSEGMENTID"].ToString();
            string processSegmentVersion = processPath.Rows[0]["PROCESSSEGMENTVERSION"].ToString();

            // 작업장 조회
            if (productDefIdToCreate != null)
            {
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("PRODUCTDEFID", productDefIdToCreate);
                param.Add("PRODUCTDEFVERSION", productDefVersionToCreate);
                param.Add("PROCESSDEFID", processDefId);
                param.Add("PROCESSDEFVERSION", processDefVersion);
                param.Add("PROCESSSEGMENTID", processSegmentId);
                param.Add("PROCESSSEGMENTVERSION", processSegmentVersion);
                param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                param.Add("PLANTID", UserInfo.Current.Plant);
                //cboArea.Editor.DataSource = SqlExecuter.Query("GetTransitAreaList", "10021", param);
                cboArea.Editor.DataSource = SqlExecuter.Query("GetTransitAreaList", "10022", param);
            }
            else
            {
                cboArea.Editor.DataSource = null;
            }
        } 
        #endregion

        #endregion
    }
}
