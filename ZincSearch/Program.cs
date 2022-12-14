using ZincSearch;
using ZincSearch.Models;
using ZincSearch.Services;

// Prepare Input
var textFile = File.ReadLines("input.txt");

var webPages = new List<WebPage>();
var queries = new List<Query>();

foreach(var line in textFile)
{
    if (line.StartsWith("P"))
    {
        var i = webPages.Count+1;
        var webpage = new WebPage(i, line.Split(' ')[1..].ToList());
        webPages.Add(webpage);
    }
    else if (line.StartsWith("Q"))
    {
        var i = queries.Count + 1;
        var query = new Query(i, line.Split(' ')[1..].ToList());
        queries.Add(query);
    }
    else
    {
        throw new Exception("Invalid Input Format");
    }
}

var searchEngine = new SearchEngine();

// Create an index
foreach (var page in webPages)
{
    searchEngine.AddPageToIndex(page);
}


//Print Query Results
foreach(Query query in queries)
{
    var queryResult = searchEngine.GetQueryResult(query);

    Console.WriteLine($"{query.Name}: {string.Join(" ", queryResult.Select(a => a.Name))}");
}