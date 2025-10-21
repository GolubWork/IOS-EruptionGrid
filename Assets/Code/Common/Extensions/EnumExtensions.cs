using System;
using Code.Gameplay.Obstacles.Configs;

namespace Code.Common.Extensions
{
    public static class EnumExtensions<T> where T: Enum
    {
        private static Random _random = new Random();

        public static T GetRandom()
        {
            var values = Enum.GetValues(typeof(ObstacleTypeId));
            
            int startIndex = 1;
            int randomIndex = _random.Next(startIndex, values.Length);
            
            return (T)values.GetValue(randomIndex);
        }
    }
}