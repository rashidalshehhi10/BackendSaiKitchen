using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class FineRemoteDesignAuth
    {
        public string Id { get; set; }
        public string Path { get; set; }
        public bool PathType { get; set; }
        public int RoleType { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
    }
}
