using UnityEngine;

public class SwitchObject : MonoBehaviour
{
    GameObject objectToSwitch;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        objectToSwitch= transform.GetChild(0).gameObject;
        Events.SwitchReality += SwitchView;
    }

    private void OnDisable()
    {
        Events.SwitchReality -= SwitchView;
    }

    void SwitchView()
    {
        if (objectToSwitch.activeSelf)
        {
            objectToSwitch.SetActive(false);
        }
        else
        {
            objectToSwitch.SetActive(true);
        }
    }
}
