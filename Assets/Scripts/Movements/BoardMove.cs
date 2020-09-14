namespace MarblesAndMonsters.Actions
{

    //  default board movement action, applies force during Update proportional to device tilt vector
    //      bool    _moving     whether or not the board moves the object (can still be pushed)
    //      float   _forceMultiplier    the default acceleration (m/s^2)
    public class BoardMove: Movement<BoardMove>
    {
        protected bool _moving = true;  //default to moving state

        public bool Moving { get { return _moving; } set { _moving = value; } }

        //all board movable objects will receive a force vector to move
        public override void Move()
        {
            if (gameObject.activeInHierarchy && _moving)
            {
                base.Move();
            }
            
        }
    }

}
