using Grid;
using SphereZone;
using UnityEngine;

namespace Accumulator
{
    public sealed class MaterialAccumulator
    {
        private readonly HeightMapData _heightMap;

        public MaterialAccumulator(HeightMapData heightMap)
        {
            _heightMap = heightMap;
        }

        //Поднимает высоту во всех узлах сетки в пределах zone на delta
        public void Accumulate(AccumulationZone zone, float delta)
        {
            zone.GetWorldBounds(out var minX, out var maxX, out var minZ, out var maxZ);

            _heightMap.WorldToGrid(new Vector3(minX, 0f, minZ), out var xMin, out var zMin);
            _heightMap.WorldToGrid(new Vector3(maxX, 0f, maxZ), out var xMax, out var zMax);

            xMin = Mathf.Clamp(xMin, 0, _heightMap.ResolutionX - 1);
            xMax = Mathf.Clamp(xMax, 0, _heightMap.ResolutionX - 1);
            zMin = Mathf.Clamp(zMin, 0, _heightMap.ResolutionZ - 1);
            zMax = Mathf.Clamp(zMax, 0, _heightMap.ResolutionZ - 1);

            for (var z = zMin; z <= zMax; z++)
            {
                for (var x = xMin; x <= xMax; x++)
                {
                    var worldPoint = new Vector3(x * _heightMap.CellSizeX, 0f, z * _heightMap.CellSizeZ);
                    if (!zone.TryGetHeightCap(worldPoint, out var cap))
                        continue;

                    var current = _heightMap.GetHeight(x, z);
                    var target = Mathf.Min(current + delta, cap);
                    _heightMap.RaiseHeight(x, z, target);
                }
            }
        }
    }
}