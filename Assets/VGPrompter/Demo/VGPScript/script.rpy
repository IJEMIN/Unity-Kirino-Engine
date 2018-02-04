define noun = "color"
define b = "Blue"
define adj = "spinning"
define sth = "cube"
define things = "[adj] [sth]s"

label start:
    if False:
        "Whatever!"

    eu "Hi there."
    eu "Let me show you the power of [things]!"
    eu "Pick a [noun], would you?"

    while True:
        menu 5000:
            "Green" if CurrentColorNotGreen:
                TurnCubeColor("green")
            "[b]":
                TurnCubeColor("blue")
            "The prohibited color!" if False:
                DoNothing
            "Not interested, thanks":
                eu "That's enough for today."
                jump another_scene
                
        "Come on, another one!"

    eu "Cool, ah?"