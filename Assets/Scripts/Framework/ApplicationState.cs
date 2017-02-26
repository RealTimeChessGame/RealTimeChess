using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationState : MonoBehaviour
{
  public ApplicationStates state;

  public virtual void OnStateEnter() { }
  public virtual void OnStateExit() { }

  public virtual void OnStateUpdate() { }
}
