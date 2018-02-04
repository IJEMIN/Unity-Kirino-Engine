using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using VGPrompter;

public class ScriptManager : MonoBehaviour {

    static readonly Color GREEN = new Color(0.573607f, 0.7647059f, 0.2698962f);
    static readonly CharacterDisplayInfo DEFAULT_CDI = new CharacterDisplayInfo("Narrator", Color.grey);

    public string script_path;
    public GameObject TheCube, Textbox;
    public float LineDelay = 2;

    Text textbox;
    TheCube cube;
    Script script;

    // Current character being displayed
    CharacterDisplayInfo cdi = DEFAULT_CDI;

    // Dictionary of characters by character tag
    Dictionary<string, CharacterDisplayInfo> CharacterDisplayInfos = new Dictionary<string, CharacterDisplayInfo>() {
        { "eu", new CharacterDisplayInfo("Eugenius", Color.green) }
    };

    KeyValuePair<string, Action> GenerateAction(Dictionary<string, Action<string>> actions, string key, string parameter) {
        return new KeyValuePair<string, Action>(key, () => actions[key](parameter));
    }

    readonly Dictionary<string, Color> Colors = new Dictionary<string, Color>() {
        { "green", GREEN },
        { "blue", Color.blue }
    };

    struct ActionParam {
        //public string Name { get; private set; }
        public string[] Parameters { get; private set; }
        public Action<string> Action { get; private set; }

        public ActionParam(string[] parameters, Action<string> action) : this() {
            //Name = name;
            Parameters = parameters;
            Action = action;
        }
    }

    void Start() {

        cube = TheCube.GetComponent<TheCube>();
        textbox = Textbox.GetComponent<Text>();

        Debug.Log(script_path);
        script = Script.FromSource(script_path);

        var conditions = new Dictionary<string, Func<bool>>() {
            { "True", () => true },
            { "False", () => false },
            { "CurrentColorNotGreen", () => cube.CurrentColor != GREEN }
        };

        var actions = new Dictionary<string, Action>() {
            { "DoNothing", () => { } }
        };

        var functions = new Dictionary<string, Delegate>() {
            { "TurnCubeColor", (Action<string>)ChangeCubeColor },
            { "SetString", (Action<Script, string, string>)SetString }
        };

        script.Conditions = conditions;
        script.Actions = actions;
        script.Functions = functions;

        script.Prime();
        script.Validate();

        StartCoroutine(script.GetCurrentEnumerator(OnLine, SelectChoice, OnChoiceSelected, OnReturn));
    }


    /* Coroutines required by the plugin */
    
    IEnumerator OnReturn() {

        // Show a last message when the script ends

        textbox.text = "The End";
        yield return new WaitForSecondsRealtime(2f);
        cube.GetComponent<Rigidbody>().useGravity = true;
        yield return new WaitForSecondsRealtime(5f);
        TheCube.SetActive(false);
    }

    IEnumerator OnChoiceSelected(Script.Menu menu, Script.Choice choice) {

        // Show the choice text after the choice has been selected

        textbox.text = choice.ToString();
        yield return new WaitForSecondsRealtime(2f);
    }

    IEnumerator<int?> SelectChoice(Script.Menu menu) {

        // Show the menu and return the choice index once selected

        int n = 0;

        textbox.text = string.Join("\n",
            menu.TrueChoices
                .Select((x, i) => string.Format("[{0}] {1}", i, x.Text))
                .ToArray());

        while (!(DigitWasPressed(ref n) && n >= 0 && n < menu.TrueCount))
            yield return null;

        yield return menu.TrueChoices[n].Index;
    }

    IEnumerator OnLine(Script.DialogueLine line) {

        // Change the display settings

        if (!string.IsNullOrEmpty(line.Tag)) {

            if (!CharacterDisplayInfos.TryGetValue(line.Tag, out cdi)) {

                // Ideally this should be part of validation (it will be at some point)
                throw new Exception("Unknown character tag!");

            }

        } else {

            cdi = DEFAULT_CDI;

        }

        // Update the UI based on the current CharacterDisplayInfo
        Debug.Log(cdi.FullName);

        // Show the dialogue line
        textbox.text = line.Text;
        yield return new WaitForSecondsRealtime(LineDelay);
    }


    /* Helpers */

    bool DigitWasPressed(ref int n) {
        if (!string.IsNullOrEmpty(Input.inputString)) {
            var c = Input.inputString[0];
            if (char.IsDigit(c)) {
                n = int.Parse(c.ToString());
                return true;
            }
        }
        return false;
    }

    void ChangeCubeColor(Color color) {
        cube.CurrentColor = color;
    }

    void ChangeCubeColor(string color_name) {
        Color color;
        ChangeCubeColor(Colors.TryGetValue(color_name, out color) ? color : Color.black);
    }

    void SetString(Script script, string key, string value) {
        script.Globals[key] = value;
    }

}