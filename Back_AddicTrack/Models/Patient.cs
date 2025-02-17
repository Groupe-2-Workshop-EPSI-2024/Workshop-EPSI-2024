﻿using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Back_AddicTrack.Models.DTOs;
using Back_AddicTrack.Models.Enums;

namespace Back_AddicTrack.Models;

public class Patient : User
{
    [Required] [DataType(DataType.Date)] public DateTime BirthDate { get; set; }

    [Required] public Gender Gender { get; set; }

    public ICollection<Addiction> Addictions { get; set; } = new HashSet<Addiction>();

    public void UpdateFromDTO(PatientDTO patientDTO)
    {
        FirstName = patientDTO.FirstName;
        LastName = patientDTO.LastName;
        Email = patientDTO.Email;
        PhoneNumber = patientDTO.PhoneNumber;
        BirthDate = patientDTO.BirthDate;
        Gender = patientDTO.Gender;
    }
}