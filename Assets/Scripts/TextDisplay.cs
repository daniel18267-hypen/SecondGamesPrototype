using System;
using System.Collections;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TextDisplay : MonoBehaviour
{
    public UnityEvent<string> EnteredTrigger;
    public UnityEvent LeftTrigger;
    [SerializeField] private TextMeshProUGUI m_tmpro;
    [SerializeField] private GameObject m_panel;
    // dialogue is set in editor.
    [SerializeField] private string m_text;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // passes text along to text parser script which then displays it in ui.
            EnteredTrigger?.Invoke(m_text);
        }
    }
  
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // causes text to stop displaying in text parser script
            LeftTrigger?.Invoke();
        }
    }
}
