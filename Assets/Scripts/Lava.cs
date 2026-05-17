using UnityEngine;

public class Lava : MonoBehaviour
{
    private Vector3 m_startpos;
    private bool m_movelava;
    private void Awake()
    {
        // gets the start position on awake
        m_startpos = transform.position;
    }

    private void FixedUpdate()
    {
       if (m_movelava == true)
        {
            // have to do it this way as cannot modify it's individual Y value, increases y position gradually when bool is true
            transform.position += new Vector3(0f, 0.02f, 0f);
        }
    }
    /// <summary>
    /// Triggers on death, sets lava back to origin, makes it inactive, and makes it so it won't move
    /// </summary>
    public void DeathHandler()
    {
        m_movelava = false;
        transform.position = m_startpos;
        gameObject.SetActive(false);
    }
    /// <summary>
    /// triggered when leaving the lava npcs box, sets lava to active and gets it moving
    /// </summary>
    public void StartLavaHandler()
    {
        gameObject.SetActive(true);
        m_movelava = true;
    }
}
