using UnityEngine;

namespace Grid
{
    [RequireComponent(typeof(MeshFilter))]
    public sealed class MeshView : MonoBehaviour, IMeshView
    {
        [SerializeField] private MeshFilter _meshFilter;

        public void SetMesh(Mesh mesh)
        {
            _meshFilter.sharedMesh = mesh;
        }
    }

}