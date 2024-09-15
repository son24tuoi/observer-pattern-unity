using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Không có dữ liệu
public interface IEventHandler
{
    public void EventHandler();
}

// Có dữ liệu
public interface IEventHandlerWithData
{
    public void EventHandler(EventData eventData);
}
