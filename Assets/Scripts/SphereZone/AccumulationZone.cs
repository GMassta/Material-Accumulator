using UnityEngine;

namespace SphereZone
{
    public sealed class AccumulationZone
    {
        public Vector3 Position { get; private set; }
        public float CurrentRadius { get; private set; }

        public void SetPosition(Vector3 position) => Position = position;
        public void SetRadius(float radius) => CurrentRadius = Mathf.Max(0f, radius);


        public bool TryGetHeightCap(Vector3 worldPoint, out float cap)
        {
            var dx = worldPoint.x - Position.x;
            var dz = worldPoint.z - Position.z;
            var distSq = dx * dx + dz * dz;
            var radiusSq = CurrentRadius * CurrentRadius;

            if (distSq > radiusSq)
            {
                cap = 0f;
                return false;
            }

            // Уравнение полусферы радиуса R с центром в Position:
            // это высота полусферы в этой точке.
            cap = Mathf.Sqrt(radiusSq - distSq);
            return true;
        }
        
        public void GetWorldBounds(out float minX, out float maxX, out float minZ, out float maxZ)
        {
            minX = Position.x - CurrentRadius;
            maxX = Position.x + CurrentRadius;
            minZ = Position.z - CurrentRadius;
            maxZ = Position.z + CurrentRadius;
        }
    }
}