## 目指せ！VRの頂点を！


### InputManual
Please import into using Scene
[ViveCameraRig.prefab]

```
// need using in (.cs) Script
using HTC.UnityPlugin.Vive;

// get parameter

// Trigger Param
// return float(0~1)
var val = ViveInput.GetTriggerValue(<Enum HandRole>);

// Button Param
// return bool
ViveInput.GetPressDown( <Enum HandRole>, <Enum ControllerButton> )

```
