using UnityEngine;
using System;

public class Events : MonoBehaviour
{
    public Events instance;
    public static event Action SwitchReality;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance!= this)
        {
            Destroy(this);
        }
    }
}
