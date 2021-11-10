using System;
using System.Collections.Generic;
using System.Linq;


internal class Program
{
    public static void Main(string[] args)
    {
        Graph graph = new Graph();

        Vertex<int>[] vertices = new Vertex<int>[]
        {
            new Vertex<int>(1, graph),
            new Vertex<int>(2, graph),
            new Vertex<int>(3, graph),
            new Vertex<int>(4, graph),
            new Vertex<int>(5, graph),
            new Vertex<int>(6, graph)
        };
        
        graph.AddEdge(vertices[0], vertices[1], 4f);
        graph.AddEdge(vertices[0], vertices[2], 4f);
        graph.AddEdge(vertices[1], vertices[2], 2f);
        graph.AddEdge(vertices[2], vertices[3], 3f);
        graph.AddEdge(vertices[2], vertices[5], 6f);
        graph.AddEdge(vertices[2], vertices[4], 1f);
        graph.AddEdge(vertices[3], vertices[5], 2f);
        graph.AddEdge(vertices[4], vertices[5], 3f);

        Console.WriteLine("Order: {0}", graph.Order);
        Console.WriteLine("Size: {0}", graph.Size);
        
        graph.DFS();
        graph.BFS();

        float[][] FWdistances = FloydWarshall(graph);
        foreach (var row in FWdistances)
        {
            Console.Write("[ ");
            foreach (var dist in row)
            {
                if (dist == float.MaxValue)
                {
                    Console.Write("- ");
                    continue;
                }
                Console.Write(dist + " ");

            }
            Console.WriteLine("] ");
        }
        Console.WriteLine();

        Dictionary<IVertex, float> DJDistances;
        Dictionary<IVertex, IVertex> DJPrevious;

        (DJDistances, DJPrevious) = Dijkstra(graph, vertices[5]);
        Console.Write("[ ");
        foreach (var dist in DJDistances)
        {
            Console.Write(dist.Value + " ");
        }
        Console.WriteLine("]");
        
        IVertex current = vertices[1];
        Console.Write("[ ");
        while (DJPrevious[current] != null)
        {
            Console.Write(current.IndexInGraph + " ");
            current = DJPrevious[current];
            
        }
        Console.Write(current.IndexInGraph + " ");
        Console.WriteLine("]");
    }

    private static (Dictionary<IVertex, float>, Dictionary<IVertex, IVertex>) Dijkstra(Graph G, IVertex initial)
    {
        HashSet<IVertex> unvisited = new HashSet<IVertex>(G.Vertices);
        Dictionary<IVertex, float> distances = new Dictionary<IVertex, float>();
        Dictionary<IVertex, IVertex> previous = new Dictionary<IVertex, IVertex>();

        foreach (var v in G.Vertices)
        {
            distances[v] = float.MaxValue;
            previous[v] = null;
        }
        distances[initial] = 0;

        while (unvisited.Count > 0)
        {
            IVertex current = null;
            float minDistance = float.MaxValue;
            foreach (var v in distances)
            {
                if (unvisited.Contains(v.Key) && v.Value < minDistance)
                {
                    minDistance = v.Value;
                    current = v.Key;
                }
            }

            if (minDistance == float.MaxValue)
            {
                break;
            }

            unvisited.Remove(current);

            float dist = distances[current];

            foreach (var e in current.Edges)
            {
                float temp = dist + e.Weight;
                if (temp < distances[e.Destination])
                {
                    distances[e.Destination] = temp;
                    previous[e.Destination] = current;
                }
            }
        }
        
        return distances;
    }

    private static float[][] FloydWarshall(Graph G)
    {
        float[][] distances = new float[G.Order][];
        for (int d = 0; d < G.Order; d++)
        {
            distances[d] = (float[]) Enumerable.Repeat(float.MaxValue, G.Order);
            distances[d][d] = 0f;
        }

        IVertex[] vertices = G.Vertices;
        for (int v = 0; v < G.Order; v++)
        {
            foreach (var e in vertices[v].Edges)
            {
                distances[v][e.Destination.IndexInGraph] = e.Weight;
            }
        }

        for (int k = 0; k < G.Order; k++)
        {
            for (int i = 0; i < G.Order; i++)
            {
                for (int j = 0; j < G.Order; j++)
                {
                    if (distances[i][j] > distances[i][k] + distances[k][j])
                    {
                        distances[i][j] = distances[i][k] + distances[k][j];
                    }
                }
            }
        }

        return distances;
    }

}
