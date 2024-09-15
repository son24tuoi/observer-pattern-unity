using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleSubcriber : MonoBehaviour, IEventHandler, IEventHandlerWithData
{
    #region Fields

    [SerializeField] private Material material;
    [SerializeField] private Color defaultColor = Color.white;

    #endregion Fields

    // -------------------------------------------------------------------------------------------------

    #region Properties



    #endregion Properties

    // -------------------------------------------------------------------------------------------------

    #region Unity Lifecycle Methods

    private void Start()
    {
        EventManager.Instance.Subcribe(EventID.Template, this as IEventHandler);
        EventManager.Instance.Subcribe(EventID.Template, this as IEventHandlerWithData);
    }

    private void OnDestroy()
    {
        EventManager.Instance.Unsubcribe(EventID.Template, this as IEventHandler);
        EventManager.Instance.Unsubcribe(EventID.Template, this as IEventHandlerWithData);
    }


    #endregion Unity Lifecycle Methods

    // -------------------------------------------------------------------------------------------------




    public void EventHandler()
    {
        ChangeColorMaterial(defaultColor);
    }

    public void EventHandler(EventData eventData)
    {
        Color targetColor = (Color)eventData.data[0];
        ChangeColorMaterial(targetColor);
    }

    public void ChangeColorMaterial(Color color)
    {
        material.color = color;
    }
}
