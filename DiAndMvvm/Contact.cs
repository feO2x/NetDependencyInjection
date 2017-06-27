using System;
using Light.GuardClauses;

namespace DiAndMvvm
{
    public sealed class Contact
    {
        private DateTime? _birthDate;
        private string _email;
        private string _firstName;
        private string _lastName;
        private string _phone;

        public string FirstName
        {
            get => _firstName;
            set => _firstName = value.MustNotBeNullOrWhiteSpace();
        }

        public string LastName
        {
            get => _lastName;
            set => _lastName = value.MustNotBeNullOrWhiteSpace();
        }

        public DateTime? BirthDate
        {
            get => _birthDate;
            set => _birthDate = value;
        }

        public string Phone
        {
            get => _phone;
            set => _phone = value.MustNotBeNullOrWhiteSpace();
        }

        public string Email
        {
            get => _email;
            set => _email = value.MustNotBeNullOrWhiteSpace();
        }
    }
}