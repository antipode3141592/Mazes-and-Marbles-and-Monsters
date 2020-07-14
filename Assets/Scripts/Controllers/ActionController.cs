using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarblesAndMonsters;

public class ActionController : MonoBehaviour
{
    //[SerializeField]
    protected List<Action> AvailableActions;
    [SerializeField]
    protected bool _isAwake = true;  //objects can be awake or asleep.  board only executes actions on awake objects

    public bool isAwake{ get { return _isAwake; } set { _isAwake = value; }}

    //Upon awake, populate list with all Actions attached to the parent gameObject
    void Awake()
    {
        AvailableActions = new List<Action>(gameObject.GetComponents<Action>());
    }

    // Update is called once per frame
    void Update()
    {
        if (_isAwake)
        {
            //awake!  do all available actions
            foreach(Action _action in AvailableActions)
            {
                //do the action
            }
        } else
        {
            //still sleeping
        }
    }

    public virtual bool DoAction(Action action)
    {

        return false;
    }
}
