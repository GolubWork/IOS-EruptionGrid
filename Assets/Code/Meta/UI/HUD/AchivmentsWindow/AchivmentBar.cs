using Code.Meta.Achivments.Configs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Meta.UI.HUD.AchivmentsWindow
{
    public class AchivmentBar : MonoBehaviour
    {
        public TextMeshProUGUI Title;
        public TextMeshProUGUI Description;
        public string spritePath;
        public Image image;


        public void Init(AchivmentStatusId statusId)
        {
            image.sprite = Resources.Load<Sprite>(spritePath);
            switch (statusId)
            {
                case AchivmentStatusId.Completed:
                {
                    Opacity(false);
                    break;
                }
                case AchivmentStatusId.Locked:
                {
                    Opacity(true);
                    break;
                }
            }
        }


        private void Opacity(bool isOpacity)
        {
            if (isOpacity)
            {
                Title.color = new Color(Title.color.r, Title.color.g, Title.color.b, 0.5f);
                Description.color = new Color(Description.color.r, Description.color.g, Description.color.b, 0.5f);
                image.color = new Color(image.color.r, image.color.g, image.color.b, 0.5f);
            }
            else
            {
                Title.color = new Color(Title.color.r, Title.color.g, Title.color.b, 1f);
                Description.color = new Color(Description.color.r, Description.color.g, Description.color.b, 1f);
                image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);
            }
        }
    }
}