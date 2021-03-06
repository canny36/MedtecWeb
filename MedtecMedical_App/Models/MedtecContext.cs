﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace MedtecMedical_App.Models
{
    public class MedtecContext : DbContext
    {
        public DbSet<PracticeUser> PracticeUsers { get; set; }
        public DbSet<Practice> Practices { get; set; }
        public DbSet<vwPracticeUser> vwPracticeUsers { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<EquipmentAccessories> EquipmentAccessorys { get; set; }
        public DbSet<vwPractice> vwPractices { get; set; }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<PatientVisits> PatientVisitsList { get; set; } 
        //public DbSet<PatientEncounters> encounters { get; set; }

        public DbSet<PatientEncounters> PatientEncountersList { get; set; }
        public DbSet<vwEquipmentAccessories> vwEquipmentAccessoriesList { get; set; }
       // public DbSet<Encounter_Accessories> encAccessories { get; set; }

        public DbSet<Encounter_Accessories> Encounter_AccessoriesList { get; set; }
        public DbSet<Status> StatusList { get; set; }
        public DbSet<vwPatient_PatientEncounters> vwPatients { get; set; }
        public DbSet<vwSearchPatients> vwSearch { get; set; }
        public DbSet<vwPDFGeneration> vwPDFGen { get; set; }
        public DbSet<vwPdfGenEncounterInfo> vwPDFGenEncInfo { get; set; }
        public DbSet<vwPdfGenEnconterAccess> vwPDFGenEncAcc { get; set; }
        public DbSet<vw_Messages> vwMsg { get; set; }
        public DbSet<vwBillerMessages> vwBillerMessagesList { get; set; }

        public DbSet<vwTodaySchedule> vwTodayScheduleList { get; set; }
        

        //public DbSet<ModelsForMobile.iPadEncounters> iPadEncountersList { get; set; }

        public DbSet<Encounter_Messages> Enc_Msg { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}