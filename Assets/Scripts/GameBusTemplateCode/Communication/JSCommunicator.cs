using System;
using UnityEngine;
using System.Runtime.InteropServices;

public class JSCommunicator : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void SendAlertToWindow(string message);
    [DllImport("__Internal")]
    private static extern void SendMessageToParent(string message);

    [DllImport("__Internal")]
    private static extern void NotifyParentOfInit();

    public void SendMessageToGameBus(string message)
    {
        try
        {
            SendMessageToParent(message);
            Debug.LogFormat("Sent message to DOC: {0}", message);
        }
        catch(Exception e)
        {
            #if(UNITY_EDITOR)
            if(e.GetType() != typeof(EntryPointNotFoundException))
            {
                Debug.LogException(e, this);
            }
            #endif
        }
    }

    public void SendAlertToUnityWindow(string message)
    {
        try
        {
            SendAlertToWindow(message);
            Debug.LogFormat("Sent message to DOC: {0}", message);
        }
        catch(Exception e)
        {
            #if(UNITY_EDITOR)
            if(e.GetType() != typeof(EntryPointNotFoundException))
            {
                Debug.LogException(e, this);
            }
            #endif
        }
    }

    public void SendParentInitState()
    {
        try
        {
            NotifyParentOfInit();
        }
        catch(Exception e)
        {
            #if(UNITY_EDITOR)
            if(e.GetType() != typeof(EntryPointNotFoundException))
            {
                Debug.LogException(e, this);
            }
            #endif
        }
    }
}