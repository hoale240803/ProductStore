using System;
using System.Collections.Generic;

#nullable disable

namespace ProductStore.API.DBFirst.DataModels
{
    public partial class Media
    {
        public int Id { get; set; }
        public int? IdProduct { get; set; }
        public int? IdEmployee { get; set; }
        public int? IdInternalShipper { get; set; }
        public int? IdExternalShipper { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string FileId { get; set; }
        public string ExternalUrl { get; set; }

        public virtual Employee IdEmployeeNavigation { get; set; }
        public virtual ExternalShipper IdExternalShipperNavigation { get; set; }
        public virtual InternalShipper IdInternalShipperNavigation { get; set; }
        public virtual Product IdProductNavigation { get; set; }
    }
}
