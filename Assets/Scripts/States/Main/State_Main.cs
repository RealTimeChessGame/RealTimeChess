using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Main : ApplicationState
{

  public override void OnStateEnter()
  {
    // load up the splash screen
    ApplicationStateManager.Instance.PushState(ApplicationStates.SplashScreen);
  }

}
