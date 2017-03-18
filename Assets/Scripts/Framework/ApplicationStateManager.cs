using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ApplicationStates
{
  Main,
  SplashScreen,
  MainMenu,
  Options,
  Game,
  GameOver
}

public class ApplicationStateManager : MonoSingleton<ApplicationStateManager>
{
  public List<ApplicationState> states = new List<ApplicationState>();
  public ApplicationStates startState = ApplicationStates.Main;

  private List<ApplicationState> m_StateStack = new List<ApplicationState>();
  private Dictionary<ApplicationStates, ApplicationState> m_StateTable = new Dictionary<ApplicationStates, ApplicationState>();

  private void Start()
  {
    // create an easy lookup dictionary
    InitializeLookupTable();

    // push the default state onto the stack
    PushState(startState);
  }

  private void Update()
  {
    // call the state updates on active states in the stack
    for( int i = 0; i < m_StateStack.Count; ++i )
    {
      m_StateStack[i].OnStateUpdate();
    }
  }

  private void InitializeLookupTable()
  {
    // create an easy lookup table in a dictionary to link the state enum to the state object
    for( int i =0; i < states.Count; ++i )
    {
      m_StateTable.Add(states[i].state, states[i]);
    }
  }

  public void PushState( ApplicationStates state )
  {
    if( m_StateTable.ContainsKey(state) )
    {
      // lookup the state
      ApplicationState activeState = m_StateTable[state];

      // add the state to the stack
      m_StateStack.Add(activeState);

      // call the enter method now that the state is active
      activeState.OnStateEnter();
    }
  }

  public void PopState( ApplicationStates state )
  {
    // if state exists
    if( m_StateTable.ContainsKey(state) )
    {
      // lookup the state
      ApplicationState activeState = m_StateTable[state];

      // find the state on the stack and remove it
      for (int i = 0; i < m_StateStack.Count; ++i)
      {
        if (m_StateStack[i] == activeState)
        {
          // notify the state it is no longer active
          activeState.OnStateExit();

          // remove the state from the stack
          m_StateStack.RemoveAt(i);
        }
      }
    }
  }

  public ApplicationState GetState( ApplicationStates state )
  {
    ApplicationState result = null;

    // lookup and return the state
    if( m_StateTable.ContainsKey(state) )
    {
      result = m_StateTable[state];
    }

    return result;
  }
}
