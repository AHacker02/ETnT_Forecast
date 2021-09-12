using System;
using Newtonsoft.Json;

namespace Common.Models
{
    public class ForecastViewModel
    {
        public String EtNtOrg { get; set; }
        public String Manager { get; set; }
        public String UsFocal { get; set; }
        public String Project { get; set; }
        public String SkillGroup { get; set; }
        public String BusinessUnit { get; set; }
        public String Capabilities { get; set; }
        public String ChargeLine { get; set; }
        public String ForecastConfidence { get; set; }
        public String Comments { get; set; }
        public Decimal Jan { get; set; }
        public Decimal Feb { get; set; }
        public Decimal Mar { get; set; }
        public Decimal Apr { get; set; }
        public Decimal May { get; set; }
        public Decimal June { get; set; }
        public Decimal July { get; set; }
        public Decimal Aug { get; set; }
        public Decimal Oct { get; set; }
        public Decimal Sept { get; set; }
        public Decimal Nov { get; set; }
        public Decimal Dec { get; set; }
        
    }
}