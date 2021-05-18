using Micube.SmartMES.Commons.Gerber.Core.Primitives;
using System.Collections.Generic;

namespace Micube.SmartMES.Commons.Gerber
{
    public class GerberOutlineWriter
    {
        List<PolyLine> PolyLines = new List<PolyLine>();

        public void AddPolyLine(PolyLine a) => PolyLines.Add(a);

        public void Write(string p, double inWidth = 0.0)
        {

            GerberNumberFormat GNF = new GerberNumberFormat
            {
                DigitsBefore = 4,
                DigitsAfter = 4
            };

            GNF.SetImperialMode();

            List<string> lines = new List<string>
            {
                Gerber.INCH,
                "%OFA0B0*%",
                GNF.BuildGerberFormatLine(),
                "%IPPOS*%",
                "%LPD*%"
            };

            GerberApertureType Apt = new GerberApertureType();
            Apt.SetCircle(inWidth);
            Apt.ID = 10;
            lines.Add(Apt.BuildGerber(GNF));
            lines.Add(Apt.BuildSelectGerber());

            foreach (var a in PolyLines)
            {
                lines.Add(Gerber.MoveTo(a.Vertices[a.Vertices.Count-1], GNF));

                for (int i = 0; i < a.Vertices.Count; i++)
                {
                    lines.Add(Gerber.LineTo(a.Vertices[i], GNF));
                }
            }

            lines.Add(Gerber.EOF);
            Gerber.WriteAllLines(p, lines);
        }
    }

}
