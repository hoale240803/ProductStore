using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductStore.RestfulAPI.Data.Abstracts
{
    public interface IEntityBase<TKey>
    {
        TKey Id { get; set; }
    }

    public interface IDeleteEntity
    {
        bool IsDeleted { get; set; }
    }

    public interface IDeleteEntity<TKey> : IDeleteEntity, IEntityBase<TKey>
    {
    }

    public interface IAuditEntity
    {
        DateTime CreatedDate { get; set; }
        string CreatedBy { get; set; }
        DateTime? UpdatedDate { get; set; }
        string UpdatedBy { get; set; }
    }

    public interface IAuditEntity<TKey> : IAuditEntity, IDeleteEntity<TKey>
    {
    }
    /// <summary>
    /// EVERY TABLE MUST HAVE PRIMARY KEY
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class EntityBase<TKey> : IEntityBase<TKey>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual TKey Id { get; set; }
    }
    /// <summary>
    /// WHENEVER CONDUCT DELETE METHOD, THAT ENTITY MUST HAVE CHECK ONE IS DELETED OR NOT
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class DeleteEntity<TKey> : EntityBase<TKey>, IDeleteEntity<TKey>
    {
        public bool IsDeleted { get; set; }
    }
    /// <summary>
    /// IF WORK WITH INSERT, UPDATE, DELETE WE HAVE TO SAVE HISTORY. IT'S TIME
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class AuditEntity<TKey> : DeleteEntity<TKey>, IAuditEntity<TKey>
    {
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}