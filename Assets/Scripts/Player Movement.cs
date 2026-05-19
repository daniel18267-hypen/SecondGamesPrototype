using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.Events;


public class PlayerMovement : MonoBehaviour
{
    // intialises all the variables
    public UnityEvent<bool> SwitchChanged;
    public UnityEvent PlayerDeath;
    public UnityEvent ReachedEnding;
    public UnityEvent StartLava;
    private Vector2 m_playerdirection;
    private Vector3 m_spawnpoint;
    private float m_speed = 15;
    private int m_jumpcount = 1;
    private int m_maxjumpcount = 1;
    private float m_jumpheight = 10;
    private int m_dashcount = 0;
    private int m_maxdashcount = 0;
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
        // sets spawn point to where the player spawns.
        m_spawnpoint = transform.position; 
    }

    private void FixedUpdate()
    {
        // if rigidbody is null, rigidbodies x value is changed based on player directon and speed
         if (m_rigidbody != null)
         {
            m_tempvelocity = m_rigidbody.linearVelocity;
            // can't just get rigidbodyvelocity.x so have to set it to temp variable and get thats .x value
            m_tempvelocity.x = (m_playerdirection.x * m_speed);
            m_rigidbody.linearVelocity = m_tempvelocity;
           
         }
        if (Physics.Raycast(transform.position, Vector3.down,out m_hit,20, m_raylayer))
        {
            // detects if on the ground and gives you back jump  and dash if you are.
            if ( m_hit.distance < 1.2 )
            {
                if (m_jumpcount < m_maxjumpcount && m_hasjumpedrecently == false)
                {
                    m_jumpcount++;
                }
                if (m_dashcount < m_maxdashcount)
                {
                    m_dashcount++;
                }
            }
            // starts coyote time if not on ground, this takes away 1 from jump count after short amount of time, so can only jump if have double jump
            else if (m_hit.distance > 1.2 && m_jumpcount == m_maxjumpcount && m_coyoteactive == false)
            {
                StartCoroutine(CoyoteTime());
            }
        }
    }
    /// <summary>
    /// used for handling when cutscene is entered / ends, flips bool which determines whether player can move / use abilities
    /// </summary>
    public void HandleCutscene()
    {
        m_incutscene = !m_incutscene;
    }
    /// <summary>
    /// used for handling player movement
    /// </summary>
    /// <param name="ctx"></param>
    public void HandleMovement(InputAction.CallbackContext ctx)
    {
        // cant move unless in cutscene
        if (m_incutscene == false) 
        {
            // player direction is used in fixedupdate code for movement
            if (ctx.performed || ctx.started)
            {
                m_playerdirection = ctx.ReadValue<Vector2>();
            }
            // if cancelled set rigidbody's value to 0, but retain y velocity.
            else if (ctx.canceled)
            {
                if (m_rigidbody != null)
                {
                    m_rigidbody.linearVelocity = new Vector3(0, m_tempvelocity.y, 0);
                }
                // sets player direction to 0
                m_playerdirection = Vector3.zero;
            }
        }
    }
    public void HandleJump(InputAction.CallbackContext ctx)
    { 
        // can jump if jump count greater than 0, did this instead of bool as double jump exists
        if (ctx.started && m_jumpcount > 0 )
        {
            Debug.Log(m_jumpcount);
            m_tempvelocity.y = 0;
            m_rigidbody.linearVelocity = m_tempvelocity;
            m_rigidbody.AddForce(Vector3.up * m_jumpheight, ForceMode.Impulse);
            m_jumpcount = m_jumpcount - 1;
            // does this coroutine so you don't get an additional jump as soon as you leave the ground, due to leniency on hit distance.
            StartCoroutine(HasJumpedRecently());
        }
    }
    /// <summary>
    /// for handling dash ability, called when "A" or "D" are double tapped
    /// </summary>
    /// <param name="ctx"></param>
    public void HandleDash(InputAction.CallbackContext ctx)
    {

        if (ctx.performed && m_dashcount > 0 )
        {
            // gets what input you did and dashes left or right accordingly.
            if(ctx.control.name == "a")
            { 
                // calls coroutine so dash doesn't move you instantly, instead lots of short movements with very short pauses inbetween
                StartCoroutine(DashCoroutine(ctx.control.name));
            }
            if(ctx.control.name == "d")
            {
                StartCoroutine(DashCoroutine(ctx.control.name));
            }
            // subtracts from dash count
            m_dashcount = m_dashcount - 1;
        }
    }
    /// <summary>
    /// handles switching, called when shift key pressed
    /// </summary>
    /// <param name="ctx"></param>
    public void HandleSwitch(InputAction.CallbackContext ctx)
    {
        // calls event to switch blocks as long as you have that ability
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
        // so you can still have your double jump but not your initial "grounded" jump;
        m_jumpcount = m_maxjumpcount - 1;
        m_coyoteactive = false;
    }
    /// <summary>
    /// if double pressing "a" or "d" will more gradually add force in that direction to ensure it is smoother.
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    private IEnumerator DashCoroutine(string direction) // code for actually doing the dash
    {
        if (direction == "a")
        {
            for (int i = 0; i < 10; i++)
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
    /// <summary>
    /// for checking collisions with other things, mainly used for npcs, when colliding with them you get given their respective abilties.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case ("doublejump"):
                m_maxjumpcount = 2;
                break;
            case ("dash"):
                m_maxdashcount = 1;
                break;
            case ("switch"):
                m_canswitch = true;
                break;
            case ("lava"):
                // kills the player
                Death();
                break;
            case ("end"):
                // starts ending cutscene
                ReachedEnding?.Invoke();
                break;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        // starts lava rising when you exit the npc dialog and sets the checkpoint.
        if (other.gameObject.tag == "lavasummoner")
        {
            StartLava?.Invoke();
            SetCheckPoint(other.transform.position);
        }
    }
    /// <summary>
    /// sets checkpoint to be same as the passed in vector, however z value persists as to not cause collisions with backdrop
    /// </summary>
    /// <param name="m_checkpointlocation"></param>
    private void SetCheckPoint(Vector3 m_checkpointlocation)
    {
        m_spawnpoint = new Vector3(m_checkpointlocation.x, m_checkpointlocation.y, transform.position.z);
    }
    /// <summary>
    /// called when player dies, teleports player to last checkpoint, invokes player death event
    /// </summary>
    private void Death()
    {
        transform.position = m_spawnpoint;
        // player death events activates event in lava script that sets lava back to original position.
        PlayerDeath?.Invoke();

    }
}
