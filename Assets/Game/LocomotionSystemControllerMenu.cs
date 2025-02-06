using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class LocomotionSystemControllerMenu : MonoBehaviour
{
  [SerializeField] private ControllerInputActionManager leftControllerInputActionManager;
  [SerializeField] private TextMeshProUGUI motionText;
  [SerializeField] private ControllerInputActionManager rightControllerInputActionManager;
  [SerializeField] private TextMeshProUGUI turnText;
  
  public void ChangeMotionMovement()
  {
    leftControllerInputActionManager.smoothMotionEnabled = !leftControllerInputActionManager.smoothMotionEnabled;

    switch (leftControllerInputActionManager.smoothMotionEnabled)
    {
      case true:
        motionText.text = "Smooth Movement";
        break;
      case false:
        motionText.text = "Teleport Movement";
        break;
    }
  }


  public void ChangeTurnMotion()
  {
    rightControllerInputActionManager.smoothTurnEnabled = !rightControllerInputActionManager.smoothTurnEnabled;

    switch (rightControllerInputActionManager.smoothTurnEnabled)
    {
      case true:
        turnText.text = "SmoothTurn";
        break;
      case false:
        turnText.text = "SnapTurn";
        break;
    }
  }
}
