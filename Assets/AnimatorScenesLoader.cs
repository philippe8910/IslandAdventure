using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimatorScenesLoader : MonoBehaviour
{
        private Animator animator;
        public string nextSceneName;
    
        void Start()
        {
            // 獲取物體上的Animator組件
            animator = GetComponent<Animator>();
    
            // 訂閱動畫事件，當動畫播放完畢時調用OnAnimationComplete函數
            AnimationEvent animationEvent = new AnimationEvent();
            animationEvent.functionName = "OnAnimationComplete";
            animationEvent.time = animator.runtimeAnimatorController.animationClips[0].length; // 這裡假設只有一個動畫Clip
            animator.runtimeAnimatorController.animationClips[0].AddEvent(animationEvent);
        }
    
        // 動畫播放完畢時調用的函數
        void OnAnimationComplete()
        {
            // 切換場景
            SceneManager.LoadScene(nextSceneName);
        }
}
