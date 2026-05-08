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
  
  
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            EnteredTrigger?.Invoke(m_text);
            
      
        }
    }
  
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            LeftTrigger?.Invoke();
            SetSpawnPoint?.Invoke(transform.position);
           
        }
    }
  
}
