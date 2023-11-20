using System.Threading.Tasks;
using UnityEngine;

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
        
        DialogueSystem.instance.Send("Test_01" , levelController.OnFirstIntroductionStateDialogEndDetected);
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
        DialogueSystem.instance.Send("Test_02" , levelController.OnSecIntroductionStateDialogEndDetected);
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
        DialogueSystem.instance.Send("Test_03" , levelController.OnThirdIntroductionStateDialogEndDetected);
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


    private void Start()
    {
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
        SetState(new SecIntroductionState(this));
    }
    
    public void OnSecIntroductionStateDialogEndDetected()
    {
        Debug.Log("Sec End");
        var selectPanel = GameObject.FindWithTag("SelectPanel").GetComponent<CanvasGroup>();
        selectPanel.alpha = 1;
    }

    public void SelectCurrentAnswer()
    {
        Debug.Log("Current Answer");
        var selectPanel = GameObject.FindWithTag("SelectPanel").GetComponent<CanvasGroup>();
        selectPanel.alpha = 0;

        SetState(new ThirdIntroductionState(this));
    }

    public void OnThirdIntroductionStateDialogEndDetected()
    {
        
    }
}