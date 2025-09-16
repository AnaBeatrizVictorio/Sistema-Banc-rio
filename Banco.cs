using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaBancario
{
    /// <summary>
    /// Classe Banco que gerencia todas as contas
    /// Demonstra ENCAPSULAMENTO e uso de coleções
    /// </summary>
    public class Banco
    {
        // Campo privado para armazenar as contas - ENCAPSULAMENTO
        private readonly List<Conta> _contas;
        private readonly string _nomeBanco;

        // Propriedades públicas - ENCAPSULAMENTO
        public string NomeBanco
        {
            get { return _nomeBanco; }
        }

        public int TotalContas
        {
            get { return _contas.Count; }
        }

        public decimal CapitalTotal
        {
            get { return _contas.Sum(c => c.Saldo); }
        }

        // Construtor
        public Banco(string nomeBanco)
        {
            _nomeBanco = nomeBanco ?? throw new ArgumentNullException(nameof(nomeBanco));
            _contas = new List<Conta>();
        }

        // Métodos públicos para gerenciar contas - ENCAPSULAMENTO
        public bool AdicionarConta(Conta conta)
        {
            if (conta == null)
                throw new ArgumentNullException(nameof(conta));

            // Verifica se já existe uma conta com o mesmo número
            if (_contas.Any(c => c.NumeroConta == conta.NumeroConta))
            {
                Console.WriteLine($"Erro: Já existe uma conta com o número {conta.NumeroConta}");
                return false;
            }

            _contas.Add(conta);
            Console.WriteLine($"Conta {conta.NumeroConta} adicionada com sucesso ao banco {_nomeBanco}");
            return true;
        }

        public bool RemoverConta(int numeroConta)
        {
            var conta = BuscarConta(numeroConta);
            if (conta != null)
            {
                _contas.Remove(conta);
                Console.WriteLine($"Conta {numeroConta} removida com sucesso");
                return true;
            }
            Console.WriteLine($"Conta {numeroConta} não encontrada");
            return false;
        }

        public Conta BuscarConta(int numeroConta)
        {
            return _contas.FirstOrDefault(c => c.NumeroConta == numeroConta);
        }

        public List<Conta> BuscarContasPorTitular(string nomeTitular)
        {
            return _contas.Where(c => c.Titular.Nome.Contains(nomeTitular, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public List<Conta> ListarContas()
        {
            return new List<Conta>(_contas); // Retorna uma cópia para manter encapsulamento
        }

        public List<ContaCorrente> ListarContasCorrente()
        {
            return _contas.OfType<ContaCorrente>().ToList();
        }

        public List<ContaPoupanca> ListarContasPoupanca()
        {
            return _contas.OfType<ContaPoupanca>().ToList();
        }

        // Métodos para operações bancárias
        public bool RealizarTransferencia(int contaOrigem, int contaDestino, decimal valor)
        {
            var contaOrig = BuscarConta(contaOrigem);
            var contaDest = BuscarConta(contaDestino);

            if (contaOrig == null)
            {
                Console.WriteLine($"Conta origem {contaOrigem} não encontrada");
                return false;
            }

            if (contaDest == null)
            {
                Console.WriteLine($"Conta destino {contaDestino} não encontrada");
                return false;
            }

            return contaOrig.Transferir(contaDest, valor);
        }

        // Métodos para relatórios
        public void ExibirRelatorioGeral()
        {
            Console.WriteLine($"\n=== RELATÓRIO GERAL - {_nomeBanco} ===");
            Console.WriteLine($"Total de contas: {TotalContas}");
            Console.WriteLine($"Capital total: {CapitalTotal:C}");
            Console.WriteLine($"Contas corrente: {ListarContasCorrente().Count}");
            Console.WriteLine($"Contas poupança: {ListarContasPoupanca().Count}");
            Console.WriteLine($"Capital em contas corrente: {ListarContasCorrente().Sum(c => c.Saldo):C}");
            Console.WriteLine($"Capital em contas poupança: {ListarContasPoupanca().Sum(c => c.Saldo):C}");
        }

        public void ExibirContasComSaldoNegativo()
        {
            var contasNegativas = _contas.Where(c => c.Saldo < 0).ToList();
            
            Console.WriteLine($"\n=== CONTAS COM SALDO NEGATIVO ===");
            if (contasNegativas.Any())
            {
                foreach (var conta in contasNegativas)
                {
                    Console.WriteLine(conta.ToString());
                }
            }
            else
            {
                Console.WriteLine("Nenhuma conta com saldo negativo encontrada.");
            }
        }

        public void AplicarTaxasMensais()
        {
            Console.WriteLine("\n=== APLICANDO TAXAS MENSAL ===");
            foreach (var conta in _contas)
            {
                conta.AplicarTaxaMensal();
            }
        }

        public void ExibirRankingMaioresSaldos(int quantidade = 5)
        {
            var maioresSaldos = _contas
                .OrderByDescending(c => c.Saldo)
                .Take(quantidade)
                .ToList();

            Console.WriteLine($"\n=== TOP {quantidade} MAIORES SALDOS ===");
            for (int i = 0; i < maioresSaldos.Count; i++)
            {
                Console.WriteLine($"{i + 1}º - {maioresSaldos[i].ToString()}");
            }
        }
    }
}
