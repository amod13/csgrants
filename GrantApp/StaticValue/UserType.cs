using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GrantApp.StaticValue
{
    public enum UserType
    {
        PROVINCELEVELUSER=2,
        LOCALLEVELUSER=4
    }

    public enum LocalLevelOfficeType
    {
        RURAL=1,
        MUNICIPALITIES=4,
        SUBMETROPOLITANT=2,
        METROPOLITANT=3

    }
    public enum AdmiUserType
    {
        SUPERADMIN=5,
        ADMIN=1
    }
}