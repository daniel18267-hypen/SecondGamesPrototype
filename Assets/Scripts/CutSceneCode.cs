using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System.Collections;
using UnityEditor.Experimental.GraphView;
public class CutsceneCode : MonoBehaviour
{
    public UnityEvent CutSceneEnd;
    private TextMeshProUGUI m_tmpro;
    // cutscene lines for beginning and end cutscene, set in editor.
    [SerializeField] private string[] m_cutscenelinesbeginning;
    [SerializeField] private string[] m_cutscenelinesend;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
       m_tmpro =  GetComponentInChildren<TextMeshProUGUI>();
    }
    void Start()
    {
        StartCoroutine(TextCooldown(false));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void EndHandler()
    {
        StartCoroutine(TextCooldown(true));
    }

    private IEnumerator TextCooldown(bool m_isend)
    {
        if (m_isend == false)
        {
            for (int i = 0; i < m_cutscenelinesbeginning.Length; i++)
            {
                m_tmpro.text = m_cutscenelinesbeginning[i];
                yield return new WaitForSeconds(2.5f);
            }

            CutSceneEnd?.Invoke();
            gameObject.SetActive(false);
        }
        else if (m_isend == true)
        {
            for (int i = 0; i < m_cutscenelinesend.Length; i++)
            {
                m_tmpro.text = m_cutscenelinesend[i];
                yield return new WaitForSeconds(2.5f);
            }
        }
        
    }
}
