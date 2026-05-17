using System;
using System.Collections;
using System.Runtime.CompilerServices;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TerrainTools;
using UnityEngine.UI;

public class TextParser : MonoBehaviour
{
    // initialise variables
    private bool m_textfinished = false;
    private CanvasRenderer m_canvasrenderer;
    [SerializeField] private TextMeshProUGUI m_tmpro;
    private bool m_textcooldown = true;
    private bool m_intrigger = false;
    private bool m_textboxcooldown = true;
    private int m_textboxcount = 1;
    private string m_text;
    int m_index = 0;
 // on awake get components, sets the ui to be fully transparent
    private void Awake()
    {
        m_canvasrenderer = GetComponent<CanvasRenderer>();
        m_canvasrenderer.SetAlpha(0f);
    }

    void Update()
    {
            // while in trigger adds the current char in the index to the text, creates text parser effect
            if (m_intrigger == true && m_textfinished == false && m_textcooldown == true && m_textboxcooldown == true )
            {
                m_tmpro.text = m_tmpro.text + ($"{m_text[m_index]}");
            // if there are more than 50 characters in current text box and there is a space next it'll reset, done so it doesn't cut of mid sentence
            if (m_index > (50 * m_textboxcount) && m_text[m_index] == ' ') 
            {
                    m_textboxcount++;
                    StartCoroutine(TextBoxCooldown());
            }
            // increases index so next char added is next char in array
                m_index++;
            // if end of text is reached, stop ui from displaying.
                if (m_index == m_text.Length)
                {
                    m_textfinished = true;
                    m_intrigger = false;
                }
                // starts coroutine so there is cooldown between characters being added to string.
                StartCoroutine(TextCooldown());
            }
    }
    // passes in text, sets index to 0, sets text variable to passed in text, called when entering trigger box
    public void TextHandler(string text)
    {
        m_textfinished = false;
        m_canvasrenderer.SetAlpha(1f);
        m_index = 0;
        m_textboxcount = 1;
        m_intrigger = true;
        m_text = text;
    }
    /// <summary>
    /// called when exiting trigger box, sets the text to blank and sets ui to be fully transparnet
    /// </summary>
    public void StopText()
    {
        m_intrigger = false;
        m_canvasrenderer.SetAlpha(0f);
        m_tmpro.text = "";
    }
    /// <summary>
    /// used to get a parsing effect, text displays a character at a time with very short breaks inbetween
    /// </summary>
    /// <returns></returns>
    private IEnumerator TextCooldown()
    {
        m_textcooldown = false;
        yield return new WaitForSeconds(0.03f);
        m_textcooldown = true;
    }
    /// <summary>
    /// used to leave current text box on screen for a bit so player can read it all
    /// </summary>
    /// <returns></returns>
    private IEnumerator TextBoxCooldown()
    {
        m_textboxcooldown = false;
        yield return new WaitForSeconds(0.5f);
        m_tmpro.text = "";
        m_textboxcooldown = true;
    }
}
