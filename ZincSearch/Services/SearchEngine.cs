using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZincSearch.Models;

namespace ZincSearch.Services
{
    public class SearchEngine
    {
        private Dictionary<string, List<WebPage>> _globalIndex = new Dictionary<string, List<WebPage>>();

        public List<WebPage> GetQueryResult(Query query)
        {
            var dict = new Dictionary<WebPage, int>();
            var webpages = _globalIndex.Where(a => query.Keywords.Contains(a.Key)).SelectMany(a => a.Value).Distinct();
            foreach (var webPage in webpages)
            {
                dict.Add(webPage, CalculatePageScores(webPage, query));
            }
            return dict.Where(a => a.Value != 0).OrderByDescending(a => a.Value).ThenBy(a => a.Key.Index).Select(a => a.Key).Take(5).ToList();
        }

        private static int CalculatePageScores(WebPage webPage, Query query)
        {
            var weight = 0;
            foreach (var item in query.KeywordsWithWeights)
            {
                weight += webPage.KeywordsWithWeights.GetValueOrDefault(item.Key) * item.Value;
            }
            return weight;
        }


        public void AddPageToIndex(WebPage webPage)
        {
            foreach (var keyword in webPage.Keywords)
            {
                if (_globalIndex.ContainsKey(keyword))
                {
                    _globalIndex[keyword].Add(webPage);
                }
                else
                {
                    _globalIndex.Add(keyword, new List<WebPage>() { webPage });
                }

            }
        }


    }
}
