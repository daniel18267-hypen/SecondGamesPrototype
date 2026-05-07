using UnityEngine;

public class Lava : MonoBehaviour
{
    private Vector3 m_startpos;
    private bool m_movelava;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        m_startpos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
       if (m_movelava == true)
        {
            transform.position += new Vector3(0f, 0.1f, 0f);
        }
    }
    public void DeathHandler()
    {
        m_movelava = false;
        transform.position = m_startpos;
    }
    public void StartLavaHandler()
    {
        m_movelava = true;
    }
}
