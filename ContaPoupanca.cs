using System;

namespace SistemaBancario
{

    /// Classe ContaPoupanca que demonstra HERANÇA
    /// Herda de Conta e implementa comportamentos específicos da poupança

    public class ContaPoupanca : Conta
    {
        // Campos privados específicos da conta poupança
        private DateTime _dataUltimaRendimento;
        private decimal _taxaRendimento;

        // Propriedades específicas - ENCAPSULAMENTO
        public DateTime DataUltimaRendimento
        {
            get { return _dataUltimaRendimento; }
            private set { _dataUltimaRendimento = value; }
        }

        public decimal TaxaRendimento
        {
            get { return _taxaRendimento; }
            set
            {
                if (value < 0 || value > 0.1m) // Máximo 10% ao mês
                    throw new ArgumentException("Taxa de rendimento deve estar entre 0% e 10%");
                _taxaRendimento = value;
            }
        }

        // Implementação das propriedades abstratas
        public override decimal TaxaOperacao => 0m; // Poupança não tem taxa de operação
        public override decimal LimiteSaque => 1000m; // Limite menor para poupança

        // Construtor
        public ContaPoupanca(Cliente titular, decimal taxaRendimento = 0.005m) : base(titular)
        {
            TaxaRendimento = taxaRendimento;
            DataUltimaRendimento = DateTime.Now;
        }

        // Implementação do método Sacar com regras específicas da poupança
        public override bool Sacar(decimal valor)
        {
            if (valor <= 0)
                throw new ArgumentException("Valor deve ser positivo");

            if (valor > LimiteSaque)
            {
                Console.WriteLine($"Erro: Limite de saque excedido. Máximo permitido: {LimiteSaque:C}");
                return false;
            }

            if (valor <= Saldo)
            {
                Saldo -= valor;
                AdicionarMovimentacao("Saque", -valor, Saldo);
                Console.WriteLine($"Saque realizado com sucesso. Valor: {valor:C}");
                Console.WriteLine($"Saldo atual: {Saldo:C}");
                return true;
            }
            else
            {
                Console.WriteLine("Erro: Saldo insuficiente");
                return false;
            }
        }

        // Implementação do método Depositar
        public override bool Depositar(decimal valor)
        {
            if (valor <= 0)
                throw new ArgumentException("Valor deve ser positivo");

            Saldo += valor;
            AdicionarMovimentacao("Depósito", valor, Saldo);
            Console.WriteLine($"Depósito realizado com sucesso. Valor: {valor:C}");
            Console.WriteLine($"Saldo atual: {Saldo:C}");
            return true;
        }

        // Método específico da conta poupança - aplica rendimento
        public void AplicarRendimento()
        {
            DateTime hoje = DateTime.Now;
            
            // Aplica rendimento mensalmente
            if (hoje.Month != DataUltimaRendimento.Month || hoje.Year != DataUltimaRendimento.Year)
            {
                if (Saldo > 0)
                {
                    decimal rendimento = Saldo * TaxaRendimento;
                    Saldo += rendimento;
                    DataUltimaRendimento = hoje;
                    AdicionarMovimentacao("Rendimento", rendimento, Saldo);
                    Console.WriteLine($"Rendimento aplicado: {rendimento:C}");
                    Console.WriteLine($"Novo saldo: {Saldo:C}");
                }
            }
            else
            {
                Console.WriteLine("Rendimento já foi aplicado este mês");
            }
        }

        // Sobrescreve o método de taxa mensal para aplicar rendimento em vez de cobrar taxa
        public override void AplicarTaxaMensal()
        {
            AplicarRendimento();
        }

        // Método específico para calcular rendimento projetado
        public decimal CalcularRendimentoProjetado(int meses)
        {
            if (meses <= 0)
                throw new ArgumentException("Número de meses deve ser positivo");

            decimal saldoProjetado = Saldo;
            for (int i = 0; i < meses; i++)
            {
                saldoProjetado += saldoProjetado * TaxaRendimento;
            }
            return saldoProjetado - Saldo;
        }

        public override string ToString()
        {
            return base.ToString() + $" - Tipo: Conta Poupança - Taxa: {TaxaRendimento:P}";
        }
    }
}
