using System.Collections.Generic;

namespace AspDotNetLab2.Services
{
    public class GeneralCounterService : IGeneralCounterService
    {
        private readonly Dictionary<string, int> _counters = new Dictionary<string, int>();

        public void IncrementCount(string url)
        {
            if (!_counters.ContainsKey(url))
            {
                _counters[url] = 0;
            }

            _counters[url]++;
        }

        public int GetCount(string url)
        {
            if (!_counters.ContainsKey(url))
            {
                return 0;
            }

            return _counters[url];
        }
    }
}
