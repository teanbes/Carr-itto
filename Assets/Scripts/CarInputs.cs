//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.6.3
//     from Assets/Scripts/CarInputs.inputactions
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

public partial class @CarInputs: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @CarInputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""CarInputs"",
    ""maps"": [
        {
            ""name"": ""Car"",
            ""id"": ""846ef144-a76b-4ff4-8ee7-169db65be658"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""16e4c60a-6e3f-414b-9aba-da4169b9a2c7"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Drift"",
                    ""type"": ""Button"",
                    ""id"": ""e9c26654-b90a-44e7-bf91-ed01373f506d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Accelerate"",
                    ""type"": ""Value"",
                    ""id"": ""520acc08-f710-4255-bf8b-01d56b23e3d6"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Steering"",
                    ""type"": ""Value"",
                    ""id"": ""2709e3e2-d3dd-4916-8850-2c386978bb9d"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""2266e31a-835a-4782-8850-fe0dd0d1b742"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""58acbcd6-505e-4b47-86b8-e8f20087073e"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""d9d52f2b-dd4e-4f8c-a012-6df6b3f41ee2"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""05ef3e96-7d56-4e48-bc3a-22bbafd214e9"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""f7c99aef-bae9-40b1-88ac-39ca79f66c23"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""ebed3f91-1cb4-43cc-afa3-e8cf9dc90e45"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cafff1fd-292e-4413-b0c1-b2dcb48ba6e2"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Drift"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e77980ba-6161-4f41-a8f4-cf3aeefbd226"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Drift"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Accelerate/Brake"",
                    ""id"": ""cb4d8968-51be-4930-b28c-21fdd963e264"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Accelerate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""bfacb560-a4da-4e83-9280-b036076105e1"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Accelerate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""fdd62310-e9da-4421-a92c-4be970d39493"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Accelerate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""GamepadAccelaration"",
                    ""id"": ""92b1e05c-4bce-4a19-b4a3-e61872d3c910"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Accelerate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""8a2994e4-4bc9-4db0-9fe9-8c318bbc5c16"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Accelerate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""583528e7-27a1-4508-827a-ac260b205e5a"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Accelerate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Leff/Right"",
                    ""id"": ""0063a47f-85ac-47a6-bae2-7f582f745539"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Steering"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""7c7c6c9f-20ce-40ec-8e58-aa9fe34c5037"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Steering"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""4897e3b1-2491-4eec-bc9d-affe99d44164"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Steering"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""GamepadSteering"",
                    ""id"": ""138091a5-819e-4d67-99e3-cc75a9e7afc3"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Steering"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""5234dd1c-680d-445d-9303-69e76d684789"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Steering"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""423f5450-d323-403c-8181-5988fb360da3"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Steering"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": []
        },
        {
            ""name"": ""Keyboard and Mouse"",
            ""bindingGroup"": ""Keyboard and Mouse"",
            ""devices"": []
        }
    ]
}");
        // Car
        m_Car = asset.FindActionMap("Car", throwIfNotFound: true);
        m_Car_Move = m_Car.FindAction("Move", throwIfNotFound: true);
        m_Car_Drift = m_Car.FindAction("Drift", throwIfNotFound: true);
        m_Car_Accelerate = m_Car.FindAction("Accelerate", throwIfNotFound: true);
        m_Car_Steering = m_Car.FindAction("Steering", throwIfNotFound: true);
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

    // Car
    private readonly InputActionMap m_Car;
    private List<ICarActions> m_CarActionsCallbackInterfaces = new List<ICarActions>();
    private readonly InputAction m_Car_Move;
    private readonly InputAction m_Car_Drift;
    private readonly InputAction m_Car_Accelerate;
    private readonly InputAction m_Car_Steering;
    public struct CarActions
    {
        private @CarInputs m_Wrapper;
        public CarActions(@CarInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Car_Move;
        public InputAction @Drift => m_Wrapper.m_Car_Drift;
        public InputAction @Accelerate => m_Wrapper.m_Car_Accelerate;
        public InputAction @Steering => m_Wrapper.m_Car_Steering;
        public InputActionMap Get() { return m_Wrapper.m_Car; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CarActions set) { return set.Get(); }
        public void AddCallbacks(ICarActions instance)
        {
            if (instance == null || m_Wrapper.m_CarActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_CarActionsCallbackInterfaces.Add(instance);
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
            @Drift.started += instance.OnDrift;
            @Drift.performed += instance.OnDrift;
            @Drift.canceled += instance.OnDrift;
            @Accelerate.started += instance.OnAccelerate;
            @Accelerate.performed += instance.OnAccelerate;
            @Accelerate.canceled += instance.OnAccelerate;
            @Steering.started += instance.OnSteering;
            @Steering.performed += instance.OnSteering;
            @Steering.canceled += instance.OnSteering;
        }

        private void UnregisterCallbacks(ICarActions instance)
        {
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @Drift.started -= instance.OnDrift;
            @Drift.performed -= instance.OnDrift;
            @Drift.canceled -= instance.OnDrift;
            @Accelerate.started -= instance.OnAccelerate;
            @Accelerate.performed -= instance.OnAccelerate;
            @Accelerate.canceled -= instance.OnAccelerate;
            @Steering.started -= instance.OnSteering;
            @Steering.performed -= instance.OnSteering;
            @Steering.canceled -= instance.OnSteering;
        }

        public void RemoveCallbacks(ICarActions instance)
        {
            if (m_Wrapper.m_CarActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ICarActions instance)
        {
            foreach (var item in m_Wrapper.m_CarActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_CarActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public CarActions @Car => new CarActions(this);
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    private int m_KeyboardandMouseSchemeIndex = -1;
    public InputControlScheme KeyboardandMouseScheme
    {
        get
        {
            if (m_KeyboardandMouseSchemeIndex == -1) m_KeyboardandMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard and Mouse");
            return asset.controlSchemes[m_KeyboardandMouseSchemeIndex];
        }
    }
    public interface ICarActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnDrift(InputAction.CallbackContext context);
        void OnAccelerate(InputAction.CallbackContext context);
        void OnSteering(InputAction.CallbackContext context);
    }
}