namespace MarblesAndMonsters.Items
{
    //for all your locking needs
    public interface ILockable
    {
        //no key required to lock a lockable item
        public bool Lock();

        //returns true if testKey unlocks else false
        public bool Unlock();
    }
}
