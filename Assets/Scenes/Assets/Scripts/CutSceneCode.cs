using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System.Collections;
public class CutsceneCode : MonoBehaviour
{
    public UnityEvent CutSceneEnd;
    private TextMeshProUGUI m_tmpro;
    [SerializeField] private string[] m_cutscenelines;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
       m_tmpro =  GetComponentInChildren<TextMeshProUGUI>();
    }
    void Start()
    {
        StartCoroutine(TextCooldown());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator TextCooldown()
    {
        for (int i = 0; i < m_cutscenelines.Length; i++)
        {
            m_tmpro.text = m_cutscenelines[i];
            yield return new WaitForSeconds(2.5f);
        }
        CutSceneEnd?.Invoke();
        gameObject.SetActive(false);
    }
}
