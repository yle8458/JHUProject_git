using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.EntityFrameworkCore;
using JHUProject.Models;

namespace JHUProject.Daos
{
    public class JHUDaosInitializer 
    {
        public static void Initialize(JHUContext context)
        {
            context.Database.EnsureCreated();

            if (context.Patients.Any())
                return;

            var patients = new List<Patient>
            {
            new Patient{PatientID=1,FirstName="Amy",LastName="Anderson",RegisterDate=DateTime.Parse("2017-09-01"),CreatedDate=DateTime.Parse("2017-09-05"),DateOfBirth=DateTime.Parse("1980-02-01"),},
            new Patient{PatientID=2,FirstName="Brian",LastName="Bush",RegisterDate=DateTime.Parse("2017-10-21"),CreatedDate=DateTime.Parse("2017-11-01"),DateOfBirth=DateTime.Parse("1981-01-21"),}
            };

            patients.ForEach(s => context.Patients.Add(s));
            context.SaveChanges();
            var clinicians = new List<Clinician>
            {
            new Clinician{ClinicianID=1, FirstName="Danial",LastName="Dosen",CreatedDate=DateTime.Parse("2017-06-17")},
            new Clinician{ClinicianID=2, FirstName="Ema",LastName="Eason",CreatedDate=DateTime.Parse("2017-11-02")},

            };
            clinicians.ForEach(s => context.Clinicians.Add(s));
            context.SaveChanges();
            var biopsies = new List<Biopsy>
            {
            new Biopsy{PatientID=1,BiopsyID=1,RecordNumber="JH1234568", ClinicianID=1,ServiceDate=DateTime.Parse("2017-01-02"), BiopsySite="Distal Leg",},
            new Biopsy{PatientID=1,BiopsyID=2,RecordNumber="JH1213431", ClinicianID=2,ServiceDate=DateTime.Parse("2017-01-03"), BiopsySite="Distal Thigh",},
            new Biopsy{PatientID=2,BiopsyID=3,RecordNumber="JH1231101", ClinicianID=1,ServiceDate=DateTime.Parse("2017-02-02"), BiopsySite="Proximal Thigh",},
            };
            biopsies.ForEach(s => context.Biopsies.Add(s));
            context.SaveChanges();
        }
    }
}
