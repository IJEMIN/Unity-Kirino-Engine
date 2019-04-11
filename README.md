# Unity Kirino Engine

Kirino Engine is framework for developing Visual Novel features in Unity3D.

> Kirino Engine offer visual novel funtions to execute, but not offer how to call functions by external script.

> That means you can manually execute functions in realtime, but you have to make your own command stack or parser to implement "delayed call"

What core module do...

- display background
- display and swapping charaters sprites
- print dialogues
- play voice and music
- image transition effects
- etc...

For example, you can display texts by use this code (after setup some gameobjects)

```
VNController.textDisplayer.SetSay("Kiriring", "Hi my name is Kirino");
```

Or like this.

```
// some your own C# script

public TextDisplayer textDisplayer; // set this on inspector window

void Start() {
    textDisplayer.SetSay("Kiriring", "Hi my name is Kirino");
}
```

..and what other module can do...

- Provice variouse command types based on VNCommand
- Stacking VNCommand instance

Originally, I made this framework for [my game developed with Unity](https://play.google.com/store/apps/details?id=com.applemint.deregirl&hl=ko). I thought it might be helpful to visual novel developers if I share visual novel parts from the game's source code.

## Disclaimer
> Currenlty it's not fully functional. It's experimental solution!

## Any Question?
- i_jemin@icloud.com