﻿using System;
using System.Collections.Generic;
using System.Linq;
using Gva.Api.Models;
using Gva.Api.Models.Views.Person;

namespace Gva.Api.ModelsDO.Persons
{
    public class PersonViewDO
    {
        public PersonViewDO(GvaViewPerson personData, bool? hasExamSystData = false)
        {
            this.Id = personData.LotId;
            this.Lin = personData.Lin;
            this.LinType = personData.LinType.Code;
            this.Uin = personData.Uin;
            this.Names = personData.Names;
            this.NamesAlt = personData.NamesAlt;
            this.Age = this.GetAge(personData.BirtDate.Date);
            this.Organization = personData.Organization == null ? null : personData.Organization.Name;
            this.Employment = personData.Employment == null ? null : personData.Employment.Name;
            this.Licences = personData.Licences;
            this.Ratings = personData.Ratings;
            this.CaseTypes = personData.CaseTypes;
            this.HasExamSystData = hasExamSystData.Value;
        }

        public int Id { get; set; }

        public int? Lin { get; set; }

        public string LinType { get; set; }

        public string Uin { get; set; }

        public string Names { get; set; }

        public string NamesAlt { get; set; }

        public int Age { get; set; }

        public string Licences { get; set; }

        public string Ratings { get; set; }

        public string Organization { get; set; }

        public string Employment { get; set; }

        public string CaseTypes { get; set; }

        public bool HasExamSystData { get; set; }

        private int GetAge(DateTime birthDate)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - birthDate.Year;
            if (birthDate > today.AddYears(-age))
            {
                age--;
            }

            return age;
        }
    }
}