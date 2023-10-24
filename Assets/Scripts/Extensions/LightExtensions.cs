using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;
// ReSharper disable InconsistentNaming

public static class LightExtensions {
  public static Tween DOColor(this Light2D light2D, Color to, float duration) {
    return DOVirtual.Color(light2D.color, to, duration, value => light2D.color = value);
  }

  public static Tween DOColor(this Light2D light2D, Color from, Color to, float duration) {
    return DOVirtual.Color(from, to, duration, value => light2D.color = value);
  }
}
