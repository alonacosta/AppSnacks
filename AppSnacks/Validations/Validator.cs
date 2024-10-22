using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AppSnacks.Validations
{
    public class Validator : IValidator
    {
        public string NameErro { get; set; } = "";
        public string EmailErro { get; set; } = "";
        public string PhoneErro { get; set; } = "";
        public string PasswordErro { get; set; } = "";

        private const string NameEmptyErroMsg = "Por favor, informe o seu nome.";
        private const string NameInvalidErroMsg = "Por favor, informe um nome válido.";
        private const string EmailEmptyErroMsg = "Por favor, informe um email.";
        private const string EmailInvalidErroMsg = "Por favor, informe um email válido.";
        private const string PhoneEmptyErroMsg = "Por favor, informe um telefone.";
        private const string PhoneInvalidErroMsg = "Por favor, informe um telefone válido.";
        private const string PasswordEmptyErroMsg = "Por favor, informe a senha.";
        private const string PasswordInvalidErroMsg = "A senha deve conter pelo menos 8 caracteres, incluindo letras e números.";

        public Task<bool> Validate(string name, string email, string phone, string password)
        {
            var isNameValid = ValidateName(name);
            var isEmailValid = ValidateEmail(email);
            var isPhoneValid = ValidatePhone(phone);
            var isPasswordValid = ValidatePassword(password);

            return Task.FromResult(isNameValid && isEmailValid && isPhoneValid && isPasswordValid);
        }

        private bool ValidateName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                NameErro = NameEmptyErroMsg;
                return false;
            }

            if (name.Length < 3)
            {
                NameErro = NameInvalidErroMsg;
                return false;
            }

            NameErro = "";
            return true;
        }

        private bool ValidateEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                EmailErro = EmailEmptyErroMsg;
                return false;
            }

            if (!Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            {
                EmailErro = EmailInvalidErroMsg;
                return false;
            }

            EmailErro = "";
            return true;
        }

        private bool ValidatePhone(string phone)
        {
            if (string.IsNullOrEmpty(phone))
            {
                PhoneErro = PhoneEmptyErroMsg;
                return false;
            }

            if (phone.Length < 9)
            {
                PhoneErro = PhoneInvalidErroMsg;
                return false;
            }

            PhoneErro = "";
            return true;
        }

        private bool ValidatePassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                PasswordErro = PasswordEmptyErroMsg;
                return false;
            }

            if (password.Length < 8 || !Regex.IsMatch(password, @"[a-zA-Z]") || !Regex.IsMatch(password, @"\d"))
            {
                PasswordErro = PasswordInvalidErroMsg;
                return false;
            }

            PasswordErro = "";
            return true;
        }

       
    }
}
