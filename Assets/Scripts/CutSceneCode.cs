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
    private void Awake()
    {
        // gets text component
       m_tmpro =  GetComponentInChildren<TextMeshProUGUI>();
    }
    void Start()
    {
        // on starts starts beginning cutscene.
        StartCoroutine(TextCooldown(false));
    }
    /// <summary>
    ///  when reaches end of level, event is called which this function listens too.
    /// </summary>
    public void EndHandler()
    {
      
        gameObject.SetActive(true);
        // pases in true so ending cutscene is triggered
        StartCoroutine(TextCooldown(true));
    }
    /// <summary>
    /// if passes in false, starts beginning cutscene, if passes in true does ending cutscene
    /// </summary>
    /// <param name="m_isend"></param>
    /// <returns></returns>
    private IEnumerator TextCooldown(bool m_isend)
    {
        if (m_isend == false)
        {
            for (int i = 0; i < m_cutscenelinesbeginning.Length; i++)
            {
                // displays beginning cutscene lines, waits 2.5f before displaying next line
                m_tmpro.text = m_cutscenelinesbeginning[i];
                yield return new WaitForSeconds(2.5f);
            }
            //event causes movement to be unfreezed  in player controller, done so cant move around while in cutscene
            CutSceneEnd?.Invoke();
            // sets cutscene ui to be inactive.
            gameObject.SetActive(false);
        }
        else if (m_isend == true)
        {
            for (int i = 0; i < m_cutscenelinesend.Length; i++)
            {
                // displays ending cutscene lines, waits 2.5f before displaying next line.
                m_tmpro.text = m_cutscenelinesend[i];
                yield return new WaitForSeconds(2.5f);
            }
        }
        
    }
}
