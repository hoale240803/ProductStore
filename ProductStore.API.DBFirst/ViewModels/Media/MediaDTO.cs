using System.ComponentModel.DataAnnotations.Schema;

namespace ProductStore.API.DBFirst.ViewModels.Media
{
    public class MediaDTO
    {
        public int Id { get; set; }
        [NotMapped]
        public int? IdProduct { get; set; }
        [NotMapped]
        public int? IdEmployee { get; set; }
        [NotMapped]
        public int? IdInternalShipper { get; set; }
        [NotMapped]
        public int? IdExternalShipper { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string FileId { get; set; }
        public string ExternalUrl { get; set; }

    }
}