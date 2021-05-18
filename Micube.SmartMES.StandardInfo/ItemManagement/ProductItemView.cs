#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons.Controls;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

#endregion using

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프 로 그 램 명  : 기준정보 > 품목관리 > 품목조회
    /// 업  무  설  명  : 품목을 조회 한다.
    /// 생    성    자  : 장선미
    /// 생    성    일  : 2019-12-19
    /// 수  정  이  력  :
    ///     1. 2021.01.21 전우성 - 입력관련 버튼 삭제 및 코드 정리
    ///
    /// </summary>
    public partial class ProductItemView : SmartConditionManualBaseForm
    {
        #region 생성자

        public ProductItemView()
        {
            InitializeComponent();
        }

        #endregion 생성자

        #region 컨텐츠 영역 초기화

        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeGrid();
            InitializeGridMask();
        }

        /// <summary>
        /// COLUMN별 EditMask 초기화
        /// </summary>
        private void InitializeGridMask()
        {
            DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repository = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();

            //연신율
            for (int i = 1; i < 4; i++)
            {
                repository = grdProductItem.View.Columns["ELONGATION" + i.ToString()].ColumnEdit as DevExpress.XtraEditors.Repository.RepositoryItemTextEdit;
                repository.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                repository.Mask.EditMask = @"\d* %";
                repository.Mask.UseMaskAsDisplayFormat = true;

                repository = grdProductItem.View.Columns["PITCHBEFORE" + i.ToString()].ColumnEdit as DevExpress.XtraEditors.Repository.RepositoryItemTextEdit;
                repository.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                repository.Mask.EditMask = @"\d* ㎛";
                repository.Mask.UseMaskAsDisplayFormat = true;

                repository = grdProductItem.View.Columns["PITCHAFTER" + i.ToString()].ColumnEdit as DevExpress.XtraEditors.Repository.RepositoryItemTextEdit;
                repository.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                repository.Mask.EditMask = @"\d* ㎛";
                repository.Mask.UseMaskAsDisplayFormat = true;

                repository = grdProductItem.View.Columns["DATA" + i.ToString()].ColumnEdit as DevExpress.XtraEditors.Repository.RepositoryItemTextEdit;
                repository.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                repository.Mask.EditMask = @"\d* ㎛";
                repository.Mask.UseMaskAsDisplayFormat = true;
            }

            //for (int i = 1; i < 7; i++)
            //{
            //    repository = grdProductItem.View.Columns["PLATING" + i.ToString() + "_HOLE_LSL"].ColumnEdit as DevExpress.XtraEditors.Repository.RepositoryItemTextEdit;
            //    repository.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            //    repository.Mask.EditMask = @"\d* ㎛";
            //    repository.Mask.UseMaskAsDisplayFormat = true;

            //    repository = grdProductItem.View.Columns["PLATING" + i.ToString() + "_HOLE_SL"].ColumnEdit as DevExpress.XtraEditors.Repository.RepositoryItemTextEdit;
            //    repository.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            //    repository.Mask.EditMask = @"\d* ㎛";
            //    repository.Mask.UseMaskAsDisplayFormat = true;

            //    repository = grdProductItem.View.Columns["PLATING" + i.ToString() + "_HOLE_USL"].ColumnEdit as DevExpress.XtraEditors.Repository.RepositoryItemTextEdit;
            //    repository.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            //    repository.Mask.EditMask = @"\d* ㎛";
            //    repository.Mask.UseMaskAsDisplayFormat = true;

            //    repository = grdProductItem.View.Columns["PLATING" + i.ToString() + "_TOTAL_LSL"].ColumnEdit as DevExpress.XtraEditors.Repository.RepositoryItemTextEdit;
            //    repository.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            //    repository.Mask.EditMask = @"\d* ㎛";
            //    repository.Mask.UseMaskAsDisplayFormat = true;

            //    repository = grdProductItem.View.Columns["PLATING" + i.ToString() + "_TOTAL_SL"].ColumnEdit as DevExpress.XtraEditors.Repository.RepositoryItemTextEdit;
            //    repository.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            //    repository.Mask.EditMask = @"\d* ㎛";
            //    repository.Mask.UseMaskAsDisplayFormat = true;

            //    repository = grdProductItem.View.Columns["PLATING" + i.ToString() + "_TOTAL_USL"].ColumnEdit as DevExpress.XtraEditors.Repository.RepositoryItemTextEdit;
            //    repository.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            //    repository.Mask.EditMask = @"\d* ㎛";
            //    repository.Mask.UseMaskAsDisplayFormat = true;

            //    repository = grdProductItem.View.Columns["PLATING" + i.ToString() + "_DIMPLE_LSL"].ColumnEdit as DevExpress.XtraEditors.Repository.RepositoryItemTextEdit;
            //    repository.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            //    repository.Mask.EditMask = @"\d* ㎛";
            //    repository.Mask.UseMaskAsDisplayFormat = true;

            //    repository = grdProductItem.View.Columns["PLATING" + i.ToString() + "_DIMPLE_SL"].ColumnEdit as DevExpress.XtraEditors.Repository.RepositoryItemTextEdit;
            //    repository.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            //    repository.Mask.EditMask = @"\d* ㎛";
            //    repository.Mask.UseMaskAsDisplayFormat = true;

            //    repository = grdProductItem.View.Columns["PLATING" + i.ToString() + "_DIMPLE_USL"].ColumnEdit as DevExpress.XtraEditors.Repository.RepositoryItemTextEdit;
            //    repository.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            //    repository.Mask.EditMask = @"\d* ㎛";
            //    repository.Mask.UseMaskAsDisplayFormat = true;

            //    repository = grdProductItem.View.Columns["PLATING" + i.ToString() + "_OVERFILL_LSL"].ColumnEdit as DevExpress.XtraEditors.Repository.RepositoryItemTextEdit;
            //    repository.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            //    repository.Mask.EditMask = @"\d* ㎛";
            //    repository.Mask.UseMaskAsDisplayFormat = true;

            //    repository = grdProductItem.View.Columns["PLATING" + i.ToString() + "_OVERFILL_SL"].ColumnEdit as DevExpress.XtraEditors.Repository.RepositoryItemTextEdit;
            //    repository.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            //    repository.Mask.EditMask = @"\d* ㎛";
            //    repository.Mask.UseMaskAsDisplayFormat = true;

            //    repository = grdProductItem.View.Columns["PLATING" + i.ToString() + "_OVERFILL_USL"].ColumnEdit as DevExpress.XtraEditors.Repository.RepositoryItemTextEdit;
            //    repository.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            //    repository.Mask.EditMask = @"\d* ㎛";
            //    repository.Mask.UseMaskAsDisplayFormat = true;
            //}

            ////표면처리 사양
            //for (int i = 1; i < 4; i++)
            //{
            //    repository = grdProductItem.View.Columns["SURFACE" + i.ToString() + "_NIOSP_LSL"].ColumnEdit as DevExpress.XtraEditors.Repository.RepositoryItemTextEdit;
            //    repository.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            //    repository.Mask.EditMask = @"\d* ㎛";
            //    repository.Mask.UseMaskAsDisplayFormat = true;

            //    repository = grdProductItem.View.Columns["SURFACE" + i.ToString() + "_NIOSP_USL"].ColumnEdit as DevExpress.XtraEditors.Repository.RepositoryItemTextEdit;
            //    repository.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            //    repository.Mask.EditMask = @"\d* ㎛";
            //    repository.Mask.UseMaskAsDisplayFormat = true;

            //    repository = grdProductItem.View.Columns["SURFACE" + i.ToString() + "_AU_LSL"].ColumnEdit as DevExpress.XtraEditors.Repository.RepositoryItemTextEdit;
            //    repository.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            //    repository.Mask.EditMask = @"\d* ㎛";
            //    repository.Mask.UseMaskAsDisplayFormat = true;

            //    repository = grdProductItem.View.Columns["SURFACE" + i.ToString() + "_AU_USL"].ColumnEdit as DevExpress.XtraEditors.Repository.RepositoryItemTextEdit;
            //    repository.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            //    repository.Mask.EditMask = @"\d* ㎛";
            //    repository.Mask.UseMaskAsDisplayFormat = true;

            //    repository = grdProductItem.View.Columns["SURFACE" + i.ToString() + "_PDSN_LSL"].ColumnEdit as DevExpress.XtraEditors.Repository.RepositoryItemTextEdit;
            //    repository.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            //    repository.Mask.EditMask = @"\d* ㎛";
            //    repository.Mask.UseMaskAsDisplayFormat = true;

            //    repository = grdProductItem.View.Columns["SURFACE" + i.ToString() + "_PDSN_USL"].ColumnEdit as DevExpress.XtraEditors.Repository.RepositoryItemTextEdit;
            //    repository.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            //    repository.Mask.EditMask = @"\d* ㎛";
            //    repository.Mask.UseMaskAsDisplayFormat = true;

            //    repository = grdProductItem.View.Columns["SURFACE" + i.ToString() + "_PRODUCT_LSL"].ColumnEdit as DevExpress.XtraEditors.Repository.RepositoryItemTextEdit;
            //    repository.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            //    repository.Mask.EditMask = @"\d* sq/mm";
            //    repository.Mask.UseMaskAsDisplayFormat = true;

            //    repository = grdProductItem.View.Columns["SURFACE" + i.ToString() + "_PRODUCT_USL"].ColumnEdit as DevExpress.XtraEditors.Repository.RepositoryItemTextEdit;
            //    repository.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            //    repository.Mask.EditMask = @"\d* sq/mm";
            //    repository.Mask.UseMaskAsDisplayFormat = true;

            //    repository = grdProductItem.View.Columns["SURFACE" + i.ToString() + "_SCRAP_LSL"].ColumnEdit as DevExpress.XtraEditors.Repository.RepositoryItemTextEdit;
            //    repository.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            //    repository.Mask.EditMask = @"\d* sq/mm";
            //    repository.Mask.UseMaskAsDisplayFormat = true;

            //    repository = grdProductItem.View.Columns["SURFACE" + i.ToString() + "_SCRAP_USL"].ColumnEdit as DevExpress.XtraEditors.Repository.RepositoryItemTextEdit;
            //    repository.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            //    repository.Mask.EditMask = @"\d* sq/mm";
            //    repository.Mask.UseMaskAsDisplayFormat = true;
            //}

            //repository = grdProductItem.View.Columns["HOLE1_CS"].ColumnEdit as DevExpress.XtraEditors.Repository.RepositoryItemTextEdit;
            //repository.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            //repository.Mask.EditMask = @"\d* ㎛";
            //repository.Mask.UseMaskAsDisplayFormat = true;

            //repository = grdProductItem.View.Columns["HOLE1_SS"].ColumnEdit as DevExpress.XtraEditors.Repository.RepositoryItemTextEdit;
            //repository.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            //repository.Mask.EditMask = @"\d* ㎛";
            //repository.Mask.UseMaskAsDisplayFormat = true;

            //repository = grdProductItem.View.Columns["HOLE2_CS"].ColumnEdit as DevExpress.XtraEditors.Repository.RepositoryItemTextEdit;
            //repository.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            //repository.Mask.EditMask = @"\d* ㎛";
            //repository.Mask.UseMaskAsDisplayFormat = true;

            //repository = grdProductItem.View.Columns["HOLE2_SS"].ColumnEdit as DevExpress.XtraEditors.Repository.RepositoryItemTextEdit;
            //repository.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            //repository.Mask.EditMask = @"\d* ㎛";
            //repository.Mask.UseMaskAsDisplayFormat = true;
        }

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            grdProductItem.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdProductItem.GridButtonItem = GridButtonItem.Export;
            grdProductItem.View.SetIsReadOnly();

            var group1 = grdProductItem.View.AddGroupColumn("");

            //Site
            group1.AddTextBoxColumn("PLANTID", 50).SetTextAlignment(TextAlignment.Center);
            //품목코드
            group1.AddTextBoxColumn("ITEMID", 100).SetLabel("PRODUCTDEFID");
            //내부REV
            group1.AddTextBoxColumn("ITEMVERSION", 50).SetTextAlignment(TextAlignment.Center).SetLabel("PRODUCTDEFVERSION");
            //품목명
            group1.AddTextBoxColumn("ITEMNAME", 180);
            // 제품Type
            group1.AddComboBoxColumn("PRODUCTTYPE", 50, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetTextAlignment(TextAlignment.Center);
            //Layer
            group1.AddComboBoxColumn("LAYER", 50, new SqlQuery("GetCodeList", "00001", "CODECLASSID=Layer", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetTextAlignment(TextAlignment.Center);
            // 고객코드
            group1.AddTextBoxColumn("CUSTOMERCODENAME", 150);
            //고객REV
            group1.AddTextBoxColumn("CUSTOMERREV", 50).SetTextAlignment(TextAlignment.Center).SetLabel("COMPANYCLIENTREV");
            // 제품등급
            group1.AddComboBoxColumn("PRODUCTLEVEL", 50, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductLevel", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetTextAlignment(TextAlignment.Center);
            //생산구분
            group1.AddComboBoxColumn("PRODUCTIONTYPE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetTextAlignment(TextAlignment.Center);
            //--------------수정해야함
            // UOM
            group1.AddTextBoxColumn("UOM", 60).SetTextAlignment(TextAlignment.Center);
            // 주제조공장
            group1.AddTextBoxColumn("FACTORYID", 80).SetTextAlignment(TextAlignment.Center);
            //--------------수정해야함
            //사양담당
            group1.AddTextBoxColumn("SPECPERSON", 70).SetTextAlignment(TextAlignment.Center);
            //영업담당
            group1.AddTextBoxColumn("SALESMAN", 70).SetTextAlignment(TextAlignment.Center);
            // 품목계정
            group1.AddComboBoxColumn("ITEMACCOUNT", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ItemAccount", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetTextAlignment(TextAlignment.Center);
            // 품목종류
            group1.AddComboBoxColumn("ITEMKIND", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ItemClass2", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetTextAlignment(TextAlignment.Center)
                .SetLabel("ITEMCLASS");
            // BASIC CODE
            group1.AddTextBoxColumn("BASICCODE", 100).SetTextAlignment(TextAlignment.Center);
            // 판매범주
            group1.AddTextBoxColumn("SALEORDERCATEGORY", 100);
            // QR 사업부정보
            group1.AddComboBoxColumn("QRDIVISION", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=QRBusinessInfo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetTextAlignment(TextAlignment.Center)
                .SetLabel("QRBUSINESSINFO");
            // QR 사업부 SUB
            group1.AddComboBoxColumn("QRSUBDIVISON", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=QRBusinessSub", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetTextAlignment(TextAlignment.Center)
                .SetLabel("QRBUSINESSSUB");
            // QR 생산구분
            group1.AddComboBoxColumn("QRPRODUCTIONTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=QRProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetTextAlignment(TextAlignment.Center);
            // QR 자재 REV
            group1.AddTextBoxColumn("QRMATERIALVERSION", 100).SetTextAlignment(TextAlignment.Center)
                .SetLabel("QRMATERIALREV");
            // APN 입력
            group1.AddTextBoxColumn("APN", 100).SetTextAlignment(TextAlignment.Center);
            //신규데이터 여부
            group1.AddComboBoxColumn("ACCEPTFLAG", 50, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetTextAlignment(TextAlignment.Center).SetLabel("RECEIVENEWDATA");
            // 투입유형
            group1.AddComboBoxColumn("INPUTTYPE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=InputType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetTextAlignment(TextAlignment.Center);
            //PCS SIZE
            group1.AddTextBoxColumn("PCSSIZE", 70);
            //PNL SIZE
            group1.AddTextBoxColumn("PNLSIZE", 70);
            //ARY SIZE
            group1.AddTextBoxColumn("ARYSIZE", 70).SetLabel("BLKSIZE");
            //PCSPNL
            group1.AddTextBoxColumn("PCSPNL", 60).SetTextAlignment(TextAlignment.Right);
            //PNLMM
            group1.AddTextBoxColumn("PNLMM", 60).SetTextAlignment(TextAlignment.Right);
            //PCSMM
            group1.AddTextBoxColumn("PCSMM", 60).SetTextAlignment(TextAlignment.Right);
            //PCS/ARY
            group1.AddTextBoxColumn("PCSARY", 60).SetTextAlignment(TextAlignment.Right).SetLabel("BLKPNL");
            //XOUT
            group1.AddTextBoxColumn("XOUT", 60).SetTextAlignment(TextAlignment.Right).SetLabel("X-OUT");
            //제품면적
            group1.AddTextBoxColumn("PRODUCTDIMENSIONS", 60).SetTextAlignment(TextAlignment.Center);
            //RTR/Sheet
            group1.AddComboBoxColumn("RTRSHT", 50, new SqlQuery("GetCodeList", "00001", "CODECLASSID=RTRSHT", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetTextAlignment(TextAlignment.Center);
            //주차관리
            group1.AddComboBoxColumn("ISWEEKMNG", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=WeekCount", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            //OXIDE (Y/N)
            group1.AddComboBoxColumn("OXIDE", 50, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetTextAlignment(TextAlignment.Center);
            //분리부 (Y/N)
            group1.AddComboBoxColumn("SEPARATINGPORTION", 50, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetTextAlignment(TextAlignment.Center);
            //ASSY (Y/N)
            group1.AddComboBoxColumn("ASSY", 50, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetTextAlignment(TextAlignment.Center);
            //ULMARK
            group1.AddComboBoxColumn("ULMARK", 50, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetTextAlignment(TextAlignment.Center);
            //무효화일자
            group1.AddTextBoxColumn("INVALIDDATE", 80).SetTextAlignment(TextAlignment.Center);
            //등록일자
            group1.AddTextBoxColumn("REGISTRATIONDATE", 80).SetTextAlignment(TextAlignment.Center);
            //CAM담당
            group1.AddTextBoxColumn("CAMMAN", 70).SetTextAlignment(TextAlignment.Center);
            //ENDUSER
            group1.AddComboBoxColumn("ENDUSER", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=EndUser", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetTextAlignment(TextAlignment.Center);
            //작업구분
            group1.AddComboBoxColumn("JOBTYPE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=JobType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetTextAlignment(TextAlignment.Center);
            //연신율 (Y/N)
            group1.AddComboBoxColumn("ELONGATION", 50, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetTextAlignment(TextAlignment.Center);
            //임피던스 (Y/N)
            group1.AddComboBoxColumn("IMPEDANCE", 50, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetTextAlignment(TextAlignment.Center);

            group1 = grdProductItem.View.AddGroupColumn("IMPEDANCE1");
            //임피던스1
            group1.AddTextBoxColumn("IMPEDANCESPEC1", 80).SetLabel("IMPEDANCESPEC2");
            group1.AddTextBoxColumn("IMPEDANCETYPE1", 80).SetLabel("IMPEDANCETYPE2");
            group1.AddTextBoxColumn("IMPEDANCEAPPLYLAYER1", 80).SetLabel("IMPEDANCELAYER");

            group1 = grdProductItem.View.AddGroupColumn("IMPEDANCE2");
            //임피던스2
            group1.AddTextBoxColumn("IMPEDANCESPEC2", 80).SetLabel("IMPEDANCESPEC2");
            group1.AddTextBoxColumn("IMPEDANCETYPE2", 80).SetLabel("IMPEDANCETYPE2");
            group1.AddTextBoxColumn("IMPEDANCEAPPLYLAYER2", 80).SetLabel("IMPEDANCELAYER");

            group1 = grdProductItem.View.AddGroupColumn("IMPEDANCE3");
            //임피던스 구분
            group1.AddTextBoxColumn("IMPEDANCESPEC3", 80).SetLabel("IMPEDANCESPEC2");
            group1.AddTextBoxColumn("IMPEDANCETYPE3", 80).SetLabel("IMPEDANCETYPE2");
            group1.AddTextBoxColumn("IMPEDANCEAPPLYLAYER3", 80).SetLabel("IMPEDANCELAYER");

            var group21 = grdProductItem.View.AddGroupColumn("ELONGATION-1");
            var group22 = grdProductItem.View.AddGroupColumn("ELONGATION-2");
            var group23 = grdProductItem.View.AddGroupColumn("ELONGATION-3");
            for (int i = 1; i < 4; i++)
            {
                Framework.SmartControls.Grid.Conditions.ConditionItemGroup group = null;
                switch (i)
                {
                    case 1:
                        group = group21;
                        break;

                    case 2:
                        group = group22;
                        break;

                    case 3:
                        group = group23;
                        break;
                }

                //연신율 유/무(Y/N)
                group.AddComboBoxColumn("ELONGATIONCHECK" + i.ToString(), 50, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetTextAlignment(TextAlignment.Center);
                //연신율
                group.AddTextBoxColumn("ELONGATION" + i.ToString(), 50).SetTextAlignment(TextAlignment.Right);
                //적용전 PITCH
                group.AddTextBoxColumn("PITCHBEFORE" + i.ToString(), 50).SetTextAlignment(TextAlignment.Right);
                //적용후 PITCH
                group.AddTextBoxColumn("PITCHAFTER" + i.ToString(), 50).SetTextAlignment(TextAlignment.Right);
                //중간 Data
                group.AddTextBoxColumn("DATA" + i.ToString(), 50).SetTextAlignment(TextAlignment.Right).SetLabel("SL");
                //Scale
                group.AddTextBoxColumn("SCALEVALUE" + i.ToString(), 50).SetTextAlignment(TextAlignment.Right).SetLabel("SCALE");
            }

            //var group24 = grdProductItem.View.AddGroupColumn("PLATING1_TYPE");
            //var group25 = grdProductItem.View.AddGroupColumn("PLATING2_TYPE");
            //var group26 = grdProductItem.View.AddGroupColumn("PLATING3_TYPE");
            //var group27 = grdProductItem.View.AddGroupColumn("PLATING4_TYPE");
            //var group28 = grdProductItem.View.AddGroupColumn("PLATING5_TYPE");
            //var group29 = grdProductItem.View.AddGroupColumn("PLATING6_TYPE");
            ////도금사양
            //for (int i = 1; i < 7; i++)
            //{
            //    Framework.SmartControls.Grid.Conditions.ConditionItemGroup group = null;
            //    switch (i)
            //    {
            //        case 1:
            //            group = group24;
            //            break;

            //        case 2:
            //            group = group25;
            //            break;

            //        case 3:
            //            group = group26;
            //            break;

            //        case 4:
            //            group = group27;
            //            break;

            //        case 5:
            //            group = group28;
            //            break;

            //        case 6:
            //            group = group29;
            //            break;
            //    }

            //    group.AddTextBoxColumn("PLATING" + i.ToString() + "_SEGMENT", 80).SetTextAlignment(TextAlignment.Center).SetLabel("PROCESSSEGMENTID");
            //    group.AddTextBoxColumn("PLATING" + i.ToString() + "_TYPE", 100).SetLabel("PROCESSSEGMENTNAME");
            //    group.AddTextBoxColumn("PLATING" + i.ToString() + "_SPEC1", 80).SetLabel("SPECINSPITEMID");
            //    group.AddTextBoxColumn("PLATING" + i.ToString() + "_THICK_LSL", 50).SetTextAlignment(TextAlignment.Right).SetLabel("PLATING_THICK_LSL");
            //    group.AddTextBoxColumn("PLATING" + i.ToString() + "_THICK_SL", 50).SetTextAlignment(TextAlignment.Right).SetLabel("PLATING_THICK_SL");
            //    group.AddTextBoxColumn("PLATING" + i.ToString() + "_THICK_USL", 50).SetTextAlignment(TextAlignment.Right).SetLabel("PLATING_THICK_USL");

            //    group.AddTextBoxColumn("PLATING" + i.ToString() + "_SPEC2", 80).SetLabel("SPECINSPITEMID");
            //    group.AddTextBoxColumn("PLATING" + i.ToString() + "_HOLE_LSL", 50).SetTextAlignment(TextAlignment.Right).SetLabel("PLATING_HOLE_LSL");
            //    group.AddTextBoxColumn("PLATING" + i.ToString() + "_HOLE_SL", 50).SetTextAlignment(TextAlignment.Right).SetLabel("PLATING_HOLE_SL");
            //    group.AddTextBoxColumn("PLATING" + i.ToString() + "_HOLE_USL", 50).SetTextAlignment(TextAlignment.Right).SetLabel("PLATING_HOLE_USL");

            //    group.AddTextBoxColumn("PLATING" + i.ToString() + "_SPEC3", 80).SetLabel("SPECINSPITEMID");
            //    group.AddTextBoxColumn("PLATING" + i.ToString() + "_TOTAL_LSL", 50).SetTextAlignment(TextAlignment.Right).SetLabel("PLATING_TOTAL_LSL");
            //    group.AddTextBoxColumn("PLATING" + i.ToString() + "_TOTAL_SL", 50).SetTextAlignment(TextAlignment.Right).SetLabel("PLATING_TOTAL_SL");
            //    group.AddTextBoxColumn("PLATING" + i.ToString() + "_TOTAL_USL", 50).SetTextAlignment(TextAlignment.Right).SetLabel("PLATING_TOTAL_USL");

            //    group.AddTextBoxColumn("PLATING" + i.ToString() + "_SPEC4", 80).SetLabel("SPECINSPITEMID");
            //    group.AddTextBoxColumn("PLATING" + i.ToString() + "_DIMPLE_LSL", 50).SetTextAlignment(TextAlignment.Right).SetLabel("PLATING_DIMPLE_LSL");
            //    group.AddTextBoxColumn("PLATING" + i.ToString() + "_DIMPLE_SL", 50).SetTextAlignment(TextAlignment.Right).SetLabel("PLATING_DIMPLE_SL");
            //    group.AddTextBoxColumn("PLATING" + i.ToString() + "_DIMPLE_USL", 50).SetTextAlignment(TextAlignment.Right).SetLabel("PLATING_DIMPLE_USL");

            //    group.AddTextBoxColumn("PLATING" + i.ToString() + "_SPEC5", 80).SetLabel("SPECINSPITEMID");
            //    group.AddTextBoxColumn("PLATING" + i.ToString() + "_OVERFILL_LSL", 50).SetTextAlignment(TextAlignment.Right).SetLabel("PLATING_OVERFILL_LSL");
            //    group.AddTextBoxColumn("PLATING" + i.ToString() + "_OVERFILL_SL", 50).SetTextAlignment(TextAlignment.Right).SetLabel("PLATING_OVERFILL_SL");
            //    group.AddTextBoxColumn("PLATING" + i.ToString() + "_OVERFILL_USL", 50).SetTextAlignment(TextAlignment.Right).SetLabel("PLATING_OVERFILL_USL");
            //}

            //var group30 = grdProductItem.View.AddGroupColumn("SURFACE1_TYPE");
            //var group31 = grdProductItem.View.AddGroupColumn("SURFACE2_TYPE");
            //var group32 = grdProductItem.View.AddGroupColumn("SURFACE3_TYPE");
            ////표면처리 사양
            //for (int i = 1; i < 4; i++)
            //{
            //    Framework.SmartControls.Grid.Conditions.ConditionItemGroup group = null;
            //    switch (i)
            //    {
            //        case 1:
            //            group = group30;
            //            break;

            //        case 2:
            //            group = group31;
            //            break;

            //        case 3:
            //            group = group32;
            //            break;
            //    }

            //    group.AddTextBoxColumn("SURFACE" + i.ToString() + "_SEGMENT", 80).SetTextAlignment(TextAlignment.Center).SetLabel("PROCESSSEGMENTID");
            //    group.AddTextBoxColumn("SURFACE" + i.ToString() + "_TYPE", 100).SetLabel("PROCESSSEGMENTNAME");
            //    group.AddTextBoxColumn("SURFACE" + i.ToString() + "_NIOSP_LSL", 50).SetTextAlignment(TextAlignment.Right);
            //    group.AddTextBoxColumn("SURFACE" + i.ToString() + "_NIOSP_USL", 50).SetTextAlignment(TextAlignment.Right);
            //    group.AddTextBoxColumn("SURFACE" + i.ToString() + "_AU_LSL", 50).SetTextAlignment(TextAlignment.Right);
            //    group.AddTextBoxColumn("SURFACE" + i.ToString() + "_AU_USL", 50).SetTextAlignment(TextAlignment.Right);
            //    group.AddTextBoxColumn("SURFACE" + i.ToString() + "_PDSN_LSL", 50).SetTextAlignment(TextAlignment.Right);
            //    group.AddTextBoxColumn("SURFACE" + i.ToString() + "_PDSN_USL", 50).SetTextAlignment(TextAlignment.Right);
            //    group.AddTextBoxColumn("SURFACE" + i.ToString() + "_PRODUCT_LSL", 50).SetTextAlignment(TextAlignment.Right);
            //    group.AddTextBoxColumn("SURFACE" + i.ToString() + "_PRODUCT_USL", 50).SetTextAlignment(TextAlignment.Right);
            //    group.AddTextBoxColumn("SURFACE" + i.ToString() + "_SCRAP_LSL", 50).SetTextAlignment(TextAlignment.Right);
            //    group.AddTextBoxColumn("SURFACE" + i.ToString() + "_SCRAP_USL", 50).SetTextAlignment(TextAlignment.Right);
            //}

            //var group33 = grdProductItem.View.AddGroupColumn("HOLEPLATINGAREA1");
            //group33.AddTextBoxColumn("HOLE1_CS", 50).SetTextAlignment(TextAlignment.Right);
            //group33.AddTextBoxColumn("HOLE1_SS", 50).SetTextAlignment(TextAlignment.Right);
            //var group34 = grdProductItem.View.AddGroupColumn("HOLEPLATINGAREA2");
            //group34.AddTextBoxColumn("HOLE2_CS", 50).SetTextAlignment(TextAlignment.Right);
            //group34.AddTextBoxColumn("HOLE2_SS", 50).SetTextAlignment(TextAlignment.Right);

            grdProductItem.View.PopulateColumns();
        }

        #endregion 컨텐츠 영역 초기화

        #region Event

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
                Conditions.SetValue("P_ITEMID", 0, parameters["ITEMID"]);
                Conditions.SetValue("P_PRODUCTDEFVERSION", 0, parameters["ITEMVERSION"]);
                Conditions.SetValue("P_PRODUCTDEFNAME", 0, parameters["ITEMNAME"]);
                Search();
            }
        }

        #endregion Event

        #region 검색

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            var values = Conditions.GetValues();
            values.Add("p_enterpriseid", UserInfo.Current.Enterprise);
            values.Add("p_languagetype", UserInfo.Current.LanguageType);

            // 표면도금TYPE, 동도금TYPE 값이 달라도 같이 조회되도록  PARAM 수정
            values.TryGetValue("P_SURFACEPLATINGTYPE", out object surfacePlatingType);
            values.TryGetValue("P_COPPERPLATINGTYPE", out object copperPlatingType);

            List<string> lstPlatingType = new List<string>();

            if (!string.IsNullOrEmpty(Format.GetString(surfacePlatingType, string.Empty).Replace("*", "")))
            {
                lstPlatingType.Add(string.Format("'{0}'", Format.GetString(surfacePlatingType, string.Empty)));
            }

            if (!string.IsNullOrEmpty(Format.GetString(copperPlatingType, string.Empty).Replace("*", "")))
            {
                lstPlatingType.Add(string.Format("'{0}'", Format.GetString(copperPlatingType, string.Empty)));
            }

            if (lstPlatingType.Count > 0)
            {
                values.Add("P_PLATINGTYPE", string.Join(",", lstPlatingType));
            }

            grdProductItem.DataSource = await SqlExecuter.QueryAsync("SelectProductItemView", "10002", values);
        }

        /// <summary>
        /// 조회 조건 초기화
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            #region 품목 코드

            var condition = Conditions.AddSelectPopup("P_ITEMID", new SqlQuery("GetItemMasterList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "ITEMID", "ITEMID")
                                      .SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)
                                      .SetPopupLayoutForm(800, 800)
                                      .SetPopupAutoFillColumns("ITEMNAME")
                                      .SetLabel("ITEMID")
                                      .SetPosition(3)
                                      .SetPopupResultCount(1);

            // 팝업에서 사용되는 검색조건 (품목코드/명)
            condition.Conditions.AddTextBox("TXTITEM");

            // 품목코드
            condition.GridColumns.AddTextBoxColumn("ITEMID", 150);
            // 품목명
            condition.GridColumns.AddTextBoxColumn("ITEMNAME", 200);
            // 품목버전
            condition.GridColumns.AddTextBoxColumn("ITEMVERSION", 100);

            #endregion 품목 코드



            //고객Rev
            Conditions.AddTextBox("P_CUSTOMERREV").SetPosition(4).SetLabel("CUSTOMERREV");
            //내부Rev
            Conditions.AddTextBox("P_PRODUCTDEFVERSION").SetPosition(5).SetLabel("INNERREVISION");
            //품목명
            Conditions.AddTextBox("P_PRODUCTDEFNAME").SetPosition(6).SetLabel("PRODUCTDEFNAME");

            #region 사양담당

            condition = Conditions.AddSelectPopup("P_SPECOWNER", new SqlQuery("GetUserList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "USERNAME", "USERID")
                                  .SetPopupLayout("SELECTSPECOWNER", PopupButtonStyles.Ok_Cancel, true, true)
                                  .SetPopupLayoutForm(800, 800)
                                  .SetPopupAutoFillColumns("DEPARTMENT")
                                  .SetLabel("SPECPERSON")
                                  .SetPosition(7)
                                  .SetPopupResultCount(0);

            // 팝업 조회조건
            condition.Conditions.AddTextBox("USERIDNAME");

            // 팝업 그리드
            condition.GridColumns.AddTextBoxColumn("USERID", 150);
            condition.GridColumns.AddTextBoxColumn("USERNAME", 200);
            condition.GridColumns.AddTextBoxColumn("DEPARTMENT", 200);

            #endregion 사양담당

            //표면도금 Type
            //Conditions.AddComboBox("P_SURFACEPLATINGTYPE", new SqlQuery("GetCodeList", "00001", "CODECLASSID=PlatingType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
            //    .SetPosition(8)
            //    .SetLabel("SURFACEPLATINGTYPE")
            //    .SetEmptyItem(Language.Get("ALLVIEWS"), "*", true)
            //    .SetIsHidden(); 
            Conditions.AddTextBox("P_SURFACEPLATINGTYPE").SetLabel("SURFACEPLATINGTYPE").SetDefault("*").SetIsHidden(); //2021.01.21 전우성 이용희 과장 요청

            //동도금 Type
            //Conditions.AddComboBox("P_COPPERPLATINGTYPE", new SqlQuery("GetCodeList", "00001", "CODECLASSID=CopperPlatingType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
            //    .SetPosition(9)
            //    .SetLabel("COPPERPLATINGTYPE")
            //    .SetEmptyItem(Language.Get("ALLVIEWS"), "*", true);
            Conditions.AddTextBox("P_COPPERPLATINGTYPE").SetLabel("COPPERPLATINGTYPE").SetDefault("*").SetIsHidden(); //2021.01.21 전우성 이용희 과장 요청

            //작업구분
            Conditions.AddComboBox("P_JOBTYPE", new SqlQuery("GetCodeList", "00001", "CODECLASSID=JobType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetPosition(10)
                .SetLabel("JOBTYPE")
                .SetEmptyItem(Language.Get("ALLVIEWS"), "*", true);

            //생산구분
            Conditions.AddComboBox("P_PRODUCTIONTYPE", new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetPosition(11)
                .SetLabel("PRODUCTIONTYPE")
                .SetEmptyItem(Language.Get("ALLVIEWS"), "*", true);

            //제품등급
            Conditions.AddComboBox("P_PRODUCTLEVEL", new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductLevel", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetPosition(12)
                .SetLabel("PRODUCTLEVEL")
                .SetEmptyItem(Language.Get("ALLVIEWS"), "*", true);

            //고객코드/고객명
            Conditions.AddTextBox("P_CUSTOMER")
                .SetPosition(13)
                .SetLabel("CUSTOMERCODENAME");

            //투입유형
            Conditions.AddComboBox("P_INPUTTYPE", new SqlQuery("GetCodeList", "00001", "CODECLASSID=InputType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetPosition(14)
                .SetLabel("INPUTTYPE")
                .SetEmptyItem(Language.Get("ALLVIEWS"), "*", true);

            //제품Type
            Conditions.AddComboBox("P_PRODUCTTYPE", new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetPosition(15)
                .SetLabel("PRODUCTTYPE")
                .SetEmptyItem(Language.Get("ALLVIEWS"), "*", true);

            //Layer
            Conditions.AddComboBox("P_LAYER", new SqlQuery("GetCodeList", "00001", "CODECLASSID=Layer", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetPosition(16)
                .SetLabel("LAYER")
                .SetEmptyItem(Language.Get("ALLVIEWS"), "*", true);

            //RTR/Sheet
            Conditions.AddComboBox("P_RTRSHT", new SqlQuery("GetCodeList", "00001", "CODECLASSID=RTRSHT", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetPosition(17)
                .SetLabel("RTRSHT")
                .SetEmptyItem(Language.Get("ALLVIEWS"), "*", true);

            //연신율
            Conditions.AddComboBox("P_ELONGATION", new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetPosition(18)
                .SetLabel("ELONGATION")
                .SetEmptyItem(Language.Get("ALLVIEWS"), "*", true);

            //임피던스
            Conditions.AddComboBox("P_IMPEDANCE", new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetPosition(19)
                .SetLabel("IMPEDANCE")
                .SetEmptyItem(Language.Get("ALLVIEWS"), "*", true);

            //OXIDE
            Conditions.AddComboBox("P_OXIDE", new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetPosition(21)
                .SetLabel("OXIDE")
                .SetEmptyItem(Language.Get("ALLVIEWS"), "*", true);

            //Assy
            Conditions.AddComboBox("P_ASSY", new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetPosition(21)
                .SetLabel("ASSY")
                .SetEmptyItem(Language.Get("ALLVIEWS"), "*", true);

            //분리부
            Conditions.AddComboBox("P_SEPARATINGPORTION", new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetPosition(22)
                .SetLabel("SEPARATINGPORTION")
                .SetEmptyItem(Language.Get("ALLVIEWS"), "*", true);
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            //3개월 설정
            SmartPeriodEdit period = Conditions.GetControl<SmartPeriodEdit>("P_PERIOD");
            DateTime toDate = Convert.ToDateTime(period.datePeriodTo.EditValue);
            period.datePeriodFr.EditValue = toDate.AddMonths(-3);
        }

        #endregion 검색

        #region private Function

        /// <summary>
        /// '조회' 실행
        /// </summary>
        private new async void Search()
        {
            await OnSearchAsync();
        }

        #endregion private Function
    }
}