#region using
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors.Repository;
using Micube.Framework.SmartControls.Grid.BandedGrid;
#endregion

namespace Micube.SmartMES.OutsideOrderMgnt.Popup
{
    /// <summary>
    /// 프 로 그 램 명   : Claim 마감 기간등록
    /// 업  무  설  명  : Claim 마감 기간등록
    /// 생    성    자  : 최별
    /// 생    성    일  : 2019-08-28
    /// 수  정  이  력  :    
    /// 
    /// 
    /// </summary>
    public partial class OutsourcingYoungPongPriceCodePopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        public DataRow CurrentDataRow { get; set; }
     
        string _IsProduct = "";
        string _ProcesssegmentClassid = "";
        DataRow _row = null;
        #region Local Variables

        /// <summary>
        ///  Handler
        /// </summary>
        /// <param name="dt"></param>
        public delegate void chat_room_evecnt_handler(DataRow row);
        //public event chat_room_evecnt_handler write_handler;
        #endregion

        #region 생성자
        public OutsourcingYoungPongPriceCodePopup()
        {
            InitializeComponent();
            InitializeComboBox();  // 콤보박스 셋팅 
            InitializeEvent();
            InitializeGrid();
            InitializeCondition();



        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SPlantid"></param>
        public OutsourcingYoungPongPriceCodePopup( string IsProduct ,DataRow row)
        {
            InitializeComponent();
            
            _IsProduct =IsProduct;
           
            _row = row;
            _ProcesssegmentClassid = _row["PROCESSSEGMENTCLASSID"].ToString();
            InitializeEvent();
            InitializeGrid();
            this.Load += Popup_Load;
            InitializeCondition();
            
            this.Shown += Popup_Shown;


        }
       
        private void Popup_Shown(Object sender, EventArgs e)
        {
            InitializeGrid_GrdTab1MasterDisplayCaption();
            
        }
        
        private void Popup_Load(object sender, EventArgs e)
        {
            grdTab1Master.View.ClearColumns();
            InitializeGrid_GrdTab1MasterDisplay(_IsProduct);
            grdTab1Master.View.PopulateColumns();
            InitializeGrid_Tab1RangeTextEdit();
            DataTable dtTab1Master = GetOspypricecombination();
            if (dtTab1Master.Rows.Count < 1)
            {
                //단가 그리드 clear
                DataTable dtPrice = (grdTab1Master.DataSource as DataTable).Clone();
                //grdDetail.View.ClearDatas();
                grdTab1Master.DataSource = dtPrice;

            }
            else
            {
                grdTab1Master.DataSource = dtTab1Master;
            }
        }
         public void OutsourcingYoungPongPriceCodePopupGrid()
        {
            InitializeGrid_GrdTab1MasterDisplayCaption();
        }
        private void InitializeGrid_GrdTab1MasterDisplayCaption()
        {
            DataRow row = _row;
            if (row != null)
            {
                #region PRICEITEMID01 

                if (!(row["PRICEITEMID01"].ToString().Equals("")))
                {
                    if (row["ISRANGE01"].ToString().Equals("Y"))  //구간여부 
                    {
                        grdTab1Master.View.Columns["ITEMVALUE01FR"].Caption = row["PRICEITEMNAME01"].ToString() + "Fr";
                        grdTab1Master.View.Columns["ITEMVALUE01FR"].ToolTip = row["PRICEITEMNAME01"].ToString() + "Fr";
                        grdTab1Master.View.Columns["ITEMVALUE01TO"].Caption = row["PRICEITEMNAME01"].ToString() + "To";
                        grdTab1Master.View.Columns["ITEMVALUE01TO"].ToolTip = row["PRICEITEMNAME01"].ToString() + "To";
                    }
                    else
                    {
                        if (row["COMPONENTTYPE01"].ToString().Equals("PopUp"))
                        {
                            grdTab1Master.View.Columns["ITEMVALUE01"].Caption = row["PRICEITEMNAME01"].ToString() + "Id";
                            grdTab1Master.View.Columns["ITEMVALUE01"].ToolTip = row["PRICEITEMNAME01"].ToString() + "Id";
                            grdTab1Master.View.Columns["ITEMVALUE01NAME"].Caption = row["PRICEITEMNAME01"].ToString();
                            grdTab1Master.View.Columns["ITEMVALUE01NAME"].ToolTip = row["PRICEITEMNAME01"].ToString();

                        }
                        else if (row["COMPONENTTYPE01"].ToString().Equals("ComboBox"))
                        {
                            grdTab1Master.View.Columns["ITEMVALUE01"].ToolTip = row["PRICEITEMNAME01"].ToString();
                            grdTab1Master.View.Columns["ITEMVALUE01"].Caption = row["PRICEITEMNAME01"].ToString();
                        }
                        else
                        {
                            grdTab1Master.View.Columns["ITEMVALUE01"].ToolTip = row["PRICEITEMNAME01"].ToString();
                            grdTab1Master.View.Columns["ITEMVALUE01"].Caption = row["PRICEITEMNAME01"].ToString();

                        }

                    }
                }

                #endregion
                #region PRICEITEMID02 

                if (!(row["PRICEITEMID02"].ToString().Equals("")))
                {
                    if (row["ISRANGE02"].ToString().Equals("Y"))  //구간여부 
                    {
                        grdTab1Master.View.Columns["ITEMVALUE02FR"].Caption = row["PRICEITEMNAME02"].ToString() + "Fr";
                        grdTab1Master.View.Columns["ITEMVALUE02FR"].ToolTip = row["PRICEITEMNAME02"].ToString() + "Fr";
                        grdTab1Master.View.Columns["ITEMVALUE02TO"].Caption = row["PRICEITEMNAME02"].ToString() + "To";
                        grdTab1Master.View.Columns["ITEMVALUE02TO"].ToolTip = row["PRICEITEMNAME02"].ToString() + "To";
                    }
                    else
                    {
                        if (row["COMPONENTTYPE02"].ToString().Equals("PopUp"))
                        {
                            //수정 예정 

                            grdTab1Master.View.Columns["ITEMVALUE02"].Caption = row["PRICEITEMNAME02"].ToString() + "Id";
                            grdTab1Master.View.Columns["ITEMVALUE02"].ToolTip = row["PRICEITEMNAME02"].ToString() + "Id";
                            grdTab1Master.View.Columns["ITEMVALUE02NAME"].Caption = row["PRICEITEMNAME02"].ToString();
                            grdTab1Master.View.Columns["ITEMVALUE02NAME"].ToolTip = row["PRICEITEMNAME02"].ToString();

                        }
                        else if (row["COMPONENTTYPE02"].ToString().Equals("ComboBox"))
                        {
                            grdTab1Master.View.Columns["ITEMVALUE02"].ToolTip = row["PRICEITEMNAME02"].ToString();
                            grdTab1Master.View.Columns["ITEMVALUE02"].Caption = row["PRICEITEMNAME02"].ToString();
                        }
                        else
                        {
                            grdTab1Master.View.Columns["ITEMVALUE02"].ToolTip = row["PRICEITEMNAME02"].ToString();
                            grdTab1Master.View.Columns["ITEMVALUE02"].Caption = row["PRICEITEMNAME02"].ToString();

                        }

                    }
                }

                #endregion
                #region PRICEITEMID03 

                if (!(row["PRICEITEMID03"].ToString().Equals("")))
                {
                    if (row["ISRANGE03"].ToString().Equals("Y"))  //구간여부 
                    {
                        grdTab1Master.View.Columns["ITEMVALUE03FR"].Caption = row["PRICEITEMNAME03"].ToString() + "Fr";
                        grdTab1Master.View.Columns["ITEMVALUE03FR"].ToolTip = row["PRICEITEMNAME03"].ToString() + "Fr";
                        grdTab1Master.View.Columns["ITEMVALUE03TO"].Caption = row["PRICEITEMNAME03"].ToString() + "To";
                        grdTab1Master.View.Columns["ITEMVALUE03TO"].ToolTip = row["PRICEITEMNAME03"].ToString() + "To";
                    }
                    else
                    {
                        if (row["COMPONENTTYPE03"].ToString().Equals("PopUp"))
                        {
                            grdTab1Master.View.Columns["ITEMVALUE03"].Caption = row["PRICEITEMNAME03"].ToString() + "Id";
                            grdTab1Master.View.Columns["ITEMVALUE03"].ToolTip = row["PRICEITEMNAME03"].ToString() + "Id";
                            grdTab1Master.View.Columns["ITEMVALUE03NAME"].Caption = row["PRICEITEMNAME03"].ToString();
                            grdTab1Master.View.Columns["ITEMVALUE03NAME"].ToolTip = row["PRICEITEMNAME03"].ToString();
                        }
                        else if (row["COMPONENTTYPE03"].ToString().Equals("ComboBox"))
                        {
                            grdTab1Master.View.Columns["ITEMVALUE03"].Caption = row["PRICEITEMNAME03"].ToString();
                            grdTab1Master.View.Columns["ITEMVALUE03"].ToolTip = row["PRICEITEMNAME03"].ToString();
                        }
                        else
                        {
                            grdTab1Master.View.Columns["ITEMVALUE03"].Caption = row["PRICEITEMNAME03"].ToString();
                            grdTab1Master.View.Columns["ITEMVALUE03"].ToolTip = row["PRICEITEMNAME03"].ToString();

                        }

                    }
                }

                #endregion

                #region PRICEITEMID04 

                if (!(row["PRICEITEMID04"].ToString().Equals("")))
                {
                    if (row["ISRANGE04"].ToString().Equals("Y"))  //구간여부 
                    {
                        grdTab1Master.View.Columns["ITEMVALUE04FR"].Caption = row["PRICEITEMNAME04"].ToString() + "Fr";
                        grdTab1Master.View.Columns["ITEMVALUE04FR"].ToolTip = row["PRICEITEMNAME04"].ToString() + "Fr";
                        grdTab1Master.View.Columns["ITEMVALUE04TO"].Caption = row["PRICEITEMNAME04"].ToString() + "To";
                        grdTab1Master.View.Columns["ITEMVALUE04TO"].ToolTip = row["PRICEITEMNAME04"].ToString() + "To";
                    }
                    else
                    {
                        if (row["COMPONENTTYPE04"].ToString().Equals("PopUp"))
                        {
                            grdTab1Master.View.Columns["ITEMVALUE04"].Caption = row["PRICEITEMNAME04"].ToString() + "Id";
                            grdTab1Master.View.Columns["ITEMVALUE04"].ToolTip = row["PRICEITEMNAME04"].ToString() + "Id";
                            grdTab1Master.View.Columns["ITEMVALUE04NAME"].Caption = row["PRICEITEMNAME04"].ToString();
                            grdTab1Master.View.Columns["ITEMVALUE04NAME"].ToolTip = row["PRICEITEMNAME04"].ToString();
                        }
                        else if (row["COMPONENTTYPE04"].ToString().Equals("ComboBox"))
                        {
                            grdTab1Master.View.Columns["ITEMVALUE04"].Caption = row["PRICEITEMNAME04"].ToString();
                            grdTab1Master.View.Columns["ITEMVALUE04"].ToolTip = row["PRICEITEMNAME04"].ToString();
                        }
                        else
                        {
                            grdTab1Master.View.Columns["ITEMVALUE04"].Caption = row["PRICEITEMNAME04"].ToString();
                            grdTab1Master.View.Columns["ITEMVALUE04"].ToolTip = row["PRICEITEMNAME04"].ToString();

                        }

                    }
                }

                #endregion
                #region PRICEITEMID05 

                if (!(row["PRICEITEMID05"].ToString().Equals("")))
                {
                    if (row["ISRANGE05"].ToString().Equals("Y"))  //구간여부 
                    {
                        grdTab1Master.View.Columns["ITEMVALUE05FR"].Caption = row["PRICEITEMNAME05"].ToString() + "Fr";
                        grdTab1Master.View.Columns["ITEMVALUE05FR"].ToolTip = row["PRICEITEMNAME05"].ToString() + "Fr";
                        grdTab1Master.View.Columns["ITEMVALUE05TO"].Caption = row["PRICEITEMNAME05"].ToString() + "To";
                        grdTab1Master.View.Columns["ITEMVALUE05TO"].ToolTip = row["PRICEITEMNAME05"].ToString() + "To";
                    }
                    else
                    {
                        if (row["COMPONENTTYPE05"].ToString().Equals("PopUp"))
                        {
                            grdTab1Master.View.Columns["ITEMVALUE05"].Caption = row["PRICEITEMNAME05"].ToString() + "Id";
                            grdTab1Master.View.Columns["ITEMVALUE05"].ToolTip = row["PRICEITEMNAME05"].ToString() + "Id";
                            grdTab1Master.View.Columns["ITEMVALUE05NAME"].Caption = row["PRICEITEMNAME05"].ToString();
                            grdTab1Master.View.Columns["ITEMVALUE05NAME"].ToolTip = row["PRICEITEMNAME05"].ToString();
                        }
                        else if (row["COMPONENTTYPE05"].ToString().Equals("ComboBox"))
                        {
                            grdTab1Master.View.Columns["ITEMVALUE05"].Caption = row["PRICEITEMNAME05"].ToString();
                            grdTab1Master.View.Columns["ITEMVALUE05"].ToolTip = row["PRICEITEMNAME05"].ToString();
                        }
                        else
                        {
                            grdTab1Master.View.Columns["ITEMVALUE05"].Caption = row["PRICEITEMNAME05"].ToString();
                            grdTab1Master.View.Columns["ITEMVALUE05"].ToolTip = row["PRICEITEMNAME05"].ToString();

                        }

                    }
                }

                #endregion
            }
        }
        /// <summary>        
        /// 코드그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {

            grdTab1Master.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export;
          
            
        }
        private void InitializeGrid_GrdTab1MasterDisplay(string IsProduct)
        {
            grdTab1Master.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
         
            grdTab1Master.View.AddTextBoxColumn("ENTERPRISEID", 200).SetIsHidden();
            grdTab1Master.View.AddTextBoxColumn("PLANTID", 200).SetIsHidden();
            grdTab1Master.View.AddTextBoxColumn("PRICECOMBINATIONID", 200).SetIsHidden();
            grdTab1Master.View.AddTextBoxColumn("PRICECOMBINATIONNAME", 200).SetIsHidden();
            grdTab1Master.View.AddTextBoxColumn("OSPPRICECODE", 100).SetIsHidden();

            if (IsProduct.Equals("N"))
            {
                grdTab1Master.View.AddComboBoxColumn("OSPPRODUCTIONTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OSPProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                  .SetLabel("PRODUCTIONTYPE")
                  .SetEmptyItem("*", "*");  // 
                grdTab1Master.View.AddTextBoxColumn("PRODUCTDEFID", 100).SetIsHidden();
                grdTab1Master.View.AddTextBoxColumn("PRODUCTDEFVERSION", 100).SetIsHidden();
                grdTab1Master.View.AddTextBoxColumn("PRODUCTDEFNAME", 200).SetIsHidden();
            }
            else
            {
                grdTab1Master.View.AddComboBoxColumn("OSPPRODUCTIONTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OSPProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                    .SetLabel("PRODUCTIONTYPE")
                    .SetEmptyItem("*", "*")
                    .SetIsHidden();  // 
                InitializeGrid_productdePopup();
                grdTab1Master.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetIsReadOnly();
                grdTab1Master.View.AddTextBoxColumn("PRODUCTDEFNAME", 150).SetIsReadOnly();
            }

            grdTab1Master.View.AddTextBoxColumn("PRICEITEMID01", 200).SetIsHidden();
            grdTab1Master.View.AddTextBoxColumn("PRICEITEMID02", 200).SetIsHidden();
            grdTab1Master.View.AddTextBoxColumn("PRICEITEMID03", 200).SetIsHidden();
            grdTab1Master.View.AddTextBoxColumn("PRICEITEMID04", 200).SetIsHidden();
            grdTab1Master.View.AddTextBoxColumn("PRICEITEMID05", 200).SetIsHidden();
            grdTab1Master.View.AddTextBoxColumn("ISRANGE01", 200).SetIsHidden();
            grdTab1Master.View.AddTextBoxColumn("ISRANGE02", 200).SetIsHidden();
            grdTab1Master.View.AddTextBoxColumn("ISRANGE03", 200).SetIsHidden();
            grdTab1Master.View.AddTextBoxColumn("ISRANGE04", 200).SetIsHidden();
            grdTab1Master.View.AddTextBoxColumn("ISRANGE05", 200).SetIsHidden();
            DataRow row = _row;
            if (row != null)
            {
                #region PRICEITEMID01 

                if (!(row["PRICEITEMID01"].ToString().Equals("")))
                {

                    if (row["ISRANGE01"].ToString().Equals("Y"))  //구간여부 
                    {
                        string strFormatmask = row["FORMATMASK01"].ToString();
                        if (strFormatmask.Equals(""))
                        {
                            strFormatmask = "#,###";
                        }
                        grdTab1Master.View.AddSpinEditColumn("ITEMVALUE01FR", 120)
                           .SetTextAlignment(TextAlignment.Right)
                           .SetDisplayFormat(strFormatmask, MaskTypes.Numeric)
                             .IsFloatValue = true;//
                        grdTab1Master.View.AddSpinEditColumn("ITEMVALUE01TO", 120)
                           .SetTextAlignment(TextAlignment.Right)
                           .SetDisplayFormat(strFormatmask, MaskTypes.Numeric)
                             .IsFloatValue = true;//
                        grdTab1Master.View.AddTextBoxColumn("ITEMVALUE01", 100).SetIsHidden();
                        grdTab1Master.View.AddTextBoxColumn("ITEMVALUE01NAME", 100).SetIsHidden();
                    }
                    else
                    {
                        grdTab1Master.View.AddTextBoxColumn("ITEMVALUE01FR", 80).SetIsHidden();
                        grdTab1Master.View.AddTextBoxColumn("ITEMVALUE01TO", 80).SetIsHidden();
                        if (row["COMPONENTTYPE01"].ToString().Equals("PopUp"))
                        {
                            InitializeGrid_Tab1CommonPopup(row["DATASETTYPE01"].ToString(), row["DATASET01"].ToString(), "ITEMVALUE01", "ITEMVALUE01NAME");
                            grdTab1Master.View.AddTextBoxColumn("ITEMVALUE01NAME", 100).SetIsReadOnly();
                        }
                        else if (row["COMPONENTTYPE01"].ToString().Equals("ComboBox"))
                        {
                            if (row["DATASETTYPE01"].ToString().Equals("CodeClass"))
                            {
                                string param = "CODECLASSID=" + row["DATASET01"].ToString();
                                grdTab1Master.View.AddComboBoxColumn("ITEMVALUE01", 100, new SqlQuery("GetCodeList", "00001", param, $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                   .SetEmptyItem("*", "*");  // 
                            }
                            else
                            {
                                string param = row["DATASET01"].ToString();
                                if (param.Equals("GetYpStdProcesssegmentidByOsp"))
                                {
                                    grdTab1Master.View.AddComboBoxColumn("ITEMVALUE01", 100, new SqlQuery("GetYpStdProcesssegmentidByOsp", "10001", $"P_PROCESSSEGMENTCLASSID={_ProcesssegmentClassid}"
                                        , $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                       .SetEmptyItem("*", "*");  //
                                }
                                else if  (param.Equals("GetYpOutsourcingSpecOpenShortByOsp"))
                                {
                                    grdTab1Master.View.AddComboBoxColumn("ITEMVALUE01", 100, new SqlQuery("GetYpOutsourcingSpecOpenShortByOsp", "10001", $"P_PROCESSSEGMENTCLASSID={_ProcesssegmentClassid}"
                                        , $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                        .SetEmptyItem("*", "*");  //
                                }
                                
                                else
                                {
                                    grdTab1Master.View.AddComboBoxColumn("ITEMVALUE01", 100, new SqlQuery("GetCodeList", "10001", param, $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                   .SetEmptyItem("*", "*");  // 
                                }
                               
                            }
                            grdTab1Master.View.AddTextBoxColumn("ITEMVALUE01NAME", 100).SetIsHidden();
                        }
                        else
                        {
                            string strFormatmask = row["FORMATMASK01"].ToString();
                            if (strFormatmask.Equals(""))
                            {
                                grdTab1Master.View.AddTextBoxColumn("ITEMVALUE01", 100);
                            }
                            else
                            {
                                grdTab1Master.View.AddSpinEditColumn("ITEMVALUE01", 120)
                               .SetTextAlignment(TextAlignment.Right)
                               .SetDisplayFormat(strFormatmask, MaskTypes.Numeric);
                            }
                            grdTab1Master.View.AddTextBoxColumn("ITEMVALUE01NAME", 100).SetIsHidden();
                        }

                    }
                }
                else
                {
                    grdTab1Master.View.AddTextBoxColumn("ITEMVALUE01", 200).SetIsHidden();
                    grdTab1Master.View.AddTextBoxColumn("ITEMVALUE01FR", 200).SetIsHidden();
                    grdTab1Master.View.AddTextBoxColumn("ITEMVALUE01TO", 200).SetIsHidden();
                }
                #endregion
                #region PRICEITEMID02 

                if (!(row["PRICEITEMID02"].ToString().Equals("")))
                {

                    if (row["ISRANGE02"].ToString().Equals("Y"))  //구간여부 
                    {
                        string strFormatmask = row["FORMATMASK02"].ToString();
                        if (strFormatmask.Equals(""))
                        {
                            strFormatmask = "#,###";
                        }
                        grdTab1Master.View.AddSpinEditColumn("ITEMVALUE02FR", 120)
                           .SetTextAlignment(TextAlignment.Right)
                           .SetDisplayFormat(strFormatmask, MaskTypes.Numeric)
                             .IsFloatValue = true;//
                        grdTab1Master.View.AddSpinEditColumn("ITEMVALUE02TO", 120)
                           .SetTextAlignment(TextAlignment.Right)
                           .SetDisplayFormat(strFormatmask, MaskTypes.Numeric)
                             .IsFloatValue = true;//
                        grdTab1Master.View.AddTextBoxColumn("ITEMVALUE02", 100).SetIsHidden();
                        grdTab1Master.View.AddTextBoxColumn("ITEMVALUE02NAME", 100).SetIsHidden();
                    }
                    else
                    {
                        grdTab1Master.View.AddTextBoxColumn("ITEMVALUE02FR", 80).SetIsHidden();
                        grdTab1Master.View.AddTextBoxColumn("ITEMVALUE02TO", 80).SetIsHidden();
                        if (row["COMPONENTTYPE02"].ToString().Equals("PopUp"))
                        {
                            InitializeGrid_Tab1CommonPopup(row["DATASETTYPE02"].ToString(), row["DATASET02"].ToString(), "ITEMVALUE02", "ITEMVALUE02NAME");
                            grdTab1Master.View.AddTextBoxColumn("ITEMVALUE02NAME", 100).SetIsReadOnly();
                        }
                        else if (row["COMPONENTTYPE02"].ToString().Equals("ComboBox"))
                        {
                            if (row["DATASETTYPE02"].ToString().Equals("CodeClass"))
                            {
                                string param = "CODECLASSID=" + row["DATASET02"].ToString();
                                grdTab1Master.View.AddComboBoxColumn("ITEMVALUE02", 100, new SqlQuery("GetCodeList", "00001", param, $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                   .SetEmptyItem("*", "*");  // 
                            }
                            else
                            {
                                string param = row["DATASET02"].ToString();
                                if (param.Equals("GetYpStdProcesssegmentidByOsp"))
                                {
                                    grdTab1Master.View.AddComboBoxColumn("ITEMVALUE02", 100, new SqlQuery("GetYpStdProcesssegmentidByOsp", "10001", $"P_PROCESSSEGMENTCLASSID={_ProcesssegmentClassid}"
                                        , $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                       .SetEmptyItem("*", "*");  //
                                }
                                else if (param.Equals("GetYpOutsourcingSpecOpenShortByOsp"))
                                {
                                    grdTab1Master.View.AddComboBoxColumn("ITEMVALUE02", 100, new SqlQuery("GetYpOutsourcingSpecOpenShortByOsp", "10001", $"P_PROCESSSEGMENTCLASSID={_ProcesssegmentClassid}"
                                        , $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                        .SetEmptyItem("*", "*");  //
                                }
                                else
                                {
                                    grdTab1Master.View.AddComboBoxColumn("ITEMVALUE02", 100, new SqlQuery("GetCodeList", "10001", param, $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                   .SetEmptyItem("*", "*");  // 
                                }
                            }
                            grdTab1Master.View.AddTextBoxColumn("ITEMVALUE02NAME", 100).SetIsHidden();
                        }
                        else
                        {
                            string strFormatmask = row["FORMATMASK02"].ToString();
                            if (strFormatmask.Equals(""))
                            {
                                grdTab1Master.View.AddTextBoxColumn("ITEMVALUE02", 100);
                            }
                            else
                            {
                                grdTab1Master.View.AddSpinEditColumn("ITEMVALUE02", 120)
                               .SetTextAlignment(TextAlignment.Right)
                               .SetDisplayFormat(strFormatmask, MaskTypes.Numeric);
                            }
                            grdTab1Master.View.AddTextBoxColumn("ITEMVALUE02NAME", 100).SetIsHidden();
                        }
                    }
                }
                else
                {
                    grdTab1Master.View.AddTextBoxColumn("ITEMVALUE02", 200).SetIsHidden();
                    grdTab1Master.View.AddTextBoxColumn("ITEMVALUE02FR", 200).SetIsHidden();
                    grdTab1Master.View.AddTextBoxColumn("ITEMVALUE02TO", 200).SetIsHidden();
                }
                #endregion
                #region PRICEITEMID03 

                if (!(row["PRICEITEMID03"].ToString().Equals("")))
                {

                    if (row["ISRANGE03"].ToString().Equals("Y"))  //구간여부 
                    {
                        string strFormatmask = row["FORMATMASK03"].ToString();
                        if (strFormatmask.Equals(""))
                        {
                            strFormatmask = "#,###";
                        }
                        grdTab1Master.View.AddSpinEditColumn("ITEMVALUE03FR", 120)
                           .SetTextAlignment(TextAlignment.Right)
                           .SetDisplayFormat(strFormatmask, MaskTypes.Numeric)
                            .IsFloatValue = true;//
                        grdTab1Master.View.AddSpinEditColumn("ITEMVALUE03TO", 120)
                           .SetTextAlignment(TextAlignment.Right)
                           .SetDisplayFormat(strFormatmask, MaskTypes.Numeric)
                           .IsFloatValue = true;//
                        grdTab1Master.View.AddTextBoxColumn("ITEMVALUE03", 100).SetIsHidden();
                        grdTab1Master.View.AddTextBoxColumn("ITEMVALUE03NAME", 100).SetIsHidden();
                    }
                    else
                    {
                        grdTab1Master.View.AddTextBoxColumn("ITEMVALUE03FR", 80).SetIsHidden();
                        grdTab1Master.View.AddTextBoxColumn("ITEMVALUE03TO", 80).SetIsHidden();
                        if (row["COMPONENTTYPE03"].ToString().Equals("PopUp"))
                        {
                            InitializeGrid_Tab1CommonPopup(row["DATASETTYPE03"].ToString(), row["DATASET03"].ToString(), "ITEMVALUE03", "ITEMVALUE03NAME");
                            grdTab1Master.View.AddTextBoxColumn("ITEMVALUE03NAME", 100).SetIsReadOnly();
                        }
                        else if (row["COMPONENTTYPE03"].ToString().Equals("ComboBox"))
                        {
                            if (row["DATASETTYPE03"].ToString().Equals("CodeClass"))
                            {
                                string param = "CODECLASSID=" + row["DATASET03"].ToString();
                                grdTab1Master.View.AddComboBoxColumn("ITEMVALUE03", 100, new SqlQuery("GetCodeList", "00001", param, $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                   .SetEmptyItem("*", "*");  // 
                            }
                            else
                            {
                                string param = row["DATASET03"].ToString();
                                if (param.Equals("GetYpStdProcesssegmentidByOsp"))
                                {
                                    grdTab1Master.View.AddComboBoxColumn("ITEMVALUE03", 100, new SqlQuery("GetYpStdProcesssegmentidByOsp", "10001", $"P_PROCESSSEGMENTCLASSID={_ProcesssegmentClassid}"
                                        , $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                       .SetEmptyItem("*", "*");  //
                                }
                                else if (param.Equals("GetYpOutsourcingSpecOpenShortByOsp"))
                                {
                                    grdTab1Master.View.AddComboBoxColumn("ITEMVALUE03", 100, new SqlQuery("GetYpOutsourcingSpecOpenShortByOsp", "10001", $"P_PROCESSSEGMENTCLASSID={_ProcesssegmentClassid}"
                                        , $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                        .SetEmptyItem("*", "*");  //
                                }
                                else
                                {
                                    grdTab1Master.View.AddComboBoxColumn("ITEMVALUE03", 100, new SqlQuery("GetCodeList", "10001", param, $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                   .SetEmptyItem("*", "*");  // 
                                }
                            }
                            grdTab1Master.View.AddTextBoxColumn("ITEMVALUE03NAME", 100).SetIsHidden();
                        }
                        else
                        {
                            string strFormatmask = row["FORMATMASK03"].ToString();
                            if (strFormatmask.Equals(""))
                            {
                                grdTab1Master.View.AddTextBoxColumn("ITEMVALUE03", 100);
                            }
                            else
                            {
                                
                                grdTab1Master.View.AddSpinEditColumn("ITEMVALUE03", 120)
                               .SetTextAlignment(TextAlignment.Right)
                               .SetDisplayFormat(strFormatmask, MaskTypes.Numeric);
                            }
                            
                            
                            grdTab1Master.View.AddTextBoxColumn("ITEMVALUE03NAME", 100).SetIsHidden();
                        }
                    }
                }
                else
                {
                    grdTab1Master.View.AddTextBoxColumn("ITEMVALUE03", 200).SetIsHidden();
                    grdTab1Master.View.AddTextBoxColumn("ITEMVALUE03FR", 200).SetIsHidden();
                    grdTab1Master.View.AddTextBoxColumn("ITEMVALUE03TO", 200).SetIsHidden();
                }
                #endregion
                #region PRICEITEMID04 

                if (!(row["PRICEITEMID04"].ToString().Equals("")))
                {

                    if (row["ISRANGE04"].ToString().Equals("Y"))  //구간여부 
                    {
                        string strFormatmask = row["FORMATMASK04"].ToString();
                        if (strFormatmask.Equals(""))
                        {
                            strFormatmask = "#,###";
                        }
                        grdTab1Master.View.AddSpinEditColumn("ITEMVALUE04FR", 120)
                           .SetTextAlignment(TextAlignment.Right)
                           .SetDisplayFormat(strFormatmask, MaskTypes.Numeric)
                           .IsFloatValue = true;//
                        grdTab1Master.View.AddSpinEditColumn("ITEMVALUE04TO", 120)
                           .SetTextAlignment(TextAlignment.Right)
                           .SetDisplayFormat(strFormatmask, MaskTypes.Numeric)
                           .IsFloatValue = true;//
                        grdTab1Master.View.AddTextBoxColumn("ITEMVALUE04", 100).SetIsHidden();
                        grdTab1Master.View.AddTextBoxColumn("ITEMVALUE04NAME", 100).SetIsHidden();
                    }
                    else
                    {
                        grdTab1Master.View.AddTextBoxColumn("ITEMVALUE04FR", 80).SetIsHidden();
                        grdTab1Master.View.AddTextBoxColumn("ITEMVALUE04TO", 80).SetIsHidden();
                        if (row["COMPONENTTYPE04"].ToString().Equals("PopUp"))
                        {
                            InitializeGrid_Tab1CommonPopup(row["DATASETTYPE04"].ToString(), row["DATASET04"].ToString(), "ITEMVALUE04", "ITEMVALUE04NAME");
                            grdTab1Master.View.AddTextBoxColumn("ITEMVALUE04NAME", 100).SetIsReadOnly();
                        }
                        else if (row["COMPONENTTYPE04"].ToString().Equals("ComboBox"))
                        {
                            if (row["DATASETTYPE04"].ToString().Equals("CodeClass"))
                            {
                                string param = "CODECLASSID=" + row["DATASET04"].ToString();
                                grdTab1Master.View.AddComboBoxColumn("ITEMVALUE04", 100, new SqlQuery("GetCodeList", "00001", param, $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                   .SetEmptyItem("*", "*");  // 
                            }
                            else
                            {
                                string param = row["DATASET04"].ToString();
                                if (param.Equals("GetYpStdProcesssegmentidByOsp"))
                                {
                                    grdTab1Master.View.AddComboBoxColumn("ITEMVALUE04", 100, new SqlQuery("GetYpStdProcesssegmentidByOsp", "10001", $"P_PROCESSSEGMENTCLASSID={_ProcesssegmentClassid}"
                                        , $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                       .SetEmptyItem("*", "*");  //
                                }
                                else if (param.Equals("GetYpOutsourcingSpecOpenShortByOsp"))
                                {
                                    grdTab1Master.View.AddComboBoxColumn("ITEMVALUE04", 100, new SqlQuery("GetYpOutsourcingSpecOpenShortByOsp", "10001", $"P_PROCESSSEGMENTCLASSID={_ProcesssegmentClassid}"
                                        , $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                        .SetEmptyItem("*", "*");  //
                                }
                                else
                                {
                                    grdTab1Master.View.AddComboBoxColumn("ITEMVALUE04", 100, new SqlQuery("GetCodeList", "10001", param, $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                   .SetEmptyItem("*", "*");  // 
                                }
                            }
                            grdTab1Master.View.AddTextBoxColumn("ITEMVALUE04NAME", 100).SetIsHidden();
                        }
                        else
                        {
                           
                            string strFormatmask = row["FORMATMASK04"].ToString();
                            if (strFormatmask.Equals(""))
                            {
                                grdTab1Master.View.AddTextBoxColumn("ITEMVALUE04", 100);
                            }
                            else
                            {

                                grdTab1Master.View.AddSpinEditColumn("ITEMVALUE04", 120)
                               .SetTextAlignment(TextAlignment.Right)
                               .SetDisplayFormat(strFormatmask, MaskTypes.Numeric);
                            }
                            grdTab1Master.View.AddTextBoxColumn("ITEMVALUE04NAME", 100).SetIsHidden();
                        }
                    }
                }
                else
                {
                    grdTab1Master.View.AddTextBoxColumn("ITEMVALUE04", 200).SetIsHidden();
                    grdTab1Master.View.AddTextBoxColumn("ITEMVALUE04FR", 200).SetIsHidden();
                    grdTab1Master.View.AddTextBoxColumn("ITEMVALUE04TO", 200).SetIsHidden();
                }
                #endregion
                #region PRICEITEMID05 

                if (!(row["PRICEITEMID05"].ToString().Equals("")))
                {

                    if (row["ISRANGE05"].ToString().Equals("Y"))  //구간여부 
                    {
                        string strFormatmask = row["FORMATMASK05"].ToString();
                        if (strFormatmask.Equals(""))
                        {
                            strFormatmask = "#,###";
                        }
                        grdTab1Master.View.AddSpinEditColumn("ITEMVALUE05FR", 120)
                           .SetTextAlignment(TextAlignment.Right)
                           .SetDisplayFormat(strFormatmask, MaskTypes.Numeric);//
                        grdTab1Master.View.AddSpinEditColumn("ITEMVALUE05TO", 120)
                           .SetTextAlignment(TextAlignment.Right)
                           .SetDisplayFormat(strFormatmask, MaskTypes.Numeric);//
                        grdTab1Master.View.AddTextBoxColumn("ITEMVALUE05", 100).SetIsHidden();
                        grdTab1Master.View.AddTextBoxColumn("ITEMVALUE05NAME", 100).SetIsHidden();
                    }
                    else
                    {
                        grdTab1Master.View.AddTextBoxColumn("ITEMVALUE05FR", 80).SetIsHidden();
                        grdTab1Master.View.AddTextBoxColumn("ITEMVALUE05TO", 80).SetIsHidden();
                        if (row["COMPONENTTYPE05"].ToString().Equals("PopUp"))
                        {
                            //수정 예정 

                            InitializeGrid_Tab1CommonPopup(row["DATASETTYPE05"].ToString(), row["DATASET05"].ToString(), "ITEMVALUE05", "ITEMVALUE05NAME");
                            grdTab1Master.View.AddTextBoxColumn("ITEMVALUE05NAME", 100).SetIsReadOnly();
                        }
                        else if (row["COMPONENTTYPE05"].ToString().Equals("ComboBox"))
                        {
                            if (row["DATASETTYPE05"].ToString().Equals("CodeClass"))
                            {
                                string param = "CODECLASSID=" + row["DATASET05"].ToString();
                                grdTab1Master.View.AddComboBoxColumn("ITEMVALUE05", 100, new SqlQuery("GetCodeList", "00001", param, $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                   .SetEmptyItem("*", "*");  // 
                            }
                            else
                            {
                                string param = row["DATASET05"].ToString();
                                if (param.Equals("GetYpStdProcesssegmentidByOsp"))
                                {
                                    grdTab1Master.View.AddComboBoxColumn("ITEMVALUE05", 100, new SqlQuery("GetYpStdProcesssegmentidByOsp", "10001", $"P_PROCESSSEGMENTCLASSID={_ProcesssegmentClassid}"
                                        , $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                       .SetEmptyItem("*", "*");  //
                                }
                                else if (param.Equals("GetYpOutsourcingSpecOpenShortByOsp"))
                                {
                                    grdTab1Master.View.AddComboBoxColumn("ITEMVALUE05", 100, new SqlQuery("GetYpOutsourcingSpecOpenShortByOsp", "10001", $"P_PROCESSSEGMENTCLASSID={_ProcesssegmentClassid}"
                                        , $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                        .SetEmptyItem("*", "*");  //
                                }
                                else
                                {
                                    grdTab1Master.View.AddComboBoxColumn("ITEMVALUE05", 100, new SqlQuery("GetCodeList", "10001", param, $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                   .SetEmptyItem("*", "*");  // 
                                }
                            }
                            grdTab1Master.View.AddTextBoxColumn("ITEMVALUE05NAME", 100).SetIsHidden();
                        }
                        else
                        {
                            string strFormatmask = row["FORMATMASK05"].ToString();
                            if (strFormatmask.Equals(""))
                            {
                                grdTab1Master.View.AddTextBoxColumn("ITEMVALUE05", 100);
                            }
                            else
                            {

                                grdTab1Master.View.AddSpinEditColumn("ITEMVALUE05", 120)
                               .SetTextAlignment(TextAlignment.Right)
                               .SetDisplayFormat(strFormatmask, MaskTypes.Numeric);
                            }
                            //grdTab1Master.View.AddTextBoxColumn("ITEMVALUE05", 100);
                            grdTab1Master.View.AddTextBoxColumn("ITEMVALUE05NAME", 100).SetIsHidden();
                        }
                    }
                }
                else
                {
                    grdTab1Master.View.AddTextBoxColumn("ITEMVALUE05", 200).SetIsHidden();
                    grdTab1Master.View.AddTextBoxColumn("ITEMVALUE05FR", 200).SetIsHidden();
                    grdTab1Master.View.AddTextBoxColumn("ITEMVALUE05TO", 200).SetIsHidden();
                }
                #endregion
            }

            this.InitializeGrid_Tab1VendorPopup();// 
            grdTab1Master.View.AddTextBoxColumn("OSPVENDORNAME", 200).SetIsReadOnly();
            grdTab1Master.View.AddComboBoxColumn("PRICEUNIT", 100, new SqlQuery("GetUomDefinitionMapListByOsp", "10001", "UOMCATEGORY=OSPPRICEUNIT", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "UOMDEFID", "UOMDEFID")
                .SetValidationIsRequired()
                .SetDefault("");

            grdTab1Master.View.AddComboBoxColumn("VALIDSTATE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetValidationIsRequired()
                .SetDefault("Valid")
                .SetIsReadOnly();
          
            //grdTab1Master.View.SetKeyColumn("OSPPRODUCTIONTYPE", "PRODUCTDEFID", "OSPVENDORID", "PRICEUNIT", "ITEMVALUE01", "ITEMVALUE02", "ITEMVALUE03", "ITEMVALUE04", "ITEMVALUE05"
            //    ,"ITEMVALUE01FR", "ITEMVALUE02FR", "ITEMVALUE03FR", "ITEMVALUE04FR", "ITEMVALUE05FR"
            //    ,"ITEMVALUE01TO", "ITEMVALUE02TO", "ITEMVALUE03TO", "ITEMVALUE04TO", "ITEMVALUE05TO");
        }
        private void InitializeGrid_Tab1RangeTextEdit()
        {
            DataRow row = _row;
            if (row != null)
            {
                

                if (!(row["PRICEITEMID01"].ToString().Equals("")))
                {

                    if (row["ISRANGE01"].ToString().Equals("Y"))  //구간여부 
                    {
                        string strFormatmask = row["FORMATMASK01"].ToString();
                        if (strFormatmask.Equals(""))
                        {
                            strFormatmask = "#,###,##0";
                        }
                        InitializeGrid_Tab1ItemTextEdit(strFormatmask, "ITEMVALUE01FR", "ITEMVALUE01TO");
                    }
                }
                if (!(row["PRICEITEMID02"].ToString().Equals("")))
                {

                    if (row["ISRANGE02"].ToString().Equals("Y"))  //구간여부 
                    {
                        string strFormatmask = row["FORMATMASK02"].ToString();
                        if (strFormatmask.Equals(""))
                        {
                            strFormatmask = "#,###,##0";
                        }
                        InitializeGrid_Tab1ItemTextEdit(strFormatmask, "ITEMVALUE02FR", "ITEMVALUE02TO");
                    }
                }
                if (!(row["PRICEITEMID03"].ToString().Equals("")))
                {

                    if (row["ISRANGE03"].ToString().Equals("Y"))  //구간여부 
                    {
                        string strFormatmask = row["FORMATMASK03"].ToString();
                        if (strFormatmask.Equals(""))
                        {
                            strFormatmask = "#,###";
                        }
                        InitializeGrid_Tab1ItemTextEdit(strFormatmask, "ITEMVALUE03FR", "ITEMVALUE03TO");
                    }
                }
                if (!(row["PRICEITEMID04"].ToString().Equals("")))
                {

                    if (row["ISRANGE04"].ToString().Equals("Y"))  //구간여부 
                    {
                        string strFormatmask = row["FORMATMASK04"].ToString();
                        if (strFormatmask.Equals(""))
                        {
                            strFormatmask = "#,###,##0";
                        }
                        InitializeGrid_Tab1ItemTextEdit(strFormatmask, "ITEMVALUE04FR", "ITEMVALUE04TO");
                    }
                }
                if (!(row["PRICEITEMID05"].ToString().Equals("")))
                {

                    if (row["ISRANGE05"].ToString().Equals("Y"))  //구간여부 
                    {
                        string strFormatmask = row["FORMATMASK05"].ToString();
                        if (strFormatmask.Equals(""))
                        {
                            strFormatmask = "#,###,##0";
                        }
                        InitializeGrid_Tab1ItemTextEdit(strFormatmask, "ITEMVALUE05FR", "ITEMVALUE05TO");
                    }
                }
            }
        }
        private void InitializeGrid_Tab1ItemTextEdit(string strFormatmask ,string colFr ,string colTo)
        { 
            RepositoryItemTextEdit edit = new RepositoryItemTextEdit();
            edit.Mask.EditMask = strFormatmask;
            edit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            grdTab1Master.View.Columns[colFr].ColumnEdit = edit;
            grdTab1Master.View.Columns[colTo].ColumnEdit = edit;

        }

        private void InitializeGrid_Tab1CommonPopup(string strDataSettype, string strDataset, string targetid, string targetname)
        {
            //GetCodeListPopupByosp
            string SqlQueryid = "";
            string param = "CODECLASSID=" + strDataset;
            if (strDataSettype.Equals("CodeClass"))
            {
                SqlQueryid = "GetCodeListPopupByOsp";
                param = "CODECLASSID=" + strDataset;
            }
            else
            {
                SqlQueryid = strDataset;
               // param = "CODECLASSID=" + strDataset;
            }
            var popupGridProcessSegments = grdTab1Master.View.AddSelectPopupColumn(targetid, 120,
                new SqlQuery(SqlQueryid, "10001", param, $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}"))
                // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
                .SetPopupLayout("OSPCOMMONPOPUP", PopupButtonStyles.Ok_Cancel, true, false)
                // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
                .SetPopupResultCount(1)
                // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
                .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                .SetPopupResultMapping(targetid, "CODEID")
                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {
                    DataRow classRow = grdTab1Master.View.GetFocusedDataRow();

                    foreach (DataRow row in selectedRows)
                    {
                        classRow[targetid] = row["CODEID"];
                        classRow[targetname] = row["CODENAME"];
                    }
                })
            ;

            popupGridProcessSegments.Conditions.AddTextBox("P_CODENAME")
                .SetLabel("CODENAME");
            popupGridProcessSegments.GridColumns.AddTextBoxColumn("CODEID", 100);
            popupGridProcessSegments.GridColumns.AddTextBoxColumn("CODENAME", 200);

        }

        /// <summary>
        /// InitializeGrid_productdePopup
        /// </summary>
        /// <param name="TargetView"></param>
        private void InitializeGrid_productdePopup()
        {
            
            var popupGridProcessSegments = grdTab1Master.View.AddSelectPopupColumn("PRODUCTDEFID", 120,
               new SqlQuery("GetProductdefidlistYpPriceCodeByOsp", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}"))
               // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
               .SetPopupLayout("PRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)
               // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
               .SetPopupResultCount(1)
               .SetRelationIds("p_plantid")
               // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
               .SetPopupResultMapping("PRODUCTDEFID", "PRODUCTDEFID")
               // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
               .SetPopupLayoutForm(750, 600, FormBorderStyle.FixedToolWindow)
               // 그리드의 남은 영역을 채울 컬럼 설정

               .SetPopupAutoFillColumns("PRODUCTDEFID")
               .SetPopupApplySelection((selectedRows, dataGridRow) =>
               {
                   DataRow classRow = grdTab1Master.View.GetFocusedDataRow();

                   foreach (DataRow row in selectedRows)
                   {
                       classRow["PRODUCTDEFID"] = row["PRODUCTDEFID"];
                       classRow["PRODUCTDEFVERSION"] = row["PRODUCTDEFVERSION"];
                       classRow["PRODUCTDEFNAME"] = row["PRODUCTDEFNAME"];
                   }
               })
           ;
            popupGridProcessSegments.Conditions.AddComboBox("P_PRODUCTDEFTYPE", new SqlQuery("GetCodeListByOspProductdeftype", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("PRODUCTDEFTYPE")
                .SetDefault("Product");
            popupGridProcessSegments.Conditions.AddTextBox("PRODUCTDEFNAME");
            // 팝업 그리드 설정
            popupGridProcessSegments.GridColumns.AddComboBoxColumn("PRODUCTDEFTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductDefType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsReadOnly();
            popupGridProcessSegments.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            popupGridProcessSegments.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 150);
            popupGridProcessSegments.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);


        }

        /// <summary>
        /// InitializeGrid_VendorPopup
        /// </summary>
        private void InitializeGrid_Tab1VendorPopup()
        {
            var popupGridProcessSegments = grdTab1Master.View.AddSelectPopupColumn("OSPVENDORID",
                new SqlQuery("GetVendorListByOsp", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}"))
                // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
                .SetPopupLayout("OSPVENDORID", PopupButtonStyles.Ok_Cancel, true, false)
                // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
                .SetPopupResultCount(1)
                .SetRelationIds("PLANTID")
                // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
                .SetPopupResultMapping("OSPVENDORID", "OSPVENDORID")
                // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
                .SetPopupLayoutForm(500, 600, FormBorderStyle.FixedToolWindow)
                // 그리드의 남은 영역을 채울 컬럼 설정

                .SetPopupAutoFillColumns("OSPVENDORNAME")
                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {
                    DataRow classRow = grdTab1Master.View.GetFocusedDataRow();

                    foreach (DataRow row in selectedRows)
                    {
                        classRow["OSPVENDORNAME"] = row["OSPVENDORNAME"];
                    }
                })
            ;
            // 팝업에서 사용할 조회조건 항목 추가
            popupGridProcessSegments.Conditions.AddTextBox("OSPVENDORNAME");
            popupGridProcessSegments.Conditions.AddTextBox("PLANTID")
               .SetPopupDefaultByGridColumnId("PLANTID")
                .SetLabel("")
               .SetIsHidden();
            // 팝업 그리드 설정
            popupGridProcessSegments.GridColumns.AddTextBoxColumn("OSPVENDORID", 150)
                .SetValidationKeyColumn();
            popupGridProcessSegments.GridColumns.AddTextBoxColumn("OSPVENDORNAME", 200);


        }
        #region ComboBox  

        /// <summary>
        /// SmartComboBox 컨트롤 추가
        /// </summary>
        private void InitializeComboBox()
        {

           
        }

        #endregion
        #endregion

        #region 초기화
        /// <summary>
        /// 조회 조건 초기화
        /// </summary>
        private void InitializeCondition()
        {


        }
        #endregion

        #region  이벤트
        private void InitializeEvent()
        { 

           
            btnClose.Click += BtnClose_Click;
            // 저장 
            btnSave.Click += BtnSave_Click;

            grdTab1Master.View.ShowingEditor += GrdTab1Master_ShowingEditor;
            grdTab1Master.View.AddingNewRow += GrdTab1Master_AddingNewRow;
            
            grdTab1Master.ToolbarDeleteRow += GrdTab1Master_ToolbarDeleteRow;
        }

        /// <summary>
        /// GrdTab1Master_ToolbarDeleteRow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdTab1Master_ToolbarDeleteRow(object sender, EventArgs e)
        {

            DataRow row = grdTab1Master.View.GetFocusedDataRow();
            if (row == null)
            {
                return;
            }
            if (row.RowState != DataRowState.Added)
            {
                row["VALIDSTATE"] = "Invalid";
                (grdTab1Master.View.DataSource as DataView).Table.AcceptChanges();
            }
            //(grdTab1Master.View.DataSource as DataView).Delete(grdTab1Master.View.FocusedRowHandle);
            //(grdTab1Master.View.DataSource as DataView).Table.AcceptChanges();
        }
        private void GrdTab1Master_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
           
            grdTab1Master.View.SetFocusedRowCellValue("ENTERPRISEID", UserInfo.Current.Enterprise.ToString());// 회사코드
            grdTab1Master.View.SetFocusedRowCellValue("PLANTID", _row["PLANTID"].ToString());// plantid

            grdTab1Master.View.SetFocusedRowCellValue("OSPPRICECODE", _row["OSPPRICECODE"].ToString());
            grdTab1Master.View.SetFocusedRowCellValue("OSPPRODUCTIONTYPE", "*");
            if (_IsProduct.Equals("Y"))
            {
                grdTab1Master.View.SetFocusedRowCellValue("PRODUCTDEFID", "");
                grdTab1Master.View.SetFocusedRowCellValue("PRODUCTDEFVERSION", "");
            }
            else
            {
                grdTab1Master.View.SetFocusedRowCellValue("PRODUCTDEFID", "*");
                grdTab1Master.View.SetFocusedRowCellValue("PRODUCTDEFVERSION", "*");
            }
            ////grdTab1Master.View.SetFocusedRowCellValue("PRODUCTDEFID", "*");
            ////grdTab1Master.View.SetFocusedRowCellValue("PRODUCTDEFVERSION", "*");
            grdTab1Master.View.SetFocusedRowCellValue("OSPVENDORID", "*");
            grdTab1Master.View.SetFocusedRowCellValue("OSPVENDORNAME", "*");
            grdTab1Master.View.SetFocusedRowCellValue("PRICEITEMID01", _row["PRICEITEMID01"].ToString());
            grdTab1Master.View.SetFocusedRowCellValue("PRICEITEMID02", _row["PRICEITEMID02"].ToString());
            grdTab1Master.View.SetFocusedRowCellValue("PRICEITEMID03", _row["PRICEITEMID03"].ToString());
            grdTab1Master.View.SetFocusedRowCellValue("PRICEITEMID04", _row["PRICEITEMID04"].ToString());
            grdTab1Master.View.SetFocusedRowCellValue("PRICEITEMID05", _row["PRICEITEMID05"].ToString());
            grdTab1Master.View.SetFocusedRowCellValue("ISRANGE01", _row["ISRANGE01"].ToString());
            grdTab1Master.View.SetFocusedRowCellValue("ISRANGE02", _row["ISRANGE02"].ToString());
            grdTab1Master.View.SetFocusedRowCellValue("ISRANGE03", _row["ISRANGE03"].ToString());
            grdTab1Master.View.SetFocusedRowCellValue("ISRANGE04", _row["ISRANGE04"].ToString());
            grdTab1Master.View.SetFocusedRowCellValue("ISRANGE05", _row["ISRANGE05"].ToString());
            if (!(_row["PRICEITEMID01"].ToString().Equals("")))
            {
                grdTab1Master.View.SetFocusedRowCellValue("ITEMVALUE01", "*");
                grdTab1Master.View.SetFocusedRowCellValue("ITEMVALUE01NAME", "*");
            }
            if (!(_row["PRICEITEMID02"].ToString().Equals("")))
            {
                grdTab1Master.View.SetFocusedRowCellValue("ITEMVALUE02", "*");
                grdTab1Master.View.SetFocusedRowCellValue("ITEMVALUE02NAME", "*");
            }
            if (!(_row["PRICEITEMID03"].ToString().Equals("")))
            {
                grdTab1Master.View.SetFocusedRowCellValue("ITEMVALUE03", "*");
                grdTab1Master.View.SetFocusedRowCellValue("ITEMVALUE03NAME", "*");
            }
            if (!(_row["PRICEITEMID04"].ToString().Equals("")))
            {
                grdTab1Master.View.SetFocusedRowCellValue("ITEMVALUE04", "*");
                grdTab1Master.View.SetFocusedRowCellValue("ITEMVALUE04NAME", "*");
            }
            if (!(_row["PRICEITEMID05"].ToString().Equals("")))
            {
                grdTab1Master.View.SetFocusedRowCellValue("ITEMVALUE05", "*");
                grdTab1Master.View.SetFocusedRowCellValue("ITEMVALUE05NAME", "*");
            }

            grdTab1Master.View.SetFocusedRowCellValue("VALIDSTATE", "Valid");// 
        }
        private void GrdTab1Master_ShowingEditor(object sender, CancelEventArgs e)
        {
            ////
            DataRow row = grdTab1Master.View.GetFocusedDataRow();

            if (row.RowState != DataRowState.Added)
            {
                e.Cancel = true;
            }
        }
        private bool ProcSaveCheck()
        {
            grdTab1Master.View.FocusedRowHandle = grdTab1Master.View.FocusedRowHandle;
            grdTab1Master.View.FocusedColumn = grdTab1Master.View.Columns["OSPVENDORNAME"];
            grdTab1Master.View.ShowEditor();
            grdTab1Master.View.CheckValidation();
           
            DataTable changedTab1Master = grdTab1Master.GetChangedRows();

            if (changedTab1Master.Rows.Count > 0)
            {
                if (ProcSaveCheckTabMaster() == false)
                {

                    return false;
                }
            }
            
            if (changedTab1Master.Rows.Count == 0 )
            {
                // 저장할 데이터가 존재하지 않습니다.
                this.ShowMessage(MessageBoxButtons.OK, "NoSaveData");
                return false;
            }

            return true;
        }

        private bool ProcSaveCheckTabMaster()
        {

          
            for (int irow = 0; irow < grdTab1Master.View.DataRowCount; irow++)
            {

                DataRow row = grdTab1Master.View.GetDataRow(irow);

                if (row.RowState == DataRowState.Added || row.RowState == DataRowState.Modified)
                {
                    if ((_IsProduct.Equals("Y")) && (row["PRODUCTDEFID"].ToString().Equals("") || row["PRODUCTDEFID"].ToString().Equals("*")))
                    {
                        string lblQty = grdTab1Master.View.Columns["PRODUCTDEFID"].Caption.ToString();
                        this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblQty); //메세지
                        return false;
                    }
                    if (grdTab1Master.View.Columns["ITEMVALUE01"].Visible == true && row["ITEMVALUE01"].ToString().Equals(""))
                    {
                        string lblQty = grdTab1Master.View.Columns["ITEMVALUE01"].Caption.ToString();
                        this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblQty); //메세지
                        return false;
                    }
                    if (grdTab1Master.View.Columns["ITEMVALUE02"].Visible == true && row["ITEMVALUE02"].ToString().Equals(""))
                    {
                        string lblQty = grdTab1Master.View.Columns["ITEMVALUE02"].Caption.ToString();
                        this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblQty); //메세지
                        return false;
                    }
                    if (grdTab1Master.View.Columns["ITEMVALUE03"].Visible == true && row["ITEMVALUE03"].ToString().Equals(""))
                    {
                        string lblQty = grdTab1Master.View.Columns["ITEMVALUE03"].Caption.ToString();
                        this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblQty); //메세지
                        return false;
                    }
                    if (grdTab1Master.View.Columns["ITEMVALUE04"].Visible == true && row["ITEMVALUE04"].ToString().Equals(""))
                    {
                        string lblQty = grdTab1Master.View.Columns["ITEMVALUE04"].Caption.ToString();
                        this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblQty); //메세지
                        return false;
                    }
                    if (grdTab1Master.View.Columns["ITEMVALUE05"].Visible == true && row["ITEMVALUE05"].ToString().Equals(""))
                    {
                        string lblQty = grdTab1Master.View.Columns["ITEMVALUE01FR"].Caption.ToString();
                        this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblQty); //메세지
                        return false;
                    }
                    if (grdTab1Master.View.Columns["ITEMVALUE01FR"].Visible == true && row["ITEMVALUE01FR"].ToString().Equals(""))
                    {
                        string lblQty = grdTab1Master.View.Columns["ITEMVALUE01FR"].Caption.ToString();
                        this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblQty); //메세지
                        return false;
                    }
                    if (grdTab1Master.View.Columns["ITEMVALUE01TO"].Visible == true && row["ITEMVALUE01TO"].ToString().Equals(""))
                    {
                        string lblQty = grdTab1Master.View.Columns["ITEMVALUE01TO"].Caption.ToString();
                        this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblQty); //메세지
                        return false;
                    }
                    if (grdTab1Master.View.Columns["ITEMVALUE02FR"].Visible == true && row["ITEMVALUE02FR"].ToString().Equals(""))
                    {
                        string lblQty = grdTab1Master.View.Columns["ITEMVALUE02FR"].Caption.ToString();
                        this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblQty); //메세지
                        return false;
                    }
                    if (grdTab1Master.View.Columns["ITEMVALUE02TO"].Visible == true && row["ITEMVALUE02TO"].ToString().Equals(""))
                    {
                        string lblQty = grdTab1Master.View.Columns["ITEMVALUE02TO"].Caption.ToString();
                        this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblQty); //메세지
                        return false;
                    }
                    if (grdTab1Master.View.Columns["ITEMVALUE03FR"].Visible == true && row["ITEMVALUE03FR"].ToString().Equals(""))
                    {
                        string lblQty = grdTab1Master.View.Columns["ITEMVALUE03FR"].Caption.ToString();
                        this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblQty); //메세지
                        return false;
                    }
                    if (grdTab1Master.View.Columns["ITEMVALUE03TO"].Visible == true && row["ITEMVALUE03TO"].ToString().Equals(""))
                    {
                        string lblQty = grdTab1Master.View.Columns["ITEMVALUE03TO"].Caption.ToString();
                        this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblQty); //메세지
                    }
                    if (grdTab1Master.View.Columns["ITEMVALUE04FR"].Visible == true && row["ITEMVALUE04FR"].ToString().Equals(""))
                    {
                        string lblQty = grdTab1Master.View.Columns["ITEMVALUE04FR"].Caption.ToString();
                        this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblQty); //메세지
                        return false;
                    }
                    if (grdTab1Master.View.Columns["ITEMVALUE04TO"].Visible == true && row["ITEMVALUE04TO"].ToString().Equals(""))
                    {
                        string lblQty = grdTab1Master.View.Columns["ITEMVALUE04TO"].Caption.ToString();
                        this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblQty); //메세지
                    }
                    if (grdTab1Master.View.Columns["ITEMVALUE05FR"].Visible == true && row["ITEMVALUE05FR"].ToString().Equals(""))
                    {
                        string lblQty = grdTab1Master.View.Columns["ITEMVALUE05FR"].Caption.ToString();
                        this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblQty); //메세지
                        return false;
                    }
                    if (grdTab1Master.View.Columns["ITEMVALUE05TO"].Visible == true && row["ITEMVALUE05TO"].ToString().Equals(""))
                    {
                        string lblQty = grdTab1Master.View.Columns["ITEMVALUE05TO"].Caption.ToString();
                        this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblQty); //메세지
                        return false;
                    }
                    if (row["OSPVENDORID"].ToString().Equals(""))
                    {
                        row["OSPVENDORID"] = "*";
                    }
                    int checkrow = SearchTab1MasterValueKey(irow);
                    if (checkrow > -1)
                    {
                        string lblPeriodid = grdTab1Master.Caption.ToString();
                        this.ShowMessage(MessageBoxButtons.OK, "InValidOspData007", lblPeriodid);
                        return false;
                    }

                }
            }
           
          

            return true;
        }
        /// <summary>
        ///  중복 체크 함수
        /// </summary>
        /// <param name="dateStartdate"></param>
        /// <param name="dateEnddate"></param>
        /// <param name="icurRow"></param>
        /// <returns></returns>
        private int SearchTab1MasterValueKey(int icurRow)
        {
            int iresultRow = -1;
            DataRow dr =_row;
            string strPriceitemid01 = dr["PRICEITEMID01"].ToString();
            string strPriceitemid02 = dr["PRICEITEMID02"].ToString();
            string strPriceitemid03 = dr["PRICEITEMID03"].ToString();
            string strPriceitemid04 = dr["PRICEITEMID04"].ToString();
            string strPriceitemid05 = dr["PRICEITEMID05"].ToString();
            string strIsrange01 = dr["ISRANGE01"].ToString();
            string strIsrange02 = dr["ISRANGE02"].ToString();
            string strIsrange03 = dr["ISRANGE03"].ToString();
            string strIsrange04 = dr["ISRANGE04"].ToString();
            string strIsrange05 = dr["ISRANGE05"].ToString();
            DataRow drValue = grdTab1Master.View.GetDataRow(icurRow);
            string strOspproductiontype = drValue["OSPPRODUCTIONTYPE"].ToString();
            string strProductdefid = drValue["PRODUCTDEFID"].ToString();
            string strProductdefversion = drValue["PRODUCTDEFVERSION"].ToString();
            string strOspvendorid = drValue["OSPVENDORID"].ToString();
            string strItemvalue01 = ""; string strItemvalue01fr = ""; string strItemvalue01to = "";
            string strItemvalue02 = ""; string strItemvalue02fr = ""; string strItemvalue02to = "";
            string strItemvalue03 = ""; string strItemvalue03fr = ""; string strItemvalue03to = "";
            string strItemvalue04 = ""; string strItemvalue04fr = ""; string strItemvalue04to = "";
            string strItemvalue05 = ""; string strItemvalue05fr = ""; string strItemvalue05to = "";
            #region  strItemvalue셋팅
            #region PRICEITEMID01
            if (strPriceitemid01.Equals(""))
            {
                strItemvalue01 = ""; strItemvalue01fr = ""; strItemvalue01to = "";
            }
            else
            {
                if (strIsrange01.Equals("N"))
                {
                    strItemvalue01 = drValue["ITEMVALUE01"].ToString();
                    strItemvalue01fr = ""; strItemvalue01to = "";
                }
                else
                {
                    strItemvalue01 = "";
                    strItemvalue01fr = drValue["ITEMVALUE01FR"].ToString(); strItemvalue01to = drValue["ITEMVALUE01TO"].ToString();
                }

            }
            #endregion
            #region PRICEITEMID02
            if (strPriceitemid02.Equals(""))
            {
                strItemvalue02 = ""; strItemvalue02fr = ""; strItemvalue02to = "";
            }
            else
            {
                if (strIsrange02.Equals("N"))
                {
                    strItemvalue02 = drValue["ITEMVALUE02"].ToString();
                    strItemvalue02fr = ""; strItemvalue02to = "";
                }
                else
                {
                    strItemvalue02 = "";
                    strItemvalue02fr = drValue["ITEMVALUE02FR"].ToString(); strItemvalue02to = drValue["ITEMVALUE02TO"].ToString();
                }

            }
            #endregion
            #region PRICEITEMID03
            if (strPriceitemid03.Equals(""))
            {
                strItemvalue03 = ""; strItemvalue03fr = ""; strItemvalue03to = "";
            }
            else
            {
                if (strIsrange03.Equals("N"))
                {
                    strItemvalue03 = drValue["ITEMVALUE03"].ToString();
                    strItemvalue03fr = ""; strItemvalue03to = "";
                }
                else
                {
                    strItemvalue03 = "";
                    strItemvalue03fr = drValue["ITEMVALUE03FR"].ToString(); strItemvalue03to = drValue["ITEMVALUE03TO"].ToString();
                }

            }
            #endregion
            #region PRICEITEMID04
            if (strPriceitemid04.Equals(""))
            {
                strItemvalue04 = ""; strItemvalue04fr = ""; strItemvalue04to = "";
            }
            else
            {
                if (strIsrange04.Equals("N"))
                {
                    strItemvalue04 = drValue["ITEMVALUE04"].ToString();
                    strItemvalue04fr = ""; strItemvalue04to = "";
                }
                else
                {
                    strItemvalue04 = "";
                    strItemvalue04fr = drValue["ITEMVALUE04FR"].ToString(); strItemvalue04to = drValue["ITEMVALUE04TO"].ToString();
                }

            }
            #endregion
            #region PRICEITEMID05
            if (strPriceitemid05.Equals(""))
            {
                strItemvalue05 = ""; strItemvalue05fr = ""; strItemvalue05to = "";
            }
            else
            {
                if (strIsrange05.Equals("N"))
                {
                    strItemvalue05 = drValue["ITEMVALUE05"].ToString();
                    strItemvalue05fr = ""; strItemvalue01to = "";
                }
                else
                {
                    strItemvalue05 = "";
                    strItemvalue05fr = drValue["ITEMVALUE05FR"].ToString(); strItemvalue05to = drValue["ITEMVALUE05TO"].ToString();
                }

            }
            #endregion
            #endregion
            for (int irow = 0; irow < grdTab1Master.View.DataRowCount; irow++)
            {
                if (icurRow != irow)
                {
                    DataRow row = grdTab1Master.View.GetDataRow(irow);
                    // 행 삭제만 제외 
                    if (grdTab1Master.View.IsDeletedRow(row) == false)
                    {
                        string strTempOspproductiontype = row["OSPPRODUCTIONTYPE"].ToString();
                        string strTempProductdefid = row["PRODUCTDEFID"].ToString();
                        string strTempProductdefversion = row["PRODUCTDEFVERSION"].ToString();
                        string strTempOspvendorid = row["OSPVENDORID"].ToString();
                        string strTempItemvalue01 = ""; string strTempItemvalue01fr = ""; string strTempItemvalue01to = "";
                        string strTempItemvalue02 = ""; string strTempItemvalue02fr = ""; string strTempItemvalue02to = "";
                        string strTempItemvalue03 = ""; string strTempItemvalue03fr = ""; string strTempItemvalue03to = "";
                        string strTempItemvalue04 = ""; string strTempItemvalue04fr = ""; string strTempItemvalue04to = "";
                        string strTempItemvalue05 = ""; string strTempItemvalue05fr = ""; string strTempItemvalue05to = "";
                        #region  strTempItemvalue셋팅
                        #region PRICEITEMID01
                        if (strPriceitemid01.Equals(""))
                        {
                            strTempItemvalue01 = ""; strTempItemvalue01fr = ""; strTempItemvalue01to = "";
                        }
                        else
                        {
                            if (strIsrange01.Equals("N"))
                            {
                                strTempItemvalue01 = row["ITEMVALUE01"].ToString();
                                strTempItemvalue01fr = ""; strTempItemvalue01to = "";
                            }
                            else
                            {
                                strTempItemvalue01 = "";
                                strTempItemvalue01fr = row["ITEMVALUE01FR"].ToString(); strTempItemvalue01to = row["ITEMVALUE01TO"].ToString();
                            }

                        }
                        #endregion
                        #region PRICEITEMID02
                        if (strPriceitemid02.Equals(""))
                        {
                            strTempItemvalue02 = ""; strTempItemvalue02fr = ""; strTempItemvalue02to = "";
                        }
                        else
                        {
                            if (strIsrange02.Equals("N"))
                            {
                                strTempItemvalue02 = row["ITEMVALUE02"].ToString();
                                strTempItemvalue02fr = ""; strTempItemvalue02to = "";
                            }
                            else
                            {
                                strTempItemvalue02 = "";
                                strTempItemvalue02fr = row["ITEMVALUE02FR"].ToString(); strTempItemvalue02to = row["ITEMVALUE02TO"].ToString();
                            }

                        }
                        #endregion
                        #region PRICEITEMID03
                        if (strPriceitemid03.Equals(""))
                        {
                            strTempItemvalue03 = ""; strTempItemvalue03fr = ""; strTempItemvalue03to = "";
                        }
                        else
                        {
                            if (strIsrange03.Equals("N"))
                            {
                                strTempItemvalue03 = row["ITEMVALUE03"].ToString();
                                strTempItemvalue03fr = ""; strTempItemvalue03to = "";
                            }
                            else
                            {
                                strTempItemvalue03 = "";
                                strTempItemvalue03fr = row["ITEMVALUE03FR"].ToString(); strTempItemvalue03to = row["ITEMVALUE03TO"].ToString();
                            }

                        }
                        #endregion
                        #region PRICEITEMID04
                        if (strPriceitemid04.Equals(""))
                        {
                            strTempItemvalue04 = ""; strTempItemvalue04fr = ""; strTempItemvalue04to = "";
                        }
                        else
                        {
                            if (strIsrange04.Equals("N"))
                            {
                                strTempItemvalue04 = row["ITEMVALUE04"].ToString();
                                strTempItemvalue04fr = ""; strTempItemvalue04to = "";
                            }
                            else
                            {
                                strTempItemvalue04 = "";
                                strTempItemvalue04fr = row["ITEMVALUE04FR"].ToString(); strTempItemvalue04to = row["ITEMVALUE04TO"].ToString();
                            }

                        }
                        #endregion
                        #region PRICEITEMID05
                        if (strPriceitemid05.Equals(""))
                        {
                            strTempItemvalue05 = ""; strTempItemvalue05fr = ""; strTempItemvalue05to = "";
                        }
                        else
                        {
                            if (strIsrange05.Equals("N"))
                            {
                                strTempItemvalue05 = row["ITEMVALUE05"].ToString();
                                strTempItemvalue05fr = ""; strTempItemvalue01to = "";
                            }
                            else
                            {
                                strTempItemvalue05 = "";
                                strTempItemvalue05fr = row["ITEMVALUE05FR"].ToString(); strTempItemvalue05to = row["ITEMVALUE05TO"].ToString();
                            }

                        }
                        #endregion

                        #endregion

                        if (strOspproductiontype.Equals(strTempOspproductiontype) && strProductdefid.Equals(strTempProductdefid) && strProductdefversion.Equals(strTempProductdefversion)
                            && strItemvalue01.Equals(strTempItemvalue01) && strItemvalue01fr.Equals(strTempItemvalue01fr) && strItemvalue01to.Equals(strTempItemvalue01to)
                            && strItemvalue02.Equals(strTempItemvalue02) && strItemvalue02fr.Equals(strTempItemvalue02fr) && strItemvalue02to.Equals(strTempItemvalue02to)
                            && strItemvalue03.Equals(strTempItemvalue03) && strItemvalue03fr.Equals(strTempItemvalue03fr) && strItemvalue03to.Equals(strTempItemvalue03to)
                            && strItemvalue04.Equals(strTempItemvalue04) && strItemvalue04fr.Equals(strTempItemvalue04fr) && strItemvalue04to.Equals(strTempItemvalue04to)
                            && strItemvalue05.Equals(strTempItemvalue05) && strItemvalue05fr.Equals(strTempItemvalue05fr) && strItemvalue05to.Equals(strTempItemvalue05to)
                            && strOspvendorid.Equals(strTempOspvendorid))
                        {
                            return irow;
                        }

                    }
                }
            }
            return iresultRow;
        }
        /// <summary>
        /// 저장 클릭시
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            grdTab1Master.View.FocusedRowHandle = grdTab1Master.View.FocusedRowHandle;
            grdTab1Master.View.FocusedColumn = grdTab1Master.View.Columns["OSPVENDORNAME"];
            grdTab1Master.View.ShowEditor();
            DataTable changed = grdTab1Master.GetChangedRows();
            if (ProcSaveCheck() == false)
            {
                return;
            }

            //DataTable changed = grdTab1Master.GetChangedRows();

            if (changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                this.ShowMessage(MessageBoxButtons.OK, "NoSaveData");
                return;
            }
            DialogResult result = System.Windows.Forms.DialogResult.No;

            result = this.ShowMessage(MessageBoxButtons.YesNo, "OspDoProcessWant", btnSave.Text);//저장하시겠습니까? 

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    this.ShowWaitArea();
                    btnSave.Enabled = false;
                    btnClose.Enabled = false;

                    DataTable dtSaveMaster = (grdTab1Master.DataSource as DataTable).Clone();
                    dtSaveMaster.TableName = "listMain";
                    DataTable dtSaveDetail = (grdTab1Master.DataSource as DataTable).Clone();
                    dtSaveDetail.TableName = "listDetail";
                    DataTable dtSaveTab1Master = changed;
                    DataTable dtSaveTab1Detail = (grdTab1Master.DataSource as DataTable).Clone();
                    DataRow dr=null;
                    for (int irow = 0; irow < dtSaveTab1Master.Rows.Count; irow++)
                    {

                        dr = dtSaveTab1Master.Rows[irow];
                        string Ospvendorid = dr["OSPVENDORID"].ToString();

                        Ospvendorid = Ospvendorid.Replace(",", "");
                        string[] arr = Ospvendorid.Split('.');
                        dr["OSPVENDORID"] = arr[0];
                        if (dr["ISRANGE01"].ToString().Equals("Y"))
                        {
                            string Itemvalue01fr = dr["ITEMVALUE01FR"].ToString();
                            string Itemvalue01to = dr["ITEMVALUE01TO"].ToString();
                            Itemvalue01fr = Itemvalue01fr.Replace(",", "");
                            Itemvalue01to = Itemvalue01to.Replace(",", "");
                            dr["ITEMVALUE01FR"] = Itemvalue01fr;
                            dr["ITEMVALUE01TO"] = Itemvalue01to;
                        }
                        if (dr["ISRANGE02"].ToString().Equals("Y"))
                        {
                            string Itemvalue02fr = dr["ITEMVALUE02FR"].ToString();
                            string Itemvalue02to = dr["ITEMVALUE02TO"].ToString();
                            Itemvalue02fr = Itemvalue02fr.Replace(",", "");
                            Itemvalue02to = Itemvalue02to.Replace(",", "");
                            dr["ITEMVALUE02FR"] = Itemvalue02fr;
                            dr["ITEMVALUE02TO"] = Itemvalue02to;
                        }
                        if (dr["ISRANGE03"].ToString().Equals("Y"))
                        {
                            string Itemvalue03fr = dr["ITEMVALUE03FR"].ToString();
                            string Itemvalue03to = dr["ITEMVALUE03TO"].ToString();
                            Itemvalue03fr = Itemvalue03fr.Replace(",", "");
                            Itemvalue03to = Itemvalue03to.Replace(",", "");
                            dr["ITEMVALUE03FR"] = Itemvalue03fr;
                            dr["ITEMVALUE03TO"] = Itemvalue03to;
                        }
                        if (dr["ISRANGE04"].ToString().Equals("Y"))
                        {
                            string Itemvalue04fr = dr["ITEMVALUE04FR"].ToString();
                            string Itemvalue04to = dr["ITEMVALUE04TO"].ToString();
                            Itemvalue04fr = Itemvalue04fr.Replace(",", "");
                            Itemvalue04to = Itemvalue04to.Replace(",", "");
                            dr["ITEMVALUE04FR"] = Itemvalue04fr;
                            dr["ITEMVALUE04TO"] = Itemvalue04to;
                        }
                        if (dr["ISRANGE05"].ToString().Equals("Y"))
                        {
                            string Itemvalue05fr = dr["ITEMVALUE05FR"].ToString();
                            string Itemvalue05to = dr["ITEMVALUE05TO"].ToString();
                            Itemvalue05fr = Itemvalue05fr.Replace(",", "");
                            Itemvalue05to = Itemvalue05to.Replace(",", "");
                            dr["ITEMVALUE05FR"] = Itemvalue05fr;
                            dr["ITEMVALUE05TO"] = Itemvalue05to;
                        }
                    }
                   
                    dtSaveTab1Master.TableName = "listTab1Main";
                    dtSaveTab1Detail.TableName = "listTab1Detail";
                    DataSet dsSave = new DataSet();
                    dsSave.Tables.Add(dtSaveMaster);
                    dsSave.Tables.Add(dtSaveDetail);
                    dsSave.Tables.Add(dtSaveTab1Master);
                    dsSave.Tables.Add(dtSaveTab1Detail);

                    DataTable saveResult = this.ExecuteRule<DataTable>("OutsourcingYoungPongPriceCode", dsSave);
                    DataRow resultData = saveResult.Rows[0];
                    string strtempno = resultData.ItemArray[0].ToString();
                    ShowMessage("SuccessOspProcess");
                    DataTable dtTab1Master = GetOspypricecombination();
                    if (dtTab1Master.Rows.Count < 1)
                    {
                        //단가 그리드 clear
                        DataTable dtPrice = (grdTab1Master.DataSource as DataTable).Clone();
                        //grdDetail.View.ClearDatas();
                        grdTab1Master.DataSource = dtPrice;

                    }
                    else
                    {
                        grdTab1Master.DataSource = dtTab1Master;
                    }
                    if (dtSaveTab1Master.Rows.Count > 0)
                    {
                        OnSaveTabMasterDisplay(strtempno);
                    }
                   
                }
                catch (Exception ex)
                {
                    this.ShowError(ex);
                }
                finally
                {
                    this.CloseWaitArea();
                   
                    btnSave.Enabled = true;
                    btnClose.Enabled = true;

                }
            }

        }
        private void OnSaveTabMasterDisplay(string strtempno)
        {

              //재조회 
                DataTable dtTab1Master = GetOspypricecombination();
                if (dtTab1Master.Rows.Count < 1)
                {
                    //단가 그리드 clear
                    DataTable dtPrice = (grdTab1Master.DataSource as DataTable).Clone();
                    //grdDetail.View.ClearDatas();
                    grdTab1Master.DataSource = dtPrice;
            
                }
                int irow = GetGridRowSearch( "PRICECOMBINATIONID", strtempno);
                if (irow >= 0)
                {
                    grdTab1Master.View.FocusedRowHandle = irow;
                    grdTab1Master.View.SelectRow(irow);
                   
                }
                else
                {
                    grdTab1Master.View.FocusedRowHandle = 0;
                    grdTab1Master.View.SelectRow(0);
                 
                }
           

        }

        /// <summary>
        /// 그리드 이동에 필요한 row 찾기
        /// </summary>
        /// <param name="strRequestno"></param>
        private int GetGridRowSearch( string strcol, string strvalue)
        {
            int iRow = -1;
            if (grdTab1Master.View.DataRowCount == 0)
            {
                return iRow;
            }
            for (int i = 0; i < grdTab1Master.View.DataRowCount; i++)
            {
                if (grdTab1Master.View.GetRowCellValue(i, strcol).ToString().Equals(strvalue))
                {
                    return i;
                }
            }
            return iRow;
        }
        private DataTable GetOspypricecombination()
        {
            //단가코드 정보 가져오기 
            Dictionary<string, object> Param = new Dictionary<string, object>();

            Param.Add("P_PLANTID", _row["PLANTID"].ToString());
            Param.Add("P_OSPPRICECODE", _row["OSPPRICECODE"].ToString());
            Param.Add("P_PRODUCTDEFID", _IsProduct);
            Param.Add("P_ISRANGE01", _row["ISRANGE01"].ToString());
            Param.Add("P_ISRANGE02", _row["ISRANGE02"].ToString());
            Param.Add("P_ISRANGE03", _row["ISRANGE03"].ToString());
            Param.Add("P_ISRANGE04", _row["ISRANGE04"].ToString());
            Param.Add("P_ISRANGE05", _row["ISRANGE05"].ToString());
            Param.Add("P_COMPONENTTYPE01", _row["COMPONENTTYPE01"].ToString());
            Param.Add("P_COMPONENTTYPE02", _row["COMPONENTTYPE02"].ToString());
            Param.Add("P_COMPONENTTYPE03", _row["COMPONENTTYPE03"].ToString());
            Param.Add("P_COMPONENTTYPE04", _row["COMPONENTTYPE04"].ToString());
            Param.Add("P_COMPONENTTYPE05", _row["COMPONENTTYPE05"].ToString());
            Param.Add("P_VALIDSTATE", "Valid");
            Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            Param = Commons.CommonFunction.ConvertParameter(Param);
            DataTable dtDetail = SqlExecuter.Query("GetOutsourcingYoungPongPricecombinationPopUp", "10001", Param);

            return dtDetail;
        }


        /// <summary>
        /// 닫기 클릭시
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClose_Click(object sender, EventArgs e)
        {

            this.Close();
        }
       
       
        



        #endregion


    }
}
