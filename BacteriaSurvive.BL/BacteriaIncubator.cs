using System;
using System.Collections.Generic;
using System.Linq;

namespace BacteriaSurvive.BL
{
    public class BacteriaIncubator
    {
        private readonly int _mutationLimit;
        private readonly Random _random;

        IList<IList<int>> _possibleMutations=new List<IList<int>>();

        public BacteriaIncubator(int mutationLimit, Random random)
        {
            _mutationLimit = mutationLimit;
            _random = random;
            _possibleMutations = GeneratePossibleMutation(_mutationLimit);
        }



        private IList<IList<int>> GeneratePossibleMutation(int commonMutationLimit)
        {
            IList<IList<int>> result = new List<IList<int>>();
            
            
            for (int atr1 = (-1 * commonMutationLimit)/2; atr1 <= commonMutationLimit/2; atr1++)
            {
                for (int atr2 = (-1 * commonMutationLimit)/2; atr2 <= commonMutationLimit/2; atr2++)
                {
                    int atr3Abs = commonMutationLimit - Math.Abs(atr1) - Math.Abs(atr2);
                    int atr3Positive = atr3Abs;
                    if ((Math.Abs(atr1) + Math.Abs(atr2) + atr3Positive == commonMutationLimit) && (atr1 + atr2 + atr3Positive == 0))
                    {
                        IList<int> mutation = new List<int> { atr1, atr2, atr3Positive };
                        result.Add(mutation);
                    }
                    int atr3Negative =-1* atr3Abs;
                    if ((Math.Abs(atr1) + Math.Abs(atr2) + atr3Abs == commonMutationLimit) && (atr1 + atr2 + atr3Negative == 0))
                    {
                        IList<int> mutation = new List<int> { atr1, atr2, atr3Negative };
                        result.Add(mutation);
                    }
                }
            }
            return result;
        }


        private KeyValuePair<int, int> GetProbability(int parentProbability, IList<int> mutation, Random random, int recursiveStep)
        {
            if (recursiveStep>40)
                throw new ApplicationException("731765AC-E477-47B5-9028-B244C15543BC: зацикливание");

            int index = random.Next(0, mutation.Count);
            int propabilityMutattion= mutation[index];



            while ((parentProbability == 100) && (propabilityMutattion >= 0))
            {
                index = random.Next(0, mutation.Count);
                propabilityMutattion= mutation[index];
                
            }

            while ((parentProbability == 0) && (propabilityMutattion < 0))
            {
                index = random.Next(0, mutation.Count);
                propabilityMutattion = mutation[index];
               

            }
            int propability = parentProbability+propabilityMutattion;
            if ((propability > 100) || (propability < 0))
            {
                return GetProbability(parentProbability, mutation, random, recursiveStep+1);
            }
            return new KeyValuePair<int, int>(index, propability);

        }

        public int MutationLimit
        {
            get { return _mutationLimit; }
        }


        
        public Bacteria Clone(Bacteria source)
        {
            Bacteria clonedBacteria=new Bacteria(source.StoneProbability, source.ScissorsProbability, source.PaperProbability, source.Type, source.CommonMutationLimit);
            return clonedBacteria;
        }



        public Bacteria GenerateChild(Bacteria parent)
        {
          
         
            if (parent.CommonMutationLimit == 0)
            {
                return Clone(parent);
            }


            int mutationNumber = _random.Next(0, _possibleMutations.Count - 1);
            IList<int> mutation = _possibleMutations[mutationNumber];

            int halfOfCommonMutationLimit = parent.CommonMutationLimit/2;

            if ((parent.PaperProbability > 100 - halfOfCommonMutationLimit) ||
                (parent.ScissorsProbability > 100 - halfOfCommonMutationLimit) ||
                (parent.StoneProbability > 100 - halfOfCommonMutationLimit))
            {
                while (Math.Abs(mutation.Min()) != _mutationLimit/2)
                {
                    mutationNumber = _random.Next(0, _possibleMutations.Count - 1);
                    mutation = _possibleMutations[mutationNumber];
                }
            }
            mutation = mutation.ToList();



            IList<BacteriaAttribute> parentAttributes=new List<BacteriaAttribute>();
            parentAttributes.Add(new BacteriaAttribute(EBacteriaAttributes.Paper, parent.PaperProbability));
            parentAttributes.Add(new BacteriaAttribute(EBacteriaAttributes.Stone, parent.StoneProbability));
            parentAttributes.Add(new BacteriaAttribute(EBacteriaAttributes.Scissors, parent.ScissorsProbability));

            parentAttributes = parentAttributes.OrderBy(attr => attr.Value).ToList();


            int childStoneProbability=0;
            int childScissorsProbability=0;
            int childPaperProbability=0;



            foreach (BacteriaAttribute bacteriaAttribute in parentAttributes)
            {


                KeyValuePair<int, int> childProbabilityInfo = GetProbability(bacteriaAttribute.Value, mutation, _random,0);
                    mutation.RemoveAt(childProbabilityInfo.Key);

                    switch (bacteriaAttribute.AttributeType)
                    {
                        case EBacteriaAttributes.Stone:
                            childStoneProbability = childProbabilityInfo.Value;
                            break;
                        case EBacteriaAttributes.Paper:
                            childPaperProbability = childProbabilityInfo.Value;
                            break;
                        case EBacteriaAttributes.Scissors:
                            childScissorsProbability = childProbabilityInfo.Value;
                            break;
                    }

                

            }

            return new Bacteria(childStoneProbability, childScissorsProbability, childPaperProbability, parent.Type, parent.CommonMutationLimit);


        }

    }
}
