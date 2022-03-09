using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;
public class Headless : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RefreshServerList()
    {
        NetworkManager.singleton.StartServer();
    }
}
