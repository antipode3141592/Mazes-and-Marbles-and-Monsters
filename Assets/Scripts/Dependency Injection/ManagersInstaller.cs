using LevelManagement;
using LevelManagement.DataPersistence;
using MarblesAndMonsters;
using MarblesAndMonsters.Managers;
using MarblesAndMonsters.Menus;
using Zenject;

public class ManagersInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IMenuManager>().To<MenuManager>()
            .FromComponentInHierarchy().AsSingle();
        Container.Bind<IDataManager>().To<DataManager>()
            .FromComponentInHierarchy().AsSingle();
        Container.Bind<ILevelManager>().To<LevelManager>()
            .FromComponentInHierarchy().AsSingle();
        Container.Bind<IGameManager>().To<GameManager>().
            FromComponentInHierarchy().AsSingle();
        Container.Bind<IInputManager>().To<InputManager>()
            .FromComponentInHierarchy().AsSingle();
        Container.Bind<ICharacterManager>().To<CharacterManager>()
            .FromComponentInHierarchy().AsSingle();
        Container.Bind<ITimeTracker>().To<TimeTracker>()
            .FromComponentInHierarchy().AsSingle();
        Container.Bind<ICameraManager>().To<CameraManager>()
            .FromComponentInHierarchy().AsSingle();
        Container.Bind<IAudioManager>().To<AudioManager>()
            .FromComponentInHierarchy().AsSingle();
    }
}