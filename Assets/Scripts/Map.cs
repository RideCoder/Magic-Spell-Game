using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Map : MonoBehaviour
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
        foreach (var key in enemyIcons)
        {
            if (key.Key == null)
            {
                Destroy(key.Value.gameObject);
                toRemove.Add(key.Key);
           
            }
        }
        foreach (var key in toRemove)
        {
            enemyIcons.Remove(key);
        }
        transform.eulerAngles = new Vector3(0, 0, Player.cam.transform.eulerAngles.y + 180);
   
        foreach (var enemy in EnemyManager.Instance.enemies)
        {
            if (!enemyIcons.ContainsKey(enemy))
            {
                
                RawImage clone = Instantiate(enemyIcon);
                clone.rectTransform.SetParent(transform, false);
                clone.rectTransform.anchoredPosition = new Vector3(Player.cam.transform.position.x, Player.cam.transform.position.z,0) * zoom - new Vector3(enemy.transform.position.x, enemy.transform.position.z, 0)* zoom;
                
                enemyIcons.Add(enemy,clone);
            }
            else
            {
            
                if (enemyIcons.TryGetValue(enemy, out RawImage clone))
                {
                    clone.rectTransform.anchoredPosition = new Vector3(Player.cam.transform.position.x, Player.cam.transform.position.z, 0) * zoom - new Vector3(enemy.transform.position.x, enemy.transform.position.z, 0) * zoom;
                    
                
                }
                    
                    
            }
            
        }
    }
}
