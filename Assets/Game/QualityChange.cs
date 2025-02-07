using System;
using Unity.XR.Oculus;
using Unity.XR.Oculus.Input;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.OpenXR.Features.Extensions.PerformanceSettings;

public class QualityChange : MonoBehaviour
{
    void Awake()
    {
        XRSettings.eyeTextureResolutionScale = 1.5f;
    }
}
