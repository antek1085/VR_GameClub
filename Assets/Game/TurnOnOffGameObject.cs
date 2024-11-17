using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Comfort;

public class TurnOnOffGameObject : MonoBehaviour
{
    [SerializeField] private GameObject gameObject;
    [SerializeField] private GameObject anotherObject;
    [SerializeField] private Dropdown m_Dropdown;
    [SerializeField] private TunnelingVignetteController vignette;

    private void Awake()
    {
        m_Dropdown = anotherObject.GetComponent<Dropdown>();
        m_Dropdown.onValueChanged.AddListener(delegate {
            DropdownValueChanged(m_Dropdown);
        });
    }


    public void ChangeActiv()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        if (anotherObject != null)
        {
            switch (gameObject.activeSelf)
            {
                case true:
                    anotherObject.SetActive(true);
                    break;
                case false:
                    anotherObject.SetActive(false);
                    break;
            }
        }
    }

    void  DropdownValueChanged(Dropdown change)
    {
        switch (change.value)
        {
            case 0:
                vignette.defaultParameters.apertureSize = 0.7f;
                vignette.defaultParameters.featheringEffect = 0.4f;
                break;
            case 1:
                vignette.defaultParameters.apertureSize = 0.5f;
                vignette.defaultParameters.featheringEffect = 0.4f;
                break;
            case 2:
                vignette.defaultParameters.apertureSize = 0.2f;
                vignette.defaultParameters.featheringEffect = 0.4f;
                break;
        }
    }
}
