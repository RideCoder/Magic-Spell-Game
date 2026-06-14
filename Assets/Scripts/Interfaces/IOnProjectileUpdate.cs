using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public interface IOnProjectileUpdate : IProjectileEffect
{
   public void OnProjectileUpdate(Projectile projectile);
}
