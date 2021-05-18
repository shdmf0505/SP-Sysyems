#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;

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
    /// 프 로 그 램 명  : 공정 관리 > 공정작업 > 작업 시작 > 설비 Recipe 파라미터 팝업
    /// 업  무  설  명  : 작업 시작에서 저장 시 선택된 설비 Recipe의 파라미터 리스트를 보여주는 팝업
    /// 생    성    자  : 강호윤
    /// 생    성    일  : 2020-01-08
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class EquipmentRecipeParameterPopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region ISmartCustomPopup

        public DataRow CurrentDataRow { get; set; }

        #endregion

        #region Local Variables

        DataTable _dataSource;

        #endregion

        #region 생성자

        public EquipmentRecipeParameterPopup()
        {
            InitializeComponent();
        }

        public EquipmentRecipeParameterPopup(DataTable dataSource)
        {
            InitializeComponent();

            _dataSource = dataSource;

            InitializeEvent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        private void InitializeControls()
        {
            grdEquipmentRecipe.GridButtonItem = GridButtonItem.None;
            grdEquipmentRecipe.ShowButtonBar = false;
            grdEquipmentRecipe.ShowStatusBar = false;

            grdEquipmentRecipe.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdEquipmentRecipe.View.SetIsReadOnly();

            // 설비 코드
            grdEquipmentRecipe.View.AddTextBoxColumn("EQUIPMENTID", 150);
            // 설비명
            grdEquipmentRecipe.View.AddTextBoxColumn("EQUIPMENTNAME", 200);
            // Recipe Id
            grdEquipmentRecipe.View.AddTextBoxColumn("RECIPEID", 120);
            // Recipe Version
            grdEquipmentRecipe.View.AddTextBoxColumn("RECIPEVERSION", 70);
            // Recipe Name
            grdEquipmentRecipe.View.AddTextBoxColumn("RECIPENAME", 150);
            // Parameter Id
            grdEquipmentRecipe.View.AddTextBoxColumn("PARAMETERID", 120);
            // Parameter Name
            grdEquipmentRecipe.View.AddTextBoxColumn("PARAMETERNAME", 150);
            // 하한값
            grdEquipmentRecipe.View.AddSpinEditColumn("LSL", 80);
            // 목표값
            grdEquipmentRecipe.View.AddSpinEditColumn("TARGET", 80);
            // 상한값
            grdEquipmentRecipe.View.AddSpinEditColumn("USL", 80);
            // Validation Type
            grdEquipmentRecipe.View.AddTextBoxColumn("VALIDATIONTYPE", 120);
            // Data Type
            grdEquipmentRecipe.View.AddComboBoxColumn("DATATYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=RecipeParameterDataType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetTextAlignment(TextAlignment.Center)
                .SetIsHidden();
            // Value
            grdEquipmentRecipe.View.AddTextBoxColumn("VALUE", 100)
                .SetIsHidden();


            grdEquipmentRecipe.View.PopulateColumns();


            grdEquipmentRecipe.View.OptionsView.ShowIndicator = false;
        }

        #endregion

        #region Event

        private void InitializeEvent()
        {
            InitializeControls();

            grdEquipmentRecipe.DataSource = _dataSource;
        }

        #endregion
    }
}