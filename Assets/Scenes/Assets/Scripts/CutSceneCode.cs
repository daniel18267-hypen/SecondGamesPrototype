using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System.Collections;
public class CutsceneCode : MonoBehaviour
{
    public UnityEvent<bool> CutSceneEnd;
    private TextMeshProUGUI m_text;
    private string[] m_cutscenelines;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
       m_text =  GetComponentInChildren<TextMeshProUGUI>();
        m_cutscenelines[0] = "1";
        m_cutscenelines[1] = "2";
        m_cutscenelines[2] = "3";
        m_cutscenelines[3] = "4";
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator TextCooldown()
    {
        for (int i = 0; i < m_cutscenelines.Length; i++)
        {
            m_text = m_cutscenelines[i];
        }
    }
}
