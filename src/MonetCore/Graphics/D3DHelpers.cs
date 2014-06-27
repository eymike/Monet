using System.Collections.Generic;

using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;

static class D3DHelper
{
    private static Dictionary<PrimitiveTopology, uint> s_topologySize = new Dictionary<PrimitiveTopology,uint>()
    {
        {PrimitiveTopology.PointList, 1},
        {PrimitiveTopology.TriangleList, 3},
        {PrimitiveTopology.TriangleListWithAdjacency, 3},
        {PrimitiveTopology.TriangleStrip, 3},
        {PrimitiveTopology.TriangleStripWithAdjacency, 3},
        {PrimitiveTopology.Undefined, 0},
        {PrimitiveTopology.LineList, 2},
        {PrimitiveTopology.LineListWithAdjacency, 2},
        {PrimitiveTopology.LineStrip, 2},
        {PrimitiveTopology.LineStripWithAdjacency, 2},
        {PrimitiveTopology.PatchListWith1ControlPoints, 1},
        {PrimitiveTopology.PatchListWith2ControlPoints, 2},
        {PrimitiveTopology.PatchListWith3ControlPoints, 3},
        {PrimitiveTopology.PatchListWith4ControlPoints, 4},
        {PrimitiveTopology.PatchListWith5ControlPoints, 5},
        {PrimitiveTopology.PatchListWith6ControlPoints, 6},
        {PrimitiveTopology.PatchListWith7ControlPoints, 7},
        {PrimitiveTopology.PatchListWith8ControlPoints, 8},
        {PrimitiveTopology.PatchListWith9ControlPoints, 9},
        {PrimitiveTopology.PatchListWith10ControlPoints, 10},
        {PrimitiveTopology.PatchListWith11ControlPoints, 11},
        {PrimitiveTopology.PatchListWith12ControlPoints, 12},
        {PrimitiveTopology.PatchListWith13ControlPoints, 13},
        {PrimitiveTopology.PatchListWith14ControlPoints, 14},
        {PrimitiveTopology.PatchListWith15ControlPoints, 15},
        {PrimitiveTopology.PatchListWith16ControlPoints, 16},
        {PrimitiveTopology.PatchListWith17ControlPoints, 17},
        {PrimitiveTopology.PatchListWith18ControlPoints, 18},
        {PrimitiveTopology.PatchListWith19ControlPoints, 19},
        {PrimitiveTopology.PatchListWith20ControlPoints, 20},
        {PrimitiveTopology.PatchListWith21ControlPoints, 21},
        {PrimitiveTopology.PatchListWith22ControlPoints, 22},
        {PrimitiveTopology.PatchListWith23ControlPoints, 23},
        {PrimitiveTopology.PatchListWith24ControlPoints, 24},
        {PrimitiveTopology.PatchListWith25ControlPoints, 25},
        {PrimitiveTopology.PatchListWith26ControlPoints, 26},
        {PrimitiveTopology.PatchListWith27ControlPoints, 27},
        {PrimitiveTopology.PatchListWith28ControlPoints, 28},
        {PrimitiveTopology.PatchListWith29ControlPoints, 29},
        {PrimitiveTopology.PatchListWith30ControlPoints, 30},
        {PrimitiveTopology.PatchListWith31ControlPoints, 31},
        {PrimitiveTopology.PatchListWith32ControlPoints, 32}
    };
    
    public static uint PrimitiveSizeForTopology(PrimitiveTopology topology)
    {
        return s_topologySize[topology]; 
    }
}