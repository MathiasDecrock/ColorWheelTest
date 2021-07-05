# Colorwheel Example

This will tag a HSV/RGB colorwheel and gradient selector to your screen on F6 press 

you need to add some things to your project to make this work 



```AddMonoBehaviour(CustomMonoBehavioursNames.ColorPickerExampleScript);```

Using MonoBehaviorRepoLoader you need to add this monoscript from our project onto your object 

The colorwheel uses this script to "choose" its color 

```button.onClick.AddListener(ColorPickerExampleScript.ChooseColorButtonClick);```

idk if this is proper syntax because this is where I am stuck at currently 
adding the listener in C# the way it would look in Unity 

Unity shows OnClick ChooseColorButtonClick 

being chosen from GameObject -> ColorPickerExample Component -> ChooseColorButtonClick
