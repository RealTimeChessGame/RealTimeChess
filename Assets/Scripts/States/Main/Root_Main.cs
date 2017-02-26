using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root_Main : MonoBehaviour {

  private void Awake()
  {
    // Keep alive for duration of application lifetime
    DontDestroyOnLoad(this.gameObject);
  }
}
