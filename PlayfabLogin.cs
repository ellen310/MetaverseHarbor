using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayfabLogin : MonoBehaviour
{
    private string username;
    private string password;

    private string email;

    public TMP_InputField ID_Input;
    public TMP_InputField PW_Input;

    public TMP_InputField Email_Input;

public bool isFilledName=false;
    public void isBtnClicked(){
        isFilledName = true;
    }


    public void ID_value_Changed()
    {
        username = ID_Input.text.ToString();
    }
    
    public void PW_value_Changed()
    {
        password = PW_Input.text.ToString();
    }

    public void Email_value_Changed()
    {
        email = Email_Input.text.ToString();
    }


    // Start is called before the first frame update
    void Start()
    {
        PlayFabSettings.TitleId = "29700";
    }


    public void Login(){
        var request = new LoginWithPlayFabRequest{Username=username, Password = password};
        PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSuccess, OnLoginFailure);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("로그인 성공");
        SceneManager.LoadScene("SampleScene");
    }
    
    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogWarning("로그인 실패");
        Debug.LogWarning(error.GenerateErrorReport());
    }


    public void Register()
    {
        var request = new RegisterPlayFabUserRequest { Username = username, Password = password, Email = email };
        PlayFabClientAPI.RegisterPlayFabUser(request, RegisterSuccess, RegisterFailure);
    }
    
    private void RegisterSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("가입 성공");
    }
    
    private void RegisterFailure(PlayFabError error)
    {
        Debug.LogWarning("가입 실패");
        Debug.LogWarning(error.GenerateErrorReport());
    }
}
