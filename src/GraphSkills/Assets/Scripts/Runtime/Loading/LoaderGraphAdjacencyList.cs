using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Kefir.Loading
{
    public sealed class LoaderGraphAdjacencyList : ILoaderGraphAdjacencyList
    {
        private const string NewLine = "\r\n";
        private const string Separator = ":";
        private const string SeparatorValues = " ";
        
        private readonly string _path;

        public LoaderGraphAdjacencyList(string path)
        {
            _path = path;
        }
        
        public IDictionary<int, IEnumerable<int>> Load()
        {
            var dictionary = new Dictionary<int, IEnumerable<int>>();
            var text = Resources.Load<TextAsset>(_path).text;
            var lines = text.Split(NewLine);
            
            foreach (var line in lines)
            {
                var mainParts = line.Split(Separator);
                var neighbors = mainParts[1].Split(SeparatorValues);
                dictionary.Add(int.Parse(mainParts[0]), neighbors.Select(int.Parse));
            }
            
            return dictionary;
        }
    }
}