using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.SceneManagement;


public class NetworkManager_v3 : MonoBehaviourPunCallbacks
{
    public DataManager dataManager;
    public CameraController cameraController;
    public PhotonView PV;

    private void Start() {
        PhotonNetwork.ConnectUsingSettings();
        dataManager = GameObject.Find("DataManager").GetComponent<DataManager>();
        cameraController = GameObject.FindWithTag("MainCamera").GetComponent<CameraController>();
        PV = this.photonView;
    }

    public override void OnConnectedToMaster(){
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions{MaxPlayers = 10}, null);    
    } 

    public override void OnJoinedRoom(){
        GameObject PI = PhotonNetwork.Instantiate("OVRPlayerController", Vector3.zero, Quaternion.identity);
        
        PhotonNetwork.LocalPlayer.NickName = dataManager.username;
        PI.GetComponentInChildren<TextMesh>().text = PhotonNetwork.LocalPlayer.NickName;

        PV.RPC("setNameRPC", RpcTarget.AllBuffered, PhotonNetwork.NickName, PI.GetComponent<PhotonView>().ViewID); //PhotonNetwork.NickName 를  PhotonNetwork.LocalPlayer.NickName 로 바꿔야되나?

    }

    [PunRPC]
    public void setNameRPC(string name, int viewID){ 
        var targetView = PhotonView.Find(viewID).gameObject;
        targetView.GetComponentInChildren<TextMesh>().text = name;
        cameraController.player = targetView;
    }

}