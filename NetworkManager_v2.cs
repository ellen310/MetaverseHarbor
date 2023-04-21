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
    public DataManager dataManager;
    public PhotonView PV;

    private void Start() {
        PhotonNetwork.ConnectUsingSettings();
        dataManager = GameObject.Find("DataManager").GetComponent<DataManager>();
        PV = this.photonView;
    }

    public override void OnConnectedToMaster(){
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions{MaxPlayers = 10}, null);    
    } 

    public override void OnJoinedRoom(){
        GameObject PI = PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
        
        PhotonNetwork.LocalPlayer.NickName = dataManager.username;
        PI.GetComponentInChildren<TextMesh>().text = PhotonNetwork.LocalPlayer.NickName;
        
        PV.RPC("setNameRPC", RpcTarget.AllBuffered, PhotonNetwork.NickName, PI.GetComponent<PhotonView>().ViewID);

    }

    [PunRPC]
    public void setNameRPC(string name, int viewID){ 
        var targetView = PhotonView.Find(viewID).gameObject;
        targetView.GetComponentInChildren<TextMesh>().text = name;
    }

}