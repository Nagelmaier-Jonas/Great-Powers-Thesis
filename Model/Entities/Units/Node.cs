using Model.Entities.Regions;

public class Node{
    public ARegion Region{ get; set; }
    public int Distance{ get; set; }

    public Node(ARegion region, int distance){
        Region = region;
        Distance = distance;
    }

    public override bool Equals(object? obj) => obj is Node node && node.Region.Id == Region.Id;
}