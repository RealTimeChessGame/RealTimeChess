using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScreen_Fade : UIScreen
{
  public Image overlayImage;
  public Color fromColor;
  public Color toColor;
  public float time = 1.0f; // time (in seconds) to lerp from one color to another

  private Color m_CurrentColor = Color.clear;
  private bool m_IsBusy = false;

  protected override void CustomShow()
  {
    m_IsBusy = true;

    // fade to black
    fromColor = Color.clear;
    toColor = Color.black;

    // use a coroutine to process a loop over multiple frames
    StopCoroutine("FadeScreen");
    StartCoroutine(FadeScreen());
  }

  protected override void CustomHide()
  {
    m_IsBusy = true;

    // fade from black
    fromColor = Color.black;
    toColor = Color.clear;

    StopCoroutine("FadeScreen");
    StartCoroutine(FadeScreen());
  }

  protected override bool CustomIsBusy()
  {
    bool result = false;

    result = m_IsBusy;

    return result;
  }

  // a coroutine to fade the screen over multiple frames
  IEnumerator FadeScreen()
  {
    float timeAccumulator = 0;

    while(m_IsBusy == true)
    {
      // add the time that has passed since last frame to lerp between colors
      timeAccumulator += Time.deltaTime;

      // linearly interpolate between the two colors.  the time value is normalized 0 to 1.0
      m_CurrentColor = Color.Lerp( fromColor, toColor, timeAccumulator );
      overlayImage.color = m_CurrentColor;

      // check if lerp has completed
      if( timeAccumulator >= 1.0f )
      {
        // the fade has completed, we flag we are not busy so UIManager can complete the command
        m_IsBusy = false;
      }

      yield return null;
    }
  }
}
