using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HazardIdentifySystemApi.Models.Routing.Request
{
    public class AddOrUpdateCSignRoutingForScrap
    {
        public string empNo { set; get; }
        public string stationName { set; get; }
        public string factoryZone { set; get; }
        public bool isAddNew { set; get; }
    }
    public class AddOrUpdateCSignRoutingForIssuance: AddOrUpdateCSignRoutingForScrap
    {

    }

    public class AddOrUpdateCSignRoutingForReturnWH: AddOrUpdateCSignRoutingForScrap
    {

    }
    public class AddOrUpdateCSignRoutingForCScrap: AddOrUpdateCSignRoutingForScrap
    {

    }
}