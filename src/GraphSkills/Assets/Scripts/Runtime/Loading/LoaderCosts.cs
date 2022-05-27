using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Kefir.Loading
{
    public class LoaderCosts : ILoaderCosts
    {
        private readonly string _path;

        public LoaderCosts(string path)
        {
            _path = path;
        }
        
        public IEnumerable<int> LoadCosts()
        {
            var text = Resources.Load<TextAsset>(_path).text;
            return text.Split(' ').Select(int.Parse);
        }
    }
}