//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Scripts/Controls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @Controls : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""Arcade"",
            ""id"": ""d6c8a3a1-99b0-4103-a3e8-e0f53c1a1c47"",
            ""actions"": [
                {
                    ""name"": ""Enter"",
                    ""type"": ""Button"",
                    ""id"": ""15e08590-4211-45f9-acff-793a48529eab"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Left"",
                    ""type"": ""Button"",
                    ""id"": ""819f42f0-79ac-415d-8886-23fbbef9193c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Right"",
                    ""type"": ""Button"",
                    ""id"": ""db6e13d6-d57f-4245-a378-8231b31b40a9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Down"",
                    ""type"": ""Button"",
                    ""id"": ""8fe6fe34-e1fa-4af8-bd73-b8a0ec329537"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Up"",
                    ""type"": ""Button"",
                    ""id"": ""6e4a9be6-46dd-449c-8e19-fda8479c148c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Info"",
                    ""type"": ""Button"",
                    ""id"": ""ae0b4584-f3e5-4093-8041-c5e2f30ac27d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Volume"",
                    ""type"": ""Button"",
                    ""id"": ""0ede491d-91b0-49a7-8ad0-a06b09854201"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""963b4bc8-720a-401d-aaf6-ee4c7a693077"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Enter"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9c46fae7-4164-4567-9e23-8e915b448d81"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Enter"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cc78a203-569b-4a80-8215-31682fe8bec2"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Enter"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d77bfeff-e429-4199-a25d-15a9b8ee1edb"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""47e41385-7b22-46c0-a352-55ece1d59e02"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b6a49908-eb41-47be-808a-79f137832d90"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4170444f-569f-43fa-bfe5-262636d138e3"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""52dd2cdc-e1be-4095-8e4c-86a3e0b313ef"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3676c898-6e82-442d-8258-f79b384e963c"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c6b21793-c42b-4b81-acb8-1271038eca9d"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c71eef25-ab18-46f9-beca-9d1c3a7dbb49"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2a0f7184-3d82-40ab-967e-e34cf3fe0755"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Info"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9bacb311-4388-4ff4-85a8-ef177ff652f8"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Info"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c09241e5-6336-4704-88c1-5b1de0800f5b"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Info"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ab54ac35-3342-4ee1-b178-4abca6f69f31"",
                    ""path"": ""<Keyboard>/v"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Volume"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1a59015d-c76d-4fcc-8e65-5e712fdc5d94"",
                    ""path"": ""<Keyboard>/t"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Volume"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""927078fd-cb7b-46a8-970a-10da422214b0"",
                    ""path"": ""<Keyboard>/o"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Volume"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Volume"",
            ""id"": ""4430f721-e2c1-4a45-a2dd-4dce694bcc0b"",
            ""actions"": [
                {
                    ""name"": ""VolumeUp"",
                    ""type"": ""Button"",
                    ""id"": ""528c29e7-2396-4969-9bde-b9a57c8021dd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""VolumeDown"",
                    ""type"": ""Button"",
                    ""id"": ""b0090698-18d3-4f67-b57b-fefe9b509b01"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""VolumeExit"",
                    ""type"": ""Button"",
                    ""id"": ""8401ff4a-00e8-44ba-b6be-d736675d681e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""3ab65da4-0935-4d95-b143-4d67b95796da"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""VolumeUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a3d52e2e-7fdc-4240-bb63-0f01e84d1842"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""VolumeUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""665ab830-fa89-4ba0-98c7-fea8170b7bfd"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""VolumeDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""11d317ad-94f5-4894-8239-b804967941e4"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""VolumeDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""06239e6b-f679-4cad-bafa-1b496402c78f"",
                    ""path"": ""<Keyboard>/v"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""VolumeExit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1c321014-cbc5-410c-a4d9-35d2008d69ba"",
                    ""path"": ""<Keyboard>/t"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""VolumeExit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c8c4beb0-92ce-4168-b9cc-8ff590bb42e2"",
                    ""path"": ""<Keyboard>/o"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""VolumeExit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Arcade
        m_Arcade = asset.FindActionMap("Arcade", throwIfNotFound: true);
        m_Arcade_Enter = m_Arcade.FindAction("Enter", throwIfNotFound: true);
        m_Arcade_Left = m_Arcade.FindAction("Left", throwIfNotFound: true);
        m_Arcade_Right = m_Arcade.FindAction("Right", throwIfNotFound: true);
        m_Arcade_Down = m_Arcade.FindAction("Down", throwIfNotFound: true);
        m_Arcade_Up = m_Arcade.FindAction("Up", throwIfNotFound: true);
        m_Arcade_Info = m_Arcade.FindAction("Info", throwIfNotFound: true);
        m_Arcade_Volume = m_Arcade.FindAction("Volume", throwIfNotFound: true);
        // Volume
        m_Volume = asset.FindActionMap("Volume", throwIfNotFound: true);
        m_Volume_VolumeUp = m_Volume.FindAction("VolumeUp", throwIfNotFound: true);
        m_Volume_VolumeDown = m_Volume.FindAction("VolumeDown", throwIfNotFound: true);
        m_Volume_VolumeExit = m_Volume.FindAction("VolumeExit", throwIfNotFound: true);
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
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Arcade
    private readonly InputActionMap m_Arcade;
    private IArcadeActions m_ArcadeActionsCallbackInterface;
    private readonly InputAction m_Arcade_Enter;
    private readonly InputAction m_Arcade_Left;
    private readonly InputAction m_Arcade_Right;
    private readonly InputAction m_Arcade_Down;
    private readonly InputAction m_Arcade_Up;
    private readonly InputAction m_Arcade_Info;
    private readonly InputAction m_Arcade_Volume;
    public struct ArcadeActions
    {
        private @Controls m_Wrapper;
        public ArcadeActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Enter => m_Wrapper.m_Arcade_Enter;
        public InputAction @Left => m_Wrapper.m_Arcade_Left;
        public InputAction @Right => m_Wrapper.m_Arcade_Right;
        public InputAction @Down => m_Wrapper.m_Arcade_Down;
        public InputAction @Up => m_Wrapper.m_Arcade_Up;
        public InputAction @Info => m_Wrapper.m_Arcade_Info;
        public InputAction @Volume => m_Wrapper.m_Arcade_Volume;
        public InputActionMap Get() { return m_Wrapper.m_Arcade; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ArcadeActions set) { return set.Get(); }
        public void SetCallbacks(IArcadeActions instance)
        {
            if (m_Wrapper.m_ArcadeActionsCallbackInterface != null)
            {
                @Enter.started -= m_Wrapper.m_ArcadeActionsCallbackInterface.OnEnter;
                @Enter.performed -= m_Wrapper.m_ArcadeActionsCallbackInterface.OnEnter;
                @Enter.canceled -= m_Wrapper.m_ArcadeActionsCallbackInterface.OnEnter;
                @Left.started -= m_Wrapper.m_ArcadeActionsCallbackInterface.OnLeft;
                @Left.performed -= m_Wrapper.m_ArcadeActionsCallbackInterface.OnLeft;
                @Left.canceled -= m_Wrapper.m_ArcadeActionsCallbackInterface.OnLeft;
                @Right.started -= m_Wrapper.m_ArcadeActionsCallbackInterface.OnRight;
                @Right.performed -= m_Wrapper.m_ArcadeActionsCallbackInterface.OnRight;
                @Right.canceled -= m_Wrapper.m_ArcadeActionsCallbackInterface.OnRight;
                @Down.started -= m_Wrapper.m_ArcadeActionsCallbackInterface.OnDown;
                @Down.performed -= m_Wrapper.m_ArcadeActionsCallbackInterface.OnDown;
                @Down.canceled -= m_Wrapper.m_ArcadeActionsCallbackInterface.OnDown;
                @Up.started -= m_Wrapper.m_ArcadeActionsCallbackInterface.OnUp;
                @Up.performed -= m_Wrapper.m_ArcadeActionsCallbackInterface.OnUp;
                @Up.canceled -= m_Wrapper.m_ArcadeActionsCallbackInterface.OnUp;
                @Info.started -= m_Wrapper.m_ArcadeActionsCallbackInterface.OnInfo;
                @Info.performed -= m_Wrapper.m_ArcadeActionsCallbackInterface.OnInfo;
                @Info.canceled -= m_Wrapper.m_ArcadeActionsCallbackInterface.OnInfo;
                @Volume.started -= m_Wrapper.m_ArcadeActionsCallbackInterface.OnVolume;
                @Volume.performed -= m_Wrapper.m_ArcadeActionsCallbackInterface.OnVolume;
                @Volume.canceled -= m_Wrapper.m_ArcadeActionsCallbackInterface.OnVolume;
            }
            m_Wrapper.m_ArcadeActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Enter.started += instance.OnEnter;
                @Enter.performed += instance.OnEnter;
                @Enter.canceled += instance.OnEnter;
                @Left.started += instance.OnLeft;
                @Left.performed += instance.OnLeft;
                @Left.canceled += instance.OnLeft;
                @Right.started += instance.OnRight;
                @Right.performed += instance.OnRight;
                @Right.canceled += instance.OnRight;
                @Down.started += instance.OnDown;
                @Down.performed += instance.OnDown;
                @Down.canceled += instance.OnDown;
                @Up.started += instance.OnUp;
                @Up.performed += instance.OnUp;
                @Up.canceled += instance.OnUp;
                @Info.started += instance.OnInfo;
                @Info.performed += instance.OnInfo;
                @Info.canceled += instance.OnInfo;
                @Volume.started += instance.OnVolume;
                @Volume.performed += instance.OnVolume;
                @Volume.canceled += instance.OnVolume;
            }
        }
    }
    public ArcadeActions @Arcade => new ArcadeActions(this);

    // Volume
    private readonly InputActionMap m_Volume;
    private IVolumeActions m_VolumeActionsCallbackInterface;
    private readonly InputAction m_Volume_VolumeUp;
    private readonly InputAction m_Volume_VolumeDown;
    private readonly InputAction m_Volume_VolumeExit;
    public struct VolumeActions
    {
        private @Controls m_Wrapper;
        public VolumeActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @VolumeUp => m_Wrapper.m_Volume_VolumeUp;
        public InputAction @VolumeDown => m_Wrapper.m_Volume_VolumeDown;
        public InputAction @VolumeExit => m_Wrapper.m_Volume_VolumeExit;
        public InputActionMap Get() { return m_Wrapper.m_Volume; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(VolumeActions set) { return set.Get(); }
        public void SetCallbacks(IVolumeActions instance)
        {
            if (m_Wrapper.m_VolumeActionsCallbackInterface != null)
            {
                @VolumeUp.started -= m_Wrapper.m_VolumeActionsCallbackInterface.OnVolumeUp;
                @VolumeUp.performed -= m_Wrapper.m_VolumeActionsCallbackInterface.OnVolumeUp;
                @VolumeUp.canceled -= m_Wrapper.m_VolumeActionsCallbackInterface.OnVolumeUp;
                @VolumeDown.started -= m_Wrapper.m_VolumeActionsCallbackInterface.OnVolumeDown;
                @VolumeDown.performed -= m_Wrapper.m_VolumeActionsCallbackInterface.OnVolumeDown;
                @VolumeDown.canceled -= m_Wrapper.m_VolumeActionsCallbackInterface.OnVolumeDown;
                @VolumeExit.started -= m_Wrapper.m_VolumeActionsCallbackInterface.OnVolumeExit;
                @VolumeExit.performed -= m_Wrapper.m_VolumeActionsCallbackInterface.OnVolumeExit;
                @VolumeExit.canceled -= m_Wrapper.m_VolumeActionsCallbackInterface.OnVolumeExit;
            }
            m_Wrapper.m_VolumeActionsCallbackInterface = instance;
            if (instance != null)
            {
                @VolumeUp.started += instance.OnVolumeUp;
                @VolumeUp.performed += instance.OnVolumeUp;
                @VolumeUp.canceled += instance.OnVolumeUp;
                @VolumeDown.started += instance.OnVolumeDown;
                @VolumeDown.performed += instance.OnVolumeDown;
                @VolumeDown.canceled += instance.OnVolumeDown;
                @VolumeExit.started += instance.OnVolumeExit;
                @VolumeExit.performed += instance.OnVolumeExit;
                @VolumeExit.canceled += instance.OnVolumeExit;
            }
        }
    }
    public VolumeActions @Volume => new VolumeActions(this);
    public interface IArcadeActions
    {
        void OnEnter(InputAction.CallbackContext context);
        void OnLeft(InputAction.CallbackContext context);
        void OnRight(InputAction.CallbackContext context);
        void OnDown(InputAction.CallbackContext context);
        void OnUp(InputAction.CallbackContext context);
        void OnInfo(InputAction.CallbackContext context);
        void OnVolume(InputAction.CallbackContext context);
    }
    public interface IVolumeActions
    {
        void OnVolumeUp(InputAction.CallbackContext context);
        void OnVolumeDown(InputAction.CallbackContext context);
        void OnVolumeExit(InputAction.CallbackContext context);
    }
}
