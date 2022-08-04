using UnityEngine;
using System.Runtime.InteropServices;

public class DataManager : MonoBehaviour
{
    public string dataFromGameBus;
    public GameManager gameManager;
    private JSCommunicator jsCommunicator;

    void Awake()
    {
        dataFromGameBus = "";
        jsCommunicator =  gameObject.GetComponent<JSCommunicator>();
        jsCommunicator.SendParentInitState();
    }

    public void ReceiveDataFromGameBus(string data)
    {
        if(data != null || data != ""){
            Debug.Log("[IN UNITY GAME] GOT THIS MESSAGE FROM THE CHILD PAGE: " + data);
            dataFromGameBus = data;
            gameManager.ReceiveInitialDataFromGameBus();
        }else{
            Debug.Log("[IN UNITY GAME] MESSAGE RECIEVED FROM CHILD IS EMPTY: " + data);
            jsCommunicator.SendAlertToUnityWindow("[IN UNITY GAME] MESSAGE RECIEVED FROM CHILD IS EMPTY:");
        }
    }
}
