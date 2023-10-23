using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public static class PlayerPrefManager {
  private const string CheckpointNameKey = "checkpoint";

  public static CheckpointType GetSavedCheckpoint(Dictionary<CheckpointType, ICheckpoint> checkpoints) {
    // TODO: Hardcoded for now, only one level
    return CheckpointType.SpaceshipCrash;

    string name = PlayerPrefs.GetString(CheckpointNameKey);
    if (name == "") {
      Debug.Log("Loading Spaceship");
      return CheckpointType.SpaceshipCrash;
    }

    CheckpointType target = checkpoints.First(kvp => kvp.Value.GetName() == name).Key;
    return target;
  }

  public static void SaveCheckpoint(string name) {
    Debug.Log("Saving " + name);
    PlayerPrefs.SetString(CheckpointNameKey, name);
  }
}
