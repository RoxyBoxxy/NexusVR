using LightReflectiveMirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using System;

public class ServerList : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Button;
    public Transform Menu;
    private LightReflectiveMirrorTransport _LRM;
    public Transform hud;

    void Start()
    {
        if(_LRM == null)
            _LRM = (LightReflectiveMirrorTransport)Transport.activeTransport;
            _LRM.serverListUpdated.AddListener(ServerListUpdate);
    }
    
    void OnDisable()
    {
        if(_LRM == null)
            _LRM.serverListUpdated.RemoveListener(ServerListUpdate);
    }

    public void RefreshServerList()
    {
        _LRM.RequestServerList();
    }
    void ServerListUpdate()
    {
        foreach(Transform t in Menu)
            Destroy(t.gameObject);
        for(int i = 0; i < _LRM.relayServerList.Count; i++)
        {
            var newEntry = Instantiate(Button, Menu);
            newEntry.transform.GetChild(0).GetComponent<Text>().text = _LRM.relayServerList[i].serverName;
            string serverID = _LRM.relayServerList[i].serverId;
            newEntry.GetComponent<Button>().onClick.AddListener(() => ConnectToServer(serverID));
                                            
            Debug.Log(serverID);
        }
    }
    public void ConnectToServer(string serverID)
    {
        NetworkManager.singleton.networkAddress = serverID.ToString();
        NetworkManager.singleton.StartClient();
    }
    public void ConnectionStatus(string connectionStatus)
    {
        var newEntry = Instantiate(hud);
        newEntry.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = connectionStatus;
    }
}