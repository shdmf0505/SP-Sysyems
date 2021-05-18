﻿namespace Micube.SmartMES.Commons.Gerber.Core
{
    public enum BoardLayer
    {
        Silk,
        Copper,
        Paste,
        SolderMask,
        Carbon,
        Drill,
        Outline,
        Mill,
        Unknown,
        Notes,
        Assembly
    }

    public enum BoardSide
    {
        Top,
        Bottom,
        Internal,
        Both,
        Unknown,
        Internal1,
        Internal2
    }

    public enum InterpolationMode
    {
        Linear,
        ClockWise,
        CounterClockwise
    }

    public enum Quadrant
    {
        xposypos,
        xnegypos,
        xnegyneg,
        xposyneg

    }

    public enum GerberQuadrantMode
    {
        Multi,
        Single
    }

    public enum BoardFileType
    {
        Gerber,
        Drill,
        Unsupported
    }
}
