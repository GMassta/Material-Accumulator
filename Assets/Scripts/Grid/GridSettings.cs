using UnityEngine;

namespace Grid
{
    [CreateAssetMenu(fileName = "GridSettings", menuName = "Settings/GridSettings", order = 0)]
    public class GridSettings : ScriptableObject
    {
        public int resolutionX = 100;
        public int resolutionZ = 100;
        public float sizeX = 10;
        public float sizeZ = 10;
    }
}