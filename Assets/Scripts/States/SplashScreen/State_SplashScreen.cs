using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class State_SplashScreen : ApplicationState
{
  const string SCENE_NAME = "SplashScreen";

  public override void OnStateEnter()
  {
    // load the splash screen scene
    SceneManager.LoadSceneAsync( SCENE_NAME, LoadSceneMode.Single );

    StartCoroutine(CountDown());
  }

  IEnumerator CountDown()
  {
    float seconds = 3.0f;

    while(seconds > 0)
    {
      seconds -= Time.deltaTime;
      yield return null;
    }

    LoadMainMenu();
  }

  private void LoadMainMenu()
  {
    //push the main menu state on
    ApplicationStateManager.Instance.PushState(ApplicationStates.MainMenu);

    // pop the splash screen off the stack
    ApplicationStateManager.Instance.PopState(ApplicationStates.SplashScreen);
  }
}
