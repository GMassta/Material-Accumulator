using UnityEngine;

namespace Grid
{
    // Накапливает границы изменившихся с последнего чтения ячеек.
    // Позволяет MeshPresenter обновлять только реально задетые вершины,
    // а не весь меш каждый кадр.
    
    public readonly struct GridBounds
    {
        public readonly int XMin, XMax, ZMin, ZMax;

        public bool IsEmpty => XMin > XMax || ZMin > ZMax;

        public static readonly GridBounds Empty = new GridBounds(0, -1, 0, -1);

        public GridBounds(int xMin, int xMax, int zMin, int zMax)
        {
            XMin = xMin;
            XMax = xMax;
            ZMin = zMin;
            ZMax = zMax;
        }
        
        public GridBounds Encapsulate(int x, int z)
        {
            return IsEmpty 
                ? new GridBounds(x, x, z, z) 
                : new GridBounds(Mathf.Min(XMin, x), Mathf.Max(XMax, x), Mathf.Min(ZMin, z), Mathf.Max(ZMax, z));
        }
        
        public GridBounds Expanded(int padding, int maxX, int maxZ)
        {
            if (IsEmpty)
                return this;

            return new GridBounds(Mathf.Max(0, XMin - padding), Mathf.Min(maxX, XMax + padding),
                Mathf.Max(0, ZMin - padding), Mathf.Min(maxZ, ZMax + padding));
        }
    }
}