using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ShowEnemies : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Dictionary<Enemy,RawImage> enemyIcons = new Dictionary<Enemy, RawImage>();
    public RawImage enemyIcon;
    List<Enemy> toRemove = new List<Enemy>();
    public float zoom = 10f;
    public float minZoom = 3f;
    public float maxZoom = 10f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       // zoom += Mouse.current.scroll.value.magnitude;
            
       // zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
       toRemove.Clear();
        foreach (var key in enemyIcons)
        {
            if (key.Key == null)
            {
                Destroy(key.Value.gameObject);
                toRemove.Add(key.Key);
           
            }
            else
            {

                if (IsVisible(key.Key))
                {
                    Destroy(key.Value.gameObject);
                    toRemove.Add(key.Key);
                }
            }
           
        }
        foreach (var key in toRemove)
        {
            enemyIcons.Remove(key);
        }
        transform.eulerAngles = new Vector3(0, 0, Player.cam.transform.eulerAngles.y + 180);
   
        foreach (var enemy in EnemyManager.Instance.enemies)
        {
            float magnitude = new Vector2(Player.cam.transform.position.x - enemy.transform.position.x, Player.cam.transform.position.z - enemy.transform.position.z).magnitude;
            float angle = Mathf.Atan2(Player.cam.transform.position.x - enemy.transform.position.x, Player.cam.transform.position.z - enemy.transform.position.z);
            //Debug.Log(angle*Mathf.Rad2Deg);
          
           /// if (angle*Mathf.Rad2Deg <= 45f)
           // {
             //   toRemove.Add(enemy);
            //    return;
           // }
            
            if (!enemyIcons.ContainsKey(enemy) && !IsVisible(enemy))
            {
                
                RawImage clone = Instantiate(enemyIcon);
                clone.rectTransform.SetParent(transform, false);
          
               // float angle = Mathf.Atan2(Player.cam.transform.position.x - enemy.transform.position.x, Player.cam.transform.position.z - enemy.transform.position.z);
                //  clone.rectTransform.anchoredPosition = new Vector3(Player.cam.transform.position.x, Player.cam.transform.position.z,0) * zoom - new Vector3(enemy.transform.position.x, enemy.transform.position.z, 0)* zoom;
                clone.rectTransform.anchoredPosition = new Vector3(Mathf.Sin(angle) * 500f, Mathf.Cos(angle) * 500f, 0);
                clone.color = new Color(255, 255, 255, 1f - ((magnitude * 60f) / 255f));
                enemyIcons.Add(enemy,clone);
            }
            else
            {
            
                if (enemyIcons.TryGetValue(enemy, out RawImage clone) && !IsVisible(enemy))
                {
                  //  Debug.Log("Raw: "+angle * Mathf.Rad2Deg);
                 //   Debug.Log("Camera: "+(Camera.main.transform.eulerAngles.y));
                   // Debug.Log((angle*Mathf.Rad2Deg)+(Camera.main.transform.eulerAngles.y*Mathf.Rad2Deg));
                    // float angle = Mathf.Atan2(Player.cam.transform.position.x - enemy.transform.position.x, Player.cam.transform.position.z - enemy.transform.position.z);
                    //  clone.rectTransform.anchoredPosition = new Vector3(Player.cam.transform.position.x, Player.cam.transform.position.z,0) * zoom - new Vector3(enemy.transform.position.x, enemy.transform.position.z, 0)* zoom;
                    clone.rectTransform.anchoredPosition = new Vector3(Mathf.Sin(angle) * 500f, Mathf.Cos(angle) * 500f, 0);

                    Debug.Log("Magnitude: "+magnitude);
                    //255/300
                    
                    clone.color = new Color(255, 255, 255, 1f - ((magnitude * 60f) / 255f));
                }
                    
                    
            }
            
        }
    }

    private bool IsVisible(Enemy obj)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Player.cam);

        return planes.All(plane => plane.GetDistanceToPoint(obj.transform.position) >= 0);

    }
}
