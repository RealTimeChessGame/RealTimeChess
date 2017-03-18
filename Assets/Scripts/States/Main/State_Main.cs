using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Main : ApplicationState
{

  private UIManager m_UIManager = null;

  public override void OnStateEnter()
  {
    m_UIManager = UIManager.Instance;

    // load up the splash screen
    ApplicationStateManager.Instance.PushState(ApplicationStates.SplashScreen);
  }

  public override void OnStateUpdate()
  {
    // the main state will tick the UIManager
    m_UIManager.OnUpdate();
  }

}
