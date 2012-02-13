﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Transvoxel.VolumeData.CompactOctree;

namespace Transvoxel.SurfaceExtractor
{
    class CellData
    {
        public uint[] index = new uint[4];
    }

    // For Vertex reusing we need access to 2 cell slices of the current polygonized cube
    // First one is the current slice, second one is the previous slice (sliceA,sliceB)
    // the order of sliceA and B alternates for each calculated slice in the cube
    
    public class CellCache
    {
        public const uint NOINDEX = 0xFFFFFFFF;
        private readonly CellData[, ,] slices = new CellData[2, VolumeChunk.CHUNKSIZE, VolumeChunk.CHUNKSIZE];

        public CellCache()
        {
            ClearSlice(0);
            ClearSlice(1);
        }        
        
        public void setReuseIndex(int x,int y,int z,int rind,uint val)
        {
            CellData cachePlace = slices[x%2,y,z];
            cachePlace.index[rind] = val;
        }

        public uint getReuseIndex(int x, int y, int z,int rind)
        {
            CellData cachePlace = slices[x % 2, y, z];
            return cachePlace.index[rind];
        }

        public void ClearSlice(int x)
        { 
            for(int y=0;y<VolumeChunk.CHUNKSIZE;y++)
                for (int z = 0; z < VolumeChunk.CHUNKSIZE; z++)
                    for (int a = 0; a < 4; a++)
                    {
                        slices[x%2, y, z].index[a] = NOINDEX;
                    }
        }
    }
}