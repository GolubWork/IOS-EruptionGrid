using Code.Audios.Audio.Factory;
using Code.Gameplay.StaticData.SkinStaticData;
using Code.Meta.Shop.Factories;
using Code.Meta.UI.HUD.ShopWindow.Providers;
using Code.Windows.StaticWindows;
using Code.Windows.UpdatableWindows;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Meta.UI.HUD.ShopWindow.SkinShop
{
    public class SkinShopWindowController : StaticWindow
    {
        [SerializeField] private Button btnReturn;
        [SerializeField] private Button btnLeft;
        [SerializeField] private Button btnRight;
        [SerializeField] private RectTransform itemContainer;
        [SerializeField] private SkinItemBar skinItemPrefab;
        
        private SkinShopWindowModel _model;
        private IStaticWindowService _staticWindowService;
        private IUpdatableWindowService _updatableWindowService;
        private IAudioFactory _audioFactory;
        private IShopFactory _shopFactory;
        private ISkinDataProvider _skinDataProvider;
        private IShopProvider<SkinShopWindowController> _shopProvider;
        private SkinItemBar itemBar;
        
        [Inject]
        private void Construct(
            IStaticWindowService staticWindowService,
            IUpdatableWindowService updatableWindowService,
            IAudioFactory audioFactory,
            IShopFactory shopFactory,
            ISkinDataProvider skinDataProvider,
            IShopProvider<SkinShopWindowController> shopDataProvider)
        {
            Id = StaticWindowId.ShopWindow;
            _staticWindowService = staticWindowService;
            _updatableWindowService = updatableWindowService;
            _audioFactory = audioFactory;
            _shopFactory = shopFactory;
            _skinDataProvider = skinDataProvider;
            _shopProvider = shopDataProvider;
        }
        
        protected override void Initialize()
        {
            _shopProvider.SetController(this);
            
            itemBar = Instantiate(skinItemPrefab, itemContainer);
            itemBar.Init(_shopFactory, _skinDataProvider);
            _model = new SkinShopWindowModel(_staticWindowService, _updatableWindowService, _audioFactory, itemBar);
            _model.InitShop();
            
            (bool isFirst, bool isLast) = _model.IsFirstOrLast();
            btnRight.gameObject.SetActive(!isLast);
            btnLeft.gameObject.SetActive(!isFirst);
            
            _updatableWindowService.Open(UpdatableWindowId.TotalCurrencyWindow);

        }

        public void UpdateBar()
        {
            itemBar.UpdateBar();
        }
        protected override void SubscribeUpdates()
        {
            btnReturn.onClick.AddListener(_model.ReturnHome);
            btnLeft.onClick.AddListener(OnLeft);
            btnRight.onClick.AddListener(OnRight);
        }

        protected override void UnsubscribeUpdates()
        {
            btnReturn.onClick.RemoveListener(_model.ReturnHome);
            btnLeft.onClick.RemoveListener(OnLeft);
            btnRight.onClick.RemoveListener(OnRight);
        }

        private void OnLeft()
        {
            _model.OnButtonleft(out (bool last, bool first) isFirstOrLast);
            btnRight.gameObject.SetActive(!isFirstOrLast.first);
            btnLeft.gameObject.SetActive(!isFirstOrLast.last);
        }

        private void OnRight()
        {
            _model.OnButtonRight(out (bool last, bool first) isFirstOrLast);
            btnRight.gameObject.SetActive(!isFirstOrLast.first);
            btnLeft.gameObject.SetActive(!isFirstOrLast.last);
        }
    }
}