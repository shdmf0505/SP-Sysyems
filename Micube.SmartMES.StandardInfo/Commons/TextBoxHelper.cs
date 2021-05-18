using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Utils;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Mask;
using Micube.Framework;
using Micube.Framework.SmartControls;

namespace Micube.SmartMES.StandardInfo
{
    public static class TextBoxHelper
    {
        public static void SetUnitMask(Unit unit, SmartTextBox textBox)
        {
            try
            {
                textBox.Properties.Appearance.Options.UseTextOptions = true;
                textBox.Properties.Mask.AutoComplete = AutoCompleteType.Optimistic;

                switch (unit)
                {
                    case Unit.percent:
                        textBox.Properties.Mask.EditMask = @"\d* %";
                        break;
                    case Unit.percentWithminus:
                        //textBox.Properties.Mask.EditMask = @"[-]?\d+(\.\d+)?* %";
						textBox.Properties.Mask.EditMask = @"[-]?\d*(\.?\d*) %";
						break;
                    case Unit.um:
						//textBox.Properties.Mask.EditMask = @"[-]?\d+(\.\d+)? ㎛";
						//textBox.Properties.Mask.EditMask = @"[-]?\d+(\.\d+)?[±]?\d+(\.\d+)? ㎛";
						textBox.Properties.Mask.EditMask = @"[-]?\d*(\.?\d*)?[±+-]?\d*(\.?\d*)? ㎛";
						break;
                    case Unit.umWithplusminus:
                        //textBox.Properties.Mask.EditMask = @"[-]?\d[±]?+(\.\d+)? ㎛";
                        textBox.Properties.Mask.EditMask = @"[-]?\d*(\.?\d*)?[±+-]?\d*(\.?\d*)? ㎛";

                        break;
                    case Unit.mm2pnl:
                        //textBox.Properties.Mask.EditMask = @".\d+(\.\d+)? ㎟/PNL";
						textBox.Properties.Mask.EditMask = @"\d*(\.?\d*) ㎟/PNL";
						break;
                    case Unit.mm2pnlWithplusminus:
						//textBox.Properties.Mask.EditMask = @".\d+(\.\d+)?[±]?\d+(\.\d+)? ㎟/PNL";
						textBox.Properties.Mask.EditMask = @"\d*(\.?\d*)?[±+-]?\d*(\.?\d*)? ㎟/PNL";
						break;
                    case Unit.sqmm:
                        //textBox.Properties.Mask.EditMask = @".\d+(\.\d+)? sq/mm";
						textBox.Properties.Mask.EditMask = @"\d*(\.?\d*) sq/mm";
						break;
                    case Unit.mm2:
                        //textBox.Properties.Mask.EditMask = @".\d+(\.\d+)? ㎟";
						textBox.Properties.Mask.EditMask = @"\d*(\.?\d*) ㎟";
						break;
                    case Unit.PCS:
						//textBox.Properties.Mask.EditMask = @".\d+(\.\d+)? PCS";
						textBox.Properties.Mask.EditMask = @"\d* PCS";
						break;
                    case Unit.MM:
                        //textBox.Properties.Mask.EditMask = @".\d+(\.\d+)? MM";
						textBox.Properties.Mask.EditMask = @"\d*(\.?\d*) MM";
                        break;
                }

                textBox.Properties.Mask.IgnoreMaskBlank = false;
                textBox.Properties.Mask.MaskType = MaskType.RegEx;
                textBox.Properties.Mask.BeepOnError = false;
                textBox.Properties.Mask.ShowPlaceHolders = true;
                textBox.Properties.Mask.SaveLiteral = true;
                textBox.Properties.Mask.UseMaskAsDisplayFormat = true;

                textBox.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
                textBox.Properties.Appearance.TextOptions.VAlignment = VertAlignment.Center;
            }
            catch (Exception ex)
            {
                
            }
        }

        public static void SetMarkMask(Mark mark, SmartTextBox textBox)
        {
            try
            {
                switch (mark)
                {
                    case Mark.BY:
                        textBox.Properties.Mask.EditMask = @"\d *\ X \d*";
                        break;
                    case Mark.LR:
                        textBox.Properties.Mask.EditMask =
                            Language.GetDictionary("LEFT").Name + @": \d*" + Language.GetDictionary("RIGHT").Name +
                            @": \d*";
                        break;
                    case Mark.XAxis:
                        textBox.Properties.Mask.EditMask = @"X : \d*";
                        break;
                    case Mark.YAxis:
                        textBox.Properties.Mask.EditMask = @"Y : \d*";
                        break;
                    case Mark.LEFT:
                        textBox.Properties.Mask.EditMask =
                            Language.GetDictionary("LEFT").Name + @": \d*";
                        break;
                    case Mark.RIGHT:
                        textBox.Properties.Mask.EditMask =
                            Language.GetDictionary("RIGHT").Name + @": \d*";
                        break;
                    case Mark.PLUSMINUS:
                        textBox.Properties.Mask.EditMask = @"±  \d*";
                        break;
					case Mark.XAxisDecimalPoint:
						textBox.Properties.Mask.EditMask = @"X : \d*(\.?\d*)";
						break;
					case Mark.YAxisDecimalPoint:
						textBox.Properties.Mask.EditMask = @"Y : \d*(\.?\d*)";
						break;
                    case Mark.Scale:
                        textBox.Properties.Mask.EditMask = @"Scale : \d*(\.?\d*)";
                        textBox.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
                        textBox.Properties.Appearance.TextOptions.VAlignment = VertAlignment.Center;
                        break;

                }

                textBox.Properties.Mask.MaskType = MaskType.RegEx;
                textBox.Properties.Mask.AutoComplete = AutoCompleteType.Optimistic;

                textBox.Properties.Mask.IgnoreMaskBlank = false;
                textBox.Properties.Mask.UseMaskAsDisplayFormat = true;
                textBox.Properties.Mask.BeepOnError = false;
                textBox.Properties.Mask.ShowPlaceHolders = true;
            }
            catch (Exception ex)
            {
                
            }
        }
    }

    public enum Unit
    {
        um,
        umWithplusminus,
        percent,
        percentWithminus,
        MM,
        mm2,
        mm2pnl,
        mm2pnlWithplusminus,
        sqmm,
        PCS
    }

    public enum Mark
    {
        BY,
        LR,
        XAxis,
        YAxis,
        LEFT,
        RIGHT,
        PLUSMINUS,
		XAxisDecimalPoint,
		YAxisDecimalPoint,
        Scale
        
	}
}
