using Unity.VisualScripting;
using UnityEngine;

public class SwitchBlocks : MonoBehaviour
{
    [SerializeField] private bool m_initialstate;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        gameObject.SetActive(m_initialstate);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SwitchHandler(bool m_switchstate)
    {
        if(m_switchstate == true)
        {
            gameObject.SetActive(m_initialstate);
        }
        else if (m_switchstate == false)
        {
            gameObject.SetActive(!m_initialstate);
        }
       
        
    }
}
