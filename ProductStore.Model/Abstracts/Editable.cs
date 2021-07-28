using System;
using System.ComponentModel.DataAnnotations;

namespace ProductStore.Model.Abstracts
{
    /// <summary>
    /// Those fields frequently occur others class, so sombining the common properties into a file, 
    /// </summary>
    public abstract class Editable : IEditable
    {
        public DateTime? createdDate { get; set; }
        [MaxLength(256)]
        public string createBy { get; set; }
        public DateTime? updatedDate { get; set; }
        [MaxLength(256)]
        public string updatedBy { get; set; }
        string status { get; set; }
        [MaxLength(256)]
        public string MetaKeyword { get; set; }
        [MaxLength(256)]
        public string MetaDescription { get; set; }
        string IEditable.status { get ; set ; }
    }
}