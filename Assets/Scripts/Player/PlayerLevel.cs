using System;
using UnityEngine;

public class PlayerLevel : MonoBehaviour
{
    public int xp;
    public int requiredXp;
    public int level = 1;
    public static event Action<int, int, int> OnXpChange;
    public static event Action OnLevelChange;
    public void Start()
    {
        requiredXp = level * 10;
        Enemy.OnDeath += AddXp;
    }

    public void AddXp(Enemy e)
    {
     
        int temp = xp;
        xp += 2;
        
        
        if (xp >= requiredXp)
        {
            OnLevelChange?.Invoke();
            level++;
            xp -= requiredXp;
            requiredXp = level * 10;
           
        }
       
        OnXpChange?.Invoke(temp, xp, requiredXp);
    }
}
