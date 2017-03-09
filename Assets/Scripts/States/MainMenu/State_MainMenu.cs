using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class State_MainMenu : ApplicationState
{
  const string SCENE_NAME = "MainMenu";

  public override void OnStateEnter()
  {
    // load the main menu scene
    SceneManager.LoadSceneAsync( SCENE_NAME, LoadSceneMode.Single );
    ApplicationStateManager.Instance.PushState(ApplicationStates.Options);
  }
}
