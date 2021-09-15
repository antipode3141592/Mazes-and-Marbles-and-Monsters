using MarblesAndMonsters;
using UnityEngine;
using Zenject;

public class ProjectileFactoryInstaller : MonoInstaller<ProjectileFactoryInstaller>
{
    public GameObject ProjectilePrefab;

    public override void InstallBindings()
    {
        Container.Bind<ProjectilePooler>().AsSingle();

        Container.BindMemoryPool<Projectile, Projectile.Pool>()
            .WithInitialSize(6)
            .FromComponentInNewPrefab(ProjectilePrefab);
            //.UnderTransformGroup("Foos");
    }
}
