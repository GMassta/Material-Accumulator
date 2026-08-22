using Grid;
using SphereZone;
using UnityEngine;
using VContainer.Unity;

namespace DefaultNamespace
{
    public class SceneEntryPoint: ITickable
    {
        private readonly ZoneController _zoneController;
        private readonly ZonePresenter _zonePresenter;
        private readonly MeshPresenter _meshPresenter;
        
        public SceneEntryPoint(            
            ZoneController zoneController,
            ZonePresenter zonePresenter,
            MeshPresenter meshPresenter)
        {
            _zoneController = zoneController;
            _zonePresenter = zonePresenter;
            _meshPresenter = meshPresenter;
        }

        public void Tick()
        {
            _zoneController.Tick(Time.deltaTime);
            _zonePresenter.Sync();
            _meshPresenter.Refresh();
        }
    }
}