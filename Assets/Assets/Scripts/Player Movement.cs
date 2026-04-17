using TMPro;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;


public class PlayerMovement : MonoBehaviour
{
    private Vector2 m_playerdirection;
    private float m_speed = 15;
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
    private void FixedUpdate()
    {
         if (m_rigidbody != null)
            {
                m_rigidbody.linearVelocity = new Vector3(m_playerdirection.x * m_speed,m_rigidbody.linearVelocity.y,m_rigidbody.linearVelocity.z);
            }
    }
    public void HandleMovement(InputAction.CallbackContext ctx)
    {
        if (ctx.performed || ctx.started)
        {
            Debug.Log("PERFORMED");
            m_playerdirection = ctx.ReadValue<Vector2>();
            Debug.Log(m_playerdirection);
           
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
