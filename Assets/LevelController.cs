using System.Threading.Tasks;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.SceneManagement;

// 状态基类
public abstract class LevelStateBase
{
    protected LevelController levelController;

    public LevelStateBase(LevelController controller)
    {
        levelController = controller;
    }

    public virtual void EnterState() { }
    public virtual void UpdateState() { }
    public virtual void ExitState() { }
}

public class FirstIntroductionState : LevelStateBase
{
    public FirstIntroductionState(LevelController controller) : base(controller) { }

    public async override void EnterState()
    {
        await Task.Delay(100);
        Debug.Log("First Start");
        
        DialogueSystem.instance.Send("Character_1" , levelController.OnFirstIntroductionStateDialogEndDetected);
    }

    public override void UpdateState()
    {
        // FirstIntroduction 状态的更新逻辑
    }

    public override void ExitState()
    {
        
    }
}

public class SecIntroductionState : LevelStateBase
{
    public SecIntroductionState(LevelController controller) : base(controller) {}
    
    public async override void EnterState()
    {
        await Task.Delay(100);
        DialogueSystem.instance.Send("Character_1_Introduction_1" , levelController.OnSecIntroductionStateDialogEndDetected);
    }

    public override void UpdateState()
    {
        // FirstIntroduction 状态的更新逻辑
    }

    public override void ExitState()
    {
        
    }
}

public class ThirdIntroductionState : LevelStateBase
{
    public ThirdIntroductionState(LevelController controller) : base(controller) {}
    
    public async override void EnterState()
    {
        await Task.Delay(100);
        DialogueSystem.instance.Send("Character_1_Introduction_2" , levelController.OnThirdIntroductionStateDialogEndDetected);
    }

    public override void UpdateState()
    {
        // FirstIntroduction 状态的更新逻辑
    }

    public override void ExitState()
    {
        
    }
}

public class FirstLeaderIntroductionState : LevelStateBase
{
    public FirstLeaderIntroductionState(LevelController controller) : base(controller) { }

    public async override void EnterState()
    {
        await Task.Delay(100);
        Debug.Log("First Start");
        
        DialogueSystem.instance.Send("Character_2" , levelController.OnFirstLeaderIntroductionStateDialogEndDetected);
    }

    public override void UpdateState()
    {
        // FirstIntroduction 状态的更新逻辑
    }

    public override void ExitState()
    {
        
    }
}

public class SecLeaderIntroductionState : LevelStateBase
{
    public SecLeaderIntroductionState(LevelController controller) : base(controller) {}
    
    public async override void EnterState()
    {
        await Task.Delay(100);
        DialogueSystem.instance.Send("Character_2_Introduction_1" , levelController.OnSecLeaderIntroductionStateDialogEndDetected);
    }

    public override void UpdateState()
    {
        // FirstIntroduction 状态的更新逻辑
    }

    public override void ExitState()
    {
        
    }
}

public class ThirdLeaderIntroductionState : LevelStateBase
{
    public ThirdLeaderIntroductionState(LevelController controller) : base(controller) {}
    
    public async override void EnterState()
    {
        await Task.Delay(100);
        DialogueSystem.instance.Send("Character_2_Introduction_2" , levelController.OnThirdLeaderIntroductionStateDialogEndDetected);
    }

    public override void UpdateState()
    {
        // FirstIntroduction 状态的更新逻辑
    }

    public override void ExitState()
    {
        
    }
}

public class FirstMissionaryIntroductionState : LevelStateBase
{
    public FirstMissionaryIntroductionState(LevelController controller) : base(controller) { }

    public async override void EnterState()
    {
        await Task.Delay(100);
        Debug.Log("First Start");
        
        DialogueSystem.instance.Send("Character_3" , levelController.OnFirstMissionaryIntroductionStateDialogEndDetected);
    }

    public override void UpdateState()
    {
        // FirstIntroduction 状态的更新逻辑
    }

    public override void ExitState()
    {
        
    }
}

public class SecMissionaryIntroductionState : LevelStateBase
{
    public SecMissionaryIntroductionState(LevelController controller) : base(controller) {}
    
    public async override void EnterState()
    {
        await Task.Delay(100);
        DialogueSystem.instance.Send("Character_3_Introduction_1" , levelController.OnSecMissionaryIntroductionStateDialogEndDetected);
    }

    public override void UpdateState()
    {
        // FirstIntroduction 状态的更新逻辑
    }

    public override void ExitState()
    {
        
    }
}

public class ThirdMissionaryIntroductionState : LevelStateBase
{
    public ThirdMissionaryIntroductionState(LevelController controller) : base(controller) {}
    
    public async override void EnterState()
    {
        await Task.Delay(100);
        DialogueSystem.instance.Send("Character_3_Introduction_2" , levelController.OnThirdMissionaryIntroductionStateDialogEndDetected);
    }

    public override void UpdateState()
    {
        // FirstIntroduction 状态的更新逻辑
    }

    public override void ExitState()
    {
        
    }
}

public class LevelController : MonoBehaviour
{
    private LevelStateBase currentState;

    public Animator character_1, character_2 , character_3;

    public GameObject theEnd;


    private void Start()
    {
        Debug.Log("Start");
        
        currentState = new FirstIntroductionState(this);
        currentState.EnterState();
    }

    private void Update()
    {
        currentState?.UpdateState();
    }

    public void SetState(LevelStateBase newState)
    {
        currentState?.ExitState();
        currentState = newState;
        currentState.EnterState();
    }

    public void OnFirstIntroductionStateDialogEndDetected()
    {
        Debug.Log("First End");
        var selectPanel = GameObject.FindWithTag("SelectPanel").GetComponent<CanvasGroup>();

        selectPanel.interactable = true;
        selectPanel.blocksRaycasts = true;
        selectPanel.alpha = 1;
    }
    
    public void OnSecIntroductionStateDialogEndDetected()
    {
        Debug.Log("Sec End");

        SetState(new ThirdIntroductionState(this));
    }

    public void SelectFirstCurrentAnswer()
    {
        Debug.Log("Current Answer");
        var selectPanel = GameObject.FindWithTag("SelectPanel").GetComponent<CanvasGroup>();
        
        selectPanel.interactable = false;
        selectPanel.blocksRaycasts = false;
        selectPanel.alpha = 0;

        SetState(new SecIntroductionState(this));
    }

    public void OnThirdIntroductionStateDialogEndDetected()
    {
        Debug.Log("Third End");
        var player = GameObject.FindWithTag("Player");
        var XRPlayer = GameObject.FindObjectOfType<XROrigin>();
        var SystemAchor = GameObject.FindWithTag("System");
        var playerPoint = GameObject.FindWithTag("Point_2").transform.position;

        //player.transform.Rotate(0,90,0);
        XRPlayer.transform.Rotate(0,-90,0);
        SystemAchor.transform.Rotate(0,-90,0);

        DialogueSystem.instance.isLock = true;
        
        BlackEffectSystem.instance.onFadeInExit = delegate
        {
            player.transform.position = playerPoint;
            
            BlackEffectSystem.instance.SetFadeOut();
            DialogueSystem.instance.isLock = false;
            
            Debug.Log("Here is call back");
            
            BlackEffectSystem.instance.onFadeInExit = delegate {  };
        };
        BlackEffectSystem.instance.SetFadeIn();
        
        character_1.Play("Happy Idle");
        character_2.Play("Explane_HandDirect");
        
        SetState(new FirstLeaderIntroductionState(this));
    }
    
    public void OnFirstLeaderIntroductionStateDialogEndDetected()
    {
        Debug.Log("First End");
        var selectPanel = GameObject.FindWithTag("SelectPanel2").GetComponent<CanvasGroup>();
        
        selectPanel.interactable = true;
        selectPanel.blocksRaycasts = true;
        selectPanel.alpha = 1;
    }
    
    public void SelectSecCurrentAnswer()
    {
        Debug.Log("Current Answer");
        var selectPanel = GameObject.FindWithTag("SelectPanel2").GetComponent<CanvasGroup>();
        
        selectPanel.interactable = false;
        selectPanel.blocksRaycasts = false;
        selectPanel.alpha = 0;

        SetState(new SecLeaderIntroductionState(this));
    }
    
    public void OnSecLeaderIntroductionStateDialogEndDetected()
    {
        Debug.Log("Sec End");
        
        SetState(new ThirdLeaderIntroductionState(this));
    }
    
    public void OnThirdLeaderIntroductionStateDialogEndDetected()
    {
        Debug.Log("Third End");
        var player = GameObject.FindWithTag("Player");
        var XRPlayer = GameObject.FindObjectOfType<XROrigin>();
        var SystemAchor = GameObject.FindWithTag("System");
        var playerPoint = GameObject.FindWithTag("Point_3").transform.position;

        //player.transform.Rotate(0,90,0);
        XRPlayer.transform.Rotate(0,0,0);
        SystemAchor.transform.Rotate(0,0,0);

        BlackEffectSystem.instance.onFadeInExit = null;
        DialogueSystem.instance.isLock = true;
        
        BlackEffectSystem.instance.onFadeInExit = delegate
        {
            player.transform.position = playerPoint;
            
            BlackEffectSystem.instance.SetFadeOut();
            DialogueSystem.instance.isLock = false;
            
            Debug.Log("Black Effect Last End");
            
            BlackEffectSystem.instance.onFadeInExit = delegate {  };
        };
        BlackEffectSystem.instance.SetFadeIn();
        
        character_2.Play("Happy Idle");
        character_3.Play("Explane_HandDirect");
        
        SetState(new FirstMissionaryIntroductionState(this));
        
    }

    public void OnFirstMissionaryIntroductionStateDialogEndDetected()
    {
        Debug.Log("First End");
        var selectPanel = GameObject.FindWithTag("SelectPanel3").GetComponent<CanvasGroup>();
        
        selectPanel.interactable = true;
        selectPanel.blocksRaycasts = true;
        selectPanel.alpha = 1;
    }
    
    public void SelectThirdCurrentAnswer()
    {
        Debug.Log("Current Answer");
        var selectPanel = GameObject.FindWithTag("SelectPanel3").GetComponent<CanvasGroup>();
        
        selectPanel.interactable = false;
        selectPanel.blocksRaycasts = false;
        selectPanel.alpha = 0;

        SetState(new SecMissionaryIntroductionState(this));
    }

    public void OnSecMissionaryIntroductionStateDialogEndDetected()
    {
        Debug.Log("Current Answer");
        var selectPanel = GameObject.FindWithTag("SelectPanel3").GetComponent<CanvasGroup>();
        
        selectPanel.interactable = false;
        selectPanel.blocksRaycasts = false;
        selectPanel.alpha = 0;

        SetState(new ThirdMissionaryIntroductionState(this));
    }

    public void OnThirdMissionaryIntroductionStateDialogEndDetected()
    {
        theEnd.SetActive(true);
    }

    public void ReloadScenes()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}