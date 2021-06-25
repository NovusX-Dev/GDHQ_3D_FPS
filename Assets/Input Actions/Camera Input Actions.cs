// GENERATED AUTOMATICALLY FROM 'Assets/Input Actions/Camera Input Actions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @CameraInputActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @CameraInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Camera Input Actions"",
    ""maps"": [
        {
            ""name"": ""Camera Controls"",
            ""id"": ""738703fb-19dc-4a91-bdca-898545006bf2"",
            ""actions"": [
                {
                    ""name"": ""Look Y"",
                    ""type"": ""Value"",
                    ""id"": ""4754be81-51b7-47f2-b841-b54459f3d0cb"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""c9088396-ed2b-419f-8c7f-0e9638d671f2"",
                    ""path"": ""<Mouse>/delta/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Look Y"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Camera Controls
        m_CameraControls = asset.FindActionMap("Camera Controls", throwIfNotFound: true);
        m_CameraControls_LookY = m_CameraControls.FindAction("Look Y", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Camera Controls
    private readonly InputActionMap m_CameraControls;
    private ICameraControlsActions m_CameraControlsActionsCallbackInterface;
    private readonly InputAction m_CameraControls_LookY;
    public struct CameraControlsActions
    {
        private @CameraInputActions m_Wrapper;
        public CameraControlsActions(@CameraInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @LookY => m_Wrapper.m_CameraControls_LookY;
        public InputActionMap Get() { return m_Wrapper.m_CameraControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CameraControlsActions set) { return set.Get(); }
        public void SetCallbacks(ICameraControlsActions instance)
        {
            if (m_Wrapper.m_CameraControlsActionsCallbackInterface != null)
            {
                @LookY.started -= m_Wrapper.m_CameraControlsActionsCallbackInterface.OnLookY;
                @LookY.performed -= m_Wrapper.m_CameraControlsActionsCallbackInterface.OnLookY;
                @LookY.canceled -= m_Wrapper.m_CameraControlsActionsCallbackInterface.OnLookY;
            }
            m_Wrapper.m_CameraControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @LookY.started += instance.OnLookY;
                @LookY.performed += instance.OnLookY;
                @LookY.canceled += instance.OnLookY;
            }
        }
    }
    public CameraControlsActions @CameraControls => new CameraControlsActions(this);
    public interface ICameraControlsActions
    {
        void OnLookY(InputAction.CallbackContext context);
    }
}
