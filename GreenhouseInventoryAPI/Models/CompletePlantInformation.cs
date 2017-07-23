using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenhouseInventoryAPI.Models
{
    public class CompletePlantInformation
    {
        public int ID;
        public string Genus;
        public string Species;
        public string CommonNames;
        public bool LowWater;
        public bool MedWater;
        public bool HighWater;
        public float MaxHeight;
        public float MaxSpread;
        public bool CuttingPropagation;
        public bool DivisionPropagation;
        public bool LeafPropagation;
        public bool Poisonous;
        public bool Sharp;
        public bool Irritant;
        public string SpecialInstructions;
    }
}