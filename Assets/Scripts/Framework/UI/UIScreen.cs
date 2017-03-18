using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScreen : MonoBehaviour
{
  public UIScreenNames screenName;

  // methods that can be overriden by any UIScreen
  protected virtual void Initialize() { }
  protected virtual void CustomShow() { }
  protected virtual void CustomHide() { }
  protected virtual void CustomEnable( bool enabled ) { }
  protected virtual bool CustomIsBusy() { return false; }

  private void Start()
  {
    // register this screen with the UIManager
    UIManager.Instance.AddScreen(screenName, this);

    // call to the derived class for any initialization
    Initialize();
  }

  private void OnDestroy()
  {
    UIManager.Instance.RemoveScreen(screenName);
  }

  public void Show()
  {
    // call to the derived class to act on the show command
    CustomShow();
  }

  public void Hide()
  {
    // call to the derived class to act on the hide command
    CustomHide();
  }

  public void Enable( bool enabled )
  {
    // call to the derived class to decide how enable/disable is implemented
    CustomEnable( enabled );
  }

  public bool IsBusy()
  {
    bool result = false;

    // return false unless the screen has a different result
    // if we decide to implement a mechanism common to all UIScreens (ie. using animations), we can implement it here
    result = false || CustomIsBusy();

    return result;
  }

}
