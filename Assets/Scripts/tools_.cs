using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace tools_
{
    public class arrayCalculationsVector
    { 
        public static void array_slicer(ref Vector3[] original_array,ref Vector3[] sliced_array,int start,int end)
        { 
            Array.Resize(ref sliced_array,end-start);
    
            for (int i = 0; i < end-start; i++)
            {
                sliced_array[i] = original_array[start + i];
            } 
        }
        
        public static Vector3[] array_slicer_wo_return(Vector3[] original_array,int start,int end)
        {
            Vector3[] sliced_array={};
            
            Array.Resize(ref sliced_array,end-start);
   
            for (int i = 0; i < end-start; i++)
            {
                sliced_array[i] = original_array[start + i];
            }

            return sliced_array;
        }
        
        public static Vector2[] array_slicer_wo_return(Vector2[] original_array,int start,int end)
        {
            Vector2[] sliced_array={};
            
            Array.Resize(ref sliced_array,end-start);

            for (int i = 0; i < end-start; i++)
            {
                sliced_array[i] = original_array[start + i];
            }

            return sliced_array;
        }
        
        public static void array_sorter(ref Transform[] array_to_sort, Transform first_wire_parent, Transform second_wire_parent, Transform pins_parent) // instrument game guide array algorithm
        {
            Array.Resize(ref array_to_sort,first_wire_parent.childCount-4+second_wire_parent.childCount+pins_parent.childCount); 

            for (int i = 0; i < second_wire_parent.childCount ; i++)
            {
                array_to_sort[first_wire_parent.childCount-4 + i * 2] = second_wire_parent.GetChild(i);
            }
            
            for (int i = 0; i < second_wire_parent.childCount ; i++)
            {
                array_to_sort[first_wire_parent.childCount - 4+ i * 2+1] = pins_parent.GetChild(i);
            }
        }
        
        public static void array_sorter_two_seg(ref Transform[] array_to_sort, Transform first_wire_parent, Transform second_wire_parent, Transform pins_parent) // instrument game guide array algorithm
        {
            Array.Resize(ref array_to_sort,first_wire_parent.childCount+second_wire_parent.childCount+pins_parent.childCount); 

            for (int i = 0; i < second_wire_parent.childCount ; i++)
            {
                array_to_sort[first_wire_parent.childCount + i * 2] = second_wire_parent.GetChild(i);
            }
            
            for (int i = 0; i < second_wire_parent.childCount ; i++)
            {
                array_to_sort[first_wire_parent.childCount + i * 2+1] = pins_parent.GetChild(i);
            }
        }
    }

    public class mouse_ray
    {
        public static GameObject ray_func_hited_object(Camera camera)
        { 
            Ray ray = camera.ScreenPointToRay(Input.mousePosition * new Vector2(1, 1));
            GameObject hitted_object=null;

            if (Physics.Raycast(ray, out RaycastHit hit, 30))
            {
                hitted_object= hit.transform.gameObject;
            }

            return hitted_object;
        }
        
        public static Vector3 ray_func_hited_hit_point(Camera camera)
        { 
            Ray ray = camera.ScreenPointToRay(Input.mousePosition * new Vector2(1, 1));
            Vector3 hitted_object= new Vector3();

            if (Physics.Raycast(ray, out RaycastHit hit, 30))
            {
                hitted_object= hit.point;
            }

            return hitted_object;
        }

        public static void mouse_touch_simulator(ref bool _bool)
        {
            if (Input.GetMouseButtonDown(0)) _bool = true;
            if (Input.GetMouseButtonUp(0)) _bool = false;
        }
        
        void screen_to_ray_pos(Camera camera, GameObject move_object, GameObject aim_object)
        { 
            Vector2 world_to_screen = camera.WorldToScreenPoint(aim_object.transform.position)-new Vector3(Screen.width, Screen.height, 0)/2;
            move_object.transform.GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp( move_object.transform.GetComponent<RectTransform>().anchoredPosition, world_to_screen, 0.1f); 
        }
    }

    public class GameObject_conf
    { 
        void lock_gameObject(Transform locked,float x_min,float x_max,float y_min,float y_max,float z_min, float z_max)
        { 
            locked.localPosition = new Vector3(Mathf.Clamp(locked.localPosition.x,x_min,x_max),Mathf.Clamp(locked.localPosition.y,y_min,y_max) ,Mathf.Clamp(locked.localPosition.z,z_min,z_max) );
        }

        public static void object_rotator(GameObject rotating_object,float clamp, float min,float max,float sensitivity)
        { 
            clamp-=Input.GetAxis("Mouse X")*sensitivity;
            clamp = Mathf.Clamp(clamp, min,max);
        
            rotating_object.transform.localEulerAngles = new Vector3(0, clamp, 0);
        } 
        
        public static void object_rotator(Transform rotating_object,ref float clamp, float min,float max,float sensitivity)
        { 
            clamp-=Input.GetAxis("Mouse X")*sensitivity;
            clamp = Mathf.Clamp(clamp, min,max);
        
            rotating_object.localEulerAngles = new Vector3(0, clamp, 0);
        }

        public static void drag_object(GameObject toDrag_object, float sensitivity)
        {
            toDrag_object.transform.position += new Vector3(Input.GetAxis("Mouse X")*sensitivity, Input.GetAxis("Mouse Y")*sensitivity, 0);
        }
    }

    public class _lineRenderer
    {
        public static void line_drawer(Transform parent_object, int segment_quantity)
        {
            for (int i = 0; i < parent_object.childCount / segment_quantity; i++)
            { 
                for (int j = 0; j < segment_quantity; j++)
                {
                    parent_object.GetChild(i * segment_quantity).GetComponent<LineRenderer>()
                        .SetPosition(j, parent_object.GetChild(i * segment_quantity + j).transform.position);
                }
            }
        }
        
        public static void triple_segment_rubber(Transform parent, int segment)
        {
            for (int i = 0; i < parent.childCount/segment; i++)
            {
                Transform first_wire=parent.GetChild(i*segment);
                Transform second_wire=parent.GetChild(i*segment+1);
                Transform third_wire=parent.GetChild(i*segment+2);
                
                if (!first_wire.GetComponent<Rigidbody>().isKinematic)
                {
                    first_wire.GetComponent<Rigidbody>().useGravity = true;
                    second_wire.transform.position = third_wire.position;
                }
            
                if (!third_wire.GetComponent<Rigidbody>().isKinematic)
                { 
                    third_wire.GetComponent<Rigidbody>().useGravity = true;
                    second_wire.transform.position = third_wire.position;
                } 
            }
        } 
    }

    public class real_time_watch
    {
        public static void time_flow(TMP_Text timer_text,ref float hour_timer_int,ref float minutes_timer_int)
        {
            if (minutes_timer_int>59)
            {
                minutes_timer_int = 0;
                hour_timer_int++;
            }

            if (hour_timer_int>23)
            {
                hour_timer_int = 0;
            }
            
            if (hour_timer_int < 10 && minutes_timer_int<10)  timer_text.text = "0" + (int)hour_timer_int + ":" +"0"+ (int)minutes_timer_int; 
            if (hour_timer_int < 10 && minutes_timer_int>10)  timer_text.text = "0" + (int)hour_timer_int + ":" +     (int)minutes_timer_int; 
            if (hour_timer_int >= 10 && minutes_timer_int<10) timer_text.text =       (int)hour_timer_int + ":" +"0"+ (int)minutes_timer_int;
            if (hour_timer_int >= 10 && minutes_timer_int>10) timer_text.text =       (int)hour_timer_int + ":" +     (int)minutes_timer_int;

            minutes_timer_int += Time.deltaTime;
        }
    }

} 