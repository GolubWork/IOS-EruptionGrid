using Code.Meta.Achivments.Factories;
using Entitas;

namespace Code.Meta.Achivments.Systems
{
    public class InitializeAchivmentSystem: IInitializeSystem
    {
        private readonly IAchivmentFactory _achivmentFactory;

        public InitializeAchivmentSystem(IAchivmentFactory achivmentFactory)
        {
            _achivmentFactory = achivmentFactory;
        }

        public void Initialize()
        {

        }
    }
}