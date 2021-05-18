#region using
using DevExpress.XtraEditors.Repository;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Validations;
using Micube.SmartMES.Commons;
using System;
using Micube.SmartMES.Commons.Controls;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
#endregion

namespace Micube.SmartMES.Test
{
    /// <summary>
    /// 프 로 그 램 명  : 교육 > TEST  > 테스트 설비관리 
    /// 업  무  설  명  : 설비/ 설비단 정보를 관리한다.
    /// 생    성    자  : 정수현
    /// 생    성    일  : 20121-04-19
    /// 수  정  이  력  : 
    /// </summary>
    public partial class TestEquipmentManagement : SmartConditionManualBaseForm
    {
        #region Local Variables

        string _focusedId = "";

        #endregion

        #region 생성자
        public TestEquipmentManagement()
        {
            InitializeComponent();

        }
        #endregion

        #region 컨텐츠 영역 초기화   
        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeGridEquipmentList();

            InitializeEvent();
            InitializeTreeEquipmentClass();
            smartTabControl1.SetLanguageKey(xtraTabPage1, "EQUIPMENTSTATUS");
            smartTabControl1.SetLanguageKey(xtraTabPage2, "EQUIPMENTREGISTER");
        }

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGridEquipmentList()
        {
            #region 현황 

            grdEquipmentList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdEquipmentList.GridButtonItem = GridButtonItem.Export | GridButtonItem.Refresh;

            grdEquipmentList.View.SetIsReadOnly();
            grdEquipmentList.View.AddTextBoxColumn("ENTERPRISEID")//SITE
               .SetIsHidden();

            grdEquipmentList.View.AddTextBoxColumn("PLANTID", 70)//설비명(k)
               .SetLabel("SITE")
               .SetIsReadOnly();
            grdEquipmentList.View.AddComboBoxColumn("EQUIPMENTTYPE", 70, new SqlQuery("GetTypeList", "10001", "CODECLASSID=EquipmentType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID");//설비타입


            grdEquipmentList.View.AddComboBoxColumn("SEPARATOR", 120, new SqlQuery("GetCodeList", "00001", "CODECLASSID=Separator", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))//유효상태
                .SetTextAlignment(TextAlignment.Center);

            grdEquipmentList.View.AddTextBoxColumn("EQUIPMENTID", 120);//설비명(E)

            grdEquipmentList.View.AddLanguageColumn("EQUIPMENTNAME", 250);//설비명(c)


            grdEquipmentList.View.AddTextBoxColumn("EQUIPMENTCLASSID", 70)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);// 설비그룹 ID

            grdEquipmentList.View.AddTextBoxColumn("EQUIPMENTCLASSNAME", 110).SetIsReadOnly();// 설비그룹명


            grdEquipmentList.View.AddComboBoxColumn("DETAILEQUIPMENTTYPE", 130, new SqlQuery("GetTypeList", "10001", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}", "CODECLASSID=DetailEquipmentType"), "CODENAME", "CODEID")
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false); //상세설비타입

            grdEquipmentList.View.AddTextBoxColumn("AREAID", 130)//작업장 ID
                .SetIsReadOnly();

            grdEquipmentList.View.AddTextBoxColumn("AREANAME", 210)//작업장명
                .SetIsReadOnly();

            grdEquipmentList.View.AddTextBoxColumn("MODEL", 150);

            grdEquipmentList.View.AddTextBoxColumn("SERIALNO", 120);//시리얼넘버

            grdEquipmentList.View.AddTextBoxColumn("CONTROLMODE", 120)
               .SetTextAlignment(TextAlignment.Center);// ControlMode

            grdEquipmentList.View.AddSpinEditColumn("MINCAPACITY", 110);//최소CAPACITY

            grdEquipmentList.View.AddSpinEditColumn("MAXCAPACITY", 110);//최대 CAPACITY

            grdEquipmentList.View.AddTextBoxColumn("TACTTIME", 110);//TAKT 타임

            grdEquipmentList.View.AddTextBoxColumn("OP", 110);//OP

            grdEquipmentList.View.AddTextBoxColumn("ASSIGNMENTWORKER", 110);// 배정작업자

            grdEquipmentList.View.AddTextBoxColumn("LEADTIME", 110);//리드타임

            grdEquipmentList.View.AddComboBoxColumn("STATE", 80, new SqlQuery("GetEquipmentState", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "STATEID", "STATEID")
                .SetDefault("Idle")
                .SetTextAlignment(TextAlignment.Center);

            grdEquipmentList.View.AddComboBoxColumn("MANAGEMENTSTATE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=EquipmentState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetTextAlignment(TextAlignment.Center);


            grdEquipmentList.View.AddTextBoxColumn("DESCRIPTION", 250);//설비명(v)              


            grdEquipmentList.View.AddTextBoxColumn("EQUIPMENTCHARACTERISTICS", 90);

            grdEquipmentList.View.AddTextBoxColumn("EQUIPMENTCHARACTERISTICSNAME", 120)
                .SetIsReadOnly();

            grdEquipmentList.View.AddTextBoxColumn("ISCONTINUOUSWORK", 80)
                .SetTextAlignment(TextAlignment.Center);



            grdEquipmentList.View.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 100);
            grdEquipmentList.View.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 100);
            grdEquipmentList.View.AddComboBoxColumn("PURCHASETYPE", new SqlQuery("GetCodeList", "00001", "CODECLASSID=PurchaseType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetTextAlignment(TextAlignment.Center);

            grdEquipmentList.View.AddComboBoxColumn("PRODUCTIONTYPE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdEquipmentList.View.AddComboBoxColumn("EQUIPMENTLEVEL", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=EquipmentLevel", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetTextAlignment(TextAlignment.Center);
            grdEquipmentList.View.AddTextBoxColumn("PURCHASECOST", 100)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("{0:#,##0}")
                .SetIsReadOnly();

            grdEquipmentList.View.AddTextBoxColumn("MANUFACTUREDDATE", 250); //제조일

            grdEquipmentList.View.AddTextBoxColumn("INSTALLATIONDATE", 250); //설치일
            grdEquipmentList.View.AddTextBoxColumn("MANUFACTURECOUNTRY", 80);//MANUFACTURECOUNTRY

            grdEquipmentList.View.AddTextBoxColumn("MANUFACTURER", 80);//제작처      
            grdEquipmentList.View.AddTextBoxColumn("VENDORID", 130).SetIsReadOnly(); //거래처
            grdEquipmentList.View.AddTextBoxColumn("VENDORNAME", 130).SetIsReadOnly();


            grdEquipmentList.View.AddTextBoxColumn("TELNO", 100);//TELNO


            grdEquipmentList.View.AddComboBoxColumn("ISKPI", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetTextAlignment(TextAlignment.Center).SetEmptyItem("", "", true);

            grdEquipmentList.View.AddComboBoxColumn("ISCAPA", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetTextAlignment(TextAlignment.Center).SetEmptyItem("", "", true);


            grdEquipmentList.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))//유효상태
                .SetDefault("Valid")
                .SetTextAlignment(TextAlignment.Center);

            grdEquipmentList.View.AddTextBoxColumn("CREATOR")// 생성자
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdEquipmentList.View.AddTextBoxColumn("CREATEDTIME", 130)//생성일
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdEquipmentList.View.AddTextBoxColumn("MODIFIERSTANDARD", 80)      //수정자 
                .SetLabel("MODIFIER")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdEquipmentList.View.AddTextBoxColumn("MODIFIEDTIMESTANDARD", 130) //수정일 
                .SetLabel("MODIFIEDTIME")
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdEquipmentList.View.PopulateColumns();

            #endregion

            #region 설비 등록 탭 설비 
            grdEquipmentClass.GridButtonItem = GridButtonItem.Export | GridButtonItem.Refresh | GridButtonItem.Delete;


            grdEquipmentClass.View.AddTextBoxColumn("ENTERPRISEID")//SITE
               .SetIsHidden();


            grdEquipmentClass.View.AddTextBoxColumn("PLANTID", 70)//설비명(k)
               .SetLabel("SITE")
               .SetIsHidden()
              .SetIsReadOnly();

            grdEquipmentClass.View.AddComboBoxColumn("EQUIPMENTTYPE", 70, new SqlQuery("GetTypeList", "10001", "CODECLASSID=EquipmentType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID");//설비타입


            grdEquipmentClass.View.AddComboBoxColumn("SEPARATOR", 120, new SqlQuery("GetCodeList", "00001", "CODECLASSID=Separator", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))//유효상태
                .SetTextAlignment(TextAlignment.Center)
                .SetValidationKeyColumn();

            grdEquipmentClass.View.AddTextBoxColumn("EQUIPMENTID", 120)//설비명(E)
                .SetIsReadOnly()
               .SetValidationKeyColumn();

            grdEquipmentClass.View.AddLanguageColumn("EQUIPMENTNAME", 250);//설비명(c)

            grdEquipmentClass.View.AddComboBoxColumn("EQUIPMENTCLASSID", 70, new SqlQuery("GetMainEquipment", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "EQUIPMENTCLASSID", "EQUIPMENTCLASSID")
                .SetTextAlignment(TextAlignment.Center);
            grdEquipmentClass.View.AddTextBoxColumn("EQUIPMENTCLASSNAME", 110);

            grdEquipmentClass.View.AddTextBoxColumn("DETAILEQUIPMENTTYPE", 130)
                .SetDefault("Main")
                .SetIsHidden();


            InitializeGrid_AreaListPopup();//작업장id
            grdEquipmentClass.View.AddTextBoxColumn("AREANAME", 210)//작업장명
                .SetIsReadOnly();


            grdEquipmentClass.View.AddTextBoxColumn("MODEL", 150);
            grdEquipmentClass.View.AddTextBoxColumn("SERIALNO", 120);//시리얼넘버
            grdEquipmentClass.View.AddComboBoxColumn("CONTROLMODE", 120, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ControlMode", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetTextAlignment(TextAlignment.Center);//ControlMode

            grdEquipmentClass.View.AddSpinEditColumn("MINCAPACITY", 110);//최소CAPACITY

            grdEquipmentClass.View.AddSpinEditColumn("MAXCAPACITY", 110);//최대 CAPACITY

            grdEquipmentClass.View.AddTextBoxColumn("TACTTIME", 110);//TAKT 타임

            grdEquipmentClass.View.AddTextBoxColumn("OP", 110);//OP

            grdEquipmentClass.View.AddTextBoxColumn("ASSIGNMENTWORKER", 110);// 배정작업자

            grdEquipmentClass.View.AddTextBoxColumn("LEADTIME", 110);//리드타임

            grdEquipmentClass.View.AddComboBoxColumn("STATE", 80, new SqlQuery("GetEquipmentState", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "STATEID", "STATEID")
                .SetDefault("Idle")
                .SetTextAlignment(TextAlignment.Center);

            grdEquipmentClass.View.AddComboBoxColumn("MANAGEMENTSTATE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=EquipmentState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetTextAlignment(TextAlignment.Center);

            grdEquipmentClass.View.AddTextBoxColumn("DESCRIPTION", 250);//설비명(v)        
            
            InitializeGrid_EquipmentCharacteristicsPopup();
            grdEquipmentClass.View.AddTextBoxColumn("EQUIPMENTCHARACTERISTICSNAME", 120);

            grdEquipmentClass.View.AddComboBoxColumn("ISCONTINUOUSWORK", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetTextAlignment(TextAlignment.Center);

            InitializeGrd_ProcessSegmentClassPopup();
            grdEquipmentClass.View.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 100);


            grdEquipmentClass.View.AddComboBoxColumn("PURCHASETYPE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=PurchaseType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
            .SetTextAlignment(TextAlignment.Center);

            grdEquipmentClass.View.AddComboBoxColumn("PRODUCTIONTYPE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));//생산구분

            grdEquipmentClass.View.AddComboBoxColumn("EQUIPMENTLEVEL", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=EquipmentLevel", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
            .SetTextAlignment(TextAlignment.Center); //EQUIPMENTLEVEL

            grdEquipmentClass.View.AddTextBoxColumn("PURCHASECOST", 100)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("{0:#,##0}");

            grdEquipmentClass.View.AddTextBoxColumn("MANUFACTUREDDATE", 250);
            grdEquipmentClass.View.AddTextBoxColumn("INSTALLATIONDATE", 250);


            grdEquipmentClass.View.AddTextBoxColumn("MANUFACTURECOUNTRY", 80);//MANUFACTURECOUNTRY

            grdEquipmentClass.View.AddTextBoxColumn("MANUFACTURER", 80);//제작처                     
            InitializeGrid_VendorListPopup(); //거래처
            grdEquipmentClass.View.AddTextBoxColumn("VENDORNAME", 130).SetIsReadOnly();

            grdEquipmentClass.View.AddTextBoxColumn("TELNO", 100);//TELNO

            grdEquipmentClass.View.AddComboBoxColumn("ISKPI", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
            .SetTextAlignment(TextAlignment.Center).SetEmptyItem("", "", true);

            grdEquipmentClass.View.AddComboBoxColumn("ISCAPA", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
            .SetTextAlignment(TextAlignment.Center).SetEmptyItem("", "", true);

            //grdEquipmentList.View.AddTextBoxColumn("LINKTYPE");//LINKTYPE
            grdEquipmentClass.View.AddTextBoxColumn("FILEYESNO", 70)
                .SetDefault("N")
                .SetIsHidden();


            grdEquipmentClass.View.AddComboBoxColumn("VALIDSTATE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))//유효상태
                .SetDefault("Valid")
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Center);

            grdEquipmentClass.View.AddTextBoxColumn("CREATOR")// 생성자
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdEquipmentClass.View.AddTextBoxColumn("CREATEDTIME", 130)//생성일
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);


            grdEquipmentClass.View.AddTextBoxColumn("MODIFIERSTANDARD", 80)      //수정자 
                .SetLabel("MODIFIER")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdEquipmentClass.View.AddTextBoxColumn("MODIFIEDTIMESTANDARD", 130) //수정일 
                .SetLabel("MODIFIEDTIME")
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdEquipmentClass.View.PopulateColumns();

            #endregion

            #region 설비 등록 탭 설비단

            grdEquipment.View.AddTextBoxColumn("EQUIPMENTCLASSID", 100)
                .SetIsHidden();
            grdEquipment.View.AddTextBoxColumn("PARENTEQUIPMENTID", 100)
              .SetLabel("EQUIPMENTID"); //따로 라벨링해주기

            InitializeGrd_EquimentClassPopup();


            grdEquipment.View.AddLanguageColumn("EQUIPMENTNAME", 150)
                .SetLabel("CHILDEQUIPMENTNAME");

            grdEquipment.View.PopulateColumns();
            #endregion


        }

        /// <summary>
        /// 설비유형 트리 초기화
        /// </summary>
        private void InitializeTreeEquipmentClass()
        {
            treeEquipmentClass.SetResultCount(1);
            treeEquipmentClass.SetIsReadOnly();
            treeEquipmentClass.SetFocusedNode(treeEquipmentClass.FindNodeByFieldValue("NODETYPE", "ENTERPRISE"));

            Dictionary<string, object> param = new Dictionary<string, object>();
            //UserEnterprise  임시
            if (UserInfo.Current.Enterprise.Equals(""))
            {
                treeEquipmentClass.SetEmptyRoot("YPE", "YPE");
                param.Add("p_enterpriseid", "INTERFLEX");
            }
            else
            {
                treeEquipmentClass.SetEmptyRoot(UserInfo.Current.Enterprise, UserInfo.Current.Enterprise);
                param.Add("p_enterpriseid", UserInfo.Current.Enterprise);
            }
            treeEquipmentClass.SetMember("NAME", "ID", "PARENT");

            //param.Add("p_enterpriseid", UserInfo.Current.Enterprise);
            param.Add("p_languagetype", UserInfo.Current.LanguageType);


            treeEquipmentClass.DataSource = SqlExecuter.Query("SelectEquipmentClassManagement_Tree", "10001", param);
            treeEquipmentClass.PopulateColumns();
            treeEquipmentClass.ExpandAll();

            if (_focusedId.Equals(""))
            {
                treeEquipmentClass.SetFocusedNode(treeEquipmentClass.FindNodeByFieldValue("NODETYPE", "ENTERPRISE"));
            }
            else
            {
                treeEquipmentClass.SetFocusedNode(treeEquipmentClass.FindNodeByFieldValue("ID", _focusedId));
            }

        }



        /// <summary>
        /// 거래처 팝업 설비등록 탭
        /// </summary>
        private void InitializeGrid_VendorListPopup()
        {
            var values = Conditions.GetValues();
            string plantId = values["P_PLANTID"].ToString();

            var vendorPopupColumn = grdEquipmentClass.View.AddSelectPopupColumn("VENDORID", new SqlQuery("GetVendorList", "10001"))
                .SetPopupLayout("SELECTVENDORID", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupResultCount(1)
                .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                .SetPopupAutoFillColumns("VENDORNAME")
                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {
                    // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
                    // dataGridRow : 현재 Focus가 있는 그리드의 DataRow
                    foreach (DataRow row in selectedRows)
                    {
                        dataGridRow["VENDORNAME"] = row["VENDORNAME"].ToString();
                    }
                });

            vendorPopupColumn.Conditions.AddComboBox("PLANTID", new SqlQuery("GetPlantList", "10001"), "PLANTNAME", "PLANTID")
                .SetLabel("SITE")
                .SetIsReadOnly()
                .SetDefault(plantId);
            vendorPopupColumn.Conditions.AddTextBox("VENDORID");

            vendorPopupColumn.GridColumns.AddTextBoxColumn("VENDORID", 150);
            vendorPopupColumn.GridColumns.AddTextBoxColumn("VENDORNAME", 250);



        }




        /// <summary>
        /// 설비그룹이 속한 Area만 조회 해 주는 팝업 설비등록 탭
        /// </summary> 
        private void InitializeGrid_AreaListPopup()//작업장id
        {

            //팝업 컬럼 설정 설비등록 탭

            var areaPopupColumnclass = grdEquipmentClass.View.AddSelectPopupColumn("AREAID", 100, new SqlQuery("GetAreaListByEquipmentClassId", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"PLANTID={UserInfo.Current.Plant}"))
                                                    .SetPopupLayout("AREA", PopupButtonStyles.Ok_Cancel, true, false)
                                                    .SetPopupResultCount(1)
                                                    .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                                                    .SetPopupAutoFillColumns("AREANAME")
                                                     .SetPopupApplySelection((selectedRows, dataGridRow) =>
                                                     {

                                                         // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
                                                         // dataGridRow : 현재 Focus가 있는 그리드의 DataRow
                                                         foreach (DataRow row in selectedRows)
                                                         {

                                                             dataGridRow["AREANAME"] = row["AREANAME"].ToString();

                                                         }
                                                     });

            //설비등록 탭
            areaPopupColumnclass.Conditions.AddTextBox("AREA");
            areaPopupColumnclass.Conditions.AddTextBox("EQUIPMENTCLASSID")
                           .SetPopupDefaultByGridColumnId("EQUIPMENTCLASSID")
                           .SetIsHidden();

            areaPopupColumnclass.GridColumns.AddTextBoxColumn("AREAID", 150);
            areaPopupColumnclass.GridColumns.AddTextBoxColumn("AREANAME", 200);
        }


        /// <summary>
        /// 설비특성 선택하는 팝업 설비등록 탭
        /// </summary>
        private void InitializeGrid_EquipmentCharacteristicsPopup()
        {
            //팝업 컬럼 설정
            var parentEqpPopupColumn = grdEquipmentClass.View.AddSelectPopupColumn("EQUIPMENTCHARACTERISTICS", new SqlQuery("GetTypeList", "10001", $"CODECLASSID={"EquipmentCharacteristics"}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                             .SetPopupLayout("EQUIPMENTCHARACTERISTICS", PopupButtonStyles.Ok_Cancel, true, false)
                                             .SetPopupResultCount(1)
                                             .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow)
                                             //.SetPopupAutoFillColumns("EQUIPMENTNAME")
                                             .SetPopupResultMapping("EQUIPMENTCHARACTERISTICS", "CODEID")
                                             .SetValidationCustom(CustomValidation)

                                             .SetPopupApplySelection((selectedRows, dataGridRow) =>
                                             {
                                                 // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
                                                 // dataGridRow : 현재 Focus가 있는 그리드의 DataRow
                                                 foreach (DataRow row in selectedRows)
                                                 {
                                                     dataGridRow["EQUIPMENTCHARACTERISTICSNAME"] = row["CODENAME"].ToString();
                                                 }
                                             });

            //cboStatus.DisplayMember = "CODENAME";
            //cboStatus.ValueMember = "CODEID";

            parentEqpPopupColumn.Conditions.AddTextBox("CODEID");
            parentEqpPopupColumn.Conditions.AddTextBox("CODENAME");

            // 팝업 그리드
            parentEqpPopupColumn.GridColumns.AddTextBoxColumn("CODEID", 100);
            parentEqpPopupColumn.GridColumns.AddTextBoxColumn("CODENAME", 200);
        }

        /// <summary>
        /// 대공정 선택하는 팝업 설비등록 탭
        /// </summary>
        private void InitializeGrd_ProcessSegmentClassPopup()
        {
            //팝업 컬럼 설정
            var ProcessSegmentClassId = grdEquipmentClass.View.AddSelectPopupColumn("PROCESSSEGMENTCLASSID", new SqlQuery("GetProcessSegmentClass", "10001", "PROCESSSEGMENTCLASSTYPE=TopProcessSegmentClass", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                                       .SetPopupLayout("PROCESSSEGMENT", PopupButtonStyles.Ok_Cancel, true, false)
                                                       .SetPopupResultCount(1)
                                                       .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow)
                                                        .SetPopupApplySelection((selectedRows, dataGridRow) =>
                                                        {
                                                            // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
                                                            // dataGridRow : 현재 Focus가 있는 그리드의 DataRow
                                                            foreach (DataRow row in selectedRows)
                                                            {
                                                                dataGridRow["PROCESSSEGMENTCLASSNAME"] = row["PROCESSSEGMENTCLASSNAME"].ToString();
                                                            }
                                                        });
            ;

            //.SetValidationKeyColumn()


            ProcessSegmentClassId.Conditions.AddTextBox("PROCESSSEGMENTCLASS")
                .SetLabel("TOPPROCESSSEGMENTCLASS");


            // 팝업 그리드
            ProcessSegmentClassId.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 150);
            ProcessSegmentClassId.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 200);

        }


        /// <summary>
        /// 설비단 선택하는 팝업
        /// </summary>
        private void InitializeGrd_EquimentClassPopup()
        {

            //팝업 컬럼 설정
            var EquipmentClassId = grdEquipment.View.AddSelectPopupColumn("EQUIPMENTID", 120, new SqlQuery("GetEquipmentClass", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"P_ENTERPRISEID={UserInfo.Current.Enterprise}", $"P_VALIDSTATE={"Valid"}"))
                                                       .SetPopupLayout("CHILDEQUIPMENTID", PopupButtonStyles.Ok_Cancel, true, false)
                                                       .SetPopupResultCount(0)
                                                       .SetLabel("CHILDEQUIPMENTID")
                                                       .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow)
                                                        .SetPopupApplySelection((selectedRows, dataGridRow) =>
                                                        {
                                                            DataTable dt2 = grdEquipment.DataSource as DataTable;
                                                            int handle = grdEquipment.View.FocusedRowHandle;
                                                            DataRow dr = grdEquipmentClass.View.GetFocusedDataRow();

                                                            dt2.Rows.RemoveAt(handle);
                                                            // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
                                                            // dataGridRow : 현재 Focus가 있는 그리드의 DataRow
                                                            foreach (DataRow row in selectedRows)
                                                            {
                                                                DataRow newrow = dt2.NewRow();
                                                                newrow["PARENTEQUIPMENTID"] = dr["EQUIPMENTID"];
                                                                newrow["EQUIPMENTID"] = dr["EQUIPMENTID"].ToString() + row["EQUIPMENTID"].ToString();
                                                                newrow["EQUIPMENTNAME$$KO-KR"] = row["CHILDEQUIPMENTNAME"].ToString();
                                                                newrow["EQUIPMENTNAME$$EN-US"] = row["CHILDEQUIPMENTNAME"].ToString();
                                                                newrow["EQUIPMENTNAME$$ZH-CN"] = row["CHILDEQUIPMENTNAME"].ToString();
                                                                newrow["EQUIPMENTNAME$$VI-VN"] = row["CHILDEQUIPMENTNAME"].ToString();
                                                                dt2.Rows.Add(newrow);
                                                            }
                                                        });


            //.SetValidationKeyColumn()


            EquipmentClassId.Conditions.AddTextBox("CHILDEQUIPMENTIDNAME");

            // 팝업 그리드
            EquipmentClassId.GridColumns.AddTextBoxColumn("EQUIPMENTID", 150)
                .SetLabel("CHILDEQUIPMENTID");
            EquipmentClassId.GridColumns.AddTextBoxColumn("CHILDEQUIPMENTNAME", 200);

        }
        #endregion


        #region Event
        private void InitializeEvent()
        {
            this.grdEquipmentClass.View.KeyUp += View_KeyUp;

            this.grdEquipmentList.View.CellValueChanged += GridView_CellValueChanged;
            this.grdEquipmentList.View.ShowingEditor += GridView_ShowingEditor;
            this.grdEquipment.View.ShowingEditor += View_ShowingEditor;
            this.grdEquipmentClass.View.AddingNewRow += (s, e) =>
            {
                DataRow tree = treeEquipmentClass.GetFocusedDataRow();
                e.NewRow["PLANTID"] = Framework.UserInfo.Current.Plant;
                e.NewRow["ENTERPRISEID"] = Framework.UserInfo.Current.Enterprise;
                e.NewRow["DETAILEQUIPMENTTYPE"] = "Main";
                e.NewRow["EQUIPMENTCLASSID"] = tree["ID"];
                e.NewRow["EQUIPMENTCLASSNAME"] = tree["NAME"];

            };
            this.grdEquipment.View.AddingNewRow += (s, e) =>
            {
                e.NewRow["PARENTEQUIPMENTID"] = grdEquipmentClass.View.GetFocusedDataRow()["EQUIPMENTID"];
            };

            this.grdEquipmentList.View.CellValueChanged += View_CellValueChanged;
            this.treeEquipmentClass.FocusedNodeChanged += TreeEquipmentClass_FocusedNodeChanged;
            this.grdEquipmentClass.View.FocusedRowChanged += View_FocusedRowChanged;
            //new SetGridDeleteButonVisible(grdEquipmentList);
            this.grdEquipmentClass.View.CellValueChanged += View_CellValueChanged1;
            this.picbox.SizeChanged += Picbox_SizeChanged;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_KeyUp(object sender, KeyEventArgs e)
        {
            if (e == null) return;

            if ((e.KeyCode == Keys.V && e.Modifiers == Keys.Control) || e.KeyCode == Keys.V)
            {
                DataTable dt = PasteTable(grdEquipmentClass);
                (grdEquipmentClass.DataSource as DataTable).Merge(dt);
            }
        }


        /// <summary>
        /// key down 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdEquipmentClass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e == null) return;

            if (e.Control || e.KeyCode == Keys.V)
            {
                PasteTable(grdEquipmentClass);
            }
        }

        private string ClipboardData
        {
            get
            {
                IDataObject iData = Clipboard.GetDataObject();
                if (iData == null) return "";

                if (iData.GetDataPresent(DataFormats.Text))
                    return (string)iData.GetData(DataFormats.Text);

                return "";

            }
            set { Clipboard.SetDataObject(value); }
        }

        public DataTable PasteTable(SmartBandedGrid grid)
        {
            DataTable tbl = new DataTable();

            DataTable targetGrid = grid.DataSource as DataTable;


            foreach (DataColumn item in targetGrid.Columns)
            {
                tbl.Columns.Add(item.ColumnName).DataType = item.DataType;
            }

            tbl = targetGrid.Clone();

            string[] data = ClipboardData.Split('\n');

            if (data.Length < 1) return tbl;

            foreach (string row in data)
            {
                AddRow(tbl, row);
            }
            return tbl;

        }

        private void AddRow(DataTable tbl, string data)
        {
            object[] rowData = data.Split(new char[] { '\r', '\x09' });

            DataRow newRow = tbl.NewRow();
            for (int i = 0; i < tbl.Columns.Count; i++)
            {
                if (i <= rowData.Length) break;

                newRow[i] = Convert.ChangeType(rowData[i], tbl.Columns[i].DataType);
            }
            tbl.Rows.Add(newRow);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CellValueChanged1(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (!e.Column.FieldName.Equals("EQUIPMENTCLASSID"))
            {
                return;
            }

            if (grdEquipmentClass.View.GetDataRow(e.RowHandle) is DataRow row)
            {
                if (SqlExecuter.Query("GetMainEquipment", "10001", new Dictionary<string, object> { { "LANGUAGETYPE", UserInfo.Current.LanguageType } }) is DataTable dt)
                {
                    foreach (DataRow dtRow in dt.Rows) //코드에서 고른 설비 그룹ID와 쿼리에서 불러온 설비그룹ID가 일치할때 그 쿼리의 그룹NAME을 찾아서 넣는다
                    {

                        if (dtRow["EQUIPMENTCLASSID"].Equals(Format.GetString(row["EQUIPMENTCLASSID"])))
                        {
                            row["EQUIPMENTCLASSNAME"] = dtRow["EQUIPMENTCLASSNAME"];
                            break;
                        }
                    }
                }
            }
        }


        /// <summary>
        /// 사진 박스 사이즈 조절마다 사진 사이즈 조절
        /// </summary>
        private void Picbox_SizeChanged(object sender, EventArgs e)
        {

            equmentclass_FocusedFileChanged();
            int width = (int)(picbox.Size.Width * 0.9);
            int height = (int)(picbox.Size.Height * 0.9);
            Size resize = new Size(width, height);
            Bitmap resizeImage = new Bitmap(picbox.Image, resize);
            picbox.Image = resizeImage;
        }


        private void View_ShowingEditor(object sender, CancelEventArgs e)
        {
            DataRow dr = grdEquipment.View.GetFocusedDataRow();
            DataRowState state = dr.RowState;

            if (state.ToString().Equals("Unchanged"))
            {
                e.Cancel = true;
            }
        }

 


        private void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            equmentclass_FocusedNodeChanged();
            equmentclass_FocusedFileChanged();
        }

        /// <summary>
        /// 설비그룹값을 바꾸면 설비타입을 설비그룹타입으로 입력해주는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName.Equals("EQUIPMENTCLASSID"))
            {
                if (e.Column.ColumnEdit is RepositoryItemLookUpEdit lookup)
                {
                    object equipmentType = lookup.GetDataSourceValue("EQUIPMENTTYPE", lookup.GetDataSourceRowIndex("EQUIPMENTCLASSID", e.Value));

                    grdEquipmentList.View.SetFocusedRowCellValue("EQUIPMENTTYPE", equipmentType);
                }
            }

        }


        /// <summary>
        /// 설비그룹의 포커스가 바뀔때 설비 그룹 재조회하는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeEquipmentClass_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            equmentclass_TreeNodeFocused();
        }



        /// <summary>
        /// DetailEquipmentType이 Main일 경우 상위 설비ID 선택 불가 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridView_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (grdEquipmentList.View.FocusedColumn.FieldName == "PARENTEQUIPMENTID")
            {
                var detailEquipmentType = this.grdEquipmentList.View.GetRowCellValue(grdEquipmentList.View.FocusedRowHandle, "DETAILEQUIPMENTTYPE");

                if (detailEquipmentType != null && detailEquipmentType.ToString() == "Main")
                {
                    this.ShowMessage("CantSelectMainEqpParent");
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// DetailEquipmentType이 Main으로 설정될 경우 상위 설비ID를 "" 로 설정해주는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "DETAILEQUIPMENTTYPE")
            {
                if (e.Value != null && e.Value.ToString() == "Main")
                {
                    this.grdEquipmentList.View.SetRowCellValue(e.RowHandle, "PARENTEQUIPMENTID", "");
                }
            }
            else if (e.Column.FieldName == "PLANTID")
            {

                this.grdEquipmentList.View.SetRowCellValue(e.RowHandle, "AREAID", "");
                this.grdEquipmentList.View.SetRowCellValue(e.RowHandle, "AREANAME", "");

            }
        }

        /// <summary>
        /// DetailEquipmentType이 Sub인 경우 상위설비 필수입력 유효성 검사
        /// </summary>
        /// <param name="rowHandle"></param>
        /// <returns></returns>
        private IEnumerable<ValidationResult> CustomValidation(int rowHandle)
        {
            var currentRow = grdEquipmentList.View.GetDataRow(rowHandle);

            List<ValidationResult> result = new List<ValidationResult>();

            if (!string.IsNullOrWhiteSpace(currentRow.ToStringNullToEmpty("DETAILEQUIPMENTTYPE")) &&
                currentRow["DETAILEQUIPMENTTYPE"].ToString().Equals("Sub"))
            {
                ValidationResult resultLsl = new ValidationResult();

                resultLsl.Caption = Language.Get("PARENTEQUIPMENTID"); //위반한 컬럼의 다국어
                resultLsl.FailMessage = Language.GetMessage("ParentEquipmentRequire").Message;
                resultLsl.Id = "PARENTEQUIPMENTID";

                if (string.IsNullOrWhiteSpace(currentRow["PARENTEQUIPMENTID"].ToString()))
                {
                    resultLsl.IsSucced = false;
                }

                result.Add(resultLsl);
            }

            return result;
        }

        #endregion

        #region 툴바


        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            base.OnToolbarClick(sender, e);
            SmartButton btn = sender as SmartButton;
            if (smartTabControl1.SelectedTabPageIndex == 1)
            {
                if (btn.Name.ToString().Equals("ImageAdd"))
                {
                    try
                    {
                        DataRow row = grdEquipmentClass.View.GetFocusedDataRow();
                        if (row == null) return;

                        DialogManager.ShowWaitArea(this);

                        string imageFile = string.Empty;

                        OpenFileDialog dialog = new OpenFileDialog
                        {
                            Multiselect = false,
                            Filter = "Image Files (*.bmp, *.jpg, *.jpeg, *.png)|*.BMP;*.JPG;*.JPEG;*.PNG",
                            FilterIndex = 0
                        };

                        if (dialog.ShowDialog() == DialogResult.OK)
                        {

                            imageFile = dialog.FileName;
                            FileInfo fileInfo = new FileInfo(dialog.FileName);
                            using (FileStream fs = new FileStream(dialog.FileName, FileMode.Open, FileAccess.Read))
                            {
                                int width = 0;
                                int height = 0;
                                Size resize;
                                Bitmap resizeImage;
                                byte[] data = new byte[fileInfo.Length];
                                fs.Read(data, 0, (int)fileInfo.Length);

                                MemoryStream ms = new MemoryStream(Convert.FromBase64String(Convert.ToBase64String(data).ToString()));

                                Byte[] image = null;

                                if (picbox.EditValue != null)
                                {
                                    width = (int)(picbox.Width * 0.9);
                                    height = (int)(picbox.Height * 0.9);
                                    resize = new Size(width, height);
                                    resizeImage = new Bitmap(Image.FromStream(ms), resize);
                                    image = (byte[])new ImageConverter().ConvertTo(new Bitmap(resizeImage), typeof(byte[]));
                                    picbox.Image = resizeImage;
                                }

                                string ruleName = "SaveEquipmentFileImage";
                                MessageWorker worker = new MessageWorker(ruleName);
                                worker.SetBody(new MessageBody()
                    {
                    { "image" , image },
                    { "equipmentid",row["EQUIPMENTID"] }
                    });
                                worker.Execute();
                                this.ShowMessage("SuccessSave");
                                equmentclass_TreeNodeFocused();

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw Framework.MessageException.Create(ex.ToString());
                    }
                    finally
                    {
                        DialogManager.CloseWaitArea(this);
                    }


                }
                else if (btn.Name.ToString().Equals("ImageDelete"))
                {

                    DataRow equipmentclassinfo = grdEquipmentClass.View.GetFocusedDataRow();




                    if (equipmentclassinfo == null)
                        return;

                    string ruleName = "SaveEquipmentFileImage";
                    Byte[] image = null;

                    if (picbox.EditValue != null)
                    {
                        image = (byte[])new ImageConverter().ConvertTo(new Bitmap(picbox.Image), typeof(byte[]));
                    }


                    MessageWorker worker = new MessageWorker(ruleName);
                    worker.SetBody(new MessageBody()
                    {
                    { "equipmentid",equipmentclassinfo["EQUIPMENTID"] }
                    });
                    worker.Execute();
                    this.ShowMessage("SuccessSave");
                    equmentclass_TreeNodeFocused();
                    picbox.Image = Properties.Resources.defalut;

                }
            }
        }
        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();
            DataTable equipmentclasschanged = grdEquipmentClass.GetChangedRows();
            DataRow equipmentclassinfo = grdEquipmentClass.View.GetFocusedDataRow();
            DataTable equipmentchanged = grdEquipment.GetChangedRows();
            grdEquipment.View.ShowEditor();
            DataTable dtItemserialI = new DataTable();
            // 채번 시리얼 존재 유무 체크




            if (equipmentclasschanged.Rows.Count > 0)
            {
                MessageWorker worker = new MessageWorker("SaveEquipmentManagement");
                worker.SetBody(new MessageBody()
                {
                    { "list", equipmentclasschanged },

                });
                worker.Execute();
                equmentclass_FocusedNodeChanged();
                equmentclass_TreeNodeFocused();
            }
            if (equipmentchanged.Rows.Count > 0 && equipmentclassinfo != null)
            {
                string ruleName = "SaveEquipmentClass";
                string tableName = "list";
                equipmentchanged.TableName = tableName;

                MessageWorker worker = new MessageWorker(ruleName);
                worker.SetBody(new MessageBody()
                    {
                    { "enterpriseId", UserInfo.Current.Enterprise },
                    { "plantId", UserInfo.Current.Plant },
                    { tableName, equipmentchanged },
                    { "DETAILEQUIPMENTTYPE" , "Sub"},
                    {  "PARENTEQUIPMENTID",  equipmentclassinfo["EQUIPMENTID"]},
                    { "ValidState", "Valid" }
                    });
                worker.Execute();
                equmentclass_FocusedNodeChanged();


            }






        }



        #endregion

        #region 검색
        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();
            int page = smartTabControl1.SelectedTabPageIndex;
            if (page == 0)
            {
                var values = Conditions.GetValues();
                values.Add("P_LANGUAGETYPE", UserInfo.Current.LanguageType);

                //DataTable dtEquipmentClasstList = await SqlExecuter.QueryAsync("SelectEquipmentManagement", "10001", values);
                DataTable dtEquipmentClasstList = await SqlExecuter.QueryAsync("SelectEquipmentManagement2", "10002", values);

                if (dtEquipmentClasstList.Rows.Count < 1)
                {
                    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                }

                grdEquipmentList.DataSource = dtEquipmentClasstList;
            }
            else
            {
                equmentclass_TreeNodeFocused();
                equmentclass_FocusedNodeChanged();
                equmentclass_FocusedFileChanged();
            }
        }
        #endregion

        #region 유효성 검사
        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            grdEquipmentList.View.CheckValidation();

            DataTable equipmentclasschanged = grdEquipmentClass.GetChangedRows();
            DataTable equipmentchanged = grdEquipment.GetChangedRows();
            if (equipmentclasschanged.Rows.Count == 0 && equipmentchanged.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");

            }


            if (equipmentclasschanged.Rows.Count > 0)
            {
                foreach (DataRow row in equipmentclasschanged.Rows)
                {
                    if (row["DETAILEQUIPMENTTYPE"].ToString() == "Main" && row["AREAID"].ToString() == "")
                    {
                        throw MessageException.Create("YN_AREAID");
                    }
                }
            }

        }

        #endregion

        #region Private Function





        private void equmentclass_FocusedNodeChanged()
        {

            DataRow focusRow = grdEquipmentClass.View.GetFocusedDataRow();
            if (focusRow == null)
            {
                this.grdEquipment.DataSource = null;
                return;
            }
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("P_EQUIPMENTID", focusRow["EQUIPMENTID"].ToString()); // 
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            this.grdEquipment.DataSource = SqlExecuter.Query("GetEquimentClass", "10001", param);

        }
        private void equmentclass_FocusedFileChanged() //설비에서 포커스체인지 이벤트 발생할경우
        {


            DataRow dr = grdEquipmentClass.View.GetFocusedDataRow(); //설비그리드의 포커스된 Row를 가져옴
            if (dr != null) //이 dr이 null이 아닐경우에
            {
                if (dr["FILEYESNO"].ToString().Equals("N") || dr["FILEYESNO"].ToString().Equals("")) //dr의 FILEYESNO의 문자열이 N이거나 공백일 경우
                {
                    picbox.Image = Properties.Resources.defalut; //이미지박스.이름 = 속성 고정값

                }
                else
                {
                    ImageConverter converter = new ImageConverter();
                    Dictionary<string, object> values = new Dictionary<string, object>()
                {
                    {"EQUIPMENTID", dr["EQUIPMENTID"] }

                };
                    DataTable dt = SqlExecuter.Query("GetEquipmentClassFile", "10001", values);

                    int width = (int)(picbox.Width * 0.9);
                    int height = (int)(picbox.Height * 0.9);
                    Size resize = new Size(width, height);

                    Bitmap resizeImage = new Bitmap((Image)converter.ConvertFrom(dt.Rows[0]["IMAGE"]), resize);
                    picbox.Image = resizeImage;


                }
            }
            else
            {
                picbox.Image = Properties.Resources.defalut;
            }


        }
        private void equmentclass_FisrtNodeSeleted()
        {

            DataTable dt = grdEquipmentClass.DataSource as DataTable;
            if (dt.Rows.Count < 1)
            {
                this.grdEquipment.DataSource = null;
                return;
            }
            DataRow focusRow = dt.Rows[0];

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("P_EQUIPMENTID", focusRow["EQUIPMENTID"].ToString()); // 
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            this.grdEquipment.DataSource = SqlExecuter.Query("GetEquimentClass", "10001", param);

        }


        private void equmentclass_TreeNodeFocused()
        {



            if (treeEquipmentClass.GetFocusedDataRow() is DataRow focusRow)
            {

                var param = Conditions.GetValues();


                //UserEnterprise 설정안되어있어서 임의로 지정 나중에 삭제 임시
                param.Add("p_enterpriseid", UserInfo.Current.Enterprise ?? "INTERFLEX");
                param.Add("p_plantid", UserInfo.Current.Plant);
                param.Add("p_languagetype", UserInfo.Current.LanguageType);
                grdEquipmentClass.GridButtonItem = GridButtonItem.All;
                grdEquipmentClass.GridButtonItem -= GridButtonItem.Add;


                if (focusRow["NODETYPE"].ToString().Equals("MC"))
                {

                    param.Add("p_parentequipmentclassid", focusRow["ID"]);
                    param.Add("p_grouptype", "");
                    //this.grdEquipmentClass.DataSource = SqlExecuter.Query("SelectEquipmentClass", "10003", param);
                    this.grdEquipmentClass.DataSource = SqlExecuter.Query("SelectEquipmentClass5", "10005", param);
                    equmentclass_FisrtNodeSeleted();
                    equmentclass_FocusedFileChanged();
                }
                else if (focusRow["NODETYPE"].ToString().Equals("SC") || focusRow["NODETYPE"].ToString().Equals("ENTERPRISE"))
                {
                    grdEquipmentClass.GridButtonItem = GridButtonItem.All;
                    param.Add("P_EQUIPMENTCLASS", focusRow["ID"]);
                    param.Add("EQUIPMENTCLASSTYPE", focusRow["EQUIPMENTCLASSTYPE"]);

                    //this.grdEquipmentClass.DataSource = SqlExecuter.Query("SelectEquipmentClass", "10002", param);
                    this.grdEquipmentClass.DataSource = SqlExecuter.Query("SelectEquipmentClass4", "10004", param);
                    equmentclass_FisrtNodeSeleted();
                    equmentclass_FocusedFileChanged();
                }
                else
                {
                    grdEquipmentClass.DataSource = null;
                    grdEquipment.DataSource = null;
                    picbox.EditValue = null;
                }
            }

        }





        private void pnlToolbar_Paint(object sender, PaintEventArgs e)
        {

        }
        #endregion
    }
}


