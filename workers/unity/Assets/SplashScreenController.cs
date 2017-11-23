
using Assets.Gamelogic;
using Assets.Gamelogic.Core;
using Assets.Gamelogic.Utils;
using Improbable.Unity.Core;
using UnityEngine.UI;
using UnityEngine;

public class SplashScreenController : MonoBehaviour
{
    [SerializeField]
    public Button ConnectButton;

    public void AttemptSpatialOsConnection()
    {
        DisableConnectionButton();
        AttemptConnection();
    }

    private void DisableConnectionButton()
    {
        ConnectButton.interactable = false;

    }

    private void AttemptConnection()
    {
        FindObjectOfType<Bootstrap>().ConnectToClient();
        StartCoroutine(TimerUtils.WaitAndPerform(SimulationSettings.ClientConnectionTimeoutSecs, ConnectionTimeout));
    }

    private void ConnectionTimeout()
    {
        if (SpatialOS.IsConnected)
        {
            SpatialOS.Disconnect();
        }

        ConnectButton.interactable = true;
    }
}
