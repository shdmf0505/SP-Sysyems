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

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프 로 그 램 명  : 기준정보 > 설비기준정보 > 유틸리티사양
    /// 업  무  설  명  : 
    /// 생    성    자  : 신상철
    /// 생    성    일  : 2019-12-05
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class EquipmentUtilitySpec : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가

        #endregion

        #region 생성자

        public EquipmentUtilitySpec()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

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

        /// <summary>        
        /// 코드그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            grdEquipmentSpec.GridButtonItem = GridButtonItem.Export | GridButtonItem.Import;
            //grdEquipmentSpec.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            var groupDefaultCol = grdEquipmentSpec.View.AddGroupColumn("");

            //grdEquipmentSpec.View.AddTextBoxColumn("DESCRIPTION", 250);

            groupDefaultCol.AddTextBoxColumn("EQUIPMENTID", 80)
                .SetIsReadOnly().SetTextAlignment(TextAlignment.Left);
            groupDefaultCol.AddTextBoxColumn("EQUIPMENTNAME", 120)
                .SetIsReadOnly().SetTextAlignment(TextAlignment.Left);
            groupDefaultCol.AddTextBoxColumn("PLANTNAME", 80)
                .SetIsReadOnly().SetTextAlignment(TextAlignment.Left);
            groupDefaultCol.AddTextBoxColumn("EQUIPMENTCLASSNAME", 80)
                .SetIsReadOnly().SetTextAlignment(TextAlignment.Left);
            groupDefaultCol.AddTextBoxColumn("AREANAME", 120)
                .SetIsReadOnly().SetTextAlignment(TextAlignment.Left);

            //[장비 제원]
            var groupEqpSpecCol = grdEquipmentSpec.View.AddGroupColumn("EQUIPMENTSPECIFICATION");

            //-장비 Size
            var groupEqpSizeCol = groupEqpSpecCol.AddGroupColumn("EQUIPMENTSIZE");
            //폭W
            groupEqpSizeCol.AddSpinEditColumn("WIDTH", 55).SetLabel("W")
                .SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);
            //길이L
            groupEqpSizeCol.AddSpinEditColumn("LENGTH", 55).SetLabel("L")
                .SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);
            //높이H
            groupEqpSizeCol.AddSpinEditColumn("HEIGHT", 55).SetLabel("H")
                .SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);

            //-중량
            var groupWeightCol = groupEqpSpecCol.AddGroupColumn("TON");
            //TON
            groupWeightCol.AddSpinEditColumn("WEIGHT", 55).SetLabel("TON")
                .SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);

            //-면적
            var groupSizeCol = groupEqpSpecCol.AddGroupColumn("EXTENT");
            //㎡
            groupSizeCol.AddSpinEditColumn("SIZE", 55).SetLabel("㎡")
                .SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);


            //[전기]
            var groupElectricCol = grdEquipmentSpec.View.AddGroupColumn("ELECTRIC");

            //-상
            var groupPhaseCol = groupElectricCol.AddGroupColumn("PHASE");
            //P
            groupPhaseCol.AddSpinEditColumn("VOLTAGE", 55).SetLabel("P")
                .SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);
            //-전원
            var groupPowerCol = groupElectricCol.AddGroupColumn("POWER");
            //V
            groupPowerCol.AddSpinEditColumn("POWER", 55).SetLabel("V")
                .SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);
            //-전기용량
            var groupCapacityCol = groupElectricCol.AddGroupColumn("CAPACITANCE");
            //㎾
            groupCapacityCol.AddSpinEditColumn("CAPACITY", 55).SetLabel("㎾")
                .SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);


            //[AIR]
            var groupAirSpecCol = grdEquipmentSpec.View.AddGroupColumn("AIR");

            //-사용량
            var groupAirUsageCol = groupAirSpecCol.AddGroupColumn("USAGE");
            //㎥/Min
            groupAirUsageCol.AddSpinEditColumn("AIRUSAGE", 55).SetLabel("㎥/Min")
                .SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);
            //-압력
            var groupAirPressureCol = groupAirSpecCol.AddGroupColumn("PRESSURE");
            //㎥/Min
            groupAirPressureCol.AddSpinEditColumn("AIRPRESSURE", 55).SetLabel("㎏/㎠")
                .SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);
            //-배관
            var groupAirPipeCol = groupAirSpecCol.AddGroupColumn("PIPE");
            //㎾
            groupAirPipeCol.AddSpinEditColumn("AIRPIPING", 55).SetLabel("A")
                .SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);


            //[냉각수(PCW)]
            var groupCoolantPCWCol = grdEquipmentSpec.View.AddGroupColumn("COOLANTPCW");
            

            //-압력
            var groupPCWPressureCol = groupCoolantPCWCol.AddGroupColumn("PRESSURE");
            //㎏/㎠
            groupPCWPressureCol.AddSpinEditColumn("PCW_PRESSURE", 55).SetLabel("㎏/㎠")
                .SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);
            //-온도
            var groupPCWTemprangeCol = groupCoolantPCWCol.AddGroupColumn("TEMP");
            //℃
            groupPCWTemprangeCol.AddSpinEditColumn("PCW_TEMPRANGE", 55).SetLabel("℃")
                .SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);
            //-냉각열량
            var groupPCWCoolingheatCol = groupCoolantPCWCol.AddGroupColumn("COOLINGHEAT");

            groupPCWCoolingheatCol.AddSpinEditColumn("PCW_COOLINGHEAT", 55).SetLabel("㎉")
                .SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);
            //-배관
            var groupPCWPipeCol = groupCoolantPCWCol.AddGroupColumn("PIPE");
            //A
            groupPCWPipeCol.AddSpinEditColumn("PCW_PIPING", 55).SetLabel("A")
                .SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);


            //[냉각수(공조기용)]
            var groupCoolantCHWCol = grdEquipmentSpec.View.AddGroupColumn("COOLANTCHW");


            //-압력
            var groupCHWPressureCol = groupCoolantCHWCol.AddGroupColumn("PRESSURE");
            //㎏/㎠
            groupCHWPressureCol.AddSpinEditColumn("CHW_PRESSURE", 55).SetLabel("㎏/㎠")
                .SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);
            //-온도
            var groupCHWTemprangeCol = groupCoolantCHWCol.AddGroupColumn("TEMP");
            //℃
            groupCHWTemprangeCol.AddSpinEditColumn("CHW_TEMPRANGE", 55).SetLabel("℃")
                .SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);
            //-냉각열량
            var groupCHWCoolingheatCol = groupCoolantCHWCol.AddGroupColumn("COOLINGHEAT");

            groupCHWCoolingheatCol.AddSpinEditColumn("CHW_COOLINGHEAT", 55).SetLabel("㎉")
                .SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);
            //-배관
            var groupCHWPipingCol = groupCoolantCHWCol.AddGroupColumn("PIPE");
            //A
            groupCHWPipingCol.AddSpinEditColumn("CHW_PIPING", 55).SetLabel("A")
                .SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);


            //[스크러버]
            var groupScrubberCol = grdEquipmentSpec.View.AddGroupColumn("SCRUBBER");

            //-사용량
            var groupScrubberUsageCol = groupScrubberCol.AddGroupColumn("USAGE");
            //P
            groupScrubberUsageCol.AddSpinEditColumn("SCRUBBER_USAGE", 55).SetLabel("㎥/Min")
                .SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);
            //-배관
            var groupScrubberPipeCol = groupScrubberCol.AddGroupColumn("PIPE");
            //V
            groupScrubberPipeCol.AddSpinEditColumn("SCRUBBER_PIPING", 55).SetLabel("A")
                .SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);


            //[스크러버(HCL)]
            var groupScrubberHCLCol = grdEquipmentSpec.View.AddGroupColumn("SCRUBBERHCL");

            //-사용량
            var groupHCLUsageCol = groupScrubberHCLCol.AddGroupColumn("USAGE");
            //P
            groupHCLUsageCol.AddSpinEditColumn("HCI_USAGE", 55).SetLabel("㎥/day")
                .SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);
            //-배관
            var groupHCLPipeCol = groupScrubberHCLCol.AddGroupColumn("PIPE");
            //V
            groupHCLPipeCol.AddSpinEditColumn("HCI_PIPING", 55).SetLabel("A")
                .SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);


            //[스크러버(AC)]
            var groupScrubberACCol = grdEquipmentSpec.View.AddGroupColumn("SCRUBBERAC");

            //-사용량
            var groupACUsageCol = groupScrubberACCol.AddGroupColumn("USAGE");
            //P
            groupACUsageCol.AddSpinEditColumn("AC_USAGE", 55).SetLabel("㎥/day")
                .SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);
            //-배관
            var groupACPipeCol = groupScrubberACCol.AddGroupColumn("PIPE");
            //V
            groupACPipeCol.AddSpinEditColumn("AC_PIPING", 55).SetLabel("A")
                .SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);


            //[흡착탑]
            var groupAdsorptionCol = grdEquipmentSpec.View.AddGroupColumn("ADSORPTION");

            //-사용량
            var groupAdsorptionUsageCol = groupAdsorptionCol.AddGroupColumn("USAGE");
            //P
            groupAdsorptionUsageCol.AddSpinEditColumn("ADSORPTIONTOWER_USAGE", 55).SetLabel("㎥/Min")
                .SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);
            //-배관
            var groupAdsorptionPipeCol = groupAdsorptionCol.AddGroupColumn("PIPE");
            //V
            groupAdsorptionPipeCol.AddSpinEditColumn("ADSORPTIONTOWER_PIPING", 55).SetLabel("A")
                .SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);


            //[열배기]
            var groupHeatCol = grdEquipmentSpec.View.AddGroupColumn("HEAT");

            //-사용량
            var groupHeatUsageCol = groupHeatCol.AddGroupColumn("USAGE");
            //P
            groupHeatUsageCol.AddSpinEditColumn("HEAT_USAGE", 55).SetLabel("㎥/Min")
                .SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);
            //-배관
            var groupHeatPipeCol = groupHeatCol.AddGroupColumn("PIPE");
            //V
            groupHeatPipeCol.AddSpinEditColumn("HEAT_PIPING", 55).SetLabel("A")
                .SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);

            //[집진기]
            var groupDustCol = grdEquipmentSpec.View.AddGroupColumn("DUST");

            //-사용량
            var groupDustUsageCol = groupDustCol.AddGroupColumn("USAGE");
            //P
            groupDustUsageCol.AddSpinEditColumn("DUST_USAGE", 55).SetLabel("㎥/Min")
                .SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);
            //-배관
            var groupDustPipeCol = groupDustCol.AddGroupColumn("PIPE");
            //V
            groupDustPipeCol.AddSpinEditColumn("DUST_PIPING", 55).SetLabel("A")
                .SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);

            //[페수(ACID)]
            var groupWasteWaterACIDCol = grdEquipmentSpec.View.AddGroupColumn("WASTEWATERACID");

            //-사용량
            var groupACIDUsageCol = groupWasteWaterACIDCol.AddGroupColumn("USAGE");
            //P
            groupACIDUsageCol.AddSpinEditColumn("ACID_USAGE", 55).SetLabel("㎥/Min")
                .SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);
            //-배관
            var groupACIDPipeCol = groupWasteWaterACIDCol.AddGroupColumn("PIPE");
            //V
            groupACIDPipeCol.AddSpinEditColumn("ACID_PIPING", 55).SetLabel("A")
                .SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);

            //[페수(ALK)]
            var groupWasteWaterALKCol = grdEquipmentSpec.View.AddGroupColumn("WASTEWATERALK");

            //-사용량
            var groupALKUsageCol = groupWasteWaterALKCol.AddGroupColumn("USAGE");
            //P
            groupALKUsageCol.AddSpinEditColumn("ALK_USAGE", 55).SetLabel("㎥/Min")
                .SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);
            //-배관
            var groupALKPipeCol = groupWasteWaterALKCol.AddGroupColumn("PIPE");
            //V
            groupALKPipeCol.AddSpinEditColumn("ALK_PIPING", 55).SetLabel("A")
                .SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);

            //[페수(일반)]
            var groupWasteWaterGeneralCol = grdEquipmentSpec.View.AddGroupColumn("WASTEWATERGENERAL");

            //-사용량
            var groupGenaralUsageCol = groupWasteWaterGeneralCol.AddGroupColumn("USAGE");
            //P
            groupGenaralUsageCol.AddSpinEditColumn("GENERAL_USAGE", 55).SetLabel("㎥/Min")
                .SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);
            //-배관
            var groupGenaralPipeCol = groupWasteWaterGeneralCol.AddGroupColumn("PIPE");
            //V
            groupGenaralPipeCol.AddSpinEditColumn("GENERAL_PIPING", 55).SetLabel("A")
                .SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);


            //[DI WATER]
            var groupDIWaterCol = grdEquipmentSpec.View.AddGroupColumn("DIWATER");

            //-사용량
            var groupDIUsageCol = groupDIWaterCol.AddGroupColumn("USAGE");
            //P
            groupDIUsageCol.AddSpinEditColumn("DIWATER_USAGE", 55).SetLabel("ℓ/Min")
                .SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);
            //-압력
            var groupDIPressureCol = groupDIWaterCol.AddGroupColumn("PRESSURE");
            //V
            groupDIPressureCol.AddSpinEditColumn("DIWATER_PRESSURE", 55).SetLabel("㎏/㎠")
                .SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);
            //-온도
            var groupDITempCol = groupDIWaterCol.AddGroupColumn("TEMP");
            //V
            groupDITempCol.AddSpinEditColumn("DIWATER_TEMPRANGE", 55).SetLabel("℃")
                .SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);
            //-배관
            var groupDIPipeCol = groupDIWaterCol.AddGroupColumn("PIPE");
            //V
            groupDIPipeCol.AddSpinEditColumn("DIWATER_PIPING", 55).SetLabel("A")
                .SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);


            //[HOT DI WATER]
            var groupHotDIWaterCol = grdEquipmentSpec.View.AddGroupColumn("HOTDIWATER");

            //-사용량
            var groupHotDIUsageCol = groupHotDIWaterCol.AddGroupColumn("USAGE");
            //P
            groupHotDIUsageCol.AddSpinEditColumn("HOTWATER_USAGE", 55).SetLabel("ℓ/Min")
                .SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);
            //-압력
            var groupHotDIPressureCol = groupHotDIWaterCol.AddGroupColumn("PRESSURE");
            //V
            groupHotDIPressureCol.AddSpinEditColumn("HOTWATER_PRESSURE", 55).SetLabel("㎏/㎠")
                .SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);
            //-온도
            var groupHotDITempCol = groupHotDIWaterCol.AddGroupColumn("TEMP");
            //V
            groupHotDITempCol.AddSpinEditColumn("HOTWATER_TEMPRANGE", 55).SetLabel("℃")
                .SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);
            //-배관
            var groupHotDIPipeCol = groupHotDIWaterCol.AddGroupColumn("PIPE");
            //V
            groupHotDIPipeCol.AddSpinEditColumn("HOTWATER_PIPING", 55).SetLabel("A")
                .SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);


            //[용수]
            var groupWaterCol = grdEquipmentSpec.View.AddGroupColumn("WATER");

            //-사용량
            var groupWaterUsageCol = groupWaterCol.AddGroupColumn("USAGE");
            //P
            groupWaterUsageCol.AddSpinEditColumn("WATER_USAGE", 55).SetLabel("ℓ/Min")
                .SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);
            //-압력
            var groupWaterPressureCol = groupWaterCol.AddGroupColumn("PRESSURE");
            //V
            groupWaterPressureCol.AddSpinEditColumn("WATER_PRESSURE", 55).SetLabel("㎏/㎠")
                .SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);
            //-배관
            var groupWaterPipeCol = groupWaterCol.AddGroupColumn("PIPE");
            //V
            groupWaterPipeCol.AddSpinEditColumn("WATER_PIPING", 55).SetLabel("A")
                .SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);


            //[스팀]
            var groupSteamCol = grdEquipmentSpec.View.AddGroupColumn("STEAM");

            //-사용량
            var groupSteamUsageCol = groupSteamCol.AddGroupColumn("USAGE");
            //P
            groupSteamUsageCol.AddSpinEditColumn("STEAM_USAGE", 55).SetLabel("ℓ/Min")
                .SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);
            //-압력
            var groupSteamPressureCol = groupSteamCol.AddGroupColumn("PRESSURE");
            //V
            groupSteamPressureCol.AddSpinEditColumn("STEAM_PRESSURE", 55).SetLabel("㎏/㎠")
                .SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);
            //-배관
            var groupSteamPipeCol = groupSteamCol.AddGroupColumn("PIPE");
            //V
            groupSteamPipeCol.AddSpinEditColumn("STEAM_PIPING", 55).SetLabel("A")
                .SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);


            //[열매체]
            var groupHeatingMediumCol = grdEquipmentSpec.View.AddGroupColumn("HEATINGMEDIUM");

            //-사용량
            var groupHeatingMediumUsageCol = groupHeatingMediumCol.AddGroupColumn("USAGE");
            //P
            groupHeatingMediumUsageCol.AddSpinEditColumn("HEATINGMEDIUM_USAGE", 55).SetLabel("ℓ/Min")
                .SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);
            //-압력
            var groupHeatingMediumPressureCol = groupHeatingMediumCol.AddGroupColumn("PRESSURE");
            //V
            groupHeatingMediumPressureCol.AddSpinEditColumn("HEATINGMEDIUM_PRESSURE", 55).SetLabel("㎏/㎠")
                .SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);
            //-온도
            var groupHeatingMediumTempCol = groupHeatingMediumCol.AddGroupColumn("TEMP");
            //V
            groupHeatingMediumTempCol.AddSpinEditColumn("HEATINGMEDIUM_TEMPRANGE", 55).SetLabel("℃")
                .SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);
            //-배관
            var groupHeatingMediumPipeCol = groupHeatingMediumCol.AddGroupColumn("PIPE");
            //V
            groupHeatingMediumPipeCol.AddSpinEditColumn("HEATINGMEDIUM_PIPING", 55).SetLabel("A")
                .SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);


            //[공정조건]
            var groupProcessCol = grdEquipmentSpec.View.AddGroupColumn("PROCESSCONDITION");

            //-조도
            var groupProcessIlluminanceCol = groupProcessCol.AddGroupColumn("ILLUMINANCE");
            //P
            groupProcessIlluminanceCol.AddSpinEditColumn("ILLUMINANCE", 55).SetLabel("Lux")
                .SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);
            //-온도
            var groupProcessTempCol = groupProcessCol.AddGroupColumn("TEMP");
            //V
            groupProcessTempCol.AddSpinEditColumn("TEMPERATURE", 55).SetLabel("℃")
                .SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);
            //-습도
            var groupProcessHumidityCol = groupProcessCol.AddGroupColumn("HUMIDITY");
            //V
            groupProcessHumidityCol.AddSpinEditColumn("HUMIDITY", 55).SetLabel("%")
                .SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);
            //-파티클
            var groupProcessParticleCol = groupProcessCol.AddGroupColumn("PARTICLE");
            //V
            groupProcessParticleCol.AddSpinEditColumn("PARTICLE", 55).SetLabel("EA")
                .SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);


            //[약품사용량(L/日)]
            var groupMedicineUsageCol = grdEquipmentSpec.View.AddGroupColumn("CHEMICALUSAGELITER");

            //-신액량(L/日)
            var groupNewWaterCol = groupMedicineUsageCol.AddGroupColumn("USECAPACITY");
            //염산
            groupNewWaterCol.AddSpinEditColumn("HYDROCHLORICACID", 55)
                .SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);
            //과산화물
            groupNewWaterCol.AddSpinEditColumn("PEROXIDE", 55)
                .SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);
            //황산
            groupNewWaterCol.AddSpinEditColumn("SULFURICACID", 55)
                .SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);
            //염화동
            groupNewWaterCol.AddSpinEditColumn("COPPERCHLORIDE", 55)
                .SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);
            //소다회
            groupNewWaterCol.AddSpinEditColumn("SODAASH", 55)
                .SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);
            //가성소다
            groupNewWaterCol.AddSpinEditColumn("CAUSTICSODA", 55)
                .SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);

            //-폐액량(L/日)
            var groupWasteWaterCol = groupMedicineUsageCol.AddGroupColumn("DISPOSALCAPACITY");
            //염화동
            groupWasteWaterCol.AddSpinEditColumn("WASTECOPPERCHLORIDE", 55).SetLabel("COPPERCHLORIDE")
                .SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);
            //니켈
            groupWasteWaterCol.AddSpinEditColumn("WASTENICKEL", 55)
                .SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);
            //질산
            groupWasteWaterCol.AddSpinEditColumn("WASTENITRICACID", 55)
                .SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);
            //일반
            groupWasteWaterCol.AddSpinEditColumn("WASTEGENERAL", 55)
                .SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);


            var groupDefaultCol2 = grdEquipmentSpec.View.AddGroupColumn(""); 

            //비고
            groupDefaultCol2.AddTextBoxColumn("DESCRIPTION", 200).SetLabel("REMARK").SetTextAlignment(TextAlignment.Left);
            //유효상태
            //groupDefaultCol2.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetDefault("Valid").SetValidationIsRequired().SetTextAlignment(TextAlignment.Center);
            //생성자
            groupDefaultCol2.AddTextBoxColumn("CREATOR", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            //생성시간
            groupDefaultCol2.AddTextBoxColumn("CREATEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            //수정자
            groupDefaultCol2.AddTextBoxColumn("MODIFIER", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            //수정시간
            groupDefaultCol2.AddTextBoxColumn("MODIFIEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly().SetTextAlignment(TextAlignment.Center);

            grdEquipmentSpec.View.PopulateColumns();

        }

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            
        }
        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            // TODO : 저장 Rule 변경
            DataTable changed = grdEquipmentSpec.GetChangedRows();
            
            ExecuteRule("SaveEquipmentUtilitySpec", changed);
        }

        #endregion

        #region 검색

        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            // TODO : 조회 SP 변경
            var values = Conditions.GetValues();
            values.Add("P_LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable dtEquipmentSpecList = await QueryAsync("SelectEquipmentSpecList", "10001", values);

            if (dtEquipmentSpecList.Rows.Count < 1)
            {
                // 조회할 데이터가 없습니다.
                ShowMessage("NoSelectData");
            }

            grdEquipmentSpec.DataSource = dtEquipmentSpecList;

            grdEquipmentSpec.View.ClearSelection();

            grdEquipmentSpec.View.FocusedRowHandle = 0;
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용

            //공정
            InitializeConditionPopup_EquipmentClass();
            //설비
            InitializeConditionPopup_Equipment();
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
            this.Conditions.GetControl<SmartComboBox>("P_PLANTID").EditValue = UserInfo.Current.Plant;
            this.Conditions.GetControl<SmartComboBox>("P_PLANTID").Enabled = false;

        }

        #region 조회조건 팝업 초기화

        /// <summary>
        /// 설비 조회조건 팝업
        /// </summary>
        private void InitializeConditionPopup_Equipment()
        {
            var equipmentId = Conditions.AddSelectPopup("P_EQUIPMENTID", new SqlQuery("GetEquipmentListByEqp", "10001", $"LANGUAGETYPE={ UserInfo.Current.LanguageType }"), "EQUIPMENTNAME", "EQUIPMENTID")
                         .SetPopupLayout("EQUIPMENT", PopupButtonStyles.Ok_Cancel, true, false)
                         .SetPopupResultCount(1)
                         .SetPosition(2.7)
                         .SetLabel("EQUIPMENT")
                         .SetRelationIds("EQUIPMENTCLASSID")
                         .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                         .SetPopupAutoFillColumns("EQUIPMENTNAME");

            //equipmentId.Conditions.AddComboBox("PARENTEQUIPMENTCLASSID", new SqlQuery("GetEquipmentClassHierarchyAndType", "10001", "HIERARCHY=TopEquipment", "EQUIPMENTCLASSTYPE=Production", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "EQUIPMENTCLASSNAME", "EQUIPMENTCLASSID")
            //                      //.SetValidationKeyColumn()
            //                      .SetLabel("TOPEQUIPMENTCLASS");

            //equipmentId.Conditions.AddComboBox("MIDDLEEQUIPMENTCLASSID", new SqlQuery("GetEquipmentClassHierarchyAndType", "10001", "HIERARCHY=MiddleEquipment", "EQUIPMENTCLASSTYPE=Production", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "EQUIPMENTCLASSNAME", "EQUIPMENTCLASSID")
            //                      //.SetValidationKeyColumn()
            //                      .SetRelationIds("PARENTEQUIPMENTCLASSID")
            //                      .SetLabel("MIDDLEEQUIPMENTCLASS");

            equipmentId.Conditions.AddTextBox("EQUIPMENT")
                .SetLabel("EQUIPMENTIDNAME");

            // 팝업 그리드
            //equipmentId.GridColumns.AddTextBoxColumn("EQUIPMENTCLASSID", 150);
            //equipmentId.GridColumns.AddTextBoxColumn("EQUIPMENTCLASSNAME", 150);
            equipmentId.GridColumns.AddTextBoxColumn("EQUIPMENTID", 150);
            equipmentId.GridColumns.AddTextBoxColumn("EQUIPMENTNAME", 200);

        }

        /// <summary>
        /// 설비그룹 선택하는 팝업
        /// </summary>
        private void InitializeConditionPopup_EquipmentClass()
        {
            //팝업 컬럼 설정
            var EquipmentClassPopup = this.Conditions.AddSelectPopup("EQUIPMENTCLASSID", new SqlQuery("GetEquipmentClassList", "10001", "EQUIPMENTCLASSTYPE=Production", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "EQUIPMENTCLASSNAME", "EQUIPMENTCLASSID")
                                           .SetPopupLayout("EQUIPMENTCLASS", PopupButtonStyles.Ok_Cancel, true, false)
                                           .SetPopupResultCount(1)  //팝업창 선택가능한 개수                 
                                           .SetPopupLayoutForm(800, 500, FormBorderStyle.FixedToolWindow)
                                           .SetLabel("EQUIPMENTCLASS")
                                           //.SetRelationIds("P_PLANTID")
                                           .SetPopupAutoFillColumns("EQUIPMENTCLASSNAME")
                                           .SetPosition(2.5);
            //팝업 조회조건
            EquipmentClassPopup.Conditions.AddComboBox("LARGEEQUIPMENTCLASSID", new SqlQuery("GetEquipmentClassListRelationCombo", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "EQUIPMENTCLASSNAME", "EQUIPMENTCLASSID")
                                          .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                                          .SetLabel("LARGECLASS"); ;

            EquipmentClassPopup.Conditions.AddComboBox("MIDDLEEQUIPMENTCLASSID", new SqlQuery("GetEquipmentClassListRelationCombo", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "EQUIPMENTCLASSNAME", "EQUIPMENTCLASSID")
                                          .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                                          .SetValidationIsRequiredCondition("LARGEEQUIPMENTCLASSID")
                                          .SetRelationIds("LARGEEQUIPMENTCLASSID")
                                          .SetLabel("MIDDLECLASS");

            EquipmentClassPopup.Conditions.AddTextBox("SMALLEQUIPMENTCLASSID")
                                          .SetLabel("SMALLCLASS");
            //팝업 그리드
            EquipmentClassPopup.GridColumns.AddTextBoxColumn("EQUIPMENTCLASSID", 90);
            EquipmentClassPopup.GridColumns.AddTextBoxColumn("EQUIPMENTCLASSNAME", 130);
            EquipmentClassPopup.GridColumns.AddTextBoxColumn("DESCRIPTION", 90);
            EquipmentClassPopup.GridColumns.AddTextBoxColumn("EQUIPMENTCLASSTYPE", 130);
            EquipmentClassPopup.GridColumns.AddTextBoxColumn("LEQUIPMENTCLASSNAME", 100)
                                           .SetLabel("LARGECLASS");
            EquipmentClassPopup.GridColumns.AddTextBoxColumn("MEQUIPMENTCLASSNAME", 100)
                                           .SetLabel("MIDDLECLASS");
            EquipmentClassPopup.GridColumns.AddTextBoxColumn("VALIDSTATE", 100);
        }
        #endregion

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            // TODO : 유효성 로직 변경
            grdEquipmentSpec.View.CheckValidation();

            DataTable changed = grdEquipmentSpec.GetChangedRows();

            if (changed.Rows.Count == 0)
            {
                //저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }

        }

        #endregion

        #region Private Function

        #endregion
    }
}
