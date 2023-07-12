using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    public PhotonView PV;
    public int Speed;
    //public GameObject avatar;
    public GameObject LocalVRCam;
    //public GameObject player;




    void Start()
    {
        PV = GetComponent<PhotonView>();

        if (PV.IsMine)
        {
            LocalVRCam = GameObject.Find("OVRPlayerController");

        }

    }


    void Update() 
    {
        if(PV.IsMine){
            
            Vector2 thumbstick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
            float h = thumbstick.x; 
            float v = thumbstick.y; 

            Vector3 dir = new Vector3(h, 0, v);
            dir =  Camera.main.transform.TransformDirection(dir);

            if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
                Speed = 10;
            else
                Speed = 5;

            LocalVRCam.GetComponent<CharacterController>().SimpleMove(dir * Speed); //VR캠을 움직이더라도, photon transform view를 추가하지 않았으니 동기화되는건 플레이어 위치 뿐인거지.

            this.transform.position = new Vector3(LocalVRCam.transform.position.x, LocalVRCam.transform.position.y-1.8f, LocalVRCam.transform.position.z);  //LocalVRCam.transform.position; //플레이어 위치에 카메라 위치를 대입
            this.transform.rotation = LocalVRCam.transform.rotation; //플레이어 각도에 카메라 각도를 대입
        }
    }
}
