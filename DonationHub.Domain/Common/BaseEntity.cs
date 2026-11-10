using Microsoft.AspNetCore.Http.HttpResults;

namespace DonationHub.Domain.Common
{
    public class BaseEntity
    {
        public Guid Id { get; protected set; } = Guid.NewGuid();
        public DateTime CreateAt { get; protected set; } 
        public  DateTime? UpdateAt { get; protected set; }
        public bool IsDeleted { get; protected set; }
        public byte[]? RowVersion { get; protected set; }
        protected BaseEntity()
        {
        }
        protected BaseEntity(DateTime currentTime)
        {
            CreateAt = currentTime;
        }

        protected void SetUpdatedAt(DateTime currentTime)
        {
            UpdateAt = currentTime;
        }
    }
}
