using UnityEngine;

namespace DefaultNamespace.Events
{
    public class FollowObjectChanged
    {
        private GameObject newFollowObject;
        
        public FollowObjectChanged(GameObject newFollowObject)
        {
            this.newFollowObject = newFollowObject;
        }

        public GameObject NewFollowObject
        {
            get => newFollowObject;
        }
    }
}