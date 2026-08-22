using UnityEngine;
using UnityEngine.Rendering;

namespace Grid
{
    public sealed class MeshPresenter
    {
        private readonly HeightMapData _heightMap;
        private readonly IMeshView _view;

        private readonly Mesh _mesh;
        
        //Кеширую вершины и нормали
        private readonly Vector3[] _vertices;
        private readonly Vector3[] _normals;

        private float _offsetX;
        private float _offsetZ;

        public MeshPresenter(HeightMapData heightMap, IMeshView view)
        {
            _heightMap = heightMap;
            _view = view;

            var resX = heightMap.ResolutionX;
            var resZ = heightMap.ResolutionZ;
            var vertexCount = resX * resZ;

            _offsetX = resX * .5f;
            _offsetZ = resZ * .5f;

            _vertices = new Vector3[vertexCount];
            _normals = new Vector3[vertexCount];
            var uv = new Vector2[vertexCount];

            for (var z = 0; z < resZ; z++)
            {
                for (var x = 0; x < resX; x++)
                {
                    var i = heightMap.Index(x, z);
                    _vertices[i] = heightMap.GetWorldPosition(x, z);
                    _normals[i] = Vector3.up;
                    uv[i] = new Vector2((float)x / (resX - 1), (float)z / (resZ - 1));
                }
            }

            var triangles = new int[(resX - 1) * (resZ - 1) * 6];
            var t = 0;
            for (var z = 0; z < resZ - 1; z++)
            {
                for (var x = 0; x < resX - 1; x++)
                {
                    var i00 = heightMap.Index(x, z);
                    var i10 = heightMap.Index(x + 1, z);
                    var i01 = heightMap.Index(x, z + 1);
                    var i11 = heightMap.Index(x + 1, z + 1);

                    triangles[t++] = i00;
                    triangles[t++] = i01;
                    triangles[t++] = i11;
                    triangles[t++] = i00;
                    triangles[t++] = i11;
                    triangles[t++] = i10;
                }
            }

            _mesh = new Mesh();
            
            _mesh.MarkDynamic();
            _mesh.vertices = _vertices;
            _mesh.triangles = triangles;
            _mesh.normals = _normals;
            _mesh.uv = uv;
            _mesh.RecalculateBounds();

            _view.SetMesh(_mesh);
        }
        
        public void Refresh()
        {
            var dirty = _heightMap.ConsumeDirtyRegion();
            if (dirty.IsEmpty)
                return;

            var maxIndexX = _heightMap.ResolutionX - 1;
            var maxIndexZ = _heightMap.ResolutionZ - 1;

            UpdateVertices(dirty, out var peakX, out var peakZ, out var peakHeight);
            
            //На одну вершину больше, для плавности
            var normalsRegion = dirty.Expanded(1, maxIndexX, maxIndexZ);
            UpdateNormals(normalsRegion);

            _mesh.SetVertices(_vertices);
            _mesh.SetNormals(_normals);
        }

        private void UpdateVertices(GridBounds dirty, out int peakX, out int peakZ, out float peakHeight)
        {
            peakHeight = float.NegativeInfinity;
            peakX = dirty.XMin;
            peakZ = dirty.ZMin;

            for (var z = dirty.ZMin; z <= dirty.ZMax; z++)
            {
                for (var x = dirty.XMin; x <= dirty.XMax; x++)
                {
                    var h = _heightMap.GetHeight(x, z);
                    _vertices[_heightMap.Index(x, z)].y = h;

                    if (!(h > peakHeight)) continue;

                    peakHeight = h;
                    peakX = x;
                    peakZ = z;
                }
            }
        }

        private void UpdateNormals(GridBounds region)
        {
            for (var z = region.ZMin; z <= region.ZMax; z++)
            {
                for (var x = region.XMin; x <= region.XMax; x++)
                {
                    _normals[_heightMap.Index(x, z)] = ComputeNormal(x, z);
                }
            }
        }
        
        private Vector3 ComputeNormal(int x, int z)
        {
            var hL = _heightMap.GetHeight(x - 1, z);
            var hR = _heightMap.GetHeight(x + 1, z);
            var hD = _heightMap.GetHeight(x, z - 1);
            var hU = _heightMap.GetHeight(x, z + 1);

            var tangentX = new Vector3(2f * _heightMap.CellSizeX, hR - hL, 0f);
            var tangentZ = new Vector3(0f, hU - hD, 2f * _heightMap.CellSizeZ);
            return Vector3.Cross(tangentZ, tangentX).normalized;
        }
    }

}