using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T: MonoSingleton<T>
{
  private static T m_Instance;
  public static T Instance
  {
    get
    {
      return m_Instance;
    }
  }

  private void Awake()
  {
    if(m_Instance == null)
    {
      m_Instance = this as T;
    }
  }


}
