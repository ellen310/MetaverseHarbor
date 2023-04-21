using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.SceneManagement;
using TMPro;
using Photon.Pun;

public class DataManager : MonoBehaviour
{
    public string username, password, myID;

    public TMP_InputField ID_Input, PW_Input;



    
    public void ID_value_Changed()
    {
        username = ID_Input.text.ToString();
    }
    public void PW_value_Changed()
    {
        password = PW_Input.text.ToString();
    }




    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        PlayFabSettings.TitleId = "29700";
    }




    public void Login(){
        var request = new LoginWithPlayFabRequest{Username=username, Password = password};
        PlayFabClientAPI.LoginWithPlayFab(request, (result) => { print("로그인 성공"); myID = result.PlayFabId;  SceneManager.LoadScene("SampleScene"); }, (error) => print("로그인 실패"));
       
    }
    public void Register()
    {
        var request = new RegisterPlayFabUserRequest { Username = username, Password = password, RequireBothUsernameAndEmail = false}; //이메일은 안 받아도 되도록
        PlayFabClientAPI.RegisterPlayFabUser(request, (result) => { print("가입 성공");  }, (error) => print("가입 실패"));
    }




    
    //아직 딱히 저장할 데이터가 없어서 사용하진 않는 중. 나중에 사고대처기록 부분에서 기록 저장하고, 원한다면 읽어올 수 있도록.(랭킹매겨도 좋을듯 항만 구석탱이에 게시판같은거 하나 만들어놓고 거기에 랭킹출력)
    public void SetData(int viewID)
    {
        var request = new UpdateUserDataRequest() { Data = new Dictionary<string, string>() { { viewID.ToString(), username } } };
        PlayFabClientAPI.UpdateUserData(request, (result) => print("데이터 저장 성공"), (error) => print("데이터 저장 실패"));
    }
    //아직 딱히 가져올 데이터가 없어서 사용하진 않는 중.
    public void GetData()
    { 
        var request = new GetUserDataRequest() { PlayFabId = myID };
    }



}