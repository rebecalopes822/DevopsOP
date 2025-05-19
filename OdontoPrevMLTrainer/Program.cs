using System;
using OdontoPrevMLTrainer;

class Program
{
    static void Main(string[] args)
    {
        var resultado = ComplexidadeModel.Predict(new ComplexidadeModel.ModelInput
        {
            TipoTratamento = "Ortodontia",
            Idade = 30,
            Sintomatico = "Sim"
        });

        Console.WriteLine($"Resultado previsto: {resultado.PredictedLabel}");
    }
}
