using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class State_Game : ApplicationState {

    const string SCENE_NAME = "Game";

    public override void OnStateEnter()
    {
      StartCoroutine( LoadScene() );
    }

  IEnumerator LoadScene()
  {
    // load the game screen scene
    AsyncOperation loadSceneOperation = SceneManager.LoadSceneAsync(SCENE_NAME, LoadSceneMode.Single);

    // idle until the scene is loaded
    while (loadSceneOperation.isDone != true )
    {
      yield return null;
    }

    // fade in to the game scene
    UIManager.Instance.SendCommand(UIManager.UICommand.Hide, UIScreenNames.Fade);
  }
}
