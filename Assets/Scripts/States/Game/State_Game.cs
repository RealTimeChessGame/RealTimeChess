using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class State_Game : ApplicationState {

    const string SCENE_NAME = "GameScreen";

    public override void OnStateEnter()
    {
        // load the game screen scene
        SceneManager.LoadSceneAsync(SCENE_NAME, LoadSceneMode.Single);
    }
}
