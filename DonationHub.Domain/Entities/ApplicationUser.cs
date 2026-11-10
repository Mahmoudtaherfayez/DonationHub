using Microsoft.AspNetCore.Identity;

namespace DonationHub.Domain.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FullName { get; private set; } = null!;
        public string PreferredLanguage { get; private set; } = "en";
        public bool IsActive { get; private set; } = true;
        public DateTime CreatedAt { get; private set; } 
        private ApplicationUser() { }
        public ApplicationUser(
            string email,
            string username,
            string fullName,
             DateTime currentTime
            )
        {
            Email = email;
            UserName = username;
            FullName = fullName;
            CreatedAt = currentTime;
        }

        public void UpdateProfile(string fullname, string preferredLanguage)
        {
            if (string.IsNullOrWhiteSpace(fullname))
                throw new ArgumentException("Full name is Required");
            if (preferredLanguage != "en" && preferredLanguage != "ar")
                throw new ArgumentException("Language must be en or ar.");
            FullName = fullname;
            PreferredLanguage = preferredLanguage;
            
        }
        public void Deactivate()
        {
            IsActive = false;
        }
        public void Activate()
        {
            IsActive = true;
        }



    }
}
