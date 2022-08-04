# Minigame Template#


### Changes needed to be made

Step 1: Change the domain values in the Assets/WebGLTemplates/GameBusTemplate/Index.html file to match the GameBus frontend domain and minigame domain that you are using

        //TODO: CHANGE THESE VALUES 
        var gameBusDomain = "https://localhost:8100"; //NO slashes after the domain:(e.g: "https://app3.gamebus.eu")
        var miniGameDomain = "https://localhost:8081"; //NO slashes after the domain:(e.g: "https://lorenzo456.github.io")

Step 2: Change the Enum of in the Assets/Scripts/GameBusTemplate/Properties/GamePropertyEnum.cs file to match the GameProperties that your game has.

        public enum GamePropertyEnum
        {
            //TODO: ADD/REMOVE enum values to reflect the Game Properties that are in your game
            MINIGAME_TIME,
            MINIGAME_Complexity
        }

Step 3: Change the values that the Enum of Step 2 will return, based on the value your propertyTK in the GameBusData have. In the Assets/Scripts/GameBusTemplate/GameManager.cs file 

        private string GamePropertyEnumToStringMapper(GamePropertyEnum gamePropertyEnum)
        {
            // Hardcode the value that the GamePropertyEnum will return, base it on the propertyTK 
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

Step 4: In order to overwrite the GameProperties you recieve from GameBus, update the OverwriteGameBusData() function found in the Assets/Scripts/GameBusTemplate/GameManager.cs file. The Parameters should be the values that you are using in your game, and in the FOR loop you overwrite the GameProperties that you received from GameBus with these parameter values. 

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


### Building the Game 

Step 1: Make sure that the Unity Build Settings are set on WebGL 


Step 2: Make sure that in the "PlayerSettings/WebGLSettings/Resolution and Presentation/WebGL Template" the WebGL Template is set on GameBusTemplate


Step 3: Build the game and host the built game online (Don't forget to update the domain URL in the HTML file as previously stated)