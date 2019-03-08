public class Solution {
    public IList<int[]> PacificAtlantic(int[,] matrix) {
        // bfs 
        // tc:O(mn); sc:O(mn)
        if(matrix.GetLength(0) == 0) {
            return new List<int[]>();
        }
        int m = matrix.GetLength(0), n = matrix.GetLength(1);
        List<int[]> res = new List<int[]>();
        
        bool[,] aVisited = new bool[m, n]; // for atlantic
        bool[,] pVisited = new bool[m, n]; // for pacific
        
        for(int i = 0; i < m; i++) { // vertical
            Dfs(matrix, aVisited, i, n - 1); // atlantic
            Dfs(matrix, pVisited, i, 0); // pacific
        }
        for(int j = 0; j < n; j++) { // horizontal
            Dfs(matrix, aVisited, m - 1, j); // atlantic 
            Dfs(matrix, pVisited, 0, j); // pacific
        }
        
        for(int i = 0; i < m; i++) { // last scan, if both true in two visited[] => true
            for(int j = 0; j < n; j++) {
                if(aVisited[i, j] && pVisited[i, j]) {
                    res.Add(new int[]{i, j});
                }
            }
        }
        return res;
    }
    
    private void Dfs(int[,] matrix, bool[,] visited, int x, int y) {
        if(visited[x, y]) {
            return;
        }
        visited[x, y] = true;
        int[] dx = {1, -1, 0, 0};
        int[] dy = {0, 0, 1, -1};
        for(int i = 0; i < 4; i++) {
            int newX = x + dx[i];
            int newY = y + dy[i];
            if(IsValid(matrix, newX, newY) && !visited[newX, newY] && matrix[x, y] <= matrix[newX, newY]) {
                Dfs(matrix, visited, newX, newY);
            }
        }
    }
    
    private bool IsValid(int[,] matrix, int x, int y) {
        return x >=0 && x < matrix.GetLength(0) && y >= 0 && y < matrix.GetLength(1);
    }
}