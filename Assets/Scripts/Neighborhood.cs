using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Neighborhood : MonoBehaviour
{
    [Header("Set Dynamically")] public List<Boid> neighbors;
    private SphereCollider coll;


    // Start is called before the first frame update
    void Start()
    {
        neighbors = new List<Boid>();
        coll = GetComponent<SphereCollider>();
        coll.radius = Spawner.s.neighborDist / 2;
    }

    void FixedUpdate()
    {
        if (coll.radius != Spawner.s.neighborDist / 2)
            coll.radius = Spawner.s.neighborDist / 2;
    }

    void OnTriggerEnter(Collider other)
    {
        var b = other.GetComponent<Boid>();
        if (b != null && neighbors.IndexOf(b) == -1)
            neighbors.Add(b);
    }

    void OnTriggerExit(Collider other)
    {
        Boid b = other.GetComponent<Boid>();
        if (b != null && neighbors.IndexOf(b) != -1)
            neighbors.Remove(b);
    }

    public Vector3 avgPos
    {
        get
        {
            var avg = Vector3.zero;
            if (neighbors.Count == 0) return avg;
            avg = neighbors.Aggregate(avg, (current, t) => current + t.pos);
            avg /= neighbors.Count;
            return avg;
        }
    }


    public Vector3 avgVel
    {
        get
        {
            var avg = Vector3.zero;
            if (neighbors.Count == 0) return avg;
            avg = neighbors.Aggregate(avg, (current, t) => current + t.rigid.velocity);
            avg /= neighbors.Count;
            return avg;
        }
    }

    public Vector3 avgClosePos
    {
        get
        {
            var avg = Vector3.zero;
            Vector3 delta;
            var nearCount = 0;
            foreach (var t in neighbors)
            {
                delta = t.pos - transform.position;
                if (!(delta.magnitude <= Spawner.s.collDist)) continue;
                avg += t.pos;
                nearCount++;
            }

            // Если нет соседей, летящих слишком близко, вернуть Vector3.zero
            if (nearCount == 0) return avg;
            // Иначе координаты центральной точки
            avg /= nearCount;
            return avg;
        }
    }
}