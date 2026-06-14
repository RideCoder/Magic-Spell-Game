using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Dictionary<Enemy,RawImage> enemyIcons = new Dictionary<Enemy, RawImage>();
    public RawImage enemyIcon;
    List<Enemy> toRemove = new List<Enemy>();
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

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
        transform.eulerAngles = new Vector3(0, 0, Camera.main.transform.eulerAngles.y + 180);
   
        foreach (var enemy in EnemyManager.Instance.enemies)
        {
            if (!enemyIcons.ContainsKey(enemy))
            {
                
                RawImage clone = Instantiate(enemyIcon);
                clone.rectTransform.SetParent(transform, false);
                clone.rectTransform.anchoredPosition = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.z,0) * 10f - new Vector3(enemy.transform.position.x, enemy.transform.position.z, 0)*10f;
                
                enemyIcons.Add(enemy,clone);
            }
            else
            {
            
                if (enemyIcons.TryGetValue(enemy, out RawImage clone))
                {
                    clone.rectTransform.anchoredPosition = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.z, 0) * 10f - new Vector3(enemy.transform.position.x, enemy.transform.position.z, 0) * 10f;
                }
                    
                    
            }
            
        }
    }
}
