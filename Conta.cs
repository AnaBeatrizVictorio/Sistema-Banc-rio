using System;
using System.Collections.Generic;

namespace SistemaBancario
{

    /// Classe abstrata Conta que demonstra ABSTRAÇÃO e ENCAPSULAMENTO
    /// Define o comportamento comum para todos os tipos de conta

    public abstract class Conta : ITransacao
    {
        // Campos privados - ENCAPSULAMENTO
        private static int _proximoNumeroConta = 1000;
        private readonly int _numeroConta;
        private decimal _saldo;
        private readonly Cliente _titular;
        private readonly List<string> _extrato;

        // Propriedades públicas - ENCAPSULAMENTO
        public int NumeroConta
        {
            get { return _numeroConta; }
        }

        public decimal Saldo
        {
            get { return _saldo; }
            protected set { _saldo = value; }
        }

        public Cliente Titular
        {
            get { return _titular; }
        }

        public List<string> Extrato
        {
            get { return new List<string>(_extrato); }
        }

        // Propriedades abstratas - ABSTRAÇÃO
        public abstract decimal TaxaOperacao { get; }
        public abstract decimal LimiteSaque { get; }

        // Construtor
        protected Conta(Cliente titular)
        {
            _numeroConta = _proximoNumeroConta++;
            _saldo = 0;
            _titular = titular ?? throw new ArgumentNullException(nameof(titular));
            _extrato = new List<string>();
            AdicionarMovimentacao("Conta criada", 0, _saldo);
        }

        // Métodos abstratos - ABSTRAÇÃO
        public abstract bool Sacar(decimal valor);
        public abstract bool Depositar(decimal valor);

        // Implementação da interface ITransacao
        public virtual bool Transferir(Conta contaDestino, decimal valor)
        {
            if (contaDestino == null)
                throw new ArgumentNullException(nameof(contaDestino));

            if (valor <= 0)
                throw new ArgumentException("Valor deve ser positivo");

            if (Sacar(valor))
            {
                if (contaDestino.Depositar(valor))
                {
                    AdicionarMovimentacao($"Transferência para conta {contaDestino.NumeroConta}", -valor, _saldo);
                    return true;
                }
                else
                {
                    // Reverte o saque se o depósito falhar
                    Depositar(valor);
                    return false;
                }
            }
            return false;
        }

        public virtual void ExibirExtrato()
        {
            Console.WriteLine($"\n=== EXTRATO DA CONTA {_numeroConta} ===");
            Console.WriteLine($"Titular: {_titular.Nome}");
            Console.WriteLine($"Saldo Atual: {_saldo:C}");
            Console.WriteLine("Movimentações:");
            Console.WriteLine("Data\t\t\tOperação\t\t\tValor\t\tSaldo");
            Console.WriteLine(new string('-', 80));

            foreach (string movimentacao in _extrato)
            {
                Console.WriteLine(movimentacao);
            }
        }

        // Método protegido para adicionar movimentações ao extrato
        protected void AdicionarMovimentacao(string operacao, decimal valor, decimal saldoAtual)
        {
            string dataHora = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            string linhaExtrato = $"{dataHora}\t{operacao,-20}\t{valor:C}\t{saldoAtual:C}";
            _extrato.Add(linhaExtrato);
        }

        // Método virtual que pode ser sobrescrito pelas classes filhas
        public virtual void AplicarTaxaMensal()
        {
            if (_saldo > 0)
            {
                decimal taxa = _saldo * TaxaOperacao;
                _saldo -= taxa;
                AdicionarMovimentacao("Taxa mensal", -taxa, _saldo);
            }
        }

        public override string ToString()
        {
            return $"Conta {_numeroConta} - {_titular.Nome} - Saldo: {_saldo:C}";
        }
    }
}
