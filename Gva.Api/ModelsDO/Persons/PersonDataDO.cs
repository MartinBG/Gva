﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;
using Common.ValidationAttributes;

namespace Gva.Api.ModelsDO.Persons
{
    public class PersonDataDO
    {
        public PersonDataDO()
        {
            this.CaseTypes = new List<int>();
        }

        [Required(ErrorMessage = "FirstName is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "FirstNameAlt is required.")]
        public string FirstNameAlt { get; set; }

        [Required(ErrorMessage = "MiddleName is required.")]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "MiddleNameAlt is required.")]
        public string MiddleNameAlt { get; set; }

        [Required(ErrorMessage = "LastName is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "LastNameAlt is required.")]
        public string LastNameAlt { get; set; }

        public string Uin { get; set; }

        [Required(ErrorMessage = "Sex is required.")]
        public int? SexId { get; set; }

        [Required(ErrorMessage = "LinType is required.")]
        public int? LinTypeId { get; set; }

        public int? Lin { get; set; }

        [Required(ErrorMessage = "DateOfBirth is required.")]
        public DateTime? DateOfBirth { get; set; }

        public int? PlaceOfBirthId { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        public int? CountryId { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email.")]
        public string Email { get; set; }

        public string Fax { get; set; }

        public string Phone1 { get; set; }

        public string Phone2 { get; set; }

        public string Phone3 { get; set; }

        public string Phone4 { get; set; }

        public string Phone5 { get; set; }

        public List<int> CaseTypes { get; set; }
    }
}
