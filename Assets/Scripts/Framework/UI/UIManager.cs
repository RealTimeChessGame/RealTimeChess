using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIScreenNames
{
  Splash,
  Loading,
  Fade,
  MainMenu,
  HUD,
  GameOver
}

public class UIManager : Singleton<UIManager>
{
  // a delegate provides a function signature, we use this to define any callbacks so we can know when commands are completed
  public delegate void CommandCallback(UIScreen screen, UICommand command);

  // a command to act upon a UIScreen
  public enum UICommand
  {
    Show,
    Hide,
    Enable,
    Disable
  }

  // this object exists until the command is completed
  private class CommandQueueItem
  {
    public UIScreenNames name;
    public UICommand command;
    public UIScreen screen;
    public CommandCallback callback;

    // used to flag if the command has been completed by the UIScreen so it can be removed from the command queue
    public bool completed = false;

    public CommandQueueItem( UIScreenNames name, UICommand command, UIScreen screen, CommandCallback callback )
    {
      this.name = name;
      this.command = command;
      this.screen = screen;
      this.callback = callback;
    }
  }

  // a map to all of the registered screens
  private Dictionary<UIScreenNames, UIScreen> m_Screens = new Dictionary<UIScreenNames, UIScreen>();
  // commands to be processed
  private List<CommandQueueItem> m_Commands = new List<CommandQueueItem>();
  // commands currently being processed
  private List<CommandQueueItem> m_CommandQueue = new List<CommandQueueItem>();

  public void AddScreen( UIScreenNames name, UIScreen screen )
  {
    // check if the name already exists as a key in the dictionary
    if( m_Screens.ContainsKey(name) == false )
    {
      // add the screen to our list, it is now ready to take commands
      m_Screens.Add( name, screen );
    }
  }

  public void SendCommand( UICommand command, UIScreenNames name, CommandCallback callback = null )
  {
    // use the name in the command to get a reference of the screen from our dictionary
    UIScreen screen = m_Screens[ name ];

    // create a new command object to be processed
    CommandQueueItem screenCommand = new CommandQueueItem( name, command, screen, callback );
    m_Commands.Add(screenCommand); 
  }

  public void OnUpdate()
  {
    ProcessNewCommands();

    ProcessCommandQueue();
  }

  private void ProcessNewCommands()
  {
    // iterate over the ui commands to be processed
    for (int i = 0; i < m_Commands.Count; ++i)
    {
      CommandQueueItem commandItem = m_Commands[i];

      // process the command
      switch (m_Commands[i].command)
      {
        case UICommand.Show:
          {
            // call the Show method on the UIScreen
            commandItem.screen.Show();

            // add it to the command queue to wait for it to complete the action
            m_CommandQueue.Add( commandItem );

            break;
          }
        case UICommand.Hide:
          {
            // call the Hide method on the UIScreen
            commandItem.screen.Hide();

            // add it to the command queue to wait for it to complete the action
            m_CommandQueue.Add(commandItem);

            break;
          }
        case UICommand.Enable:
          {
            // enable is an instant command, so it can be processed and disposed of this frame
            commandItem.screen.Enable( true );

            break;
          }
        case UICommand.Disable:
          {
            // disable is an instant command, so it can be processed and disposed of this frame
            commandItem.screen.Enable( false );

            break;
          }
      }
    }

    // all new commands processed, the list can be cleared
    m_Commands.Clear();
  }

  private void ProcessCommandQueue()
  {
    // iterate over the command queue
    // wait until the uiscreen says it is completed doing the command that was invoked on it
    for ( int i = 0; i < m_CommandQueue.Count; ++i )
    {
      CommandQueueItem commandItem = m_CommandQueue[i];

      // check if UIScreen has finished its command
      if( commandItem.screen.IsBusy() == false )
      {
        // screen has finished its command, notify if anything has registered for the callback
        if(commandItem.callback != null)
        {
          // if a callback function was given when the command was added, then we call that function to notify completion
          commandItem.callback.Invoke( commandItem.screen, commandItem.command );
        }

        // flag as completed so we can delete this command in the cleanup loop
        commandItem.completed = true;
      }
    }

    // remove any commands that are flagged as complete
    // the command queue is iterated in reverse so we can remove items without affecting the index as the list is traversed
    for ( int i = (m_CommandQueue.Count - 1); i >= 0 ; --i )
    {
      CommandQueueItem commandItem = m_CommandQueue[i];
      if(commandItem.completed == true )
      {
        m_CommandQueue.RemoveAt(i);
      }
    }
  }
}
