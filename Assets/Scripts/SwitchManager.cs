using UnityEngine;

public class SwitchManager : MonoBehaviour
{
    [SerializeField] private SwitchBlocks[] switches;
    public void SwitchHandler(bool m_switchstate) // cleaner this way
    {
        for (int i = 0; i < switches.Length; i++)
        {
            switches[i].ToggleSwitch(m_switchstate);
        }
    }
}
