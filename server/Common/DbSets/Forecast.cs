using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DbSets
{
    public class Forecast : BaseEntity
    {
        public Org Org { get; set; }
        public User Manager { get; set; }
        public User USFocal { get; set; }
        public Project Project { get; set; }
        public Skill SkillGroup { get; set; }
        public Business Business { get; set; }
        public Capability Capability { get; set; }
        public string Chargeline { get; set; }
        public Category ForecastConfidence { get; set; }
        public string Comments { get; set; }
        public List<ForecastData> ForecastData { get; set; }
    }
}
