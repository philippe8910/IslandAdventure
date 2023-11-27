using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BlackEffectSystem : SingletonService<BlackEffectSystem>
{
    public Material material; // 你想要控制的Material
    public float duration = 2f; // 动画持续时间

    public bool isTransparent;

    public Action onFadeInExit;

    void Start()
    {
        material.color = Color.black;
        SetFadeOut();
    }

    public void SetFadeOut()
    {
        material.SetFloat("_SurfaceType", isTransparent ? 1 : 0);
        
        // 使用DOTween动画来改变Material的透明度
        var dotTweenerCore = material.DOColor(new Color(0f, 0f, 0f, 0f), duration)
            .OnComplete(OnCompleteCallback); // 动画完成时调用回调函数

        DOTween.Kill(dotTweenerCore);
    }

    public void SetFadeIn()
    {
        // 使用DOTween动画来改变Material的透明度
        var dotTweenerCore =  material.DOColor(new Color(0f, 0f, 0f, 1f), duration)
            .OnComplete(OnCompleteCallback); // 动画完成时调用回调函数

        DOTween.Kill(dotTweenerCore);
    }

    public void OnCompleteCallback()
    {
        Debug.Log("透明度已经降为0了！");
        onFadeInExit?.Invoke();
        // 在这里添加你想要在透明度降为0时执行的代码
    }
}
