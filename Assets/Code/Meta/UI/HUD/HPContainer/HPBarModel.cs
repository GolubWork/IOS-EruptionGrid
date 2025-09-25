using UnityEngine.UI;

namespace Code.Meta.UI.HUD.HPContainer
{
    public class HPBarModel
    {
        private Slider _healthBar;
        private Image _fill;
        
        public HPBarModel(Slider healthBar, Image fill)
        {
            _healthBar = healthBar;
            _fill = fill;
        }
        public void SetHealth(float heroHp, float maxHp)
        {
            _fill.type = Image.Type.Tiled;
            _healthBar.value = heroHp / maxHp;
        }
    }
}