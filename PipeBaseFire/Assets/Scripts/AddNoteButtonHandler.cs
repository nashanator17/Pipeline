using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Note
{
    /*
     {
        "id": "uuid",
        "location": [lat, long],
        "note": "note text"
     }
     */

    public string id;
    public double loclat;
    public double loclong;
    public string note;
}

public class AddNoteButtonHandler : MonoBehaviour {

    private Button btn;

    // Use this for initialization
    void Start () {
        btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void TaskOnClick()
    {

    }
}
