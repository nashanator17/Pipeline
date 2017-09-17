using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public double loclat;
    public double loclong;
    public string note;
    public int timestamp;

    public Note(double loclat, double loclong, string note)
    {
        this.loclat = loclat;
        this.loclong = loclong;
        this.note = note;
        this.timestamp = DBUtils.GetUnixTimestamp();
    }

    public Note(double loclat, double loclong, string note, int timestamp)
    {
        this.loclat = loclat;
        this.loclong = loclong;
        this.note = note;
        this.timestamp = timestamp;
    }

}

public static class DBUtils {

    public static int GetUnixTimestamp()
    {
        System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
        int cur_time = (int)(System.DateTime.UtcNow - epochStart).TotalSeconds;
        return cur_time;
    }
}
