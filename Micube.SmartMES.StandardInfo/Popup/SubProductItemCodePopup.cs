using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;

namespace Micube.SmartMES.StandardInfo
{
    public partial class SubProductItemCodePopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region Interface
        public delegate void AddSemiProductEventHandlerDelegate(object sender, AddSemiProductEventArgs e);

        public event AddSemiProductEventHandlerDelegate AddSemiProductEventHandler;

        public DataRow CurrentDataRow { get; set; }

        public List<string> SemiProductItemCodeList { get; set; }


        #endregion

        #region 생성자
        public SubProductItemCodePopup(DataRow dataRow)
        {
            InitializeComponent();
            this.CurrentDataRow = dataRow;
            SemiProductItemCodeList = new List<string>();
            InitializeEvent();
            InitializeGrid();
            initializeControl();
            Initialize();
        }
        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// Grid 초기화
        /// </summary>
        private void InitializeGrid()
        {
            grdSubProductItemCode.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export;

            grdSubProductItemCode.View.AddComboBoxColumn("SEMIPRODUCTTYPE", 150,
                new SqlQuery("GetCodeList", "00001", "CODECLASSID=SemiProductType",
                    $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetValidationIsRequired();


            if ( UserInfo.Current.Enterprise == "INTERFLEX")
            {
                grdSubProductItemCode.View.AddComboBoxColumn("LAYER1", 90,
                    new SqlQuery("GetCodeList", "00001", "CODECLASSID=Layer",
                        $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

                grdSubProductItemCode.View.AddComboBoxColumn("LAYER2", 90,
                    new SqlQuery("GetCodeList", "00001", "CODECLASSID=Layer",
                        $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            }


            grdSubProductItemCode.View.AddSpinEditColumn("OSPDAY01QTY", 120);
            
            grdSubProductItemCode.View.PopulateColumns();
        }


        /// <summary>
        /// Control 초기화
        /// </summary>
        private void initializeControl()
        {
            if ( UserInfo.Current.Enterprise.Equals("INTERFLEX"))
            { 
                // 반제품 코드 채번시 인터의 경우 사이트 구분자 추가 됨
                cboSiteType.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
                cboSiteType.ValueMember = "CODEID";
                cboSiteType.DisplayMember = "CODENAME";


                Dictionary<string, object> dicParam = new Dictionary<string, object>();
                dicParam.Add("CODECLASSID", "SiteType");
                dicParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                cboSiteType.DataSource = SqlExecuter.Query("GetCodeList", "00001", dicParam);
                
                cboSiteType.ShowHeader = false;
            }
            else
            {
                cboSiteType.Visible = false;
            }
        }

        public void Initialize()
        {
            //var row = CurrentDataRow;
            tbProductID.Text = CurrentDataRow["ITEMID"].ToString();
            tbProductName.Text = CurrentDataRow["ITEMNAME"].ToString();
            tbProductRev.Text = CurrentDataRow["ITEMVERSION"].ToString();

            tbProductID.ReadOnly = true;
            tbProductName.ReadOnly = true;
            tbProductRev.ReadOnly = true;
        }

        #endregion

        #region Event

        /// <summary>
        /// Event 초기화 
        /// </summary>
        private void InitializeEvent()
        {
            btnOK.Click += BtnOK_Click;
            
        }

        /// <summary>
        /// 취소 버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 저장 버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOK_Click(object sender, EventArgs e)
        {
			grdSubProductItemCode.View.PostEditor();
			grdSubProductItemCode.View.UpdateCurrentRow();

			try
            {
                DataTable changed = new DataTable();

                changed = grdSubProductItemCode.GetChangedRows();


                if (changed.Rows.Count == 0)
                {
                    // 저장할 데이터가 존재하지 않습니다.
                    throw MessageException.Create("NoSaveData");
                }

                // 저장할 파일데이터가 있다면 서버에 업로드한다.
                if (changed.Rows.Count != 0)
                {

                    int chkAdded = 0;

                    Dictionary<string, List<string>> semiProductDictionary = new Dictionary<string, List<string>>();
                    foreach (DataRow row in changed.Rows)
                    {
                        if (row["_STATE_"].ToString() == "added")
                        {
                            if (UserInfo.Current.Enterprise.Equals("INTERFLEX"))
                                SemiProductItemCodeList = GetSemiProductCode(row["SEMIPRODUCTTYPE"].ToString(), tbProductRev.Text, row["LAYER1"].ToString(), row["LAYER2"].ToString(), cboSiteType.EditValue.ToString(), int.Parse(row["OSPDAY01QTY"].ToString()));
                            else
                                SemiProductItemCodeList = GetSemiProductCode(row["SEMIPRODUCTTYPE"].ToString(), tbProductRev.Text, "", "", "", int.Parse(row["OSPDAY01QTY"].ToString()));

                            if (semiProductDictionary.ContainsKey(row["SEMIPRODUCTTYPE"].ToString()))
                            { 
                                ShowMessage("SameTypeForSubItemCode");
                                return;
                            }
                            if ( UserInfo.Current.Enterprise.Equals("INTERFLEX") && int.Parse(row["OSPDAY01QTY"].ToString()) > 9)
                            { 
                                ShowMessage("WrongTimeForSubItemCode");
                                return;
                            }
                            semiProductDictionary.Add(row["SEMIPRODUCTTYPE"].ToString(), SemiProductItemCodeList);
                        }
                    }
                    RaiseOnAddSemiProduct(sender,
                        new AddSemiProductEventArgs()
                        {
                            SemiProductCodeDictionary = semiProductDictionary,
                            ProductDataRow = CurrentDataRow
                        });

                    this.Close();

                }
                changed.AcceptChanges();
            }
            catch (Exception ex)
            {
                //MessageException.Create(ex.ToString());
                MessageBox.Show(ex.ToString());
            }
          
        }




        #endregion

        #region Public Function
        
       

        #endregion

        private List<string> GetSemiProductCode(string semiProudctId, string rev, string layer1, string layer2, string siteType, int count)
        {
            try
            {
                List<string> returnList = new List<string>();

                string productId = tbProductID.Text;
                string customerName = string.Empty;
                string itemType = string.Empty;
                string number = string.Empty;
                string layer = string.Empty;
                string customerRev = string.Empty;
                if (string.IsNullOrWhiteSpace(productId))
                    return null;

                if (UserInfo.Current.Enterprise.Equals("YOUNGPOONG"))
                {
                    customerName = productId.Substring(0, 3);
                    number = productId.Substring(3, 4);
                    customerRev = productId.Substring(7, 2);

                    for (int i = 0; i < count; i++)
                    {
                        StringBuilder semiProductCode = new StringBuilder();
                        semiProductCode.Append(customerName);
                        semiProductCode.Append(number);
                        semiProductCode.Append(semiProudctId);
                        semiProductCode.Append($"{i + 1:D2}");
                        semiProductCode.Append(customerRev);
                        returnList.Add(semiProductCode.ToString());
                    }
                }
                else
                {
                    // 인터 반제품 채번룰 변경 (2020/03/12) 
                    // 품목유형(1) + LAYER(2) + 일련번호(5) + 고객Rev(2) + 자재구분(2) + 사용 LAYER1(2) + 사용 LAYER2(2) + 차수(1) + 투입사이트(1)
                    itemType = productId.Substring(0, 1);
                    layer = productId.Substring(1, 2);
                    number = productId.Substring(3, 5);
                    customerRev = productId.Substring(8, 2);

                    if (layer1.Length < 2)
                        layer1 = "00";
                    if (layer2.Length < 2)
                        layer2 = "00";

                    for (int i = 0; i < count; i++)
                    {
                        StringBuilder semiProductCode = new StringBuilder();
                        semiProductCode.Append("2");
                        semiProductCode.Append(layer);
                        semiProductCode.Append(number);
                        semiProductCode.Append(customerRev);
                        semiProductCode.Append(semiProudctId);
                        semiProductCode.Append($"{layer1:D2}");
                        semiProductCode.Append($"{layer2:D2}");
                        semiProductCode.Append(siteType);
                        semiProductCode.Append($"{i + 1:D1}");
                        returnList.Add(semiProductCode.ToString());
                    }
                }
                
               
                return returnList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private void RaiseOnAddSemiProduct(object sender, AddSemiProductEventArgs e)
        {
            AddSemiProductEventHandler?.Invoke(sender, e);
        }
    }

    public class AddSemiProductEventArgs
    {
        public Dictionary<string, List<string>> SemiProductCodeDictionary { get; set; }
        
        public DataRow ProductDataRow { get; set; }

        public AddSemiProductEventArgs()
        {
            SemiProductCodeDictionary = new Dictionary<string, List<string>>();
            
        }
    }
}
