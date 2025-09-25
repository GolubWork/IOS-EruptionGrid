using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Code.Audios.Audio;
using Code.Audios.Audio.Factory;
using Code.Common.Helpers;
using Code.Windows.StaticWindows;
using TMPro;

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

        public void SetPrivacyText(TextMeshProUGUI container, string privacyText)
        {
            container.text = StringUpdater.UpdateString(MarkedText(privacyText));
        }


        private string MarkedText(string rawPrivacyText)
        {
            var headers = new[]
            {
                "Privacy Policy",
                "Definitions",
                "Information Collection and Use",
                "Use of Data",
                "Transfer Of Data",
                "Disclosure Of Data",
                "Security of Data",
                "Service Providers",
                "Links to Other Sites",
                "Children's Privacy",
                "Changes to This Privacy Policy"
            };

            string result = rawPrivacyText;
            bool isFirstHeader = true;

            foreach (var header in headers)
            {
                int index = result.IndexOf(header, StringComparison.OrdinalIgnoreCase);
                if (index >= 0)
                {
                    int startOfRemainingText = index + header.Length;

                    while (startOfRemainingText < result.Length &&
                           (result[startOfRemainingText] == ' ' || result[startOfRemainingText] == '\n' || result[startOfRemainingText] == '\r'))
                    {
                        startOfRemainingText++;
                    }

                    string topSpacing = isFirstHeader ? "" : "\n\n\n";

                    result = result.Substring(0, index) +
                             $"{topSpacing}<size=160%><b>{header}</b></size>\n" +
                             result.Substring(startOfRemainingText);
                    isFirstHeader = false;
                }
            }

            return result;
        }


    }
}