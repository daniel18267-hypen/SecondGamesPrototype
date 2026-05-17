using UnityEngine;

public class SwitchManager : MonoBehaviour
{
    // switches set in UI
    [SerializeField] private SwitchBlocks[] switches;
    /// <summary>
    /// loops through array of switches and toggles them, easier to manage then having each one listen to the event
    /// </summary>
    /// <param name="m_switchstate"></param>
    public void SwitchHandler(bool m_switchstate) 
    {
        for (int i = 0; i < switches.Length; i++)
        {
            switches[i].ToggleSwitch(m_switchstate);
        }
    }
}
