using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Code.Audios.Audio;
using Code.Audios.Audio.Factory;
using Code.Windows.StaticWindows;
using TMPro;
using UnityEngine;

namespace Code.Meta.UI.HUD.PrivacyWindow
{
    public class PrivacyWindowModel
    {
        private readonly IStaticWindowService _staticWindowService;
        private readonly IAudioFactory _audioFactory;

        public PrivacyWindowModel(IStaticWindowService staticWindowService, IAudioFactory audioFactory)
        {
            _staticWindowService = staticWindowService;
            _audioFactory = audioFactory;
        }

        public void ReturnHome()
        {
            _audioFactory.CreateSound(SoundTypeId.BtnClick);
            _staticWindowService.Close(StaticWindowId.PrivacyWindow);
            _staticWindowService.Open(StaticWindowId.HomeWindow);
        }

        public void SetPrivacyText(TextMeshProUGUI container)
        {
            TextAsset jsonText = Resources.Load<TextAsset>("Privacy");
            if (jsonText == null)
            {
                Debug.LogError("Privacy.json не найден в Resources!");
                return;
            }
            string wrappedJson = "{ \"Sections\": " + jsonText.text + "}";
            PrivacyData privacyData = JsonUtility.FromJson<PrivacyData>(wrappedJson);
            if (privacyData == null || privacyData.Sections == null)
            {
                Debug.LogError("Ошибка парсинга Privacy.json");
                return;
            }

            string resultText = "";
            foreach (var section in privacyData.Sections)
            {
                resultText += $"\n<align=\"center\"><size=36><b>{section.Header}</b></size></align>\n";
                resultText += "<align=\"center\">______________</align>\n\n";
                string cleanedContent = CleanText(section.Content);
                resultText += $"<size=28>{cleanedContent}</size>\n\n";
            }

            container.SetText(resultText.Trim());
        }


        private string CleanText(string rawText)
        {
            if (string.IsNullOrEmpty(rawText))
                return "";
            string result = Regex.Replace(rawText, @"[ \t]+", " ");
            result = Regex.Replace(result, @"(\r?\n){2,}", "\n");
            return result.Trim();
        }


        [Serializable]
        public class PrivacySection
        {
            public string Header;
            public string Content;
        }

        [Serializable]
        public class PrivacyData
        {
            public List<PrivacySection> Sections;
        }
    }
}
