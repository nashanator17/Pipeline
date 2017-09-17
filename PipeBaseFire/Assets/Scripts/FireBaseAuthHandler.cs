using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Auth;

public class FireBaseAuthHandler : MonoBehaviour {

    FirebaseAuth auth;

    private Button btn;
    private InputField usernameField;
    private InputField passwordField;

    // Use this for initialization
    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;

        btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);

        usernameField = GameObject.Find("UsernameField").GetComponent<InputField>();
        passwordField = GameObject.Find("PasswordField").GetComponent<InputField>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void TaskOnClick()
    {
        string email = usernameField.text;
        string password = passwordField.text;
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
        });
    }
}
