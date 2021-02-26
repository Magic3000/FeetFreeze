using MelonLoader;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.XR;
using VRC;

namespace FeetFreeze
{
    public static class BuildInfo
    {
        public const string Name = "FeetFreeze"; // Name of the Mod.  (MUST BE SET)
        public const string Author = "Magic3000"; // Author of the Mod.  (Set as null if none)
        public const string Company = null; // Company that made the Mod.  (Set as null if none)
        public const string Version = "1.0.0"; // Version of the Mod.  (MUST BE SET)
        public const string DownloadLink = null; // Download Link for the Mod.  (Set as null if none)
    }
    public class FeetFreezeMain : MelonMod
    {
        public override void OnApplicationStart() => MelonCoroutines.Start(InitButton());

        public static IEnumerator InitButton()
        {
            _sout("Starting FeetFreeze...", ConsoleColor.Yellow);
            while (quickMenu == null)
                yield return null;
            ToggleFeetFreeze = InstantiateGameobject("nameplates");
            CreateToggle(ToggleFeetFreeze, "ToggleFeetFreeze", "Feet Freeze on", "Feet Freeze off", "Toggle feet-freeze for VR.", 55, new Color(1f, 1f, 1f), new Color(0.5f, 0.5f, 0.5f), 4, -1, quickMenu.transform.Find("CameraMenu").gameObject.transform, new Action(() =>
            {
                toggleFeetFreeze = !toggleFeetFreeze;
                _sout("Feete-Freeze " + (toggleFeetFreeze ? "enabled" : "disabled"), ConsoleColor.Cyan);
                FeetFreezeToggle();
            }));
            SetToggle(ToggleFeetFreeze, false);
            _sout("FeetFreeze loaded!", ConsoleColor.Cyan);
            ToggleFeetFreeze.GetComponent<Button>().interactable = !string.IsNullOrEmpty(XRDevice.model);
        }

        private static void FeetFreezeToggle()
        {
            var isc = InputStateControllerManager.prop_InputStateControllerManager_0;
            if (isc == null)
                return;
            if (toggleFeetFreeze)
                InputStateControllerManager.prop_InputStateControllerManager_0.Method_Public_InputStateController_Type_0(UnhollowerRuntimeLib.Il2CppType.Of<MonoBehaviour1PublicObUnique>());   //UIInputController
            else
                InputStateControllerManager.prop_InputStateControllerManager_0.Method_Public_InputStateController_0();
            SetToggle(ToggleFeetFreeze, toggleFeetFreeze);
        }

        /*private static void PushInputController(InputStateControllerManager isc)
        {
            if (isc != null)
                isc.enabled = false;
            MonoBehaviour1PublicObUnique inputStateController = isc.gameObject.AddComponent<MonoBehaviour1PublicObUnique>();
            isc.field_Private_List_1_InputStateController_0.Add(inputStateController);
            inputStateController.Method_Public_Virtual_New_Void_0();                //OnActivate
        }

        private static InputStateController PopInputController(InputStateControllerManager isc)
        {
            InputStateController inputStateController = null;
            if (isc.field_Private_List_1_InputStateController_0.Count > 1)          //mInputStateControllerStack
            {
                inputStateController = PeekInputController(isc);
                isc.field_Private_List_1_InputStateController_0.RemoveAt(isc.field_Private_List_1_InputStateController_0.Count - 1);    //mInputStateControllerStack
                UnityEngine.Object.Destroy(inputStateController);
                PeekInputController(isc).enabled = true;
                PeekInputController(isc).Method_Public_Virtual_New_Void_0();        //OnActivate
            }
            return inputStateController;
        }

        private static InputStateController PeekInputController(InputStateControllerManager isc)
        {
            InputStateController result = null;
            if (isc.field_Private_List_1_InputStateController_0.Count > 0)          //mInputStateControllerStack
            {
                result = isc.field_Private_List_1_InputStateController_0[isc.field_Private_List_1_InputStateController_0.Count - 1];    //mInputStateControllerStack
            }
            return result;
        }*/

        private static Transform ToggleFeetFreeze;
        private static bool toggleFeetFreeze;

        public static void SetToggle(Transform button, bool toggleOn, bool disabled = false)
        {
            //button.GetComponentInChildren<UiToggleButton>().Method_Public_Void_Boolean_Boolean_2(state); obfuscated
            var uiToggleButton = button.GetComponentInChildren<UiToggleButton>();
            uiToggleButton.field_Public_Boolean_0 = toggleOn;
            var disabledButton = uiToggleButton.field_Public_GameObject_2;          //disabledButton
            var toggledOnButton = uiToggleButton.field_Public_GameObject_0;         //toggledOnButton
            var toggledOffButton = uiToggleButton.field_Public_GameObject_1;        //toggledOffButton
            if (disabled)
                toggleOn = false;
            if (disabledButton != null)
            {
                disabledButton.SetActive(disabled);
                Transform disabledButtonTransform = uiToggleButton.gameObject.transform.Find("DISABLED");
                if (disabledButtonTransform != null)
                    uiToggleButton.field_Public_GameObject_2 = disabledButtonTransform.gameObject;
            }
            if (toggledOnButton != null)
            {
                toggledOnButton.SetActive(toggleOn);
                Transform toggledOnButtonTransform = uiToggleButton.gameObject.transform.Find("ON");
                if (toggledOnButtonTransform != null)
                    uiToggleButton.field_Public_GameObject_0 = toggledOnButtonTransform.gameObject;
            }
            if (toggledOffButton != null)
            {
                toggledOffButton.SetActive(!toggleOn);
                Transform toggledOffButtonTransform = uiToggleButton.gameObject.transform.Find("OFF");
                if (toggledOffButtonTransform != null)
                    uiToggleButton.field_Public_GameObject_1 = toggledOffButtonTransform.gameObject;
            }
        }

        public static void _sout(object _in, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(_in.ToString());
            Console.ForegroundColor = ConsoleColor.White;
        }

        internal static Transform InstantiateGameobject(string type, string go = "")
        {
            if (type == "back")
                return UnityEngine.Object.Instantiate<GameObject>(quickMenu.transform.Find("CameraMenu/BackButton").gameObject).transform;
            if (type == "nameplates")
                return UnityEngine.Object.Instantiate<GameObject>(quickMenu.transform.Find("UIElementsMenu/ToggleHUDButton").gameObject).transform;
            if (type == "block")
                return UnityEngine.Object.Instantiate<GameObject>(quickMenu.transform.Find("NotificationInteractMenu/BlockButton").gameObject).transform;
            if (type == "next")
                return UnityEngine.Object.Instantiate<GameObject>(quickMenu.transform.Find("QuickMenu_NewElements/_CONTEXT/QM_Context_User_Selected/NextArrow_Button").gameObject).transform;
            if (type == "prev")
                return UnityEngine.Object.Instantiate<GameObject>(quickMenu.transform.Find("QuickMenu_NewElements/_CONTEXT/QM_Context_User_Selected/PreviousArrow_Button").gameObject).transform;
            if (type == "emojimenu")
                return UnityEngine.Object.Instantiate<GameObject>(quickMenu.transform.Find("EmojiMenu").gameObject).transform;
            if (type == "EarlyAccessText")
                return UnityEngine.Object.Instantiate<GameObject>(quickMenu.transform.Find("ShortcutMenu/EarlyAccessText").gameObject).transform;
            if (!string.IsNullOrEmpty(go))
                _sout("InstantiateGameobject " + type + " for " + go + " is null", ConsoleColor.Red);
            throw new ArgumentOutOfRangeException(type);
        }

        public static void CreateToggle(Transform transform, string name, string text1, string text2, string tooltip, int fontSize, Color color1, Color color2, float x_pos, float y_pos, Transform parent, UnityAction listener)
        {
            var NameplatesOnButton = quickMenu.transform.Find("UIElementsMenu/NameplatesOnButton");
            if (NameplatesOnButton == null)
            {
                _sout("UIElementsMenu/NameplatesOnButton is null in CreateToggle", ConsoleColor.Red);
                return;
            }
            var NameplatesIconsButton = quickMenu.transform.Find("UIElementsMenu/NameplatesIconsButton");
            if (NameplatesIconsButton == null)
            {
                _sout("UIElementsMenu/NameplatesIconsButton is null in CreateToggle", ConsoleColor.Red);
                return;
            }
            var x_button = NameplatesIconsButton.localPosition.x - NameplatesOnButton.localPosition.x;
            var y_button = NameplatesIconsButton.localPosition.x - NameplatesOnButton.localPosition.x;
            transform.Find("Toggle_States_HUDEnabled/ON/Text_HudOn").GetComponent<Text>().text =
                transform.Find("Toggle_States_HUDEnabled/OFF/Text_HudOn").GetComponent<Text>().text = text1;
            transform.Find("Toggle_States_HUDEnabled/ON/Text_HudOff").GetComponent<Text>().text =
                transform.Find("Toggle_States_HUDEnabled/OFF/Text_HudOff").GetComponent<Text>().text = text2;
            transform.Find("Toggle_States_HUDEnabled/ON/Text_HudOn").GetComponent<Text>().fontSize =
                transform.Find("Toggle_States_HUDEnabled/OFF/Text_HudOn").GetComponent<Text>().fontSize =
                transform.Find("Toggle_States_HUDEnabled/ON/Text_HudOff").GetComponent<Text>().fontSize =
                transform.Find("Toggle_States_HUDEnabled/OFF/Text_HudOff").GetComponent<Text>().fontSize = 50;
            transform.Find("Toggle_States_HUDEnabled/ON/Text_HudOn").GetComponent<RectTransform>().sizeDelta =
                transform.Find("Toggle_States_HUDEnabled/OFF/Text_HudOn").GetComponent<RectTransform>().sizeDelta =
                transform.Find("Toggle_States_HUDEnabled/ON/Text_HudOff").GetComponent<RectTransform>().sizeDelta =
                transform.Find("Toggle_States_HUDEnabled/OFF/Text_HudOff").GetComponent<RectTransform>().sizeDelta = new Vector2(transform.Find("Toggle_States_HUDEnabled/ON/Text_HudOn").GetComponent<RectTransform>().sizeDelta.x, 1000f);
            transform.GetComponentsInChildren<UiTooltip>()[0].field_Public_String_0 = transform.GetComponentsInChildren<UiTooltip>()[0].field_Public_String_1 = tooltip;
            transform.GetComponentInChildren<Text>().fontSize = 50;
            transform.Find("Toggle_States_HUDEnabled/ON/Text_HudOn").GetComponent<Text>().color =
                transform.Find("Toggle_States_HUDEnabled/OFF/Text_HudOff").GetComponent<Text>().color = color1;
            transform.Find("Toggle_States_HUDEnabled/ON/Text_HudOff").GetComponent<Text>().color =
                transform.Find("Toggle_States_HUDEnabled/OFF/Text_HudOn").GetComponent<Text>().color = color2;
            if (x_pos == 0 && y_pos == 0)
            {
                transform.localPosition = transform.localPosition;
            }
            else
            {
                transform.localPosition = new Vector3(transform.localPosition.x + x_button * x_pos, transform.localPosition.y + y_button * y_pos, transform.localPosition.z);
            }
            transform.SetParent(parent, false);
            transform.GetComponent<Button>().onClick = new Button.ButtonClickedEvent();
            transform.GetComponent<Button>().onClick.AddListener(listener);
            transform.gameObject.name = name;
        }

        internal static QuickMenu quickMenu
        {
            get
            {
                return QuickMenu.prop_QuickMenu_0;
            }
        }
    }
}
