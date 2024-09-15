using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventData
{
    public EventID eventID;

    public object[] data;

    public EventData(EventID eventID, object[] data)
    {
        this.eventID = eventID;
        this.data = data;
    }
}
