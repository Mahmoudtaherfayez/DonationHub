using DonationHub.Domain.Common;

namespace DonationHub.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string NameAr { get; private set; } = null!;

        public string NameEn { get; private set; } = null!;

        public string DescriptionAr { get; private set; } = null!;

        public string DescriptionEn { get; private set; } = null!;
        //navigation prop
        public ICollection<Campaign> Campaigns { get; private set; }
        = new List<Campaign>();

        private Category() { }

        public Category(
            string nameAr,
            string nameEn,
            string descriptionAr,
            string descriptionEn,
            DateTime currentTime) : base(currentTime)
        {
            if (string.IsNullOrWhiteSpace(nameAr))
                throw new ArgumentException("Arabic name is required.");
        
           if (string.IsNullOrWhiteSpace(nameEn))
            throw new ArgumentException("English name is required.");

              NameAr = nameAr;
              NameEn = nameEn;
              DescriptionAr = descriptionAr;
              DescriptionEn = descriptionEn;
        }
        public void UpdateDetails(
      string nameAr,
      string nameEn,
      string descriptionAr,
      string descriptionEn,
      DateTime currentTime)
        {
            if (string.IsNullOrWhiteSpace(nameAr))
                throw new ArgumentException("Arabic name is required.");

            if (string.IsNullOrWhiteSpace(nameEn))
                throw new ArgumentException("English name is required.");

            NameAr = nameAr;
            NameEn = nameEn;
            DescriptionAr = descriptionAr;
            DescriptionEn = descriptionEn;

            //UpdateAt = DateTime.UtcNow;
            SetUpdatedAt(currentTime);
        }

    }

}
