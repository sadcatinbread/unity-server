using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Riptide.Utils;
using Riptide;

public enum ClientToServerId : ushort {
    name = 1
}

public class NetworkManager : MonoBehaviour
{
    private static NetworkManager _singleton;
    public static NetworkManager Singleton{
        get => _singleton;
        private set{
            if(_singleton == null){
                _singleton = value;
            }
            else if(_singleton != value){
                Debug.Log($"{nameof(NetworkManager)} instance already exists, destroying duplicate!");
                Destroy(value);
            }
        }
    }
    public Server Server{get; private set;}
    [SerializeField] private ushort port;
    [SerializeField] private ushort maxClients;
    private void Awake(){
        Singleton = this;
    }
    private void Start(){
        RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, false);

        Server = new Server();
        Server.Start(port, maxClients);
    }
    private void FixedUpdate(){
        Server.Update();
    }
    private void OnApplicationQuit(){
        Server.Stop();
    }
}
