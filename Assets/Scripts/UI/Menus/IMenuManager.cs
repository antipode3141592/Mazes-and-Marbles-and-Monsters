namespace MarblesAndMonsters.Menus
{
    public interface IMenuManager
    {
        void CloseMenu();
        void OpenMenu(MenuTypes menuType);
    }
}