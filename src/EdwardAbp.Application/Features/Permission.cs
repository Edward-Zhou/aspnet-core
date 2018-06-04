using System;
using System.Collections.Generic;
using System.Text;

namespace EdwardAbp.Features
{
    public class Permission
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public List<Permission> Children { get; set; }
    }

    public class Cache
    {
        public void SetPermission()
        {
            
        }
    }

    
}
