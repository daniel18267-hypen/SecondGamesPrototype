using TMPro;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;


public class PlayerMovement : MonoBehaviour
{
    private Vector2 m_playerdirection;
    private float m_speed = 5;
    private Rigidbody m_rigidbody;
    private CapsuleCollider m_capsulecollider;
    private void Awake()
    {   
         m_rigidbody = GetComponent<Rigidbody>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void HandleMovement(InputAction.CallbackContext ctx)
    {
        if (ctx.performed || ctx.started)
        {
            Debug.Log("PERFORMED");
            m_playerdirection = ctx.ReadValue<Vector2>();
            if (m_rigidbody != null)
            {
                m_rigidbody.linearVelocity = m_playerdirection * m_speed;
            }
        }
        
        else if (ctx.canceled)
        {
            if (m_rigidbody != null)
            {
                m_rigidbody.linearVelocity = Vector3.zero;
            }
            m_playerdirection = Vector3.zero;
        }

    }
    
}
