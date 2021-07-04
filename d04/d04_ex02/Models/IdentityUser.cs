using System;
using System.ComponentModel;
using d04_ex02.Attributes;

namespace d04_ex02.Models
{
    public class IdentityUser
    {
        public IdentityUser()
        {
        }

        
        [Description("User name")]
        [DefaultValue("Me")]
        public virtual string UserName { get; set; }
        
        [NoDisplayAttribute]
        public virtual string NormalizedUserName { get; set; }
        
        [Description("Email address")]
        [DefaultValue("test@test")]
        public virtual string Email { get; set; }

        [NoDisplayAttribute]
        public virtual string NormalizedEmail { get; set; }
        [NoDisplayAttribute]
        public virtual bool EmailConfirmed { get; set; }
        [NoDisplayAttribute]
        public virtual string PasswordHash { get; set; }
        [NoDisplayAttribute]
        public virtual string SecurityStamp { get; set; }
        
        [Description("Phone number")]
        [DefaultValue("1234567890")]
        public virtual string PhoneNumber { get; set; }
        [NoDisplayAttribute]
        public virtual bool PhoneNumberConfirmed { get; set; }
        [NoDisplayAttribute]
        public virtual bool TwoFactorEnabled { get; set; }
        [NoDisplayAttribute]
        public virtual DateTimeOffset? LockoutEnd { get; set; }
        [NoDisplayAttribute]
        public virtual bool LockoutEnabled { get; set; }
        
        public IdentityUser(string userName) : this()
        {
            UserName = userName;
        }
        
        public override string ToString()
            => $"User: {UserName}, {Email}, {PhoneNumber}";
        public virtual string ConcurrencyStamp() => Guid.NewGuid().ToString();

    }

}