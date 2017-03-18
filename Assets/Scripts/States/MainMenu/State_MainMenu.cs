using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class State_MainMenu : ApplicationState
{
  const string SCENE_NAME = "MainMenu";

  public override void OnStateEnter()
  {
    // load the main menu scene and wait until it is complete
    StartCoroutine( LoadScene() );
  }

  IEnumerator LoadScene()
  {
    // load the main menu scene
    AsyncOperation loadSceneOperation = SceneManager.LoadSceneAsync(SCENE_NAME, LoadSceneMode.Single);

    // idle in this loop until the scene is loaded
    while( loadSceneOperation.isDone != true )
    {
      yield return null;
    }

    //scene has completed loading, we fade in the scene because it was faded out from splashscreen
    UIManager.Instance.SendCommand( UIManager.UICommand.Hide, UIScreenNames.Fade );
  }
}
