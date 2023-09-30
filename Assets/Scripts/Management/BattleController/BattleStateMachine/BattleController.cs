using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleController : MonoBehaviour
{

    #region State Variables

        public BattleStateMachine             StateMachine            { get; private set; }

        public BattleStartState               BattleStartState              { get; private set; }
        public BattleOverState                BattleOverState         { get; private set; }
        public BattleWaitState                BattleWaitState         { get; private set; }
        
    #endregion

    #region Stuff
        public GameController GameController    { get; private set; }

    #endregion

    private void Awake() 
    {

        StateMachine            = new BattleStateMachine();

        BattleStartState        = new BattleStartState(this, StateMachine, "Battle Start");
        BattleOverState         = new BattleOverState(this, StateMachine, "Battle Over");
        BattleWaitState         = new BattleWaitState(this, StateMachine, "Battle Wait");
    }

    // Start is called before the first frame update
    void Start()
    {
        StateMachine.Initialize(BattleWaitState);
    }

    private void Update()
    {
        StateMachine.CurrentState.LogicUpdate();   
    }

    private void FixedUpdate() {
        StateMachine.CurrentState.PhysicsUpdate();    
    }

}
