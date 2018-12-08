using System.Collections.Generic;

namespace Day8
{
    public class Node
    {
        public int NumberOfChildren { get; }
        public int NumberOfMetadata { get; }
        public List<Node> Children { get; set; } = new List<Node>();
        public List<int> Metadata { get; set; } = new List<int>();

        public Node(int numberOfChildren, int numberOfMetadata)
        {
            NumberOfChildren = numberOfChildren;
            NumberOfMetadata = numberOfMetadata;
        }
    }
}