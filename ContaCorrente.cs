using System;

namespace SistemaBancario
{
    /// <summary>
    /// Classe ContaCorrente que demonstra HERANÇA
    /// Herda de Conta e implementa comportamentos específicos
    /// </summary>
    public class ContaCorrente : Conta
    {
        // Campos privados específicos da conta corrente
        private decimal _limiteCredito;
        private decimal _valorUsadoLimite;

        // Propriedades específicas - ENCAPSULAMENTO
        public decimal LimiteCredito
        {
            get { return _limiteCredito; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Limite de crédito não pode ser negativo");
                _limiteCredito = value;
            }
        }

        public decimal ValorUsadoLimite
        {
            get { return _valorUsadoLimite; }
            private set { _valorUsadoLimite = value; }
        }

        public decimal ValorDisponivelLimite
        {
            get { return _limiteCredito - _valorUsadoLimite; }
        }

        // Implementação das propriedades abstratas
        public override decimal TaxaOperacao => 0.01m; // 1% ao mês
        public override decimal LimiteSaque => 5000m; // Limite de R$ 5.000 por saque

        // Construtor
        public ContaCorrente(Cliente titular, decimal limiteCredito = 1000m) : base(titular)
        {
            LimiteCredito = limiteCredito;
            ValorUsadoLimite = 0;
        }

        // Implementação do método Sacar com regras específicas da conta corrente
        public override bool Sacar(decimal valor)
        {
            if (valor <= 0)
                throw new ArgumentException("Valor deve ser positivo");

            if (valor > LimiteSaque)
            {
                Console.WriteLine($"Erro: Limite de saque excedido. Máximo permitido: {LimiteSaque:C}");
                return false;
            }

            decimal saldoDisponivel = Saldo + ValorDisponivelLimite;

            if (valor <= saldoDisponivel)
            {
                if (valor <= Saldo)
                {
                    // Saque normal
                    Saldo -= valor;
                }
                else
                {
                    // Usa o limite de crédito
                    decimal valorLimite = valor - Saldo;
                    Saldo = 0;
                    ValorUsadoLimite += valorLimite;
                }

                AdicionarMovimentacao("Saque", -valor, Saldo);
                Console.WriteLine($"Saque realizado com sucesso. Valor: {valor:C}");
                Console.WriteLine($"Saldo atual: {Saldo:C}");
                Console.WriteLine($"Limite usado: {ValorUsadoLimite:C}");
                return true;
            }
            else
            {
                Console.WriteLine("Erro: Saldo insuficiente e limite de crédito excedido");
                return false;
            }
        }

        // Implementação do método Depositar
        public override bool Depositar(decimal valor)
        {
            if (valor <= 0)
                throw new ArgumentException("Valor deve ser positivo");

            // Se há limite usado, paga primeiro o limite
            if (ValorUsadoLimite > 0)
            {
                if (valor <= ValorUsadoLimite)
                {
                    ValorUsadoLimite -= valor;
                }
                else
                {
                    decimal valorRestante = valor - ValorUsadoLimite;
                    ValorUsadoLimite = 0;
                    Saldo += valorRestante;
                }
            }
            else
            {
                Saldo += valor;
            }

            AdicionarMovimentacao("Depósito", valor, Saldo);
            Console.WriteLine($"Depósito realizado com sucesso. Valor: {valor:C}");
            Console.WriteLine($"Saldo atual: {Saldo:C}");
            return true;
        }

        // Método específico da conta corrente
        public void AumentarLimiteCredito(decimal novoLimite)
        {
            if (novoLimite <= LimiteCredito)
                throw new ArgumentException("Novo limite deve ser maior que o atual");

            LimiteCredito = novoLimite;
            Console.WriteLine($"Limite de crédito aumentado para {LimiteCredito:C}");
        }

        public override string ToString()
        {
            return base.ToString() + $" - Tipo: Conta Corrente - Limite: {LimiteCredito:C}";
        }
    }
}
