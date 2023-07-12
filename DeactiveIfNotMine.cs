using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DeactiveIfNotMine : MonoBehaviourPunCallbacks
{
    public Camera cam;
    void Start()
    {
        cam = GetComponentInChildren<Camera>();  //다른 플레이어의 카메라가 setactive false되기 전에 카메라끼리 혼동이 있기 때문에 카메라가 멈춘다. 그러므로 카메라 컴포넌트를 껐다켜주면 다시 돌아온다(그랬으면 좋겠다ㅋ)

        if(!photonView.IsMine){
            gameObject.SetActive(false);
            
            cam.enabled = false; //안되면 invoke도 줘보자.
            cam.enabled = true;
        }

    }

}
