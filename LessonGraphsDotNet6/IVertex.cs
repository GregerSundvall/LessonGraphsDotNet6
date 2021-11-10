using System.Collections.Generic;
using System.Xml.Schema;


public interface IVertex
{
    public dynamic Value { get; }
    public HashSet<Edge> Edges { get; }
    public int IndexInGraph { get; }
    public Edge AddEdge(IVertex target, float weight = 1);
}
