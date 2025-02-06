using UnityEngine;
using UnityEngine.InputSystem;

public class MenuAppearButton : MonoBehaviour
{
   [SerializeField] InputActionReference menuInputActionReference;
   [SerializeField] GameObject smallMenu;

   private void OnEnable()
   {
      menuInputActionReference.action.started += ChangeMenuState;
   }
   private void OnDisable()
   {
      menuInputActionReference.action.started -= ChangeMenuState;
   }
   void ChangeMenuState(InputAction.CallbackContext obj)
   {
      smallMenu.SetActive(!smallMenu.activeSelf);
   }

   
}
