﻿using System;

namespace Evolve.NET.Core
{
    public class Chromosome : IChromosome
    {
        private int[] m_Genes;

        public int this[int index]
        {
            get { return m_Genes[index]; }
            set { m_Genes[index] = value; }
        }

        public double Fitness { get; private set; }

        public int[] Genes
        {
            get { return m_Genes; }
        }

        public int Length
        {
            get { return m_Genes.Length; }
        }

        public Chromosome(int length, int min, int max)
        {
            m_Genes = new int[length];
            for (int i = 0; i < length; i++)
                m_Genes[i] = Helper.RandomInt(min, max);
        }

        public Chromosome(int[] genes)
        {
            m_Genes = new int[genes.Length];
            Array.ConstrainedCopy(genes, 0, m_Genes, 0, genes.Length);
        }

        public Chromosome(Chromosome chromosome)
            : this(chromosome.Genes)
        {

        }

        public int CompareTo(IChromosome other)
        {
            if (Fitness > other.Fitness)
                return -1;

            if (Fitness < other.Fitness)
                return 1;

            return 0;
        }

        public void EvaluateFitness(IFitness function)
        {
            Fitness = function.Evaluate(this);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            IChromosome chromosome = (IChromosome)obj;
            for (int i = 0; i < Length; i++)
                if (Genes[i] != chromosome[i])
                    return false;

            return true;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                foreach (int alelo in m_Genes)
                    hash = hash * alelo.GetHashCode();

                return hash;
            }
        }

        public static bool operator ==(Chromosome a, Chromosome b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Chromosome a, Chromosome b)
        {
            return !(a == b);
        }
    }
}
