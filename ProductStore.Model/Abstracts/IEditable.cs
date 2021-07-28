using System;
using System.ComponentModel.DataAnnotations;

namespace ProductStore.Model.Abstracts
{
    interface IEditable
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
    }
}