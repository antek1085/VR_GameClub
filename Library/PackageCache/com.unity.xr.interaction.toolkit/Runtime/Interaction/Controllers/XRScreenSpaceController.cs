// These are the guards in TouchscreenGestureInputController.cs
#if ((ENABLE_VR || UNITY_GAMECORE) && AR_FOUNDATION_PRESENT) || PACKAGE_DOCS_GENERATION
#define TOUCHSCREEN_GESTURE_INPUT_CONTROLLER_AVAILABLE
#endif

using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using UnityEngine.XR.Interaction.Toolkit.UI;
using System;

#if TOUCHSCREEN_GESTURE_INPUT_CONTROLLER_AVAILABLE
using UnityEngine.XR.Interaction.Toolkit.AR.Inputs;
#endif

namespace UnityEngine.XR.Interaction.Toolkit
{
    /// <summary>
    /// Interprets screen presses and gestures by using actions from the  Input System and converting them
    /// into XR Interaction states, such as Select. It applies the current press position on the screen to
    /// move the transform of the GameObject.
    /// </summary>
    /// <remarks>
    /// This behavior requires that the Input System is enabled in the <b>Active Input Handling</b>
    /// setting in <b>Edit &gt; Project Settings &gt; Player</b> for input values to be read.
    /// Each input action must also be enabled to read the current value of the action. Referenced
    /// input actions in an Input Action Asset are not enabled by default.
    /// </remarks>
    /// <seealso cref="XRBaseController"/>
    /// <seealso cref="TouchscreenGestureInputController"/>
    [AddComponentMenu("XR/XR Screen Space Controller", 11)]
    [HelpURL(XRHelpURLConstants.k_XRScreenSpaceController)]
    [Obsolete("XRScreenSpaceController has been deprecated in version 3.0.0. Its functionality has been distributed into different components.")]
    public class XRScreenSpaceController : XRBaseController
    {
        [Header("Touchscreen Gesture Actions")]
        [SerializeField]
        [Tooltip("When enabled, a Touchscreen Gesture Input Controller will be added to the Input System device list to detect touch gestures.")]
        bool m_EnableTouchscreenGestureInputController = true;
        /// <summary>
        /// When enabled, a <see cref="TouchscreenGestureInputController"/> will be added to the Input System device list to detect touch gestures.
        /// This input controller drives the gesture values for the input actions for the screen space controller.
        /// </summary>
        public bool enableTouchscreenGestureInputController
        {
            get => m_EnableTouchscreenGestureInputController;
            set => m_EnableTouchscreenGestureInputController = value;
        }

        [SerializeField]
        [Tooltip("The action to use for the screen tap position. (Vector 2 Control).")]
        InputActionProperty m_TapStartPositionAction = new InputActionProperty(new InputAction("Tap Start Position", expectedControlType: "Vector2"));
        /// <summary>
        /// The Input System action to use for reading screen Tap Position for this GameObject. Must be a <see cref="Vector2Control"/> Control.
        /// </summary>
        public InputActionProperty tapStartPositionAction
        {
            get => m_TapStartPositionAction;
            set => SetInputActionProperty(ref m_TapStartPositionAction, value);
        }

        [SerializeField]
        [Tooltip("The action to use for the current screen drag position. (Vector 2 Control).")]
        InputActionProperty m_DragCurrentPositionAction = new InputActionProperty(new InputAction("Drag Current Position", expectedControlType: "Vector2"));
        /// <summary>
        /// The Input System action to use for reading the screen Drag Position for this GameObject. Must be a <see cref="Vector2Control"/> Control.
        /// </summary>
        /// <seealso cref="dragDeltaAction"/>
        public InputActionProperty dragCurrentPositionAction
        {
            get => m_DragCurrentPositionAction;
            set => SetInputActionProperty(ref m_DragCurrentPositionAction, value);
        }

        [SerializeField]
        [Tooltip("The action to use for the delta of the screen drag. (Vector 2 Control).")]
        InputActionProperty m_DragDeltaAction = new InputActionProperty(new InputAction("Drag Delta", expectedControlType: "Vector2"));
        /// <summary>
        /// The Input System action used to read the delta Drag values for this GameObject. Must be a <see cref="Vector2Control"/> Control.
        /// </summary>
        /// <seealso cref="dragCurrentPositionAction"/>
        public InputActionProperty dragDeltaAction
        {
            get => m_DragDeltaAction;
            set => SetInputActionProperty(ref m_DragDeltaAction, value);
        }

        [SerializeField, FormerlySerializedAs("m_PinchStartPosition")]
        [Tooltip("The action to use for the screen pinch gesture start position. (Vector 2 Control).")]
        InputActionProperty m_PinchStartPositionAction = new InputActionProperty(new InputAction("Pinch Start Position", expectedControlType: "Vector2"));
        /// <summary>
        /// The Input System action to use for reading the Pinch Start Position for this GameObject. Must be a <see cref="Vector2Control"/> Control.
        /// </summary>
        /// <seealso cref="pinchGapDeltaAction"/>
        public InputActionProperty pinchStartPositionAction
        {
            get => m_PinchStartPositionAction;
            set => SetInputActionProperty(ref m_PinchStartPositionAction, value);
        }

        [SerializeField]
        [Tooltip("The action to use for the gap of the screen pinch gesture. (Axis Control).")]
        InputActionProperty m_PinchGapAction = new InputActionProperty(new InputAction(expectedControlType: "Axis"));
        /// <summary>
        /// The Input System action used to read the Pinch values for this GameObject. Must be an <see cref="AxisControl"/> Control.
        /// </summary>
        /// <seealso cref="pinchGapDeltaAction"/>
        public InputActionProperty pinchGapAction
        {
            get => m_PinchGapAction;
            set => SetInputActionProperty(ref m_PinchGapAction, value);
        }

        [SerializeField]
        [Tooltip("The action to use for the delta of the screen pinch gesture. (Axis Control).")]
        InputActionProperty m_PinchGapDeltaAction = new InputActionProperty(new InputAction("Pinch Gap Delta", expectedControlType: "Axis"));
        /// <summary>
        /// The Input System action used to read the delta Pinch values for this GameObject. Must be an <see cref="AxisControl"/> Control.
        /// </summary>
        /// <seealso cref="pinchStartPositionAction"/>
        public InputActionProperty pinchGapDeltaAction
        {
            get => m_PinchGapDeltaAction;
            set => SetInputActionProperty(ref m_PinchGapDeltaAction, value);
        }

        [SerializeField, FormerlySerializedAs("m_TwistStartPosition")]
        [Tooltip("The action to use for the screen twist gesture start position. (Vector 2 Control).")]
        InputActionProperty m_TwistStartPositionAction = new InputActionProperty(new InputAction("Twist Start Position", expectedControlType: "Vector2"));
        /// <summary>
        /// The Input System action to use for reading the Twist Start Position for this GameObject. Must be a <see cref="Vector2Control"/> Control.
        /// </summary>
        /// <seealso cref="twistDeltaRotationAction"/>
        public InputActionProperty twistStartPositionAction
        {
            get => m_TwistStartPositionAction;
            set => SetInputActionProperty(ref m_TwistStartPositionAction, value);
        }

        [SerializeField, FormerlySerializedAs("m_TwistRotationDeltaAction")]
        [Tooltip("The action to use for the delta of the screen twist gesture. (Axis Control).")]
        InputActionProperty m_TwistDeltaRotationAction = new InputActionProperty(new InputAction("Twist Delta Rotation", expectedControlType: "Axis"));
        /// <summary>
        /// The Input System action used to read the delta Twist values for this GameObject. Must be an <see cref="AxisControl"/> Control.
        /// </summary>
        /// <seealso cref="twistStartPositionAction"/>
        public InputActionProperty twistDeltaRotationAction
        {
            get => m_TwistDeltaRotationAction;
            set => SetInputActionProperty(ref m_TwistDeltaRotationAction, value);
        }

        [SerializeField, FormerlySerializedAs("m_ScreenTouchCount")]
        [Tooltip("The number of concurrent touches on the screen. (Integer Control).")]
        InputActionProperty m_ScreenTouchCountAction = new InputActionProperty(new InputAction("Screen Touch Count", expectedControlType: "Integer"));
        /// <summary>
        /// The number of concurrent touches on the screen. Must be an <see cref="IntegerControl"/> Control.
        /// </summary>
        public InputActionProperty screenTouchCountAction
        {
            get => m_ScreenTouchCountAction;
            set => SetInputActionProperty(ref m_ScreenTouchCountAction, value);
        }

        [SerializeField]
        [Tooltip("The camera associated with the screen, and through which screen presses/touches will be interpreted.")]
        Camera m_ControllerCamera;
        /// <summary>
        /// The camera associated with the screen, and through which screen presses/touches will be interpreted.
        /// </summary>
        public Camera controllerCamera
        {
            get => m_ControllerCamera;
            set => m_ControllerCamera = value;
        }

        [SerializeField]
        [Tooltip("Tells the XR Screen Space Controller to ignore interactions when hitting a screen space canvas.")]
        bool m_BlockInteractionsWithScreenSpaceUI = true;
        /// <summary>
        /// Tells the XR Screen Space Controller to ignore interactions when hitting a screen space canvas.
        /// </summary>
        /// <seealso cref="Canvas.renderMode"/>
        public bool blockInteractionsWithScreenSpaceUI
        {
            get => m_BlockInteractionsWithScreenSpaceUI;
            set => m_BlockInteractionsWithScreenSpaceUI = value;
        }

        [SerializeField]
        [Tooltip("Enables a rotation threshold that blocks pinch scale gestures when surpassed.")]
        bool m_UseRotationThreshold = true;
        /// <summary>
        /// Enables a rotation threshold that blocks pinch scale gestures when surpassed.
        /// </summary>
        /// <seealso cref="rotationThreshold"/>
        public bool useRotationThreshold
        {
            get => m_UseRotationThreshold;
            set => m_UseRotationThreshold = value;
        }

        [SerializeField]
        [Tooltip("The threshold at which a gestures will be interpreted only as rotation and not a pinch scale gesture.")]
        float m_RotationThreshold = 0.02f;

        /// <summary>
        /// The threshold at which a gestures will be interpreted only as rotation and not a pinch scale gesture.
        /// </summary>
        /// <seealso cref="useRotationThreshold"/>
        public float rotationThreshold
        {
            get => m_RotationThreshold;
            set => m_RotationThreshold = value;
        }

        /// <summary>
        /// This value is the change in scale based on input from the <see cref="pinchGapDeltaAction"/> pinch gap delta action
        /// with <see cref="Screen.dpi"/> applied as a factor of the value read in. The delta refers to the change from the previous frame.
        /// </summary>
        /// <remarks>
        /// This value may come back as zero if the input action is not assigned or cannot be read.
        /// </remarks>
        public float scaleDelta { get; private set; }

        /// <summary>
        /// (Deprecated) pinchStartPosition has been deprecated. Use pinchStartPositionAction instead.
        /// </summary>
        [Obsolete("pinchStartPosition has been deprecated. Use pinchStartPositionAction instead. (UnityUpgradable) -> pinchStartPositionAction", true)]
        public InputActionProperty pinchStartPosition
        {
            get => default;
            set => _ = value;
        }

        /// <summary>
        /// (Deprecated) pinchGapDelta has been deprecated. Use pinchGapDeltaAction instead.
        /// </summary>
        [Obsolete("pinchGapDelta has been deprecated. Use pinchGapDeltaAction instead. (UnityUpgradable) -> pinchGapDeltaAction", true)]
        public InputActionProperty pinchGapDelta
        {
            get => default;
            set => _ = value;
        }

        /// <summary>
        /// (Deprecated) twistStartPosition has been deprecated. Use twistStartPositionAction instead.
        /// </summary>
        [Obsolete("twistStartPosition has been deprecated. Use twistStartPositionAction instead. (UnityUpgradable) -> twistStartPositionAction", true)]
        public InputActionProperty twistStartPosition
        {
            get => default;
            set => _ = value;
        }

        /// <summary>
        /// (Deprecated) twistRotationDeltaAction has been deprecated. Use twistDeltaRotationAction instead.
        /// </summary>
        [Obsolete("twistRotationDeltaAction has been deprecated. Use twistDeltaRotationAction instead. (UnityUpgradable) -> twistDeltaRotationAction", true)]
        public InputActionProperty twistRotationDeltaAction
        {
            get => default;
            set => _ = value;
        }

        /// <summary>
        /// (Deprecated) screenTouchCount has been deprecated. Use screenTouchCountAction instead.
        /// </summary>
        [Obsolete("screenTouchCount has been deprecated. Use screenTouchCountAction instead. (UnityUpgradable) -> screenTouchCountAction", true)]
        public InputActionProperty screenTouchCount
        {
            get => default;
            set => _ = value;
        }

#if TOUCHSCREEN_GESTURE_INPUT_CONTROLLER_AVAILABLE
        TouchscreenGestureInputController m_GestureInputController;
#endif
        bool m_HasCheckedDisabledTrackingInputReferenceActions;
        bool m_HasCheckedDisabledInputReferenceActions;
        UIInputModule m_UIInputModule;

        /// <summary>
        /// See <see cref="MonoBehaviour"/>.
        /// </summary>
        protected void Start()
        {
            if (m_ControllerCamera == null)
            {
                m_ControllerCamera = Camera.main;
                if (m_ControllerCamera == null)
                {
                    Debug.LogWarning($"Could not find associated {nameof(Camera)} in scene." +
                        $"This {nameof(XRScreenSpaceController)} will be disabled.", this);
                    enabled = false;
                }
            }
        }

        /// <inheritdoc />
        protected override void OnEnable()
        {
            base.OnEnable();
            EnableAllDirectActions();
            InitializeTouchscreenGestureController();
        }

        /// <inheritdoc />
        protected override void OnDisable()
        {
            base.OnDisable();
            DisableAllDirectActions();
            RemoveTouchscreenGestureController();
            m_UIInputModule = null;
        }

        /// <inheritdoc />
        protected override void UpdateTrackingInput(XRControllerState controllerState)
        {
            base.UpdateTrackingInput(controllerState);
            if (controllerState == null || IsPointerOverScreenSpaceCanvas())
                return;

            // Warn the user if using referenced actions and they are disabled
            if (!m_HasCheckedDisabledTrackingInputReferenceActions &&
                (m_DragCurrentPositionAction.action != null || m_TapStartPositionAction.action != null || m_TwistStartPositionAction.action != null))
            {
                if (IsDisabledReferenceAction(m_DragCurrentPositionAction) ||
                    IsDisabledReferenceAction(m_TapStartPositionAction) ||
                    IsDisabledReferenceAction(m_TwistStartPositionAction))
                {
                    Debug.LogWarning("'Enable Input Tracking' is enabled, but the Tap, Drag, Pinch, and/or Twist Action is disabled." +
                        " The pose of the controller will not be updated correctly until the Input Actions are enabled." +
                        " Input Actions in an Input Action Asset must be explicitly enabled to read the current value of the action." +
                        " The Input Action Manager behavior can be added to a GameObject in a Scene and used to enable all Input Actions in a referenced Input Action Asset.",
                        this);
                }

                m_HasCheckedDisabledTrackingInputReferenceActions = true;
            }

            var currentTouchCount = m_ScreenTouchCountAction.action?.ReadValue<int>() ?? 0;

            if (TryGetCurrentPositionAction(currentTouchCount, out var posAction))
            {
                var screenPos = posAction.ReadValue<Vector2>();
                var screenToWorldPoint = m_ControllerCamera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, m_ControllerCamera.nearClipPlane));
                var directionVector = (screenToWorldPoint - m_ControllerCamera.transform.position).normalized;
                controllerState.position = transform.parent != null ? transform.parent.InverseTransformPoint(screenToWorldPoint) : screenToWorldPoint;
                controllerState.rotation = Quaternion.LookRotation(directionVector);
                controllerState.inputTrackingState = InputTrackingState.Position | InputTrackingState.Rotation;
                controllerState.isTracked = currentTouchCount > 0;
            }
            else
            {
                controllerState.inputTrackingState = InputTrackingState.None;
                controllerState.isTracked = false;
            }
        }

        /// <inheritdoc />
        protected override void UpdateInput(XRControllerState controllerState)
        {
            base.UpdateInput(controllerState);
            if (controllerState == null || IsPointerOverScreenSpaceCanvas())
                return;

            // Warn the user if using referenced actions and they are disabled
            if (!m_HasCheckedDisabledInputReferenceActions &&
                (m_TwistDeltaRotationAction.action != null || m_DragCurrentPositionAction.action != null || m_TapStartPositionAction.action != null))
            {
                if (IsDisabledReferenceAction(m_TwistDeltaRotationAction) ||
                    IsDisabledReferenceAction(m_DragCurrentPositionAction) ||
                    IsDisabledReferenceAction(m_TapStartPositionAction))
                {
                    Debug.LogWarning("'Enable Input Actions' is enabled, but the Tap, Drag, Pinch, and/or Twist Action is disabled." +
                        " The controller input will not be handled correctly until the Input Actions are enabled." +
                        " Input Actions in an Input Action Asset must be explicitly enabled to read the current value of the action." +
                        " The Input Action Manager behavior can be added to a GameObject in a Scene and used to enable all Input Actions in a referenced Input Action Asset.",
                        this);
                }

                m_HasCheckedDisabledInputReferenceActions = true;
            }

            controllerState.ResetFrameDependentStates();

            if (TryGetCurrentTwoInputSelectAction(out var twoInputSelectAction))
            {
                controllerState.selectInteractionState.SetFrameState(true, twoInputSelectAction.ReadValue<float>());
            }
            else if (TryGetCurrentOneInputSelectAction(out var oneInputSelectAction))
            {
                controllerState.selectInteractionState.SetFrameState(true, oneInputSelectAction.ReadValue<Vector2>().magnitude);
            }
            else
            {
                controllerState.selectInteractionState.SetFrameState(false, 0f);
            }

            if (m_UseRotationThreshold && TryGetAbsoluteValue(m_TwistDeltaRotationAction.action, out var absRotationValue) && absRotationValue >= m_RotationThreshold)
            {
                scaleDelta = 0f;
            }
            else
            {
                scaleDelta = m_PinchGapDeltaAction.action != null ? m_PinchGapDeltaAction.action.ReadValue<float>() / Screen.dpi : 0f;
            }
        }

        bool TryGetCurrentPositionAction(int touchCount, out InputAction action)
        {
            if (touchCount <= 1)
            {
                if (m_DragCurrentPositionAction.action != null &&
                    m_DragCurrentPositionAction.action.IsInProgress())
                {
                    action = m_DragCurrentPositionAction.action;
                    return true;
                }

                if (m_TapStartPositionAction.action != null &&
                    m_TapStartPositionAction.action.WasPerformedThisFrame())
                {
                    action = m_TapStartPositionAction.action;
                    return true;
                }
            }

            action = null;
            return false;
        }

        bool TryGetCurrentOneInputSelectAction(out InputAction action)
        {
            if (m_DragCurrentPositionAction.action != null &&
                m_DragCurrentPositionAction.action.IsInProgress())
            {
                action = m_DragCurrentPositionAction.action;
                return true;
            }

            if (m_TapStartPositionAction.action != null &&
                m_TapStartPositionAction.action.WasPerformedThisFrame())
            {
                action = m_TapStartPositionAction.action;
                return true;
            }

            action = null;
            return false;
        }

        bool TryGetCurrentTwoInputSelectAction(out InputAction action)
        {
            if (m_PinchGapAction.action != null &&
                m_PinchGapAction.action.IsInProgress())
            {
                action = m_PinchGapAction.action;
                return true;
            }

            if (m_PinchGapDeltaAction.action != null &&
                m_PinchGapDeltaAction.action.IsInProgress())
            {
                action = m_PinchGapDeltaAction.action;
                return true;
            }

            if (m_TwistDeltaRotationAction.action != null &&
                m_TwistDeltaRotationAction.action.IsInProgress())
            {
                action = m_TwistDeltaRotationAction.action;
                return true;
            }

            action = null;
            return false;
        }

        static bool TryGetAbsoluteValue(InputAction action, out float value)
        {
            if (action != null &&
                action.IsInProgress())
            {
                value = Mathf.Abs(action.ReadValue<float>());
                return true;
            }

            value = 0f;
            return false;
        }

        bool FindUIInputModule()
        {
            var eventSystem = EventSystem.current;
            if (eventSystem != null && eventSystem.currentInputModule != null)
            {
                m_UIInputModule = eventSystem.currentInputModule as UIInputModule;
            }
            return m_UIInputModule != null;
        }

        bool IsPointerOverScreenSpaceCanvas()
        {
            if (m_BlockInteractionsWithScreenSpaceUI)
            {
                if (m_UIInputModule != null || FindUIInputModule())
                {
                    var uiObject = m_UIInputModule.GetCurrentGameObject(-1);
                    if (uiObject == null)
                        return false;

                    var canvas = uiObject.GetComponentInParent<Canvas>();
                    var renderMode = canvas.renderMode;
                    return renderMode == RenderMode.ScreenSpaceOverlay || renderMode == RenderMode.ScreenSpaceCamera;
                }
            }

            return false;
        }

        void InitializeTouchscreenGestureController()
        {
#if TOUCHSCREEN_GESTURE_INPUT_CONTROLLER_AVAILABLE
            if (!m_EnableTouchscreenGestureInputController)
                return;

            if (m_GestureInputController == null)
            {
                m_GestureInputController = InputSystem.InputSystem.AddDevice<TouchscreenGestureInputController>();
                if (m_GestureInputController == null)
                {
                    Debug.LogError($"Failed to create {nameof(TouchscreenGestureInputController)}.", this);
                }
            }
            else
            {
                InputSystem.InputSystem.AddDevice(m_GestureInputController);
            }
#endif
        }

        void RemoveTouchscreenGestureController()
        {
#if TOUCHSCREEN_GESTURE_INPUT_CONTROLLER_AVAILABLE
            if (m_GestureInputController != null && m_GestureInputController.added)
            {
                InputSystem.InputSystem.RemoveDevice(m_GestureInputController);
            }
#endif
        }

        void EnableAllDirectActions()
        {
            m_TapStartPositionAction.EnableDirectAction();
            m_DragCurrentPositionAction.EnableDirectAction();
            m_DragDeltaAction.EnableDirectAction();
            m_PinchStartPositionAction.EnableDirectAction();
            m_PinchGapAction.EnableDirectAction();
            m_PinchGapDeltaAction.EnableDirectAction();
            m_TwistStartPositionAction.EnableDirectAction();
            m_TwistDeltaRotationAction.EnableDirectAction();
            m_ScreenTouchCountAction.EnableDirectAction();
        }

        void DisableAllDirectActions()
        {
            m_TapStartPositionAction.DisableDirectAction();
            m_DragCurrentPositionAction.DisableDirectAction();
            m_DragDeltaAction.DisableDirectAction();
            m_PinchStartPositionAction.DisableDirectAction();
            m_PinchGapAction.DisableDirectAction();
            m_PinchGapDeltaAction.DisableDirectAction();
            m_TwistStartPositionAction.DisableDirectAction();
            m_TwistDeltaRotationAction.DisableDirectAction();
            m_ScreenTouchCountAction.DisableDirectAction();
        }

        void SetInputActionProperty(ref InputActionProperty property, InputActionProperty value)
        {
            if (Application.isPlaying)
                property.DisableDirectAction();

            property = value;

            if (Application.isPlaying && isActiveAndEnabled)
                property.EnableDirectAction();
        }

        static bool IsDisabledReferenceAction(InputActionProperty property) =>
            property.reference != null && property.reference.action != null && !property.reference.action.enabled;
    }
}
