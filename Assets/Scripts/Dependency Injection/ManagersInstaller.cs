using LevelManagement;
using LevelManagement.DataPersistence;
using MarblesAndMonsters;
using MarblesAndMonsters.Menus;
using Zenject;

public class ManagersInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<MenuManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<DataManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<LevelManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<GameManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<CharacterManager>().FromComponentInHierarchy().AsSingle();
    }
}