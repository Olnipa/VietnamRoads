using UnityEngine;
using Zenject;

public class TestInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ProvinceLevelIncreaser>().FromInstance(new ProvinceLevelIncreaser());
        Container.Bind<PriceList>().AsSingle().NonLazy();
    }
}