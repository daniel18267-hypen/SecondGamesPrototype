using System.Collections;
using TMPro;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.Events;


public class PlayerMovement : MonoBehaviour
{
    public UnityEvent<bool> SwitchChanged;
    public UnityEvent PlayerDeath;
    public UnityEvent StartLava;
    private Vector2 m_playerdirection;
    private Vector3 m_spawnpoint;
    private float m_speed = 15;
    private int m_jumpcount = 1;
    private int m_maxjumpcount = 1;
    private float m_jumpheight = 10;
    private int m_dashcount = 1;
    private int m_maxdashcount = 1;
    private bool m_incutscene = true;
    private bool m_canswitch;
    private float m_dashpower = 50;
    private Rigidbody m_rigidbody;
    private RaycastHit m_hit;
    private bool m_hasjumpedrecently;
    private bool m_switchstate;
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
        m_spawnpoint = transform.position; // sets spawn point to where the player spawns.
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
            // detects if on the ground and gives you back jump  and dashif you are.
            if ( m_hit.distance < 1.2 )
            {
                //Debug.Log("Jump added too");

                if (m_jumpcount < m_maxjumpcount && m_hasjumpedrecently == false)
                {
                   // Debug.Log("added to jump count ");
                    m_jumpcount++;

                }
                if (m_dashcount < m_maxdashcount)
                {
                    Debug.Log("got to max dash count");
                    m_dashcount++;
                }




            }
            else if (m_hit.distance > 1.2 && m_jumpcount == m_maxjumpcount && m_coyoteactive == false)
            {
                StartCoroutine(CoyoteTime());
                Debug.Log("triggered");

                Debug.Log("coyote");
            }
          
        }
      

    }
    public void HandleCutscene()
    {
        m_incutscene = !m_incutscene;
    }
    public void HandleMovement(InputAction.CallbackContext ctx)
    {
        if (m_incutscene == false) // cant move unless in cutscene
        {
            if (ctx.performed || ctx.started)
            {
                // Debug.Log("PERFORMED");
                m_playerdirection = ctx.ReadValue<Vector2>();
                //  Debug.Log(m_playerdirection);

            }

            else if (ctx.canceled)
            {
                if (m_rigidbody != null)
                {
                    m_rigidbody.linearVelocity = new Vector3(0, m_tempvelocity.y, 0);
                }
                m_playerdirection = Vector3.zero;
            }
        }
    }
    public void HandleJump(InputAction.CallbackContext ctx)
    { // can jump if jump count greater than 0, did this instead of bool as double jump exists
        if (ctx.started && m_jumpcount > 0 )
        {
            Debug.Log(m_jumpcount);
           // Debug.Log("Fired");
            m_tempvelocity.y = 0;
            m_rigidbody.linearVelocity = m_tempvelocity;
            m_rigidbody.AddForce(Vector3.up * m_jumpheight, ForceMode.Impulse);
            m_jumpcount = m_jumpcount - 1;
            // does this coroutine so you don't get an additional jump as soon as you leave the ground, due to leniency on hit distance.
            StartCoroutine(HasJumpedRecently());

        }
    }
    public void HandleDash(InputAction.CallbackContext ctx)
    {
        // for handling dash ability

        if (ctx.performed && m_dashcount > 0 )
        {
            // gets what input you did and dashes left or right accordingly.
            if(ctx.control.name == "a")
            {


                StartCoroutine(DashCoroutine(ctx.control.name));
            }
            if(ctx.control.name == "d")
            {
                StartCoroutine(DashCoroutine(ctx.control.name));

            }
            m_dashcount = m_dashcount - 1;
        }
    }
    public void HandleSwitch(InputAction.CallbackContext ctx)
    {
        // for handling switching between blocks
        if (ctx.started && m_canswitch == true)
        {
            m_switchstate = !m_switchstate;
            SwitchChanged?.Invoke(!m_switchstate);
        }
    }
    // coroutine so you can't get another jump right after you have just jumped (raycast distance check has room for error)
    private IEnumerator HasJumpedRecently()
    {
        //Debug.Log("got to enumerator");
        m_hasjumpedrecently = true;
        yield return new WaitForSeconds(0.2f);
        
        m_hasjumpedrecently = false;

    }
    private IEnumerator CoyoteTime()
    {
        m_coyoteactive = true;
        yield return new WaitForSeconds(0.1f);
        m_jumpcount = m_maxjumpcount - 1 ; // so you can still have your double jump but not your initial "grounded" jump;
        m_coyoteactive = false;
        
    }
    private IEnumerator DashCoroutine(string direction) // code for actually doing the dash
    {
        if (direction == "a")
        {
            for ( int i = 0; i < 10; i++)
            {
                m_rigidbody.AddForce(Vector3.left * m_dashpower, ForceMode.Impulse);
                yield return new WaitForSeconds(0.025f);
            }
            
        }
        if (direction == "d")
        {
            for (int i = 0; i < 10; i++)
            {
                m_rigidbody.AddForce(Vector3.right * m_dashpower, ForceMode.Impulse);
                yield return new WaitForSeconds(0.025f);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "doublejump")
        {
            m_maxjumpcount = 2;
        }
        else if(other.gameObject.tag == "dash")
        {
            m_maxdashcount = 1;
        }
        else if (other.gameObject.tag == "switch")
        {
            m_canswitch = true;
            SetCheckPoint(other.transform.position);
            StartLava?.Invoke();

        }
        else if (other.gameObject.tag == "lava")
        {
            Death();
        }
    }
    private void SetCheckPoint(Vector3 m_checkpointlocation)
    {
       m_spawnpoint = m_checkpointlocation;
    }
    private void Death()
    {
        transform.position = m_spawnpoint;
        PlayerDeath?.Invoke();

    }
    



}
