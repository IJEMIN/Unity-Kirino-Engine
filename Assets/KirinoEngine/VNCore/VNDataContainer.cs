using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace KirinoEngine
{
    namespace VNCore
    {
        // VN Component provide own static access
        public class VNDataContainer : MonoBehaviour
        {

            public static VNDataContainer Instance
            {
                get
                {
                    if (!m_instance)
                    {
                        m_instance = FindObjectOfType<VNDataContainer>();

                        if (!m_instance)
                        {
                            Debug.LogWarning("There is no VNDataContainer instance");
                        }
                    }
                    return m_instance;
                }
            }

            private static VNDataContainer m_instance;

            public List<Displayable> displayables;

            void Awake()
            {
                if (m_instance != null && m_instance != this)
                {
                    Debug.LogWarning("There are more than one VNDataContainer instance");
                    Destroy(gameObject);
                }

            }

            public Displayable GetDisplayable(string key)
            {
                foreach (Displayable displayable in displayables)
                {
                    if (displayable.key == key)
                    {
                        return displayable;
                    }
                }

                Debug.LogWarning(string.Format("There is no {0} displayble", key));
                return null;
            }

        }
    }
}
