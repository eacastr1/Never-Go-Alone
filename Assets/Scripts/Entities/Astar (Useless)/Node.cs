using UnityEngine;
public class Node : MonoBehaviour
{
    [Header("A* Pathfinding Data")]
    public bool Walkable = true;

    public int GCost, HCost;

    public int FCost => GCost + HCost;

    [Header("Linked Nodes (manual)")]
    public Node prev;
    public Node next;

    public static int GetDistance(Node a, Node b)
    {
        return Mathf.RoundToInt(Vector2.Distance(a.transform.position, b.transform.position) * 10);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Walkable ? Color.green : Color.red;

        // Draw a sphere at the node
        Gizmos.DrawSphere(transform.position, 0.1f);

        Gizmos.color = Color.green;
        if (next != null)
            Gizmos.DrawLine(transform.position, next.transform.position);
    }
}