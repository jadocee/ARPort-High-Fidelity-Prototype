using System;

namespace Model
{
    [Serializable]
    public class GroupMember
    {
        public GroupMember(string firstName, string lastName, string locationName)
        {
            FirstName = firstName;
            LastName = lastName;
            LocationName = locationName;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public string LocationName { get; set; }
    }
}