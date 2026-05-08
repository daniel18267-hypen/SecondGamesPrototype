using UnityEngine;

public class Lava : MonoBehaviour
{
    private Vector3 m_startpos;
    private bool m_movelava;
    private void Awake()
    {
        m_startpos = transform.position;
    }

    private void FixedUpdate()
    {
       if (m_movelava == true)
        {
            transform.position += new Vector3(0f, 0.01f, 0f);
        }
    }
    public void DeathHandler()
    {
        m_movelava = false;
        transform.position = m_startpos;
        gameObject.SetActive(false);
    }
    public void StartLavaHandler()
    {
        gameObject.SetActive(true);
        m_movelava = true;
    }
}
