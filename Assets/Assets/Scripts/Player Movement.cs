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
    private int m_maxjumpcount = 1;
    private float m_initjumpheight = 10;
    private float m_finaljumpheight;
    private Rigidbody m_rigidbody;
    private RaycastHit m_hit;
    private bool m_hasjumpedrecently;
    private bool m_coyoteactive;
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
            m_tempvelocity.x = (m_playerdirection.x * m_speed); // can't just get rigidbodyvelocity.x so have to set it to temp variable and get thats .x
            m_rigidbody.linearVelocity = m_tempvelocity;
           
        }
        if (Physics.Raycast(transform.position, Vector3.down,out m_hit,20, m_raylayer))
        {
            //Debug.Log(m_hit.distance);
            // detects if on the ground and gives you back jump if you are.
            if (m_jumpcount < m_maxjumpcount && m_hit.distance < 1.2 && m_hasjumpedrecently == false)
            {
                Debug.Log("Jump added too");

                if (m_jumpcount < m_maxjumpcount)
                {
                    Debug.Log("added to jump count ");
                    m_jumpcount++;

                }




            }
            else if (m_hit.distance > 1.2 && m_jumpcount == m_maxjumpcount && m_coyoteactive == false)
            {
                StartCoroutine(CoyoteTime());
                Debug.Log("triggered");

                Debug.Log("coyote");
            }
          
        }
        // no jump if you fall off something, need to fix this tbh
        //else if (!Physics.Raycast(transform.position, Vector3.down, out m_hit, 20, m_raylayer))
       // {
            // so you can jump if you have double jump but not if you don't
         //   m_jumpcount = m_maxjumpcount - 1;
       // }

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
            if (m_tempvelocity.y < -0.1)
            {
                m_finaljumpheight = m_initjumpheight * 2f;
            }
            else
            {
                m_finaljumpheight = m_initjumpheight;
            }
            m_rigidbody.AddForce(Vector3.up * m_finaljumpheight, ForceMode.Impulse);
            m_jumpcount = m_jumpcount - 1;
            // does this coroutine so you don't get an additional jump as soon as you leave the ground, due to leniency on hit distance.
            StartCoroutine(HasJumpedRecently());

        }
    }
    // coroutine so you can't get another jump right after you have just jumped (raycast distance check has room for error)
    private IEnumerator HasJumpedRecently()
    {
        Debug.Log("got to enumerator");
        m_hasjumpedrecently = true;
        yield return new WaitForSeconds(0.2f);
        
        m_hasjumpedrecently = false;

    }
    private IEnumerator CoyoteTime()
    {
        m_coyoteactive = true;
        yield return new WaitForSeconds(1f);
        m_jumpcount--; // so you can still have your double jump but not your initial "grounded" jump;
        m_coyoteactive = false;
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "doublejump")
        {
            m_maxjumpcount = 2;
            Destroy(other.gameObject);
        }
    }
    



}
