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

    public override void EnterState()
    {
        DialogueSystem.instance.Send("Test_01" , levelController.OnSecIntroductionStateDetected);
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
    
    public override void EnterState()
    {
        DialogueSystem.instance.Send("Test_02" , levelController.OnFirstIntroductionStateDetected);
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
        SetState(new FirstIntroductionState(this));
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

    public void OnFirstIntroductionStateDetected()
    {
        Debug.Log("End");
        SetState(new SecIntroductionState(this));
    }
    
    public void OnSecIntroductionStateDetected()
    {
        Debug.Log("End");
    }
}