using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionSystem : SingletonService<SelectionSystem>
{
    private void Start()
    {
        OnLevelEnter();
    }

    private void Update()
    {
        OnLevelStay();
    }

    public virtual void OnLevelEnter()
    {
        Debug.Log("Level Start!!!!");
    }

    public virtual void OnLevelStay()
    {
        Debug.Log("Level Stay!!!!");
    }

    public virtual void OnLevelExit()
    {
        Debug.Log("Level Complete!!!!");
    }
    
}
