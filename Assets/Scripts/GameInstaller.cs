
using System.ComponentModel;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public Bonus BonusPrefab;
    public override void InstallBindings()
    {
        Container.Bind<IInteracable>().To<Bonus>();

        Container.Bind<IInteracable>().To<PathPart>();

        //Container.BindFactory<Bonus, BonusSpawner.Factory>().FromComponentInNewPrefab(BonusPrefab);
        //Container.Bind<IGreeting>().To<Greeting>().AsSingle();
        //base.InstallBindings();
    }
}
