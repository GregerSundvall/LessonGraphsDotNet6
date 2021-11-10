


public class Edge
{
    private IVertex source;
    private IVertex destination;
    private float weight;
    
    public IVertex Source
    {
        get { return source; }
    }
    public IVertex Destination
    {
        get { return destination; }
    }
    
    public Edge(IVertex source, IVertex destination, float weight = 1)
    {
        this.source = source;
        this.destination = destination;
        this.weight = weight;
    }
}
