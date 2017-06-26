using UnityEngine;

public class CharacterDisplayInfo {

    public string FullName { get; private set; }
    public Color TextColor { get; private set; }

    public CharacterDisplayInfo(string name, Color color) {
        FullName = name;
        TextColor = color;
    }

}
