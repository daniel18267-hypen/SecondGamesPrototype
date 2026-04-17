using System.Collections;
using TMPro;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;


public class PlayerMovement : MonoBehaviour
{
    private Vector2 m_playerdirection;
    private float m_speed = 15;
    private int m_jumpcount = 1;
    private int m_maxjumpcount = 2;
    private float m_jumpheight = 10;
    private Rigidbody m_rigidbody;
    private RaycastHit m_hit;
    private bool m_coroutinerunning;
    private Vector3 m_tempvelocity;
    [SerializeField] private LayerMask m_raylayer;
    private bool m_jumpedrecently;
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
            m_tempvelocity = m_rigidbody.linearVelocity;
            // m_rigidbody.linearVelocity = new Vector3(m_playerdirection.x * m_speed,m_rigidbody.linearVelocity.y,m_rigidbody.linearVelocity.z); // handles x axis movement, y and z axis movement unchanged
            m_tempvelocity.x = (m_playerdirection.x * m_speed);
            m_rigidbody.linearVelocity = m_tempvelocity;
            }
        if (Physics.Raycast(transform.position, Vector3.down,out m_hit,20, m_raylayer))
        {
            //Debug.Log(m_hit.distance);
            if (m_jumpcount < m_maxjumpcount && m_hit.distance < 1.2 && m_coroutinerunning == false)
            {
                Debug.Log("Jump added too");
                while (m_jumpcount < m_maxjumpcount)
                {
                    m_jumpcount++;
                }
                StartCoroutine(HasJumpedRecently());

                
            }
        }
        else if (!Physics.Raycast(transform.position, Vector3.down, out m_hit, 20, m_raylayer))
        {
            m_jumpcount = 0;
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
                m_rigidbody.linearVelocity = new Vector3(0,m_tempvelocity.y,0);
            }
            m_playerdirection = Vector3.zero;
        }

    }
    public void HandleJump(InputAction.CallbackContext ctx)
    { // can jump if jump count greater than 0, did this instead of bool as double jump exists
        if (ctx.started && m_jumpcount > 0)
        {
            Debug.Log(m_jumpcount);
            Debug.Log("Fired");
            m_rigidbody.AddForce(Vector3.up * m_jumpheight, ForceMode.Impulse);
            m_jumpcount = m_jumpcount - 1;
           
        }
    }
    // coroutine so you can't get another jump right after you have just jumped (raycast distance check has room for error)
    private IEnumerator HasJumpedRecently()
    {
        Debug.Log("got to enumerator");
        m_coroutinerunning = true;
        Debug.Log(m_coroutinerunning);
        yield return new WaitForSeconds(1f);
        m_coroutinerunning = false;
        Debug.Log(m_coroutinerunning);

    }


}
