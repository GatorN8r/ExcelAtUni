﻿namespace ExcelAtUni.Models
{
    public class Role
    {
        public Role()
        {

        }
        public Role(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
