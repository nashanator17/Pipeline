using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Unity.Editor;

public class FireBaseAuthHandler : MonoBehaviour {

    FirebaseAuth auth;

    private Button btn;
    private InputField usernameField;
    private InputField passwordField;

    public static Firebase.Auth.FirebaseUser user;

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
            FireBaseAuthHandler.user = newUser;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
            //GameObject.Find("InputField").GetComponent<InputField>().text = "Successful Login!";

            /* TESTING ONLY */

            TestWriteToFirebase();

            /* /TESTING ONLY */
            // Start the main activity
            SceneManager.LoadScene(1);
        });
    }

    void TestWriteToFirebase()
    {
        //https://pipebasefire.firebaseio.com/
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://pipebasefire.firebaseio.com/");
        // Get the root reference location of the database.
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

        string userId = user.UserId;
        Note note = new Note(0.0, 0.0, "Why am I in the middle of the sea?");
        string json = JsonUtility.ToJson(note);

        string keyToPush = reference.Child("users").Child(userId).Child("notes").Push().Key;
        reference.Child("users").Child(userId).Child("notes").Child(keyToPush).SetRawJsonValueAsync(json);

    }
}
