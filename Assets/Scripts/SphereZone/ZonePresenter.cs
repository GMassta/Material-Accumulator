using Accumulator;
using VContainer.Unity;

namespace SphereZone
{
    public sealed class ZonePresenter
    {
        private readonly AccumulationZone _zone;
        private readonly IZoneView _view;

        public ZonePresenter(AccumulationZone zone, IZoneView view)
        {
            _zone = zone;
            _view = view;
        }

        public void Sync()
        {
            _view.SetPosition(_zone.Position);
            _view.SetRadius(_zone.CurrentRadius);
        }
    }
}