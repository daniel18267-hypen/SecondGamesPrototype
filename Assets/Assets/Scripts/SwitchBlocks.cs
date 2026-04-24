using Unity.VisualScripting;
using UnityEngine;

public class SwitchBlocks : MonoBehaviour
{
    [SerializeField] private bool m_initialstate;
    private Collider m_collider;
    private MeshRenderer m_meshrenderer;
    private Color m_color;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        m_collider = GetComponent<Collider>();
        m_collider.enabled = m_initialstate;
        m_meshrenderer = GetComponent<MeshRenderer>();
        m_collider.enabled = m_initialstate;
        m_color = m_meshrenderer.material.color;
        if (m_initialstate == true)
        {
            m_meshrenderer.material.color = new Vector4(m_color.r, m_color.g, m_color.b, 1f);
        }
        else if (m_initialstate == false)
        {
            m_meshrenderer.material.color = new Vector4(m_color.r, m_color.g, m_color.b, 0.5f);
        }
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
            m_collider.enabled = m_initialstate;
            Debug.Log("true");
        }
        else if (m_switchstate == false)
        {
            m_collider.enabled = !m_initialstate;
            Debug.Log("false");
        }
        if (m_collider.enabled == true)
        {
            m_meshrenderer.material.color = new Vector4(m_color.r, m_color.g, m_color.b, 1f);
        }
        else if (m_collider.enabled  == false)
        {
            m_meshrenderer.material.color = new Vector4(m_color.r, m_color.g, m_color.b, 0.5f);
        }
       
        
    }
}
