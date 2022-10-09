using System.Collections;
using System.Collections.Generic;
using UnityEditor;

namespace Navigation
{
    public class Path : IEnumerable<Waypoint>
    {
        private readonly string pathDesc;
        private readonly string pathName;
        private Waypoint current;
        private Waypoint end;
        private int size;
        private Waypoint start;

        public Path(string pathName = "", string pathDesc = "No description")
        {
            this.pathDesc = pathDesc;
            this.pathName = pathName.Length == 0
                ? "Path\\" + GUID.Generate().ToString().Substring(0, 3)
                : pathName;
            current = start = end = null;
            size = 0;
        }

        
        public bool IsEnd()
        {
            return current.Equals(end);
        }
        public Waypoint Next()
        {
            current = current.GetNext();
            return current;
        }

        public Waypoint Start()
        {
            MoveToStart();
            return current;
        }

        public string GetPathName()
        {
            return pathName;
        }

        public string GetPathDesc()
        {
            return pathDesc;
        }

        public int GetSize()
        {
            return size;
        }

        private void MoveToStart()
        {
            current = start;
        }

        private void MoveToEnd()
        {
            current = end;
        }

        public void AppendWaypoint(Landmark landmark)
        {
            if (size == 0)
            {
                end = start = new Waypoint(landmark);
                size++;
            }
            else
            {
                ConnectWaypoints(landmark, end.GetNext(), end);
                end = current;
            }
        }

        private void ConnectWaypoints(Landmark landmark, Waypoint next, Waypoint prev)
        {
            current = new Waypoint(landmark);
            if (next != null) next.SetPrev(current);
            if (prev != null) prev.SetNext(current);
            size++;
        }

        public IEnumerator<Waypoint> GetEnumerator()
        {
            MoveToStart();
            while (current.GetNext() != null)
            {
                yield return current;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}