using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    public void EnterState(object action);
    public void StayState(object action);
    public void ExitState(object action);
}
