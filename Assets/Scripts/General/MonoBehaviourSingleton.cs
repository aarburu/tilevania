using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public abstract class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviourSingleton<T>
    {
        /// <summary>
        /// Instance del singleton
        /// </summary>
        public static T Instance;

        public bool NewInstanceSustitutesOldOne;

        protected bool isBeingDestroyed;

        protected virtual void Awake()
        {
            if (Instance != null && Instance != this)
            {
                var instanceToDestroy = NewInstanceSustitutesOldOne ? Instance : this;
                isBeingDestroyed = instanceToDestroy == this;

                Destroy(instanceToDestroy.gameObject);
            }

            if (NewInstanceSustitutesOldOne || Instance == null)
            {
                Instance = this as T;
            }
        }

        protected virtual void OnDestroy()
        {
            if (Instance == this)
                Instance = null;
        }
    }
