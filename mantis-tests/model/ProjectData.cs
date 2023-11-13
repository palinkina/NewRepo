﻿using System;
//
namespace mantis_tests
{
    public class ProjectData : IEquatable<ProjectData>, IComparable<ProjectData>
    {
        public ProjectData()
        {
        }

        public ProjectData(string name)
        {
            Name = name;
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int CompareTo(ProjectData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            return Name.CompareTo(other.Name);
        }

        public bool Equals(ProjectData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(other, this))
            {
                return true;
            }
            return Name == other.Name;
        }
        public override string ToString()
        {
            return "name = " + Name;
        }
    }
}