using Unity.VisualScripting;
using UnityEngine;

public class SwitchBlocks : MonoBehaviour
{
    // if blue initial state is false, if red it's true, determines if object is collidable or not on start, set in ui
    [SerializeField] private bool m_initialstate;
    private Collider m_collider;
    private MeshRenderer m_meshrenderer;
    private Color m_color;
    /// <summary>
    /// Initialises switches
    /// </summary>
    private void Awake()
    {
        // gets all components
        m_collider = GetComponent<Collider>();
        m_collider.enabled = m_initialstate;
        m_meshrenderer = GetComponent<MeshRenderer>();
        m_collider.enabled = m_initialstate;
        // sets color to be same as material color
        m_color = m_meshrenderer.material.color;
        // if initial state is true, material color is the same as it already is
        if (m_initialstate == true)
        {
            m_meshrenderer.material.color = new Vector4(m_color.r, m_color.g, m_color.b, 1f);
        }
        // if false, it's the same colour but it's alpha value is changed so that it is semi transparent
        else if (m_initialstate == false)
        {
            m_meshrenderer.material.color = new Vector4(m_color.r, m_color.g, m_color.b, 0.5f);
        }
    }
   /// <summary>
   /// called in switchmanager gameobject, used to trigger switches on / off
   /// </summary>
   /// <param name="m_switchstate"></param>
    public void ToggleSwitch(bool m_switchstate)
    {
        // if switch state true, then collider is equal to initial state
        if(m_switchstate == true)
        {
            m_collider.enabled = m_initialstate;
            Debug.Log("true");
        }
        // if false, collider is opposite of initial state
        else if (m_switchstate == false)
        {
            m_collider.enabled = !m_initialstate;
            Debug.Log("false");
        }
        // if collider is enabled material is fully opaque
        if (m_collider.enabled == true)
        {
            m_meshrenderer.material.color = new Vector4(m_color.r, m_color.g, m_color.b, 1f);
        }
        // if not enabled, semi - transparent
        else if (m_collider.enabled  == false)
        {
            m_meshrenderer.material.color = new Vector4(m_color.r, m_color.g, m_color.b, 0.5f);
        }
    }
}
