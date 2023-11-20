using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SystemLoader : MonoBehaviour
{
    private void Awake()
    {
        SceneManager.LoadSceneAsync("SystemScene", LoadSceneMode.Additive);
        //SceneManager.LoadSceneAsync("InterationScenes", LoadSceneMode.Additive);
    }
}
