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

    FadeOut();
  }

  private void LoadMainMenu()
  {
    //push the main menu state on
    ApplicationStateManager.Instance.PushState( ApplicationStates.MainMenu );

    // pop the splash screen off the stack
    ApplicationStateManager.Instance.PopState( ApplicationStates.SplashScreen );
  }

  private void FadeOut()
  {
    // show the fade screen and wait until it calls the FadeCallback function when it completes
    UIManager.Instance.SendCommand( UIManager.UICommand.Show, UIScreenNames.Fade, FadeCallback );
  }

  private void FadeCallback( UIScreen screen, UIManager.UICommand command)
  {
    // screen has finished fading out, load the main menu
    LoadMainMenu();
  }
}
