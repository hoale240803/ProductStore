using System;

namespace ProductStore.Models.Abstracts
{
    public interface IEditable
    {
        public DateTime? CreatedDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        string Status { get; set; }
        public string MetaKeyword { get; set; }
        public string MetaDescription { get; set; }
    }
}