using LevelManagement;
using LevelManagement.DataPersistence;
using MarblesAndMonsters.Menus;
using Zenject;

public class ManagersInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<MenuManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<DataManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<LevelManager>().FromComponentInHierarchy().AsSingle();
    }
}