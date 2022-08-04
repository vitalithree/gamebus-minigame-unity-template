using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public JSCommunicator jsCommunicator;
    public DataManager dataManager;
    private FrameData processedDataFromGameBus;


    //TEMPLATE EXAMPLE CAN BE REMOVED
    public void SendExampleDataToGameBus(){
        SendGameBusDataToParent(2.2f, true);
    }

    public void ReceiveInitialDataFromGameBus(){
        processedDataFromGameBus = MapGameBusData(); 
    }

    private string GamePropertyEnumToStringMapper(GamePropertyEnum gamePropertyEnum)
    {
        // TODO: Hardcode the value that the GamePropertyEnum will return, base it on the propertyTK 
        // value in the PropertyInstances found in the GameBusData.  
        if (gamePropertyEnum == GamePropertyEnum.MINIGAME_Complexity)
        {
            return "COMPLEXITY";
        }
        else if (gamePropertyEnum == GamePropertyEnum.MINIGAME_TIME)
        {
            return "MINIGAME_TIME";
        }
        else
        {
            return null;
        }
    }

    private FrameData MapGameBusData()
    {
        Debug.Log("[IN UNITY GAME] MAPPING FRAMEDATA: " + dataManager.dataFromGameBus);
        if (dataManager.dataFromGameBus == "")
        {
            Debug.Log("[IN UNITY GAME] ERROR GAMEBUSDATA IS EMPTY");
            jsCommunicator.SendAlertToUnityWindow("[IN UNITY GAME] ERROR GAMEBUSDATA IS EMPTY");
            return null;
        }

        try{
        return JsonUtility.FromJson<FrameData>(dataManager.dataFromGameBus);
        }catch(Exception e){
            Debug.Log("[IN UNITY GAME] ERROR MAPPING FRAMEDATA: " + e.ToString());
            return null;
        }        
    }

    public string OverwriteGameBusData(double timeAlive, bool completedLevel)
    {
        if (processedDataFromGameBus == null)
        {
            Debug.Log("[IN UNITY GAME] GAMEBUSDATA IS NOT MAPPED");
            jsCommunicator.SendAlertToUnityWindow("[IN UNITY GAME] GAMEBUSDATA IS NOT MAPPED");
            return null;
        }

        //Change the values you are overwriting based on the variables and gameproperties in your game
        for (int i = 0; i < processedDataFromGameBus.activityForm.propertyInstances.Length; i++)
        {
            if (processedDataFromGameBus.activityForm.propertyInstances[i].propertyTK == GamePropertyEnumToStringMapper(GamePropertyEnum.MINIGAME_Complexity))
            {
                processedDataFromGameBus.activityForm.propertyInstances[i].value = completedLevel.ToString();
            }
            else if (processedDataFromGameBus.activityForm.propertyInstances[i].propertyTK == GamePropertyEnumToStringMapper(GamePropertyEnum.MINIGAME_TIME))
            {
                processedDataFromGameBus.activityForm.propertyInstances[i].value = timeAlive.ToString();
            }
        }

        processedDataFromGameBus.gameData = processedDataFromGameBus.activityForm.propertyInstances;
        return JsonUtility.ToJson(processedDataFromGameBus);
    }

    public void SendGameBusDataToParent(double timeAlive, bool completedLevel)
    {
        string tempJson = OverwriteGameBusData(timeAlive, completedLevel);
        if(tempJson != null)
        {
            jsCommunicator.SendMessageToGameBus(tempJson);
            Debug.Log("[IN UNITY GAME] DATA SENDING TO GAMEBUS: " + tempJson);
        }else{
            Debug.Log("[IN UNITY GAME] DATA FAILED TO SEND TO GAMEBUS");
            jsCommunicator.SendAlertToUnityWindow("[IN UNITY GAME] DATA FAILED TO SEND TO GAMEBUS");
        }
    }
}