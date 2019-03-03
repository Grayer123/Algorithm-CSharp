public class Solution {
    public string AlienOrder(string[] words) {
        // dfs + topological sort
        // tc:O(n); sc:O(n)
        if(words == null) {
            return String.Empty;
        }
        Dictionary<char, List<char>> graph = ConstructGraph(words);
        Dictionary<char, int> inBounds = CalculateInBounds(graph);
        
        StringBuilder str = new StringBuilder();
        foreach(char ch in graph.Keys) {
            if(!inBounds.ContainsKey(ch)) { // add all nodes with in-bound = 0 to queue
                Dfs(graph, inBounds, ch, str);
            }
        }
        return str.Length == graph.Count ? str.ToString() : String.Empty;
    }
    
    private Dictionary<char, List<char>> ConstructGraph(string[] words) {
        Dictionary<char, List<char>> graph = new Dictionary<char, List<char>>();
        foreach(var word in words) { // create nodes
            foreach(var ch in word) {
                if(!graph.ContainsKey(ch)) {
                    graph[ch] = new List<char>();
                }
            }
        }
        for(int i = 0; i < words.Length - 1; i++) {  // create edges
            int index = 0;
            while(index < words[i].Length && index < words[i + 1].Length) {
                if(words[i][index] != words[i + 1][index]) {
                    graph[words[i][index]].Add(words[i + 1][index]);
                    break;
                }
                index++;
            }
        }
        return graph;
    }
    
    private Dictionary<char, int> CalculateInBounds(Dictionary<char, List<char>> graph) {  
        Dictionary<char, int> inBounds = new Dictionary<char, int>(); // calculate in-bounds for nodes
        foreach(var key in graph.Keys) {
            foreach(var ngb in graph[key]) {
                inBounds[ngb] = inBounds.ContainsKey(ngb) ? ++inBounds[ngb] : 1;
            }
        }
        return inBounds;
    }
    
    private void Dfs(Dictionary<char, List<char>> graph, Dictionary<char, int> inBounds, char ch, StringBuilder str) {
        str.Append(ch);
        foreach(var ngb in graph[ch]) {
            inBounds[ngb]--;
            if(inBounds[ngb] == 0) {
                Dfs(graph, inBounds, ngb, str);
            }
        }
    }
}