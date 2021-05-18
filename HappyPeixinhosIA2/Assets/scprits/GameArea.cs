/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 *
 * Author: Nuno Fachada
 * */

using UnityEngine;

namespace AIUnityExamples.Movement.Core
{
    // Helper class to determine game area and obtain random positions within it
    public class GameArea : MonoBehaviour
    {
        public float Xmax { get; private set; }
        public float Xmin { get; private set; }
        public float Ymax { get; private set; }
        public float Ymin { get; private set; }
        public float Zmax { get; private set; }
        public float Zmin { get; private set; }

        public Vector3 center { get; private set; }

        private BoxCollider box;

        // Create new game area limits object
        
        public void Awake(){
            box = GetComponent<BoxCollider>();
        }
        public void Start()
        {
            // Get world bounds
            Bounds bounds = box.bounds;

            // Determine and keep game area limits
            Xmax = (bounds.center.x - bounds.extents.x) * box.transform.lossyScale.x;
            Xmin = (bounds.center.x + bounds.extents.x) * box.transform.lossyScale.x;
            Ymax = (bounds.center.y - bounds.extents.y) * box.transform.lossyScale.y;
            Ymin = (bounds.center.y + bounds.extents.y) * box.transform.lossyScale.y;
            Zmax = (bounds.center.z - bounds.extents.z) * box.transform.lossyScale.z;
            Zmin = (bounds.center.z + bounds.extents.z) * box.transform.lossyScale.z;

            center = bounds.center;
        }

        // Determine random position within game area
        public Vector3 RandomPosition(float margin)
        {
            return new Vector3(
                Random.Range(Xmin * margin, Xmax * margin),
                Random.Range(Ymin * margin, Ymax * margin),
                Random.Range(Zmin * margin, Zmax * margin));
        }

        // Determine opposite position within game area
        public Vector3 OppositePosition(char wall, Vector3 currentPos)
        {
            Vector3 newPosition;
            switch (wall)
            {
                case 'E':
                    newPosition = new Vector3(Xmax * 0.9f, currentPos.y, currentPos.z);
                    break;
                case 'W':
                    newPosition = new Vector3(Xmin * 0.9f, currentPos.y, currentPos.z);
                    break;
                case 'N':
                    newPosition = new Vector3(currentPos.x, currentPos.z, Ymax * 0.9f);
                    break;
                case 'S':
                    newPosition = new Vector3(currentPos.x, currentPos.z, Ymin * 0.9f);
                    break;
                
                case 'U':
                    newPosition = new Vector3(currentPos.x, currentPos.y, Zmax * 0.9f);
                    break;

                case 'D':
                    newPosition = new Vector3(currentPos.x, currentPos.y, Zmin * 0.9f);
                    break;
                
                default:
                    newPosition = new Vector3();
                    break;
            }
            return newPosition;
        }
    }
}