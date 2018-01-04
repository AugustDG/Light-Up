using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace AMPStudios
{
  public class QualityDrop : MonoBehaviour
  {

    TMP_Dropdown dropdown;

    private void Awake()
    {
      dropdown = GetComponent<TMP_Dropdown>();

      List<string> qualities = new List<string>();
      foreach (string quality in QualitySettings.names)
      {
        qualities.Add(quality);
      }

      dropdown.AddOptions(qualities);

      if (!PlayerPrefs.HasKey("QualityLevel"))
      {
        PlayerPrefs.SetInt("QualityLevel", 0);
      }
      else
      {
        dropdown.value = PlayerPrefs.GetInt("QualityLevel");
      }

      QualitySettings.SetQualityLevel(dropdown.value, true);
      PlayerPrefs.Save();
    }

    public void SetQuality()
    {
      QualitySettings.SetQualityLevel(dropdown.value, false);
      PlayerPrefs.SetInt("QualityLevel", dropdown.value);
      PlayerPrefs.Save();
    }
  }
}