using System;
using System.Collections;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextDisplay : MonoBehaviour
{
    private TextMeshProUGUI m_tmpro;
    private Button m_button;
    private string m_text;
    private bool m_textfinished = false;
    private bool m_textcooldown = true;
    private bool m_textboxcooldown = true;
    private int textboxcount = 1;
    int index = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        m_text = "Hello, this is a test, awesome awesome awesome awesome test test test test";
        m_tmpro = GetComponentInChildren<TextMeshProUGUI>();
       m_button = GetComponentInChildren<Button>();
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && m_textfinished == false && m_textcooldown == true && m_textboxcooldown == true)
        {
               
            m_tmpro.text = m_tmpro.text + ($"{m_text[index]}");
            if (index > (30 * textboxcount) && m_text[index] == ' ' ) // if there are more than 30 characters in current text box and there is a space next it'll reset, done so it doesn't cut of mid sentence
            {
               
                Debug.Log("got here 3");
                textboxcount++;
                StartCoroutine(TextBoxCooldown());

            }
            index++;
            if (index == m_text.Length)
                {
                Debug.Log("text length");
                    m_textfinished = true;
                }
                StartCoroutine(TextCooldown());
            
            
        }

    }
    private IEnumerator TextCooldown()
    {
        m_textcooldown = false;
        yield return new WaitForSeconds(0.05f);
        m_textcooldown = true;
    }
    private IEnumerator TextBoxCooldown()
    {
        
        m_textboxcooldown = false;
        yield return new WaitForSeconds(1f);
        m_tmpro.text = "";
        m_textboxcooldown = true;
      
      
        
    }
}
