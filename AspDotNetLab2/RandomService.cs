using System;

namespace AspDotNetLab2.Services
{
    public interface IRandomService
    {
        int GetRandomNumber();
    }

    public class RandomService : IRandomService
    {
        private readonly int _randomNumber;

        public RandomService()
        {
            _randomNumber = new Random().Next();
        }

        public int GetRandomNumber()
        {
            return _randomNumber;
        }
    }
}
