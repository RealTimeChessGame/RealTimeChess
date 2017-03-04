using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class State_Options : ApplicationState {

    const string SCENE_NAME = "OptionsScreen";

    public override void OnStateEnter()
    {
        // load the splash screen scene
        SceneManager.LoadSceneAsync(SCENE_NAME, LoadSceneMode.Single);
    }
}
