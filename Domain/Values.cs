using System;

namespace Domain;

public class Values
{
    // Organisation Values
    public static readonly Guid MainOrganisationGuid = new("8A7FE96F-C979-4660-A592-B50128954216");

    // Role Values
    public static readonly string SuperAdminRole = "SuperAdmin";
    public static readonly string OrgAdminRole = new("OrgAdmin");
    public static readonly string OrgUserRole = new("OrgUser");

    public class SuperAdmin
    {
        public const string Email = "superadmin@superadmin.xyz";

        public const string Password =
            "WEohGXBQ5n6QvfM4EvnzRS4NASmuiRvZ2aZmgFaiKX9gbmdJkPKDQuq2M5mBxWzWnb4UdvY3CHjD8zut4EYJtgk2ehaDWBEMEaS3gpoZNxcndiCaiv56E7HHpiuU7sFn";
    }
}