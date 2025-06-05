using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/Target Detected")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "Target Detected", message: "[Agent] has spotted [Target]", category: "Events", id: "a65694b98ec272c60edf11aa2e4cbaa3")]
public sealed partial class TargetDetected : EventChannel<GameObject, GameObject> { }

