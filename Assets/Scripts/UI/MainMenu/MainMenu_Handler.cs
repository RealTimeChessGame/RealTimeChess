using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu_Handler : MonoBehaviour
{

  public void ButtonClicked_Play()
  {
    // fade out to hide the transition to the game scene
    UIManager.Instance.SendCommand( UIManager.UICommand.Show, UIScreenNames.Fade, FadeCallback );
  }
	

  void FadeCallback(UIScreen screen, UIManager.UICommand command)
  {
    // fade out has completed
    ApplicationStateManager.Instance.PushState(ApplicationStates.Game);
    ApplicationStateManager.Instance.PopState(ApplicationStates.MainMenu);
  }
}
