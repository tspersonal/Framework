using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Assets.Tools.Other.Editor
{
    /// <summary>
    /// 快捷操作
    /// </summary>
    public class ShortcutActions
    {
        /// <summary>
        /// 隐藏所选中的对象
        /// </summary>
        [MenuItem("Tools/Shortcut Actions/Set Active &=")]
        static void SetActiveTrue()
        {
            GameObject[] objs = Selection.gameObjects;
            foreach (var obj in objs)
            {
                obj.SetActive(true);
            }
        }

        /// <summary>
        /// 激活所选中的对象
        /// </summary>
        [MenuItem("Tools/Shortcut Actions/Set Active &-")]
        static void SetActiveFalse()
        {
            GameObject[] objs = Selection.gameObjects;
            foreach (var obj in objs)
            {
                obj.SetActive(false);
            }
        }

        /// <summary>
        /// 播放粒子特效
        /// </summary>
        [MenuItem("Tools/Shortcut Actions/Play Particle &p")]
        static void PlayParticle()
        {
            GameObject[] objs = Selection.gameObjects;
            List<ParticleSystem> pars = new List<ParticleSystem>();
            foreach (var obj in objs)
            {
                ParticleSystem p = obj.GetComponent<ParticleSystem>();
                if (p != null)
                {
                    pars.Add(p);
                    p.Stop();
                    p.Play(true);
                }
            }
        }
        /// <summary>
        /// 停止粒子特效
        /// </summary>
        [MenuItem("Tools/Shortcut Actions/Stop Particle &s")]
        static void StopParticle()
        {
            GameObject[] objs = Selection.gameObjects;
            List<ParticleSystem> pars = new List<ParticleSystem>();
            foreach (var obj in objs)
            {
                ParticleSystem p = obj.GetComponent<ParticleSystem>();
                if (p != null)
                {
                    pars.Add(p);
                    p.Stop();
                }
            }
        }
    }
}