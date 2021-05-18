﻿using ClipperLib;
using Micube.SmartMES.Commons.Gerber.Core.Primitives;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
//using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Micube.SmartMES.Commons.Gerber.Core;

namespace Micube.SmartMES.Commons.Gerber
{
    public class BoardRenderColorSet
    {
        public Color BoardRenderBaseMaterialColor = Gerber.ParseColor("#808080");
        public Color BoardRenderColor = Gerber.ParseColor("green");
        public Color BoardRenderCopperColor = Color.FromArgb(219, 125, 104);
        public Color BoardRenderPadColor = Gerber.ParseColor("gold");
        public Color BoardRenderSilkColor = Gerber.ParseColor("white");

        public Color BackgroundColor = Color.White;
        public Color BoardRenderTraceColor = Gerber.ParseColor("green");
        public void SetupColors(string SolderMaskColor, string SilkScreenColor, string TracesColor = "auto", string CopperColor = "gold")
        {
            BoardRenderColor = Gerber.ParseColor(SolderMaskColor);
            BoardRenderSilkColor = Gerber.ParseColor(SilkScreenColor);
            BoardRenderPadColor = Gerber.ParseColor(CopperColor);
            BoardRenderTraceColor = Gerber.ParseColor(TracesColor);
        }
        public Color GetDefaultColor(BoardLayer layer, BoardSide side)
        {
            switch(layer)
            {
                case BoardLayer.Drill: return BackgroundColor;
                case BoardLayer.Copper: return BoardRenderCopperColor;
                case BoardLayer.Outline: return BoardRenderColor;
                case BoardLayer.SolderMask: return MathHelpers.Interpolate(BoardRenderColor, BoardRenderBaseMaterialColor, 0.2f);
                case BoardLayer.Silk: return BoardRenderSilkColor;
            }
            return Color.FromArgb(100, 255, 255, 255);
        }
    }
    public static class Gerber
    {
        #region GERBERPROCESSINGDEFAULTS
        public static double ArcQualityScaleFactor = 15;
        
        public static bool DirectlyShowGeneratedBoardImages = true;
        public static bool DumpSanitizedOutput = false;
        public static string EOF = "M02*";
        public static bool ExtremelyVerbose = false;
        public static bool GerberRenderBumpMapOutput = true;
        public static string INCH = "%MOIN*%";
        public static string LinearInterpolation = "G01*";
        public static string LineEnding = "\n";
        public static string MM = "%MOMM*%";
        public static bool SaveDebugImageOutput = false;
        public static bool SaveIntermediateImages = false;
        public static bool SaveOutlineImages = false;
        public static bool ShowProgress = false;
        public static string StartRegion = "G36*";
        public static string StopRegion = "G37*";
        public static bool WaitForKey = false;
        public static bool WriteSanitized = false;
        #endregion


        public static int GetDefaultSortOrder(BoardSide side, BoardLayer layer)
        {
            int R = 0;
            switch(layer)
            {

                case BoardLayer.Mill: R = 11; break;
                case BoardLayer.Silk: R = 101;break;
                case BoardLayer.Paste: R = 10; break;
                case BoardLayer.SolderMask: R = 102; break;
                case BoardLayer.Copper: R = 100; break;
                case BoardLayer.Carbon: R = 103; break;
            }
            if (side == BoardSide.Bottom) R *= -1;
            return R;
        }

        private static readonly Regex rxScientific = new Regex(@"^(?<sign>-?)(?<head>\d+)(\.(?<tail>\d*?)0*)?E(?<exponent>[+\-]\d+)$", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.CultureInvariant);
        public static bool SkipEagleDrillFix = false;

        public static List<PointD> CreateCurvePoints(double LastX, double LastY, double X, double Y, double I, double J, InterpolationMode mode, GerberQuadrantMode qmode)
        {
            //   Console.WriteLine("Current curve mode: {0}", qmode);
            List<PointD> R = new List<PointD>();

            double Radius = Math.Sqrt(I * I + J * J);
            double CX = LastX + I;
            double CY = LastY + J;

            Quadrant Q = Quadrant.xposypos;

            double HS = Math.Atan2(LastY - CY, LastX - CX);
            double HE = Math.Atan2(Y - CY, X - CX);
            if (qmode == GerberQuadrantMode.Multi)
            {

                if (mode == InterpolationMode.ClockWise)
                {
                    while (HS <= HE) HS += Math.PI * 2;
                }
                else
                {
                    while (HS >= HE) HS -= Math.PI * 2;
                }

            }
            else
            {
                double LastDiff = Math.PI * 2;
                List<QuadR> qR = new List<QuadR>
                {
                    new QuadR() { CX = LastX + I, CY = LastY + J },
                    new QuadR() { CX = LastX - I, CY = LastY + J },
                    new QuadR() { CX = LastX - I, CY = LastY - J },
                    new QuadR() { CX = LastX + I, CY = LastY - J }
                };

                foreach (var a in qR) a.Calc(LastX, LastY, X, Y);
                int candidates = 0;


                if (ExtremelyVerbose)
                {
                    var DX = LastX - X;

                    var DY = LastY - Y;
                    var L = Math.Sqrt(DX * DX + DY * DY);
                    if (L < 1.0)
                    {


                        R.Add(new PointD(X, Y));

                        return R;
                    }

                    Console.WriteLine("length: {0}", L);
                }
                if (mode == InterpolationMode.CounterClockwise)
                {

                    double LastRat = 10;
                    foreach (var a in qR) a.FixCounterClockwise();
                    foreach (var a in qR)
                    {
                        if (a.Diff <= Math.PI / 2.0)
                        {
                            candidates++;
                            if (Math.Abs(1 - a.DRat) < LastRat)
                            {
                                CX = a.CX;
                                CY = a.CY;
                                HS = a.S;
                                HE = a.E;
                                LastRat = Math.Abs(1 - a.DRat);
                            }

                            if (Gerber.ExtremelyVerbose) Console.WriteLine("candidate: {0:N1} - {1:N1} - {2:N1}", RadToDeg(a.S), RadToDeg(a.E), RadToDeg(a.Diff));
                        }
                    }
                    /*
                                        HS = qR[3].S;
                                        CX = qR[3].CX;
                                        CY = qR[3].CY;
                                        HE = qR[3].E;
                                        */

                }
                else
                {

                    foreach (var a in qR) a.FixClockwise();

                    foreach (var a in qR)
                    {
                        if (a.Diff >= 0 && a.Diff <= Math.PI / 2.0 + 0.00001)
                        {
                            candidates++;
                            if (Math.Abs(a.Diff) < LastDiff)
                            {
                                CX = a.CX;
                                CY = a.CY;
                                HS = a.S;
                                HE = a.E;
                                LastDiff = Math.Abs(a.Diff);
                            }

                            if (Gerber.ExtremelyVerbose) Console.WriteLine("candidate: {0} - {1} - {2}", a.S, a.E, a.Diff);
                        }
                        if (Gerber.ExtremelyVerbose) Console.WriteLine("selected : {0} - {1} - {2}", HS, HE, LastDiff);

                    }

                }

                if (candidates == 0 && Gerber.ExtremelyVerbose)
                {
                    foreach (var a in qR)
                    {
                        Console.WriteLine("no candidate: {0} - {1} - {2}  ( should be smaller than {3}) ", a.S, a.E, a.Diff, Math.PI / 2.0);
                    }
                }

            }
            if (Gerber.ExtremelyVerbose)
            {
                Console.WriteLine("HS {0:N1}  HE {1:N1} DIFF {2:N1} QUAD {3} CX {4} CY {5}", RadToDeg(HS), RadToDeg(HE), RadToDeg(HE - HS), Q, CX, CY);


            }
            int segs = (int)(Gerber.ArcQualityScaleFactor * Math.Max(2.0, Radius) * Math.Abs(HS - HE) / (Math.PI * 2));

            if (segs < 10) segs = 10;

            double HEdeg = RadToDeg(HE);

            double HSdeg = RadToDeg(HS);
            for (int i = 0; i <= segs; i++)
            {
                double P = ((double)i / (double)segs) * (HE - HS) + HS;
                double nx = Math.Cos(P) * Radius + CX;
                double ny = Math.Sin(P) * Radius + CY;
                R.Add(new PointD(nx, ny));
            }

        //    R.Add(new PointD(X, Y));

            return R;
        }

        public static void DetermineBoardSideAndLayer(string gerberfile, out BoardSide Side, out BoardLayer Layer)
        {
            Side = BoardSide.Unknown;
            Layer = BoardLayer.Unknown;
            string[] filesplit = Path.GetFileName( gerberfile).Split('.');
            string ext = filesplit[filesplit.Count() - 1].ToLower();
            switch (ext)
            {
                case "gbr":

                    switch (Path.GetFileNameWithoutExtension(gerberfile).ToLower())
                    {
                        case "boardoutline":
                            Side = BoardSide.Both;
                            Layer = BoardLayer.Outline;
                            break;
                        case "outline":
                            Side = BoardSide.Both;
                            Layer = BoardLayer.Outline;
                            break;

                        case "board":
                            Side = BoardSide.Both;
                            Layer = BoardLayer.Outline;
                            break;
                        case "bottom":
                            Side = BoardSide.Bottom;
                            Layer = BoardLayer.Copper;
                            break;
                        case "bottommask":
                            Side = BoardSide.Bottom;
                            Layer = BoardLayer.SolderMask;
                            break;
                        case "bottompaste":
                            Side = BoardSide.Bottom;
                            Layer = BoardLayer.Paste;
                            break;
                        case "bottomsilk":
                            Side = BoardSide.Bottom;
                            Layer = BoardLayer.Silk;
                            break;
                        case "top":
                            Side = BoardSide.Top;
                            Layer = BoardLayer.Copper;
                            break;
                        case "topmask":
                            Side = BoardSide.Top;
                            Layer = BoardLayer.SolderMask;
                            break;
                        case "toppaste":
                            Side = BoardSide.Top;
                            Layer = BoardLayer.Paste;
                            break;
                        case "topsilk":
                            Side = BoardSide.Top;
                            Layer = BoardLayer.Silk;
                            break;
                        case "inner1":
                            Side = BoardSide.Internal1;
                            Layer = BoardLayer.Copper;
                            break;
                        case "inner2":
                            Side = BoardSide.Internal2;
                            Layer = BoardLayer.Copper;
                            break;

                        default:
                            if (gerberfile.ToLower().Contains("outline")) { Side = BoardSide.Both; Layer = BoardLayer.Outline; }
                            if (gerberfile.ToLower().Contains("-edge_cuts")) { Side = BoardSide.Both;Layer = BoardLayer.Outline;}

                            if (gerberfile.ToLower().Contains("-b_cu")) { Side = BoardSide.Bottom; Layer = BoardLayer.Copper; }
                            if (gerberfile.ToLower().Contains("-f_cu")) { Side = BoardSide.Top; Layer = BoardLayer.Copper; }
                            if (gerberfile.ToLower().Contains("-b_silks")) { Side = BoardSide.Bottom; Layer = BoardLayer.Silk; }
                            if (gerberfile.ToLower().Contains("-f_silks")) { Side = BoardSide.Top; Layer = BoardLayer.Silk; }
                            if (gerberfile.ToLower().Contains("-b_mask")) { Side = BoardSide.Bottom; Layer = BoardLayer.SolderMask; }
                            if (gerberfile.ToLower().Contains("-f_mask")) { Side = BoardSide.Top; Layer = BoardLayer.SolderMask; }
                            if (gerberfile.ToLower().Contains("-b_paste")) { Side = BoardSide.Bottom; Layer = BoardLayer.Paste; }
                            if (gerberfile.ToLower().Contains("-f_paste")) { Side = BoardSide.Top; Layer = BoardLayer.Paste; }
                            break;

                    }
                    break;

                case "ger":
                    {
                        string l = gerberfile.ToLower();
                        List<Boardset> bs = new List<Boardset>
                        {
                            new Boardset() { name = ".topsoldermask", side = BoardSide.Top, layer = BoardLayer.SolderMask },
                            new Boardset() { name = ".topsilkscreen", side = BoardSide.Top, layer = BoardLayer.Silk },
                            new Boardset() { name = ".toplayer", side = BoardSide.Top, layer = BoardLayer.Copper },
                            new Boardset() { name = ".tcream", side = BoardSide.Top, layer = BoardLayer.Paste },
                            new Boardset() { name = ".boardoutline", side = BoardSide.Both, layer = BoardLayer.Outline },
                            new Boardset() { name = ".bcream", side = BoardSide.Bottom, layer = BoardLayer.SolderMask },
                            new Boardset() { name = ".bottomsoldermask", side = BoardSide.Bottom, layer = BoardLayer.SolderMask },
                            new Boardset() { name = ".bottomsilkscreen", side = BoardSide.Bottom, layer = BoardLayer.Silk },
                            new Boardset() { name = ".bottomlayer", side = BoardSide.Bottom, layer = BoardLayer.Copper },
                            new Boardset() { name = ".bcream", side = BoardSide.Bottom, layer = BoardLayer.Paste },
                            new Boardset() { name = ".internalplane1", side = BoardSide.Internal1, layer = BoardLayer.Copper },
                            new Boardset() { name = ".internalplane2", side = BoardSide.Internal2, layer = BoardLayer.Copper }
                        };

                        foreach (var a in bs)
                        {
                            if (l.Contains(a.name))
                            {
                                Side = a.side;
                                Layer = a.layer;
                            }
                        }
                    }
                    break;

                case "gml":
                    Side = BoardSide.Both;
                    Layer = BoardLayer.Mill;
                    break;
                case "fabrd":
                case "oln":
                case "gko":
                    Side = BoardSide.Both;
                    Layer = BoardLayer.Outline;
                    break;
                case "l2":
                case "gl1":
                    Side = BoardSide.Internal1;
                    Layer = BoardLayer.Copper;
                    break;
                case "adtop":
                    Side = BoardSide.Top;
                    Layer = BoardLayer.Assembly;
                    break;
                case "adbottom":
                    Side = BoardSide.Bottom;
                    Layer = BoardLayer.Assembly;
                    break;
                case "notes":
                    Side = BoardSide.Both;
                    Layer = BoardLayer.Notes;
                    break;
                case "l3":

                case "gl2":
                    Side = BoardSide.Internal2;
                    Layer = BoardLayer.Copper;
                    break;

                case "l4":
                case "gbl":
                case "l2m":
                    Side = BoardSide.Bottom;
                    Layer = BoardLayer.Copper;
                    break;

                case "l1":
                case "l1m":
                case "gtl":
                    Side = BoardSide.Top;
                    Layer = BoardLayer.Copper;
                    break;

                case "gbp":
                case "spbottom":
                    Side = BoardSide.Bottom;
                    Layer = BoardLayer.Paste;
                    break;

                case "gtp":
                case "sptop":
                    Side = BoardSide.Top;
                    Layer = BoardLayer.Paste;
                    break;

                case "gbo":
                case "ss2":
                case "ssbottom":
                    Side = BoardSide.Bottom;
                    Layer = BoardLayer.Silk;
                    break;

                case "gto":
                case "ss1":
                case "sstop":
                    Side = BoardSide.Top;
                    Layer = BoardLayer.Silk;
                    break;

                case "gbs":
                case "sm2":
                case "smbottom":
                    Side = BoardSide.Bottom;
                    Layer = BoardLayer.SolderMask;
                    break;

                case "gts":
                case "sm1":
                case "smtop":

                    Side = BoardSide.Top;
                    Layer = BoardLayer.SolderMask;
                    break;

                case "gb3": // oshstencils bottom outline
                    Side = BoardSide.Both;
                    Layer = BoardLayer.Outline;
                    break;

                case "gt3": // oshstencils top outline
                    Side = BoardSide.Both;
                    Layer = BoardLayer.Outline;
                    break;

                case "top":
                    Side = BoardSide.Top;
                    Layer = BoardLayer.Copper;
                    break;

                case "bottom":
                case "bot":
                    Side = BoardSide.Bottom;
                    Layer = BoardLayer.Copper;
                    break;

                case "smb":
                    Side = BoardSide.Bottom;
                    Layer = BoardLayer.SolderMask;
                    break;

                case "smt":
                    Side = BoardSide.Top;
                    Layer = BoardLayer.SolderMask;
                    break;

                case "slk":
                case "sst":
                    Side = BoardSide.Top;
                    Layer = BoardLayer.Silk;
                    break;

                case "bsk":
                case "ssb":
                    Side = BoardSide.Bottom;
                    Layer = BoardLayer.Silk;
                    break;

                case "spt":
                    Side = BoardSide.Top;
                    Layer = BoardLayer.Paste;
                    break;

                case "spb":
                    Side = BoardSide.Bottom;
                    Layer = BoardLayer.Paste;
                    break;
                case "drill_TOP_BOTTOM":
                case "drl":
                case "drill":
                case "drillnpt":
                    Side = BoardSide.Both;
                    Layer = BoardLayer.Drill;
                    break;

            }
        }

        public static BoardFileType FindFileType(string filename)
        {
            //filename = filename.ToLower();
            List<string> unsupported = new List<string>() { "config", "exe", "dll", "png", "zip", "gif", "jpeg", "doc", "docx", "jpg", "bmp" };
            string[] filesplit = filename.Split('.');
            string ext = filesplit[filesplit.Count() - 1].ToLower();
            foreach (var s in unsupported)
            {
                if (ext == s)
                {

                    return BoardFileType.Unsupported;
                }
            }
            try
            {
                // var F = File.OpenText(a);
                var F = File.ReadAllLines(filename);
                for (int i = 0; i < F.Count(); i++)
                {
                    string L = F[i];
                    if (L.Contains("%FS")) return BoardFileType.Gerber;
                    if (L.Contains("M48")) return BoardFileType.Drill;
                };


            }
            catch (Exception E)
            {
                if (Gerber.ExtremelyVerbose)
                {
                    Console.WriteLine("Exception determining filetype: {0}", E.Message);
                }
                return BoardFileType.Unsupported;
            }

            return BoardFileType.Unsupported;


        }

        public static BoardFileType FindFileTypeFromStream(StreamReader l, string filename)
        {
            filename = filename.ToLower();
            List<string> unsupported = new List<string>() { "config", "exe", "dll", "png", "zip", "gif", "jpeg", "doc", "docx", "jpg", "bmp" };
            string[] filesplit = filename.Split('.');
            string ext = filesplit[filesplit.Count() - 1].ToLower();
            foreach (var s in unsupported)
            {
                if (ext == s)
                {

                    return BoardFileType.Unsupported;
                }
            }
            try
            {
                // var F = File.OpenText(a);
                List<string> lines = new List<string>();
                while (!l.EndOfStream)
                {
                    lines.Add(l.ReadLine());
                }
                //var F = File.ReadAllLines(filename);


                for (int i = 0; i < lines.Count(); i++)
                {
                    string L = lines[i];
                    if (L.Contains("%FS")) return BoardFileType.Gerber;
                    if (L.Contains("M48")) return BoardFileType.Drill;
                };


            }
            catch (Exception)
            {
                return BoardFileType.Unsupported;
            }

            return BoardFileType.Unsupported;

        }

        public static PolyLineSet.Bounds GetBoundingBox(List<string> generatedFiles)
        {
            PolyLineSet.Bounds A = new PolyLineSet.Bounds();

            foreach (var a in generatedFiles)
            {
                ParsedGerber PLS = PolyLineSet.LoadGerberFile(a, State: new GerberParserState() { PreCombinePolygons = false });
                A.AddBox(PLS.BoundingBox);
            }
            return A;
        }

        public static Color ParseColor(string color)
        {
            if (color == null)
            {
                Console.WriteLine("Error: Null color! Defaulting to lime!");
                return Color.Lime;
            }

            switch (color.ToLower())
            {
                case "blue": return Color.FromArgb(0, 40, 74);
                case "yellow": return Color.FromArgb(234, 206, 39);
                case "green": return Color.FromArgb(68, 105, 80);
                case "black": return Color.FromArgb(5, 5, 5);
                case "white": return Color.FromArgb(250, 250, 250);
                case "red": return Color.FromArgb(192, 43, 43);
                case "silver": return Color.FromArgb(160, 160, 160);
                case "gold": return Color.FromArgb(239, 205, 85);
            }

            try
            {
                return System.Drawing.ColorTranslator.FromHtml(color);
            }
            catch (Exception)
            {
                // unknown colors end up here... no need to worry, just pass it on to the default color handler which returns 0,0,0 as error-color if it too cant find anything.
            }

            return Color.FromName(color);
        }


        public static double RadToDeg(double inp)
        {
            return inp * 360.0 / (Math.PI * 2.0);
        }

        public static bool SaveDebugImage(string GerberFilename, string BitmapFilename, float dpi, Color Foreground, Color Background, float borderThick = (float)0.5)
        {
            ParsedGerber PLS;
            GerberParserState State = new GerberParserState()
            {
                PreCombinePolygons = false
            };

            var FileType = Gerber.FindFileType(GerberFilename);
            Gerber.DetermineBoardSideAndLayer(GerberFilename, out State.Side, out State.Layer);
            bool forcezero = false;

            if (State.Layer == BoardLayer.Outline)
            {
                //    PLS.PreCombinePolygons = true;

                //    forcezero = true;
            }
            if (FileType == BoardFileType.Drill)
            {
                PLS = PolyLineSet.LoadExcellonDrillFile(GerberFilename);
            }
            else
            {
                PLS = PolyLineSet.LoadGerberFile(GerberFilename, forcezero, Gerber.WriteSanitized, State);

            }
            double WidthInMM = PLS.BoundingBox.BottomRight.X - PLS.BoundingBox.TopLeft.X;
            double HeightInMM = PLS.BoundingBox.BottomRight.Y - PLS.BoundingBox.TopLeft.Y;
            int Width = (int)(Math.Ceiling((WidthInMM) * (dpi / 25.4)));
            int Height = (int)(Math.Ceiling((HeightInMM) * (dpi / 25.4)));

            Console.WriteLine("Progress: Exporting {0} ({2},{3}mm) to {1} ({4},{5})", GerberFilename, BitmapFilename, WidthInMM, HeightInMM, Width, Height);
            GerberImageCreator GIC = new GerberImageCreator
            {
                scale = dpi / 25.4f // dpi
            };
            GIC.BoundingBox.AddBox(PLS.BoundingBox);

            var Tr = GIC.BuildMatrix(Width, Height);

            Bitmap B2 = GIC.RenderToBitmap(Width, Height, Tr, Foreground, Background, PLS, true);
            if (B2 == null) return false;

            var GerberLines = PolyLineSet.SanitizeInputLines(System.IO.File.ReadAllLines(GerberFilename).ToList());
            double LastX = 0;
            double LastY = 0;



            Graphics G2 = Graphics.FromImage(B2);

            GerberImageCreator.ApplyAASettings(G2);
            //G2.Clear(Background);
            G2.Transform = Tr.Clone();



            foreach (var L in GerberLines)
            {
                if (L[0] != '%')
                {
                    GerberSplitter GS = new GerberSplitter();
                    GS.Split(L, PLS.State.CoordinateFormat);

                    if (GS.Has("G") && (int)GS.Get("G") == 3)
                    {
                        double X = PLS.State.CoordinateFormat.ScaleFileToMM(GS.Get("X"));
                        double Y = PLS.State.CoordinateFormat.ScaleFileToMM(GS.Get("Y"));

                        double I = PLS.State.CoordinateFormat.ScaleFileToMM(GS.Get("I"));
                        double J = PLS.State.CoordinateFormat.ScaleFileToMM(GS.Get("J"));

                        //Console.WriteLine("Counterclockwise Curve {0},{1} -> {2},{3}", LastX, LastY, X, Y);
                        DrawCross(G2, X, Y, Color.Blue);
                        DrawCross(G2, LastX, LastY, Color.Red);

                        DrawCross(G2, LastX + I, LastY - J, Color.Yellow);
                        DrawCross(G2, LastX + I, LastY + J, Color.Purple);
                        DrawCross(G2, LastX - I, LastY - J, Color.Green);
                        DrawCross(G2, LastX - I, LastY + J, Color.Orange);

                    }

                    if (GS.Has("X")) LastX = PLS.State.CoordinateFormat.ScaleFileToMM(GS.Get("X"));
                    if (GS.Has("Y")) LastY = PLS.State.CoordinateFormat.ScaleFileToMM(GS.Get("Y"));
                }

            }


            B2.Save(BitmapFilename);
            return true;
        }

        public static ParsedGerber SaveGerberFileToImage(string gerberFileName, string bitMapFileName, float dpi, Color foreground, Color background, float borderThick = (float)0.5)
        {
            try
            {
                ParsedGerber parsedGerber;
                GerberParserState gerberState = new GerberParserState()
                {
                    PreCombinePolygons = false
                };

                var fileType = Gerber.FindFileType(gerberFileName);
                Gerber.DetermineBoardSideAndLayer(gerberFileName, out gerberState.Side, out gerberState.Layer);
                bool forcezero = false;

                if (gerberState.Layer == BoardLayer.Outline)
                {
                    //    PLS.PreCombinePolygons = true;
                    //    forcezero = true;
                }
                if (fileType == BoardFileType.Drill)
                {
                    parsedGerber = PolyLineSet.LoadExcellonDrillFile(gerberFileName);
                }
                else
                {
                    parsedGerber = PolyLineSet.LoadGerberFile(gerberFileName, forcezero, Gerber.WriteSanitized, gerberState);

                }
                double widthInMM = parsedGerber.BoundingBox.BottomRight.X - parsedGerber.BoundingBox.TopLeft.X;
                double heightInMM = parsedGerber.BoundingBox.BottomRight.Y - parsedGerber.BoundingBox.TopLeft.Y;
                int bitmapWidth = (int)(Math.Ceiling((widthInMM) * (dpi / 25.4)));
                int bitmapHeight = (int)(Math.Ceiling((heightInMM) * (dpi / 25.4)));

                Console.WriteLine(string.Format("Width \t: {0}", widthInMM));
                Console.WriteLine(string.Format("Height \t: {0}", heightInMM));

                Console.WriteLine("Progress: Exporting {0} ({2},{3}mm) to {1} ({4},{5})", gerberFileName, bitMapFileName, widthInMM, heightInMM, bitmapWidth, bitmapHeight);
                Console.WriteLine(string.Format("Parsing CompleteTime : {0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
                Console.WriteLine(string.Format("Image Save Start Time : {0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));

                GerberImageCreator gic = new GerberImageCreator
                {
                    scale = dpi / 25.4f
                };
                ;// dpi / 25.4f; // dpi
                gic.BoundingBox.AddBox(parsedGerber.BoundingBox);

                System.Drawing.Drawing2D.Matrix transform = gic.BuildMatrix(bitmapWidth, bitmapHeight);

                parsedGerber.Background = background;
                parsedGerber.Foreground = foreground;
                parsedGerber.PanelWidth = widthInMM;
                parsedGerber.PanelHeight = heightInMM;
                parsedGerber.GerberFileName = gerberFileName;
                parsedGerber.BitmapFileName = bitMapFileName;
                parsedGerber.Transform = transform;
                parsedGerber.BitMapWidth = bitmapWidth;
                parsedGerber.BitMapHeight = bitmapHeight;


                Bitmap bitmap = gic.RenderToBitmap(bitmapWidth, bitmapHeight, transform, foreground, background, parsedGerber, true, borderThick, false);
                if (bitmap == null)
                    return null;

                bitmap = new Bitmap(bitmap, (int)widthInMM, (int)heightInMM);
                parsedGerber.TopBitmap = bitmap;

                //bitmap.Save(bitMapFileName + "_Top.png");


                Bitmap bmp = (Bitmap)bitmap.Clone();
                // 이미지 대칭
                bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);

                //bmp.Save(bitMapFileName + "Bottom.png");

                parsedGerber.BottomBitmap = bmp;

                Console.WriteLine(string.Format("Image Save End Time : {0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));

                return parsedGerber;
            }
            catch (Exception E)
            {
                Console.WriteLine("Error: Errors while writing bitmap {0} for gerberfile {1} at dpi {2}:", bitMapFileName, gerberFileName, dpi);
                while (E != null)
                {
                    Console.WriteLine("Error: \t{0}", E.Message);
                    E = E.InnerException;
                }
                return null;
            }

        }

        public static ParsedGerber SaveExternalImage(Bitmap bitMap, float dpi, int width, int height)
        {
            try
            {
                ParsedGerber parsedGerber = new ParsedGerber();
                parsedGerber.PanelWidth = width;
                parsedGerber.PanelHeight = height;
                parsedGerber.BitMapWidth = bitMap.Width;
                parsedGerber.BitMapHeight = bitMap.Height;

                parsedGerber.TopBitmap = bitMap;

                Bitmap bottomBipmap = (Bitmap)bitMap.Clone();
                bottomBipmap.RotateFlip(RotateFlipType.RotateNoneFlipY);

                parsedGerber.BottomBitmap = bottomBipmap;


                return parsedGerber;
            }
            catch (Exception E)
            {
                while (E != null)
                {
                    Console.WriteLine("Error: \t{0}", E.Message);
                    E = E.InnerException;
                }
                return null;
            }

        }

        public static string ToFloatingPointString(double value)
        {
            return ToFloatingPointString(value, NumberFormatInfo.CurrentInfo);
        }

        public static string ToFloatingPointString(double value, NumberFormatInfo formatInfo)
        {
            string result = value.ToString("r", NumberFormatInfo.InvariantInfo);
            Match match = rxScientific.Match(result);
            if (match.Success)
            {
                Debug.WriteLine("Found scientific format: {0} => [{1}] [{2}] [{3}] [{4}]", result, match.Groups["sign"], match.Groups["head"], match.Groups["tail"], match.Groups["exponent"]);
                int exponent = int.Parse(match.Groups["exponent"].Value, NumberStyles.Integer, NumberFormatInfo.InvariantInfo);
                StringBuilder builder = new StringBuilder(result.Length + Math.Abs(exponent));
                builder.Append(match.Groups["sign"].Value);
                if (exponent >= 0)
                {
                    builder.Append(match.Groups["head"].Value);
                    string tail = match.Groups["tail"].Value;
                    if (exponent < tail.Length)
                    {
                        builder.Append(tail, 0, exponent);
                        builder.Append(formatInfo.NumberDecimalSeparator);
                        builder.Append(tail, exponent, tail.Length - exponent);
                    }
                    else
                    {
                        builder.Append(tail);
                        builder.Append('0', exponent - tail.Length);
                    }
                }
                else
                {
                    builder.Append('0');
                    builder.Append(formatInfo.NumberDecimalSeparator);
                    builder.Append('0', (-exponent) - 1);
                    builder.Append(match.Groups["head"].Value);
                    builder.Append(match.Groups["tail"].Value);
                }
                result = builder.ToString();
            }
            return result;
        }

        public static void WriteAllLines(string filename, List<string> lines)
        {

            File.WriteAllText(filename, string.Join(Gerber.LineEnding, lines));


        }

        internal static double ParseDouble(string inp)
        {
            return double.Parse(inp, NumberStyles.Any, CultureInfo.InvariantCulture);
        }

        internal static bool TryParseDouble(string inp, out double N)
        {
            inp = inp.Replace("*", "");
            return double.TryParse(inp, NumberStyles.Any, CultureInfo.InvariantCulture, out N);
        }

        private static void DrawCross(Graphics G2, double X, double Y, Color C)
        {
            float S = 0.2f;
            Pen P = new Pen(C, 1.0f);
            G2.DrawLine(P, (float)X - S, (float)Y - S, (float)X + S, (float)Y - S);
            G2.DrawLine(P, (float)X - S, (float)Y + S, (float)X + S, (float)Y + S);
            G2.DrawLine(P, (float)X - S, (float)Y - S, (float)X - S, (float)Y + S);
            G2.DrawLine(P, (float)X + S, (float)Y - S, (float)X + S, (float)Y + S);

        }

        private static double LimitPos2PI(double dA)
        {
            while (dA < -Math.PI / 2) dA += Math.PI * 2;
            while (dA >= Math.PI / 2) dA -= Math.PI * 2;
            return dA;
        }

        private class Boardset
        {
            public BoardLayer layer;
            public string name; public BoardSide side;
        };

        class QuadR
        {
            public double CX;
            public double CY;
            public double D1 = 0;
            public double D2 = 0;
            public double Diff;
            public double DRat = 0;
            public double E;
            public double S;
            internal void Calc(double LastX, double LastY, double X, double Y)
            {
                double CX1 = LastX - CX;
                double CX2 = X - CX;
                double CY1 = LastY - CY;
                double CY2 = Y - CY;

                D1 = Math.Sqrt(CX1 * CX1 + CY1 * CY1);
                D2 = Math.Sqrt(CX2 * CX2 + CY2 * CY2);
                if (D2 != 0) DRat = D1 / D2;

                S = Math.Atan2(LastY - CY, LastX - CX);
                E = Math.Atan2(Y - CY, X - CX);

            }

            internal void FixClockwise()
            {
                //       while (S < E) S += Math.PI * 2;
                Diff = S - E;
                while (Diff > Math.PI) Diff -= Math.PI * 2;
                //              Console.WriteLine("clock: {0:N2}", Gerber.RadToDeg(Diff));
            }

            internal void FixCounterClockwise()
            {
                while (S > E) S -= Math.PI * 2;
                while (S < 0)
                {
                    S += Math.PI * 2.0;
                    E += Math.PI * 2.0;
                }
                Diff = E - S;

                // while (Diff < 0) Diff += Math.PI * 2.0;
//                Console.WriteLine("counterclock: {0:N2}", Gerber.RadToDeg(Diff));

            }
        }
        #region GERBERCOMMANDSTRINGS
        public static string BuildOutlineApertureMacro(string name, List<PointD> Vertices, GerberNumberFormat format)
        {
            string res = "%AM" + name + "*" + Gerber.LineEnding;
            res += String.Format("4,1,{0}," + Gerber.LineEnding, (Vertices.Count - 2));
            for (int i = 0; i < Vertices.Count - 1; i++)
            {
                res += String.Format("{0},{1}," + Gerber.LineEnding, Gerber.ToFloatingPointString(format._ScaleMMToFile(Vertices[i].X)).Replace(',', '.'), Gerber.ToFloatingPointString(format._ScaleMMToFile(Vertices[i].Y)).Replace(',', '.'));
            }

            res += "0*" + Gerber.LineEnding + "%" + Gerber.LineEnding;
            return res;
        }

        public static string Flash(PointD t, GerberNumberFormat GNF)
        {
            return String.Format("X{0}Y{1}D03*", GNF.Format(GNF._ScaleMMToFile(t.X)), GNF.Format(GNF._ScaleMMToFile(t.Y)));
        }

        public static string LineTo(PointD t, GerberNumberFormat GNF)
        {
            return String.Format("X{0}Y{1}D01*", GNF.Format(GNF._ScaleMMToFile(t.X)), GNF.Format(GNF._ScaleMMToFile(t.Y)));
        }

        public static string MoveTo(PointD t, GerberNumberFormat GNF)
        {
            return String.Format("X{0}Y{1}D02*", GNF.Format(GNF._ScaleMMToFile(t.X)), GNF.Format(GNF._ScaleMMToFile(t.Y)));
        }
        public static string WriteMacroEnd()
        {
            return "" + Gerber.LineEnding + "%" + Gerber.LineEnding;

        }

        public static string WriteMacroPartVertices(List<PointD> Vertices, GerberNumberFormat format)
        {
            string res = "";
            res += String.Format("4,1,{0}," + Gerber.LineEnding, (Vertices.Count - 2));
            for (int i = 0; i < Vertices.Count - 1; i++)
            {
                res += String.Format("{0},{1}," + Gerber.LineEnding, Gerber.ToFloatingPointString(format._ScaleMMToFile(Vertices[i].X)).Replace(',', '.'), Gerber.ToFloatingPointString(format._ScaleMMToFile(Vertices[i].Y)).Replace(',', '.'));
            }
            res += "0*";
            return res;
        }

        public static string WriteMacroStart(string name)
        {
            return "%AM" + name + "*" + Gerber.LineEnding;
        }
#endregion
    }   
}
