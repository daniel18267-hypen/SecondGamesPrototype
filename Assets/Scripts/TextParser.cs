using System;
using System.Collections;
using System.Runtime.CompilerServices;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TextParser : MonoBehaviour
{
    private bool m_textfinished = false;
    private CanvasRenderer m_canvasrenderer;
    [SerializeField] private TextMeshProUGUI m_tmpro;
    private bool m_textcooldown = true;
    private bool m_intrigger = false;
    private bool m_textboxcooldown = true;
    private int m_textboxcount = 1;
    private string m_text;
    int m_index = 0;
 
    private void Awake()
    {
        m_canvasrenderer = GetComponent<CanvasRenderer>();
        m_canvasrenderer.SetAlpha(0f);
    }

    void Update()
    {
        
            if (m_textfinished == false && m_textcooldown == true && m_textboxcooldown == true && m_intrigger == true )
            {
                m_tmpro.text = m_tmpro.text + ($"{m_text[m_index]}");
                if (m_index > (50 * m_textboxcount) && m_text[m_index] == ' ') // if there are more than 30 characters in current text box and there is a space next it'll reset, done so it doesn't cut of mid sentence
                {

                    Debug.Log("got here 3");
                    m_textboxcount++;
                    StartCoroutine(TextBoxCooldown());

                }
                m_index++;
                if (m_index == m_text.Length)
                {
                    Debug.Log("text length");
                    m_textfinished = true;
                    m_intrigger = false;
                }
                StartCoroutine(TextCooldown());
            }
        
    }
    public void TextHandler(string text)
    {
        m_textfinished = false;
        m_canvasrenderer.SetAlpha(1f);
        m_index = 0;
        m_textboxcount = 1;
        m_intrigger = true;
        m_text = text;
      

    }
    public void StopText()
    {
      
        m_intrigger = false;
        m_canvasrenderer.SetAlpha(0f);
        m_tmpro.text = "";
    }
    private IEnumerator TextCooldown()
    {
        m_textcooldown = false;
        yield return new WaitForSeconds(0.03f);
        m_textcooldown = true;
    }
    private IEnumerator TextBoxCooldown()
    {

        m_textboxcooldown = false;
        yield return new WaitForSeconds(0.5f);
        m_tmpro.text = "";
        m_textboxcooldown = true;



    }
}
