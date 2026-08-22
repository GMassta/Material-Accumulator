using Accumulator;
using DefaultNamespace;
using DefaultNamespace.Ui;
using Grid;
using SphereZone;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class RootLifeTimeScope: LifetimeScope
{
    [SerializeField] private UiObject ui;
    [Space(10)]
    [SerializeField] private MeshView meshView;
    [SerializeField] private ZoneView zoneView;
    [Space(10)]
    [SerializeField] private GridSettings gridSettings;
    [SerializeField] private Settings settings;
    
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<InputSystem_Actions>(Lifetime.Singleton);

        builder.RegisterInstance(meshView).As<IMeshView>();
        builder.RegisterInstance(zoneView).As<IZoneView>();

        builder.RegisterComponent(ui);
        builder.RegisterInstance(settings);
        builder.RegisterInstance(gridSettings);

        builder.Register<MeshPresenter>(Lifetime.Singleton);
        builder.Register<ZonePresenter>(Lifetime.Singleton);
        
        builder.Register<KeyboardZoneInput>(Lifetime.Singleton).As<IZoneInput>();
        
        builder.Register<HeightMapData>(Lifetime.Singleton).AsSelf();
        builder.Register<AccumulationZone>(Lifetime.Singleton);
        builder.Register<RadiusOscillator>(Lifetime.Singleton);
        builder.Register<MaterialAccumulator>(Lifetime.Singleton);
        builder.Register<TrajectoryStepper>(Lifetime.Singleton);
        builder.Register<ZoneController>(Lifetime.Singleton);

        builder.RegisterEntryPoint<SceneEntryPoint>().AsSelf();
    }
}