using System;
using System.Collections;
using System.Runtime.CompilerServices;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TextDisplay : MonoBehaviour
{
    public UnityEvent<string> EnteredTrigger;
    public UnityEvent<Vector3> SetSpawnPoint;
    public UnityEvent LeftTrigger;
   [SerializeField] private TextMeshProUGUI m_tmpro;
    [SerializeField] private GameObject m_panel;
    [SerializeField] private string m_text;
    private bool m_textfinished = false;
    private bool m_textcooldown = true;
    private bool m_textboxcooldown = true;
    private int m_textboxcount = 1;
    int m_index = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
       
        //m_panel.SetActive(false);
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            EnteredTrigger?.Invoke(m_text);
            
         /*   m_textfinished = false;
            m_panel.SetActive(true);
            m_index = 0;
            m_tmpro.text = "";
            m_textboxcount = 1; */
        }
    }
   /* private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && m_textfinished == false && m_textcooldown == true && m_textboxcooldown == true)
        {
            m_tmpro.text = m_tmpro.text + ($"{m_text[m_index]}");
            if (m_index > (50 * m_textboxcount) && m_text[m_index] == ' ' ) // if there are more than 30 characters in current text box and there is a space next it'll reset, done so it doesn't cut of mid sentence
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
                }
                StartCoroutine(TextCooldown());   
        }

    }*/
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            LeftTrigger?.Invoke();
            SetSpawnPoint?.Invoke(transform.position);
           // m_panel.SetActive(false);
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
