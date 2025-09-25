using System;
using System.Linq;
using System.Text;
using Code.Common.Entity.ToStrings;
using Code.Common.Extensions;
using Code.Gameplay.Score;
using Code.Meta.Achivments;
using Entitas;
using UnityEngine;


    public sealed partial class MetaEntity: INamedEntity
    {
        private EntityPrinter _printer;

        public override string ToString()
        {
            if (_printer == null)
                _printer = new EntityPrinter(this);

            _printer.InvalidateCache();

            return _printer.BuildToString();
        }

        public string EntityName(IComponent[] components)
        {
            try
            {
                if (components.Length == 1)
                    return components[0].GetType().Name;

                foreach (IComponent component in components)
                {
                    switch (component.GetType().Name)
                    {
                        case nameof(CurrencyStorage):
                            return PrintCurrencyStorage();
                        
                        case nameof(ScoreStorage):
                            return PrintScoreStorage();
                        
                        case nameof(AchivmentsStorage):
                            return PrintAchivmentsStorage();                       
                        
                        case nameof(ShopStorage):
                            return PrintShopStorage();
                        
                        case nameof(SelectedSkinStorage):
                            return PrintSelectedSkinStorage();
                    }
                }
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.Message);
            }

            return components.First().GetType().Name;
        }

        private string PrintSelectedSkinStorage() => 
            new StringBuilder($"Selected Skin ")
                .ToString();        
        
        private string PrintCurrencyStorage() => 
            new StringBuilder($"Currency Storage ")
                .ToString();        
        
        private string PrintScoreStorage() => 
            new StringBuilder($"Score Storage ")
                .ToString();        
        
        private string PrintAchivmentsStorage() => 
            new StringBuilder($"Achivments Storage ")
                .ToString();        
        
        private string PrintShopStorage() => 
            new StringBuilder($"Shop Storage ")
                .ToString();
    
    
        
        public string BaseToString() => base.ToString();
    }
