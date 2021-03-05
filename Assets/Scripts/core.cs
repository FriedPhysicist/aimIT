using System;
using System.Collections;
using System.Collections.Generic;
using tools_;
using UnityEngine;

public class core : MonoBehaviour
{
    public GameObject prophet;
    public GameObject final_part;

    public class prophet_circle
    { 
        void prophet_circle_movement(GameObject prophet)
        { 
        }
    }

    [Serializable]
    public class parts
    {
        public GameObject parts_parent;
        public GameObject[] parts_particles;
        public Vector3[] parts_positions;

        public Material parts_material;
        public float parent_distance;

        public void parent_maker()
        {
            Array.Resize(ref parts_particles, parts_parent.transform.childCount);
            Array.Resize(ref parts_positions, parts_parent.transform.childCount);

            for (int i = 0; i < parts_particles.Length; i++)
            {
                parts_particles[i] = parts_parent.transform.GetChild(i).gameObject;
                parts_positions[i] = parts_parent.transform.GetChild(i).position;
            }
        }

        public void basic_parts_motion(GameObject prophet)
        {
            for (int i=0; i<parts_particles.Length;i++)
            { 
                parts_particles[i].transform.LookAt(prophet.transform);
                
                float current_distance= Vector3.Distance(prophet.transform.position, parts_particles[i].transform.position); // current distance between main and particles
                parts_particles[i].transform.localScale = new Vector3(current_distance,current_distance,current_distance)/7f; // calculate particles scale
                
                if (current_distance<parent_distance) // if current distance of particle less than parent_distance, parent of particle will be main
                { 
                    parts_particles[i].transform.SetParent(prophet.transform);
                } 
                 
                if (Vector3.Distance(parts_particles[i].transform.position,parts_positions[i])>=parent_distance)
                { 
                    parts_particles[i].transform.SetParent(parts_parent.transform);
                } 
                
                if(parts_particles[i].transform.parent.IsChildOf(parts_parent.transform))
                    parts_particles[i].transform.position = Vector3.Lerp(parts_particles[i].transform.position,parts_positions[i],0.1f);
            } 
        }
    }

    public parts Parts = new parts();

    void Start()
    {
        Parts.parent_maker();
    }
    
    void Update()
    { 
        Parts.basic_parts_motion(prophet);
    }
    
}
