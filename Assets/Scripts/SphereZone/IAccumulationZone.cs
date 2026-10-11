using UnityEngine;

namespace SphereZone
{
    public interface IAccumulationZone
    {
        void GetWorldBounds(out float minX, out float maxX, out float minZ, out float maxZ);
        bool TryGetHeightCap(Vector3 worldPoint, out float cap);
    }
}