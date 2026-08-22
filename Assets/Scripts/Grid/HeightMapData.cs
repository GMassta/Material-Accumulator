using System;
using UnityEngine;

namespace Grid
{
    public sealed class HeightMapData
    {
        public int ResolutionX { get; }
        public int ResolutionZ { get; }
        public float Width { get; }
        public float Depth { get; }
        public float CellSizeX { get; }
        public float CellSizeZ { get; }

        private readonly float[] _heights;
        
        private GridBounds _dirtyRegion;
        
        public HeightMapData(GridSettings config)
        {
            if (config.resolutionX < 2 || config.resolutionZ < 2)
                throw new ArgumentException("Resolution must be at least 2x2.");

            ResolutionX = config.resolutionX;
            ResolutionZ = config.resolutionZ;
            Width = config.sizeX;
            Depth = config.sizeZ;
            CellSizeX = Width / (ResolutionX - 1);
            CellSizeZ = Depth / (ResolutionZ - 1);

            _heights = new float[ResolutionX * ResolutionZ];
            _dirtyRegion = GridBounds.Empty;
        }

        public int Index(int x, int z) => z * ResolutionX + x;
        
        public float GetHeight(int x, int z)
        {
            x = Mathf.Clamp(x, 0, ResolutionX - 1);
            z = Mathf.Clamp(z, 0, ResolutionZ - 1);
            return _heights[Index(x, z)];
        }

        public Vector3 GetWorldPosition(int x, int z) => 
            new Vector3(x * CellSizeX, GetHeight(x, z), z * CellSizeZ);

        public void WorldToGrid(Vector3 worldPosition, out int x, out int z)
        {
            x = Mathf.RoundToInt(worldPosition.x / CellSizeX);
            z = Mathf.RoundToInt(worldPosition.z / CellSizeZ);
        }
        
        public void RaiseHeight(int x, int z, float targetHeight)
        {
            if (x < 0 || x >= ResolutionX || z < 0 || z >= ResolutionZ)
                return;

            var i = Index(x, z);
            if (targetHeight <= _heights[i])
                return;

            _heights[i] = targetHeight;
            _dirtyRegion = _dirtyRegion.Encapsulate(x, z);
        }
        
        public void Reset()
        {
            Array.Clear(_heights, 0, _heights.Length);
            _dirtyRegion = new GridBounds(0, ResolutionX - 1, 0, ResolutionZ - 1);
        }
        
        public GridBounds ConsumeDirtyRegion()
        {
            var region = _dirtyRegion;
            _dirtyRegion = GridBounds.Empty;
            return region;
        }
    }

}