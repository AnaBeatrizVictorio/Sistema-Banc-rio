using System;

namespace SistemaBancario
{

    /// Classe Cliente que demonstra ENCAPSULAMENTO
    /// Os dados são protegidos e acessados apenas através de propriedades
    
    public class Cliente
    {
        // Campos privados - ENCAPSULAMENTO
        private string _nome;
        private string _cpf;
        private DateTime _dataNascimento;
        private string _endereco;

        // Propriedades públicas com validação - ENCAPSULAMENTO
        public string Nome
        {
            get { return _nome; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Nome não pode ser vazio ou nulo");
                _nome = value;
            }
        }

        public string Cpf
        {
            get { return _cpf; }
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length != 11)
                    throw new ArgumentException("CPF deve ter 11 dígitos");
                _cpf = value;
            }
        }

        public DateTime DataNascimento
        {
            get { return _dataNascimento; }
            set
            {
                if (value > DateTime.Now)
                    throw new ArgumentException("Data de nascimento não pode ser futura");
                _dataNascimento = value;
            }
        }

        public string Endereco
        {
            get { return _endereco; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Endereço não pode ser vazio");
                _endereco = value;
            }
        }

        // Propriedade calculada - ENCAPSULAMENTO
        public int Idade
        {
            get
            {
                int idade = DateTime.Now.Year - _dataNascimento.Year;
                if (DateTime.Now.DayOfYear < _dataNascimento.DayOfYear)
                    idade--;
                return idade;
            }
        }

        // Construtor
        public Cliente(string nome, string cpf, DateTime dataNascimento, string endereco)
        {
            Nome = nome;
            Cpf = cpf;
            DataNascimento = dataNascimento;
            Endereco = endereco;
        }

        // Método público para exibir informações do cliente
        public override string ToString()
        {
            return $"Cliente: {Nome}\nCPF: {Cpf}\nIdade: {Idade} anos\nEndereço: {Endereco}";
        }
    }
}
