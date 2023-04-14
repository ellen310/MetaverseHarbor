using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.SceneManagement;


public class NetworkManager : MonoBehaviourPunCallbacks
{
    public TMP_InputField nameInputField;
    public GameObject canvas;
    public ButtonManager btnManager;

    public List<string> userList;

    private void Start() {
        userList = new List<string>();
    }
    
    private void Update() {
        if(btnManager.isFilledName){
            PhotonNetwork.ConnectUsingSettings();
            btnManager.isFilledName = false;
        }
    }
    
    public override void OnConnectedToMaster(){
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions{MaxPlayers = 10}, null);    
    } 

    public override void OnJoinedRoom(){
        GameObject PI = PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
        photonView.RPC("SetName", RpcTarget.AllBuffered, PI.GetComponent<PhotonView>().ViewID);
        
        canvas.SetActive(false);
    }

    [PunRPC]
    void SetName(int viewID){
        userList.Add(nameInputField.text);
        var targetView = PhotonView.Find(viewID);
        switch(viewID){
            case 1001: 
                targetView.GetComponentInChildren<TextMesh>().text = userList[0];
                break;
            case 2001: 
                targetView.GetComponentInChildren<TextMesh>().text = userList[1];
                break;
            case 3001: 
                targetView.GetComponentInChildren<TextMesh>().text = userList[2];
                break;
            case 4001: 
                targetView.GetComponentInChildren<TextMesh>().text = userList[3];
                break;
            case 5001: 
                targetView.GetComponentInChildren<TextMesh>().text = userList[4];
                break;

        }
    }

}