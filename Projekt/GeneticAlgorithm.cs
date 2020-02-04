using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AForge.Genetic;
using AForge.Math.Random;

namespace Projekt
{
    public class GeneticAlgorithm
    {

        private int selectedMethod; //wybrana metoda selekcji
        private double[,] result;


        public GeneticAlgorithm(int selectedFitnessFunction, int populationSize, int iterationsNumber, int selectedMethod)
        {

            this.selectedMethod = selectedMethod;
            ISelectionMethod selectionMethot = GetSelectionMethod();
            
            DoubleArrayChromosome chromosome = new DoubleArrayChromosome(new AForge.Math.Random.StandardGenerator(), new AForge.Math.Random.StandardGenerator(), new AForge.Math.Random.StandardGenerator(), 2);
            FitnessFunction fitnessfunction = new FitnessFunction(selectedFitnessFunction);
            Population population = new Population(populationSize, chromosome, fitnessfunction, selectionMethot);
            double[,] tmp = new double[iterationsNumber, 2];
            this.result = new double[iterationsNumber, 2];
            int i;
            for (i=0; i < iterationsNumber; i++)
            {
                population.RunEpoch();
                DoubleArrayChromosome bestChromosome = (DoubleArrayChromosome)population.BestChromosome;
                double x = (double)bestChromosome.Value.GetValue(0);
                double y = (double)bestChromosome.Value.GetValue(1);
                tmp[i, 0] = x;
                tmp[i, 1] = y;
                if(fitnessfunction.Evaluate(bestChromosome)==100)
                {
                    break;
                }
            }
            this.result = new double[i, 2];
            for(int j=0; j<i; j++)
            {
                result[j, 0] = tmp[j,0];
                result[j, 1] = tmp[j,1];
            }


        }
        public double[,] returnResult()
        {
            return this.result;
        }
        
        #region funkcje dopasowania
        private class FitnessFunction : IFitnessFunction
        {
            int selectedFitnessFunction;
            public FitnessFunction(int selectedFitnessFunction)
            {
                this.selectedFitnessFunction = selectedFitnessFunction;
            }
            public double Evaluate(IChromosome chromosome)
            {

                DoubleArrayChromosome actualChromosome = (DoubleArrayChromosome)chromosome;
                double x = (double)actualChromosome.Value.GetValue(0);
                double y = (double)actualChromosome.Value.GetValue(1);
                double functionResult;
                switch (selectedFitnessFunction)
                {
                    case 0:
                        functionResult = Math.Pow(x, 2) * y + 8 * x - x * Math.Pow(y, 2) + 5 * y;
                        break;
                    case 1:
                        functionResult = Math.Pow(x, 2) * y + x * Math.Pow(y, 2) - 2 * x;
                        break;
                    case 2:
                        functionResult = 4 * x + 2 * Math.Pow(y, 2) + x * y;
                        break;
                    case 3:
                        functionResult = Math.Pow(y, 2) + 2 * x * Math.Pow(y, 2) + 3 * x;
                        break;
                    default:
                        functionResult = 0;
                        break;
                }


                functionResult = Math.Abs(functionResult);
                double result = 100 - functionResult;

                return result;

            }
        }
        #endregion

        // private IChromosome chromosone = new DoubleArrayChromosome();
        private ISelectionMethod GetSelectionMethod() //wybor metody selekcji
        {
            switch (this.selectedMethod)
            {
                case 0:
                    return new EliteSelection();
                case 1:
                    return new RankSelection();
                case 2:
                    return new RouletteWheelSelection();
                default:
                    return null;


            }
        }
    }
}
