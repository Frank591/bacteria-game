﻿using System;

namespace BacteriaSurvive.BL
{
    public class Bacteria
    {
        public BacteriaType Type { get; set; }

        public int StoneProbability
        {
            get { return _stoneProbability; }
        }

        public int ScissorsProbability
        {
            get { return _scissorsProbability; }
        }

        public int PaperProbability
        {
            get { return _paperProbability; }
        }

        public int CommonMutationLimit
        {
            get { return _commonMutationLimit; }
        }

        
        private readonly int _stoneProbability;
        private readonly int _scissorsProbability;
        private readonly int _paperProbability;
        private readonly int _commonMutationLimit;
        
        
        public Bacteria(int stoneProbability, int scissorsProbability, int paperProbability,
                        BacteriaType bacteriaType, int commonMutationLimit)
        {
            Type = bacteriaType;

            if (stoneProbability > 100)
                throw new ArgumentOutOfRangeException("CC2D2A3F-7B04-4541-A803-EC005548C2C2: параметр stoneProbability имеет диапазон от 0 до 100");
            if (scissorsProbability > 100)
                throw new ArgumentOutOfRangeException("F5D7DB98-CE7B-4761-AD26-C298F77212DC: параметр ScissorsProbability имеет диапазон от 0 до 100");
            if (paperProbability > 100)
                throw new ArgumentOutOfRangeException("CA096C0D-A0F6-4409-89D6-25283D7060A0 :параметр paperProbability имеет диапазон от 0 до 100");
            if (Math.Abs(stoneProbability) + Math.Abs(scissorsProbability) + Math.Abs(paperProbability) > 100)
                throw new ArgumentOutOfRangeException("B92524C4-7C4B-492C-8778-9CBE8ADE009B: сумма модулей параметрове  paperProbability,ScissorsProbability,stoneProbability не должны превышать 100");
            if (Math.Abs(commonMutationLimit) > 10)
                throw new ArgumentOutOfRangeException("45559139-81A0-4FBB-89D5-E1429801AFC0: модуль максимального отклонения при мутации в одном поколении не может превышать 10");


            _stoneProbability = stoneProbability;
            _scissorsProbability = scissorsProbability;
            _paperProbability = paperProbability;
            _commonMutationLimit = commonMutationLimit;
            
        }

     

    

    }
}