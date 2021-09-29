using System;
using System.Collections.Generic;
using FluentValidation.Results;

namespace Common.Commands
{
    public class ForecastCommand
    {
        public Guid Id { get; set; }
        public string Org { get; set; }
        public string Manager { get; set; }
        public string USFocal { get; set; }
        public string Project { get; set; }
        public string SkillGroup { get; set; }
        public string Business { get; set; }
        public string Capability { get; set; }
        public string Chargeline { get; set; }
        public string ForecastConfidence { get; set; }
        public string Comments { get; set; }
        public decimal Jan { get; set; }
        public decimal Feb { get; set; }
        public decimal Mar { get; set; }
        public decimal Apr { get; set; }
        public decimal May { get; set; }
        public decimal June { get; set; }
        public decimal July { get; set; }
        public decimal Aug { get; set; }
        public decimal Sep { get; set; }
        public decimal Oct { get; set; }
        public decimal Nov { get; set; }
        public decimal Dec { get; set; }
        public int Year { get; set; }
    }

    public class ForecastCommandError : ForecastCommand
    {
        public List<ValidationFailure> Errors { get; set; }
    }
}