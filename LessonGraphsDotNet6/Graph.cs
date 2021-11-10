using System;
using System.Collections.Generic;



public class Graph
{
    private List<IVertex> vertices;
    private HashSet<Edge> edges;
    
    public int Order
    {
        get { return vertices.Count; }
    }
    public int Size
    {
        get { return edges.Count; }
    }
    
    public IVertex[] Vertices
    {
        get { return vertices.ToArray(); }
    }
    
    public Edge[] Edges
    {
        get
        {
            Edge[] retval = new Edge[edges.Count];
            edges.CopyTo(retval);
            return retval;
        }
    }

    public void DFS(IVertex v = null)
    {
        HashSet<IVertex> visited = new HashSet<IVertex>();
        Stack<IVertex> S = new Stack<IVertex>();

        if (v == null)
        {
            v = vertices[0];
        }
        S.Push(v);
        Console.Write("DFS: [");
        while (S.Count > 0)
        {
            IVertex current = S.Pop();
            Console.Write(current.Value + " ");
            visited.Add(current);
            foreach (Edge e in current.Edges)
            {
                if (!visited.Contains(e.Destination))
                {
                    S.Push(e.Destination);
                }
            }
        }
        Console.Write("]");
    }
    
    public void BFS(IVertex v = null)
    {
        HashSet<IVertex> visited = new HashSet<IVertex>();
        Queue<IVertex> Q = new Queue<IVertex>();

        if (v == null)
        {
            v = vertices[0];
        }
        Q.Enqueue(v);
        Console.Write("BFS: [");
        while (Q.Count > 0)
        {
            IVertex current = Q.Dequeue();
            Console.Write(current.Value + " ");
            visited.Add(current);
            foreach (Edge e in current.Edges)
            {
                if (!visited.Contains(e.Destination))
                {
                    Q.Enqueue(e.Destination);
                }
            }
        }
        Console.Write("]");
    }
    
    public void AddVertex(IVertex vertex)
    {
        vertices.Add(vertex);
    }

    public void AddEdge(IVertex v1, IVertex v2, float weight = 1f)
    {
        edges.Add(v1.AddEdge(v2, weight));
        edges.Add(v2.AddEdge(v1, weight));
    }
    
    public Graph()
    {
        vertices = new List<IVertex>();
        edges = new HashSet<Edge>();
    }
    
}
