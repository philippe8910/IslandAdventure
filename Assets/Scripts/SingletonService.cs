using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonService<T> : MonoBehaviour
{
    public static T instance;

    private void Awake()
    {
        instance = GetComponent<T>();
    }

    public T Get()
    {
        return instance;
    }
}
