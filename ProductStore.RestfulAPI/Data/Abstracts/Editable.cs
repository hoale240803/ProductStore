using System;
using System.ComponentModel.DataAnnotations;

namespace ProductStore.RestfulAPI.Data.Abstracts
{
    /// <summary>
    /// Those fields frequently occur others class, so sombining the common properties into a file, 
    /// </summary>
    public abstract class Editable : IEditable
    {
        public DateTime? CreatedDate { get; set; }
        [MaxLength(256)]
        public string CreateBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        [MaxLength(256)]
        public string UpdatedBy { get; set; }
        [MaxLength(256)]
        public string Status { get; set; }
        [MaxLength(256)]
        public string MetaKeyword { get; set; }
        [MaxLength(256)]
        public string MetaDescription { get; set; }
    }
}